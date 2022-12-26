using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Reflection;
using BusinessFacade;
using Domain;
using DataAccessLayer;

namespace GRCweb1.ModalDialog
{
    public partial class PODistrribusiDialog : System.Web.UI.Page
    {
        private POPurchn pp = new POPurchn();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string POID = Request.QueryString["p"].ToString();
                LoadPO(POID);
            }
        }

        private void LoadPO(string IDn)
        {
            PODist po = new PODist();
            po.Criteria = " where PO.ID=" + IDn;
            POPurchn p = po.RetrieveDetail();
            NoPO.Text = p.NoPO.ToString();
            txtSupplierName.Text = p.SupplierName.ToString();
            if (p.ID > 0)
            {
                ddlKirim.SelectedValue = p.KirimVia.ToString();
                txtKirimDate.Text = (p.KirimDate.Year < 1902) ? "" : p.KirimDate.ToString("dd-MM-yyyy");
                txtTerimaBy.Text = p.TerimaBy.ToString();
                txtTerimaDate.Text = (p.TerimaDate.Year < 1902) ? "" : p.TerimaDate.ToString("dd-MM-yyyy");
                txtEstDelivery.Text = (p.EstDelivery.Year < 1902) ? "" : p.EstDelivery.ToString("dd-MM-yyyy");
                txtActDelivery.Text = (p.ActDelivery.Year < 1902) ? "" : p.ActDelivery.ToString("dd-MM-yyyy");
                txtKeterangan.Text = p.Keterangan.ToString();
                txtID.Text = p.ID.ToString();
            }

        }
        protected void btnSimpan_onserverclick(object sender, EventArgs e)
        {
            Session["Update"] = null;
            ArrayList arrData = new ArrayList();
            pp.POID = int.Parse(Request.QueryString["p"].ToString());
            pp.KirimVia = ddlKirim.SelectedValue.ToString();
            pp.KirimDate = DateTime.Parse(txtKirimDate.Text);
            pp.TerimaBy = txtTerimaBy.Text;
            pp.TerimaDate = (txtTerimaDate.Text == string.Empty) ? DateTime.MinValue.AddYears(1900) : DateTime.Parse(txtTerimaDate.Text);
            pp.EstDelivery = (txtEstDelivery.Text == string.Empty) ? DateTime.MinValue.AddYears(1900) : DateTime.Parse(txtEstDelivery.Text);
            pp.ActDelivery = (txtActDelivery.Text == string.Empty) ? DateTime.MinValue.AddYears(1900) : DateTime.Parse(txtActDelivery.Text);
            pp.Keterangan = txtKeterangan.Text;
            if (txtID.Text != string.Empty)
            {
                pp.ID = int.Parse(txtID.Text);
                arrData.Add(pp);
                Session["Update"] = arrData;
                Response.Write("<script language='javascript'>window.close();</script>");
            }
            else
            {
                arrData.Add(pp);
                Session["Update"] = arrData;
                Response.Write("<script language='javascript'>window.close();</script>");
            }



        }

    }


    public class PODist
    {
        private ArrayList arrData = new ArrayList();
        private POPurchn p = new POPurchn();
        public string Criteria { get; set; }
        public string POID { get; set; }
        private string Query()
        {
            return "Select PO.NoPO,(Select SuppPurch.SupplierName from SuppPurch where ID=PO.SupplierID)SupplierName, " +
                   " PO.POPurchnDate,PO.Approval, PD.* from  " +
                   " POPurchn as PO " +
                   " LEFT JOIN PODistribusi as PD " +
                   " ON PD.POID=PO.ID  " + this.Criteria;
        }
        public POPurchn RetrieveDetail()
        {
            p = new POPurchn();
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(this.Query());
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    p.NoPO = sdr["NoPO"].ToString();
                    p.POPurchnDate = Convert.ToDateTime(sdr["POPurchnDate"].ToString());
                    p.SupplierName = sdr["SupplierName"].ToString();
                    if (sdr["ID"] != DBNull.Value)
                    {
                        p.ID = Convert.ToInt32(sdr["ID"].ToString());
                        p.KirimVia = sdr["KirimVia"].ToString();
                        p.KirimDate = Convert.ToDateTime(sdr["KirimDate"].ToString());
                        p.TerimaBy = sdr["TerimaBy"].ToString();
                        p.TerimaDate = Convert.ToDateTime(sdr["TerimaDate"].ToString());
                        p.EstDelivery = Convert.ToDateTime(sdr["EstDelivery"].ToString());
                        p.ActDelivery = Convert.ToDateTime(sdr["ActDelivery"].ToString());
                    }
                }
            }
            return p;
        }
    }
}