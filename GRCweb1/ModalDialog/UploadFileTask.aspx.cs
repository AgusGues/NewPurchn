using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Domain;
using BusinessFacade;
using DataAccessLayer;

namespace GRCweb1.ModalDialog
{
    public partial class UploadFileTask : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string BAID = (Request.QueryString["ba"] != null) ? Request.QueryString["ba"].ToString() : "";
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
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
                Upload1.PostedFile.SaveAs("D:\\Data Lampiran Purchn\\IsoTask\\" + UploadedFileName);
                try
                {
                    String pdfUrl = UploadedFileName;
                    if (pdfUrl.IndexOf("/") >= 0 || pdfUrl.IndexOf("\\") >= 0)
                    {
                        Response.End();
                    }
                    string tablename = Request.QueryString["tablename"].ToString();
                    ZetroView zl = new ZetroView();
                    zl.QueryType = Operation.CUSTOM;
                    zl.CustomQuery =
                        "insert " + tablename + "( TaskID, FileName, RowStatus, CreatedBy, CreatedTime, LastModifiedBy, LastModifiedTime)values(" +
                        int.Parse(Request.QueryString["ba"].ToString()) + ",'" +
                        UploadedFileName + "',0,'" + users.UserName + "',getdate(),'" + users.UserName + "',getdate())";
                    SqlDataReader sdr = zl.Retrieve();
                    //if (Request.QueryString["from"].ToString() == "listtask")
                    //{
                    //    DeptFacade deptFacade = new DeptFacade();
                    //    Dept dept = deptFacade.RetrieveDeptByUserID(((Users)Session["Users"]).ID);

                    //    string strDeptID = ((Users)Session["Users"]).DeptID.ToString();// dept.DeptID.ToString();

                    //    Response.Redirect("ListTask.aspx?TaskDept=" + strDeptID + "&TipeTask=Solved" + "&FormTask=1");
                    //}
                    CloseWindow(this);
                }
                catch (Exception ex)
                { Response.Write("An error occurred - " + ex.ToString()); lblMessage.Text = "Upload Data Gagal!"; }
            }
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        private bool CheckAttachment(string DocName)
        {
            string tablename = Request.QueryString["tablename"].ToString();
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
        static public void CloseWindow(Control page)
        {
            string myScript = "window.close();";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "MyScript", myScript, true);
        }
    }
}