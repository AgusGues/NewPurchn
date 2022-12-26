using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;
using Factory;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.IO;

namespace GRCweb1.Modul.ListReportT1T3
{
    public partial class LTransitIn1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.link = "~/Default.aspx";

            if (!Page.IsPostBack)
            {
                txtFromPostingPeriod.Text = DateTime.Now.AddDays(-1).Date.ToString("dd-MMM-yyyy");
                txtToPostingPeriod.Text = DateTime.Now.AddDays(-1).Date.ToString("dd-MMM-yyyy");
                txtFromPostingPeriod0.Text = DateTime.Now.AddDays(-1).Date.ToString("dd-MMM-yyyy");
                txtToPostingPeriod0.Text = DateTime.Now.AddDays(-1).Date.ToString("dd-MMM-yyyy");
                txtFromPostingPeriod1.Text = DateTime.Now.AddDays(-1).Date.ToString("dd-MMM-yyyy");
                txtToPostingPeriod1.Text = DateTime.Now.AddDays(-1).Date.ToString("dd-MMM-yyyy");
            }
         ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(LinkButton3);
        }
        protected void btnPrint0_ServerClick(object sender, EventArgs e)
        {
            loadDynamicGrid();
        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            if (GrdDynamic.Rows.Count == 0)
                return;
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "List Transit Finishing.xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GrdDynamic.AllowPaging = false;
            GrdDynamic.HeaderRow.Style.Add("background-color", "#FFFFFF");
            for (int i = 0; i < GrdDynamic.HeaderRow.Cells.Count; i++)
            {
                GrdDynamic.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
            }
            GrdDynamic.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }

        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            if (txtFromPostingPeriod.Text == string.Empty || txtToPostingPeriod.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Periode Tanggal tidak boleh kosong");
                return;
            }
            string periodeAwal = (txtFromPostingPeriod.Text == string.Empty) ? string.Empty : DateTime.Parse(txtFromPostingPeriod.Text).ToString("yyyyMMdd");
            string periodeAkhir = (txtFromPostingPeriod.Text == string.Empty) ? string.Empty : DateTime.Parse(txtToPostingPeriod.Text).ToString("yyyyMMdd");
            string periodeAwal0 = (txtFromPostingPeriod.Text == string.Empty) ? string.Empty : DateTime.Parse(txtFromPostingPeriod0.Text).ToString("yyyyMMdd");
            string periodeAkhir0 = (txtFromPostingPeriod.Text == string.Empty) ? string.Empty : DateTime.Parse(txtToPostingPeriod0.Text).ToString("yyyyMMdd");
            string periodeAwal1 = (txtFromPostingPeriod.Text == string.Empty) ? string.Empty : DateTime.Parse(txtFromPostingPeriod1.Text).ToString("yyyyMMdd");
            string periodeAkhir1 = (txtFromPostingPeriod.Text == string.Empty) ? string.Empty : DateTime.Parse(txtToPostingPeriod1.Text).ToString("yyyyMMdd");
            string txPeriodeAwal = txtFromPostingPeriod.Text;
            string txPeriodeAkhir = txtToPostingPeriod.Text;

            string tglProduksi = string.Empty;
            string tglSerah = string.Empty;

            string strError = string.Empty;
            int thn = DateTime.Parse(txtFromPostingPeriod.Text).Year;
            int blnLalu = DateTime.Parse(txtFromPostingPeriod.Text).Month;
            string frmtPrint = string.Empty;

            Users users = (Users)Session["Users"];
            Dept dept = new Dept();
            DeptFacade deptfacade = new DeptFacade();
            int test = users.ID;
            dept = deptfacade.RetrieveById(users.DeptID);
            //string deptname = string.Empty;
            //if (dept.DeptName != string.Empty)
            //    deptname = dept.DeptName.Substring(0, 3).ToUpper();
            //else
            //    deptname = " ";

            if (txtFromPostingPeriod.Text == string.Empty || txtToPostingPeriod.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Periode Tanggal tidak boleh kosong");
                return;
            }

            int userID = ((Users)Session["Users"]).ID;

            ReportFacadeT1T3 reportFacade = new ReportFacadeT1T3();
            string strQuery = string.Empty;
            int tgltype = 0;
            if (ChkTglProduksi.Checked == true) //&& ChkTglSerah.Checked ==false 
            {
                tgltype = 1;
                Session["periode"] = "Tgl. Produksi : " + DateTime.Parse(txtFromPostingPeriod.Text).ToString("dd/MM/yyyy") +
                " s/d " + DateTime.Parse(txtToPostingPeriod.Text).ToString("dd/MM/yyyy");
                tglProduksi = " and CONVERT(char(8), B.TglProduksi, 112)>='" + periodeAwal + "' AND CONVERT(char(8), B.TglProduksi, 112)<='" +
                    periodeAkhir + "' ";
            }
            if (ChkTglSerah.Checked == true)//&& ChkTglProduksi.Checked==false
            {
                tgltype = 2;
                Session["periode"] = "Tgl. Serah : " + DateTime.Parse(txtFromPostingPeriod0.Text).ToString("dd/MM/yyyy") +
                " s/d " + DateTime.Parse(txtToPostingPeriod0.Text).ToString("dd/MM/yyyy");
                tglSerah = " and CONVERT(char(8), A.tglserah, 112)>='" + periodeAwal0 + "' AND CONVERT(char(8),  A.tglserah, 112)<='" + periodeAkhir0 + "' ";
            }
            if (ChkTglSerah.Checked == true && ChkTglProduksi.Checked == true)
            {
                Session["periode"] = "Tgl. Produksi : " + DateTime.Parse(txtFromPostingPeriod.Text).ToString("dd/MM/yyyy") +
                " s/d " + DateTime.Parse(txtToPostingPeriod.Text).ToString("dd/MM/yyyy") +
                ", Tgl. Serah : " + DateTime.Parse(txtFromPostingPeriod0.Text).ToString("dd/MM/yyyy") +
                " s/d " + DateTime.Parse(txtToPostingPeriod0.Text).ToString("dd/MM/yyyy");
            }
            if (ChkTglInput.Checked == true)
            {
                tgltype = 2;
                Session["periode"] = "Tgl. Input : " + DateTime.Parse(txtFromPostingPeriod1.Text).ToString("dd/MM/yyyy") +
                " s/d " + DateTime.Parse(txtToPostingPeriod1.Text).ToString("dd/MM/yyyy");
                tglSerah = " and CONVERT(char(8), A.CreatedTime, 112)>='" + periodeAwal1 + "' AND CONVERT(char(8),  A.CreatedTime, 112)<='" + periodeAkhir1 + "' ";
            }
            string partno = string.Empty;
            if (txtPartno.Text.Trim().Length > 10)
                partno = " and (I1.partno='" + txtPartno.Text.Trim() + "' OR I2.partno='" + txtPartno.Text.Trim() + "') ";
            strQuery = reportFacade.ViewLTransitIn(tglProduksi, tglSerah, tgltype, partno);
            Session["Query"] = strQuery;
            Cetak(this);
        }

        static public void Cetak(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LTransitIn', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            string myScript = "Cetak();";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void txtFromPostingPeriod_TextChanged(object sender, EventArgs e)
        {
            txtToPostingPeriod.Text = txtFromPostingPeriod.Text;
        }
        protected void ChkTglProduksi_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkTglProduksi.Checked == true)
            {
                txtFromPostingPeriod.Visible = true;
                txtToPostingPeriod.Visible = true;
                ChkTglInput.Checked = false;
            }
            else
            {
                txtFromPostingPeriod.Visible = false;
                txtToPostingPeriod.Visible = false;
            }
        }
        protected void ChkTglSerah_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkTglSerah.Checked == true)
            {
                txtFromPostingPeriod0.Visible = true;
                txtToPostingPeriod0.Visible = true;
                ChkTglInput.Checked = false;
                GrdDynamic.Visible = true;
                btnPrint0.Visible = true;
                LinkButton3.Visible = true;
            }
            else
            {
                txtFromPostingPeriod0.Visible = false;
                txtToPostingPeriod0.Visible = false;
                GrdDynamic.Visible = false;
                btnPrint0.Visible = false;
                LinkButton3.Visible = false;
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        private void loadDynamicGrid()
        {
            Users users = (Users)Session["Users"];
            string thnBln = string.Empty;
            thnBln = DateTime.Parse(txtFromPostingPeriod0.Text).ToString("yyyyMM");
            string strSQL = string.Empty;
            strSQL = "SELECT A.ID, B.TglProduksi,  A.TglSerah, I1.PartNo AS PartNo1, L2.Lokasi AS Lokasi1, I2.PartNo AS PartNo2, " +
            "L1.Lokasi AS Lokasi2, A.QtyIn AS qty,A.CreatedBy AS [user], P.NoPAlet ,case when left(convert(char,A.tglserah,112),6)<'201904' then J.Oven else A.oven end Oven, " +
            "(I1.Tebal * I1.Lebar * I1.Panjang)/1000000000 * A.QtyIn M3,Ln.PlantName Line,GP.[Group] Grup " +
            "FROM FC_Lokasi AS L1 RIGHT OUTER JOIN T1_Serah AS A ON L1.ID = A.LokID LEFT OUTER JOIN FC_Items AS I2 ON A.itemID = I2.ID LEFT OUTER JOIN  " +
            "FC_Lokasi AS L2 RIGHT OUTER JOIN BM_Palet AS P RIGHT OUTER JOIN BM_Destacking AS B INNER JOIN FC_Items AS I1 ON B.ItemID =  " +
            "I1.ID ON P.ID = B.PaletID ON L2.ID = B.LokasiID ON A.DestID = B.ID left join T1_Jemur J on J.DestID=A.DestID and J.ID=A.JemurID  left join BM_Plant Ln on B.PlantID =Ln.ID  " +
            "left join BM_PlantGroup GP on B.PlantGroupID=GP.ID " +
            "where  A.SFrom<>'lari' and  A.status>-1 and left(convert(char,A.tglserah,112),6)='" + thnBln + "' order by Line,Grup,PartNo1,TglSerah ";
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
            string formdeci = "{0:N1}";
            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();

                bfield.DataField = col.ColumnName; bfield.HeaderText = col.ColumnName;
                //if (col.ColumnName.Substring(0, 1) == "%")
                //{
                //    bfield.HeaderText = "%";
                //    bfield.DataFormatString = "{0:N1}";
                //}
                //else
                //{
                //    bfield.DataFormatString = "{0:N2}";
                //    bfield.HeaderText = col.ColumnName;
                //}
                //if (col.ColumnName.Substring(0, 2) == "M3")
                //{
                //    bfield.HeaderText = "M3";
                //    bfield.DataFormatString = "{0:N4}";
                //}
                GrdDynamic.Columns.Add(bfield);
            }
            GrdDynamic.DataSource = dt;
            GrdDynamic.DataBind();
        }
        protected void txtFromPostingPeriod0_TextChanged(object sender, EventArgs e)
        {
            txtToPostingPeriod0.Text = txtFromPostingPeriod0.Text;
        }
        protected void ChkTglInput_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkTglInput.Checked == true)
            {
                txtFromPostingPeriod1.Visible = true;
                txtToPostingPeriod1.Visible = true;
                ChkTglProduksi.Checked = false;
                txtFromPostingPeriod.Visible = false;
                txtToPostingPeriod.Visible = false;
                ChkTglSerah.Checked = false;
                txtFromPostingPeriod0.Visible = false;
                txtToPostingPeriod0.Visible = false;
            }
            else
            {
                txtFromPostingPeriod1.Visible = false;
                txtToPostingPeriod1.Visible = false;
            }
        }
    }
}