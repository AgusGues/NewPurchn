using BusinessFacade;
using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.ListReport
{
    public partial class LaporanPemantaunForklift : System.Web.UI.Page
    {
        public string Bulan = Global.nBulan(DateTime.Now.Month);
        private ArrayList arrData = new ArrayList();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadBulan();
                LoadTahun();
                LoadForklift("Forklift", "F" + ((Users)Session["Users"]).UnitKerjaID.ToString());
            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('thr', 450, 99 , 20 ,false); </script>", false);
            //PanelRekap.Visible = false;
        }

        private void PemantauanForkLift()
        {
            Bulan = ddlBulan.SelectedValue;
            PakaiFacade pakaiFacade = new PakaiFacade();
            ArrayList arrData = new ArrayList();
            arrData = pakaiFacade.RetrievePemantauanForkLift(ddlBulan.SelectedValue, ddlTahun.SelectedValue, ddlForkLift.SelectedValue);
            Reportpakaispfl.DataSource = arrData;
            Reportpakaispfl.DataBind();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        public decimal TotalP = 0;
        protected void Reportpakaispfl_databound(object sender, RepeaterItemEventArgs e)
        {
            ForkLift fr = (ForkLift)e.Item.DataItem;
            HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("tr1");
            //TotalP = 0;
            if (e.Item.ItemType == ListItemType.Footer)
            {
                tr = (HtmlTableRow)e.Item.FindControl("tr1");
                //switch (ddlBulan.SelectedValue)
                //{
                //    case "1": tr.Cells[0].ColSpan = 14; break;
                //    case "2": tr.Cells[0].ColSpan = 14; break;
                //    default: tr.Cells[0].ColSpan = 26; break;
                //}
                //tr.Cells[1].InnerText = TotalPES.ToString("N1");

                tr.Cells[1].InnerText = TotalP.ToString("N1");

            }
            if (tr.Cells.Count < 9)
                return;
            int jmlkolom = tr.Cells.Count;
            tr.Cells[(jmlkolom - 1)].InnerHtml = fr.Keterangan.Replace("]", "");
            TotalP += fr.Quantity * fr.AvgPrice;

        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            Bulan = ddlBulan.SelectedItem.Text;
            PemantauanForkLift();
            loadDynamicGrid();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (RBDetail.Checked == true)
            {
                btnPreview_Click(null, null);
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Buffer = true;
                Response.BufferOutput = true;
                Response.AddHeader("content-disposition", "attachment;filename=LapPemantauanSparePartForkLift.xls");
                Response.Charset = "utf-8";
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                string Html = "";
                string HtmlEnd = "</html>";
                FileInfo fi = new FileInfo(Server.MapPath("~/Script/text.css"));
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                StreamReader sr = fi.OpenText();
                while (sr.Peek() >= 0)
                {
                    sb.Append(sr.ReadLine());
                }
                sr.Close();
                Html += "<html><head><style type='text/css'>" + sb.ToString() + "</style></head>";
                Html += "<b>LAPORAN PEMANTAUAN SPARE PART FORKLIFT</b>";
                Html += "<br>Periode : " + ddlBulan.SelectedItem + "  " + ddlTahun.SelectedValue;
                rpl.RenderControl(hw);
                string Contents = sw.ToString();
                Contents = Contents.Replace("border=\"0", "border=\"1").Replace("title=\"\">", ">'");
                Response.Write(Html + Contents + HtmlEnd);
                Response.Flush();
                Response.End();
            }
            else
            {
                if (GrdDynamic.Rows.Count == 0)
                    return;
                Response.ClearContent();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "LapPemantauanSparePartForkLift.xls"));
                Response.ContentType = "application/ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                GrdDynamic.AllowPaging = false;
                GrdDynamic.HeaderRow.Style.Add("background-color", "#FFFFFF");
                for (int i = 0; i < GrdDynamic.HeaderRow.Cells.Count; i++)
                {
                    GrdDynamic.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
                }
                PanelRekap.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();
            }
        }

        private void LoadBulan()
        {
            ddlBulan.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i).ToString(), i.ToString()));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
        }

        private void LoadTahun()
        {
            PakaiFacade pk = new PakaiFacade();
            pk.GetTahun(ddlTahun);
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }

        private void LoadForklift(string Section, string Key)
        {
            string[] arrData = new Inifiles(Server.MapPath("~/App_Data/GroupArmadaOnly.ini")).Read(Key, Section).Split(',');
            if (arrData.Count() > 0)
            {
                ddlForkLift.Items.Clear();
                ddlForkLift.Items.Add(new ListItem("--ALL--", "0"));
                for (int i = 0; i < arrData.Count(); i++)
                {
                    ddlForkLift.Items.Add(new ListItem(arrData[i].ToString(), arrData[i].ToString()));
                }
            }
            else
            {
                ddlForkLift.Items.Clear();
            }

        }
        private void loadDynamicGrid()
        {
            Users users = (Users)Session["Users"];
            string strSQL = string.Empty;
            string wherein = string.Empty;
            string whereinJml = string.Empty;
            string where = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/GroupArmadaOnly.ini")).Read("F" +
                    ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID.ToString(), "Forklift");//
            where = where.Replace(",", "','");
            wherein = "'" + where + "''";
            wherein = wherein.Replace("'", "[");
            wherein = wherein.Replace("[,", "],");
            wherein = wherein.Replace("[[", "]");
            whereinJml = wherein.Replace("[", "isnull([");
            whereinJml = whereinJml.Replace("],", "],0)+") + " Jumlah";
            whereinJml = whereinJml.Replace("] Jumlah", "],0) Jumlah");
            strSQL = "select * ," + whereinJml + " from ( " +
                "SELECT itemcode,itemname,Quantity,keterangan FROM ( " +
                "    SELECT p.pakaidate,(Select dbo.ItemCodeInv(pd.ItemID,pd.ItemTypeID))ItemCode, " +
                "    (Select dbo.ItemNameInv(pd.ItemID,pd.ItemTypeID))ItemName,Quantity, " +
                "    REPLACE(LTRIM(RIGHT(pd.Keterangan,LEN(pd.Keterangan) - CHARINDEX('[',pd.Keterangan) )),']','') as Keterangan FROM PakaiDetail pd  " +
                "    LEFT JOIN Pakai p ON p.ID=pd.PakaiID LEFT JOIN UOM st on pd.UomID=st.ID WHERE left(convert(char,p.Pakaidate,112),6)='202109' AND " +
                "    p.Status>-1 AND pd.RowStatus>-1  " +
                ") AS x WHERE x.Keterangan in('" + where + "')  " +
                ") y pivot ( " +
                "    sum(quantity) for keterangan in (" + wherein + ") " +
                ")z";
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

                bfield.DataField = col.ColumnName;
                bfield.HeaderText = col.ColumnName;
                if (col.ColumnName.Substring(0, 1) == "%")
                {
                    //bfield.HeaderText = col.ColumnName;
                    //bfield.HeaderText = "%";
                    bfield.DataFormatString = "{0:N1}";
                }
                else
                {
                    bfield.DataFormatString = "{0:N0}";
                    //bfield.HeaderText = col.ColumnName;
                }

                GrdDynamic.Columns.Add(bfield);
            }
            GrdDynamic.DataSource = dt;
            GrdDynamic.DataBind();
        }

        protected void RBDetail_CheckedChanged(object sender, EventArgs e)
        {
            if (RBDetail.Checked == true)
            {
                PanelDetail.Visible = true;
                PanelRekap.Visible = false;
            }
        }
        protected void RBRekap_CheckedChanged(object sender, EventArgs e)
        {
            if (RBRekap.Checked == true)
            {
                PanelDetail.Visible = false;
                PanelRekap.Visible = true;
            }
        }
    }
}