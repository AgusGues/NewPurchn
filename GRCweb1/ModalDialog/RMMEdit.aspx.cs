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
using Factory;
using BusinessFacade;
using DataAccessLayer;

namespace GRCweb1.ModalDialog
{
    public partial class RMMEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string RMMID = (Request.QueryString["p"] != null) ? Request.QueryString["p"].ToString() : "";
                txtRMMID.Text = RMMID.ToString();
                LoadRMM(RMMID);
                LoadDetailRMM();
                btnSimpan.Enabled = false;
                //txtDateSolved.Enabled = false;
            }
        }

        protected void btnSimpan_CLick(object sender, EventArgs e)
        {
            //ArrayList arrData = new ArrayList();
            //Session["RmmDetai"] = null;
            Users user = ((Users)Session["Users"]);
            RMM_Detail rmmd = new RMM_Detail();
            RMMDetailFacade rmmdf = new RMMDetailFacade();
            int value = 0;
            if (chksolved.Checked == true)
                value = 1;
            else
                value = 0;
            //txtRMM_No.Text = rmmd.Ket;
            string strerror = rmmdf.UpdateSolveRMM(txtRMMID.Text, txtDateSolved.SelectedDate.ToString("yyyyMMdd"),
                txtKeterangan.Text, value);
            //ArrayList arrData = new ArrayList();
            //Session["RmmDetai"] = null;
            //RMM_Detail rmdtl= new RMM_Detail();//POPurchn po = new POPurchn();
            //rmdtl.ID = int.Parse(Request.QueryString["p"].ToString());////po.ID = int.Parse(Request.QueryString["p"].ToString());
            //rmdtl.Ket = txtKeterangan.Text;////po.POID = int.Parse(ddlPO.SelectedValue.ToString());
            //////po.ItemID = int.Parse(txtItemID.Text);
            //////po.SupplierID = int.Parse(txtSupID.Text);
            //////po.Qty = Convert.ToDecimal(txtQtyMobil.Text);
            //////po.EstQty = Convert.ToDecimal(txtEstQty.Text);
            //////po.CreatedBy = ((Users)Session["Users"]).UserID.ToString();
            //////po.Keterangan = txtKeterangan.Text;
            //////string[] TypePO = txtDelivery.Text.Split(' ');
            //////po.DocumentNo = TypePO[0];
            //arrData.Add(rmdtl);
            //Session["RmmDetai"] = arrData;

            Response.Write("<script language='javascript'>window.close();</script>");
        }

        protected void chksolved_CheckedChanged(object sender, EventArgs e)
        {
            btnSimpan.Enabled = true;
            txtDateSolved.SelectedDate = DateTime.Now;
        }

        private void LoadRMM(string ID)
        {
            ArrayList arrData = new ArrayList();
            RMMParsialedit pd = new RMMParsialedit();
            //RMM rmm = new RMM();
            pd.Field = "RmmDetail";
            pd.Criteria = "";
            pd.ID = "(select ID from RMM_Detail where ID=" + ID + ")";
            arrData = pd.ListRMM();
            //txtRMM_No.Text = pd.GetDetailRMM; ;
            //ddlRMM.Items.Clear();
            //ddlRMM.Items.Add(new ListItem("--Pilih RMM--", "0"));
            //foreach (RMM rmm in arrData)
            //{
            //    ddlRMM.Items.Add(new ListItem(rmm.RMM_No));
            //}
        }
        private void LoadDetailRMM()
        {
            //Session["QtyM"] = null;
            RMM_Detail rmmdtl = new RMM_Detail();
            RMMParsialedit pa = new RMMParsialedit();
            pa.Criteria = " and ID=" + txtRMMID.Text;
            pa.Field = "RmmDetail";
            rmmdtl = pa.GetDetailRMM();//po = pa.GetDetailSchPO();
            txtRMM_No.Text = rmmdtl.Aktivitas.ToString();
        }
    }

    public class RMMParsialedit
    {
        private ArrayList arrData = new ArrayList();
        private RMM_Detail rmmdtl = new RMM_Detail();
        public string Criteria { get; set; }
        public string Field { get; set; }
        public string ID { get; set; }
        public ArrayList ListRMM()
        {
            arrData = new ArrayList();
            string strSQL = "select * from RMM_Detail";//"select (select RMM_No from RMM where ID=pd.RMM_ID)RMM_No,pd.RMM_ID from RMM_Detail pd order by RMM_ID desc";
                                                       //string strSQL = "select (select NoPO from POPurchn where ID=pd.POID)NoPO, (select SupplierName from SuppPurch where ID=( " +
                                                       //                "(select SupplierID from POPurchn where ID=pd.POID)))SupplierName,  pd.POID,pd.Qty " +
                                                       //                ",isnull((Select SUM(Quantity) From ReceiptDetail where RowStatus>-1 and ReceiptDetail.PODetailID= " +
                                                       //                "pd.ID group by PODetailID),0)RCP " +
                                                       //                "from POPurchnDetail pd  " +
                                                       //                "where pd.POID in " +
                                                       //                "(select POID from POPurchnDetail where ItemID=(select ItemID from MemoHarian where ID=" + this.schID + ") " +
                                                       //                "and Status>-2)  " +
                                                       //                "and pd.Status>-1  " +
                                                       //                "and pd.Qty > isnull((Select SUM(Quantity) from ReceiptDetail where RowStatus>-1 and ReceiptDetail.PODetailID=pd.ID group by PODetailID),0) " +
                                                       //                "order by POID desc";
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

        public RMM_Detail GetDetailRMM()
        {
            rmmdtl = new RMM_Detail();//po = new POPurchn();
            string strsql = "select * from RMM_Detail where RowStatus>-1" + this.Criteria; ;//string strsql = "Select * from MemoHarian_PO where RowStatus>-1 " + this.Criteria;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strsql);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    return this.gObject(sdr);
                }
            }
            return rmmdtl;
        }

        private RMM_Detail gObject(SqlDataReader sdr)
        {
            rmmdtl = new RMM_Detail();
            switch (this.Field)
            {
                case "RMMD":
                    rmmdtl.ID = Convert.ToInt32(sdr["ID"].ToString()); //po.POID = Convert.ToInt32(sdr["POID"].ToString());
                                                                       //rmmdtl.RMM_No = sdr["RMM_No"].ToString(); //po.NoPO = sdr["NoPO"].ToString();
                                                                       //po.SupplierName = sdr["SupplierName"].ToString();
                    break;
                //    //case "PODetail":
                //    //    po.ID = Convert.ToInt32(sdr["ID"].ToString());
                //    //    po.SupplierName = sdr["SupplierName"].ToString();
                //    //    po.ItemCode = sdr["ItemCode"].ToString();
                //    //    po.ItemName = sdr["ItemName"].ToString();
                //    //    po.QtyReceipt = Convert.ToDecimal(sdr["OutPO"].ToString());
                //    //    po.Qty = Convert.ToDecimal(sdr["QtyMobil"].ToString());
                //    //    po.QtyPO = Convert.ToDecimal(sdr["EstQty"].ToString());
                //    //    po.SupplierID = Convert.ToInt32(sdr["SupplierID"].ToString());
                //    //    po.ItemID = Convert.ToInt32(sdr["ItemID"].ToString());
                //    //    po.Delivery = sdr["Delivery"].ToString();
                //    //    break;
                case "RmmDetail":
                    rmmdtl.Aktivitas = sdr["Aktivitas"].ToString();
                    rmmdtl.Ket = sdr["Ket"].ToString();
                    //rmmdtl.RMM_No = sdr["RMM_No"].ToString();//po.ID = Convert.ToInt32(sdr["ID"].ToString());
                    //po.POID = Convert.ToInt32(sdr["POID"].ToString());
                    //po.Qty = Convert.ToDecimal(sdr["QtyMobil"].ToString());
                    //po.QtyPO = Convert.ToDecimal(sdr["EstQty"].ToString());
                    //po.Keterangan = sdr["Keterangan"].ToString();
                    break;
            }
            return rmmdtl;
        }
    }
}