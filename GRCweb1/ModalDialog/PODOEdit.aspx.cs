using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Domain;
using BusinessFacade;
using DataAccessLayer;

namespace GRCweb1.ModalDialog
{
    public partial class PODOEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string schPOID = (Request.QueryString["p"] != null) ? Request.QueryString["p"].ToString() : "";
                txtSchPOID.Text = schPOID.ToString();
                LoadPO(schPOID);
                LoadDetailSchPO();
            }
        }

        protected void btnSimpan_CLick(object sender, EventArgs e)
        {
            ArrayList arrData = new ArrayList();
            Session["SchDelEd"] = null;
            POPurchn po = new POPurchn();
            po.ID = int.Parse(Request.QueryString["p"].ToString());
            po.POID = int.Parse(ddlPO.SelectedValue.ToString());
            po.ItemID = int.Parse(txtItemID.Text);
            po.SupplierID = int.Parse(txtSupID.Text);
            po.Qty = Convert.ToDecimal(txtQtyMobil.Text);
            po.EstQty = Convert.ToDecimal(txtEstQty.Text);
            po.CreatedBy = ((Users)Session["Users"]).UserID.ToString();
            po.Keterangan = txtKeterangan.Text;
            string[] TypePO = txtDelivery.Text.Split(' ');
            po.DocumentNo = TypePO[0];
            arrData.Add(po);
            Session["SchDelEd"] = arrData;
            Response.Write("<script language='javascript'>window.close();</script>");
        }
        protected void txtQtyMobil_Change(object sender, EventArgs e)
        {
            string Kapasitas = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read(txtItemID.Text, "KapasitasHiblow");
            int qty = int.Parse(txtQtyMobil.Text) * int.Parse(Kapasitas.ToString());
            txtEstQty.Text = qty.ToString("###,#00.00");
            txtKeterangan.Text = string.Empty;
            #region validasi jml mobil yng diminta
            Parsialedit pd = new Parsialedit();
            pd.schID = "(select SchID from MemoHarian_PO where ID=" + Request.QueryString["p"].ToString() + " AND RowStatus>-1)";
            pd.Field = "Qty";
            decimal rst = pd.GetJmlMobil();
            pd.Field = "schQty";
            decimal sch = pd.GetJmlMobil();
            decimal jdw = (Session["QtyM"] != null) ? decimal.Parse(Session["QtyM"].ToString()) : 0;
            sch = sch - jdw;
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
            Parsialedit pd = new Parsialedit();
            pd.Field = "PO";
            pd.Criteria = "";
            pd.schID = "(select SchID from MemoHarian_PO where ID=" + SchID + ")";
            arrData = pd.ListPO();
            ddlPO.Items.Clear();
            ddlPO.Items.Add(new ListItem("--Pilih PO--", "0"));
            foreach (POPurchn po in arrData)
            {
                ddlPO.Items.Add(new ListItem(po.NoPO + " - " + po.SupplierName, po.POID.ToString()));
            }

        }
        private void LoadDetailSchPO()
        {
            Session["QtyM"] = null;
            POPurchn po = new POPurchn();
            Parsialedit pa = new Parsialedit();
            pa.Criteria = " and ID=" + txtSchPOID.Text;
            pa.Field = "SchDetail";
            po = pa.GetDetailSchPO();
            ddlPO.SelectedValue = po.POID.ToString();
            ddlPO_Change(null, null);
            txtQtyMobil.Text = po.Qty.ToString();
            txtEstQty.Text = po.QtyPO.ToString();
            txtKeterangan.Text = po.Keterangan;
            Session["QtyM"] = po.Qty.ToString();
        }
        protected void ddlPO_Change(object sender, EventArgs e)
        {
            ArrayList arrData = new ArrayList();
            Parsialedit pd = new Parsialedit();
            pd.Field = "PODetail";
            pd.schID = Request.QueryString["p"].ToString();
            pd.Criteria = ddlPO.SelectedValue.ToString();
            POPurchn po = pd.DetailPO();
            decimal poDipakai = pd.GetPODipakai(ddlPO.SelectedValue.ToString());
            txtSupplierName.Text = po.SupplierName.ToString();
            txtMaterial.Text = po.ItemCode.ToString() + "- " + po.ItemName.ToString();
            txtQtyMobil.Text = "";
            txtQtyMobil.ToolTip = po.Qty.ToString();
            txtOP.Text = (po.QtyReceipt - poDipakai).ToString("###,#00.00");
            txtItemID.Text = po.ItemID.ToString();
            txtSupID.Text = po.SupplierID.ToString();
            txtKeterangan.Text = po.Keterangan.ToString();
            txtDelivery.Text = po.Delivery.ToString();

            //btnSimpan.Enabled = false;
        }
    }

    public class Parsialedit
    {
        private ArrayList arrData = new ArrayList();
        private POPurchn po = new POPurchn();
        public string Criteria { get; set; }
        public string Field { get; set; }
        public string schID { get; set; }
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
                            "and Status>-2)  " +
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
                           " isnull((select Qty from MemoHarian where ID=(select SchID from memoharian_po where ID=" + this.schID + ")),0)QtyMobil,   " +
                           " isnull((select estimasiqty from MemoHarian where ID=(select SchID from MemoHarian_PO where ID=" + this.schID + ")),0)EstQty   " +
                           " from(    " +
                           " Select p.ID,p.SupplierID,p.Delivery,pa.ItemID,pa.Qty," +
                           "isnull((Select SUM(Quantity) From ReceiptDetail where RowStatus>-1 and PODetailID=pa.ID),0) Rms  " +
                           " from POPurchn  as p    " +
                           " INNER JOIN POPurchnDetail as pa on pa.POID=p.ID    " +
                           " where  p.Status>-1 and   p.ID=" + this.Criteria + "  ) as x    " +
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
        public decimal GetPODipakai(string POID)
        {
            decimal result = 0;
            string strSQL = "select isnull(SUM(EstQty),0)Qty from MemoHarian_PO where POID=" + POID + " and RowStatus>-1";
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
            string strSQL = "select *,isnull((select SUM(mp.QtyMobil) from MemoHarian_PO mp where mp.SchID =mm.ID AND mp.RowStatus>-1 group by mp.SchID),0)schQty " +
                            "from MemoHarian as mm where mm.RowStatus >-1 and mm.ID=" + this.schID;
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
        public POPurchn GetDetailSchPO()
        {
            po = new POPurchn();
            string strsql = "Select * from MemoHarian_PO where RowStatus>-1 " + this.Criteria;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strsql);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    return this.gObject(sdr);
                }
            }
            return po;
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
                case "SchDetail":
                    po.ID = Convert.ToInt32(sdr["ID"].ToString());
                    po.POID = Convert.ToInt32(sdr["POID"].ToString());
                    po.Qty = Convert.ToDecimal(sdr["QtyMobil"].ToString());
                    po.QtyPO = Convert.ToDecimal(sdr["EstQty"].ToString());
                    po.Keterangan = sdr["Keterangan"].ToString();
                    break;
            }

            return po;
        }
    }
}