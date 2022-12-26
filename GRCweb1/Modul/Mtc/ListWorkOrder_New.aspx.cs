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
using System.Web.Script.Serialization;
using AjaxControlToolkit;

namespace GRCweb1.Modul.Mtc
{
    public partial class ListWorkOrder_New : System.Web.UI.Page
    {
        private byte[] bytes;

        protected void Page_Load(object sender, EventArgs e)
        {
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);

            if (!Page.IsPostBack)
            {
                Session["share"] = "-"; Session["UnApprove"] = "-";

                appLevele.Value = (Request.QueryString["p"] != null) ? Request.QueryString["P"].ToString() : "";

                Global.link = "~/Default.aspx";
                Users user = ((Users)Session["Users"]);

                WorkOrder_New wor = new WorkOrder_New();
                WorkOrderFacade_New worf = new WorkOrderFacade_New();
                int StatusApv = worf.RetrieveUserLevel1(user.ID);
                Session["StatusApv"] = StatusApv;

                if (StatusApv > 0)
                {

                    if (StatusApv == 1 || StatusApv == 2 || StatusApv == 4)
                    {
                        btnApprove.Enabled = true; btnUnApprove.Enabled = false;
                    }
                }
                else { btnApprove.Enabled = false; btnUnApprove.Enabled = false; }

                LoadDept();
                LoadBulan();
                LoadTahun();
                LoadListWO();

            }
            int Stt = Convert.ToInt32(Session["StatusApv"]);
            if (Stt == 1)
            {
                btnUnApprove.Attributes.Add("onclick", "return confirm_batal();");
            }
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
        private void LoadTahun()
        {
            ArrayList arrD = this.ListWOTahun();
            ddlTahun.Items.Clear();

            foreach (WorkOrder_New wo in arrD)
            {
                ddlTahun.Items.Add(new ListItem(wo.Tahun.ToString(), wo.Tahun.ToString()));
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("InputWorkOrder_New.aspx");
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            this.StateView = 2;
            criteria.Visible = true;
            LoadListWO();


        }
        protected void btnExport_Click(object sender, EventArgs e)
        {

            foreach (RepeaterItem rpt in lstBA.Items)
            {
                ((Image)rpt.FindControl("attach")).Visible = false;
                ((Image)rpt.FindControl("view")).Visible = false;
                ((Image)rpt.FindControl("att")).Visible = false;
                ((Image)rpt.FindControl("info")).Visible = false;
                ((Image)rpt.FindControl("detail")).Visible = false;
                ((Image)rpt.FindControl("adjust")).Visible = false;
                Repeater att = (Repeater)rpt.FindControl("attachm");
                foreach (RepeaterItem rp in att.Items)
                {
                    ((Image)rp.FindControl("lihat")).Visible = false;
                    ((Image)rp.FindControl("hapus")).Visible = false;
                }


            }

            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=ListBeritaAcara.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<b>REKAP PES</b>";
            Html += "<br>Periode : " + ddlBulan.SelectedItem.Text + "  " + ddlTahun.SelectedValue.ToString();
            //Html += "<br>Departement :" + ddlDept.SelectedItem.Text;
            string HtmlEnd = "";
            //lstForPrint.RenderControl(hw);
            lst.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0", "border=\"1");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }
        private int StateView { get; set; }

        protected void RBInt_CheckedChanged(object sender, EventArgs e)
        {
            LoadListWO();
        }

        protected void RB10_CheckedChanged(object sender, EventArgs e)
        {
            Response.Redirect("InputWorkOrder_New.aspx");
        }

        //protected void RB20_CheckedChanged(object sender, EventArgs e)
        //{
        //    Response.Redirect("ListWorkOrder_New.aspx");
        //}

        protected void RBback_CheckedChanged(object sender, EventArgs e)
        {
            Session["share"] = "-";
            LoadListWO();
        }

        private void LoadListWO()
        {
            Users users = (Users)Session["Users"];
            string ShareWO = string.Empty;

            #region Share WO
            if (RBInt.Checked == true)
            {
                Session["share"] = "1";
                ShareWO = Session["share"].ToString();
            }
            else
            {
                ShareWO = Session["share"].ToString();
            }

            WorkOrder_New WO50 = new WorkOrder_New();
            WorkOrderFacade_New WOF50 = new WorkOrderFacade_New();
            int Share = WOF50.cekShareWO(users.DeptID, users.UnitKerjaID);
            if (Share > 0)
            {
                PaneLShare.Visible = true;
                LabelShare.Visible = true; LabelShare.Text = "Ada " + " " + Share + " " + "  Share WO yang harus di lihat";
            }
            else
            {
                PaneLShare.Visible = false; LabelShare.Visible = false;
            }
            #endregion

            #region WO habis apv plant terkait - masuk ke pak sodik
            WorkOrder_New WO500 = new WorkOrder_New();
            WorkOrderFacade_New WOF500 = new WorkOrderFacade_New();

            if (users.DeptID == 14 || users.DeptID == 19 || users.DeptID == 4 || users.DeptID == 25 || users.DeptID == 18 || users.DeptID == 7)
            {
                int ttl = WOF500.cekWO(users.DeptID, users.UnitKerjaID);
                if (ttl > 0)
                {
                    PanelUpdateWO.Visible = true;
                    LabelUpdate1.Visible = true;
                    if (users.Apv > 0 && users.DeptID == 14)
                    {
                        LabelUpdate1.Text = "Ada " + " " + ttl + " " + "  WO yang harus di update target selesai ";
                    }

                    if (users.Apv > 0 && users.DeptID == 7)
                    {
                        LabelUpdate1.Text = "Ada " + " " + ttl + " " + "  WO yang sdh di update target selesai ";
                    }

                    if (users.Apv > 0 && users.DeptID == 19 || users.Apv > 0 && users.DeptID == 4 || users.Apv > 0 && users.DeptID == 18 || users.Apv > 0 && users.DeptID == 5)
                    {
                        LabelUpdate1.Text = "Ada " + " " + ttl + " " + "  WO yang harus di tentukan PIC pelaksana ";
                    }
                }
                else
                {
                    PanelUpdateWO.Visible = false; LabelUpdate1.Visible = false;
                }
            }
            #endregion


            int StatusApv = Convert.ToInt32(Session["StatusApv"]);

            if (StatusApv > 0)
            {
                btnBack.Visible = false; btnPreview.Visible = false; btnExport.Visible = false;
                ddlTahun.Visible = false; ddlBulan.Visible = false; LabelPeriode.Visible = false; btnApprove.Visible = true;
            }


            WorkOrder_New WO5 = new WorkOrder_New();
            WorkOrderFacade_New WOF5 = new WorkOrderFacade_New();

            if (users.DeptID == 4 || users.DeptID == 18 || users.DeptID == 5 || users.DeptID == 25)
            {
                int DeptID5 = 19;
                Session["DeptID5"] = DeptID5;
            }
            else
            {
                int DeptID5 = users.DeptID;
                Session["DeptID5"] = DeptID5;
            }

            int DeptID6 = Convert.ToInt32(Session["DeptID5"]);
            int DeptID = WOF5.RetrieveDeptIDHead(users.DeptID);


            if (StatusApv == 3)
            {
                int DeptID1 = DeptID6;
                Session["DeptID1"] = DeptID1;
            }
            else
            {
                int DeptID1 = users.DeptID;
                Session["DeptID1"] = DeptID1;
            }

            int DeptID2 = Convert.ToInt32(Session["DeptID1"]);

            WorkOrder_New WO = new WorkOrder_New();
            WorkOrderFacade_New WOF = new WorkOrderFacade_New();
            ArrayList arrWO = new ArrayList();
            arrWO = WOF.RetrieveListWO_1(DeptID2, StatusApv, users.UnitKerjaID, users.ID, ShareWO);
            lstBA.DataSource = arrWO;
            lstBA.DataBind();

            if (arrWO.Count > 0)
            {
                PanelRptr.Visible = true;
            }
            else
            {
                PanelRptr.Visible = false;
            }

        }

        private void LoadDept()
        {
            Users users = (Users)Session["Users"];
            string DeptID = users.DeptID.ToString();
            WorkOrder_New WO = new WorkOrder_New();
            WorkOrderFacade_New WOF = new WorkOrderFacade_New();
            ArrayList arrWO = new ArrayList();

            arrWO = WOF.RetrieveHeaderDept(DeptID);

            //lstDeptH.DataSource = arrWO;
            //lstDeptH.DataBind();
        }

        protected void lstDeptH_DataBound(object sender, RepeaterItemEventArgs e)
        { }

        protected void lstBA_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Users users = (Users)Session["Users"];
            int StatusApv = Convert.ToInt32(Session["StatusApv"]);
            WorkOrder_New wo = (WorkOrder_New)e.Item.DataItem;
            CheckBox chk = (CheckBox)e.Item.FindControl("chk");
            Image attach = (Image)e.Item.FindControl("attach");
            Image view = (Image)e.Item.FindControl("view");
            Image att = (Image)e.Item.FindControl("att");
            Image hapus = (Image)e.Item.FindControl("hapus");
            Label lbl = (Label)e.Item.FindControl("LabelDeptPemohon");



            att.Attributes.Add("onclick", "OpenDialog('" + wo.WOID.ToString() + "&tp=1')");

            Session["Status"] = wo.StatusApv;

            if (StatusApv == 1 && wo.StatusApv == "Open"
                || StatusApv == 1 && wo.StatusApv == "PM Ctrp"
                || StatusApv == 1 && wo.StatusApv == "PM Krwg"
                || StatusApv == 1 && wo.StatusApv == "PM"
                || StatusApv == 1 && wo.StatusApv == "Verifikasi ISO"
                || users.DeptID == 11
                || StatusApv == 1 && wo.StatusApv == "Share Krwg"
                || StatusApv == 1 && wo.StatusApv == "Share Ctrp")
            {
                chk.Visible = true; att.Visible = false; btnUnApprove.Visible = true; btnUnApprove.Enabled = true;
                btnApprove.Visible = true; btnApprove.Enabled = true;

            }
            else if (StatusApv == 2 && wo.StatusApv == "Mgr Dept."
                       || StatusApv == 2 && wo.StatusApv == "Verified ISO"
                       || StatusApv == 2 && wo.StatusApv == "Tidak Ikut"
                       || StatusApv == 2 && wo.StatusApv == "Ikut")
            {
                chk.Visible = true; att.Visible = false; hapus.Visible = false;
                btnUnApprove.Visible = true; btnApprove.Visible = true; btnUnApprove.Enabled = true;
            }
            else if (StatusApv == 3 || StatusApv == 4)
            {
                chk.Visible = true; att.Visible = false; hapus.Visible = false; btnUnApprove.Visible = false;
            }
            else if (StatusApv == 10 || StatusApv == 11)
            {
                chk.Visible = true; att.Visible = false; hapus.Visible = false; btnUnApprove.Visible = false;
            }
            else if (StatusApv == 9)
            {
                chk.Visible = false; att.Visible = false; btnApprove.Visible = false; btnUnApprove.Visible = false;
                hapus.Visible = false; lbl.Visible = false;
            }
            else
            {
                chk.Visible = false; btnApprove.Visible = false; btnUnApprove.Visible = false;

            }

            Repeater rps = (Repeater)e.Item.FindControl("attachm");
            LoadListAttachment(wo.WOID.ToString(), rps);

            Repeater rps1 = (Repeater)e.Item.FindControl("attachm1");
            LoadListPlant(wo.WOID.ToString(), rps1);
        }
        protected void attachm1_DataBound(object sender, RepeaterItemEventArgs e)
        { }

        protected void attachm_DataBound(object sender, RepeaterItemEventArgs e)
        {
            int StatusApv = Convert.ToInt32(Session["StatusApv"]);
            Users users = (Users)Session["Users"];


            if (e.Item.ItemType == ListItemType.Header)
            {
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                Image pre = (Image)e.Item.FindControl("lihat");
                Image hps = (Image)e.Item.FindControl("hapus");
                WorkOrder_New att = (WorkOrder_New)e.Item.DataItem;
                //pre.Attributes.Add("onclick", "PreviewPDF('" + pre.CssClass.ToString() + "')");
                pre.Attributes.Add("onclick", "PreviewPDF('" + att.ID + "')");

                WorkOrder_New wo = new WorkOrder_New();
                WorkOrderFacade_New wof = new WorkOrderFacade_New();
                wo.ID = Convert.ToInt32(hps.CssClass);
                int StatApvWO = wof.Retrieve_apv_wo_atch(wo.ID);

                if (StatusApv > 0)
                { hps.Visible = false; }
                if (StatApvWO > 0)
                { hps.Visible = false; }

            }
        }
        protected void attachm_Command(object sender, RepeaterCommandEventArgs e)
        {
            Repeater rpt = (Repeater)sender;
            Image pre = (Image)rpt.Items[int.Parse(e.CommandArgument.ToString())].FindControl("hapus");
            switch (e.CommandName)
            {
                case "pre":
                    break;

                case "hps":

                    WorkOrder_New wo = new WorkOrder_New();
                    WorkOrderFacade_New wof = new WorkOrderFacade_New();
                    wo.ID = Convert.ToInt32(pre.CssClass);
                    string IDLampiran = wo.ID.ToString();
                    string woid = wof.RetrieveWOID_int(IDLampiran);
                    int intResult = 0;
                    intResult = wof.CancelWO_Lampiran(wo);
                    LoadListAttachment(woid, rpt);
                    break;
            }
        }

        protected void lstBA_Command(object sender, RepeaterCommandEventArgs e)
        {
            Repeater rpt = (Repeater)sender;
            Image pre = (Image)rpt.Items[int.Parse(e.CommandArgument.ToString())].FindControl("hapus");

            switch (e.CommandName)
            {
                case "hps":

                    WorkOrder_New wo = new WorkOrder_New();
                    WorkOrderFacade_New wof = new WorkOrderFacade_New();
                    int ID = Convert.ToInt32(pre.CssClass);
                    wo.ID = ID;
                    int Apv = wof.Retrieve_apv_wo(ID);
                    if (Apv == 0)
                    {
                        int intResult = 0;
                        wo.ID = Convert.ToInt32(pre.CssClass);

                        intResult = wof.Cancel_WO(wo);
                        if (intResult > -1)
                        { Response.Redirect("ListWorkOrder_New.aspx"); }
                    }
                    else
                    {
                        DisplayAJAXMessage(this, "WO sudah di approve");
                        return;
                    }

                    break;


                case "upload":
                    if (CheckAttach(int.Parse(e.CommandArgument.ToString())) == true) { this.LoadListWO(); }
                    break;

                case "attach":
                    Repeater rpts = (Repeater)lstBA.Items[int.Parse(e.CommandArgument.ToString())].FindControl("attachm");
                    ImageButton imgs = (ImageButton)lstBA.Items[int.Parse(e.CommandArgument.ToString())].FindControl("att");
                    //rpts.Visible = (rpts.Visible == true) ? false : true;
                    LoadListAttachment(imgs.CssClass.ToString(), rpts);
                    break;

            }
        }

        protected void chk_CheckedChange(object sender, EventArgs e)
        {

            WorkOrder_New wo = new WorkOrder_New();
            CheckBox chk = (CheckBox)sender;
            //btnApprove.Enabled = (chk.Checked == true && wo1.RowStatus > 0) ? true : false;
            //btnUnApprove.Enabled = (chk.Checked == true) ? true : false;
            if (chk.Checked == true)
            {
                btnApprove.Enabled = true; btnUnApprove.Enabled = true;

            }
            else if (chk.Checked == false)
            {
                btnApprove.Enabled = false; btnUnApprove.Enabled = false;
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstBA.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)lstBA.Items[i].FindControl("chk");

                if (chk.Checked == true)
                {
                    int intResult = 0;
                    int intResult2 = 0;
                    int StatusApv = Convert.ToInt32(Session["StatusApv"]);
                    Users users = (Users)Session["Users"];
                    WorkOrder_New wo = new WorkOrder_New();
                    WorkOrderFacade_New woFacade = new WorkOrderFacade_New();
                    wo.WOID = int.Parse(chk.CssClass.ToString());
                    wo = woFacade.RetrieveDataWO(int.Parse(chk.CssClass.ToString()));

                    WorkOrder_New wo5 = new WorkOrder_New();
                    WorkOrderFacade_New woFacade5 = new WorkOrderFacade_New();
                    int DeptID = woFacade5.RetrieveDeptIDHead(users.ID);

                    if (wo.ToDept == 19 || wo.ToDept == 7)
                    {
                        if (users.UnitKerjaID == 11) // Tidak DiPakai ....
                        {
                            if (StatusApv == 10)
                            { wo.Apv = 1; }
                            else if (StatusApv == 3 && DeptID == 19 || StatusApv == 3 && DeptID == 7)
                            { wo.Apv = 1; }
                            else { wo.Apv = StatusApv; }
                        }
                        else if (users.UnitKerjaID == 7 || users.UnitKerjaID == 1 || users.UnitKerjaID == 13)
                        {
                            if (StatusApv == 10 || StatusApv == 4)
                            { wo.Apv = 1; }
                            else if (StatusApv == 3 && DeptID == 19 || StatusApv == 3 && DeptID == 7)
                            { wo.Apv = 2; }
                            else { wo.Apv = 2; }
                        }
                    }
                    else if (wo.ToDept == 14 && wo.AreaWO.Trim() == "HardWare")
                    {
                        wo.Apv = 2;
                    }
                    else
                    {
                        if (StatusApv == 10 && wo.Apv < 2)
                        { wo.Apv = 1; }
                        else if (StatusApv == 10 && wo.Apv == 2)
                        { wo.Apv = 2; }
                        else if (StatusApv == 3 && DeptID == 19 || StatusApv == 3 && DeptID == 14 || StatusApv == 3 && DeptID == 7)
                        { wo.Apv = 1; }
                        else { wo.Apv = StatusApv; }
                    }

                    wo.UserID = users.ID;
                    wo.UserName = users.UserName;
                    wo.StatusApv = StatusApv.ToString();
                    wo.UnitKerjaID = users.UnitKerjaID;

                    Session["NoWO"] = wo.NoWO;
                    Session["UraianPekerjaan"] = wo.UraianPekerjaan;
                    Session["DeptID_Users"] = wo.DeptID_Users;
                    Session["ToDept"] = wo.ToDept;
                    Session["AreaWO"] = wo.AreaWO;
                    Session["Apv"] = wo.Apv;
                    Session["VerifikasiISO"] = wo.VerifikasiISO;
                    //Session["DeptID_PenerimaWO"] = wo.ToDeptName;
                    Session["DeptID_PenerimaWO"] = wo.ToDept;
                    Session["PlantID"] = wo.PlantID;
                    Session["UnitKerjaID"] = wo.UnitKerjaID;
                    Session["VerifikasiSec"] = wo.VerifikasiSec;
                    Session["TanggalBuat"] = wo.CreatedTime;
                    Session["ApvOP"] = wo.ApvOP;

                    intResult = woFacade.UpdateWO_Apv(wo);

                    if (intResult > -1 && wo.AreaWO.Trim() == "Kendaraan" && wo.Apv == 2 && users.UserName.Trim() != "hrd-security"
                        || intResult > -1 && wo.AreaWO.Trim() != "Kendaraan" && wo.Apv != 3)
                    {
                        intResult2 = woFacade.InsertLog_Apv(wo);

                        if (intResult2 > -1)
                        {
                            WorkOrder_New woDo = new WorkOrder_New();
                            WorkOrderFacade_New woFacadeFa = new WorkOrderFacade_New();
                            woDo = woFacadeFa.RetrieveSPM(int.Parse(chk.CssClass.ToString()));

                            Session["PlantID"] = woDo.PlantID;
                            Session["CreatedBy"] = woDo.CreatedBy.ToString();
                            Session["CreatedTime"] = woDo.CreatedTime.ToString();
                            Session["RowStatus"] = woDo.RowStatus;
                            Session["ApvOP"] = woDo.ApvOP;

                            //Belum DiPakai
                            WorkOrder_New woDoL = new WorkOrder_New();
                            WorkOrderFacade_New woFacadeFaL = new WorkOrderFacade_New();
                            woDoL = woFacadeFa.RetrieveLampiran(int.Parse(chk.CssClass.ToString()));

                            Session["FileName"] = woDoL.FileName;

                            if (StatusApv == 1 || StatusApv == 3 || StatusApv == 0 && users.DeptID == 11)
                            {
                                Session["ApvOP"] = 2;
                            }

                            if (StatusApv == 10 && woDo.AreaWO.Trim() == "SoftWare" && woDo.ToDept == 14 && woDo.VerifikasiISO == 1 && woDo.NoWO != "" && woDo.PlantID == users.UnitKerjaID
                                 || StatusApv == 10 && woDo.AreaWO.Trim() == "SoftWare" && woDo.ToDept == 14 && woDo.VerifikasiISO == 1 && woDo.NoWO == "" && woDo.PlantID != users.UnitKerjaID)
                            {
                                InsertWOAntarPlant();
                            }

                            if (

                                woDo.AreaWO.Trim() == "SoftWare" && woDo.ToDept == 14 && StatusApv == 1
                                     && (woDo.ApvOP == 2) && woDo.PlantID != users.UnitKerjaID
                                ||
                                woDo.AreaWO.Trim() == "SoftWare" && woDo.ToDept == 14 && StatusApv == 3
                                     && (woDo.ApvOP == 2) && woDo.PlantID != users.UnitKerjaID

                                || woDo.AreaWO.Trim() == "SoftWare" && woDo.ToDept == 14 && StatusApv == 0 && users.DeptID == 11
                                     && (woDo.ApvOP == 2) && woDo.PlantID != users.UnitKerjaID)
                            {
                                UpdateWOAntarPlant();
                            }

                            KirimEmail2(wo.NoWO);

                            LoadListWO();
                        }
                    }
                    LoadListWO();
                }
            }
        }

        protected void btnUnApprove_ServerClick(object sender, EventArgs e)
        {
            Session["UnApprove"] = "UnApprove";
            for (int i = 0; i < lstBA.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)lstBA.Items[i].FindControl("chk");

                if (chk.Checked == true)
                {
                    int StatusApv = Convert.ToInt32(Session["StatusApv"]);

                    WorkOrder_New wo = new WorkOrder_New();

                    WorkOrderFacade_New woFacade = new WorkOrderFacade_New();

                    wo.WOID = int.Parse(chk.CssClass.ToString());
                    wo = woFacade.RetrieveDataWO(int.Parse(chk.CssClass.ToString()));
                    Session["NoWO"] = wo.NoWO;

                    Users users = (Users)Session["Users"];
                    WorkOrder_New woR = new WorkOrder_New();
                    WorkOrderFacade_New worF = new WorkOrderFacade_New();
                    int intResult = 0;
                    if (wo.PlantID != users.UnitKerjaID && StatusApv == 1)
                    {
                        woR.AlasanNotApvOP = Session["AlasanCancel"].ToString(); woR.AlasanCancel = "";
                    }
                    else if (wo.PlantID != users.UnitKerjaID && StatusApv == 2)
                    {
                        woR.AlasanNotApvOP = ""; woR.AlasanCancel = "";
                    }
                    else if (wo.PlantID == users.UnitKerjaID && StatusApv == 1)
                    {
                        woR.AlasanNotApvOP = ""; woR.AlasanCancel = Session["AlasanCancel"].ToString();
                    }
                    //woR.AlasanNotApvOP = Session["AlasanCancel"].ToString();
                    woR.WOID = (int.Parse(chk.CssClass.ToString()));
                    woR.UserName = users.UserName;
                    woR.StatusApv = StatusApv.ToString();
                    intResult = worF.UpdateNotOP(woR);

                    if (intResult > -1)
                    {

                        WorkOrder_New woDo1 = new WorkOrder_New();
                        WorkOrderFacade_New woFacadeFa1 = new WorkOrderFacade_New();
                        woDo1 = woFacadeFa1.RetrieveSPM(int.Parse(chk.CssClass.ToString()));

                        if (StatusApv == 1)
                        {
                            Session["ApvOP"] = -1;
                        }
                        else if (StatusApv == 2)
                        {
                            Session["ApvOP"] = -2;
                        }

                        if (woDo1.AreaWO.Trim() == "SoftWare" && woDo1.ToDept == 14 && woDo1.VerifikasiISO == 1
                                 && (woDo1.ApvOP == 0 || woDo1.ApvOP == -1))
                        {
                            UpdateWOAntarPlant();
                        }

                        if (wo.Apv > 0)
                        {
                            //KirimEmail2(wo.NoWO);
                        }

                        Response.Redirect("ListWorkOrder_New.aspx");
                    }

                }
            }
        }

        protected void UpdateWOAntarPlant()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("NoWO", typeof(string));
            dt.Columns.Add("ApvOP", typeof(int));

            DataRow row = dt.NewRow();
            row["NoWO"] = Session["NoWO"].ToString().Trim();
            row["ApvOP"] = Session["ApvOP"].ToString().Trim();

            dt.Rows.Add(row);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            if (((Users)Session["Users"]).UnitKerjaID == 1)
            {
                try
                {
                    WebReference_Krwg.Service1 bpas = new WebReference_Krwg.Service1();
                    bpas.UpdateWOAntarPlant(dt);
                }
                catch
                {
                    DisplayAJAXMessage(this, "Koneksi Database ke Karawang error");
                }

            }
            else
            {
                try
                {
                    WebReference_Ctrp.Service1 bpas = new WebReference_Ctrp.Service1();
                    bpas.UpdateWOAntarPlant(dt);

                }
                catch
                {
                    DisplayAJAXMessage(this, "Koneksi Database ke Citeureup error");
                }
            }

        }

        protected void InsertWOAntarPlant()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("NoWO", typeof(string));
            dt.Columns.Add("UraianPekerjaan", typeof(string));
            dt.Columns.Add("DeptID_Users", typeof(int));
            dt.Columns.Add("DeptID_PenerimaWO", typeof(int));
            dt.Columns.Add("AreaWO", typeof(string));
            dt.Columns.Add("CreatedBy", typeof(string));
            dt.Columns.Add("CreatedTime", typeof(DateTime));
            dt.Columns.Add("PlantID", typeof(int));
            dt.Columns.Add("RowStatus", typeof(int));

            dt.Columns.Add("FileName", typeof(string));

            DataRow row = dt.NewRow();
            row["NoWO"] = Session["NoWO"].ToString().Trim();
            row["UraianPekerjaan"] = Session["UraianPekerjaan"].ToString().Trim();
            row["DeptID_Users"] = Convert.ToInt32(Session["DeptID_Users"]);
            row["DeptID_PenerimaWO"] = Convert.ToInt32(Session["DeptID_PenerimaWO"]);
            row["AreaWO"] = Session["AreaWO"].ToString().Trim();
            row["CreatedBy"] = Session["CreatedBy"].ToString().Trim();
            row["CreatedTime"] = Convert.ToDateTime(Session["CreatedTime"]);
            row["PlantID"] = Convert.ToInt32(Session["PlantID"]);
            row["RowStatus"] = Convert.ToInt32(Session["RowStatus"]);

            row["FileName"] = Session["FileName"].ToString().Trim();

            dt.Rows.Add(row);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            if (((Users)Session["Users"]).UnitKerjaID == 1)
            {
                try
                {
                    WebReference_Krwg.Service1 bpas2 = new WebReference_Krwg.Service1();
                    bpas2.InsertWOAntarPlant(dt);
                }
                catch
                {
                    DisplayAJAXMessage(this, "Koneksi Database ke Karawang error");
                }

            }
            else
            {
                try
                {
                    WebReference_Ctrp.Service1 bpas2 = new WebReference_Ctrp.Service1();
                    bpas2.InsertWOAntarPlant(dt);
                }
                catch
                {
                    DisplayAJAXMessage(this, "Koneksi Database ke Citeureup error");
                }
            }
        }

        protected void InsertLampiranAntarPlant()
        {
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("NoWO", typeof(string));
            dt2.Columns.Add("FileLampiranOP", typeof(System.Byte[]));
            dt2.Columns.Add("FileName", typeof(string));

            DataRow row = dt2.NewRow();
            row["NoWO"] = Session["NoWO"].ToString().Trim();
            row["FileLampiranOP"] = Session["FileLampiranOP"] as byte[];
            row["FileName"] = Session["FileName"].ToString().Trim();

            dt2.Rows.Add(row);
            DataSet ds2 = new DataSet();
            ds2.Tables.Add(dt2);

            if (((Users)Session["Users"]).UnitKerjaID == 1)
            {
                try
                {
                    WebReference_Krwg.Service1 bpas2 = new WebReference_Krwg.Service1();
                    //bpas2.InsertWOAntarPlant(dt);
                    //bpas2.InsertLampiran(dt);
                }
                catch
                {
                    DisplayAJAXMessage(this, "Koneksi Database ke Karawang error");
                }

            }
            else
            {
                try
                {
                    WebReference_Ctrp.Service1 bpas2 = new WebReference_Ctrp.Service1();
                    //bpas2.InsertWOAntarPlant(dt);
                    //bpas2.InsertLampiran(dt2);
                }
                catch
                {
                    DisplayAJAXMessage(this, "Koneksi Database ke Citeureup error");
                }
            }
        }

        private void ApprovalPreview(int BAID, Repeater lstApp)
        {

        }

        private void LoadDetailBA(string BAID, Repeater lst)
        {

        }
        private void LoadListAttachment(string WOID, Repeater lst)
        {
            ArrayList arrWork = new ArrayList();
            WorkOrder_New work1 = new WorkOrder_New();
            WorkOrderFacade_New workF1 = new WorkOrderFacade_New();
            string cekWOID = workF1.RetrieveWOID(WOID);

            if (cekWOID == string.Empty)
            {
                arrWork = workF1.RetrieveListLampiranKosong();
            }
            else
            {
                arrWork = workF1.RetrieveListLampiran(WOID);
            }

            lst.DataSource = arrWork;
            lst.DataBind();
        }

        private void LoadListPlant(string WOID, Repeater lst)
        {
            ArrayList arrWork2 = new ArrayList();
            WorkOrder_New work2 = new WorkOrder_New();
            WorkOrderFacade_New workF2 = new WorkOrderFacade_New();
            //string cekWOID = workF2.RetrieveWOID(WOID);

            arrWork2 = workF2.RetrieveListDept(WOID);

            lst.DataSource = arrWork2;
            lst.DataBind();
        }

        private bool CheckAttach(int WOID)
        {
            bool result = false;
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "Select ID from WorkOrder_Lampiran where RowStatus>-1 and WOID=" + WOID.ToString();
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                result = true;
            }
            return result;
        }

        private ArrayList ListWOTahun()
        {
            ArrayList arrData = new ArrayList();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "select DISTINCT(LEFT(convert(char,CreatedTime,112),4))Tahun from workorder where RowStatus>-1 order by CreatedTime";
            SqlDataReader sdr = zw.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new WorkOrder_New
                        {
                            Tahun = Convert.ToInt32(sdr["Tahun"].ToString())
                        });
                    }
                }
            }
            else
            {
                arrData.Add(new WorkOrder_New { Tahun = DateTime.Now.Year });
            }
            return arrData;
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        public void KirimEmail2(string NoWO)
        {
            string Flag = Session["UnApprove"].ToString();
            int StatusApv = Convert.ToInt32(Session["StatusApv"]);
            string AreaWO = Session["AreaWO"].ToString().Trim();
            int Apv = Convert.ToInt32(Session["Apv"]);
            Users users = (Users)Session["Users"];

            WorkOrder_New wn1 = new WorkOrder_New();
            WorkOrderFacade_New wnF1 = new WorkOrderFacade_New();
            wn1 = wnF1.RetrieveData(NoWO);

            int ToDept = wn1.ToDept;
            int FromDept = wn1.DeptID_Users;

            int VerSec = wn1.VerifikasiSec;
            int VerISO = wn1.VerISO;
            int ApvOP = wn1.ApvOP;

            int corp = 0;
            if (/**FromDept == 7 ||**/ FromDept == 15 || FromDept == 26 || FromDept == 12 || FromDept == 23 || FromDept == 24)
            { corp = 1; Session["corp"] = corp; }
            else { corp = 0; Session["corp"] = corp; }

            int corp1 = Convert.ToInt32(Session["corp"]);

            // WO baru dibuat .... tidak di pakai XXXX
            if (ToDept == 14 && Apv == 0)
            {
                int StsApv1 = 2; int DeptID1 = 0; int UserID1 = 0; string DeptName = "IT";
                Session["StsApv"] = StsApv1; Session["DeptID"] = DeptID1; Session["UserID"] = UserID1; Session["Dept"] = DeptName;
                string NamaPlantSession = ""; Session["NamaPlant"] = NamaPlantSession;
            }
            // WO HardWare Approved PM ....
            else if (ToDept == 14 && Apv == 2 && AreaWO == "HardWare") // Kirim Email Ke User Status 3 u/ WO Hardware ( Ctrp : Pak Iko , Krwg : Pak Sodik )
            {
                int StsApv1 = 3; int DeptID1 = 14; int UserID1 = 0; string DeptName = "IT";
                Session["StsApv"] = StsApv1; Session["DeptID"] = DeptID1; Session["UserID"] = UserID1; Session["Dept"] = DeptName;
                string NamaPlantSession = ""; Session["NamaPlant"] = NamaPlantSession;
            }
            // WO HardWare Approved Manager Peminta u/ DepartMent diBawah PM ....
            else if (ToDept == 14 && Apv == 1 && AreaWO == "HardWare") // Kirim Email Ke PM
            {
                int StsApv1 = 2; int DeptID1 = 14; int UserID1 = 0; string DeptName = "IT";
                Session["StsApv"] = StsApv1; Session["DeptID"] = DeptID1; Session["UserID"] = UserID1; Session["Dept"] = DeptName;
                string NamaPlantSession = ""; Session["NamaPlant"] = NamaPlantSession;
            }
            // WO SoftWare Approved Head ISO .....
            else if (ToDept == 14 && Apv == 2 && AreaWO == "SoftWare" && VerISO == 1 && ApvOP == 2
                || ToDept == 14 && Apv == 2 && AreaWO == "SoftWare" && VerISO == 1 && ApvOP == -2)
            {
                int StsApv1 = 3; int DeptID1 = 14; int UserID1 = 0; string DeptName = "IT";
                Session["StsApv"] = StsApv1; Session["DeptID"] = DeptID1; Session["UserID"] = UserID1; Session["Dept"] = DeptName;
                string NamaPlantSession = ""; Session["NamaPlant"] = NamaPlantSession;
            }

            // 1. WO SoftWare Approved Manager Pembuat WO bukan Corporate <<Next>> Kirim Email Ke PM - Test OK
            else if (ToDept == 14 && Apv == 1 && AreaWO == "SoftWare" && VerISO == 0 && StatusApv != 10 && corp1 != 1 && ApvOP != 2)
            {
                int StsApv1 = 2; int DeptID1 = 0; int UserID1 = 0; string DeptName = "IT";
                Session["StsApv"] = StsApv1; Session["DeptID"] = DeptID1; Session["UserID"] = UserID1; Session["Dept"] = DeptName;
                string NamaPlantSession = ""; Session["NamaPlant"] = NamaPlantSession;
            }

            // 1. WO SoftWare Approved Manager Pembuat WO Lintas Plant <<Next>> Kirim Email Ke Pak Iko & Pak Odik - Test OK
            // 2. WO SoftWare Manager Peminta u/ Dept Corporate <<Next>> Kirim Email Ke Pak Iko & Pak Odik - Test OK
            else if (ToDept == 14 && Apv == 1 && AreaWO == "SoftWare" && ApvOP == 2 && StatusApv != 10 && corp1 != 1)
            //|| ToDept == 14 && Apv == 1 && AreaWO == "SoftWare" && VerISO == 0 && corp1 == 1 && StatusApv != 10)
            {
                int StsApv1 = 12; int DeptID1 = 0; int UserID1 = 0; string DeptName = "IT";
                Session["StsApv"] = StsApv1; Session["DeptID"] = DeptID1; Session["UserID"] = UserID1; Session["Dept"] = DeptName;
                string NamaPlantSession = ""; Session["NamaPlant"] = NamaPlantSession;
            }
            // 1. WO IT-SoftWare Approved PM u/ Dept. Dibawah nya <<Next>> Kirim Email Ke Bu Mia - Test OK
            // 2. WO IT-SoftWare Approved Mgr Corp. <<Next>> Kirim Email Ke Bu Mia - Test OK
            else if (ToDept == 14 && Apv == 2 && AreaWO == "SoftWare" && VerISO == 0 && corp1 == 0 && StatusApv != 10
                || ToDept == 14 && Apv == 1 && AreaWO == "SoftWare" && VerISO == 0 && corp1 == 1 && StatusApv == 1)
            {
                int StsApv1 = 10; int DeptID1 = 0; int UserID1 = 0; string DeptName = "IT";
                Session["StsApv"] = StsApv1; Session["DeptID"] = DeptID1; Session["UserID"] = UserID1; Session["Dept"] = DeptName;
                string NamaPlantSession = ""; Session["NamaPlant"] = NamaPlantSession;
            }

            //// WO SoftWare Manager Peminta u/ Dept Corporate ..... OK
            //else if (ToDept == 14 && Apv == 1 && AreaWO == "SoftWare" && VerISO == 0 && corp1 == 1 && StatusApv != 10)
            //{
            //    int StsApv1 = 13; int DeptID1 = 0; int UserID1 = 0; string DeptName = "IT";
            //    Session["StsApv"] = StsApv1; Session["DeptID"] = DeptID1; Session["UserID"] = UserID1; Session["Dept"] = DeptName;
            //    string NamaPlantSession = ""; Session["NamaPlant"] = NamaPlantSession;
            //}

            // Cek Ulang
            else if (ToDept == 14 && Apv == 2 && AreaWO == "SoftWare" && VerISO == 0 && corp1 == 0 && StatusApv == 10
                     || ToDept == 14 && Apv == 1 && AreaWO == "SoftWare" && VerISO == 0 && corp1 == 1 && StatusApv == 10)
            {
                int StsApv1 = 3; int DeptID1 = 14; int UserID1 = 0; string DeptName = "IT";
                Session["StsApv"] = StsApv1; Session["DeptID"] = DeptID1; Session["UserID"] = UserID1; Session["Dept"] = DeptName;
                string NamaPlantSession = ""; Session["NamaPlant"] = NamaPlantSession;
            }
            else if (ToDept == 14 && Apv == 1 && AreaWO == "SoftWare" && VerISO == 0 && FromDept != 7 && StatusApv == 10 && corp1 == 0)
            {
                int StsApv1 = 3; int DeptID1 = 14; int UserID1 = 0; string DeptName = "IT";
                Session["StsApv"] = StsApv1; Session["DeptID"] = DeptID1; Session["UserID"] = UserID1; Session["Dept"] = DeptName;
                string NamaPlantSession = ""; Session["NamaPlant"] = NamaPlantSession;
            }

            // KE MAINTENANCE
            else if (ToDept == 19 && AreaWO != "Kendaraan" && Apv == 2 || ToDept == 19 && AreaWO == "Kendaraan" && Apv == 2 && VerSec == 1)
            {
                int StsApv1 = 3; int DeptID1 = ToDept; int UserID1 = 0; string DeptName = "MAINTENANCE";
                Session["StsApv"] = StsApv1; Session["DeptID"] = DeptID1; Session["UserID"] = UserID1; Session["Dept"] = DeptName;
                string NamaPlantSession = ""; Session["NamaPlant"] = NamaPlantSession;
            }

            // KE HRD
            else if (ToDept == 7)
            {
                int StsApv1 = 3; int DeptID1 = ToDept; int UserID1 = 0; string DeptName = "HRD & GA";
                Session["StsApv"] = StsApv1; Session["DeptID"] = DeptID1; Session["UserID"] = UserID1; Session["Dept"] = DeptName;
                string NamaPlantSession = ""; Session["NamaPlant"] = NamaPlantSession;
            }

            // WO IT - SoftWare Approved Head ISO , Next Kirim email ke Manager Peminta Lintas Plant ..... OK 
            else if (ToDept == 14 && Apv == 2 && AreaWO == "SoftWare" && VerISO == 1 && ApvOP == 0)
            {
                int StsApv1 = 3; int DeptID1 = ToDept; int UserID1 = 0; string DeptName = "IT";
                Session["StsApv"] = StsApv1; Session["DeptID"] = DeptID1; Session["UserID"] = UserID1; Session["Dept"] = DeptName;
                if (users.UnitKerjaID == 1)
                {
                    string NamaPlantSession = "Citeureup"; Session["NamaPlant"] = NamaPlantSession;
                }
                else if (users.UnitKerjaID == 7)
                { string NamaPlantSession = "Karawang"; Session["NamaPlant"] = NamaPlantSession; }
                else if (users.UnitKerjaID == 13)
                { string NamaPlantSession = "Jombang"; Session["NamaPlant"] = NamaPlantSession; }
            }
            else if (ToDept == 14 && AreaWO == "SoftWare" && VerISO == 1 && ApvOP == 0)
            {
                int StsApv1 = 1; int DeptID1 = ToDept; int UserID1 = 0; string DeptName = "IT";
                Session["StsApv"] = StsApv1; Session["DeptID"] = DeptID1; Session["UserID"] = UserID1; Session["Dept"] = DeptName;
                if (users.UnitKerjaID == 7)
                {
                    string NamaPlantSession = "Karawang"; Session["NamaPlant"] = NamaPlantSession;
                }
                else if (users.UnitKerjaID == 1)
                { string NamaPlantSession = "Citeureup"; Session["NamaPlant"] = NamaPlantSession; }
                else if (users.UnitKerjaID == 13)
                {
                    string NamaPlantSession = "Jombang"; Session["NamaPlant"] = NamaPlantSession;
                }
            }

            /** Region Proses Send Email **/
            string NamaPlant = Session["NamaPlant"].ToString();
            int StsApv = Convert.ToInt32(Session["StsApv"]);
            int DeptID = Convert.ToInt32(Session["DeptID"]);
            int UserID = Convert.ToInt32(Session["UserID"]);
            string TanggalBuat = Convert.ToDateTime(Session["TanggalBuat"]).ToString("dd-MM-yyyy");
            string NamaDept = Session["Dept"].ToString();
            string Uraian = Session["UraianPekerjaan"].ToString();

            WorkOrder_New wn2 = new WorkOrder_New();
            WorkOrderFacade_New wnF2 = new WorkOrderFacade_New();
            wn2 = wnF2.CekNamaDeptPembuat(FromDept);

            ArrayList arrEmaila = new ArrayList();
            WorkOrder_New wn = new WorkOrder_New();
            WorkOrderFacade_New wnF = new WorkOrderFacade_New();
            //wn = wnF.RetrieveDataEmail(DeptID, UserID, StsApv, wn1.PlantID, users.UnitKerjaID, VerISO, ApvOP, FromDept);

            arrEmaila = wnF.RetrieveDataEmailArray(DeptID, UserID, StsApv, wn1.PlantID, users.UnitKerjaID, VerISO, ApvOP, FromDept, AreaWO, ToDept);
            //string AlamatEmail = wn.AccountEmail.Trim();
            string NomorWO = Session["NoWO"].ToString();

            MailMessage mail = new MailMessage();
            SmtpClient Smtp = new SmtpClient();
            mail.From = new MailAddress("system_support@grcboard.com", "-WO System Support-");
            //mail.To.Add(new MailAddress(AlamatEmail));
            //mail.To.Add(new MailAddress("beny@grcboard.com"));

            foreach (WorkOrder_New ListEmaila in arrEmaila)
            {
                mail.To.Add(new MailAddress(ListEmaila.AccountEmail.Trim()));
            }

            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            if (Flag == "UnApprove")
            {
                mail.Subject = "Pemberitahuan WO Un-Approve : " + NomorWO.Trim();
                mail.Body += "Pemberitahuan Un-Approve Work Order dibawah ini : \n\r\n\r";
                mail.Body += "WO Nomor            : " + NomorWO.Trim() + "\n\r";
                mail.Body += "Dept. Pembuat       : " + wn2.NamaDept.Trim() + " - " + NamaPlant + "\n\r";
                mail.Body += "Dept. Tujuan        : " + NamaDept.Trim() + "\n\r";
                mail.Body += "Permintaan WO       : " + Uraian.Trim() + "\n\r";
                mail.Body += "Not Approved By     : " + users.UserName.Trim() + "\n\r\n\r";
                mail.Body += "Alasan Un-Approved  : " + wn1.AlasanCancel.Trim() + "\n\r\n\r\n\r ";
            }
            else
            {
                mail.Subject = "Permohonan Approval Work Order : " + NomorWO.Trim();
                mail.Body += "Mohon Approval untuk Work Order di bawah ini : \n\r\n\r";
                mail.Body += "WO Nomor        : " + NomorWO.Trim() + "\n\r";
                mail.Body += "Tanggal Buat    : " + TanggalBuat + "\n\r";
                mail.Body += "Dept. Pembuat   : " + wn2.NamaDept.Trim() + " - " + NamaPlant + "\n\r";
                mail.Body += "Dept. Tujuan    : " + NamaDept.Trim() + "\n\r";
                mail.Body += "Approved By     : " + users.UserName.Trim() + "\n\r\n\r";
                mail.Body += "Permintaan WO   : " + Uraian.Trim() + "\n\r\n\r\n\r ";

            }

            //mail.Subject = "Permohonan Approval Work Order : " + NomorWO.Trim();
            //mail.Body += "Mohon Approval untuk Work Order di bawah ini : \n\r\n\r";
            //mail.Body += "WO Nomor        : " + NomorWO.Trim() + "\n\r";
            //mail.Body += "Tanggal Buat    : " + TanggalBuat + "\n\r";
            //mail.Body += "Dept. Pembuat   : " + wn2.NamaDept.Trim() + " - " + NamaPlant + "\n\r";
            //mail.Body += "Dept. Tujuan    : " + NamaDept.Trim() + "\n\r";
            //mail.Body += "Approved By     : " + users.UserName.Trim() + "\n\r\n\r";
            //mail.Body += "Permintaan WO   : " + Uraian.Trim() + "\n\r\n\r\n\r ";

            mail.Body += "Terima Kasih, \n\r\n\r\n\r";
            mail.Body += "PT. BANGUNPERKASA ADHITAMASENTRA \n\r";
            mail.Body += "- Ahlinya Papan Semen - \n\r";

            Smtp.Host = "mail.grcboard.com";
            Smtp.Port = 587;
            System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
            NetworkCred.UserName = "system_support@grcboard.com";
            NetworkCred.Password = "grc123!@#";
            Smtp.EnableSsl = true;
            Smtp.UseDefaultCredentials = false;
            Smtp.Credentials = NetworkCred;
            Smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            try
            {
                Smtp.Send(mail);
            }

            catch (Exception ex)
            { }

        }
    }
}