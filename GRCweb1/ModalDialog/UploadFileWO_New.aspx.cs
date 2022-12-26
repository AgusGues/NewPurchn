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
    public partial class UploadFileWO_New : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string WOID = (Request.QueryString["wo"] != null) ? Request.QueryString["wo"].ToString() : "";
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];
            WorkOrder_New work1 = new WorkOrder_New();
            WorkOrderFacade_New workF1 = new WorkOrderFacade_New();
            work1 = workF1.RetrievePlantID(int.Parse(Request.QueryString["wo"].ToString()));

            WorkOrder_New work = new WorkOrder_New();
            WorkOrderFacade_New workF = new WorkOrderFacade_New();

            if (Upload1.HasFile)
            {
                //Users users = (Users)Session["Users"];
                string FilePath = Upload1.PostedFile.FileName;
                string filename = Path.GetFileName(FilePath);
                string ext = Path.GetExtension(filename);
                int ExtractPos = FilePath.LastIndexOf("\\") + 1;
                String UploadedFileName = FilePath.Substring(ExtractPos, FilePath.Length - ExtractPos);
                Upload1.PostedFile.SaveAs(Path.Combine(@"D:\DATA LAMPIRAN PURCHN\WO MTC\", filename));

                if (ext.ToLower() == ".pdf")
                {
                    Stream fs = Upload1.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);

                    work.WOID = int.Parse(Request.QueryString["wo"].ToString());
                    int ID = work.WOID;
                    if (ID != 0)
                    {
                        int intResult = 0;
                        work.WOID = ID;
                        work.FileName = filename.ToString();
                        work.CreatedBy = users.UserName;
                        work.FileLampiranOP = bytes;
                        work.PlantID = work1.PlantID;
                        work.AreaWO = work1.AreaWO;
                        work.ToDept = work1.ToDept;


                        intResult = workF.insertLampiranWO(work);
                    }

                    Response.Write("<script language='javascript'>window.close();</script>");
                }
            }
        }


        private bool CheckAttachment(string DocName, string BAID)
        {
            bool rst = false;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select DocName from BeritaAcaraAttachment where RowStatus >-1 and DocName='" + DocName + "' and BAID=" + BAID;
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

    }
}