using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using BusinessFacade;
using Domain;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Globalization;

namespace GRCweb1.Modul.ISO
{
    public partial class InputSarmut : System.Web.UI.Page
    {
        public int ApprovalLevel { get; set; }
        public string Judul = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            //scriptManager.RegisterPostBackControl(this.chkAll);
            string path = Request.QueryString["path"];
            if (path != null)
            {
                System.Drawing.Bitmap img = new System.Drawing.Bitmap(path);
                img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
            if (!Page.IsPostBack)
            {

                Session["Ada"] = 0;
                string token = (Request.QueryString["token"] != null) ?
                    new EncryptPasswordFacade().DecryptString(Request.QueryString["token"].ToString()) : string.Empty;
                if (token != string.Empty)
                {
                    NameValueCollection url = HttpUtility.ParseQueryString(token);
                    UsersFacade usr = new UsersFacade();
                    Users users = usr.RetrieveByUserNameAndPassword(url[1].ToString(), url[2].ToString());
                    Session["Users"] = users;
                }
                Global.link = "~/Default.aspx";
                Users user = ((Users)Session["Users"]);
                //string[] AppUser = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Approval", "BeritaAcara").Split(',');
                int Urutan = 0;
                Urutan = user.Apv;
                LoadBulan();
                LoadTahun();
                LoadDept();
                FacadeBA f = new FacadeBA();
                BeritaAcara01 b = new BeritaAcara01();
                int Ada = f.LoadA(user.DeptID.ToString());
                Session["Ada"] = Ada;
                if (Ada > 1)
                { Urutan = 0; }


                switch (Urutan)
                {
                    case 3:
                        //case 4:
                        this.ApprovalLevel = 3;
                        break;
                    default:
                        this.ApprovalLevel = Urutan;
                        break;
                }
                if (ApprovalLevel == 0)
                {

                    this.StateView = 0;
                    appLevele.Value = ApprovalLevel.ToString();
                    btnApprove.Visible = false;
                    btnSimpan.Visible = true;
                    btnUnApprove.Visible = false;
                    btnUnApprove.Enabled = false;
                    btnExport.Visible = true;
                    //btnPreview.Visible = false;
                    chkAll.Visible = false;
                    criteria.Visible = true;
                    Judul = "INPUT";
                    ddlBulan.Visible = true;
                    ddlTahun.Visible = true;
                    lst.Attributes.Add("style", "height:500px");
                    PanelApv.Visible = false;
                    LoadTypeSarmut();
                }
                if (ApprovalLevel > 0)
                {
                    this.StateView = 0;
                    PanelApv.Visible = true;
                    appLevele.Value = ApprovalLevel.ToString();
                    btnApprove.Visible = true;
                    btnSimpan.Visible = false;
                    btnUnApprove.Visible = true;
                    btnUnApprove.Enabled = true;
                    btnExport.Visible = false;
                    //btnPreview.Visible = false;
                    chkAll.Visible = true;
                    criteria.Visible = true;
                    Judul = "APPROVAL";
                    ddlBulan.Visible = true;
                    ddlTahun.Visible = true;
                    lst.Attributes.Add("style", "height:500px");
                    LoadTypeSarmut();
                }


                runSarmut();

            }
        }

        private void runSarmut()
        {
            Users user = ((Users)Session["Users"]);

            if (user.DeptID == 15) //Purchasing
            {
                //Update sarmut Agus
                Update_sarmut_Purchn();
                //Update sarmut Agus
            }
            else if (user.DeptID == 2) //Boardmill
            {
                //Update sarmut Agus Scrab Basah
                Update_sarmut_Scrab_Basah();
                //Update sarmut Agus Scrab Basah

                // add hasan pelarian produk nov 2021
                Update_sarmut_Pelarian();
                //

                //agus-Bending-strength-2022-03-16
                Update_sarmut_BendingStrength();
                //agus-Bending-strength2022-03-16

                //add hasan 2022-08
                Update_sarmut_DefectBM();
                //end add hasan 2022-08


                //Update sarmut Razib BM_Floculant
                #region add by Razib
                #region #1  Press <15 mm 
                //if (user.UnitKerjaID == 1 || user.UnitKerjaID == 13)
                //{
                //    Update_sarmut_k15_mm_PRESS_L1();
                //    Update_sarmut_k15_mm_PRESS_L2();
                //    Update_sarmut_k15_mm_PRESS_L3();
                //    Update_sarmut_k15_mm_PRESS_L4();
                //}
                //else if (user.UnitKerjaID == 7)
                //{
                //    Update_sarmut_k15_mm_PRESS_L1();
                //    Update_sarmut_k15_mm_PRESS_L2();
                //    Update_sarmut_k15_mm_PRESS_L3();
                //    Update_sarmut_k15_mm_PRESS_L4();
                //    Update_sarmut_k15_mm_PRESS_L5();
                //    Update_sarmut_k15_mm_PRESS_L6();
                //}
                //#endregion
                //#region #2  Press >=15 mm 
                //if (user.UnitKerjaID == 1 || user.UnitKerjaID == 13)
                //{
                //    Update_sarmut_Lsd15_mm_PRESS_L1();
                //    Update_sarmut_Lsd15_mm_PRESS_L2();
                //    Update_sarmut_Lsd15_mm_PRESS_L3();
                //    Update_sarmut_Lsd15_mm_PRESS_L4();
                //}
                //else if (user.UnitKerjaID == 7)
                //{
                //    Update_sarmut_Lsd15_mm_PRESS_L1();
                //    Update_sarmut_Lsd15_mm_PRESS_L2();
                //    Update_sarmut_Lsd15_mm_PRESS_L3();
                //    Update_sarmut_Lsd15_mm_PRESS_L4();
                //    Update_sarmut_Lsd15_mm_PRESS_L5();
                //    Update_sarmut_Lsd15_mm_PRESS_L6();
                //}
                //#endregion
                //#region #3 Non Press 
                //if (user.UnitKerjaID == 1 || user.UnitKerjaID == 13)
                //{
                //    Update_sarmut_Non_PressL1();
                //    Update_sarmut_Non_PressL2();
                //    Update_sarmut_Non_PressL3();
                //    Update_sarmut_Non_PressL4();
                //}
                //else if (user.UnitKerjaID == 7)
                //{
                //    Update_sarmut_Non_PressL1();
                //    Update_sarmut_Non_PressL2();
                //    Update_sarmut_Non_PressL3();
                //    Update_sarmut_Non_PressL4();
                //    Update_sarmut_Non_PressL5();
                //    Update_sarmut_Non_PressL6();
                //}
                //#endregion
                //#region #4  Akumulasi 
                //if (user.UnitKerjaID == 1 || user.UnitKerjaID == 13)
                //{
                //    Update_sarmut_AkumulasiL1();
                //    Update_sarmut_AkumulasiL2();
                //    Update_sarmut_AkumulasiL3();
                //    Update_sarmut_AkumulasiL4();
                //}
                //else if (user.UnitKerjaID == 7)
                //{
                //    Update_sarmut_AkumulasiL1();
                //    Update_sarmut_AkumulasiL2();
                //    Update_sarmut_AkumulasiL3();
                //    Update_sarmut_AkumulasiL4();
                //    Update_sarmut_AkumulasiL5();
                //    Update_sarmut_AkumulasiL6();
                //}
                #endregion
                #endregion
                //end sarmut Razib BM_Floculant
                //Update MMS FOAM by Razib
                //Update_sarmut_MMS_Foam();
                //End MMS FOAM


            }
            else if (user.DeptID == 3) // finishing
            {
                //Update sarmut Razib Scrab Kering
                Update_sarmut_Scrab_Kering();
                //Update sarmut Razib Scrab Kering
            }
            else if (user.DeptID == 10)//logistik material
            {
                Update_Sarmut_Logistik_M();
            }
            else if (user.DeptID == 13) //Marketing
            {
                /** Beny added 30 November 2021 **/
                AutoLink_Sarmut_Produktifitas();
                /** End **/

                // add hasan customer complaint non mutu des 2021
                Update_customer_non_mutu();
                // end hasan

            }
            else if (user.DeptID == 6)
            {
                //add hasan end januari
                Update_pemantauan_hasil_packing();
                //end hasan
            }
            else if (user.DeptID == 26)
            {
                //add Beny 2022-10
                Update_sarmut_OnTimeDelivery();
            }
        }


        private void Update_sarmut_MMS_Foam()
        {
            Users user = ((Users)Session["Users"]);
            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "BOARDMILL")
            {
                if (user.UnitKerjaID == 1 || user.UnitKerjaID == 13)
                {
                    SarmutOnSystem1();
                    SarmutOnSystem2();
                    SarmutOnSystem3();
                    SarmutOnSystem4();
                }
                else if (user.UnitKerjaID == 7)
                {
                    SarmutOnSystem1();
                    SarmutOnSystem2();
                    SarmutOnSystem3();
                    SarmutOnSystem4();
                    SarmutOnSystem5();
                    SarmutOnSystem6();
                }
            }
        }

        protected void SarmutOnSystem1()
        {
            #region Update Sarmut Effisiensi Pemakaian Anti foam add by Razib  Line-1
            string Queryx = string.Empty;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = Queryx;
            SqlDataReader sdr = zl.Retrieve();

            #region update sarmut BM - MS Anti FOam
            string sarmutPrs = "Effisiensi Pemakaian Anti foam";
            string strDept = string.Empty;
            int deptid = getDeptID("BOARD");

            #endregion

            #region #1
            decimal actual = 0;
            //decimal actual1 = 0;
            //decimal actual2 = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                            " select isnull(round(QtySPBL1/nullif(OutM3L1,0),2),0)actual from ( " +
                            " select cast(isnull(sum(OutM3L1),0)as decimal(10,2))OutM3L1,round(cast(isnull(sum(QtySPBL1),0)as decimal(10,2)),0) QtySPBL1 from  TempMSFoamx " +
                            " ) as xx ";

            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actual = decimal.Parse(sdr["actual"].ToString());
                }
            }


            #endregion
            #region #2
            ZetroView zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery =
                "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                " set @bulan=" + ddlBulan.SelectedIndex + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                //For Line 1
                "update SPD_Trans set actual=" + actual.ToString().Replace(",", ".") + " where Approval=0 and tahun=@tahun and bulan=@bulan and  SarmutDeptID in ( " +
                "select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and sarmutdepartemen='Line 1' and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "'))";

            SqlDataReader sdr1 = zl1.Retrieve();
            #endregion
            #region #3
            //hitung average untuk spd_perusahaan
            actual = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                " set @bulan=" + ddlBulan.SelectedIndex + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                "select isnull(cast(avg(actual) as decimal(18,2)),0) actual from SPD_Trans where Approval=0 and tahun=@tahun and bulan=@bulan and  SarmutDeptID in ( " +
                "select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "')) and actual>0";
            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actual = Decimal.Parse(sdr["actual"].ToString());
                }
            }
            #endregion
            #region #4
            zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;

            zl1.CustomQuery =
                "update SPD_TransPrs set actual= " + actual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddlTahun.SelectedValue +
                 " and Bulan=" + ddlBulan.SelectedIndex + " " +
                 " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "')";
            sdr1 = zl1.Retrieve();
            #endregion

            #endregion
        }

        protected void SarmutOnSystem2()
        {
            #region Update Sarmut Effisiensi Pemakaian Anti foam add by Razib  Line-2
            string Queryx = string.Empty;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = Queryx;
            SqlDataReader sdr = zl.Retrieve();

            #region update sarmut BM - MS Anti Foam
            string sarmutPrs = "Effisiensi Pemakaian Anti foam";
            string strDept = string.Empty;
            int deptid = getDeptID("BOARD");

            #endregion

            #region #1
            decimal actual = 0;
            //decimal actual1 = 0;
            //decimal actual2 = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                            " select isnull(round(QtySPBL2/nullif(OutM3L2,0),2),0)actual from ( " +
                            " select cast(isnull(sum(OutM3L2),0)as decimal(10,2))OutM3L2,round(cast(isnull(sum(QtySPBL2),0)as decimal(10,2)),0) QtySPBL2 from TempMSFoamx " +
                            " ) as xx ";

            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actual = decimal.Parse(sdr["actual"].ToString());
                }
            }


            #endregion
            #region #2
            ZetroView zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery =
                "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                " set @bulan=" + ddlBulan.SelectedIndex + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                //For Line 1
                "update SPD_Trans set actual=" + actual.ToString().Replace(",", ".") + " where Approval=0 and tahun=@tahun and bulan=@bulan and  SarmutDeptID in ( " +
                "select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and sarmutdepartemen='Line 2' and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "'))";

            SqlDataReader sdr1 = zl1.Retrieve();
            #endregion
            #region #3
            //hitung average untuk spd_perusahaan
            actual = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                " set @bulan=" + ddlBulan.SelectedIndex + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                "select isnull(cast(avg(actual) as decimal(18,2)),0) actual from SPD_Trans where Approval=0 and tahun=@tahun and bulan=@bulan and  SarmutDeptID in ( " +
                "select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "')) and actual>0";
            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actual = Decimal.Parse(sdr["actual"].ToString());
                }
            }
            #endregion
            #region #4
            zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;

            zl1.CustomQuery =
                "update SPD_TransPrs set actual= " + actual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddlTahun.SelectedValue +
                 " and Bulan=" + ddlBulan.SelectedIndex + " " +
                 " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "')";
            sdr1 = zl1.Retrieve();
            #endregion

            #endregion
        }

        protected void SarmutOnSystem3()
        {
            #region Update Sarmut Effisiensi Pemakaian Anti foam add by Razib  Line-3
            string Queryx = string.Empty;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = Queryx;
            SqlDataReader sdr = zl.Retrieve();

            #region update sarmut BM - MS Anti Foam
            string sarmutPrs = "Effisiensi Pemakaian Anti foam";
            string strDept = string.Empty;
            int deptid = getDeptID("BOARD");

            #endregion

            #region #1
            decimal actual = 0;
            //decimal actual1 = 0;
            //decimal actual2 = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                            " select isnull(round(QtySPBL3/nullif(OutM3L3,0),2),0)actual from ( " +
                            " select cast(isnull(sum(OutM3L3),0)as decimal(10,2))OutM3L3,round(cast(isnull(sum(QtySPBL3),0)as decimal(10,2)),0) QtySPBL3 from TempMSFoamx " +
                            " ) as xx ";


            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actual = decimal.Parse(sdr["actual"].ToString());
                }
            }


            #endregion
            #region #2
            ZetroView zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery =
                "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                " set @bulan=" + ddlBulan.SelectedIndex + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                //For Line 1
                "update SPD_Trans set actual=" + actual.ToString().Replace(",", ".") + " where Approval=0 and tahun=@tahun and bulan=@bulan and  SarmutDeptID in ( " +
                "select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and sarmutdepartemen='Line 3' and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "'))";

            SqlDataReader sdr1 = zl1.Retrieve();
            #endregion
            #region #3
            //hitung average untuk spd_perusahaan
            actual = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                " set @bulan=" + ddlBulan.SelectedIndex + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                "select isnull(cast(avg(actual) as decimal(18,2)),0) actual from SPD_Trans where Approval=0 and tahun=@tahun and bulan=@bulan and  SarmutDeptID in ( " +
                "select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "')) and actual>0";
            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actual = Decimal.Parse(sdr["actual"].ToString());
                }
            }
            #endregion
            #region #4
            zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;

            zl1.CustomQuery =
                "update SPD_TransPrs set actual= " + actual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddlTahun.SelectedValue +
                 " and Bulan=" + ddlBulan.SelectedIndex + " " +
                 " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "')";
            sdr1 = zl1.Retrieve();
            #endregion

            #endregion
        }

        protected void SarmutOnSystem4()
        {
            #region Update Sarmut Effisiensi Pemakaian Anti foam add by Razib  Line-4
            string Queryx = string.Empty;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = Queryx;
            SqlDataReader sdr = zl.Retrieve();

            #region update sarmut BM - MS Anti Foam
            string sarmutPrs = "Effisiensi Pemakaian Anti foam";
            string strDept = string.Empty;
            int deptid = getDeptID("BOARD");

            #endregion

            #region #1
            decimal actual = 0;
            //decimal actual1 = 0;
            //decimal actual2 = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                            " select isnull(round(QtySPBL4/nullif(OutM3L4,0),2),0)actual from ( " +
                            " select cast(isnull(sum(OutM3L4),0)as decimal(10,2))OutM3L4,round(cast(isnull(sum(QtySPBL4),0)as decimal(10,2)),0) QtySPBL4 " +
                            " from TempMSFoamx  " +
                            " ) as xx ";

            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actual = decimal.Parse(sdr["actual"].ToString());
                }
            }


            #endregion
            #region #2
            ZetroView zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery =
                "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                " set @bulan=" + ddlBulan.SelectedIndex + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                //For Line 1
                "update SPD_Trans set actual=" + actual.ToString().Replace(",", ".") + " where Approval=0 and tahun=@tahun and bulan=@bulan and  SarmutDeptID in ( " +
                "select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and sarmutdepartemen='Line 4' and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "'))";

            SqlDataReader sdr1 = zl1.Retrieve();
            #endregion
            #region #3
            //hitung average untuk spd_perusahaan
            actual = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                " set @bulan=" + ddlBulan.SelectedIndex + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                "select isnull(cast(avg(actual) as decimal(18,2)),0) actual from SPD_Trans where Approval=0 and tahun=@tahun and bulan=@bulan and  SarmutDeptID in ( " +
                "select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "')) and actual>0";
            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actual = Decimal.Parse(sdr["actual"].ToString());
                }
            }
            #endregion
            #region #4
            zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;

            zl1.CustomQuery =
                "update SPD_TransPrs set actual= " + actual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddlTahun.SelectedValue +
                 " and Bulan=" + ddlBulan.SelectedIndex + " " +
                 " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "')";
            sdr1 = zl1.Retrieve();
            #endregion

            #endregion
        }

        protected void SarmutOnSystem5()
        {
            #region Update Sarmut Effisiensi Pemakaian Anti foam add by Razib  Line-5
            string Queryx = string.Empty;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = Queryx;
            SqlDataReader sdr = zl.Retrieve();

            #region update sarmut BM - MS ANti FOam
            string sarmutPrs = "Effisiensi Pemakaian Anti foam";
            string strDept = string.Empty;
            int deptid = getDeptID("BOARD");

            #endregion

            #region #1
            decimal actual = 0;
            //decimal actual1 = 0;
            //decimal actual2 = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                            " select isnull(round(QtySPBL5/nullif(OutM3L5,0),2),0)actual from ( " +
                            " select cast(isnull(sum(OutM3L5),0)as decimal(10,2))OutM3L5,round(cast(isnull(sum(QtySPBL5),0)as decimal(10,2)),0) QtySPBL5 " +
                            " from TempMSFoamx  " +
                            " ) as xx ";

            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actual = decimal.Parse(sdr["actual"].ToString());
                }
            }


            #endregion
            #region #2
            ZetroView zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery =
                "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                " set @bulan=" + ddlBulan.SelectedIndex + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                //For Line 1
                "update SPD_Trans set actual=" + actual.ToString().Replace(",", ".") + " where Approval=0 and tahun=@tahun and bulan=@bulan and  SarmutDeptID in ( " +
                "select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and sarmutdepartemen='Line 5' and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "'))";

            SqlDataReader sdr1 = zl1.Retrieve();
            #endregion
            #region #3
            //hitung average untuk spd_perusahaan
            actual = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                " set @bulan=" + ddlBulan.SelectedIndex + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                "select isnull(cast(avg(actual) as decimal(18,2)),0) actual from SPD_Trans where Approval=0 and tahun=@tahun and bulan=@bulan and  SarmutDeptID in ( " +
                "select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "')) and actual>0";
            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actual = Decimal.Parse(sdr["actual"].ToString());
                }
            }
            #endregion
            #region #4
            zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;

            zl1.CustomQuery =
                "update SPD_TransPrs set actual= " + actual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddlTahun.SelectedValue +
                 " and Bulan=" + ddlBulan.SelectedIndex + " " +
                 " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "')";
            sdr1 = zl1.Retrieve();
            #endregion

            #endregion
        }

        protected void SarmutOnSystem6()
        {
            #region Update Sarmut Effisiensi Pemakaian Anti foam add by Razib  Line-6
            string Queryx = string.Empty;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = Queryx;
            SqlDataReader sdr = zl.Retrieve();

            #region update sarmut BM - MS Anti Foam
            string sarmutPrs = "Effisiensi Pemakaian Anti foam";
            string strDept = string.Empty;
            int deptid = getDeptID("BOARD");

            #endregion

            #region #1
            decimal actual = 0;
            //decimal actual1 = 0;
            //decimal actual2 = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                            " select isnull(round(QtySPBL6/nullif(OutM3L6,0),2),0)actual from ( " +
                            " select cast(isnull(sum(OutM3L6),0)as decimal(10,2))OutM3L6,round(cast(isnull(sum(QtySPBL6),0)as decimal(10,2)),0) QtySPBL6 " +
                            " from TempMSFoamx  " +
                            " ) as xx ";

            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actual = decimal.Parse(sdr["actual"].ToString());
                }
            }


            #endregion
            #region #2
            ZetroView zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery =
                "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                " set @bulan=" + ddlBulan.SelectedIndex + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                //For Line 1
                "update SPD_Trans set actual=" + actual.ToString().Replace(",", ".") + " where Approval=0 and tahun=@tahun and bulan=@bulan and  SarmutDeptID in ( " +
                "select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and sarmutdepartemen='Line 6' and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "'))";

            SqlDataReader sdr1 = zl1.Retrieve();
            #endregion
            #region #3
            //hitung average untuk spd_perusahaan
            actual = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                " set @bulan=" + ddlBulan.SelectedIndex + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                "select isnull(cast(avg(actual) as decimal(18,2)),0) actual from SPD_Trans where Approval=0 and tahun=@tahun and bulan=@bulan and  SarmutDeptID in ( " +
                "select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "')) and actual>0";
            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actual = Decimal.Parse(sdr["actual"].ToString());
                }
            }
            #endregion
            #region #4
            zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;

            zl1.CustomQuery =
                "update SPD_TransPrs set actual= " + actual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddlTahun.SelectedValue +
                 " and Bulan=" + ddlBulan.SelectedIndex + " " +
                 " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "')";
            sdr1 = zl1.Retrieve();
            #endregion

            #endregion
        }
        private void Update_sarmut_Non_PressL1()
        {
            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "BOARDMILL")
            {
                decimal aktual = 0;

                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "select ROUND(cast((Tot_EfL1)as decimal(10,2)),2) Aktual from TempBawah2 where Ket='Sub Total Nonpress'";

                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["aktual"].ToString());

                    }
                }
                if (aktual <= 0)
                    aktual = 0;
                string sarmutPrs = "Effisiensi Pemakaian Flooculant";
                int deptid = getDeptID("BOARDMILL");
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery = " declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                                  " set @bulan=" + ddlBulan.SelectedValue + " " +
                                  " set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                                  " Update  SPD_TransDet set actual = " + aktual.ToString().Replace(",", ".") + " where Approval = 0 and tahun = @tahun and bulan = @bulan and SarmutDeptID in " +
                                  " (select ID from SPD_DepartemenDet where @thnbln >= drperiode and @thnbln<= sdperiode and rowstatus> -1 and SubDepartemen = 'Non Press' and TargetVID>0 and SarmutDepID in " +
                                  " (select ID from SPD_Departemen where rowstatus > -1 and SarmutDepartemen = 'Line 1' " +
                                  " and SarmutPID in (select ID from SPD_Perusahaan where deptid = 1 and rowstatus> -1 and SarMutPerusahaan = '" + sarmutPrs + "'))) ";
                SqlDataReader sdr1 = zl1.Retrieve();
            }
        }
        private void Update_sarmut_Non_PressL2()
        {
            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "BOARDMILL")
            {
                decimal aktual = 0;

                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "select ROUND(cast((Tot_EfL2)as decimal(10,2)),2) Aktual from TempBawah2 where Ket='Sub Total Nonpress'";

                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["aktual"].ToString());

                    }
                }
                if (aktual <= 0)
                    aktual = 0;
                string sarmutPrs = "Effisiensi Pemakaian Flooculant";
                int deptid = getDeptID("BOARDMILL");
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery = " declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                                  " set @bulan=" + ddlBulan.SelectedValue + " " +
                                  " set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                                  " Update  SPD_TransDet set actual = " + aktual.ToString().Replace(",", ".") + " where Approval = 0 and tahun = @tahun and bulan = @bulan and SarmutDeptID in " +
                                  " (select ID from SPD_DepartemenDet where @thnbln >= drperiode and @thnbln<= sdperiode and rowstatus> -1 and SubDepartemen = 'Non Press' and TargetVID>0 and SarmutDepID in " +
                                  " (select ID from SPD_Departemen where rowstatus > -1 and SarmutDepartemen = 'Line 2' " +
                                  " and SarmutPID in (select ID from SPD_Perusahaan where deptid = 1 and rowstatus> -1 and SarMutPerusahaan = '" + sarmutPrs + "'))) ";
                SqlDataReader sdr1 = zl1.Retrieve();
            }
        }
        private void Update_sarmut_Non_PressL3()
        {
            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "BOARDMILL")
            {
                decimal aktual = 0;

                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "select ROUND(cast((Tot_EfL3)as decimal(10,2)),2) Aktual from TempBawah2 where Ket='Sub Total Nonpress'";

                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["aktual"].ToString());

                    }
                }
                if (aktual <= 0)
                    aktual = 0;
                string sarmutPrs = "Effisiensi Pemakaian Flooculant";
                int deptid = getDeptID("BOARDMILL");
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery = " declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                                  " set @bulan=" + ddlBulan.SelectedValue + " " +
                                  " set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                                  " Update  SPD_TransDet set actual = " + aktual.ToString().Replace(",", ".") + " where Approval = 0 and tahun = @tahun and bulan = @bulan and SarmutDeptID in " +
                                  " (select ID from SPD_DepartemenDet where @thnbln >= drperiode and @thnbln<= sdperiode and rowstatus> -1 and SubDepartemen = 'Non Press' and TargetVID>0 and SarmutDepID in " +
                                  " (select ID from SPD_Departemen where rowstatus > -1 and SarmutDepartemen = 'Line 3' " +
                                  " and SarmutPID in (select ID from SPD_Perusahaan where deptid = 1 and rowstatus> -1 and SarMutPerusahaan = '" + sarmutPrs + "'))) ";
                SqlDataReader sdr1 = zl1.Retrieve();
            }
        }
        private void Update_sarmut_Non_PressL4()
        {
            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "BOARDMILL")
            {
                decimal aktual = 0;

                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "select ROUND(cast((Tot_EfL4)as decimal(10,2)),2) Aktual from TempBawah2 where Ket='Sub Total Nonpress'";

                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["aktual"].ToString());

                    }
                }
                if (aktual <= 0)
                    aktual = 0;
                string sarmutPrs = "Effisiensi Pemakaian Flooculant";
                int deptid = getDeptID("BOARDMILL");
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery = " declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                                  " set @bulan=" + ddlBulan.SelectedValue + " " +
                                  " set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                                  " Update  SPD_TransDet set actual = " + aktual.ToString().Replace(",", ".") + " where Approval = 0 and tahun = @tahun and bulan = @bulan and SarmutDeptID in " +
                                  " (select ID from SPD_DepartemenDet where @thnbln >= drperiode and @thnbln<= sdperiode and rowstatus> -1 and SubDepartemen = 'Non Press' and TargetVID>0 and SarmutDepID in " +
                                  " (select ID from SPD_Departemen where rowstatus > -1 and SarmutDepartemen = 'Line 4' " +
                                  " and SarmutPID in (select ID from SPD_Perusahaan where deptid = 1 and rowstatus> -1 and SarMutPerusahaan = '" + sarmutPrs + "'))) ";
                SqlDataReader sdr1 = zl1.Retrieve();
            }
        }
        private void Update_sarmut_Non_PressL5()
        {
            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "BOARDMILL")
            {
                decimal aktual = 0;

                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "select ROUND(cast((Tot_EfL5)as decimal(10,2)),2) Aktual from TempBawah2 where Ket='Sub Total Nonpress'";

                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["aktual"].ToString());

                    }
                }
                if (aktual <= 0)
                    aktual = 0;
                string sarmutPrs = "Effisiensi Pemakaian Flooculant";
                int deptid = getDeptID("BOARDMILL");
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery = " declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                                  " set @bulan=" + ddlBulan.SelectedValue + " " +
                                  " set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                                  " Update  SPD_TransDet set actual = " + aktual.ToString().Replace(",", ".") + " where Approval = 0 and tahun = @tahun and bulan = @bulan and SarmutDeptID in " +
                                  " (select ID from SPD_DepartemenDet where @thnbln >= drperiode and @thnbln<= sdperiode and rowstatus> -1 and SubDepartemen = 'Non Press' and TargetVID>0 and SarmutDepID in " +
                                  " (select ID from SPD_Departemen where rowstatus > -1 and SarmutDepartemen = 'Line 5' " +
                                  " and SarmutPID in (select ID from SPD_Perusahaan where deptid = 1 and rowstatus> -1 and SarMutPerusahaan = '" + sarmutPrs + "'))) ";
                SqlDataReader sdr1 = zl1.Retrieve();
            }
        }
        private void Update_sarmut_Non_PressL6()
        {
            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "BOARDMILL")
            {
                decimal aktual = 0;

                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "select ROUND(cast((Tot_EfL6)as decimal(10,2)),2) Aktual from TempBawah2 where Ket='Sub Total Nonpress'";

                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["aktual"].ToString());

                    }
                }
                if (aktual <= 0)
                    aktual = 0;
                string sarmutPrs = "Effisiensi Pemakaian Flooculant";
                int deptid = getDeptID("BOARDMILL");
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery = " declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                                  " set @bulan=" + ddlBulan.SelectedValue + " " +
                                  " set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                                  " Update  SPD_TransDet set actual = " + aktual.ToString().Replace(",", ".") + " where Approval = 0 and tahun = @tahun and bulan = @bulan and SarmutDeptID in " +
                                  " (select ID from SPD_DepartemenDet where @thnbln >= drperiode and @thnbln<= sdperiode and rowstatus> -1 and SubDepartemen = 'Non Press' and TargetVID>0 and SarmutDepID in " +
                                  " (select ID from SPD_Departemen where rowstatus > -1 and SarmutDepartemen = 'Line 6' " +
                                  " and SarmutPID in (select ID from SPD_Perusahaan where deptid = 1 and rowstatus> -1 and SarMutPerusahaan = '" + sarmutPrs + "'))) ";
                SqlDataReader sdr1 = zl1.Retrieve();
            }
        }
        private void Update_sarmut_AkumulasiL1()
        {
            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "BOARDMILL")
            {
                decimal aktual = 0;

                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "select ROUND(cast((Tot_EfL1)as decimal(10,2)),2) Aktual from TempBawah2 where Ket='Grand Total'";

                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["aktual"].ToString());

                    }
                }
                if (aktual <= 0)
                    aktual = 0;
                string sarmutPrs = "Effisiensi Pemakaian Flooculant";
                int deptid = getDeptID("BOARDMILL");
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery = " declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                                  " set @bulan=" + ddlBulan.SelectedValue + " " +
                                  " set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                                  " Update  SPD_TransDet set actual = " + aktual.ToString().Replace(",", ".") + " where Approval = 0 and tahun = @tahun and bulan = @bulan and SarmutDeptID in " +
                                  " (select ID from SPD_DepartemenDet where @thnbln >= drperiode and @thnbln<= sdperiode and rowstatus> -1 and SubDepartemen = 'Akumulasi' and SarmutDepID in " +
                                  " (select ID from SPD_Departemen where rowstatus > -1 and SarmutDepartemen = 'Line 1' " +
                                  " and SarmutPID in (select ID from SPD_Perusahaan where deptid = 1 and rowstatus> -1 and SarMutPerusahaan = '" + sarmutPrs + "'))) ";
                SqlDataReader sdr1 = zl1.Retrieve();
            }
        }
        private void Update_sarmut_AkumulasiL2()
        {
            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "BOARDMILL")
            {
                decimal aktual = 0;

                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "select ROUND(cast((Tot_EfL2)as decimal(10,2)),2) Aktual from TempBawah2 where Ket='Grand Total'";

                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["aktual"].ToString());

                    }
                }
                if (aktual <= 0)
                    aktual = 0;
                string sarmutPrs = "Effisiensi Pemakaian Flooculant";
                int deptid = getDeptID("BOARDMILL");
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery = " declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                                  " set @bulan=" + ddlBulan.SelectedValue + " " +
                                  " set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                                  " Update  SPD_TransDet set actual = " + aktual.ToString().Replace(",", ".") + " where Approval = 0 and tahun = @tahun and bulan = @bulan and SarmutDeptID in " +
                                  " (select ID from SPD_DepartemenDet where @thnbln >= drperiode and @thnbln<= sdperiode and rowstatus> -1 and SubDepartemen = 'Akumulasi' and SarmutDepID in " +
                                  " (select ID from SPD_Departemen where rowstatus > -1 and SarmutDepartemen = 'Line 2' " +
                                  " and SarmutPID in (select ID from SPD_Perusahaan where deptid = 1 and rowstatus> -1 and SarMutPerusahaan = '" + sarmutPrs + "'))) ";
                SqlDataReader sdr1 = zl1.Retrieve();
            }
        }
        private void Update_sarmut_AkumulasiL3()
        {
            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "BOARDMILL")
            {
                decimal aktual = 0;

                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "select ROUND(cast((Tot_EfL3)as decimal(10,2)),2) Aktual from TempBawah2 where Ket='Grand Total'";

                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["aktual"].ToString());

                    }
                }
                if (aktual <= 0)
                    aktual = 0;
                string sarmutPrs = "Effisiensi Pemakaian Flooculant";
                int deptid = getDeptID("BOARDMILL");
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery = " declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                                  " set @bulan=" + ddlBulan.SelectedValue + " " +
                                  " set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                                  " Update  SPD_TransDet set actual = " + aktual.ToString().Replace(",", ".") + " where Approval = 0 and tahun = @tahun and bulan = @bulan and SarmutDeptID in " +
                                  " (select ID from SPD_DepartemenDet where @thnbln >= drperiode and @thnbln<= sdperiode and rowstatus> -1 and SubDepartemen = 'Akumulasi' and SarmutDepID in " +
                                  " (select ID from SPD_Departemen where rowstatus > -1 and SarmutDepartemen = 'Line 3' " +
                                  " and SarmutPID in (select ID from SPD_Perusahaan where deptid = 1 and rowstatus> -1 and SarMutPerusahaan = '" + sarmutPrs + "'))) ";
                SqlDataReader sdr1 = zl1.Retrieve();
            }
        }
        private void Update_sarmut_AkumulasiL4()
        {
            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "BOARDMILL")
            {
                decimal aktual = 0;

                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "select ROUND(cast((Tot_EfL4)as decimal(10,2)),2) Aktual from TempBawah2 where Ket='Grand Total'";

                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["aktual"].ToString());

                    }
                }
                if (aktual <= 0)
                    aktual = 0;
                string sarmutPrs = "Effisiensi Pemakaian Flooculant";
                int deptid = getDeptID("BOARDMILL");
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery = " declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                                  " set @bulan=" + ddlBulan.SelectedValue + " " +
                                  " set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                                  " Update  SPD_TransDet set actual = " + aktual.ToString().Replace(",", ".") + " where Approval = 0 and tahun = @tahun and bulan = @bulan and SarmutDeptID in " +
                                  " (select ID from SPD_DepartemenDet where @thnbln >= drperiode and @thnbln<= sdperiode and rowstatus> -1 and SubDepartemen = 'Akumulasi' and SarmutDepID in " +
                                  " (select ID from SPD_Departemen where rowstatus > -1 and SarmutDepartemen = 'Line 4' " +
                                  " and SarmutPID in (select ID from SPD_Perusahaan where deptid = 1 and rowstatus> -1 and SarMutPerusahaan = '" + sarmutPrs + "'))) ";
                SqlDataReader sdr1 = zl1.Retrieve();
            }
        }
        private void Update_sarmut_AkumulasiL5()
        {
            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "BOARDMILL")
            {
                decimal aktual = 0;

                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "select ROUND(cast((Tot_EfL5)as decimal(10,2)),2) Aktual from TempBawah2 where Ket='Grand Total'";

                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["aktual"].ToString());

                    }
                }
                if (aktual <= 0)
                    aktual = 0;
                string sarmutPrs = "Effisiensi Pemakaian Flooculant";
                int deptid = getDeptID("BOARDMILL");
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery = " declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                                  " set @bulan=" + ddlBulan.SelectedValue + " " +
                                  " set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                                  " Update  SPD_TransDet set actual = " + aktual.ToString().Replace(",", ".") + " where Approval = 0 and tahun = @tahun and bulan = @bulan and SarmutDeptID in " +
                                  " (select ID from SPD_DepartemenDet where @thnbln >= drperiode and @thnbln<= sdperiode and rowstatus> -1 and SubDepartemen = 'Akumulasi' and SarmutDepID in " +
                                  " (select ID from SPD_Departemen where rowstatus > -1 and SarmutDepartemen = 'Line 5' " +
                                  " and SarmutPID in (select ID from SPD_Perusahaan where deptid = 1 and rowstatus> -1 and SarMutPerusahaan = '" + sarmutPrs + "'))) ";
                SqlDataReader sdr1 = zl1.Retrieve();
            }
        }
        private void Update_sarmut_AkumulasiL6()
        {
            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "BOARDMILL")
            {
                decimal aktual = 0;

                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "select ROUND(cast((Tot_EfL6)as decimal(10,2)),2) Aktual from TempBawah2 where Ket='Grand Total'";

                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["aktual"].ToString());

                    }
                }
                if (aktual <= 0)
                    aktual = 0;
                string sarmutPrs = "Effisiensi Pemakaian Flooculant";
                int deptid = getDeptID("BOARDMILL");
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery = " declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                                  " set @bulan=" + ddlBulan.SelectedValue + " " +
                                  " set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                                  " Update  SPD_TransDet set actual = " + aktual.ToString().Replace(",", ".") + " where Approval = 0 and tahun = @tahun and bulan = @bulan and SarmutDeptID in " +
                                  " (select ID from SPD_DepartemenDet where @thnbln >= drperiode and @thnbln<= sdperiode and rowstatus> -1 and SubDepartemen = 'Akumulasi' and SarmutDepID in " +
                                  " (select ID from SPD_Departemen where rowstatus > -1 and SarmutDepartemen = 'Line 6' " +
                                  " and SarmutPID in (select ID from SPD_Perusahaan where deptid = 1 and rowstatus> -1 and SarMutPerusahaan = '" + sarmutPrs + "'))) ";
                SqlDataReader sdr1 = zl1.Retrieve();
            }
        }
        private void Update_sarmut_k15_mm_PRESS_L1()
        {
            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "BOARDMILL")
            {
                decimal aktual = 0;

                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "select ROUND(cast((Tot_EfL1)as decimal(10,2)),2) aktual from TempBawah2 where Ket='Sub Total < 15 mm PRESS'";

                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["aktual"].ToString());

                    }
                }
                if (aktual <= 0)
                    aktual = 0;
                string sarmutPrs = "Effisiensi Pemakaian Flooculant";
                int deptid = getDeptID("BOARDMILL");
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery = " declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                                  " set @bulan=" + ddlBulan.SelectedValue + " " +
                                  " set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                                  " Update  SPD_TransDet set actual = " + aktual.ToString().Replace(",", ".") + " where Approval = 0 and tahun = @tahun and bulan = @bulan and SarmutDeptID in " +
                                  " (select ID from SPD_DepartemenDet where @thnbln >= drperiode and @thnbln<= sdperiode and rowstatus> -1 and SubDepartemen = 'Press <15 mm' and SarmutDepID in " +
                                  " (select ID from SPD_Departemen where rowstatus > -1 and SarmutDepartemen = 'Line 1' " +
                                  " and SarmutPID in (select ID from SPD_Perusahaan where deptid = 1 and rowstatus> -1 and SarMutPerusahaan = '" + sarmutPrs + "'))) ";
                SqlDataReader sdr1 = zl1.Retrieve();
            }
        }
        private void Update_sarmut_k15_mm_PRESS_L2()
        {
            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "BOARDMILL")
            {
                decimal aktual = 0;

                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "select ROUND(cast((Tot_EfL2)as decimal(10,2)),2) aktual from TempBawah2 where Ket='Sub Total < 15 mm PRESS'";

                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["aktual"].ToString());

                    }
                }
                if (aktual <= 0)
                    aktual = 0;
                string sarmutPrs = "Effisiensi Pemakaian Flooculant";
                int deptid = getDeptID("BOARDMILL");
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery = " declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                                  " set @bulan=" + ddlBulan.SelectedValue + " " +
                                  " set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                                  " Update  SPD_TransDet set actual = " + aktual.ToString().Replace(",", ".") + " where Approval = 0 and tahun = @tahun and bulan = @bulan and SarmutDeptID in " +
                                  " (select ID from SPD_DepartemenDet where @thnbln >= drperiode and @thnbln<= sdperiode and rowstatus> -1 and SubDepartemen = 'Press <15 mm' and SarmutDepID in " +
                                  " (select ID from SPD_Departemen where rowstatus > -1 and SarmutDepartemen = 'Line 2' " +
                                  " and SarmutPID in (select ID from SPD_Perusahaan where deptid = 1 and rowstatus> -1 and SarMutPerusahaan = '" + sarmutPrs + "'))) ";
                SqlDataReader sdr1 = zl1.Retrieve();
            }
        }
        private void Update_sarmut_k15_mm_PRESS_L3()
        {
            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "BOARDMILL")
            {
                decimal aktual = 0;

                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "select ROUND(cast((Tot_EfL3)as decimal(10,2)),2) aktual from TempBawah2 where Ket='Sub Total < 15 mm PRESS'";

                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["aktual"].ToString());

                    }
                }
                if (aktual <= 0)
                    aktual = 0;
                string sarmutPrs = "Effisiensi Pemakaian Flooculant";
                int deptid = getDeptID("BOARDMILL");
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery = " declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                                  " set @bulan=" + ddlBulan.SelectedValue + " " +
                                  " set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                                  " Update  SPD_TransDet set actual = " + aktual.ToString().Replace(",", ".") + " where Approval = 0 and tahun = @tahun and bulan = @bulan and SarmutDeptID in " +
                                  " (select ID from SPD_DepartemenDet where @thnbln >= drperiode and @thnbln<= sdperiode and rowstatus> -1 and SubDepartemen = 'Press <15 mm' and SarmutDepID in " +
                                  " (select ID from SPD_Departemen where rowstatus > -1 and SarmutDepartemen = 'Line 3' " +
                                  " and SarmutPID in (select ID from SPD_Perusahaan where deptid = 1 and rowstatus> -1 and SarMutPerusahaan = '" + sarmutPrs + "'))) ";
                SqlDataReader sdr1 = zl1.Retrieve();
            }
        }
        private void Update_sarmut_k15_mm_PRESS_L4()
        {
            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "BOARDMILL")
            {
                decimal aktual = 0;

                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "select ROUND(cast((Tot_EfL4)as decimal(10,2)),2) aktual from TempBawah2 where Ket='Sub Total < 15 mm PRESS'";

                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["aktual"].ToString());

                    }
                }
                if (aktual <= 0)
                    aktual = 0;
                string sarmutPrs = "Effisiensi Pemakaian Flooculant";
                int deptid = getDeptID("BOARDMILL");
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery = " declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                                  " set @bulan=" + ddlBulan.SelectedValue + " " +
                                  " set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                                  " Update  SPD_TransDet set actual = " + aktual.ToString().Replace(",", ".") + " where Approval = 0 and tahun = @tahun and bulan = @bulan and SarmutDeptID in " +
                                  " (select ID from SPD_DepartemenDet where @thnbln >= drperiode and @thnbln<= sdperiode and rowstatus> -1 and SubDepartemen = 'Press <15 mm' and SarmutDepID in " +
                                  " (select ID from SPD_Departemen where rowstatus > -1 and SarmutDepartemen = 'Line 4' " +
                                  " and SarmutPID in (select ID from SPD_Perusahaan where deptid = 1 and rowstatus> -1 and SarMutPerusahaan = '" + sarmutPrs + "'))) ";
                SqlDataReader sdr1 = zl1.Retrieve();
            }
        }
        private void Update_sarmut_k15_mm_PRESS_L5()
        {
            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "BOARDMILL")
            {
                decimal aktual = 0;

                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "select ROUND(cast((Tot_EfL5)as decimal(10,2)),2) aktual from TempBawah2 where Ket='Sub Total < 15 mm PRESS'";

                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["aktual"].ToString());

                    }
                }
                if (aktual <= 0)
                    aktual = 0;
                string sarmutPrs = "Effisiensi Pemakaian Flooculant";
                int deptid = getDeptID("BOARDMILL");
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery = " declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                                  " set @bulan=" + ddlBulan.SelectedValue + " " +
                                  " set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                                  " Update  SPD_TransDet set actual = " + aktual.ToString().Replace(",", ".") + " where Approval = 0 and tahun = @tahun and bulan = @bulan and SarmutDeptID in " +
                                  " (select ID from SPD_DepartemenDet where @thnbln >= drperiode and @thnbln<= sdperiode and rowstatus> -1 and SubDepartemen = 'Press <15 mm' and SarmutDepID in " +
                                  " (select ID from SPD_Departemen where rowstatus > -1 and SarmutDepartemen = 'Line 5' " +
                                  " and SarmutPID in (select ID from SPD_Perusahaan where deptid = 1 and rowstatus> -1 and SarMutPerusahaan = '" + sarmutPrs + "'))) ";
                SqlDataReader sdr1 = zl1.Retrieve();
            }
        }
        private void Update_sarmut_k15_mm_PRESS_L6()
        {
            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "BOARDMILL")
            {
                decimal aktual = 0;

                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "select ROUND(cast((Tot_EfL6)as decimal(10,2)),2) aktual from TempBawah2 where Ket='Sub Total < 15 mm PRESS'";

                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["aktual"].ToString());

                    }
                }
                if (aktual <= 0)
                    aktual = 0;
                string sarmutPrs = "Effisiensi Pemakaian Flooculant";
                int deptid = getDeptID("BOARDMILL");
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery = " declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                                  " set @bulan=" + ddlBulan.SelectedValue + " " +
                                  " set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                                  " Update  SPD_TransDet set actual = " + aktual.ToString().Replace(",", ".") + " where Approval = 0 and tahun = @tahun and bulan = @bulan and SarmutDeptID in " +
                                  " (select ID from SPD_DepartemenDet where @thnbln >= drperiode and @thnbln<= sdperiode and rowstatus> -1 and SubDepartemen = 'Press <15 mm' and SarmutDepID in " +
                                  " (select ID from SPD_Departemen where rowstatus > -1 and SarmutDepartemen = 'Line 6' " +
                                  " and SarmutPID in (select ID from SPD_Perusahaan where deptid = 1 and rowstatus> -1 and SarMutPerusahaan = '" + sarmutPrs + "'))) ";
                SqlDataReader sdr1 = zl1.Retrieve();
            }
        }
        private void Update_sarmut_Lsd15_mm_PRESS_L1()
        {
            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "BOARDMILL")
            {
                decimal aktual = 0;

                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "select ROUND(cast((Tot_EfL1)as decimal(10,2)),2) aktual from TempBawah2 where Ket='Sub Total >= 15 mm PRESS'";

                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["aktual"].ToString());

                    }
                }
                if (aktual <= 0)
                    aktual = 0;
                string sarmutPrs = "Effisiensi Pemakaian Flooculant";
                int deptid = getDeptID("BOARDMILL");
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery = " declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                                  " set @bulan=" + ddlBulan.SelectedValue + " " +
                                  " set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                                  " Update  SPD_TransDet set actual = " + aktual.ToString().Replace(",", ".") + " where Approval = 0 and tahun = @tahun and bulan = @bulan and SarmutDeptID in " +
                                  " (select ID from SPD_DepartemenDet where @thnbln >= drperiode and @thnbln<= sdperiode and rowstatus> -1 and SubDepartemen = 'Press >=15 mm' and SarmutDepID in " +
                                  " (select ID from SPD_Departemen where rowstatus > -1 and SarmutDepartemen = 'Line 1' " +
                                  " and SarmutPID in (select ID from SPD_Perusahaan where deptid = 1 and rowstatus> -1 and SarMutPerusahaan = '" + sarmutPrs + "'))) ";
                SqlDataReader sdr1 = zl1.Retrieve();
            }
        }
        private void Update_sarmut_Lsd15_mm_PRESS_L2()
        {
            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "BOARDMILL")
            {
                decimal aktual = 0;

                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "select ROUND(cast((Tot_EfL2)as decimal(10,2)),2) aktual from TempBawah2 where Ket='Sub Total >= 15 mm PRESS'";

                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["aktual"].ToString());

                    }
                }
                if (aktual <= 0)
                    aktual = 0;
                string sarmutPrs = "Effisiensi Pemakaian Flooculant";
                int deptid = getDeptID("BOARDMILL");
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery = " declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                                  " set @bulan=" + ddlBulan.SelectedValue + " " +
                                  " set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                                  " Update  SPD_TransDet set actual = " + aktual.ToString().Replace(",", ".") + " where Approval = 0 and tahun = @tahun and bulan = @bulan and SarmutDeptID in " +
                                  " (select ID from SPD_DepartemenDet where @thnbln >= drperiode and @thnbln<= sdperiode and rowstatus> -1 and SubDepartemen = 'Press >=15 mm' and SarmutDepID in " +
                                  " (select ID from SPD_Departemen where rowstatus > -1 and SarmutDepartemen = 'Line 2' " +
                                  " and SarmutPID in (select ID from SPD_Perusahaan where deptid = 1 and rowstatus> -1 and SarMutPerusahaan = '" + sarmutPrs + "'))) ";
                SqlDataReader sdr1 = zl1.Retrieve();
            }
        }
        private void Update_sarmut_Lsd15_mm_PRESS_L3()
        {
            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "BOARDMILL")
            {
                decimal aktual = 0;

                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "select ROUND(cast((Tot_EfL3)as decimal(10,2)),2) aktual from TempBawah2 where Ket='Sub Total >= 15 mm PRESS'";

                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["aktual"].ToString());

                    }
                }
                if (aktual <= 0)
                    aktual = 0;
                string sarmutPrs = "Effisiensi Pemakaian Flooculant";
                int deptid = getDeptID("BOARDMILL");
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery = " declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                                  " set @bulan=" + ddlBulan.SelectedValue + " " +
                                  " set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                                  " Update  SPD_TransDet set actual = " + aktual.ToString().Replace(",", ".") + " where Approval = 0 and tahun = @tahun and bulan = @bulan and SarmutDeptID in " +
                                  " (select ID from SPD_DepartemenDet where @thnbln >= drperiode and @thnbln<= sdperiode and rowstatus> -1 and SubDepartemen = 'Press >=15 mm' and SarmutDepID in " +
                                  " (select ID from SPD_Departemen where rowstatus > -1 and SarmutDepartemen = 'Line 3' " +
                                  " and SarmutPID in (select ID from SPD_Perusahaan where deptid = 1 and rowstatus> -1 and SarMutPerusahaan = '" + sarmutPrs + "'))) ";
                SqlDataReader sdr1 = zl1.Retrieve();
            }
        }
        private void Update_sarmut_Lsd15_mm_PRESS_L4()
        {
            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "BOARDMILL")
            {
                decimal aktual = 0;

                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "select ROUND(cast((Tot_EfL4)as decimal(10,2)),2) aktual from TempBawah2 where Ket='Sub Total >= 15 mm PRESS'";

                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["aktual"].ToString());

                    }
                }
                if (aktual <= 0)
                    aktual = 0;
                string sarmutPrs = "Effisiensi Pemakaian Flooculant";
                int deptid = getDeptID("BOARDMILL");
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery = " declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                                  " set @bulan=" + ddlBulan.SelectedValue + " " +
                                  " set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                                  " Update  SPD_TransDet set actual = " + aktual.ToString().Replace(",", ".") + " where Approval = 0 and tahun = @tahun and bulan = @bulan and SarmutDeptID in " +
                                  " (select ID from SPD_DepartemenDet where @thnbln >= drperiode and @thnbln<= sdperiode and rowstatus> -1 and SubDepartemen = 'Press >=15 mm' and SarmutDepID in " +
                                  " (select ID from SPD_Departemen where rowstatus > -1 and SarmutDepartemen = 'Line 4' " +
                                  " and SarmutPID in (select ID from SPD_Perusahaan where deptid = 1 and rowstatus> -1 and SarMutPerusahaan = '" + sarmutPrs + "'))) ";
                SqlDataReader sdr1 = zl1.Retrieve();
            }
        }
        private void Update_sarmut_Lsd15_mm_PRESS_L5()
        {
            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "BOARDMILL")
            {
                decimal aktual = 0;

                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "select ROUND(cast((Tot_EfL5)as decimal(10,2)),2) aktual from TempBawah2 where Ket='Sub Total >= 15 mm PRESS'";

                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["aktual"].ToString());

                    }
                }
                if (aktual <= 0)
                    aktual = 0;
                string sarmutPrs = "Effisiensi Pemakaian Flooculant";
                int deptid = getDeptID("BOARDMILL");
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery = " declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                                  " set @bulan=" + ddlBulan.SelectedValue + " " +
                                  " set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                                  " Update  SPD_TransDet set actual = " + aktual.ToString().Replace(",", ".") + " where Approval = 0 and tahun = @tahun and bulan = @bulan and SarmutDeptID in " +
                                  " (select ID from SPD_DepartemenDet where @thnbln >= drperiode and @thnbln<= sdperiode and rowstatus> -1 and SubDepartemen = 'Press >=15 mm' and SarmutDepID in " +
                                  " (select ID from SPD_Departemen where rowstatus > -1 and SarmutDepartemen = 'Line 5' " +
                                  " and SarmutPID in (select ID from SPD_Perusahaan where deptid = 1 and rowstatus> -1 and SarMutPerusahaan = '" + sarmutPrs + "'))) ";
                SqlDataReader sdr1 = zl1.Retrieve();
            }
        }
        private void Update_sarmut_Lsd15_mm_PRESS_L6()
        {
            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "BOARDMILL")
            {
                decimal aktual = 0;

                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "select ROUND(cast((Tot_EfL6)as decimal(10,2)),2) aktual from TempBawah2 where Ket='Sub Total >= 15 mm PRESS'";

                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["aktual"].ToString());

                    }
                }
                if (aktual <= 0)
                    aktual = 0;
                string sarmutPrs = "Effisiensi Pemakaian Flooculant";
                int deptid = getDeptID("BOARDMILL");
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery = " declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                                  " set @bulan=" + ddlBulan.SelectedValue + " " +
                                  " set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                                  " Update  SPD_TransDet set actual = " + aktual.ToString().Replace(",", ".") + " where Approval = 0 and tahun = @tahun and bulan = @bulan and SarmutDeptID in " +
                                  " (select ID from SPD_DepartemenDet where @thnbln >= drperiode and @thnbln<= sdperiode and rowstatus> -1 and SubDepartemen = 'Press >=15 mm' and SarmutDepID in " +
                                  " (select ID from SPD_Departemen where rowstatus > -1 and SarmutDepartemen = 'Line 6' " +
                                  " and SarmutPID in (select ID from SPD_Perusahaan where deptid = 1 and rowstatus> -1 and SarMutPerusahaan = '" + sarmutPrs + "'))) ";
                SqlDataReader sdr1 = zl1.Retrieve();
            }
        }
        private void Update_sarmut_OnTimeDelivery()
        {
            decimal aktual = 0;

            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
            " Declare @date as date = (select dateadd(DAY,0,Getdate())); " +
            " DECLARE @JumlahHari TABLE (Hari VARCHAR(10)); " +

            " INSERT INTO @JumlahHari (Hari) " +
            " VALUES ('0'),('1'),('2'),('3'),('4'),('5'),('6'),('7'),('>7') " +

            " ;with dataAwal as ( " +
            " SELECT DepoName, TotalDOMasuk, TotalDOOntime, CONVERT(decimal(18,2), (CONVERT(decimal, TotalDOOntime)/CONVERT(decimal, TotalDOMasuk))*100) as Hasil FROM " +
            " (SELECT DepoName, SUM(TotalDo) TotalDOMasuk, SUM(CASE WHEN Urutan <= 2 THEN TotalDo ELSE 0 END) TotalDOOntime " +
            " FROM (SELECT DepoName,Jml.Hari,CASE jml.Hari WHEN '0'	THEN 0 WHEN '1'	THEN 1 WHEN '2'	THEN 2 WHEN '3'	THEN 3 WHEN '4' " +
            " THEN 4 WHEN '5' THEN 5 WHEN '6' THEN 6 WHEN '7' THEN 7 WHEN '>7' THEN 8 END Urutan,TotalDo " +
            " FROM @JumlahHari Jml " +
            " LEFT JOIN ( " +
            " SELECT CASE WHEN UmurSJ <= 0 THEN '0'	WHEN UmurSJ > 7	THEN '>7' ELSE convert(VARCHAR, UmurSJ)END UmurSJ,COUNT(OPNo) AS TotalDo,DepoName " +
            " FROM ( " +
            " SELECT * FROM [sql1.grcboard.com].GRCboard.dbo.ISO_OnTimeDelivery " +
            " WHERE YEAR(fromdate)=" + ddlTahun.SelectedValue + " and MONTH(fromdate)=" + ddlBulan.SelectedValue + " " +
            " ) AS A GROUP BY DepoName, " +
            " CASE WHEN UmurSJ <= 0	THEN '0' WHEN UmurSJ > 7 THEN '>7' ELSE convert(VARCHAR, UmurSJ) END " +
            " ) Z ON UmurSJ = Jml.Hari ) AS X GROUP BY DepoName) AS Y ), " +

            " data01 as (select sum(TotalDOMasuk)TotalDOMasuk,sum(TotalDOOntime)TotalDOOntime from dataAwal) " +

            " select cast(cast(sum(TotalDOOntime) as decimal(18,2)) / cast(sum(TotalDOMasuk) as decimal(18,2))*100 as decimal(18,2)) aktual from data01 ";


            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    aktual = Convert.ToDecimal(sdr["aktual"].ToString());

                }
            }
            if (aktual <= 0)
                aktual = 0;
            string sarmutPrs = "Tingkat Produktivitas Delivery";
            int deptid = getDeptID("MARKETING");
            ZetroView zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery =
                "update SPD_TransPrs set actual= " + aktual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddlTahun.SelectedValue +
                " and Bulan=" + ddlBulan.SelectedValue +
                " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "' and deptid=" + deptid + " and Rowstatus>-1) ";
            SqlDataReader sdr1 = zl1.Retrieve();
        }

        //agus-Bending-strength2022-03-16
        public ArrayList ListSubDepartemen(string tahun, string bulan)
        {
            ArrayList arrData = new ArrayList();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery =
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TotalLineAkumulasi]') AND type in (N'U')) DROP TABLE [dbo].[TotalLineAkumulasi]; " +
                "CREATE TABLE TotalLineAkumulasi(Line int,Jenis varchar(50),bendingStrengthC decimal(18,2),bendingStrengthL decimal(18,2),bendingStrengthCL decimal(18,2)) " +
                ";with r as ( " +
                "select " +
                "bt.tgl_prod TglProd, " +
                "bp.plantID as line, " +
                "bt.formula, " +
                "bt.group_produksi, " +
                "bt.jenis_produksi, " +
                //"bt.bendingStrengthC,bt.bendingStrengthL,((bt.bendingStrengthL)/(bt.bendingStrengthC)*100) bendingStrengthCL " +
                "cast(bt.bendingStrengthC as decimal(18,2))bendingStrengthC, cast( bt.bendingStrengthL as decimal(18,2))bendingStrengthL,cast(((cast(bt.bendingStrengthL as decimal(18,2)))/(cast(bt.bendingStrengthC as decimal(18,2)))*100) as decimal(18,2)) bendingStrengthCL " +
                "from BS_Production_Testing_Report bt " +
                "left join BS_Rountine_Test br on bt.tgl_prod = br.tgl_prod and bt.formula = br.formula and br.id_prod = bt.ID " +
                "left join BM_PlantGroup bp on bp.[Group]=bt.group_produksi " +
                "where left(CONVERT(varchar,bt.tgl_prod,112),6)= '" + tahun + "" + bulan + "'), " +

                "AkumulasiLine1 as ( " +

                "select '1' AkumulasiLine,'GRC' Jenis, " +

                //"isnull(AVG(bendingStrengthC),0) bendingStrengthC, " +
                //"isnull(AVG(bendingStrengthL),0) bendingStrengthL, " +
                //"isnull(AVG(bendingStrengthCL),0) bendingStrengthCL " +

                "isnull(cast(AVG(bendingStrengthC)as decimal(18,2)),0) bendingStrengthC, " +
                "isnull(cast(AVG(bendingStrengthL)as decimal(18,2)),0) bendingStrengthL, " +
                "isnull(cast(AVG(bendingStrengthCL)as decimal(18,2)),0) bendingStrengthCL " +
                "from r where left(CONVERT(varchar,TglProd,112),6)='" + tahun + "" + bulan + "' and line = '1' and jenis_produksi in ('GRC','SUP','SMP','SPL')), " +

                "GrcLine1 as ( " +

                "select '1' Line,'GRC' Jenis, " +

                //"isnull(AVG(bendingStrengthC),0) bendingStrengthC, " +
                //"isnull(AVG(bendingStrengthL),0) bendingStrengthL, " +
                //"isnull(AVG(bendingStrengthCL),0) bendingStrengthCL " +

                "isnull(cast(AVG(bendingStrengthC) as decimal(18,2)),0) bendingStrengthC, " +
                "isnull(cast(AVG(bendingStrengthL) as decimal(18,2)),0) bendingStrengthL, " +
                "isnull(cast(AVG(bendingStrengthCL) as decimal(18,2)),0) bendingStrengthCL " +
                "from r " +
                "where left(CONVERT(varchar,TglProd,112),6)='" + tahun + "" + bulan + "' and line = '1'  and jenis_produksi in ('GRC','SUP','SMP','SPL')), " +

                "EraGarudaNanoPinkBoard1  as ( " +

                "select '1' Line,'ERA, GARUDA, NANO, PINKBOARD' Jenis, " +

                //"isnull(AVG(bendingStrengthC),0) bendingStrengthC, " +
                //"isnull(AVG(bendingStrengthL),0) bendingStrengthL, " +
                //"isnull(AVG(bendingStrengthCL),0) bendingStrengthCL " +

                "isnull(cast(AVG(bendingStrengthC) as decimal(18,2)),0) bendingStrengthC, " +
                "isnull(cast(AVG(bendingStrengthL)as decimal(18,2)),0) bendingStrengthL, " +
                "isnull(cast(AVG(bendingStrengthCL) as decimal(18,2)),0) bendingStrengthCL " +
                "from r where left(CONVERT(varchar,TglProd,112),6)='" + tahun + "" + bulan + "' and line = '1' and jenis_produksi in ('PNK','ERA','NNO','STU','GRD')), " +

                "NonPress1  as ( " +

                "select '1' Line,'NON PRESS' Jenis, " +

                //"isnull(AVG(bendingStrengthC),0) bendingStrengthC, " +
                //"isnull(AVG(bendingStrengthL),0) bendingStrengthL, " +
                //"isnull(AVG(bendingStrengthCL),0) bendingStrengthCL " +

                "isnull(cast(AVG(bendingStrengthC)as decimal(18,2)),0) bendingStrengthC, " +
                "isnull(cast(AVG(bendingStrengthL)as decimal(18,2)),0) bendingStrengthL, " +
                "isnull(cast(AVG(bendingStrengthCL)as decimal(18,2)),0) bendingStrengthCL " +

                "from r where left(CONVERT(varchar,TglProd,112),6)='" + tahun + "" + bulan + "' and line = '1' and jenis_produksi in ('CLB','HRE','NJA','TGR','LOT','SUB','DRG','JVB','LOT','TBP','RWP','SMP4')), " +

                "GrcLine2 as ( " +

                "select '2' Line,'GRC' Jenis, " +

                //"isnull(AVG(bendingStrengthC),0) bendingStrengthC, " +
                //"isnull(AVG(bendingStrengthL),0) bendingStrengthL, " +
                //"isnull(AVG(bendingStrengthCL),0) bendingStrengthCL " +

                "isnull(cast(AVG(bendingStrengthC) as decimal(18,2)),0) bendingStrengthC, " +
                "isnull(cast(AVG(bendingStrengthL) as decimal(18,2)),0) bendingStrengthL, " +
                "isnull(cast(AVG(bendingStrengthCL) as decimal(18,2)),0) bendingStrengthCL " +
                "from r where left(CONVERT(varchar,TglProd,112),6)='" + tahun + "" + bulan + "' and line = '2' and jenis_produksi in ('GRC','SUP','SMP','SPL')), " +

                "EraGarudaNanoPinkBoard2  as ( " +

                "select '2' Line,'ERA, GARUDA, NANO, PINKBOARD' Jenis, " +

                //"isnull(AVG(bendingStrengthC),0) bendingStrengthC, " +
                //"isnull(AVG(bendingStrengthL),0) bendingStrengthL, " +
                //"isnull(AVG(bendingStrengthCL),0) bendingStrengthCL " +

                "isnull(cast(AVG(bendingStrengthC) as decimal(18,2)),0) bendingStrengthC, " +
                "isnull(cast(AVG(bendingStrengthL) as decimal(18,2)),0) bendingStrengthL, " +
                "isnull(cast(AVG(bendingStrengthCL) as decimal(18,2)),0) bendingStrengthCL " +
                "from r where left(CONVERT(varchar,TglProd,112),6)='" + tahun + "" + bulan + "' and line = '2' and jenis_produksi in ('PNK','ERA','NNO','STU','GRD')), " +

                "NonPress2  as ( " +

                "select '2' Line,'NON PRESS' Jenis, " +
                "isnull(cast(AVG(bendingStrengthC) as decimal(18,2)),0) bendingStrengthC, " +
                "isnull(cast(AVG(bendingStrengthL) as decimal(18,2)),0) bendingStrengthL, " +
                "isnull(cast(AVG(bendingStrengthCL) as decimal(18,2)),0) bendingStrengthCL " +
                "from r where left(CONVERT(varchar,TglProd,112),6)='" + tahun + "" + bulan + "' and line = '2' and jenis_produksi in ('CLB','HRE','NJA','TGR','LOT','SUB','DRG','JVB','LOT','TBP','RWP','SMP4')), " +

                "GrcLine3 as ( " +

                "select '3' Line,'GRC' Jenis, " +
                "isnull(cast(AVG(bendingStrengthC)as decimal(18,2)),0) bendingStrengthC, " +
                "isnull(cast(AVG(bendingStrengthL)as decimal(18,2)),0) bendingStrengthL, " +
                "isnull(cast(AVG(bendingStrengthCL)as decimal(18,2)),0) bendingStrengthCL " +
                "from r where left(CONVERT(varchar,TglProd,112),6)='" + tahun + "" + bulan + "' and line = '3' and jenis_produksi in ('GRC','SUP','SMP','SPL')), " +

                "EraGarudaNanoPinkBoard3  as ( " +

                "select '3' Line,'ERA, GARUDA, NANO, PINKBOARD' Jenis, " +
                "isnull(cast(AVG(bendingStrengthC)as decimal(18,2)),0) bendingStrengthC, " +
                "isnull(cast(AVG(bendingStrengthL)as decimal(18,2)),0) bendingStrengthL, " +
                "isnull(cast(AVG(bendingStrengthCL)as decimal(18,2)),0) bendingStrengthCL " +
                "from r where left(CONVERT(varchar,TglProd,112),6)='" + tahun + "" + bulan + "' and line = '3' and jenis_produksi in ('PNK','ERA','NNO','STU','GRD')), " +

                "NonPress3  as ( " +

                "select '3' Line,'NON PRESS' Jenis, " +
                "isnull(cast(AVG(bendingStrengthC) as decimal(18,2)),0) bendingStrengthC, " +
                "isnull(cast(AVG(bendingStrengthL)as decimal(18,2)),0) bendingStrengthL, " +
                "isnull(cast(AVG(bendingStrengthCL)as decimal(18,2)),0) bendingStrengthCL " +
                "from r where left(CONVERT(varchar,TglProd,112),6)='" + tahun + "" + bulan + "' and line = '3' and jenis_produksi in ('CLB','HRE','NJA','TGR','LOT','SUB','DRG','JVB','LOT','TBP','RWP','SMP4')), " +

                "GrcLine4 as ( " +

                "select '4' Line,'GRC' Jenis, " +
                "isnull(cast(AVG(bendingStrengthC) as decimal(18,2)),0) bendingStrengthC, " +
                "isnull(cast(AVG(bendingStrengthL)as decimal(18,2)),0) bendingStrengthL, " +
                "isnull(cast(AVG(bendingStrengthCL)as decimal(18,2)),0) bendingStrengthCL " +
                "from r where left(CONVERT(varchar,TglProd,112),6)='" + tahun + "" + bulan + "' and line = '4' and jenis_produksi in ('GRC','SUP','SMP','SPL')), " +

                "EraGarudaNanoPinkBoard4  as ( " +

                "select '4' Line,'ERA, GARUDA, NANO, PINKBOARD' Jenis, " +
                "isnull(cast(AVG(bendingStrengthC)as decimal(18,2)),0) bendingStrengthC, " +
                "isnull(cast(AVG(bendingStrengthL)as decimal(18,2)),0) bendingStrengthL, " +
                "isnull(cast(AVG(bendingStrengthCL)as decimal(18,2)),0) bendingStrengthCL " +
                "from r where left(CONVERT(varchar,TglProd,112),6)='" + tahun + "" + bulan + "' and line = '4' and jenis_produksi in ('PNK','ERA','NNO','STU','GRD')), " +

                "NonPress4  as ( " +

                "select '4' Line,'NON PRESS' Jenis, " +
                "isnull(cast(AVG(bendingStrengthC)as decimal(18,2)),0) bendingStrengthC, " +
                "isnull(cast(AVG(bendingStrengthL)as decimal(18,2)),0) bendingStrengthL, " +
                "isnull(cast(AVG(bendingStrengthCL)as decimal(18,2)),0) bendingStrengthCL " +
                "from r where left(CONVERT(varchar,TglProd,112),6)='" + tahun + "" + bulan + "' and line = '4' and jenis_produksi in ('CLB','HRE','NJA','TGR','LOT','SUB','DRG','JVB','LOT','TBP','RWP','SMP4')), " +

                "GrcLine5 as ( " +

                "select '5' Line,'GRC' Jenis, " +
                "isnull(cast(AVG(bendingStrengthC)as decimal(18,2)),0) bendingStrengthC, " +
                "isnull(cast(AVG(bendingStrengthL)as decimal(18,2)),0) bendingStrengthL, " +
                "isnull(cast(AVG(bendingStrengthCL)as decimal(18,2)),0) bendingStrengthCL " +
                "from r where left(CONVERT(varchar,TglProd,112),6)='" + tahun + "" + bulan + "' and line = '5' and jenis_produksi in ('GRC','SUP','SMP','SPL')), " +

                "EraGarudaNanoPinkBoard5  as ( " +

                "select '5' Line,'ERA, GARUDA, NANO, PINKBOARD' Jenis, " +
                "isnull(cast(AVG(bendingStrengthC)as decimal(18,2)),0) bendingStrengthC, " +
                "isnull(cast(AVG(bendingStrengthL)as decimal(18,2)),0) bendingStrengthL, " +
                "isnull(cast(AVG(bendingStrengthCL)as decimal(18,2)),0) bendingStrengthCL " +
                "from r where left(CONVERT(varchar,TglProd,112),6)='" + tahun + "" + bulan + "' and line = '5' and jenis_produksi in ('PNK','ERA','NNO','STU','GRD')), " +

                "NonPress5  as ( " +

                "select '5' Line,'NON PRESS' Jenis, " +
                "isnull(cast(AVG(bendingStrengthC)as decimal(18,2)),0) bendingStrengthC, " +
                "isnull(cast(AVG(bendingStrengthL)as decimal(18,2)),0) bendingStrengthL, " +
                "isnull(cast(AVG(bendingStrengthCL)as decimal(18,2)),0) bendingStrengthCL " +
                "from r where left(CONVERT(varchar,TglProd,112),6)='" + tahun + "" + bulan + "' and line = '5' and jenis_produksi in ('CLB','HRE','NJA','TGR','LOT','SUB','DRG','JVB','LOT','TBP','RWP','SMP4')), " +

                "GrcLine6 as ( " +

                "select '6' Line,'GRC' Jenis, " +
                "isnull(cast(AVG(bendingStrengthC)as decimal(18,2)),0) bendingStrengthC, " +
                "isnull(cast(AVG(bendingStrengthL)as decimal(18,2)),0) bendingStrengthL, " +
                "isnull(cast(AVG(bendingStrengthCL)as decimal(18,2)) ,0) bendingStrengthCL " +
                "from r where left(CONVERT(varchar,TglProd,112),6)='" + tahun + "" + bulan + "' and line = '6' and jenis_produksi in ('GRC','SUP','SMP','SPL')), " +

                "EraGarudaNanoPinkBoard6  as ( " +

                "select '6' Line,'ERA, GARUDA, NANO, PINKBOARD' Jenis, " +
                "isnull(cast(AVG(bendingStrengthC)as decimal(18,2)),0) bendingStrengthC, " +
                "isnull(cast(AVG(bendingStrengthL)as decimal(18,2)),0) bendingStrengthL, " +
                "isnull(cast(AVG(bendingStrengthCL)as decimal(18,2)),0) bendingStrengthCL " +
                "from r where left(CONVERT(varchar,TglProd,112),6)='" + tahun + "" + bulan + "' and line = '6' and jenis_produksi in ('PNK','ERA','NNO','STU','GRD')), " +

                "NonPress6 as ( " +

                "select '6' Line,'NON PRESS' Jenis, " +
                "isnull(cast(AVG(bendingStrengthC)as decimal(18,2)),0) bendingStrengthC, " +
                "isnull(cast(AVG(bendingStrengthL)as decimal(18,2)),0) bendingStrengthL, " +
                "isnull(cast(AVG(bendingStrengthCL)as decimal(18,2)),0) bendingStrengthCL " +
                "from r where left(CONVERT(varchar,TglProd,112),6)='" + tahun + "" + bulan + "' and line = '6' and jenis_produksi in ('CLB','HRE','NJA','TGR','LOT','SUB','DRG','JVB','LOT','TBP','RWP','SMP4')) " +

                "insert into TotalLineAkumulasi " +

                "SELECT '1' as Line, 'Line 1' AS Jenis, " +
                "isnull(cast((((g1.bendingStrengthC * ( CAST( 182 as decimal(18,2))/cast(182 as decimal(18,2)))) + " +
                "(E1.bendingStrengthC * ( CAST( 182 as decimal(18,2))/cast(149 as decimal(18,2)))) + " +
                "(N1.bendingStrengthC * ( CAST( 182 as decimal(18,2))/cast(132 as decimal(18,2)))))/nullif((select COUNT(bendingStrengthC) + (select count(Line) from EraGarudaNanoPinkBoard1 where bendingStrengthC > 0) + (select count(Line) from NonPress1 where bendingStrengthC > 0) from GrcLine1 where bendingStrengthC > 0),0)) as decimal(18,2)),0) bendingStrengthC, " +

                "isnull(cast((((g1.bendingStrengthL * ( CAST( 127 as decimal(18,2))/cast(127 as decimal(18,2)))) + " +
                "(E1.bendingStrengthL * ( CAST( 127 as decimal(18,2))/cast(105 as decimal(18,2)))) + " +
                "(N1.bendingStrengthL * ( CAST( 127 as decimal(18,2))/cast(94 as decimal(18,2)))))/nullif((select COUNT(bendingStrengthC) + (select count(Line) from EraGarudaNanoPinkBoard1 where bendingStrengthC > 0) + (select count(Line) from NonPress1 where bendingStrengthC > 0) from GrcLine1 where bendingStrengthC > 0),0)) as decimal(18,2)),0) bendingStrengthL, " +

                "isnull(cast((((((g1.bendingStrengthL * ( CAST( 127 as decimal(18,2))/cast(127 as decimal(18,2)))) + " +
                "(E1.bendingStrengthL * ( CAST( 127 as decimal(18,2))/cast(105 as decimal(18,2)))) + " +
                "(N1.bendingStrengthL * ( CAST( 127 as decimal(18,2))/cast(94 as decimal(18,2)))))/nullif((select COUNT(bendingStrengthC) + (select count(Line) from EraGarudaNanoPinkBoard1 where bendingStrengthC > 0) + (select count(Line) from NonPress1 where bendingStrengthC > 0) from GrcLine1 where bendingStrengthC > 0),0)) / " +

                "nullif((((g1.bendingStrengthC * ( CAST( 182 as decimal(18,2))/cast(182 as decimal(18,2)))) +  " +
                "(E1.bendingStrengthC * ( CAST( 182 as decimal(18,2))/cast(149 as decimal(18,2)))) + " +
                "(N1.bendingStrengthC * ( CAST( 182 as decimal(18,2))/cast(132 as decimal(18,2)))))/nullif((select COUNT(bendingStrengthC) + (select count(Line) from EraGarudaNanoPinkBoard1 where bendingStrengthC > 0) + (select count(Line) from NonPress1 where bendingStrengthC > 0) from GrcLine1 where bendingStrengthC > 0),0)),0) ) * 100) as decimal(18,2)),0) bendingStrengthCL " +
                "FROM GrcLine1 G1, EraGarudaNanoPinkBoard1 E1, NonPress1 N1 where E1.Line=1 and G1.Line=1 and N1.Line=1 " +
                "union all " +
                "select * from GrcLine1 " +
                "union all " +
                "select * from EraGarudaNanoPinkBoard1 " +
                "union all " +
                "select * from NonPress1 " +

                "union all " +

                "SELECT '2' as Line, 'Line 2' AS Jenis, " +
                "isnull(cast((((g1.bendingStrengthC * ( CAST( 182 as decimal(18,2))/cast(182 as decimal(18,2)))) + " +
                "(E1.bendingStrengthC * ( CAST( 182 as decimal(18,2))/cast(149 as decimal(18,2)))) + " +
                "(N1.bendingStrengthC * ( CAST( 182 as decimal(18,2))/cast(132 as decimal(18,2)))))/nullif((select COUNT(bendingStrengthC) + (select count(Line) from EraGarudaNanoPinkBoard2 where bendingStrengthC > 0) + (select count(Line) from NonPress2 where bendingStrengthC > 0) from GrcLine2 where bendingStrengthC > 0),0)) as decimal(18,2)),0) bendingStrengthC, " +

                "isnull(cast((((g1.bendingStrengthL * ( CAST( 127 as decimal(18,2))/cast(127 as decimal(18,2)))) + " +
                "(E1.bendingStrengthL * ( CAST( 127 as decimal(18,2))/cast(105 as decimal(18,2)))) + " +
                "(N1.bendingStrengthL * ( CAST( 127 as decimal(18,2))/cast(94 as decimal(18,2)))))/nullif((select COUNT(bendingStrengthC) + (select count(Line) from EraGarudaNanoPinkBoard2 where bendingStrengthC > 0) + (select count(Line) from NonPress2 where bendingStrengthC > 0) from GrcLine2 where bendingStrengthC > 0),0)) as decimal(18,2)),0) bendingStrengthL, " +

                "isnull(cast((((((g1.bendingStrengthL * ( CAST( 127 as decimal(18,2))/cast(127 as decimal(18,2)))) + " +
                "(E1.bendingStrengthL * ( CAST( 127 as decimal(18,2))/cast(105 as decimal(18,2)))) + " +
                "(N1.bendingStrengthL * ( CAST( 127 as decimal(18,2))/cast(94 as decimal(18,2)))))/nullif((select COUNT(bendingStrengthC) + (select count(Line) from EraGarudaNanoPinkBoard2 where bendingStrengthC > 0) + (select count(Line) from NonPress2 where bendingStrengthC > 0) from GrcLine2 where bendingStrengthC > 0),0)) / " +

                "nullif((((g1.bendingStrengthC * ( CAST( 182 as decimal(18,2))/cast(182 as decimal(18,2)))) + " +
                "(E1.bendingStrengthC * ( CAST( 182 as decimal(18,2))/cast(149 as decimal(18,2)))) + " +
                "(N1.bendingStrengthC * ( CAST( 182 as decimal(18,2))/cast(132 as decimal(18,2)))))/nullif((select COUNT(bendingStrengthC) + (select count(Line) from EraGarudaNanoPinkBoard2 where bendingStrengthC > 0) + (select count(Line) from NonPress2 where bendingStrengthC > 0) from GrcLine2 where bendingStrengthC > 0),0)),0) ) * 100) as decimal(18,2)),0) bendingStrengthL " +
                "FROM GrcLine2 G1, EraGarudaNanoPinkBoard2 E1, NonPress2 N1 where E1.Line=2 and G1.Line=2 and N1.Line=2 " +
                "union all " +
                "select * from GrcLine2 " +
                "union all " +
                "select * from EraGarudaNanoPinkBoard2 " +
                "union all " +
                "select * from NonPress2 " +

                "union all " +

                "SELECT '3' as Line, 'Line 3' AS Jenis, " +
                "isnull(cast((((g1.bendingStrengthC * ( CAST( 182 as decimal(18,2))/cast(182 as decimal(18,2)))) + " +
                "(E1.bendingStrengthC * ( CAST( 182 as decimal(18,2))/cast(149 as decimal(18,2)))) + " +
                "(N1.bendingStrengthC * ( CAST( 182 as decimal(18,2))/cast(132 as decimal(18,2)))))/nullif((select COUNT(bendingStrengthC) + (select count(Line) from EraGarudaNanoPinkBoard3 where bendingStrengthC > 0) + (select count(Line) from NonPress3 where bendingStrengthC > 0) from GrcLine3 where bendingStrengthC > 0),0)) as decimal(18,2)),0) bendingStrengthC, " +

                "isnull(cast((((g1.bendingStrengthL * ( CAST( 127 as decimal(18,2))/cast(127 as decimal(18,2)))) + " +
                "(E1.bendingStrengthL * ( CAST( 127 as decimal(18,2))/cast(105 as decimal(18,2)))) + " +
                "(N1.bendingStrengthL * ( CAST( 127 as decimal(18,2))/cast(94 as decimal(18,2)))))/nullif((select COUNT(bendingStrengthC) + (select count(Line) from EraGarudaNanoPinkBoard3 where bendingStrengthC > 0) + (select count(Line) from NonPress3 where bendingStrengthC > 0) from GrcLine3 where bendingStrengthC > 0),0)) as decimal(18,2)),0) bendingStrengthL, " +

                "isnull(cast((((((g1.bendingStrengthL * ( CAST( 127 as decimal(18,2))/cast(127 as decimal(18,2)))) + " +
                "(E1.bendingStrengthL * ( CAST( 127 as decimal(18,2))/cast(105 as decimal(18,2)))) + " +
                "(N1.bendingStrengthL * ( CAST( 127 as decimal(18,2))/cast(94 as decimal(18,2)))))/nullif((select COUNT(bendingStrengthC) + (select count(Line) from EraGarudaNanoPinkBoard3 where bendingStrengthC > 0) + (select count(Line) from NonPress3 where bendingStrengthC > 0) from GrcLine3 where bendingStrengthC > 0),0)) / " +

                "nullif((((g1.bendingStrengthC * ( CAST( 182 as decimal(18,2))/cast(182 as decimal(18,2)))) + " +
                "(E1.bendingStrengthC * ( CAST( 182 as decimal(18,2))/cast(149 as decimal(18,2)))) + " +
                "(N1.bendingStrengthC * ( CAST( 182 as decimal(18,2))/cast(132 as decimal(18,2)))))/nullif((select COUNT(bendingStrengthC) + (select count(Line) from EraGarudaNanoPinkBoard3 where bendingStrengthC > 0) + (select count(Line) from NonPress3 where bendingStrengthC > 0) from GrcLine3 where bendingStrengthC > 0),0)),0) ) * 100) as decimal(18,2)),0) bendingStrengthL " +
                "FROM GrcLine3 G1, EraGarudaNanoPinkBoard3 E1, NonPress3 N1 where E1.Line=3 and G1.Line=3 and N1.Line=3 " +
                "union all " +
                "select * from GrcLine3 " +
                "union all " +
                "select * from EraGarudaNanoPinkBoard3 " +
                "union all " +
                "select * from NonPress3 " +

                "union all " +

                "SELECT '4' as Line, 'Line 4' AS Jenis, " +
                "isnull(cast((((g1.bendingStrengthC * ( CAST( 182 as decimal(18,2))/cast(182 as decimal(18,2)))) + " +
                "(E1.bendingStrengthC * ( CAST( 182 as decimal(18,2))/cast(149 as decimal(18,2)))) + " +
                "(N1.bendingStrengthC * ( CAST( 182 as decimal(18,2))/cast(132 as decimal(18,2)))))/nullif((select COUNT(bendingStrengthC) + (select count(Line) from EraGarudaNanoPinkBoard4 where bendingStrengthC > 0) + (select count(Line) from NonPress4 where bendingStrengthC > 0) from GrcLine4 where bendingStrengthC > 0),0)) as decimal(18,2)),0) bendingStrengthC, " +

                "isnull(cast((((g1.bendingStrengthL * ( CAST( 127 as decimal(18,2))/cast(127 as decimal(18,2)))) + " +
                "(E1.bendingStrengthL * ( CAST( 127 as decimal(18,2))/cast(105 as decimal(18,2)))) + " +
                "(N1.bendingStrengthL * ( CAST( 127 as decimal(18,2))/cast(94 as decimal(18,2)))))/nullif((select COUNT(bendingStrengthC) + (select count(Line) from EraGarudaNanoPinkBoard4 where bendingStrengthC > 0) + (select count(Line) from NonPress4 where bendingStrengthC > 0) from GrcLine4 where bendingStrengthC > 0),0)) as decimal(18,2)),0) bendingStrengthL, " +

                "isnull(cast((((((g1.bendingStrengthL * ( CAST( 127 as decimal(18,2))/cast(127 as decimal(18,2)))) + " +
                "(E1.bendingStrengthL * ( CAST( 127 as decimal(18,2))/cast(105 as decimal(18,2)))) + " +
                "(N1.bendingStrengthL * ( CAST( 127 as decimal(18,2))/cast(94 as decimal(18,2)))))/nullif((select COUNT(bendingStrengthC) + (select count(Line) from EraGarudaNanoPinkBoard4 where bendingStrengthC > 0) + (select count(Line) from NonPress4 where bendingStrengthC > 0) from GrcLine4 where bendingStrengthC > 0),0)) / " +

                "nullif((((g1.bendingStrengthC * ( CAST( 182 as decimal(18,2))/cast(182 as decimal(18,2)))) + " +
                "(E1.bendingStrengthC * ( CAST( 182 as decimal(18,2))/cast(149 as decimal(18,2)))) + " +
                "(N1.bendingStrengthC * ( CAST( 182 as decimal(18,2))/cast(132 as decimal(18,2)))))/nullif((select COUNT(bendingStrengthC) + (select count(Line) from EraGarudaNanoPinkBoard4 where bendingStrengthC > 0) + (select count(Line) from NonPress4 where bendingStrengthC > 0) from GrcLine4 where bendingStrengthC > 0),0)),0) ) * 100) as decimal(18,2)),0) bendingStrengthL " +
                "FROM GrcLine4 G1, EraGarudaNanoPinkBoard4 E1, NonPress4 N1 where E1.Line=4 and G1.Line=4 and N1.Line=4 " +
                "union all " +
                "select * from GrcLine4 " +
                "union all " +
                "select * from EraGarudaNanoPinkBoard4 " +
                "union all " +
                "select * from NonPress4 " +

                "union all " +

                "SELECT '5' as Line, 'Line 5' AS Jenis, " +
                "isnull(cast((((g1.bendingStrengthC * ( CAST( 182 as decimal(18,2))/cast(182 as decimal(18,2)))) + " +
                "(E1.bendingStrengthC * ( CAST( 182 as decimal(18,2))/cast(149 as decimal(18,2)))) + " +
                "(N1.bendingStrengthC * ( CAST( 182 as decimal(18,2))/cast(132 as decimal(18,2)))))/nullif((select COUNT(bendingStrengthC) + (select count(Line) from EraGarudaNanoPinkBoard5 where bendingStrengthC > 0) + (select count(Line) from NonPress5 where bendingStrengthC > 0) from GrcLine5 where bendingStrengthC > 0),0)) as decimal(18,2)),0) bendingStrengthC, " +

                "isnull(cast((((g1.bendingStrengthL * ( CAST( 127 as decimal(18,2))/cast(127 as decimal(18,2)))) + " +
                "(E1.bendingStrengthL * ( CAST( 127 as decimal(18,2))/cast(105 as decimal(18,2)))) + " +
                "(N1.bendingStrengthL * ( CAST( 127 as decimal(18,2))/cast(94 as decimal(18,2)))))/nullif((select COUNT(bendingStrengthC) + (select count(Line) from EraGarudaNanoPinkBoard5 where bendingStrengthC > 0) + (select count(Line) from NonPress5 where bendingStrengthC > 0) from GrcLine5 where bendingStrengthC > 0),0)) as decimal(18,2)),0) bendingStrengthL, " +

                "isnull(cast((((((g1.bendingStrengthL * ( CAST( 127 as decimal(18,2))/cast(127 as decimal(18,2)))) + " +
                "(E1.bendingStrengthL * ( CAST( 127 as decimal(18,2))/cast(105 as decimal(18,2)))) + " +
                "(N1.bendingStrengthL * ( CAST( 127 as decimal(18,2))/cast(94 as decimal(18,2)))))/nullif((select COUNT(bendingStrengthC) + (select count(Line) from EraGarudaNanoPinkBoard5 where bendingStrengthC > 0) + (select count(Line) from NonPress5 where bendingStrengthC > 0) from GrcLine5 where bendingStrengthC > 0),0)) / " +

                "nullif((((g1.bendingStrengthC * ( CAST( 182 as decimal(18,2))/cast(182 as decimal(18,2)))) + " +
                "(E1.bendingStrengthC * ( CAST( 182 as decimal(18,2))/cast(149 as decimal(18,2)))) + " +
                "(N1.bendingStrengthC * ( CAST( 182 as decimal(18,2))/cast(132 as decimal(18,2)))))/nullif((select COUNT(bendingStrengthC) + (select count(Line) from EraGarudaNanoPinkBoard5 where bendingStrengthC > 0) + (select count(Line) from NonPress5 where bendingStrengthC > 0) from GrcLine5 where bendingStrengthC > 0),0)),0) ) * 100) as decimal(18,2)),0) bendingStrengthL " +
                "FROM GrcLine5 G1, EraGarudaNanoPinkBoard5 E1, NonPress5 N1 where E1.Line=5 and G1.Line=5 and N1.Line=5 " +
                "union all " +
                "select * from GrcLine5 " +
                "union all " +
                "select * from EraGarudaNanoPinkBoard5 " +
                "union all " +
                "select * from NonPress5 " +

                "union all " +

                "SELECT '6' as Line, 'Line 6' AS Jenis, " +
                "isnull(cast((((g1.bendingStrengthC * ( CAST( 182 as decimal(18,2))/cast(182 as decimal(18,2)))) + " +
                "(E1.bendingStrengthC * ( CAST( 182 as decimal(18,2))/cast(149 as decimal(18,2)))) + " +
                "(N1.bendingStrengthC * ( CAST( 182 as decimal(18,2))/cast(132 as decimal(18,2)))))/nullif((select COUNT(bendingStrengthC) + (select count(Line) from EraGarudaNanoPinkBoard6 where bendingStrengthC > 0) + (select count(Line) from NonPress6 where bendingStrengthC > 0) from GrcLine6 where bendingStrengthC > 0),0)) as decimal(18,2)),0) bendingStrengthC, " +

                "isnull(cast((((g1.bendingStrengthL * ( CAST( 127 as decimal(18,2))/cast(127 as decimal(18,2)))) + " +
                "(E1.bendingStrengthL * ( CAST( 127 as decimal(18,2))/cast(105 as decimal(18,2)))) + " +
                "(N1.bendingStrengthL * ( CAST( 127 as decimal(18,2))/cast(94 as decimal(18,2)))))/nullif((select COUNT(bendingStrengthC) + (select count(Line) from EraGarudaNanoPinkBoard6 where bendingStrengthC > 0) + (select count(Line) from NonPress6 where bendingStrengthC > 0) from GrcLine6 where bendingStrengthC > 0),0)) as decimal(18,2)),0) bendingStrengthL, " +

                "isnull(cast((((((g1.bendingStrengthL * ( CAST( 127 as decimal(18,2))/cast(127 as decimal(18,2)))) +  " +
                "(E1.bendingStrengthL * ( CAST( 127 as decimal(18,2))/cast(105 as decimal(18,2)))) + " +
                "(N1.bendingStrengthL * ( CAST( 127 as decimal(18,2))/cast(94 as decimal(18,2)))))/nullif((select COUNT(bendingStrengthC) + (select count(Line) from EraGarudaNanoPinkBoard6 where bendingStrengthC > 0) + (select count(Line) from NonPress6 where bendingStrengthC > 0) from GrcLine6 where bendingStrengthC > 0),0)) / " +

                "nullif((((g1.bendingStrengthC * ( CAST( 182 as decimal(18,2))/cast(182 as decimal(18,2)))) + " +
                "(E1.bendingStrengthC * ( CAST( 182 as decimal(18,2))/cast(149 as decimal(18,2)))) + " +
                "(N1.bendingStrengthC * ( CAST( 182 as decimal(18,2))/cast(132 as decimal(18,2)))))/nullif((select COUNT(bendingStrengthC) + (select count(Line) from EraGarudaNanoPinkBoard6 where bendingStrengthC > 0) + (select count(Line) from NonPress6 where bendingStrengthC > 0) from GrcLine6 where bendingStrengthC > 0),0)),0) ) * 100) as decimal(18,2)),0) bendingStrengthL " +
                "FROM GrcLine6 G1, EraGarudaNanoPinkBoard6 E1, NonPress6 N1 where E1.Line=6 and G1.Line=6 and N1.Line=6 " +
                "union all " +
                "select * from GrcLine6 " +
                "union all " +
                "select * from EraGarudaNanoPinkBoard6 " +
                "union all " +
                "select * from NonPress6 " +

                //"select Line,Jenis, bendingStrengthC 'Cross', bendingStrengthL 'Long', bendingStrengthCL 'L/C Ratio' from TotalLineAkumulasi  "+

                //Line 1
                "select Line,Jenis, cast(round(bendingStrengthC,0,0) as float) Actual, 'Cross' Keterangan from TotalLineAkumulasi where Line = 1 and Jenis = 'Line 1' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthL,0,0) as float) Actual, 'Long' Keterangan from TotalLineAkumulasi where Line = 1 and Jenis = 'Line 1' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthCL,0,0) as float) Actual, 'L/C Ratio' Keterangan from TotalLineAkumulasi where Line = 1 and Jenis = 'Line 1' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthC,0,0) as float) Actual, 'Cross' Keterangan from TotalLineAkumulasi where Line = 1 and Jenis = 'GRC' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthL,0,0) as float) Actual, 'Long' Keterangan from TotalLineAkumulasi where Line = 1 and Jenis = 'GRC' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthCL,0,0) as float) Actual, 'L/C Ratio' Keterangan from TotalLineAkumulasi where Line = 1 and Jenis = 'GRC' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthC,0,0) as float) Actual, 'Cross' Keterangan from TotalLineAkumulasi where Line = 1 and Jenis = 'ERA, GARUDA, NANO, PINKBOARD' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthL,0,0) as float) Actual, 'Long' Keterangan from TotalLineAkumulasi where Line = 1 and Jenis = 'ERA, GARUDA, NANO, PINKBOARD' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthCL,0,0) as float) Actual, 'L/C Ratio' Keterangan from TotalLineAkumulasi where Line = 1 and Jenis = 'ERA, GARUDA, NANO, PINKBOARD' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthC,0,0) as float) Actual, 'Cross' Keterangan from TotalLineAkumulasi where Line = 1 and Jenis = 'NON PRESS' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthL,0,0) as float) Actual, 'Long' Keterangan from TotalLineAkumulasi where Line = 1 and Jenis = 'NON PRESS' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthCL,0,0) as float) Actual, 'L/C Ratio' Keterangan from TotalLineAkumulasi where Line = 1 and Jenis = 'NON PRESS' " +

                //Line 2
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthC,0,0) as float) Actual, 'Cross' Keterangan from TotalLineAkumulasi where Line = 2 and Jenis = 'Line 2' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthL,0,0) as float) Actual, 'Long' Keterangan from TotalLineAkumulasi where Line = 2 and Jenis = 'Line 2' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthCL,0,0) as float) Actual, 'L/C Ratio' Keterangan from TotalLineAkumulasi where Line = 2 and Jenis = 'Line 2' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthC,0,0) as float) Actual, 'Cross' Keterangan from TotalLineAkumulasi where Line = 2 and Jenis = 'GRC' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthL,0,0) as float) Actual, 'Long' Keterangan from TotalLineAkumulasi where Line = 2 and Jenis = 'GRC' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthCL,0,0) as float) Actual, 'L/C Ratio' Keterangan from TotalLineAkumulasi where Line = 2 and Jenis = 'GRC' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthC,0,0) as float) Actual, 'Cross' Keterangan from TotalLineAkumulasi where Line = 2 and Jenis = 'ERA, GARUDA, NANO, PINKBOARD' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthL,0,0) as float) Actual, 'Long' Keterangan from TotalLineAkumulasi where Line = 2 and Jenis = 'ERA, GARUDA, NANO, PINKBOARD' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthCL,0,0) as float) Actual, 'L/C Ratio' Keterangan from TotalLineAkumulasi where Line = 2 and Jenis = 'ERA, GARUDA, NANO, PINKBOARD' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthC,0,0) as float) Actual, 'Cross' Keterangan from TotalLineAkumulasi where Line = 2 and Jenis = 'NON PRESS' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthL,0,0) as float) Actual, 'Long' Keterangan from TotalLineAkumulasi where Line = 2 and Jenis = 'NON PRESS' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthCL,0,0) as float) Actual, 'L/C Ratio' Keterangan from TotalLineAkumulasi where Line = 2 and Jenis = 'NON PRESS' " +

                "union all " +

                //Line 3
                "select Line,Jenis, cast(round(bendingStrengthC,0,0) as float) Actual, 'Cross' Keterangan from TotalLineAkumulasi where Line = 3 and Jenis = 'Line 3' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthL,0,0) as float) Actual, 'Long' Keterangan from TotalLineAkumulasi where Line = 3 and Jenis = 'Line 3' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthCL,0,0) as float) Actual, 'L/C Ratio' Keterangan from TotalLineAkumulasi where Line = 3 and Jenis = 'Line 3' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthC,0,0) as float) Actual, 'Cross' Keterangan from TotalLineAkumulasi where Line = 3 and Jenis = 'GRC' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthL,0,0) as float) Actual, 'Long' Keterangan from TotalLineAkumulasi where Line = 3 and Jenis = 'GRC' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthCL,0,0) as float) Actual, 'L/C Ratio' Keterangan from TotalLineAkumulasi where Line = 3 and Jenis = 'GRC' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthC,0,0) as float) Actual, 'Cross' Keterangan from TotalLineAkumulasi where Line = 3 and Jenis = 'ERA, GARUDA, NANO, PINKBOARD' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthL,0,0) as float) Actual, 'Long' Keterangan from TotalLineAkumulasi where Line = 3 and Jenis = 'ERA, GARUDA, NANO, PINKBOARD' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthCL,0,0) as float) Actual, 'L/C Ratio' Keterangan from TotalLineAkumulasi where Line = 3 and Jenis = 'ERA, GARUDA, NANO, PINKBOARD' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthC,0,0) as float) Actual, 'Cross' Keterangan from TotalLineAkumulasi where Line = 3 and Jenis = 'NON PRESS' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthL,0,0) as float) Actual, 'Long' Keterangan from TotalLineAkumulasi where Line = 3 and Jenis = 'NON PRESS' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthCL,0,0) as float) Actual, 'L/C Ratio' Keterangan from TotalLineAkumulasi where Line = 3 and Jenis = 'NON PRESS' " +

                "union all " +
                //Line 4
                "select Line,Jenis, cast(round(bendingStrengthC,0,0) as float) Actual, 'Cross' Keterangan from TotalLineAkumulasi where Line = 4 and Jenis = 'Line 4' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthL,0,0) as float) Actual, 'Long' Keterangan from TotalLineAkumulasi where Line = 4 and Jenis = 'Line 4' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthCL,0,0) as float) Actual, 'L/C Ratio' Keterangan from TotalLineAkumulasi where Line = 4 and Jenis = 'Line 4' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthC,0,0) as float) Actual, 'Cross' Keterangan from TotalLineAkumulasi where Line = 4 and Jenis = 'GRC' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthL,0,0) as float) Actual, 'Long' Keterangan from TotalLineAkumulasi where Line = 4 and Jenis = 'GRC' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthCL,0,0) as float) Actual, 'L/C Ratio' Keterangan from TotalLineAkumulasi where Line = 4 and Jenis = 'GRC' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthC,0,0) as float) Actual, 'Cross' Keterangan from TotalLineAkumulasi where Line = 4 and Jenis = 'ERA, GARUDA, NANO, PINKBOARD' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthL,0,0) as float) Actual, 'Long' Keterangan from TotalLineAkumulasi where Line = 4 and Jenis = 'ERA, GARUDA, NANO, PINKBOARD' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthCL,0,0) as float) Actual, 'L/C Ratio' Keterangan from TotalLineAkumulasi where Line = 4 and Jenis = 'ERA, GARUDA, NANO, PINKBOARD' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthC,0,0) as float) Actual, 'Cross' Keterangan from TotalLineAkumulasi where Line = 4 and Jenis = 'NON PRESS' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthL,0,0) as float) Actual, 'Long' Keterangan from TotalLineAkumulasi where Line = 4 and Jenis = 'NON PRESS' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthCL,0,0) as float) Actual, 'L/C Ratio' Keterangan from TotalLineAkumulasi where Line = 4 and Jenis = 'NON PRESS' " +

                "union all " +

                //Line 5
                "select Line,Jenis, cast(round(bendingStrengthC,0,0) as float) Actual, 'Cross' Keterangan from TotalLineAkumulasi where Line = 5 and Jenis = 'Line 5' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthL,0,0) as float) Actual, 'Long' Keterangan from TotalLineAkumulasi where Line = 5 and Jenis = 'Line 5' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthCL,0,0) as float) Actual, 'L/C Ratio' Keterangan from TotalLineAkumulasi where Line = 5 and Jenis = 'Line 5' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthC,0,0) as float) Actual, 'Cross' Keterangan from TotalLineAkumulasi where Line = 5 and Jenis = 'GRC' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthL,0,0) as float) Actual, 'Long' Keterangan from TotalLineAkumulasi where Line = 5 and Jenis = 'GRC' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthCL,0,0) as float) Actual, 'L/C Ratio' Keterangan from TotalLineAkumulasi where Line = 5 and Jenis = 'GRC' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthC,0,0) as float) Actual, 'Cross' Keterangan from TotalLineAkumulasi where Line = 5 and Jenis = 'ERA, GARUDA, NANO, PINKBOARD' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthL,0,0) as float) Actual, 'Long' Keterangan from TotalLineAkumulasi where Line = 5 and Jenis = 'ERA, GARUDA, NANO, PINKBOARD' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthCL,0,0) as float) Actual, 'L/C Ratio' Keterangan from TotalLineAkumulasi where Line = 5 and Jenis = 'ERA, GARUDA, NANO, PINKBOARD' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthC,0,0) as float) Actual, 'Cross' Keterangan from TotalLineAkumulasi where Line = 5 and Jenis = 'NON PRESS' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthL,0,0) as float) Actual, 'Long' Keterangan from TotalLineAkumulasi where Line = 5 and Jenis = 'NON PRESS' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthCL,0,0) as float) Actual, 'L/C Ratio' Keterangan from TotalLineAkumulasi where Line = 5 and Jenis = 'NON PRESS' " +

                "union all " +

                //Line 6
                "select Line,Jenis, cast(round(bendingStrengthC,0,0) as float) Actual, 'Cross' Keterangan from TotalLineAkumulasi where Line = 6 and Jenis = 'Line 6' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthL,0,0) as float) Actual, 'Long' Keterangan from TotalLineAkumulasi where Line = 6 and Jenis = 'Line 6' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthCL,0,0) as float) Actual, 'L/C Ratio' Keterangan from TotalLineAkumulasi where Line = 6 and Jenis = 'Line 6' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthC,0,0) as float) Actual, 'Cross' Keterangan from TotalLineAkumulasi where Line = 6 and Jenis = 'GRC' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthL,0,0) as float) Actual, 'Long' Keterangan from TotalLineAkumulasi where Line = 6 and Jenis = 'GRC' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthCL,0,0) as float) Actual, 'L/C Ratio' Keterangan from TotalLineAkumulasi where Line = 6 and Jenis = 'GRC' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthC,0,0) as float) Actual, 'Cross' Keterangan from TotalLineAkumulasi where Line = 6 and Jenis = 'ERA, GARUDA, NANO, PINKBOARD' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthL,0,0) as float) Actual, 'Long' Keterangan from TotalLineAkumulasi where Line = 6 and Jenis = 'ERA, GARUDA, NANO, PINKBOARD' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthCL,0,0) as float) Actual, 'L/C Ratio' Keterangan from TotalLineAkumulasi where Line = 6 and Jenis = 'ERA, GARUDA, NANO, PINKBOARD' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthC,0,0) as float) Actual, 'Cross' Keterangan from TotalLineAkumulasi where Line = 6 and Jenis = 'NON PRESS' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthL,0,0) as float) Actual, 'Long' Keterangan from TotalLineAkumulasi where Line = 6 and Jenis = 'NON PRESS' " +
                "union all " +
                "select Line,Jenis, cast(round(bendingStrengthCL,0,0) as float) Actual, 'L/C Ratio' Keterangan from TotalLineAkumulasi where Line = 6 and Jenis = 'NON PRESS' " +

                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TotalLineAkumulasi]') AND type in (N'U')) DROP TABLE [dbo].[TotalLineAkumulasi]; ";

            SqlDataReader sdr = zw.Retrieve();


            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new BendingStrength
                    {
                        Line = Convert.ToInt32(sdr["Line"].ToString()),
                        Jenis = sdr["Jenis"].ToString(),
                        Actual = Convert.ToDecimal(sdr["Actual"].ToString()),
                        Keterangan = sdr["Keterangan"].ToString()

                    });
                }
            }



            return arrData;
        }
        //agus-Bending-strength 2022-03-16


        //Update Sarmut BendingStrength-Agus-2022-03-16
        private void Update_sarmut_BendingStrength()
        {
            SarmutPESFacade sp = new SarmutPESFacade();

            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "BOARDMILL")
            {
                ArrayList arrD = this.ListSubDepartemen(ddlTahun.SelectedValue, ddlBulan.SelectedValue);
                decimal aktual = 0;
                string sarmutPrs = "Bending Strength";
                foreach (BendingStrength bs in arrD)
                {

                    aktual = bs.Actual;


                    if (aktual <= 0)
                        aktual = 0;

                    int deptid = getDeptID("BOARDMILL");
                    ZetroView zl1 = new ZetroView();
                    zl1.QueryType = Operation.CUSTOM;
                    zl1.CustomQuery =

                    "update st set st.Actual = " + aktual.ToString().Replace(",", ".") + " " +
                    "from " +
                    "SPD_TransDet st, " +
                    "SPD_DepartemenDet sdd, " +
                    "SPD_Departemen sd, " +
                    "SPD_Perusahaan sp " +
                    "where " +
                    "sd.SarmutPID = sp.ID " +
                    "and sdd.SarmutDepID =  sd.ID " +
                    "and st.SarmutDeptID = sdd.ID " +
                    "and Tahun=" + ddlTahun.SelectedValue + " and Bulan=" + ddlBulan.SelectedValue + " " +
                    "and sp.SarMutPerusahaan = '" + sarmutPrs + "' and sd.SarmutDepartemen= 'Line " + bs.Line + "'  " +
                    "and sdd.SubDepartemen= '" + bs.Keterangan + "' and sdd.keterangan='" + bs.Jenis + "' and st.approval=0 ";

                    SqlDataReader sdr1 = zl1.Retrieve();

                }
            }
        }
        //Update Sarmut BendingStrength-Agus-2022-03-16

        //add hasan end januari
        private void Update_pemantauan_hasil_packing()
        {
            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "LOGISTIK FINISHGOOD")
            {
                string sarmutPrs = "Pemantauan Hasil Packing";
                decimal aktual = 0;
                string thn = ddlTahun.SelectedValue.ToString().Trim();
                string bln = (ddlBulan.SelectedValue.Length == 1) ? "0" + ddlBulan.SelectedValue.ToString().Trim() : ddlBulan.SelectedValue.ToString().Trim();
                string periode = thn + bln;
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "select pencapaian from pantaupackingaproval where bulan= " + ddlBulan.SelectedValue.ToString() + " and tahun= " + ddlTahun.SelectedValue.ToString() +
                                " and rowstatus>-1 and statusapv=3";
                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["pencapaian"].ToString());
                    }
                }

                if (aktual <= 0)
                    aktual = 0;
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery =
                    "update SPD_TransPrs set actual= " + aktual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddlTahun.SelectedValue +
                    " and Bulan=" + ddlBulan.SelectedValue +
                    " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "' and deptid=16) ";
                SqlDataReader sdr1 = zl1.Retrieve();
            }
        }
        // add hasan des 2021
        private void Update_customer_non_mutu()
        {
            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "MARKETING")
            {
                string sarmutPrs = "Customer Complaint Non Mutu";
                decimal aktual = 0;
                string thn = ddlTahun.SelectedValue.ToString().Trim();
                string bln = (ddlBulan.SelectedValue.Length == 1) ? "0" + ddlBulan.SelectedValue.ToString().Trim() : ddlBulan.SelectedValue.ToString().Trim();
                string periode = thn + bln;
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "select count(id)aktual from tpp where rowstatus>-1 and Asal_M_ID=7 and left(convert(char,TPP_Date,112),6)=" + periode;
                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["aktual"].ToString());
                    }
                }

                if (aktual <= 0)
                    aktual = 0;
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery =
                    "update SPD_TransPrs set actual= " + aktual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddlTahun.SelectedValue +
                    " and Bulan=" + ddlBulan.SelectedValue +
                    " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "' and deptid=17) ";
                SqlDataReader sdr1 = zl1.Retrieve();
            }
        }

        /** Beny added 30 November 2021 **/
        private void AutoLink_Sarmut_Produktifitas()
        {
            string sarmutPrs = "Tingkat Produktivitas Delivery"; decimal aktual = 0;
            string thn = ddlTahun.SelectedValue.ToString().Trim();
            string bln = (ddlBulan.SelectedValue.Length == 1) ? "0" + ddlBulan.SelectedValue.ToString().Trim() : ddlBulan.SelectedValue.ToString().Trim();
            string periode = thn + bln;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
            " select isnull(Persen,0)Aktual from Sarmut_Nilai where ItemSarmut='Tingkat Produktivitas Delivery' " +
            " and periode=" + periode + " and rowstatus>-1 ";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    aktual = Convert.ToDecimal(sdr["aktual"].ToString());

                }
            }

            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "MARKETING")
            {
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery =
                    "update SPD_TransPrs set actual= " + aktual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddlTahun.SelectedValue +
                    " and Bulan=" + ddlBulan.SelectedValue +
                    " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "' and deptid=17) ";
                SqlDataReader sdr1 = zl1.Retrieve();
            }
        }

        //Update Sarmut Agus
        private void Update_sarmut_Purchn()
        {
            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "PURCHASING")
            {
                decimal aktual = 0;

                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "EXEC('IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].[ParDe]'') AND type in (N''U'')) DROP TABLE [dbo].[ParDe]')CREATE TABLE ParDe  (ID int,SchID INT, ReceiptDate varchar(10),SupplierName varchar(100),ItemName Varchar(100),ItemID int,SPPno Varchar(20),PONo Varchar(20),SchDeliver datetime,EstimasiQty decimal(18,2),QtySch decimal(18,2), ReceiptNo varchar(20), Qty decimal(18,2), TotalDatang int,ReceiptOK int,JenisBiaya int,Keterangan Varchar(max),Urutan int )" +
                                "INSERT INTO ParDe exec dbo.ParsialDelivery " + ddlBulan.SelectedValue + ", " + ddlTahun.SelectedValue + ",'false';" +
                                "INSERT INTO ParDe exec dbo.sarmutParsialMonitoring '" + ddlTahun.SelectedValue + "','" + ddlBulan.SelectedValue + "'" +
                                ";WITH DeliveryD AS (" +
                                "SELECT case when ReceiptDate > SchDeliver  then 1 Else 0 end NotOntime,TotalDatang FROM ParDe where ItemName!='PVA FIBER' and ItemName!='PULP VIRGIN NUKP' and ItemName!='UNCOATED KRAFT PAPER' and ID!=99999)" +
                                "select isnull(cast(cast(count(TotalDatang)-sum(NotOntime) as decimal(18,2)) / cast(count(TotalDatang)as decimal(18,2)) * 100 as decimal(18,2)),0) aktual from DeliveryD " +
                                "EXEC('IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].[ParDe]'') AND type in (N''U'')) DROP TABLE [dbo].[ParDe]')";
                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["aktual"].ToString());

                    }
                }
                if (aktual <= 0)
                    aktual = 0;
                string sarmutPrs = "ON Time Pembelian Bhn Baku , Bhn Penunjang, & bahan bakar";
                int deptid = getDeptID("PURCHASING");
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery =
                    "update SPD_TransPrs set actual= " + aktual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddlTahun.SelectedValue +
                    " and Bulan=" + ddlBulan.SelectedValue +
                    " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "' and deptid=" + deptid + ") ";
                SqlDataReader sdr1 = zl1.Retrieve();
            }
        }
        //Update Sarmut Agus

        //Update Sarmut Razib
        private void Update_sarmut_Scrab_Kering()
        {
            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "FINISHING")
            {
                decimal aktual = 0;

                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = ";with hasil as " +
                                "( select * from ( select ID, isnull(convert(varchar,tglinput,105),0)as Tanggal, " +
                                "case typescrab when isnull(1,0) then 'Scrab Kering' when isnull(2,0) then 'Scrab Basah' end 'TypeScrab', " +
                                "case typeinput when isnull(1,0) then 'Palet' else '' end Palet, isnull(jumlah,0) as 'Jumlah(Palet)', isnull(berat,0) as 'Berat(Palet)Kg', " +
                                "isnull((CAST(berat as decimal))/CAST(1596 as decimal),0) as 'M3/Palet', keterangan as 'Keterangan(Palet)' " +
                                "from Scrab  WHERE left(CONVERT(CHAR,tglinput,112),6)='" + ddlTahun.SelectedValue + "" + ddlBulan.SelectedValue + "' and RowStatus >-1  and typeinput=1  and typescrab =1) A full outer join " +
                                "( select isnull(convert(varchar,tglinput,105),0) as Tanggal1, case typescrab when isnull(1,0) then 'Scrab Kering' " +
                                "when isnull(2,0) then 'Scrab Basah' end 'TypeScrab1', " +
                                "case typeinput when isnull(2,0) then 'Bin' else '' end Bin, isnull(jumlah,0) as 'Jumlah(Bin)', isnull(berat,0) as 'Berat(Bin)Kg', " +
                                "isnull(CAST(berat as decimal)/CAST(1595 as decimal),0) as 'M3/Bin', keterangan 'Keterangan(Bin)' " +
                                "from Scrab where left(CONVERT(CHAR,tglinput,112),6)='" + ddlTahun.SelectedValue + "" + ddlBulan.SelectedValue + "' and RowStatus >-1  and typeinput=2  and typescrab =1) B on A.Tanggal =B.Tanggal1) " +
                                "select isnull(cast(sum(isnull([M3/Palet],0) + isnull([M3/Bin],0))as decimal(18,1)),0) as 'aktual' from hasil";
                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["aktual"].ToString());

                    }
                }
                if (aktual <= 0)
                    aktual = 0;
                string sarmutPrs = "Scrab Kering";
                int deptid = getDeptID("FINISHING");
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery =
                    "update SPD_TransPrs set actual= " + aktual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddlTahun.SelectedValue +
                    " and Bulan=" + ddlBulan.SelectedValue +
                    " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "' and deptid=" + deptid + ") ";
                SqlDataReader sdr1 = zl1.Retrieve();
            }
        }
        //End by Razib
        //Update Sarmut Agus Scrab Basah
        private void Update_sarmut_Scrab_Basah()
        {
            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "BOARDMILL")
            {
                decimal aktual = 0;

                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = ";with hasil as " +
                                "( select * from ( select ID, isnull(convert(varchar,tglinput,105),0)as Tanggal, " +
                                "case typescrab when isnull(1,0) then 'Scrab Kering' when isnull(2,0) then 'Scrab Basah' end 'TypeScrab', " +
                                "case typeinput when isnull(1,0) then 'Palet' else '' end Palet, isnull(jumlah,0) as 'Jumlah(Palet)', isnull(berat,0) as 'Berat(Palet)Kg', " +
                                "isnull((CAST(berat as decimal))/CAST(1596 as decimal),0) as 'M3/Palet', keterangan as 'Keterangan(Palet)' " +
                                "from Scrab  WHERE left(CONVERT(CHAR,tglinput,112),6)='" + ddlTahun.SelectedValue + "" + ddlBulan.SelectedValue + "' and RowStatus >-1  and typeinput=1  and typescrab =2) A full outer join " +
                                "( select isnull(convert(varchar,tglinput,105),0) as Tanggal1, case typescrab when isnull(1,0) then 'Scrab Kering' " +
                                "when isnull(2,0) then 'Scrab Basah' end 'TypeScrab1', " +
                                "case typeinput when isnull(2,0) then 'Bin' else '' end Bin, isnull(jumlah,0) as 'Jumlah(Bin)', isnull(berat,0) as 'Berat(Bin)Kg', " +
                                "isnull(CAST(berat as decimal)/CAST(1595 as decimal),0) as 'M3/Bin', keterangan 'Keterangan(Bin)' " +
                                "from Scrab where left(CONVERT(CHAR,tglinput,112),6)='" + ddlTahun.SelectedValue + "" + ddlBulan.SelectedValue + "' and RowStatus >-1  and typeinput=2  and typescrab =2) B on A.Tanggal =B.Tanggal1) " +
                                "select isnull(cast(sum(isnull([M3/Palet],0) + isnull([M3/Bin],0))as decimal(18,1)),0) as 'aktual' from hasil";
                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["aktual"].ToString());

                    }
                }
                if (aktual <= 0)
                    aktual = 0;
                string sarmutPrs = "Scrab Basah";
                int deptid = getDeptID("BOARDMILL");
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery =
                    "update SPD_TransPrs set actual= " + aktual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddlTahun.SelectedValue +
                    " and Bulan=" + ddlBulan.SelectedValue +
                    " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "' and deptid=" + deptid + ") ";
                SqlDataReader sdr1 = zl1.Retrieve();
            }
        }
        //Update Sarmut Agus Scrab Basah


        // add hasan nov 2021
        private void Update_sarmut_Pelarian()
        {
            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "BOARDMILL")
            {
                decimal aktual = 0;

                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "declare @thnbln char(6) " +
                                "set @thnbln='" + ddlTahun.SelectedValue + ddlBulan.SelectedValue.ToString().PadLeft(2, '0') + "' " +
                                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SmtLari]') AND type in (N'U')) DROP TABLE [dbo].SmtLari  " +
                                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SmtLari1]') AND type in (N'U')) DROP TABLE [dbo].SmtLari1  " +
                                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ItemLari]') AND type in (N'U')) DROP TABLE [dbo].ItemLari  " +
                                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DataLari]') AND type in (N'U')) DROP TABLE [dbo].DataLari  " +

                                "select distinct itemid0 into ItemLari from vw_kartustockwip K inner join BM_Destacking D on K.destid=D.ID  inner join BM_Plant P on D.PlantID =P.ID   " +
                                "where left(convert(char,tanggal,112),6)=@thnbln and lokasi='p99'  " +
                                "select * into SmtLari1 from (  " +
                                "select I.Tebal,I.PartNo,P.PlantName,PG.[Group],(sum(K.qty*-1*(I.tebal*I.lebar*I.Panjang)/1000000000)) qty,  " +
                                "rtrim(cast(cast(I.Tebal as decimal(8,1)) as char)) + ' mm x ' + rtrim(cast(I.lebar as int)) + ' x ' + rtrim(cast(I.Panjang as int)) Ukuran  from vw_kartustockwip K    " +
                                "inner join fc_items I on K.itemid0=I.id  inner join BM_Destacking D on K.destid=D.ID  inner join BM_Plant P on D.PlantID =P.ID  " +
                                "inner join BM_PlantGroup PG on D.PlantGroupID=PG.ID    " +
                                "where left(convert(char,tanggal,112),6)=@thnbln and K.qty<0 and lokasi<>'p99' and  " +
                                "itemid0 in (select itemid0 from ItemLari) group by I.PartNo,P.PlantName,PG.[Group],I.Tebal,I.lebar,I.Panjang )A  " +
                                "select  P.PlantName,PG.[Group],  rtrim(cast(cast(I1.Tebal as decimal(8,1)) as char)) + ' mm x ' + rtrim(cast(I1.lebar as int)) + ' x ' + rtrim(cast(I1.Panjang as int)) Ukuran,  " +
                                "I1.partno partno0, I2.partno,cast(I2.Tebal as decimal(8,1)) lari, I1.tebal,sum(J.QtyIn *(I2.tebal*I2.lebar*I2.Panjang)/1000000000) QtyIn  " +
                                "into SmtLari from T1_JemurLg J inner join BM_Destacking D on J.DestID=D.ID inner join fc_items I1 on D.ItemID=I1.ID inner join fc_items I2 on J.ItemID=I2.ID   " +
                                "inner join BM_Plant P on D.PlantID=P.ID inner join BM_PlantGroup PG on D.PlantGroupID=PG.ID   " +
                                "where left(convert(char,tgljemur,112),6)=@thnbln  group by P.PlantName,PG.[Group],I1.Tebal,I1.lebar,I1.Panjang, " +
                                "I1.partno,I2.partno,I2.Tebal,I1.id order by P.PlantName   " +

                                //"select *,([3A]+[3B]+[4A]+[4B]+[4C]+[5B]+[5C]+[6]+[7]+[8]+[9]+[10]+[11]+[12]+[13]+[14]+[15]+[16]+[17]+[18]+[19]+[20]+[21]+[22])+TPotong TLari from (  " +

                                "select *,([3A]+[3B]+[4A]+[4B]+[4C]+[5B]+[5C]+[6]+[7]+[8]+[9]+[10]+[11]+[12]+[13]+[14]+[15]+[16]+[17]+[18]+[19]+[20]+[21]+[22])+TPotong TLari into DataLari from (  " +
                                "select Tebal,[group],ukuran, sum([3A])[3A],sum([3B])[3B],sum([4A])[4A],sum([4B])[4B],sum([4C])[4C],sum([5B])[5B],sum([5C])[5C],sum([6])[6],sum([7])[7],sum([8])[8],  " +
                                "sum([9])[9],sum([10])[10],sum([11])[11],sum([12])[12],sum([13])[13],sum([14])[14],sum([15])[15],sum([16])[16],sum([17])[17],sum([18])[18],  " +
                                "sum([19])[19],sum([20])[20],sum([21])[21],sum([22])[22],sum(TPotong) TPotong from (  " +
                                "select * from (  " +
                                "select Tebal,[group],ukuran, sum([3A])[3A],sum([3B])[3B],sum([4A])[4A],sum([4B])[4B],sum([4C])[4C],sum([5B])[5B],sum([5C])[5C],sum([6])[6],sum([7])[7],sum([8])[8],  " +
                                "sum([9])[9],sum([10])[10],sum([11])[11],sum([12])[12],sum([13])[13],sum([14])[14],sum([15])[15],sum([16])[16],sum([17])[17],sum([18])[18],  " +
                                "sum([19])[19],sum([20])[20],sum([21])[21],sum([22])[22],0 TPotong from (  " +
                                "select *,case when lari=3 then QtyIn else 0 end [3A],0.000000 [3B],case when lari=3.5 then QtyIn else 0 end [4A],case when lari=4 then QtyIn else 0 end  [4B],case when lari>4 and lari<5 then QtyIn else 0 end  [4C],  " +
                                "case when lari=5 then QtyIn else 0 end [5B],case when lari>5 and lari<6 then QtyIn else 0 end [5C],case when lari=6 then QtyIn else 0 end [6],  " +
                                "case when lari=7 then QtyIn else 0 end [7],case when lari=8 then QtyIn else 0 end [8],case when lari=9 then QtyIn else 0 end [9],case when lari=10 then QtyIn else 0 end [10],  " +
                                "case when lari=11 then QtyIn else 0 end [11],case when lari=12 then QtyIn else 0 end [12],case when lari=13 then QtyIn else 0 end [13],case when lari=14 then QtyIn else 0 end [14], " +
                                "case when lari=15 then QtyIn else 0 end [15],case when lari=16 then QtyIn else 0 end [16],case when lari=17 then QtyIn else 0 end [17],case when lari=18 then QtyIn else 0 end [18],  " +
                                "case when lari=19 then QtyIn else 0 end [19],case when lari=20 then QtyIn else 0 end [20],case when lari=21 then QtyIn else 0 end [21],case when lari=22 then QtyIn else 0 end [22]  " +
                                "from SmtLari    " +
                                ")A group by [group],ukuran,tebal,Partno0)B   " +
                                "union all  " +
                                "select Tebal,A.[Group],Ukuran,[3A],[3B],[4A],[4B],[4C],[5B],[5C],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],qty TPotong from smtlari1 A    " +
                                "inner join (  " +
                                "select P.PlantName,G.[Group],0.0 [3A],0.0 [3B],0.0 [4A],0.0 [4B],0.0 [4C],0.0 [5B],0.0 [5C],0.0 [6],0.0 [7],0.0 [8],0.0 [9],0.0 [10],0.0 [11],0.0 [12],0.0 [13],0.0 [14],  " +
                                "0.0 [15],0.0 [16],0.0 [17],0.0 [18],0.0 [19],0.0 [20],0.0 [21],0.0 [22] from BM_PlantGroup G inner join BM_Plant P on G.PlantID =P.ID )A0   " +
                                "on A.PlantName=A0.PlantName and A.[Group]=A0.[group]" +
                                ")C group by Tebal,[group],ukuran)D   order by [group],tebal,ukuran  " +

                                "select isnull(round(actual,2),0)actual from( " +
                                "select (Isnull(trk6 /  NULLIF((trk6+trb6), 0), 0)*tpk)+(Isnull(trb6 /  NULLIF((trk6+trb6), 0), 0)*tpb)/8 actual from( " +
                                "select (Isnull(trk6 /  NULLIF(trlk, 0), 0)*100)tpk,(Isnull(trb6 /  NULLIF(trlb, 0), 0)*100)tpb, trk6,trb6,trlk,trlb from(" +
                                "select sum(trk6)trk6,sum(trb6)trb6, sum(tlk)trlk,sum(tlb)trlb from ( " +
                                "select case when tebal<=6 then total else 0 end trk6,case when tebal>6 then total else 0 end trb6, " +
                                "case when tebal<=6 then TLari else 0 end tlk,case when tebal>6 then tlari else 0 end tlb from ( " +
                                "select Tebal, [3A]+ [3B]+ [4A]+ [4B]+ [4C]+ [5B]+ [5C]+ [6]+[7]+ [8]+ [9]+ [10]+ [11]+ [12]+ [13]+ [14]+ " +
                                "[15]+ [16]+ [17]+ [18]+ [19]+ [20]+ [21]+ [22] total , TLari " +
                                "from DataLari )A )B )C )D )E " +

                                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ItemLari]') AND type in (N'U')) DROP TABLE [dbo].ItemLari  " +
                                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SmtLari1]') AND type in (N'U')) DROP TABLE [dbo].SmtLari1  " +
                                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SmtLari]') AND type in (N'U')) DROP TABLE [dbo].SmtLari " +
                                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DataLari]') AND type in (N'U')) DROP TABLE [dbo].DataLari  ";

                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["actual"].ToString());

                    }
                }
                if (aktual <= 0)
                    aktual = 0;
                string sarmutPrs = "Pelarian Produk";
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery =
                    "update SPD_TransPrs set actual= " + aktual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddlTahun.SelectedValue +
                    " and Bulan=" + ddlBulan.SelectedValue +
                    " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "' ) ";
                SqlDataReader sdr1 = zl1.Retrieve();
            }
        }
        private void Update_Sarmut_Logistik_M()
        {
            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "LOGISTIK MATERIAL")
            {
                //update BB,BP
                decimal aktual = 0;
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = " declare @tahun int,@bulan int,@tahun1 int,@bulan1 int " +
                     "set @tahun=" + ddlTahun.SelectedValue + " set @bulan=" + ddlBulan.SelectedValue + " " +
                     "if @bulan=12 begin set  @bulan1=1 set @tahun1=@tahun+1 end " +
                     "else begin set  @bulan1=@bulan+1 set @tahun1=@tahun end " +
                     "select top 1 cast(cast(CAST(@tahun1 as char(4))+ REPLACE(STR(@bulan1, 2), SPACE(1), '0') +'04' as datetime) - cast(convert(char,TglKirim,112) as datetime) as int)*-1 aktual from ( " +
                     "select B.ID,C.GroupDescription,A.FileName, case when B.Status=4 then 'Email sent' when B.Status=1  then 'Release' when B.Status=2 then 'Approved Mgr Log'  " +
                     "when B.Status=3 and B.Cetak is null then 'Approved PM'  when B.Cetak=1 and B.status=3 then 'Printed' end keterangan,isnull(B.SentEmailTime,0) as TglKirim  " +
                     "from Elapbul_File as A  INNER JOIN ELapbul as B ON A.LapID=B.ID  INNER JOIN GroupsPurchn as C ON C.ID=B.GroupPurchn where A.RowStatus>-1 and B.Rowstatus>-1  " +
                     "and B.tahun=@tahun and B.bulan=@bulan and B.GroupPurchn in (1,2,11))a order by TglKirim desc";
                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["aktual"].ToString());
                    }
                }
                if (aktual <= 0)
                    aktual = 0;
                string sarmutPrs = "Ketepatan waktu penyerahan Laporan Bhn Baku, penunjang & bhn bakar";
                int deptid = getDeptID("LOGISTIK MATERIAL");
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery =
                    "update SPD_TransPrs set actual= " + aktual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddlTahun.SelectedValue +
                    " and Bulan=" + ddlBulan.SelectedValue +
                    " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "' and deptid=" + deptid + ") ";
                SqlDataReader sdr1 = zl1.Retrieve();

                //update sparepart
                aktual = 0;
                zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = " declare @tahun int,@bulan int,@tahun1 int,@bulan1 int " +
                     "set @tahun=" + ddlTahun.SelectedValue + " set @bulan=" + ddlBulan.SelectedValue + " " +
                     "if @bulan=12 begin set  @bulan1=1 set @tahun1=@tahun+1 end " +
                     "else begin set  @bulan1=@bulan+1 set @tahun1=@tahun end " +
                     "select top 1 cast(cast(CAST(@tahun1 as char(4))+ REPLACE(STR(@bulan1, 2), SPACE(1), '0') +'04' as datetime) - cast(convert(char,TglKirim,112) as datetime) as int) *-1 aktual from ( " +
                     "select B.ID,C.GroupDescription,A.FileName, case when B.Status=4 then 'Email sent' when B.Status=1  then 'Release' when B.Status=2 then 'Approved Mgr Log'  " +
                     "when B.Status=3 and B.Cetak is null then 'Approved PM'  when B.Cetak=1 and B.status=3 then 'Printed' end keterangan,isnull(B.SentEmailTime,0) as TglKirim  " +
                     "from Elapbul_File as A  INNER JOIN ELapbul as B ON A.LapID=B.ID  INNER JOIN GroupsPurchn as C ON C.ID=B.GroupPurchn where A.RowStatus>-1 and B.Rowstatus>-1  " +
                     "and B.tahun=@tahun and B.bulan=@bulan and B.GroupPurchn in (8,9))a order by TglKirim desc";
                sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["aktual"].ToString());
                    }
                }
                if (aktual <= 0)
                    aktual = 0;
                sarmutPrs = "Ketepatan waktu penyerahan Laporan Spare Part";
                deptid = getDeptID("LOGISTIK MATERIAL");
                zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery =
                    "update SPD_TransPrs set actual= " + aktual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddlTahun.SelectedValue +
                    " and Bulan=" + ddlBulan.SelectedValue +
                    " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "' and deptid=" + deptid + ") ";
                sdr1 = zl1.Retrieve();

                //update marketing
                aktual = 0;
                zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = " declare @tahun int,@bulan int,@tahun1 int,@bulan1 int " +
                     "set @tahun=" + ddlTahun.SelectedValue + " set @bulan=" + ddlBulan.SelectedValue + " " +
                     "if @bulan=12 begin set  @bulan1=1 set @tahun1=@tahun+1 end " +
                     "else begin set  @bulan1=@bulan+1 set @tahun1=@tahun end " +
                     "select top 1 cast(cast(CAST(@tahun1 as char(4))+ REPLACE(STR(@bulan1, 2), SPACE(1), '0') +'04' as datetime) - cast(convert(char,TglKirim,112) as datetime) as int)*-1 aktual from ( " +
                     "select B.ID,C.GroupDescription,A.FileName, case when B.Status=4 then 'Email sent' when B.Status=1  then 'Release' when B.Status=2 then 'Approved Mgr Log'  " +
                     "when B.Status=3 and B.Cetak is null then 'Approved PM'  when B.Cetak=1 and B.status=3 then 'Printed' end keterangan,isnull(B.SentEmailTime,0) as TglKirim  " +
                     "from Elapbul_File as A  INNER JOIN ELapbul as B ON A.LapID=B.ID  INNER JOIN GroupsPurchn as C ON C.ID=B.GroupPurchn where A.RowStatus>-1 and B.Rowstatus>-1  " +
                     "and B.tahun=@tahun and B.bulan=@bulan and B.GroupPurchn in (7))a order by TglKirim desc";
                sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["aktual"].ToString());
                    }
                }
                if (aktual <= 0)
                    aktual = 0;
                sarmutPrs = "Ketepatan waktu penyerahan Laporan Accessories Marketing";
                deptid = getDeptID("LOGISTIK MATERIAL");
                zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery =
                    "update SPD_TransPrs set actual= " + aktual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddlTahun.SelectedValue +
                    " and Bulan=" + ddlBulan.SelectedValue +
                    " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "' and deptid=" + deptid + ") ";
                sdr1 = zl1.Retrieve();

                //update ATK
                aktual = 0;
                zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = " declare @tahun int,@bulan int,@tahun1 int,@bulan1 int " +
                     "set @tahun=" + ddlTahun.SelectedValue + " set @bulan=" + ddlBulan.SelectedValue + " " +
                     "if @bulan=12 begin set  @bulan1=1 set @tahun1=@tahun+1 end " +
                     "else begin set  @bulan1=@bulan+1 set @tahun1=@tahun end " +
                     "select top 1 cast(cast(CAST(@tahun1 as char(4))+ REPLACE(STR(@bulan1, 2), SPACE(1), '0') +'04' as datetime) - cast(convert(char,TglKirim,112) as datetime) as int)*-1 aktual from ( " +
                     "select B.ID,C.GroupDescription,A.FileName, case when B.Status=4 then 'Email sent' when B.Status=1  then 'Release' when B.Status=2 then 'Approved Mgr Log'  " +
                     "when B.Status=3 and B.Cetak is null then 'Approved PM'  when B.Cetak=1 and B.status=3 then 'Printed' end keterangan,isnull(B.SentEmailTime,0) as TglKirim  " +
                     "from Elapbul_File as A  INNER JOIN ELapbul as B ON A.LapID=B.ID  INNER JOIN GroupsPurchn as C ON C.ID=B.GroupPurchn where A.RowStatus>-1 and B.Rowstatus>-1  " +
                     "and B.tahun=@tahun and B.bulan=@bulan and B.GroupPurchn in (3))a order by TglKirim desc";
                sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["aktual"].ToString());
                    }
                }
                if (aktual <= 0)
                    aktual = 0;
                sarmutPrs = "Ketepatan waktu penyerahan Laporan ATK";
                deptid = getDeptID("LOGISTIK MATERIAL");
                zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery =
                    "update SPD_TransPrs set actual= " + aktual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddlTahun.SelectedValue +
                    " and Bulan=" + ddlBulan.SelectedValue +
                    " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "' and deptid=" + deptid + ") ";
                sdr1 = zl1.Retrieve();
            }
        }

        // add hasan agustus 2022
        private void Update_sarmut_DefectBM()
        {
            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "BOARDMILL")
            {
                decimal aktual = 0;
                int Line = 0;
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = " select isnull(cast((TotalDefect/TotalSerah)*100 as decimal(18,2)),0) actual from ( " +
                                 " select sum(TotalDefect)TotalDefect, sum(TotalSerah)TotalSerah from Temp_DefectSarMut " +
                                 " where GP not like ('Total%') and Periode = '" + ddlTahun.SelectedValue + ddlBulan.SelectedValue + "') as x";
                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        aktual = Convert.ToDecimal(sdr["actual"].ToString());

                    }
                }

                if (aktual <= 0)
                    aktual = 0;
                string sarmutPrs = "Defect Board Mill";
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery =
                    "update SPD_TransPrs set actual= " + aktual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddlTahun.SelectedValue +
                    " and Bulan=" + ddlBulan.SelectedValue +
                    " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "' ) ";
                SqlDataReader sdr1 = zl1.Retrieve();

                zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "select count(id)line from SPD_Departemen where SarmutPID in(select id from SPD_Perusahaan where SarMutPerusahaan='" + sarmutPrs + "')";
                sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        Line = Convert.ToInt16(sdr["line"].ToString());
                    }
                }
                for (int a = 1; a <= Line; a++)
                {
                    zl = new ZetroView();
                    zl.QueryType = Operation.CUSTOM;
                    zl.CustomQuery = " select isnull( cast(sum(TotalDefect)/sum(TotalSerah)*100 as decimal(18,2)),0) actual  from Temp_DefectSarMut" +
                                     " where gp='total_L" + a + "' and Periode = '" + ddlTahun.SelectedValue + ddlBulan.SelectedValue + "'";
                    sdr = zl.Retrieve();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            aktual = Convert.ToDecimal(sdr["actual"].ToString());

                        }
                    }
                    if (aktual <= 0)
                        aktual = 0;
                    zl1.QueryType = Operation.CUSTOM;
                    zl1.CustomQuery = " update SPD_Trans set Actual = " + aktual.ToString().Replace(",", ".") + " where Approval = 0 and tahun = '" + ddlTahun.SelectedValue + "' and Bulan = '" + ddlBulan.SelectedValue + "' and SarmutDeptID in " +
                                        " (select id from SPD_Departemen where SarmutPID in(select id from SPD_Perusahaan where SarMutPerusahaan = '" + sarmutPrs + "') " +
                                        " and SarmutDepartemen = 'Line " + a + "')";
                    sdr1 = zl1.Retrieve();
                }
            }
        }
        //end add hasan agustus 2022
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
        private void LoadBulan()
        {
            ddlBulan.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i).ToString(), i.ToString().PadLeft(2, '0')));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString().PadLeft(2, '0');
        }
        protected void txtKeterangan_TextChanged(object sender, EventArgs e)
        {
            Repeater lstDetail;
            Repeater lstPrs;
            foreach (RepeaterItem objItem0 in lstType.Items)
            {
                lstPrs = ((Repeater)(objItem0.FindControl("lstPrs")));
                foreach (RepeaterItem objItem in lstPrs.Items)
                {
                    lstDetail = ((Repeater)(objItem.FindControl("lstDetail")));
                    int i = 0;
                    foreach (RepeaterItem objDetail in lstDetail.Items)
                    {
                        Label lblTercapai = (Label)lstDetail.Items[i].FindControl("lblTercapai");
                        Label lblTarget = (Label)lstDetail.Items[i].FindControl("lblTarget");
                        TextBox txtActual = (TextBox)lstDetail.Items[i].FindControl("txtActual");
                        if (txtActual.ToolTip.Trim().ToUpper() == "MIN")
                        {

                            if (decimal.Parse(lblTarget.Text) > decimal.Parse(txtActual.Text))
                                lblTercapai.Text = "Tidak tercapai";
                            else
                                lblTercapai.Text = "Tercapai";
                        }
                        else if (txtActual.ToolTip.Trim().ToUpper() == "ORDER KEMBALI")
                        {
                            if (decimal.Parse(lblTarget.Text) > decimal.Parse(txtActual.Text))
                                lblTercapai.Text = "Tidak tercapai";
                            else
                                lblTercapai.Text = "Tercapai";
                        }
                        else
                        {
                            if (decimal.Parse(lblTarget.Text) >= decimal.Parse(txtActual.Text))
                                lblTercapai.Text = "Tercapai";
                            else
                                lblTercapai.Text = "Tidak Tercapai";
                        }
                        i++;
                    }
                }
            }
        }
        protected void txtKeterangan1_TextChanged(object sender, EventArgs e)
        {
            Repeater lstDetail;
            Repeater lstPrs;
            foreach (RepeaterItem objItem0 in lstType.Items)
            {
                lstPrs = ((Repeater)(objItem0.FindControl("lstPrs")));
                int i = 0;
                foreach (RepeaterItem objItem in lstPrs.Items)
                {
                    Label lblTercapai1 = (Label)lstPrs.Items[i].FindControl("lblTercapaiP");
                    Label lblTarget1 = (Label)lstPrs.Items[i].FindControl("lblTargetP");
                    TextBox txtActual1 = (TextBox)lstPrs.Items[i].FindControl("txtActualP");
                    TextBox txtTarget = (TextBox)lstPrs.Items[i].FindControl("txtTarget");
                    lstDetail = ((Repeater)(objItem.FindControl("lstDetail")));

                    int a = Convert.ToInt32(ddlTahun.SelectedValue + ddlBulan.SelectedValue);
                    if (txtActual1.ToolTip.Trim().ToUpper() == "RANGE" && a >= 202201)
                    {
                        string query = string.Empty;
                        ZetroView zl = new ZetroView();
                        zl.QueryType = Operation.CUSTOM;

                        zl.CustomQuery = "select query from spd_targetv where id in(select targetvid from spd_perusahaan where sarmutperusahaan='Effesiensi Pemakaian Kertas' )";
                        //" + ba.ID + "
                        SqlDataReader sdr = zl.Retrieve();
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                query = sdr["query"].ToString().Trim();

                            }
                        }
                        string tercapai = string.Empty;
                        string param = "declare @target decimal set @target=" + txtActual1.Text.ToString().Replace(",", ".");
                        ZetroView zl1 = new ZetroView();
                        zl1.QueryType = Operation.CUSTOM;
                        zl1.CustomQuery = param + " " + query;
                        SqlDataReader sdr2 = zl1.Retrieve();
                        if (sdr2.HasRows)
                        {
                            while (sdr2.Read())
                            {
                                tercapai = (sdr2["tercapai"].ToString());

                            }
                        }
                        lblTercapai1.Text = tercapai;

                    }
                    else if (txtActual1.ToolTip.Trim().ToUpper() == "RANGE" && a < 202201)
                    {

                        if (Convert.ToDecimal(txtActual1.Text) >= -2 && Convert.ToDecimal(txtActual1.Text) <= 2)
                        {
                            lblTercapai1.Text = "Tercapai";
                        }
                        else
                        {
                            lblTercapai1.Text = "Tidak Tercapai";
                        }
                    }
                    else if (txtActual1.ToolTip.Trim().ToUpper() == "MIN")
                    {
                        decimal n;
                        bool isNumeric = decimal.TryParse(txtTarget.Text, out n);
                        if (isNumeric == true)
                        {
                            if (decimal.Parse(txtTarget.Text) > decimal.Parse(txtActual1.Text))
                                lblTercapai1.Text = "Tidak tercapai";
                            else
                                lblTercapai1.Text = "Tercapai";
                        }
                    }
                    else if (txtActual1.ToolTip.Trim().ToUpper() == "ORDER KEMBALI")
                    {
                        decimal n;
                        bool isNumeric = decimal.TryParse(txtTarget.Text, out n);
                        if (isNumeric == true)
                        {
                            if (decimal.Parse(txtTarget.Text) > decimal.Parse(txtActual1.Text))
                                lblTercapai1.Text = "Tidak tercapai";
                            else
                                lblTercapai1.Text = "Tercapai";
                        }
                    }
                    else
                    {
                        decimal n;
                        bool isNumeric = decimal.TryParse(txtTarget.Text, out n);
                        if (isNumeric == true)
                        {
                            if (decimal.Parse(txtTarget.Text) >= decimal.Parse(txtActual1.Text))
                                lblTercapai1.Text = "Tercapai";
                            else
                                lblTercapai1.Text = "Tidak Tercapai";
                        }
                    }
                    i++;

                }
            }

        }
        protected void txtKeterangan2_TextChanged(object sender, EventArgs e)
        {
            Repeater lstDetail;
            Repeater lstDetail2;
            Repeater lstPrs;
            foreach (RepeaterItem objItem0 in lstType.Items)
            {
                lstPrs = ((Repeater)(objItem0.FindControl("lstPrs")));
                foreach (RepeaterItem objItem in lstPrs.Items)
                {
                    lstDetail = ((Repeater)(objItem.FindControl("lstDetail")));
                    int i = 0;
                    foreach (RepeaterItem objDetail in lstDetail.Items)
                    {
                        lstDetail2 = ((Repeater)(objDetail.FindControl("lstDetail2")));
                        int j = 0;
                        foreach (RepeaterItem objDetail2 in lstDetail2.Items)
                        {
                            Label lblTercapai2 = (Label)lstDetail2.Items[j].FindControl("lblTercapai2");
                            Label lblTarget2 = (Label)lstDetail2.Items[j].FindControl("lblTarget2");
                            TextBox txtActual2 = (TextBox)lstDetail2.Items[j].FindControl("txtActual2");
                            if (lblTarget2.Text.Trim() != string.Empty)
                            {
                                if (txtActual2.ToolTip.Trim().ToUpper() == "MIN")
                                {
                                    decimal n;
                                    bool isNumeric = decimal.TryParse(lblTarget2.Text, out n);
                                    if (isNumeric == true)
                                    {
                                        if (decimal.Parse(lblTarget2.Text) > decimal.Parse(txtActual2.Text))
                                            lblTercapai2.Text = "Tidak tercapai";
                                        else
                                            lblTercapai2.Text = "Tercapai";
                                    }
                                }
                                else
                                {
                                    decimal n;
                                    bool isNumeric = decimal.TryParse(lblTarget2.Text, out n);
                                    if (isNumeric == true)
                                    {
                                        if (decimal.Parse(lblTarget2.Text) >= decimal.Parse(txtActual2.Text))
                                            lblTercapai2.Text = "Tercapai";
                                        else
                                            lblTercapai2.Text = "Tidak Tercapai";
                                    }
                                }
                            }
                            j++;
                        }
                        i++;
                    }
                }
            }

        }

        private void LoadTahun()
        {
            ArrayList arrD = this.ListBATahun();
            ddlTahun.Items.Clear();
            foreach (BeritaAcara ba in arrD)
            {
                ddlTahun.Items.Add(new ListItem(ba.Tahun.ToString(), ba.Tahun.ToString()));
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            // update transaksi departemen
            Repeater lstDetail;
            Repeater lstPrs;
            Regex pattern = new Regex("[,]");
            Regex pattern1 = new Regex("[.]");
            foreach (RepeaterItem objItem0 in lstType.Items)
            {
                lstPrs = ((Repeater)(objItem0.FindControl("lstPrs")));
                int i = 0;
                foreach (RepeaterItem objItem in lstPrs.Items)
                {

                    Label lblTercapai = (Label)lstPrs.Items[i].FindControl("lblTercapaiP");
                    Label lblTarget = (Label)lstPrs.Items[i].FindControl("lblTargetP");
                    TextBox txtActual = (TextBox)lstPrs.Items[i].FindControl("txtActualP");
                    TextBox txtTarget = (TextBox)lstPrs.Items[i].FindControl("txtTarget");
                    //txtActual.Text = pattern1.Replace(decimal.Parse(txtActual.Text).ToString(), "");
                    //txtActual.Text = pattern1.Replace(decimal.Parse(txtActual.Text).ToString(), "");
                    //CultureInfo ci = CultureInfo.GetCultureInfo("NL-be");
                    //double d = Convert.ToDouble("1.234,45", ci);

                    //txtActual.Text = decimal.Parse(txtActual.Text,ci).ToString();
                    //txtActual.Text = txtActual.Text.Replace(",", ".");
                    int a = Convert.ToInt32(ddlTahun.SelectedValue + ddlBulan.SelectedValue);
                    string target = string.Empty;
                    if (txtActual.ToolTip.Trim().ToUpper() == "RANGE")
                    {
                        if (a >= 202201)
                        {
                            string query = string.Empty;
                            ZetroView zl11 = new ZetroView();
                            zl11.QueryType = Operation.CUSTOM;

                            zl11.CustomQuery = "select query from spd_targetv where id in(select targetvid from spd_perusahaan where id in(select sarmutpid from spd_transprs where id=" + lblTercapai.ToolTip + " ))";
                            //" + ba.ID + "
                            SqlDataReader sdr3 = zl11.Retrieve();
                            if (sdr3.HasRows)
                            {
                                while (sdr3.Read())
                                {
                                    query = sdr3["query"].ToString().Trim();

                                }
                            }
                            string tercapai = string.Empty;
                            string param = "declare @target decimal set @target=" + txtActual.Text.ToString().Replace(",", ".");
                            ZetroView zl1 = new ZetroView();
                            zl1.QueryType = Operation.CUSTOM;
                            zl1.CustomQuery = param + " " + query;
                            SqlDataReader sdr2 = zl1.Retrieve();
                            if (sdr2.HasRows)
                            {
                                while (sdr2.Read())
                                {
                                    tercapai = (sdr2["tercapai"].ToString());

                                }
                            }
                            lblTercapai.Text = tercapai;
                            target = "0";

                        }
                        else if (a < 202201)
                        {

                            if (Convert.ToDecimal(txtActual.Text) >= -2 && Convert.ToDecimal(txtActual.Text) <= 2)
                            {
                                lblTercapai.Text = "Tercapai";
                            }
                            else
                            {
                                lblTercapai.Text = "Tidak Tercapai";
                            }
                        }
                        if (lblTercapai.ToolTip == "6120")
                        {
                            int test = 0;
                        }
                        ZetroView z2 = new ZetroView();
                        z2.QueryType = Operation.CUSTOM;
                        z2.CustomQuery = "update SPD_TransPrs set actual =" + txtActual.Text.Replace(",", ".") +
                        //z2.CustomQuery = "update SPD_TransPrs set actual =" + pattern.Replace(decimal.Parse(txtActual.Text).ToString(), ".") +
                            ", Target2='" + (txtTarget.Text).ToString().Replace(",", ".") + "' where ID=" + lblTercapai.ToolTip;
                        //zl.CustomQuery = "exec spd_updateTransPrs " + pattern.Replace(decimal.Parse(txtActual.Text).ToString(), ".") + "," + lblTercapai.ToolTip;
                        SqlDataReader sdr11 = z2.Retrieve();
                    }
                    else
                    {
                        decimal n;
                        bool isNumeric = decimal.TryParse(txtTarget.Text, out n);
                        if (isNumeric == true)
                        {
                            if (decimal.Parse(txtTarget.Text) > decimal.Parse(txtActual.Text))
                            {
                                lblTercapai.Text = "Tidak tercapai";
                            }
                            else
                            {
                                lblTercapai.Text = "Tercapai";
                            }
                        }
                        if (lblTercapai.ToolTip == "6120")
                        {
                            int test = 0;
                        }

                        ZetroView zl = new ZetroView();
                        zl.QueryType = Operation.CUSTOM;
                        zl.CustomQuery = "update SPD_TransPrs set actual =" + txtActual.Text.Replace(",", ".") +
                        //zl.CustomQuery = "update SPD_TransPrs set actual =" + pattern.Replace(decimal.Parse(txtActual.Text).ToString(), ".") +
                        //", Target=" + pattern.Replace(decimal.Parse(txtTarget.Text).ToString(), ".") + " where ID=" + lblTercapai.ToolTip;
                        ", Target=" + decimal.Parse(txtTarget.Text).ToString().Replace(",", ".") + " where ID=" + lblTercapai.ToolTip;
                        //
                        //zl.CustomQuery = "exec spd_updateTransPrs " + pattern.Replace(decimal.Parse(txtActual.Text).ToString(), ".") + "," + lblTercapai.ToolTip;
                        SqlDataReader sdr = zl.Retrieve();

                    }
                    i++;
                }
            }

            foreach (RepeaterItem objItem0 in lstType.Items)
            {
                lstPrs = ((Repeater)(objItem0.FindControl("lstPrs")));
                foreach (RepeaterItem objItem in lstPrs.Items)
                {
                    lstDetail = ((Repeater)(objItem.FindControl("lstDetail")));
                    int i = 0;
                    foreach (RepeaterItem objDetail in lstDetail.Items)
                    {
                        Label lblTercapai = (Label)lstDetail.Items[i].FindControl("lblTercapai");
                        Label lblTarget = (Label)lstDetail.Items[i].FindControl("lblTarget");
                        TextBox txtActual = (TextBox)lstDetail.Items[i].FindControl("txtActual");
                        //txtActual.Text = pattern1.Replace(decimal.Parse(txtActual.Text).ToString(), "");

                        //txtActual.Text = decimal.Parse(txtActual.Text).ToString();
                        decimal n;
                        bool isNumeric = decimal.TryParse(lblTarget.Text, out n);
                        if (isNumeric == true)
                        {
                            if (decimal.Parse(lblTarget.Text) > decimal.Parse(txtActual.Text))
                                lblTercapai.Text = "Tidak tercapai";
                            else
                                lblTercapai.Text = "Tercapai";
                        }
                        ZetroView zl = new ZetroView();
                        if (lblTercapai.ToolTip == "6120")
                        {
                            int test = 0;
                        }
                        zl.QueryType = Operation.CUSTOM;
                        zl.CustomQuery = "update SPD_Trans set actual =" + txtActual.Text.Replace(",", ".") + " where ID=" + lblTercapai.ToolTip;
                        //zl.CustomQuery = "update SPD_Trans set actual =" + pattern.Replace(decimal.Parse(txtActual.Text).ToString(), ".") + " where ID=" + lblTercapai.ToolTip;
                        SqlDataReader sdr = zl.Retrieve();
                        i++;
                    }
                }
            }
            Repeater lstDetail2;
            foreach (RepeaterItem objItem0 in lstType.Items)
            {
                lstPrs = ((Repeater)(objItem0.FindControl("lstPrs")));
                foreach (RepeaterItem objItem in lstPrs.Items)
                {
                    lstDetail = ((Repeater)(objItem.FindControl("lstDetail")));
                    int i = 0;
                    foreach (RepeaterItem objDetail in lstDetail.Items)
                    {
                        lstDetail2 = ((Repeater)(objDetail.FindControl("lstDetail2")));
                        int j = 0;
                        foreach (RepeaterItem objDetail2 in lstDetail2.Items)
                        {
                            Label lblTercapai2 = (Label)lstDetail2.Items[j].FindControl("lblTercapai2");
                            Label lblTarget2 = (Label)lstDetail2.Items[j].FindControl("lblTarget2");
                            TextBox txtActual2 = (TextBox)lstDetail2.Items[j].FindControl("txtActual2");
                            if (txtActual2.Text.Trim() != string.Empty)
                            {
                                txtActual2.Text = decimal.Parse(txtActual2.Text).ToString();
                                //txtActual2.Text = pattern1.Replace(decimal.Parse(txtActual2.Text).ToString(), "");
                                decimal n;
                                bool isNumeric = decimal.TryParse(lblTarget2.Text, out n);
                                if (isNumeric == true)
                                {
                                    if (decimal.Parse(lblTarget2.Text) > decimal.Parse(txtActual2.Text))
                                        lblTercapai2.Text = "Tidak tercapai";
                                    else
                                        lblTercapai2.Text = "Tercapai";
                                    if (decimal.Parse(lblTarget2.Text) > decimal.Parse(txtActual2.Text))
                                        lblTercapai2.Text = "Tidak tercapai";
                                    else
                                        lblTercapai2.Text = "Tercapai";

                                    ZetroView zl = new ZetroView();
                                    zl.QueryType = Operation.CUSTOM;
                                    zl.CustomQuery = "update SPD_TransDet set actual =" + decimal.Parse(txtActual2.Text).ToString().Replace(",", ".") + " where ID=" + lblTercapai2.ToolTip;

                                    //zl.CustomQuery = "update SPD_TransDet set actual =" + pattern.Replace(decimal.Parse(txtActual2.Text).ToString(), ".") + " where ID=" + lblTercapai2.ToolTip;
                                    SqlDataReader sdr = zl.Retrieve();
                                }
                            }
                            j++;
                        }
                        i++;
                    }
                }
            }
            LoadTypeSarmut();
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            this.StateView = 2;
            criteria.Visible = true;
            Judul = "LIST";
            //LoadListSarmut(int.Parse(appLevele.Value));
        }
        private string GetAbsoluteUrl(string relativeUrl)
        {
            relativeUrl = relativeUrl.Replace("~/", string.Empty);
            string[] splits = Request.Url.AbsoluteUri.Split('/');
            if (splits.Length >= 2)
            {
                string url = splits[0] + "//";
                for (int i = 2; i < splits.Length - 1; i++)
                {
                    url += splits[i];
                    url += "/";
                }

                return url + relativeUrl;
            }
            return relativeUrl;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            lst2.Visible = true;

            int levelApv = getAllApv(ddlBulan.SelectedValue, ddlTahun.SelectedItem.Text);

            if (levelApv == 0)
                LoadSignNamePict("admin");
            if (levelApv == 1)
                LoadSignNamePict("head");
            if (levelApv == 2)
                LoadSignNamePict("mgr");
            //if (levelApv == 3)
            //    LoadSignNamePict("pm");

            /** Admin **/
            if (SgnAdmName != string.Empty)
                LblAdmin.Text = SgnAdmName;
            else
                LblAdmin.Text = string.Empty;
            if (PictAdmName != string.Empty)
                Image3.ImageUrl = this.GetAbsoluteUrl("~/images/" + PictAdmName);
            else
                Image3.ImageUrl = this.GetAbsoluteUrl("~/images/Empty.jpg");

            /** Head **/
            if (SgnMgrName != string.Empty)
                LblMgr.Text = SgnMgrName;
            //LblPM.Text = SgnMgrName;
            else
                LblMgr.Text = string.Empty;
            if (PictMgrName != string.Empty)
                Image2.ImageUrl = this.GetAbsoluteUrl("~/images/" + PictMgrName);
            //Image2.ImageUrl = this.GetAbsoluteUrl("~/images/" + PictPMName);
            else
                Image2.ImageUrl = this.GetAbsoluteUrl("~/images/Empty.jpg");

            /** Manager **/
            if (SgnPMName != string.Empty)
                LblPM.Text = SgnPMName;
            //LblMgr.Text = SgnPMName;
            else
                LblPM.Text = string.Empty;
            if (PictPMName != string.Empty)
                Image1.ImageUrl = this.GetAbsoluteUrl("~/images/" + PictPMName);
            //Image1.ImageUrl = this.GetAbsoluteUrl("~/images/" + PictMgrName);
            else
                Image1.ImageUrl = this.GetAbsoluteUrl("~/images/Empty.jpg");

            LblPlant.Text = getAlamat() + "," + DateTime.Now.ToString("dd MMM yyyy");
            Image1.Width = 47;
            Image1.Height = 27;
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            //FileInfo fi = new FileInfo(dirPath);
            //if (fi.Exists)
            //{
            Response.AddHeader("content-disposition", "attachment;filename=ListSarmut.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<b>LIST SARMUT DAN PEMANTAUAN</b>";
            Html += "<br>Periode : " + ddlBulan.SelectedItem.Text + "  " + ddlTahun.SelectedValue.ToString();
            Html += "<br>Departement :" + ddlDept.SelectedItem.Text;
            string HtmlEnd = "";
            // Table table = new Table();
            // TableRow row = new TableRow();
            // row.Cells.Add(new TableCell());
            // row.Cells.Add(new TableCell());
            // row.Cells.Add(new TableCell());
            // row.Cells.Add(new TableCell());
            //// row.Cells[0].Controls.Add(Image1);
            // row.Cells[1].Text = "1";
            // row.Cells[2].Text = "2";
            // row.Cells[3].Text = "3";
            // row.BorderStyle = BorderStyle.None;
            // row.Cells[0].BorderStyle = BorderStyle.None;
            // row.Cells[1].BorderStyle = BorderStyle.None;
            // row.Cells[2].BorderStyle = BorderStyle.None;
            // row.Cells[3].BorderStyle = BorderStyle.None;
            // table.Rows.Add(row);

            lst2.RenderControl(hw);
            //table.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0", "border=\"1");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
            lst2.Visible = false;
        }

        private int StateView { get; set; }
        protected void ddlBulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            Users user = ((Users)Session["Users"]);
            LoadTypeSarmut();
            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "LOGISTIK MATERIAL")
            {
                Update_Sarmut_Logistik_M();
                LoadTypeSarmut();
            }
            else if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "PURCHASING")
            {
                Update_sarmut_Purchn();
                LoadTypeSarmut();
            }
            else if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "BOARDMILL")
            {
                Update_sarmut_Pelarian();
                Update_sarmut_Scrab_Basah();
                Update_sarmut_Scrab_Kering();
                Update_sarmut_BendingStrength();
                Update_sarmut_DefectBM();
                #region add by Razib
                #region #1   Press <15 mm 
                //if (user.UnitKerjaID == 1 || user.UnitKerjaID == 13)
                //{
                //    Update_sarmut_k15_mm_PRESS_L1();
                //    Update_sarmut_k15_mm_PRESS_L2();
                //    Update_sarmut_k15_mm_PRESS_L3();
                //    Update_sarmut_k15_mm_PRESS_L4();
                //}
                //else if (user.UnitKerjaID == 7)
                //{
                //    Update_sarmut_k15_mm_PRESS_L1();
                //    Update_sarmut_k15_mm_PRESS_L2();
                //    Update_sarmut_k15_mm_PRESS_L3();
                //    Update_sarmut_k15_mm_PRESS_L4();
                //    Update_sarmut_k15_mm_PRESS_L5();
                //    Update_sarmut_k15_mm_PRESS_L6();
                //}

                //#endregion
                //#region #2  Press >=15 mm 
                //if (user.UnitKerjaID == 1 || user.UnitKerjaID == 13)
                //{
                //    Update_sarmut_Lsd15_mm_PRESS_L1();
                //    Update_sarmut_Lsd15_mm_PRESS_L2();
                //    Update_sarmut_Lsd15_mm_PRESS_L3();
                //    Update_sarmut_Lsd15_mm_PRESS_L4();
                //}
                //else if (user.UnitKerjaID == 7)
                //{
                //    Update_sarmut_Lsd15_mm_PRESS_L1();
                //    Update_sarmut_Lsd15_mm_PRESS_L2();
                //    Update_sarmut_Lsd15_mm_PRESS_L3();
                //    Update_sarmut_Lsd15_mm_PRESS_L4();
                //    Update_sarmut_Lsd15_mm_PRESS_L5();
                //    Update_sarmut_Lsd15_mm_PRESS_L6();
                //}

                //#endregion
                //#region #3  Non Press
                //if (user.UnitKerjaID == 1 || user.UnitKerjaID == 13)
                //{
                //    Update_sarmut_Non_PressL1();
                //    Update_sarmut_Non_PressL2();
                //    Update_sarmut_Non_PressL3();
                //    Update_sarmut_Non_PressL4();
                //}
                //else if (user.UnitKerjaID == 7)
                //{
                //    Update_sarmut_Non_PressL1();
                //    Update_sarmut_Non_PressL2();
                //    Update_sarmut_Non_PressL3();
                //    Update_sarmut_Non_PressL4();
                //    Update_sarmut_Non_PressL5();
                //    Update_sarmut_Non_PressL6();
                //}
                //#endregion
                //#region #4 Akumulasi
                //if (user.UnitKerjaID == 1 || user.UnitKerjaID == 13)
                //{
                //    Update_sarmut_AkumulasiL1();
                //    Update_sarmut_AkumulasiL2();
                //    Update_sarmut_AkumulasiL3();
                //    Update_sarmut_AkumulasiL4();
                //}
                //else if (user.UnitKerjaID == 7)
                //{
                //    Update_sarmut_AkumulasiL1();
                //    Update_sarmut_AkumulasiL2();
                //    Update_sarmut_AkumulasiL3();
                //    Update_sarmut_AkumulasiL4();
                //    Update_sarmut_AkumulasiL5();
                //    Update_sarmut_AkumulasiL6();
                //}
                #endregion
                #endregion
                LoadTypeSarmut();
            }
            else if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "MARKETING")
            {
                Update_customer_non_mutu();
                Update_sarmut_OnTimeDelivery();
                LoadTypeSarmut();
            }
            else if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "LOGISTIK FINISHGOOD")
            {
                Update_pemantauan_hasil_packing();
                LoadTypeSarmut();
            }
        }

        private void LoadDept()
        {
            Users users = (Users)Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select * from SPD_Dept where RowStatus>-1 and dptID in (" + users.DeptID + ") order by dept";
            SqlDataReader sdr = zl.Retrieve();
            ddlDept.Items.Clear();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlDept.Items.Add(new ListItem(sdr["Dept"].ToString(), sdr["ID"].ToString()));
                }
            }
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
            zl.CustomQuery = "select * from SPD_Dept where RowStatus>-1 and dptID in (" + users.DeptID + ")order by dept";
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
        private string getAlamat()
        {
            string Alamat = string.Empty;
            Users users = (Users)Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select lokasi from company where depoid=" + users.UnitKerjaID;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    Alamat = sdr["lokasi"].ToString();
                }
            }
            return Alamat;
        }
        private void LoadTypeSarmut()
        {
            ArrayList arrData = new ArrayList();
            newdata = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select * from spd_Type order by ID";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new SPD_Type
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        JenisSarmut = sdr["JenisSarmut"].ToString()
                    });
                }
            }
            lstType.DataSource = arrData;
            lstType.DataBind();
            if (btnExport.Visible == true)
                LoadTypexSarmut();
            LoadSign();
        }
        private void LoadSign()
        {
            string Nama2 = @"\" + "";
            //string dirPath = @"D:\DATA LAMPIRAN PURCHN\sarmut\TandaTangan\PM.jpg";
            string dirPath = "~/Images/PM.jpg";
            //FileInfo fi = new FileInfo(dirPath);
            //if (fi.Exists)
            //{
            //ImgPM.ImageUrl = Page.ResolveUrl(dirPath);// +fi.Name; // You just need to pass name
            //YOu need to make path like ~/ImageFiles/maui4.jpg
            //}
        }
        private void LoadTypexSarmut()
        {
            ArrayList arrData = new ArrayList();
            newdata = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select * from spd_Type order by ID";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new SPD_Type
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        JenisSarmut = sdr["JenisSarmut"].ToString()
                    });
                }
            }
            lstTypex.DataSource = arrData;
            lstTypex.DataBind();
        }
        protected void lstType_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Users users = (Users)Session["Users"];
            SPD_Type ba = (SPD_Type)e.Item.DataItem;
            Repeater rps = (Repeater)e.Item.FindControl("lstPrs");
            HtmlGenericControl ps = (HtmlGenericControl)e.Item.FindControl("ps");
            LoadListSarmutP(ba.ID, rps);
        }

        protected void lstTypex_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Users users = (Users)Session["Users"];
            SPD_Type ba = (SPD_Type)e.Item.DataItem;
            Repeater rps = (Repeater)e.Item.FindControl("lstPrsx");
            HtmlGenericControl ps = (HtmlGenericControl)e.Item.FindControl("ps");
            LoadListSarmutPx(ba.ID, rps);
        }
        public int newdata = 0;

        private void LoadListSarmutP(int ID, Repeater rps)
        {
            Users users = (Users)Session["Users"];
            int Ada = Convert.ToInt32(Session["Ada"]);
            if (Ada == 1 || users.Apv == 0)
            { btnSimpan.Visible = true; btnApprove.Visible = false; btnUnApprove.Visible = false; btnExport.Visible = true; }

            ArrayList arrData = new ArrayList();
            newdata = 0;
            string sqlApv = string.Empty;
            //if (users.Apv == 2)
            //    sqlApv = " and ST.Approval=0 ";
            //if (users.Apv == 3)
            //    sqlApv = " and ST.Approval=2 ";

            if (users.Apv == 1)
                sqlApv = " and ST.Approval=0 ";
            if (users.Apv == 2 && Ada == 0)
                sqlApv = " and ST.Approval=1 ";
            if (users.Apv == 2 && Ada == 1)
                sqlApv = " and ST.Approval=0 ";

            if (PanelApv.Visible == true)
            {
                if (RbList.Checked == true)
                    sqlApv = string.Empty;
            }
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "select * from (select isnull(B.OnSystem,0)OnSystem,case isnull(ST.Approval,0) when 0 then 'Admin' when 2 then 'Apv Manager' " +
                "when 1 then 'Apv Head' end StatusApv,B.ID sdeptid,isnull(ST.ID,0) ID ,B.TypeSarmutID TypeID, B.sarmutperusahaan  [Description],S.Satuan, " +

                //"case when (select isnull([Target],0) from spd_transprs where sarmutpid=B.id and Tahun=" + ddlTahun.SelectedValue + " and bulan=" + 
                //Convert.ToInt32(ddlBulan.SelectedValue) + ")=0 then isnull(T.[TargetV1],0) " +
                //"else isnull((select isnull([Target],0) from spd_transprs where sarmutpid=B.id and Tahun=" + ddlTahun.SelectedValue + " and bulan=" +
                //Convert.ToInt32(ddlBulan.SelectedValue) + "),0)end [target],


                //Edit agus tgl 12-04-2022

                "case when (select isnull([Target],0) from spd_transprs where sarmutpid=B.id and Tahun=" + ddlTahun.SelectedValue + " and bulan=" +
                Convert.ToInt32(ddlBulan.SelectedValue) + ")=0 and b.SarMutPerusahaan='Akumulasi line berjalan' then " +
                "(select 40 * count(distinct bp.PlantID) " +
                "from BS_Production_Testing_Report bt " +
                "left join BS_Rountine_Test br on bt.tgl_prod = br.tgl_prod and bt.formula = br.formula and br.id_prod = bt.ID " +
                "left join BM_PlantGroup bp on bp.[Group]=bt.group_produksi where left(convert(char,bt.tgl_prod,112),6)=" + ddlTahun.SelectedValue + "" + ddlBulan.SelectedValue + " and bp.PlantID in(1,2,3,4,5,6))" +

                "when (select isnull([Target],0) from spd_transprs where sarmutpid=B.id and Tahun=" + ddlTahun.SelectedValue + " and bulan=" +
                Convert.ToInt32(ddlBulan.SelectedValue) + ")=0 then " +
                "isnull(T.[TargetV1],0) " +
                "else isnull((select isnull([Target],0) from spd_transprs where sarmutpid=B.id and Tahun=" + ddlTahun.SelectedValue + " and bulan=" +
                Convert.ToInt32(ddlBulan.SelectedValue) + "),0)end [target], " +

                //Edit agus tgl 12-04-2022

                "pr.Param,isnull(ST.Actual,0)Actual , " +
                " case when (select isnull([Target2],'a') from SPD_TransPrs where sarmutpid=B.id and Tahun=" + ddlTahun.SelectedValue + " and bulan= " +
                Convert.ToInt32(ddlBulan.SelectedValue) + ")='a' then	t.[Range] " +
                " else( select target2 from SPD_TransPrs where sarmutpid=B.id and Tahun=" + ddlTahun.SelectedValue + " and bulan=" +
                Convert.ToInt32(ddlBulan.SelectedValue) + ") end target2, " +
                "isnull(ST.Approval,0) Approval,B.Urutan,isnull(ST.Tahun," + ddlTahun.SelectedValue + ")Tahun,isnull(ST.Bulan," +
                Convert.ToInt32(ddlBulan.SelectedValue) +
                ")bulan,isnull(ST.checked,0)checked,isnull(B.EditTarget,0)EditTarget from  SPD_perusahaan B left join SPD_Satuan S on B.satuanID=S.id  " +
                "left join SPD_TargetV T on B.targetVID=T.id left join spd_parameter pr on B.paramid =PR.ID left join (select * from spd_transprs where Tahun=" +
                ddlTahun.SelectedValue + " and bulan=" + Convert.ToInt32(ddlBulan.SelectedValue) + "  )  st on B.ID=ST.sarmutPID " +
                "where B.RowStatus>-1 and   B.typesarmutid=" + ID + " and depoid=" + users.UnitKerjaID + " and B.deptid=" +
                ddlDept.SelectedValue + sqlApv + ")A where Tahun=" + ddlTahun.SelectedValue + " and bulan=" + Convert.ToInt32(ddlBulan.SelectedValue) + " order by Urutan  ";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new SPD_Prs
                    {
                        OnSystem = Convert.ToInt32(sdr["OnSystem"].ToString()),
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        Description = sdr["Description"].ToString(),
                        Satuan = sdr["Satuan"].ToString(),
                        Param = sdr["Param"].ToString(),
                        Target = Convert.ToDecimal(sdr["Target"].ToString()),
                        Actual = Convert.ToDecimal(sdr["Actual"].ToString()),
                        Target2 = sdr["Target2"].ToString(),
                        Approval = Convert.ToInt32(sdr["Approval"].ToString()),
                        Urutan = Convert.ToInt32(sdr["Urutan"].ToString()),
                        TypeID = Convert.ToInt32(sdr["TypeID"].ToString()),
                        SDeptID = Convert.ToInt32(sdr["sdeptID"].ToString()),
                        StatusApv = sdr["StatusApv"].ToString(),
                        EditTarget = Convert.ToInt32(sdr["EditTarget"].ToString()),
                        Checked = Convert.ToInt32(sdr["Checked"].ToString())
                    });
                }
            }
            rps.DataSource = arrData;
            rps.DataBind();
        }
        private void LoadListSarmutPx(int ID, Repeater rps)
        {
            Users users = (Users)Session["Users"];
            ArrayList arrData = new ArrayList();
            newdata = 0;
            string sqlApv = string.Empty;
            //if (users.Apv == 2)
            //    sqlApv = " and ST.Approval=0 ";
            //if (users.Apv == 3)
            //    sqlApv = " and ST.Approval=2 ";

            if (users.Apv == 1)
                sqlApv = " and ST.Approval=0 ";
            if (users.Apv == 2)
                sqlApv = " and ST.Approval=1 ";

            if (PanelApv.Visible == true)
            {
                if (RbList.Checked == true)
                    sqlApv = string.Empty;
            }
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select * from (select case isnull(ST.Approval,0) when 0 then 'Admin' when 2 then 'Apv Manager' when 1 then 'Apv Head' end StatusApv,B.ID sdeptid,isnull(ST.ID,0) ID ,B.TypeSarmutID TypeID, B.sarmutperusahaan  [Description],S.Satuan, " +
                "case when (select isnull([Target],0) from spd_transprs where sarmutpid=B.id and Tahun=" + ddlTahun.SelectedValue + " and bulan=" +
                Convert.ToInt32(ddlBulan.SelectedValue) + ")=0 then isnull(T.[TargetV1],0) " +
                "else (select isnull([Target],0) from spd_transprs where sarmutpid=B.id and Tahun=" + ddlTahun.SelectedValue + " and bulan=" +
                Convert.ToInt32(ddlBulan.SelectedValue) + ")end [target],pr.Param,isnull(ST.Actual,0)Actual ,t.Range target2, " +
                "isnull(ST.Approval,0) Approval ,B.Urutan,isnull(ST.Tahun," + ddlTahun.SelectedValue + ")Tahun,isnull(ST.Bulan," + Convert.ToInt32(ddlBulan.SelectedValue) +
                ")bulan,isnull(ST.checked,0)checked from  SPD_perusahaan B left join SPD_Satuan S on B.satuanID=S.id  " +
                "left join SPD_TargetV T on B.targetVID=T.id left join spd_parameter pr on B.paramid =PR.ID left join (select * from spd_transprs where Tahun=" +
                ddlTahun.SelectedValue + " and bulan=" + Convert.ToInt32(ddlBulan.SelectedValue) + "  )  st on B.ID=ST.sarmutPID " +
                "where B.RowStatus>-1 and   B.typesarmutid=" + ID + " and depoid=" + users.UnitKerjaID + " and B.deptid=" +
                ddlDept.SelectedValue + sqlApv + ")A where Tahun=" + ddlTahun.SelectedValue + " and bulan=" + Convert.ToInt32(ddlBulan.SelectedValue) + " order by Urutan  ";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new SPD_Prs
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        Description = sdr["Description"].ToString(),
                        Satuan = sdr["Satuan"].ToString(),
                        Param = sdr["Param"].ToString(),
                        Target = Convert.ToDecimal(sdr["Target"].ToString()),
                        Target2 = sdr["Target2"].ToString(),
                        Actual = Convert.ToDecimal(sdr["Actual"].ToString()),
                        Approval = Convert.ToInt32(sdr["Approval"].ToString()),
                        Urutan = Convert.ToInt32(sdr["Urutan"].ToString()),
                        TypeID = Convert.ToInt32(sdr["TypeID"].ToString()),
                        SDeptID = Convert.ToInt32(sdr["sdeptID"].ToString()),
                        StatusApv = sdr["StatusApv"].ToString(),
                        Checked = Convert.ToInt32(sdr["Checked"].ToString())
                    });
                }
            }
            rps.DataSource = arrData;
            rps.DataBind();
        }

        private void LoadListSarmut(int typeID, int prsID, Repeater lst)
        {
            Users users = (Users)Session["Users"];
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            string sqlApv = string.Empty;
            //if (users.Apv == 2)
            //    sqlApv = " and ST.Approval=0 ";
            //if (users.Apv == 3)
            //    sqlApv = " and ST.Approval=2 ";

            if (users.Apv == 1)
                sqlApv = " and ST.Approval=0 ";
            if (users.Apv == 2)
                sqlApv = " and ST.Approval=1 ";

            if (PanelApv.Visible == true)
            {
                if (RbList.Checked == true)
                    sqlApv = string.Empty;
            }
            zl.CustomQuery = "select * from (select isnull(A.onsystem,0)onsystem, case isnull(ST.Approval,0) when 0 then 'Admin' when 2 then 'Apv Manager' when 1 then 'Apv Head' end StatusApv,A.ID sdeptID, isnull(ST.ID,0)ID , A.SarmutDepartemen [Description],S.Satuan, isnull(T.TargetV1,0) [Target],pr.Param," +
                "isnull(ST.Actual,0)Actual,B.Urutan,isnull(ST.Approval,0)Approval ,isnull(ST.Tahun," + ddlTahun.SelectedValue +
                ")Tahun,isnull(ST.Bulan," + Convert.ToInt32(ddlBulan.SelectedValue) + ")bulan,isnull(ST.checked,0)checked  from SPD_Departemen A Right join SPD_perusahaan B on A.sarmutpid=B.ID " +
                "left join spd_parameter pr on A.paramID=pr.id left join SPD_TargetV T on A.TargetVID=T.ID left join SPD_Satuan S on A.satuanid=S.id " +
                "left join (select * from SPD_Trans where tahun=" + ddlTahun.SelectedValue + " and bulan=" + ddlBulan.SelectedValue + " ) ST on A.ID=ST.SarmutDeptID and ST.RowStatus>-1 where B.typesarmutid=" + typeID + " and depoid= " + users.UnitKerjaID +
                "and B.deptid= " + ddlDept.SelectedValue + " and B.ID=" + prsID + " and drperiode<=" + ddlTahun.SelectedValue + ddlBulan.SelectedValue +
                " and sdperiode>=" + ddlTahun.SelectedValue + ddlBulan.SelectedValue + sqlApv + ")A where Tahun=" + ddlTahun.SelectedValue +
                " and bulan=" + Convert.ToInt32(ddlBulan.SelectedValue) + " order by Urutan,[Description]";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new SPD_Dept
                    {
                        OnSystem = Convert.ToInt32(sdr["OnSystem"].ToString()),
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        Description = sdr["Description"].ToString(),
                        Satuan = sdr["Satuan"].ToString(),
                        Param = sdr["Param"].ToString(),
                        Target = Convert.ToDecimal(sdr["Target"].ToString()),
                        Actual = Convert.ToDecimal(sdr["Actual"].ToString()),
                        Approval = Convert.ToInt32(sdr["Approval"].ToString()),
                        SDeptID = Convert.ToInt32(sdr["SDeptID"].ToString()),
                        Urutan = Convert.ToInt32(sdr["Urutan"].ToString()),
                        StatusApv = sdr["StatusApv"].ToString(),
                        Checked = Convert.ToInt32(sdr["Checked"].ToString())
                    });
                }
            }
            lst.DataSource = arrData;
            lst.DataBind();
        }

        private void LoadListSarmutx(int typeID, int prsID, Repeater lst)
        {
            Users users = (Users)Session["Users"];
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            string sqlApv = string.Empty;
            //if (users.Apv == 2)
            //    sqlApv = " and ST.Approval=0 ";
            //if (users.Apv == 3)
            //    sqlApv = " and ST.Approval=2 ";

            if (users.Apv == 1)
                sqlApv = " and ST.Approval=0 ";
            if (users.Apv == 2)
                sqlApv = " and ST.Approval=1 ";

            if (PanelApv.Visible == true)
            {
                if (RbList.Checked == true)
                    sqlApv = string.Empty;
            }
            zl.CustomQuery = "select * from (select case isnull(ST.Approval,0) when 0 then 'Admin' when 2 then 'Apv Manager' when 1 then 'Apv Head' end StatusApv,A.ID sdeptID, isnull(ST.ID,0)ID , A.SarmutDepartemen [Description],S.Satuan, isnull(T.TargetV1,0) [Target],pr.Param," +
                "isnull(ST.Actual,0)Actual,B.Urutan,isnull(ST.Approval,0)Approval ,isnull(ST.Tahun," + ddlTahun.SelectedValue +
                ")Tahun,isnull(ST.Bulan," + Convert.ToInt32(ddlBulan.SelectedValue) + ")bulan,isnull(ST.checked,0)checked  from SPD_Departemen A Right join SPD_perusahaan B on A.sarmutpid=B.ID " +
                "left join spd_parameter pr on A.paramID=pr.id left join SPD_TargetV T on A.TargetVID=T.ID left join SPD_Satuan S on A.satuanid=S.id " +
                "left join (select * from SPD_Trans where tahun=" + ddlTahun.SelectedValue + " and bulan=" + ddlBulan.SelectedValue + " ) ST on A.ID=ST.SarmutDeptID and ST.RowStatus>-1 where B.typesarmutid=" + typeID + " and depoid= " + users.UnitKerjaID +
                "and B.deptid= " + ddlDept.SelectedValue + " and B.ID=" + prsID + " and drperiode<=" + ddlTahun.SelectedValue + ddlBulan.SelectedValue +
                " and sdperiode>=" + ddlTahun.SelectedValue + ddlBulan.SelectedValue + sqlApv + ")A where Tahun=" + ddlTahun.SelectedValue +
                " and bulan=" + Convert.ToInt32(ddlBulan.SelectedValue) + " order by Urutan,[Description]";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new SPD_Dept
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        Description = sdr["Description"].ToString(),
                        Satuan = sdr["Satuan"].ToString(),
                        Param = sdr["Param"].ToString(),
                        Target = Convert.ToDecimal(sdr["Target"].ToString()),

                        Actual = Convert.ToDecimal(sdr["Actual"].ToString()),
                        Approval = Convert.ToInt32(sdr["Approval"].ToString()),
                        SDeptID = Convert.ToInt32(sdr["SDeptID"].ToString()),
                        Urutan = Convert.ToInt32(sdr["Urutan"].ToString()),
                        StatusApv = sdr["StatusApv"].ToString(),
                        Checked = Convert.ToInt32(sdr["Checked"].ToString())
                    });
                }
            }
            lst.DataSource = arrData;
            lst.DataBind();
        }

        private void LoadListSarmutDetail(int prsID, Repeater lst)
        {
            Users users = (Users)Session["Users"];
            ArrayList arrData = new ArrayList();
            newdata = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "select * from (select case isnull(ST.Approval,0) when 0 then 'Admin' when 2 then 'Apv Manager' when 1 then 'Apv Head' end StatusApv, A.ID sdeptID, isnull(ST.ID,0)ID , A.SubDepartemen [Description],isnull(S.Satuan,'')Satuan, isnull(T.TargetV1,0) [Target], " +
                "pr.Param,isnull(ST.Actual,0)Actual,A.Urutan,isnull(ST.Approval,0)Approval,isnull(ST.Tahun," + ddlTahun.SelectedValue +
                ")Tahun,isnull(ST.Bulan," + Convert.ToInt32(ddlBulan.SelectedValue) + ")bulan from " +
                "SPD_Departemendet A inner join SPD_Departemen B on A.sarmutdepId=B.ID  left join SPD_TargetV T on A.TargetVID=T.ID " +
                "left join SPD_Satuan S on A.satuanid=S.id left join (select * from SPD_TransDet where tahun=" + ddlTahun.SelectedValue + " and bulan=" + Convert.ToInt32(ddlBulan.SelectedValue) + ") ST on A.ID=ST.SarmutDeptID and ST.RowStatus>-1 " +
                "left join spd_parameter pr on A.paramid=pr.ID " +
                "where A.sarmutdepID=" + prsID + " and A.drperiode<=" + ddlTahun.SelectedValue + ddlBulan.SelectedValue +
                " and A.sdperiode>=" + ddlTahun.SelectedValue + ddlBulan.SelectedValue + ")A where Tahun=" + ddlTahun.SelectedValue +
                " and bulan=" + Convert.ToInt32(ddlBulan.SelectedValue) + " order by urutan,[Description]";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new SPD_DeptDet
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        Description = sdr["Description"].ToString(),
                        Satuan = sdr["Satuan"].ToString(),
                        Param = sdr["Param"].ToString(),
                        Target = Convert.ToDecimal(sdr["Target"].ToString()),
                        Actual = Convert.ToDecimal(sdr["Actual"].ToString()),
                        Approval = Convert.ToInt32(sdr["Approval"].ToString()),
                        SDeptID = Convert.ToInt32(sdr["SDeptID"].ToString()),
                        StatusApv = sdr["StatusApv"].ToString(),
                        Urutan = Convert.ToInt32(sdr["Urutan"].ToString())
                    });
                }
            }

            lst.DataSource = arrData;
            lst.DataBind();
        }

        private void LoadListSarmutDetailx(int prsID, Repeater lst)
        {
            Users users = (Users)Session["Users"];
            ArrayList arrData = new ArrayList();
            newdata = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "select * from (select case isnull(ST.Approval,0) when 0 then 'Admin' when 2 then 'Apv Manager' when 1 then 'Apv Head' end StatusApv, A.ID sdeptID, isnull(ST.ID,0)ID , A.SubDepartemen [Description],isnull(S.Satuan,'')Satuan, isnull(T.TargetV1,0) [Target], " +
                "pr.Param,isnull(ST.Actual,0)Actual,A.Urutan,isnull(ST.Approval,0)Approval,isnull(ST.Tahun," + ddlTahun.SelectedValue +
                ")Tahun,isnull(ST.Bulan," + Convert.ToInt32(ddlBulan.SelectedValue) + ")bulan from " +
                "SPD_Departemendet A inner join SPD_Departemen B on A.sarmutdepId=B.ID  left join SPD_TargetV T on A.TargetVID=T.ID " +
                "left join SPD_Satuan S on A.satuanid=S.id left join (select * from SPD_TransDet where tahun=" + ddlTahun.SelectedValue + " and bulan=" + Convert.ToInt32(ddlBulan.SelectedValue) + ") ST on A.ID=ST.SarmutDeptID and ST.RowStatus>-1 " +
                "left join spd_parameter pr on A.paramid=pr.ID " +
                "where A.sarmutdepID=" + prsID + " and A.drperiode<=" + ddlTahun.SelectedValue + ddlBulan.SelectedValue +
                " and A.sdperiode>=" + ddlTahun.SelectedValue + ddlBulan.SelectedValue + ")A where Tahun=" + ddlTahun.SelectedValue +
                " and bulan=" + Convert.ToInt32(ddlBulan.SelectedValue) + " order by urutan,[Description]";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new SPD_DeptDet
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        Description = sdr["Description"].ToString(),
                        Satuan = sdr["Satuan"].ToString(),
                        Param = sdr["Param"].ToString(),
                        Target = Convert.ToDecimal(sdr["Target"].ToString()),
                        Actual = Convert.ToDecimal(sdr["Actual"].ToString()),
                        Approval = Convert.ToInt32(sdr["Approval"].ToString()),
                        SDeptID = Convert.ToInt32(sdr["SDeptID"].ToString()),
                        StatusApv = sdr["StatusApv"].ToString(),
                        Urutan = Convert.ToInt32(sdr["Urutan"].ToString())
                    });
                }
            }

            lst.DataSource = arrData;
            lst.DataBind();
        }

        protected void lstPrs_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Users users = (Users)Session["Users"];
            SPD_Prs ba = (SPD_Prs)e.Item.DataItem;
            Label lblTercapai = (Label)e.Item.FindControl("lblTercapaiP");
            Label desc = (Label)e.Item.FindControl("lbldesc");
            CheckBox chk = (CheckBox)e.Item.FindControl("chkprs");
            TextBox txtActual = (TextBox)e.Item.FindControl("txtActualP");
            TextBox txtTarget = (TextBox)e.Item.FindControl("txtTarget");
            //CheckBox chkall = (CheckBox)e.Item.FindControl("chkAll");
            int a = Convert.ToInt32(ddlTahun.SelectedValue + ddlBulan.SelectedValue);
            string str = ba.Param;
            desc.Text = ba.Description.ToString();
            str = str.Replace(" ", "");
            if (str == "Range" && a >= 202201)
            {
                txtTarget.Text = ba.Target2;
            }
            else if (str == "Range" && a < 202201)
            {
                txtTarget.Text = "-2 s/d 2";
            }

            if (ba.Description == "Penurunan Reject Produk " && a >= 202204)
            {
                desc.Text = "Reject Produk";
            }
            //else if (ba.Description == "Penurunan Reject Produk " && a < 202205)
            //{
            //    desc.Text = "Penurunan Reject Produk";
            //}
            if (ba.Approval > 1)
                txtActual.ReadOnly = true;
            else
                txtActual.ReadOnly = false;

            if (ba.ID == 0)
            {
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "insert SPD_TransPrs(Tahun, Bulan, SarmutPID, Actual, RowStatus, CreatedBy, CreatedTime, " +
                    "LastModifiedBy, LastModifiedTime, Approval)values(" + ddlTahun.SelectedValue + "," + ddlBulan.SelectedValue + "," +
                    ba.SDeptID + ",0,0,'" + users.UserName + "',getdate(),'" + users.UserName + "',getdate(),0)";
                SqlDataReader sdr = zl.Retrieve();
                newdata = 1;
            }
            if (ba.EditTarget > 1)
                txtTarget.ReadOnly = false;
            else
                txtTarget.ReadOnly = true;

            int Ada = Convert.ToInt32(Session["Ada"]);
            if (Ada == 1 || users.Apv == 0)
            { chk.Visible = false; chkAll.Visible = false; }
            else
            {
                chk.Visible = (users.Apv > 0) ? true : false;
                chk.Checked = (ba.Checked > 0) ? true : false;
            }

            Image att = (Image)e.Item.FindControl("attPrs");
            att.Visible = (users.Apv > 0) ? false : true;
            att.Attributes.Add("onclick", "OpenDialog2('" + ba.ID.ToString() + "&tp=1')");

            lblTercapai.ToolTip = ba.ID.ToString();

            txtActual.ToolTip = ba.Param;
            if (ba.OnSystem == 1)
            {
                txtActual.ReadOnly = true;
                txtActual.ToolTip = "Actual value on System";
            }
            if (ba.Param.Trim().ToUpper() == "RANGE" && a >= 202201)
            {
                string query = string.Empty;
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "select query from spd_targetv where id in(select targetvid from spd_perusahaan where id in(select sarmutpid " +
                                " from spd_transprs where id=" + ba.ID + "))";
                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        query = sdr["query"].ToString().Trim();

                    }
                }
                string tercapai = string.Empty;
                string param = "declare @target decimal set @target=" + ba.Actual.ToString().Replace(",", ".");
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery = param + " " + query;
                SqlDataReader sdr2 = zl1.Retrieve();
                if (sdr2.HasRows)
                {
                    while (sdr2.Read())
                    {
                        tercapai = (sdr2["tercapai"].ToString());

                    }
                }
                lblTercapai.Text = tercapai;

            }
            else if (ba.Param.Trim().ToUpper() == "RANGE" && a < 202201)
            {
                if (ba.Actual >= -2 && ba.Actual <= 2)
                {
                    lblTercapai.Text = "Tercapai";
                }
                else
                {
                    lblTercapai.Text = "Tidak Tercapai";
                }
            }
            else if (ba.Param.Trim().ToUpper() == "MIN")
            {
                if (ba.Target > ba.Actual)
                    lblTercapai.Text = "Tidak Tercapai";
                else
                    lblTercapai.Text = "Tercapai";
            }
            else if (ba.Param.Trim().ToUpper() == "ORDER KEMBALI")
            {
                if (ba.Target > ba.Actual)
                    lblTercapai.Text = "Tidak Tercapai";
                else
                    lblTercapai.Text = "Tercapai";
            }
            else
            {
                if (ba.Target >= ba.Actual)
                    lblTercapai.Text = "Tercapai";
                else
                    lblTercapai.Text = "Tidak Tercapai";
            }


            Repeater rpts = (Repeater)e.Item.FindControl("attachPrs");
            ImageButton imgs = (ImageButton)e.Item.FindControl("attPrs");
            LoadListAttachmentPrs(imgs.CssClass.ToString(), rpts);

            Repeater rps = (Repeater)e.Item.FindControl("lstDetail");
            HtmlGenericControl ps = (HtmlGenericControl)e.Item.FindControl("ps");
            LoadListSarmut(ba.TypeID, ba.SDeptID, rps);
        }
        protected void lstPrsx_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Users users = (Users)Session["Users"];
            SPD_Prs ba = (SPD_Prs)e.Item.DataItem;

            if (ba.ID == 0)
            {
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "insert SPD_TransPrs(Tahun, Bulan, SarmutPID, Actual, RowStatus, CreatedBy, CreatedTime, " +
                    "LastModifiedBy, LastModifiedTime, Approval)values(" + ddlTahun.SelectedValue + "," + ddlBulan.SelectedValue + "," +
                    ba.SDeptID + ",0,0,'" + users.UserName + "',getdate(),'" + users.UserName + "',getdate(),0)";
                SqlDataReader sdr = zl.Retrieve();
            }
            Label lblTercapai = (Label)e.Item.FindControl("lblTercapaiP");
            if (ba.Param.Trim().ToUpper() == "MIN")
            {
                if (ba.Target > ba.Actual)
                    lblTercapai.Text = "Tidak Tercapai";
                else
                    lblTercapai.Text = "Tercapai";
            }
            else if (ba.Param.Trim().ToUpper() == "ORDER KEMBALI")
            {
                if (ba.Target > ba.Actual)
                    lblTercapai.Text = "Tidak Tercapai";
                else
                    lblTercapai.Text = "Tercapai";
            }
            else
            {
                if (ba.Target >= ba.Actual)
                    lblTercapai.Text = "Tercapai";
                else
                    lblTercapai.Text = "Tidak Tercapai";
            }


            Repeater rps = (Repeater)e.Item.FindControl("lstDetailx");
            HtmlGenericControl ps = (HtmlGenericControl)e.Item.FindControl("ps");
            LoadListSarmutx(ba.TypeID, ba.SDeptID, rps);
        }

        private void LoadListAttachment(string BAID, Repeater lst)
        {
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            if (Convert.ToInt32(BAID) == 0)
                return;
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select *,(select approval from SPD_Trans where ID=SPD_AttachmentDep.A_SarmutTransID)Approval " +
                "from SPD_AttachmentDep where rowstatus>-1 and A_SarmutTransID=" + BAID;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new SPD_Attachment
                        {
                            ID = Convert.ToInt32(sdr["ID"].ToString()),
                            SarmutransID = Convert.ToInt32(sdr["A_SarmutTransID"].ToString()),
                            DocName = sdr["DocName"].ToString(),
                            FileName = sdr["FileName"].ToString(),
                            Approval = Convert.ToInt32(sdr["Approval"].ToString())
                        });
                    }
                }
            }
            lst.DataSource = arrData;
            lst.DataBind();
        }

        private void LoadListAttachmentPrs(string BAID, Repeater lst)
        {
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            if (Convert.ToInt32(BAID) == 0)
                return;
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select *,(select approval from SPD_TransPrs where ID=SPD_AttachmentPrs.A_SarmutTransID)Approval " +
                "from SPD_AttachmentPrs where rowstatus>-1 and A_SarmutTransID=" + BAID;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new SPD_AttachmentPrs
                        {
                            ID = Convert.ToInt32(sdr["ID"].ToString()),
                            SarmutransID = Convert.ToInt32(sdr["A_SarmutTransID"].ToString()),
                            DocName = sdr["DocName"].ToString(),
                            FileName = sdr["FileName"].ToString(),
                            Approval = Convert.ToInt32(sdr["Approval"].ToString())
                        });
                    }
                }
            }
            lst.DataSource = arrData;
            lst.DataBind();
        }

        protected void attachm_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ScriptManager scriptMan = ScriptManager.GetCurrent(this.Page);
            Users users = (Users)Session["Users"];
            if (e.Item.ItemType == ListItemType.Header)
            {
                Image info = (Image)e.Item.Parent.Parent.FindControl("info");
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ImageButton btn = (ImageButton)e.Item.FindControl("lihat") as ImageButton;
                scriptMan.RegisterPostBackControl(btn);
                Image pre = (Image)e.Item.FindControl("lihat");
                Image hps = (Image)e.Item.FindControl("hapus");
                SPD_Attachment att = (SPD_Attachment)e.Item.DataItem;
                //pre.Attributes.Add("onclick", "PDFPreviewSarmut('" + pre.CssClass.ToString() + "')");
                hps.Visible = (att.Approval < 1) ? true : false;
                //hps.Visible = (users.Apv < 1) ? true : false;

            }
        }

        protected void attachm_Command(object sender, RepeaterCommandEventArgs e)
        {
            Repeater rpt = (Repeater)sender;
            try
            {
                switch (e.CommandName)
                {
                    case "pre":
                        string Nama = e.CommandArgument.ToString();
                        string Nama2 = @"\" + Nama;
                        string dirPath = @"D:\DATA LAMPIRAN PURCHN\sarmut\";
                        string ext = Path.GetExtension(Nama);
                        HttpResponse response = HttpContext.Current.Response;
                        Response.Clear();
                        string excelFilePath = dirPath + Nama;
                        System.IO.FileInfo file = new System.IO.FileInfo(excelFilePath);
                        if (file.Exists)
                        {
                            response.Clear();
                            response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                            response.AddHeader("Content-Length", file.Length.ToString());
                            response.ContentType = "application/octet-stream";
                            response.WriteFile(file.FullName);
                            response.End();
                        }
                        break;
                    case "hps":
                        Image hps = (Image)rpt.Items[int.Parse(e.CommandArgument.ToString())].FindControl("hapus");
                        ZetroView zl = new ZetroView();
                        zl.QueryType = Operation.CUSTOM;
                        zl.CustomQuery = "Update SPD_AttachmentDep set RowStatus=-1 where ID=" + hps.CssClass;
                        SqlDataReader sdr = zl.Retrieve();
                        LoadTypeSarmut();
                        break;
                }
            }
            catch
            {
                DisplayAJAXMessage(this, "Data belum tersimpan atau di approve");
                return;
            }

        }

        protected void attachPrs_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ScriptManager scriptMan = ScriptManager.GetCurrent(this.Page);

            Users users = (Users)Session["Users"];
            string[] viewApv = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ApprovalStatus", "BeritaAcara").Split(',');
            int idx = Array.IndexOf(viewApv, users.DeptID.ToString());
            if (e.Item.ItemType == ListItemType.Header)
            {
                Image info = (Image)e.Item.Parent.Parent.FindControl("info");
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ImageButton btn = (ImageButton)e.Item.FindControl("lihatprs") as ImageButton;
                scriptMan.RegisterPostBackControl(btn);
                Image pre = (Image)e.Item.FindControl("lihatprs");
                Image hps = (Image)e.Item.FindControl("hapusprs");
                SPD_AttachmentPrs att = (SPD_AttachmentPrs)e.Item.DataItem;
                hps.Visible = (att.Approval < 1) ? true : false;
                hps.Visible = (users.Apv < 1) ? true : false;
            }
        }

        protected void attachPrs_Command(object sender, RepeaterCommandEventArgs e)
        {
            Repeater rpt = (Repeater)sender;
            try
            {
                switch (e.CommandName)
                {
                    case "preprs":
                        string Nama = e.CommandArgument.ToString();
                        string Nama2 = @"\" + Nama;
                        string dirPath = @"D:\DATA LAMPIRAN PURCHN\sarmut\";
                        string ext = Path.GetExtension(Nama);

                        Response.Clear();
                        string excelFilePath = dirPath + Nama;
                        System.IO.FileInfo file = new System.IO.FileInfo(excelFilePath);
                        if (file.Exists)
                        {
                            Response.Clear();
                            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                            Response.AddHeader("Content-Length", file.Length.ToString());
                            Response.ContentType = "application/octet-stream";
                            Response.WriteFile(file.FullName);
                            Response.End();
                        }
                        break;
                    case "hpsprs":
                        Image hps = (Image)rpt.Items[int.Parse(e.CommandArgument.ToString())].FindControl("hapusprs");
                        ZetroView zl = new ZetroView();
                        zl.QueryType = Operation.CUSTOM;
                        zl.CustomQuery = "Update SPD_AttachmentPrs set RowStatus=-1 where ID=" + hps.CssClass;
                        SqlDataReader sdr = zl.Retrieve();
                        //LoadListAttachmentPrs(hps.AlternateText.ToString(), rpt);
                        LoadTypeSarmut();
                        break;
                }
            }
            catch
            {
                DisplayAJAXMessage(this, "Data belum tersimpan atau di approve");
                return;
            }
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        protected void lstDetail_DataBound(object sender, RepeaterItemEventArgs e)
        {


            Users users = (Users)Session["Users"];
            SPD_Dept ba = (SPD_Dept)e.Item.DataItem;
            Label lblTercapai = (Label)e.Item.FindControl("lblTercapai");
            CheckBox chk = (CheckBox)e.Item.FindControl("chk");
            TextBox txtActual = (TextBox)e.Item.FindControl("txtActual");
            if (ba.Approval > 1)
                txtActual.ReadOnly = true;
            else
                txtActual.ReadOnly = false;
            if (ba.ID == 0)
            {
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "insert SPD_Trans(Tahun, Bulan, SarmutDeptID, Actual, RowStatus, CreatedBy, CreatedTime, " +
                    "LastModifiedBy, LastModifiedTime, Approval)values(" + ddlTahun.SelectedValue + "," + ddlBulan.SelectedValue + "," +
                    ba.SDeptID + ",0,0,'" + users.UserName + "',getdate(),'" + users.UserName + "',getdate(),0)";
                SqlDataReader sdr = zl.Retrieve();
                newdata = 1;
            }

            lblTercapai.ToolTip = ba.ID.ToString();
            txtActual.ToolTip = ba.Param;
            if (ba.Param.Trim().ToUpper() == "MIN")
            {
                if (ba.Target > ba.Actual)
                    lblTercapai.Text = "Tidak Tercapai";
                else
                    lblTercapai.Text = "Tercapai";
            }
            else if (ba.Param.Trim().ToUpper() == "ORDER KEMBALI")
            {
                if (ba.Target > ba.Actual)
                    lblTercapai.Text = "Tidak Tercapai";
                else
                    lblTercapai.Text = "Tercapai";
            }
            else
            {
                if (ba.Target >= ba.Actual)
                    lblTercapai.Text = "Tercapai";
                else
                    lblTercapai.Text = "Tidak Tercapai";
            }
            chk.Visible = (users.Apv > 0) ? true : false;
            chk.Checked = (ba.Checked > 0) ? true : false;
            Image att = (Image)e.Item.FindControl("att");
            att.Visible = (users.Apv > 0) ? false : true;
            att.Attributes.Add("onclick", "OpenDialog('" + ba.ID.ToString() + "&tp=1')");
            Repeater rps = (Repeater)e.Item.FindControl("attachm");
            //HtmlGenericControl ps = (HtmlGenericControl)e.Item.FindControl("ps");
            LoadListAttachment(ba.ID.ToString(), rps);
            if (ba.OnSystem == 1)
            {
                txtActual.ReadOnly = true;
                txtActual.ToolTip = "Actual value on System";
            }
            Repeater rps1 = (Repeater)e.Item.FindControl("lstDetail2");
            HtmlGenericControl ps1 = (HtmlGenericControl)e.Item.FindControl("ps");
            LoadListSarmutDetail(ba.SDeptID, rps1);
        }

        protected void lstDetailx_DataBound(object sender, RepeaterItemEventArgs e)
        {
            SPD_Dept ba = (SPD_Dept)e.Item.DataItem;
            Users users = (Users)Session["Users"];
            Label lblTercapai2 = (Label)e.Item.FindControl("lblTercapai2");
            Label lblTarget2 = (Label)e.Item.FindControl("lblTarget2");
            Label lblStatusApv = (Label)e.Item.FindControl("lblStatusApv");
            CheckBox chk = (CheckBox)e.Item.FindControl("chk2");
            TextBox txtActual2 = (TextBox)e.Item.FindControl("txtActual2");

            if (ba.ID == 0)
            {
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "insert SPD_Trans(Tahun, Bulan, SarmutDeptID, Actual, RowStatus, CreatedBy, CreatedTime, " +
                    "LastModifiedBy, LastModifiedTime, Approval)values(" + ddlTahun.SelectedValue + "," + ddlBulan.SelectedValue + "," +
                    ba.SDeptID + ",0,0,'" + users.UserName + "',getdate(),'" + users.UserName + "',getdate(),0)";
                SqlDataReader sdr = zl.Retrieve();
                newdata = 1;
            }

            Label lblTercapai = (Label)e.Item.FindControl("lblTercapai");

            if (ba.Param.Trim().ToUpper() == "MIN")
            {
                if (ba.Target > ba.Actual)
                    lblTercapai.Text = "Tidak Tercapai";
                else
                    lblTercapai.Text = "Tercapai";
            }
            else if (ba.Param.Trim().ToUpper() == "ORDER KEMBALI")
            {
                if (ba.Target > ba.Actual)
                    lblTercapai.Text = "Tidak Tercapai";
                else
                    lblTercapai.Text = "Tercapai";
            }
            else
            {
                if (ba.Target >= ba.Actual)
                    lblTercapai.Text = "Tercapai";
                else
                    lblTercapai.Text = "Tidak Tercapai";
            }

            //if (ba.Satuan.Trim() == string.Empty)
            //{
            //    lblTercapai2.Text = string.Empty;
            //    lblTarget2.Text = string.Empty;
            //    txtActual2.Text = string.Empty;
            //    lblStatusApv.Text = string.Empty;
            //    txtActual2.Enabled = false;
            //}
            Repeater rps1 = (Repeater)e.Item.FindControl("lstDetail2x");
            HtmlGenericControl ps1 = (HtmlGenericControl)e.Item.FindControl("ps");
            LoadListSarmutDetailx(ba.SDeptID, rps1);
        }

        protected void lstDetail2_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Users users = (Users)Session["Users"];
            SPD_DeptDet ba = (SPD_DeptDet)e.Item.DataItem;
            Label lblTercapai2 = (Label)e.Item.FindControl("lblTercapai2");
            Label lblTarget2 = (Label)e.Item.FindControl("lblTarget2");
            Label lblStatusApv = (Label)e.Item.FindControl("lblStatusApv");
            CheckBox chk = (CheckBox)e.Item.FindControl("chk2");
            TextBox txtActual2 = (TextBox)e.Item.FindControl("txtActual2");
            if (ba.ID == 0)
            {
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "insert SPD_TransDet(Tahun, Bulan, SarmutDeptID, Actual, RowStatus, CreatedBy, CreatedTime, " +
                    "LastModifiedBy, LastModifiedTime, Approval)values(" + ddlTahun.SelectedValue + "," + ddlBulan.SelectedValue + "," +
                    ba.SDeptID + ",0,0,'" + users.UserName + "',getdate(),'" + users.UserName + "',getdate(),0)";
                SqlDataReader sdr = zl.Retrieve();
                newdata = 1;
            }

            lblTercapai2.ToolTip = ba.ID.ToString();
            txtActual2.ToolTip = ba.Param;
            if (txtActual2.ToolTip.Trim().ToUpper() == "MIN")
            {
                if (ba.Target > ba.Actual)
                    lblTercapai2.Text = "Tidak Tercapai";
                else
                    lblTercapai2.Text = "Tercapai";
            }
            else if (txtActual2.ToolTip.Trim().ToUpper() == "ORDER KEMBALI")
            {
                if (ba.Target > ba.Actual)
                    lblTercapai2.Text = "Tidak Tercapai";
                else
                    lblTercapai2.Text = "Tercapai";
            }
            else
            {
                if (ba.Target >= ba.Actual)
                    lblTercapai2.Text = "Tercapai";
                else
                    lblTercapai2.Text = "Tidak Tercapai";
            }
            if (ba.Satuan.Trim() == string.Empty)
            {
                lblTercapai2.Text = string.Empty;
                lblTarget2.Text = string.Empty;
                txtActual2.Text = string.Empty;
                lblStatusApv.Text = string.Empty;
                txtActual2.Enabled = false;
            }
            chk.Visible = (ba.Approval < ApprovalLevel) ? chk.Visible : false;
        }

        protected void lstDetail2_Command(object sender, RepeaterCommandEventArgs e)
        {

        }
        protected void lstPrs_Command(object sender, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "attachPrs":
                    Repeater rpts = (Repeater)e.Item.FindControl("attachPrs");
                    ImageButton imgs = (ImageButton)e.Item.FindControl("attPrs");
                    LoadTypeSarmut();
                    break;
            }
        }
        protected void lstDetail_Command(object sender, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "attach":
                    Repeater rpts = (Repeater)e.Item.FindControl("attachm");
                    ImageButton imgs = (ImageButton)e.Item.FindControl("att");
                    LoadTypeSarmut();
                    break;
            }
        }
        protected void chk_CheckedChangeRpt(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            string transID = chk.ToolTip;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            if (chk.Checked == true)
                zl.CustomQuery = "update spd_trans set checked=1 where id=" + transID;
            else
                zl.CustomQuery = "update spd_trans set checked=0 where id=" + transID;
            SqlDataReader sdr = zl.Retrieve();
            LoadTypeSarmut();
        }
        protected void chk_CheckedChangePrs(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            string transID = chk.ToolTip;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            if (chk.Checked == true)
                zl.CustomQuery = "update spd_transPrs set checked=1 where id=" + transID;
            else
                zl.CustomQuery = "update spd_transPrs set checked=0 where id=" + transID;
            SqlDataReader sdr = zl.Retrieve();
            LoadTypeSarmut();
        }
        protected void chk_CheckedChange(object sender, EventArgs e)
        {
            Repeater lstDetail;
            Repeater lstPrs;
            int i = 0;
            string transID = string.Empty;
            foreach (RepeaterItem objItem0 in lstType.Items)
            {
                lstPrs = ((Repeater)(objItem0.FindControl("lstPrs")));
                foreach (RepeaterItem objItem in lstPrs.Items)
                {
                    lstDetail = ((Repeater)(objItem.FindControl("lstDetail")));
                    i = 0;
                    foreach (RepeaterItem objDetail in lstDetail.Items)
                    {
                        CheckBox chk = (CheckBox)lstDetail.Items[i].FindControl("chk");
                        chk.Checked = chkAll.Checked;
                        transID = chk.ToolTip;
                        ZetroView zl = new ZetroView();
                        zl.QueryType = Operation.CUSTOM;
                        if (chk.Checked == true)
                            zl.CustomQuery = "update spd_trans set checked=1 where id=" + transID;
                        else
                            zl.CustomQuery = "update spd_trans set checked=0 where id=" + transID;
                        SqlDataReader sdr = zl.Retrieve();
                        i++;
                    }
                }
            }
            foreach (RepeaterItem objItem0 in lstType.Items)
            {
                lstPrs = ((Repeater)(objItem0.FindControl("lstPrs")));
                i = 0;
                foreach (RepeaterItem objItem in lstPrs.Items)
                {
                    CheckBox chk = (CheckBox)lstPrs.Items[i].FindControl("chkprs");
                    chk.Checked = chkAll.Checked;
                    transID = chk.ToolTip;
                    ZetroView zl = new ZetroView();
                    zl.QueryType = Operation.CUSTOM;
                    if (chk.Checked == true)
                        zl.CustomQuery = "update spd_transprs set checked=1 where id=" + transID;
                    else
                        zl.CustomQuery = "update spd_transprs set checked=0 where id=" + transID;
                    SqlDataReader sdr = zl.Retrieve();
                    i++;
                }
            }
            LoadTypeSarmut();
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];
            Repeater lstDetail;
            Repeater lstPrs;
            foreach (RepeaterItem objItem0 in lstType.Items)
            {
                lstPrs = ((Repeater)(objItem0.FindControl("lstPrs")));
                foreach (RepeaterItem objItem in lstPrs.Items)
                {
                    lstDetail = ((Repeater)(objItem.FindControl("lstDetail")));
                    int i = 0;
                    foreach (RepeaterItem objDetail in lstDetail.Items)
                    {
                        Label lblTercapai = (Label)lstDetail.Items[i].FindControl("lblTercapai");
                        Label lblTarget = (Label)lstDetail.Items[i].FindControl("lblTarget");
                        CheckBox chk = (CheckBox)lstDetail.Items[i].FindControl("chk");
                        ZetroView zl = new ZetroView();
                        zl.QueryType = Operation.CUSTOM;
                        if (chk.Checked == true)
                            zl.CustomQuery = "update SPD_Trans set approval =" + users.Apv + " where ID=" + chk.ToolTip +
                                " update SPD_Transdet set approval =" + users.Apv + " where sarmutdeptID=" + chk.ToolTip;
                        SqlDataReader sdr = zl.Retrieve();
                        i++;
                    }
                }
            }
            foreach (RepeaterItem objItem0 in lstType.Items)
            {
                lstPrs = ((Repeater)(objItem0.FindControl("lstPrs")));
                int j = 0;
                foreach (RepeaterItem objItem in lstPrs.Items)
                {
                    CheckBox chk = (CheckBox)lstPrs.Items[j].FindControl("chkprs");
                    ZetroView zl = new ZetroView();
                    zl.QueryType = Operation.CUSTOM;
                    if (chk.Checked == true)
                        zl.CustomQuery = "update SPD_TransPrs set approval =" + users.Apv + " where ID=" + chk.ToolTip;
                    SqlDataReader sdr = zl.Retrieve();
                    j++;
                }
            }
            LoadTypeSarmut();
        }

        private void ApprovalPreview(int BAID, Repeater lstApp)
        {
            ArrayList arrApp = new ArrayList();
            string AppUser = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Approval", "BeritaAcara");
            ZetroView zv = new ZetroView();
            zv.QueryType = Operation.CUSTOM;
            zv.CustomQuery = "Select Distinct BAID as ID,UserID,UserName,IPAddress,Approval," +
                             "(Select Top 1 CreatedTime from BeritaAcaraApproval b where b.BAID=BeritaAcaraApproval.BAID and " +
                             " b.UserID=BeritaAcaraApproval.UserID)CreatedTime from BeritaAcaraApproval where BAID=" + BAID +
                             " order by BeritaAcaraApproval.Approval";
            SqlDataReader sdr = zv.Retrieve();
            if (sdr.HasRows && sdr != null)
            {
                while (sdr.Read())
                {
                    arrApp.Add(new ApprovalBA
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        UserID = Convert.ToInt32(sdr["UserID"].ToString()),
                        UserName = sdr["UserName"].ToString(),
                        IPAddress = sdr["IPAddress"].ToString(),
                        Approval = Convert.ToInt32(sdr["Approval"].ToString()),
                        AppStatus = (Convert.ToInt32(sdr["Approval"].ToString()) > 0) ? "Approved" : "UnApproved",
                        CreatedTime = Convert.ToDateTime(sdr["CreatedTime"].ToString())
                    });
                }
            }
            lstApp.DataSource = arrApp;
            lstApp.DataBind();
        }
        private ArrayList ApprovalPreview(int BAID)
        {
            ArrayList arrApp = new ArrayList();
            string AppUser = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Approval", "BeritaAcara");
            ZetroView zv = new ZetroView();
            zv.QueryType = Operation.CUSTOM;
            zv.CustomQuery = "Select Distinct BAID as ID,UserID,UserName,IPAddress,Approval," +
                                     "(Select Top 1 CreatedTime from BeritaAcaraApproval b where b.BAID=BeritaAcaraApproval.BAID and " +
                                     " b.UserID=BeritaAcaraApproval.UserID)CreatedTime from BeritaAcaraApproval where BAID=" + BAID +
                                     " order by BeritaAcaraApproval.Approval";
            SqlDataReader sdr = zv.Retrieve();
            if (sdr.HasRows && sdr != null)
            {
                while (sdr.Read())
                {
                    arrApp.Add(new ApprovalBA
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        UserID = Convert.ToInt32(sdr["UserID"].ToString()),
                        UserName = sdr["UserName"].ToString(),
                        IPAddress = sdr["IPAddress"].ToString(),
                        Approval = Convert.ToInt32(sdr["Approval"].ToString()),
                        AppStatus = (Convert.ToInt32(sdr["Approval"].ToString()) > 0) ? "Approved" : "UnApproved",
                        CreatedTime = Convert.ToDateTime(sdr["CreatedTime"].ToString())
                    });
                }
            }
            return arrApp;
        }

        private int ByPassLevel()
        {
            int result = 0;
            string[] ByPass = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ByPass", "BeritaAcara").Split(',');
            if (ByPass.Count() > 1)
            {
                result = int.Parse(ByPass[1]);
            }
            return result;
        }

        private int ByPassUser()
        {
            int result = 0;
            string[] ByPass = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ByPass", "BeritaAcara").Split(',');
            if (ByPass.Count() > 1)
            {
                result = int.Parse(ByPass[0]);
            }
            return result;
        }
        private bool CheckAttach(int BAID)
        {
            bool result = false;
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "Select ID from BeritaAcaraAttachment where RowStatus>-1 and BAID=" + BAID.ToString();
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                result = true;
            }
            return result;
        }
        private int CountAttachDoc(int BAID)
        {
            int result = 0;
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "Select COUNT(ID) as jml from BeritaAcaraAttachment where RowStatus>-1 and BAID=" + BAID.ToString();
            SqlDataReader sdr = zw.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        result = Convert.ToInt32(sdr["jml"].ToString());
                    }
                }
            }
            return result;
        }
        //private void UploadFile(int BAID)
        //{
        //    if (Upload1.HasFile)
        //    {
        //        string FilePath = Upload1.PostedFile.FileName;
        //        string filename = Path.GetFileName(FilePath);
        //        string ext = Path.GetExtension(filename);
        //        if (ext.ToLower() == ".pdf")
        //        {
        //            Stream fs = Upload1.PostedFile.InputStream;
        //            BinaryReader br = new BinaryReader(fs);
        //            Byte[] bytes = br.ReadBytes((Int32)fs.Length);
        //            ZetroLib zl = new ZetroLib();
        //            zl.Option = "Insert";
        //            zl.Criteria = "BAID,FileName,Attachment,RowStatus,Createdby,CreatedTime";
        //            zl.hlp = new AttachmentBA();
        //            zl.StoreProcedurName = "spBeritaAcaraAtt_Insert";
        //            string rst = zl.CreateProcedure();
        //            if (rst == string.Empty)
        //            {
        //                AttachmentBA ba = new AttachmentBA();
        //                ba.BAID = BAID;
        //                ba.FileName = filename.ToString();
        //                ba.Attachment = bytes;
        //                ba.RowStatus = 0;
        //                ba.CreatedBy = ((Users)Session["Users"]).UserName;
        //                zl.hlp = ba;
        //                int rs = zl.ProcessData();
        //                if (rs > 0)
        //                {
        //                    //LoadListSarmut(int.Parse(appLevele.Value));
        //                }
        //            }
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", "alert('Hanya file pdf yng bisa di uploadf')", true);
        //        }
        //    }
        //}

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

        private void KirimEmail(int NextApproval, string NoBA, string Approver)
        {
            string[] AprovalList = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Approval", "BeritaAcara").Split(',');
            UsersFacade usf = new UsersFacade();
            Users users = usf.RetrieveById(NextApproval);
            MailMessage mail = new MailMessage();
            EmailReportFacade msg = new EmailReportFacade();
            string token = new EncryptPasswordFacade().EncryptToString("id=2&UserID=" + users.UserID + "&pwd=" + users.Password.ToString());
            try
            {
                mail.From = new MailAddress("system_support@grcboard.com");
                mail.To.Add(users.UsrMail.ToString());
                // mail.Bcc.Add("noreplay@grcboard.com");
                mail.Subject = "Approval BA Kertas Kantong Semen ";
                mail.Body = "Mohon untuk di Approve BA Kertas Kantong Semen sebagai berikut : \n\r";
                mail.Body += NoBA;
                mail.Body += "Silahkan klik link berikut untuk Approval : \n\r";
                mail.Body += (users.UnitKerjaID == 7) ? "http://krwg.grcboard.com/?link=" ://Modul/Purchasing/FormBAApproval.aspx?token=" + token :
                            "http://ctrp.grcboard.com/?link=";//Modul/Purchasing/FormBAApproval.aspx?token=" + token;
                mail.Body += "\n\r";
                mail.Body += "Approver List :\n\r";
                //mail.Body += Approver;
                mail.Body += "Terimakasih, " + "\n\r";
                mail.Body += "Salam GRCBOARD " + "\n\r";
                mail.Body += "Regard's, " + "\n\r";
                mail.Body += ((Users)Session["Users"]).UserName + "\n\r\n\r\n\r";
                //msg.Body += emailFacade.mailFooter();
                SmtpClient smt = new SmtpClient(msg.mailSmtp());
                smt.Host = msg.mailSmtp();
                smt.Port = msg.mailPort();
                smt.EnableSsl = true;
                smt.DeliveryMethod = SmtpDeliveryMethod.Network;
                smt.UseDefaultCredentials = false;
                smt.Credentials = new System.Net.NetworkCredential("sodikin@grcbaord.com", "grc123!@#");
                //bypas certificate
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                        System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                        System.Security.Cryptography.X509Certificates.X509Chain chain,
                        System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
                smt.Send(mail);
            }
            catch { }
        }

        private string ListBA(string BAID)
        {
            string result = string.Empty;
            BeritaAcara ba = new BeritaAcara();
            ba = this.GetDetailBA(BAID);
            result += " BA No : " + ba.BANum + " ";
            result += " Depo :" + ba.DepoKertasName + "\n\r";
            return result;
        }
        private string ListApprover(string BAID)
        {
            string result = string.Empty;
            ArrayList arrd = this.ApprovalPreview(int.Parse(BAID));
            foreach (ApprovalBA ap in arrd)
            {
                result += ap.Approval.ToString() + ". " + ap.UserName + " on :" + ap.CreatedTime + " [ " + ap.AppStatus.ToString() + " ]\n\r";
            }
            return result;
        }
        private BeritaAcara GetDetailBA(string BAID)
        {
            BeritaAcara ba = new BeritaAcara();
            string strSQL = "select *,(Select DepoKertas.DepoName From DepoKertas where ID=DepoKertasID)Depo " +
                            "from BeritaAcara where ID=" + BAID;
            DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ba.ID = Convert.ToInt32(sdr["ID"].ToString());
                    ba.BANum = sdr["BaNum"].ToString();
                    ba.DepoKertasName = sdr["Depo"].ToString();
                }
            }
            return ba;
        }

        //private int LoadA(string DeptID)
        //{
        //    int Ada = 0;
        //    BeritaAcara01 ba = new BeritaAcara01();
        //    string strSQL =

        //    "select count(ID)Ada from users where DeptID=" + DeptID + " and apv=2 and RowStatus>-1 and UserAlias in " +
        //    "(select SgnAdmName from SPD_Dept where RowStatus>-1 and DptID="+DeptID+") ";

        //    DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
        //    SqlDataReader sdr = da.RetrieveDataByString(strSQL);
        //    if (da.Error == string.Empty && sdr.HasRows)
        //    {
        //        while (sdr.Read())
        //        {
        //            ba.Ada = Convert.ToInt32(sdr["Ada"].ToString());               
        //        }
        //    }
        //    return Ada;
        //}

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {

            LoadTypeSarmut();

        }
        protected void RbApv_CheckedChanged(object sender, EventArgs e)
        {
            if (RbApv.Checked == true)
            {
                btnApprove.Enabled = true;
                btnUnApprove.Enabled = true;
                LoadTypeSarmut();
            }
        }
        protected void RbList_CheckedChanged(object sender, EventArgs e)
        {
            if (RbList.Checked == true)
            {
                btnApprove.Enabled = false;
                btnUnApprove.Enabled = false;
                LoadTypeSarmut();
            }
        }
        protected void ddlTahun_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTypeSarmut();
            if (ddlDept.SelectedItem.Text.Trim().ToUpper() == "LOGISTIK MATERIAL")
            {
                Update_Sarmut_Logistik_M();
                LoadTypeSarmut();
            }
        }
    }
    public class SPD_Type : GRCBaseDomain
    {
        public string JenisSarmut { get; set; }
    }
    public class SPD_Prs : GRCBaseDomain
    {
        public string Description { get; set; }
        public string Satuan { get; set; }
        public string Param { get; set; }
        public decimal Target { get; set; }
        public string Target2 { get; set; }
        public decimal Actual { get; set; }
        public int Approval { get; set; }
        public int Urutan { get; set; }
        public int TypeID { get; set; }
        public int SDeptID { get; set; }
        public int Checked { get; set; }
        public int EditTarget { get; set; }
        public string StatusApv { get; set; }
        public int OnSystem { get; set; }
    }
    public class SPD_Dept : GRCBaseDomain
    {
        public string Description { get; set; }
        public string Satuan { get; set; }
        public string Param { get; set; }
        public decimal Target { get; set; }
        public decimal Actual { get; set; }
        public int Approval { get; set; }
        public int SDeptID { get; set; }
        public int Urutan { get; set; }
        public int Checked { get; set; }
        public string StatusApv { get; set; }
        public int OnSystem { get; set; }
    }
    public class SPD_DeptDet : GRCBaseDomain
    {
        public string Description { get; set; }
        public string Satuan { get; set; }
        public string Param { get; set; }
        public decimal Target { get; set; }
        public decimal Actual { get; set; }
        public int Approval { get; set; }
        public int SDeptID { get; set; }
        public int Urutan { get; set; }
        public int Checked { get; set; }
        public string StatusApv { get; set; }
        public int OnSystem { get; set; }
    }
    public class SPD_Attachment : GRCBaseDomain
    {
        public string FileName { get; set; }
        public string DocName { get; set; }
        public int SarmutransID { get; set; }
        public int Approval { get; set; }
    }

    public class SPD_AttachmentPrs : GRCBaseDomain
    {
        public string FileName { get; set; }
        public string DocName { get; set; }
        public int SarmutransID { get; set; }
        public int Approval { get; set; }
    }

    public class BeritaAcara01 : GRCBaseDomain
    {
        public int Ada { get; set; }
    }

    public class FacadeBA
    {
        public string strError = string.Empty;
        private ArrayList arrData = new ArrayList();
        private BeritaAcara01 pm = new BeritaAcara01();
        private List<SqlParameter> sqlListParam;

        public string Criteria { get; set; }
        public string Field { get; set; }
        public string Where { get; set; }

        public int LoadA(string DeptID)
        {
            // BeritaAcara01 ba1 = new BeritaAcara01();
            string strSQL =

            "select count(ID)Ada from users where DeptID=" + DeptID + " and apv=2 and RowStatus>-1 and UserAlias in " +
            "(select SgnAdmName from SPD_Dept where RowStatus>-1 and DptID=" + DeptID + ") ";

            DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    return Convert.ToInt32(sdr["Ada"]);
                }
            }
            return 0;
        }
    }
}