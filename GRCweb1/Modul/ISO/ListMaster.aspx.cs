using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Domain;
using BusinessFacade;
using DataAccessLayer;

namespace GRCweb1.Modul.ISO
{
    public partial class ListMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadDept();
                ddlDept.SelectedValue = ((Users)Session["Users"]).DeptID.ToString();
                LoadPIC();
            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            ArrayList arrDept = new ArrayList();
            string dpName = string.Empty;
            MasterPES mp = new MasterPES();
            mp.Criteria = (ddlDept.SelectedIndex == 0) ? "" : " and ID=" + ddlDept.SelectedValue.ToString();
            //string forHoOnly = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("HeadOffice", "PES").ToString();
            foreach (PESM dept in mp.RetrieveDept())
            {
                if (((Users)Session["Users"]).UnitKerjaID != 10)
                {
                    if (dept.DeptName.Trim().Length > 5)
                        dpName = ((dept.DeptName.Substring(0, 5).Trim().ToUpper() == "MAINT")) ? "MAINTENANCE" : dept.DeptName;
                    else
                        dpName = dept.DeptName;
                    if ((((Users)Session["Users"]).Apv > 0 || ((Users)Session["Users"]).UserLevel > 0)
                        //&& dept.DeptID != 4 && dept.DeptID != 5 && dept.DeptID != 18 && dept.DeptID != 1
                        )
                    {
                        arrDept.Add(new PESM
                        {
                            DeptID = dept.DeptID,
                            DeptName = dpName
                        });
                    }
                    else
                    {
                        if (((Users)Session["Users"]).DeptID == dept.DeptID)
                        {
                            arrDept.Add(new PESM
                            {
                                DeptID = dept.DeptID,
                                DeptName = dpName
                            });
                        }
                    }
                }
                else
                {
                    dpName = dept.DeptName;
                    if ((((Users)Session["Users"]).Apv > 0 || ((Users)Session["Users"]).UserLevel > 0))
                    {
                        arrDept.Add(new PESM
                        {
                            DeptID = dept.DeptID,
                            DeptName = dpName
                        });
                    }
                }
            }
            lstDept.DataSource = arrDept;
            lstDept.DataBind();
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=ListMasterSOPKPI.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "Departement :" + ddlDept.SelectedItem.Text;
            Html += "<br>PIC     : " + ddlPIC.SelectedItem.Text;
            string HtmlEnd = "";
            //lstForPrint.RenderControl(hw);
            lst.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0\"", "border=\"1\"");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }
        protected void ddlDept_SelectedChange(object sender, EventArgs e)
        {
            LoadPIC();
            btnPreview_Click(null, null);
        }
        protected void ddlPIC_Change(object sender, EventArgs e)
        {
            btnPreview_Click(null, null);
        }
        protected void lstDept_DataBound(object sender, RepeaterItemEventArgs e)
        {
            MasterPES mp = new MasterPES();
            Repeater lstpic = (Repeater)e.Item.FindControl("lstPIC");
            PESM pm = (PESM)e.Item.DataItem;
            if (ddlPIC.SelectedIndex == 0 && ddlDept.SelectedIndex == 0)
            {
                mp.Criteria = "and ua.DeptID=" + pm.DeptID;
            }
            else if (ddlPIC.SelectedIndex > 0 && ddlDept.SelectedIndex > 0)
            {
                mp.Criteria = " and ua.DeptID=" + ddlDept.SelectedValue.ToString();
                mp.Criteria += " and ua.UserID=" + ddlPIC.SelectedValue.ToString();
            }
            else if (ddlPIC.SelectedIndex == 0 && ddlDept.SelectedIndex > 0)
            {
                mp.Criteria = "and ua.DeptID=" + ddlDept.SelectedValue.ToString();
            }

            mp.Field = "PICSop";
            lstpic.DataSource = mp.Retrieve();
            lstpic.DataBind();
        }
        protected void lstPIC_DataBound(object sender, RepeaterItemEventArgs e)
        {
            MasterPES mp = new MasterPES();
            ArrayList arrP = new ArrayList();
            PESM pm = (PESM)e.Item.DataItem;
            Repeater lstcat = (Repeater)e.Item.FindControl("lstCat");
            mp.Field = "PESType";
            arrP = mp.Retrieve();

            lstcat.DataSource = arrP;
            lstcat.DataBind();
        }
        protected void lstCat_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ArrayList arrData = new ArrayList();
            ArrayList arrTot = new ArrayList();
            PESM pmp = new PESM();
            Repeater lstSOP = (Repeater)e.Item.FindControl("lstPES");
            //Repeater lstTot = (Repeater)e.Item.FindControl("lstToBot");
            MasterPES mp = new MasterPES();
            Repeater lstP = (Repeater)sender;
            var data = ((RepeaterItem)e.Item.Parent.Parent).DataItem;
            PESM pm = (PESM)data;
            PESM pm2 = (PESM)e.Item.DataItem;
            mp.Criteria = " and ic.DeptID=" + pm.DeptID;
            mp.Criteria += " and iu.UserID=" + pm.ISOUserID;
            mp.Criteria += " and ic.PesType=" + pm2.ID.ToString();
            mp.Criteria += " and iu.SectionID=" + pm.BagianID.ToString();
            mp.Field = "Cat";
            arrData = mp.Retrieve();
            lstSOP.DataSource = arrData;
            lstSOP.DataBind();
            Label txtTotal = (Label)e.Item.FindControl("txtTotal");
            txtTotal.Text = mp.GetTotalPes(pm.ISOUserID.ToString(), pm.BagianID.ToString(), pm2.ID.ToString()).ToString() + "%";
        }
        protected void lstPES_DataBound(object sender, RepeaterItemEventArgs e)
        {
            PESM pm = (PESM)e.Item.DataItem;
            Repeater scr = (Repeater)e.Item.FindControl("lstScr");
            MasterPES mp = new MasterPES();
            mp.Criteria = " and CategoryID=" + pm.ID;
            mp.Field = "Score";
            scr.DataSource = mp.Retrieve();
            scr.DataBind();
        }
        private void LoadDept()
        {
            UsersFacade usersFacade = new UsersFacade();
            Users users2 = usersFacade.RetrieveByUserName(((Users)Session["Users"]).UserName);
            MasterPES rps = new MasterPES();
            ArrayList arrDept = new ArrayList();
            ddlDept.Items.Clear();
            ddlDept.Items.Add(new ListItem("-- All Dept --", "0"));
            arrDept = rps.RetrieveDept();
            string dpName = string.Empty;

            foreach (PESM dept in arrDept)
            {
                switch (users2.UnitKerjaID)
                {
                    case 1:
                    case 7:
                        dpName = (dept.DeptID == 4 || dept.DeptID == 5 || dept.DeptID == 18 || dept.DeptID == 19) ? "MAINTENANCE" : dept.DeptName;
                        if (((Users)Session["Users"]).Apv > 0 && dept.DeptID != 4 && dept.DeptID != 5 && dept.DeptID != 18 && dept.DeptID != 1)
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
                        ddlDept.Items.Add(new ListItem(dept.DeptName, dept.DeptID.ToString()));
                        break;
                }

            }

            ddlDept.SelectedValue = ((Users)Session["Users"]).DeptID.ToString();
        }
        private void LoadPIC()
        {
            ArrayList arrPIC = new ArrayList();
            MasterPES p = new MasterPES();
            p.Criteria = " and ua.DeptID=" + ddlDept.SelectedValue.ToString();
            p.Field = "PICSop";
            arrPIC = p.Retrieve();
            ddlPIC.Items.Clear();
            ddlPIC.Items.Add(new ListItem("--All PIC--", "0"));
            foreach (PESM ps in arrPIC)
            {
                ddlPIC.Items.Add(new ListItem(ps.PIC, ps.ISOUserID.ToString()));
            }
            //ddlPIC.SelectedValue = ((Users)Session["Users"]).ID.ToString();
        }
    }

    public class MasterPES
    {
        private ArrayList arrData = new ArrayList();
        private PESM pm = new PESM();
        public string Criteria { get; set; }
        public string Field { get; set; }
        public string Where { get; set; }
        private string Query()
        {
            string query = string.Empty;
            switch (this.Field)
            {
                case "PICSop":
                    query = "select ua.UserID,ua.UserName,ua.NIK,ib.BagianName,ua.DeptID,ua.BagianID from UserAccount as ua " +
                            "left join ISO_Bagian as ib on ib.ID=ua.BagianID " +
                            "where ua.RowStatus>-1 " + this.Criteria +
                             "order by ua.DeptID,ib.Urutan,ib.BagianName,ua.UserName ";
                    break;
                case "PESType":
                    query = "Select * from ISO_PES where id in(1,3) order by ID ";
                    break;
                case "Cat":
                    query = "select iu.UserID,ic.ID,ic.Description,ic.DeptID,ic.Target,ic.Checking,CAST(ic.KodeUrutan as int)KodeUrutan, " +
                          "iu.Bobot,iu.SectionID,iu.TypeBobot " +
                          "from ISO_Category as ic " +
                          "LEFT JOIN ISO_UserCategory as iu " +
                          "on iu.CategoryID=ic.ID and iu.PesType=ic.PesType " +
                          "where ic.Description != '' and iu.RowStatus>-1 and iu.UserID is not null " + this.Criteria +
                          " order by iu.UserID, KodeUrutan ";
                    break;
                case "Score":
                    query = "select * from ISO_SOPScore where RowStatus>-1" + this.Criteria + " order by PointNilai desc";
                    break;
            }
            return query;
        }
        private PESM GenerateObject(SqlDataReader sdr)
        {
            pm = new PESM();
            switch (this.Field)
            {
                case "PICSop":
                    pm.PIC = sdr["UserName"].ToString();
                    pm.ISOUserID = Convert.ToInt32(sdr["UserID"].ToString());
                    pm.BagianName = sdr["BagianName"].ToString();
                    pm.NIK = sdr["NIK"].ToString();
                    pm.DeptID = Convert.ToInt32(sdr["DeptID"].ToString());
                    pm.BagianID = Convert.ToInt32(sdr["BagianID"].ToString());
                    break;
                case "PESType":
                    pm.ID = Convert.ToInt32(sdr["ID"].ToString());
                    pm.PESName = sdr["PESName"].ToString();
                    break;
                case "Cat":
                    pm.ID = Convert.ToInt32(sdr["ID"].ToString());
                    pm.UserID = Convert.ToInt32(sdr["UserID"].ToString());
                    pm.Desk = sdr["Description"].ToString();
                    pm.BobotNilai = Convert.ToDecimal(sdr["Bobot"].ToString()) * 100;
                    pm.DeptID = Convert.ToInt32(sdr["DeptID"].ToString());
                    pm.TypeBobot = sdr["TypeBobot"].ToString();
                    pm.Target = sdr["Target"].ToString();
                    pm.Checking = sdr["Checking"].ToString();
                    break;
                case "Score":
                    pm.Pencapaian = sdr["TargetKe"].ToString();
                    pm.Score = Convert.ToDecimal(sdr["PointNilai"].ToString());
                    break;
            }
            return pm;
        }
        public int GetTotalPes(string userid, string bagianID, string pestype)
        {
            int result = 0;
            string strSQL = "select cast(isnull(sum(bobot * 100),0) as int) total from iso_usercategory where RowStatus>-1 and " +
                "userid =" + userid + " and pestype=" + pestype + " and sectionID=" + bagianID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToInt32(sdr["total"].ToString());
                }
            }
            return result;
        }
        public ArrayList Retrieve()
        {
            arrData = new ArrayList();
            string strSQL = this.Query();
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateObject(sdr));
                }
            }
            return arrData;
        }
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
                    arrData.Add(new PESM
                    {
                        DeptID = Convert.ToInt32(sdr["ID"].ToString()),
                        DeptName = sdr["DeptName"].ToString()
                    });
                }
            }
            return arrData;
        }
    }

    public class PESM
    {
        public int ID { get; set; }
        public string Desk { get; set; }
        public string TypeBobot { get; set; }
        public int TypePes { get; set; }
        public int ISOUserID { get; set; }
        public string Target { get; set; }
        public string SOPNo { get; set; }
        public string SOPName { get; set; }
        public decimal BobotNilai { get; set; }
        public string PIC { get; set; }
        public string Tahun { get; set; }
        public decimal Score { get; set; }
        public decimal TotalBobot { get; set; }
        public decimal TotalNilai { get; set; }
        public string Pencapaian { get; set; }
        public decimal Nilai { get; set; }
        public int DeptID { get; set; }
        public int BagianID { get; set; }
        public string DeptName { get; set; }
        public string BagianName { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string PESName { get; set; }
        public string Checking { get; set; }
        public string NIK { get; set; }
    }
}
