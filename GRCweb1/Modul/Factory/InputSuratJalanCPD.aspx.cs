using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GRCweb1.Modul.Factory
{
    public partial class InputSuratJalanCPD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";

                clearForm();
                Session["id"] = null;
                Session["stat"] = null;
                Session["ctk"] = null;
                Session["CountCtk"] = null;

                Session["JudulReasonCancel"] = null;
                ViewState["OPNo"] = string.Empty;
                Users users = (Users)Session["Users"];
                lbUpdateTglAktualKirim0_Click(null, null);
                //if (!((Hashtable)Session["Rules"]).ContainsValue("Cancel Receive"))
                //{
                btnCancelReceive.Enabled = true;
                //}

                if (Request.QueryString["SJNo"] != null)
                {
                    LoadSuratJalan(Request.QueryString["SJNo"].ToString());
                    if (users.DeptID == 12)
                    {
                        //btnPengajuan.Visible = true;
                        btnCancel.Visible = false;
                    }
                }
                if (Request.QueryString["ScheduleNo"] != null)
                {
                    /////
                    WebReference_HO.Service1 cpdWebService = new WebReference_HO.Service1();

                    //iko24Okt pindahan & addd baru
                    DataSet dsScreen1 = cpdWebService.sj_RetrieveForScreen1(Request.QueryString["OPNo"].ToString(), Request.QueryString["ScheduleNo"].ToString());

                    DataTable dt0 = new DataTable();
                    dt0.Columns.Add("ScheduleID", typeof(int));
                    dt0.Columns.Add("TglKirimActual", typeof(DateTime));
                    dt0.Columns.Add("OPID", typeof(int));
                    dt0.Columns.Add("PoliceCarNo", typeof(string));
                    dt0.Columns.Add("DriverName", typeof(string));
                    dt0.Columns.Add("kenekname", typeof(string));
                    dt0.Columns.Add("Keterangan", typeof(string));
                    dt0.Columns.Add("CreatedBy", typeof(string));

                    DataRow row = dt0.NewRow();
                    row["ScheduleID"] = int.Parse(dsScreen1.Tables[0].Rows[0]["ScheduleID"].ToString());
                    row["TglKirimActual"] = DateTime.Parse(dsScreen1.Tables[0].Rows[0]["ScheduleDate"].ToString());
                    row["OPID"] = int.Parse(dsScreen1.Tables[0].Rows[0]["ID"].ToString());
                    row["PoliceCarNo"] = '-';   //belum perlu karena cuma checking Adapter flag lock payment kah ?
                    row["DriverName"] = '-';
                    row["kenekname"] = '-';
                    row["Keterangan"] = '-';
                    row["CreatedBy"] = '-';
                    dt0.Rows.Add(row);
                    dt0.TableName = "sj_InsertNewSJ2forCheckingDahulu";

                    cpdWebService = new WebReference_HO.Service1();
                    //DataSet dsStrResult = cpdWebService.sj_CekCreditLimitDanTOP(Request.QueryString["OPNo"].ToString(), Request.QueryString["ScheduleNo"].ToString());
                    string dsStrResult = cpdWebService.sj_InsertNewSJ2(dt0, 0);
                    if (int.Parse(dsStrResult) == 0)
                    {
                        DisplayAJAXMessage(this, "Gagal karena belum di-define ");
                    }
                    //else if (int.Parse(dsStrResult) > 0)
                    //{ }
                    else if (int.Parse(dsStrResult) < 0)
                        switch (dsStrResult.Trim())
                        {
                            case "-1":
                                DisplayAJAXMessage(this, "Gagal create Surat Jalan karena sudah dibuat Surat Jalan");
                                break;
                            case "-2":
                                DisplayAJAXMessage(this, "Gagal create Surat Jalan karena melewati waktu lama pembayaran");
                                break;
                            case "-3":
                                DisplayAJAXMessage(this, "Gagal create Surat Jalan karena melewati waktu lama pembayaran dan uang masuk pada bank-in kurang / belum ada");
                                break;
                            case "-4":
                                DisplayAJAXMessage(this, "Gagal create Surat Jalan karena melewati batas credit limit");
                                break;
                            case "-5":
                                DisplayAJAXMessage(this, "Gagal create Surat Jalan karena uang masuk pada bank-in kurang / belum ada");
                                break;
                            case "-6":
                                DisplayAJAXMessage(this, "Gagal create Surat Jalan karena gagal simpan data surat jalan");
                                break;
                        }
                    //string strResult = c.Tables[0].Rows[0][0].ToString();
                    //iko24Okt

                    else
                    {
                        Session["ListOfSuratJalanHeader"] = null;

                        //iko24Okt di-remark
                        //cpdWebService = new WebReference_HO.Service1();
                        //DataSet dsScreen1 = cpdWebService.sj_RetrieveForScreen1(Request.QueryString["OPNo"].ToString(), Request.QueryString["ScheduleNo"].ToString());
                        //iko24Okt

                        string strKeterangan2 = dsScreen1.Tables[0].Rows[0]["Keterangan2"].ToString();
                        string strNoPolisi = dsScreen1.Tables[0].Rows[0]["NoPolisi"].ToString();
                        int strCustomerType = int.Parse(dsScreen1.Tables[0].Rows[0]["CustomerType"].ToString());
                        string strCekProyek = dsScreen1.Tables[0].Rows[0]["CekProyek"].ToString(); //beda/sama
                        DateTime scheduleDate = DateTime.Parse(dsScreen1.Tables[0].Rows[0]["ScheduleDate"].ToString());
                        string strAlamatlain = dsScreen1.Tables[0].Rows[0]["AlamatLain"].ToString();
                        string strAddress = dsScreen1.Tables[0].Rows[0]["Address"].ToString();


                        txtSalesOrderNo.Text = Request.QueryString["OPNo"].ToString();
                        txtScheduleNo.Text = Request.QueryString["ScheduleNo"].ToString();
                        txtKeterangan.Text = strKeterangan2 + "(" + txtScheduleNo.Text.Trim() + ")";
                        txtNoMobil.Text = strNoPolisi;

                        Session["ListOfSuratJalanHeader"] = dsScreen1;
                        Session["ListOfSuratJalanDetail"] = null;

                        if (strAlamatlain != string.Empty)
                        {
                            txtAlamat.Text = strAlamatlain;
                        }
                        else
                        {
                            txtAlamat.Text = strAddress;
                        }
                        if (strCekProyek == "Beda")
                        {
                            DisplayAJAXMessage(this, "Alamat Kirim Toko tidak sama dengan di-Master atau Bukan Proyek, cek TokoName, Address, Telepon");
                            return;
                        }
                        else
                            GetScheduleDetailAPI(Request.QueryString["ScheduleNo"].ToString(), Request.QueryString["OPNo"].ToString(), strCustomerType, scheduleDate);
                    }
                }
            }

            btnCancel.Attributes.Add("onclick", "return confirm_delete();");
            btnTurunStatus.Attributes.Add("onclick", "return confirm_turun();");
        }
        protected void btnPengajuan_ServerClick(object sender, EventArgs e)
        {
            string strValidate = ValidateText();
        }

        protected void btnAutoSJ_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("CreateAutoSJ.aspx?receive=no");
        }
        protected void btnListPengajuan_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("ListPengajuanSJ.aspx?receive=no");
        }
        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            clearForm();
            LoadSuratJalan(txtSearch.Text);
        }

        protected void btnList_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("ListSuratJalan.aspx?receive=no");
        }

        protected void btnCancel_ServerClick(object sender, EventArgs e)
        {
            string strValidate = ValidateText();
        }

        protected void btnListTO_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("ListScheduleOP.aspx");
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearForm();
        }

        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            string strValidate = ValidateText();

            if (DateTime.Parse(txtActualKirim.Text) < DateTime.Parse(txtScheduleDate.Text))
            {
                DisplayAJAXMessage(this, "Tgl Kirim Aktual harus sama / lebih dari dari Tgl Kirim Schedule " + txtScheduleDate.Text);
                return;
            }

            int gudangID = 0;
            int depoID = 0;

            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }

            Users users = (Users)Session["Users"];
            string strEvent = "Insert";
            int intDepoID = 0;

            SuratJalan suratJalan = new SuratJalan();

            if (Session["id"] != null)
            {
                suratJalan.ID = int.Parse(Session["id"].ToString());
                suratJalan.Status = int.Parse(Session["stat"].ToString());
                suratJalan.Cetak = int.Parse(Session["ctk"].ToString());
                suratJalan.CountPrint = int.Parse(Session["CountCtk"].ToString());

                strEvent = "Edit";
            }

            suratJalan.SuratJalanNo = txtSuratJalanNo.Text;

            int opID = 0; int scheduleID = 0; DateTime scheduleDate = DateTime.MinValue; int expedisiDetailID = 0;
            if (Session["ListOfSuratJalanHeader"] != null)
            {
                DataSet dsScreen2 = (DataSet)Session["ListOfSuratJalanHeader"];

                opID = int.Parse(dsScreen2.Tables[0].Rows[0]["ID"].ToString());
                depoID = int.Parse(dsScreen2.Tables[0].Rows[0]["DepoID"].ToString());
                scheduleID = int.Parse(dsScreen2.Tables[0].Rows[0]["ScheduleID"].ToString());
                scheduleDate = DateTime.Parse(dsScreen2.Tables[0].Rows[0]["ScheduleDate"].ToString());
                expedisiDetailID = int.Parse(dsScreen2.Tables[0].Rows[0]["ExpedisiDetailID"].ToString());

            }

            suratJalan.OPID = opID;     //op.ID;
            suratJalan.DepoID = depoID; // op.DepoID;

            suratJalan.ScheduleID = scheduleID;
            if (txtActualKirim.Text == string.Empty)
                txtActualKirim.Text = scheduleDate.ToString("dd-MMM-yyyy");
            intDepoID = depoID;
            suratJalan.ExpedisiDetailID = expedisiDetailID;

            suratJalan.PoliceCarNo = txtNoMobil.Text;
            suratJalan.DriverName = txtDriverName.Text;
            //suratJalan.KenekName = "-";
            suratJalan.CreatedBy = txtCreatedBy.Text;
            suratJalan.TypeOP = 1;   //op.TypeOP;
            suratJalan.Keterangan = txtKeterangan.Text;
            suratJalan.TglKirimActual = DateTime.Parse(txtActualKirim.Text);
            suratJalan.ScheduleID = scheduleID;

            WebReference_HO.Service1 cpdWebService = new WebReference_HO.Service1();

            SJNumber sjNumber = new SJNumber();

            string strError = string.Empty;
            ArrayList arrSuratJalanDetail = new ArrayList();
            if (Session["ListOfSuratJalanDetail"] != null)
            {
            }

            DataTable dt = new DataTable();
            dt.Columns.Add("ScheduleID", typeof(int));
            dt.Columns.Add("TglKirimActual", typeof(DateTime));
            dt.Columns.Add("OPID", typeof(int));
            dt.Columns.Add("PoliceCarNo", typeof(string));
            dt.Columns.Add("DriverName", typeof(string));
            dt.Columns.Add("KenekName", typeof(string));
            dt.Columns.Add("Keterangan", typeof(string));
            dt.Columns.Add("CreatedBy", typeof(string));

            DataRow row = dt.NewRow();
            row["ScheduleID"] = scheduleID;
            row["TglKirimActual"] = DateTime.Parse(txtActualKirim.Text);
            row["OPID"] = opID;
            row["PoliceCarNo"] = txtNoMobil.Text;
            row["DriverName"] = txtDriverName.Text;
            row["KenekName"] = string.Empty;
            row["Keterangan"] = txtKeterangan.Text;
            row["CreatedBy"] = txtCreatedBy.Text;
            dt.Rows.Add(row);
            dt.TableName = "sj_InsertNewSJ";

            cpdWebService = new WebReference_HO.Service1();
            if (suratJalan.ID > 0)
            {
                //strError = suratJalanProsessFacade.Update();
                clearForm();
            }
            else
            {
                //coba insert dulu aja nanti feedback suratJalan.ID; & suratJalanProsessFacade.SuratJalanNo;
                Session["dtCetakSJ"] = null;
                //iko24Okt
                //string strResult = cpdWebService.sj_InsertNewSJ(dt);
                string strResult = cpdWebService.sj_InsertNewSJ2(dt, 1);
                //iko24Okt

                if (int.Parse(strResult) > 0)
                {
                    btnSave.Disabled = true;

                    cpdWebService = new WebReference_HO.Service1();
                    DataTable dtCetakSJ = cpdWebService.sj_CetakSJ(int.Parse(strResult));

                    string sjNo = dtCetakSJ.Rows[0]["SuratJalanNo"].ToString();
                    //txtSuratJalanNo.Text = dsResult.Tables[0].Rows[0][1].ToString();
                    txtSuratJalanNo.Text = sjNo;
                    Session["SuratJalanNo"] = txtSuratJalanNo.Text;
                    Session["id"] = int.Parse(strResult);
                    Session["dtCetakSJ"] = dtCetakSJ;
                }
                else
                {
                    //iko24Okt
                    switch (strResult.Trim())
                    {
                        case "-1":
                            DisplayAJAXMessage(this, "Gagal create Surat Jalan karena sudah dibuat Surat Jalan");
                            break;
                        case "-2":
                            DisplayAJAXMessage(this, "Gagal create Surat Jalan karena melewati waktu lama pembayaran");
                            break;
                        case "-3":
                            DisplayAJAXMessage(this, "Gagal create Surat Jalan karena melewati waktu lama pembayaran dan uang masuk pada bank-in kurang / belum ada");
                            break;
                        case "-4":
                            DisplayAJAXMessage(this, "Gagal create Surat Jalan karena melewati batas credit limit");
                            break;
                        case "-5":
                            DisplayAJAXMessage(this, "Gagal create Surat Jalan karena uang masuk pada bank-in kurang / belum ada");
                            break;
                        case "-6":
                            DisplayAJAXMessage(this, "Gagal create Surat Jalan karena gagal simpan data surat jalan");
                            break;
                    }
                    //iko24Okt
                }
            }

        }

        protected void btnCancelReceive_ServerClick(object sender, EventArgs e)
        {
            SuratJalan suratJalan = new SuratJalan();

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

            SuratJalan suratJalan = new SuratJalan();

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
            row["ReceiveDate"] = DateTime.Parse(txtActualKirim.Text);
            row["LastModifiedBy"] = txtCreatedBy.Text;
            dt.Rows.Add(row);
            dt.TableName = "sj_UpdatePostingDateSJ";

            WebReference_HO.Service1 cpdWebService = new WebReference_HO.Service1();
            string strResult = cpdWebService.sj_UpdatePostingDateSJ(dt, 2);

            if (int.Parse(strResult) > 0)
            {
                if (flagSmsOutbox > 0)
                    DisplayAJAXMessage(this, "Posting Shipment & Send Data ke SMS Outbox berhasil");
                else
                    DisplayAJAXMessage(this, "Posting Shipment berhasil");

                clearForm();
            }

            //24Agus2018
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

            SuratJalan suratJalan = new SuratJalan();

            if (Session["id"] != null)
                suratJalan.ID = int.Parse(Session["id"].ToString());

            //24Agus2018
            if (Session["stat"] != null)
            {
                if (int.Parse(Session["stat"].ToString()) > 0)
                {
                    DisplayAJAXMessage(this, "Status Surat Jalan sudah Shipment ");
                    return;
                }
            }
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
            dt.TableName = "sj_UpdatePostingDateSJ";

            WebReference_HO.Service1 cpdWebService = new WebReference_HO.Service1();
            string strResult = cpdWebService.sj_UpdatePostingDateSJ(dt, 1);

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
        protected void CetakServerClick(object sender, EventArgs e)
        {
            btnPrintLgsg.Disabled = false;
            Cetak(this);

        }
        static public void Cetak(Control page)
        {
            //      string myScript = "var wn = window.showModalDialog('../../ReportT1T3/Report.aspx?IdReport=LMutasiBP', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";

            //string myScript = "var wn = window.showModalDialog('../../Report/Report.aspx?IdReport=SuratJalan', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
            string myScript = "Cetak();";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private void LoadSuratJalan(string strSJNo)
        {
            SuratJalan suratJalan = new SuratJalan();
            WebReference_HO.Service1 cpdWebService = new WebReference_HO.Service1();
            DataSet dsStrResult = cpdWebService.sj_RetrieveByNoPostDepoID(strSJNo, ((Users)Session["Users"]).UnitKerjaID);

            int suratJalanID = int.Parse(dsStrResult.Tables[0].Rows[0]["ID"].ToString());
            Session["CetakSJ"] = int.Parse(dsStrResult.Tables[0].Rows[0]["cetak"].ToString());
            if (Int32.Parse(Session["CetakSJ"].ToString()) > 0)
                btnPrintLgsg.Disabled = true;
            else
                btnPrintLgsg.Disabled = false;

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

                Session["id"] = suratJalanID;           //suratJalan.ID;
                Session["stat"] = intStatus;            // suratJalan.Status;
                Session["ctk"] = int.Parse(dsStrResult.Tables[0].Rows[0]["Cetak"].ToString());         //suratJalan.Cetak;
                Session["CountCtk"] = int.Parse(dsStrResult.Tables[0].Rows[0]["CountPrint"].ToString());    //suratJalan.CountPrint;

                txtSuratJalanNo.Text = dsStrResult.Tables[0].Rows[0]["SuratJalanNo"].ToString();      //suratJalan.SuratJalanNo;
                txtSalesOrderNo.Text = dsStrResult.Tables[0].Rows[0]["OPNo"].ToString();      //suratJalan.OPNo;
                txtScheduleNo.Text = dsStrResult.Tables[0].Rows[0]["ScheduleNo"].ToString();        //suratJalan.ScheduleNo;
                txtNoMobil.Text = dsStrResult.Tables[0].Rows[0]["PoliceCarNo"].ToString();           //suratJalan.PoliceCarNo;
                txtDriverName.Text = dsStrResult.Tables[0].Rows[0]["DriverName"].ToString();        //suratJalan.DriverName;
                txtCreateDate.Text = DateTime.Parse(dsStrResult.Tables[0].Rows[0]["CreatedTime"].ToString()).ToString("dd-MMM-yyyy");        //suratJalan.CreatedTime.ToString("dd-MMM-yyyy");
                txtCreatedBy.Text = dsStrResult.Tables[0].Rows[0]["CreatedBy"].ToString();         //suratJalan.CreatedBy;
                txtKeterangan.Text = dsStrResult.Tables[0].Rows[0]["Keterangan"].ToString();        //suratJalan.Keterangan;
                txtActualKirim.Text = DateTime.Parse(dsStrResult.Tables[0].Rows[0]["ActualShipmentDate"].ToString()).ToString("dd-MMM-yyyy");       //suratJalan.TglKirimActual.ToString("dd-MMM-yyyy");
                txtAlamat.Text = dsStrResult.Tables[0].Rows[0]["AlamatLain"].ToString(); ;

                if (intStatus > 1)
                {
                    if (intStatus >= 3)
                        btnTurunStatus.Enabled = false;
                    else
                        btnTurunStatus.Enabled = true;

                    //btnUpdate.Disabled = true;
                    btnSave.Disabled = true;
                    btnCancelReceive.Enabled = false;
                    btnCancel.Enabled = false;
                    btnPrintLgsg.Disabled = true;

                    lbUpdateTglAktualKirim.Enabled = false;

                    if (!((Hashtable)Session["Rules"]).ContainsValue("Cancel Receive"))
                    {
                        btnCancelReceive.Enabled = true;
                    }
                }
                else
                {
                    if (intStatus >= 1)
                    {
                        if (intStatus >= 3)
                            btnTurunStatus.Enabled = false;
                        else
                            btnTurunStatus.Enabled = true;

                        //btnUpdate.Disabled = true;
                        btnCancel.Enabled = false;
                    }
                    else if (intStatus == -1)
                    {
                        //btnUpdate.Disabled = true;
                        btnSave.Disabled = true;
                        btnCancelReceive.Enabled = false;
                        btnCancel.Enabled = false;
                        btnTurunStatus.Enabled = false;
                        btnPrintLgsg.Disabled = true;
                    }
                    else
                    {
                        btnSave.Disabled = true;
                        btnCancelReceive.Enabled = false;
                        btnTurunStatus.Enabled = false;
                    }
                }

                ArrayList arrSuratJalan = new ArrayList();

                cpdWebService = new WebReference_HO.Service1();
                DataTable dsDetSJ = new DataTable();
                //try
                //{
                dsDetSJ = cpdWebService.sj_CetakSJ(suratJalanID);
                //}
                //catch (Exception ex)
                //{
                //    DisplayAJAXMessage(this, ex.Message.ToString());
                //    return;
                //}

                foreach (DataRow row in dsDetSJ.Rows)
                {
                    ScheduleDetail sDet = new ScheduleDetail();

                    sDet.ItemCode = row["ItemCode"].ToString();
                    sDet.ItemName = row["ItemName"].ToString();
                    sDet.Qty = int.Parse(row["Qty"].ToString());

                    arrSuratJalan.Add(sDet);
                }
                if (arrSuratJalan.Count > 0)
                {
                    Session["ListOfSuratJalanDetail"] = arrSuratJalan;
                    GridView1.DataSource = arrSuratJalan;
                    GridView1.DataBind();

                    Session["id"] = suratJalanID;
                    Session["dtCetakSJ"] = dsDetSJ;
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

        private void clearForm()
        {
            txtActualKirim.Text = string.Empty;
            txtAlamat.Text = string.Empty;

            Session["id"] = null;
            Session["stat"] = null;
            Session["ctk"] = null;
            Session["CountCtk"] = null;
            Session["SJNo"] = null;
            Session["OPNo"] = null;
            Session["ListOfSuratJalanDetail"] = null;
            Session["SuratJalanNo"] = null;
            Session["AlasanCancel"] = null;
            Session["AlasanTurunStatusSJ"] = null;

            txtSuratJalanNo.Text = string.Empty;
            txtSalesOrderNo.Text = string.Empty;
            txtScheduleNo.Text = string.Empty;
            txtNoMobil.Text = string.Empty;
            txtDriverName.Text = string.Empty;
            txtKeterangan.Text = string.Empty;
            txtCreateDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtCreatedBy.Text = ((Users)Session["Users"]).UserName;
            txtScheduleDate.Text = string.Empty;

            //if (((Users)Session["Users"]).UnitKerjaID != 1 && ((Users)Session["Users"]).UnitKerjaID != 7)
            //    btnUpdate.Visible = false;
            //else
            //    btnUpdate.Visible = true;


            btnSave.Disabled = false;
            ArrayList arrList = new ArrayList();
            arrList.Add(new SuratJalanDetail());
            GridView1.DataSource = arrList;
            GridView1.DataBind();
        }

        protected void lbUpdateTglAktualKirim_Click(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];

            // cek apakah SJ sdh Receive
            int cekSJID = 0;
            int cekSchedID = 0;
            DateTime tglAktualKirimAwal = new DateTime();
            SuratJalanFacade cekReceiveFacade = new SuratJalanFacade();
            SuratJalan cekReceive = cekReceiveFacade.RetrieveByNo(txtSuratJalanNo.Text);
            if (cekReceiveFacade.Error == string.Empty)
            {
                cekSJID = cekReceive.ID;
                cekSchedID = cekReceive.ScheduleID;
                tglAktualKirimAwal = cekReceive.TglKirimActual;
                if (cekReceive.Status >= 2)
                {
                    DisplayAJAXMessage(this, "SJ sudah di Receive, tidak bisa di edit Tgl Aktual Kirim nya ......");
                    return;
                }
                // cek cuma 2 hari dr shipmentdate nya OP
                ScheduleFacade cekExpireScheduleFacade = new ScheduleFacade();
                string strAktualKirim = txtActualKirim.Text;
                int SelisihHari = cekExpireScheduleFacade.RetrieveSelisihHari(txtActualKirim.Text.ToString(), cekSchedID);

                if (cekExpireScheduleFacade.Error == string.Empty)
                {

                    if (SelisihHari > 2)
                    {
                        DisplayAJAXMessage(this, "Sudah melewati batas 2 hari dari Scheduledate !");
                        txtActualKirim.Text = tglAktualKirimAwal.ToString("dd-MMM-yyyy");
                        return;
                    }
                    else
                    {
                        if (SelisihHari < 0)
                        {
                            DisplayAJAXMessage(this, "Selisih Hari tidak boleh Minus.");
                            txtActualKirim.Text = tglAktualKirimAwal.ToString("dd-MMM-yyyy");
                            return;
                        }
                        else
                        {
                            if ((int)DateTime.Parse(txtActualKirim.Text).DayOfWeek == 0 || (int)DateTime.Parse(txtActualKirim.Text).DayOfWeek == 6)
                            {
                                DisplayAJAXMessage(this, "Pilihan Hari Sabtu atau Minggu tidak merubah Tgl Aktual Kirim !");
                                txtActualKirim.Text = tglAktualKirimAwal.ToString("dd-MMM-yyyy");
                                cekReceive.TglKirimActual = tglAktualKirimAwal;
                            }
                            else
                            {
                                cekReceive.TglKirimActual = DateTime.Parse(txtActualKirim.Text);
                            }

                            // Update TglKirimAktual

                            cekReceive.LastModifiedBy = users.UserName;
                            cekReceive.ID = cekSJID;
                            string strErrorCek = string.Empty;
                            SuratJalanProcessFacade actshipSJProcessFacade = new SuratJalanProcessFacade(cekReceive, new ArrayList(), new SJNumber());

                            if (cekReceive.ID > 0)
                            {
                                strErrorCek = actshipSJProcessFacade.UpdateTglKirimAktual();
                                if (strErrorCek == string.Empty)
                                {
                                    InsertLog("Update Tgl Kirim Aktual");
                                    //clearForm();
                                }
                            }

                            DisplayAJAXMessage(this, "Tgl Kirim Aktual berhasil di upadate !");
                        }
                    }
                }
            }

        }

        private void GetScheduleDetail()
        {
            ScheduleDetailFacade scheduleDetailFacade = new ScheduleDetailFacade();
            OPFacade opFacade = new OPFacade();
            OP op = opFacade.RetrieveByNo(txtSalesOrderNo.Text);
            if (opFacade.Error == string.Empty)
            {
                if (op.ID > 0)
                {
                    ArrayList arrScheduleDetail = scheduleDetailFacade.RetrieveByOPNo(txtScheduleNo.Text, txtSalesOrderNo.Text, op.CustomerType);
                    if (scheduleDetailFacade.Error == string.Empty)
                    {
                        Session["ListOfSuratJalanDetail"] = arrScheduleDetail;

                        GridView1.DataSource = arrScheduleDetail;
                        GridView1.DataBind();
                    }

                    ScheduleFacade scheduleFacade = new ScheduleFacade();
                    Schedule schedule = scheduleFacade.RetrieveByNo(txtScheduleNo.Text);
                    if (schedule.ID > 0)
                    {
                        txtActualKirim.Text = schedule.ScheduleDate.ToString("dd-MMM-yyyy");
                        txtScheduleDate.Text = schedule.ScheduleDate.ToString("dd-MMM-yyyy");
                    }
                    else
                        txtScheduleDate.Text = string.Empty;

                }
            }
        }
        private void GetScheduleDetailAPI(string txtScheduleNo, string txtSalesOrderNo, int CustomerType, DateTime tglKirim)
        {
            if (txtScheduleNo != string.Empty && txtSalesOrderNo != string.Empty)
            {
                ArrayList arrScheduleDetail = new ArrayList();
                WebReference_HO.Service1 cpdWebService = new WebReference_HO.Service1();

                DataSet dsScheduleDetail = cpdWebService.sj_RetrieveForGrid(txtScheduleNo, txtSalesOrderNo, CustomerType);

                foreach (DataRow row in dsScheduleDetail.Tables[0].Rows)
                {
                    ScheduleDetail sDet = new ScheduleDetail();

                    sDet.ID = int.Parse(row["ID"].ToString());
                    sDet.ScheduleID = int.Parse(row["ScheduleID"].ToString());
                    sDet.DocumentID = int.Parse(row["DocumentID"].ToString());
                    sDet.DocumentDetailID = int.Parse(row["DocumentDetailID"].ToString());
                    sDet.TypeDoc = int.Parse(row["TypeDoc"].ToString());
                    sDet.DocumentNo = row["DocumentNo"].ToString();
                    sDet.ItemID = int.Parse(row["ItemID"].ToString());
                    sDet.ItemCode = row["ItemCode"].ToString();
                    sDet.ItemName = row["ItemName"].ToString();
                    sDet.KotaTujuan = row["KotaTujuan"].ToString();
                    sDet.AreaTujuan = row["AreaTujuan"].ToString();
                    sDet.Qty = int.Parse(row["Qty"].ToString());
                    sDet.TotalKubikasi = decimal.Parse(row["TotalKubikasi"].ToString());
                    sDet.Paket = int.Parse(row["Paket"].ToString());
                    sDet.DepoID = int.Parse(row["DepoID"].ToString());

                    arrScheduleDetail.Add(sDet);
                }

                Session["ListOfSuratJalanDetail"] = arrScheduleDetail;

                GridView1.DataSource = arrScheduleDetail;
                GridView1.DataBind();
                //}

                txtActualKirim.Text = tglKirim.ToString("dd-MMM-yyyy");
                txtScheduleDate.Text = tglKirim.ToString("dd-MMM-yyyy");
            }
        }

        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Posting Shipment SJ OP";
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
            if (txtSalesOrderNo.Text == string.Empty)
                return "No OP tidak boleh kosong";
            else if (txtNoMobil.Text == string.Empty)
                return "No Mobil tidak boleh kosong";
            else if (txtDriverName.Text == string.Empty)
                return "Nama Supir harus diisi";

            return string.Empty;
        }

        public string LocalIPAddress()
        {
            System.Net.IPHostEntry host;
            string localIP = "";
            host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            foreach (System.Net.IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }
        protected void txtActualKirim_TextChanged(object sender, EventArgs e)
        {
            if (txtScheduleDate.Text.Trim() == string.Empty)
                return;
            if (DateTime.Parse(txtActualKirim.Text) < DateTime.Parse(txtScheduleDate.Text))
            {
                DisplayAJAXMessage(this, "Tgl Kirim Aktual harus sama / lebih dari dari Tgl Kirim Schedule " + txtScheduleDate.Text);
                return;
            }
        }
        protected void btnTurunStatus_ServerClick(object sender, EventArgs e)
        {
            string strValidate = ValidateText();
            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }
            if (Session["AlasanTurunStatusSJ"] == null || Session["AlasanTurunStatusSJ"].ToString().Length <= 2)
            {
                DisplayAJAXMessage(this, "Alasan Turun status tidak boleh kosong ");
                return;
            }

            SuratJalanFacade suratJalanFacade = new SuratJalanFacade();

            SuratJalan suratJalan = suratJalanFacade.RetrieveByNo(txtSuratJalanNo.Text);
            if (suratJalanFacade.Error == string.Empty)
            {
                if (suratJalan.ID > 0)
                {
                    ScheduleFacade scheFacade = new ScheduleFacade();
                    Schedule sche = scheFacade.CekSchedule(suratJalan.ScheduleID);
                    if (sche.ID > 0)
                        suratJalan.DepoID = sche.DepoID;

                    suratJalan.AlasanTurunStatus = Session["AlasanTurunStatusSJ"].ToString();

                    if (string.IsNullOrEmpty(suratJalan.TglKirimActual.ToString()))
                    {
                        suratJalan.TglKirimActual = suratJalan.ScheduleDate;
                    }
                    if (suratJalan.TglKirimActual == DateTime.MinValue)
                    {
                        suratJalan.TglKirimActual = suratJalan.ScheduleDate;
                    }

                    suratJalan.LastModifiedBy = ((Users)Session["Users"]).UserName;

                    Session["AlasanTurunStatusSJ"] = string.Empty;
                    string strError = string.Empty;
                    ArrayList arrSuratJalanDetail = new ArrayList();
                    if (Session["ListOfSuratJalanDetail"] != null)
                    {
                        ArrayList arrScheduleDetail = (ArrayList)Session["ListOfSuratJalanDetail"];
                        foreach (SuratJalanDetail scheduleDetail in arrScheduleDetail)
                        {
                            SuratJalanDetail suratJalanDetail = new SuratJalanDetail();
                            suratJalanDetail.ItemCode = scheduleDetail.ItemCode;
                            suratJalanDetail.ItemID = scheduleDetail.ItemID;
                            suratJalanDetail.ItemName = scheduleDetail.ItemName;
                            suratJalanDetail.Qty = scheduleDetail.Qty;
                            suratJalanDetail.ScheduleDetailId = scheduleDetail.ScheduleDetailId;
                            suratJalanDetail.Paket = scheduleDetail.Paket;

                            arrSuratJalanDetail.Add(suratJalanDetail);
                        }
                    }

                    SuratJalanProcessFacade suratJalanProsessFacade = new SuratJalanProcessFacade(suratJalan, arrSuratJalanDetail, new SJNumber());

                    if (suratJalan.ID > 0)
                    {
                        //strError = suratJalanProsessFacade.CancelSuratJalan();
                        strError = suratJalanProsessFacade.TurunStatusSuratJalan();
                        if (strError == string.Empty)
                        {
                            InsertLog("Turun Status Surat Jalan");
                            clearForm();
                            DisplayAJAXMessage(this, "Surat Jalan " + suratJalan.SuratJalanNo + " sudah diturunkan");
                        }
                    }
                }
            }
        }
        protected void lbUpdateTglAktualKirim0_Click(object sender, EventArgs e)
        {
            ArrayList listAntrian = new ArrayList();
            ZetroView zl = new ZetroView();
            string strSort = string.Empty;
            if (RBAntrian.Checked == true)
                strSort = "  order by noantrian";
            else
                strSort = "  order by scheduleno";

            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "select distinct L.UrutanNo Noantrian,A.OPNo,A.ScheduleNo,A.SuratJalanNo,A.expedisiname " +
                "from [sql1.grcboard.com].grcboard.dbo.vw_CetakSJOP A inner join loadingtime L on A.ScheduleNo=L.Keterangan  " +
                "where convert(char,tglin,112)='" + DateTime.Now.ToString("yyyyMMdd") + "' " + strSort;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    listAntrian.Add(GenerateObj(sdr));
                }
            }
            GridAntrian.DataSource = listAntrian;
            GridAntrian.DataBind();
        }
        ArrayList arrData = new ArrayList();
        ListScheduleCPD1 spc = new ListScheduleCPD1();
        private ListScheduleCPD1 GenerateObj(SqlDataReader sdr)
        {
            spc = new ListScheduleCPD1();
            spc.NoAntrian = sdr["NoAntrian"].ToString();
            spc.NoSchedule = sdr["ScheduleNo"].ToString();
            spc.NoOP = sdr["OPNo"].ToString();
            spc.ExpedisiName = sdr["ExpedisiName"].ToString();
            spc.suratjalanNo = sdr["suratjalanNo"].ToString();
            return spc;
        }
        protected void RBAntrian_CheckedChanged(object sender, EventArgs e)
        {
            lbUpdateTglAktualKirim0_Click(null, null);
        }
        protected void RBSchedule_CheckedChanged(object sender, EventArgs e)
        {
            lbUpdateTglAktualKirim0_Click(null, null);
        }
        protected void GridAntrian_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            clearForm();
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GridAntrian.Rows[index];
            if (e.CommandName == "pilih")
            {
                string scheduleno = row.Cells[3].Text;
                string OPno = row.Cells[2].Text;
                LoadSuratJalan(scheduleno);
            }
        }
        protected void buatSJ(string opno, string scheduleno)
        {
            WebReference_HO.Service1 cpdWebService = new WebReference_HO.Service1();

            //iko24Okt pindahan & addd baru
            DataSet dsScreen1 = cpdWebService.sj_RetrieveForScreen1(opno, scheduleno);

            DataTable dt0 = new DataTable();
            dt0.Columns.Add("ScheduleID", typeof(int));
            dt0.Columns.Add("TglKirimActual", typeof(DateTime));
            dt0.Columns.Add("OPID", typeof(int));
            dt0.Columns.Add("PoliceCarNo", typeof(string));
            dt0.Columns.Add("DriverName", typeof(string));
            dt0.Columns.Add("kenekname", typeof(string));
            dt0.Columns.Add("Keterangan", typeof(string));
            dt0.Columns.Add("CreatedBy", typeof(string));

            DataRow row = dt0.NewRow();
            row["ScheduleID"] = int.Parse(dsScreen1.Tables[0].Rows[0]["ScheduleID"].ToString());
            row["TglKirimActual"] = DateTime.Parse(dsScreen1.Tables[0].Rows[0]["ScheduleDate"].ToString());
            row["OPID"] = int.Parse(dsScreen1.Tables[0].Rows[0]["ID"].ToString());
            row["PoliceCarNo"] = '-';   //belum perlu karena cuma checking Adapter flag lock payment kah ?
            row["DriverName"] = '-';
            row["kenekname"] = '-';
            row["Keterangan"] = '-';
            row["CreatedBy"] = '-';
            dt0.Rows.Add(row);
            dt0.TableName = "sj_InsertNewSJ2forCheckingDahulu";

            cpdWebService = new WebReference_HO.Service1();
            //DataSet dsStrResult = cpdWebService.sj_CekCreditLimitDanTOP(Request.QueryString["OPNo"].ToString(), Request.QueryString["ScheduleNo"].ToString());
            string dsStrResult = cpdWebService.sj_InsertNewSJ2(dt0, 0);
            if (int.Parse(dsStrResult) == 0)
            {
                DisplayAJAXMessage(this, "Gagal karena belum di-define ");
            }
            //else if (int.Parse(dsStrResult) > 0)
            //{ }
            else if (int.Parse(dsStrResult) < 0)
                switch (dsStrResult.Trim())
                {
                    case "-1":
                        DisplayAJAXMessage(this, "Gagal create Surat Jalan karena sudah dibuat Surat Jalan");
                        break;
                    case "-2":
                        DisplayAJAXMessage(this, "Gagal create Surat Jalan karena melewati waktu lama pembayaran");
                        break;
                    case "-3":
                        DisplayAJAXMessage(this, "Gagal create Surat Jalan karena melewati waktu lama pembayaran dan uang masuk pada bank-in kurang / belum ada");
                        break;
                    case "-4":
                        DisplayAJAXMessage(this, "Gagal create Surat Jalan karena melewati batas credit limit");
                        break;
                    case "-5":
                        DisplayAJAXMessage(this, "Gagal create Surat Jalan karena uang masuk pada bank-in kurang / belum ada");
                        break;
                    case "-6":
                        DisplayAJAXMessage(this, "Gagal create Surat Jalan karena gagal simpan data surat jalan");
                        break;
                }
            //string strResult = c.Tables[0].Rows[0][0].ToString();
            //iko24Okt

            else
            {
                Session["ListOfSuratJalanHeader"] = null;

                //iko24Okt di-remark
                //cpdWebService = new WebReference_HO.Service1();
                //DataSet dsScreen1 = cpdWebService.sj_RetrieveForScreen1(Request.QueryString["OPNo"].ToString(), Request.QueryString["ScheduleNo"].ToString());
                //iko24Okt

                string strKeterangan2 = dsScreen1.Tables[0].Rows[0]["Keterangan2"].ToString();
                string strNoPolisi = dsScreen1.Tables[0].Rows[0]["NoPolisi"].ToString();
                int strCustomerType = int.Parse(dsScreen1.Tables[0].Rows[0]["CustomerType"].ToString());
                string strCekProyek = dsScreen1.Tables[0].Rows[0]["CekProyek"].ToString(); //beda/sama
                DateTime scheduleDate = DateTime.Parse(dsScreen1.Tables[0].Rows[0]["ScheduleDate"].ToString());
                string strAlamatlain = dsScreen1.Tables[0].Rows[0]["AlamatLain"].ToString();
                string strAddress = dsScreen1.Tables[0].Rows[0]["Address"].ToString();

                txtKeterangan.Text = strKeterangan2 + "(" + txtScheduleNo.Text.Trim() + ")";
                txtSalesOrderNo.Text = opno;
                txtScheduleNo.Text = scheduleno;
                txtNoMobil.Text = strNoPolisi;

                Session["ListOfSuratJalanHeader"] = dsScreen1;
                Session["ListOfSuratJalanDetail"] = null;

                if (strAlamatlain != string.Empty)
                {
                    txtAlamat.Text = strAlamatlain;
                }
                else
                {
                    txtAlamat.Text = strAddress;
                }
                if (strCekProyek == "Beda")
                {
                    DisplayAJAXMessage(this, "Alamat Kirim Toko tidak sama dengan di-Master atau Bukan Proyek, cek TokoName, Address, Telepon");
                    return;
                }
                else
                    GetScheduleDetailAPI(scheduleno, opno, strCustomerType, scheduleDate);
            }
        }
    }
    public class ListScheduleCPD1 : GRCBaseDomain
    {
        public string NoAntrian { get; set; }
        public string NoSchedule { get; set; }
        public string NoOP { get; set; }
        public string ExpedisiName { get; set; }
        public string suratjalanNo { get; set; }
    }
}