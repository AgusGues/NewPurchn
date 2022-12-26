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
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.ListReport
{
    public partial class LapPemantauanNotMatchSPP : System.Web.UI.Page
    {
        public decimal TotalSPP = 0;
        public decimal TotalLate = 0;
        public decimal Procentase = 0;
        public string ddlGroupText;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["TotalSPP"] = "";
                Session["TotalLate"] = "";
                Session["Pros"] = "";
                Global.link = "~/Default.aspx";
                txtDrTgl.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtSdTgl.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                LoadPurchnGroup();
                ddlGroup.Items[0].Selected = true;
                rbData.Items[1].Selected = true;
            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(ExportXls);
        }
        protected void preview_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void ExportToExcel(object sender, EventArgs e)
        {
            string txtgrp = string.Empty;
            for (int i = 0; i < ddlGroup.Items.Count; i++)
            {
                if (ddlGroup.Items[i].Selected)
                {
                    txtgrp += ddlGroup.Items[i].Text + ",";
                }
            }
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=PemantauanDelivery.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<b>LEMBAR PEMANTAUAN ON TIME & KESESUAIAN PEMENUHAN SPP</b><br>";
            Html += "Material Group : " + txtgrp.ToUpper();
            Html += "<br>Periode : " + txtDrTgl.Text + " s/d " + txtSdTgl.Text;
            Html += "";
            string HtmlEnd = "";
            lst.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0\"", "border=\"1\"");
            Contents = Contents.Replace("tp\">", "\">" + Session["TotalSPP"].ToString());
            Contents = Contents.Replace("tm\">", "\">" + Session["TotalLate"].ToString());
            Contents = Contents.Replace("pr\">", "\">" + Session["Pros"].ToString());
            Contents = Contents.Replace("%<", "<");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }
        private void LoadData()
        {
            Session["TotalSPP"] = "";
            Session["TotalLate"] = "";
            Session["Pros"] = "";
            ArrayList arrData = new ArrayList();
            MatchSPP mSPP = new MatchSPP();
            string txtgrp = string.Empty;
            string txtrb = string.Empty;
            for (int i = 0; i < ddlGroup.Items.Count; i++)
            {
                if (ddlGroup.Items[i].Selected)
                {
                    txtgrp += ddlGroup.Items[i].Value + ",";
                }
            }
            for (int n = 0; n < rbData.Items.Count; n++)
            {
                if (rbData.Items[n].Selected)
                {
                    txtrb = rbData.Items[n].Value.ToString();
                }
            }
            txtgrp = (txtgrp != string.Empty) ? txtgrp.Substring(0, (txtgrp.Length - 1)) : string.Empty;
            mSPP.FromDate = DateTime.Parse(txtDrTgl.Text).ToString("yyyyMMdd");
            mSPP.ToDate = DateTime.Parse(txtSdTgl.Text).ToString("yyyyMMdd");
            mSPP.Criteria = (txtgrp != string.Empty) ? " and SPPDetail.GroupID  in(" + txtgrp + ")" : string.Empty;
            mSPP.OnlyLate = (chkAll.Checked == true) ? "and mm.Late >0 " : "";// " where mm.ReceiptDate is not null";
            mSPP.inventory = (rbtStock.Checked || rbtNonStock.Checked) ? "left join Inventory i on i.ID=SPPDetail.ItemID " : string.Empty;
            mSPP.Type = (rbtStock.Checked) ? "i.Stock=1 and " : (rbtNonStock.Checked) ? "i.Stock=0 and " : string.Empty;
            mSPP.BaseOn = txtrb;
            arrData = mSPP.Retieve();
            lstLate.DataSource = arrData;
            lstLate.DataBind();
            TotalSPP = mSPP.GetTotalSPP();
            TotalLate = mSPP.GetTotalLate();
            Procentase = ((TotalSPP - TotalLate) > 0) ? ((TotalSPP - TotalLate) / TotalSPP) * 100 : 0;
            Session["TotalSPP"] = TotalSPP.ToString();
            Session["TotalLate"] = TotalLate.ToString();
            Session["Pros"] = Procentase.ToString("N0");
            decimal aktual = 0;
            aktual = Procentase;
            string sarmutPrs = "On Time dan Kesesuaian pemenuhan SPP (diluar kategori bahan baku ,bahan penunjang & bahan bakar )";
            int deptid = getDeptID("purchasing");
            ZetroView zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery =
                "update SPD_TransPrs set actual= " + aktual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + Convert.ToDateTime(txtSdTgl.Text).ToString("yyyy") +
                " and Bulan=" + Convert.ToDateTime(txtSdTgl.Text).ToString("MM") +
                " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "' and deptid=" + deptid + ") ";
            SqlDataReader sdr1 = zl1.Retrieve();
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
    public class MatchSPP
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Criteria { get; set; }
        public string OnlyLate { get; set; }
        public string BaseOn { get; set; }
        public string Prefix { get; set; }
        public string Type { get; set; }
        public string inventory { get; set; }
        private string Query()
        {
            string strQ = " SET DATEFIRST 1; " + this.Prefix + " select *,(select NoSPP From SPP Where ID=SPPID)NoSPP,case when ReceiptNo is null then 'Belum Datang' else '' end Ket from (" +
                            "select *,case when ReceiptDate is not null and ReceiptDate > Sch then (Select dbo.GetWorkingDay(Sch,ReceiptDate)) else 0 end Late from " +
                            "(select ID,SPPID,POno,(select SupplierName from SuppPurch where ID=(Select SupplierID from POPurchn where ID=POID))SuplierName," +
                            "ItemName,qtyreceipt Quantity,QtyPO,Keterangan,LeadTime,AppDate,tglkirim," +
                            " (" + this.sch() + ") Sch,ReceiptDate,ReceiptNo,Status,PendingPO,PermintaanType," + this.StatSPP() + " Stat " +
                            "from( select SPPDetail.*," + this.ItemName() +
                            "(select ApproveDate3 from SPP where ID=SPPDetail.SPPID)AppDate,rc.ReceiptDate,rc.ReceiptNo, rc.PONo, rc.POID, " +
                            "pod.DlvDate  Deliv, (select PermintaanType From SPP where ID=SPPDetail.SPPID) PermintaanType, rcd.Quantity qtyreceipt  " +
                            "from SPPDetail  inner join POPurchnDetail pod on SPPDetail.id=pod.SppDetailID inner join ReceiptDetail rcd on " +
                            "pod.id=rcd.PODetailID  inner join  Receipt rc on rcd.ReceiptID =rc.id " + this.inventory +
                            "where " + this.Type + " SPPDetail.Status >-1 and rcd.RowStatus>-1 and rc.Status>-1  " + this.ReadDataBaseOn() +
                            this.Criteria + " and rc.ReceiptNo not in ('JA2103-00019','JA2103-00017','JA2103-00020','JA2103-00018')) as x ) as m ) as mm where isnull(mm.PONo,'')<>'' and mm.ReceiptDate between '" + this.FromDate + "' and '" + this.ToDate + "'" +
                            this.OnlyLate + "  order by mm.ReceiptDate,mm.ItemName";
            return strQ;
        }
        public ArrayList Retieve()
        {
            ArrayList arrLate = new ArrayList();
            try
            {
                DataAccess dta = new DataAccess(Global.ConnectionString());
                string strsql = this.Query();
                SqlDataReader sdr = dta.RetrieveDataByString(this.Query());
                if (dta.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrLate.Add(new Late
                        {
                            ID = Convert.ToInt32(sdr["ID"].ToString()),
                            NoSPP = sdr["NoSPP"].ToString(),
                            ItemName = sdr["ItemName"].ToString(),
                            Quantity = Convert.ToDecimal(sdr["Quantity"].ToString()),
                            LeadTime = Convert.ToInt32(sdr["leadTime"].ToString()),
                            DelivDate = Convert.ToDateTime(sdr["Sch"].ToString()),
                            Minta = Convert.ToDateTime(sdr["ReceiptDate"].ToString()),
                            RMSNo = sdr["ReceiptNo"].ToString(),
                            Lambat = Convert.ToInt32(sdr["Late"].ToString()),
                            PONo = sdr["POno"].ToString(),
                            SupplierName = sdr["SuplierName"].ToString(),
                            Keterangan = sdr["Ket"].ToString()
                        });
                    }
                }
                return arrLate;
            }
            catch (Exception ex)
            {
                return arrLate;
            }
        }
        private string rmsQuery(string field, string ID)
        {
            return "select top 1 r." + field + " from ReceiptDetail as rd " +
                   " Left Join Receipt as r " +
                   " on r.ID=rd.ReceiptID " +
                   " where PODetailID in(select ID from POPurchnDetail where SppDetailID=" + ID + " and Status >-1)" +
                   " and rd.RowStatus>-1 and r.Status>-1 and r.ReceiptDate between '" + this.FromDate + "' and '" + this.ToDate + "'";
        }
        private string sch()
        {
            return "case when Deliv is null and PermintaanType=2 then (DATEADD(DAY,LeadTime,AppDate)) " +
                   " when (Deliv is null and PermintaanType=1) then AppDate " +
                   " when (Deliv is null and PermintaanType=3) then tglkirim " +
                   " else Deliv end";
        }
        private string StatSPP()
        {
            return "case when (Status=0 and (PendingPO=0 or PendingPO is null)) then '' " +
                   "  when (status=0) then " +
                   "    case when PendingPO=1 then 'Spesifikasi tidak lengkap' " +
                   "         when PendingPO=2 then 'Discontinoue' " +
                   "         when PendingPO=3 then AlasanPending " +
                   "    end " +
                   "  when (status=1) then 'Menunggu Perbandingan Harga' " +
                   "  when (status=2) Then 'Status PO' " +
                   "end";
        }
        private string ItemName()
        {
            return "(Select dbo.ItemNameInv((" + ItemFromRMS() + "),SPPDetail.ItemTypeID)) ItemName, " +
                   "  Case (select ItemTypeID from SPP where ID=SppDetail.SPPID) " +
                   "     when 1 then isnull((select LeadTime From Inventory where ID=SPPDetail.ItemID),0) " +
                   "     when 2 then isnull((select LeadTime From Asset where ID=SPPDetail.ItemID),0) " +
                   "     when 3 then isnull((select top 1 LeadTime From Biaya where ItemName=SPPDetail.Keterangan),0) end LeadTime,";
        }
        private string ItemFromRMS()
        {
            return "select top 1 rd.ItemID from ReceiptDetail as rd " +
                    "Left Join Receipt as r  on r.ID=rd.ReceiptID  where PODetailID in(" +
                    "select ID from POPurchnDetail where SppDetailID=SPPDetail.ID and Status >-1) and rd.RowStatus>-1 and r.Status>-1";
        }
        public decimal GetTotalSPP()
        {
            decimal result = 0;
            this.Prefix = "Select Count(ID)Jml from (";
            this.OnlyLate = ") as xxx";
            string query = this.Query().Replace("order by mm.ReceiptDate,mm.ItemName", "");
            //"Select Count(ID)Jml from SPPDetail Where Status >-1 " + this.ReadDataBaseOn() + this.Criteria;
            //"and SPPID in(" +
            //"Select ID from SPP where Convert(CHAR,Minta,112) between '" + this.FromDate + "' and '" +
            //"' and Approval >1 and Status >-1 " + this.Criteria + ")";
            //this.Criteria;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(query);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToDecimal(sdr["Jml"].ToString());
                }
            }
            return result;
        }
        public decimal GetTotalLate()
        {
            decimal result = 0;
            this.OnlyLate = " and mm.Late >0";
            string query = "SET DATEFIRST 1;Select Count(ID) Jml from (";
            query += " select *,(select NoSPP From SPP Where ID=SPPID)NoSPP from (" +
                            "select *,case when ReceiptDate is not null and ReceiptDate > Sch then (Select dbo.GetWorkingDay(Sch,ReceiptDate)) else 0 end Late from " +
                            "(select ID,SPPID,ItemName,Quantity,QtyPO,Keterangan,LeadTime,AppDate,tglkirim," +
                            " (" + this.sch() + ") Sch,ReceiptDate,ReceiptNo,Status,PendingPO,PermintaanType," + this.StatSPP() + " Stat " +
                            "from( select SPPDetail.*," + this.ItemName() +
                            "(select ApproveDate3 from SPP where ID=SPPDetail.SPPID)AppDate,rc.ReceiptDate,rc.ReceiptNo, rc.PONo, rc.POID, " +
                            "pod.DlvDate  Deliv, (select PermintaanType From SPP where ID=SPPDetail.SPPID) PermintaanType, rcd.Quantity qtyreceipt  " +
                            "from SPPDetail  inner join POPurchnDetail pod on SPPDetail.id=pod.SppDetailID inner join ReceiptDetail rcd on " +
                            "pod.id=rcd.PODetailID  inner join  Receipt rc on rcd.ReceiptID =rc.id " + this.inventory +
                            "where " + this.Type + " rc.Status>-1 and rcd.RowStatus>-1 and SPPDetail.Status >-1  " +
                            this.ReadDataBaseOn() +
                            this.Criteria + "  and rc.ReceiptNo not in ('JA2103-00019','JA2103-00017','JA2103-00020','JA2103-00018')) as x ) as m ) as mm where mm.ReceiptDate between '" + this.FromDate + "' and '" + this.ToDate + "'" +
                            this.OnlyLate;
            query += ") as xd";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(query);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToDecimal(sdr["Jml"].ToString());
                }
            }
            return result;
        }
        private string ReadDataBaseOn()
        {
            string query = "";
            switch (this.BaseOn)
            {
                case "SPP":
                    query = "and SPPDetail.SPPID in(" +
                            "Select ID From SPP where Convert(char,Minta,112) between '" + this.FromDate + "' and '" + this.ToDate + "' and Approval >1 and Status >-1)";
                    break;
                case "RMS":
                    query = "and  SPPDetail.ID in " +
                            "(select SppDetailID from POPurchnDetail where POID in(Select distinct POID from Receipt rc where Convert(char,rc.ReceiptDate,112) between '" +
                            this.FromDate + "' and '" + this.ToDate + "' and rc.Status>-1))";
                    break;
            }
            return query;
        }
    }
    public class Late : SPP
    {
        public decimal Quantity { get; set; }
        public int LeadTime { get; set; }
        public DateTime DelivDate { get; set; }
        public string RMSNo { get; set; }
        public int Lambat { get; set; }
        public string PONo { get; set; }
        public string SupplierName { get; set; }

    }
}

