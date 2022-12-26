using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
namespace GRCweb1.Modul.Purchasing
{
    public partial class FormPostingSupplier : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ddlBulan.SelectedIndex = 0;
                txtTahun.Text = DateTime.Now.Year.ToString();

            }
        }


        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            try
            {
                int cekNumber = int.Parse(txtTahun.Text);
            }
            catch
            {
                DisplayAJAXMessage(this, "Tahun harus angka");
                return;
            }

            if (ddlBulan.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Pilih Bulan");
                return;

            }

            string ThBl = txtTahun.Text.ToString() + ddlBulan.SelectedIndex.ToString().PadLeft(2, '0');
            int groupID = 0;

            int tahun = 0;
            if (ddlBulan.SelectedIndex == 1)
                tahun = int.Parse(txtTahun.Text) - 1;
            else
                tahun = int.Parse(txtTahun.Text);

            string strLastMonth = string.Empty;
            if (ddlBulan.SelectedIndex == 1)
            {
                strLastMonth = "Des";
            }
            else if (ddlBulan.SelectedIndex == 2)
            {
                strLastMonth = "Jan";
            }
            else if (ddlBulan.SelectedIndex == 3)
            {
                strLastMonth = "Feb";
            }
            else if (ddlBulan.SelectedIndex == 4)
            {
                strLastMonth = "Mar";
            }
            else if (ddlBulan.SelectedIndex == 5)
            {
                strLastMonth = "Apr";
            }
            else if (ddlBulan.SelectedIndex == 6)
            {
                strLastMonth = "Mei";
            }
            else if (ddlBulan.SelectedIndex == 7)
            {
                strLastMonth = "Jun";
            }
            else if (ddlBulan.SelectedIndex == 8)
            {
                strLastMonth = "Jul";
            }
            else if (ddlBulan.SelectedIndex == 9)
            {
                strLastMonth = "Agu";
            }
            else if (ddlBulan.SelectedIndex == 10)
            {
                strLastMonth = "Sep";
            }
            else if (ddlBulan.SelectedIndex == 11)
            {
                strLastMonth = "Okt";
            }
            else if (ddlBulan.SelectedIndex == 12)
            {
                strLastMonth = "Nov";
            }

            //kosongkan dahulu per bln proses
            string strError1 = string.Empty;
            SaldoSupplier saldoKosongkan = new SaldoSupplier();
            saldoKosongkan.YearPeriod = int.Parse(txtTahun.Text);
            saldoKosongkan.MonthPeriod = ddlBulan.SelectedIndex;

            SaldoSupplierProcessFacade saldoSupKosongkanProcessFacade = new SaldoSupplierProcessFacade(saldoKosongkan);

            strError1 = saldoSupKosongkanProcessFacade.Kosongkan();
            if (strError1 != string.Empty)
            {
                DisplayAJAXMessage(this, "Posting Kosongkan Saldo Error ...!");
                return;
            }

            //new add update saldo bulan lalu
            if (strError1 == string.Empty)
            {
                SaldoSupplier saldoSupplier = new SaldoSupplier();
                string strError = string.Empty;

                saldoSupplier.YearPeriod = int.Parse(txtTahun.Text);
                saldoSupplier.MonthPeriod = ddlBulan.SelectedIndex;

                SaldoSupplierProcessFacade saldoSupplierProcessFacade = new SaldoSupplierProcessFacade(saldoSupplier);
                strError = saldoSupplierProcessFacade.UpdateSaldoBlnLalu();
                if (strError != string.Empty)
                {
                    DisplayAJAXMessage(this, "Posting Supplier Error utk saldo bulan lalu ...!");
                    return;
                }
            }
            // until new add update saldo bulan lalu

            //cek SupplierID baru pada SuppPurch
            SuppPurchFacade supplierFacade = new SuppPurchFacade();
            ArrayList arrSupplierIDbaru = supplierFacade.RetrieveBySupplierIDBaru();

            if (supplierFacade.Error == string.Empty && arrSupplierIDbaru.Count > 0)
            {
                SaldoSupplier saldoSupplier = new SaldoSupplier();
                foreach (SuppPurch supplier in arrSupplierIDbaru)
                {
                    string strError = string.Empty;
                    saldoSupplier = new SaldoSupplier();

                    if (supplier.ID > 0)
                    {
                        saldoSupplier.SupplierID = supplier.ID;
                        saldoSupplier.YearPeriod = int.Parse(txtTahun.Text);

                        SaldoSupplierProcessFacade saldoSupplierProcessFacade = new SaldoSupplierProcessFacade(saldoSupplier);
                        strError = saldoSupplierProcessFacade.Insert();
                        if (strError != string.Empty)
                        {
                            DisplayAJAXMessage(this, "Posting Supplier Error utk Supplier Baru...!");
                            return;
                        }
                    }
                }

            }
            // until new add update saldo bulan lalu

            //add data receipt per SupplierID
            //new logik
            ReceiptDetailFacade receiptDetailFacade = new ReceiptDetailFacade();
            ArrayList arrReceiptDetail = receiptDetailFacade.RetrieveBySupplierIDwithSUM(ThBl);
            if (receiptDetailFacade.Error == string.Empty)
            {
                foreach (ReceiptDetail rcpDetail in arrReceiptDetail)
                {
                    if (rcpDetail.SupplierID > 0)
                    {
                        SaldoSupplier saldoSupplier = new SaldoSupplier();
                        string strError = string.Empty;

                        saldoSupplier.YearPeriod = int.Parse(txtTahun.Text);
                        saldoSupplier.MonthPeriod = ddlBulan.SelectedIndex;

                        saldoSupplier.SupplierID = rcpDetail.SupplierID;
                        saldoSupplier.Price = rcpDetail.TotalPrice;
                        saldoSupplier.Posting = 0;
                        string test = rcpDetail.TotalPrice.ToString();
                        string teqst = rcpDetail.SupplierID.ToString();

                        SaldoSupplierProcessFacade saldoSupplierProcessFacade = new SaldoSupplierProcessFacade(saldoSupplier);
                        strError = saldoSupplierProcessFacade.Update();
                        if (strError != string.Empty)
                        {
                            DisplayAJAXMessage(this, "Posting Receipt Error ...!");
                            return;
                        }
                    }
                }
            }
            //until new logik


            //kok error ajax ya


            //tinggal bankOut utk kurangi dgn WebService
            //

            WebReference_HO.Service1 webserviceHO = new WebReference_HO.Service1();


            //yg skrg pake BankOutDate, nanti ubah ke TglRelease
            DataSet dsListBankOut = webserviceHO.GetBPASBankOutByMonth(ThBl, "K");
            if (dsListBankOut.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dsListBankOut.Tables[0].Rows)
                {
                    string strError = string.Empty;
                    SaldoSupplier saldoSupplier = new SaldoSupplier();
                    saldoSupplier.SupplierID = int.Parse(row["SupplierID"].ToString());
                    saldoSupplier.YearPeriod = int.Parse(txtTahun.Text);
                    saldoSupplier.MonthPeriod = ddlBulan.SelectedIndex;
                    saldoSupplier.Price = decimal.Parse(row["Bayar"].ToString());
                    saldoSupplier.Posting = 0;
                    SaldoSupplierProcessFacade saldoSupplierProcessFacade = new SaldoSupplierProcessFacade(saldoSupplier);
                    strError = saldoSupplierProcessFacade.MinusSaldo();
                    if (strError != string.Empty)
                    {
                        DisplayAJAXMessage(this, "Posting Pembayaran / Bank-Out Error ...!");
                        return;
                    }
                }
            }

            DisplayAJAXMessage(this, "Posting selesai ...!");
            ddlBulan.SelectedIndex = 0;
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

    }
}