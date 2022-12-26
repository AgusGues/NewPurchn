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
using BusinessFacade;
using DataAccessLayer;

namespace GRCweb1.Modul.ISO
{
    public partial class MasterPESScore : System.Web.UI.Page
    {
        private ArrayList arrData = new ArrayList();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["Scoree"] = null;
                Session["SaveScored"] = null;
                Global.link = "~/Default.aspx";
                LoadDept();
            }
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            Session["Scoree"] = null;
            arrData = new ArrayList();
            lstScore.DataSource = arrData;
            lstScore.DataBind();
        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["Scoree"] != null)
                {
                    ArrayList arrData = (ArrayList)Session["Scoree"];
                    Scored ed = new Scored();
                    foreach (Score ct in arrData)
                    {
                        Score obj = new Score();
                        obj.CategoryID = ct.CategoryID;
                        obj.PesType = ct.PesType;
                        obj.Target = ct.Target;
                        obj.Nilai = ct.Nilai;
                        ed.Criteria = "CategoryID,PesType,Target,Nilai";
                        if (ct.CategoryID != 0)
                        {
                            int result = ed.ProcessData(obj, "spISO_SOPScore_insert");
                        }
                    }
                    txtMsg.Text = "Simpan data success...";
                    txtMsg.Attributes.Add("color", "red");
                    Session["Scoree"] = null;
                    arrData = new ArrayList();
                    lstScore.DataSource = arrData;
                    lstScore.DataBind();
                    ddlCategori.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                txtMsg.Text = ex.Message;
                txtMsg.Attributes.Add("color", "red");
            }
        }
        protected void ddlPesType_Click(object sender, EventArgs e)
        {
            LoadCategory();
        }
        protected void btnList_Click(object sender, EventArgs e)
        {

        }
        protected void ddlDept_Change(object sender, EventArgs e)
        {
            LoadSection();

        }
        protected void ddlSection_Change(object sender, EventArgs e)
        {
            LoadCategory();
        }
        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            arrData = (Session["Scoree"] == null) ? new ArrayList() : (ArrayList)Session["Scoree"];
            ArrayList arrSaved = (Session["SaveScored"] == null) ? new ArrayList() : (ArrayList)Session["SaveScored"];
            if (arrSaved.Count > arrData.Count)
            {
                foreach (Score ss in arrSaved)
                {
                    arrData.Add(ss);
                }
            }
            Score obj = new Score();
            obj.CategoryDescription = ddlCategori.SelectedItem.ToString();
            obj.CategoryID = int.Parse(ddlCategori.SelectedValue.ToString());
            obj.SectionID = int.Parse(ddlSection.SelectedValue.ToString());
            obj.SectionName = ddlSection.SelectedItem.ToString();
            obj.Target = txtTarget.Text;
            obj.Nilai = int.Parse(txtScore.Text);
            obj.PESName = ddlPesType.SelectedItem.ToString();
            obj.PesType = int.Parse(ddlPesType.SelectedValue.ToString());
            arrData.Add(obj);
            Session["Scoree"] = arrData;
            lstScore.DataSource = arrData;
            lstScore.DataBind();
            txtTarget.Text = string.Empty;
            txtScore.Text = string.Empty;
            txtTarget.Focus();
        }
        protected void ddlCategori_Change(object sender, EventArgs e)
        {
            Session["SaveScored"] = null;
            Scored scr = new Scored();
            scr.Criteria = " and sp.CategoryID=" + ddlCategori.SelectedValue.ToString();
            //ArrayList arrData =(Session["Scoree"]!=null)?(ArrayList)Session["Scoree"]:new ArrayList();
            ArrayList arrData = scr.Retrieve();
            Session["SaveScored"] = arrData;
            lstScore.DataSource = arrData;
            lstScore.DataBind();
        }
        protected void lstScore_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Image img = (Image)e.Item.FindControl("btnDel");
            Score scr = (Score)e.Item.DataItem;
            img.Visible = (scr.CategoryID == 0) ? false : true;
            ((Image)e.Item.FindControl("btnExt")).Visible = (scr.ID > 0) ? true : false;
        }
        protected void lstScore_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            ArrayList arrData = (ArrayList)Session["Scoree"];
            HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("br");
            switch (e.CommandName.ToString())
            {
                case "del":
                    arrData.RemoveAt(int.Parse(e.CommandArgument.ToString()));
                    lstScore.DataSource = arrData;
                    lstScore.DataBind();
                    break;
                case "nonaktif":
                    Scored sd = new Scored();
                    int rst = sd.NonAktifOldScore(int.Parse(e.CommandArgument.ToString()));
                    tr.Visible = false;
                    break;
            }
        }
        private void LoadDept()
        {
            ArrayList arrData = new ArrayList();
            arrData = new DeptFacade().Retrieve();
            ddlDept.Items.Clear();
            ddlDept.Items.Add(new ListItem("--Pilih--", "0"));
            foreach (Dept dept in arrData)
            {
                ddlDept.Items.Add(new ListItem(dept.DeptName, dept.ID.ToString()));
            }
            // ddlDept.SelectedValue = ((Users)Session["Users"]).DeptID.ToString();
        }
        private void LoadSection()
        {
            ISO_BagianFacade sc = new ISO_BagianFacade();
            //sc.Criteria = " and DeptID=" + ddlDept.SelectedValue;
            ArrayList arrD = sc.RetrieveByDept(int.Parse(ddlDept.SelectedValue.ToString()));
            ddlSection.Items.Clear();
            ddlSection.Items.Add(new ListItem("--Pilih--", "0"));
            foreach (ISO_Bagian ss in arrD)
            {
                ddlSection.Items.Add(new ListItem(ss.BagianName, ss.ID.ToString()));
            }
        }
        private void LoadCategory()
        {
            Scored isc = new Scored();
            isc.Criteria = " and uc.SectionID=" + ddlSection.SelectedValue + " and uc.PesType=" + ddlPesType.SelectedValue;
            ArrayList arrD = isc.CategoryList();
            ddlCategori.Items.Clear();
            ddlCategori.Items.Add(new ListItem("--Pilih Kategori", "0"));
            foreach (Category ic in arrD)
            {
                ddlCategori.Items.Add(new ListItem(ic.CategoryDescription, ic.CategoryID.ToString()));
            }
        }
    }
}

public class Scored
{
    private ArrayList arrData = new ArrayList();
    public string Criteria { get; set; }
    private List<SqlParameter> sqlListParam;
    private Score alk = new Score();

    public string Query()
    {
        string query = "Select sp.*,ct.Description,ct.Category from ISO_SOPScore as sp,ISO_Category as ct " +
                       "where ct.PesType=sp.PesType and ct.ID=sp.CategoryID " +
                       " and ct.RowStatus>-1 and sp.RowStatus>-1 " + this.Criteria;
        return query;
    }
    public ArrayList Retrieve()
    {
        arrData = new ArrayList();
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(this.Query());
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrData.Add(new Score
                {
                    ID = Convert.ToInt32(sdr["ID"].ToString()),
                    CategoryDescription = sdr["Description"].ToString(),
                    PESName = sdr["Category"].ToString(),
                    PesType = Convert.ToInt32(sdr["PesType"].ToString()),
                    Target = sdr["TargetKe"].ToString(),
                    Nilai = Convert.ToInt32(sdr["PointNilai"].ToString())
                });
            }
        }
        return arrData;
    }
    public int ProcessData(object arrAL, string spName)
    {
        try
        {
            alk = (Score)arrAL;
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
    public ArrayList CategoryList()
    {
        arrData = new ArrayList();
        string Query = "select distinct uc.CategoryID,ic.Description,CAST(ic.KodeUrutan as int)Urutan from " +
                     "ISO_UserCategory as uc   left join ISO_Category as ic   " +
                     "on ic.ID=uc.CategoryID where ic.RowStatus >-1 and uc.RowStatus >-1 " +
                     this.Criteria + " order by Urutan,CategoryID";
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(Query);
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrData.Add(new Category
                {
                    CategoryDescription = sdr["Description"].ToString(),
                    CategoryID = Convert.ToInt32(sdr["CategoryID"].ToString())
                });
            }
        }
        return arrData;
    }
    public int NonAktifOldScore(int ID)
    {
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString("Update ISO_SOPScore set CategoryID=(-1 * CategoryID),RowStatus=2 where ID=" + ID);
        return sdr.RecordsAffected;
    }
}
public class Score : Category
{
    public string PESName { get; set; }
    public int PesType { get; set; }
    public string SectionName { get; set; }
    public int Nilai { get; set; }
    public int CategoryID { get; set; }
}