using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Mail;
using Org.BouncyCastle.Asn1.Crmf;

namespace GRCweb1.Modul.Master
{
    public partial class NewMasterSupplierPurch : System.Web.UI.Page
    {
        readonly PagedDataSource _pgsource = new PagedDataSource();
        int _firstIndex, _lastIndex;
        private int _pageSize = 10;
        private int CurrentPage
        {
            get
            {
                if (ViewState["CurrentPage"] == null)
                {
                    return 0;
                }
                return ((int)ViewState["CurrentPage"]);
            }
            set
            {
                ViewState["CurrentPage"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";

                Session["AlasanCancel"] = "";
                Session["AlasanBatal"] = "";

                Users user = (Users)Session["Users"];
                if (Request.QueryString["SupplierID"] != null)
                {
                    ViewState["id"] = ID;
                    NewSuppPurch sp = new NewSuppPurch();
                    sp = new NewSuppPurchFacade().RetrieveById(int.Parse(Request.QueryString["SupplierID"].ToString()));
                    txtSupplierCode.Text = sp.SupplierCode.ToString();
                    txtSupplierName.Text = sp.SupplierName.ToString();
                    txtTelepon.Text = sp.Telepon.ToString();
                    txtUP.Text = sp.UP.ToString();
                    txtEmail.Text = sp.EMail.ToString();
                    txtAlamat.Text = sp.Alamat.ToString();
                    txtNPWP.Text = sp.NPWP.ToString();
                    txtKeterangan.Text = sp.Keterangan.ToString();
                    txtFax.Text = sp.Fax.ToString();
                    txtJenisUsaha.Text = sp.JenisUsaha.ToString();

                    txtNamaRekening.Text = sp.NamaRekening.ToString();
                    txtBankRekening.Text = sp.BankRekening.ToString();
                    txtNomorRekening.Text = sp.NomorRekening.ToString();

                    txtJoin.Text = sp.JoinDate.ToString();
                    ddlMataUang.SelectedValue = (sp.Flag > 1) ? sp.Flag.ToString() : "1";
                    txtHandphone.Text = sp.Handphone.ToString();
                    ddlPKP.SelectedValue = (sp.PKP.Trim() == "") ? "No" : sp.PKP.Trim();
                    ddlSubCompany.SelectedValue = sp.SubCompanyID.ToString();
                    txtKTP.Text = sp.KTP;
                    txtNPWP0.Text = sp.NPWP_P;



                    Label3.Visible = (sp.PKP.Trim() == "No") ? false : true;
                    if (user.Apv > 1)
                    {
                        btnDelete.Visible = (sp.Aktif == 0) ? true : false;
                        btnUnlock.Visible = (sp.Aktif == 0) ? false : true;
                    }
                    if (sp.Aktif == 0)
                    {
                        txtKeterangan.Visible = (sp.Keterangan != null) ? false : true;
                    }
                    if (sp.Aktif == 1)
                    {
                        txtKeterangan.Visible = (sp.Keterangan != null) ? true : false;
                    }
                    //btnDelete.Enabled = (posx > -1) ? true : false;
                    //btnUnlock.Enabled = (posx > -1) ? true : false;
                    //btnUpdate.Disabled = (posx > -1) ? false : true;
                    statSupp.InnerHtml = (sp.Aktif == 0) ? "" : "<i>[ Non Aktif Supplier ]</i>";
                    //txtInfoRevisi.Text = sp.Keterangan(true);
                    //txtInfoRevisi.ToolTip = sp.Keterangan();
                    //revInfo.Attributes.Add("style", "color:Blue;font-size:x-small; position:absolute; float:left;border:2px solid grey; background-color:Highlight");
                    //statSupp.InnerHtml = (sp.Keterangan != null) ? txtKeterangan.Text : "";
                    Session.Remove("AlasanCancel");
                    //btnDelete.Attributes.Add("onclick", "return confirm_lock();");
                    //btnUnlock.Attributes.Add("onclick", "return confirm_lock();");
                    btnUpload.Visible = true;
                    btnUpload.Attributes.Add("onclick", "OpenDialog('" + sp.ID.ToString().Trim() + "&tp=1')");
                    LoadListAttachmentPrs(sp.ID.ToString().Trim(), attachm);
                }
                LoadDataGridSupplier(LoadGridSupplier());
                clearForm();
                LoadMataUang();
                DropDownList1_SelectedIndexChanged(null, null);
                btnDelete.Visible = false;
                btnUnlock.Visible = false;
                txtKeterangan.Visible = false;
                string[] MasterEdited = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("SuppUserEdit", "MasterData").Split(',');
                int posx = Array.IndexOf(MasterEdited, user.UserName);
                btnNew.Disabled = (posx > -1) ? false : true;
                btnUpdate.Disabled = (posx > -1) ? false : true;
                BindDataIntoRepeater();
                LoadSubCompany();
                LoadAgenLapak();
            }
        }

        private void LoadSubCompany()
        {
            Users users = (Users)Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select * from Company where RowStatus>-1 and id in (5,6,8,7) order by Nama";
            SqlDataReader sdr = zl.Retrieve();
            ddlSubCompany.Items.Clear();
            ddlSubCompany.Items.Add(new ListItem("Pilih SubCompany", "0"));
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlSubCompany.Items.Add(new ListItem(sdr["Nama"].ToString(), sdr["ID"].ToString()));
                }
            }
        }
        private void LoadAgenLapak()

        {
            try
            {
                Users users = (Users)Session["Users"];
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "select ID,Nama from [sql1.grcboard.com].AgenLapak.dbo.Agen_DtAgen where RowStatus>-1 order by nama";
                SqlDataReader sdr = zl.Retrieve();
                ddlAgen.Items.Clear();
                ddlAgen.Items.Add(new ListItem("Pilih Agen", "0"));
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        ddlAgen.Items.Add(new ListItem(sdr["Nama"].ToString().Trim(), sdr["ID"].ToString()));
                    }
                }
            }
            catch { }
        }
        private void updateLapak()
        {

        }
        private void GetLapak(int supplierID,int PlantID)
        {
            Users users = (Users)Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select top 1 A.ID from [sql1.grcboard.com].AgenLapak.dbo.Agen_DtAgen A inner join [sql1.grcboard.com].AgenLapak.dbo.Agen_DtAgenIDtoSupID B on A.ID=B.agenid where B.RowStatus>-1 and A.rowstatus>-1 and B.plantid=" + PlantID+" and  B.supplierID=" + supplierID;
            SqlDataReader sdr = zl.Retrieve();
           
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    //string test = sdr["nama"].ToString().Trim();
                    ddlAgen.SelectedValue= (sdr["id"].ToString().Trim());
                }
            }
        }

        private void LoadDataGridSupplier(ArrayList arrSupplier)
        {
            lstSup.DataSource = arrSupplier;
            lstSup.DataBind();
            GridView1.Visible = false;
            this.GridView1.DataSource = arrSupplier;
            this.GridView1.DataBind();
        }

        private void LoadMataUang()
        {
            ArrayList arrMataUang = new ArrayList();
            MataUangFacade mataUangFacade = new MataUangFacade();
            arrMataUang = mataUangFacade.Retrieve();
            //ddlMataUang.Items.Add(new ListItem("-- Pilih Mata Uang --", string.Empty));
            foreach (MataUang mataUang in arrMataUang)
            {
                ddlMataUang.Items.Add(new ListItem(mataUang.Lambang, mataUang.ID.ToString()));
            }
        }
        private ArrayList LoadGridSupplier()
        {
            ArrayList arrSupplier = new ArrayList();
            NewSuppPurchFacade suppPurchFacade = new NewSuppPurchFacade();
            arrSupplier = suppPurchFacade.Retrieve2();
            if (arrSupplier.Count > 0)
            {
                return arrSupplier;
            }

            arrSupplier.Add(new Supplier());
            return arrSupplier;
        }

        // Bind PagedDataSource into Repeater
        private void BindDataIntoRepeater()
        {
            NewSuppPurchFacade suppPurchFacade = new NewSuppPurchFacade();
            var arrSupplier = suppPurchFacade.Retrieve2();
            _pgsource.DataSource = arrSupplier;
            _pgsource.AllowPaging = true;
            // Number of items to be displayed in the Repeater
            _pgsource.PageSize = _pageSize;
            _pgsource.CurrentPageIndex = CurrentPage;
            // Keep the Total pages in View State
            ViewState["TotalPages"] = _pgsource.PageCount;
            // Example: "Page 1 of 10"
            lblpage.Text = "Page " + (CurrentPage + 1) + " of " + _pgsource.PageCount;
            // Enable First, Last, Previous, Next buttons
            lbPrevious.Enabled = !_pgsource.IsFirstPage;
            lbNext.Enabled = !_pgsource.IsLastPage;
            lbFirst.Enabled = !_pgsource.IsFirstPage;
            lbLast.Enabled = !_pgsource.IsLastPage;

            // Bind data into repeater
            lstSup.DataSource = _pgsource;
            lstSup.DataBind();

            // Call the function to do paging
            HandlePaging();
        }

        private void HandlePaging()
        {
            var arrSupplier = new DataTable();
            arrSupplier.Columns.Add("PageIndex"); //Start from 0
            arrSupplier.Columns.Add("PageText"); //Start from 1

            _firstIndex = CurrentPage - 5;
            if (CurrentPage > 5)
                _lastIndex = CurrentPage + 5;
            else
                _lastIndex = 10;

            // Check last page is greater than total page then reduced it 
            // to total no. of page is last index
            if (_lastIndex > Convert.ToInt32(ViewState["TotalPages"]))
            {
                _lastIndex = Convert.ToInt32(ViewState["TotalPages"]);
                _firstIndex = _lastIndex - 10;
            }

            if (_firstIndex < 0)
                _firstIndex = 0;

            // Now creating page number based on above first and last page index
            for (var i = _firstIndex; i < _lastIndex; i++)
            {
                var dr = arrSupplier.NewRow();
                dr[0] = i;
                dr[1] = i + 1;
                arrSupplier.Rows.Add(dr);
            }

            rptPaging.DataSource = arrSupplier;
            rptPaging.DataBind();
        }

        protected void lbFirst_Click(object sender, EventArgs e)
        {
            CurrentPage = 0;
            BindDataIntoRepeater();
        }
        protected void lbLast_Click(object sender, EventArgs e)
        {
            CurrentPage = (Convert.ToInt32(ViewState["TotalPages"]) - 1);
            BindDataIntoRepeater();
        }
        protected void lbPrevious_Click(object sender, EventArgs e)
        {
            CurrentPage -= 1;
            BindDataIntoRepeater();
        }
        protected void lbNext_Click(object sender, EventArgs e)
        {
            CurrentPage += 1;
            BindDataIntoRepeater();
        }

        protected void rptPaging_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (!e.CommandName.Equals("newPage")) return;
            CurrentPage = Convert.ToInt32(e.CommandArgument.ToString());
            BindDataIntoRepeater();
        }

        protected void rptPaging_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            var lnkPage = (LinkButton)e.Item.FindControl("lbPaging");
            if (lnkPage.CommandArgument != CurrentPage.ToString()) return;
            lnkPage.Enabled = false;
            //lnkPage.BackColor = Color.FromName("#00FF00");
        }

        private ArrayList LoadGridByCriteria()
        {
            ArrayList arrSupplier = new ArrayList();
            NewSuppPurchFacade suppPurchFacade = new NewSuppPurchFacade();
            if (txtSearch.Text == string.Empty)
                arrSupplier = suppPurchFacade.Retrieve();
            else
                arrSupplier = suppPurchFacade.RetrieveByCriteria(ddlSearch.SelectedValue, txtSearch.Text);

            if (arrSupplier.Count > 0)
            {
                return arrSupplier;
            }

            arrSupplier.Add(new NewSuppPurch());
            return arrSupplier;
        }

        private void clearForm()
        {
            ViewState["id"] = null;
            txtSupplierCode.Text = string.Empty;
            txtSupplierName.Text = string.Empty;
            txtAlamat.Text = string.Empty;
            txtJoin.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");


            txtUP.Text = string.Empty;
            ddlSearch.SelectedIndex = 0;
            txtSearch.Text = string.Empty;

            txtTelepon.Text = string.Empty;
            txtHandphone.Text = string.Empty;
            txtFax.Text = string.Empty;
            txtJenisUsaha.Text = string.Empty;

            txtSupplierName.Focus();
            btnDelete.Enabled = false;
            btnUnlock.Enabled = false;
            txtSupplierCode.Enabled = false;
            txtNPWP.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtKTP.Text = string.Empty;
            txtNPWP0.Text = string.Empty;
            ddlSubCompany.SelectedIndex = 0;

            txtNamaRekening.Text = string.Empty;
            txtBankRekening.Text = string.Empty;
            txtNomorRekening.Text = string.Empty;

            //txtSupplierName.Enabled = false;
            //txtAlamat.Enabled = false;

        }

        protected void btnUpdateAlasan1_ServerClick(object sender, EventArgs e)
        {

            Session["AlasanCancel"] = txtAlasanCancel1.Text;
            Session["AlasanBatal"] = txtAlasanCancel1.Text;

            if (Session["AlasanCancel"] != null)
            {
                if (Session["AlasanCancel"].ToString() == string.Empty)
                {
                    DisplayAJAXMessage(this, "Alasan Revisi harus diisi");
                    return;
                }
                NewSuppPurch suppPurch = new NewSuppPurch();
                NewSuppPurchFacade SuppPurchFacade = new NewSuppPurchFacade();

                suppPurch.ID = int.Parse(ViewState["id"].ToString());
                suppPurch.LastModifiedBy = Global.UserLogin.UserName;
                suppPurch.Aktif = 0;
                suppPurch.Keterangan = Session["AlasanCancel"].ToString();
                int intResult = SuppPurchFacade.Delete(suppPurch);
                if (SuppPurchFacade.Error == string.Empty && intResult > 0)
                {
                    LoadDataGridSupplier(LoadGridSupplier());
                    //clearForm();
                    txtKeterangan.Visible = false;

                    InsertLog("Aktif");
                }

                
            }
        }

        protected void btnUpdateClose1_ServerClick(object sender, EventArgs e)
        {
            
        }




        protected void btnUnlock_ServerClick(object sender, EventArgs e)
        {
            mpePopUp1.Show();

            
        }
        protected void btnUpload_ServerClick(object sender, EventArgs e)
        {
            if (Upload1.HasFile)
            {
                Users users = (Users)Session["Users"];
                string FilePath = Upload1.PostedFile.FileName;
                string filename = Path.GetFileName(FilePath);
                string ext = Path.GetExtension(filename);
                int ExtractPos = FilePath.LastIndexOf("\\") + 1;
                String UploadedFileName = FilePath.Substring(ExtractPos, FilePath.Length - ExtractPos);
                if (CheckAttachment(UploadedFileName) == true)
                {
                    DisplayAJAXMessage(this, "File Attacment pernah di upload, ganti nama file terlebih dahulu");
                    return;
                }
                Upload1.PostedFile.SaveAs("D:\\Data Lampiran Purchn\\Supplier\\" + UploadedFileName);
                //TPP_LampiranFacade tppLF = new TPP_LampiranFacade();
                //string idTPP = (Request.QueryString["ba"]);
                //string IDLampiran = tppLF.GetIDlampiran(idTPP);
                //string apv = tppLF.GetApvTPP(idTPP);
                try
                {
                    String pdfUrl = UploadedFileName;
                    if (pdfUrl.IndexOf("/") >= 0 || pdfUrl.IndexOf("\\") >= 0)
                    {
                        Response.End();
                    }
                    ZetroView zl = new ZetroView();
                    zl.QueryType = Operation.CUSTOM;
                    zl.CustomQuery = "insert suppPurchatachment(SupplierID, docName, FileName, RowStatus, CreatedBy, " +
                        "CreatedTime, LastModifiedBy, LastModifiedTime)values((select id from supppurch where suppliercode='" + txtSupplierCode.Text.Trim() + "')" + ",'" +
                        "','" + UploadedFileName + "',0,'" + users.UserName + "',getdate(),'" + users.UserName + "',getdate())";
                    SqlDataReader sdr = zl.Retrieve();

                }
                catch (Exception ex)
                { Response.Write("An error occurred - " + ex.ToString()); }
                LoadListAttachmentCode(txtSupplierCode.Text.Trim(), attachm);
            }
        }
        private bool CheckAttachment(string DocName)
        {
            string tablename = "suppPurchatachment";
            bool rst = false;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select FileName from " + tablename + " where RowStatus >-1 and FileName='" + DocName + "'";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    rst = true;
                }
            }
            return rst;
        }
        protected void attachm_Command(object sender, RepeaterCommandEventArgs e)
        {
            Repeater rpt = (Repeater)sender;
            try
            {
                switch (e.CommandName)
                {
                    case "pre":
                        string Nama = e.CommandArgument.ToString();
                        string Nama2 = @"\" + Nama;
                        string dirPath = @"D:\DATA LAMPIRAN PURCHN\Supplier\";
                        string ext = Path.GetExtension(Nama);
                        HttpResponse response = HttpContext.Current.Response;
                        Response.Clear();
                        string excelFilePath = dirPath + Nama;
                        System.IO.FileInfo file = new System.IO.FileInfo(excelFilePath);
                        if (file.Exists)
                        {
                            response.Clear();
                            response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                            response.AddHeader("Content-Length", file.Length.ToString());
                            response.ContentType = "application/octet-stream";
                            response.WriteFile(file.FullName);
                            response.End();
                        }
                        break;
                    case "hps":
                        Image hps = (Image)rpt.Items[int.Parse(e.CommandArgument.ToString())].FindControl("hapus");
                        ZetroView zl = new ZetroView();
                        zl.QueryType = Operation.CUSTOM;
                        zl.CustomQuery = "Update SuppPurchAtachment set RowStatus=-1 where ID=" + hps.CssClass;
                        SqlDataReader sdr = zl.Retrieve();
                        //LoadTypeSarmut();
                        break;
                }
            }
            catch
            {
                DisplayAJAXMessage(this, "Data belum tersimpan atau di approve");
                return;
            }
        }
        protected void attachm_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ScriptManager scriptMan = ScriptManager.GetCurrent(this.Page);
            Users users = (Users)Session["Users"];
            if (e.Item.ItemType == ListItemType.Header)
            {
                Image info = (Image)e.Item.Parent.Parent.FindControl("info");
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ImageButton btn = (ImageButton)e.Item.FindControl("lihat") as ImageButton;
                scriptMan.RegisterPostBackControl(btn);
                Image pre = (Image)e.Item.FindControl("lihat");
                Image hps = (Image)e.Item.FindControl("hapus");
                //SPD_Attachment att = (SPD_Attachment)e.Item.DataItem;
                //pre.Attributes.Add("onclick", "PDFPreviewSarmut('" + pre.CssClass.ToString() + "')");
                //hps.Visible = (att.Approval < 1) ? true : false;
                //hps.Visible = (users.Apv < 1) ? true : false;
            }
        }
        private void LoadListAttachmentPrs(string BAID, Repeater lst)
        {
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            if (Convert.ToInt32(BAID) == 0)
                return;
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select * " +
                "from suppPurchatachment where rowstatus>-1 and supplierid=" + BAID;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new AttachmentSP
                        {
                            ID = Convert.ToInt32(sdr["ID"].ToString()),
                            SupplierID = Convert.ToInt32(sdr["supplierID"].ToString()),
                            DocName = sdr["DocName"].ToString(),
                            FileName = sdr["FileName"].ToString()
                        });
                    }
                }
            }
            lst.DataSource = arrData;
            lst.DataBind();
        }
        private void LoadListAttachmentCode(string BAID, Repeater lst)
        {
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();

            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select * from suppPurchatachment where rowstatus>-1 and supplierid in (select ID from suppPurch where suppliercode='" + BAID + "')";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new AttachmentSP
                        {
                            ID = Convert.ToInt32(sdr["ID"].ToString()),
                            SupplierID = Convert.ToInt32(sdr["supplierID"].ToString()),
                            DocName = sdr["DocName"].ToString(),
                            FileName = sdr["FileName"].ToString()
                        });
                    }
                }
            }
            lst.DataSource = arrData;
            lst.DataBind();
        }
        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];
            string strValidate = ValidateText();
            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }
            string strEvent = "Insert";
            NewSuppPurch suppPurch = new NewSuppPurch();
            NewSuppPurchFacade suppPurchFacade = new NewSuppPurchFacade();
            if (ViewState["id"] != null)
            {
                suppPurch.ID = int.Parse(ViewState["id"].ToString());
                suppPurch.SupplierCode = txtSupplierCode.Text;
                strEvent = "Edit";
            }
            else
            {
                suppPurchFacade = new NewSuppPurchFacade();
                int noUrut = suppPurchFacade.CountSupplier();
                noUrut = noUrut + 1;
                suppPurch.SupplierCode = "SU" + noUrut.ToString().PadLeft(4, '0');
            }
            suppPurch.SupplierName = txtSupplierName.Text;
            suppPurch.Alamat = txtAlamat.Text;
            suppPurch.JoinDate = DateTime.Parse(txtJoin.Text);
            suppPurch.UP = txtUP.Text;
            suppPurch.JenisUsaha = txtJenisUsaha.Text;
            suppPurch.Telepon = txtTelepon.Text;
            suppPurch.Handphone = txtHandphone.Text;
            suppPurch.Fax = txtFax.Text;
            suppPurch.NamaRekening = txtNamaRekening.Text;
            suppPurch.BankRekening = txtBankRekening.Text;
            suppPurch.NomorRekening = txtNomorRekening.Text;
            suppPurch.CreatedBy = ((Users)Session["Users"]).UserName;
            if (ddlPKP.SelectedValue == "Yes")
                suppPurch.NPWP = txtNPWP.Text;
            else
                suppPurch.NPWP = string.Empty;
            suppPurch.LastModifiedBy = ((Users)Session["Users"]).UserName;
            suppPurch.EMail = txtEmail.Text;
            suppPurch.PKP = ddlPKP.SelectedValue;
            suppPurch.Flag = int.Parse(ddlMataUang.SelectedValue);
            suppPurch.KTP = txtKTP.Text;
            suppPurch.NPWP_P = txtNPWP0.Text;
            suppPurch.SubCompanyID = Convert.ToInt32(ddlSubCompany.SelectedValue);
            int intResult = 0;
            if (suppPurch.ID > 0)
            {
                //DisplayAJAXMessage(this, "Tidak bisa di edit, Harus hubungi IT ..");
                //return;
                intResult = suppPurchFacade.Update(suppPurch);
            }
            else
            {
                intResult = suppPurchFacade.Insert(suppPurch);
                ViewState["id"] = intResult;
                if (suppPurchFacade.Error == string.Empty)
                {
                    if (intResult > 0)
                    {
                        txtSupplierCode.Text = suppPurch.SupplierCode;
                    }
                }
            }

            if (suppPurchFacade.Error == string.Empty && intResult > 0)
            {
                //update link to agen lapak 08-12-2022
                if (suppPurch.SubCompanyID == 5 || suppPurch.SubCompanyID == 6)
                {
                    ArrayList arrData = new ArrayList();
                    ZetroView zl = new ZetroView();
                    zl.QueryType = Operation.CUSTOM;
                    //@AgenID int, @PlantID  int, @SupplierID int, @NamaAgen varchar(max), @NamaSupplier varchar(max)
                    zl.CustomQuery = "exec [sql1.grcboard.com].agenlapak.dbo.insert_from_plant " + ddlAgen.SelectedValue.ToString() + ", " + users.UnitKerjaID.ToString() + "," + ViewState["id"].ToString() + ",'" + 
                        ddlAgen.SelectedItem.Text + "', '" + suppPurch.SupplierName  + "'";
                    SqlDataReader sdr = zl.Retrieve();
                }
                LoadDataGridSupplier(LoadGridSupplier());
                BindDataIntoRepeater();
                txtSupplierCode.Enabled = false;
                txtSupplierName.Enabled = true;
                txtAlamat.Enabled = true;
                //clearForm();
                InsertLog(strEvent);
            }
        }

        protected void btnUpdateAlasan_ServerClick(object sender, EventArgs e)
        {

            Session["AlasanCancel"] = txtAlasanCancel.Text;
            Session["AlasanBatal"] = txtAlasanCancel.Text;

            NewSuppPurch suppPurch = new NewSuppPurch();
            NewSuppPurchFacade SuppPurchFacade = new NewSuppPurchFacade();

            if (Session["AlasanCancel"] != null)
            {
                if (Session["AlasanCancel"].ToString() == string.Empty)
                {
                    DisplayAJAXMessage(this, "Alasan Revisi harus diisi");
                    return;
                }
                suppPurch.ID = int.Parse(ViewState["id"].ToString());
                suppPurch.LastModifiedBy = Global.UserLogin.UserName;
                suppPurch.Aktif = 1;
                suppPurch.Keterangan = Session["AlasanCancel"].ToString();
                int intResult = SuppPurchFacade.Delete(suppPurch);
                if (SuppPurchFacade.Error == string.Empty && intResult > 0)
                {
                    LoadDataGridSupplier(LoadGridSupplier());
                    //clearForm();

                    //lstSup_Command(null,null);
                    InsertLog("Non Aktif");
                }
                else
                {
                    DisplayAJAXMessage(this, SuppPurchFacade.Error);
                }

                
            }

           
        }

        protected void btnDelete_ServerClick(object sender, EventArgs e)
        {
            mpePopUp.Show();
            
        }

        protected void btnUpdateClose_ServerClick(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];

                ViewState["id"] = int.Parse(row.Cells[1].Text);
                txtSupplierCode.Text = CekString(row.Cells[2].Text);
                txtSupplierName.Text = CekString(row.Cells[3].Text);
                txtAlamat.Text = CekString(row.Cells[4].Text);
                txtUP.Text = CekString(row.Cells[5].Text);
                txtTelepon.Text = CekString(row.Cells[6].Text);
                txtFax.Text = CekString(row.Cells[7].Text);
                txtHandphone.Text = CekString(row.Cells[8].Text);
                txtJoin.Text = CekString(row.Cells[9].Text);
                txtNPWP.Text = CekString(row.Cells[10].Text);
                txtEmail.Text = CekString(row.Cells[11].Text);
                txtJenisUsaha.Text = CekString(row.Cells[12].Text);
                txtKeterangan.Text = CekString(row.Cells[13].Text);
                txtNamaRekening.Text = CekString(row.Cells[14].Text);
                txtBankRekening.Text = CekString(row.Cells[15].Text);
                txtNomorRekening.Text = CekString(row.Cells[16].Text);
                ddlSubCompany_SelectedIndexChanged(null, null);

            }
        }
        protected void lstSup_Command(object sender, RepeaterCommandEventArgs e)
        {
            Users user = (Users)Session["Users"];
            int ID = int.Parse(e.CommandArgument.ToString());
            string[] MasterEdited = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("SuppUserEdit", "MasterData").Split(',');
            int posx = Array.IndexOf(MasterEdited, user.UserName.ToLower());
            if (e.CommandName == "add")
            {
                ViewState["id"] = ID;
                NewSuppPurch sp = new NewSuppPurch();
                sp = new NewSuppPurchFacade().RetrieveById(ID);
                txtSupplierCode.Text = sp.SupplierCode.ToString();
                txtSupplierName.Text = sp.SupplierName.ToString();
                txtTelepon.Text = sp.Telepon.ToString();
                txtUP.Text = sp.UP.ToString();
                txtEmail.Text = sp.EMail.ToString();
                txtAlamat.Text = sp.Alamat.ToString();
                txtNPWP.Text = sp.NPWP.ToString();
                txtKeterangan.Text = sp.Keterangan.ToString();
                txtFax.Text = sp.Fax.ToString();
                txtJenisUsaha.Text = sp.JenisUsaha.ToString();
                txtNamaRekening.Text = sp.NamaRekening.ToString();
                txtBankRekening.Text = sp.BankRekening.ToString();
                txtNomorRekening.Text = sp.NomorRekening.ToString();
                txtJoin.Text = sp.JoinDate.ToString();
                ddlMataUang.SelectedValue = (sp.Flag > 1) ? sp.Flag.ToString() : "1";
                txtHandphone.Text = sp.Handphone.ToString();
                ddlPKP.SelectedValue = (sp.PKP.Trim() == "") ? "No" : sp.PKP.Trim();
                ddlSubCompany.SelectedValue = sp.SubCompanyID.ToString();
                txtKTP.Text = sp.KTP;
                txtNPWP0.Text = sp.NPWP_P;

                Label3.Visible = (sp.PKP.Trim() == "No") ? false : true;
                if (user.Apv > 1)
                {
                    btnDelete.Visible = (sp.Aktif == 0) ? true : false;
                    btnUnlock.Visible = (sp.Aktif == 0) ? false : true;
                }
                if (sp.Aktif == 0)
                {
                    txtKeterangan.Visible = (sp.Keterangan != null) ? false : true;
                }
                if (sp.Aktif == 1)
                {
                    txtKeterangan.Visible = (sp.Keterangan != null) ? true : false;
                }
                btnDelete.Enabled = (posx > -1) ? true : false;
                btnUnlock.Enabled = (posx > -1) ? true : false;
                btnUpdate.Disabled = (posx > -1) ? false : true;
                statSupp.InnerHtml = (sp.Aktif == 0) ? "" : "<i>[ Non Aktif Supplier ]</i>";
                //txtInfoRevisi.Text = sp.Keterangan(true);
                //txtInfoRevisi.ToolTip = sp.Keterangan();
                //revInfo.Attributes.Add("style", "color:Blue;font-size:x-small; position:absolute; float:left;border:2px solid grey; background-color:Highlight");
                //statSupp.InnerHtml = (sp.Keterangan != null) ? txtKeterangan.Text : "";
                Session.Remove("AlasanCancel");
                //btnDelete.Attributes.Add("onclick", "return confirm_lock();");
                //btnUnlock.Attributes.Add("onclick", "return confirm_lock();");
                btnUpload.Visible = true;
                //btnUpload.Attributes.Add("onclick", "OpenDialog('" + sp.ID.ToString().Trim() + "&tp=1')");
                LoadListAttachmentPrs(sp.ID.ToString().Trim(), attachm);
                ddlSubCompany_SelectedIndexChanged(null, null);
            }
        }
        protected void lstSup_Databound(object sender, RepeaterItemEventArgs e)
        {
            Users user = (Users)Session["Users"];
            NewSuppPurch sp = (NewSuppPurch)e.Item.DataItem;
            HtmlTableRow trd = (HtmlTableRow)e.Item.FindControl("lstR");
            txtKeterangan.Text = sp.Keterangan.ToString();
            trd.Attributes["class"] = (sp.Aktif == 1) ? "EvenRows baris result_fail" : "EvenRows baris";
            trd.Attributes["title"] = (sp.Aktif == 1) ? "Non Aktif supplier" : "";
            //trd.Attributes["title"] = (sp.Keterangan != null) ? txtKeterangan.Text : "";
            string[] MasterEdited = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("SuppUserEdit", "MasterData").Split(',');
            int posx = Array.IndexOf(MasterEdited, user.UserName);
            Image img = (Image)e.Item.FindControl("btnEdit");
            //img.Visible = (posx > -1) ? true : false;

            var col = e.Item.FindControl("ket");
            col.Visible = false;

        }
        private string CekString(string strValue)
        {
            if (strValue == "&nbsp;")
                return string.Empty;

            return strValue;
        }




        //private void SelectSalesman(string strSalesmanName)
        //{
        //    ddlSalesman.ClearSelection();
        //    foreach (ListItem item in ddlSalesman.Items)
        //    {
        //        if (item.Text == strSalesmanName)
        //        {
        //            item.Selected = true;
        //            return;
        //        }
        //    }
        //}

        //private void SelectKabupaten(string strKabupaten)
        //{
        //    ddlKabupaten.ClearSelection();
        //    foreach (ListItem item in ddlKabupaten.Items)
        //    {
        //        if (item.Text == strKabupaten)
        //        {
        //            item.Selected = true;
        //            return;
        //        }
        //    }
        //}


        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            LoadDataGridSupplier(LoadGridByCriteria());
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearForm();
            txtSupplierCode.Enabled = true;
            txtSupplierName.Enabled = true;
            txtAlamat.Enabled = true;
        }

        //private void SelectZona(string strZonaCode)
        //{
        //    ddlZonaCode.ClearSelection();
        //    foreach (ListItem item in ddlZonaCode.Items)
        //    {
        //        if (item.Text == strZonaCode)
        //        {
        //            item.Selected = true;
        //            return;
        //        }
        //    }
        //}

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            // e.Row.Cells[4].Text = DateTime.Parse(e.Row.Cells[4].Text).ToString("dd-MMM-yy");
        }

        //protected void ddlPropinsi_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlPropinsi.SelectedIndex > 0)
        //        LoadCity();
        //}

        //protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlCity.SelectedIndex > 0)
        //        LoadKabupaten();
        //}

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            if (txtSearch.Text == string.Empty)
                LoadDataGridSupplier(LoadGridSupplier());
            else
                LoadDataGridSupplier(LoadGridByCriteria());
        }

        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Master Supplier";
            eventLog.EventName = eventName;
            eventLog.DocumentNo = txtSupplierCode.Text;
            eventLog.CreatedBy = ((Users)Session["Users"]).UserName;

            EventLogFacade eventLogFacade = new EventLogFacade();
            int intResult = eventLogFacade.Insert(eventLog);
            if (eventLogFacade.Error == string.Empty)
                clearForm();
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private string ValidateText()
        {
            string rmsg = string.Empty;
            if (txtSupplierName.Text.Trim() == string.Empty)
            {
                return "Nama Customer tidak boleh kosong";
            }
            else if (txtAlamat.Text.Trim() == string.Empty)
            {
                return "Alamat tidak boleh kosong";
            }
            else if (txtJenisUsaha.Text.Trim() == string.Empty)
            {
                return "Jenis Usaha tidak boleh kosong";
            }
            else if (txtHandphone.Text.Trim() == string.Empty)
            {
                return "Handphone tidak boleh kosong";
            }
            else if (txtNamaRekening.Text.Trim() == string.Empty)
            {
                return "Nama Rekening tidak boleh kosong";
            }
            else if (txtBankRekening.Text.Trim() == string.Empty)
            {
                return "Nama Bank Rekening tidak boleh kosong";
            }
            else if (txtNomorRekening.Text.Trim() == string.Empty)
            {
                return "Nomor Rekening tidak boleh kosong";
            }
            else if (ddlSubCompany.SelectedValue == "5" || ddlSubCompany.SelectedValue == "6")
            {
                if (ddlAgen.SelectedIndex == 0)
                {
                     rmsg= "Agen lapak harus diisi";
                }
                return rmsg;
            }
            else
            {
                /*else 
                //else if (ddlSalesman.SelectedIndex == 0)
                //    return "Pilih Sales";
                else if (txtUP.Text == string.Empty)
                    return "Nama UP tidak boleh kosong";
                else if (ddlPKP.SelectedValue=="Yes")
                {
                    if (txtNPWP.Text.Trim().Length != 20)
                        return "NPWP belum lengkap, hubungi Purchasing Dept.";
                }
                try
                {
                    //decimal dec = decimal.Parse(txtCreditLimit.Text);
                }
                catch
                {
                    return "Batas credit harus numeric";
                }
                if (IsEmail(txtEmail.Text)==false)
                    return "Alamat Email salah";*/
                return string.Empty;
            }
        }

        public const string MatchEmailPattern =
                 @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
          + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
          + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
          + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

        public static bool IsEmail(string email)
        {
            if (email != null) return Regex.IsMatch(email, MatchEmailPattern);
            else return false;
        }

        protected void ddlSubCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            Users user = (Users)Session["Users"];
            if (ddlSubCompany.SelectedValue =="5" || ddlSubCompany.SelectedValue == "6")
            {
                lblAgen.Visible = true;
                ddlAgen.Visible = true;
                if (ViewState["id"] != null)
                {
                    GetLapak(int.Parse(ViewState["id"].ToString()), user.UnitKerjaID);
                }
            }
            else
            {
                lblAgen.Visible = false;
                ddlAgen.Visible = false;
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPKP.SelectedValue == "Yes")
                Label3.Visible = true;
            else
                Label3.Visible = false;

        }


        /* menambah class css pada gridview row at server side */
        //protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        //{
        //    //on mouse over
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        Users user = (Users)Session["Users"];
        //        NewSuppPurch sp = new NewSuppPurch();

        //        txtKeterangan.Text = sp.Keterangan.ToString();
        //        //trd.Attributes["class"] = (sp.Aktif == 1) ? "EvenRows baris result_fail" : "EvenRows baris";
        //        //trd.Attributes["title"] = (sp.Aktif == 1) ? "Non Aktif supplier" : "";

        //        if (sp.Aktif == 1)
        //        {
        //            e.Row.Attributes.Add("title", "Non Aktif supplier");
        //        }
        //        if (sp.Aktif == 1)
        //        {
        //            e.Row.Attributes.Add("class", "EvenRows baris result_fail");
        //        }
        //        else 
        //        {
        //            e.Row.Attributes.Add("class", "EvenRows baris");
        //        }
        //        //e.Row.Attributes.Add("onmouseout", "this.className='normal'");
        //    }

        //    //if (e.Row.RowType == DataControlRowType.Header) 
        //    //{
        //    //e.Row.Cells[1].CssClass = "hidecontrol"; //css client side
        //    //}
        //}
        /* menambah class css pada gridview row at server side */
    }
}

public class AttachmentSP : GRCBaseDomain
{
    public string FileName { get; set; }
    public string DocName { get; set; }
    public int SupplierID { get; set; }
    public int ID { get; set; }
}