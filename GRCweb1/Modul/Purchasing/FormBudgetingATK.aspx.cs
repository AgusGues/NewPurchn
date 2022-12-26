using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using BusinessFacade;
using Domain;

namespace GRCweb1.Modul.Purchasing
{
    public partial class FormBudgetingATK : System.Web.UI.Page
    {
        public string BulanPakai;
        public string Kolom = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string MaxTglInput = new Inifiles(Server.MapPath("~/App_Data/PopurchnConfig.ini")).Read("MaxDateInput", "BudgetATK");
                string MaxTglApvHrd = new Inifiles(Server.MapPath("~/App_Data/PopurchnConfig.ini")).Read("MaxDateAppHRD", "BudgetATK");
                Session["ListAtk"] = null;
                string onlyHrd = (((Users)Session["Users"]).DeptID == 7) ? "" : " and Head=1";
                act.ContextKey = onlyHrd;
                Global.link = "~/Default.aspx";
                GetBulan();
                GetTahun();
                LoadDept();
                GetHeaderHistory();
                btnAdditem.Enabled = true;
            }

        }
        private void GetBulan()
        {
            ddlBulan.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i).ToString(), i.ToString()));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
        }
        private void GetTahun()
        {
            BudgetingFacade bg = new BudgetingFacade();
            bg.Pilihan = "Tahun";
            bg.Prefix = " Top 3 ";
            ArrayList arrData = bg.Retrieve();
            ddlTahun.Items.Clear();
            ddlTahun.Items.Add(new ListItem((DateTime.Now.Year + 1).ToString(), (DateTime.Now.Year + 1).ToString()));
            ddlTahun.Items.Add(new ListItem((DateTime.Now.Year).ToString(), (DateTime.Now.Year).ToString()));
            foreach (Budget b in arrData)
            {
                ddlTahun.Items.Add(new ListItem(b.Tahun.ToString(), b.Tahun.ToString()));
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }
        private void GetHeaderHistory()
        {
            BulanPakai = string.Empty;
            string PrevHist = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ReadHistory", "SPB").ToString();
            int MulaiBulan = int.Parse(ddlBulan.SelectedValue) - int.Parse(PrevHist);
            Kolom = (int.Parse(PrevHist) * 2).ToString();
            int TahunLalu = (MulaiBulan <= 0) ? 12 + MulaiBulan : 0;
            MulaiBulan = (MulaiBulan <= 0) ? 1 : MulaiBulan;
            if (TahunLalu > 0)
            {
                for (int n = TahunLalu; n <= 12; n++)
                {
                    BulanPakai += "<td class='kotak tengah' colspan='2'>" + Global.nBulan(n, true) + " " + (int.Parse(ddlTahun.SelectedValue) - 1).ToString() + "</td>";
                }
            }
            for (int i = MulaiBulan; i < int.Parse(ddlBulan.SelectedValue); i++)
            {
                BulanPakai += "<td class='kotak tengah' colspan='2'>" + Global.nBulan(i, true) + " " + ddlTahun.SelectedValue + "</td>";
            }
            BulanPakai += "<tr class='tbHeader'>";
            for (int i = 1; i <= int.Parse(PrevHist); i++)
            {
                BulanPakai += "<td class='kotak tengah' style='width:10%'>Budget</td>";
                BulanPakai += "<td class='kotak tengah' style='width:10%'>Actual</td>";
            }
            BulanPakai += "</tr>";
            if (txtItemID.Text != string.Empty)
            {
                if (int.Parse(txtItemID.Text) > 0)
                {
                    BulanPakai += "<tr class='EventRows baris'>";
                    if (TahunLalu > 0)
                    {
                        for (int n = TahunLalu; n <= 12; n++)
                        {
                            BulanPakai += "<td class='kotak tengah' style='width:10%'>" + GetHistoryBgd((int.Parse(ddlTahun.SelectedValue) - 1).ToString(), n.ToString()) + "</td>";

                            BulanPakai += "<td class='kotak tengah' style='width:10%'>" + GetHistory((int.Parse(ddlTahun.SelectedValue) - 1).ToString(), n.ToString()) + "</td>";
                        }
                    }
                    for (int i = MulaiBulan; i < int.Parse(ddlBulan.SelectedValue); i++)
                    {
                        BulanPakai += "<td class='kotak tengah' style='width:10%'>" + GetHistoryBgd(ddlTahun.SelectedValue, i.ToString()) + "</td>";
                        BulanPakai += "<td class='kotak tengah' style='width:10%'>" + GetHistory(ddlTahun.SelectedValue, i.ToString()) + "</td>";
                    }
                    BulanPakai += "</tr>";
                }
            }
        }
        private decimal GetHistory(string Tahun, string Bulan)
        {
            decimal result = 0;
            ArrayList arrData = new ArrayList();
            BudgetingFacade bg = new BudgetingFacade();
            bg.Pilihan = "History";
            bg.Criteria = " and pk.DeptID=" + ddlDept.SelectedValue.ToString();
            bg.Criteria += " and pd.ItemID=" + txtItemID.Text;
            bg.Criteria += " and Year(pk.PakaiDate)=" + Tahun;
            Budget bu = bg.Retrieve(true);
            switch (Global.nBulan(int.Parse(Bulan), true).ToString())
            {
                case "Jan": result = bu.Jan; break;
                case "Feb": result = bu.Feb; break;
                case "Mar": result = bu.Mar; break;
                case "Apr": result = bu.Apr; break;
                case "Mei": result = bu.Mei; break;
                case "Jun": result = bu.Jun; break;
                case "Jul": result = bu.Jul; break;
                case "Agu": result = bu.Ags; break;
                case "Sep": result = bu.Sep; break;
                case "Okt": result = bu.Okt; break;
                case "Nov": result = bu.Nov; break;
                case "Des": result = bu.Des; break;

            }
            return result;
        }

        private decimal GetHistoryBgd(string Tahun, string Bulan)
        {
            decimal result = 0;
            ArrayList arrData = new ArrayList();
            BudgetingFacade bg = new BudgetingFacade();
            bg.Pilihan = "HistoryBudget";
            bg.Criteria = " and b.DeptID=" + ddlDept.SelectedValue.ToString();
            bg.Criteria += " and bd.ItemID=" + txtItemID.Text;
            bg.Criteria += " and Tahun=" + Tahun;
            Budget bu = bg.Retrieve(true);
            switch (Global.nBulan(int.Parse(Bulan), true).ToString())
            {
                case "Jan": result = bu.Jan; break;
                case "Feb": result = bu.Feb; break;
                case "Mar": result = bu.Mar; break;
                case "Apr": result = bu.Apr; break;
                case "Mei": result = bu.Mei; break;
                case "Jun": result = bu.Jun; break;
                case "Jul": result = bu.Jul; break;
                case "Agu": result = bu.Ags; break;
                case "Sep": result = bu.Sep; break;
                case "Okt": result = bu.Okt; break;
                case "Nov": result = bu.Nov; break;
                case "Des": result = bu.Des; break;

            }
            return result;
        }



        protected void ddlBulan_Change(object sender, EventArgs e)
        {
            GetHeaderHistory();
        }
        protected void ddlTahun_Change(object sender, EventArgs e)
        {

        }
        private void LoadDept()
        {
            ArrayList arrData = new ArrayList();
            arrData = new DeptFacade().Retrieve();
            ddlDept.Items.Clear();
            ddlDept.Items.Add(new ListItem("--Pilih Dept--", "0"));
            foreach (Dept dp in arrData)
            {
                ddlDept.Items.Add(new ListItem(dp.DeptName, dp.ID.ToString()));
            }

            ddlDept.SelectedValue = ((Users)Session["Users"]).DeptID.ToString();
        }
        protected void txtMaterial_Change(object sender, EventArgs e)
        {
            try
            {
                string[] ItemCode = txtMaterial.Text.Split('-');
                BudgetingFacade bg = new BudgetingFacade();
                bg.Pilihan = "Material";
                bg.Criteria = " and ItemCode='" + ItemCode[0] + "'";
                Budget bd = bg.Retrieve(true);
                txtUnit.Text = bd.UomCode.ToString();
                txtItemID.Text = bd.ID.ToString();
                HeadID();
                GetHeaderHistory();
                txtQty.Focus();
                string[] item = txtMaterial.Text.Split('-');
                lblItemName.Text = item[1];
            }
            catch { }
        }

        private void cek_budget()
        {
            decimal BulanPakai = 0;
            string PrevHist = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ReadHistory", "SPB").ToString();
            int MulaiBulan = int.Parse(ddlBulan.SelectedValue) - int.Parse(PrevHist);

            //tambahan 13-12-2021
            //Kolom = (int.Parse(PrevHist)*2).ToString();
            //int TahunLalu = (MulaiBulan <= 0) ? 12 + MulaiBulan : 0;

            MulaiBulan = (MulaiBulan <= 0) ? 1 : MulaiBulan;

            //tambahan 13-12-2021

            decimal MasterBudget = this.CheckMasterBudget();
            decimal RuleCalc = this.CheckRuleCalc();

            if (RuleCalc == 6)
            {
                for (int i = MulaiBulan; i < int.Parse(ddlBulan.SelectedValue); i++)
                {
                    BulanPakai += GetHistoryBgd(ddlTahun.SelectedValue, i.ToString());
                }


                if (BulanPakai == MasterBudget)
                {
                    DisplayAJAXMessage(this, "Penambahan Budget Tidak Boleh Melebihi Master Budget = " + MasterBudget.ToString("##0"));
                    txtQty.Text = "";
                    btnAdditem.Enabled = false;
                }
                else if (BulanPakai >= MasterBudget && RuleCalc >= 1)
                {
                    DisplayAJAXMessage(this, "Penambahan Budget Tidak Boleh Melebihi Master Budget = " + MasterBudget.ToString("##0"));
                    txtQty.Text = "";
                    btnAdditem.Enabled = false;
                }
            }

        }

        private decimal CheckMasterBudget()
        {
            decimal master = 0;
            BudgetingFacade bg = new BudgetingFacade();
            Budget bu = new Budget();
            bg.Pilihan = "MasterBudget";
            bg.Criteria = " and Tahun=" + ddlTahun.SelectedValue;
            bg.Criteria += " and DeptID=" + ddlDept.SelectedValue;
            bg.Criteria += " and ItemID=" + txtItemID.Text;
            bu = bg.Retrieve(true);
            master = bu.Quantity;

            return master;

        }

        private decimal CheckRuleCalc()
        {
            decimal ruleCalc = 0;
            BudgetingFacade bg = new BudgetingFacade();
            Budget bu = new Budget();
            bg.Pilihan = "MasterBudget";
            bg.Criteria = " and Tahun=" + ddlTahun.SelectedValue;
            bg.Criteria += " and DeptID=" + ddlDept.SelectedValue;
            bg.Criteria += " and ItemID=" + txtItemID.Text;
            bu = bg.Retrieve(true);
            ruleCalc = bu.RuleCalc;
            return ruleCalc;

        }

        private decimal CheckSudahDibuat()
        {
            decimal data = 0;
            string periode = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("PeriodeBudget", "BudgetATK");
            string between = string.Empty;
            if (periode == "6")
            {
                switch (int.Parse(ddlBulan.SelectedValue))
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        between = " and Bulan between 1 and 6";
                        break;
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                        between = " and Bulan between 7 and 12";
                        break;
                }
            }
            BudgetingFacade bg = new BudgetingFacade();
            Budget bu = new Budget();
            bg.Pilihan = "DetailBudget";
            bg.Criteria = " and Tahun=" + ddlTahun.SelectedValue;
            bg.Criteria += " and DeptID=" + ddlDept.SelectedValue;
            bg.Criteria += " and ItemID=" + txtItemID.Text;
            bg.Criteria += (periode == "1") ? " and Bulan=" + ddlBulan.SelectedValue : between;
            bu = bg.Retrieve(true);
            data = bu.Quantity;
            return data;
        }

        protected void txtQty_Change(object sender, EventArgs e)
        {
            decimal QtyMasterBudget = this.CheckMasterBudget();
            //decimal Qty = this.CheckSudahDibuat();
            decimal QtyUdahBudget = 0; string bulan = ""; int bln = int.Parse(ddlBulan.SelectedValue);
            decimal CheckRuleCalc = this.CheckRuleCalc();
            if(CheckRuleCalc==6)
            {
                if(bln==1 || bln == 2 || bln == 3 || bln == 4 || bln == 5 || bln == 6){ bulan = " and b.Bulan between 1 and 6"; }
                else { bulan = " and b.Bulan between 7 and 12"; }
            }
            else { bulan = " and b.Bulan="+bln; }
            string query=
"SELECT isnull(sum(d.QtyApv),0) SumQty FROM BudgetATKDetail d, BudgetATK b " +
"WHERE d.BudgetID = b.ID AND d.RowStatus > -1 AND b.RowStatus > -1 " +
"AND b.DeptiD = "+ ddlDept.SelectedValue + " AND b.Tahun = "+ ddlTahun.SelectedValue + " AND d.ItemID = "+ txtItemID.Text + bulan;
            ZetroView zl = new ZetroView();zl.QueryType = Operation.CUSTOM; zl.CustomQuery =query;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows) { while (sdr.Read()){ QtyUdahBudget = decimal.Parse(sdr["SumQty"].ToString());} }
            if (txtQty.Text != string.Empty)
            {
                if ((decimal.Parse(txtQty.Text) + QtyUdahBudget) > QtyMasterBudget)
                {
                    DisplayAJAXMessage(this, "Quantity Melebihi Master Budget = " + QtyMasterBudget.ToString("##0"));
                    txtQty.Text = (QtyMasterBudget - QtyUdahBudget).ToString("##0");
                    return;
                }
                GetHeaderHistory();
                cek_budget();
            }
        }
        private void HeadID()
        {
            BudgetingFacade bg = new BudgetingFacade();
            bg.Pilihan = "HeadDept";
            bg.Criteria = " and UserID=" + ((Users)Session["Users"]).ID;
            Budget bd = bg.Retrieve(true);
            txtHeadID.Text = bd.HeadID.ToString();
        }
        private string BudgetNumber()
        {
            string Number = string.Empty;
            BudgetingFacade bg = new BudgetingFacade();
            Number = ((Users)Session["Users"]).KodeLokasi.ToString();
            Number += DateTime.Now.Year.ToString().Substring(2, 2) + DateTime.Now.Month.ToString().PadLeft(2, '0');
            Number += ddlDept.SelectedValue.ToString().PadLeft(3, '0');
            Number += "-";
            bg.Pilihan = "Nomor";
            bg.Criteria = " Where Tahun=" + ddlTahun.SelectedValue;
            Budget db = bg.Retrieve(true);
            if (db.BudgetID > 0)
            {
                Number += (db.BudgetID + 1).ToString().PadLeft(3, '0');
            }
            else
            {
                Number += "001";
            }
            return Number;
        }
        protected void ddlDept_Change(object sender, EventArgs e)
        {
            HeadID();
            GetHeaderHistory();
        }
        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            ArrayList arrItem = (Session["ListAtk"] == null) ? new ArrayList() : (ArrayList)Session["ListAtk"];
            Budget bu = new Budget();
            /** validasi inputan */
            if (txtItemID.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Material tidak ada di database");
                return;
            }
            if (txtQty.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Qty Harus di isi");
                return;
            }
            string[] item = txtMaterial.Text.Split('-');
            bu.ItemID = int.Parse(txtItemID.Text);
            bu.ItemCode = item[0].ToString();
            bu.ItemName = item[1].ToString();
            bu.Quantity = decimal.Parse(txtQty.Text);
            bu.Keterangan = txtKeterangan.Text;
            bu.DeptID = int.Parse(ddlDept.SelectedValue);
            bu.Tahun = int.Parse(ddlTahun.SelectedValue);
            bu.Bulan = int.Parse(ddlBulan.SelectedValue);
            bu.UomCode = txtUnit.Text;
            bu.ID = 0;
            arrItem.Add(bu);
            Session["ListAtk"] = arrItem;
            lstAtk.DataSource = arrItem;
            lstAtk.DataBind();
            GetHeaderHistory();
            ClearField();
        }
        protected void lstAtk_Command(object sender, RepeaterCommandEventArgs e)
        {
            Budget bg = new Budget();
            BudgetingFacade bud = new BudgetingFacade();
            switch (e.CommandName)
            {
                case "edit":
                    bud.Pilihan = "DetailBudget";
                    bud.Criteria = " where bad.ID=" + e.CommandArgument;
                    bg = bud.Retrieve(true);
                    txtBudgetNo.Text = bg.BudgetNo;
                    txtItemID.Text = bg.ItemID.ToString();
                    txtMaterial.Text = bg.ItemCode + " - " + bg.ItemName;
                    txtQty.Text = bg.Quantity.ToString();
                    txtUnit.Text = bg.UomCode.ToString();
                    txtKeterangan.Text = bg.Keterangan.ToString();
                    btnAdditem.Enabled = false;
                    break;
                case "delet":
                    if (Session["AlasanCancel"] != null)
                    {
                        if (Session["AlasanCancel"].ToString() != string.Empty)
                        {
                            bg.ID = int.Parse(e.CommandArgument.ToString());
                            bg.RowStatus = -2;
                            bg.Keterangan = Session["AlasanCancel"].ToString();
                            int result = bud.Delete(bg);
                            if (result > 0)
                            {
                                LoadBudget(txtBudgetNo.Text);
                            }
                        }
                        else
                        {
                            DisplayAJAXMessage(this, "Alasan Cancel harus di isi");
                            return;
                        }
                    }
                    break;
                case "dele":
                    ArrayList arrData = (ArrayList)Session["ListAtk"];
                    int idx = int.Parse(e.CommandArgument.ToString());
                    arrData.RemoveAt(idx);
                    Session["ListAtk"] = arrData;
                    lstAtk.DataSource = arrData;
                    lstAtk.DataBind();
                    break;
            }
        }

        private void ClearField()
        {
            txtUnit.Text = string.Empty;
            txtItemID.Text = string.Empty;
            txtKeterangan.Text = string.Empty;
            txtMaterial.Text = string.Empty;
            txtQty.Text = string.Empty;
        }
        protected void lstAtk_Databound(object sender, RepeaterItemEventArgs e)
        {
            Image edt = (Image)e.Item.FindControl("edt");
            Image del = (Image)e.Item.FindControl("del");
            Image dls = (Image)e.Item.FindControl("dels");
            Budget bu = (Budget)e.Item.DataItem;
            edt.Visible = (bu.ID > 0) ? true : false;
            del.Visible = (bu.ID == 0) ? true : false;
            dls.Visible = (bu.ID > 0) ? true : false;
            dls.Attributes.Add("onclick", "return HapusData()");
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormBudgetingATK.aspx");
        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            if (Session["ListAtk"] != null)
            {
                BudgetingFacade bg = new BudgetingFacade();
                ArrayList arrData = (ArrayList)Session["ListAtk"];
                Budget header = new Budget();
                header.UserID = ((Users)Session["Users"]).ID;
                header.HeadID = int.Parse(txtHeadID.Text);
                header.DeptID = int.Parse(ddlDept.SelectedValue);
                header.CreatedBy = ((Users)Session["Users"]).UserID.ToString();
                header.Bulan = int.Parse(ddlBulan.SelectedValue.ToString());
                header.Tahun = int.Parse(ddlTahun.SelectedValue.ToString());
                string bdgno = BudgetNumber();
                header.BudgetNo = bdgno.ToString();
                int result = bg.Insert(header);
                if (result > 0)
                {
                    Budget Detail = new Budget();
                    foreach (Budget bu in arrData)
                    {
                        Detail.BudgetID = result;
                        Detail.ItemID = bu.ItemID;
                        Detail.UomCode = bu.UomCode;
                        Detail.Quantity = bu.Quantity;
                        Detail.AppvQty = bu.Quantity;
                        Detail.Keterangan = bu.Keterangan;
                        Detail.CreatedBy = ((Users)Session["Users"]).UserID.ToString();
                        int rest = bg.insert_detail(Detail);
                        if (rest > 0)
                        {
                            Session["ListAtk"] = null;
                            // Response.Redirect("BudgetingATK.aspx");
                            txtBudgetNo.Text = bdgno.ToString();
                        }
                    }
                }
            }

            else
            {
                Response.Redirect("FormBudgetingATK.aspx");
            }
        }
        protected void btnList_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListBudgetATK.aspx?fr=1");
        }
        protected void btnCari_Click(object sender, EventArgs e)
        {
            BudgetingFacade bg = new BudgetingFacade();
            ArrayList arrData = new ArrayList();
            bg.Pilihan = "DetailBudget";
            bg.Criteria = " where BudgetNo='" + txtCari.Text + "'";
            arrData = bg.Retrieve();
            lstAtk.DataSource = arrData;
            lstAtk.DataBind();
            foreach (Budget bu in arrData)
            {
                ddlBulan.SelectedValue = bu.Bulan.ToString();
                ddlTahun.SelectedValue = bu.Tahun.ToString();
                ddlDept.SelectedValue = bu.DeptID.ToString();
                txtBudgetNo.Text = bu.BudgetNo.ToString();
            }
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "MyScript", myScript, true);
        }
        private void LoadBudget(string budgetNo)
        {
            BudgetingFacade bg = new BudgetingFacade();
            ArrayList arrData = new ArrayList();
            bg.Pilihan = "DetailBudget";
            bg.Criteria = " where BudgetNo='" + budgetNo + "'";
            arrData = bg.Retrieve();
            lstAtk.DataSource = arrData;
            lstAtk.DataBind();
            foreach (Budget bu in arrData)
            {
                ddlBulan.SelectedValue = bu.Bulan.ToString();
                ddlTahun.SelectedValue = bu.Tahun.ToString();
                ddlDept.SelectedValue = bu.DeptID.ToString();
                txtBudgetNo.Text = bu.BudgetNo.ToString();
            }
        }
    }
}

