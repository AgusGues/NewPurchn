using System;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using BusinessFacade;
using Domain;
using DataAccessLayer;

namespace GRCweb1.Modul.ISO
{
    public partial class LapTask : System.Web.UI.Page
    {
        public decimal TotalBobot = 0;
        public decimal TotalScore = 0;
        public decimal score = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                txtTanggal.Text = DateTime.Now.AddDays(-(DateTime.Now.Day - 1)).ToString("dd-MMM-yyyy");
                txtSdTanggal.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                LoadDept();
                ddlDept.SelectedValue = ((Users)Session["Users"]).DeptID.ToString();
                LoadPIC();
            }
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('thr', 250, 98 , 20 ,false); </script>", false);
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=RekapTask.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<b>REKAP TASK</b>";
            Html += "<br>Periode : " + txtTanggal.Text + "  " + txtSdTanggal.Text;
            Html += "<br>Departement :" + ddlDept.SelectedItem.Text;
            string HtmlEnd = "";
            //lstForPrint.RenderControl(hw);
            lstr.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0", "border=\"1");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }
        protected void ddlPIC_SelectedChange(object sender, EventArgs e)
        {
            LoadList();
        }
        protected void ddlstatus_SelectedChange(object sender, EventArgs e)
        {
            LoadList();
        }
        private void LoadDept()
        {
            #region depreciated line
            //Users users = (Users)Session["Users"];
            //ddlDept.Items.Clear();

            //ArrayList arrDept = new ArrayList();
            //DeptFacade deptFacade = new DeptFacade();
            //arrDept = deptFacade.RetrieveByDepoID(users.CompanyID);

            //if (users.UnitKerjaID == 100)
            //    arrPettyCash = pettyCashFacade.RetrieveByCriteria("ptid", users.UnitKerjaID.ToString());
            ////arrPettyCash = pettyCashFacade.RetrieveByIdPusat();
            //else
            //{
            //    arrPettyCash = pettyCashFacade.RetrieveByDepoIDandCompanyID(users.UnitKerjaID, users.CompanyID);
            //}

            //foreach (Dept dept in arrDept)
            //{
            //    ddlDept.Items.Add(new ListItem(dept.DeptName, dept.ID.ToString()));
            //}
            #endregion
            Users users = (Users)Session["Users"];
            DeptFacade deptFacade = new DeptFacade();
            UsersFacade usersFacade = new UsersFacade();
            ArrayList arrDept = new ArrayList();
            #region methode lama
            //if (users.UserLevel >= 1)
            //{
            //    deptFacade = new DeptFacade();
            //    arrDept = deptFacade.RetrieveByAll();

            //}
            //else
            //{
            //    usersFacade = new UsersFacade();
            //    Users users2 = usersFacade.RetrieveByUserName(users.UserName);

            //    deptFacade = new DeptFacade();
            //    arrDept = deptFacade.RetrieveByUserID(users2.ID);
            //}
            #endregion
            deptFacade.Criteria = " and ID in(" + UserDeptAuth() + ")";
            deptFacade.Criteria += " order by Alias";
            arrDept = deptFacade.RetrieveAliasDept();
            ddlDept.Items.Clear();
            ddlDept.Items.Add(new ListItem("-- Choose Dept --", "0"));
            if (deptFacade.Error == string.Empty)
            {
                foreach (Dept dept in arrDept)
                {
                    ddlDept.Items.Add(new ListItem(dept.AlisName, dept.ID.ToString()));
                }
            }
        }
        private void LoadPIC()
        {
            string strTgl = DateTime.Parse(txtTanggal.Text).ToString("yyyyMMdd");
            string strSdTgl = DateTime.Parse(txtSdTanggal.Text).ToString("yyyyMMdd");
            ArrayList arrData = new ArrayList();
            Laporan lp = new Laporan();
            lp.Criteria = " Where XXX.BagianID is not null  Order by Cast(XXX.Urutan as int),UserName ";
            lp.DeptPilihan = (ddlDept.SelectedIndex > 0) ? " and UA.DeptID=" + ddlDept.SelectedValue : " ";
            lp.TglMulai = strTgl;
            lp.TglSelesai = strSdTgl;
            lp.Pilihan = "PIC";
            ddlPICs.Items.Clear();
            ddlPICs.Items.Add(new ListItem("--Pilih PIC--", "0"));
            arrData = lp.Retrieve();
            foreach (Lap ta in arrData)
            {
                ddlPICs.Items.Add(new ListItem(ta.UserName, ta.UserID.ToString()));
            }
        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            string strTgl = DateTime.Parse(txtTanggal.Text).ToString("yyyyMMdd");
            string strSdTgl = DateTime.Parse(txtSdTanggal.Text).ToString("yyyyMMdd");
            int intDeptID = 0;
            intDeptID = int.Parse(ddlDept.SelectedValue);

            ReportFacade reportFacade = new ReportFacade();
            string strQuery = string.Empty;

            Users users = (Users)Session["Users"];

            UsersFacade usersFacade = new UsersFacade();
            Users users2 = usersFacade.RetrieveByUserName(users.UserName);

            DeptFacade deptFacade = new DeptFacade();
            Dept dept = deptFacade.RetrieveDeptByUserGroup(users2.ID);
            int intUserID = users2.ID;

            if (deptFacade.Error == string.Empty)
            {
                strQuery = reportFacade.ViewTask3(strTgl, strSdTgl, intDeptID, ddlPICs.SelectedItem.ToString(), int.Parse(ddlPICs.SelectedValue));
            }

            Session["Query"] = strQuery;
            Session["TglTask"] = DateTime.Parse(txtTanggal.Text).ToString("dd/MM/yyyy");
            Session["sdTglTask"] = DateTime.Parse(txtSdTanggal.Text).ToString("dd/MM/yyyy");
            if (ddlDept.SelectedIndex == 0)
                Session["group"] = 1;
            else
                Session["group"] = 2;
            Cetak(this);
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        static public void Cetak(Control page)
        {
            string myScript = "var wn = window.showModalDialog('../Report/Report.aspx?IdReport=LapTask', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        protected void ddlDept_SelectedChange(object sender, EventArgs e)
        {
            LoadPIC();
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            LoadList();
        }
        private void LoadList()
        {
            ArrayList arrData = new ArrayList();
            Laporan nl = new Laporan();
            nl.Pilihan = "Dept";
            //nl.Status = ddlstatus.SelectedValue.ToString();
            nl.Criteria = (ddlDept.SelectedIndex == 0) ? " and ID in(" + this.UserDeptAuth() + ")" : " and ID=" + ddlDept.SelectedValue;
            arrData = nl.Retrieve();
            lstDept.DataSource = arrData;
            lstDept.DataBind();
        }
        protected void lstDept_DataBound(object sender, RepeaterItemEventArgs e)
        {
            TotalScore = 0;
            TotalBobot = 0;
            string strTgl = DateTime.Parse(txtTanggal.Text).ToString("yyyyMMdd");
            string strSdTgl = DateTime.Parse(txtSdTanggal.Text).ToString("yyyyMMdd");
            ArrayList arrData = new ArrayList();
            Lap lp = (Lap)e.Item.DataItem;
            Repeater lstPC = (Repeater)e.Item.FindControl("lstPIC");
            Laporan lpl = new Laporan();
            lpl.TglMulai = strTgl;
            lpl.TglSelesai = strSdTgl;
            lpl.DeptPilihan = (ddlDept.SelectedIndex > 0) ? " and UA.DeptID=" + ddlDept.SelectedValue : "";
            lpl.Criteria = " WHERE XXX.BagianID is not null and XXX.DeptID=" + lp.DeptID;
            lpl.Criteria += (ddlPICs.SelectedIndex == 0) ? "" : " and XXX.UserID='" + ddlPICs.SelectedValue.ToString() + "'";
            lpl.Criteria += " GROUP BY UserID,UserName,BagianName,BagianID,DeptID,NIK,Urutan ";
            lpl.Criteria += " Order by Cast(XXX.Urutan as int),XXX.UserName ";
            lpl.Pilihan = "PIC";
            lstPC.DataSource = lpl.Retrieve();
            lstPC.DataBind();

        }
        protected void lstPIC_DataBound(object sender, RepeaterItemEventArgs e)
        {
            TotalBobot = 0; TotalScore = 0;
            score = 0; decimal bobotTask = 0;
            ArrayList arrData = new ArrayList();
            ArrayList arrD = new ArrayList();
            Repeater lstPC = (Repeater)e.Item.FindControl("lstTaks");
            string strTgl = DateTime.Parse(txtTanggal.Text).ToString("yyyyMMdd");
            string strSdTgl = DateTime.Parse(txtSdTanggal.Text).ToString("yyyyMMdd");
            Label minBobot = (Label)e.Item.FindControl("min");
            Label BobotPES = (Label)e.Item.FindControl("bbPES");
            decimal minimalTask = 0;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Lap lp = (Lap)e.Item.DataItem;
                Laporan lpl = new Laporan();
                lpl.Criteria = " and PIC='" + this.ISOName(lp.UserID.ToString()) + "'";
                lpl.Criteria += " and BagianID=" + lp.BagianID;
                //lpl.Criteria += (wUnsolve.Checked == true) ? " and (" : " and ";
                //lpl.Criteria += "Convert(Char,TglSelesai,112) between '" + strTgl + "' and '" + strSdTgl + "'";
                //lpl.Criteria += (wUnsolve.Checked == true) ? " OR (Convert(Char,TglMulai,112) between '" + strTgl + "' and '" + strSdTgl + "' and ISNULL(TglSelesai ,'1/1/1900')='1/1/1900'))" : "";
                lpl.Criteria += (ddlstatus.SelectedIndex == 0) ? " and (" : " and ";
                lpl.Criteria += ((ddlstatus.SelectedIndex == 0) || (ddlstatus.SelectedIndex == 1)) ? "Convert(Char,TglSelesai,112) between '" + strTgl + "' and '" + strSdTgl + "'" : "";
                lpl.Criteria += (ddlstatus.SelectedIndex == 0) ? " OR (Convert(Char,TglMulai,112) between '" + strTgl + "' and '" + strSdTgl + "' and (RowStatus>-1 AND RowStatus!=9) and ISNULL(TglSelesai ,'1/1/1900')='1/1/1900'))" : "";
                lpl.Criteria += (ddlstatus.SelectedIndex == 2) ? " ISNULL(TglSelesai ,'1/1/1900')='1/1/1900' and (RowStatus>-1 AND RowStatus!=9)" : "";
                lpl.Criteria += (ddlstatus.SelectedIndex == 3) ? " Convert(Char,TglMulai,112) between '" + strTgl + "' and '" + strSdTgl + "' and Status=9 and RowStatus=9" : "";
                lpl.Pilihan = "Solve";
                arrData = lpl.Retrieve();
                lstPC.DataSource = arrData;
                lstPC.DataBind();
                this.DeptID = lp.DeptID;
                this.Bagian = lp.BagianID;
                bobotTask = GetBobotPES(DateTime.Parse(txtSdTanggal.Text).Month, DateTime.Parse(txtSdTanggal.Text).Year, 2);
                int bbte = (Convert.ToInt32(bobotTask * 100));
                switch (bbte)
                {
                    case 10:
                        minimalTask = 6;
                        break;
                    case 15:
                        minimalTask = 8;
                        break;
                    default:
                        minimalTask = (bbte - 10);
                        break;

                }
                minBobot.Text = minimalTask.ToString("##0");
                BobotPES.Text = (bobotTask * 100).ToString("##0");
            }



        }
        protected void lstTask_DataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                decimal scr = 0;
                try
                {
                    Lap lp = (Lap)e.Item.DataItem;
                    Laporan lpl = new Laporan();
                    Label T1 = (Label)e.Item.FindControl("T1");
                    Label T2 = (Label)e.Item.FindControl("T2");
                    Label T3 = (Label)e.Item.FindControl("T3");
                    Label T4 = (Label)e.Item.FindControl("T4");
                    Label T5 = (Label)e.Item.FindControl("T5");
                    Label T6 = (Label)e.Item.FindControl("T6");
                    Label Point = (Label)e.Item.FindControl("Point");
                    Label Score = (Label)e.Item.FindControl("score");
                    Label TotSC = (Label)e.Item.FindControl("ttSc");
                    Label TglTar = (Label)e.Item.FindControl("TglSelesai");
                    HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("xx");
                    T1.Text = TglTarget(1, lp.ID);
                    T2.Text = TglTarget(2, lp.ID);
                    T3.Text = TglTarget(3, lp.ID);
                    T4.Text = TglTarget(4, lp.ID);
                    T5.Text = TglTarget(5, lp.ID);
                    T6.Text = TglTarget(6, lp.ID);
                    //ArdiYoga (WO tgl selesai d tambahkan status progress)
                    DateTime aDate = DateTime.Now;
                    if (lp.TglSelesai != null && lp.App != 0)
                    {
                        TglTar.Text = Convert.ToDateTime(lp.TglSelesai).ToString("dd/MM/yyyy");
                    }
                    //else if (TglTar.Text != null && T2.Text == null)
                    //{
                    //    TglTar.Text = Convert.ToDateTime(lp.TglSelesai).ToString("dd/MM/yyyy");
                    //}
                    else if (T2.Text == "" && Convert.ToDateTime(aDate) > DateTime.Parse(T1.Text))
                    {
                        TglTar.Text = "Naik Target";
                    }
                    else if (T2.Text != "" && T3.Text == "" && Convert.ToDateTime(aDate) > DateTime.Parse(T2.Text) && DateTime.Parse(T2.Text) > DateTime.Parse(T1.Text))
                    {
                        TglTar.Text = "Naik Target";
                    }
                    else if (T3.Text != "" && T4.Text == "" && Convert.ToDateTime(aDate) > DateTime.Parse(T3.Text) && DateTime.Parse(T3.Text) > DateTime.Parse(T2.Text))
                    {
                        TglTar.Text = "Naik Target";
                    }
                    else if (T4.Text != "" && T5.Text == "" && Convert.ToDateTime(aDate) > DateTime.Parse(T4.Text) && DateTime.Parse(T4.Text) > DateTime.Parse(T3.Text))
                    {
                        TglTar.Text = "Naik Target";
                    }
                    else if (T5.Text != "" && T6.Text == "" && Convert.ToDateTime(aDate) > DateTime.Parse(T5.Text) && DateTime.Parse(T5.Text) > DateTime.Parse(T4.Text))
                    {
                        TglTar.Text = "Naik Target";
                    }
                    else if (T6.Text != "" && Convert.ToDateTime(aDate) > DateTime.Parse(T6.Text) && DateTime.Parse(T6.Text) > DateTime.Parse(T5.Text))
                    {
                        TglTar.Text = "Naik Target";
                    }
                    else
                    {
                        TglTar.Text = "Progress";
                    }
                    //ArdiYoga
                    Point.Text = GetPoint(lp.ID).ToString("###,##0");
                    Point.ForeColor = (GetPoint(lp.ID) < 0) ? System.Drawing.Color.Red : System.Drawing.Color.Black;
                    score = (lp.TotalBBT > 0) ? (lp.BobotNilai / lp.TotalBBT) * GetPoint(lp.ID) : 0;
                    Score.Text = score.ToString("###,##0.00");
                    TotalScore = (score > -101) ? (TotalScore + score) : TotalScore;
                    TotSC.Text = TotalScore.ToString("###,##0.00");
                    tr.Visible = (e.Item.ItemIndex + 1 == lp.TaskID) ? true : false;
                }
                catch { }
            }
        }
        private string UserDeptAuth()
        {
            string result = ((Users)Session["Users"]).DeptID.ToString();
            ZetroView zv = new ZetroView();
            zv.QueryType = Operation.CUSTOM;
            zv.CustomQuery = "Select * from UserDeptAuth where RowStatus>-1 and UserID=" + ((Users)Session["Users"]).ID;
            zv.CustomQuery += " and ModulName='Rekap Task'";
            SqlDataReader sdr = zv.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = sdr["DeptID"].ToString();
                }
            }
            return result;
        }
        private string TglTarget(int TargetKe, int TaskID)
        {
            string result = string.Empty;
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "Select TglTargetSelesai from ISO_TaskDetail where RowStatus>-1 and TaskID=" + TaskID + " and TargetKe=" + TargetKe;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToDateTime(sdr["TglTargetSelesai"].ToString()).ToString("dd/MM/yyyy");
                }
            }
            return result;
        }
        private decimal GetPoint(int TaskID)
        {
            decimal result = 0;
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "Select top 1 isnull(PointNilai,0)PointNilai from ISO_TaskDetail where RowStatus>-1 and TaskID=" + TaskID;
            zw.CustomQuery += " Order by ID Desc";
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToDecimal(sdr["PointNilai"].ToString());
                }
            }
            return result;
        }
        private int DeptID { get; set; }
        private int Bagian { get; set; }
        public decimal GetBobotPES(int Bulan, int Tahun, int PesType)
        {
            decimal bobot = 0;
            string query = "Select top 1 isnull((Bobot),0) bobot from ISO_BobotPES where " +
                            "CAST(RTRIM(LTRIM(cast(activetahun as char(4))))+''+" +
                            "(Right('0'+ RTRIM(LTRIM(CAST(activebulan as CHAR))),2)) as int)<=" + Tahun + Bulan.ToString().PadLeft(2, '0');
            query += " and DeptID=" + this.DeptID + " and BagianID=" + this.Bagian;
            query += " and PesType=" + PesType;
            query += " order by ID desc ";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(query);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    bobot = Convert.ToDecimal(sdr["bobot"].ToString());
                }
            }
            return bobot;
        }
        private string ISOName(string UserID)
        {
            string result = string.Empty;
            string query = "Select * from ISO_Users where ID=" + UserID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(query);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = sdr["UserName"].ToString();
                }
            }
            return result;
        }
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}