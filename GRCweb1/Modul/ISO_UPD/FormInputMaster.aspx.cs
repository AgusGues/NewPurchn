using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;

namespace GRCweb1.Modul.ISO_UPD
{
    public partial class FormInputMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadJenisUPD();
                btnUlang.Disabled = true;
            }

        }

        private void LoadJenisUPD()
        {
            ArrayList arrIsoMasterD = new ArrayList();
            ISO_UpdMasterDocFacade masterDFacade = new ISO_UpdMasterDocFacade();
            arrIsoMasterD = masterDFacade.RetrieveJenis();
            ddlJenis.Items.Clear();
            ddlJenis.Items.Add(new ListItem("---- Pilih ----", "0"));
            foreach (ISO_UpdMasterDoc jenis in arrIsoMasterD)
            {
                ddlJenis.Items.Add(new ListItem(jenis.DocCategory, jenis.ID.ToString()));
            }
        }

        protected void ddlJenisChanged(object sender, EventArgs e)
        {
            //string IDJenis = ddlJenis.SelectedItem.ToString();
            //string Jenis = Session["Jenis"].ToString();
            ISO_UpdDMD D1 = new ISO_UpdDMD();
            ISO_UPD2Facade F1 = new ISO_UPD2Facade();
            int Type = F1.CekType(Convert.ToInt32(ddlJenis.SelectedValue));
            AutoCompleteExtender4.ContextKey = ddlJenis.SelectedValue;
            //AutoCompleteExtender5.ContextKey = Type.ToString();


            Session["tipe"] = Type;
            if (Type == 1)
            {
                LabelCek.Visible = true; LabelCek.Text = "Cek Nomor Dokumen"; txtD.Visible = true;
                PanelBiasa.Visible = true;
                PanelKhusus.Visible = false;
            }
            else if (Type == 2)
            {
                PanelKhusus.Visible = true;
                PanelBiasa.Visible = false;
            }
        }

        private void LoadDept()
        {
            ArrayList arrDept = new ArrayList();
            ISO_UpdMasterDocFacade updDFacade = new ISO_UpdMasterDocFacade();
            arrDept = updDFacade.RetrieveDeptUPD();
            ddlDept.Items.Clear();
            ddlDept.Items.Add(new ListItem("---- Pilih ----"));
            foreach (ISO_UpdMasterDoc masterDD in arrDept)
            {
                ddlDept.Items.Add(new ListItem(masterDD.namaDept, masterDD.idDept.ToString()));
            }
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            int jenis = int.Parse(ddlJenis.SelectedValue);

            ISO_Upd isoD1 = new ISO_Upd();
            ISO_UPD2Facade isoFacade1 = new ISO_UPD2Facade();
            isoD1 = isoFacade1.RetrieveTipe(jenis);

            ISO_Upd isoD = new ISO_Upd();
            ISO_UPD2Facade isoFacade = new ISO_UPD2Facade();

            isoD = isoFacade.RetrieveMasterDokumen(txtD.Text, jenis);

            if (txtD.Text == "")
            {
                DisplayAJAXMessage(this, " Nomor Dokumen harus diisi ");
                return;
            }

            if (isoD.NoDokumen == string.Empty)
            {
                LabelKet.Visible = true;
                LabelKet.Text = "Nomor dokumen tidak ada di database";
                PanelFormInput.Visible = true;
                txtNO.Text = txtD.Text;
                txtTipe.Text = isoD1.Type.ToString();
                LoadDept();
                btnSearch.Disabled = true;
                ddlJenis.Enabled = false;
                txtD.Enabled = false;
                btnUlang.Disabled = false;

            }
            else if (isoD.NoDokumen != string.Empty && isoD.Aktif == "2")
            {
                LabelKet.Visible = true;
                LabelKet.Text = "Nomor dokumen sudah terdistribusikan";
                ddlJenis.Enabled = false;
            }
            else if (isoD.NoDokumen != string.Empty && isoD.Aktif == "1")
            {
                LabelKet.Visible = true;
                LabelKet.Text = "Nomor dokumen sudah ada di Database tapi blm terdistribusikan";
                ddlJenis.Enabled = false; btnUlang.Disabled = false;
            }
        }

        protected void btnSearch2_ServerClick(object sender, EventArgs e)
        {
            int jenis = int.Parse(ddlJenis.SelectedValue);
            Users users = (Users)Session["Users"];
            ISO_Upd isoD11 = new ISO_Upd();
            ISO_UPD2Facade isoFacade11 = new ISO_UPD2Facade();
            isoD11 = isoFacade11.RetrieveTipe(jenis);

            ISO_Upd isoD1 = new ISO_Upd();
            ISO_UPD2Facade isoFacade1 = new ISO_UPD2Facade();

            isoD1 = isoFacade1.RetrieveMasterDokumenKhusus(txtNamaKhusus.Text, jenis);

            if (txtNamaKhusus.Text == "")
            {
                DisplayAJAXMessage(this, "Nomor Dokumen harus diisi ");
                return;
            }

            if (isoD1.NamaDokumen == null || isoD1.NamaDokumen == "")
            {
                Label3.Visible = true;
                Label3.Text = "Nama dokumen tidak ada di database";
                PanelFormInput.Visible = true;
                //txtNama.Text = isoD1.NamaDokumen.Trim();
                //txtNO.Text = isoD1.NoDokumen.Trim();

                if (ddlJenis.SelectedValue == "10" && users.UnitKerjaID == 7)
                {
                    txtNO.Text = "HRD/K/SOD/46/09/R0";
                }
                else if (ddlJenis.SelectedValue == "10" && users.UnitKerjaID == 1)
                {
                    txtNO.Text = "HRD/SOD/46/09/R0";
                }
                else if (ddlJenis.SelectedValue == "11" && users.UnitKerjaID == 1)
                {
                    txtNO.Text = "HRD/JD/44/09/R0";
                }
                else if (ddlJenis.SelectedValue == "11" && users.UnitKerjaID == 7)
                {
                    txtNO.Text = "HRD/K/JD/44/09/R0";
                }

                txtRevisi.Text = "0";
                txtNama.Text = txtNamaKhusus.Text.Trim().ToUpper();
                txtTipe.Text = "2";
                LoadDept();
                btnSearch.Disabled = true;
                ddlJenis.Enabled = false;
                txtD.Enabled = false;
                btnUlang.Disabled = false;
                txtNO.Enabled = true;
                txtNO.ReadOnly = false;

            }
            else if (isoD1.NamaDokumen != null && isoD1.Aktif == "2")
            {
                Label3.Visible = true;
                Label3.Text = "Dokumen sudah terdistribusikan";
                ddlJenis.Enabled = false;
            }
            else if (isoD1.NamaDokumen != null && isoD1.Aktif == "1")
            {
                Label3.Visible = true;
                Label3.Text = "Dokumen sudah ada di Database tapi blm terdistribusikan";
                ddlJenis.Enabled = false; btnUlang.Disabled = false;
            }
        }

        protected void btnUlang2_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("FormInputMaster.aspx");
        }

        protected void btnUlang_ServerClick(object sender, EventArgs e)
        { Response.Redirect("FormInputMaster.aspx"); }

        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            ISO_Upd isoUPD = new ISO_Upd();
            ISO_UPD2Facade isoUPDFacade = new ISO_UPD2Facade();
            Users users = (Users)Session["Users"];



            int intResult = 0;
            isoUPD.NoDokumen = txtNO.Text;
            isoUPD.NamaDokumen = txtNama.Text;
            isoUPD.RevisiNo = txtRevisi.Text;
            isoUPD.TglBerlaku = DateTime.Parse(txtMulai.Text);
            isoUPD.DeptID = int.Parse(ddlDept.SelectedValue);
            isoUPD.CreatedBy = users.UserName;
            isoUPD.CategoryUPD = int.Parse(ddlJenis.SelectedValue);
            isoUPD.Type = int.Parse(txtTipe.Text);
            isoUPD.PlantID = users.UnitKerjaID;

            intResult = isoUPDFacade.InsertDokumen(isoUPD);
            if (intResult > -1)
            {
                txtNO.Text = string.Empty;
                txtNO.Enabled = false;
                txtNama.Text = string.Empty;
                txtNama.Enabled = false;
                txtRevisi.Text = string.Empty;
                txtRevisi.Enabled = false;
                txtMulai.Text = string.Empty;
                txtMulai.Enabled = false;
                ddlDept.Enabled = false;
                LabelSave.Visible = true;
                LabelSave.Text = "Data berhasil di Simpan !!";
                btnUlang.Disabled = false;
                btnSave.Disabled = true;
            }

        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
    }
}