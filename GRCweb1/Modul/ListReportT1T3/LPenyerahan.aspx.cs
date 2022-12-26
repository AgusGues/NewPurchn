using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;
using Factory;
using DataAccessLayer;
using System.Data.SqlClient;
using BasicFrame.WebControls;
using System.Web.UI.HtmlControls;

namespace GRCweb1.Modul.ListReportT1T3
{
    public partial class LPenyerahan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.link = "~/Default.aspx";

            if (!Page.IsPostBack)
            {
                Users users = (Users)Session["Users"]; btnApv.Visible = false; btnCancel.Visible = false;
                txtFromPostingPeriod.Text = DateTime.Now.AddDays(-1).Date.ToString("dd-MMM-yyyy");
                txtToPostingPeriod.Text = DateTime.Now.AddDays(-1).Date.ToString("dd-MMM-yyyy");
                txtTahun.Text = DateTime.Now.Year.ToString();
                btnRelease.Visible = false;
                Session["Flag"] = "b_awal";

                if (users.DeptID == 2 || users.DeptID == 3)
                {
                    Session["Flag"] = "awal";
                    LoadDataIsi();
                }
            }
        }

        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            if (txtFromPostingPeriod.Text == string.Empty || txtToPostingPeriod.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Periode Tanggal tidak boleh kosong");
                return;
            }

            string periodeAwal = DateTime.Parse(txtFromPostingPeriod.Text).ToString("yyyyMMdd");
            string periodeAkhir = DateTime.Parse(txtToPostingPeriod.Text).ToString("yyyyMMdd");
            string txPeriodeAwal = txtFromPostingPeriod.Text;
            string txPeriodeAkhir = txtToPostingPeriod.Text;

            string strError = string.Empty;
            int thn = DateTime.Parse(txtFromPostingPeriod.Text).Year;
            int blnLalu = DateTime.Parse(txtFromPostingPeriod.Text).Month;
            string frmtPrint = string.Empty;
            string ThBl = txtTahun.Text.ToString() + ddlBulan.SelectedValue.ToString().PadLeft(2, '0');

            Users users = (Users)Session["Users"];
            Dept dept = new Dept();
            DeptFacade deptfacade = new DeptFacade();
            int test = users.ID;
            dept = deptfacade.RetrieveById(users.DeptID);
            string deptname = string.Empty;
            if (dept.DeptName != string.Empty && dept.DeptName.Trim().Length > 3)
                deptname = dept.DeptName.Substring(0, 3).ToUpper();
            else
                deptname = " ";

            if (txtFromPostingPeriod.Text == string.Empty || txtToPostingPeriod.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Periode Tanggal tidak boleh kosong");
                return;
            }

            int userID = ((Users)Session["Users"]).ID;

            ReportFacadeT1T3 reportFacade = new ReportFacadeT1T3();
            string strQuery = string.Empty;
            string strcriteria = string.Empty;
            string ThBlA = string.Empty;
            if (ddlBulan.SelectedIndex > 1)
                ThBlA = txtTahun.Text.ToString() + (ddlBulan.SelectedIndex - 1).ToString().PadLeft(2, '0');
            else
            {
                ThBlA = (int.Parse(txtTahun.Text) - 1).ToString() + "12";
            }

            if (RBTgl.Checked == true)
            {
                strQuery = reportFacade.ViewLPenyerahan(periodeAwal, "harian");
                Session["periode"] = " ";
            }
            else
            {
                strQuery = reportFacade.ViewLPenyerahan(ThBl, "bulanan");
                Session["periode"] = ddlBulan.SelectedItem.Text + " " + txtTahun.Text;
            }

            SerahCuringDomain SCDomain20 = new SerahCuringDomain();
            SerahCuringFacade SCFacade20 = new SerahCuringFacade();
            int StsApv = 0;
            try
            {
                StsApv = SCFacade20.CekStsApv(periodeAwal);
            }
            catch { };

            SerahCuringDomain SCDomain200 = new SerahCuringDomain();
            SerahCuringFacade SCFacade200 = new SerahCuringFacade();
            SCDomain200 = SCFacade200.CekNama(periodeAwal);

            //if (SCDomain200.CreatedBy == null)
            //{
            //    DisplayAJAXMessage(this, " Data tidak ada !! "); return;
            //}

            if (RBBln.Checked == true)
            {
                Session["CreatedBy"] = "-";
                Session["ApproveBy"] = "-";
                Session["AcceptedBy"] = "-";
                Session["StsApv"] = "0";
            }
            else
            {
                //Session["CreatedBy"] = SCDomain200.CreatedBy.Trim();
                //Session["ApproveBy"] = SCDomain200.ApproveBy.Trim();
                //Session["AcceptedBy"] = SCDomain200.AcceptedBy.Trim();
                Session["StsApv"] = StsApv;
            }
            Session["Query"] = strQuery;

            Cetak(this);
        }

        protected void btnApv_ServerClick(object sender, EventArgs e)
        {
            int hasil2 = 0;
            Users user = (Users)Session["Users"]; int StsApv = 0;
            SerahCuringDomain SCDomain2 = new SerahCuringDomain();
            SerahCuringFacade SCFacade2 = new SerahCuringFacade();

            StsApv = (user.DeptID == 3) ? 1 : 2;

            for (int i = 0; i < DataSerah.Items.Count; i++)
            {
                HtmlTableRow tr1 = (HtmlTableRow)DataSerah.Items[i].FindControl("ps1");
                string tgl = Convert.ToDateTime(tr1.Cells[1].InnerHtml.Replace("&nbsp;", "").Trim()).ToString("yyyy-MM-dd");

                SCDomain2.TglProduksi = Convert.ToDateTime(tgl);
                SCDomain2.LastModifiedBy = user.UserAlias.Trim();
                SCDomain2.StatusApv = StsApv.ToString();

                hasil2 = SCFacade2.Approval(SCDomain2);
            }

            if (hasil2 > 0)
            {
                LoadDataIsi();
            }
        }

        protected void btnCancel_ServerClick(object sender, EventArgs e)
        {
            int hasil3 = 0;
            Users user = (Users)Session["Users"];
            SerahCuringDomain SCDomain3 = new SerahCuringDomain();
            SerahCuringFacade SCFacade3 = new SerahCuringFacade();

            //StsApv = (user.DeptID == 3) ? 2 : 1;

            for (int i = 0; i < DataSerah.Items.Count; i++)
            {
                HtmlTableRow tr1 = (HtmlTableRow)DataSerah.Items[i].FindControl("ps1");
                string tgl = Convert.ToDateTime(tr1.Cells[1].InnerHtml.Replace("&nbsp;", "").Trim()).ToString("yyyy-MM-dd");

                SCDomain3.TglProduksi = Convert.ToDateTime(tgl);
                SCDomain3.LastModifiedBy = user.UserAlias.Trim();
                //SCDomain2.StatusApv = StsApv.ToString();

                hasil3 = SCFacade3.Batal(SCDomain3);
            }

            if (hasil3 > -1)
            {
                LoadDataIsi();
            }
        }

        protected void btnRelease_ServerClick(object sender, EventArgs e)
        {
            int hasil = 0;
            Users user = (Users)Session["Users"];
            SerahCuringDomain SCDomain = new SerahCuringDomain();
            SerahCuringFacade SCFacade = new SerahCuringFacade();
            
            for (int i = 0; i < DataSerah.Items.Count; i++)
            {
                HtmlTableRow tr1 = (HtmlTableRow)DataSerah.Items[i].FindControl("ps1");

                SCDomain.No = tr1.Cells[0].InnerHtml.Replace("&nbsp;", "").Trim();
                SCDomain.TglProduksi = Convert.ToDateTime(tr1.Cells[1].InnerHtml.Replace("&nbsp;", "").Trim());
                SCDomain.Partno = tr1.Cells[2].InnerHtml.Replace("&nbsp;", "").Trim();
                SCDomain.Jumlah = Convert.ToDecimal(tr1.Cells[3].InnerHtml);
                SCDomain.M3 = Convert.ToDecimal(tr1.Cells[4].InnerHtml);
                SCDomain.MM_Produksi = Convert.ToDecimal(tr1.Cells[5].InnerHtml);
                SCDomain.Konversi = Convert.ToDecimal(tr1.Cells[6].InnerHtml);
                SCDomain.CreatedBy = user.UserAlias.Trim();

                hasil = SCFacade.Simpan(SCDomain);
            }
            if (hasil > 0)
            {
                btnRelease.Disabled = true; btnCancel.Visible = true;
                LoadDataIsi();
            }
        }

        protected void btnLihat_ServerClick(object sender, EventArgs e)
        {
            Session["Flag"] = "b_awal";
            DateTime drTgl = DateTime.Parse(txtFromPostingPeriod.Text);
            string Thn = txtTahun.Text.ToString();
            string Periode = string.Empty;

            if (RBTgl.Checked == true)
            {
                string PeriodeHarian = drTgl.ToString("yyyyMMdd");
                Periode = PeriodeHarian;
            }
            else
            {
                string Bln = (ddlBulan.SelectedValue).ToString().Length == 1 ? "0" + ddlBulan.SelectedValue : ddlBulan.SelectedValue;
                string PeriodeBulanan = txtTahun.Text.ToString() + Bln;
                Periode = PeriodeBulanan;
            }

            SerahCuringDomain Serah1 = new SerahCuringDomain();
            SerahCuringFacade FacadeSerah1 = new SerahCuringFacade();
            ArrayList arrData1 = new ArrayList();
            arrData1 = FacadeSerah1.RetrieveIsiDataCuring(Periode, "Flag", "b_awal");
            if (arrData1.Count > 0)
            {
                LoadDataIsi();
                btnRelease.Disabled = true;
            }
            else
            {
                LoadData();
                btnRelease.Disabled = false;
            }
        }
        protected void LoadDataIsi()
        {
            string Flag2 = Session["Flag"].ToString();

            Users users = (Users)Session["Users"];
            DateTime drTgl = DateTime.Parse(txtFromPostingPeriod.Text);
            string Thn = txtTahun.Text.ToString();
            string Periode = string.Empty;

            if (RBTgl.Checked == true)
            {
                string PeriodeHarian = drTgl.ToString("yyyyMMdd");
                Periode = PeriodeHarian;
            }
            else
            {
                string Bln = (ddlBulan.SelectedValue).ToString().Length == 1 ? "0" + ddlBulan.SelectedValue : ddlBulan.SelectedValue;
                string PeriodeBulanan = txtTahun.Text.ToString() + Bln;
                Periode = PeriodeBulanan;
            }

            if (users.Apv > 0 && users.DeptID == 2)
            {
                btnApv.Visible = true; btnRelease.Visible = false;

                SerahCuringDomain Serah1 = new SerahCuringDomain();
                SerahCuringFacade FacadeSerah1 = new SerahCuringFacade();
                ArrayList arrData1 = new ArrayList();
                arrData1 = FacadeSerah1.RetrieveIsiDataCuring(Periode, "MgrBM", Flag2);
                if (arrData1.Count > 0)
                {
                    DataSerah.DataSource = arrData1;
                    DataSerah.DataBind();

                    btnApv.Visible = true;
                }
                else
                {
                    SerahCuringDomain Serah11 = new SerahCuringDomain();
                    SerahCuringFacade FacadeSerah11 = new SerahCuringFacade();
                    ArrayList arrData11 = new ArrayList();
                    arrData11 = FacadeSerah11.RetrieveIsiDataCuring(Periode, "-", Flag2);

                    DataSerah.DataSource = arrData11;
                    DataSerah.DataBind();

                    btnApv.Visible = false;
                }
            }
            if (users.DeptID == 2 && users.Apv < 2)
            {
                btnApv.Visible = true; btnRelease.Visible = false;
                //btnCancel.Visible = true;

                SerahCuringDomain Serah1 = new SerahCuringDomain();
                SerahCuringFacade FacadeSerah1 = new SerahCuringFacade();
                ArrayList arrData1 = new ArrayList();
                arrData1 = FacadeSerah1.RetrieveIsiDataCuring(Periode, "AdmBM", Flag2);
                if (arrData1.Count > 0)
                {
                    DataSerah.DataSource = arrData1;
                    DataSerah.DataBind();

                    btnApv.Visible = false;
                }
                else
                {
                    SerahCuringDomain Serah10 = new SerahCuringDomain();
                    SerahCuringFacade FacadeSerah10 = new SerahCuringFacade();
                    ArrayList arrData10 = new ArrayList();
                    arrData10 = FacadeSerah10.RetrieveIsiDataCuring(Periode, "---", Flag2);

                    DataSerah.DataSource = arrData10;
                    DataSerah.DataBind();

                    btnApv.Visible = false;
                }
            }
            else if (users.DeptID == 3)
            {
                SerahCuringDomain Serah1 = new SerahCuringDomain();
                SerahCuringFacade FacadeSerah1 = new SerahCuringFacade();
                ArrayList arrData1 = new ArrayList();
                arrData1 = FacadeSerah1.RetrieveIsiDataCuring(Periode, "Finishing", Flag2);
                if (arrData1.Count > 0)
                {
                    DataSerah.DataSource = arrData1;
                    DataSerah.DataBind();

                    btnApv.Visible = true;
                }
                else
                {
                    SerahCuringDomain Serah11 = new SerahCuringDomain();
                    SerahCuringFacade FacadeSerah11 = new SerahCuringFacade();
                    ArrayList arrData11 = new ArrayList();
                    arrData11 = FacadeSerah11.RetrieveIsiDataCuring(Periode, "--", Flag2);

                    DataSerah.DataSource = arrData11;
                    DataSerah.DataBind();

                    btnApv.Visible = false;
                }
            }
        }
        protected void LoadData()
        {
            Users users = (Users)Session["Users"];
            DateTime drTgl = DateTime.Parse(txtFromPostingPeriod.Text);
            string Thn = txtTahun.Text.ToString();

            string Periode = string.Empty;
            if (RBTgl.Checked == true)
            {
                string PeriodeHarian = drTgl.ToString("yyyyMMdd");
                Periode = PeriodeHarian;
            }
            else
            {
                string Bln = (ddlBulan.SelectedValue).ToString().Length == 1 ? "0" + ddlBulan.SelectedValue : ddlBulan.SelectedValue;
                string PeriodeBulanan = txtTahun.Text.ToString() + Bln;
                Periode = PeriodeBulanan;
            }

            SerahCuringDomain Serah = new SerahCuringDomain();
            SerahCuringFacade FacadeSerah = new SerahCuringFacade();
            ArrayList arrData = new ArrayList();
            arrData = FacadeSerah.RetrieveDataCuring(Periode);
            if (arrData.Count > 0)
            {
                DataSerah.DataSource = arrData;
                DataSerah.DataBind();

                if (users.Apv > 0 || users.DeptID == 3)
                {
                    btnApv.Visible = false; btnRelease.Visible = false; btnApv.Visible = false;
                }
                else
                {
                    btnRelease.Visible = true;
                }
            }
            else
            {
                DisplayAJAXMessage(this, "Data tidak di temukan !!");
                return;
            }
        }

        protected void DataSerah_DataBound(object sender, RepeaterItemEventArgs e)
        { }

        static public void Cetak(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LPenyerahan', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            string myScript = "Cetak();";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void txtFromPostingPeriod_TextChanged(object sender, EventArgs e)
        {
            txtToPostingPeriod.Text = txtFromPostingPeriod.Text;
        }

        protected void RBTgl_CheckedChanged(object sender, EventArgs e)
        {
            if (RBTgl.Checked == true)
            {
                Panel3.Visible = true;
                Panel4.Visible = false;
            }
        }
        protected void RBBln_CheckedChanged(object sender, EventArgs e)
        {
            if (RBBln.Checked == true)
            {
                Panel3.Visible = false;
                Panel4.Visible = true;
            }
        }

        protected void txtFromPostingPeriod_TextChanged1(object sender, EventArgs e)
        {
            txtToPostingPeriod.Text = txtFromPostingPeriod.Text;
        }
        protected void txtLokasi_TextChanged(object sender, EventArgs e)
        {
            btnPrint.Focus();
        }
    }

    public class SerahCuringFacade
    {
        public string strError = string.Empty;
        private ArrayList arrData = new ArrayList();
        private SerahCuringDomain SC = new SerahCuringDomain();
        private List<SqlParameter> sqlListParam;

        public int Simpan(object objDomain)
        {
            try
            {
                SC = (SerahCuringDomain)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@No", SC.No));
                sqlListParam.Add(new SqlParameter("@TglProduksi", SC.TglProduksi));
                sqlListParam.Add(new SqlParameter("@Partno", SC.Partno));
                sqlListParam.Add(new SqlParameter("@Jumlah", SC.Jumlah));
                sqlListParam.Add(new SqlParameter("@M3", SC.M3));
                sqlListParam.Add(new SqlParameter("@MM_Produksi", SC.MM_Produksi));
                sqlListParam.Add(new SqlParameter("@Konversi", SC.Konversi));
                sqlListParam.Add(new SqlParameter("@CreatedBy", SC.CreatedBy));

                DataAccess da = new DataAccess(Global.ConnectionString());
                int result = da.ProcessData(sqlListParam, "BMSerahCuring_Simpan");
                strError = da.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int Batal(object objDomain)
        {
            try
            {
                SC = (SerahCuringDomain)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@TglProduksi", SC.TglProduksi));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", SC.LastModifiedBy));
                //sqlListParam.Add(new SqlParameter("@StatusApv", SC.StatusApv));

                DataAccess da = new DataAccess(Global.ConnectionString());
                int result = da.ProcessData(sqlListParam, "BMSerahCuring_Batal");
                strError = da.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int Approval(object objDomain)
        {
            try
            {
                SC = (SerahCuringDomain)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@TglProduksi", SC.TglProduksi));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", SC.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@StatusApv", SC.StatusApv));

                DataAccess da = new DataAccess(Global.ConnectionString());
                int result = da.ProcessData(sqlListParam, "BMSerahCuring_Approval");
                strError = da.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public ArrayList RetrieveDataCuring(string Periode)
        {
            arrData = new ArrayList();
            string Period = string.Empty; string Period1 = string.Empty;

            if (Periode.Length.ToString() == "6")
            {
                Period = " left(convert(varchar,A.TglProduksi,112),6)='" + Periode + "' ";
            }
            else
            {
                Period = " left(convert(varchar,A.TglProduksi,112),8)='" + Periode + "' ";
            }

            string strSQL =
            " select ROW_NUMBER() OVER(ORDER BY TglProduksi ASC)No,*,'Belum Release'Status,'-'LastApv from ( " +
            " select convert(varchar, TglProduksi,103)TglProduksi,cast(Jenis as varchar) +'  '+ str(Tebal,8,1) +'  '+ str(Lebar,8,0) +'  '+ str(Panjang,8,0) Partno, " +
            " qty Jumlah,round(qty*Volume,2) M3,round(Tebal*qty,2) mM_Produksi,round(((Lebar*Panjang)/(1220*2440))*qty,1) Konversi from ( " +
            " SELECT A.TglProduksi,  SUBSTRING(B.partno,1,3) Jenis, B.Tebal, B.Lebar, B.Panjang, " +
            " ((B.Tebal*B.Lebar*B.Panjang)/1000000000)Volume, SUM(A.Qty) as qty, C.Lokasi " +
            " FROM BM_Destacking AS A " +
            " LEFT OUTER JOIN FC_Items AS B ON A.ItemID = B.ID " +
            " LEFT OUTER JOIN FC_Lokasi AS C ON A.LokasiID = C.ID " +
            " LEFT OUTER JOIN BM_Formula AS D ON A.FormulaID = D.ID where qty>0 and " + Period + "" +
            " and C.Lokasi not like'%adj%' and  A.rowstatus>-1 GROUP BY A.TglProduksi, B.partno, B.Tebal, B.Lebar, B.Panjang, C.Lokasi ) as x " +
            " ) as x1 order by No ";

            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);

            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new SerahCuringDomain
                    {
                        No = sdr["No"].ToString(),
                        TglProduksi = Convert.ToDateTime(sdr["TglProduksi"].ToString()),
                        //TglProduksi2 = sdr["TglProduksi2"].ToString(),
                        Partno = sdr["Partno"].ToString(),
                        Jumlah = Convert.ToDecimal(sdr["Jumlah"].ToString()),
                        M3 = Convert.ToDecimal(sdr["M3"].ToString()),
                        MM_Produksi = Convert.ToDecimal(sdr["MM_Produksi"].ToString()),
                        Konversi = Convert.ToDecimal(sdr["Konversi"].ToString()),
                        Status = sdr["Status"].ToString(),
                        LastApv = sdr["LastApv"].ToString(),
                    });
                }
            }
            return arrData;
        }

        public ArrayList RetrieveIsiDataCuring(string Periode, string Flag, string Flag2)
        {
            arrData = new ArrayList();
            string Period = string.Empty; string Period1 = string.Empty;
            string Query = string.Empty; string Query2 = string.Empty;

            if (Periode.Length.ToString() == "6")
            {
                if (Flag2 == "b_awal")
                {
                    Period = " and left(convert(varchar,TglProduksi,112),6)='" + Periode + "' ";
                }
                else
                {
                    Period = "  ";
                }
            }
            else
            {
                if (Flag2 == "b_awal")
                {
                    Period = " and left(convert(varchar,TglProduksi,112),8)='" + Periode + "' ";
                }
                else
                {
                    Period = "  ";
                }
            }

            if (Flag == "MgrBM" && Flag2 == "b_awal")
            {
                Query = " where StatusApv=1 and rowstatus>-1 ";
            }
            else if (Flag == "Finishing" && Flag2 == "b_awal")
            {
                Query = " where StatusApv=0 and rowstatus>-1 ";
            }
            else
            {
                if (Flag2 == "awal" && Flag == "MgrBM" || Flag2 == "awal" && Flag == "-")
                {
                    Query = " where StatusApv=1 and rowstatus>-1 ";
                }
                else if (Flag2 == "awal" && Flag == "Finishing" || Flag2 == "awal" && Flag == "--")
                {
                    Query = " where StatusApv=0 and rowstatus>-1 ";
                }
                else if (Flag == "AdmBM" && Flag2 == "awal" || Flag == "---" && Flag2 == "awal")
                {
                    Query = " where StatusApv<2 and rowstatus>-1 ";
                }
                else
                {
                    Query = "";
                }
            }

            string strSQL =
            " select Nomor No,TglProduksi,Partno,Qty Jumlah,M3,MiliP MM_Produksi,Konversi4x8 Konversi, " +
            " case when StatusApv=0 then 'Open' when StatusApv=1 then 'Approved' when StatusApv=2 then 'Finished' end Status, " +
            " case when StatusApv=0 then '-' when StatusApv=1 then AcceptedBy when StatusApv=2 then ApproveBy end LastApv from ( " +
            " select cast(No as int)Nomor,* from bm_serahcuring " + Query + " ) as x where Rowstatus>-1 " + Period + " " +
            " order by Nomor asc";

            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);

            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new SerahCuringDomain
                    {
                        No = sdr["No"].ToString(),
                        TglProduksi = Convert.ToDateTime(sdr["TglProduksi"].ToString()),
                        //TglProduksi2 = sdr["TglProduksi2"].ToString(),
                        Partno = sdr["Partno"].ToString(),
                        Jumlah = Convert.ToDecimal(sdr["Jumlah"].ToString()),
                        M3 = Convert.ToDecimal(sdr["M3"].ToString()),
                        MM_Produksi = Convert.ToDecimal(sdr["MM_Produksi"].ToString()),
                        Konversi = Convert.ToDecimal(sdr["Konversi"].ToString()),
                        Status = sdr["Status"].ToString(),
                        LastApv = sdr["LastApv"].ToString(),
                    });
                }
            }
            return arrData;
        }

        public int CekStsApv(string Periode)
        {
            string StrSql = " select sum(StatusApv)StsApv from ( " +
                            " select distinct StatusApv from BM_SerahCuring where TglProduksi='" + Periode + "' and Rowstatus>-1) as x ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["StsApv"]);
                }
            }

            return 0;
        }

        public SerahCuringDomain CekNama(string Periode)
        {
            string strSQL = string.Empty;
            strSQL =
            " select distinct CreatedBy,ApproveBy,AcceptedBy from BM_SerahCuring where TglProduksi='" + Periode + "' and Rowstatus>-1 ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject_1(sqlDataReader);
                }
            }

            return new SerahCuringDomain();
        }

        public SerahCuringDomain GenerateObject_1(SqlDataReader sqlDataReader)
        {
            SC = new SerahCuringDomain();
            SC.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            SC.ApproveBy = sqlDataReader["ApproveBy"].ToString();
            SC.AcceptedBy = sqlDataReader["AcceptedBy"].ToString();
            return SC;
        }
    }

    public class SerahCuringDomain
    {
        public string No { get; set; }
        public string Jenis { get; set; }
        public string Lokasi { get; set; }
        public string CreatedBy { get; set; }
        public string Partno { get; set; }
        public string Status { get; set; }
        public string LastApv { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedTime { get; set; }

        public decimal Tebal { get; set; }
        public decimal Lebar { get; set; }
        public decimal Panjang { get; set; }
        public decimal Jumlah { get; set; }
        public decimal M3 { get; set; }
        public decimal MM_Produksi { get; set; }
        public decimal Konversi { get; set; }

        public DateTime TglProduksi { get; set; }
        public string TglProduksi2 { get; set; }
        public string StatusApv { get; set; }
        public string ApproveBy { get; set; }
        public string AcceptedBy { get; set; }
    }
}