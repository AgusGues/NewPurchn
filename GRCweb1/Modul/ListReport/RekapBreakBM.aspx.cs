using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;

using Domain;
using BusinessFacade;

namespace GRCweb1.Modul.ListReport
{
    public partial class RekapBreakBM : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.link = "~/Default.aspx";
            if (!Page.IsPostBack)
            {
                LoadBreakBMPlant();
                txtFromPostingPeriod.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                txtToPostingPeriod.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");

            }


        }

        private void LoadBreakBMPlant()
        {
            ArrayList arrBreakBMPlant = new ArrayList();
            BreakBMFacade breakBMFacade = new BreakBMFacade();
            arrBreakBMPlant = breakBMFacade.RetrieveBMPlant();
            ddlPlanID.Items.Add(new ListItem("-- Pilih Line --", "all"));
            foreach (BreakBMPlant breakBMPlant in arrBreakBMPlant)
            {
                ddlPlanID.Items.Add(new ListItem(breakBMPlant.PlanName, breakBMPlant.ID.ToString()));
            }
        }

        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            string periodeAwal = DateTime.Parse(txtFromPostingPeriod.Text).ToString("yyyyMMdd");
            string periodeAkhir = DateTime.Parse(txtToPostingPeriod.Text).ToString("yyyyMMdd");

            string txPeriodeAwal = txtFromPostingPeriod.Text;
            string txPeriodeAkhir = txtToPostingPeriod.Text;

            if (ddlPlanID.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Pilih Line");
                return;
            }
            if (txtFromPostingPeriod.Text == string.Empty || txtToPostingPeriod.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Periode Tanggal tidak boleh kosong");
                return;
            }

            string strError = string.Empty;
            int bln = DateTime.Parse(txtFromPostingPeriod.Text).Month;
            int planID = int.Parse(ddlPlanID.SelectedValue);
            string plan = Convert.ToString(ddlPlanID.SelectedValue);
            Users users = (Users)Session["Users"];
            string UnitKerja = users.UnitKerjaID.ToString();
            Session["UnitKerja"] = UnitKerja;
            Session["Query"] = null;
            Session["prdawal"] = null;
            Session["prdakhir"] = null;
            Session["xjudul"] = null;
            Session["GP1"] = null;
            Session["GP2"] = null;
            Session["GP3"] = null;
            Session["GP4"] = null;
            Session["formno"] = null;
            Session["TargetBM"] = null;
            Session["TargetMTN"] = null;

            ReportFacade reportFacade = new ReportFacade();
            string strQuery = reportFacade.ViewLapBreakBMPlan(periodeAwal, periodeAkhir, plan, UnitKerja);

            Session["Query"] = strQuery;
            Session["prdawal"] = txPeriodeAwal;
            Session["prdakhir"] = txPeriodeAkhir;
            Session["xjudul"] = "PRODUCTION BREAK DOWN TIME";
            Session["formno"] = "Form No :PIC/PBD/05/03/R1";


            if (UnitKerja == "1")
            {
                if (ddlPlanID.SelectedIndex == 1)
                {
                    Session["GP1"] = "CA";
                    Session["GP2"] = "CB";
                    Session["GP3"] = "CC";
                    Session["GP4"] = "CD";
                    Session["TargetBM"] = "6,3";
                    Session["TargetMTN"] = "3,0";

                }

                if (ddlPlanID.SelectedIndex == 2)
                {
                    Session["GP1"] = "CE";
                    Session["GP2"] = "CF";
                    Session["GP3"] = "CG";
                    Session["GP4"] = "CH";
                    Session["TargetBM"] = "6,3";
                    Session["TargetMTN"] = "3,0";
                }

                if (ddlPlanID.SelectedIndex == 3)
                {
                    Session["GP1"] = "CI";
                    Session["GP2"] = "CJ";
                    Session["GP3"] = "CK";
                    Session["GP4"] = "CL";
                    Session["TargetBM"] = "6,3";
                    Session["TargetMTN"] = "3,0";
                }

                if (ddlPlanID.SelectedIndex == 4)
                {
                    Session["GP1"] = "CM";
                    Session["GP2"] = "CN";
                    Session["GP3"] = "CO";
                    Session["GP4"] = "CP";
                    Session["TargetBM"] = "6,3";
                    Session["TargetMTN"] = "3,0";
                }
            }
            else if (UnitKerja == "7")
            {
                if (ddlPlanID.SelectedIndex == 1)
                {
                    Session["GP1"] = "KA";
                    Session["GP2"] = "KB";
                    Session["GP3"] = "KC";
                    Session["GP4"] = "KD";
                    Session["TargetBM"] = "6,3";
                    Session["TargetMTN"] = "3,0";

                }

                if (ddlPlanID.SelectedIndex == 2)
                {
                    Session["GP1"] = "KE";
                    Session["GP2"] = "KF";
                    Session["GP3"] = "KG";
                    Session["GP4"] = "KH";
                    Session["TargetBM"] = "6,3";
                    Session["TargetMTN"] = "3,0";
                }

                if (ddlPlanID.SelectedIndex == 3)
                {
                    Session["GP1"] = "KI";
                    Session["GP2"] = "KJ";
                    Session["GP3"] = "KK";
                    Session["GP4"] = "KL";
                    Session["TargetBM"] = "6,3";
                    Session["TargetMTN"] = "3,0";
                }

                if (ddlPlanID.SelectedIndex == 4)
                {
                    Session["GP1"] = "KM";
                    Session["GP2"] = "KN";
                    Session["GP3"] = "KO";
                    Session["GP4"] = "KP";
                    Session["TargetBM"] = "6,3";
                    Session["TargetMTN"] = "3,0";
                }
                if (ddlPlanID.SelectedIndex == 5)
                {
                    Session["GP1"] = "KQ";
                    Session["GP2"] = "KR";
                    Session["GP3"] = "KS";
                    Session["GP4"] = "KT";
                    Session["TargetBM"] = "6,3";
                    Session["TargetMTN"] = "3,0";
                }
                if (ddlPlanID.SelectedIndex == 6)
                {
                    Session["GP1"] = "KU";
                    Session["GP2"] = "KV";
                    Session["GP3"] = "KW";
                    Session["GP4"] = "KX";
                    Session["TargetBM"] = "6,3";
                    Session["TargetMTN"] = "3,0";
                }
            }
            else
            {
                if (ddlPlanID.SelectedIndex == 1)
                {
                    Session["GP1"] = "JA";
                    Session["GP2"] = "JB";
                    Session["GP3"] = "JC";
                    Session["GP4"] = "JD";
                    Session["TargetBM"] = "6,3";
                    Session["TargetMTN"] = "3,0";

                }
                if (ddlPlanID.SelectedIndex == 2)
                {
                    Session["GP1"] = "JE";
                    Session["GP2"] = "JF";
                    Session["GP3"] = "JG";
                    Session["GP4"] = "JH";
                    Session["TargetBM"] = "6,3";
                    Session["TargetMTN"] = "3,0";

                }
                if (ddlPlanID.SelectedIndex == 3)
                {
                    Session["GP1"] = "JI";
                    Session["GP2"] = "JJ";
                    Session["GP3"] = "JK";
                    Session["GP4"] = "JL";
                    Session["TargetBM"] = "6,3";
                    Session["TargetMTN"] = "3,0";

                }
                if (ddlPlanID.SelectedIndex == 4)
                {
                    Session["GP1"] = "JM";
                    Session["GP2"] = "JN";
                    Session["GP3"] = "JO";
                    Session["GP4"] = "JP";
                    Session["TargetBM"] = "6,3";
                    Session["TargetMTN"] = "3,0";

                }
            }
            Cetak(this);

            #region Boardmill
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = strQuery;
            SqlDataReader sdr = zl.Retrieve();
            #region update sarmut BM
            string sarmutPrs = "Breakdown Time";
            string strJmlLine = string.Empty;
            string strDept = string.Empty;
            int deptid = getDeptID("BOARD");
            switch (int.Parse(plan))
            {
                case 1:
                    strJmlLine = "Line 1";
                    break;
                case 2:
                    strJmlLine = "Line 2";
                    break;
                case 3:
                    strJmlLine = "Line 3";
                    break;
                case 4:
                    strJmlLine = "Line 4";
                    break;
                case 5:
                    strJmlLine = "Line 5";
                    break;
                case 6:
                    strJmlLine = "Line 6";
                    break;
            }
            #endregion
            decimal actual = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select isnull(cast (((sum(bdnpms_g1)+sum(bdnpms_g2)+sum(bdnpms_g3)+sum(bdnpms_g4))/(select sum(ttlps) from ( " +
                "select distinct tglbreak,ttlps from TempBreakDown )s))*100 as decimal(18,2)),0) actual from TempBreakDown";
            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actual = Decimal.Parse(sdr["actual"].ToString());
                }
            }

            decimal wp = 0;
            decimal tbj = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            //zl.CustomQuery = " select * from ( " +
            //                 " select isnull(cast(sum(bdnpms_M)+sum(bdnpms_E)+sum(bdnpms_U)as decimal(18,2)),0)wp,(select isnull(cast(sum(ttlps)as decimal(18,2)),0) from ( " +
            //                 " select distinct tglbreak,ttlps from TempBreakDown )s)tbj from TempBreakDown ) as Breakz ";
            zl.CustomQuery = " select * from ( " +
                               " select isnull(cast(sum(BDNPMS_G1)+sum(BDNPMS_G2)+sum(BDNPMS_G3)+sum(BDNPMS_G4)as decimal(18,2)),0)wp,(select isnull(cast(sum(ttlps)as decimal(18,2)),0) from ( " +
                              " select distinct tglbreak,ttlps from TempBreakDown )s)tbj from TempBreakDown ) as Breakz ";

            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    wp = Decimal.Parse(sdr["wp"].ToString());
                    tbj = Decimal.Parse(sdr["tbj"].ToString());
                }
            }



            ZetroView zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery =
                "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + DateTime.Parse(txtFromPostingPeriod.Text).Year.ToString() +
                " set @bulan=" + DateTime.Parse(txtFromPostingPeriod.Text).Month.ToString() + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                "update SPD_Trans set actual=" + actual.ToString().Replace(",", ".") + " where Approval=0 and tahun=@tahun and bulan=@bulan and  SarmutDeptID in ( " +
                "select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and sarmutdepartemen='" +
                strJmlLine + "' and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "'))";
            SqlDataReader sdr1 = zl1.Retrieve();

            //ZetroView zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery =
                "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + DateTime.Parse(txtFromPostingPeriod.Text).Year.ToString() +
                " set @bulan=" + DateTime.Parse(txtFromPostingPeriod.Text).Month.ToString() + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                "update SPD_Trans set wp=" + wp.ToString().Replace(",", ".") + " ,tbj=" + tbj.ToString().Replace(",", ".") + " where Approval=0 and tahun=@tahun and bulan=@bulan and  SarmutDeptID in ( " +
                "select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and sarmutdepartemen='" +
                strJmlLine + "' and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "'))";
            SqlDataReader sdr1x = zl1.Retrieve();


            //hitung average untuk spd_perusahaan
            actual = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                " declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + DateTime.Parse(txtFromPostingPeriod.Text).Year.ToString() +
                " set @bulan=" + DateTime.Parse(txtFromPostingPeriod.Text).Month.ToString() + " " +
                " set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                " select isnull(CAST((sum(wp)/sum(tbj)*100)as decimal(18,2)),0)actual from ( " +
                " select sum(wp)wp,sum(tbj)tbj from ( " +
                " select * from  SPD_Trans  where Approval=0 and tahun=@tahun and bulan=@bulan and  SarmutDeptID in " +
                " ( select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and SarmutPID " +
                " in ( select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "')) " +
                " ) as d1 ) as d2 ";
            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actual = Decimal.Parse(sdr["actual"].ToString());
                }
            }
            zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;

            zl1.CustomQuery =
                "update SPD_TransPrs set actual= " + actual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + DateTime.Parse(txtFromPostingPeriod.Text).Year.ToString() +
                 " and Bulan=" + DateTime.Parse(txtFromPostingPeriod.Text).Month.ToString() +
                 " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "') ";
            sdr1 = zl1.Retrieve();
            #endregion

            #region Maintenance
            #region update sarmut Maintenance
            sarmutPrs = "Breakdown Time Maintenance ";
            strJmlLine = string.Empty;
            strDept = "MAINTENANCE";
            decimal actualm = 0;
            deptid = getDeptID("MAINTENANCE");
            if (strDept == "MAINTENANCE")
            {
                switch (int.Parse(plan))
                {
                    case 1:
                        strJmlLine = "Breakdown Time Line 1";
                        break;
                    case 2:
                        strJmlLine = "Breakdown Time Line 2";
                        break;
                    case 3:
                        strJmlLine = "Breakdown Time Line 3";
                        break;
                    case 4:
                        strJmlLine = "Breakdown Time Line 4";
                        break;
                    case 5:
                        strJmlLine = "Breakdown Time Line 5";
                        break;
                    case 6:
                        strJmlLine = "Breakdown Time Line 6";
                        break;
                }
            }
            #endregion
            actualm = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select isnull(cast ((((sum(bdnpms_M)+sum(bdnpms_E)+sum(bdnpms_U)))/(select sum(ttlps) from ( " +
                "select distinct tglbreak,ttlps from TempBreakDown )s))*100 as decimal(18,2)),0) actual from TempBreakDown";
            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actualm = Decimal.Parse(sdr["actual"].ToString());
                }
            }

            decimal wpm = 0;
            decimal tbjm = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = " select * from ( " +
                             " select isnull(cast(sum(bdnpms_M)+sum(bdnpms_E)+sum(bdnpms_U)as decimal(18,2)),0)wp,(select isnull(cast(sum(ttlps)as decimal(18,2)),0) from ( " +
                             " select distinct tglbreak,ttlps from TempBreakDown )s)tbj from TempBreakDown ) as Breakz ";

            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    wpm = Decimal.Parse(sdr["wp"].ToString());
                    tbjm = Decimal.Parse(sdr["tbj"].ToString());
                }
            }

            zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery =
                "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + DateTime.Parse(txtFromPostingPeriod.Text).Year.ToString() +
                " set @bulan=" + DateTime.Parse(txtFromPostingPeriod.Text).Month.ToString() + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                "update SPD_Trans set actual=" + actualm.ToString().Replace(",", ".") + " where Approval=0 and tahun=@tahun and bulan=@bulan and  SarmutDeptID in ( " +
                "select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and sarmutdepartemen='" +
                strJmlLine + "' and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "'))";
            sdr1 = zl1.Retrieve();

            //ZetroView zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery =
                "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + DateTime.Parse(txtFromPostingPeriod.Text).Year.ToString() +
                " set @bulan=" + DateTime.Parse(txtFromPostingPeriod.Text).Month.ToString() + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                "update SPD_Trans set wp=" + wpm.ToString().Replace(",", ".") + " ,tbj=" + tbjm.ToString().Replace(",", ".") + " where Approval=0 and tahun=@tahun and bulan=@bulan and  SarmutDeptID in ( " +
                "select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and sarmutdepartemen='" +
                strJmlLine + "' and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "'))";
            SqlDataReader sdr1xz = zl1.Retrieve();


            //hitung average untuk spd_perusahaan
            actualm = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                " declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + DateTime.Parse(txtFromPostingPeriod.Text).Year.ToString() +
                " set @bulan=" + DateTime.Parse(txtFromPostingPeriod.Text).Month.ToString() + " " +
                " set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                " select isnull(CAST((sum(wp)/sum(tbj)*100)as decimal(18,2)),0)actual from ( " +
                " select sum(wp)wp,sum(tbj)tbj from ( " +
                " select * from  SPD_Trans  where Approval=0 and tahun=@tahun and bulan=@bulan and tbj!=1440 and wp!=1440 and SarmutDeptID in " +
                " ( select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and SarmutPID " +
                " in ( select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "')) " +
                " ) as d1 ) as d2 ";

            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actualm = Decimal.Parse(sdr["actual"].ToString());
                }
            }
            zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery =
                "update SPD_TransPrs set actual= " + actualm.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + DateTime.Parse(txtFromPostingPeriod.Text).Year.ToString() +
                 " and Bulan=" + DateTime.Parse(txtFromPostingPeriod.Text).Month.ToString() +
                 " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "') ";

            sdr1 = zl1.Retrieve();
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempBreakDown]') AND type in (N'U')) DROP TABLE [dbo].[TempBreakDown] ";
            sdr = zl.Retrieve();
            #endregion

        }
        protected int getDeptID(string deptName)
        {
            int result = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select id from spd_dept where dept like '%" + deptName + "%'";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Int32.Parse(sdr["ID"].ToString());
                }
            }
            return result;
        }
        static public void Cetak(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../Report/Report.aspx?IdReport=LapBreakBM', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
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

    }
}