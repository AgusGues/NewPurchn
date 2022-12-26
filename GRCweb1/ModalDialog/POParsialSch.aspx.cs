using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using BusinessFacade;
using Domain;
using DataAccessLayer;

namespace GRCweb1.ModalDialog
{
    public partial class POParsialSch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                string ID = Request.QueryString["p"].ToString();
                LoadPO(ID);
            }
        }

        protected void ddlPO_Change(object sender, EventArgs e)
        {
            ArrayList arrData = new ArrayList();
            ParsialDelivery pd = new ParsialDelivery();
            pd.Field = "PODetail";
            pd.schID = Request.QueryString["p"].ToString();
            pd.Criteria = ddlPOx.SelectedValue.ToString();
            POPurchn po = pd.DetailPO();
            string DlvDate = pd.GetDeliverySch(Request.QueryString["p"].ToString());
            decimal po1 = 0;
            decimal po2 = 0;
            po1 = pd.GetPODipakai(ddlPOx.SelectedValue.ToString(), DlvDate);
            po2 = pd.GetPODipakai(ddlPOx.SelectedValue.ToString());
            //decimal poDipakai = 
            //    (po.Delivery.Substring(0, 5).ToUpper() == "FRANC") ? 
            //    pd.GetPODipakai(ddlPOx.SelectedValue.ToString(), DlvDate) : 
            //    pd.GetPODipakai(ddlPOx.SelectedValue.ToString());
            decimal poDipakai = 0;
            txtEstQty.Text = po.QtyPO.ToString("N2");
            txtSupplierName.Text = po.SupplierName.ToString();
            txtMaterial.Text = po.ItemCode.ToString() + "- " + po.ItemName.ToString();
            txtQtyMobil.Text = "";
            txtQtyMobil.ToolTip = po.Qty.ToString();
            txtOP.Text = ((po.QtyReceipt - poDipakai) < 0) ? "0" : (po.QtyReceipt - poDipakai).ToString("N2");
            txtItemID.Text = po.ItemID.ToString();
            txtSupID.Text = po.SupplierID.ToString();
            txtKeterangan.Text = po.Keterangan.ToString();
            txtDelivery.Text = po.Delivery.ToString();
            btnSimpan.Enabled = false;
        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            ArrayList arrData = new ArrayList();
            Session["SchDel"] = null;
            POPurchn po = new POPurchn();
            po.SchID = int.Parse(Request.QueryString["p"].ToString());
            po.POID = int.Parse(ddlPOx.SelectedValue.ToString());
            po.ItemID = int.Parse(txtItemID.Text);
            po.SupplierID = int.Parse(txtSupID.Text);
            po.Qty = Convert.ToDecimal(txtQtyMobil.Text);
            po.EstQty = Convert.ToDecimal(txtEstQty.Text);
            po.CreatedBy = ((Users)Session["Users"]).UserID.ToString();
            po.Keterangan = txtKeterangan.Text;
            string[] TypePO = txtDelivery.Text.ToUpper().Split(' ');
            po.DocumentNo = txtDelivery.Text.Substring(0, 4).ToUpper();
            arrData.Add(po);
            Session["SchDel"] = arrData;
            Response.Write("<script language='javascript'>window.close();</script>");
        }
        protected void txtQtyMobil_Change(object sender, EventArgs e)
        {
            string Kapasitas = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read(txtItemID.Text, "KapasitasHiblow");
            int qty = int.Parse(txtQtyMobil.Text) * int.Parse(Kapasitas.ToString());
            txtEstQty.Text = qty.ToString("N2");
            #region validasi jml mobil yng diminta
            ParsialDelivery pd = new ParsialDelivery();
            pd.schID = Request.QueryString["p"].ToString();
            pd.Field = "Qty";
            decimal rst = pd.GetJmlMobil();
            pd.Field = "schQty";
            decimal sch = pd.GetJmlMobil();
            if ((sch + decimal.Parse(txtQtyMobil.Text)) > rst)
            {
                txtKeterangan.Text = "Jml Mobil melebihi permintaan";
                btnSimpan.Enabled = false;
            }
            else if (decimal.Parse(txtEstQty.Text) > decimal.Parse(txtOP.Text))
            {
                txtKeterangan.Text = "Outstanding PO tidak mencukupi";
                btnSimpan.Enabled = false;
            }
            else
            {
                btnSimpan.Enabled = true;
            }
            #endregion
        }
        private void LoadPO(string SchID)
        {
            ArrayList arrData = new ArrayList();
            ParsialDelivery pd = new ParsialDelivery();
            pd.Field = "PO";
            pd.Criteria = "";
            pd.schID = SchID;
            arrData = pd.ListPO();
            ddlPOx.Items.Clear();
            ddlPOx.Items.Add(new ListItem("--Pilih PO--", "0"));
            foreach (POPurchn po in arrData)
            {
                ddlPOx.Items.Add(new ListItem(po.NoPO + " - " + po.SupplierName, po.POID.ToString()));
            }

        }

        protected void ddlPOx_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
    public class ParsialDelivery
    {
        private ArrayList arrData = new ArrayList();
        private POPurchn po = new POPurchn();
        public string Criteria { get; set; }
        public string Field { get; set; }
        public string schID { get; set; }
        private string HiblowCap = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("HiblowCapacity", "MemoHarian");
        public ArrayList ListPO()
        {
            arrData = new ArrayList();
            string strSQL = "select (select NoPO from POPurchn where ID=pd.POID)NoPO, (select SupplierName from SuppPurch where ID=( " +
                            "(select SupplierID from POPurchn where ID=pd.POID)))SupplierName,  pd.POID,pd.Qty " +
                            ",isnull((Select SUM(Quantity) From ReceiptDetail where RowStatus>-1 and ReceiptDetail.PODetailID= " +
                            "pd.ID group by PODetailID),0)RCP " +
                            "from POPurchnDetail pd  " +
                            "where pd.POID in " +
                            "(select POID from POPurchnDetail where ItemID=(select ItemID from MemoHarian where ID=" + this.schID + ") " +
                            "and Status>-1)  " +
                            "and pd.Status>-1  " +
                            "and pd.Qty > isnull((Select SUM(Quantity) from ReceiptDetail where RowStatus>-1 and ReceiptDetail.PODetailID=pd.ID group by PODetailID),0) " +
                            "order by POID desc";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(gObject(sdr));
                }
            }
            return arrData;
        }
        public POPurchn DetailPO()
        {
            po = new POPurchn();
            string query = "select *,(Qty-Rms)OutPO,s.SupplierName, " +
                           "(select dbo.ItemCodeInv(itemID,1))ItemCode,   " +
                           " (select dbo.ItemNameInv(itemID,1))ItemName,  " +
                           " (select Qty from MemoHarian where ID=" + this.schID + ")QtyMobil,   " +
                           " (select estimasiqty from MemoHarian where ID=" + this.schID + ")EstQty   " +
                           " from(    " +
                           " Select p.ID,p.SupplierID,p.Delivery,pa.ItemID,pa.Qty," +
                           "isnull((Select SUM(Quantity) From ReceiptDetail where RowStatus>-1 and PODetailID=pa.ID),0) Rms  " +
                           " from POPurchn  as p    " +
                           " INNER JOIN POPurchnDetail as pa on pa.POID=p.ID    " +
                           " where  p.Status>-1 and pa.Status>-1 and   p.ID=" + this.Criteria + "  ) as x    " +
                           " Left Join SuppPurch as s on s.ID=x.SupplierID ";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(query);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    return this.gObject(sdr);
                }
            }
            return po;
        }
        public string GetDeliverySch(string SCHID)
        {
            string result = string.Empty;
            string strSQL = "Select * from MemoHarian where ID=" + schID + " and RowStatus >-1";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = sdr["DvlDate"].ToString();
                }
            }
            return result;
        }
        public decimal GetPODipakai(string POID)
        {
            decimal result = 0;
            string strSQL = "select /*isnull(sum(po.QtyMobil*po.EstQty),0)*/(isnull(Count(ma.ArmadaID),0)* " + this.HiblowCap + ") as Qty " +
                            "from MemoHarian_PO as po " +
                            "left join MemoHarian_Armada as ma " +
                            "on ma.SchPOID=po.ID " +
                            "where po.POID=" + POID + " and ma.Flag=0  and po.RowStatus >-1 and ma.RowStatus>-1";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToDecimal(sdr["Qty"].ToString());
                }
            }
            return result;
        }
        public decimal GetPODipakai(string POID, string DlvDate)
        {
            decimal result = 0;
            string strSQL = "select /*isnull(SUM(po.QtyMobil*po.EstQty),0)*/ (isnull(Count(ma.ArmadaID),0)* " + this.HiblowCap + ") as Qty " +
                            "from MemoHarian_PO as po " +
                            "left join MemoHarian as ma on ma.ID=po.SchID " +
                            "where po.POID=" + POID + " and ma.DvlDate='" + DlvDate + "'  and po.RowStatus >-1 and ma.RowStatus>-1";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToDecimal(sdr["Qty"].ToString());
                }
            }
            return result;
        }
        public decimal GetJmlMobil()
        {
            decimal result = 0;
            string strSQL = "select *,isnull((select SUM(mp.QtyMobil) from MemoHarian_PO mp where mp.SchID =mm.ID " +
                            " and mp.RowStatus>-1 group by SchID),0)schQty " +
                            "from MemoHarian as mm where mm.RowStatus >-1  and mm.ID=" + this.schID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToDecimal(sdr[this.Field].ToString());
                }
            }
            return result;
        }
        private POPurchn gObject(SqlDataReader sdr)
        {
            po = new POPurchn();
            switch (this.Field)
            {
                case "PO":
                    po.POID = Convert.ToInt32(sdr["POID"].ToString());
                    po.NoPO = sdr["NoPO"].ToString();
                    po.SupplierName = sdr["SupplierName"].ToString();
                    break;
                case "PODetail":
                    po.ID = Convert.ToInt32(sdr["ID"].ToString());
                    po.SupplierName = sdr["SupplierName"].ToString();
                    po.ItemCode = sdr["ItemCode"].ToString();
                    po.ItemName = sdr["ItemName"].ToString();
                    po.QtyReceipt = Convert.ToDecimal(sdr["OutPO"].ToString());
                    po.Qty = Convert.ToDecimal(sdr["QtyMobil"].ToString());
                    po.QtyPO = Convert.ToDecimal(sdr["EstQty"].ToString());
                    po.SupplierID = Convert.ToInt32(sdr["SupplierID"].ToString());
                    po.ItemID = Convert.ToInt32(sdr["ItemID"].ToString());
                    po.Delivery = sdr["Delivery"].ToString();
                    break;
            }

            return po;
        }
    }
}