using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Domain;
using BusinessFacade;
using DataAccessLayer;

namespace GRCweb1.Modul.ListReport
{
    public partial class PemantauanMinMax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadBulan();
                LoadTahun();
            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }
        private void LoadBulan()
        {
            ddlBulan.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i), i.ToString()));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
        }
        private void LoadTahun()
        {
            ArrayList arrData = new ArrayList();
            Minmax mm = new Minmax();
            mm.Tabel = "Tahun";
            arrData = mm.Retrieve();
            ddlTahun.Items.Clear();
            foreach (Mmax mn in arrData)
            {
                ddlTahun.Items.Add(new ListItem(mn.Tahun.ToString(), mn.Tahun.ToString()));
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            ArrayList arrData = new ArrayList();
            Minmax mm = new Minmax();
            mm.bln = int.Parse(ddlBulan.SelectedValue.ToString());
            mm.tahun = int.Parse(ddlTahun.SelectedValue.ToString());
            mm.GroupID = ddlGroup.SelectedValue.ToString();
            mm.Tabel = "Data";
            arrData = mm.Retrieve();
            lstMinMax.DataSource = arrData;
            lstMinMax.DataBind();
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=PemantauanMinMax.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "Pemantauan Min Max (Spare Part)";
            Html += "<br>Periode : " + ddlBulan.SelectedItem + " " + ddlTahun.SelectedValue;
            Html += "";
            string HtmlEnd = "";
            //lstForPrint.RenderControl(hw);
            lst.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0\"", "border=\"1\"");
            Contents = Contents.Replace("xx\">", "\">'");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }
    }
}

public class Minmax
{
    private ArrayList arrData = new ArrayList();
    private Mmax objM = new Mmax();
    public string Criteria { get; set; }
    public string Field { get; set; }
    public string Tabel { get; set; }
    public string GroupID { get; set; }
    public int bln { get; set; }
    public int tahun { get; set; }
    public string periodAwal { get; set; }
    private string BlnFld(int i)
    {
        string blnfld = ",JanQty,FebQty,MarQty,AprQty,MeiQty,JunQty,JulQty,AguQty,SepQty,OktQty,NovQty,DesQty";
        string[] bln = blnfld.Split(',');
        return ((i - 1) > 0) ? bln[i - 1] : bln[12];
    }
    public string Query()
    {
        string query = string.Empty;
        string blne = ((this.bln - 1) > 0) ? (this.bln - 1).ToString().PadLeft(2, '0') : "12";
        string thne = ((this.bln == 1)) ? (this.tahun - 1).ToString() : this.tahun.ToString();
        switch (this.Tabel)
        {
            case "Tahun":
                query = "Select distinct Tahun from vw_StockPurchn where tahun >0 order by tahun";
                break;
            case "Data":
                query = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[test]') AND type in (N'U')) DROP TABLE [dbo].[test] " +
                        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[test1]') AND type in (N'U')) DROP TABLE [dbo].[test1] " +
                        "select ROW_NUMBER() over (Partition by ItemID order by IDn,Tanggal)ID, * into test from( " +
                        "select *,(SaldoAwal+Transaksi)SaldoAkhir, " +
                        "Case when (SaldoAwal+Transaksi)< MinStock then 1 else 0 end inMin, " +
                        "Case when (SaldoAwal+Transaksi)> MaxStock then 1 else 0 end inMax from( " +
                        "select 0 as IDn,'" + thne + "-" + this.bln.ToString().PadLeft(2, '0') + "-01' as Tanggal,ItemID,(select dbo.ItemCodeInv(ItemID,1))ItemCode,(select dbo.ItemNameInv(ItemID,1))Itemname, " +
                        "1 as Stoke,(select dbo.GetInventory(ItemID,1,'MinStock')) as MinStock, " +
                        "(select dbo.GetInventory(ItemID,1,'MaxStock')) as MaxStock," + this.BlnFld(this.bln) + " as SaldoAwal, " +
                        "0 as transaksi,GroupID from SaldoInventory where YearPeriod=" + thne + " and ItemID in( " +
                        "select ID from Inventory where GroupID in(" + this.GroupID + ") and Aktif=1 and Stock=1)) as m " +
                        "Union all " +
                        "( " +
                        "select 1 as IDn, *,(SaldoAwal+Transaksi)SaldoAkhir, " +
                        "Case when (SaldoAwal+Transaksi)< MinStock then 1 else 0 end inMin, " +
                        "Case when (SaldoAwal+Transaksi)> MaxStock then 1 else 0 end inMax from( " +
                        "select Tanggal,ItemID, (select dbo.ItemCodeInv(sp.ItemID,1))Itemcode, " +
                        "(select dbo.ItemNameInv(sp.ItemID,1))ItemName, " +
                        "(select Stock from Inventory where ID=sp.ItemID)Stoke, " +
                        "(select MinStock from Inventory where ID=sp.ItemID and ItemTypeID=1)MinStock, " +
                        "(select MaxStock from Inventory where ID=sp.ItemID and ItemTypeID=1)MaxStock,0 as SaldoAwal, " +
                        "SUM(quantity)Transaksi,GroupID from vw_StockPurchn sp where GroupID in(" + this.GroupID + ") and YM='" + this.tahun.ToString() + this.bln.ToString().PadLeft(2, '0') + "' " +
                        "group by ItemID,Tanggal,GroupID) as m " +
                        "where Stoke=1)) as x " +
                        "order by ID,itemcode,ItemName,tanggal " +
                        "select ROW_NUMBER()Over(order by ItemID)IDn,ItemID into test1 from test group by ItemID ; " +
                        "declare @i int; " +
                        "declare @c int; " +
                        "declare @countItem int " +
                        "declare @itm int " +
                        "declare @itemid int " +
                        "declare @sa decimal(18,2) " +
                        "declare @sk decimal(18,2) " +
                        "declare @ts decimal(18,2) " +
                        "set @itm=1 " +
                        "set @countItem=(select COUNT(IDn) from test1) " +
                        "while @itm <=@countItem " +
                        " begin " +
                        "    set @itemid=(select ItemID from test1 where IDn=@itm); " +
                        "    set @i=2; " +
                        "    set @c=(select COUNT(ID) from test where ItemID=@itemid); " +
                        "    if @c>=@i " +
                        "     begin " +
                        "      while @i <= @c " +
                        "      begin " +
                        "       Update test set SaldoAwal=(select SaldoAwal+transaksi from test where ID=@i-1 and ItemID=@itemid) where ID=@i and ItemID=@itemid " +
                        "       Update test set SaldoAkhir=(select SaldoAwal+transaksi from test where ID=@i and ItemID=@itemid) where ID=@i and itemid=@itemid " +
                        "       set @i=@i+1 " +
                        "      end " +
                        "     end " +
                        " set @itm=@itm+1 " +
                        "end	 " +
                        "select Tanggal,ItemCode,ItemName,MinStock,MaxStock,SaldoAwal,Transaksi,SaldoAkhir, " +
                        "Case When SaldoAkhir < MinStock then 1 else 0 end inMin, " +
                        "Case When SaldoAkhir > MaxStock then 1 else 0 end inMax from test order by GroupID,ItemName,ID ";
                break;
        }
        return query;
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
                arrData.Add(GeneratObject(sdr));
            }
        }
        return arrData;
    }

    public Mmax GeneratObject(SqlDataReader sdr)
    {
        objM = new Mmax();
        switch (this.Tabel)
        {
            case "Tahun":
                objM.Tahun = Convert.ToInt32(sdr["Tahun"].ToString());
                break;
            case "Data":
                objM.ItemCode = sdr["ItemCode"].ToString();
                objM.ItemName = sdr["ItemName"].ToString();
                objM.MinStock = Convert.ToDecimal(sdr["MinStock"].ToString());
                objM.MaxStock = Convert.ToDecimal(sdr["MaxStock"].ToString());
                objM.SaldoAwal = Convert.ToDecimal(sdr["SaldoAwal"].ToString());
                objM.Transaksi = Convert.ToDecimal(sdr["Transaksi"].ToString());
                objM.SaldoAkhir = Convert.ToDecimal(sdr["SaldoAkhir"].ToString());
                objM.inMin = Convert.ToDecimal(sdr["inMin"].ToString());
                objM.inMax = Convert.ToDecimal(sdr["inMax"].ToString());
                objM.Tanggal = Convert.ToDateTime(sdr["Tanggal"].ToString());
                break;
        }
        return objM;
    }
}
public class Mmax : GRCBaseDomain
{
    public int Tahun { get; set; }
    public int Bulan { get; set; }
    public string ItemCode { get; set; }
    public string ItemName { get; set; }
    public decimal MinStock { get; set; }
    public decimal MaxStock { get; set; }
    public decimal SaldoAwal { get; set; }
    public decimal Transaksi { get; set; }
    public decimal SaldoAkhir { get; set; }
    public decimal inMin { get; set; }
    public decimal inMax { get; set; }
    public DateTime Tanggal { get; set; }
}