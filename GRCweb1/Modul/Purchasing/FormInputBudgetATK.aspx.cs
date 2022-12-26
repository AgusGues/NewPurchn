using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;

namespace GRCweb1.Modul.Purchasing
{
    public partial class FormInputBudgetATK : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.link = "~/Default.aspx";
            if (!Page.IsPostBack)
            {
                GetTahun();
                LoadDept();
                Session["ListBudget"] = null;
                ddlMasaBerlaku.Enabled = false;
                LoadMasaBerlaku();
            }
        }

        private void ClearForm()
        {
            Session["ListBudget"] = null;

            txtBudgetQty.Text = string.Empty;

            if (ddlDeptName.SelectedIndex > 0)
                ddlDeptName.SelectedIndex = 0;
            ddlItemName.Items.Clear();

            if (ddlTipeBudget.SelectedIndex > 0)
                ddlTipeBudget.SelectedIndex = 0;

            txtCariNamaBrg.Text = string.Empty;
            //kosongkan txtID menjadi 0 lagi
            txtID.Value = "0";
            //hapus grid gunakan arraylist kosong
            ArrayList arrData = new ArrayList();
            lstBudget.DataSource = arrData;
            lstBudget.DataBind();
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }



        private void GetTahun()
        {
            BudgetingFacade bg = new BudgetingFacade();
            bg.Pilihan = "Tahun";
            bg.Prefix = " Top 3 ";
            ArrayList arrData = bg.Retrieve();
            ddlTahun.Items.Clear();
            ddlTahun.Items.Add(new ListItem((DateTime.Now.Year + 1).ToString(), (DateTime.Now.Year + 1).ToString()));
            if (!arrData.Contains(DateTime.Now.Year)) ddlTahun.Items.Add(new ListItem((DateTime.Now.Year).ToString(), (DateTime.Now.Year).ToString()));
            foreach (Budget b in arrData)
            {
                ddlTahun.Items.Add(new ListItem(b.Tahun.ToString(), b.Tahun.ToString()));
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }

        private void LoadDept()
        {
            ddlDeptName.Items.Clear();
            ArrayList arrDept = new ArrayList();
            DeptFacade deptFacade = new DeptFacade();
            arrDept = deptFacade.Retrieve();
            ddlDeptName.Items.Add(new ListItem("-- Pilih Dept --", "0"));
            foreach (Dept dept in arrDept)
            {
                ddlDeptName.Items.Add(new ListItem(dept.DeptName, dept.ID.ToString()));

            }
        }
        protected void ddlDept_Change(object sender, EventArgs e)
        {
            /**
             * check apakah dept terpilih sudah punya budget untuk material terpilih
             * jika blm tampilkan pilihan baru adn aktifkan button additem
             */
            BudgetAtkFacade bgd = new BudgetAtkFacade();
            ArrayList arrData = new ArrayList();
            if (ddlDeptName.SelectedIndex == 0) return;
            if (ddlItemName.SelectedIndex > 0)
            {
                arrData = bgd.Retrieve(int.Parse(ddlItemName.SelectedValue), int.Parse(ddlTahun.SelectedValue), int.Parse(ddlDeptName.SelectedValue));
            }
            txtBaru.Enabled = (arrData.Count > 0) ? false : true;
            txtBaru.Checked = (arrData.Count > 0) ? false : true;
            //txtAdd.Enabled = (arrData.Count > 0) ? true : false;
            //txtAdd.Checked = (arrData.Count > 0) ? true : false;
            lstBudget.DataSource = arrData;
            lstBudget.DataBind();
        }
        protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            /**
             * disini check apakah item tersebut sudah ada id table budgetsp
             * jika ada tampilkan ke repeater
             */
            BudgetAtkFacade bgd = new BudgetAtkFacade();
            ArrayList arrData = new ArrayList();
            if (ddlItemName.SelectedIndex > 0)
            {
                arrData = bgd.Retrieve(int.Parse(ddlItemName.SelectedValue), int.Parse(ddlTahun.SelectedValue));
            }
            txtBaru.Enabled = (arrData.Count > 0) ? false : true;
            txtBaru.Checked = (arrData.Count > 0) ? false : true;
            //txtAdd.Enabled = (arrData.Count > 0) ? true : false;
            //txtAdd.Checked = (arrData.Count > 0) ? true : false;
            lstBudget.DataSource = arrData;
            lstBudget.DataBind();


        }
        protected void txtCariNamaBrg_Change(object sender, EventArgs e)
        {
            InventoryFacade invFacade = new InventoryFacade();
            ArrayList inv = invFacade.RetrieveByCriteria("ItemName", txtCariNamaBrg.Text);
            ddlItemName.Items.Clear();
            ddlItemName.Items.Add(new ListItem("--Pilih--", "0"));
            foreach (Inventory vs in inv)
            {
                ddlItemName.Items.Add(new ListItem(vs.ItemName.ToString() + " [" + vs.ItemCode + "]", vs.ID.ToString()));
            }

        }
        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            ClearForm();
        }

        protected void btnList_ServerClick(object sender, EventArgs e)
        {

        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {

        }

        protected void lbAddOP_Click(object sender, EventArgs e)
        {
            ArrayList arrItem = (Session["ListBudget"] == null) ? new ArrayList() : (ArrayList)Session["ListBudget"];
            BudgetAtk buc = new BudgetAtk();
            if (txtCariNamaBrg.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Nama Material Tidak Ada");
                return;
            }
            if (txtBudgetQty.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Budget Quantity Harus Diisi");
                return;
            }
            if (ddlDeptName.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Pilih Departemen");
                return;
            }
            if (ddlTipeBudget.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Pilih Tipe Budget");
                return;
            }

            string[] item = txtCariNamaBrg.Text.Split('-');
            buc.Bulan = 1;// int.Parse(ddlbulan.SelectedValue);
            buc.Tahun = int.Parse(ddlTahun.SelectedValue);
            buc.DeptID = int.Parse(ddlDeptName.SelectedValue);
            buc.DeptName = ddlDeptName.SelectedItem.Text;
            buc.ItemID = int.Parse(ddlItemName.SelectedValue);
            buc.ItemCode = ddlItemName.SelectedItem.Text;
            buc.Quantity = decimal.Parse(txtBudgetQty.Text);
            buc.ItemTypeID = 1;// default barang inventory sementara int.Parse(ddlTipeBudget.SelectedValue);
            buc.Jenis = ddlTipeBudget.SelectedItem.Text;
            buc.RuleCalc = int.Parse(ddlTipeBudget.SelectedValue.ToString());
            buc.ID = int.Parse(txtID.Value);
            arrItem.Add(buc);
            Session["ListBudget"] = arrItem;
            lstBudget.DataSource = arrItem;
            lstBudget.DataBind();
            btnUpdate.Enabled = true;

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void chk_change(object sender, EventArgs e)
        {

        }

        protected void btnUpdate_serverClick(object sender, EventArgs e)
        {
            int intResult = 0;
            string strEvent = "Insert";
            BudgetAtk budgetAtk = new BudgetAtk();
            BudgetAtkFacade budgetAtkFacade = new BudgetAtkFacade();
            if (ViewState["id"] != null)
            {
                budgetAtk.ID = int.Parse(ViewState["id"].ToString());
                strEvent = "Edit";
            }
            if (Session["ListBudget"] == null) { return; }
            ArrayList arrData = (ArrayList)Session["ListBudget"];
            foreach (BudgetAtk buc in arrData)
            {
                intResult = 0;
                budgetAtk.ItemID = buc.ItemID;// int.Parse(ddlItemName.SelectedValue.ToString());
                budgetAtk.Tahun = buc.Tahun;// int.Parse(ddlTahun.SelectedValue.ToString());
                budgetAtk.Quantity = buc.Quantity;// (txtBudgetQty.Text != string.Empty) ? Convert.ToDecimal(txtBudgetQty.Text) : 0;
                budgetAtk.DeptID = buc.DeptID;// int.Parse(ddlDeptName.SelectedValue.ToString());
                budgetAtk.CreatedBy = ((Users)Session["Users"]).UserName;
                budgetAtk.Bulan = 1;// int.Parse(ddlbulan.SelectedValue.ToString());
                budgetAtk.ItemTypeID = buc.ItemTypeID;// int.Parse(ddlTipeBudget.SelectedValue.ToString());
                budgetAtk.RuleCalc = buc.RuleCalc;
                if (budgetAtk.ID > 0)
                {
                    intResult = budgetAtkFacade.Update(budgetAtk);
                    InsertLog(budgetAtk);
                }
                else
                {
                    //disini update dulu field head di table inventory
                    //jika result >0 lanjutkan jika tidak return false;
                    intResult = budgetAtkFacade.UpdateHeadForBudget(buc.ItemID, 1);
                    if (intResult > 0)
                    {
                        intResult = budgetAtkFacade.Insert(budgetAtk);

                        if (budgetAtkFacade.Error == string.Empty)
                        {
                            if (intResult > 0)
                            {
                                DisplayAJAXMessage(this, "Budget Telah Disimpan");
                                ClearForm();
                            }
                        }
                        InsertLog(budgetAtk);
                    }
                    else
                    {
                        DisplayAJAXMessage(this, "Gagal Simpan Budget");
                    }
                }
            }

        }

        //private string ValidasiText()
        //{

        //}

        protected void lstBudget_Databound(object sender, RepeaterItemEventArgs e)
        {
            Image edt = (Image)e.Item.FindControl("edt");
            Image del = (Image)e.Item.FindControl("del");
            Image dls = (Image)e.Item.FindControl("dels");
            BudgetAtk buc = (BudgetAtk)e.Item.DataItem;
            edt.Visible = (buc.ID > 0) ? true : false;
            del.Visible = (buc.ID == 0) ? true : false;
            dls.Visible = (buc.ID > 0) ? true : false;
            //dls.Attributes.Add("onclick", "return HapusData()");
            Label lbl = (Label)e.Item.FindControl("txtBgType");
            switch (buc.RuleCalc)
            {
                case 1:
                    lbl.Text = "Bulanan";
                    break;
                case 12:
                    lbl.Text = "Tahunan";
                    break;
                case 6:
                    lbl.Text = "Semesteran";
                    break;
                default:
                    lbl.Text = "";
                    break;
            }
        }
        private void LoadItemBarang(int ItemID)
        {
            InventoryFacade iv = new InventoryFacade();
            iv.Criteriane = " AND A.ID=" + ItemID;
            ddlItemName.Items.Clear();
            ddlItemName.Items.Add(new ListItem("--Pilih --", "0"));
            ArrayList arrData = iv.Retrieve();
            foreach (Inventory inv in arrData)
            {
                ddlItemName.Items.Add(new ListItem(inv.ItemName, inv.ID.ToString()));
            }

        }
        protected void lstBudget_Command(object sender, RepeaterCommandEventArgs e)
        {
            string BudgetID = e.CommandArgument.ToString();
            BudgetAtk bdc = new BudgetAtk();
            BudgetAtkFacade bdf = new BudgetAtkFacade();
            switch (e.CommandName)
            {
                case "edit":
                    bdc = new BudgetAtk();
                    bdf = new BudgetAtkFacade();
                    bdc = bdf.Retrieve(int.Parse(BudgetID));
                    txtCariNamaBrg.Text = bdc.ItemName.ToString();
                    txtCariNamaBrg_Change(null, null);
                    txtBaru.Enabled = false;
                    txtRevisi.Enabled = true;
                    //txtAdd.Enabled = true;
                    txtBaru.Checked = false;
                    txtID.Value = bdc.ID.ToString();
                    ddlTahun.SelectedValue = bdc.Tahun.ToString();
                    LoadItemBarang(bdc.ItemID);
                    ddlItemName.SelectedValue = bdc.ItemID.ToString();
                    txtBudgetQty.Text = bdc.Quantity.ToString("###,##0.#0");
                    //txtBudgetQty.ReadOnly = true;
                    ddlTipeBudget.SelectedValue = bdc.RuleCalc.ToString();
                    lbAddOP.Enabled = false;
                    btnUpdate.Enabled = false;
                    ddlDeptName.SelectedValue = bdc.DeptID.ToString();
                    ddlMasaBerlaku.Enabled = true;
                    break;
                case "dele":
                    break;
                case "delet":
                    bdc = new BudgetAtk();
                    bdf = new BudgetAtkFacade();
                    bdc.ID = int.Parse(BudgetID);
                    bdc.CreatedBy = ((Users)Session["Users"]).UserName;
                    int result = bdf.Delete(bdc);
                    if (result > -1)
                    {
                        ddlItemName_SelectedIndexChanged(null, null);
                    }
                    break;
            }
        }
        protected void tpp_Change(object sender, EventArgs e)
        {
            if (txtBaru.Checked == true)
            {
                lbAddOP.Enabled = true;
                lbUpdate.Visible = false;
            }
            if (txtRevisi.Checked == true)
            {
                lbAddOP.Enabled = false;
                txtBudgetQty.ReadOnly = false;
                btnUpdate.Enabled = false;
                lbUpdate.Visible = true;
            }
            ddlMasaBerlaku.Enabled = (txtBaru.Checked == true) ? true : false;
        }
        private void InsertLog(BudgetAtk bc)
        {
            //insert ke log budget
            BudgetAtk log = new BudgetAtk();
            BudgetAtkFacade bdf = new BudgetAtkFacade();
            log.Tahun = bc.Tahun;// int.Parse(ddlTahun.SelectedValue);
            log.DeptID = bc.DeptID;// int.Parse(ddlDeptName.SelectedValue);
            log.ItemID = bc.ItemID;// int.Parse(ddlItemName.SelectedValue);
            log.BudgetAwal = bc.Quantity;// (txtBaru.Checked == true) ? decimal.Parse(txtBudgetQty.Text.ToString()) : 0;
            log.Revisi = 0;// (txtRevisi.Checked == true) ? decimal.Parse(txtBudgetQty.Text.ToString()) : 0;
            log.Tambahan = 0;// (txtAdd.Checked == true) ? decimal.Parse(txtBudgetQty.Text.ToString()) : 0;
            log.MasaBerlaku = 0;// int.Parse(ddlMasaBerlaku.SelectedValue);
            int result = bdf.Insert(log, true);
        }
        protected void lbUpdate_Click(object sender, EventArgs e)
        {
            BudgetAtk bd = new BudgetAtk();
            BudgetAtkFacade bdf = new BudgetAtkFacade();
            bd.ID = int.Parse(txtID.Value);
            bd.Bulan = 1;
            bd.Tahun = int.Parse(ddlTahun.SelectedValue);
            bd.DeptID = int.Parse(ddlDeptName.SelectedValue);
            bd.ItemID = int.Parse(ddlItemName.SelectedValue);
            bd.Quantity = decimal.Parse(txtBudgetQty.Text.ToString());
            if (txtRevisi.Checked == true)
            {
                bd.ItemTypeID = 1;
                bd.RuleCalc = int.Parse(ddlTipeBudget.SelectedValue);
                bd.MasaBerlaku = int.Parse(ddlMasaBerlaku.SelectedValue);
            }
            else
            {
                bd.RuleCalc = int.Parse(ddlTipeBudget.SelectedValue);
                bd.MasaBerlaku = int.Parse(ddlMasaBerlaku.SelectedValue);
            }
            int result = (txtRevisi.Checked == true) ? bdf.Update(bd) : bdf.UpdateQty(bd, false);
            if (result > 0)
            {
                //insert ke log budget
                BudgetAtk log = new BudgetAtk();
                log.Tahun = int.Parse(ddlTahun.SelectedValue);
                log.DeptID = int.Parse(ddlDeptName.SelectedValue);
                log.ItemID = int.Parse(ddlItemName.SelectedValue);
                log.BudgetAwal = (txtBaru.Checked == true) ? decimal.Parse(txtBudgetQty.Text.ToString()) : 0;
                log.Revisi = (txtRevisi.Checked == true) ? decimal.Parse(txtBudgetQty.Text.ToString()) : 0;
                //log.Tambahan = (txtAdd.Checked == true) ? decimal.Parse(txtBudgetQty.Text.ToString()) : 0;
                log.MasaBerlaku = int.Parse(ddlMasaBerlaku.SelectedValue);
                result = bdf.Insert(log, true);
                ddlItemName_SelectedIndexChanged(null, null);
            }
        }
        private void LoadMasaBerlaku()
        {
            ddlMasaBerlaku.Items.Clear();
            ddlMasaBerlaku.Items.Add(new ListItem("Setahun", "12"));
            for (int i = DateTime.Now.Month; i <= 12; i++)
            {
                ddlMasaBerlaku.Items.Add(new ListItem(Global.nBulan(i), i.ToString()));
            }
            ddlMasaBerlaku.SelectedIndex = 1;
        }
    }
}