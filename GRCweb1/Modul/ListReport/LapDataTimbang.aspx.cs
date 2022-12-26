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
    public partial class LapDataTimbang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                txtDrTgl.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtSdTgl.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                LoadSupplier();
            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);

        }
        protected void lstSupplier_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ArrayList arrData = new ArrayList();
            DataTimbang dt = new DataTimbang();
            Repeater rpt = (Repeater)e.Item.FindControl("lstTimbang");
            string[] arrSupp = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("SupplierID", "Receipt").Split(',');
            dt.Criteria = (ddlSupplier.SelectedValue == string.Empty) ? " and SupplierID in(" + dt.GetSupplierID(arrSupp) + ")" : " and SupplierID=" + ddlSupplier.SelectedValue;
            dt.Criteria += " and Convert(Char,R.ReceiptDate,112) between '" + DateTime.Parse(txtDrTgl.Text).ToString("yyyyMMdd") + "' and '" +
                           DateTime.Parse(txtSdTgl.Text).ToString("yyyyMMdd") + "'";
            dt.Criteria += " order by ID";
            arrData = dt.ListDataTimbang();
            rpt.DataSource = arrData;
            rpt.DataBind();
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=ListDataTimbangRMS.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<H2>LIST DATA TIMBANG RMS</H2>";
            Html += "Periode : " + txtDrTgl.Text + " s/d " + txtSdTgl.Text;
            Html += "";
            string HtmlEnd = "";
            div2.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0\"", "border=\"1\"");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }
        private void LoadSupplier()
        {
            ArrayList arrSpl = new ArrayList();
            string[] arrSupp = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("SupplierID", "Receipt").Split(',');
            arrSpl = new SuppPurchFacade().Retrieve();
            ddlSupplier.Items.Clear();
            ddlSupplier.Items.Add(new ListItem("All", ""));
            foreach (SuppPurch spl in arrSpl)
            {
                if (arrSupp.Contains(spl.SupplierName.ToString()))
                {
                    ddlSupplier.Items.Add(new ListItem(spl.SupplierName, spl.ID.ToString()));
                }
            }
        }
        private void LoadData()
        {
            ArrayList arrData = new ArrayList();
            string[] arrSupp = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("SupplierID", "Receipt").Split(',');
            arrData = new DataTimbang().ListSupplier(arrSupp);
            lstSupplier.DataSource = arrData;
            lstSupplier.DataBind();
        }
    }
}

public class Timbang : Receipt
{
    public decimal Selisih { get; set; }
    public decimal QtyTimbang { get; set; }
    public string Unit { get; set; }
    public decimal Persentase { get; set; }
}

public class DataTimbang
{
    private ArrayList arrSupp = new ArrayList();
    private ArrayList arrData = new ArrayList();
    public string SupplierID { get; set; }
    public string Criteria { get; set; }
    public ArrayList ListSupplier(Array arrSP)
    {
        arrData = new ArrayList();
        arrSupp = new SuppPurchFacade().Retrieve();
        foreach (SuppPurch Supp in arrSupp)
        {
            //if (arrSP.Equals(Supp.SupplierName))
            int idx = Array.IndexOf(arrSP, Supp.SupplierName);
            if (idx > -1)
            {
                arrData.Add(new SuppPurch
                {
                    ID = Supp.ID,
                    SupplierName = Supp.SupplierName,
                    SupplierCode = Supp.SupplierCode
                });
            }
        }
        return arrData;
    }
    public string GetSupplierID(Array arrSP)
    {
        string arrSup = string.Empty;
        arrData = new ArrayList();
        arrData = new SuppPurchFacade().Retrieve();
        foreach (SuppPurch Supp in arrData)
        {
            int idx = Array.IndexOf(arrSP, Supp.SupplierName);
            if (idx > -1)
            {
                arrSup += Supp.ID.ToString() + ",";
            }
        }
        return (arrSup != string.Empty) ? arrSup.Substring(0, arrSup.Length - 1) : "";
    }
    public ArrayList ListDataTimbang()
    {
        arrData = new ArrayList();
        string strSQL = "select R.ReceiptNo,R.ReceiptDate,A.ID,A.ItemID,(Select dbo.ItemCodeInv(A.ItemID,A.ItemTypeID))ItemCode," +
                        "(SELECT dbo.ItemNameInv(A.ItemID,A.ItemTypeID)) as ItemName,(Select UOMCode from UOM where ID=UomID)Unit," +
                        "Quantity,QtyTimbang,(Quantity-QtyTimbang)Selisih,R.PONo,A.Keterangan from ReceiptDetail as A " +
                        "LEFT JOIN Receipt as R " +
                        "On R.ID=A.ReceiptID " +
                        "where R.Status>-1  " + this.Criteria;
        //"and CONVERT(CHAR,R.ReceiptDate,112) Between '20150301' and '20150304'";
        DataAccess dta = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = dta.RetrieveDataByString(strSQL);
        if (sdr.HasRows && dta.Error == string.Empty)
        {
            while (sdr.Read())
            {
                arrData.Add(new Timbang
                {
                    ID = Convert.ToInt32(sdr["ID"].ToString()),
                    ReceiptNo = sdr["ReceiptNo"].ToString(),
                    ReceiptDate = Convert.ToDateTime(sdr["ReceiptDate"].ToString()),
                    ItemCode = sdr["ItemCode"].ToString(),
                    ItemName = sdr["ItemName"].ToString(),
                    Quantity = Convert.ToDecimal(sdr["Quantity"].ToString()),
                    QtyTimbang = Convert.ToDecimal(sdr["QtyTimbang"].ToString()),
                    Selisih = Convert.ToDecimal(sdr["Selisih"].ToString()),
                    PoNo = sdr["PONo"].ToString(),
                    Unit = sdr["Unit"].ToString(),
                    keterangan = sdr["keterangan"].ToString(),
                    Persentase = Convert.ToDecimal(sdr["Selisih"].ToString()) / Convert.ToDecimal(sdr["Quantity"].ToString())
                });
            }
        }
        return arrData;
    }
}