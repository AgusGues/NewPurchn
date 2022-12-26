using BusinessFacade;
using DataAccessLayer;
using Domain;
using Factory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;

//using DefectFacade;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.SarMut
{
    public partial class BM_PelumasPlat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "../../Default.aspx";
                getYear();
                ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
                txtdrtanggal.Text = "01-" + DateTime.Now.ToString("MMM-yyyy");
                txtsdtanggal.Text = Convert.ToDateTime(DateTime.Parse("1-" + (DateTime.Now.AddMonths(1)).ToString("MMM-yyyy"))).AddDays(-1).ToString("dd") + "-" + DateTime.Now.ToString("MMM") + "-" + DateTime.Now.ToString("yyyy");
            }
        //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GrdDynamic.ClientID + "', 500, 100 , 30 ,false); </script>", false);
        //((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(LinkButton3);
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

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void txtsdtanggal_TextChanged(object sender, EventArgs e)
        {
            //if (DateTime.Parse(txtsdtanggal.Text) < DateTime.Parse(txtdrtanggal.Text) ||
            //    DateTime.Parse(txtdrtanggal.Text).ToString("MMM-yyyy")!=DateTime.Parse(txtsdtanggal.Text).ToString("MMMyyyy"))
            //    txtdrtanggal.Text = "01-" + DateTime.Parse(txtsdtanggal.Text).ToString("MMM-yyyy");
        }

        protected void txtdrtanggal_TextChanged(object sender, EventArgs e)
        {
            //if (DateTime.Parse(txtsdtanggal.Text) < DateTime.Parse(txtdrtanggal.Text) ||
            //    DateTime.Parse(txtdrtanggal.Text).ToString("MMM-yyyy") != DateTime.Parse(txtsdtanggal.Text).ToString("MMMyyyy"))
            //    txtsdtanggal.Text = Convert.ToDateTime(DateTime.Parse("1-" + (DateTime.Parse(txtdrtanggal.Text).AddMonths(1)).ToString("MMM-yyyy"))).AddDays(-1).ToString("dd")
            //        + "-" + DateTime.Parse(txtdrtanggal.Text).ToString("MMM") + "-" + DateTime.Now.ToString("yyyy");
        }

        protected void ddlBulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime tgl = DateTime.Parse("01-" + ddlBulan.SelectedValue + "-" + ddTahun.SelectedValue);
            txtdrtanggal.Text = "01-" + tgl.ToString("MMM-yyyy");
            txtsdtanggal.Text = Convert.ToDateTime(DateTime.Parse("1-" + (tgl.AddMonths(1)).ToString("MMM-yyyy"))).AddDays(-1).ToString("dd") +
                "-" + tgl.ToString("MMM") + "-" + tgl.ToString("yyyy");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            LoadPreview();
            LblLine.Text = ddlLine.SelectedValue;
            LblTgl1.Text = DateTime.Parse(txtdrtanggal.Text).ToString("MMMM yyyy");
            LoadTarget();
            //LoadHarga();
        }

        protected void LoadPreview()
        {
            string TahunR = DateTime.Now.Year.ToString();
            string BulanR = DateTime.Now.Month.ToString();
            int HariR = DateTime.Now.Day;
            if (HariR < 10)
            { string HariR0 = "0" + HariR.ToString(); Session["HariR"] = HariR0; }
            else
            { string HariR0 = HariR.ToString(); Session["HariR"] = HariR0; }

            if (BulanR == "1") { string BulanString = "Januari"; Session["BulanS"] = BulanString; }
            else if (BulanR == "2") { string BulanString = "Februari"; Session["BulanS"] = BulanString; }
            else if (BulanR == "3") { string BulanString = "Maret"; Session["BulanS"] = BulanString; }
            else if (BulanR == "4") { string BulanString = "April"; Session["BulanS"] = BulanString; }
            else if (BulanR == "5") { string BulanString = "Mei"; Session["BulanS"] = BulanString; }
            else if (BulanR == "6") { string BulanString = "Juni"; Session["BulanS"] = BulanString; }
            else if (BulanR == "7") { string BulanString = "Juli"; Session["BulanS"] = BulanString; }
            else if (BulanR == "8") { string BulanString = "Agustus"; Session["BulanS"] = BulanString; }
            else if (BulanR == "9") { string BulanString = "September"; Session["BulanS"] = BulanString; }
            else if (BulanR == "10") { string BulanString = "Oktober"; Session["BulanS"] = BulanString; }
            else if (BulanR == "11") { string BulanString = "November"; Session["BulanS"] = BulanString; }
            else if (BulanR == "12") { string BulanString = "Desember"; Session["BulanS"] = BulanString; }
            string BulanAlias = Session["BulanS"].ToString();
            string HariAlias = Session["HariR"].ToString();
            string TglAliasR = HariAlias + "-" + BulanAlias + "-" + TahunR; Session["Tgl"] = TglAliasR;
            Users user = (Users)Session["Users"];
            int Bulan1 = int.Parse(ddlBulan.SelectedValue);
            string Tahun = ddTahun.SelectedValue.ToString();
            if (Bulan1 < 10)
            {
                string Bulan2 = "0" + Bulan1.ToString(); Session["Bulan2"] = Bulan2;
            }
            else
            {
                string Bulan2 = Bulan1.ToString(); Session["Bulan2"] = Bulan2;
            }

            string Periode = Tahun + Session["Bulan2"].ToString() + "01";
            string PeriodeBulanTahun = Tahun + Session["Bulan2"].ToString();
            int Line = int.Parse(ddlLine.SelectedValue);
            Session["PeriodeBulanTahun"] = PeriodeBulanTahun;
            PlatDomain PlatDt = new PlatDomain();
            FacadePlat FcdPlat = new FacadePlat();
            ArrayList arrData = new ArrayList();
            if (user.UnitKerjaID == 13)
            {
                arrData = FcdPlat.RetrieveDataPlatJmbg(Periode, PeriodeBulanTahun, Line); Session["Data"] = arrData;
                lstPlatjmbg.DataSource = arrData;
                lstPlatjmbg.DataBind();
                Panel1.Visible = false;
                Panel2.Visible = true;
            }
            else
            {
                arrData = FcdPlat.RetrieveDataPlat(Periode, PeriodeBulanTahun, Line); Session["Data"] = arrData;
                lstPlat.DataSource = arrData;
                lstPlat.DataBind();
                Panel1.Visible = true;
                Panel2.Visible = false;
            }

            #region AutoSarmut [Board Mill] Efisiensi Pemakaian Solar Destacking
            string strQuery = string.Empty;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = strQuery;
            SqlDataReader sdr = zl.Retrieve();
            string sarmutPrs = "Efisiensi Pemakaian Solar Destacking";
            string strJmlLine = string.Empty;
            string strDept = string.Empty;
            int deptid = getDeptID("BOARD");
            switch (Line)
            {
                case 1:
                    strJmlLine = "Line 1";
                    break;

                case 2:
                    strJmlLine = "Line 2";
                    break;

                case 3:
                    strJmlLine = "Line 3";
                    break;

                case 4:
                    strJmlLine = "Line 4";
                    break;

                case 5:
                    strJmlLine = "Line 5";
                    break;

                case 6:
                    strJmlLine = "Line 6";
                    break;
            }
            string queryy;
            if (user.UnitKerjaID != 13)
            {
                if (Convert.ToInt32(PeriodeBulanTahun) >= 202210)
                {
                    queryy = " select cast(((((select sum(QtyBco) from PelumasPlatTemp where day(tanggal)>=1 and day(tanggal)<=14) * " +
                            " (Select Price from BM_PlatHarga where [Range]='1-14' and NmPelumas='Bco') /" +
                            " (Select Price from BM_PlatHarga where [Range]='1-14' and NmPelumas='Solar')) + " +
                            " ((select sum(QtySolar) from PelumasPlatTemp where day(tanggal)>=1 and day(tanggal)<=14) * " +
                            " (Select Price from BM_PlatHarga where [Range] = '1-14' and NmPelumas = 'Solar') /" +
                            " (Select Price from BM_PlatHarga where[Range] = '1-14' and NmPelumas = 'Solar')) +" +
                            " ((select sum(QtyBco) from PelumasPlatTemp where day(tanggal)>=15 and day(tanggal)<=31) *" +
                            " (Select Price from BM_PlatHarga where [Range]='15-31' and NmPelumas='Bco') /" +
                            " (Select Price from BM_PlatHarga where [Range]='15-31' and NmPelumas='Solar')) + " +
                            " ((select sum(QtySolar) from PelumasPlatTemp where day(tanggal)>=15 and day(tanggal)<=31) * " +
                            " (Select Price from BM_PlatHarga where [Range] = '15-31' and NmPelumas = 'Solar') / " +
                            " (Select Price from BM_PlatHarga where[Range] = '15-31' and NmPelumas = 'Solar')) )/ sum(OutPutM3))as decimal(18,2))actual from PelumasPlatTemp ";
                }
                else
                {
                    queryy = " select cast(((((select sum(QtyBco) from PelumasPlatTemp where day(tanggal)>=1 and day(tanggal)<=15) * " +
                            " (Select Price from BM_PlatHarga where [Range]='1-15' and NmPelumas='Bco') /" +
                            " (Select Price from BM_PlatHarga where [Range]='1-15' and NmPelumas='Solar')) + " +
                            " ((select sum(QtySolar) from PelumasPlatTemp where day(tanggal)>=1 and day(tanggal)<=15) * " +
                            " (Select Price from BM_PlatHarga where [Range] = '1-15' and NmPelumas = 'Solar') /" +
                            " (Select Price from BM_PlatHarga where[Range] = '1-15' and NmPelumas = 'Solar')) +" +
                            " ((select sum(QtyBco) from PelumasPlatTemp where day(tanggal)>=16 and day(tanggal)<=31) *" +
                            " (Select Price from BM_PlatHarga where [Range]='16-31' and NmPelumas='Bco') /" +
                            " (Select Price from BM_PlatHarga where [Range]='16-31' and NmPelumas='Solar')) + " +
                            " ((select sum(QtySolar) from PelumasPlatTemp where day(tanggal)>=16 and day(tanggal)<=31) * " +
                            " (Select Price from BM_PlatHarga where [Range] = '16-31' and NmPelumas = 'Solar') / " +
                            " (Select Price from BM_PlatHarga where[Range] = '16-31' and NmPelumas = 'Solar')) )/ sum(OutPutM3))as decimal(18,2))actual from PelumasPlatTemp ";
                }
            }
            else
            {
                queryy = " select cast(((((select sum(QtyBco) from PelumasPlatTemp where day(tanggal)>=1 and day(tanggal)<=15) * " +
                           " (Select Price from BM_PlatHarga where [Range]='1-15' and NmPelumas='Bco') /" +
                           " (Select Price from BM_PlatHarga where [Range]='1-15' and NmPelumas='Solar')) + " +
                           " ((select sum(QtySolar) from PelumasPlatTemp where day(tanggal)>=1 and day(tanggal)<=15) * " +
                           " (Select Price from BM_PlatHarga where [Range] = '1-15' and NmPelumas = 'Solar') /" +
                           " (Select Price from BM_PlatHarga where[Range] = '1-15' and NmPelumas = 'Solar')) +" +
                           " ((select sum(QtyBco) from PelumasPlatTemp where day(tanggal)>=16 and day(tanggal)<=31) *" +
                           " (Select Price from BM_PlatHarga where [Range]='16-31' and NmPelumas='Bco') /" +
                           " (Select Price from BM_PlatHarga where [Range]='16-31' and NmPelumas='Solar')) + " +
                           " ((select sum(QtySolar) from PelumasPlatTemp where day(tanggal)>=16 and day(tanggal)<=31) * " +
                           " (Select Price from BM_PlatHarga where [Range] = '16-31' and NmPelumas = 'Solar') / " +
                           " (Select Price from BM_PlatHarga where[Range] = '16-31' and NmPelumas = 'Solar')) )/ sum(OutPutM3))as decimal(18,2))actual from PelumasPlatTemp ";
            }
            #region #1
            decimal actual = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = queryy;
            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actual = Decimal.Parse(sdr["actual"].ToString());
                }
            }

            ZetroView zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery =
                "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + DateTime.Parse(txtdrtanggal.Text).Year.ToString() +
                " set @bulan=" + DateTime.Parse(txtsdtanggal.Text).Month.ToString() + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                "update SPD_Trans set actual=" + actual.ToString().Replace(",", ".") + " where Approval=0 and tahun=@tahun and bulan=@bulan and  SarmutDeptID in ( " +
                "select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and sarmutdepartemen='" +
                strJmlLine + "' and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "'))";
            SqlDataReader sdr1 = zl1.Retrieve();
            if (sdr1.HasRows)
            {
                while (sdr1.Read())
                {
                    actual = Decimal.Parse(sdr1["actual"].ToString());
                }
            }
            #endregion #1

            #region#2
            //hitung average untuk spd_perusahaan
            actual = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                " declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + DateTime.Parse(txtdrtanggal.Text).Year.ToString() +
                " set @bulan=" + DateTime.Parse(txtsdtanggal.Text).Month.ToString() + " " +
                " set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                " select isnull(CAST((sum(Actual))as decimal(18,2)),0) / ( select Count(ID) from  SPD_Trans  where tahun=@tahun and bulan=@bulan and  SarmutDeptID in " +
                " ( select ID from SPD_Departemen where Actual>0 and @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and SarmutPID " +
                " in ( select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "'))) Actual from ( " +
                " select * from  SPD_Trans  where tahun=@tahun and bulan=@bulan and  SarmutDeptID in " +
                " ( select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and SarmutPID " +
                " in ( select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "')) " +
                " ) as d1 ";
            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actual = Decimal.Parse(sdr["actual"].ToString());
                }
            }
            zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;

            zl1.CustomQuery =
                "update SPD_TransPrs set actual= " + actual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + DateTime.Parse(txtdrtanggal.Text).Year.ToString() +
                 " and Bulan=" + DateTime.Parse(txtsdtanggal.Text).Month.ToString() +
                 " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "') ";
            sdr1 = zl1.Retrieve();
            #endregion AutoSarmut [Board Mill] Efisiensi Pemakaian Solar Destacking

            #endregion

            string thnbln = ddTahun.SelectedValue.Trim() + ddlBulan.SelectedValue.Trim().PadLeft(2, '0');

            if (user.UnitKerjaID == 7)
            {
                string nilai = FcdPlat.HasilKarawang(thnbln);
                lbltotalakumulasi.Text = nilai;
            }
            else if (user.UnitKerjaID == 13)
            {
                string nilai = FcdPlat.HasilJombang(thnbln);
                lbltotalakumulasi.Text = nilai;
            }
            else if (user.UnitKerjaID == 1)
            {
                string nilai = FcdPlat.HasilCitereup(thnbln);
                lbltotalakumulasi.Text = nilai;
            }
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

        protected void ExportToExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            //LoadData();
            Response.AddHeader("content-disposition", "attachment;filename=PemantauanPemakaianSolar.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "";
            //Html += "<br>Periode : " + ddlBulan.SelectedItem.Text + "  " + ddlTahun.SelectedValue.ToString();
            string HtmlEnd = "";
            dataTable.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("xx\">", "\">\'");
            Contents = Contents.Replace("border=\"0", "border=\"1");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
        }

        public int newdata = 0;

        private void LoadHarga(Repeater lst)
        {
            #region Ambil Harga dari PO Terakhir
            ArrayList arrData = new ArrayList();
            //newdata = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = " select top 1 E.ItemName Nama,A.Price Harga from POPurchnDetail A " +
                             " inner join POPurchn C on A.POID=C.ID " +
                             " inner join Inventory E on A.ItemID=E.ID " +
                             " where A. ItemID in (select ID from Inventory where ItemCode='112102001040300') and Month(C.POPurchnDate)='" + ddlBulan.SelectedValue + "' " +
                             " and Year(C.POPurchnDate)='" + ddTahun.SelectedValue + "' " +
                             " union all " +
                             " select top 1 E.ItemName Nama,A.Price Harga from POPurchnDetail A " +
                             " inner join POPurchn C on A.POID=C.ID " +
                             " inner join Inventory E on A.ItemID=E.ID " +
                             " where A. ItemID in (select ID from Inventory where ItemCode='020140001010000') and Month(C.POPurchnDate)='" + ddlBulan.SelectedValue + "' " +
                             " and Year(C.POPurchnDate)='" + ddTahun.SelectedValue + "' ";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new PlatDomain
                        {
                            Nama = sdr["Nama"].ToString(),
                            Harga = Convert.ToDecimal(sdr["Harga"].ToString())
                        });
                    }
                }
            }
            //lstPlat.DataSource = arrData;
            //lstPlat.DataBind();
            #endregion
        }

        private void LoadTarget()
        {
            #region Ambil Target Dari Sarmut
            string sarmutPrs = "Efisiensi Pemakaian Solar Destacking";
            string strDept = string.Empty;
            int deptid = getDeptID("BOARD");
            #endregion
            decimal Target = 0;
            string Satuan = string.Empty;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = " select D.TargetV1 [Target],E.Satuan from  SPD_Trans A " +
                             " inner join SPD_Departemen C on A.SarmutDeptID=C.ID " +
                             " inner join SPD_TargetV D on C.TargetVID=D.ID " +
                             " inner join SPD_Satuan E on E.ID=C.SatuanID " +
                             " where tahun=" + ddTahun.SelectedValue + " " +
                             " and bulan=" + ddlBulan.SelectedValue + " and  SarmutDeptID in " +
                             " ( select ID from SPD_Departemen where rowstatus>-1  and sarmutdepartemen='LINE " + ddlLine.SelectedValue + "' and SarmutPID " +
                             " in ( select ID from SPD_Perusahaan where deptid=" + deptid + " " +
                             " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "'))";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    Target = Decimal.Parse(sdr["Target"].ToString());
                }
            }

            LblTarget.Text = Target.ToString();
        }

        protected void lstPlat_DataBound(object sender, RepeaterItemEventArgs e)
        {
            //decimal totalBco = 0;
            ////Repeater rptsprice = (Repeater)e.Item.FindControl("rptrHarga");
            //////LoadHarga(rptsprice);
            //Users user = (Users)Session["Users"];
            //for (int i = 0; i < lstPlat.Items.Count; i++)
            //{
            //    HtmlTableRow tr = (HtmlTableRow)lstPlat.Items[i].FindControl("lst2");
            //    totalBco += tr.Cells[1].InnerHtml != "" ? decimal.Parse(tr.Cells[1].InnerHtml) : 0;
            ////    Label LtrMtr = (Label)lstPlat.Items[i].FindControl("lblLtrMtr");
            ////    Label QtySolar = (Label)lstPlat.Items[i].FindControl("lblQtySolar");
            ////    Label OutPutM3 = (Label)lstPlat.Items[i].FindControl("lblOutPutM3");

            ////    //decimal xxx = Convert.ToDecimal(LtrMtr);
            ////    if (LtrMtr.Text == "")
            ////    {
            ////        LtrMtr.Text = "0,0";
            ////    }
            ////    try
            ////    {
            ////        LtrMtr.Text = (((decimal.Parse(QtySolar.Text) * (7864) / (7864)) / (decimal.Parse(OutPutM3.Text))).ToString("N1"));
            ////    }
            ////    catch (DivideByZeroException)
            ////    {
            ////    }
            //}
            //string thnbln = ddTahun.SelectedValue.Trim() + ddlBulan.SelectedValue.Trim().PadLeft(2, '0');
            //FacadePlat fp = new FacadePlat();
            //string nilai = fp.HasilKarawang(thnbln);

            //HtmlTableRow trfooter = (HtmlTableRow)DataTb.Controls[0].FindControl("lstK2F1");
            //trfooter.Cells[0].InnerHtml = "&nbsp;&nbsp;" + "<b> TOTAL AKUMULASI </b>";
            //trfooter.Cells[1].InnerHtml = nilai;
        }

        protected void lstPlatjmbg_DataBound(object sender, RepeaterItemEventArgs e)
        {
        }

        protected void lstPlat_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
        }

        protected void lstPlatjmbg_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
        }

        protected void rptrHarga_DataBound(object sender, RepeaterItemEventArgs e)
        {
        }

        protected void rptrHarga_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
        }
    }
}

public class PlatDomain
{
    public int UnitKerjaID { get; set; }
    public int ID { get; set; }
    public DateTime Tanggalx { get; set; }
    public string Tanggal { get; set; }
    public Decimal QtySolar { get; set; }
    public Decimal QtyBco { get; set; }
    public Decimal OutPutM3 { get; set; }
    public Decimal OutPutM3NP { get; set; }
    public Decimal LtrMtr { get; set; }
    public Decimal Harga { get; set; }
    public string Nama { get; set; }
}

public class FacadePlat
{
    public string strError = string.Empty;
    private ArrayList arrData = new ArrayList();
    private List<SqlParameter> sqlListParam;
    private PlatDomain objPlat = new PlatDomain();

    public FacadePlat()
        : base()
    {
    }

    public ArrayList RetrieveDataPlat(string Periode, string Periode2, int Line)
    {
        arrData = new ArrayList();
        string strsql = this.DataPlat(Periode, Periode2, Line);
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(this.DataPlat(Periode, Periode2, Line));
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrData.Add(GenerateObjectX(sdr));
            }
        }
        return arrData;
    }

    public ArrayList RetrieveDataPlatJmbg(string Periode, string Periode2, int Line)
    {
        arrData = new ArrayList();
        string strsql = this.DataPlatJmbg(Periode, Periode2, Line);
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(this.DataPlatJmbg(Periode, Periode2, Line));
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrData.Add(GenerateObjectJmbg(sdr));
            }
        }
        return arrData;
    }

    public string HasilKarawang(string thnbln)
    {
        string actual = string.Empty;
        ZetroView zv = new ZetroView();
        zv.QueryType = Operation.CUSTOM;
        string strSQL = string.Empty;
        zv.CustomQuery = "SELECT isnull(QueryAutoPES,'')QueryAutoPES from iso_usercategory where id=9747";
        SqlDataReader dr = zv.Retrieve();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                strSQL = dr["QueryAutoPES"].ToString().Trim();
            }
        }
        if (strSQL != string.Empty)
        {
            string strparameter = "declare @thnbln int set @thnbln='" + thnbln + " '";
            ZetroView zv1 = new ZetroView();
            zv1.QueryType = Operation.CUSTOM;
            zv1.CustomQuery = strparameter + strSQL;
            //zv1.CustomQuery = "SELECT 1.85 actual";
            SqlDataReader dr1 = zv1.Retrieve();
            if (dr1.HasRows)
            {
                while (dr1.Read())
                {
                    actual = dr1["actual"].ToString().Trim();
                }
            }
            actual = actual.Replace(",", ".");
            //if (actual == "0" )
            //    actual = "100";
            //if (actual == "")
            //    actual = "100";
        }

        return actual;
    }

    public string HasilCitereup(string thnbln)
    {
        string actual = string.Empty;
        ZetroView zv = new ZetroView();
        zv.QueryType = Operation.CUSTOM;
        string strSQL = string.Empty;
        zv.CustomQuery = "SELECT isnull(QueryAutoPES,'')QueryAutoPES from iso_usercategory where id=8379";
        SqlDataReader dr = zv.Retrieve();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                strSQL = dr["QueryAutoPES"].ToString().Trim();
            }
        }
        if (strSQL != string.Empty)
        {
            string strparameter = "declare @thnbln int set @thnbln='" + thnbln + " '";
            ZetroView zv1 = new ZetroView();
            zv1.QueryType = Operation.CUSTOM;
            zv1.CustomQuery = strparameter + strSQL;
            SqlDataReader dr1 = zv1.Retrieve();
            if (dr1.HasRows)
            {
                while (dr1.Read())
                {
                    actual = dr1["actual"].ToString().Trim();
                }
            }
            actual = actual.Replace(",", ".");
            //if (actual == "0" )
            //    actual = "100";
            //if (actual == "")
            //    actual = "100";
        }

        return actual;
    }

    public string HasilJombang(string thnbln)
    {
        string actual = string.Empty;
        ZetroView zv = new ZetroView();
        zv.QueryType = Operation.CUSTOM;
        string strSQL = string.Empty;
        zv.CustomQuery = "SELECT isnull(QueryAutoPES,'')QueryAutoPES from iso_usercategory where id=9670";
        SqlDataReader dr = zv.Retrieve();
        if (dr.HasRows)
        {
            while (dr.Read())
            {
                strSQL = dr["QueryAutoPES"].ToString().Trim();
            }
        }
        if (strSQL != string.Empty)
        {
            string strparameter = "declare @thnbln int set @thnbln='" + thnbln + " '";
            ZetroView zv1 = new ZetroView();
            zv1.QueryType = Operation.CUSTOM;
            zv1.CustomQuery = strparameter + strSQL;
            SqlDataReader dr1 = zv1.Retrieve();
            if (dr1.HasRows)
            {
                while (dr1.Read())
                {
                    actual = dr1["actual"].ToString().Trim();
                }
            }
            actual = actual.Replace(",", ".");
            //if (actual == "0" )
            //    actual = "100";
            //if (actual == "")
            //    actual = "100";
        }

        return actual;
    }

    private string DataPlat(string Periode, string Periode2, int Line)
    {
        string Result;
        if (Convert.ToInt32(Periode) >= 20221001)
        {
            Result = "IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PelumasPlatTemp]') AND type in (N'U')) DROP TABLE [dbo].PelumasPlatTemp  " +
                    "IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HargaBcS]') AND type in (N'U')) DROP TABLE[dbo].HargaBcS " +
                    "IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OutPutM3NP]') AND type in (N'U')) DROP TABLE[dbo].OutPutM3NP " +
                    "IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OutPutM3]') AND type in (N'U')) DROP TABLE[dbo].OutPutM3 " +
                    "IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PakaiBco]') AND type in (N'U')) DROP TABLE[dbo].PakaiBco " +
                    "IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PakaiSolar]') AND type in (N'U')) DROP TABLE[dbo].PakaiSolar " +
                    "IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DaysInMonth]') AND type in (N'U')) DROP TABLE[dbo].DaysInMonth " +
                    "IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BM_PlatHarga]') AND type in (N'U')) DROP TABLE[dbo].BM_PlatHarga " +
                    "declare @date datetime " +
                    "declare @line char " +
                    "set @date = '" + Periode + "' " +
                    "declare @date1 varchar(max) " +
                    "set @date1 = substring(convert(char, @date, 112), 1, 6) " +
                    "set @line = '" + Line + "' " +
                    ";with DaysInMont as (select @date as Date " +
                    "union all " +
                    "select dateadd(dd,1, Date)  from DaysInMont  where month(date) = month(@Date))  " +
                    "select * into DaysInMonth from DaysInMont " +

                    "select * into PakaiSolar from (select C.Quantity, A.PakaiDate from Pakai A " +
                    "                INNER JOIN PakaiDetail C ON A.ID = C.PakaiID " +
                    "                where LEFT(convert(char, A.pakaidate, 112), 6) = @date1 and C.ItemID   " +
                    "                in (select ID from Inventory where ItemCode = '112102001040300' and aktif = 1 and C.rowstatus > -1) " +
                    "                and C.ProdLine = @line  and A.Status = 3  )z " +

                    "select * into PakaiBco from (select C0.Quantity, A0.PakaiDate from Pakai A0 " +
                    "                INNER JOIN PakaiDetail C0 ON A0.ID = C0.PakaiID " +
                    "                where LEFT(convert(char, A0.pakaidate, 112), 6) = @date1 and C0.ItemID  " +
                    "                in (select ID from Inventory where ItemCode = '020140001010000' and aktif = 1 and C0.rowstatus > -1)  " +
                    "                and C0.ProdLine = @line  and A0.Status = 3 )z0 " +

                    "select * into OutPutM3 from (select C1.Qty, LEFT(convert(char, C1.TglProduksi, 112), 8)TglProduksi, ((C2.Tebal * C2.Lebar * C2.Panjang) / 1000000000)Volume from BM_Destacking C1 " +
                    "                INNER JOIN fc_items C2 ON C1.ItemID = C2.ID " +
                    "                INNER JOIN BM_PlantGroup C3 ON C1.PlantGroupID = C3.ID " +
                    "                where LEFT(convert(char, tglproduksi, 112), 6) = @date1 and C3.PlantID = @line  and C1.rowstatus > -1 and c2.Pressing != 'NO')z1 " +

                    "select* into OutPutM3NP from (select C1.Qty, LEFT(convert(char, C1.TglProduksi, 112), 8)TglProduksi, ((C2.Tebal * C2.Lebar * C2.Panjang) / 1000000000)Volume from BM_Destacking C1 " +
                    "                        INNER JOIN fc_items C2 ON C1.ItemID = C2.ID " +
                    "                INNER JOIN BM_PlantGroup C3 ON C1.PlantGroupID = C3.ID " +
                    "                where LEFT(convert(char, tglproduksi, 112), 6) = @date1 and C3.PlantID = @line  and C1.rowstatus > -1 and c2.Pressing = 'NO')z2 " +

                    "select* into HargaBcS from (select * from((select Ex.ItemName Nama, Ax.Price Harga, Cx.POPurchnDate from POPurchnDetail Ax " +
                    "                inner join POPurchn Cx on Ax.POID = Cx.ID " +
                    "                inner join Inventory Ex on Ax.ItemID = Ex.ID " +
                    "                where Ax.ItemID in (select ID from Inventory where ItemCode = '020140001010000') " +
                    "                and LEFT(convert(char, Cx.POPurchnDate, 112), 6) = @date1 and cx.Status > -1)  " +
                    "                union all " +
                    "                (select Ex1.ItemName Nama, Ax1.Price Harga, Cx1.POPurchnDate from POPurchnDetail Ax1 " +
                    "                inner join POPurchn Cx1 on Ax1.POID = Cx1.ID " +
                    "                inner join Inventory Ex1 on Ax1.ItemID = Ex1.ID " +
                    "                where Ax1.ItemID in (select ID from Inventory where ItemCode = '112102001040300') " +
                    "                and LEFT(convert(char, Cx1.POPurchnDate, 112), 6)= @date1 and Cx1.Status > -1) " +
                    "                union all " +
                    "                (select top 1 Ex.ItemName Nama, Ax.Price Harga, Cx.POPurchnDate from POPurchnDetail Ax " +
                    "                inner join POPurchn Cx on Ax.POID = Cx.ID " +
                    "                inner join Inventory Ex on Ax.ItemID = Ex.ID " +
                    "                where Ax.ItemID in (select ID from Inventory where ItemCode = '020140001010000') " +
                    "                and LEFT(convert(char, Cx.POPurchnDate, 112), 6)< @date1 and cx.Status > -1 order by Cx.POPurchnDate desc) " +
                    "                union all " +
                    "                (select top 1 Ex1.ItemName Nama, Ax1.Price Harga, Cx1.POPurchnDate from POPurchnDetail Ax1 " +
                    "                inner join POPurchn Cx1 on Ax1.POID = Cx1.ID " +
                    "                inner join Inventory Ex1 on Ax1.ItemID = Ex1.ID " +
                    "                where Ax1.ItemID in (select ID from Inventory where ItemCode = '112102001040300') " +
                    "                and LEFT(convert(char, Cx1.POPurchnDate, 112), 6)< @date1 and Cx1.Status > -1 order by Cx1.POPurchnDate desc))a)x " +

                    "select* into BM_PlatHarga from( " +
                    "               select '1' id, 'Solar' Nmpelumas, " +
                    "               case when(select count(harga) from HargaBcS where nama like 'solar%' and(day(POPurchnDate) >= 1 and day(POPurchnDate) <= 14)) > 0 then " +
                    "               (select top 1 harga from HargaBcS where nama like 'solar%' and(day(POPurchnDate) >= 1 and day(POPurchnDate) <= 14) order by POPurchnDate desc)  " +
                    "	            else(select top 1 harga from HargaBcS where nama like 'solar%' order by POPurchnDate desc)	 end Price,'1-14' Range, '0' Rowstatus " +
                    "               union all " +
                    "               select '2' id,'Solar' Nmpelumas, " +
                    "               case when(select count(harga) from HargaBcS where nama like 'Solar%' and(day(POPurchnDate) >= 15 and day(POPurchnDate) <= 31)) > 0 then " +
                    "               (select top 1 harga from HargaBcS where nama like 'Solar%' and(day(POPurchnDate) >= 15 and day(POPurchnDate) <= 31) order by POPurchnDate desc) else " +
                    "               (select top 1 harga from HargaBcS where nama like 'solar%' order by POPurchnDate desc) end Price,'15-31' Range, '0' Rowstatus " +
                    "               union all " +
                    "               select '3' id,'Bco' Nmpelumas, " +
                    "               case when(select count(harga) from HargaBcS where nama like 'bco%' and(day(POPurchnDate) >= 1 and day(POPurchnDate) <= 14)) > 0 then " +
                    "               (select top 1 harga from HargaBcS where nama like 'bco%' and(day(POPurchnDate) >= 1 and day(POPurchnDate) <= 14) order by POPurchnDate desc) else " +
                    "               (select top 1 harga from HargaBcS where nama like 'bco%' order by POPurchnDate desc)end Price,'1-14' Range, '0' Rowstatus " +
                    "               union all " +
                    "               select '4' id,'Bco' Nmpelumas, " +
                    "               case when(select count(harga) from HargaBcS where nama like 'bco%' and(day(POPurchnDate) >= 15 and day(POPurchnDate) <= 31)) > 0 then " +
                    "               (select top 1 harga from HargaBcS where nama like 'bco%' and(day(POPurchnDate) >= 15 and day(POPurchnDate) <= 31) order by POPurchnDate desc) else " +
                    "               (select top 1 harga from HargaBcS where nama like 'bco%' order by POPurchnDate desc)end Price,'15-31' Range, '0' Rowstatus ) ss " +

                    "select* into PelumasPlatTemp from ( " +
                    "   select Tanggal, Tanggal2, QtyBco, QtySolar, OutPutM3NP, OutPutM3, Harga from (" +
                    "       select Tanggal, Tanggal2, sum(QtyBco)QtyBco, sum(QtySolar)QtySolar," +
                    "       sum(OutPutM3NP)OutPutM3NP, sum(OutPutM3)OutPutM3, sum(harga)Harga from(" +
                    "           select Tanggal, Tanggal2, QtyBco, QtySolar, OutPutM3NP, OutPutM3, Harga from(" +
                    "               select LEFT(convert(char, A.date, 106), 11)Tanggal, A.Date Tanggal2," +
                    "               ISNULL(C.Quantity, 0)'QtyBco', '0'QtySolar, '0' OutPutM3NP," +
                    "               isnull(sum(((B.Qty) * (B.Volume))), 0) 'OutPutM3'," +
                    "				case when ISNULL(C.Quantity, 0) > 0 then  " +
                    "					case when day(A.date) >= 1 and day(A.date) <= 14 then" +
                    "                      (Select Price from BM_PlatHarga where [Range] = '1-14' and NmPelumas = 'Bco')  " +
                    "					   else " +
                    "                      (Select Price from BM_PlatHarga where [Range] = '15-31' and NmPelumas = 'Bco') end  " +
                    "				else 0 end harga " +
                    "               from DaysInMonth A " +
                    "               LEFT JOIN OutPutM3 B ON A.[Date] = B.TglProduksi " +
                    "               left Join PakaiBco C on C.PakaiDate = A.Date " +
                    "               Left Join HargaBcS Cb1 on Cb1.POPurchnDate = A.Date " +
                    "               where month(date) = month(@Date)  group by A.Date, C.Quantity, Cb1.Harga " +
                    "               union all" +
                    "               select LEFT(convert(char, A1.date, 106), 11)Tanggal, A1.Date Tanggal2, '0'QtyBco, ISNULL(C01.Quantity, 0)'QtySolar' " +
                    "               ,isnull(sum(((D.Qty) * (D.Volume))), 0) 'OutPutM3NP', '0' OutPutM3, " +
                    "				case when ISNULL(C01.Quantity, 0) > 0 then " +
                    "				    case when day(A1.date) >= 1 and day(A1.date) <= 14 then " +
                    "                      (Select Price from BM_PlatHarga where [Range] = '1-14' and NmPelumas = 'Solar')   " +
                    "					   else  " +
                    "                      (Select Price from BM_PlatHarga where [Range] = '15-31' and NmPelumas = 'Solar') end  " +
                    "				else 0 end harga " +
                    "               from DaysInMonth A1 " +
                    "               LEFT JOIN OutPutM3NP D ON A1.[Date] = D.TglProduksi " +
                    "               left Join PakaiSolar C01 on C01.PakaiDate = A1.Date " +
                    "               Left Join HargaBcS Cb1 on Cb1.POPurchnDate = A1.Date " +
                    "               where month(date) = month(@Date)  group by A1.Date, C01.Quantity, Cb1.Harga " +
                    "           ) as xx group by Tanggal,Tanggal2,QtyBco,QtySolar,OutPutM3NP,OutPutM3,Harga " +
                    "	    ) as xx1 group by tanggal,Tanggal2 " +
                    "   ) as xx2 " +
                    ") as xx3 " +

                    "select* from  ( " +
                    "   select 'A' urt, Tanggal, QtyBco, QtySolar, OutPutM3NP, OutPutM3, isnull(((QtyBco * (Select Price from BM_PlatHarga where [Range] = '1-14' and NmPelumas = 'Bco') / (Select Price from BM_PlatHarga where [Range] = '1-14' and NmPelumas = 'Solar')) +(QtySolar * (Select Price from BM_PlatHarga where[Range] = '1-14' and NmPelumas = 'Solar')/ (Select Price from BM_PlatHarga where[Range] = '1-14' and NmPelumas = 'Solar'))) / nullif(OutPutM3, 0) ,0)LtrMtr,Harga from PelumasPlatTemp where day(tanggal) >= 1 and day(tanggal) <= 14 " +
                    "   union all " +
                    "   select 'B' urt, '' Tanggal, sum(QtyBco)QtyBco, sum(QtySolar)QtySolar, '0'OutPutM3NP, '0'OutPutM3, '0'LtrMtr, '0'Harga from PelumasPlatTemp where day(tanggal) >= 1 and day(tanggal) <= 14 " +
                    "   union all " +
                    "   select 'C' urt, Tanggal, QtyBco, QtySolar, OutPutM3NP, OutPutM3, isnull(((QtyBco * (Select Price from BM_PlatHarga where [Range] = '15-31' and NmPelumas = 'Bco') / (Select Price from BM_PlatHarga where [Range] = '15-31' and NmPelumas = 'Solar')) +(QtySolar * (Select Price from BM_PlatHarga where[Range] = '15-31' and NmPelumas = 'Solar')/ (Select Price from BM_PlatHarga where[Range] = '15-31' and NmPelumas = 'Solar'))) / nullif(OutPutM3, 0) ,0)LtrMtr,Harga from PelumasPlatTemp where day(tanggal) >= 15 and day(tanggal) <= 31 " +
                    "   union all " +
                    "   select 'D' urt, '' Tanggal2, sum(QtyBco)QtyBco, sum(QtySolar)QtySolar, '0'OutPutM3NP, '0'OutPutM3, '0'LtrMtr, '0'Harga from PelumasPlatTemp where day(tanggal) >= 15 and day(tanggal) <= 31 " +
                    "   union all " +
                    "   select 'E' urt, 'TOTAL' Tanggal, sum(QtyBco)QtyBco, sum(QtySolar)QtySolar, sum(OutPutM3NP)OutPutM3NP, sum(OutPutM3)OutPutM3, " +
                    "   isnull(cast(((cast(((select sum(QtyBco) from PelumasPlatTemp where day(tanggal)>=1 and day(tanggal)<=14) *  " +
                    "   nullif((Select Price from BM_PlatHarga where [Range] = '1-14' and NmPelumas = 'Bco'),0)/  " +
                    "   nullif((Select Price from BM_PlatHarga where [Range] = '1-14' and NmPelumas = 'Solar'),0) +  " +
                    "   (select sum(QtySolar) from PelumasPlatTemp where day(tanggal) >= 1 and day(tanggal) <= 14) *  " +
                    "   nullif((Select Price from BM_PlatHarga where [Range] = '1-14' and NmPelumas = 'Solar'),0)/  " +
                    "   nullif((Select Price from BM_PlatHarga where [Range] = '1-14' and NmPelumas = 'Solar'),0)) as decimal (18,2))+  " +

                    "   cast(((select sum(QtyBco) from PelumasPlatTemp where day(tanggal) >= 15 and day(tanggal) <= 31) *  " +
                    "   nullif((Select Price from BM_PlatHarga where [Range] = '15-31' and NmPelumas = 'Bco'),0)/  " +
                    "   nullif((Select Price from BM_PlatHarga where [Range] = '15-31' and NmPelumas = 'Solar'),0) +  " +
                    "   (select sum(QtySolar) from PelumasPlatTemp where day(tanggal) >= 15 and day(tanggal) <= 31) *  " +
                    "   nullif((Select Price from BM_PlatHarga where [Range] = '15-31' and NmPelumas = 'Solar'),0)/  " +
                    "   nullif((Select Price from BM_PlatHarga where [Range] = '15-31' and NmPelumas = 'Solar'),0)) as decimal(18,2))  " +
                    "   )/ nullif(sum(OutPutM3), 0))as decimal(18,5)),0)LtrMtr,  " +
                    "   '0'Harga from PelumasPlatTemp " +
                    ") as xx4 order by urt,Tanggal";
        }
        else
        {
            Result = "IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PelumasPlatTemp]') AND type in (N'U')) DROP TABLE[dbo].PelumasPlatTemp  " +
                    "IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HargaBcS]') AND type in (N'U')) DROP TABLE[dbo].HargaBcS " +
                    "IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OutPutM3NP]') AND type in (N'U')) DROP TABLE[dbo].OutPutM3NP " +
                    "IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OutPutM3]') AND type in (N'U')) DROP TABLE[dbo].OutPutM3 " +
                    "IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PakaiBco]') AND type in (N'U')) DROP TABLE[dbo].PakaiBco " +
                    "IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PakaiSolar]') AND type in (N'U')) DROP TABLE[dbo].PakaiSolar " +
                    "IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DaysInMonth]') AND type in (N'U')) DROP TABLE[dbo].DaysInMonth " +
                    "IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BM_PlatHarga]') AND type in (N'U')) DROP TABLE[dbo].BM_PlatHarga " +
                    "declare @date datetime " +
                    "declare @line char " +
                    "set @date = '" + Periode + "' " +
                    "declare @date1 varchar(max) " +
                    "set @date1 = substring(convert(char, @date, 112), 1, 6) " +
                    "set @line = '" + Line + "' " +
                    ";with DaysInMont as (select @date as Date " +
                    "union all " +
                    "select dateadd(dd,1, Date)  from DaysInMont  where month(date) = month(@Date))  " +
                    "select * into DaysInMonth from DaysInMont " +

                    "select * into PakaiSolar from (select C.Quantity, A.PakaiDate from Pakai A " +
                    "                INNER JOIN PakaiDetail C ON A.ID = C.PakaiID " +
                    "                where LEFT(convert(char, A.pakaidate, 112), 6) = @date1 and C.ItemID   " +
                    "                in (select ID from Inventory where ItemCode = '112102001040300' and aktif = 1 and C.rowstatus > -1) " +
                    "                and C.ProdLine = @line  and A.Status = 3  )z " +

                    "select * into PakaiBco from (select C0.Quantity, A0.PakaiDate from Pakai A0 " +
                    "                INNER JOIN PakaiDetail C0 ON A0.ID = C0.PakaiID " +
                    "                where LEFT(convert(char, A0.pakaidate, 112), 6) = @date1 and C0.ItemID  " +
                    "                in (select ID from Inventory where ItemCode = '020140001010000' and aktif = 1 and C0.rowstatus > -1)  " +
                    "                and C0.ProdLine = @line  and A0.Status = 3 )z0 " +

                    "select * into OutPutM3 from (select C1.Qty, LEFT(convert(char, C1.TglProduksi, 112), 8)TglProduksi, ((C2.Tebal * C2.Lebar * C2.Panjang) / 1000000000)Volume from BM_Destacking C1 " +
                    "                INNER JOIN fc_items C2 ON C1.ItemID = C2.ID " +
                    "                INNER JOIN BM_PlantGroup C3 ON C1.PlantGroupID = C3.ID " +
                    "                where LEFT(convert(char, tglproduksi, 112), 6) = @date1 and C3.PlantID = @line  and C1.rowstatus > -1 and c2.Pressing != 'NO')z1 " +

                    "select* into OutPutM3NP from (select C1.Qty, LEFT(convert(char, C1.TglProduksi, 112), 8)TglProduksi, ((C2.Tebal * C2.Lebar * C2.Panjang) / 1000000000)Volume from BM_Destacking C1 " +
                    "                        INNER JOIN fc_items C2 ON C1.ItemID = C2.ID " +
                    "                INNER JOIN BM_PlantGroup C3 ON C1.PlantGroupID = C3.ID " +
                    "                where LEFT(convert(char, tglproduksi, 112), 6) = @date1 and C3.PlantID = @line  and C1.rowstatus > -1 and c2.Pressing = 'NO')z2 " +

                    "select* into HargaBcS from (select * from((select Ex.ItemName Nama, Ax.Price Harga, Cx.POPurchnDate from POPurchnDetail Ax " +
                    "                inner join POPurchn Cx on Ax.POID = Cx.ID " +
                    "                inner join Inventory Ex on Ax.ItemID = Ex.ID " +
                    "                where Ax.ItemID in (select ID from Inventory where ItemCode = '020140001010000') " +
                    "                and LEFT(convert(char, Cx.POPurchnDate, 112), 6) = @date1 and cx.Status > -1)  " +
                    "                union all " +
                    "                (select Ex1.ItemName Nama, Ax1.Price Harga, Cx1.POPurchnDate from POPurchnDetail Ax1 " +
                    "                inner join POPurchn Cx1 on Ax1.POID = Cx1.ID " +
                    "                inner join Inventory Ex1 on Ax1.ItemID = Ex1.ID " +
                    "                where Ax1.ItemID in (select ID from Inventory where ItemCode = '112102001040300') " +
                    "                and LEFT(convert(char, Cx1.POPurchnDate, 112), 6)= @date1 and Cx1.Status > -1) " +
                    "                union all " +
                    "                (select top 1 Ex.ItemName Nama, Ax.Price Harga, Cx.POPurchnDate from POPurchnDetail Ax " +
                    "                inner join POPurchn Cx on Ax.POID = Cx.ID " +
                    "                inner join Inventory Ex on Ax.ItemID = Ex.ID " +
                    "                where Ax.ItemID in (select ID from Inventory where ItemCode = '020140001010000') " +
                    "                and LEFT(convert(char, Cx.POPurchnDate, 112), 6)< @date1 and cx.Status > -1 order by Cx.POPurchnDate desc) " +
                    "                union all " +
                    "                (select top 1 Ex1.ItemName Nama, Ax1.Price Harga, Cx1.POPurchnDate from POPurchnDetail Ax1 " +
                    "                inner join POPurchn Cx1 on Ax1.POID = Cx1.ID " +
                    "                inner join Inventory Ex1 on Ax1.ItemID = Ex1.ID " +
                    "                where Ax1.ItemID in (select ID from Inventory where ItemCode = '112102001040300') " +
                    "                and LEFT(convert(char, Cx1.POPurchnDate, 112), 6)< @date1 and Cx1.Status > -1 order by Cx1.POPurchnDate desc))a)x " +

                    "select* into BM_PlatHarga from( " +
                    "               select '1' id, 'Solar' Nmpelumas, " +
                    "               case when(select count(harga) from HargaBcS where nama like 'solar%' and(day(POPurchnDate) >= 1 and day(POPurchnDate) <= 15)) > 0 then " +
                    "               (select top 1 harga from HargaBcS where nama like 'solar%' and(day(POPurchnDate) >= 1 and day(POPurchnDate) <= 15) order by POPurchnDate desc)  " +
                    "	            else(select top 1 harga from HargaBcS where nama like 'solar%' order by POPurchnDate desc)	 end Price,'1-15' Range, '0' Rowstatus " +
                    "               union all " +
                    "               select '2' id,'Solar' Nmpelumas, " +
                    "               case when(select count(harga) from HargaBcS where nama like 'Solar%' and(day(POPurchnDate) >= 16 and day(POPurchnDate) <= 31)) > 0 then " +
                    "               (select top 1 harga from HargaBcS where nama like 'Solar%' and(day(POPurchnDate) >= 16 and day(POPurchnDate) <= 31) order by POPurchnDate desc) else " +
                    "               (select top 1 harga from HargaBcS where nama like 'solar%' order by POPurchnDate desc) end Price,'16-31' Range, '0' Rowstatus " +
                    "               union all " +
                    "               select '3' id,'Bco' Nmpelumas, " +
                    "               case when(select count(harga) from HargaBcS where nama like 'bco%' and(day(POPurchnDate) >= 1 and day(POPurchnDate) <= 15)) > 0 then " +
                    "               (select top 1 harga from HargaBcS where nama like 'bco%' and(day(POPurchnDate) >= 1 and day(POPurchnDate) <= 15) order by POPurchnDate desc) else " +
                    "               (select top 1 harga from HargaBcS where nama like 'bco%' order by POPurchnDate desc)end Price,'1-15' Range, '0' Rowstatus " +
                    "               union all " +
                    "               select '4' id,'Bco' Nmpelumas, " +
                    "               case when(select count(harga) from HargaBcS where nama like 'bco%' and(day(POPurchnDate) >= 16 and day(POPurchnDate) <= 31)) > 0 then " +
                    "               (select top 1 harga from HargaBcS where nama like 'bco%' and(day(POPurchnDate) >= 16 and day(POPurchnDate) <= 31) order by POPurchnDate desc) else " +
                    "               (select top 1 harga from HargaBcS where nama like 'bco%' order by POPurchnDate desc)end Price,'16-31' Range, '0' Rowstatus ) ss " +

                    "select* into PelumasPlatTemp from ( " +
                    "   select Tanggal, Tanggal2, QtyBco, QtySolar, OutPutM3NP, OutPutM3, Harga from (" +
                    "       select Tanggal, Tanggal2, sum(QtyBco)QtyBco, sum(QtySolar)QtySolar," +
                    "       sum(OutPutM3NP)OutPutM3NP, sum(OutPutM3)OutPutM3, sum(harga)Harga from(" +
                    "           select Tanggal, Tanggal2, QtyBco, QtySolar, OutPutM3NP, OutPutM3, Harga from(" +
                    "               select LEFT(convert(char, A.date, 106), 11)Tanggal, A.Date Tanggal2," +
                    "               ISNULL(C.Quantity, 0)'QtyBco', '0'QtySolar, '0' OutPutM3NP," +
                    "               isnull(sum(((B.Qty) * (B.Volume))), 0) 'OutPutM3'," +
                    "				case when ISNULL(C.Quantity, 0) > 0 then  " +
                    "					case when day(A.date) >= 1 and day(A.date) <= 15 then" +
                    "                      (Select Price from BM_PlatHarga where [Range] = '1-15' and NmPelumas = 'Bco')  " +
                    "					   else " +
                    "                      (Select Price from BM_PlatHarga where [Range] = '16-31' and NmPelumas = 'Bco') end  " +
                    "				else 0 end harga " +
                    "               from DaysInMonth A " +
                    "               LEFT JOIN OutPutM3 B ON A.[Date] = B.TglProduksi " +
                    "               left Join PakaiBco C on C.PakaiDate = A.Date " +
                    "               Left Join HargaBcS Cb1 on Cb1.POPurchnDate = A.Date " +
                    "               where month(date) = month(@Date)  group by A.Date, C.Quantity, Cb1.Harga " +
                    "               union all" +
                    "               select LEFT(convert(char, A1.date, 106), 11)Tanggal, A1.Date Tanggal2, '0'QtyBco, ISNULL(C01.Quantity, 0)'QtySolar' " +
                    "               ,isnull(sum(((D.Qty) * (D.Volume))), 0) 'OutPutM3NP', '0' OutPutM3, " +
                    "				case when ISNULL(C01.Quantity, 0) > 0 then " +
                    "				    case when day(A1.date) >= 1 and day(A1.date) <= 15 then " +
                    "                      (Select Price from BM_PlatHarga where [Range] = '1-15' and NmPelumas = 'Solar')   " +
                    "					   else  " +
                    "                      (Select Price from BM_PlatHarga where [Range] = '16-31' and NmPelumas = 'Solar') end  " +
                    "				else 0 end harga " +
                    "               from DaysInMonth A1 " +
                    "               LEFT JOIN OutPutM3NP D ON A1.[Date] = D.TglProduksi " +
                    "               left Join PakaiSolar C01 on C01.PakaiDate = A1.Date " +
                    "               Left Join HargaBcS Cb1 on Cb1.POPurchnDate = A1.Date " +
                    "               where month(date) = month(@Date)  group by A1.Date, C01.Quantity, Cb1.Harga " +
                    "           ) as xx group by Tanggal,Tanggal2,QtyBco,QtySolar,OutPutM3NP,OutPutM3,Harga " +
                    "	    ) as xx1 group by tanggal,Tanggal2 " +
                    "   ) as xx2 " +
                    ") as xx3 " +

                    "select* from  ( " +
                    "   select 'A' urt, Tanggal, QtyBco, QtySolar, OutPutM3NP, OutPutM3, isnull(((QtyBco * (Select Price from BM_PlatHarga where [Range] = '1-15' and NmPelumas = 'Bco') / (Select Price from BM_PlatHarga where [Range] = '1-15' and NmPelumas = 'Solar')) +(QtySolar * (Select Price from BM_PlatHarga where[Range] = '1-15' and NmPelumas = 'Solar')/ (Select Price from BM_PlatHarga where[Range] = '1-15' and NmPelumas = 'Solar'))) / nullif(OutPutM3, 0) ,0)LtrMtr,Harga from PelumasPlatTemp where day(tanggal) >= 1 and day(tanggal) <= 15 " +
                    "   union all " +
                    "   select 'B' urt, '' Tanggal, sum(QtyBco)QtyBco, sum(QtySolar)QtySolar, '0'OutPutM3NP, '0'OutPutM3, '0'LtrMtr, '0'Harga from PelumasPlatTemp where day(tanggal) >= 1 and day(tanggal) <= 15 " +
                    "   union all " +
                    "   select 'C' urt, Tanggal, QtyBco, QtySolar, OutPutM3NP, OutPutM3, isnull(((QtyBco * (Select Price from BM_PlatHarga where [Range] = '16-31' and NmPelumas = 'Bco') / (Select Price from BM_PlatHarga where [Range] = '16-31' and NmPelumas = 'Solar')) +(QtySolar * (Select Price from BM_PlatHarga where[Range] = '16-31' and NmPelumas = 'Solar')/ (Select Price from BM_PlatHarga where[Range] = '16-31' and NmPelumas = 'Solar'))) / nullif(OutPutM3, 0) ,0)LtrMtr,Harga from PelumasPlatTemp where day(tanggal) >= 16 and day(tanggal) <= 31 " +
                    "   union all " +
                    "   select 'D' urt, '' Tanggal2, sum(QtyBco)QtyBco, sum(QtySolar)QtySolar, '0'OutPutM3NP, '0'OutPutM3, '0'LtrMtr, '0'Harga from PelumasPlatTemp where day(tanggal) >= 16 and day(tanggal) <= 31 " +
                    "   union all " +
                    "   select 'E' urt, 'TOTAL' Tanggal, sum(QtyBco)QtyBco, sum(QtySolar)QtySolar, sum(OutPutM3NP)OutPutM3NP, sum(OutPutM3)OutPutM3, " +
                    "   isnull(cast(((cast(((select sum(QtyBco) from PelumasPlatTemp where day(tanggal)>=1 and day(tanggal)<=15) *  " +
                    "   nullif((Select Price from BM_PlatHarga where [Range] = '1-15' and NmPelumas = 'Bco'),0)/ " +
                    "   nullif((Select Price from BM_PlatHarga where [Range] = '1-15' and NmPelumas = 'Solar'),0) + " +
                    "   (select sum(QtySolar) from PelumasPlatTemp where day(tanggal) >= 1 and day(tanggal) <= 15) * " +
                    "   nullif((Select Price from BM_PlatHarga where [Range] = '1-15' and NmPelumas = 'Solar'),0)/ " +
                    "   nullif((Select Price from BM_PlatHarga where [Range] = '1-15' and NmPelumas = 'Solar'),0)) as decimal (18,2))+ " +

                    "   cast(((select sum(QtyBco) from PelumasPlatTemp where day(tanggal) >= 16 and day(tanggal) <= 31) * " +
                    "   nullif((Select Price from BM_PlatHarga where [Range] = '16-31' and NmPelumas = 'Bco'),0)/ " +
                    "   nullif((Select Price from BM_PlatHarga where [Range] = '16-31' and NmPelumas = 'Solar'),0) + " +
                    "   (select sum(QtySolar) from PelumasPlatTemp where day(tanggal) >= 16 and day(tanggal) <= 31) * " +
                    "   nullif((Select Price from BM_PlatHarga where [Range] = '16-31' and NmPelumas = 'Solar'),0)/ " +
                    "   nullif((Select Price from BM_PlatHarga where [Range] = '16-31' and NmPelumas = 'Solar'),0)) as decimal(18,2))  " +
                    "   )/ nullif(sum(OutPutM3), 0))as decimal(18,5)),0)LtrMtr, " +
                    "   '0'Harga from PelumasPlatTemp " +
                    ") as xx4 order by urt,Tanggal";
        }

        return Result;
    }

    private string DataPlatJmbg(string Periode, string Periode2, int Line)
    {
        string Result =
                        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PelumasPlatTemp]') AND type in (N'U')) DROP TABLE [dbo].PelumasPlatTemp " +
                        " declare @date datetime  " +
                        " set @date =  '" + Periode + "'; " +

                        " with DaysInMonth as (select @date as Date " +
                        " union all  " +
                        " select dateadd(dd,1,Date)  from DaysInMonth  where month(date) = month(@Date)), " +

                        " PakaiSolar As (	select C.Quantity,A.PakaiDate from Pakai A " +
                                        " INNER JOIN PakaiDetail C ON A.ID=C.PakaiID " +
                                        " where LEFT(convert(char,A.pakaidate,112),6)='" + Periode2 + "' and C.ItemID  " +
                                        " in (select ID from Inventory where ItemCode='112102001040300' and aktif=1 and C.rowstatus>-1) " +
                                        " and C.ProdLine='" + Line + "' and A.Status=3  )," +

                        " PakaiBco As ( select sum(Quantity) Quantity, PakaiDate FROM (select C0.Quantity,A0.PakaiDate from Pakai A0   " +
                                        " INNER JOIN PakaiDetail C0 ON A0.ID=C0.PakaiID  " +
                                        " where LEFT(convert(char,A0.pakaidate,112),6)='" + Periode2 + "' and C0.ItemID " +
                                        " in (select ID from Inventory where ItemCode='020140001010000' and aktif=1 and C0.rowstatus>-1)" +
                                        " and C0.ProdLine='" + Line + "' and A0.Status=3) a GROUP by pakaidate)," +

                        " OutPutM3 As (  select C1.Qty,LEFT(convert(char,C1.TglProduksi,112),8)TglProduksi,((C2.Tebal*C2.Lebar*C2.Panjang)/1000000000)Volume from BM_Destacking C1  " +
                                        " INNER JOIN fc_items C2 ON C1.ItemID=C2.ID " +
                                        " INNER JOIN BM_PlantGroup C3 ON C1.PlantGroupID=C3.ID " +
                                        " where LEFT(convert(char,tglproduksi,112),6)='" + Periode2 + "' and C3.PlantID='" + Line + "' and C1.rowstatus>-1)," +

                        " HargaBcS As ( select Ex.ItemName Nama,Ax.Price Harga,Cx.POPurchnDate from POPurchnDetail Ax " +
                                        " inner join POPurchn Cx on Ax.POID=Cx.ID " +
                                        " inner join Inventory Ex on Ax.ItemID=Ex.ID " +
                                        " where Ax. ItemID in (select ID from Inventory where ItemCode='020140001010000') " +
                                        " and LEFT(convert(char,Cx.POPurchnDate,112),6)='" + Periode2 + "' " +
                                        " union all " +
                                        " select Ex1.ItemName Nama,Ax1.Price Harga,Cx1.POPurchnDate from POPurchnDetail Ax1 " +
                                        " inner join POPurchn Cx1 on Ax1.POID=Cx1.ID " +
                                        " inner join Inventory Ex1 on Ax1.ItemID=Ex1.ID " +
                                        " where Ax1.ItemID in (select ID from Inventory where ItemCode='112102001040300') " +
                                        " and LEFT(convert(char,Cx1.POPurchnDate,112),6)='" + Periode2 + "') " +

                        " select * into PelumasPlatTemp from ( " +
                        " select Tanggal,Tanggal2,QtyBco,QtySolar,OutPutM3,Harga " +
                        " from ( " +
                        " select Tanggal,Tanggal2,sum(QtyBco)QtyBco,sum(QtySolar)QtySolar,sum(OutPutM3)OutPutM3,Harga " +
                        " from ( " +
                        " select Tanggal,Tanggal2,QtyBco,QtySolar,OutPutM3,Harga " +
                        " from ( " +
                        " select LEFT(convert(char,A.date,106),11)Tanggal,A.Date Tanggal2, " +
                        " ISNULL(C.Quantity,0)'QtyBco','0'QtySolar " +
                        " ,isnull(sum(((B.Qty)* (B.Volume))),0) 'OutPutM3',isnull(Cb1.Harga,0)Harga " +
                        " from DaysInMonth A  " +
                        " LEFT JOIN OutPutM3 B ON A.[Date]=B.TglProduksi " +
                        " left Join PakaiBco C on C.PakaiDate=A.Date " +
                        " Left Join HargaBcS Cb1 on Cb1.POPurchnDate=A.Date " +
                        " where month(date) = month(@Date)  group by A.Date, C.Quantity,Cb1.Harga" +

                        " union all " +

                        " select LEFT(convert(char,A1.date,106),11)Tanggal,A1.Date Tanggal2, " +
                        " '0'QtyBco,ISNULL(C01.Quantity,0)'QtySolar' " +
                        " ,'0' OutPutM3,isnull(Cb1.Harga,0)Harga " +
                        " from DaysInMonth A1 " +
                        " LEFT JOIN OutPutM3 B1a ON A1.[Date]=B1a.TglProduksi " +
                        " left Join PakaiSolar C01 on C01.PakaiDate=A1.Date " +
                        " Left Join HargaBcS Cb1 on Cb1.POPurchnDate=A1.Date " +
                        " where month(date) = month(@Date)  group by A1.Date,B1a.Volume,C01.Quantity,Cb1.Harga " +

                        " ) as xx group by Tanggal,Tanggal2,QtyBco,QtySolar,OutPutM3,Harga) " +
                        " as xx1 group by tanggal,Tanggal2,Harga ) as xx2 ) as xx3 " +

                        " select * from  ( " +
                        " select 'A' urt, Tanggal,QtyBco,QtySolar,OutPutM3,isnull(((QtyBco * (Select Price from BM_PlatHarga where [Range]='1-15' and NmPelumas='Bco')/  (Select Price from BM_PlatHarga where [Range]='1-15' and NmPelumas='Solar')) + (QtySolar * (Select Price from BM_PlatHarga where [Range]='1-15' and NmPelumas='Solar')/  (Select Price from BM_PlatHarga where [Range]='1-15' and NmPelumas='Solar'))) / nullif(OutPutM3,0) ,0)LtrMtr,Harga from PelumasPlatTemp where day(tanggal)>=1 and day(tanggal)<=15 " +
                        " union all " +
                        " select 'B' urt,'' Tanggal,sum(QtyBco)QtyBco,sum(QtySolar)QtySolar,'0'OutPutM3,'0'LtrMtr,'0'Harga from PelumasPlatTemp where day(tanggal)>=1 and day(tanggal)<=15 " +
                        " union all " +
                        " select 'C' urt, Tanggal,QtyBco,QtySolar,OutPutM3,isnull(((QtyBco * (Select Price from BM_PlatHarga where [Range]='1-15' and NmPelumas='Bco')/  (Select Price from BM_PlatHarga where [Range]='1-15' and NmPelumas='Solar')) + (QtySolar * (Select Price from BM_PlatHarga where [Range]='1-15' and NmPelumas='Solar')/  (Select Price from BM_PlatHarga where [Range]='1-15' and NmPelumas='Solar'))) / nullif(OutPutM3,0) ,0)LtrMtr,Harga from PelumasPlatTemp where day(tanggal)>=16 and day(tanggal)<=31 " +
                        " union all " +
                        " select 'D' urt,'' Tanggal2,sum(QtyBco)QtyBco,sum(QtySolar)QtySolar,'0'OutPutM3,'0'LtrMtr,'0'Harga from PelumasPlatTemp where day(tanggal)>=16 and day(tanggal)<=31 " +
                        " union all " +
                        " select 'E' urt,'TOTAL' Tanggal,sum(QtyBco)QtyBco,sum(QtySolar)QtySolar,sum(OutPutM3)OutPutM3," +
                        " cast(((((select sum(QtyBco) from PelumasPlatTemp where day(tanggal)>=1 and day(tanggal)<=15) * " +
                        " (Select Price from BM_PlatHarga where [Range]='1-15' and NmPelumas='Bco') /" +
                        " (Select Price from BM_PlatHarga where [Range]='1-15' and NmPelumas='Solar')) + " +
                        " ((select sum(QtySolar) from PelumasPlatTemp where day(tanggal)>=1 and day(tanggal)<=15) * " +
                        " (Select Price from BM_PlatHarga where [Range] = '1-15' and NmPelumas = 'Solar') /" +
                        " (Select Price from BM_PlatHarga where[Range] = '1-15' and NmPelumas = 'Solar')) +" +
                        " ((select sum(QtyBco) from PelumasPlatTemp where day(tanggal)>=16 and day(tanggal)<=31) *" +
                        " (Select Price from BM_PlatHarga where [Range]='16-31' and NmPelumas='Bco') /" +
                        " (Select Price from BM_PlatHarga where [Range]='16-31' and NmPelumas='Solar')) + " +
                        " ((select sum(QtySolar) from PelumasPlatTemp where day(tanggal)>=16 and day(tanggal)<=31) * " +
                        " (Select Price from BM_PlatHarga where [Range] = '16-31' and NmPelumas = 'Solar') / " +
                        " (Select Price from BM_PlatHarga where[Range] = '16-31' and NmPelumas = 'Solar')) )/ sum(OutPutM3))as decimal(18,5))LtrMtr," +
                        " '0'Harga from PelumasPlatTemp " +
                        " ) as xx4 order by urt,Tanggal ";

        return Result;
    }

    private PlatDomain GenerateObjectX(SqlDataReader sdr)
    {
        PlatDomain objL = new PlatDomain();
        objL.Tanggal = sdr["Tanggal"].ToString();
        objL.QtySolar = Convert.ToDecimal(sdr["QtySolar"]);
        objL.QtyBco = Convert.ToDecimal(sdr["QtyBco"]);
        objL.OutPutM3 = Convert.ToDecimal(sdr["OutPutM3"]);
        objL.OutPutM3NP = Convert.ToDecimal(sdr["OutPutM3NP"]);
        objL.LtrMtr = Convert.ToDecimal(sdr["LtrMtr"]);
        objL.Harga = Convert.ToDecimal(sdr["Harga"]);
        return objL;
    }

    private PlatDomain GenerateObjectJmbg(SqlDataReader sdr)
    {
        PlatDomain objL = new PlatDomain();
        objL.Tanggal = sdr["Tanggal"].ToString();
        objL.QtySolar = Convert.ToDecimal(sdr["QtySolar"]);
        objL.QtyBco = Convert.ToDecimal(sdr["QtyBco"]);
        objL.OutPutM3 = Convert.ToDecimal(sdr["OutPutM3"]);
        objL.LtrMtr = Convert.ToDecimal(sdr["LtrMtr"]);
        objL.Harga = Convert.ToDecimal(sdr["Harga"]);
        return objL;
    }
}