using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;
using Factory;
using Cogs;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GRCweb1.Modul.Factory
{
    public partial class PostingSuratJalanToCPDNew : System.Web.UI.Page
    {

        private bool bIsConnected = false;//the boolean value identifies whether the device is connected
        private int iMachineNumber;// = 1;//the serial number of the device.After connecting the device ,this value will be changed.
                                   //private string ipDevice = "10.0.0.151";
                                   // public string ipDevice;// = "192.168.100.147";
        public string ipDeviceIn;//= "192.168.100.147";
        public string ipDeviceOut;// = "192.168.100.148";
        public string lokasi;
        private string port = "4370";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";

                clearForm();
                Session["sjid"] = null;
                ViewState["TONo"] = string.Empty;
                if (Request.QueryString["SJNo"] != null)
                {
                    LoadSuratJalan(Request.QueryString["SJNo"].ToString());
                }
                if (Request.QueryString["ScheduleNo"] != null)
                {
                    txtTransferOrderNo.Text = Request.QueryString["TONo"].ToString();
                    txtScheduleNo.Text = Request.QueryString["ScheduleNo"].ToString();
                    txtFromAddress.Text = Request.QueryString["Address1"].ToString();
                    txtToAddress.Text = Request.QueryString["Address2"].ToString();

                    GetScheduleDetail();
                }
                txtActualKirim.Text = DateTime.Now.ToString("dd MMM yyyy");
                txtScheduleDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                LoadStocker();
                LoadKendaraan();
                //LoadDataGridLoading(LoadGridLoading());

            }

            btnTurunStatus.Attributes.Add("onclick", "return confirm_turunTO();");
        }
        private void LoadStocker()
        {
            Users user = (Users)HttpContext.Current.Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select distinct isnull(stocker,'')stocker from FC_LinkItemMkt where isnull(stocker,'') <>'' and RowStatus>-1 order by stocker";
            SqlDataReader sdr = zl.Retrieve();
            ddlStocker.Items.Clear();
            ddlStocker.Items.Add(new ListItem("-", "0"));
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlStocker.Items.Add(new ListItem(sdr["stocker"].ToString(), sdr["stocker"].ToString().TrimEnd()));
                }
            }
            ddlStocker.SelectedValue = user.UnitKerjaID.ToString();
        }
        protected void btnTurunStatus_ServerClick(object sender, EventArgs e)
        {

        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            LoadSuratJalan(txtSearch.Text);
        }

        private void clearForm()
        {
            Session["sjid"] = null;
            Session["ListOfSuratJalanDetailTO"] = null;
            txtSuratJalanNo.Text = string.Empty;
            txtTransferOrderNo.Text = string.Empty;
            txtScheduleNo.Text = string.Empty;
            txtNoMobil.Text = string.Empty;
            txtDriverName.Text = string.Empty;
            txtCreateDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtCreatedBy.Text = ((Users)Session["Users"]).UserName;
            btnSimpan.Disabled = false;
            ArrayList arrList = new ArrayList();
            arrList.Add(new SuratJalanDetailTO());
            GridView1.DataSource = arrList;
            GridView1.DataBind();
        }
        protected void ddlPengiriman_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadJenisPalet(ddlPengiriman.SelectedValue);
            DropDownList ddlPengiriman = (DropDownList)sender;
            string Pengiriman = ddlPengiriman.SelectedValue;

            // Get the GridViewRow in which this master DropDownList exists
            GridViewRow gridrow = (GridViewRow)ddlPengiriman.NamingContainer;
            DropDownList ddlJenisPalet = (DropDownList)gridrow.FindControl("ddlJenisPalet");
            ZetroView zl = new ZetroView();
            ArrayList arrlistkirim = new ArrayList();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "select distinct jenisPacking from MaterialCCMatrixPJ where rowstatus>-1 and pengiriman='" + Pengiriman + "' and tahun=" +
                DateTime.Now.Year + " order by jenisPacking";
            SqlDataReader sdr = zl.Retrieve();
            DataTable dt = new DataTable();
            dt.Columns.Add("jenisPacking", typeof(string));
            DataRow row = dt.NewRow();
            row["jenisPacking"] = "-";
            dt.Rows.Add(row);
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    row = dt.NewRow();
                    row["jenisPacking"] = sdr["jenisPacking"].ToString();
                    dt.Rows.Add(row);
                }
            }
            ddlJenisPalet.DataSource = dt;
            ddlJenisPalet.DataValueField = "jenisPacking";
            ddlJenisPalet.DataTextField = "jenisPacking";
            ddlJenisPalet.DataBind();
        }
        private void LoadSuratJalan(string strSJNo)
        {
            WebReference_HO.Service1 cpdWebService = new WebReference_HO.Service1();
            DataSet dsStrResult = cpdWebService.sjTORetrieveByNo(strSJNo);

            int suratJalanID = int.Parse(dsStrResult.Tables[0].Rows[0]["ID"].ToString());
            int intStatus = int.Parse(dsStrResult.Tables[0].Rows[0]["Status"].ToString());

            if (suratJalanID > 0)
            {
                if (intStatus == -1)
                {
                    DisplayAJAXMessage(this, "Surat Jalan ini sudah di-Cancel ");
                    return;
                }

                if (intStatus == 1)
                {
                    btnPostingReceipt.Disabled = false;
                }

                Session["sjid"] = suratJalanID;           //suratJalan.ID;
                Session["stat"] = intStatus;            // suratJalan.Status;
                Session["ctk"] = int.Parse(dsStrResult.Tables[0].Rows[0]["Cetak"].ToString());         //suratJalan.Cetak;
                                                                                                       //Session["CountCtk"] = int.Parse(dsStrResult.Tables[0].Rows[0]["CountPrint"].ToString());    //suratJalan.CountPrint;

                txtSuratJalanNo.Text = dsStrResult.Tables[0].Rows[0]["SuratJalanNo"].ToString();
                txtTransferOrderNo.Text = dsStrResult.Tables[0].Rows[0]["TransferOrderNo"].ToString();
                txtScheduleNo.Text = dsStrResult.Tables[0].Rows[0]["ScheduleNo"].ToString();
                txtNoMobil.Text = dsStrResult.Tables[0].Rows[0]["PoliceCarNo"].ToString();
                txtDriverName.Text = dsStrResult.Tables[0].Rows[0]["DriverName"].ToString();
                txtCreateDate.Text = DateTime.Parse(dsStrResult.Tables[0].Rows[0]["CreatedTime"].ToString()).ToString("dd-MMM-yyyy");
                txtCreatedBy.Text = dsStrResult.Tables[0].Rows[0]["CreatedBy"].ToString();
                txtActualKirim.Text = DateTime.Parse(dsStrResult.Tables[0].Rows[0]["postingshipmentdate"].ToString()).ToString("dd-MMM-yyyy");
                if (intStatus > 1)
                {
                    btnSimpan.Disabled = true;
                }
                else
                {
                    btnSimpan.Disabled = false;
                }
                if (intStatus > 0 || intStatus == -1)
                {
                    btnCancel.Enabled = false;
                    btnSimpan.Disabled = true;
                }
                else
                    btnCancel.Enabled = true;

                ArrayList arrSuratJalan = new ArrayList();

                //cpdWebService = new WebReference_HO.Service1();
                WebserviceFacade webServicePusat = new WebserviceFacade();
                DataTable dsDetSJ = webServicePusat.sjTO_CetakSJto(strSJNo);

                foreach (DataRow row in dsDetSJ.Rows)
                {
                    ScheduleDetail sDet = new ScheduleDetail();

                    sDet.ItemID = Int32.Parse(row["ItemID"].ToString());
                    sDet.ItemCode = row["ItemCode"].ToString();
                    sDet.ItemName = row["description"].ToString();
                    sDet.Qty = int.Parse(row["Qty"].ToString());

                    arrSuratJalan.Add(sDet);
                }
                if (arrSuratJalan.Count > 0)
                {
                    Session["ListOfSuratJalanDetail"] = arrSuratJalan;
                    GridView1.DataSource = arrSuratJalan;
                    GridView1.DataBind();

                    Session["sjid"] = suratJalanID;
                    Session["dtCetakSJto"] = dsDetSJ;
                }

                Session["SuratJalanNo"] = txtSuratJalanNo.Text;
                DataSet SJInfo = new DataSet();

                if (((Users)Session["Users"]).UnitKerjaID == 1)
                {
                    WebReference_Ctrp.Service1 webService1 = new WebReference_Ctrp.Service1();
                    SJInfo = webService1.GetSJInfoTO(txtSuratJalanNo.Text);
                }
                if (((Users)Session["Users"]).UnitKerjaID == 7)
                {
                    WebReference_Krwg.Service1 webService1 = new WebReference_Krwg.Service1();
                    SJInfo = webService1.GetSJInfoTO(txtSuratJalanNo.Text);
                }
                if (((Users)Session["Users"]).UnitKerjaID == 13)
                {
                    WebReference_Jmb.Service1 webService1 = new WebReference_Jmb.Service1();
                    SJInfo = webService1.GetSJInfoTO(txtSuratJalanNo.Text);
                }

                Session["customer"] = SJInfo.Tables[0].Rows[0]["customer"].ToString();
                LoadingTimeBySJ();
            }

        }


        private void SelectSchedule(DropDownList ddl, string strScheduleNo)
        {
            ddl.ClearSelection();
            foreach (ListItem item in ddl.Items)
            {
                if (item.Value == strScheduleNo)
                {
                    item.Selected = true;
                    return;
                }
            }
        }
        protected void btnList_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("ListPostSuratJalanTONew.aspx?receive=no");
        }

        protected void btnListTO_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("ListPostScheduleTONew.aspx");
        }
        protected void btnMap_ServerClick(object sender, EventArgs e)
        {
            string result = insertPartno(lblIDSJ.Text, txtPartnoA.Text);
            if (result == "sukses")
            {
                Panelmap.Visible = false;
                LoadSuratJalan(txtSuratJalanNo.Text);
            }
            else
            {
                DisplayAJAXMessage(this, result);
            }
        }
        private string insertPartno(string idsj, string partno)
        {
            string result = string.Empty;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "declare @ItemIDMkt int " +
                "declare @partno varchar(max) " +
                "declare @pesan varchar(max) " +
                "declare @volHO int " +
                "declare @volPlant int " +
                "set @partno='" + partno + "' " +
                "set @ItemIDMkt=" + idsj + " " +
                "set @pesan='Error' " +
                "set @volHO=(select tebal*panjang*lebar from [sql1.grcboard.com].grcboard.dbo.items where ID=@ItemIDMkt) " +
                "set @volPlant=(select tebal*panjang*lebar from fc_items where rowstatus>-1 and partno='" + partno + "') " +
                "if (select count(id) jml from FC_LinkItemMkt where itemidmkt=@ItemIDMkt and partno=@partno)>0 " +
                "begin set @pesan='partno sudah tersedia, tentukan partno lainnya' end " +
                "if @volHO<>@volPlant " +
                "begin set @pesan='Dimensi Produk harus sama' end " +
                "if (select count(id) jml from FC_LinkItemMkt where itemidmkt=@ItemIDMkt and partno=@partno )=0 and @volHO=@volPlant " +
                "begin " +
                "    insert FC_LinkItemMkt " +
                "    select @ItemIDMkt ItemIDMkt,(select [description] from [sql1.grcboard.com].grcboard.dbo.items where ID=@ItemIDMkt) ItemNameMkt,ID ItemIDFc, PartNo,0 RowStatus,  " +
                "    'admin' CreatedBy, getdate() CreatedTime, 'admin' LastModifiedBy, getdate()LastModifiedTime,'" + ddlStocker.SelectedItem.Text + "' stocker " +
                "     from FC_Items where partno=@partno " +
                "     set @pesan='sukses' " +
                "end " +
                "select @pesan pesan";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = sdr["pesan"].ToString().Trim();
                }
            }
            return result;
        }
        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearForm();
        }


        protected void btnSimpan_ServerClick(object sender, EventArgs e)
        {
            string strValidate = ValidateText();
            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }

            string strEvent = "Insert";

            SuratJalanTO suratJalanTO = new SuratJalanTO();

            if (Session["sjid"] != null)
            {
                suratJalanTO.ID = int.Parse(Session["sjid"].ToString());
                strEvent = "Edit";
            }

            int transferOrderID = 0; int scheduleID = 0; DateTime tglKirim = DateTime.MinValue;
            ArrayList arrSuratJalanDetailTO = new ArrayList();
            if (Session["ListOfSuratJalanDetailTO"] != null)
            {
                ArrayList arrScheduleDetailTO = (ArrayList)Session["ListOfSuratJalanDetailTO"];
                foreach (ScheduleDetail scheduleDetail in arrScheduleDetailTO)
                {
                    SuratJalanDetailTO suratJalanDetailTO = new SuratJalanDetailTO();
                    suratJalanDetailTO.ItemCode = scheduleDetail.ItemCode;
                    suratJalanDetailTO.ItemID = scheduleDetail.ItemID;
                    suratJalanDetailTO.ItemName = scheduleDetail.ItemName;
                    suratJalanDetailTO.Qty = scheduleDetail.Qty;
                    suratJalanDetailTO.ScheduleDetailId = scheduleDetail.ID;
                    suratJalanDetailTO.Paket = scheduleDetail.Paket;

                    transferOrderID = scheduleDetail.DocumentID;
                    scheduleID = scheduleDetail.ScheduleID;
                    tglKirim = scheduleDetail.ScheduleDate;

                    arrSuratJalanDetailTO.Add(suratJalanDetailTO);
                }
            }

            suratJalanTO.SuratJalanNo = txtSuratJalanNo.Text;
            suratJalanTO.TransferOrderID = transferOrderID;
            suratJalanTO.ScheduleID = scheduleID;
            suratJalanTO.PoliceCarNo = txtNoMobil.Text;
            suratJalanTO.DriverName = txtDriverName.Text;
            suratJalanTO.CreatedBy = txtCreatedBy.Text;

            WebReference_HO.Service1 cpdWebService = new WebReference_HO.Service1();

            DataTable dtTO = new DataTable();
            dtTO.Columns.Add("ScheduleID", typeof(int));
            dtTO.Columns.Add("TglKirimActual", typeof(DateTime));
            dtTO.Columns.Add("TransferOrderID", typeof(int));
            dtTO.Columns.Add("PoliceCarNo", typeof(string));
            dtTO.Columns.Add("DriverName", typeof(string));
            dtTO.Columns.Add("Keterangan", typeof(string));
            dtTO.Columns.Add("CreatedBy", typeof(string));

            DataRow row = dtTO.NewRow();
            row["ScheduleID"] = scheduleID;
            row["TglKirimActual"] = tglKirim;
            row["OPID"] = transferOrderID;
            row["PoliceCarNo"] = txtNoMobil.Text;
            row["DriverName"] = txtDriverName.Text;
            row["Keterangan"] = "";
            row["CreatedBy"] = txtCreatedBy.Text;
            dtTO.Rows.Add(row);
            dtTO.TableName = "sj_InsertNewSJto";

            cpdWebService = new WebReference_HO.Service1();
            if (suratJalanTO.ID > 0)
            {
                //strError = suratJalanProsessFacade.Update();
                clearForm();
            }
            else
            {
                //coba insert dulu aja nanti feedback suratJalan.ID; & suratJalanProsessFacade.SuratJalanNo;
                Session["dtCetakSJ"] = null;
                string strResult = cpdWebService.sjTO_InsertNewSJto(dtTO);

                if (int.Parse(strResult) > 0)
                {
                    btnSimpan.Disabled = true;

                    //cpdWebService = new WebReference_HO.Service1();
                    WebserviceFacade webServicePusat = new WebserviceFacade();
                    DataTable dtCetakSJ = cpdWebService.sjTO_CetakSJto(int.Parse(strResult));

                    string sjNo = dtCetakSJ.Rows[0]["SuratJalanNo"].ToString();
                    Session["sjid"] = int.Parse(strResult);
                    Session["dtCetakSJto"] = dtCetakSJ;
                }
            }


            //string strError = string.Empty;
            //SuratJalanProcessTOFacade suratJalanProsessTOFacade = new SuratJalanProcessTOFacade(suratJalanTO, arrSuratJalanDetailTO);

            //if (suratJalanTO.ID > 0)
            //{
            //    strError = suratJalanProsessTOFacade.Update();
            //    clearForm();
            //}
            //else
            //{
            //    strError = suratJalanProsessTOFacade.Insert();
            //    if (strError == string.Empty)
            //    {
            //        txtSuratJalanNo.Text = suratJalanProsessTOFacade.SuratJalanTONo;
            //        Session["sjid"] = suratJalanTO.ID;

            //        suratJalanProsessTOFacade = new SuratJalanProcessTOFacade(suratJalanTO, arrSuratJalanDetailTO);
            //        strError = suratJalanProsessTOFacade.PostingShipment();
            //    }
            //}

            //if (strError == string.Empty)
            //    InsertLog(strEvent);

        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            string strValidate = ValidateText();
            int flagSmsOutbox = 0;

            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }

            SuratJalanTO suratJalan = new SuratJalanTO();

            if (Session["sjid"] != null)
                suratJalan.ID = int.Parse(Session["sjid"].ToString());

            //24Agus2018
            //if (Session["stat"] != null)
            //{
            //    if (int.Parse(Session["stat"].ToString()) > 0)
            //    {
            //        DisplayAJAXMessage(this, "Status Surat Jalan sudah Shipment ");
            //        return;
            //    }
            //}
            //24Agus2018

            string strError = string.Empty;
            ArrayList arrSuratJalanDetail = new ArrayList();

            DataTable dt = new DataTable();
            dt.Columns.Add("SuratJalanID", typeof(int));
            dt.Columns.Add("ReceiveDate", typeof(DateTime));
            dt.Columns.Add("LastModifiedBy", typeof(string));

            DataRow row = dt.NewRow();
            row["SuratJalanID"] = suratJalan.ID;
            row["ReceiveDate"] = DateTime.Parse(txtActualKirim.Text);
            row["LastModifiedBy"] = txtCreatedBy.Text;
            dt.Rows.Add(row);
            dt.TableName = "sjTO_UpdatePostingDateSJ";

            WebReference_HO.Service1 cpdWebService = new WebReference_HO.Service1();
            string strResult = cpdWebService.sjTO_UpdatePostingDateSJ(dt, 1);

            if (int.Parse(strResult) > 0)
            {
                transferitem();
                if (ddlKendaraan.SelectedIndex > 0)
                    TransferLoadingTime();
                if (flagSmsOutbox > 0)
                    DisplayAJAXMessage(this, "Posting Shipment & Send Data ke SMS Outbox berhasil");
                else
                    DisplayAJAXMessage(this, "Posting Shipment berhasil");
                //InsertLog("Posting Shipment");
                clearForm();
            }

        }

        protected void btnPostingReceipt_ServerClick(object sender, EventArgs e)
        {
            //24Agus2018
            string strValidate = ValidateText();
            int flagSmsOutbox = 0;

            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }

            SuratJalanTO suratJalan = new SuratJalanTO();

            if (Session["sjid"] != null)
                suratJalan.ID = int.Parse(Session["sjid"].ToString());

            if (Session["stat"] != null)
            {
                if (int.Parse(Session["stat"].ToString()) > 1)
                {
                    DisplayAJAXMessage(this, "Status Surat Jalan sudah Receipt / Invoice");
                    return;
                }
            }

            string strError = string.Empty;
            ArrayList arrSuratJalanDetail = new ArrayList();

            DataTable dt = new DataTable();
            dt.Columns.Add("SuratJalanID", typeof(int));
            dt.Columns.Add("ReceiveDate", typeof(DateTime));
            dt.Columns.Add("LastModifiedBy", typeof(string));

            DataRow row = dt.NewRow();
            row["SuratJalanID"] = suratJalan.ID;
            row["ReceiveDate"] = DateTime.Now;
            row["LastModifiedBy"] = txtCreatedBy.Text;
            dt.Rows.Add(row);
            dt.TableName = "sjTO_UpdatePostingDateSJ2";

            WebReference_HO.Service1 cpdWebService = new WebReference_HO.Service1();
            string strResult = cpdWebService.sjTO_UpdatePostingDateSJ(dt, 2);

            if (int.Parse(strResult) > 0)
            {
                if (flagSmsOutbox > 0)
                    DisplayAJAXMessage(this, "Posting Receipt & Send Data ke SMS Outbox berhasil");
                else
                    DisplayAJAXMessage(this, "Posting Receipt berhasil");

                clearForm();
            }

            //24Agus2018
        }


        protected void btnCancel_ServerClick(object sender, EventArgs e)
        {
            string strValidate = ValidateText();
            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }

            SuratJalanTOFacade suratJalanTOFacade = new SuratJalanTOFacade();

            SuratJalanTO suratJalanTO = suratJalanTOFacade.RetrieveByNo(txtSuratJalanNo.Text);
            if (suratJalanTOFacade.Error == string.Empty)
            {
                if (suratJalanTO.ID > 0)
                {
                    string strError = string.Empty;
                    ArrayList arrSuratJalanDetail = new ArrayList();
                    if (Session["ListOfSuratJalanDetailTO"] != null)
                    {
                        ArrayList arrScheduleDetail = (ArrayList)Session["ListOfSuratJalanDetailTO"];
                        foreach (SuratJalanDetailTO scheduleDetail in arrScheduleDetail)
                        {
                            SuratJalanDetailTO suratJalanDetail = new SuratJalanDetailTO();
                            suratJalanDetail.ItemCode = scheduleDetail.ItemCode;
                            suratJalanDetail.ItemID = scheduleDetail.ItemID;
                            suratJalanDetail.ItemName = scheduleDetail.ItemName;
                            suratJalanDetail.Qty = scheduleDetail.Qty;
                            suratJalanDetail.ScheduleDetailId = scheduleDetail.ScheduleDetailId;
                            suratJalanDetail.Paket = scheduleDetail.Paket;
                            arrSuratJalanDetail.Add(suratJalanDetail);
                        }
                    }

                    SuratJalanProcessTOFacade suratJalanProsessTOFacade = new SuratJalanProcessTOFacade(suratJalanTO, arrSuratJalanDetail);

                    if (suratJalanTO.ID > 0)
                    {
                        strError = suratJalanProsessTOFacade.CancelSuratJalan();
                        if (strError == string.Empty)
                        {
                            InsertLog("Cancel Surat Jalan");
                            clearForm();
                        }
                    }
                }
            }
        }
        protected void btnPosting_ServerClick(object sender, EventArgs e)
        {
            transferitem();
        }
        private void GetScheduleDetail()
        {
            ArrayList arrScheduleTO = new ArrayList();
            WebReference_HO.Service1 cpdWebService = new WebReference_HO.Service1();
            DataSet dsScheduleOP = cpdWebService.sjTORetrieveByTONo(txtScheduleNo.Text, txtTransferOrderNo.Text);
            foreach (DataRow row in dsScheduleOP.Tables[0].Rows)
            {
                ScheduleDetail scheOP = new ScheduleDetail();

                scheOP.ID = int.Parse(row["ID"].ToString());
                scheOP.ScheduleID = int.Parse(row["ScheduleID"].ToString());
                scheOP.DocumentID = int.Parse(row["DocumentID"].ToString());
                scheOP.DocumentDetailID = int.Parse(row["DocumentDetailID"].ToString());
                scheOP.TypeDoc = int.Parse(row["TypeDoc"].ToString());
                scheOP.DocumentNo = row["DocumentNo"].ToString();
                scheOP.ItemID = int.Parse(row["ItemID"].ToString());
                scheOP.ItemCode = row["ItemCode"].ToString(); ;
                scheOP.ItemName = row["ItemName"].ToString();
                scheOP.KotaTujuan = row["KotaTujuan"].ToString();
                scheOP.AreaTujuan = row["AreaTujuan"].ToString();
                scheOP.Qty = int.Parse(row["Qty"].ToString());
                scheOP.TotalKubikasi = decimal.Parse(row["TotalKubikasi"].ToString());
                scheOP.Paket = int.Parse(row["Paket"].ToString());
                scheOP.DepoID = int.Parse(row["DepoID"].ToString());

                arrScheduleTO.Add(scheOP);
            }

            Session["ListOfSuratJalanDetailTO"] = arrScheduleTO;
            GridView1.DataSource = arrScheduleTO;
            GridView1.DataBind();
        }

        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Posting Shipment SJ TO";
            eventLog.EventName = eventName;
            eventLog.DocumentNo = txtSuratJalanNo.Text;
            eventLog.CreatedBy = ((Users)Session["Users"]).UserName;

            EventLogFacade eventLogFacade = new EventLogFacade();
            int intResult = eventLogFacade.Insert(eventLog);
        }


        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }


        private string ValidateText()
        {
            if (txtTransferOrderNo.Text == string.Empty)
                return "No TO tidak boleh kosong";
            else if (txtNoMobil.Text == string.Empty)
                return "No Mobil tidak boleh kosong";
            else if (txtDriverName.Text == string.Empty)
                return "Nama Supir harus diisi";

            return string.Empty;
        }
        protected void txtActualKirim_TextChanged(object sender, EventArgs e)
        {
            if (DateTime.Parse(txtActualKirim.Text) < DateTime.Parse(txtScheduleDate.Text))
            {
                DisplayAJAXMessage(this, "Tgl Kirim Aktual harus sama / lebih dari dari Tgl Kirim Schedule " + txtScheduleDate.Text);
                return;
            }
            LoadingByTglIn();
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
            bpas_api.WebService1 api = new bpas_api.WebService1();
            int IDSJ = 0;
            GridView grv = (GridView)GridView1.Rows[rowindex].FindControl("GridViewtrans");
            GridView grv0 = (GridView)GridView1.Rows[rowindex].FindControl("GridViewtrans0");
            Label lbl = (Label)GridView1.Rows[rowindex].FindControl("Label2");
            //GridView1.Rows[rowindex].FindControl("Cancel").Visible = false;
            ArrayList arrT3SiapKirim = new ArrayList();
            int luas = 0;
            int i = 0;
            T3_KirimDetailFacade T3KirimDetail = new T3_KirimDetailFacade();
            // 
            int konvDeco = 0;
            switch (e.CommandName)
            {
                case "tambah":
                    Panelmap.Visible = true;
                    lblIDSJ.Text = GridView1.Rows[rowindex].Cells[0].Text;
                    LblItemName.Text = GridView1.Rows[rowindex].Cells[2].Text;
                    break;
                case "Details":
                    GridView1.Rows[rowindex].FindControl("Cancel").Visible = true;
                    GridView1.Rows[rowindex].FindControl("transfer").Visible = true;
                    GridView1.Rows[rowindex].FindControl("btn_Show").Visible = false;

                    break;
                case "Cancel":
                    grv.Visible = false;
                    grv0.Visible = false;
                    GridView1.Rows[rowindex].FindControl("Cancel").Visible = false;
                    GridView1.Rows[rowindex].FindControl("transfer").Visible = false;
                    GridView1.Rows[rowindex].FindControl("btn_Show").Visible = true;
                    return;
                case "transfer":
                    //int intresult = 0;
                    // 
                    DataSet dsStrResult = api.GetItemsSJ(Int32.Parse(GridView1.Rows[rowindex].Cells[0].Text));
                    decimal tebal = decimal.Parse(dsStrResult.Tables[0].Rows[0]["tebal"].ToString());
                    int panjang = int.Parse(dsStrResult.Tables[0].Rows[0]["panjang"].ToString());
                    int lebar = int.Parse(dsStrResult.Tables[0].Rows[0]["lebar"].ToString());
                    int kirimID = 0;
                    if (txtSuratJalanNo.Text == string.Empty && txtSuratJalanNo.Text == string.Empty && txtSuratJalanNo.Text == string.Empty)
                    {
                        DisplayAJAXMessage(this, "Input Data belum lengkap");
                        return;
                    }
                    #region Verifikasi Closing Periode
                    /**
                     * check closing periode saat ini
                     * added on 19-08-2014
                     */
                    ClosingFacade Closing = new ClosingFacade();
                    int Tahun = DateTime.Parse(txtActualKirim.Text).Year;
                    int Bulan = DateTime.Parse(txtActualKirim.Text).Month;
                    int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
                    int clsStat = Closing.GetClosingStatus("SystemClosing");
                    if (status == 1 && clsStat == 1)
                    {
                        DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing. Transaksi Tidak bisa dilakukan");
                        return;
                    }
                    #endregion
                    T3_Kirim t3kirim = new T3_Kirim();
                    T3_KirimFacade t3kirimfacade = new T3_KirimFacade();
                    T3_SerahFacade SerahFacade = new T3_SerahFacade();
                    FC_LokasiFacade getlokasi = new FC_LokasiFacade();
                    FC_Lokasi fclokasi = new FC_Lokasi();
                    T3_Serah t3serah = new T3_Serah();
                    T3_Rekap rekap = new T3_Rekap();
                    fclokasi = getlokasi.RetrieveByLokasi("C00");
                    int lokid = fclokasi.ID;
                    Users users = (Users)Session["Users"];

                    T3_KirimDetailFacade T3_KirimDetailFacade = new T3_KirimDetailFacade();
                    T3_SiapKirimFacade t3siapkirimfacade = new T3_SiapKirimFacade();
                    //luas = Convert.ToInt32(GridView1.Rows[rowindex].Cells[3].Text) * Convert.ToInt32(GridView1.Rows[rowindex].Cells[4].Text);
                    ArrayList arrsiapkirim = new ArrayList();
                    //arrsiapkirim = t3siapkirimfacade.Retrievebyluas(luas, Convert.ToInt32(GridView1.Rows[rowindex].Cells[0].Text));
                    //decimal vVolume = decimal.Parse(GridView1.Rows[rowindex].Cells[2].Text) * Convert.ToInt32(GridView1.Rows[rowindex].Cells[3].Text) * Convert.ToInt32(GridView1.Rows[rowindex].Cells[4].Text);
                    //////if (ChkSJNo.Checked == false)
                    //////    arrT3SiapKirim = t3siapkirimfacade.RetrievebyVolume(Convert.ToInt32(vVolume), Convert.ToInt32(GridView1.Rows[rowindex].Cells[0].Text));
                    //////else
                    //arrT3SiapKirim = t3siapkirimfacade.RetrievebySJNo(txtSuratJalanNo.Text.Trim(), Convert.ToInt32(GridView1.Rows[rowindex].Cells[0].Text));
                    //if (Session["kirimID"] == null)
                    //{
                    arrT3SiapKirim = t3siapkirimfacade.RetrievebyVolumeNew(tebal, panjang, lebar, GridView1.Rows[rowindex].Cells[0].Text);
                    t3kirim.SJNo = txtSuratJalanNo.Text;
                    t3kirim.OPNo = txtTransferOrderNo.Text;
                    t3kirim.Customer = Session["customer"].ToString();//txtCustomer.Text;
                    t3kirim.TglKirim = DateTime.Parse(txtActualKirim.Text);
                    t3kirim.Total = 0;
                    t3kirim.CreatedBy = users.UserName;
                    //kirimID = t3kirimfacade.Insert(t3kirim);
                    Session["kirimID"] = kirimID;
                    //}
                    int totpotong = 0;
                    konvDeco = 0;
                    foreach (T3_SiapKirim t3siapkirim in arrT3SiapKirim)
                    {
                        TextBox txtQtyKirim = (TextBox)grv.Rows[i].FindControl("txtQtyKirim");
                        konvDeco = 0;
                        //if (chkDeco.Checked == true)
                        //    konvDeco = t3siapkirimfacade.GetKonversiDeco(Convert.ToInt32(grv.Rows[i].Cells[1].Text));
                        //else
                        konvDeco = 1;

                        if (txtQtyKirim.Text != string.Empty && Convert.ToInt32(grv.Rows[i].Cells[1].Text) > 0)
                        {
                            totpotong = totpotong + Convert.ToInt32(txtQtyKirim.Text);
                        }
                        i = i + 1;
                    }
                    int totalkirimdet = T3_KirimDetailFacade.RetrieveBySJQtyNew(txtSuratJalanNo.Text, GridView1.Rows[rowindex].Cells[0].Text);
                    if (totpotong + totalkirimdet != Convert.ToInt32(GridView1.Rows[rowindex].Cells[3].Text) * konvDeco)
                    {
                        DisplayAJAXMessage(this, "Qty potong tidak memenuhi Qty SJ");
                        grv.Visible = false;
                        GridView1.Rows[rowindex].FindControl("Cancel").Visible = false;
                        GridView1.Rows[rowindex].FindControl("transfer").Visible = false;
                        GridView1.Rows[rowindex].FindControl("btn_Show").Visible = true;
                        return;
                    }
                    i = 0;
                    //Session["ListofKirimDetail1"] = null;
                    ArrayList arrKirimDetail1 = new ArrayList();
                    ArrayList arrKirimDetail = new ArrayList();
                    foreach (T3_SiapKirim t3siapkirim in arrT3SiapKirim)
                    {
                        TextBox txtQtyKirim = (TextBox)grv.Rows[i].FindControl("txtQtyKirim");
                        if (txtQtyKirim.Text != string.Empty && Convert.ToInt32(grv.Rows[i].Cells[1].Text) > 0)
                        {
                            if (Session["ListofKirimDetail"] != null)
                            {
                                arrKirimDetail = (ArrayList)Session["ListofKirimDetail"];
                            }
                            t3serah.Flag = "kurang";
                            t3serah.ItemID = t3siapkirim.ItemIDSer;
                            t3serah.GroupID = t3siapkirim.GroupID;
                            t3serah.ID = t3siapkirim.SerahID;
                            fclokasi = getlokasi.RetrieveByLokasi(t3siapkirim.LokasiKrm);
                            int lokidser = fclokasi.ID;
                            t3serah.LokID = lokidser;
                            int stock = 0;
                            stock = SerahFacade.GetStock(lokidser, t3siapkirim.ItemIDSer);
                            string jmlstock = "cukup";
                            if (stock - Convert.ToInt32(txtQtyKirim.Text) < 0)
                            {
                                //DisplayAJAXMessage(this, "Stock tidak mencukupi, proses dibatalkan !");
                                //return;
                                jmlstock = "tidak cukup";
                            }
                            //t3serah.Qty = Convert.ToInt32(txtQtyKirim.Text);
                            //t3serah.CreatedBy = users.UserName;
                            ////intresult = SerahFacade.Insert(t3serah);
                            if (jmlstock == "cukup")
                            {
                                T3_KirimDetail t3KirimDetail = new T3_KirimDetail();
                                t3KirimDetail.T3siapKirimID = t3siapkirim.ID;
                                //t3KirimDetail.KirimID = Convert.ToInt32(Session["kirimID"].ToString()) ;
                                t3KirimDetail.SerahID = t3siapkirim.SerahID;
                                t3KirimDetail.LokasiID = lokid;
                                t3KirimDetail.LokasiLoadingID = lokidser;
                                t3KirimDetail.ItemIDSer = t3siapkirim.ItemIDSer;
                                t3KirimDetail.Tgltrans = DateTime.Parse(txtActualKirim.Text);
                                t3KirimDetail.GroupID = t3siapkirim.GroupID;
                                t3KirimDetail.Qty = Convert.ToInt32(txtQtyKirim.Text);
                                t3KirimDetail.HPP = t3siapkirim.HPP;
                                t3KirimDetail.CreatedBy = users.UserName;
                                t3KirimDetail.ItemIDSJ = Convert.ToInt32(GridView1.Rows[rowindex].Cells[0].Text);
                                arrKirimDetail1.Add(t3KirimDetail);
                            }
                            //Session["ListofKirimDetail1"] = arrKirimDetail;
                            //intresult = T3_KirimDetailFacade.Insert(t3KirimDetail);
                            //t3siapkirimfacade.UpdatebyKirim(t3siapkirim.ID, Convert.ToInt32(txtQtyKirim.Text), users.UserName);
                        }
                        i = i + 1;
                    }

                    //arrKirimDetail = (ArrayList)Session["ListofKirimDetail1"];
                    T3_KirimProcessFacade SimetrisProcessFacade = new T3_KirimProcessFacade(t3kirim, arrKirimDetail1);
                    string strError = SimetrisProcessFacade.Insert();
                    if (strError == string.Empty)
                    {
                        grv.Visible = false;
                        GridView1.Rows[rowindex].FindControl("Cancel").Visible = false;
                        GridView1.Rows[rowindex].FindControl("transfer").Visible = false;
                        GridView1.Rows[rowindex].FindControl("btn_Show").Visible = true;
                        //LoadDataGridView1();
                        //LoadDataGridViewKirim();
                    }
                    break;
            }
        }
        public void LoadKendaraan()
        {
            ArrayList arrKendaraan = new ArrayList();
            MasterKendaraanFacade kendaraanFacade = new MasterKendaraanFacade();
            arrKendaraan = kendaraanFacade.Retrieve();
            MasterKendaraan masterKendaraan = new MasterKendaraan();
            // masterKendaraan = kendaraanFacade.RetrieveById(int.Parse(ddlKendaraan.SelectedValue));
            if (arrKendaraan.Count > 0)
            {
                ddlKendaraan.Items.Clear();
                ddlKendaraan.Items.Add(new ListItem("-- Pilih Kendaraan --", string.Empty));
                //foreach (MasterKendaraan kendaraan in arrKendaraan)
                foreach (MasterKendaraan mobil in arrKendaraan)
                {
                    //ddlKendaraan.Items.Add(new ListItem(kendaraan.JenisMobil.Trim(), kendaraan.ID.ToString()));
                    ddlKendaraan.Items.Add(new ListItem(mobil.JenisMobil.Trim(), mobil.ID.ToString()));
                }
            }
            if (kendaraanFacade.Error == string.Empty)
            {
                //if (kendaraan.ID > 0)
                if (masterKendaraan.ID > 0)
                {

                    //txtStok.Text = inventory.Jumlah.ToString("N2");
                    txtUrutanOut.Text = masterKendaraan.Target.ToString("N2");
                    //ddlKendaraan.Items = masterKendaraan.JenisMobil.ToString(); 

                }
            }
        }
        protected void chkDeco_CheckedChanged(object sender, EventArgs e)
        {
            LoadSuratJalan(txtSuratJalanNo.Text);
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (txtSuratJalanNo.Text.Trim() == string.Empty)
                return;
            bpas_api.WebService1 api = new bpas_api.WebService1();
            T3_KirimDetailFacade T3_KirimDetailFacade = new T3_KirimDetailFacade();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //try
                //{
                int rowindex = Convert.ToInt32(e.Row.RowIndex.ToString());
                GridView grv = (GridView)e.Row.FindControl("GridViewtrans");
                GridView grv0 = (GridView)e.Row.FindControl("GridViewtrans0");
                Label lbl = (Label)e.Row.FindControl("Label2");
                //e.Row.FindControl("Cancel").Visible = false;

                //e.Row.FindControl("Cancel").Visible = false;
                //e.Row.FindControl("transfer").Visible = true;
                //e.Row.FindControl("btn_Show").Visible = false;
                DataSet dsStrResult = api.GetItemsSJ(Int32.Parse(e.Row.Cells[0].Text));
                decimal tebal = decimal.Parse(dsStrResult.Tables[0].Rows[0]["tebal"].ToString());
                int panjang = int.Parse(dsStrResult.Tables[0].Rows[0]["panjang"].ToString());
                int lebar = int.Parse(dsStrResult.Tables[0].Rows[0]["lebar"].ToString());
                ArrayList arrT3SiapKirim = new ArrayList();
                T3_SiapKirimFacade T3_SiapKirimFacade = new T3_SiapKirimFacade();
                T3_KirimDetailFacade T3KirimDetail = new T3_KirimDetailFacade();
                int qtyP = 0;
                qtyP = T3KirimDetail.RetrieveBySJQtyNew(txtSuratJalanNo.Text, e.Row.Cells[0].Text);
                int qtySJ = Convert.ToInt32((e.Row.Cells[3].Text)) - qtyP;
                //e.Row.Cells[3].Text = qtyP.ToString();

                //int luas = Convert.ToInt32(e.Row.Cells[3].Text) * Convert.ToInt32(e.Row.Cells[4].Text);
                //Decimal Volume = Convert.ToDecimal(e.Row.Cells[2].Text) * Convert.ToInt32(e.Row.Cells[3].Text) * Convert.ToInt32(e.Row.Cells[4].Text);
                int totalkirimdet = T3_KirimDetailFacade.RetrieveBySJQtyNew(txtSuratJalanNo.Text, e.Row.Cells[0].Text);
                if (Convert.ToInt32((e.Row.Cells[3].Text)) > totalkirimdet)
                {
                    arrT3SiapKirim = T3_SiapKirimFacade.RetrievebyVolumeNew(tebal, panjang, lebar, e.Row.Cells[0].Text);
                    //if (ChkSJNo.Checked == false)
                    //    arrT3SiapKirim = T3_SiapKirimFacade.RetrievebyVolume(Convert.ToInt32(Volume), Convert.ToInt32(e.Row.Cells[0].Text));
                    //else
                    //arrT3SiapKirim = T3_SiapKirimFacade.RetrievebySJNo(txtSuratJalanNo.Text.Trim(), Convert.ToInt32(e.Row.Cells[0].Text));
                    Session["arrsiapkirim"] = arrT3SiapKirim;
                    grv.DataSource = arrT3SiapKirim;
                    grv.DataBind();
                    grv.Visible = true;
                    for (int gd = 0; gd < grv.Rows.Count; gd++)
                    {
                        DropDownList ddlPengiriman = (DropDownList)grv.Rows[gd].FindControl("ddlPengiriman");
                        ZetroView zl = new ZetroView();
                        ArrayList arrlistkirim = new ArrayList();
                        zl.QueryType = Operation.CUSTOM;
                        zl.CustomQuery =
                            "select * from MaterialCCMatrixPJPengiriman where rowstatus>-1 and pengiriman in (select pengiriman from MaterialCCMatrixPJ where tahun=" +
                            DateTime.Now.Year + " and pengiriman not like '%kembali%') order by urutan";
                        SqlDataReader sdr = zl.Retrieve();
                        DataTable dt = new DataTable();
                        dt.Columns.Add("Pengiriman", typeof(string));
                        DataRow row = dt.NewRow();
                        row["Pengiriman"] = "-";
                        dt.Rows.Add(row);
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                row = dt.NewRow();
                                row["Pengiriman"] = sdr["Pengiriman"].ToString();
                                dt.Rows.Add(row);
                            }
                        }
                        ddlPengiriman.DataSource = dt;
                        ddlPengiriman.DataTextField = "Pengiriman";
                        ddlPengiriman.DataValueField = "Pengiriman";
                        ddlPengiriman.DataBind();
                    }
                    int i = 0;
                    int konvDeco = 0;
                    foreach (T3_SiapKirim t3siapkirim in arrT3SiapKirim)
                    {
                        TextBox txtQtyKirim = (TextBox)grv.Rows[i].FindControl("txtQtyKirim");
                        if (chkDeco.Checked == true)
                            konvDeco = T3_SiapKirimFacade.GetKonversiDeco(Convert.ToInt32(grv.Rows[i].Cells[1].Text));
                        else
                            konvDeco = 1;
                        if (Convert.ToInt32(grv.Rows[i].Cells[7].Text) - qtySJ >= 0)
                        {
                            txtQtyKirim.Text = (qtySJ * konvDeco).ToString();
                            break;
                        }
                        else
                        {
                            txtQtyKirim.Text = grv.Rows[i].Cells[7].Text;
                            qtySJ = qtySJ - Convert.ToInt32(grv.Rows[i].Cells[7].Text);
                        }
                        i = i + 1;
                    }
                    //e.Row.FindControl("transfer").Visible = true;
                }
                else
                {
                    ArrayList arrT3KirimDetail = new ArrayList();
                    arrT3KirimDetail = T3KirimDetail.RetrieveBySJ(txtSuratJalanNo.Text, e.Row.Cells[0].Text, e.Row.Cells[3].Text);
                    grv0.DataSource = arrT3KirimDetail;
                    grv0.DataBind();
                    grv0.Visible = true;
                    // e.Row.FindControl("transfer").Visible = false;
                }
                //}
                //catch { }
            }

        }
        protected void transferitem()
        {
            bpas_api.WebService1 api = new bpas_api.WebService1();
            int IDSJ = 0;
            int konvDeco = 0;
            ArrayList arrT3SiapKirim = new ArrayList();
            int luas = 0;
            int i = 0;
            T3_KirimDetailFacade T3KirimDetail = new T3_KirimDetailFacade();
            for (int j = 0; j <= GridView1.Rows.Count - 1; j++)
            {
                GridView grv = (GridView)GridView1.Rows[j].FindControl("GridViewtrans");
                GridView grv0 = (GridView)GridView1.Rows[j].FindControl("GridViewtrans0");
                Label lbl = (Label)GridView1.Rows[j].FindControl("Label2");
                // GridView1.Rows[j].FindControl("Cancel").Visible = false;
                DataSet dsStrResult = api.GetItemsSJ(Int32.Parse(GridView1.Rows[j].Cells[0].Text));
                decimal tebal = decimal.Parse(dsStrResult.Tables[0].Rows[0]["tebal"].ToString());
                int panjang = int.Parse(dsStrResult.Tables[0].Rows[0]["panjang"].ToString());
                int lebar = int.Parse(dsStrResult.Tables[0].Rows[0]["lebar"].ToString());
                int kirimID = 0;
                if (txtSuratJalanNo.Text == string.Empty && txtSuratJalanNo.Text == string.Empty && txtSuratJalanNo.Text == string.Empty)
                {
                    DisplayAJAXMessage(this, "Input Data belum lengkap");
                    return;
                }
                #region Verifikasi Closing Periode
                /**
                     * check closing periode saat ini
                     * added on 19-08-2014
                     */
                ClosingFacade Closing = new ClosingFacade();
                int Tahun = DateTime.Parse(txtActualKirim.Text).Year;
                int Bulan = DateTime.Parse(txtActualKirim.Text).Month;
                int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
                int clsStat = Closing.GetClosingStatus("SystemClosing");
                if (status == 1 && clsStat == 1)
                {
                    DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing. Transaksi Tidak bisa dilakukan");
                    return;
                }
                #endregion
                T3_Kirim t3kirim = new T3_Kirim();
                T3_KirimFacade t3kirimfacade = new T3_KirimFacade();
                T3_SerahFacade SerahFacade = new T3_SerahFacade();
                FC_LokasiFacade getlokasi = new FC_LokasiFacade();
                FC_Lokasi fclokasi = new FC_Lokasi();
                T3_Serah t3serah = new T3_Serah();
                T3_Rekap rekap = new T3_Rekap();
                fclokasi = getlokasi.RetrieveByLokasi("C00");
                int lokid = fclokasi.ID;
                Users users = (Users)Session["Users"];

                T3_KirimDetailFacade T3_KirimDetailFacade = new T3_KirimDetailFacade();
                T3_SiapKirimFacade t3siapkirimfacade = new T3_SiapKirimFacade();
                ArrayList arrsiapkirim = new ArrayList();
                arrT3SiapKirim = t3siapkirimfacade.RetrievebyVolumeNew(tebal, panjang, lebar, GridView1.Rows[j].Cells[0].Text);
                t3kirim.SJNo = txtSuratJalanNo.Text;
                t3kirim.OPNo = txtTransferOrderNo.Text;
                t3kirim.Customer = Session["customer"].ToString();//txtCustomer.Text;
                t3kirim.TglKirim = DateTime.Parse(txtActualKirim.Text);
                t3kirim.Total = 0;
                t3kirim.CreatedBy = users.UserName;
                //kirimID = t3kirimfacade.Insert(t3kirim);
                Session["kirimID"] = kirimID;
                //}
                int totpotong = 0;
                konvDeco = 0;
                int totalkirimdet = T3_KirimDetailFacade.RetrieveBySJQtyNew(txtSuratJalanNo.Text, GridView1.Rows[j].Cells[0].Text);
                if (Convert.ToInt32((GridView1.Rows[j].Cells[3].Text)) > totalkirimdet)
                {
                    i = 0;
                    foreach (T3_SiapKirim t3siapkirim in arrT3SiapKirim)
                    {
                        TextBox txtQtyKirim = (TextBox)grv.Rows[i].FindControl("txtQtyKirim");
                        konvDeco = 0;
                        if (chkDeco.Checked == true)
                            konvDeco = t3siapkirimfacade.GetKonversiDeco(Convert.ToInt32(grv.Rows[i].Cells[1].Text));
                        else
                            konvDeco = 1;

                        if (txtQtyKirim.Text != string.Empty && Convert.ToInt32(grv.Rows[i].Cells[1].Text) > 0)
                        {
                            totpotong = totpotong + Convert.ToInt32(txtQtyKirim.Text);
                        }
                        i = i + 1;
                    }

                    if (totpotong + totalkirimdet != Convert.ToInt32(GridView1.Rows[j].Cells[3].Text) * konvDeco)
                    {
                        //DisplayAJAXMessage(this, "Qty potong tidak memenuhi Qty SJ");
                        //grv.Visible = false;
                        //GridView1.Rows[j].FindControl("Cancel").Visible = false;
                        //GridView1.Rows[j].FindControl("transfer").Visible = false;
                        //GridView1.Rows[j].FindControl("btn_Show").Visible = true;
                        //return;
                    }
                    i = 0;
                    //Session["ListofKirimDetail1"] = null;
                    ArrayList arrKirimDetail1 = new ArrayList();
                    ArrayList arrKirimDetail = new ArrayList();
                    foreach (T3_SiapKirim t3siapkirim in arrT3SiapKirim)
                    {
                        TextBox txtQtyKirim = (TextBox)grv.Rows[i].FindControl("txtQtyKirim");
                        DropDownList ddlPengiriman = (DropDownList)grv.Rows[i].FindControl("ddlPengiriman");
                        DropDownList ddljenispalet = (DropDownList)grv.Rows[i].FindControl("ddljenispalet");
                        TextBox txtjmlpalet = (TextBox)grv.Rows[i].FindControl("txtJumlah");
                        if (txtQtyKirim.Text != string.Empty && Convert.ToInt32(grv.Rows[i].Cells[1].Text) > 0)
                        {
                            if (Session["ListofKirimDetail"] != null)
                            {
                                arrKirimDetail = (ArrayList)Session["ListofKirimDetail"];
                            }
                            t3serah.Flag = "kurang";
                            t3serah.ItemID = t3siapkirim.ItemIDSer;
                            t3serah.GroupID = t3siapkirim.GroupID;
                            t3serah.ID = t3siapkirim.SerahID;
                            fclokasi = getlokasi.RetrieveByLokasi(t3siapkirim.LokasiKrm);
                            int lokidser = fclokasi.ID;
                            t3serah.LokID = lokidser;
                            int stock = 0;
                            stock = SerahFacade.GetStock(lokidser, t3siapkirim.ItemIDSer);
                            string jmlstock = "cukup";
                            if (stock - Convert.ToInt32(txtQtyKirim.Text) < 0)
                            {
                                //DisplayAJAXMessage(this, "Stock tidak mencukupi, proses dibatalkan !");
                                //return;
                                jmlstock = "tidak cukup";
                            }
                            //if (stock - Convert.ToInt32(txtQtyKirim.Text) < 0)
                            //{
                            //    DisplayAJAXMessage(this, "Stock tidak mencukupi, proses dibatalkan !");
                            //    return;
                            //}
                            //t3serah.Qty = Convert.ToInt32(txtQtyKirim.Text);
                            //t3serah.CreatedBy = users.UserName;
                            ////intresult = SerahFacade.Insert(t3serah);
                            if (jmlstock == "cukup")
                            {
                                T3_KirimDetail t3KirimDetail = new T3_KirimDetail();
                                t3KirimDetail.T3siapKirimID = t3siapkirim.ID;
                                //t3KirimDetail.KirimID = Convert.ToInt32(Session["kirimID"].ToString()) ;
                                t3KirimDetail.SerahID = t3siapkirim.SerahID;
                                t3KirimDetail.LokasiID = lokid;
                                t3KirimDetail.LokasiLoadingID = lokidser;
                                t3KirimDetail.ItemIDSer = t3siapkirim.ItemIDSer;
                                t3KirimDetail.Tgltrans = DateTime.Parse(txtActualKirim.Text);
                                t3KirimDetail.GroupID = t3siapkirim.GroupID;
                                t3KirimDetail.Qty = Convert.ToInt32(txtQtyKirim.Text);
                                t3KirimDetail.HPP = t3siapkirim.HPP;
                                t3KirimDetail.CreatedBy = users.UserName;
                                t3KirimDetail.ItemIDSJ = Convert.ToInt32(GridView1.Rows[j].Cells[0].Text);
                                t3KirimDetail.Pengiriman = ddlPengiriman.SelectedValue;
                                t3KirimDetail.JenisPalet = ddljenispalet.SelectedValue;
                                t3KirimDetail.JmlPalet = Convert.ToInt32(txtjmlpalet.Text);
                                arrKirimDetail1.Add(t3KirimDetail);
                            }
                            //Session["ListofKirimDetail1"] = arrKirimDetail;
                            //intresult = T3_KirimDetailFacade.Insert(t3KirimDetail);
                            //t3siapkirimfacade.UpdatebyKirim(t3siapkirim.ID, Convert.ToInt32(txtQtyKirim.Text), users.UserName);
                        }
                        i = i + 1;
                    }

                    //arrKirimDetail = (ArrayList)Session["ListofKirimDetail1"];
                    T3_KirimProcessFacade SimetrisProcessFacade = new T3_KirimProcessFacade(t3kirim, arrKirimDetail1);
                    string strError = SimetrisProcessFacade.Insert1();
                    if (strError == string.Empty)
                    {
                        grv.Visible = false;
                        ArrayList arrT3KirimDetail = new ArrayList();
                        arrT3KirimDetail = T3KirimDetail.RetrieveBySJ(txtSuratJalanNo.Text, GridView1.Rows[j].Cells[0].Text, GridView1.Rows[j].Cells[3].Text);
                        grv0.DataSource = arrT3KirimDetail;
                        grv0.DataBind();
                        grv0.Visible = true;
                    }
                }
            }
        }
        private void LoadDataGridLoading(ArrayList arrLoading)
        {
            this.GridView2.DataSource = arrLoading;
            this.GridView2.DataBind();
        }
        private ArrayList LoadGridLoading()
        {
            ArrayList arrLoading = new ArrayList();
            LoadingTimeFacade LoadingFacade = new LoadingTimeFacade();
            arrLoading = LoadingFacade.RetrieveAll3("");

            if (arrLoading.Count > 0)
            {
                return arrLoading;
            }

            arrLoading.Add(new LoadingTime());
            return arrLoading;
        }

        protected void ddlKendaraan_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlKendaraan.SelectedIndex > 0)
            //{
            //    txtNoPol.Focus();
            //}
        }
        public void GetDetailKendaraan(string NoPol)
        {
            //bpas_api.WebService1 api = new bpas_api.WebService1();
            Global2 api = new Global2();
            DataSet arrKend = new DataSet();
            arrKend = api.DetailKendaraan(NoPol);
            foreach (DataRow rw in arrKend.Tables[0].Rows)
            {

            }
        }
        public string GetIP(string Section)
        {
            var Conf = new Inifile(Server.MapPath("~/App_Data/GroupArmadaOnly.ini"));
            return Conf.Read(Section, "LoadingTime");
        }
        protected void txtUrutanNo_TextChanged(object sender, EventArgs e)
        {
            ArrayList arrLd = new ArrayList();
            LoadingTimeFacade ld = new LoadingTimeFacade();
            arrLd = ld.RetrieveByNoUrut3New(txtUrutanNo.Text, DateTime.Parse(txtActualKirim.Text).ToString("yyyyMMdd"));
            GridView2.DataSource = arrLd;
            GridView2.DataBind();
            txtUrutanOut.Text = txtUrutanNo.Text;

        }
        protected void LoadingByTglIn()
        {
            ArrayList arrLd = new ArrayList();
            LoadingTimeFacade ld = new LoadingTimeFacade();
            arrLd = ld.RetrieveByTglIn(DateTime.Parse(txtActualKirim.Text).ToString("yyyyMMdd"));
            GridView2.DataSource = arrLd;
            GridView2.DataBind();

        }
        protected void LoadingTimeBySJ()
        {
            ArrayList arrLd = new ArrayList();
            LoadingTimeFacade ld = new LoadingTimeFacade();
            arrLd = ld.RetrieveByNoSJ(txtSuratJalanNo.Text);
            if (arrLd.Count == 0)
            {
                arrLd = ld.RetrieveByTglIn(DateTime.Parse(txtActualKirim.Text).ToString("yyyyMMdd"));
            }
            GridView2.DataSource = arrLd;
            GridView2.DataBind();
            if (arrLd.Count > 0)
            {
                Session["ldid"] = int.Parse(GridView2.Rows[0].Cells[0].Text);
                txtUrutanNo.Text = GridView2.Rows[0].Cells[1].Text;
                txtUrutanOut.Text = GridView2.Rows[0].Cells[1].Text;
                txtTglIn.Text = DateTime.Parse(GridView2.Rows[0].Cells[2].Text).ToString("dd-MMM-yyyy HH:mm:ss");
                txtTglOut.Text = (GridView2.Rows[0].Cells[3].Text == string.Empty) ? DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss") : DateTime.Parse(GridView2.Rows[0].Cells[3].Text).ToString("dd-MMM-yyyy HH:mm:ss");
                txtCardID.Text = GridView2.Rows[0].Cells[0].Text;
                //txtSJno.Text = (row.Cells[7].Text == "&nbsp;") ? string.Empty : row.Cells[7].Text.TrimEnd();
                //txtNoPol.Text = (row.Cells[6].Text == "&nbsp;") ? string.Empty : row.Cells[6].Text.TrimEnd();
                ddlKendaraan.SelectedItem.Text = (GridView2.Rows[0].Cells[4].Text == "&nbsp;") ? "--Pilih Kendaraan--" : GridView2.Rows[0].Cells[4].Text.ToString().TrimEnd();
                string jm = (GridView2.Rows[0].Cells[5].Text == "&nbsp;") ? "0" : GridView2.Rows[0].Cells[5].Text.ToString();
                rbMblSendiri.Checked = (int.Parse(jm) == 0) ? true : false;
                rbMblLuar.Checked = (int.Parse(jm) == 0) ? false : true;
            }
        }
        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            //if (txtSearch.Text == string.Empty)
            //    LoadDataGridLoading(LoadGridLoading());
            //else
            //    LoadDataGridLoading(LoadGridByCriteria());
        }
        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Cells[0].Text = DateTime.Parse(e.Row.Cells[0].Text).ToString("dd-MMM-yyyy");
                //e.Row.Cells[6].Text = Decimal.Parse(e.Row.Cells[6].Text).ToString("N2");

                if (DateTime.Parse((e.Row.Cells[2].Text).ToString()) == DateTime.MinValue)
                    e.Row.Cells[2].Text = "";
                else
                    e.Row.Cells[2].Text = DateTime.Parse(e.Row.Cells[2].Text).ToString();
                if (DateTime.Parse((e.Row.Cells[3].Text).ToString()) == DateTime.MinValue)
                    e.Row.Cells[3].Text = "";
                else
                    e.Row.Cells[3].Text = DateTime.Parse(e.Row.Cells[3].Text).ToString();

            }
        }
        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Add")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView2.Rows[index];

                if (row.Cells[2].Text == string.Empty)
                {
                    DisplayAJAXMessage(this, "Belum Input Jam Masuk Loading Time ");
                    return;
                }

                Session["ldid"] = int.Parse(row.Cells[0].Text);
                txtUrutanNo.Text = row.Cells[1].Text;
                txtUrutanOut.Text = row.Cells[1].Text;
                txtTglIn.Text = DateTime.Parse(row.Cells[2].Text).ToString("dd-MMM-yyyy HH:mm:ss");
                txtTglOut.Text = (row.Cells[3].Text == string.Empty) ? DateTime.Parse(row.Cells[2].Text).ToString("dd-MMM-yyyy HH:mm:ss") : DateTime.Parse(row.Cells[3].Text).ToString("dd-MMM-yyyy HH:mm:ss");
                txtCardID.Text = row.Cells[0].Text;
                //txtSJno.Text = (row.Cells[7].Text == "&nbsp;") ? string.Empty : row.Cells[7].Text.TrimEnd();
                //txtNoPol.Text = (row.Cells[6].Text == "&nbsp;") ? string.Empty : row.Cells[6].Text.TrimEnd();
                ddlKendaraan.SelectedItem.Text = (row.Cells[4].Text == "&nbsp;") ? "--Pilih Kendaraan--" : row.Cells[4].Text.ToString().TrimEnd();
                string jm = (row.Cells[5].Text == "&nbsp;") ? "0" : row.Cells[5].Text.ToString();
                rbMblSendiri.Checked = (int.Parse(jm) == 0) ? true : false;
                rbMblLuar.Checked = (int.Parse(jm) == 0) ? false : true;
                DateTime test = DateTime.Parse(txtTglOut.Text);
                string test1 = test.ToString("yyyyMMdd HH:mm:ss");
            }


        }
        protected void TransferLoadingTime()
        {
            if (txtScheduleNo.Text.Trim() != string.Empty)
            {
                int ada = 0;
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery =
                    "select count(id) ada from loadingtime where isnull(kendaraanid,0)>0 and keterangan='" + txtScheduleNo.Text.Trim() + "'";
                SqlDataReader sdr1 = zl1.Retrieve();
                if (sdr1.HasRows)
                {
                    while (sdr1.Read())
                    {
                        ada = Convert.ToInt32(sdr1["ada"].ToString().Trim());
                        //rit = sdr1["rate"].ToString().Trim();
                    }
                }
                if (ada > 0)
                {
                    return;
                }
            }
            else
                return;
            string strValidate = ValidateText();
            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                txtUrutanOut.Text = string.Empty;
                return;
            }

            string strEvent = "Insert";

            LoadingTime Loading = new LoadingTime();
            LoadingTimeFacade LoadingFacade = new LoadingTimeFacade();

            int nobaru = 0;
            string strNo = string.Empty;
            if (Session["ldid"] != null)
            {
                Loading.ID = int.Parse(Session["ldid"].ToString());
            }
            else
            {
                Loading.ID = 0;
                //DisplayAJAXMessage(this, "Pilih Loading ID dahulu  ");
                //return;
            }

            /** tambahan pilihan tujuan kirim **/
            string tkirim = string.Empty;
            if (RbDalam.Checked == true)
            {
                tkirim = "DPulau";
            }
            else if (RbLuar.Checked == true)
            {
                tkirim = "LPulau";
            }
            else
            {
                tkirim = "-";
            }
            /** end tambahan pilihan tujuan kirim **/

            LoadingTime CekLoading = LoadingFacade.RetrieveByNoUrut(txtUrutanNo.Text);
            if (CekLoading.ID > 0 && CekLoading.TimeOut == DateTime.MinValue)
                Loading.Ritase = CekLoading.Ritase + 1;
            else
                Loading.Ritase = 1;

            /** tambahan pilihan tujuan kirim **/
            //Loading.TKirim = tkirim;
            /** end tambahan pilihan tujuan kirim **/
            //Loading.CardID = txtCardID.Text;
            Loading.NoUrut = txtUrutanNo.Text;
            Loading.Tanggal = DateTime.Parse(txtTglIn.Text);
            Loading.TglOut = DateTime.Parse(txtTglOut.Text);
            Loading.TimeOut = DateTime.Parse(txtTglOut.Text);
            Loading.TglIn = DateTime.Parse(txtTglIn.Text);
            Loading.TimeIn = DateTime.Parse(txtTglIn.Text);
            Loading.NoPolisi = txtNoMobil.Text;
            Loading.KendaraanID = int.Parse(ddlKendaraan.SelectedValue.ToString());
            Loading.JenisMobil = ddlKendaraan.SelectedValue.ToString();
            Loading.Status = 1;
            Loading.CreatedBy = ((Users)Session["Users"]).UserName;
            if (rbMblLuar.Checked == true && rbMblSendiri.Checked == false)
                Loading.MobilSendiri = 1;
            if (rbMblLuar.Checked == false && rbMblSendiri.Checked == true)
                Loading.MobilSendiri = 0;
            Loading.Keterangan = txtSuratJalanNo.Text;

            Loading.TKirim = tkirim;

            string strError = string.Empty;
            int intResult = 0;

            if (Loading.ID > 0)
            {
                intResult = LoadingFacade.UpdateLoadingtimeBySJ1(Loading);
            }
            //else
            //    intResult = LoadingFacade.Insert(Loading);


            if (intResult >= 0)
            {
                //txtNoLoad.Text = nobaru.ToString().PadLeft(6, '0');
                LoadDataGridLoading(LoadGridLoading());
                //InsertLog(strEvent);
                DisplayAJAXMessage(this, "Data loading Tersimpan ");
                clearForm();
            }
        }
        protected void btnCancelKirim_ServerClick(object sender, EventArgs e)
        {
            if (txtSuratJalanNo.Text != string.Empty)
            {
                #region Verifikasi Closing Periode
                /**
             * check closing periode saat ini
             * added on 13-08-2014
             */
                ClosingFacade Closing = new ClosingFacade();
                int Tahun = DateTime.Parse(txtCreateDate.Text).Year;
                int Bulan = DateTime.Parse(txtCreateDate.Text).Month;
                int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
                int clsStat = Closing.GetClosingStatus("SystemClosing");
                if (status == 1 && clsStat == 1)
                {
                    DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing. Transaksi Tidak bisa dilakukan");
                    return;
                }
                #endregion
                CancelSJ(txtSuratJalanNo.Text);
            }

        }
        private string CancelSJ(string SJNo)
        {
            string result = string.Empty;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;


            zl.CustomQuery = "declare @nosj varchar(max) " +
                "set @nosj='" + SJNo + "' " +
                "update T3_Kirim set RowStatus=-1 where SJNo =@nosj " +
                "update T3_KirimDetail set RowStatus=-1 where KirimID in (select ID from T3_Kirim where SJNo =@nosj) " +
                "update T3_Serah set qty=qty+(select sum(Qty) from T3_KirimDetail where ItemID=T3_Serah.ItemID and KirimID in " +
                "(select ID from T3_Kirim where SJNo=@nosj)) " +
                "where ID in (select SerahID  from  T3_KirimDetail  where KirimID in (select ID from T3_Kirim where SJNo =@nosj))";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = sdr["pesan"].ToString().Trim();
                }
            }
            return result;
        }
        protected void btnUpdate0_ServerClick(object sender, EventArgs e)
        {
            TransferLoadingTime();
        }
        protected void ddlPengirimanE_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadJenisPalet(ddlPengiriman.SelectedValue);
            DropDownList ddlPengiriman = (DropDownList)sender;
            string Pengiriman = ddlPengirimanE.SelectedValue;

            ZetroView zl = new ZetroView();
            ArrayList arrlistkirim = new ArrayList();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "select distinct jenisPacking from MaterialCCMatrixPJ where rowstatus>-1 and pengiriman='" + Pengiriman + "' and tahun=" +
                DateTime.Now.Year + " order by jenisPacking";
            SqlDataReader sdr = zl.Retrieve();
            DataTable dt = new DataTable();
            dt.Columns.Add("jenisPacking", typeof(string));
            DataRow row = dt.NewRow();
            row["jenisPacking"] = "-";
            dt.Rows.Add(row);
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    row = dt.NewRow();
                    row["jenisPacking"] = sdr["jenisPacking"].ToString();
                    dt.Rows.Add(row);
                }
            }
            ddlJenisPaletE.DataSource = dt;
            ddlJenisPaletE.DataValueField = "jenisPacking";
            ddlJenisPaletE.DataTextField = "jenisPacking";
            ddlJenisPaletE.DataBind();
        }
        protected void GridViewtrans0_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "tambah":
                    PanelPalet.Visible = true;

                    int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
                    GridView TranstoCon = sender as GridView;
                    GridViewRow parentRow = (GridViewRow)TranstoCon.Parent.Parent;
                    int parentRowIndex = parentRow.RowIndex;
                    GridView grv0 = (GridView)GridView1.Rows[parentRowIndex].FindControl("GridViewtrans0");

                    lblid.Text = grv0.Rows[rowindex].Cells[0].Text;
                    lbljmlkirim.Text = grv0.Rows[rowindex].Cells[4].Text;
                    lbltmpjmlkirim.Text = grv0.Rows[rowindex].Cells[4].Text;
                    lblpengiriman.Text = grv0.Rows[rowindex].Cells[5].Text;
                    lbljenispalet.Text = grv0.Rows[rowindex].Cells[6].Text;
                    lbljmlpalet.Text = grv0.Rows[rowindex].Cells[7].Text;
                    ZetroView zl = new ZetroView();
                    ArrayList arrlistkirim = new ArrayList();
                    zl.QueryType = Operation.CUSTOM;
                    zl.CustomQuery =
                        "select * from MaterialCCMatrixPJPengiriman where rowstatus>-1 and pengiriman in (select pengiriman from MaterialCCMatrixPJ where tahun=" +
                        DateTime.Now.Year + " and pengiriman not like '%kembali%') order by urutan";
                    SqlDataReader sdr = zl.Retrieve();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Pengiriman", typeof(string));
                    DataRow row = dt.NewRow();
                    row["Pengiriman"] = "-";
                    dt.Rows.Add(row);
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            row = dt.NewRow();
                            row["Pengiriman"] = sdr["Pengiriman"].ToString();
                            dt.Rows.Add(row);
                        }
                    }
                    ddlPengirimanE.DataSource = dt;
                    ddlPengirimanE.DataTextField = "Pengiriman";
                    ddlPengirimanE.DataValueField = "Pengiriman";
                    ddlPengirimanE.DataBind();
                    ddlPengirimanE.SelectedValue = grv0.Rows[rowindex].Cells[5].Text.Trim();
                    ddlPengirimanE_SelectedIndexChanged(null, null);
                    break;
            }
        }
        protected void btnSave0_ServerClick(object sender, EventArgs e)
        {
            if (ddlPengirimanE.SelectedIndex <= 0)
            {
                DisplayAJAXMessage(this, "pengiriman harus diisi");
                return;
            }

            if (ddlJenisPaletE.SelectedIndex <= 0)
            {
                DisplayAJAXMessage(this, "Jenis palet harus diisi");
                return;
            }

            if (Convert.ToInt32(txtjmlkirim.Text) <= 0)
            {
                DisplayAJAXMessage(this, "Jumlah pengiriman tidak boleh <= nol");
                return;
            }

            if (Convert.ToInt32(lbljmlkirim.Text) < 0)
            {
                DisplayAJAXMessage(this, "Jumlah pengiriman tidak boleh melebihi jumlah sebelumnya");
                return;
            }

            if (Convert.ToInt32(txtJumlahE.Text) <= 0)
            {
                DisplayAJAXMessage(this, "Jumlah palet tidak boleh <= nol");
                return;
            }
            ZetroView zl = new ZetroView();
            if (Convert.ToInt32(lbljmlkirim.Text) > 0)
            {
                zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery =
                    "update t3_kirimdetail set qty=" + lbljmlkirim.Text + " where id=" + lblid.Text + " " +
                    "insert t3_kirimdetail " +
                    "SELECT  GroupID, KirimID, SerahID, LokID, ItemID, ItemIDSJ, " + txtjmlkirim.Text + " Qty, HPP, Status, CreatedBy, TglTrans, " +
                    "CreatedTime, LastModifiedBy, LastModifiedTime,RowStatus, Status_SJ, LoadingID,'" + ddlPengirimanE.SelectedValue + "' Pengiriman, '" +
                    ddlJenisPaletE.SelectedValue + "' JenisPalet," + txtJumlahE.Text + " JmlPalet " +
                    "FROM T3_KirimDetail where id=" + lblid.Text;
                SqlDataReader sdr = zl.Retrieve();
            }
            else
            {
                zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery =
                    "update t3_kirimdetail set Pengiriman='" + ddlPengirimanE.SelectedValue + "' , JenisPalet= '" +
                    ddlJenisPaletE.SelectedValue + "' ,JmlPalet=" + txtJumlahE.Text + " where id=" + lblid.Text;
                SqlDataReader sdr = zl.Retrieve();
            }
            //clearForm();
            LoadSuratJalan(txtSuratJalanNo.Text);
            txtjmlkirim.Text = "0";
            txtJumlahE.Text = "0";
            PanelPalet.Visible = false;
        }
        protected void txtJumlahE_TextChanged(object sender, EventArgs e)
        {
            if (lbljmlpalet.Text.Trim() != string.Empty)
            {
                lbljmlpalet.Text = (Convert.ToInt32(lbljmlpalet.Text) - Convert.ToInt32(txtJumlahE.Text)).ToString();
            }
        }
        protected void txtjmlkirim_TextChanged(object sender, EventArgs e)
        {
            if (lbljmlkirim.Text.Trim() != string.Empty)
            {
                lbljmlkirim.Text = (Convert.ToInt32(lbltmpjmlkirim.Text) - Convert.ToInt32(txtjmlkirim.Text)).ToString();
                txtJumlahE.Focus();
            }
        }

        protected void RbDalam_CheckedChanged(object sender, EventArgs e)
        {
            if (RbDalam.Checked == true)
            {
                RbLuar.Checked = false;
            }
        }

        protected void RbLuar_CheckedChanged(object sender, EventArgs e)
        {
            if (RbLuar.Checked == true)
            {
                RbDalam.Checked = false;
            }
        }
    }
}