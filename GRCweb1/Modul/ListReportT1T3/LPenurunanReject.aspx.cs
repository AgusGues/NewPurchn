using BusinessFacade;
using Domain;
using Factory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.ListReportT1T3
{
    public partial class LPenurunanReject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
                getYear();

                int LastDay = DateTime.DaysInMonth(int.Parse(ddTahun.SelectedValue.ToString()), int.Parse(ddlBulan.SelectedValue.ToString()));
                txtTglMulai.Text = LastDay.ToString().PadLeft(2, '0') + "-" + ddlBulan.SelectedValue.PadLeft(2, '0') + "-" + ddTahun.SelectedValue.ToString();
            }
        }
        private void getYear()
        {
            /**
             * Fill dropdown items with data tahun from database
             */
            ddTahun.Items.Clear();
            ArrayList arrTahun = new ArrayList();
            T3_MutasiWIPFacade T3_MutasiWIPFacade = new T3_MutasiWIPFacade();
            arrTahun = T3_MutasiWIPFacade.BM_Tahun();
            ddTahun.Items.Clear();
            ddTahun.Items.Add(new ListItem("-- Pilih Tahun --", "0"));
            foreach (T3_MutasiWIP bmTahun in arrTahun)
            {
                ddTahun.Items.Add(new ListItem(bmTahun.Tahune.ToString(), bmTahun.Tahune.ToString()));
            }
            ddTahun.SelectedValue = DateTime.Now.Year.ToString();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string periodeBulan = ddlBulan.SelectedValue;
            string txtBulan = ddlBulan.SelectedItem.ToString();
            string periodeTahun = ddTahun.SelectedValue;
            string frmtPrint = string.Empty;
            string padbulan = ddlBulan.SelectedValue.ToString().PadLeft(2, '0');
            Users users = (Users)Session["Users"];
            Company cmp = new Company();
            CompanyFacade cmpf = new CompanyFacade();
            cmp = cmpf.RetrieveByDepoId(users.UnitKerjaID);
            int userID = ((Users)Session["Users"]).ID;
            ReportFacadeT1T3 reportFacade = new ReportFacadeT1T3();
            string strQuery = string.Empty;

            /** Added 20 Juli 2022 : Beny **/
            string Judul = string.Empty; string info1 = string.Empty;
            int PeriodeBlnTahun = 0;
            PeriodeBlnTahun = Convert.ToInt32(periodeTahun + padbulan);
            if (PeriodeBlnTahun >= 202204)
            {
                Judul = "LAPORAN REJECT & STOCK PRODUK";
                info1 = "Reject Produk";
            }
            else
            {
                Judul = "LAPORAN PENURUNAN REJECT & STOCK PRODUK";
                info1 = "Penurunan Reject";
            }

            Session["Judul"] = Judul.Trim(); Session["Info1"] = info1.Trim();
            /** end Added **/

            Session["periode"] = txtBulan + " " + periodeTahun;
            strQuery = reportFacade.ViewPenurunanReject(periodeTahun + padbulan);
            Session["Query"] = strQuery;

            #region Yoga SarmutToPES
            string sarmutPrs = "Penurunan Reject Produk ";
            ArrayList arrData = new ArrayList();
            ZetroView zz = new ZetroView();
            zz.QueryType = Operation.CUSTOM;
            decimal actual = 0;
            int bulan = Convert.ToInt32(ddlBulan.SelectedValue) - 1;
            zz.CustomQuery = "select * from SPD_TransPrs where tahun=" + ddTahun.SelectedValue + " and bulan=" + ddlBulan.SelectedValue + " and SarmutPID in " +
                             "(select ID from SPD_Perusahaan where deptid=(select ID from spd_dept where dept like '%finishing%') and  " +
                             "rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "') ";
            SqlDataReader zdr = zz.Retrieve();
            if (zdr.HasRows)
            {
                while (zdr.Read())
                {
                    actual = (zdr["Actual"] == DBNull.Value) ? 0 : Convert.ToDecimal(zdr["Actual"]);
                }
            }

            ZetroView zs = new ZetroView();
            zs.QueryType = Operation.CUSTOM;
            decimal targetSarmutFin = 0;
            zs.CustomQuery = "SELECT top 1 * FROM SPD_TransPrs WHERE SarmutPID IN (SELECT ID from SPD_Perusahaan where Rowstatus>-1 and " +
                             "SarMutPerusahaan='" + sarmutPrs + "') AND Approval>1 order by id desc ";
            SqlDataReader adr = zs.Retrieve();
            if (adr.HasRows)
            {
                while (adr.Read())
                {
                    targetSarmutFin = (adr["Target"] == DBNull.Value) ? 0 : Convert.ToDecimal(adr["Target"]);
                }
            }
            if (Convert.ToDouble(actual) > 0)
            {
                string strError = "";
                //decimal newActual = (grandTotalA / grandTotal) * 100;
                string ketPes = (actual <= targetSarmutFin) ? "<1.0%" : (Convert.ToDouble(actual) >= 1.0 && Convert.ToDouble(actual) <= 2.0) ? "1.0-2.0%" : ">2.0%";
                //string valueActual = ((newActual / targetSarmutFin) * 100).ToString();
                //double percentActual = Convert.ToDouble(valueActual);
                ZetroView zv = new ZetroView();
                zv.QueryType = Operation.CUSTOM;
                int IdKPI = 0;
                string kpiName = string.Empty;
                zv.CustomQuery = "SELECT * FROM ISO_KPI WHERE DeptID=3 and month(TglMulai)=" + ddlBulan.SelectedValue + " AND year(TglMulai)=" + ddTahun.SelectedValue + " ORDER BY ID desc";
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
                    int LastDay = DateTime.DaysInMonth(int.Parse(ddTahun.SelectedValue.ToString()), int.Parse(ddlBulan.SelectedValue.ToString()));
                    ArrayList arrSarPes = new ArrayList();
                    SarmutPESFacade sarPesFacade = new SarmutPESFacade();
                    arrSarPes = sarPesFacade.RetrieveUserCategory(sarmutPrs);
                    foreach (SarmutPes uc in arrSarPes)
                    {
                        int idUserCategory = uc.IDUserCategory;
                        int IDuser = uc.UserID;
                        int bagianID = uc.BagianID;
                        decimal bobotNilai = uc.BobotNilai;
                        string pic = uc.Pic;
                        int deptID = uc.DeptID;
                        string description = uc.Description;
                        int pesType = uc.PesType;
                        int categoryID = uc.CategoryID;
                        //uc.Actual = string.Concat(actual.ToString(), valueActual.ToString()) ;
                        uc.Ket = actual.ToString().Replace(",", ".");
                        //uc.Percent = valueActual.ToString();
                        //uc.TglMulai = Convert.ToDateTime(txtTglMulai.Text);
                        int pjgDept = ((Users)Session["Users"]).DeptID;
                        string ddlDept = "FINISHING";
                        uc.DeptName = (pjgDept >= 4) ? ddlDept.Substring(0, 3) : ddlDept.Substring(0, pjgDept);
                        txtTglMulai.Text = Convert.ToDateTime(uc.TglMulai).ToString();
                        uc.TglMulai = Convert.ToDateTime(LastDay.ToString().PadLeft(2, '0') + "-" + ddlBulan.SelectedValue.PadLeft(2, '0') + "-" + ddTahun.SelectedValue.ToString());

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
                                    ketActual = actual.ToString().Replace(",", ".");
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

                        arrSarPesUPdate2 = updatesarPesFacade.RetrieveID(deptID, ddlBulan.SelectedValue, ddTahun.SelectedValue, categoryID);
                        foreach (SarmutPes tp in arrSarPesUPdate2)
                        {
                            //SarmutPes updateSarPes = updatesarPesFacade.RetrieveID(deptID, ddlBulan.SelectedValue, ddTahun.SelectedValue, categoryID);
                            int id = tp.ID;

                            SarmutPes updateSarPesScore = updatesarPesFacade.RetrieveUpdateScore(categoryID, ketPes);
                            int IDScore = updateSarPesScore.IDScore;
                            int CategoryID = updateSarPesScore.CategoryID;
                            string TargetKe = updateSarPesScore.KetTargetKe;
                            decimal PointNilai = updateSarPesScore.PointNilai;

                            up.KPIID = id;
                            up.Ket = actual.ToString().Replace(",", ".");
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

            //Cetak(this);
            Session["yearmonth"] = periodeTahun + padbulan;
            Session["plant"] = cmp.Lokasi;
        }

        private string SimpanKPI(SarmutPes sop)
        {
            string strEvent = "Insert";
            ISO_DocumentNoFacade docNoFacade = new ISO_DocumentNoFacade();
            ISO_DocumentNo docNo = docNoFacade.RetrieveByDept(1, 3, Convert.ToDateTime(txtTglMulai.Text).Year);
            if (docNo.ID > 0)
            {
                docNo.DocNo = docNo.DocNo + 1;
                //docNo.PesType = int.Parse(txtPES.Text); //sop
                docNo.DeptID = 3;
                docNo.Tahun = Convert.ToDateTime(txtTglMulai.Text).Year;
                Company cp = new CompanyFacade().RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                docNo.Plant = cp.KodeLokasi;
            }
            else
            {
                docNo.DocNo = 1;
                //docNo.PesType = int.Parse(txtPES.Text); //sop
                docNo.DeptID = 3;
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
            ISO_DocumentNo docNo = docNoFacade.RetrieveByDept(1, 3, Convert.ToDateTime(txtTglMulai.Text).Year);
            if (docNo.ID > 0)
            {
                docNo.DocNo = docNo.DocNo + 1;
                //docNo.PesType = int.Parse(txtPES.Text); //sop
                docNo.DeptID = 3;
                docNo.Tahun = Convert.ToDateTime(txtTglMulai.Text).Year;
                Company cp = new CompanyFacade().RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                docNo.Plant = cp.KodeLokasi;
            }
            else
            {
                docNo.DocNo = 1;
                //docNo.PesType = int.Parse(txtPES.Text); //sop
                docNo.DeptID = 3;
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

        static public void Cetak(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=PenurunanReject', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            string myScript = "Cetak()";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        static public void Cetak2(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LPemantauan4mm', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            string myScript = "Cetak2()";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "MyScript", myScript, true);
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void ddTahun_SelectedIndexChanged(object sender, EventArgs e)
        {
            int LastDay = DateTime.DaysInMonth(int.Parse(ddTahun.SelectedValue.ToString()), int.Parse(ddlBulan.SelectedValue.ToString()));
            txtTglMulai.Text = LastDay.ToString().PadLeft(2, '0') + "-" + ddlBulan.SelectedValue.PadLeft(2, '0') + "-" + ddTahun.SelectedValue.ToString();
        }

        protected void ddlBulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            //getYear();
            int LastDay = DateTime.DaysInMonth(int.Parse(ddTahun.SelectedValue.ToString()), int.Parse(ddlBulan.SelectedValue.ToString()));
            txtTglMulai.Text = LastDay.ToString().PadLeft(2, '0') + "-" + ddlBulan.SelectedValue.PadLeft(2, '0') + "-" + ddTahun.SelectedValue.ToString();
        }

    }
}