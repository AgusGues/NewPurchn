using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
//using System.Drawing;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using DefectFacade;
using Factory;
using DataAccessLayer;
using System.IO;
using BasicFrame.WebControls;
using System.Text.RegularExpressions;
using System.Collections.Specialized;

namespace GRCweb1.Modul.Sarmut
{
    public partial class PemantauanAnalisaData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadDept();
                txtTgl1.Text = "01-Jan-" + DateTime.Now.ToString("yyyy");
                txtTgl2.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }

        private void LoadDept()
        {
            Users users = (Users)Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select * from SPD_Dept where RowStatus>-1 order by dept";
            SqlDataReader sdr = zl.Retrieve();
            ddlDepartemen.Items.Add(new ListItem("-- ALL --", "0"));
            //ddlDepartemen.Items.Clear();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlDepartemen.Items.Add(new ListItem(sdr["Dept"].ToString(), sdr["ID"].ToString()));
                }
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            //LoadData();
            Response.AddHeader("content-disposition", "attachment;filename=PemantauanAnalisaData.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<b>PEMANTAUAN ANALISA DATA</b>";
            Html += "<br>Departemen : " + ddlDepartemen.SelectedItem.Text;
            string HtmlEnd = "";
            Div6.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("xx\">", "\">\'");
            Contents = Contents.Replace("border=\"0", "border=\"1");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void BtnPreview2_Click(object sender, EventArgs e)
        {
            LihatData();
        }

        public int newdata = 0;
        private void LihatData()
        {
            string kriteria = string.Empty;
            if (ddlDepartemen.SelectedValue != "0")
                kriteria = kriteria + " and A.DeptID=" + ddlDepartemen.SelectedValue;
            ArrayList arrData = new ArrayList();
            newdata = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = " select A.DeptID,A.CreatedTime,A.ID ID,isnull(C.Dept,'')Dept,A.AnNo,A.TglAnalisa,A.SarmutPID,B.SarMutPerusahaan,A.SarmutDeptID, " +
                            " isnull(D.SarmutDepartemen,'')SarmutDepartemen,A.TargetVID,A.ParamID,A.SatuanID,A.TypeSarmutID,E.JenisSarmut Jenis,A.Apv," +
                            " isnull(D.SarmutDepartemen,'')SarmutDepartemen,A.TargetVID,A.ParamID,A.SatuanID,A.TypeSarmutID,E.JenisSarmut Jenis,A.Apv," +
                            " case when Apv=0 then 'Admin' when apv is null then 'Admin' " +
                            " when apv=1 then 'Manager Dept' when apv=2 then 'ISO' end Approval,isnull(A.CLosed,0)Closed,isnull(A.Close_Date,'1/1/1900')Close_Date," +
                            " isnull(A.Solved,0)Solved,Case when A.Solved=0 then 'Open' when A.Solved=1 then 'Closed' end [Status],isnull(A.Solve_Date,'1/1/1900')Solve_Date," +
                            " A.Bulan, " +
                            " case " +
                            " when A.Bulan=1 then 'JANUARI' " +
                            " when A.Bulan=2 then 'FEBRUARI' " +
                            " when A.Bulan=3 then 'MARET' " +
                            " when A.Bulan=4 then 'APRIL' " +
                            " when A.Bulan=5 then 'MEI' " +
                            " when A.Bulan=6 then 'JUNI' " +
                            " when A.Bulan=7 then 'JULI' " +
                            " when A.Bulan=8 then 'AGUSTUS' " +
                            " when A.Bulan=9 then 'SEPTEMBER' " +
                            " when A.Bulan=10 then 'OKTOBER' " +
                            " when A.Bulan=11 then 'NOVEMBER' " +
                            " when A.Bulan=12 then 'DESEMBER' " +
                            " end NamaBulan , " +
                            " A.Tahun,A.Actual,case when A.Kesim=1 then 'Tercapai' else 'Tidak Tercapai' end Ket,A.Kesim,A.RowStatus," +
                            " (select COUNT(ID)verified from SPD_Tindakan where Verifikasi=1 and SPDAnalisaID=A.ID)Verifikasi," +
                            " (select COUNT(ID)verified from SPD_Tindakan where Verifikasi=0 and SPDAnalisaID=A.ID)notverified," +
                            " isnull((select top 1 Jadwal_Selesai from SPD_Tindakan where SPDAnalisaID =A.ID and RowStatus>-1 order by Jadwal_Selesai desc),'1/1/1900') due_date " +
                            " from SPD_Analisa A  left join SPD_Perusahaan B on A.SarmutPID=B.ID left join SPD_Dept C on A.Deptid=C.ID " +
                            " left join SPD_Departemen D on A.SarmutDeptID=D.ID inner join SPD_Type E on A.TypeSarmutID=E.ID  " +
                            " where A.RowStatus>-1 and convert(char,A.TglAnalisa,112)>='" +
                            Convert.ToDateTime(txtTgl1.Text.Trim()).ToString("yyyyMMdd") + "' and convert(char,A.TglAnalisa,112)<='" +
                             Convert.ToDateTime(txtTgl2.Text.Trim()).ToString("yyyyMMdd") + "'" + kriteria;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new AnalisaDataX
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        AnNo = sdr["AnNo"].ToString(),
                        TglAnalisa = Convert.ToDateTime(sdr["TglAnalisa"].ToString()),
                        Dept = sdr["Dept"].ToString(),
                        SarmutPerusahaan = sdr["SarmutPerusahaan"].ToString(),
                        SarmutDepartemen = sdr["SarmutDepartemen"].ToString(),
                        TargetVID = Convert.ToDecimal(sdr["TargetVID"].ToString()),
                        ParamID = sdr["ParamID"].ToString(),
                        SatuanID = sdr["SatuanID"].ToString(),
                        Jenis = sdr["Jenis"].ToString(),
                        Status = sdr["Status"].ToString()
                        //Uraian = sdr["Uraian"].ToString(),
                        //Penyebab = sdr["Penyebab"].ToString()
                    });
                }
            }

            //ZetroView zl1 = new ZetroView();
            //zl1.QueryType = Operation.CUSTOM;
            //zl1.CustomQuery = " select C.AnNo,A.Penyebab,B.Uraian from SPD_Analisa_Penyebab A inner join SPD_Analisa_Penyebab_Detail B on A.ID=B.Penyebab_ID " +
            //                  " inner join SPD_Analisa C on C.ID=B.SPDAnalisaID " +
            //                  " where C.RowStatus>-1 and B.RowStatus >-1 ";
            //SqlDataReader sdrx = zl1.Retrieve();
            //if (sdrx.HasRows)
            //{
            //    while (sdrx.Read())
            //    {
            //        arrData.Add(new AnalisaDataX
            //        {
            //            //ID = Convert.ToInt32(sdr["ID"].ToString()),
            //            //AnNo = sdr["AnNo"].ToString(),
            //            //TglAnalisa = Convert.ToDateTime(sdr["TglAnalisa"].ToString()),
            //            //Dept = sdr["Dept"].ToString(),
            //            //SarmutPerusahaan = sdr["SarmutPerusahaan"].ToString(),
            //            //SarmutDepartemen = sdr["SarmutDepartemen"].ToString(),
            //            //TargetVID = Convert.ToDecimal(sdr["TargetVID"].ToString()),
            //            //ParamID = sdr["ParamID"].ToString(),
            //            //SatuanID = sdr["SatuanID"].ToString(),
            //            //Jenis = sdr["Jenis"].ToString(),
            //            //Uraian = sdrx["Uraian"].ToString()
            //        });
            //    }
            //}
            lstR2.DataSource = arrData;
            lstR2.DataBind();

        }

        protected void lstR2_Command(object sender, RepeaterCommandEventArgs e)
        {

        }

        protected void lstR2_Databound(object sender, RepeaterItemEventArgs e)
        {
            Repeater rpts = (Repeater)e.Item.FindControl("attachPrs");
            Repeater rpts0 = (Repeater)e.Item.FindControl("attachPrs2");
            Repeater rpts1 = (Repeater)e.Item.FindControl("attachPrs3");
            Repeater rpts2 = (Repeater)e.Item.FindControl("attachPrs4");
            Repeater rpts3 = (Repeater)e.Item.FindControl("attachPrs5");
            ImageButton imgs = (ImageButton)e.Item.FindControl("attPrs");
            LoadListAll(imgs.CssClass.ToString(), rpts, rpts0);
            LoadListAll0(imgs.CssClass.ToString(), rpts1, rpts2, rpts3);

        }

        private void LoadListAll(string BAID, Repeater attachPrs, Repeater attachPrs2)
        {
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            if (Convert.ToInt32(BAID) == 0)
                return;
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = " select C.AnNo,A.Penyebab,B.Uraian from SPD_Analisa_Penyebab A inner join SPD_Analisa_Penyebab_Detail B on A.ID=B.Penyebab_ID " +
                              " inner join SPD_Analisa C on C.ID=B.SPDAnalisaID " +
                              " where C.RowStatus>-1 and B.RowStatus >-1 and B.SPDAnalisaID=" + BAID;
            SqlDataReader sdrx = zl.Retrieve();
            if (sdrx.HasRows)
            {
                while (sdrx.Read())
                {
                    arrData.Add(new AnalisaDataX
                    {
                        Penyebab = sdrx["Penyebab"].ToString(),
                        Uraian = sdrx["Uraian"].ToString()
                    });
                }
            }
            attachPrs.DataSource = arrData;
            attachPrs.DataBind();
            attachPrs2.DataSource = arrData;
            attachPrs2.DataBind();
        }

        private void LoadListAll0(string BAID, Repeater attachPrs3, Repeater attachPrs4, Repeater attachPrs5)
        {
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            if (Convert.ToInt32(BAID) == 0)
                return;
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = " select B.AnNo,A.Jenis,A.Tindakan,isnull(A.TglVerifikasi,'1/1/1900')Tanggal  from SPD_Tindakan A inner join SPD_Analisa B " +
                            " on A.SPDAnalisaID=B.ID where A.RowStatus>-1  and B.ID=" + BAID;
            SqlDataReader sdrx = zl.Retrieve();
            if (sdrx.HasRows)
            {
                while (sdrx.Read())
                {
                    arrData.Add(new AnalisaDataX
                    {
                        Tanggal = Convert.ToDateTime(sdrx["Tanggal"].ToString()),
                        Tindakan = sdrx["Tindakan"].ToString(),
                        Jenis = sdrx["Jenis"].ToString()
                    });
                }
            }
            attachPrs3.DataSource = arrData;
            attachPrs3.DataBind();
            attachPrs4.DataSource = arrData;
            attachPrs4.DataBind();
            attachPrs5.DataSource = arrData;
            attachPrs5.DataBind();
        }

        protected void BtnPreview_Click(object sender, EventArgs e)
        {
            string kriteria = string.Empty;
            if (ddlDepartemen.SelectedValue != "0")
                kriteria = kriteria + " and A.DeptID=" + ddlDepartemen.SelectedValue;

            string strSQL = " select A.DeptID,A.CreatedTime,A.ID ID,isnull(C.Dept,'')Dept,A.AnNo,A.TglAnalisa,A.SarmutPID,B.SarMutPerusahaan,A.SarmutDeptID, " +
                            " isnull(D.SarmutDepartemen,'')SarmutDepartemen,A.TargetVID,A.ParamID,A.SatuanID,A.TypeSarmutID,E.JenisSarmut Jenis,A.Apv," +
                            " case when Apv=0 then 'Admin' when apv is null then 'Admin' " +
                            " when apv=1 then 'Manager Dept' when apv=2 then 'ISO' end Approval,isnull(A.CLosed,0)Closed,isnull(A.Close_Date,'1/1/1900')Close_Date," +
                            " isnull(A.Solved,0)Solved,Case when A.Solved=0 then 'Open' when A.Solved=1 then 'Closed' end [Status],isnull(A.Solve_Date,'1/1/1900')Solve_Date," +
                            " A.Bulan, " +
                            " case " +
                            " when A.Bulan=1 then 'JANUARI' " +
                            " when A.Bulan=2 then 'FEBRUARI' " +
                            " when A.Bulan=3 then 'MARET' " +
                            " when A.Bulan=4 then 'APRIL' " +
                            " when A.Bulan=5 then 'MEI' " +
                            " when A.Bulan=6 then 'JUNI' " +
                            " when A.Bulan=7 then 'JULI' " +
                            " when A.Bulan=8 then 'AGUSTUS' " +
                            " when A.Bulan=9 then 'SEPTEMBER' " +
                            " when A.Bulan=10 then 'OKTOBER' " +
                            " when A.Bulan=11 then 'NOVEMBER' " +
                            " when A.Bulan=12 then 'DESEMBER' " +
                            " end NamaBulan , " +
                            " A.Tahun,A.Actual,case when A.Kesim=1 then 'Tercapai' else 'Tidak Tercapai' end Ket,A.Kesim,A.RowStatus," +
                            " (select COUNT(ID)verified from SPD_Tindakan where Verifikasi=1 and SPDAnalisaID=A.ID)Verifikasi," +
                            " (select COUNT(ID)verified from SPD_Tindakan where Verifikasi=0 and SPDAnalisaID=A.ID)notverified," +
                            " isnull((select top 1 Jadwal_Selesai from SPD_Tindakan where SPDAnalisaID =A.ID and RowStatus>-1 order by Jadwal_Selesai desc),'1/1/1900') due_date " +
                            " from SPD_Analisa A  left join SPD_Perusahaan B on A.SarmutPID=B.ID left join SPD_Dept C on A.Deptid=C.ID " +
                            " left join SPD_Departemen D on A.SarmutDeptID=D.ID inner join SPD_Type E on A.TypeSarmutID=E.ID " +
                            " where A.RowStatus>-1 and convert(char,A.TglAnalisa,112)>='" +
                            Convert.ToDateTime(txtTgl1.Text.Trim()).ToString("yyyyMMdd") + "' and convert(char,A.TglAnalisa,112)<='" +
                             Convert.ToDateTime(txtTgl2.Text.Trim()).ToString("yyyyMMdd") + "'" + kriteria;
            //" order by A.TglAnalisa Desc,C.Dept ";

            string strSQL1 = " select C.AnNo,A.Penyebab,B.Uraian from SPD_Analisa_Penyebab A inner join SPD_Analisa_Penyebab_Detail B on A.ID=B.Penyebab_ID " +
                             " inner join SPD_Analisa C on C.ID=B.SPDAnalisaID " +
                             " where C.RowStatus>-1 and B.RowStatus >-1 ";

            string strSQL2 = " select B.AnNo,A.Jenis,A.Tindakan,A.TglVerifikasi Tanggal  from SPD_Tindakan A inner join SPD_Analisa B  " +
                             " on A.SPDAnalisaID=B.ID where A.RowStatus>-1 ";

            if (txtTgl1.Text.Trim() != string.Empty)
            {
                Session["Query"] = strSQL;
                Session["Query1"] = strSQL1;
                Session["Query2"] = strSQL2;
                Session["periode"] = txtTgl1.Text.Trim() + " s/d " + txtTgl2.Text.Trim();
                Cetak(this);
            }
        }

        static public void Cetak(Control page)
        {
            string myScript = "var wn = window.showModalDialog('../Sarmut/Report.aspx?IdReport=pemantauanAdata', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
    }

    public class AnalisaDataX : GRCBaseDomain
    {
        public string Dept { get; set; }
        public DateTime TglAnalisa { get; set; }
        public string AnNo { get; set; }
        public string SarmutPerusahaan { get; set; }
        public string SarmutDepartemen { get; set; }
        public decimal TargetVID { get; set; }
        public string ParamID { get; set; }
        public string SatuanID { get; set; }
        public string Jenis { get; set; }
        public string Uraian { get; set; }
        public int SarmutPID { get; set; }
        public int SarmutDeptID { get; set; }
        public int TypeSarmutID { get; set; }
        public int Apv { get; set; }
        public string Approval { get; set; }
        public int Closed { get; set; }
        public DateTime Close_Date { get; set; }
        public int Solved { get; set; }
        public string Status { get; set; }
        public DateTime Solve_Date { get; set; }
        public int Bulan { get; set; }
        public string NamaBulan { get; set; }
        public int Tahun { get; set; }
        public Decimal Actual { get; set; }
        public string Ket { get; set; }
        public int Kesim { get; set; }
        public int Verifikasi { get; set; }
        public int notverified { get; set; }
        public DateTime due_date { get; set; }
        public string Penyebab { get; set; }
        public string Tindakan1 { get; set; }
        public string Tindakan2 { get; set; }
        public DateTime Tanggal { get; set; }
        public string Tindakan { get; set; }
    }
}