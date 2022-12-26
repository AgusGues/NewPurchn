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
    public partial class InputSuratJalanToCpd : System.Web.UI.Page
    {
        //public zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();
        
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
                Session["id"] = null;
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
            }

            btnTurunStatus.Attributes.Add("onclick", "return confirm_turunTO();");
        }
        protected void btnTurunStatus_ServerClick(object sender, EventArgs e)
        {
            //nyusul

            //// Add By Anang Perubahan Status Surat Jalan TO 26 Juni 2018
            string strValidate = ValidateText();
            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }
            if (Session["AlasanTurunStatusSJTO"] == null || Session["AlasanTurunStatusSJTO"].ToString().Length <= 2)
            {
                DisplayAJAXMessage(this, "Alasan Turun status tidak boleh kosong ");
                return;
            }

            SuratJalanTOFacade suratJalanTOFacade = new SuratJalanTOFacade();

            SuratJalanTO suratJalanTO = suratJalanTOFacade.RetrieveByNo(txtSuratJalanNo.Text);
            if (suratJalanTOFacade.Error == string.Empty)
            {
                if (suratJalanTO.ID > 0)
                {
                    suratJalanTO.AlasanTurunStatus = Session["AlasanTurunStatusSJTO"].ToString();

                    if (string.IsNullOrEmpty(suratJalanTO.TglKirimActual.ToString()))
                    {
                        suratJalanTO.TglKirimActual = suratJalanTO.ScheduleDate;
                    }
                    if (suratJalanTO.TglKirimActual == DateTime.MinValue)
                    {
                        suratJalanTO.TglKirimActual = suratJalanTO.ScheduleDate;
                    }

                    suratJalanTO.LastModifiedBy = ((Users)Session["Users"]).UserName;
                    Session["AlasanTurunStatusSJTO"] = string.Empty;
                    string strError = string.Empty;
                    ArrayList arrSuratJalanTODetail = new ArrayList();
                    if (Session["ListOfSuratJalanDetailTO"] != null)
                    {
                        ArrayList arrScheduleDetail = (ArrayList)Session["ListOfSuratJalanDetailTO"];
                        foreach (SuratJalanDetail scheduleDetail in arrScheduleDetail)
                        {
                            SuratJalanDetailTO suratJalanDetailTO = new SuratJalanDetailTO();
                            suratJalanDetailTO.ItemCode = scheduleDetail.ItemCode;
                            suratJalanDetailTO.ItemID = scheduleDetail.ItemID;
                            suratJalanDetailTO.ItemName = scheduleDetail.ItemName;
                            suratJalanDetailTO.Qty = scheduleDetail.Qty;
                            suratJalanDetailTO.ScheduleDetailId = scheduleDetail.ScheduleDetailId;
                            suratJalanDetailTO.Paket = scheduleDetail.Paket;
                        }
                    }
                    SuratJalanProcessTOFacade suratJalanProsessTOFacade = new SuratJalanProcessTOFacade(suratJalanTO, arrSuratJalanTODetail);

                    if (suratJalanTO.ID > 0)
                    {
                        strError = suratJalanProsessTOFacade.TurunStatusSuratJalanTO();
                        if (strError == string.Empty)
                        {
                            InsertLog("Turun Status Surat Jalan");
                            clearForm();
                            DisplayAJAXMessage(this, "Surat Jalan " + suratJalanTO.SuratJalanNo + " sudah diturunkan");
                        }
                    }
                }
            }
            // End
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            LoadSuratJalan(txtSearch.Text);
        }

        private void clearForm()
        {
            Session["id"] = null;
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
        protected void CetakServerClick(object sender, EventArgs e)
        {

            btnPrintLgsg.Disabled = false;
            Cetak(this);
        }
        static public void Cetak(Control page)
        {
            //      string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LMutasiBP', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";

            //string myScript = "var wn = window.showModalDialog('../../Report/Report.aspx?IdReport=SuratJalanTO', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
            string myScript = "Cetak();";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        private void LoadSuratJalan(string strSJNo)
        {
            WebReference_HO.Service1 cpdWebService = new WebReference_HO.Service1();
            DataSet dsStrResult = cpdWebService.sjTORetrieveByNo(strSJNo);

            int suratJalanID = int.Parse(dsStrResult.Tables[0].Rows[0]["ID"].ToString());
            int intStatus = int.Parse(dsStrResult.Tables[0].Rows[0]["Status"].ToString());
            Session["CetakSJTO"] = int.Parse(dsStrResult.Tables[0].Rows[0]["cetak"].ToString());
            if (Int32.Parse(Session["CetakSJTO"].ToString()) > 0)
                btnPrintLgsg.Disabled = true;
            else
                btnPrintLgsg.Disabled = false;

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

                Session["id"] = suratJalanID;           //suratJalan.ID;
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
                if (Int32.Parse(Session["ctk"].ToString()) > 0)
                    btnPrintLgsg.Disabled = true;
                else
                    btnPrintLgsg.Disabled = false;
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
                //DataTable dsDetSJ = cpdWebService.sjTO_CetakSJto(suratJalanID);
                WebserviceFacade webs = new WebserviceFacade();
                DataTable dsDetSJ = webs.sjTO_CetakSJto(strSJNo);

                foreach (DataRow row in dsDetSJ.Rows)
                {
                    ScheduleDetail sDet = new ScheduleDetail();

                    sDet.ItemCode = row["ItemCode"].ToString();
                    sDet.ItemName = row["Description"].ToString();
                    sDet.Qty = int.Parse(row["Qty"].ToString());
                    arrSuratJalan.Add(sDet);
                }
                if (arrSuratJalan.Count > 0)
                {
                    Session["ListOfSuratJalanDetail"] = arrSuratJalan;
                    GridView1.DataSource = arrSuratJalan;
                    GridView1.DataBind();

                    Session["id"] = suratJalanID;
                    Session["dtCetakSJto"] = dsDetSJ;
                }
                Session["SuratJalanNo"] = txtSuratJalanNo.Text;
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
            Response.Redirect("ListSuratJalanTO.aspx?receive=no");
        }

        protected void btnListTO_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("ListScheduleTO.aspx");
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

            if (Session["id"] != null)
            {
                suratJalanTO.ID = int.Parse(Session["id"].ToString());
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
            row["TransferOrderID"] = transferOrderID;
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

                    cpdWebService = new WebReference_HO.Service1();
                    DataTable dtCetakSJ = cpdWebService.sjTO_CetakSJto(int.Parse(strResult));

                    string sjNo = dtCetakSJ.Rows[0]["SuratJalanNo"].ToString();
                    txtSuratJalanNo.Text = sjNo;
                    Session["SuratJalanNo"] = txtSuratJalanNo.Text;
                    Session["id"] = int.Parse(strResult);
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
            //        Session["id"] = suratJalanTO.ID;

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

            if (Session["id"] != null)
                suratJalan.ID = int.Parse(Session["id"].ToString());

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
            row["ReceiveDate"] = DateTime.Now;
            row["LastModifiedBy"] = txtCreatedBy.Text;
            dt.Rows.Add(row);
            dt.TableName = "sjTO_UpdatePostingDateSJ";

            WebReference_HO.Service1 cpdWebService = new WebReference_HO.Service1();
            string strResult = cpdWebService.sjTO_UpdatePostingDateSJ(dt, 1);

            if (int.Parse(strResult) > 0)
            {
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

            if (Session["id"] != null)
                suratJalan.ID = int.Parse(Session["id"].ToString());

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
            //cpdWebService.ca
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
    }
}