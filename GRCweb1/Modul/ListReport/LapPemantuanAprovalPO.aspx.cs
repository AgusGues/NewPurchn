using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.IO;
using Domain;
using BusinessFacade;
using DataAccessLayer;

namespace GRCweb1.Modul.ListReport
{
    public partial class LapPemantuanAprovalPO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadBulan();
                LoadTahun();
                LoadPurchnGroup();
            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }

        private void LoadBulan()
        {
            ddlBulan.Items.Clear();
            for (int i = 0; i <= 12; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i).ToString(), i.ToString().PadLeft(2, '0')));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString().PadLeft(2, '0');
        }
        private void LoadTahun()
        {
            ListApproval la = new ListApproval();
            la.Pilih = "Tahun";
            ddlTahuan.Items.Clear();
            foreach (POPurchn po in la.Retrieve())
            {
                ddlTahuan.Items.Add(new ListItem(po.Tahun.ToString(), po.Tahun.ToString()));
            }
            ddlTahuan.SelectedValue = DateTime.Now.Year.ToString();
        }
        private bool TampilkanDetail { get; set; }
        private decimal ttSPPe = 0;
        private decimal ttOKe = 0;
        private decimal ttNoOKe = 0;
        private decimal ttApOKe = 0;
        private decimal ttApNoOKe = 0;
        //protected void lstPO_DataBound(object sender, RepeaterItemEventArgs e)
        //{

        //}
        protected void lstNew_DataBound(object sender, RepeaterItemEventArgs e)
        {
            POPurchn p = (POPurchn)e.Item.DataItem;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ttSPPe = e.Item.ItemIndex + 1;
                ttOKe += (p.UOMCode == "X") ? 1 : 0;
                ttNoOKe += (p.AlasanClose == "X") ? 1 : 0;
                ttApOKe += (p.UP == "X") ? 1 : 0;
                ttApNoOKe += (p.NoPol == "X") ? 1 : 0;

            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label ttSPP = (Label)e.Item.FindControl("ttSPP");
                ttSPP.Text = ttSPPe.ToString("###.#");
                Label ttOK = (Label)e.Item.FindControl("ttOK");
                //tampilkan prosen
                ttOK.Text = (ttOKe == 0) ? "0" : (ttOKe / ttSPPe * 100).ToString("###.#");
                ((Label)e.Item.FindControl("ttNoOK")).Text = (ttNoOKe > 0) ? (ttNoOKe / ttSPPe * 100).ToString("###.#") : "";
                ((Label)e.Item.FindControl("ttApOK")).Text = (ttApOKe > 0) ? (ttApOKe / ttSPPe * 100).ToString("###.#") : "";
                ((Label)e.Item.FindControl("ttApNoOK")).Text = (ttApNoOKe > 0) ? (ttApNoOKe / ttSPPe * 100).ToString("###.#") : "";
                //tampilkan jumlah
                ((Label)e.Item.FindControl("ttOKP")).Text = (ttOKe.ToString("###.#"));
                ((Label)e.Item.FindControl("ttNoOKP")).Text = (ttNoOKe.ToString("###.#"));
                ((Label)e.Item.FindControl("ttApOKP")).Text = (ttApOKe.ToString("###.#"));
                ((Label)e.Item.FindControl("ttApNoOKP")).Text = (ttApNoOKe.ToString("###.#"));

            }
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            this.TampilkanDetail = false;
            LoadData();
        }
        protected void btnPreviewByTglPO_Click(object sender, EventArgs e)
        {
            this.TampilkanDetail = false;
            LoadDataByTglPO();
        }
        private void LoadData()
        {
            ArrayList arrData = new ArrayList();
            ListApproval la = new ListApproval();
            string txtGroupID = string.Empty;
            for (int i = 0; i < ddlGroupID.Items.Count; i++)
            {
                if (ddlGroupID.Items[i].Selected == true)
                {
                    txtGroupID += ddlGroupID.Items[i].Value + ",";
                }
            }
            string LamaSPPKePO = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("LeadTimeSPPtoPO", "PO");
            txtGroupID = (txtGroupID != string.Empty) ? txtGroupID.Substring(0, txtGroupID.Length - 1) : "";
            la.Pilih = "Detail";
            la.Criteria = " and Year(PO0.POPurchnDate)=" + ddlTahuan.SelectedValue.ToString();
            la.Criteria += " and Month(PO0.POPurchnDate)=" + ddlBulan.SelectedValue.ToString();
            la.Prefix = "distinct ";
            la.Field = ",SPP.NoSPP,CONVERT(CHAR,SPP.ApproveDate3,103)AS ApvMgr,";
            la.Field += "Case when (DATEDIFF(DAY,SPP.ApproveDate3,x.CreatedTime)-(select dbo.GetOFFDay(SPP.ApproveDate3,x.CreatedTime)))>" +
                LamaSPPKePO + " Then 'NO' else 'OK' end OnPO ";
            //la.Additional = " LEFT JOIN POPurchnDetail as PO on PO.POID=xx.ID and PO.Status>-1 ";
            //la.Additional += "LEFT JOIN SPP on SPP.ID=PO.SPPID  ";
            //la.Additional += "LEFT JOIN SPPDetail on SPPDetail.ID=PO.SPPDetailID ";
            la.Additional = " WHERE GroupID in(" + txtGroupID + ")";
            la.inventory = (rbtStock.Checked || rbtNonStock.Checked) ? "left join Inventory i on i.ID=sppd.ItemID " : string.Empty;
            la.Type = (rbtStock.Checked) ? "i.Stock=1 and " : (rbtNonStock.Checked) ? "i.Stock=0 and " : string.Empty;
            arrData = la.Retrieve();
            //lstPO.DataSource = arrData;
            //lstPO.DataBind();
            lstNew.DataSource = arrData;
            lstNew.DataBind();
        }
        private void LoadDataByTglPO()
        {
            ArrayList arrData = new ArrayList();
            ListApproval ll = new ListApproval();
            string txtGroupID = string.Empty;
            for (int i = 0; i < ddlGroupID.Items.Count; i++)
            {
                if (ddlGroupID.Items[i].Selected == true)
                {
                    txtGroupID += ddlGroupID.Items[i].Value + ",";
                }
            }
            string LamaSPPKePO = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("LeadTimeSPPtoPO", "PO");
            txtGroupID = (txtGroupID != string.Empty) ? txtGroupID.Substring(0, txtGroupID.Length - 1) : "";
            ll.Pilih = "DetailByTglPO";
            ll.Criteria = " and Year(PO0.POPurchnDate)=" + ddlTahuan.SelectedValue.ToString();
            ll.Criteria += " and Month(PO0.POPurchnDate)=" + ddlBulan.SelectedValue.ToString();
            ll.Prefix = "distinct ";
            ll.Field = ",SPP.NoSPP,CONVERT(CHAR,SPP.ApproveDate3,103)AS ApvMgr,";
            ll.Field += "Case when (DATEDIFF(DAY,SPP.ApproveDate3,x.POPurchnDate)-(select dbo.GetOFFDay(SPP.ApproveDate3,x.POPurchnDate)))>" +
                LamaSPPKePO + " Then 'NO' else 'OK' end OnPO ";
            //ll.Additional = " LEFT JOIN POPurchnDetail as PO on PO.POID=xx.ID and PO.Status>-1 ";
            //ll.Additional += "LEFT JOIN SPP on SPP.ID=PO.SPPID  ";
            //ll.Additional += "LEFT JOIN SPPDetail on SPPDetail.ID=PO.SPPDetailID ";
            ll.Additional = " WHERE GroupID in(" + txtGroupID + ")";
            ll.inventory = "left join Inventory i on i.ID=sppd.ItemID ";
            ll.Type = (rbtStock.Checked) ? "i.Stock=1 and " : (rbtNonStock.Checked) ? "i.Stock=0 and " : string.Empty;
            arrData = ll.Retrieve();
            //lstPO.DataSource = arrData;
            //lstPO.DataBind();
            lstNew.DataSource = arrData;
            lstNew.DataBind();
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            this.TampilkanDetail = false;// (chkDetail.Checked);
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            string txtGroupID = string.Empty;
            for (int i = 0; i < ddlGroupID.Items.Count; i++)
            {
                if (ddlGroupID.Items[i].Selected == true)
                {
                    txtGroupID += ddlGroupID.Items[i].Text + ",";
                }
            }
            //LoadData();
            LoadDataByTglPO();
            Response.AddHeader("content-disposition", "attachment;filename=PemantauanApprovalPO.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<b>PEMANTAUAN APPROVAL PO</b>";
            Html += "<br>Periode : " + ddlBulan.SelectedItem.Text + "  " + ddlTahuan.SelectedValue.ToString();
            Html += (txtGroupID != string.Empty) ? "<br>Material Group : " + txtGroupID.Substring(0, txtGroupID.Length - 1) : "";
            string HtmlEnd = "";
            //lstForPrint.RenderControl(hw);
            lstNewP.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("xx\">", "\">\'");
            Contents = Contents.Replace("border=\"0", "border=\"1");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }
        private void LoadPurchnGroup()
        {
            try
            {
                ArrayList arrSPP = new ArrayList();
                GroupsPurchnFacade grps = new GroupsPurchnFacade();
                grps.without = " and A.ID !=10";
                arrSPP = grps.Retrieve();
                ddlGroupID.DataSource = arrSPP;
                ddlGroupID.DataTextField = "GroupDescription";
                ddlGroupID.DataValueField = "ID";
                ddlGroupID.AutoPostBack = false;
                ddlGroupID.DataBind();
            }
            catch
            {
                Global.link = "~/Default.aspx";
            }
        }
        protected void ddlGroup_DataBound(object sender, EventArgs e)
        {
            POPurchn p = new POPurchn();
            ListApproval ls = new ListApproval();
            ls.Criteria = " and UserID=" + ((Users)Session["Users"]).ID.ToString();
            ls.Pilih = "UserAcount";
            p = ls.Retrieve(true);
            string[] uid = p.AlasanBatal.Split(',');

            for (int i = 0; i <= ddlGroupID.Items.Count; i++)
            {
                ddlGroupID.Items[i].Selected = (uid.Contains((i + 1).ToString())) ? true : false;
            }

        }
        public ArrayList GetItemPO(string NoPO)
        {
            ArrayList arrData = new ArrayList();
            POPurchnDetailFacade pod = new POPurchnDetailFacade();
            arrData = pod.RetrieveByNoSPP(NoPO);
            return arrData;
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetItemByNoPO(string NoPO)
        {
            string result = string.Empty;
            ArrayList arrData = new ArrayList();
            LapPemantuanAprovalPO lpo = new LapPemantuanAprovalPO();
            arrData = lpo.GetItemPO(NoPO);
            int i = 0;
            foreach (POPurchnDetail po in arrData)
            {
                i++;
                string cls = ((i % 2) == 0) ? " EvenRows" : "OddRows";
                result += "<tr class='" + cls + " baris'>";
                result += "<td class='kotak tengah'>" + i + "</td>";
                result += "<td class='kotak tengah'>" + po.ItemCode.ToString() + "</td>";
                result += "<td class='kotak '>" + po.ItemName.ToString() + "</td>";
                result += "<td class='kotak angka'>" + po.Qty.ToString("###,###.#0") + "</td>";
                result += "<td class='kotak tengah'>" + po.DlvDate.ToString("dd-MMM-yyyy") + "</td>";
                result += "<td class='kotak '>" + po.NamaBarang.ToString() + "</td>";
                result += "</tr>";
            }
            return result;
        }
    }

    public class ListApproval
    {
        ArrayList arrData = new ArrayList();
        POPurchn objP = new POPurchn();
        public string Criteria { get; set; }
        public string Field { get; set; }
        public string Prefix { get; set; }
        public string Additional { get; set; }
        public string Type { get; set; }
        public string inventory { get; set; }
        public string Pilih { get; set; }
        public ArrayList Retrieve()
        {
            arrData = new ArrayList();
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(this.Query());
            string strsql = this.Query();
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(gObject(sdr));
                }
            }
            return arrData;
        }
        public POPurchn Retrieve(bool detail)
        {
            objP = new POPurchn();
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(this.Query());
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    return gObject(sdr);
                }
            }
            return objP;
        }
        private string Query()
        {
            string LamaSPPKePO = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("LeadTimeSPPtoPO", "PO");
            string query = string.Empty;
            switch (this.Pilih)
            {
                case "Tahun":
                    query = "select * from (select distinct YEAR(POPurchnDate)Tahun from POPurchn) as x where x.Tahun is not null " +
                          this.Criteria + "order by x.Tahun desc";
                    break;
                case "Detailxx"://tidak dipakai lagi
                    query = "set DATEFIRST 1; Select " + this.Prefix + " x.*,Case When (Selisih-Libur)>2 Then 'No' else 'OK' End OnTime " + this.Field +
                            "from ( " +
                          "select ID,NoPO,Convert(Char,CreatedTime,103)TglPO,Convert(Char,CreatedTime,108)Jam, " +
                          "(Select Convert(char,AppDate,103)from EventApprovalLog where DocNo=POPurchn.NoPO and AppLevel=1 and DocType='PO')AppPO," +
                          "(Select Convert(Char,AppDate,108)from EventApprovalLog where DocNo=POPurchn.NoPO and AppLevel=1 and DocType='PO')JamApv, " +
                          "(SELECT Convert(char,AppDate,103)FROM EventApprovalLog where DocNo=POPurchn.NoPO and AppLevel=2 and DocType='PO')AppPOM," +
                          "(SELECT Convert(Char,AppDate,108)FROM EventApprovalLog where DocNo=POPurchn.NoPO and AppLevel=2 and DocType='PO')JamApvM, " +
                          "(Select AppDate from EventApprovalLog where DocNo=POPurchn.NoPO and AppLevel=1)ApproveDate1," +
                          "(Select AppDate from EventApprovalLog where DocNo=POPurchn.NoPO and AppLevel=2)ApproveDate2, " +
                          "CreatedTime,DATEDIFF(DAY,CreatedTime,(Select AppDate from EventApprovalLog where DocNo=POPurchn.NoPO and AppLevel=1))Selisih, " +
                          "(select dbo.GetOFFDay(CreatedTime,(Select AppDate from EventApprovalLog where DocNo=POPurchn.NoPO and AppLevel=1)))Libur" +
                          " from POPurchn   " +
                          "where Status>-1 " + this.Criteria +
                          ")as x " + this.Additional + " order by nopo";
                    break;
                case "Detail":
                    #region salah1
                    //query = "set DATEFIRST 1; "+
                    //        "Select distinct  xx.*, (Select dbo.ItemCodeInv(po.ItemID,Po.ItemTypeID))ItemCode, "+
                    //        "Case When Po.ItemTypeID=3 Then Case When SPPDetail.TypeBiaya='BUAT' THEN "+
                    //        "'BUAT '+SPPDetail.Keterangan ELSE SPPDetail.Keterangan END ELSE "+
                    //        "(Select dbo.ItemNameInv(po.ItemID,Po.ItemTypeID))END ItemName, "+
                    //        "Case When (Selisih-Libur)>2 Then 'No' else 'OK' End OnTime ,SPP.NoSPP, "+
                    //        "CONVERT(CHAR,xx.ApproveDate3,103)AS ApvMgr, "+
                    //        "Case when (DATEDIFF(DAY,isnull(xx.ApproveDate3,'1/1/1900'),isnull(xx.CreatedTime,'1/1/1900'))- (select dbo.GetOFFDay(isnull(xx.ApproveDate3,'1/1/1900'),isnull(xx.CreatedTime,'1/1/1900'))))>" + LamaSPPKePO +
                    //        " Then 'NO' else 'OK' end OnPO  "+
                    //        "FROM ( "+
                    //        "SELECT *,CAST(Convert(datetime,AppPO) +JamApv as datetime)ApproveDate1, "+
                    //        "CAST(AppPOM +JamApvM as datetime)ApproveDate2, "+
                    //        "(Select Top 1 AppDate From EventApprovalLog WHERE DocType='SPP' AND AppLevel=3 and DocNo=( "+
                    //        " Select top 1 DocumentNo From POPurchnDetail where POID=x.ID) order by id desc) ApproveDate3,"+
                    //        "DATEDIFF(DAY,x.CreatedTime, "+
                    //        "CAST(Convert(datetime,AppPO) +JamApv as datetime))Selisih,Case when AppPO is not null then  "+
                    //        "(select dbo.GetOFFDay(x.CreatedTime,CAST(convert(datetime,AppPO)+JamApv as datetime))) else 0 end Libur "+ 
                    //        "FROM (  "+

                    //         "FROM ( " +
                    //        "SELECT *,CAST(Convert(datetime,AppPO) +JamApv as datetime)ApproveDate1, " +
                    //        "CAST(AppPOM +JamApvM as datetime)ApproveDate2, " +
                    //        "(Select Top 1 AppDate From EventApprovalLog WHERE DocType='SPP' AND AppLevel=3 and DocNo=( " +
                    //        " Select top 1 DocumentNo From POPurchnDetail where POID=x.ID) order by id desc) ApproveDate3," +
                    //        "DATEDIFF(DAY,x.POPurchnDate, " +
                    //        "CAST(Convert(datetime,AppPO) +JamApv as datetime))Selisih,Case when AppPO is not null then  " +
                    //        "(select dbo.GetOFFDay(x.POPurchnDate,CAST(convert(datetime,AppPO)+JamApv as datetime))) else 0 end Libur " +
                    //        "FROM (  " +

                    //        "select ID,NoPO,Convert(Char,CreatedTime,103)TglPO,Convert(Char,CreatedTime,108)Jam,  "+
                    //        "(Select TOP 1 Convert(char,AppDate,112)from EventApprovalLog where DocNo=POPurchn.NoPO and AppLevel=1 and DocType='PO' ORDER BY EventApprovalLog.ID Desc )AppPO, " +
                    //        "(Select TOP 1 Convert(Char,AppDate,108)from EventApprovalLog where DocNo=POPurchn.NoPO and AppLevel=1 and DocType='PO' ORDER BY EventApprovalLog.ID Desc )JamApv,  " +
                    //        "(SELECT TOP 1 Convert(char,AppDate,112)FROM EventApprovalLog where DocNo=POPurchn.NoPO and AppLevel=2 and DocType='PO' ORDER BY EventApprovalLog.ID Desc )AppPOM, " +
                    //        "(SELECT TOP 1 Convert(Char,AppDate,108)FROM EventApprovalLog where DocNo=POPurchn.NoPO and AppLevel=2 and DocType='PO' ORDER BY EventApprovalLog.ID Desc )JamApvM,  " +
                    //        "CreatedTime from POPurchn   where Status>-1 " + this.Criteria +
                    //        " )as x "+
                    //        " ) as xx " +
                    //         this.Additional + " order by nopo";
                    #endregion salah1
                    query = "set DATEFIRST 1; Select distinct  xx.*, " +
                        "(Select dbo.ItemCodeInv(ItemID,ItemTypeID))ItemCode,  " +
                        "Case When ItemTypeID=3 Then Case When TypeBiaya='BUAT' THEN 'BUAT '+Keterangan ELSE Keterangan END  " +
                        "ELSE (Select dbo.ItemNameInv(ItemID,ItemTypeID))END ItemName, Case When (Selisih-Libur)>2 Then 'No' else 'OK' End OnTime ,NoSPP, " +
                        " CONVERT(CHAR,xx.ApproveDate3,103)AS ApvMgr " +
                        " ,Case when((DATEDIFF(DAY,isnull(xx.ApproveDate3,'1/1/1900'),isnull(xx.CreatedTime,'1/1/1900')) -  " +
                        " (case when xx.ApproveDate3<xx.CreatedTime then (select dbo.GetOFFDay(isnull(xx.ApproveDate3,'1/1/1900'),  " +
                        " isnull(xx.CreatedTime,'1/1/1900'))) else 0 end)))>" + LamaSPPKePO + " Then 'NO' else 'OK' end  OnPO   " +
                        " FROM (  " +
                        " SELECT *, " +
                        " CAST(Convert(datetime,AppPO) +JamApv as datetime)ApproveDate1,  " +
                        " CAST(AppPOM +JamApvM as datetime)ApproveDate2, " +
                        "  (Select Top 1 AppDate From EventApprovalLog WHERE DocType='SPP' AND AppLevel=3 and DocNo=NoSPP) ApproveDate3, " +
                        "  DATEDIFF(DAY,x.CreatedTime, CAST(Convert(datetime,AppPO) +JamApv as datetime))Selisih, " +
                        "  Case when AppPO is not null then  (select dbo.GetOFFDay(x.CreatedTime,CAST(convert(datetime,AppPO)+JamApv as datetime))) else 0 end  " +
                        "  Libur FROM (   " +
                        "  select PO0.ID,PO0.NoPO,Convert(Char,PO0.CreatedTime,103)TglPO,Convert(Char,PO0.CreatedTime,108)Jam,   " +
                        "  (Select TOP 1 Convert(char,AppDate,112)from EventApprovalLog where DocNo=PO0.NoPO and AppLevel=1 and DocType='PO' ORDER BY EventApprovalLog.ID Desc )AppPO,  " +
                        "  (Select TOP 1 Convert(Char,AppDate,108)from EventApprovalLog where DocNo=PO0.NoPO and AppLevel=1 and DocType='PO' ORDER BY EventApprovalLog.ID Desc )JamApv,   " +
                        "  (SELECT TOP 1 Convert(char,AppDate,112)FROM EventApprovalLog where DocNo=PO0.NoPO and AppLevel=2 and DocType='PO' ORDER BY EventApprovalLog.ID Desc )AppPOM,  " +
                        "  (SELECT TOP 1 Convert(Char,AppDate,108)FROM EventApprovalLog where DocNo=PO0.NoPO and AppLevel=2 and DocType='PO' ORDER BY EventApprovalLog.ID Desc )JamApvM,   " +
                        "  PO0.CreatedTime, PO.GroupID,PO.ItemID,PO.ItemTypeID,sppd.TypeBiaya,sppd.Keterangan,SPP.NoSPP from POPurchn PO0  LEFT JOIN POPurchnDetail as PO on PO.POID=PO0.ID and PO.Status>-1  " +
                        "   LEFT JOIN SPP on SPP.ID=PO.SPPID  LEFT JOIN SPPDetail sppd on sppd.ID=PO.SPPDetailID " + this.inventory +
                        "   where " + this.Type + " PO0.Status>-1 " + this.Criteria +
                            " )as x  " +
                        "   ) as xx   " +
                             this.Additional + " order by NOPO";
                    break;
                case "DetailByTglPO":
                    #region salah2
                    //query = "set DATEFIRST 1; " +
                    //        "Select distinct  xx.*, (Select dbo.ItemCodeInv(po.ItemID,Po.ItemTypeID))ItemCode, " +
                    //        "Case When Po.ItemTypeID=3 Then Case When SPPDetail.TypeBiaya='BUAT' THEN " +
                    //        "'BUAT '+SPPDetail.Keterangan ELSE SPPDetail.Keterangan END ELSE " +
                    //        "(Select dbo.ItemNameInv(po.ItemID,Po.ItemTypeID))END ItemName, " +
                    //        "Case When (Selisih-Libur)>2 Then 'No' else 'OK' End OnTime ,SPP.NoSPP, " +
                    //        "CONVERT(CHAR,xx.ApproveDate3,103)AS ApvMgr, " +
                    //        "Case when (DATEDIFF(DAY,isnull(xx.ApproveDate3,'1/1/1900'),isnull(xx.POPurchnDate,'1/1/1900'))- "+
                    //        "(select dbo.GetOFFDay(isnull(xx.ApproveDate3,'1/1/1900'),isnull(xx.POPurchnDate,'1/1/1900'))))>" + LamaSPPKePO +
                    //        " Then 'NO' else 'OK' end OnPO  " +
                    //        "FROM ( " +
                    //        "SELECT *,CAST(Convert(datetime,AppPO) +JamApv as datetime)ApproveDate1, " +
                    //        "CAST(AppPOM +JamApvM as datetime)ApproveDate2, " +
                    //        "(Select Top 1 AppDate From EventApprovalLog WHERE DocType='SPP' AND AppLevel=3 and DocNo=( " +
                    //        " Select top 1 DocumentNo From POPurchnDetail where POID=x.ID) order by id desc) ApproveDate3," +
                    //        "DATEDIFF(DAY,x.POPurchnDate, " +
                    //        "CAST(Convert(datetime,AppPO) +JamApv as datetime))Selisih,Case when AppPO is not null then  " +
                    //        "(select dbo.GetOFFDay(x.POPurchnDate,CAST(convert(datetime,AppPO)+JamApv as datetime))) else 0 end Libur " +
                    //        "FROM (  " +
                    //        "select ID,NoPO,Convert(Char,POPurchnDate,103)TglPO,Convert(Char,POPurchnDate,108)Jam,  " +
                    //        "(Select TOP 1 Convert(char,AppDate,112)from EventApprovalLog where DocNo=POPurchn.NoPO and AppLevel=1 and DocType='PO' ORDER BY EventApprovalLog.ID Desc )AppPO, " +
                    //        "(Select TOP 1 Convert(Char,AppDate,108)from EventApprovalLog where DocNo=POPurchn.NoPO and AppLevel=1 and DocType='PO' ORDER BY EventApprovalLog.ID Desc )JamApv,  " +
                    //        "(SELECT TOP 1 Convert(char,AppDate,112)FROM EventApprovalLog where DocNo=POPurchn.NoPO and AppLevel=2 and DocType='PO' ORDER BY EventApprovalLog.ID Desc )AppPOM, " +
                    //        "(SELECT TOP 1 Convert(Char,AppDate,108)FROM EventApprovalLog where DocNo=POPurchn.NoPO and AppLevel=2 and DocType='PO' ORDER BY EventApprovalLog.ID Desc )JamApvM,  " +
                    //        "POPurchnDate from POPurchn   where Status>-1 " + this.Criteria +
                    //        " )as x " +
                    //        " ) as xx " +
                    //         this.Additional + " order by nopo";
                    #endregion salah2
                    query = "set DATEFIRST 1; Select distinct  xx.*, " +
                        "(Select dbo.ItemCodeInv(ItemID,ItemTypeID))ItemCode,  " +
                        "Case When ItemTypeID=3 Then Case When TypeBiaya='BUAT' THEN 'BUAT '+Keterangan ELSE Keterangan END  " +
                        "ELSE (Select dbo.ItemNameInv(ItemID,ItemTypeID))END ItemName, Case When (Selisih-Libur)>2 Then 'No' else 'OK' End OnTime ,NoSPP, " +
                        " CONVERT(CHAR,xx.ApproveDate3,103)AS ApvMgr " +
                        " ,Case when((DATEDIFF(DAY,isnull(xx.ApproveDate3,'1/1/1900'),isnull(xx.popurchndate,'1/1/1900')) -  " +
                        " (case when xx.ApproveDate3<xx.popurchndate then (select dbo.GetOFFDay(isnull(xx.ApproveDate3,'1/1/1900'),  " +
                        " isnull(xx.popurchndate,'1/1/1900'))) else 0 end)))>" + LamaSPPKePO + " Then 'NO' else 'OK' end  OnPO   " +
                        " FROM (  " +
                        " SELECT *, " +
                        " CAST(Convert(datetime,AppPO) +JamApv as datetime)ApproveDate1,  " +
                        " CAST(AppPOM +JamApvM as datetime)ApproveDate2, " +
                        "  (Select Top 1 AppDate From EventApprovalLog WHERE DocType='SPP' AND AppLevel=3 and DocNo=NoSPP) ApproveDate3, " +
                        "  DATEDIFF(DAY,x.popurchndate, CAST(Convert(datetime,AppPO) +JamApv as datetime))Selisih, " +
                        "  Case when AppPO is not null then  (select dbo.GetOFFDay(x.popurchndate,CAST(convert(datetime,AppPO)+JamApv as datetime))) else 0 end  " +
                        "  Libur FROM (   " +
                        "  select PO0.ID,PO0.NoPO,Convert(Char,PO0.popurchndate,103)TglPO,Convert(Char,PO0.CreatedTime,108)Jam,   " +
                        "  (Select TOP 1 Convert(char,AppDate,112)from EventApprovalLog where DocNo=PO0.NoPO and AppLevel=1 and DocType='PO' ORDER BY EventApprovalLog.ID Desc )AppPO,  " +
                        "  (Select TOP 1 Convert(Char,AppDate,108)from EventApprovalLog where DocNo=PO0.NoPO and AppLevel=1 and DocType='PO' ORDER BY EventApprovalLog.ID Desc )JamApv,   " +
                        "  (SELECT TOP 1 Convert(char,AppDate,112)FROM EventApprovalLog where DocNo=PO0.NoPO and AppLevel=2 and DocType='PO' ORDER BY EventApprovalLog.ID Desc )AppPOM,  " +
                        "  (SELECT TOP 1 Convert(Char,AppDate,108)FROM EventApprovalLog where DocNo=PO0.NoPO and AppLevel=2 and DocType='PO' ORDER BY EventApprovalLog.ID Desc )JamApvM,   " +
                        "  PO0.popurchndate, PO.GroupID,PO.ItemID,PO.ItemTypeID,sppd.TypeBiaya,sppd.Keterangan,SPP.NoSPP from POPurchn PO0  LEFT JOIN POPurchnDetail as PO on PO.POID=PO0.ID and PO.Status>-1  " +
                        "   LEFT JOIN SPP on SPP.ID=PO.SPPID  LEFT JOIN SPPDetail sppd on sppd.ID=PO.SPPDetailID " + this.inventory +
                        "   where " + this.Type + " PO0.Status>-1 " + this.Criteria +
                            " )as x  " +
                        "   ) as xx   " +
                             this.Additional + " order by NOPO";
                    break;
                case "UserAcount":
                    query = "select * from UsersApp where RowStatus>-1 and AppDoc='PO' " + this.Criteria;
                    break;
            }

            return query;
        }
        private POPurchn gObject(SqlDataReader sdr)
        {
            objP = new POPurchn();
            switch (this.Pilih)
            {
                case "Tahun":
                    objP.Tahun = Convert.ToInt32(sdr["Tahun"].ToString());
                    break;
                case "Detail":
                    objP.NoPO = sdr["NoPO"].ToString();
                    objP.POPurchnDate = Convert.ToDateTime(sdr["CreatedTime"].ToString());
                    objP.Jam = Convert.ToDateTime(sdr["Jam"].ToString()).ToString("HH:mm");
                    objP.ApproveDate1 = (sdr["ApproveDate1"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(sdr["ApproveDate1"].ToString());
                    objP.ApproveDate2 = (sdr["ApproveDate2"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(sdr["ApproveDate2"].ToString());
                    objP.UP = (sdr["OnTime"].ToString() == "OK") ? "X" : string.Empty;
                    objP.NoPol = (sdr["OnTime"].ToString() != "OK") ? "X" : string.Empty;
                    objP.ID = (sdr["Selisih"] == DBNull.Value) ? 0 : Convert.ToInt32(sdr["Selisih"].ToString());
                    objP.NOSPP = sdr["NoSPP"].ToString();
                    objP.AlasanBatal = sdr["ApvMgr"].ToString();
                    objP.UOMCode = (sdr["onPO"].ToString() == "OK") ? "X" : string.Empty;
                    objP.AlasanClose = (sdr["onPO"].ToString() != "OK") ? "X" : string.Empty;
                    objP.ItemCode = sdr["ItemCode"].ToString();
                    objP.ItemName = sdr["ItemName"].ToString();
                    objP.ApproveDate1S = (sdr["ApproveDate1"] == DBNull.Value) ? "" : Convert.ToDateTime(sdr["ApproveDate1"].ToString()).ToString("dd/MM/yyyy");
                    objP.ApproveDate2S = (sdr["ApproveDate2"] == DBNull.Value) ? "" : Convert.ToDateTime(sdr["ApproveDate2"].ToString()).ToString("dd/MM/yyyy");
                    objP.JamAppv1 = (sdr["ApproveDate1"] == DBNull.Value) ? "" : Convert.ToDateTime(sdr["ApproveDate1"].ToString()).ToString("HH:mm");
                    objP.JamAppv2 = (sdr["ApproveDate2"] == DBNull.Value) ? "" : Convert.ToDateTime(sdr["ApproveDate2"].ToString()).ToString("HH:mm");
                    break;
                case "DetailByTglPO":
                    objP.NoPO = sdr["NoPO"].ToString();
                    objP.POPurchnDate = Convert.ToDateTime(sdr["POPurchnDate"].ToString());
                    objP.Jam = Convert.ToDateTime(sdr["Jam"].ToString()).ToString("HH:mm");
                    objP.ApproveDate1 = (sdr["ApproveDate1"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(sdr["ApproveDate1"].ToString());
                    objP.ApproveDate2 = (sdr["ApproveDate2"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(sdr["ApproveDate2"].ToString());
                    objP.UP = (sdr["OnTime"].ToString() == "OK") ? "X" : string.Empty;
                    objP.NoPol = (sdr["OnTime"].ToString() != "OK") ? "X" : string.Empty;
                    objP.ID = (sdr["Selisih"] == DBNull.Value) ? 0 : Convert.ToInt32(sdr["Selisih"].ToString());
                    objP.NOSPP = sdr["NoSPP"].ToString();
                    objP.AlasanBatal = sdr["ApvMgr"].ToString();
                    objP.UOMCode = (sdr["onPO"].ToString() == "OK") ? "X" : string.Empty;
                    objP.AlasanClose = (sdr["onPO"].ToString() != "OK") ? "X" : string.Empty;
                    if (sdr["onPO"].ToString() == "OK" && sdr["ItemCode"].ToString() == "011102041060000" || sdr["ItemCode"].ToString() == "011102037020000" || sdr["ItemCode"].ToString() == "011073007050000"
                        || sdr["ItemCode"].ToString() == "015268001000000" || sdr["ItemCode"].ToString() == "011102043080000" || sdr["ItemCode"].ToString() == "011100002030000"
                        || sdr["ItemCode"].ToString() == "011102058170000" || sdr["ItemCode"].ToString() == "011102059180000" || sdr["ItemCode"].ToString() == "113250004040300"
                        || sdr["ItemCode"].ToString() == "011102038030000" || sdr["ItemCode"].ToString() == "014707001010000" || sdr["ItemCode"].ToString() == "015267001000000" || sdr["ItemCode"].ToString() == "014707002010100"
                        || sdr["ItemCode"].ToString() == "014793003010000" || sdr["ItemCode"].ToString() == "011102044090000" || sdr["ItemCode"].ToString() == "011102003330000"
                        || sdr["ItemCode"].ToString() == "011047003000200" || sdr["ItemCode"].ToString() == "012019003020000" || sdr["ItemCode"].ToString() == "115370001000000"
                        || sdr["ItemCode"].ToString() == "012019007020003" || sdr["ItemCode"].ToString() == "113250004040300" || sdr["ItemCode"].ToString() == "113250003030100"
                        || sdr["ItemCode"].ToString() == "015795001000000" || sdr["ItemCode"].ToString() == "015794004030100" || sdr["ItemCode"].ToString() == "015794003020100"
                        || sdr["ItemCode"].ToString() == "015794002010100" || sdr["ItemCode"].ToString() == "015794001010000" || sdr["ItemCode"].ToString() == "035998001000300")
                    {
                        objP.UOMCode = "X";
                        objP.AlasanClose = (sdr["onPO"].ToString() != "OK") ? string.Empty : string.Empty;
                    }
                    else if (sdr["onPO"].ToString() != "OK" && sdr["ItemCode"].ToString() == "011102041060000" || sdr["ItemCode"].ToString() == "011102037020000" || sdr["ItemCode"].ToString() == "011073007050000"
                        || sdr["ItemCode"].ToString() == "015268001000000" || sdr["ItemCode"].ToString() == "011102043080000" || sdr["ItemCode"].ToString() == "011100002030000"
                        || sdr["ItemCode"].ToString() == "011102058170000" || sdr["ItemCode"].ToString() == "011102059180000" || sdr["ItemCode"].ToString() == "113250004040300"
                        || sdr["ItemCode"].ToString() == "011102038030000" || sdr["ItemCode"].ToString() == "014707001010000" || sdr["ItemCode"].ToString() == "015267001000000" || sdr["ItemCode"].ToString() == "014707002010100"
                        || sdr["ItemCode"].ToString() == "014793003010000" || sdr["ItemCode"].ToString() == "011102044090000" || sdr["ItemCode"].ToString() == "011102003330000"
                        || sdr["ItemCode"].ToString() == "011047003000200" || sdr["ItemCode"].ToString() == "012019003020000" || sdr["ItemCode"].ToString() == "115370001000000"
                        || sdr["ItemCode"].ToString() == "012019007020003" || sdr["ItemCode"].ToString() == "113250004040300" || sdr["ItemCode"].ToString() == "113250003030100"
                        || sdr["ItemCode"].ToString() == "015795001000000" || sdr["ItemCode"].ToString() == "015794004030100" || sdr["ItemCode"].ToString() == "015794003020100"
                        || sdr["ItemCode"].ToString() == "015794002010100" || sdr["ItemCode"].ToString() == "015794001010000" || sdr["ItemCode"].ToString() == "035998001000300")
                    {
                        objP.UOMCode = "X";
                        objP.AlasanClose = (sdr["onPO"].ToString() != "OK") ? string.Empty : string.Empty;
                    }
                    //if (sdr["ItemCode"].ToString() == "011102041060000" || sdr["ItemCode"].ToString() == "011102037020000" || sdr["ItemCode"].ToString() ==  "011073007050000"
                    //    || sdr["ItemCode"].ToString() == "015268001000000" || sdr["ItemCode"].ToString() == "011102043080000" || sdr["ItemCode"].ToString() == "011100002030000"
                    //    || sdr["ItemCode"].ToString() == "011102058170000" || sdr["ItemCode"].ToString() == "011102059180000" || sdr["ItemCode"].ToString() == "113250004040300"
                    //    || sdr["ItemCode"].ToString() == "011102038030000" || sdr["ItemCode"].ToString() == "014707001010000" || sdr["ItemCode"].ToString() == "015267001000000")
                    //{
                    //    objP.UOMCode = (sdr["onPO"].ToString() != "OK") ? "X" : string.Empty;
                    //    objP.AlasanClose = (sdr["onPO"].ToString() != "OK") ? string.Empty : "X";
                    //}
                    objP.ItemCode = sdr["ItemCode"].ToString();
                    objP.ItemName = sdr["ItemName"].ToString();
                    objP.ApproveDate1S = (sdr["ApproveDate1"] == DBNull.Value) ? "" : Convert.ToDateTime(sdr["ApproveDate1"].ToString()).ToString("dd/MM/yyyy");
                    objP.ApproveDate2S = (sdr["ApproveDate2"] == DBNull.Value) ? "" : Convert.ToDateTime(sdr["ApproveDate2"].ToString()).ToString("dd/MM/yyyy");
                    objP.JamAppv1 = (sdr["ApproveDate1"] == DBNull.Value) ? "" : Convert.ToDateTime(sdr["ApproveDate1"].ToString()).ToString("HH:mm");
                    objP.JamAppv2 = (sdr["ApproveDate2"] == DBNull.Value) ? "" : Convert.ToDateTime(sdr["ApproveDate2"].ToString()).ToString("HH:mm");
                    break;
                case "UserAcount":
                    objP.AlasanBatal = sdr["AppGroup"].ToString();
                    break;
            }
            return objP;
        }

    }
}

