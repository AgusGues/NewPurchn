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
    public partial class KartuStock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.link = "~/Default.aspx";
            if (!Page.IsPostBack)
            {
                txtTahun1.Text = DateTime.Now.Year.ToString();//(Convert.ToInt32(DateTime.Now.Date.ToString("yyyy"))-1).ToString();
                ddlBulan1.SelectedValue = DateTime.Now.Month.ToString().PadLeft(2, '0');
            }
        }



        protected void txtCari_TextChanged(object sender, EventArgs e)
        {

            if (txtCari.Text != string.Empty)
            {
                LoadItem(txtCari.Text);
                lstPrev.Visible = false;
            }
        }

        private void LoadItem(string strNabar)
        {
            ddlNamaBarang.Items.Clear();
            ArrayList arrItems = new ArrayList();
            InventoryFacade inventoryFacade = new InventoryFacade();
            if (ChkNonAktif.Checked == false)
                arrItems = inventoryFacade.RetrieveByKartuStock(strNabar);
            else
                arrItems = inventoryFacade.RetrieveByKartuStockWithNonAktif(strNabar);
            ddlNamaBarang.Items.Add(new ListItem("-- Pilih Items --", "0"));
            foreach (Inventory inventory in arrItems)
            {
                ddlNamaBarang.Items.Add(new ListItem(inventory.ItemName + " (" + inventory.ItemCode + ")", inventory.ItemTypeID.ToString()
                    + inventory.ID));
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
            if (ddlNamaBarang.SelectedValue.Trim().Length >= 15)
            {
                strbar = " itemtypeid=" + ddlNamaBarang.SelectedValue.Substring(0, 1) +
                    " and itemID='" + ddlNamaBarang.SelectedValue.Substring(1, 15);
            }
            else
            {
                strbar = " itemtypeid=" + ddlNamaBarang.SelectedValue.Substring(0, 1) +
                                " and A.ID='" + ddlNamaBarang.SelectedValue.Substring(1, ddlNamaBarang.SelectedValue.Trim().Length - 1);
            }
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
            if (groupID == 10)
            {
                strQuery = reportFacade.ViewKartuStockRepack(tgl1, tgl2, itemid, itemtypeid, tglSA, yearSA, monthSA);
            }
            else
            {
                strQuery = reportFacade.ViewKartuStock(tgl1, tgl2, itemid, itemtypeid, tglSA, yearSA, monthSA);
            }
            Session["Query"] = strQuery;
            Session["itemcode"] = inventory.ItemCode.ToString();
            Session["itemname"] = inventory.ItemName.ToString();
            Session["satuan"] = inventory.UOMDesc.ToString();
            if (inventory.Stock == 0)
                Session["type"] = "NON STOCK";
            else
                Session["type"] = "STOCK";

            Session["minstock"] = inventory.MinStock.ToString();
            Session["maxstock"] = inventory.MaxStock.ToString();
            Session["reorder"] = inventory.ReOrder.ToString();
            Cetak(this);
        }

        static public void Cetak(Control page)
        {
            string myScript = "var wn = window.showModalDialog('../Report/Report.aspx?IdReport=kartustock', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
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
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            string strValidate = ValidateText();
            string strQuery = string.Empty;
            string strbar = string.Empty;

            InventoryFacade inventoryFacade = new InventoryFacade();
            if (ddlNamaBarang.SelectedValue.Trim().Length >= 15)
            {
                strbar = " itemtypeid=" + ddlNamaBarang.SelectedValue.Substring(0, 1) +
                    " and itemID='" + ddlNamaBarang.SelectedValue.Substring(1, 15);
            }
            else
            {
                strbar = " itemtypeid=" + ddlNamaBarang.SelectedValue.Substring(0, 1) +
                                " and A.ID='" + ddlNamaBarang.SelectedValue.Substring(1, ddlNamaBarang.SelectedValue.Trim().Length - 1);
            }
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
            yearSA = (ddlBulan1.SelectedIndex == 0) ? (Convert.ToInt16(txtTahun1.Text) - 1).ToString() : txtTahun1.Text;
            switch (ddlBulan1.SelectedIndex)
            {
                case 1: monthSA = "janqty"; break;
                case 2: monthSA = "febqty"; break;
                case 3: monthSA = "marqty"; break;
                case 4: monthSA = "aprqty"; break;
                case 5: monthSA = "meiqty"; break;
                case 6: monthSA = "junqty"; break;
                case 7: monthSA = "julqty"; break;
                case 8: monthSA = "aguqty"; break;
                case 9: monthSA = "sepqty"; break;
                case 10: monthSA = "oktqty"; break;
                case 11: monthSA = "novqty"; break;
                case 0: monthSA = "desqty"; break;
            }

            strQuery = (groupID != 10) ? reportFacade.ViewKartuStock(tgl1, tgl2, itemid, itemtypeid, tglSA, yearSA, monthSA) :
                                      reportFacade.ViewKartuStockRepack(tgl1, tgl2, itemid, itemtypeid, tglSA, yearSA, monthSA);

            txtItemCode.Text = inventory.ItemCode.ToString();
            txtItemName.Text = inventory.ItemName.ToString();
            txtUnit.Text = inventory.UOMDesc.ToString();
            txtMatType.Text = (inventory.Stock == 0) ? "NON STOCK" : "STOCK";

            txtMin.Text = inventory.MinStock.ToString("N2");
            txtMax.Text = inventory.MaxStock.ToString("N2");
            txtRe.Text = inventory.ReOrder.ToString("N2");
            //string strsql = "WITH KartuStock AS( " + strQuery.Replace("SELECT * FROM (", "").Replace(") as x order by Tanggal,Tipe", "");
            //strsql += "), KartuStock2 AS (select ROW_NUMBER() OVER(ORDER BY tipe,id) IDn, * from kartustock) " +
            //         "SELECT  *, Case  when A.IDn=1 then A.masuk ELSE (select SUM(Masuk) from KartuStock2 where " +
            //         "IDn<= A.IDn)-(select SUM(Keluar) from KartuStock2 where " +
            //         "IDn<= A.IDn) end Saldo from KartuStock2 as A Order by tipe ";
            string strsql = "WITH KartuStock AS( " + strQuery.Replace("SELECT * FROM (", "").Replace(") as x order by Tanggal,Tipe", "");
            strsql += "), KartuStock2 AS (select ROW_NUMBER() OVER(ORDER BY urut,tanggal,tipe,id) IDn, * from kartustock) " +
                     "SELECT  *, Case  when A.IDn=1 then A.masuk ELSE (select SUM(Masuk) from KartuStock2 where " +
                     "IDn<= A.IDn)-(select SUM(Keluar) from KartuStock2 where " +
                     "IDn<= A.IDn) end Saldo from KartuStock2 as A Order by urut,tanggal,tipe ";
            ArrayList arrData = new ArrayList();
            arrData = inventoryFacade.RetrieveMaterialTrans(strsql);
            lstTrans.DataSource = arrData;
            lstTrans.DataBind();
            lstPrev.Visible = true;
            //ScriptManager.RegisterStartupScript(this, Page.GetType(), "alert", "fixHead()", true);
        }
        protected void lstTrans_DataBound(object sender, RepeaterItemEventArgs e)
        {
            KartStock ks = (KartStock)e.Item.DataItem;
            Label label = (Label)e.Item.FindControl("txtKet");
            Label lbl = (Label)e.Item.FindControl("Label1");
            switch (ks.Tipe)
            {
                case 6:
                case 1:
                    //label.Text = "<a href='#' style='font-size:xx-small'> <i>View pending SPB</i></a>";
                    break;
                case 3:
                case 4:
                    string[] bn = ks.BonNo.ToString().Split('-');
                    lbl.Text = bn[0].ToString().ToUpper();
                    label.Text = bn[1].ToString().ToUpper();
                    break;
                case 5:
                    break;
                case 0:
                    break;
                default:
                    DeptFacade d = new DeptFacade();
                    label.Text = " [ " + ((Dept)d.RetrieveByCode(ks.Uraian.TrimEnd())).DeptName + " ]";
                    break;
            }
        }
    }
}