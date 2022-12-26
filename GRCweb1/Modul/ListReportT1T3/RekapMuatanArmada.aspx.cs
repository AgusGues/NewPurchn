using BusinessFacade;
using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.ListReportT1T3
{
    public partial class RekapMuatanArmada : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                txtTglPeriod.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                LoadPlant();

            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }
        private void LoadPlant()
        {
            //ddlPlant.Items.Clear();
            //ArrayList arrArmada = new ArrayList();
            //Expedisi xpdisi = new Expedisi();
            //ExpedisiFacade xpdFacade = new ExpedisiFacade();
            //arrArmada = xpdFacade.RetrieveByHO();
            //ddlPlant.Items.Add(new ListItem("-- Pilih Plant --", "0"));
            //foreach (Expedisi xpd in arrArmada)
            //{
            //    ddlPlant.Items.Add(new ListItem(xpd.ExpedisiName.ToUpper().Trim(), xpd.ID.ToString()));
            //}
            Users user = ((Users)Session["Users"]);
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select  * from [sql1.grcboard.com].GRCboard.dbo.Expedisi where expedisiname in (select expedisiname from armadaexpedisi " +
                "where rowstatus>-1) and RowStatus>-1";
            SqlDataReader sdr = zl.Retrieve();
            ddlPlant.Items.Clear();
            ddlPlant.Items.Add(new ListItem("--Pilih--", "0"));
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlPlant.Items.Add(new ListItem(sdr["ExpedisiName"].ToString().ToUpper().Trim(), sdr["ID"].ToString().TrimEnd()));
                }
            }
        }

        protected void ddlPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPlant.SelectedIndex > 0)
                LoadArmada();

        }
        private void LoadArmada()
        {
            //try
            //{
            ZetroView zl = new ZetroView();
            Users user = ((Users)Session["Users"]);
            zl.QueryType = Operation.CUSTOM;
            if (ddlPlant.SelectedItem.Text.Substring(0, 4).ToUpper() == "BPAS")
            {
                zl.CustomQuery = "select A.ID,A.ExpedisiID ,A.Cartype from [sql1.grcboard.com].GRCboard.dbo.ExpedisiDetail as A , " +
                       "[sql1.grcboard.com].GRCboard.dbo.Expedisi as B where B.ID=A.ExpedisiID and B.ID=" + int.Parse(ddlPlant.SelectedValue) +
                       "  and A.CarType like'%Double%'";
            }
            else
                zl.CustomQuery = "select distinct cast(case when rtrim(S.rate)='' then '0' else rtrim(S.rate) " +
                    "end as int)Cartype,rate from [sql1.grcboard.com].GRCboard.dbo.Schedule S inner join [sql1.grcboard.com].GRCboard.dbo.ExpedisiDetail ED " +
                    "on S.ExpedisiDetailID=ED.ID  inner join [sql1.grcboard.com].GRCboard.dbo.expedisi E on ED.ExpedisiID =E.ID " +
                    "where convert(char,scheduledate,112)=" + DateTime.Parse(txtTglPeriod.Text).ToString("yyyyMMdd") + " and S.DepoID=" + user.UnitKerjaID + " and E.ID=" + int.Parse(ddlPlant.SelectedValue) + "order by Cartype";
            SqlDataReader sdr = zl.Retrieve();
            ddlArmada.Items.Clear();
            if (ddlPlant.SelectedItem.Text.Substring(0, 4).ToUpper() == "BPAS")
                ddlArmada.Items.Add(new ListItem("--Pilih--", "0"));
            else
                ddlArmada.Items.Add(new ListItem("--Pilih--", "-1"));
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    if (ddlPlant.SelectedItem.Text.Substring(0, 4).ToUpper() == "BPAS")
                        ddlArmada.Items.Add(new ListItem(sdr["CarType"].ToString(), sdr["ID"].ToString()));
                    else
                        ddlArmada.Items.Add(new ListItem("Rit : " + sdr["CarType"].ToString(), sdr["Rate"].ToString()));
                }
            }
            lblPlant.Text = ddlPlant.SelectedItem.Text;
            //}
            //catch { }
        }
        protected void lstArmada_DataBound(object sender, RepeaterItemEventArgs e)
        {
            //Repeater ListArmada1 = (Repeater)e.Item.FindControl("ListArmada1");
            //Label lbl = (Label)e.Item.FindControl("lblCarType");
            //string myString = lbl.Text.ToString();
            //string modifiedString = myString.ToUpper();
            string tglMuat = string.Empty;
            tglMuat = DateTime.Parse(txtTglPeriod.Text).ToString("yyyyMMdd");
            Repeater ListArmada1 = (Repeater)e.Item.FindControl("ListArmada1");
            Label lblarmada = (Label)e.Item.FindControl("lblCarType");
            Expedisi exp = (Expedisi)e.Item.DataItem;
            ExpedisiFacade expF = new ExpedisiFacade();
            ArrayList arrData = new ArrayList();
            //arrData = expF.RetrieveAllMuatan(tglMuat, ddlPlant.SelectedValue, ddlArmada.SelectedValue);
            if (ddlPlant.SelectedItem.Text.Substring(0, 4).ToUpper() == "BPAS")
                arrData = expF.RetrieveAllMuatan(tglMuat, ddlPlant.SelectedValue, exp.ID.ToString());
            else
                arrData = expF.RetrieveAllMuatan1(tglMuat, ddlPlant.SelectedValue, lblarmada.Text, ddlPlant.SelectedValue);
            ListArmada1.DataSource = arrData;
            ListArmada1.DataBind();

        }
        protected void ListArmada1_DataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void ListArmada1_Command(object sender, RepeaterCommandEventArgs e)
        {

        }
        private ArrayList arrData = new ArrayList();

        private void ListMuatan()
        {
            lbltglSch.Text = DateTime.Parse(txtTglPeriod.Text).ToString("dd/MM/yyyy");
            ExpedisiFacade exped = new ExpedisiFacade();
            arrData = new ArrayList();
            arrData = exped.RetrieveRecapMuatan(ddlArmada.SelectedValue.ToString());
            lstArmada.DataSource = arrData;
            lstArmada.DataBind();
        }
        private void ListMuatan1()
        {
            lbltglSch.Text = DateTime.Parse(txtTglPeriod.Text).ToString("dd/MM/yyyy");
            ExpedisiFacade exped = new ExpedisiFacade();
            arrData = new ArrayList();
            arrData = exped.RetrieveRecapMuatan1(ddlArmada.SelectedValue, ddlPlant.SelectedValue.ToString(), DateTime.Parse(txtTglPeriod.Text).ToString("yyyyMMdd"));
            lstArmada.DataSource = arrData;
            lstArmada.DataBind();
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            if (ddlPlant.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Pilih Plant !!!");
                return;
            }
            if (ddlPlant.SelectedItem.Text.Substring(0, 4).ToUpper() == "BPAS")
                ListMuatan();
            else
                ListMuatan1();
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=ListMuatanDouble.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<H3>FORM MUATAN ARMADA DOUBLE BPAS</H3>";
            //Html += "<br>Tanggal : " + ddlSemester.SelectedItem.Text + " &nbsp; " + ddlTahun.SelectedValue.ToString();
            Html += "<br>Tanggal : &nbsp; " + txtTglPeriod.Text;
            //Html += "<br>Departement : &nbsp;" + ddlDept.SelectedItem.Text;
            Html += "";
            string HtmlEnd = "";
            div2.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0\"", "border=\"1\"");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }

        protected void txtTglPeriod_TextChanged(object sender, EventArgs e)
        {
            LoadPlant();
            ddlArmada.Items.Clear();
        }
    }
}