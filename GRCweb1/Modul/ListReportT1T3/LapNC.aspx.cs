using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using Domain;
using DataAccessLayer;
using BusinessFacade;
using Factory;
using BasicFrame.WebControls;

namespace GRCweb1.Modul.ListReportT1T3
{
    public partial class LapNC : System.Web.UI.Page
    {
        int TotalQty = 0; decimal TotalM3 = 0;
        int TotalQty_1 = 0; decimal TotalQty_2 = 0;
        decimal TotalM3_1 = 0; decimal TotalM3_2 = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
                getYear();

                btnExport.Enabled = false;

                //int LastDay = DateTime.DaysInMonth(int.Parse(ddTahun.SelectedValue.ToString()), int.Parse(ddlBulan.SelectedValue.ToString()));
                //txtTglMulai.Text = LastDay.ToString().PadLeft(2, '0') + "-" + ddlBulan.SelectedValue.PadLeft(2, '0') + "-" + ddTahun.SelectedValue.ToString();
            }
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
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

        protected void btnP_Click(object sender, EventArgs e)
        {
            btnExport.Enabled = true;
            NCdomain BSD1 = new NCdomain();
            NCfacade NCF1 = new NCfacade();

            string txtBulan = ddlBulan.SelectedItem.ToString();
            string periodeTahun = ddTahun.SelectedValue;
            string padbulan = ddlBulan.SelectedValue.ToString().PadLeft(2, '0');
            string thnblbln = periodeTahun.Trim() + padbulan.Trim();

            decimal lembar = NCF1.GetTotalLembarOK(thnblbln);
            decimal kubik = NCF1.GetTotalKubikOK(thnblbln);
            Session["lembar"] = lembar; Session["kubik"] = kubik;


            if (RB_Rekap.Checked == true)
            {
                PaneLS.Visible = false; PaneLH.Visible = true;

                Session["periode"] = txtBulan + " " + periodeTahun;
                //update sarmut
                decimal aktual = 0;
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "declare @thnbln varchar(6) set @thnbln='" + thnblbln + "' " +
                "select cast( (select sum(kubikakhir) from ( " +
                "SELECT I2.PartNo AS PartnoAkhir, sum(B.QtyOut) as LembarAkhir,sum(I2.Volume*B.QtyOut) KubikAkhir  " +
                "FROM T3_Simetris AS B inner join FC_Items AS I2 ON B.ItemID = I2.ID LEFT JOIN T3_GroupM AS D ON B.GroupID = D.ID where B.NCH>0 and B.rowstatus>-1 and  " +
                "left(convert(varchar,B.tgltrans,112),6)=@thnbln group by I2.PartNo)a)/ " +
                "(select SUM(kubik)kubik from ( select isnull(SUM(A.qty*((I.tebal*I.panjang*I.lebar)/1000000000)),0) kubik from vw_KartuStockBJNew A  " +
                "inner join fc_items I on A.itemid=I.ID  where LEFT(convert(char,A.tanggal,112),6)=@thnbln and A.qty>0 and A.Process not in ('adjust-IN','retur','direct') and   (I.PartNo  like '%-3-%' or I.PartNo   " +
                "like '%-W-%' or I.PartNo  like '%-M-%') and (Keterangan like '%-P-%' or Keterangan like '%-1-%' ) union all  " +
                "select isnull(SUM(A.qty*((I.tebal*I.panjang*I.lebar)/1000000000)),0) kubik from vw_KartuStockBJNew A inner join fc_items I on A.itemid=I.ID  where LEFT(convert(char,A.tanggal,112),6)=@thnbln and  " +
                "A.qty>0 and A.Process in ('direct') and   (I.PartNo  like '%-3-%' or I.PartNo  like '%-W-%' or I.PartNo  like '%-M-%'))S) *100 as decimal(18,2)) aktual";
                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["aktual"].ToString());
                    }
                }
                string sarmutPrs = "Nonconformity Produk Akibat Handling";
                int deptid = getDeptID("Logistik f");
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery =
                    "update SPD_TransPrs set actual= " + aktual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddTahun.SelectedValue +
                    " and Bulan=" + ddlBulan.SelectedValue +
                    " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "' and deptid=" + deptid + ") ";
                SqlDataReader sdr1 = zl1.Retrieve();

                PaneLS.Visible = false; PaneLH.Visible = true;

                NCdomain BSD = new NCdomain();
                NCfacade NCF = new NCfacade();
                ArrayList arrData = new ArrayList();
                arrData = NCF.RetrieveDataNCH(thnblbln);
                lst.DataSource = arrData;
                lst.DataBind();
            }
            else
            {
                PaneLS.Visible = true; PaneLH.Visible = false;

                Session["periode"] = txtBulan + " " + periodeTahun;
                //update sarmut
                decimal aktual = 0;
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "declare @thnbln varchar(6) set @thnbln='" + thnblbln + "' " +
                "select cast( (select sum(M3AkhirSS +M3AkhirSE ) from ( " +
                "select PartnoAkhir,sum(QtyAkhirSS)QtyAkhirSS,sum(M3AkhirSS)M3AkhirSS, sum(QtyAkhirSE)QtyAkhirSE,sum(M3AkhirSE)M3AkhirSE from ( SELECT B.tgltrans as Tanggal, I1.PartNo as PartnoAwal,  " +
                "sum(B.QtyIn) as QtyAwal,sum(I1.Volume*B.QtyIn) M3Awal,  I2.PartNo AS PartnoAkhir, case when B.NCSS>0 then sum(B.QtyOut) else 0 end QtyAkhirSS,  " +
                "case when B.NCSS>0 then sum(I2.Volume*B.QtyOut) else 0 end M3AkhirSS,case when B.NCSE>0 then sum(B.QtyOut) else 0 end QtyAkhirSE,  " +
                "case when B.NCSE>0 then sum(I2.Volume*B.QtyOut) else 0 end M3AkhirSE,B.NCSS,B.NCSE  FROM T3_Serah AS A  INNER JOIN T3_Simetris AS B ON A.ID = B.SerahID  " +
                "INNER JOIN FC_Items AS I1 ON A.ItemID = I1.ID INNER JOIN  FC_Items AS I2 ON B.ItemID = I2.ID LEFT JOIN T3_GroupM AS D ON B.GroupID = D.ID   " +
                "where (B.NCSS>0 or B.NCSE>0) and B.rowstatus>-1 and left(convert(varchar,B.tgltrans,112),6)=@thnbln  group by B.tgltrans,I1.PartNo,I2.PartNo,B.NCSS,B.NCSE) S  group by PartnoAkhir)a)/ " +
                "(select SUM(kubik)kubik from ( select isnull(SUM(A.qty*((I.tebal*I.panjang*I.lebar)/1000000000)),0) kubik from vw_KartuStockBJNew A  " +
                "inner join fc_items I on A.itemid=I.ID  where LEFT(convert(char,A.tanggal,112),6)=@thnbln and A.qty>0 and A.Process not in ('adjust-IN','retur','direct') and   (I.PartNo  like '%-3-%' or I.PartNo   " +
                "like '%-W-%' or I.PartNo  like '%-M-%') and (Keterangan like '%-P-%' or Keterangan like '%-1-%' ) union all  " +
                "select isnull(SUM(A.qty*((I.tebal*I.panjang*I.lebar)/1000000000)),0) kubik from vw_KartuStockBJNew A inner join fc_items I on A.itemid=I.ID  where LEFT(convert(char,A.tanggal,112),6)=@thnbln and  " +
                "A.qty>0 and A.Process in ('direct') and   (I.PartNo  like '%-3-%' or I.PartNo  like '%-W-%' or I.PartNo  like '%-M-%'))S) *100 as decimal(18,2)) aktual";
                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["aktual"].ToString());
                    }
                }
                string sarmutPrs = "Nonconformity Produk Akibat Proses Sortir";
                int deptid = getDeptID("quality");
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery =
                    "update SPD_TransPrs set actual= " + aktual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddTahun.SelectedValue +
                    " and Bulan=" + ddlBulan.SelectedValue +
                    " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "' and deptid=" + deptid + ") ";
                SqlDataReader sdr1 = zl1.Retrieve();

                #region Yoga SarmutToPES
                ArrayList arrData = new ArrayList();
                ZetroView zs = new ZetroView();
                zs.QueryType = Operation.CUSTOM;
                decimal targetSarmutQA = 0;
                int bulan = Convert.ToInt32(ddlBulan.SelectedValue) - 1;
                zs.CustomQuery = "SELECT top 1 * FROM SPD_TransPrs WHERE SarmutPID IN (SELECT ID from SPD_Perusahaan where Rowstatus>-1 and " +
                                 "SarMutPerusahaan='" + sarmutPrs + "') AND Approval>1 order by id desc ";
                SqlDataReader adr = zs.Retrieve();
                if (adr.HasRows)
                {
                    while (adr.Read())
                    {
                        targetSarmutQA = (adr["Target"] == DBNull.Value) ? 0 : Convert.ToDecimal(adr["Target"]);
                    }
                }
                if (Convert.ToInt32(kubik) > 0)
                {
                    string strError = "";
                    //decimal newActual = (grandTotalA / grandTotal) * 100;
                    string ketPes = (aktual < targetSarmutQA) ? "< 0.01%" : (Convert.ToDouble(aktual) >= 0.010 && Convert.ToDouble(aktual) <= 0.025) ? "0.010% - 0.025%" : (Convert.ToDouble(aktual) >= 0.026 && Convert.ToDouble(aktual) <= 0.045) ? "0,026% - 0,045%" : (Convert.ToDouble(aktual) >= 0.046 && Convert.ToDouble(aktual) <= 0.06) ? "0.046% - 0.06%" : "> 0.060%";
                    //string valueActual = ((newActual / targetSarmutQA) * 100).ToString();
                    //double percentActual = Convert.ToDouble(valueActual);
                    ZetroView zv = new ZetroView();
                    zv.QueryType = Operation.CUSTOM;
                    int IdKPI = 0;
                    string kpiName = string.Empty;
                    zv.CustomQuery = "SELECT * FROM ISO_KPI WHERE DeptID=9 and month(TglMulai)=" + ddlBulan.SelectedValue + " AND year(TglMulai)=" + ddTahun.SelectedValue + " ORDER BY ID desc";
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
                            int userID = uc.UserID;
                            int bagianID = uc.BagianID;
                            decimal bobotNilai = uc.BobotNilai;
                            string pic = uc.Pic;
                            int deptID = uc.DeptID;
                            string description = uc.Description;
                            int pesType = uc.PesType;
                            int categoryID = uc.CategoryID;
                            //uc.Actual = string.Concat(actual.ToString(), valueActual.ToString()) ;
                            uc.Ket = aktual.ToString().Replace(",", ".");
                            //uc.Percent = valueActual.ToString();
                            //uc.TglMulai = Convert.ToDateTime(txtTglMulai.Text);
                            int pjgDept = ((Users)Session["Users"]).DeptID;
                            string ddlDept = "QUALITY ASSURANCE";
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
                                        ketActual = aktual.ToString().Replace(",", ".");
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
                                up.Ket = aktual.ToString().Replace(",", ".");
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

                PaneLS.Visible = true; PaneLH.Visible = false;

                NCdomain BSD = new NCdomain();
                NCfacade NCF = new NCfacade();
                arrData = NCF.RetrieveDataNCS(thnblbln);
                lst2.DataSource = arrData;
                lst2.DataBind();
            }
        }
        private string SimpanKPI(SarmutPes sop)
        {
            string strEvent = "Insert";
            ISO_DocumentNoFacade docNoFacade = new ISO_DocumentNoFacade();
            ISO_DocumentNo docNo = docNoFacade.RetrieveByDept(1, 9, Convert.ToDateTime(txtTglMulai.Text).Year);
            if (docNo.ID > 0)
            {
                docNo.DocNo = docNo.DocNo + 1;
                //docNo.PesType = int.Parse(txtPES.Text); //sop
                docNo.DeptID = 9;
                docNo.Tahun = Convert.ToDateTime(txtTglMulai.Text).Year;
                Company cp = new CompanyFacade().RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                docNo.Plant = cp.KodeLokasi;
            }
            else
            {
                docNo.DocNo = 1;
                //docNo.PesType = int.Parse(txtPES.Text); //sop
                docNo.DeptID = 9;
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
            ISO_DocumentNo docNo = docNoFacade.RetrieveByDept(1, 9, Convert.ToInt32(ddTahun.SelectedValue));
            if (docNo.ID > 0)
            {
                docNo.DocNo = docNo.DocNo + 1;
                //docNo.PesType = int.Parse(txtPES.Text); //sop
                docNo.DeptID = 9;
                docNo.Tahun = Convert.ToInt32(ddTahun.SelectedValue);
                Company cp = new CompanyFacade().RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                docNo.Plant = cp.KodeLokasi;
            }
            else
            {
                docNo.DocNo = 1;
                //docNo.PesType = int.Parse(txtPES.Text); //sop
                docNo.DeptID = 9;
                docNo.Tahun = Convert.ToInt32(ddTahun.SelectedValue);
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

        protected void lst2_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Users user = (Users)Session["Users"];
            NCdomain nc2 = (NCdomain)e.Item.DataItem;
            string hr = DateTime.Now.Day.ToString(); string tahun = DateTime.Now.Year.ToString(); string bln = DateTime.Now.Month.ToString();
            string bulan = string.Empty; string hari = string.Empty;
            string bln1 = ddlBulan.SelectedItem.ToString();

            if (hr.Length == 1)
            {
                hari = "0" + hr; ;
            }

            if (bln == "1") { bulan = "Januari"; }
            else if (bln == "2") { bulan = "Februari"; }
            else if (bulan == "3") { bulan = "Maret"; }
            else if (bln == "4") { bulan = "April"; }
            else if (bln == "5") { bulan = "Mei"; }
            else if (bln == "6") { bulan = "Juni"; }
            else if (bln == "7") { bulan = "Juli"; }
            else if (bln == "8") { bulan = "Agustus"; }
            else if (bln == "9") { bulan = "September"; }
            else if (bln == "10") { bulan = "Oktober"; }
            else if (bln == "11") { bulan = "November"; } else if (bln == "12") { bulan = "Desember"; }

            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("isi2");
                    if (tr != null)
                    {
                        TotalQty_1 += nc2.QtyAkhirSS; TotalQty_2 += nc2.QtyAkhirSE;
                        TotalM3_1 += nc2.M3AkhirSS; TotalM3_2 += nc2.M3AkhirSE;
                    }
                }
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    HtmlTableRow tr2 = (HtmlTableRow)e.Item.FindControl("ftr");
                    tr2.Cells[1].InnerText = TotalQty_2.ToString("N0");
                    tr2.Cells[2].InnerText = TotalQty_1.ToString("N0");
                    tr2.Cells[3].InnerText = TotalM3_2.ToString("N2");
                    tr2.Cells[4].InnerText = TotalM3_1.ToString("N2");
                }
                decimal lmbr = Convert.ToDecimal(Session["lembar"]);
                decimal m3 = Convert.ToDecimal(Session["kubik"]);

                lbl1.Text = lmbr.ToString("N0");
                lbl2.Text = m3.ToString("N2");
                lbl3.Text = (((TotalM3_2 + TotalM3_1) / m3) * 100).ToString("N2");

                //lblLembar.Text = lmbr.ToString("N0");
                //lblKubik.Text = m3.ToString("N2");
                //lblPersen.Text = ((TotalM3 / m3) * 100).ToString("N2");
                lblbpas01.Text = "PT. BANGUNPERKASA ADHITAMASENTRA";
                lblJdl01.Text = "LEMBAR PEMANTAUAN";
                lblJdl02.Text = "NON CONFORMITY PRODUK AKIBAT PROSES SORTIR";
                lblPeriode01.Text = "Bulan :" + " " + bln1 + " " + tahun;
                if (user.UnitKerjaID == 1)
                {
                    lblplant01.Text = "Citeureup" + "," + hari + "-" + bulan + "-" + tahun;
                }
                else if (user.UnitKerjaID == 7)
                {
                    lblplant01.Text = "Karawang" + "," + hari + "-" + bulan + "-" + tahun;
                }
                else if (user.UnitKerjaID == 13)
                {
                    lblplant01.Text = "Jombang" + "," + hari + "-" + bulan + "-" + tahun;
                }

            }
            catch { }
        }

        protected void lst_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Users user = (Users)Session["Users"];
            NCdomain nc = (NCdomain)e.Item.DataItem;
            string hr = DateTime.Now.Day.ToString(); string tahun = DateTime.Now.Year.ToString(); string bln = DateTime.Now.Month.ToString();
            string bulan = string.Empty; string hari = string.Empty;
            string bln1 = ddlBulan.SelectedItem.ToString();

            if (hr.Length == 1)
            {
                hari = "0" + hr; ;
            }

            if (bln == "1") { bulan = "Januari"; }
            else if (bln == "2") { bulan = "Februari"; }
            else if (bulan == "3") { bulan = "Maret"; }
            else if (bln == "4") { bulan = "April"; }
            else if (bln == "5") { bulan = "Mei"; }
            else if (bln == "6") { bulan = "Juni"; }
            else if (bln == "7") { bulan = "Juli"; }
            else if (bln == "8") { bulan = "Agustus"; }
            else if (bln == "9") { bulan = "September"; }
            else if (bln == "10") { bulan = "Oktober"; }
            else if (bln == "11") { bulan = "November"; } else if (bln == "12") { bulan = "Desember"; }

            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("isi");
                    if (tr != null)
                    {
                        TotalQty += nc.LembarAkhir; TotalM3 += nc.KubikAkhir;
                    }
                }
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    HtmlTableRow tr2 = (HtmlTableRow)e.Item.FindControl("ftr");
                    tr2.Cells[1].InnerText = TotalQty.ToString("N0");
                    tr2.Cells[2].InnerText = TotalM3.ToString("N2");
                }
                decimal lmbr = Convert.ToDecimal(Session["lembar"]);
                decimal m3 = Convert.ToDecimal(Session["kubik"]);

                lblLembar.Text = lmbr.ToString("N0");
                lblKubik.Text = m3.ToString("N2");
                lblPersen.Text = ((TotalM3 / m3) * 100).ToString("N2");
                lblbpas.Text = "PT. BANGUNPERKASA ADHITAMASENTRA";
                lblJdl1.Text = "LEMBAR PEMANTAUAN";
                lblJdl2.Text = "NON CONFORMITY PRODUK AKIBAT PROSES HANDLING";
                lblperiode.Text = "Bulan :" + " " + bln1 + " " + tahun;
                if (user.UnitKerjaID == 1)
                {
                    lblplant.Text = "Citeureup" + "," + hari + "-" + bulan + "-" + tahun;
                }
                else if (user.UnitKerjaID == 7)
                {
                    lblplant.Text = "Karawang" + "," + hari + "-" + bulan + "-" + tahun;
                }
                else if (user.UnitKerjaID == 13)
                {
                    lblplant.Text = "Jombang" + "," + hari + "-" + bulan + "-" + tahun;
                }
            }
            catch { }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string periodeBulan = ddlBulan.SelectedValue;
            string txtBulan = ddlBulan.SelectedItem.ToString();
            string periodeTahun = ddTahun.SelectedValue;
            string frmtPrint = string.Empty;
            string padbulan = ddlBulan.SelectedValue.ToString().PadLeft(2, '0');
            string thnblbln = periodeTahun.Trim() + padbulan.Trim();
            Users users = (Users)Session["Users"];
            int userID = ((Users)Session["Users"]).ID;
            ReportFacadeT1T3 reportFacade = new ReportFacadeT1T3();
            string strQuery = string.Empty;
            if (RB_Rekap.Checked == true)
            {
                if (RB_Harian.Checked == true)
                {
                    Session["periode"] = txtBulan + " " + periodeTahun;
                    strQuery = reportFacade.ViewNCHandling(thnblbln);
                    Session["Query"] = strQuery;
                    Session["thnbln"] = thnblbln;
                    Cetak(this);
                }
                else
                {
                    Session["periode"] = txtBulan + " " + periodeTahun;
                    //update sarmut
                    decimal aktual = 0;
                    ZetroView zl = new ZetroView();
                    zl.QueryType = Operation.CUSTOM;
                    zl.CustomQuery = "declare @thnbln varchar(6) set @thnbln='" + thnblbln + "' " +
                    "select cast( (select sum(kubikakhir) from ( " +
                    "SELECT I2.PartNo AS PartnoAkhir, sum(B.QtyOut) as LembarAkhir,sum(I2.Volume*B.QtyOut) KubikAkhir  " +
                    "FROM T3_Simetris AS B inner join FC_Items AS I2 ON B.ItemID = I2.ID LEFT JOIN T3_GroupM AS D ON B.GroupID = D.ID where B.NCH>0 and B.rowstatus>-1 and  " +
                    "left(convert(varchar,B.tgltrans,112),6)=@thnbln group by I2.PartNo)a)/ " +
                    "(select SUM(kubik)kubik from ( select isnull(SUM(A.qty*((I.tebal*I.panjang*I.lebar)/1000000000)),0) kubik from vw_KartuStockBJNew A  " +
                    "inner join fc_items I on A.itemid=I.ID  where LEFT(convert(char,A.tanggal,112),6)=@thnbln and A.qty>0 and A.Process not in ('adjust-IN','retur','direct') and   (I.PartNo  like '%-3-%' or I.PartNo   " +
                    "like '%-W-%' or I.PartNo  like '%-M-%') and (Keterangan like '%-P-%' or Keterangan like '%-1-%' ) union all  " +
                    "select isnull(SUM(A.qty*((I.tebal*I.panjang*I.lebar)/1000000000)),0) kubik from vw_KartuStockBJNew A inner join fc_items I on A.itemid=I.ID  where LEFT(convert(char,A.tanggal,112),6)=@thnbln and  " +
                    "A.qty>0 and A.Process in ('direct') and   (I.PartNo  like '%-3-%' or I.PartNo  like '%-W-%' or I.PartNo  like '%-M-%'))S) *100 as decimal(18,2)) aktual";
                    SqlDataReader sdr = zl.Retrieve();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            aktual = Convert.ToDecimal(sdr["aktual"].ToString());
                        }
                    }
                    string sarmutPrs = "Nonconformity Produk Akibat Handling";
                    int deptid = getDeptID("Logistik f");
                    ZetroView zl1 = new ZetroView();
                    zl1.QueryType = Operation.CUSTOM;
                    zl1.CustomQuery =
                        "update SPD_TransPrs set actual= " + aktual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddTahun.SelectedValue +
                        " and Bulan=" + ddlBulan.SelectedValue +
                        " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "' and deptid=" + deptid + ") ";
                    SqlDataReader sdr1 = zl1.Retrieve();
                    //end update sarmut
                    strQuery = reportFacade.ViewNCHandlingBln(thnblbln);
                    Session["Query"] = strQuery;
                    Session["thnbln"] = thnblbln;

                    T3_SimetrisFacade simF = new T3_SimetrisFacade();
                    decimal kubik = simF.GetTotalKubikOK(Session["thnbln"].ToString());
                    Session["kubikOK"] = kubik;
                    Cetak3(this);
                }
            }
            else
            {
                if (RB_Harian.Checked == true)
                {
                    Session["periode"] = txtBulan + " " + periodeTahun;
                    strQuery = reportFacade.ViewNCSortir(thnblbln);
                    Session["Query"] = strQuery;
                    Session["thnbln"] = thnblbln;
                    Cetak2(this);
                }
                else
                {
                    Session["periode"] = txtBulan + " " + periodeTahun;
                    //update sarmut
                    decimal aktual = 0;
                    ZetroView zl = new ZetroView();
                    zl.QueryType = Operation.CUSTOM;
                    zl.CustomQuery = "declare @thnbln varchar(6) set @thnbln='" + thnblbln + "' " +
                    "select cast( (select sum(M3AkhirSS +M3AkhirSE ) from ( " +
                    "select PartnoAkhir,sum(QtyAkhirSS)QtyAkhirSS,sum(M3AkhirSS)M3AkhirSS, sum(QtyAkhirSE)QtyAkhirSE,sum(M3AkhirSE)M3AkhirSE from ( SELECT B.tgltrans as Tanggal, I1.PartNo as PartnoAwal,  " +
                    "sum(B.QtyIn) as QtyAwal,sum(I1.Volume*B.QtyIn) M3Awal,  I2.PartNo AS PartnoAkhir, case when B.NCSS>0 then sum(B.QtyOut) else 0 end QtyAkhirSS,  " +
                    "case when B.NCSS>0 then sum(I2.Volume*B.QtyOut) else 0 end M3AkhirSS,case when B.NCSE>0 then sum(B.QtyOut) else 0 end QtyAkhirSE,  " +
                    "case when B.NCSE>0 then sum(I2.Volume*B.QtyOut) else 0 end M3AkhirSE,B.NCSS,B.NCSE  FROM T3_Serah AS A  INNER JOIN T3_Simetris AS B ON A.ID = B.SerahID  " +
                    "INNER JOIN FC_Items AS I1 ON A.ItemID = I1.ID INNER JOIN  FC_Items AS I2 ON B.ItemID = I2.ID LEFT JOIN T3_GroupM AS D ON B.GroupID = D.ID   " +
                    "where (B.NCSS>0 or B.NCSE>0) and B.rowstatus>-1 and left(convert(varchar,B.tgltrans,112),6)=@thnbln  group by B.tgltrans,I1.PartNo,I2.PartNo,B.NCSS,B.NCSE) S  group by PartnoAkhir)a)/ " +
                    "(select SUM(kubik)kubik from ( select isnull(SUM(A.qty*((I.tebal*I.panjang*I.lebar)/1000000000)),0) kubik from vw_KartuStockBJNew A  " +
                    "inner join fc_items I on A.itemid=I.ID  where LEFT(convert(char,A.tanggal,112),6)=@thnbln and A.qty>0 and A.Process not in ('adjust-IN','retur','direct') and   (I.PartNo  like '%-3-%' or I.PartNo   " +
                    "like '%-W-%' or I.PartNo  like '%-M-%') and (Keterangan like '%-P-%' or Keterangan like '%-1-%' ) union all  " +
                    "select isnull(SUM(A.qty*((I.tebal*I.panjang*I.lebar)/1000000000)),0) kubik from vw_KartuStockBJNew A inner join fc_items I on A.itemid=I.ID  where LEFT(convert(char,A.tanggal,112),6)=@thnbln and  " +
                    "A.qty>0 and A.Process in ('direct') and   (I.PartNo  like '%-3-%' or I.PartNo  like '%-W-%' or I.PartNo  like '%-M-%'))S) *100 as decimal(18,2)) aktual";
                    SqlDataReader sdr = zl.Retrieve();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            aktual = Convert.ToDecimal(sdr["aktual"].ToString());
                        }
                    }
                    string sarmutPrs = "Nonconformity Produk Akibat Proses Sortir";
                    int deptid = getDeptID("quality");
                    ZetroView zl1 = new ZetroView();
                    zl1.QueryType = Operation.CUSTOM;
                    zl1.CustomQuery =
                        "update SPD_TransPrs set actual= " + aktual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddTahun.SelectedValue +
                        " and Bulan=" + ddlBulan.SelectedValue +
                        " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "' and deptid=" + deptid + ") ";
                    SqlDataReader sdr1 = zl1.Retrieve();
                    //end update sarmut
                    strQuery = reportFacade.ViewNCSortirBln(thnblbln);
                    Session["Query"] = strQuery;
                    Session["thnbln"] = thnblbln;
                    Cetak4(this);
                }
            }
            Session["yearmonth"] = periodeTahun + padbulan;
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
        static public void Cetak(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LNCHandling', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            string myScript = "Cetak();";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        static public void Cetak2(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LNCSortir', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            string myScript = "Cetak2();";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        static public void Cetak3(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LNCHandlingB', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            string myScript = "Cetak3();";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        static public void Cetak4(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LNCSortirB', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            string myScript = "Cetak4();";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void txtNoProduksi_TextChanged(object sender, EventArgs e)
        { }
        protected void ddTahun_SelectedIndexChanged(object sender, EventArgs e)
        { }

        protected void ddlBulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            int LastDay = DateTime.DaysInMonth(int.Parse(ddTahun.SelectedValue.ToString()), int.Parse(ddlBulan.SelectedValue.ToString()));
            txtTglMulai.Text = LastDay.ToString().PadLeft(2, '0') + "-" + ddlBulan.SelectedValue.PadLeft(2, '0') + "-" + ddTahun.SelectedValue.ToString();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            //if (RB_Rekap.Checked == true)
            //{
            //    PaneLS.Visible = false; PaneLH.Visible = true; 
            //}

            string Periode = ddlBulan.SelectedItem.ToString() + " " + ddTahun.SelectedItem.ToString();

            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=PemantauanNC_Handling.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            if (RB_Rekap.Checked == true)
            {
                //string Html = "<b><h1>PT. BANGUNPERKASA ADHITAMASENTRA</h1>";
                //Html += "<center><b>LEMBAR PEMANTAUAN";
                //Html += "<center><b>NON CONFORMITY PRODUK AKIBAT PROSES HANDLING";
                //Html += "<br><b><i>Periode : " + Periode;
                //Html += "<br>";
                //string HtmlEnd = "";
                div2.RenderControl(hw);
                string Contents = sw.ToString();
                Contents = Contents.Replace("border=\"0", "border=\"1");
                Response.Write(Contents);
                Response.Flush();
                Response.End();
            }
            else
            {
                //string Html = "<center><b>LEMBAR PEMANTAUAN";
                //Html += "<br><center><b>NON CONFORMITY PRODUK AKIBAT PROSES SORTIR";
                //Html += "<br><b><i>Periode : " + Periode;
                //Html += "<br>";
                //string HtmlEnd = "";
                div3.RenderControl(hw);
                string Contents = sw.ToString();
                Contents = Contents.Replace("border=\"0", "border=\"1");
                Response.Write(Contents);
                Response.Flush();
                Response.End();
            }

        }
    }

    public class NCdomain
    {
        public int QtyAkhirSS { get; set; }
        public int QtyAkhirSE { get; set; }
        public int LembarAkhir { get; set; }
        public string No { get; set; }
        public string PartnoAkhir { get; set; }
        public decimal Qty { get; set; }
        public decimal KubikAkhir { get; set; }
        public decimal M3AkhirSS { get; set; }
        public decimal M3AkhirSE { get; set; }
    }

    public class NCfacade
    {
        public string strError = string.Empty;
        private ArrayList arrDataNC = new ArrayList();
        private NCdomain NC = new NCdomain();
        private List<SqlParameter> sqlListParam;

        public ArrayList RetrieveDataNCH(string thnbln)
        {
            arrDataNC = new ArrayList();
            string strSQL = "declare @thnbln varchar(6) " +
                    "set @thnbln='" + thnbln + "' " +
                    "select ROW_NUMBER() over (order by PartnoAkhir asc ) as No,* from ( " +
                    "SELECT I2.PartNo AS PartnoAkhir, sum(B.QtyOut) as LembarAkhir,sum(I2.Volume*B.QtyOut) KubikAkhir  FROM " +
                    "T3_Simetris AS B inner join FC_Items AS I2 ON B.ItemID = I2.ID LEFT JOIN T3_GroupM AS D ON B.GroupID = D.ID " +
                    "where B.NCH>0 and B.rowstatus>-1 and left(convert(varchar,B.tgltrans,112),6)=@thnbln " +
                    "group by I2.PartNo ) as x order by x.PartnoAkhir ";

            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);

            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrDataNC.Add(new NCdomain
                    {
                        No = sdr["No"].ToString(),
                        PartnoAkhir = sdr["PartnoAkhir"].ToString(),
                        LembarAkhir = Convert.ToInt32(sdr["LembarAkhir"]),
                        KubikAkhir = Convert.ToDecimal(sdr["KubikAkhir"])

                    });
                }
            }
            return arrDataNC;
        }

        public ArrayList RetrieveDataNCS(string thnbln)
        {
            arrDataNC = new ArrayList();
            string strSQL = "declare @thnbln varchar(6) " +
                    "set @thnbln='" + thnbln + "' " +
                     "select ROW_NUMBER() over (order by PartnoAkhir asc ) as No,* from ( " +
                    "select PartnoAkhir,sum(QtyAkhirSS)QtyAkhirSS,sum(M3AkhirSS)M3AkhirSS, " +
                    "sum(QtyAkhirSE)QtyAkhirSE,sum(M3AkhirSE)M3AkhirSE from ( " +
                    "SELECT B.tgltrans as Tanggal, I1.PartNo as PartnoAwal, sum(B.QtyIn) as QtyAwal,sum(I1.Volume*B.QtyIn) M3Awal,  " +
                    "I2.PartNo AS PartnoAkhir, case when B.NCSS>0 then sum(B.QtyOut) else 0 end QtyAkhirSS, " +
                    "case when B.NCSS>0 then sum(I2.Volume*B.QtyOut) else 0 end M3AkhirSS,case when B.NCSE>0 then sum(B.QtyOut) else 0 end QtyAkhirSE, " +
                    "case when B.NCSE>0 then sum(I2.Volume*B.QtyOut) else 0 end M3AkhirSE,B.NCSS,B.NCSE  FROM T3_Serah AS A  " +
                    "INNER JOIN T3_Simetris AS B ON A.ID = B.SerahID INNER JOIN FC_Items AS I1 ON A.ItemID = I1.ID INNER JOIN  " +
                    "FC_Items AS I2 ON B.ItemID = I2.ID LEFT JOIN T3_GroupM AS D ON B.GroupID = D.ID  " +
                    "where (B.NCSS>0 or B.NCSE>0) and B.rowstatus>-1 and left(convert(varchar,B.tgltrans,112),6)=@thnbln  " +
                    "group by B.tgltrans,I1.PartNo,I2.PartNo,B.NCSS,B.NCSE) S  group by PartnoAkhir " +
                    ") as x order by x.PartnoAkhir ";

            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);

            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrDataNC.Add(new NCdomain
                    {
                        No = sdr["No"].ToString(),
                        PartnoAkhir = sdr["PartnoAkhir"].ToString(),
                        QtyAkhirSS = Convert.ToInt32(sdr["QtyAkhirSS"]),
                        M3AkhirSS = Convert.ToDecimal(sdr["M3AkhirSS"]),
                        QtyAkhirSE = Convert.ToInt32(sdr["QtyAkhirSE"]),
                        M3AkhirSE = Convert.ToDecimal(sdr["M3AkhirSE"])


                    });
                }
            }
            return arrDataNC;
        }

        public decimal GetTotalLembarOK(string thnbln)
        {
            decimal lembar = 0;
            string strSQL = "declare @thnbln varchar(6) " +
                "set @thnbln='" + thnbln + "' " +
                "select SUM(qty) qty from ( " +
                "select isnull(SUM(qty),0) qty from vw_KartuStockBJNew A where LEFT(convert(char,tanggal,112),6)=@thnbln and " +
                "A.qty>0 and A.Process  in ('direct') and ItemID  in (select ID from FC_Items where PartNo like '%-3-%' or partno like '%-w-%' or partno like '%-m-%' ) " +
                "union all " +
                "select isnull(SUM(qty),0) qty from vw_KartuStockBJNew A where LEFT(convert(char,tanggal,112),6)=@thnbln and " +
                "A.qty>0 and A.Process  not in ('adjust-IN','retur') and ItemID  in (select ID from FC_Items where PartNo like '%-3-%' or partno like '%-w-%' or partno like '%-m-%' ) " +
                "and  (Keterangan like '%-P-%' or Keterangan like '%-1-%' ) and Process like '%simetris%')S";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrDataNC = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    lembar = Convert.ToDecimal(sqlDataReader["qty"]);
                }
            }
            return lembar;
        }

        public decimal GetTotalKubikOK(string thnbln)
        {
            decimal Kubik = 0;
            string strSQL = "declare @thnbln varchar(6) " +
                "set @thnbln='" + thnbln + "' " +
                "select cast(SUM(kubik) as decimal(10,2))kubik from ( " +
                "select isnull(SUM(A.qty*((I.tebal*I.panjang*I.lebar)/1000000000)),0) kubik from vw_KartuStockBJNew A inner join fc_items I on A.itemid=I.ID  " +
                "where LEFT(convert(char,A.tanggal,112),6)=@thnbln and A.qty>0 and A.Process not in ('adjust-IN','retur','direct') and   " +
                "(I.PartNo  like '%-3-%' or I.PartNo  like '%-W-%' or I.PartNo  like '%-M-%') and (Keterangan like '%-P-%' or Keterangan like '%-1-%' )" +
                "union all " +
                "select isnull(SUM(A.qty*((I.tebal*I.panjang*I.lebar)/1000000000)),0) kubik from vw_KartuStockBJNew A inner join fc_items I on A.itemid=I.ID  " +
                "where LEFT(convert(char,A.tanggal,112),6)=@thnbln and A.qty>0 and A.Process in ('direct') and   " +
                "(I.PartNo  like '%-3-%' or I.PartNo  like '%-W-%' or I.PartNo  like '%-M-%'))S";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrDataNC = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    Kubik = Convert.ToDecimal(sqlDataReader["kubik"]);
                }
            }
            return Kubik;
        }
    }
}