using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using Factory;
using Cogs;

using System.Web.Services;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using DefectFacade;
using System.IO;

namespace GRCweb1.Modul.ISO
{
    public partial class RekapPES_LS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadTahun();
                LoadDept();
            }
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GrdDynamic.ClientID + "', 500, 100 , 30 ,false); </script>", false);
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(LinkButton3);
        }
        private void LoadTahun()
        {
            ISO_PES p = new ISO_PES();
            ArrayList arrData = new ArrayList();
            arrData = p.LoadTahun();
            ddlTahun.Items.Clear();
            if (arrData.Count == 0)
            {
                ddlTahun.Items.Add(new ListItem(DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString()));
            }
            foreach (PES2016 ps in arrData)
            {
                ddlTahun.Items.Add(new ListItem(ps.Tahun.ToString(), ps.Tahun.ToString()));
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }
        //private void LoadDept()
        //{
        //    try
        //    {
        //        ISO_PES ip = new ISO_PES();
        //        ArrayList arrData = new ArrayList();
        //        ip.Tahun = int.Parse(ddlTahun.SelectedValue.ToString());
        //        Users user = (Users)Session["Users"];
        //        switch (user.UnitKerjaID)
        //        {
        //            case 1:
        //            case 7:
        //                //

        //                if (((Users)Session["Users"]).DeptID.ToString() == "7" || ((Users)Session["Users"]).RowStatus.ToString() == "10")
        //                    arrData = ip.LoadDept();
        //                else
        //                    arrData = ip.LoadDeptRPES(user.ID);
        //                break;
        //            default:
        //                //arrData = this.LoadDept(false,string.Empty);
        //                if (((Users)Session["Users"]).DeptID.ToString() == "7" || ((Users)Session["Users"]).RowStatus.ToString() == "10")
        //                    arrData = ip.LoadDept();
        //                else
        //                    arrData = ip.LoadDeptRPES(user.ID);
        //                break;
        //        }
        //        ddlDept.Items.Clear();
        //        if (((Users)Session["Users"]).DeptID.ToString() == "7" || ((Users)Session["Users"]).RowStatus.ToString() == "10")
        //            ddlDept.Items.Add(new ListItem("--ALL Dept--", "0"));
        //        else
        //            ddlDept.Items.Add(new ListItem(" ", "-1"));
        //        foreach (PES2016 d in arrData)
        //        {
        //            //if (((Users)Session["Users"]).DeptID.ToString() == "7" || ((Users)Session["Users"]).DeptID.ToString() == "14")
        //            //    ddlDept.Items.Add(new ListItem(d.DeptName, d.DeptID.ToString()));
        //            //else
        //            //{
        //            //if (d.DeptID.ToString()== ((Users)Session["Users"]).DeptID.ToString())
        //            ddlDept.Items.Add(new ListItem(d.DeptName, d.DeptID.ToString()));
        //            //}
        //        }

        //    }
        //    catch { }
        //}
        private void LoadDept()
        {
            UsersFacade usersFacade = new UsersFacade();
            Users users2 = usersFacade.RetrieveByUserName(((Users)Session["Users"]).UserName);

            DeptFacade deptFacade = new DeptFacade();
            deptFacade.Criteria = " order by Alias";
            ArrayList arrDept = new ArrayList();
            switch (users2.UnitKerjaID)
            {
                case 1:
                case 7:
                    arrDept = deptFacade.RetrieveAliasDept();
                    break;
                default:
                    arrDept = deptFacade.RetrieveAliasDept();
                    break;
            }

            Auth DeptAuth = new Auth();
            UserAuth oto = new UserAuth();
            oto.Option = "All";
            oto.Criteria = " and UserID=" + ((Users)Session["Users"]).ID.ToString();
            oto.Criteria += " and ModulName='REKAP PES' ";
            DeptAuth = oto.Retrieve(true);
            string UserDept = ((Users)Session["Users"]).DeptID.ToString() + ",0";
            string[] arrDepts = (DeptAuth.AuthDept != null) ? DeptAuth.AuthDept.Split(',') : UserDept.ToString().Split(',');
            ddlDept.Items.Clear();
            List<ListItem> lte = new List<ListItem>();
            if (deptFacade.Error == string.Empty)
            {
                ddlDept.Items.Add(new ListItem("-- All Dept --", "0"));

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
        public ArrayList LoadDept(bool forList, string Criteria)
        {
            ArrayList arrData = new ArrayList();
            //bpas_api.WebService1 api = new bpas_api.WebService1();
            Global2 api = new Global2();
            DataSet da = new DataSet();
            da = api.GetDataTable("Dept", "*", "Where RowStatus>-1 " + Criteria + " Order By DeptName", "GRCBoardPurch");
            foreach (DataRow d in da.Tables[0].Rows)
            {
                arrData.Add(new PES2016
                {
                    DeptID = Convert.ToInt32(d["ID"].ToString()),
                    DeptName = d["DeptName"].ToString()
                });
            }
            return arrData;
        }
        private void LoadDept(bool forList)
        {
            if (forList == true)
            {
                ISO_PES ip = new ISO_PES();
                ArrayList arrData = new ArrayList();
                ip.Criteria = (ddlDept.SelectedIndex > 0) ? " WHERE DeptID=" + ddlDept.SelectedValue.ToString() : "";
                string Kriteria = (ddlDept.SelectedIndex > 0) ? " AND ID=" + ddlDept.SelectedValue.ToString() : "";
                ip.Tahun = int.Parse(ddlTahun.SelectedValue.ToString());
                Users user = (Users)Session["Users"];
                switch (user.UnitKerjaID)
                {
                    case 1:
                    case 7:
                        arrData = ip.LoadDept();
                        break;
                    default:
                        arrData = this.LoadDept(false, Kriteria);
                        break;
                }
                //lstDept.DataSource = arrData;
                //lstDept.DataBind();
            }
            else
            {
                LoadDept();
            }
        }
        public ArrayList LoadDept(string Criteria)
        {
            ArrayList arrData = new ArrayList();
            //bpas_api.WebService1 api = new bpas_api.WebService1();
            Global2 api = new Global2();
            DataSet da = new DataSet();
            da = api.GetDataTable("Dept", "*", "Where RowStatus>-1 " + Criteria + " Order By DeptName", "GRCBoardPurch");
            foreach (DataRow d in da.Tables[0].Rows)
            {
                arrData.Add(new Dept
                {
                    ID = Convert.ToInt32(d["ID"].ToString()),
                    AlisName = d["DeptName"].ToString()
                });
            }
            return arrData;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            //if (RBTglInput.Checked == true)
            //    LblPeriode.Text = "Tanggal Input";
            //if (RBTglProduksi.Checked == true)
            //    LblPeriode.Text = "Tanggal Produksi";
            //if (RBTglPotong.Checked == true)
            //    LblPeriode.Text = "Tanggal Potong";
            //LblTgl1.Text = txtdrtanggal.Text;
            //LblTgl2.Text = txtsdtanggal.Text;
            loadDynamicGrid();
            loadDynamicGrid0();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        private void loadDynamicGrid()
        {
            Users user = (Users)Session["Users"];
            string strTahun = ddlTahun.SelectedItem.Text;
            int smt = ddlBulan.SelectedIndex;
            string kriteria = string.Empty;
            string strdeptid = string.Empty;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select top 1 deptid from userdeptauth where rowstatus>-1 and modulname='rekap pes' and userid =" + user.ID;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    strdeptid = sdr["deptid"].ToString();
                }
            }
            if (strdeptid == string.Empty)
                strdeptid = "0";
            if (ddlDept.Items.Count > 0 && ddlDept.SelectedIndex > 0)
            {
                string strdept = string.Empty;
                if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "MAINTENANCE")
                {
                    strdept = "select id from Dept where DeptName like 'maint%'";
                }
                else
                    strdept = ddlDept.SelectedValue.ToString(); ;

                kriteria = " and deptid in(" + strdept + ") ";
            }
            else
            {
                kriteria = " and deptid in(0," + strdeptid + ") ";
            }
            if (ddlBulan.SelectedIndex == 0)
                LPeriode.Text = " TAHUN " + ddlTahun.SelectedItem.Text;
            else
                LPeriode.Text = "SEMESTER " + ddlBulan.SelectedIndex.ToString() + " TAHUN " + ddlTahun.SelectedItem.Text;
            #region query
            string strSQL = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpResult]') AND type in (N'U')) DROP TABLE [dbo].[tmpResult] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TmpQ1]') AND type in (N'U')) DROP TABLE [dbo].[TmpQ1] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpsmt1]') AND type in (N'U')) DROP TABLE [dbo].[tmpsmt1]   " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpsmt2]') AND type in (N'U')) DROP TABLE [dbo].[tmpsmt2] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpResult]') AND type in (N'U')) DROP TABLE [dbo].[tmpResult] " +
                "declare @picName varchar(max)declare @startp int declare @endp int declare @tahun int declare @rekap bit " +
                "declare @Deptid int declare @bagianID int " +
                "declare @Start varchar(10) declare @Stop varchar(10) declare @Semester int declare @OutputField int " +
                "declare @EndSmt varchar(max) " +
                "set @tahun= " + strTahun + " " +
                "set @Semester= " + smt + " " +
                "if @Semester =1  " +
                "begin set @startp=1 set @endp=6  set @EndSmt=rtrim(CAST(@tahun as CHAR))+'06'  " +
                "set @Start=rtrim(CAST(@tahun as CHAR))+ +'0101' set @Stop =rtrim(CAST(@tahun as CHAR))+ +'0630'  end " +
                "if @Semester =2 " +
                "begin set @startp=7 set @endp=12  set @EndSmt=rtrim(CAST(@tahun as CHAR))+'12'  " +
                "set @Start=rtrim(CAST(@tahun as CHAR))+ +'0701' set @Stop =rtrim(CAST(@tahun as CHAR))+ +'1231' end " +
                "if @Semester =0 " +
                "begin set @startp=1 set @endp=12  set @EndSmt=rtrim(CAST(@tahun as CHAR))+'12'  " +
                "set @Start=rtrim(CAST(@tahun as CHAR))+ +'0101' set @Stop =rtrim(CAST(@tahun as CHAR))+ +'1231' end " +
                " " +
                "if @Semester =1  " +
                "begin " +
                "CREATE Table tmpResult (        " +
                "DeptName varchar(100) NULL,PIC varchar(100) NULL, " +
                "BagianName varchar(100) NULL,K1 decimal(18,2),K2 decimal(18,2),K3 decimal(18,2), K4  decimal(18,2),K5 decimal(18,2),K6 decimal(18,2),KJml decimal(18,2), " +
                "S1 decimal(18,2),S2 decimal(18,2),S3 decimal(18,2),S4 decimal(18,2),S5 decimal(18,2),S6 decimal(18,2),SJml decimal(18,2), " +
                "T1 decimal(18,2),T2 decimal(18,2),T3 decimal(18,2),T4 decimal(18,2),T5 decimal(18,2),T6 decimal(18,2),TJml decimal(18,2), " +
                "D1 decimal(18,2),D2 decimal(18,2),D3 decimal(18,2),D4 decimal(18,2),D5 decimal(18,2),D6 decimal(18,2),DJml decimal(18,2), " +
                "Nilai decimal(18,2)) SET ANSI_WARNINGS OFF  " +
                "end " +
                "if @Semester =2  " +
                "begin " +
                "CREATE Table tmpResult (        " +
                "DeptName varchar(100) NULL,PIC varchar(100) NULL, " +
                "BagianName varchar(100) NULL,K7 decimal(18,2),K8 decimal(18,2),K9 decimal(18,2),K10 decimal(18,2),K11 decimal(18,2),K12 decimal(18,2),KJml decimal(18,2), " +
                "S7 decimal(18,2),S8 decimal(18,2),S9 decimal(18,2),S10 decimal(18,2),S11 decimal(18,2),S12 decimal(18,2),SJml decimal(18,2), " +
                "T7 decimal(18,2),T8 decimal(18,2),T9 decimal(18,2),T10 decimal(18,2),T11 decimal(18,2),T12 decimal(18,2),TJml decimal(18,2), " +
                "D7 decimal(18,2),D8 decimal(18,2),D9 decimal(18,2),D10 decimal(18,2),D11 decimal(18,2),D12 decimal(18,2),DJml decimal(18,2), " +
                "Nilai decimal(18,2)) SET ANSI_WARNINGS OFF " +
                "end " +
                "if @Semester =0  " +
                "begin " +
                "CREATE Table tmpResult (        " +
                "DeptName varchar(100) NULL,PIC varchar(100) NULL, " +
                "BagianName varchar(100) NULL,K1 decimal(18,2),K2 decimal(18,2),K3 decimal(18,2), K4  decimal(18,2),K5 decimal(18,2),K6 decimal(18,2),K7 decimal(18,2), " +
                "K8 decimal(18,2),K9 decimal(18,2),K10 decimal(18,2),K11 decimal(18,2),K12 decimal(18,2),KJml decimal(18,2), " +
                "S1 decimal(18,2),S2 decimal(18,2),S3 decimal(18,2),S4 decimal(18,2),S5 decimal(18,2),S6 decimal(18,2),S7 decimal(18,2),S8 decimal(18,2), " +
                "S9 decimal(18,2),S10 decimal(18,2),S11 decimal(18,2),S12 decimal(18,2),SJml decimal(18,2), " +
                "T1 decimal(18,2),T2 decimal(18,2),T3 decimal(18,2),T4 decimal(18,2),T5 decimal(18,2),T6 decimal(18,2),T7 decimal(18,2),T8 decimal(18,2), " +
                "T9 decimal(18,2),T10 decimal(18,2),T11 decimal(18,2),T12 decimal(18,2),TJml decimal(18,2), " +
                "D1 decimal(18,2),D2 decimal(18,2),D3 decimal(18,2),D4 decimal(18,2),D5 decimal(18,2),D6 decimal(18,2),D7 decimal(18,2),D8 decimal(18,2),D9 decimal(18,2), " +
                "D10 decimal(18,2),D11 decimal(18,2),D12 decimal(18,2),DJml decimal(18,2),SMT1 decimal(18,2),SMT2 decimal(18,2),Nilai decimal(18,2)) SET ANSI_WARNINGS OFF  " +
                "end " +
                " " +
                "declare kursor cursor for  " +
                "select distinct @Semester semester, pic  from iso_sop where RowStatus>-1 and Convert(char,tglmulai,112)>=@start and Convert(char,tglmulai,112)<=@Stop and " +
                "iso_userid in (select ID from iso_users where rowstatus>-1 " + kriteria + ") order by pic " +
                "open kursor  " +
                "FETCH NEXT FROM kursor  " +
                "INTO @Semester,@picName " +
                "WHILE @@FETCH_STATUS = 0  " +
                "begin " +
                " " +
                "set @Deptid=(select top 1 deptid from ISO_Users where RowStatus>-1 and UserName=@picName  order by ID desc)  " +
                "set @bagianID=(select top 1 DeptJabatanID from ISO_Users where RowStatus>-1 and UserName=@picName order by ID desc)  " +
                "set @OutputField=1 " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TmpQ1]') AND type in (N'U')) DROP TABLE [dbo].[TmpQ1] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpsmt1]') AND type in (N'U')) DROP TABLE [dbo].[tmpsmt1]   " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpsmt2]') AND type in (N'U')) DROP TABLE [dbo].[tmpsmt2] " +
                " " +
                "DECLARE @task INT;DECLARE @kpi INT;DECLARE @sop INT; DECLARE @disp INT CREATE Table tmpQ1 (        " +
                "PIC varchar(100) NULL,DeptID int NULL,BagianID int NULL,CategoryID varchar NULL,KPIName varchar(max),Tahun int,Bobot decimal(18,2),        " +
                "JanP decimal(18,2),JanN decimal(18,2),FebP decimal(18,2),FebN decimal(18,2),MarP decimal(18,2),MarN decimal(18,2),        " +
                "AprP decimal(18,2),AprN decimal(18,2),MeiP decimal(18,2),MeiN decimal(18,2),JunP decimal(18,2),JunN decimal(18,2),        " +
                "JulP decimal(18,2),JulN decimal(18,2),AgsP decimal(18,2),AgsN decimal(18,2),SepP decimal(18,2),SepN decimal(18,2),        " +
                "OktP decimal(18,2),OktN decimal(18,2),NovP decimal(18,2),NovN decimal(18,2),DesP decimal(18,2),DesN decimal(18,2),        " +
                "JanB decimal(18,2), FebB decimal(18,2), MarB decimal(18,2), AprB decimal(18,2), MeiB decimal(18,2), JunB decimal(18,2),        " +
                "JulB decimal(18,2), AgsB decimal(18,2), SepB decimal(18,2), OktB decimal(18,2), NovB decimal(18,2), DesB decimal(18,2),Approval int         " +
                ",Penilaian int,bulan varchar(max),ThnMulai int,Urutan int ) SET ANSI_WARNINGS OFF    " +
                "INSERT INTO tmpQ1 exec dbo.RekapPES_KPI @picName,@startp,@endp,@tahun,'true'    " +
                "INSERT INTO tmpQ1 exec dbo.RekapPES_SOP @picName,@startp,@endp,@tahun,'true'    " +
                "INSERT INTO tmpQ1 exec dbo.RekapDisiplin @picName,@Deptid,@bagianID,@tahun   " +
                "INSERT INTO tmpQ1 exec dbo.RekapTask @picName,@Deptid,@bagianID,@Start,@Stop,@tahun   " +
                "SET @task =(Select Count(CategoryID) From tmpQ1 Where CategoryID=2)    " +
                "IF @task=0 BEGIN INSERT INTO tmpQ1 (KPIName,PIC,DeptID,BagianID,CategoryID)VALUES('TASK',@picName,@Deptid,@bagianID,2) END    " +
                "SET @kpi =(Select Count(CategoryID) From tmpQ1 Where CategoryID=1)    " +
                "IF @kpi=0 BEGIN INSERT INTO tmpQ1 (KPIName,PIC,DeptID,BagianID,CategoryID)VALUES('KPI',@picName,@Deptid,@bagianID,1) END    " +
                "SET @sop =(Select Count(CategoryID) From tmpQ1 Where CategoryID=3)    " +
                "IF @sop=0 BEGIN INSERT INTO tmpQ1 (KPIName,PIC,DeptID,BagianID,CategoryID)VALUES('SOP',@picName,@Deptid,@bagianID,3) END    " +
                "SET @disp =(Select Count(CategoryID) From tmpQ1 Where CategoryID=4)    " +
                "IF @disp=0 BEGIN INSERT INTO tmpQ1 (KPIName,PIC,DeptID,BagianID,CategoryID)VALUES('DISIPLIN',@picName,@Deptid,@bagianID,4) END   " +
                "declare @pembagi int declare @awalpes int declare @akhirpes int " +
                " " +
                "if @Semester=1 " +
                "begin  " +
                "set @awalpes= (select dbo.fnTglMulaiPES(@picName,@bagianID)) " +
                "set @akhirpes= (select dbo.fnTglAkhirPES(@picName,@bagianID)) " +
                "if @awalpes <= CAST(CAST(@tahun as varchar(4))+'01' as int) begin set @awalpes=CAST(CAST(@tahun as varchar(4))+'01' as int) end  " +
                "if @akhirpes >= CAST(CAST(@tahun as varchar(4))+'06' as int) begin set @akhirpes=CAST(CAST(@tahun as varchar(4))+'06' as int) end  " +
                "set @pembagi=@akhirpes-@awalpes+1 " +
                "if @pembagi=0 begin set @pembagi=1 end " +
                "select *,KJml+SJml+TJml+DJml Nilai  into tmpsmt1 from ( " +
                "select (select top 1 Alias from dept where ID=SS.deptid)DeptName,Username,BagianName, " +
                " K1,K2,K3,K4,K5,K6,cast((K1+K2+K3+K4+K5+K6)/@pembagi as decimal(8,1))KJml, " +
                " S1,S2,S3,S4,S5,S6,cast((S1+S2+S3+S4+S5+S6)/@pembagi as decimal(8,1))SJml, " +
                " T1,T2,T3,T4,T5,T6,TB,case when TN>minB then cast(1*TB*TBobot /100 as decimal(8,1)) else cast((TN/minB)*TB*TBobot /100 as decimal(8,1))end TJml, " +
                " D1,D2,D3,D4,D5,D6,cast((D1+D2+D3+D4+D5+D6)/@pembagi as decimal(8,2))as DJml from ( " +
                " select case when isnull((select top 1 UserName from UserAccount where  UserID in (select ID from ISO_Users where UserName =@picName)),'')<>'' then (select top 1 UserName from UserAccount where  UserID in (select ID from ISO_Users where UserName =@picName)) else @picName end Username, DeptID,BagianName, " +
                " sum(KBobot)KBobot, cast(sum(K1)as decimal (8,1))K1, cast(sum(K2)as decimal (8,1))K2, cast(sum(K3)as decimal (8,1))K3, cast(sum(K4)as decimal (8,1))K4, cast(sum(K5)as decimal (8,1))K5,  " +
                " cast(sum(K6)as decimal (8,1))K6,  " +
                "  sum(SBobot)SBobot, cast(sum(S1)as decimal (8,1))S1, cast(sum(S2)as decimal (8,1))S2, cast(sum(S3)as decimal (8,1))S3, cast(sum(S4)as decimal (8,1))S4, cast(sum(S5)as decimal (8,1))S5, cast(sum(S6)as decimal (8,1))S6,  " +
                " sum(TBobot)TBobot, case sum(TBobot) when 10 then 6 when 15 then 8 else  sum(TBobot)-10 end minB,  " +
                " cast(sum(T1)as decimal (8,1))T1, cast(sum(T2)as decimal (8,1))T2, cast(sum(T3)as decimal (8,1))T3,  " +
                " cast(sum(T4)as decimal (8,1))T4, cast(sum(T5)as decimal (8,1))T5, cast(sum(T6)as decimal (8,1))T6,  " +
                " (SELECT dbo.GetNilaiTask(@picName,@Deptid,@bagianID,@Start,@Stop,@Semester,1)) TB, " +
                " (SELECT dbo.GetNilaiTask(@picName,@Deptid,@bagianID,@Start,@Stop,@Semester,2)) TN, " +
                " sum(DBobot)DBobot,cast(sum(D1)as decimal(8,2))D1,cast(sum(D2)as decimal(8,2))D2,cast(sum(D3)as decimal(8,2))D3,cast(sum(D4)as decimal(8,2))D4, " +
                " cast(sum(D5)as decimal (8,1))D5, cast(sum(D6)as decimal (8,1))D6 from ( " +
                " SELECT DISTINCT ISNULL(PIC,@picName)PIC,ISNULL(TmpQ1.DeptID,ib.DeptID)DeptID,ib.BagianName,    " +
                " isnull(((SELECT dbo.GetBobotPES(@Deptid,@bagianID,@EndSmt,ip.ID))*100),0) KBobot,      " +
                " ISNULL((JanN*JanB),0)K1,ISNULL((FebN*FebB),0)K2,ISNULL((MarN*MarB),0)K3,ISNULL((AprN*AprB),0)K4,ISNULL((MeiN*MeiB),0)K5,ISNULL((JunN*JunB),0)K6,  " +
                " 0 SBobot,0 S1,0 S2,0 S3,0 S4,0 S5,0 S6,0 TBobot,0 T1,0 T2,0 T3,0 T4,0 T5,0 T6,0 DBobot,0 D1,0 D2,0 D3,0 D4,0 D5,0 D6 " +
                " FROM TmpQ1 FULL OUTER JOIN ISO_PES ip ON ip.ID=TmpQ1.CategoryID LEFT JOIN ISO_Bagian ib ON ib.ID=TmpQ1.BagianID   WHERE ip.ID =1  " +
                " union all " +
                " SELECT DISTINCT ISNULL(PIC,@picName)PIC,ISNULL(TmpQ1.DeptID,ib.DeptID)DeptID,ib.BagianName,    " +
                " 0 KBobot,0 K1,0 K2,0 K3,0 K4,0 K5,0 K6,isnull(((SELECT dbo.GetBobotPES(@Deptid,@bagianID,@EndSmt,ip.ID))*100),0) SBobot, " +
                " ISNULL((JanN*JanB),0)S1,ISNULL((FebN*FebB),0)S2,ISNULL((MarN*MarB),0)S3,ISNULL((AprN*AprB),0)S4,ISNULL((MeiN*MeiB),0)S5,ISNULL((JunN*JunB),0)S6, " +
                " 0 TBobot,0 T1,0 T2,0 T2,0 T4,0 T5,0 T6,0 DBobot,0 D1,0 D2,0 D3,0 D4,0 D5,0 D6 " +
                " FROM TmpQ1 FULL OUTER JOIN ISO_PES ip ON ip.ID=TmpQ1.CategoryID LEFT JOIN ISO_Bagian ib ON ib.ID=TmpQ1.BagianID   WHERE ip.ID =3  " +
                " union all " +
                " SELECT DISTINCT ISNULL(PIC,@picName)PIC,ISNULL(TmpQ1.DeptID,ib.DeptID)DeptID,ib.BagianName,    " +
                " 0 KBobot, 0 K1,0 K2,0 K3,0 K4,0 K5,0 K6,0 SBobot,0 S1,0 S2,0 S3,0 S4,0 S5,0 S6, " +
                " isnull(((SELECT dbo.GetBobotPES(@Deptid,@bagianID,@EndSmt,ip.ID))*100),0) TBobot, ISNULL((JanN*JanB),0) T1,ISNULL((FebN*FebB),0)T2,ISNULL((MarN*MarB),0) T3, " +
                " ISNULL((AprN*AprB),0)T4,ISNULL((MeiN*MeiB),0)T5,ISNULL((JunN*JunB),0) T6, " +
                " 0 DBobot,0 D1,0 D2,0 D3,0 D4,0 D5,0 D6 " +
                " FROM TmpQ1 FULL OUTER JOIN ISO_PES ip ON ip.ID=TmpQ1.CategoryID LEFT JOIN ISO_Bagian ib ON ib.ID=TmpQ1.BagianID   WHERE ip.ID =2 " +
                " union all " +
                " SELECT DISTINCT ISNULL(PIC,@picName)PIC,ISNULL(TmpQ1.DeptID,ib.DeptID)DeptID,ib.BagianName,    " +
                " 0 KBobot,0 K1,0 K2,0 K3,0 K4,0 K5,0 K6,0 SBobot,0 S1,0 S2,0 S3,0 S4,0 S5,0 S6, 0 TBobot, 0 T1,0 T2,0 T3,0 T4,0 T5,0 T6, " +
                " isnull(((SELECT dbo.GetBobotPES(@Deptid,@bagianID,@EndSmt,ip.ID))*100),0) DBobot,ISNULL((JanN*JanB),0) D1,ISNULL((FebN*FebB),0) D2, " +
                " ISNULL((MarN*MarB),0)D3,ISNULL((AprN*AprB),0) D4,ISNULL((MeiN*MeiB),0) D5,ISNULL((JunN*JunB),0) D6 " +
                " FROM TmpQ1 FULL OUTER JOIN ISO_PES ip ON ip.ID=TmpQ1.CategoryID LEFT JOIN ISO_Bagian ib ON ib.ID=TmpQ1.BagianID   WHERE ip.ID =4 )S  " +
                " group by pic,DeptID,BagianName)SS)SSS " +
                "  " +
                "insert  tmpResult " +
                "select  DeptName,Username,BagianName,K1,K2,K3, K4 ,K5,K6, KJml, " +
                "S1,S2,S3,S4,S5,S6,cast(SJml as decimal(8,2)) SJml, " +
                "T1,T2,T3,T4,T5,T6,cast(TJml as decimal(8,2)) TJml, " +
                "D1,D2,D3,D4,D5,D6,cast(DJml as decimal(8,2)) DJml,cast(Nilai as decimal(8,1)) Nilai " +
                "from tmpsmt1 " +
                "end " +
                " " +
                "if @Semester=2 " +
                "begin  " +
                "set @awalpes= (select dbo.fnTglMulaiPES(@picName,@bagianID)) " +
                "set @akhirpes= (select dbo.fnTglAkhirPES(@picName,@bagianID)) " +
                "if @awalpes <= CAST(CAST(@tahun as varchar(4))+'07' as int) begin set @awalpes=CAST(CAST(@tahun as varchar(4))+'07' as int) end  " +
                "if @akhirpes >= CAST(CAST(@tahun as varchar(4))+'12' as int) begin set @akhirpes=CAST(CAST(@tahun as varchar(4))+'12' as int) end  " +
                "set @pembagi=@akhirpes-@awalpes+1 " +
                "if @pembagi=0 begin set @pembagi=1 end " +
                "select *,KJml+SJml+TJml+DJml Nilai  into tmpsmt2 from ( " +
                "select (select top 1 Alias from dept where ID=SS.deptid)DeptName,Username,BagianName, " +
                " K7,K8,K9,K10,K11,K12,cast((K7+K8+K9+K10+K11+K12)/@pembagi as decimal(8,1))KJml, " +
                " S7,S8,S9,S10,S11,S12,cast((S7+S8+S9+S10+S11+S12)/@pembagi as decimal(8,1))SJml, " +
                " T7,T8,T9,T10,T11,T12,case when TN>minB then cast(1*TB*TBobot /100 as decimal(8,1)) else cast((TN/minB)*TB*TBobot /100 as decimal(8,1))end TJml, " +
                " D7,D8,D9,D10,D11,D12,cast((D7+D8+D9+D10+D11+D12)/@pembagi as decimal(8,2))as DJml from ( " +
                " select case when isnull((select top 1 UserName from UserAccount where  UserID in (select ID from ISO_Users where UserName =@picName)),'')<>'' then (select top 1 UserName from UserAccount where  UserID in (select ID from ISO_Users where UserName =@picName)) else @picName end Username, DeptID,BagianName, " +
                " sum(KBobot)KBobot, cast(sum(K7)as decimal (8,1))K7, cast(sum(K8)as decimal (8,1))K8, cast(sum(K9)as decimal (8,1))K9, cast(sum(K10)as decimal (8,1))K10, cast(sum(K11)as decimal (8,1))K11,  " +
                " cast(sum(K12)as decimal (8,1))K12,  " +
                "  sum(SBobot)SBobot, cast(sum(S7)as decimal (8,1))S7, cast(sum(S8)as decimal (8,1))S8, cast(sum(S9)as decimal (8,1))S9, cast(sum(S10)as decimal (8,1))S10, cast(sum(S11)as decimal (8,1))S11, cast(sum(S12)as decimal (8,1))S12,  " +
                " sum(TBobot)TBobot, case sum(TBobot) when 10 then 6 when 15 then 8 else  sum(TBobot)-10 end minB,  " +
                " cast(sum(T7)as decimal (8,1))T7, cast(sum(T8)as decimal (8,1))T8, cast(sum(T9)as decimal (8,1))T9, cast(sum(T10)as decimal (8,1))T10, cast(sum(T11)as decimal (8,1))T11, cast(sum(T12)as decimal (8,1))T12,  " +
                " (SELECT dbo.GetNilaiTask(@picName,@Deptid,@bagianID,@Start,@Stop,@Semester,1)) TB, " +
                " (SELECT dbo.GetNilaiTask(@picName,@Deptid,@bagianID,@Start,@Stop,@Semester,2)) TN, " +
                " sum(DBobot)DBobot,cast(sum(D7)as decimal(8,2))D7,cast(sum(D8)as decimal(8,2))D8,cast(sum(D9)as decimal(8,2))D9,cast(sum(D10)as decimal(8,2))D10, " +
                " cast(sum(D11)as decimal (8,1))D11, cast(sum(D12)as decimal (8,1))D12 from ( " +
                " SELECT DISTINCT ISNULL(PIC,@picName)PIC,ISNULL(TmpQ1.DeptID,ib.DeptID)DeptID,ib.BagianName,    " +
                " isnull(((SELECT dbo.GetBobotPES(@Deptid,@bagianID,@EndSmt,ip.ID))*100),0) KBobot,      " +
                " ISNULL((JulN*JulB),0)K7,ISNULL((AgsN*AgsB),0)K8,ISNULL((SepN*SepB),0)K9,ISNULL((OktN*OktB),0)K10,ISNULL((NovN*NovB),0)K11,ISNULL((DesN*DesB),0)K12,  " +
                " 0 SBobot,0 S7,0 S8,0 S9,0 S10,0 S11,0 S12,0 TBobot,0 T7,0 T8,0 T9,0 T10,0 T11,0 T12,0 DBobot,0 D7,0 D8,0 D9,0 D10,0 D11,0 D12 " +
                " FROM TmpQ1 FULL OUTER JOIN ISO_PES ip ON ip.ID=TmpQ1.CategoryID LEFT JOIN ISO_Bagian ib ON ib.ID=TmpQ1.BagianID   WHERE ip.ID =1  " +
                " union all " +
                " SELECT DISTINCT ISNULL(PIC,@picName)PIC,ISNULL(TmpQ1.DeptID,ib.DeptID)DeptID,ib.BagianName,    " +
                " 0 KBobot,0 K7,0 K8,0 K9,0 K10,0 K11,0 K12,isnull(((SELECT dbo.GetBobotPES(@Deptid,@bagianID,@EndSmt,ip.ID))*100),0) SBobot, " +
                " ISNULL((JulN*JulB),0)S7,ISNULL((AgsN*AgsB),0)S8,ISNULL((SepN*SepB),0)S9,ISNULL((OktN*OktB),0)S10,ISNULL((NovN*NovB),0)S11,ISNULL((DesN*DesB),0)S12, " +
                " 0 TBobot,0 T7,0 T8,0 T8,0 T10,0 T11,0 T12,0 DBobot,0 D7,0 D8,0 D9,0 D10,0 D11,0 D12 " +
                " FROM TmpQ1 FULL OUTER JOIN ISO_PES ip ON ip.ID=TmpQ1.CategoryID LEFT JOIN ISO_Bagian ib ON ib.ID=TmpQ1.BagianID   WHERE ip.ID =3  " +
                " union all " +
                " SELECT DISTINCT ISNULL(PIC,@picName)PIC,ISNULL(TmpQ1.DeptID,ib.DeptID)DeptID,ib.BagianName,    " +
                " 0 KBobot, 0 K7,0 K8,0 K9,0 K10,0 K11,0 K12,0 SBobot,0 S7,0 S8,0 S9,0 S10,0 S11,0 S12, " +
                " isnull(((SELECT dbo.GetBobotPES(@Deptid,@bagianID,@EndSmt,ip.ID))*100),0) TBobot, ISNULL((JulN*JulB),0) T7,ISNULL((AgsN*AgsB),0)T8,ISNULL((SepN*SepB),0) T9, " +
                " ISNULL((OktN*OktB),0)T10,ISNULL((NovN*NovB),0)T11,ISNULL((DesN*DesB),0) T12,0 DBobot,0 D7,0 D8,0 D9,0 D10,0 D11,0 D12 " +
                " FROM TmpQ1 FULL OUTER JOIN ISO_PES ip ON ip.ID=TmpQ1.CategoryID LEFT JOIN ISO_Bagian ib ON ib.ID=TmpQ1.BagianID   WHERE ip.ID =2 " +
                " union all " +
                " SELECT DISTINCT ISNULL(PIC,@picName)PIC,ISNULL(TmpQ1.DeptID,ib.DeptID)DeptID,ib.BagianName,    " +
                " 0 KBobot,0 K7,0 K8,0 K9,0 K10,0 K11,0 K12,0 SBobot,0 S7,0 S8,0 S9,0 S10,0 S11,0 S12, 0 TBobot, 0 T7,0 T8,0 T9,0 T10,0 T11,0 T12, " +
                " isnull(((SELECT dbo.GetBobotPES(@Deptid,@bagianID,@EndSmt,ip.ID))*100),0) DBobot,ISNULL((JulN*JulB),0) D7,ISNULL((AgsN*AgsB),0) D8, " +
                " ISNULL((SepN*SepB),0)D9,ISNULL((OktN*OktB),0) D10,ISNULL((NovN*NovB),0) D11,ISNULL((DesN*DesB),0) D12 " +
                " FROM TmpQ1 FULL OUTER JOIN ISO_PES ip ON ip.ID=TmpQ1.CategoryID LEFT JOIN ISO_Bagian ib ON ib.ID=TmpQ1.BagianID   WHERE ip.ID =4 )S  " +
                " group by pic,DeptID,BagianName)SS)SSS " +
                " " +
                "insert  tmpResult " +
                "select DeptName,Username,BagianName,K7,K8,K9,K10,K11,K12,cast(KJml as decimal(8,2)) KJml, " +
                "S7,S8,S9,S10,S11,S12,cast(SJml as decimal(8,2)) SJml, " +
                "T7,T8,T9,T10,T11,T12,cast(TJml as decimal(8,2)) TJml, " +
                "D7,D8,D9,D10,D11,D12,cast(DJml as decimal(8,2)) DJml,cast(Nilai as decimal(8,1)) Nilai " +
                "from tmpsmt2 " +
                "end " +
                " " +
                "if @Semester=0 " +
                "begin " +
                "set @Semester=1 " +
                "set @awalpes= (select dbo.fnTglMulaiPES(@picName,@bagianID)) " +
                "set @akhirpes= (select dbo.fnTglAkhirPES(@picName,@bagianID)) " +
                "if @awalpes <= CAST(CAST(@tahun as varchar(4))+'01' as int) begin set @awalpes=CAST(CAST(@tahun as varchar(4))+'01' as int) end  " +
                "if @akhirpes >= CAST(CAST(@tahun as varchar(4))+'06' as int) begin set @akhirpes=CAST(CAST(@tahun as varchar(4))+'06' as int) end  " +
                "set @pembagi=@akhirpes-@awalpes+1 " +
                "if @pembagi=0 begin set @pembagi=1 end " +
                "select *,KJml1+SJml1+TJml1+DJml1 Nilai1 into tmpsmt1 from ( " +
                "select (select top 1 Alias from dept where ID=SS.deptid)DeptName,Username,BagianName, " +
                " K1,K2,K3,K4,K5,K6,cast((K1+K2+K3+K4+K5+K6)/@pembagi as decimal(8,1))KJml1, " +
                " S1,S2,S3,S4,S5,S6,cast((S1+S2+S3+S4+S5+S6)/@pembagi as decimal(8,1))SJml1, " +
                " T1,T2,T3,T4,T5,T6,case when TN>minB then cast(1*TB*TBobot /100 as decimal(8,1)) else cast((TN/minB)*TB*TBobot /100 as decimal(8,1))end TJml1, " +
                " D1,D2,D3,D4,D5,D6,cast((D1+D2+D3+D4+D5+D6)/@pembagi as decimal(8,2))as DJml1 from ( " +
                " select case when isnull((select top 1 UserName from UserAccount where  UserID in (select ID from ISO_Users where UserName =@picName)),'')<>'' then (select top 1 UserName from UserAccount where  UserID in (select ID from ISO_Users where UserName =@picName)) else @picName end Username, DeptID,BagianName, " +
                " sum(KBobot)KBobot, cast(sum(K1)as decimal (8,1))K1, cast(sum(K2)as decimal (8,1))K2, cast(sum(K3)as decimal (8,1))K3,  " +
                " cast(sum(K4)as decimal (8,1))K4, cast(sum(K5)as decimal (8,1))K5, cast(sum(K6)as decimal (8,1))K6,  " +
                "  sum(SBobot)SBobot, cast(sum(S1)as decimal (8,1))S1, cast(sum(S2)as decimal (8,1))S2, cast(sum(S3)as decimal (8,1))S3,  " +
                "  cast(sum(S4)as decimal (8,1))S4, cast(sum(S5)as decimal (8,1))S5, cast(sum(S6)as decimal (8,1))S6,  " +
                " sum(TBobot)TBobot, case sum(TBobot) when 10 then 6 when 15 then 8 else  sum(TBobot)-10 end minB,  " +
                " cast(sum(T1)as decimal (8,1))T1, cast(sum(T2)as decimal (8,1))T2, cast(sum(T3)as decimal (8,1))T3,  " +
                " cast(sum(T4)as decimal (8,1))T4, cast(sum(T5)as decimal (8,1))T5, cast(sum(T6)as decimal (8,1))T6,  " +
                " (SELECT dbo.GetNilaiTask(@picName,@Deptid,@bagianID,@Start,@Stop,@Semester,1)) TB, " +
                " (SELECT dbo.GetNilaiTask(@picName,@Deptid,@bagianID,@Start,@Stop,@Semester,2)) TN, " +
                " sum(DBobot)DBobot,cast(sum(D1)as decimal(8,2))D1,cast(sum(D2)as decimal(8,2))D2,cast(sum(D3)as decimal(8,2))D3, " +
                " cast(sum(D4)as decimal(8,2))D4, cast(sum(D5)as decimal (8,1))D5, cast(sum(D6)as decimal (8,1))D6 from ( " +
                " SELECT DISTINCT ISNULL(PIC,@picName)PIC,ISNULL(TmpQ1.DeptID,ib.DeptID)DeptID,ib.BagianName,    " +
                " isnull(((SELECT dbo.GetBobotPES(@Deptid,@bagianID,@EndSmt,ip.ID))*100),0) KBobot,      " +
                " ISNULL((JanN*JanB),0)K1,ISNULL((FebN*FebB),0)K2,ISNULL((MarN*MarB),0)K3,ISNULL((AprN*AprB),0)K4,ISNULL((MeiN*MeiB),0)K5,ISNULL((JunN*JunB),0)K6,  " +
                " 0 SBobot,0 S1,0 S2,0 S3,0 S4,0 S5,0 S6,0 TBobot,0 T1,0 T2,0 T3,0 T4,0 T5,0 T6,0 DBobot,0 D1,0 D2,0 D3,0 D4,0 D5,0 D6 " +
                " FROM TmpQ1 FULL OUTER JOIN ISO_PES ip ON ip.ID=TmpQ1.CategoryID LEFT JOIN ISO_Bagian ib ON ib.ID=TmpQ1.BagianID   WHERE ip.ID =1  " +
                " union all " +
                " SELECT DISTINCT ISNULL(PIC,@picName)PIC,ISNULL(TmpQ1.DeptID,ib.DeptID)DeptID,ib.BagianName,    " +
                " 0 KBobot,0 K1,0 K2,0 K3,0 K4,0 K5,0 K6,isnull(((SELECT dbo.GetBobotPES(@Deptid,@bagianID,@EndSmt,ip.ID))*100),0) SBobot, " +
                " ISNULL((JanN*JanB),0)S1,ISNULL((FebN*FebB),0)S2,ISNULL((MarN*MarB),0)S3,ISNULL((AprN*AprB),0)S4,ISNULL((MeiN*MeiB),0)S5,ISNULL((JunN*JunB),0)S6, " +
                " 0 TBobot,0 T1,0 T2,0 T2,0 T4,0 T5,0 T6,0 DBobot,0 D1,0 D2,0 D3,0 D4,0 D5,0 D6 " +
                " FROM TmpQ1 FULL OUTER JOIN ISO_PES ip ON ip.ID=TmpQ1.CategoryID LEFT JOIN ISO_Bagian ib ON ib.ID=TmpQ1.BagianID   WHERE ip.ID =3  " +
                " union all " +
                " SELECT DISTINCT ISNULL(PIC,@picName)PIC,ISNULL(TmpQ1.DeptID,ib.DeptID)DeptID,ib.BagianName,    " +
                " 0 KBobot, 0 K1,0 K2,0 K3,0 K4,0 K5,0 K6,0 SBobot,0 S1,0 S2,0 S3,0 S4,0 S5,0 S6, " +
                " isnull(((SELECT dbo.GetBobotPES(@Deptid,@bagianID,@EndSmt,ip.ID))*100),0) TBobot, ISNULL((JanN*JanB),0) T1, " +
                " ISNULL((FebN*FebB),0)T2,ISNULL((MarN*MarB),0) T3, ISNULL((AprN*AprB),0)T4,ISNULL((MeiN*MeiB),0)T5,ISNULL((JunN*JunB),0) T6, " +
                " 0 DBobot,0 D1,0 D2,0 D3,0 D4,0 D5,0 D6 " +
                " FROM TmpQ1 FULL OUTER JOIN ISO_PES ip ON ip.ID=TmpQ1.CategoryID LEFT JOIN ISO_Bagian ib ON ib.ID=TmpQ1.BagianID   WHERE ip.ID =2 " +
                " union all " +
                " SELECT DISTINCT ISNULL(PIC,@picName)PIC,ISNULL(TmpQ1.DeptID,ib.DeptID)DeptID,ib.BagianName,    " +
                " 0 KBobot,0 K1,0 K2,0 K3,0 K4,0 K5,0 K6,0 SBobot,0 S1,0 S2,0 S3,0 S4,0 S5,0 S6, 0 TBobot, 0 T1,0 T2,0 T3,0 T4,0 T5,0 T6, " +
                " isnull(((SELECT dbo.GetBobotPES(@Deptid,@bagianID,@EndSmt,ip.ID))*100),0) DBobot,ISNULL((JanN*JanB),0) D1,ISNULL((FebN*FebB),0) D2, " +
                " ISNULL((MarN*MarB),0)D3,ISNULL((AprN*AprB),0) D4,ISNULL((MeiN*MeiB),0) D5,ISNULL((JunN*JunB),0) D6 " +
                " FROM TmpQ1 FULL OUTER JOIN ISO_PES ip ON ip.ID=TmpQ1.CategoryID LEFT JOIN ISO_Bagian ib ON ib.ID=TmpQ1.BagianID   WHERE ip.ID =4 )S  " +
                " group by pic,DeptID,BagianName)SS)SSS " +
                " " +
                "set @Semester=2  " +
                "set @awalpes= (select dbo.fnTglMulaiPES(@picName,@bagianID)) " +
                "set @akhirpes= (select dbo.fnTglAkhirPES(@picName,@bagianID)) " +
                "if @awalpes <= CAST(CAST(@tahun as varchar(4))+'07' as int) begin set @awalpes=CAST(CAST(@tahun as varchar(4))+'07' as int) end  " +
                "if @akhirpes >= CAST(CAST(@tahun as varchar(4))+'12' as int) begin set @akhirpes=CAST(CAST(@tahun as varchar(4))+'12' as int) end  " +
                "set @pembagi=@akhirpes-@awalpes+1 " +
                "if @pembagi=0 begin set @pembagi=1 end " +
                "select *,KJml2+SJml2+TJml2+DJml2 Nilai2 into tmpsmt2 from ( " +
                "select (select top 1 Alias from dept where ID=SS.deptid)DeptName,Username,BagianName, " +
                " K7,K8,K9,K10,K11,K12,cast((K7+K8+K9+K10+K11+K12)/@pembagi as decimal(8,1))KJml2, " +
                " S7,S8,S9,S10,S11,S12,cast((S7+S8+S9+S10+S11+S12)/@pembagi as decimal(8,1))SJml2, " +
                " T7,T8,T9,T10,T11,T12,case when TN>minB then cast(1*TB*TBobot /100 as decimal(8,1)) else cast((TN/minB)*TB*TBobot /100 as decimal(8,1))end TJml2, " +
                " D7,D8,D9,D10,D11,D12,cast((D7+D8+D9+D10+D11+D12)/@pembagi as decimal(8,2))as DJml2 from ( " +
                " select case when isnull((select top 1 UserName from UserAccount where  UserID in (select ID from ISO_Users where UserName =@picName)),'')<>'' then (select top 1 UserName from UserAccount where  UserID in (select ID from ISO_Users where UserName =@picName)) else @picName end Username, DeptID,BagianName, " +
                " sum(KBobot)KBobot, cast(sum(K7)as decimal (8,1))K7, cast(sum(K8)as decimal (8,1))K8, cast(sum(K9)as decimal (8,1))K9,  " +
                " cast(sum(K10)as decimal (8,1))K10, cast(sum(K11)as decimal (8,1))K11, cast(sum(K12)as decimal (8,1))K12,  " +
                "  sum(SBobot)SBobot, cast(sum(S7)as decimal (8,1))S7, cast(sum(S8)as decimal (8,1))S8, cast(sum(S9)as decimal (8,1))S9,  " +
                "  cast(sum(S10)as decimal (8,1))S10, cast(sum(S11)as decimal (8,1))S11, cast(sum(S12)as decimal (8,1))S12,  " +
                " sum(TBobot)TBobot, case sum(TBobot) when 10 then 6 when 15 then 8 else  sum(TBobot)-10 end minB,  " +
                " cast(sum(T7)as decimal (8,1))T7, cast(sum(T8)as decimal (8,1))T8, cast(sum(T9)as decimal (8,1))T9,  " +
                " cast(sum(T10)as decimal (8,1))T10, cast(sum(T11)as decimal (8,1))T11, cast(sum(T12)as decimal (8,1))T12,  " +
                " (SELECT dbo.GetNilaiTask(@picName,@Deptid,@bagianID,@Start,@Stop,@Semester,1)) TB, " +
                " (SELECT dbo.GetNilaiTask(@picName,@Deptid,@bagianID,@Start,@Stop,@Semester,2)) TN, " +
                " sum(DBobot)DBobot,cast(sum(D7)as decimal(8,2))D7,cast(sum(D8)as decimal(8,2))D8,cast(sum(D9)as decimal(8,2))D9,cast(sum(D10)as decimal(8,2))D10, " +
                " cast(sum(D11)as decimal (8,1))D11, cast(sum(D12)as decimal (8,1))D12 from ( " +
                " SELECT DISTINCT ISNULL(PIC,@picName)PIC,ISNULL(TmpQ1.DeptID,ib.DeptID)DeptID,ib.BagianName,    " +
                " isnull(((SELECT dbo.GetBobotPES(@Deptid,@bagianID,@EndSmt,ip.ID))*100),0) KBobot,      " +
                " ISNULL((JulN*JulB),0)K7,ISNULL((AgsN*AgsB),0)K8,ISNULL((SepN*SepB),0)K9,ISNULL((OktN*OktB),0)K10,ISNULL((NovN*NovB),0)K11,ISNULL((DesN*DesB),0)K12,  " +
                " 0 SBobot,0 S7,0 S8,0 S9,0 S10,0 S11,0 S12,0 TBobot,0 T7,0 T8,0 T9,0 T10,0 T11,0 T12,0 DBobot,0 D7,0 D8,0 D9,0 D10,0 D11,0 D12 " +
                " FROM TmpQ1 FULL OUTER JOIN ISO_PES ip ON ip.ID=TmpQ1.CategoryID LEFT JOIN ISO_Bagian ib ON ib.ID=TmpQ1.BagianID   WHERE ip.ID =1  " +
                " union all " +
                " SELECT DISTINCT ISNULL(PIC,@picName)PIC,ISNULL(TmpQ1.DeptID,ib.DeptID)DeptID,ib.BagianName,    " +
                " 0 KBobot,0 K7,0 K8,0 K9,0 K10,0 K11,0 K12,isnull(((SELECT dbo.GetBobotPES(@Deptid,@bagianID,@EndSmt,ip.ID))*100),0) SBobot, " +
                " ISNULL((JulN*JulB),0)S7,ISNULL((AgsN*AgsB),0)S8,ISNULL((SepN*SepB),0)S9,ISNULL((OktN*OktB),0)S10,ISNULL((NovN*NovB),0)S11,ISNULL((DesN*DesB),0)S12, " +
                " 0 TBobot,0 T7,0 T8,0 T8,0 T10,0 T11,0 T12,0 DBobot,0 D7,0 D8,0 D9,0 D10,0 D11,0 D12 " +
                " FROM TmpQ1 FULL OUTER JOIN ISO_PES ip ON ip.ID=TmpQ1.CategoryID LEFT JOIN ISO_Bagian ib ON ib.ID=TmpQ1.BagianID   WHERE ip.ID =3  " +
                " union all " +
                " SELECT DISTINCT ISNULL(PIC,@picName)PIC,ISNULL(TmpQ1.DeptID,ib.DeptID)DeptID,ib.BagianName,    " +
                " 0 KBobot, 0 K7,0 K8,0 K9,0 K10,0 K11,0 K12,0 SBobot,0 S7,0 S8,0 S9,0 S10,0 S11,0 S12, " +
                " isnull(((SELECT dbo.GetBobotPES(@Deptid,@bagianID,@EndSmt,ip.ID))*100),0) TBobot, ISNULL((JulN*JulB),0) T7,ISNULL((AgsN*AgsB),0)T8,ISNULL((SepN*SepB),0) T9, " +
                " ISNULL((OktN*OktB),0)T10,ISNULL((NovN*NovB),0)T11,ISNULL((DesN*DesB),0) T12,0 DBobot,0 D7,0 D8,0 D9,0 D10,0 D11,0 D12 " +
                " FROM TmpQ1 FULL OUTER JOIN ISO_PES ip ON ip.ID=TmpQ1.CategoryID LEFT JOIN ISO_Bagian ib ON ib.ID=TmpQ1.BagianID   WHERE ip.ID =2 " +
                " union all " +
                " SELECT DISTINCT ISNULL(PIC,@picName)PIC,ISNULL(TmpQ1.DeptID,ib.DeptID)DeptID,ib.BagianName,    " +
                " 0 KBobot,0 K7,0 K8,0 K9,0 K10,0 K11,0 K12,0 SBobot,0 S7,0 S8,0 S9,0 S10,0 S11,0 S12, 0 TBobot, 0 T7,0 T8,0 T9,0 T10,0 T11,0 T12, " +
                " isnull(((SELECT dbo.GetBobotPES(@Deptid,@bagianID,@EndSmt,ip.ID))*100),0) DBobot,ISNULL((JulN*JulB),0) D7,ISNULL((AgsN*AgsB),0) D8, " +
                " ISNULL((SepN*SepB),0)D9,ISNULL((OktN*OktB),0) D10,ISNULL((NovN*NovB),0) D11,ISNULL((DesN*DesB),0) D12 " +
                " FROM TmpQ1 FULL OUTER JOIN ISO_PES ip ON ip.ID=TmpQ1.CategoryID LEFT JOIN ISO_Bagian ib ON ib.ID=TmpQ1.BagianID   WHERE ip.ID =4 )S  " +
                " group by pic,DeptID,BagianName)SS)SSS " +
                "  " +
                "insert tmpResult " +
                "select  A.DeptName,A.Username,A.BagianName,K1,K2,K3, K4 ,K5,K6,K7,K8,K9,K10,K11,K12, " +
                "case when KJml1=0 then cast(KJml2 as decimal(8,2)) when KJml2=0 then cast(KJml1 as decimal(8,2)) else cast((KJml2+KJml1)/2 as decimal(8,2)) end KJml, " +
                "S1,S2,S3,S4,S5,S6,S7,S8,S9,S10,S11,S12, " +
                "case when SJml1=0 then cast(SJml2 as decimal(8,2)) when SJml2=0 then cast(SJml1 as decimal(8,2)) else cast((SJml2+SJml1)/2 as decimal(8,2)) end SJml, " +
                "T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12, " +
                "case when TJml1=0 then cast(TJml2 as decimal(8,2)) when TJml2=0 then cast(TJml1 as decimal(8,2)) else cast((TJml2+TJml1)/2 as decimal(8,2)) end TJml, " +
                "D1,D2,D3,D4,D5,D6,D7,D8,D9,D10,D11,D12, " +
                "case when DJml1=0 then cast(DJml2 as decimal(8,2)) when DJml2=0 then cast(DJml1 as decimal(8,2)) else cast((DJml2+DJml1)/2 as decimal(8,2)) end DJml, " +
                "Nilai1 SMT1,Nilai2 SMT2, " +
                "case when Nilai1=0 then cast(Nilai2 as decimal(8,2)) when Nilai2=0 then cast(Nilai1 as decimal(8,2)) else cast((Nilai1+Nilai2)/2 as decimal(8,2)) end Nilai " +
                "from tmpsmt1 A inner join tmpsmt2 B on A.DeptName=B.DeptName and A.username=A.username and A.bagianname=B.bagianname " +
                "set @Semester=0 " +
                "end " +
                "  " +
                " FETCH NEXT FROM kursor  " +
                "    INTO @Semester,@picName " +
                "END  " +
                "CLOSE kursor  " +
                "DEALLOCATE kursor  " +
                " " +
                "if  @Semester =1 " +
                "begin " +
                "select case when Nomor > 1 then Nomor-1 else Null end  Nomor,case when PIC='-' then 'Dept : ' +Deptname else PIC end PIC,BagianName, " +
                "K1,K2,K3,K4 ,K5 ,K6 ,KJml,S1,S2,S3,S4 ,S5 ,S6 ,SJml,T1,T2,T3,T4 ,T5 ,T6 ,TJml,D1,D2,D3,D4 ,D5 ,D6 ,DJml,Nilai " +
                "from( " +
                "select ROW_NUMBER() OVER (PARTITION BY DeptName ORDER BY PIC) AS Nomor,* from ( " +
                "select* from tmpresult where isnull(deptname,'')<>'' " +
                "union all " +
                "select  DeptName,'-','',Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null from tmpresult where isnull(deptname,'')<>'' group by deptname " +
                ")S)SS " +
                "end " +
                "if  @Semester =2 " +
                "begin " +
                "select case when Nomor > 1 then Nomor-1 else Null end  Nomor,case when PIC='-' then 'Dept : ' +Deptname else PIC end PIC,BagianName,K7,K8,K9,K10,K11,K12,KJml,S7,S8,S9,S10,S11,S12,SJml,T7,T8,T9,T10,T11,T12,TJml,D7,D8,D9,D10,D11,D12,DJml,Nilai " +
                "from( " +
                "select ROW_NUMBER() OVER (PARTITION BY DeptName ORDER BY PIC) AS Nomor,* from ( " +
                "select* from tmpresult where isnull(deptname,'')<>'' " +
                "union all " +
                "select  DeptName,'-','',Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null from tmpresult where isnull(deptname,'')<>'' group by deptname " +
                ")S)SS " +
                "end " +
                "if  @Semester =0 " +
                "begin " +
                "select case when Nomor > 1 then Nomor-1 else Null end Nomor ,case when PIC='-' then 'Dept : ' +Deptname else PIC end PIC,BagianName, " +
                "K1,K2,K3, K4 ,K5,K6,K7,K8,K9,K10,K11,K12,KJml,S1,S2,S3,S4,S5,S6,S7,S8,S9,S10,S11,S12,SJml,T1,T2,T3,T4,T5,T6,T7,T8,T9,T10,T11,T12,TJml,D1,D2,D3,D4,D5,D6,D7,D8,D9,D10,D11,D12,DJml,SMT1,SMT2,Nilai " +
                "from( " +
                "select ROW_NUMBER() OVER (PARTITION BY DeptName ORDER BY PIC) AS Nomor,* from ( " +
                "select* from tmpresult where isnull(deptname,'')<>'' " +
                "union all " +
                "select  DeptName,'-','',Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null,Null from tmpresult where isnull(deptname,'')<>'' group by deptname " +
                ")S)SS " +
                "end " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TmpQ1]') AND type in (N'U')) DROP TABLE [dbo].[TmpQ1] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpsmt1]') AND type in (N'U')) DROP TABLE [dbo].[tmpsmt1]   " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpsmt2]') AND type in (N'U')) DROP TABLE [dbo].[tmpsmt2] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpResult]') AND type in (N'U')) DROP TABLE [dbo].[tmpResult] ";
            #endregion
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
            da.SelectCommand.CommandTimeout = 0;
            #region Code for preparing the DataTable
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataColumn dcol = new DataColumn(ID, typeof(System.Int32));
            dcol.AutoIncrement = true;
            #endregion
            GrdDynamic.Columns.Clear();
            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();
                if (col.ColumnName != "Nomor" && col.ColumnName != "PIC"
                    && col.ColumnName != "BagianName")
                {
                    bfield.DataField = col.ColumnName;
                    if (col.ColumnName == "K1") bfield.HeaderText = "Jan";
                    if (col.ColumnName == "K2") bfield.HeaderText = "Feb";
                    if (col.ColumnName == "K3") bfield.HeaderText = "Mar";
                    if (col.ColumnName == "K4") bfield.HeaderText = "Apr";
                    if (col.ColumnName == "K5") bfield.HeaderText = "Mei";
                    if (col.ColumnName == "K6") bfield.HeaderText = "Jun";
                    if (col.ColumnName == "K7") bfield.HeaderText = "Jul";
                    if (col.ColumnName == "K8") bfield.HeaderText = "Agu";
                    if (col.ColumnName == "K9") bfield.HeaderText = "Sep";
                    if (col.ColumnName == "K10") bfield.HeaderText = "Okt";
                    if (col.ColumnName == "K11") bfield.HeaderText = "Nop";
                    if (col.ColumnName == "K12") bfield.HeaderText = "Des";
                    if (col.ColumnName == "KJml") bfield.HeaderText = "Total KPI";
                    if (col.ColumnName == "S1") bfield.HeaderText = "Jan";
                    if (col.ColumnName == "S2") bfield.HeaderText = "Feb";
                    if (col.ColumnName == "S3") bfield.HeaderText = "Mar";
                    if (col.ColumnName == "S4") bfield.HeaderText = "Apr";
                    if (col.ColumnName == "S5") bfield.HeaderText = "Mei";
                    if (col.ColumnName == "S6") bfield.HeaderText = "Jun";
                    if (col.ColumnName == "S7") bfield.HeaderText = "Jul";
                    if (col.ColumnName == "S8") bfield.HeaderText = "Agu";
                    if (col.ColumnName == "S9") bfield.HeaderText = "Sep";
                    if (col.ColumnName == "S10") bfield.HeaderText = "Okt";
                    if (col.ColumnName == "S11") bfield.HeaderText = "Nop";
                    if (col.ColumnName == "S12") bfield.HeaderText = "Des";
                    if (col.ColumnName == "SJml") bfield.HeaderText = "Total SOP";
                    if (col.ColumnName == "T1") bfield.HeaderText = "Jan";
                    if (col.ColumnName == "T2") bfield.HeaderText = "Feb";
                    if (col.ColumnName == "T3") bfield.HeaderText = "Mar";
                    if (col.ColumnName == "T4") bfield.HeaderText = "Apr";
                    if (col.ColumnName == "T5") bfield.HeaderText = "Mei";
                    if (col.ColumnName == "T6") bfield.HeaderText = "Jun";
                    if (col.ColumnName == "T7") bfield.HeaderText = "Jul";
                    if (col.ColumnName == "T8") bfield.HeaderText = "Agu";
                    if (col.ColumnName == "T9") bfield.HeaderText = "Sep";
                    if (col.ColumnName == "T10") bfield.HeaderText = "Okt";
                    if (col.ColumnName == "T11") bfield.HeaderText = "Nop";
                    if (col.ColumnName == "T12") bfield.HeaderText = "Des";
                    if (col.ColumnName == "TJml") bfield.HeaderText = "Total Task";
                    if (col.ColumnName == "D1") bfield.HeaderText = "Jan";
                    if (col.ColumnName == "D2") bfield.HeaderText = "Feb";
                    if (col.ColumnName == "D3") bfield.HeaderText = "Mar";
                    if (col.ColumnName == "D4") bfield.HeaderText = "Apr";
                    if (col.ColumnName == "D5") bfield.HeaderText = "Mei";
                    if (col.ColumnName == "D6") bfield.HeaderText = "Jun";
                    if (col.ColumnName == "D7") bfield.HeaderText = "Jul";
                    if (col.ColumnName == "D8") bfield.HeaderText = "Agu";
                    if (col.ColumnName == "D9") bfield.HeaderText = "Sep";
                    if (col.ColumnName == "D10") bfield.HeaderText = "Okt";
                    if (col.ColumnName == "D11") bfield.HeaderText = "Nop";
                    if (col.ColumnName == "D12") bfield.HeaderText = "Des";
                    if (col.ColumnName == "DJml") bfield.HeaderText = "Total Disiplin";
                    if (col.ColumnName == "SMT1") bfield.HeaderText = "SMT1";
                    if (col.ColumnName == "SMT2") bfield.HeaderText = "SMT2";
                    if (col.ColumnName == "Nilai") bfield.HeaderText = "Total Nilai PES";
                    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                }

                else
                {
                    bfield.DataField = col.ColumnName;
                    bfield.HeaderText = "";
                }

                GrdDynamic.Columns.Add(bfield);

            }
            Users users = (Users)Session["Users"];
            CompanyFacade companyFacade = new CompanyFacade();
            Company company = companyFacade.RetrieveById(users.UnitKerjaID);
            txtLokasi.Text = company.Lokasi + ", " + DateTime.Now.ToString("dd - MMM - yyyy");
            GrdDynamic.DataSource = dt;
            GrdDynamic.DataBind();
        }
        private void loadDynamicGrid0()
        {
            string strTahun = ddlTahun.SelectedItem.Text;
            int smt = ddlBulan.SelectedIndex;
            string kriteria = string.Empty;
            if (ddlDept.Items.Count > 0 && ddlDept.SelectedIndex > 0)
                kriteria = " where UA.DeptID=" + ddlDept.SelectedValue.ToString();
            if (ddlBulan.SelectedIndex == 0)
                LPeriode.Text = " TAHUN " + ddlTahun.SelectedItem.Text;
            else
                LPeriode.Text = "SEMESTER " + ddlBulan.SelectedIndex.ToString() + " TAHUN " + ddlTahun.SelectedItem.Text;
            #region query
            string strSQL = "select ROW_NUMBER() OVER (PARTITION BY DeptName ORDER BY UA.UserName) AS  No, UA.UserName Nama,IB.BagianName Jabatan, " +
                "rtrim(cast(cast((select top 1 bobot*100 from ISO_BobotPES where  activetahun<=" + ddlTahun.SelectedItem.Text + " and PesType =1 and BagianID=UA.BagianID order by activetahun desc,activebulan desc)as int) as char))+'%' KPI, " +
                "rtrim(cast(cast((select top 1 bobot*100 from ISO_BobotPES where  activetahun<=" + ddlTahun.SelectedItem.Text + " and PesType =2 and BagianID=UA.BagianID order by activetahun desc,activebulan desc)as int) as char))+'%'Task, " +
                "rtrim(cast(cast((select top 1 bobot*100 from ISO_BobotPES where  activetahun<=" + ddlTahun.SelectedItem.Text + " and PesType =3 and BagianID=UA.BagianID order by activetahun desc,activebulan desc)as int) as char))+'%'SOP, " +
                "rtrim(cast(cast((select top 1 bobot*100 from ISO_BobotPES where  activetahun<=" + ddlTahun.SelectedItem.Text + " and PesType =4 and BagianID=UA.BagianID order by activetahun desc,activebulan desc)as int) as char))+'%'Disiplin " +
                "from UserAccount UA inner join Dept D on UA.DeptID=D.ID and UA.RowStatus>-1  " +
                "inner join ISO_Bagian IB on IB.ID=UA.BagianID  " +
                 kriteria + " order by UA.UserName";
            #endregion
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
            da.SelectCommand.CommandTimeout = 0;
            #region Code for preparing the DataTable
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataColumn dcol = new DataColumn(ID, typeof(System.Int32));
            dcol.AutoIncrement = true;
            #endregion
            GrdDynamic0.Columns.Clear();
            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();
                //if (col.ColumnName != "Nomor" && col.ColumnName != "PIC"
                //    && col.ColumnName != "BagianName")
                //{
                //    bfield.DataField = col.ColumnName;

                //    bfield.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                //}

                //else
                //{
                bfield.DataField = col.ColumnName;
                bfield.HeaderText = col.ColumnName;
                //}

                GrdDynamic0.Columns.Add(bfield);

            }
            Users users = (Users)Session["Users"];
            CompanyFacade companyFacade = new CompanyFacade();
            Company company = companyFacade.RetrieveById(users.UnitKerjaID);
            lblPlant.Text = company.Lokasi;
            lblDept.Text = ddlDept.SelectedItem.Text;
            GrdDynamic0.DataSource = dt;
            GrdDynamic0.DataBind();
        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            if (GrdDynamic.Rows.Count == 0)
                return;
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "rekap_PES.xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GrdDynamic.AllowPaging = false;
            //GrdDynamic.HeaderRow.Style.Add("background-color", "#FFFFFF");
            //for (int i = 0; i < GrdDynamic.HeaderRow.Cells.Count; i++)
            //{
            //    GrdDynamic.HeaderRow.Cells[i].Style.Add("background-color", "#FFFFFF");
            //}
            Panel1.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        protected void txtsdtanggal_TextChanged(object sender, EventArgs e)
        {
            //if (DateTime.Parse(txtsdtanggal.Text) < DateTime.Parse(txtdrtanggal.Text) || 
            //    DateTime.Parse(txtdrtanggal.Text).ToString("MMM-yyyy")!=DateTime.Parse(txtsdtanggal.Text).ToString("MMMyyyy"))
            //    txtdrtanggal.Text = "01-" + DateTime.Parse(txtsdtanggal.Text).ToString("MMM-yyyy");
        }
        protected void txtdrtanggal_TextChanged(object sender, EventArgs e)
        {
            //if (DateTime.Parse(txtsdtanggal.Text) < DateTime.Parse(txtdrtanggal.Text) ||
            //    DateTime.Parse(txtdrtanggal.Text).ToString("MMM-yyyy") != DateTime.Parse(txtsdtanggal.Text).ToString("MMMyyyy"))
            //    txtsdtanggal.Text = Convert.ToDateTime(DateTime.Parse("1-" + (DateTime.Parse(txtdrtanggal.Text).AddMonths(1)).ToString("MMM-yyyy"))).AddDays(-1).ToString("dd")
            //        + "-" + DateTime.Parse(txtdrtanggal.Text).ToString("MMM") + "-" + DateTime.Now.ToString("yyyy");
        }
        protected void grvMergeHeader_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell = new TableCell();
                if (ddlBulan.SelectedIndex > 0)
                {
                    HeaderCell.Text = "No.";
                    HeaderCell.ColumnSpan = 1;
                    //HeaderCell.RowSpan = 2;
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    HeaderGridRow.Cells.Add(HeaderCell);
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Nama";
                    HeaderCell.ColumnSpan = 1;
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    //HeaderCell.RowSpan = 2;
                    HeaderGridRow.Cells.Add(HeaderCell);
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Jabatan";
                    HeaderCell.ColumnSpan = 1;
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    //HeaderCell.RowSpan = 2;
                    HeaderGridRow.Cells.Add(HeaderCell);
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "KPI";
                    HeaderCell.ColumnSpan = 7;
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    HeaderGridRow.Cells.Add(HeaderCell);
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "SOP";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    HeaderCell.ColumnSpan = 7;
                    HeaderGridRow.Cells.Add(HeaderCell);
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Task";
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    HeaderCell.ColumnSpan = 7;
                    HeaderGridRow.Cells.Add(HeaderCell);
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Disiplin";
                    HeaderCell.ColumnSpan = 7;
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    HeaderGridRow.Cells.Add(HeaderCell);
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Nilai";
                    HeaderCell.ColumnSpan = 1;
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    //HeaderCell.RowSpan = 2;
                    HeaderGridRow.Cells.Add(HeaderCell);
                }
                else
                {
                    HeaderCell.Text = "No.";
                    HeaderCell.ColumnSpan = 1;
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    //HeaderCell.RowSpan = 2;
                    HeaderGridRow.Cells.Add(HeaderCell);
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Nama";
                    HeaderCell.ColumnSpan = 1;
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    //HeaderCell.RowSpan = 2;
                    HeaderGridRow.Cells.Add(HeaderCell);
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Jabatan";
                    HeaderCell.ColumnSpan = 1;
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    //HeaderCell.RowSpan = 2;
                    HeaderGridRow.Cells.Add(HeaderCell);
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "KPI";
                    HeaderCell.ColumnSpan = 13;
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    HeaderGridRow.Cells.Add(HeaderCell);
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "SOP";
                    HeaderCell.ColumnSpan = 13;
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    HeaderGridRow.Cells.Add(HeaderCell);
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Task";
                    HeaderCell.ColumnSpan = 13;
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    HeaderGridRow.Cells.Add(HeaderCell);
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Disiplin";
                    HeaderCell.ColumnSpan = 13;
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    HeaderGridRow.Cells.Add(HeaderCell);
                    HeaderCell = new TableCell();
                    HeaderCell.Text = "Nilai";
                    HeaderCell.ColumnSpan = 3;
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                    //HeaderCell.RowSpan = 2;
                    HeaderGridRow.Cells.Add(HeaderCell);
                    HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                }
                GrdDynamic.Controls[0].Controls.AddAt(0, HeaderGridRow);
            }
        }
        protected void grv0MergeHeader_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.Header)
            //{
            //    GridView HeaderGrid = (GridView)sender;
            //    GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //    TableCell HeaderCell = new TableCell();
            //        HeaderCell.Text = "No.";
            //        //HeaderCell.ColumnSpan = 1;
            //        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            //        HeaderGridRow.Cells.Add(HeaderCell);
            //        HeaderCell = new TableCell();
            //        HeaderCell.Text = "Nama";
            //        //HeaderCell.ColumnSpan = 1;
            //        HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            //        HeaderGridRow.Cells.Add(HeaderCell);
            //        HeaderCell = new TableCell();
            //        HeaderCell.Text = "Jabatan";
            //        //HeaderCell.ColumnSpan = 1;
            //        HeaderCell.HorizontalAlign = HorizontalAlign.Left;
            //        HeaderGridRow.Cells.Add(HeaderCell);
            //        HeaderCell = new TableCell();
            //        HeaderCell.Text = "KPI";
            //        //HeaderCell.ColumnSpan = 1;
            //        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            //        HeaderGridRow.Cells.Add(HeaderCell);
            //        HeaderCell = new TableCell();
            //        HeaderCell.Text = "SOP";
            //        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            //        //HeaderCell.ColumnSpan = 1;
            //        HeaderGridRow.Cells.Add(HeaderCell);
            //        HeaderCell = new TableCell();
            //        HeaderCell.Text = "Task";
            //        HeaderCell.HorizontalAlign = HorizontalAlign.Center;
            //        //HeaderCell.ColumnSpan = 1;
            //        HeaderGridRow.Cells.Add(HeaderCell);
            //        HeaderCell = new TableCell();
            //        HeaderCell.Text = "Disiplin";
            //    GrdDynamic0.Controls[0].Controls.AddAt(0, HeaderGridRow);
            //}
        }
        protected void GridView1_DataBinding(object sender, EventArgs e)
        {

        }
        protected void GridView2_DataBinding(object sender, EventArgs e)
        {

        }
        protected void ddlBulan_Change(object sender, EventArgs e)
        {
            //ddlDept_Change(null, null);
        }
        protected void ddlTahun_Change(object sender, EventArgs e)
        {
            switch (((Users)Session["Users"]).UnitKerjaID)
            {
                case 1:
                    if (int.Parse(ddlTahun.SelectedValue) < 2017)
                    {
                        Response.Redirect("RekapPesNew2.aspx");
                    }
                    break;
                case 7:
                    if (int.Parse(ddlTahun.SelectedValue) < 2016)
                    {
                        Response.Redirect("RekapPesNew2.aspx");
                    }
                    break;
                default:
                    if (int.Parse(ddlTahun.SelectedValue) < 2017)
                    {
                        Response.Redirect("RekapPesNew2.aspx");
                    }
                    break;

            }
            LoadDept();
        }
    }
}