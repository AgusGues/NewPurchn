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
    public partial class RekapReceipt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.link = "~/Default.aspx";
            if (!Page.IsPostBack)
            {
                txtTgl1.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                txtTgl2.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                LoadGroup();
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
            string IDGroup = string.Empty;
            string allQueryNew = string.Empty;
            Users users = (Users)Session["Users"];
            Dept dept = new Dept();
            DeptFacade deptfacade = new DeptFacade();
            int test = users.ID;
            dept = deptfacade.RetrieveById(users.DeptID);

            drTgl = DateTime.Parse(txtTgl1.Text).ToString("yyyyMMdd");
            sdTgl = DateTime.Parse(txtTgl2.Text).ToString("yyyyMMdd");
            PdrTgl = DateTime.Parse(txtTgl1.Text).ToString("dd/MM/yyyy");
            PsdTgl = DateTime.Parse(txtTgl2.Text).ToString("dd/MM/yyyy");
            IDGroup = ddlGroup.SelectedValue;
            Session["drTgl"] = PdrTgl;
            Session["sdTgl"] = PsdTgl;
            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }
            ReportFacade reportFacade = new ReportFacade();
            //Group Purchn or Doc Prefic
            if (RadioButton1.Checked == true || RadioButton2.Checked == true)
            {
                if (users.DeptID > 0 && dept.DeptName.Trim().Length>2 )
                {
                    string deptname = dept.DeptName.Substring(0, 3).ToUpper();
                    //if (deptname == "PUR" || deptname == "KEU" || deptname == "EDP" || deptname == "ACC")
                    //{
                    if (users.DeptID == 14 || users.DeptID == 15 || users.DeptID == 12 || users.DeptID == 24)
                    {
                        allQuery = reportFacade.ViewRekapReceipt2(drTgl, sdTgl, IDGroup, users.ViewPrice);
                        if (users.DeptID == 15)
                            allQuery = reportFacade.ViewRekapReceipt2P(drTgl, sdTgl, IDGroup, users.ViewPrice);
                    }
                    else
                    {
                        allQuery = reportFacade.ViewRekapReceipt3(drTgl, sdTgl, IDGroup);
                    }
                }
                else
                {
                    allQuery = reportFacade.ViewRekapReceipt3(drTgl, sdTgl, IDGroup);
                }
                //allQueryNew = reportFacade.ViewRekapReceiptNew(drTgl, sdTgl, IDGroup);
                allQueryNew = allQuery;
            }
            else if (RadioButton3.Checked == true)
            {
                Inventory inv = new Inventory();
                InventoryFacade invF = new InventoryFacade();
                inv = invF.RetrieveByCode2(txtInput.Text.Trim());
                int groupid = inv.GroupID;
                int itemid = inv.ID;
                int itemtypeid = inv.ItemTypeID;

                if (users.DeptID > 0)
                {

                    string deptname = dept.DeptName.Substring(0, 3).ToUpper();
                    //if (deptname == "PUR" || deptname == "KEU" || deptname == "EDP" || deptname == "ACC")
                    //{
                    if (users.DeptID == 14 || users.DeptID == 15 || users.DeptID == 12 || users.DeptID == 24)
                    {
                        allQuery = reportFacade.ViewRekapReceipt2ByItemCode(drTgl, sdTgl, users.ViewPrice, itemid, itemtypeid, groupid);
                        if (users.DeptID == 15)
                            allQuery = reportFacade.ViewRekapReceipt2PByItemCode(drTgl, sdTgl, users.ViewPrice, itemid, itemtypeid, groupid);
                    }
                    else
                    {
                        allQuery = reportFacade.ViewRekapReceipt3ByItemCode(drTgl, sdTgl, users.ViewPrice, itemid, itemtypeid, groupid);
                    }
                }
                else
                {
                    allQuery = reportFacade.ViewRekapReceipt3ByItemCode(drTgl, sdTgl, users.ViewPrice, itemid, itemtypeid, groupid);
                }
                allQueryNew = allQuery;
            }
            else if (RadioButton4.Checked == true)
            {
                if (users.DeptID > 0)
                {
                    string deptname = dept.DeptName.Substring(0, 3).ToUpper();
                    //if (deptname == "PUR" || deptname == "KEU" || deptname == "EDP" || deptname == "ACC")
                    //{
                    if (users.DeptID == 14 || users.DeptID == 15 || users.DeptID == 12 || users.DeptID == 24)
                    {
                        allQuery = reportFacade.ViewRekapReceipt2BySupp(drTgl, sdTgl, txtInput.Text, users.ViewPrice);
                        if (users.DeptID == 15)
                            allQuery = reportFacade.ViewRekapReceipt2PBySupp(drTgl, sdTgl, txtInput.Text, users.ViewPrice);
                    }
                    else
                    {
                        allQuery = reportFacade.ViewRekapReceipt3BySupp(drTgl, sdTgl, txtInput.Text);
                    }
                }
                else
                {
                    allQuery = reportFacade.ViewRekapReceipt3BySupp(drTgl, sdTgl, txtInput.Text);
                }
                allQueryNew = allQuery;
            }


            if (ddlPrint.SelectedIndex == 0)
            {
                strQuery = allQuery;
                Session["Query"] = strQuery;
                Cetak(this);
            }
            else
            {
                strQuery = allQueryNew;
                Session["Query"] = strQuery;
                CetakExcel(this);
            }

        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlPrint_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        static public void Cetak(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../Report/Report.aspx?IdReport=RekapReceipt', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
            string myScript = "Cetak();";
            ScriptManager.RegisterStartupScript(page, page.GetType(),"MyScript", myScript, true);
        }

        static public void CetakExcel(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../Report/Report.aspx?IdReport=RekapReceiptExcel', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
            string myScript = "CetakExcel();";
            ScriptManager.RegisterStartupScript(page, page.GetType(),"MyScript", myScript, true);
        }

        private string ValidateText()
        {
            if (txtTgl1.Text == string.Empty)
                return "Dari Tanggal tidak boleh kosong";
            else if (txtTgl2.Text == string.Empty)
                return "s/d Tanggal tidak boleh kosong";
            return string.Empty;
        }

        private void LoadGroup()
        {
            ArrayList arrGroupsPurchn = new ArrayList();
            GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
            arrGroupsPurchn = groupsPurchnFacade.Retrieve();
            ddlGroup.Items.Clear();
            ddlGroup.Items.Add(new ListItem("-- Pilih Group Receipt --", string.Empty));
            ddlGroup.Items.Add(new ListItem("ALL", "0"));
            foreach (GroupsPurchn groupsPurchn in arrGroupsPurchn)
            {
                ddlGroup.Items.Add(new ListItem(groupsPurchn.GroupDescription, groupsPurchn.ID.ToString()));
            }
        }


        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            LoadGroup();
            ddlGroup.Visible = true;
            txtInput.Visible = false;
        }
        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            LoadDocNoPref();
            ddlGroup.Visible = true;
            txtInput.Visible = false;
        }

        private void LoadDocNoPref()
        {
            ddlGroup.Items.Clear();

            ArrayList arrGroupsPurchn = new ArrayList();
            GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
            arrGroupsPurchn = groupsPurchnFacade.RetrieveCode();
            Company company = new Company();
            CompanyFacade companyFacade = new CompanyFacade();
            string kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID);
            ddlGroup.Items.Add(new ListItem("-- Pilih Doc No Prefix --", string.Empty));
            ddlGroup.Items.Add(new ListItem("ALL", "0"));
            foreach (GroupsPurchn groupsPurchn in arrGroupsPurchn)
            {
                ddlGroup.Items.Add(new ListItem(kd + groupsPurchn.GroupCode, kd + groupsPurchn.GroupCode));
            }
        }
        protected void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            ddlGroup.Visible = false;
            txtInput.Visible = true;
        }
        protected void RadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            ddlGroup.Visible = false;
            txtInput.Visible = true;
        }

    }
}