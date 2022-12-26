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
//using System.Drawing;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
////using DefectFacade;
using Factory;
using DataAccessLayer;
using System.IO;
using BasicFrame.WebControls;
using System.Text.RegularExpressions;
using System.Collections.Specialized;

namespace GRCweb1.Modul.ISO
{
    public partial class ApprovalAnalisaResiko : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Users user = ((Users)Session["Users"]);
                //AnalisaResikoFacade1 riskF = new AnalisaResikoFacade1();
                Ax riskD = new Ax();
                string UserID = string.Empty;
                UserID = user.ID.ToString();
                string usertype = GetUserType(UserID);
                Session["usertype"] = usertype;
                LoadOpenARisk();
                LoadDataARisk();
                LoadDept();
                LoadTahun();
                btnApprove.Enabled = false;
                btnUnApprove.Enabled = false;
            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }

        private void LoadDept()
        {
            Users users = (Users)Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            if (users.DeptID == 23 || users.DeptID == 14)
            {
                zl.CustomQuery = "select * from SPD_Dept where RowStatus>-1 order by dept";
            }
            else
            {
                zl.CustomQuery = "select * from SPD_Dept where RowStatus>-1 and dptID in (" + users.DeptID + ") order by dept";
            }
            SqlDataReader sdr = zl.Retrieve();
            ddlDeptName.Items.Clear();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlDeptName.Items.Add(new ListItem(sdr["Dept"].ToString(), sdr["ID"].ToString()));
                }
            }
        }

        private ArrayList LoadDataARisk()
        {
            Users users = (Users)Session["Users"];
            ArrayList arrARisk = new ArrayList();
            string UserInput1 = Session["UserInput1"].ToString();
            string UserHead = users.ID.ToString();
            arrARisk = RetrieveARisk(UserHead, UserInput1);
            lstPMX.DataSource = arrRisk;
            lstPMX.DataBind();
            return arrARisk;
        }

        protected void chk_CheckedChange(object sender, EventArgs e)
        {
            int i = 0;
            string transID = string.Empty;
            foreach (RepeaterItem objDetail in lstPMX.Items)
            {
                CheckBox chk = (CheckBox)lstPMX.Items[i].FindControl("chkprs");
                chk.Checked = chkAll.Checked;
                transID = chk.ToolTip;
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                //if (chk.Checked == true)
                //    zl.CustomQuery = "update spd_trans set checked=1 where id=" + transID;
                //else
                //    zl.CustomQuery = "update spd_trans set checked=0 where id=" + transID;
                SqlDataReader sdr = zl.Retrieve();
                i++;

                btnApprove.Enabled = true;
                btnUnApprove.Enabled = true;
            }
        }

        private void LoadOpenARisk()
        {
            Users user = ((Users)Session["Users"]);
            ArrayList arrARisk = new ArrayList();
            string UserID = string.Empty;
            UserID = user.ID.ToString();
            string Apv = GetApv(UserID);
            string ApvARisk = GetStatusApv(UserID);
            Session["Apv"] = Apv;
            Session["ApvARisk"] = ApvARisk;
            string UserInput = GetUserI(UserID);
            string UserInput1 = GetUserIGrid(UserID);
            Session["UserInput"] = UserInput;
            Session["UserInput1"] = UserInput1;
            arrARisk = RetrieveOpenARHeader(UserInput, Apv);
            foreach (Ax aRsik in arrARisk)
            {
                if (aRsik.ID != 0)
                {
                    ArID.Value += aRsik.ID + ",";
                }
            }
        }

        public ArrayList arrRisk;
        private ArrayList RetrieveOpenARHeader(string UserInput, string Apv)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            arrRisk = new ArrayList();
            int UserID = users.ID;
            arrRisk = RetrieveForOpenARisk(UserID, UserInput, Apv);
            return arrRisk;
        }

        private ArrayList RetrieveARisk(string UserHead, string UserInput1)
        {
            Users users = (Users)Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = " select * from  (  select A.ID,A.TglAnalisaTrans,A.DeptID,C.Dept,A.AnalisaClassID,B.AnalisaResiko,A.Ket,A.Aktivitas,A.Risk,A.IssueInternal1,A.IssueEkternal1," +
                             "A.Peluang1,A.LvlKemungkinan,A.LvlDampak,A.LvlResiko,case when A.LvlResiko >=10 then 'HIGH' when A.LvlResiko >=6  then 'MEDIUM' when A.LvlResiko >=1 " +
                             " then 'LOW' end LvlResiko1,  A.Treatment1,case when DAY (A.DueDate1) between  1 and 7 then 'M1' when Day(A.DueDate1) between 8 and 14 Then 'M2' when  DAY(A.DueDate1) " +
                             " between 15 and 21  then 'M3' else 'M4' end DueDate,  Month(A.DueDate1) Bulan,YEAR(A.DueDate1)Tahun,A.Apv,case when A.Apv='0' then 'Open' when A.Apv='1' then 'Manager' when A.Apv='2' " +
                             " then 'Corp.Plant Mgr/Corp.Mgr' else 'Corp.ISO Manager'  end Approval,Case when A.Status=0 then 'UnSolved' else 'Solved' end [Statusx],A.RowStatus,case when MONTH(A.DueDate1) between 1 and 6 then 'Semester I' else 'Semester II' end Periode " +
                             " from ISO_AnalisaTrans A  inner join ISO_AnalisaRMaster B on A.AnalisaClassID=B.ID  inner join ISO_DeptRisk C on A.DeptID=C.ID ) " +
                             " as data1 where data1.RowStatus>-1 and data1.DeptID in(select DptID from ISO_AnalisaRUsers where RowStatus > -1 and [User_ID]=" + UserHead + ") " +
                             " and data1.Apv=(select top 1 Approval  from ISO_AnalisaRUsers where RowStatus > -1 and User_ID=" + UserHead + ")-1";
            SqlDataReader sdr = zl.Retrieve();
            arrRisk = new ArrayList();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrRisk.Add(new Ax
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        Dept = sdr["Dept"].ToString(),
                        AnalisaResiko = sdr["AnalisaResiko"].ToString(),
                        Ket = sdr["Ket"].ToString(),
                        Risk = sdr["Risk"].ToString(),
                        Aktivitas = sdr["Aktivitas"].ToString(),
                        IssueInternal1 = sdr["IssueInternal1"].ToString(),
                        //IssueInternal2 = sdr["IssueInternal2"].ToString(),
                        IssueEkternal1 = sdr["IssueEkternal1"].ToString(),
                        //IssueEkternal2 = sdr["IssueEkternal2"].ToString(),
                        Peluang1 = sdr["Peluang1"].ToString(),
                        //Peluang2 = sdr["Peluang2"].ToString(),
                        LvlKemungkinan = Convert.ToInt32(sdr["LvlKemungkinan"].ToString()),
                        LvlDampak = Convert.ToInt32(sdr["LvlDampak"].ToString()),
                        LvlResiko = Convert.ToInt32(sdr["LvlResiko"].ToString()),
                        LvlResiko1 = sdr["LvlResiko1"].ToString(),
                        Treatment1 = sdr["Treatment1"].ToString(),
                        //Treatment2 = sdr["Treatment2"].ToString(),
                        DueDate = sdr["DueDate"].ToString(),
                        Bulan = sdr["Bulan"].ToString(),
                        Tahun = sdr["Tahun"].ToString(),
                        Approval = sdr["Approval"].ToString(),
                        //FileName = sdr["FileName"].ToString(),
                        StatusX = sdr["StatusX"].ToString(),
                    });
                }
            }
            else
                arrRisk.Add(new Ax());
            return arrRisk;

        }


        private ArrayList RetrieveForOpenARisk(int UserID, string UserInput, string Apv)
        {
            Users users = (Users)Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = " select * from  (  select A.ID,A.TglAnalisaTrans,A.DeptID,C.Dept,A.AnalisaClassID,B.AnalisaResiko,A.Ket,A.Aktivitas,A.Risk,A.IssueInternal1,A.IssueEkternal1," +
                             " A.Peluang1,A.LvlKemungkinan,A.LvlDampak,A.LvlResiko,case when A.LvlResiko >=10 then 'HIGH' when A.LvlResiko >=6  then 'MEDIUM' when A.LvlResiko >=1 " +
                             " then 'LOW' end LvlResiko1,  A.Treatment1, case when DAY (A.DueDate1) between  1 and 7 then 'M1' when Day(A.DueDate1) between 8 and 14 Then 'M2' when  DAY(A.DueDate1) " +
                             " between 15 and 21  then 'M3' else 'M4' end DueDate,  Month(A.DueDate1) Bulan,YEAR(A.DueDate1)Tahun,A.Apv,case when A.Apv='0' then 'Open' when A.Apv='1' then 'Manager' when A.Apv='2' " +
                             " then 'Corp.Plant Mgr/Corp.Mgr' else 'Corp.ISO Manager' end Approval,Case when A.Status=0 then 'UnSolved' else 'Solved' end [Statusx],A.RowStatus, case when MONTH(A.DueDate1) between 1 and 6 then 'Semester I' else 'Semester II' end Periode" +
                             " from ISO_AnalisaTrans A  inner join ISO_AnalisaRMaster B on A.AnalisaClassID=B.ID  inner join ISO_DeptRisk C on A.DeptID=C.ID) " +
                             " as data1 where data1.Apv=" + Apv + "-1 and data1.RowStatus>-1 and data1.DeptID in(select DptID from ISO_AnalisaRUsers where [User_ID]='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ")";
            SqlDataReader sdr = zl.Retrieve();
            arrRisk = new ArrayList();
            //if (sdr != null)
            //{
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrRisk.Add(new Ax
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        Dept = sdr["Dept"].ToString(),
                        AnalisaResiko = sdr["AnalisaResiko"].ToString(),
                        Ket = sdr["Ket"].ToString(),
                        Risk = sdr["Risk"].ToString(),
                        Aktivitas = sdr["Aktivitas"].ToString(),
                        IssueInternal1 = sdr["IssueInternal1"].ToString(),
                        //IssueInternal2 = sdr["IssueInternal2"].ToString(),
                        IssueEkternal1 = sdr["IssueEkternal1"].ToString(),
                        //IssueEkternal2 = sdr["IssueEkternal2"].ToString(),
                        Peluang1 = sdr["Peluang1"].ToString(),
                        //Peluang2 = sdr["Peluang2"].ToString(),
                        LvlKemungkinan = Convert.ToInt32(sdr["LvlKemungkinan"].ToString()),
                        LvlDampak = Convert.ToInt32(sdr["LvlDampak"].ToString()),
                        LvlResiko = Convert.ToInt32(sdr["LvlResiko"].ToString()),
                        LvlResiko1 = sdr["LvlResiko1"].ToString(),
                        Treatment1 = sdr["Treatment1"].ToString(),
                        //Treatment2 = sdr["Treatment2"].ToString(),
                        DueDate = sdr["DueDate"].ToString(),
                        Bulan = sdr["Bulan"].ToString(),
                        Tahun = sdr["Tahun"].ToString(),
                        Approval = sdr["Approval"].ToString(),
                        //FileName = sdr["FileName"].ToString(),
                        StatusX = sdr["StatusX"].ToString(),
                    });
                }
            }
            else
                arrRisk.Add(new Ax());
            return arrRisk;
            //}
        }

        private string GetUserType(string userID)
        {
            Users users = (Users)Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select keterangan from ISO_AnalisaRUsers where user_id=" + userID;
            SqlDataReader sdr = zl.Retrieve();
            string usertype = string.Empty;
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    usertype = sdr["keterangan"].ToString();
                }
            }
            return usertype;
        }

        private string GetApv(string UserID)
        {
            Users users = (Users)Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select Approval as Apv from ISO_AnalisaRUsers where User_ID=" + UserID + " and rowstatus>-1";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    return sdr["Apv"].ToString();
                }
            }
            return string.Empty;
        }

        private string GetStatusApv(string UserID)
        {
            Users users = (Users)Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select Apv from ISO_AnalisaTrans where RowStatus > -1 and DeptID in (select Dept_ID from ISO_AnalisaRUsers where RowStatus > -1 " +
                "and User_ID=" + UserID + ")";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    return sdr["Apv"].ToString();
                }
            }
            return string.Empty;
        }

        private string GetUserI(string UserID)
        {
            Users users = (Users)Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select User_ID from ISO_AnalisaTrans where DeptID in (select Dept_ID from ISO_AnalisaRUsers where User_ID=" + UserID + ") and apv=0 and RowStatus>-1";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    return sdr["User_ID"].ToString();
                }
            }
            return string.Empty;
        }

        private string GetUserIGrid(string UserID)
        {
            Users users = (Users)Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select User_ID from ISO_AnalisaTrans where DeptID in (select Dept_ID from ISO_AnalisaRUsers where User_ID=" + UserID + ") and RowStatus>-1";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    return sdr["User_ID"].ToString();
                }
            }
            return string.Empty;
        }

        //protected void chk_CheckedChangePrs(object sender, EventArgs e)
        //{
        //    CheckBox chk = (CheckBox)sender;
        //    string transID = chk.ToolTip;
        //    ZetroView zl = new ZetroView();
        //}

        private void LoadTahun()
        {
            ArrayList arrD = this.ListBATahun();
            ddlTahun.Items.Clear();
            foreach (BeritaAcara ba in arrD)
            {
                ddlTahun.Items.Add(new ListItem(ba.Tahun.ToString(), ba.Tahun.ToString()));
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

        //Penambahan agus 31-08-2022
        private string cekApproval(string UserID)
        {
            Users users = (Users)Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            //zl.CustomQuery = "select Apv from Users where ID in(" + UserID + ") and RowStatus>-1";
            zl.CustomQuery = "select Approval from ISO_AnalisaRUsers where User_ID in(" + UserID + ") and RowStatus>-1";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    return sdr["Approval"].ToString();
                }
            }
            return string.Empty;
        }
        //Penambahan agus 31-08-2022

        //Penambahan agus 31-08-2022
        private string cekApproval2(string UserID)
        {
            Users users = (Users)Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            //zl.CustomQuery = "select Apv from Users where ID in(" + UserID + ") and RowStatus>-1";
            zl.CustomQuery = "select Approval from ISO_AnalisaRUsers where Dept_ID not in(23,14,15,26,13,7) and User_ID in(" + UserID + ") and RowStatus>-1";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    return sdr["Approval"].ToString();
                }
            }
            return string.Empty;
        }
        //Penambahan agus 31-08-2022
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstPMX.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)lstPMX.Items[i].FindControl("chkprs");
                if (chk.Checked == true)
                {
                    Users user = (Users)Session["Users"];
                    Ax riskD = new Ax();

                    string cekUser = user.ID.ToString();
                    string hasil = cekApproval(cekUser);
                    string hasil2 = cekApproval2(cekUser);

                    int Result = 0;
                    
                    string strError = string.Empty;
                    string UserID = user.ID.ToString();
                    int Apv = Convert.ToInt32(Session["Apv"].ToString());

                    riskD.Apv = Apv;
                    riskD.ID = int.Parse(chk.ToolTip.ToString());
                    riskD.User_ID = int.Parse(UserID);
                    riskD.LastModifiedBy = user.UserName;
                    riskD.DeptID = ((Users)Session["Users"]).DeptID;
                    string strSQL;
                    
                    //Penambahan agus 31-08-2021

                    //if (user.DeptID == 23 || user.DeptID == 14 || user.DeptID == 15 || user.DeptID == 26 || user.DeptID == 13 || user.DeptID == 7)
                    if (user.DeptID == 23 && hasil=="1" || user.DeptID == 14 && hasil == "1" || user.DeptID == 15 && hasil == "1" || user.DeptID == 26 && hasil == "1" || user.DeptID == 13 && hasil == "1" || user.DeptID == 7 && hasil == "1")
                    
                    {
                        //strSQL = "update ISO_AnalisaTrans set Apv =" + Apv + " ,LastModifiedTime = GETDATE() , LastModifiedBy = '" + ((Users)Session["Users"]).UserName + "' where ID = " + chk.ToolTip.ToString() + " ";

                        strSQL = "update ISO_AnalisaTrans set Apv =" + Apv + " +1 ,LastModifiedTime = GETDATE() , LastModifiedBy = '" + ((Users)Session["Users"]).UserName + "' where ID = " + chk.ToolTip.ToString() + " ";

                    }else if(hasil2 == "1")
                    {
                        strSQL = "update ISO_AnalisaTrans set Apv =" + Apv + " +1 ,LastModifiedTime = GETDATE() , LastModifiedBy = '" + ((Users)Session["Users"]).UserName + "' where ID = " + chk.ToolTip.ToString() + " ";
                    }
                    else
                    {
                        strSQL = "update ISO_AnalisaTrans set Apv =" + Apv + " ,LastModifiedTime = GETDATE() , LastModifiedBy = '" + ((Users)Session["Users"]).UserName + "' where ID = " + chk.ToolTip.ToString() + " ";
                        //strSQL = "update ISO_AnalisaTrans set Apv =" + Apv + " +1 ,LastModifiedTime = GETDATE() , LastModifiedBy = '" + ((Users)Session["Users"]).UserName + "' where ID = " + chk.ToolTip.ToString() + " ";
                    }

                    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
                    if (dataAccess.Error != string.Empty)
                        Result = -1;
                    //return Result;
                }
            }
            LoadOpenARisk();
            LoadDataARisk();
            btnApprove.Enabled = false;
            btnUnApprove.Enabled = false;
        }

        //Penambahan agus 31-08-2021
        protected void btnExport_Click(object sender, EventArgs e)
        {

        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlTahun_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lstPMX_Command(object sender, RepeaterCommandEventArgs e)
        {
        }

        protected void lstPMX_Databound(object sender, RepeaterItemEventArgs e)
        {
        }

        protected void chk_CheckedChangePrs(object sender, EventArgs e)
        {
            Ax rmm = new Ax();
            CheckBox chk = (CheckBox)sender;
            if (chk.Checked == true)
            {
                btnApprove.Enabled = true; btnUnApprove.Enabled = true;

            }
            else if (chk.Checked == false)
            {
                btnApprove.Enabled = false; btnUnApprove.Enabled = false;
            }
        }
    }
}

public class Ax : GRCBaseDomain
{
    public DateTime TglAnalisaTrans { get; set; }
    public int DeptID { get; set; }
    public string Dept { get; set; }
    public string AnalisaResiko { get; set; }
    public int AnalisaClassID { get; set; }
    public string Aktivitas { get; set; }
    public string Risk { get; set; }
    public string IssueInternal1 { get; set; }
    public string IssueInternal2 { get; set; }
    public string IssueEkternal1 { get; set; }
    public string IssueEkternal2 { get; set; }
    public string Peluang1 { get; set; }
    public string Peluang2 { get; set; }
    public int LvlKemungkinan { get; set; }
    public int LvlDampak { get; set; }
    public int LvlResiko { get; set; }
    public string LvlResiko1 { get; set; }
    public string Treatment1 { get; set; }
    public string Treatment2 { get; set; }
    public DateTime DueDate1 { get; set; }
    public DateTime DueDate2 { get; set; }
    public int Apv { get; set; }
    public int Approval1 { get; set; }
    public string Approval { get; set; }
    public int Status { get; set; }
    public string DueDate { get; set; }
    public string Bulan { get; set; }
    public string Tahun { get; set; }
    public string FileName { get; set; }
    public int SarmutransID { get; set; }
    public string DocName { get; set; }
    public string StatusX { get; set; }
    public int User_ID { get; set; }
    public string Ket { get; set; }
}