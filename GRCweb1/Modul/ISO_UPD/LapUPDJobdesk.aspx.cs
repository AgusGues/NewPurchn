using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using Domain;
using DataAccessLayer;
using BusinessFacade;
using Factory;
using BasicFrame.WebControls;
using System.ComponentModel;

namespace GRCweb1.Modul.ISO_UPD
{
    public partial class LapUPDJobdesk : System.Web.UI.Page
    {
        public string Tahun = Global.nBulan(DateTime.Now.Month);
        private ArrayList arrData = new ArrayList();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadTahun();
                LoadDept();
            }
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('jd', 450, 150 , 40 ,false); </script>", false);
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem rpt in lstDept.Items)
            {
                Repeater pic = (Repeater)rpt.FindControl("ListJobDesk");
                //((Image)rpt.FindControl("lstEdit")).Visible = false;
                //((Image)rpt.FindControl("lstDel")).Visible = false;
                foreach (RepeaterItem rp in pic.Items)
                {
                    ((Image)rp.FindControl("lstEdit")).Visible = false;
                    ((Image)rp.FindControl("lstDel")).Visible = false;
                }

            }
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=LaporanJobDesk.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<H2>LIST DATA UPD JOBDESK</H2>";
            Html += "<br>Departement : &nbsp;" + ddlDept.SelectedItem.Text;
            Html += "";
            string HtmlEnd = "";
            div2.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0\"", "border=\"1\"");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            ListJOBDESK();
        }
        protected void ddlDept_Change(object sender, EventArgs e)
        {

        }
        protected void ddlTahun_Change(object sender, EventArgs e)
        {

        }

        protected void lstDept_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater ListJobDesk = (Repeater)e.Item.FindControl("ListJobDesk");
            JobDesk jobdesk = (JobDesk)e.Item.DataItem;
            JobDeskFacade jobdeskFacade = new JobDeskFacade();
            ArrayList arrData = new ArrayList();
            arrData = jobdeskFacade.RetrieveLapJOBDESK(ddlTahun.SelectedValue, jobdesk.DeptID.ToString());

            ListJobDesk.DataSource = arrData;
            ListJobDesk.DataBind();

        }

        private void ListJOBDESK()
        {
            JobDeskFacade jd = new JobDeskFacade();
            arrData = new ArrayList();
            arrData = jd.RetrieveLapJOBDESK2(ddlDept.SelectedValue.ToString());
            lstDept.DataSource = arrData;
            lstDept.DataBind();

        }

        protected void ListJobDesk_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ArrayList arrData = new ArrayList();
            JobDesk jobDesk = (JobDesk)e.Item.DataItem;
            Image cetakJOBDESK = (Image)e.Item.FindControl("lstEdit");
            string querystr = jobDesk.ID.ToString();
            Label lbl = (Label)e.Item.FindControl("slvd");
            switch (jobDesk.Approval)
            {
                case 0:
                    lbl.Text = "Open";
                    break;
                case 1:
                    lbl.Text = "Approved Hrd";
                    break;
                case 2:
                    lbl.Text = "Approved Manager";
                    break;
                case 3:
                    lbl.Text = "Approved Plant Manager/Corp Manager";
                    break;
                case 4:
                    lbl.Text = "Approved Corp Manager Hrd";
                    break;
                case 5:
                    lbl.Text = "Approved Iso";
                    break;
                case 6:
                    lbl.Text = "Distributed By Iso";
                    break;
                case 7:
                    lbl.Text = "Finish";
                    break;
                default:
                    lbl.Text = "";
                    break;
            }
        }

        protected void ListJobDesk_Command(object sender, RepeaterCommandEventArgs e)
        {
            JobDesk jobDesk = (JobDesk)e.Item.DataItem;
            if (e.CommandName == "Add")
            {
                //int index = Convert.ToInt32(e.CommandArgument);
                Label IDstr = (Label)e.Item.FindControl("id");
                //TextBox ID = (TextBox)e.Item.FindControl("id");
                //GridViewRow row = GridView1.Rows[index];

                Response.Redirect("JobDeskInput.aspx?ID=" + IDstr.Text);

            }
            //if (e.CommandName == "cetak")
            //{
            //    //Session["ID"] = jobDesk.ID;
            //    //string IDstr = jobDesk.ID.ToString();
            //    Label IDstr = (Label)e.Item.FindControl("id");

            //    //string strQuery = string.Empty;
            //    ////strQuery = "SELECT * FROM JobDesk AS jd LEFT JOIN JobDeskDetail AS jdd ON jd.ID=jdd.JOBDESKID WHERE jd.RowStatus>-1 AND jdd.RowStatus>-1 and " +
            //    ////           "jd.JOBDESK_No='" + txtJOBDESK_No.Text.Trim() + "' ";
            //    //strQuery = "SELECT jd.ID, jdd.JOBDESKID, jd.JOBDESK_No, jd.DeptID, d.Alias, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, " +
            //    //           "jd.Approval,jdd.TujuanUmumJabatan, jdd.TugasPokokJabatan, jdd.HubunganKerja, jdd.TanggungJawab, jdd.Wewenang, jdd.PendidikanPengalaman, " +
            //    //           "jdd.Pengetahuan,jdd.Keterampilan, jdd.Fisik, jdd.NonFisik, jdd.Bawahan FROM Dept AS d left join JobDesk AS jd on d.ID=jd.DeptID LEFT JOIN JobDeskDetail AS jdd " +
            //    //           "ON jd.ID=jdd.JOBDESKID WHERE jd.RowStatus>-1 AND jdd.RowStatus>-1 and jd.ID='" + IDstr + "' ";

            //    string strQuery = "SELECT ID, DeptID, Jabatan, Atasan, TglSusun, Revisi, Status, Approval, RowStatus " +
            //                    "FROM JobDesk WHERE RowStatus>-1 AND " +
            //                    "ID='" + IDstr + "' ";
            //    string strQuery1 = "SELECT jd.ID, jdd.Bawahan FROM JobDesk as jd inner join JobDeskBawahan as jdd on jd.ID=jdd.JOBDESKID WHERE jd.RowStatus>-1 and jdd.RowStatus>-1 AND jd.ID='" + IDstr + "' ";
            //    string strQuery2 = "SELECT jd.ID, jdd.TujuanUmumJabatan FROM JobDesk as jd inner join JobDeskTUJabatan as jdd on jd.ID=jdd.JOBDESKID WHERE jd.RowStatus>-1 and jdd.RowStatus>-1 AND jd.ID='" + IDstr + "' ";
            //    string strQuery3 = "SELECT jd.ID, jdp.TugasPokokJabatan FROM JobDesk as jd inner join JobDeskTPJabatan as jdp on jd.ID=jdp.JOBDESKID WHERE jd.RowStatus>-1 and jdp.RowStatus>-1 AND jd.ID='" + IDstr + "' ";
            //    string strQuery4 = "SELECT jd.ID, jdp.HubunganKerja FROM JobDesk as jd inner join JobDeskHK as jdp on jd.ID=jdp.JOBDESKID WHERE jd.RowStatus>-1 and jdp.RowStatus>-1 AND jd.ID='" + IDstr + "' ";
            //    string strQuery5 = "SELECT jd.ID, jdp.TanggungJawab FROM JobDesk as jd inner join JobDeskTJ as jdp on jd.ID=jdp.JOBDESKID WHERE jd.RowStatus>-1 and jdp.RowStatus>-1 AND jd.ID='" + IDstr + "' ";
            //    string strQuery6 = "SELECT jd.ID, jdp.Wewenang FROM JobDesk as jd inner join JobDeskWewenang as jdp on jd.ID=jdp.JOBDESKID WHERE jd.RowStatus>-1 and jdp.RowStatus>-1 AND jd.ID='" + IDstr + "' ";
            //    string strQuery7 = "SELECT jd.ID, jdp.Pendidikan FROM JobDesk as jd inner join JobDeskPendidikan as jdp on jd.ID=jdp.JOBDESKID WHERE jd.RowStatus>-1 and jdp.RowStatus>-1 AND jd.ID='" + IDstr + "' ";
            //    string strQuery8 = "SELECT jd.ID, jdp.Pengalaman FROM JobDesk as jd inner join JobDeskPengalaman as jdp on jd.ID=jdp.JOBDESKID WHERE jd.RowStatus>-1 and jdp.RowStatus>-1 AND jd.ID='" + IDstr + "' ";
            //    string strQuery9 = "SELECT jd.ID, jdp.Pengetahuan FROM JobDesk as jd inner join JobDeskPengetahuan as jdp on jd.ID=jdp.JOBDESKID WHERE jd.RowStatus>-1 and jdp.RowStatus>-1 AND jd.ID='" + IDstr + "' ";
            //    string strQuery10 = "SELECT jd.ID, jdp.Keterampilan FROM JobDesk as jd inner join JobDeskKeterampilan as jdp on jd.ID=jdp.JOBDESKID WHERE jd.RowStatus>-1 and jdp.RowStatus>-1 AND jd.ID='" + IDstr + "' ";
            //    string strQuery11 = "SELECT jd.ID, jdp.Fisik FROM JobDesk as jd inner join JobDeskFisik as jdp on jd.ID=jdp.JOBDESKID WHERE jd.RowStatus>-1 and jdp.RowStatus>-1 AND jd.ID='" + IDstr + "' ";
            //    string strQuery12 = "SELECT jd.ID, jdp.NonFisik FROM JobDesk as jd inner join JobDeskNonFisik as jdp on jd.ID=jdp.JOBDESKID WHERE jd.RowStatus>-1 and jdp.RowStatus>-1 AND jd.ID='" + IDstr + "' ";

            //    if (IDstr != null)
            //    {
            //        //Session["query"] = strQuery;
            //        //Session["xjudul"] = "Job Desk";
            //        //Session["formno"] = "Form No. HRD/JD/44/09/R0";
            //        //Session["namaPT"] = "PT.BANGUNPERKASA ADHITAMASENTRA";
            //        Session["Query"] = strQuery;
            //        Session["Query1"] = strQuery1;
            //        Session["Query2"] = strQuery2;
            //        Session["Query3"] = strQuery3;
            //        Session["Query4"] = strQuery4;
            //        Session["Query5"] = strQuery5;
            //        Session["Query6"] = strQuery6;
            //        Session["Query7"] = strQuery7;
            //        Session["Query8"] = strQuery8;
            //        Session["Query9"] = strQuery9;
            //        Session["Query10"] = strQuery10;
            //        Session["Query11"] = strQuery11;
            //        Session["Query12"] = strQuery12;

            //        Cetak(this);
            //    }
            //}
        }

        static public void Cetak(Control page)
        {
            string myScript = "var wn = window.showModalDialog('Report.aspx?IdReport=jobdeskLaporan', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 780px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private void LoadTahun()
        {
            JobDeskFacade p = new JobDeskFacade();
            ArrayList arrData = new ArrayList();
            arrData = p.LoadTahun();
            ddlTahun.Items.Clear();
            if (arrData.Count == 0)
            {
                ddlTahun.Items.Add(new ListItem(DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString()));
            }
            foreach (JobDesk jobdesk in arrData)
            {
                ddlTahun.Items.Add(new ListItem(jobdesk.Tahun.ToString(), jobdesk.Tahun.ToString()));
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }

        private void LoadDept()
        {
            UsersFacade usersFacade = new UsersFacade();
            Users users2 = usersFacade.RetrieveByUserName(((Users)Session["Users"]).UserName);
            JOBD rps = new JOBD();
            ArrayList arrDept = new ArrayList();
            ddlDept.Items.Clear();
            ddlDept.Items.Add(new ListItem("-- All Dept --", "0"));
            arrDept = rps.RetrieveDept();
            string dpName = string.Empty;

            foreach (JOB dept in arrDept)
            {
                switch (users2.UnitKerjaID)
                {
                    case 1:
                    case 7:
                        dpName = (dept.DeptID == 4 || dept.DeptID == 5 || dept.DeptID == 18 || dept.DeptID == 19) ? "MAINTENANCE" : dept.DeptName;
                        if (((Users)Session["Users"]).Apv == 0 && dept.DeptID != 4 && dept.DeptID != 5 && dept.DeptID != 18 && dept.DeptID != 1)
                        {
                            ddlDept.Items.Add(new ListItem(dpName, dept.DeptID.ToString()));
                        }
                        else
                        {
                            if (((Users)Session["Users"]).DeptID == dept.DeptID)
                            {
                                ddlDept.Items.Add(new ListItem(dpName, dept.DeptID.ToString()));
                            }
                        }
                        break;
                    default:
                        //ddlDept.Items.Add(new ListItem(dept.DeptName, dept.DeptID.ToString()));
                        break;
                }

            }
            //ddlDept.SelectedValue = ((Users)Session["Users"]).DeptID.ToString();
        }
    }
}

public class JOBD
{
    private ArrayList arrData = new ArrayList();
    private JOB pm = new JOB();
    public string Criteria { get; set; }
    public string Field { get; set; }
    public string Where { get; set; }
    public ArrayList RetrieveDept()
    {
        arrData = new ArrayList();
        string strSQL = "Select * from Dept Where RowStatus>-1 " + this.Criteria + " order by DeptName";
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(strSQL);
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrData.Add(new JOB
                {
                    DeptID = Convert.ToInt32(sdr["ID"].ToString()),
                    DeptName = sdr["DeptName"].ToString()
                });
            }
        }
        return arrData;
    }
}

public class JOB
{
    public int ID { get; set; }
    public string DeptName { get; set; }
    public int DeptID { get; set; }
}