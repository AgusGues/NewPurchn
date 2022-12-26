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
using System.Web.UI.DataVisualization.Charting;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace GRCweb1.Modul.Sarmut
{
    public partial class APSarmut : System.Web.UI.Page
    {
        public string Tahun = Global.nBulan(DateTime.Now.Month);
        private ArrayList arrData = new ArrayList();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Users user = ((Users)Session["Users"]);
                FacadePrsaX smtFacade = new FacadePrsaX();
                SPD_PrsAx spd = new SPD_PrsAx();
                LoadDept();
                LoadDeptRekap();
                LoadBulanRekap();
                LoadTahunRekap();
                LoadDept1();
                LoadSmtP();
                LoadBulan();
                LoadTahun();
                LoadTahun2();
                LoadLampiran(sarmutnotxt.Text);
                txtAsarmut_Date0.SelectedDate = DateTime.Now;
                txtDateTKasus.SelectedDate = DateTime.Now;
                LoadSMT();
                txtSemester.Text = "Semester";
                BulanPilih.Text = "Bulan";
                //chkTercapai.Enabled = false;
                //chkTTercapai.Enabled = false;
                string UserID = string.Empty;
                UserID = user.ID.ToString();
                string usertype = smtFacade.GetUserType(UserID);
                Session["usertype"] = usertype;

                //WO PM JOMBANG bisa liat all dept
                SPD_Dept spda = new SPD_Dept();
                FacadePrsaX facadeP = new FacadePrsaX();
                spda = facadeP.GetUserSPD(user.ID);
                if (spda.Dept.Trim() == "IT" || spda.Dept.Trim() == "ISO")
                {
                    LDept.Visible = true;
                    ddlDeptName1.Visible = true;
                    btnRekap.Visible = true;
                    //BtnPemantauan.Visible = true;
                }
                else
                {
                    LDept.Visible = false;
                    ddlDeptName1.Visible = false;
                    btnRekap.Visible = false;
                    //BtnPemantauan.Visible = false;
                }

                //if (user.DeptID == 23 || user.DeptID == 14)
                //{
                //    LDept.Visible = true;
                //    ddlDeptName1.Visible = true;
                //    btnRekap.Visible = true;
                //    //BtnPemantauan.Visible = true;
                //}
                //else
                //{
                //    LDept.Visible = false;
                //    ddlDeptName1.Visible = false;
                //    btnRekap.Visible = false;
                //    //BtnPemantauan.Visible = false;
                //}

                if (sarmutnotxt.Text.Trim() != string.Empty)
                //if (Request.QueryString["AnNo"] != null)
                {
                    DataSMTx(Request.QueryString["AnNo"].ToString());
                }
                //string Apv = smtFacade.GetApv(UserID);
                //if (Convert.ToInt32(Apv) > 0)
                //    btnBackApv.Visible = true;


            }
            //((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GridSMT.ClientID  + "', 450, 99 , 21 ,false); </script>", false);

        }

        protected void RBx_CheckedChanged(object sender, EventArgs e)
        {
            if (RBAnalisa.Checked == true)
            {
                ddlBulan.Visible = true;
                ddlSemester.Visible = false;
                txtSemeterAct.Visible = false;
                txtActual.Visible = true;
                //BulanPilih.Visible = true;
                //ddlBulan0.Visible = true;
                //txtSemester.Visible = false;
                //ddlsmtList.Visible = false;
                LoadBulan();
            }
            //else
            //{
            //    ddlBulan.Visible = false;
            //    ddlSemester.Visible = true;

            //}

        }

        protected void RBx1_CheckedChanged(object sender, EventArgs e)
        {
            if (RBSemester.Checked == true)
            {
                ddlSemester.Visible = true;
                ddlBulan.Visible = false;
                txtActual.Visible = false;
                ddlsmtP.SelectedIndex = 0;
                //ddlsarmutDept.SelectedIndex = 0;
                ddlSemester.SelectedIndex = 0;
                txtTargetx.Text = string.Empty;
                Label1.Text = string.Empty;
                txtParam.Text = string.Empty;
                txtSemeterAct.Visible = true;
                chkTercapai.Checked = false;
                chkTTercapai.Checked = false;
                //BulanPilih.Visible = false;
                //ddlBulan0.Visible = false;
                //txtSemester.Visible = true;
                //ddlsmtList.Visible = true;

            }
            //else
            //{
            //    ddlSemester.Visible = false;
            //    txtSemeterAct.Visible = false;
            //    ddlBulan.Visible = true;
            //    LoadBulan();
            //}

        }

        protected void btnApproveForm_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApprovalAPSarmut.aspx");
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

        private void LoadBulanRekap()
        {
            ddlPeriodeBulan.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                ddlPeriodeBulan.Items.Add(new ListItem(Global.nBulan(i).ToString(), i.ToString().PadLeft(2, '0')));
            }
            ddlPeriodeBulan.SelectedValue = DateTime.Now.Month.ToString().PadLeft(2, '0');
        }

        protected void ddlBulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlsarmutDept.SelectedIndex > 0)
            //{
                FacadePrsaX deptx = new FacadePrsaX();
                SPD_PrsAx xxx = new SPD_PrsAx();
            //if (RBAnalisa.Checked == true)
            //{
            //    xxx = deptx.RetrieveById(int.Parse(ddlBulan.SelectedValue), (int.Parse(ddlTahun.SelectedValue)), (int.Parse(ddlsmtP.SelectedValue)), (int.Parse(ddlsarmutDept.SelectedValue)));
            //    txtTargetx.Text = xxx.Target.ToString();
            //    txtActual.Text = xxx.Actual.ToString();
            //    txtSatuan.Text = xxx.Satuan.ToString();
            //    txtParam.Text = xxx.Param.ToString();
            //    Label1.Text = xxx.Satuan.ToString();
            //}
            //if (RBSemester.Checked == true)
            //{
            //    xxx = deptx.RetrieveByIdR((ddlSemester.SelectedValue).ToString(), (int.Parse(ddlTahun.SelectedValue)), (int.Parse(ddlsmtP.SelectedValue)), (int.Parse(ddlsarmutDept.SelectedValue)));
            //    txtTargetx.Text = xxx.Target.ToString();
            //    txtActual.Text = xxx.Actual.ToString();
            //    txtSatuan.Text = xxx.Satuan.ToString();
            //    txtParam.Text = xxx.Param.ToString();
            //    Label1.Text = xxx.Satuan.ToString();
            //}
            
            if (ddlsarmutDept.SelectedIndex > 0)
                ddlsmtDept_SelectedIndexChanged(null, null);
            else 
                ddlsmtP_SelectedIndexChanged(null, null);
            // }
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
                        //    Image hps = (Image)rpt.Items[int.Parse(e.CommandArgument.ToString())].FindControl("hapusprs");
                        //    ZetroView zl = new ZetroView();
                        //    zl.QueryType = Operation.CUSTOM;
                        //    zl.CustomQuery = "Update SPD_AttachmentPrs set RowStatus=-1 where ID=" + hps.CssClass;
                        //    SqlDataReader sdr = zl.Retrieve();
                        //    //LoadListAttachmentPrs(hps.AlternateText.ToString(), rpt);
                        //    LoadTypeSarmut();
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
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ImageButton btn = (ImageButton)e.Item.FindControl("lihatprs") as ImageButton;
                scriptMan.RegisterPostBackControl(btn);
            }
        }


        protected void btnPreview_Click(object sender, EventArgs e)
        {
            ListAnalisaPemantauan();
        }

        private void ListAnalisaPemantauan()
        {
            string strSQuery = string.Empty;
            FacadePrsaX aNFacade = new FacadePrsaX();
            SPD_PrsAx spd = new SPD_PrsAx();
            ArrayList arrData = new ArrayList();
            if (RBAll.Checked == true)
            {
                arrData = aNFacade.RetrieveAllMonth(ddlPeriodeBulan.SelectedValue, ddlPeriodeTahun.SelectedValue, ddlDeptRekap.SelectedValue);
            }
            else if (RBOpen.Checked == true)
            {
                arrData = aNFacade.RetrieveAllOpen(ddlPeriodeBulan.SelectedValue, ddlPeriodeTahun.SelectedValue, ddlDeptRekap.SelectedValue);
            }
            else if (RBClosed.Checked == true)
            {
                arrData = aNFacade.RetrieveAllClosed(ddlPeriodeBulan.SelectedValue, ddlPeriodeTahun.SelectedValue, ddlDeptRekap.SelectedValue);
            }

            //PagedDataSource pg = new PagedDataSource();
            //pg.DataSource = arrData;
            //pg.AllowPaging = true;
            //pg.PageSize = 10;
            //pg.CurrentPageIndex = PageNumber;
            //if (pg.Count > 0)
            //{
            //    rptPaging.Visible = true;
            //    ArrayList pages = new ArrayList();
            //    pgs.Text = "Hal : 1 to " + pg.PageCount.ToString();
            //    for (int i = 0; i <= pg.PageCount - 1; i++)
            //    {
            //        pages.Add((i + 1).ToString());
            //    }
            //    rptPaging.DataSource = pages;
            //    rptPaging.DataBind();
            //    rptPaging.Visible = true;
            //}
            //else
            //{
            //    rptPaging.Visible = false;
            //}
            ListAnalisaData.DataSource = arrData;
            ListAnalisaData.DataBind();
        }

        protected void rptPaging_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            PageNumber = Convert.ToInt32(e.CommandArgument) - 1;
            ListAnalisaPemantauan();
        }

        public int PageNumber
        {
            get
            {
                if (ViewState["PageNumber"] != null)
                {
                    return Convert.ToInt32(ViewState["PageNumber"]);
                }
                else
                {
                    return 0;
                }
            }
            set { ViewState["PageNumber"] = value; }
        }

        protected void ListAnalisaData_Command(object sender, RepeaterCommandEventArgs e)
        {

        }

        protected void ListAnalisaData_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ArrayList arrData = new ArrayList();
            SPD_PrsAx sPD = (SPD_PrsAx)e.Item.DataItem;
            Label lbl = (Label)e.Item.FindControl("Status");
            Label lbl2 = (Label)e.Item.FindControl("Sop");
            Label lbl3 = (Label)e.Item.FindControl("Verf");
            //Status 
            switch (sPD.CLosed)
            {
                case 0:
                    lbl.Text = "Open";
                    break;
                case 1:
                    lbl.Text = "Close";
                    break;
                default:
                    lbl.Text = "";
                    break;
            }
            //Status Solved
            switch (sPD.Solved)
            {
                case 0:
                    lbl2.Text = "UnSolved";
                    break;
                case 1:
                    lbl2.Text = "Solved";
                    break;
                default:
                    lbl2.Text = "";
                    break;
            }
            //Verf
            switch (sPD.Verifikasi)
            {
                case 0:
                    lbl3.Text = "X";
                    break;
                case 1:
                    lbl3.Text = "Y";
                    break;
                case 2:
                    lbl3.Text = "Y";
                    break;
                case 3:
                    lbl3.Text = "Y";
                    break;
                case 4:
                    lbl3.Text = "Y";
                    break;
                case 5:
                    lbl3.Text = "Y";
                    break;
                case 6:
                    lbl3.Text = "Y";
                    break;
                default:
                    lbl3.Text = "";
                    break;
            }


        }

        protected void BtnRekap_ServerClick(object sender, EventArgs e)
        {
            if (btnRekap.Value == "Rekap")
            {
                AnalisaPanel.Visible = false;
                btnNew.Visible = false;
                btnUpdate.Visible = false;
                btnList.Visible = false;
                //btnRekap.Visible = false;
                btnRekap.Value = "Form";
                PanelList.Visible = false;
                PanelRekap.Visible = true;
                LoadSMT();
            }
            else
            {
                AnalisaPanel.Visible = true;
                btnNew.Visible = true;
                btnUpdate.Visible = true;
                btnRekap.Visible = true;
                btnList.Visible = true;
                btnRekap.Value = "Rekap";
                PanelList.Visible = false;
                PanelRekap.Visible = false;
            }
        }

        protected void BtnLampiran_ServerClick(object sender, EventArgs e)
        {
            if (sarmutnotxt.Text.Trim() != string.Empty)
            {
                if (btnLampiran.Value == "Lampiran")
                {
                    AnalisaPanel.Visible = false;
                    btnNew.Visible = false;
                    btnUpdate.Visible = false;
                    btnRekap.Visible = false;
                    btnList.Visible = false;
                    BtnPareto.Visible = false;
                    btnLampiran.Value = "Form";
                    PanelLampiran.Visible = true;
                    LoadLampiran(sarmutnotxt.Text);
                }
                else
                {
                    AnalisaPanel.Visible = true;
                    btnNew.Visible = true;
                    btnUpdate.Visible = true;
                    btnRekap.Visible = true;
                    btnList.Visible = true;
                    BtnPareto.Visible = true;
                    btnLampiran.Value = "Lampiran";
                    PanelLampiran.Visible = false;
                    btnRekap.Visible = false;
                }
            }
        }
        protected void BtnPareto_ServerClick(object sender, EventArgs e)
        {
            if (sarmutnotxt.Text.Trim() != string.Empty)
            {
                if (BtnPareto.Value == "Pareto")
                {
                    AnalisaPanel.Visible = false;
                    btnNew.Visible = false;
                    btnUpdate.Visible = false;
                    btnRekap.Visible = false;
                    btnList.Visible = false;
                    btnLampiran.Visible = false;
                    BtnPareto.Value = "Form";
                    PanelPareto.Visible = true;
                    LoadPareto(sarmutnotxt.Text);
                }
                else
                {
                    AnalisaPanel.Visible = true;
                    btnNew.Visible = true;
                    btnUpdate.Visible = true;
                    btnRekap.Visible = true;
                    btnList.Visible = true;
                    btnLampiran.Visible = true;
                    BtnPareto.Value = "Pareto";
                    PanelPareto.Visible = false;
                    btnRekap.Visible = false;
                }
            }
        }

        protected void GridLampiran_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName == "lihat")
            {

                PriviewPdf(this, GridLampiran.Rows[rowindex].Cells[0].Text);
            }
            if (e.CommandName == "hapus")
            {
                FacadePrsaX lampiranF = new FacadePrsaX();
                string err = lampiranF.hapus(GridLampiran.Rows[rowindex].Cells[0].Text);
                LoadLampiran(sarmutnotxt.Text);
            }
        }

        protected void chk_CheckedChangePrs(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            

            if (chk.Checked == true)
            {

                btnHapus.Disabled = false;

            }
            else if (chk.Checked == false)
            {
                btnHapus.Disabled = true;
            }
        }

        //private void kontrolBtnHapus()
        //{
        //    FacadePrsaX p = new FacadePrsaX();
        //    string nilai = p.CekApproval(sarmutnotxt.Text);
        //    if (nilai == "0")
        //    {
        //        btnHapus.Visible = true;
        //    }
        //    else
        //    {
        //        btnHapus.Visible = false;
        //    }
        //}

        protected void btnHapus_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < attachPrs.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)attachPrs.Items[i].FindControl("chkprs");
                if (chk.Checked == true)
                {
                    ZetroView zl = new ZetroView();
                    zl.QueryType = Operation.CUSTOM;
                    zl.CustomQuery = "Update SPD_Attachment_AP set RowStatus=-1 where ID=" + chk.ToolTip.ToString();
                    SqlDataReader sdr = zl.Retrieve();
                    LoadLampiran(sarmutnotxt.Text);
                    //btnHapus.Disabled = true;
                }
            }
        }

        static public void PriviewPdf(Control page, string ID)
        {
            string myScript = "var wn = window.showModalDialog('../../ModalDialog/PdfPreviewTPP.aspx?ba=" + ID + "', 'Preview', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void btnUpload0_ServerClick(object sender, EventArgs e)
        {
            LoadLampiran(sarmutnotxt.Text);
        }

        protected void GridLampiran_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //try
            //{
            //    LinkButton btn = (LinkButton)e.Row.Cells[3].Controls[0];
            //    if (chkClose.Checked == true)
            //        btn.Enabled = false;
            //    else
            //        btn.Enabled = true;
            //}
            //catch { }
        }

        protected void LoadPareto(string Laporan_no)
        {
            ArrayList arrLampiran = new ArrayList();
            FacadePrsaX facaXlamp = new FacadePrsaX();
            arrLampiran = facaXlamp.RetrieveParetoByNo(Laporan_no);
            //GridLampiran.DataSource = arrLampiran;
            //GridLampiran.DataBind();
            attachPareto.DataSource = arrLampiran;
            attachPareto.DataBind();
        }
        protected void LoadLampiran(string Laporan_no)
        {
            ArrayList arrLampiran = new ArrayList();
            FacadePrsaX facaXlamp = new FacadePrsaX();
            arrLampiran = facaXlamp.RetrieveLampiranByNo(Laporan_no);
            GridLampiran.DataSource = arrLampiran;
            GridLampiran.DataBind();
            attachPrs.DataSource = arrLampiran;
            attachPrs.DataBind();
            //kontrolBtnHapus();
        }
        
        protected void btnUpload_ServerClick(object sender, EventArgs e)
        {
            SPD_PrsAx spdx = new SPD_PrsAx();
            FacadePrsaX fcx = new FacadePrsaX();
            spdx = fcx.RetrieveByNo(sarmutnotxt.Text);
            UploadPdf(this, spdx.ID.ToString());
        }

        static public void UploadPdf(Control page, string ID)
        {
            string myScript = "var wn = window.showModalDialog('../../ModalDialog/UploadFileAnalisaSP.aspx?ba=" + ID + "', 'Preview', 'resizable:yes;dialogHeight: 200px; dialogWidth: 900px;scrollbars=no');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void BtnList_ServerClick(object sender, EventArgs e)
        {
            if (btnList.Value == "List")
            {
                AnalisaPanel.Visible = false;
                btnNew.Visible = false;
                btnUpdate.Visible = false;
                //btnRekap.Visible = false;
                btnList.Value = "Form";
                PanelList.Visible = true;
                PanelPebaikan.Visible = false;
                PanelPencegahan.Visible = false;
                LoadSMT();
            }
            else
            {
                AnalisaPanel.Visible = true;
                btnNew.Visible = true;
                btnUpdate.Visible = true;
                btnRekap.Visible = false;
                btnList.Value = "List";
                PanelList.Visible = false;
            }
            ddlTahun0.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            ddlDeptName1.SelectedIndex = 0;
            ddlBulan0.SelectedIndex = 0;
            if (ddlDeptName1.Items.Count == 1)
                ddlDeptName0_SelectedIndexChanged(null, null);
        }

        protected void ddlTahun_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsarmutDept.SelectedIndex > 0)
            {
                FacadePrsaX deptx = new FacadePrsaX();
                SPD_PrsAx xxx = new SPD_PrsAx();
                xxx = deptx.RetrieveById(int.Parse(ddlBulan.SelectedValue), (int.Parse(ddlTahun.SelectedValue)), (int.Parse(ddlsmtP.SelectedValue)), (int.Parse(ddlsarmutDept.SelectedValue)));
                txtTargetx.Text = xxx.Target.ToString();
                txtActual.Text = xxx.Actual.ToString();
                txtSatuan.Text = xxx.Satuan.ToString();
                txtParam.Text = xxx.Param.ToString();
                Label1.Text = xxx.Satuan.ToString();
            }
        }

        protected void LoadSMT()
        {
            SPD_PrsAx spdzx = new SPD_PrsAx();
            FacadePrsaX fcdxx = new FacadePrsaX();
            ArrayList smtfc = new ArrayList();

            Users user = ((Users)Session["Users"]);
            SPD_PrsAx spdxx = new SPD_PrsAx();
            FacadePrsaX fcadsx = new FacadePrsaX();
            spdxx = fcadsx.GetUserDept(user.ID);
            if (spdxx.Dept.Trim() == "IT" || spdxx.Dept.Trim() == "ISO")
            {
                smtfc = fcadsx.RetrieveAll();
                Session["smtfc"] = smtfc;
                GridSMT.DataSource = smtfc;
                GridSMT.DataBind();
                lstAnalisaData.DataSource = smtfc;
                lstAnalisaData.DataBind();

            }
            else
            {
                LoadSMTyDept(spdxx.Dept);
            }
        }

        protected void btnPrint0_ServerClick(object sender, EventArgs e)
        {
            SPD_PrsAx spdx = new SPD_PrsAx();
            FacadePrsaX fcx = new FacadePrsaX();
            spdx = fcx.RetrieveByNo(sarmutnotxt.Text);
            //Cetak(this, spdx.ID.ToString());
        }

        protected void BtnPrint_ServerClick(object sender, EventArgs e)
        {
            #region old
            Users users = (Users)Session["Users"]; string Nomor = string.Empty;
            string strQuery = string.Empty; string strQuery1 = string.Empty; string strQuery2 = string.Empty; string strQuery3 = string.Empty; string strQuery4 = string.Empty; string strQuery3a = string.Empty; string strQuery4a = string.Empty;
            strQuery = " select A.DeptID,A.CreatedTime,A.ID,isnull(C.Dept,'')Dept,A.AnNo,A.TglAnalisa,A.SarmutPID,B.SarMutPerusahaan,A.SarmutDeptID, " +
                              " isnull(D.SarmutDepartemen,'')SarmutDepartemen,A.TargetVID,A.ParamID,A.SatuanID,A.TypeSarmutID,E.JenisSarmut Jenis," +
                              " A.Bulan,case when A.Bulan=1 then 'Jan' when A.Bulan=2 then 'Feb' when A.Bulan=3 then 'Mar' when A.Bulan=4 then 'Apr' when A.Bulan=5 then 'Mei' " +
                              " when A.Bulan=6 then 'Jun' when A.Bulan=7 then 'Jul' when A.Bulan=8 then 'Agust' when A.Bulan=9 then 'Sept' " +
                              " when A.Bulan=10 then 'Okt' when A.Bulan=11 then 'Nov' when A.Bulan=12  then 'Des' end Bulan1,A.Semester, " +
                              " A.Tahun,A.Actual,case when A.Kesim=1 then 'P' else ' ' end [tcp], case when A.Kesim=2 then 'P' else ' ' end [ttcp],A.Kesim,A.RowStatus," +
                              " case when AnNo<>'' then (select COUNT(ID) from SPD_Analisa_Penyebab_Detail where SPDAnalisaID=A.ID and Penyebab_ID=1 and RowStatus>-1) else 0 end PLin, " +
                              " case when AnNo<>'' then (select COUNT(ID) from SPD_Analisa_Penyebab_Detail where SPDAnalisaID=A.ID and Penyebab_ID=2 and RowStatus>-1) else 0 end PMan, " +
                              " case when AnNo<>'' then (select COUNT(ID) from SPD_Analisa_Penyebab_Detail where SPDAnalisaID=A.ID and Penyebab_ID=3 and RowStatus>-1) else 0 end PMat, " +
                              " case when AnNo<>'' then (select COUNT(ID) from SPD_Analisa_Penyebab_Detail where SPDAnalisaID=A.ID and Penyebab_ID=4 and RowStatus>-1) else 0 end PMes, " +
                              " case when AnNo<>'' then (select COUNT(ID) from SPD_Analisa_Penyebab_Detail where SPDAnalisaID=A.ID and Penyebab_ID=5 and RowStatus>-1) else 0 end PMet, " +
                              " case when AnNo<>'' then isnull((select URAIAN from SPD_Analisa_Penyebab_Detail where SPDAnalisaID=A.ID and Penyebab_ID=1 and RowStatus>-1),'') end UPLin, " +
                              " case when AnNo<>'' then isnull((select URAIAN from SPD_Analisa_Penyebab_Detail where SPDAnalisaID=A.ID and Penyebab_ID=2 and RowStatus>-1),'') end UPMan, " +
                              " case when AnNo<>'' then isnull((select URAIAN from SPD_Analisa_Penyebab_Detail where SPDAnalisaID=A.ID and Penyebab_ID=3 and RowStatus>-1),'') end UPMat, " +
                              " case when AnNo<>'' then isnull((select URAIAN from SPD_Analisa_Penyebab_Detail where SPDAnalisaID=A.ID and Penyebab_ID=4 and RowStatus>-1),'') end UPMes, " +
                              " case when AnNo<>'' then isnull((select URAIAN from SPD_Analisa_Penyebab_Detail where SPDAnalisaID=A.ID and Penyebab_ID=5 and RowStatus>-1),'') end UPMet, " +
                              " (select COUNT(ID)verified from SPD_Tindakan where Verifikasi=1 and SPDAnalisaID=A.ID)verified, " +
                              " (select COUNT(ID)verified from SPD_Tindakan where Verifikasi=0 and SPDAnalisaID=A.ID)notverified, " +
                              " isnull((select top 1 Jadwal_Selesai from SPD_Tindakan where SPDAnalisaID =A.ID and RowStatus>-1 order by Jadwal_Selesai desc),'1/1/1900') due_date, A.Apv " +
                              " from SPD_Analisa A  left join SPD_Perusahaan B on A.SarmutPID=B.ID left join SPD_Dept C on A.Deptid=C.ID " +
                              " left join SPD_Departemen D on A.SarmutDeptID=D.ID inner join SPD_Type E on A.TypeSarmutID=E.ID " +
                              " where A.AnNo = '" + sarmutnotxt.Text.Trim() + "'  order by A.TglAnalisa,C.Dept ";

            strQuery1 = " select Tindakan, Pelaku,Jadwal_selesai,case when DAY(Jadwal_selesai) between 1 and 7 then 'M1' when DAY(Jadwal_Selesai) between 8 and 14 then 'M2' " +
                              " when DAY(Jadwal_Selesai) between 15 and 21 then 'M3' else 'M4' end w,MONTH(jadwal_selesai)m,YEAR(jadwal_selesai)y,Aktual_selesai Aktual, " +
                              " case when DAY(Aktual_selesai) between 1 and 7 then 'M1' when DAY(Aktual_selesai) between 8 and 14 then 'M2' " +
                              " when DAY(Aktual_selesai) between 15 and 21 then 'M3' else 'M4' end w1,MONTH(Aktual_selesai)m1,YEAR(jadwal_selesai)y1," +
                              " case when Verifikasi=1 then 'P' else ' ' end Verifikasi,tglVerifikasi Tanggal from SPD_Tindakan " +
                              " where SPDAnalisaID in (select ID from SPD_Analisa where AnNo='" + sarmutnotxt.Text.Trim() + "') and Jenis='perbaikan' and RowStatus>-1";

            strQuery2 = " select Tindakan, Pelaku,Jadwal_selesai,case when DAY(Jadwal_selesai) between 1 and 7 then 'M1' when DAY(Jadwal_Selesai) between 8 and 14 then 'M2' " +
                              " when DAY(Jadwal_Selesai) between 15 and 21 then 'M3' else 'M4' end w,MONTH(jadwal_selesai)m,YEAR(jadwal_selesai)y,Aktual_selesai Aktual, " +
                              " case when DAY(Aktual_selesai) between 1 and 7 then 'M1' when DAY(Aktual_selesai) between 8 and 14 then 'M2' " +
                              " when DAY(Aktual_selesai) between 15 and 21 then 'M3' else 'M4' end w1,MONTH(Aktual_selesai)m1,YEAR(jadwal_selesai)y1," +
                              " case when Verifikasi=1 then 'P' else ' ' end Verifikasi,tglVerifikasi Tanggal from SPD_Tindakan " +
                              " where SPDAnalisaID in (select ID from SPD_Analisa where AnNo='" + sarmutnotxt.Text.Trim() + "') and Jenis='pencegahan' and RowStatus>-1";


            strQuery3 = " select B.SarmutDepartemen Sarmut,A.Actual,A.Bulan,A.Tahun From SPD_Trans A " +
                        " inner join SPD_Departemen B on B.ID=A.SarmutDeptID " +
                        " inner join SPD_Perusahaan C on C.ID=B.SarmutPID " +
                        " where SarmutDeptID='" + IDx.Text + "' and Tahun='" + Convert.ToInt32(ddlTahun.SelectedValue.ToString()) + "'  " +
                        " and A.Bulan <= 6  and A.RowStatus>-1 order by Bulan ";
            strQuery3a = " select B.SarmutDepartemen Sarmut,A.Actual,A.Bulan,A.Tahun From SPD_Trans A " +
                         " inner join SPD_Departemen B on B.ID=A.SarmutDeptID " +
                         " inner join SPD_Perusahaan C on C.ID=B.SarmutPID " +
                         " where SarmutDeptID='" + IDx.Text + "' and Tahun='" + Convert.ToInt32(ddlTahun.SelectedValue.ToString()) + "'  " +
                         " and A.Bulan >= 7  and A.RowStatus>-1 order by Bulan ";

            strQuery4 = " select B.SarMutPerusahaan Sarmut,A.Actual,A.Bulan,A.Tahun From SPD_TransPrs A " +
                        " inner join SPD_Perusahaan B on B.ID=A.SarmutPID " +
                        " where A.SarmutPID='" + typeID.Text + "' and A.Tahun='" + Convert.ToInt32(ddlTahun.SelectedValue.ToString()) + "'   and A.Bulan <= 6 and A.RowStatus>-1 order by Bulan ";
            strQuery4a = " select B.SarMutPerusahaan Sarmut,A.Actual,A.Bulan,A.Tahun From SPD_TransPrs A " +
                       " inner join SPD_Perusahaan B on B.ID=A.SarmutPID " +
                       " where A.SarmutPID='" + typeID.Text + "' and A.Tahun='" + Convert.ToInt32(ddlTahun.SelectedValue.ToString()) + "'   and A.Bulan >= 7 and A.RowStatus>-1 order by Bulan ";

            if (sarmutnotxt.Text.Trim() != string.Empty)
            {
                Session["Query"] = strQuery;
                Session["Query1"] = strQuery1;
                Session["Query2"] = strQuery2;
                Session["Query4"] = strQuery4;
                if (Convert.ToInt32(IDx.Text) > 0)
                {
                    if (Convert.ToInt32(txtbulan.Text) <= 6)
                    {
                        Session["Query3"] = strQuery3;
                    }
                    else
                    {
                        Session["Query3a"] = strQuery3a;
                    }

                }
                else
                {
                    if (Convert.ToInt32(txtbulan.Text) <= 6)
                    {
                        Session["Query4"] = strQuery4;
                    }
                    else
                    {
                        Session["Query4a"] = strQuery4a;
                    }

                }
                if (users.UnitKerjaID == 1)
                { Nomor = "ISO/ADPS/18/02/R4"; Session["Nomor"] = Nomor.Trim(); }
                else if (users.UnitKerjaID == 7)
                { Nomor = "ISO/K/ADPS/18/02/R4"; Session["Nomor"] = Nomor.Trim(); }
                else if (users.UnitKerjaID == 13)
                { Nomor = "ISO/J/ADPS/13/20"; Session["Nomor"] = Nomor.Trim(); }

                #endregion
                Cetak(this);
            }
        }

        static public void Cetak(Control page)
        {
            string myScript = "var wn = window.showModalDialog('Report.aspx?IdReport=ana', '', 'resizable:yes;dialogHeight: 750px; dialogWidth: 950px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        static public void Cetak2(Control page, string ID)
        {
            string myScript = "var wn = window.showModalDialog('ReportNewAnalisaData.aspx?p='" + ID + "', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private void panelcolour()
        {
            if (chkLingkungan.Checked == true)
            {
                chkLingkungan.ForeColor = System.Drawing.Color.White;
                Panel14.BackImageUrl = ResolveUrl("~/images/ellipse_L.png");
            }
            else
            {
                chkLingkungan.ForeColor = System.Drawing.Color.Black;
                Panel14.BackImageUrl = string.Empty;
            }
            if (chkManusia.Checked == true)
            {
                chkManusia.ForeColor = System.Drawing.Color.White;
                Panel13.BackImageUrl = ResolveUrl("~/images/ellipse_L.png");
            }
            else
            {
                chkManusia.ForeColor = System.Drawing.Color.Black;
                Panel13.BackImageUrl = string.Empty;
            }
            if (chkMesin.Checked == true)
            {
                chkMesin.ForeColor = System.Drawing.Color.White;
                Panel15.BackImageUrl = ResolveUrl("~/images/ellipse_L.png");
            }
            else
            {
                chkMesin.ForeColor = System.Drawing.Color.Black;
                Panel15.BackImageUrl = string.Empty;
            }
            if (chkMaterial.Checked == true)
            {
                chkMaterial.ForeColor = System.Drawing.Color.White;
                Panel17.BackImageUrl = ResolveUrl("~/images/ellipse_L.png");
            }
            else
            {
                chkMaterial.ForeColor = System.Drawing.Color.Black;
                Panel17.BackImageUrl = string.Empty;
            }
            if (chkMetode.Checked == true)
            {
                chkMetode.ForeColor = System.Drawing.Color.White;
                Panel16.BackImageUrl = ResolveUrl("~/images/ellipse_L.png");
            }
            else
            {
                chkMetode.ForeColor = System.Drawing.Color.Black;
                Panel16.BackImageUrl = string.Empty;
            }
        }


        protected void LoadSMTyDept(string dep)
        {
            SPD_PrsAx spdx = new SPD_PrsAx();
            FacadePrsaX fspdx = new FacadePrsaX();
            ArrayList arrspdxq = new ArrayList();
            string kriteria = string.Empty;

            kriteria = " and A.deptID in (select ID from SPD_dept where dept='" + dep + "')";
            arrspdxq = fspdx.RetrieveByKriteria(kriteria);
            GridSMT.DataSource = arrspdxq;
            GridSMT.DataBind();
        }

        protected void GridSMT_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridSMT.PageIndex = e.NewPageIndex;
            LoadSMT();
        }

        protected void GridSMT_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
                string tes = GridSMT.Rows[rowindex].Cells[2].Text;
                DataSMT(GridSMT.Rows[rowindex].Cells[2].Text);
                if (btnList.Value == "List")
                {
                    AnalisaPanel.Visible = false;
                    btnNew.Visible = false;
                    btnUpdate.Visible = false;
                    btnUpdate.Disabled = false;
                    btnList.Value = "Form";
                    PanelList.Visible = true;
                }
                else
                {
                    AnalisaPanel.Visible = true;
                    btnNew.Visible = true;
                    btnUpdate.Disabled = true;
                    btnUpdate.Visible = true;
                    btnList.Value = "List";
                    PanelList.Visible = false;
                }
            }
            //if (e.CommandName == "Print")
            //{
            //    //int index = Convert.ToInt32(e.CommandArgument.ToString());
            //    // GridViewRow row = GridSMT.Rows[index];//GrdDynamic.Rows[index];
            //    if (e.CommandName == "Print")
            //    {
            //        int id = Convert.ToInt32(e.CommandArgument);
            //        Cetak2(this);
            //    }
            //}
        }


        protected void GridSMT_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[12].Text == "0")
                {
                    e.Row.Cells[12].Text = "Open";
                }
                if (e.Row.Cells[12].Text == "1")
                {
                    e.Row.Cells[12].Text = "Close";
                }
                if (e.Row.Cells[13].Text == "0")
                {
                    e.Row.Cells[13].Text = "UnSloved";
                }
                if (e.Row.Cells[13].Text == "1")
                {
                    e.Row.Cells[13].Text = "Solved";
                }
                if (e.Row.Cells[14].Text == "0")
                {
                    e.Row.Cells[14].Text = "X";
                }
                if (e.Row.Cells[14].Text == "1")
                {
                    e.Row.Cells[14].Text = "Y";
                }
                if (e.Row.Cells[14].Text == "2")
                {
                    e.Row.Cells[14].Text = "Y";
                }
                if (e.Row.Cells[14].Text == "3")
                {
                    e.Row.Cells[14].Text = "Y";
                }
                if (e.Row.Cells[14].Text == "4")
                {
                    e.Row.Cells[14].Text = "Y";
                }
                if (e.Row.Cells[14].Text == "5")
                {
                    e.Row.Cells[14].Text = "Y";
                }
                if (e.Row.Cells[19].Text == "01/01/1900")
                {
                    e.Row.Cells[19].Text = "";
                }

            }

            if (((Users)Session["Users"]).DeptID == 14)
            {
                GridSMT.Columns[18].Visible = true;
            }
            else
            {
                GridSMT.Columns[18].Visible = false;
            }
        }

        protected void ddlDeptName0_SelectedIndexChanged(object sender, EventArgs e)
        {
            SPD_PrsAx tpp = new SPD_PrsAx();
            FacadePrsaX tppf = new FacadePrsaX();
            ArrayList arrtpp = new ArrayList();

            Users user = ((Users)Session["Users"]);
            FacadePrsaX tppFacade = new FacadePrsaX();
            //LoadTPP();
            SPD_PrsAx dpt = new SPD_PrsAx();
            FacadePrsaX dptf = new FacadePrsaX();
            dpt = dptf.GetUserDept(user.ID);
            int ketemu = 0;

            if (dpt.Dept.Trim() != "IT" && dpt.Dept.Trim() != "ISO" && ddlDeptName1.Items.Count>1)
            {
                for (int i = 1; i <= ddlDeptName1.Items.Count; i++)
                {
                    ddlDeptName1.SelectedIndex = i;
                    if (ddlDeptName1.SelectedItem.Text.Trim().ToUpper() == dpt.Dept.Trim().ToUpper())
                    {
                        ketemu = 1;
                        break;
                    }
                }
                if (ketemu == 0)
                    ddlDeptName1.SelectedIndex = 0;
            }
            LoadSMTByKriteria();
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

        protected void LoadSMTByKriteria()
        {
            //select * from SPD_Dept where RowStatus>-1 and dptID in (" + users.DeptID + ") order by dept";
            string Kriteria = string.Empty;
            Users users = (Users)Session["Users"];
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            if (ddlDeptName1.SelectedItem.Text.Trim() != "ALL")
                Kriteria = " and A.deptID in (select ID from SPD_dept where dept='" + ddlDeptName1.SelectedItem.Text + "')";
            if (ddlTahun0.SelectedItem.Text.Trim() != "ALL")
                Kriteria = Kriteria + " and left(convert(char,A.TglAnalisa,112),4)=" + ddlTahun0.SelectedItem.Text;
            if (ddlStatus.SelectedItem.Text.Trim() != "ALL")
                Kriteria = Kriteria + "  and isnull(A.CLosed,0)=" + ddlStatus.SelectedItem.Value;
            if (ddlBulan0.SelectedItem.Text.Trim() != "ALL")
                Kriteria = Kriteria + " and month(A.TglAnalisa)=" + ddlBulan0.SelectedIndex;
            if (ddlsmtList.SelectedItem.Text.Trim() != "ALL")
                Kriteria = Kriteria + " and A.Semester in ( '" + ddlsmtList.SelectedItem.Text + "')";
            zl.CustomQuery = " select A.DeptID,A.CreatedTime,A.ID ID,isnull(C.Dept,'')Dept,A.AnNo,A.TglAnalisa,A.SarmutPID,B.SarMutPerusahaan,A.SarmutDeptID, " +
                            " isnull(D.SarmutDepartemen,'')SarmutDepartemen,A.TargetVID,A.ParamID,A.SatuanID,A.TypeSarmutID,E.JenisSarmut Jenis,A.Apv," +
                            " case when Apv=0 then 'Admin' when apv is null then 'Admin' " +
                            " when apv=1 then 'Manager Dept' when apv=2 then 'ISO' end Approval,isnull(A.CLosed,0)Closed,isnull(A.Close_Date,'1/1/1900')Close_Date," +
                            " isnull(A.Solved,0)Solved,isnull(A.Solve_Date,'1/1/1900')Solve_Date," +
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
                            " end NamaBulan , A.Semester" +
                            " ,A.Tahun,A.Actual,case when A.Kesim=1 then 'Tercapai' else 'Tidak Tercapai' end Ket,A.Kesim,A.RowStatus," +
                            " (select COUNT(ID)verified from SPD_Tindakan where Verifikasi=1 and SPDAnalisaID=A.ID)Verifikasi," +
                            " (select COUNT(ID)verified from SPD_Tindakan where Verifikasi=0 and SPDAnalisaID=A.ID)notverified," +
                            " isnull((select top 1 Jadwal_Selesai from SPD_Tindakan where SPDAnalisaID =A.ID and RowStatus>-1 order by Jadwal_Selesai desc),'1/1/1900') due_date,isnull(apvmgr,'1/1/1900')apvmgr " +
                            " from SPD_Analisa A  left join SPD_Perusahaan B on A.SarmutPID=B.ID left join SPD_Dept C on A.Deptid=C.ID " +
                            " left join SPD_Departemen D on A.SarmutDeptID=D.ID inner join SPD_Type E on A.TypeSarmutID=E.ID " +
                            " where A.RowStatus>-1 " + Kriteria + " order by A.TglAnalisa Desc,C.Dept ";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new SPD_PrsAx
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        CLosed = Convert.ToInt32(sdr["CLosed"].ToString()),
                        Dept = sdr["Dept"].ToString(),
                        AnNo = sdr["AnNo"].ToString(),
                        TglAnalisa = Convert.ToDateTime(sdr["TglAnalisa"].ToString()),
                        Tahun = Convert.ToInt32(sdr["Tahun"].ToString()),
                        Bulan = Convert.ToInt32(sdr["Bulan"].ToString()),
                        NamaBulan = sdr["NamaBulan"].ToString(),
                        Semester = sdr["Semester"].ToString(),
                        SarmutDepartemen = sdr["SarmutDepartemen"].ToString(),
                        SarMutPerusahaan = sdr["SarmutPerusahaan"].ToString(),
                        Actual = Convert.ToDecimal(sdr["Actual"].ToString()),
                        TargetVID = sdr["TargetVID"].ToString(),
                        Due_Date = Convert.ToDateTime(sdr["Due_Date"]),
                        Jenis = sdr["Jenis"].ToString(),
                        Ket = sdr["Ket"].ToString(),
                        Approval = sdr["Approval"].ToString(),
                        Solved = Convert.ToInt32(sdr["Solved"].ToString()),
                        ApvMgr = Convert.ToDateTime(sdr["ApvMgr"].ToString()),
                    });
                }
            }
            GridSMT.DataSource = arrData;
            GridSMT.DataBind();
            lstAnalisaData.DataSource = arrData;
            lstAnalisaData.DataBind();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=ListAnalisaData.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<H2>LIST DATA ANALISA DAN PEMANTAUAN</H2>";
            Html += "<br>Periode : " + ddlPeriodeBulan.SelectedItem.Text + " &nbsp; " + ddlPeriodeTahun.SelectedValue.ToString();
            Html += "<br>Departement : &nbsp;" + ddlDeptRekap.SelectedItem.Text;
            Html += "";
            string HtmlEnd = "";
            div3.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0\"", "border=\"1\"");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }

        //public override void VerifyRenderingInServerForm(Control control)
        //{
        //    //return;
        //}

        protected void DataSMT(string AnNo)
        {
            ArrayList arrsmt = new ArrayList();
            SPD_PrsAx spdsh = new SPD_PrsAx();
            FacadePrsaX fspdsh = new FacadePrsaX();
            spdsh = fspdsh.RetrieveByNo(AnNo);
            arrsmt.Add(spdsh);
            GridSMT.DataSource = arrsmt;
            GridSMT.DataBind();
            ClearForm();
            sarmutnotxt.Text = spdsh.AnNo;
            sarmutNo.Text = spdsh.AnNo;
            sarmutNoP.Text= spdsh.AnNo;
            sarmutID.Text = spdsh.ID.ToString(); 
            txtAsarmut_Date0.SelectedDate = spdsh.TglAnalisa;
            for (int i = ddlDeptName.Items.Count - 1; i > 0; i--)
            {
                if (ddlDeptName.Items[i].Text.Contains(spdsh.Dept))
                {
                    ddlDeptName.Items[i].Selected = true;
                    break;
                }
            }
            //printEx.Visible = true;
            //btnCetak.Visible = true;
            ddlDeptName.SelectedItem.Text = spdsh.Dept;
            ddlsmtP.SelectedItem.Text = spdsh.SarMutPerusahaan;
            ddlsmtP.Enabled = false;
            ddlsarmutDept.Visible = false;
            xxx.Text = spdsh.SarmutDepartemen;
            typeID.Text = spdsh.SarmutPID.ToString();
            IDx.Text = spdsh.SarmutDeptID.ToString();
            IDa.Text = spdsh.ID.ToString();
            Label1.Text = spdsh.TargetVID;
            txtParam.Text = spdsh.ParamID;
            txttahun.Text = spdsh.Tahun.ToString();
            bulanSmT0.Text = spdsh.Bulan.ToString();
            //txtbulan.Text = spdsh.Bulan.ToString();
            ddlTahun.SelectedItem.Text = spdsh.Tahun.ToString();

            if (spdsh.Bulan == 0)
            {
                ddlBulan.Visible = false;
                ddlSemester.SelectedItem.Text = spdsh.Semester.ToString();
                ddlSemester.Visible = true;
                ddlSemester.Enabled = false;
            }
            else
            {
                ddlBulan.SelectedItem.Text = spdsh.NamaBulan.ToString();
                ddlSemester.Visible = false;
                ddlBulan.Visible = true;
            }
            txtbulan.Text = spdsh.Bulan.ToString();
            txtActual.Text = spdsh.Actual.ToString();
            txtSatuan.Text = spdsh.SatuanID;
            chkTTercapai.Checked = false;
            chkTercapai.Checked = false;
            if (spdsh.Kesim == 2)
                chkTTercapai.Checked = true;
            if (spdsh.Kesim == 1)
                chkTercapai.Checked = true;

            LoadPerbaikan(AnNo);
            LoadPencegahan(AnNo);
            ArrayList arrPenyebab = new ArrayList();
            FacadePrsaX facadeP = new FacadePrsaX();
            arrPenyebab = facadeP.RetrieveByNo2(AnNo);
            foreach (SPD_PrsAx Pdetail in arrPenyebab)
            {
                if (Pdetail.Penyebab.Trim() == "Lingkungan")
                {
                    chkLingkungan.Checked = true;
                    txtLingkungan.Enabled = true;
                    txtLingkungan.Text = Pdetail.Uraian;
                }
                if (Pdetail.Penyebab.Trim() == "Manusia")
                {
                    chkManusia.Checked = true;
                    txtManusia.Enabled = true;
                    txtManusia.Text = Pdetail.Uraian;
                }
                if (Pdetail.Penyebab.Trim() == "Material")
                {
                    chkMaterial.Checked = true;
                    txtMaterial.Enabled = true;
                    txtMaterial.Text = Pdetail.Uraian;
                }
                if (Pdetail.Penyebab.Trim() == "Mesin")
                {
                    chkMesin.Checked = true;
                    txtMesin.Enabled = true;
                    txtMesin.Text = Pdetail.Uraian;
                }
                if (Pdetail.Penyebab.Trim() == "Metode")
                {
                    chkMetode.Checked = true;
                    txtMetode.Enabled = true;
                    txtMetode.Text = Pdetail.Uraian;
                }
            }
            if (spdsh.Apv > 1)
                btnUpdate.Disabled = true;
            else
                btnUpdate.Disabled = false;
            panelcolour();
            if (Session["usertype"].ToString().Trim().ToUpper() == "ISO")
            {
                btnUpdate.Disabled = false;
                PanelStatus.Enabled = true;
                //chkClose.Enabled = true;
                //txtDateTKasus.Enabled = true;
                chksolved.Enabled = true;
                txtDateSolved.Enabled = true;
                txtDueDate.Enabled = true;
            }
            else
            {
                PanelStatus.Enabled = false;
                //chkClose.Enabled = false;
                //txtDateTKasus.Enabled = false;
                chksolved.Enabled = false;
                txtDateSolved.Enabled = false;
                txtDueDate.Enabled = false;
            }
            if (spdsh.Solved == 1)
            {
                chksolved.Checked = true;
                txtDateSolved.SelectedDate = spdsh.Solve_Date;
                txtDueDate.SelectedDate = spdsh.Due_Date;
            }
            else
            {
                chksolved.Checked = false;
                txtDateSolved.SelectedValue = string.Empty;
                txtDueDate.SelectedValue = string.Empty;
            }
            if (bulanSmT0.Text == "0")
            {
                LoadDatasmt1R2((int.Parse(txttahun.Text.ToString())), (int.Parse(typeID.Text.ToString())), (int.Parse(IDx.Text.ToString())));
            }
            else
            {
                LoadDatasmt1((int.Parse(txttahun.Text.ToString())), (int.Parse(typeID.Text.ToString())), (int.Parse(IDx.Text.ToString())));
            }
            ddlBulan.Enabled = false;
            ddlTahun.Enabled = false;
            chkTercapai.Enabled = false;
            chkTTercapai.Enabled = false;

        }

        protected void DataSMTx(string AnNo)
        {
            ArrayList arrsmt = new ArrayList();
            SPD_PrsAx spdsh = new SPD_PrsAx();
            FacadePrsaX fspdsh = new FacadePrsaX();
            spdsh = fspdsh.RetrieveByNo(AnNo);
            arrsmt.Add(spdsh);
            GridSMT.DataSource = arrsmt;
            GridSMT.DataBind();
            ClearForm();
            sarmutnotxt.Text = spdsh.AnNo;
            sarmutNo.Text = spdsh.AnNo;
            txtAsarmut_Date0.SelectedDate = spdsh.TglAnalisa;
            for (int i = ddlDeptName.Items.Count - 1; i > 0; i--)
            {
                if (ddlDeptName.Items[i].Text.Contains(spdsh.Dept))
                {
                    ddlDeptName.Items[i].Selected = true;
                    break;
                }
            }
            ddlDeptName.SelectedItem.Text = spdsh.Dept;
            ddlsmtP.SelectedItem.Text = spdsh.SarMutPerusahaan;
            //ddlsarmutDept.SelectedItem.Text = spdsh.SarmutDepartemen;
            typeID.Text = spdsh.SarmutPID.ToString();
            IDx.Text = spdsh.SarmutDeptID.ToString();
            IDa.Text = spdsh.ID.ToString();
            Label1.Text = spdsh.TargetVID;
            txtParam.Text = spdsh.ParamID;
            txttahun.Text = spdsh.Tahun.ToString();
            //txtbulan.Text = spdsh.Bulan.ToString();
            ddlTahun.SelectedItem.Text = spdsh.Tahun.ToString();
            ddlBulan.SelectedItem.Text = spdsh.NamaBulan.ToString();

            txtbulan.Text = spdsh.Bulan.ToString();
            txtActual.Text = spdsh.Actual.ToString();
            txtSatuan.Text = spdsh.SatuanID;
            chkTTercapai.Checked = false;
            chkTercapai.Checked = false;
            if (spdsh.Kesim == 2)
                chkTTercapai.Checked = true;
            if (spdsh.Kesim == 1)
                chkTercapai.Checked = true;

            LoadPerbaikan(AnNo);
            LoadPencegahan(AnNo);
            ArrayList arrPenyebab = new ArrayList();
            FacadePrsaX facadeP = new FacadePrsaX();
            arrPenyebab = facadeP.RetrieveByNo2(AnNo);
            foreach (SPD_PrsAx Pdetail in arrPenyebab)
            {
                if (Pdetail.Penyebab.Trim() == "Lingkungan")
                {
                    chkLingkungan.Checked = true;
                    txtLingkungan.Enabled = true;
                    txtLingkungan.Text = Pdetail.Uraian;
                }
                if (Pdetail.Penyebab.Trim() == "Manusia")
                {
                    chkManusia.Checked = true;
                    txtManusia.Enabled = true;
                    txtManusia.Text = Pdetail.Uraian;
                }
                if (Pdetail.Penyebab.Trim() == "Material")
                {
                    chkMaterial.Checked = true;
                    txtMaterial.Enabled = true;
                    txtMaterial.Text = Pdetail.Uraian;
                }
                if (Pdetail.Penyebab.Trim() == "Mesin")
                {
                    chkMesin.Checked = true;
                    txtMesin.Enabled = true;
                    txtMesin.Text = Pdetail.Uraian;
                }
                if (Pdetail.Penyebab.Trim() == "Metode")
                {
                    chkMetode.Checked = true;
                    txtMetode.Enabled = true;
                    txtMetode.Text = Pdetail.Uraian;
                }
            }
            if (spdsh.Apv > 1)
                btnUpdate.Disabled = true;
            else
                btnUpdate.Disabled = false;
            panelcolour();
            if (Session["usertype"].ToString().Trim().ToUpper() == "ISO")
            {
                btnUpdate.Disabled = false;
                PanelStatus.Enabled = true;
                //chkClose.Enabled = true;
                //txtDateTKasus.Enabled = true;
                chksolved.Enabled = true;
                txtDateSolved.Enabled = true;
                txtDueDate.Enabled = true;
            }
            else
            {
                PanelStatus.Enabled = false;
                //chkClose.Enabled = false;
                //txtDateTKasus.Enabled = false;
                chksolved.Enabled = false;
                txtDateSolved.Enabled = false;
                txtDueDate.Enabled = false;
            }
            //if (spdsh.CLosed == 1)
            //{
            //    chkClose.Checked = true;
            //    txtDateTKasus.SelectedDate = spdsh.Close_Date;
            //}
            //else
            //{
            //    chkClose.Checked = false;
            //    txtDateTKasus.SelectedValue = string.Empty;
            //}
            if (spdsh.Solved == 1)
            {
                chksolved.Checked = true;
                txtDateSolved.SelectedDate = spdsh.Solve_Date;
                txtDueDate.SelectedDate = spdsh.Due_Date;
            }
            else
            {
                chksolved.Checked = false;
                txtDateSolved.SelectedValue = string.Empty;
                txtDueDate.SelectedValue = string.Empty;
            }
            //LoadDatasmt1((int.Parse(ddlTahun.SelectedValue)), (int.Parse(typeID.Text.ToString())), (int.Parse(IDx.Text.ToString())));
            LoadDatasmt1(spdsh.Tahun, (int.Parse(typeID.Text.ToString())), (int.Parse(IDx.Text.ToString())));

        }

        protected void btnClose_ServerClick(object sender, EventArgs e)
        {
            Users user = ((Users)Session["Users"]);
            FacadePrsaX ftpf = new FacadePrsaX();
            int value = 0;
            if (chkClose.Checked == true)
                value = 1;
            else
                value = 0;
            string strerror = ftpf.UpdateCloseAna(sarmutnotxt.Text, txtDateTKasus.SelectedDate.ToString("yyyyMMdd"), user.UserName.Trim(), value);
            btnClose.Visible = false;
        }

        protected void btnSolve_ServerClick(object sender, EventArgs e)
        {
            Users user = ((Users)Session["Users"]);
            FacadePrsaX ftpf = new FacadePrsaX();
            int value = 0;
            if (chksolved.Checked == true)
                value = 1;
            else
                value = 0;
            string strerror = ftpf.UpdateSolveAna(sarmutnotxt.Text, txtDateSolved.SelectedDate.ToString("yyyyMMdd"),
                txtDueDate.SelectedDate.ToString("yyyyMMdd"), user.UserName.Trim(), value);
            btnSolve.Visible = false;
        }

        protected void chkClose_CheckedChanged(object sender, EventArgs e)
        {
            btnClose.Visible = true;
            if (chkClose.Checked == true)
                btnClose.Value = "Close";
            else
                btnClose.Value = "Open";
        }

        protected void chksolved_CheckedChanged(object sender, EventArgs e)
        {
            btnSolve.Visible = true;
            txtDateSolved.SelectedDate = DateTime.Now;
            txtDueDate.SelectedDate = DateTime.Now;
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

        private void LoadTahunRekap()
        {
            ArrayList arrD = this.ListBATahun();
            ddlPeriodeTahun.Items.Clear();
            foreach (BeritaAcara ba in arrD)
            {
                ddlPeriodeTahun.Items.Add(new ListItem(ba.Tahun.ToString(), ba.Tahun.ToString()));
            }
            ddlPeriodeTahun.SelectedValue = DateTime.Now.Year.ToString();
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

        private void LoadTahun2()
        {
            ArrayList arrD = this.ListBATahun();
            ddlTahun0.Items.Clear();
            ddlTahun0.Items.Add(new ListItem("ALL", "0"));
            foreach (BeritaAcara ba in arrD)
            {
                ddlTahun0.Items.Add(new ListItem(ba.Tahun.ToString(), ba.Tahun.ToString()));
            }
            ddlTahun0.SelectedValue = DateTime.Now.Year.ToString();
        }

        protected void chkLingkungan_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLingkungan.Checked == true)
            { txtLingkungan.Enabled = true; }
            else
            { txtLingkungan.Enabled = false; }
            if (chkLingkungan.Checked == false && txtLingkungan.Text.Trim() != string.Empty)
            {
                txtLingkungan.Enabled = true;
                chkLingkungan.Checked = true;
            }
            panelcolour();
            ddlsmtDept_SelectedIndexChanged(null, null);
        }

        protected void chkMesin_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMesin.Checked == true)
            { txtMesin.Enabled = true; }
            else
            { txtMesin.Enabled = false; }
            if (chkMesin.Checked == false && txtMesin.Text.Trim() != string.Empty)
            {
                txtMesin.Enabled = true;
                chkMesin.Checked = true;
            }
            panelcolour();
            ddlsmtDept_SelectedIndexChanged(null, null);
        }

        protected void chkManusia_CheckedChanged(object sender, EventArgs e)
        {
            if (chkManusia.Checked == true)
            { txtManusia.Enabled = true; }
            else
            { txtManusia.Enabled = false; }
            if (chkManusia.Checked == false && txtManusia.Text.Trim() != string.Empty)
            {
                txtManusia.Enabled = true;
                chkManusia.Checked = true;
            }
            panelcolour();
            ddlsmtDept_SelectedIndexChanged(null, null);
        }

        protected void chkMaterial_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMaterial.Checked == true)
            { txtMaterial.Enabled = true; }
            else
            { txtMaterial.Enabled = false; }
            if (chkMaterial.Checked == false && txtMaterial.Text.Trim() != string.Empty)
            {
                txtMaterial.Enabled = true;
                chkMaterial.Checked = true;
            }
            panelcolour();
            ddlsmtDept_SelectedIndexChanged(null, null);
        }

        protected void chkMetode_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMetode.Checked == true)
            { txtMetode.Enabled = true; }
            else
            { txtMetode.Enabled = false; }
            if (chkMetode.Checked == false && txtMetode.Text.Trim() != string.Empty)
            {
                txtMetode.Enabled = true;
                chkMetode.Checked = true;
            }
            panelcolour();
            ddlsmtDept_SelectedIndexChanged(null, null);
        }

        private void ClearForm()
        {
            LoadDept();
            sarmutnotxt.Text = string.Empty;
            txtAsarmut_Date0.SelectedDate = DateTime.Now;
            Users user = ((Users)Session["Users"]);
            ddlsmtP.SelectedIndex = 0;
            ddlsarmutDept.Items.Clear();
            txtTargetx.Text = string.Empty;
            Label1.Text = string.Empty;
            txtParam.Text = string.Empty;
            txtActual.Text = string.Empty;
            txtSatuan.Text = string.Empty;
            chkTercapai.Checked = false;
            chkTTercapai.Checked = false;
            chkLingkungan.Checked = false;
            chkManusia.Checked = false;
            chkMaterial.Checked = false;
            chkMesin.Checked = false;
            chkMetode.Checked = false;
            txtLingkungan.Text = string.Empty;
            txtManusia.Text = string.Empty;
            txtMaterial.Text = string.Empty;
            txtMesin.Text = string.Empty;
            txtMetode.Text = string.Empty;
            txtLingkungan.Enabled = false;
            txtManusia.Enabled = false;
            txtMaterial.Enabled = false;
            txtMesin.Enabled = false;
            txtMetode.Enabled = false;
            btnUpdate.Disabled = false;
            LoadPerbaikan("0");
            //ddlsmtP.Enabled = true;
            //ddlTahun.Enabled = true;
            //ddlBulan.Enabled = true;
        }

        protected void BtnNew_ServerClick(object sender, EventArgs e)
        {
            ClearForm();
            Response.Redirect("APSarmut.aspx");
        }

        protected void BtnUpdate_ServerClick(object sender, EventArgs e)
        {
            if (chkTercapai.Checked == false && chkTTercapai.Checked == false)
            {
                DisplayAJAXMessage(this, "Tentukan Tercapai & Tidak Tercapai !!");
                ddlsmtDept_SelectedIndexChanged(null, null);
                return;
            }
            if (chkLingkungan.Checked == false && chkManusia.Checked == false && chkMaterial.Checked == false && chkMesin.Checked == false && chkMetode.Checked == false)
            {
                DisplayAJAXMessage(this, "Tentukan Analisa Penyebab !!");
                ddlsmtDept_SelectedIndexChanged(null, null);
                if (chkTercapai.Checked == false)
                {
                    chkTercapai.Checked = false;
                    chkTTercapai.Checked = true;
                }
                else
                {
                    chkTercapai.Checked = true;
                    chkTTercapai.Checked = false;
                }
                return;
            }
            if (txtLingkungan.Text == string.Empty && txtManusia.Text == string.Empty && txtMaterial.Text == string.Empty && txtMesin.Text == string.Empty && txtMetode.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Isi Keterangan Analisa");
                ddlsmtDept_SelectedIndexChanged(null, null);
                if (chkTercapai.Checked == false)
                {
                    chkTercapai.Checked = false;
                    chkTTercapai.Checked = true;
                }
                else
                {
                    chkTercapai.Checked = true;
                    chkTTercapai.Checked = false;
                }
                return;
            }

            if (txtTIndakan.Text.Trim() == string.Empty || txtPelaku.Text.Trim() == string.Empty)
            {
                DisplayAJAXMessage(this, "Isi TIndakan Analisa belum lengkap");
                ddlsmtDept_SelectedIndexChanged(null, null);
                if (chkTercapai.Checked == false)
                {
                    chkTercapai.Checked = false;
                    chkTTercapai.Checked = true;
                }
                else
                {
                    chkTercapai.Checked = true;
                    chkTTercapai.Checked = false;
                }
                return;
            }
            if (txtTIndakan0.Text.Trim() == string.Empty || txtPelaku0.Text.Trim() == string.Empty)
            {
                DisplayAJAXMessage(this, "Isi Pencegahan Analisa belum lengkap");
                ddlsmtDept_SelectedIndexChanged(null, null);
                if (chkTercapai.Checked == false)
                {
                    chkTercapai.Checked = false;
                    chkTTercapai.Checked = true;
                }
                else
                {
                    chkTercapai.Checked = true;
                    chkTTercapai.Checked = false;
                }
                return;
            }
            if (UploadPareto.HasFile)
            {
                string FilePath = UploadPareto.PostedFile.FileName;
                string filename = Path.GetFileName(FilePath);
                string ext = Path.GetExtension(filename);
                int ExtractPos = FilePath.LastIndexOf("\\") + 1;
                String UploadedFileName = FilePath.Substring(ExtractPos, FilePath.Length - ExtractPos);
                if (CheckAttachment(UploadedFileName, "SPD_Attachment_Pareto") == true)
                {
                    DisplayAJAXMessage(this, "Nama File Pareto sudah tersedia, silahkan ganti dengan nama file Pareto yang lain");
                    ddlsmtDept_SelectedIndexChanged(null, null);
                    return;
                }
               
            }
            else
            {
                DisplayAJAXMessage(this, "File Pareto harus dilampirkan");
                ddlsmtDept_SelectedIndexChanged(null, null);
                return;
            }

            //simpan di mulai
            string noAnalisa = string.Empty;
            FacadePrsaX fcds = new FacadePrsaX();
            FacadePrsaX fcdsx = new FacadePrsaX();
            string xxx = txtAsarmut_Date0.SelectedDate.ToString("yyyy");
            int urutan = fcds.GetLastUrutan(Convert.ToInt32(txtAsarmut_Date0.SelectedDate.ToString("yyyy"))) + 1;
            string bulanR = Global.ConvertNumericToRomawi(Convert.ToInt32(txtAsarmut_Date0.SelectedDate.ToString("MM")));
            Company company = new Company();
            CompanyFacade companyFacade = new CompanyFacade();
            string OldNo = " ";
            string kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID);
            noAnalisa = urutan.ToString().PadLeft(3, '0').Trim() + "/SMT/" + kd + "/" + bulanR + "/" + txtAsarmut_Date0.SelectedDate.ToString("yy");
            if (sarmutnotxt.Text == string.Empty)
            {
                sarmutnotxt.Text = noAnalisa;
                OldNo = noAnalisa;
            }
            else
            {
                OldNo = sarmutnotxt.Text;
                sarmutnotxt.Text = sarmutnotxt.Text.Substring(0, sarmutnotxt.Text.Trim().Length - 5) +
                sarmutnotxt.Text.Substring(sarmutnotxt.Text.Trim().Length - 3, 3);
            }
            #region #1 insert
            Users user = ((Users)Session["Users"]);
            SPD_PrsAx spdx = new SPD_PrsAx();
            int spdID = 0;
            spdx.Old_sarmutNo = OldNo;
            spdx.AnNo = sarmutnotxt.Text;
            spdx.TglAnalisa = txtAsarmut_Date0.SelectedDate;
            spdx.dptID = Convert.ToInt32(ddlDeptName.SelectedValue);
            spdx.SarmutPID = Convert.ToInt32(ddlsmtP.SelectedValue);
            spdx.SarmutDeptID = Convert.ToInt32(ddlsarmutDept.SelectedValue);
            spdx.TargetVID = txtTargetx.Text;
            spdx.ParamID = txtParam.Text;
            spdx.SatuanID = Label1.Text;
            spdx.TypeSarmutID = Convert.ToInt32(typeID.Text);
            if (RBAnalisa.Checked == true)
            {
                spdx.Tahun = Convert.ToInt32(ddlTahun.SelectedValue);
                spdx.Bulan = Convert.ToInt32(ddlBulan.SelectedValue);
                spdx.Actual = Convert.ToDecimal(txtActual.Text);
            }
            if (RBSemester.Checked == true)
            {
                spdx.Semester = ddlSemester.SelectedValue.ToString();
                spdx.Tahun = Convert.ToInt32(ddlTahun.SelectedValue);
                spdx.Actual = Convert.ToDecimal(txtSemeterAct.Text);
            }

            int Kesim = 0;
            if (chkTercapai.Checked == true)
                Kesim = 1;
            if (chkTTercapai.Checked == true)
                Kesim = 2;
            //catch { }
            spdx.Kesim = Kesim;
            spdx.CreatedBy = user.UserName;
            spdx.User_ID = user.ID;
            spdx.Semester = ddlSemester.SelectedValue.ToString();
            //int rst = 0;
            int rst = fcds.insertAnSarmut(spdx);
            if (rst > 0)
            {
                btnUpdate.Disabled = true;
                DisplayAJAXMessage(this, "Data Telah diSimpan");
            }
            spdID = rst;
            if (spdID > 0)
            {
                SPD_PrsAx pDetail = new SPD_PrsAx();
                fcdsx = new FacadePrsaX();
                if (chkLingkungan.Checked == true)
                {
                    pDetail = new SPD_PrsAx();
                    pDetail.Penyebab_ID = 1;
                    pDetail.SPDAnalisaID = spdID;
                    pDetail.Uraian = txtLingkungan.Text;
                    if (txtLingkungan.Text.Trim() != string.Empty)
                        pDetail.RowStatus = 0;
                    else
                        pDetail.RowStatus = -1;
                    rst = fcdsx.insertPdetail(pDetail);
                }
                else
                {
                    pDetail = new SPD_PrsAx();
                    pDetail.Penyebab_ID = 1;
                    pDetail.SPDAnalisaID = spdID;
                    pDetail.Uraian = txtLingkungan.Text;
                    pDetail.RowStatus = -1;
                    rst = fcdsx.insertPdetail(pDetail);
                }
                pDetail = new SPD_PrsAx();
                if (chkManusia.Checked == true)
                {
                    pDetail = new SPD_PrsAx();
                    pDetail.Penyebab_ID = 2;
                    pDetail.SPDAnalisaID = spdID;
                    pDetail.Uraian = txtManusia.Text;
                    if (txtManusia.Text.Trim() != string.Empty)
                        pDetail.RowStatus = 0;
                    else
                        pDetail.RowStatus = -1;
                    rst = fcdsx.insertPdetail(pDetail);
                }
                else
                {
                    pDetail = new SPD_PrsAx();
                    pDetail.Penyebab_ID = 2;
                    pDetail.SPDAnalisaID = spdID;
                    pDetail.Uraian = txtManusia.Text;
                    pDetail.RowStatus = -1;
                    rst = fcdsx.insertPdetail(pDetail);
                }
                if (chkMaterial.Checked == true)
                {
                    pDetail = new SPD_PrsAx();
                    pDetail.Penyebab_ID = 3;
                    pDetail.SPDAnalisaID = spdID;
                    pDetail.Uraian = txtMaterial.Text;
                    if (txtMaterial.Text.Trim() != string.Empty)
                        pDetail.RowStatus = 0;
                    else
                        pDetail.RowStatus = -1;
                    rst = fcdsx.insertPdetail(pDetail);
                }
                else
                {
                    pDetail = new SPD_PrsAx();
                    pDetail.Penyebab_ID = 3;
                    pDetail.SPDAnalisaID = spdID;
                    pDetail.Uraian = txtMaterial.Text;
                    pDetail.RowStatus = -1;
                    rst = fcdsx.insertPdetail(pDetail);
                }
                pDetail = new SPD_PrsAx();
                if (chkMesin.Checked == true)
                {
                    pDetail = new SPD_PrsAx();
                    pDetail.Penyebab_ID = 4;
                    pDetail.SPDAnalisaID = spdID;
                    pDetail.Uraian = txtMesin.Text;
                    if (txtMesin.Text.Trim() != string.Empty)
                        pDetail.RowStatus = 0;
                    else
                        pDetail.RowStatus = -1;
                    rst = fcdsx.insertPdetail(pDetail);
                }
                else
                {
                    pDetail = new SPD_PrsAx();
                    pDetail.Penyebab_ID = 4;
                    pDetail.SPDAnalisaID = spdID;
                    pDetail.Uraian = txtMesin.Text;
                    pDetail.RowStatus = -1;
                    rst = fcdsx.insertPdetail(pDetail);
                }
                if (chkMetode.Checked == true)
                {
                    pDetail = new SPD_PrsAx();
                    pDetail.Penyebab_ID = 5;
                    pDetail.SPDAnalisaID = spdID;
                    pDetail.Uraian = txtMetode.Text;
                    if (txtMetode.Text.Trim() != string.Empty)
                        pDetail.RowStatus = 0;
                    else
                        pDetail.RowStatus = -1;
                    rst = fcdsx.insertPdetail(pDetail);
                }
                else
                {
                    pDetail = new SPD_PrsAx();
                    pDetail.Penyebab_ID = 5;
                    pDetail.SPDAnalisaID = spdID;
                    pDetail.Uraian = txtMetode.Text;
                    pDetail.RowStatus = -1;
                    rst = fcdsx.insertPdetail(pDetail);
                }
                UploadFilePareto(spdID);
            }

            #endregion
            #region #2
            if (spdID > 0)
            {
                string strQuery = string.Empty;
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = strQuery;
                SqlDataReader sdr = zl.Retrieve();
                zl = new ZetroView();
                int IdAp = spdID;
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = " select ID IdAp from SPD_Analisa where Tahun='" + ddlTahun.SelectedItem.Text + "' and Bulan='" + Convert.ToInt32(ddlBulan.SelectedValue) + "' and sarmutPID='" + Convert.ToInt32(ddlsmtP.SelectedValue) + "' ";
                sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        IdAp = int.Parse(sdr["IdAp"].ToString());
                    }
                }

                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery =
                    "update SPD_TransPrs set IdAp=" + IdAp + " where Approval=0 and tahun='" + ddlTahun.SelectedItem.Text + "' and bulan='" + Convert.ToInt32(ddlBulan.SelectedValue) + "' " +
                    " and SarmutPID ='" + Convert.ToInt32(ddlsmtP.SelectedValue) + "' ";
                SqlDataReader sdr1 = zl1.Retrieve();


                zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;

                zl1.CustomQuery =
                   "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun='" + ddlTahun.SelectedItem.Text + "' " +
                   " set @bulan='" + Convert.ToInt32(ddlBulan.SelectedValue) + "' " +
                   "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                   "update SPD_Trans set IdAp=" + IdAp + " where Approval=0 and tahun=@tahun and bulan=@bulan and  SarmutDeptID in ( " +
                   "select ID from SPD_Departemen where @thnbln>=drperiode and @thnbln<=sdperiode and rowstatus>-1 and sarmutdepartemen='" + ddlsarmutDept.SelectedItem + "' and SarmutPID in ( " +
                   "select ID from SPD_Perusahaan where deptid='" + Convert.ToInt32(ddlDeptName.SelectedValue) + "' and rowstatus>-1 and SarMutPerusahaan='" + ddlsmtP.SelectedItem + "'))";
                sdr1 = zl1.Retrieve();
                #endregion
            
                FacadePrsaX spdf = new FacadePrsaX();
                SPD_PrsAx spdx1 = new SPD_PrsAx();
                spdx1.SPDAnalisaID = spdID;
                spdx1.Tindakan = txtTIndakan.Text;
                spdx1.Pelaku = txtPelaku.Text;
                spdx1.Jadwal_Selesai = DateTime.Parse(txtDateJSp.Text);
                spdx1.Jenis = "Perbaikan";
                spdx1.CreatedBy = user.UserName;
                int rstT = spdf.InsertTindakan(spdx1);

                spdx1= new SPD_PrsAx();
                spdx1.SPDAnalisaID = spdID;
                spdx1.Tindakan = txtTIndakan0.Text;
                spdx1.Pelaku = txtPelaku0.Text;
                spdx1.Jadwal_Selesai = DateTime.Parse(txtDateJS0.Text);
                spdx1.Jenis = "Pencegahan";
                spdx1.CreatedBy = user.UserName;
                int rstP = spdf.InsertTindakan(spdx1);

            }
        }
        private bool CheckAttachment(string DocName,string tablename)
        {
            bool rst = false;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select FileName from " + tablename + " where RowStatus >-1 and FileName='" + DocName + "'";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    rst = true;
                }
            }
            return rst;
        }
        protected void UploadFilePareto(int AnalisaID)
        {
            if (UploadPareto.HasFile)
            {
                Users users = (Users)Session["Users"];
                string FilePath = UploadPareto.PostedFile.FileName;
                string filename = Path.GetFileName(FilePath);
                string ext = Path.GetExtension(filename);
                int ExtractPos = FilePath.LastIndexOf("\\") + 1;
                String UploadedFileName = FilePath.Substring(ExtractPos, FilePath.Length - ExtractPos);
                UploadPareto.PostedFile.SaveAs("D:\\Data Lampiran Purchn\\sarmut\\" + UploadedFileName);
                TPP_LampiranFacade tppLF = new TPP_LampiranFacade();
                //string idTPP = AnalisaID;
                //string IDLampiran = tppLF.GetIDlampiran(idTPP);
                //string apv = tppLF.GetApvTPP(idTPP);
                try
                {
                    String pdfUrl = UploadedFileName;
                    if (pdfUrl.IndexOf("/") >= 0 || pdfUrl.IndexOf("\\") >= 0)
                    {
                        Response.End();
                    }
                    //string tablename = Request.QueryString["tablename"].ToString();
                    ZetroView zl = new ZetroView();
                    zl.QueryType = Operation.CUSTOM;
                    zl.CustomQuery = "insert into SPD_Attachment_Pareto(A_SarmutTransID, docName, FileName, RowStatus, CreatedBy, " +
                        "CreatedTime, LastModifiedBy, LastModifiedTime)values(" + AnalisaID + ",'-','" + UploadedFileName + "',0,'" + users.UserName + "',getdate(),'" + users.UserName + "',getdate())";
                    SqlDataReader sdr = zl.Retrieve();
                }
                catch (Exception ex)
                { }
            }
        }
        protected void UploadFileParetoT(int AnalisaID)
        {
            if (UploadPareto0.HasFile)
            {
                Users users = (Users)Session["Users"];
                string FilePath = UploadPareto0.PostedFile.FileName;
                string filename = Path.GetFileName(FilePath);
                string ext = Path.GetExtension(filename);
                int ExtractPos = FilePath.LastIndexOf("\\") + 1;
                String UploadedFileName = FilePath.Substring(ExtractPos, FilePath.Length - ExtractPos);
                UploadPareto0.PostedFile.SaveAs("D:\\Data Lampiran Purchn\\sarmut\\" + UploadedFileName);
                TPP_LampiranFacade tppLF = new TPP_LampiranFacade();
                //string idTPP = AnalisaID;
                //string IDLampiran = tppLF.GetIDlampiran(idTPP);
                //string apv = tppLF.GetApvTPP(idTPP);
                try
                {
                    String pdfUrl = UploadedFileName;
                    if (pdfUrl.IndexOf("/") >= 0 || pdfUrl.IndexOf("\\") >= 0)
                    {
                        Response.End();
                    }
                    //string tablename = Request.QueryString["tablename"].ToString();
                    ZetroView zl = new ZetroView();
                    zl.QueryType = Operation.CUSTOM;
                    zl.CustomQuery = "insert into SPD_Attachment_Pareto(A_SarmutTransID, docName, FileName, RowStatus, CreatedBy, " +
                        "CreatedTime, LastModifiedBy, LastModifiedTime)values(" + AnalisaID + ",'-','" + UploadedFileName + "',0,'" + users.UserName + "',getdate(),'" + users.UserName + "',getdate())";
                    SqlDataReader sdr = zl.Retrieve();
                }
                catch (Exception ex)
                { }
            }
        }
        protected void btnTambahPareto_ServerClick(object sender, EventArgs e)
        {
            Panel18.Visible = true;
        }
        protected void btnUploadPareto0_ServerClick(object sender, EventArgs e)
        {
            UploadFileParetoT( int.Parse(sarmutID.Text));
            LoadPareto(sarmutnotxt.Text);
            Panel18.Visible = false ;
        }

        protected void LoadPerbaikan(string AnNo)
        {

            if (AnNo.Length == 1)
                return;
            Session["perbaikan"] = null;
            ArrayList arrPerbaikan = new ArrayList();
            FacadePrsaX PerbaikanF = new FacadePrsaX();
            arrPerbaikan = PerbaikanF.RetrieveByNo1(AnNo, "Perbaikan");
            Session["perbaikan"] = arrPerbaikan;
            GridPerbaikan.DataSource = arrPerbaikan;
            GridPerbaikan.DataBind();
        }

        protected void LoadPencegahan(string AnNo)
        {

            if (AnNo.Length == 1)
                return;
            Session["pencegahan"] = null;
            ArrayList arrpencegahan = new ArrayList();
            FacadePrsaX PencegahanF = new FacadePrsaX();
            arrpencegahan = PencegahanF.RetrieveByNo1(AnNo, "Pencegahan");
            Session["pencegahan"] = arrpencegahan;
            GridPencegahan.DataSource = arrpencegahan;
            GridPencegahan.DataBind();
        }

        protected void btnPerbaikan_ServerClick(object sender, EventArgs e)
        {
            if (sarmutnotxt.Text != string.Empty)
            {
                PanelPebaikan.Visible = true;
            }
            txtTIndakan.Text = string.Empty;
            txtPelaku.Text = string.Empty;
            txtDateJSp.Text = string.Empty;
            btnAddPerbaikan.Disabled = false;
        }

        protected void chkVerifikasi1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow gr = (GridViewRow)chk.NamingContainer;
            Label lDateVF = (Label)GridPerbaikan.Rows[gr.RowIndex].FindControl("lDateVF");
            BDPLite txtDateVF = (BDPLite)GridPerbaikan.Rows[gr.RowIndex].FindControl("txtDateVF");
            Label lDateAS = (Label)GridPerbaikan.Rows[gr.RowIndex].FindControl("lDateAS");
            //txtITSesuai.Text = GridPerbaikan.Rows[gr.RowIndex].Cells[1].Text ;
            string verf = string.Empty;
            if (chk.Checked == true)
                verf = "1";
            else
                verf = "0";
            FacadePrsaX fcdtf = new FacadePrsaX();
            //if (lDateAS.Text.Trim() == string.Empty)
            //{
            //    chk.Checked = false;
            //    return;
            //}
            string strerror = fcdtf.UpdateTindakan(GridPerbaikan.Rows[gr.RowIndex].Cells[1].Text, txtDateVF.SelectedDate.ToString("yyyyMMdd"), verf);
            LoadPerbaikan(sarmutnotxt.Text.Trim());

        }

        protected void GridPerbaikan_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
            Users user = ((Users)Session["Users"]);
            CheckBox chkver = (CheckBox)GridPerbaikan.Rows[rowindex].FindControl("chkVerifikasi1");
            BDPLite txtDateAS = (BDPLite)GridPerbaikan.Rows[rowindex].FindControl("txtDateAS");
            Label lDateJS = (Label)GridPerbaikan.Rows[rowindex].FindControl("lDateJS");
            BDPLite txtDateJS = (BDPLite)GridPerbaikan.Rows[rowindex].FindControl("txtDateJS");
            Label lDateAS = (Label)GridPerbaikan.Rows[rowindex].FindControl("lDateAS");


            //Penambahan agus 12-08-2022
            Label lblTindakanPerbaikan = (Label)GridPerbaikan.Rows[rowindex].FindControl("lblTindakanPerbaikan");
            TextBox txtEditTindakanPerbaikan = (TextBox)GridPerbaikan.Rows[rowindex].FindControl("txtEditTindakanPerbaikan");

            Label lblPelakuPerbaikan = (Label)GridPerbaikan.Rows[rowindex].FindControl("lblPelakuPerbaikan");
            TextBox txtEditPelakuPerbaikan = (TextBox)GridPerbaikan.Rows[rowindex].FindControl("txtEditPelakuPerbaikan");
            //Penambahan agus 12-08-2022

            LinkButton btn1 = (LinkButton)GridPerbaikan.Rows[rowindex].Cells[4].Controls[0];

            LinkButton btn = (LinkButton)GridPerbaikan.Rows[rowindex].Cells[7].Controls[0];
            LinkButton btntarget = (LinkButton)GridPerbaikan.Rows[rowindex].Cells[10].Controls[0];
            if (e.CommandName == "rubah")
            {

                if (btn.Text == "Simpan")
                {
                    btn.Text = "Edit";
                    FacadePrsaX smttf = new FacadePrsaX();
                    if (txtDateJS.SelectedDate < txtDateAS.SelectedDate)
                    {
                        txtDateAS.Visible = false;
                        lDateAS.Visible = true;
                        txtDateJS.Visible = false;
                        lDateJS.Visible = true;
                        DisplayAJAXMessage(this, "Tanggal aktual tidak boleh lebih besar dari Tanggal Jadwal ");
                        return;
                    }
                    string strerror = smttf.UpdateAktualSelesai(GridPerbaikan.Rows[rowindex].Cells[1].Text, txtDateJS.SelectedDate.ToString("yyyyMMdd"), txtDateAS.SelectedDate.ToString("yyyyMMdd"));
                    LoadPerbaikan(sarmutnotxt.Text.Trim());
                    txtDateAS.Visible = false;
                    lDateAS.Visible = true;
                    txtDateJS.Visible = false;
                    lDateJS.Visible = true;
                }
                else
                {
                    btn.Text = "Simpan";
                    txtDateAS.Visible = true;
                    lDateAS.Visible = false;
                    txtDateJS.Visible = true;
                    lDateJS.Visible = false;
                }
            }


            //penambahan agus 12-08-2022

            if (e.CommandName == "rubahperbaikan")//belum dibuat validasi kalo sudah approve 2 tidak bisa edit
            {




                if (btn1.Text == "Simpan")
                {
                    btn1.Text = "Edit";
                    FacadePrsaX smttf = new FacadePrsaX();

                    if (txtEditTindakanPerbaikan.Text == "" && txtEditPelakuPerbaikan.Text == "")
                    {

                        txtEditTindakanPerbaikan.Visible = false;
                        lblTindakanPerbaikan.Visible = true;

                        txtEditPelakuPerbaikan.Visible = false;
                        lblPelakuPerbaikan.Visible = true;



                        DisplayAJAXMessage(this, "Data kosong");
                        return;
                    }
                    string strerror1 = smttf.UpdateTindakanPerbaikan(GridPerbaikan.Rows[rowindex].Cells[1].Text, txtEditTindakanPerbaikan.Text, txtEditPelakuPerbaikan.Text);
                    LoadPerbaikan(sarmutnotxt.Text.Trim());


                    txtEditTindakanPerbaikan.Visible = false;
                    lblTindakanPerbaikan.Visible = true;

                    txtEditPelakuPerbaikan.Visible = false;
                    lblPelakuPerbaikan.Visible = true;



                }
                else
                {
                    btn1.Text = "Simpan";


                    txtEditTindakanPerbaikan.Visible = true;
                    lblTindakanPerbaikan.Visible = false;

                    txtEditPelakuPerbaikan.Visible = true;
                    lblPelakuPerbaikan.Visible = false;



                }
            }

            //penambahan agus 12-08-2022



            if (e.CommandName == "hapus")
            {
                FacadePrsaX tpptf = new FacadePrsaX();
                string strerror = tpptf.DeleteTindakan(GridPerbaikan.Rows[rowindex].Cells[1].Text);
                LoadPerbaikan(sarmutnotxt.Text.Trim());
            }
            if (e.CommandName == "targetx")
            {
                SPD_PrsAx tindakan = new SPD_PrsAx();
                FacadePrsaX tindakanF = new FacadePrsaX();
                SPD_PrsAx spd = new SPD_PrsAx();
                FacadePrsaX tppf = new FacadePrsaX();
                spd = tppf.RetrieveByNo(sarmutnotxt.Text.Trim());
                tindakan.SPDAnalisaID = spd.ID;
                tindakan.Tindakan = GridPerbaikan.Rows[rowindex].Cells[2].Text;
                tindakan.Pelaku = GridPerbaikan.Rows[rowindex].Cells[3].Text;
                tindakan.Jenis = "Perbaikan";
                tindakan.Targetx = "T" + (tindakanF.getTarget(sarmutnotxt.Text.Trim(), tindakan.Jenis, tindakan.Tindakan) + 1).ToString();
                tindakan.CreatedBy = user.UserName;
                string strerror = tindakanF.UpdateAktualSelesai(GridPerbaikan.Rows[rowindex].Cells[1].Text,
                    DateTime.Now.ToString("yyyyMMdd"), DateTime.MinValue.ToString("yyyyMMdd"));
                strerror = tindakanF.UpdateTindakan(GridPerbaikan.Rows[rowindex].Cells[1].Text, DateTime.Now.ToString("yyyyMMdd"), "1");
                int rst = tppf.InsertTindakan(tindakan);
                LoadPerbaikan(sarmutnotxt.Text.Trim());
            }

        }

        protected void GridPerbaikan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            FacadePrsaX p = new FacadePrsaX();
            string nilai = p.CekApproval(sarmutnotxt.Text);
            ArrayList arrperbaikan = new ArrayList();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btn3 = (LinkButton)e.Row.Cells[4].Controls[0];

                CheckBox chkver = (CheckBox)e.Row.FindControl("chkVerifikasi1");

                if (nilai == "0")
                {
                    btn3.Visible = true;
                }
                else
                {
                    btn3.Visible = false;
                }

                if (Convert.ToInt32(e.Row.Cells[0].Text.Trim()) == 0)
                    chkver.Checked = false;
                else
                    chkver.Checked = true;
                if (Session["usertype"].ToString().Trim().ToUpper() == "ISO")
                    chkver.Enabled = true;
                else
                    chkver.Enabled = false;
                if (Session["perbaikan"] != null)
                {
                    arrperbaikan = (ArrayList)Session["perbaikan"];
                    SPD_PrsAx tindakan = (SPD_PrsAx)arrperbaikan[e.Row.RowIndex];
                    Label lDateJS = (Label)e.Row.FindControl("lDateJS");
                    BDPLite txtDateJS = (BDPLite)e.Row.FindControl("txtDateJS");
                    Label lDateAS = (Label)e.Row.FindControl("lDateAS");
                    BDPLite txtDateAS = (BDPLite)e.Row.FindControl("txtDateAS");
                    Label lDateVF = (Label)e.Row.FindControl("lDateVF");
                    BDPLite txtDateVF = (BDPLite)e.Row.FindControl("txtDateVF");
                    LinkButton btn = (LinkButton)e.Row.Cells[7].Controls[0];
                    LinkButton btn1 = (LinkButton)e.Row.Cells[12].Controls[0];
                    LinkButton btn2 = (LinkButton)e.Row.Cells[10].Controls[0];
                    ((System.Web.UI.WebControls.LinkButton)(e.Row.Cells[10].Controls[0])).ToolTip = "Klik untuk naik target";
                    if (e.Row.Cells[0].Text.Trim() == "0")
                    {
                        btn.Enabled = true;
                        btn1.Enabled = true;
                        btn2.Enabled = true;
                    }
                    else
                    {
                        btn.Enabled = false;
                        btn1.Enabled = false;
                        btn2.Enabled = false;
                    }
                    if (tindakan.Jadwal_Selesai.ToString("yyyyMMdd") != "00010101" && tindakan.Jadwal_Selesai.ToString("yyyyMMdd") != "17530101")
                    {
                        lDateJS.Text = tindakan.Jadwal_Selesai.ToString("dd-MMM-yyyy");
                        txtDateJS.SelectedValue = tindakan.Jadwal_Selesai.ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        lDateJS.Text = "";
                        txtDateJS.SelectedValue = DateTime.Now.ToString("dd-MMM-yyyy");
                    }

                    if (tindakan.Aktual_Selesai.ToString("yyyyMMdd") != "00010101" && tindakan.Aktual_Selesai.ToString("yyyyMMdd") != "17530101")
                    {
                        lDateAS.Text = tindakan.Aktual_Selesai.ToString("dd-MMM-yyyy");
                        txtDateAS.SelectedValue = tindakan.Aktual_Selesai.ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        lDateAS.Text = "";
                        txtDateAS.SelectedValue = DateTime.Now.ToString("dd-MMM-yyyy");
                    }
                    if (tindakan.Tglverifikasi.ToString("yyyyMMdd") != "00010101" && tindakan.Tglverifikasi.ToString("yyyyMMdd") != "17530101")
                    {
                        lDateVF.Text = tindakan.Tglverifikasi.ToString("dd-MMM-yyyy");
                        txtDateVF.SelectedValue = tindakan.Tglverifikasi.ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        lDateVF.Text = "";
                        txtDateVF.SelectedValue = DateTime.Now.ToString("dd-MMM-yyyy");
                    }
                    if (chkver.Checked == false && chkver.Enabled == true)
                    {
                        txtDateVF.Visible = true;
                        lDateVF.Visible = false;
                    }
                    else
                    {
                        txtDateVF.Visible = false;
                        lDateVF.Visible = true;
                    }
                }
            }
        }

        protected void btnPencegahan_ServerClick(object sender, EventArgs e)
        {
            if (sarmutnotxt.Text != string.Empty)
            {
                PanelPencegahan.Visible = true;
            }
            txtTIndakan0.Text = string.Empty;
            txtPelaku0.Text = string.Empty;
            txtDateJS0.Text = string.Empty;
            btnAddPencegahan.Disabled = false;
        }

        protected void btnAddPencegahan_ServerClick(object sender, EventArgs e)
        {
            if (sarmutnotxt.Text.Trim() != string.Empty)
            {
                if (txtTIndakan0.Text.Trim() == string.Empty || txtPelaku0.Text.Trim() == string.Empty || txtDateJS0.Text.Trim() == string.Empty)
                    return;
                Users user = ((Users)Session["Users"]);
                SPD_PrsAx spdx = new SPD_PrsAx();
                FacadePrsaX spdf = new FacadePrsaX();
                spdx = spdf.RetrieveByNo(sarmutnotxt.Text.Trim());
                SPD_PrsAx spdx1 = new SPD_PrsAx();
                spdx1.SPDAnalisaID = spdx.ID;
                spdx1.Tindakan = txtTIndakan0.Text;
                spdx1.Pelaku = txtPelaku0.Text;
                spdx1.Jadwal_Selesai = DateTime.Parse( txtDateJS0.Text );
                spdx1.Jenis = "Pencegahan";
                spdx1.CreatedBy = user.UserName;
                int rst = spdf.InsertTindakan(spdx1);
                PanelPencegahan.Visible = false;
                LoadPencegahan(sarmutnotxt.Text.Trim());
                txtTIndakan0.Text = string.Empty;
                txtPelaku0.Text = string.Empty;
                txtDateJS0.Text = string.Empty;
                btnAddPencegahan.Disabled = false;
            }
        }

        protected void btnAddPerbaikan_ServerClick(object sender, EventArgs e)
        {
            if (sarmutnotxt.Text.Trim() != string.Empty)
            {
                if (txtTIndakan.Text.Trim() == string.Empty || txtPelaku.Text.Trim()==string.Empty || txtDateJSp.Text.Trim()==string.Empty )
                    return;
                Users user = ((Users)Session["Users"]);
                SPD_PrsAx spdx = new SPD_PrsAx();
                FacadePrsaX spdf = new FacadePrsaX();
                spdx = spdf.RetrieveByNo(sarmutnotxt.Text.Trim());
                SPD_PrsAx spdx1 = new SPD_PrsAx();
                spdx1.SPDAnalisaID = spdx.ID;
                spdx1.Tindakan = txtTIndakan.Text;
                spdx1.Pelaku = txtPelaku.Text;
                spdx1.Jadwal_Selesai = DateTime.Parse( txtDateJSp.Text );
                spdx1.Jenis = "Perbaikan";
                spdx1.CreatedBy = user.UserName;
                int rst = spdf.InsertTindakan(spdx1);
                PanelPebaikan.Visible = false;
                LoadPerbaikan(sarmutnotxt.Text.Trim());
                txtTIndakan.Text = string.Empty;
                txtPelaku.Text = string.Empty;
                txtDateJSp.Text = string.Empty;
                btnAddPerbaikan.Disabled = false;
            }
        }

        protected void chkVerifikasi0_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow gr = (GridViewRow)chk.NamingContainer;
            Label lDateVF = (Label)GridPencegahan.Rows[gr.RowIndex].FindControl("lDateVF");
            BDPLite txtDateVF = (BDPLite)GridPencegahan.Rows[gr.RowIndex].FindControl("txtDateVF");
            Label lDateAS = (Label)GridPencegahan.Rows[gr.RowIndex].FindControl("lDateAS");
            string verf = string.Empty;
            if (chk.Checked == true)
                verf = "1";
            else
                verf = "0";
            FacadePrsaX fsm = new FacadePrsaX();
            string strerror = fsm.UpdateTindakan(GridPencegahan.Rows[gr.RowIndex].Cells[1].Text, txtDateVF.SelectedDate.ToString("yyyyMMdd"), verf);
            LoadPencegahan(sarmutnotxt.Text.Trim()); LoadPerbaikan(sarmutnotxt.Text.Trim());
        }

        protected void GridPencegahan_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
            Users user = ((Users)Session["Users"]);
            CheckBox chkver = (CheckBox)GridPencegahan.Rows[rowindex].FindControl("chkVerifikasi0");

            //penambahan agus 2022-07-22
            TextBox txtEditTindakanPencegahan = (TextBox)GridPencegahan.Rows[rowindex].FindControl("txtEditTindakanPencegahan");
            Label lblTindakanPencegahan = (Label)GridPencegahan.Rows[rowindex].FindControl("lblTindakanPencegahan");
            TextBox txtEditPelakuPencegahan = (TextBox)GridPencegahan.Rows[rowindex].FindControl("txtEditPelakuPencegahan");
            Label lblPelakuPencegahan = (Label)GridPencegahan.Rows[rowindex].FindControl("lblPelakuPencegahan");
            //penambahan agus 2022-07-22

            BDPLite txtDateAS = (BDPLite)GridPencegahan.Rows[rowindex].FindControl("txtDateAS");
            Label lDateJS = (Label)GridPencegahan.Rows[rowindex].FindControl("lDateJS");
            BDPLite txtDateJS = (BDPLite)GridPencegahan.Rows[rowindex].FindControl("txtDateJS");
            Label lDateAS = (Label)GridPencegahan.Rows[rowindex].FindControl("lDateAS");

            LinkButton btn1 = (LinkButton)GridPencegahan.Rows[rowindex].Cells[4].Controls[0];

            LinkButton btn = (LinkButton)GridPencegahan.Rows[rowindex].Cells[7].Controls[0];
            LinkButton btntarget = (LinkButton)GridPencegahan.Rows[rowindex].Cells[10].Controls[0];
            if (e.CommandName == "rubah")
            {

                if (btn.Text == "Simpan")
                {
                    btn.Text = "Edit";
                    FacadePrsaX smttf = new FacadePrsaX();
                    if (txtDateJS.SelectedDate < txtDateAS.SelectedDate)
                    {
                        txtDateAS.Visible = false;
                        lDateAS.Visible = true;
                        txtDateJS.Visible = false;
                        lDateJS.Visible = true;
                        DisplayAJAXMessage(this, "Tanggal aktual tidak boleh lebih besar dari Tanggal Jadwal ");
                        return;
                    }

                    string strerror = smttf.UpdateAktualSelesai(GridPencegahan.Rows[rowindex].Cells[1].Text, txtDateJS.SelectedDate.ToString("yyyyMMdd"), txtDateAS.SelectedDate.ToString("yyyyMMdd"));
                    LoadPencegahan(sarmutnotxt.Text.Trim());
                    txtDateAS.Visible = false;
                    lDateAS.Visible = true;
                    txtDateJS.Visible = false;
                    lDateJS.Visible = true;
                }
                else
                {
                    btn.Text = "Simpan";
                    txtDateAS.Visible = true;
                    lDateAS.Visible = false;
                    txtDateJS.Visible = true;
                    lDateJS.Visible = false;
                }
            }


            //penambahan agus 12-08-2022
            if (e.CommandName == "rubahpencegahan")
            {



                if (btn1.Text == "Simpan")
                {
                    btn1.Text = "Edit";
                    FacadePrsaX smttf = new FacadePrsaX();
                    if (txtEditTindakanPencegahan.Text == "" && txtEditPelakuPencegahan.Text == "")
                    {

                        txtEditTindakanPencegahan.Visible = false;
                        lblTindakanPencegahan.Visible = true;

                        txtEditPelakuPencegahan.Visible = false;
                        lblPelakuPencegahan.Visible = true;

                        DisplayAJAXMessage(this, "Data Kosong ");
                        return;
                    }

                    string strerror = smttf.UpdateTindakanPencegahan(GridPencegahan.Rows[rowindex].Cells[1].Text, txtEditTindakanPencegahan.Text, txtEditPelakuPencegahan.Text);
                    LoadPencegahan(sarmutnotxt.Text.Trim());


                    txtEditTindakanPencegahan.Visible = false;
                    lblTindakanPencegahan.Visible = true;

                    txtEditPelakuPencegahan.Visible = false;
                    lblPelakuPencegahan.Visible = true;

                }
                else
                {
                    btn1.Text = "Simpan";

                    txtEditTindakanPencegahan.Visible = true;
                    lblTindakanPencegahan.Visible = false;
                    txtEditPelakuPencegahan.Visible = true;
                    lblPelakuPencegahan.Visible = false;

                }
            }
            //penambahan agus 12-08-2022

            if (e.CommandName == "hapus")
            {
                FacadePrsaX smttf = new FacadePrsaX();
                string strerror = smttf.DeleteTindakan(GridPencegahan.Rows[rowindex].Cells[1].Text);
                LoadPencegahan(sarmutnotxt.Text.Trim());
            }
            if (e.CommandName == "target")
            {
                SPD_PrsAx tindakan = new SPD_PrsAx();
                FacadePrsaX tindakanF = new FacadePrsaX();
                SPD_PrsAx spd = new SPD_PrsAx();
                FacadePrsaX tppf = new FacadePrsaX();
                //btntarget.Attributes.Add("onclick", "openWindow()");
                spd = tppf.RetrieveByNo(sarmutnotxt.Text.Trim());
                tindakan.SPDAnalisaID = spd.ID;
                tindakan.Tindakan = GridPencegahan.Rows[rowindex].Cells[2].Text;
                tindakan.Pelaku = GridPencegahan.Rows[rowindex].Cells[3].Text;
                //tindakan.Jadwal_Selesai = DateTime.Parse(GridPencegahan.Rows[rowindex].Cells[1].Text);
                tindakan.Jenis = "Pencegahan";
                tindakan.Targetx = "T" + (tindakanF.getTarget(sarmutnotxt.Text.Trim(), tindakan.Jenis, tindakan.Tindakan) + 1).ToString();
                tindakan.CreatedBy = user.UserName;
                string strerror = tindakanF.UpdateAktualSelesai(GridPencegahan.Rows[rowindex].Cells[1].Text,
                    DateTime.Now.ToString("yyyyMMdd"), DateTime.MinValue.ToString("yyyyMMdd"));
                strerror = tindakanF.UpdateTindakan(GridPencegahan.Rows[rowindex].Cells[1].Text, DateTime.Now.ToString("yyyyMMdd"), "1");
                int rst = tppf.InsertTindakan(tindakan);
                LoadPencegahan(sarmutnotxt.Text.Trim());
            }

        }

        protected void GridPencegahan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            FacadePrsaX p = new FacadePrsaX();
            string nilai = p.CekApproval(sarmutnotxt.Text);

            ArrayList arrpencegahan = new ArrayList();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btn3 = (LinkButton)e.Row.Cells[4].Controls[0];
                CheckBox chkver = (CheckBox)e.Row.FindControl("chkVerifikasi0");

                if (nilai == "0")
                {
                    btn3.Visible = true;
                }
                else
                {
                    btn3.Visible = true;
                }

                if (Convert.ToInt32(e.Row.Cells[0].Text.Trim()) == 0)
                    chkver.Checked = false;
                else
                    chkver.Checked = true;
                if (Session["usertype"].ToString().Trim().ToUpper() == "ISO")
                    chkver.Enabled = true;
                else
                    chkver.Enabled = false;
                if (Session["pencegahan"] != null)
                {
                    arrpencegahan = (ArrayList)Session["pencegahan"];
                    SPD_PrsAx tindakan = (SPD_PrsAx)arrpencegahan[e.Row.RowIndex];
                    Label lDateJS = (Label)e.Row.FindControl("lDateJS");
                    BDPLite txtDateJS = (BDPLite)e.Row.FindControl("txtDateJS");
                    Label lDateAS = (Label)e.Row.FindControl("lDateAS");
                    BDPLite txtDateAS = (BDPLite)e.Row.FindControl("txtDateAS");
                    Label lDateVF = (Label)e.Row.FindControl("lDateVF");
                    BDPLite txtDateVF = (BDPLite)e.Row.FindControl("txtDateVF");
                    LinkButton btn = (LinkButton)e.Row.Cells[7].Controls[0];
                    LinkButton btn1 = (LinkButton)e.Row.Cells[12].Controls[0];
                    LinkButton btn2 = (LinkButton)e.Row.Cells[10].Controls[0];
                    ((System.Web.UI.WebControls.LinkButton)(e.Row.Cells[10].Controls[0])).ToolTip = "Klik untuk naik target";
                    if (e.Row.Cells[0].Text.Trim() == "0")
                    {
                        btn.Enabled = true;
                        btn1.Enabled = true;
                        btn2.Enabled = true;
                    }
                    else
                    {
                        btn.Enabled = false;
                        btn1.Enabled = false;
                        btn2.Enabled = false;
                    }
                    if (tindakan.Jadwal_Selesai.ToString("yyyyMMdd") != "00010101" && tindakan.Jadwal_Selesai.ToString("yyyyMMdd") != "17530101")
                    {
                        lDateJS.Text = tindakan.Jadwal_Selesai.ToString("dd-MMM-yyyy");
                        txtDateJS.SelectedValue = tindakan.Jadwal_Selesai.ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        lDateJS.Text = "";
                        txtDateJS.SelectedValue = DateTime.Now.ToString("dd-MMM-yyyy");
                    }

                    if (tindakan.Aktual_Selesai.ToString("yyyyMMdd") != "00010101" && tindakan.Aktual_Selesai.ToString("yyyyMMdd") != "17530101")
                    {
                        lDateAS.Text = tindakan.Aktual_Selesai.ToString("dd-MMM-yyyy");
                        txtDateAS.SelectedValue = tindakan.Aktual_Selesai.ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        lDateAS.Text = "";
                        txtDateAS.SelectedValue = DateTime.Now.ToString("dd-MMM-yyyy");
                    }
                    if (tindakan.Tglverifikasi.ToString("yyyyMMdd") != "00010101" && tindakan.Tglverifikasi.ToString("yyyyMMdd") != "17530101")
                    {
                        lDateVF.Text = tindakan.Tglverifikasi.ToString("dd-MMM-yyyy");
                        txtDateVF.SelectedValue = tindakan.Tglverifikasi.ToString("dd-MMM-yyyy");
                    }
                    else
                    {
                        lDateVF.Text = "";
                        txtDateVF.SelectedValue = DateTime.Now.ToString("dd-MMM-yyyy");
                    }
                    if (chkver.Checked == false && chkver.Enabled == true)
                    {
                        txtDateVF.Visible = true;
                        lDateVF.Visible = false;
                    }
                    else
                    {
                        txtDateVF.Visible = false;
                        lDateVF.Visible = true;
                    }
                }
            }
        }

        private void LoadDept()
        {
            Users users = (Users)Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select * from SPD_Dept where RowStatus>-1 and dptID in (" + users.DeptID + ") order by dept";
            SqlDataReader sdr = zl.Retrieve();
            ddlDeptName.Items.Clear();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlDeptName.Items.Add(new ListItem(sdr["Dept"].ToString(), sdr["ID"].ToString()));
                }
            }
        }
        //private void LoadDept0()
        //{
        //    Users users = (Users)Session["Users"];
        //    ZetroView zl = new ZetroView();
        //    zl.QueryType = Operation.CUSTOM;
        //    zl.CustomQuery = "select * from SPD_Dept where RowStatus>-1 and dptID in (" + users.DeptID + ") order by dept";
        //    SqlDataReader sdr = zl.Retrieve();

        //    ddlDeptName0.Items.Clear();
        //    if (sdr.HasRows)
        //    {
        //        while (sdr.Read())
        //        {

        //            ddlDeptName0.Items.Add(new ListItem(sdr["Dept"].ToString(), sdr["ID"].ToString()));
        //        }
        //    }
        //}

        private void LoadDept1()
        {
            Users users = (Users)Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            if (users.DeptID == 23)
            {
                zl.CustomQuery = "select * from SPD_Dept where RowStatus>-1 order by dept";
                ddlDeptName1.Items.Add(new ListItem("ALL", "0"));
            }
            else if (users.DeptID == 14)
            {
                zl.CustomQuery = "select * from SPD_Dept where RowStatus>-1 order by dept";
                ddlDeptName1.Items.Add(new ListItem("ALL", "0"));
            }
            //WO PM JOMBANG bisa liat all dept
            else if (users.DeptID == 0)
            {
                zl.CustomQuery = "select * from SPD_Dept where RowStatus>-1 order by dept";
                ddlDeptName1.Items.Add(new ListItem("ALL", "0"));
            }
            else
            {
                zl.CustomQuery = "select * from SPD_Dept where RowStatus>-1 and dptID in (" + users.DeptID + ") order by dept";
            }
            SqlDataReader sdr = zl.Retrieve();
            //ddlDeptName1.Items.Add(new ListItem("ALL", "0"));
            //ddlDeptName1.Items.Clear();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {

                    ddlDeptName1.Items.Add(new ListItem(sdr["Dept"].ToString(), sdr["ID"].ToString()));
                }
            }
            //if (ddlDeptName1.Items.Count == 1)
            //{
            //    ddlDeptName0_SelectedIndexChanged(null, null);


            //}
           
            //Users user = ((Users)Session["Users"]);
            //tppdept = deptFacade.GetUserDept(user.ID);
            //ddlDeptName.SelectedValue = tppdept.ID.ToString();
        }

        private void LoadDeptRekap()
        {
            Users users = (Users)Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select * from SPD_Dept where RowStatus>-1 order by dept";
            SqlDataReader sdr = zl.Retrieve();
            ddlDeptRekap.Items.Add(new ListItem("ALL", "0"));
            //ddlDeptName1.Items.Clear();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {

                    ddlDeptRekap.Items.Add(new ListItem(sdr["Dept"].ToString(), sdr["ID"].ToString()));
                }
            }
        }

        private void LoadSmtP()
        {
            Users users = (Users)Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = " select  A.ID,A.SarMutPerusahaan,B.DptID,C.TargetV1,X.Param,Z.Satuan From SPD_Perusahaan A  " +
                             " inner join SPD_Dept B on B.ID=A.DeptID " +
                             " inner join SPD_TargetV C on A.TargetVID=c.ID " +
                             " inner join SPD_Parameter x on A.ParamID=X.ID " +
                             " inner join SPD_Satuan z on A.SatuanID=z.ID " +
                             //" where B.DptID in (" + users.DeptID + ") and A.TypeSarmutID=1 ";
                             " where B.DptID in (" + users.DeptID + ") and A.RowStatus>-1 order by A.TypeSarmutID ";
            SqlDataReader sdr = zl.Retrieve();
            ddlsmtP.Items.Add(new ListItem("-- Pilih -- ", "0"));
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlsmtP.Items.Add(new ListItem(sdr["SarMutPerusahaan"].ToString(), sdr["ID"].ToString()));
                }
            }
        }

        //private void LoadSmtP0()
        //{
        //    Users users = (Users)Session["Users"];
        //    ZetroView zl = new ZetroView();
        //    zl.QueryType = Operation.CUSTOM;
        //    zl.CustomQuery = " select  A.ID,A.SarMutPerusahaan,B.DptID,C.TargetV1,X.Param,Z.Satuan From SPD_Perusahaan A  " +
        //                     " inner join SPD_Dept B on B.ID=A.DeptID " +
        //                     " inner join SPD_TargetV C on A.TargetVID=c.ID " +
        //                     " inner join SPD_Parameter x on A.ParamID=X.ID " +
        //                     " inner join SPD_Satuan z on A.SatuanID=z.ID " +
        //        //" where B.DptID in (" + users.DeptID + ") and A.TypeSarmutID=1 ";
        //                     " where B.DptID in (" + users.DeptID + ") order by A.TypeSarmutID ";
        //    SqlDataReader sdr = zl.Retrieve();
        //    ddlsmtP0.Items.Add(new ListItem("-- Pilih -- ", "0"));
        //    if (sdr.HasRows)
        //    {
        //        while (sdr.Read())
        //        {
        //            ddlsmtP0.Items.Add(new ListItem(sdr["SarMutPerusahaan"].ToString(), sdr["ID"].ToString()));
        //        }
        //    }
        //}

        protected void ddlDeptName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlsmtP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsmtP.SelectedIndex > 0)
            {
                FacadePrsaX deptx = new FacadePrsaX();
                SPD_PrsAx xxx = new SPD_PrsAx();
                if (RBAnalisa.Checked == true)
                {
                    xxx = deptx.RetrieveById1((int.Parse(ddlTahun.SelectedValue)), (int.Parse(ddlBulan.SelectedValue)), (int.Parse(ddlsmtP.SelectedValue)));
                }
                if (RBSemester.Checked == true)
                {
                    xxx = deptx.RetrieveById1xx1((ddlSemester.SelectedValue).ToString(), (int.Parse(ddlTahun.SelectedValue)), (int.Parse(ddlsmtP.SelectedValue)));
                }
                if (xxx.ID == 0) 
                    return;
                txtTargetx.Text = xxx.Target.ToString();
                txtActual.Text = xxx.Actual.ToString();
                txtSatuan.Text = xxx.Satuan.ToString();
                txtParam.Text = xxx.Param.ToString();
                Label1.Text = xxx.Satuan.ToString();
                typeID.Text = xxx.TypeSarmutID.ToString();
                //IDx.Text = xxx.IDX.ToString();
                LoadsmtDeptx();
                //LoadsmtDeptx0();
                decimal pencapaian = Convert.ToDecimal(txtActual.Text);
                decimal PTarget = Convert.ToDecimal(txtTargetx.Text);
                if (pencapaian < PTarget)
                {
                    chkTTercapai.Checked = true;
                }
                else
                {
                    chkTercapai.Checked = false;
                }
            }
            if (RBAnalisa.Checked == true)
            {
                LoadDatasmt0((int.Parse(ddlTahun.SelectedValue)), (int.Parse(ddlsmtP.SelectedValue)));
            }
            if (RBSemester.Checked == true)
            {
                LoadDatasmt0x((int.Parse(ddlTahun.SelectedValue)), (int.Parse(ddlsmtP.SelectedValue)), ((ddlSemester.SelectedValue).ToString()));
            }
        }

        private void LoadsmtDeptx()
        {
            string periode = string.Empty;
            periode = ddlTahun.SelectedValue.Trim() + Int32.Parse(ddlBulan.SelectedValue).ToString().PadLeft(2, '0');
            Users users = (Users)Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = " select A.ID,A.SarmutPID,A.SarmutDepartemen,A.TargetVID,A.SatuanID,A.ParamID From SPD_Departemen A  " +
                             " inner join SPD_TargetV B on A.TargetVID=B.ID " +
                             " where A.SarmutPID=" + Convert.ToInt32(ddlsmtP.SelectedValue) + " and A.Rowstatus>-1 " +
                             "and A.drperiode=(select top 1 drperiode  From SPD_Departemen where SarmutPID=" +
                             Convert.ToInt32(ddlsmtP.SelectedValue) + " and drperiode <='" + periode + "' order by id desc)";
            SqlDataReader sdr = zl.Retrieve();
            ddlsarmutDept.Items.Clear();
            ddlsarmutDept.Items.Add(new ListItem("-- Pilih -- ", "0"));
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlsarmutDept.Items.Add(new ListItem(sdr["SarmutDepartemen"].ToString(), sdr["ID"].ToString()));

                }
            }
        }


        private void LoadDatasmt1(int tahun, int xxx, int xxxx)
        {
            if (Convert.ToInt32(IDx.Text) > 0)
            {
                #region #1
                string semester = string.Empty;
                //tahun = 2020;

                if (Convert.ToInt32(txtbulan.Text) >= 7)
                    semester = " A.Tahun=" + tahun + " and A.Bulan in (7,8,9,10,11,12)  and A.RowStatus>-1 " +
                                " and A.actual>0 and  B.SarmutPID=" + xxx + " and B.ID in (select id from SPD_Departemen where B.SarmutPID=" + xxx + ")";
                if (Convert.ToInt32(txtbulan.Text) <= 6)
                    semester = " A.Tahun=" + tahun + " and A.Bulan in (1,2,3,4,5,6)  and A.RowStatus>-1 " +
                                 " and A.actual>0 and  B.SarmutPID=" + xxx + " and B.ID in (select id from SPD_Departemen where B.SarmutPID=" + xxx + ")";
                Users users = (Users)Session["Users"];
                ArrayList arrData = new ArrayList();
                //newdata = 0;
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = " select B.ID,A.Tahun,A.Bulan," +
                                 " B.SarmutDepartemen,A.Actual,C.ParamID,E.[Param],isnull(G.Satuan,0)Satuan,F.TargetV1 Target, " +
                                 " C.ID IDx,c.DeptID,D.dptID From SPD_Trans A  inner join SPD_Departemen B on B.ID=A.SarmutDeptID   inner join SPD_Perusahaan c on B.SarmutPID=C.ID " +
                                 " inner join SPD_Dept D on C.DeptID=D.ID  inner join SPD_Parameter E on E.ID=C.ParamID  inner join SPD_TargetV F on F.ID=C.TargetVID " +
                                 " inner join SPD_Satuan G on G.ID=C.SatuanID  where " + semester + " order by Bulan Asc ";
                //zl.CustomQuery = " select * from ( " +
                //                 " select 'A'Urt,B.ID,A.Tahun,A.Bulan,case when A.Bulan=1 then 'Jan' when A.Bulan=2 then 'Feb' when A.Bulan=3 Then 'Mar' when A.Bulan=4 then 'Apr' when A.Bulan=5 then 'Mei' when A.Bulan=6 then 'Jun' end NamaBulan, " +
                //                 " B.SarmutDepartemen,A.Actual,C.ParamID,E.[Param],isnull(G.Satuan,0)Satuan,F.TargetV1 Target,  C.ID IDx,c.DeptID,D.dptID " +
                //                 " From SPD_Trans A " +
                //                 " inner join SPD_Departemen B on B.ID=A.SarmutDeptID " +
                //                 " inner join SPD_Perusahaan c on B.SarmutPID=C.ID  " +
                //                 " inner join SPD_Dept D on C.DeptID=D.ID  " +
                //                 " inner join SPD_Parameter E on E.ID=C.ParamID  " +
                //                 " inner join SPD_TargetV F on F.ID=C.TargetVID  " +
                //                 " inner join SPD_Satuan G on G.ID=C.SatuanID  where " + semester + " " +

                //                 " union all " +
                //                 " select 'B'Urt,A.SarmutDeptID ID,A.Tahun,A.Bulan,case when A.Bulan>=0 and A.Bulan<=6 then 'SMT I' else 'SMT II' end NamaBulan,C.SarmutDepartemen,A.Actual,'0'ParamID, " +
                //                 " A.ParamID [Param],A.SatuanID Satuan,'0'Target,A.SarmutPID IDx,A.DeptID,'0'dptID " +
                //                 " from SPD_Analisa A " +
                //                 " left join SPD_Perusahaan B on B.ID=A.SarmutPID " +
                //                 " left join SPD_Departemen C on C.ID=A.SarmutDeptID " +
                //                 " where AnNo='" + sarmutnotxt.Text + "' and Tahun=" + tahun + " " +
                //                 " ) as DataR order by Urt,Bulan ";
                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new SPD_PrsAx
                        {
                            ID = Convert.ToInt32(sdr["ID"].ToString()),
                            Tahun = Convert.ToInt32(sdr["Tahun"].ToString()),
                            Bulan = Convert.ToInt32(sdr["Bulan"].ToString()),
                            SarmutDepartemen = sdr["SarmutDepartemen"].ToString(),
                            //SarMutPerusahaan = sdr["SarMutPerusahaan"].ToString(),
                            //NamaBulan = sdr["NamaBulan"].ToString(),
                            Actual = Convert.ToDecimal(sdr["Actual"].ToString()),
                            Target = Convert.ToDecimal(sdr["Target"].ToString()),
                        });
                    }
                }

                string[] x = new string[arrData.Count];
                decimal[] z = new decimal[arrData.Count];
                decimal[] y = new decimal[arrData.Count];
                decimal[] xy = new decimal[arrData.Count];
                int i = 0;
                foreach (SPD_PrsAx lo in arrData)
                {
                    y[i] = lo.Actual;
                    x[i] = lo.Bulan.ToString();
                    //x[i] = lo.NamaBulan;
                    //z[i] = lo.Target;
                    xy[i] = lo.Actual;
                    i = i + 1;
                }
                Chart1.Legends.Add("Actual");
                Chart1.Series.Add("Actual");
                Chart1.Series[0].IsVisibleInLegend = true;
                Chart1.Series[0].IsValueShownAsLabel = true;
                Chart1.Series[0].Points.DataBindXY(x, y);
                Chart1.Series[0].ChartType = SeriesChartType.Column;
                Chart1.Legends[0].Enabled = true;
                Chart1.Legends[0].Docking = Docking.Bottom;
                Chart1.Legends.Add("Target");
                Chart1.Series.Add("Target");
                Chart1.Series[1].Points.DataBindXY(x, xy);
                Chart1.Series[1].ChartType = SeriesChartType.Line;
                Chart1.Legends[1].Enabled = true;
                Chart1.Legends[1].Docking = Docking.Bottom;
                Chart1.ChartAreas["Area1"].Area3DStyle.Enable3D = false;
                Chart1.ChartAreas["Area1"].AxisY.MajorGrid.LineWidth = 1;
                Chart1.ChartAreas["Area1"].AxisX.MajorGrid.LineWidth = 0;
                Chart1.ChartAreas["Area1"].AxisX.Interval = 1;
                Chart1.ChartAreas["Area1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 9.25F, System.Drawing.FontStyle.Regular);
                if (Convert.ToInt32(txtbulan.Text) <= 6)
                {
                    Chart1.Titles.Add(
                    new Title(
                      //" Grafik Pencapaian  : JANUARI - JUNI  " + ddlTahun.SelectedValue.ToString(),
                      " Grafik Pencapaian  : JANUARI - JUNI  " + tahun,
                      Docking.Top,
                      new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black
                      )
                      );
                }
                else
                {
                    Chart1.Titles.Add(
                    new Title(
                      //" Grafik Pencapaian  : JULI - DESEMBER  " + ddlTahun.SelectedValue.ToString(),
                      " Grafik Pencapaian  : JULI - DESEMBER  " + tahun,
                      Docking.Top,
                      new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black
                      )
                      );
                }


                lstdt.DataSource = arrData;
                lstdt.DataBind();

                #endregion
            }
            else
            {
                #region #2
                string semesterx = string.Empty;
                if (Convert.ToInt32(txtbulan.Text) >= 7)
                    semesterx = " A.Tahun=" + tahun + " and A.Bulan in (7,8,9,10,11,12) and A.RowStatus>-1 and " +
                                 " SarmutPID=" + xxx + "";
                if (Convert.ToInt32(txtbulan.Text) <= 6)
                    semesterx = " A.Tahun=" + tahun + " and A.Bulan in (1,2,3,4,5,6) and A.RowStatus>-1 and " +
                                 " SarmutPID=" + xxx + " ";
                Users users = (Users)Session["Users"];
                ArrayList arrData = new ArrayList();
                //newdata = 0;
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = " select A.SarmutPID ID,A.Tahun,A.Bulan," +
                                 " B.SarMutPerusahaan,A.Actual,C.Param,D.Satuan,E.TargetV1[Target],B.DeptID,B.TypeSarmutID from SPD_TransPrs A " +
                                 " inner join SPD_Perusahaan B on A.SarmutPID=B.ID " +
                                 " inner join SPD_Parameter C on B.ParamID=C.ID " +
                                 " inner join SPD_Satuan D on D.ID=B.SatuanID " +
                                 " inner join SPD_TargetV E on E.ID=B.TargetVID " +
                                 " where " + semesterx + " order by A.Bulan";
                //zl.CustomQuery = " select * from ( " +
                //                 " select 'A'Urt,A.SarmutPID ID,A.Tahun,A.Bulan," + 
                //                 " case when A.Bulan=0 then 'SMT' when A.Bulan=1 then 'Jan' when A.Bulan=2 then 'Feb' when A.Bulan=3 then 'Mar' when A.Bulan=4 then 'Apr' when A.Bulan=5 then 'Mei' when A.Bulan=6 then 'Jun' " +
                //                 " when A.Bulan=7 then 'Jul' when A.Bulan=8 then 'Ags' when A.Bulan=9 then 'Sep' when A.Bulan=10 then 'Okt' when A.Bulan=11 then 'Nov' when A.Bulan=12 then 'Des' " +
                //                 " end NamaBulan, " +
                //                 " B.SarMutPerusahaan,A.Actual,C.Param,D.Satuan,A.Target,B.DeptID,B.TypeSarmutID " +
                //                 " from SPD_TransPrs A  " +
                //                 " inner join SPD_Perusahaan B on A.SarmutPID=B.ID  " +
                //                 " inner join SPD_Parameter C on B.ParamID=C.ID  " +
                //                 " inner join SPD_Satuan D on D.ID=B.SatuanID " +
                //                 " where  " + semesterx + " " +
                //                 " union all " +
                //                 " select 'B'Urt,A.SarmutPID ID,A.Tahun,A.Bulan, " +
                //                 " case when A.Bulan=0 then 'SMT' when A.Bulan=1 then 'Jan' when A.Bulan=2 then 'Feb' when A.Bulan=3 then 'Mar' when A.Bulan=4 then 'Apr' when A.Bulan=5 then 'Mei' when A.Bulan=6 then 'Jun' " +
                //                 " when A.Bulan=7 then 'Jul' when A.Bulan=8 then 'Ags' when A.Bulan=9 then 'Sep' when A.Bulan=10 then 'Okt' when A.Bulan=11 then 'Nov' when A.Bulan=12 then 'Des' " +
                //                 " end NamaBulan, " +
                //                 " B.SarMutPerusahaan,A.Actual,A.ParamID[Param],A.SatuanID Satuan,C.TargetV1[Target], " +
                //                 " A.DeptID,A.TypeSarmutID " +
                //                 " from SPD_Analisa A " +
                //                 " left join SPD_Perusahaan B on A.SarmutPID=B.ID " +
                //                 " left join SPD_TargetV C on B.TargetVID=C.ID " +
                //                 " where A.AnNo='" + sarmutnotxt.Text + " ') as dtR order by Urt,Bulan";
                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new SPD_PrsAx
                        {
                            ID = Convert.ToInt32(sdr["ID"].ToString()),
                            Tahun = Convert.ToInt32(sdr["Tahun"].ToString()),
                            Bulan = Convert.ToInt32(sdr["Bulan"].ToString()),
                            //SarmutDepartemen = sdr["SarmutDepartemen"].ToString(),
                            SarMutPerusahaan = sdr["SarMutPerusahaan"].ToString(),
                            Actual = Convert.ToDecimal(sdr["Actual"].ToString()),
                            Target = Convert.ToDecimal(sdr["Target"].ToString()),
                            //NamaBulan = sdr["NamaBulan"].ToString(),
                            TypeSarmutID = Convert.ToInt32(sdr["TypeSarmutID"].ToString()),
                        });
                    }
                }

                string[] x = new string[arrData.Count];
                decimal[] z = new decimal[arrData.Count];
                decimal[] y = new decimal[arrData.Count];
                decimal[] xy = new decimal[arrData.Count];
                int i = 0;
                foreach (SPD_PrsAx lo in arrData)
                {
                    y[i] = lo.Actual;
                    //y[i] = lo.Target;
                    x[i] = lo.Bulan.ToString();
                    //x[i] = lo.NamaBulan;
                    //z[i] = lo.Target;
                    xy[i] = lo.Actual;
                    //xy[i] = lo.Target;
                    i = i + 1;
                }
                Chart1.Legends.Add("Actual");
                Chart1.Series.Add("Actual");
                Chart1.Series[0].IsVisibleInLegend = true;
                Chart1.Series[0].IsValueShownAsLabel = true;
                Chart1.Series[0].Points.DataBindXY(x, y);
                Chart1.Series[0].ChartType = SeriesChartType.Column;
                Chart1.Legends[0].Enabled = true;
                Chart1.Legends[0].Docking = Docking.Bottom;
                Chart1.Legends.Add("Target");
                Chart1.Series.Add("Target");
                Chart1.Series[1].Points.DataBindXY(x, xy);
                Chart1.Series[1].ChartType = SeriesChartType.Line;
                Chart1.Legends[1].Enabled = true;
                Chart1.Legends[1].Docking = Docking.Bottom;
                Chart1.ChartAreas["Area1"].Area3DStyle.Enable3D = false;
                Chart1.ChartAreas["Area1"].AxisY.MajorGrid.LineWidth = 1;
                Chart1.ChartAreas["Area1"].AxisX.MajorGrid.LineWidth = 0;
                Chart1.ChartAreas["Area1"].AxisX.Interval = 1;
                Chart1.ChartAreas["Area1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 9.25F, System.Drawing.FontStyle.Regular);
                if (Convert.ToInt32(txtbulan.Text) <= 6)
                {
                    Chart1.Titles.Add(
                    new Title(
                      //" Grafik Pencapaian  : JANUARI - JUNI  " + ddlTahun.SelectedValue.ToString(),
                      " Grafik Pencapaian  : JANUARI - JUNI  " + tahun,
                      Docking.Top,
                      new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black
                      )
                      );
                }
                else
                {
                    Chart1.Titles.Add(
                    new Title(
                      //" Grafik Pencapaian  : JULI - DESEMBER  " + ddlTahun.SelectedValue.ToString(),
                      " Grafik Pencapaian  : JULI - DESEMBER  " + tahun,
                      Docking.Top,
                      new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black
                      )
                      );
                }
                lstdt.DataSource = arrData;
                lstdt.DataBind();

                #endregion
            }

        }

        private void LoadDatasmt1R2(int tahun, int xxx, int xxxx)
        {
            if (Convert.ToInt32(IDx.Text) > 0)
            {
                #region #1
                string semester = string.Empty;
                //tahun = 2020;

                if (ddlSemester.SelectedItem.Text == "Semester II")
                    semester = " A.Tahun=" + tahun + " and A.Bulan in (7,8,9,10,11,12)  and A.RowStatus>-1 " +
                                 " and B.SarmutPID=" + xxx + " and B.ID=" + xxxx + "";
                if (ddlSemester.SelectedItem.Text == "Semester I")
                    semester = " A.Tahun=" + tahun + " and A.Bulan in (1,2,3,4,5,6)  and A.RowStatus>-1 " +
                                 " and B.SarmutPID=" + xxx + " and B.ID=" + xxxx + "";
                Users users = (Users)Session["Users"];
                ArrayList arrData = new ArrayList();
                //newdata = 0;
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                #region OLD
                //zl.CustomQuery = " select B.ID,A.Tahun,A.Bulan," +
                //                 " B.SarmutDepartemen,A.Actual,C.ParamID,E.[Param],isnull(G.Satuan,0)Satuan,F.TargetV1 Target, " +
                //                 " C.ID IDx,c.DeptID,D.dptID From SPD_Trans A  inner join SPD_Departemen B on B.ID=A.SarmutDeptID   inner join SPD_Perusahaan c on B.SarmutPID=C.ID " +
                //                 " inner join SPD_Dept D on C.DeptID=D.ID  inner join SPD_Parameter E on E.ID=C.ParamID  inner join SPD_TargetV F on F.ID=C.TargetVID " +
                //                 " inner join SPD_Satuan G on G.ID=C.SatuanID  where " + semester + " order by Bulan Asc ";
                #endregion
                zl.CustomQuery = " select * from ( " +
                                 " select 'A'Urt,B.ID,A.Tahun,A.Bulan,case when A.Bulan=1 then 'Jan' when A.Bulan=2 then 'Feb' when A.Bulan=3 Then 'Mar' when A.Bulan=4 then 'Apr' when A.Bulan=5 then 'Mei' when A.Bulan=6 then 'Jun' " +
                                 " when A.Bulan=7 then 'Jul' when A.Bulan=8 then 'Ags' when A.Bulan=9 then 'Sep' when A.Bulan=10 then 'Okt' when A.Bulan=11 then 'Nov' when A.Bulan=12 then 'Des' end NamaBulan, " +
                                 " B.SarmutDepartemen,A.Actual,C.ParamID,E.[Param],isnull(G.Satuan,0)Satuan,F.TargetV1 Target,  C.ID IDx,c.DeptID,D.dptID " +
                                 " From SPD_Trans A " +
                                 " inner join SPD_Departemen B on B.ID=A.SarmutDeptID " +
                                 " inner join SPD_Perusahaan c on B.SarmutPID=C.ID  " +
                                 " inner join SPD_Dept D on C.DeptID=D.ID  " +
                                 " inner join SPD_Parameter E on E.ID=C.ParamID  " +
                                 " inner join SPD_TargetV F on F.ID=C.TargetVID  " +
                                 " inner join SPD_Satuan G on G.ID=C.SatuanID  where " + semester + " " +

                                 " union all " +
                                 " select 'B'Urt,A.SarmutDeptID ID,A.Tahun,A.Bulan, " +
                                 " case when A.Bulan=0 and A.Semester='Semester I'  then 'SMT I' else 'SMT II' end NamaBulan, " +
                                 " C.SarmutDepartemen,A.Actual,'0'ParamID, " +
                                 " A.ParamID [Param],A.SatuanID Satuan,'0'Target,A.SarmutPID IDx,A.DeptID,'0'dptID " +
                                 " from SPD_Analisa A " +
                                 " left join SPD_Perusahaan B on B.ID=A.SarmutPID " +
                                 " left join SPD_Departemen C on C.ID=A.SarmutDeptID " +
                                 " where AnNo='" + sarmutnotxt.Text + "' and Tahun=" + tahun + " " +
                                 " ) as DataR order by Urt,Bulan ";
                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new SPD_PrsAx
                        {
                            ID = Convert.ToInt32(sdr["ID"].ToString()),
                            Tahun = Convert.ToInt32(sdr["Tahun"].ToString()),
                            Bulan = Convert.ToInt32(sdr["Bulan"].ToString()),
                            SarmutDepartemen = sdr["SarmutDepartemen"].ToString(),
                            //SarMutPerusahaan = sdr["SarMutPerusahaan"].ToString(),
                            NamaBulan = sdr["NamaBulan"].ToString(),
                            Actual = Convert.ToDecimal(sdr["Actual"].ToString()),
                            Target = Convert.ToDecimal(sdr["Target"].ToString()),
                        });
                    }
                }

                string[] x = new string[arrData.Count];
                decimal[] z = new decimal[arrData.Count];
                decimal[] y = new decimal[arrData.Count];
                decimal[] xy = new decimal[arrData.Count];
                int i = 0;
                foreach (SPD_PrsAx lo in arrData)
                {
                    y[i] = lo.Actual;
                    //x[i] = lo.Bulan.ToString();
                    x[i] = lo.NamaBulan;
                    //z[i] = lo.Target;
                    xy[i] = lo.Actual;
                    i = i + 1;
                }
                Chart1.Legends.Add("Actual");
                Chart1.Series.Add("Actual");
                Chart1.Series[0].IsVisibleInLegend = true;
                Chart1.Series[0].IsValueShownAsLabel = true;
                Chart1.Series[0].Points.DataBindXY(x, y);
                Chart1.Series[0].ChartType = SeriesChartType.Column;
                Chart1.Legends[0].Enabled = true;
                Chart1.Legends[0].Docking = Docking.Bottom;
                Chart1.Legends.Add("Target");
                Chart1.Series.Add("Target");
                Chart1.Series[1].Points.DataBindXY(x, xy);
                Chart1.Series[1].ChartType = SeriesChartType.Line;
                Chart1.Legends[1].Enabled = true;
                Chart1.Legends[1].Docking = Docking.Bottom;
                Chart1.ChartAreas["Area1"].Area3DStyle.Enable3D = false;
                Chart1.ChartAreas["Area1"].AxisY.MajorGrid.LineWidth = 1;
                Chart1.ChartAreas["Area1"].AxisX.MajorGrid.LineWidth = 0;
                Chart1.ChartAreas["Area1"].AxisX.Interval = 1;
                Chart1.ChartAreas["Area1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 9.25F, System.Drawing.FontStyle.Regular);
                if (ddlSemester.SelectedItem.Text == "Semester I")
                {
                    Chart1.Titles.Add(
                    new Title(
                      //" Grafik Pencapaian  : JANUARI - JUNI  " + ddlTahun.SelectedValue.ToString(),
                      " Grafik Pencapaian  : JANUARI - JUNI  " + tahun,
                      Docking.Top,
                      new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black
                      )
                      );
                }
                else
                {
                    Chart1.Titles.Add(
                    new Title(
                      //" Grafik Pencapaian  : JULI - DESEMBER  " + ddlTahun.SelectedValue.ToString(),
                      " Grafik Pencapaian  : JULI - DESEMBER  " + tahun,
                      Docking.Top,
                      new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black
                      )
                      );
                }


                lstdt.DataSource = arrData;
                lstdt.DataBind();

                #endregion
            }
            else
            {
                #region #2
                string semesterx = string.Empty;
                if (ddlSemester.SelectedItem.Text == "Semester II")
                    semesterx = " A.Tahun=" + tahun + " and A.Bulan in (7,8,9,10,11,12) and A.RowStatus>-1 and " +
                                 " SarmutPID=" + xxx + "";
                if (ddlSemester.SelectedItem.Text == "Semester I")
                    semesterx = " A.Tahun=" + tahun + " and A.Bulan in (1,2,3,4,5,6) and A.RowStatus>-1 and " +
                                 " SarmutPID=" + xxx + " ";
                Users users = (Users)Session["Users"];
                ArrayList arrData = new ArrayList();
                //newdata = 0;
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                #region Old
                //zl.CustomQuery = " select A.SarmutPID ID,A.Tahun,A.Bulan," +
                //                 " B.SarMutPerusahaan,A.Actual,C.Param,D.Satuan,E.TargetV1[Target],B.DeptID,B.TypeSarmutID from SPD_TransPrs A " +
                //                 " inner join SPD_Perusahaan B on A.SarmutPID=B.ID " +
                //                 " inner join SPD_Parameter C on B.ParamID=C.ID " +
                //                 " inner join SPD_Satuan D on D.ID=B.SatuanID " +
                //                 " inner join SPD_TargetV E on E.ID=B.TargetVID " +
                //                 " where " + semesterx + " ";
                #endregion
                zl.CustomQuery = " select * from ( " +
                                 " select 'A'Urt,A.SarmutPID ID,A.Tahun,A.Bulan," +
                                 " case  when A.Bulan=1 then 'Jan' when A.Bulan=2 then 'Feb' when A.Bulan=3 then 'Mar' when A.Bulan=4 then 'Apr' when A.Bulan=5 then 'Mei' when A.Bulan=6 then 'Jun' " +
                                 " when A.Bulan=7 then 'Jul' when A.Bulan=8 then 'Ags' when A.Bulan=9 then 'Sep' when A.Bulan=10 then 'Okt' when A.Bulan=11 then 'Nov' when A.Bulan=12 then 'Des' " +
                                 " end NamaBulan, " +
                                 " B.SarMutPerusahaan,A.Actual,C.Param,D.Satuan,isnull(A.Target,0)Target,B.DeptID,B.TypeSarmutID " +
                                 " from SPD_TransPrs A  " +
                                 " inner join SPD_Perusahaan B on A.SarmutPID=B.ID  " +
                                 " inner join SPD_Parameter C on B.ParamID=C.ID  " +
                                 " inner join SPD_Satuan D on D.ID=B.SatuanID " +
                                 " where  " + semesterx + " " +
                                 " union all " +
                                 " select 'B'Urt,A.SarmutPID ID,A.Tahun,A.Bulan, " +
                                 " case when A.Bulan=0 and A.Semester='Semester I'  then 'SMT I' else 'SMT II' " +
                                 " end NamaBulan, " +
                                 " B.SarMutPerusahaan,A.Actual,A.ParamID[Param],A.SatuanID Satuan,C.TargetV1[Target], " +
                                 " A.DeptID,A.TypeSarmutID " +
                                 " from SPD_Analisa A " +
                                 " left join SPD_Perusahaan B on A.SarmutPID=B.ID " +
                                 " left join SPD_TargetV C on B.TargetVID=C.ID " +
                                 " where A.AnNo='" + sarmutnotxt.Text + " ') as dtR order by Urt,Bulan";
                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new SPD_PrsAx
                        {
                            ID = Convert.ToInt32(sdr["ID"].ToString()),
                            Tahun = Convert.ToInt32(sdr["Tahun"].ToString()),
                            Bulan = Convert.ToInt32(sdr["Bulan"].ToString()),
                            //SarmutDepartemen = sdr["SarmutDepartemen"].ToString(),
                            SarMutPerusahaan = sdr["SarMutPerusahaan"].ToString(),
                            Actual = Convert.ToDecimal(sdr["Actual"].ToString()),
                            Target = Convert.ToDecimal(sdr["Target"].ToString()),
                            NamaBulan = sdr["NamaBulan"].ToString(),
                            TypeSarmutID = Convert.ToInt32(sdr["TypeSarmutID"].ToString()),
                        });
                    }
                }

                string[] x = new string[arrData.Count];
                decimal[] z = new decimal[arrData.Count];
                decimal[] y = new decimal[arrData.Count];
                decimal[] xy = new decimal[arrData.Count];
                int i = 0;
                foreach (SPD_PrsAx lo in arrData)
                {
                    y[i] = lo.Actual;
                    //y[i] = lo.Target;
                    //x[i] = lo.Bulan.ToString();
                    x[i] = lo.NamaBulan;
                    //z[i] = lo.Target;
                    xy[i] = lo.Actual;
                    //xy[i] = lo.Target;
                    i = i + 1;
                }
                Chart1.Legends.Add("Actual");
                Chart1.Series.Add("Actual");
                Chart1.Series[0].IsVisibleInLegend = true;
                Chart1.Series[0].IsValueShownAsLabel = true;
                Chart1.Series[0].Points.DataBindXY(x, y);
                Chart1.Series[0].ChartType = SeriesChartType.Column;
                Chart1.Legends[0].Enabled = true;
                Chart1.Legends[0].Docking = Docking.Bottom;
                Chart1.Legends.Add("Target");
                Chart1.Series.Add("Target");
                Chart1.Series[1].Points.DataBindXY(x, xy);
                Chart1.Series[1].ChartType = SeriesChartType.Line;
                Chart1.Legends[1].Enabled = true;
                Chart1.Legends[1].Docking = Docking.Bottom;
                Chart1.ChartAreas["Area1"].Area3DStyle.Enable3D = false;
                Chart1.ChartAreas["Area1"].AxisY.MajorGrid.LineWidth = 1;
                Chart1.ChartAreas["Area1"].AxisX.MajorGrid.LineWidth = 0;
                Chart1.ChartAreas["Area1"].AxisX.Interval = 1;
                Chart1.ChartAreas["Area1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 9.25F, System.Drawing.FontStyle.Regular);
                if (ddlSemester.SelectedItem.Text == "Semester I")
                {
                    Chart1.Titles.Add(
                    new Title(
                      //" Grafik Pencapaian  : JANUARI - JUNI  " + ddlTahun.SelectedValue.ToString(),
                      " Grafik Pencapaian  : JANUARI - JUNI  " + tahun,
                      Docking.Top,
                      new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black
                      )
                      );
                }
                else
                {
                    Chart1.Titles.Add(
                    new Title(
                      //" Grafik Pencapaian  : JULI - DESEMBER  " + ddlTahun.SelectedValue.ToString(),
                      " Grafik Pencapaian  : JULI - DESEMBER  " + tahun,
                      Docking.Top,
                      new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black
                      )
                      );
                }
                lstdt.DataSource = arrData;
                lstdt.DataBind();

                #endregion
            }

        }

        private void LoadDatasmt(int tahun, int xxx, int xxxx)
        {
            Users users = (Users)Session["Users"];
            ArrayList arrData = new ArrayList();
            //newdata = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = " select B.ID,A.Tahun,A.Bulan, " +
                             " B.SarmutDepartemen,A.Actual,C.ParamID,E.[Param],isnull(G.Satuan,0)Satuan,F.TargetV1 Target, " +
                             " C.ID IDx,c.DeptID,D.dptID From SPD_Trans A  inner join SPD_Departemen B on B.ID=A.SarmutDeptID   inner join SPD_Perusahaan c on B.SarmutPID=C.ID " +
                             " inner join SPD_Dept D on C.DeptID=D.ID  inner join SPD_Parameter E on E.ID=C.ParamID  inner join SPD_TargetV F on F.ID=C.TargetVID " +
                             " inner join SPD_Satuan G on G.ID=C.SatuanID  where A.Tahun=" + tahun + "  and A.RowStatus>-1 " +
                             " and B.SarmutPID=" + xxx + " and B.ID=" + xxxx + " order by Bulan ";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new SPD_PrsAx
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        Tahun = Convert.ToInt32(sdr["Tahun"].ToString()),
                        Bulan = Convert.ToInt32(sdr["Bulan"].ToString()),
                        SarmutDepartemen = sdr["SarmutDepartemen"].ToString(),
                        //SarMutPerusahaan = sdr["SarMutPerusahaan"].ToString(),
                        Actual = Convert.ToDecimal(sdr["Actual"].ToString()),
                        //NamaBulan = sdr["NamaBulan"].ToString(),
                        Target = Convert.ToDecimal(sdr["Target"].ToString()),
                    });
                }
            }

            string[] x = new string[arrData.Count];
            decimal[] z = new decimal[arrData.Count];
            decimal[] y = new decimal[arrData.Count];
            decimal[] xy = new decimal[arrData.Count];
            int i = 0;
            foreach (SPD_PrsAx lo in arrData)
            {
                y[i] = lo.Actual;
                x[i] = lo.Bulan.ToString();
                //x[i] = lo.NamaBulan;
                //z[i] = lo.Target;
                xy[i] = lo.Actual;
                i = i + 1;
            }
            Chart1.Legends.Add("Actual");
            Chart1.Series.Add("Actual");
            Chart1.Series[0].IsVisibleInLegend = true;
            Chart1.Series[0].IsValueShownAsLabel = true;
            Chart1.Series[0].Points.DataBindXY(x, y);
            Chart1.Series[0].ChartType = SeriesChartType.Column;
            Chart1.Legends[0].Enabled = true;
            Chart1.Legends[0].Docking = Docking.Bottom;
            Chart1.Legends.Add("Target");
            Chart1.Series.Add("Target");
            Chart1.Series[1].Points.DataBindXY(x, xy);
            Chart1.Series[1].ChartType = SeriesChartType.Line;
            Chart1.Legends[1].Enabled = true;
            Chart1.Legends[1].Docking = Docking.Bottom;
            Chart1.ChartAreas["Area1"].Area3DStyle.Enable3D = false;
            Chart1.ChartAreas["Area1"].AxisY.MajorGrid.LineWidth = 1;
            Chart1.ChartAreas["Area1"].AxisX.MajorGrid.LineWidth = 0;
            Chart1.ChartAreas["Area1"].AxisX.Interval = 1;
            Chart1.ChartAreas["Area1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 9.25F, System.Drawing.FontStyle.Regular);
            Chart1.Titles.Add(
               new Title(
                   " Grafik Pencapaian " + ddlBulan.SelectedItem.Text.ToString() + " " + ddlTahun.SelectedValue.ToString(),
                   Docking.Top,
                   new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black
                   )
                   );
            lstdt.DataSource = arrData;
            lstdt.DataBind();
        }

        private void LoadDatasmtx0(int tahun, int xxx, int xxxx, string smt)
        {
            string kriteria = string.Empty;
            if (smt == "Semester I")
            {
                kriteria = kriteria + " A.Bulan in (1,2,3,4,5,6) ";
            }
            else
            {
                kriteria = kriteria + "A.Bulan in (7,8,9,10,11,12) ";
            }
            Users users = (Users)Session["Users"];
            ArrayList arrData = new ArrayList();
            //newdata = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = " select B.ID,A.Tahun,A.Bulan, " +
                             " B.SarmutDepartemen,A.Actual,C.ParamID,E.[Param],isnull(G.Satuan,0)Satuan,F.TargetV1 Target, " +
                             " C.ID IDx,c.DeptID,D.dptID From SPD_Trans A  inner join SPD_Departemen B on B.ID=A.SarmutDeptID   inner join SPD_Perusahaan c on B.SarmutPID=C.ID " +
                             " inner join SPD_Dept D on C.DeptID=D.ID  inner join SPD_Parameter E on E.ID=C.ParamID  inner join SPD_TargetV F on F.ID=C.TargetVID " +
                             " inner join SPD_Satuan G on G.ID=C.SatuanID  where A.Tahun=" + tahun + "  and A.RowStatus>-1 " +
                             " and B.SarmutPID=" + xxx + " and B.ID=" + xxxx + " and " + kriteria + "  " +
                             " order by Bulan ";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new SPD_PrsAx
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        Tahun = Convert.ToInt32(sdr["Tahun"].ToString()),
                        Bulan = Convert.ToInt32(sdr["Bulan"].ToString()),
                        SarmutDepartemen = sdr["SarmutDepartemen"].ToString(),
                        //SarMutPerusahaan = sdr["SarMutPerusahaan"].ToString(),
                        Actual = Convert.ToDecimal(sdr["Actual"].ToString()),
                        //NamaBulan = sdr["NamaBulan"].ToString(),
                        Target = Convert.ToDecimal(sdr["Target"].ToString()),
                    });
                }
            }

            string[] x = new string[arrData.Count];
            decimal[] z = new decimal[arrData.Count];
            decimal[] y = new decimal[arrData.Count];
            decimal[] xy = new decimal[arrData.Count];
            int i = 0;
            foreach (SPD_PrsAx lo in arrData)
            {
                y[i] = lo.Actual;
                x[i] = lo.Bulan.ToString();
                //x[i] = lo.NamaBulan;
                //z[i] = lo.Target;
                xy[i] = lo.Actual;
                i = i + 1;
            }
            Chart1.Legends.Add("Actual");
            Chart1.Series.Add("Actual");
            Chart1.Series[0].IsVisibleInLegend = true;
            Chart1.Series[0].IsValueShownAsLabel = true;
            Chart1.Series[0].Points.DataBindXY(x, y);
            Chart1.Series[0].ChartType = SeriesChartType.Column;
            Chart1.Legends[0].Enabled = true;
            Chart1.Legends[0].Docking = Docking.Bottom;
            Chart1.Legends.Add("Target");
            Chart1.Series.Add("Target");
            Chart1.Series[1].Points.DataBindXY(x, xy);
            Chart1.Series[1].ChartType = SeriesChartType.Line;
            Chart1.Legends[1].Enabled = true;
            Chart1.Legends[1].Docking = Docking.Bottom;
            Chart1.ChartAreas["Area1"].Area3DStyle.Enable3D = false;
            Chart1.ChartAreas["Area1"].AxisY.MajorGrid.LineWidth = 1;
            Chart1.ChartAreas["Area1"].AxisX.MajorGrid.LineWidth = 0;
            Chart1.ChartAreas["Area1"].AxisX.Interval = 1;
            Chart1.ChartAreas["Area1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 9.25F, System.Drawing.FontStyle.Regular);
            Chart1.Titles.Add(
               new Title(
                   " Grafik Pencapaian " + ddlSemester.SelectedItem.Text.ToString() + " " + ddlTahun.SelectedValue.ToString(),
                   Docking.Top,
                   new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black
                   )
                   );
            lstdt.DataSource = arrData;
        }

        private void LoadDatasmt0(int tahun, int xxx)
        {
            Users users = (Users)Session["Users"];
            ArrayList arrData = new ArrayList();
            //newdata = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = " select A.SarmutPID ID,A.Tahun,A.Bulan, " +
                             " B.SarMutPerusahaan,A.Actual,C.Param,D.Satuan,E.TargetV1[Target],B.DeptID,B.TypeSarmutID from SPD_TransPrs A " +
                             " inner join SPD_Perusahaan B on A.SarmutPID=B.ID " +
                             " inner join SPD_Parameter C on B.ParamID=C.ID " +
                             " inner join SPD_Satuan D on D.ID=B.SatuanID " +
                             " inner join SPD_TargetV E on E.ID=B.TargetVID " +
                             " where A.Tahun=" + tahun + " and A.RowStatus>-1 and " +
                             " SarmutPID=" + xxx + " order by Bulan";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new SPD_PrsAx
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        Tahun = Convert.ToInt32(sdr["Tahun"].ToString()),
                        Bulan = Convert.ToInt32(sdr["Bulan"].ToString()),
                        //NamaBulan = sdr["NamaBulan"].ToString(),
                        SarMutPerusahaan = sdr["SarMutPerusahaan"].ToString(),
                        Actual = Convert.ToDecimal(sdr["Actual"].ToString()),
                        Target = Convert.ToDecimal(sdr["Target"].ToString()),
                        TypeSarmutID = Convert.ToInt32(sdr["TypeSarmutID"].ToString()),
                    });
                }
            }

            string[] x = new string[arrData.Count];
            decimal[] z = new decimal[arrData.Count];
            decimal[] y = new decimal[arrData.Count];
            decimal[] xy = new decimal[arrData.Count];
            int i = 0;
            foreach (SPD_PrsAx lo in arrData)
            {
                y[i] = lo.Actual;
                //y[i] = lo.Target;
                x[i] = lo.Bulan.ToString();
                //x[i] = lo.NamaBulan;
                //z[i] = lo.Target;
                xy[i] = lo.Actual;
                //xy[i] = lo.Target;
                i = i + 1;
            }
            Chart1.Legends.Add("Actual");
            Chart1.Series.Add("Actual");
            Chart1.Series[0].IsVisibleInLegend = true;
            Chart1.Series[0].IsValueShownAsLabel = true;
            Chart1.Series[0].Points.DataBindXY(x, y);
            Chart1.Series[0].ChartType = SeriesChartType.Column;
            Chart1.Legends[0].Enabled = true;
            Chart1.Legends[0].Docking = Docking.Bottom;
            Chart1.Legends.Add("Target");
            Chart1.Series.Add("Target");
            Chart1.Series[1].Points.DataBindXY(x, xy);
            Chart1.Series[1].ChartType = SeriesChartType.Line;
            Chart1.Legends[1].Enabled = true;
            Chart1.Legends[1].Docking = Docking.Bottom;
            Chart1.ChartAreas["Area1"].Area3DStyle.Enable3D = false;
            Chart1.ChartAreas["Area1"].AxisY.MajorGrid.LineWidth = 1;
            Chart1.ChartAreas["Area1"].AxisX.MajorGrid.LineWidth = 0;
            Chart1.ChartAreas["Area1"].AxisX.Interval = 1;
            Chart1.ChartAreas["Area1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 9.25F, System.Drawing.FontStyle.Regular);
            Chart1.Titles.Add(
               new Title(
                   " Grafik Pencapaian " + ddlBulan.SelectedItem.Text.ToString() + " " + ddlTahun.SelectedValue.ToString(),
                   Docking.Top,
                   new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black
                   )
                   );
            lstdt.DataSource = arrData;
            lstdt.DataBind();


        }

        private void LoadDatasmt0x(int tahun, int xxx, string smt)
        {
            string kriteria = string.Empty;
            if (smt == "Semester I")
            {
                kriteria = kriteria + " A.Bulan in (1,2,3,4,5,6) ";
            }
            else
            {
                kriteria = kriteria + "A.Bulan in (7,8,9,10,11,12) ";
            }
            Users users = (Users)Session["Users"];
            ArrayList arrData = new ArrayList();
            //newdata = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = " select A.SarmutPID ID,A.Tahun,A.Bulan, " +
                             " case when A.Bulan =1 then 'Jan' when A.Bulan=2 then 'Feb' when A.Bulan=3 then 'Mar' when A.Bulan=4 then 'Apr' when A.Bulan=5 then 'Mei' when A.Bulan=6 then 'Jun' " +
                             " when A.Bulan=7 then 'Jul' when A.Bulan=8 then 'Agst' when A.Bulan=9 then 'Sep' when A.Bulan=10 then 'Okt' when A.Bulan=11 then 'Nov' when A.Bulan=12 then 'Des'  end NamaBulan, " +
                             " B.SarMutPerusahaan,A.Actual,C.Param,D.Satuan,E.TargetV1[Target],B.DeptID,B.TypeSarmutID from SPD_TransPrs A " +
                             " inner join SPD_Perusahaan B on A.SarmutPID=B.ID " +
                             " inner join SPD_Parameter C on B.ParamID=C.ID " +
                             " inner join SPD_Satuan D on D.ID=B.SatuanID " +
                             " inner join SPD_TargetV E on E.ID=B.TargetVID " +
                             " where A.Tahun=" + tahun + " and A.RowStatus>-1 and " + kriteria + "  " +
                             " and SarmutPID=" + xxx + " order by Bulan";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new SPD_PrsAx
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        Tahun = Convert.ToInt32(sdr["Tahun"].ToString()),
                        Bulan = Convert.ToInt32(sdr["Bulan"].ToString()),
                        NamaBulan = sdr["NamaBulan"].ToString(),
                        SarMutPerusahaan = sdr["SarMutPerusahaan"].ToString(),
                        Actual = Convert.ToDecimal(sdr["Actual"].ToString()),
                        Target = Convert.ToDecimal(sdr["Target"].ToString()),
                        TypeSarmutID = Convert.ToInt32(sdr["TypeSarmutID"].ToString()),
                    });
                }
            }

            string[] x = new string[arrData.Count];
            decimal[] z = new decimal[arrData.Count];
            decimal[] y = new decimal[arrData.Count];
            decimal[] xy = new decimal[arrData.Count];
            int i = 0;
            foreach (SPD_PrsAx lo in arrData)
            {
                y[i] = lo.Actual;
                //y[i] = lo.Target;
                x[i] = lo.NamaBulan.ToString();
                //x[i] = lo.NamaBulan;
                //z[i] = lo.Target;
                xy[i] = lo.Actual;
                //xy[i] = lo.Target;
                i = i + 1;
            }
            Chart1.Legends.Add("Actual");
            Chart1.Series.Add("Actual");
            Chart1.Series[0].IsVisibleInLegend = true;
            Chart1.Series[0].IsValueShownAsLabel = true;
            Chart1.Series[0].Points.DataBindXY(x, y);
            Chart1.Series[0].ChartType = SeriesChartType.Column;
            Chart1.Legends[0].Enabled = true;
            Chart1.Legends[0].Docking = Docking.Bottom;
            Chart1.Legends.Add("Target");
            Chart1.Series.Add("Target");
            Chart1.Series[1].Points.DataBindXY(x, xy);
            Chart1.Series[1].ChartType = SeriesChartType.Line;
            Chart1.Legends[1].Enabled = true;
            Chart1.Legends[1].Docking = Docking.Bottom;
            Chart1.ChartAreas["Area1"].Area3DStyle.Enable3D = false;
            Chart1.ChartAreas["Area1"].AxisY.MajorGrid.LineWidth = 1;
            Chart1.ChartAreas["Area1"].AxisX.MajorGrid.LineWidth = 0;
            Chart1.ChartAreas["Area1"].AxisX.Interval = 1;
            Chart1.ChartAreas["Area1"].AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 9.25F, System.Drawing.FontStyle.Regular);
            Chart1.Titles.Add(
               new Title(
                   " Grafik Pencapaian " + ddlSemester.SelectedItem.Text.ToString() + " " + ddlTahun.SelectedValue.ToString(),
                   Docking.Top,
                   new System.Drawing.Font("Arial", 8, System.Drawing.FontStyle.Bold), System.Drawing.Color.Black
                   )
                   );
            lstdt.DataSource = arrData;
            lstdt.DataBind();
        }

        protected void lstdt_DataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void ddlsmtDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlsarmutDept.SelectedIndex <= 0)
                return;
            try
            {
                if (ddlsarmutDept.SelectedIndex > 0)
                {
                    FacadePrsaX deptx = new FacadePrsaX();
                    SPD_PrsAx xxx = new SPD_PrsAx();
                    if (RBAnalisa.Checked == true)
                    {
                        xxx = deptx.RetrieveById(int.Parse(ddlBulan.SelectedValue), (int.Parse(ddlTahun.SelectedValue)), (int.Parse(ddlsmtP.SelectedValue)), (int.Parse(ddlsarmutDept.SelectedValue)));
                        
                    }
                    if (RBSemester.Checked == true)
                    {
                        xxx = deptx.RetrieveByIdxxx1z((ddlSemester.SelectedValue).ToString(), (int.Parse(ddlTahun.SelectedValue)), (int.Parse(ddlsmtP.SelectedValue)), (int.Parse(ddlsarmutDept.SelectedValue)));
                    }
                    if (xxx.IDX == 0)
                        return;
                    txtTargetx.Text = xxx.Target.ToString();
                    txtActual.Text = xxx.Actual.ToString();
                    txtSatuan.Text = xxx.Satuan.ToString();
                    txtParam.Text = xxx.Param.ToString();
                    Label1.Text = xxx.Satuan.ToString();
                    IDx.Text = xxx.SarmutDeptID.ToString();
                    //IDx.Text = xxx.IDX.ToString();
                    decimal pencapaian = Convert.ToDecimal(txtActual.Text);
                    decimal PTarget = Convert.ToDecimal(txtTargetx.Text);
                    if (pencapaian > PTarget)
                    {
                        chkTTercapai.Checked = true;
                    }
                    else
                    {
                        chkTercapai.Checked = false;
                    }
                }
                if(sarmutnotxt.Text.Trim()==string.Empty )
                    chkTTercapai.Checked = true;
                if (RBAnalisa.Checked == true)
                {
                    LoadDatasmt((int.Parse(ddlTahun.SelectedValue)), (int.Parse(ddlsmtP.SelectedValue)), (int.Parse(ddlsarmutDept.SelectedValue)));
                }
                if (RBSemester.Checked == true)
                {
                    LoadDatasmtx0((int.Parse(ddlTahun.SelectedValue)), (int.Parse(ddlsmtP.SelectedValue)), (int.Parse(ddlsarmutDept.SelectedValue)), ((ddlSemester.SelectedValue).ToString()));
                }
                if (sarmutnotxt.Text.Trim()==string.Empty )
                {
                    btnAddPencegahan.Visible = false;
                    btnAddPerbaikan.Visible = false;
                }
                else
                {
                    btnAddPencegahan.Visible = true;
                    btnAddPerbaikan.Visible = true;
                }
            }
            catch { }

        }

        protected void Target_Change(object sender, EventArgs e)
        {

        }

        protected void actual_Change(object sender, EventArgs e)
        {

        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),"MyScript", myScript, true);
        }
       

    }

    public class FacadePrsaX
    {
        public string strError = string.Empty;
        private ArrayList arrData = new ArrayList();
        private List<SqlParameter> sqlListParam;
        private SPD_PrsAx objSPD = new SPD_PrsAx();

        public FacadePrsaX()
            : base()
        {

        }

        public int insertAnSarmut(object objDomain)
        {
            try
            {
                objSPD = (SPD_PrsAx)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Old_sarmutNo", objSPD.Old_sarmutNo));
                sqlListParam.Add(new SqlParameter("@AnNo", objSPD.AnNo));
                sqlListParam.Add(new SqlParameter("@TglAnalisa", objSPD.TglAnalisa));
                sqlListParam.Add(new SqlParameter("@DeptID", objSPD.dptID));
                sqlListParam.Add(new SqlParameter("@SarmutPID", objSPD.SarmutPID));
                sqlListParam.Add(new SqlParameter("@SarmutDeptID", objSPD.SarmutDeptID));
                sqlListParam.Add(new SqlParameter("@TargetVID", objSPD.TargetVID));
                sqlListParam.Add(new SqlParameter("@ParamID", objSPD.ParamID));
                sqlListParam.Add(new SqlParameter("@SatuanID", objSPD.SatuanID));
                sqlListParam.Add(new SqlParameter("@TypeSarmutID", objSPD.TypeSarmutID));
                sqlListParam.Add(new SqlParameter("@Tahun", objSPD.Tahun));
                sqlListParam.Add(new SqlParameter("@Bulan", objSPD.Bulan));
                sqlListParam.Add(new SqlParameter("@Actual", objSPD.Actual));
                sqlListParam.Add(new SqlParameter("@Kesim", objSPD.Kesim));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objSPD.CreatedBy));
                sqlListParam.Add(new SqlParameter("@User_ID", objSPD.User_ID));
                sqlListParam.Add(new SqlParameter("@Semester", objSPD.Semester));
                DataAccess da = new DataAccess(Global.ConnectionString());
                int result = da.ProcessData(sqlListParam, "AnSarmut_Insert");
                strError = da.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int insertPdetail(object objDomain)
        {
            try
            {
                objSPD = (SPD_PrsAx)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Penyebab_ID", objSPD.Penyebab_ID));
                sqlListParam.Add(new SqlParameter("@SPDAnalisaID", objSPD.SPDAnalisaID));
                sqlListParam.Add(new SqlParameter("@Uraian", objSPD.Uraian));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objSPD.CreatedBy));
                sqlListParam.Add(new SqlParameter("@RowStatus", objSPD.RowStatus));
                DataAccess da = new DataAccess(Global.ConnectionString());
                int result = da.ProcessData(sqlListParam, "AnSarmut_Pdetail");
                strError = da.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }


        //penambahan agus 12-08-2022
        public string UpdateTindakanPerbaikan(string ID, string editperbaikan, string editpelaksana)
        {
            string strSQL = "update SPD_Tindakan set Tindakan='" + editperbaikan + "', Pelaku='" + editpelaksana + "' where ID=" + ID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }
        //penambahan agus 12-08-2022


        //penambahan agus 12-08-2022
        public string CekApproval(string cekno)
        {
            string hasil = string.Empty;

            string strsql = "select Apv from SPD_Analisa where AnNo='" + cekno + "'";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strsql);

            if (da.Error == string.Empty || sdr.HasRows)
            {
                while (sdr.Read())
                {
                    hasil = sdr["Apv"].ToString();

                }
            }

            return hasil;
        }
        //penambahan agus 12-08-2022


        //penambahan agus 12-08-2022
        public string UpdateTindakanPencegahan(string ID, string editperbaikan, string editpelaksana)
        {
            string strSQL = "update SPD_Tindakan set Tindakan='" + editperbaikan + "', Pelaku='" + editpelaksana + "' where ID=" + ID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }
        //penambahan agus 12-08-2022

        public int InsertTindakan(object objDomain)
        {
            try
            {
                objSPD = (SPD_PrsAx)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@SPDAnalisaID", objSPD.SPDAnalisaID));
                sqlListParam.Add(new SqlParameter("@Tindakan", objSPD.Tindakan));
                sqlListParam.Add(new SqlParameter("@Pelaku", objSPD.Pelaku));
                sqlListParam.Add(new SqlParameter("@Jadwal_Selesai", objSPD.Jadwal_Selesai));
                //sqlListParam.Add(new SqlParameter("@Aktual_Selesai", objTPP.Aktual_Selesai));
                sqlListParam.Add(new SqlParameter("@Jenis", objSPD.Jenis));
                //sqlListParam.Add(new SqlParameter("@Targetx", objSPD.Targetx));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objSPD.CreatedBy));
                DataAccess da = new DataAccess(Global.ConnectionString());
                int result = da.ProcessData(sqlListParam, "Insert_TindakanAnalisa");
                strError = da.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public SPD_PrsAx RetrieveByIdxxx1z(string smt, int tahun, int smtp, int sarmutDept)
        {
            string kriteria = string.Empty;
            if (smt == "Semester I")
            {
                kriteria = kriteria + " A.Bulan in (1,2,3,4,5,6) ";
            }
            else
            {
                kriteria = kriteria + " A.Bulan in (7,8,9,10,11,12) ";
            }

            string StrSql = " select B.ID,A.Tahun,A.Bulan,B.SarmutDepartemen,isnull(A.Actual,0)Actual,C.ParamID,E.[Param],isnull(G.Satuan,0)Satuan,F.TargetV1 Target,C.ID IDx,c.DeptID,D.dptID From SPD_Trans A " +
                         " inner join SPD_Departemen B on B.ID=A.SarmutDeptID  " +
                         " inner join SPD_Perusahaan c on B.SarmutPID=C.ID  " +
                         " inner join SPD_Dept D on C.DeptID=D.ID " +
                         " inner join SPD_Parameter E on E.ID=C.ParamID " +
                         " inner join SPD_TargetV F on F.ID=B.TargetVID " +
                         " inner join SPD_Satuan G on G.ID=C.SatuanID " +
                         " where A.Tahun=" + tahun + " and " + kriteria + "  " +
                         " and A.RowStatus>-1 and B.SarmutPID=" + smtp + " and B.ID=" + sarmutDept + "";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new SPD_PrsAx();
        }

        public SPD_PrsAx RetrieveById(int tahun, int bulan, int smtp, int sarmutDept)
        {

            string StrSql = " select B.ID,A.Tahun,A.Bulan,B.SarmutDepartemen,isnull(A.Actual,0)Actual,C.ParamID,E.[Param],isnull(G.Satuan,0)Satuan,F.TargetV1 Target,C.ID IDx,c.DeptID,D.dptID From SPD_Trans A " +
                         " inner join SPD_Departemen B on B.ID=A.SarmutDeptID  " +
                         " inner join SPD_Perusahaan c on B.SarmutPID=C.ID  " +
                         " inner join SPD_Dept D on C.DeptID=D.ID " +
                         " inner join SPD_Parameter E on E.ID=C.ParamID " +
                         " inner join SPD_TargetV F on F.ID=B.TargetVID " +
                         " inner join SPD_Satuan G on G.ID=C.SatuanID " +
                         " where A.Tahun=" + bulan + " and A.Bulan=" + tahun + " and A.RowStatus>-1  " +
                         " and B.SarmutPID=" + smtp + " and B.ID=" + sarmutDept + "";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new SPD_PrsAx();
        }


        public SPD_PrsAx RetrieveByIdR(string smt, int tahun, int smtp, int sarmutDept)
        {
            string kriteria = string.Empty;
            if (smt == "Semester I")
            {
                kriteria = kriteria + " A.Bulan in (1,2,3,4,5,6) ";
            }
            else
            {
                kriteria = kriteria + " A.Bulan in (7,8,9,10,11,12) ";
            }
            string StrSql = " select B.ID,A.Tahun,A.Bulan,B.SarmutDepartemen,isnull(A.Actual,0)Actual,C.ParamID,E.[Param],isnull(G.Satuan,0)Satuan,F.TargetV1 Target,C.ID IDx,c.DeptID,D.dptID From SPD_Trans A " +
                         " inner join SPD_Departemen B on B.ID=A.SarmutDeptID  " +
                         " inner join SPD_Perusahaan c on B.SarmutPID=C.ID  " +
                         " inner join SPD_Dept D on C.DeptID=D.ID " +
                         " inner join SPD_Parameter E on E.ID=C.ParamID " +
                         " inner join SPD_TargetV F on F.ID=B.TargetVID " +
                         " inner join SPD_Satuan G on G.ID=C.SatuanID " +
                         " where A.Tahun=" + tahun + " and " + kriteria + "  " +
                         " and A.RowStatus>-1 and B.SarmutPID=" + smtp + " and B.ID=" + sarmutDept + "";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new SPD_PrsAx();
        }

        public ArrayList RetrieveAllMonth(string bulan, string Tahun, string Dept_ID)
        {
            string kriteria = string.Empty;
            if (bulan != "0")
                kriteria = kriteria + " and Month(Due_date)= " + bulan;
            if (Dept_ID != "0")
                kriteria = kriteria + " and AnalisaPemantaunx.DeptID=" + Dept_ID;
            string strSQL = " with AnalisaPemantaunx as ( " +
                            " select A.DeptID,A.CreatedTime,A.ID ID,isnull(C.Dept,'')Dept,A.AnNo,A.TglAnalisa,A.SarmutPID,B.SarMutPerusahaan,A.SarmutDeptID, " +
                            " isnull(D.SarmutDepartemen,'')SarmutDepartemen,A.TargetVID,A.ParamID,A.SatuanID,A.TypeSarmutID,E.JenisSarmut Jenis,A.Apv," +
                            " case when Apv=0 then 'Admin' when apv is null then 'Admin' " +
                            " when apv=1 then 'Manager Dept' when apv=2 then 'ISO' end Approval,isnull(A.CLosed,0)Closed,isnull(A.Close_Date,'1/1/1900')Close_Date, " +
                            " isnull(A.Solved,0)Solved,isnull(A.Solve_Date,'1/1/1900')Solve_Date, " +
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
                            " end NamaBulan ,A.Semester, " +
                            " A.Tahun,A.Actual,case when A.Kesim=1 then 'Tercapai' else 'Tidak Tercapai' end Ket,A.Kesim,A.RowStatus, " +
                            " (select COUNT(ID)verified from SPD_Tindakan where Verifikasi=1 and SPDAnalisaID=A.ID)Verifikasi, " +
                            " (select COUNT(ID)verified from SPD_Tindakan where Verifikasi=0 and SPDAnalisaID=A.ID)notverified," +
                            " isnull((select top 1 Jadwal_Selesai from SPD_Tindakan where SPDAnalisaID =A.ID and RowStatus>-1 order by Jadwal_Selesai desc),'1/1/1900') due_date,isnull(apvmgr,'1/1/1900')apvmgr  " +
                            " from SPD_Analisa A  left join SPD_Perusahaan B on A.SarmutPID=B.ID left join SPD_Dept C on A.Deptid=C.ID " +
                            " left join SPD_Departemen D on A.SarmutDeptID=D.ID inner join SPD_Type E on A.TypeSarmutID=E.ID  " +
                            " where A.RowStatus>-1 " +
                            " ) select * from AnalisaPemantaunx where Year(Due_date)='" + Tahun + "'" + kriteria;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObjectAll(sqlDataReader));
                }
            }
            else
                arrData.Add(new SPD_PrsAx());
            return arrData;
        }

        public ArrayList RetrieveAllOpen(string bulan, string Tahun, string Dept_ID)
        {
            string kriteria = string.Empty;
            if (bulan != "0")
                kriteria = kriteria + " and Month(Due_date)= " + bulan;
            if (Dept_ID != "0")
                kriteria = kriteria + " and AnalisaPemantaunx.DeptID=" + Dept_ID;
            string strSQL = " with AnalisaPemantaunx as ( " +
                            " select A.DeptID,A.CreatedTime,A.ID ID,isnull(C.Dept,'')Dept,A.AnNo,A.TglAnalisa,A.SarmutPID,B.SarMutPerusahaan,A.SarmutDeptID, " +
                            " isnull(D.SarmutDepartemen,'')SarmutDepartemen,A.TargetVID,A.ParamID,A.SatuanID,A.TypeSarmutID,E.JenisSarmut Jenis,A.Apv," +
                            " case when Apv=0 then 'Admin' when apv is null then 'Admin' " +
                            " when apv=1 then 'Manager Dept' when apv=2 then 'ISO' end Approval,isnull(A.CLosed,0)Closed,isnull(A.Close_Date,'1/1/1900')Close_Date, " +
                            " isnull(A.Solved,0)Solved,isnull(A.Solve_Date,'1/1/1900')Solve_Date, " +
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
                            " end NamaBulan ,A.Semester, " +
                            " A.Tahun,A.Actual,case when A.Kesim=1 then 'Tercapai' else 'Tidak Tercapai' end Ket,A.Kesim,A.RowStatus, " +
                            " (select COUNT(ID)verified from SPD_Tindakan where Verifikasi=1 and SPDAnalisaID=A.ID)Verifikasi, " +
                            " (select COUNT(ID)verified from SPD_Tindakan where Verifikasi=0 and SPDAnalisaID=A.ID)notverified," +
                            " isnull((select top 1 Jadwal_Selesai from SPD_Tindakan where SPDAnalisaID =A.ID and RowStatus>-1 order by Jadwal_Selesai desc),'1/1/1900') due_date,isnull(apvmgr,'1/1/1900')apvmgr  " +
                            " from SPD_Analisa A  left join SPD_Perusahaan B on A.SarmutPID=B.ID left join SPD_Dept C on A.Deptid=C.ID " +
                            " left join SPD_Departemen D on A.SarmutDeptID=D.ID inner join SPD_Type E on A.TypeSarmutID=E.ID  " +
                            " where A.RowStatus>-1 " +
                            " ) select * from AnalisaPemantaunx where AnalisaPemantaunx.Closed=0 and Year(Due_date)='" + Tahun + "'" + kriteria;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObjectAll(sqlDataReader));
                }
            }
            else
                arrData.Add(new SPD_PrsAx());
            return arrData;
        }

        public ArrayList RetrieveAllClosed(string bulan, string Tahun, string Dept_ID)
        {
            string kriteria = string.Empty;
            if (bulan != "0")
                kriteria = kriteria + " and Month(Due_date)= " + bulan;
            if (Dept_ID != "0")
                kriteria = kriteria + " and AnalisaPemantaunx.DeptID=" + Dept_ID;
            string strSQL = " with AnalisaPemantaunx as ( " +
                            " select A.DeptID,A.CreatedTime,A.ID ID,isnull(C.Dept,'')Dept,A.AnNo,A.TglAnalisa,A.SarmutPID,B.SarMutPerusahaan,A.SarmutDeptID, " +
                            " isnull(D.SarmutDepartemen,'')SarmutDepartemen,A.TargetVID,A.ParamID,A.SatuanID,A.TypeSarmutID,E.JenisSarmut Jenis,A.Apv," +
                            " case when Apv=0 then 'Admin' when apv is null then 'Admin' " +
                            " when apv=1 then 'Manager Dept' when apv=2 then 'ISO' end Approval,isnull(A.CLosed,0)Closed,isnull(A.Close_Date,'1/1/1900')Close_Date, " +
                            " isnull(A.Solved,0)Solved,isnull(A.Solve_Date,'1/1/1900')Solve_Date, " +
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
                            " end NamaBulan,A.Semester, " +
                            " A.Tahun,A.Actual,case when A.Kesim=1 then 'Tercapai' else 'Tidak Tercapai' end Ket,A.Kesim,A.RowStatus, " +
                            " (select COUNT(ID)verified from SPD_Tindakan where Verifikasi=1 and SPDAnalisaID=A.ID)Verifikasi, " +
                            " (select COUNT(ID)verified from SPD_Tindakan where Verifikasi=0 and SPDAnalisaID=A.ID)notverified," +
                            " isnull((select top 1 Jadwal_Selesai from SPD_Tindakan where SPDAnalisaID =A.ID and RowStatus>-1 order by Jadwal_Selesai desc),'1/1/1900') due_date,isnull(apvmgr,'1/1/1900')apvmgr  " +
                            " from SPD_Analisa A  left join SPD_Perusahaan B on A.SarmutPID=B.ID left join SPD_Dept C on A.Deptid=C.ID " +
                            " left join SPD_Departemen D on A.SarmutDeptID=D.ID inner join SPD_Type E on A.TypeSarmutID=E.ID  " +
                            " where A.RowStatus>-1 " +
                            " ) select * from AnalisaPemantaunx where AnalisaPemantaunx.Closed=1 and Year(Due_date)='" + Tahun + "'" + kriteria;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObjectAll(sqlDataReader));
                }
            }
            else
                arrData.Add(new SPD_PrsAx());
            return arrData;
        }

        public ArrayList RetrieveAll()
        {
            #region old
            //string strSQL = " select A.DeptID,A.CreatedTime,A.ID ID,isnull(C.Dept,'')Dept,A.AnNo,A.TglAnalisa,A.SarmutPID,B.SarMutPerusahaan,A.SarmutDeptID, " +
            //                " isnull(D.SarmutDepartemen,'')SarmutDepartemen,A.TargetVID,A.ParamID,A.SatuanID,A.TypeSarmutID,E.JenisSarmut Jenis," +
            //                " A.Bulan,A.Tahun,A.Actual,case when A.Kesim=1 then 'Tercapai' else 'Tidak Tercapai' end Ket,A.Kesim,A.RowStatus," +
            //                " (select COUNT(ID)verified from SPD_Tindakan where Verifikasi=1 and SPDAnalisaID=A.ID)verified, " +
            //                " (select COUNT(ID)verified from SPD_Tindakan where Verifikasi=0 and SPDAnalisaID=A.ID)notverified," +
            //                " isnull((select top 1 Jadwal_Selesai from SPD_Tindakan where SPDAnalisaID =A.ID and RowStatus>-1 order by Jadwal_Selesai desc),'1/1/1900') due_date " +
            //                " from SPD_Analisa A  left join SPD_Perusahaan B on A.SarmutPID=B.ID left join SPD_Dept C on A.Deptid=C.ID " +
            //                " left join SPD_Departemen D on A.SarmutDeptID=D.ID inner join SPD_Type E on A.TypeSarmutID=E.ID " +
            //                " where A.RowStatus>-1 order by A.TglAnalisa Desc,C.Dept ";
            #endregion
            string strSQL = " select A.DeptID,A.CreatedTime,A.ID ID,isnull(C.Dept,'')Dept,A.AnNo,A.TglAnalisa,A.SarmutPID,B.SarMutPerusahaan,A.SarmutDeptID, " +
                            " isnull(D.SarmutDepartemen,'')SarmutDepartemen,A.TargetVID,A.ParamID,A.SatuanID,A.TypeSarmutID,E.JenisSarmut Jenis,A.Apv," +
                            " case when Apv=0 then 'Admin' when apv is null then 'Admin' " +
                            " when apv=1 then 'Manager Dept' when apv=2 then 'ISO' end Approval,isnull(A.CLosed,0)Closed,isnull(A.Close_Date,'1/1/1900')Close_Date," +
                            " isnull(A.Solved,0)Solved,isnull(A.Solve_Date,'1/1/1900')Solve_Date," +
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
                            " end NamaBulan,A.Semester, " +
                            " A.Tahun,A.Actual,case when A.Kesim=1 then 'Tercapai' else 'Tidak Tercapai' end Ket,A.Kesim,A.RowStatus," +
                            " (select COUNT(ID)verified from SPD_Tindakan where Verifikasi=1 and SPDAnalisaID=A.ID)Verifikasi," +
                            " (select COUNT(ID)verified from SPD_Tindakan where Verifikasi=0 and SPDAnalisaID=A.ID)notverified," +
                            " isnull((select top 1 Jadwal_Selesai from SPD_Tindakan where SPDAnalisaID =A.ID and RowStatus>-1 order by Jadwal_Selesai desc),'1/1/1900') due_date,isnull(apvmgr,'1/1/1900')apvmgr " +
                            " from SPD_Analisa A  left join SPD_Perusahaan B on A.SarmutPID=B.ID left join SPD_Dept C on A.Deptid=C.ID " +
                            " left join SPD_Departemen D on A.SarmutDeptID=D.ID inner join SPD_Type E on A.TypeSarmutID=E.ID " +
                            " where A.RowStatus>-1 order by A.TglAnalisa Desc,C.Dept ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObjectAll(sqlDataReader));
                }
            }
            else
                arrData.Add(new SPD_PrsAx());
            return arrData;
        }

        public ArrayList RetrieveByKriteria(string kriteria)
        {
            string strSQL = " select A.DeptID,A.CreatedTime,A.ID ID,isnull(C.Dept,'')Dept,A.AnNo,A.TglAnalisa,A.SarmutPID,B.SarMutPerusahaan,A.SarmutDeptID, " +
                            " isnull(D.SarmutDepartemen,'')SarmutDepartemen,A.TargetVID,A.ParamID,A.SatuanID,A.TypeSarmutID,E.JenisSarmut Jenis,A.Apv," +
                            " case when Apv=0 then 'Admin' when apv is null then 'Admin' " +
                            " when apv=1 then 'Manager Dept' when apv=2 then 'ISO' end Approval,isnull(A.CLosed,0)Closed,isnull(A.Close_Date,'1/1/1900')Close_Date," +
                            " isnull(A.Solved,0)Solved,isnull(A.Solve_Date,'1/1/1900')Solve_Date," +
                            " A.Bulan, " +
                            //" '-'NamaBulan,"+
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
                            " end NamaBulan,A.Semester, " +
                            " A.Tahun,A.Actual,case when A.Kesim=1 then 'Tercapai' else 'Tidak Tercapai' end Ket,A.Kesim,A.RowStatus," +
                            " (select COUNT(ID)verified from SPD_Tindakan where Verifikasi=1 and SPDAnalisaID=A.ID)Verifikasi," +
                            " (select COUNT(ID)verified from SPD_Tindakan where Verifikasi=0 and SPDAnalisaID=A.ID)notverified," +
                            " isnull((select top 1 Jadwal_Selesai from SPD_Tindakan where SPDAnalisaID =A.ID and RowStatus>-1 order by Jadwal_Selesai desc),'1/1/1900') due_date,isnull(apvmgr,'1/1/1900')apvmgr" +
                            " from SPD_Analisa A  left join SPD_Perusahaan B on A.SarmutPID=B.ID left join SPD_Dept C on A.Deptid=C.ID " +
                            " left join SPD_Departemen D on A.SarmutDeptID=D.ID inner join SPD_Type E on A.TypeSarmutID=E.ID " +
                            " where A.RowStatus>-1 " + kriteria + " order by A.TglAnalisa Desc,C.Dept ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObjectAll(sqlDataReader));
                }
            }
            else
                arrData.Add(new SPD_PrsAx());
            return arrData;
        }

        public SPD_PrsAx RetrieveByNo(string No)
        {
            string strSQL = " select A.DeptID,A.CreatedTime,A.ID ID,isnull(C.Dept,'')Dept,A.AnNo,A.TglAnalisa,A.SarmutPID,B.SarMutPerusahaan,A.SarmutDeptID, " +
                            " isnull(D.SarmutDepartemen,'')SarmutDepartemen,A.TargetVID,A.ParamID,A.SatuanID,A.TypeSarmutID,E.JenisSarmut Jenis,A.Apv," +
                            " case when Apv=0 then 'Admin' when apv is null then 'Admin' " +
                            " when apv=1 then 'Manager Dept' when apv=2 then 'ISO' end Approval,isnull(A.Closed,0)Closed,isnull(A.Close_Date,'1/1/1900')Close_Date," +
                            " isnull(A.Solved,0)Solved,isnull(A.Solve_Date,'1/1/1900')Solve_Date,A.Bulan," +
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
                            " end NamaBulan ,A.Semester, " +
                            " A.Tahun,A.Actual,case when A.Kesim=1 then 'Tercapai' else 'Tidak Tercapai' end Ket,A.Kesim,A.RowStatus," +
                            " (select COUNT(ID)verified from SPD_Tindakan where Verifikasi=1 and SPDAnalisaID=A.ID)Verifikasi," +
                            " (select COUNT(ID)verified from SPD_Tindakan where Verifikasi=0 and SPDAnalisaID=A.ID)notverified," +
                            " isnull((select top 1 Jadwal_Selesai from SPD_Tindakan where SPDAnalisaID =A.ID and RowStatus>-1 order by Jadwal_Selesai desc),'1/1/1900') due_date,isnull(apvmgr,'1/1/1900')apvmgr " +
                            " from SPD_Analisa A  left join SPD_Perusahaan B on A.SarmutPID=B.ID left join SPD_Dept C on A.Deptid=C.ID " +
                            " left join SPD_Departemen D on A.SarmutDeptID=D.ID inner join SPD_Type E on A.TypeSarmutID=E.ID " +
                            " where A.AnNo = '" + No + "'  order by A.TglAnalisa,C.Dept ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectAll(sqlDataReader);
                }
            }
            return new SPD_PrsAx();
        }

        public ArrayList RetrieveByNo1(string No, string jenis)
        {
            string strSQL = "SELECT A.ID IDPer,A.SPDAnalisaID, A.Tindakan, A.Pelaku, A.Jadwal_Selesai, isnull(A.Aktual_Selesai,'17530101')Aktual_Selesai, A.Verifikasi,isnull(A.tglVerifikasi,getdate())tglVerifikasi " +
                ", A.Targetx FROM SPD_Tindakan A inner join SPD_Analisa C on A.SPDAnalisaID=C.ID " +
                "where A.rowstatus>-1 and C.AnNo='" + No + "' and A.jenis='" + jenis + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObjectPP(sqlDataReader));
                }
            }
            else
                arrData.Add(new SPD_PrsAx());
            return arrData;
        }

        public string GetApv(string UserID)
        {
            string strsql = "select Approval as Apv from SPD_Users where User_ID=" + UserID + " and rowstatus>-1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return sqlDataReader["Apv"].ToString();
                }
            }

            return string.Empty;
        }

        public string GetUserType(string userID)
        {
            string strSQL = "select keterangan from SPD_users where user_id=" + userID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            string usertype = string.Empty;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    usertype = sqlDataReader["keterangan"].ToString();
                }
            }
            return usertype;
        }

        public ArrayList RetrieveLampiranByNo(string laporan)
        {

            string strSQL = "select ID,A_SarmutTransID ,FileName,format(CreatedTime,'dd-MM-yyyy') CreatedTime from SPD_Attachment_AP where A_SarmutTransID in (select ID from SPD_Analisa where AnNo='" + laporan + "') and rowstatus > -1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObjectLampiran(sqlDataReader));
                }
            }
            else
                arrData.Add(new SPD_PrsAx());

            return arrData;
        }
        public ArrayList RetrieveParetoByNo(string laporan)
        {

            string strSQL = "select ID,A_SarmutTransID ,FileName,format(CreatedTime,'dd-MM-yyyy') CreatedTime from SPD_Attachment_Pareto where A_SarmutTransID in (select ID from SPD_Analisa where AnNo='" + laporan + "') and rowstatus > -1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObjectLampiran(sqlDataReader));
                }
            }
            else
                arrData.Add(new SPD_PrsAx());

            return arrData;
        }

        public string UpdateAktualSelesai(string ID, string tgl0, string tgl)
        {
            string strSQL = "update SPD_Tindakan set jadwal_Selesai='" + tgl0 + "',Aktual_Selesai='" + tgl + "' where ID=" + ID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }

        public string UpdateTindakan(string ID, string tgl, string verf)
        {
            string strSQL = "update SPD_tindakan set verifikasi=" + verf + ",tglverifikasi='" + tgl + "' where ID=" + ID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }

        public string DeleteTindakan(string ID)
        {
            string strSQL = "update SPD_tindakan set RowStatus = -1 where ID=" + ID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }

        public string hapus(string idAnID)
        {
            string strsql = "update SPD_AttachmentAResiko set RowStatus = -1 where ID=" + idAnID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;


            return string.Empty;
        }

        public string UpdateSolveAna(string anno, string tglsolve, string tgldue, string userby, int value)
        {
            string strSQL = "update SPD_Analisa set lastmodifiedtime=getdate(),lastmodifiedby='" + userby + "', Solved=" + value +
                ",Solve_Date='" + tglsolve + "',Due_Date='" + tgldue + "' where AnNo='" + anno + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }

        public string UpdateCloseAna(string anno, string tgl, string userby, int value)
        {
            string strSQL = "update SPD_Analisa set lastmodifiedtime=getdate(),Closeby='" + userby + "',lastmodifiedby='" + userby + "', closed=" + value + ",CLose_Date='" + tgl +
                "' where AnNo='" + anno + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }

        public SPD_PrsAx RetrieveById1(int tahun, int Bulan, int smtp)
        {

            string StrSql = " select A.SarmutPID ID,A.Tahun,A.Bulan,B.SarMutPerusahaan,A.Actual,C.Param,D.Satuan,E.TargetV1[Target],B.DeptID,B.TypeSarmutID from SPD_TransPrs A " +
                            " inner join SPD_Perusahaan B on A.SarmutPID=B.ID " +
                            " inner join SPD_Parameter C on B.ParamID=C.ID " +
                            " inner join SPD_Satuan D on D.ID=B.SatuanID " +
                            " inner join SPD_TargetV E on E.ID=B.TargetVID " +
                            " where Tahun=" + tahun + " and Bulan=" + Bulan + "  " +
                            " and SarmutPID=" + smtp + "  and B.RowStatus>-1 order by A.Bulan,B.TypeSarmutID ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new SPD_PrsAx();
        }

        public SPD_PrsAx RetrieveById1xx1(string Smt, int tahun, int smtp)
        {
            string kriteria = string.Empty;
            if (Smt == "Semester I")
            {
                kriteria = kriteria + " A.Bulan in (1,2,3,4,5,6) ";
            }
            else
            {
                kriteria = kriteria + " A.Bulan in (7,8,9,10,11,12) ";
            }

            string StrSql = " select A.SarmutPID ID,A.Tahun,A.Bulan,B.SarMutPerusahaan,A.Actual,C.Param,D.Satuan,E.TargetV1[Target],B.DeptID,B.TypeSarmutID from SPD_TransPrs A " +
                            " inner join SPD_Perusahaan B on A.SarmutPID=B.ID " +
                            " inner join SPD_Parameter C on B.ParamID=C.ID " +
                            " inner join SPD_Satuan D on D.ID=B.SatuanID " +
                            " inner join SPD_TargetV E on E.ID=B.TargetVID " +
                            " where Tahun=" + tahun + " and " + kriteria + "  " +
                            " and SarmutPID=" + smtp + "  and B.RowStatus>-1 order by A.Bulan,B.TypeSarmutID ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new SPD_PrsAx();
        }

        public int GetLastUrutan(int tahun)
        {
            int urutan = 0;
            string strSQL = "select top 1 urutan from (select CAST(SUBSTRING(AnNo,1,3) as int) urutan from SPD_Analisa where YEAR(TglAnalisa)=" + tahun + ")A order by urutan desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    urutan = Convert.ToInt32(sqlDataReader["urutan"]);
                }
            }
            return urutan;
        }

        public int getTarget(string anno, string jenis, string tindakan)
        {
            int intresult = 0;
            string strSQL = "select count(tindakan)targetke from SPD_Tindakan where rowstatus>-1 and Tindakan='" + tindakan +
                "' and jenis='" + jenis + "' and SPDAnalisaID in (select ID from SPD_Analisa where AnNo='" + anno + "')";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    intresult = Convert.ToInt32(sqlDataReader["targetke"]);
                }
            }
            return intresult;
        }

        public SPD_PrsAx GetUserDept(int userid)
        {
            string strSQL = "select A.*  from SPD_Dept  A inner join SPD_Users B on A.ID=B.Dept_ID  where A.rowstatus>-1 and  B.rowstatus>-1 and B.User_ID=" + userid;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectDept(sqlDataReader);
                }
            }
            return new SPD_PrsAx();
        }

        public ArrayList RetrieveByNo2(string No)
        {
            string strSQL = " SELECT A.Penyebab_ID, A.SPDAnalisaID, A.Uraian,B.Penyebab " +
                            " FROM SPD_Analisa_Penyebab_Detail A inner join SPD_Analisa_Penyebab B on A.Penyebab_ID=B.ID inner join SPD_Analisa C on A.SPDAnalisaID=C.ID " +
                            " where A.rowstatus>-1 and C.AnNo='" + No + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObjectPenyebab(sqlDataReader));
                }
            }
            else
                arrData.Add(new FacadePrsaX());
            return arrData;
        }

        //WO PM JOMBANG bisa liat all dept
        public SPD_Dept GetUserSPD(int userid)
        {
            string strSQL = "select A.*  from spd_Dept  A inner join spd_Users B on A.ID=B.Dept_ID  where A.rowstatus>-1 and  B.rowstatus>-1 AND B.User_ID=" + userid;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectSPD(sqlDataReader);
                }
            }
            return new SPD_Dept();
        }

        private SPD_PrsAx GenerateObjectxx(SqlDataReader sdr)
        {
            objSPD = new SPD_PrsAx();
            objSPD.ID = Convert.ToInt32(sdr["ID"]);
            //objSPD.Kode = sdr["Kode"].ToString();
            objSPD.Dept = sdr["Dept"].ToString();
            return objSPD;
        }

        public SPD_PrsAx GenerateObjectLampiran(SqlDataReader sqlDataReader)
        {
            objSPD = new SPD_PrsAx();
            objSPD.FileName = sqlDataReader["FileName"].ToString();
            objSPD.TanggalUpload = sqlDataReader["CreatedTime"].ToString();
            objSPD.A_SarmutTransID = Convert.ToInt32(sqlDataReader["A_SarmutTransID"]);
            objSPD.ID = Convert.ToInt32(sqlDataReader["ID"]);
            return objSPD;
        }

        private SPD_PrsAx GenerateObjectAll(SqlDataReader sdr)
        {
            SPD_PrsAx xxx = new SPD_PrsAx();
            xxx.ID = Convert.ToInt32(sdr["ID"]);
            xxx.DeptID = Convert.ToInt32(sdr["DeptID"]);
            xxx.Dept = sdr["Dept"].ToString();
            xxx.AnNo = sdr["AnNo"].ToString();
            xxx.TglAnalisa = Convert.ToDateTime(sdr["TglAnalisa"]);
            xxx.SarMutPerusahaan = sdr["SarMutPerusahaan"].ToString();
            xxx.SarmutDepartemen = sdr["SarmutDepartemen"].ToString();
            xxx.TargetVID = sdr["TargetVID"].ToString();
            xxx.Actual = Convert.ToDecimal(sdr["Actual"]);
            xxx.SatuanID = sdr["SatuanID"].ToString();
            xxx.Jenis = sdr["Jenis"].ToString();
            xxx.Ket = sdr["Ket"].ToString();
            xxx.Kesim = Convert.ToInt32(sdr["Kesim"]);
            xxx.SarmutDeptID = Convert.ToInt32(sdr["SarmutDeptID"]);
            xxx.Tahun = Convert.ToInt32(sdr["Tahun"]);
            xxx.Bulan = Convert.ToInt32(sdr["Bulan"]);
            xxx.NamaBulan = sdr["NamaBulan"].ToString();
            xxx.SarmutPID = Convert.ToInt32(sdr["SarmutPID"]);
            xxx.Apv = Convert.ToInt32(sdr["Apv"]);
            xxx.Approval = sdr["Approval"].ToString();
            xxx.Solved = Convert.ToInt32(sdr["Solved"]);
            xxx.Solve_Date = Convert.ToDateTime(sdr["Solve_Date"]);
            xxx.CLosed = Convert.ToInt32(sdr["CLosed"]);
            xxx.Close_Date = Convert.ToDateTime(sdr["Close_Date"]);
            xxx.Verifikasi = Convert.ToInt32(sdr["Verifikasi"]);
            xxx.Due_Date = Convert.ToDateTime(sdr["Due_Date"]);
            xxx.Semester = sdr["Semester"].ToString();
            xxx.ApvMgr = DateTime.Parse(sdr["ApvMgr"].ToString());
            return xxx;
        }


        private SPD_PrsAx GenerateObjectDept(SqlDataReader sdr)
        {
            SPD_PrsAx xx1 = new SPD_PrsAx();
            xx1.dptID = Convert.ToInt32(sdr["DptID"]);
            xx1.Dept = sdr["Dept"].ToString();
            return xx1;
        }

        private SPD_PrsAx GenerateObject(SqlDataReader sdr)
        {
            SPD_PrsAx xx1 = new SPD_PrsAx();
            xx1.IDX = Convert.ToInt32(sdr["IDx"]);
            xx1.Target = Convert.ToDecimal(sdr["Target"]);
            xx1.Actual = Convert.ToDecimal(sdr["Actual"]);
            xx1.Param = sdr["Param"].ToString();
            xx1.Satuan = sdr["Satuan"].ToString();
            return xx1;
        }

        private SPD_PrsAx GenerateObject2(SqlDataReader sdr)
        {
            SPD_PrsAx xx1 = new SPD_PrsAx();
            xx1.ID = Convert.ToInt32(sdr["ID"]);
            xx1.Target = Convert.ToDecimal(sdr["Target"]);
            xx1.Actual = Convert.ToDecimal(sdr["Actual"]);
            xx1.Param = sdr["Param"].ToString();
            xx1.Satuan = sdr["Satuan"].ToString();
            xx1.TypeSarmutID = Convert.ToInt32(sdr["TypeSarmutID"]);
            return xx1;
        }

        private SPD_PrsAx GenerateObjectPenyebab(SqlDataReader sdr)
        {
            SPD_PrsAx xx1 = new SPD_PrsAx();
            xx1.Penyebab_ID = Convert.ToInt32(sdr["Penyebab_ID"]);
            xx1.SPDAnalisaID = Convert.ToInt32(sdr["SPDAnalisaID"]);
            xx1.Uraian = sdr["Uraian"].ToString();
            xx1.Penyebab = sdr["Penyebab"].ToString();
            return xx1;
        }

        private SPD_PrsAx GenerateObjectPP(SqlDataReader sdr)
        {
            SPD_PrsAx xx1 = new SPD_PrsAx();
            xx1.IDPer = Convert.ToInt32(sdr["IDPer"]);
            xx1.SPDAnalisaID = Convert.ToInt32(sdr["SPDAnalisaID"]);
            xx1.Tindakan = sdr["Tindakan"].ToString();
            xx1.Pelaku = sdr["Pelaku"].ToString();
            xx1.Jadwal_Selesai = Convert.ToDateTime(sdr["Jadwal_Selesai"]);
            xx1.Aktual_Selesai = Convert.ToDateTime(sdr["Aktual_Selesai"]);
            xx1.Tglverifikasi = Convert.ToDateTime(sdr["Tglverifikasi"]);
            xx1.Verifikasi = Convert.ToInt32(sdr["Verifikasi"]);
            xx1.Targetx = sdr["Targetx"].ToString();
            return xx1;
        }

        //WO PM JOMBANG bisa liat all dept
        private SPD_Dept GenerateObjectSPD(SqlDataReader sdr)
        {
            SPD_Dept xx1 = new SPD_Dept();
            xx1.ID = Convert.ToInt32(sdr["ID"]);
            xx1.Dept = sdr["Dept"].ToString();
            return xx1;
        }
    }

    public class SPD_PrsAx : GRCBaseDomain
    {
        public string Description { get; set; }
        //public string Satuan { get; set; }
        public string Param { get; set; }
        public decimal Target { get; set; }
        public decimal Actual { get; set; }
        public int IDX { get; set; }
        public int Tahun { get; set; }
        public int Bulan { get; set; }
        public int DeptID { get; set; }
        public int dptID { get; set; }
        public int Urutan { get; set; }
        public int TypeSarmutID { get; set; }
        public int SDeptID { get; set; }
        public int Checked { get; set; }
        public string StatusApv { get; set; }
        public int OnSystem { get; set; }
        public int SarmutPID { get; set; }
        public int SarmutDeptID { get; set; }
        public string TargetVID { get; set; }
        public string ParamID { get; set; }
        public string SatuanID { get; set; }
        public int Kesim { get; set; }
        public string SarmutDepartemen { get; set; }
        public string SarMutPerusahaan { get; set; }
        public string Old_sarmutNo { get; set; }
        public string AnNo { get; set; }
        public DateTime TglAnalisa { get; set; }
        public int Penyebab_ID { get; set; }
        public string Penyebab { get; set; }
        public int SPDAnalisaID { get; set; }
        public string Uraian { get; set; }
        //public int RowStatus { get; set; }
        public string Tindakan { get; set; }
        public string Pelaku { get; set; }
        public string Jenis { get; set; }
        public string Targetx { get; set; }
        public DateTime Jadwal_Selesai { get; set; }
        public DateTime Aktual_Selesai { get; set; }
        public DateTime Tglverifikasi { get; set; }
        public int Verifikasi { get; set; }
        public string Dept { get; set; }
        public string Ket { get; set; }
        public int ID1 { get; set; }
        public int IDPer { get; set; }
        public int User_ID { get; set; }
        public int Apv { get; set; }
        public string Approval { get; set; }
        public int CLosed { get; set; }
        public DateTime Close_Date { get; set; }
        public string CloseBy { get; set; }
        public int Solved { get; set; }
        public DateTime Solve_Date { get; set; }
        public DateTime Due_Date { get; set; }
        public string FileName { get; set; }
        public int A_SarmutTransID { get; set; }

        public string NamaBulan { get; set; }
        public string Semester { get; set; }
        public string NmSemester { get; set; }

        public string TanggalUpload { get; set; }
        public DateTime ApvMgr { get; set; }
    }

    //WO PM JOMBANG bisa liat all dept
    public class SPD_Dept
    {
        private int id = 0;
        private string dept = string.Empty;

        public int ID { get { return id; } set { id = value; } }
        public string Dept { get { return dept; } set { dept = value; } }
    }
}