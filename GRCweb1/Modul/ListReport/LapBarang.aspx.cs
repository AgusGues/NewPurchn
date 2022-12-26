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
    public partial class LapBarang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.link = "~/Default.aspx";
            if (!Page.IsPostBack)
            {

                LoadTipeBarang();

                LoadGroup();

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




        private void LoadGroup()
        {
            ddlGroup.Items.Clear();

            ArrayList arrGroupPurchn = new ArrayList();
            GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
            arrGroupPurchn = groupsPurchnFacade.Retrieve();

            ddlGroup.Items.Add(new ListItem("-- Pilih Group --", string.Empty));
            foreach (GroupsPurchn groupsPurchn in arrGroupPurchn)
            {
                ddlGroup.Items.Add(new ListItem(groupsPurchn.GroupDescription, groupsPurchn.ID.ToString()));
            }

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

            //Session["drTgl"] = PdrTgl;
            //Session["sdTgl"] = PsdTgl;
            //Session["deptName"] = ddlNamaBarang.SelectedItem;
            //Session["deptName"] = ddlDeptName.SelectedItem;

            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }

            ReportFacade reportFacade = new ReportFacade();

            //Jika Barang inventory
            string strStock = string.Empty;
            string strAktif = string.Empty;
            int valstock = ddlStock.SelectedIndex;
            if (valstock == 0)
                strStock = "Stock dan Non Stock";
            if (valstock == 1)
                strStock = "Stock";
            if (valstock == 2)
                strStock = "Non Stock";

            Session["rStock"] = strStock;
            int valgroup = ddlGroup.SelectedIndex;
            string strGroup = ddlGroup.SelectedItem.ToString();
            Session["rGroup"] = strGroup.ToUpper();

            int valaktif = ddlAktif.SelectedIndex;
            if (valaktif == 0)
                strAktif = "Aktif dan Non Aktif";
            if (valaktif == 1)
                strAktif = "Aktif";
            if (valaktif == 2)
                strAktif = "Non Aktif";

            Session["rAktif"] = strAktif;
            if (ddlTipeBarang.SelectedIndex == 1)

            {
                string tipebarang = "Inventory";
                Session["TipeBarang"] = tipebarang.ToUpper();
                int dgItemType = 1;
                allQuery = reportFacade.ViewLapBarang(dgItemType, valstock, valgroup, valaktif, tipebarang);

            }
            // Jika Barang  Asset
            if (ddlTipeBarang.SelectedIndex == 2)
            {
                string tipebarang = "Asset";
                Session["TipeBarang"] = tipebarang.ToUpper();
                int dgItemType = 2;
                allQuery = reportFacade.ViewLapBarang(dgItemType, valstock, valgroup, valaktif, tipebarang);


            }
            // Jika barang Biaya
            if (ddlTipeBarang.SelectedIndex == 3)
            {
                string tipebarang = "Biaya";
                Session["TipeBarang"] = tipebarang.ToUpper();
                int dgItemType = 3;
                allQuery = reportFacade.ViewLapBarang(dgItemType, valstock, valgroup, valaktif, tipebarang);

            }

            strQuery = allQuery;
            Session["Query"] = strQuery;

            Cetak(this);
        }

        static public void Cetak(Control page)
        {
            string myScript = "var wn = window.showModalDialog('../Report/Report.aspx?IdReport=LapBarang', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private string ValidateText()
        {
            //if (txtTgl1.Text == string.Empty)
            //    return "Dari Tanggal tidak boleh kosong";
            //else if (txtTgl2.Text == string.Empty)
            //    return "s/d Tanggal tidak boleh kosong";

            ////if (ddlDeptName.SelectedIndex == 0)
            ////    return "Pilih Dept tidak boleh kosong";

            return string.Empty;
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
    }
}