using System;
using System.Collections;
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
using Domain;
using BusinessFacade;
using DataAccessLayer;

namespace GRCweb1.Modul.MTC
{
    public partial class EstimasiMaterial_Rev1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadItemGroup();
                Session["Uom"] = null;
                Session["Material"] = null;
            }
        }
        protected void txtNomor_Change(object sender, EventArgs e)
        {
            MTC_ProjectFacade_Rev1 mpp = new MTC_ProjectFacade_Rev1();
            MTC_Project_Rev1 mp = mpp.RetrieveProject(txtNomor.Text);
            txtNamaImprovement.Text = mp.NamaProject;
            txtProjectID.Value = mp.ID.ToString();
            txtFinishDate.Value = mp.FinishDate.ToString();
            Session["ApvPM"] = mp.VerPM;

            if (mp.VerPM == 2 || mp.VerPM == 3)
            {
                btnSimpan.Enabled = false; txtNamaBarang.Enabled = false; txtMessage.Text = "Improvment sudah di approved PM";
            }
            else
            {

                if (mp.Status > 1)
                { btnSimpan.Enabled = false; txtNamaBarang.Enabled = false; }
                else { btnSimpan.Enabled = true; txtNamaBarang.Enabled = true; }

                LoadEstimasiMaterial(mp.ID, true);
            }
        }
        protected void txtNamaBarang_Change(object sender, EventArgs e)
        {
            Session["Uom"] = null;
            LoadBarang();
            txtNamaBarang.Text = "";
        }
        protected void ddlNamaBarang_Change(object sender, EventArgs e)
        {
            if (ddlNamaBarang.SelectedIndex > 0)
            {
                txtJumlah.Focus();
                POPurchnDetailFacade po = new POPurchnDetailFacade();
                POPurchnDetail pd = po.GetLastPOPrice(int.Parse(ddlNamaBarang.SelectedValue.ToString()), int.Parse(ddlMaterialType.SelectedValue));
                if (Session["Uom"] != null && ddlNamaBarang.SelectedIndex > 0)
                {
                    string[] satuan = Session["Uom"].ToString().Split(',');
                    txtUom.Text = satuan[ddlNamaBarang.SelectedIndex - 1].ToString();
                }
                txtHarga.Text = pd.Price.ToString("###,###.#0");
                txtTglPO.Text = pd.POPurchnDate.ToString("dd-MMM-yyyy");

            }
        }
        private void LoadItemGroup()
        {
            ddlMaterialType.Items.Clear();
            ArrayList arrItemTypePurchn = new ArrayList();
            ItemTypePurchnFacade itemTypePurchnFacade = new ItemTypePurchnFacade();
            arrItemTypePurchn = itemTypePurchnFacade.Retrieve();

            //ddlMaterialType.Items.Add(new ListItem("-- Pilih Tipe Barang --", string.Empty));
            foreach (ItemTypePurchn itemTypePurchn in arrItemTypePurchn)
            {
                ddlMaterialType.Items.Add(new ListItem(itemTypePurchn.TypeDescription, itemTypePurchn.ID.ToString()));
            }
        }
        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            int result = 0;
            ArrayList arrData = (Session["Material"] != null) ? (ArrayList)Session["Material"] : new ArrayList();
            Domain.EstimasiMaterial_Rev1 esm = new Domain.EstimasiMaterial_Rev1();
            if (txtProjectID.Value == string.Empty) return;
            if (ddlNamaBarang.SelectedIndex <= 0) return;
            string[] item = ddlNamaBarang.SelectedItem.Text.Split('-');
            int jml = (int.TryParse(txtJumlah.Text, out jml)) ? jml : 0;
            if (jml == 0) { txtMessage.Text = "Jumlah Harus Lebih dari nol"; return; }
            if (txtSchedule.Text == string.Empty)
            {
                txtMessage.Text = "Tanggal Schedule pemakaian harus di isi"; return;
            }
            //check tanggal
            if (DateTime.Parse(txtSchedule.Text) > DateTime.Parse(txtFinishDate.Value))
            {
                txtMessage.Text = "Tanggal Pakai melebihi tgl Finish Project (" + txtFinishDate.Value + ")"; return;
            }
            esm.ItemCode = item[0].ToString();
            esm.ItemName = item[1].ToString();
            esm.UomCode = txtUom.Text;
            esm.ProjectID = int.Parse(txtProjectID.Value);
            esm.ItemID = int.Parse(ddlNamaBarang.SelectedValue);
            esm.ItemTypeID = int.Parse(ddlMaterialType.SelectedValue);
            esm.Jumlah = decimal.Parse(txtJumlah.Text);
            esm.Harga = (txtHarga.Text == string.Empty) ? 0 : decimal.Parse(txtHarga.Text);
            esm.Schedule = DateTime.Parse(txtSchedule.Text);
            //arrData.Add(esm);
            MTC_ProjectFacade_Rev1 mpp = new MTC_ProjectFacade_Rev1();
            esm.RowStatus = -1;
            result = mpp.Insert(esm, false);
            arrData = mpp.RetrieveEstimasiMaterial(int.Parse(txtProjectID.Value), true);
            Session["Material"] = arrData;
            lstMaterial.DataSource = arrData;
            lstMaterial.DataBind();
            ddlNamaBarang.SelectedIndex = 0;
            txtJumlah.Text = "";
            txtHarga.Text = "";
            txtSchedule.Text = "";
        }
        private void LoadBarang()
        {
            InventoryFacade inv = new InventoryFacade();
            ArrayList arrData = new ArrayList();
            switch (ddlMaterialType.SelectedValue)
            {
                case "1":
                    arrData = inv.RetrieveByCriteria("ItemName", txtNamaBarang.Text);
                    break;
                case "2":
                    arrData = inv.AssetRetrieveByCriteria("ItemName", txtNamaBarang.Text);
                    break;
                case "3":
                    arrData = inv.BiayaRetrieveByCriteria("ItemName", txtNamaBarang.Text);
                    break;
            }
            string Uome = string.Empty;
            ddlNamaBarang.Items.Clear();
            ddlNamaBarang.Items.Add(new ListItem("--Pilih Material--", "0"));
            foreach (Inventory ive in arrData)
            {
                ddlNamaBarang.Items.Add(new ListItem(ive.ItemCode + " - " + ive.ItemName, ive.ID.ToString()));
                Uome += ive.UomCode + ",";
            }
            Session["Uom"] = Uome.Substring(0, Uome.Length - 1);
        }
        private void LoadEstimasiMaterial(int ProjectID)
        {
            ArrayList arrData = new ArrayList();
            MTC_ProjectFacade_Rev1 mpp = new MTC_ProjectFacade_Rev1();
            arrData = mpp.RetrieveEstimasiMaterial(ProjectID, true);
            lstMaterial.DataSource = arrData;
            lstMaterial.DataBind();
        }
        private void LoadEstimasiMaterial(int ProjectID, bool Draf)
        {
            ArrayList arrData = new ArrayList();
            MTC_ProjectFacade_Rev1 mpp = new MTC_ProjectFacade_Rev1();
            arrData = mpp.RetrieveEstimasiMaterial(ProjectID, Draf);
            lstMaterial.DataSource = arrData;
            lstMaterial.DataBind();
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("EstimasiMaterial_Rev1.aspx", true);
        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            int ApvPM = Convert.ToInt32(Session["ApvPM"]);

            ArrayList arrData = (Session["Material"] != null) ? (ArrayList)Session["Material"] : new ArrayList();
            if (arrData.Count == 0) { return; }
            MTC_ProjectFacade_Rev1 mpp = new MTC_ProjectFacade_Rev1();
            int result = 0;

            foreach (Domain.EstimasiMaterial_Rev1 esm in arrData)
            {
                Domain.EstimasiMaterial_Rev1 em = new Domain.EstimasiMaterial_Rev1();
                em.ID = esm.ID;
                em.ProjectID = esm.ProjectID;
                em.ItemTypeID = esm.ItemTypeID;
                em.ItemID = esm.ItemID;
                em.Jumlah = esm.Jumlah;
                em.Harga = esm.Harga;
                em.Schedule = esm.Schedule;
                em.RowStatus = 0;

                result += (esm.ID == 0) ? mpp.Insert(em, true) : mpp.Update(em, true);
            }
            if (result > 0)
                txtMessage.Text = "Data Estimasi Material Tersimpan " + result.ToString() + " item(s)";
            LoadEstimasiMaterial(int.Parse(txtProjectID.Value));
            Session["Material"] = null;

        }
        protected void btnCari_Click(object sender, EventArgs e)
        {
            Response.Redirect("LapImprovement.aspx?p=2&n=" + txtCari.Text);

            //MTC_ProjectFacade mpp = new MTC_ProjectFacade();
            //ArrayList arrData = mpp.RetrieveEstimasiMaterialSearch(txtCari.Text);
            //lstMaterial.DataSource = arrData;
            //lstMaterial.DataBind();
            //txtCari.Text = "";
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            MTC_ProjectFacade_Rev1 mpp = new MTC_ProjectFacade_Rev1();
            Domain.EstimasiMaterial_Rev1 esm = new Domain.EstimasiMaterial_Rev1();
            esm.ID = int.Parse(txtID.Value);
            esm.RowStatus = 0;
            esm.Jumlah = decimal.Parse(txtJumlah.Text);
            esm.Harga = (txtHarga.Text == string.Empty) ? 0 : decimal.Parse(txtHarga.Text);
            esm.Schedule = DateTime.Parse(txtSchedule.Text);
            int result = mpp.Update(esm, true);
            LoadEstimasiMaterial(int.Parse(txtProjectID.Value));
            btnUpdate.Visible = false;
            btnAddItem.Visible = true;
            txtMessage.Text = (result > 1) ? "Update berhasil" : "";
            ddlNamaBarang.SelectedIndex = 0;
            txtJumlah.Text = "";
            txtHarga.Text = "";
            txtSchedule.Text = "";
        }
        protected void lstMaterial_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Domain.EstimasiMaterial_Rev1 em = (Domain.EstimasiMaterial_Rev1)e.Item.DataItem;
            string Nomor = txtNomor.Text.Trim();

            MTC_ProjectFacade_Rev1 ProjectFacade = new MTC_ProjectFacade_Rev1();
            Domain.EstimasiMaterial_Rev1 esmD = new Domain.EstimasiMaterial_Rev1();
            esmD = ProjectFacade.RetrieveDataProject(Nomor);

            ((Image)e.Item.FindControl("edt")).Visible = (esmD.Status > 1) ? false : true;
            ((Image)e.Item.FindControl("del")).Visible = (esmD.Status > 1) ? false : true;
            ((Image)e.Item.FindControl("clo")).Visible = (esmD.Status > 1) ? false : true;
            //((Image)e.Item.FindControl("edt")).Visible = (em.ID > 0) ? true : false;
            //((Image)e.Item.FindControl("del")).Visible = (em.ID > 0) ? true : false;
            //((Image)e.Item.FindControl("clo")).Visible = (em.ID > 0) ? false : true;
            //if (esmD.Status > 1)
            //{ btnSimpan.Enabled = false; txtNamaBarang.Enabled = false; }
            //else { btnSimpan.Enabled = true; txtNamaBarang.Enabled = true; }

            HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("xx");
            if (tr.Cells[2].InnerHtml.ToString().TrimEnd().ToUpper() == "TOTAL")
            {
                tr.Attributes.Add("class", "Line3");
                tr.Cells[0].InnerHtml = "";
                tr.Cells[4].InnerHtml = "";
                tr.Cells[5].InnerHtml = "";
                tr.Cells[7].InnerHtml = "";
                tr.Cells[8].InnerHtml = "";
            }
        }
        protected void lstMaterial_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            int ID = int.Parse(e.CommandArgument.ToString());
            MTC_ProjectFacade_Rev1 mpp = new MTC_ProjectFacade_Rev1();
            switch (e.CommandName)
            {
                case "edit":
                    Domain.EstimasiMaterial_Rev1 mp = mpp.RetrieveEstimasiMaterial(int.Parse(txtProjectID.Value), false, ID);
                    txtID.Value = mp.ID.ToString();
                    ddlMaterialType.SelectedValue = mp.ItemTypeID.ToString();
                    //txtNomor.Text = mp.Nomor;
                    //txtNamaImprovement.Text = mp.NamaProject;
                    txtNamaBarang.Text = mp.ItemName.ToString();
                    txtNamaBarang_Change(null, null);
                    ddlNamaBarang.SelectedValue = mp.ItemID.ToString();
                    txtJumlah.Text = mp.Jumlah.ToString();
                    txtHarga.Text = mp.Harga.ToString();
                    txtSchedule.Text = mp.Schedule.ToString("dd-MMM-yyyy");
                    btnAddItem.Visible = false;
                    btnUpdate.Visible = true;
                    break;
                case "hps":
                case "del":
                    Domain.EstimasiMaterial_Rev1 esm = new Domain.EstimasiMaterial_Rev1();
                    Domain.EstimasiMaterial_Rev1 mp2 = mpp.RetrieveEstimasiMaterial(int.Parse(txtProjectID.Value), false, ID);
                    esm.ID = mp2.ID;
                    esm.RowStatus = -2;
                    esm.Jumlah = mp2.Jumlah;
                    esm.Harga = mp2.Harga;
                    esm.Schedule = mp2.Schedule;
                    int result = mpp.Update(esm, true);
                    LoadEstimasiMaterial(int.Parse(txtProjectID.Value), true);
                    break;
                    //case "hps":
                    //    ArrayList arrData = (ArrayList)Session["Material"];
                    //    arrData.RemoveAt(ID);
                    //    e.Item.Visible = false;
                    //    Session["Material"] = arrData;
                    //break;
            }
        }
        protected void btnList_Click(object sender, EventArgs e)
        {
            Response.Redirect("LapImprovement.aspx?p=2");
        }

        public static void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);

        }
    }
}