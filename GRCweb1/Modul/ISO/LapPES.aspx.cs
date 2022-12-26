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
    public partial class LapPES : System.Web.UI.Page
    {
        public string Bulan = Global.nBulan(DateTime.Now.Month);
        public string Judul = string.Empty;
        public decimal GrandTotal = 0;
        public decimal TotalBobot = 0;
        private PES1 pes = new PES1();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Judul = Request.QueryString["p"].ToString();
                LoadDept();
                ddlDept.SelectedValue = ((Users)Session["Users"]).DeptID.ToString();
                LoadPICNew();
                LoadTahun();
                LoadBulan();
                Bulan = ddlBulan.SelectedItem.Text;
                btnTest.Attributes.Add("onclick", "fixedHd()");
            }
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('thr', 450, 100 , 70 ,false); </script>", false);
        }
        protected void ddlDept_SelectedChange(object sender, EventArgs e)
        {
            LoadPICNew();
            btnPreview_Click(null, null);
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            Bulan = ddlBulan.SelectedItem.Text;
            ListPIC();
            //ScriptManager.RegisterStartupScript(this, this.GetType, "alert", "fixedHd()", true);
        }
        protected void ddlBulan_Change(object sender, EventArgs e)
        {
            Bulan = ddlBulan.SelectedItem.Text;
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=RekapSOPKPI.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "Departement :" + ddlDept.SelectedItem.Text;
            Html += "<br>Periode : " + ddlBulan.SelectedItem.Text + " " + ddlTahun.SelectedValue.ToString();
            Html += "<br>PIC     : " + ddlPIC.SelectedItem.Text;
            Bulan = ddlBulan.SelectedItem.Text;
            string HtmlEnd = "";
            //lstForPrint.RenderControl(hw);
            lst.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0\"", "border=\"1\"");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }
        protected void lstPES1_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lbl = (Label)e.Item.FindControl("sc");
            PES1 p = (PES1)e.Item.DataItem;
            lbl.Text = (p.Pencapaian == "N/A") ? "N/A" : p.Score.ToString("##0");

        }
        private void LoadDept()
        {
            UsersFacade usersFacade = new UsersFacade();
            Users users2 = usersFacade.RetrieveByUserName(((Users)Session["Users"]).UserName);

            DeptFacade deptFacade = new DeptFacade();
            deptFacade.Criteria = " order by alias";
            ArrayList arrDept = deptFacade.RetrieveAliasDept();
            Auth DeptAuth = new Auth();
            UserAuth oto = new UserAuth();
            oto.Option = "All";
            oto.Criteria = " and UserID=" + ((Users)Session["Users"]).ID.ToString();
            oto.Criteria += " and ModulName='Rekap " + Request.QueryString["p"] + "' ";
            DeptAuth = oto.Retrieve(true);
            string UserDept = ((Users)Session["Users"]).DeptID.ToString() + ",0";
            string[] arrDepts = (DeptAuth.AuthDept != null) ? DeptAuth.AuthDept.Split(',') : UserDept.ToString().Split(',');
            ddlDept.Items.Clear();
            if (deptFacade.Error == string.Empty)
            {
                ddlDept.Items.Add(new ListItem("-- Choose Dept --", "0"));
                for (int i = 0; i < arrDepts.Count(); i++)
                {
                    foreach (Dept dept in arrDept)
                    {
                        if (int.Parse(arrDepts[i].ToString()) == dept.ID)
                        {
                            ddlDept.Items.Add(new ListItem(dept.AlisName, dept.ID.ToString()));
                        }
                    }
                }
            }
            //sorting dropdwonlist
            List<ListItem> lte = new List<ListItem>();
            foreach (ListItem lt in ddlDept.Items)
            {
                lte.Add(lt);
            }
            List<ListItem> sorted = lte.OrderBy(b => b.Text).ToList();
            ddlDept.Items.Clear();
            foreach (ListItem l in sorted)
            {
                ddlDept.Items.Add(l);
            }

        }
        private void LoadDept2()
        {
            Users users = (Users)Session["Users"];

            DeptFacade deptFacade = new DeptFacade();
            UsersFacade usersFacade = new UsersFacade();
            ArrayList arrDept = new ArrayList();

            if (users.UserLevel >= 1)
            {
                deptFacade = new DeptFacade();
                arrDept = deptFacade.RetrieveByAll();

            }
            else
            {
                usersFacade = new UsersFacade();
                Users users2 = usersFacade.RetrieveByUserName(users.UserName);

                deptFacade = new DeptFacade();
                arrDept = deptFacade.RetrieveByUserID(users2.ID);
            }


            ddlDept.Items.Clear();
            ddlDept.Items.Add(new ListItem("-- Choose Dept --", "0"));
            if (deptFacade.Error == string.Empty)
            {

                foreach (Dept dept in arrDept)
                {
                    ddlDept.Items.Add(new ListItem(dept.DeptName, dept.DeptID.ToString()));
                }
            }
        }
        private void LoadPICNew()
        {
            ArrayList arrUser = new ArrayList();
            UsersFacade usFac = new UsersFacade();
            usFac.Criteria = " and DeptID=" + ddlDept.SelectedValue.ToString();
            usFac.Criteria += " order by UserName";
            arrUser = usFac.RetriveUserAccount();
            ddlPIC.Items.Clear();
            ddlPIC.Items.Add(new ListItem("--Pilih PIC--", "0"));
            foreach (Users user in arrUser)
            {
                ListItem lst = new ListItem(user.UserName, user.ID.ToString());
                lst.Attributes.Add("style", "tooltip:" + user.BagianID.ToString());
                ddlPIC.Items.Add(lst);
            }
            //ddlPIC.SelectedValue = ((Users)Session["Users"]).ID.ToString();
        }
        private void LoadPIC()
        {
            Users users = (Users)Session["Users"];
            ArrayList arrPIC = new ArrayList();
            PesFacade1 p = new PesFacade1();
            p.Criteria = " and DeptID=" + ddlDept.SelectedValue.ToString();
            p.Criteria += (ddlPIC.SelectedIndex > 0) ? " and userid='" + ddlPIC.SelectedValue + "'" : "";
            p.Criteria += "or UserID in(select ISOuserID from ISO_BagianHead where DeptApp  like '%" + ddlDept.SelectedValue + "%')";
            p.Field = "PICSop";
            p.Where = "PICSop";
            arrPIC = p.Retrieve();
            ddlPIC.Items.Clear();
            ddlPIC.Items.Add(new ListItem("--All PIC--", "0"));
            foreach (PES1 ps in arrPIC)
            {
                ddlPIC.Items.Add(new ListItem(ps.PIC, ps.ISOUserID.ToString()));
            }
            //ddlPIC.SelectedValue = ((Users)Session["Users"]).ID.ToString();
        }
        private void ListPIC()
        {
            ArrayList arrPIC = new ArrayList();
            PesFacade1 p = new PesFacade1();
            Auth DeptAuth = new Auth();
            UserAuth oto = new UserAuth();
            oto.Option = "All";
            oto.Criteria = " and UserID=" + ((Users)Session["Users"]).ID.ToString();
            oto.Criteria += " and ModulName='Rekap " + Judul + "' ";
            DeptAuth = oto.Retrieve(true);
            string UserDept = ((Users)Session["Users"]).DeptID.ToString();
            string arrDepts = (DeptAuth.AuthDept != null) ? DeptAuth.AuthDept : UserDept.ToString();
            p.Criteria = (ddlDept.SelectedIndex > 0) ? " and DeptID=" + ddlDept.SelectedValue.ToString() : " and DeptID in(" + arrDepts + ")";
            p.Criteria += (ddlPIC.SelectedIndex > 0) ? " and userid='" + ddlPIC.SelectedValue + "'" : "";
            //p.Criteria += (ddlPIC.SelectedIndex > 0) ? "" : "or UserID in(select ISOuserID from ISO_BagianHead where DeptApp  like '%" + ddlDept.SelectedValue + "%')";
            p.Field = "PICSop";
            p.Where = "PICSop";
            arrPIC = p.Retrieve();
            lstH.DataSource = arrPIC;
            lstH.DataBind();
        }
        private void LoadTahun()
        {
            //ddlTahun.Items.Clear();
            //ddlTahun.Items.Add(new ListItem((DateTime.Now.Year - 1).ToString(), (DateTime.Now.Year - 1).ToString()));
            //ddlTahun.Items.Add(new ListItem(DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString()));
            PesFacade1 p = new PesFacade1();
            p.GetTahun(ddlTahun);
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();

        }
        private void LoadBulan()
        {
            ddlBulan.Items.Clear();
            ddlBulan.Items.Add(new ListItem("--All--", "0"));
            for (int i = 1; i <= 12; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i).ToString(), i.ToString()));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
        }
        private void LoadData()
        {

            ArrayList arrData = new ArrayList();
            PesFacade1 pes = new PesFacade1();
            pes.Criteria = (ddlPIC.SelectedIndex == 0) ? "" : " and PIC='" + ddlPIC.SelectedItem.ToString() + "'";
            pes.Criteria += " and DeptID=" + ddlDept.SelectedValue.ToString();
            pes.Criteria += " and Year(tglMulai)=" + ddlTahun.SelectedValue;
            pes.Where = "NewRpt";
            pes.Field = Request.QueryString["p"].ToString();
            arrData = pes.Retrieve();

            //lstPES1.DataSource = arrData;
            //lstPES1.DataBind();
            lstH.DataSource = arrData;
            lstH.DataBind();
        }
        protected void lstH_DataBound(object sender, RepeaterItemEventArgs e)
        {
            PES1 pes = new PES1();
            ArrayList arrData = new ArrayList();
            ArrayList arrTot = new ArrayList();
            Repeater lst = (Repeater)e.Item.FindControl("lstRkp");
            Repeater upLst = (Repeater)e.Item.FindControl("lstTot");
            PES1 p = (PES1)e.Item.DataItem;
            PesFacade1 ps = new PesFacade1();
            //ps.Criteria = " where xx.UserID=" + p.ISOUserID.ToString();
            ps.UserID = p.ISOUserID.ToString();
            ps.Bulan = ddlBulan.SelectedValue.ToString();
            ps.Tahun = ddlTahun.SelectedValue.ToString();
            ps.Bagian = p.BagianID.ToString();
            ps.Where = "ItemSOP";
            if (Request.QueryString["p"].ToString() == "SOP")
            {
                ps.Field = (ps.isHasbeenInput("ISO_SOP") > 0) ? "ItemSOPNew" : "ItemSOP";
            }
            else
            {
                ps.Field = (ps.isHasbeenInput("ISO_KPI") > 0) ? "ItemKPINew" : "ItemKPI";
            }
            arrData = ps.Retrieve();
            foreach (PES1 pp in arrData)
            {
                pes.TotalBobot += pp.BobotNilai;
                pes.TotalNilai += pp.Nilai;
            }
            arrTot.Add(pes);
            upLst.DataSource = arrTot;
            upLst.DataBind();
            lst.DataSource = arrData;
            lst.DataBind();


        }
        private void sumDataTable(DataTable dt, Dictionary<string, decimal> totals)
        {
            List<string> keyList = new List<string>(totals.Keys);
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                foreach (string key in keyList)
                {
                    totals[key] += Convert.ToDecimal(dt.Rows[i][key]);
                }
            }
        }
    }
}

public class PES1 : GRCBaseDomain
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
    public int Approval { get; set; }
    public decimal Jan { get; set; }
    public decimal JanS { get; set; }
    public decimal Feb { get; set; }
    public decimal FebS { get; set; }
    public decimal Mar { get; set; }
    public decimal MarS { get; set; }
    public decimal Apr { get; set; }
    public decimal AprS { get; set; }
    public decimal Mei { get; set; }
    public decimal MeiS { get; set; }
    public decimal Jun { get; set; }
    public decimal JunS { get; set; }
    public decimal Jul { get; set; }
    public decimal JulS { get; set; }
    public decimal Ags { get; set; }
    public decimal AgsS { get; set; }
    public decimal Sep { get; set; }
    public decimal SepS { get; set; }
    public decimal Okt { get; set; }
    public decimal OktS { get; set; }
    public decimal Nop { get; set; }
    public decimal NopS { get; set; }
    public decimal Des { get; set; }
    public decimal DesS { get; set; }
    public decimal Score { get; set; }
    public decimal TotalBobot { get; set; }
    public decimal TotalNilai { get; set; }
    public string Pencapaian { get; set; }
    public decimal Nilai { get; set; }
    public int DeptID { get; set; }
    public int BagianID { get; set; }
    public string DeptName { get; set; }
    public string BagianName { get; set; }
    public string UserID { get; set; }
    public string UserName { get; set; }
}
public class PesFacade1
{
    private ArrayList arrData = new ArrayList();
    public string UserID { get; set; }
    public string Bulan { get; set; }
    public string Tahun { get; set; }
    public string Field { get; set; }
    public string Where { get; set; }
    public string Bagian { get; set; }
    public string Criteria { get; set; }
    public string Query()
    {
        string query = ""; string query1 = "";
        string Periode = (this.Tahun != null && this.Bulan != null) ? this.Tahun + this.Bulan.PadLeft(2, '0') + DateTime.DaysInMonth(int.Parse(this.Tahun), int.Parse(this.Bulan)) : string.Empty;
        switch (this.Field)
        {
            case "SOP":
                #region query SOP
                query = "/*select Tahun,SopName,NilaiBobot,PIC,Approval, " +
                           "SUM(Jan)Jan,SUM(janS)JanS,(SUM(jan)*SUM(janS)/100)JanN, " +
                           " SUM(Feb)Feb,SUM(febS)FebS,(SUM(feb)*SUM(febS)/100)FebN, " +
                           " SUM(Mar)Mar,SUM(marS)MarS,(SUM(Mar)*SUM(marS)/100)MarN, " +
                           " SUM(Apr)Apr,SUM(aprS)AprS,(SUM(Apr)*SUM(aprS)/100)AprN from ( */" +
                           "select Tahun,SOPNo,SOPName,(NilaiBobot*100)NilaiBobot,CategoryID,PIC,Approval, " +
                           " ISNULL([1],0)Jan,ISNULL([2],0)Feb,ISNULL([3],0)Mar,ISNULL([4],0)Apr,ISNULL([5],0)Mei, " +
                           " ISNULL([6],0)Jun,ISNULL([7],0)Jul,ISNULL([8],0)Ags,ISNULL([9],0)Sep,ISNULL([10],0)Okt, " +
                           " ISNULL([11],0)Nop,ISNULL([12],0)Desm, " +
                           " (NilaiBobot*ISNULL([1],0)) janS,(NilaiBobot*ISNULL([2],0)) febS,(NilaiBobot*ISNULL([3],0)) marS, " +
                           " (NilaiBobot*ISNULL([4],0)) aprS,(NilaiBobot*ISNULL([5],0)) meiS,(NilaiBobot*ISNULL([6],0)) junS, " +
                           " (NilaiBobot*ISNULL([7],0)) julS,(NilaiBobot*ISNULL([8],0)) agsS,(NilaiBobot*ISNULL([9],0)) sepS, " +
                           " (NilaiBobot*ISNULL([10],0)) oktS,(NilaiBobot*ISNULL([11],0)) nopS,(NilaiBobot*ISNULL([12],0)) desS  " +
                           " from ( " +
                           " select sp.ID,SOPNo,SOPName,NilaiBobot,CategoryID,PIC,sd.Approval, CAST(MONTH(TglMulai) as varchar) as Bulan," +
                           " YEAR(tglMulai)Tahun,sd.PointNilai " +
                           " from ISO_SOP as sp " +
                           " left Join ISO_SOPDetail as sd " +
                           " on sd.SOPID=sp.ID " +
                           " where sp.RowStatus>-1 and sd.RowStatus>-1 " + this.Criteria +
                           " Group by Month(tglMulai),YEAR(tglMulai),sp.ID,SOPNo,SOPName,NilaiBobot,CategoryID,PIC,sd.Approval,sd.PointNilai) " +
                           " up pivot (SUM(PointNilai) for Bulan in([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12])) as pv1 " +
                           " order by RIGHT(SOPNo,4),Tahun";
                //") as x " +
                //" group by x.Tahun,x.SOPName,x.NilaiBobot,x.PIC,x.Approval " +
                //" order by Tahun ";
                #endregion
                break;

            case "KPI":
                #region query KPI
                query = "/*select Tahun,SopName,NilaiBobot,PIC,Approval, " +
                           "SUM(Jan)Jan,SUM(janS)JanS,(SUM(jan)*SUM(janS)/100)JanN, " +
                           " SUM(Feb)Feb,SUM(febS)FebS,(SUM(feb)*SUM(febS)/100)FebN, " +
                           " SUM(Mar)Mar,SUM(marS)MarS,(SUM(Mar)*SUM(marS)/100)MarN, " +
                           " SUM(Apr)Apr,SUM(aprS)AprS,(SUM(Apr)*SUM(aprS)/100)AprN from ( */" +
                           "select Tahun,KPINo as SOPNo,KPIName as SOPName,(NilaiBobot*100)NilaiBobot,CategoryID,PIC,Approval, " +
                           " ISNULL([1],0)Jan,ISNULL([2],0)Feb,ISNULL([3],0)Mar,ISNULL([4],0)Apr,ISNULL([5],0)Mei, " +
                           " ISNULL([6],0)Jun,ISNULL([7],0)Jul,ISNULL([8],0)Ags,ISNULL([9],0)Sep,ISNULL([10],0)Okt, " +
                           " ISNULL([11],0)Nop,ISNULL([12],0)Desm, " +
                           " (NilaiBobot*ISNULL([1],0)) janS,(NilaiBobot*ISNULL([2],0)) febS,(NilaiBobot*ISNULL([3],0)) marS, " +
                           " (NilaiBobot*ISNULL([4],0)) aprS,(NilaiBobot*ISNULL([5],0)) meiS,(NilaiBobot*ISNULL([6],0)) junS, " +
                           " (NilaiBobot*ISNULL([7],0)) julS,(NilaiBobot*ISNULL([8],0)) agsS,(NilaiBobot*ISNULL([9],0)) sepS, " +
                           " (NilaiBobot*ISNULL([10],0)) oktS,(NilaiBobot*ISNULL([11],0)) nopS,(NilaiBobot*ISNULL([12],0)) desS  " +
                           " from ( " +
                           " select KPINo,KPIName,NilaiBobot,CategoryID,PIC,sd.Approval, CAST(MONTH(TglMulai) as varchar) as Bulan,YEAR(tglMulai)Tahun,sd.PointNilai " +
                           " from ISO_KPI as sp " +
                           " left Join ISO_KPIDetail as sd " +
                           " on sd.KPIID=sp.ID " +
                           " where sp.RowStatus>-1 and sd.RowStatus>-1 " + this.Criteria +
                           " Group by Month(tglMulai),YEAR(tglMulai),KPINo,KPIName,NilaiBobot,CategoryID,PIC,sd.Approval,sd.PointNilai) " +
                           " up pivot (SUM(PointNilai) for Bulan in([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12])) as pv1 " +
                           " order by RIGHT(KPINo,4),Tahun";
                //"/*) as x " +
                //" group by x.Tahun,x.SOPName,x.NilaiBobot,x.PIC,x.Approval " +
                //" order by Tahun*/ ";
                #endregion
                break;
            case "ItemSOP":
                #region old query
                query1 = "select *,case when Approval=2 then xxx.Bobot*PointNilai/100 else 0 end Nilai from ( " +
                        "select iu.ID,iu.UserID,iu.CategoryID,ic.Description,iu.PesType, Case when iu.TypeBobot='%' " +
                        "then iu.Bobot *100 else iu.Bobot end Bobot,iu.TypeBobot,ic.Target,CAST(ic.KodeUrutan as int)KodeUrutan," +
                        "isnull(xx.NilaiBobot,0)NilaiBobot," +
                        "xx.Keterangan,xx.KetTargetKe,isnull(xx.PointNilai,0)PointNilai,isnull(xx.Approval,0)Approval  " +
                        "from ISO_UserCategory iu   LEFT Join ISO_Category as ic on ic.ID=iu.CategoryID  " +
                        "LEFT JOIN " +
                        "( " +
                        "select sp.ID,sp.CategoryID,sp.Keterangan,sp.Status,sp.NilaiBobot,sd.KetTargetKe,sd.PointNilai,sd.Approval " +
                        "from ISO_SOP as sp " +
                        "LEFT JOIN ISO_SOPDetail as sd " +
                        "on sd.SOPID=sp.ID " +
                        "where sp.RowStatus>-1 and sd.RowStatus>-1 and MONTH(sp.TglMulai)=" + this.Bulan + " and YEAR(sp.TglMulai)= " + this.Tahun +
                        "and sp.PIC=(Select Top 1 UserName from ISO_Users where ID=" + this.UserID + " and RowStatus>-1) ) as xx on xx.CategoryID=iu.ID " +
                        "where  iu.RowStatus>-1 and iu.CategoryID >0 and iu.PesType=3 and iu.UserID=" + this.UserID +
                        " ) as xxx " + this.Criteria +
                        "order by xxx.KodeUrutan";
                #endregion
                #region new query
                query = "select *,case when Approval=2 then" +
                      " case when xd.Penilaian=0 AND (xd.KetTargetKe='' OR xd.KetTargetKe is null) then 0 else (xd.Bobot*xd.PointNilai) end  else 0 end Nilai from ( " +
                      " select x.*,PIC,isnull(SopScoreID,0)ScoreID, isnull(xx.PointNilai,0) PointNilai,isnull((select dbo.GetTotalBobotSOP(" + this.UserID + "," + this.Bulan + "," + this.Bagian + ")),0)Bbt," +
                      " xx.Keterangan,isnull(xx.Approval,0)Approval,xx.KetTargetKe,isnull(xx.IDx,0)Inputed from ( " +
                      "  select iu.ID,ic.ID as CatID,iu.UserID,ic.Description,ic.calc,iu.Penilaian,iu.Bobot,Cast(ic.KodeUrutan as int)KodeUrutan,iu.PesType,ic.Target  " +
                      "  from ISO_UserCategory as iu " +
                      "  LEFT JOIN ISO_Category as ic ON ic.ID=iu.CategoryID " +
                      "  /*LEFT JOIN ISO_SOPScore as sp ON sp.CategoryID=iu.CategoryID */ " +
                      "  where UserID=" + this.UserID + " and iu.PesType=3 and iu.RowStatus>-1 and iu.SectionID= " + this.Bagian +
                      "  ) as x " +
                      "  LEFT JOIN  " +
                      "  (select ip.ID,ip.CategoryID ,ip.ISO_UserID,ip.BagianID,sd.SopScoreID,sd.PointNilai,ip.PIC,ip.Keterangan,sd.Approval,sd.KetTargetKe " +
                      "     ,count(ip.ID) over(partition by ip.ID) IDx " +
                      "      FROM ISO_SOP as ip " +
                      "      LEFT JOIN ISO_SOPDetail as sd ON sd.SOPID=ip.ID " +
                      "      Where ip.RowStatus>-1 and sd.RowStatus>-1 and RTRIM(CONVERT(Char,TglMulai,112))='" + Periode + "'  " +
                      "      and PIC=(select Username from ISO_Users where ID=" + this.UserID + ")) " +
                      "      as xx ON xx.CategoryID=x.ID " +
                      "  LEFT JOIN ISO_SOPScore AS sc ON sc.ID=xx.SopScoreID " +
                      "  ) as xd " +
                      "  order by xd.KodeUrutan";
                #endregion
                break;
            case "ItemSOPNew":
                query = "select *,Case When Approval=2 then (PointNilai*NilaiBobot) else 0 end Nilai from (  " +
                        "select sop.ID,SOPName as Description,sop.CategoryID,NilaiBobot,Keterangan,sop.Status as StatusH,  " +
                        "sod.KetTargetKe,ic.ID as CatID,ic.Target,isnull((select dbo.GetTotalBobotSOP(" + this.UserID + "," + this.Bulan + "," + this.Bagian + ")),0)Bbt,   " +
                        " sod.PointNilai,sod.SopScoreID,sod.Status,sod.TargetKe,iuc.UserID,iuc.Bobot,iuc.PesType,sod.Approval   " +
                        " FROM ISO_SOP as sop    " +
                        " LEFT JOIN ISO_SOPDetail as sod ON sod.SOPID=sop.ID   " +
                        " LEFT JOIN ISO_UserCategory as iuc ON iuc.ID=sop.CategoryID  " +
                        " LEFT JOIN ISO_Category as ic on ic.ID=iuc.CategoryID  " +
                        " where sod.RowStatus>-1 and sop.RowStatus>-1 and MONTH(TglMulai)=" + this.Bulan + " AND YEAR(TglMulai)=" + this.Tahun + " and   " +
                        " PIC=(select Username from ISO_Users where ID=" + this.UserID + ") and sop.BagianID=" + this.Bagian +
                        " ) as x " + UnionItemSOP("3", "SOP");

                break;
            case "PICSopOld":
                query = "select Distinct UserName,isnull(UserID,0) UserID,isnull(BagianID,0)BagianID,(select BagianName from ISO_Bagian where ID=BagianID)BagianName " +
                        "from UserAccount where RowStatus>-1 " + this.Criteria + " order by userName";
                break;
            case "PICSop":
                query = "select * from (select Distinct UserName,isnull(UserID,0) UserID,isnull(BagianID,0)BagianID, " +
                      "(select BagianName from ISO_Bagian where ID=BagianID)BagianName,(select Urutan from ISO_Bagian where ID=BagianID)Urutan " +
                      "from UserAccount where RowStatus>-1 " + this.Criteria +
                      ")as x order by Urutan,UserName";
                break;
            case "ItemKPI":
                #region OldQuery
                query1 = "select *,case when Approval=2 then xxx.Bobot*PointNilai/100 else 0 end Nilai from ( " +
                       "select iu.ID,iu.UserID,iu.CategoryID,ic.Description,iu.PesType, Case when iu.TypeBobot='%' " +
                       "then iu.Bobot *100 else iu.Bobot end Bobot,iu.TypeBobot,ic.Target,CAST(ic.KodeUrutan as int)KodeUrutan," +
                       "isnull(xx.NilaiBobot,0)NilaiBobot," +
                       "xx.Keterangan,xx.KetTargetKe,isnull(xx.PointNilai,0)PointNilai,isnull(xx.Approval,0)Approval  " +
                       "from ISO_UserCategory iu   LEFT Join ISO_Category as ic on ic.ID=iu.CategoryID  " +
                       "LEFT JOIN " +
                       "( " +
                       "select sp.ID,sp.CategoryID,sp.Keterangan,sp.Status,sp.NilaiBobot,sd.KetTargetKe,sd.PointNilai,sd.Approval " +
                       "from ISO_KPI as sp " +
                       "LEFT JOIN ISO_KPIDetail as sd " +
                       "on sd.KPIID=sp.ID " +
                       "where sp.RowStatus>-1 and sd.RowStatus>-1 and MONTH(sp.TglMulai)=" + this.Bulan + " and YEAR(sp.TglMulai)= " + this.Tahun +
                       "and sp.PIC=(Select Top 1 UserName from ISO_Users where ID=" + this.UserID + " and RowStatus>-1) ) as xx on xx.CategoryID=iu.ID " +
                       "where  iu.RowStatus>-1 and iu.CategoryID >0 and iu.PesType=1 and iu.UserID=" + this.UserID +
                       " ) as xxx " + this.Criteria +
                       "order by xxx.KodeUrutan";
                #endregion
                #region new query
                query = "select *,case when Approval=2 then " +
                      " case when xd.Penilaian=0 AND (xd.KetTargetKe='' OR xd.KetTargetKe is null) then 0 else (xd.Bobot*xd.PointNilai) end  else 0 end Nilai from ( " +
                      " select x.*,PIC,isnull(SopScoreID,0)ScoreID,isnull(xx.PointNilai,0)PointNilai," +
                      " isnull((select dbo.GetTotalBobotKPI(" + this.UserID + "," + this.Bulan + "," + this.Bagian + ")),0)Bbt," +
                      " xx.Keterangan,isnull(xx.Approval,0)Approval,xx.KetTargetKe,isnull(xx.IDx,0)Inputed from ( " +
                      "  select iu.ID,ic.ID as CatID,iu.UserID,ic.Description,ic.calc,iu.Penilaian,iu.Bobot,Cast(ic.KodeUrutan as int)KodeUrutan,iu.PesType,ic.Target  " +
                      "  from ISO_UserCategory as iu " +
                      "  LEFT JOIN ISO_Category as ic ON ic.ID=iu.CategoryID " +
                      "  /*LEFT JOIN ISO_SOPScore as sp ON sp.CategoryID=iu.CategoryID */ " +
                      "  where UserID=" + this.UserID + " and iu.PesType=1 and iu.RowStatus>-1 and iu.SectionID= " + this.Bagian +
                      "  ) as x " +
                      "  LEFT JOIN  " +
                      "  (select ip.ID,ip.CategoryID ,ip.ISO_UserID,ip.BagianID,sd.SopScoreID,sd.PointNilai,ip.PIC,ip.Keterangan,sd.Approval,sd.KetTargetKe " +
                      "     ,count(ip.ID) over(partition by ip.ID) IDx " +
                      "      FROM ISO_KPI as ip " +
                      "      LEFT JOIN ISO_KPIDetail as sd ON sd.KPIID=ip.ID " +
                      "      Where ip.RowStatus>-1 and sd.RowStatus>-1 and CONVERT(Char,TglMulai,112)='" + Periode + "'  " +
                      "      and PIC=(select Username from ISO_Users where ID=" + this.UserID + ")) " +
                      "      as xx ON xx.CategoryID=x.ID " +
                      "  LEFT JOIN ISO_SOPScore AS sc ON sc.ID=xx.SopScoreID " +
                      "  ) as xd " +
                      "  order by xd.KodeUrutan";
                #endregion
                break;
            case "ItemKPINew":
                query = "select *,Case When Approval=2 then (PointNilai*NilaiBobot) else 0 end Nilai from (  " +
                        "select sop.ID,KPIName as Description,sop.CategoryID,NilaiBobot,Keterangan,sop.Status as StatusH,  " +
                        "sod.KetTargetKe,ic.ID as CatID,ic.Target,isnull((select dbo.GetTotalBobotKPI(" + this.UserID + "," + this.Bulan + "," + this.Bagian + ")),0)Bbt,   " +
                        " sod.PointNilai,sod.SopScoreID,sod.Status,sod.TargetKe,iuc.UserID,iuc.Bobot,iuc.PesType,sod.Approval   " +
                        " FROM ISO_KPI as sop    " +
                        " LEFT JOIN ISO_KPIDetail as sod ON sod.KPIID=sop.ID   " +
                        " LEFT JOIN ISO_UserCategory as iuc ON iuc.ID=sop.CategoryID  " +
                        " LEFT JOIN ISO_Category as ic on ic.ID=iuc.CategoryID  " +
                        " where sod.RowStatus>-1 and sop.RowStatus>-1 and MONTH(TglMulai)=" + this.Bulan + " AND YEAR(TglMulai)=" + this.Tahun + " and   " +
                        " PIC=(select Username from ISO_Users where ID=" + this.UserID + ") and sop.BagianID=" + this.Bagian +
                        " ) as x " + UnionItemSOP("1", "KPI");
                break;
            case "TotalSOP":
                query = "";
                break;
        }
        return query;
    }
    private string UnionItemSOP(string PesType, string PES1)
    {
        int check = CheckInputan(PES1);
        string query = (CheckInputan(PES1) < 201509) ? "union all " +
                       "select iso.ID,ic.Description,iso.CategoryID,iso.Bobot NilaiBobot,''Keterangan,0,'',ic.ID as CatID " +
                       " ,ic.Target,iso.Bobot as Bbt,0 PoinNilai,0 SopScoreID,0 Status,'' TargetKe,iso.UserID,iso.Bobot, " +
                       " iso.PesType,0 Approval,0 Nilai from ISO_UserCategory iso  " +
                       " LEFT JOIN ISO_Category as ic ON ic.ID=iso.CategoryID " +
                       " where iso.ID not in( Select CategoryID  " +
                       " FROM ISO_" + PES1 + " as sop     " +
                       " where sop.RowStatus>-1  " +
                       " and MONTH(TglMulai)=" + this.Bulan + " AND YEAR(TglMulai)=" + this.Tahun + " and    " +
                       " PIC=(select Username from ISO_Users where ID=" + this.UserID + ")  " +
                       " and sop.BagianID=" + this.Bagian + " ) and iso.RowStatus >-2" +
                       " and iso.UserID=" + this.UserID + " and iso.SectionID=" + this.Bagian + " and iso.PesType= " + PesType +
                       " and iso.bulan <=" + this.Bulan + " and iso.Tahun<=" + this.Tahun : "";
        return query;
    }
    private int CheckInputan(string Pes)
    {
        int bulantahun = 0;
        string query = "SELECT (CAST(YEAR(CreatedTime) as CHAR(4))+''+RIGHT('0'+RTRIM(CAST(MONTH(CreatedTime) as CHAR(2))),2)) as YM " +
                        "FROM ISO_" + Pes + " where PIC =(SELECT UserName From ISO_Users Where ID=" + this.UserID + " and BagianID=" + this.Bagian + ")" +
                        " AND MONTH(TglMulai)=" + this.Bulan + " AND YEAR(TglMulai)=" + this.Tahun;
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(query);
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                bulantahun = (sdr["YM"] != DBNull.Value || sdr["YM"].ToString() != "") ? Convert.ToInt32(sdr["YM"].ToString()) : 0;
            }
        }
        return bulantahun;
    }
    public ArrayList Retrieve()
    {
        arrData = new ArrayList();
        string strsql = this.Query();
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(this.Query());
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                int app = 0;

                switch (this.Where)
                {
                    case "Detail":
                        #region Detail
                        app = Convert.ToInt32(sdr["Approval"].ToString());
                        arrData.Add(new PES1
                        {
                            SOPName = sdr["SOPName"].ToString(),
                            SOPNo = sdr["SOPNo"].ToString(),
                            BobotNilai = Convert.ToDecimal(sdr["NilaiBobot"].ToString()),
                            PIC = sdr["PIC"].ToString(),
                            Approval = Convert.ToInt32(sdr["Approval"].ToString()),
                            Jan = Convert.ToDecimal(sdr["Jan"].ToString()),
                            JanS = (app < 2) ? 0 : Convert.ToDecimal(sdr["janS"].ToString()),
                            Feb = Convert.ToDecimal(sdr["Feb"].ToString()),
                            FebS = (app < 2) ? 0 : Convert.ToDecimal(sdr["FebS"].ToString()),
                            Mar = Convert.ToDecimal(sdr["Mar"].ToString()),
                            MarS = (app < 2) ? 0 : Convert.ToDecimal(sdr["MarS"].ToString()),
                            Apr = Convert.ToDecimal(sdr["Apr"].ToString()),
                            AprS = (app < 2) ? 0 : Convert.ToDecimal(sdr["AprS"].ToString()),
                            Mei = Convert.ToDecimal(sdr["Mei"].ToString()),
                            MeiS = (app < 2) ? 0 : Convert.ToDecimal(sdr["MeiS"].ToString()),
                            Jun = Convert.ToDecimal(sdr["Jun"].ToString()),
                            JunS = (app < 2) ? 0 : Convert.ToDecimal(sdr["JunS"].ToString()),
                            Jul = Convert.ToDecimal(sdr["Jul"].ToString()),
                            JulS = (app < 2) ? 0 : Convert.ToDecimal(sdr["JulS"].ToString()),
                            Ags = Convert.ToDecimal(sdr["Ags"].ToString()),
                            AgsS = (app < 2) ? 0 : Convert.ToDecimal(sdr["AgsS"].ToString()),
                            Okt = Convert.ToDecimal(sdr["Okt"].ToString()),
                            OktS = (app < 2) ? 0 : Convert.ToDecimal(sdr["OktS"].ToString()),
                            Nop = Convert.ToDecimal(sdr["Nop"].ToString()),
                            NopS = (app < 2) ? 0 : Convert.ToDecimal(sdr["NopS"].ToString()),
                            Des = Convert.ToDecimal(sdr["Desm"].ToString()),
                            DesS = (app < 2) ? 0 : Convert.ToDecimal(sdr["DesS"].ToString()),
                            Sep = Convert.ToDecimal(sdr["Sep"].ToString()),
                            SepS = (app < 2) ? 0 : Convert.ToDecimal(sdr["SepS"].ToString())
                        });
                        #endregion
                        break;
                    case "Rekap":
                        #region rekap
                        app = Convert.ToInt32(sdr["Approval"].ToString());
                        arrData.Add(new PES1
                        {
                            SOPName = sdr["SOPName"].ToString(),
                            BobotNilai = Convert.ToDecimal(sdr["NilaiBobot"].ToString()),
                            PIC = sdr["PIC"].ToString(),
                            Approval = Convert.ToInt32(sdr["Approval"].ToString()),
                            Jan = Convert.ToDecimal(sdr["Jan"].ToString()),
                            JanS = (app < 2) ? 0 : Convert.ToDecimal(sdr["janS"].ToString()),
                            Feb = Convert.ToDecimal(sdr["Feb"].ToString()),
                            FebS = (app < 2) ? 0 : Convert.ToDecimal(sdr["FebS"].ToString()),
                            Mar = Convert.ToDecimal(sdr["Mar"].ToString()),
                            MarS = (app < 2) ? 0 : Convert.ToDecimal(sdr["MarS"].ToString()),
                            Apr = Convert.ToDecimal(sdr["Apr"].ToString()),
                            AprS = (app < 2) ? 0 : Convert.ToDecimal(sdr["AprS"].ToString())
                            /*,Mei = Convert.ToDecimal(sdr["Mei"].ToString()),
                            MeiS = (app < 2) ? 0 : Convert.ToDecimal(sdr["MeiS"].ToString()),
                            Jun = Convert.ToDecimal(sdr["Jun"].ToString()),
                            JunS = (app < 2) ? 0 : Convert.ToDecimal(sdr["JunS"].ToString()),
                            Jul = Convert.ToDecimal(sdr["Jul"].ToString()),
                            JulS = (app < 2) ? 0 : Convert.ToDecimal(sdr["JulS"].ToString()),
                            Ags = Convert.ToDecimal(sdr["Ags"].ToString()),
                            AgsS = (app < 2) ? 0 : Convert.ToDecimal(sdr["AgsS"].ToString()),
                            Okt = Convert.ToDecimal(sdr["Okt"].ToString()),
                            OktS = (app < 2) ? 0 : Convert.ToDecimal(sdr["OktS"].ToString()),
                            Nop = Convert.ToDecimal(sdr["Nop"].ToString()),
                            NopS = (app < 2) ? 0 : Convert.ToDecimal(sdr["NopS"].ToString()),
                            Des = Convert.ToDecimal(sdr["Desm"].ToString()),
                            DesS = (app < 2) ? 0 : Convert.ToDecimal(sdr["DesS"].ToString()),
                            Sep = Convert.ToDecimal(sdr["Sep"].ToString()),
                            SepS = (app < 2) ? 0 : Convert.ToDecimal(sdr["SepS"].ToString())*/
                        });
                        #endregion
                        break;
                    case "ItemSOP":
                        #region NewReport
                        string[] scr = new string[] { };
                        scr = CheckNA(Convert.ToInt32(sdr["CatID"].ToString())).Split(',');
                        arrData.Add(new PES1
                        {
                            ID = Convert.ToInt32(sdr["ID"].ToString()),
                            ISOUserID = Convert.ToInt32(sdr["UserID"].ToString()),
                            SOPName = sdr["Description"].ToString(),
                            BobotNilai = Convert.ToDecimal(sdr["Bobot"].ToString()) * 100,
                            TypePes = Convert.ToInt32(sdr["PesType"].ToString()),
                            Target = sdr["Target"].ToString(),
                            Pencapaian = (scr.Contains(this.Bulan) || scr.Contains("0")) ? sdr["Keterangan"].ToString() : "N/A",
                            Score = Convert.ToDecimal(sdr["PointNilai"].ToString()),
                            Nilai = Convert.ToDecimal(sdr["Nilai"].ToString()),
                            TotalBobot = Convert.ToDecimal(sdr["Bbt"].ToString()) * 100
                        });
                        #endregion
                        break;
                    case "PICSop":
                        arrData.Add(new PES1
                        {
                            PIC = sdr["UserName"].ToString(),
                            ISOUserID = Convert.ToInt32(sdr["UserID"].ToString()),
                            BagianID = Convert.ToInt32(sdr["BagianID"].ToString()),
                            BagianName = sdr["BagianName"].ToString()
                        });
                        break;
                }
            }
        }
        return arrData;
    }
    public void GetTahun(DropDownList ddl)
    {
        arrData = new ArrayList();
        string strSQL = "select distinct YEAR(tglMulai) Tahun from ISO_Task order by year(tglMulai)";
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(strSQL);
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                ddl.Items.Add(new ListItem(sdr["Tahun"].ToString(), sdr["Tahun"].ToString()));
            }
        }
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
                arrData.Add(new PES1
                {
                    DeptID = Convert.ToInt32(sdr["ID"].ToString()),
                    DeptName = sdr["Alias"].ToString()
                });
            }
        }
        return arrData;
    }
    private string CheckNA(int CatID)
    {
        string na = string.Empty;
        string strSQL = "Select calc from ISO_Category where ID=" + CatID;
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(strSQL);
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                na = sdr["Calc"].ToString();
            }
        }
        return na;
    }
    public int isHasbeenInput(string Tabled)
    {
        int result = 0;
        string strSQL = "Select Count(ID)ID from " + Tabled + " where Month(tglMulai)=" + this.Bulan + " and Year(tglMulai)=" + this.Tahun +
                      " and PIC=(Select UserName from ISO_Users where ID=" + this.UserID + ") and BagianID=" + this.Bagian +
                      " and RowStatus>-1";
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(strSQL);
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                result = Convert.ToInt32(sdr["ID"].ToString());
            }
        }
        return result;
    }
}