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
using System.Net;

using System.Runtime.InteropServices;

namespace GRCweb1.ModalDialog
{
    public partial class UploadFileUPD_APlant : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Users users = (Users)Session["Users"];
                string IDmaster = (Request.QueryString["ba"] != null) ? Request.QueryString["ba"].ToString() : "";
                Session["IDmaster"] = IDmaster;

                Upload1.Enabled = false; btnUpload.Enabled = false; txtNo.Enabled = false;
                txtNama.Enabled = false; btnCek.Enabled = false; btnCek2.Enabled = false;

                if (users.UnitKerjaID == 1)
                {
                    RBC.Enabled = false; RBK.Enabled = true; RBJ.Enabled = true;
                }
                else if (users.UnitKerjaID == 7)
                {
                    RBK.Enabled = false; RBC.Enabled = true; RBJ.Enabled = true;
                }
                else if (users.UnitKerjaID == 13)
                {
                    RBJ.Enabled = false; RBK.Enabled = true; RBC.Enabled = true;
                }

                if (RBBiasa.Checked == true)
                {
                    tr1.Visible = true; tr2.Visible = false; tr01.Visible = false;
                }
                else
                {
                    tr1.Visible = false; tr2.Visible = true; tr01.Visible = false;
                }
            }
        }

        protected void btnCek_ServerClick(object sender, EventArgs e)
        {
            if (RBC.Checked == false && RBK.Checked == false && RBJ.Checked == false)
            {
                DisplayAJAXMessage(this, "Plant tujuan harus dipilih !!"); return;
            }

            UPDdomain upd = new UPDdomain();
            UPDfacade fupd = new UPDfacade();
            string queryLink = string.Empty; string queryLokasi = string.Empty; string queryLink_Jmbg = string.Empty;
            string nodokumen = txtNo.Text.Trim(); string namadokumen = txtNama.Text.Trim();

            if (RBC.Checked == true)
            {
                queryLokasi = "Citeureup";
                queryLink = " select * from [sqlctrp.grcboard.com].bpasctrp.dbo.ISO_UPDTemp where RowStatus>-1 and NoDokumen='" + nodokumen + "'";
            }

            if (RBK.Checked == true)
            {
                queryLokasi = "Karawang";
                queryLink = " select * from [sqlkrwg.grcboard.com].bpaskrwg.dbo.ISO_UPDTemp where RowStatus>-1 and NoDokumen='" + nodokumen + "'";
            }

            if (RBJ.Checked == true)
            {
                queryLokasi = "Jombang";
                queryLink = " select * from [sqljombang.grcboard.com].bpasjombang.dbo.ISO_UPDTemp where RowStatus>-1 and NoDokumen='" + nodokumen + "'";
                //queryLink = " select * from [sqljombang.grcboard.com].bpasjombang.dbo.ISO_UPDTemp where RowStatus>-1 and NamaDokumen='" + namadokumen + "'";            
            }

            upd = fupd.Retrieve(queryLink);

            if (upd.ID > 0)
            {
                Upload1.Enabled = true; btnUpload.Enabled = true; tr1.Visible = true; tr2.Visible = false; tr01.Visible = true; tr02.Visible = false;
                LabelNo.Visible = true; LabelNo.Text = " Dokumen ditemukan di Plant " + queryLokasi;
                LabelNo.Attributes.Add("style", "font-family: Calibri; font-size: small; font-weight: 700; color:green;");
            }
            else
            {
                Upload1.Enabled = false; btnUpload.Enabled = false; tr1.Visible = true; tr2.Visible = false; tr01.Visible = true; tr02.Visible = false;
                LabelNo.Visible = true; LabelNo.Text = " Dokumen tidak ditemukan di Plant " + queryLokasi;
                LabelNo.Attributes.Add("style", "font-family: Calibri; font-size: small; font-weight: 700; color:red;");
            }
        }

        protected void btnCek2_ServerClick(object sender, EventArgs e)
        {
            if (RBC.Checked == false && RBK.Checked == false && RBJ.Checked == false)
            {
                DisplayAJAXMessage(this, "Plant tujuan harus dipilih !!"); return;
            }

            UPDdomain upd = new UPDdomain();
            UPDfacade fupd = new UPDfacade();
            string queryLink = string.Empty; string queryLokasi = string.Empty; string queryLink_Jmbg = string.Empty;
            string nodokumen = txtNo.Text.Trim(); string namadokumen = txtNama.Text.Trim();

            if (RBC.Checked == true)
            {
                queryLokasi = "Citeureup";
                queryLink = " select * from [sqlctrp.grcboard.com].bpasctrp.dbo.ISO_UPDTemp where RowStatus>-1 and NamaDokumen='" + namadokumen + "'";
            }

            if (RBK.Checked == true)
            {
                queryLokasi = "Karawang";
                queryLink = " select * from [sqlkrwg.grcboard.com].bpaskrwg.dbo.ISO_UPDTemp where RowStatus>-1 and NamaDokumen='" + namadokumen + "'";
            }

            if (RBJ.Checked == true)
            {
                queryLokasi = "Jombang";
                queryLink = " select * from [sqljombang.grcboard.com].bpasjombang.dbo.ISO_UPDTemp where RowStatus>-1 and NamaDokumen='" + namadokumen + "'";
            }

            upd = fupd.Retrieve(queryLink);

            if (upd.ID > 0)
            {
                Upload1.Enabled = true; btnUpload.Enabled = true; tr1.Visible = false; tr2.Visible = true; tr02.Visible = true; tr01.Visible = false;
                LabelNama.Visible = true; LabelNama.Text = " Dokumen ditemukan di Plant " + queryLokasi;
                LabelNama.Attributes.Add("style", "font-family: Calibri; font-size: small; font-weight: 700; color:green;");
            }
            else
            {
                Upload1.Enabled = false; btnUpload.Enabled = false; tr1.Visible = false; tr2.Visible = true; tr02.Visible = true; tr01.Visible = false;
                LabelNama.Visible = true; LabelNama.Text = " Dokumen tidak ditemukan di Plant " + queryLokasi;
                LabelNama.Attributes.Add("style", "font-family: Calibri; font-size: small; font-weight: 700; color:red;");
            }
        }

        protected void RBC_CheckedChanged(object sender, EventArgs e)
        {
            if (RBC.Checked == true)
            {
                RBK.Checked = false; RBJ.Checked = false;

                if (RBBiasa.Checked == true)
                {
                    txtNo.Enabled = true; btnCek.Enabled = true; tr1.Visible = true; tr2.Visible = false;
                }
                else
                {
                    txtNama.Enabled = true; btnCek2.Enabled = true; tr1.Visible = false; tr2.Visible = true;
                }
            }
        }

        protected void RBK_CheckedChanged(object sender, EventArgs e)
        {
            if (RBK.Checked == true)
            {
                RBC.Checked = false; RBJ.Checked = false;
            }

            if (RBBiasa.Checked == true)
            {
                txtNo.Enabled = true; btnCek.Enabled = true; tr1.Visible = true; tr2.Visible = false;
            }
            else
            {
                txtNama.Enabled = true; btnCek2.Enabled = true; tr1.Visible = false; tr2.Visible = true;
            }
        }

        protected void RBJ_CheckedChanged(object sender, EventArgs e)
        {
            if (RBJ.Checked == true)
            {
                RBC.Checked = false; RBK.Checked = false;
            }

            if (RBBiasa.Checked == true)
            {
                txtNo.Enabled = true; btnCek.Enabled = true; tr1.Visible = true; tr2.Visible = false;
            }
            else
            {
                txtNama.Enabled = true; btnCek2.Enabled = true; tr1.Visible = false; tr2.Visible = true;
            }
        }

        protected void RBBiasa_CheckedChanged(object sender, EventArgs e)
        {
            if (RBBiasa.Checked == true)
            {
                RBKhusus.Checked = false; tr1.Visible = true; tr2.Visible = false;
            }
        }

        protected void RBKhusus_CheckedChanged(object sender, EventArgs e)
        {
            if (RBKhusus.Checked == true)
            {
                RBBiasa.Checked = false; tr1.Visible = false; tr2.Visible = true;
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (Upload1.HasFile)
            {
                string NamaDokumen = txtNama.Text.Trim();
                string NomorDokumen = txtNo.Text.Trim();

                Users users = (Users)Session["Users"];
                string FilePath = Upload1.PostedFile.FileName;
                string filename = Path.GetFileName(FilePath);
                string ext = Path.GetExtension(filename);
                int ExtractPos = FilePath.LastIndexOf("\\") + 1;
                String UploadedFileName = FilePath.Substring(ExtractPos, FilePath.Length - ExtractPos);
                //Upload1.PostedFile.SaveAs(Path.Combine(@"D:\UPD_PDF\", filename));
                Session["File"] = filename;

                if (ext.ToLower() == ".pdf")
                {
                    Stream fs = Upload1.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);

                    if (RBBiasa.Checked == true)
                    {
                        Session["NoDocument"] = NomorDokumen; Session["DocName"] = "-";
                    }
                    else if (RBKhusus.Checked == true)
                    {
                        Session["DocName"] = NamaDokumen; Session["NoDocument"] = "-";
                    }

                    Session["FileName"] = filename.ToString();


                    try
                    {
                        ShareUPD();
                    }
                    catch
                    {

                    }

                    CloseWindow(this);
                }
            }
        }

        protected void ShareUPD()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("NoDocument", typeof(string));
            dt.Columns.Add("DocName", typeof(string));
            dt.Columns.Add("FileName", typeof(string));

            DataRow row = dt.NewRow();
            row["NoDocument"] = Session["NoDocument"].ToString().Trim();
            row["DocName"] = Session["DocName"].ToString().Trim();
            row["FileName"] = Session["FileName"].ToString().Trim();

            dt.Rows.Add(row);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            if (RBC.Checked == true)
            {
                try
                {
                    WebReference_Ctrp.Service1 bpas2 = new WebReference_Ctrp.Service1();
                    if (RBBiasa.Checked == true)
                    {
                        //string intResult = bpas2.InsertShareDoc1(dt);
                    }
                    else if (RBKhusus.Checked == true)
                    {
                        //string intResult = bpas2.InsertShareDoc2(dt);
                    }
                }
                catch
                {
                    DisplayAJAXMessage(this, "Koneksi Database ke Citeureup ada masalah");
                }

            }
            else if (RBK.Checked == true)
            {
                try
                {
                    WebReference_Krwg.Service1 bpas3 = new WebReference_Krwg.Service1();
                    //string intResult1 = bpas3.InsertShareUPD(dt);
                    if (RBBiasa.Checked == true)
                    {
                        //string intResult = bpas3.InsertShareDoc1(dt);
                    }
                    else if (RBKhusus.Checked == true)
                    {
                        //string intResult = bpas3.InsertShareDoc2(dt);
                    }
                }
                catch
                {
                    DisplayAJAXMessage(this, "Koneksi Database ke Karawang ada masalah");
                }
            }
            else if (RBJ.Checked == true)
            {
                try
                {
                    WebReference_Jmb.Service1 bpas4 = new WebReference_Jmb.Service1();
                    //string intResult1 = bpas3.InsertShareUPD(dt);
                    if (RBBiasa.Checked == true)
                    {
                        //string intResult = bpas4.InsertShareDoc1(dt);
                    }
                    else if (RBKhusus.Checked == true)
                    {
                        //string intResult = bpas4.InsertShareDoc2(dt);
                    }
                }
                catch
                {
                    DisplayAJAXMessage(this, "Koneksi Database ke Jombang ada masalah");
                }
            }
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        //protected void btnclose_ServerClick(object sender, EventArgs e)
        //{
        //    Response.Write("<script language='javascript'>window.close();</script>");        
        //}   

        protected void MyButton_Click(object sender, EventArgs e)
        {
            //Response.Write("<script language='javascript'>window.close();</script>");        
        }

        static public void CloseWindow(Control page)
        {
            string myScript = "window.close();";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "MyScript", myScript, true);
        }


    }


    public class UPDfacade
    {
        public string strError = string.Empty;
        private ArrayList arrData = new ArrayList();
        private UPDdomain dm = new UPDdomain();
        private List<SqlParameter> sqlListParam;

        public UPDdomain Retrieve(string Query)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = Query;

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return RetrieveData(sqlDataReader);
                }
            }

            return new UPDdomain();
        }

        public UPDdomain RetrieveData(SqlDataReader sqlDataReader)
        {
            dm = new UPDdomain();
            dm.ID = Convert.ToInt32(sqlDataReader["ID"]);
            return dm;
        }
    }
}

public class UPDdomain
{
    public string PeriodeCostControl { get; set; }
    public string NamaPlant { get; set; }
    public string JenisKendaraan { get; set; }
    public int Rowstatus { get; set; }
    public string CreatedBy { get; set; }
    public DateTime Createdtime { get; set; }
    public int ID { get; set; }
}