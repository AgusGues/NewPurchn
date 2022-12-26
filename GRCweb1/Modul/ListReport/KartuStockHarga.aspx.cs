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
    public partial class KartuStockHarga : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.link = "~/Default.aspx";
            if (!Page.IsPostBack)
            {
                txtTahun1.Text = (Convert.ToInt32(DateTime.Now.Date.ToString("yyyy"))).ToString();
            }
        }



        protected void txtCari_TextChanged(object sender, EventArgs e)
        {

            if (txtCari.Text != string.Empty)
            {
                LoadItem(txtCari.Text);
            }
        }

        private void LoadItem(string strNabar)
        {
            ddlNamaBarang.Items.Clear();
            ArrayList arrItems = new ArrayList();
            InventoryFacade inventoryFacade = new InventoryFacade();
            arrItems = inventoryFacade.RetrieveByKartuStock(strNabar);
            ddlNamaBarang.Items.Add(new ListItem("-- Pilih Items --", "0"));
            foreach (Inventory inventory in arrItems)
            {
                ddlNamaBarang.Items.Add(new ListItem(inventory.ItemName + " (" + inventory.ItemCode + ")", inventory.ItemTypeID.ToString() + inventory.ItemCode));
            }
        }

        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            string strValidate = ValidateText();
            string strQuery = string.Empty;
            string strbar = string.Empty;
            //Session["drTgl"] = PdrTgl;
            //Session["sdTgl"] = PsdTgl;
            InventoryFacade inventoryFacade = new InventoryFacade();
            strbar = " itemtypeid=" + ddlNamaBarang.SelectedValue.Substring(0, 1) +
                    " and itemcode='" + ddlNamaBarang.SelectedValue.Substring(1, 15);
            Inventory inventory = inventoryFacade.RetrieveByKartuStockItemID(strbar);

            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }
            ReportFacade reportFacade = new ReportFacade();
            string tgl1 = string.Empty;
            string tgl2 = string.Empty;
            string itemid = string.Empty;
            string itemtypeid = string.Empty;
            string tglSA = string.Empty;
            string yearSA = string.Empty;
            string monthSA = string.Empty;
            int groupID = 0;
            if (RadioButton1.Checked == true)
            {
                tgl1 = txtTahun1.Text.Trim() + ddlBulan1.SelectedValue + "01";
                if (ddlBulan1.SelectedIndex == 11)
                    tgl2 = (Convert.ToInt16(txtTahun1.Text) + 1).ToString() + "0101";
                else
                {
                    ddlBulan1.SelectedIndex = ddlBulan1.SelectedIndex + 1;
                    tgl2 = txtTahun1.Text.Trim() + ddlBulan1.SelectedValue + "01";
                    ddlBulan1.SelectedIndex = ddlBulan1.SelectedIndex - 1;
                }
            }
            if (RadioButton2.Checked == true)
            {
                tgl1 = txtTahun1.Text.Trim() + ddlBulan1.SelectedValue + "01";
                if (ddlBulan2.SelectedIndex == 11)
                    tgl2 = (Convert.ToInt16(txtTahun2.Text) + 1).ToString() + "0101";
                else
                {
                    ddlBulan2.SelectedIndex = ddlBulan2.SelectedIndex + 1;
                    tgl2 = txtTahun2.Text.Trim() + ddlBulan2.SelectedValue + "01";
                    ddlBulan2.SelectedIndex = ddlBulan2.SelectedIndex - 1;
                }
            }
            if (RadioButton3.Checked == true)
            {
                tgl1 = txtTahun1.Text.Trim() + "0101";
                tgl2 = (Convert.ToInt16(txtTahun1.Text) + 1).ToString() + "0101";
                ddlBulan1.SelectedIndex = 0;
            }
            itemid = inventory.ID.ToString();
            groupID = inventory.GroupID;
            itemtypeid = inventory.ItemTypeID.ToString();
            tglSA = ddlBulan1.SelectedValue + "/01/" + txtTahun1.Text;
            if (ddlBulan1.SelectedIndex == 0)
                yearSA = (Convert.ToInt16(txtTahun1.Text) - 1).ToString();
            else
                yearSA = txtTahun1.Text;
            switch (ddlBulan1.SelectedIndex)
            {
                case 1:
                    monthSA = "janqty";
                    break;
                case 2:
                    monthSA = "febqty";
                    break;
                case 3:
                    monthSA = "marqty";
                    break;
                case 4:
                    monthSA = "aprqty";
                    break;
                case 5:
                    monthSA = "meiqty";
                    break;
                case 6:
                    monthSA = "junqty";
                    break;
                case 7:
                    monthSA = "julqty";
                    break;
                case 8:
                    monthSA = "aguqty";
                    break;
                case 9:
                    monthSA = "sepqty";
                    break;
                case 10:
                    monthSA = "oktqty";
                    break;
                case 11:
                    monthSA = "novqty";
                    break;
                case 0:
                    monthSA = "desqty";
                    break;
            }
            if (groupID < 10)
                strQuery = reportFacade.ViewKartuStockHarga(tgl1, tgl2, itemid, itemtypeid, tglSA, yearSA, monthSA);
            else
                strQuery = reportFacade.ViewKartuStockRepack(tgl1, tgl2, itemid, itemtypeid, tglSA, yearSA, monthSA);
            Session["Query"] = strQuery;
            Session["itemcode"] = inventory.ItemCode.ToString();
            Session["itemname"] = inventory.ItemName.ToString();
            Session["satuan"] = inventory.UOMDesc.ToString();
            if (inventory.ItemCode.Substring(6, 1) == "0")
                Session["type"] = "NON STOCK";
            else
                Session["type"] = "STOCK";

            Session["minstock"] = inventory.MinStock.ToString();
            Session["maxstock"] = inventory.MaxStock.ToString();
            Session["reorder"] = inventory.ReOrder.ToString();
            if (RadioButton1.Checked == true)
                Session["periode"] = ddlBulan1.SelectedItem.Text + " " + txtTahun1.Text;
            else
                Session["periode"] = ddlBulan1.SelectedItem.Text + " " + txtTahun1.Text + " s/d " + ddlBulan2.SelectedItem.Text + " " + txtTahun2.Text;
            Cetak(this);
        }

        static public void Cetak(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../Report/Report.aspx?IdReport=kartustockharga', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
            string myScript = "Cetak();";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "MyScript", myScript, true);
        }

        private string ValidateText()
        {
            if (txtTahun1.Text == string.Empty)
                return "Tahun tidak boleh kosong";

            return string.Empty;
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton2.Checked == true)
            {
                Label3.Visible = true;
                ddlBulan1.Visible = true;
                ddlBulan2.Visible = true;
                txtTahun2.Visible = true;
                lblBulan.Text = "Dari ";
                loadBulan2();
            }
        }
        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton1.Checked == true)
            {
                Label3.Visible = false;
                ddlBulan1.Visible = true;
                ddlBulan2.Visible = false;
                txtTahun2.Visible = false;
                lblBulan.Text = "Dari ";
            }
        }
        protected void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButton3.Checked == true)
            {
                Label3.Visible = false;
                ddlBulan2.Visible = false;
                txtTahun2.Visible = false;
                ddlBulan1.Visible = false;
                lblBulan.Text = "Tahun ";

            }
        }
        protected void ddlBulan1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadBulan2();
        }
        protected void txtTahun1_TextChanged(object sender, EventArgs e)
        {
            loadBulan2();
        }
        private void loadBulan2()
        {
            if (ddlBulan1.SelectedIndex <= 6)
            {
                ddlBulan2.SelectedIndex = ddlBulan1.SelectedIndex + 5;
                txtTahun2.Text = txtTahun1.Text;
            }
            else
            {
                ddlBulan2.SelectedIndex = ddlBulan1.SelectedIndex - 7;
                txtTahun2.Text = (Convert.ToInt16(txtTahun1.Text) + 1).ToString();
            }
        }

        protected void ddlNamaBarang_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}