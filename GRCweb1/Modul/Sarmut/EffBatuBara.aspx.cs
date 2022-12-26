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
using System.Text;
using System.Reflection;
using Domain;
using DataAccessLayer;
using BusinessFacade;
using Factory;
using System.Web.UI.DataVisualization.Charting;


namespace GRCweb1.Modul.Sarmut
{
    public partial class EffBatuBara : System.Web.UI.Page
    {
        public decimal totalbatubara = 0;
        public decimal totalm3 = 0;
        public decimal hasilpencapaian = 0;
        public decimal hasilakhir = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                getYear();
            }

            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }

        


        private void getYear()
        {
            /**
             * Fill dropdown items with data tahun from database
             */
            ddlTahun.Items.Clear();
            ArrayList arrTahun = new ArrayList();
            T3_MutasiWIPFacade T3_MutasiWIPFacade = new T3_MutasiWIPFacade();
            arrTahun = T3_MutasiWIPFacade.BM_Tahun();
            ddlTahun.Items.Clear();
            ddlTahun.Items.Add(new ListItem("-- Pilih Tahun --", "0"));
            foreach (T3_MutasiWIP bmTahun in arrTahun)
            {
                ddlTahun.Items.Add(new ListItem(bmTahun.Tahune.ToString(), bmTahun.Tahune.ToString()));
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            
            ArrayList arrBB = new ArrayList();
            ArrayList arrBK = new ArrayList();

            BatuBaraFacade bd = new BatuBaraFacade();
            arrBB = bd.RetrieveBatubara(ddlTahun.Text.Trim(), ddlBulan.SelectedValue.ToString());
            lstBatuBara.DataSource = arrBB;
            lstBatuBara.DataBind();

            arrBK = bd.RetrieveBatubaraAkumulasi(ddlTahun.Text.Trim(),ddlBulan.SelectedValue.ToString());
            lstBatuBaraJumlah.DataSource = arrBK;
            lstBatuBaraJumlah.DataBind();

            string[] x = new string[arrBB.Count];
            decimal[] z = new decimal[arrBB.Count];
            decimal[] y = new decimal[arrBB.Count];
            decimal[] xy = new decimal[arrBB.Count];

            int i = 0;


            foreach (Batubara lo in arrBB)
            {
                y[i] = lo.Kgm3;
                x[i] = lo.Nom.ToString();
                xy[i] = lo.Kgm3;

                i = i + 1;

                totalbatubara += lo.QtyBatubara;
                totalm3 += lo.M3;

            }

            hasilpencapaian = totalbatubara / totalm3;

            hasilakhir = (hasilpencapaian / 40) * 100;

            Chart1.Legends.Add("Kgm3");
            Chart1.Series.Add("Kgm3");
            Chart1.Series[0].IsVisibleInLegend = true;
            Chart1.Series[0].IsValueShownAsLabel = true;
            Chart1.Series[0].Points.DataBindXY(x, y);
            Chart1.Series[0].ChartType = SeriesChartType.Column;
            Chart1.Legends[0].Enabled = true;
            Chart1.Legends[0].Docking = Docking.Bottom;

            Chart1.ChartAreas["Area1"].Area3DStyle.Enable3D = false;
            Chart1.ChartAreas["Area1"].AxisY.MajorGrid.LineWidth = 1;
            Chart1.ChartAreas["Area1"].AxisX.MajorGrid.LineWidth = 0;
            Chart1.ChartAreas["Area1"].AxisX.Interval = 1;
            Chart1.ChartAreas["Area1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 9.25F, System.Drawing.FontStyle.Regular);
            Chart1.ChartAreas["Area1"].AxisX.Title = "Tanggal";

            Chart1.Titles.Add(
                new Title(
                    "EFFISIENSI BATU BARA",
                    Docking.Top,
                    new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black
                    )
                    );
            Chart1.Titles.Add(
                new Title(
                    "Periode : " + ddlBulan.SelectedItem.Text + " " + ddlTahun.SelectedValue,
                    Docking.Top,
                    new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black
                    )
                    );


            #region tandatangan
            int levelApv = getAllApv(ddlBulan.SelectedValue, ddlTahun.SelectedItem.Text);

            if (levelApv == 0)
                LoadSignNamePict("admin");
            if (levelApv == 1)
                LoadSignNamePict("head");
            if (levelApv == 2)
                LoadSignNamePict("mgr");


            /** Admin **/
            if (SgnAdmName != string.Empty)
                LblAdmin.Text = SgnAdmName;
            else
                LblAdmin.Text = string.Empty;
            if (PictAdmName != string.Empty)
                Image2.ImageUrl = this.GetAbsoluteUrl("~/Modul/ISO/images/" + PictAdmName);

            else
                Image2.ImageUrl = this.GetAbsoluteUrl("~/Modul/ISO/images/Empty.jpg");

            /** Head **/
            if (SgnMgrName != string.Empty)
                LblMgr.Text = SgnMgrName;

            else
                LblMgr.Text = string.Empty;
            if (PictMgrName != string.Empty)
                Image1.ImageUrl = this.GetAbsoluteUrl("~/Modul/ISO/images/" + PictMgrName);
            else
                Image1.ImageUrl = this.GetAbsoluteUrl("~/Modul/ISO/images/Empty.jpg");

            /** Manager **/
            if (SgnPMName != string.Empty)
                LblPM.Text = SgnPMName;
            else
                LblPM.Text = string.Empty;
            if (PictPMName != string.Empty)
                Image3.ImageUrl = this.GetAbsoluteUrl("~/Modul/ISO/images/" + PictPMName);
            else
                Image3.ImageUrl = this.GetAbsoluteUrl("~/Modul/ISO/images/Empty.jpg");


            #endregion

           
            Image1.Width = (System.Web.UI.WebControls.Unit)(27);
            Image1.Height = (System.Web.UI.WebControls.Unit)(47);
        }


        private string GetAbsoluteUrl(string relativeUrl)
        {
            relativeUrl = relativeUrl.Replace("~/", string.Empty);
            string[] splits = Request.Url.AbsoluteUri.Split('/');
            if (splits.Length >= 2)
            {
                string url = splits[0] + "//";
                for (int i = 4; i < splits.Length - 1; i++)
                {
                    url += splits[2];
                    url += "/";
                }

                return url +relativeUrl;
            }

            return relativeUrl;
        }


        private int getAllApv(string bulan, string tahun)
        {
            int apvAll = 0;
            Users users = (Users)Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select top 1 Approval from SPD_TransPrs where RowStatus>-1 and tahun=" + tahun + " and bulan=" + bulan + " and SarmutPID in " +
                "(select id from SPD_Perusahaan where DeptID in(select id from spd_dept where dptid=" + users.DeptID + ")) order by Approval desc";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    apvAll = Int32.Parse(sdr["Approval"].ToString());
                }
            }
            return apvAll;
        }

        string SgnAdmName = string.Empty;
        string PictAdmName = string.Empty;
        string SgnMgrName = string.Empty;
        string PictMgrName = string.Empty;
        string SgnPMName = string.Empty;
        string PictPMName = string.Empty;

        private void LoadSignNamePict(string sign)
        {
            SgnAdmName = string.Empty;
            PictAdmName = string.Empty;
            SgnMgrName = string.Empty;
            PictMgrName = string.Empty;
            SgnPMName = string.Empty;
            PictPMName = string.Empty;
            Users users = (Users)Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            //zl.CustomQuery = "select * from SPD_Dept where RowStatus>-1 and dptID in (" + users.DeptID + ")order by dept";
            zl.CustomQuery = 
            "select sd.SgnPMName as SgnMgrName,PictPMName as PictMgrName,"+
            "(select UserName from ISO_Users where DeptID=27 and RowStatus >-1 "+
            "and DeptJabatanID in(select ID from ISO_Bagian where BagianName like 'Plant Manager%' and RowStatus >-1)) as SgnPMname, "+
            "case (select plant from ISO_Bagian where BagianName like 'Plant Manager%' and RowStatus >-1) "+
            "when(7) then 'Zuhri.jpg' "+
            "when(13) then 'Tresna-PM-Jombang.jpg' " +
            "when(1) then '' end PictPMName, " +
           
            "SgnAdmName,PictAdmName from SPD_Dept sd where sd.DptID in("+users.DeptID+") and sd.RowStatus >-1";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    if (sign == "admin")
                    {
                        SgnAdmName = sdr["SgnAdmName"].ToString();
                        PictAdmName = sdr["PictAdmName"].ToString();
                    }
                    if (sign == "head")
                    {
                        SgnAdmName = sdr["SgnAdmName"].ToString();
                        PictAdmName = sdr["PictAdmName"].ToString();

                        SgnMgrName = sdr["SgnMgrName"].ToString();
                        PictMgrName = sdr["PictMgrName"].ToString();
                    }
                    if (sign == "mgr")
                    {
                        SgnAdmName = sdr["SgnAdmName"].ToString();
                        PictAdmName = sdr["PictAdmName"].ToString();

                        SgnPMName = sdr["SgnPMName"].ToString();
                        PictPMName = sdr["PictPMName"].ToString();

                        SgnMgrName = sdr["SgnMgrName"].ToString();
                        PictMgrName = sdr["PictMgrName"].ToString();
                    }
                    //if (sign == "pm")
                    //{
                    //    SgnAdmName = sdr["SgnAdmName"].ToString();
                    //    PictAdmName = sdr["PictAdmName"].ToString();

                    //    SgnMgrName = sdr["SgnMgrName"].ToString();
                    //    PictMgrName = sdr["PictMgrName"].ToString();

                    //    SgnPMName = sdr["SgnPMName"].ToString();
                    //    PictPMName = sdr["PictPMName"].ToString();
                    //}
                }
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            

            btnPreview_Click(null, null);
            Image im = (Image)lst.FindControl("Image4");
            Chart ct = (Chart)lst.FindControl("Chart1");
            if (im != null) { ct.Visible = false; }
            if (ct != null) { ct.Visible = false; }
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=Effisiensi Pemakaian Batu Bara " + ddlBulan.SelectedItem.Text + " " +ddlTahun.SelectedValue+" "+DateTime.Now.ToString("HH:mm:ss") +" .xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "";
            string HtmlEnd = "</html>";
            FileInfo fi = new FileInfo(Server.MapPath("~/Scripts/text.css"));
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            StreamReader sr = fi.OpenText();
            while (sr.Peek() >= 0)
            {
                sb.Append(sr.ReadLine());

            }

            string imgPath2 = HttpContext.Current.Request.PhysicalApplicationPath + "\\modul\\sarmut\\Grafik.jpg";

            
            Chart1.SaveImage(imgPath2);
            string imgPath = Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/modul/sarmut/Grafik.jpg");
            string imgPath1 = Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/images/grc.jpg");
            string imgPath0 = Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/images/isosai.jpg");

            string imgPath3 = Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/Modul/ISO/Images/"+ PictAdmName);

            sr.Close();
            Html += "<html><head><style type='text/css'>" + sb.ToString() + "</style></head>";
            Html += "";
            Html += "";

            lst.RenderControl(hw);
            
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0", "border=\"0");
            Contents = Contents.Replace(" angka", "");
            Contents = Contents.Replace(" tengah", "");

            Contents = Contents.Replace("id=\"grc\">", "class=\"textmode\" style=\"height:80px\"><img src='"+ imgPath1 + "' width=\"230\" heigth=\"230\" /> ");
            Contents = Contents.Replace("id=\"spasi\">", "class=\"textmode\">");
            Contents = Contents.Replace("id=\"spasi1\">", "class=\"textmode\">");
            Contents = Contents.Replace("id=\"spasi3\">", "class=\"textmode\">");
            Contents = Contents.Replace("id=\"isosai\">", "class=\"textmode\" style=\"height:80px\"><img src='" + imgPath0 + "' width=\"50\" heigth=\"47\" />");
            Contents = Contents.Replace("id=\"spasi2\">", "class=\"textmode\"> Form no.MTN/K/EBB/57/16/R0");

            Contents = Contents.Replace("id=\"xx\">", "class=\"textmode\" style=\"height:400px\"><img src='" + imgPath + "' width=\"100px\" heigth=\"100px\" />");
            Contents = Contents.Replace("id=\"admin\">", "class=\"textmode\" style=\"height:100px\" \"height:100px\" ><img src='" + imgPath3 + "' width=\"30px\" heigth=\"30px\" />");

            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
            File.Delete(imgPath2);

            
        }

        protected void lstBatuBara_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //Image Image1 = (Image)e.Item.FindControl("Image1");
            //Image Image2 = (Image)e.Item.FindControl("Image2");
            //Image Image3 = (Image)e.Item.FindControl("Image3");

            //height = height * 0.5;
            //width = width * 0.5;

            //Image1.Height = new Unit(height);
            //Image1.Width = new Unit(width);

        }


        protected void lstBatuBaraJumlah_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

    }
}