using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.IO;
using Domain;
using BusinessFacade;
using DataAccessLayer;

namespace GRCweb1.Modul.SarMut
{
    public partial class RekapPulp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Users user = (Users)Session["Users"];
                LoadBulan();
                LoadTahun();
                #region Header Line
                line1.Text = "Line 1";
                line2.Text = "Line 2";
                line3.Text = "Line 3";
                line4.Text = "Line 4";
                line5.Text = "Line 5";
                line6.Text = "Line 6";
                if (user.UnitKerjaID == 1)
                {
                    lblF1.Text = "G185";
                    lblF2.Text = "G206";
                    frmula1.Text = (75.0).ToString("N0");
                    frmula1a.Text = (79.0).ToString("N0");
                }
                else if (user.UnitKerjaID == 7)
                {
                    lblF1.Text = "G185";
                    lblF2.Text = "G206";
                    frmula1.Text = (90.0).ToString("N0");
                    frmula1a.Text = (79.0).ToString("N0");
                }
                else
                {
                    lblF1.Text = "G208";
                    lblF2.Text = "G208";
                    frmula1.Text = (90.0).ToString("N0");
                    frmula1a.Text = (90.0).ToString("N0");
                }
                frmula2.Text = (90.0).ToString("N0");
                frmula2a.Text = (79.0).ToString("N0");
                frmula3.Text = (90.0).ToString("N0");
                frmula3a.Text = (79.0).ToString("N0");
                frmula4.Text = (90.0).ToString("N0");
                frmula4a.Text = (79.0).ToString("N0");
                frmula5.Text = (90.0).ToString("N0");
                frmula5a.Text = (79.0).ToString("N0");
                frmula6.Text = (90.0).ToString("N0");
                frmula6a.Text = (79.0).ToString("N0");
                #endregion
            }
         ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('zib', 500, 150 , 40 ,false); </script>", false);
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
            ddlBulan.Items.Add(new ListItem("--Pilih Bulan--", "0"));
            for (int i = 1; i < 13; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i).ToString(), i.ToString()));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            LoadData();
            Response.AddHeader("content-disposition", "attachment;filename=PemantauanPemakaianKertas.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<b>REPORT EFFESIENSI BAHAN BAKU PULP</b>";
            Html += "<br>Periode : " + ddlBulan.SelectedItem.Text + "  " + ddlTahun.SelectedValue.ToString();
            string HtmlEnd = "";
            lst.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("xx\">", "\">\'");
            Contents = Contents.Replace("border=\"0", "border=\"1");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }

        protected void btnForm_Click(object sender, EventArgs e)
        {
            Response.Redirect("PbahanBaku3.aspx");
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void lstPMX_Command(object sender, RepeaterCommandEventArgs e)
        {

        }

        protected void lstPMX_Databound(object sender, RepeaterItemEventArgs e)
        {

            #region Yang Lama
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                PbahanBaku p = (PbahanBaku)e.Item.DataItem;
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {

            }
            #endregion

        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private void LoadData()
        {

            Users user = (Users)Session["Users"];
            ArrayList arrData = new ArrayList();
            PbahanBakuFacade pbb = new PbahanBakuFacade();
            if (user.UnitKerjaID == 1)
            {
                arrData = pbb.RetrievePulpCtrp(ddlBulan.SelectedValue, ddlTahun.SelectedValue);
                lstPMX.DataSource = arrData;
                lstPMX.DataBind();
            }
            else if (user.UnitKerjaID == 7)
            {
                arrData = pbb.RetrievePulpKrwg(ddlBulan.SelectedValue, ddlTahun.SelectedValue);
                lstPMX.DataSource = arrData;
                lstPMX.DataBind();
            }
            else
            {
                arrData = pbb.RetrievePulpJombang(ddlBulan.SelectedValue, ddlTahun.SelectedValue);
                lstPMX.DataSource = arrData;
                lstPMX.DataBind();
            }

            #region update sarmut Effesiensi Pemakaian Kertas
            string Queryx = string.Empty;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = Queryx;
            SqlDataReader sdr = zl.Retrieve();

            #region update sarmut Boardmill
            string sarmutPrs = "Effesiensi Pemakaian Kertas";
            string strDept = string.Empty;
            int deptid = getDeptID("BOARD");
            #endregion
            #region #1
            decimal actual = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            //zl.CustomQuery = "select isnull(cast((((sum(KvKg) + sum(KsKg) + sum(KkKg) + sum(Kbtkg2)) -sum(Formula)) / (sum(Formula)) * 100)as decimal(18,1)),0)actual from TempPulp";
            zl.CustomQuery = " select isnull(cast((((sum(KvKg2) + sum(KsKg) + sum(KuKg) + sum(Kgrade2Kg)+ sum(Kgrade3Kg)) -sum(Formula)) / (sum(Formula)) * 100)as decimal(18,1)),0)actual from TempPulp";
            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actual = Decimal.Parse(sdr["actual"].ToString());
                }
            }
            #endregion
            #region #2
            ZetroView zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery =
                "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                " set @bulan=" + ddlBulan.SelectedIndex + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                "update SPD_TransPrs set actual=" + actual.ToString().Replace(",", ".") + " where Approval=0 and tahun=@tahun and bulan=@bulan " +
                " and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "')";
            SqlDataReader sdr1 = zl1.Retrieve();
            #endregion
            #region #3
            //hitung average untuk spd_perusahaan
            actual = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                " set @bulan=" + ddlBulan.SelectedIndex + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                "select isnull(cast(avg(actual) as decimal(18,2)),0) actual from SPD_TransPrs where Approval=0 and tahun=@tahun and bulan=@bulan " +
                " and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "') and actual>0";
            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actual = Decimal.Parse(sdr["actual"].ToString());
                }
            }
            #endregion
            #region #4
            //zl1 = new ZetroView();
            //zl1.QueryType = Operation.CUSTOM;

            //zl1.CustomQuery =
            //    "update SPD_TransPrs set actual= " + actual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddlTahun.SelectedValue +
            //     " and Bulan=" + ddlBulan.SelectedIndex +
            //     " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "') ";
            //sdr1 = zl1.Retrieve();
            #endregion
            #endregion
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
    }
}