using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BusinessFacade;
using Domain;
using System.IO;
using DataAccessLayer;
using System.Collections.Generic;

namespace GRCweb1.Modul.Budgeting
{
    public partial class MaterialCCMonitoring : System.Web.UI.Page
    {
        private PakaiFacade p = new PakaiFacade();
        private CostCenterFacade cf = new CostCenterFacade();
        private ArrayList arrData = new ArrayList();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Global.LoadBulan(ddlBulan);
                LoadTahun();
                LoadCostCenter();

                int LastDay = DateTime.DaysInMonth(int.Parse(ddlTahun.SelectedValue.ToString()), int.Parse(ddlBulan.SelectedValue.ToString()));
                txtTglMulai.Text = LastDay.ToString().PadLeft(2, '0') + "-" + ddlBulan.SelectedValue.PadLeft(2, '0') + "-" + ddlTahun.SelectedValue.ToString();
            }
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }

        private void LoadCostCenter()
        {
            cf.GetCostCenter(ddlDept);
            ddlDept.SelectedValue = "1";
        }

        private void LoadTahun()
        {
            p.GetTahun(ddlTahun);
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            switch (int.Parse(ddlDept.SelectedValue))
            {
                case 3:
                    Response.Redirect("BudgetingLogistikNew.aspx?id=2");
                    break;

                case 2:
                    Response.Redirect("BudgetingFinishing.aspx");
                    break;

                default:
                    LoadMonitoring();
                    break;
            }
        }

        private void LoadMonitoring()
        {
            //btnPerview.Text = "Tes";
            cf.StartPeriode = ddlTahun.SelectedValue + ddlBulan.SelectedValue.PadLeft(2, '0') + "01";
            cf.EndPeriode = ddlTahun.SelectedValue + ddlBulan.SelectedValue.PadLeft(2, '0') +
                            DateTime.DaysInMonth(int.Parse(ddlTahun.SelectedValue), int.Parse(ddlBulan.SelectedValue)).ToString().PadLeft(2, '0');
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            int Bulan = 0;
            int Tahun = 0;
            zl.CustomQuery = "SELECT distinct top 1 costperiod Bulan,CostYear Tahun FROM  MaterialCCMatrix mx where MaterialCCID= " + int.Parse(ddlDept.SelectedValue.ToString()) +
                "and costperiod <= SUBSTRING('" + cf.StartPeriode + "',5,2) and CostYear <=SUBSTRING('" + cf.StartPeriode + "',1,4) order by  CostYear desc,costperiod desc";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    Bulan = Int32.Parse(sdr["Bulan"].ToString());
                    Tahun = Int32.Parse(sdr["Tahun"].ToString());
                }
            }
            arrData = new ArrayList();
            arrData = cf.Retrieve(int.Parse(ddlDept.SelectedValue.ToString()), Tahun, Bulan);
            lstMonitor.DataSource = arrData;
            lstMonitor.DataBind();
            #region update sarmut BM
            string sarmutPrs = "Effisiensi Pemantauan Budget";
            string strJmlLine = string.Empty;
            string strDept = ddlDept.SelectedItem.Text.Substring(0, 5).ToUpper();
            int deptid = getDeptID("BOARD");
            int actual = cf.GetActual(int.Parse(ddlDept.SelectedValue.ToString()), Tahun, Bulan);
            if (strDept == "BOARD")
            {
                switch (int.Parse(cf.PlanningProdLine()))
                {
                    case 2:
                        strJmlLine = "Budget 2 Line";
                        break;
                    case 3:
                        strJmlLine = "Budget 3 Line";
                        break;
                    case 4:
                        strJmlLine = "Budget 4 Line";
                        break;
                    case 5:
                        strJmlLine = "Budget 5 Line";
                        break;
                    case 6:
                        strJmlLine = "Budget 6 Line";
                        break;
                }
            }
            #region Boardmill
            if (ddlDept.SelectedItem.Text.ToUpper() == "BOARDMILL")
            {
                zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue + " set @bulan=" + ddlBulan.SelectedValue + " " +
                    "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +

                    "update SPD_Trans set actual=0 where Approval=0 and tahun=@tahun and bulan=@bulan and  SarmutDeptID in ( " +
                    "select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and SarmutPID in ( " +
                    "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "'))" +

                    "update SPD_Trans set actual=" + actual + " where Approval=0 and tahun=@tahun and bulan=@bulan and  SarmutDeptID in ( " +
                    "select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and sarmutdepartemen='" + strJmlLine + "' and SarmutPID in ( " +
                    "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "'))";
                sdr = zl.Retrieve();

                #region Yoga SarmutToPES
                ZetroView zs = new ZetroView();
                zs.QueryType = Operation.CUSTOM;
                decimal targetSarmutBM = 0;
                decimal actualSarmut = 0;
                zs.CustomQuery = "SELECT * FROM SPD_Trans WHERE SarmutDeptID IN (SELECT ID FROM SPD_Departemen WHERE SarmutPID=114) " +
                                 "AND Tahun=" + ddlTahun.SelectedValue + " AND Bulan=" + ddlBulan.SelectedValue + " ";
                SqlDataReader tdr = zs.Retrieve();
                if (tdr.HasRows)
                {
                    while (tdr.Read())
                    {
                        actualSarmut = (tdr["Actual"] == DBNull.Value) ? 0 : Convert.ToDecimal(tdr["Actual"]);

                        if (actualSarmut > 0)
                        {
                            zs.CustomQuery = "select a.TargetV1, b.SarmutDepartemen,b.SarmutPID,c.Bulan,c.Tahun,c.Actual from SPD_TargetV as a left join " +
                                             "SPD_Departemen as b on a.ID=b.TargetVID left join SPD_Trans as c on b.ID=c.SarmutDeptID where " +
                                             "a.RowStatus>-1 and b.RowStatus>-1 and c.RowStatus>-1 and c.Bulan=" + ddlBulan.SelectedValue + " " +
                                             "and Tahun=" + ddlTahun.SelectedValue + " and a.ID IN (SELECT TargetVID FROM SPD_Departemen WHERE SarmutPID=114) and c.Actual>0";
                            SqlDataReader adr = zs.Retrieve();
                            if (adr.HasRows)
                            {
                                while (adr.Read())
                                {
                                    //targetSarmutBM = Convert.ToDecimal(adr["TargetV1"].ToString());
                                    targetSarmutBM = (adr["TargetV1"] == DBNull.Value) ? 0 : Convert.ToDecimal(adr["TargetV1"]);
                                }
                            }
                            string strError = "";
                            string ketPes = (actual > targetSarmutBM) ? "Tidak Tercapai (> 100%)" : "Tercapai(100% Maks)";
                            if (actual > 0)
                            {
                                string valueActual = ((actual / targetSarmutBM) * 100).ToString();
                                double percentActual = Convert.ToDouble(valueActual); Session["Persen"] = percentActual;
                                ZetroView zv = new ZetroView();
                                zv.QueryType = Operation.CUSTOM;
                                int IdKPI = 0;
                                string kpiName = string.Empty;
                                zv.CustomQuery = "SELECT * FROM ISO_KPI WHERE DeptID=2 and month(TglMulai)=" + ddlBulan.SelectedValue + " AND year(TglMulai)=" + ddlTahun.SelectedValue + " ORDER BY ID desc";
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
                                        uc.Ket = string.Concat(actual.ToString(), " (" + Math.Round(percentActual) + "%)");
                                        //uc.Percent = valueActual.ToString();
                                        //uc.TglMulai = Convert.ToDateTime(txtTglMulai.Text);
                                        int pjgDept = ddlDept.SelectedItem.ToString().Length;
                                        uc.DeptName = (pjgDept >= 4) ? ddlDept.SelectedItem.ToString().Substring(0, 3) : ddlDept.SelectedItem.ToString().Substring(0, pjgDept);
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
                                                    ketActual = string.Concat(actual.ToString(), " (" + Math.Round(percentActual) + "%)");
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
                                            up.Ket = string.Concat(actual.ToString(), " (" + Math.Round(percentActual) + "%)");
                                            up.SopScoreID = IDScore;
                                            up.KetTargetKe = TargetKe;
                                            up.PointNilai = PointNilai;
                                            arrData.Add(up);
                                            strError = UpdateKPI(up);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                #endregion
            }
            #endregion
            #endregion
            #region update sarmut Maintenance
            sarmutPrs = "Effisiensi pemakaian spare part";
            strJmlLine = string.Empty;
            strDept = ddlDept.SelectedItem.Text.Substring(0, 5).ToUpper();
            actual = cf.GetActual(int.Parse(ddlDept.SelectedValue.ToString()), Tahun, Bulan);
            deptid = getDeptID("MAINT");
            if (strDept == "MAINT")
            {
                switch (int.Parse(cf.PlanningProdLine()))
                {
                    case 2:
                        strJmlLine = "2 Line";
                        break;
                    case 3:
                        strJmlLine = "3 Line";
                        break;
                    case 4:
                        strJmlLine = "4 Line";
                        break;
                    case 5:
                        strJmlLine = "5 Line";
                        break;
                    case 6:
                        strJmlLine = "6 Line";
                        break;
                }
            }
            #endregion

            if (ddlDept.SelectedItem.Text.ToUpper() == "MAINTENANCE")
            {
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery = "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue + " set @bulan=" + ddlBulan.SelectedValue + " " +
                    "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +

                    "update SPD_Trans set actual=0 where Approval=0 and tahun=@tahun and bulan=@bulan and  SarmutDeptID in ( " +
                    "select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and SarmutPID in ( " +
                    "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "'))" +

                    "update SPD_Trans set actual=" + actual + " where Approval=0 and tahun=@tahun and bulan=@bulan and  SarmutDeptID in ( " +
                    "select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and sarmutdepartemen='" + strJmlLine + "' and SarmutPID in ( " +
                    "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "'))";

                SqlDataReader sdr1 = zl1.Retrieve();
            }

            #region Update Nilai ke DataBase sebelum Update ke Buku Besar ISO, Update ke Buku Besar ketika Apv Manager di Apv SarMut
            Users users = (Users)Session["Users"];
            decimal TotalBudgetPerBulan = 0; decimal ActualSPBPerBulan = 0; string Persen = string.Empty;
            string thn = string.Empty; string bln = string.Empty; string NBulan = string.Empty;
            string Value = string.Empty; string Item = string.Empty;

            if (users.DeptID == 2 || users.DeptID == 25 || users.DeptID == 19)
            {
                bln = ddlBulan.SelectedValue;
                thn = ddlTahun.SelectedValue;
                if (bln == "1") { NBulan = "Jan"; }
                else if (bln == "2") { NBulan = "Feb"; }
                else if (bln == "3") { NBulan = "Mar"; }
                else if (bln == "4") { NBulan = "Apr"; }
                else if (bln == "5") { NBulan = "Mei"; }
                else if (bln == "6") { NBulan = "Jun"; }
                else if (bln == "7") { NBulan = "Jul"; }
                else if (bln == "8") { NBulan = "Agu"; }
                else if (bln == "9") { NBulan = "Sep"; }
                else if (bln == "10") { NBulan = "Okt"; }
                else if (bln == "11") { NBulan = "Nov"; }
                else if (bln == "12") { NBulan = "Des"; }

                if (users.DeptID == 2)
                {
                    Item = "Effisiensi Pemantauan Budget";
                }
                else if (users.DeptID == 4 || users.DeptID == 19 || users.DeptID == 5 || users.DeptID == 18)
                {
                    Item = "Effisiensi pemakaian spare part";
                }

                TotalBudgetPerBulan = Convert.ToDecimal(Session["TotalBudget"]);
                ActualSPBPerBulan = Convert.ToDecimal(Session["TotalPakai"]);
                Persen = Session["Persen"].ToString();

                BudgetCC CC = new BudgetCC();
                FacadeBudgetCC FCC = new FacadeBudgetCC();
                int Nilai1 = FCC.RetrieveTableBB(bln, thn, Item);
                if (Nilai1 == 0)
                {
                    ArrayList arrBB = new ArrayList();
                    FacadeBudgetCC F = new FacadeBudgetCC();
                    arrBB = F.RetrieveMapping(Item);
                    if (arrBB.Count > 0)
                    {
                        int i = 0;
                        foreach (BudgetCC List in arrBB)
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
                                Value = Persen.Replace(",", ".");
                            }

                            ZetroView z0 = new ZetroView();
                            z0.QueryType = Operation.CUSTOM;
                            z0.CustomQuery =

                            "insert into BukuBesar_Mapping (Bulan,Tahun,Nilai,ParameterTerukur,InitialBulan,CreatedBy,CreatedTime,RowStatus,ItemSarmut) " +
                            "values " +
                            "('" + ddlBulan.SelectedValue + "','" + ddlTahun.SelectedValue + "','" + Value + "','" + List.ParameterTerukur + "','" + NBulan +
                                 "','" + users.UserName + "'," + "GetDate()" + "," + "0" + ",'" + List.ItemSarmut + "')";

                            SqlDataReader sd0 = z0.Retrieve();
                        }
                        i = i + 1;
                    }

                }
                else
                {
                    BudgetCC CC1 = new BudgetCC();
                    FacadeBudgetCC FCC1 = new FacadeBudgetCC();
                    int Nilai = FCC1.RetrieveApv(bln, thn);
                    string query = string.Empty;
                    if (users.DeptID == 2)
                    {
                        query = "BB_NilaiCCBM";
                    }
                    else if (users.DeptID == 4 || users.DeptID == 19 || users.DeptID == 5 || users.DeptID == 18)
                    {
                        query = "BB_NilaiCCMTC";
                    }

                    if (Nilai < 2)
                    {
                        ZetroView z1 = new ZetroView();
                        z1.QueryType = Operation.CUSTOM;
                        z1.CustomQuery =

                        "declare @tahun int,@bulan int " +
                        "set @tahun=" + ddlTahun.SelectedValue + " set @bulan=" + ddlBulan.SelectedValue + " " +

                        "update " + query + " set TotalBudget=" + TotalBudgetPerBulan + "," + ActualSPBPerBulan + " where RowStatus>-1 " +
                        "and Bulan=@bulan and Tahun=@tahun ";

                        SqlDataReader sd1 = z1.Retrieve();

                    }
                }

            }

            #endregion

        }

        protected void ddlBulan_Change(object sender, EventArgs e)
        {
            if (ddlBulan.SelectedValue == "0")
            {
                DisplayAJAXMessage(this, "Silahkan pilih bulan terlebih dahulu !! "); return;
            }

            int LastDay = DateTime.DaysInMonth(int.Parse(ddlTahun.SelectedValue.ToString()), int.Parse(ddlBulan.SelectedValue.ToString()));
            txtTglMulai.Text = LastDay.ToString().PadLeft(2, '0') + "-" + ddlBulan.SelectedValue.PadLeft(2, '0') + "-" + ddlTahun.SelectedValue.ToString();
        }

        private string SimpanKPI(SarmutPes sop)
        {
            string strEvent = "Insert";
            ISO_DocumentNoFacade docNoFacade = new ISO_DocumentNoFacade();
            ISO_DocumentNo docNo = docNoFacade.RetrieveByDept(3, Convert.ToInt32(ddlDept.SelectedValue), Convert.ToDateTime(txtTglMulai.Text).Year);
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
            ISO_DocumentNo docNo = docNoFacade.RetrieveByDept(3, Convert.ToInt32(ddlDept.SelectedValue), Convert.ToDateTime(txtTglMulai.Text).Year);
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
            strError = sarpesProcessFacade.UpdateKpi();
            if (strError == string.Empty)
            {
                //InsertLog(strEvent);
                //txtTaskNo.Text = "Doc No. : " + sarpesProcessFacade.sopNonya;
            }
            return strError;
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
        protected void lstMonitor_DataBound(object sender, RepeaterItemEventArgs e)
        {
            HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("xx");
            if (tr.Attributes["title"].ToString() == "TOTAL ")
            {
                tr.Attributes.Add("class", "Line3 baris angka");
                tr.Cells[0].InnerHtml = "";
                tr.Cells[2].InnerHtml = "";

                Session["TotalBudget"] = tr.Cells[3].InnerHtml;
                Session["TotalPakai"] = tr.Cells[4].InnerHtml;
                Label Persen = (Label)tr.FindControl("prs");
                Session["Persen"] = Persen.Text.ToString();
            }
            lstDtl.Visible = false;
        }
        protected void lstMonitor_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            int idx = e.Item.ItemIndex;
            LinkButton lnk = (LinkButton)e.Item.FindControl("grp");

            if (e.CommandName == "pilih")
            {
                foreach (RepeaterItem rr in lstMonitor.Items)
                {
                    HtmlTableRow tr = (HtmlTableRow)rr.FindControl("xx");

                    if (rr.ItemIndex == lstMonitor.Items.Count - 1)
                    {
                        // HtmlTableRow trs = (HtmlTableRow)e.Item.FindControl("xx");
                        tr.Attributes["style"] = "background-color:#F5DEB3;";
                        //trs.Cells[0].Attributes["style"] = "background-color:#F5DEB3;";
                    }
                    else
                    {
                        tr.Attributes["style"] = "background-color:#f0ffff;";
                        //tr.Cells[6].Attributes["style"] = "border:0px solid";
                    }
                }
                RepeaterItem rpt = (RepeaterItem)((LinkButton)e.CommandSource).NamingContainer;
                HtmlTableRow currentrow = (HtmlTableRow)rpt.FindControl("xx");
                currentrow.Attributes["style"] = "background-color: #B0C4DE;";
                //currentrow.Cells[6].Attributes["style"] = "border-top:1px solid #838b8b; border-bottom:1px solid #838b8b";
                //txtlbl.Text = lnk.Text;
                LoadDetailGroup(ddlDept.SelectedValue, int.Parse(e.CommandArgument.ToString()), lnk.Text);
            }
        }
        private void LoadDetailGroup(string MatGroupID, int MatCCID, string Judul)
        {
            arrData = new ArrayList();
            string header = "";
            cf.StartPeriode = ddlTahun.SelectedValue + ddlBulan.SelectedValue.PadLeft(2, '0') + "01";
            cf.EndPeriode = ddlTahun.SelectedValue + ddlBulan.SelectedValue.PadLeft(2, '0') +
                            DateTime.DaysInMonth(int.Parse(ddlTahun.SelectedValue), int.Parse(ddlBulan.SelectedValue)).ToString().PadLeft(2, '0');
            cf.MatCCGroupID = MatCCID;
            arrData = cf.Retrieve(int.Parse(MatGroupID), true);
            switch (int.Parse(MatGroupID))
            {
                case 1:
                    header = "<tr class=\"total\"><td colspan=\"6\" style=\"padding-left:5px\"><b>" + Judul + "</b></td></tr>";
                    header += "<tr class=\"tbHeader\">";
                    header += "<th style=\"width:5%\" rowspan=\"2\" class=\"kotak\">No.</th>";
                    header += "<th style=\"width:10%\" rowspan=\"2\" class=\"kotak\">ItemCode</th>";
                    header += "<th style=\"width:25%\" rowspan=\"2\" class=\"kotak\">ItemName</th>";
                    header += "<th style=\"width:8%\" class=\"kotak\" colspan=\"2\">Quantity</th>";
                    header += "<th style=\"width:8%\" rowspan=\"2\" class=\"kotak\">Total Price</th>";
                    header += "</tr>";
                    header += "<tr class=\"tbHeader\">";
                    header += "<th style=\"width:8%\" class=\"kotak\">Budget</th>";
                    header += "<th style=\"width:8%\" class=\"kotak\">Actual</th>";
                    header += "</tr>";
                    lstDetail.DataSource = arrData;
                    lstDetail.DataBind();
                    lstMtc.DataSource = new ArrayList();
                    lstMtc.DataBind();
                    break;
                case 4:
                    header = "<tr class=\"total\"><td colspan=\"4\" style=\"padding-left:5px\"><b>" + Judul + "</b></td></tr>";
                    header += "<tr class=\"tbHeader\">";
                    header += "<th style=\"width:5%\" class=\"kotak\">No.</th>";
                    header += "<th style=\"width:25%\"class=\"kotak\">Group Name</th>";
                    header += "<th style=\"width:8%\" class=\"kotak\">Cost Actual</th>";
                    header += "<th style=\"width:8%\" class=\"kotak\">%</th>";
                    header += "</tr>";
                    lstDetail.DataSource = new ArrayList();
                    lstDetail.DataBind();
                    lstMtc.DataSource = arrData;
                    lstMtc.DataBind();
                    break;
            }
            txtHeader.Text = header;
            lstDtl.Visible = true;
        }
        protected void lstDetailDataBound(object sender, RepeaterItemEventArgs e)
        {
            HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("xDetail");
            if (ddlDept.SelectedValue == "4")
            {
                decimal d = decimal.Parse(tr.Cells[3].InnerText);
                tr.Cells[3].InnerHtml = (d * 100).ToString("###,###.##");
            }
            if (tr.Attributes["title"].ToString() == "TOTAL " || tr.Attributes["title"].ToString() == string.Empty)
            {
                //LinkButton jdl = (LinkButton)e.Item.FindControl("txtItem");
                tr.Attributes.Add("class", "Line3 baris angka");
                tr.Cells[0].InnerHtml = "";
                tr.Cells[1].Attributes.Add("class", "kotak angka");
                if ((ddlDept.SelectedValue != "1"))
                {
                    tr.Cells[3].InnerHtml = "";
                }
                //jdl.Text = "TOTAL";

            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            LoadMonitoring();
            arrData = new ArrayList();
            string header = "";
            cf.StartPeriode = ddlTahun.SelectedValue + ddlBulan.SelectedValue.PadLeft(2, '0') + "01";
            cf.EndPeriode = ddlTahun.SelectedValue + ddlBulan.SelectedValue.PadLeft(2, '0') +
            DateTime.DaysInMonth(int.Parse(ddlTahun.SelectedValue), int.Parse(ddlBulan.SelectedValue)).ToString().PadLeft(2, '0');
            //cf.MatCCGroupID = MatCCID;
            arrData = cf.RetrieveBM(1, true);
            header = "<tr class=\"total\"><td colspan=\"6\" style=\"padding-left:5px\"><b>" + "List Aktual Budget BM Periode " + ddlBulan.SelectedItem.Text +
                " " + ddlTahun.SelectedItem.Text + "</b></td></tr>";
            header += "<tr class=\"tbHeader\">";
            header += "<th style=\"width:5%\" rowspan=\"2\" class=\"kotak\">No.</th>";
            header += "<th style=\"width:10%\" rowspan=\"2\" class=\"kotak\">ItemCode</th>";
            header += "<th style=\"width:25%\" rowspan=\"2\" class=\"kotak\">ItemName</th>";
            header += "<th style=\"width:8%\" class=\"kotak\" colspan=\"2\">Quantity</th>";
            header += "<th style=\"width:8%\" rowspan=\"2\" class=\"kotak\">Total Price</th>";
            header += "</tr>";
            header += "<tr class=\"tbHeader\">";
            header += "<th style=\"width:8%\" class=\"kotak\">Budget</th>";
            header += "<th style=\"width:8%\" class=\"kotak\">Actual</th>";
            header += "</tr>";
            lstDetail.DataSource = arrData;
            lstDetail.DataBind();
            lstMtc.DataSource = new ArrayList();
            lstMtc.DataBind();
            txtHeader.Text = header;
            lstDtl.Visible = true;
            lstRekap.Visible = false;
        }
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDept.SelectedValue == "1")
            {
                Button1.Visible = true;
                btnExport.Visible = true;
            }
            else
            {
                Button1.Visible = false;
                btnExport.Visible = false;
            }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=RekapPes.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            //string Html = "<b>REKAP PES</b>";
            //Html += "<br>Periode : " + ddlBulan.SelectedItem.Text + "  " + ddlTahun.SelectedValue.ToString();
            //Html += "<br>Departement :" + ddlDept.SelectedItem.Text;
            //string HtmlEnd = "";
            //lstForPrint.RenderControl(hw);
            lstDtl.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0", "border=\"1");
            Response.Write(Contents);
            Response.Flush();
            Response.End();
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
    }

    public class BudgetCC
    {
        public string ItemSarmut { get; set; }
        public string ParameterTerukur { get; set; }
        public string Persen { get; set; }
        public string Flag { get; set; }
        public int Rowstatus { get; set; }
    }

    public class FacadeBudgetCC
    {
        public string strError = string.Empty;
        private ArrayList arrData = new ArrayList();
        private BudgetCC objBudget = new BudgetCC();
        private List<SqlParameter> sqlListParam;

        public string Criteria { get; set; }
        public string Field { get; set; }
        public string Where { get; set; }

        public int RetrieveApv(string Bulan, string Tahun)
        {
            string StrSql =
            " select Approval from SPD_TransPrs where SarmutPID in (select ID from SPD_Perusahaan where DeptID=1 and  " +
            " SarMutPerusahaan='Effisiensi Pemantauan Budget' and RowStatus>-1) and RowStatus>-1 and bulan=" + Bulan + " and tahun=" + Tahun + " ";

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

        public int RetrieveTableBB(string Bulan, string Tahun, string Item)
        {
            string StrSql =
            " select count(ID) Nilai from BukuBesar_Mapping where rowstatus>-1 and bulan=" + Bulan + " and tahun=" + Tahun + " " +
            " and ItemSarMut='" + Item + "' ";

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
                    arrData.Add(new BudgetCC
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