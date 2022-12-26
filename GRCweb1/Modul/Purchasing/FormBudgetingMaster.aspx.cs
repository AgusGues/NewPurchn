using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Domain;
using BusinessFacade;
using DataAccessLayer;

namespace GRCweb1.Modul.Purchasing
{
    public partial class FormBudgetingMaster : System.Web.UI.Page
    {
        public string Kolom = "0";
        public string DeptCode = string.Empty;
        public string inDeptCode = string.Empty;
        public string Judul = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                string LinkFrom = (Request.QueryString["fr"] != null) ? Request.QueryString["fr"].ToString() : "";
                switch (LinkFrom) { case "2": Judul = "CONSUMABLE MATERIAL"; break; default: Judul = "ATK"; break; }
                GetTahun();
                Loadheader();
                string[] UpdateMaster = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("BudgetMasterUpdate", "BudgetATK").Split(',');
                btnSimpan.Visible = (UpdateMaster.Contains(((Users)Session["Users"]).DeptID.ToString())) ? true : false;
                addMaster.Visible = (UpdateMaster.Contains(((Users)Session["Users"]).DeptID.ToString())) ? false : false;
                addMat.Visible = false;
                addMat1.Visible = false;
                ddlSmt.Visible = (LinkFrom == "2") ? true : false;
                btnSimpan.Visible = (LinkFrom == "2") ? false : false;// btnSimpan.Visible;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Al", "divWidth()", true);
                jdl.Text = Judul;
            }
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Al", "divWidth()", true);
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }
        protected void addMaster_Click(object sender, EventArgs e)
        {
            addMat.Visible = true;
            addMat1.Visible = true;
            Loadheader();
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            tbl.Controls.Clear();
            LoadItemATK(true);
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=MasterBudget.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<b>MASTER BUDGET " + Judul + "</b>";
            Html += "<br>Periode : " + ddlTahun.SelectedValue.ToString();
            string HtmlEnd = "";
            //lstForPrint.RenderControl(hw);
            tbl.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0", "border=\"1");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }
        private void GetTahun()
        {
            BudgetingFacade bg = new BudgetingFacade();
            bg.Pilihan = "Tahun";
            bg.Prefix = " Top 3 ";
            ArrayList arrData = bg.Retrieve();
            ddlTahun.Items.Clear();
            ddlTahun.Items.Add(new ListItem((DateTime.Now.Year + 1).ToString(), (DateTime.Now.Year + 1).ToString()));
            //if (!arrData.Contains( DateTime.Now.Year)) 
            foreach (Budget b in arrData)
            {
                if (b.Tahun < DateTime.Now.Year)
                {
                    ddlTahun.Items.Add(new ListItem(b.Tahun.ToString(), b.Tahun.ToString()));
                }
                else
                {
                    ddlTahun.Items.Add(new ListItem((DateTime.Now.Year).ToString(), (DateTime.Now.Year).ToString()));
                }
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            tbl.Controls.Clear();
            string LinkFrom = (Request.QueryString["fr"] != null) ? Request.QueryString["fr"].ToString() : "";
            if (LinkFrom == "2")
            {
                Loadheader();
                LoadItemATK(5, true);
            }
            else
            {
                Loadheader();
                LoadItemATK();
            }
        }
        private void LoadItemBudget()
        {
            tbl.Controls.Clear();
            string LinkFrom = (Request.QueryString["fr"] != null) ? Request.QueryString["fr"].ToString() : "";
            if (LinkFrom == "2")
            {
                Loadheader();
                LoadItemATK(5, true);
            }
            else
            {
                Loadheader();
                LoadItemATK();
            }
        }
        protected void btnCari_Click(object sender, EventArgs e)
        {
            btnPreview_Click(null, null);
        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            SimpanData();

        }
        private void Loadheader()
        {

            ArrayList arrDept = new ArrayList();
            DeptFacade dept = new DeptFacade();
            dept.Criteria = " and ID not in(1,25,27)";
            arrDept = dept.Retrieve();
            Kolom = arrDept.Count.ToString();
            tbl.Controls.Clear();
            TableRow thd = new TableRow();
            thd.CssClass = "tbHeader";
            TableCell th1 = new TableCell();
            th1.Text = "No";
            th1.Width = Unit.Pixel(50);
            th1.CssClass = "kotak tengah bold";
            th1.RowSpan = 2;
            thd.Cells.Add(th1);
            TableCell th2 = new TableCell();
            th2.Text = "ItemCode";
            th2.Width = Unit.Pixel(220);
            th2.CssClass = "kotak tengah bold";
            th2.RowSpan = 2;
            thd.Cells.Add(th2);
            TableCell th3 = new TableCell();
            th3.Text = "ItemName";
            th3.Width = Unit.Pixel(600);
            th3.CssClass = "kotak tengah bold";
            th3.RowSpan = 2;
            thd.Cells.Add(th3);
            TableCell th4 = new TableCell();
            th4.Text = "Unit";
            th4.Width = Unit.Pixel(50);
            th4.CssClass = "kotak tengah bold";
            th4.RowSpan = 2;
            thd.Cells.Add(th4);
            TableCell th5 = new TableCell();
            th5.Text = "Department Code";
            //th5.Width = Unit.Pixel(30);
            th5.CssClass = "kotak tengah bold";
            th5.ColumnSpan = arrDept.Count;
            thd.Cells.Add(th5);
            TableCell th6 = new TableCell();
            th6.Text = "Total";
            th6.Width = Unit.Pixel(60);
            th6.CssClass = "kotak tengah bold";
            th6.RowSpan = 2;
            thd.Cells.Add(th6);
            tbl.Controls.Add(thd);
            TableRow tr = new TableRow();
            tr.CssClass = "Line3";
            foreach (Dept dep in arrDept)
            {
                /*TextBox tb = new TextBox();
                DeptCode += "<th class='kotak' style='width:100px' title='" + dep.DeptName + "'>" + dep.DeptCode + "</th>";
                inDeptCode += "<td class='kotak'><asp:TextBox ID='txt" + dep.DeptCode + "' runat='server' width='100%' BorderStyle='None' BackColor='Transparent'></asp:TextBox></td>";
                */
                TableCell th = new TableCell();
                th.Width = Unit.Pixel(50);
                th.Text = dep.DeptCode.ToString();
                th.ToolTip = dep.DeptName.ToString();
                th.CssClass = "kotak tengah cursor bold";

                tr.Cells.Add(th);

            }
            tbl.Controls.Add(tr);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Al", "_showAddMaster()", true);

        }
        protected void txtFind_Change(object sender, EventArgs e)
        {
            Loadheader();
            InventoryFacade inf = new InventoryFacade();
            string LinkFrom = (Request.QueryString["fr"] != null) ? Request.QueryString["fr"].ToString() : "";
            switch (LinkFrom)
            {
                case "2":
                    inf.Criteriane = " and GroupID in(8,9) and Head !=5 and aktif=1 and ItemName like '%" + txtFind.Text + "%'";
                    break;
                default:
                    inf.Criteriane = " and GroupID in(3) and Head not in(1,2,5) and aktif=1 and ItemName like '%" + txtFind.Text + "%'";
                    break;
            }
            ArrayList arrData = inf.Retrieve();
            ddlMaterial.Items.Clear();
            ddlMaterial.Items.Add(new ListItem("--Pilih Material--", "0"));
            foreach (Inventory inv in arrData)
            {
                ddlMaterial.Items.Add(new ListItem(inv.ItemName, inv.ID.ToString()));
            }
        }
        protected void addToMaster_Click(object sender, EventArgs e)
        {
            Budget bud = new Budget();
            BudgetingFacade bgd = new BudgetingFacade();
            string LinkFrom = (Request.QueryString["fr"] != null) ? Request.QueryString["fr"].ToString() : "";
            bud.ItemID = int.Parse(ddlMaterial.SelectedValue.ToString());
            bud.Quantity = (LinkFrom == "2") ? 5 : 1;
            int result = bgd.AddMaterialToMasterBudget(bud);
            if (result > 0)
            {
                Loadheader();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "Al", "_hideAddMaster()", true);
                switch (LinkFrom) { case "2": LoadItemATK(5, true); break; default: LoadItemATK(); break; }
                addMat.Visible = false;
                addMat1.Visible = false;
            }
        }
        private void LoadItemATK(int GroupID, bool SP)
        {
            ArrayList arrData = new ArrayList();
            InventoryFacade inv = new InventoryFacade();
            inv.Limit = " ";
            inv.Criteriane = " and Head in(" + GroupID + ")";
            inv.Criteriane += (txtCari.Text.Substring(0, 4) != "Find") ? " and Itemname like '%" + txtCari.Text + "%'" : "";
            arrData = inv.RetrieveByGroupID("8,9", 1, true);
            int n = 0;
            foreach (Inventory v in arrData)
            {
                n = n + 1;
                TableRow tr = new TableRow();
                tr.CssClass = (n % 2 == 0) ? "EvenRows baris" : "OddRows baris";
                TableCell td = new TableCell();
                td.Text = n.ToString();
                td.CssClass = "kotak tengah";
                tr.Cells.Add(td);
                TableCell itmCode = new TableCell();
                itmCode.Text = v.ItemCode.ToString();
                itmCode.CssClass = "kotak tengah";
                tr.Cells.Add(itmCode);
                TableCell Name = new TableCell();
                Name.Width = Unit.Pixel(700);
                Name.Text = v.ItemName;
                Name.CssClass = "kotak";
                Name.Attributes.Add("style", "overflow:hidden");
                Name.ToolTip = v.ItemName;
                Name.Wrap = true;
                //Name.Height = Unit.Pixel(24);
                Name.VerticalAlign = VerticalAlign.Middle;
                tr.Cells.Add(Name);
                TableCell Satuan = new TableCell();
                Satuan.Text = v.UomCode;
                Satuan.CssClass = "kotak tengah";
                tr.Cells.Add(Satuan);
                //textbox
                ArrayList arrDept = new ArrayList();
                DeptFacade dept = new DeptFacade();
                dept.Criteria = " and ID not in(1,25,27)";
                arrDept = dept.Retrieve();
                decimal TotQty = 0;
                foreach (Dept dep in arrDept)
                {
                    /**
                     * Baca Data Master
                     */
                    BudgetingFacade bg = new BudgetingFacade();
                    Budget bds = new Budget();
                    bg.Pilihan = "MasterBudgetSP";
                    bg.Criteria = " and DeptID=" + dep.ID;
                    bg.Criteria += " and ItemID=" + v.ID.ToString();
                    bg.Criteria += " and Tahun=" + ddlTahun.SelectedValue;
                    //bg.Criteria += " and Bulan=" + ddlSmt.SelectedValue;
                    bds = bg.Retrieve(true);
                    TotQty += bds.Quantity;
                    TableCell dko = new TableCell();
                    TextBox txt = new TextBox();
                    dko.CssClass = "kotak angka";
                    //txt.Text = (bds.Quantity == 0) ? string.Empty : bds.Quantity.ToString("##0");
                    //txt.ID = "txt-" + dep.ID.ToString() + "-" + v.ID.ToString();
                    //txt.CssClass = "tengah cursor";
                    //txt.Width = Unit.Pixel(30);
                    //txt.BorderStyle = BorderStyle.None;
                    //txt.BackColor = System.Drawing.Color.Transparent;
                    //txt.ToolTip = dep.DeptName.ToString();
                    //txt.Attributes.Add("onfocus", "this.select()");
                    //txt.Attributes.Add("runat", "server");
                    dko.Text = (bds.Quantity == 0) ? string.Empty : bds.Quantity.ToString("###,##0");
                    dko.ToolTip = dep.DeptName.ToString();
                    tr.Cells.Add(dko);
                }
                TableCell total = new TableCell();
                total.CssClass = "kotak tengah";
                total.Text = TotQty.ToString("###,##0");
                tr.Cells.Add(total);
                tbl.Controls.Add(tr);
                tbl.EnableViewState = true;
                ViewState["tbl"] = true;
            }
            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "A", "divWidth()", true);

        }
        private void LoadItemATK()
        {
            Loadheader();
            ArrayList arrData = new ArrayList();
            InventoryFacade inv = new InventoryFacade();
            inv.Limit = " ";
            inv.Criteriane = " and Head in(1,2)";
            inv.Criteriane += (txtCari.Text.Substring(0, 4) != "Find") ? " and Itemname like '%" + txtCari.Text + "%'" : "";
            arrData = inv.RetrieveByGroupID("3", 1);
            //lstMaster.DataSource = arrData;
            //lstMaster.DataBind();

            int n = 0;
            foreach (Inventory v in arrData)
            {
                n = n + 1;
                TableRow tr = new TableRow();
                tr.CssClass = (n % 2 == 0) ? "EvenRows baris" : "OddRows baris";
                TableCell td = new TableCell();
                td.Text = n.ToString();
                td.CssClass = "kotak tengah";
                tr.Cells.Add(td);
                TableCell itmCode = new TableCell();
                itmCode.Text = v.ItemCode.ToString();
                itmCode.CssClass = "kotak tengah";
                tr.Cells.Add(itmCode);
                TableCell Name = new TableCell();
                Name.Text = v.ItemName;
                Name.CssClass = "kotak";
                Name.Wrap = false;
                Name.Attributes.Add("style", "overflow:hidden");
                Name.ToolTip = v.ItemName;
                tr.Cells.Add(Name);
                TableCell Satuan = new TableCell();
                Satuan.Text = v.UomCode;
                Satuan.CssClass = "kotak tengah";
                tr.Cells.Add(Satuan);
                //textbox
                ArrayList arrDept = new ArrayList();
                DeptFacade dept = new DeptFacade();
                dept.Criteria = " and ID not in(1,25,27)";
                arrDept = dept.Retrieve();
                decimal TotQty = 0;
                foreach (Dept dep in arrDept)
                {
                    /**
                     * Baca Data Master
                     */

                    BudgetingFacade bg = new BudgetingFacade();
                    Budget bds = new Budget();
                    bg.Pilihan = "MasterBudget";
                    bg.Criteria = " and DeptID=" + dep.ID;
                    bg.Criteria += " and ItemID=" + v.ID.ToString();
                    bg.Criteria += " and Tahun=" + ddlTahun.SelectedValue;
                    bds = bg.Retrieve(true);
                    TableCell dko = new TableCell();
                    TextBox txt = new TextBox();
                    dko.CssClass = "kotak tengah";
                    if (bds.ID > 0)
                    {
                        TotQty += bds.Quantity;


                        txt.ID = "txt_" + bds.ID.ToString();


                    }
                    txt.Text = (bds.Quantity == 0) ? string.Empty : bds.Quantity.ToString("##0");
                    //txt.ID = "txt-" + dep.ID.ToString() + "-" + v.ID.ToString();
                    txt.CssClass = "tengah cursor";
                    txt.Width = Unit.Pixel(30);
                    txt.BorderStyle = BorderStyle.None;
                    txt.BackColor = System.Drawing.Color.Transparent;
                    txt.ToolTip = dep.DeptName.ToString();
                    txt.AutoPostBack = false;
                    txt.Attributes.Add("onfocus", "this.select()");
                    txt.Attributes.Add("runat", "server");
                    txt.Attributes.Add("onkeypress", "simpandata('" + bds.ID + "',event);");
                    dko.Controls.Add(txt);
                    tr.Cells.Add(dko);
                }
                TableCell total = new TableCell();
                total.CssClass = "kotak tengah";
                total.Text = TotQty.ToString("###");
                tr.Cells.Add(total);
                tbl.Controls.Add(tr);
                tbl.EnableViewState = true;
                ViewState["tbl"] = true;
            }
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "A", "divWidth()", true);
        }
        private void LoadItemATK(bool print)
        {
            Loadheader();
            string LinkFrom = (Request.QueryString["fr"] != null) ? Request.QueryString["fr"].ToString() : "";
            ArrayList arrData = new ArrayList();
            InventoryFacade inv = new InventoryFacade();
            inv.Limit = " ";
            switch (LinkFrom)
            {
                case "2":
                    inv.Criteriane = " and Head in(5)";
                    arrData = inv.RetrieveByGroupID("9,8", 1);
                    break;
                default:
                    inv.Criteriane = " and Head in(1,2)";
                    arrData = inv.RetrieveByGroupID("3", 1);
                    break;
            }
            //lstMaster.DataSource = arrData;
            //lstMaster.DataBind();

            int n = 0;
            foreach (Inventory v in arrData)
            {
                n = n + 1;
                TableRow tr = new TableRow();
                tr.CssClass = (n % 2 == 0) ? "EvenRows baris" : "OddRows baris";
                TableCell td = new TableCell();
                td.Text = n.ToString();
                td.CssClass = "kotak tengah";
                tr.Cells.Add(td);
                TableCell itmCode = new TableCell();
                itmCode.Text = "'" + v.ItemCode.ToString();
                itmCode.CssClass = "kotak tengah";
                tr.Cells.Add(itmCode);
                TableCell Name = new TableCell();
                Name.Text = v.ItemName;
                Name.CssClass = "kotak";
                Name.Wrap = false;
                Name.Attributes.Add("style", "overflow:hidden");
                Name.ToolTip = v.ItemName;
                tr.Cells.Add(Name);
                TableCell Satuan = new TableCell();
                Satuan.Text = v.UomCode;
                Satuan.CssClass = "kotak tengah";
                tr.Cells.Add(Satuan);
                //textbox
                ArrayList arrDept = new ArrayList();
                DeptFacade dept = new DeptFacade();
                dept.Criteria = " and ID not in(1,25,27)";
                arrDept = dept.Retrieve();
                decimal TotQty = 0;
                foreach (Dept dep in arrDept)
                {
                    /**
                     * Baca Data Master
                     */
                    BudgetingFacade bg = new BudgetingFacade();
                    Budget bds = new Budget();
                    switch (LinkFrom)
                    {

                        case "2":
                            bg.Pilihan = "MasterBudgetSP";
                            bg.Criteria = " and DeptID=" + dep.ID;
                            bg.Criteria += " and ItemID=" + v.ID.ToString();
                            bg.Criteria += " and Tahun=" + ddlTahun.SelectedValue;
                            bg.Criteria += " and Bulan=" + ddlSmt.SelectedValue;
                            bg.Criteria += " and ItemTypeID=1";
                            break;
                        default:
                            bg.Pilihan = "MasterBudget";
                            bg.Criteria = " and DeptID=" + dep.ID;
                            bg.Criteria += " and ItemID=" + v.ID.ToString();
                            bg.Criteria += " and Tahun=" + ddlTahun.SelectedValue;
                            break;
                    }
                    bds = bg.Retrieve(true);
                    TotQty += bds.Quantity;
                    TableCell dko = new TableCell();
                    TextBox txt = new TextBox();
                    dko.CssClass = "kotak tengah";
                    dko.Text = (bds.Quantity == 0) ? string.Empty : bds.Quantity.ToString("##0");
                    tr.Cells.Add(dko);
                }
                TableCell total = new TableCell();
                total.CssClass = "kotak tengah";
                total.Text = TotQty.ToString("###");
                tr.Cells.Add(total);
                tbl.Controls.Add(tr);
                tbl.EnableViewState = true;
                ViewState["tbl"] = true;
            }
        }
        protected void lstMaster_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            TextBox ID = (TextBox)e.Item.FindControl("txtItemID");
            TextBox txt = (TextBox)e.Item.FindControl("txt");
            ArrayList arrDept = new ArrayList();
            DeptFacade dept = new DeptFacade();
            foreach (Dept dep in arrDept)
            {
                txt.Attributes.Add("ID", dep.ID.ToString() + "-" + ID.Text);
            }

        }
        protected override object SaveViewState()
        {
            object[] newViewState = new object[2];
            List<string> txtValues = new List<string>();

            //foreach (Table tb in tbl.Controls)
            //{
            foreach (TableRow row in tbl.Controls)
            {
                foreach (TableCell cell in row.Controls)
                {
                    if (cell.Controls.Count > 0)
                    {
                        if (cell.Controls[0] is TextBox)
                        {
                            txtValues.Add(((TextBox)cell.Controls[0]).Text);
                        }
                    }
                }
            }
            //}

            newViewState[0] = txtValues.ToArray();
            newViewState[1] = base.SaveViewState();
            return newViewState;
        }
        protected override void LoadViewState(object savedState)
        {
            if (savedState is object[] && ((object[])savedState).Length == 2 && ((object[])savedState)[0] is string[])
            {
                object[] newViewState = (object[])savedState;
                string[] txtValues = (string[])(newViewState[0]);
                if (txtValues.Length > 0)
                {
                    string LinkFrom = (Request.QueryString["fr"] != null) ? Request.QueryString["fr"].ToString() : "";
                    if (LinkFrom == "2")
                    {
                        Loadheader();
                        LoadItemATK(5, true);
                    }
                    else
                    {
                        Loadheader();
                        LoadItemATK();
                    }
                    int i = 0;
                    foreach (TableRow row in tbl.Controls)
                    {
                        foreach (TableCell cell in row.Controls)
                        {
                            if (cell.Controls.Count > 0)
                            {
                                if (cell.Controls[0] is TextBox && i < txtValues.Length)
                                {
                                    ((TextBox)cell.Controls[0]).Text = txtValues[i++].ToString();

                                }
                            }
                        }
                    }
                }
                base.LoadViewState(newViewState[1]);
            }
            else
            {
                base.LoadViewState(savedState);
            }

        }
        protected void lstMaster_Databound(object sender, RepeaterItemEventArgs e)
        {

            Inventory inve = (Inventory)e.Item.DataItem;
            ArrayList arrDept = new ArrayList();
            DeptFacade dept = new DeptFacade();
            dept.Criteria = " and ID not in(1,25,27)";
            arrDept = dept.Retrieve();
            decimal n = 0;

            //var txt = lstMaster.Items[0].FindControl("txt") as TextBox;
            foreach (Dept dep in arrDept)
            {
                if (e.Item.ItemType == ListItemType.Header)
                {
                    Panel pnlH = (Panel)e.Item.FindControl("pnlH");
                    TableHeaderCell th = new TableHeaderCell();
                    th.Text = dep.DeptCode.ToString();
                    th.CssClass = "kotak";
                    th.ToolTip = dep.DeptName;
                    th.CssClass = "cursor";
                    pnlH.Controls.Add(th);
                }
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    TextBox ID = (TextBox)e.Item.FindControl("txtItemID");
                    Panel pnl = (Panel)e.Item.FindControl("pnl");
                    TextBox txt = (TextBox)e.Item.FindControl("txt");
                    Label total = (Label)e.Item.FindControl("lbltotat");
                    TextBox tex = new TextBox();
                    TableCell tc = new TableCell();
                    //Table tb = new Table();
                    tc.CssClass = "kotak";
                    tc.ID = "tcl_" + dep.ID.ToString();
                    BudgetingFacade bg = new BudgetingFacade();
                    Budget bds = new Budget();
                    string LinkFrom = (Request.QueryString["fr"] != null) ? Request.QueryString["fr"].ToString() : "";
                    bg.Pilihan = (LinkFrom == "2") ? "MasterBudgetSP" : "MasterBudget";
                    bg.Criteria = " and DeptID=" + dep.ID;
                    bg.Criteria += " and ItemID=" + ID.Text;
                    bg.Criteria += " and Tahun=" + ddlTahun.SelectedValue;
                    bg.Criteria += (LinkFrom == "2") ? " and Bulan=" + ddlSmt.SelectedValue : "";
                    bds = bg.Retrieve(true);
                    tex.Text = bds.Quantity.ToString("###");
                    tex.ID = "txt_" + dep.ID.ToString();
                    tex.ToolTip = dep.ID.ToString();
                    tex.Width = Unit.Pixel(30);
                    tex.BorderStyle = BorderStyle.None;
                    tex.BackColor = System.Drawing.Color.Transparent;
                    tex.CssClass = "tengah";
                    tc.Controls.Add(tex);
                    pnl.Controls.Add(tc);
                    n += bds.Quantity;
                    txt.ToolTip = dep.DeptName.ToString();
                    total.Text = n.ToString("###");
                }

            }
        }
        private void SimpanData()
        {
            ArrayList arrData = new ArrayList();
            foreach (TableRow tr in tbl.Controls)
            {
                foreach (TableCell tc in tr.Controls)
                {
                    if (tc.Controls.Count > 0)
                    {
                        if (tc.Controls[0] is TextBox)
                        {
                            TextBox txt = (TextBox)tc.Controls[0];
                            string[] dpt = txt.ID.Split('-');
                            Budget db = new Budget();
                            db.Quantity = (txt.Text == "") ? 0 : decimal.Parse(txt.Text);
                            db.DeptID = int.Parse(dpt[1].ToString());
                            db.ItemID = int.Parse(dpt[2].ToString());
                            db.Tahun = int.Parse(ddlTahun.SelectedValue);
                            db.Bulan = int.Parse(ddlSmt.SelectedValue);
                            db.ItemTypeID = 1;
                            arrData.Add(db);
                        }
                    }
                }
            }
            int res = 0;
            string linkFromn = (Request.QueryString["fr"] != null) ? Request.QueryString["fr"].ToString() : "";
            switch (linkFromn)
            {
                case "2":
                    res = ProcessData(arrData, true);
                    break;
                default:
                    res = ProcessData(arrData);
                    break;
            }
            if (res > 0)
            {
                this.btnPreview_Click(null, null);
            }
        }
        private int ProcessData(ArrayList arrData, bool SP)
        {
            int result = 0;
            BudgetingFacade bdf = new BudgetingFacade();
            if (arrData.Count > 0)
            {
                foreach (Budget bg in arrData)
                {
                    Budget bd = new Budget();
                    Budget bu = new Budget();
                    bdf.Pilihan = "MasterBudgetSP";
                    bdf.Criteria = " and ItemID=" + bg.ItemID;
                    bdf.Criteria += " and Tahun=" + bg.Tahun;
                    bdf.Criteria += " and DeptID=" + bg.DeptID;
                    bdf.Criteria += " and Bulan=" + bg.Bulan;
                    bdf.Criteria += " and ItemTypeID=" + bg.ItemTypeID;
                    bu = bdf.Retrieve(true);
                    if (bu.ID > 0)
                    {
                        bd.ID = bu.ID;
                        bd.MaxQty = bg.Quantity;
                        bd.RowStatus = 0;
                    }
                    else
                    {
                        bd.Tahun = bg.Tahun;
                        bd.ItemID = bg.ItemID;
                        bd.MaxQty = bg.Quantity;
                        bd.DeptID = bg.DeptID;
                        bd.ItemTypeID = bg.ItemTypeID;
                        bd.Bulan = bg.Bulan;
                    }
                    int res = (bu.ID > 0) ? bdf.UpdateMasterSP(bd) : bdf.InsertMasterSP(bd);
                    result += res;
                }
            }
            return result;
        }
        private int ProcessData(ArrayList arrData)
        {
            int result = 0;
            BudgetingFacade bdf = new BudgetingFacade();
            if (arrData.Count > 0)
            {
                foreach (Budget bg in arrData)
                {
                    Budget bd = new Budget();
                    Budget bu = new Budget();
                    bdf.Pilihan = "MasterBudget";
                    bdf.Criteria = " and ItemID=" + bg.ItemID;
                    bdf.Criteria += " and Tahun=" + bg.Tahun;
                    bdf.Criteria += " and DeptID=" + bg.DeptID;
                    bu = bdf.Retrieve(true);
                    if (bu.ID > 0)
                    {
                        bd.ID = bu.ID;
                        bd.Quantity = bg.Quantity;
                        bd.RowStatus = 0;
                    }
                    else
                    {
                        bd.Tahun = bg.Tahun;
                        bd.ItemID = bg.ItemID;
                        bd.Quantity = bg.Quantity;
                        bd.DeptID = bg.DeptID;
                    }
                    //cek apakah sudah tersimpan apa belum itemid dan tahun
                    int res = (bu.ID > 0) ? bdf.UpdateMaster(bd) : bdf.InsertMaster(bd);
                    result += res;
                }
            }
            return result;
        }
    }
}