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
    public partial class Pengiriman : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                txtDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtDate0.Text = DateTime.Now.AddDays(1).ToString("dd-MMM-yyyy");
                //AutoCompleteExtender1.ContextKey = "toko";
                clearform();
            }
        }

        private void clearform()
        {
            Session["arrsiapkirim"] = null;
            txtSJNo.Text = string.Empty;
            txtOPNo.Text = string.Empty;
            txtCustomer.Text = string.Empty;
            LoadDataGridViewKirim();
            txtSJNo.Focus();
        }

        private void LoadDataGridViewSJ()
        {
            if (RBExport.Checked == false)
            {
                DataSet arrT3SJDetail = new DataSet();
                if (RBOffLine.Checked == false)
                {
                    try
                    {
                        WebReference_Krwg.Service1 webServicePusat = new WebReference_Krwg.Service1();

                        if (txtSJNo.Text == string.Empty)
                            return;
                        arrT3SJDetail = webServicePusat.GetSjDetail(txtSJNo.Text);
                    }
                    catch
                    {
                        DisplayAJAXMessage(this, "Koneksi Internet Error");
                    }
                }
                else
                {
                    WebserviceFacade webServicePusat = new WebserviceFacade();
                    if (txtSJNo.Text == string.Empty)
                        return;
                    arrT3SJDetail = webServicePusat.GetSjDetail(txtSJNo.Text);
                }
                GridViewSJ.DataSource = arrT3SJDetail;
                GridViewSJ.DataBind();
            }
            else
            {
                if (RBExport.Checked == true)
                    LoadSJExDetail(txtSJNo.Text);
                if (RBCuma.Checked == true)
                    LoadSJCMDetail(txtSJNo.Text);
            }
        }

        private void LoadDataGridViewSJTO()
        {
            DataSet arrT3SJDetail = new DataSet();

            if (RBOffLine.Checked == false)
            {
                try
                {
                    WebReference_Krwg.Service1 webServicePusat = new WebReference_Krwg.Service1();
                    if (txtSJNo.Text == string.Empty)
                        return;
                    arrT3SJDetail = webServicePusat.GetSjDetailTO(txtSJNo.Text);
                }
                catch
                {
                    DisplayAJAXMessage(this, "Koneksi Internet Error");
                }
            }
            else
            {
                WebserviceFacade webServicePusat = new WebserviceFacade();
                if (txtSJNo.Text == string.Empty)
                    return;
                arrT3SJDetail = webServicePusat.GetSjDetailTO(txtSJNo.Text);
            }
            GridViewSJ.DataSource = arrT3SJDetail;
            GridViewSJ.DataBind();
        }

        private void LoadDataGridViewKirim()
        {
            ArrayList arrT3KirimDetail = new ArrayList();
            T3_KirimDetailFacade T3KirimDetail = new T3_KirimDetailFacade();
            if (txtDate0.Text != string.Empty)
            {
                arrT3KirimDetail = T3KirimDetail.RetrieveByTgl(DateTime.Parse(txtDate0.Text).ToString("yyyyMMdd"));
                GridViewKirim.DataSource = arrT3KirimDetail;
                GridViewKirim.DataBind();
            }
        }
        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearform();
        }

        protected void btnRefresh_ServerClick(object sender, EventArgs e)
        {
            LoadDataGridViewKirim();
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {

        }

        protected void ChkHide1_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkHide1.Checked == false)
                Panel8.Visible = false;
            else
                Panel8.Visible = true;
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtOPNo_TextChanged(object sender, EventArgs e)
        {
            txtCustomer.Focus();
        }
        protected void LoadSJ()
        {
            DataSet SJInfo = new DataSet();
            T3_Kirim t3kirim = new T3_Kirim();
            T3_KirimFacade t3kirimF = new T3_KirimFacade();
            t3kirim = t3kirimF.RetrieveBySJNo(txtSJNo.Text);

            if (RBExport.Checked == false && RBCuma.Checked == false)
            {
                if (RBOffLine.Checked == false)
                {
                    //try
                    //{
                    WebReference_Krwg.Service1 webServicePusat = new WebReference_Krwg.Service1();
                    if (txtSJNo.Text == string.Empty)
                    {
                        GridViewSJ.Visible = false;
                        clearform();
                        return;
                    }
                    Session["kirimID"] = null;
                    if (RBToko.Checked == true)
                    {
                        SJInfo = webServicePusat.GetSJInfo(txtSJNo.Text);
                        if (SJInfo.Tables[0].Rows.Count > 0)
                        {
                            txtOPNo.Text = SJInfo.Tables[0].Rows[0]["OPNo"].ToString();
                            txtDate.Text = Convert.ToDateTime(SJInfo.Tables[0].Rows[0]["tglkirim"]).ToString("dd-MMM-yyyy");
                            if (t3kirim.SJNo.Trim() != string.Empty)
                                txtDate0.Text = t3kirim.TglKirim.ToString("dd-MMM-yyyy");
                            else
                                txtDate0.Text = DateTime.Now.AddDays(1).ToString("dd-MMM-yyyy");
                            txtCustomer.Text = SJInfo.Tables[0].Rows[0]["customer"].ToString();
                            GridViewSJ.Visible = true;
                            LoadDataGridViewSJ();
                        }
                        else
                        {
                            txtOPNo.Text = "";
                            txtDate.Text = "";
                            txtCustomer.Text = "";
                            GridViewSJ.Visible = false;
                            DisplayAJAXMessage(this, "Data surat jalan kosong");
                        }
                    }
                    else
                    {
                        SJInfo = webServicePusat.GetSJInfoTO(txtSJNo.Text);
                        if (SJInfo.Tables[0].Rows.Count > 0)
                        {
                            txtOPNo.Text = SJInfo.Tables[0].Rows[0]["OPNo"].ToString();
                            txtDate.Text = SJInfo.Tables[0].Rows[0]["tglkirim"].ToString();
                            if (t3kirim.SJNo.Trim() != string.Empty)
                                txtDate0.Text = t3kirim.TglKirim.ToString("dd-MMM-yyyy");
                            else
                                txtDate0.Text = DateTime.Now.AddDays(-1).ToString("dd-MMM-yyyy");
                            txtCustomer.Text = SJInfo.Tables[0].Rows[0]["customer"].ToString();
                            LoadDataGridViewSJTO();
                        }
                        else
                        {
                            txtOPNo.Text = "";
                            txtDate.Text = "";
                            txtCustomer.Text = "";
                            GridViewSJ.Visible = false;
                            DisplayAJAXMessage(this, "Data surat jalan kosong");
                        }
                    }
                    LoadDataGridViewKirim();
                    //}
                    //catch
                    //{
                    //    DisplayAJAXMessage(this, "Koneksi Internet Error");
                    //}
                }
                else
                {
                    WebserviceFacade webServicePusat = new WebserviceFacade();
                    if (txtSJNo.Text == string.Empty)
                    {
                        GridViewSJ.Visible = false;
                        clearform();
                        return;
                    }
                    Session["kirimID"] = null;
                    if (RBToko.Checked == true)
                    {
                        SJInfo = webServicePusat.GetSJInfo(txtSJNo.Text);
                        if (SJInfo.Tables[0].Rows.Count > 0)
                        {
                            txtOPNo.Text = SJInfo.Tables[0].Rows[0]["OPNo"].ToString();
                            txtDate.Text = Convert.ToDateTime(SJInfo.Tables[0].Rows[0]["tglkirim"]).ToString("dd-MMM-yyyy");
                            if (t3kirim.SJNo.Trim() != string.Empty)
                                txtDate0.Text = t3kirim.TglKirim.ToString("dd-MMM-yyyy");
                            else
                                txtDate0.Text = DateTime.Now.AddDays(-1).ToString("dd-MMM-yyyy");
                            txtCustomer.Text = SJInfo.Tables[0].Rows[0]["customer"].ToString();
                            GridViewSJ.Visible = true;
                            LoadDataGridViewSJ();
                        }
                        else
                        {
                            txtOPNo.Text = "";
                            txtDate.Text = "";
                            txtCustomer.Text = "";
                            GridViewSJ.Visible = false;
                            DisplayAJAXMessage(this, "Data surat jalan kosong");
                        }
                    }
                    else
                    {


                        SJInfo = webServicePusat.GetSJInfoTO(txtSJNo.Text);
                        if (SJInfo.Tables[0].Rows.Count > 0)
                        {
                            txtOPNo.Text = SJInfo.Tables[0].Rows[0]["OPNo"].ToString();
                            txtDate.Text = Convert.ToDateTime(SJInfo.Tables[0].Rows[0]["tglkirim"]).ToString("dd-MMM-yyyy");
                            if (t3kirim.SJNo.Trim() != string.Empty)
                                txtDate0.Text = t3kirim.TglKirim.ToString("dd-MMM-yyyy");
                            else
                                txtDate0.Text = DateTime.Now.AddDays(-1).ToString("dd-MMM-yyyy");
                            txtCustomer.Text = SJInfo.Tables[0].Rows[0]["customer"].ToString();
                            LoadDataGridViewSJTO();
                        }
                        else
                        {
                            txtOPNo.Text = "";
                            txtDate.Text = "";
                            txtCustomer.Text = "";
                            GridViewSJ.Visible = false;
                            DisplayAJAXMessage(this, "Data surat jalan kosong");
                        }
                    }
                    LoadDataGridViewKirim();
                }
            }
            else
            {

                if (RBExport.Checked == true)
                {
                    T3_SJ_Ex t3sjex = new T3_SJ_Ex();
                    T3_SJ_ExFacade t3sjexF = new T3_SJ_ExFacade();
                    t3sjex = t3sjexF.RetrieveBySJno(txtSJNo.Text);
                    txtCustomer.Text = t3sjex.Customer;
                    LoadSJExDetail(txtSJNo.Text);
                    LoadDataGridViewKirim();
                }
                if (RBCuma.Checked == true)
                {
                    T3_SJ_CM t3sjcm = new T3_SJ_CM();
                    T3_SJ_CMFacade t3sjcmF = new T3_SJ_CMFacade();
                    t3sjcm = t3sjcmF.RetrieveBySJno(txtSJNo.Text);
                    txtCustomer.Text = t3sjcm.Customer;
                    LoadSJCMDetail(txtSJNo.Text);
                    LoadDataGridViewKirim();
                }
            }
        }
        protected void LoadSJExDetail(string sjno)
        {
            T3_SJ_ExDetailFacade t3sjexDetail = new T3_SJ_ExDetailFacade();
            ArrayList arrT3sjexDetail = new ArrayList();
            arrT3sjexDetail = t3sjexDetail.RetrieveBySJ(sjno);
            GridViewSJ.DataSource = arrT3sjexDetail;
            GridViewSJ.DataBind();
        }
        protected void LoadSJCMDetail(string sjno)
        {
            T3_SJ_CMDetailFacade t3sjCMDetail = new T3_SJ_CMDetailFacade();
            ArrayList arrT3sjcmDetail = new ArrayList();
            arrT3sjcmDetail = t3sjCMDetail.RetrieveBySJ(sjno);
            GridViewSJ.DataSource = arrT3sjcmDetail;
            GridViewSJ.DataBind();
        }
        protected void txtSJNo_TextChanged(object sender, EventArgs e)
        {
            LoadSJ();
        }

        protected void ChkHide2_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkHide2.Checked == false)
                Panel9.Visible = false;
            else
                Panel9.Visible = true;
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        protected void RBToko_CheckedChanged(object sender, EventArgs e)
        {
            //AutoCompleteExtender1.ContextKey = "toko";
            clearform();
            LoadDataGridViewSJ();
        }

        protected void RBDepo_CheckedChanged(object sender, EventArgs e)
        {
            //AutoCompleteExtender1.ContextKey = "depo";
            clearform();
            LoadDataGridViewSJTO();
        }

        protected void GridViewSJ_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
            int IDSJ = 0;
            GridView grv = (GridView)GridViewSJ.Rows[rowindex].FindControl("GridViewtrans");
            GridView grv0 = (GridView)GridViewSJ.Rows[rowindex].FindControl("GridViewtrans0");
            Label lbl = (Label)GridViewSJ.Rows[rowindex].FindControl("Label2");
            GridViewSJ.Rows[rowindex].FindControl("Cancel").Visible = false;

            int luas = 0;
            int i = 0;
            T3_KirimDetailFacade T3KirimDetail = new T3_KirimDetailFacade();
            // 
            int konvDeco = 0;
            switch (e.CommandName)
            {
                case "Details":
                    GridViewSJ.Rows[rowindex].FindControl("Cancel").Visible = true;
                    GridViewSJ.Rows[rowindex].FindControl("transfer").Visible = true;
                    GridViewSJ.Rows[rowindex].FindControl("btn_Show").Visible = false;
                    ArrayList arrT3SiapKirim = new ArrayList();
                    T3_SiapKirimFacade T3_SiapKirimFacade = new T3_SiapKirimFacade();
                    int qtyP = 0;
                    int qtySJ = Convert.ToInt32((GridViewSJ.Rows[rowindex].Cells[5].Text));

                    qtyP = T3KirimDetail.RetrieveBySJQty(txtSJNo.Text, GridViewSJ.Rows[rowindex].Cells[0].Text, GridViewSJ.Rows[rowindex].Cells[5].Text);
                    GridViewSJ.Rows[rowindex].Cells[6].Text = qtyP.ToString();
                    luas = Convert.ToInt32(GridViewSJ.Rows[rowindex].Cells[3].Text) * Convert.ToInt32(GridViewSJ.Rows[rowindex].Cells[4].Text);
                    decimal Volume = decimal.Parse(GridViewSJ.Rows[rowindex].Cells[2].Text) * Convert.ToInt32(GridViewSJ.Rows[rowindex].Cells[3].Text) * Convert.ToInt32(GridViewSJ.Rows[rowindex].Cells[4].Text);
                    if (Convert.ToInt32((GridViewSJ.Rows[rowindex].Cells[5].Text)) > Convert.ToInt32((GridViewSJ.Rows[rowindex].Cells[6].Text)))
                    {
                        //arrT3SiapKirim = T3_SiapKirimFacade.Retrievebyluas(luas, Convert.ToInt32(GridViewSJ.Rows[rowindex].Cells[0].Text));
                        if (ChkSJNo.Checked == false)
                            arrT3SiapKirim = T3_SiapKirimFacade.RetrievebyVolume(Convert.ToInt32(Volume), Convert.ToInt32(GridViewSJ.Rows[rowindex].Cells[0].Text));
                        else
                            arrT3SiapKirim = T3_SiapKirimFacade.RetrievebySJNo(txtSJNo.Text.Trim(), Convert.ToInt32(GridViewSJ.Rows[rowindex].Cells[0].Text));
                        Session["arrsiapkirim"] = arrT3SiapKirim;
                        grv.DataSource = arrT3SiapKirim;
                        grv.DataBind();
                        grv.Visible = true;
                        i = 0;
                        konvDeco = 0;
                        foreach (T3_SiapKirim t3siapkirim in arrT3SiapKirim)
                        {
                            TextBox txtQtyKirim = (TextBox)grv.Rows[i].FindControl("txtQtyKirim");
                            if (chkDeco.Checked == true)
                                konvDeco = T3_SiapKirimFacade.GetKonversiDeco(Convert.ToInt32(grv.Rows[i].Cells[1].Text));
                            else
                                konvDeco = 1;
                            if (Convert.ToInt32(grv.Rows[i].Cells[7].Text) - (qtySJ * konvDeco) >= 0)
                            {
                                txtQtyKirim.Text = (qtySJ * konvDeco).ToString();
                                break;
                            }
                            else
                            {
                                txtQtyKirim.Text = grv.Rows[i].Cells[7].Text;
                                qtySJ = (qtySJ * konvDeco) - Convert.ToInt32(grv.Rows[i].Cells[7].Text);
                            }
                            i = i + 1;
                        }
                        GridViewSJ.Rows[rowindex].FindControl("transfer").Visible = true;
                    }
                    else
                    {
                        ArrayList arrT3KirimDetail = new ArrayList();
                        //T3_KirimDetailFacade T3KirimDetail = new T3_KirimDetailFacade();
                        arrT3KirimDetail = T3KirimDetail.RetrieveBySJ(txtSJNo.Text, GridViewSJ.Rows[rowindex].Cells[0].Text, GridViewSJ.Rows[rowindex].Cells[5].Text);
                        grv0.DataSource = arrT3KirimDetail;
                        grv0.DataBind();
                        grv0.Visible = true;
                        GridViewSJ.Rows[rowindex].FindControl("transfer").Visible = false;
                    }
                    break;
                case "Cancel":
                    grv.Visible = false;
                    GridViewSJ.Rows[rowindex].FindControl("Cancel").Visible = false;
                    GridViewSJ.Rows[rowindex].FindControl("transfer").Visible = false;
                    GridViewSJ.Rows[rowindex].FindControl("btn_Show").Visible = true;
                    return;
                case "transfer":
                    //int intresult = 0;
                    // 

                    int kirimID = 0;
                    if (txtSJNo.Text == string.Empty && txtSJNo.Text == string.Empty && txtSJNo.Text == string.Empty)
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
                    int Tahun = DateTime.Parse(txtDate0.Text).Year;
                    int Bulan = DateTime.Parse(txtDate0.Text).Month;
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
                    luas = Convert.ToInt32(GridViewSJ.Rows[rowindex].Cells[3].Text) * Convert.ToInt32(GridViewSJ.Rows[rowindex].Cells[4].Text);
                    ArrayList arrsiapkirim = new ArrayList();
                    //arrsiapkirim = t3siapkirimfacade.Retrievebyluas(luas, Convert.ToInt32(GridViewSJ.Rows[rowindex].Cells[0].Text));
                    decimal vVolume = decimal.Parse(GridViewSJ.Rows[rowindex].Cells[2].Text) * Convert.ToInt32(GridViewSJ.Rows[rowindex].Cells[3].Text) * Convert.ToInt32(GridViewSJ.Rows[rowindex].Cells[4].Text);
                    if (ChkSJNo.Checked == false)
                        arrT3SiapKirim = t3siapkirimfacade.RetrievebyVolume(Convert.ToInt32(vVolume), Convert.ToInt32(GridViewSJ.Rows[rowindex].Cells[0].Text));
                    else
                        arrT3SiapKirim = t3siapkirimfacade.RetrievebySJNo(txtSJNo.Text.Trim(), Convert.ToInt32(GridViewSJ.Rows[rowindex].Cells[0].Text));
                    //if (Session["kirimID"] == null)
                    //{
                    t3kirim.SJNo = txtSJNo.Text;
                    t3kirim.OPNo = txtOPNo.Text;
                    t3kirim.Customer = txtCustomer.Text;
                    t3kirim.TglKirim = DateTime.Parse(txtDate0.Text);
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
                    if (totpotong + Convert.ToInt32(GridViewSJ.Rows[rowindex].Cells[6].Text) != Convert.ToInt32(GridViewSJ.Rows[rowindex].Cells[5].Text) * konvDeco)
                    {
                        DisplayAJAXMessage(this, "Qty potong tidak memenuhi Qty SJ");
                        grv.Visible = false;
                        GridViewSJ.Rows[rowindex].FindControl("Cancel").Visible = false;
                        GridViewSJ.Rows[rowindex].FindControl("transfer").Visible = false;
                        GridViewSJ.Rows[rowindex].FindControl("btn_Show").Visible = true;
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
                            if (stock - Convert.ToInt32(txtQtyKirim.Text) < 0)
                            {
                                DisplayAJAXMessage(this, "Stock tidak mencukupi, proses dibatalkan !");
                                return;
                            }
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
                            t3KirimDetail.Tgltrans = DateTime.Parse(txtDate0.Text);
                            t3KirimDetail.GroupID = t3siapkirim.GroupID;
                            t3KirimDetail.Qty = Convert.ToInt32(txtQtyKirim.Text);
                            t3KirimDetail.HPP = t3siapkirim.HPP;
                            t3KirimDetail.CreatedBy = users.UserName;
                            t3KirimDetail.ItemIDSJ = Convert.ToInt32(GridViewSJ.Rows[rowindex].Cells[0].Text);
                            arrKirimDetail1.Add(t3KirimDetail);
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
                        GridViewSJ.Rows[rowindex].FindControl("Cancel").Visible = false;
                        GridViewSJ.Rows[rowindex].FindControl("transfer").Visible = false;
                        GridViewSJ.Rows[rowindex].FindControl("btn_Show").Visible = true;
                        LoadDataGridViewSJ();
                        LoadDataGridViewKirim();
                    }
                    break;
            }
        }
        protected void GridViewSJ_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Add")
            {
                GridViewRow row = GridView1.Rows[index];
                txtSJNo.Text = row.Cells[2].Text;
                LoadSJ();
            }
        }
        protected void txtScheduleNo_TextChanged(object sender, EventArgs e)
        {
            DataSet dsListSch = new DataSet();
            if (txtScheduleNo.Text == string.Empty)
                return;
            if (RBOffLine.Checked == false)
            {
                try
                {
                    WebReference_Krwg.Service1 webServicePusat = new WebReference_Krwg.Service1();
                    dsListSch = new DataSet();
                    if (RBToko.Checked == true)
                        dsListSch = webServicePusat.GetSjNoBySchNo(txtScheduleNo.Text);
                    else
                        dsListSch = webServicePusat.GetSjNoBySchNoTO(txtScheduleNo.Text);
                    GridView1.DataSource = dsListSch;
                    GridView1.DataBind();
                }
                catch
                {
                    DisplayAJAXMessage(this, "Koneksi Internet Error");
                }
            }
            else
            {
                try
                {
                    WebserviceFacade webServicePusat = new WebserviceFacade();
                    dsListSch = new DataSet();
                    if (RBToko.Checked == true)
                        dsListSch = webServicePusat.GetSjNoBySchNo(txtScheduleNo.Text);
                    else
                        dsListSch = webServicePusat.GetSjNoBySchNoTO(txtScheduleNo.Text);
                    GridView1.DataSource = dsListSch;
                    GridView1.DataBind();
                }
                catch
                {
                    DisplayAJAXMessage(this, "Data schedule belum tersedia, lakukan download Data");
                }
            }
            txtScheduleNo.Text = string.Empty;
        }
        protected void GridViewSJ_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int rowindex = Convert.ToInt32(e.Row.RowIndex.ToString());
                GridView grv = (GridView)e.Row.FindControl("GridViewtrans");
                GridView grv0 = (GridView)e.Row.FindControl("GridViewtrans0");
                Label lbl = (Label)e.Row.FindControl("Label2");
                e.Row.FindControl("Cancel").Visible = false;

                e.Row.FindControl("Cancel").Visible = true;
                e.Row.FindControl("transfer").Visible = true;
                e.Row.FindControl("btn_Show").Visible = false;

                ArrayList arrT3SiapKirim = new ArrayList();
                T3_SiapKirimFacade T3_SiapKirimFacade = new T3_SiapKirimFacade();
                T3_KirimDetailFacade T3KirimDetail = new T3_KirimDetailFacade();
                int qtyP = 0;
                qtyP = T3KirimDetail.RetrieveBySJQty(txtSJNo.Text, e.Row.Cells[0].Text, e.Row.Cells[5].Text);
                int qtySJ = Convert.ToInt32((e.Row.Cells[5].Text)) - qtyP;
                e.Row.Cells[6].Text = qtyP.ToString();
                int luas = Convert.ToInt32(e.Row.Cells[3].Text) * Convert.ToInt32(e.Row.Cells[4].Text);
                Decimal Volume = Convert.ToDecimal(e.Row.Cells[2].Text) * Convert.ToInt32(e.Row.Cells[3].Text) * Convert.ToInt32(e.Row.Cells[4].Text);
                if (Convert.ToInt32((e.Row.Cells[5].Text)) > Convert.ToInt32((e.Row.Cells[6].Text)))
                {
                    //arrT3SiapKirim = T3_SiapKirimFacade.Retrievebyluas(luas, Convert.ToInt32(e.Row.Cells[0].Text));
                    if (ChkSJNo.Checked == false)
                        arrT3SiapKirim = T3_SiapKirimFacade.RetrievebyVolume(Convert.ToInt32(Volume), Convert.ToInt32(e.Row.Cells[0].Text));
                    else
                        arrT3SiapKirim = T3_SiapKirimFacade.RetrievebySJNo(txtSJNo.Text.Trim(), Convert.ToInt32(e.Row.Cells[0].Text));
                    Session["arrsiapkirim"] = arrT3SiapKirim;
                    grv.DataSource = arrT3SiapKirim;
                    grv.DataBind();
                    grv.Visible = true;
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
                    e.Row.FindControl("transfer").Visible = true;
                }
                else
                {
                    ArrayList arrT3KirimDetail = new ArrayList();
                    arrT3KirimDetail = T3KirimDetail.RetrieveBySJ(txtSJNo.Text, e.Row.Cells[0].Text, e.Row.Cells[5].Text);
                    grv0.DataSource = arrT3KirimDetail;
                    grv0.DataBind();
                    grv0.Visible = true;
                    e.Row.FindControl("transfer").Visible = false;
                }
            }
        }

        protected void RBExport_CheckedChanged(object sender, EventArgs e)
        {
            if (RBExport.Checked == true)
            {
                clearform();
                //Panel10.Visible = false;
                //Panel12.Visible = true;
                //txtDateEx.Visible = true;
                //txtDate.Visible = false;
            }
        }
        protected void RBCuma_CheckedChanged(object sender, EventArgs e)
        {
            if (RBCuma.Checked == true)
            {
                clearform();
                //Panel10.Visible = false;
                //Panel12.Visible = true;
                //txtDateEx.Visible = true;
                //txtDate.Visible = false;
            }
        }
        protected void transfer_Click(object sender, EventArgs e)
        {

        }

        protected void chkDeco_CheckedChanged(object sender, EventArgs e)
        {
            LoadSJ();
        }
    }
}