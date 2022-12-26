using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.DataVisualization.Charting;
using Domain;
using BusinessFacade;
using DataAccessLayer;

namespace GRCweb1.Modul.Sarmut
{
    public partial class RekapLoadingTime : System.Web.UI.Page
    {
        public decimal TotalMobil = 0;
        public decimal AvgPerHari = 0;
        public decimal TolBulan = 0;
        public decimal TolHari = 0;
        public decimal CapaiBulan = 0;
        public decimal CapaiHari = 0;
        public string TargetSOP = "95%";
        public int BPAS = 0;
        public int Luar = 0;
        public decimal Pencapaiane = 0;
        public int TjmlMobil = 0;
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
            //ddlBulan.Items.Add(new ListItem("--Pilih Bulan--", "0"));
            for (int i = 1; i <= 12; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i).ToString(), i.ToString().PadLeft(2, '0')));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString().PadLeft(2, '0');
        }
        private void LoadTahun()
        {
            ddlTahun.Items.Clear();
            //ddlTahun.Items.Add(new ListItem("--Pilih--", "0"));
            LapLoadingTime2 lp = new LapLoadingTime2();
            lp.Pilihan = "Tahun";
            foreach (LoadingTime ld in lp.Retrieve())
            {
                ddlTahun.Items.Add(new ListItem(ld.RowStatus.ToString(), ld.RowStatus.ToString()));
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            LoadGrafik();
        }
        private void LoadGrafik()
        {
            try
            {
                LapLoadingTime2 lp = new LapLoadingTime2();
                ArrayList arrData = new ArrayList();
                string start = ddlTahun.SelectedValue.ToString() + ddlBulan.SelectedValue.ToString() + "01";
                string endate = ddlTahun.SelectedValue.ToString() + ddlBulan.SelectedValue.ToString() +
                              DateTime.DaysInMonth(int.Parse(ddlTahun.SelectedValue), int.Parse(ddlBulan.SelectedValue.ToString()));
                lp.Pilihan = "Rekap";
                lp.DariTgl = start;// DateTime.Parse(start).ToString("yyyyMMdd");
                lp.SampaiTgl = endate;// DateTime.Parse(endate).ToString("yyyyMMdd");
                lp.StartTime = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("StartTime", "LoadingTime").ToString();
                lp.ArmadaID = "0";
                //arrData = lp.Retrieve();

                if (Convert.ToInt32(ddlTahun.SelectedValue.ToString() + ddlBulan.SelectedValue.ToString()) < 202109)
                {
                    arrData = lp.Retrieve2();
                }
                else
                {
                    arrData = lp.Retrieve();
                }

                string[] x = new string[arrData.Count];
                int[] z = new int[arrData.Count];
                int[] y = new int[arrData.Count];
                int[] xy = new int[arrData.Count];
                int i = 0;
                foreach (LoadingTime lo in arrData)
                {

                    y[i] = lo.MobilSendiri;
                    x[i] = lo.ID.ToString();
                    z[i] = lo.EkspedisiID;
                    xy[i] = lo.JmlLewat;
                    i = i + 1;
                    TotalMobil += lo.JmlMobil;
                    CapaiBulan += lo.JmlLewat;
                    BPAS += lo.MobilSendiri;
                    Luar += lo.EkspedisiID;
                }
                AvgPerHari = TotalMobil / 25;
                TolBulan = TotalMobil * 5 / 100;
                TolHari = TolBulan / 25;
                CapaiHari = CapaiBulan / 25;
                Pencapaiane = (TotalMobil - CapaiBulan) / TotalMobil * 100;
                Chart1.Legends.Add("BPAS");
                Chart1.Series.Add("BPAS");
                Chart1.Series[0].IsVisibleInLegend = true;
                Chart1.Series[0].IsValueShownAsLabel = true;
                Chart1.Series[0].Points.DataBindXY(x, y);
                Chart1.Series[0].ChartType = SeriesChartType.Column;
                Chart1.Legends[0].Enabled = true;
                Chart1.Legends[0].Docking = Docking.Bottom;
                Chart1.Legends.Add("Armada Luar");
                Chart1.Series.Add("Armada Luar");
                Chart1.Series[1].IsValueShownAsLabel = true;
                Chart1.Series[1].Points.DataBindXY(x, z);
                Chart1.Series[1].ChartType = SeriesChartType.Column;
                Chart1.Legends[1].Enabled = true;
                Chart1.Legends[1].Docking = Docking.Bottom;
                Chart1.Legends.Add("Total");
                Chart1.Series.Add("Total");
                Chart1.Series[2].Points.DataBindXY(x, xy);
                Chart1.Series[2].ChartType = SeriesChartType.Line;
                Chart1.Legends[2].Enabled = true;
                Chart1.Legends[2].Docking = Docking.Bottom;
                Chart1.ChartAreas["Area1"].Area3DStyle.Enable3D = false;
                Chart1.ChartAreas["Area1"].AxisY.MajorGrid.LineWidth = 1;
                Chart1.ChartAreas["Area1"].AxisX.MajorGrid.LineWidth = 0;
                Chart1.ChartAreas["Area1"].AxisX.Interval = 1;
                Chart1.ChartAreas["Area1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 9.25F, System.Drawing.FontStyle.Regular);
                Chart1.ChartAreas["Area1"].AxisX.Title = "Tanggal";
                Chart1.Titles.Add(
                    new Title(
                        "Mobil Lewat Batas Loading " + ddlBulan.SelectedItem.Text.ToString() + " " + ddlTahun.SelectedValue.ToString(),
                        Docking.Top,
                        new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black
                        )
                        );
                lstLoading.DataSource = arrData;
                lstLoading.DataBind();
                string actual = Pencapaiane.ToString("0.##").Replace(",", ".");
                string sarmutPrs = "Kecepatan Waktu Loading";
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery =
                    "update SPD_TransPrs set actual= " + actual.Replace(",", ".") + " where approval=0 and Tahun =" + ddlTahun.SelectedValue +
                    " and Bulan=" + ddlBulan.SelectedValue +
                    " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "') ";
                SqlDataReader sdr1 = zl1.Retrieve();

                #region Yoga SarmutToPES
                ZetroView zs = new ZetroView();
                zs.QueryType = Operation.CUSTOM;
                decimal targetSarmutLogPJ = 0;
                int bulan = Convert.ToInt32(ddlBulan.SelectedValue) - 1;
                zs.CustomQuery = "SELECT top 1 * FROM SPD_TransPrs WHERE SarmutPID IN (SELECT ID from SPD_Perusahaan where Rowstatus>-1 and " +
                                 "SarMutPerusahaan='" + sarmutPrs + "') AND Approval>1 order by id desc ";
                SqlDataReader adr = zs.Retrieve();
                if (adr.HasRows)
                {
                    while (adr.Read())
                    {
                        targetSarmutLogPJ = (adr["Target"] == DBNull.Value) ? 0 : Convert.ToDecimal(adr["Target"]);
                    }
                }
                if (Convert.ToDecimal(actual) != 0)
                {
                    string strError = "";
                    int valueActual = 0;

                    string ketPes = (Convert.ToDecimal(actual) > targetSarmutLogPJ) ? ">95%" : (Convert.ToDecimal(actual) >= 94 && Convert.ToDecimal(actual) <= 95) ? "94-95%" : (Convert.ToDecimal(actual) >= 92 && Convert.ToDecimal(actual) <= 93) ? "92-93%" : (Convert.ToDecimal(actual) >= 90 && Convert.ToDecimal(actual) <= 91) ? "90-91%" : "<90%";

                    double percentActual = Convert.ToDouble(valueActual);
                    ZetroView zv = new ZetroView();
                    zv.QueryType = Operation.CUSTOM;
                    int IdKPI = 0;
                    string kpiName = string.Empty;
                    zv.CustomQuery = "SELECT * FROM ISO_KPI WHERE CategoryID in (select ID from ISO_UserCategory where Sarmut='" + sarmutPrs + "') and DeptID=6 and month(TglMulai)=" + ddlBulan.SelectedValue + " AND year(TglMulai)=" + ddlTahun.SelectedValue + " ORDER BY ID desc";
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
                            uc.Ket = actual + "%";
                            //uc.Percent = valueActual.ToString();
                            //uc.TglMulai = Convert.ToDateTime(txtTglMulai.Text);
                            int pjgDept = ((Users)Session["Users"]).DeptID;
                            string DdlDept = "HRD & GA";
                            uc.DeptName = (pjgDept >= 4) ? DdlDept.Substring(0, 3) : DdlDept.Substring(0, pjgDept);
                            txtTglMulai.Text = Convert.ToDateTime(uc.TglMulai).ToString();
                            uc.TglMulai = Convert.ToDateTime(LastDay.ToString().PadLeft(2, '0') + "-" + ddlBulan.SelectedValue.PadLeft(2, '0') + "-" + ddlTahun.SelectedValue.ToString());

                            ZetroView zx = new ZetroView();
                            zx.QueryType = Operation.CUSTOM;
                            int idSopScore = 0;
                            string targetKe = string.Empty;
                            decimal pointNilai = 0;
                            string ketActual = string.Empty;
                            zx.CustomQuery = "select ID,CategoryID,TargetKe,PointNilai from ISO_SOPScore where CategoryID=" + categoryID + " " +
                                             "and RowStatus>-1 and TargetKe='" + ketPes + "' " +
                                             "and CategoryID in (select CategoryID from ISO_UserCategory where Sarmut like '%" + sarmutPrs + "%') ";
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
                                        ketActual = actual + "%";
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
                                up.Ket = actual + "%";
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
            catch { }
            #endregion
        }
        private string SimpanKPI(SarmutPes sop)
        {
            string strEvent = "Insert";
            ISO_DocumentNoFacade docNoFacade = new ISO_DocumentNoFacade();
            ISO_DocumentNo docNo = docNoFacade.RetrieveByDept(1, 6, Convert.ToDateTime(txtTglMulai.Text).Year);
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
            ISO_DocumentNo docNo = docNoFacade.RetrieveByDept(1, 6, Convert.ToInt32(ddlTahun.SelectedValue));
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
        protected void lstLoading_Databind(object sender, RepeaterItemEventArgs e)
        {
            LoadingTime ld = (LoadingTime)e.Item.DataItem;
            Label ket = (Label)e.Item.FindControl("ket");
            TjmlMobil += ld.JmlMobil;
            if (ket != null)
            {
                ket.Text = (ld.Targete <= ld.Pencapaian) ? "OK" : "Not OK";
            }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            btnPreview_Click(null, null);
            Chart ct = (Chart)lst.FindControl("Chart1");
            if (ct != null) { ct.Visible = false; }
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=RekapLoadingTime.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "";
            string HtmlEnd = "</html>";
            FileInfo fi = new FileInfo(Server.MapPath("~/Scripts/text.css"));
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            StreamReader sr = fi.OpenText();
            while (sr.Peek() >= 0)
            {
                sb.Append(sr.ReadLine());

            }
            string imgPath2 = HttpContext.Current.Request.PhysicalApplicationPath + "\\modul\\sarmut\\Grafik.jpg";

            Chart1.SaveImage(imgPath2);
            string imgPath = Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/modul/sarmut/Grafik.jpg");
            sr.Close();
            Html += "<html><head><style type='text/css'>" + sb.ToString() + "</style></head>";
            Html += "<b>DATA MOBIL YNG MELEBIHI WAKTU LOADING</b>";
            Html += "<br>Periode : " + ddlBulan.SelectedItem.Text + " " + ddlTahun.SelectedValue;
            //HtmlEnd = "<br><img src='" + imgPath + "'></html>";
            lst.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0", "border=\"0");
            Contents = Contents.Replace(" angka", "");
            Contents = Contents.Replace(" tengah", "");
            Contents = Contents.Replace("id=\"xx\">", "class=\"textmode\" style=\"height:400px\"><img src='" + imgPath + "'/>");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
            File.Delete(imgPath2);
        }

        protected void ddlBulan_Change(object sender, EventArgs e)
        {
            int LastDay = DateTime.DaysInMonth(int.Parse(ddlTahun.SelectedValue.ToString()), int.Parse(ddlBulan.SelectedValue.ToString()));
            txtTglMulai.Text = LastDay.ToString().PadLeft(2, '0') + "-" + ddlBulan.SelectedValue.PadLeft(2, '0') + "-" + ddlTahun.SelectedValue.ToString();
        }
        protected void ddlTahun_Change(object sender, EventArgs e)
        {
            int LastDay = DateTime.DaysInMonth(int.Parse(ddlTahun.SelectedValue.ToString()), int.Parse(ddlBulan.SelectedValue.ToString()));
            txtTglMulai.Text = LastDay.ToString().PadLeft(2, '0') + "-" + ddlBulan.SelectedValue.PadLeft(2, '0') + "-" + ddlTahun.SelectedValue.ToString();
        }
    }

    public class LapLoadingTime2
    {

        ArrayList arrData = new ArrayList();
        LoadingTime ld = new LoadingTime();
        public string Criteria { get; set; }
        public string Pilihan { get; set; }
        public string DariTgl { get; set; }
        public string SampaiTgl { get; set; }
        public string StartTime { get; set; }
        public string ArmadaID { get; set; }

        private string Query()
        {
            string query = string.Empty; string query1 = string.Empty;
            switch (this.Pilihan)
            {
                case "Detail":
                    query1 = " from data_600 order by MobilSendiri,UrutanNo ";
                    query = new ReportFacade().ViewLapLoadingTime(this.DariTgl, this.SampaiTgl, int.Parse(this.ArmadaID), int.Parse(this.StartTime), query1);
                    break;
                case "Tahun":
                    query = "Select Distinct Year(tglin) as Tahun from LoadingTime order by Year(tglin) desc";
                    break;
                case "Rekap":
                    query = "if exists (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'laploadingtmp')drop table laploadingtmp; ";
                    //"select * into laploadingtmp from ( ";
                    query1 = " into laploadingtmp from data_600 order by MobilSendiri,UrutanNo ";
                    query += new ReportFacade().ViewLapLoadingTime(this.DariTgl, this.SampaiTgl, int.Parse(this.ArmadaID), int.Parse(this.StartTime), query1).Replace("order by MobilSendiri,TimeIn", "");
                    //query += " )  as XX ";

                    query += "select Cast(Tanggal as datetime)Tanggal,Day(Cast(Tanggal as datetime))Tgl,Targete,(Cast(OK as decimal)/cast(JmlMobil as decimal))*100 as Capai,JmlMobil,OK,Lewat,mBpas,Luar From( " +
                          "  select Tanggal,'95' Targete,SUM(jmlMobil) jmlMobil,SUM(jmlOK)OK,SUM(jmlLewat)Lewat, " +
                          "  SUM(BPAS)mBpas,SUM(Luar)Luar from " +
                          "  ( " +
                          "   select Convert(Char,Timein,112)Tanggal,COUNT(NoPolisi)JmlMobil, " +
                          "   Case when StatusLoading='OK' then COUNT(Nopolisi) else 0 end jmlok, " +
                          "   Case when StatusLoading!='OK' then COUNT(NoPolisi) else 0 end jmllewat, " +
                          "   Case when (StatusLoading!='OK' and Mobilsendiri=0)then Count(NoPolisi) else 0 end BPAS, " +
                          "   case when (StatusLoading!='OK' and Mobilsendiri=1) then Count(NoPolisi) else 0 end Luar " +
                          "   from laploadingtmp  " +
                          "   group by Convert(CHAR,timein,112),statusloading,MobilSendiri " +
                          "   ) as x Group By Tanggal " +
                          "   ) as xx " +
                          "   order by Tanggal";
                    break;
            }
            return query;
        }
        private LoadingTime CreateObject(SqlDataReader sdr)
        {
            ld = new LoadingTime();
            switch (this.Pilihan)
            {
                case "Detail":
                    ld.NoPolisi = sdr["NoPolisi"].ToString();
                    ld.MobilSendiri = int.Parse(sdr["MobilSendiri"].ToString());
                    ld.Keterangan = sdr["Keterangan"].ToString();
                    ld.JenisMobil = sdr["JenisMobil"].ToString();
                    ld.TimeIn = Convert.ToDateTime(sdr["TimeIn"].ToString());
                    ld.TimeOut = Convert.ToDateTime(sdr["TimeOut"].ToString());
                    ld.Status = Convert.ToInt32(sdr["Status"].ToString());
                    ld.LoadingNo = sdr["StatusLoading"].ToString();
                    //ld.LoadingNo = sdr["Status2"].ToString();
                    ld.NoUrut = sdr["urutanno"].ToString();
                    ld.Tujuan2 = sdr["Tujuan2"].ToString();

                    ld.TimeDaftar = Convert.ToDateTime(sdr["TimeDaftar"].ToString());
                    ld.Noted = sdr["Noted"].ToString();
                    //ld.Target = Convert.ToDecimal(sdr["Target"]);
                    ld.Target = sdr["Target"].ToString();
                    ld.Status2 = Convert.ToInt32(sdr["Status2"].ToString());

                    break;
                case "Tahun":
                    ld.RowStatus = Convert.ToInt32(sdr["Tahun"].ToString());
                    break;
                case "Rekap":
                    ld.ID = Convert.ToInt32(sdr["Tgl"].ToString());
                    ld.TglIn = Convert.ToDateTime(sdr["Tanggal"].ToString());
                    ld.Targete = Convert.ToDecimal(sdr["Targete"].ToString());
                    ld.JmlMobil = Convert.ToInt32(sdr["JmlMobil"].ToString());
                    ld.JmlOK = Convert.ToInt32(sdr["OK"].ToString());
                    ld.JmlLewat = Convert.ToInt32(sdr["Lewat"].ToString());
                    ld.Pencapaian = Convert.ToDecimal(sdr["Capai"].ToString());
                    ld.MobilSendiri = Convert.ToInt32(sdr["mBpas"].ToString());
                    ld.EkspedisiID = Convert.ToInt32(sdr["Luar"].ToString());

                    break;
            }
            return ld;
        }

        private string Query2()
        {
            string query = string.Empty; string query1 = string.Empty;
            switch (this.Pilihan)
            {
                case "Detail":
                    query1 = "";
                    query = new ReportFacade().ViewLapLoadingTime_asli(this.DariTgl, this.SampaiTgl, int.Parse(this.ArmadaID), int.Parse(this.StartTime));
                    break;
                case "Tahun":
                    query = "Select Distinct Year(Timein) as Tahun from LoadingTime order by Year(Timein) desc";
                    break;
                case "Rekap":
                    query = "if exists (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'laploadingtmp')drop table laploadingtmp; " +
                            "select * into laploadingtmp from ( ";
                    query1 = "";
                    query += new ReportFacade().ViewLapLoadingTime_asli(this.DariTgl, this.SampaiTgl, int.Parse(this.ArmadaID), int.Parse(this.StartTime)).Replace("order by MobilSendiri,TimeIn", "");
                    query += " )  as XX ";
                    query += "select Cast(Tanggal as datetime)Tanggal,Day(Cast(Tanggal as datetime))Tgl,Targete,(Cast(OK as decimal)/cast(JmlMobil as decimal))*100 as Capai,JmlMobil,OK,Lewat,mBpas,Luar From( " +
                          "  select Tanggal,'95' Targete,SUM(jmlMobil) jmlMobil,SUM(jmlOK)OK,SUM(jmlLewat)Lewat, " +
                          "  SUM(BPAS)mBpas,SUM(Luar)Luar from " +
                          "  ( " +
                          "   select Convert(Char,Timein,112)Tanggal,COUNT(NoPolisi)JmlMobil, " +
                          "   Case when StatusLoading='OK' then COUNT(Nopolisi) else 0 end jmlok, " +
                          "   Case when StatusLoading!='OK' then COUNT(NoPolisi) else 0 end jmllewat, " +
                          "   Case when (StatusLoading!='OK' and Mobilsendiri=0)then Count(NoPolisi) else 0 end BPAS, " +
                          "   case when (StatusLoading!='OK' and Mobilsendiri=1) then Count(NoPolisi) else 0 end Luar " +
                          "   from laploadingtmp  " +
                          "   group by Convert(CHAR,timein,112),statusloading,MobilSendiri " +
                          "   ) as x Group By Tanggal " +
                          "   ) as xx " +
                          "   order by Tanggal";
                    break;
            }
            return query;
        }
        private LoadingTime CreateObject2(SqlDataReader sdr)
        {
            ld = new LoadingTime();
            switch (this.Pilihan)
            {
                case "Detail":
                    ld.NoPolisi = sdr["NoPolisi"].ToString();
                    ld.MobilSendiri = int.Parse(sdr["MobilSendiri"].ToString());
                    ld.Keterangan = sdr["Ket"].ToString();
                    ld.JenisMobil = sdr["JenisMobil"].ToString();
                    ld.TimeIn = Convert.ToDateTime(sdr["TimeIn"].ToString());
                    ld.TimeOut = Convert.ToDateTime(sdr["TimeOut"].ToString());
                    ld.Status = Convert.ToInt32(sdr["Status2"].ToString());
                    ld.LoadingNo = sdr["StatusLoading"].ToString();
                    ld.NoUrut = sdr["urutanno"].ToString();
                    ld.Tujuan2 = sdr["Tujuan2"].ToString();

                    ld.TimeDaftar = Convert.ToDateTime(sdr["TimeDaftar"].ToString());
                    ld.Noted = sdr["Noted"].ToString();
                    ld.Target = sdr["Target"].ToString();

                    break;
                case "Tahun":
                    ld.RowStatus = Convert.ToInt32(sdr["Tahun"].ToString());
                    break;
                case "Rekap":
                    ld.ID = Convert.ToInt32(sdr["Tgl"].ToString());
                    ld.TglIn = Convert.ToDateTime(sdr["Tanggal"].ToString());
                    ld.Targete = Convert.ToDecimal(sdr["Targete"].ToString());
                    ld.JmlMobil = Convert.ToInt32(sdr["JmlMobil"].ToString());
                    ld.JmlOK = Convert.ToInt32(sdr["OK"].ToString());
                    ld.JmlLewat = Convert.ToInt32(sdr["Lewat"].ToString());
                    ld.Pencapaian = Convert.ToDecimal(sdr["Capai"].ToString());
                    ld.MobilSendiri = Convert.ToInt32(sdr["mBpas"].ToString());
                    ld.EkspedisiID = Convert.ToInt32(sdr["Luar"].ToString());

                    break;
            }
            return ld;
        }

        public ArrayList Retrieve()
        {
            arrData = new ArrayList();
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(this.Query());
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(CreateObject(sdr));
                }
            }
            return arrData;
        }

        public ArrayList Retrieve2()
        {
            arrData = new ArrayList();
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(this.Query2());
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(CreateObject2(sdr));
                }
            }
            return arrData;
        }
    }
}