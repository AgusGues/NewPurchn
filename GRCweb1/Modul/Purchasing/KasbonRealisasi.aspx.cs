using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using System.Threading;
using System.Globalization;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
//using System.Drawing;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Web.Script.Serialization;

namespace GRCweb1.Modul.Purchasing
{
    public partial class KasbonRealisasi : System.Web.UI.Page
    {
        public decimal gHarga = 0;
        public decimal gPO = 0;
        public decimal gTotal = 0;
        public string ItemCD = "";
        public decimal DanaCadangan = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //DataTable dt = new DataTable();
                //dt.Columns.AddRange(new DataColumn[1] {
                //    new DataColumn("TotalPO", typeof(decimal)) });
                //lstKasbon.DataSource = dt;
                //lstKasbon.DataBind();
                //int Total = dt.Select().Sum(p => Convert.ToInt32(p["TotalPO"]));
                //(lstKasbon.Controls[lstKasbon.Controls.Count - 1].Controls[0].FindControl("gTotal2") as Label).Text = Total.ToString();

                Users users = (Users)Session["Users"];
                if (ddlKasbonNO.SelectedValue.ToString() == string.Empty)
                {
                    LoadKasbon();
                }
                txtDept.Text = "Purchasing";

                if (Request.QueryString["NoPengajuan"] != null)
                {
                    LoadKasbon(Request.QueryString["NoPengajuan"].ToString());
                    btnUpdate.Enabled = false;

                    int Apv = 0;
                    int CetakRealisasi = 0;
                    ZetroView zl = new ZetroView();
                    zl.QueryType = Operation.CUSTOM;
                    zl.CustomQuery = "SELECT Approval,CetakRealisasi FROM Kasbon WHERE NoPengajuan='" + Request.QueryString["NoPengajuan"].ToString() + "' ";
                    SqlDataReader sdr = zl.Retrieve();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            Apv = Convert.ToInt32(sdr["Approval"].ToString());
                            CetakRealisasi = Convert.ToInt32(sdr["CetakRealisasi"].ToString());
                        }
                        if (users.DeptID == 15 && Apv == 3 && CetakRealisasi == 0)
                        {
                            btnPrint.Enabled = true;
                        }
                        else if (users.DeptID == 12 && Apv == 4 && CetakRealisasi == 1)
                        {
                            btnPrint.Enabled = true;
                        }
                        else
                        {
                            btnPrint.Enabled = false;
                        }
                    }

                }
            }
        }

        private void LoadKasbon()
        {
            ArrayList arrKasbon = new ArrayList();
            KasbonFacade kasbonFacade = new KasbonFacade();
            arrKasbon = kasbonFacade.RetrieveKasbon();
            ddlKasbonNO.Items.Add(new ListItem("-- Pilih No Kasbon --", string.Empty));
            //ddlSupPurch.Items.Add(new ListItem(" ", string.Empty));
            foreach (Kasbon kasbon in arrKasbon)
            {
                ddlKasbonNO.Items.Add(new ListItem(kasbon.NoKasbon, kasbon.ID.ToString()));
            }
        }
        private void LoadKasbon(string strNoPengajuan)
        {
            Users users = (Users)Session["Users"];
            KasbonFacade kasbonFacade = new KasbonFacade();

            Kasbon kasbon = kasbonFacade.RetrieveByNoWithKasbonNO(strNoPengajuan);
            //try
            //{
            if (kasbonFacade.Error == string.Empty && kasbon.ID > 0)
            {
                Session["id"] = kasbon.ID;
                txtDept.Text = "Purchasing";
                txtDate.Text = kasbon.KasbonDate.ToString();
                txtKasbonNo.Visible = true;
                txtKasbonNo.Text = kasbon.NoKasbon.ToString();
                ddlKasbonNO.Visible = false;
                txtDanaCadangan.Text = kasbon.DanaCadangan.ToString();
                txtPIC.Text = kasbon.PIC;

                KasbonFacade kasbonFacade3 = new KasbonFacade();
                ArrayList arrItemList = new ArrayList();
                arrItemList = kasbonFacade3.ViewGridR(kasbon.ID);
                //arrItemList.Add(kasbon);

                Session["NoKasbon"] = strNoPengajuan;
                //Session["ListOfKasbonDetail"] = arrItemList;
                lstKasbon.DataSource = arrItemList;
                lstKasbon.DataBind();
            }
            else
            {
                DisplayAJAXMessage(this, "No. Kasbon tersebut tidak bisa ditampilkan karena tidak ada, atau telah dicancel");
                return;
            }
            //}

            //catch (Exception ex)
            //{
            //    DisplayAJAXMessage(this, ex.Message);
            //}
        }
        protected void ddlKasbonNO_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl = (DropDownList)sender;
                Users users = (Users)Session["Users"];
                KasbonFacade kasbonFacade = new KasbonFacade();
                Kasbon kasbon = kasbonFacade.RetrieveByNoKasbon(ddlKasbonNO.SelectedValue.ToString());

                if (kasbonFacade.Error == string.Empty && kasbon.ID > 0)
                {
                    txtDate.Text = kasbon.KasbonDate.ToString("dd-MMM-yyyy");
                    txtDanaCadangan.Text = kasbon.DanaCadangan.ToString("###,##0.#0");
                    txtPIC.Text = kasbon.PIC;
                    txtKasbontype.Text = (kasbon.KasbonType == 0) ? "Biasa" : "Top Urgent";
                    //txtSPP.Text = kasbon.NoSPP;
                    if (kasbon.KasbonType == 0)
                    {
                        KasbonFacade kasbonFacade2 = new KasbonFacade();
                        ArrayList arrItemList = kasbonFacade2.ViewGridKasbon(kasbon.ID);

                        lstKasbon.DataSource = arrItemList;
                        lstKasbon.DataBind();
                    }
                    else
                    {
                        KasbonFacade kasbonFacade3 = new KasbonFacade();
                        ArrayList arrItemList = kasbonFacade3.ViewGridKasbon2(kasbon.ID);

                        lstKasbon.DataSource = arrItemList;
                        lstKasbon.DataBind();
                    }
                }
            }

            catch (Exception ex)
            {
                DisplayAJAXMessage(this, ex.Message);
            }
        }

        protected void btnList_ServerClick(object sender, EventArgs e)
        {
            Session["ListOfPODetail"] = null;
            Session["NoSPP"] = null;

            Response.Redirect("KasbonListRealisasi.aspx?approve=" + (((Users)Session["Users"]).GroupID));
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        protected void lstKasbon_DataBound(object sender, RepeaterItemEventArgs e)
        {
            DropDownList ddlpo = (DropDownList)e.Item.FindControl("ddlNopo");
            Label txtNoPO = (Label)e.Item.FindControl("txtNoPO");
            Label ItemPO = (Label)e.Item.FindControl("ItemPO");
            Label txtItemPO = (Label)e.Item.FindControl("txtItemPO");
            Label QtyPO = (Label)e.Item.FindControl("QtyPO");
            Label txtQtyPO = (Label)e.Item.FindControl("txtQtyPO");
            Label HargaPO = (Label)e.Item.FindControl("HargaPO");
            Label txtHargaPO = (Label)e.Item.FindControl("txtHargaPO");
            Label txtNamaBarang = (Label)e.Item.FindControl("txtNamaBarang");
            TextBox txtInputPO = (TextBox)e.Item.FindControl("txtInputPO");
            DropDownList ddlNamaBarangPO = (DropDownList)e.Item.FindControl("ddlNamaBarangPO");
            Label gTotal2 = (Label)e.Item.FindControl("gTotal2");
            TextBox txtTotalAllPO = (TextBox)e.Item.FindControl("txtTotalAllPO");
            TextBox txtPOInput = (TextBox)e.Item.FindControl("txtInputPO");
            //Label qty = (Label)e.Item.FindControl("qty");
            //Label estimasi = (Label)e.Item.FindControl("estimasi");
            if (txtKasbonNo.Text != string.Empty)
            {
                KasbonFacade sp = new KasbonFacade();
                //KasbonDetail kd = (KasbonDetail)e.Item.DataItem;
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "SELECT distinct k.ID,kd.ID AS KDID,k.DeptID,u.UOMDesc,k.KasbonNo,k.NoPengajuan,s.NoSPP,p.NoPO,kd.ItemName,kd.ItemID,kd.UomID, " +
                                "kd.EstimasiKasbon,kd.Qty,(kd.EstimasiKasbon*kd.Qty) AS Total,pd.Qty AS QtyPO,pd.Price,(pd.Qty*pd.Price) AS TotalPO, " +
                                "k.DanaCadangan,k.Pic, k.CreatedTime,k.TglKasbon,k.Approval,k.Status,k.AlasanNotApproved FROM Kasbon as k " +
                                "left join KasbonDetail as kd on k.ID=kd.KID left join SPP as s on s.ID=kd.SPPID LEFT JOIN POPurchnDetail AS pd " +
                                "ON s.ID=pd.SPPID LEFT JOIN POPurchn AS p ON p.ID=pd.POID LEFT JOIN uom AS u ON u.id=kd.UomID WHERE k.ID in (SELECT ID FROM Kasbon WHERE KasbonNo='" + txtKasbonNo.Text.Trim() + "') AND " +
                                "k.Status>-1 and pd.ItemID=kd.ItemID and kd.PODetailID!=0 and kd.Status>-1 AND kd.EstimasiKasbon=pd.Price";
                SqlDataReader sdr = zl.Retrieve();
            }
            //if (ddlpo != null)
            //{
            else
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    KasbonFacade sp = new KasbonFacade();
                    Kasbon kd = (Kasbon)e.Item.DataItem;
                    if (kd.KasbonType == 0)
                    {
                        ddlNamaBarangPO.Visible = false;
                        txtInputPO.Visible = false;
                        if (ddlNamaBarangPO.Visible = true)
                        {
                            ddlNamaBarangPO.Visible = false;
                            ddlpo.Items.Clear();
                            ddlpo.Items.Add(new ListItem("Pilih PO", "0"));
                            foreach (Kasbon kasbon in sp.RetrieveNoPO(ddlKasbonNO.SelectedValue.ToString()))
                            {
                                ddlpo.Items.Add(new ListItem(kasbon.NoPo.ToString(), kasbon.ID.ToString()));
                            }
                            if (ddlpo.SelectedIndex == 0)
                            {
                                KasbonFacade kasbonFacade = new KasbonFacade();
                                Kasbon kdd = kasbonFacade.RetrieveByKasbon(ddlKasbonNO.SelectedValue.ToString());

                                gHarga = kdd.TotalEstimasi;
                            }
                            if (ddlpo.SelectedIndex != 1)
                            {
                                // if (txtItemPO.Text == "KERTAS KANTONG SEMEN" || txtItemPO.Text == "KERTAS KANTONG SEMEN MENTAH" ||
                                //txtItemPO.Text == "KERTAS KRAFT" || txtItemPO.Text == "KERTAS KRAFT MENTAH")
                                // {

                                // }
                                // else
                                // {
                                if (txtItemPO.Text == "KERTAS KANTONG SEMEN" || txtItemPO.Text == "KERTAS KANTONG SEMEN MENTAH" ||
                               txtItemPO.Text == "KERTAS KRAFT" || txtItemPO.Text == "KERTAS KRAFT MENTAH")
                                {
                                    //gPO += Convert.ToDecimal(txtQtyPO.Text) * Convert.ToDecimal(txtHargaPO.Text);
                                    ItemCD = txtItemPO.Text;
                                    //((Label)e.Item.FindControl("total")).Text = (Convert.ToDecimal(qty.Text) * Convert.ToDecimal(qty.Text)).ToString("###,##0.#0");
                                    ((Label)e.Item.FindControl("ItemPO")).Text = txtItemPO.Text;
                                    ((Label)e.Item.FindControl("QtyPO")).Text = Convert.ToDecimal(kd.QtyPO).ToString();
                                    ((Label)e.Item.FindControl("HargaPO")).Text = Convert.ToDecimal(kd.Price).ToString();
                                    ((Label)e.Item.FindControl("qty")).Text = Convert.ToDecimal(kd.Qty).ToString();
                                    ((Label)e.Item.FindControl("satuan")).Text = kd.Satuan.ToString();
                                    ddlpo.SelectedValue = txtItemPO.Text;
                                }
                                else
                                {
                                    KasbonFacade kasbonFacade2 = new KasbonFacade();
                                    Kasbon kdt = kasbonFacade2.RetrieveByPO(ddlKasbonNO.SelectedValue.ToString());
                                    gPO = kdt.TotalAllPO;
                                }
                            }
                        }
                        else
                        {
                            KasbonFacade POName = new KasbonFacade();
                            ddlpo.Visible = false;
                            txtInputPO.Visible = true;
                            txtItemPO.Visible = true;
                            txtHargaPO.Visible = true;
                            //((Label)e.Item.FindControl("txtInputPO")).Text = txtInputPO.Text;
                            ((Label)e.Item.FindControl("txtItemPO")).Text = txtItemPO.Text;
                            ((Label)e.Item.FindControl("QtyPO")).Text = txtQtyPO.Text;
                            ((Label)e.Item.FindControl("txtHargaPO")).Text = txtHargaPO.Text;
                            gPO += Convert.ToDecimal(txtHargaPO.Text);
                        }
                    }
                    else
                    {
                        KasbonFacade kasbonFacade = new KasbonFacade();
                        Kasbon kdd = kasbonFacade.RetrieveByKasbon(ddlKasbonNO.SelectedValue.ToString());

                        gHarga = kdd.TotalEstimasi;

                        Kasbon kdt = kasbonFacade.RetrieveByNamaBarangPO(ddlKasbonNO.SelectedValue.ToString());
                        gPO = kdt.TotalAllPO;
                        ddlpo.Visible = false;
                        txtInputPO.Visible = true;
                        ddlNamaBarangPO.Visible = true;

                        //((Label)e.Item.FindControl("txtInputPO")).Text = txtInputPO.Text;
                        ((Label)e.Item.FindControl("txtItemPO")).Text = txtItemPO.Text;
                        ((Label)e.Item.FindControl("QtyPO")).Text = txtQtyPO.Text;
                        ((Label)e.Item.FindControl("txtHargaPO")).Text = txtHargaPO.Text;
                        gPO += Convert.ToDecimal(txtHargaPO.Text);
                    }

                }
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    if (txtKasbontype.Text == "Top Urgent")
                    {
                        //HtmlGenericControl amount = (HtmlGenericControl)e.Item.FindControl("gTotal2");
                        ((Label)e.Item.FindControl("gTotal")).Text = gHarga.ToString("###,##0.#0");
                        //amount.InnerHtml = gPO.ToString("###,##0.#0");
                        ((Label)e.Item.FindControl("gTotal2")).Text = gPO.ToString("###,##0.#0");
                        ((Label)e.Item.FindControl("gDC")).Text = Convert.ToDecimal(txtDanaCadangan.Text).ToString("###,##0.#0");
                        //((Label)e.Item.FindControl("gDC2")).Text = Convert.ToDecimal(txtDanaCadangan.Text).ToString("###,##0.#0");
                        ((TextBox)e.Item.FindControl("grnTotal")).Text = (gHarga + Convert.ToDecimal(txtDanaCadangan.Text)).ToString("###,##0.#0");
                        ((Label)e.Item.FindControl("grnTotal2")).Text = ((gHarga + Convert.ToDecimal(txtDanaCadangan.Text)) - (gPO)).ToString("###,##0.#0");
                        gHarga += Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "total"));
                    }
                    else
                    {
                        Label gTotalK = (Label)e.Item.FindControl("gTotal2");
                        TextBox TotalAllPO = (TextBox)e.Item.FindControl("txtTotalAllPO");
                        gTotalK.Visible = true;
                        TotalAllPO.Visible = false;
                        ((Label)e.Item.FindControl("gTotal")).Text = gHarga.ToString("###,##0.#0");
                        ((Label)e.Item.FindControl("gDC")).Text = Convert.ToDecimal(txtDanaCadangan.Text).ToString("###,##0.#0");
                        ((TextBox)e.Item.FindControl("grnTotal")).Text = (gHarga + Convert.ToDecimal(txtDanaCadangan.Text)).ToString("###,##0.#0");
                        ((Label)e.Item.FindControl("gTotal2")).Text = gPO.ToString("###,##0.#0");
                        ((Label)e.Item.FindControl("grnTotal2")).Text = ((gHarga + Convert.ToDecimal(txtDanaCadangan.Text)) - (gPO)).ToString("###,##0.#0");
                        //((Label)e.Item.FindControl("gTotal2")).Visible = true;
                        //((TextBox)e.Item.FindControl("txtTotalAllPO")).Visible = false;
                    }
                }
            }
        }

        protected void txtTotalAllPO_TextChanged(object sender, EventArgs e)
        {
            TextBox tb1 = (TextBox)sender;
            RepeaterItem rp1 = ((RepeaterItem)(tb1.NamingContainer));

            TextBox AllPO = (TextBox)rp1.FindControl("txtTotalAllPO");
            TextBox AllRealisasi = (TextBox)rp1.FindControl("txtTotalRealisasi");
            //TextBox AllRealisasi = (TextBox)rp1.FindControl("txtTotalRealisasi");
            TextBox grnTotal = (TextBox)rp1.FindControl("grnTotal");
            for (int i = 0; i < lstKasbon.Items.Count; i++)
            {
                Label totalPO = (Label)lstKasbon.Items[i].FindControl("totalPO");
                gPO += Convert.ToDecimal(totalPO.Text);
            }
            AllPO.Text = gPO.ToString("###,##0.#0");
            AllRealisasi.Text = (Convert.ToDecimal(grnTotal.Text) - gPO).ToString("###,##0.#0");
        }

        protected void ddlNopo_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < lstKasbon.Items.Count; i++)
            {
                DropDownList ddlNopo = (DropDownList)lstKasbon.Items[i].FindControl("ddlNopo");
                Label ItemPO = (Label)lstKasbon.Items[i].FindControl("ItemPO");
                Label QtyPO = (Label)lstKasbon.Items[i].FindControl("QtyPO");
                Label txtItemPO = (Label)lstKasbon.Items[i].FindControl("txtItemPO");
                Label HargaPO = (Label)lstKasbon.Items[i].FindControl("HargaPO");
                Label SelisihKasbon = (Label)lstKasbon.Items[i].FindControl("SelisihKasbon");
                Label total = (Label)lstKasbon.Items[i].FindControl("total");
                Label totalPO = (Label)lstKasbon.Items[i].FindControl("totalPO");

                if (txtItemPO.Text == "KERTAS KANTONG SEMEN" || txtItemPO.Text == "KERTAS KANTONG SEMEN MENTAH" ||
                       txtItemPO.Text == "KERTAS KRAFT" || txtItemPO.Text == "KERTAS KRAFT MENTAH")
                {
                    KasbonFacade kasbonFacade = new KasbonFacade();
                    Kasbon kasbon = kasbonFacade.RetrieveByItemKertas(int.Parse(ddlNopo.SelectedValue));
                    if (ddlNopo.SelectedIndex > 0)
                    {
                        if (kasbon.ID > 0)
                            gPO += kasbon.TotalPO;
                        //ArrayList arr = new ArrayList();
                        KasbonFacade kasbonFacade2 = new KasbonFacade();
                        ArrayList arr = kasbonFacade2.RetrieveIDByPOKertas(int.Parse(ddlNopo.SelectedValue), int.Parse(ddlKasbonNO.SelectedValue));
                        lstKasbon.DataSource = arr;
                        lstKasbon.DataBind();
                    }
                    else
                    {
                        QtyPO.Text = "";
                    }
                }
                else
                {
                    KasbonFacade kasbonFacade = new KasbonFacade();
                    Kasbon kasbon = kasbonFacade.RetrieveByID(int.Parse(ddlNopo.SelectedValue));
                    if (ddlNopo.SelectedIndex > 0)
                    {
                        if (kasbon.ID > 0)
                            ItemPO.Text = kasbon.NamaBarang.ToString();
                        QtyPO.Text = kasbon.Qty.ToString();
                        HargaPO.Text = kasbon.Price.ToString("###,##0.#0");
                        totalPO.Text = kasbon.TotalPO.ToString("###,##0.#0");
                        SelisihKasbon.Text = (Convert.ToDouble(total.Text) - Convert.ToDouble(totalPO.Text)).ToString("###,##0.#0");
                        gPO += kasbon.TotalPO;
                    }
                    else
                    {
                        QtyPO.Text = "";
                    }
                }
            }
        }

        protected void txtInputPO_TextChanged(object sender, EventArgs e)
        {
            TextBox txts = (TextBox)sender;
            int n = int.Parse(txts.ToolTip);
            TextBox txtPOInput = (TextBox)lstKasbon.Items[n].FindControl("txtInputPO");
            DropDownList ddlNamaBarangPO = (DropDownList)lstKasbon.Items[n].FindControl("ddlNamaBarangPO");
            Label QtyPO = (Label)lstKasbon.Items[n].FindControl("QtyPO");
            Label txtItemPO = (Label)lstKasbon.Items[n].FindControl("txtItemPO");
            Label HargaPO = (Label)lstKasbon.Items[n].FindControl("HargaPO");
            Label SelisihKasbon = (Label)lstKasbon.Items[n].FindControl("SelisihKasbon");
            Label total = (Label)lstKasbon.Items[n].FindControl("total");
            Label totalPO = (Label)lstKasbon.Items[n].FindControl("totalPO");
            Label ItemTypeID = (Label)lstKasbon.Items[n].FindControl("ItemTypeID");
            KasbonFacade kasbonFacade2 = new KasbonFacade();
            ddlNamaBarangPO.Items.Clear();
            ddlNamaBarangPO.Items.Add(new ListItem("Pilih Nama Barang PO", "0"));
            foreach (Kasbon kasbon in kasbonFacade2.RetrieveIDByInputPO(txtPOInput.Text))
            {
                ddlNamaBarangPO.Items.Add(new ListItem(kasbon.NamaBarang.ToString(), kasbon.ID.ToString()));
                ItemTypeID.Text = kasbon.ItemTypeID.ToString();
                //QtyPO.Text = kasbon.QtyPO.ToString();
                //HargaPO.Text = kasbon.Price.ToString("###,##0.#0");
                //totalPO.Text = kasbon.TotalPO.ToString("###,##0.#0");
                //SelisihKasbon.Text = (Convert.ToDouble(total.Text) - Convert.ToDouble(totalPO.Text)).ToString("###,##0.#0");
                gPO += kasbon.TotalPO;
            }
        }

        protected void ddlNamaBarangPO_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < lstKasbon.Items.Count; i++)
            {
                //DropDownList txts = (DropDownList)sender;
                //int n = int.Parse(txts.ToolTip);
                TextBox txtPOInput = (TextBox)lstKasbon.Items[i].FindControl("txtInputPO");
                DropDownList ddlNamaBarangPO = (DropDownList)lstKasbon.Items[i].FindControl("ddlNamaBarangPO");
                Label QtyPO = (Label)lstKasbon.Items[i].FindControl("QtyPO");
                Label txtItemPO = (Label)lstKasbon.Items[i].FindControl("txtItemPO");
                Label HargaPO = (Label)lstKasbon.Items[i].FindControl("HargaPO");
                Label SelisihKasbon = (Label)lstKasbon.Items[i].FindControl("SelisihKasbon");
                Label total = (Label)lstKasbon.Items[i].FindControl("total");
                Label totalPO = (Label)lstKasbon.Items[i].FindControl("totalPO");
                Label ItemTypeID = (Label)lstKasbon.Items[i].FindControl("ItemTypeID");

                if (ddlNamaBarangPO.SelectedIndex > 0)
                {
                    KasbonFacade kasbonFacade = new KasbonFacade();
                    Kasbon kasbon = kasbonFacade.RetrieveByNamaBarangPO2(txtPOInput.Text, (ddlKasbonNO.SelectedItem.ToString()), (ddlNamaBarangPO.SelectedItem.ToString()), ItemTypeID.Text);
                    if (kasbon.ID > 0)
                        //KasbonFacade kasbonFacade2 = new KasbonFacade();
                        //ArrayList arr = kasbonFacade2.RetrieveByNamaBarangPO(txtPOInput.Text, (ddlKasbonNO.SelectedItem.ToString()));
                        //lstKasbon.DataSource = arr;
                        //lstKasbon.DataBind();
                        QtyPO.Text = kasbon.QtyPO.ToString();
                    HargaPO.Text = kasbon.Price.ToString("###,##0.#0");
                    totalPO.Text = (kasbon.QtyPO * kasbon.Price).ToString("###,##0.#0");
                    SelisihKasbon.Text = (Convert.ToDouble(total.Text) - Convert.ToDouble(totalPO.Text)).ToString("###,##0.#0");
                }
            }
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];
            KasbonFacade kasbonFacade = new KasbonFacade();
            Kasbon kasbon = new Kasbon();
            string strError = string.Empty;

            if (ddlKasbonNO.SelectedValue.ToString() != string.Empty)
            {
                for (int i = 0; i < lstKasbon.Items.Count; i++)
                {
                    Label KDID = (Label)lstKasbon.Items[i].FindControl("KDID");
                    DropDownList ddlNopo = (DropDownList)lstKasbon.Items[i].FindControl("ddlNopo");
                    Label QtyPO = (Label)lstKasbon.Items[i].FindControl("QtyPO");
                    Label HargaPO = (Label)lstKasbon.Items[i].FindControl("HargaPO");
                    DropDownList ddlNamaBarangPO = (DropDownList)lstKasbon.Items[i].FindControl("ddlNamaBarangPO");

                    kasbon.NoKasbon = ddlKasbonNO.SelectedValue.ToString();
                    kasbon.KDID = Convert.ToInt32(KDID.Text);
                    if (txtKasbontype.Text == "Biasa")
                    {
                        kasbon.POID = Convert.ToInt32(ddlNopo.SelectedValue);
                    }
                    else
                    {
                        kasbon.POID = Convert.ToInt32(ddlNamaBarangPO.SelectedValue);
                    }
                    kasbon.QtyPO = QtyPO.Text.ToString() == "" ? 0 : Convert.ToDecimal(QtyPO.Text.ToString());
                    kasbon.Price = HargaPO.Text.ToString() == "" ? 0 : Convert.ToDecimal(HargaPO.Text.ToString());

                    ZetroView zl = new ZetroView();
                    zl.QueryType = Operation.CUSTOM;
                    zl.CustomQuery = "UPDATE KasbonDetail SET PODetailID=" + kasbon.POID + ",HargaPO='" + kasbon.Price.ToString().Replace(",", ".") + "',QtyPO='" + kasbon.QtyPO.ToString().Replace(",", ".") + "' WHERE ID=" + kasbon.KDID + " ";
                    SqlDataReader sdr = zl.Retrieve();
                }
                ZetroView zl2 = new ZetroView();
                zl2.QueryType = Operation.CUSTOM;
                zl2.CustomQuery = "UPDATE Kasbon SET TglRealisasi=GETDATE(), CetakRealisasi='0' WHERE ID='" + kasbon.NoKasbon + "' ";
                SqlDataReader sdr2 = zl2.Retrieve();
            }
            if (strError == string.Empty)
            {
                Response.Redirect("KasbonRealisasi.Aspx");
            }
            else
            {
                DisplayAJAXMessage(this, strError);
            }
        }

        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];
            if (txtKasbonNo.Text.Trim() != string.Empty)
            {
                if (users.DeptID == 15)
                {
                    ZetroView zl = new ZetroView();
                    zl.QueryType = Operation.CUSTOM;
                    zl.CustomQuery = "UPDATE Kasbon SET CetakRealisasi='1' WHERE KasbonNo='" + txtKasbonNo.Text.Trim() + "' ";
                    SqlDataReader sdr = zl.Retrieve();
                }
                else
                {
                    ZetroView zl2 = new ZetroView();
                    zl2.QueryType = Operation.CUSTOM;
                    zl2.CustomQuery = "UPDATE Kasbon SET CetakRealisasi='2' WHERE KasbonNo='" + txtKasbonNo.Text.Trim() + "' ";
                    SqlDataReader sdr2 = zl2.Retrieve();
                }
            }
            string strQuery = "SELECT ID, KasbonNo, NoPengajuan, DeptID, Pic, Status, Approval, TglKasbon, DanaCadangan, AlasanNotApproved, ApprovedDate4, ApvMgr FROM Kasbon " +
                              "WHERE KasbonNo='" + txtKasbonNo.Text.Trim() + "' AND Status>-1 ";
            string strQuery1 = "SELECT DISTINCT k.ID,kd.ID AS KDID,u.UOMDesc,pd.DocumentNo,p.NoPO,p.PPN,p.Ongkos,kd.ItemName, " +
                                "kd.EstimasiKasbon,kd.Qty,pd.Qty AS QtyPO,pd.Price,k.DanaCadangan,k.Pic " +
                                "FROM Kasbon as k left join KasbonDetail as kd on k.ID=kd.KID " +
                                "LEFT JOIN POPurchnDetail AS pd ON pd.ID=kd.PODetailID LEFT JOIN POPurchn AS p ON p.ID=pd.POID LEFT JOIN uom AS u " +
                                "ON u.id=kd.UomID WHERE kd.KID IN (SELECT ID FROM Kasbon WHERE KasbonNo='" + txtKasbonNo.Text.Trim() + "') AND kd.Status>-1";

            Session["Query"] = strQuery;
            Session["Query1"] = strQuery1;
            Cetak(this);
        }

        static public void Cetak(Control page)
        {
            string myScript = "var wn = window.showModalDialog('../report/Report.aspx?IdReport=kasbonRealisasi', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 980px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
    }
}