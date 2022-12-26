using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using BusinessFacade;
using Domain;

namespace GRCweb1.ModalDialog
{
    public partial class POParsialUpdate : System.Web.UI.Page
    {
        private string schID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                schID = (Request.QueryString["p"] != null) ? Request.QueryString["p"].ToString() : "0";
                LoadMaterial();
                LoadSCH(schID);
            }
        }

        protected void btnSimpan_CLick(object sender, EventArgs e)
        {
            string Query = "Update Memoharian set Qty=" + txtQuantity1.Text.Replace(".", "").Replace(",", ".");
            Query += ",DvlDate='" + DateTime.Parse(txtSchDate1.Text).ToString("yyyy-MM-dd") + "'";
            Query += ",Keterangan='" + txtKeterangan1.Text + "'";
            Query += ",EstimasiQty=" + txtOutPO1.Text.Replace(".", "").Replace(",", ".");
            Query += " where ID=" + scID.Value;
            DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(Query);
            if (sdr.RecordsAffected > 0)
            {
                Response.Write("<script language='javascript'>window.close();</script>");
            }
        }
        private void LoadSCH(string IDSch)
        {
            string query = "Select * from Memoharian where ID=" + IDSch;
            DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(query);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    scID.Value = sdr["ID"].ToString();
                    ddlMaterial1.SelectedValue = sdr["ItemID"].ToString();
                    ddlMaterial1.Enabled = false;
                    txtQuantity1.Text = sdr["Qty"].ToString();
                    txtQuantity1.Enabled = (TotalSudahDiPO(sdr["ID"].ToString(), sdr["ItemID"].ToString()) < Convert.ToDecimal(sdr["Qty"].ToString())) ? true : false;
                    txtSchNo1.Text = sdr["SchNo"].ToString();
                    txtSchDate1.Text = DateTime.Parse(sdr["DvlDate"].ToString()).ToString("dd-MM-yyyy"); ;
                    txtOutPO1.Text = sdr["EstimasiQty"].ToString();
                    txtOutPO1.Enabled = false;
                    txtKeterangan1.Text = sdr["Keterangan"].ToString();
                    txtSchDate1.Enabled = (TotalSudahDiPO(sdr["ID"].ToString(), sdr["ItemID"].ToString()) < Convert.ToDecimal(sdr["Qty"].ToString())) ? true : false;
                }
            }
        }
        protected void ddlPoNo_Click(object sender, EventArgs e)
        {

        }
        private void LoadMaterial()
        {
            try
            {
                ArrayList arrData = new ArrayList();
                ArrayList arrInv = new ArrayList();
                string[] ItemID = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ItemID", "ReorderPointPOType" + ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID.ToString()).Split(',');
                string ItemIDs = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ItemID", "ReorderPointPOType" + ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID.ToString());
                InventoryFacade ip = new InventoryFacade();
                ip.Criteriane = " and A.ID in(" + ItemIDs + ")";
                arrInv = ip.Retrieve();
                ddlMaterial1.Items.Clear();
                ddlMaterial1.Items.Add(new ListItem("", "0"));
                foreach (Inventory inv in arrInv)
                {
                    if (ItemID.Contains(inv.ID.ToString()))
                    {
                        ddlMaterial1.Items.Add(new ListItem(inv.ItemCode + " - " + inv.ItemName, inv.ID.ToString()));
                    }
                }
            }
            catch (Exception ex)
            {

                return;
            }
        }
        protected void txt_onChange1(object sender, EventArgs e)
        {
            string Kapasitas = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read(ddlMaterial1.SelectedValue.ToString(), "KapasitasHiblow");
            int qty = int.Parse(txtQuantity1.Text) * int.Parse(Kapasitas.ToString());
            txtOutPO1.Text = qty.ToString("###,#00.00");

        }
        private decimal TotalSudahDiPO(string SCHID, string ItemID)
        {
            decimal result = 0;
            string Query = "select schID,ItemID, sum(QtyMobil)Qty from MemoHarian_PO where SchID=" + SCHID + " and ItemID=" + ItemID + " group by ItemID,SchID";
            DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(Query);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToDecimal(sdr["Qty"].ToString());
                }
            }
            return result;
        }

    }
}