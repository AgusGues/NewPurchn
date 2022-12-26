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
using System.IO;
using System.Xml.Linq;
using BusinessFacade;
using Domain;
using DataAccessLayer;
namespace GRCweb1.Modul.Budgeting
{
    public partial class BudgetingFinishing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadBulan();
                LoadTahun();

                int LastDay = DateTime.DaysInMonth(int.Parse(ddlTahun.SelectedValue.ToString()), int.Parse(ddlBulan.SelectedValue.ToString()));
                txtTglMulai.Text = LastDay.ToString().PadLeft(2, '0') + "-" + ddlBulan.SelectedValue.PadLeft(2, '0') + "-" + ddlTahun.SelectedValue.ToString();
            }
                ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }
        private void LoadBulan()
        {
            ddlBulan.Items.Clear();
            //ddlBulan.Items.Add(new ListItem("--Pilih--", "0"));
            for (int i = 1; i < 13; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i).ToString(), i.ToString()));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
        }
        private void LoadTahun()
        {
            PakaiFacade pd = new PakaiFacade();
            pd.GetTahun(ddlTahun);
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }
        private int nomor = 0;
        private decimal tHargaSatuan = 0;
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            CostCenterFacade cfd = new CostCenterFacade();
            ArrayList arrData = new ArrayList();
            cfd.Bulan = ddlBulan.SelectedValue.ToString();
            cfd.Tahun = ddlTahun.SelectedValue.ToString();
            arrData = cfd.RetrieveBudgetFinishing(true);
            nomor = 0;
            lstFin.DataSource = arrData;
            lstFin.DataBind();

            decimal total = 0; decimal gtotal = 0; decimal grandTotal = 0;
            decimal totalA = 0; decimal gtotalA = 0; decimal grandTotalA = 0;
            decimal TotalP = 0; decimal gtotalP = 0; decimal grandTotalP = 0;
            decimal AvgHarga = 0;
            string itemname = string.Empty;
            for (int i = 0; i < lstFin.Items.Count; i++)
            {
                TextBox tHarga = (TextBox)lstFin.Items[i].FindControl("txtPrice");

                HiddenField tItemName = (HiddenField)lstFin.Items[i].FindControl("txtBarang");
                HtmlTableRow tr = (HtmlTableRow)lstFin.Items[i].FindControl("tr1");
                HtmlTableRow trTotal = ((i + 1) < lstFin.Items.Count) ? (HtmlTableRow)lstFin.Items[i + 1].FindControl("tr1") : new HtmlTableRow();
                HiddenField rws = (HiddenField)lstFin.Items[i].FindControl("txtStatus");
                HiddenField ccID = (HiddenField)lstFin.Items[i].FindControl("txtMCCID");
                txtPrice_Change(tHarga, null);
                decimal lain2 = 0;

                if (tItemName.Value.Trim().Length > 13)
                {
                    if (tItemName.Value.Substring(10, 4).ToUpper().Trim() == "LAIN")
                    {
                        lain2 = cfd.GetBudgetlainfin();
                    }
                }
                if (int.Parse(rws.Value) < 2 && int.Parse(ccID.Value) != 1000)
                {
                    decimal.TryParse(tr.Cells[6].InnerText, out total);
                    decimal.TryParse(tr.Cells[9].InnerText, out TotalP);
                    decimal.TryParse(tr.Cells[10].InnerText, out totalA);
                    gtotal += (int.Parse(rws.Value) == 1) ? total : 0;
                    grandTotal += (int.Parse(rws.Value) == 1) ? total : 0;
                    gtotalA += (int.Parse(rws.Value) == 1) ? totalA : 0;
                    grandTotalA += (int.Parse(rws.Value) == 1) ? totalA : 0;
                    gtotalP += (int.Parse(rws.Value) == 1) ? TotalP : 0;
                    grandTotalP += (int.Parse(rws.Value) == 1) ? TotalP : 0;
                    //itemname = tr.Cells[1].InnerText.ToString();
                }

                /**
                 * Kolom Budget Total Rupiah per bulan
                 */

                itemname = tItemName.Value;

                tr.Cells[6].InnerHtml = (int.Parse(rws.Value) == 2 && int.Parse(ccID.Value) != 1000) ? gtotal.ToString("N0") : tr.Cells[6].InnerText;
                //tr.Cells[6].InnerHtml = (int.Parse(rws.Value) == 2 && int.Parse(ccID.Value) == 1000) ? lain2.ToString("N0") : tr.Cells[6].InnerText;
                if (tItemName.Value.Trim().Length > 13)
                {
                    if (int.Parse(rws.Value) == 2 && tItemName.Value.Substring(10, 4).ToUpper().Trim() == "LAIN")
                        tr.Cells[6].InnerHtml = lain2.ToString("N0");
                    else
                        tr.Cells[6].InnerHtml = tr.Cells[6].InnerText;
                }
                tr.Cells[11].InnerHtml = (int.Parse(rws.Value) == 2 && int.Parse(ccID.Value) != 1000 && gtotal > 0) ? ((gtotalA / gtotal) * 100).ToString("N0") : "";
                tr.Cells[12].InnerHtml = (int.Parse(rws.Value) == 2 && int.Parse(ccID.Value) != 1000 && gtotalP > 0) ? (gtotalA / gtotalP).ToString("N0") : "";
                /**
                 * Proses hitung grand total Rupiah per bulab
                 */
                gtotal = (int.Parse(rws.Value) == 2 && int.Parse(ccID.Value) != 1000) ? 0 : gtotal;
                gtotalP = (int.Parse(rws.Value) == 2 && int.Parse(ccID.Value) != 1000) ? 0 : gtotalP;
                decimal ggTotal = 0;
                decimal.TryParse(tr.Cells[6].InnerText, out ggTotal);
                //grandTotal += (int.Parse(rws.Value) == 2 && int.Parse(ccID.Value) == 1000) ? ggTotal : 0;
                if (tItemName.Value.Trim().Length > 13)
                {
                    if (int.Parse(rws.Value) == 2 && tItemName.Value.Substring(10, 4).ToUpper().Trim() == "LAIN")
                        grandTotal = grandTotal + decimal.Parse(lain2.ToString("N0"));
                    else
                        grandTotal += 0;
                }
                /**
                 * Kolom Aktual Total Rupiah per bulan
                 */
                tr.Cells[10].InnerHtml = (int.Parse(rws.Value) == 2 && int.Parse(ccID.Value) != 1000) ? gtotalA.ToString("N0") : tr.Cells[10].InnerText;

                gtotalA = (int.Parse(rws.Value) == 2 && int.Parse(ccID.Value) != 1000) ? 0 : gtotalA;
                grandTotalA += (int.Parse(rws.Value) == 2 && int.Parse(ccID.Value) == 1000) ? decimal.Parse(tr.Cells[10].InnerText) : 0;

            }
            rpBudget.InnerHtml = grandTotal.ToString("N0");//kolom grand total budget
            rpAktual.InnerHtml = grandTotalA.ToString("N0");//kolom grand total aktual
            string persen = string.Empty;
            if (grandTotal > 0)
            {
                prosene.InnerHtml = (grandTotalA / grandTotal).ToString("P2");
                prosene.Attributes["title"] = "( " + grandTotalA.ToString("N0") + " / " + grandTotal.ToString("N0") + " )/100 = " +
                    (grandTotalA / grandTotal).ToString("P2");
                persen = ((grandTotalA / grandTotal) * 100).ToString("0.##");

                Session["Actual"] = grandTotalA; Session["Persen"] = persen;
            }
            else
            {
                prosene.InnerHtml = (0).ToString("P2");
                prosene.Attributes["title"] = "(0)";
            }

            #region update sarmut Fiishing
            int deptid = getDeptID("FINISHING");
            string actual = persen.Replace(",", ".");
            string sarmutPrs = "Efisiensi Budget Finishing";
            ZetroView zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery = "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue + " set @bulan=" + ddlBulan.SelectedValue + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                "update SPD_TransPrs set actual=" + actual + " where Approval=0 and tahun=@tahun and bulan=@bulan and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "')";
            SqlDataReader sdr1 = zl1.Retrieve();
            #endregion

            #region Yoga SarmutToPES

            ZetroView zs = new ZetroView();
            zs.QueryType = Operation.CUSTOM;
            decimal targetSarmutFinishing = 0;
            int bulan = Convert.ToInt32(ddlBulan.SelectedValue) - 1;
            zs.CustomQuery = "SELECT top 1 * FROM SPD_TransPrs WHERE SarmutPID IN (SELECT ID from SPD_Perusahaan where Rowstatus>-1 and " +
                             "SarMutPerusahaan='" + sarmutPrs + "') and Approval>1 order by id desc ";
            SqlDataReader adr = zs.Retrieve();
            if (adr.HasRows)
            {
                while (adr.Read())
                {
                    targetSarmutFinishing = (adr["Target"] == DBNull.Value) ? 0 : Convert.ToDecimal(adr["Target"]);
                }
            }
            if (Convert.ToInt32(grandTotalA) > 0)
            {
                string strError = ""; string valueActual = string.Empty;

                decimal newActual = (grandTotalA / grandTotal) * 100;
                string ketPes = (newActual > targetSarmutFinishing) ? "Tidak Tercapai (> 100%)" : "Tercapai(100% Maks)";
                //if (targetSarmutFinishing == 0)
                //{
                //    valueActual = "0";
                //}
                //else
                //{
                //    valueActual = ((newActual / targetSarmutFinishing) * 100).ToString();
                //}
                valueActual = (targetSarmutFinishing == 0) ? "0" : ((newActual / targetSarmutFinishing) * 100).ToString();

                double percentActual = Convert.ToDouble(valueActual);
                ZetroView zv = new ZetroView();
                zv.QueryType = Operation.CUSTOM;
                int IdKPI = 0;
                string kpiName = string.Empty;
                zv.CustomQuery = "SELECT * FROM ISO_KPI WHERE DeptID=3 and month(TglMulai)=" + ddlBulan.SelectedValue + " AND year(TglMulai)=" + ddlTahun.SelectedValue + " ORDER BY ID desc";
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
                        uc.Ket = string.Concat(newActual.ToString(), " (" + Math.Round(percentActual) + "%)");
                        //uc.Percent = valueActual.ToString();
                        //uc.TglMulai = Convert.ToDateTime(txtTglMulai.Text);
                        int pjgDept = ((Users)Session["Users"]).DeptID;
                        string ddlDept = "FINISHING";
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
                                    ketActual = string.Concat(grandTotalA, " (" + Math.Round(percentActual) + "%)");
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
                            up.Ket = string.Concat(grandTotalA, " (" + Math.Round(percentActual) + "%)");
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

            /** Start 01 Februari 2021, Created by: Beny **/
            /** Update ke Buku Besar ISO **/
            #region Update Nilai ke DataBase sebelum Update ke Buku Besar ISO, Update ke Buku Besar ketika Apv Manager di Apv SarMut

            Users users = (Users)Session["Users"];

            decimal TotalBudgetPerBulan = 0;
            decimal ActualSPBPerBulan = 0;
            string Tahun = string.Empty;
            string NilaiBulan = string.Empty;
            string NamaBulan = string.Empty;
            string Value = string.Empty;

            /** Jika Dept. Finishing maka  **/
            if (users.DeptID == 3)
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

                TotalBudgetPerBulan = Convert.ToDecimal(grandTotal.ToString("N0"));
                ActualSPBPerBulan = Convert.ToDecimal(grandTotalA.ToString("N0"));

                BudgetFinishing fin0 = new BudgetFinishing();
                FacadeBudgetFinishing fin1 = new FacadeBudgetFinishing();
                int Nilai1 = fin1.RetrieveTableBB(NilaiBulan, Tahun);
                if (Nilai1 == 0)
                {
                    ArrayList arrBB = new ArrayList();
                    FacadeBudgetFinishing F = new FacadeBudgetFinishing();
                    arrBB = F.RetrieveMapping("Efisiensi Budget Finishing");
                    if (arrBB.Count > 0)
                    {
                        int i = 0;
                        foreach (BudgetFinishing List in arrBB)
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
                                Value = persen.Replace(",", ".");
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
                else
                {
                    BudgetFinishing fin = new BudgetFinishing();
                    FacadeBudgetFinishing fin2 = new FacadeBudgetFinishing();
                    int Nilai = fin2.RetrieveApv(NilaiBulan, Tahun);

                    if (Nilai < 2)
                    {
                        ZetroView z1 = new ZetroView();
                        z1.QueryType = Operation.CUSTOM;
                        z1.CustomQuery =

                        "declare @tahun int,@bulan int " +
                        "set @tahun=" + ddlTahun.SelectedValue + " set @bulan=" + ddlBulan.SelectedValue + " " +

                        "update BB_NilaiCCFinishing set TotalBudget=" + TotalBudgetPerBulan + "," + ActualSPBPerBulan + " where RowStatus>-1 " +
                        "and Bulan=@bulan and Tahun=@tahun ";

                        SqlDataReader sd1 = z1.Retrieve();
                    }
                }
            }
            #endregion
            #region Link Report ke PES , Dibuat : 18 Juli 2021 oleh Beny
            Users user = (Users)Session["Users"];

            string Periode = (ddlBulan.SelectedValue.ToString().Length == 1) ? ddlTahun.SelectedValue.ToString().Trim() + "0" + ddlBulan.SelectedValue.ToString().Trim() : ddlTahun.SelectedValue.ToString().Trim() + ddlBulan.SelectedValue.ToString().Trim();
            int Approval = 0; int ID_NilaiPES = 0; decimal Actual = 0; decimal Persen = 0;

            ArrayList arrLink = new ArrayList();
            SarmutPESFacade arrLinkFacade = new SarmutPESFacade();
            arrLink = arrLinkFacade.RetrieveUserPES(sarmutPrs);
            foreach (SarmutPes link in arrLink)
            {
                /** Cek apakah PES sdh ada nilai nya di table PES_Nilai **/
                ZetroView zetro1 = new ZetroView();
                zetro1.QueryType = Operation.CUSTOM;
                zetro1.CustomQuery =
                " select ID from PES_Nilai where ItemSarmut='" + sarmutPrs + "' and  Periode='" + Periode + "' and RowStatus>-1 ";
                SqlDataReader sdrZetro1 = zetro1.Retrieve();
                if (sdrZetro1.HasRows)
                {
                    while (sdrZetro1.Read())
                    {
                        ID_NilaiPES = Convert.ToInt32(sdrZetro1["ID"]);
                    }
                }

                /** Cek Status Aproval KPI **/
                ZetroView zetro0 = new ZetroView();
                zetro0.QueryType = Operation.CUSTOM;
                zetro0.CustomQuery =
                " select Approval from ISO_KPIDetail " +
                " where KPIID in (select ID from ISO_KPI " +
                " where CategoryID in (select ID from ISO_UserCategory where Sarmut='" + sarmutPrs + "' and RowStatus>-1) " +
                " and left(convert(char,tglmulai,112),6)='" + Periode + "' and DeptID=3 and ISO_UserID='" + link.UserID + "' and RowStatus>-1 ) ";
                SqlDataReader sdrZetro0 = zetro0.Retrieve();
                /** Jika sdh ada inputan KPI **/
                if (sdrZetro0.HasRows)
                {
                    while (sdrZetro0.Read())
                    {
                        Approval = Convert.ToInt32(sdrZetro0["Approval"]);
                    }

                    /** Jika sdh ada inputan KPI dan approval=0 dan belum ada data di table PES_Nilai maka langsung Insert **/
                    if (Approval == 0 && ID_NilaiPES == 0)
                    {
                        #region Insert ke table PES_Nilai

                        ZetroView zSave = new ZetroView();
                        zSave.QueryType = Operation.CUSTOM;
                        zSave.CustomQuery =

                        " insert into PES_Nilai (ItemSarmut,DeptID,UserID_ISO,Periode,Actual_Value,RowStatus,CreatedTime,CreatedBy,Persen) " +
                        " values " +
                        " ('" + sarmutPrs + "','3','0','" + Periode + "','" + Convert.ToDecimal(Session["Actual"]) + "','0',GETDATE(),'" + user.UserName + "','" + Math.Round(Convert.ToDecimal(Session["Persen"])) + "')";

                        SqlDataReader sdSave = zSave.Retrieve();

                        #endregion
                    }
                    /** Jika sdh ada inputan KPI dan approval=0 dan sdh ada data di table PES_Nilai maka langsung Update **/
                    else if (Approval == 0 && ID_NilaiPES != 0)
                    {
                        #region Update ke table PES_Nilai
                        ZetroView zUpdate = new ZetroView();
                        zUpdate.QueryType = Operation.CUSTOM;

                        zUpdate.CustomQuery =
                        " update PES_Nilai set Actual_Value='" + Convert.ToDecimal(Session["Actual"]) + "',Persen='" + Math.Round(Convert.ToDecimal(Session["Persen"])) + "' where Periode='" + Periode + "' and RowStatus>-1 ";
                        SqlDataReader sdUpdate = zUpdate.Retrieve();
                        #endregion
                    }
                }
                else
                {
                    /** Jika belum ada inputan KPI dan belum ada data di table PES_Nilai maka langsung Insert **/
                    if (ID_NilaiPES == 0)
                    {
                        #region Insert ke table PES_Nilai
                        Actual = Convert.ToDecimal(Session["Actual"]);
                        Persen = Math.Round(Convert.ToDecimal(Session["Persen"]));
                        string Users = user.UserName.ToString().Trim();
                        ZetroView zSave = new ZetroView();
                        zSave.QueryType = Operation.CUSTOM;

                        zSave.CustomQuery =

                        " insert into PES_Nilai (ItemSarmut,DeptID,UserID_ISO,Periode,Actual_Value,RowStatus,CreatedTime,CreatedBy,Persen) " +
                        " values " +
                        " ('" + sarmutPrs + "','3','0','" + Periode + "','" + Convert.ToDecimal(Session["Actual"]) + "','0',GETDATE(),'" + user.UserName + "','" + Math.Round(Convert.ToDecimal(Session["Persen"])) + "')";

                        SqlDataReader sdSave = zSave.Retrieve();

                        #endregion
                    }
                    /** Jika belum ada inputan KPI dan belum ada data di table PES_Nilai maka langsung Update **/
                    else
                    {
                        #region Update ke table PES_Nilai
                        ZetroView zUpdate = new ZetroView();
                        zUpdate.QueryType = Operation.CUSTOM;

                        zUpdate.CustomQuery =
                          " update PES_Nilai set Actual_Value='" + Convert.ToDecimal(Session["Actual"]) + "',Persen='" + Math.Round(Convert.ToDecimal(Session["Persen"])) + "' where Periode='" + Periode + "' and RowStatus>-1 ";
                        SqlDataReader sdUpdate = zUpdate.Retrieve();
                        #endregion
                    }

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
                docNo.Tahun = int.Parse(ddlTahun.SelectedItem.Text );
                Company cp = new CompanyFacade().RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                docNo.Plant = cp.KodeLokasi;
            }
            else
            {
                docNo.DocNo = 1;
                //docNo.PesType = int.Parse(txtPES.Text); //sop
                docNo.DeptID = 2;
                docNo.Tahun = int.Parse(ddlTahun.SelectedItem.Text);
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
                docNo.DeptID = 2;
                docNo.Tahun = int.Parse(ddlTahun.SelectedItem.Text);
                Company cp = new CompanyFacade().RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                docNo.Plant = cp.KodeLokasi;
            }
            else
            {
                docNo.DocNo = 1;
                //docNo.PesType = int.Parse(txtPES.Text); //sop
                docNo.DeptID = 2;
                docNo.Tahun = int.Parse(ddlTahun.SelectedItem.Text);
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
        protected void btnExport_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstFin.Items.Count; i++)
            {
                TextBox tPrice = (TextBox)lstFin.Items[i].FindControl("txtPrice");
                HtmlTableRow tr = (HtmlTableRow)lstFin.Items[i].FindControl("tr1");
                HiddenField hItemID = (HiddenField)lstFin.Items[i].FindControl("txtItemID");
                HiddenField hStatus = (HiddenField)lstFin.Items[i].FindControl("txtStatus");
                HiddenField hMatCCID = (HiddenField)lstFin.Items[i].FindControl("txtMCCID");
                HiddenField hIdSimpan = (HiddenField)lstFin.Items[i].FindControl("txtSimpanID");
                HiddenField tItemName = (HiddenField)lstFin.Items[i].FindControl("txtBarang");
                tItemName.Visible = false;
                hItemID.Visible = false;
                hStatus.Visible = false;
                hMatCCID.Visible = false;
                hIdSimpan.Visible = false;
                tr.Cells[3].InnerText = tPrice.Text;
                tPrice.Visible = false;
            }
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=CostMonitoringFinishing.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();

            Response.Write("<table>");
            Response.Write(sw.ToString());

            Response.Write("</table>");
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<b>COST MONITORING FINISHING</b>";
            Html += "<br>Periode : " + ddlBulan.SelectedItem + " " + ddlTahun.SelectedValue;
            Html += "";
            string HtmlEnd = "";
            lst.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0\"", "border=\"1\"");
            Response.Write(Html + Contents.Trim() + HtmlEnd);
            Response.Flush();
            Response.End();
        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            ArrayList arrData = new ArrayList();
            for (int i = 0; i < lstFin.Items.Count; i++)
            {
                HiddenField ItemIDx = (HiddenField)lstFin.Items[i].FindControl("txtItemID");
                HiddenField NilaiID = (HiddenField)lstFin.Items[i].FindControl("txtSimpanID");
                TextBox txt = (TextBox)lstFin.Items[i].FindControl("txtPrice");
                CostFIN cf = new CostFIN();
                decimal price = 0;
                decimal.TryParse(txt.Text, out price);
                if (ItemIDx != null)
                {
                    if (int.Parse(ItemIDx.Value) > 0 && price > 0)
                    {
                        cf.ItemID = int.Parse(ItemIDx.Value);
                        cf.Price = price;
                        cf.Bulan = int.Parse(ddlBulan.SelectedValue);
                        cf.Tahun = int.Parse(ddlTahun.SelectedValue);
                        cf.CreatedBy = ((Users)Session["Users"]).UserName;
                        cf.CreatedTime = DateTime.Now;
                        cf.ID = int.Parse(NilaiID.Value);
                        CostCenterFacade c = new CostCenterFacade();
                        int result = c.InsertBudgetNilaiFin(cf, (int.Parse(NilaiID.Value) > 0) ? false : true);
                        if (result > 0)
                        {
                            txt.BackColor = System.Drawing.Color.YellowGreen;
                        }
                    }
                }
            }
        }
        protected void lstFin_DataBound(object sender, RepeaterItemEventArgs e)
        {
            CostFIN cf = (CostFIN)e.Item.DataItem;
            HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("tr1");
            HiddenField NilaiID = (HiddenField)e.Item.FindControl("txtSimpanID");
            TextBox txt = (TextBox)e.Item.FindControl("txtPrice");
            nomor = (cf.RowStatus == 0) ? nomor + 1 : nomor;
            tr.Visible = (cf.MaterialGroupID == 1000 && cf.RowStatus == 1) ? false : true;
            tr.Attributes.Add("class", (cf.RowStatus == 0 || cf.RowStatus == 2) ? "total baris" : "EvenRows baris");
            tr.Cells[0].InnerHtml = (cf.RowStatus == 0) ? nomor.ToString() : "";
            tr.Cells[0].Align = "Center";
            tr.Cells[4].InnerHtml = (cf.Barang == 0) ? "" : (cf.Quantity == 0) ? "" : cf.Barang.ToString("N2");
            tr.Cells[5].InnerHtml = (cf.Lembar == 0) ? "" : (cf.Quantity == 0) ? "" : cf.Lembar.ToString("N0");
            tr.Cells[6].InnerHtml = (cf.RupiahPerBln == 0) ? "" : "0";// cf.RupiahPerBln.ToString("N0");
            tr.Cells[7].InnerHtml = (cf.MaterialGroupID == 1000 && cf.RowStatus == 2) ? "" : tr.Cells[7].InnerText.ToString();
            tr.Cells[10].InnerHtml = cf.Price.ToString("N0");
            tr.Cells[9].InnerHtml = (cf.ProdukOut > 0) ? cf.ProdukOut.ToString("N0") : "0";

            decimal price, avgPrice = 0;
            decimal.TryParse(this.DataNilai(cf.ItemID, "Price").ToString(), out price);
            avgPrice = (cf.Quantity > 0) ? this.DataNilai(cf.MaterialGroupID, "Price", true) : 0;
            txt.Text = (cf.RowStatus == 2) ? avgPrice.ToString("N0") : (cf.Quantity > 0) ? price.ToString("N0") : "";
            txt.Attributes.Add("class", "txtongrid fontkecil angka");
            NilaiID.Value = this.DataNilai(cf.ItemID, "ID");
            /*
             * Perhitungannaya
             * Kolom Lembar/Satuan Barang
             */
            decimal spb = 0; decimal output = 0;
            //decimal HargaPerSatuan = 0;
            decimal BarangSatuan = 0;
            decimal.TryParse(tr.Cells[7].InnerText, out spb);
            decimal.TryParse(tr.Cells[9].InnerText, out output);
            BarangSatuan = (cf.Barang > 0) ? (output / cf.Barang) : 0;
            BarangSatuan = (cf.Lembar > 0) ? (output / cf.Lembar) : BarangSatuan;
            BarangSatuan = (cf.RupiahPerBln > 0) ? (output / cf.RupiahPerBln) : BarangSatuan;
            tr.Cells[8].InnerHtml = (output > 0 && spb > 0/*&& cf.RowStatus==1*/) ? (output / spb).ToString("N0") : "";
            tr.Cells[4].InnerHtml = (output > 0 && cf.RowStatus == 1) ? BarangSatuan.ToString("N2") : "";
            if (txt.Text == string.Empty || txt.Text == "0") { txt.Text = ""; }
        }
        private decimal DataNilai(int p, string p_2, bool p_3)
        {
            CostCenterFacade cn = new CostCenterFacade();
            decimal result = 0;
            cn.Bulan = ddlBulan.SelectedValue;
            cn.Tahun = ddlTahun.SelectedValue;
            result = cn.RetrieveTotalNilaiFIN(p);
            return result;
        }
        private string DataNilai(int ItemID, string Field)
        {
            CostCenterFacade cn = new CostCenterFacade();
            string result = "0";
            cn.Bulan = ddlBulan.SelectedValue;
            cn.Tahun = ddlTahun.SelectedValue;
            result = cn.RetieveNilaiFIN(ItemID, Field);
            return result;
        }
        protected void txtPrice_Change(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            int idx = int.Parse(t.ToolTip);
            string itemname = string.Empty;
            CostCenterFacade cfd = new CostCenterFacade();
            //for (int i = 0; i < lstFin.Items.Count; i++)
            //{
            // TextBox tHarga = (TextBox)lstFin.Items[idx].FindControl("txtPrice");
            HiddenField tItemName = (HiddenField)lstFin.Items[idx].FindControl("txtBarang");
            HtmlTableRow tr = (HtmlTableRow)lstFin.Items[idx].FindControl("tr1");
            HtmlTableRow trTotal = ((idx + 1) < lstFin.Items.Count) ? (HtmlTableRow)lstFin.Items[idx + 1].FindControl("tr1") : new HtmlTableRow();
            decimal tPrice = 0; decimal KolBarangSatuan = 0; decimal SPB = 0; decimal output = 0; //decimal KolBarangSatuan1 = 0;
            decimal.TryParse(t.Text, out tPrice);
            decimal.TryParse(tr.Cells[4].InnerText, out KolBarangSatuan);
            decimal.TryParse(tr.Cells[7].InnerText, out SPB);
            //decimal.TryParse(tr.Cells[9].InnerText,out output);
            itemname = tItemName.Value;

            if (itemname.Trim().Length >= 6)
            {
                if (itemname.Substring(0, 6).ToUpper() == "SARUNG")
                {
                    KolBarangSatuan = cfd.GetBudgetSarungTangan(ddlTahun.SelectedValue.ToString(), ddlBulan.SelectedValue.ToString());
                }
            }
            if (itemname.Trim().Length >= 6)
            {
                if (itemname.ToUpper() == "MASKER")
                {
                    KolBarangSatuan = cfd.GetBudgetMasker(ddlTahun.SelectedValue.ToString(), ddlBulan.SelectedValue.ToString());
                }
            }
            //add by razib -- mohon di koreksi pak 
            if (itemname.Trim().Length >= 21)
            {
                if (itemname.Substring(0, 21).ToUpper() == "SARUNG TANGAN KAIN B8")
                {
                    KolBarangSatuan = cfd.GetBudgetSarungTanganB8(ddlTahun.SelectedValue.ToString(), ddlBulan.SelectedValue.ToString());

                }
            }
            //add by razib -- mohon di koreksi pak 
            if (itemname.Trim().Length >= 16)
            {
                if (itemname.Substring(0, 16).ToUpper() == "MASKER KAIN TALI")
                {
                    KolBarangSatuan = cfd.GetBudgetMaskerKT(ddlTahun.SelectedValue.ToString(), ddlBulan.SelectedValue.ToString());
                }
            }
            //if (itemname.Substring(10, 4).ToUpper() == "LAIN")
            //{
            //   //KolBarangSatuan =1;
            //}
            tr.Cells[4].InnerHtml = (KolBarangSatuan > 0) ? (KolBarangSatuan).ToString("N2") : "";
            if (tPrice > 0)
            {
                /* kolom rupiah perbulan budget
                 * Rumus Kolom Harga Persatuan * kolom Barang Satuan (Budget)
                 */
                //tr.Cells[6].InnerHtml = (tr.Cells[4].InnerText == "") ? "" : (tPrice * KolBarangSatuan).ToString("N0");
                tr.Cells[6].InnerHtml = (KolBarangSatuan > 0) ? (tPrice * KolBarangSatuan).ToString("N0") : "";
                //trTotal.Cells[6].InnerHtml="";
                /* kolom rupiah perbulan aktual
                 * Rumus Kolom Harga Persatuan * kolom Barang Satuan (Budget)
                 */
                // tr.Cells[10].InnerHtml = (tr.Cells[4].InnerText == "") ? "" : (tPrice * SPB).ToString("N0");
                /* kolom prosen
                 * Rumus Rupiah per bulan aktual / rupiah per bulan budget * 100
                 */
                //tr.Cells[11].InnerHtml = (tr.Cells[5].InnerText == "") ? "" : ((tPrice * SPB) / (tPrice * KolBarangSatuan)).ToString("P2");
                // tr.Cells[11].InnerHtml = (tr.Cells[0].InnerText == "") ? "test" :tr.Cells[4].InnerText;
                /* kolom rupiah pe lembar
                 * rumus output/rupiah perbulan
                 */
                if (output > 0)
                    tr.Cells[12].InnerHtml = (KolBarangSatuan == 0) ? "" : ((tPrice * SPB) / output).ToString("N0");
            }
            //}
        }
    }

    public class BudgetFinishing
    {
        public string ItemSarmut { get; set; }
        public string ParameterTerukur { get; set; }
        public string Persen { get; set; }
        public string Flag { get; set; }
        public int Rowstatus { get; set; }
    }

    public class FacadeBudgetFinishing
    {
        public string strError = string.Empty;
        private ArrayList arrData = new ArrayList();
        private BudgetFinishing objFinishing = new BudgetFinishing();
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
                    arrData.Add(new BudgetFinishing
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