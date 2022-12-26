using BusinessFacade;
using DataAccessLayer;
using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.ListReport
{
    public partial class NewLapPemantauanStatusSPP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Global.link = "~/Default.aspx";
                //TahunSPP();
                txtDrTgl.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtSdTgl.Text = DateTime.Now.ToString("dd-MMM-yyyy");

                LoadPurchnGroup();

                ddlGroup.Items[0].Selected = true;

            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(ExportXls);
        }
        private void TahunSPP()
        {
            ArrayList arrTH = new ArrayList();
            arrTH = new SPPFacade().TahunSPP();
            ddlTahun.Items.Clear();
            ddlTahun.Items.Add(new ListItem("--Pilih--", ""));
            foreach (SPP sp in arrTH)
            {
                ddlTahun.Items.Add(new ListItem(sp.ID.ToString(), sp.ID.ToString()));
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }


        protected void preview_Click(object sender, EventArgs e)
        {
            load();
            #region validasi tanggal
            if (DateTime.Parse(txtDrTgl.Text) > DateTime.Parse(txtSdTgl.Text)) return;
            #endregion
            StatusSPP Sts = new StatusSPP();
            string[] arrDept = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("OnlyDeptID", "PO").Split(',');
            string[] arrUsr = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ExceptionUser", "SPP").Split(',');
            string uid = ((Users)Session["Users"]).ID.ToString();
            Sts.Limit = "";
            Sts.Fields = "SPP.ID,SPP.NoSPP,(select Top 1 AppDate from EventApprovalLog where DocType='SPP' and DocNo=SPP.NoSPP and AppLevel=3)ApproveDate3,SPP.ItemTypeID";
            Sts.Criteria = "and Convert(Char,SPP.ApproveDate3,112) between '" + DateTime.Parse(txtDrTgl.Text).ToString("yyyyMMdd") + "' and '" +
                          DateTime.Parse(txtSdTgl.Text).ToString("yyyyMMdd") + "'";

            string txtgrp = string.Empty;

            for (int i = 0; i < ddlGroup.Items.Count; i++)
            {
                if (ddlGroup.Items[i].Selected)
                {
                    txtgrp += ddlGroup.Items[i].Value + ",";
                }
            }

            txtgrp = (txtgrp != string.Empty) ? txtgrp.Substring(0, (txtgrp.Length - 1)) : string.Empty;
            Sts.kriteria = (txtgrp != string.Empty) ? " and SPP.GroupID  in(" + txtgrp + ")" : string.Empty;
            Sts.inventory = (rbtStock.Checked || rbtNonStock.Checked) ? "inner join Inventory i on i.ID=SPPDetail.ItemID " : string.Empty;
            Sts.Type = (rbtStock.Checked) ? "i.Stock=1 and " : (rbtNonStock.Checked) ? "i.Stock=0 and " : string.Empty;




            if ((arrDept.Contains(((Users)Session["Users"]).DeptID.ToString()) ||
                arrUsr.Contains(((Users)Session["Users"]).UserID.ToString())))
            {
                Sts.Criteria += "";
            }
            else
            {
                if (((Users)Session["Users"]).UserLevel > 0)
                {
                    Sts.Criteria += " and UserID in(select ID from users where DeptID in(" + ((Users)Session["Users"]).DeptID.ToString() + "))";
                }
                else
                {
                    Sts.Criteria += " and (userID=" + uid + " or HeadID=" + uid + ") ";
                }
            }
            Sts.Criteria += (arrUsr.Contains(((Users)Session["Users"]).UserID.ToString()) && (ddlGroupID.SelectedValue == "0")) ? " and GroupID in(3,4,6,8,9) " : "";
            Sts.Criteria += (noStatusPO.Checked == true) ? " and ((select COUNT(ID) from SPPDetail where SPPID=SPP.ID and SPPDetail.Status>-1 and ItemID>0)> " +
                                                           "(Select COUNT(ID) from SPPDetail where SPPID=SPP.ID and SPPDetail.Status>-1 and QtyPO>0 and ItemID>0))" : "";
            Sts.Orderby = "order by ApproveDate3 desc";
            ArrayList arrData = Sts.Retrieve();
            lstSPP.DataSource = arrData;
            lstSPP.DataBind();
            Repeater1.DataSource = arrData;
            Repeater1.DataBind();

            //load();
        }

        private void load()
        {

        }

        protected void ExportToExcel(object sender, EventArgs e)
        {
            foreach (RepeaterItem img in lstSPP.Items)
            {
                Repeater rpt = (Repeater)img.FindControl("lstDetailSPP");
                foreach (RepeaterItem rm in rpt.Items)
                {
                    Image im = (Image)rm.FindControl("UpdSts");
                    Panel edt = (Panel)rm.FindControl("edt");
                    im.Visible = false;
                    edt.Visible = false;
                }
            }

            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=PemantauanStatusSPP.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "Material Group :" + ddlGroupID.SelectedItem.ToString().ToUpper();
            Html += "<br>Periode : " + txtDrTgl.Text + " s/d " + txtSdTgl.Text;
            Html += "<br><form id='frm1' runat='server' method='post'>";
            string HtmlEnd = "</form>";
            //lstForPrint.RenderControl(hw);
            lst.RenderControl(hw);
            string Contents = sw.ToString();
            //Contents = Contents.Replace("", "");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }
        protected void lstSPP_DataBound(object sender, RepeaterItemEventArgs e)
        {
            SPP spp = (SPP)e.Item.DataItem;
            string noPO = (noStatusPO.Checked == true) ? " and QtyPO=0" : "";
            Repeater lstDetail = (Repeater)e.Item.FindControl("lstDetailSPP");
            ArrayList arrDataDetail = new StatusSPP().ReceiptDetail(spp.ID.ToString() + noPO);
            lstDetail.DataSource = arrDataDetail;
            lstDetail.DataBind();
        }
        protected void lstSPP_DataBound4(object sender, RepeaterItemEventArgs e)
        {
            SPP spp = (SPP)e.Item.DataItem;
            string noPO = (noStatusPO.Checked == true) ? " and QtyPO=0" : "";
            Repeater lstDetail = (Repeater)e.Item.FindControl("lstDetailSPP4");
            ArrayList arrDataDetail = new StatusSPP().ReceiptDetail(spp.ID.ToString() + noPO);
            lstDetail.DataSource = arrDataDetail;
            lstDetail.DataBind();
        }
        protected void ddlStatus_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < lstSPP.Items.Count; i++)
            {
                Repeater rpt = (Repeater)lstSPP.Items[i].FindControl("lstDetailSPP");
                for (int n = 0; n < rpt.Items.Count; n++)
                {
                    DropDownList ddl = (DropDownList)rpt.Items[n].FindControl("ddlStatus");
                    TextBox txt = (TextBox)rpt.Items[n].FindControl("txtInputMan");
                    //ddl.Attributes.Add("onchange", "return alasanpending(" + ddl.SelectedValue.ToString() + ")");
                    if (ddl.SelectedValue == "0" || ddl.SelectedValue == "2" || ddl.SelectedValue == "3")
                    {
                        ddl.Visible = false;
                        txt.Visible = true;
                    }
                    else
                    {
                        ddl.Visible = true;
                        txt.Visible = false;
                    }
                }
            }
            //DropDownList ddl = (DropDownList)sender;
            //if (ddl.SelectedValue.ToString() == "3")
            //{
            //    //ScriptManager.RegisterStartupScript(this,this.GetType(), "alert", "return alasanpending()",true);

            //}
        }
        protected void lstDtlSPP_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Image upd = (Image)e.Item.FindControl("updSts");
            Image sim = (Image)e.Item.FindControl("simpan");
            Label lbl = (Label)e.Item.FindControl("txt");
            Panel dll = (Panel)e.Item.FindControl("edt");
            DropDownList ddl = (DropDownList)e.Item.FindControl("ddlStatus");
            NewSPPDetail spp = (NewSPPDetail)e.Item.DataItem;
            string[] arrDept = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("OnlyDeptID", "PO").Split(',');
            lbl.Attributes.Add("title", (spp.Status == 2) ? new StatusSPP().FindPO(spp.ID) : spp.Satuan);

            if (spp.Status == 2)
            {
                upd.Visible = false;
                sim.Visible = false;
                ddl.Visible = false;
                lbl.Visible = true;
                dll.Visible = false;
            }
            else if (spp.Status != 2 && arrDept.Contains(((Users)Session["Users"]).DeptID.ToString()))
            {
                upd.Visible = true;
                sim.Visible = false;
                ddl.Visible = false;
                lbl.Visible = true;
                dll.Visible = false;
            }
            else
            {
                upd.Visible = false;
                sim.Visible = false;
                ddl.Visible = false;
                lbl.Visible = true;
                dll.Visible = false;
            }
        }
        protected void lstDtlSPP_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            Image upd = (Image)e.Item.FindControl("updSts");
            Image sim = (Image)e.Item.FindControl("simpan");
            DropDownList ddl = (DropDownList)e.Item.FindControl("ddlStatus");
            Label lbl = (Label)e.Item.FindControl("txt");
            Panel dll = (Panel)e.Item.FindControl("edt");
            HtmlGenericControl spn = (HtmlGenericControl)e.Item.FindControl("ext");
            TextBox txt = (TextBox)e.Item.FindControl("txtInputMan");
            string ID = e.CommandArgument.ToString();
            string cmd = e.CommandName.ToString();
            if (cmd == "upd")
            {
                sim.Visible = true;
                ddl.Visible = true;
                dll.Visible = true;
                lbl.Visible = false;
                upd.Visible = false;
                spn.Visible = false;
            }
            else if (cmd == "save")
            {
                NewSPPDetail spd = new NewSPPDetail();
                spd.ID = int.Parse(ID.ToString());
                spd.AlasanPending = (txt.Visible == true) ? txt.Text : ddl.SelectedItem.Text;
                spd.PendingPO = (ddl.SelectedIndex == 0) ? 1 : 0;
                spd.Status = ddl.SelectedIndex;
                int result = new NewSPPDetailFacade().UpdateStatusSPP(spd);
                if (result > 0)
                {
                    sim.Visible = false;
                    ddl.Visible = false;
                    dll.Visible = false;
                    lbl.Text = (txt.Visible == true) ? txt.Text : ddl.SelectedItem.ToString();
                    lbl.Visible = true;
                    upd.Visible = true;
                    spn.Visible = true;
                }
            }
        }

        private void LoadPurchnGroup()
        {
            try
            {
                ArrayList arrSPP = new ArrayList();
                GroupsPurchnFacade grps = new GroupsPurchnFacade();
                grps.without = " and A.ID !=10";
                arrSPP = grps.Retrieve();
                ddlGroupID.Items.Clear();
                ddlGroupID.Items.Add(new ListItem("All Group", "0"));
                foreach (GroupsPurchn grp in arrSPP)
                {
                    if (grp.ID < 10)
                    {
                        ddlGroupID.Items.Add(new ListItem(grp.GroupDescription, grp.ID.ToString()));

                        //ddlGroupText += "<li style='cursor:pointer'><input type='checkbox' id='" + grp.ID.ToString() + "' value='" + grp.GroupDescription + "' />" + grp.GroupDescription + "</li><br>";
                    }
                }
                ddlGroup.DataSource = arrSPP;
                ddlGroup.DataTextField = "GroupDescription";
                ddlGroup.DataValueField = "ID";
                ddlGroup.AutoPostBack = false;
                ddlGroup.DataBind();
                ddlGroupID.SelectedValue = ((Users)Session["Users"]).GroupID.ToString();
                //ddlGroupID.Enabled =  ? true : false;
            }
            catch
            {
                Global.link = "~/Default.aspx";
            }
        }
    }

    public class StatusSPP
    {
        public string Limit { get; set; }
        public string Fields { get; set; }
        public string Criteria { get; set; }
        public string Orderby { get; set; }
        public string inventory { get; set; }
        public string Type { get; set; }
        public string kriteria { get; set; }
        ArrayList arrData = new ArrayList();
        private string Query()
        {
            string strQuery = "select " + this.Limit + " " + this.Fields + " from SPP Inner join SPPDetail on SPPDetail.SPPID = SPP.ID " + this.inventory + " where " + this.Type + " SPP.approval=3 and SPP.status >-1 " + this.Criteria + " " + this.kriteria + " group by SPP.ID,SPP.NoSPP,SPP.itemTypeID " + this.Orderby;

            return strQuery;
        }
        public ArrayList Retrieve()
        {

            //DatabaseLib dbLib = new DatabaseLib();
            //dbLib.strConn = Global.ConnectionString();
            //SqlDataReader sdr = dbLib.DataReader(this.Query());
            string test = this.Query();
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = dataAccess.RetrieveDataByString(this.Query());

            arrData = new ArrayList();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new SPP
                    {
                        ID = (sdr["ID"] == DBNull.Value) ? 0 : Convert.ToInt32(sdr["ID"].ToString()),
                        ApproveDate2 = (sdr["ApproveDate3"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(sdr["ApproveDate3"].ToString()),
                        NoSPP = (sdr["NoSPP"] == DBNull.Value) ? "" : sdr["NoSPP"].ToString(),
                        ItemTypeID = Convert.ToInt32(sdr["ItemTypeID"].ToString())
                    });
                }
            }

            return arrData;
        }
        public ArrayList ReceiptDetail(string ID)
        {
            //DatabaseLib dbLib = new DatabaseLib();
            //dbLib.strConn = Global.ConnectionString();
            //SqlDataReader sdr = dbLib.DataReader(this.dtlQuery(ID));
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = dataAccess.RetrieveDataByString(this.dtlQuery(ID));
            arrData = new ArrayList();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new NewSPPDetail
                    {
                        ItemName = sdr["ItemName"].ToString(),
                        Quantity = Convert.ToDecimal(sdr["Quantity"].ToString()),
                        ItemID = (sdr["LeadTime"] == DBNull.Value) ? 0 : Convert.ToInt32(sdr["LeadTime"].ToString()),
                        TglKirim = Convert.ToDateTime(sdr["sch"].ToString()),
                        AlasanPending = (sdr["ReceiptDate"] == DBNull.Value) ? "" : sdr["ReceiptDate"].ToString().Substring(0, 10),
                        CariItemName = sdr["ReceiptNo"].ToString(),
                        Status = Convert.ToInt32(sdr["Status"].ToString()),
                        PendingPO = (sdr["PendingPO"] == DBNull.Value) ? 0 : Convert.ToInt32(sdr["PendingPO"].ToString()),
                        ItemCode = (int.Parse(sdr["Late"].ToString()) <= 0) ? "" : sdr["Late"].ToString(),
                        Satuan = sdr["Stat"].ToString(),
                        ID = Convert.ToInt32(sdr["ID"].ToString())
                    });
                }
            }
            return arrData;
        }
        private string dtlQuery(string SPPID)
        {
            string strQuery = "SET DATEFIRST 1; select *,case when (ReceiptDate is not null and ReceiptDate > Sch) then (select dbo.GetWorkingDay(Sch,ReceiptDate)) else 0 end Late from " +
                            "(select ID,SPPID,ItemName,Quantity,QtyPO,Keterangan,LeadTime,AppDate,tglkirim," +
                            " (" + this.sch() + ") Sch,ReceiptDate,ReceiptNo,Status,PendingPO,PermintaanType," + this.StatSPP() + " Stat " +
                            "from( select *," + this.ItemName() +
                            "(select ApproveDate2 from SPP where ID=SPPID)AppDate, (" + rmsQuery("ReceiptDate", "SPPDetail.ID") + ") as ReceiptDate,(" +
                             rmsQuery("ReceiptNo", "SPPDetail.ID") + ") as ReceiptNo, " +
                            "(select top 1 DlvDate from POPurchnDetail where SppDetailID=SPPDetail.ID and Status>-1 and ItemID=SPPDetail.ItemID) Deliv," +
                            "(select PermintaanType From SPP where ID=SPPDetail.SPPID) PermintaanType " +
                            "from SPPDetail where Status >-1 and  SPPID=" + SPPID +
                            " ) as x ) as m";
            return strQuery;
        }
        private string rmsQuery(string field, string ID)
        {
            return "select top 1 r." + field + " from ReceiptDetail as rd " +
                   " Left Join Receipt as r " +
                   " on r.ID=rd.ReceiptID " +
                   " where PODetailID in(select ID from POPurchnDetail where SppDetailID=" + ID + " and Status >-1)" +
                   " and rd.RowStatus>-1 and r.Status>-1";
        }
        private string sch()
        {
            return "case when Deliv is null /*and PermintaanType=2*/ then " +
                   "Case when (select dbo.GetOFFDay(DATEADD(DAY,1,AppDate),(DATEADD(DAY,LeadTime,DATEADD(DAY,1,AppDate)))))>0 then " +
                   "    /* DATEADD(DAY,LeadTime,AppDate) ELSE*/ " +
                   " CASE (SELECT DATEPART(WEEKDAY,(DATEADD(DAY,(LeadTime+(select dbo.GetOFFDay(AppDate,(DATEADD(DAY,LeadTime,AppDate))))),AppDate))))  " +
                   "     WHEN 6 THEN DATEADD(DAY,(3+LeadTime+(select dbo.GetOFFDay(AppDate,(DATEADD(DAY,LeadTime,AppDate))))),AppDate) " +
                   "     WHEN 7 THEN DATEADD(DAY,(2+LeadTime+(select dbo.GetOFFDay(AppDate,(DATEADD(DAY,LeadTime,AppDate))))),AppDate) " +
                   "     ELSE " +
                   "     DATEADD(DAY,(0+LeadTime+(select dbo.GetOFFDay(AppDate,(DATEADD(DAY,LeadTime,AppDate))))),AppDate) " +
                   "     END" +
                   " ELSE DATEADD(DAY,(LeadTime+(select dbo.GetOFFDay(AppDate,(DATEADD(DAY,LeadTime,AppDate))))),AppDate) " +
                   " end " +
                   " when (Deliv is null and PermintaanType=1) then AppDate " +
                   " when (Deliv is null and PermintaanType=3) then tglkirim " +
                   " else Deliv end";
        }
        private string StatSPP()
        {
            return "case when (Status=0 and (PendingPO=0 or PendingPO is null)) then '' " +
                   "  when (status=0 and PendingPO=1) then 'Spesifikasi tidak lengkap' + ' - ' + AlasanPending " +
                   "  when (status=1) then 'Menunggu Perbandingan Harga' " +
                   "  when (status=3) then 'Input Manual' + ' - ' + AlasanPending " +
                   "  when (status=2) Then 'Status PO' end";
        }
        private string ItemName()
        {
            return "Case (select ItemTypeID from SPP where ID=SppDetail.SPPID) " +
                   "     When 1 then (select ItemName From Inventory where ID=ItemID) " +
                   "     When 2 then (select ItemName From Asset where ID=ItemID) " +
                   "     when 3 then SPPDetail.Keterangan end ItemName, " +
                   "  Case (select ItemTypeID from SPP where ID=SppDetail.SPPID) " +
                   "     when 1 then isnull((select top 1 LeadTime From Inventory where ID=ItemID),0) " +
                   "     when 2 then isnull((select top 1 LeadTime From Asset where ID=ItemID),0) " +
                   "     when 3 then isnull((select top 1 LeadTime From Biaya where ItemName=SPPDetail.Keterangan and rowstatus>-1),0) end LeadTime,";
        }
        public string FindPO(int SPPDetailID)
        {
            //DatabaseLib dbLib = new DatabaseLib();
            string result = string.Empty;
            //dbLib.strConn = Global.ConnectionString();
            string strSQL = "select NoPO, Case Approval when 0 then 'Admin' when 1 then 'Head' when 2 then 'Ast.Manager' when 3 then 'Manager' end App from POpurchn where id in(select POID from POPurchnDetail where SppDetailID=" + SPPDetailID.ToString() + ")";
            //SqlDataReader sdr = dbLib.DataReader(strSQL);
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = dataAccess.RetrieveDataByString(strSQL);
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = sdr["NoPO"].ToString();// +" Apv :" + sdr["App"].ToString();
                }
            }
            return result;
        }
    }
    public class DatabaseLib
    {
        public string strConn { get; set; }
        public SqlDataReader DataReader(string Query)
        {
            DataAccess dta = new DataAccess(this.strConn);
            SqlDataReader sdr = dta.RetrieveDataByString(Query);
            return sdr;
        }
    }
}
