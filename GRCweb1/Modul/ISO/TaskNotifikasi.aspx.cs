using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Reflection;

namespace GRCweb1.Modul.ISO
{
    public partial class TaskNotifikasi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadTask();
            }
        }
        private void LoadTask()
        {
            try
            {
                ArrayList arrData = new ArrayList();
                Notifikasi note = new Notifikasi();
                note.SubQuery = (target.Checked == true) ? "Select * from (" : "";
                note.EmailISO = (((Users)Session["Users"]).UnitKerjaID == 7) ? "iso_krwg@grcboard.com" : "iso_ctrp@grcboard.com";
                note.Criteria = " where TglSelesai is null and (t.RowStatus >-1 and t.Status !=9)";
                note.Criteria += (target.Checked == true) ? " And Approval=1 and Convert(char,x.TglTargetSelesai,112)in(" +
                                                            " left(Convert(CHAR,TglTargetSelesai,112),6)+'7'," +
                                                            " left(Convert(CHAR,TglTargetSelesai,112),6)+'14'," +
                                                            " left(Convert(CHAR,TglTargetSelesai,112),6)+'21'," +
                                                            " left(Convert(CHAR,TglTargetSelesai,112),6)+'30'," +
                                                            " left(Convert(CHAR,TglTargetSelesai,112),6)+'31'," +
                                                            " left(Convert(CHAR,TglTargetSelesai,112),6)+'29'," +
                                                            " left(Convert(CHAR,TglTargetSelesai,112),6)+'28')" +
                                                            " ) as xx where CONVERT(char,xx.TglTargetSelesai,112)< convert(char,GETDATE(),112)" : "";
                note.Target = (mustApproval.Checked == true) ? " And Approval=0" : "";// " And Approval=1 and Convert(char,ISO_TaskDetail.TglTargetSelesai,112)=Convert(char,DATEADD(DAY,-2,GETDATE()),112) ";
                arrData = note.Retrieve();
                lstTask.DataSource = arrData;
                lstTask.DataBind();
                //SendNotifikasi(arrData);
            }
            catch (Exception e)
            {
                string strError = e.Message;
                Global.link = "~/Default.aspx";
            }
        }
        protected void mustAproval_Change(object sender, EventArgs e)
        {
            if (mustApproval.Checked == true)
            {
                LoadTask();
            }
        }
        protected void target_Change(object sender, EventArgs e)
        {
            if (target.Checked == true)
            {
                LoadTask();
            }
        }
        protected void lstTask_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lbl = (Label)e.Item.FindControl("stsTask");
            Note da = (Note)e.Item.DataItem;
            ArrayList arrData = new ArrayList();
            arrData.Add(da);
            Notifikasi nf = new Notifikasi();
            nf.Criteria = "TaskID,TaskDetailID,UserMail,HeadMail,EmailKe";
            nf.Target = (target.Checked == true) ? " and EmailKe=0" : "";
            if (nf.CheckTaskID(da.TaskID, da.ID) == 0)
            {
                int result = nf.ProcessData(da, "spISO_Notifikasi_Insert");
                if (mustApproval.Checked == true)
                {
                    SendNotifikasi(arrData);
                }
                else
                {

                    KirimEmail(arrData);
                }
            }
            else
            {
                if (nf.CheckLastEmail(da.TaskID, da.ID) > 3 && mustApproval.Checked == true)
                {
                    SendNotifikasi(arrData);
                }
                else if (nf.CheckLastEmail(da.TaskID, da.ID) > 3 && target.Checked == true)
                {
                    KirimEmail(arrData);
                }
            }
            lbl.Text = nf.CheckSendEmail(da.TaskID, da.ID);
        }
        public void KirimEmail(ArrayList arrData)
        {
            foreach (Note nt in arrData)
            {
                MailMessage msg = new MailMessage();
                EmailReportFacade emailFacade = new EmailReportFacade();
                msg.From = new MailAddress("system_support@grcboard.com");
                //msg.To.Add("noreplay@grcboard.com");
                msg.To.Add(nt.UserMail.ToString() + "," + nt.HeadMail.ToString());
                msg.Subject = "Remainder Task";
                msg.Body += "Di Informasikan Bahwa :\n\r";
                msg.Body += "No. Task    : " + nt.TaskNo + "\n\r";
                msg.Body += "Target Ke  : " + nt.TargetKe.ToString() + "\n\r";
                msg.Body += "Target Date : " + nt.TglTarget.ToString("dd-MMM-yyyy") + "\n\r";
                msg.Body += "PIC         : " + nt.Pic.ToUpperInvariant() + "\n\r";
                msg.Body += "SUDAH MELAMPAUI TANGGAL TARGET YANG DITENTUKAN " + "\n\r";
                msg.Body += "Mohon untuk di check kembali " + "\n\r";
                msg.Body += "Terima Kasih " + "\n\rAuto Remainder by System";
                SmtpClient smt = new SmtpClient(emailFacade.mailSmtp());
                smt.Host = emailFacade.mailSmtp();
                smt.Port = emailFacade.mailPort();
                smt.EnableSsl = true;
                smt.DeliveryMethod = SmtpDeliveryMethod.Network;
                smt.UseDefaultCredentials = false;
                smt.Credentials = new System.Net.NetworkCredential("noreplay@grcboard.com", "grc123!@#");
                //bypas certificate
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                        System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                        System.Security.Cryptography.X509Certificates.X509Chain chain,
                        System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
                smt.Send(msg);
            }
        }
        private void SendNotifikasi(ArrayList arrData)
        {
            string plant = (((Users)Session["Users"]).UnitKerjaID == 1) ? "ctrp" : "krwg";
            string plant1 = (((Users)Session["Users"]).UnitKerjaID == 1) ? "123.123.123.129" : "10.0.0.252";
            foreach (Note noted in arrData)
            {
                string email = string.Empty;
                MailMessage msg = new MailMessage();
                EmailReportFacade emailFacade = new EmailReportFacade();
                email = (mustApproval.Checked == true) ? noted.HeadMail.ToString() : noted.UserMail.ToString();
                if (email != string.Empty)
                {
                    msg.From = new MailAddress("system_support@grcboard.com");
                    msg.To.Add(noted.HeadMail);//noted.UserEmail);
                                               //msg.To.Add("noreplay@grcboard.com");
                    msg.Subject = "Approval Task";
                    msg.Body += emailFacade.mailBody1Task() + "\n\r";
                    msg.Body += "Taks No    : " + noted.TaskNo + "\n\r";
                    msg.Body += "Task Name  : " + noted.NewTask + "\n\r";
                    msg.Body += "Task Date : " + noted.TglMulai.ToString("dd-MMM-yyyy") + "\n\r";
                    msg.Body += "Target Ke : " + noted.TargetKe.ToString() + " [ " + noted.TglTarget.ToString("dd-MM-yyyy") + "]\n\r";
                    msg.Body += "PIC         : " + noted.Pic.ToUpperInvariant() + "\n\r";
                    msg.Body += "Silahkan Klik : http://" + plant1 + ":212" + "\n\r";
                    msg.Body += "atau Klik : http://" + plant + ".grcboard.com" + "\n\r\n\r";
                    msg.Body += "Terimakasih, " + "\n\r";
                    msg.Body += "Salam GRCBOARD " + "\n\r\n\r\n\r";
                    //msg.Body += "Auto's, " + "\n\r";
                    //msg.Body += ((Users)Session["Users"]).UserName + "\n\r\n\r\n\r";
                    //msg.Body += emailFacade.mailFooter();
                    SmtpClient smt = new SmtpClient(emailFacade.mailSmtp());
                    smt.Host = emailFacade.mailSmtp();
                    smt.Port = emailFacade.mailPort();
                    smt.EnableSsl = true;
                    smt.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smt.UseDefaultCredentials = false;
                    smt.Credentials = new System.Net.NetworkCredential("noreplay@grcboard.com", "grc123!@#");
                    //bypas certificate
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                            System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                            System.Security.Cryptography.X509Certificates.X509Chain chain,
                            System.Net.Security.SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };
                    smt.Send(msg);
                    //update status
                    UpdateStatusData(noted.TaskID, noted.ID);
                }
            }
        }
        private void UpdateStatusData(int ID, int DetailID)
        {
            Notifikasi ntn = new Notifikasi();
            Note nt = new Note();
            nt.TaskID = ID;
            nt.TaskDetailID = DetailID;
            nt.EmailKe = ntn.CheckEmailKe(ID, DetailID) + 1;
            ntn.Criteria = "TaskID,TaskDetailID,EmailKe";
            ntn.EmailISO = (((Users)Session["Users"]).UnitKerjaID == 7) ? "iso_krwg@grcboard.com" : "ctrp_krwg@grcboard.com";
            int result = ntn.ProcessData(nt, "spISO_Notifikasi_Update");

        }
    }
}

public class Note : Task
{
    public string UserName { get; set; }
    public string UserHead { get; set; }
    public string UserMail { get; set; }
    public string HeadMail { get; set; }
    public int TaskDetailID { get; set; }
    public int EmailKe { get; set; }
    public DateTime SendDate { get; set; }
}
public class Notifikasi
{
    private ArrayList arrD = new ArrayList();
    private List<SqlParameter> sqlListParam;
    private Note alk = new Note();
    private ArrayList arrData = new ArrayList();
    public string Criteria { get; set; }
    public string Target { get; set; }
    public string EmailISO { get; set; }
    public string SubQuery { get; set; }
    private string Query()
    {

        string query = "Set DATEFIRST 1; " + this.SubQuery + " select x.ID,x.TaskID,TglMulai,TargetKe,TglTargetSelesai,TglSelesai,TaskNo,TaskName,PIC,t.UserID,t.CreatedBy, " +
                       " (select top 1 isnull(UserMail,'" + this.EmailISO + "') from vw_ISO_Usrmail c where c.UserID=t.UserID)UserMail, " +
                       " (select top 1 isnull(HeadMail,'" + this.EmailISO + "') from vw_ISO_Usrmail c where c.UserID=t.UserID)HeadMail,t.Status,Approval, " +
                       " (select top 1 UserName from vw_ISO_Usrmail c where c.UserID=t.UserID)UserName, " +
                       " (select top 1 HeadName from vw_ISO_Usrmail c where c.UserID=t.UserID)HeadName, " +
                       " (Select dbo.GetWorkingDay(TglMulai,GETDATE()))Hari " +
                       " from ( " +
                       " select ROW_NUMBER() over(Partition by TaskID order by ID)Nom, * from ISO_TaskDetail  " +
                       " where  RowStatus >-1 and Status =0 " +
                       this.Target +
                       " ) as X " +
                       " Left join ISO_Task t " +
                       " on t.ID=X.TaskID  and year(TglMulai)>2014 " +
                      this.Criteria +

                       " order by TaskNo,UserID";
        return query;
    }
    public ArrayList Retrieve()
    {
        arrData = new ArrayList();
        DataAccess dta = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = dta.RetrieveDataByString(this.Query());
        if (dta.Error == string.Empty && sdr.HasRows/**/)
        {
            while (sdr.Read())
            {
                if (Convert.ToInt32(sdr["Hari"].ToString()) > 3)
                {
                    arrData.Add(new Note
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        TaskNo = sdr["TaskNo"].ToString(),
                        TglMulai = Convert.ToDateTime(sdr["TglMulai"].ToString()),
                        TargetKe = Convert.ToInt32(sdr["TargetKe"].ToString()),
                        TglTarget = Convert.ToDateTime(sdr["TglTargetSelesai"].ToString()),
                        NewTask = sdr["TaskName"].ToString(),
                        UserName = sdr["UserName"].ToString(),
                        UserHead = sdr["HeadName"].ToString(),
                        Pic = sdr["PIC"].ToString(),
                        UserMail = sdr["UserMail"].ToString(),
                        HeadMail = sdr["HeadMail"].ToString(),
                        TaskID = Convert.ToInt32(sdr["TaskID"].ToString()),
                        TaskDetailID = Convert.ToInt32(sdr["ID"].ToString())
                    });
                }
            }
        }
        return arrData;
    }
    public int Update(ArrayList arrData)
    {
        int result = 0;

        return result;
    }
    public int ProcessData(object arrAL, string spName)
    {
        try
        {
            alk = (Note)arrAL;
            string[] arrCriteria = this.Criteria.Split(',');
            PropertyInfo[] data = alk.GetType().GetProperties();
            DataAccess da = new DataAccess(Global.ConnectionString());
            var equ = new List<string>();
            sqlListParam = new List<SqlParameter>();
            foreach (PropertyInfo items in data)
            {
                if (items.GetValue(alk, null) != null && arrCriteria.Contains(items.Name))
                {
                    sqlListParam.Add(new SqlParameter("@" + items.Name.ToString(), items.GetValue(alk, null)));
                }
            }
            int result = da.ProcessData(sqlListParam, spName);
            string err = da.Error;
            return result;
        }
        catch (Exception ex)
        {
            string er = ex.Message;
            return -1;
        }
    }
    public int CheckTaskID(int ID, int DetailID)
    {
        int resutl = 0;
        string strSQL = "Select ID from ISO_Notifikasi where TaskID=" + ID + " and TaskDetailID=" + DetailID + this.Target;
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(strSQL);
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                resutl = Convert.ToInt32(sdr["ID"].ToString());
            }
        }
        return resutl;
    }
    public int CheckEmailKe(int ID, int DetailID)
    {
        int resutl = 0;
        string strSQL = "Select EmailKe from ISO_Notifikasi where TaskID=" + ID + " and TaskDetailID=" + DetailID;
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(strSQL);
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                resutl = Convert.ToInt32(sdr["EmailKe"].ToString());
            }
        }
        return resutl;
    }
    public int CheckLastEmail(int ID, int DetailID)
    {
        int resutl = 0;
        string strSQL = "SET DATEFIRST 1; Select (Select dbo.GetWorkingDay(SendDate,GETDATE()))Hari from ISO_Notifikasi " +
                        "where TaskID=" + ID + " and TaskDetailID=" + DetailID;
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(strSQL);
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                resutl = Convert.ToInt32(sdr["Hari"].ToString());
            }
        }
        return resutl;
    }
    public string CheckSendEmail(int ID, int DetailID)
    {
        string resutl = string.Empty;
        string strSQL = "Select EmailKe,Convert(Char,SendDate,103)Tgl from ISO_Notifikasi where TaskID=" + ID + " and TaskDetailID=" + DetailID;
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(strSQL);
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                resutl = (sdr["EmailKe"].ToString() == "0") ? "Email on " + sdr["Tgl"].ToString() : "Email Ke " + sdr["EmailKe"] + " on " + sdr["Tgl"].ToString();
            }
        }
        return resutl;
    }
}