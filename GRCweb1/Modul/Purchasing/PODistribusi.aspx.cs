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
using System.Reflection;
using Domain;
using BusinessFacade;
using DataAccessLayer;

namespace GRCweb1.Modul.Purchasing
{
    public partial class PODistribusi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadBulan();
                LoadTahun();
            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnPrint);
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadBulan()
        {
            ddlBulan.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i).ToString(), i.ToString().PadLeft(2, '0')));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString().PadLeft(2, '0');
        }
        private void LoadTahun()
        {
            ArrayList arrData = new ArrayList();
            PantauPO po = new PantauPO();
            po.Prefix = "Year(POPurchnDate) as Tahun";
            po.Field = "Tahun";
            po.Criteria = "where POPurchnDate is not null Group by Year(POPurchnDate) order by Year(POPurchnDate) ";
            arrData = po.Retrieve();
            ddlTahun.Items.Clear();
            foreach (POPurchn p in arrData)
            {
                ddlTahun.Items.Add(new ListItem(p.Tahun.ToString(), p.Tahun.ToString()));
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }
        private void LoadData()
        {
            string[] arrDept = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("OnlyDeptID", "PO").Split(',');
            ArrayList arrData = new ArrayList();
            PantauPO po = new PantauPO();
            po.Field = "Data";
            po.Tahun = ddlTahun.SelectedValue.ToString();
            po.Bulan = ddlBulan.SelectedValue.ToString();
            po.Criteria = (arrDept.Contains(((Users)Session["Users"]).DeptID.ToString()) && ((Users)Session["Users"]).Apv < 1) ?
                            " where Createdby='" + ((Users)Session["Users"]).UserID.ToString() + "'" : "";
            arrData = po.Retrieve();
            lstPO.DataSource = arrData;
            lstPO.DataBind();
        }
        protected void lstPO_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            ArrayList arrData = new ArrayList();
            PantauPO po = new PantauPO();
            POPurchn pp = new POPurchn();
            int result = 0;
            int ID = int.Parse(e.CommandArgument.ToString());
            Image img = (Image)e.Item.FindControl("ActDlv");
            switch (e.CommandName)
            {
                case "act":
                    if (Session["Update"] != null)
                    {
                        arrData = (ArrayList)Session["Update"];
                        foreach (POPurchn p in arrData)
                        {
                            pp.POID = p.POID;
                            pp.KirimVia = p.KirimVia;
                            pp.KirimDate = p.KirimDate;
                            pp.TerimaBy = p.TerimaBy;
                            pp.TerimaDate = p.TerimaDate;
                            pp.EstDelivery = p.EstDelivery;
                            pp.ActDelivery = p.ActDelivery;
                            pp.Keterangan = p.Keterangan;
                            if (p.ID > 0)
                            {
                                pp.ID = p.ID;
                                po.Criteria = "ID,KirimVia,KirimDate,TerimaBy,TerimaDate,EstDelivery,ActDelivery,Keterangan,RowStatus";
                                result = po.ProcessData(pp, "spPODistribusi_Update");
                            }
                            else
                            {
                                po.Criteria = "POID,KirimVia,KirimDate,TerimaBy,TerimaDate,EstDelivery,ActDelivery,Keterangan,CreatedBy";
                                result = po.ProcessData(pp, "spPODistribusi_Insert");
                            }
                        }
                    }
                    if (result > 0)
                    {
                        LoadData();
                    }
                    break;
            }
        }
        protected void lstPO_DataBound(object sender, RepeaterItemEventArgs e)
        {
            string[] arrDept = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("OnlyDeptID", "PO").Split(',');
            Label dll = (Label)e.Item.FindControl("ddlKirim");
            Label tgKirim = (Label)e.Item.FindControl("Label1");
            Label diterima = (Label)e.Item.FindControl("Label2");
            Label tglterima = (Label)e.Item.FindControl("Label3");
            Label estdel = (Label)e.Item.FindControl("Label4");
            Label actdel = (Label)e.Item.FindControl("Label5");
            Label ket = (Label)e.Item.FindControl("Keterangan");
            Image img = (Image)e.Item.FindControl("ActDlv");
            POPurchn po = (POPurchn)e.Item.DataItem;
            PantauPO p = new PantauPO();
            img.Attributes.Add("onclick", "openWindow(" + po.ID.ToString() + ")");
            img.Visible = (arrDept.Contains(((Users)Session["Users"]).DeptID.ToString()) && ((Users)Session["Users"]).UserID == po.CreatedBy) ? true : false;
            p.Criteria = " where PO.ID=" + po.ID;
            p.Field = "Detail";
            POPurchn pp = p.RetrieveDetail();
            dll.Text = (pp.KirimVia != null) ? pp.KirimVia.ToString() : "";
            tgKirim.Text = (pp.KirimDate.Year < 1902) ? "" : pp.KirimDate.ToShortDateString();
            tgKirim.Text = (pp.TerimaDate.Year < 1902) ? "" : pp.TerimaDate.ToShortDateString();
            diterima.Text = (pp.TerimaBy != null) ? pp.TerimaBy : "";
            tglterima.Text = (pp.TerimaDate.Year < 1902) ? "" : pp.TerimaDate.ToShortDateString();
            estdel.Text = (pp.EstDelivery.Year < 1902) ? "" : pp.EstDelivery.ToShortDateString();
            actdel.Text = (pp.ActDelivery.Year < 1902) ? "" : pp.ActDelivery.ToShortDateString();
            ket.Text = (pp.Keterangan != null) ? pp.Keterangan : "";
        }
        protected void btnPrint_onClick(object sender, EventArgs e)
        {
            foreach (RepeaterItem img in lstPO.Items)
            {
                Image im = (Image)img.FindControl("ActDlv");
                im.Visible = false;
            }

            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=PemantauanDistribusiPO.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "PEMANTAUAN DISTRIBUSI PO";
            Html += "<br>Periode : " + ddlBulan.SelectedItem.Text + " " + ddlTahun.SelectedValue.ToString();
            string HtmlEnd = "";
            //lstForPrint.RenderControl(hw);
            lstRkp.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0\"", "border=\"1\"");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }
    }
    public class PantauPO
    {
        private ArrayList arrData = new ArrayList();
        private POPurchn objPO = new POPurchn();
        private List<SqlParameter> sqlListParam;
        public string Criteria { get; set; }
        public string Prefix { get; set; }
        public string Field { get; set; }
        public string Tahun { get; set; }
        public string Bulan { get; set; }
        public PantauPO()
            : base()
        {

        }
        private string Query()
        {
            string query = string.Empty;
            switch (this.Field)
            {
                case "Tahun":
                    query = "select " + this.Prefix + " from POPurchn " + this.Criteria;
                    break;
                case "Data":
                    query = this.QueryData();
                    break;
                case "Detail":
                    query = this.QueryDetail();
                    break;
            }
            return query;
        }
        private string QueryDetail()
        {
            return "Select PO.NoPO,(Select SuppPurch.SupplierName from SuppPurch where ID=PO.SupplierID)SupplierName, " +
                   " PO.POPurchnDate,PO.Approval, PD.* from  " +
                   " POPurchn as PO " +
                   " LEFT JOIN PODistribusi as PD " +
                   " ON PD.POID=PO.ID  " + this.Criteria;
        }
        private string QueryData()
        {
            return "SELECT ID,NoPO,POPurchnDate,SuplierName,Status,Approval,CreatedBy, " +
                   " SUM(Harga) as Harga,((SUM(Harga)-(SUM(harga)*isnull(PPN,0))-isnull(Disc,0)))TotalHarga FROM ( " +
                   " select PO.ID,PD.ID as DetailID,PO.NoPO,PO.POPurchnDate," +
                   "(Select SuppPurch.SupplierName from SuppPurch where ID=PO.SupplierID)SuplierName," +
                   "PO.Status,Approval,CreatedBy,PD.ItemID,PD.Price,PD.Qty,(PD.Qty*PD.Price)Harga,PO.PPN,PO.Disc " +
                   " from POPurchn as PO " +
                   " Left JOIN POPurchnDetail as PD " +
                   " ON PD.POID=PO.ID " +
                   "  where YEAR(POPurchnDate)=" + this.Tahun + " and MONTH(POPurchnDate)= " + this.Bulan +
                   "  and PO.Status>-1 and PD.Status>-1 " +
                   "  ) as x " +
                   this.Criteria +
                   "  Group By ID,NoPO,POPurchnDate,SuplierName,Status,Approval,CreatedBy,PPN,Disc " +
                   "  order by NoPO,ID";
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
                    arrData.Add(this.GenerateObject(sdr));
                }
            }
            return arrData;
        }
        public POPurchn RetrieveDetail()
        {
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(this.Query());
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    return this.GenerateObject(sdr);
                }
            }
            return new POPurchn();
        }
        private POPurchn GenerateObject(SqlDataReader sdr)
        {
            objPO = new POPurchn();
            switch (this.Field)
            {
                case "Tahun":
                    objPO.Tahun = Convert.ToInt32(sdr["Tahun"].ToString());
                    break;
                case "Data":
                    objPO.ID = Convert.ToInt32(sdr["ID"].ToString());
                    objPO.NoPO = sdr["NoPO"].ToString();
                    objPO.POPurchnDate = Convert.ToDateTime(sdr["POPurchnDate"].ToString());
                    objPO.SupplierName = sdr["SuplierName"].ToString();
                    objPO.Approval = Convert.ToInt32(sdr["Approval"].ToString());
                    objPO.CreatedBy = sdr["CreatedBy"].ToString();
                    objPO.TotalHarga = Convert.ToDecimal(sdr["TotalHarga"].ToString());
                    break;
                case "Detail":
                    if (sdr["ID"] != DBNull.Value)
                    {
                        objPO.POID = Convert.ToInt32(sdr["POID"].ToString());
                        objPO.KirimVia = sdr["KirimVia"].ToString();
                        objPO.KirimDate = Convert.ToDateTime(sdr["KirimDate"].ToString());
                        objPO.TerimaBy = sdr["TerimaBy"].ToString();
                        objPO.TerimaDate = Convert.ToDateTime(sdr["TerimaDate"].ToString());
                        objPO.EstDelivery = Convert.ToDateTime(sdr["EstDelivery"].ToString());
                        objPO.ActDelivery = Convert.ToDateTime(sdr["ActDelivery"].ToString());
                    }
                    break;
            }
            return objPO;
        }
        public int ProcessData(object arrAL, string spName)
        {
            try
            {
                objPO = (POPurchn)arrAL;
                string[] arrCriteria = this.Criteria.Split(',');
                PropertyInfo[] data = objPO.GetType().GetProperties();
                DataAccess da = new DataAccess(Global.ConnectionString());
                var equ = new List<string>();
                sqlListParam = new List<SqlParameter>();
                foreach (PropertyInfo items in data)
                {
                    if (items.GetValue(objPO, null) != null && arrCriteria.Contains(items.Name))
                    {
                        sqlListParam.Add(new SqlParameter("@" + items.Name.ToString(), items.GetValue(objPO, null)));
                    }
                }
                int result = da.ProcessData(sqlListParam, spName);
                string err = da.Error;
                return result;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return -1;
            }
        }
    }
}