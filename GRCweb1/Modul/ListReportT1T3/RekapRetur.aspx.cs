using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using Factory;
using BusinessFacade;
using Domain;

namespace GRCweb1.Modul.ListReportT1T3
{
    public partial class RekapRetur : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Global.link = "~/Default.aspx";
            if (!Page.IsPostBack)
            {
                Session["SortBy"] = null;
                txtTgl1.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                txtTgl2.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                LoadType();
            }
        }
        private string plant1 = string.Empty;
        private string plant2 = string.Empty;
        private string plant3 = string.Empty;
        private void LoadType()
        {
            Users users = (Users)Session["Users"];
            Company company = new Company();
            CompanyFacade companyf = new CompanyFacade();
            company = companyf.RetrieveById(users.UnitKerjaID);
            if (users.UnitKerjaID == 7)
            {
                plant1 = "Karawang";
                plant2 = "Citeureup";
                plant3 = "Jombang";
            }
            if (users.UnitKerjaID == 1)
            {
                plant1 = "Citeureup";
                plant2 = "Karawang";
                plant3 = "Jombang";
            }
            if (users.UnitKerjaID == 13)
            {
                plant1 = "Jombang";
                plant2 = "Citeureup";
                plant3 = "Karawang";
            }
        }
        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];
            Company company = new Company();
            CompanyFacade companyf = new CompanyFacade();
            company = companyf.RetrieveById(users.UnitKerjaID);
            if (users.UnitKerjaID == 7)
            {
                plant1 = "Karawang";
                plant2 = "Citeureup";
                plant3 = "Jombang";
            }
            if (users.UnitKerjaID == 1)
            {
                plant1 = "Citeureup";
                plant2 = "Karawang";
                plant3 = "Jombang";
            }
            if (users.UnitKerjaID == 13)
            {
                plant1 = "Jombang";
                plant2 = "Citeureup";
                plant3 = "Karawang";
            }
            string strValidate = ValidateText();
            string strQuery = string.Empty;
            string allQuery = string.Empty;
            string drTgl = string.Empty;
            string sdTgl = string.Empty;
            string PdrTgl = string.Empty;
            string PsdTgl = string.Empty;
            Session["SortBy"] = "Order by tgltrans";
            drTgl = DateTime.Parse(txtTgl1.Text).ToString("yyyyMMdd");
            sdTgl = DateTime.Parse(txtTgl2.Text).ToString("yyyyMMdd");
            PdrTgl = DateTime.Parse(txtTgl1.Text).ToString("dd-MM-yyyy");
            PsdTgl = DateTime.Parse(txtTgl2.Text).ToString("dd-MM-yyyy");
            Session["periode"] = PdrTgl + " s/d " + PsdTgl;
            Session["plant"] = plant1.ToUpper();
            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }
            decimal totkirim = 0;
            decimal totkiriml = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select sum(qty)* -1 as qtyl,sum(qty * ((tebal*lebar*Panjang)/1000000000))*-1 as qty from vw_KartuStockBJNew " +
                "where tanggal >='" + drTgl + "' and tanggal <='" + sdTgl + "' and Process='kirim'and (partno like '%-3-%' or partno like '%-m-%')";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    totkirim = Convert.ToDecimal(sdr["qty"].ToString());
                    totkiriml = Convert.ToDecimal(sdr["qtyl"].ToString());
                }
            }
            Session["totalkirim"] = totkirim;
            Session["totalkiriml"] = totkiriml;
            decimal aktual = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "select cast(sum((tot1*volume) -(tot2*volume)+(tot3*volume)+(tot4*volume)) /(select sum(qty * ((tebal*lebar*Panjang)/1000000000))*-1  " +
                "from vw_KartuStockBJNew where tanggal >='" + drTgl + "' and tanggal <='" + sdTgl + "' and Process='kirim'and (partno like '%-3-%' or partno like '%-m-%'))  * 100 as decimal(18,2)) aktual from ( " +
                "select tanggal,PartNo,volume ,sum(isnull(QtyC1,0))QtyC1,sum(isnull(QtyP1,0)) QtyP1,sum(isnull(QtyS1,0)) QtyS1,sum(isnull(Tot1,0)) Tot1,sum(isnull(TotK1,0)) TotK1, " +
                "sum(isnull(QtyC2,0)) QtyC2,sum(isnull(QtyP2,0)) QtyP2,sum(isnull(QtyS2,0)) QtyS2,sum(isnull(Tot2,0)) Tot2, " +
                "sum(isnull(TotK2,0)) TotK2,sum(isnull(QtyC3,0)) QtyC3,sum(isnull(QtyP3,0)) QtyP3,sum(isnull(QtyS3,0)) QtyS3,sum(isnull(Tot3,0)) Tot3,sum(isnull(TotK3,0)) TotK3, " +
                "sum(isnull(QtyC4,0)) QtyC4,sum(isnull(QtyP4,0)) QtyP4,sum(isnull(QtyS4,0)) QtyS4,sum(isnull(Tot4,0)) Tot4,sum(isnull(TotK4,0)) TotK4 from ( " +
                "select tanggal ,PartNo,volume,case when typeR=1 and ((KW='3' or KW='W')or lokasi='cl') then Qty end QtyC1," +
                "case when typeR=1 and (KW='P'and lokasi <> 'cl') then Qty end QtyP1, " +
                "case when typeR=1 and (KW='S'and lokasi <> 'cl') then Qty end QtyS1, " +
                "case when typeR=1 and ((KW='P' or KW='S') and lokasi <> 'cl') then Qty end Tot1, " +
                "case when typeR=1 and (KW='P' or KW='S') then QtyK end TotK1, " +
                "case when typeR=2 and ((KW='3' or KW='W')or lokasi='cl') then Qty end QtyC2, " +
                "case when typeR=2 and (KW='P' and lokasi <> 'cl') then Qty end QtyP2, " +
                "case when typeR=2 and (KW='S' and lokasi <> 'cl') then Qty end QtyS2, " +
                "case when typeR=2 and ((KW='P' or KW='S') and lokasi <> 'cl') then Qty end Tot2, " +
                "case when typeR=2 and ((KW='P' or KW='S') and lokasi <> 'cl') then QtyK end TotK2, " +
                "case when typeR=3 and ((KW='3' or KW='W')or lokasi='cl') then Qty end QtyC3, " +
                "case when typeR=3 and (KW='P' and lokasi <> 'cl') then Qty end QtyP3, " +
                "case when typeR=3 and (KW='S' and lokasi <> 'cl') then Qty end QtyS3, " +
                "case when typeR=3 and ((KW='P' or KW='S') and lokasi <> 'cl') then Qty   end Tot3, " +
                "case when typeR=3 and ((KW='P' or KW='S') and lokasi <> 'cl') then QtyK end TotK3, " +
                "case when typeR=4 and ((KW='3' or KW='W')or lokasi='cl') then Qty end QtyC4,  " +
                "case when typeR=4 and (KW='P' and lokasi <> 'cl') then Qty end QtyP4,  " +
                "case when typeR=4 and (KW='S' and lokasi <> 'cl') then Qty end QtyS4,  " +
                "case when typeR=4 and ((KW='P' or KW='S') and lokasi <> 'cl') then Qty   end Tot4,  " +
                "case when typeR=4 and ((KW='P' or KW='S') and lokasi <> 'cl') then QtyK end TotK4  " +
                "from ( " +
                "    select R.TglTrans tanggal,I.PartNo,I.volume,SUBSTRING(I.partno,5,1) as KW,sum(R.Qty)Qty, " +
                "    sum(((I.Panjang*I.Lebar)/(1220*2440) ) * R.Qty) QtyK,isnull(R.TypeR,1)  TypeR,I.ID,L.Lokasi " +
                "    from T3_Retur R inner join FC_Items I on R.ItemID =I.ID  inner join FC_lokasi L on R.LokID=L.ID  where R.RowStatus>-1 " +
                "    group by R.TglTrans,I.PartNo,I.volume,R.TypeR,I.ID,L.Lokasi " +
                "union all " +
                "select R.TglTrans tanggal,I.PartNo,I.volume,SUBSTRING(I.partno,5,1) as KW,sum(R.Qty)Qty," +
                "    sum(((I.Panjang*I.Lebar)/(1220*2440) ) * R.Qty) QtyK,3  TypeR,I.ID,L.Lokasi " +
                "    from T3_ReturO R inner join FC_Items I on R.ItemID =I.ID  inner join FC_lokasi L on R.LokID=L.ID  where R.RowStatus>-1 " +
                "    group by R.TglTrans,I.PartNo,I.volume,I.ID,L.Lokasi " +
                ") as A ) as B where convert(char,tanggal,112)>='" + drTgl + "' and convert(char,tanggal,112)<='" + sdTgl + "'group by tanggal,PartNo,volume)a";
            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    aktual = Convert.ToDecimal(sdr["aktual"].ToString());
                }
            }
            string sarmutPrs = "Return Produk";
            int deptid = getDeptID("QUALITY ASSURANCE");
            ZetroView zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery =
                "update SPD_TransPrs set actual= " + aktual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + drTgl.Substring(0, 4) +
                " and Bulan=" + drTgl.Substring(4, 2) +
                " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "') ";
            SqlDataReader sdr1 = zl1.Retrieve();

            #region Yoga SarmutToPES
            ArrayList arrData = new ArrayList();
            ZetroView zs = new ZetroView();
            zs.QueryType = Operation.CUSTOM;
            decimal targetSarmutQA = 0;
            int bulan = Convert.ToInt32(Convert.ToDateTime(txtTgl1.Text).Month) - 1;
            int tahun = Convert.ToInt32(Convert.ToDateTime(txtTgl1.Text).Year);
            int bulan2 = Convert.ToInt32(Convert.ToDateTime(txtTgl1.Text).Month);
            zs.CustomQuery = "SELECT TOP 1 * FROM SPD_TransPrs WHERE SarmutPID IN (SELECT ID from SPD_Perusahaan where Rowstatus>-1 and " +
                             "SarMutPerusahaan='" + sarmutPrs + "') AND Approval>1 ORDER BY ID DESC ";
            SqlDataReader adr = zs.Retrieve();
            if (adr.HasRows)
            {
                while (adr.Read())
                {
                    targetSarmutQA = (adr["Target"] == DBNull.Value) ? 0 : Convert.ToDecimal(adr["Target"]);
                }
            }
            if (Convert.ToDecimal(aktual) != 0)
            {
                string strError = "";
                //decimal newActual = (grandTotalA / grandTotal) * 100;
                string ketPes = (aktual < targetSarmutQA) ? "<0.25%" : (Convert.ToDouble(aktual) >= 0.25 && Convert.ToDouble(aktual) <= 0.43) ? "0.25 - 0.43%" : (Convert.ToDouble(aktual) >= 0.44 && Convert.ToDouble(aktual) <= 0.60) ? "0.44 - 0.60%" : (Convert.ToDouble(aktual) >= 0.61 && Convert.ToDouble(aktual) <= 0.77) ? "0.61 - 0.77%" : ">0.77%";
                //string valueActual = ((newActual / targetSarmutQA) * 100).ToString();
                //double percentActual = Convert.ToDouble(valueActual);
                ZetroView zv = new ZetroView();
                zv.QueryType = Operation.CUSTOM;
                int IdKPI = 0;
                string kpiName = string.Empty;
                zv.CustomQuery = "SELECT * FROM ISO_KPI WHERE DeptID=9 and month(TglMulai)=" + Convert.ToDateTime(txtTgl2.Text).Month + " AND year(TglMulai)=" + tahun + " ORDER BY ID desc";
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
                    int LastDay = DateTime.DaysInMonth(Convert.ToDateTime(txtTgl1.Text).Year, Convert.ToDateTime(txtTgl1.Text).Month);
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
                        //uc.TglMulai = Convert.ToDateTime(txtTgl2.Text);
                        int pjgDept = ((Users)Session["Users"]).DeptID;
                        string ddlDept = "QUALITY ASSURANCE";
                        uc.DeptName = (pjgDept >= 4) ? ddlDept.Substring(0, 3) : ddlDept.Substring(0, pjgDept);
                        txtTgl2.Text = Convert.ToDateTime(uc.TglMulai).ToString();
                        uc.TglMulai = Convert.ToDateTime(LastDay.ToString().PadLeft(2, '0') + "-" + Convert.ToDateTime(txtTgl1.Text).Month + "-" + tahun);

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

                        arrSarPesUPdate2 = updatesarPesFacade.RetrieveID(deptID, bulan2.ToString(), tahun.ToString(), categoryID);
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

            ReportFacadeT1T3 reportFacade = new ReportFacadeT1T3();
            allQuery = "select tanggal,PartNo,volume ,sum(isnull(QtyC1,0))QtyC1,sum(isnull(QtyP1,0)) QtyP1,sum(isnull(QtyS1,0)) QtyS1,sum(isnull(Tot1,0)) Tot1,sum(isnull(TotK1,0)) TotK1, " +
                    "sum(isnull(QtyC2,0)) QtyC2,sum(isnull(QtyP2,0)) QtyP2,sum(isnull(QtyS2,0)) QtyS2,sum(isnull(Tot2,0)) Tot2, " +
                    "sum(isnull(TotK2,0)) TotK2,sum(isnull(QtyC3,0)) QtyC3,sum(isnull(QtyP3,0)) QtyP3,sum(isnull(QtyS3,0)) QtyS3,sum(isnull(Tot3,0)) Tot3,sum(isnull(TotK3,0)) TotK3, " +
                    "sum(isnull(QtyC4,0)) QtyC4,sum(isnull(QtyP4,0)) QtyP4,sum(isnull(QtyS4,0)) QtyS4,sum(isnull(Tot4,0)) Tot4,sum(isnull(TotK4,0)) TotK4 from ( " +
                    "select tanggal ,PartNo,volume,case when typeR=1 and ((KW='3' or KW='W')or lokasi='cl') then Qty end QtyC1," +
                    "case when typeR=1 and (KW='P'and lokasi <> 'cl') then Qty end QtyP1, " +
                    "case when typeR=1 and (KW='S'and lokasi <> 'cl') then Qty end QtyS1, " +
                    "case when typeR=1 and ((KW='P' or KW='S') and lokasi <> 'cl') then Qty end Tot1, " +
                    "case when typeR=1 and (KW='P' or KW='S') then QtyK end TotK1, " +
                    "case when typeR=2 and ((KW='3' or KW='W')or lokasi='cl') then Qty end QtyC2, " +
                    "case when typeR=2 and (KW='P' and lokasi <> 'cl') then Qty end QtyP2, " +
                    "case when typeR=2 and (KW='S' and lokasi <> 'cl') then Qty end QtyS2, " +
                    "case when typeR=2 and ((KW='P' or KW='S') and lokasi <> 'cl') then Qty end Tot2, " +
                    "case when typeR=2 and ((KW='P' or KW='S') and lokasi <> 'cl') then QtyK end TotK2, " +

                    "case when typeR=3 and ((KW='3' or KW='W')or lokasi='cl') then Qty end QtyC3, " +
                    "case when typeR=3 and (KW='P' and lokasi <> 'cl') then Qty end QtyP3, " +
                    "case when typeR=3 and (KW='S' and lokasi <> 'cl') then Qty end QtyS3, " +
                    "case when typeR=3 and ((KW='P' or KW='S') and lokasi <> 'cl') then Qty   end Tot3, " +
                    "case when typeR=3 and ((KW='P' or KW='S') and lokasi <> 'cl') then QtyK end TotK3, " +

                    "case when typeR=5 and ((KW='3' or KW='W')or lokasi='cl') then Qty end QtyC4,  " +
                    "case when typeR=5 and (KW='P' and lokasi <> 'cl') then Qty end QtyP4,  " +
                    "case when typeR=5 and (KW='S' and lokasi <> 'cl') then Qty end QtyS4,  " +
                    "case when typeR=5 and ((KW='P' or KW='S') and lokasi <> 'cl') then Qty   end Tot4,  " +
                    "case when typeR=5 and ((KW='P' or KW='S') and lokasi <> 'cl') then QtyK end TotK4  " +
                    "from ( " +
                    "    select R.TglTrans tanggal,I.PartNo,I.volume,SUBSTRING(I.partno,5,1) as KW,sum(R.Qty)Qty, " +
                    "    sum(((I.Panjang*I.Lebar)/(1220*2440) ) * R.Qty) QtyK,isnull(R.TypeR,1)  TypeR,I.ID,L.Lokasi " +
                    "    from T3_Retur R inner join FC_Items I on R.ItemID =I.ID  inner join FC_lokasi L on R.LokID=L.ID  where R.RowStatus>-1 " +
                    "    group by R.TglTrans,I.PartNo,I.volume,R.TypeR,I.ID,L.Lokasi " +
                    "union all " +
                    "select R.TglTrans tanggal,I.PartNo,I.volume,SUBSTRING(I.partno,5,1) as KW,sum(R.Qty)Qty," +
                    "    sum(((I.Panjang*I.Lebar)/(1220*2440) ) * R.Qty) QtyK,3  TypeR,I.ID,L.Lokasi " +
                    "    from T3_ReturO R inner join FC_Items I on R.ItemID =I.ID  inner join FC_lokasi L on R.LokID=L.ID  where R.RowStatus>-1 " +
                    "    group by R.TglTrans,I.PartNo,I.volume,I.ID,L.Lokasi " +
                    ") as A ) as B where convert(char,tanggal,112)>='" + drTgl + "' and convert(char,tanggal,112)<='" + sdTgl +
                    "'group by tanggal,PartNo,volume order by PartNo";
            strQuery = allQuery;
            Session["Query"] = strQuery;
            Cetak(this);
        }

        private string SimpanKPI(SarmutPes sop)
        {
            string strEvent = "Insert";
            int tahun = Convert.ToInt32(Convert.ToDateTime(txtTgl1.Text).Year);
            ISO_DocumentNoFacade docNoFacade = new ISO_DocumentNoFacade();
            ISO_DocumentNo docNo = docNoFacade.RetrieveByDept(1, 9, tahun);
            if (docNo.ID > 0)
            {
                docNo.DocNo = docNo.DocNo + 1;
                //docNo.PesType = int.Parse(txtPES.Text); //sop
                docNo.DeptID = 9;
                docNo.Tahun = Convert.ToDateTime(txtTgl1.Text).Year;
                Company cp = new CompanyFacade().RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                docNo.Plant = cp.KodeLokasi;
            }
            else
            {
                docNo.DocNo = 1;
                //docNo.PesType = int.Parse(txtPES.Text); //sop
                docNo.DeptID = 9;
                docNo.Tahun = Convert.ToDateTime(txtTgl1.Text).Year;
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
            int tahun = Convert.ToInt32(Convert.ToDateTime(txtTgl1.Text).Year);
            ISO_DocumentNoFacade docNoFacade = new ISO_DocumentNoFacade();
            ISO_DocumentNo docNo = docNoFacade.RetrieveByDept(1, 9, tahun);
            if (docNo.ID > 0)
            {
                docNo.DocNo = docNo.DocNo + 1;
                //docNo.PesType = int.Parse(txtPES.Text); //sop
                docNo.DeptID = 9;
                docNo.Tahun = Convert.ToDateTime(txtTgl1.Text).Year;
                Company cp = new CompanyFacade().RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                docNo.Plant = cp.KodeLokasi;
            }
            else
            {
                docNo.DocNo = 1;
                //docNo.PesType = int.Parse(txtPES.Text); //sop
                docNo.DeptID = 9;
                docNo.Tahun = Convert.ToDateTime(txtTgl1.Text).Year;
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
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            Session["SortBy"] = "Order by Customer";
            LoadDataList();
        }
        protected void SortBy_tgltrans(object sender, EventArgs e)
        {
            Session["SortBy"] = "Order by tgltrans";
            LoadDataList();
        }
        protected void SortBy_Customer(object sender, EventArgs e)
        {
            Session["SortBy"] = "Order by Customer";
            LoadDataList();
        }
        private void LoadDataList()
        {
            Users users = (Users)Session["Users"];
            Company company = new Company();
            CompanyFacade companyf = new CompanyFacade();
            company = companyf.RetrieveById(users.UnitKerjaID);
            //if (company.Lokasi.ToUpper().Trim() == "KARAWANG")
            //{
            //    plant1 = "Karawang";
            //    plant2 = "Citeureup";
            //}
            //else
            //{
            //    plant2 = "Karawang";
            //    plant1 = "Citeureup";
            //}

            if (users.UnitKerjaID == 7)
            {
                plant1 = "Karawang";
                plant2 = "Citeureup";
                plant3 = "Jombang";
            }
            if (users.UnitKerjaID == 1)
            {
                plant1 = "Citeureup";
                plant2 = "Karawang";
                plant3 = "Jombang";
            }
            if (users.UnitKerjaID == 13)
            {
                plant1 = "Jombang";
                plant2 = "Citeureup";
                plant3 = "Karawang";
            }
            ArrayList arrReturn = new ArrayList();
            ArrayList arrTotal = new ArrayList();
            string drTgl = DateTime.Parse(txtTgl1.Text).ToString("yyyyMMdd");
            string sdTgl = DateTime.Parse(txtTgl2.Text).ToString("yyyyMMdd");
            arrReturn = new ReportFacadeT1T3().ViewRekapReturPrev(drTgl, sdTgl, plant1);
            lstRetur.DataSource = arrReturn;
            lstRetur.DataBind();
            decimal tt = 0;
            foreach (DomainReturnBJ rbj in arrReturn)
            {
                tt = rbj.Total;
            }
            arrTotal.Add(new DomainReturnBJ { Total = tt });
            lstTotal.DataSource = arrTotal;
            lstTotal.DataBind();
            Session["ShortBy"] = null;
        }
        static public void Cetak(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../Reportt1t3/Report.aspx?IdReport=PemantauanRetur', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
            string myScript = "Cetak();";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        static public void Cetak1(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../Reportt1t3/Report.aspx?IdReport=RekapRetur', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
            string myScript = "Cetak1();";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        private string ValidateText()
        {
            if (txtTgl1.Text == string.Empty)
                return "Dari Tanggal tidak boleh kosong";
            else if (txtTgl2.Text == string.Empty)
                return "s/d Tanggal tidak boleh kosong";
            return string.Empty;
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void btnPrint0_Click(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];
            Company company = new Company();
            CompanyFacade companyf = new CompanyFacade();
            company = companyf.RetrieveById(users.UnitKerjaID);
            if (users.UnitKerjaID == 7)
            {
                plant1 = "Karawang";
                plant2 = "Citeureup";
                plant3 = "Jombang";
            }
            if (users.UnitKerjaID == 1)
            {
                plant1 = "Citeureup";
                plant2 = "Karawang";
                plant3 = "Jombang";
            }
            if (users.UnitKerjaID == 13)
            {
                plant1 = "Jombang";
                plant2 = "Citeureup";
                plant3 = "Karawang";
            }

            string strValidate = ValidateText();
            string strQuery = string.Empty;
            string allQuery = string.Empty;
            string drTgl = string.Empty;
            string sdTgl = string.Empty;
            string PdrTgl = string.Empty;
            string PsdTgl = string.Empty;
            Session["SortBy"] = "Order by tgltrans";
            drTgl = DateTime.Parse(txtTgl1.Text).ToString("yyyyMMdd");
            sdTgl = DateTime.Parse(txtTgl2.Text).ToString("yyyyMMdd");
            PdrTgl = DateTime.Parse(txtTgl1.Text).ToString("dd-MM-yyyy");
            PsdTgl = DateTime.Parse(txtTgl2.Text).ToString("dd-MM-yyyy");
            Session["drTgl"] = PdrTgl;
            Session["sdTgl"] = PsdTgl;

            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }
            ReportFacadeT1T3 reportFacade = new ReportFacadeT1T3();
            allQuery = reportFacade.ViewRekapRetur(drTgl, sdTgl, plant1);
            strQuery = allQuery;
            Session["Query"] = strQuery;
            Cetak1(this);
        }
    }
}