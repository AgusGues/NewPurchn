using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BusinessFacade;
using Domain;
using System.IO;
using DataAccessLayer;

namespace GRCweb1.Modul.Budgeting
{
    public partial class BudgetingLogistikNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                string[] costpj = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("costpj", "CostControl").Split(',');
                int post = Array.IndexOf(costpj, ((Users)Session["Users"]).UserName.ToString());
                btnSimpan.Enabled = (post > -1) ? true : false;
                LoadBulan();
                LoadTahun();
                Session["Nilai"] = null;
                Session["Nilai1"] = null;

                int LastDay = DateTime.DaysInMonth(int.Parse(ddlTahun.SelectedValue.ToString()), int.Parse(ddlBulan.SelectedValue.ToString()));
                txtTglMulai.Text = LastDay.ToString().PadLeft(2, '0') + "-" + ddlBulan.SelectedValue.PadLeft(2, '0') + "-" + ddlTahun.SelectedValue.ToString();
            }
         ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('thr', 450, 99 , 80 ,false); </script>", false);
        }

        private void LoadTahun()
        {
            PakaiFacade pd = new PakaiFacade();
            pd.GetTahun(ddlTahun);
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }

        private void LoadBulan()
        {
            ddlBulan.Items.Clear();
            //ddlBulan.Items.Add(new ListItem("--Pilih Bulan--", "0"));
            for (int i = 1; i < 13; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i).ToString(), i.ToString()));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            Session["Nilai"] = null;
            Session["Nilai1"] = null;
            btnSimpan.Enabled = true;

            Repeater rPengiriman = (Repeater)lstPengiriman;
            for (int x = 0; x < rPengiriman.Items.Count; x++)
            {
                Repeater lstMatrix = (Repeater)rPengiriman.Items[x].FindControl("lstMatrix");
                for (int i = 0; i < lstMatrix.Items.Count; i++)
                {
                    TextBox txt = (TextBox)lstMatrix.Items[i].FindControl("jmlDelivery");
                    txt.Text = "";
                }
            }
            Response.Redirect("BudgetingLogistikNew.aspx");
        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            CostCenterFacade cp = new CostCenterFacade();
            if (Session["Nilai"] != null)
            {
                int rst = 0;
                ArrayList arrD = (ArrayList)Session["Nilai"];
                foreach (CostPJ pj in arrD)
                {
                    CostPJ pjj = new CostPJ();
                    pjj = pj;
                    rst = (pj.ID == 0) ? cp.InsertNilai(pjj) : cp.InsertNilai(pjj, true);
                }
                if (rst > 0)
                {
                    btnSimpan.Enabled = false;
                    Session["Nilai"] = null;
                }
            }
            if (Session["Nilai1"] != null)
            {
                int rst = 0;
                ArrayList arrD = (ArrayList)Session["Nilai1"];
                foreach (CostPJ pj in arrD)
                {
                    CostPJ pjj = new CostPJ();
                    pjj = pj;
                    rst = (pj.ID == 0) ? cp.InsertNilai(pjj) : cp.InsertNilai(pjj, true);
                }
                if (rst > 0)
                {
                    btnSimpan.Enabled = false;
                    Session["Nilai1"] = null;
                }
            }
            DisplayAJAXMessage(this, "Prses Simpa selesai");
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            Repeater rPengiriman = (Repeater)lstPengiriman;
            for (int x = 0; x < rPengiriman.Items.Count; x++)
            {
                Repeater lstMatrix = (Repeater)rPengiriman.Items[x].FindControl("lstMatrix");
                for (int i = 0; i < lstMatrix.Items.Count; i++)
                {
                    TextBox jml = (TextBox)lstMatrix.Items[i].FindControl("jmlDelivery");
                    Label txtjml = (Label)lstMatrix.Items[i].FindControl("txtjmlDelivery");
                    HiddenField hd = (HiddenField)lstMatrix.Items[i].FindControl("txtNilaiID");
                    if (hd != null) { hd.Visible = false; }
                    if (jml != null) { jml.Visible = false; }
                    if (txtjml != null) { txtjml.Visible = true; }
                }
            }
            rPengiriman = (Repeater)lstPengiriman1;
            for (int x = 0; x < rPengiriman.Items.Count; x++)
            {
                Repeater lstMatrix = (Repeater)rPengiriman.Items[x].FindControl("lstMatrix1");
                for (int i = 0; i < lstMatrix.Items.Count; i++)
                {
                    TextBox jml = (TextBox)lstMatrix.Items[i].FindControl("jmlDelivery");
                    Label txtjml = (Label)lstMatrix.Items[i].FindControl("txtjmlDelivery");
                    HiddenField hd = (HiddenField)lstMatrix.Items[i].FindControl("txtNilaiID");
                    if (hd != null) { hd.Visible = false; }
                    if (jml != null) { jml.Visible = false; }
                    if (txtjml != null) { txtjml.Visible = true; }
                }
            }
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=MatrixBudgetLogitic.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<b>MATRIX BUDGET LOGISTIK PRODUK JADI</b>";
            Html += "<br>Periode : " + ddlBulan.SelectedItem.Text + "  " + ddlTahun.SelectedItem.Text;
            Html += "";
            string HtmlEnd = "";
            lst.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0\"", "border=\"1\"");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            //priviewbgt();
            //priviewbgt();
            LoadPengiriman();
            LoadPengiriman1();
        }
        protected void LoadPengiriman()
        {
            ArrayList listPengiriman = new ArrayList();
            ZetroView zl = new ZetroView();
            tPKA0 = 0; tBKA0 = 0;
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "select * from MaterialCCMatrixPJPengiriman where rowstatus>-1 and pengiriman in (select pengiriman from MaterialCCMatrixPJ where tahun=" +
                DateTime.Now.Year + " and pengiriman not like '%kembali%' and  pengiriman not like '%repair%' ) order by urutan";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    listPengiriman.Add(GenerateObj(sdr));
                }
            }
            lstPengiriman.DataSource = listPengiriman;
            lstPengiriman.DataBind();
            priviewbgt();
        }
        protected void LoadPengiriman1()
        {
            ArrayList listPengiriman = new ArrayList();
            ZetroView zl = new ZetroView();
            tPKA0 = 0; tBKA0 = 0;
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "select * from MaterialCCMatrixPJPengiriman where rowstatus>-1 and pengiriman in (select pengiriman from MaterialCCMatrixPJ where tahun=" +
                DateTime.Now.Year + " and (pengiriman like '%kembali%' or pengiriman like '%repair%' ) ) order by urutan";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    listPengiriman.Add(GenerateObj(sdr));
                }
            }
            lstPengiriman1.DataSource = listPengiriman;
            lstPengiriman1.DataBind();
            priviewbgt1();
        }
        protected void priviewbgt()
        {
            ArrayList arrData = new ArrayList();
            CostCenterFacade cc = new CostCenterFacade();
            Repeater rPengiriman = (Repeater)lstPengiriman;
            for (int x = 0; x < rPengiriman.Items.Count; x++)
            {
                HtmlTableRow tkirim = (HtmlTableRow)rPengiriman.Items[x].FindControl("Pengiriman");
                Repeater lstMatrix = (Repeater)rPengiriman.Items[x].FindControl("lstMatrix");
                cc.StartPeriode = ddlTahun.SelectedValue.ToString();
                cc.StartBulan = ddlBulan.SelectedValue;
                string pengiriman = tkirim.Cells[1].InnerHtml.ToString().Trim();
                arrData = cc.RetrieveMatrixPJ1(pengiriman);
                lstMatrix.DataSource = arrData;
                lstMatrix.DataBind();
                jmlDelivery_Change(null, null);
            }

        }
        protected void priviewbgt1()
        {
            ArrayList arrData = new ArrayList();
            CostCenterFacade cc = new CostCenterFacade();
            Repeater rPengiriman = (Repeater)lstPengiriman1;
            for (int x = 0; x < rPengiriman.Items.Count; x++)
            {
                HtmlTableRow tkirim = (HtmlTableRow)rPengiriman.Items[x].FindControl("Pengiriman1");
                Repeater lstMatrix = (Repeater)rPengiriman.Items[x].FindControl("lstMatrix1");
                cc.StartPeriode = ddlTahun.SelectedValue.ToString();
                cc.StartBulan = ddlBulan.SelectedValue;
                string pengiriman = tkirim.Cells[1].InnerHtml.ToString().Trim();
                arrData = cc.RetrieveMatrixPJ1(pengiriman);
                lstMatrix.DataSource = arrData;
                lstMatrix.DataBind();
                jmlDelivery1_Change(null, null);
            }
            #region update sarmut Logistik finishgood
            string sarmutPrs = "Efisiensi Budget Logistik ";
            string strJmlLine = string.Empty;
            //string strDept = "TRANS";
            int deptid = getDeptID("LOGISTIK FINISHGOOD");
            string actual = (Convert.ToDecimal(Session["actual"].ToString())).ToString("0.##").Replace(",", ".");
            ZetroView zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery = "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue + " set @bulan=" + ddlBulan.SelectedValue + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                "update SPD_TransPrs set actual=" + actual + " where Approval=0 and tahun=@tahun and bulan=@bulan and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "')";
            SqlDataReader sdr1 = zl1.Retrieve();
            #endregion

            #region Yoga SarmutToPES
            string totalPakai = (Convert.ToDecimal(Session["TPakai"].ToString())).ToString("0.##").Replace(",", ".");
            string newactual = (Convert.ToDecimal(Session["actual"].ToString())).ToString("0.##").Replace(",", ",");
            double percentActual = Convert.ToDouble(newactual);
            ZetroView zs = new ZetroView();
            zs.QueryType = Operation.CUSTOM;
            decimal targetSarmutLogistik = 0;
            int bulan = Convert.ToInt32(ddlBulan.SelectedValue) - 1;
            zs.CustomQuery = "SELECT top 1 * FROM SPD_TransPrs WHERE SarmutPID IN (SELECT ID from SPD_Perusahaan where Rowstatus>-1 and " +
                             "SarMutPerusahaan='" + sarmutPrs + "') and Approval>1 order by id desc ";
            SqlDataReader adr = zs.Retrieve();
            if (adr.HasRows)
            {
                while (adr.Read())
                {
                    targetSarmutLogistik = (adr["Target"] == DBNull.Value) ? 0 : Convert.ToDecimal(adr["Target"]);
                }
            }
            if (Convert.ToDecimal(actual) > 0)
            {
                string strError = "";
                string ketPes = (Convert.ToDecimal(newactual) > targetSarmutLogistik) ? "Tidak Tercapai (> 100%)" : "Tercapai(100% Maks)";
                ZetroView zv = new ZetroView();
                zv.QueryType = Operation.CUSTOM;
                int IdKPI = 0;
                string kpiName = string.Empty;
                zv.CustomQuery = "SELECT * FROM ISO_KPI WHERE DeptID=6 and month(TglMulai)=" + ddlBulan.SelectedValue + " AND year(TglMulai)=" + ddlTahun.SelectedValue + " ORDER BY ID desc";
                SqlDataReader dr = zv.Retrieve();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        IdKPI = Int32.Parse(dr["ID"].ToString());
                        kpiName = dr["KPIName"].ToString();
                    }
                }
                if (kpiName == string.Empty)
                {
                    int LastDay = DateTime.DaysInMonth(int.Parse(ddlTahun.SelectedValue.ToString()), int.Parse(ddlBulan.SelectedValue.ToString()));
                    ArrayList arrSarPes = new ArrayList();
                    SarmutPESFacade sarPesFacade = new SarmutPESFacade();
                    arrSarPes = sarPesFacade.RetrieveUserCategory(sarmutPrs);
                    foreach (SarmutPes uc in arrSarPes)
                    {
                        int idUserCategory = uc.IDUserCategory;
                        int userID = uc.UserID;
                        int bagianID = uc.BagianID;
                        decimal bobotNilai = uc.BobotNilai;
                        string pic = uc.Pic;
                        int deptID = uc.DeptID;
                        string description = uc.Description;
                        int pesType = uc.PesType;
                        int categoryID = uc.CategoryID;
                        //uc.Actual = string.Concat(actual.ToString(), valueActual.ToString()) ;
                        uc.Ket = string.Concat(totalPakai, " (" + Math.Round(percentActual) + "%)");
                        //uc.Percent = valueActual.ToString();
                        //uc.TglMulai = Convert.ToDateTime(txtTglMulai.Text);
                        int pjgDept = ((Users)Session["Users"]).DeptID;
                        string ddlDept = "LOGISTIK PRODUK JADI";
                        uc.DeptName = (pjgDept >= 4) ? ddlDept.Substring(0, 3) : ddlDept.Substring(0, pjgDept);
                        txtTglMulai.Text = Convert.ToDateTime(uc.TglMulai).ToString();
                        uc.TglMulai = Convert.ToDateTime(LastDay.ToString().PadLeft(2, '0') + "-" + ddlBulan.SelectedValue.PadLeft(2, '0') + "-" + ddlTahun.SelectedValue.ToString());

                        ZetroView zx = new ZetroView();
                        zx.QueryType = Operation.CUSTOM;
                        int idSopScore = 0;
                        string targetKe = string.Empty;
                        decimal pointNilai = 0;
                        string ketActual = string.Empty;
                        zx.CustomQuery = "select ID,CategoryID,TargetKe,PointNilai from ISO_SOPScore where CategoryID=" + categoryID + " " +
                                         "and RowStatus>-1 and TargetKe='" + ketPes + "' ";
                        SqlDataReader xdr = zx.Retrieve();
                        if (xdr.HasRows)
                        {
                            while (xdr.Read())
                            {
                                idSopScore = Int32.Parse(xdr["ID"].ToString());
                                targetKe = xdr["TargetKe"].ToString();
                                pointNilai = Convert.ToDecimal(xdr["PointNilai"].ToString());
                                if (idSopScore > 0)
                                {
                                    ketActual = string.Concat(totalPakai, " (" + Math.Round(percentActual) + "%)");
                                }
                            }
                        }
                        uc.SopScoreID = idSopScore;
                        uc.KetTargetKe = targetKe;
                        uc.PointNilai = pointNilai;
                        uc.Actual = ketActual;

                        arrData.Add(uc);
                        strError = SimpanKPI(uc);
                    }
                }
                else
                {
                    ArrayList arrSarPesUPdate = new ArrayList();
                    ArrayList arrSarPesUPdate2 = new ArrayList();
                    SarmutPESFacade updatesarPesFacade = new SarmutPESFacade();
                    arrSarPesUPdate = updatesarPesFacade.RetrieveUserCategory2(sarmutPrs);
                    foreach (SarmutPes up in arrSarPesUPdate)
                    {
                        int categoryID = up.CategoryID;
                        int deptID = up.DeptID;

                        arrSarPesUPdate2 = updatesarPesFacade.RetrieveID(deptID, ddlBulan.SelectedValue, ddlTahun.SelectedValue, categoryID);
                        foreach (SarmutPes tp in arrSarPesUPdate2)
                        {
                            //SarmutPes updateSarPes = updatesarPesFacade.RetrieveID(deptID, ddlBulan.SelectedValue, ddlTahun.SelectedValue, categoryID);
                            int id = tp.ID;

                            SarmutPes updateSarPesScore = updatesarPesFacade.RetrieveUpdateScore(categoryID, ketPes);
                            int IDScore = updateSarPesScore.IDScore;
                            int CategoryID = updateSarPesScore.CategoryID;
                            string TargetKe = updateSarPesScore.KetTargetKe;
                            decimal PointNilai = updateSarPesScore.PointNilai;

                            up.KPIID = id;
                            //up.Ket = string.Concat(totalPakai, " (" + Math.Round(percentActual) + "%)");
                            up.Ket = Math.Round(percentActual) + "%";
                            up.SopScoreID = IDScore;
                            up.KetTargetKe = TargetKe;
                            up.PointNilai = PointNilai;
                            arrData.Add(up);
                            strError = UpdateKPI(up);
                        }
                    }
                }
            }
            #endregion



            #region Update Nilai ke DataBase sebelum Update ke Buku Besar ISO, Update ke Buku Besar ketika Apv Manager di Apv SarMut

            string Persen = Session["actual"].ToString().Replace(",", ".");
            string TotalPakai = string.Empty ;
            if (Session["TotalPakai"] != null )
                TotalPakai = Session["TotalPakai"].ToString();
            else
                TotalPakai = "0";

            string TotalBudget = string.Empty;
            if (Session["TotalBudget"] != null)
                TotalBudget = Session["TotalBudget"].ToString();
            else
                TotalBudget = "0";

            Users users = (Users)Session["Users"];
            decimal TotalBudgetPerBulan = 0;
            decimal ActualSPBPerBulan = 0;
            string Tahun = string.Empty;
            string NilaiBulan = string.Empty;
            string NamaBulan = string.Empty;
            string Value = string.Empty;

            /** Jika Dept. Logistik FinishGood maka  **/
            if (users.DeptID > 6)
            {
                NilaiBulan = ddlBulan.SelectedValue;
                Tahun = ddlTahun.SelectedValue;
                if (NilaiBulan == "1") { NamaBulan = "Jan"; }
                else if (NilaiBulan == "2") { NamaBulan = "Feb"; }
                else if (NilaiBulan == "3") { NamaBulan = "Mar"; }
                else if (NilaiBulan == "4") { NamaBulan = "Apr"; }
                else if (NilaiBulan == "5") { NamaBulan = "Mei"; }
                else if (NilaiBulan == "6") { NamaBulan = "Jun"; }
                else if (NilaiBulan == "7") { NamaBulan = "Jul"; }
                else if (NilaiBulan == "8") { NamaBulan = "Agu"; }
                else if (NilaiBulan == "9") { NamaBulan = "Sep"; }
                else if (NilaiBulan == "10") { NamaBulan = "Okt"; }
                else if (NilaiBulan == "11") { NamaBulan = "Nov"; } else if (NilaiBulan == "12") { NamaBulan = "Des"; }


                TotalBudgetPerBulan = Convert.ToDecimal(TotalBudget);
                ActualSPBPerBulan = Convert.ToDecimal(TotalPakai);

                ArrayList arrBB = new ArrayList();
                FacadeBudgetLogistik F = new FacadeBudgetLogistik();
                arrBB = F.RetrieveMapping("Efisiensi Budget Logistik");
                if (arrBB.Count > 0)
                {
                    int i = 0;
                    foreach (BudgetLogistik List in arrBB)
                    {
                        if (List.Flag == "TotalBudget")
                        {
                            Value = TotalBudgetPerBulan.ToString();
                        }
                        else if (List.Flag == "TotalPakai")
                        {
                            Value = ActualSPBPerBulan.ToString();
                        }
                        else if (List.Flag == "Persen")
                        {
                            Value = Persen.ToString().Replace(",", ".");
                        }

                        ZetroView z0 = new ZetroView();
                        z0.QueryType = Operation.CUSTOM;
                        z0.CustomQuery =

                        "insert into BukuBesar_Mapping (Bulan,Tahun,Nilai,ParameterTerukur,InitialBulan,CreatedBy,CreatedTime,RowStatus,ItemSarmut) " +
                        "values " +
                        "('" + ddlBulan.SelectedValue + "','" + ddlTahun.SelectedValue + "','" + Value + "','" + List.ParameterTerukur + "','" + NamaBulan +
                             "','" + users.UserName + "'," + "GetDate()" + "," + "0" + ",'" + List.ItemSarmut + "')";

                        SqlDataReader sd0 = z0.Retrieve();
                    }
                    i = i + 1;
                }


            }
            #endregion

        }
        private string SimpanKPI(SarmutPes sop)
        {
            string strEvent = "Insert";
            ISO_DocumentNoFacade docNoFacade = new ISO_DocumentNoFacade();
            ISO_DocumentNo docNo = docNoFacade.RetrieveByDept(3, 3, Convert.ToDateTime(txtTglMulai.Text).Year);
            if (docNo.ID > 0)
            {
                docNo.DocNo = docNo.DocNo + 1;
                //docNo.PesType = int.Parse(txtPES.Text); //sop
                docNo.DeptID = 2;
                docNo.Tahun = Convert.ToDateTime(txtTglMulai.Text).Year;
                Company cp = new CompanyFacade().RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                docNo.Plant = cp.KodeLokasi;
            }
            else
            {
                docNo.DocNo = 1;
                //docNo.PesType = int.Parse(txtPES.Text); //sop
                docNo.DeptID = 2;
                docNo.Tahun = Convert.ToDateTime(txtTglMulai.Text).Year;
                //HO ikut C dulu
                Company cp = new CompanyFacade().RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                docNo.Plant = cp.KodeLokasi;
            }

            SarmutPESProcessFacade sarpesProcessFacade = new SarmutPESProcessFacade(sop, docNo);
            string strError = string.Empty;
            strError = sarpesProcessFacade.Insert();
            if (strError == string.Empty)
            {
                //InsertLog(strEvent);
                //txtTaskNo.Text = "Doc No. : " + sarpesProcessFacade.sopNonya;
            }
            return strError;
        }
        private string UpdateKPI(SarmutPes sop)
        {
            string strEvent = "Update";
            ISO_DocumentNoFacade docNoFacade = new ISO_DocumentNoFacade();
            ISO_DocumentNo docNo = docNoFacade.RetrieveByDept(3, 3, Convert.ToInt32(ddlTahun.SelectedValue));
            if (docNo.ID > 0)
            {
                docNo.DocNo = docNo.DocNo + 1;
                //docNo.PesType = int.Parse(txtPES.Text); //sop
                docNo.DeptID = 6;
                docNo.Tahun = Convert.ToDateTime(txtTglMulai.Text).Year;
                Company cp = new CompanyFacade().RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                docNo.Plant = cp.KodeLokasi;
            }
            else
            {
                docNo.DocNo = 1;
                //docNo.PesType = int.Parse(txtPES.Text); //sop
                docNo.DeptID = 6;
                docNo.Tahun = Convert.ToDateTime(txtTglMulai.Text).Year;
                //HO ikut C dulu
                Company cp = new CompanyFacade().RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                docNo.Plant = cp.KodeLokasi;
            }

            SarmutPESProcessFacade sarpesProcessFacade = new SarmutPESProcessFacade(sop, docNo);
            string strError = string.Empty;
            strError = sarpesProcessFacade.UpdateKpi();
            if (strError == string.Empty)
            {
                //InsertLog(strEvent);
                //txtTaskNo.Text = "Doc No. : " + sarpesProcessFacade.sopNonya;
            }
            return strError;
        }
        protected void ddlBulan_Change(object sender, EventArgs e)
        {
            int LastDay = DateTime.DaysInMonth(int.Parse(ddlTahun.SelectedValue.ToString()), int.Parse(ddlBulan.SelectedValue.ToString()));
            txtTglMulai.Text = LastDay.ToString().PadLeft(2, '0') + "-" + ddlBulan.SelectedValue.PadLeft(2, '0') + "-" + ddlTahun.SelectedValue.ToString();
        }
        private decimal TotalPakai = 0;
        private decimal TotalStd = 0;
        private decimal tPKA0 = 0; private decimal tBKA0 = 0; private int tDlv0 = 0; private decimal TminP0 = 0; private decimal TminB0 = 0;
        private decimal tPKA1 = 0; private decimal tBKA1 = 0; private int tDlv1 = 0; private decimal TminP1 = 0; private decimal TminB1 = 0;
        protected void lstMatrix_DataBound(object sender, RepeaterItemEventArgs e)
        {

            //TotalPakai = 0;
            //TotalStd = 0;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CostPJ pj = (CostPJ)e.Item.DataItem;
                TextBox jml = (TextBox)e.Item.FindControl("jmlDelivery");
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("lst");
                HtmlTableRow trF = (HtmlTableRow)tb.Controls[0].FindControl("lstF");
                HiddenField hd = (HiddenField)e.Item.FindControl("txtNilaiID");
                Label txtJml = (Label)e.Item.FindControl("txtJmlDelivery");
                Label lblPengiriman = (Label)e.Item.FindControl("lblPengiriman");
                /*
                 * ambil data perolehan nilai per pengiriman dan jenispacking
                 * bulan dan tahun terpilij
                 */
                CostCenterFacade cp = new CostCenterFacade();

                //cp.Criteria += " AND Urutan=" + tr.Cells[0].InnerHtml;
                CostPJ cpj = new CostPJ();
                int post = 0;
                if (Convert.ToInt32(ddlBulan.SelectedValue) >= 4 && Convert.ToInt32(ddlTahun.SelectedValue) >= 2020
                || Convert.ToInt32(ddlBulan.SelectedValue) >= 1 && Convert.ToInt32(ddlTahun.SelectedValue) > 2020)
                {
                    cp.Criteria = " AND Month(tgltrans)=" + ddlBulan.SelectedValue;
                    cp.Criteria += " AND Year(tgltrans)=" + ddlTahun.SelectedValue;
                    cp.Criteria += " AND Pengiriman='" + lblPengiriman.Text.Trim() + "'";
                    cp.Criteria += " AND JenisPalet='" + tr.Cells[2].InnerHtml.Trim() + "'";
                    cpj = cp.RetrieveNilaiP();
                    post = -1;
                    jml.Visible = (post > -1) ? true : false;
                    txtJml.Visible = (post > -1) ? false : true;
                }
                else
                {
                    cp.Criteria = " AND Bulan=" + ddlBulan.SelectedValue;
                    cp.Criteria += " AND Tahun=" + ddlTahun.SelectedValue;
                    cp.Criteria += " AND Pengiriman='" + lblPengiriman.Text.Trim() + "'";
                    cp.Criteria += " AND JenisPacking='" + tr.Cells[2].InnerHtml.Trim() + "'";
                    cpj = cp.RetrieveNilai();
                    string[] costpj = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("costpj", "CostControl").Split(',');
                    post = Array.IndexOf(costpj, ((Users)Session["Users"]).UserName.ToString());
                    jml.Visible = (post > -1) ? true : false;
                    txtJml.Visible = (post > -1) ? false : true;
                }

                jml.Text = (cpj.Nilai > 0) ? cpj.Nilai.ToString("N0") : "";
                txtJml.Text = (cpj.Nilai > 0) ? cpj.Nilai.ToString("N0") : "";
                hd.Value = (cpj.Nilai > 0) ? cpj.ID.ToString() : "";

                if (lblPengiriman.Text.Trim().ToUpper() != "PALET STOCK YANG TIDAK KEMBALI")
                {
                    if (jml.Text.Trim() != string.Empty)
                    {
                        tDlv0 = tDlv0 + Convert.ToInt32(jml.Text.Replace(".", ""));

                    }
                }
                if (lblPengiriman.Text == "Repair")
                {
                    tr.Cells[5].InnerHtml = ((decimal.Parse(tr.Cells[4].InnerHtml)).ToString("N0"));
                    tr.Cells[4].InnerHtml = string.Empty;
                    tr.Cells[9].InnerHtml = ((decimal.Parse(tr.Cells[8].InnerHtml)).ToString("N0"));
                    tr.Cells[8].InnerHtml = string.Empty;
                }
                try
                {
                    tPKA0 += (tr.Cells[5].InnerHtml != string.Empty) ? decimal.Parse(tr.Cells[5].InnerHtml.ToString()) : 0;
                    tBKA0 += (tr.Cells[9].InnerHtml != string.Empty) ? decimal.Parse(tr.Cells[9].InnerHtml.ToString()) : 0;
                }
                catch { }
            }
        }
        protected void lstMatrix1_DataBound(object sender, RepeaterItemEventArgs e)
        {

            //TotalPakai = 0;
            //TotalStd = 0;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CostPJ pj = (CostPJ)e.Item.DataItem;
                TextBox jml = (TextBox)e.Item.FindControl("jmlDelivery");
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("lst1");
                HtmlTableRow trF = (HtmlTableRow)tb.Controls[0].FindControl("lstF");
                HiddenField hd = (HiddenField)e.Item.FindControl("txtNilaiID");
                Label txtJml = (Label)e.Item.FindControl("txtJmlDelivery");
                Label lblPengiriman = (Label)e.Item.FindControl("lblPengiriman");

                /*
                 * ambil data perolehan nilai per pengiriman dan jenispacking
                 * bulan dan tahun terpilij
                 */
                CostCenterFacade cp = new CostCenterFacade();
                cp.Criteria = " AND Bulan=" + ddlBulan.SelectedValue;
                cp.Criteria += " AND Tahun=" + ddlTahun.SelectedValue;
                cp.Criteria += " AND Pengiriman='" + lblPengiriman.Text.Trim() + "'";
                cp.Criteria += " AND JenisPacking='" + tr.Cells[2].InnerHtml.Trim() + "'";
                //cp.Criteria += " AND Urutan=" + tr.Cells[0].InnerHtml;
                CostPJ cpj = cp.RetrieveNilai();
                jml.Text = (cpj.Nilai > 0) ? cpj.Nilai.ToString("N0") : "";
                txtJml.Text = (cpj.Nilai > 0) ? cpj.Nilai.ToString("N0") : "";
                hd.Value = (cpj.Nilai > 0) ? cpj.ID.ToString() : "";
                string[] costpj = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("costpj", "CostControl").Split(',');
                int post = Array.IndexOf(costpj, ((Users)Session["Users"]).UserName.ToString());
                jml.Visible = (post > -1) ? true : false;
                txtJml.Visible = (post > -1) ? false : true;
                Session["tkirim"] = lblPengiriman.Text.Trim();
                if (lblPengiriman.Text.Trim().ToUpper() == "PALET STOCK YANG TIDAK KEMBALI")
                {
                    if (jml.Text.Trim() != string.Empty)
                    {
                        tDlv1 = tDlv1 + Int32.Parse(jml.Text.Replace(".", ""));
                        if (lblPengiriman.Text.Trim() != "Repair")
                        {
                            tr.Cells[5].InnerHtml = (decimal.Parse(jml.Text.Replace(".", "")) * decimal.Parse(tr.Cells[4].InnerHtml)).ToString("N0");
                            tr.Cells[9].InnerHtml = (decimal.Parse(jml.Text.Replace(".", "")) * decimal.Parse(tr.Cells[8].InnerHtml)).ToString("N0");
                        }
                        tr.Cells[6].InnerHtml = (decimal.Parse(jml.Text.Replace(".", "")) * decimal.Parse(tr.Cells[4].InnerHtml) * -1).ToString("N0");
                        tr.Cells[10].InnerHtml = (decimal.Parse(jml.Text.Replace(".", "")) * decimal.Parse(tr.Cells[8].InnerHtml) * -1).ToString("N0");
                    }
                    try
                    {
                        TminP1 += (tr.Cells[6].InnerHtml != string.Empty) ? decimal.Parse(tr.Cells[6].InnerHtml.ToString()) :
                                  decimal.Parse(tr.Cells[6].InnerHtml.ToString()) * -1;
                        TminB1 += (tr.Cells[10].InnerHtml != string.Empty) ? decimal.Parse(tr.Cells[10].InnerHtml.ToString()) :
                            decimal.Parse(tr.Cells[10].InnerHtml.ToString()) * -1;
                    }
                    catch { }
                }
                else
                {
                    if (lblPengiriman.Text == "Repair")
                    {
                        jml.Visible = false;
                        tr.Cells[5].InnerHtml = ((decimal.Parse(tr.Cells[4].InnerHtml)).ToString("N0"));
                        tr.Cells[4].InnerHtml = string.Empty;
                        tr.Cells[9].InnerHtml = ((decimal.Parse(tr.Cells[8].InnerHtml)).ToString("N0"));
                        tr.Cells[8].InnerHtml = string.Empty;
                        tPKA1 += (tr.Cells[5].InnerHtml != string.Empty) ? decimal.Parse(tr.Cells[5].InnerHtml.ToString()) : 0;
                        tBKA1 += (tr.Cells[9].InnerHtml != string.Empty) ? decimal.Parse(tr.Cells[9].InnerHtml.ToString()) : 0;
                        Session["tPKA1"] = tPKA1;
                        Session["tBKA1"] = tBKA1;
                        Session["TotalStd"] = Convert.ToInt32(Session["TotalStd"].ToString()) + tPKA1 + tBKA1;
                    }
                    if (jml.Text.Trim() != string.Empty)
                    {
                        tDlv1 = tDlv1 + Int32.Parse(jml.Text.Replace(".", ""));
                        tr.Cells[6].InnerHtml = (decimal.Parse(jml.Text.Replace(".", "")) * decimal.Parse(tr.Cells[4].InnerHtml)).ToString("N0");
                        tr.Cells[10].InnerHtml = (decimal.Parse(jml.Text.Replace(".", "")) * decimal.Parse(tr.Cells[8].InnerHtml)).ToString("N0");
                    }
                    try
                    {
                        TminP1 += (tr.Cells[6].InnerHtml != string.Empty) ? decimal.Parse(tr.Cells[6].InnerHtml.ToString()) :
                                  decimal.Parse(tr.Cells[6].InnerHtml.ToString());
                        TminB1 += (tr.Cells[10].InnerHtml != string.Empty) ? decimal.Parse(tr.Cells[10].InnerHtml.ToString()) :
                            decimal.Parse(tr.Cells[10].InnerHtml.ToString());
                    }
                    catch { }
                }

            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                HtmlTableRow trfooter = (HtmlTableRow)e.Item.FindControl("ftr1");
                trfooter.Cells[1].InnerHtml = "Total " + Session["tkirim"].ToString().Trim();
                trfooter.Cells[2].InnerHtml = tDlv1.ToString("N0");
                trfooter.Cells[4].InnerHtml = tPKA1.ToString("N0");
                trfooter.Cells[8].InnerHtml = tBKA1.ToString("N0");
                trfooter.Cells[5].InnerHtml = TminP1.ToString("N0");
                trfooter.Cells[9].InnerHtml = TminB1.ToString("N0");
                //if (Session["TminP1"] != null)
                //{
                Session["TminP1"] = Convert.ToInt32(Session["TminP1"].ToString()) + TminP1;
                Session["TminB1"] = Convert.ToInt32(Session["TminB1"].ToString()) + TminB1;
                //}
                tPKA1 = 0; tBKA1 = 0; TminP1 = 0; TminB1 = 0; tDlv1 = 0;

            }
        }

        protected void jmlDelivery_Change(object sender, EventArgs e)
        {
            decimal tPKA = 0; decimal tBKA = 0; int tDlv = 0;
            decimal TminP = 0; decimal TminB = 0;
            TotalPakai = 0;
            TotalStd = 0;
            ArrayList arrData = /*(Session["Nilai"] != null) ? (ArrayList)Session["Nilai"] :*/ new ArrayList();
            CostCenterFacade cp = new CostCenterFacade();
            cp.Criteria = " AND MONTH(PakaiDate)=" + ddlBulan.SelectedValue + " AND Year(PakaiDate)=" + ddlTahun.SelectedValue + " AND MatCCGroupID=15 ";
            //trfooter.Cells[4].InnerHtml = cp.RetrieveNilai(true).ToString("N0");
            TotalPakai += cp.RetrieveNilai(true) + TminP;
            //BKA
            cp.Criteria = " AND MONTH(PakaiDate)=" + ddlBulan.SelectedValue + " AND Year(PakaiDate)=" + ddlTahun.SelectedValue + " AND MatCCGroupID=16 ";
            //trfooter.Cells[8].InnerHtml = cp.RetrieveNilai(true).ToString("N0");
            TotalPakai += cp.RetrieveNilai(true) + TminB;

            Repeater rPengiriman = (Repeater)lstPengiriman;
            for (int x = 0; x < rPengiriman.Items.Count; x++)
            {
                Repeater lstMatrix = (Repeater)rPengiriman.Items[x].FindControl("lstMatrix");
                for (int i = 0; i < lstMatrix.Items.Count; i++)
                {
                    HtmlTableRow tr = (HtmlTableRow)lstMatrix.Items[i].FindControl("lst");
                    CostPJ pj = new CostPJ();
                    TextBox jml = (TextBox)lstMatrix.Items[i].FindControl("jmlDelivery");
                    HiddenField hd = (HiddenField)lstMatrix.Items[i].FindControl("txtNilaiID");
                    Label lblPengiriman = (Label)lstMatrix.Items[i].FindControl("lblPengiriman");
                    string jmlL5S = jml.Text.Replace(".", "");
                    jml.Text = jmlL5S;
                    if (jml.Text != string.Empty)
                    {
                        if (lblPengiriman.Text.Trim().ToUpper() != "PALET STOCK YANG TIDAK KEMBALI")
                        {
                            tDlv = tDlv + Int32.Parse(jml.Text.Replace(".", ""));
                            if (lblPengiriman.Text.Trim() != "Repair")
                            {
                                tr.Cells[5].InnerHtml = (decimal.Parse(jml.Text.Replace(".", "")) * decimal.Parse(tr.Cells[4].InnerHtml)).ToString("N0");
                                tr.Cells[9].InnerHtml = (decimal.Parse(jml.Text.Replace(".", "")) * decimal.Parse(tr.Cells[8].InnerHtml)).ToString("N0");
                            }
                            string Standare = tr.Cells[4].InnerHtml + "," +
                                tr.Cells[8].InnerHtml + ",";
                            tPKA += (tr.Cells[5].InnerHtml != string.Empty) ? decimal.Parse(tr.Cells[5].InnerHtml) : 0;
                            tBKA += (tr.Cells[9].InnerHtml != string.Empty) ? decimal.Parse(tr.Cells[9].InnerHtml) : 0;
                            pj.Bulan = int.Parse(ddlBulan.SelectedValue);
                            pj.Tahun = int.Parse(ddlTahun.SelectedValue);
                            pj.Pengiriman = lblPengiriman.Text.Trim();
                            pj.JenisPacking = tr.Cells[2].InnerHtml.ToString().Trim();
                            pj.Urutan = int.Parse(tr.Cells[0].InnerHtml.ToString().Trim());
                            pj.Nilai = decimal.Parse(jml.Text.Replace(".", ""));
                            pj.Approval = 0;
                            pj.RowStatus = 0;
                            pj.ApprovalBy = Standare;
                            pj.ID = (hd.Value != string.Empty) ? int.Parse(hd.Value) : 0;
                            pj.CreatedBy = ((Users)Session["Users"]).UserName;
                        }
                        else
                        {
                            tDlv = tDlv + Int32.Parse(jml.Text.Replace(".", ""));
                            if (lblPengiriman.Text.Trim() != "Repair")
                            {
                                if (lblPengiriman.Text.Trim().ToUpper() != "PALET STOCK YANG TIDAK KEMBALI")
                                {
                                    tr.Cells[5].InnerHtml = (decimal.Parse(jml.Text.Replace(".", "")) * decimal.Parse(tr.Cells[4].InnerHtml)).ToString("N0");
                                    tr.Cells[9].InnerHtml = (decimal.Parse(jml.Text.Replace(".", "")) * decimal.Parse(tr.Cells[8].InnerHtml)).ToString("N0");
                                }
                                else
                                {
                                    tr.Cells[5].InnerHtml = "0";
                                    tr.Cells[9].InnerHtml = "0";
                                }
                                tr.Cells[6].InnerHtml = (decimal.Parse(jml.Text.Replace(".", "")) * (decimal.Parse(tr.Cells[4].InnerHtml)) * -1).ToString("N0");
                                tr.Cells[10].InnerHtml = (decimal.Parse(jml.Text.Replace(".", "")) * (decimal.Parse(tr.Cells[8].InnerHtml)) * -1).ToString("N0");
                            }
                            string Standare = tr.Cells[4].InnerHtml + "," +
                                tr.Cells[8].InnerHtml + ",";
                            tPKA += (tr.Cells[5].InnerHtml != string.Empty) ? decimal.Parse(tr.Cells[5].InnerHtml) : 0;
                            tBKA += (tr.Cells[9].InnerHtml != string.Empty) ? decimal.Parse(tr.Cells[9].InnerHtml) : 0;
                            TminP += (tr.Cells[6].InnerHtml != string.Empty) ? decimal.Parse(tr.Cells[6].InnerHtml) : decimal.Parse(tr.Cells[6].InnerHtml) * -1;
                            TminB += (tr.Cells[10].InnerHtml != string.Empty) ? decimal.Parse(tr.Cells[10].InnerHtml) : decimal.Parse(tr.Cells[10].InnerHtml) * -1;
                            pj.Bulan = int.Parse(ddlBulan.SelectedValue);
                            pj.Tahun = int.Parse(ddlTahun.SelectedValue);
                            pj.Pengiriman = lblPengiriman.Text.Trim();
                            pj.JenisPacking = tr.Cells[2].InnerHtml.ToString().Trim();
                            pj.Urutan = int.Parse(tr.Cells[0].InnerHtml.ToString().Trim());
                            pj.Nilai = decimal.Parse(jml.Text.Replace(".", ""));
                            pj.Approval = 0;
                            pj.RowStatus = 0;
                            pj.ApprovalBy = Standare;
                            pj.ID = (hd.Value != string.Empty) ? int.Parse(hd.Value) : 0;
                            pj.CreatedBy = ((Users)Session["Users"]).UserName;
                        }

                    }
                    else
                    {
                        if (lblPengiriman.Text.Trim() == "Repair")
                        {
                            tr.Cells[5].InnerHtml = ((decimal.Parse(tr.Cells[5].InnerHtml)).ToString("N0"));
                            tr.Cells[9].InnerHtml = (decimal.Parse(tr.Cells[9].InnerHtml)).ToString("N0");
                            string Standare = tr.Cells[4].InnerHtml + "," +
                                tr.Cells[8].InnerHtml + ",";
                            tPKA += (tr.Cells[5].InnerHtml != string.Empty) ? decimal.Parse(tr.Cells[5].InnerHtml) : 0;
                            tBKA += (tr.Cells[9].InnerHtml != string.Empty) ? decimal.Parse(tr.Cells[9].InnerHtml) : 0;

                            pj.Bulan = int.Parse(ddlBulan.SelectedValue);
                            pj.Tahun = int.Parse(ddlTahun.SelectedValue);
                            pj.Pengiriman = lblPengiriman.Text.Trim();
                            pj.JenisPacking = tr.Cells[2].InnerHtml.ToString().Trim();
                            pj.Urutan = int.Parse(tr.Cells[0].InnerHtml.ToString().Trim());
                            pj.Nilai = (jml.Text != string.Empty) ? decimal.Parse(jml.Text.Replace(".", "")) : 0;
                            pj.Approval = 0;
                            pj.RowStatus = 0;
                            pj.ApprovalBy = Standare;
                            pj.ID = (hd.Value != string.Empty) ? int.Parse(hd.Value) : 0;
                            pj.CreatedBy = ((Users)Session["Users"]).UserName;
                        }
                        hd.Value = "0";
                        pj.ID = 0;
                    }
                    arrData.Add(pj);
                }
            }
            Session["Nilai"] = arrData;
            HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstF");
            trfooter.Cells[3].InnerHtml = tPKA.ToString("N0");
            trfooter.Cells[7].InnerHtml = tBKA.ToString("N0");
            Session["TminB1"] = null;


            try
            {
                //trfooter.Cells[5].InnerHtml = (trfooter.Cells[3].InnerHtml != "0" && trfooter.Cells[4].InnerHtml != "&nbsp;") ? 
                //    (decimal.Parse(trfooter.Cells[4].InnerHtml) / decimal.Parse(trfooter.Cells[3].InnerHtml) * 100).ToString("N2") : "0";
                trfooter.Cells[5].InnerHtml = "";
                TotalStd += decimal.Parse(trfooter.Cells[3].InnerHtml.ToString());
                trfooter.Cells[4].InnerHtml = (decimal.Parse(trfooter.Cells[4].InnerHtml)).ToString("N0");
                //trfooter.Cells[9].InnerHtml = (decimal.Parse(trfooter.Cells[7].InnerHtml) > 0 && trfooter.Cells[8].InnerHtml != "&nbsp;") ? 
                //    (decimal.Parse(trfooter.Cells[8].InnerHtml) / decimal.Parse(trfooter.Cells[7].InnerHtml) * 100).ToString("N2") : "0";
                trfooter.Cells[9].InnerHtml = "";
                TotalStd += decimal.Parse(trfooter.Cells[7].InnerHtml);

                trfooter.Cells[1].InnerHtml = tDlv.ToString("N0");
            }
            catch
            {
                trfooter.Cells[5].InnerHtml = "";
                trfooter.Cells[9].InnerHtml = "";

            }
            Session["tPKA"] = tPKA;
            Session["tBKA"] = tBKA;
            Session["TotalStd"] = TotalStd;
            trfooter.Cells[5].Attributes["style"] = "white-space:nowrap;text-align:right";
            trfooter.Cells[9].Attributes["style"] = "white-space:nowrap;text-align:right";

            /*
             * Dapatkan Data dari SPB
             */

            //PKA
            cp.Criteria = " AND MONTH(PakaiDate)=" + ddlBulan.SelectedValue + " AND Year(PakaiDate)=" + ddlTahun.SelectedValue + " AND MatCCGroupID=15 ";
            trfooter.Cells[4].InnerHtml = cp.RetrieveNilai(true).ToString("N0");
            TotalPakai += TminP;
            //BKA
            cp.Criteria = " AND MONTH(PakaiDate)=" + ddlBulan.SelectedValue + " AND Year(PakaiDate)=" + ddlTahun.SelectedValue + " AND MatCCGroupID=16 ";
            trfooter.Cells[8].InnerHtml = cp.RetrieveNilai(true).ToString("N0");
            TotalPakai += TminB;

            Session["actual"] = null;
            //jmlPakai.Text = TotalPakai.ToString("N2");
            //jmlStd.Text = TotalStd.ToString("N2");
            //jmlPros.Text = (TotalStd > 0) ? (TotalPakai / TotalStd).ToString("P2") : "";
            if (TotalStd > 0)
                Session["actual"] = (TotalPakai / TotalStd) * 100;
            else
                Session["actual"] = 0;
            Session["TPakai"] = TotalPakai;
            Session["TPakaiP"] = trfooter.Cells[4].InnerHtml.ToString();
            Session["TPakaiB"] = trfooter.Cells[8].InnerHtml.ToString();
            Session["TStdP"] = trfooter.Cells[3].InnerHtml.ToString();
            Session["TStdB"] = trfooter.Cells[7].InnerHtml.ToString();
            Session["TminP1"] = 0;
            Session["TminB1"] = 0;

        }
        protected void jmlDelivery1_Change(object sender, EventArgs e)
        {
            //decimal tPKA = 0; decimal tBKA = 0; 
            int tDlv = 0;
            decimal TminP = 0; decimal TminB = 0;
            TotalPakai = 0;
            TotalStd = 0;
            ArrayList arrData = /*(Session["Nilai"] != null) ? (ArrayList)Session["Nilai"] :*/ new ArrayList();
            CostCenterFacade cp = new CostCenterFacade();
            //cp.Criteria = " AND MONTH(PakaiDate)=" + ddlBulan.SelectedValue + " AND Year(PakaiDate)=" + ddlTahun.SelectedValue + " AND MatCCGroupID=15 ";
            //trfooter.Cells[4].InnerHtml = cp.RetrieveNilai(true).ToString("N0");
            //TotalPakai += cp.RetrieveNilai(true) + TminP;
            //BKA
            //cp.Criteria = " AND MONTH(PakaiDate)=" + ddlBulan.SelectedValue + " AND Year(PakaiDate)=" + ddlTahun.SelectedValue + " AND MatCCGroupID=16 ";
            //trfooter.Cells[8].InnerHtml = cp.RetrieveNilai(true).ToString("N0");
            //TotalPakai += cp.RetrieveNilai(true) + TminB;

            Repeater rPengiriman = (Repeater)lstPengiriman1;
            for (int x = 0; x < rPengiriman.Items.Count; x++)
            {
                Repeater lstMatrix = (Repeater)rPengiriman.Items[x].FindControl("lstMatrix1");
                for (int i = 0; i < lstMatrix.Items.Count; i++)
                {
                    HtmlTableRow tr = (HtmlTableRow)lstMatrix.Items[i].FindControl("lst1");
                    CostPJ pj = new CostPJ();
                    TextBox jml = (TextBox)lstMatrix.Items[i].FindControl("jmlDelivery");
                    HiddenField hd = (HiddenField)lstMatrix.Items[i].FindControl("txtNilaiID");
                    Label lblPengiriman = (Label)lstMatrix.Items[i].FindControl("lblPengiriman");
                    string jmlL5S = jml.Text.Replace(".", "");
                    jml.Text = jmlL5S;
                    if (jml.Text != string.Empty)
                    {
                        tDlv = tDlv + Int32.Parse(jml.Text.Replace(".", ""));
                        if (lblPengiriman.Text.Trim().ToUpper() == "PALET STOCK YANG TIDAK KEMBALI")
                        {

                            tr.Cells[6].InnerHtml = (decimal.Parse(jml.Text.Replace(".", "")) * (decimal.Parse(tr.Cells[4].InnerHtml)) * -1).ToString("N0");
                            tr.Cells[10].InnerHtml = (decimal.Parse(jml.Text.Replace(".", "")) * (decimal.Parse(tr.Cells[8].InnerHtml)) * -1).ToString("N0");
                        }
                        else
                        {
                            if (lblPengiriman.Text != "Repair")
                            {
                                tr.Cells[6].InnerHtml = (decimal.Parse(jml.Text.Replace(".", "")) * (decimal.Parse(tr.Cells[4].InnerHtml)) * 1).ToString("N0");
                                tr.Cells[10].InnerHtml = (decimal.Parse(jml.Text.Replace(".", "")) * (decimal.Parse(tr.Cells[8].InnerHtml)) * 1).ToString("N0");
                            }
                        }
                        string Standare = tr.Cells[4].InnerHtml + "," + tr.Cells[8].InnerHtml + ",";
                        TminP += (tr.Cells[6].InnerHtml != string.Empty) ? decimal.Parse(tr.Cells[6].InnerHtml) : decimal.Parse(tr.Cells[6].InnerHtml) * -1;
                        TminB += (tr.Cells[10].InnerHtml != string.Empty) ? decimal.Parse(tr.Cells[10].InnerHtml) : decimal.Parse(tr.Cells[10].InnerHtml) * -1;
                        pj.Bulan = int.Parse(ddlBulan.SelectedValue);
                        pj.Tahun = int.Parse(ddlTahun.SelectedValue);
                        pj.Pengiriman = lblPengiriman.Text.Trim();
                        pj.JenisPacking = tr.Cells[2].InnerHtml.ToString().Trim();
                        pj.Urutan = int.Parse(tr.Cells[0].InnerHtml.ToString().Trim());
                        pj.Nilai = decimal.Parse(jml.Text.Replace(".", ""));
                        pj.Approval = 0;
                        pj.RowStatus = 0;
                        pj.ApprovalBy = Standare;
                        pj.ID = (hd.Value != string.Empty) ? int.Parse(hd.Value) : 0;
                        pj.CreatedBy = ((Users)Session["Users"]).UserName;
                    }
                    arrData.Add(pj);
                }
            }
            Session["Nilai1"] = arrData;
            try
            {
                HtmlTableRow trfooter = (HtmlTableRow)tb.Controls[0].FindControl("lstPakai");
                trfooter.Cells[2].InnerHtml =
                    ((TminP + Convert.ToDecimal(Session["TPakaiP"].ToString().Replace(".", ""))) /
                    (Convert.ToDecimal(Session["TStdP"].ToString().Replace(".", "")) +
                    Convert.ToDecimal(Session["tPKA1"].ToString().Replace(".", ""))) * 100).ToString("N2");
                decimal test1 = TminP;
                decimal test2 = Convert.ToDecimal(Session["TPakaiP"].ToString().Replace(".", ""));
                decimal test3 = Convert.ToDecimal(Session["TStdP"].ToString().Replace(".", ""));
                trfooter.Cells[4].InnerHtml =
                    ((TminB + Convert.ToDecimal(Session["TPakaiB"].ToString().Replace(".", ""))) /
                    (Convert.ToDecimal(Session["TStdB"].ToString().Replace(".", "")) +
                    Convert.ToDecimal(Session["tBKA1"].ToString().Replace(".", ""))) * 100).ToString("N2");
                HtmlTableRow trfooterP = (HtmlTableRow)tb.Controls[0].FindControl("lstSPB");
                if (Session["TPakaiP"]!=null && Session["TPakaiB"] != null )
                    trfooterP.Cells[1].InnerHtml = (Convert.ToDecimal (Session["TPakaiP"].ToString().Replace(".", "")) +
                    Convert.ToDecimal(Session["TPakaiB"].ToString().Replace(".", "")) + Convert.ToDecimal(TminP) + Convert.ToDecimal(TminB)).ToString("N0");
                decimal TP = Convert.ToDecimal(trfooterP.Cells[1].InnerHtml.ToString());
                HtmlTableRow trfooterStd = (HtmlTableRow)tb.Controls[0].FindControl("lstStandar");
                if (Session["TotalStd"]!=null)
                    trfooterStd.Cells[1].InnerHtml = Convert.ToInt32(Session["TotalStd"].ToString()).ToString("N0");
                decimal TS = Convert.ToDecimal(trfooterStd.Cells[1].InnerHtml.ToString());
                HtmlTableRow trfooterCapai = (HtmlTableRow)tb.Controls[0].FindControl("lstPencapaian");
                trfooterCapai.Cells[1].InnerHtml = ((TP / TS) * 100).ToString("N2");
                Session["actual"] = (TP / TS) * 100;
                Session["TotalPakai"] = TP;
                Session["TotalBudget"] = TS;
        }
            catch { }
        }
        protected int getDeptID(string deptName)
        {
            int result = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select id from spd_dept where dept like '%" + deptName + "%'";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Int32.Parse(sdr["ID"].ToString());
                }
            }
            return result;
        }
        bdgtKirim spc = new bdgtKirim();
        private bdgtKirim GenerateObj(SqlDataReader sdr)
        {
            spc = new bdgtKirim();
            spc.Pengiriman = sdr["Pengiriman"].ToString();
            return spc;
        }
    }
    public class bdgtKirim : GRCBaseDomain
    {
        public string Pengiriman { get; set; }
    }

    public class BudgetLogistik
    {
        public string ItemSarmut { get; set; }
        public string ParameterTerukur { get; set; }
        public string Persen { get; set; }
        public string Flag { get; set; }
        public int Rowstatus { get; set; }
    }

    public class FacadeBudgetLogistik
    {
        public string strError = string.Empty;
        private ArrayList arrData = new ArrayList();
        private BudgetLogistik objLog = new BudgetLogistik();
        private List<SqlParameter> sqlListParam;

        public string Criteria { get; set; }
        public string Field { get; set; }
        public string Where { get; set; }

        public int RetrieveApv(string Bulan, string Tahun)
        {
            string StrSql =
            " select Approval from SPD_TransPrs where SarmutPID in (select ID from SPD_Perusahaan where DeptID=2 and  " +
            " SarMutPerusahaan='Efisiensi Budget Finishing' and RowStatus>-1) and RowStatus>-1 and bulan=" + Bulan + " and tahun=" + Tahun + " ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Approval"]);
                }
            }

            return 0;
        }

        public int RetrieveTableBB(string Bulan, string Tahun)
        {
            string StrSql =
            " select count(ID) Nilai from BukuBesar_Mapping where rowstatus>-1 and bulan=" + Bulan + " and tahun=" + Tahun + " " +
            " and ItemSarMut='Efisiensi Budget Finishing' ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Nilai"]);
                }
            }

            return 0;
        }

        public ArrayList RetrieveMapping(string A)
        {
            arrData = new ArrayList();
            string strSQL =
            " select * from BukuBesar_Flaging where ItemSarmut='" + A + "' and RowStatus>-1";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);

            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new BudgetLogistik
                    {
                        ItemSarmut = sdr["ItemSarmut"].ToString(),
                        ParameterTerukur = sdr["ParameterTerukur"].ToString(),
                        Flag = sdr["Flag"].ToString()
                    });
                }
            }
            return arrData;
        }
    }
}