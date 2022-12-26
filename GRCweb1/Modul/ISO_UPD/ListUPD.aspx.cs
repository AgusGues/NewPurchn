using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using Domain;
using BusinessFacade;
/*using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;*/
using System.Text;

namespace GRCweb1.Modul.ISO_UPD
{
    public partial class ListUPD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.Count == 0)
            {
                Response.Redirect("~/ISO_UPD/ListUPD.aspx");
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    Global.link = "~/Default.aspx";
                    //LoadData();
                    btnCancel.Disabled = true;
                    PanelUtama.Visible = false;
                    PanelISI.Visible = false;
                    PanelUpload.Visible = false;
                    Users users = (Users)Session["Users"];
                    int DeptiD = users.DeptID;
                    Session["DeptID"] = DeptiD;
                    LoadData();

                    if (users.DeptID == 23)
                    {
                        RB1.Visible = true; RB2.Visible = true; RB1.Checked = true;
                    }
                    else
                    {
                        RB1.Visible = false; RB2.Visible = false;
                    }
                    //((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(Button);
                }
            }

        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(Button);
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(Button1);
        }

        protected void btnList_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("ListUPD.aspx");
        }

        protected void RB1_CheckedChanged(object sender, EventArgs e)
        {
            RB2.Checked = false; LoadData();
        }

        protected void RB2_CheckedChanged(object sender, EventArgs e)
        {
            RB1.Checked = false; LoadData2();
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private void LoadData()
        {
            ISO_Upd uPD = new ISO_Upd();
            Users users = (Users)Session["Users"];
            ArrayList arrUPD = new ArrayList();
            ISO_UpdFacade updF = new ISO_UpdFacade();
            int deptid = users.DeptID;
            arrUPD = updF.RetrieveListUPD(Convert.ToInt32(Session["DeptID"]));
            GridView1.DataSource = arrUPD;
            GridView1.DataBind();
        }

        private void LoadData2()
        {
            ISO_Upd uPD2 = new ISO_Upd();
            //Users users = (Users)Session["Users"];
            ArrayList arrUPD2 = new ArrayList();
            ISO_UpdFacade updF2 = new ISO_UpdFacade();
            //int deptid = users.DeptID;
            arrUPD2 = updF2.RetrieveListUPD2();
            GridView1.DataSource = arrUPD2;
            GridView1.DataBind();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                Users users = (Users)Session["Users"];
                PanelISI.Visible = true;
                PanelUtama.Visible = true;
                PanelUpload.Visible = false;
                Button1.Enabled = true;
                btnlampiran.Disabled = false; PanelH.Visible = false;

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                row.BackColor = System.Drawing.Color.GreenYellow;

                Session["UPDid"] = int.Parse(row.Cells[0].Text);
                ViewState["id"] = int.Parse(row.Cells[0].Text);
                txtID.Text = int.Parse(row.Cells[0].Text).ToString();
                txtNo.Text = CekString(row.Cells[2].Text);
                txtNama.Text = CekString(row.Cells[3].Text);
                txtNamaFile.Text = CekString(row.Cells[9].Text);
                Session["NamaDokumen"] = txtNamaFile.Text;
                int IDupd = int.Parse(txtID.Text);
                string NamaDep = CekString(row.Cells[1].Text);
                if (NamaDep.Contains("&amp;"))
                {
                    NamaDep = NamaDep.Replace("&amp;", "&");
                }

                ISO_UpdFacade updFacade = new ISO_UpdFacade();
                ISO_Upd uPD = new ISO_Upd();
                uPD = updFacade.CekStatusApv(IDupd);



                ISO_UpdFacade updF0 = new ISO_UpdFacade();
                ISO_Upd sp0 = new ISO_Upd();
                string NamaDept = string.Empty;
                sp0 = updF0.RetrieveForNamaDept(users.DeptID);

                if (sp0.DeptName != "")
                {
                    NamaDept = sp0.DeptName;    // Baru add 25 Maret 2017        
                }

                if (uPD.apv <= 2)
                {
                    btnCancel.Disabled = false;
                    //LoadFile();

                    DataTable dt = new DataTable();
                    dt.Columns.Add("File");
                    dt.Columns.Add("Date");

                    DirectoryInfo info = new DirectoryInfo(Path.Combine(@"D:\DATA LAMPIRAN PURCHN\UPD\", NamaDept));
                    FileInfo[] files = info.GetFiles(CekString(row.Cells[9].Text));

                    foreach (FileInfo file in files)
                    {
                        dt.Rows.Add(file.Name, file.CreationTime);
                    }

                    GridView2.DataSource = dt;
                    GridView2.DataBind();
                }

                //btnCancel.Disabled = true;

                //DataTable dt = new DataTable();
                //dt.Columns.Add("File");
                //dt.Columns.Add("Date");
                //GridView2.DataSource = dt;
                //GridView2.DataBind();
            }
        }
        private void LoadUploadForm()
        {
            LoadData();
        }
        //private void LoadFile()
        //{  
        //    Users users = (Users)Session["Users"];
        //    int userDeptID = users.DeptID;
        //    string NamaDept = string.Empty;

        //    if (userDeptID != 0)
        //    {
        //        if (userDeptID == 23) { NamaDept = "ISO"; }
        //        else if (userDeptID == 14) { NamaDept = "IT"; }
        //        else if (userDeptID == 7) { NamaDept = "HRD"; }
        //        else if (userDeptID == 15) { NamaDept = "Purchasing"; }
        //        else if (userDeptID == 26) { NamaDept = "Transportation"; }
        //        else if (userDeptID == 13) { NamaDept = "Marketing"; }
        //        else if (userDeptID == 11) { NamaDept = "PPIC"; }
        //        else if (userDeptID == 6) { NamaDept = "LogistikBJ"; }
        //        else if (userDeptID == 10) { NamaDept = "LogistikBB"; }
        //        else if (userDeptID == 3) { NamaDept = "Finishing"; }
        //        else if (userDeptID == 2) { NamaDept = "BoardMill"; }
        //        else if (userDeptID == 19 || userDeptID == 4 || userDeptID == 5 || userDeptID == 18) { NamaDept = "Maintenance"; }
        //        else { NamaDept = "QA"; }
        //    }

        //    Session["NamaDokumen"] = txtNamaFile.Text;
        //    string Nama = txtNamaFile.Text;

        //    if (Nama != string.Empty)
        //    {
        //        DataTable dt = new DataTable();
        //        dt.Columns.Add("File");
        //        dt.Columns.Add("Date");

        //        DirectoryInfo info = new DirectoryInfo(Server.MapPath("~/App_Data/UploadUPD/" + NamaDept + "/"));
        //        FileInfo[] files = info.GetFiles(Nama);

        //        foreach (FileInfo file in files)
        //        {
        //            dt.Rows.Add(file.Name, file.CreationTime);
        //        }

        //        GridView2.DataSource = dt;
        //        GridView2.DataBind();
        //    }
        //    else
        //    {
        //        DataTable dt1 = new DataTable();
        //        dt1.Columns.Add("File");
        //        dt1.Columns.Add("Date");

        //        GridView2.DataSource = dt1;
        //        GridView2.DataBind();
        //    }
        //}

        private void LoadFile()
        {
            Users users = (Users)Session["Users"];
            int userDeptID = users.DeptID;
            string NamaDept = string.Empty;
            string UpdID = Session["UpdID"].ToString();
            string NamaFile = Session["NamaDokumen"].ToString();

            ISO_Upd upd1 = new ISO_Upd();
            ISO_DMDFacade dmdF = new ISO_DMDFacade();

            upd1 = dmdF.cekDept(UpdID);
            Session["DeptID1"] = upd1.DeptID;

            if (upd1.DeptID != 0)
            {
                if (upd1.DeptID == 23) { NamaDept = "ISO"; }
                else if (upd1.DeptID == 14) { NamaDept = "IT"; }
                else if (upd1.DeptID == 7) { NamaDept = "HRD"; }
                else if (upd1.DeptID == 15) { NamaDept = "Purchasing"; }
                else if (upd1.DeptID == 26) { NamaDept = "Delivery"; }
                else if (upd1.DeptID == 13) { NamaDept = "Marketing"; }
                else if (upd1.DeptID == 11) { NamaDept = "PPIC"; }
                else if (upd1.DeptID == 6) { NamaDept = "LogistikBJ"; }
                else if (upd1.DeptID == 10) { NamaDept = "LogistikBB"; }
                else if (upd1.DeptID == 3) { NamaDept = "Finishing"; }
                else if (upd1.DeptID == 2) { NamaDept = "BM"; }
                else if (upd1.DeptID == 19 || upd1.DeptID == 4 || upd1.DeptID == 5 || upd1.DeptID == 18) { NamaDept = "Maintenance"; }
                else { NamaDept = "QA"; }
            }

            //Session["NamaDokumen"] = txtNamaFile2.Value;
            //string Nama = txtNamaFile2.Value;

            if (NamaFile != string.Empty)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("File");
                dt.Columns.Add("Date");

                //DirectoryInfo info = new DirectoryInfo(Server.MapPath("~/App_Data/UploadUPD/" + NamaDept + "/"));
                DirectoryInfo info = new DirectoryInfo(Path.Combine(@"D:\DATA LAMPIRAN PURCHN\UPD\", NamaDept));
                FileInfo[] files = info.GetFiles(NamaFile);

                foreach (FileInfo file in files)
                {
                    dt.Rows.Add(file.Name, file.CreationTime);
                }

                GridView2.DataSource = dt;
                GridView2.DataBind();
            }
            else
            {
                DataTable dt1 = new DataTable();
                dt1.Columns.Add("File");
                dt1.Columns.Add("Date");

                GridView2.DataSource = dt1;
                GridView2.DataBind();
            }
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Users users = (Users)Session["Users"];
            ISO_UpdFacade updF = new ISO_UpdFacade();
            ISO_Upd sp = new ISO_Upd();
            string NamaDept = string.Empty;

            //int userDeptID = Convert.ToInt32(Session["userDeptID"]);
            sp = updF.RetrieveForNamaDept(users.DeptID);

            if (sp.DeptName.ToString() != "")
            {
                NamaDept = sp.DeptName;    // Baru add 25 Maret 2017        
            }

            if (e.CommandName == "Download")
            {

                string Nama = e.CommandArgument.ToString();
                string Nama2 = @"\" + Nama;
                string dirPath = Path.Combine(@"D:\DATA LAMPIRAN PURCHN\UPD\", NamaDept);
                string ext = Path.GetExtension(Nama);

                Response.Clear();

                if (ext == ".xlsx" || ext == ".docx")
                {
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                }
                else
                {
                    Response.ContentType = "application/octet-stream";
                }

                Response.AppendHeader("Content-Disposition", "filename="
                    + e.CommandArgument);
                Response.TransmitFile(Path.Combine(dirPath, Nama));
                Response.Flush();
            }

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    DataRowView drv = e.Row.DataItem as DataRowView;
            //    if (drv["ColumnName"].ToString().Equals("Something"))
            //    {
            //        e.Row.BackColor = System.Drawing.Color.Red;
            //    }
            //    else
            //    {
            //        e.Row.BackColor = System.Drawing.Color.Green;
            //    }
            //}
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                GridView2.Enabled = true;

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton lbn1 = e.Row.FindControl("LinkButton1") as LinkButton;
                    ScriptManager.GetCurrent(this).RegisterPostBackControl(lbn1);
                }
            }
            catch (Exception ex)
            { }

        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }
        protected void btn1_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("FormInputUPDNew.aspx");
        }
        protected void btnCancel_ServerClick(object sender, EventArgs e)
        {
            ISO_Upd upd = new ISO_Upd();
            ISO_DMDFacade updFacade = new ISO_DMDFacade();
            int ID = int.Parse(txtID.Text);
            upd.LastModifiedBy = ((Users)Session["Users"]).UserName;
            upd.ID = ID;

            int intResult = 0;
            intResult = updFacade.CancelUPD(upd);

            clearForm();
            btnCancel.Disabled = true;
            LoadData();
        }

        protected void btnUpload_ServerClick(object sender, EventArgs e)
        {

        }

        protected void btnlampiran_ServerClick(object sender, EventArgs e)
        {
            PanelUpload.Visible = true;
            //PanelISI.Visible = true;
            PanelUtama.Visible = true;
            FileUpload1.Enabled = false;
            PanelISI.Visible = false;
            //PanelUtama.Visible = false;
            Button.Enabled = false;
            btnCancel.Disabled = true;
            btnlampiran.Disabled = true;
        }


        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadData();
        }

        private string CekString(string strValue)
        {
            if (strValue == "&nbsp;")
                return string.Empty;

            return strValue;
        }
        private void clearForm()
        {
            txtID.Text = string.Empty;
            txtNo.Text = string.Empty;
            txtNama.Text = string.Empty;
            //txtAlasan.Text = string.Empty;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            FileUpload1.Enabled = true;
            Button.Enabled = true;
            Button1.Enabled = false;
        }

        protected void Button_Click(object sender, EventArgs e)
        {
            //FileUpload1.Enabled = false;
            Button1.Enabled = false;
            PanelUtama.Visible = true;
            btnCancel.Disabled = true;

            Users user = (Users)Session["Users"];
            int userDeptID = user.DeptID;
            string NamaDept = string.Empty;
            ISO_Upd upd1 = new ISO_Upd();
            ISO_DMDFacade updFacade = new ISO_DMDFacade();

            if (userDeptID != 0)
            {
                if (userDeptID == 23) { NamaDept = "ISO"; Session["NamaDept"] = NamaDept; }
                else if (userDeptID == 14) { NamaDept = "IT"; Session["NamaDept"] = NamaDept; }
                else if (userDeptID == 7) { NamaDept = "HRD"; Session["NamaDept"] = NamaDept; }
                else if (userDeptID == 15) { NamaDept = "Purchasing"; Session["NamaDept"] = NamaDept; }
                else if (userDeptID == 26) { NamaDept = "Transportation"; Session["NamaDept"] = NamaDept; }
                else if (userDeptID == 13) { NamaDept = "Marketing"; Session["NamaDept"] = NamaDept; }
                else if (userDeptID == 11) { NamaDept = "PPIC"; Session["NamaDept"] = NamaDept; }
                else if (userDeptID == 6) { NamaDept = "LogistikBJ"; Session["NamaDept"] = NamaDept; }
                else if (userDeptID == 10) { NamaDept = "LogistikBB"; Session["NamaDept"] = NamaDept; }
                else if (userDeptID == 3) { NamaDept = "Finishing"; Session["NamaDept"] = NamaDept; }
                else if (userDeptID == 2) { NamaDept = "BoardMill"; Session["NamaDept"] = NamaDept; }
                else if (userDeptID == 30 || userDeptID == 31) { NamaDept = "RnD"; Session["NamaDept"] = NamaDept; }
                else if (userDeptID == 19 || userDeptID == 4 || userDeptID == 5 || userDeptID == 18) { NamaDept = "Maintenance"; Session["NamaDept"] = NamaDept; }
                else { NamaDept = "QA"; Session["NamaDept"] = NamaDept; }
            }
            string NamaDept1 = Session["NamaDept"].ToString();

            if (FileUpload1.HasFile)
            {
                //string fileName = FileUpload1.FileName;
                //FileUpload1.PostedFile.SaveAs(Server.MapPath("~/App_Data/UploadUPD/" + NamaDept + "/") + fileName);
                string fileName = FileUpload1.FileName;

                string ext = Path.GetExtension(fileName);

                //if (ext.ToLower() != ".xls" && ext.ToLower() != ".doc")
                //{
                //    DisplayAJAXMessage(this, " Format yg di izin kan : xls,doc"); return;
                //}

                if (ext.ToLower() != ".xls" && ext.ToLower() != ".doc" && ext.ToLower() != ".docx" && ext.ToLower() != ".xlsx")
                {
                    DisplayAJAXMessage(this, " Format yg di izin kan : xls,xlsx,doc,docx"); return;
                }

                string dirPath = Path.Combine(@"D:\", "DATA LAMPIRAN PURCHN");
                if (!System.IO.Directory.Exists(dirPath))
                {
                    System.IO.Directory.CreateDirectory(dirPath);
                }

                string dirPath1 = Path.Combine(@"D:\DATA LAMPIRAN PURCHN\", "UPD");
                if (!System.IO.Directory.Exists(dirPath1))
                {
                    System.IO.Directory.CreateDirectory(dirPath1);
                }

                string dirPath2 = Path.Combine(@"D:\DATA LAMPIRAN PURCHN\UPD\", NamaDept1);
                Session["dirPath2"] = dirPath2;
                if (!System.IO.Directory.Exists(dirPath2))
                {
                    System.IO.Directory.CreateDirectory(dirPath2);
                }

                FileUpload1.PostedFile.SaveAs(Path.Combine(dirPath2, fileName));
                Session["linkFile"] = fileName;



                upd1.UPDid = Convert.ToInt32(Session["UPDid"]);
                upd1.NamaFile = fileName;
                upd1.CreatedBy = user.UserName;

                int intResult = 0;
                //intResult = updFacade.InsertLampiran(upd1);

                ISO_Upd domain1 = new ISO_Upd();
                ISO_DMDFacade facade1 = new ISO_DMDFacade();
                int CekLampiran = facade1.CekLampiran(Convert.ToInt32(Session["UPDid"]));

                if (CekLampiran > 0)
                {
                    ISO_Upd domain2 = new ISO_Upd();
                    ISO_DMDFacade facade2 = new ISO_DMDFacade();
                    domain2.ID = Convert.ToInt32(Session["UPDid"]);
                    domain2.FileNama = fileName.Trim();

                    intResult = facade2.UpdateLampiran(domain2);
                }
                else
                {
                    intResult = updFacade.InsertLampiran(upd1);
                }

            }
            string NamaFileLampiran = Session["linkFile"].ToString();
            DataTable dt = new DataTable();
            dt.Columns.Add("File");
            dt.Columns.Add("Size");
            string dirPath3 = Session["dirPath2"].ToString();
            FileInfo fi = new FileInfo(Path.Combine(dirPath3, NamaFileLampiran));

            int Bytes = Convert.ToInt32(fi.Length);
            decimal KiloBytes = Bytes / 1000;
            decimal MB1 = KiloBytes / 1024;
            decimal MB = System.Math.Round(MB1, 3);

            dt.Rows.Add(fi.Name, KiloBytes + " " + "Kb" + "/" + " " + MB + " " + "Mb");

            LoadData();

        }
    }
}