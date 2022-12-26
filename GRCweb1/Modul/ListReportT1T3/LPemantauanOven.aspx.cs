using BusinessFacade;
using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Factory;
using System.IO;
using System.Web.UI.HtmlControls;

namespace GRCweb1.Modul.ListReportT1T3
{
    public partial class LPemantauanOven : System.Web.UI.Page
    {
        private ArrayList arrData = new ArrayList();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                getYear();
                LoadProduk();
            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }
        private void getYear()
        {
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
        private void LoadProduk()
        {

            Users user = ((Users)Session["Users"]);
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select * from rptoven where RowStatus>-1";
            SqlDataReader sdr = zl.Retrieve();
            ddlProduk.Items.Clear();
            ddlProduk.Items.Add(new ListItem("--Pilih--", "0"));
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlProduk.Items.Add(new ListItem(sdr["Judul"].ToString().ToUpper().Trim(), sdr["ID"].ToString().TrimEnd()));
                }
            }
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            loadpantau();
        }
        protected void loadpantau()
        {
            Users user = ((Users)Session["Users"]);

            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "declare @Judul nvarchar(Max) " +
                "declare @tgl nvarchar(Max) " +
                "set @judul='" + ddlProduk.SelectedItem.Text + "' " +
                "set @tgl='" + ddTahun.SelectedItem.Text.Trim() + ddlBulan.SelectedValue + "' " +
                "select tglserah,sum(TPotong)TPotong,sum(OK) OK,cast((sum(OK)/sum(TPotong))*100 as decimal(18,2)) persenOK,sum(bp) BP,cast((sum(BP)/sum(TPotong))*100 as decimal(18,2)) PersenBP from ( " +
                "select tglserah,sum(qtyin)TPotong,0 OK,0 BP from T1_Serah where left(convert(char,tglserah,112),6)=@tgl and Status>-1  " +
                "and itemid0 in (select id from fc_items where partno in (select partno from rptovenPartno where rptovenid in  " +
                "(select id from rptoven where judul=@judul))) group by tglserah  " +
                "union all " +
                "select tglserah,0 TPotong,sum(qtyin) OK,0 BP from T1_Serah where left(convert(char,tglserah,112),6)=@tgl and Status>-1  " +
                "and itemid0 in (select id from fc_items where partno in (select partno from rptovenPartno where rptovenid in  " +
                "(select id from rptoven where judul=@judul))) and itemid in (select id from fc_items where partno like '%-3-%' or partno like '%-M-%') group by tglserah " +
                "union all " +
                "select tglserah,0 TPotong,0 OK,sum(qtyin) BP from T1_Serah where left(convert(char,tglserah,112),6)=@tgl and Status>-1  " +
                "and itemid0 in (select id from fc_items where partno in (select partno from rptovenPartno where rptovenid in  " +
                "(select id from rptoven where judul=@judul))) and itemid in (select id from fc_items where partno like '%-P-%' )group by tglserah)a group by tglserah " +
                "order by tglserah";
            SqlDataReader sdr = zl.Retrieve();
            string tglMuat = string.Empty;
            ArrayList arrData = new ArrayList();
            RptOven rov = new RptOven();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    rov = new RptOven();
                    rov.TglSerah = DateTime.Parse(sdr["tglserah"].ToString());
                    rov.Tpotong = int.Parse(sdr["tpotong"].ToString());
                    rov.OK = int.Parse(sdr["OK"].ToString());
                    rov.PersenOK = Decimal.Parse(sdr["PersenOK"].ToString());
                    rov.BP = int.Parse(sdr["BP"].ToString());
                    rov.PersenBP = Decimal.Parse(sdr["PersenBP"].ToString());
                    arrData.Add(rov);
                }
            }
            ListProduk.DataSource = arrData;
            ListProduk.DataBind();
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=PemantauanOven.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<H3>PEMANTAUAN HASIL OVEN " + ddlProduk.SelectedItem.Text + "</H3>";
            //Html += "<br>Tanggal : " + ddlSemester.SelectedItem.Text + " &nbsp; " + ddlTahun.SelectedValue.ToString();
            Html += "<br>Periode : &nbsp; " + ddlBulan.SelectedItem.Text + " " + ddTahun.SelectedItem.Text;
            //Html += "<br>Departement : &nbsp;" + ddlDept.SelectedItem.Text;
            Html += "";
            string HtmlEnd = "";
            div2.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0\"", "border=\"1\"");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }

        protected void ddlBulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadpantau();
        }
        public decimal tPotong = 0;
        public decimal OK = 0;
        public decimal persenOK = 0;
        public decimal BP = 0;
        public decimal persenBP = 0;
        protected void ListProduk_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RptOven rov = (RptOven)e.Item.DataItem;
            HtmlTableRow tr1 = (HtmlTableRow)e.Item.FindControl("ps1");
            if (tr1 != null)
            {
                tPotong += rov.Tpotong;
                OK += rov.OK;
                persenOK = OK / tPotong * 100;
                BP += rov.BP;
                persenBP = BP / tPotong * 100;
            }

            if (e.Item.ItemType == ListItemType.Footer)
            {
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("ftr");
                tr.Cells[1].InnerText = tPotong.ToString();
                tr.Cells[2].InnerText = OK.ToString();
                tr.Cells[3].InnerText = persenOK.ToString("N2");
                tr.Cells[4].InnerText = BP.ToString();
                tr.Cells[5].InnerText = persenBP.ToString("N2");
            }
        }

        protected void ddTahun_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadpantau();
        }

        protected void ddlProduk_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadpantau();
        }
    }
}
public class RptOven : GRCBaseDomain
{
    public DateTime TglSerah { get; set; }
    public int Tpotong { get; set; }
    public int OK { get; set; }
    public Decimal PersenOK { get; set; }
    public int BP { get; set; }
    public Decimal PersenBP { get; set; }
}