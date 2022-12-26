using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using System.Reflection;
using System.IO;

namespace GRCweb1.Modul.Factory
{
    public partial class LinkProduktifitasOutput : System.Web.UI.Page
    {
        private int iPageSize = 40;

        decimal Total = 0; int Total2 = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx"; RBP.Checked = true;
                Session["thnbln"] = "-"; Session["paging"] = "-";

                txtFromPostingPeriod.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");

                LoadBulan();
                LoadTahun();
                LoadLine();
            }

            Page.MaintainScrollPositionOnPostBack = true;
        }

        protected void rbBulan_CheckedChanged(object sender, EventArgs e)
        {
            periodebln.Visible = true; periodeharian.Visible = false;
            rbHarian.Checked = false; rbBulan.Checked = true;
            ddlGroup.Enabled = true;
        }

        protected void rbHarian_CheckedChanged(object sender, EventArgs e)
        {
            periodebln.Visible = false; periodeharian.Visible = true;
            rbBulan.Checked = false; rbHarian.Checked = true; ddlGroup.Enabled = false;
        }


        protected void LoadLine()
        {
            Users users = (Users)Session["Users"];
            ArrayList arrLine = new ArrayList();
            ProduktifitasOutput_Facade data = new ProduktifitasOutput_Facade();
            arrLine = data.RetrieveLine();
            ddlLine.Items.Clear();
            ddlLine.Items.Add(new ListItem(" -- Line --", "0"));
            foreach (ProduktifitasOutput Linee in arrLine)
            {
                ddlLine.Items.Add(new ListItem(Linee.Line.ToString(), Linee.ID.ToString()));
            }
        }

        protected void ddlLine_SelectedChange(object sender, EventArgs e)
        {
            ArrayList arrGroup = new ArrayList();
            ProduktifitasOutput_Facade dataG = new ProduktifitasOutput_Facade();
            arrGroup = dataG.RetrieveGroup(ddlLine.SelectedValue.ToString());
            ddlGroup.Items.Clear();
            ddlGroup.Items.Add(new ListItem(" -- Group --", "0"));
            foreach (ProduktifitasOutput Group in arrGroup)
            {
                ddlGroup.Items.Add(new ListItem(Group.Group.ToString(), Group.ID.ToString()));
            }
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            Session["Line"] = string.Empty; Session["thnbln"] = string.Empty; Session["GP"] = string.Empty;
            Session["paging"] = "preview";

            Users user = (Users)Session["Users"];
            string tahun = ddlTahun.SelectedItem.ToString();
            string bulan = (ddlBulan.SelectedValue.ToString()).Length == 1 ? "0" + ddlBulan.SelectedValue.ToString().Trim() : ddlBulan.SelectedValue.ToString().Trim();
            string Line = ddlLine.SelectedItem.ToString().Trim(); Session["Line"] = Line.Trim();
            string thnbln = tahun.Trim() + bulan.Trim();
            string Group = ddlGroup.SelectedItem.ToString().Trim(); Session["GP"] = Group.Trim();

            string PeriodeTgl = DateTime.Parse(txtFromPostingPeriod.Text).ToString("yyyy-MM-dd");
            string thnbln2 = DateTime.Parse(txtFromPostingPeriod.Text).ToString("yyyyMM");
            string flag = string.Empty; string thnbulan = string.Empty;

            if (RBP.Checked == true)
            {
                if (rbHarian.Checked == true)
                {
                    flag = "1"; thnbulan = thnbln2; Session["thnbln"] = thnbulan;
                }
                else
                {
                    flag = "2"; thnbulan = thnbln; Session["thnbln"] = thnbulan;
                }

                LoadDataProduktifitas(thnbulan, user.UnitKerjaID.ToString(), Line, Group, flag, PeriodeTgl);
                PanelOutPut.Visible = false;
            }
            else
            {
                if (rbHarian.Checked == true)
                {
                    flag = "1"; thnbulan = thnbln2; Session["thnbln"] = thnbulan;
                }
                else
                {
                    flag = "2"; thnbulan = thnbln; Session["thnbln"] = thnbulan;
                }

                LoadDataOutput(thnbulan, user.UnitKerjaID.ToString(), Line, Group, flag, PeriodeTgl);
                PanelProduktifitas.Visible = false;
            }
        }

        protected void LoadBulan()
        {
            ArrayList arrBulan = new ArrayList();
            ProduktifitasOutput_Facade data = new ProduktifitasOutput_Facade();
            arrBulan = data.RetrieveBulan();
            ddlBulan.Items.Clear();
            ddlBulan.Items.Add(new ListItem("-- Bulan --", DateTime.Now.Month.ToString()));
            foreach (ProduktifitasOutput bulan in arrBulan)
            {
                ddlBulan.Items.Add(new ListItem(bulan.BulanNama, bulan.Bulan));
            }
        }
        protected void LoadTahun()
        {
            ArrayList arrTahun = new ArrayList();
            ProduktifitasOutput_Facade data = new ProduktifitasOutput_Facade();
            arrTahun = data.RetrieveTahun();
            ddlTahun.Items.Clear();
            ddlTahun.Items.Add(new ListItem(DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString()));
            foreach (ProduktifitasOutput tahun in arrTahun)
            {
                ddlTahun.Items.Add(new ListItem(tahun.Tahun, tahun.Tahun));
            }
        }

        private void LoadDataOutput(string thnbln, string UnitKerjaID, string Line, string GP, string flag, string PeriodeTgl)
        {
            string kueri = string.Empty;

            //if (flag == "1")
            //{
            //    kueri = " where B.tgl='" + PeriodeTgl + "' ";
            //}
            //else
            //{
            //    kueri = " where GP='" + GP + "' ";
            //}

            if (flag == "1")
            {
                kueri = " where B.tgl='" + PeriodeTgl + "' ) as x order by GP ";
            }
            else
            {
                kueri = " where GP='" + GP + "' ) as x order by tgl,deskripsi ";
            }

            if (Session["paging"].ToString() == "preview")
            {
                ViewState["PageNumber"] = null;
            }

            ProduktifitasOutput_Facade f1 = new ProduktifitasOutput_Facade();
            ArrayList arrData1 = new ArrayList(); string query = string.Empty;
            query =
            "select Tgl,Line,Deskripsi,GP,WaktuMenit WaktuMenit2,BDT_Sch,BDT_NonSch,WaktuMenit-BDT_Sch-BDT_NonSch WaktuMenit,ttl from ( " +
            "select  Tgl,Line,Deskripsi,GP,[Waktu/(Menit)]WaktuMenit, " +
            "isnull((select BDT_Sch from Temp_OutPutProduktifitas_BDT A where A.Tgl=B.Tgl and A.Deskripsi=B.Deskripsi and A.GP=B.GP and A.Noted='O' and A.RowStatus>-1),0)BDT_Sch, " +
            "isnull((select BDT_NonSch from Temp_OutPutProduktifitas_BDT A where A.Tgl=B.Tgl and A.Deskripsi=B.Deskripsi and A.GP=B.GP and A.Noted='O' and A.RowStatus>-1),0)BDT_NonSch " +
            ",(select count(B2.Tgl) from LastData_P B2 where B2.Tgl=B.Tgl and B2.GP=B.GP and B2.Line=B.Line group by B2.Tgl)ttl " +
            "from LastData_P B " + kueri + " ";

            arrData1 = f1.RetrieveData(thnbln, UnitKerjaID, Line, GP, query);
            lst3.DataSource = arrData1;
            lst3.DataBind();

            PagedDataSource pdsData = new PagedDataSource();
            pdsData.DataSource = arrData1;
            pdsData.AllowPaging = true;
            pdsData.PageSize = iPageSize;

            if (ViewState["PageNumber"] != null)
                pdsData.CurrentPageIndex = Convert.ToInt32(ViewState["PageNumber"]);
            else
                pdsData.CurrentPageIndex = 0;

            lblIndex1.Text = pdsData.CurrentPageIndex.ToString();

            if (pdsData.PageCount - 1 > 0)
            {
                Repeater1.Visible = true;
                ArrayList alPages = new ArrayList();
                for (int i = 0; i <= pdsData.PageCount - 1; i++)
                    alPages.Add((i).ToString());
                Repeater2.DataSource = alPages;
                Repeater2.DataBind();
            }
            else
            {
                Repeater2.Visible = false;
            }
            //lst3.DataSource = pdsData;
            //lst3.DataBind();  
        }

        private void LoadDataProduktifitas(string thnbln, string UnitKerjaID, string Line, string GP, string flag, string PeriodeTgl)
        {
            string kueri = string.Empty;
            if (flag == "1")
            {
                kueri = " where B.tgl='" + PeriodeTgl + "' ) as x order by GP ";
            }
            else
            {
                kueri = " where GP='" + GP + "' ) as x order by tgl,deskripsi ";
            }

            if (Session["paging"].ToString() == "preview")
            {
                ViewState["PageNumber"] = null;
            }

            ProduktifitasOutput_Facade f0 = new ProduktifitasOutput_Facade();
            ArrayList arrData = new ArrayList(); string query = string.Empty;
            query =
           "select Tgl,Line,Deskripsi,GP,isnull(WaktuMenit,0) WaktuMenit2,isnull(BDT_Sch,0)BDT_Sch,isnull(BDT_NonSch,0)BDT_NonSch,isnull(WaktuMenit-BDT_Sch,0) WaktuMenit,ttl from ( " +
           "select  Tgl,Line,Deskripsi,GP,[Waktu/(Menit)]WaktuMenit, " +
           "isnull((select BDT_Sch from Temp_OutPutProduktifitas_BDT A where A.Tgl=B.Tgl and A.Deskripsi=B.Deskripsi and A.GP=B.GP and A.Noted='P' and A.RowStatus>-1),0)BDT_Sch, " +
           "isnull((select BDT_NonSch from Temp_OutPutProduktifitas_BDT A where A.Tgl=B.Tgl and A.Deskripsi=B.Deskripsi and A.GP=B.GP and A.Noted='O' and A.RowStatus>-1),0)BDT_NonSch " +
           ",(select count(B2.Tgl) from LastData_P B2 where B2.Tgl=B.Tgl and B2.GP=B.GP and B2.Line=B.Line group by B2.Tgl)ttl " +
           "from LastData_P B " + kueri + " ";

            arrData = f0.RetrieveData(thnbln, UnitKerjaID, Line, GP, query);
            lst.DataSource = arrData;
            lst.DataBind();

            //PagedDataSource pdsData = new PagedDataSource();
            //pdsData.DataSource = arrData;
            //pdsData.AllowPaging = true;
            //pdsData.PageSize = iPageSize;

            //if (ViewState["PageNumber"] != null)
            //    pdsData.CurrentPageIndex = Convert.ToInt32(ViewState["PageNumber"]);
            //else
            //    pdsData.CurrentPageIndex = 0;

            //lblIndex.Text = pdsData.CurrentPageIndex.ToString();

            //if (pdsData.PageCount-1 > 1)
            //{
            //    Repeater1.Visible = true;
            //    ArrayList alPages = new ArrayList();
            //    for (int i = 0; i <= pdsData.PageCount-1; i++)
            //        alPages.Add((i).ToString());
            //    Repeater1.DataSource = alPages;
            //    Repeater1.DataBind();
            //}
            //else
            //{
            //    Repeater1.Visible = false;
            //}

            //lst.DataSource = pdsData;
            //lst.DataBind();  


        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Session["paging"] = "paging";
            ViewState["PageNumber"] = Convert.ToInt32(e.CommandArgument);
            Session["Line"] = string.Empty; Session["thnbln"] = string.Empty; Session["GP"] = string.Empty;

            Users user = (Users)Session["Users"];
            string tahun = ddlTahun.SelectedItem.ToString();
            string bulan = (ddlBulan.SelectedValue.ToString()).Length == 1 ? "0" + ddlBulan.SelectedValue.ToString().Trim() : ddlBulan.SelectedValue.ToString().Trim();
            string Line = ddlLine.SelectedItem.ToString().Trim(); Session["Line"] = Line.Trim();
            string thnbln = tahun.Trim() + bulan.Trim(); Session["thnbln"] = thnbln;
            string Group = ddlGroup.SelectedItem.ToString().Trim(); Session["GP"] = Group.Trim();

            //LoadDataProduktifitas(thnbln, user.UnitKerjaID.ToString(), Line, Group);
        }

        protected void Repeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ViewState["PageNumber"] = Convert.ToInt32(e.CommandArgument);
            Session["paging"] = "paging";
            Session["Line"] = string.Empty; Session["thnbln"] = string.Empty; Session["GP"] = string.Empty;

            Users user = (Users)Session["Users"];
            string tahun = ddlTahun.SelectedItem.ToString();
            string bulan = (ddlBulan.SelectedValue.ToString()).Length == 1 ? "0" + ddlBulan.SelectedValue.ToString().Trim() : ddlBulan.SelectedValue.ToString().Trim();
            string Line = ddlLine.SelectedItem.ToString().Trim(); Session["Line"] = Line.Trim();
            string thnbln = tahun.Trim() + bulan.Trim(); Session["thnbln"] = thnbln;
            string Group = ddlGroup.SelectedItem.ToString().Trim(); Session["GP"] = Group.Trim();

            //LoadDataOutput(thnbln, user.UnitKerjaID.ToString(), Line, Group);
        }

        protected void lst3_DataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Users user = (Users)Session["Users"];
                ProduktifitasOutput gm = (ProduktifitasOutput)e.Item.DataItem;
                DropDownList ddlBDT_Sch = (DropDownList)e.Item.FindControl("ddlSch");
                DropDownList ddlBDT_NonSch = (DropDownList)e.Item.FindControl("ddlNonSch");
                Image del = (Image)e.Item.FindControl("del2");
                Image edit = (Image)e.Item.FindControl("edit2");

                string thnbln = Session["thnbln"].ToString().Trim();
                string query = string.Empty; string query1 = string.Empty;

                /** BDT Schedulle **/
                if (gm.ttl > 1)
                {
                    query =
                    " select TglBreak,GP,BDT_Sch,'0'BDT_NonSch from ( " +
                    " select * from ( " +
                    " select TglBreak,Line,cast(BDNPMS_S as decimal(18,0)) BDT_Sch,RIGHT(TRIM(Syarat),2)GP,StartBD,FinishBD from TempBreakDown1_P0 ) as x " +
                    " group by TglBreak,Line,BDT_Sch,GP,StartBD,FinishBD " +
                    " ) as xx where BDT_Sch>0 and GP like'%" + gm.GP.Trim() + "%' and TglBreak='" + gm.Tgl + "' ";
                }
                else
                {
                    query =
                    " select TglBreak,GP,sum(BDT_Sch)BDT_Sch,'0'BDT_NonSch from ( " +
                    " select * from ( " +
                    " select TglBreak,Line,cast(BDNPMS_S as decimal(18,0)) BDT_Sch,RIGHT(TRIM(Syarat),2)GP,StartBD,FinishBD from TempBreakDown1_P0 ) as x " +
                    " group by TglBreak,Line,BDT_Sch,GP,StartBD,FinishBD " +
                    " ) as xx where BDT_Sch>0 and GP like'%" + gm.GP.Trim() + "%' and TglBreak='" + gm.Tgl + "' " +
                    "  group by TglBreak,GP ";
                }

                ProduktifitasOutput_Facade f1 = new ProduktifitasOutput_Facade();
                ArrayList arrData = f1.RetrieveBDTSch(gm.Tgl, gm.GP.Trim(), gm.Line.Trim(), user.UnitKerjaID.ToString(), thnbln, query);
                if (arrData.Count > 0)
                {
                    ddlBDT_Sch.Items.Clear();
                    ddlBDT_Sch.Items.Add(new ListItem("- BDT Sch -", "0"));
                    foreach (ProduktifitasOutput gm1 in arrData)
                    {
                        ddlBDT_Sch.Items.Add(new ListItem(gm1.BDT_Sch.ToString(), gm1.BDT_Sch.ToString()));
                    }

                    if (gm.ttl == 1)
                    {
                        /** Insert Data **/
                        ZetroView zv = new ZetroView();
                        zv.QueryType = Operation.CUSTOM;
                        string GP = string.Empty; string TglBreak = string.Empty; decimal BDT_Sch = 0;
                        zv.CustomQuery =

                        " select TglBreak,GP,sum(BDT_Sch)BDT_Sch,'0'BDT_NonSch from (  select * from " +
                        " (select TglBreak,Line,cast(BDNPMS_S as decimal(18,0)) BDT_Sch,RIGHT(TRIM(Syarat),2)GP,StartBD,FinishBD from TempBreakDown1_P0 ) as x  " +
                        " group by TglBreak,Line,BDT_Sch,GP,StartBD,FinishBD  ) as xx " +
                        " where BDT_Sch>0 and GP like'%" + gm.GP.Trim() + "%' and TglBreak='" + gm.Tgl + "' " +
                        " group by TglBreak,GP " +

                        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBreakBMPO_P0]') AND type in (N'U')) DROP TABLE [dbo].[tempBreakBMPO_P0]  " +
                        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempBreakDown1_P0]') AND type in (N'U')) DROP TABLE [dbo].[TempBreakDown1_P0] ";

                        SqlDataReader dr = zv.Retrieve();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                GP = dr["GP"].ToString().Trim();
                                TglBreak = dr["TglBreak"].ToString().Trim();
                                BDT_Sch = Convert.ToDecimal(dr["BDT_Sch"].ToString());
                            }
                        }

                        Users users = (Users)Session["Users"];
                        int RowNum = e.Item.ItemIndex;
                        Label Deskripsi = (Label)e.Item.FindControl("txtDeskripsi2");

                        if (BDT_Sch > 0)
                        {
                            ProduktifitasOutput gm3 = new ProduktifitasOutput();
                            ProduktifitasOutput_Facade fgm3 = new ProduktifitasOutput_Facade();

                            gm3.Deskripsi = Deskripsi.Text.Trim();
                            gm3.Tgl = TglBreak.ToString();
                            gm3.BDT_Sch = Convert.ToInt32(BDT_Sch);
                            gm3.BDT_NonSch = 0;
                            gm3.GP = GP.ToString();
                            gm3.Noted = "O";

                            int intResult = 0;
                            ZetroView zv1 = new ZetroView();
                            zv.QueryType = Operation.CUSTOM;
                            zv.CustomQuery =
                            " select ID from Temp_OutPutProduktifitas_BDT " +
                            " where tgl='" + gm.Tgl + "' and GP='" + gm.GP.Trim() + "' and RowStatus>-1 and Noted='O' ";
                            SqlDataReader dr1 = zv.Retrieve();
                            if (dr1.HasRows)
                            {
                                while (dr1.Read())
                                {
                                    ID = dr1["ID"].ToString().Trim();
                                }

                                ZetroView zv2 = new ZetroView();
                                zv.QueryType = Operation.CUSTOM;
                                zv.CustomQuery =
                                " update Temp_OutPutProduktifitas_BDT set BDT_Sch='" + Convert.ToInt32(BDT_Sch) + "' where  " +
                                " ID='" + ID + "' ";

                                SqlDataReader dr2 = zv.Retrieve();
                            }
                            else
                            {
                                intResult = fgm3.InsertBDT_Sch(gm3);
                            }

                        }
                    }
                    /** End Insert Data **/
                }

                ddlBDT_Sch.Enabled = false;
                /** End Part BDT Sch **/

                /** BDT Non-Schedulle **/
                if (gm.ttl > 1)
                {
                    query1 =
                    " select TglBreak,GP,'0'BDT_Sch,cast(BDT_NonSch as decimal(18,0))BDT_NonSch from ( " +
                    " select TglBreak,Line,right(TRIM(Syarat),2)GP,(BDNPMS_M+BDNPMS_E+BDNPMS_U+BDNPMS_L+BDNPMS_G1+BDNPMS_G2+BDNPMS_G3+BDNPMS_G4)BDT_NonSch " +
                    " from TempBreakDown1_P0) as x where BDT_NonSch>0 and GP like'%" + gm.GP.Trim() + "%' and TglBreak='" + gm.Tgl + "' ";
                }
                else
                {
                    query1 =
                    " select TglBreak,GP,'0'BDT_Sch,cast(sum(BDT_NonSch) as decimal(18,0))BDT_NonSch from ( " +
                    " select TglBreak,Line,right(TRIM(Syarat),2)GP,(BDNPMS_M+BDNPMS_E+BDNPMS_U+BDNPMS_L+BDNPMS_G1+BDNPMS_G2+BDNPMS_G3+BDNPMS_G4)BDT_NonSch " +
                    " from TempBreakDown1_P0) as x where BDT_NonSch>0 and GP like'%" + gm.GP.Trim() + "%' and TglBreak='" + gm.Tgl + "' " +
                    " group by TglBreak,GP ";
                }

                /* BDT Non Schedulle **/
                ProduktifitasOutput_Facade f2 = new ProduktifitasOutput_Facade();
                ArrayList arrData2 = f1.RetrieveBDTSch(gm.Tgl, gm.GP.Trim(), gm.Line.Trim(), user.UnitKerjaID.ToString(), thnbln, query1);

                if (arrData2.Count > 0)
                {
                    ddlBDT_NonSch.Items.Clear();
                    ddlBDT_NonSch.Items.Add(new ListItem("- BDT NonSch -", "0"));
                    foreach (ProduktifitasOutput gm1 in arrData2)
                    {
                        ddlBDT_NonSch.Items.Add(new ListItem(gm1.BDT_NonSch.ToString(), gm1.BDT_NonSch.ToString()));
                    }

                    if (gm.ttl == 1)
                    {
                        /** Insert Data **/
                        ZetroView zv = new ZetroView();
                        zv.QueryType = Operation.CUSTOM;
                        string GP = string.Empty; string TglBreak = string.Empty; decimal BDT_NonSch = 0;
                        zv.CustomQuery =

                        " select TglBreak,GP,'0'BDT_Sch,cast(sum(BDT_NonSch) as decimal(18,0))BDT_NonSch from ( " +
                        " select TglBreak,Line,right(TRIM(Syarat),2)GP,(BDNPMS_M+BDNPMS_E+BDNPMS_U+BDNPMS_L+BDNPMS_G1+BDNPMS_G2+BDNPMS_G3+BDNPMS_G4)BDT_NonSch " +
                        " from TempBreakDown1_P0) as x where BDT_NonSch>0 and GP like'%" + gm.GP.Trim() + "%' and TglBreak='" + gm.Tgl + "' " +
                        " group by TglBreak,GP " +

                        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBreakBMPO_P0]') AND type in (N'U')) DROP TABLE [dbo].[tempBreakBMPO_P0]  " +
                        " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempBreakDown1_P0]') AND type in (N'U')) DROP TABLE [dbo].[TempBreakDown1_P0] ";

                        SqlDataReader dr = zv.Retrieve();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                GP = dr["GP"].ToString().Trim();
                                TglBreak = dr["TglBreak"].ToString().Trim();
                                BDT_NonSch = Convert.ToDecimal(dr["BDT_NonSch"].ToString());
                            }
                        }

                        Users users = (Users)Session["Users"];
                        int RowNum = e.Item.ItemIndex;
                        Label Deskripsi = (Label)e.Item.FindControl("txtDeskripsi2");

                        if (BDT_NonSch > 0)
                        {
                            ProduktifitasOutput gm3 = new ProduktifitasOutput();
                            ProduktifitasOutput_Facade fgm3 = new ProduktifitasOutput_Facade();

                            gm3.Deskripsi = Deskripsi.Text.Trim();
                            gm3.Tgl = TglBreak.ToString();
                            gm3.BDT_NonSch = Convert.ToInt32(BDT_NonSch);
                            gm3.BDT_Sch = 0;
                            gm3.GP = GP.ToString();
                            gm3.Noted = "O";

                            int intResult = 0;
                            ZetroView zv1 = new ZetroView();
                            zv.QueryType = Operation.CUSTOM;
                            zv.CustomQuery =
                            " select ID from Temp_OutPutProduktifitas_BDT " +
                            " where tgl='" + gm.Tgl + "' and GP='" + gm.GP.Trim() + "' and RowStatus>-1 ";
                            SqlDataReader dr1 = zv.Retrieve();
                            if (dr1.HasRows)
                            {
                                while (dr1.Read())
                                {
                                    ID = dr1["ID"].ToString().Trim();
                                }

                                ZetroView zv2 = new ZetroView();
                                zv.QueryType = Operation.CUSTOM;
                                zv.CustomQuery =
                                " update Temp_OutPutProduktifitas_BDT set BDT_NonSch='" + Convert.ToInt32(BDT_NonSch) + "' where  " +
                                " ID='" + ID + "' ";

                                SqlDataReader dr2 = zv.Retrieve();
                            }
                            else
                            {
                                intResult = fgm3.InsertBDT_Sch(gm3);
                            }

                        }
                    }
                    /** End Insert Data **/
                }

                ddlBDT_NonSch.Enabled = false;
            }



            ProduktifitasOutput p = (ProduktifitasOutput)e.Item.DataItem;
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Image del = (Image)e.Item.FindControl("del2");
                    Image edit = (Image)e.Item.FindControl("edit2");
                    DropDownList ddlBDT_Sch = (DropDownList)e.Item.FindControl("ddlSch");
                    DropDownList ddlBDT_NonSch = (DropDownList)e.Item.FindControl("ddlNonSch");

                    HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("lst4");
                    if (tr != null)
                    {
                        Total += p.WaktuMenit2 - p.BDT_NonSch - p.BDT_Sch;

                        if (p.BDT_Sch == 0 && p.BDT_NonSch == 0)
                        {
                            del.Visible = false;
                        }
                        else if (p.BDT_Sch == 0 && p.BDT_NonSch > 0 || p.BDT_Sch > 0 && p.BDT_NonSch == 0 || p.BDT_Sch > 0 && p.BDT_NonSch > 0)
                        {
                            del.Visible = true;
                        }

                        if (ddlBDT_Sch.Text == "" && ddlBDT_NonSch.Text == "0")
                        {
                            edit.Visible = true;
                        }
                        else if (ddlBDT_Sch.Text == "" && ddlBDT_NonSch.Text == "")
                        {
                            edit.Visible = false; del.Visible = false;
                        }

                    }
                }
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    HtmlTableRow tr2 = (HtmlTableRow)e.Item.FindControl("ftr2");
                    tr2.Cells[1].InnerText = Total.ToString("N0");
                }
            }
            catch (Exception ex)
            {
                DisplayAJAXMessage(this, ex.Message);
            }
        }

        protected void lst_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ProduktifitasOutput gm = (ProduktifitasOutput)e.Item.DataItem;
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("lst2");
                    if (tr != null)
                    {
                        Image del = (Image)e.Item.FindControl("del");

                        Total2 += gm.WaktuMenit2 - gm.BDT_Sch;

                        if (gm.BDT_Sch == 0)
                        {
                            del.Visible = false;
                        }
                        else
                        {
                            del.Visible = true;
                        }

                        Users user = (Users)Session["Users"];
                        //ProduktifitasOutput gm = (ProduktifitasOutput)e.Item.DataItem;           
                        DropDownList ddl = (DropDownList)e.Item.FindControl("ddlGP");
                        //Image del = (Image)e.Item.FindControl("del");
                        Label Ket = (Label)e.Item.FindControl("txtKet");
                        Image edit = (Image)e.Item.FindControl("edit");
                        string thnbln = Session["thnbln"].ToString().Trim();
                        string query = string.Empty;

                        if (gm.ttl > 1)
                        {
                            query =
                            " select TglBreak,GP,BDT_Sch,'0'BDT_NonSch from ( " +
                            " select * from ( " +
                            " select TglBreak,Line,cast(BDNPMS_S as decimal(18,0)) BDT_Sch,RIGHT(TRIM(Syarat),2)GP,StartBD,FinishBD from TempBreakDown1_P0) as x " +
                            " group by TglBreak,Line,BDT_Sch,GP,StartBD,FinishBD " +
                            " ) as xx where BDT_Sch>0 and GP like'%" + gm.GP.Trim() + "%' and TglBreak='" + gm.Tgl + "' ";
                        }
                        else
                        {
                            query =
                            " select TglBreak,GP,sum(BDT_Sch)BDT_Sch,'0'BDT_NonSch from ( " +
                            " select * from ( " +
                            " select TglBreak,Line,cast(BDNPMS_S as decimal(18,0)) BDT_Sch,RIGHT(TRIM(Syarat),2)GP,StartBD,FinishBD from TempBreakDown1_P0) as x " +
                            " group by TglBreak,Line,BDT_Sch,GP,StartBD,FinishBD " +
                            " ) as xx where BDT_Sch>0 and GP like'%" + gm.GP.Trim() + "%' and TglBreak='" + gm.Tgl + "' " +
                            "  group by TglBreak,GP ";
                        }

                        ProduktifitasOutput_Facade f1 = new ProduktifitasOutput_Facade();
                        ArrayList arrData = f1.RetrieveBDTSch(gm.Tgl, gm.GP.Trim(), gm.Line.Trim(), user.UnitKerjaID.ToString(), thnbln, query);
                        if (arrData.Count > 0)
                        {
                            ddl.Items.Clear();
                            ddl.Items.Add(new ListItem("- BDT Sch -", "0"));
                            foreach (ProduktifitasOutput gm1 in arrData)
                            {
                                ddl.Items.Add(new ListItem(gm1.BDT_Sch.ToString(), gm1.BDT_Sch.ToString()));
                            }

                            if (gm.ttl == 1)
                            {
                                /** Insert Data **/
                                ZetroView zv = new ZetroView();
                                zv.QueryType = Operation.CUSTOM;
                                string GP = string.Empty; string TglBreak = string.Empty; decimal BDT_Sch = 0;
                                zv.CustomQuery =

                                " select TglBreak,GP,sum(BDT_Sch)BDT_Sch,'0'BDT_NonSch from ( " +
                                " select * from ( " +
                                " select TglBreak,Line,cast(BDNPMS_S as decimal(18,0)) BDT_Sch,RIGHT(TRIM(Syarat),2)GP,StartBD,FinishBD from TempBreakDown1_P0) as x " +
                                " group by TglBreak,Line,BDT_Sch,GP,StartBD,FinishBD " +
                                " ) as xx where BDT_Sch>0 and GP like'%" + gm.GP.Trim() + "%' and TglBreak='" + gm.Tgl + "' " +
                                "  group by TglBreak,GP " +

                                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBreakBMPO_P0]') AND type in (N'U')) DROP TABLE [dbo].[tempBreakBMPO_P0]  " +
                                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempBreakDown1_P0]') AND type in (N'U')) DROP TABLE [dbo].[TempBreakDown1_P0] ";

                                SqlDataReader dr = zv.Retrieve();
                                if (dr.HasRows)
                                {
                                    while (dr.Read())
                                    {
                                        GP = dr["GP"].ToString().Trim();
                                        TglBreak = dr["TglBreak"].ToString().Trim();
                                        BDT_Sch = Convert.ToDecimal(dr["BDT_Sch"].ToString());
                                    }
                                }

                                Users users = (Users)Session["Users"];
                                int RowNum = e.Item.ItemIndex;
                                Label Deskripsi = (Label)e.Item.FindControl("txtDeskripsi");

                                if (BDT_Sch > 0)
                                {
                                    ProduktifitasOutput gm3 = new ProduktifitasOutput();
                                    ProduktifitasOutput_Facade fgm3 = new ProduktifitasOutput_Facade();

                                    gm3.Deskripsi = Deskripsi.Text.Trim();
                                    gm3.Tgl = TglBreak.ToString();
                                    gm3.BDT_Sch = Convert.ToInt32(BDT_Sch);
                                    gm3.BDT_NonSch = 0;
                                    gm3.GP = GP.ToString();
                                    gm3.Noted = "P";

                                    int intResult = 0;
                                    ZetroView zv1 = new ZetroView();
                                    zv.QueryType = Operation.CUSTOM;
                                    zv.CustomQuery =
                                    " select ID from Temp_OutPutProduktifitas_BDT " +
                                    " where tgl='" + gm.Tgl + "' and GP='" + gm.GP.Trim() + "' and RowStatus>-1 and Noted='P' ";
                                    SqlDataReader dr1 = zv.Retrieve();
                                    if (dr1.HasRows)
                                    {
                                        while (dr1.Read())
                                        {
                                            ID = dr1["ID"].ToString().Trim();
                                        }

                                        ZetroView zv2 = new ZetroView();
                                        zv.QueryType = Operation.CUSTOM;
                                        zv.CustomQuery =
                                        " update Temp_OutPutProduktifitas_BDT set BDT_Sch='" + Convert.ToInt32(BDT_Sch) + "' where  " +
                                        " ID='" + ID + "' ";

                                        SqlDataReader dr2 = zv.Retrieve();
                                    }
                                    else
                                    {
                                        intResult = fgm3.InsertBDT_Sch(gm3);
                                    }

                                }
                            }
                            /** End Insert Data **/
                        }
                        else
                        {
                            edit.Visible = false; del.Visible = false;
                        }
                        ddl.Enabled = false;
                    }
                }

                if (e.Item.ItemType == ListItemType.Footer)
                {
                    HtmlTableRow tr2 = (HtmlTableRow)e.Item.FindControl("ftr");
                    tr2.Cells[1].InnerText = Total2.ToString("N0");
                    string A = string.Empty;
                }
            }
            catch { }
            //ProduktifitasOutput p = (ProduktifitasOutput)e.Item.DataItem;
            //try
            //{
            //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            //    {
            //        Image del = (Image)e.Item.FindControl("del");

            //        HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("lst2");
            //        if (tr != null)
            //        {
            //            Total2 += p.WaktuMenit - p.BDT_Sch;
            //            //Total2 += Convert.ToInt32(p.Tgl);
            //            //string A = Convert.ToDateTime(p.Tgl).ToString("yyyyMMdd");

            //            if (p.BDT_Sch == 0)
            //            {
            //                del.Visible = false;
            //            }
            //            else
            //            {
            //                del.Visible = true;
            //            }
            //        }

            //    }
            //if (e.Item.ItemType == ListItemType.Footer)
            //{
            //    HtmlTableRow tr2 = (HtmlTableRow)e.Item.FindControl("ftr");
            //    tr2.Cells[1].InnerText = Total2.ToString("N0");
            //    string A = string.Empty;
            //}
            //}





            //catch (Exception ex)
            //{
            //    DisplayAJAXMessage(this, ex.Message);
            //}           
        }


        protected void lst3_Command(object sender, RepeaterCommandEventArgs e)
        {
            Users users = (Users)Session["Users"];
            int RowNum = e.Item.ItemIndex;

            Image Simpan = (Image)lst3.Items[RowNum].FindControl("simpan2");
            Image edit = (Image)lst3.Items[RowNum].FindControl("edit2");
            DropDownList ddlBDT_Sch = (DropDownList)lst3.Items[RowNum].FindControl("ddlSch");
            DropDownList ddlBDT_NonSch = (DropDownList)lst3.Items[RowNum].FindControl("ddlNonSch");
            Label Deskripsi = (Label)e.Item.FindControl("txtDeskripsi2");
            Label GP = (Label)e.Item.FindControl("txtGP2");
            Label BDT_Sch = (Label)e.Item.FindControl("txtBDT_Sch2");
            Label BDT_NonSch = (Label)e.Item.FindControl("txtBDT_NonSch2");
            Label Tgl = (Label)e.Item.FindControl("txtTgl2");

            string A = ddlBDT_Sch.Text;
            string B = ddlBDT_NonSch.Text;

            switch (e.CommandName)
            {
                case "Edit2":
                    Simpan.Visible = true;
                    edit.Visible = false;
                    ddlBDT_Sch.Enabled = true; ddlBDT_NonSch.Enabled = true;

                    if (ddlBDT_Sch.Text == "") { ddlBDT_Sch.Enabled = false; }
                    if (ddlBDT_NonSch.Text == "") { ddlBDT_NonSch.Enabled = false; }
                    if (ddlBDT_Sch.Text == "0") { ddlBDT_Sch.Enabled = true; }
                    if (ddlBDT_NonSch.Text == "0") { ddlBDT_NonSch.Enabled = true; }

                    break;

                case "Save2":
                    ProduktifitasOutput gm3 = new ProduktifitasOutput();
                    ProduktifitasOutput_Facade fgm3 = new ProduktifitasOutput_Facade();

                    gm3.Deskripsi = Deskripsi.Text.Trim();
                    gm3.Tgl = Tgl.Text.Trim();
                    if (ddlBDT_Sch.Text == "")
                    {
                        gm3.BDT_Sch = 0;
                    }
                    else
                    {
                        gm3.BDT_Sch = Convert.ToInt32(ddlBDT_Sch.SelectedValue);
                    }

                    if (ddlBDT_NonSch.Text == "")
                    {
                        gm3.BDT_NonSch = 0;
                    }
                    else
                    {
                        gm3.BDT_NonSch = Convert.ToInt32(ddlBDT_NonSch.SelectedValue);
                    }

                    gm3.GP = GP.Text.Trim();
                    gm3.Noted = "O";

                    int intResult = 0;
                    intResult = fgm3.InsertBDT_Sch(gm3);
                    if (intResult > -1)
                    {
                        //LoadDataOutput(Session["thnbln"].ToString(), users.UnitKerjaID.ToString(), Session["Line"].ToString(), Session["GP"].ToString());
                        DisplayAJAXMessage(this, " Berhasil Simpan !! ");
                    }
                    else
                    {
                        DisplayAJAXMessage(this, " Gagal simpan !! "); return;
                    }

                    break;

                case "del2":
                    ProduktifitasOutput gm40 = new ProduktifitasOutput();
                    ProduktifitasOutput_Facade fgm40 = new ProduktifitasOutput_Facade();

                    gm40.Deskripsi = Deskripsi.Text.Trim();
                    gm40.Tgl = Tgl.Text.Trim();
                    gm40.GP = GP.Text.Trim();
                    gm40.Noted = "O";

                    int intHasil1 = 0;

                    intHasil1 = fgm40.HapusData(gm40);

                    if (intHasil1 > -1)
                    {
                        //LoadDataOutput(Session["thnbln"].ToString(), users.UnitKerjaID.ToString(), Session["Line"].ToString(), Session["GP"].ToString());
                        DisplayAJAXMessage(this, " Berhasil Simpan !! ");
                    }
                    else
                    {
                        DisplayAJAXMessage(this, " Gagal Hapus !! "); return;
                    }


                    break;
            }
        }


        protected void lst_Command(object sender, RepeaterCommandEventArgs e)
        {
            Users users = (Users)Session["Users"];
            int RowNum = e.Item.ItemIndex;
            Image Simpan = (Image)lst.Items[RowNum].FindControl("simpan");
            Image img = (Image)lst.Items[RowNum].FindControl("edit");
            DropDownList ddl = (DropDownList)lst.Items[RowNum].FindControl("ddlGP");
            Label Deskripsi = (Label)e.Item.FindControl("txtDeskripsi");
            Label GP = (Label)e.Item.FindControl("txtGP");
            Label BDT_Sch = (Label)e.Item.FindControl("txtBDT_Sch");
            Label Tgl = (Label)e.Item.FindControl("txtTgl");

            switch (e.CommandName)
            {
                case "Edit":
                    Simpan.Visible = true;
                    img.Visible = false;
                    ddl.Enabled = true;
                    break;

                case "Save":
                    ProduktifitasOutput gm2 = new ProduktifitasOutput();
                    ProduktifitasOutput_Facade fgm2 = new ProduktifitasOutput_Facade();

                    gm2.Deskripsi = Deskripsi.Text.Trim();
                    gm2.Tgl = Tgl.Text.Trim();
                    gm2.BDT_Sch = Convert.ToInt32(ddl.SelectedValue);
                    gm2.GP = GP.Text.Trim();
                    gm2.Noted = "P";
                    gm2.BDT_NonSch = 0;
                    int intResult = 0;
                    intResult = fgm2.InsertBDT_Sch(gm2);
                    if (intResult > -1)
                    {
                        //LoadDataProduktifitas(Session["thnbln"].ToString(), users.UnitKerjaID.ToString(), Session["Line"].ToString(), Session["GP"].ToString());
                        DisplayAJAXMessage(this, " Berhasil di simpan !! ");
                    }
                    else
                    {
                        DisplayAJAXMessage(this, " Gagal simpan !! "); return;
                    }

                    break;

                case "del":
                    ProduktifitasOutput gm4 = new ProduktifitasOutput();
                    ProduktifitasOutput_Facade fgm4 = new ProduktifitasOutput_Facade();

                    gm4.Deskripsi = Deskripsi.Text.Trim();
                    gm4.Tgl = Tgl.Text.Trim();
                    gm4.GP = GP.Text.Trim();
                    gm4.Noted = "P";

                    int intHasil1 = 0;

                    intHasil1 = fgm4.HapusData(gm4);
                    if (intHasil1 > -1)
                    {
                        //LoadDataProduktifitas(Session["thnbln"].ToString(), users.UnitKerjaID.ToString(), Session["Line"].ToString(), Session["GP"].ToString());
                        DisplayAJAXMessage(this, " Berhasil di simpan !! ");
                    }
                    else
                    {
                        DisplayAJAXMessage(this, " Gagal Hapus !! "); return;
                    }


                    break;
            }
        }

        protected void ddlGP_SelectedIndexChanged(object sender, EventArgs e)
        { }

        protected void ddlSch_SelectedIndexChanged(object sender, EventArgs e)
        { }

        protected void ddlNonSch_SelectedIndexChanged(object sender, EventArgs e)
        { }

        protected void RBO_CheckedChanged(object sender, EventArgs e)
        {
            if (RBO.Checked == true) { RBP.Checked = false; }
            PanelOutPut.Visible = true; PanelProduktifitas.Visible = false;
            //LoadData(Session["Periode"].ToString(), Session["Periode2"].ToString());
        }

        protected void RBP_CheckedChanged(object sender, EventArgs e)
        {
            if (RBP.Checked == true)
            { RBO.Checked = false; }
            PanelOutPut.Visible = false; PanelProduktifitas.Visible = true;
            //LoadData(Session["Periode"].ToString(), Session["Periode2"].ToString());
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void ddlBulan_SelectedChange(object sender, EventArgs e)
        { }

        protected void ddlTahun_SelectedChange(object sender, EventArgs e)
        { }

        public class ProduktifitasOutput : GRCBaseDomain
        {
            public int ttl { get; set; }
            private DateTime tglinput = DateTime.Now.Date;
            public decimal Stok { get; set; }
            public string Partno { get; set; }
            public string Periode { get; set; }
            public string Tgl { get; set; }
            public int WaktuMenit { get; set; }
            public int WaktuMenit2 { get; set; }
            public string Line { get; set; }
            public string GP { get; set; }
            public string Tahun { get; set; }
            public string Bulan { get; set; }
            public string BulanNama { get; set; }
            public string Deskripsi { get; set; }
            public string No { get; set; }
            public string Ket { get; set; }
            public string Group { get; set; }
            public int ID { get; set; }
            public int BDT_Sch { get; set; }
            public int BDT_NonSch { get; set; }
            public string Noted { get; set; }
        }

        public class ProduktifitasOutput_Facade
        {
            private ArrayList arrData = new ArrayList();
            private ProduktifitasOutput objDF = new ProduktifitasOutput();
            public string strError = string.Empty;
            private List<SqlParameter> sqlListParam;

            public string UserID { get; set; }
            public string Bulan { get; set; }
            public string Tahun { get; set; }
            public string Field { get; set; }
            public string Where { get; set; }
            public string Bagian { get; set; }
            public string Criteria { get; set; }

            public int Update(object objDomain)
            {
                try
                {
                    objDF = (ProduktifitasOutput)objDomain;
                    sqlListParam = new List<SqlParameter>();
                    sqlListParam.Add(new SqlParameter("@Partno", objDF.Partno));
                    sqlListParam.Add(new SqlParameter("@Line", objDF.Line));
                    sqlListParam.Add(new SqlParameter("@Periode", objDF.Periode));
                    sqlListParam.Add(new SqlParameter("@CreatedBy", objDF.CreatedBy));
                    //sqlListParam.Add(new SqlParameter("@LineBefore", objDF.LineBefore));

                    DataAccess da = new DataAccess(Global.ConnectionString());
                    int result = da.ProcessData(sqlListParam, "update_partno2line");
                    strError = da.Error;
                    return result;
                }
                catch (Exception ex)
                {
                    strError = ex.Message;
                    return -1;
                }

            }

            public int InsertBDT_Sch(object objDomain)
            {
                try
                {
                    objDF = (ProduktifitasOutput)objDomain;
                    sqlListParam = new List<SqlParameter>();
                    sqlListParam.Add(new SqlParameter("@Deskripsi", objDF.Deskripsi));
                    sqlListParam.Add(new SqlParameter("@GP", objDF.GP));
                    sqlListParam.Add(new SqlParameter("@Tgl", objDF.Tgl));
                    sqlListParam.Add(new SqlParameter("@BDT_Sch", objDF.BDT_Sch));
                    sqlListParam.Add(new SqlParameter("@Noted", objDF.Noted));
                    sqlListParam.Add(new SqlParameter("@BDT_NonSch", objDF.BDT_NonSch));

                    DataAccess da = new DataAccess(Global.ConnectionString());
                    int result = da.ProcessData(sqlListParam, "insert_BDT_Sch");
                    strError = da.Error;
                    return result;
                }
                catch (Exception ex)
                {
                    strError = ex.Message;
                    return -1;
                }

            }

            public int HapusData(object objDomain)
            {
                try
                {
                    objDF = (ProduktifitasOutput)objDomain;
                    sqlListParam = new List<SqlParameter>();
                    sqlListParam.Add(new SqlParameter("@Deskripsi", objDF.Deskripsi));
                    sqlListParam.Add(new SqlParameter("@GP", objDF.GP));
                    sqlListParam.Add(new SqlParameter("@Tgl", objDF.Tgl));
                    sqlListParam.Add(new SqlParameter("@Noted", objDF.Noted));

                    DataAccess da = new DataAccess(Global.ConnectionString());
                    int result = da.ProcessData(sqlListParam, "hapus_BDT_Sch");
                    strError = da.Error;
                    return result;
                }
                catch (Exception ex)
                {
                    strError = ex.Message;
                    return -1;
                }

            }

            public ArrayList RetrieveLine()
            {
                ArrayList arrData = new ArrayList();
                string strSQL =
                "  select ID,PlantName Line from BM_Plant ";
                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(strSQL);
                if (da.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new ProduktifitasOutput
                        {
                            Line = sdr["Line"].ToString(),
                            ID = Convert.ToInt32(sdr["ID"])
                        });
                    }
                }
                return arrData;
            }

            public ArrayList RetrieveGroup(string PlantID)
            {
                ArrayList arrData = new ArrayList();
                string strSQL =
                "  select * from BM_PlantGroup where PlantID=" + PlantID + " order by [group]";
                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(strSQL);
                if (da.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new ProduktifitasOutput
                        {
                            Group = sdr["Group"].ToString(),
                            ID = Convert.ToInt32(sdr["ID"])
                        });
                    }
                }
                return arrData;
            }

            public ArrayList RetrieveBDTSch(string Tgl, string GP, string Line, string UnitKerjaID, string thnbln, string query)
            {
                //string thn = Tgl.Substring(1,4);string bln = Tgl.Substring(6,2);
                //string thnbln = thn.Trim()+bln.Trim();

                ArrayList arrData = new ArrayList();
                string strSQL =
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBreakBMPO_P0]') AND type in (N'U')) DROP TABLE [dbo].[tempBreakBMPO_P0]  " +
                " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempBreakDown1_P0]') AND type in (N'U')) DROP TABLE [dbo].[TempBreakDown1_P0] " +

                " declare @thnbln  varchar(6) " +
                " declare @line varchar(max) " +
                " declare @unitkerjaID varchar(6) " +

                " set @thnbln='" + thnbln + "' " +
                " set @line='" + Line + "' " +
                " set @unitkerjaID='" + UnitKerjaID + "' " +

                "SELECT * into tempBreakBMPO_P0 From( " +
                "select line, left(convert(char,TglBreak,23),10)TglBreak,TTLPS,RowStatus,convert(varchar,StartBD,108)as StartBD, " +
                "convert(varchar,FinishBD,108) as FinishBD ,convert(varchar,FinaltyBD,108) as FinaltyBD,Syarat,  480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) , " +
                "cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT1 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP1,  " +
                "480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) ,cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT2  " +
                "and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP2,  480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) ,cast (FinaltyBD as datetime )))from BreakBM  " +
                "where RIGHT( rtrim (Syarat),2) = B.GPAT3 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP3,  480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) , " +
                "cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT4 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP4,  case when Pinalti=0  " +
                "then BDNPMS_M else (BDNPMS_M * pinalti /100) end as BDNPMS_M, case when Pinalti=0 then BDNPMS_E else (BDNPMS_E * Pinalti/100) end as BDNPMS_E, case when Pinalti=0  " +
                "then BDNPMS_U else (BDNPMS_U * Pinalti/100) end as BDNPMS_U,  case when Pinalti=0 then BDNPMS_G1 else (BDNPMS_G1 * Pinalti/100) end as BDNPMS_G1,  " +
                "case when Pinalti=0 then BDNPMS_G2 else (BDNPMS_G2 * Pinalti/100) end as BDNPMS_G2,  case when Pinalti=0 then BDNPMS_G3 else (BDNPMS_G3 * Pinalti/100) end as BDNPMS_G3,  " +
                "case when Pinalti=0 then BDNPMS_G4 else (BDNPMS_G4 * Pinalti/100) end as BDNPMS_G4,  case when Pinalti=0 then BDNPMS_L else (BDNPMS_L * Pinalti/100) end as BDNPMS_L,   " +
                "case when Pinalti=0 then BDNPMS_S else (BDNPMS_S * Pinalti/100) end as BDNPMS_S,Ket,Pinalti,DP,DIC,GroupOff  from (   " +
                "select (select PlanName from MasterPlan where ID=A.BM_PlantID) as line, TglBreak,RowStatus,  1440-isnull(  (  select sum(DATEDIFF(Minute,StartBD ,FinaltyBD))   " +
                "from BreakBM  where breakbm_masterchargeID=4 and BM_PlantID=A.BM_PlantID and BreakBM.TglBreak=A.TglBreak  ),0) as TTLPS,  StartBD,FinishBD,FinaltyBD,Syarat,0 as GP1, " +
                "0 as GP2,0 as GP3,0 as GP4, case when SUBSTRING(Syarat,1,1)='M' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_M,  case when SUBSTRING(Syarat,1,1)='E' and LEN(Syarat)>2  " +
                "then menit else 0 end  BDNPMS_E,  case when SUBSTRING(Syarat,1,1)='U' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_U,  case when SUBSTRING(Syarat,1,2)=(select top 1 [group]  " +
                "from  (select top 4 * from BM_PlantGroupbr  where PlantID =A.BM_PlantID and  LEN([group])>1 order by [group] desc ) as Gr order by [group]) and LEN(Syarat)=2 then menit else 0  " +
                "end  BDNPMS_G1,  case when SUBSTRING(Syarat,1,2)=(select top 1 [group] from  (select top 3 * from BM_PlantGroupbr  where PlantID =A.BM_PlantID and  LEN([group])>1  " +
                "order by [group] desc ) as Gr order by [group]) and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G2, case when SUBSTRING(Syarat,1,2)=(select top 1 [group] from  ( " +
                "select top 2 * from BM_PlantGroupbr  where PlantID =A.BM_PlantID and  LEN([group])>1 order by [group] desc ) as Gr order by [group]) and LEN(Syarat)=2 then menit else 0  " +
                "end  BDNPMS_G3, case when SUBSTRING(Syarat,1,2)=(select top 1 [group] from  (select top 1 * from BM_PlantGroupbr where PlantID =A.BM_PlantID and  LEN([group])>1  " +
                "order by [group] desc ) as Gr order by [group])  and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G4, case when SUBSTRING(Syarat,1,1)='L' and LEN(Syarat)>2 then menit else 0 end   " +
                "BDNPMS_L,case when SUBSTRING(Syarat,1,2)='KH' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_S,Ket, CAST(Pinalti as decimal(18,2))Pinalti, (select lokasiproblem  " +
                "from breakbmproblem where ID=A.Breakbm_masterproblemID) as DP,  case when LEN(Syarat)=2 then 'BoardMill' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='L' then 'Logistik'  " +
                "when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='E' then 'Elektrik' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,2)='KH' then'Schedule'  when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='M'  " +
                "then 'Mekanik' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='U' then 'Utility' end DIC,  (select top 1 [group] from (select top 4 * from BM_PlantGroupbr  " +
                "where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT1, (select top 1 [group] from (select top 3 * from BM_PlantGroupbr  " +
                "where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT2,  (select top 1 [group] from (select top 2 * from BM_PlantGroupbr  " +
                "where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT3,  (select top 1 [group] from (select top 1 * from BM_PlantGroupbr  " +
                "where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT4 ,GroupOff  from(  select  isnull(xx.minutex,0) as menit,*  " +
                "from BreakBM   left join(select x.IDs, DATEDIFF(minute,sbd,finbd) minutex,sbd,finbd from(  select d.ID as IDs, Convert(datetime,tglbreak+startbd) as sbd,StartBD,  " +
                "Case when Cast(startBD as int)>=23 and CAST(FinaltyBD as int)<=1 then convert(datetime,DATEADD(day,1,tglbreak)+FinaltyBD) else Convert(datetime,TglBreak+FinishBD) end finbd, " +
                "FinaltyBD,TglBreak  from BreakBM as d where d.RowStatus='0'  )as x  ) as xx on xx.IDs=BreakBM.ID  ) as A  )  as B  " +
                "where left(convert(char,TglBreak,112),6)=@thnbln  and DP is not null and RowStatus>-1  and line =@line " +
                ") BM order by TglBreak " +

                "declare @gp1 varchar(max),@gp2 varchar(max),@gp3 varchar(max),@gp4 varchar(max) " +
                "declare @K char " +
                "set @K=(select rtrim(kodelokasi) from company where depoid=@unitkerjaID) " +

                "if @line like '%1%' begin set @gp1=@K+'A' set @gp2=@K+'B' set @gp3=@K+'C' set @gp4=@K+'D' end " +
                "if @line like '%2%' begin set @gp1=@K+'E' set @gp2=@K+'F' set @gp3=@K+'G' set @gp4=@K+'H' end " +
                "if @line like '%3%' begin set @gp1=@K+'I' set @gp2=@K+'J' set @gp3=@K+'K' set @gp4=@K+'L' end " +
                "if @line like '%4%' begin set @gp1=@K+'M' set @gp2=@K+'N' set @gp3=@K+'O' set @gp4=@K+'P' end " +
                "if @line like '%5%' begin set @gp1=@K+'Q' set @gp2=@K+'R' set @gp3=@K+'S' set @gp4=@K+'T' end " +
                "if @line like '%6%' begin set @gp1=@K+'U' set @gp2=@K+'V' set @gp3=@K+'W' set @gp4=@K+'X' end " +

                "SELECT line,left(convert(char,TglBreak,23),10)TglBreak,TTLPS,StartBD,FinishBD,FinaltyBD,syarat,GroupOff,RowStatus, " +
                "CASE WHEN GroupOff=@gp1 OR GroupOff=@gp1 OR GroupOff=@gp1 OR GroupOff=@gp1 OR GroupOff=@gp1 OR GroupOff=@gp1 THEN (GP1-480) " +
                "ELSE GP1 END GP1, " +

                "CASE WHEN GroupOff=@gp2 OR GroupOff=@gp2 OR GroupOff=@gp2 OR GroupOff=@gp2 OR GroupOff=@gp2 OR GroupOff=@gp2 " +
                "THEN (GP2-480) ELSE GP2  END GP2, " +

                "CASE WHEN GroupOff=@gp3 OR GroupOff=@gp3 OR GroupOff=@gp3 OR GroupOff=@gp3 OR GroupOff=@gp3 " +
                "OR GroupOff=@gp3 THEN (GP3-480) ELSE GP3  END GP3, " +

                "CASE WHEN GroupOff=@gp4 OR GroupOff=@gp4 OR GroupOff=@gp4 OR GroupOff=@gp4 " +
                "OR GroupOff=@gp4 OR GroupOff=@gp4 " +

                "THEN (GP4-480) ELSE GP4 END GP4 ,  BDNPMS_M,BDNPMS_E,BDNPMS_U,BDNPMS_G1,BDNPMS_G2,BDNPMS_G3, " +
                "BDNPMS_G4,BDNPMS_L,Pinalti,  BDNPMS_S,Ket,DP,DIC into TempBreakDown1_P0 " +
                "From tempBreakBMPO_P0 where RowStatus=0 order by TglBreak,StartBD,line  " +

                //" select * from ( " +
                //" select * from ( " +
                //" select TglBreak,Line,BDNPMS_S BDT_Sch,RIGHT(Syarat,2)GP,StartBD,FinishBD from TempBreakDown1_P0 where Syarat like'%" + GP + "%' ) as x " +
                //" group by TglBreak,Line,BDT_Sch,GP,StartBD,FinishBD " +
                //" ) as xx where BDT_Sch>0 and TglBreak='" + Tgl + "' " +

                //"  select TglBreak,GP,sum(BDT_Sch)BDT_Sch,BDT_NonSch from ( " +
                //" " + query + " and TglBreak='" + Tgl + "' " +
                //"  ) as xx group by TglBreak,GP,BDT_NonSch " +
                " " + query + " ";

                //" IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBreakBMPO_P0]') AND type in (N'U')) DROP TABLE [dbo].[tempBreakBMPO_P0]  " +
                //" IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempBreakDown1_P0]') AND type in (N'U')) DROP TABLE [dbo].[TempBreakDown1_P0] ";



                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(strSQL);
                if (da.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new ProduktifitasOutput
                        {
                            BDT_Sch = Convert.ToInt32(sdr["BDT_Sch"]),
                            BDT_NonSch = Convert.ToInt32(sdr["BDT_NonSch"])
                        });
                    }
                }
                return arrData;
            }

            public ArrayList RetrieveBulan()
            {
                arrData = new ArrayList();
                string strSQL =
                " select Bulan,case " +
                " when Bulan=1 then 'JANUARI' " +
                " when Bulan=2 then 'FEBRUARI' " +
                " when Bulan=3 then 'MARET' " +
                " when Bulan=4 then 'APRIL' " +
                " when Bulan=5 then 'MEI' " +
                " when Bulan=6 then 'JUNI' " +
                " when Bulan=7 then 'JULI' " +
                " when Bulan=8 then 'AGUSTUS' " +
                " when Bulan=9 then 'SEPTEMBER' " +
                " when Bulan=10 then 'OKTOBER' " +
                " when Bulan=11 then 'NOVEMBER' " +
                " when Bulan=12 then 'DESEMBER' " +
                " end BulanNama from " +
                " (select DISTINCT(MONTH(CreatedTime))Bulan from SPP where LEFT(convert(char,createdtime,112),6)>='201810') as Data1 order by Bulan";
                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(strSQL);

                if (da.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new ProduktifitasOutput
                        {
                            Bulan = sdr["Bulan"].ToString(),
                            BulanNama = sdr["BulanNama"].ToString()
                        });
                    }
                }
                return arrData;
            }

            public ArrayList RetrieveTahun()
            {
                arrData = new ArrayList();
                string strSQL =
                " select DISTINCT(YEAR(CreatedTime))Tahun from SPP where LEFT(convert(char,createdtime,112),6)>='201810' ";
                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(strSQL);

                if (da.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new ProduktifitasOutput
                        {
                            Tahun = sdr["Tahun"].ToString()
                        });
                    }
                }
                return arrData;
            }

            public ArrayList RetrieveData(string thnbln, string UnitKerjaID, string Line, string GP, string query)
            {
                //arrData = new ArrayList();
                string Query = string.Empty;

                if (UnitKerjaID == "1")
                {
                    Query = " and A.Line=B.Line ";
                }
                else
                {
                    Query = "";
                }

                string strSQL =
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBreakBMPO_P]') AND type in (N'U')) DROP TABLE [dbo].[tempBreakBMPO_P]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDMS0_P]') AND type in (N'U')) DROP TABLE [dbo].[tempBDMS0_P]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDMS_P]') AND type in (N'U')) DROP TABLE [dbo].[tempBDMS_P]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OuputProduksiPO_P]') AND type in (N'U')) DROP TABLE [dbo].[OuputProduksiPO_P] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDT1_P]') AND type in (N'U')) DROP TABLE [dbo].[tempBDT1_P]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDT2_P]') AND type in (N'U')) DROP TABLE [dbo].[tempBDT2_P]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDT3_P]') AND type in (N'U')) DROP TABLE [dbo].[tempBDT3_P] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDTP_P]') AND type in (N'U')) DROP TABLE [dbo].[tempBDTP_P] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDTO_P]') AND type in (N'U')) DROP TABLE [dbo].[tempBDTO_P] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempBreakDown1_P]') AND type in (N'U')) DROP TABLE [dbo].[TempBreakDown1_P] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bdt1_P]') AND type in (N'U')) DROP TABLE [dbo].[Bdt1_P]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bdt2_P]') AND type in (N'U')) DROP TABLE [dbo].[Bdt2_P]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[compare1_P]') AND type in (N'U')) DROP TABLE [dbo].[compare1_P] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[compare2_P]') AND type in (N'U')) DROP TABLE [dbo].[compare2_P] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataBdt1_P]') AND type in (N'U')) DROP TABLE [dbo].[tempDataBdt1_P]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataBdt01_P]') AND type in (N'U')) DROP TABLE [dbo].[tempDataBdt01_P]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataBdt2_P]') AND type in (N'U')) DROP TABLE [dbo].[tempDataBdt2_P]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataBdt3_P]') AND type in (N'U')) DROP TABLE [dbo].[tempDataBdt3_P]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataBdt4_P]') AND type in (N'U')) DROP TABLE [dbo].[tempDataBdt4_P] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataBdt5_P]') AND type in (N'U')) DROP TABLE [dbo].[tempDataBdt5_P] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataBdt05_P]') AND type in (N'U')) DROP TABLE [dbo].[tempDataBdt05_P] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataBdt6_P]') AND type in (N'U')) DROP TABLE [dbo].[tempDataBdt6_P] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataBdt06_P]') AND type in (N'U')) DROP TABLE [dbo].[tempDataBdt06_P] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData_P]') AND type in (N'U')) DROP TABLE [dbo].[LastData_P] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData2_P]') AND type in (N'U')) DROP TABLE [dbo].[LastData2_P] " +

                " " +
                "declare @thnbln  varchar(6) " +
                "declare @line varchar(max) " +
                "declare @unitkerjaID varchar(6) " +
                "set @thnbln='" + thnbln + "' " +
                "set @line='" + Line + "' " +
                "set @unitkerjaID='" + UnitKerjaID + "' " +
                " " +
                "update bm_bdtProduktifitas set rowstatus=-1 where rowstatus>-1 and ThnBln=" + thnbln + "  and line=@line " +
                "SELECT * into tempBreakBMPO_P From( " +

                "select line, left(convert(char,TglBreak,23),10)TglBreak,TTLPS,RowStatus,convert(varchar,StartBD,108)as StartBD, " +
                "convert(varchar,FinishBD,108) as FinishBD ,convert(varchar,FinaltyBD,108) as FinaltyBD,Syarat,  480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) , " +
                "cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT1 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP1,  " +
                "480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) ,cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT2  " +
                "and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP2,  480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) ,cast (FinaltyBD as datetime )))from BreakBM  " +
                "where RIGHT( rtrim (Syarat),2) = B.GPAT3 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP3,  480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) , " +
                "cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT4 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP4,  case when Pinalti=0  " +
                "then BDNPMS_M else (BDNPMS_M * pinalti /100) end as BDNPMS_M, case when Pinalti=0 then BDNPMS_E else (BDNPMS_E * Pinalti/100) end as BDNPMS_E, case when Pinalti=0  " +
                "then BDNPMS_U else (BDNPMS_U * Pinalti/100) end as BDNPMS_U,  case when Pinalti=0 then BDNPMS_G1 else (BDNPMS_G1 * Pinalti/100) end as BDNPMS_G1,  " +
                "case when Pinalti=0 then BDNPMS_G2 else (BDNPMS_G2 * Pinalti/100) end as BDNPMS_G2,  case when Pinalti=0 then BDNPMS_G3 else (BDNPMS_G3 * Pinalti/100) end as BDNPMS_G3,  " +
                "case when Pinalti=0 then BDNPMS_G4 else (BDNPMS_G4 * Pinalti/100) end as BDNPMS_G4,  case when Pinalti=0 then BDNPMS_L else (BDNPMS_L * Pinalti/100) end as BDNPMS_L,   " +
                "case when Pinalti=0 then BDNPMS_S else (BDNPMS_S * Pinalti/100) end as BDNPMS_S,Ket,Pinalti,DP,DIC,GroupOff  from (   " +
                "select (select PlanName from MasterPlan where ID=A.BM_PlantID) as line, TglBreak,RowStatus,  1440-isnull(  (  select sum(DATEDIFF(Minute,StartBD ,FinaltyBD))   " +
                "from BreakBM  where breakbm_masterchargeID=4 and BM_PlantID=A.BM_PlantID and BreakBM.TglBreak=A.TglBreak  ),0) as TTLPS,  StartBD,FinishBD,FinaltyBD,Syarat,0 as GP1, " +
                "0 as GP2,0 as GP3,0 as GP4, case when SUBSTRING(Syarat,1,1)='M' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_M,  case when SUBSTRING(Syarat,1,1)='E' and LEN(Syarat)>2  " +
                "then menit else 0 end  BDNPMS_E,  case when SUBSTRING(Syarat,1,1)='U' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_U,  case when SUBSTRING(Syarat,1,2)=(select top 1 [group]  " +
                "from  (select top 4 * from BM_PlantGroupbr  where PlantID =A.BM_PlantID and  LEN([group])>1 order by [group] desc ) as Gr order by [group]) and LEN(Syarat)=2 then menit else 0  " +
                "end  BDNPMS_G1,  case when SUBSTRING(Syarat,1,2)=(select top 1 [group] from  (select top 3 * from BM_PlantGroupbr  where PlantID =A.BM_PlantID and  LEN([group])>1  " +
                "order by [group] desc ) as Gr order by [group]) and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G2, case when SUBSTRING(Syarat,1,2)=(select top 1 [group] from  ( " +
                "select top 2 * from BM_PlantGroupbr  where PlantID =A.BM_PlantID and  LEN([group])>1 order by [group] desc ) as Gr order by [group]) and LEN(Syarat)=2 then menit else 0  " +
                "end  BDNPMS_G3, case when SUBSTRING(Syarat,1,2)=(select top 1 [group] from  (select top 1 * from BM_PlantGroupbr where PlantID =A.BM_PlantID and  LEN([group])>1  " +
                "order by [group] desc ) as Gr order by [group])  and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G4, case when SUBSTRING(Syarat,1,1)='L' and LEN(Syarat)>2 then menit else 0 end   " +
                "BDNPMS_L,case when SUBSTRING(Syarat,1,2)='KH' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_S,Ket, CAST(Pinalti as decimal(18,2))Pinalti, (select lokasiproblem  " +
                "from breakbmproblem where ID=A.Breakbm_masterproblemID) as DP,  case when LEN(Syarat)=2 then 'BoardMill' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='L' then 'Logistik'  " +
                "when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='E' then 'Elektrik' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,2)='KH' then'Schedule'  when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='M'  " +
                "then 'Mekanik' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='U' then 'Utility' end DIC,  (select top 1 [group] from (select top 4 * from BM_PlantGroupbr  " +
                "where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT1, (select top 1 [group] from (select top 3 * from BM_PlantGroupbr  " +
                "where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT2,  (select top 1 [group] from (select top 2 * from BM_PlantGroupbr  " +
                "where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT3,  (select top 1 [group] from (select top 1 * from BM_PlantGroupbr  " +
                "where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT4 ,GroupOff  from(  select  isnull(xx.minutex,0) as menit,*  " +
                "from BreakBM   left join(select x.IDs, DATEDIFF(minute,sbd,finbd) minutex,sbd,finbd from(  select d.ID as IDs, Convert(datetime,tglbreak+startbd) as sbd,StartBD,  " +
                "Case when Cast(startBD as int)>=23 and CAST(FinaltyBD as int)<=1 then convert(datetime,DATEADD(day,1,tglbreak)+FinaltyBD) else Convert(datetime,TglBreak+FinishBD) end finbd, " +
                "FinaltyBD,TglBreak  from BreakBM as d where d.RowStatus='0'  )as x  ) as xx on xx.IDs=BreakBM.ID  ) as A  )  as B  " +
                "where left(convert(char,TglBreak,112),6)=@thnbln  and DP is not null and RowStatus>-1  and line =@line " +
                ") BM order by TglBreak " +

                "declare @gp1 varchar(max),@gp2 varchar(max),@gp3 varchar(max),@gp4 varchar(max) " +
                "declare @K char " +
                "set @K=(select rtrim(kodelokasi) from company where depoid=@unitkerjaID) " +

                "if @line like '%1%' begin set @gp1=@K+'A' set @gp2=@K+'B' set @gp3=@K+'C' set @gp4=@K+'D' end " +
                "if @line like '%2%' begin set @gp1=@K+'E' set @gp2=@K+'F' set @gp3=@K+'G' set @gp4=@K+'H' end " +
                "if @line like '%3%' begin set @gp1=@K+'I' set @gp2=@K+'J' set @gp3=@K+'K' set @gp4=@K+'L' end " +
                "if @line like '%4%' begin set @gp1=@K+'M' set @gp2=@K+'N' set @gp3=@K+'O' set @gp4=@K+'P' end " +
                "if @line like '%5%' begin set @gp1=@K+'Q' set @gp2=@K+'R' set @gp3=@K+'S' set @gp4=@K+'T' end " +
                "if @line like '%6%' begin set @gp1=@K+'U' set @gp2=@K+'V' set @gp3=@K+'W' set @gp4=@K+'X' end " +

                "SELECT line,left(convert(char,TglBreak,23),10)TglBreak,TTLPS,StartBD,FinishBD,FinaltyBD,syarat,GroupOff,RowStatus, " +
                "CASE WHEN GroupOff=@gp1 OR GroupOff=@gp1 OR GroupOff=@gp1 OR GroupOff=@gp1 OR GroupOff=@gp1 OR GroupOff=@gp1 THEN (GP1-480) " +
                "ELSE GP1 END GP1, " +

                "CASE WHEN GroupOff=@gp2 OR GroupOff=@gp2 OR GroupOff=@gp2 OR GroupOff=@gp2 OR GroupOff=@gp2 OR GroupOff=@gp2 " +
                "THEN (GP2-480) ELSE GP2  END GP2, " +

                "CASE WHEN GroupOff=@gp3 OR GroupOff=@gp3 OR GroupOff=@gp3 OR GroupOff=@gp3 OR GroupOff=@gp3 " +
                "OR GroupOff=@gp3 THEN (GP3-480) ELSE GP3  END GP3, " +

                "CASE WHEN GroupOff=@gp4 OR GroupOff=@gp4 OR GroupOff=@gp4 OR GroupOff=@gp4 " +
                "OR GroupOff=@gp4 OR GroupOff=@gp4 " +

                "THEN (GP4-480) ELSE GP4 END GP4 ,  BDNPMS_M,BDNPMS_E,BDNPMS_U,BDNPMS_G1,BDNPMS_G2,BDNPMS_G3, " +
                "BDNPMS_G4,BDNPMS_L,Pinalti,  BDNPMS_S,Ket,DP,DIC into TempBreakDown1_P " +
                "From tempBreakBMPO_P where RowStatus=0 order by TglBreak,StartBD,line  " +

                #region Temp Table Bdt1_P
            "select TglProduksi,Line,GP,Kubik,Tebal,Lebar,Panjang,drJam,sdJam,Prod_Mulai,Prod_Done,[shift] into Bdt1_P from ( " +

                "select *, " +
                "case  " +
                //"when shift=1 and drJam='07:00:00' then DATEDIFF(MINUTE,drJam2,TglProduksi+' '+'07:00:00')  "+
                //"when shift=1 and drJam>'07:00:00' then DATEDIFF(MINUTE,TglProduksi+' '+'07:00:00',drJam2)  "+
                //"when shift=2 and drJam='15:00:00' then DATEDIFF(MINUTE,drJam2,TglProduksi+' '+'15:00:00') "+
                //"when shift=2 and drJam>'15:00:00' then DATEDIFF(MINUTE,TglProduksi+' '+'15:00:00',drJam2)  "+
                //"when shift=3 and drJam='23:00:00' then DATEDIFF(MINUTE,drJam2,TglProduksi+' '+'23:00:00') "+ 
                //"when shift=3 and H1<>H2 then DATEDIFF(MINUTE,TglProduksi+' '+'23:00:00',drJam2) "+
                //"when shift=3 and H1=H2 and drJam>='23:00:00' and drJam<='23:59:59' then DATEDIFF(MINUTE,TglProduksi+' '+'23:00:00',drJam2) "+
                ////"when shift=3 and H1=H2 and drJam>'00:00:00' and drJam<'06:59:00' then DATEDIFF(MINUTE,SUBSTRING(TglProduksi,1,8)+trim(cast((H2) as nchar))+' '+'00:00:00',drJam2) "+
                //"when shift=3 and H1=H2 and drJam>='00:00:00' then DATEDIFF(MINUTE,SUBSTRING(TglProduksi,1,8)+trim(cast((H1) as nchar))+' '+'00:00:00',SUBSTRING(TglProduksi,1,8)+trim(cast((H1) as nchar))+' '+drJam) + 60  "+                        

                "when shift=1 and drJam='07:00:00' then DATEDIFF(MINUTE,drJam2,TglProduksi+' '+'07:00:00') " +
                "when shift=1 and drJam>'07:00:00' then DATEDIFF(MINUTE,TglProduksi+' '+'07:00:00',drJam2) " +
                "when shift=2 and drJam='15:00:00' then DATEDIFF(MINUTE,drJam2,TglProduksi+' '+'15:00:00') " +
                "when shift=2 and drJam>'15:00:00' then DATEDIFF(MINUTE,TglProduksi+' '+'15:00:00',drJam2)  " +
                "when shift=3 and drJam='23:00:00' then DATEDIFF(MINUTE,drJam2,TglProduksi+' '+'23:00:00') " +
                "when shift=3 and H1<>H2 then DATEDIFF(MINUTE,TglProduksi+' '+'23:00:00',drJam2) " +
                "when shift=3 and H1=H2 and drJam>='23:00:00' and drJam<='23:59:59' then DATEDIFF(MINUTE,TglProduksi+' '+'23:00:00',drJam2) " +
                "when shift=3 and H1=H2 and drJam>='00:00:00' then DATEDIFF(MINUTE,SUBSTRING(TglProduksi,1,8)+trim(cast((H1) as nchar))+' '+'00:00:00',SUBSTRING(TglProduksi,1,8)+trim(cast((H1) as nchar))+' '+drJam) + 60 " +

                "end Prod_Mulai, " +

                "case " +

                //"when shift=1 and drJam>='07:00:00' then DATEDIFF(MINUTE,TglProduksi+' '+'07:00:00',sdJam2) " +
                //"when shift=2 and drJam>='15:00:00' then DATEDIFF(MINUTE,TglProduksi+' '+'15:00:00',sdJam2) " +
                //"when shift=3 and H1=H2 and drJam>='23:00:00' and sdJam<='00:00:00' then DATEDIFF(MINUTE,TglProduksi+' '+'23:00:00',sdJam2) " +
                //"when shift=3 and H1=H2 and sdJam>'00:00:00' and sdJam<'06:59:00' then DATEDIFF(MINUTE,SUBSTRING(TglProduksi,1,8)+trim(cast((H2) as nchar))+' '+'00:00:00',sdJam2) " +
                //"when shift=3  and sdJam='06:59:00' or shift=3  and sdJam='07:00:00' then '480' " +
                //"when shift=3 and H1<>H2 then DATEDIFF(MINUTE,SUBSTRING(TglProduksi,1,8)+trim(cast((H2) as nchar))+' '+'00:00:00',sdJam2) " +
                //"when shift=3 and H1=H2 and drJam>='23:00:00' and sdJam<='23:59:59' then DATEDIFF(MINUTE,TglProduksi+' '+'23:00:00',sdJam2)  " +            

                "when shift=1 and drJam>='07:00:00' then DATEDIFF(MINUTE,TglProduksi+' '+'07:00:00',sdJam2) " +
                "when shift=2 and drJam>='15:00:00' then DATEDIFF(MINUTE,TglProduksi+' '+'15:00:00',sdJam2) " +
                "when shift=3 and H1=H2 and drJam>='23:00:00' and sdJam<='00:00:00' then DATEDIFF(MINUTE,TglProduksi+' '+'23:00:00',sdJam2) " +

                "when shift=3 and H1=H2 and sdJam>'00:00:00' and sdJam<'06:59:00' then  " +
                "DATEDIFF(MINUTE,SUBSTRING(TglProduksi,1,8)+trim(cast((H2) as nchar))+' '+drJam,sdJam2) " +
                "+ DATEDIFF(MINUTE,SUBSTRING(TglProduksi,1,8)+trim(cast((H1) as nchar))+' '+'23:00:00',SUBSTRING(TglProduksi,1,8)+trim(cast((H1) as nchar))+' '+'23:59:59')+1 " +
                "+ DATEDIFF(MINUTE,SUBSTRING(TglProduksi,1,8)+trim(cast((H2) as nchar))+' '+'00:00:00',SUBSTRING(TglProduksi,1,8)+trim(cast((H2) as nchar))+' '+drJam) " +
                "when shift=3  and sdJam='06:59:00' or shift=3  and sdJam='07:00:00' then '480' " +
                "when shift=3 and H1<>H2 and drJam>='23:00:00' and sdJam<'00:00:00' then (DATEDIFF(MINUTE,drJam,'23:59:59')+1) + DATEDIFF(MINUTE,SUBSTRING(TglProduksi,1,8)+trim(cast((H2) as nchar))+' '+'00:00:00',sdJam2) " +
                "when shift=3 and H1=H2 and drJam>='23:00:00' and sdJam<='23:59:59' then DATEDIFF(MINUTE,TglProduksi+' '+'23:00:00',sdJam2)  " +
                "when shift=3 and H1<>H2 and drJam>='23:00:00' and sdJam>'00:00:00' then " +
                "DATEDIFF(MINUTE,SUBSTRING(TglProduksi,1,8)+trim(cast((H1) as nchar))+' '+'23:00:00',SUBSTRING(TglProduksi,1,8)+trim(cast((H1) as nchar))+' '+'23:59:59')+1 " +
                "+ DATEDIFF(MINUTE,SUBSTRING(TglProduksi,1,8)+trim(cast((H2) as nchar))+' '+'00:00:00',SUBSTRING(TglProduksi,1,8)+trim(cast((H2) as nchar))+' '+sdJam) " +
                "when shift=3 and H1<>H2 and drJam>='23:00:00' and sdJam='00:00:00' then " +
                "DATEDIFF(MINUTE,SUBSTRING(TglProduksi,1,8)+trim(cast((H2) as nchar))+' '+drJam,sdJam2)  " +
                "+ DATEDIFF(MINUTE,SUBSTRING(TglProduksi,1,8)+trim(cast((H1) as nchar))+' '+'23:00:00',SUBSTRING(TglProduksi,1,8)+trim(cast((H1) as nchar))+' '+'23:59:59')+1 " +
                "+ DATEDIFF(MINUTE,SUBSTRING(TglProduksi,1,8)+trim(cast((H2) as nchar))+' '+'00:00:00',SUBSTRING(TglProduksi,1,8)+trim(cast((H2) as nchar))+' '+drJam) " +
                "end Prod_Done " +

                "from ( " +
                "select left(convert(char,A.TglProduksi,23),10)TglProduksi,P.PlantName Line,G.[Group] GP,sum(A.Qty *(I.tebal*I.Lebar*I.Panjang)/1000000000) Kubik,I.tebal,I.Lebar,I.Panjang,DATEPART(DAY,drJam)H1,DATEPART(DAY,sdJam)H2,left(convert(char,A.drJam,114),8)drJam,left(convert(char,A.sdJam,114),8)sdJam,drJam drJam2,sdJam sdJam2,A.shift " +
                ",DAY(TglProduksi)HariAsli,DAY(EOMONTH(TglProduksi)) AS TtlHari,MONTH(TglProduksi)Bulan " +
                "from BM_Destacking A  " +
                "inner join BM_Plant P on A.PlantID=P.ID   " +
                "inner join BM_PlantGroup G on A.PlantGroupID =G.ID    " +
                "inner join fc_items I on A.ItemID=I.ID   " +
                "where A.rowstatus>-1 and left(convert(char,TglProduksi,112),6)=@thnbln and P.PlantName=@line  " +
                "group by P.PlantName,G.[Group],I.tebal, I.Lebar,I.Panjang,A.sdJam,A.drJam,A.TglProduksi,A.shift ) as x  " +
                ") as AA order by TglProduksi,[shift],Prod_Mulai " +
                #endregion

                #region Temp Table Bdt2_P
            "select TglBreak,GP,Line,MenitMulai,MenitDone,StartBD,FinishBD,GP1,BDNPMS_G1 ,BDNPMS_S  into Bdt2_P  from ( " +
                "select *, " +
                "case   " +
                "when StartBD='07:00:00' then DATEDIFF(MINUTE,TglBreak+' '+'07:00:00',TglBreak+' '+'07:00:00')   " +
                "when StartBD>'07:00:00' and StartBD<'15:00:00' then DATEDIFF(MINUTE,TglBreak+' '+'07:00:00',StartBD2)  " +
                "when StartBD='15:00:00' then DATEDIFF(MINUTE,TglBreak+' '+'15:00:00',TglBreak+' '+'15:00:00')   " +
                "when StartBD>'15:00:00' and StartBD<'23:00:00' then DATEDIFF(MINUTE,TglBreak+' '+'15:00:00',StartBD2) " +
                "when StartBD='23:00:00' then DATEDIFF(MINUTE,TglBreak+' '+'23:00:00',TglBreak+' '+'23:00:00')  " +
                "when (StartBD>'23:00:00' and StartBD<='23:59:59') or  (StartBD>='00:00:00' and StartBD<'07:00:00') then DATEDIFF(MINUTE,TglBreak+' '+'23:00:00',StartBD2)  " +
                "end MenitMulai, " +
                "case  " +
                "when FinishBD='07:00:00' then DATEDIFF(MINUTE,TglBreak+' '+'07:00:00',TglBreak+' '+'15:00:00') " +
                "when FinishBD>'07:00:00' and FinishBD<='15:00:00' then DATEDIFF(MINUTE,TglBreak+' '+'07:00:00',FinishBD2)  " +
                "when FinishBD='15:00:00' then DATEDIFF(MINUTE,TglBreak+' '+'15:00:00',TglBreak+' '+'23:00:00')  " +
                "when FinishBD>'15:00:00' and FinishBD<='23:00:00' then DATEDIFF(MINUTE,TglBreak+' '+'15:00:00',FinishBD2) " +
                "when FinishBD='23:00:00' then DATEDIFF(MINUTE,TglBreak+' '+'23:00:00',TglBreak+' '+'23:00:00')  " +
                "when (FinishBD>'23:00:00' and FinishBD<='23:59:59') or  (FinishBD>='00:00:00' and FinishBD<'07:00:00') then DATEDIFF(MINUTE,TglBreak+' '+'23:00:00',FinishBD2) " +
                "end MenitDone " +
                "from ( " +
                //"select *, " +
                //"case  " +
                //"when Bulan=12 and HariAsli=TtlHari " +
                //"then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01 07:00:00' " +
                //"when bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2  " +           
                //"then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01 07:00:00'  " +
                //"when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9  " +
                //"then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00'  " +
                //"when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9  " +
                //"then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00'  " +
                //"when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-0'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00'    end time2, " +

                //"case  " +
                //"when StartBD>='00:00:00' and StartBD<='07:00:00' and Bulan=12 and HariAsli=TtlHari " +
                //"then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01'+' '+StartBD " +
                //"when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2  " +           
                //"then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+StartBD " +
                //"when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 " +
                //"then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD   " +
                //"when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9  " +
                //"then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD   " +
                //"when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 " +
                //"then trim(cast(YEAR(TglBreak) as nchar))+'-0'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD  else (TglBreak)+' '+StartBD end StartBD2, " +

                //"case  when FinishBD>='00:00:00' and FinishBD<='07:00:00' and Bulan=12 and HariAsli=TtlHari " +
                //"then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01'+' '+FinishBD " +
                //"when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 " +           
                // "then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+FinishBD  " +
                //"when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 " +
                //"then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD " +
                //"when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 " +
                //"then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD   " +
                //"when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 " +
                //"then trim(cast(YEAR(TglBreak) as nchar))+'-0'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD     else (TglBreak)+' '+FinishBD   end FinishBD2 from ( " +
                //"select left(convert(char,TglBreak,23),10)TglBreak,right(syarat,2)GP,Line, StartBD,FinishBD,GP4 GP1,(BDNPMS_M+BDNPMS_E+BDNPMS_U+BDNPMS_L+BDNPMS_G4)BDNPMS_G1,BDNPMS_S,DAY(TglBreak)HariAsli,DAY(EOMONTH(TglBreak)) AS TtlHari,MONTH(TglBreak)Bulan  from TempBreakDown1_P)  as x "+
                " select *, case  " +
                "when Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01 07:00:00'  " +
                "when bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 and  len(month(TglBreak))=1 and month(TglBreak)<9 then trim(cast(YEAR(TglBreak) as nchar))+'-0'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01 07:00:00' " +
                "when bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 and  len(month(TglBreak))=1 and month(TglBreak)>=9 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01 07:00:00' " +
                "when bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 and  len(month(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01 07:00:00' " +

                "when Bulan=12 and HariAsli<TtlHari and len(day(TglBreak))=2 then substring(TglBreak,1,8)+''+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' " +
                "when Bulan=12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' " +
                "when Bulan=12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 then substring(TglBreak,1,8)+''+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' " +
                "when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9  then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' " +
                "when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9  then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' " +
                "when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 and len(month(TglBreak))=1 then trim(cast(YEAR(TglBreak) as nchar))+'-0'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00'    " +
                "when bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2 and len(month(TglBreak))=2 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+'07:00:00' " +
                "end time2,  " +

                "case  " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan=12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 " +
                "then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-0'+trim(cast(day(TglBreak)+1 as nchar))+' '+StartBD  " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan=12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 " +
                "then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(day(TglBreak)+1 as nchar))+' '+StartBD  " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan=12 and HariAsli<TtlHari and len(day(TglBreak))=2  " +
                "then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(day(TglBreak)+1 as nchar))+' '+StartBD  " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01'+' '+StartBD " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 and len(month(TglBreak))=2 and Bulan<9 then trim(cast(YEAR(TglBreak) as nchar))+'-0'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+StartBD " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 and len(month(TglBreak))=1 and Bulan<9 then trim(cast(YEAR(TglBreak) as nchar))+'-0'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+StartBD " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 and len(month(TglBreak))=1 and Bulan>=9 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+StartBD  " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 and len(month(TglBreak))=2 and Bulan>=9 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+StartBD " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD   " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9  then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD   " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2  and len(month(TglBreak))=2 " +
                "then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD   " +
                "when StartBD>='00:00:00' and StartBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2  and len(month(TglBreak))=1 " +
                "then trim(cast(YEAR(TglBreak) as nchar))+'-0'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+StartBD  " +
                "else (TglBreak)+' '+StartBD " +
                "end StartBD2, " +

                "case " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan=12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 " +
                "then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-0'+trim(cast(day(TglBreak)+1 as nchar))+' '+FinishBD " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan=12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9 " +
                "then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(day(TglBreak)+1 as nchar))+' '+FinishBD " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan=12 and HariAsli<TtlHari and len(day(TglBreak))=2  " +
                "then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(day(TglBreak)+1 as nchar))+' '+FinishBD  " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and Bulan=12 and HariAsli=TtlHari then trim(cast(YEAR(TglBreak)+1 as nchar))+'-01-01'+' '+FinishBD " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 and len(month(TglBreak))=2 and Bulan<9 then trim(cast(YEAR(TglBreak) as nchar))+'-0'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+StartBD  " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 and len(month(TglBreak))=1 and Bulan<9 then trim(cast(YEAR(TglBreak) as nchar))+'-0'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+FinishBD  " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 and len(month(TglBreak))=1 and Bulan>=9 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+FinishBD " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli=TtlHari and len(day(TglBreak))=2 and len(month(TglBreak))=2 and Bulan>=9 then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak)+1 as nchar))+'-01'+' '+FinishBD " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli<9 then substring(TglBreak,1,8)+'0'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD   " +
                "when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=1 and HariAsli>=9  then substring(TglBreak,1,8)+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD   " +
                " when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2  and len(month(TglBreak))=2 " +
                " then trim(cast(YEAR(TglBreak) as nchar))+'-'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD   " +
                "  when FinishBD>='00:00:00' and FinishBD<='07:00:00' and bulan<12 and HariAsli<TtlHari and len(day(TglBreak))=2  and len(month(TglBreak))=1 " +
                " then trim(cast(YEAR(TglBreak) as nchar))+'-0'+trim(cast(MONTH(TglBreak) as nchar))+'-'+trim(cast(DAY(TglBreak)+1 as nchar))+' '+FinishBD  " +
                "else (TglBreak)+' '+FinishBD  " +

                "end FinishBD2  " +

                "from ( select left(convert(char,TglBreak,23),10)TglBreak,right(syarat,2)GP,Line, StartBD,FinishBD,GP4 GP1,(BDNPMS_M+BDNPMS_E+BDNPMS_U+BDNPMS_L+BDNPMS_G4)BDNPMS_G1,BDNPMS_S,DAY(TglBreak)HariAsli,DAY(EOMONTH(TglBreak)) AS TtlHari,MONTH(TglBreak)Bulan  from TempBreakDown1_P)  as x  " +

                " ) as x1 ) as x2 " +
                "group by TglBreak,GP,Line,MenitMulai,MenitDone,StartBD,FinishBD,GP1,BDNPMS_G1 ,BDNPMS_S " +

                "select Prod_Mulai,case when Prod_Done=479 then 480 else Prod_Done end Prod_Done,TglProduksi Tgl,GP,Kubik,tebal,lebar,panjang,Line " +
                ",(select count(B.TglProduksi) from Bdt1_P B where B.TglProduksi=A.TglProduksi and B.GP=A.GP)ttl " +
                "into compare1_P from Bdt1_P A " +

                "select MenitMulai,case when MenitDone=0 then 480 else MenitDone end MenitDone,TglBreak Tgl,GP,BDNPMS_G1 BDT_NonSch,BDNPMS_S BDT_Sch,GP1 BDTTime into compare2_P from ( " +
                "select TglBreak,GP,Line,MenitMulai,MenitDone,StartBD,FinishBD,GP1,sum(BDNPMS_G1)BDNPMS_G1,sum(BDNPMS_S)BDNPMS_S from Bdt2_P group by TglBreak,GP,Line,MenitMulai,MenitDone,StartBD,FinishBD,GP1 ) as x " +

                "select Tgl,GP,Prod_Mulai,Prod_Done,Kubik,Tebal,Lebar,Panjang,Line,MulaiBDT,AkhirBDT,BDT_Sch,BDT_NonSch into tempDataBdt1_P from ( " +
                "select A.Tgl,A.GP,A.Prod_Mulai,A.Prod_Done,A.Kubik,Tebal,Lebar,Panjang,Line, " +
                "case when B.MenitMulai>=A.Prod_Mulai and B.MenitDone<=A.Prod_Done and A.ttl>1 then B.MenitMulai else '0' end MulaiBDT, " +
                "case when B.MenitMulai>=A.Prod_Mulai and B.MenitDone<=A.Prod_Done and A.ttl>1 then B.MenitDone else '0' end AkhirBDT,isnull(B.BDT_Sch,0)BDT_Sch,isnull(B.BDT_NonSch,0)BDT_NonSch  " +
                "from  compare1_P A " +
                "left join compare2_P B ON A.Tgl=B.Tgl and A.GP=B.GP  where  A.Tgl in (select TglBreak from TempBreakDown1_P where (GP1+GP2+GP3+GP4)>0) " +
                ") as x group by Tgl,GP,Prod_Mulai,Prod_Done,Kubik,Tebal,Lebar,Panjang,Line,MulaiBDT,AkhirBDT,BDT_Sch,BDT_NonSch " +

                "select Tgl,GP,Prod_Mulai,Prod_Done,MulaiBDT,AkhirBDT,BDT_Sch,BDT_NonSch,Tebal,Lebar,Panjang,Line into tempDataBdt01_P from ( " +
                "select Tgl,GP,Prod_Mulai,Prod_Done,MulaiBDT,AkhirBDT,case when (MulaiBDT>0 or AkhirBDT>0) then BDT_Sch else 0 end BDT_Sch, " +
                "case when MulaiBDT=0 and AkhirBDT=0 then 0 " +
                "else BDT_NonSch end BDT_NonSch,Tebal,Lebar,Panjang,Line from tempDataBdt1_P ) as x group by Tgl,GP,Prod_Mulai,Prod_Done,MulaiBDT,AkhirBDT,BDT_Sch,BDT_NonSch,Tebal,Lebar,Panjang,Line " +

                "select Tgl,GP,Prod_Mulai,Prod_done,sum(MulaiBDT)MulaiBDT,sum(AkhirBDT)AkhirBDT,sum(BDT_Sch)BDT_Sch,sum(BDT_NonSch)BDT_NonSch,Tebal,Lebar,Panjang,Line into tempDataBdt2_P " +
                "from tempDataBdt01_P " +
                "group by Tgl,GP,Prod_Mulai,Prod_done,Tebal,Lebar,Panjang,Line " +

                "select Tgl,GP,sum(Prod_Mulai)Prod_Mulai,sum(Prod_Done)Prod_Done,sum(MulaiBDT)MulaiBDT,sum(AkhirBDT)AkhirBDT,sum(BDT_Sch)BDT_Sch,sum(BDT_NonSch)BDT_NonSch,Tebal,Lebar,Panjang,Line into tempDataBdt3_P from tempDataBdt2_P group by Tgl,GP,Tebal,Lebar,Panjang,Line " +

                "select  Tgl,GP,Prod_Mulai,Prod_Done,sum(MulaiBDT)MulaiBDT,sum(AkhirBDT)AkhirBDT,sum(BDT_Sch)BDT_Sch,sum(BDT_NonSch)BDT_NonSch,Tebal,Lebar,Panjang,Line  into tempDataBdt4_P from tempDataBdt3_P group by Tgl,GP,Prod_Mulai,Prod_Done,Tebal,Lebar,Panjang,Line " +

                "select *,Prod_Done-Prod_Mulai WaktuPerMenit into tempDataBdt5_P from tempDataBdt4_P A  " +

                "select A.Line,B.Deskripsi,A.Tgl,A.GP,A.Prod_Mulai,A.Prod_Done,A.MulaiBDT,A.AkhirBDT,A.BDT_Sch,A.BDT_NonSch,A.WaktuPerMenit " +
                "into tempDataBdt6_P from tempDataBdt5_P  A left join BM_StdTargetOutPut B ON A.Tebal=B.Tebal and A.Lebar=B.Lebar and A.Panjang=B.Panjang " +
                "" + Query + " " +
                "where B.RowStatus>-1 " +

                "select Tgl,Line,Deskripsi,GP,sum(WaktuPerMenit) [Waktu/(Menit)] into tempDataBdt06_P from tempDataBdt6_P group by Tgl,Line,Deskripsi,GP " +

                "select * into LastData_P from tempDataBdt06_P " +

                //"select Tgl,Line,Deskripsi,GP,WaktuMenit WaktuMenit2,BDT_Sch,WaktuMenit-BDT_Sch WaktuMenit from ( " +
                //"select  Tgl,Line,Deskripsi,GP,[Waktu/(Menit)]WaktuMenit,isnull((select BDT_Sch from Temp_OutPutProduktifitas_BDT A where A.Tgl=B.Tgl and A.Deskripsi=B.Deskripsi and A.GP=B.GP and A.RowStatus>-1),0)BDT_Sch "+
                //"from LastData_P B where GP='" + GP + "' ) as x order by tgl,deskripsi " +

                "" + query + "" +

                //"select Tgl,Line,Deskripsi,GP,WaktuMenit WaktuMenit2,BDT_Sch,WaktuMenit-BDT_Sch-BDT_NonSch WaktuMenit from ( " +
                //"select  Tgl,Line,Deskripsi,GP,[Waktu/(Menit)]WaktuMenit, " +
                //"isnull((select BDT_Sch from Temp_OutPutProduktifitas_BDT A where A.Tgl=B.Tgl and A.Deskripsi=B.Deskripsi and A.GP=B.GP and A.Noted='O' and A.RowStatus>-1),0)BDT_Sch, " +
                //"isnull((select BDT_NonSch from Temp_OutPutProduktifitas_BDT A where A.Tgl=B.Tgl and A.Deskripsi=B.Deskripsi and A.GP=B.GP and A.Noted='O' and A.RowStatus>-1),0)BDT_NonSch " +
                //"from LastData_P B where GP='" + GP + "' ) as x order by tgl,deskripsi "+

                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBreakBMPO_P]') AND type in (N'U')) DROP TABLE [dbo].[tempBreakBMPO_P]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDMS0_P]') AND type in (N'U')) DROP TABLE [dbo].[tempBDMS0_P]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDMS_P]') AND type in (N'U')) DROP TABLE [dbo].[tempBDMS_P]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OuputProduksiPO_P]') AND type in (N'U')) DROP TABLE [dbo].[OuputProduksiPO_P] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDT1_P]') AND type in (N'U')) DROP TABLE [dbo].[tempBDT1_P]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDT2_P]') AND type in (N'U')) DROP TABLE [dbo].[tempBDT2_P]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDT3_P]') AND type in (N'U')) DROP TABLE [dbo].[tempBDT3_P] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDTP_P]') AND type in (N'U')) DROP TABLE [dbo].[tempBDTP_P] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempBDTO_P]') AND type in (N'U')) DROP TABLE [dbo].[tempBDTO_P] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempBreakDown1_P]') AND type in (N'U')) DROP TABLE [dbo].[TempBreakDown1_P] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bdt1_P]') AND type in (N'U')) DROP TABLE [dbo].[Bdt1_P]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Bdt2_P]') AND type in (N'U')) DROP TABLE [dbo].[Bdt2_P]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[compare1_P]') AND type in (N'U')) DROP TABLE [dbo].[compare1_P] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[compare2_P]') AND type in (N'U')) DROP TABLE [dbo].[compare2_P] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataBdt1_P]') AND type in (N'U')) DROP TABLE [dbo].[tempDataBdt1_P]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataBdt01_P]') AND type in (N'U')) DROP TABLE [dbo].[tempDataBdt01_P]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataBdt2_P]') AND type in (N'U')) DROP TABLE [dbo].[tempDataBdt2_P]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataBdt3_P]') AND type in (N'U')) DROP TABLE [dbo].[tempDataBdt3_P]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataBdt4_P]') AND type in (N'U')) DROP TABLE [dbo].[tempDataBdt4_P] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataBdt5_P]') AND type in (N'U')) DROP TABLE [dbo].[tempDataBdt5_P] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataBdt05_P]') AND type in (N'U')) DROP TABLE [dbo].[tempDataBdt05_P] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataBdt6_P]') AND type in (N'U')) DROP TABLE [dbo].[tempDataBdt6_P] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataBdt06_P]') AND type in (N'U')) DROP TABLE [dbo].[tempDataBdt06_P] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData_P]') AND type in (N'U')) DROP TABLE [dbo].[LastData_P] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LastData2_P]') AND type in (N'U')) DROP TABLE [dbo].[LastData2_P] ";

                #endregion


                arrData = new ArrayList();
                string strSQL1 = strSQL;


                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(strSQL1);
                if (da.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new ProduktifitasOutput
                        {
                            Tgl = sdr["Tgl"].ToString(),
                            Line = sdr["Line"].ToString(),
                            Deskripsi = sdr["Deskripsi"].ToString(),
                            GP = sdr["GP"].ToString(),
                            WaktuMenit = Convert.ToInt32(sdr["WaktuMenit"]),
                            WaktuMenit2 = Convert.ToInt32(sdr["WaktuMenit2"]),
                            BDT_Sch = Convert.ToInt32(sdr["BDT_Sch"]),
                            BDT_NonSch = Convert.ToInt32(sdr["BDT_NonSch"]),
                            ttl = Convert.ToInt32(sdr["ttl"])
                        });
                    }
                }
                return arrData;
            }
        }

    }
}