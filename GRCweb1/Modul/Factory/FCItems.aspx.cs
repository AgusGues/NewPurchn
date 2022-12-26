using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;
using Factory;
using System.Data;

namespace GRCweb1.Modul.Factory
{
    public partial class FCItems : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadKodeItem();
                LoadKodeSisi();
                LoadItem();
                LoadGroupM();
            }
        }
        private void LoadGroupM()
        {
            ArrayList arrGroupM = new ArrayList();
            T3_GroupsFacade groupFacade = new T3_GroupsFacade();
            arrGroupM = groupFacade.Retrieve();
            try
            {
                foreach (T3_Groups groups in arrGroupM)
                {
                    RBList.Items.Add(new ListItem(groups.Groups, groups.ID.ToString()));
                }
            }
            catch { }
        }
        protected void LoadItem()
        {

            ArrayList arrItem = new ArrayList();
            FC_ItemsFacade pf = new FC_ItemsFacade();
            arrItem = pf.RetrieveByItemTypeID(int.Parse(ddlProses.SelectedValue));
            GridView1.DataSource = arrItem;
            GridView1.DataBind();
        }
        protected void LoadKodeItem()
        {
            ArrayList arritems = new ArrayList();
            FC_ItemsFacade itemsFacade = new FC_ItemsFacade();
            arritems = itemsFacade.GetKode();
            ddlKode.Items.Add(new ListItem("--Pilih Kode--", "0"));
            foreach (FC_Items items in arritems)
            {
                ddlKode.Items.Add(new ListItem(items.Kode.ToString(), items.Kode.ToString().Trim()));
            }
        }

        protected void LoadKodeSisi()
        {
            ArrayList arritems = new ArrayList();
            FC_ItemsFacade itemsFacade = new FC_ItemsFacade();
            arritems = itemsFacade.GetSisi();
            ddlSisi.Items.Add(new ListItem("--Pilih Sisi--", ""));
            foreach (FC_Items items in arritems)
            {
                ddlSisi.Items.Add(new ListItem(items.Sisi.ToString(), items.Sisi.ToString().Trim()));
            }
        }

        protected void clearform()
        {
            txtTebal.Text = string.Empty;
            txtID.Text = "0";
            txtLebar.Text = string.Empty;
            txtPanjang.Text = string.Empty;
            txtVolume.Text = string.Empty;
            txtPartNo.Text = string.Empty;
            txtNamaItem.Text = string.Empty;
            //ddlJenis.SelectedIndex = 0;
            //ddlKode.SelectedIndex = 0;
            //ddlKode.SelectedIndex = 0;
            //btnUpdate.Disabled = true;
            btnDelete.Enabled = false;
            LoadItem();
        }

        protected void generatePartNo()
        {

            if (txtTebal.Text != string.Empty && txtPanjang.Text != string.Empty && txtLebar.Text != string.Empty)
            {
                decimal tebal = decimal.Parse(txtTebal.Text.Trim());
                decimal Panjang = int.Parse(txtPanjang.Text.Trim());
                decimal Lebar = int.Parse(txtLebar.Text.Trim());
                decimal volume = (tebal / 1000) * (Panjang / 1000) * (Lebar / 1000);
                string strtebal = (Convert.ToDecimal(tebal) * 10).ToString("0").PadLeft(3, '0');
                string strPanjang = (Convert.ToInt16(Panjang)).ToString().PadLeft(4, '0');
                string strLebar = (Convert.ToInt16(Lebar)).ToString().PadLeft(4, '0');
                if (ddlProses.SelectedValue == "3")
                    txtPartNo.Text = ddlKode.SelectedValue + "-" + ddlJenis.SelectedValue + "-" + strtebal + strLebar + strPanjang + ddlSisi.SelectedValue;
                else
                    txtPartNo.Text = ddlKode.SelectedValue + "-1-" + strtebal + txtLebar.Text.Trim() + txtPanjang.Text.Trim();
                txtVolume.Text = volume.ToString();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Add")
            {
                try
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = GridView1.Rows[index];
                    txtID.Text = row.Cells[0].Text;
                    txtNamaItem.Text = row.Cells[1].Text;
                    txtPartNo.Text = row.Cells[2].Text;

                    if (row.Cells[2].Text.Trim().Substring(7, 1) == "-")
                    {
                        ddlProses.SelectedValue = "1";
                        Panel4.Enabled = false;
                    }
                    else
                    {
                        ddlProses.SelectedValue = "3";
                        Panel4.Enabled = true;
                        string jenis = row.Cells[2].Text.Trim().Substring(4, 1);
                        ddlJenis.SelectedValue = jenis;
                    }
                    if (row.Cells[2].Text.Trim().Length >= 18)
                        ddlSisi.SelectedValue = row.Cells[2].Text.Trim().Substring(17, row.Cells[2].Text.Trim().Length - 17);
                    else
                        ddlSisi.SelectedIndex = 0;

                    ddlKode.SelectedValue = row.Cells[3].Text.Trim();
                    txtTebal.Text = row.Cells[4].Text;
                    txtPanjang.Text = row.Cells[5].Text;
                    txtLebar.Text = row.Cells[6].Text;
                    txtVolume.Text = row.Cells[7].Text;

                }
                catch
                {
                }
                btnUpdate.Disabled = false;
                btnDelete.Enabled = true;
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadItem();
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            if (RBList.SelectedIndex < 0)
            {
                DisplayAJAXMessage(this, "Group Marketing belum dipilih");
                return;
            }
            FC_Items Item = new FC_Items();
            FC_ItemsFacade ItemF = new FC_ItemsFacade();
            Item.ItemTypeID = int.Parse(ddlProses.SelectedValue);
            Item.Kode = ddlKode.SelectedValue;
            Item.Tebal = decimal.Parse(txtTebal.Text);
            Item.Panjang = int.Parse(txtPanjang.Text);
            Item.Lebar = int.Parse(txtLebar.Text);
            Item.Volume = decimal.Parse(txtVolume.Text);
            Item.Partno = txtPartNo.Text;
            Item.ItemDesc = txtNamaItem.Text;
            Item.GroupID = int.Parse(RBList.SelectedValue);

            if (int.Parse(txtID.Text) == 0)
            {
                if (txtID.Text != string.Empty)
                {
                    int ada = ItemF.Check(txtPartNo.Text);
                    if (ada == 0)
                    {
                        if (txtPartNo.Text != string.Empty && txtTebal.Text != string.Empty && txtPanjang.Text != string.Empty && txtLebar.Text != string.Empty)
                        {

                            ItemF.Insert(Item);
                        }
                        else
                        {
                            DisplayAJAXMessage(this, "Input data belum lengkap");
                        }
                    }
                    else
                        DisplayAJAXMessage(this, "Data Item : " + txtPartNo.Text + " sudah tersedia");
                }
            }
            else
            {
                if (txtID.Text != string.Empty)
                {
                    Item.ID = int.Parse(txtID.Text);
                    int ada = ItemF.Check(txtPartNo.Text);
                    if (ada == 0)
                    {
                        ItemF.Update(Item);
                    }
                    else
                        DisplayAJAXMessage(this, "Data Item : " + txtPartNo.Text + " sudah tersedia");
                }
            }
            clearform();
        }

        protected void btnDelete_ServerClick(object sender, EventArgs e)
        {

        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            FC_Items Item = new FC_Items();
            ArrayList arrItem = new ArrayList();
            FC_ItemsFacade pf = new FC_ItemsFacade();
            arrItem = pf.RetrieveByPartNo1(txtSearch.Text);
            //int test=arrItem.
            if (arrItem.Count > 0)
            {
                GridView1.DataSource = arrItem;
                GridView1.DataBind();
            }
            else
                DisplayAJAXMessage(this, "Data tidak ditemukan");
            //clearform();
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearform();
            txtID.Text = "0";
            btnUpdate.Disabled = false;
            btnDelete.Enabled = false;
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            FC_Items Item = new FC_Items();
            FC_ItemsFacade ItemF = new FC_ItemsFacade();
            if (int.Parse(txtID.Text) != 0)
            {
                Item.ID = int.Parse(txtID.Text);
                ItemF.Delete(Item);
            }
            clearform();
        }

        protected void ddlProses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProses.SelectedValue == "1")
            {
                Panel4.Enabled = false;
            }
            else
                Panel4.Enabled = true;
            clearform();
            generatePartNo();
        }

        protected void ddlKode_SelectedIndexChanged(object sender, EventArgs e)
        {
            generatePartNo();
            ddlKode.Focus();
        }
        protected void ddlJenis_SelectedIndexChanged(object sender, EventArgs e)
        {
            generatePartNo();
            ddlJenis.Focus();
        }
        protected void ddlSisi_SelectedIndexChanged(object sender, EventArgs e)
        {
            generatePartNo();
            txtTebal.Focus();
        }

        protected void txtTebal_TextChanged(object sender, EventArgs e)
        {
            generatePartNo();
            txtLebar.Focus();
        }

        protected void txtLebar_TextChanged(object sender, EventArgs e)
        {
            generatePartNo();
            txtPanjang.Focus();

        }
        protected void txtPanjang_TextChanged(object sender, EventArgs e)
        {
            generatePartNo();
            txtNamaItem.Focus();
        }

    }
}