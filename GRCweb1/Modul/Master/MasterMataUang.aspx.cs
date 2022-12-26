using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;
using System.Data;

namespace GRCweb1.Modul.Master
{
    public partial class MasterMataUang : System.Web.UI.Page
    {
        public string SimbolMataUang = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                frm10.Visible = false;
                FillWithISOCurrencySymbols(ddlCurrency);
                SortDDL(ref ddlCurrency);
                clearForm();
                //ddlCurrency.SelectedValue = "USD";
                SelectKurs("USD");
                SimbolMataUang = ddlCurrency.SelectedItem.ToString();
                LoadMataUang();
                lstMataUang();
                string[] arrDpt = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("InputKurs", "MasterKurs").Split(',');
                if (arrDpt.Contains(((Users)Session["Users"]).DeptID.ToString()))
                {
                    btnAddKurs.Disabled = false;
                    btnNew.Disabled = false;
                    btnUpdate.Disabled = false;
                    btnAddKurs0.Disabled = false;
                    btnDelete.Disabled = false;
                }
                else
                {
                    btnAddKurs.Disabled = true;
                    btnNew.Disabled = true;
                    btnUpdate.Disabled = true;
                    btnAddKurs0.Disabled = true;
                    btnDelete.Disabled = true;
                }
            }
        }
        private void SelectKurs(string kurs)
        {
            ddlCurrency.ClearSelection();
            foreach (ListItem item in ddlCurrency.Items)
            {
                if (item.Value == kurs)
                {
                    item.Selected = true;
                    return;
                }
            }
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private ArrayList LoadGridMataUangKurs(int typekurs)
        {
            ArrayList arrMataUangKurs = new ArrayList();
            MataUangKursFacade mataUangKursFacade = new MataUangKursFacade();
            if (txtMUID.Text.Trim() == string.Empty)
                txtMUID.Text = "0";
            arrMataUangKurs = mataUangKursFacade.RetrieveByMUID(Convert.ToInt32(txtMUID.Text), typekurs);
            if (arrMataUangKurs.Count > 0)
            {
                return arrMataUangKurs;
            }
            arrMataUangKurs.Add(new MataUang());
            return arrMataUangKurs;
        }

        private void LoadDataGridMataUang(ArrayList arrMataUangKurs)
        {
            this.GridView1.DataSource = arrMataUangKurs;
            this.GridView1.DataBind();
        }

        private void clearForm()
        {
            txtMUID.ReadOnly = true;
            txtMUName.ReadOnly = false;
            ViewState["id"] = null;
            txtMUID.Text = string.Empty;
            txtMUName.Text = string.Empty;
            txtMUSymbol.Text = string.Empty;
            txtMUID.Focus();
        }

        protected void btnUpdate1_ServerClick(object sender, EventArgs e)
        {
            LoadMataUang();
            PanelKurs.Visible = false;
            Panel3.Visible = false;
            newList.Visible = false;
        }

        protected void btnUpdate0_ServerClick(object sender, EventArgs e)
        {
            MataUangKurs mataUangKurs = new MataUangKurs();
            MataUangKursFacade mataUangKursFacade = new MataUangKursFacade();
            mataUangKurs.MuId = Convert.ToInt32(txtMUID.Text);
            mataUangKurs.Kurs = Convert.ToDecimal(txtKurs.Text);
            mataUangKurs.DrTgl = DateTime.Parse(txtTgl1.Text);
            mataUangKurs.SdTgl = DateTime.Parse(txtTgl2.Text);
            mataUangKurs.TypeKurs = 1;
            int intResult = 0;
            if (mataUangKurs.SdTgl < mataUangKurs.DrTgl)
            {
                return;
            }
            intResult = mataUangKursFacade.Insert(mataUangKurs);
            LoadMataUang();
            PanelKurs.Visible = false;
            Panel3.Visible = false;
            newList.Visible = false;
        }

        protected void btnUpdate2_ServerClick(object sender, EventArgs e)
        {
            LoadMataUang();
            PanelKurs0.Visible = false;
            Panel3.Visible = false;
            newList.Visible = false;
        }

        protected void btnUpdate3_ServerClick(object sender, EventArgs e)
        {
            MataUangKurs mataUangKurs = new MataUangKurs();
            MataUangKursFacade mataUangKursFacade = new MataUangKursFacade();
            mataUangKurs.MuId = Convert.ToInt32(txtMUID.Text);
            mataUangKurs.Kurs = Convert.ToDecimal(txtKurs0.Text);
            mataUangKurs.DrTgl = DateTime.Parse(txtTgl3.Text);
            mataUangKurs.SdTgl = DateTime.Parse(txtTgl4.Text);
            mataUangKurs.TypeKurs = 2;
            int intResult = 0;
            if (mataUangKurs.SdTgl < mataUangKurs.DrTgl)
            {
                return;
            }
            intResult = mataUangKursFacade.Insert(mataUangKurs);
            LoadMataUang();
            PanelKurs0.Visible = false;
            Panel3.Visible = false;
            newList.Visible = false;
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            MataUang mataUang = new MataUang();
            MataUangFacade mataUangFacade = new MataUangFacade();
            mataUang.Lambang = txtMUSymbol.Text;
            mataUang.Nama = txtMUName.Text;
            mataUang.Twoiso = Txt2ISO.Text;
            int intResult = 0;
            intResult = mataUangFacade.Insert(mataUang);
            LoadMataUang();
        }

        protected void btnDelete_ServerClick(object sender, EventArgs e)
        {
            MataUang mataUang = new MataUang();
            MataUangFacade mataUangFacade = new MataUangFacade();
            mataUang.ID = int.Parse(txtMUID.Text);
            mataUang.Twoiso = Txt2ISO.Text;
            int intResult = mataUangFacade.Delete(mataUang);
            int typekurs = 0;
            if (PanelKurs.Visible == true)
                typekurs = 1;
            else
                typekurs = 2;
            if (mataUangFacade.Error == string.Empty && intResult > 0)
            {
                LoadDataGridMataUang(LoadGridMataUangKurs(typekurs));
                InsertLog("Delete");
                //clearForm();
            }
        }

        protected void btnAddKurs_ServerClick(object sender, EventArgs e)
        {
            MataUangKurs mataUangKurs = new MataUangKurs();
            MataUangKursFacade mataUangKursFacade = new MataUangKursFacade();


            if (txtMUID.Text != string.Empty)
            {
                mataUangKurs = mataUangKursFacade.RetrieveByLast(Convert.ToInt32(txtMUID.Text), 1);
                DateTime lasttgl = mataUangKurs.SdTgl.AddDays(1);
                DateTime lasttglnext = mataUangKurs.SdTgl.AddDays(7);
                txtTgl1.Text = lasttgl.ToString("dd-MMM-yyyy");
                txtTgl2.Text = lasttglnext.ToString("dd-MMM-yyyy");
                LoadDataGridMataUang(LoadGridMataUangKurs(1));
                Panel3.Visible = true;
                PanelKurs.Visible = true;
                PanelKurs0.Visible = false;
                frm10.Visible = true;
                GridView1.Visible = true;
                lstKurs.Visible = false;
                newList.Visible = false;
            }
            else
            {
                DisplayAJAXMessage(this, "Tentukan Mata Uang");
                PanelKurs.Visible = false;
            }
        }
        protected void btnAddKurs0_ServerClick(object sender, EventArgs e)
        {
            MataUangKurs mataUangKurs = new MataUangKurs();
            MataUangKursFacade mataUangKursFacade = new MataUangKursFacade();


            if (txtMUID.Text != string.Empty)
            {
                mataUangKurs = mataUangKursFacade.RetrieveByLast(Convert.ToInt32(txtMUID.Text), 2);
                DateTime lasttgl = mataUangKurs.SdTgl.AddDays(1);
                DateTime lasttglnext = mataUangKurs.SdTgl.AddDays(7);
                txtTgl3.Text = lasttgl.ToString("dd-MMM-yyyy");
                txtTgl4.Text = lasttglnext.ToString("dd-MMM-yyyy");
                LoadDataGridMataUang(LoadGridMataUangKurs(2));
                PanelKurs.Visible = false;
                PanelKurs0.Visible = true;
                Panel3.Visible = true;
                frm10.Visible = true;
                GridView1.Visible = true;
                lstKurs.Visible = false;
                newList.Visible = false;
            }
            else
            {
                DisplayAJAXMessage(this, "Tentukan Mata Uang");
                PanelKurs0.Visible = false;
            }
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearForm();
            frm10.Visible = true;

        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        public static void FillWithISOCurrencySymbols(ListControl ctrl)
        {
            foreach (CultureInfo cultureInfo in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                RegionInfo regionInfo = new RegionInfo(cultureInfo.LCID);
                if (ctrl.Items.FindByValue(regionInfo.ISOCurrencySymbol) == null)
                {
                    ctrl.Items.Add(new ListItem(regionInfo.Name + "," + regionInfo.EnglishName + " (" + regionInfo.CurrencyEnglishName + ")" + " {" +
                    regionInfo.CurrencySymbol + "}", regionInfo.ISOCurrencySymbol));
                }
            }

            RegionInfo currentRegionInfo = new RegionInfo(CultureInfo.CurrentCulture.LCID);
            //- Default the selection to the current cultures currency symbol
            if (ctrl.Items.FindByValue(currentRegionInfo.ISOCurrencySymbol) != null)
            {
                ctrl.Items.FindByValue(currentRegionInfo.ISOCurrencySymbol).Selected = true;
            }
        }

        protected void ddlCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            SimbolMataUang = ddlCurrency.SelectedItem.ToString();
            LoadMataUang();
        }

        private void LoadMataUang()
        {
            MataUangFacade mataUangFacade = new MataUangFacade();
            MataUang mataUang = new MataUang();
            int typekurs = 0;
            if (PanelKurs.Visible == true)
                typekurs = 1;
            else
                typekurs = 2;
            mataUang = mataUangFacade.Retrieveby2ISO(ddlCurrency.SelectedItem.Text.Substring(0, 2));
            if (mataUangFacade.Error == string.Empty)
            {
                if (mataUang.ID > 0)
                {
                    txtMUID.Text = mataUang.ID.ToString();
                    txtMUName.Text = mataUang.Nama;
                    txtMUSymbol.Text = mataUang.Lambang;
                    Txt2ISO.Text = mataUang.Twoiso;
                    LabelKeterangan.Text = "*) Mata Uang SUDAH tersedia di DATABASE";
                }
                else
                {
                    RegionInfo currentRegionInfo = new RegionInfo(ddlCurrency.SelectedItem.Text.Substring(0, 2));
                    txtMUID.Text = string.Empty;
                    txtMUName.Text = currentRegionInfo.CurrencyEnglishName;
                    txtMUSymbol.Text = currentRegionInfo.TwoLetterISORegionName + currentRegionInfo.CurrencySymbol;
                    Txt2ISO.Text = currentRegionInfo.TwoLetterISORegionName;
                    LabelKeterangan.Text = "*) Mata Uang BELUM tersedia di DATABASE, klik tombol SIMPAN";
                }
            }
            LoadDataGridMataUang(LoadGridMataUangKurs(typekurs));
        }

        protected void btnSymbol_serverclick(object sender, EventArgs e)
        {
            RegionInfo currentRegionInfo = new RegionInfo(ddlCurrency.SelectedItem.Text.Substring(0, 2));
            txtMUSymbol.Text = currentRegionInfo.CurrencySymbol;
        }
        protected void btnSymbol0_serverclick(object sender, EventArgs e)
        {
            RegionInfo currentRegionInfo = new RegionInfo(ddlCurrency.SelectedItem.Text.Substring(0, 2));
            txtMUSymbol.Text = currentRegionInfo.TwoLetterISORegionName + currentRegionInfo.CurrencySymbol;
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;

            //if (txtSearch.Text == string.Empty)
            //    LoadDataGridMataUang(LoadGridMataUang());
            //else
            //    LoadDataGridMataUang(LoadGridByCriteria());
        }

        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Master MataUang";
            eventLog.EventName = eventName;
            eventLog.DocumentNo = txtMUID.Text;

            EventLogFacade eventLogFacade = new EventLogFacade();
            int intResult = eventLogFacade.Insert(eventLog);
            if (eventLogFacade.Error == string.Empty)
                clearForm();
        }

        public string ValidasiText()
        {
            if (txtKurs.Text == string.Empty)
                return "Nilai Kurs tidak boleh kosong";
            else if (txtTgl1.Text == string.Empty)
                return "Dari Tanggal tidak boleh kosong";
            else if (txtTgl2.Text == string.Empty)
                return "Sampai dengan Tanggal tidak boleh kosong";
            //else if (ddlTypeUnitKerja.SelectedIndex == 0)
            //    return "Tipe Unit Kerja harus dipilih";
            //else if (ddlUnitKerja.SelectedIndex == 0)
            //    return "Unit Kerja harus dipilih";

            MataUangFacade mataUangFacade = new MataUangFacade();
            MataUang mataUang = new MataUang();

            if (txtMUID.Text != string.Empty && ViewState["id"] == null)
            {
                mataUang = mataUangFacade.RetrieveById(Convert.ToInt32(txtMUID.Text));
                if (mataUangFacade.Error == string.Empty)
                {
                    if (mataUang.ID > 0)
                    {
                        return "Mata Uang sudah ada";
                    }
                }

            }

            return string.Empty;
        }

        private void SortDDL(ref DropDownList objDDL)
        {
            ArrayList textList = new ArrayList();
            ArrayList valueList = new ArrayList();


            foreach (ListItem li in objDDL.Items)
            {
                textList.Add(li.Text);
            }

            textList.Sort();


            foreach (object item in textList)
            {
                string value = objDDL.Items.FindByText(item.ToString()).Value;
                valueList.Add(value);
            }
            objDDL.Items.Clear();

            for (int i = 0; i < textList.Count; i++)
            {
                ListItem objItem = new ListItem(textList[i].ToString(), valueList[i].ToString());
                objDDL.Items.Add(objItem);
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int strError = 0;
            MataUangKurs mataUangKurs = new MataUangKurs();
            MataUangKursFacade mataUangKursFacade = new MataUangKursFacade();
            GridViewRow row = GridView1.Rows[index];
            int typekurs = 0;
            if (PanelKurs.Visible == true)
                typekurs = 1;
            else
                typekurs = 2;
            mataUangKurs.Id = Convert.ToInt32(row.Cells[0].Text);
            strError = mataUangKursFacade.Delete(mataUangKurs);
            LoadDataGridMataUang(LoadGridMataUangKurs(typekurs));
            PanelKurs.Visible = false;
        }
        private void lstMataUang()
        {
            frm10.Visible = false;
            GridView1.Visible = false;
            ArrayList arrMU = new ArrayList();
            arrMU = new MataUangFacade().RetrieveList();
            lstKurs.DataSource = arrMU;
            lstKurs.DataBind();
            newList.Visible = true;
            lstKurs.Visible = true;
        }
        protected void btnList_ServerClick(object sender, EventArgs e)
        {
            lstMataUang();
        }

    }
}