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

namespace GRCweb1.Modul.ISO
{
    public partial class AnalisaResiko : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Users user = ((Users)Session["Users"]);
                txtTglAnalisaRisk.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtDueDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                LoadDept();
                //IDDeptx();
                LoadKlasifikasiResiko();
                LoadKemungkinan();
                LoadDampak();
                //txtTahun.Text = DateTime.Now.Year.ToString();
                LoadTahun();
            }

        }

        #region penambahan tahun agus
        private void LoadTahun()
        {
            ArrayList arrD = this.ListBATahun();
            ddlTahun.Items.Clear();
            foreach (BeritaAcara ba in arrD)
            {
                //ddlTahun.Items.Add(new ListItem(ba.Tahun.ToString(), ba.Tahun.ToString()));
                ddlTahun.Items.Add(new System.Web.UI.WebControls.ListItem(ba.Tahun.ToString(), ba.Tahun.ToString()));
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
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

        protected void ddlTahun_SelectedIndexChanged(object sender, EventArgs e)
        { }
        #endregion 

        private void LoadDept()
        {
            Users users = (Users)Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select * from SPD_Dept where RowStatus>-1 and dptID in (" + users.DeptID + ") order by dept";
            SqlDataReader sdr = zl.Retrieve();
            ddlDeptName.Items.Clear();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlDeptName.Items.Add(new ListItem(sdr["Dept"].ToString(), sdr["ID"].ToString()));
                }
            }
            IDDept.Text = users.DeptID.ToString();
        }

        private void LoadKlasifikasiResiko()
        {
            Users users = (Users)Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = " select A.ID,A.DeptID,A.AnalisaResiko from ISO_AnalisaRMaster A " +
                             " inner join ISO_DeptRisk B on B.DptID=A.DeptID where B.dptID in (" + users.DeptID + ") and A.RowStatus>-1 ";
            SqlDataReader sdr = zl.Retrieve();
            ddlklasifikasiRisk.Items.Add(new ListItem("-- Pilih -- ", "0"));
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlklasifikasiRisk.Items.Add(new ListItem(sdr["AnalisaResiko"].ToString(), sdr["ID"].ToString()));
                }
            }
        }

        private void LoadKemungkinan()
        {
            Users users = (Users)Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select * from ISO_AnalisaKemungkinan where RowStatus>-1 ";
            SqlDataReader sdr = zl.Retrieve();
            ddlKemungkinan.Items.Add(new ListItem("-- Pilih -- ", "0"));
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlKemungkinan.Items.Add(new ListItem(sdr["Point"].ToString() + HttpUtility.HtmlDecode(".&nbsp;") + sdr["Kemungkinan"].ToString(), sdr["ID"].ToString()));
                }
            }
        }

        private void LoadDampak()
        {
            Users users = (Users)Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select * from ISO_AnalisaDampak where RowStatus>-1 ";
            SqlDataReader sdr = zl.Retrieve();
            ddlDampak.Items.Add(new ListItem("-- Pilih -- ", "0"));
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlDampak.Items.Add(new ListItem(sdr["Point"].ToString() + HttpUtility.HtmlDecode(".&nbsp;") + sdr["Dampak"].ToString(), sdr["ID"].ToString()));
                }
            }
        }

        protected void ddlKemungkinan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlKemungkinan.SelectedIndex == 1)
            {
                lvlKemungkinan.Text = "1";
            }
            if (ddlKemungkinan.SelectedIndex == 2)
            {
                lvlKemungkinan.Text = "2";
            }
            if (ddlKemungkinan.SelectedIndex == 3)
            {
                lvlKemungkinan.Text = "3";
            }
            if (ddlKemungkinan.SelectedIndex == 4)
            {
                lvlKemungkinan.Text = "4";
            }
            if (ddlKemungkinan.SelectedIndex == 5)
            {
                lvlKemungkinan.Text = "5";
            }
            //lblResiko.Text = (int.Parse(lvlKemungkinan.Text) * (int.Parse(lvlDampak.Text))).ToString("N0");
        }

        protected void ddlDampak_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDampak.SelectedIndex == 1)
            {
                lvlDampak.Text = "1";
            }
            if (ddlDampak.SelectedIndex == 2)
            {
                lvlDampak.Text = "2";
            }
            if (ddlDampak.SelectedIndex == 3)
            {
                lvlDampak.Text = "3";
            }
            if (ddlDampak.SelectedIndex == 4)
            {
                lvlDampak.Text = "4";
            }
            if (ddlDampak.SelectedIndex == 5)
            {
                lvlDampak.Text = "5";
            }

            lblResiko.Text = (int.Parse(lvlKemungkinan.Text) * (int.Parse(lvlDampak.Text))).ToString("N0");
        }

        protected void lstPMX_Command(object sender, RepeaterCommandEventArgs e)
        {

        }

        protected void lstPMX_Databound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void btnUpdate_serverClick(object sender, EventArgs e)
        {
            if (RbIK.Checked == true)
            {
                #region Sesuai IK
                string strValidate = ValidateText();
                if (strValidate != string.Empty)
                {
                    DisplayAJAXMessage(this, strValidate);
                    return;
                }

                int tgl = 0;
                int bln = 0;
                int thn = int.Parse(ddlTahun.SelectedValue);

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
                    DateTime final = new DateTime(int.Parse(ddlTahun.SelectedValue), ddlBulan.SelectedIndex, DateTime.DaysInMonth(int.Parse(ddlTahun.SelectedValue), ddlBulan.SelectedIndex));

                    tgl = final.Day;
                }
                else
                {
                    tgl = DateTime.Parse(txtDueDate.Text).Day;
                }

                DateTime TglTarget = new DateTime(int.Parse(ddlTahun.SelectedValue), ddlBulan.SelectedIndex, tgl, 0, 0, 0);
                Users user = ((Users)Session["Users"]);
                string strEvent = "Insert";
                AnalisaResikoFacade ArFacade = new AnalisaResikoFacade();
                AnaLisaResiko ArRisk = new AnaLisaResiko();
                if (ViewState["id"] != null)
                {
                    ArRisk.ID = int.Parse(ViewState["id"].ToString());
                    strEvent = "Edit";
                }

                ArRisk.TglAnalisaTrans = DateTime.Parse(txtTglAnalisaRisk.Text);
                ArRisk.DeptID = int.Parse(ddlDeptName.SelectedValue.ToString());
                ArRisk.AnalisaClassID = int.Parse(ddlklasifikasiRisk.SelectedValue.ToString());
                ArRisk.Aktivitas = txtKegiatan.Text;
                ArRisk.Risk = txtRisk.Text;
                ArRisk.IssueInternal1 = txtIssueInternal.Text;
                ArRisk.IssueEkternal1 = txtIssueExternal.Text;
                ArRisk.Peluang1 = txtOportunity.Text;
                ArRisk.LvlKemungkinan = int.Parse(ddlKemungkinan.SelectedValue.ToString());
                ArRisk.LvlDampak = int.Parse(ddlDampak.SelectedValue.ToString());
                ArRisk.LvlResiko = int.Parse(lblResiko.Text.ToString());
                ArRisk.Treatment1 = txttreatment.Text;
                ArRisk.DueDate1 = TglTarget;//DateTime.Parse(txtDueDate.Text);
                ArRisk.CreatedBy = ((Users)Session["Users"]).UserName;
                ArRisk.User_ID = user.ID;
                if (user.DeptID == 15 || user.DeptID == 13)
                {
                    ArRisk.Apv = 1;
                }
                else if (user.DeptID == 23)
                {
                    ArRisk.Apv = 2;
                }
                else
                {
                    ArRisk.Apv = 0;
                }
                int intResult = 0;
                if (ArRisk.ID > 0)
                {
                    intResult = ArFacade.UpdateAnalisaRisk(ArRisk);
                    InsertLog(strEvent);
                }
                else
                {
                    intResult = ArFacade.insertAnalisaRisk(ArRisk);
                    if (intResult > 0)
                    {
                        DisplayAJAXMessage(this, "Data Telah Disimpan");
                        InsertLog(strEvent);

                    }
                    else
                    {
                        DisplayAJAXMessage(this, "Gagal Simpan");
                    }
                }
                InsertLog(strEvent);
                btnUpdate.Enabled = false;
                clearForm();
                #endregion
            }
            else
            {
                #region Lain-lain
                string strValidate = ValidateText();
                if (strValidate != string.Empty)
                {
                    DisplayAJAXMessage(this, strValidate);
                    return;
                }

                int tgl = 0;
                int bln = 0;
                int thn = int.Parse(ddlTahun.SelectedValue);

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
                    DateTime final = new DateTime(int.Parse(ddlTahun.SelectedValue), ddlBulan.SelectedIndex, DateTime.DaysInMonth(int.Parse(ddlTahun.SelectedValue), ddlBulan.SelectedIndex));

                    tgl = final.Day;
                }
                else
                {
                    tgl = DateTime.Parse(txtDueDate.Text).Day;
                }

                DateTime TglTarget = new DateTime(int.Parse(ddlTahun.SelectedValue), ddlBulan.SelectedIndex, tgl, 0, 0, 0);
                Users user = ((Users)Session["Users"]);
                string strEvent = "Insert";
                AnalisaResikoFacade ArFacade = new AnalisaResikoFacade();
                AnaLisaResiko ArRisk = new AnaLisaResiko();
                if (ViewState["id"] != null)
                {
                    ArRisk.ID = int.Parse(ViewState["id"].ToString());
                    strEvent = "Edit";
                }

                ArRisk.TglAnalisaTrans = DateTime.Parse(txtTglAnalisaRisk.Text);
                ArRisk.DeptID = int.Parse(ddlDeptName.SelectedValue.ToString());
                ArRisk.Ket = txtKlasifikasi.Text;
                ArRisk.AnalisaClassID = int.Parse(IDLain2.Text.ToString());
                ArRisk.Aktivitas = txtKegiatan.Text;
                ArRisk.Risk = txtRisk.Text;
                ArRisk.IssueInternal1 = txtIssueInternal.Text;
                ArRisk.IssueEkternal1 = txtIssueExternal.Text;
                ArRisk.Peluang1 = txtOportunity.Text;
                ArRisk.LvlKemungkinan = int.Parse(ddlKemungkinan.SelectedValue.ToString());
                ArRisk.LvlDampak = int.Parse(ddlDampak.SelectedValue.ToString());
                ArRisk.LvlResiko = int.Parse(lblResiko.Text.ToString());
                ArRisk.Treatment1 = txttreatment.Text;
                ArRisk.DueDate1 = TglTarget;//DateTime.Parse(txtDueDate.Text);
                ArRisk.CreatedBy = ((Users)Session["Users"]).UserName;
                ArRisk.User_ID = user.ID;
                if (user.DeptID == 14 || user.DeptID == 15)
                {
                    ArRisk.Apv = 1;
                }
                else
                {
                    ArRisk.Apv = 0;
                }
                int intResult = 0;
                if (ArRisk.ID > 0)
                {
                    intResult = ArFacade.UpdateAnalisaRisk(ArRisk);
                    InsertLog(strEvent);
                }
                else
                {
                    intResult = ArFacade.insertAnalisaRisk2(ArRisk);
                    if (intResult > 0)
                    {
                        DisplayAJAXMessage(this, "Data Telah Disimpan");
                        InsertLog(strEvent);

                    }
                    else
                    {
                        DisplayAJAXMessage(this, "Gagal Simpan");
                    }
                }
                InsertLog(strEvent);
                btnUpdate.Enabled = false;
                clearForm();
                #endregion
            }
        }

        protected void btnRekap_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("RekapAnalisaResiko.aspx");
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            btnUpdate.Enabled = true;
            clearForm();
        }

        private void clearForm()
        {
            ViewState["id"] = null;
            txtTglAnalisaRisk.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtKegiatan.Text = string.Empty;
            txtRisk.Text = string.Empty;
            txtIssueInternal.Text = string.Empty;
            txtIssueExternal.Text = string.Empty;
            txtOportunity.Text = string.Empty;
            ddlKemungkinan.SelectedIndex = 0;
            ddlDampak.SelectedIndex = 0;
            lblResiko.Text = string.Empty;
            txttreatment.Text = string.Empty;
            ddlBulan.SelectedIndex = 0;
            ddlTargetM.SelectedIndex = 0;
            //txtTahun.Text = DateTime.Now.Year.ToString();
            ddlklasifikasiRisk.SelectedIndex = 0;
            txtKlasifikasi.Text = string.Empty;
        }

        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Modul ISO - Analisa Resiko";
            eventLog.EventName = eventName;
            eventLog.CreatedBy = ((Users)Session["Users"]).UserName;
            EventLogFacade eventLogFacade = new EventLogFacade();
            int intResult = eventLogFacade.Insert(eventLog);
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private string ValidateText()
        {
            //if (ddlklasifikasiRisk.SelectedIndex == 0)
            //    return "Klasifikasi Resiko Harus di Pilih..";
            if (txtKegiatan.Text == string.Empty)
                return "Kegiatan harus di Isi..";
            if (txtRisk.Text == string.Empty)
                return "Risk harus di Isi..";
            if (txtIssueInternal.Text == string.Empty)
                return "Issue Internal harus di Isi.. ";
            if (txtIssueExternal.Text == string.Empty)
                return "Issue External harus di Isi.. ";
            if (txtOportunity.Text == string.Empty)
                return "Oportunity harus di Isi..";
            if (ddlKemungkinan.SelectedIndex == 0)
                return "Level Kemungkinan Harus di Pilih..";
            if (ddlDampak.SelectedIndex == 0)
                return "Level Dampak Harus di Pilih";
            if (txttreatment.Text == string.Empty)
                return "Treatment harus di Isi..";
            if (ddlTargetM.SelectedIndex == 0)
                return "Pilih Target M";
            if (ddlBulan.SelectedIndex == 0)
                return "Pilih Bulan";
            return string.Empty;
        }
        protected void ddlklasifikasiRisk_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void RbLain_CheckedChanged(object sender, EventArgs e)
        {
            if (RbLain.Checked == true)
            {
                ddlklasifikasiRisk.Visible = false;
                txtKlasifikasi.Visible = true;
                txtKlasifikasi.Text = string.Empty;
                AnalisaResikoFacade ArFacade = new AnalisaResikoFacade();
                AnaLisaResiko ArRisk = new AnaLisaResiko();
                ArRisk = ArFacade.RetrieveById1(int.Parse(IDDept.Text));
                IDLain2.Text = ArRisk.ID.ToString();

            }
            else
            {
                ddlklasifikasiRisk.Visible = true;
                txtKlasifikasi.Visible = false;
            }
        }
        protected void RbIK_CheckedChanged(object sender, EventArgs e)
        {
            if (RbIK.Checked == true)
            {
                ddlklasifikasiRisk.Visible = true;
                txtKlasifikasi.Visible = false;
                ddlklasifikasiRisk.SelectedIndex = 0;
            }
            else
            {
                ddlklasifikasiRisk.Visible = false;
                txtKlasifikasi.Visible = true;

            }
        }
    }
}

public class AnalisaResikoFacade
{
    public string strError = string.Empty;
    private ArrayList arrData = new ArrayList();
    private List<SqlParameter> sqlListParam;
    private AnaLisaResiko objRisk = new AnaLisaResiko();

    public AnalisaResikoFacade()
            : base()
    {

    }

    public int insertAnalisaRisk(object objDomain)
    {
        try
        {
            objRisk = (AnaLisaResiko)objDomain;
            sqlListParam = new List<SqlParameter>();
            sqlListParam.Add(new SqlParameter("@TglAnalisaTrans", objRisk.TglAnalisaTrans));
            sqlListParam.Add(new SqlParameter("@DeptID", objRisk.DeptID));
            sqlListParam.Add(new SqlParameter("@AnalisaClassID", objRisk.AnalisaClassID));
            sqlListParam.Add(new SqlParameter("@Aktivitas", objRisk.Aktivitas));
            sqlListParam.Add(new SqlParameter("@Risk", objRisk.Risk));
            sqlListParam.Add(new SqlParameter("@IssueInternal1", objRisk.IssueInternal1));
            //sqlListParam.Add(new SqlParameter("@IssueInternal2", objRisk.IssueInternal2));
            sqlListParam.Add(new SqlParameter("@IssueEkternal1", objRisk.IssueEkternal1));
            //sqlListParam.Add(new SqlParameter("@IssueEkternal2", objRisk.IssueEkternal2));
            sqlListParam.Add(new SqlParameter("@Peluang1", objRisk.Peluang1));
            //sqlListParam.Add(new SqlParameter("@Peluang2", objRisk.Peluang2));
            sqlListParam.Add(new SqlParameter("@LvlKemungkinan", objRisk.LvlKemungkinan));
            sqlListParam.Add(new SqlParameter("@LvlDampak", objRisk.LvlDampak));
            sqlListParam.Add(new SqlParameter("@LvlResiko", objRisk.LvlResiko));
            sqlListParam.Add(new SqlParameter("@Treatment1", objRisk.Treatment1));
            //sqlListParam.Add(new SqlParameter("@Treatment2", objRisk.Treatment2));
            sqlListParam.Add(new SqlParameter("@DueDate1", objRisk.DueDate1));
            sqlListParam.Add(new SqlParameter("@Apv", objRisk.Apv));
            sqlListParam.Add(new SqlParameter("@CreatedBy", objRisk.CreatedBy));
            sqlListParam.Add(new SqlParameter("@User_ID", objRisk.User_ID));
            DataAccess da = new DataAccess(Global.ConnectionString());
            int result = da.ProcessData(sqlListParam, "AnaRisk_Insert");
            strError = da.Error;
            return result;
        }
        catch (Exception ex)
        {
            strError = ex.Message;
            return -1;
        }
    }

    public int insertAnalisaRisk2(object objDomain)
    {
        try
        {
            objRisk = (AnaLisaResiko)objDomain;
            sqlListParam = new List<SqlParameter>();
            sqlListParam.Add(new SqlParameter("@TglAnalisaTrans", objRisk.TglAnalisaTrans));
            sqlListParam.Add(new SqlParameter("@DeptID", objRisk.DeptID));
            sqlListParam.Add(new SqlParameter("@Ket", objRisk.Ket));
            sqlListParam.Add(new SqlParameter("@AnalisaClassID", objRisk.AnalisaClassID));
            sqlListParam.Add(new SqlParameter("@Aktivitas", objRisk.Aktivitas));
            sqlListParam.Add(new SqlParameter("@Risk", objRisk.Risk));
            sqlListParam.Add(new SqlParameter("@IssueInternal1", objRisk.IssueInternal1));
            //sqlListParam.Add(new SqlParameter("@IssueInternal2", objRisk.IssueInternal2));
            sqlListParam.Add(new SqlParameter("@IssueEkternal1", objRisk.IssueEkternal1));
            //sqlListParam.Add(new SqlParameter("@IssueEkternal2", objRisk.IssueEkternal2));
            sqlListParam.Add(new SqlParameter("@Peluang1", objRisk.Peluang1));
            //sqlListParam.Add(new SqlParameter("@Peluang2", objRisk.Peluang2));
            sqlListParam.Add(new SqlParameter("@LvlKemungkinan", objRisk.LvlKemungkinan));
            sqlListParam.Add(new SqlParameter("@LvlDampak", objRisk.LvlDampak));
            sqlListParam.Add(new SqlParameter("@LvlResiko", objRisk.LvlResiko));
            sqlListParam.Add(new SqlParameter("@Treatment1", objRisk.Treatment1));
            //sqlListParam.Add(new SqlParameter("@Treatment2", objRisk.Treatment2));
            sqlListParam.Add(new SqlParameter("@DueDate1", objRisk.DueDate1));
            sqlListParam.Add(new SqlParameter("@Apv", objRisk.Apv));
            sqlListParam.Add(new SqlParameter("@CreatedBy", objRisk.CreatedBy));
            sqlListParam.Add(new SqlParameter("@User_ID", objRisk.User_ID));
            DataAccess da = new DataAccess(Global.ConnectionString());
            int result = da.ProcessData(sqlListParam, "AnaRisk_Insert2");
            strError = da.Error;
            return result;
        }
        catch (Exception ex)
        {
            strError = ex.Message;
            return -1;
        }
    }

    public int UpdateAnalisaRisk(object objDomain)
    {
        try
        {
            objRisk = (AnaLisaResiko)objDomain;
            DataAccess da = new DataAccess(Global.ConnectionString());
            int result = da.ProcessData(sqlListParam, "AnaRisk_Update");
            strError = da.Error;
            return result;
        }
        catch (Exception ex)
        {
            strError = ex.Message;
            return -1;
        }
    }

    public AnaLisaResiko RetrieveById1(int DeptID)
    {

        string StrSql = " select ID From ISO_AnalisaRMaster where DeptID=" + DeptID + " and AnalisaResiko='Lain-lain' ";
        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
        strError = dataAccess.Error;

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return GenerateObject(sqlDataReader);
            }
        }

        return new AnaLisaResiko();
    }

    private AnaLisaResiko GenerateObject(SqlDataReader sdr)
    {
        AnaLisaResiko xx1 = new AnaLisaResiko();
        xx1.ID = Convert.ToInt32(sdr["ID"]);
        return xx1;
    }


}

public class AnaLisaResiko : GRCBaseDomain
{
    public DateTime TglAnalisaTrans { get; set; }
    public int DeptID { get; set; }
    public int AnalisaClassID { get; set; }
    public string Aktivitas { get; set; }
    public string Risk { get; set; }
    public string IssueInternal1 { get; set; }
    //public string IssueInternal2 { get; set; }
    //public string IssueInternal3 { get; set; }
    public string IssueEkternal1 { get; set; }
    //public string IssueEkternal2 { get; set; }
    //public string IssueEkternal3 { get; set; }
    public string Peluang1 { get; set; }
    //public string Peluang2 { get; set; }
    //public string Peluang3 { get; set; }
    public int LvlKemungkinan { get; set; }
    public int LvlDampak { get; set; }
    public int LvlResiko { get; set; }
    public string Treatment1 { get; set; }
    //public string Treatment2 { get; set; }
    //public string Treatment3 { get; set; }
    public DateTime DueDate1 { get; set; }
    //public DateTime DueDate2 { get; set; }
    public int Apv { get; set; }
    public int Status { get; set; }
    public int User_ID { get; set; }
    public string Ket { get; set; }

}