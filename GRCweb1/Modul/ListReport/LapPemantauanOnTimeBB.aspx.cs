using BusinessFacade;
using DataAccessLayer;
using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.ListReport
{
    public partial class LapPemantauanOnTimeBB : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(ExportXls);
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                txtDrTgl.Text = DateTime.Now.AddDays(-1).ToString("dd-MMM-yyyy");
                txtSdTgl.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                LoadBulan();
                LoadTahun();
            }
        }

        private void LoadTahun()
        {
            ddlTahun.Items.Clear();
            ArrayList arrData = new ArrayList();
            arrData = new Ontimebb().GetTahun();
            foreach (BB b in arrData)
            {
                ddlTahun.Items.Add(new ListItem(b.Tahun.ToString(), b.Tahun.ToString()));
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }

        private void LoadBulan()
        {
            ddlBulan.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i), i.ToString()));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
        }
        private decimal OK = 0;
        private decimal NotOK = 0;
        private int Sesuai = 0;
        private int Sesuai1 = 0;
        private int TSesuai1 = 0;
        private int TakSesuia = 0;
        private decimal nomor = 0;

        protected void preview_Click(object sender, EventArgs e)
        {
            OK = 0; NotOK = 0; Sesuai = 0; TakSesuia = 0; nomor = 0;
            ArrayList arrData = new ArrayList();
            Ontimebb onTime = new Ontimebb();
            onTime.Criteria = (onlyParDe.Checked == true) ? "true" : "false";
            onTime.StartDate = ddlTahun.SelectedValue.ToString() + ddlBulan.SelectedValue.ToString().PadLeft(2, '0') + "01";// DateTime.Parse(txtDrTgl.Text).ToString("yyyyMMdd");
            onTime.EndDate = ddlTahun.SelectedValue.ToString() + ddlBulan.SelectedValue.ToString().PadLeft(2, '0') + (DateTime.DaysInMonth(int.Parse(ddlTahun.SelectedValue), int.Parse(ddlBulan.SelectedValue))).ToString(); // DateTime.Parse(txtSdTgl.Text).ToString("yyyyMMdd");
            arrData = onTime.Retrieve(true);
            lstRepeater.DataSource = arrData;
            lstRepeater.DataBind();
        }

        protected void ExportToExcel(object sender, EventArgs e)
        {
            foreach (RepeaterItem rpt in lstRepeater.Items)
            {
                Image img = (Image)rpt.FindControl("edt");
                img.Visible = false;
            }
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=PemantauanOnTimeBB.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            Response.Write("Lembar Pemantauan<br>");
            Response.Write("On Time Pembelian Bahan Baku/ Penunjang/ Bakar dan Pembelian Sesuai Persyaratan Perusahaan<br>");
            string Html = "";
            Html += "<br>Periode : " + ddlBulan.SelectedItem.Text + "  " + ddlTahun.SelectedValue;
            Html += "";
            string HtmlEnd = "";
            lst.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0\"", "border=\"1\"");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }
        protected void lstRepeater_DataBound(object sender, RepeaterItemEventArgs e)
        {
            BB oBB = (BB)e.Item.DataItem;

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string x = ""; string xx = ""; string xxx = "";
                nomor = (oBB.RowStatus == 2) ? nomor : nomor + 1;
                decimal jadwal = (oBB.QtySCH);// *oBB.QtyMobil;
                decimal sel = (oBB.Quantity - jadwal);
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("trr");
                Label lOk = (Label)e.Item.FindControl("ok");
                Label lNo = (Label)e.Item.FindControl("no");
                Image img = (Image)e.Item.FindControl("edt");
                Label Sesuaix = (Label)e.Item.FindControl("Label1");
                Label tdkSesuai = (Label)e.Item.FindControl("Label2");
                tr.Cells[0].InnerHtml = nomor.ToString();
                //if (tr.Cells[0].InnerHtml.ToString() != "")
                //{
                //    Sesuai = (xx == "X") ? Sesuai + 1 : Sesuai;
                //    Sesuai1 = Sesuai;
                //}

                string[] arrDept = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("OnlyDeptID", "PO").Split(',');
                decimal selisih = (oBB.QtySCH > 0) ? Math.Round((oBB.Quantity - oBB.QtySCH) / oBB.QtySCH, 0, MidpointRounding.AwayFromZero) : 0;
                if (oBB.RowStatus == 1)
                {
                    lOk.Visible = (oBB.SchDates >= DateTime.Parse(oBB.TglReceipt)) ? true : false;
                    ///lNo.Visible = (oBB.ReceiptOK == 0) ? true : false;
                    lNo.Visible = (oBB.SchDates < DateTime.Parse(oBB.TglReceipt)) ? true : false;
                }
                //Sesuaix.Visible = (oBB.ReceiptOK > 0)? true : false;
                //tdkSesuai.Visible = ((oBB.ReceiptOK == 0 )) ? true : false;
                Sesuaix.Visible = (jadwal <= oBB.Quantity)/* ((tselisih * 100) >= -20)*/ ? true : false;
                tdkSesuai.Visible = (jadwal > oBB.Quantity)/*(((tselisih * 100) < -20)) */? true : false;
                img.Visible = (arrDept.Contains(((Users)Session["Users"]).DeptID.ToString())) ? true : false;
                //img.Visible = (oBB.SchDate.ToStrin=g() == (oBB.SchDeliv)) ? false : true;
                img.Attributes.Add("onclick", "openDialog()");
                if (oBB.RowStatus == 2)
                {


                    decimal tselisih = (jadwal > 0) ? Math.Round(sel / jadwal, 3, MidpointRounding.AwayFromZero) : 0;
                    tr.Attributes.Add("class", "Line3 bold");
                    tr.Cells[0].InnerText = "";
                    tr.Cells[1].InnerText = "";
                    tr.Cells[7].InnerText = (oBB.ItemID2 > 0) ? (jadwal * oBB.QtyMobil).ToString("N2") : jadwal.ToString("N2");
                    tr.Cells[7].Attributes.Add("title", " [ " + oBB.QtyMobil.ToString("N0") + " Mobil ]" + (jadwal).ToString("N2"));
                    tr.Cells[8].InnerText = "";
                    tr.Cells[6].InnerText = "";
                    tr.Cells[13].InnerText = "";
                    Sesuaix.Visible = (jadwal <= oBB.Quantity)/* ((tselisih * 100) >= -20)*/ ? true : false;
                    tdkSesuai.Visible = (jadwal > oBB.Quantity)/*(((tselisih * 100) < -20)) */? true : false;
                    lOk.Visible = false;
                    lNo.Visible = false;
                    Sesuaix.Attributes.Add("title", "Selisih Sch dangan Actual(%) =" + (tselisih * 100).ToString("N2"));
                    tdkSesuai.Attributes.Add("title", "Selisih Sch dangan Actual(%) =" + (tselisih * 100).ToString("N2"));
                    tr.Cells[10].Attributes.Add("title", "Selisih Sch dangan Actual(%) =" + (tselisih * 100).ToString("N2"));
                    tr.Cells[11].Attributes.Add("title", "Selisih Sch dangan Actual(%) =" + (tselisih * 100).ToString("N2"));
                    if (((tselisih * 100) >= -20)) { Sesuai += 1; }
                    if ((((tselisih * 100) < -20))) { TakSesuia += 1; }
                }
                if (oBB.RowStatus == 1)
                {
                    x = (lOk.Visible == true) ? "X" : "";
                    xx = (Sesuaix.Visible == true) ? "X" : "";
                    xxx = (tdkSesuai.Visible == true) ? "X" : "";
                    OK = (x == "X") ? OK + 1 : OK;
                    NotOK = (oBB.ReceiptOK > 0 && oBB.RowStatus == 0) ? NotOK + 1 : NotOK;
                    Sesuai1 = (xx.Trim() == "X") ? Sesuai1 + 1 : Sesuai1;
                    TSesuai1 = (xx.Trim() == "X") ? TSesuai1 + 1 : TSesuai1;
                    Sesuai = (xx.Trim() == "X" && oBB.RowStatus == 0) ? Sesuai + 1 : Sesuai;
                }

            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                HtmlTableRow trd = (HtmlTableRow)e.Item.FindControl("trfoot");
                HtmlTableRow trd2 = (HtmlTableRow)e.Item.FindControl("tr1");
                trd.Cells[1].InnerText = OK.ToString();// +"OK";
                trd.Cells[2].InnerText = (nomor - OK).ToString();// +"NotOK";
                trd.Cells[3].InnerText = (Sesuai1).ToString();// +"Sesuai";
                trd.Cells[4].InnerText = (nomor - TSesuai1).ToString();// +"Tdk";
                trd2.Cells[0].InnerHtml = (OK == 0) ? "" : "<b>Pencapaian : (" + OK.ToString("N0") + "/" + nomor.ToString("N0") + ") x 100% =" + (OK / (nomor)).ToString("P2") + "</b>";
            }

        }
        protected void lst_Repeater_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            int ID = int.Parse(e.CommandArgument.ToString());
            Label lbl = (Label)e.Item.FindControl("lblKet");
            ArrayList arrTrans = new ArrayList();
            switch (e.CommandName)
            {
                case "edit":
                    if (Session["Keterangan"] != null)
                    {
                        lbl.Text = Session["Keterangan"].ToString();
                        //update ke memhomahrian_trans
                        BB obe = new BB();
                        obe.ID = ID;
                        obe.Keterangan = Session["Keterangan"].ToString();
                        obe.CreatedBy = ((Users)Session["Users"]).UserID.ToString();
                        obe.CreatedTime = DateTime.Now;
                        Ontimebb otb = new Ontimebb();
                        otb.Criteria = "ID,Keterangan,CreatedBy,CreatedTime";
                        int result = otb.ProcessData(obe, "spMemoHarian_monitor_insert");
                    }
                    break;

            }
        }
    }
    public class Ontimebb
    {
        private ArrayList arrD = new ArrayList();
        private List<SqlParameter> sqlListParam;
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        private BB alk = new BB();
        public string Criteria { get; set; }
        private string Query()
        {
            #region OldQuery
            string query = "select rd.ID,rd.ReceiptID,rd.PODetailID,rd.POID,rd.PONo,rd.SPPNo,rd.ItemID, " +
                            "(Select ReceiptNo from Receipt where ID=rd.ReceiptID)ReceiptNo, " +
                            "(Select ReceiptDate from Receipt where ID=rd.ReceiptID)ReceiptDate, " +
                            "(select dbo.ItemCodeInv(rd.ItemID,rd.ItemTypeID))ItemCode, " +
                            "(select dbo.ItemNameInv(rd.ItemID,rd.ItemTypeID))ItemName,rd.Quantity,rd.Keterangan, " +
                            "isnull(mh.SchNo,'')MemoHarian,mh.DvlDate,isnull(mt.Qty,0)Qty, " +
                            "(Select SuppPurch.SupplierName from SuppPurch where ID=(Select SupplierID from POPurchn where ID=rd.POID))SupplierName, " +
                            "(select dbo.ItemNameInv((select ItemID from POPurchnDetail where ID=rd.PODetailID),ItemTypeID))ItemPO, " +
                            "(select DlvDate from POPurchnDetail where ID=rd.PODetailID)SchDeliver,mt.Keterangan as Ket,mt.CreatedBy " +
                            "from ReceiptDetail rd " +
                            " Left Join MemoHarian_PO mhu " +
                            " on mhu.POID=rd.POID and mhu.RowStatus >-1 " +
                            " Left Join Memoharian mh on mh.ID=mhu.SchID " +
                            " Left Join MemoHarian_trans mt " +
                            " on mt.RMSID=rd.ReceiptID " +
                            " where ReceiptID in " +
                            " (Select ID from Receipt where convert(char,ReceiptDate,112) between " +
                            "'" + this.StartDate + "' and '" + this.EndDate + "' and Receipt.Status >-1) and rd.GroupID in(1,2) and rd.ItemTypeID=1 and rd.RowStatus>-1 " +
                            "and rd.ItemID in(Select ID from Inventory where ShortKey in('G','L','M'))  " +
                            "order by SupplierName,ItemName,ID desc";
            #endregion
            #region query lama lagi
            string query2 = "select mt.ID,mt.CreateTime as ReceiptDate,mt.SchID,Case when mt.SchNo='--pilih--' then '' " +
                            "else mt.SchNo end  MemoHarian,mt.RMSID,mt.ItemID, " +
                            " (select dbo.ItemNameInv(mt.ItemID,1))ItemName,isnull(mt.Qty,0)Qty, " +
                          "  CASE WHEN LEFT(mt.SchNo,2)='PK' THEN " +
                          "      ISNULL(CONVERT(CHAR,(Select DvlDate From MemoHarian Where Memoharian.ID= " +
                          " (SELECT Top 1 SchID FROM MemoHarian_PO where MemoHarian_PO.ID=mt.SchID)),103),'') ELSE  " +
                          "      CONVERT(char,(ma.SchDate),103) " +
                          "  END SchDeliver, " +
                          "  isnull(rd.QtyTimbang,0)QtyTimbang,isnull(rd.Quantity,0)Quantity," +
                          "  CASE WHEN iv.ShortKey in('G','L','M') THEN CASE WHEN mp.EstQty>0 THEN (mp.EstQty/mp.QtyMobil)ELSE 0 END ELSE mt.Qty END EstQty," +
                          " (select dbo.ItemCodeInv(mt.ItemID,1))ItemPO, " +
                          "  (select ReceiptNo from Receipt where status>-1 and ID=rd.ReceiptID)ReceiptNo, " +
                          "(select Convert(char,ReceiptDate,103) from Receipt where status>-1 and ID=rd.ReceiptID) SchDeliv, " +
                          "  rd.PONo,rd.SPPNo,(select SuppPurch.SupplierName from SuppPurch where ID= " +
                          " (select SupplierID from POPurchn where ID=rd.POID))SupplierName,rd.Keterangan as Ket " +
                          "  from MemoHarian_trans  mt " +
                          "  LEFT JOIN MemoHarian_Armada as ma on ma.ID=mt.SchID " +
                          "  LEFT JOIN MemoHarian_PO as mp on mp.ID=ma.SchID /*and mp.ItemID=mt.ItemID*/ " +
                          "  LEFT JOIN MemoHarian as mh on mh.ID=mp.SchID /*and mh.ItemID=mp.ItemID*/ " +
                          "  LEFT JOIN ReceiptDetail as rd on rd.ReceiptID=mt.RMSID and rd.ItemID=mt.ItemID and rd.RowStatus>-1 " +
                          "  LEFT JOIN Inventory as iv ON iv.ID=mt.ItemID " +
                          "  where CONVERT(char,CreateTime,112) between '" + this.StartDate + "' and '" + this.EndDate + "'" +
                          "  and mt.ItemID in (Select ID from Inventory " + this.Criteria + ") and rd.rowstatus>-1 " +
                          "  order by RMSID ";
            #endregion
            #region Query Baru
            //Perubahan Agus
            query2 = "exec dbo.ParsialDeliveryMonitoring '" + StartDate + "','" + EndDate + "'";
            query2 = "EXEC('IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].[ParDe]'') AND type in (N''U'')) " +
                     "DROP TABLE [dbo].[ParDe]') " +
                    "CREATE TABLE ParDe  " +
                    "( " +
                    "    ID int,SchID INT, ReceiptDate varchar(10),SupplierName varchar(100),ItemName Varchar(100),ItemID int,SPPno Varchar(20),PONo Varchar(20), " +
                    "    SchDeliver datetime,EstimasiQty decimal(18,2),QtySch decimal(18,2), ReceiptNo varchar(20), Qty decimal(18,2), TotalDatang int, " +
                    "    ReceiptOK int,JenisBiaya int,Keterangan Varchar(max),Urutan int " +
                    ") ";
            if (this.Criteria == "false")
            {
                query2 += "INSERT INTO ParDe exec dbo.ParsialDelivery " + StartDate.Substring(4, 2) + "," + StartDate.Substring(0, 4) + ",'false'; " +
                    "INSERT INTO ParDe exec dbo.ParsialDeliveryMonitoring '" + StartDate + "','" + EndDate + "' ";
            }
            else
            {
                query2 += "INSERT INTO ParDe exec dbo.ParsialItem '" + StartDate + "','" + EndDate + "' ";
            }
            query2 += "SELECT * FROM ParDe where ItemName!='PVA FIBER' and ItemName!='VIRGIN PULP NUKP' and ItemName!='UNCOATED KRAFT PAPER' ORDER By SchDeliver,Itemname,SupplierName,Urutan " +
                    "EXEC('IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].[ParDe]'') AND type in (N''U'')) " +
                    "DROP TABLE [dbo].[ParDe]');";
            return query2;
            //Perubahan Agus
            #endregion
        }
        public ArrayList Retrieve()
        {
            ArrayList arrData = new ArrayList();
            try
            {
                DataAccess dta = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = dta.RetrieveDataByString(this.Query());
                if (dta.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        if (sdr["ReceiptNo"].ToString() != "")
                        {
                            arrData.Add(new BB
                            {
                                ID = Convert.ToInt32(sdr["ID"].ToString()),
                                NoPO = sdr["PONo"].ToString(),
                                SppNo = sdr["SppNo"].ToString(),
                                DOSupplier = sdr["SupplierName"].ToString(),
                                ItemName = sdr["ItemName"].ToString(),
                                ItemCode = sdr["ItemPO"].ToString(),
                                ReceiptDate = Convert.ToDateTime(sdr["ReceiptDate"].ToString()),
                                ScheduleNo = sdr["MemoHarian"].ToString(),
                                Quantity = Convert.ToDecimal(sdr["Quantity"].ToString()),
                                QtyTimbang = Convert.ToDecimal(sdr["EstQty"].ToString()),
                                SchDate = sdr["SchDeliver"].ToString(),
                                ReceiptNo = sdr["ReceiptNo"].ToString(),
                                Keterangan = sdr["Ket"].ToString(),
                                SchDeliv = sdr["SchDeliv"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex) { arrData.Add(new BB { DOSupplier = ex.Message + "-" + ex.StackTrace.ToString() }); }
            return arrData;
        }
        public ArrayList Retrieve(bool baru)
        {
            arrD = new ArrayList();
            string strsql = this.Query();
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strsql);

            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrD.Add(GenerateObject(sdr));
                }
            }
            return arrD;
        }

        private BB GenerateObject(SqlDataReader sdr)
        {
            alk = new BB();
            string[] rcp = sdr["ReceiptDate"].ToString().Split('.');
            alk.TglReceipt = (rcp.Count() > 1) ? rcp[2].ToString().TrimEnd() + "/" + rcp[1] + "/" + rcp[0] : "";
            alk.ItemName = sdr["ItemName"].ToString();
            alk.SupplierName = sdr["SupplierName"].ToString();
            alk.NoPO = sdr["PoNo"].ToString();
            alk.NoSPP = sdr["SPPNo"].ToString();
            alk.QtySCH = decimal.Parse(sdr["EstimasiQty"].ToString());
            alk.SchDates = DateTime.Parse(sdr["SchDeliver"].ToString());
            alk.ReceiptNo = sdr["ReceiptNo"].ToString();
            alk.Quantity = decimal.Parse(sdr["Qty"].ToString());
            alk.QtyMobil = decimal.Parse(sdr["QtySch"].ToString());
            alk.QtyMobilDatang = int.Parse(sdr["TotalDatang"].ToString());
            alk.ReceiptOK = int.Parse(sdr["ReceiptOK"].ToString());
            alk.ItemID = int.Parse(sdr["ItemID"].ToString());
            alk.ItemID2 = int.Parse(sdr["JenisBiaya"].ToString());
            alk.RowStatus = int.Parse(sdr["Urutan"].ToString());
            alk.ID = int.Parse(sdr["ID"].ToString());
            alk.Keterangan = sdr["Keterangan"].ToString();
            return alk;
        }
        public int ProcessData(object arrAL, string spName)
        {
            try
            {
                alk = (BB)arrAL;
                string[] arrCriteria = this.Criteria.Split(',');
                PropertyInfo[] data = alk.GetType().GetProperties();
                DataAccess da = new DataAccess(Global.ConnectionString());
                var equ = new List<string>();
                sqlListParam = new List<SqlParameter>();
                foreach (PropertyInfo items in data)
                {
                    if (items.GetValue(alk, null) != null && arrCriteria.Contains(items.Name))
                    {
                        sqlListParam.Add(new SqlParameter("@" + items.Name.ToString(), items.GetValue(alk, null)));
                    }
                }
                int result = da.ProcessData(sqlListParam, spName);
                string err = da.Error;
                return result;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return -1;
            }
        }
        public ArrayList GetTahun()
        {
            arrD = new ArrayList();
            DataAccess da = new DataAccess(Global.ConnectionString());
            string strSQL = "SELECT DISTINCT Year(DvlDate)Tahun FROM MemoHarian where isnull(Year(DvlDate),0) >0 and rowstatus>-1 ORDER By YEAR(DvlDate)";
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrD.Add(new BB { Tahun = int.Parse(sdr["Tahun"].ToString()) });
                }
            }
            return arrD;
        }
    }
    public class BB : ReceiptDetail
    {
        public DateTime ReceiptDate { get; set; }
        public string TglReceipt { get; set; }
        public string ReceiptNo { get; set; }
        public string SchDeliv { get; set; }
        public string SchDate { get; set; }
        public int RMSDetailID { get; set; }
        public string CreatedBy { get; set; }
        public int Tahun { get; set; }
        public int Bulan { get; set; }
        public int ReceiptOK { get; set; }
        public string SupplierName { get; set; }
        public string NoSPP { get; set; }
        public decimal QtySCH { get; set; }
        public decimal QtyMobil { get; set; }
        public int QtyMobilDatang { get; set; }
        public DateTime SchDates { get; set; }
    }

}