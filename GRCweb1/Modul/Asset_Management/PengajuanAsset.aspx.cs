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
using DataAccessLayer;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace GRCweb1.Modul.Asset_Management
{
    public partial class PengajuanAsset : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Users user = (Users)Session["Users"];
                RBAssetUtama.Checked = true; RBAssetKomponen.Checked = false;
                LoadGroupAsset();
                LoadDeptAsset();
                LoadSatuan();
                LoadLokasi();
                LoadJenisAsset();
                ddlSubClassAsset.Enabled = false; ddlDeptID.Enabled = false; ddlGroupAsset.Enabled = false;
                txtKodeIndukAsset.ReadOnly = true;
                txtKodeIndukAsset.Text = user.UnitKerjaID + ".0.000.0.000";
                btnSave.Visible = true; btnSave.Disabled = true;
                //LoadDataGrid(LoadData());
                btnNewClass.Enabled = true; btnNewAsset.Enabled = true;
                ddlSatuan.Enabled = false; txtUkuran.Enabled = false; txtMerk.Enabled = false; txtJenis.Enabled = false; txtType.Enabled = false;
                LokasiID.Enabled = false; txtPartNumber.Enabled = false; txtLeadTime.Enabled = false;

                LabelNamaAsset.Visible = true; LabelNamaAsset.Text = "Nama Asset";

                LoadDataGrid(LoadData());

            }
        }
        private void LoadSatuan()
        {
            ArrayList arrUOM = new ArrayList();
            UOMFacade uOMFacade = new UOMFacade();
            arrUOM = uOMFacade.Retrieve1();
            ddlSatuan.Items.Clear();
            ddlSatuan.Items.Add(new ListItem("-- Pilih Satuan --", "0"));
            foreach (UOM uOM in arrUOM)
            {
                ddlSatuan.Items.Add(new ListItem(uOM.UOMDesc, uOM.ID.ToString()));
            }

            ddlSatuan2.Items.Clear();
            ddlSatuan2.Items.Add(new ListItem("-- Pilih Satuan --", "0"));
            foreach (UOM uOM in arrUOM)
            {
                ddlSatuan2.Items.Add(new ListItem(uOM.UOMDesc, uOM.ID.ToString()));
            }
        }
        private void LoadSatuan0()
        {
            ArrayList arrUOM = new ArrayList();
            UOMFacade uOMFacade = new UOMFacade();
            arrUOM = uOMFacade.Retrieve1();
            ddlSatuan0.Items.Clear();
            ddlSatuan0.Items.Add(new ListItem("-- Pilih Satuan --", "0"));
            foreach (UOM uOM in arrUOM)
            {
                ddlSatuan0.Items.Add(new ListItem(uOM.UOMDesc, uOM.ID.ToString()));
            }

            ddlSatuan2.Items.Clear();
            ddlSatuan2.Items.Add(new ListItem("-- Pilih Satuan --", "0"));
            foreach (UOM uOM in arrUOM)
            {
                ddlSatuan2.Items.Add(new ListItem(uOM.UOMDesc, uOM.ID.ToString()));
            }
        }
        private void LoadLokasi()
        {
            ArrayList arrGroup = new ArrayList();
            FacadeAsset facadeGroup = new FacadeAsset();
            arrGroup = facadeGroup.RetrieveLokasi();
            LokasiID.Items.Clear();
            LokasiID.Items.Add(new ListItem("-- Pilih Lokasi --", "0"));
            foreach (DomainPengajuanAsset NewAsset in arrGroup)
            {
                LokasiID.Items.Add(new ListItem(NewAsset.NamaLokasi, NewAsset.ID.ToString()));
            }
        }
        private void LoadLokasi0()
        {
            ArrayList arrGroup = new ArrayList();
            FacadeAsset facadeGroup = new FacadeAsset();
            arrGroup = facadeGroup.RetrieveLokasi();
            LokasiID0.Items.Clear();
            LokasiID0.Items.Add(new ListItem("-- Pilih Lokasi --", "0"));
            foreach (DomainPengajuanAsset NewAsset in arrGroup)
            {
                LokasiID0.Items.Add(new ListItem(NewAsset.NamaLokasi, NewAsset.ID.ToString()));
            }
        }
        private void LoadDataGridAsset(ArrayList arrAsset)
        {
            //this.GridView1.DataSource = arrAsset;
            //this.GridView1.DataBind();
        }

        private void LoadGroupAsset()
        {
            //txtClassAsset.ReadOnly = true;
            ArrayList arrGroup = new ArrayList();
            FacadeAsset facadeGroup = new FacadeAsset();
            arrGroup = facadeGroup.RetrieveGroupAsset();
            ddlGroupAsset.Items.Clear();
            ddlGroupAsset.Items.Add(new ListItem("-- Pilih Group --", "0"));
            foreach (DomainPengajuanAsset NewAsset in arrGroup)
            {
                ddlGroupAsset.Items.Add(new ListItem(NewAsset.NamaGroup, NewAsset.KodeGroup.ToString()));
            }
        }

        protected void ddlGroupAsset_Change(object sender, EventArgs e)
        {
            // LoadClassAsset();
            AutoCompleteExtender3.ContextKey = ddlGroupAsset.SelectedValue;
            Users user = (Users)Session["Users"]; txtClassAsset.Enabled = true;
            //txtKodeIndukAsset.Text =  ViewState["ddlDeptID"].ToString()+"."+ddlGroupAsset.SelectedValue.ToString() + ".00.000.000";
            txtKodeIndukAsset.Text = user.UnitKerjaID.ToString().Trim() + "." + ddlGroupAsset.SelectedValue.ToString().Trim() + "." + "000" + "." + ddlDeptID.SelectedValue.ToString().Trim() + "." + "000";
        }

        protected void ddlIndukAsset_Change(object sender, EventArgs e)
        {

            //string urut = string.Empty;
            //FacadeAsset facadeKodePlant = new FacadeAsset();

            //DomainPengajuanAsset domain1 = facadeKodePlant.GetLastNumKomponen( ddlIndukAsset.SelectedItem.ToString().Substring(0,15) );
            //if (int.Parse(domain1.ItemKode) > 0)
            //    urut = (int.Parse(domain1.ItemKode) + 1).ToString().PadLeft(3, '0');
            //else
            //    urut = "001";


            //txtKomponenAsset.Text = "06."+urut+"-" + ddlIndukAsset.SelectedItem.ToString().Trim().Substring(0,15);

            //LoadDataGridKomponenAsset(LoadDataKomponenAsset());
            txtNamaKomponenAsset.Focus();
            txtNamaKomponenAsset.Enabled = true;
            LoadDataUrutan();
        }

        protected void LoadDataUrutan()
        {
            Users users = (Users)Session["Users"];
            string query = string.Empty;
            if (users.UnitKerjaID.ToString().Length == 1)
            {
                query = "13";
            }
            else
            {
                query = "14";
            }
            string urut = string.Empty;
            FacadeAsset facadeKodePlant = new FacadeAsset();
            DomainPengajuanAsset domain1 = facadeKodePlant.GetLastNumKomponen(ddlIndukAsset.SelectedItem.ToString().Substring(0, Convert.ToInt32(query)));
            if (int.Parse(domain1.ItemKode) > 0)
                urut = (int.Parse(domain1.ItemKode) + 1).ToString().PadLeft(3, '0');
            else
                urut = "001";


            txtKomponenAsset.Text = "06." + urut + "-" + ddlIndukAsset.SelectedItem.ToString().Trim().Substring(0, Convert.ToInt32(query));

            LoadDataGridKomponenAsset(LoadDataKomponenAsset());
        }
        protected void txtLeadTime_Change(object sender, EventArgs e)
        {
            txtType.Enabled = true; txtType.Focus();
            txtUkuran.Enabled = true;
            txtMerk.Enabled = true;
            txtJenis.Enabled = true;
            txtPartNumber.Enabled = true;
            ddlSatuan.Enabled = true;


        }

        protected void txtLeadTime2_Change(object sender, EventArgs e)
        {
            ddlSatuan2.Focus();
        }
        //protected void txtType_Change(object sender, EventArgs e)
        //{
        //    txtUkuran.Enabled = true; txtUkuran.Focus();     
        //}
        //protected void txtUkuran_Change(object sender, EventArgs e)
        //{
        //    txtMerk.Enabled = true; txtMerk.Focus();
        //}
        //protected void txtMerk_Change(object sender, EventArgs e)
        //{
        //    txtJenis.Enabled = true; txtJenis.Focus();
        //}
        //protected void txtJenis_Change(object sender, EventArgs e)
        //{
        //    txtPartNumber.Enabled = true; txtPartNumber.Focus();
        //}   
        //protected void txtPartNumber_Change(object sender, EventArgs e)
        //{
        //    ddlSatuan.Enabled = true; 
        //}

        protected void txtClassAsset_Change(object sender, EventArgs e)
        {
            Users user = (Users)Session["Users"];

            string KodeDept = ddlDeptID.SelectedValue.ToString().Trim();
            string GroupAsset = ddlGroupAsset.SelectedValue.ToString().Trim();
            ddlSubClassAsset.Enabled = true; txtCLassNew.Enabled = true;


            DomainPengajuanAsset DCekClassID = new DomainPengajuanAsset();
            FacadeAsset FCekClassID = new FacadeAsset();
            DCekClassID = FCekClassID.RetrieveClassID(txtClassAsset.Text.Trim(), GroupAsset);
            Session["ClassID"] = DCekClassID.ClassID;


            DomainPengajuanAsset DCekKode2 = new DomainPengajuanAsset();
            FacadeAsset FCekKode2 = new FacadeAsset();
            DCekKode2 = FCekKode2.RetrieveKodeClass(txtClassAsset.Text.Trim(), ddlGroupAsset.SelectedValue);
            Session["KodeClass"] = DCekKode2.KodeClass;

            if (DCekKode2.KodeClass == null)
            {
                btnNewClass.Enabled = true;
                ddlSubClassAsset.Enabled = false;
                ddlSatuan.Enabled = false;
                LokasiID.Enabled = false;
            }
            else if (DCekKode2.KodeClass != string.Empty)
            {
                if (DCekKode2.KodeClass.Length == 1)
                {
                    txtKodeIndukAsset.Text = user.UnitKerjaID.ToString().Trim() + "." + GroupAsset + "." + "00" + DCekKode2.KodeClass.Trim() + "." + KodeDept + "." + "000";
                }
                else if (DCekKode2.KodeClass.Length == 2)
                {
                    txtKodeIndukAsset.Text = user.UnitKerjaID.ToString().Trim() + "." + GroupAsset + "." + "0" + DCekKode2.KodeClass.Trim() + "." + KodeDept + "." + "000";
                }
            }


            if (txtClassAsset.Text != string.Empty)
            {
                LoadDataAsset();
            }
        }

        protected void txtCLassNew_Change(object sender, EventArgs e)
        {
            Users user = (Users)Session["Users"]; string query = string.Empty;
            string GroupAsset = ddlGroupAsset.SelectedValue.ToString().Trim();
            txtLeadTime.Focus();

            if (user.UnitKerjaID.ToString().Length == 1)
            {
                query = "10";
            }
            else
            {
                query = "11";
            }

            //DomainPengajuanAsset DCekSubClassID = new DomainPengajuanAsset();
            //FacadeAsset FCekSubClassID = new FacadeAsset();
            //DCekSubClassID = FCekSubClassID.RetrieveIDSubClass(ddlSubClassAsset.SelectedItem.ToString().Trim(),txtClassAsset.Text.Trim(),ddlSubClassAsset.SelectedItem.ToString().Trim(), GroupAsset);
            //Session["SubClassID"] = DCekSubClassID.SubClassID;

            if (ddlJenis.SelectedItem.ToString().Trim() == "BERKOMPONEN")
            {
                DomainPengajuanAsset DCekSubClass = new DomainPengajuanAsset();
                FacadeAsset FCekSubClass = new FacadeAsset();
                DCekSubClass = FCekSubClass.RetrieveSubClassNew(txtCLassNew.Text.Trim(), GroupAsset);

                //if (DCekSubClass.NamaSubClass == null)
                //{
                //    btnNewAsset.Enabled = true;
                //    btnSave.Disabled = true;
                //    DisplayAJAXMessage(this, "Nama Asset belum ada !! ");
                //    return;
                //}
                //else
                //{

                //}
            }
            else if (ddlJenis.SelectedItem.ToString().Trim() == "TUNGGAL")
            {
                //DomainPengajuanAsset DCekSubClassID = new DomainPengajuanAsset();
                //FacadeAsset FCekSubClassID = new FacadeAsset();
                //DCekSubClassID = FCekSubClassID.RetrieveIDSubClass(ddlSubClassAsset.SelectedItem.ToString().Trim(), txtClassAsset.Text.Trim(), ddlSubClassAsset.SelectedItem.ToString().Trim(), GroupAsset);
                //Session["SubClassID"] = DCekSubClassID.SubClassID;
                //Session["KodeSubClass"] = DCekSubClassID.KodeSubClass;

                DomainPengajuanAsset DCekAssetTunggal = new DomainPengajuanAsset();
                FacadeAsset FCekAssetTunggal = new FacadeAsset();
                DCekAssetTunggal = FCekAssetTunggal.RetrieveAssetTunggal(txtCLassNew.Text.Trim(), GroupAsset);



                if (DCekAssetTunggal.NamaAsset != null)
                {
                    txtKodeIndukAsset.Text = txtKodeIndukAsset.Text.Substring(0, 10);
                    btnNewAsset.Enabled = false;
                    btnSave.Disabled = false;
                    DisplayAJAXMessage(this, "Nama Asset sudah ada !! ");
                    return;
                }
                else if (DCekAssetTunggal.NamaAsset == null)
                {


                    txtLeadTime.Enabled = true; txtType.Enabled = true; txtUkuran.Enabled = true; txtPartNumber.Enabled = true;
                    txtMerk.Enabled = true; txtJenis.Enabled = true; ddlSatuan.Enabled = true; LokasiID.Enabled = true; txtType.Focus();
                    txtLeadTime.Enabled = true;

                    //txtLeadTime.Enabled = true;
                    DomainPengajuanAsset DCekAssetTNoUrut0 = new DomainPengajuanAsset();
                    FacadeAsset FCekAssetTNoUrut0 = new FacadeAsset();
                    //DCekAssetTNoUrut0 = FCekAssetTNoUrut0.RetrieveAssetTNoUrut0(txtCLassNew.Text.Trim(), txtClassAsset.Text.Trim(), ddlSubClassAsset.SelectedItem.ToString().Trim(), GroupAsset);
                    DCekAssetTNoUrut0 = FCekAssetTNoUrut0.RetrieveAssetTNoUrut0(txtCLassNew.Text.Trim(), txtClassAsset.Text.Trim(), GroupAsset);

                    DomainPengajuanAsset DCekAssetTNoUrut = new DomainPengajuanAsset();
                    FacadeAsset FCekAssetTNoUrut = new FacadeAsset();
                    //DCekAssetTNoUrut = FCekAssetTNoUrut.RetrieveAssetTNoUrut(txtCLassNew.Text.Trim(),txtClassAsset.Text.Trim(),ddlSubClassAsset.SelectedItem.ToString().Trim(), GroupAsset);
                    DCekAssetTNoUrut = FCekAssetTNoUrut.RetrieveAssetTNoUrut(txtCLassNew.Text.Trim(), txtClassAsset.Text.Trim(), GroupAsset);

                    if (DCekAssetTNoUrut0.Total > DCekAssetTNoUrut.Total)
                    {
                        int NoUrut = DCekAssetTNoUrut0.Total + 1; Session["NoUrut"] = NoUrut;
                    }
                    else if (DCekAssetTNoUrut0.Total <= DCekAssetTNoUrut.Total)
                    {
                        int NoUrut = DCekAssetTNoUrut.Total + 1; Session["NoUrut"] = NoUrut;
                    }

                    int NoUrutan = Convert.ToInt32(Session["NoUrut"]);
                    string NoUrutLagi = string.Empty;
                    //string KodeSubClass = Session["KodeSubClass"].ToString();

                    if (NoUrutan > 0)
                    {
                        if (NoUrutan.ToString().Length == 1)
                        {
                            NoUrutLagi = "00" + NoUrutan.ToString();
                        }
                        else if (NoUrutan.ToString().Length == 2)
                        {
                            NoUrutLagi = "0" + NoUrutan.ToString();
                        }
                        else if (NoUrutan.ToString().Length == 3)
                        {
                            NoUrutLagi = NoUrutan.ToString();
                        }
                    }


                    if (NoUrutan.ToString().Length > 0)
                    {
                        //txtKodeIndukAsset.Text = txtKodeIndukAsset.Text.Substring(0, Convert.ToInt32(query)) + KodeSubClass.ToString().Trim();
                        txtKodeIndukAsset.Text = txtKodeIndukAsset.Text.Substring(0, Convert.ToInt32(query)) + NoUrutLagi.ToString().Trim();
                    }

                    btnSave.Disabled = false;

                }
            }


        }

        protected void txtNamaKomponenAsset_Change(object sender, EventArgs e)
        {
            //string urut = string.Empty;
            //FacadeAsset facadeKodePlant2 = new FacadeAsset();

            //int LastNumber = facadeKodePlant2.GetLastNumItemKomponen(ddlIndukAsset.SelectedItem.ToString().Substring(0, 15));
            //int Number = 1 + LastNumber;
            //if (Number < 10)
            //{
            //    string NoUrutan = "00"+Number.ToString().Trim(); Session["NoUrutan"] = NoUrutan.Trim();
            //}
            //else if (Number > 9 && Number < 100)
            //{
            //    string NoUrutan = "0"+Number.ToString().Trim(); Session["NoUrutan"] = NoUrutan.Trim();
            //}
            //else if (Number >99)
            //{
            //    string NoUrutan = Number.ToString().Trim(); Session["NoUrutan"] = NoUrutan.Trim();
            //}

            //txtKomponenAsset.Text = "06." + Session["NoUrutan"].ToString().Trim() + "-" + ddlIndukAsset.SelectedItem.ToString().Trim().Substring(0, 15);

            //LoadDataGridKomponenAsset(LoadDataKomponenAsset());

            ddlSatuan2.Enabled = true; txtLeadTime2.Focus();
            LoadDataKomponen();
        }

        protected void LoadDataKomponen()
        {
            string urut = string.Empty; string query = string.Empty;
            Users users = (Users)Session["Users"];
            if (users.UnitKerjaID.ToString().Length == 1)
            {
                query = "13";
            }
            else
            {
                query = "14";
            }
            FacadeAsset facadeKodePlant2 = new FacadeAsset();

            int LastNumber = facadeKodePlant2.GetLastNumItemKomponen(ddlIndukAsset.SelectedItem.ToString().Substring(0, Convert.ToInt32(query)));
            int Number = 1 + LastNumber;
            if (Number < 10)
            {
                string NoUrutan = "00" + Number.ToString().Trim(); Session["NoUrutan"] = NoUrutan.Trim();
            }
            else if (Number > 9 && Number < 100)
            {
                string NoUrutan = "0" + Number.ToString().Trim(); Session["NoUrutan"] = NoUrutan.Trim();
            }
            else if (Number > 99)
            {
                string NoUrutan = Number.ToString().Trim(); Session["NoUrutan"] = NoUrutan.Trim();
            }

            txtKomponenAsset.Text = "06." + Session["NoUrutan"].ToString().Trim() + "-" + ddlIndukAsset.SelectedItem.ToString().Trim().Substring(0, Convert.ToInt32(query));

            LoadDataGridKomponenAsset(LoadDataKomponenAsset());
        }


        protected void ddlClasAsset_Change(object sender, EventArgs e)
        {
            //LoadSubClassAsset();
            //LoadDeptAsset();        
            Users user = (Users)Session["Users"];
            DomainPengajuanAsset domain1 = new DomainPengajuanAsset();
            FacadeAsset facade1 = new FacadeAsset();
            domain1 = facade1.RetrieveKodeClass(txtClassAsset.Text.Trim(), ddlGroupAsset.SelectedValue.ToString());
            Session["KodeClass"] = domain1.KodeClass.ToString();
            Session["ClassID"] = domain1.ClassID;
            //btnNewClass.Enabled = true;
            if (domain1.KodeClass != string.Empty)
            {
                //txtKodeIndukAsset.Text = ViewState["ddlDeptID"].ToString() +"." + ddlGroupAsset.SelectedValue.ToString() + "." + domain1.KodeClass.ToString() + "." + "000.000";
                txtKodeIndukAsset.Text = user.UnitKerjaID.ToString().Trim() + "." + ddlGroupAsset.SelectedValue.ToString().Trim() + domain1.KodeClass.ToString().Trim() + "." + ddlDeptID.SelectedValue.ToString().Trim() + "." + "001";
            }
            else
            {

                DisplayAJAXMessage(this, " Class" + " " + ":" + " " + txtClassAsset.Text.Trim() + " " + " belum ada ! ");
                return;

            }
            //if (ddlClasAsset.SelectedIndex == 0)
            //    txtSubCLass.Text = string.Empty;
            //else
            //    txtSubCLass.Enabled = true;

        }

        protected void ddlJenis_Change(object sender, EventArgs e)
        {
            ddlDeptID.Enabled = true;

            LoadDeptAsset();
            LoadGroupAsset();
            ddlGroupAsset.Enabled = false;

            if (ddlJenis.SelectedItem.ToString() == "TUNGGAL")
            {
                LabelNamaAsset.Visible = false; LabelNamaAsset.Text = "Sub-Class Asset"; txtClassAsset.Enabled = false; PanelAssetTunggalDetail.Visible = true;
                LabelSatuan.Visible = false; LabelKomaSatuan.Visible = false; ddlSatuan0.Visible = false;
                LabelLokasi.Visible = false; LabelKomaLokasi.Visible = false; LokasiID0.Visible = false;
                ddlSubClassAsset.Visible = false;
                LoadLokasi0(); LoadSatuan0();
            }
            else
            {
                LabelNamaAsset.Visible = true; LabelNamaAsset.Text = "Nama Asset"; txtClassAsset.Enabled = false; PanelAssetTunggalDetail.Visible = false;
                //LabelSatuan.Visible = false; LabelKomaSatuan.Visible = false; ddlSatuan0.Visible = false;
                LabelSatuan.Visible = true; LabelSatuan.Text = "Satuan"; LabelKomaSatuan.Visible = true; LabelKomaSatuan.Text = ":";
                ddlSatuan0.Visible = true;
                LabelLokasi.Visible = true; LabelLokasi.Text = "Lokasi"; LabelKomaLokasi.Visible = true; LabelKomaLokasi.Text = ":"; LokasiID0.Visible = true;
            }

            //txtClassAsset.Text = "KETIK NAMA CLASS DISINI"; txtClassAsset.Enabled = false;

            ddlSubClassAsset.Items.Clear();
            txtCLassNew.Enabled = false; txtCLassNew.Text = string.Empty;

            if (ddlJenis.SelectedItem.ToString().Trim() == "BERKOMPONEN")
            {
                LoadDataGrid(LoadData());
            }
            else if (ddlJenis.SelectedItem.ToString().Trim() == "TUNGGAL")
            {
                LoadDataGrid(LoadDataAssetTunggal());
            }

        }
        protected void ddlSatuan_Change(object sender, EventArgs e)
        {
            LokasiID.Enabled = true;
        }

        protected void ddlSatuan0_Change(object sender, EventArgs e)
        {
            LokasiID.Enabled = true;
        }

        protected void ddlSubClassAsset_Change(object sender, EventArgs e)
        {
            Users user = (Users)Session["Users"];
            txtCLassNew.Enabled = true; ddlSubClassAsset.Enabled = false; txtCLassNew.Focus();
            txtCLassNew.Text = ddlSubClassAsset.SelectedItem.ToString();
            btnNewClass.Enabled = false;
            DomainPengajuanAsset domainNoUrut = new DomainPengajuanAsset();
            FacadeAsset facadeNoUrut = new FacadeAsset();
            domainNoUrut = facadeNoUrut.RetrieveNoUrut(ddlSubClassAsset.SelectedItem.ToString().Trim(), ddlGroupAsset.SelectedValue);
            string Kelas = string.Empty;
            if (Session["KodeClass"].ToString().Length == 1)
            {
                Kelas = "00" + Session["KodeClass"].ToString();
            }
            else if (Session["KodeClass"].ToString().Length == 2)
            {
                Kelas = "0" + Session["KodeClass"].ToString();
            }
            else
            {
                Kelas = Session["KodeClass"].ToString();
            }

            //txtKodeIndukAsset.Text = user.UnitKerjaID.ToString().Trim() + "." + ddlGroupAsset.SelectedValue.ToString().Trim() + "." + "0" + Session["KodeClass"].ToString().Trim() + "." + Session["DeptID"].ToString().Trim() + "." + domainNoUrut.NoUrutan.Trim();
            txtKodeIndukAsset.Text = user.UnitKerjaID.ToString().Trim() + "." + ddlGroupAsset.SelectedValue.ToString().Trim() + "." + Kelas + "." + Session["DeptID"].ToString().Trim() + "." + domainNoUrut.NoUrutan.Trim();

            DomainPengajuanAsset DCekKodeAsset = new DomainPengajuanAsset();
            FacadeAsset FCekKodeAsset = new FacadeAsset();
            DCekKodeAsset = FCekKodeAsset.RetrieveKodeAsset(txtKodeIndukAsset.Text.Trim());

            if (DCekKodeAsset.Total == 1 && ddlJenis.SelectedItem.ToString() == "BERKOMPONEN")
            {
                Session["HeadID"] = 1;
                btnSave.Disabled = false; txtLeadTime.Enabled = true; txtLeadTime.Focus();
                //DisplayAJAXMessage(this, "Kode Asset belum ada !! ");
                LoadLokasi0(); LoadSatuan0(); ddlSatuan0.Focus(); txtCLassNew.Enabled = false;
            }
            else if (DCekKodeAsset.Total > 1 && ddlJenis.SelectedItem.ToString() == "BERKOMPONEN")
            {
                Session["HeadID"] = 0;
                btnSave.Disabled = true; txtLeadTime.Enabled = false;
                DisplayAJAXMessage(this, "Nama Asset Master sudah ada !! ");
                LoadDataAsset();
                txtCLassNew.Enabled = false; txtCLassNew.Text = string.Empty; ddlSubClassAsset.Enabled = true; ddlSubClassAsset.Focus();
            }

            if (ddlJenis.SelectedItem.ToString() == "TUNGGAL")
            {
                Session["HeadID"] = 2;
                txtCLassNew.Text = string.Empty;
            }
            AutoCompleteExtender2.ContextKey = txtClassAsset.Text.Trim();

        }

        protected void ddlDeptID_Change(object sender, EventArgs e)
        {
            Users user = (Users)Session["Users"];
            txtKodeIndukAsset.Text = user.UnitKerjaID.ToString().Trim() + "." + "0" + "." + "000" + "." + ddlDeptID.SelectedValue.ToString().Trim() + "." + "000";
            Session["DeptID"] = ddlDeptID.SelectedValue; ddlGroupAsset.Enabled = true;

        }

        protected void ddlDeptID0_Change(object sender, EventArgs e)
        {
            Users user = (Users)Session["Users"];
            if (ddlDeptID0.SelectedValue == "19")
            {
                RBLama.Visible = true; RBBaru.Visible = true;
            }
            else
            {
                RBLama.Visible = false; RBBaru.Visible = false;

            }
            txtKodeIndukAsset.Text = user.UnitKerjaID.ToString().Trim() + "." + "0" + "." + "000" + "." + ddlDeptID.SelectedValue.ToString().Trim() + "." + "000";
            Session["DeptID"] = ddlDeptID.SelectedValue; ddlGroupAsset.Enabled = true;

        }

        private void LoadDeptAsset()
        {
            ddlDeptID.Enabled = true;

            ArrayList arrDept = new ArrayList();
            FacadeAsset facadeDept = new FacadeAsset();
            arrDept = facadeDept.RetrieveDept();
            ddlDeptID.Items.Clear();
            ddlDeptID.Items.Add(new ListItem("-- Pilih Dept --", "0"));

            foreach (DomainPengajuanAsset Dept in arrDept)
            {
                ddlDeptID.Items.Add(new ListItem(Dept.Department, Dept.DeptID.ToString()));
            }

        }

        private void LoadJenisAsset()
        {
            ddlDeptID.Enabled = true;

            ArrayList arrJenis = new ArrayList();
            FacadeAsset facadeJenis = new FacadeAsset();
            ddlJenis.Items.Clear();
            ddlJenis.Items.Add(new ListItem("-- Pilih Jenis --", "0"));

            ddlJenis.Items.Add(new ListItem("TUNGGAL", "1"));
            ddlJenis.Items.Add(new ListItem("BERKOMPONEN", "2"));
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            //clearForm();
        }

        protected void btnNewClass_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~\\Modul\\AssetManagement\\AssetClass.aspx");
        }

        protected void btnNewAsset_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("~\\Modul\\AssetManagement\\AssetSubClass.aspx");
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        { }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {

            //LoadDataGridImprovement(LoadGridByCriteria());
            //Thread.Sleep(100);
            //txtSearch.Text = string.Empty;
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadDataGrid(LoadData());

        }

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            LoadDataGridKomponenAsset(LoadDataKomponenAsset());
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }




        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            //if (PanelItemName.Visible == false)
            //    PanelItemName.Visible = true;
            //else
            //    PanelItemName.Visible = false;
        }

        protected void btnRefresh_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("PengajuanAsset.aspx");
        }



        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];

            string Group = ddlGroupAsset.SelectedValue.ToString().Trim();

            if (ddlJenis.SelectedItem.ToString() == "TUNGGAL")
            {
                if (ddlSatuan.SelectedValue == "0")
                {
                    DisplayAJAXMessage(this, "Satuan Asset yg harus diisi !! ");
                    return;
                }

                if (LokasiID.SelectedValue == "0")
                {
                    DisplayAJAXMessage(this, "Lokasi Asset harus ditentukan !! ");
                    return;
                }
            }
            else if (ddlJenis.SelectedItem.ToString() == "BERKOMPONEN")
            {
                if (ddlSatuan0.SelectedValue == "0")
                {
                    DisplayAJAXMessage(this, "Satuan Asset yg harus diisi !! ");
                    return;
                }

                if (LokasiID0.SelectedValue == "0")
                {
                    DisplayAJAXMessage(this, "Lokasi Asset harus ditentukan !! ");
                    return;
                }
            }

            DomainPengajuanAsset D1 = new DomainPengajuanAsset();
            FacadeAsset F1 = new FacadeAsset();
            D1 = F1.RetrieveSClassID(txtClassAsset.Text.Trim(), ddlSubClassAsset.SelectedItem.ToString().Trim(), Group);

            DomainPengajuanAsset domainSave = new DomainPengajuanAsset();
            FacadeAsset facadeSave = new FacadeAsset();

            domainSave.GroupID = Convert.ToInt32(ddlGroupAsset.SelectedValue);
            domainSave.ClassID = Convert.ToInt32(Session["ClassID"]);
            domainSave.KodeAsset = txtKodeIndukAsset.Text.Trim().ToUpper();
            //domainSave.NamaAsset = txtCLassNew.Text.Trim().ToUpper(); 
            domainSave.CreatedBy = users.UserName.Trim();
            domainSave.ItemKode = txtKodeIndukAsset.Text.Trim().ToUpper();
            domainSave.AssetID = Convert.ToInt32(Session["IDasset"]);
            domainSave.UnitKerjaID = users.UnitKerjaID;
            domainSave.AssetPemilikDept = int.Parse(ddlDeptID.SelectedValue);
            //domainSave.UomID = int.Parse(ddlSatuan.SelectedValue);
            domainSave.TipeAsset = 4; //lihat select * from GroupsPurchn
                                      //domainSave.LokasiID = int.Parse(LokasiID.SelectedValue);
            domainSave.HeadID = Convert.ToInt32(Session["HeadID"]);

            //int LeadTime = Convert.ToInt32(txtLeadTime.Text);
            if (ddlJenis.SelectedItem.ToString() == "TUNGGAL")
            {
                domainSave.LeadTime = (Convert.ToInt32(txtLeadTime.Text) == 0) ? 0 : Convert.ToInt32(txtLeadTime.Text);
                domainSave.ITYPE = txtType.Text.Trim().ToUpper();
                domainSave.Ukuran = txtUkuran.Text.Trim().ToUpper();
                domainSave.Merk = txtMerk.Text.Trim().ToUpper();
                domainSave.Jenis = txtJenis.Text.Trim().ToUpper();
                domainSave.PartNumber = txtPartNumber.Text.Trim().ToUpper();
            }
            else if (ddlJenis.SelectedItem.ToString() == "BERKOMPONEN")
            {
                domainSave.LeadTime = 0;
                domainSave.ITYPE = "-";
                domainSave.Ukuran = "-";
                domainSave.Merk = "-";
                domainSave.Jenis = "-";
                domainSave.PartNumber = "-";
            }
            if (ddlJenis.SelectedItem.ToString().Trim() == "TUNGGAL")
            {
                string merk = (txtMerk.Text == "") ? "" : "Merk:";
                domainSave.UomID = int.Parse(ddlSatuan.SelectedValue);
                domainSave.LokasiID = int.Parse(LokasiID.SelectedValue);
                domainSave.SubClassID = Convert.ToInt32(Session["SubClassID"]);
                domainSave.NamaAsset = txtCLassNew.Text.Trim().ToUpper() + " " + txtType.Text.Trim().ToUpper() + " " + txtUkuran.Text.Trim().ToUpper() + " " + merk + " " + txtMerk.Text.Trim().ToUpper() + " " + txtJenis.Text.Trim().ToUpper() + " " + txtPartNumber.Text.Trim().ToUpper();
            }
            else
            {
                domainSave.UomID = int.Parse(ddlSatuan0.SelectedValue);
                domainSave.LokasiID = int.Parse(LokasiID0.SelectedValue);
                domainSave.SubClassID = D1.SubClassID;
                domainSave.NamaAsset = txtCLassNew.Text.Trim().ToUpper();
            }

            if (ddlJenis.SelectedItem.ToString().Trim() == "TUNGGAL")
            {
                domainSave.HeadID = 2;
            }
            else if (ddlJenis.SelectedItem.ToString().Trim() == "BERKOMPONEN")
            {
                domainSave.HeadID = 1;
            }

            int intResult = 0;
            intResult = facadeSave.InsertAssetUtama(domainSave);
            if (intResult > 0)
            {
                LoadDataGrid(LoadData());
                LoadDeptAsset(); LoadGroupAsset(); LoadSatuan(); LoadLokasi(); LoadJenisAsset();

                txtClassAsset.Text = string.Empty; txtClassAsset.Enabled = false; txtCLassNew.Text = string.Empty;
                txtType.Text = string.Empty; txtType.Enabled = false;
                txtMerk.Text = string.Empty; txtMerk.Enabled = false;
                txtJenis.Text = string.Empty; txtJenis.Enabled = false;
                txtUkuran.Text = string.Empty; txtUkuran.Enabled = false;
                txtPartNumber.Text = string.Empty; txtPartNumber.Enabled = false;
                txtLeadTime.Text = string.Empty; txtLeadTime.Enabled = false;
                txtCLassNew.Enabled = false;

                ddlSatuan.Enabled = false; ddlDeptID.Enabled = false; ddlGroupAsset.Enabled = false; ddlSatuan.Enabled = false; LokasiID.Enabled = false;

                txtKomponenAsset.Text = "00.0.00.000.000"; txtKodeIndukAsset.Text = users.UnitKerjaID + ".0.000.0.000";

                DisplayAJAXMessage(this, "Data asset berhasil tersimpan !! ");
                return;
            }
        }

        protected void btnSaveAssetKomp_ServerClick(object sender, EventArgs e)
        {
            if (txtNamaKomponenAsset.Text == string.Empty || txtKomponenAsset.Text == string.Empty || ddlIndukAsset.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Data belum lengkap ");

                return;
            }

            Users users = (Users)Session["Users"];
            string query = string.Empty;
            if (users.UnitKerjaID.ToString().Length == 1)
            {
                query = "9";
            }
            else
            {
                query = "10";
            }

            DomainPengajuanAsset domainCek = new DomainPengajuanAsset();
            FacadeAsset facadeCek = new FacadeAsset();
            Session["KodeInduk"] = ddlIndukAsset.SelectedItem.ToString().Trim().Substring(0, Convert.ToInt32(query));

            DomainPengajuanAsset domainSaveAssetKomponen = new DomainPengajuanAsset();
            FacadeAsset facadeSaveAssetKomponen = new FacadeAsset();

            //DomainPengajuanAsset domainSaveAssetKomponen01= new DomainPengajuanAsset();
            //FacadeAsset facadeSaveAssetKomponen01 = new FacadeAsset();
            //int TtlAsset = facadeSaveAssetKomponen01.RetrieveAssetAda(txtKomponenAsset.Text.Trim());

            //int Upgrade = (TtlAsset == 0) ? 0 : TtlAsset + 1;

            domainSaveAssetKomponen.KodeAsset = txtKomponenAsset.Text.Trim();
            domainSaveAssetKomponen.NamaAsset = txtNamaKomponenAsset.Text.Trim().ToUpper();
            domainSaveAssetKomponen.CreatedBy = users.UserName.Trim();
            domainSaveAssetKomponen.UnitKerjaID = users.UnitKerjaID;
            domainSaveAssetKomponen.UomID = int.Parse(ddlSatuan2.SelectedValue);
            domainSaveAssetKomponen.GroupID = 12;
            domainSaveAssetKomponen.LeadTime2 = Convert.ToInt32(txtLeadTime2.Text);
            //domainSaveAssetKomponen.Upgrade = Upgrade;

            int intResult = 0;
            intResult = facadeSaveAssetKomponen.InsertKomponenAsset(domainSaveAssetKomponen);

            if (intResult > 0)
            {
                //txtNamaKomponenAsset.Text = string.Empty;        
                //LoadDataAssetUtama();
                txtLeadTime2.Text = string.Empty;
                LoadDataGridKomponenAsset(LoadDataKomponenAsset());
                //txtKomponenAsset.Text = "06." + Session["Urutan"].ToString() + "." + Session["KodeInduk"].ToString().Trim();
                txtNamaKomponenAsset.Text = string.Empty;
                LoadSatuan();
                DisplayAJAXMessage(this, "Data asset berhasil tersimpan !! ");
                return;
            }
        }

        private ArrayList LoadData()
        {
            Users users = (Users)Session["Users"];
            ArrayList arrAsset = new ArrayList();
            FacadeAsset FacadeGrid = new FacadeAsset();
            arrAsset = FacadeGrid.RetrieveDataGrid();
            arrAsset.Add(new DomainPengajuanAsset());
            return arrAsset;
        }
        private ArrayList LoadDataAssetTunggal()
        {
            Users users = (Users)Session["Users"];
            ArrayList arrAsset = new ArrayList();
            FacadeAsset FacadeGrid = new FacadeAsset();
            arrAsset = FacadeGrid.RetrieveDataGridAssetTunggal();
            arrAsset.Add(new DomainPengajuanAsset());
            return arrAsset;
        }
        private ArrayList LoadDataKomponenAsset()
        {
            LoadSatuan();

            Users users = (Users)Session["Users"]; string query = string.Empty;
            if (users.UnitKerjaID.ToString().Length == 1)
            {
                query = "13";
            }
            else
            {
                query = "14";
            }
            ArrayList arrAsset2 = new ArrayList();
            FacadeAsset FacadeGrid2 = new FacadeAsset();
            arrAsset2 = FacadeGrid2.RetrieveDataGridKomponen(ddlIndukAsset.SelectedItem.ToString().Trim().Substring(0, Convert.ToInt32(query)));
            arrAsset2.Add(new DomainPengajuanAsset());
            return arrAsset2;
        }

        private void LoadDataAsset()
        {
            ArrayList arrDataAsset = new ArrayList();
            FacadeAsset FacadeDataAsset = new FacadeAsset();
            arrDataAsset = FacadeDataAsset.RetrieveDataAsset(txtClassAsset.Text.Trim(), Convert.ToInt32(ddlGroupAsset.SelectedValue));
            ddlSubClassAsset.Items.Clear();
            ddlSubClassAsset.Items.Add(new ListItem("-- Pilih Asset --", "0"));
            foreach (DomainPengajuanAsset asset in arrDataAsset)
            {
                ddlSubClassAsset.Items.Add(new ListItem(asset.NamaAsset, asset.NamaAsset));
            }
        }

        private void LoadDataGrid(ArrayList arrAsset)
        {
            this.GridView1.DataSource = arrAsset;
            this.GridView1.DataBind();
        }
        private void LoadDataGridKomponenAsset(ArrayList arrAsset2)
        {
            this.GridView2.DataSource = arrAsset2;
            this.GridView2.DataBind();
        }






        protected void LoadUkuran(string INCode)
        {

        }
        protected void GridItemName_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "Add")
            //{
            //    int index = Convert.ToInt32(e.CommandArgument);
            //    string kode = string.Empty;
            //    GridViewRow row = GridItemName.Rows[index];
            //    txtNama.Text = row.Cells[1].Text.Trim();
            //    PanelItemName.Visible = false;
            //    Session["incode"] = row.Cells[0].Text;
            //    LoadItemMerk(Session["incode"].ToString());
            //    LoadItemType(Session["incode"].ToString());
            //    LoadUkuran(Session["incode"].ToString());
            //    CollectName();
            //    CollectCode();
            //}
        }

        protected void RBAssetKomponen_CheckedChanged(object sender, EventArgs e)
        {
            LoadDataAssetUtama(); LoadDeptPIC(); RBLama.Visible = false; RBBaru.Visible = false;
            //ddlDeptID0.Items.Add("Project"); 
            txtNamaKomponenAsset.Enabled = false; ddlSatuan2.Enabled = false;
            PanelKomponenAsset.Visible = true; PanelAssetUtama.Visible = false; RBAssetKomponen.Checked = true; RBAssetUtama.Checked = false;
            PanelGridAssetUtama.Visible = false; PanelGridKomponen.Visible = true;
            btnSave.Visible = false; btnSaveAssetKomp.Visible = true;
            txtKomponenAsset.Text = "06.000.0.0.0.000";
            GridView1.Visible = false; GridView2.Visible = true;

        }

        protected void RBAssetUtama_CheckedChanged(object sender, EventArgs e)
        {
            PanelGridAssetUtama.Visible = true; PanelGridKomponen.Visible = false;
            PanelKomponenAsset.Visible = false; PanelAssetUtama.Visible = true; RBAssetUtama.Checked = true; RBAssetKomponen.Checked = false;
            Response.Redirect("PengajuanAsset.aspx");
        }

        protected void RBBaru_CheckedChanged(object sender, EventArgs e)
        {
            RBBaru.Checked = true; RBLama.Checked = false; LoadDataAssetUtama();
        }

        protected void RBLama_CheckedChanged(object sender, EventArgs e)
        {
            RBBaru.Checked = false; RBLama.Checked = true;

            ArrayList arrAssetLama = new ArrayList();
            FacadeAsset facadeAssetLama = new FacadeAsset();
            arrAssetLama = facadeAssetLama.RetrieveAssetLama();
            ddlIndukAsset.Items.Clear();
            ddlIndukAsset.Items.Add(new ListItem("-- Pilih Master Asset Existing --", "0"));
            foreach (DomainPengajuanAsset NewAssetLama in arrAssetLama)
            {
                ddlIndukAsset.Items.Add(new ListItem(NewAssetLama.KodeProjectAsset + " - " + NewAssetLama.NamaProjectAsset, NewAssetLama.KodeProjectAsset));
            }
        }

        private void LoadDataAssetUtama()
        {
            ArrayList arrAssetUtama = new ArrayList();
            FacadeAsset facadeAssetUtama = new FacadeAsset();
            arrAssetUtama = facadeAssetUtama.RetrieveAssetUtama();
            ddlIndukAsset.Items.Clear();
            ddlIndukAsset.Items.Add(new ListItem("-- Pilih Master Asset Baru --", "0"));
            foreach (DomainPengajuanAsset NewAssetUtama in arrAssetUtama)
            {
                ddlIndukAsset.Items.Add(new ListItem(NewAssetUtama.KodeProjectAsset + " - " + NewAssetUtama.NamaProjectAsset, NewAssetUtama.KodeProjectAsset));
            }
        }

        private void LoadDeptPIC()
        {
            ArrayList arrDept = new ArrayList();
            FacadeAsset facadeDept = new FacadeAsset();
            arrDept = facadeDept.RetrieveDept2();
            ddlDeptID0.Items.Clear();
            ddlDeptID0.Items.Add(new ListItem("-- Pilih Dept Pelaksana --", "0"));
            foreach (DomainPengajuanAsset NewDept in arrDept)
            {
                ddlDeptID0.Items.Add(new ListItem(NewDept.Department, NewDept.DeptID.ToString()));
            }
        }

    }

    #region Facade Asset
    public class FacadeAsset
    {
        public string strError = string.Empty;
        private ArrayList arrData = new ArrayList();
        private List<SqlParameter> sqlListParam;
        private DomainPengajuanAsset objAsset = new DomainPengajuanAsset();

        public FacadeAsset()
            : base()
        {

        }
        public string Criteria { get; set; }
        public string Field { get; set; }
        public string Where { get; set; }

        public int InsertAssetUtama(object objDomain)
        {
            try
            {
                objAsset = (DomainPengajuanAsset)objDomain;

                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@GroupID", objAsset.GroupID));
                sqlListParam.Add(new SqlParameter("@ClassID", objAsset.ClassID));
                sqlListParam.Add(new SqlParameter("@SubClassID", objAsset.SubClassID));
                sqlListParam.Add(new SqlParameter("@LokasiID", objAsset.LokasiID));
                sqlListParam.Add(new SqlParameter("@KodeAsset", objAsset.KodeAsset));
                sqlListParam.Add(new SqlParameter("@NamaAsset", objAsset.NamaAsset));
                sqlListParam.Add(new SqlParameter("@ItemKode", objAsset.ItemKode));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objAsset.CreatedBy));
                sqlListParam.Add(new SqlParameter("@OwnerDeptID", objAsset.AssetPemilikDept));
                sqlListParam.Add(new SqlParameter("@TipeAsset", objAsset.TipeAsset));
                sqlListParam.Add(new SqlParameter("@UomID", objAsset.UomID));
                sqlListParam.Add(new SqlParameter("@HeadID", objAsset.HeadID));

                sqlListParam.Add(new SqlParameter("@LeadTime", objAsset.LeadTime));
                sqlListParam.Add(new SqlParameter("@ITYPE", objAsset.ITYPE));
                sqlListParam.Add(new SqlParameter("@Ukuran", objAsset.Ukuran));
                sqlListParam.Add(new SqlParameter("@Merk", objAsset.Merk));
                sqlListParam.Add(new SqlParameter("@Jenis", objAsset.Jenis));
                sqlListParam.Add(new SqlParameter("@PartNumber", objAsset.PartNumber));

                DataAccess da = new DataAccess(Global.ConnectionString());
                int result = da.ProcessData(sqlListParam, "spInsert_PengajuanAssetForAM");

                strError = da.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

        public int InsertKomponenAsset(object objDomain)
        {
            try
            {
                objAsset = (DomainPengajuanAsset)objDomain;

                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@KodeAsset", objAsset.KodeAsset));
                sqlListParam.Add(new SqlParameter("@NamaAsset", objAsset.NamaAsset));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objAsset.CreatedBy));
                sqlListParam.Add(new SqlParameter("@UomID", objAsset.UomID));
                sqlListParam.Add(new SqlParameter("@GroupID", objAsset.GroupID));
                sqlListParam.Add(new SqlParameter("@LeadTime2", objAsset.LeadTime2));


                DataAccess da = new DataAccess(Global.ConnectionString());
                int result = da.ProcessData(sqlListParam, "spInsert_PengajuanAssetKomponenForAM");
                strError = da.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

        public ArrayList RetrieveGroupAsset()
        {
            string strSQL = " select KodeGroup,NamaGroup from AM_Group where RowStatus>-1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObjectGroupAsset(sqlDataReader));
                }
            }

            return arrData;
        }
        private DomainPengajuanAsset GenerateObjectGroupAsset(SqlDataReader sdr)
        {
            DomainPengajuanAsset objAsset = new DomainPengajuanAsset();
            objAsset.KodeGroup = sdr["KodeGroup"].ToString();
            objAsset.NamaGroup = sdr["NamaGroup"].ToString();
            return objAsset;
        }

        public ArrayList RetrieveClassAsset(int GroupAssetID)
        {
            string strSQL = " select ID,NamaClass from AM_Class where GroupID=" + GroupAssetID + " and RowStatus>-1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObjectClassAsset(sqlDataReader));
                }
            }

            return arrData;
        }
        private DomainPengajuanAsset GenerateObjectClassAsset(SqlDataReader sdr)
        {
            DomainPengajuanAsset objAsset = new DomainPengajuanAsset();

            objAsset.ID = Convert.ToInt32(sdr["ID"]);
            objAsset.NamaClass = sdr["NamaClass"].ToString();


            return objAsset;
        }
        public ArrayList RetrieveSubClassAsset(int ClassID, string ClassNama)
        {
            string strSQL =
            " select ID,NamaClass NamaSubClass from AM_SubClass where ClassID in (select ID from AM_Class where ID=" + ClassID + ") and RowStatus>-1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObjectSubClass(sqlDataReader));
                }
            }

            return arrData;
        }
        private DomainPengajuanAsset GenerateObjectSubClass(SqlDataReader sdr)
        {
            DomainPengajuanAsset objAsset = new DomainPengajuanAsset();

            objAsset.ID = Convert.ToInt32(sdr["ID"]);
            objAsset.NamaSubClass = sdr["NamaSubClass"].ToString();


            return objAsset;
        }
        public DomainPengajuanAsset RetrieveKodeSubClass(int KodeClass, string NamaClass)
        {
            string result = string.Empty;
            string StrSql =
            " select KodeClass KodeSubClass, ID SubClassID from AM_SubClass where ClassID in (select ID from AM_Class where ID=" + KodeClass + " and RowStatus>-1) " +
            " and NamaClass='" + NamaClass + "' and RowStatus>-1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectKodeSubClass(sqlDataReader);
                }
            }

            return new DomainPengajuanAsset();
        }
        private DomainPengajuanAsset GenerateObjectKodeSubClass(SqlDataReader sdr)
        {
            DomainPengajuanAsset objAsset = new DomainPengajuanAsset();

            objAsset.SubClassID = Convert.ToInt32(sdr["SubClassID"]);
            objAsset.KodeSubClass = sdr["KodeSubClass"].ToString();


            return objAsset;
        }

        public DomainPengajuanAsset RetrieveKodeClass(string NamaClasss, string GroupAsset)
        {
            string result = string.Empty;
            string StrSql =
            " select KodeClass,ID ClassID from AM_Class where RowStatus>-1 and GroupID='" + GroupAsset + "' and NamaClass='" + NamaClasss + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectKodeClass(sqlDataReader);
                }
            }

            return new DomainPengajuanAsset();
        }
        private DomainPengajuanAsset GenerateObjectKodeClass(SqlDataReader sdr)
        {
            DomainPengajuanAsset objAsset = new DomainPengajuanAsset();

            objAsset.ClassID = Convert.ToInt32(sdr["ClassID"]);
            objAsset.KodeClass = sdr["KodeClass"].ToString();

            return objAsset;
        }

        public int RetrieveLastAsset(string KodeDept, string KodeGroupAsset, string KodeClass, string KodeSubCLass)
        {
            string StrSql =
            //" select COUNT(KodeAsset)LastAssetTotal from AM_Asset where  ClassID='"+ClassID+"' and SubClassID='"+SubClassID+"' "+
            //" and GroupID='"+GroupAsset+"' and RowStatus>-1 ";
            " select COUNT(ItemCode)LastAssetTotal from Asset " +
            " where SUBSTRING(ItemCode,1,2)='" + KodeDept + "' and SUBSTRING(ItemCode,4,1)='" + KodeGroupAsset + "' and " +
            " SUBSTRING(ItemCode,6,2)='" + KodeClass + "' and SUBSTRING(ItemCode,9,4)='" + KodeSubCLass + ".' ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["LastAssetTotal"]);
                }
            }

            return 0;
        }
        public DomainPengajuanAsset RetrieveKodePlant(string NamaAsset)
        {
            string hasil = string.Empty;
            string StrSql =
            "  select distinct ItemKode,AssetID from AM_Asset where NamaAsset='" + NamaAsset + "' and RowStatus>-1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectKodePlant(sqlDataReader);
                }
            }

            return new DomainPengajuanAsset();
        }
        public DomainPengajuanAsset GetLastNumKomponen(string itemMaster)
        {
            string hasil = string.Empty;
            string StrSql =
            "select 1 as AssetID,  convert(varchar, isnull(MAX( SUBSTRING(ItemCode,4,3) ),0) ) as ItemKode from Asset where Head=0 and RowStatus>-1 and right(ItemCode,15)='" + itemMaster + "' and LEN(ItemCode)>15 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectKodePlant(sqlDataReader);
                }
            }

            return new DomainPengajuanAsset();
        }
        public int GetLastNumItemKomponen(string itemMaster)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string hasil = string.Empty; string query = string.Empty; string query2 = string.Empty;
            if (users.UnitKerjaID.ToString().Length == 1)
            {
                query = "20"; query2 = "13";
            }
            else
            {
                query = "21"; query2 = "14";
            }
            string StrSql =
            " select COUNT(Urutan)LastNo from " +
            " (select SUBSTRING(ItemCode,8," + query2 + ")KodeAsset,SUBSTRING(ItemCode,1,2)KodeProject,SUBSTRING(ItemCode,4,3)Urutan " +
            " from Asset where Head=0 and LEN(ItemCode)=" + query + " ) as xx where xx.KodeAsset='" + itemMaster + "' ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["LastNo"]);
                }
            }

            return 0;
        }
        private DomainPengajuanAsset GenerateObjectKodePlant(SqlDataReader sdr)
        {
            DomainPengajuanAsset objAsset = new DomainPengajuanAsset();

            objAsset.AssetID = Convert.ToInt32(sdr["AssetID"]);
            objAsset.ItemKode = sdr["ItemKode"].ToString();

            return objAsset;
        }
        public ArrayList RetrieveDataGrid()
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string query = string.Empty; string query2 = string.Empty;
            if (users.UnitKerjaID.ToString().Length == 1)
            {
                query = "9";
            }
            else
            {
                query = "10";
            }
            string strSQL =

            " select ID,NamAsset NamaAsset,KodeAsset,(select AA.NamaDept from AM_department AA where AA.DeptID=data1.KodeDept and AA.RowStatus>-1)NamaDepartment,(select top 1 A1.NamaGroup from AM_Group A1 where RowStatus>-1 and A1.ID=data1.AMgroupID)NamaGroup," +
            " (select top 1 B1.NamaClass from AM_Class B1 where B1.ID=data1.AMclassID and B1.RowStatus>-1)NamaClass,case when Head=1 then 'Komponen' when Head=2 then 'Tunggal' end TipeAssetS,left(convert(char,createdtime,113),20) TglBuat " +
            " from (select  ID,Urutan,NamAsset," +
            " KodeAsset,AMgroupID,AMclassID,SUBSTRING(KodeAsset," + query + ",1)KodeDept,CreatedTime,Head from (select top 10 'A'+cast(ID as NCHAR) Urutan,ID,ItemName " +
            " NamAsset,ItemCode KodeAsset,AMgroupID,AMclassID,CreatedTime,Head from Asset where Head in (1,2)  and RowStatus>-1 " +
            " ) as xx   ) as data1 order by ID desc ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObjectDataGrid(sqlDataReader));
                }
            }

            return arrData;
        }
        private DomainPengajuanAsset GenerateObjectDataGrid(SqlDataReader sdr)
        {
            DomainPengajuanAsset objAsset = new DomainPengajuanAsset();

            objAsset.ID = Convert.ToInt32(sdr["ID"]);
            objAsset.KodeAsset = sdr["KodeAsset"].ToString();
            objAsset.NamaGroup = sdr["NamaGroup"].ToString();
            objAsset.NamaClass = sdr["NamaClass"].ToString();
            //objAsset.NamaSubClass = sdr["NamaSubClass"].ToString();
            objAsset.NamaDepartment = sdr["NamaDepartment"].ToString();
            objAsset.TglBuat = sdr["TglBuat"].ToString();
            objAsset.NamaAsset = sdr["NamaAsset"].ToString();
            objAsset.TipeAssetS = sdr["TipeAssetS"].ToString();

            return objAsset;
        }
        public ArrayList RetrieveDataGridAssetTunggal()
        {
            string strSQL =
            " select ID,NamAsset NamaAsset,KodeAsset,(select AA.NamaDept from AM_department AA where AA.DeptID=data1.KodeDept and AA.RowStatus>-1)NamaDepartment,(select top 1 A1.NamaGroup from AM_Group A1 where RowStatus>-1 and A1.ID=data1.AMgroupID)NamaGroup," +
            " (select top 1 B1.NamaClass from AM_Class B1 where B1.ID=data1.AMclassID and B1.RowStatus>-1)NamaClass,left(convert(char,createdtime,113),20) TglBuat " +
            " from (select  ID,Urutan,NamAsset," +
            " KodeAsset,AMgroupID,AMclassID,SUBSTRING(KodeAsset,9,1)KodeDept,CreatedTime from (select top 10 'A'+cast(ID as NCHAR) Urutan,ID,ItemName " +
            " NamAsset,ItemCode KodeAsset,AMgroupID,AMclassID,CreatedTime from Asset where Head=2  and RowStatus>-1 " +
            " ) as xx   ) as data1 order by ID desc ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObjectDataGridAT(sqlDataReader));
                }
            }

            return arrData;
        }
        private DomainPengajuanAsset GenerateObjectDataGridAT(SqlDataReader sdr)
        {
            DomainPengajuanAsset objAsset = new DomainPengajuanAsset();

            objAsset.ID = Convert.ToInt32(sdr["ID"]);
            objAsset.KodeAsset = sdr["KodeAsset"].ToString();
            objAsset.NamaGroup = sdr["NamaGroup"].ToString();
            objAsset.NamaClass = sdr["NamaClass"].ToString();
            //objAsset.NamaSubClass = sdr["NamaSubClass"].ToString();
            objAsset.NamaDepartment = sdr["NamaDepartment"].ToString();
            objAsset.TglBuat = sdr["TglBuat"].ToString();
            objAsset.NamaAsset = sdr["NamaAsset"].ToString();

            return objAsset;
        }
        public ArrayList RetrieveDataGridKomponen(string KodeAssetKomponen)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string query = string.Empty; string query2 = string.Empty;
            if (users.UnitKerjaID.ToString().Length == 1)
            {
                query = "13"; query2 = "20";
            }
            else
            {
                query = "14"; query2 = "21";
            }

            string strSQL =
            "select case when Head=1 then ItemCode else '' end KodeAsset,case when Head=1 then ItemName else '' end NamaAsset," +
            "case when Head=1 then '' else ItemCode end KodeKomponenAsset,case when Head=1 then '' else ItemName end NamaKomponenAsset," +
            "(select top 1 A.UOMDesc from UOM A where A.ID=UOMID)Satuan, " +
            "left(convert(char,CreatedTime,113)," + query2 + ") as TglBuat from Asset where RowStatus>-1 and RIGHT(ItemCode," + query + ")='" + KodeAssetKomponen + "' " +
            "order by head desc,ID";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObjectDataGridKomponen(sqlDataReader));
                }
            }

            return arrData;
        }
        private DomainPengajuanAsset GenerateObjectDataGridKomponen(SqlDataReader sdr)
        {
            DomainPengajuanAsset objAsset = new DomainPengajuanAsset();

            objAsset.KodeAsset = sdr["KodeAsset"].ToString();
            objAsset.NamaAsset = sdr["NamaAsset"].ToString();
            objAsset.NamaKomponenAsset = sdr["NamaKomponenAsset"].ToString();
            objAsset.KodeKomponenAsset = sdr["KodeKomponenAsset"].ToString();
            objAsset.TglBuat = sdr["TglBuat"].ToString();
            objAsset.Satuan = sdr["Satuan"].ToString();

            return objAsset;
        }
        public ArrayList RetrieveAssetUtama()
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string query = string.Empty;
            if (users.UnitKerjaID.ToString().Length == 1)
            {
                query = "13";
            }
            else
            {
                query = "14";
            }
            string strSQL =
            " select xx1.ID,xx1.NamaAsset,xx1.KodeProjectAsset,xx1.NamaProjectAsset from " +
            " (select B.ItemName NamaAsset,xx.* from (select ID,substring(ItemCode,1," + query + ")KodeAsset,ItemCode as  KodeProjectAsset, ItemName as  NamaProjectAsset " +
            " from Asset where Head=1 and RowStatus>-1  and LEN(ItemCode)>=" + query + ") as xx inner join Asset B ON B.ItemCode=xx.KodeAsset ) as xx1 " +
            " where NamaAsset not in (select NamaAsset from AM_Asset where RowStatus>-1) ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObjectAssetUtama(sqlDataReader));
                }
            }

            return arrData;
        }

        public ArrayList RetrieveAssetLama()
        {
            //Users users = (Users)HttpContext.Current.Session["Users"];
            //string query = string.Empty;
            //if (users.UnitKerjaID.ToString().Length == 1)
            //{
            //    query = "13";
            //}
            //else
            //{
            //    query = "14";
            //}
            string strSQL =
            " select AssetID ID,KodeAsset KodeProjectAsset,NamaAsset,NamaAsset NamaProjectAsset,'1'Head,'Lama'Noted from AM_Asset " +
            " where RowStatus>-1 and AssetID in (select ID from Asset where RowStatus>-1 and Head=1) group by KodeAsset,AssetID,NamaAsset ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObjectAssetLama(sqlDataReader));
                }
            }

            return arrData;
        }

        public ArrayList RetrieveLokasi()
        {
            string strSQL = "select distinct ID,isnull(Ket1,'')+' - '+isnull(Ket2,'')+' - '+isnull(Ket3,'') as NamaLokasi from AM_Lokasi where RowStatus>-1 and (Ket1 is not null or Ket2 is not null or Ket3 is not null)";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObjectLokasi(sqlDataReader));
                }
            }

            return arrData;
        }
        public ArrayList RetrieveDataAsset(string NamaAsset, int GroupID)
        {
            string strSQL =
            " select distinct(NamaClass)NamaAsset from AM_SubClass where ClassID in (select ID from AM_Class where GroupID=" + GroupID + " " +
            " and NamaClass='" + NamaAsset + "' and RowStatus>-1) and RowStatus>-1 ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObjectDataAsset(sqlDataReader));
                }
            }

            return arrData;
        }
        private DomainPengajuanAsset GenerateObjectDataAsset(SqlDataReader sdr)
        {
            DomainPengajuanAsset objAsset = new DomainPengajuanAsset();

            //objAsset.ID = Convert.ToInt32(sdr["ID"]);
            //objAsset.KodeAsset = sdr["KodeAsset"].ToString();       
            objAsset.NamaAsset = sdr["NamaAsset"].ToString();
            //objAsset.KodeProjectAsset = sdr["KodeProjectAsset"].ToString();
            //objAsset.NamaProjectAsset = sdr["NamaProjectAsset"].ToString();

            return objAsset;
        }
        private DomainPengajuanAsset GenerateObjectAssetUtama(SqlDataReader sdr)
        {
            DomainPengajuanAsset objAsset = new DomainPengajuanAsset();

            objAsset.ID = Convert.ToInt32(sdr["ID"]);
            //objAsset.KodeAsset = sdr["KodeAsset"].ToString();       
            objAsset.NamaAsset = sdr["NamaAsset"].ToString();
            objAsset.KodeProjectAsset = sdr["KodeProjectAsset"].ToString();
            objAsset.NamaProjectAsset = sdr["NamaProjectAsset"].ToString();

            return objAsset;
        }
        private DomainPengajuanAsset GenerateObjectAssetLama(SqlDataReader sdr)
        {
            DomainPengajuanAsset objAsset = new DomainPengajuanAsset();

            objAsset.ID = Convert.ToInt32(sdr["ID"]);
            //objAsset.KodeAsset = sdr["KodeAsset"].ToString();       
            objAsset.NamaAsset = sdr["NamaAsset"].ToString();
            objAsset.KodeProjectAsset = sdr["KodeProjectAsset"].ToString();
            objAsset.NamaProjectAsset = sdr["NamaProjectAsset"].ToString();

            return objAsset;
        }
        private DomainPengajuanAsset GenerateObjectLokasi(SqlDataReader sdr)
        {
            DomainPengajuanAsset objAsset = new DomainPengajuanAsset();

            objAsset.ID = Convert.ToInt32(sdr["ID"]);
            objAsset.NamaLokasi = sdr["NamaLokasi"].ToString();

            return objAsset;
        }

        //RetrieveDataAssetUtama
        public int RetrieveKompAsset(string NamaKompAsset)
        {
            string StrSql =
            //" select COUNT(KodeAsset)NoUrut  from AM_Asset where KodeAsset like'%" + NamaKompAsset + "%' and JenisAsset=2 and RowStatus>-1 ";
            " select SUM(NoUrut)NoUrut from (select COUNT(KodeAsset)NoUrut  from AM_Asset where KodeAsset like'%" + NamaKompAsset + "%' and " +
            " JenisAsset=2 and RowStatus>-1 " +
            " union all " +
            " select 0'NoUrut' ) as xx ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["NoUrut"]);
                }
            }

            return 0;
        }

        public int RetrieveAssetAda(string KodeKompAsset)
        {
            string StrSql =
            //" select COUNT(KodeAsset)NoUrut  from AM_Asset where KodeAsset like'%" + NamaKompAsset + "%' and JenisAsset=2 and RowStatus>-1 ";
            " select sum(Ttl)Ttl  from ( " +
            " select count(ID)Ttl from AM_AssetSerah where NoAsset='" + KodeKompAsset + "' and RowStatus>-1 " +
            " union all " +
            " select 0 Ttl) as x ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Ttl"]);
                }
            }

            return 0;
        }

        public DomainPengajuanAsset RetrieveKode(int ClassID, int SubClassID)
        {
            string hasil = string.Empty;
            string StrSql =

            "  select '0'+CAST(KodeClass as NCHAR(3))KodeClass,'00'+CAST(KodeSubClass as NCHAR(3))KodeSubClass from " +
            " (select sum(kodeclass)KodeClass,sum(KodeSubClass)KodeSubClass from " +
            " (select CAST(KodeClass as decimal(10,0))KodeClass,'0'KodeSubClass from AM_Class where  ID='" + ClassID + "' and RowStatus>-1 " +
            " union all " +
            " select '0'KodeClass,cast(KodeClass as decimal(10,0)) KodeSubClass from AM_SubClass where  ID='" + SubClassID + "' and RowStatus>-1 ) as xx ) as xx1 ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectKode(sqlDataReader);
                }
            }

            return new DomainPengajuanAsset();
        }
        private DomainPengajuanAsset GenerateObjectKode(SqlDataReader sdr)
        {
            DomainPengajuanAsset objAsset = new DomainPengajuanAsset();

            objAsset.KodeClass = sdr["KodeClass"].ToString();
            objAsset.KodeSubClass = sdr["KodeSubClass"].ToString();

            return objAsset;
        }

        public DomainPengajuanAsset RetrieveKodeAsset(string KodeAsset)
        {
            string hasil = string.Empty;
            string StrSql =

            " select COUNT(KodeAsset)Total from (select ItemCode KodeAsset from Asset where RowStatus>-1 and ItemCode='" + KodeAsset + "' and Head=1 " +
            " union all " +
            " select '01'KodeAsset ) as xx ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectKode2(sqlDataReader);
                }
            }

            return new DomainPengajuanAsset();
        }
        //public DomainPengajuanAsset RetrieveKodeClass(string NamaClass)
        //{
        //    string hasil = string.Empty;
        //    string StrSql =

        //    " select KodeClass from AM_Class where NamaClass='" + NamaClass + "' and RowStatus>-1 ";

        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
        //    strError = dataAccess.Error;

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            return GenerateObjectKodeClass(sqlDataReader);
        //        }
        //    }

        //    return new DomainPengajuanAsset();
        //}
        private DomainPengajuanAsset GenerateObjectKode2(SqlDataReader sdr)
        {
            DomainPengajuanAsset objAsset = new DomainPengajuanAsset();

            objAsset.Total = Convert.ToInt32(sdr["Total"]);
            //objAsset.KodeSubClass = sdr["KodeSubClass"].ToString();

            return objAsset;
        }

        public DomainPengajuanAsset RetrieveClass(string Nama, string Group)
        {
            string StrSql =
                //" select COUNT(KodeAsset)NoUrut  from AM_Asset where KodeAsset like'%" + NamaKompAsset + "%' and JenisAsset=2 and RowStatus>-1 ";
                //" select ID ClassID,KodeClass,NamaClass from AM_Class where RowStatus>-1 and GroupID="+Group+" and NamaClass='"+Nama+"' ";

                " select COUNT(NamaClass)NoUrut from AM_SubClass where ClassID in (select ID from AM_Class where GroupID=" + Group + " and " +
                " NamaClass='" + Nama + "' and RowStatus>-1) and RowStatus>-1 ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectKodeAsset(sqlDataReader);
                }
            }

            return new DomainPengajuanAsset();
        }

        private DomainPengajuanAsset GenerateObjectKodeAsset(SqlDataReader sdr)
        {
            DomainPengajuanAsset objAsset = new DomainPengajuanAsset();
            objAsset.NoUrut = Convert.ToInt32(sdr["NoUrut"]);
            return objAsset;
        }

        public DomainPengajuanAsset RetrieveNoUrut(string NamaClasss, string GroupAsset)
        {
            string result = string.Empty;
            string StrSql =
            " select cast (KodeClass as NCHAR)NoUrutan from AM_SubClass where NamaClass='" + NamaClasss + "' and ClassID in " +
            " (select ID from AM_Class where GroupID=" + GroupAsset + " and RowStatus>-1) and RowStatus>-1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectUrut(sqlDataReader);
                }
            }
            return new DomainPengajuanAsset();
        }
        private DomainPengajuanAsset GenerateObjectUrut(SqlDataReader sdr)
        {
            DomainPengajuanAsset objAsset = new DomainPengajuanAsset();
            objAsset.NoUrutan = sdr["NoUrutan"].ToString();
            return objAsset;
        }

        public DomainPengajuanAsset RetrieveSubClassNew(string NamaSubClass, string Group)
        {
            string result = string.Empty;
            string StrSql =
            " select NamaClass NamaSubClass from AM_SubClass where NamaClass='" + NamaSubClass + "' and ClassID in " +
            " (select ID from AM_Class where GroupID=" + Group + " and RowStatus>-1) and RowStatus>-1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectSubClassNew(sqlDataReader);
                }
            }

            return new DomainPengajuanAsset();
        }
        private DomainPengajuanAsset GenerateObjectSubClassNew(SqlDataReader sdr)
        {
            DomainPengajuanAsset objAsset = new DomainPengajuanAsset();
            objAsset.NamaSubClass = sdr["NamaSubClass"].ToString();
            return objAsset;
        }

        public DomainPengajuanAsset RetrieveClassID(string NamaClass, string Group)
        {
            string result = string.Empty;
            string StrSql =
            " select ID ClassID from AM_Class where NamaClass='" + NamaClass + "' and GroupID=" + Group + " and RowStatus>-1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectClassID(sqlDataReader);
                }
            }

            return new DomainPengajuanAsset();
        }
        private DomainPengajuanAsset GenerateObjectClassID(SqlDataReader sdr)
        {
            DomainPengajuanAsset objAsset = new DomainPengajuanAsset();
            objAsset.ClassID = Convert.ToInt32(sdr["ClassID"]);
            return objAsset;
        }

        public ArrayList RetrieveDept()
        {
            string strSQL = "  select DeptID,NamaDept Department from AM_Department where rowstatus>-1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObjectDept(sqlDataReader));
                }
            }

            return arrData;
        }

        public ArrayList RetrieveDept2()
        {
            string strSQL = "  select ID DeptID,DeptName Department from Dept where ID in (22,30,19)  ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObjectDept2(sqlDataReader));
                }
            }

            return arrData;
        }

        private DomainPengajuanAsset GenerateObjectDept2(SqlDataReader sdr)
        {
            DomainPengajuanAsset objAsset = new DomainPengajuanAsset();
            objAsset.Department = sdr["Department"].ToString();
            objAsset.DeptID = Convert.ToInt32(sdr["DeptID"]);
            return objAsset;
        }

        private DomainPengajuanAsset GenerateObjectDept(SqlDataReader sdr)
        {
            DomainPengajuanAsset objAsset = new DomainPengajuanAsset();
            objAsset.Department = sdr["Department"].ToString();
            objAsset.DeptID = Convert.ToInt32(sdr["DeptID"]);
            return objAsset;
        }

        public DomainPengajuanAsset RetrieveAssetTunggal(string NamaAsset, string Group)
        {
            string result = string.Empty;
            string StrSql =
            " select distinct(ItemName)NamaAsset from Asset where ItemName='" + NamaAsset + "' and RowStatus>-1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectAssetTunggal(sqlDataReader);
                }
            }

            return new DomainPengajuanAsset();
        }
        private DomainPengajuanAsset GenerateObjectAssetTunggal(SqlDataReader sdr)
        {
            DomainPengajuanAsset objAsset = new DomainPengajuanAsset();
            objAsset.NamaAsset = sdr["NamaAsset"].ToString();
            return objAsset;
        }

        //public DomainPengajuanAsset RetrieveAssetTNoUrut(string NamaAsset,string klass,string SubKlass, string Group)
        public DomainPengajuanAsset RetrieveAssetTNoUrut(string NamaAsset, string klass, string Group)
        {
            string result = string.Empty;
            string StrSql =
            " select COUNT(NamaAsset)Total  from(select ROW_NUMBER() OVER(ORDER BY xx1.IDAsset asc)No,xx1.NamaAsset " +
            " from (select *,(select top 1 AssyDate from AM_Asset ams where ams.NamaAsset=xx.NamaAsset and RowStatus>-1 order by ams.AssyDate asc)IDAsset " +
            " from (select DISTINCT(NamaAsset)NamaAsset from AM_Asset where GroupID=" + Group + " and ClassID " +
            //" in (select ID from AM_Class where NamaClass='"+klass+"' and RowStatus>-1) and SubClassID "+
            //" in (select ID from AM_SubClass where NamaClass='"+SubKlass+"' and RowStatus>-1) and RowStatus>-1 ) as xx ) as xx1 ) as xx2 ";
            " in (select ID from AM_Class where NamaClass='" + klass + "' and RowStatus>-1) and RowStatus>-1 ) as xx ) as xx1 ) as xx2 ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectAssetTNoUrut(sqlDataReader);
                }
            }

            return new DomainPengajuanAsset();
        }
        //public DomainPengajuanAsset RetrieveAssetTNoUrut0(string NamaAsset, string klass, string SubKlass, string Group)
        public DomainPengajuanAsset RetrieveAssetTNoUrut0(string NamaAsset, string klass, string Group)
        {
            Users user = (Users)HttpContext.Current.Session["Users"];
            string query = string.Empty; string query1 = string.Empty;
            string SubKlass = string.Empty;
            SubKlass = "-";
            if (user.UnitKerjaID.ToString().Length == 1)
            {
                query = "11";
                query1 =
                //" select CAST(SUBSTRING(ItemCode,"+query+",3) as Decimal(2))Total from Asset where AMgroupID=" + Group + " and AMclassID in (select ID from AM_Class " +
                //" where NamaClass='" + klass + "' and RowStatus>-1) and AMsubClassID in (select ID from AM_SubClass where NamaClass='" + SubKlass + "' " +
                //" and RowStatus>-1) and RowStatus>-1 order by ID desc";
                " select CAST(SUBSTRING(ItemCode," + query + ",3) as Decimal(2))Total from Asset where AMgroupID=" + Group + " and AMclassID in (select ID from AM_Class " +
                " where NamaClass='" + klass + "' and RowStatus>-1) and RowStatus>-1 and ItemName='" + NamaAsset + "' order by ID desc";
            }
            else
            {
                query = "12";
                query1 =
                " select COUNT(Total)Total from (select isnull(CAST(SUBSTRING(AMKodeAsset_New," + query + ",3) as Decimal(2)),0)Total from Asset " +
                " where AMgroupID=" + Group + " and AMclassID in (select ID from AM_Class  where NamaClass='" + klass + "' and RowStatus>-1) and AMsubClassID " +
                " in (select ID from AM_SubClass where NamaClass='" + SubKlass + "' and RowStatus>-1) and AMKodeAsset_New is not null ) as xx ";
            }
            string result = string.Empty;
            string StrSql = query1;
            //" select CAST(SUBSTRING(ItemCode,"+query+",3) as Decimal(2))Total from Asset where AMgroupID=" + Group + " and AMclassID in (select ID from AM_Class " +
            //" where NamaClass='" + klass + "' and RowStatus>-1) and AMsubClassID in (select ID from AM_SubClass where NamaClass='" + SubKlass + "' " +
            //" and RowStatus>-1) and RowStatus>-1 order by ID desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectAssetTNoUrut(sqlDataReader);
                }
            }

            return new DomainPengajuanAsset();
        }
        private DomainPengajuanAsset GenerateObjectAssetTNoUrut(SqlDataReader sdr)
        {
            DomainPengajuanAsset objAsset = new DomainPengajuanAsset();
            objAsset.Total = Convert.ToInt32(sdr["Total"]);
            return objAsset;
        }

        public DomainPengajuanAsset RetrieveIDSubClass(string NamaAsset, string klass, string SubKlass, string Group)
        {
            string result = string.Empty;
            string StrSql =
            " select ID SubClassID,KodeClass KodeSubClass from AM_SubClass where ClassID in (select ID from AM_Class " +
            " where NamaClass='" + klass + "' and GroupID=" + Group + " and RowStatus>-1) and NamaClass='" + SubKlass + "' and RowStatus>-1 ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectIDSubClass(sqlDataReader);
                }
            }

            return new DomainPengajuanAsset();
        }
        private DomainPengajuanAsset GenerateObjectIDSubClass(SqlDataReader sdr)
        {
            DomainPengajuanAsset objAsset = new DomainPengajuanAsset();
            objAsset.SubClassID = Convert.ToInt32(sdr["SubClassID"]);
            objAsset.KodeSubClass = sdr["KodeSubClass"].ToString();
            return objAsset;
        }

        public DomainPengajuanAsset RetrieveSClassID(string klass, string SubKlass, string Group)
        {
            string result = string.Empty;
            string StrSql =
            " select ID SubClassID from AM_SubClass where ClassID in (select ID from AM_Class " +
            " where NamaClass='" + klass + "' and GroupID=" + Group + " and RowStatus>-1) and NamaClass='" + SubKlass + "' and RowStatus>-1 ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectIDSubClass2(sqlDataReader);
                }
            }

            return new DomainPengajuanAsset();
        }
        private DomainPengajuanAsset GenerateObjectIDSubClass2(SqlDataReader sdr)
        {
            DomainPengajuanAsset objAsset = new DomainPengajuanAsset();
            objAsset.SubClassID = Convert.ToInt32(sdr["SubClassID"]);
            return objAsset;
        }
    }
    #endregion

    #region Domain Asset
    public class DomainPengajuanAsset
    {
        public int Upgrade { get; set; }
        public int UnitKerjaID { get; set; }
        public int ID { get; set; }
        public int PlantID { get; set; }
        public int ClassID { get; set; }
        public int SubClassID { get; set; }
        public int GroupID { get; set; }
        public int AssetID { get; set; }
        public int AssetPemilikDept { get; set; }
        public int UomID { get; set; }
        public int TipeAsset { get; set; }
        public int LokasiID { get; set; }
        public int Total { get; set; }
        public int HeadID { get; set; }
        public int NoUrut { get; set; }
        public int DeptID { get; set; }

        public int LeadTime2 { get; set; }
        public int LeadTime { get; set; }
        public string Type { get; set; }
        public string Ukuran { get; set; }
        public string Merk { get; set; }
        public string Jenis { get; set; }
        public string PartNumber { get; set; }
        public string ITYPE { get; set; }

        public DateTime LastModifiedTime { get; set; }
        public DateTime Tanggal2 { get; set; }

        public string TipeAssetS { get; set; }
        public string Department { get; set; }
        public string NoUrutan { get; set; }
        public string Satuan { get; set; }
        public string KodeGroup { get; set; }
        public string NamaGroup { get; set; }
        public string NamaClass { get; set; }
        public string NamaSubClass { get; set; }
        public string KodeClass { get; set; }
        public string KodeSubClass { get; set; }
        public string ItemKode { get; set; }
        public string CreatedBy { get; set; }
        public string NamaDepartment { get; set; }
        public string TglBuat { get; set; }
        public string KodeAsset { get; set; }
        public string NamaAsset { get; set; }
        public string NamaKomponenAsset { get; set; }
        public string KodeKomponenAsset { get; set; }
        public string NamaLokasi { get; set; }
        public string KodeProjectAsset { get; set; }
        public string NamaProjectAsset { get; set; }

        public Decimal OutM3 { get; set; }
        public Decimal EfesiensiL6 { get; set; }


    }

    #endregion
}