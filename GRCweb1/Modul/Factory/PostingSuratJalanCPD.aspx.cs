using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;
using Factory;
using Cogs;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace GRCweb1.Modul.Factory
{
    public partial class PostingSuratJalanCPD : System.Web.UI.Page
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
                Session["sjid"] = null;
                Session["stat"] = null;
                Session["ctk"] = null;
                Session["CountCtk"] = null;

                Session["JudulReasonCancel"] = null;
                ViewState["OPNo"] = string.Empty;
                Users users = (Users)Session["Users"];
                LoadStocker();
                LoadKendaraan();
                //LoadDataGridLoading(LoadGridLoading());
                //btnGetFromDevice.Disabled = true;

                //if (!((Hashtable)Session["Rules"]).ContainsValue("Cancel Receive"))
                //{
                //    btnCancelReceive.Enabled = true;
                //}

                if (Request.QueryString["SJNo"] != null)
                {
                    LoadSuratJalan(Request.QueryString["SJNo"].ToString());
                    ZetroView zl1 = new ZetroView();
                    zl1.QueryType = Operation.CUSTOM;
                    zl1.CustomQuery =
                        "select urutanno from loadingtime where keterangan='" + txtScheduleNo.Text.Trim() + "' or keterangan='" + txtSuratJalanNo.Text.Trim() + "'";
                    SqlDataReader sdr1 = zl1.Retrieve();
                    if (sdr1.HasRows)
                    {
                        while (sdr1.Read())
                        {
                            txtUrutanNo.Text = sdr1["urutanno"].ToString().Trim();
                        }
                        txtUrutanNo_TextChanged(null, null);
                    }
                    else
                        txtUrutanNo.Text = "";
                    if (users.DeptID == 12)
                    {
                        //btnPengajuan.Visible = true;
                        btnCancel.Visible = false;
                    }
                }
                if (users.UnitKerjaID == 1 || users.UnitKerjaID == 7)
                {
                    int jumsj = 0;

                    //btnListPengajuan.Visible = true;
                    //SuratJalanFacade suratJalanFacade = new SuratJalanFacade();
                    //jumsj = suratJalanFacade.RetrievePengajuanSJ(users.UnitKerjaID);
                    //btnListPengajuan.Value = "Pengajuan SJ(" + jumsj.ToString() + ")";
                }
                //LoadPengiriman();
                if (Request.QueryString["ScheduleNo"] != null)
                {
                    #region gak pake lagi

                    //DateTime DtLimit = Convert.ToDateTime("2018-05-09 23:59:59");

                    //OPFacade opFacade = new OPFacade();
                    //OP op = opFacade.RetrieveByNo(Request.QueryString["OPNo"].ToString());
                    //if (opFacade.Error == string.Empty)
                    //{
                    //    if (op.ID > 0)
                    //    {
                    //        txtKeterangan.Text = op.Keterangan2;

                    //        txtSalesOrderNo.Text = Request.QueryString["OPNo"].ToString();
                    //        txtScheduleNo.Text = Request.QueryString["ScheduleNo"].ToString();
                    //        //tambahan 10 Feb 14 by oati
                    //        ExpedisiDetailFacade mbexpedisiFacade = new ExpedisiDetailFacade();
                    //        ExpedisiDetail mbExpedise = mbexpedisiFacade.RetrieveByNoPolisi(Request.QueryString["ScheduleNo"].ToString());
                    //        txtNoMobil.Text = mbExpedise.NoPolisi;

                    //        string cekAlamatLain = string.Empty;

                    //        if (op.AlamatLain != string.Empty)
                    //        {
                    //            txtAlamat.Text = op.AlamatLain;
                    //            //tadi cuma ini aja

                    //            #region cek alamat lain
                    //            //cek op.alamatlain dgn toko.TokoName + " " + toko.Address + " Telp. " + toko.Telepon;, iko - 22sept2014
                    //            if (op.CustomerType == 1 && op.Proyek == 1)
                    //            {
                    //                TokoFacade tokoFacade = new TokoFacade();
                    //                Toko toko = tokoFacade.RetrieveById(op.CustomerID);
                    //                if (tokoFacade.Error == string.Empty)
                    //                {
                    //                    if (toko.ID > 0)
                    //                    {
                    //                        cekAlamatLain = toko.TokoName + " " + toko.Address + " Telp. " + toko.Telepon;
                    //                    }
                    //                }

                    //                if (cekAlamatLain != op.AlamatLain)
                    //                {
                    //                    btnSave.Disabled = true;
                    //                    txtAlamat.BackColor = System.Drawing.Color.Red;
                    //                    DisplayAJAXMessage(this, "Alamat Kirim Toko tidak sama dengan di-Master atau Bukan Proyek, cek TokoName, Address, Telepon ");
                    //                    return;
                    //                }
                    //                else
                    //                {
                    //                    btnUpdate.Disabled = false;
                    //                    txtAlamat.BackColor = System.Drawing.Color.Transparent;
                    //                }
                    //            }
                    //            //cek op.alamatlain dgn toko.TokoName + " " + toko.Address + " Telp. " + toko.Telepon;, iko - 22sept2014
                    //            #endregion cek alamat lain

                    //            #region cek CL & pembayaran
                    //            //add 25April
                    //            else if (op.CustomerType == 2 && op.CreatedTime > DtLimit)
                    //            {
                    //                //cek op. luar pulau / nerita, leni minta lock credit limit & CBD
                    //                if (op.DeptID == 7)
                    //                {
                    //                    decimal creditLimitNya = 0; decimal uangMukaPersen = 0; int CekInternalMarketing = 1;
                    //                    decimal totUMBankIn = 0; int flagUM = 0; decimal totalSales = 0;
                    //                    CustomerFacade customerFacade = new CustomerFacade();
                    //                    Customer customer = customerFacade.RetrieveById(op.CustomerID);
                    //                    if (customerFacade.Error == string.Empty)
                    //                    {
                    //                        creditLimitNya = customer.CreditLimit;

                    //                        ////jika kreditlimit tidak perlu cek ke Bank-In = like di-approval OP / Nerita
                    //                        if (customer.CategoryPaymentID == 1 || customer.CategoryPaymentID == 2 || customer.CategoryPaymentID == 4)
                    //                        {
                    //                            OPDetailFacade opDetailFacade = new OPDetailFacade();
                    //                            ArrayList arrOpDetail = opDetailFacade.RetrieveNoPaketByIdRaceToPerth(op.ID);
                    //                            foreach (OPDetail OPDetailCheck in arrOpDetail)
                    //                            {
                    //                                totalSales = totalSales + OPDetailCheck.TotalPrice;
                    //                            }

                    //                            uangMukaPersen = customer.UangMukaPersen;

                    //                            UangMukaFacade umDetailFacade = new UangMukaFacade();
                    //                            Decimal TotalUM = umDetailFacade.GetBankInNo2(op.ID);
                    //                            if (TotalUM > 0)
                    //                            //    if (uMuka.ID > 0)
                    //                            {
                    //                                CekInternalMarketing = 1;

                    //                                totUMBankIn = TotalUM;  // uMuka.TotalUM;

                    //                                flagUM = 1;
                    //                            }
                    //                            else
                    //                            {
                    //                                CekInternalMarketing = 0;
                    //                            }

                    //                        }

                    //                        decimal TotInvpending = 0; decimal TotOPpending = 0;
                    //                        string strCategoryPayment = string.Empty;
                    //                        OPDetailPaymentFacade PaymentInfoFacade = new OPDetailPaymentFacade();
                    //                        ArrayList arrCekOP = PaymentInfoFacade.CekOPforCustomerOnly(op.CustomerID);
                    //                        if (arrCekOP.Count > 0)
                    //                        {
                    //                            foreach (OPDetailPaymentInfo paymentInfo in arrCekOP)
                    //                            {
                    //                                TotOPpending = TotOPpending + paymentInfo.TotOP;
                    //                            }

                    //                        }
                    //                        //cek yg tanda terima / invoice belum dibayar & jatuh tempo tgl pembayaran
                    //                        ArrayList arrCekInvoice = PaymentInfoFacade.CekInvoiceforCustomerOnly2(op.CustomerID);
                    //                        if (arrCekInvoice.Count > 0)
                    //                        {
                    //                            foreach (OPDetailPaymentInfo paymentInfo in arrCekInvoice)
                    //                            {
                    //                                if (paymentInfo.TotOP > 0)
                    //                                {
                    //                                    TotInvpending = TotInvpending + paymentInfo.TotOP;
                    //                                }
                    //                            }
                    //                        }

                    //                        if (flagUM > 0 && (customer.CategoryPaymentID == 1 || customer.CategoryPaymentID == 2))
                    //                        {
                    //                            //pengecekan per No OP
                    //                            if (totalSales > totUMBankIn)
                    //                            {
                    //                                CekInternalMarketing = 0;
                    //                                DisplayAJAXMessage(this, "Penerimaan Cash/Transfer, kurang dari Total Harga");
                    //                                return;
                    //                            }
                    //                        }
                    //                        //jika UM misalkan 30% dr total
                    //                        else if (flagUM > 0 && customer.CategoryPaymentID == 4)
                    //                        {
                    //                            if (totalSales * (uangMukaPersen / 100) > totUMBankIn)
                    //                            {
                    //                                CekInternalMarketing = 0;
                    //                                DisplayAJAXMessage(this, "Penerimaan UangMuka " + uangMukaPersen + " %, kurang dari Total Harga");
                    //                                return;
                    //                            }
                    //                        }
                    //                        if (customer.CategoryPaymentID == 4 || customer.CategoryPaymentID == 5 || customer.CategoryPaymentID == 6)
                    //                        {
                    //                            if (TotOPpending + TotInvpending > creditLimitNya)
                    //                            {
                    //                                DisplayAJAXMessage(this, "Orderan ini melewati Credit Limit yang diberikan, cek Payment Info ");
                    //                                CekInternalMarketing = -1;
                    //                                return;
                    //                            }
                    //                        }
                    //                        if (CekInternalMarketing <= 0)
                    //                        {
                    //                            btnUpdate.Disabled = true;
                    //                            if (CekInternalMarketing == -1)
                    //                            {
                    //                                DisplayAJAXMessage(this, "Data Payment Info belum di-isi dan call Admin Luar Pulau ");
                    //                                //txtStatus.Text = ((Status)op.Status).ToString() + "-Data Payment Info belum di-isi";
                    //                                return;
                    //                            }
                    //                            else
                    //                            {
                    //                                DisplayAJAXMessage(this, "Belum ada/Kurang Penerimaan Bank-In dan call Admin Luar Pulau ");
                    //                                //txtStatus.Text = ((Status)op.Status).ToString() + "-Belum ada/Kurang Penerimaan Bank-In";
                    //                                return;
                    //                            }
                    //                        }


                    //                    }
                    //                    else
                    //                    {
                    //                        DisplayAJAXMessage(this, "Data Customer tidak ada ");
                    //                        return;
                    //                    }
                    //                }
                    //            }
                    //            //add 25April
                    //            #endregion cek CL & pembayaran

                    //        }
                    //        else
                    //        {
                    //            #region satu

                    //            if (op.CustomerType == 1)
                    //            {
                    //                TokoFacade tokoFacade = new TokoFacade();
                    //                Toko toko = tokoFacade.RetrieveById(op.CustomerID);
                    //                if (tokoFacade.Error == string.Empty)
                    //                {
                    //                    if (toko.ID > 0)
                    //                    {
                    //                        txtAlamat.Text = toko.Address;

                    //                        cekAlamatLain = toko.TokoName + " " + toko.Address + " Telp. " + toko.Telepon;
                    //                    }
                    //                }

                    //                //cek op.alamatlain dgn toko.TokoName + " " + toko.Address + " Telp. " + toko.Telepon;, iko - 22sept2014
                    //                if (op.CustomerType == 1 && op.Proyek == 1)
                    //                {
                    //                    if (cekAlamatLain != op.AlamatLain)
                    //                    {
                    //                        btnSave.Disabled = true;
                    //                        txtAlamat.BackColor = System.Drawing.Color.Red;
                    //                        DisplayAJAXMessage(this, "Alamat Kirim Toko tidak sama dengan di-Master atau Bukan Proyek, cek TokoName, Address, Telepon ");
                    //                        return;
                    //                    }
                    //                    else
                    //                    {
                    //                        btnUpdate.Disabled = false;
                    //                        txtAlamat.BackColor = System.Drawing.Color.Transparent;
                    //                    }
                    //                }

                    //            }
                    //            #endregion satu

                    //            #region dua/customer
                    //            else
                    //            {
                    //                CustomerFacade customerFacade = new CustomerFacade();
                    //                Customer customer = customerFacade.RetrieveById(op.CustomerID);
                    //                if (customerFacade.Error == string.Empty)
                    //                {
                    //                    if (customer.ID > 0)
                    //                    {
                    //                        txtAlamat.Text = customer.Address;
                    //                    }
                    //                }

                    //                else if (op.CustomerType == 2 && op.CreatedTime > DtLimit)
                    //                {
                    //                    //cek op. luar pulau / nerita, leni minta lock credit limit & CBD
                    //                    if (op.DeptID == 7)
                    //                    {
                    //                        decimal creditLimitNya = 0; decimal uangMukaPersen = 0; int CekInternalMarketing = 0;
                    //                        decimal totUMBankIn = 0; int flagUM = 0; decimal totalSales = 0;
                    //                        if (customer.ID > 0)
                    //                        {
                    //                            creditLimitNya = customer.CreditLimit;

                    //                            ////jika kreditlimit tidak perlu cek ke Bank-In = like di-approval OP
                    //                            if (customer.CategoryPaymentID == 1 || customer.CategoryPaymentID == 2 || customer.CategoryPaymentID == 4)
                    //                            {
                    //                                OPDetailFacade opDetailFacade = new OPDetailFacade();
                    //                                ArrayList arrOpDetail = opDetailFacade.RetrieveNoPaketByIdRaceToPerth(op.ID);
                    //                                foreach (OPDetail OPDetailCheck in arrOpDetail)
                    //                                {
                    //                                    totalSales = totalSales + OPDetailCheck.TotalPrice;
                    //                                }

                    //                                uangMukaPersen = customer.UangMukaPersen;

                    //                                UangMukaFacade umDetailFacade = new UangMukaFacade();
                    //                                Decimal TotalUM = umDetailFacade.GetBankInNo2(op.ID);
                    //                                if (TotalUM > 0)
                    //                                //    if (uMuka.ID > 0)
                    //                                {
                    //                                    CekInternalMarketing = 1;

                    //                                    totUMBankIn = TotalUM;  // uMuka.TotalUM;

                    //                                    flagUM = 1;
                    //                                }
                    //                                else
                    //                                {
                    //                                    CekInternalMarketing = 0;
                    //                                }

                    //                            }

                    //                            decimal TotInvpending = 0; decimal TotOPpending = 0;
                    //                            string strCategoryPayment = string.Empty;
                    //                            OPDetailPaymentFacade PaymentInfoFacade = new OPDetailPaymentFacade();
                    //                            ArrayList arrCekOP = PaymentInfoFacade.CekOPforCustomerOnly(op.CustomerID);
                    //                            if (arrCekOP.Count > 0)
                    //                            {
                    //                                foreach (OPDetailPaymentInfo paymentInfo in arrCekOP)
                    //                                {
                    //                                    TotOPpending = TotOPpending + paymentInfo.TotOP;
                    //                                }

                    //                            }
                    //                            //cek yg tanda terima / invoice belum dibayar & jatuh tempo tgl pembayaran
                    //                            ArrayList arrCekInvoice = PaymentInfoFacade.CekInvoiceforCustomerOnly2(op.CustomerID);
                    //                            if (arrCekInvoice.Count > 0)
                    //                            {
                    //                                foreach (OPDetailPaymentInfo paymentInfo in arrCekInvoice)
                    //                                {
                    //                                    if (paymentInfo.TotOP > 0)
                    //                                    {
                    //                                        TotInvpending = TotInvpending + paymentInfo.TotOP;
                    //                                    }
                    //                                }
                    //                            }

                    //                            if (flagUM > 0 && (customer.CategoryPaymentID == 1 || customer.CategoryPaymentID == 2))
                    //                            {
                    //                                //pengecekan per No OP
                    //                                if (totalSales > totUMBankIn)
                    //                                {
                    //                                    CekInternalMarketing = 0;
                    //                                    DisplayAJAXMessage(this, "Penerimaan Cash/Transfer, kurang dari Total Harga");
                    //                                    return;
                    //                                }
                    //                            }
                    //                            //jika UM misalkan 30% dr total
                    //                            else if (flagUM > 0 && customer.CategoryPaymentID == 4)
                    //                            {
                    //                                if (totalSales * (uangMukaPersen / 100) > totUMBankIn)
                    //                                {
                    //                                    CekInternalMarketing = 0;
                    //                                    DisplayAJAXMessage(this, "Penerimaan UangMuka " + uangMukaPersen + " %, kurang dari Total Harga");
                    //                                    return;
                    //                                }
                    //                            }
                    //                            if (customer.CategoryPaymentID == 4 || customer.CategoryPaymentID == 5 || customer.CategoryPaymentID == 6)
                    //                            {
                    //                                if (TotOPpending + TotInvpending > creditLimitNya)
                    //                                {
                    //                                    DisplayAJAXMessage(this, "Orderan ini melewati Credit Limit yang diberikan, cek Payment Info ");
                    //                                    CekInternalMarketing = -1;
                    //                                    return;
                    //                                }

                    //                            }


                    //                            if (CekInternalMarketing <= 0)
                    //                            {
                    //                                btnUpdate.Disabled = true;
                    //                                if (CekInternalMarketing == -1)
                    //                                {
                    //                                    DisplayAJAXMessage(this, "Data Payment Info belum di-isi dan call Admin Luar Pulau ");
                    //                                    //txtStatus.Text = ((Status)op.Status).ToString() + "-Data Payment Info belum di-isi";
                    //                                    return;
                    //                                }
                    //                                else
                    //                                {
                    //                                    DisplayAJAXMessage(this, "Belum ada/Kurang Penerimaan Bank-In dan call Admin Luar Pulau ");
                    //                                    //txtStatus.Text = ((Status)op.Status).ToString() + "-Belum ada/Kurang Penerimaan Bank-In";
                    //                                    return;
                    //                                }
                    //                            }
                    //                        }
                    //                        else
                    //                        {
                    //                            DisplayAJAXMessage(this, "Data Customer tidak ada ");
                    //                            return;
                    //                        }
                    //                    }
                    //                }
                    //                //add 25April
                    //            }
                    //            #endregion dua/customer

                    //        }

                    //        GetScheduleDetail();

                    //    }
                    //}
                    #endregion gak pake lagi
                    WebReference_HO.Service1 cpdWebService = new WebReference_HO.Service1();

                    DataSet dsStrResult = cpdWebService.sj_CekCreditLimitDanTOP(Request.QueryString["OPNo"].ToString(), Request.QueryString["ScheduleNo"].ToString());
                    string strResult = dsStrResult.Tables[0].Rows[0][0].ToString();
                    if (strResult != string.Empty)
                    {
                        DisplayAJAXMessage(this, strResult);
                        return;
                    }
                    else
                    {
                        Session["ListOfSuratJalanHeader"] = null;

                        cpdWebService = new WebReference_HO.Service1();
                        DataSet dsScreen1 = cpdWebService.sj_RetrieveForScreen1(Request.QueryString["OPNo"].ToString(), Request.QueryString["ScheduleNo"].ToString());

                        string strKeterangan2 = dsScreen1.Tables[0].Rows[0]["Keterangan2"].ToString();
                        string strNoPolisi = dsScreen1.Tables[0].Rows[0]["NoPolisi"].ToString();
                        int strCustomerType = int.Parse(dsScreen1.Tables[0].Rows[0]["CustomerType"].ToString());
                        string strCekProyek = dsScreen1.Tables[0].Rows[0]["CekProyek"].ToString(); //beda/sama
                        DateTime scheduleDate = DateTime.Parse(dsScreen1.Tables[0].Rows[0]["ScheduleDate"].ToString());
                        string strAlamatlain = dsScreen1.Tables[0].Rows[0]["AlamatLain"].ToString();
                        string strAddress = dsScreen1.Tables[0].Rows[0]["Address"].ToString();

                        txtKeterangan.Text = strKeterangan2;
                        txtSalesOrderNo.Text = Request.QueryString["OPNo"].ToString();
                        txtScheduleNo.Text = Request.QueryString["ScheduleNo"].ToString();
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
        public string Plant { get { return (((Users)Session["Users"]).UserID == "7") ? "KR" : "CT"; } }
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
        //private void LoadPengiriman()
        //{
        //    ZetroView zl = new ZetroView();
        //    zl.QueryType = Operation.CUSTOM;
        //    zl.CustomQuery = 
        //        "select * from MaterialCCMatrixPJPengiriman where rowstatus>-1 and pengiriman in (select pengiriman from MaterialCCMatrixPJ where tahun=" +
        //        DateTime.Now.Year + " and pengiriman not like '%kembali%') order by urutan";
        //    SqlDataReader sdr = zl.Retrieve();
        //    ddlPengiriman.Items.Clear();
        //    ddlPengiriman.Items.Add(new ListItem("-", "0"));
        //    if (sdr.HasRows)
        //    {
        //        while (sdr.Read())
        //        {
        //            ddlPengiriman.Items.Add(new ListItem(sdr["Pengiriman"].ToString(), sdr["Pengiriman"].ToString().TrimEnd()));
        //        }
        //    }
        //}
        //private void LoadJenisPalet(string Pengiriman)
        //{
        //    ZetroView zl = new ZetroView();
        //    zl.QueryType = Operation.CUSTOM;
        //    zl.CustomQuery =
        //        "select distinct jenisPacking from MaterialCCMatrixPJ where rowstatus>-1 and pengiriman='" + Pengiriman + "' and tahun=" +
        //        DateTime.Now.Year + " order by jenisPacking";
        //    SqlDataReader sdr = zl.Retrieve();
        //    ddlJenisPalet.Items.Clear();
        //    ddlJenisPalet.Items.Add(new ListItem("-", "0"));
        //    if (sdr.HasRows)
        //    {
        //        while (sdr.Read())
        //        {
        //            ddlJenisPalet.Items.Add(new ListItem(sdr["jenisPacking"].ToString(), sdr["jenisPacking"].ToString().TrimEnd()));
        //        }
        //    }
        //}
        protected void btnPengajuan_ServerClick(object sender, EventArgs e)
        {
            string strValidate = ValidateText();
            //if (strValidate != string.Empty)
            //{
            //    DisplayAJAXMessage(this, strValidate);
            //    return;
            //}

            //SuratJalanFacade suratJalanFacade = new SuratJalanFacade();

            //SuratJalan suratJalan = suratJalanFacade.RetrieveByNo(txtSuratJalanNo.Text);

            //if (suratJalanFacade.Error == string.Empty)
            //{
            //    if (suratJalan.ID > 0)
            //    {
            //        ScheduleFacade scheFacade = new ScheduleFacade();
            //        Schedule sche = scheFacade.CekSchedule(suratJalan.ScheduleID);
            //        if (sche.ID > 0)
            //            suratJalan.DepoID = sche.DepoID;

            //        //suratJalan.AlasanCancel = Session["AlasanCancel"].ToString();
            //        //            if (string.IsNullOrEmpty(sqlDataReader["ActualShipmentDate"].ToString()))

            //        if (string.IsNullOrEmpty(suratJalan.TglKirimActual.ToString()))
            //        {
            //            suratJalan.TglKirimActual = suratJalan.ScheduleDate;
            //        }
            //        if (suratJalan.TglKirimActual == DateTime.MinValue)
            //        {
            //            suratJalan.TglKirimActual = suratJalan.ScheduleDate;
            //        }

            //        suratJalan.LastModifiedBy = ((Users)Session["Users"]).UserName;
            //        //suratJalan.TglKirimActual = suratJalan.TglKirimActual;

            //        //Session["AlasanCancel"] = string.Empty;
            //        string strError = string.Empty;
            //        ArrayList arrSuratJalanDetail = new ArrayList();
            //        if (Session["ListOfSuratJalanDetail"] != null)
            //        {
            //            ArrayList arrScheduleDetail = (ArrayList)Session["ListOfSuratJalanDetail"];
            //            foreach (SuratJalanDetail scheduleDetail in arrScheduleDetail)
            //            {
            //                SuratJalanDetail suratJalanDetail = new SuratJalanDetail();
            //                suratJalanDetail.ItemCode = scheduleDetail.ItemCode;
            //                suratJalanDetail.ItemID = scheduleDetail.ItemID;
            //                suratJalanDetail.ItemName = scheduleDetail.ItemName;
            //                suratJalanDetail.Qty = scheduleDetail.Qty;
            //                suratJalanDetail.ScheduleDetailId = scheduleDetail.ScheduleDetailId;
            //                suratJalanDetail.Paket = scheduleDetail.Paket;
            //                arrSuratJalanDetail.Add(suratJalanDetail);
            //            }
            //        }

            //        SuratJalanProcessFacade suratJalanProsessFacade = new SuratJalanProcessFacade(suratJalan, arrSuratJalanDetail, new SJNumber());

            //        if (suratJalan.ID > 0)
            //        {
            //            strError = suratJalanProsessFacade.UpdateStatusSJ();
            //            if (strError == string.Empty)
            //            {
            //                InsertLog("List Surat Jalan yg Mau Di Cancel");
            //                clearForm();
            //            }
            //        }
            //    }
            //}
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
        protected void btnSearch0_ServerClick(object sender, EventArgs e)
        {
            clearForm();
        }
        protected void btnList_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("ListPostSuratJalanNew.aspx?receive=no");
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

        protected void btnCancel_ServerClick(object sender, EventArgs e)
        {
            string strValidate = ValidateText();
            //if (strValidate != string.Empty)
            //{
            //    DisplayAJAXMessage(this, strValidate);
            //    return;
            //}
            //if (Session["AlasanCancel"] == null || Session["AlasanCancel"].ToString().Length <= 2)
            //{
            //    DisplayAJAXMessage(this, "Alasan cancel tidak boleh kosong ");
            //    return;
            //}

            //SuratJalanFacade suratJalanFacade = new SuratJalanFacade();

            //SuratJalan suratJalan = suratJalanFacade.RetrieveByNo(txtSuratJalanNo.Text);
            //if (suratJalanFacade.Error == string.Empty)
            //{
            //    if (suratJalan.ID > 0)
            //    {
            //        //cek ke NR dulu
            //        NotaReturDetailFacade nrDetFacade = new NotaReturDetailFacade();
            //        NotaReturDetail nrDetail = nrDetFacade.RetrieveByHeadSJid(suratJalan.ID);
            //        if (nrDetail.ID > 0)
            //        {
            //            DisplayAJAXMessage(this, "Surat Jalan ini sudah ada No Nota Retur ");
            //            return;
            //        }
            //        //

            //        ScheduleFacade scheFacade = new ScheduleFacade();
            //        Schedule sche = scheFacade.CekSchedule(suratJalan.ScheduleID);
            //        if (sche.ID > 0)
            //            suratJalan.DepoID = sche.DepoID;

            //        suratJalan.AlasanCancel = Session["AlasanCancel"].ToString();
            //        //            if (string.IsNullOrEmpty(sqlDataReader["ActualShipmentDate"].ToString()))

            //        if (string.IsNullOrEmpty(suratJalan.TglKirimActual.ToString()))
            //        {
            //            suratJalan.TglKirimActual = suratJalan.ScheduleDate;
            //        }
            //        if (suratJalan.TglKirimActual == DateTime.MinValue)
            //        {
            //            suratJalan.TglKirimActual = suratJalan.ScheduleDate;
            //        }

            //        suratJalan.LastModifiedBy = ((Users)Session["Users"]).UserName;
            //        //suratJalan.TglKirimActual = suratJalan.TglKirimActual;

            //        //utk update email ke IMS Dist jika SJ Cancel
            //        OPFacade opFacade = new OPFacade();
            //        OP op = opFacade.RetrieveByID(suratJalan.OPID);
            //        if (op.ID > 0)
            //        {
            //            if (op.CustomerType == 1 && op.TypeDistSub == 1)
            //            {
            //                suratJalan.DistID = op.DistSubID;
            //                suratJalan.SubDistID = 0;

            //                DistributorFacade distFacade = new DistributorFacade();
            //                Distributor dist = distFacade.GetEmailAddress(op.DistSubID);
            //                if (dist.EmailAddress.Length > 3)
            //                {
            //                    suratJalan.Pic = dist.Pic;
            //                    suratJalan.EmailAddress = dist.EmailAddress;
            //                }
            //            }
            //            else if (op.CustomerType == 1 && op.TypeDistSub == 2)
            //            {
            //                suratJalan.DistID = 0;
            //                suratJalan.SubDistID = op.DistSubID;

            //                SubDistributorFacade subFacade = new SubDistributorFacade();
            //                SubDistributor subDist = subFacade.GetEmailAddress(op.DistSubID);
            //                if (subDist.EmailAddress.Length > 3)
            //                {
            //                    suratJalan.Pic = subDist.Pic;
            //                    suratJalan.EmailAddress = subDist.EmailAddress;
            //                }
            //            }
            //        }
            //        //utk update email ke IMS Dist jika SJ Cancel


            //        Session["AlasanCancel"] = string.Empty;
            //        string strError = string.Empty;
            //        ArrayList arrSuratJalanDetail = new ArrayList();
            //        if (Session["ListOfSuratJalanDetail"] != null)
            //        {
            //            ArrayList arrScheduleDetail = (ArrayList)Session["ListOfSuratJalanDetail"];
            //            foreach (SuratJalanDetail scheduleDetail in arrScheduleDetail)
            //            {
            //                SuratJalanDetail suratJalanDetail = new SuratJalanDetail();
            //                suratJalanDetail.ItemCode = scheduleDetail.ItemCode;
            //                suratJalanDetail.ItemID = scheduleDetail.ItemID;
            //                suratJalanDetail.ItemName = scheduleDetail.ItemName;
            //                suratJalanDetail.Qty = scheduleDetail.Qty;
            //                suratJalanDetail.ScheduleDetailId = scheduleDetail.ScheduleDetailId;
            //                suratJalanDetail.Paket = scheduleDetail.Paket;

            //                arrSuratJalanDetail.Add(suratJalanDetail);
            //            }
            //        }

            //        SuratJalanProcessFacade suratJalanProsessFacade = new SuratJalanProcessFacade(suratJalan, arrSuratJalanDetail, new SJNumber());

            //        if (suratJalan.ID > 0)
            //        {
            //            strError = suratJalanProsessFacade.CancelSuratJalan();
            //            if (strError == string.Empty)
            //            {
            //                InsertLog("Cancel Surat Jalan");
            //                clearForm();
            //            }
            //        }
            //    }
            //}


        }

        protected void btnListTO_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("ListPostScheduleOP.aspx");
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

            if (Session["sjid"] != null)
            {
                suratJalan.ID = int.Parse(Session["sjid"].ToString());
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
            dt.Columns.Add("Keterangan", typeof(string));
            dt.Columns.Add("CreatedBy", typeof(string));

            DataRow row = dt.NewRow();
            row["ScheduleID"] = scheduleID;
            row["TglKirimActual"] = DateTime.Parse(txtActualKirim.Text);
            row["OPID"] = opID;
            row["PoliceCarNo"] = txtNoMobil.Text;
            row["DriverName"] = txtDriverName.Text;
            row["Keterangan"] = txtKeterangan.Text;
            row["CreatedBy"] = txtCreatedBy.Text;
            dt.Rows.Add(row);
            dt.TableName = "sj_InsertNewSJ";

            cpdWebService = new WebReference_HO.Service1();
            if (suratJalan.ID > 0)
            { }
            else
            {
                //coba insert dulu aja nanti feedback suratJalan.ID; & suratJalanProsessFacade.SuratJalanNo;
                Session["dtCetakSJ"] = null;
                string strResult = cpdWebService.sj_InsertNewSJ(dt);

                if (int.Parse(strResult) > 0)
                {
                    btnSave.Disabled = true;

                    cpdWebService = new WebReference_HO.Service1();
                    DataTable dtCetakSJ = cpdWebService.sj_CetakSJ(int.Parse(strResult));

                    string sjNo = dtCetakSJ.Rows[0]["SuratJalanNo"].ToString();
                    //txtSuratJalanNo.Text = dsResult.Tables[0].Rows[0][1].ToString();
                    //Session["SuratJalanNo"] = txtSuratJalanNo.Text;
                    Session["sjid"] = int.Parse(strResult);
                    Session["dtCetakSJ"] = dtCetakSJ;
                }
            }

            InsertLog(strEvent);
        }

        protected void btnCancelReceive_ServerClick(object sender, EventArgs e)
        {
            SuratJalan suratJalan = new SuratJalan();

            ////only for vivi aja, iko add 27Sept2014
            //if (Session["id"] != null && ((Users)Session["Users"]).DeptID == 5)
            //{
            //    SuratJalanFacade suratjalanfacade = new SuratJalanFacade();
            //    suratJalan = suratjalanfacade.RetrieveByID(int.Parse(Session["id"].ToString()));
            //    if (suratJalan.ID > 0)
            //    {
            //        if (suratJalan.Status == 2)
            //        {
            //            suratJalan.Status = 1;
            //            suratJalan.LastModifiedBy = ((Users)Session["Users"]).UserName;

            //            SuratJalanProcessFacade suratJalanProcessFacade = new SuratJalanProcessFacade(suratJalan, new ArrayList(), new SJNumber());
            //            string strError = suratJalanProcessFacade.CancelReceive();

            //            if (strError == string.Empty)
            //            {
            //                InsertLog("Cancel Receive Surat Jalan");
            //                clearForm();
            //            }
            //            else
            //                DisplayAJAXMessage(this, strError);
            //        }
            //    }
            //}
            //else
            //{
            //    DisplayAJAXMessage(this, "Only Vivi can do ");
            //}

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

            if (Session["sjid"] != null)
                suratJalan.ID = int.Parse(Session["sjid"].ToString());

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
                transferitem();
                if (ddlKendaraan.SelectedIndex > 0)
                    TransferLoadingTime();
                //InsertLog("Posting Shipment");
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
        protected void btnPosting_ServerClick(object sender, EventArgs e)
        {
            transferitem();
            TransferLoadingTime();
        }
        private void LoadSuratJalan(string strSJNo)
        {
            //SuratJalanFacade suratJalanFacade = new SuratJalanFacade();
            ////SuratJalan suratJalan = suratJalanFacade.RetrieveByNoPost(strSJNo);
            ////penambahan depoID agar hanya bisa cetak sesuai users.unitkerjaID
            SuratJalan suratJalan = new SuratJalan();
            //if (((Users)Session["Users"]).TypeUnitKerja == 2 && ((Users)Session["Users"]).UnitKerjaID == 1)
            //    suratJalan = suratJalanFacade.RetrieveByNoPost(strSJNo);
            //else
            //    suratJalan = suratJalanFacade.RetrieveByNoPostDepoID(strSJNo, ((Users)Session["Users"]).UnitKerjaID);

            WebReference_HO.Service1 cpdWebService = new WebReference_HO.Service1();
            DataSet dsStrResult = cpdWebService.sj_RetrieveByNoPostDepoID(strSJNo, ((Users)Session["Users"]).UnitKerjaID);

            int suratJalanID = int.Parse(dsStrResult.Tables[0].Rows[0]["ID"].ToString());
            int intStatus = int.Parse(dsStrResult.Tables[0].Rows[0]["Status"].ToString());

            if (suratJalanID > 0)
            //if (suratJalanFacade.Error == string.Empty && suratJalan.ID > 0)
            {
                if (intStatus == -1)
                //if (suratJalan.Status == -1)
                {
                    DisplayAJAXMessage(this, "Surat Jalan ini sudah di-Cancel ");
                    return;
                }

                Session["sjid"] = suratJalanID;           //suratJalan.ID;
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
                txtScheduleDate.Text = DateTime.Parse(dsStrResult.Tables[0].Rows[0]["ScheduleDate"].ToString()).ToString("dd-MMM-yyyy");
                //ampesini


                //OPFacade opFacade = new OPFacade();
                //OP op = opFacade.RetrieveByNo(txtSalesOrderNo.Text);
                //if (opFacade.Error == string.Empty)
                //{
                //    if (op.AlamatLain != string.Empty)
                //        txtAlamat.Text = op.AlamatLain;
                //    else
                //        txtAlamat.Text = op.Address;
                //    //  txtKeterangan.Text = op.Keterangan2;
                //}

                if (intStatus > 1)
                //if (suratJalan.Status > 1)
                {
                    if (intStatus >= 3)
                        //if (suratJalan.Status >= 3)
                        btnTurunStatus.Enabled = false;
                    else
                        btnTurunStatus.Enabled = true;

                    btnUpdate.Disabled = true;
                    btnSave.Disabled = true;
                    btnCancelReceive.Enabled = false;
                    btnCancel.Enabled = false;
                    //btnPrintLgsg.Disabled = true;

                    lbUpdateTglAktualKirim.Enabled = false;

                    //if (!((Hashtable)Session["Rules"]).ContainsValue("Cancel Receive"))
                    //{
                    //    btnCancelReceive.Enabled = true;
                    //}
                }
                else
                {
                    if (intStatus >= 1)
                    //if (suratJalan.Status >= 1)
                    {
                        if (intStatus >= 3)
                            //if (suratJalan.Status >= 3)
                            btnTurunStatus.Enabled = false;
                        else
                            btnTurunStatus.Enabled = true;

                        btnUpdate.Disabled = true;
                        btnCancel.Enabled = false;
                    }
                    else if (intStatus == -1)
                    //else if (suratJalan.Status == -1)
                    {
                        btnUpdate.Disabled = true;
                        btnSave.Disabled = true;
                        btnCancelReceive.Enabled = false;
                        btnCancel.Enabled = false;
                        btnTurunStatus.Enabled = false;
                        //btnPrintLgsg.Disabled = true;
                    }
                    else
                    {
                        btnSave.Disabled = true;
                        btnCancelReceive.Enabled = false;
                        btnTurunStatus.Enabled = false;
                    }
                }

                ArrayList arrSuratJalan = new ArrayList();
                //SuratJalanDetailFacade suratJalanDetailFacade = new SuratJalanDetailFacade();
                //ArrayList arrSuratJalan = suratJalanDetailFacade.RetrieveById(suratJalan.ID);
                //if (suratJalanDetailFacade.Error == string.Empty)
                //{
                //    Session["ListOfSuratJalanDetail"] = arrSuratJalan;
                //    GridView1.DataSource = arrSuratJalan;
                //    GridView1.DataBind();
                //}
                cpdWebService = new WebReference_HO.Service1();
                DataTable dsDetSJ = cpdWebService.sj_CetakSJ(suratJalanID);

                foreach (DataRow row in dsDetSJ.Rows)
                {
                    ScheduleDetail sDet = new ScheduleDetail();

                    sDet.ItemCode = row["ItemCode"].ToString();
                    sDet.ItemName = row["ItemName"].ToString();
                    sDet.Qty = int.Parse(row["Qty"].ToString());
                    sDet.ItemID = int.Parse(row["ItemID"].ToString());
                    arrSuratJalan.Add(sDet);
                }
                if (arrSuratJalan.Count > 0)
                {
                    Session["ListOfSuratJalanDetail"] = arrSuratJalan;
                    GridView1.DataSource = arrSuratJalan;
                    GridView1.DataBind();

                    Session["sjid"] = suratJalanID;
                    Session["dtCetakSJ"] = dsDetSJ;
                }

                Session["SuratJalanNo"] = txtSuratJalanNo.Text;

            }
            //btnCancelKirim.Disabled = btnUpdate.Disabled;
            WebReference_Krwg.Service1 webServicePusat = new WebReference_Krwg.Service1();
            DataSet SJInfo = new DataSet();
            SJInfo = webServicePusat.GetSJInfo(txtSuratJalanNo.Text);
            Session["customer"] = SJInfo.Tables[0].Rows[0]["customer"].ToString();
            LoadingTimeBySJ();
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
            Session["sjid"] = null;
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

            if (((Users)Session["Users"]).UnitKerjaID != 1 && ((Users)Session["Users"]).UnitKerjaID != 7 && ((Users)Session["Users"]).UnitKerjaID != 13)
                btnUpdate.Visible = false;
            else
                btnUpdate.Visible = true;


            btnSave.Disabled = false;
            ArrayList arrList = new ArrayList();
            arrList.Add(new SuratJalanDetail());
            GridView1.DataSource = arrList;
            GridView1.DataBind();

            Session["ldid"] = null;
            txtTglIn.Text = DateTime.Now.ToString("dd-MMM-yyyy 00:00:00");
            txtTglOut.Text = DateTime.Now.ToString("dd-MMM-yyyy 00:00:00");
            txtUrutanNo.Text = string.Empty;
            txtUrutanOut.Text = string.Empty;
            //txtNoPol.Text = string.Empty;
            ddlKendaraan.ClearSelection();
            txtUrutanOut.Text = string.Empty;
            rbMblLuar.Checked = false;
            rbMblSendiri.Checked = false;
            txtCardID.Text = string.Empty;
            lbKoneksi.Text = string.Empty;
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

                //if (scheduleDetailFacade.Error == string.Empty)
                //{
                Session["ListOfSuratJalanDetail"] = arrScheduleDetail;

                GridView1.DataSource = arrScheduleDetail;
                GridView1.DataBind();
                //}

                txtActualKirim.Text = tglKirim.ToString("dd-MMM-yyyy");
                txtScheduleDate.Text = tglKirim.ToString("dd-MMM-yyyy");

                //ScheduleFacade scheduleFacade = new ScheduleFacade();
                //Schedule schedule = scheduleFacade.RetrieveByNo(txtScheduleNo.Text);
                //if (schedule.ID > 0)
                //{
                //    txtActualKirim.Text = schedule.ScheduleDate.ToString("dd-MMM-yyyy");
                //    txtScheduleDate.Text = schedule.ScheduleDate.ToString("dd-MMM-yyyy");
                //}
                //else
                //    txtScheduleDate.Text = string.Empty;

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
            else if (txtNoMobil.Text.Trim().Length < 4)
                return "No Mobil tidak boleh kosong";
            else if (txtDriverName.Text == string.Empty)
                return "Nama Supir harus diisi";


            else if (txtUrutanNo.Text == string.Empty)
                return "No Urutan Masuk tidak boleh kosong ";
            else if (rbMblSendiri.Checked == false && rbMblLuar.Checked == false)
                return "Pilih Mobil Sendiri / Luar ";
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
            if (DateTime.Parse(txtActualKirim.Text) < DateTime.Parse(txtScheduleDate.Text))
            {
                DisplayAJAXMessage(this, "Tgl Kirim Aktual harus sama / lebih dari dari Tgl Kirim Schedule " + txtScheduleDate.Text);
                txtActualKirim.Text = txtScheduleDate.Text;
                return;
            }
            LoadingByTglIn();

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
                    #region remark
                    //arrT3SiapKirim = new ArrayList();
                    // T3_SiapKirimFacade T3_SiapKirimFacade = new T3_SiapKirimFacade();
                    // int qtyP = 0;
                    //// int qtySJ = Convert.ToInt32((GridView1.Rows[rowindex].Cells[5].Text));

                    // qtyP = T3KirimDetail.RetrieveBySJQty(txtSuratJalanNo.Text, GridView1.Rows[rowindex].Cells[0].Text, GridView1.Rows[rowindex].Cells[5].Text);
                    // GridView1.Rows[rowindex].Cells[6].Text = qtyP.ToString();
                    // luas = Convert.ToInt32(GridView1.Rows[rowindex].Cells[3].Text) * Convert.ToInt32(GridView1.Rows[rowindex].Cells[4].Text);
                    // decimal Volume = decimal.Parse(GridView1.Rows[rowindex].Cells[2].Text) * Convert.ToInt32(GridView1.Rows[rowindex].Cells[3].Text) * Convert.ToInt32(GridView1.Rows[rowindex].Cells[4].Text);
                    // if (Convert.ToInt32((GridView1.Rows[rowindex].Cells[5].Text)) > Convert.ToInt32((GridView1.Rows[rowindex].Cells[6].Text)))
                    // {
                    //     //arrT3SiapKirim = T3_SiapKirimFacade.Retrievebyluas(luas, Convert.ToInt32(GridView1.Rows[rowindex].Cells[0].Text));
                    //     //if (ChkSJNo.Checked == false)
                    //     //    arrT3SiapKirim = T3_SiapKirimFacade.RetrievebyVolume(Convert.ToInt32(Volume), Convert.ToInt32(GridView1.Rows[rowindex].Cells[0].Text));
                    //     //else
                    //     arrT3SiapKirim = T3_SiapKirimFacade.RetrievebySJNo(txtSuratJalanNo.Text.Trim(), Convert.ToInt32(GridView1.Rows[rowindex].Cells[0].Text));
                    //     Session["arrsiapkirim"] = arrT3SiapKirim;
                    //     grv.DataSource = arrT3SiapKirim;
                    //     grv.DataBind();
                    //     grv.Visible = true;
                    //     i = 0;
                    //     konvDeco = 0;
                    //     foreach (T3_SiapKirim t3siapkirim in arrT3SiapKirim)
                    //     {
                    //         TextBox txtQtyKirim = (TextBox)grv.Rows[i].FindControl("txtQtyKirim");
                    //         //if (chkDeco.Checked == true)
                    //         //    konvDeco = T3_SiapKirimFacade.GetKonversiDeco(Convert.ToInt32(grv.Rows[i].Cells[1].Text));
                    //         //else
                    //         konvDeco = 1;
                    //         if (Convert.ToInt32(grv.Rows[i].Cells[7].Text) - (qtySJ * konvDeco) >= 0)
                    //         {
                    //             txtQtyKirim.Text = (qtySJ * konvDeco).ToString();
                    //             break;
                    //         }
                    //         else
                    //         {
                    //             txtQtyKirim.Text = grv.Rows[i].Cells[7].Text;
                    //             qtySJ = (qtySJ * konvDeco) - Convert.ToInt32(grv.Rows[i].Cells[7].Text);
                    //         }
                    //         i = i + 1;
                    //     }
                    //     GridView1.Rows[rowindex].FindControl("transfer").Visible = true;
                    // }
                    // else
                    // {
                    //     ArrayList arrT3KirimDetail = new ArrayList();
                    //     //T3_KirimDetailFacade T3KirimDetail = new T3_KirimDetailFacade();
                    //     arrT3KirimDetail = T3KirimDetail.RetrieveBySJ(txtSuratJalanNo.Text, GridView1.Rows[rowindex].Cells[0].Text, GridView1.Rows[rowindex].Cells[5].Text);
                    //     grv0.DataSource = arrT3KirimDetail;
                    //     grv0.DataBind();
                    //     grv0.Visible = true;
                    //     GridView1.Rows[rowindex].FindControl("transfer").Visible = false;
                    // }
                    #endregion
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
                    t3kirim.OPNo = txtSalesOrderNo.Text;
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
                            //if (stock - Convert.ToInt32(txtQtyKirim.Text) < 0)
                            //{
                            //    DisplayAJAXMessage(this, "Stock tidak mencukupi, proses dibatalkan !");
                            //    return;
                            //}
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
                int totalkirimdet = T3_KirimDetailFacade.RetrieveBySJQtyNew(txtSuratJalanNo.Text, e.Row.Cells[0].Text);
                if (Convert.ToInt32((e.Row.Cells[3].Text)) > totalkirimdet)
                {
                    arrT3SiapKirim = T3_SiapKirimFacade.RetrievebyVolumeNew(tebal, panjang, lebar, e.Row.Cells[0].Text);
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
                }
                else
                {
                    ArrayList arrT3KirimDetail = new ArrayList();
                    arrT3KirimDetail = T3KirimDetail.RetrieveBySJ(txtSuratJalanNo.Text, e.Row.Cells[0].Text, e.Row.Cells[3].Text);
                    grv0.DataSource = arrT3KirimDetail;
                    grv0.DataBind();
                    grv0.Visible = true;
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
                t3kirim.OPNo = txtSalesOrderNo.Text;
                t3kirim.Customer = Session["customer"].ToString();//txtCustomer.Text;
                t3kirim.TglKirim = DateTime.Parse(txtActualKirim.Text);
                t3kirim.Total = 0;
                t3kirim.CreatedBy = users.UserName;
                //kirimID = t3kirimfacade.Insert(t3kirim);
                Session["kirimID"] = kirimID;
                //}
                int totpotong = 0;
                konvDeco = 0;
                int Cekitemid = 0;
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
                        Cekitemid = Cekitemid + t3siapkirim.ItemIDSer;
                        i = i + 1;
                    }
                    if (Cekitemid > 0)
                    {
                        if (totpotong + totalkirimdet != Convert.ToInt32(GridView1.Rows[j].Cells[3].Text) * konvDeco)
                        {
                            DisplayAJAXMessage(this, "Qty potong tidak memenuhi Qty SJ");
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
                                //t3serah.Qty = Convert.ToInt32(txtQtyKirim.Text);
                                //t3serah.CreatedBy = users.UserName;
                                ////intresult = SerahFacade.Insert(t3serah);
                                if (jmlstock == "cukup")
                                {
                                    //t3serah.Qty = Convert.ToInt32(txtQtyKirim.Text);
                                    //t3serah.CreatedBy = users.UserName;
                                    ////intresult = SerahFacade.Insert(t3serah);
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
                        T3_KirimProcessFacade kirim = new T3_KirimProcessFacade(t3kirim, arrKirimDetail1);
                        string strError = kirim.Insert1();
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
        protected void btnGetFromDevice_ServerClick(object sender, EventArgs e)
        {
            //try
            //{
            //    //tarik data dari device keluar
            //    int idwErrorCode = 0;
            //    lokasi = ((Users)Session["Users"]).UnitKerjaID.ToString();
            //    ipDeviceOut = GetIP("Keluar" + lokasi);
            //    iMachineNumber = int.Parse(GetIP("DevKeluar" + lokasi).ToString());
            //    bIsConnected = axCZKEM1.Connect_Net(ipDeviceOut, Convert.ToInt32(port));
            //    if (bIsConnected == true)
            //    {
            //        lbKoneksi.Text = "Terkoneksi";
            //        iMachineNumber = int.Parse(GetIP("DevKeluar" + lokasi).ToString()); ;//In fact,when you are using the tcp/ip communication,this parameter will be ignored,that is any integer will all right.Here we use 1.
            //        axCZKEM1.RegEvent(iMachineNumber, 65535);//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
            //    }
            //    else
            //    {
            //        lbKoneksi.Text = "Tidak Terkoneksi";

            //        axCZKEM1.GetLastError(ref idwErrorCode);
            //        string pesan = "Unable to connect the device,ErrorCode=" + idwErrorCode.ToString();
            //        DisplayAJAXMessage(this, pesan);

            //        axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
            //        return;
            //    }
            //    //Cursor = Cursors.Default;
            //    ////koneksi ke alat


            //    int idwTMachineNumber = 0;
            //    int idwEnrollNumber = 0;
            //    int idwEMachineNumber = 0;
            //    int idwVerifyMode = 0;
            //    int idwInOutMode = 0;
            //    int idwYear = 0;
            //    int idwMonth = 0;
            //    int idwDay = 0;
            //    int idwHour = 0;
            //    int idwMinute = 0;
            //    int iGLCount = 0;
            //    int iIndex = 0;
            //    int recordDevice = 0;

            //    //Cursor = Cursors.WaitCursor;
            //    //lvLogs.Items.Clear();
            //    axCZKEM1.EnableDevice(iMachineNumber, false);//disable the device
            //    if (axCZKEM1.GetDeviceStatus(iMachineNumber, 6, ref recordDevice))
            //    //Here we use the function "GetDeviceStatus" to get the record's count.The parameter "Status" is 6.
            //    {
            //        //MessageBox.Show("The count of the AttLogs in the device is " + iValue.ToString(), "Success");
            //    }
            //    else
            //    {
            //        axCZKEM1.GetLastError(ref idwErrorCode);
            //        //MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            //        string pesan = "Operation failed,ErrorCode=" + idwErrorCode.ToString();
            //        DisplayAJAXMessage(this, pesan);

            //        axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
            //        return;
            //    }

            //    LoadingTime loading = new LoadingTime();
            //    LoadingTimeFacade loadingFacade = new LoadingTimeFacade();

            //    if (axCZKEM1.ReadGeneralLogData(iMachineNumber))//read all the attendance records to the memory
            //    {
            //        int countInsert = 0;
            //        while (axCZKEM1.GetGeneralLogData(iMachineNumber, ref idwTMachineNumber, ref idwEnrollNumber,
            //                ref idwEMachineNumber, ref idwVerifyMode, ref idwInOutMode, ref idwYear, ref idwMonth,
            //                ref idwDay, ref idwHour, ref idwMinute))//get records from the memory
            //        {


            //            string CardNo = idwEnrollNumber.ToString();

            //            LoadingTime CekCard = loadingFacade.RetrieveByCardNo(CardNo);
            //            if (CekCard.ID == 0)
            //            {

            //                DisplayAJAXMessage(this, "No Kartu belum Terdaftar ");

            //                axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
            //                return;
            //            }

            //            loading = new LoadingTime();

            //            loading.CardNo = CardNo;
            //            loading.Tanggal = DateTime.Parse(idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString() + " " + idwHour.ToString() + ":" + idwMinute.ToString());

            //            //for tabel loadingtime
            //            loading.CardID = CekCard.CardID;
            //            loading.CreatedBy = ((Users)Session["Users"]).UserName;

            //            int idnya = loadingFacade.CekMinuteTglOutByCardID(int.Parse(CekCard.CardID));
            //            if (idnya == 0 || idnya == null)
            //                loading.Flag = 0;
            //            else
            //                loading.Flag = 1;

            //            loading.ID = idnya;
            //            //for tabel loadingtime


            //            int intResult = 0;
            //            if (CekCard.ID > 0)
            //            {
            //                intResult = loadingFacade.InsertLoadingTimeDeviceOut(loading);

            //                if (intResult > 0)
            //                {
            //                    countInsert = countInsert + 1;

            //                    LoadDataGridLoading(LoadGridLoading());
            //                }
            //                else
            //                {
            //                    DisplayAJAXMessage(this, "Error Insert ");

            //                    axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
            //                    return;
            //                }
            //            }

            //        }

            //        if (countInsert == recordDevice)
            //        {
            //            if (axCZKEM1.ClearGLog(iMachineNumber))
            //            {
            //                axCZKEM1.RefreshData(iMachineNumber);
            //            }
            //            else
            //            {
            //                axCZKEM1.GetLastError(ref idwErrorCode);
            //                string pesan = "Operation failed,ErrorCode=" + idwErrorCode.ToString();
            //                DisplayAJAXMessage(this, pesan);

            //                axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
            //                return;
            //            }
            //        }
            //        #region depreciated code
            //        //ArrayList arrLoading = new ArrayList();
            //        //arrLoading = loadingFacade.RetrieveAll();

            //        //foreach (LoadingTime lt in arrLoading)
            //        //{

            //        //    loading = loadingFacade.RetrieveByNoUrutOut(lt.NoUrut);

            //        //    if (loading.ID > 0 && loading.TimeOut == DateTime.MinValue)
            //        //    {
            //        //        LoadingTime cekLoadingTimeDevice = loadingFacade.RetrieveLoadingTimeDeviceByCardNo(int.Parse(loading.NoUrut), loading.TimeIn.ToString());
            //        //        if (cekLoadingTimeDevice.CardNo > 0)
            //        //        {
            //        //            Session["id"] = loading.ID;
            //        //            //loading.TimeOut = DateTime.Parse(idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString() + " " + idwHour.ToString() + ":" + idwMinute.ToString());
            //        //            loading.TimeOut = cekLoadingTimeDevice.TimeOut;
            //        //            //timeOut ambil 

            //        //            int intResult = 0;
            //        //            if (loading.ID > 0)
            //        //            {
            //        //                intResult = loadingFacade.UpdateFromDevice(loading);
            //        //            }

            //        //            if (intResult < 0)
            //        //            {
            //        //                DisplayAJAXMessage(this, "Error Update ke Grid ");
            //        //                //clearForm();

            //        //                axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
            //        //                return;

            //        //            }
            //        //        }

            //        //    }
            //        //}


            //        //int idwEnrollNumber1 = 0;
            //        //string sName = "";
            //        //string sPassword = "";
            //        //int iPrivilege = 0;
            //        //bool bEnabled = false;
            //        //string sCardnumber = "";


            //        //axCZKEM1.EnableDevice(iMachineNumber, false);//disable the device
            //        //axCZKEM1.ReadAllUserID(iMachineNumber);//read all the user information to the memory

            //        ////get user information from memory
            //        //while (axCZKEM1.GetAllUserInfo(iMachineNumber, ref idwEnrollNumber1, ref sName, ref sPassword, ref iPrivilege, ref bEnabled))
            //        //{
            //        //    if (idwEnrollNumber == idwEnrollNumber1)
            //        //    {
            //        //        if (axCZKEM1.GetStrCardNumber(out sCardnumber))//get the card number from the memory
            //        //        {
            //        //            //ListViewItem list = new ListViewItem();
            //        //            //list.Text = idwEnrollNumber.ToString();
            //        //            //list.SubItems.Add(sName);
            //        //            //list.SubItems.Add(sCardnumber);
            //        //            //list.SubItems.Add(iPrivilege.ToString());
            //        //            //list.SubItems.Add(sPassword);
            //        //            //if (bEnabled == true)
            //        //            //{
            //        //            //    list.SubItems.Add("true");
            //        //            //}
            //        //            //else
            //        //            //{
            //        //            //    list.SubItems.Add("false");
            //        //            //}
            //        //        }
            //        //    }
            //        //}


            //        //}
            //        #endregion
            //    }
            //    else
            //    {
            //        //Cursor = Cursors.Default;
            //        axCZKEM1.GetLastError(ref idwErrorCode);

            //        if (idwErrorCode != 0)
            //        {
            //            //MessageBox.Show("Reading data from terminal failed,ErrorCode: " + idwErrorCode.ToString(), "Error");
            //            string pesan = "Reading data from terminal failed,ErrorCode: " + idwErrorCode.ToString();
            //            DisplayAJAXMessage(this, pesan);
            //        }
            //        else
            //        {
            //            #region depreciated code
            //            //seandaikan dari Device udah ke Tarik tp error pas Update TimeOut maka update lewat sini
            //            int flag1 = 0;
            //            //ArrayList arrLoading = new ArrayList();
            //            //arrLoading = loadingFacade.RetrieveAll();

            //            //foreach (LoadingTime lt in arrLoading)
            //            //{

            //            //    loading = loadingFacade.RetrieveByNoUrutOut(lt.NoUrut);

            //            //    if (loading.ID > 0 && loading.TimeOut == DateTime.MinValue)
            //            //    {
            //            //        LoadingTime cekLoadingTimeDevice = loadingFacade.RetrieveLoadingTimeDeviceByCardNo(int.Parse(loading.NoUrut), loading.TimeIn.ToString());
            //            //        if (cekLoadingTimeDevice.CardNo > 0)
            //            //        {
            //            //            Session["id"] = loading.ID;
            //            //            //loading.TimeOut = DateTime.Parse(idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString() + " " + idwHour.ToString() + ":" + idwMinute.ToString());
            //            //            loading.TimeOut = cekLoadingTimeDevice.TimeOut;
            //            //            //timeOut ambil 

            //            //            int intResult = 0;
            //            //            if (loading.ID > 0)
            //            //            {
            //            //                intResult = loadingFacade.UpdateFromDevice(loading);
            //            //                flag1 = 1;
            //            //            }

            //            //            if (intResult < 0)
            //            //            {
            //            //                DisplayAJAXMessage(this, "Error Update ke Grid ");
            //            //                //clearForm();

            //            //                axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
            //            //                return;

            //            //            }
            //            //        }

            //            //    }
            //            //}
            //            //seandaikan dari Device udah ke Tarik tp error pas Update TimeOut maka update lewat sini

            //            //MessageBox.Show("No data from terminal returns!", "Error");
            //            #endregion
            //            if (flag1 == 0)
            //            {
            //                string pesan = "No data from terminal returns !";
            //                DisplayAJAXMessage(this, pesan);
            //            }
            //            //else
            //            //{
            //            //    string pesan = "Update from LoadingTimeDevice berhasil ";
            //            //    DisplayAJAXMessage(this, pesan);
            //            //}
            //        }
            //    }


            //    axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
            //                                                //Cursor = Cursors.Default;
            //}
            //catch { }

        }
        protected void btnGetFromDeviceIn_ServerClick(object sender, EventArgs e)
        {
            //try
            //{
            //    //tarikdata dari device masuk (sekurity)
            //    int idwErrorCode = 0;
            //    lokasi = ((Users)Session["Users"]).UnitKerjaID.ToString();
            //    ipDeviceIn = GetIP("Masuk" + lokasi);
            //    iMachineNumber = int.Parse(GetIP("DevMasuk" + lokasi));
            //    bIsConnected = axCZKEM1.Connect_Net(ipDeviceIn, Convert.ToInt32(port));
            //    if (bIsConnected == true)
            //    {
            //        lbKoneksi.Text = "Terkoneksi";
            //        iMachineNumber = int.Parse(GetIP("DevMasuk" + lokasi)); //In fact,when you are using the tcp/ip communication,this parameter will be ignored,that is any integer will all right.Here we use 1.
            //        axCZKEM1.RegEvent(iMachineNumber, 65535);//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
            //    }
            //    else
            //    {
            //        lbKoneksi.Text = "Tidak Terkoneksi";

            //        axCZKEM1.GetLastError(ref idwErrorCode);
            //        string pesan = "Unable to connect the device,ErrorCode=" + idwErrorCode.ToString();
            //        DisplayAJAXMessage(this, pesan);

            //        axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
            //        return;
            //    }
            //    //Cursor = Cursors.Default;
            //    ////koneksi ke alat


            //    int idwTMachineNumber = 0;
            //    int idwEnrollNumber = 0;
            //    int idwEMachineNumber = 0;
            //    int idwVerifyMode = 0;
            //    int idwInOutMode = 0;
            //    int idwYear = 0;
            //    int idwMonth = 0;
            //    int idwDay = 0;
            //    int idwHour = 0;
            //    int idwMinute = 0;
            //    int iGLCount = 0;
            //    int iIndex = 0;
            //    int recordDevice = 0;

            //    //Cursor = Cursors.WaitCursor;
            //    //lvLogs.Items.Clear();
            //    axCZKEM1.EnableDevice(iMachineNumber, false);//disable the device
            //    if (axCZKEM1.GetDeviceStatus(iMachineNumber, 6, ref recordDevice)) //Here we use the function "GetDeviceStatus" to get the record's count.The parameter "Status" is 6.
            //    {
            //        //MessageBox.Show("The count of the AttLogs in the device is " + iValue.ToString(), "Success");
            //    }
            //    else
            //    {
            //        axCZKEM1.GetLastError(ref idwErrorCode);
            //        //MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            //        string pesan = "Operation failed,ErrorCode=" + idwErrorCode.ToString();
            //        DisplayAJAXMessage(this, pesan);

            //        axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
            //        return;
            //    }

            //    LoadingTime loading = new LoadingTime();
            //    LoadingTimeFacade loadingFacade = new LoadingTimeFacade();

            //    if (axCZKEM1.ReadGeneralLogData(iMachineNumber))//read all the attendance records to the memory
            //    {
            //        int countInsert = 0;
            //        while (axCZKEM1.GetGeneralLogData(iMachineNumber, ref idwTMachineNumber, ref idwEnrollNumber,
            //                ref idwEMachineNumber, ref idwVerifyMode, ref idwInOutMode, ref idwYear, ref idwMonth, ref idwDay, ref idwHour, ref idwMinute))//get records from the memory
            //        {
            //            #region depreciated line
            //            //iGLCount++;
            //            //lvLogs.Items.Add(iGLCount.ToString());
            //            //lvLogs.Items[iIndex].SubItems.Add(idwEnrollNumber.ToString());
            //            //lvLogs.Items[iIndex].SubItems.Add(idwVerifyMode.ToString());
            //            //lvLogs.Items[iIndex].SubItems.Add(idwInOutMode.ToString());
            //            //lvLogs.Items[iIndex].SubItems.Add(idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString() + " " + idwHour.ToString() + ":" + idwMinute.ToString());
            //            //iIndex++;
            //            #endregion
            //            string CardNo = idwEnrollNumber.ToString();

            //            //cek udah ada di-loadingtimeCard
            //            LoadingTime CekCard = loadingFacade.RetrieveByCardNo(CardNo);
            //            if (CekCard.ID == 0)
            //            {
            //                //txtCardID.Text = string.Empty;
            //                //txtUrutanNo.Focus();
            //                DisplayAJAXMessage(this, "No Kartu belum Terdaftar ");

            //                axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
            //                return;
            //            }

            //            loading = new LoadingTime();

            //            loading.CardNo = CardNo;
            //            loading.Tanggal = DateTime.Parse(idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString() + " " + idwHour.ToString() + ":" + idwMinute.ToString());

            //            //for tabel loadingtime
            //            loading.CardID = CekCard.CardID;

            //            string strNo = string.Empty;
            //            int nobaru = 0;
            //            LoadingTime cekno = loadingFacade.GetLastNumber();
            //            if (loadingFacade.Error == string.Empty)
            //            {
            //                strNo = cekno.LoadingNo;
            //            }
            //            else
            //            {
            //                DisplayAJAXMessage(this, loadingFacade.Error);
            //            }
            //            if (strNo == string.Empty) strNo = "0";
            //            nobaru = Convert.ToInt32(strNo) + 1;
            //            loading.LoadingNo = nobaru.ToString().PadLeft(6, '0');
            //            loading.CreatedBy = ((Users)Session["Users"]).UserName;

            //            DateTime tglIN = loadingFacade.CekMinuteTglInByCardIDTglIn(int.Parse(CekCard.CardID), DateTime.Parse(idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString()).ToString("yyyyMMdd"));
            //            if (tglIN != DateTime.MinValue)
            //            {
            //                if (((idwHour * 60) + idwMinute) - ((tglIN.Hour * 60) + tglIN.Minute) <= 1)
            //                    loading.Flag = 0;
            //                else
            //                    loading.Flag = 1;
            //            }
            //            //for tabel loadingtime

            //            int intResult = 0;
            //            if (CekCard.ID > 0)
            //            {

            //                intResult = loadingFacade.InsertLoadingTimeDeviceIn(loading);

            //                if (intResult > 0)
            //                {
            //                    countInsert = countInsert + 1;

            //                    LoadDataGridLoading(LoadGridLoading());
            //                    btnGetFromDevice.Disabled = false;
            //                }
            //                else
            //                {
            //                    DisplayAJAXMessage(this, "Error Insert ");

            //                    axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
            //                    return;
            //                }
            //            }

            //        }

            //        if (countInsert == recordDevice)
            //        {
            //            if (axCZKEM1.ClearGLog(iMachineNumber))
            //            {
            //                axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
            //                                                     //MessageBox.Show("All att Logs have been cleared from teiminal!", "Success");
            //            }
            //            else
            //            {
            //                axCZKEM1.GetLastError(ref idwErrorCode);
            //                //MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            //                string pesan = "Operation failed,ErrorCode=" + idwErrorCode.ToString();
            //                DisplayAJAXMessage(this, pesan);

            //                axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
            //                return;
            //            }
            //        }
            //        #region depreciated code
            //        //ArrayList arrLoading = new ArrayList();
            //        //arrLoading = loadingFacade.RetrieveAll();

            //        //foreach (LoadingTime lt in arrLoading)
            //        //{

            //        //    loading = loadingFacade.RetrieveByNoUrutOut(lt.NoUrut);

            //        //    if (loading.ID > 0 && loading.TimeOut == DateTime.MinValue)
            //        //    {
            //        //        LoadingTime cekLoadingTimeDevice = loadingFacade.RetrieveLoadingTimeDeviceByCardNo(int.Parse(loading.NoUrut), loading.TimeIn.ToString());
            //        //        if (cekLoadingTimeDevice.CardNo > 0)
            //        //        {
            //        //            Session["id"] = loading.ID;
            //        //            //loading.TimeOut = DateTime.Parse(idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString() + " " + idwHour.ToString() + ":" + idwMinute.ToString());
            //        //            loading.TimeOut = cekLoadingTimeDevice.TimeOut;
            //        //            //timeOut ambil 

            //        //            int intResult = 0;
            //        //            if (loading.ID > 0)
            //        //            {
            //        //                intResult = loadingFacade.UpdateFromDevice(loading);
            //        //            }

            //        //            if (intResult < 0)
            //        //            {
            //        //                DisplayAJAXMessage(this, "Error Update ke Grid ");

            //        //                axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
            //        //                return;

            //        //            }
            //        //        }

            //        //    }
            //        //}
            //        #endregion
            //    }
            //    else
            //    {
            //        //Cursor = Cursors.Default;
            //        axCZKEM1.GetLastError(ref idwErrorCode);

            //        if (idwErrorCode != 0)
            //        {
            //            //MessageBox.Show("Reading data from terminal failed,ErrorCode: " + idwErrorCode.ToString(), "Error");
            //            string pesan = "Reading data from terminal failed,ErrorCode: " + idwErrorCode.ToString();
            //            DisplayAJAXMessage(this, pesan);
            //        }
            //        else
            //        {
            //            ////seandaikan dari Device udah ke Tarik tp error pas Update TimeOut maka update lewat sini
            //            int flag1 = 0;
            //            #region depreciated code
            //            //ArrayList arrLoading = new ArrayList();
            //            //arrLoading = loadingFacade.RetrieveAll();

            //            //foreach (LoadingTime lt in arrLoading)
            //            //{

            //            //    loading = loadingFacade.RetrieveByNoUrutOut(lt.NoUrut);

            //            //    if (loading.ID > 0 && loading.TimeOut == DateTime.MinValue)
            //            //    {
            //            //        LoadingTime cekLoadingTimeDevice = loadingFacade.RetrieveLoadingTimeDeviceByCardNo(int.Parse(loading.NoUrut), loading.TimeIn.ToString());
            //            //        if (cekLoadingTimeDevice.CardNo > 0)
            //            //        {
            //            //            Session["id"] = loading.ID;
            //            //            //loading.TimeOut = DateTime.Parse(idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString() + " " + idwHour.ToString() + ":" + idwMinute.ToString());
            //            //            loading.TimeOut = cekLoadingTimeDevice.TimeOut;
            //            //            //timeOut ambil 

            //            //            int intResult = 0;
            //            //            if (loading.ID > 0)
            //            //            {
            //            //                intResult = loadingFacade.UpdateFromDevice(loading);
            //            //                flag1 = 1;
            //            //            }

            //            //            if (intResult < 0)
            //            //            {
            //            //                DisplayAJAXMessage(this, "Error Update ke Grid ");

            //            //                axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
            //            //                return;
            //            //            }
            //            //        }

            //            //    }
            //            //}
            //            ////seandaikan dari Device udah ke Tarik tp error pas Update TimeOut maka update lewat sini

            //            //MessageBox.Show("No data from terminal returns!", "Error");
            //            #endregion
            //            if (flag1 == 0)
            //            {
            //                string pesan = "No data from terminal returns !";
            //                DisplayAJAXMessage(this, pesan);
            //                btnGetFromDevice.Disabled = false;
            //            }
            //            //else
            //            //{
            //            //    string pesan = "Update from LoadingTimeDevice berhasil ";
            //            //    DisplayAJAXMessage(this, pesan);
            //            //}

            //        }
            //    }


            //    axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
            //                                                //Cursor = Cursors.Default;
            //}
            //catch { }

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
            bpas_api.WebService1 api = new bpas_api.WebService1();
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
            try
            {
                //arrLd = ld.RetrieveByNoUrut3New(txtScheduleNo.Text, DateTime.Parse(txtActualKirim.Text).ToString("yyyyMMdd"));
                arrLd = ld.RetrieveByNoUrut3New2(txtScheduleNo.Text);
                GridView2.DataSource = arrLd;
                GridView2.DataBind();
                txtUrutanOut.Text = txtUrutanNo.Text;
                txtTglIn.Text = GridView2.Rows[0].Cells[2].Text;
                txtTglOut.Text = GridView2.Rows[0].Cells[3].Text;
                if (txtUrutanNo.Text.Substring(0, 1) == "A" || txtUrutanNo.Text.Substring(0, 1) == "B")
                {
                    txtTglIn.ReadOnly = true;
                    txtTglOut.ReadOnly = true;
                }
                else
                {
                    txtTglIn.ReadOnly = false;
                    txtTglOut.ReadOnly = false;
                }
            }
            catch { }
        }
        protected void LoadingByTglIn()
        {
            ArrayList arrLd = new ArrayList();
            LoadingTimeFacade ld = new LoadingTimeFacade();
            //arrLd = ld.RetrieveByTglIn(DateTime.Parse(txtActualKirim.Text).ToString("yyyyMMdd"));
            arrLd = ld.RetrieveByNoSchedule(txtScheduleNo.Text);
            GridView2.DataSource = arrLd;
            GridView2.DataBind();

        }
        protected void LoadingTimeBySJ()
        {
            ArrayList arrLd = new ArrayList();
            LoadingTimeFacade ld = new LoadingTimeFacade();
            arrLd = ld.RetrieveByNoSchedule(txtScheduleNo.Text);
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
                //btnGetFromDevice.Disabled = true;

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
            Loading.TKirim = tkirim;
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
            Loading.Keterangan = txtScheduleNo.Text;

            string strError = string.Empty;
            int intResult = 0;

            //if (Loading.ID > 0)
            //{
            intResult = LoadingFacade.UpdateLoadingtimeBySJ1(Loading);
            //}
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