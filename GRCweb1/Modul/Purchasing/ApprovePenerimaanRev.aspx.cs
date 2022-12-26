using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;
using System.Globalization;
using System.Data.SqlTypes;


namespace GRCweb1.Modul.Purchasing
{
    public partial class ApprovePenerimaanRev : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["RowStatus"] = "";
                Global.link = "~/Default.aspx";
                Users users = (Users)Session["Users"];
                LoadGroup();
                LoadOpenReceipt();
            }
        }
        private void ClearForm()
        {
            txtTTagihanDate.Text = string.Empty;
            txtFakPajakDate.Text = string.Empty;
            //txtJTempoDate.Text = string.Empty;
            txtFakturPajak.Text = string.Empty;
            txtInvoiceNo.Text = string.Empty;
            txtKursPajak.Text = "1";
            LPPN.Text = "0";
            LTotal.Text = "0";
            LTagihan.Text = "0";
            Session["ListInvoice"] = null;
            Session["receiptNo"] = null;
            Session["PPN"] = null;
            Session["Total"] = null;
            Session["Tagihan"] = null;
            Session["Currency"] = null;
            PanelReceipt.Visible = false;
            btnNew0.Value = "Add Receipt";
            btnNew0.Disabled = false;
            Receipt rcp = new Receipt();
            ArrayList arrReceipt = new ArrayList();
            arrReceipt.Add(rcp);
            GridViewInvoice.DataSource = arrReceipt;
            GridViewInvoice.DataBind();
        }

        private void LoadReceipt(int intRow)
        {
            ArrayList arrListReceipt = new ArrayList();
            //ClearForm();
            if (Session["ListOpenReceipt"] != null)
                arrListReceipt = (ArrayList)Session["ListOpenReceipt"];

            if (intRow < arrListReceipt.Count && intRow > -1)
            {
                Receipt receipt = new Receipt();
                receipt = (Receipt)arrListReceipt[intRow];
                ReceiptFacade rcf = new ReceiptFacade();
                if (receipt.ID > 0)
                {
                    double jtp = rcf.GetJTP(receipt.PoID);
                    SuppPurch supp = new SuppPurch();
                    SuppPurchFacade suppF = new SuppPurchFacade();
                    supp = suppF.RetrieveById(receipt.SupplierID);

                    Session["id"] = receipt.ID;
                    Session["ReceiptNo"] = receipt.ReceiptNo;
                    //LoadSupplier();

                    ArrayList arrListReceiptDetail = new ArrayList();
                    ReceiptDetailFacade receiptDetailFacade = new ReceiptDetailFacade();
                    ArrayList arrReceiptDetail = new ArrayList();
                    if (receipt.ItemTypeID == 1)
                        arrReceiptDetail = receiptDetailFacade.RetrieveByReceiptId(receipt.ID);
                    else if (receipt.ItemTypeID == 2)
                        arrReceiptDetail = receiptDetailFacade.RetrieveByReceiptIdForAsset(receipt.ID);
                    else if (receipt.ItemTypeID == 3)
                        arrReceiptDetail = receiptDetailFacade.RetrieveByReceiptIdForBiaya(receipt.ID);
                    if (receiptDetailFacade.Error == string.Empty)
                    {
                        foreach (ReceiptDetail receiptDetail in arrReceiptDetail)
                        {
                            if (receiptDetail.ID > 0)
                                arrListReceiptDetail.Add(receiptDetail);
                        }
                    }
                    Session["ListOfReceiptDetail"] = arrListReceiptDetail;
                    GridView1.DataSource = arrListReceiptDetail;
                    GridView1.DataBind();
                }
            }
            else
            {
                if (intRow == -1)
                    ViewState["counter"] = (int)ViewState["counter"] + 1;
                else
                    ViewState["counter"] = (int)ViewState["counter"] - 1;

                //POPurchn pOPurchn = new POPurchn();
                //pOPurchn = (POPurchn)arrListPakai[(int)ViewState["counter"]];

                Receipt receipt = new Receipt();
                receipt = (Receipt)arrListReceipt[(int)ViewState["counter"]];
            }

        }

        private int FindReceipt(string receiptNo)
        {
            Users users = (Users)Session["Users"];
            ReceiptFacade receiptFacade = new ReceiptFacade();
            ArrayList arrRcp = new ArrayList();

            //arrPO = pOPurchnFacade.RetrieveOpenApprovalByNo(users.Apv - 1, pONo);
            arrRcp = receiptFacade.RetrieveOpenStatusApprove("0");
            if (receiptFacade.Error == string.Empty)
            {
                Session["ListOpenReceipt"] = arrRcp;
            }
            int counter = 0;

            foreach (Receipt receipt in arrRcp)
            {
                if (receipt.ReceiptNo == receiptNo)
                    return counter;

                counter = counter + 1;
            }

            return counter;
        }

        private void LoadTermOfPay()
        {
            ArrayList arrTermOfPay = new ArrayList();
            TermOfPayFacade termOfPayFacade = new TermOfPayFacade();
            arrTermOfPay = termOfPayFacade.Retrieve();
            //ddlTermOfPay.Items.Add(new ListItem("-- Pilih Jenis TOP --", string.Empty));
            foreach (TermOfPay termOfPay in arrTermOfPay)
            {
                //ddlTermOfPay.Items.Add(new ListItem(termOfPay.TermPay, termOfPay.ID.ToString()));
            }
        }

        private void SelectTermOfPay(string strItemName)
        {
            //ddlTermOfPay.ClearSelection();
            //foreach (ListItem item in ddlTermOfPay.Items)
            //{
            //    if (item.Text == strItemName)
            //    {
            //        item.Selected = true;
            //        return;
            //    }
            //}
        }
        private void LoadGroup()
        {
            ArrayList arrGroupsPurchn = new ArrayList();
            GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();

            arrGroupsPurchn = groupsPurchnFacade.Retrieve();
            ddlGroup.Items.Clear();
            ddlGroup.Items.Add(new ListItem("-- Pilih Group --", "00"));
            foreach (GroupsPurchn groupsPurchn in arrGroupsPurchn)
            {
                ddlGroup.Items.Add(new ListItem(groupsPurchn.GroupDescription, groupsPurchn.CodeID.Trim()));
            }
        }
        private void LoadOpenReceipt()
        {
            Users users = (Users)Session["Users"];
            ReceiptFacade receiptFacade = new ReceiptFacade();
            ArrayList arrReceipt = new ArrayList();
            string notinreceipt = string.Empty;
            if (Session["receiptNo"] == null)
                notinreceipt = "''";
            else
                notinreceipt = Session["receiptNo"].ToString();
            if (ddlGroup.SelectedIndex < 1)
                arrReceipt = receiptFacade.RetrieveByTagihan();
            else
                arrReceipt = receiptFacade.RetrieveByTagihanGroup(ddlGroup.SelectedItem.Text.Trim(), notinreceipt);
            if (receiptFacade.Error == string.Empty)
            {
                Session["ListOpenReceipt"] = arrReceipt;
            }
            GridViewReceipt.DataSource = arrReceipt;
            GridViewReceipt.DataBind();
        }
        private void LoadOpenReceiptByReceiptNo(string receiptno)
        {
            Users users = (Users)Session["Users"];
            ReceiptFacade receiptFacade = new ReceiptFacade();
            ArrayList arrReceipt = new ArrayList();
            string notinreceipt = string.Empty;
            Session["RowStatus"] = " and RCD.RowStatus >-1";
            if (Session["receiptNo"] == null)
                notinreceipt = "''";
            else
                notinreceipt = Session["receiptNo"].ToString();
            if (ddlGroup.SelectedIndex < 1)
                arrReceipt = receiptFacade.RetrieveByTagihan();
            arrReceipt = receiptFacade.RetrieveByTagihanReceiptNo(receiptno, notinreceipt);
            {
                Session["ListOpenReceipt"] = arrReceipt;
            }
            GridViewReceipt.DataSource = arrReceipt;
            GridViewReceipt.DataBind();
            Session["RowStatus"] = string.Empty;
        }

        private void LoadOpenReceiptByReceiptNoSupplier(string supplierName)
        {
            Users users = (Users)Session["Users"];
            ReceiptFacade receiptFacade = new ReceiptFacade();
            ArrayList arrReceipt = new ArrayList();
            string notinreceipt = string.Empty;
            string groupname = string.Empty;
            Session["RowStatus"] = " and RCD.RowStatus >-1";
            if (Session["receiptNo"] == null)
                notinreceipt = "''";
            else
                notinreceipt = Session["receiptNo"].ToString();
            if (ddlGroup.SelectedIndex < 1)
                groupname = "";
            else
            {
                groupname = ddlGroup.SelectedItem.Text;
            }
            arrReceipt = receiptFacade.RetrieveByTagihanReceiptNoSupplier(supplierName, notinreceipt, groupname);
            Session["ListOpenReceipt"] = arrReceipt;
            GridViewReceipt.DataSource = arrReceipt;
            GridViewReceipt.DataBind();
            Session["RowStatus"] = string.Empty;
        }

        private void LoadOpenReceiptByNotInReceiptNo(string receiptno)
        {
            Users users = (Users)Session["Users"];
            ReceiptFacade receiptFacade = new ReceiptFacade();
            ArrayList arrReceipt = new ArrayList();
            Session["RowStatus"] = " and RCD.RowStatus >-1";
            arrReceipt = receiptFacade.RetrieveByTagihanNotInReceiptNo(receiptno);

            {
                Session["ListOpenReceipt"] = arrReceipt;
            }
            GridViewReceipt.DataSource = arrReceipt;
            GridViewReceipt.DataBind();
            Session["RowStatus"] = string.Empty;
        }

        private void LoadOpenReceiptByInvoiceNo(string InvoiceNo)
        {
            Users users = (Users)Session["Users"];
            ReceiptFacade receiptFacade = new ReceiptFacade();
            ArrayList arrReceipt = new ArrayList();
            Session["RowStatus"] = " and RCD.RowStatus >-1";
            arrReceipt = receiptFacade.RetrieveByTagihanInvoiceNo(InvoiceNo);
            GridViewInvoice.DataSource = arrReceipt;
            GridViewInvoice.DataBind();
            Session["RowStatus"] = string.Empty;
        }

        private int FindPO(string strNoPO)
        {
            Users users = (Users)Session["Users"];
            ReceiptFacade receiptFacade = new ReceiptFacade();
            ArrayList arrRcp = new ArrayList();

            //arrPO = pOPurchnFacade.RetrieveOpenApprovalByNo(users.Apv - 1, pONo);
            arrRcp = receiptFacade.RetrieveOpenStatusApprove("0");
            if (receiptFacade.Error == string.Empty)
            {
                Session["ListOpenReceipt"] = arrRcp;
            }
            int counter = 0;

            foreach (Receipt receipt in arrRcp)
            {
                if (receipt.PoNo == strNoPO)
                    return counter;

                counter = counter + 1;
            }

            return counter;
        }

        private void LoadMataUang()
        {
            ArrayList arrMataUang = new ArrayList();
            MataUangFacade mataUangFacade = new MataUangFacade();
            arrMataUang = mataUangFacade.Retrieve();
            //ddlMataUang.Items.Add(new ListItem("-- Pilih Mata Uang --", string.Empty));
            foreach (MataUang mataUang in arrMataUang)
            {
                //ddlMataUang.Items.Add(new ListItem(mataUang.Lambang, mataUang.ID.ToString()));
            }
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            LoadOpenReceiptByInvoiceNo(txtSearch.Text);
            btnNew0.Disabled = true;
            PanelReceipt.Visible = false;
            btnNew0.Value = "Add Receipt";
            btnNew0.Disabled = true;
        }

        protected void btnSebelumnya_ServerClick(object sender, EventArgs e)
        {
            ViewState["counter"] = (int)ViewState["counter"] - 1;
            //LoadPO((int)ViewState["counter"]);
            LoadReceipt((int)ViewState["counter"]);
        }
        private string ValidateItem()
        {
            if (txtKursPajak.Text.Trim() == string.Empty)
                return "";
            if (txtTTagihanDate.Text == string.Empty)
                return "Tanggal tagihan tidak boleh kosong";
            else if (decimal.Parse(txtKursPajak.Text) <= 1 && Session["Currency"].ToString().Trim().ToUpper() != "RUPIAH")
                return "Kurs Pajak belum ditentukan";
            //else if (txtJTempoDate.Text == string.Empty)
            //    return "Tanggal jatuh tempo tidak boleh kosong";
            //else if (txtFakturPajak.Text == string.Empty)
            //    return "Fatur pajak tidak boleh kosong";
            else if (txtInvoiceNo.Text == string.Empty)
                return "Nomor Invoice tidak boleh kosong";
            return string.Empty;
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            ClearForm();
        }
        protected void btnNew0_ServerClick(object sender, EventArgs e)
        {
            string strValidate = ValidateItem();
            //if (strValidate != string.Empty)
            //{
            //    DisplayAJAXMessage(this, strValidate);
            //    return;
            //}
            if (PanelReceipt.Visible == false)
            {
                PanelReceipt.Visible = true;
                btnNew0.Value = "Close List Receipt";
                if (Session["receiptNo"] == null)
                    LoadOpenReceipt();
                else
                    LoadOpenReceiptByNotInReceiptNo(Session["receiptNo"].ToString());
                if (txtCariSupplier.Text.Trim() != string.Empty)
                    LoadOpenReceiptByReceiptNoSupplier(txtCariSupplier.Text.Trim());
            }
            else
            {
                PanelReceipt.Visible = false;
                btnNew0.Value = "Add Receipt";
            }
        }
        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];
            ArrayList arrInvoice = (ArrayList)Session["ListInvoice"];
            //SqlDateTime sqldatenull; 
            //if (txtReceipt.Text != string.Empty)
            //{
            string strValidate = ValidateItem();
            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }
            foreach (Receipt rcp in arrInvoice)
            {
                ReceiptFacade receiptFacade = new ReceiptFacade();
                if (receiptFacade.Error == string.Empty)
                {
                    rcp.Status = 1;
                    rcp.LastModifiedBy = users.UserName;
                    rcp.TTagihanDate = DateTime.Parse(txtTTagihanDate.Text);
                    rcp.ApprovalBy = users.UserName;
                    rcp.FakturPajak = txtFakturPajak.Text;
                    rcp.KursPajak = decimal.Parse(txtKursPajak.Text);
                    rcp.Keteranganpay = txtKeterangan.Text;
                    SuppPurch supp = new SuppPurch();
                    SuppPurchFacade suppF = new SuppPurchFacade();
                    supp = suppF.RetrieveById(rcp.SupplierID);
                    if (txtFakPajakDate.Text.Trim() != string.Empty)
                        rcp.FakturPajakDate = DateTime.Parse(txtFakPajakDate.Text);

                    rcp.InvoiceNo = txtInvoiceNo.Text;
                    string strError = string.Empty;
                    ArrayList arrReceiptDetail = new ArrayList();

                    ReceiptDetailFacade receiptDetailFacade = new ReceiptDetailFacade();
                    if (rcp.ItemTypeID == 1)
                        arrReceiptDetail = receiptDetailFacade.RetrieveByReceiptId(rcp.ID);
                    else if (rcp.ItemTypeID == 2)
                        arrReceiptDetail = receiptDetailFacade.RetrieveByReceiptIdForAsset(rcp.ID);
                    else if (rcp.ItemTypeID == 3)
                        arrReceiptDetail = receiptDetailFacade.RetrieveByReceiptIdForBiaya(rcp.ID);

                    ReceiptProcessFacade receiptProcessFacade = new ReceiptProcessFacade(rcp, arrReceiptDetail, new ReceiptDocNo());

                    if (rcp.ID > 0)
                    {
                        strError = receiptProcessFacade.UpdateApprove();

                    }
                }
            }
            ClearForm();

        }

        protected void btnSesudahnya_ServerClick(object sender, EventArgs e)
        {
            ViewState["counter"] = (int)ViewState["counter"] + 1;
            LoadReceipt((int)ViewState["counter"]);
        }

        protected void btnList_ServerClick(object sender, EventArgs e)
        {
            Session["ListOfReceiptDetail"] = null;
            Session["ReceiptNo"] = null;
            Company company = new Company();
            CompanyFacade companyFacade = new CompanyFacade();
            string kd = "X0";
            Response.Redirect("ListReceiptMRS.aspx?approve=" + kd);
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];

                SelectItem(row.Cells[1].Text);

            }
            else if (e.CommandName == "AddDelete")
            {

            }
        }

        private void SelectItem(string strItemName)
        {

        }

        private void LoadTipeSPP()
        {
            ArrayList arrGroupsPurchn = new ArrayList();
            GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
            arrGroupsPurchn = groupsPurchnFacade.Retrieve();
            //ddlTipeSPP.Items.Add(new ListItem("-- Pilih Tipe SPP --", string.Empty));
            foreach (GroupsPurchn groupsPurchn in arrGroupsPurchn)
            {
                //ddlTipeSPP.Items.Add(new ListItem(groupsPurchn.GroupDescription, groupsPurchn.ID.ToString()));
            }
        }

        private void LoadSupplier()
        {
            ArrayList arrSuppPurch = new ArrayList();
            SuppPurchFacade suppPurchFacade = new SuppPurchFacade();
            arrSuppPurch = suppPurchFacade.Retrieve();
            //ddlSupplier.Items.Add(new ListItem("-- Pilih Supplier --", string.Empty));
            foreach (SuppPurch suppPurch in arrSuppPurch)
            {
                //ddlSupplier.Items.Add(new ListItem(suppPurch.SupplierName, suppPurch.ID.ToString()));
            }
        }
        protected void txtTTagihanDate_TextChanged(object sender, EventArgs e)
        {
            //int jtp = 0;
            //jtp = Convert.ToInt32(Session["jtp"].ToString());
            //txtJTempoDate.Text = DateTime.Parse(txtTTagihanDate.Text).AddDays(jtp).ToString("dd-MMM-yyyy");
            //tampilkan kurs tengah BI
            MataUangKursFacade mtk = new MataUangKursFacade();
            int Kurs = mtk.GetKurs(DateTime.Parse(txtTTagihanDate.Text).ToString("yyyyMMdd"));
            if (Kurs > 0)
            {
                infoKurs.InnerHtml = "Kurs Tengah BI Tanggal " + txtTTagihanDate.Text + " : Rp. " + Kurs.ToString("##,###.00");
            }
            else
            {
                infoKurs.InnerHtml = string.Empty;
            }
        }

        protected void GridViewInvoice_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }
        protected void GridViewInvoice_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Receipt rcp = new Receipt();
            ReceiptFacade rcpF = new ReceiptFacade();
            ArrayList arrInvoice = new ArrayList();
            string strReceiptNo = string.Empty;
            int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
            GridView grv = (GridView)GridViewInvoice.Rows[rowindex].FindControl("GridView3");
            Label lbl = (Label)GridViewInvoice.Rows[rowindex].FindControl("Label3");
            //GridViewInvoice.Rows[rowindex].FindControl("Cancel0").Visible = false;
            //try
            //{
            // 
            if (e.CommandName == "Details")
            {
                GridViewInvoice.Rows[rowindex].FindControl("Cancel0").Visible = true;
                GridViewInvoice.Rows[rowindex].FindControl("btn_Show0").Visible = false;
                int receiptID = Int32.Parse(GridViewInvoice.Rows[rowindex].Cells[0].Text);
                ArrayList arrListReceiptDetail = new ArrayList();
                ReceiptDetailFacade receiptDetailFacade = new ReceiptDetailFacade();
                ArrayList arrReceiptDetail = new ArrayList();
                rcp = rcpF.RetrieveByID(receiptID);
                if (rcp.ItemTypeID == 1)
                    arrReceiptDetail = receiptDetailFacade.RetrieveByReceiptId(receiptID);
                else if (rcp.ItemTypeID == 2)
                    arrReceiptDetail = receiptDetailFacade.RetrieveByReceiptIdForAsset(receiptID);
                else if (rcp.ItemTypeID == 3)
                    arrReceiptDetail = receiptDetailFacade.RetrieveByReceiptIdForBiaya(receiptID);

                grv.DataSource = arrReceiptDetail;
                grv.DataBind();
                grv.Visible = true;
            }
            else
            {
                grv.Visible = false;
                GridViewInvoice.Rows[rowindex].FindControl("Cancel0").Visible = false;
                GridViewInvoice.Rows[rowindex].FindControl("btn_Show0").Visible = true;
            }
            //}
            //catch
            //{ }   
        }
        protected void GridViewInvoice_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btn_Show_Click(object sender, EventArgs e)
        {

        }
        protected void ddlGroup_TextChanged(object sender, EventArgs e)
        {

        }
        protected void txNama_TextChanged(object sender, EventArgs e)
        {
            ddlGroup.SelectedIndex = 0;
            LoadOpenReceiptByReceiptNo(txtCariReceipt.Text.Trim());
            txtCariReceipt.Text = string.Empty;

        }
        protected void btnSearch3_Click(object sender, ImageClickEventArgs e)
        {
            ddlGroup.SelectedIndex = 0;
        }
        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadOpenReceipt();
            txtCariSupplier_AutoCompleteExtender.ContextKey = ddlGroup.SelectedItem.Text.Trim();
        }

        protected void GridViewReceipt_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Receipt rcp = new Receipt();
            ReceiptFacade rcpF = new ReceiptFacade();
            ArrayList arrInvoice = new ArrayList();
            string strReceiptNo = string.Empty;
            int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
            GridView grv = (GridView)GridViewReceipt.Rows[rowindex].FindControl("GridView2");
            Label lbl = (Label)GridViewReceipt.Rows[rowindex].FindControl("Label2");
            GridViewReceipt.Rows[rowindex].FindControl("Cancel").Visible = false;
            Receipt receipt = new Receipt();
            ReceiptFacade rcf = new ReceiptFacade();
            if (txtTTagihanDate.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "tanggal terima tagihan belum diisi");
                return;
            }
            try
            {
                // 
                if (e.CommandName == "Details")
                {

                    GridViewReceipt.Rows[rowindex].FindControl("Cancel").Visible = true;
                    GridViewReceipt.Rows[rowindex].FindControl("btn_Show").Visible = false;
                    int receiptID = Int32.Parse(GridViewReceipt.Rows[rowindex].Cells[0].Text);
                    ArrayList arrListReceiptDetail = new ArrayList();
                    ReceiptDetailFacade receiptDetailFacade = new ReceiptDetailFacade();
                    ArrayList arrReceiptDetail = new ArrayList();
                    rcp = rcpF.RetrieveByID(receiptID);
                    if (rcp.ItemTypeID == 1)
                        arrReceiptDetail = receiptDetailFacade.RetrieveByReceiptId(receiptID);
                    else if (rcp.ItemTypeID == 2)
                        arrReceiptDetail = receiptDetailFacade.RetrieveByReceiptIdForAsset(receiptID);
                    else if (rcp.ItemTypeID == 3)
                        arrReceiptDetail = receiptDetailFacade.RetrieveByReceiptIdForBiaya(receiptID);
                    //if (receiptDetailFacade.Error == string.Empty)
                    //{
                    //    foreach (ReceiptDetail receiptDetail in arrReceiptDetail)
                    //    {
                    //        if (receiptDetail.ID > 0)
                    //            arrListReceiptDetail.Add(receiptDetail);
                    //    }
                    //}
                    grv.DataSource = arrReceiptDetail;
                    grv.DataBind();
                    grv.Visible = true;
                }
                if (e.CommandName == "Cancel")
                {
                    //// child gridview  display false when cancel button raise event
                    grv.Visible = false;
                    GridViewReceipt.Rows[rowindex].FindControl("Cancel").Visible = false;
                    GridViewReceipt.Rows[rowindex].FindControl("btn_Show").Visible = true;
                }
            }
            catch
            { }


            if (Session["ListInvoice"] != null)
                arrInvoice = (ArrayList)Session["ListInvoice"];

            if (Session["receiptNo"] != null)
                strReceiptNo = Session["receiptNo"].ToString();
            if (e.CommandName == "pilih")
            {
                double jtp = rcf.GetJTP(Int32.Parse(GridViewReceipt.Rows[rowindex].Cells[9].Text));
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridViewReceipt.Rows[index];
                if (PanelReceipt.Visible == false)
                {
                    PanelReceipt.Visible = true;
                    btnNew0.Value = "Close List Receipt";
                }
                else
                {
                    PanelReceipt.Visible = false;
                    btnNew0.Value = "Add Receipt";
                    int receiptID = Int32.Parse(GridViewReceipt.Rows[rowindex].Cells[0].Text);
                    ArrayList arrListReceiptDetail = new ArrayList();
                    ReceiptDetailFacade receiptDetailFacade = new ReceiptDetailFacade();
                    ArrayList arrReceiptDetail = new ArrayList();
                    string crc = string.Empty;
                    if (Session["Currency"] == null)
                        Session["Currency"] = GridViewReceipt.Rows[index].Cells[5].Text;
                    else
                    {
                        crc = Session["Currency"].ToString().Trim().ToUpper();
                        if (Session["Currency"].ToString().Trim().ToUpper() != GridViewReceipt.Rows[index].Cells[5].Text.Trim().ToUpper())
                        {
                            DisplayAJAXMessage(this, "Currency tidak sama, transaksi ditolak");
                            return;
                        }
                    }
                    if (GridViewReceipt.Rows[rowindex].Cells[6].Text.ToUpper() == "RUPIAH")
                    {
                        txtKursPajak.Enabled = false;
                        txtKursPajak.Text = "1";
                    }
                    else
                    {
                        txtKursPajak.Enabled = true;
                        txtKursPajak.Text = "1";
                    }

                    rcp = rcpF.RetrieveByID(receiptID);
                    rcp.ID = Convert.ToInt32(GridViewReceipt.Rows[index].Cells[0].Text);
                    rcp.ReceiptNo = GridViewReceipt.Rows[index].Cells[3].Text;
                    rcp.ReceiptDate = Convert.ToDateTime(GridViewReceipt.Rows[index].Cells[4].Text);
                    rcp.SupplierName = GridViewReceipt.Rows[index].Cells[1].Text;
                    rcp.NPWP = GridViewReceipt.Rows[index].Cells[2].Text;
                    rcp.Currency = GridViewReceipt.Rows[index].Cells[5].Text;
                    rcp.Total = Convert.ToDecimal(GridViewReceipt.Rows[index].Cells[6].Text);
                    rcp.PPN = Convert.ToDecimal(GridViewReceipt.Rows[index].Cells[7].Text);
                    rcp.Tagihan = Convert.ToDecimal(GridViewReceipt.Rows[index].Cells[8].Text);
                    rcp.JTempoDate = Convert.ToDateTime(DateTime.Parse(txtTTagihanDate.Text).AddDays(jtp).ToString("dd-MMM-yyyy"));
                    arrInvoice.Add(rcp);
                    Session["ListInvoice"] = arrInvoice;
                    GridViewInvoice.DataSource = arrInvoice;
                    GridViewInvoice.DataBind();
                    if (Session["PPN"] == null)
                        Session["PPN"] = rcp.PPN;
                    else
                        Session["PPN"] = decimal.Parse(Session["PPN"].ToString()) + rcp.PPN;
                    if (Session["Total"] == null)
                        Session["Total"] = rcp.Total;
                    else
                        Session["Total"] = decimal.Parse(Session["Total"].ToString()) + rcp.Total;
                    if (Session["Tagihan"] == null)
                        Session["Tagihan"] = rcp.Tagihan;
                    else
                        Session["Tagihan"] = decimal.Parse(Session["Tagihan"].ToString()) + rcp.Tagihan;
                    LPPN.Text = Session["PPN"].ToString();
                    LTotal.Text = Session["Total"].ToString();
                    LTagihan.Text = Session["Tagihan"].ToString();
                    if (strReceiptNo.Length == 0)
                    {
                        strReceiptNo = strReceiptNo + ("'" + GridViewReceipt.Rows[index].Cells[3].Text.Trim() + "'");
                    }
                    else
                    {
                        strReceiptNo = (strReceiptNo + ",'" + GridViewReceipt.Rows[index].Cells[3].Text.Trim() + "'");
                    }
                    Session["receiptNo"] = strReceiptNo;
                }
            }
        }
        protected void GridViewReceipt_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void txtCariSupplier_TextChanged(object sender, EventArgs e)
        {
            LoadOpenReceiptByReceiptNoSupplier(txtCariSupplier.Text.Trim());
            //txtCariSupplier.Text = string.Empty;
            ddlGroup.SelectedIndex = 0;
        }
    }
}