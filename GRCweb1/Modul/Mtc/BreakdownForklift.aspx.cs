using BusinessFacade;
using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.Mtc
{
    public partial class BreakdownForklift : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Users users = (Users)Session["Users"];
                if (users.DeptID == 3 || users.DeptID == 2 || users.DeptID == 6)
                {
                    btnrekap.Visible = false;
                    btnprodinput.Visible = false;
                }
                LoadBulan();
                LoadTahun();
                panelOperasional.Visible = false;
                panelrekap.Visible = false;
                txtTanggal.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
        }
        public void LoadTahun()
        {
            ddlTahun.Items.Clear();
            var currentYear = DateTime.Today.Year;
            for (int i = 2; i >= 0; i--)
            {
                ddlTahun.Items.Add((currentYear - i).ToString());
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }

        private void LoadBulan()
        {
            ddlBulan.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i).ToString(), i.ToString().PadLeft(2, '0')));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString().PadLeft(2, '0');
        }
        public void LoadOperasionalforklift()
        {
            int ada = 0;
            string queryadd;
            ArrayList arrData = new ArrayList();
            ZetroView zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery = " select count(id)hasil from MTC_Operasional_Forklift where Tanggal='" + Convert.ToDateTime(txtTgl.Text).ToString("yyyyMMdd") + "' " +
                              " and RowStatus>-1";
            SqlDataReader sdr2 = zl1.Retrieve();
            if (sdr2.HasRows)
            {
                while (sdr2.Read())
                {
                    ada = Convert.ToInt32(sdr2["hasil"].ToString());
                }
            }
            if (ada == 0)
            {
                queryadd = " select a.Forklift,(select isnull((select id from MTC_Operasional_Forklift where Tanggal=@tgl and Forklift=a.Forklift),0))ID, " +
                           " (select isnull((select total from MTC_Operasional_Forklift where Tanggal = @tgl and Forklift = a.Forklift), 0))total " +
                           " from MasterForklift a where a.rowstatus > -1";
            }
            else
            {
                queryadd = " select ID,Forklift,Total from  MTC_Operasional_Forklift  where Tanggal=@tgl and rowstatus>-1  order by id";
            }

            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
             zl.CustomQuery = " declare  @tgl datetime; set @tgl='" + Convert.ToDateTime(txtTgl.Text).ToString("yyyyMMdd") + "' " + queryadd;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new BD_Forklift.DProdForklift
                    {
                        id = Convert.ToInt32(sdr["ID"].ToString()),
                        prodForklift = sdr["Forklift"].ToString(),
                        Total = Convert.ToInt32(sdr["Total"].ToString())
                    });
                }
            }
            lstOperasional.DataSource = arrData;
            lstOperasional.DataBind();

        }
        protected void lstOperasional_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            Users users = (Users)Session["Users"];
            BD_Forklift.DProdForklift p = (BD_Forklift.DProdForklift)e.Item.DataItem;
            Label lbl = (Label)e.Item.FindControl("lblid");

            if (p.id == 0)
            {
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = " insert MTC_Operasional_Forklift(Tanggal,forklift,total,rowstatus,createdby,createdtime) " +
                                " values('" + Convert.ToDateTime(txtTgl.Text).ToString("yyyyMMdd") + "' ,'" + p.prodForklift + "','0','0','" + users.UserName + "',getdate())";

                SqlDataReader sdr = zl.Retrieve();
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery = "select top 1 id from MTC_Operasional_Forklift order by id desc";
                SqlDataReader sdr2 = zl1.Retrieve();
                if (sdr2.HasRows)
                {
                    while (sdr2.Read())
                    {
                        lbl.ToolTip = sdr2["id"].ToString();
                    }
                }
            }
            else
            {
                lbl.ToolTip = p.id.ToString();
            }
            
        }
        [WebMethod]
        public static string GetUser()
        {
            Users user = (Users)HttpContext.Current.Session["Users"];
            string createdby = user.UserName;

            return createdby;
        }
        [WebMethod]
        public static List<BD_Forklift> GetForklift()
        {
            List<BD_Forklift> forklift = new List<BD_Forklift>();
            forklift = BDForkliftFacade.GetFroklift();
            //return JsonConvert.SerializeObject(pic);
            return forklift;
        }
        [WebMethod]
        public static int Simpan(BD_Forklift obj)
        {
            int insert;
            DateTime Tanggal = obj.Tanggal;
            string Forklift = obj.Forklift;
            string Start = obj.Start;
            string Finish = obj.Finish;
            int Total = obj.Total;
            string Kendala = obj.Kendala;
            string Perbaikan = obj.Perbaikan;
            string Keterangan = obj.Keterangan;
            string Users = obj.users;
            BDForkliftFacade bdff = new BDForkliftFacade();
            insert = bdff.Insertbreakdown(Tanggal, Forklift, Start, Finish, Total, Kendala, Perbaikan, Keterangan, Users);
            return insert;
        }

        protected void btnprevprod_Click(object sender, EventArgs e)
        {
            LoadOperasionalforklift();
        }

        protected void btnprevRekap_Click(object sender, EventArgs e)
        {
            getoperasional();
            LoadRekapForklift();
        }

        public void LoadRekapForklift()
        {
            string thnbln = ddlTahun.Text + ddlBulan.SelectedValue;
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            //zl.CustomQuery = "select * from pantaupackingmaster pm left join pantaupacking p on pm.id=p.p_id where pm.jenis_packing='" + JenisPacking + "' and and p.rowstatus>0 and pm.rowstatus>-1 order by id";
            zl.CustomQuery = "select id,Tanggal,Forklift,Start,Finish,Total,Kendala,Perbaikan,Keterangan from MTC_Break_Forklift where RowStatus>-1 and left(convert(char,Tanggal,112),6)='"+thnbln+"'order by tanggal";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new BD_Forklift
                    {
                        Id=Convert.ToInt32(sdr["id"].ToString()),
                        Tanggal = Convert.ToDateTime(sdr["Tanggal"].ToString()),
                        Forklift = sdr["Forklift"].ToString(),
                        Start = sdr["start"].ToString(),
                        Finish = sdr["finish"].ToString(),
                        Total = Convert.ToInt32(sdr["Total"].ToString()),
                        Kendala = sdr["kendala"].ToString(),
                        Perbaikan = sdr["perbaikan"].ToString(),
                        Keterangan = sdr["keterangan"].ToString()
                    });
                }
            }
            lstrekap.DataSource = arrData;
            lstrekap.DataBind();
        }
        
        public void getoperasional()
        {
            string thnbln = ddlTahun.Text + ddlBulan.SelectedValue;
            ZetroView zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery = "select (sum(total)*420)total from MTC_Operasional_Forklift where RowStatus>-1 and left(convert(char,Tanggal,112),6)='" + thnbln + "'";
            SqlDataReader sdr2 = zl1.Retrieve();
            if (sdr2.HasRows)
            {
                while (sdr2.Read())
                {
                    lbltotalop.Text = sdr2["total"].ToString();
                    lbltotalop1.Text = sdr2["total"].ToString();
                }
            }
        }
        decimal totalrekap = 0;
        protected void lstrekap_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            decimal a =Convert.ToDecimal(lbltotalop.Text);
            BD_Forklift p = (BD_Forklift)e.Item.DataItem;
            Label lbl = (Label)e.Item.FindControl("lblid2");
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("ps2");
                    if (tr != null)
                    {
                        totalrekap += p.Total;
                        lbltotalbd.Text = totalrekap.ToString();
                        lbltotalrekap.Text = totalrekap.ToString();
                        lbltotalall.Text = (totalrekap/ a*100).ToString("N2")+" %";
                        
                    }
                }
            }
            catch
            {

            }
        }

        protected void btnnewprod_Click(object sender, EventArgs e)
        {

        }

        protected void btnsaveprod_Click(object sender, EventArgs e)
        {
            Regex pattern = new Regex("[,]");
            Regex pattern1 = new Regex("[.]");

            int i = 0;
            foreach (RepeaterItem objItem in lstOperasional.Items)
            {
                Label lbl = (Label)lstOperasional.Items[i].FindControl("lblid");
                TextBox txtTarget = (TextBox)lstOperasional.Items[i].FindControl("txtTarget");
                ZetroView zl = new ZetroView();
                if (decimal.Parse(txtTarget.Text) > 3)
                {
                    return;
                }
                else
                {
                    zl.QueryType = Operation.CUSTOM;
                    zl.CustomQuery = "update MTC_Operasional_Forklift " +
                        " set Total =" + pattern.Replace(decimal.Parse(txtTarget.Text).ToString(), ".") +
                         " where ID=" + lbl.ToolTip;
                    SqlDataReader sdr = zl.Retrieve();
                    i++;
                }
            }
        }

        protected void btnexport_Click(object sender, EventArgs e)
        {
            string Periode = ddlBulan.SelectedItem.ToString() + " " + ddlTahun.SelectedItem.ToString();

            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=Breakdown_forklift.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<center> LEMBAR PEMANTAUAN BREAKDOWN TIME FORKLIFT </center>";
            Html += "<br><center>Periode : " + ddlBulan.SelectedItem.Text + "  " + ddlTahun.SelectedValue.ToString()+ "</center>";
            string HtmlEnd = "<br>";
            HtmlEnd += " &emsp;&emsp;&emsp;";
            HtmlEnd += "Dibuat,";
            HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
            HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
            HtmlEnd += "Menyetujui";
            HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
            HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
            HtmlEnd += " &emsp;&emsp;&emsp;&emsp; ";
            HtmlEnd += "Mengetahui";
            HtmlEnd += "<br>";
            HtmlEnd += "<br>";
            HtmlEnd += "<br>";
            HtmlEnd += "<br>";
            HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
            HtmlEnd += "Manager Logistik";
            HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
            HtmlEnd += "Manager Boardmill";
            HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
            HtmlEnd += "Manager Finishing";
            HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
            HtmlEnd += "Manager Maintenance";
            HtmlEnd += "<br>";
            HtmlEnd += "<br>";
            HtmlEnd += "<br>";
            HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
            HtmlEnd += "Total Operasional &emsp;&emsp;&emsp;" + lbltotalop.Text + "Menit <br>";
            HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
            HtmlEnd += "Total Breakdown";
            HtmlEnd += "<br><br>";
            HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
            HtmlEnd += "Presentase &emsp;&emsp;&emsp; (" + lbltotalbd.Text + " / " + lbltotalop1.Text + ") X 100% <br>";
            HtmlEnd += " &emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp; ";
            HtmlEnd += "&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;= " + lbltotalall.Text+" ";
            rekap.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0", "border=\"1");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();

        }

        protected void btnrekap_Click(object sender, EventArgs e)
        {
            panelinput.Visible = false;
            panelrekap.Visible = true;
            panelOperasional.Visible = false;
        }

        protected void btnprodinput_Click(object sender, EventArgs e)
        {
            panelinput.Visible = false;
            panelOperasional.Visible = true;
            panelrekap.Visible = false;
        }

        protected void btnnew_Click(object sender, EventArgs e)
        {

        }

        protected void btninput_Click(object sender, EventArgs e)
        {
            panelinput.Visible = true;
            panelOperasional.Visible = false;
            panelrekap.Visible = false;
        }

        protected void btnrekap1_Click(object sender, EventArgs e)
        {
            panelrekap.Visible = true;
            panelOperasional.Visible = false;
            panelinput.Visible = false;
        }

        protected void btninputBD2_Click(object sender, EventArgs e)
        {
            panelinput.Visible = true;
            panelOperasional.Visible = false;
            panelrekap.Visible = false;
        }

        protected void btninputprod2_Click(object sender, EventArgs e)
        {
            panelrekap.Visible = false;
            panelOperasional.Visible = true;
            panelinput.Visible = false;
        }
    }
}