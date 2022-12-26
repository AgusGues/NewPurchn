using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using Factory;
using DataAccessLayer;
using System.IO;
using System.Globalization;

namespace GRCweb1.Modul.RMM
{
    public partial class RMMNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                txttgl.Text = DateTime.Now.ToString("dd-MM-yyyy");
                txtRMM_Date0.Text = DateTime.Now.ToString("dd-MM-yyyy");
                txtActualJS.Text = DateTime.Now.ToString("dd-MM-yyyy");
                txtActualJS.Enabled = false;
                btnHapus.Disabled = true;
                LoadDept();
                LoadDept0();
                LoadYear();
                LoadSarmutDept();
                LoadSarmutPerusahaan();
                LoadDimensi();
                LoadTahunInput();
                //txtTahun.Text = DateTime.Now.Year.ToString();
                Users user = ((Users)Session["Users"]);
                RMMFacade rmmFacade1 = new RMMFacade();
                Domain.RMM rMM = new Domain.RMM();
                LoadData();

                string UserID = string.Empty;
                UserID = user.ID.ToString();
                string Apv = rmmFacade1.GetApv(UserID);
                if (Convert.ToInt32(Apv) > 0)
                    btnUpdate.Disabled = true;
                string usertype = rmmFacade1.GetUserType(UserID);
                Session["usertype"] = usertype;
                ClearForm();
            }
            string path = Request.QueryString["path"];
            if (path != null)
            {
                System.Drawing.Bitmap img = new System.Drawing.Bitmap(path);
                img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        #region penambahan tahun agus
        private void LoadTahunInput()
        {
            ArrayList arrD = this.ListBATahun();
            ddlTahunInput.Items.Clear();
            foreach (BeritaAcara ba in arrD)
            {
                //ddlTahun.Items.Add(new ListItem(ba.Tahun.ToString(), ba.Tahun.ToString()));
                ddlTahunInput.Items.Add(new System.Web.UI.WebControls.ListItem(ba.Tahun.ToString(), ba.Tahun.ToString()));
            }
            ddlTahunInput.SelectedValue = DateTime.Now.Year.ToString();
        }

        private ArrayList ListBATahun()
        {
            ArrayList arrData = new ArrayList();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "SELECT DISTINCT YEAR(BADate)Tahun From BeritaAcara Order By YEAR(BADate)Desc";
            SqlDataReader sdr = zw.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new BeritaAcara
                        {
                            Tahun = Convert.ToInt32(sdr["Tahun"].ToString())
                        });
                    }
                }
            }
            else
            {
                arrData.Add(new BeritaAcara { Tahun = DateTime.Now.Year });
            }
            return arrData;
        }

        protected void ddlTahunInput_SelectedIndexChanged(object sender, EventArgs e)
        { }
        #endregion

        private void LoadDept()
        {
            ddlDeptName.Items.Clear();
            ArrayList arrDept = new ArrayList();
            RMM_Dept sarmutdept = new RMM_Dept();
            RMM_DeptFacade deptFacade = new RMM_DeptFacade();
            arrDept = deptFacade.Retrieve();
            ddlDeptName.Items.Add(new ListItem("-- Pilih Dept --", "0"));
            foreach (RMM_Dept dept in arrDept)
            {
                ddlDeptName.Items.Add(new ListItem(dept.Departemen.ToUpper().Trim(), dept.ID.ToString()));
            }
            Users user = ((Users)Session["Users"]);
            sarmutdept = deptFacade.GetUserDept(user.ID);
            ddlDeptName.SelectedValue = sarmutdept.ID.ToString();
        }

        private void LoadDept0()
        {
            ddlDeptName0.Items.Clear();
            ArrayList arrDept = new ArrayList();
            RMM_Dept rmmdept = new RMM_Dept();
            RMM_DeptFacade deptFacade = new RMM_DeptFacade();
            arrDept = deptFacade.Retrieve();
            ddlDeptName0.Items.Add(new ListItem("ALL", "0"));
            foreach (RMM_Dept dept in arrDept)
            {
                ddlDeptName0.Items.Add(new ListItem(dept.Departemen.ToUpper().Trim(), dept.ID.ToString()));
            }
        }

        protected void chkCancel1_CheckedChanged(object sender, EventArgs e)
        {
            Users user = ((Users)Session["Users"]);
            RMMFacade rmmf = new RMMFacade();
            int value = 0;
            if (chkCancel.Checked == true)
                value = -1;
            else
                value = 0;
            string strerror = rmmf.updateCncl(idX.Text, value);
            DisplayAJAXMessage(this, "Cancel Berhasil");
            ClearForm();
        }

        protected void chksolved_CheckedChanged(object sender, EventArgs e)
        {
            btnSolve.Visible = true;
            txtDateSolved.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtDueDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        }

        protected void btnSolve_ServerClick(object sender, EventArgs e)
        {
            Users user = ((Users)Session["Users"]);
            RMMFacade rmmf = new RMMFacade();
            int value = 0;
            if (chksolved.Checked == true) { value = 1; } else { value = 0; }
            DateTime dt = DateTime.Parse(txtDateSolved.Text);
            string tanggal = dt.ToString("yyyyMMdd");
            string strerror = rmmf.updateSolved2(idX.Text, tanggal, value);
            btnSolve.Visible = false;

        }

        private void LoadYear()
        {
            ddlTahun.Items.Clear();
            ArrayList arrRMM = new ArrayList();
            Domain.RMM rmm = new Domain.RMM();
            RMMFacade rmmFacade = new RMMFacade();
            arrRMM = rmmFacade.GetYearTPP();
            ddlTahun.Items.Add(new ListItem("ALL", "0"));
            foreach (Domain.RMM rmmyear in arrRMM)
            {
                ddlTahun.Items.Add(new ListItem(rmmyear.Year.ToUpper().Trim(), rmmyear.Year.ToUpper().Trim()));
            }
        }

        protected void LoadRMMByKriteria()
        {
            Domain.RMM rmm = new Domain.RMM();
            RMMFacade rmmf = new RMMFacade();
            ArrayList arrrmm = new ArrayList();
            string Kriteria = string.Empty;

            if (ddlDeptName0.SelectedItem.Text.Trim() != "ALL")
                Kriteria = " and A.RMM_DeptID in (select ID from rmm_dept where departemen='" + ddlDeptName0.SelectedItem.Text + "')";
            if (ddlTahun.SelectedItem.Text.Trim() != "ALL")
                Kriteria = Kriteria + " and left(convert(char,A.Tgl_RMM,112),4)=" + ddlTahun.SelectedItem.Text;
            if (ddlStatus.SelectedItem.Text.Trim() != "ALL")
                Kriteria = Kriteria + "  and isnull(C.Verifikasi,0)=" + ddlStatus.SelectedItem.Value;
            arrrmm = rmmf.RetrieveByKriteria(Kriteria);
            GridView1.DataSource = arrrmm;
            GridView1.DataBind();
            lstRMMxx.DataSource = arrrmm;
            lstRMMxx.DataBind();
        }
        private void LoadSarmutDept()
        {
            ddlSarmutDept.Items.Clear();
            Users users = (Users)Session["Users"];
            UsersFacade usersFacade = new UsersFacade();
            Users users2 = usersFacade.RetrieveByUserName(users.UserName);
            ArrayList arrSarmuDeptmn = new ArrayList();
            RMM_Departemen sarmutdeptmn = new RMM_Departemen();
            RMM_DepartemenFacade sarmutdeptmnFacade = new RMM_DepartemenFacade();
            arrSarmuDeptmn = sarmutdeptmnFacade.RetrieveByUserID(users2.ID);
            ddlSarmutDept.Items.Add(new ListItem("-- Pilih --", "0"));
            foreach (RMM_Departemen smtdptmn in arrSarmuDeptmn)
            {
                ddlSarmutDept.Items.Add(new ListItem(smtdptmn.SarMutDepartment.ToUpper().Trim(), smtdptmn.ID.ToString()));
            }
            Users user = ((Users)Session["Users"]);
            ddlSarmutDept.SelectedValue = sarmutdeptmn.ID.ToString();
        }


        private void LoadLoc()
        {
            ddlLoc.Items.Clear();
            ArrayList arrRMMLoc = new ArrayList();
            //RMM_Loc rmml = new RMM_Loc();
            RMM_LocFacade rmmlf = new RMM_LocFacade();
            arrRMMLoc = rmmlf.RetrieveBysarDeptId(int.Parse(ddlSarmutDept.SelectedValue));
            ddlLoc.Items.Add(new ListItem("-- Pilih -- ", "0"));
            foreach (RMM_Loc rmmloc in arrRMMLoc)
            {
                ddlLoc.Items.Add(new ListItem(rmmloc.Loc.ToUpper().Trim(), rmmloc.ID.ToString()));
            }

        }

        private void LoadSarmutPerusahaan()
        {
            ddlSarmutPershn.Items.Clear();
            Users users = (Users)Session["Users"];
            UsersFacade usersFacade = new UsersFacade();
            Users users2 = usersFacade.RetrieveByUserName(users.UserName);
            ArrayList arrSarMurPershn = new ArrayList();
            RMM_Perusahaan sarMut_Perusahaan = new RMM_Perusahaan();
            RMM_PerusahaanFacade sarMut_PerusahaanFacade = new RMM_PerusahaanFacade();
            arrSarMurPershn = sarMut_PerusahaanFacade.RetrieveByUserID(users2.ID);
            foreach (RMM_Perusahaan sarmutPershan in arrSarMurPershn)
            {
                ddlSarmutPershn.Items.Add(new ListItem(sarmutPershan.SarMutPerusahaan.ToUpper().Trim(), sarmutPershan.ID.ToString()));
            }
            Users user = ((Users)Session["Users"]);
            ddlSarmutPershn.SelectedValue = sarMut_Perusahaan.ID.ToString();
        }

        private void LoadDimensi()
        {
            ddlDimensi.Items.Clear();
            Users users = (Users)Session["Users"];
            UsersFacade usersFacade = new UsersFacade();
            Users users2 = usersFacade.RetrieveByUserName(users.UserName);
            ArrayList arrSarMurPershn = new ArrayList();
            RMM_Perusahaan sarMut_Perusahaan = new RMM_Perusahaan();
            RMM_PerusahaanFacade sarMut_PerusahaanFacade = new RMM_PerusahaanFacade();
            arrSarMurPershn = sarMut_PerusahaanFacade.RetrieveByUserID(users2.ID);
            foreach (RMM_Perusahaan sarmutPershan in arrSarMurPershn)
            {
                ddlDimensi.Items.Add(new ListItem(sarmutPershan.Dimensi.ToUpper().Trim(), sarmutPershan.ID.ToString()));

            }
            ddlDimensi.SelectedValue = sarMut_Perusahaan.ID.ToString();
        }

        private void ClearForm()
        {

            ViewState["id"] = null;
            Session.Remove("id");
            Session["RMM_No"] = null;
            Session["ListOfRMMDetail"] = null;
            txtRMM_No.Text = string.Empty;
            chkMesin.Checked = false;
            chkMaterial.Checked = false;
            chkMetode.Checked = false;
            chkLingkungan.Checked = false;
            chkManusia.Checked = false;
            chkMesin.Enabled = true;
            chkMaterial.Enabled = true;
            chkMetode.Enabled = true;
            chkLingkungan.Enabled = true;
            chkManusia.Enabled = true;
            txtAktivitas.Text = string.Empty;
            txtPelaku.Text = string.Empty;
            ddlSarmutPershn.SelectedIndex = 0;
            ddlDimensi.SelectedIndex = 0;
            ddlSarmutDept.SelectedIndex = 0;
            ddlBulan.SelectedIndex = 0;
            ddlTargetM.SelectedIndex = 0;
            chkCancel.Checked = false;
            txtActualJS.Text = DateTime.Now.ToString("dd-MM-yyyy");
            lbAddItem.Enabled = true;
            ArrayList arrData = new ArrayList();
            lstRMM.DataSource = arrData;
            lstRMM.DataBind();
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            ClearForm();
            Panel9.Visible = false;
            PanelLampiran.Visible = false;
            TableRepeat.Visible = true;
            btnUpdate.Disabled = false;
        }



        protected void btnList_ServerClick(object sender, EventArgs e)
        {
            if (btnList.Value == "List RMM")
            {
                PanelForm.Visible = false;
                btnNew.Visible = false;
                btnUpdate.Visible = false;
                btnLampiran.Visible = false;
                btnPrint.Visible = false;
                btnList.Value = "FormRMM";
                LJudul.Text = "List RMM";
                PanelList.Visible = true;
                PanelStatus.Visible = false;
                LoadData();
            }
            else
            {
                PanelForm.Visible = true;
                btnNew.Visible = true;
                btnUpdate.Visible = true;
                btnLampiran.Visible = true;
                btnPrint.Visible = true;
                btnList.Value = "List RMM";
                LJudul.Text = "Input Data RMM";
                PanelList.Visible = false;
                PanelStatus.Visible = true;
            }
        }

        private void LoadData()
        {
            Domain.RMM rmm = new Domain.RMM();
            RMMFacade rmmf = new RMMFacade();
            ArrayList arrrmm = new ArrayList();

            Users user = ((Users)Session["Users"]);
            RMMFacade rmmf1 = new RMMFacade();
            RMM_Dept dpt = new RMM_Dept();
            RMM_DeptFacade dptf = new RMM_DeptFacade();
            dpt = dptf.GetUserDept(user.ID);
            if (dpt.Departemen.Trim() == "IT" || dpt.Departemen.Trim() == "ISO")
            {
                arrrmm = rmmf.Retrieve();
                Session["arrsmt"] = arrrmm;

                GridView1.DataSource = arrrmm;
                GridView1.DataBind();
                lstRMMxx.DataSource = arrrmm;
                lstRMMxx.DataBind();
                filter.Visible = true;
            }
            else
            {
                LoadRMMByDept(dpt.Departemen);
                filter.Visible = false;
            }

        }

        protected void LoadRMMByDept(string dep)
        {
            Domain.RMM rmm = new Domain.RMM();
            RMMFacade rmmf = new RMMFacade();
            ArrayList arrrmm = new ArrayList();
            string Kriteria = string.Empty;

            Kriteria = " and A.RMM_DeptID in (select ID from RMM_Dept where departemen='" + dep + "')";
            arrrmm = rmmf.RetrieveByKriteria(Kriteria);
            GridView1.DataSource = arrrmm;
            GridView1.DataBind();
            lstRMMxx.DataSource = arrrmm;
            lstRMMxx.DataBind();

        }

        protected void BtnUpdate_ServerClick(object sender, EventArgs e)
        {
            Users user = ((Users)Session["Users"]);
            int bln = DateTime.Parse(txttgl.Text).Month;
            int thn = DateTime.Parse(txttgl.Text).Year;

            string strValidate = ValidateText();
            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }

            string strEvent = "Insert";
            Domain.RMM rmm = new Domain.RMM();

            if (Session["ID"] != null)
            {
                strEvent = "Edit";
            }
            string rMM_no = string.Empty;
            RMMFacade rMMFacade = new RMMFacade();
            string tahun = DateTime.Parse(txtRMM_Date0.Text).Year.ToString();
            string bulan = DateTime.Parse(txtRMM_Date0.Text).Month.ToString();
            string hun = tahun.Substring(2, 2);
            int urutan = rMMFacade.GetLastUrutan(Convert.ToInt32(tahun)) + 1;
            string bulanR = Global.ConvertNumericToRomawi(Convert.ToInt32(bulan));
            CompanyFacade companyFacade = new CompanyFacade();
            string ErMM = " ";
            string code = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID);
            rMM_no = urutan.ToString().PadLeft(3, '0').Trim() + "/RMM/" + code + "/" + bulanR + "/" + hun;
            if (txtRMM_No.Text == string.Empty)
            {
                txtRMM_No.Text = rMM_no;
                ErMM = rMM_no;
            }
            else
            {
                ErMM = txtRMM_No.Text;
                txtRMM_No.Text = txtRMM_No.Text.Substring(0, txtRMM_No.Text.Trim().Length - 5) +
                    txtRMM_No.Text.Substring(txtRMM_No.Text.Trim().Length - 3, 3);
            }
            rmm.RMM_No = txtRMM_No.Text;
            rmm.Tgl_RMM = DateTime.Parse(txtRMM_Date0.Text);
            rmm.RMM_DeptID = Convert.ToInt32(ddlDeptName.SelectedValue);
            rmm.RMM_Dimensi = Convert.ToInt32(ddlDimensi.SelectedValue);
            rmm.RMM_Perusahaan = Convert.ToInt32(ddlSarmutPershn.SelectedValue);
            rmm.Status = 0;
            rmm.Apv = 0;
            rmm.User_ID = user.ID;
            rmm.CreatedBy = user.UserName;

            string strError = string.Empty;
            ArrayList arrListRDetail = new ArrayList();
            if (Session["ListOfRMMDetail"] != null)
            {
                arrListRDetail = (ArrayList)Session["ListOfRMMDetail"];
            }

            RMMProcessFacade rmmProcessFacade = new RMMProcessFacade(rmm, arrListRDetail);
            if (rmm.ID > 0)
            {

            }
            else
            {
                strError = rmmProcessFacade.Insert();
                if (strError == string.Empty)
                {
                    txtRMM_No.Text = txtRMM_No.Text;
                    Session["ID"] = rmm.ID;
                    Session["RMM_No"] = txtRMM_No.Text;

                }
            }

            if (strError == string.Empty)
            {
                InsertLog(strEvent);
                btnUpdate.Disabled = true;
                if (strEvent == "Edit")
                    ClearForm();
            }


        }


        private string ValidateText()
        {
            ArrayList arrListRDetail = new ArrayList();
            if (Session["ListOfAdjustDetail"] != null)
            {
                arrListRDetail = (ArrayList)Session["ListOfRMMDetail"];
                if (arrListRDetail.Count == 0)
                    return "Tidak ada List Item yang di-input";
            }
            return string.Empty;
        }

        protected void ddlDeptName_SelectedIndexChanged(object sender, EventArgs e)
        {

            LoadRMMByKriteria();
        }

        protected void ddlDimensi_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        protected void ddlSarmutPershn_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        protected void ddlSarmutDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSarmutDept.SelectedIndex > 0)
                LoadLoc();

        }

        protected void chkMesin_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMesin.Checked == true)
            {
                chkMetode.Enabled = false;
                chkManusia.Enabled = false;
                chkLingkungan.Enabled = false;
                chkMaterial.Enabled = false;
            }
            else
            {
                chkMetode.Enabled = true;
                chkManusia.Enabled = true;
                chkLingkungan.Enabled = true;
                chkMaterial.Enabled = true;
            }
        }

        protected void chkMetode_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMetode.Checked == true)
            {
                chkMesin.Enabled = false;
                chkManusia.Enabled = false;
                chkLingkungan.Enabled = false;
                chkMaterial.Enabled = false;
            }
            else
            {
                chkMesin.Enabled = true;
                chkManusia.Enabled = true;
                chkLingkungan.Enabled = true;
                chkMaterial.Enabled = true;
            }

        }

        protected void chkManusia_CheckedChanged(object sender, EventArgs e)
        {
            if (chkManusia.Checked == true)
            {
                chkMaterial.Enabled = false;
                chkLingkungan.Enabled = false;
                chkMetode.Enabled = false;
                chkMesin.Enabled = false;

            }
            else
            {
                chkMaterial.Enabled = true;
                chkLingkungan.Enabled = true;
                chkMetode.Enabled = true;
                chkMesin.Enabled = true;
            }
        }

        protected void chkMaterial_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMaterial.Checked == true)
            {
                chkManusia.Enabled = false;
                chkLingkungan.Enabled = false;
                chkMetode.Enabled = false;
                chkMesin.Enabled = false;

            }
            else
            {
                chkManusia.Enabled = true;
                chkLingkungan.Enabled = true;
                chkMetode.Enabled = true;
                chkMesin.Enabled = true;
            }
        }

        protected void chkLingkungan_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLingkungan.Checked == true)
            {
                chkManusia.Enabled = false;
                chkMaterial.Enabled = false;
                chkMetode.Enabled = false;
                chkMesin.Enabled = false;

            }
            else
            {
                chkManusia.Enabled = true;
                chkMaterial.Enabled = true;
                chkMetode.Enabled = true;
                chkMesin.Enabled = true;
            }
        }

       

        protected void btnLampiran_ServerClick(object sender, EventArgs e)
        {
            if (txtRMM_No.Text.Trim() != string.Empty)
            {
                if (btnLampiran.Value == "Lampiran")
                {
                    PanelForm.Visible = false;
                    btnNew.Visible = false;
                    btnUpdate.Visible = false;
                    btnLampiran.Visible = true;
                    btnPrint.Visible = false;
                    btnList.Visible = false;
                    btnLampiran.Value = "Form RMM";
                    LJudul.Text = "Lampiran";
                    PanelLampiran.Visible = true;
                    PanelList.Visible = false;
                    PanelStatus.Visible = false;
                    LoadLampiran(txtRMM_No.Text);
                }
                else
                {
                    PanelForm.Visible = true;
                    btnNew.Visible = true;
                    btnUpdate.Visible = true;
                    btnList.Visible = true;
                    btnLampiran.Visible = true;
                    btnPrint.Visible = true;
                    btnLampiran.Value = "Lampiran";
                    LJudul.Text = "Input Data RMM";
                    PanelLampiran.Visible = false;
                    PanelList.Visible = false;
                    PanelStatus.Visible = true;
                }
            }
        }

        protected void LoadLampiran(string Laporan_no)
        {
            ArrayList arrLampiran = new ArrayList();
            RMM_LampiranFacade lampiranF = new RMM_LampiranFacade();
            arrLampiran = lampiranF.RetrieveLampiranByNo(Laporan_no);
            GridLampiran.DataSource = arrLampiran;
            GridLampiran.DataBind();
            GridLampiran0.DataSource = arrLampiran;
            GridLampiran0.DataBind();
        }

        protected void BtnPrint_ServerClick(object sender, EventArgs e)
        {

            Domain.RMM rmm = new Domain.RMM();
            //DateTime JS = rmm.Jadwal_Selesai;
            Session["SMT"] = rmm.SMT;
            string strQuery = string.Empty;
            if (txtSmtr.Text == "Semester I")
            {
                Session["SMT"] = "Semester I";
                strQuery = " select DeptName,DimensiName,SmtPerusahaan,Lokasi,SDept,Aktivitas,Pelaku ,SumberDaya,RMM_No,SMT,CreatedBy,convert(char,Tgl_RMM,106)Tgl_RMM,Apv, " +
                                   //--Jadwal Selesai----
                                   " case when Bln_JS=1 and Week1JS=1 then 'X' else '' end [Month1-1], " +
                                   " case when Bln_JS=1 and Week1JS=2 then 'X' else '' end [Month1-2], " +
                                   " case when Bln_JS=1 and Week1JS=3 then 'X' else '' end [Month1-3], " +
                                   " case when Bln_JS=1 and Week1JS=4 then 'X' else '' end [Month1-4], " +
                                   " case when Bln_JS=2 and Week1JS=1 then 'X' else '' end [Month2-1], " +
                                   " case when Bln_JS=2 and Week1JS=2 then 'X' else '' end [Month2-2], " +
                                   " case when Bln_JS=2 and Week1JS=3 then 'X' else '' end [Month2-3], " +
                                   " case when Bln_JS=2 and Week1JS=4 then 'X' else '' end [Month2-4], " +
                                   " case when Bln_JS=3 and Week1JS=1 then 'X' else '' end [Month3-1], " +
                                   " case when Bln_JS=3 and Week1JS=2 then 'X' else '' end [Month3-2], " +
                                   " case when Bln_JS=3 and Week1JS=3 then 'X' else '' end [Month3-3], " +
                                   " case when Bln_JS=3 and Week1JS=4 then 'X' else '' end [Month3-4], " +
                                   " case when Bln_JS=4 and Week1JS=1 then 'X' else '' end [Month4-1], " +
                                   " case when Bln_JS=4 and Week1JS=2 then 'X' else '' end [Month4-2], " +
                                   " case when Bln_JS=4 and Week1JS=3 then 'X' else '' end [Month4-3], " +
                                   " case when Bln_JS=4 and Week1JS=4 then 'X' else '' end [Month4-4], " +
                                   " case when Bln_JS=5 and Week1JS=1 then 'X' else '' end [Month5-1], " +
                                   " case when Bln_JS=5 and Week1JS=2 then 'X' else '' end [Month5-2], " +
                                   " case when Bln_JS=5 and Week1JS=3 then 'X' else '' end [Month5-3], " +
                                   " case when Bln_JS=5 and Week1JS=4 then 'X' else '' end [Month5-4], " +
                                   " case when Bln_JS=6 and Week1JS=1 then 'X' else '' end [Month6-1], " +
                                   " case when Bln_JS=6 and Week1JS=2 then 'X' else '' end [Month6-2], " +
                                   " case when Bln_JS=6 and Week1JS=3 then 'X' else '' end [Month6-3], " +
                                   " case when Bln_JS=6 and Week1JS=4 then 'X' else '' end [Month6-4], " +

                                   // --Actual_selesai----
                                   " case when Bln_AcS=1 and Week2Acs=1 then 'X' else '' end [SlsW1-1], " +
                                   " case when Bln_AcS=1 and Week2Acs=2 then 'X' else '' end [SlsW1-2], " +
                                   " case when Bln_AcS=1 and Week2Acs=3 then 'X' else '' end [SlsW1-3], " +
                                   " case when Bln_AcS=1 and Week2Acs=4 then 'X' else '' end [SlsW1-4], " +
                                   " case when Bln_AcS=2 and Week2Acs=1 then 'X' else '' end [SlsW2-1], " +
                                   " case when Bln_AcS=2 and Week2Acs=2 then 'X' else '' end [SlsW2-2], " +
                                   " case when Bln_AcS=2 and Week2Acs=3 then 'X' else '' end [SlsW2-3], " +
                                   " case when Bln_AcS=2 and Week2Acs=4 then 'X' else '' end [SlsW2-4], " +
                                   " case when Bln_AcS=3 and Week2Acs=1 then 'X' else '' end [SlsW3-1], " +
                                   " case when Bln_AcS=3 and Week2Acs=2 then 'X' else '' end [SlsW3-2], " +
                                   " case when Bln_AcS=3 and Week2Acs=3 then 'X' else '' end [SlsW3-3], " +
                                   " case when Bln_AcS=3 and Week2Acs=4 then 'X' else '' end [SlsW3-4], " +
                                   " case when Bln_AcS=4 and Week2Acs=1 then 'X' else '' end [SlsW4-1], " +
                                   " case when Bln_AcS=4 and Week2Acs=2 then 'X' else '' end [SlsW4-2], " +
                                   " case when Bln_AcS=4 and Week2Acs=3 then 'X' else '' end [SlsW4-3], " +
                                   " case when Bln_AcS=4 and Week2Acs=4 then 'X' else '' end [SlsW4-4], " +
                                   " case when Bln_AcS=5 and Week2Acs=1 then 'X' else '' end [SlsW5-1], " +
                                   " case when Bln_AcS=5 and Week2Acs=2 then 'X' else '' end [SlsW5-2], " +
                                   " case when Bln_AcS=5 and Week2Acs=3 then 'X' else '' end [SlsW5-3], " +
                                   " case when Bln_AcS=5 and Week2Acs=4 then 'X' else '' end [SlsW5-4], " +
                                   " case when Bln_AcS=6 and Week2Acs=1 then 'X' else '' end [SlsW6-1], " +
                                   " case when Bln_AcS=6 and Week2Acs=2 then 'X' else '' end [SlsW6-2], " +
                                   " case when Bln_AcS=6 and Week2Acs=3 then 'X' else '' end [SlsW6-3], " +
                                   " case when Bln_AcS=6 and Week2Acs=4 then 'X' else '' end [SlsW6-4] " +

                                   " from( " +
                                   " select DeptName,DimensiName,SmtPerusahaan,SDept,Lokasi,Aktivitas,Pelaku,SumberDaya,RMM_No,Bln_JS,Bln_AcS,Week1JS,Week2Acs,SMT,CreatedBy,Tgl_RMM,Apv from ( " +
                                   " select (select Departemen from RMM_Dept where ID=A.RMM_DeptID )as DeptName,A.RMM_No,A.Tgl_RMM, " +
                                   " case when DAY(C.Jadwal_Selesai) between 1 and 7 then 1 when DAY(C.Jadwal_Selesai) between 8 and 14 then 2  " +
                                   " when DAY(C.Jadwal_Selesai) between 15 and 21 then 3 else 4 end Week1JS, " +
                                   " case when DAY(C.Aktual_Selesai) between 1 and 7 then 1 when DAY(C.Aktual_Selesai) between 8 and 14 then 2  " +
                                   " when DAY(C.Aktual_Selesai) between 15 and 21 then 3 else 4 end Week2Acs, " +
                                   " month(C.Jadwal_Selesai) Bln_JS, " +
                                   " MONTH(C.Aktual_Selesai)Bln_AcS, " +
                                   " case when MONTH(C.Jadwal_Selesai)between 1 and 6 then 'Semester I' else 'Semester II' end SMT, " +
                                   " convert(char,C.Jadwal_Selesai,103)Jadwal_Selesai,A.CreatedBy,  " +
                                   " isnull(C.Aktual_Selesai,'1/1/1900')Aktual_Selesai, " +
                                                                 " isnull((select Loc from RMM_Loc where ID=C.RMM_LocID),' ') as Lokasi, " +
                                                                 " (select SarMutPerusahaan from RMM_Perusahaan where ID=A.RMM_Perusahaan) as SmtPerusahaan, " +
                                                                 " (select Dimensi from RMM_Perusahaan where ID=A.RMM_Dimensi) as DimensiName, " +
                                                                 " (select SarMutDepartment from RMM_Departemen where ID=C.RMM_Dept) as SDept,C.Aktivitas,C.Pelaku, " +
                                                                 " (select Penyebab from  RMM_Penyebab where ID=C.RMM_SumberDayaID)as SumberDaya,A.Status, " +
                                                                 " A.Apv, case when Apv=2 then 'Open' when Apv IS NULL then 'Open' when Apv=3 then 'PM/Corp Mgr' end Approval, " +
                                                                 " isnull(C.TglVerifikasi,'1/1/1900')TglVerifikasi,ISNULL(C.Verifikasi,0)Verifikasi,isnull(C.Solved,0)Solved, " +
                                                                 " isnull(C.Solved_Date,'1/1/1900')Solve_Date " +
                                                                 " from RMM A inner join RMM_Detail C on A.ID=C.RMM_ID and A.RowStatus>-1 and C.RowStatus>-1 where RMM_No='" + txtRMM_No.Text.Trim() + "' " +
                                  " )s )t ";
            }
            else
            {
                Session["SMT"] = "Semester II";
                strQuery = " select DeptName,DimensiName,SmtPerusahaan,Lokasi,SDept,Aktivitas,Pelaku ,SumberDaya,RMM_No,SMT,CreatedBy,convert(char,Tgl_RMM,106)Tgl_RMM,Apv, " +
                                   //--Jadwal Selesai----
                                   " case when Bln_JS=7 and Week1JS=1 then 'X' else '' end [Month1-1], " +
                                   " case when Bln_JS=7 and Week1JS=2 then 'X' else '' end [Month1-2], " +
                                   " case when Bln_JS=7 and Week1JS=3 then 'X' else '' end [Month1-3], " +
                                   " case when Bln_JS=7 and Week1JS=4 then 'X' else '' end [Month1-4], " +
                                   " case when Bln_JS=8 and Week1JS=1 then 'X' else '' end [Month2-1], " +
                                   " case when Bln_JS=8 and Week1JS=2 then 'X' else '' end [Month2-2], " +
                                   " case when Bln_JS=8 and Week1JS=3 then 'X' else '' end [Month2-3], " +
                                   " case when Bln_JS=8 and Week1JS=4 then 'X' else '' end [Month2-4], " +
                                   " case when Bln_JS=9 and Week1JS=1 then 'X' else '' end [Month3-1], " +
                                   " case when Bln_JS=9 and Week1JS=2 then 'X' else '' end [Month3-2], " +
                                   " case when Bln_JS=9 and Week1JS=3 then 'X' else '' end [Month3-3], " +
                                   " case when Bln_JS=9 and Week1JS=4 then 'X' else '' end [Month3-4], " +
                                   " case when Bln_JS=10 and Week1JS=1 then 'X' else '' end [Month4-1], " +
                                   " case when Bln_JS=10 and Week1JS=2 then 'X' else '' end [Month4-2], " +
                                   " case when Bln_JS=10 and Week1JS=3 then 'X' else '' end [Month4-3], " +
                                   " case when Bln_JS=10 and Week1JS=4 then 'X' else '' end [Month4-4], " +
                                   " case when Bln_JS=11 and Week1JS=1 then 'X' else '' end [Month5-1], " +
                                   " case when Bln_JS=11 and Week1JS=2 then 'X' else '' end [Month5-2], " +
                                   " case when Bln_JS=11 and Week1JS=3 then 'X' else '' end [Month5-3], " +
                                   " case when Bln_JS=11 and Week1JS=4 then 'X' else '' end [Month5-4], " +
                                   " case when Bln_JS=12 and Week1JS=1 then 'X' else '' end [Month6-1], " +
                                   " case when Bln_JS=12 and Week1JS=2 then 'X' else '' end [Month6-2], " +
                                   " case when Bln_JS=12 and Week1JS=3 then 'X' else '' end [Month6-3], " +
                                   " case when Bln_JS=12 and Week1JS=4 then 'X' else '' end [Month6-4], " +

                                   // --Actual_selesai----
                                   " case when Bln_AcS=7 and Week2Acs=1 then 'X' else '' end [SlsW1-1], " +
                                   " case when Bln_AcS=7 and Week2Acs=2 then 'X' else '' end [SlsW1-2], " +
                                   " case when Bln_AcS=7 and Week2Acs=3 then 'X' else '' end [SlsW1-3], " +
                                   " case when Bln_AcS=7 and Week2Acs=4 then 'X' else '' end [SlsW1-4], " +
                                   " case when Bln_AcS=8 and Week2Acs=1 then 'X' else '' end [SlsW2-1], " +
                                   " case when Bln_AcS=8 and Week2Acs=2 then 'X' else '' end [SlsW2-2], " +
                                   " case when Bln_AcS=8 and Week2Acs=3 then 'X' else '' end [SlsW2-3], " +
                                   " case when Bln_AcS=8 and Week2Acs=4 then 'X' else '' end [SlsW2-4], " +
                                   " case when Bln_AcS=9 and Week2Acs=1 then 'X' else '' end [SlsW3-1], " +
                                   " case when Bln_AcS=9 and Week2Acs=2 then 'X' else '' end [SlsW3-2], " +
                                   " case when Bln_AcS=9 and Week2Acs=3 then 'X' else '' end [SlsW3-3], " +
                                   " case when Bln_AcS=9 and Week2Acs=4 then 'X' else '' end [SlsW3-4], " +
                                   " case when Bln_AcS=10 and Week2Acs=1 then 'X' else '' end [SlsW4-1], " +
                                   " case when Bln_AcS=10 and Week2Acs=2 then 'X' else '' end [SlsW4-2], " +
                                   " case when Bln_AcS=10 and Week2Acs=3 then 'X' else '' end [SlsW4-3], " +
                                   " case when Bln_AcS=10 and Week2Acs=4 then 'X' else '' end [SlsW4-4], " +
                                   " case when Bln_AcS=11 and Week2Acs=1 then 'X' else '' end [SlsW5-1], " +
                                   " case when Bln_AcS=11 and Week2Acs=2 then 'X' else '' end [SlsW5-2], " +
                                   " case when Bln_AcS=11 and Week2Acs=3 then 'X' else '' end [SlsW5-3], " +
                                   " case when Bln_AcS=11 and Week2Acs=4 then 'X' else '' end [SlsW5-4], " +
                                   " case when Bln_AcS=12 and Week2Acs=1 then 'X' else '' end [SlsW6-1], " +
                                   " case when Bln_AcS=12 and Week2Acs=2 then 'X' else '' end [SlsW6-2], " +
                                   " case when Bln_AcS=12 and Week2Acs=3 then 'X' else '' end [SlsW6-3], " +
                                   " case when Bln_AcS=12 and Week2Acs=4 then 'X' else '' end [SlsW6-4] " +

                                   " from( " +
                                   " select DeptName,DimensiName,SmtPerusahaan,SDept,Lokasi,Aktivitas,Pelaku,SumberDaya,RMM_No,Bln_JS,Bln_AcS,Week1JS,Week2Acs,SMT,CreatedBy,Tgl_RMM,Apv from ( " +
                                   " select (select Departemen from RMM_Dept where ID=A.RMM_DeptID )as DeptName,A.RMM_No,A.Tgl_RMM, " +
                                   " case when DAY(C.Jadwal_Selesai) between 1 and 7 then 1 when DAY(C.Jadwal_Selesai) between 8 and 14 then 2  " +
                                   " when DAY(C.Jadwal_Selesai) between 15 and 21 then 3 else 4 end Week1JS, " +
                                   " case when DAY(C.Aktual_Selesai) between 1 and 7 then 1 when DAY(C.Aktual_Selesai) between 8 and 14 then 2  " +
                                   " when DAY(C.Aktual_Selesai) between 15 and 21 then 3 else 4 end Week2Acs, " +
                                   " month(C.Jadwal_Selesai) Bln_JS, " +
                                   " MONTH(C.Aktual_Selesai)Bln_AcS, " +
                                   " case when MONTH(C.Jadwal_Selesai)between 1 and 6 then 'Semester I' else 'Semester II' end SMT, " +
                                   " convert(char,C.Jadwal_Selesai,103)Jadwal_Selesai,A.CreatedBy,  " +
                                   " isnull(C.Aktual_Selesai,'1/1/1900')Aktual_Selesai, " +
                                                                 " isnull((select Loc from RMM_Loc where ID=C.RMM_LocID),' ') as Lokasi, " +
                                                                 " (select SarMutPerusahaan from RMM_Perusahaan where ID=A.RMM_Perusahaan) as SmtPerusahaan, " +
                                                                 " (select Dimensi from RMM_Perusahaan where ID=A.RMM_Dimensi) as DimensiName, " +
                                                                 " (select SarMutDepartment from RMM_Departemen where ID=C.RMM_Dept) as SDept,C.Aktivitas,C.Pelaku, " +
                                                                 " (select Penyebab from  RMM_Penyebab where ID=C.RMM_SumberDayaID)as SumberDaya,A.Status, " +
                                                                 " A.Apv, case when Apv=2 then 'Open' when Apv IS NULL then 'Open' when Apv=3 then 'PM/Corp Mgr' end Approval, " +
                                                                 " isnull(C.TglVerifikasi,'1/1/1900')TglVerifikasi,ISNULL(C.Verifikasi,0)Verifikasi,isnull(C.Solved,0)Solved, " +
                                                                 " isnull(C.Solved_Date,'1/1/1900')Solve_Date " +
                                                                 " from RMM A inner join RMM_Detail C on A.ID=C.RMM_ID and A.RowStatus>-1 and C.RowStatus>-1 where RMM_No='" + txtRMM_No.Text.Trim() + "' " +
                                  " )s )t ";
            }

            if (txtRMM_No.Text.Trim() != string.Empty)
            {
                Session["query"] = strQuery;
                Session["xjudul"] = "Rencana Manajemen Mutu";
                Session["formno"] = "Form No. MR/RMM/16/02/R0";
                Session["namaPT"] = "PT.BANGUNPERKASA ADHITAMASENTRA";
                Cetak(this);
            }
        }

        static public void Cetak(Control page)
        {
            string myScript = "var wn = window.showModalDialog('Report.aspx?IdReport=rmm', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1100px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void GridLampiran_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName == "download")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridLampiran.Rows[index];
                string dirPath = @"D:\RMMfile\";
                HttpResponse response = HttpContext.Current.Response;
                string fName = row.Cells[1].Text;
                string atx = dirPath + fName;
                System.IO.FileInfo file = new System.IO.FileInfo(atx);
                if (file.Exists)
                {
                    Response.Clear();
                    Response.AppendHeader("content-disposition", "attachment; filename=" + file.Name);
                    response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = "application/octet-stream";
                    response.WriteFile(file.FullName);
                    Response.End();
                }

            }
            if (e.CommandName == "hapus")
            {
                RMM_LampiranFacade lampiranF = new RMM_LampiranFacade();
                string err = lampiranF.hapus(GridLampiran.Rows[rowindex].Cells[0].Text);
                LoadLampiran(txtRMM_No.Text);
            }
        }

        protected void GridLampiran_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }

        protected void GridLampiran0_Command(object sender, RepeaterCommandEventArgs e)
        {
            Repeater rpt = (Repeater)sender;
            try
            {
                switch (e.CommandName)
                {
                    case "downloadx":
                        string Nama = e.CommandArgument.ToString();
                        string Nama2 = @"\" + Nama;
                        string dirPath = @"D:\RMMfile\";
                        string ext = Path.GetExtension(Nama);

                        Response.Clear();
                        string excelFilePath = dirPath + Nama;
                        System.IO.FileInfo file = new System.IO.FileInfo(excelFilePath);
                        if (file.Exists)
                        {
                            Response.Clear();
                            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                            Response.AddHeader("Content-Length", file.Length.ToString());
                            Response.ContentType = "application/octet-stream";
                            Response.WriteFile(file.FullName);
                            Response.End();
                        }
                        break;
                    case "hapusatt":
                        Image hps = (Image)rpt.Items[int.Parse(e.CommandArgument.ToString())].FindControl("hapusatt");
                        ZetroView zl = new ZetroView();
                        zl.QueryType = Operation.CUSTOM;
                        zl.CustomQuery = "Update RMM_Lampiran set RowStatus=-1 where ID=" + hps.CssClass;
                        SqlDataReader sdr = zl.Retrieve();
                        break;
                }
            }
            catch
            {
                DisplayAJAXMessage(this, "data gagal di hapus");
                return;
            }
        }

        protected void chk_CheckedChangePrs(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            if (chk.Checked == true)
            {
                btnHapus.Disabled = false;

            }
            else if (chk.Checked == false)
            {
                btnHapus.Disabled = true;
            }
        }

        protected void btnHapus_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GridLampiran0.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)GridLampiran0.Items[i].FindControl("chkprs");
                if (chk.Checked == true)
                {
                    ZetroView zl = new ZetroView();
                    zl.QueryType = Operation.CUSTOM;
                    zl.CustomQuery = "Update RMM_Lampiran set RowStatus=-1 where ID=" + chk.ToolTip.ToString();
                    SqlDataReader sdr = zl.Retrieve();
                    LoadLampiran(txtRMM_No.Text);
                    btnHapus.Disabled = true;
                }
            }
        }

        protected void GridLampiran0_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ScriptManager scriptMan = ScriptManager.GetCurrent(this.Page);

            Users users = (Users)Session["Users"];
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ImageButton btn = (ImageButton)e.Item.FindControl("lihatatt") as ImageButton;
                scriptMan.RegisterPostBackControl(btn);
                Image pre = (Image)e.Item.FindControl("lihatatt");
            }
        }
        static public void PriviewPdf(Control page, string ID)
        {
            string myScript = "var wn = window.showModalDialog('../../ModalDialog/PdfPreviewRMM.aspx?ba=" + ID + "', 'Preview', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void btnUpload0_ServerClick(object sender, EventArgs e)
        {
            LoadLampiran(txtRMM_No.Text);
        }

        protected void btnUpload_ServerClick(object sender, EventArgs e)
        {
            Domain.RMM rmm = new Domain.RMM();
            RMMFacade rmmdf = new RMMFacade();
            rmm = rmmdf.RetrieveByNo(txtRMM_No.Text);
            UploadPdf(this, rmm.ID.ToString());
        }

        static public void UploadPdf(Control page, string ID)
        {
            string myScript = "var wn = window.showModalDialog('../../ModalDialog/UploadRMM.aspx?ba=" + ID + "', 'Preview', 'resizable:yes;dialogHeight: 200px; dialogWidth: 900px;scrollbars=no');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            if (txtSearch.Text != string.Empty)
            {
                FormData(txtSearch.Text);
                txtSearch.Text = string.Empty;
            }

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
           
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[14].Text == "0")
                {
                    e.Row.Cells[14].Text = "Open";
                }
                if (e.Row.Cells[14].Text == "1")
                {
                    e.Row.Cells[14].Text = "Closed";
                }
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
                FormData(GridView1.Rows[rowindex].Cells[4].Text);
                if (btnList.Value == "List RMM")
                {
                    PanelForm.Visible = false;
                    btnNew.Visible = false;
                    btnUpdate.Visible = false;
                    btnLampiran.Visible = false;
                    btnPrint.Visible = false;
                    btnList.Value = "Form RMM";
                    LJudul.Text = "List RMM";
                    PanelList.Visible = true;
                    PanelStatus.Visible = true;
                    LoadData();
                }
                else
                {
                    PanelForm.Visible = true;
                    TableRepeat.Visible = false;
                    btnNew.Visible = true;
                    btnUpdate.Visible = true;
                    btnLampiran.Visible = true;
                    btnPrint.Visible = true;
                    btnList.Value = "List RMM";
                    LJudul.Text = "Input Data RMM";
                    PanelList.Visible = false;
                    PanelStatus.Visible = true;
                }
            }

        }

        protected void FormData(string rmm_no)
        {
            ArrayList arrrmm = new ArrayList();
            Domain.RMM rmm = new Domain.RMM();
            RMMFacade rmmf = new RMMFacade();
            rmm = rmmf.RetrieveByNo(rmm_no);
            arrrmm.Add(rmm);
            GridView1.DataSource = arrrmm;
            GridView1.DataBind();
            lstRMMxx.DataSource = arrrmm;
            lstRMMxx.DataBind();
            ClearForm();
            txtRMM_No.Text = rmm.RMM_No;
            LblLaporanNo.Text = rmm.RMM_No;
            txtRMM_Date0.Text = rmm.Tgl_RMM.ToString("dd-MM-yyyy");
            LoadDetail(rmm_no);
            txtSmtr.Text = rmm.SMT;
            idX.Text = rmm.ID.ToString();
            lbAddItem.Enabled = false;
            btnUpdate.Disabled = true;

            if (Session["usertype"].ToString().Trim().ToUpper() == "ISO")
            {
                btnUpdate.Disabled = false;
                PanelStatus.Enabled = true;
                chksolved.Enabled = true;
                txtDateSolved.Enabled = true;
                txtDueDate.Enabled = true;
            }
            else
            {
                PanelStatus.Enabled = false;
                chksolved.Enabled = false;
                txtDateSolved.Enabled = false;
                txtDueDate.Enabled = false;
            }

        }

        protected void LoadDetail(string rmm_no)
        {

            if (rmm_no.Length == 1)
                return;
            Session["rmmDetail"] = null;
            ArrayList arrRMMDetail = new ArrayList();
            RMMDetailFacade rmdetail = new RMMDetailFacade();
            arrRMMDetail = rmdetail.RetrieveByNo(rmm_no);
            Session["rmmDetail"] = arrRMMDetail;
            GridRMMDetail.DataSource = arrRMMDetail;
            GridRMMDetail.DataBind();
        }



        protected void chkVerifikasi1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow gr = (GridViewRow)chk.NamingContainer;
            Label lDateVF = (Label)GridRMMDetail.Rows[gr.RowIndex].FindControl("lDateVF");
            TextBox txtDateVF = (TextBox)GridRMMDetail.Rows[gr.RowIndex].FindControl("txtDateVF");
            Label lDateAS = (Label)GridRMMDetail.Rows[gr.RowIndex].FindControl("lDateAS");
            string verf = string.Empty;
            if (chk.Checked == true)
                verf = "1";
            else
                verf = "0";
            RMMDetailFacade rmmdf = new RMMDetailFacade();
            // DateTime dt = DateTime.ParseExact(txtDateVF.Text, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            DateTime dt = DateTime.Parse(txtDateVF.Text.ToString());
            var tahun = dt.ToString("yyyyMMdd");
            string strerror = rmmdf.UpdateRMMDetailverf(GridRMMDetail.Rows[gr.RowIndex].Cells[1].Text, tahun, verf);
            LoadDetail(txtRMM_No.Text.Trim());
        }


        protected void GridRMMDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
            Users user = ((Users)Session["Users"]);
            CheckBox chkver = (CheckBox)GridRMMDetail.Rows[rowindex].FindControl("chkVerifikasi1");
            TextBox txtDateAS = (TextBox)GridRMMDetail.Rows[rowindex].FindControl("txtDateAS");
            Label lDateJS = (Label)GridRMMDetail.Rows[rowindex].FindControl("lDateJS");
            TextBox txtDateJS = (TextBox)GridRMMDetail.Rows[rowindex].FindControl("txtDateJS");
            Label lDateAS = (Label)GridRMMDetail.Rows[rowindex].FindControl("lDateAS");
            LinkButton btn = (LinkButton)GridRMMDetail.Rows[rowindex].Cells[6].Controls[0];
            LinkButton btntarget = (LinkButton)GridRMMDetail.Rows[rowindex].Cells[10].Controls[0];

            if (e.CommandName == "rubah")
            {

                if (btn.Text == "Simpan")
                {
                    btn.Text = "Edit";
                    RMMDetailFacade rmmdf = new RMMDetailFacade();
                    int awal = int.Parse(DateTime.Parse(txtDateJS.Text).ToString("yyyyMMdd"));
                    int akhir = int.Parse(DateTime.Parse(txtDateAS.Text).ToString("yyyyMMdd"));
                    if (awal < akhir)
                    {
                        txtDateAS.Visible = false;
                        lDateAS.Visible = true;
                        txtDateJS.Visible = false;
                        lDateJS.Visible = true;
                        DisplayAJAXMessage(this, "Tanggal aktual tidak boleh lebih besar dari Tanggal Jadwal ");
                        return;
                    }
                    string strerror = rmmdf.UpdateAktualSelesai(GridRMMDetail.Rows[rowindex].Cells[1].Text, awal.ToString(), akhir.ToString());
                    LoadDetail(txtRMM_No.Text.Trim());
                    txtDateAS.Visible = false;
                    lDateAS.Visible = true;
                    txtDateJS.Visible = false;
                    lDateJS.Visible = true;
                }
                else
                {
                    btn.Text = "Simpan";
                    txtDateAS.Visible = true;
                    lDateAS.Visible = false;
                    txtDateJS.Visible = true;
                    lDateJS.Visible = false;
                }
            }
            if (e.CommandName == "hapus")
            {
                RMMDetailFacade rmmDetail = new RMMDetailFacade();
                string strerror = rmmDetail.DeleteRMMDetail(GridRMMDetail.Rows[rowindex].Cells[1].Text);
                LoadDetail(txtRMM_No.Text.Trim());
            }
            if (e.CommandName == "target")
            {
                RMM_Detail rmmdtl = new RMM_Detail();
                RMMDetailFacade rmmDetailF = new RMMDetailFacade();
                Domain.RMM rmm = new Domain.RMM();
                RMMFacade rmmf = new RMMFacade();
                rmm = rmmf.RetrieveByNo(txtRMM_No.Text.Trim());
                rmmdtl.RMM_ID = rmm.ID;
                rmmdtl.Aktivitas = GridRMMDetail.Rows[rowindex].Cells[2].Text;
                rmmdtl.Pelaku = GridRMMDetail.Rows[rowindex].Cells[3].Text;
                rmmdtl.RMM_SumberDayaID = Convert.ToInt32(GridRMMDetail.Rows[rowindex].Cells[9].Text);
                rmmdtl.Target = "T" + (rmmDetailF.getTarget(txtRMM_No.Text.Trim(), rmmdtl.Aktivitas) + 1).ToString();
                rmmdtl.CreatedBy = user.UserName;
                string strerror = rmmDetailF.UpdateAktualSelesai(GridRMMDetail.Rows[rowindex].Cells[1].Text,
                    DateTime.Now.ToString("yyyyMMdd"), DateTime.MinValue.ToString("yyyyMMdd"));
                strerror = rmmDetailF.UpdateRMMDetailverf(GridRMMDetail.Rows[rowindex].Cells[1].Text, DateTime.Now.ToString("yyyyMMdd"), "1");
                TransactionManager transManager = new TransactionManager(Global.ConnectionString());
                transManager.BeginTransaction();
                AbstractTransactionFacadeF absTrans = new RMMDetailFacade(rmmdtl);
                int intresult = absTrans.Insert(transManager);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return;
                }
                transManager.CommitTransaction();
                transManager.CloseConnection();
                LoadDetail(txtRMM_No.Text.Trim());
            }
        }

        protected void GridRMMDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ArrayList arrmmdetail = new ArrayList();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkver = (CheckBox)e.Row.FindControl("chkVerifikasi1");
                if (Convert.ToInt32(e.Row.Cells[0].Text.Trim()) == 0)
                    chkver.Checked = false;
                else
                    chkver.Checked = true;
                if (Session["usertype"].ToString().Trim().ToUpper() == "ISO")
                    chkver.Enabled = true;
                else
                    chkver.Enabled = false;
                if (Session["rmmdetail"] != null)
                {
                    arrmmdetail = (ArrayList)Session["rmmdetail"];
                    RMM_Detail rmmdetail = (RMM_Detail)arrmmdetail[e.Row.RowIndex];
                    Label lDateJS = (Label)e.Row.FindControl("lDateJS");
                    TextBox txtDateJS = (TextBox)e.Row.FindControl("txtDateJS");
                    Label lDateAS = (Label)e.Row.FindControl("lDateAS");
                    TextBox txtDateAS = (TextBox)e.Row.FindControl("txtDateAS");
                    Label lDateVF = (Label)e.Row.FindControl("lDateVF");
                    TextBox txtDateVF = (TextBox)e.Row.FindControl("txtDateVF");
                    LinkButton btn = (LinkButton)e.Row.Cells[6].Controls[0];
                    ((System.Web.UI.WebControls.LinkButton)(e.Row.Cells[10].Controls[0])).ToolTip = "Klik untuk Hapus";
                    if (e.Row.Cells[0].Text.Trim() == "0")
                    {
                        btn.Enabled = true;
                    }
                    else
                    {
                        btn.Enabled = false;
                    }
                    if (rmmdetail.Jadwal_Selesai.ToString("yyyyMMdd") != "00010101" && rmmdetail.Jadwal_Selesai.ToString("yyyyMMdd") != "17530101")
                    {
                        lDateJS.Text = rmmdetail.Jadwal_Selesai.ToString("dd-MMM-yyyy");
                        txtDateJS.Text = rmmdetail.Jadwal_Selesai.ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        lDateJS.Text = "";
                        txtDateJS.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                    }

                    if (rmmdetail.Aktual_Selesai.ToString("yyyyMMdd") != "00010101" && rmmdetail.Aktual_Selesai.ToString("yyyyMMdd") != "17530101")
                    {
                        lDateAS.Text = rmmdetail.Aktual_Selesai.ToString("dd-MMM-yyyy");
                        txtDateAS.Text = rmmdetail.Aktual_Selesai.ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        lDateAS.Text = "";
                        txtDateAS.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                    }
                    if (rmmdetail.TglVerifikasi.ToString("yyyyMMdd") != "00010101" && rmmdetail.TglVerifikasi.ToString("yyyyMMdd") != "17530101")
                    {
                        lDateVF.Text = rmmdetail.TglVerifikasi.ToString("dd-MMM-yyyy");
                        txtDateVF.Text = rmmdetail.TglVerifikasi.ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        lDateVF.Text = "";
                        txtDateVF.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                    }
                    if (chkver.Checked == false && chkver.Enabled == true)
                    {
                        txtDateVF.Visible = true;
                        lDateVF.Visible = false;
                    }
                    else
                    {
                        txtDateVF.Visible = false;
                        lDateVF.Visible = true;
                    }
                }
            }
        }

        protected void lbAddItem_Click(object sender, EventArgs e)
        {
            #region validasi
            if (ddlSarmutDept.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Pilih Sarmut Departemen");
                return;
            }

            if (chkMesin.Checked == false && chkMaterial.Checked == false && chkMetode.Checked == false && chkLingkungan.Checked == false && chkManusia.Checked == false)
            {
                DisplayAJAXMessage(this, "Pilih SumberDaya.. ");
                return;
            }
            if (txtAktivitas.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Isi Aktivitas");
                return;
            }
            if (txtPelaku.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Isi Pelaku");
                return;
            }
            if (ddlTargetM.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Pilih Target M");
                return;
            }
            if (ddlBulan.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Pilih Bulan");
                return;
            }
            #endregion

            RMM_Detail rMM_Detail = new RMM_Detail();
            ArrayList arrListRDetail = new ArrayList();

            if (Session["ListOfRMMDetail"] != null)
            {
                arrListRDetail = (ArrayList)Session["ListOfRMMDetail"];
                if (arrListRDetail.Count > 0)
                {
                    int ada = 0;
                    foreach (RMM_Detail rmmdtl in arrListRDetail)
                    {
                        if (rmmdtl.Aktivitas == txtAktivitas.Text)
                        {
                            DisplayAJAXMessage(this, "data sudah ada di list");
                        }

                        ada = ada + 1;
                    }
                }
            }

            int tgl = 0;
            int bln = 0;
            int thn = int.Parse(ddlTahunInput.SelectedValue);

            if (ddlTargetM.SelectedIndex == 1)
            {
                tgl = 7;
            }
            else if (ddlTargetM.SelectedIndex == 2)
            {
                tgl = 14;
            }
            else if (ddlTargetM.SelectedIndex == 3)
            {
                tgl = 21;
            }
            else if (ddlTargetM.SelectedIndex == 4)
            {
                DateTime final = new DateTime(int.Parse(ddlTahunInput.SelectedValue), ddlBulan.SelectedIndex, DateTime.DaysInMonth(int.Parse(ddlTahunInput.SelectedValue), ddlBulan.SelectedIndex));

                tgl = final.Day;
            }
            else
            {
                tgl = DateTime.Parse(txttgl.Text).Day;
            }

            DateTime TglTarget = new DateTime(int.Parse(ddlTahunInput.SelectedValue), ddlBulan.SelectedIndex, tgl, 0, 0, 0);

            rMM_Detail.Aktivitas = txtAktivitas.Text;
            rMM_Detail.Pelaku = txtPelaku.Text;
            rMM_Detail.Jadwal_Selesai = TglTarget;
            rMM_Detail.RowStatus = 0;
            rMM_Detail.RMM_LocID = Convert.ToInt32(ddlLoc.SelectedValue);
            rMM_Detail.RMM_Dept = Convert.ToInt32(ddlSarmutDept.SelectedValue);
            int RMM_SumberDaya = 0;
            string SumberDaya = string.Empty;
            if (chkMesin.Checked == true)
                RMM_SumberDaya = 1;
            if (chkMaterial.Checked == true)
                RMM_SumberDaya = 2;
            if (chkMetode.Checked == true)
                RMM_SumberDaya = 3;
            if (chkLingkungan.Checked == true)
                RMM_SumberDaya = 4;
            if (chkManusia.Checked == true)
                RMM_SumberDaya = 5;
            rMM_Detail.RMM_SumberDayaID = RMM_SumberDaya;

            arrListRDetail.Add(rMM_Detail);
            Session["ListOfRMMDetail"] = arrListRDetail;
            btnUpdate.Disabled = false;
            lstRMM.DataSource = arrListRDetail;
            lstRMM.DataBind();


        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void lstRMM_Command(object sender, RepeaterCommandEventArgs e)
        {

        }

        protected void lstRMM_Databound(object sender, RepeaterItemEventArgs e)
        {
            RMM_Detail rMMdtl = (RMM_Detail)e.Item.DataItem;
            Label lbl = (Label)e.Item.FindControl("Sumberdaya");
            switch (rMMdtl.RMM_SumberDayaID)
            {
                case 1:
                    lbl.Text = "Mesin";
                    break;
                case 2:
                    lbl.Text = "Material";
                    break;
                case 3:
                    lbl.Text = "Metode";
                    break;
                case 4:
                    lbl.Text = "Lingkungan";
                    break;
                case 5:
                    lbl.Text = "Manusia";
                    break;
                default:
                    lbl.Text = "";
                    break;
            }
        }

        protected void lstRMMxx_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            int ID = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "edit")
            {
                FormData(GridView1.Rows[ID].Cells[0].Text);
                if (btnList.Value == "List RMM")
                {
                    PanelForm.Visible = false;
                    btnNew.Visible = false;
                    btnUpdate.Visible = false;
                    btnLampiran.Visible = false;
                    btnPrint.Visible = false;
                    btnList.Value = "Form RMM";
                    LJudul.Text = "List RMM";
                    PanelList.Visible = true;
                    PanelStatus.Visible = true;
                    LoadData();
                }
                else
                {
                    PanelForm.Visible = true;
                    TableRepeat.Visible = false;
                    btnNew.Visible = true;
                    btnUpdate.Visible = true;
                    btnLampiran.Visible = true;
                    btnPrint.Visible = true;
                    btnList.Value = "List RMM";
                    LJudul.Text = "Input Data RMM";
                    PanelList.Visible = false;
                    PanelStatus.Visible = true;
                }
            }
        }

        protected void lstRMMxx_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ArrayList arrData = new ArrayList();
            Domain.RMM rMM = (Domain.RMM)e.Item.DataItem;
            RMM_Detail rmdt = new RMM_Detail();
            string[] arrDept = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("OnlyDeptID", "RMM").Split(',');
            Label lbl = (Label)e.Item.FindControl("slvd");
            Label lblx = (Label)e.Item.FindControl("verf");
            switch (rMM.Solved)
            {
                case 0:
                    lbl.Text = "UnSolved";
                    break;
                case 1:
                    lbl.Text = "Solved";
                    break;
                default:
                    lbl.Text = "";
                    break;
            }

            switch (rMM.Verifikasi)
            {
                case 0:
                    lblx.Text = "Open";
                    break;
                case 1:
                    lblx.Text = "Closed";
                    break;
                default:
                    lblx.Text = "";
                    break;
            }
        }

        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "RMM";
            eventLog.EventName = eventName;
            eventLog.DocumentNo = txtRMM_No.Text;
            eventLog.CreatedBy = ((Users)Session["Users"]).UserName;

            EventLogFacade eventLogFacade = new EventLogFacade();
            int intResult = eventLogFacade.Insert(eventLog);
        }
    }
}