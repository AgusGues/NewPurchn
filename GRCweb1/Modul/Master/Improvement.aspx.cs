using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.IO;

using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace GRCweb1.Modul.Master
{
    public partial class Improvement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Session["incode"] = "0000";
                LoadGroupI("1");
                LoadSatuan();
                LoadDataGridImprovement(LoadGridImprovement());
                LoadItemName();
                LoadEmailTo();
                btnApproveAll.Visible = (((Users)Session["Users"]).Apv >= 1) ? false : false;
                leadTime.Text = string.Empty;
                //clearForm();
            }
        }

        private void LoadDataGridImprovement(ArrayList arrImprovement)
        {
            this.GridView1.DataSource = arrImprovement;
            this.GridView1.DataBind();
        }
        private void CollectCode()
        {
            string stock = string.Empty;
            string noUrut = "0";
            string ItemNameID = string.Empty;
            string ItemMerkID = string.Empty;
            string ItemTypeID = string.Empty;

            if (RBNonStock.Checked == true)
                stock = "0";
            else
                stock = "1";
            ImprovementFacade improvementFacade = new ImprovementFacade();
            ItemNameID = improvementFacade.GetCodeItemName(txtNama.Text);

            noUrut = HitungNoAkhir(txtNama.Text, ddlGroup.SelectedValue).PadLeft(3, '0');
            txtItemCode.Text = ddlGroup.SelectedValue.Trim() + Session["incode"].ToString().Trim() + noUrut + Session["kodetype"].ToString().Trim() + Session["kodeukuran"] + Session["kodemerk"].ToString().Trim();
            // +stock + ddlItemType.SelectedValue.Trim();
        }

        private string HitungNoAkhir(string strPrefix, string ItemTypeID)
        {
            int noUrut = 0; int noUrutCtrp = 0; int noUrutKrw = 0;
            if (strPrefix.Trim() != string.Empty)
            {
                string NoAkhir = string.Empty;
                ImprovementFacade improvementFacade = new ImprovementFacade();
                //noUrut = improvementFacade.CountItemName(strPrefix,ddlItemType.SelectedValue);

                /**
                 * cek jumlah data yang sudah terbentuk di masing2 server
                 */
                try
                {
                    WebReference_Krwg.Service1 webServiceKRW = new WebReference_Krwg.Service1();
                    noUrutKrw = webServiceKRW.GetImprovementCountName(strPrefix, ItemTypeID);
                }
                catch
                {
                    noUrutKrw = 0;
                }

                try
                {
                    WebReference_Ctrp.Service1 webServiceCtrp = new WebReference_Ctrp.Service1();
                    noUrutCtrp = webServiceCtrp.GetImprovementCountName(strPrefix, ItemTypeID);
                }
                catch
                {
                    noUrutCtrp = 0;
                }


                noUrut = (noUrutKrw > noUrutCtrp) ? noUrutKrw : noUrutCtrp;
                noUrut = noUrut + 1;

            }
            return noUrut.ToString();
        }

        public string cek_kodebarang_existing(string Nama, string Type, string Ukuran, string Merk, string grpID)
        {
            int noUrutKrw = 0; int noUrutCtrp = 0;
            string nm_barang = string.Empty;
            try
            {
                WebReference_Krwg.Service1 webServiceKRW = new WebReference_Krwg.Service1();
                noUrutKrw = webServiceKRW.GetExistingKodeBarang(Nama, Type, Ukuran, Merk, grpID);

            }
            catch
            {
                noUrutKrw = 0;
            }

            try
            {
                WebReference_Ctrp.Service1 webServiceCtrp = new WebReference_Ctrp.Service1();
                noUrutCtrp = webServiceCtrp.GetExistingKodeBarang(Nama, Type, Ukuran, Merk, grpID);
            }
            catch
            {
                noUrutCtrp = 0;
            }
            nm_barang = (noUrutKrw > 0 || noUrutCtrp > 0) ? "Kode Barang Sudah ada / pernah dibuat" : string.Empty;
            return nm_barang;
        }
        private void LoadSatuan()
        {
            ArrayList arrUOM = new ArrayList();
            UOMFacade uOMFacade = new UOMFacade();
            arrUOM = uOMFacade.Retrieve1();
            ddlSatuan.Items.Add(new ListItem("-- Pilih Satuan --", "0"));
            foreach (UOM uOM in arrUOM)
            {
                ddlSatuan.Items.Add(new ListItem(uOM.UOMDesc, uOM.ID.ToString()));
            }
        }

        private void LoadGroupI(string itemtype)
        {
            ArrayList arrGroupsPurchn = new ArrayList();
            GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
            if (itemtype == "1")
            {
                Ljudul.Text = "IMPROVEMENT INVENTORY";
                //LContoh.Text = "SP-BR-9 (Barang Stock)  atau  SP-BR-0 (Barang Nonstock)";
            }
            else
            {
                Ljudul.Text = "IMPROVEMENT ASSET";
                //LContoh.Text = "CA-GA-0  atau  CA-BM-0";
            }
            arrGroupsPurchn = groupsPurchnFacade.RetrieveByItemTypeID(itemtype);
            ddlGroup.Items.Clear();
            ddlGroup.Items.Add(new ListItem("-- Pilih Group --", "00"));
            foreach (GroupsPurchn groupsPurchn in arrGroupsPurchn)
            {
                ddlGroup.Items.Add(new ListItem(groupsPurchn.GroupDescription, groupsPurchn.CodeID.Trim()));
            }
        }

        private void LoadItemType()
        {
            ArrayList arrItemTypePurchn = new ArrayList();
            ItemTypePurchnFacade itemTypePurchnFacade = new ItemTypePurchnFacade();
            arrItemTypePurchn = itemTypePurchnFacade.Retrieve();
            ddlItemType.Items.Add(new ListItem("-- Pilih Item Type --", "00"));
            foreach (ItemTypePurchn itemTypePurchn in arrItemTypePurchn)
            {
                ddlItemType.Items.Add(new ListItem(itemTypePurchn.TypeDescription, itemTypePurchn.ID.ToString()));
            }
        }

        private ArrayList LoadGridImprovement()
        {
            ArrayList arrImprovement = new ArrayList();

            if (ddlItemType.SelectedValue == "1")
            {
                if (RBImprovement.Checked == true)
                {
                    ImprovementFacade improvementFacade = new ImprovementFacade();
                    arrImprovement = improvementFacade.Retrieve2(1);
                }
                else if (RBUnApv.Checked == true)
                {
                    ImprovementFacade improvementFacade = new ImprovementFacade();
                    arrImprovement = improvementFacade.RetrieveUnApv(((Users)Session["Users"]).UnitKerjaID);
                }
                else
                {
                    ImprovementFacade improvementFacade = new ImprovementFacade();
                    arrImprovement = improvementFacade.Retrieve2ByApproval(1, ((Users)Session["Users"]).Apv, ((Users)Session["Users"]).ID);
                }
            }
            else
            {
                if (RBImprovement.Checked == true)
                {
                    ImprovementFacade improvementFacade = new ImprovementFacade();
                    arrImprovement = improvementFacade.Retrieve2(2);
                }
                else
                {
                    ImprovementFacade improvementFacade = new ImprovementFacade();
                    arrImprovement = improvementFacade.Retrieve2ByApproval(2, ((Users)Session["Users"]).Apv, ((Users)Session["Users"]).ID);
                }
            }

            if (arrImprovement.Count > 0)
            {
                return arrImprovement;
            }

            arrImprovement.Add(new Improvement());
            return arrImprovement;
        }

        private ArrayList LoadGridByCriteria()
        {
            ArrayList arrImprovement = new ArrayList();
            ImprovementFacade improvementFacade = new ImprovementFacade();
            if (txtSearch.Text == string.Empty)
                arrImprovement = improvementFacade.Retrieve();
            else
                arrImprovement = improvementFacade.RetrieveByCriteria(ddlSearch.SelectedValue, txtSearch.Text);

            if (arrImprovement.Count > 0)
            {
                return arrImprovement;
            }
            arrImprovement.Add(new Improvement());
            return arrImprovement;
        }

        private void clearForm()
        {
            Session["id"] = null;
            txtItemCode.Text = string.Empty;
            txtItemName.Text = string.Empty;
            ddlGroup.SelectedIndex = 0;
            //ddlDepartemen.SelectedIndex = 0;
            ddlSatuan.SelectedIndex = 0;
            //ddlDeptSection.SelectedIndex = 0;
            //txtMinStock.Text = "0";
            txtKeterangan.Text = "-";
            txtItemCode.Focus();
            txtNama.Text = string.Empty;
            txtType.Text = string.Empty;
            txtUkuran.Text = string.Empty;
            txtJenis.Text = string.Empty;
            txtMerk.Text = string.Empty;
            txtPartNum.Text = string.Empty;
            PanelNama.Visible = true;
            UnlockInput();
        }

        private void lockInput()
        {
            ddlGroup.Enabled = false;
            ddlItemType.Enabled = false;
            //ddlDepartemen.Enabled = false;
            ddlSatuan.Enabled = false;
            //ddlDeptSection.Enabled = false; 
            //txtMinStock.Text = "0";
            txtKeterangan.ReadOnly = true;
            txtNama.ReadOnly = true;
            txtType.ReadOnly = true;
            txtUkuran.ReadOnly = true;
            txtJenis.ReadOnly = true;
            txtMerk.ReadOnly = true;
            txtPartNum.ReadOnly = true;
            RBNonStock.Enabled = false;
            RBStock.Enabled = false;
            btnSearch0.Enabled = false;
            btnSearch1.Enabled = false;
            btnSearch2.Enabled = false;
            if (((Users)Session["Users"]).Apv > 0)
                btnUpdate.Disabled = true;
        }

        private void UnlockInput()
        {
            ddlGroup.Enabled = true;
            ddlItemType.Enabled = true;
            //ddlDepartemen.Enabled = true;
            ddlSatuan.Enabled = true;
            //ddlDeptSection.Enabled = true;
            //txtMinStock.Text = "0";
            txtKeterangan.ReadOnly = false;
            txtNama.ReadOnly = false;
            txtType.ReadOnly = false;
            txtUkuran.ReadOnly = false;
            txtJenis.ReadOnly = false;
            txtMerk.ReadOnly = false;
            txtPartNum.ReadOnly = false;
            RBNonStock.Enabled = true;
            RBStock.Enabled = true;
            btnSearch0.Enabled = true;
            btnSearch1.Enabled = true;
            btnSearch2.Enabled = true;
            if (((Users)Session["Users"]).Apv == 0)
                btnUpdate.Disabled = false;
        }
        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearForm();
        }
        protected void LoadEmailTo()
        {
            string emailto = string.Empty;
            Users usr = new Users();
            UsersHeadFacade userHF = new UsersHeadFacade();
            UsersHead userH = new UsersHead();
            usr = (Users)Session["Users"];
            int headID = 0;
            if (usr != null)
            {
                if (usr.Apv == 0)
                {
                    userH = userHF.RetrieveByUserIDimprov(usr.ID.ToString());
                    headID = userH.HeadID;
                }
                if (usr.Apv == 1)
                {
                    userH = userHF.RetrieveByImprovementHead(usr.ID.ToString());
                    headID = userH.HeadID;
                }
                if (usr.Apv == 2)
                {
                    userH = userHF.RetrieveByImprovementManager(usr.ID.ToString());
                    headID = userH.HeadID;
                }
                if (usr.Apv < 1) btnApprove.Disabled = true;
            }

            Users userC = new Users();
            UsersFacade usercF = new UsersFacade();
            userC = usercF.RetrieveById(headID);
            txtEmailApprover.Text = userC.UsrMail;
        }
        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            string strValidate = ValidateText();

            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }

            string strEvent = "Insert";

            if (txtItemName.Text == string.Empty)
            {
                //lblErrorName.Visible = true;
                return;
            }
            CollectName();

            Domain.Improvement improvement = new Domain.Improvement();
            ImprovementFacade improvementFacade = new ImprovementFacade();
            Dept dept = new Dept();
            DeptFacade deptfacade = new DeptFacade();
            int existingkode = 0;
            int intResult = 0;
            //improvementFacade
            improvement.ItemCode = txtItemCode.Text;
            improvement.ItemName = txtItemName.Text;
            //dept = deptfacade.RetrieveByCode(ddlDepartemen.SelectedValue);
            improvement.DeptID = dept.ID;
            improvement.UOMID = int.Parse(ddlSatuan.SelectedValue);
            improvement.GroupID = int.Parse(ddlGroup.SelectedValue);
            improvement.ItemTypeID = int.Parse(ddlItemType.SelectedValue);
            improvement.Keterangan = txtKeterangan.Text;
            improvement.CreatedBy = ((Users)Session["Users"]).UserName;
            improvement.UnitKerjaID = ((Users)Session["Users"]).UnitKerjaID;
            improvement.Nama = txtNama.Text;
            improvement.Type = txtType.Text;
            improvement.Merk = txtMerk.Text;
            improvement.Ukuran = txtUkuran.Text;
            improvement.Jenis = txtJenis.Text;
            improvement.Partnum = txtPartNum.Text;
            improvement.StockNonStock = (RBNonStock.Checked == true) ? 0 : 1;

            if (leadTime.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Lead Time blm di isi");
                return;
            }
            improvement.LeadTime = int.Parse(leadTime.Text);
            if (ViewState["id"] != null)
            {
                improvement.ID = int.Parse(ViewState["id"].ToString());
                strEvent = "Edit";
            }
            if (improvement.ID > 0)
            {
                intResult = improvementFacade.Update(improvement);
            }
            else
            {
                InventoryFacade inv = new InventoryFacade();
                existingkode = inv.CountNewItemCode(txtItemCode.Text);
                //cek exisiting kode di table improvement
                existingkode += improvementFacade.getExistingCode(txtNama.Text, txtType.Text, txtUkuran.Text, txtMerk.Text, ddlGroup.SelectedValue.ToString());
                if (((Users)Session["Users"]).UnitKerjaID == 7)
                {
                    try
                    {
                        WebReference_Ctrp.Service1 webServiceKRW = new WebReference_Ctrp.Service1();
                        existingkode += webServiceKRW.GetExistingKodeBarang(txtNama.Text, txtType.Text, txtUkuran.Text, txtMerk.Text, ddlGroup.SelectedValue.ToString());
                    }
                    catch
                    {
                        DisplayAJAXMessage(this, "Connection to Citeureup Server error please try Again");
                        return;
                    }
                }
                else
                {
                    try
                    {
                        WebReference_Krwg.Service1 webServiceKRW = new WebReference_Krwg.Service1();
                        existingkode += webServiceKRW.GetExistingKodeBarang(txtNama.Text, txtType.Text, txtUkuran.Text, txtMerk.Text, ddlGroup.SelectedValue.ToString());
                    }
                    catch
                    {
                        DisplayAJAXMessage(this, "Connection to Karawang Server error please try Again");
                        return;
                    }

                }
                if (existingkode > 0)
                {
                    DisplayAJAXMessage(this, "Nama Barang " + txtItemName.Text + " Sudah pernah dibuat.Mohon di check kembali.");
                }
                else
                {
                    intResult = improvementFacade.InsertNew(improvement);

                    if (improvementFacade.Error == string.Empty && intResult > 0)
                    {
                        InsertLog(strEvent);
                        if (((Users)Session["Users"]).UnitKerjaID == 7)
                        {
                            KirimEmail(txtEmailApprover.Text.ToString().TrimEnd());

                        }
                        else
                        {
                            clearForm();
                        }
                        LoadDataGridImprovement(LoadGridImprovement());
                    }
                    else
                    {
                        DisplayAJAXMessage(this, improvementFacade.Error);
                    }
                }
            }

        }

        protected void btnDelete_ServerClick(object sender, EventArgs e)
        {
            Domain.Improvement improvement = new Domain.Improvement();
            ImprovementFacade improvementFacade = new ImprovementFacade();
            string strEvent = string.Empty;
            string strError = string.Empty;
            if (ViewState["id"] != null)
            {
                improvement.ID = int.Parse(ViewState["id"].ToString());
                strEvent = "Edit";
            }
            improvement.LastModifiedBy = ((Users)Session["Users"]).UserName;
            int result = improvementFacade.Delete(improvement);
            if (improvementFacade.Error == string.Empty && result > 0)
            {
                LoadDataGridImprovement(LoadGridImprovement());
                InsertLog("Delete");
                clearForm();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                lockInput();
                int index = Convert.ToInt32(e.CommandArgument);
                string kode = string.Empty;
                GridViewRow row = GridView1.Rows[index];
                Session["id"] = int.Parse(row.Cells[0].Text);
                ImprovementFacade improvementFacade = new ImprovementFacade();
                Domain.Improvement improvement = improvementFacade.RetrieveById(int.Parse(row.Cells[0].Text));

                if (improvement.Approval == 9)
                {
                    DisplayAJAXMessage(this, " Status Un Approved");
                }
                else if (improvement.Approval <= ((Users)Session["Users"]).Apv)
                {
                    if (improvement.ApprovalStatus == "Approved") { btnApprove.Disabled = true; } else { btnApprove.Disabled = false; }
                    if (improvementFacade.Error == string.Empty && improvement.ID > 0)
                    {
                        ViewState["id"] = int.Parse(row.Cells[0].Text);
                        txtItemCode.Text = improvement.ItemCode;
                        txtItemName.Text = improvement.ItemName;
                        ddlSatuan.SelectedValue = improvement.UOMID.ToString();
                        txtKeterangan.Text = improvement.Keterangan;
                        kode = improvement.ItemCode.Substring(0, 2);
                        LoadGroupI(improvement.ItemTypeID.ToString());
                        ddlGroup.SelectedValue = improvement.ItemCode.Substring(0, 2).Trim();
                        ddlItemType.SelectedValue = improvement.ItemTypeID.ToString();
                        txtNama.Text = improvement.Nama;
                        txtType.Text = improvement.Type;
                        txtMerk.Text = improvement.Merk;
                        txtUkuran.Text = improvement.Ukuran;
                        txtJenis.Text = improvement.Jenis;
                        txtPartNum.Text = improvement.Partnum;
                        leadTime.Text = improvement.LeadTime.ToString();

                        if (improvement.Approval == 0 && improvement.UnitKerjaID == ((Users)Session["Users"]).UnitKerjaID)
                        {
                            btnDelete.Disabled = false;
                        }
                        else
                        {
                            btnDelete.Disabled = true;
                        }
                        if (improvement.Approval < ((Users)Session["Users"]).Apv && improvement.Approval > 0)
                        {
                            btnUnApprove.Disabled = false;
                            btnApprove.Disabled = false;
                        }
                        else if (improvement.Approval < ((Users)Session["Users"]).Apv)
                        {
                            btnUnApprove.Disabled = true;
                            btnApprove.Disabled = false;
                        }
                        else
                        {
                            btnUnApprove.Disabled = true;
                            btnApprove.Disabled = true;
                        }
                    }
                }
                else
                {
                    DisplayAJAXMessage(this, "Level Approval tidak mencukupi");
                }
            }
        }
        private void IsiPanelEmail()
        {
            if (RBStock.Checked == true)
                LStock.Text = "(Barang :" + RBStock.Text + ")";
            else
                LStock.Text = "(Barang :" + RBNonStock.Text + ")";
            LtxtItemCode.Text = ": " + txtItemCode.Text;
            LtxtItemName.Text = ": " + txtItemName.Text;
            //LddlDepartemen.Text = ": " + ddlDepartemen.SelectedItem.Text;
            //if (ddlDeptSection.SelectedIndex == 0)
            //    LddlDeptSection.Text = ": " ;
            //else
            //    LddlDeptSection.Text = ": "+ ddlDeptSection.SelectedItem.Text;
            LddlSatuan.Text = ": " + ddlSatuan.SelectedItem.Text;
            LtxtKeterangan.Text = ": " + txtKeterangan.Text;
            LddlGroup.Text = ": " + ddlGroup.SelectedItem.Text;
            LddlItemType.Text = ": " + ddlItemType.SelectedItem.Text;
            LtxtNama.Text = ": " + txtNama.Text;
            LtxtType.Text = ": " + txtType.Text;
            LtxtMerk.Text = ": " + txtMerk.Text;
            LtxtUkuran.Text = ": " + txtUkuran.Text;
            LtxtJenis.Text = ": " + txtJenis.Text;
            LtxtPartNum.Text = ": " + txtPartNum.Text;
        }
        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {

            LoadDataGridImprovement(LoadGridByCriteria());
            Thread.Sleep(100);
            txtSearch.Text = string.Empty;
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            if (txtSearch.Text == string.Empty)
                LoadDataGridImprovement(LoadGridImprovement());
            else
                LoadDataGridImprovement(LoadGridByCriteria());
        }

        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Master Improvement";
            eventLog.EventName = eventName;
            eventLog.DocumentNo = txtItemCode.Text;
            eventLog.CreatedBy = ((Users)Session["Users"]).UserName;
            EventLogFacade eventLogFacade = new EventLogFacade();
            int intResult = eventLogFacade.Insert(eventLog);
            if (eventLogFacade.Error == string.Empty)
                clearForm();
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private string ValidateText()
        {
            string strmessage = string.Empty;

            if (txtItemName.Text == string.Empty)
                return "Nama Item tidak boleh kosong";
            else if (ddlGroup.SelectedIndex == 0)
                return "Group barang tidak boleh kosong";
            else if (ddlSatuan.SelectedIndex == 0)
                return "Satuan tidak boleh kosong";
            return strmessage;
        }

        private void CollectName()
        {
            string strtxNama = string.Empty;
            string strtxtType = string.Empty;
            string strtxtUkuran = string.Empty;
            string strtxtMerk = string.Empty;
            string strtxtJenis = string.Empty;
            string strtxtPartNum = string.Empty;
            if (txtNama.Text.Trim() == string.Empty)
                return;
            else
                strtxNama = txtNama.Text.Trim();
            if (txtType.Text.Trim() != string.Empty) { strtxtType = " " + txtType.Text.Trim(); } else { strtxtType = string.Empty; }
            if (txtUkuran.Text.Trim() != string.Empty) { strtxtUkuran = " " + txtUkuran.Text.Trim(); } else { strtxtUkuran = string.Empty; }
            if (txtMerk.Text.Trim() != string.Empty) { strtxtMerk = " Merk " + txtMerk.Text.Trim(); } else { strtxtMerk = string.Empty; }
            if (txtJenis.Text.Trim() != string.Empty) { strtxtJenis = " " + txtJenis.Text.Trim(); } else { strtxtJenis = string.Empty; }
            if (txtPartNum.Text.Trim() != string.Empty) { strtxtPartNum = "-" + txtPartNum.Text.Trim(); } else { strtxtPartNum = string.Empty; }
            txtItemName.Text = strtxNama + strtxtType.Replace("&quote;", "''") + strtxtUkuran.Replace("&quote;", "''") + strtxtMerk + strtxtJenis + strtxtPartNum;
            if (ddlGroup.SelectedIndex == 0)
                ddlGroup.Focus();
        }

        protected void txNama_TextChanged(object sender, EventArgs e)
        {

            CollectName();
            CollectCode();
            txtMerk.Focus();
        }
        protected void txtType_TextChanged(object sender, EventArgs e)
        {
            CollectName();
            txtJenis.Focus();
        }
        protected void txtUkuran_TextChanged(object sender, EventArgs e)
        {
            CollectName();
            txtPartNum.Focus();

        }
        protected void txtMerk_TextChanged(object sender, EventArgs e)
        {
            CollectName();
            txtType.Focus();
        }

        protected void txtJenis_TextChanged(object sender, EventArgs e)
        {
            CollectName();
            txtUkuran.Focus();
        }

        protected void txtPartNum_TextChanged(object sender, EventArgs e)
        {
            CollectName();
            ddlSatuan.Focus();
        }


        protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //clearForm();
            LoadGroupI(ddlItemType.SelectedValue);
            LoadDataGridImprovement(LoadGridImprovement());
            CollectCode();
        }
        protected void txtPrefix_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ddlDepartemen_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadDeptSection();
            CollectCode();
        }
        protected void RBStock_CheckedChanged(object sender, EventArgs e)
        {
            if (RBStock.Checked == true)
            {
                CollectCode();
            }
        }
        protected void RBNonStock_CheckedChanged(object sender, EventArgs e)
        {
            if (RBNonStock.Checked == true)
            {
                CollectCode();
            }
        }

        protected void ddlDeptSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            CollectCode();
        }

        protected void btnEmail_ServerClick(object sender, EventArgs e)
        {
            string alamatEmail = txtEmailApprover.Text.ToString().TrimEnd();
            if (txtItemCode.Text == string.Empty)
                return;
            PanelEmail.Visible = true;
            //IsiPanelEmail();
            //sendMail(alamatEmail);
            KirimEmail(alamatEmail);
            PanelEmail.Visible = false;
        }

        private string GridToHtml(Panel gv)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            //gv.RenderControl(hw);
            string nBarang = "Kode Barang : " + txtItemCode.Text + "\n\r Nama Barang : " + txtItemName.Text + "\n\r";

            return nBarang;

        }

        private void sendMail(string alamatEmail)
        {
            MailMessage msg = new MailMessage();
            EncryptPasswordFacade encryptPasswordFacade = new EncryptPasswordFacade();
            string PaswTrue = string.Empty;
            try
            {
                Depo depo = new Depo();
                DepoFacade depofacade = new DepoFacade();
                depo = depofacade.RetrieveById(((Users)Session["Users"]).UnitKerjaID);
                EmailReportFacade emailFacade = new EmailReportFacade();
                msg.From = new MailAddress(((Users)Session["Users"]).UsrMail);
                msg.To.Add(alamatEmail);
                msg.Body += emailFacade.mailBody1() + "\n\r";
                msg.Body += GridToHtml(PanelEmail);
                msg.Body += emailFacade.mailBody2(((Users)Session["Users"]).UserName);
                msg.Body += emailFacade.mailFooter();

                //msg.Attachments.Add(new Attachment(pdfFile));
                msg.IsBodyHtml = true;
                msg.Subject = emailFacade.mailSubject(depo.DepoName);
                SmtpClient smt = new SmtpClient(emailFacade.mailSmtp());
                smt.Port = emailFacade.mailPort();
                PaswTrue = encryptPasswordFacade.DecryptString(((Users)Session["Users"]).PssMail.Trim());
                smt.Credentials = new System.Net.NetworkCredential(((Users)Session["Users"]).UsrMail, PaswTrue);
                smt.EnableSsl = true;
                smt.Send(msg);

                resultMailSucc.Visible = true;
                resultMailSucc.Text = "Email dikirim";
                resultMailFail.Visible = false;
                resultMailFail.Text = string.Empty;
                //InsertLog("Send Mail Laporan Iso");
            }
            catch
            {
                resultMailFail.Visible = true;
                resultMailFail.Text = "Gagal dikirim!";
                resultMailSucc.Visible = false;
                resultMailSucc.Text = string.Empty;
            }
            msg.Dispose();
        }
        public void KirimEmail(string Email)
        {
            try
            {
                if (Email.Trim() == string.Empty)
                    return;

                MailMessage msg = new MailMessage();
                EmailReportFacade emailFacade = new EmailReportFacade();
                msg.From = new MailAddress("system_support@grcboard.com");
                msg.To.Add(Email);
                msg.Subject = emailFacade.mailSubject("");
                msg.Body += emailFacade.mailBody1() + "\n\r";
                msg.Body += "Kode Barang    : " + txtItemCode.Text + "\n\r";
                msg.Body += "Nama Barang    : " + txtItemName.Text + "\n\r";
                msg.Body += emailFacade.mailBody2(((Users)Session["Users"]).UserName) + "\n\r\n\r\n\r";
                //msg.Body += emailFacade.mailFooter();
                SmtpClient smt = new SmtpClient(emailFacade.mailSmtp());
                smt.Host = emailFacade.mailSmtp();
                smt.Port = emailFacade.mailPort();
                smt.EnableSsl = true;
                smt.DeliveryMethod = SmtpDeliveryMethod.Network;
                smt.UseDefaultCredentials = false;
                smt.Credentials = new System.Net.NetworkCredential("noreplay@grcboard.com", "grc123!@#");
                //bypas certificate
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                        System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                        System.Security.Cryptography.X509Certificates.X509Chain chain,
                        System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };


                smt.Send(msg);
            }
            catch { }
            clearForm();
            resultMailSucc.Visible = true;
            resultMailSucc.Text = "Email dikirim";
            resultMailFail.Visible = false;
            resultMailFail.Text = string.Empty;
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string alamatEmail = LblEmail.Text;
            sendMail(alamatEmail);
        }

        protected void ddlGroup_TextChanged(object sender, EventArgs e)
        {
            CollectCode();
            txtNama.Focus();
        }
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            if (PanelItemName.Visible == false)
                PanelItemName.Visible = true;
            else
                PanelItemName.Visible = false;
        }
        protected void btnSearch0_Click(object sender, ImageClickEventArgs e)
        {
            if (PanelItemMerk.Visible == false)
                PanelItemMerk.Visible = true;
            else
                PanelItemMerk.Visible = false;
        }
        protected void btnSearch2_Click(object sender, ImageClickEventArgs e)
        {
            if (PanelItemType.Visible == false)
                PanelItemType.Visible = true;
            else
                PanelItemType.Visible = false;
        }
        protected void btnSearch4_Click(object sender, ImageClickEventArgs e)
        {
            if (PanelUkuran.Visible == false)
                PanelUkuran.Visible = true;
            else
                PanelUkuran.Visible = false;
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            //BItemName itemname = new BItemName();
            //BItemNameFacade itemnamefacade = new BItemNameFacade();
            if (txtAddName.Text.Trim() == string.Empty)
                return;
            //itemname.ItemName = txtAddName.Text;
            //itemname.CreatedBy = ((Users)Session["Users"]).UserName;

            if (((Users)Session["Users"]).UnitKerjaID != 7)
            {
                PostItemNameKrw();
                //itemnamefacade.Insert(itemname);
            }
            else
            {
                //itemnamefacade.Insert(itemname);
                PostItemNameCtrp();
            }
            LoadItemName();
            txtAddName.Text = string.Empty;
        }

        protected void btnSave0_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["incode"] == null || txtNama.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Nama Barang belum ditentukan");
                return;
            }
            if (((Users)Session["Users"]).UnitKerjaID != 7)
            {
                PostItemMerkKrw();
            }
            else
            {
                PostItemMerkCtrp();
            }
            LoadItemMerk(Session["incode"].ToString());
            txtAddMerk.Text = string.Empty;
        }
        protected void btnSave2_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["incode"] == null || txtNama.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Nama Barang belum ditentukan");
                return;
            }
            if (((Users)Session["Users"]).UnitKerjaID != 7)
            {
                PostUkuranKrw();
            }
            else
            {
                PostUkuranCtrp();
            }
            LoadUkuran(Session["incode"].ToString());
            txtAddUkuran.Text = string.Empty;

        }
        protected void btnSave1_Click(object sender, ImageClickEventArgs e)
        {
            /**
             * Add item type to database krwg and ctrp
             */

            if (Session["incode"] == null || txtNama.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Nama Barang belum ditentukan");
                return;
            }
            if (((Users)Session["Users"]).UnitKerjaID != 7)
            {
                PostItemTypeKrw();
            }
            else
            {
                PostItemTypeCtrp();
            }

            LoadItemType(Session["incode"].ToString());
            txtAddType.Text = string.Empty;
        }

        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            txtAddName.Visible = true;
            btnSave.Visible = true;
        }

        protected void GridView4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btnAdd0_Click(object sender, ImageClickEventArgs e)
        {
            txtAddMerk.Visible = true;
            btnSave0.Visible = true;
        }

        protected void btnAdd2_Click(object sender, ImageClickEventArgs e)
        {
            txtAddUkuran.Visible = true;
            btnSave2.Visible = true;
        }

        protected void UpdateStsApproved()
        {
            Domain.Improvement imp = new Domain.Improvement();
            ImprovementFacade impf = new ImprovementFacade();
            imp.RowStatus = 0;
            imp.Approval = ((Users)Session["Users"]).Apv;
            imp.LastModifiedBy = ((Users)Session["Users"]).UserName;
            imp.ItemCode = txtItemCode.Text.Trim();
            imp.ID = Convert.ToInt16(Session["id"]);
            impf.Update(imp);
        }
        protected void btnApprove_ServerClick(object sender, EventArgs e)
        {
            /** jika aprovalnya 2 (manager) posting juga ke server ctrp atau sebaliknya */
            if (((Users)Session["Users"]).Apv == 1) { PostImproovement(1); }
            if (((Users)Session["Users"]).Apv == 2) { UpdateImpCtrp(txtItemCode.Text.Trim().ToString(), ((Users)Session["Users"]).Apv); }
            LoadDataGridImprovement(LoadGridImprovement());
            btnApprove.Disabled = true;
        }

        protected void btnUnApprove_ServerClick(object sender, EventArgs e)
        {
            /**
             * Un approve status head
             */

            Domain.Improvement imp = new Domain.Improvement();
            ImprovementFacade impf = new ImprovementFacade();
            imp.RowStatus = 0;

            /** Status UnApproved **/
            imp.Approval = 9;// (((Users)Session["Users"]).Apv) - 1;
                             /** End Status UnApproved **/

            imp.LastModifiedBy = ((Users)Session["Users"]).UserName;
            imp.ItemCode = txtItemCode.Text.Trim();
            imp.ID = Convert.ToInt16(Session["id"]);
            /**
             * collect data 
             */
            DataTable dt = new DataTable();
            dt.Columns.Add("Approval", typeof(int));
            dt.Columns.Add("ItemCode", typeof(string));

            DataRow row = dt.NewRow();
            row["Approval"] = 0;// (((Users)Session["Users"]).Apv);
            row["ItemCode"] = txtItemCode.Text.Trim();
            dt.Rows.Add(row);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            if (((Users)Session["Users"]).UnitKerjaID == 7)
            //if(imp.UnitKerjaID==7)
            {
                try
                {
                    WebReference_Ctrp.Service1 WebServiceCtrp = new WebReference_Ctrp.Service1();
                    WebServiceCtrp.DeleteImprovement(dt);
                    impf.Update(imp);
                    try
                    {
                        WebReference_Jmb.Service1 WebServiceJombang = new WebReference_Jmb.Service1();
                        WebServiceJombang.DeleteImprovement(dt);
                        impf.Update(imp);
                    }
                    catch
                    {
                        DisplayAJAXMessage(this, "Koneksi database Jombang error");
                    }
                }
                catch
                {
                    DisplayAJAXMessage(this, "Koneksi database Karawang error");
                }
            }
            //else if (imp.UnitKerjaID==1)
            else if (((Users)Session["Users"]).UnitKerjaID == 1)
            {
                try
                {
                    WebReference_Krwg.Service1 WebServiceKrw = new WebReference_Krwg.Service1();
                    WebServiceKrw.DeleteImprovement(dt);
                    impf.Update(imp);

                    try
                    {
                        WebReference_Jmb.Service1 WebServiceJombang = new WebReference_Jmb.Service1();
                        WebServiceJombang.DeleteImprovement(dt);
                        impf.Update(imp);
                    }
                    catch
                    {
                        DisplayAJAXMessage(this, "Koneksi database Jombang error");
                    }

                }

                catch
                {
                    DisplayAJAXMessage(this, "Koneksi database Karawang error");
                }

            }

            LoadDataGridImprovement(LoadGridImprovement());
            btnApprove.Disabled = true;
            btnUnApprove.Disabled = true;
        }

        protected void btnAdd1_Click(object sender, ImageClickEventArgs e)
        {
            txtAddType.Visible = true;
            btnSave1.Visible = true;
        }

        protected void LoadItemName()
        {
            ArrayList arrItemName = new ArrayList();
            DataSet arrItemNameRemote = new DataSet();
            BItemNameFacade itemnameFacade = new BItemNameFacade();
            arrItemName = itemnameFacade.Retrieve();
            GridItemName.DataSource = arrItemName;
            GridItemName.DataBind();
            Session["kodetype"] = "00";
            Session["kodemerk"] = "00";
            Session["kodeukuran"] = "00";
        }

        protected void LoadItemMerk(string INCode)
        {
            ArrayList arrItemMerk = new ArrayList();
            DataSet arrItemMerkRemote = new DataSet();
            BItemMerkFacade itemmerkFacade = new BItemMerkFacade();
            arrItemMerk = itemmerkFacade.RetrieveByInCode(INCode);
            GridItemMerk.DataSource = arrItemMerk;
            GridItemMerk.DataBind();
            txtMerk.Text = string.Empty;
        }

        protected void LoadItemType(string INCode)
        {
            ArrayList arrItemType = new ArrayList();
            DataSet arrItemTypeRemote = new DataSet();
            BItemTypeFacade itemTypeFacade = new BItemTypeFacade();
            arrItemType = itemTypeFacade.RetrieveByInCode(INCode);
            GridItemType.DataSource = arrItemType;
            GridItemType.DataBind();
            txtType.Text = string.Empty;
        }
        protected void LoadUkuran(string INCode)
        {
            ArrayList arrItemType = new ArrayList();
            DataSet arrItemTypeRemote = new DataSet();
            BItemTypeFacade itemTypeFacade = new BItemTypeFacade();
            arrItemType = itemTypeFacade.RetrieveUkuran(INCode);
            GridUkuran.DataSource = arrItemType;
            GridUkuran.DataBind();
            txtUkuran.Text = string.Empty;
        }
        protected void GridItemName_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string kode = string.Empty;
                GridViewRow row = GridItemName.Rows[index];
                txtNama.Text = row.Cells[1].Text.Trim();
                PanelItemName.Visible = false;
                Session["incode"] = row.Cells[0].Text;
                LoadItemMerk(Session["incode"].ToString());
                LoadItemType(Session["incode"].ToString());
                LoadUkuran(Session["incode"].ToString());
                CollectName();
                CollectCode();
            }
        }
        protected void GridItemMerk_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string kode = string.Empty;
                GridViewRow row = GridItemMerk.Rows[index];
                txtMerk.Text = HttpUtility.HtmlDecode((row.Cells[1].Text == "&nbsp;") ? string.Empty : row.Cells[1].Text.Trim());
                Session["kodemerk"] = (row.Cells[0].Text == "&nbsp;") ? "00" : row.Cells[0].Text;
                PanelItemMerk.Visible = false;
                CollectName();
                CollectCode();
            }
        }

        protected void GridItemType_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string kode = string.Empty;
                GridViewRow row = GridItemType.Rows[index];
                txtType.Text = HttpUtility.HtmlDecode((row.Cells[1].Text == "&nbsp;") ? string.Empty : row.Cells[1].Text.Trim());
                txtType.Text = (row.Cells[1].Text == "&nbsp;") ? string.Empty : row.Cells[1].Text.Trim();
                Session["kodetype"] = (row.Cells[0].Text == "&nbsp;") ? "00" : row.Cells[0].Text;
                PanelItemType.Visible = false;
                CollectName();
                CollectCode();
            }
        }
        protected void GridUkuran_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string kode = string.Empty;
                GridViewRow row = GridUkuran.Rows[index];
                txtUkuran.Text = HttpUtility.HtmlDecode((row.Cells[1].Text == "&nbsp;") ? string.Empty : row.Cells[1].Text.Trim());
                Session["kodeukuran"] = (row.Cells[0].Text == "&nbsp;") ? "00" : row.Cells[0].Text;
                PanelUkuran.Visible = false;
                CollectName();
                CollectCode();
            }

        }

        protected void GridUkuran_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void PostImproovement(int app)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ItemCode", typeof(string));
            dt.Columns.Add("ItemName", typeof(string));
            dt.Columns.Add("DeptID", typeof(int));
            dt.Columns.Add("UOMID", typeof(int));
            dt.Columns.Add("GroupID", typeof(int));
            dt.Columns.Add("ItemTypeID", typeof(int));
            dt.Columns.Add("CreatedBy", typeof(string));
            dt.Columns.Add("Keterangan", typeof(string));
            dt.Columns.Add("UnitKerjaID", typeof(int));
            dt.Columns.Add("Nama", typeof(string));
            dt.Columns.Add("Type", typeof(string));
            dt.Columns.Add("Ukuran", typeof(string));
            dt.Columns.Add("Merk", typeof(string));
            dt.Columns.Add("Jenis", typeof(string));
            dt.Columns.Add("PartNum", typeof(string));
            dt.Columns.Add("Approval", typeof(int));
            dt.Columns.Add("Stock", typeof(int));
            dt.Columns.Add("LeadTime", typeof(int));

            dt.Columns.Add("AmGroupID", typeof(int));
            dt.Columns.Add("AmClassID", typeof(int));
            dt.Columns.Add("AmSubClassID", typeof(int));
            dt.Columns.Add("AmLokasiID", typeof(int));
            dt.Columns.Add("AmKodeAsset", typeof(int));


            DataRow row = dt.NewRow();
            row["ItemCode"] = txtItemCode.Text.TrimEnd();
            row["ItemName"] = txtItemName.Text.TrimEnd();
            row["DeptID"] = ((Users)Session["Users"]).DeptID;
            row["UOMID"] = int.Parse(ddlSatuan.SelectedValue);
            row["GroupID"] = int.Parse(ddlGroup.SelectedValue);
            row["ItemTypeID"] = int.Parse(ddlItemType.SelectedValue);
            row["Keterangan"] = txtKeterangan.Text.TrimEnd();
            row["CreatedBy"] = ((Users)Session["Users"]).UserName;
            row["UnitKerjaID"] = ((Users)Session["Users"]).UnitKerjaID;
            row["Nama"] = txtNama.Text.TrimEnd();
            row["Type"] = txtType.Text.TrimEnd();
            row["Merk"] = txtMerk.Text.TrimEnd();
            row["Ukuran"] = txtUkuran.Text.TrimEnd();
            row["Jenis"] = txtJenis.Text.TrimEnd();
            row["Partnum"] = txtPartNum.Text.TrimEnd();
            row["Approval"] = ((Users)Session["Users"]).Apv;
            row["Stock"] = (RBNonStock.Checked == true) ? 0 : 1;
            row["LeadTime"] = int.Parse(leadTime.Text);

            row["AmGroupID"] = 0;
            row["AmClassID"] = 0;
            row["AmSubClassID"] = 0;
            row["AmLokasiID"] = 0;
            row["AmKodeAsset"] = 0;

            dt.Rows.Add(row);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            /**
             * posting to table improvement lawan
             * tested on 19-12-2013 14:30
             */
            if (((Users)Session["Users"]).UnitKerjaID == 1)
            {
                try
                {
                    WebReference_Krwg.Service1 webServiceKRW = new WebReference_Krwg.Service1();
                    webServiceKRW.InsertImprovement(dt);

                    WebReference_Jmb.Service1 webServiceJbg = new WebReference_Jmb.Service1();
                    webServiceJbg.InsertImprovement(dt);

                    UpdateStsApproved();
                    //KirimEmail(txtEmailApprover.Text.ToString().TrimEnd()); 
                }
                catch
                {
                    DisplayAJAXMessage(this, "Koneksi Database ke Karawang error");
                }

            }
            else if (((Users)Session["Users"]).UnitKerjaID == 7)
            {
                try
                {
                    WebReference_Ctrp.Service1 webServiceCtrp = new WebReference_Ctrp.Service1();
                    webServiceCtrp.InsertImprovement(dt);

                    WebReference_Jmb.Service1 webServiceJbg = new WebReference_Jmb.Service1();
                    webServiceJbg.InsertImprovement(dt);

                    UpdateStsApproved();
                    //KirimEmail(txtEmailApprover.Text.ToString().TrimEnd()); 
                }
                catch
                {
                    DisplayAJAXMessage(this, "Koneksi Database ke Citeureup error");
                }
            }
            else
            {
                try
                {
                    WebReference_Ctrp.Service1 webServiceCtrp = new WebReference_Ctrp.Service1();
                    webServiceCtrp.InsertImprovement(dt);

                    WebReference_Krwg.Service1 webServiceKRW = new WebReference_Krwg.Service1();
                    webServiceKRW.InsertImprovement(dt);

                    UpdateStsApproved();
                    //KirimEmail(txtEmailApprover.Text.ToString().TrimEnd()); 
                }
                catch
                {
                    DisplayAJAXMessage(this, "Koneksi Database ke Citeureup error");
                }
            }
        }

        protected void PostInventory()
        {
            /**
             * Insert to table inventory
             * Transaksi ini hanya untuk server karawang
             */
            DataTable dt = new DataTable();
            dt.Columns.Add("ItemCode", typeof(string));
            dt.Columns.Add("ItemName", typeof(string));
            dt.Columns.Add("SupplierCode", typeof(string));
            dt.Columns.Add("UOMID", typeof(int));
            dt.Columns.Add("Jumlah", typeof(decimal));
            dt.Columns.Add("Harga", typeof(decimal));
            dt.Columns.Add("MinStock", typeof(decimal));
            dt.Columns.Add("DeptID", typeof(int));
            dt.Columns.Add("RakID", typeof(int));
            dt.Columns.Add("GroupID", typeof(int));
            dt.Columns.Add("ItemTypeID", typeof(int));
            dt.Columns.Add("Gudang", typeof(int));
            dt.Columns.Add("ShortKey", typeof(string));
            dt.Columns.Add("Keterangan", typeof(string));
            dt.Columns.Add("Head", typeof(int));
            dt.Columns.Add("CreatedBy", typeof(string));
            dt.Columns.Add("LastModifiedBy", typeof(string));
            dt.Columns.Add("Nama", typeof(string));
            dt.Columns.Add("Type", typeof(string));
            dt.Columns.Add("Ukuran", typeof(string));
            dt.Columns.Add("Merk", typeof(string));
            dt.Columns.Add("Jenis", typeof(string));
            dt.Columns.Add("PartNum", typeof(string));
            dt.Columns.Add("LeadTime", typeof(int));

            dt.Columns.Add("AmGroupID", typeof(int));
            dt.Columns.Add("AmClassID", typeof(int));
            dt.Columns.Add("AmSubClassID", typeof(int));
            dt.Columns.Add("AmLokasiID", typeof(int));
            dt.Columns.Add("AmKodeAsset", typeof(int));

            DataRow row = dt.NewRow();
            row["ItemCode"] = txtItemCode.Text.TrimEnd();
            row["ItemName"] = txtItemName.Text.TrimEnd();
            row["SupplierCode"] = string.Empty;
            row["UOMID"] = ddlSatuan.SelectedValue;
            row["Jumlah"] = 0;
            row["Harga"] = 0;
            row["MinStock"] = 0;
            row["DeptID"] = 0;
            row["RakID"] = 0;
            row["GroupID"] = ddlGroup.SelectedValue;
            row["ItemTypeID"] = ddlItemType.SelectedValue;
            row["Gudang"] = 0;
            row["ShortKey"] = string.Empty;
            row["Keterangan"] = string.Empty;
            row["Head"] = 0;
            row["CreatedBy"] = ((Users)Session["Users"]).UserName;
            row["LastModifiedBy"] = ((Users)Session["Users"]).UserName;
            row["Nama"] = txtNama.Text.TrimEnd();
            row["Type"] = txtType.Text.TrimEnd();
            row["Ukuran"] = txtUkuran.Text.TrimEnd();
            row["Merk"] = txtMerk.Text.TrimEnd();
            row["Jenis"] = txtJenis.Text.TrimEnd();
            row["PartNum"] = txtPartNum.Text.TrimEnd();
            row["LeadTime"] = leadTime.Text;

            row["AmGroupID"] = 0;
            row["AmClassID"] = 0;
            row["AmSubClassID"] = 0;
            row["AmLokasiID"] = 0;
            row["AmKodeAsset"] = 0;

            dt.Rows.Add(row);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            /** posting to table inventory on each server 
             * test on 19-12-2013 14:00 -- OK
             */
            try
            {
                WebReference_Krwg.Service1 webServiceKRW = new WebReference_Krwg.Service1();
                webServiceKRW.InsertInventory(dt);
                UpdateStsApproved();
                try
                {
                    WebReference_Ctrp.Service1 webServiceCtrp = new WebReference_Ctrp.Service1();
                    webServiceCtrp.InsertInventory(dt);
                    UpdateStsApproved();

                    try
                    {
                        WebReference_Jmb.Service1 webServiceJombang = new WebReference_Jmb.Service1();
                        webServiceJombang.InsertInventory(dt);
                        UpdateStsApproved();
                    }
                    catch
                    {
                        //DisplayAJAXMessage(this, "Koneksi Database ke JOmbang error");
                    }
                }

                catch
                {
                    //DisplayAJAXMessage(this, "Koneksi Database ke Citeureup error");
                }
            }
            catch
            {
                DisplayAJAXMessage(this, "Koneksi Database ke Kawangan error");
            }

        }

        protected void UpdateImpCtrp(string ItemCode, int Approval)
        {
            /**
             * Posting kode baru ke table inventory
             */
            Users User = (Users)Session["Users"];

            DataTable dt = new DataTable();
            dt.Columns.Add("Approval", typeof(int));
            dt.Columns.Add("ItemCode", typeof(string));

            DataRow row = dt.NewRow();
            row["Approval"] = Approval;
            row["ItemCode"] = ItemCode.ToString();
            dt.Rows.Add(row);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            string strerror = string.Empty; string strerror2 = string.Empty;

            if (User.UnitKerjaID != 13)
            {
                WebReference_Ctrp.Service1 webServiceCtrp = new WebReference_Ctrp.Service1();
                strerror = webServiceCtrp.UpdateStatusImprovement(dt);

                WebReference_Jmb.Service1 webServiceJombang = new WebReference_Jmb.Service1();
                strerror2 = webServiceJombang.UpdateStatusImprovement(dt);
            }
            else
            {
                WebReference_Ctrp.Service1 webServiceCtrp = new WebReference_Ctrp.Service1();
                strerror = webServiceCtrp.UpdateStatusImprovement(dt);

                WebReference_Krwg.Service1 webServiceKrwg = new WebReference_Krwg.Service1();
                strerror2 = webServiceKrwg.UpdateStatusImprovement(dt);

            }

            //WebReference_Ctrp.Service1 webServiceCtrp = new WebReference_Ctrp.Service1();
            //string strerror = webServiceCtrp.UpdateStatusImprovement(dt);

            //WebReference_Jombang.Service1 webServiceJombang = new WebReference_Jombang.Service1();       
            //string strerror2 = webServiceJombang.UpdateStatusImprovement(dt);

            if (strerror == string.Empty && strerror2 == string.Empty || strerror == "0" && strerror2 == "0")
            {
                PostInventory();
            }
            else
            {
                DisplayAJAXMessage(this, strerror + " Koneksi Database ke Citeureup error");
                //PostInventory();
                //DisplayAJAXMessage(this, "Approved Success");
            }
        }


        protected void InsertToDb(string level)
        {
            /**
             * simpan data to table BItemName, BItemType, BItemMerk
             * di masing-masing lokasi
             */

            switch (level)
            {
                case "itemname":
                    BItemName itemname = new BItemName();
                    BItemNameFacade itemnamefacade = new BItemNameFacade();
                    itemname.ItemName = txtAddName.Text;
                    itemname.CreatedBy = ((Users)Session["Users"]).UserName;
                    itemnamefacade.Insert(itemname);
                    break;
                case "itemmerk":
                    BItemMerk itemmerk = new BItemMerk();
                    BItemMerkFacade itemmerkfacade = new BItemMerkFacade();
                    itemmerk.InMerk = txtAddMerk.Text;
                    itemmerk.InCode = Session["incode"].ToString();
                    itemmerk.CreatedBy = ((Users)Session["Users"]).UserName;
                    itemmerkfacade.Insert(itemmerk);
                    break;
                case "itemtype":
                    BItemType itemtype = new BItemType();
                    BItemTypeFacade itemtypefacade = new BItemTypeFacade();
                    itemtype.InType = txtAddType.Text;
                    itemtype.InCode = Session["incode"].ToString();
                    itemtype.CreatedBy = ((Users)Session["Users"]).UserName;
                    itemtypefacade.Insert(itemtype);
                    break;
                case "itemukuran":
                    BItemType itemukuran = new BItemType();
                    BItemTypeFacade itemukuranfacade = new BItemTypeFacade();
                    itemukuran.InType = txtAddUkuran.Text;
                    itemukuran.InCode = Session["incode"].ToString();
                    itemukuran.CreatedBy = ((Users)Session["Users"]).UserName;
                    itemukuranfacade.InsertUkuran(itemukuran);
                    break;

            }
        }


        #region Post Ukuran
        protected void PostUkuranCtrp()
        {
            /**
             * Posting to BItemUkuran table ctrp
             */
            DataTable dt = new DataTable();
            dt.Columns.Add("InType", typeof(string));
            dt.Columns.Add("InCode", typeof(string));
            dt.Columns.Add("CreatedBy", typeof(string));

            DataRow row = dt.NewRow();
            row["InType"] = txtAddUkuran.Text.TrimEnd();
            row["InCode"] = Session["incode"].ToString();
            row["CreatedBy"] = ((Users)Session["Users"]).UserName;
            dt.Rows.Add(row);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            try
            {
                WebReference_Ctrp.Service1 webServiceKRW = new WebReference_Ctrp.Service1();
                webServiceKRW.InsertItemUkuran(dt);
                InsertToDb("itemukuran");
            }
            catch
            {
                DisplayAJAXMessage(this, "Koneksi Internet Error");
                return;
            }

        }
        protected void PostUkuranKrw()
        {
            /**
             * Posting to BItemUkuran table krw
             */

            DataTable dt = new DataTable();
            dt.Columns.Add("InType", typeof(string));
            dt.Columns.Add("InCode", typeof(string));
            dt.Columns.Add("CreatedBy", typeof(string));

            DataRow row = dt.NewRow();
            row["InType"] = txtAddUkuran.Text.TrimEnd();
            row["InCode"] = Session["incode"].ToString();
            row["CreatedBy"] = ((Users)Session["Users"]).UserName;
            dt.Rows.Add(row);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            try
            {
                WebReference_Krwg.Service1 webServiceKRW = new WebReference_Krwg.Service1();
                webServiceKRW.InsertItemUkuran(dt);
                InsertToDb("itemukuran");
            }
            catch
            {
                DisplayAJAXMessage(this, "Koneksi Internet Error");
                return;
            }
        }
        protected void PostUkuranJmb()
        {
            /**
             * Posting to BItemUkuran table Jombang
             */

            DataTable dt = new DataTable();
            dt.Columns.Add("InType", typeof(string));
            dt.Columns.Add("InCode", typeof(string));
            dt.Columns.Add("CreatedBy", typeof(string));

            DataRow row = dt.NewRow();
            row["InType"] = txtAddUkuran.Text.TrimEnd();
            row["InCode"] = Session["incode"].ToString();
            row["CreatedBy"] = ((Users)Session["Users"]).UserName;
            dt.Rows.Add(row);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            try
            {
                WebReference_Jmb.Service1 webServiceJombang = new WebReference_Jmb.Service1();
                webServiceJombang.InsertItemUkuran(dt);
                InsertToDb("itemukuran");
            }
            catch
            {
                DisplayAJAXMessage(this, "Koneksi Internet Error");
                return;
            }
        }
        #endregion

        #region Post ItemType
        protected void PostItemTypeCtrp()
        {
            /**
             * Posting to BItemType table Ctrp database
             */
            DataTable dt = new DataTable();
            dt.Columns.Add("InType", typeof(string));
            dt.Columns.Add("InCode", typeof(string));
            dt.Columns.Add("CreatedBy", typeof(string));

            DataRow row = dt.NewRow();
            row["InType"] = txtAddType.Text.TrimEnd();
            row["InCode"] = Session["incode"].ToString();
            row["CreatedBy"] = ((Users)Session["Users"]).UserName;
            dt.Rows.Add(row);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            try
            {
                WebReference_Ctrp.Service1 webServiceKRW = new WebReference_Ctrp.Service1();
                webServiceKRW.InsertItemType(dt);
                InsertToDb("itemtype");
            }
            catch
            {
                DisplayAJAXMessage(this, "Koneksi Internet Error");
                return;
            }
        }
        protected void PostItemTypeKrw()
        {
            /**
             * Posting to BItemType Teble Krw database
             */
            DataTable dt = new DataTable();
            dt.Columns.Add("InType", typeof(string));
            dt.Columns.Add("InCode", typeof(string));
            dt.Columns.Add("CreatedBy", typeof(string));

            DataRow row = dt.NewRow();
            row["InType"] = txtAddType.Text.TrimEnd();
            row["InCode"] = Session["incode"].ToString();
            row["CreatedBy"] = ((Users)Session["Users"]).UserName;
            dt.Rows.Add(row);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            try
            {
                WebReference_Krwg.Service1 webServiceKRW = new WebReference_Krwg.Service1();
                webServiceKRW.InsertItemType(dt);
                InsertToDb("itemtype");
            }
            catch
            {
                DisplayAJAXMessage(this, "Koneksi Internet Error");
                return;
            }
        }
        protected void PostItemTypeJmb()
        {
            /**
             * Posting to BItemType Teble Jombang database
             */
            DataTable dt = new DataTable();
            dt.Columns.Add("InType", typeof(string));
            dt.Columns.Add("InCode", typeof(string));
            dt.Columns.Add("CreatedBy", typeof(string));

            DataRow row = dt.NewRow();
            row["InType"] = txtAddType.Text.TrimEnd();
            row["InCode"] = Session["incode"].ToString();
            row["CreatedBy"] = ((Users)Session["Users"]).UserName;
            dt.Rows.Add(row);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            try
            {
                WebReference_Jmb.Service1 webServiceJombang = new WebReference_Jmb.Service1();
                webServiceJombang.InsertItemType(dt);
                InsertToDb("itemtype");
            }
            catch
            {
                DisplayAJAXMessage(this, "Koneksi Internet Error");
                return;
            }
        }
        #endregion

        #region Post ItemName
        protected void PostItemNameCtrp()
        {
            /**
             * Posting Item Name to BItemName Ctrp database
             */
            DataTable dt = new DataTable();
            dt.Columns.Add("ItemName", typeof(string));
            dt.Columns.Add("CreatedBy", typeof(string));

            DataRow row = dt.NewRow();
            row["ItemName"] = txtAddName.Text.TrimEnd();
            row["CreatedBy"] = ((Users)Session["Users"]).UserName;
            dt.Rows.Add(row);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            String strError = string.Empty;
            string strerror = string.Empty;
            try
            {
                WebReference_Ctrp.Service1 webServiceCtrp = new WebReference_Ctrp.Service1();
                webServiceCtrp.InsertItemName(dt);
                InsertToDb("itemname");
            }
            //catch
            //{
            //    DisplayAJAXMessage(this, "Koneksi Internet Error");
            //    return;
            //}
            catch (Exception ex)
            {
                strError = ex.Message;
                DisplayAJAXMessage(this, strError);
                return;
            }
        }
        protected void PostItemNameKrw()
        {
            /**
             * Posting BItemName table krw database
             */
            DataTable dt = new DataTable();
            dt.Columns.Add("ItemName", typeof(string));
            dt.Columns.Add("CreatedBy", typeof(string));

            DataRow row = dt.NewRow();
            row["ItemName"] = txtAddName.Text.TrimEnd();
            row["CreatedBy"] = ((Users)Session["Users"]).UserName;
            dt.Rows.Add(row);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            try
            {
                WebReference_Krwg.Service1 webServiceKRW = new WebReference_Krwg.Service1();
                string result = webServiceKRW.InsertItemName(dt);
                InsertToDb("itemname");
            }
            catch
            {
                DisplayAJAXMessage(this, "Koneksi Internet Error");
                return;
            }
        }
        protected void PostItemNameJmb()
        {
            /**
             * Posting BItemName table Jombang database
             */
            DataTable dt = new DataTable();
            dt.Columns.Add("ItemName", typeof(string));
            dt.Columns.Add("CreatedBy", typeof(string));

            DataRow row = dt.NewRow();
            row["ItemName"] = txtAddName.Text.TrimEnd();
            row["CreatedBy"] = ((Users)Session["Users"]).UserName;
            dt.Rows.Add(row);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            try
            {
                WebReference_Jmb.Service1 webServiceJombang = new WebReference_Jmb.Service1();
                string result = webServiceJombang.InsertItemName(dt);
                InsertToDb("itemname");
            }
            catch
            {
                DisplayAJAXMessage(this, "Koneksi Internet Error");
                return;
            }
        }
        #endregion

        #region Post Item Merk
        protected void PostItemMerkCtrp()
        {
            /**
             * Posting Item Name to BItemMerk table Ctrp database
             */

            DataTable dt = new DataTable();
            dt.Columns.Add("InMerk", typeof(string));
            dt.Columns.Add("InCode", typeof(string));
            dt.Columns.Add("CreatedBy", typeof(string));

            DataRow row = dt.NewRow();
            row["InMerk"] = txtAddMerk.Text.TrimEnd();
            row["InCode"] = Session["incode"].ToString();
            row["CreatedBy"] = ((Users)Session["Users"]).UserName;
            dt.Rows.Add(row);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            try
            {
                WebReference_Ctrp.Service1 webServiceKRW = new WebReference_Ctrp.Service1();
                webServiceKRW.InsertItemMerk(dt);
                InsertToDb("itemmerk");
            }
            catch
            {
                DisplayAJAXMessage(this, "Koneksi Internet Error");
                return;
            }
        }
        protected void PostItemMerkKrw()
        {
            /**
             * Posting BItemMerk table krwg database
             */
            DataTable dt = new DataTable();
            dt.Columns.Add("InMerk", typeof(string));
            dt.Columns.Add("InCode", typeof(string));
            dt.Columns.Add("CreatedBy", typeof(string));

            DataRow row = dt.NewRow();
            row["InMerk"] = txtAddMerk.Text.TrimEnd();
            row["InCode"] = Session["incode"].ToString();
            row["CreatedBy"] = ((Users)Session["Users"]).UserName;
            dt.Rows.Add(row);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            try
            {
                WebReference_Krwg.Service1 webServiceKRW = new WebReference_Krwg.Service1();
                string result = webServiceKRW.InsertItemMerk(dt);
                InsertToDb("itemmerk");
            }
            catch
            {
                DisplayAJAXMessage(this, "Koneksi Internet Error");
                return;
            }
        }
        protected void PostItemMerkJmbg()
        {
            /**
             * Posting BItemMerk table krwg database
             */
            DataTable dt = new DataTable();
            dt.Columns.Add("InMerk", typeof(string));
            dt.Columns.Add("InCode", typeof(string));
            dt.Columns.Add("CreatedBy", typeof(string));

            DataRow row = dt.NewRow();
            row["InMerk"] = txtAddMerk.Text.TrimEnd();
            row["InCode"] = Session["incode"].ToString();
            row["CreatedBy"] = ((Users)Session["Users"]).UserName;
            dt.Rows.Add(row);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            try
            {
                WebReference_Jmb.Service1 webServiceJombang = new WebReference_Jmb.Service1();
                string result = webServiceJombang.InsertItemMerk(dt);
                InsertToDb("itemmerk");
            }
            catch
            {
                DisplayAJAXMessage(this, "Koneksi Internet Error");
                return;
            }
        }
        #endregion

        protected void Button1_Click1(object sender, EventArgs e)
        {
            PostImproovement(0);
        }
        protected void RBListImprovement_CheckedChanged(object sender, EventArgs e)
        {
            //PanelServer.Visible = false;
            Label3.Visible = true;
            RBImprovement.Visible = true;
            RBApproval.Visible = true;
            Label4.Visible = false;
            RBCiteureup.Visible = false;
            RBKarawang.Visible = false;
            RBJombang.Visible = false;
            TextCariNama.Visible = false;
            btnSearch3.Visible = false;
            GridView1.Visible = true;
            GridBarang.Visible = false;
        }
        protected void RBListInventory_CheckedChanged(object sender, EventArgs e)
        {
            //PanelServer.Visible = true;
            if (RBListInventory.Checked == true)
            {
                Label3.Visible = false;
                RBImprovement.Visible = false;
                RBApproval.Visible = false;
                Label4.Visible = true;
                RBCiteureup.Visible = true;
                RBKarawang.Visible = true;
                RBJombang.Visible = true;
                TextCariNama.Visible = true;
                btnSearch3.Visible = true;
                GridView1.Visible = false;
                GridBarang.Visible = true;

                if (RBKarawang.Checked == true)
                    LoadBarangKarawang();
                if (RBCiteureup.Checked == true)
                    LoadBarangCiteureup();
                if (RBJombang.Checked == true)
                    LoadBarangJombang();

            }
        }

        protected void RBListAsset_CheckedChanged(object sender, EventArgs e)
        {
            if (RBListAsset.Checked == true)
            {
                Label3.Visible = false;
                RBImprovement.Visible = false;
                RBApproval.Visible = false;
                Label4.Visible = true;
                RBCiteureup.Visible = true;
                RBKarawang.Visible = true;
                TextCariNama.Visible = true;
                btnSearch3.Visible = true;
                GridView1.Visible = false;
                GridBarang.Visible = true;

                if (RBKarawang.Checked == true)
                    LoadBarangKarawang();
                if (RBCiteureup.Checked == true)
                    LoadBarangCiteureup();
                if (RBJombang.Checked == true)
                    LoadBarangJombang();
            }
        }





        #region Tampilkan Inventory Plant
        protected void LoadBarangKarawang()
        {
            ArrayList arrItemType = new ArrayList();
            DataSet arrItemTypeRemote = new DataSet();
            InventoryFacade InventoryFacade = new InventoryFacade();
            string itemtypeid = string.Empty;
            if (RBListInventory.Checked == true)
                itemtypeid = "1";
            else
                itemtypeid = "2";
            if (((Users)Session["Users"]).UnitKerjaID == 7 && RBKarawang.Checked == true)
            {
                arrItemType = InventoryFacade.RetrieveBarang(itemtypeid);
                GridBarang.DataSource = arrItemType;
                GridBarang.DataBind();
            }
            else
            {
                try
                {
                    WebReference_Krwg.Service1 webServiceKRW = new WebReference_Krwg.Service1();
                    arrItemTypeRemote = webServiceKRW.GetBarang(itemtypeid);
                }
                catch
                {
                    DisplayAJAXMessage(this, "Koneksi Internet Error");
                    return;
                }
                GridBarang.DataSource = arrItemTypeRemote;
                GridBarang.DataBind();
            }
        }
        protected void LoadBarangCiteureup()
        {
            ArrayList arrItemType = new ArrayList();
            DataSet arrItemTypeRemote = new DataSet();
            InventoryFacade InventoryFacade = new InventoryFacade();
            string itemtypeid = string.Empty;
            if (RBListInventory.Checked == true)
                itemtypeid = "1";
            else
                itemtypeid = "2";
            if (((Users)Session["Users"]).UnitKerjaID == 1 && RBCiteureup.Checked == true)
            {
                arrItemType = InventoryFacade.RetrieveBarang(itemtypeid);
                GridBarang.DataSource = arrItemType;
                GridBarang.DataBind();
            }
            else
            {
                try
                {
                    WebReference_Ctrp.Service1 WebReferenceCtrp = new WebReference_Ctrp.Service1();
                    arrItemTypeRemote = WebReferenceCtrp.GetBarang(itemtypeid);
                }
                catch
                {
                    DisplayAJAXMessage(this, "Koneksi Internet Error");
                    return;
                }
                GridBarang.DataSource = arrItemTypeRemote;
                GridBarang.DataBind();
            }
        }
        protected void LoadBarangJombang()
        {
            ArrayList arrItemType = new ArrayList();
            DataSet arrItemTypeRemote = new DataSet();
            InventoryFacade InventoryFacade = new InventoryFacade();
            string itemtypeid = string.Empty;
            if (RBListInventory.Checked == true)
                itemtypeid = "1";
            else
                itemtypeid = "2";
            if (((Users)Session["Users"]).UnitKerjaID == 13 && RBJombang.Checked == true)
            {
                arrItemType = InventoryFacade.RetrieveBarang(itemtypeid);
                GridBarang.DataSource = arrItemType;
                GridBarang.DataBind();
            }
            else
            {
                try
                {
                    WebReference_Jmb.Service1 WebReferenceJombang = new WebReference_Jmb.Service1();
                    arrItemTypeRemote = WebReferenceJombang.GetBarang(itemtypeid);
                }
                catch
                {
                    DisplayAJAXMessage(this, "Koneksi Internet Error");
                    return;
                }
                GridBarang.DataSource = arrItemTypeRemote;
                GridBarang.DataBind();
            }
        }
        #endregion

        #region Tampilkan Inventory Plant Berdasarkan Nama
        protected void LoadBarangKarawangByName(string nama)
        {
            ArrayList arrItemType = new ArrayList();
            DataSet arrItemTypeRemote = new DataSet();
            InventoryFacade InventoryFacade = new InventoryFacade();
            string itemtypeid = string.Empty;
            if (RBListInventory.Checked == true)
                itemtypeid = "1";
            else
                itemtypeid = "2";
            if (((Users)Session["Users"]).UnitKerjaID == 7 && RBKarawang.Checked == true)
            {
                arrItemType = InventoryFacade.RetrieveBarangByNama(itemtypeid, nama);
                GridBarang.DataSource = arrItemType;
                GridBarang.DataBind();
            }
            else
            {
                try
                {
                    WebReference_Krwg.Service1 webServiceKRW = new WebReference_Krwg.Service1();
                    arrItemTypeRemote = webServiceKRW.GetBarangByName(itemtypeid, nama);
                }
                catch
                {
                    DisplayAJAXMessage(this, "Koneksi Internet Error");
                    return;
                }
                GridBarang.DataSource = arrItemTypeRemote;
                GridBarang.DataBind();
            }
        }
        protected void LoadBarangCiteureupByName(string nama)
        {
            ArrayList arrItemType = new ArrayList();
            DataSet arrItemTypeRemote = new DataSet();
            InventoryFacade InventoryFacade = new InventoryFacade();
            string itemtypeid = string.Empty;
            if (RBListInventory.Checked == true)
                itemtypeid = "1";
            else
                itemtypeid = "2";
            if (((Users)Session["Users"]).UnitKerjaID == 1 && RBCiteureup.Checked == true)
            {
                arrItemType = InventoryFacade.RetrieveBarangByNama(itemtypeid, nama);
                GridBarang.DataSource = arrItemType;
                GridBarang.DataBind();
            }
            else
            {
                try
                {
                    WebReference_Ctrp.Service1 webServiceCtrp = new WebReference_Ctrp.Service1();
                    arrItemTypeRemote = webServiceCtrp.GetBarangByName(itemtypeid, nama);
                }
                catch
                {
                    DisplayAJAXMessage(this, "Koneksi Internet Error");
                    return;
                }
                GridBarang.DataSource = arrItemTypeRemote;
                GridBarang.DataBind();
            }
        }
        protected void LoadBarangJombangByName(string nama)
        {
            ArrayList arrItemType = new ArrayList();
            DataSet arrItemTypeRemote = new DataSet();
            InventoryFacade InventoryFacade = new InventoryFacade();
            string itemtypeid = string.Empty;
            if (RBListInventory.Checked == true)
                itemtypeid = "1";
            else
                itemtypeid = "2";
            if (((Users)Session["Users"]).UnitKerjaID == 13 && RBJombang.Checked == true)
            {
                arrItemType = InventoryFacade.RetrieveBarangByNama(itemtypeid, nama);
                GridBarang.DataSource = arrItemType;
                GridBarang.DataBind();
            }
            else
            {
                try
                {
                    WebReference_Jmb.Service1 webServiceJombang = new WebReference_Jmb.Service1();
                    arrItemTypeRemote = webServiceJombang.GetBarangByName(itemtypeid, nama);
                }
                catch
                {
                    DisplayAJAXMessage(this, "Koneksi Internet Error");
                    return;
                }
                GridBarang.DataSource = arrItemTypeRemote;
                GridBarang.DataBind();
            }
        }
        #endregion

        protected void RBKarawang_CheckedChanged(object sender, EventArgs e)
        {
            LoadBarangKarawang();
        }
        protected void RBCiteureup_CheckedChanged(object sender, EventArgs e)
        {
            if (RBCiteureup.Checked == true)
                LoadBarangCiteureup();
        }
        protected void RBJombang_CheckedChanged(object sender, EventArgs e)
        {
            if (RBJombang.Checked == true)
                LoadBarangJombang();
        }

        protected void btnSearch3_Click(object sender, ImageClickEventArgs e)
        {
            if (TextCariNama.Text.Trim() != string.Empty)

                if (RBKarawang.Checked == true)
                    LoadBarangKarawangByName(TextCariNama.Text.Trim());
                else if (RBCiteureup.Checked == true)
                    LoadBarangCiteureupByName(TextCariNama.Text.Trim());
                else
                    LoadBarangJombangByName(TextCariNama.Text.Trim());
        }

        protected void TextCariNama_TextChanged(object sender, EventArgs e)
        {
            /**
             * Seacrh nama barang on list inventory
             */
            if (TextCariNama.Text.Trim() != string.Empty)
            {
                if (RBKarawang.Checked == true)
                    LoadBarangKarawangByName(TextCariNama.Text.Trim());
                else if (RBCiteureup.Checked == true)
                    LoadBarangCiteureupByName(TextCariNama.Text.Trim());
                else
                    LoadBarangJombangByName(TextCariNama.Text.Trim());
            }

            TextCariNama.Text = string.Empty;
        }
        protected void RBImprovement_CheckedChanged(object sender, EventArgs e)
        {
            if (RBImprovement.Checked == true)
                RBUnApv.Checked = false; RBApproval.Checked = false; RBListImprovement.Checked = true;
            LoadDataGridImprovement(LoadGridImprovement());
        }
        protected void RBApproval_CheckedChanged(object sender, EventArgs e)
        {
            if (RBApproval.Checked == true)
                RBImprovement.Checked = false; RBUnApv.Checked = false; RBListImprovement.Checked = true;
            LoadDataGridImprovement(LoadGridImprovement());
        }
        protected void RBUnApv_CheckedChanged(object sender, EventArgs e)
        {
            if (RBUnApv.Checked == true)
                RBApproval.Checked = false; RBImprovement.Checked = false; RBListImprovement.Checked = true;
            LoadDataGridImprovement(LoadGridImprovement());
        }

        protected void ApproveALL(object sender, EventArgs e)
        {
            Improvement objImp = new Improvement();
            ImprovementFacade Imp = new ImprovementFacade();
            ArrayList arrAproveAll = (ArrayList)Session["ApproveAll"];

        }
        private void LoadApproval()
        {
            ArrayList arrImprovement = new ArrayList();
            ImprovementFacade improvementFacade = new ImprovementFacade();
            arrImprovement = improvementFacade.Retrieve2ByApproval(1, ((Users)Session["Users"]).Apv, ((Users)Session["Users"]).ID);
            if (arrImprovement.Count > 0)
            {
                Session["ApproveAll"] = arrImprovement;
            }
            else
            {
                arrImprovement.Add(new Improvement());
                Session["ApproveAll"] = arrImprovement;
            }
        }

        protected void Button1_Click2(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
        }
    }
}