using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using BusinessFacade;
using Domain;

namespace GRCweb1.Modul.ListReport
{
    public partial class RekapPakaiDeptItem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.link = "~/Default.aspx";
            if (!Page.IsPostBack)
            {
                txtTgl1.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                txtTgl2.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                LoadTipeBarang();
                //LoadTipeSPP();
                LoadDept();

            }
        }

        private void LoadTipeBarang()
        {
            ArrayList arrItemTypePurchn = new ArrayList();
            ItemTypePurchnFacade itemTypePurchnFacade = new ItemTypePurchnFacade();
            arrItemTypePurchn = itemTypePurchnFacade.Retrieve();

            ddlTipeBarang.Items.Add(new ListItem("-- Pilih Tipe Barang --", string.Empty));
            foreach (ItemTypePurchn itemTypePurchn in arrItemTypePurchn)
            {
                ddlTipeBarang.Items.Add(new ListItem(itemTypePurchn.TypeDescription, itemTypePurchn.ID.ToString()));
            }
        }

        //private void LoadTipeSPP()
        //{
        //    ddlNamaBarang.Items.Clear();

        //    ArrayList arrGroupsPurchn = new ArrayList();
        //    GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
        //    //arrGroupsPurchn = groupsPurchnFacade.RetrieveByGroupID(((Users)Session["Users"]).GroupID);
        //    arrGroupsPurchn = groupsPurchnFacade.Retrieve();

        //    ddlNamaBarang.Items.Add(new ListItem("-- Pilih Nama Barang --", string.Empty));
        //    //foreach (GroupsPurchn groupsPurchn in arrGroupsPurchn)
        //    //{
        //    //    ddlNamaBarang.Items.Add(new ListItem(groupsPurchn.GroupDescription, groupsPurchn.ID.ToString()));
        //    //}

        //}


        private void LoadDept()
        {
            ddlDeptName.Items.Clear();

            ArrayList arrDeptName = new ArrayList();
            DeptFacade deptFacade = new DeptFacade();
            arrDeptName = deptFacade.Retrieve();

            ddlDeptName.Items.Add(new ListItem("-- Pilih Dept --", string.Empty));
            foreach (Dept dept in arrDeptName)
            {
                ddlDeptName.Items.Add(new ListItem(dept.DeptName, dept.ID.ToString()));
            }

        }

        protected void txtCari_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCari.Text != string.Empty)
                {
                    if (ddlTipeBarang.SelectedIndex == 0)
                    {
                        DisplayAJAXMessage(this, "Tipe Barang tidak boleh kosong");
                        txtCari.Text = string.Empty;
                        return;
                    }

                    LoadItem(txtCari.Text);

                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                DisplayAJAXMessage(this, "Material Tidak di temukan, Gunakan kata yang lain");
            }
        }

        private void LoadItem(string strNabar)
        {
            ddlNamaBarang.Items.Clear();
            ArrayList arrItems = new ArrayList();

            InventoryFacade inventoryFacade = new InventoryFacade();
            AssetFacade assetFacade = new AssetFacade();
            BiayaFacade biayaFacade = new BiayaFacade();
            Session["tahun"] = DateTime.Parse(txtTgl1.Text).Year.ToString();
            if (ddlTipeBarang.SelectedIndex == 1)
            {
                arrItems = new ArrayList();
                arrItems = inventoryFacade.RetrieveByNameTypeID("ItemName", strNabar, int.Parse(ddlTipeBarang.SelectedValue));
            }
            if (ddlTipeBarang.SelectedIndex == 2)
            {
                arrItems = new ArrayList();
                arrItems = assetFacade.RetrieveByNameTypeID("ItemName", strNabar, int.Parse(ddlTipeBarang.SelectedValue));
            }
            if (ddlTipeBarang.SelectedIndex == 3)
            {
                arrItems = new ArrayList();
                arrItems = biayaFacade.RetrieveByNameTypeID("ItemName", strNabar, int.Parse(ddlTipeBarang.SelectedValue));
            }
            ddlNamaBarang.Items.Add(new ListItem("-- Pilih Items --", "0"));

            if (ddlTipeBarang.SelectedIndex == 1)
            {
                foreach (Inventory inventory in arrItems)
                {
                    ddlNamaBarang.Items.Add(new ListItem(inventory.ItemName + " (" + inventory.ItemCode + ")", inventory.ID.ToString()));
                }
            }
            if (ddlTipeBarang.SelectedIndex == 2)
            {
                foreach (Asset asset in arrItems)
                {
                    ddlNamaBarang.Items.Add(new ListItem(asset.ItemName + " (" + asset.ItemCode + ")", asset.ID.ToString()));
                }
            }
            if (ddlTipeBarang.SelectedIndex == 3)
            {
                foreach (Biaya biaya in arrItems)
                {
                    ddlNamaBarang.Items.Add(new ListItem(biaya.ItemName + " (" + biaya.ItemCode + ")", biaya.ID.ToString()));
                }
            }
        }

        protected void ddlNamaBarang_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////DropDownList ddl = (DropDownList)sender;
            //Users users = (Users)Session["Users"];

            //if (ddlNamaBarang.SelectedIndex > 0)
            //{
            //    //GridViewRow row = (GridViewRow)ddl.NamingContainer;

            //    if (ddlTipeBarang.SelectedIndex == 1)
            //    {
            //        InventoryFacade inventoryFacade = new InventoryFacade();
            //        Inventory inventory = inventoryFacade.RetrieveById(int.Parse(ddlNamaBarang.SelectedValue));
            //        if (inventoryFacade.Error == string.Empty)
            //        {
            //            if (inventory.ID > 0)
            //            {
            //                //TextBox txtSatuan = (TextBox)GridView1.Rows[row.RowIndex].FindControl("txtSatuan");
            //                //TextBox txtKodeBarang = (TextBox)GridView1.Rows[row.RowIndex].FindControl("txtKodeBarang");
            //                //TextBox txtQty = (TextBox)GridView1.Rows[row.RowIndex].FindControl("txtQty");

            //                txtStok.Text = inventory.Jumlah.ToString("N2");
            //                txtJmlMax.Text = inventory.MaxStock.ToString("N2");
            //                txtSatuan.Text = inventory.UomCode;
            //                txtKodeBarang.Text = inventory.ItemCode;
            //                txtQty.Text = string.Empty;
            //                txtKeterangan.Focus();
            //            }
            //        }
            //    }
            //    else
            //    {
            //        if (ddlTipeBarang.SelectedIndex == 2)
            //        {
            //            AssetFacade assetFacade = new AssetFacade();
            //            Asset asset = assetFacade.RetrieveById(int.Parse(ddlNamaBarang.SelectedValue));
            //            if (assetFacade.Error == string.Empty)
            //            {
            //                if (asset.ID > 0)
            //                {
            //                    //TextBox txtSatuan = (TextBox)GridView1.Rows[row.RowIndex].FindControl("txtSatuan");
            //                    //TextBox txtKodeBarang = (TextBox)GridView1.Rows[row.RowIndex].FindControl("txtKodeBarang");
            //                    //TextBox txtQty = (TextBox)GridView1.Rows[row.RowIndex].FindControl("txtQty");

            //                    txtStok.Text = asset.Jumlah.ToString("N2");
            //                    txtJmlMax.Text = asset.MaxStock.ToString("N2");
            //                    txtSatuan.Text = asset.UomCode;
            //                    txtKodeBarang.Text = asset.ItemCode;
            //                    txtQty.Text = string.Empty;
            //                    txtKeterangan.Focus();
            //                }
            //            }
            //        }
            //        else
            //        {
            //            BiayaFacade biayaFacade = new BiayaFacade();
            //            Biaya biaya = biayaFacade.RetrieveById(int.Parse(ddlNamaBarang.SelectedValue));
            //            if (biayaFacade.Error == string.Empty)
            //            {
            //                if (biaya.ID > 0)
            //                {
            //                    //TextBox txtSatuan = (TextBox)GridView1.Rows[row.RowIndex].FindControl("txtSatuan");
            //                    //TextBox txtKodeBarang = (TextBox)GridView1.Rows[row.RowIndex].FindControl("txtKodeBarang");
            //                    //TextBox txtQty = (TextBox)GridView1.Rows[row.RowIndex].FindControl("txtQty");

            //                    txtStok.Text = biaya.Jumlah.ToString("N2");
            //                    txtJmlMax.Text = biaya.MaxStock.ToString("N2");
            //                    txtSatuan.Text = biaya.UomCode;
            //                    txtKodeBarang.Text = biaya.ItemCode;
            //                    txtQty.Text = string.Empty;
            //                    txtKeterangan.Focus();
            //                }
            //            }
            //        }
            //    }
            //}
        }

        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            string strValidate = ValidateText();
            string strQuery = string.Empty;
            string allQuery = string.Empty;
            string drTgl = string.Empty;
            string sdTgl = string.Empty;
            string PdrTgl = string.Empty;
            string PsdTgl = string.Empty;
            drTgl = DateTime.Parse(txtTgl1.Text).ToString("yyyyMMdd");
            sdTgl = DateTime.Parse(txtTgl2.Text).ToString("yyyyMMdd");
            PdrTgl = DateTime.Parse(txtTgl1.Text).ToString("dd/MM/yyyy");
            PsdTgl = DateTime.Parse(txtTgl2.Text).ToString("dd/MM/yyyy");
            Session["drTgl"] = PdrTgl;
            Session["sdTgl"] = PsdTgl;
            Session["deptName"] = ddlNamaBarang.SelectedItem;
            Session["deptName"] = ddlDeptName.SelectedItem;
            Users users = (Users)Session["Users"];
            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }

            ReportFacade reportFacade = new ReportFacade();
            //"-- Pilih Dept --"
            if (ddlDeptName.SelectedIndex > 0 && (ddlNamaBarang.SelectedValue.ToString() != string.Empty))
            {
                Session["deptName"] = " DEPARTEMEN : " + ddlDeptName.SelectedItem + " DENGAN NAMA BARANG : " + ddlNamaBarang.SelectedItem.ToString();
                if (users.ViewPrice > 0)
                {
                    if (users.ViewPrice == 1)
                        allQuery = reportFacade.ViewRekapPakaiDeptItem(drTgl, sdTgl, Convert.ToInt32(ddlNamaBarang.SelectedValue.ToString()), Convert.ToInt32(ddlDeptName.SelectedValue), Convert.ToInt16(ddlTipeBarang.SelectedValue));
                    if (users.ViewPrice == 2)
                        allQuery = reportFacade.ViewRekapPakaiDeptItem2(drTgl, sdTgl, Convert.ToInt32(ddlNamaBarang.SelectedValue.ToString()), Convert.ToInt32(ddlDeptName.SelectedValue), Convert.ToInt16(ddlTipeBarang.SelectedValue));

                }
                else
                {
                    allQuery = reportFacade.ViewRekapPakaiDeptItemByPrice0(drTgl, sdTgl, Convert.ToInt32(ddlNamaBarang.SelectedValue.ToString()), Convert.ToInt32(ddlDeptName.SelectedValue), Convert.ToInt16(ddlTipeBarang.SelectedValue));
                }
            }
            if (ddlDeptName.SelectedIndex > 0 && (ddlNamaBarang.SelectedValue.ToString() == string.Empty))
            {
                Session["deptName"] = " DEPARTEMEN : " + ddlDeptName.SelectedItem;
                if (users.ViewPrice > 0)
                {
                    if (users.ViewPrice == 1)
                        allQuery = reportFacade.ViewRekapPakaiDept(drTgl, sdTgl, Convert.ToInt32(ddlDeptName.SelectedValue), Convert.ToInt16(ddlTipeBarang.SelectedValue));
                    if (users.ViewPrice == 2)
                        allQuery = reportFacade.ViewRekapPakaiDept2(drTgl, sdTgl, Convert.ToInt32(ddlDeptName.SelectedValue), Convert.ToInt16(ddlTipeBarang.SelectedValue));
                }
                else
                {
                    allQuery = reportFacade.ViewRekapPakaiDeptByPrice0(drTgl, sdTgl, Convert.ToInt32(ddlDeptName.SelectedValue), Convert.ToInt16(ddlTipeBarang.SelectedValue));
                }
            }
            if ((ddlDeptName.SelectedValue.ToString() == string.Empty) && (ddlNamaBarang.SelectedValue.ToString() != string.Empty))
            {
                Session["deptName"] = " DENGAN NAMA BARANG : " + ddlNamaBarang.SelectedItem.ToString();
                if (users.ViewPrice > 0)
                {
                    if (users.ViewPrice == 1)
                        allQuery = reportFacade.ViewRekapPakaiItem(drTgl, sdTgl, Convert.ToInt32(ddlNamaBarang.SelectedValue.ToString()), Convert.ToInt16(ddlTipeBarang.SelectedValue));
                    if (users.ViewPrice == 2)
                        allQuery = reportFacade.ViewRekapPakaiItem2(drTgl, sdTgl, Convert.ToInt32(ddlNamaBarang.SelectedValue.ToString()), Convert.ToInt16(ddlTipeBarang.SelectedValue));
                }
                else
                {
                    allQuery = reportFacade.ViewRekapPakaiItemByPrice0(drTgl, sdTgl, Convert.ToInt32(ddlNamaBarang.SelectedValue.ToString()), Convert.ToInt16(ddlTipeBarang.SelectedValue));
                }
            }
            strQuery = allQuery;
            Session["Query"] = strQuery;

            Cetak(this);
        }

        static public void Cetak(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../Report/Report.aspx?IdReport=RekapPakaiDeptItem', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
            string myScript = "Cetak();";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "MyScript", myScript, true);
        }

        private string ValidateText()
        {
            if (txtTgl1.Text == string.Empty)
                return "Dari Tanggal tidak boleh kosong";
            else if (txtTgl2.Text == string.Empty)
                return "s/d Tanggal tidak boleh kosong";

            //if (ddlDeptName.SelectedIndex == 0)
            //    return "Pilih Dept tidak boleh kosong";

            return string.Empty;
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void ddlTipeBarang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtCari.Text != string.Empty) LoadItem(txtCari.Text);
        }

    }
}