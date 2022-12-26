using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;

namespace GRCweb1.Modul.Purchasing
{
    public partial class ApproveReceipt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Users users = (Users)Session["Users"];


                //LoadDataGridSPP(LoadGridSPP());
                //LoadTipeBarang();
                //LoadTipeSPP();

                clearForm();
                Session["id"] = null;
                LoadOpenReceipt();
                ViewState["counter"] = 0;

                // dah cek ke itemtypeID for inv, asset, biaya
                LoadReceipt((int)ViewState["counter"]);

            }
        }



        private void LoadGroups(DropDownList ddl)
        {
            ArrayList arrGroups = new ArrayList();

            GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
            arrGroups = groupsPurchnFacade.Retrieve();
            ddl.Items.Add(new ListItem("-- Pilih Group --", "0"));
            foreach (GroupsPurchn groupsPurchn in arrGroups)
            {
                ddl.Items.Add(new ListItem(groupsPurchn.GroupDescription, groupsPurchn.ID.ToString()));
            }
        }

        private void clearForm()
        {
            ViewState["id"] = null;
            ArrayList arrList = new ArrayList();
            arrList.Add(new ReceiptDetail());
            GridView1.DataSource = arrList;
            GridView1.DataBind();
        }

        private void LoadReceipt(int intRow)
        {
            ArrayList arrReceipt = new ArrayList();

            if (Session["ListOpenReceipt"] != null)
                arrReceipt = (ArrayList)Session["ListOpenReceipt"];

            if (intRow < arrReceipt.Count && intRow > -1)
            {
                Receipt receipt = new Receipt();
                receipt = (Receipt)arrReceipt[intRow];
                if (receipt.ID > 0)
                {
                    Session["id"] = receipt.ID;
                    txtNoReceipt.Text = receipt.ReceiptNo;

                    txtPONo.Text = receipt.PoNo;
                    txtTanggal.Text = receipt.CreatedTime.ToString("dd-MM-yyyy");
                    txtNoSPP.Text = receipt.SppNo;
                    txtCreatedBy.Text = receipt.CreatedBy;



                    //if (receipt.Status == 0)
                    //{
                    //    txtStatus.Text = "Open";
                    //}
                    //else
                    //{
                    //    if (receipt.Status == 1)
                    //    {
                    //        txtStatus.Text = "By Head Dept";
                    //    }
                    //    else
                    //    {
                    //        txtStatus.Text = "By Plan Manager";
                    //    }
                    //}


                    ReceiptDetailFacade receiptDetailFacade = new ReceiptDetailFacade();

                    // cek ItemTypeID utk Inventory, asset, biaya
                    ArrayList arrItemList = receiptDetailFacade.RetrieveByReceiptId(receipt.ID);
                    if (receiptDetailFacade.Error == string.Empty)
                    {
                        Session["ListOfReceiptDetail"] = arrItemList;
                        GridView1.DataSource = arrItemList;
                        GridView1.DataBind();
                    }

                    if (receipt.Status > 0)
                        btnUpdate.Disabled = true;
                    else
                        btnUpdate.Disabled = false;

                }
            }
            else
            {

                if (intRow == -1)
                    ViewState["counter"] = (int)ViewState["counter"] + 1;
                else
                    ViewState["counter"] = (int)ViewState["counter"] - 1;

                Receipt receipt = new Receipt();
                receipt = (Receipt)arrReceipt[(int)ViewState["counter"]];
                //txtStatus.Text = ((Status)sPP.Approval).ToString();
                if (receipt.Status > 0)
                    btnUpdate.Disabled = true;
                else
                    btnUpdate.Disabled = false;
            }


        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearForm();
        }

        protected void btnSebelumnya_ServerClick(object sender, EventArgs e)
        {
            ViewState["counter"] = (int)ViewState["counter"] - 1;
            LoadReceipt((int)ViewState["counter"]);
        }

        protected void btnSesudahnya_ServerClick(object sender, EventArgs e)
        {
            ViewState["counter"] = (int)ViewState["counter"] + 1;
            LoadReceipt((int)ViewState["counter"]);
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];
            string UsName = users.UserName;

            if (txtNoReceipt.Text != string.Empty)
            {
                ReceiptFacade receiptFacade = new ReceiptFacade();
                Receipt receipt = receiptFacade.RetrieveByNo(txtNoReceipt.Text);
                ArrayList arrReceiptDetail = new ArrayList();

                if (receiptFacade.Error == string.Empty)
                {
                    receipt.Status = 1;
                    receipt.DepoID = users.UnitKerjaID;
                    receipt.ApprovalBy = UsName;
                    //receipt.ApprovalDate = 
                    string strError = string.Empty;
                    ArrayList arrSPPDetail = new ArrayList();
                    if (Session["ListOfSPPDetail"] != null)
                        arrReceiptDetail = (ArrayList)Session["ListOfReceiptDetail"];

                    ReceiptProcessFacade receiptProcessFacade = new ReceiptProcessFacade(receipt, arrReceiptDetail, new ReceiptDocNo());


                    if (receipt.ID > 0)
                    {

                        strError = receiptProcessFacade.Update();
                    }



                    //if (strError == string.Empty)
                    //{
                    //InsertLog("Approve");

                    ArrayList arrReceipt = new ArrayList();
                    if (Session["ListOpenReceipt"] != null)
                    {
                        arrReceipt = (ArrayList)Session["ListOpenReceipt"];
                        ((Receipt)arrReceipt[(int)ViewState["counter"]]).Status = 0;
                        Session["ListOpenReceipt"] = arrReceipt;
                    }

                    if (receipt.ID > 0)
                    {
                        ViewState["counter"] = (int)ViewState["counter"] + 1;
                        LoadReceipt((int)ViewState["counter"]);
                    }
                }
                //else
                //{
                //    DisplayAJAXMessage(this, strError);
                //}
                //}
            }

        }

        protected void btnList_ServerClick(object sender, EventArgs e)
        {
            //Response.Redirect("ListSPP.aspx?approve=yes");
            Session["ListOfReceiptDetail"] = null;
            Session["ReceiptNo"] = null;
            //only for open blom
            Response.Redirect("ListReceiptMRS.aspx?approve=" + (((Users)Session["Users"]).GroupID));

        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            int counter = FindReceipt(txtSearch.Text);
            ViewState["counter"] = counter;
            LoadReceipt(counter);
            txtSearch.Text = string.Empty;
        }
        private int FindReceipt(string strNoReceipt)
        {
            ArrayList arrReceipt = new ArrayList();
            int counter = 0;

            if (Session["ListOpenReceipt"] != null)
                arrReceipt = (ArrayList)Session["ListOpenReceipt"];

            foreach (Receipt receipt in arrReceipt)
            {
                if (receipt.ReceiptNo == strNoReceipt)
                    return counter;

                counter = counter + 1;
            }

            return counter;
        }

        protected void btnDelete_ServerClick(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];

                Response.Redirect("ListSPP.aspx?NoSPP=" + row.Cells[0].Text);
            }
            //else if (e.CommandName == "AddDelete")
            //{
            //    int index = Convert.ToInt32(e.CommandArgument);
            //    ArrayList arrTransferDetail = new ArrayList();
            //    arrTransferDetail = (ArrayList)Session["ListOfTransferDetail"];
            //    arrTransferDetail.RemoveAt(index);
            //    Session["ListOfTransferDetail"] = arrTransferDetail;
            //    GridView1.DataSource = arrTransferDetail;
            //    GridView1.DataBind();
            //}
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        private void LoadOpenReceipt()
        {
            Users users = (Users)Session["Users"];
            int grupiduser = users.GroupID;
            string strGroupID = string.Empty;
            if (grupiduser == 1 || grupiduser == 2)
            {
                strGroupID = "P";
            }
            if (grupiduser == 3)
            {
                strGroupID = "A";
            }
            if (grupiduser == 4)
            {
                strGroupID = "C";
            }
            if (grupiduser == 5)
            {
                strGroupID = "B";
            }
            if (grupiduser == 6 || grupiduser == 7 || grupiduser == 8 || grupiduser == 9)
            {
                strGroupID = "S";
            }


            Company company = new Company();
            CompanyFacade companyFacade = new CompanyFacade();
            //3 Mei cek inputan Receipt hrs sesuai dgn user login shg yg keluar sesuai grup nya, ga cuma S doank
            string lokasikode = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + strGroupID;

            ReceiptFacade receiptFacade = new ReceiptFacade();
            ArrayList arrReceipt = new ArrayList();

            arrReceipt = receiptFacade.RetrieveOpenStatus(lokasikode);

            if (receiptFacade.Error == string.Empty)
            {
                Session["ListOpenReceipt"] = arrReceipt;
            }
        }

    }
}