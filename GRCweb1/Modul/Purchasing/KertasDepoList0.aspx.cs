using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BusinessFacade;
using Domain;
using System.Data.SqlClient;
using System.IO;
namespace GRCweb1.Modul.Purchasing
{
    public partial class KertasDepoList0 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                string v = (Request.QueryString["v"] != null) ? Request.QueryString["v"].ToString() : "";
                LoadBulan();
                LoadTahun();
                LoadDeptKertas();
                LoadTujuanKirim();
                tarikdatakertas();
                txtEstimasi.Text = "";// DateTime.Now.ToString("dd-MMM-yyyy");
                string[] ViewKirim = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ViewKirim", "DepoKertas").Split(',');
                int posKirim = (Array.IndexOf(ViewKirim, ((Users)Session["Users"]).UserName));
                switch (v)
                {
                    case "1":
                        btnBack.Visible = true;
                        btnList.Visible = false;
                        lst.Visible = true;
                        lstMonitoring.Visible = false;
                        LoadList(true);
                        lstKirim.Attributes.Add("style", "height:490px");
                        break;
                    case "2":
                        btnBack.Visible = false;
                        btnList.Visible = (posKirim > -1) ? true : false;
                        btnList.Text = "List Kiriman";
                        lst.Visible = true;
                        lstKirim.Visible = false;
                        lstMonitoring.Visible = true;
                        Monitoring();
                        LockTujuanKirim();
                        break;
                    default:
                        btnBack.Visible = false;
                        btnList.Visible = true;
                        btnList.Visible = (posKirim > -1) ? true : false;
                        btnList.Text = "View Rekap";
                        lst.Visible = true;
                        lstMonitoring.Visible = false;
                        LoadList(true);
                        LockTujuanKirim();
                        break;
                }

            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('thr', 500, 99 , 40 ,false); </script>", false);
        }
        private void tarikdatakertas()
        {
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "exec tarik_data_kertas";
            SqlDataReader sdr = zl.Retrieve();
        }
        private void LockTujuanKirim()
        {
            Users user = (Users)Session["Users"];
            string[] ViewAllDept = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ViewAllData", "DepoKertas").Split(',');
            int posIt = Array.IndexOf(ViewAllDept, ((Users)Session["Users"]).UserName.ToLower());
            if (posIt > -1)
            {
                ddlTujuanKirim.Enabled = true;
                log.Visible = (user.DeptID == 10) ? true : false;
            }
            else
            {
                switch (user.UnitKerjaID)
                {
                    case 1:
                    case 7:
                        ddlTujuanKirim.Enabled = false;
                        ddlTujuanKirim.SelectedValue = ((Users)Session["Users"]).UnitKerjaID.ToString();
                        ddlDepo.Enabled = true;
                        log.Visible = (user.DeptID == 10) ? true : false;

                        break;
                    default:
                        ddlDepo.Enabled = true;
                        ddlDepo.SelectedValue = user.UnitKerjaID.ToString();
                        ddlTujuanKirim.Enabled = true;
                        break;
                }
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("delivkirimkertas0.aspx");
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            string v = (Request.QueryString["v"] != null) ? Request.QueryString["v"].ToString() : "";
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=DeliveryKertasList.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            switch (v)
            {
                case "1": return; break;
                case "2": lstMonitoring.RenderControl(hw); break;
                default: lstKirim.RenderControl(hw); break;
            }
            string html = "<b>LIST PENGIRIMAN KERTAS DEPO</b>";
            html += (ddlDepo.SelectedIndex == 0) ? "" : "Depo Name : " + ddlDepo.SelectedItem.Text;
            html += "Periode : " + ddlBulan.SelectedItem.Text + " " + ddlTahun.SelectedValue.ToString();
            html += (ddlTujuanKirim.SelectedIndex == 0) ? "" : "Tujuan Kirim : " + ddlTujuanKirim.SelectedItem.Text;
            string HtmlEnd = "";
            string Contentx = sw.ToString();
            Contentx = Contentx.Replace("border=\"0", "border=\"1");
            Response.Write(html + Contentx + HtmlEnd);
            Response.Flush();
            Response.End();
        }
        protected void lstDepo_DataBound(object sender, RepeaterItemEventArgs e)
        {
            DeliveryKertas dk = (DeliveryKertas)e.Item.DataItem;
            Label supplier = (Label)e.Item.FindControl("txtspl");
            HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("xx");
            //if (dk.PlantID == 1)
            //{
            //    //baca supplier
            //    DataTable dts = new DataTable();
            //    DataSet ds = new DataSet();
            //    //bpas_api.WebService1 bpas = new bpas_api.WebService1();
            //    Global2 bpas = new Global2();
            //    ds = bpas.GetDataTable("SuppPurch", "SupplierName", "where ID=" + dk.SupplierID, "GRCBOARDPURCH");
            //    //if (ds.ExtendedProperties.Count > 0)
            //    //{
            //        dts = ds.Tables[0];
            //        foreach (DataRow dr in dts.Rows)
            //        {
            //            supplier.Text = dr["SupplierName"].ToString();
            //        }
            //    //}
            //    //else
            //    //    supplier.Text = string.Empty;

            //}
            tr.Attributes.Add("title", dk.ItemName);
            tr.Attributes.Add("class", "EvenRows baris");
            ((Image)e.Item.FindControl("edt")).Visible = (dk.ID > 0) ? false : false;
            ((Image)e.Item.FindControl("hps")).Visible = (dk.ID > 0) ? false : false;
            ((Image)e.Item.FindControl("del")).Visible = (dk.ID == 0) ? false : false;
            ((Image)e.Item.FindControl("sts")).Visible = (dk.ID > 0) ? false : false;
        }
        protected void lstDepo_Command(object sender, RepeaterCommandEventArgs e)
        {
            ArrayList arrData = new ArrayList();
            DepoKertas dks = new DepoKertas();
            DeliveryKertas dv = new DeliveryKertas();
            DeliveryKertas dk = new DeliveryKertas();
            string Idx = e.CommandArgument.ToString();
            HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("xx");
            switch (e.CommandName)
            {
                case "edit":
                    //dk = dks.Retrieve("and ID=" + Idx, true);
                    //ddlDepo.SelectedValue = dk.DepoID.ToString();
                    //LoadChecker();
                    //ddlChecker.SelectedValue = dk.Checker;
                    //txtDocNo.Text = dk.ID.ToString();
                    //ddlTujuanKirim.SelectedValue = dk.PlantID.ToString();
                    //txtNoSJ.Text = dk.NoSJ.ToString();
                    //txtTglKirim.Text = dk.TglKirim.ToString("dd-MMM-yyyy");
                    //txtETA.Text = dk.TglETA.ToString("dd-MMM-yyyy");
                    //txtExpedisi.Text = dk.Expedisi.ToString();
                    //txtNOPOL.Text = dk.NOPOL.ToString();
                    //txtGross.Text = dk.GrossDepo.ToString("###,###.#0");
                    //txtNetto.Text = dk.NettDepo.ToString("###,###.#0");
                    //txtKA.Text = dk.KADepo.ToString("###,###.#0");
                    //txtJmlBAL.Text = dk.JmlBAL.ToString("###,###.#0");
                    //txtID.Value = dk.ID.ToString();
                    //btnSimpan.Enabled = true;
                    break;
                case "delete":
                    dv.ID = dk.ID;
                    dv.RowStatus = -1;
                    dv.LastModifiedBy = ((Users)Session["Users"]).UserName;
                    dv.LastModifiedTime = DateTime.Now;

                    int result = dks.Delete(dv);
                    if (result > 0) tr.Visible = false;
                    break;
                case "hapus":
                    arrData.RemoveAt(int.Parse(Idx));
                    tr.Visible = false;
                    break;
            }
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            btnPreview.Enabled = false;
            string v = (Request.QueryString["v"] != null) ? Request.QueryString["v"].ToString() : "";
            switch (v)
            {
                case "2":
                    Monitoring();
                    break;
                default:
                    LoadList(true);
                    break;
            }
            btnPreview.Enabled = true;
        }
        private void LoadList(bool p)
        {
            ArrayList arrData = new ArrayList();
            DepoKertas dp = new DepoKertas();
            Users user = (Users)Session["Users"];
            string[] UserViewAll = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ViewAllData", "DepoKertas").Split(',');
            int pos = Array.IndexOf(UserViewAll, user.UserName);
            string Criteria = (pos != -1) ? "" : (user.UnitKerjaID == 7 || user.UnitKerjaID == 1 || user.UnitKerjaID == 13) ? "and PlantID=" + user.UnitKerjaID : " and CreatedBy='" + user.UserName + "' ";
            Criteria += (ddlDepo.SelectedIndex > 0) ? " And DepoID=" + ddlDepo.SelectedValue : "";
            Criteria += " And Month(TglKirim)=" + ddlBulan.SelectedValue;
            Criteria += " And Year(TglKirim)=" + ddlTahun.SelectedValue;
            Criteria += (ddlTujuanKirim.SelectedIndex > 0) ? " and PlantID=" + ddlTujuanKirim.SelectedValue : "";
            arrData = dp.Retrieve(Criteria);
            lstDepo.DataSource = arrData;
            lstDepo.DataBind();
        }

        private void LoadList()
        {
            ArrayList arrData = new ArrayList();
            DepoKertas dp = new DepoKertas();
            Users user = (Users)Session["Users"];
            string[] UserViewAll = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ViewAllData", "DepoKertas").Split(',');
            int pos = Array.IndexOf(UserViewAll, user.UserName);
            string Criteria = (pos != -1) ? "" : (user.UnitKerjaID == 7 || user.UnitKerjaID == 1 || user.UnitKerjaID == 13) ? "and PlantID=" + user.UnitKerjaID : " and CreatedBy='" + user.UserName + "'";
            //Criteria += "Order By ID desc";
            arrData = dp.Retrieve(Criteria);
            lstDepo.DataSource = arrData;
            lstDepo.DataBind();
        }
        private void Monitoring()
        {
            ArrayList arrData = new ArrayList();
            DepoKertas dp = new DepoKertas();
            Users user = (Users)Session["Users"];
            DateTime? dt = null;
            DateTime tglETA;
            dt = DateTime.TryParse(txtEstimasi.Text, out tglETA) ? tglETA : (DateTime?)null;
            string[] UserViewAll = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ViewAllData", "DepoKertas").Split(',');
            int pos = Array.IndexOf(UserViewAll, user.UserName);
            string Criteria = (pos != -1) ? "" : (user.UnitKerjaID == 7 || user.UnitKerjaID == 1 || user.UnitKerjaID == 13) ? "and PlantID=" +
                user.UnitKerjaID : " and CreatedBy='" + user.UserName + "'";
            Criteria += (ddlDepo.SelectedIndex > 0) ? " And DepoID=" + ddlDepo.SelectedValue.ToString() : "";
            Criteria += " And Month(TglKirim)=" + ddlBulan.SelectedValue;
            Criteria += " And Year(TglKirim)=" + ddlTahun.SelectedValue;
            if (log.Visible == true)
            {
                Criteria += (dt != null) ? " And (Convert(Char,TglETA,112)='" + DateTime.Parse(dt.ToString()).ToString("yyyyMMdd") + "')" : "";
                // /*OR Convert(Char,TglReceipt,112)='" + DateTime.Now.ToString("yyyyMMdd") + "'*/)" : "";
            }//Criteria += (chkOTW1.Checked == true) ? " And TglETA='" + DateTime.Now.AddDays(1).ToString("yyyyMMdd") + "' AND ISNULL(POKAID,0)<=0" : "";
            Criteria += (ddlTujuanKirim.SelectedIndex > 0) ? " and PlantID=" + ddlTujuanKirim.SelectedValue : "";
            arrData = dp.Monitoring(Criteria);
            lstMonDepo.DataSource = arrData;
            lstMonDepo.DataBind();
        }
        protected void lstMonDepo_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ArrayList arrData = new ArrayList();
            DepoKertas dp = new DepoKertas();
            DeliveryKertas dks = (DeliveryKertas)e.Item.DataItem;
            Users user = (Users)Session["Users"];
            DateTime? dt = null;
            DateTime tglETA;
            dt = DateTime.TryParse(txtEstimasi.Text, out tglETA) ? tglETA : (DateTime?)null;
            Repeater rptDetail = (Repeater)e.Item.FindControl("lstDepoDetail");
            string[] UserViewAll = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ViewAllData", "DepoKertas").Split(',');
            int pos = Array.IndexOf(UserViewAll, user.UserName);
            string Criteria = (pos != -1) ? "" : (user.UnitKerjaID == 7 || user.UnitKerjaID == 1 || user.UnitKerjaID == 13) ? "and PlantID=" + user.UnitKerjaID : " and CreatedBy='" + user.UserName + "'";
            Criteria += " And DepoID=" + dks.DepoID;
            Criteria += " And Month(TglKirim)=" + ddlBulan.SelectedValue;
            Criteria += " And Year(TglKirim)=" + ddlTahun.SelectedValue;
            Criteria += (ddlTujuanKirim.SelectedIndex > 0) ? " and PlantID=" + ddlTujuanKirim.SelectedValue : "";
            if (log.Visible == true)
            {
                Criteria += (dt != null) ? " And (Convert(Char,TglETA,112)='" + DateTime.Parse(dt.ToString()).ToString("yyyyMMdd") + "')" : "";// /*OR Convert(Char,TglReceipt,112)='" + DateTime.Now.ToString("yyyyMMdd") + "'*/)" : "";
            }

            arrData = (int.Parse(ddlTahun.SelectedValue + ddlBulan.SelectedValue) < 20173) ? dp.Monitoring(Criteria, true) : dp.Monitoring0(Criteria, true, "201703", user.UnitKerjaID);
            rptDetail.DataSource = arrData;
            rptDetail.DataBind();
        }
        protected void lstDepoDetail_Databound(object sender, RepeaterItemEventArgs e)
        {
            Users user = (Users)HttpContext.Current.Session["Users"];
            HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("lstx");
            DeliveryKertas dp = (DeliveryKertas)e.Item.DataItem;
            try
            {
                string[] userDepo = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Depo", "DepoKertas").Split(',');
                int x = Array.IndexOf(userDepo, ((Users)Session["Users"]).UnitKerjaID.ToString());
                tr.Cells[1].Attributes.Add("title", dp.ItemName.ToString());
                tr.Cells[3].Attributes.Add("title", dp.SupplierName.ToString());
                decimal crpersen = 0;
                if (dp.NoSJ.Trim() == "JG170221 (AP)")
                {
                    string test = "";
                }
                //if (user.UnitKerjaID != dp.PlantID)
                //{
                    string strcon = string.Empty;
                    if (dp.PlantID == 1)
                        strcon = "GRCBoardCtrp";
                    if (dp.PlantID == 7)
                        strcon = "GRCBoardKrwg";
                    if (dp.PlantID == 13)
                        strcon = "GRCBoardJmb";
                //}
                    decimal selisihBal = 0;
                    DataTable dt = new DataTable();
                    DataSet dss = new DataSet();
                    Global2 bpas = new Global2();
                    //if (dp.NoSJ == "JM250121-1B (MJB-F)")
                    //{
                    //    string test = string.Empty;
                    //}
                    //string tglPO = GetTgPO(dp.NoSJ, dp.PlantID).ToString("dd/MM/yyyy");
                    //if (tglPO == "01/01/1900")
                    //    tglPO = "";
                    string Criterie = "WHERE rowstatus>-1 and NOSJ='" + dp.NoSJ + "' AND Nopol='" + dp.NOPOL + "' AND ItemCode='" + dp.ItemCode + "'";
                    dss = bpas.GetDataFromTable("DeliveryKertasKA", Criterie, strcon);
                    dt = dss.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {

                        //tr.Cells[12].InnerHtml = tglPO;
                        tr.Cells[14].InnerHtml = (decimal.Parse(dr["GrossPlant"].ToString())).ToString("N0");
                        if (dr["NettPlant0"].ToString().Trim() != string.Empty)
                            tr.Cells[15].InnerHtml = (decimal.Parse(dr["NettPlant0"].ToString())).ToString("N0");
                        else
                            tr.Cells[15].InnerHtml = "0";
                        tr.Cells[16].InnerHtml = (decimal.Parse(dr["AvgKA"].ToString())).ToString("N2");
                        tr.Cells[17].InnerHtml = (decimal.Parse(dr["JMLBal"].ToString())).ToString("N0");
                        tr.Cells[18].InnerHtml = (decimal.Parse(dr["Sampah"].ToString())).ToString("N2");
                        decimal bbdepo = 0;
                        decimal.TryParse(tr.Cells[6].InnerText, out bbdepo);

                        if (bbdepo > 0)
                        {
                            tr.Cells[19].InnerHtml = (tr.Cells[14].InnerHtml == "0") ? "0" : ((decimal.Parse(dr["NettPlant0"].ToString())) - bbdepo).ToString("N0");
                            tr.Cells[20].InnerHtml = (tr.Cells[14].InnerHtml == "0") ? "0" : ((((decimal.Parse(dr["NettPlant0"].ToString())) / bbdepo) * 100) - 100).ToString("N2");
                        }
                        else
                            tr.Cells[20].InnerHtml = bbdepo.ToString("N0");
                        // (decimal.Parse(dr["Persen"].ToString())).ToString("N2");
                        selisihBal = (decimal.Parse(dr["JMLBal"].ToString())) - dp.JmlBAL;
                        tr.Cells[21].InnerHtml = selisihBal.ToString("N0");
                    }
                    decimal.TryParse(tr.Cells[19].InnerText.Replace("(", "-").Replace(")", ""), out crpersen);

                //}
                #region remark
                //if (dp.PlantID == 13)
                //{
                //    decimal selisihBal = 0;
                //    DataTable dt = new DataTable();
                //    DataSet dss = new DataSet();
                //    //bpas_api.WebService1 bpas = new bpas_api.WebService1();
                //    Global2 bpas = new Global2();
                //    string Criterie = "WHERE rowstatus>-1 and NOSJ='" + dp.NoSJ + "' AND Nopol='" + dp.NOPOL + "' AND ItemCode='" + dp.ItemCode + "'";
                //    //AND SupplierID=" + dp.SupplierID;
                //    dss = bpas.GetDataFromTable("DeliveryKertasKA", Criterie, "GRCBoardJmb");
                //    dt = dss.Tables[0];
                //    foreach (DataRow dr in dt.Rows)
                //    {
                //        tr.Cells[12].InnerHtml = dr["TglCheck"].ToString().Substring(0, 10);
                //        tr.Cells[13].InnerHtml = (decimal.Parse(dr["GrossPlant"].ToString())).ToString("N0");
                //        tr.Cells[14].InnerHtml = (decimal.Parse(dr["NettPlant0"].ToString())).ToString("N0");
                //        tr.Cells[15].InnerHtml = (decimal.Parse(dr["AvgKA"].ToString())).ToString("N2");
                //        tr.Cells[16].InnerHtml = (decimal.Parse(dr["JMLBal"].ToString())).ToString("N0");
                //        tr.Cells[17].InnerHtml = (decimal.Parse(dr["Sampah"].ToString())).ToString("N2");

                //        decimal bbdepo = 0;
                //        decimal.TryParse(tr.Cells[6].InnerText, out bbdepo);
                //        tr.Cells[18].InnerHtml = ((decimal.Parse(dr["NettPlant0"].ToString())) - bbdepo).ToString("N0");
                //        tr.Cells[19].InnerHtml = ((((decimal.Parse(dr["NettPlant0"].ToString())) / bbdepo) * 100) - 100).ToString("N2");
                //        // (decimal.Parse(dr["Persen"].ToString())).ToString("N2");
                //        selisihBal = (decimal.Parse(dr["JMLBal"].ToString())) - dp.JmlBAL;
                //        tr.Cells[20].InnerHtml = selisihBal.ToString("N0");
                //    }
                //    decimal.TryParse(tr.Cells[19].InnerText.Replace("(", "-").Replace(")", ""), out crpersen);

                //}
                #endregion
                string p = tr.Cells[20].InnerText;


                if (dp.Persen <= 0 || crpersen < -5)
                {
                    tr.Cells[20].BgColor = "YellowGreen";
                    tr.Cells[20].InnerHtml = (CheckAttKonfirmasi(dp.NoSJ, dp.PlantID) == true) ?
                        p + " <img src='../../images/Logo_PDF.png' alt='img' title='view attachment'/>" : (x == -1) ? p.ToString() :
                        p + " <img src='../../images/Logo_Download.png' alt='upload' title='Upload Konfirmasi document in PDF'/>";

                    tr.Cells[20].Attributes.Add("onclick", (CheckAttKonfirmasi(dp.NoSJ, dp.PlantID) == true) ? "ViewPDF('" + dp.NoSJ + "')" : "UploadPDF('" + dp.NoSJ + "')");


                    string alasan = LoadAlasan(dp.NoSJ, dp.PlantID).Trim();
                    //if (dp.Persen == -100)
                    if (alasan.Trim() != string.Empty)
                    {

                        tr.Cells[1].BgColor = "Red";
                        //string alasan = LoadAlasan(dp.NoSJ).Trim();
                        tr.Cells[1].Attributes.Add("title", dp.ItemName.ToString() + " Ditolak karena (" + alasan + ")");
                    }
                }

                tr.Cells[20].InnerHtml = (dp.PlantID == 7) ? (dp.Jumlah - dp.JmlBAL).ToString("N0") : tr.Cells[20].InnerHtml;
            }
            catch { }
        }
        private string LoadAlasan(string nosj, int PlantID)
        {
            string result = string.Empty;
            Users user = (Users)HttpContext.Current.Session["Users"];
            ZetroView zl = new ZetroView();
            string strcon = string.Empty;
            if (user.UnitKerjaID != PlantID)
            {
                if (PlantID == 1 && user.UnitKerjaID != 1)
                    strcon = "[sqlctrp.grcboard.com].bpasctrp.dbo.";
                if (PlantID == 7 && user.UnitKerjaID != 7)
                    strcon = "[sqlkrwg.grcboard.com].bpaskrwg.dbo.";
                if (PlantID == 13 && user.UnitKerjaID != 13)
                    strcon = "[sqljombang.grcboard.com].bpasjombang.dbo.";
            }
            if (user.UnitKerjaID != 1 && user.UnitKerjaID != 7 && user.UnitKerjaID != 13)
                strcon = "";
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select top 1 * from " + strcon + "ListPenolakanKertas where nosj='" + nosj + "' order by id desc";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = sdr["alasan"].ToString();
                }
            }
            return result;
        }
        private DateTime GetTgPO(string nosj, int PlantID)
        {
            string strcon = string.Empty;
            Users user = (Users)HttpContext.Current.Session["Users"];
            if (user.UnitKerjaID != PlantID)
            {
                if (PlantID == 1 && user.UnitKerjaID != 1)
                    strcon = "[sqlctrp.grcboard.com].bpasctrp.dbo.";
                if (PlantID == 7 && user.UnitKerjaID != 7)
                    strcon = "[sqlkrwg.grcboard.com].bpaskrwg.dbo.";
                if (PlantID == 13 && user.UnitKerjaID != 13)
                    strcon = "[sqljombang.grcboard.com].bpasjombang.dbo.";
            }
            if (user.UnitKerjaID != 1 && user.UnitKerjaID != 7 && user.UnitKerjaID != 13 && PlantID ==7)
                strcon = "";
            DateTime result = DateTime.Parse("1900-01-01");
            ZetroView zl = new ZetroView();
            string strsql = "Select POPurchnDate from " + strcon + "POPurchn where ID =(Select Top 1 POID from " + strcon + "POPurchnKadarAir where SchNo='" + nosj + "')";
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = strsql;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = DateTime.Parse(sdr["POPurchnDate"].ToString());
                }
            }
            return result;
        }
        protected void btnList_CLick(object sender, EventArgs e)
        {
            if (btnList.Text == "View Rekap")
            {
                Response.Redirect("KertasDepoList0.aspx?v=2");
            }
            else
            {
                Response.Redirect("KertasDepoList0.aspx");
            }
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
        private void LoadTahun()
        {
            POPurchnFacade pd = new POPurchnFacade();
            pd.GetTahun(ddlTahun);
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }
        private void LoadTujuanKirim()
        {
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select * from Company where RowStatus=0 and depoID in (1,7,13)";
            SqlDataReader sdr = zl.Retrieve();
            ddlTujuanKirim.Items.Clear();
            ddlTujuanKirim.Items.Add(new ListItem("--All--", "0"));
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlTujuanKirim.Items.Add(new ListItem(sdr["Nama"].ToString(), sdr["DepoID"].ToString()));
                }
            }
            //ddlTujuanKirim.SelectedValue = "7";
        }
        private void LoadDeptKertas()
        {
            Users user = (Users)HttpContext.Current.Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select * from DepoKertas where RowStatus=0";
            SqlDataReader sdr = zl.Retrieve();
            ddlDepo.Items.Clear();
            ddlDepo.Items.Add(new ListItem("--Pilih Depo--", "0"));
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlDepo.Items.Add(new ListItem(sdr["DepoName"].ToString(), sdr["Alamat"].ToString().TrimEnd()));
                }
            }
            ddlDepo.SelectedValue = user.UnitKerjaID.ToString();
        }
        private bool CheckAttKonfirmasi(string NoSJ, int plantID)
        {
            bool result = false;
            DepoKertas dk = new DepoKertas();
            string fileName = dk.CheckAtthConfirmasi(NoSJ);
            result = (fileName == "") ? false : true;
            return result;
        }
        private string FileAttachment(string NoSJ)
        {
            DepoKertas dk = new DepoKertas();
            string fileName = dk.CheckAtthConfirmasi(NoSJ);
            return fileName;
        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Response.Redirect("KertasDepoList0.aspx?v=2");
        }
    }
}