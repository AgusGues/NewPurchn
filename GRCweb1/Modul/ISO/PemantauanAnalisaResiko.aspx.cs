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
using System.Drawing;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
//using DefectFacade;
using Factory;
using DataAccessLayer;
using System.IO;
using BasicFrame.WebControls;

namespace GRCweb1.Modul.ISO
{
    public partial class PemantauanAnalisaResiko : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadDept();
                LoadTahun();
                //LoadMasalah();
                //txtdrtanggal.Text = "01-Jan-" + DateTime.Now.ToString("yyyy");
                //txtsdtanggal.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
        }

        private void LoadDept()
        {
            ddlDepartemen.Items.Clear();
            ArrayList arrDept = new ArrayList();
            TPP_Dept tppdept = new TPP_Dept();
            TPP_DeptFacade deptFacade = new TPP_DeptFacade();
            arrDept = deptFacade.Retrieve();
            ddlDepartemen.Items.Add(new ListItem("ALL", "0"));
            foreach (TPP_Dept dept in arrDept)
            {
                ddlDepartemen.Items.Add(new ListItem(dept.Departemen.ToUpper().Trim(), dept.ID.ToString()));
            }
            Users user = ((Users)Session["Users"]);
            tppdept = deptFacade.GetUserDept(user.ID);
            ddlDepartemen.SelectedValue = tppdept.ID.ToString();
        }

        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            if (ddlSemester.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Pilih Semester");
                return;
            }

            Users users = (Users)Session["Users"]; string Nomor = string.Empty;
            string kriteria = string.Empty;
            if (ddlDepartemen.SelectedValue != "0")
                kriteria = kriteria + " and data1.DeptID=" + ddlDepartemen.SelectedValue;
            //if (ddlSemester.SelectedValue != "0")
            //    kriteria = kriteria + " and data1.Periode='" + ddlSemester.SelectedItem + '"
            //    ; 

            string strSQL =  //" select * from ( " +
                             " select * from  ( " +
                             " select A.ID,A.TglAnalisaTrans,A.DeptID,C.Dept,A.AnalisaClassID,B.AnalisaResiko,A.Ket,A.Aktivitas,A.Risk,A.IssueInternal1,A.IssueEkternal1," +
                             " A.Peluang1,A.LvlKemungkinan,A.LvlDampak,A.LvlResiko,case when A.LvlResiko >=10 then 'High' when A.LvlResiko >=6  then 'Medium' when A.LvlResiko >=1  then 'Low' end LvlResiko1, " +
                             " A.Treatment1,case when DAY (A.DueDate1) between  1 and 7 then 'M1' when Day(A.DueDate1) between 8 and 14 Then 'M2' when  DAY(A.DueDate1) between 15 and 21  then 'M3' else 'M4' end DueDate, " +
                             " Month(A.DueDate1) Bulan,YEAR(A.DueDate1)Tahun,A.Apv, " +
                             " case when A.Apv='0' then 'Open' when A.Apv='1' then 'Manager' when A.Apv='2' then 'Corp.Plant Mgr/Corp.Mgr' else 'Corp.ISO Manager' end Approval, " +
                             " Case when A.Status=0 then 'UnSolved' else 'Solved' end [Statusx],A.RowStatus, case when MONTH(A.DueDate1) between 1 and 6 then 'Semester I' else 'Semester II' end Periode from ISO_AnalisaTrans A " +
                             " inner join ISO_AnalisaRMaster B on A.AnalisaClassID=B.ID " +
                             " inner join ISO_DeptRisk C on A.DeptID=C.ID  ) as data1 where " +
                             " data1.Periode='" + ddlSemester.SelectedItem + "' " +
                             " and data1.Tahun=" + ddlTahun.SelectedValue + " and data1.RowStatus>-1 " + kriteria;
            //" ) as data1x order by data1x.ID asc ";
            Session["Query"] = strSQL;
            if (users.UnitKerjaID == 1)
            { Nomor = "ISO/RA/41/18/R5"; Session["Nomor"] = Nomor.Trim(); }
            else if (users.UnitKerjaID == 7)
            { Nomor = "ISO/RA/41/18/R5"; Session["Nomor"] = Nomor.Trim(); }
            else if (users.UnitKerjaID == 13)
            { Nomor = "ISO/RA/41/18/R5"; Session["Nomor"] = Nomor.Trim(); }
            Cetak(this);
        }

        static public void Cetak(Control page)
        {
            string myScript = "var wn = window.showModalDialog('Report.aspx?IdReport=pemantauanRA', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private void LoadTahun()
        {
            ArrayList arrD = this.ListBATahun();
            ddlTahun.Items.Clear();
            foreach (BeritaAcara ba in arrD)
            {
                //ddlTahun.Items.Add(new ListItem(ba.Tahun.ToString(), ba.Tahun.ToString()));
                ddlTahun.Items.Add(new System.Web.UI.WebControls.ListItem(ba.Tahun.ToString(), ba.Tahun.ToString()));
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }

        private ArrayList ListBATahun()
        {
            ArrayList arrData = new ArrayList();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "SELECT DISTINCT YEAR(BADate)Tahun From BeritaAcara Order By YEAR(BADate)Desc";
            SqlDataReader sdr = zw.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new BeritaAcara
                        {
                            Tahun = Convert.ToInt32(sdr["Tahun"].ToString())
                        });
                    }
                }
            }
            else
            {
                arrData.Add(new BeritaAcara { Tahun = DateTime.Now.Year });
            }
            return arrData;
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
    }
}