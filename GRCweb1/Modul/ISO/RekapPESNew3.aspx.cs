using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BusinessFacade;
using Domain;
using System.IO;
using System.Data.SqlClient;

namespace GRCweb1.Modul.ISO
{
    public partial class RekapPESNew3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadTahun();
                LoadDept();
                if (Request.QueryString["t"] != null)
                {
                    ddlTahun.SelectedValue = Request.QueryString["t"].ToString();
                }
            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('thr', 450, 99 , 50 ,false); </script>", false);
        }
        public decimal TotalPES = 0;
        public decimal TotalBobot = 0;
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
        //        if (((Users)Session["Users"]).DeptID.ToString() == "7"  || ((Users)Session["Users"]).RowStatus.ToString() == "10")
        //            ddlDept.Items.Add(new ListItem("--ALL Dept--", "0"));
        //        else
        //            ddlDept.Items.Add(new ListItem(" ", "-1"));
        //        foreach (PES2016 d in arrData)
        //        {
        //            //if (((Users)Session["Users"]).DeptID.ToString() == "7" || ((Users)Session["Users"]).DeptID.ToString() == "14")
        //            //    ddlDept.Items.Add(new ListItem(d.DeptName, d.DeptID.ToString()));
        //            //else
        //            //{
        //                //if (d.DeptID.ToString()== ((Users)Session["Users"]).DeptID.ToString())
        //                    ddlDept.Items.Add(new ListItem(d.DeptName, d.DeptID.ToString()));
        //            //}
        //        }
        //        if (arrData.Count > 0)
        //        {
        //            if (ddlDept.SelectedItem.Text.Trim()!="--ALL Dept--")
        //            //ddlDept.SelectedValue = ((Users)Session["Users"]).DeptID.ToString();
        //            ddlDept_Change(null, null);
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
                    arrDept = this.LoadDept(string.Empty);
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
            Users user = (Users)Session["Users"];
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
            if (forList == true)
            {
                ISO_PES ip = new ISO_PES();
                ArrayList arrData = new ArrayList();

                string strdept = string.Empty;
                if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "MAINTENANCE")
                {
                    strdept = "select id from Dept where DeptName like 'maint%'";
                }
                else
                    strdept = ddlDept.SelectedValue.ToString(); ;
                ip.Criteria = (ddlDept.SelectedIndex > 0) ? " WHERE DeptID in (" + strdept + ")" : " WHERE DeptID in (" + strdeptid + ")";
                string Kriteria = (ddlDept.SelectedIndex > 0) ? " AND ID=" + strdept : " and ID in (" + strdeptid + ")";
                ip.Tahun = int.Parse(ddlTahun.SelectedValue.ToString());

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
                lstDept.DataSource = arrData;
                lstDept.DataBind();
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
        protected void ddlDept_Change(object sender, EventArgs e)
        {
            ArrayList arrData = new ArrayList();
            arrData = LoadPIC();
            ddlPIC.Items.Clear();
            ddlPIC.Items.Add(new ListItem("--Pilih PIC --", "0"));
            foreach (PES2016 p in arrData)
            {
                ddlPIC.Items.Add(new ListItem(p.Nama.ToString().ToUpper(), p.ID.ToString()));
            }
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
        protected void ddlBulan_Change(object sender, EventArgs e)
        {
            ddlDept_Change(null, null);
        }
        private ArrayList LoadPIC()
        {
            ArrayList arrData = new ArrayList();
            ISO_PES ip = new ISO_PES();
            string strdept = string.Empty;
            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "MAINTENANCE")
            {
                strdept = "select id from Dept where DeptName like 'maint%'";
            }
            else
                strdept = ddlDept.SelectedValue;
            ip.Tahun = int.Parse(ddlTahun.SelectedValue.ToString());
            ip.Semester = ddlBulan.SelectedValue.ToString();
            ip.Criteria = (ddlDept.Items.Count > 0) ? " AND p.DeptID IN (" + strdept + ")" : " AND p.DeptID =" + ddlDept.SelectedValue;
            arrData = ip.LoadPIC();
            return arrData;
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
        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=RekapPes.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<b>REKAP PES</b>";
            Html += "<br>Periode : " + ddlBulan.SelectedItem.Text + "  " + ddlTahun.SelectedValue.ToString();
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
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            LoadDept(true);
            HtmlTableRow tr = (HtmlTableRow)hd1;
            HtmlTableRow tr2 = (HtmlTableRow)hd2;
            HtmlTableRow tr3 = (HtmlTableRow)hd3;
            for (int i = 1; i < tr.Cells.Count; i++)
            {
                switch (ddlBulan.SelectedValue)
                {
                    case "1": tr.Cells[i].Visible = (i > 7 && i < (tr.Cells.Count - 1)) ? false : true; break;
                    case "2": tr.Cells[i].Visible = (i < 8 && i > 1) ? false : true; break;
                    default: tr.Cells[i].Visible = true; break;
                }
            }
            for (int n = 1; n < tr2.Cells.Count; n++)
            {
                switch (ddlBulan.SelectedValue)
                {
                    case "1": tr2.Cells[n].Visible = (n > 11) ? false : true; tr2.Cells[n - 1].Style["Width"] = Unit.Pixel(70).ToString(); break;
                    case "2": tr2.Cells[n].Visible = (n < 15 && n > 2) ? false : true; tr2.Cells[n].Style["Width"] = Unit.Pixel(70).ToString(); break;
                    default: tr2.Cells[n].Visible = true; break;
                }
            }
            for (int n = 1; n < tr3.Cells.Count; n++)
            {
                switch (ddlBulan.SelectedValue)
                {
                    case "1": tr3.Cells[n].Visible = (n > 5) ? false : true; tr3.Cells[n - 1].Style["Width"] = Unit.Pixel(70).ToString(); break;
                    case "2":
                        tr3.Cells[n].Visible = (n < 7 && n > 0) ? false : true;
                        tr3.Cells[n].Style["Width"] = Unit.Pixel(70).ToString(); break;
                    default: tr3.Cells[n].Visible = true; break;
                }
            }
            Repeater rdept = (Repeater)lstDept;
            for (int x = 0; x < rdept.Items.Count; x++)
            {
                HtmlTableRow tdep = (HtmlTableRow)lstDept.Items[x].FindControl("dpt");
                switch (ddlBulan.SelectedValue)
                {
                    case "1": tdep.Cells[1].ColSpan = 14; break;
                    case "2": tdep.Cells[1].ColSpan = 14; break;
                    default: tdep.Cells[1].ColSpan = 26; break;
                }
                Repeater lstpic = (Repeater)rdept.Items[x].FindControl("lstPIC");
                for (int z = 0; z < lstpic.Items.Count; z++)
                {
                    HtmlTableRow trpic = (HtmlTableRow)lstpic.Items[z].FindControl("pic");
                    switch (ddlBulan.SelectedValue)
                    {
                        case "1": trpic.Cells[0].ColSpan = 14; break;
                        case "2": trpic.Cells[0].ColSpan = 14; break;
                        default: trpic.Cells[0].ColSpan = 26; break;
                    }
                }
            }
        }
        protected void lstDept_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ArrayList arrData = new ArrayList();
            PES2016 p = (PES2016)e.Item.DataItem;
            ISO_PES ip = new ISO_PES();
            ip.Tahun = int.Parse(ddlTahun.SelectedValue.ToString());
            ip.Semester = ddlBulan.SelectedValue.ToString();
            ip.Criteria = " AND  P.DeptID=" + p.DeptID.ToString();
            ip.Criteria += (ddlPIC.SelectedIndex > 0) ? " AND p.ID=" + ddlPIC.SelectedValue : " ";
            arrData = ip.LoadPIC();
            Repeater lstPic = (Repeater)e.Item.FindControl("lstPIC");
            lstPic.DataSource = arrData;
            lstPic.DataBind();
        }
        protected void lstPIC_DataBound(object sender, RepeaterItemEventArgs e)
        {
            TotalPES = 0;
            TotalBobot = 0;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ArrayList arrData = new ArrayList();
                ArrayList arrD = new ArrayList();
                PES2016 p = (PES2016)e.Item.DataItem;
                ISO_PES ip = new ISO_PES();
                ip.PICName = p.UserName;
                ip.Tahun = int.Parse(ddlTahun.SelectedValue.ToString());
                ip.DeptID = p.DeptID;
                ip.BagianID = p.BagianID;
                ip.Semester = (ddlBulan.SelectedIndex == 0) ? "12" : ddlBulan.SelectedValue.ToString();
                ip.Criteria = " WHERE xx.BagianID=" + p.BagianID;
                arrData = ip.LoadPES();
                Repeater lstPes = (Repeater)e.Item.FindControl("lstPES");

                lstPes.DataSource = arrData;
                lstPes.DataBind();
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                HtmlTableRow trpic = (HtmlTableRow)e.Item.FindControl("smtr");
                switch (ddlBulan.SelectedValue)
                {
                    case "1": trpic.Cells[0].ColSpan = 13; break;
                    case "2": trpic.Cells[0].ColSpan = 13; break;
                    default: trpic.Cells[0].ColSpan = 26; break;
                }

            }
        }

        protected void lstPES_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ISO_PES isp = new ISO_PES();
            PES2016 p = (PES2016)e.Item.DataItem;
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("ps1");
                    int BulanPembagi = 0;
                    int BulanPembanding = 0;
                    isp.PICName = p.Nama;
                    isp.BagianID = p.BagianID;
                    int MulaiPes = isp.MulaiPes();
                    int AhkhirPES = isp.MulaiPes(true);
                    int Pembagi = 6;
                    decimal minimalTask = 0;
                    DateTime datenow = DateTime.Now;
                    #region MinimalTas
                    if (p.PesType == 2)
                    {
                        if (p.Bobot == 10) { minimalTask = 6; }
                        else if (p.Bobot == 15) { minimalTask = 8; }
                        else { minimalTask = (p.Bobot - 10); }
                    }
                    #endregion
                    #region Hitunng Total
                    if (tr != null)
                    {
                        decimal isi = 0; string ToolTips = string.Empty;
                        string blncount = AhkhirPES.ToString().Substring(4, 2);
                        TotalBobot += p.Bobot;
                        for (int i = 1; i < tr.Cells.Count; i++)
                        //for (int i = 1; i < tr.Cells.Count; i++)
                        {
                            decimal bobotx = 0; decimal bobotx2 = 0; int aprov = 0;
                            switch (ddlBulan.SelectedValue)
                            {
                                #region semester 1
                                case "1":
                                    bobotx = 0;
                                    if (Convert.ToInt32(blncount) < 6)
                                        BulanPembagi = int.Parse(ddlTahun.SelectedValue + blncount);
                                    else
                                        BulanPembagi = int.Parse(ddlTahun.SelectedValue + "06");
                                    switch ((BulanPembagi - MulaiPes))
                                    {
                                        case 0: Pembagi = 1; break;
                                        case 1: Pembagi = 2; break;
                                        case 2: Pembagi = 3; break;
                                        case 3: Pembagi = 4; break;
                                        case 4: Pembagi = 5; break;
                                        case 5: Pembagi = 6; break;
                                    }
                                    /*
                                    switch((BulanPembagi-AhkhirPES))
                                    {
                                        case 0: Pembagi = Pembagi; break;
                                        case 1: Pembagi = 5; break;
                                        case 2: Pembagi = 4; break;
                                        case 3: Pembagi = 3; break;
                                        case 4: Pembagi = 2; break;
                                        case 5: Pembagi = 1; break;

                                    }*/
                                    tr.Cells[i].Visible = (i > 13 && i < (tr.Cells.Count - 1)) ? false : true;

                                    if (p.PesType == 2 && i == (tr.Cells.Count - 1))
                                    {
                                        #region Rekap Task
                                        bobotx = (p.BobotSmt1 > minimalTask) ? 1 : (p.BobotSmt1 / minimalTask);
                                        tr.Cells[tr.Cells.Count - 1].InnerText = Math.Round((p.Semester1 * bobotx * p.Bobot / 100), 1, MidpointRounding.AwayFromZero).ToString("N1");
                                        tr.Cells[tr.Cells.Count - 1].Attributes.Add("title", p.Semester1.ToString("N1") + "x(" + p.BobotSmt1.ToString("N1") + "/" + minimalTask.ToString("N0") + ")x" + p.Bobot.ToString("N1") + "/100");
                                        TotalPES += Math.Round((p.Semester1 * bobotx * p.Bobot / 100), 1, MidpointRounding.AwayFromZero);

                                        //infomasi Task
                                        ToolTips = "Informasi Task Semester 1 :\nBobot Task di PES" + new string(' ', 24).ToString() + ": " + (p.Bobot).ToString("N1") + " %\n";
                                        ToolTips += "Minimal Bobot Task " + new string(' ', 20).ToString() + ": " + minimalTask.ToString("N1") + "\n";
                                        ToolTips += "Total Bobot Task Per Semester" + new string(' ', 3) + " : " + p.BobotSmt1.ToString("N1") + "\n";
                                        ToolTips += "Nilai Task Per semester" + new string(' ', 16) + ": " + (p.Semester1).ToString("N1");
                                        ToolTips += "\n\nFormula Total Nilai Task Di Rekap PES : \n";
                                        ToolTips += "(Total Bobot Task Per Semester / Minimal Bobot Task) x Nilai Task per Semester x Bobot Task di PES \n";
                                        ToolTips += new string('-', 70) + "\n";
                                        ToolTips += "(" + p.BobotSmt1.ToString("N1") + "/" + minimalTask.ToString("N1") + ") x " + (p.Semester1).ToString("N1") + " x " + (p.Bobot).ToString("N1");
                                        ToolTips += "% = " + (Math.Round((p.Semester1 * bobotx * p.Bobot / 100), 1, MidpointRounding.AwayFromZero)).ToString("N1");
                                        ToolTips += "\n\nKlik untuk display info.";
                                        tr.Cells[tr.Cells.Count - 1].Attributes.Add("title", ToolTips);
                                        tr.Cells[tr.Cells.Count - 1].Attributes.Add("onclick", "js:alert('" + ToolTips.Replace("\n\nKlik untuk display info.", "").Replace("\n", "\\n") + "')");
                                        tr.Cells[0].Attributes.Add("onclick", "js:displayPopUP('DETAIL TASK');");
                                        tr.Cells[0].Attributes.Add("title", "Click for detail Task");
                                        #endregion
                                    }
                                    else
                                    {
                                        #region Rekap PES
                                        //if (ddlTahun.SelectedItem.Text == datenow.Year.ToString() && DateTime.Now.Month >= 1 && DateTime.Now.Month <= 6)
                                        //{
                                        //    if (DateTime.Now.Day < 21 )
                                        //        Pembagi = DateTime.Now.Month - 2;
                                        //    else
                                        //        Pembagi = DateTime.Now.Month - 1;
                                        //}
                                        if (MulaiPes <= Convert.ToInt32(ddlTahun.SelectedItem.Text + "01"))
                                            MulaiPes = Convert.ToInt32(ddlTahun.SelectedItem.Text + "01");
                                        if (AhkhirPES >= Convert.ToInt32(ddlTahun.SelectedItem.Text + "06"))
                                            AhkhirPES = Convert.ToInt32(ddlTahun.SelectedItem.Text + "06");
                                        Pembagi = AhkhirPES - MulaiPes + 1;
                                        if (Pembagi == 0) Pembagi = 1;
                                        tr.Cells[tr.Cells.Count - 1].InnerText = Math.Round((p.Semester1 / Pembagi), 1, MidpointRounding.AwayFromZero).ToString("N1");
                                        TotalPES += (i == (tr.Cells.Count - 1)) ? Math.Round((p.Semester1 / Pembagi), 1, MidpointRounding.AwayFromZero) : 0;
                                        string d = p.Nama + ":" + p.DeptID + ":" + p.BagianID + ":" + p.PESName + ":" + p.Tahun + ":" + ddlBulan.SelectedValue;
                                        if (p.PesType == 1 || p.PesType == 3)
                                        {
                                            string ps = (p.PesType == 1) ? "KPI" : "SOP";
                                            tr.Cells[0].Attributes.Add("onclick", "return loadPES('" + d.ToString() + "')");
                                            tr.Cells[1].Attributes.Add("onclick", "return loadPES('" + d.ToString() + "')");
                                            tr.Cells[0].Attributes.Add("title", "Click for detail");
                                            if (p.Penilaian > 0)
                                            {
                                                tr.Cells[1].Attributes.Add("class", "kotak tengah bold OddRows withRebobot baris");
                                            }
                                            tr.Cells[0].Attributes.Add("class", "kotak baris");
                                            if (i > 1 && (i + 1) < (tr.Cells.Count - 3) && (i % 2) == 0 && p.PesType == 3)
                                            {
                                                int bln = (i) / 2;
                                                aprov = this.CekApproval(p.Nama, bln, p.Tahun, ps);
                                                if (aprov == 0)
                                                {
                                                    tr.Cells[(i + 1)].Attributes.Add("style", "color:grey;");
                                                    tr.Cells[(i)].Attributes.Add("style", "color:grey");
                                                    tr.Cells[(i + 1)].Attributes.Add("title", "Belum di Approve");
                                                    tr.Cells[(i + 1)].Attributes.Add("title", "Belum di Approve");
                                                }
                                            }
                                        }
                                        #endregion
                                    }
                                    break;
                                #endregion
                                #region semester 2
                                case "2":

                                    bobotx = 0;
                                    //TotalPES = 0;
                                    //if (Convert.ToInt32(blncount) < 12)
                                    //    BulanPembagi = int.Parse(ddlTahun.SelectedValue + blncount);
                                    //else
                                    //    BulanPembagi = int.Parse(ddlTahun.SelectedValue + "12");
                                    //BulanPembanding = int.Parse(ddlTahun.SelectedValue + "06");
                                    //Pembagi = (MulaiPes < BulanPembagi && MulaiPes < BulanPembanding) ? 6 : (BulanPembagi - MulaiPes + );
                                    //Pembagi = BulanPembagi-int.Parse(ddlTahun.SelectedValue + "07") + 1;
                                    BulanPembagi = int.Parse(ddlTahun.SelectedValue + "12");
                                    Pembagi = 6;
                                    switch ((BulanPembagi - MulaiPes))
                                    {
                                        case 0: Pembagi = 1; break;
                                        case 1: Pembagi = 2; break;
                                        case 2: Pembagi = 3; break;
                                        case 3: Pembagi = 4; break;
                                        case 4: Pembagi = 5; break;
                                        case 5: Pembagi = 6; break;
                                    }
                                    //if (ddlTahun.SelectedItem.Text == datenow.Year.ToString())
                                    //{
                                    //    if (DateTime.Now.Day < 21)
                                    //        Pembagi = 12 - DateTime.Now.Month - 2;
                                    //    else
                                    //        Pembagi = 12 - DateTime.Now.Month-1;
                                    //}
                                    if (MulaiPes <= Convert.ToInt32(ddlTahun.SelectedItem.Text + "07"))
                                        MulaiPes = Convert.ToInt32(ddlTahun.SelectedItem.Text + "07");
                                    if (AhkhirPES >= Convert.ToInt32(ddlTahun.SelectedItem.Text + "12"))
                                        AhkhirPES = Convert.ToInt32(ddlTahun.SelectedItem.Text + "12");
                                    Pembagi = AhkhirPES - MulaiPes + 1;
                                    tr.Cells[i].Visible = (i < 14 && i > 1) ? false : true;

                                    if (p.PesType == 2 && i == (tr.Cells.Count - 1))
                                    {
                                        bobotx = (p.BobotSmt2 > minimalTask) ? 1 : (p.BobotSmt2 / (minimalTask));
                                        tr.Cells[tr.Cells.Count - 1].InnerText = Math.Round((p.Semester2 * bobotx * p.Bobot / 100), 1, MidpointRounding.AwayFromZero).ToString("N1");
                                        //tr.Cells[tr.Cells.Count - 1].Attributes.Add("title", p.Semester2.ToString("N1") + "x(" + p.BobotSmt2.ToString("N1") + "/" + minimalTask.ToString("N0") + ")x" + p.Bobot.ToString("N1") + "/100");
                                        TotalPES += Math.Round((p.Semester2 * bobotx * p.Bobot) / 100, 1, MidpointRounding.AwayFromZero);

                                        //infomasi Task
                                        ToolTips = "Informasi Task Semester 2 :\nBobot Task di PES" + new string(' ', 24).ToString() + ": " + (p.Bobot).ToString("N1") + " %\n";
                                        ToolTips += "Minimal Bobot Task " + new string(' ', 20).ToString() + ": " + minimalTask.ToString("N1") + "\n";
                                        ToolTips += "Total Bobot Task Per Semester" + new string(' ', 3) + " : " + p.BobotSmt2.ToString("N1") + "\n";
                                        ToolTips += "Nilai Task Per semester" + new string(' ', 16) + ": " + (p.Semester2).ToString("N1");
                                        ToolTips += "\n\nFormula Total Nilai Task Di Rekap PES : \n";
                                        ToolTips += "(Total Bobot Task Per Semester / Minimal Bobot Task) x Nilai Task per Semester x Bobot Task di PES \n";
                                        ToolTips += new string('-', 70) + "\n";
                                        ToolTips += "(" + p.BobotSmt2.ToString("N1") + "/" + minimalTask.ToString("N1") + ") x " + (p.Semester2).ToString("N1") + " x " + (p.Bobot).ToString("N1");
                                        ToolTips += "% = " + (Math.Round((p.Semester2 * bobotx * p.Bobot / 100), 1, MidpointRounding.AwayFromZero)).ToString("N1");
                                        ToolTips += "\n\nKlik untuk display info.";
                                        tr.Cells[tr.Cells.Count - 1].Attributes.Add("title", ToolTips);
                                        tr.Cells[tr.Cells.Count - 1].Attributes.Add("onclick", "js:alert('" + ToolTips.Replace("\n\nKlik untuk display info.", "").Replace("\n", "\\n") + "')");
                                        tr.Cells[0].Attributes.Add("onclick", "js:displayPopUP('DETAIL TASK');");
                                        tr.Cells[0].Attributes.Add("title", "Click for detail Task");
                                    }
                                    else
                                    {
                                        tr.Cells[tr.Cells.Count - 1].InnerText = Math.Round((p.Semester2 / Pembagi), 1, MidpointRounding.AwayFromZero).ToString("N1");
                                        if (i == (tr.Cells.Count - 1))
                                        {
                                            TotalPES += Math.Round((p.Semester2 / Pembagi), 1, MidpointRounding.AwayFromZero);
                                        }
                                        string d = p.Nama + ":" + p.DeptID + ":" + p.BagianID + ":" + p.PESName + ":" + p.Tahun + ":" + ddlBulan.SelectedValue;
                                        if (p.PesType == 1 || p.PesType == 3)
                                        {

                                            tr.Cells[0].Attributes.Add("onclick", "return loadPES('" + d.ToString() + "')");
                                            tr.Cells[1].Attributes.Add("onclick", "return loadPES('" + d.ToString() + "')");
                                            tr.Cells[0].Attributes.Add("title", "Click for detail");
                                            if (p.Penilaian > 0)
                                            {
                                                tr.Cells[1].Attributes.Add("class", " tengah bold OddRows kotak withRebobot baris");
                                            }
                                            tr.Cells[0].Attributes.Add("class", "kotak baris");
                                        }
                                    }
                                    break;
                                #endregion
                                default:
                                    bobotx = 0;
                                    bobotx2 = 0;

                                    //BulanPembagi = int.Parse(ddlTahun.SelectedValue + "12");
                                    BulanPembagi = int.Parse(ddlTahun.SelectedValue + blncount);
                                    BulanPembanding = int.Parse(ddlTahun.SelectedValue + "01");
                                    Pembagi = (MulaiPes <= BulanPembagi && MulaiPes <= BulanPembanding) ? 6 : (BulanPembagi - MulaiPes + 1);
                                    int ikutan = (MulaiPes <= BulanPembagi && MulaiPes <= BulanPembanding && Pembagi > 6) ? 2 : 1;
                                    ikutan = (AhkhirPES < BulanPembagi) ? 1 : ikutan;
                                    tr.Cells[i].Visible = true;
                                    int minimaltas1 = 0;
                                    int minimaltask2 = 0;

                                    if (p.PesType == 2 && i == (tr.Cells.Count - 1))
                                    {
                                        bobotx = (p.BobotSmt1 > minimalTask) ? 1 : (p.BobotSmt1 / minimalTask);
                                        bobotx2 = (p.BobotSmt2 > minimalTask) ? 1 : (p.BobotSmt2 / minimalTask);
                                        //if (int.Parse(blncount) < 6)
                                        //{
                                        //    bobotx = (p.BobotSmt1 / ((minimalTask / 6) * int.Parse(blncount)));
                                        //}
                                        //else
                                        //{
                                        //    bobotx = (p.BobotSmt1 / minimalTask);
                                        //}
                                        //if (int.Parse(blncount) > 6 && int.Parse(blncount) < 12)
                                        //{
                                        //    bobotx2 = (p.BobotSmt2 / ((minimalTask / 6) * (int.Parse(blncount)-6)));
                                        //}
                                        //else
                                        //{
                                        //    bobotx2 = (p.BobotSmt2 / minimalTask);
                                        //}
                                        ikutan = (bobotx != 0) ? 2 : 1;
                                        ikutan = (AhkhirPES < BulanPembagi) ? 1 : ikutan;
                                        ikutan = 2;
                                        tr.Cells[tr.Cells.Count - 1].InnerText =
                                            Math.Round((((p.Semester1 * bobotx * p.Bobot / 100) + (p.Semester2 * bobotx2 * p.Bobot / 100)) / ikutan), 1, MidpointRounding.AwayFromZero).ToString("N1");
                                        tr.Cells[tr.Cells.Count - 1].Attributes["title"] = "(" + (p.Semester1 * bobotx * p.Bobot / 100).ToString("N1") + " + " + (p.Semester2 * bobotx2 * p.Bobot / 100).ToString("N1") + ")/" + ikutan.ToString();
                                        TotalPES += Math.Round((((p.Semester1 * bobotx * p.Bobot / 100) + (p.Semester2 * bobotx2 * p.Bobot / 100)) / ikutan), 1, MidpointRounding.AwayFromZero);
                                    }
                                    else
                                    {
                                        int Pembagismt1 = 1;
                                        int Pembagismt2 = 1;
                                        int test = MulaiPes + 1;
                                        if (p.Semester1 > 0 & p.Semester2 > 0) { ikutan = 2; } else { ikutan = 1; }
                                        Pembagi = (MulaiPes <= BulanPembagi && MulaiPes <= BulanPembanding) ? 6 : (BulanPembagi - MulaiPes);
                                        Pembagismt1 = (Pembagi > 6) ? (Pembagi - 6) : Pembagi;
                                        Pembagismt2 = (Pembagi > 6) ? 6 : Pembagi;
                                        Pembagismt1 = (Pembagi < 6) ? (6 + Pembagi) : Pembagi;
                                        Pembagismt2 = (Pembagi < 6) ? (12 + Pembagi) : Pembagi;
                                        Pembagismt1 = (AhkhirPES < BulanPembagi & Pembagismt1 == 6) ? 6 : Pembagismt1;
                                        Pembagismt2 = (AhkhirPES < BulanPembagi) ? 6 : Pembagismt2;

                                        //if (int.Parse(blncount) < 6)
                                        //    Pembagismt1 = int.Parse(blncount);
                                        //else
                                        //    Pembagismt1 = 6;
                                        //if (int.Parse(blncount) > 6 && int.Parse(blncount) <12)
                                        //    Pembagismt2 = int.Parse(blncount)-6;
                                        //else
                                        //    Pembagismt2 = 6;
                                        //ikutan = (AhkhirPES < BulanPembagi) ? 1 : ikutan;
                                        //tr.Cells[tr.Cells.Count - 1].InnerText = Math.Round(((p.Semester1 / Pembagismt1) + (p.Semester2 / Pembagismt2)) / ikutan, 1, MidpointRounding.AwayFromZero).ToString("N1");
                                        tr.Cells[tr.Cells.Count - 1].InnerText = (Pembagismt1 == 0) ? Math.Round(((p.Semester2 / Pembagismt2)) / ikutan, 1, MidpointRounding.AwayFromZero).ToString("N1") : Math.Round(((p.Semester1 / Pembagismt1) + (p.Semester2 / Pembagismt2)) / ikutan, 1, MidpointRounding.AwayFromZero).ToString("N1");
                                        //tr.Cells[tr.Cells.Count - 1].Attributes["title"] = "(" + (p.Semester1 / Pembagismt1).ToString("N1") + " + " + (p.Semester2 / Pembagismt2).ToString("N1") + ")/" + ikutan.ToString();
                                        tr.Cells[tr.Cells.Count - 1].Attributes["title"] = (Pembagismt1 == 0) ? "(" + (p.Semester2 / Pembagismt2).ToString("N1") + ")/" + ikutan.ToString() : "(" + (p.Semester1 / Pembagismt1).ToString("N1") + " + " + (p.Semester2 / Pembagismt2).ToString("N1") + ")/" + ikutan.ToString();
                                        if (i == (tr.Cells.Count - 1))
                                        {
                                            //TotalPES += Math.Round(((p.Semester1 / Pembagismt1) + (p.Semester2 / Pembagismt2)) / ikutan, 1, MidpointRounding.AwayFromZero);
                                            TotalPES += (Pembagismt1 == 0) ? Math.Round(((p.Semester2 / Pembagismt2)) / ikutan, 1, MidpointRounding.AwayFromZero) : Math.Round(((p.Semester1 / Pembagismt1) + (p.Semester2 / Pembagismt2)) / ikutan, 1, MidpointRounding.AwayFromZero);

                                        }
                                    }
                                    break;
                            }
                            if (decimal.TryParse(tr.Cells[i].InnerText, out isi))
                            {
                                if (isi == 0) { tr.Cells[i].InnerText = ""; }
                                if ((i + 1) < tr.Cells.Count && i > 1)
                                {
                                    if (i != 13)
                                    {
                                        if (tr.Cells[i + 1].InnerText == "0.0" && p.PesType != 2) { tr.Cells[i + 1].InnerText = ""; }
                                    }
                                }
                            }
                            else
                            {
                                tr.Cells[i].Style["color"] = "Red";// System.Drawing.Color.Red;
                            }
                        }
                    }
                    #endregion
                }
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("ftr");
                    switch (ddlBulan.SelectedValue)
                    {
                        case "1":
                            tr.Cells[0].ColSpan = 1;
                            tr.Cells[1].ColSpan = 1;
                            tr.Cells[2].ColSpan = 12;
                            break;
                        case "2":
                            tr.Cells[0].ColSpan = 1;
                            tr.Cells[1].ColSpan = 1;
                            tr.Cells[2].ColSpan = 12;
                            break;
                        default:
                            tr.Cells[0].ColSpan = 1;
                            tr.Cells[1].ColSpan = 1;
                            tr.Cells[2].ColSpan = 24;
                            break;
                    }
                    tr.Cells[1].InnerText = TotalBobot.ToString("N1");
                    tr.Cells[3].InnerText = TotalPES.ToString("N1");
                }
            }
            catch { }
        }
        private string TaskInfo()
        {
            return string.Empty;

        }
        private int CekApproval(string PIC, int Bulan, int Tahun, string PESType)
        {
            int result = 0;
            ISO_PES ip = new ISO_PES();
            ip.Criteria = " AND YEAR(TglMulai)=" + Tahun + " AND MONTH(TglMulai)=" + Bulan + " AND PIC='" + PIC + "'";
            PES2016 p = ip.CheckApproval(PESType);
            if (p.JmlItemPES == ((p.Approval / 2) + p.Penilaian))
            {
                result = 1;
            }
            return result;
        }

    }
}