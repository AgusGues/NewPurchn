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
using System.Drawing;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
//using DefectFacade;
using Factory;
using DataAccessLayer;
using System.IO;
using BasicFrame.WebControls;

namespace GRCweb1.Modul.TPP
{
    public partial class NewFormTPP : System.Web.UI.Page
    {
        public string Tahun = Global.nBulan(DateTime.Now.Month);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) 
            {
                Global.link = "~/Default.aspx";
                txtTPP_Date0.SelectedDate = DateTime.Now;
                txtTPP_Date.SelectedDate = DateTime.Now;
                LoadDept();
                LoadDept0();
                LoadMasalah();
                //LoadTPPBagian();
                //LoadBulan();
                LoadTahun();
                LoadKlausul();
                Users user = ((Users)Session["Users"]);
                TPP_Facade tppFacade = new TPP_Facade();
                Domain.TPP tpp = new Domain.TPP(); 
                LoadTPP();

                string UserID = string.Empty;
                UserID = user.ID.ToString();
                string Apv = tppFacade.GetApv(UserID);
                if (Convert.ToInt32(Apv) > 0)
                    btnUpdate.Disabled = true;
                Session["masalah"] = " ";
                string usertype = tppFacade.GetUserType(UserID);
                Session["usertype"] = usertype;
                LoadYear();
                LoadLampiran(TxtLaporanNo.Text);
                ClearForm();
                Rb4.Attributes.Add("onclick", "openWindow()");
                //if (((Users)Session["Users"]).DeptID == 23)
                //{
                //    GridTPP.Columns[11].Visible = true;
                //}
                // munculBtnSimpan();
                PanelHSE.Enabled = false;
                PanelHSE.Visible = false;
            }
            //((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(LinkButton3);
        }

        private void LoadTahun()
        {
            TPP_Facade p = new TPP_Facade();
            ArrayList arrData = new ArrayList();
            arrData = p.GetYearTPP();
            ddlTahun.Items.Clear();
            if (arrData.Count == 0)
            {
                ddlTahun.Items.Add(new ListItem(DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString()));
            }
            foreach (Domain.TPP tpp in arrData)
            {
                ddlTahun.Items.Add(new ListItem(tpp.Tahun.ToString(), tpp.Tahun.ToString()));
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();

        }

        private void LoadBulan()
        {
            ddlBulan.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i), i.ToString()));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString();

        }

        protected void LoadTPP()
        {
            Domain.TPP tpp = new Domain.TPP();
            TPP_Facade tppf = new TPP_Facade();
            ArrayList arrtpp = new ArrayList();

            Users user = ((Users)Session["Users"]);
            TPP_Facade tppFacade = new TPP_Facade();
            //LoadTPP();
            TPP_Dept dpt = new TPP_Dept();
            TPP_DeptFacade dptf = new TPP_DeptFacade();
            dpt = dptf.GetUserDept(user.ID);
            if (dpt.Departemen.Trim() == "IT" || dpt.Departemen.Trim() == "ISO" || dpt.Departemen.Trim() == "HRD & GA")
            {
                arrtpp = tppf.Retrieve();
                Session["arrtpp"] = arrtpp;
                GridTPP.DataSource = arrtpp;
                GridTPP.DataBind();
                ddlDeptName0.Visible = true;
                LDept.Visible = true;
            }
            else
            {
                LoadTPPByDept(dpt.Departemen);
                ddlDeptName0.Visible = false;
                LDept.Visible = false;
            }
            ddlDeptName0.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            ddlTahun.SelectedIndex = 0;
            ddlMasalah.SelectedIndex = 0;
            ddlBulan.SelectedIndex = 0;
        }
        protected void LoadKlausul()
        {
            TPP_Klausul_No tpp = new TPP_Klausul_No();
            TPP_Klausul_NoFacade tppf = new TPP_Klausul_NoFacade();
            ArrayList arrtpp = new ArrayList();
            arrtpp = tppf.Retrieve();
            Session["arrtpp"] = arrtpp;
            GridKlausul.DataSource = arrtpp;
            GridKlausul.DataBind();
        }

        //private void LoadBulan()
        //{
        //    ddlBulan.Items.Clear();
        //    for (int i = 1; i <= 12; i++)
        //    {
        //        ddlBulan.Items.Add(new ListItem(Global.nBulan(i), i.ToString()));
        //    }
        //    ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
        //}

        protected int getDeptID(string deptName)
        {
            int result = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select id from spd_dept where dept like '%" + deptName + "%'";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Int32.Parse(sdr["ID"].ToString());
                }
            }
            return result;
        }

        protected void LoadTPPByKriteria()
        {
            Domain.TPP tpp = new Domain.TPP();
            TPP_Facade tppf = new TPP_Facade();
            ArrayList arrtpp = new ArrayList();
            string Kriteria = string.Empty;

            if (ddlDeptName0.SelectedItem.Text.Trim() != "ALL")
                Kriteria = " and A.dept_ID in (select ID from tpp_dept where departemen='" + ddlDeptName0.SelectedItem.Text + "')";
            if (ddlTahun.SelectedItem.Text.Trim() != "ALL")
                Kriteria = Kriteria + " and left(convert(char,A.tpp_date,112),4)=" + ddlTahun.SelectedItem.Text;
            if (ddlStatus.SelectedItem.Text.Trim() != "ALL")
                Kriteria = Kriteria + "  and isnull(A.Closed,0)=" + ddlStatus.SelectedItem.Value;
            if (ddlMasalah.SelectedItem.Text.Trim() != "ALL")
                Kriteria = Kriteria + "  and isnull(A.Asal_M_ID,0)=" + ddlMasalah.SelectedItem.Value;
            if (ddlBulan.SelectedItem.Text.Trim() != "ALL")
                Kriteria = Kriteria + " and month(A.TPP_Date)=" + ddlBulan.SelectedIndex;
            arrtpp = tppf.RetrieveByKriteria(Kriteria);
            GridTPP.DataSource = arrtpp;
            GridTPP.DataBind();

            #region update sarmut HRD & QA [Kecelakan Kerja & Customer Complaint  & Customer Complaint Non Mutu  ]
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = Kriteria;
            SqlDataReader sdr = zl.Retrieve();

            #region HRD
            #region update sarmut HRD
            string sarmutPrs = "Penurunan Tingkat Kecelakaan Kerja";
            string strDept = string.Empty;
            int deptid = getDeptID("HRD");
            #endregion

            decimal actual = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select COUNT(ID)actual from TPP where Asal_M_ID='5' and MONTH(TPP_Date)='" + ddlBulan.SelectedIndex + "' and YEAR(TPP_Date)='" + ddlTahun.SelectedItem.Text + "'";
            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actual = Decimal.Parse(sdr["actual"].ToString());
                }
            }


            ZetroView zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery =
                "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                " set @bulan=" + ddlBulan.SelectedIndex + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                "update SPD_TransPrs set actual=" + actual.ToString().Replace(",", ".") + " where Approval=0 and tahun=@tahun and bulan=@bulan " +
                " and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "')";
            SqlDataReader sdr1 = zl1.Retrieve();


            //hitung average untuk spd_perusahaan
            actual = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                " set @bulan=" + ddlBulan.SelectedIndex + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                "select isnull(cast(avg(actual) as decimal(18,2)),0) actual from SPD_TransPrs where Approval=0 and tahun=@tahun and bulan=@bulan " +
                " and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "') and actual>0";
            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actual = Decimal.Parse(sdr["actual"].ToString());
                }
            }

            zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;

            zl1.CustomQuery =
                "update SPD_TransPrs set actual= " + actual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddlTahun.SelectedValue +
                 " and Bulan=" + ddlBulan.SelectedIndex +
                 " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "') ";
            sdr1 = zl1.Retrieve();
            #endregion

            #region QA
            #region update sarmut QA
            sarmutPrs = "Customer complaint";//"NCR Customer Complain";
            strDept = "QUALITY CONTROL";
            deptid = getDeptID("QUALITY CONTROL");
            decimal actualm = 0;
            #endregion

            actualm = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select COUNT(ID)actual from TPP where rowstatus>-1 and Asal_M_ID='4' and MONTH(TPP_Date)='" + ddlBulan.SelectedIndex + "' and YEAR(TPP_Date)='" + ddlTahun.SelectedItem.Text + "'";
            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actualm = Decimal.Parse(sdr["actual"].ToString());
                }
            }

            zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery =
                "update SPD_TransPrs set actual= " + actualm.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddlTahun.SelectedValue +
                 " and Bulan=" + ddlBulan.SelectedIndex +
                 " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "') ";
            sdr1 = zl1.Retrieve();
            #endregion

            #region MARKETING
            #region update sarmut MARKETING
            sarmutPrs = "Customer Complaint Non Mutu";
            strDept = "MARKETING";
            deptid = getDeptID("MARKETING");
            decimal actualn = 0;
            #endregion

            actualn = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select COUNT(ID)actual from TPP where Asal_M_ID='7' and MONTH(TPP_Date)='" + ddlBulan.SelectedIndex + "' and YEAR(TPP_Date)='" + ddlTahun.SelectedItem.Text + "'";
            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actualn = Decimal.Parse(sdr["actual"].ToString());
                }
            }

            zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery =
                "update SPD_TransPrs set actual= " + actualn.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddlTahun.SelectedValue +
                 " and Bulan=" + ddlBulan.SelectedIndex +
                 " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "') ";
            sdr1 = zl1.Retrieve();
            #endregion

            #endregion

        }
        protected void LoadTPPByDept(string dep)
        {
            Domain.TPP tpp = new Domain.TPP();
            TPP_Facade tppf = new TPP_Facade();
            ArrayList arrtpp = new ArrayList();
            string Kriteria = string.Empty;

            Kriteria = " and A.dept_ID in (select ID from tpp_dept where departemen='" + dep + "')";
            arrtpp = tppf.RetrieveByKriteria(Kriteria);
            GridTPP.DataSource = arrtpp;
            GridTPP.DataBind();
        }
        protected void LoadPencegahan(string laporan_no)
        {

            if (laporan_no.Length == 1)
                return;
            Session["pencegahan"] = null;
            ArrayList arrPencegahan = new ArrayList();
            TPP_TindakanFacade cegahF = new TPP_TindakanFacade();
            arrPencegahan = cegahF.RetrieveByNo(laporan_no, "Pencegahan");
            Session["pencegahan"] = arrPencegahan;
            GridPencegahan.DataSource = arrPencegahan;
            GridPencegahan.DataBind();

        }

        protected void LoadPerbaikan(string laporan_no)
        {
            if (laporan_no.Length == 1)
                return;
            Session["perbaikan"] = null;
            ArrayList arrPerbaikan = new ArrayList();
            TPP_TindakanFacade PerbaikanF = new TPP_TindakanFacade();
            arrPerbaikan = PerbaikanF.RetrieveByNo(laporan_no, "Perbaikan");
            Session["perbaikan"] = arrPerbaikan;
            GridPerbaikan.DataSource = arrPerbaikan;
            GridPerbaikan.DataBind();
        }
        protected void BtnNew_ServerClick(object sender, EventArgs e)
        {
            ClearForm();
        }
        protected void btnKlausul_ServerClick(object sender, EventArgs e)
        {
            if (Panelklausul.Visible == true)
                Panelklausul.Visible = false;
            else
                Panelklausul.Visible = true;
        }
        protected void BtnLampiran_ServerClick(object sender, EventArgs e)
        {
            if (TxtLaporanNo.Text.Trim() != string.Empty)
            {
                if (btnLampiran.Value == "Lampiran")
                {
                    PanelForm.Visible = false;
                    btnNew.Visible = false;
                    btnUpdate.Visible = false;
                    btnList.Visible = false;
                    btnLampiran.Value = "Form TPP";
                    LJudul.Text = "Lampiran TPP";
                    PanelLampiran.Visible = true;
                    PanelList.Visible = false;
                    LoadLampiran(TxtLaporanNo.Text);
                }
                else
                {
                    PanelForm.Visible = true;
                    btnNew.Visible = true;
                    btnUpdate.Visible = true;
                    btnList.Visible = true;
                    btnLampiran.Value = "Lampiran";
                    LJudul.Text = "Input Data TPP";
                    PanelLampiran.Visible = false;
                    PanelList.Visible = false;
                }
            }
        }
        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            if (txtSearch.Text != string.Empty)
            {
                FormData(txtSearch.Text);
                txtSearch.Text = string.Empty;
            }
        }
        protected void BtnPrint_ServerClick(object sender, EventArgs e)
        {
            string strQuery = "SELECT D.departemen,Laporan_No, TPP_Date,Ketidaksesuaian, Uraian, " +
                            "case when Asal_M_ID=1 then 'P' else ' ' end AE,case when Asal_M_ID=2 then 'P' else ' ' end AI,case when Asal_M_ID=3 then 'P' else ' ' end NC, " +
                            "case when Asal_M_ID=4 then 'P' else ' ' end CC,case when Asal_M_ID=5 then 'P' else ' ' end KK,case when Asal_M_ID1=1 then 'P' else ' ' end major, " +
                            "case when Asal_M_ID1=2 then 'P' else ' ' end minor,case when Asal_M_ID1=3 then 'P' else ' ' end rekom, " +
                            "case when Asal_M_ID=6 then 'P' else ' ' end LL,case when Asal_M_ID=7 then 'P' else ' ' end CN, " +
                            "case when Laporan_No<>'' then (select COUNT(ID) from TPP_Penyebab_Detail where TPP_ID=A.ID and Penyebab_ID=1 and RowStatus>-1) else 0 end PLin, " +
                            "case when Laporan_No<>'' then (select COUNT(ID) from TPP_Penyebab_Detail where TPP_ID=A.ID and Penyebab_ID=2 and RowStatus>-1) else 0 end PMan, " +
                            "case when Laporan_No<>'' then (select COUNT(ID) from TPP_Penyebab_Detail where TPP_ID=A.ID and Penyebab_ID=3 and RowStatus>-1) else 0 end PMat, " +
                            "case when Laporan_No<>'' then (select COUNT(ID) from TPP_Penyebab_Detail where TPP_ID=A.ID and Penyebab_ID=4 and RowStatus>-1) else 0 end PMes, " +
                            "case when Laporan_No<>'' then (select COUNT(ID) from TPP_Penyebab_Detail where TPP_ID=A.ID and Penyebab_ID=5 and RowStatus>-1) else 0 end PMet, " +
                            "case when Laporan_No<>'' then isnull((select URAIAN from TPP_Penyebab_Detail where TPP_ID=A.ID and Penyebab_ID=1 and RowStatus>-1),'') end UPLin, " +
                            "case when Laporan_No<>'' then isnull((select URAIAN from TPP_Penyebab_Detail where TPP_ID=A.ID and Penyebab_ID=2 and RowStatus>-1),'') end UPMan, " +
                            "case when Laporan_No<>'' then isnull((select URAIAN from TPP_Penyebab_Detail where TPP_ID=A.ID and Penyebab_ID=3 and RowStatus>-1),'') end UPMat, " +
                            "case when Laporan_No<>'' then isnull((select URAIAN from TPP_Penyebab_Detail where TPP_ID=A.ID and Penyebab_ID=4 and RowStatus>-1),'') end UPMes, " +
                            "case when Laporan_No<>'' then isnull((select URAIAN from TPP_Penyebab_Detail where TPP_ID=A.ID and Penyebab_ID=5 and RowStatus>-1),'') end UPMet, " +
                            "case when isnull(CLosed,0)=0 then ' ' else 'P' end CLosed, Close_Date,case when isnull(Solved,0)=0 then ' ' else 'P' end Solved, Solve_Date, Due_Date, " +
                            "case when Asal_M_ID=6 then Keterangan else '' end Keterangan,case when Asal_M_ID=1 OR  Asal_M_ID=2 then Keterangan else '' end KeteranganA,A.createdtime, " +
                            "case when RekomID is not null then (select rekomendasi from TPP_RekomHSE where id=A.RekomID)else '' end rekomendasi, " +
                            "case when RekomID is not null then (case when(select ceklis from TPP_RekomHSE where id=a.RekomID)=1 then 'Yes' " +
                            "when (select ceklis from TPP_RekomHSE where id=a.RekomID)=2 then 'No' end ) else ' ' end ceklis  " +
                            "FROM TPP A inner join tpp_dept D on A.dept_id=D.ID where A.laporan_no='" + TxtLaporanNo.Text.Trim() + "'";
            string strQuery1 = "select Tindakan, Pelaku, Jadwal_selesai Jadwal, Aktual_selesai Aktual, case when Verifikasi=1 then 'P' else ' ' end Verifikasi,tglVerifikasi Tanggal from TPP_Tindakan where TPP_ID in (select ID from TPP where Laporan_No='" +
                TxtLaporanNo.Text.Trim() + "') and Jenis='perbaikan' and RowStatus>-1";
            string strQuery2 = "select Tindakan, Pelaku, Jadwal_selesai Jadwal, Aktual_selesai Aktual, case when Verifikasi=1 then 'P' else ' ' end Verifikasi,tglVerifikasi Tanggal  from TPP_Tindakan where TPP_ID in (select ID from TPP where Laporan_No='" +
                TxtLaporanNo.Text.Trim() + "') and Jenis='pencegahan' and RowStatus>-1";
            if (TxtLaporanNo.Text.Trim() != string.Empty)
            {
                Session["Query"] = strQuery;
                Session["Query1"] = strQuery1;
                Session["Query2"] = strQuery2;
                Cetak(this);
            }
        }
        static public void Cetak(Control page)
        {
            string myScript = "var wn = window.showModalDialog('Report.aspx?IdReport=tpp', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        protected void BtnList_ServerClick(object sender, EventArgs e)
        {
            if (btnList.Value == "List TPP")
            {
                PanelForm.Visible = false;
                btnNew.Visible = false;
                btnUpdate.Visible = false;
                btnLampiran.Visible = false;
                btnList.Value = "Form TPP";
                LJudul.Text = "List TPP";
                PanelLampiran.Visible = false;
                PanelList.Visible = true;
                LoadTPP();
            }
            else
            {
                PanelForm.Visible = true;
                btnNew.Visible = true;
                btnUpdate.Visible = true;
                btnLampiran.Visible = true;
                btnList.Value = "List TPP";
                LJudul.Text = "Input Data TPP";
                PanelLampiran.Visible = false;
                PanelList.Visible = false;
            }
        }
        protected void BtnUpdate_ServerClick(object sender, EventArgs e)
        {

            if (Rb1.Checked == false && Rb2.Checked == false && Rb3.Checked == false && Rb4.Checked == false && Rb5.Checked == false && Rb6.Checked == false && Rb7.Checked == false)
            {
                DisplayAJAXMessage(this, "Asal Masalah harus ditentukan");
                return;
            }
            if (Rb1.Checked == true || Rb2.Checked == true)
            {
                if (txtKlausulNo.Text.Trim() == string.Empty)
                {
                    DisplayAJAXMessage(this, "Nomor Klausul Audit harus diisi");
                    return;
                }
            }
            if (ddlBagian.SelectedItem.Text == "-")
            {
                DisplayAJAXMessage(this, "Bagian / Section / Area Harus di isi");
                return;
            }
            if (Rb6.Checked == true)
            {
                if (txtKeterangan.Text.Trim() == string.Empty)
                {
                    DisplayAJAXMessage(this, "Keterangan harus diisi");
                    return;
                }
            }
            if (txtUTSesuai.Text.Trim() == string.Empty)
            {
                DisplayAJAXMessage(this, "Uraian Ketidaksesuaian harus diisi");
                return;
            }
            if (txtITSesuai.Text.Trim() == string.Empty)
            {
                DisplayAJAXMessage(this, "Isi Ketidaksesuaian harus diisi");
                return;
            }
            if (txtTIndakan.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Tindakan Perbaikan Harus di isi");
                return;
            }
            if (txtPelaku.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Pelaku harus di isi");
            }
            if (txtTIndakan0.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Tindakan Pencegahan Harus di isi");
                return;
            }
            if (txtPelaku0.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Pelaku pencegahan harus di isi");
            }


            //if (txtMesin.Text.Trim() == string.Empty && txtManusia.Text.Trim() == string.Empty && txtMaterial.Text.Trim() == string.Empty &&
            //    txtMetode.Text.Trim() == string.Empty && txtLingkungan.Text.Trim() == string.Empty)
            //    {
            //        DisplayAJAXMessage(this, "Analisa Penyebab harus diisi");
            //        return;
            //    }

            string kodetpp = string.Empty;
            TPP_Facade tppf = new TPP_Facade();
            string test = txtTPP_Date.SelectedDate.ToString("yyyy");
            int urutan = tppf.GetLastUrutan(Convert.ToInt32(txtTPP_Date.SelectedDate.ToString("yyyy"))) + 1;
            string masalah = Session["masalah"].ToString();
            string bulanR = Global.ConvertNumericToRomawi(Convert.ToInt32(txtTPP_Date.SelectedDate.ToString("MM")));
            Company company = new Company();
            CompanyFacade companyFacade = new CompanyFacade();
            string OldNo = " ";
            string kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID);
            kodetpp = urutan.ToString().PadLeft(3, '0').Trim() + "/TPP/" + kd + "/" + bulanR + "/" + tppf.GetKodeMasalah(masalah.Trim()).Trim() + "/" + txtTPP_Date.SelectedDate.ToString("yy");
            if (TxtLaporanNo.Text == string.Empty)
            {
                TxtLaporanNo.Text = kodetpp;
                OldNo = kodetpp;
            }
            else
            {
                OldNo = TxtLaporanNo.Text;
                TxtLaporanNo.Text = TxtLaporanNo.Text.Substring(0, TxtLaporanNo.Text.Trim().Length - 5) + tppf.GetKodeMasalah(masalah.Trim()).Trim() +
                    TxtLaporanNo.Text.Substring(TxtLaporanNo.Text.Trim().Length - 3, 3);
            }
            #region insert
            #region insert1
            Users user = ((Users)Session["Users"]);
            Domain.TPP tpp = new Domain.TPP();
            int tppID = 0;
            tpp.Old_Laporan_No = OldNo;
            tpp.Laporan_No = TxtLaporanNo.Text;
            tpp.TPP_Date = txtTPP_Date.SelectedDate;
            tpp.PIC = " ";
            int Asal_M_ID = 0;
            if (Rb1.Checked == true)
                Asal_M_ID = 1;
            if (Rb2.Checked == true)
                Asal_M_ID = 2;
            if (Rb3.Checked == true)
                Asal_M_ID = 3;
            if (Rb4.Checked == true)
                Asal_M_ID = 4;
            if (Rb5.Checked == true)
                Asal_M_ID = 5;
            if (Rb6.Checked == true)
                Asal_M_ID = 6;
            if (Rb7.Checked == true)
                Asal_M_ID = 7;
            if (Rb1.Checked == true || Rb2.Checked == true)
            {
                if (Rb1a.Checked == true)
                    tpp.Asal_M_ID1 = 1;
                if (Rb1b.Checked == true)
                    tpp.Asal_M_ID1 = 2;
                if (Rb1c.Checked == true)
                    tpp.Asal_M_ID1 = 3;
            }
            tpp.Ketidaksesuaian = txtITSesuai.Text;
            tpp.Uraian = txtUTSesuai.Text;
            tpp.Status = 0;
            tpp.Dept_ID = Convert.ToInt32(ddlDeptName.SelectedValue);
            tpp.Asal_M_ID = Asal_M_ID;
            tpp.User_ID = user.ID;
            try
            {
                if (Rb4.Checked == true)
                    tpp.Keterangan = Session["CustomerName"].ToString();
                if (Rb6.Checked == true)
                    tpp.Keterangan = txtKeterangan.Text;
                if (Rb1.Checked == true || Rb2.Checked == true)
                    tpp.Keterangan = txtKlausulNo.Text;
                if (Rb3.Checked == true || Rb5.Checked == true)
                    tpp.Keterangan = " ";
            }
            catch { }
            tpp.CreatedBy = user.UserName;
            tpp.BagianID = Int32.Parse(ddlBagian.SelectedValue);
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacadeF absTrans = new TPP_Facade(tpp);
            int intresult = absTrans.Insert(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return;
            }
            tppID = intresult;
            if (tppID > 0)
            {
                TPP_Tindakan tppt = new TPP_Tindakan();
                TPP_Penyebab_Detail tppPD = new TPP_Penyebab_Detail();

                if (chkLingkungan.Checked == true)
                {
                    tppPD = new TPP_Penyebab_Detail();
                    tppPD.Penyebab_ID = 1;
                    tppPD.TPP_ID = tppID;
                    tppPD.Uraian = txtLingkungan.Text;
                    if (txtLingkungan.Text.Trim() != string.Empty)
                        tppPD.Rowstatus = 0;
                    else
                        tppPD.Rowstatus = -1;
                    absTrans = new TPP_Penyebab_Detail_Facade(tppPD);
                    intresult = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                }
                else
                {
                    tppPD = new TPP_Penyebab_Detail();
                    tppPD.Penyebab_ID = 1;
                    tppPD.TPP_ID = tppID;
                    tppPD.Uraian = txtLingkungan.Text;
                    tppPD.Rowstatus = -1;
                    absTrans = new TPP_Penyebab_Detail_Facade(tppPD);
                    intresult = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                }
                tppPD = new TPP_Penyebab_Detail();
                if (chkManusia.Checked == true)
                {
                    tppPD = new TPP_Penyebab_Detail();
                    tppPD.Penyebab_ID = 2;
                    tppPD.TPP_ID = tppID;
                    tppPD.Uraian = txtManusia.Text;
                    if (txtManusia.Text.Trim() != string.Empty)
                        tppPD.Rowstatus = 0;
                    else
                        tppPD.Rowstatus = -1;
                    absTrans = new TPP_Penyebab_Detail_Facade(tppPD);
                    intresult = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                }
                else
                {
                    tppPD = new TPP_Penyebab_Detail();
                    tppPD.Penyebab_ID = 2;
                    tppPD.TPP_ID = tppID;
                    tppPD.Uraian = txtManusia.Text;
                    tppPD.Rowstatus = -1;
                    absTrans = new TPP_Penyebab_Detail_Facade(tppPD);
                    intresult = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                }

                if (chkMaterial.Checked == true)
                {
                    tppPD = new TPP_Penyebab_Detail();
                    tppPD.Penyebab_ID = 3;
                    tppPD.TPP_ID = tppID;
                    tppPD.Uraian = txtMaterial.Text;
                    if (txtMaterial.Text.Trim() != string.Empty)
                        tppPD.Rowstatus = 0;
                    else
                        tppPD.Rowstatus = -1;
                    absTrans = new TPP_Penyebab_Detail_Facade(tppPD);
                    intresult = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                }
                else
                {
                    tppPD = new TPP_Penyebab_Detail();
                    tppPD.Penyebab_ID = 3;
                    tppPD.TPP_ID = tppID;
                    tppPD.Uraian = txtMaterial.Text;
                    tppPD.Rowstatus = -1;
                    absTrans = new TPP_Penyebab_Detail_Facade(tppPD);
                    intresult = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                }
                tppPD = new TPP_Penyebab_Detail();
                if (chkMesin.Checked == true)
                {
                    tppPD = new TPP_Penyebab_Detail();
                    tppPD.Penyebab_ID = 4;
                    tppPD.TPP_ID = tppID;
                    tppPD.Uraian = txtMesin.Text;
                    if (txtMesin.Text.Trim() != string.Empty)
                        tppPD.Rowstatus = 0;
                    else
                        tppPD.Rowstatus = -1;
                    absTrans = new TPP_Penyebab_Detail_Facade(tppPD);
                    intresult = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                }
                else
                {
                    tppPD = new TPP_Penyebab_Detail();
                    tppPD.Penyebab_ID = 4;
                    tppPD.TPP_ID = tppID;
                    tppPD.Uraian = txtMesin.Text;
                    tppPD.Rowstatus = -1;
                    absTrans = new TPP_Penyebab_Detail_Facade(tppPD);
                    intresult = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                }

                if (chkMetode.Checked == true)
                {
                    tppPD = new TPP_Penyebab_Detail();
                    tppPD.Penyebab_ID = 5;
                    tppPD.TPP_ID = tppID;
                    tppPD.Uraian = txtMetode.Text;
                    if (txtMetode.Text.Trim() != string.Empty)
                        tppPD.Rowstatus = 0;
                    else
                        tppPD.Rowstatus = -1;
                    absTrans = new TPP_Penyebab_Detail_Facade(tppPD);
                    intresult = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                }
                else
                {
                    tppPD = new TPP_Penyebab_Detail();
                    tppPD.Penyebab_ID = 5;
                    tppPD.TPP_ID = tppID;
                    tppPD.Uraian = txtMetode.Text;
                    tppPD.Rowstatus = -1;
                    absTrans = new TPP_Penyebab_Detail_Facade(tppPD);
                    intresult = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                }
                 DateTime  testd = txtDateJS.SelectedDate;
                if (tppID > 0)
                {
                    tppt = new TPP_Tindakan();
                    tppt.TPP_ID = tppID;
                    tppt.Tindakan = txtTIndakan.Text;
                    tppt.Pelaku = txtPelaku.Text;
                    tppt.Jadwal_Selesai = txtDateJS.SelectedDate;
                    tppt.Jenis = "Perbaikan";
                    tppt.CreatedBy = user.UserName;

                    absTrans = new TPP_TindakanFacade(tppt);
                    intresult = absTrans.Insert(transManager);

                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }

                    tppt.TPP_ID = tppID;
                    tppt.Tindakan = txtTIndakan0.Text;
                    tppt.Pelaku = txtPelaku0.Text;
                    tppt.Jadwal_Selesai = txtDateJS0.SelectedDate;
                    tppt.Jenis = "Pencegahan";
                    tppt.CreatedBy = user.UserName;
                    absTrans = new TPP_TindakanFacade(tppt);
                    intresult = absTrans.Insert(transManager);

                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }

                }
            }
            transManager.CommitTransaction();
            transManager.CloseConnection();
            LoadTPP();
            #endregion
            #endregion
        }



        protected void btnAddPerbaikan_ServerClick(object sender, EventArgs e)
        {
            if (TxtLaporanNo.Text.Trim() != string.Empty)
            {
                Users user = ((Users)Session["Users"]);
                Domain.TPP tpp = new Domain.TPP();
                TPP_Facade tppf = new TPP_Facade();
                tpp = tppf.RetrieveByNo(TxtLaporanNo.Text.Trim());
                TPP_Tindakan tppt = new TPP_Tindakan();
                tppt.TPP_ID = tpp.ID;
                tppt.Tindakan = txtTIndakan.Text;
                tppt.Pelaku = txtPelaku.Text;
                tppt.Jadwal_Selesai = txtDateJS.SelectedDate;
                //tppt.Aktual_Selesai = txtDateAS.SelectedDate;
                tppt.Jenis = "Perbaikan";
                tppt.CreatedBy = user.UserName;
                TransactionManager transManager = new TransactionManager(Global.ConnectionString());
                transManager.BeginTransaction();
                AbstractTransactionFacadeF absTrans = new TPP_TindakanFacade(tppt);
                int intresult = absTrans.Insert(transManager);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return;
                }
                transManager.CommitTransaction();
                transManager.CloseConnection();

                PanelPebaikan.Visible = false;
                LoadPerbaikan(TxtLaporanNo.Text.Trim());

            }
        }
        protected void btnPerbaikan_ServerClick(object sender, EventArgs e)
        {
            //if (TxtLaporanNo.Text != string.Empty)
            //{
            PanelPebaikan.Visible = true;
            //}
        }
        protected void btnAddPencegahan_ServerClick(object sender, EventArgs e)
        {
            if (TxtLaporanNo.Text.Trim() != string.Empty)
            {
                Users user = ((Users)Session["Users"]);
                Domain.TPP tpp = new Domain.TPP();
                TPP_Facade tppf = new TPP_Facade();
                tpp = tppf.RetrieveByNo(TxtLaporanNo.Text.Trim());
                TPP_Tindakan tppt = new TPP_Tindakan();
                tppt.TPP_ID = tpp.ID;
                tppt.Tindakan = txtTIndakan0.Text;
                tppt.Pelaku = txtPelaku0.Text;
                tppt.Jadwal_Selesai = txtDateJS0.SelectedDate;
                //tppt.Aktual_Selesai = txtDateAS0.SelectedDate;
                tppt.Jenis = "Pencegahan";
                tppt.CreatedBy = user.UserName;
                TransactionManager transManager = new TransactionManager(Global.ConnectionString());
                transManager.BeginTransaction();
                AbstractTransactionFacadeF absTrans = new TPP_TindakanFacade(tppt);
                int intresult = absTrans.Insert(transManager);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return;
                }
                transManager.CommitTransaction();
                transManager.CloseConnection();

                PanelPencegahan.Visible = false;
                LoadPencegahan(TxtLaporanNo.Text.Trim());
            }
        }
        protected void btnPencegahan_ServerClick(object sender, EventArgs e)
        {
            //if (TxtLaporanNo.Text != string.Empty)
            //{
            PanelPencegahan.Visible = true;
            //}
        }
        private void LoadDept()
        {
            ddlDeptName.Items.Clear();
            ArrayList arrDept = new ArrayList();
            TPP_Dept tppdept = new TPP_Dept();
            TPP_DeptFacade deptFacade = new TPP_DeptFacade();
            arrDept = deptFacade.Retrieve();
            ddlDeptName.Items.Add(new ListItem("-- Pilih Dept --", "0"));
            foreach (TPP_Dept dept in arrDept)
            {
                ddlDeptName.Items.Add(new ListItem(dept.Departemen.ToUpper().Trim(), dept.ID.ToString()));
            }
            Users user = ((Users)Session["Users"]);
            tppdept = deptFacade.GetUserDept(user.ID);
            ddlDeptName.SelectedValue = tppdept.ID.ToString();
            LoadTPPBagian();
        }

        private void LoadTPPBagian()
        {
            ddlBagian.Items.Clear();
            ddlBagian.Items.Add(new ListItem("-", "0"));
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select * from tpp_bagian where deptid  =" + ddlDeptName.SelectedValue;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlBagian.Items.Add(new ListItem(sdr["Bagian"].ToString().ToUpper().Trim(), sdr["ID"].ToString()));
                }
            }
        }

        private void LoadDept0()
        {
            ddlDeptName0.Items.Clear();
            ArrayList arrDept = new ArrayList();
            TPP_Dept tppdept = new TPP_Dept();
            TPP_DeptFacade deptFacade = new TPP_DeptFacade();
            arrDept = deptFacade.Retrieve();
            ddlDeptName0.Items.Add(new ListItem("ALL", "0"));
            foreach (TPP_Dept dept in arrDept)
            {
                ddlDeptName0.Items.Add(new ListItem(dept.Departemen.ToUpper().Trim(), dept.ID.ToString()));
            }
            Users user = ((Users)Session["Users"]);
            tppdept = deptFacade.GetUserDept(user.ID);
            ddlDeptName.SelectedValue = tppdept.ID.ToString();
        }
        protected void LoadMasalah()
        {
            Users user = (Users)Session["Users"];
            ddlMasalah.Items.Clear();
            ArrayList arrDept = new ArrayList();
            TPP_Dept tppdept = new TPP_Dept();
            TPP_DeptFacade deptFacade = new TPP_DeptFacade();
            arrDept = deptFacade.RetrieveMasalah();
            ddlMasalah.Items.Add(new ListItem("ALL", "0"));
            foreach (TPP_Dept dept in arrDept)
            {
                ddlMasalah.Items.Add(new ListItem(dept.Departemen.ToUpper().Trim(), dept.ID.ToString()));
            }
        }
        private void LoadYear()
        {
            ddlTahun.Items.Clear();
            ArrayList arrTPP = new ArrayList();
            Domain.TPP tpp = new Domain.TPP();
            TPP_Facade tppFacade = new TPP_Facade();
            arrTPP = tppFacade.GetYearTPP();
            ddlTahun.Items.Add(new ListItem("ALL", "0"));
            foreach (Domain.TPP tppyear in arrTPP)
            {
                ddlTahun.Items.Add(new ListItem(tppyear.Tahun.ToUpper().Trim(), tppyear.Tahun.ToUpper().Trim()));
            }
        }
        protected void ddlDeptName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Domain.TPP tpp = new Domain.TPP();
            TPP_Facade tppf = new TPP_Facade();
            ArrayList arrtpp = new ArrayList();

            Users user = ((Users)Session["Users"]);
            TPP_Facade tppFacade = new TPP_Facade();
            //LoadTPP();
            TPP_Dept dpt = new TPP_Dept();
            TPP_DeptFacade dptf = new TPP_DeptFacade();
            dpt = dptf.GetUserDept(user.ID);
            int ketemu = 0;
            if (dpt.Departemen.Trim() != "IT" && dpt.Departemen.Trim() != "ISO" && dpt.Departemen.Trim() != "HRD & GA")
            {
                for (int i = 1; i <= ddlDeptName0.Items.Count; i++)
                {
                    ddlDeptName0.SelectedIndex = i;
                    if (ddlDeptName0.SelectedItem.Text.Trim().ToUpper() == dpt.Departemen.Trim().ToUpper())
                    {
                        ketemu = 1;
                        break;
                    }
                }
                if (ketemu == 0)
                    ddlDeptName0.SelectedIndex = 0;
            }
            //ddlD
            LoadTPPByKriteria();
        }
        protected void chkLingkungan_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkLingkungan.Checked == true || chkManusia.Checked == true ||
            //    chkMaterial.Checked == true || chkMesin.Checked == true || chkMetode.Checked == true)
            //    txtITSesuai.Enabled = true;
            //else
            //{
            //    txtITSesuai.Enabled = false;
            //    txtITSesuai.Text = string.Empty;
            //}
            if (chkLingkungan.Checked == true)
            { txtLingkungan.Enabled = true; }
            else
            { txtLingkungan.Enabled = false; }
            if (chkLingkungan.Checked == false && txtLingkungan.Text.Trim() != string.Empty)
            {
                txtLingkungan.Enabled = true;
                chkLingkungan.Checked = true;
            }
            PanelColour();
        }
        protected void chkManusia_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkLingkungan.Checked == true || chkManusia.Checked == true ||
            //    chkMaterial.Checked == true || chkMesin.Checked == true || chkMetode.Checked == true)
            //    txtITSesuai.Enabled = true;
            //else
            //{
            //    txtITSesuai.Enabled = false;
            //    txtITSesuai.Text = string.Empty;
            //}
            if (chkManusia.Checked == true)
            { txtManusia.Enabled = true; }
            else
            { txtManusia.Enabled = false; }
            if (chkManusia.Checked == false && txtManusia.Text.Trim() != string.Empty)
            {
                txtManusia.Enabled = true;
                chkManusia.Checked = true;
            }
            PanelColour();
        }
        protected void chkMaterial_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkLingkungan.Checked == true || chkManusia.Checked == true ||
            //    chkMaterial.Checked == true || chkMesin.Checked == true || chkMetode.Checked == true)
            //    txtITSesuai.Enabled = true;
            //else
            //{
            //    txtITSesuai.Enabled = false;
            //    txtITSesuai.Text = string.Empty;
            //}
            if (chkMaterial.Checked == true)
            { txtMaterial.Enabled = true; }
            else
            { txtMaterial.Enabled = false; }
            if (chkMaterial.Checked == false && txtMaterial.Text.Trim() != string.Empty)
            {
                txtMaterial.Enabled = true;
                chkMaterial.Checked = true;
            }
            PanelColour();
        }
        protected void chkMesin_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkLingkungan.Checked == true || chkManusia.Checked == true ||
            //    chkMaterial.Checked == true || chkMesin.Checked == true || chkMetode.Checked == true)
            //    txtITSesuai.Enabled = true;
            //else
            //{
            //    txtITSesuai.Enabled = false;
            //    txtITSesuai.Text = string.Empty;
            //}
            if (chkMesin.Checked == true)
            { txtMesin.Enabled = true; }
            else
            { txtMesin.Enabled = false; }
            if (chkMesin.Checked == false && txtMesin.Text.Trim() != string.Empty)
            {
                txtMesin.Enabled = true;
                chkMesin.Checked = true;
            }
            PanelColour();
        }
        protected void chkMetode_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkLingkungan.Checked == true || chkManusia.Checked == true ||
            //    chkMaterial.Checked == true || chkMesin.Checked == true || chkMetode.Checked == true)
            //    txtITSesuai.Enabled = true;
            //else
            //{
            //    txtITSesuai.Enabled = false;
            //    txtITSesuai.Text = string.Empty;
            //}
            if (chkMetode.Checked == true)
            { txtMetode.Enabled = true; }
            else
            { txtMetode.Enabled = false; }
            if (chkMetode.Checked == false && txtMetode.Text.Trim() != string.Empty)
            {
                txtMetode.Enabled = true;
                chkMetode.Checked = true;
            }
            PanelColour();
        }
        protected void Rb3_CheckedChanged(object sender, EventArgs e)
        {
            if (Rb1.Checked == false && Rb2.Checked == false)
            {
                Rb1a.Checked = false;
                Rb1b.Checked = false;
                Rb1c.Checked = false;
                PanelColour();
            }
        }
        protected void Rb4_CheckedChanged(object sender, EventArgs e)
        {
            if (Rb1.Checked == false && Rb2.Checked == false)
            {
                Rb1a.Checked = false;
                Rb1b.Checked = false;
                Rb1c.Checked = false;
                PanelColour();
            }
        }
        protected void Rb5_CheckedChanged(object sender, EventArgs e)
        {
            if (Rb1.Checked == false && Rb2.Checked == false)
            {
                Rb1a.Checked = false;
                Rb1b.Checked = false;
                Rb1c.Checked = false;
                PanelColour();
                PanelHSE.Visible = true;
            }
        }
        protected void Rb6_CheckedChanged(object sender, EventArgs e)
        {
            if (Rb1.Checked == false && Rb2.Checked == false)
            {
                Rb1a.Checked = false;
                Rb1b.Checked = false;
                Rb1c.Checked = false;
                PanelColour();
            }
        }

        protected void Rb7_CheckedChanged(object sender, EventArgs e)
        {
            if (Rb1.Checked == false && Rb2.Checked == false)
            {
                Rb1a.Checked = false;
                Rb1b.Checked = false;
                Rb1c.Checked = false;
                PanelColour();
            }
        }

        protected void Rb1a_CheckedChanged(object sender, EventArgs e)
        {
            if (Rb1.Checked == false && Rb2.Checked == false)
            {
                Rb1a.Checked = true;
                Rb1b.Checked = false;
                Rb1c.Checked = false;
                Rb2.Checked = true;
                Rb1.Checked = false;
                Rb3.Checked = false;
                Rb4.Checked = false;
                Rb5.Checked = false;
                Rb6.Checked = false;
            }
            PanelColour();
        }
        protected void Rb1b_CheckedChanged(object sender, EventArgs e)
        {
            if (Rb1.Checked == false && Rb2.Checked == false)
            {
                Rb1a.Checked = false;
                Rb1b.Checked = true;
                Rb1c.Checked = false;
                Rb2.Checked = true;
                Rb1.Checked = false;
                Rb3.Checked = false;
                Rb4.Checked = false;
                Rb5.Checked = false;
                Rb6.Checked = false;
            }
            PanelColour();
        }
        protected void Rb1c_CheckedChanged(object sender, EventArgs e)
        {
            if (Rb1.Checked == false && Rb2.Checked == false)
            {
                Rb1a.Checked = false;
                Rb1b.Checked = false;
                Rb1c.Checked = true;
                Rb2.Checked = true;
                Rb1.Checked = false;
                Rb3.Checked = false;
                Rb4.Checked = false;
                Rb5.Checked = false;
                Rb6.Checked = false;
            }
            PanelColour();
        }
        protected void Rb1_CheckedChanged(object sender, EventArgs e)
        {
            if (Rb1.Checked == true && Rb1a.Checked == false && Rb1b.Checked == false && Rb1c.Checked == false)
            {
                Rb1a.Checked = false;
                Rb1b.Checked = false;
                Rb1c.Checked = true;
            }
            PanelColour();
        }
        protected void Rb2_CheckedChanged(object sender, EventArgs e)
        {
            if (Rb2.Checked == true && Rb1a.Checked == false && Rb1b.Checked == false && Rb1c.Checked == false)
            {
                Rb1a.Checked = false;
                Rb1b.Checked = false;
                Rb1c.Checked = true;
            }
            PanelColour();
        }
        protected void txtMesin_TextChanged(object sender, EventArgs e)
        {
            if (txtMesin.Text.Trim() == string.Empty)
            {
                chkMesin.Checked = false;
                txtMesin.Enabled = false;
            }
            else
                chkMesin.Checked = true;
        }
        protected void txtManusia_TextChanged(object sender, EventArgs e)
        {
            if (txtManusia.Text.Trim() == string.Empty)
            {
                chkManusia.Checked = false;
                txtManusia.Enabled = false;
            }
            else
                chkManusia.Checked = true;
        }
        protected void txtMaterial_TextChanged(object sender, EventArgs e)
        {
            if (txtMaterial.Text.Trim() == string.Empty)
            {
                chkMaterial.Checked = false;
                txtMaterial.Enabled = false;
            }
            else
                chkMaterial.Checked = true;
        }
        protected void txtMetode_TextChanged(object sender, EventArgs e)
        {
            if (txtMetode.Text.Trim() == string.Empty)
            {
                chkMetode.Checked = false;
                txtMetode.Enabled = false;
            }
            else
                chkMetode.Checked = true;
        }
        protected void txtLingkungan_TextChanged(object sender, EventArgs e)
        {
            if (txtLingkungan.Text.Trim() == string.Empty)
            {
                chkLingkungan.Checked = false;
                txtLingkungan.Enabled = false;
            }
            else
                chkLingkungan.Checked = true;
        }


        protected void GridTPP_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridTPP.PageIndex = e.NewPageIndex;
            LoadTPP();
        }



        protected void GridTPPx_Command(object sender, RepeaterCommandEventArgs e)
        {

        }

        protected void GridTPPx_DataBound(object sender, RepeaterItemEventArgs e)
        {

        }


        protected void GridTPP_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
                FormData(GridTPP.Rows[rowindex].Cells[3].Text);
                if (btnList.Value == "List TPP")
                {
                    PanelForm.Visible = false;
                    btnNew.Visible = false;
                    btnUpdate.Visible = false;
                    btnLampiran.Visible = false;
                    btnList.Value = "Form TPP";
                    LJudul.Text = "List TPP";
                    PanelLampiran.Visible = false;
                    PanelList.Visible = true;
                }
                else
                {
                    PanelForm.Visible = true;
                    btnNew.Visible = true;
                    btnUpdate.Visible = true;
                    btnLampiran.Visible = true;
                    btnList.Value = "List TPP";
                    LJudul.Text = "Input Data TPP";
                    PanelLampiran.Visible = false;
                    PanelList.Visible = false;
                }
            }
        }
        protected void FormData(string laporan_no)
        {
            //try
            //{
            ArrayList arrtpp = new ArrayList();
            Domain.TPP tpp = new Domain.TPP();
            TPP_Facade tppf = new TPP_Facade();
            tpp = tppf.RetrieveByNo(laporan_no);
            arrtpp.Add(tpp);
            GridTPP.DataSource = arrtpp;
            GridTPP.DataBind();
            ClearForm();
            TxtLaporanNo.Text = tpp.Laporan_No;
            LblLaporanNo.Text = tpp.Laporan_No;
            txtTPP_Date.SelectedDate = tpp.TPP_Date;
            txtTPP_Date0.SelectedDate = tpp.CreatedTime;
            //txtTPP_Date.SelectedValue = string.Empty;
            //txtPIC.Text = tpp.PIC;

            for (int i = ddlDeptName.Items.Count - 1; i > 0; i--)
            {
                if (ddlDeptName.Items[i].Text.Contains(tpp.Departemen))
                {
                    ddlDeptName.Items[i].Selected = true;
                    break;
                }
            }

            ddlDeptName.SelectedItem.Text = tpp.Departemen;
            string masalah = tpp.Asal_Masalah.Trim();
            if (tpp.Asal_Masalah.Trim() == "Audit External")
                Rb1.Checked = true;
            if (tpp.Asal_Masalah.Trim() == "Audit Internal")
                Rb2.Checked = true;

            Rb1a.Checked = false;
            Rb1b.Checked = false;
            Rb1c.Checked = false;
            if (tpp.Asal_M_ID1 == 1)
            {
                Rb1a.Checked = true;
            }
            if (tpp.Asal_M_ID1 == 2)
            {
                Rb1b.Checked = true;
            }
            if (tpp.Asal_M_ID1 == 3)
            {
                Rb1c.Checked = true;
            }
            if (tpp.Asal_Masalah.Trim() == "NCR Proses")
            {
                Rb3.Checked = true;
            }
            if (tpp.Asal_Masalah.Trim() == "NCR Customer Complain")
            {
                Rb4.Checked = true;
            }
            if (tpp.Asal_Masalah.Trim() == "Kecelakaan Kerja")
            {
                Rb5.Checked = true;
                PanelHSE.Visible = true;
            }
            if (tpp.Asal_Masalah.Trim() == "Lain-lain")
            {
                Rb6.Checked = true;
            }
            if (tpp.Asal_Masalah.Trim() == "NCR Customer Complain Non Mutu")
            {
                Rb7.Checked = true;
            }
            txtUTSesuai.Text = tpp.Uraian;
            txtITSesuai.Enabled = true;
            txtITSesuai.Text = tpp.Ketidaksesuaian;
            LoadLampiran(laporan_no);
            LoadPencegahan(laporan_no);
            LoadPerbaikan(laporan_no);
            txtKeterangan.Text = string.Empty;
            txtKlausulNo.Text = string.Empty;
            //ddlBagian.SelectedValue = tpp.BagianID.ToString();
            string bagian = tppf.getbagian(tpp.BagianID);
            ddlBagian.Items.Clear();
            ddlBagian.Items.Add(new ListItem(bagian, tpp.BagianID.ToString()));
            if (Rb6.Checked == true || Rb4.Checked == true)
            {
                txtKeterangan.Text = tpp.Keterangan;
                txtKlausulNo.Text = string.Empty;
            }
            if (Rb1.Checked == true || Rb2.Checked == true)
            {
                txtKeterangan.Text = string.Empty;
                txtKlausulNo.Text = tpp.Keterangan;
            }
            ArrayList arrPenyebab = new ArrayList();
            TPP_Penyebab_Detail_Facade penyebabdetailF = new TPP_Penyebab_Detail_Facade();
            arrPenyebab = penyebabdetailF.RetrieveByNo(laporan_no);
            foreach (TPP_Penyebab_Detail Pdetail in arrPenyebab)
            {
                if (Pdetail.Penyebab.Trim() == "Lingkungan")
                {
                    chkLingkungan.Checked = true;
                    txtLingkungan.Enabled = true;
                    txtLingkungan.Text = Pdetail.Uraian;
                }
                if (Pdetail.Penyebab.Trim() == "Manusia")
                {
                    chkManusia.Checked = true;
                    txtManusia.Enabled = true;
                    txtManusia.Text = Pdetail.Uraian;
                }
                if (Pdetail.Penyebab.Trim() == "Material")
                {
                    chkMaterial.Checked = true;
                    txtMaterial.Enabled = true;
                    txtMaterial.Text = Pdetail.Uraian;
                }
                if (Pdetail.Penyebab.Trim() == "Mesin")
                {
                    chkMesin.Checked = true;
                    txtMesin.Enabled = true;
                    txtMesin.Text = Pdetail.Uraian;
                }
                if (Pdetail.Penyebab.Trim() == "Metode")
                {
                    chkMetode.Checked = true;
                    txtMetode.Enabled = true;
                    txtMetode.Text = Pdetail.Uraian;
                }
            }
            //
            if (tpp.Apv > 1)
                btnUpdate.Disabled = true;
            else
                btnUpdate.Disabled = false;
            PanelColour();
            if (Session["usertype"].ToString().Trim().ToUpper() == "ISO")
            {
                btnUpdate.Disabled = false;
                PanelStatus.Enabled = true;
                chkClose.Enabled = true;
                txtDateTKasus.Enabled = true;
                chksolved.Enabled = true;
                txtDateSolved.Enabled = true;
                txtDueDate.Enabled = true;
            }
            else
            {
                PanelStatus.Enabled = false;
                chkClose.Enabled = false;
                txtDateTKasus.Enabled = false;
                chksolved.Enabled = false;
                txtDateSolved.Enabled = false;
                txtDueDate.Enabled = false;
            }
            if (tpp.Closed == 1)
            {
                chkClose.Checked = true;
                txtDateTKasus.SelectedDate = tpp.Close_Date;
            }
            else
            {
                chkClose.Checked = false;
                txtDateTKasus.SelectedValue = string.Empty;
            }
            if (tpp.Solved == 1)
            {
                chksolved.Checked = true;
                txtDateSolved.SelectedDate = tpp.Solve_Date;
                txtDueDate.SelectedDate = tpp.Due_Datex;
            }
            else
            {
                chksolved.Checked = false;
                txtDateSolved.SelectedValue = string.Empty;
                txtDueDate.SelectedValue = string.Empty;
            }
            if (tpp.Approval.Trim().ToUpper() == "ADMIN")
            {
                LSpv.Text = "Open";
                LMgr.Text = "Open";
                LMgr0.Text = "Open";
                if (tpp.Asal_M_ID == 1 || tpp.Asal_M_ID == 2)
                {
                    LPMgr.Text = " ";
                    LAuditor.Text = "Open";
                    LAuditor0.Text = "Open";
                }
                else
                {
                    LPMgr.Text = "Open";
                    LAuditor.Text = " ";
                    LAuditor0.Text = " ";
                }
            }
            if (tpp.Approval.Trim().ToUpper() == "HEAD/KASIE")
            {
                LSpv.Text = "Approved";
                LMgr.Text = "Open";
                LMgr0.Text = "Open";
                if (tpp.Asal_M_ID == 1 || tpp.Asal_M_ID == 2)
                {
                    LPMgr.Text = " ";
                    LAuditor.Text = "Open";
                    LAuditor0.Text = "Open";
                }
                else
                {
                    LPMgr.Text = "Open";
                    LAuditor.Text = " ";
                    LAuditor0.Text = " ";
                }
            }
            if (tpp.Approval.Trim().ToUpper() == "MANAGER")
            {
                LSpv.Text = "Approved";
                LMgr0.Text = "Approved";
                LMgr.Text = "Approved";

            }
            if (tpp.Approval.Trim().ToUpper() == "PM / CORP. MGR")
            {
                LSpv.Text = "Approved";
                LMgr.Text = "Approved";
                LMgr0.Text = "Approved";
                LPMgr.Text = "Approved";
                if (tpp.Asal_M_ID == 1 || tpp.Asal_M_ID == 2)
                {
                    LPMgr.Text = " ";
                    LAuditor.Text = "Open";
                    LAuditor0.Text = "Open";
                }
                else
                {
                    LPMgr.Text = "Approved";
                    LAuditor.Text = " ";
                    LAuditor0.Text = " ";
                }
            }
            if (tpp.Verified > 0 && tpp.Notverified == 0)
            {
                if (tpp.Asal_M_ID == 1 || tpp.Asal_M_ID == 2)
                {
                    LAuditor.Text = "Approved";
                    LAuditor0.Text = "Approved";
                    LMR.Text = "Approved";
                }
                else
                {
                    LAuditor.Text = " ";
                    LAuditor0.Text = " ";
                    LMR.Text = "Approved";
                }
            }
            else
            {
                if (tpp.Asal_M_ID == 1 || tpp.Asal_M_ID == 2)
                {
                    LAuditor.Text = "Open";
                    LAuditor0.Text = "Open";
                    LMR.Text = "Open";
                }
                else
                {
                    LAuditor.Text = " ";
                    LAuditor0.Text = " ";
                    LMR.Text = "Open";
                }
            }
            if (tpp.Ceklis == 1)
            {
                chkhseya.Checked = true;
                txthse.Text = tpp.rekomendasi;
            }
            if (tpp.Ceklis == 2)
            {
                chkhseno.Checked = true;
            }
            //}
            //catch { }
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        protected void LoadLampiran(string Laporan_no)
        {
            ArrayList arrLampiran = new ArrayList();
            TPP_LampiranFacade lampiranF = new TPP_LampiranFacade();
            arrLampiran = lampiranF.RetrieveLampiranByNo(Laporan_no);
            GridLampiran.DataSource = arrLampiran;
            GridLampiran.DataBind();
        }
        protected void PanelColour()
        {
            Session["masalah"] = " ";
            //if (Session["CustomerName"]== null )
            //    Rb4.Checked = false;

            if (Rb1.Checked == true)
            {
                PanelRB1.BackColor = System.Drawing.Color.Blue;
                PanelRB1.ForeColor = System.Drawing.Color.White;
                Session["masalah"] = "Audit External";
            }
            else
            {
                PanelRB1.BackColor = System.Drawing.Color.White;
                PanelRB1.ForeColor = System.Drawing.Color.Black;
            }
            if (Rb1a.Checked == true)
            {
                PanelRb1a.BackColor = System.Drawing.Color.Blue;
                PanelRb1a.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                PanelRb1a.BackColor = System.Drawing.Color.White;
                PanelRb1a.ForeColor = System.Drawing.Color.Black;
            }
            if (Rb1b.Checked == true)
            {
                PanelRb1b.BackColor = System.Drawing.Color.Blue;
                PanelRb1b.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                PanelRb1b.BackColor = System.Drawing.Color.White;
                PanelRb1b.ForeColor = System.Drawing.Color.Black;
            }
            if (Rb1c.Checked == true)
            {
                PanelRb1c.BackColor = System.Drawing.Color.Blue;
                PanelRb1c.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                PanelRb1c.BackColor = System.Drawing.Color.White;
                PanelRb1c.ForeColor = System.Drawing.Color.Black;
            }
            if (Rb2.Checked == true)
            {
                PanelRB2.BackColor = System.Drawing.Color.Blue;
                PanelRB2.ForeColor = System.Drawing.Color.White;
                Session["masalah"] = "Audit Internal";
            }
            else
            {
                PanelRB2.BackColor = System.Drawing.Color.White;
                PanelRB2.ForeColor = System.Drawing.Color.Black;
            }
            if (Rb3.Checked == true)
            {
                PanelRb3.BackColor = System.Drawing.Color.Blue;
                PanelRb3.ForeColor = System.Drawing.Color.White;
                Session["masalah"] = "NCR Proses";
            }
            else
            {
                PanelRb3.BackColor = System.Drawing.Color.White;
                PanelRb3.ForeColor = System.Drawing.Color.Black;
            }
            if (Rb4.Checked == true)
            {
                PanelRB4.BackColor = System.Drawing.Color.Blue;
                PanelRB4.ForeColor = System.Drawing.Color.White;
                Session["masalah"] = "NCR Customer Complain";
            }
            else
            {
                PanelRB4.BackColor = System.Drawing.Color.White;
                PanelRB4.ForeColor = System.Drawing.Color.Black;
            }
            if (Rb5.Checked == true)
            {
                PanelRb5.BackColor = System.Drawing.Color.Blue;
                PanelRb5.ForeColor = System.Drawing.Color.White;
                Session["masalah"] = "Kecelakaan Kerja";
            }
            else
            {
                PanelRb5.BackColor = System.Drawing.Color.White;
                PanelRb5.ForeColor = System.Drawing.Color.Black;
            }
            if (Rb6.Checked == true)
            {
                PanelRb6.BackColor = System.Drawing.Color.Blue;
                PanelRb6.ForeColor = System.Drawing.Color.White;
                Session["masalah"] = "Lain-lain";
            }
            else
            {
                PanelRb6.BackColor = System.Drawing.Color.White;
                PanelRb6.ForeColor = System.Drawing.Color.Black;
            }
            if (Rb7.Checked == true)
            {
                PanelRb7.BackColor = System.Drawing.Color.Blue;
                PanelRb7.ForeColor = System.Drawing.Color.White;
                Session["masalah"] = "NCR Customer Complain Non Mutu";
            }
            else
            {
                PanelRb7.BackColor = System.Drawing.Color.White;
                PanelRb7.ForeColor = System.Drawing.Color.Black;
            }

            if (chkLingkungan.Checked == true)
            {
                chkLingkungan.ForeColor = System.Drawing.Color.White;
                Panel14.BackImageUrl = ResolveUrl("~/images/ellipse_L.png");
            }
            else
            {
                chkLingkungan.ForeColor = System.Drawing.Color.Black;
                Panel14.BackImageUrl = string.Empty;
            }
            if (chkManusia.Checked == true)
            {
                chkManusia.ForeColor = System.Drawing.Color.White;
                Panel13.BackImageUrl = ResolveUrl("~/images/ellipse.png");
            }
            else
            {
                chkManusia.ForeColor = System.Drawing.Color.Black;
                Panel13.BackImageUrl = string.Empty;
            }

            if (chkMesin.Checked == true)
            {
                chkMesin.ForeColor = System.Drawing.Color.White;
                Panel15.BackImageUrl = ResolveUrl("~/images/ellipse.png");
            }
            else
            {
                chkMesin.ForeColor = System.Drawing.Color.Black;
                Panel15.BackImageUrl = string.Empty;
            }
            if (chkMaterial.Checked == true)
            {
                chkMaterial.ForeColor = System.Drawing.Color.White;
                Panel17.BackImageUrl = ResolveUrl("~/images/ellipse.png");
            }
            else
            {
                chkMaterial.ForeColor = System.Drawing.Color.Black;
                Panel17.BackImageUrl = string.Empty;
            }
            if (chkMetode.Checked == true)
            {
                chkMetode.ForeColor = System.Drawing.Color.White;
                Panel16.BackImageUrl = ResolveUrl("~/images/ellipse.png");
            }
            else
            {
                chkMetode.ForeColor = System.Drawing.Color.Black;
                Panel16.BackImageUrl = string.Empty;
            }
            if (Rb1.Checked == true || Rb2.Checked == true)
                PanelAudite.Visible = true;
            else
                PanelAudite.Visible = false;
            if (Rb6.Checked == true)
                txtKeterangan.Visible = true;
            else
                txtKeterangan.Visible = false;
        }
        protected void ClearForm()
        {
            LoadDept();
            TxtLaporanNo.Text = string.Empty;
            txtTPP_Date.SelectedDate = DateTime.Now;
            //txtPIC.Text = string.Empty;
            TPP_Dept tppdept = new TPP_Dept();
            TPP_DeptFacade deptFacade = new TPP_DeptFacade();
            Users user = ((Users)Session["Users"]);
            tppdept = deptFacade.GetUserDept(user.ID);
            ddlDeptName.SelectedValue = tppdept.ID.ToString();
            Rb1.Checked = false;
            Rb1a.Checked = false;
            Rb1b.Checked = false;
            Rb1c.Checked = false;
            Rb2.Checked = false;
            Rb3.Checked = false;
            Rb4.Checked = false;
            Rb5.Checked = false;
            Rb6.Checked = false;
            Rb7.Checked = false;
            txtKeterangan.Text = string.Empty;
            txtUTSesuai.Text = string.Empty;
            txtITSesuai.Text = string.Empty;
            chkLingkungan.Checked = false;
            chkManusia.Checked = false;
            chkMaterial.Checked = false;
            chkMesin.Checked = false;
            chkMetode.Checked = false;
            txtLingkungan.Text = string.Empty;
            txtManusia.Text = string.Empty;
            txtMaterial.Text = string.Empty;
            txtMesin.Text = string.Empty;
            txtMetode.Text = string.Empty;
            txtLingkungan.Enabled = false;
            txtManusia.Enabled = false;
            txtMaterial.Enabled = false;
            txtMesin.Enabled = false;
            txtMetode.Enabled = false;
            LoadPencegahan("0");
            LoadPerbaikan("0");
            //Session["pencegahan"] = null;
            //Session["perbaikan"] = null;
            PanelColour();
            btnUpdate.Disabled = false;
            txtKlausulNo.Text = string.Empty;
            if (Session["usertype"].ToString().Trim().ToUpper() == "ISO")
            {
                PanelStatus.Enabled = true;
                chkClose.Enabled = true;
                txtDateTKasus.Enabled = true;
                chksolved.Enabled = true;
                txtDateSolved.Enabled = true;
                txtDueDate.Enabled = true;
            }
            else
            {
                PanelStatus.Enabled = false;
                chkClose.Enabled = false;
                txtDateTKasus.Enabled = false;
                chksolved.Enabled = false;
                txtDateSolved.Enabled = false;
                txtDueDate.Enabled = false;
            }
            LSpv.Text = " ";
            LMgr.Text = " ";
            LPMgr.Text = " ";
            LMR.Text = " ";
            LAuditor.Text = " ";
            PanelHSE.Visible = false;
            txthse.Text = " ";
            chkhseno.Checked = false;
            chkhseya.Checked = false;
        }
        protected void GridLampiran_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName == "lihat")
            {

                PriviewPdf(this, GridLampiran.Rows[rowindex].Cells[0].Text);
            }
            if (e.CommandName == "hapus")
            {
                TPP_LampiranFacade lampiranF = new TPP_LampiranFacade();
                string err = lampiranF.hapus(GridLampiran.Rows[rowindex].Cells[0].Text);
                LoadLampiran(TxtLaporanNo.Text);
            }
        }
        static public void PriviewPdf(Control page, string ID)
        {
            string myScript = "var wn = window.showModalDialog('../../ModalDialog/PdfPreviewTPP.aspx?ba=" + ID + "', 'Preview', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        protected void btnUpload_ServerClick(object sender, EventArgs e)
        {
            Domain.TPP tpp = new Domain.TPP();
            TPP_Facade tppf = new TPP_Facade();
            tpp = tppf.RetrieveByNo(TxtLaporanNo.Text);
            UploadPdf(this, tpp.ID.ToString());
        }
        protected void btnUpload0_ServerClick(object sender, EventArgs e)
        {
            LoadLampiran(TxtLaporanNo.Text);
        }
        static public void UploadPdf(Control page, string ID)
        {
            string myScript = "var wn = window.showModalDialog('../../ModalDialog/UploadFileTPP.aspx?ba=" + ID + "', 'Preview', 'resizable:yes;dialogHeight: 200px; dialogWidth: 900px;scrollbars=no');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        protected void GridPerbaikan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ArrayList arrperbaikan = new ArrayList();
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
                if (Session["perbaikan"] != null)
                {
                    arrperbaikan = (ArrayList)Session["perbaikan"];
                    TPP_Tindakan tindakan = (TPP_Tindakan)arrperbaikan[e.Row.RowIndex];
                    Label lDateJS = (Label)e.Row.FindControl("lDateJS");
                    BDPLite txtDateJS = (BDPLite)e.Row.FindControl("txtDateJS");
                    Label lDateAS = (Label)e.Row.FindControl("lDateAS");
                    BDPLite txtDateAS = (BDPLite)e.Row.FindControl("txtDateAS");
                    Label lDateVF = (Label)e.Row.FindControl("lDateVF");
                    BDPLite txtDateVF = (BDPLite)e.Row.FindControl("txtDateVF");
                    LinkButton btn = (LinkButton)e.Row.Cells[6].Controls[0];
                    LinkButton btn1 = (LinkButton)e.Row.Cells[11].Controls[0];
                    LinkButton btn2 = (LinkButton)e.Row.Cells[9].Controls[0];
                    ((System.Web.UI.WebControls.LinkButton)(e.Row.Cells[9].Controls[0])).ToolTip = "Klik untuk naik target";
                    if (e.Row.Cells[0].Text.Trim() == "0")
                    {
                        btn.Enabled = true;
                        btn1.Enabled = true;
                        btn2.Enabled = true;
                    }
                    else
                    {
                        btn.Enabled = false;
                        btn1.Enabled = false;
                        btn2.Enabled = false;
                    }
                    if (tindakan.Jadwal_Selesai.ToString("yyyyMMdd") != "00010101" && tindakan.Jadwal_Selesai.ToString("yyyyMMdd") != "17530101")
                    {
                        lDateJS.Text = tindakan.Jadwal_Selesai.ToString("dd-MMM-yyyy");
                        txtDateJS.SelectedValue = tindakan.Jadwal_Selesai.ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        lDateJS.Text = "";
                        txtDateJS.SelectedValue = DateTime.Now.ToString("dd-MMM-yyyy");
                    }

                    if (tindakan.Aktual_Selesai.ToString("yyyyMMdd") != "00010101" && tindakan.Aktual_Selesai.ToString("yyyyMMdd") != "17530101")
                    {
                        lDateAS.Text = tindakan.Aktual_Selesai.ToString("dd-MMM-yyyy");
                        txtDateAS.SelectedValue = tindakan.Aktual_Selesai.ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        lDateAS.Text = "";
                        txtDateAS.SelectedValue = DateTime.Now.ToString("dd-MMM-yyyy");
                    }
                    if (tindakan.Tglverifikasi.ToString("yyyyMMdd") != "00010101" && tindakan.Tglverifikasi.ToString("yyyyMMdd") != "17530101")
                    {
                        lDateVF.Text = tindakan.Tglverifikasi.ToString("dd-MMM-yyyy");
                        txtDateVF.SelectedValue = tindakan.Tglverifikasi.ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        lDateVF.Text = "";
                        txtDateVF.SelectedValue = DateTime.Now.ToString("dd-MMM-yyyy");
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
        protected void GridPencegahan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            ArrayList arrpencegahan = new ArrayList();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkver = (CheckBox)e.Row.FindControl("chkVerifikasi0");
                if (Convert.ToInt32(e.Row.Cells[0].Text.Trim()) == 0)
                    chkver.Checked = false;
                else
                    chkver.Checked = true;
                if (Session["usertype"].ToString().Trim().ToUpper() == "ISO")
                    chkver.Enabled = true;
                else
                    chkver.Enabled = false;
                if (Session["pencegahan"] != null)
                {
                    arrpencegahan = (ArrayList)Session["pencegahan"];
                    TPP_Tindakan tindakan = (TPP_Tindakan)arrpencegahan[e.Row.RowIndex];
                    Label lDateJS = (Label)e.Row.FindControl("lDateJS");
                    BDPLite txtDateJS = (BDPLite)e.Row.FindControl("txtDateJS");
                    Label lDateAS = (Label)e.Row.FindControl("lDateAS");
                    BDPLite txtDateAS = (BDPLite)e.Row.FindControl("txtDateAS");
                    Label lDateVF = (Label)e.Row.FindControl("lDateVF");
                    BDPLite txtDateVF = (BDPLite)e.Row.FindControl("txtDateVF");
                    LinkButton btn = (LinkButton)e.Row.Cells[6].Controls[0];
                    LinkButton btn1 = (LinkButton)e.Row.Cells[11].Controls[0];
                    LinkButton btn2 = (LinkButton)e.Row.Cells[9].Controls[0];
                    ((System.Web.UI.WebControls.LinkButton)(e.Row.Cells[9].Controls[0])).ToolTip = "Klik untuk naik target";
                    if (e.Row.Cells[0].Text.Trim() == "0")
                    {
                        btn.Enabled = true;
                        btn1.Enabled = true;
                        btn2.Enabled = true;
                    }
                    else
                    {
                        btn.Enabled = false;
                        btn1.Enabled = false;
                        btn2.Enabled = false;
                    }
                    if (tindakan.Jadwal_Selesai.ToString("yyyyMMdd") != "00010101" && tindakan.Jadwal_Selesai.ToString("yyyyMMdd") != "17530101")
                    {
                        lDateJS.Text = tindakan.Jadwal_Selesai.ToString("dd-MMM-yyyy");
                        txtDateJS.SelectedValue = tindakan.Jadwal_Selesai.ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        lDateJS.Text = "";
                        txtDateJS.SelectedValue = DateTime.Now.ToString("dd-MMM-yyyy");
                    }

                    if (tindakan.Aktual_Selesai.ToString("yyyyMMdd") != "00010101" && tindakan.Aktual_Selesai.ToString("yyyyMMdd") != "17530101")
                    {
                        lDateAS.Text = tindakan.Aktual_Selesai.ToString("dd-MMM-yyyy");
                        txtDateAS.SelectedValue = tindakan.Aktual_Selesai.ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        lDateAS.Text = "";
                        txtDateAS.SelectedValue = DateTime.Now.ToString("dd-MMM-yyyy");
                    }
                    if (tindakan.Tglverifikasi.ToString("yyyyMMdd") != "00010101" && tindakan.Tglverifikasi.ToString("yyyyMMdd") != "17530101")
                    {
                        lDateVF.Text = tindakan.Tglverifikasi.ToString("dd-MMM-yyyy");
                        txtDateVF.SelectedValue = tindakan.Tglverifikasi.ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        lDateVF.Text = "";
                        txtDateVF.SelectedValue = DateTime.Now.ToString("dd-MMM-yyyy");
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
        protected void GridPerbaikan_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
            Users user = ((Users)Session["Users"]);
            CheckBox chkver = (CheckBox)GridPerbaikan.Rows[rowindex].FindControl("chkVerifikasi1");
            BDPLite txtDateAS = (BDPLite)GridPerbaikan.Rows[rowindex].FindControl("txtDateAS");
            Label lDateJS = (Label)GridPerbaikan.Rows[rowindex].FindControl("lDateJS");
            BDPLite txtDateJS = (BDPLite)GridPerbaikan.Rows[rowindex].FindControl("txtDateJS");
            Label lDateAS = (Label)GridPerbaikan.Rows[rowindex].FindControl("lDateAS");
            LinkButton btn = (LinkButton)GridPerbaikan.Rows[rowindex].Cells[6].Controls[0];
            LinkButton btntarget = (LinkButton)GridPerbaikan.Rows[rowindex].Cells[9].Controls[0];
            if (e.CommandName == "rubah")
            {

                if (btn.Text == "Simpan")
                {
                    btn.Text = "Edit";
                    TPP_TindakanFacade tpptf = new TPP_TindakanFacade();
                    if (txtDateJS.SelectedDate < txtDateAS.SelectedDate)
                    {
                        txtDateAS.Visible = false;
                        lDateAS.Visible = true;
                        txtDateJS.Visible = false;
                        lDateJS.Visible = true;
                        DisplayAJAXMessage(this, "Tanggal aktual tidak boleh lebih besar dari Tanggal Jadwal ");
                        return;
                    }
                    string strerror = tpptf.UpdateAktualSelesai(GridPerbaikan.Rows[rowindex].Cells[1].Text, txtDateJS.SelectedDate.ToString("yyyyMMdd"), txtDateAS.SelectedDate.ToString("yyyyMMdd"));
                    LoadPerbaikan(TxtLaporanNo.Text.Trim());
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
                TPP_TindakanFacade tpptf = new TPP_TindakanFacade();
                string strerror = tpptf.DeleteTindakan(GridPerbaikan.Rows[rowindex].Cells[1].Text);
                LoadPerbaikan(TxtLaporanNo.Text.Trim());
            }
            if (e.CommandName == "target")
            {
                TPP_Tindakan tindakan = new TPP_Tindakan();
                TPP_TindakanFacade tindakanF = new TPP_TindakanFacade();
                Domain.TPP tpp = new Domain.TPP();
                TPP_Facade tppf = new TPP_Facade();
                //btntarget.Attributes.Add("onclick", "openWindow()");
                tpp = tppf.RetrieveByNo(TxtLaporanNo.Text.Trim());
                tindakan.TPP_ID = tpp.ID;
                tindakan.Tindakan = GridPerbaikan.Rows[rowindex].Cells[2].Text;
                tindakan.Pelaku = GridPerbaikan.Rows[rowindex].Cells[3].Text;
                //tindakan.Jadwal_Selesai = DateTime.Parse(GridPerbaikan.Rows[rowindex].Cells[1].Text);
                tindakan.Jenis = "Perbaikan";
                tindakan.Target = "T" + (tindakanF.getTarget(TxtLaporanNo.Text.Trim(), tindakan.Jenis, tindakan.Tindakan) + 1).ToString();
                tindakan.CreatedBy = user.UserName;
                string strerror = tindakanF.UpdateAktualSelesai(GridPerbaikan.Rows[rowindex].Cells[1].Text,
                    DateTime.Now.ToString("yyyyMMdd"), DateTime.MinValue.ToString("yyyyMMdd"));
                strerror = tindakanF.UpdateTindakan(GridPerbaikan.Rows[rowindex].Cells[1].Text, DateTime.Now.ToString("yyyyMMdd"), "1");
                TransactionManager transManager = new TransactionManager(Global.ConnectionString());
                transManager.BeginTransaction();
                AbstractTransactionFacadeF absTrans = new TPP_TindakanFacade(tindakan);
                int intresult = absTrans.Insert(transManager);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return;
                }
                transManager.CommitTransaction();
                transManager.CloseConnection();
                LoadPerbaikan(TxtLaporanNo.Text.Trim());
            }
        }
        protected void GridPencegahan_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
            Users user = ((Users)Session["Users"]);
            CheckBox chkver = (CheckBox)GridPencegahan.Rows[rowindex].FindControl("chkVerifikasi0");
            BDPLite txtDateAS = (BDPLite)GridPencegahan.Rows[rowindex].FindControl("txtDateAS");
            Label lDateJS = (Label)GridPencegahan.Rows[rowindex].FindControl("lDateJS");
            BDPLite txtDateJS = (BDPLite)GridPencegahan.Rows[rowindex].FindControl("txtDateJS");
            Label lDateAS = (Label)GridPencegahan.Rows[rowindex].FindControl("lDateAS");
            LinkButton btn = (LinkButton)GridPencegahan.Rows[rowindex].Cells[6].Controls[0];
            LinkButton btntarget = (LinkButton)GridPencegahan.Rows[rowindex].Cells[9].Controls[0];
            if (e.CommandName == "rubah")
            {

                if (btn.Text == "Simpan")
                {
                    btn.Text = "Edit";
                    TPP_TindakanFacade tpptf = new TPP_TindakanFacade();
                    if (txtDateJS.SelectedDate < txtDateAS.SelectedDate)
                    {
                        txtDateAS.Visible = false;
                        lDateAS.Visible = true;
                        txtDateJS.Visible = false;
                        lDateJS.Visible = true;
                        DisplayAJAXMessage(this, "Tanggal aktual tidak boleh lebih besar dari Tanggal Jadwal ");
                        return;
                    }
                    string strerror = tpptf.UpdateAktualSelesai(GridPencegahan.Rows[rowindex].Cells[1].Text, txtDateJS.SelectedDate.ToString("yyyyMMdd"), txtDateAS.SelectedDate.ToString("yyyyMMdd"));
                    LoadPencegahan(TxtLaporanNo.Text.Trim());
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
                TPP_TindakanFacade tpptf = new TPP_TindakanFacade();
                string strerror = tpptf.DeleteTindakan(GridPencegahan.Rows[rowindex].Cells[1].Text);
                LoadPencegahan(TxtLaporanNo.Text.Trim());
            }
            if (e.CommandName == "target")
            {
                TPP_Tindakan tindakan = new TPP_Tindakan();
                TPP_TindakanFacade tindakanF = new TPP_TindakanFacade();
                Domain.TPP tpp = new Domain.TPP();
                TPP_Facade tppf = new TPP_Facade();
                //btntarget.Attributes.Add("onclick", "openWindow()");
                tpp = tppf.RetrieveByNo(TxtLaporanNo.Text.Trim());
                tindakan.TPP_ID = tpp.ID;
                tindakan.Tindakan = GridPencegahan.Rows[rowindex].Cells[2].Text;
                tindakan.Pelaku = GridPencegahan.Rows[rowindex].Cells[3].Text;
                //tindakan.Jadwal_Selesai = DateTime.Parse(GridPencegahan.Rows[rowindex].Cells[1].Text);
                tindakan.Jenis = "Pencegahan";
                tindakan.Target = "T" + (tindakanF.getTarget(TxtLaporanNo.Text.Trim(), tindakan.Jenis, tindakan.Tindakan) + 1).ToString();
                tindakan.CreatedBy = user.UserName;
                string strerror = tindakanF.UpdateAktualSelesai(GridPencegahan.Rows[rowindex].Cells[1].Text,
                    DateTime.Now.ToString("yyyyMMdd"), DateTime.MinValue.ToString("yyyyMMdd"));
                strerror = tindakanF.UpdateTindakan(GridPencegahan.Rows[rowindex].Cells[1].Text, DateTime.Now.ToString("yyyyMMdd"), "1");
                TransactionManager transManager = new TransactionManager(Global.ConnectionString());
                transManager.BeginTransaction();
                AbstractTransactionFacadeF absTrans = new TPP_TindakanFacade(tindakan);
                int intresult = absTrans.Insert(transManager);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return;
                }
                transManager.CommitTransaction();
                transManager.CloseConnection();
                LoadPencegahan(TxtLaporanNo.Text.Trim());
            }
        }
        protected void chkVerifikasi1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow gr = (GridViewRow)chk.NamingContainer;
            Label lDateVF = (Label)GridPerbaikan.Rows[gr.RowIndex].FindControl("lDateVF");
            BDPLite txtDateVF = (BDPLite)GridPerbaikan.Rows[gr.RowIndex].FindControl("txtDateVF");
            Label lDateAS = (Label)GridPerbaikan.Rows[gr.RowIndex].FindControl("lDateAS");
            //txtITSesuai.Text = GridPerbaikan.Rows[gr.RowIndex].Cells[1].Text ;
            string verf = string.Empty;
            if (chk.Checked == true)
                verf = "1";
            else
                verf = "0";
            TPP_TindakanFacade tpptf = new TPP_TindakanFacade();
            //if (lDateAS.Text.Trim() == string.Empty)
            //{
            //    chk.Checked = false;
            //    return;
            //}
            string strerror = tpptf.UpdateTindakan(GridPerbaikan.Rows[gr.RowIndex].Cells[1].Text, txtDateVF.SelectedDate.ToString("yyyyMMdd"), verf);
            LoadPerbaikan(TxtLaporanNo.Text.Trim());
        }
        protected void chkVerifikasi0_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow gr = (GridViewRow)chk.NamingContainer;
            Label lDateVF = (Label)GridPencegahan.Rows[gr.RowIndex].FindControl("lDateVF");
            BDPLite txtDateVF = (BDPLite)GridPencegahan.Rows[gr.RowIndex].FindControl("txtDateVF");
            Label lDateAS = (Label)GridPencegahan.Rows[gr.RowIndex].FindControl("lDateAS");
            string verf = string.Empty;
            if (chk.Checked == true)
                verf = "1";
            else
                verf = "0";
            TPP_TindakanFacade tpptf = new TPP_TindakanFacade();
            //if (lDateAS.Text.Trim() == string.Empty)
            //{
            //    chk.Checked = false;
            //    return;
            //}
            string strerror = tpptf.UpdateTindakan(GridPencegahan.Rows[gr.RowIndex].Cells[1].Text, txtDateVF.SelectedDate.ToString("yyyyMMdd"), verf);
            LoadPencegahan(TxtLaporanNo.Text.Trim()); LoadPerbaikan(TxtLaporanNo.Text.Trim());
            //txtITSesuai.Text = GridPencegahan.Rows[gr.RowIndex].Cells[1].Text;
        }
        protected void chkClose_CheckedChanged(object sender, EventArgs e)
        {
            btnClose.Visible = true;
            if (chkClose.Checked == true)
                btnClose.Value = "Close";
            else
                btnClose.Value = "Open";
            //txtDateTKasus.SelectedDate = DateTime.Now;
        }
        protected void chksolved_CheckedChanged(object sender, EventArgs e)
        {
            btnSolve.Visible = true;
            txtDateSolved.SelectedDate = DateTime.Now;
            txtDueDate.SelectedDate = DateTime.Now;
        }
        protected void btnClose_ServerClick(object sender, EventArgs e)
        {
            Users user = ((Users)Session["Users"]);
            TPP_Facade tppf = new TPP_Facade();
            int value = 0;
            if (chkClose.Checked == true)
                value = 1;
            else
                value = 0;
            string strerror = tppf.UpdateCloseTPP(TxtLaporanNo.Text, txtDateTKasus.SelectedDate.ToString("yyyyMMdd"), user.UserName.Trim(), value);
            btnClose.Visible = false;
        }
        protected void btnSolve_ServerClick(object sender, EventArgs e)
        {
            Users user = ((Users)Session["Users"]);
            TPP_Facade tppf = new TPP_Facade();
            int value = 0;
            if (chksolved.Checked == true)
                value = 1;
            else
                value = 0;
            string strerror = tppf.UpdateSolveTPP(TxtLaporanNo.Text, txtDateSolved.SelectedDate.ToString("yyyyMMdd"),
                txtDueDate.SelectedDate.ToString("yyyyMMdd"), user.UserName.Trim(), value);
            btnSolve.Visible = false;
        }

        int indexOfColumn = 1;
        protected void GridTPP_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count > indexOfColumn)
            {
                for (int i = 0; i < GridTPP.Columns.Count; i++)
                {
                    if (GridTPP.Columns[i].HeaderText == "KetStatus")
                    {
                        //GridView1.Columns[i].Visible = false;
                        e.Row.Cells[i].Visible = false; //IdSupplier
                    }
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label chkver = (Label)e.Row.FindControl("LStatus");
                if (Convert.ToInt32(e.Row.Cells[0].Text.Trim()) == 0)
                {
                    if (Convert.ToInt32(e.Row.Cells[0].Text.Trim()) == 0 && (e.Row.Cells[8].Text.Trim()) == "Not Approved PM")
                    {
                        chkver.Text = "Not Approved PM";
                    }
                    else
                    {
                        chkver.Text = "Open";
                    }

                }
                else
                {
                    chkver.Text = "Close";
                }
                string test = DateTime.Parse(e.Row.Cells[10].Text).ToString("yyyyMMdd");
                if (DateTime.Parse(e.Row.Cells[10].Text).ToString("yyyyMMdd") == "19000101" || DateTime.Parse(e.Row.Cells[10].Text).ToString("yyyyMMdd") == "20000101")
                    e.Row.Cells[10].Text = "";
            }

        }

        protected void GridKlausul_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
                txtKlausulNo.Text = GridKlausul.Rows[rowindex].Cells[0].Text.Trim();
                Panelklausul.Visible = false;

            }
        }
        protected void GridLampiran_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                LinkButton btn = (LinkButton)e.Row.Cells[4].Controls[0];
                if (chkClose.Checked == true)
                    btn.Enabled = false;
                else
                    btn.Enabled = true;
            }
            catch { }
        }
        protected void chkhseya_CheckedChanged(object sender, EventArgs e)
        {
            if (chkhseya.Checked == true)
            {
                txthse.Visible = true;
            }
            else
            {
                txthse.Visible = false;
            }
        }
    }
}

public class TPP_Bagian : GRCBaseDomain
{
    public string Bagian { get; set; }
}