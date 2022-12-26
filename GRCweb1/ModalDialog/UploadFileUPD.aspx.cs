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
    public partial class UploadFileUPD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                btnUpload.Enabled = true;

                Users users = (Users)Session["Users"];
                string IDmaster = (Request.QueryString["ba"] != null) ? Request.QueryString["ba"].ToString() : "";
                string Kategory = (Request.QueryString["ba"] != null) ? Request.QueryString["k"].ToString() : "";
                string Dept2 = (Request.QueryString["ba"] != null) ? Request.QueryString["c"].ToString() : "";
                string Type = (Request.QueryString["ba"] != null) ? Request.QueryString["t"] : "";
                Session["IDmaster"] = IDmaster;
                LoadDept();
                LoadCekDataShare();
                if (Type == "2")
                {
                    LoadDistribusi001();
                }
                else
                {
                    LoadDistribusi001();
                }
                if (users.UnitKerjaID == 13)
                {
                    Chk01.Checked = false;
                }
                else
                {
                    Chk01.Checked = false; Chk01.Enabled = false;
                }
            }
        }

        public static class NetworkShare
        {
            /// <summary>
            /// Connects to the remote share
            /// </summary>
            /// <returns>Null if successful, otherwise error message.</returns>
            public static string ConnectToShare(string uri, string username, string password)
            {
                //Create netresource and point it at the share
                NETRESOURCE nr = new NETRESOURCE();
                nr.dwType = RESOURCETYPE_DISK;
                nr.lpRemoteName = uri;

                //Create the share
                int ret = WNetUseConnection(IntPtr.Zero, nr, password, username, 0, null, null, null);

                //Check for errors
                if (ret == NO_ERROR)
                    return null;
                else
                    return GetError(ret);
            }

            /// <summary>
            /// Remove the share from cache.
            /// </summary>
            /// <returns>Null if successful, otherwise error message.</returns>
            public static string DisconnectFromShare(string uri, bool force)
            {
                //remove the share
                int ret = WNetCancelConnection(uri, force);

                //Check for errors
                if (ret == NO_ERROR)
                    return null;
                else
                    return GetError(ret);
            }

            #region P/Invoke Stuff
            [DllImport("Mpr.dll")]
            private static extern int WNetUseConnection(
                IntPtr hwndOwner,
                NETRESOURCE lpNetResource,
                string lpPassword,
                string lpUserID,
                int dwFlags,
                string lpAccessName,
                string lpBufferSize,
                string lpResult
                );

            [DllImport("Mpr.dll")]
            private static extern int WNetCancelConnection(
                string lpName,
                bool fForce
                );

            [StructLayout(LayoutKind.Sequential)]
            private class NETRESOURCE
            {
                public int dwScope = 0;
                public int dwType = 0;
                public int dwDisplayType = 0;
                public int dwUsage = 0;
                public string lpLocalName = "";
                public string lpRemoteName = "";
                public string lpComment = "";
                public string lpProvider = "";
            }

            #region Consts
            const int RESOURCETYPE_DISK = 0x00000001;
            const int CONNECT_UPDATE_PROFILE = 0x00000001;
            #endregion

            #region Errors
            const int NO_ERROR = 0;

            const int ERROR_ACCESS_DENIED = 5;
            const int ERROR_ALREADY_ASSIGNED = 85;
            const int ERROR_BAD_DEVICE = 1200;
            const int ERROR_BAD_NET_NAME = 67;
            const int ERROR_BAD_PROVIDER = 1204;
            const int ERROR_CANCELLED = 1223;
            const int ERROR_EXTENDED_ERROR = 1208;
            const int ERROR_INVALID_ADDRESS = 487;
            const int ERROR_INVALID_PARAMETER = 87;
            const int ERROR_INVALID_PASSWORD = 1216;
            const int ERROR_MORE_DATA = 234;
            const int ERROR_NO_MORE_ITEMS = 259;
            const int ERROR_NO_NET_OR_BAD_PATH = 1203;
            const int ERROR_NO_NETWORK = 1222;
            const int ERROR_SESSION_CREDENTIAL_CONFLICT = 1219;

            const int ERROR_BAD_PROFILE = 1206;
            const int ERROR_CANNOT_OPEN_PROFILE = 1205;
            const int ERROR_DEVICE_IN_USE = 2404;
            const int ERROR_NOT_CONNECTED = 2250;
            const int ERROR_OPEN_FILES = 2401;

            private struct ErrorClass
            {
                public int num;
                public string message;
                public ErrorClass(int num, string message)
                {
                    this.num = num;
                    this.message = message;
                }
            }

            private static ErrorClass[] ERROR_LIST = new ErrorClass[] {
            new ErrorClass(ERROR_ACCESS_DENIED, "Error: Access Denied"),
            new ErrorClass(ERROR_ALREADY_ASSIGNED, "Error: Already Assigned"),
            new ErrorClass(ERROR_BAD_DEVICE, "Error: Bad Device"),
            new ErrorClass(ERROR_BAD_NET_NAME, "Error: Bad Net Name"),
            new ErrorClass(ERROR_BAD_PROVIDER, "Error: Bad Provider"),
            new ErrorClass(ERROR_CANCELLED, "Error: Cancelled"),
            new ErrorClass(ERROR_EXTENDED_ERROR, "Error: Extended Error"),
            new ErrorClass(ERROR_INVALID_ADDRESS, "Error: Invalid Address"),
            new ErrorClass(ERROR_INVALID_PARAMETER, "Error: Invalid Parameter"),
            new ErrorClass(ERROR_INVALID_PASSWORD, "Error: Invalid Password"),
            new ErrorClass(ERROR_MORE_DATA, "Error: More Data"),
            new ErrorClass(ERROR_NO_MORE_ITEMS, "Error: No More Items"),
            new ErrorClass(ERROR_NO_NET_OR_BAD_PATH, "Error: No Net Or Bad Path"),
            new ErrorClass(ERROR_NO_NETWORK, "Error: No Network"),
            new ErrorClass(ERROR_BAD_PROFILE, "Error: Bad Profile"),
            new ErrorClass(ERROR_CANNOT_OPEN_PROFILE, "Error: Cannot Open Profile"),
            new ErrorClass(ERROR_DEVICE_IN_USE, "Error: Device In Use"),
            new ErrorClass(ERROR_EXTENDED_ERROR, "Error: Extended Error"),
            new ErrorClass(ERROR_NOT_CONNECTED, "Error: Not Connected"),
            new ErrorClass(ERROR_OPEN_FILES, "Error: Open Files"),
            new ErrorClass(ERROR_SESSION_CREDENTIAL_CONFLICT, "Error: Credential Conflict"),
    };

            private static string GetError(int errNum)
            {
                foreach (ErrorClass er in ERROR_LIST)
                {
                    if (er.num == errNum) return er.message;
                }
                return "Error: Unknown, " + errNum;
            }
            #endregion

            #endregion
        }

        private void LoadDistribusi01()
        {
            ISO_UPD2Facade facadeDist = new ISO_UPD2Facade();
            ArrayList arrData = facadeDist.RetrieveDataAwal(Session["IDmaster"].ToString());

            if (arrData.Count > 0)
            {
                LoadDistribusi02(arrData);
            }

        }

        private void LoadDistribusi001()
        {
            ISO_UPD2Facade facadeDist = new ISO_UPD2Facade();
            ArrayList arrData = facadeDist.RetrieveDataAwal0(Session["IDmaster"].ToString());

            if (arrData.Count > 0)
            {
                LoadDistribusi002(arrData);
            }

        }

        private void LoadDistribusi02(ArrayList arrData)
        {
            ArrayList arrListData = (ArrayList)Session["ListOfDept"];

            foreach (ISO_UpdDMD List in arrData)
            {
                int i = 0;
                foreach (ISO_UpdDMD List2 in arrListData)
                {
                    CheckBox cb = (CheckBox)GridView1.Rows[i].FindControl("check");
                    if (List2.ID == Convert.ToInt32(List.DeptID))
                    {
                        cb.Checked = true; cb.Enabled = false;
                        break;
                    }

                    i = i + 1;
                }
            }
        }

        private void LoadDistribusi002(ArrayList arrData)
        {
            ArrayList arrListData = (ArrayList)Session["ListOfDept"];

            foreach (ISO_UpdDMD List in arrData)
            {
                int i = 0;
                foreach (ISO_UpdDMD List2 in arrListData)
                {
                    CheckBox cb = (CheckBox)GridView1.Rows[i].FindControl("check");
                    if (List2.ID == Convert.ToInt32(List.DeptID))
                    {
                        cb.Checked = true; cb.Enabled = false;
                        break;
                    }

                    i = i + 1;
                }
            }
        }

        protected void LoadCekDataShare()
        {
            ISO_UPD2Facade FacadeShare = new ISO_UPD2Facade();
            ISO_UpdDMD DomainShare = new ISO_UpdDMD();
            int IDmaster = Convert.ToInt32(Session["IDmaster"]);
            DomainShare = FacadeShare.CekData(IDmaster);

            Session["PlantID"] = DomainShare.PlantID;
            Session["StatusShare"] = DomainShare.StatusShare;
            Session["Aktif"] = DomainShare.Aktif;
            Session["Type"] = DomainShare.Type;
            Session["TglBerlaku"] = DomainShare.TglBerlaku;
        }

        private void LoadDept()
        {
            ISO_UPD2Facade updF = new ISO_UPD2Facade();
            ArrayList arrUPD = updF.RetrieveDept();
            if (updF.Error == string.Empty)
            {
                Session["ListOfDept"] = arrUPD;
                GridView1.DataSource = arrUPD;
                GridView1.DataBind();
            }
        }

        protected void ChkAll_CheckedChanged(object sender, EventArgs e)
        {
            ArrayList arrUPD = (ArrayList)Session["ListOfDept"];
            int i = 0;
            if (ChkAll.Checked == true)
            {
                foreach (ISO_UpdDMD updDMD in arrUPD)
                {
                    CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("check");
                    chk.Checked = true;
                    i = i + 1;
                }

            }
            else
            {
                foreach (ISO_UpdDMD updDMD in arrUPD)
                {
                    CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("check");
                    chk.Checked = false;
                    i = i + 1;
                }
            }

        }

        protected void Chk01_CheckedChanged(object sender, EventArgs e)
        {
            //if (Chk01.Checked == true)
            //{
            //    Chk01.Checked = true;
            //}
            //else if (Chk01.Checked == false)
            //{
            //    Chk01.Checked = false;
            //}
        }

        private void LoadDeptList()
        {
            ArrayList arrUPD = (ArrayList)Session["ListOfDept"];

            int i = 0;

            foreach (ISO_UpdDMD DeptList in arrUPD)
            {
                CheckBox cb = (CheckBox)GridView1.Rows[i].FindControl("check");
                if (DeptList.ID > 0)
                {
                    cb.Checked = true;
                    break;
                }

                i = i + 1;
            }
        }

        /** Upload yang lama **/
        protected void btnUpload0_Click(object sender, EventArgs e)
        {
            if (Upload1.HasFile)
            {
                btnUpload.Enabled = false;

                int Aktif = Convert.ToInt32(Session["Aktif"]);
                int PlantID = Convert.ToInt32(Session["PlantID"]);
                int StatusShare = Convert.ToInt32(Session["StatusShare"]);
                //int Tipe = Convert.ToInt32(Session["Type"]);
                string Tipe = Session["Type"].ToString();

                Users users = (Users)Session["Users"];
                string FilePath = Upload1.PostedFile.FileName;
                string filename = Path.GetFileName(FilePath);
                string ext = Path.GetExtension(filename);
                int ExtractPos = FilePath.LastIndexOf("\\") + 1;
                String UploadedFileName = FilePath.Substring(ExtractPos, FilePath.Length - ExtractPos);
                Upload1.PostedFile.SaveAs(Path.Combine(@"D:\UPD_PDF\", filename));
                Session["File"] = filename;

                if (Aktif == 1 && StatusShare == 11)
                {
                    ISO_UPD2Facade UPDFacadeShare = new ISO_UPD2Facade();
                    ISO_UpdDMD UPDShare = new ISO_UpdDMD();
                    UPDShare.NamaFile = filename.Trim();
                    UPDShare.Unit = users.UnitKerjaID.ToString();

                    //int intResult = 0;
                    //intResult = UPDFacadeShare.KirimFileOtherPlant(UPDShare);

                    if (users.UnitKerjaID == 13)
                    {
                        /** Koneksi ke Server IIS Citeureup **/
                        NetworkShare.ConnectToShare(@"\\123.123.123.129\UPD_PDF_Temp", "administrator", "superflat1");
                        /** Koneksi ke Server IIS Karawang **/
                        NetworkShare.ConnectToShare(@"\\192.168.222.21\UPD_PDF_Temp", "sodikin", "Sayatea1");

                        if (System.IO.File.Exists(@"\\123.123.123.129\UPD_PDF_Temp\\" + filename.Trim()))
                        {
                            //System.IO.File.Replace("D:\\UPD_PDF\\" + filename.Trim(), @"\\123.123.123.129\UPD_PDF_Temp\\" + filename.Trim());                       
                        }
                        else
                        {
                            System.IO.File.Copy("D:\\UPD_PDF\\" + filename.Trim(), @"\\123.123.123.129\UPD_PDF_Temp\\" + filename.Trim() + "");
                        }

                        if (System.IO.File.Exists(@"\\192.168.222.21\UPD_PDF_Temp\\" + filename.Trim()))
                        {
                            //System.IO.File.Replace("D:\\UPD_PDF\\" + filename.Trim(), @"\\192.168.222.21\UPD_PDF_Temp\\" + filename.Trim() + "",null);
                        }
                        else
                        {
                            System.IO.File.Copy("D:\\UPD_PDF\\" + filename.Trim(), @"\\192.168.222.21\UPD_PDF_Temp\\" + filename.Trim() + "");
                        }
                    }

                    if (users.UnitKerjaID == 1)
                    {
                        /** Koneksi ke Server IIS Jombang **/
                        NetworkShare.ConnectToShare(@"\\192.168.252.2\UPD_PDF_Temp", "sodikin", "Sayatea1");
                        /** Koneksi ke Server IIS Karawang **/
                        NetworkShare.ConnectToShare(@"\\192.168.222.21\UPD_PDF_Temp", "sodikin", "Sayatea1");

                        if (System.IO.File.Exists(@"\\192.168.252.2\UPD_PDF_Temp\\" + filename.Trim()))
                        {
                            //System.IO.File.Replace("D:\\UPD_PDF\\" + filename.Trim(), @"\\192.168.252.2\UPD_PDF_Temp\\" + filename.Trim() + "", "");
                        }
                        else
                        {
                            System.IO.File.Copy("D:\\UPD_PDF\\" + filename.Trim(), @"\\192.168.252.2\UPD_PDF_Temp\\" + filename.Trim() + "");
                        }

                        if (System.IO.File.Exists(@"\\192.168.222.21\UPD_PDF_Temp\\" + filename.Trim()))
                        {
                            //System.IO.File.Replace("D:\\UPD_PDF\\" + filename.Trim(), @"\\192.168.222.21\UPD_PDF_Temp\\" + filename.Trim() + "", "");
                        }
                        else
                        {
                            System.IO.File.Copy("D:\\UPD_PDF\\" + filename.Trim(), @"\\192.168.222.21\UPD_PDF_Temp\\" + filename.Trim() + "");
                        }
                    }

                    if (users.UnitKerjaID == 7)
                    {
                        /** Koneksi ke Server IIS Citeureup **/
                        NetworkShare.ConnectToShare(@"\\123.123.123.129\UPD_PDF_Temp", "administrator", "superflat1");
                        /** Koneksi ke Server IIS Jombang **/
                        NetworkShare.ConnectToShare(@"\\192.168.252.2\UPD_PDF_Temp", "sodikin", "Sayatea1");

                        if (System.IO.File.Exists(@"\\123.123.123.129\UPD_PDF_Temp\\" + filename.Trim()))
                        {
                            //System.IO.File.Replace("D:\\UPD_PDF\\" + filename.Trim(), @"\\123.123.123.129\UPD_PDF_Temp\\" + filename.Trim() + "", "");
                        }
                        else
                        {
                            System.IO.File.Copy("D:\\UPD_PDF\\" + filename.Trim(), @"\\123.123.123.129\UPD_PDF_Temp\\" + filename.Trim() + "");
                        }

                        if (System.IO.File.Exists(@"\\192.168.252.2\UPD_PDF_Temp\\" + filename.Trim()))
                        {
                            //System.IO.File.Replace("D:\\UPD_PDF\\" + filename.Trim(), @"\\192.168.252.2\UPD_PDF_Temp\\" + filename.Trim() + "", "");
                        }
                        else
                        {
                            System.IO.File.Copy("D:\\UPD_PDF\\" + filename.Trim(), @"\\192.168.252.2\UPD_PDF_Temp\\" + filename.Trim() + "");
                        }
                    }
                }

                if (ext.ToLower() == ".pdf")
                {
                    Stream fs = Upload1.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    ISO_UPD2Facade updF = new ISO_UPD2Facade();
                    ISO_UpdDMD files = new ISO_UpdDMD();
                    ArrayList arrUPD = (ArrayList)Session["ListOfDept"];
                    int IDDokumen = int.Parse(Request.QueryString["ba"].ToString());
                    files.IDmaster = int.Parse(Request.QueryString["ba"].ToString());
                    files.Kategory = int.Parse(Request.QueryString["k"].ToString());
                    files.Dept2 = int.Parse(Request.QueryString["c"].ToString());
                    files.FileName = filename.ToString();
                    files.CreatedBy = ((Users)Session["Users"]).UserName;

                    int intResult = 0;
                    // Perlu 1
                    intResult = updF.InsertDisFile(files);

                    ArrayList arrDept = (ArrayList)Session["ListOfDept"];
                    int i = 0;
                    foreach (ISO_UpdDMD DeptList in arrDept)
                    {
                        CheckBox cb = (CheckBox)GridView1.Rows[i].Cells[1].FindControl("check");

                        //if (DeptList.ID == int.Parse(Request.QueryString["c"].ToString()))
                        //{
                        //    cb.Checked = true;
                        //    break;
                        //}

                        if (cb.Checked)
                        {
                            int DeptIDF = (int.Parse(GridView1.Rows[i].Cells[0].Text)) == 19 ? 4 : int.Parse(GridView1.Rows[i].Cells[0].Text);

                            //files.ID = int.Parse(GridView1.Rows[i].Cells[0].Text);
                            files.ID = DeptIDF;
                            files.IDmaster = int.Parse(Request.QueryString["ba"].ToString());
                            files.Kategory = int.Parse(Request.QueryString["k"].ToString());
                            files.Dept2 = int.Parse(Request.QueryString["c"].ToString());
                            files.LastModifiedBy = ((Users)Session["Users"]).UserName;
                            files.FileName = filename.ToString();
                            files.CreatedBy = ((Users)Session["Users"]).UserName;
                            files.Type = Request.QueryString["k"];
                            files.LinkID = int.Parse(Request.QueryString["ba"].ToString());
                            if (files.Type == "0")
                                files.Type = "1";
                            files.Type = Request.QueryString["t"];
                            if (files.Type == "2")
                                files.Urutan = "2";
                            if (files.Type == "1")
                                files.Urutan = "1";

                            if (PlantID != users.UnitKerjaID && StatusShare == 1)
                            {
                                intResult = updF.InsertD(files);
                                intResult = updF.UpdateMasterStatusShare(files);

                            }
                            else
                            {
                                intResult = updF.InsertD(files);
                                intResult = updF.UpdateMasterStatus(files);
                            }
                            //Perlu
                            //intResult = updF.InsertD(files);
                            //intResult = updF.UpdateMasterStatus(files);
                            //intResult = updF.InsertDisFile(files);  
                            if (updF.Error != string.Empty)
                            {
                                break;
                            }
                        }
                        i = i + 1;
                    }

                    //int IDmaster2 = int.Parse(Request.QueryString["ba"].ToString());
                    string Share = string.Empty;

                    ISO_UpdD4 upd5 = new ISO_UpdD4();
                    ISO_UPDFacade4 updf5 = new ISO_UPDFacade4();
                    upd5 = updf5.cekUPDid(IDDokumen);

                    ISO_UpdD4 upd4 = new ISO_UpdD4();
                    ISO_UPDFacade4 updf4 = new ISO_UPDFacade4();
                    int result = 0;

                    if (upd5.StatusShare == 11)
                    {
                        //ISO_UpdD4 upd4 = new ISO_UpdD4();
                        //ISO_UPDFacade4 updf4 = new ISO_UPDFacade4();
                        //int result = 0;

                        //upd4.IPAddress = GetIPAddress();
                        //upd4.UserID = users.ID.ToString();

                        if (Request.QueryString["t"] == "1")
                        {
                            upd4.Urutan = 4;
                        }
                        else
                        {
                            upd4.Urutan = 5;
                        }

                        Share = "-";
                        //upd4.IdUPD = upd5.IdUPD;
                        //result = updf4.InsertLogApv(upd4);
                    }
                    else if (upd5.StatusShare == 0)
                    {
                        upd4.Urutan = 3; Share = "Share";
                    }

                    //ISO_UpdD4 upd4 = new ISO_UpdD4();
                    //ISO_UPDFacade4 updf4 = new ISO_UPDFacade4();
                    //int result = 0;

                    upd4.IPAddress = GetIPAddress();
                    upd4.UserID = users.ID.ToString();
                    upd4.DocShare = Share;

                    upd4.IdUPD = upd5.IdUPD;

                    result = updf4.InsertLogApv(upd4);


                    int IDmaster = int.Parse(Request.QueryString["ba"].ToString());
                    string Query1 = string.Empty;
                    ISO_UPD2Facade updF11 = new ISO_UPD2Facade();
                    ISO_UpdDMD files11 = new ISO_UpdDMD();
                    int SShare = updF11.RetrieveSShare(IDmaster);
                    if (SShare != 11)
                    {
                        Query1 = " (select upd.JenisUPD from ISO_UPDTemp upd where upd.NoDokumen=NoDocument and upd.RowStatus>-1) ";

                    }
                    else
                    {
                        Query1 = " (select jenisupd from iso_upd upd where upd.ID=idupd and rowstatus>-1) ";
                    }

                    ISO_UPD2Facade updF1 = new ISO_UPD2Facade();
                    ISO_UpdDMD files1 = new ISO_UpdDMD();
                    files1 = updF1.AmbilData(int.Parse(Request.QueryString["ba"].ToString()), Query1);

                    Session["NoDocument"] = files1.NoDocument.Trim(); //1
                    Session["DocName"] = files1.DocName.Trim(); //2
                    Session["RevisiNo"] = files1.RevisiNo.Trim(); //3
                    Session["CreatedBy"] = files1.CreatedBy.Trim(); //4
                    Session["CategoryUPD"] = files1.CategoryUPD.Trim(); //5
                    Session["Type"] = files1.Type.Trim(); //6
                    Session["DeptID"] = files1.DeptID.Trim(); //7               
                    Session["FileName"] = filename.ToString().Trim(); //8
                    Session["IDmaster"] = IDmaster; //9
                    Session["JenisUPD"] = files1.JenisUPD; //10              
                    Session["PlantID"] = files1.PlantID; //11
                    Session["Alasan"] = files1.Alasan; //12
                    Session["TglShare"] = files1.TglShare; //12

                    if (files1.StatusShare == 11 && files1.Aktif == "2")
                    {
                        ISO_UPD2Facade updF4 = new ISO_UPD2Facade();
                        ISO_UpdDMD files4 = new ISO_UpdDMD();
                        int Results = 0;
                        files4.ID = files1.ID;
                        files4.LastModifiedBy = users.UserName;

                        Results = updF4.UpdateDataUPD(files4);

                        try
                        {
                            if (Chk01.Checked == false)
                            {
                                ShareUPD(); // WebService
                            }
                        }
                        catch { }

                        if (Chk01.Checked == false)
                        {
                            ISO_UPD2Facade updF3 = new ISO_UPD2Facade();
                            ISO_UpdDMD files3 = new ISO_UpdDMD();

                            int Result = 0;
                            files3.ID = int.Parse(Request.QueryString["ba"].ToString());
                            files3.LastModifiedBy = users.UserName;
                            Result = updF3.UpdateDataShare(files3);
                        }
                    }

                    CloseWindow(this);
                }
            }
        }

        /** Upload yang Baru **/
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (Upload1.HasFile)
            {
                btnUpload.Enabled = false;

                int Aktif = Convert.ToInt32(Session["Aktif"]);
                int PlantID = Convert.ToInt32(Session["PlantID"]);
                int StatusShare = Convert.ToInt32(Session["StatusShare"]);               
                string Tipe = Session["Type"].ToString();

                Users users = (Users)Session["Users"];
                string FilePath = Upload1.PostedFile.FileName;
                string filename = Path.GetFileName(FilePath);
                string ext = Path.GetExtension(filename);
                int ExtractPos = FilePath.LastIndexOf("\\") + 1;
                String UploadedFileName = FilePath.Substring(ExtractPos, FilePath.Length - ExtractPos);
                Upload1.PostedFile.SaveAs(Path.Combine(@"D:\UPD_PDF\", filename));
                Session["File"] = filename;

                if (Aktif == 1 && StatusShare == 11)
                {
                    ISO_UPD2Facade UPDFacadeShare = new ISO_UPD2Facade();
                    ISO_UpdDMD UPDShare = new ISO_UpdDMD();
                    UPDShare.NamaFile = filename.Trim();
                    UPDShare.Unit = users.UnitKerjaID.ToString();
                   
                    /** New Beny **/
                    string ID = Session["IDmaster"].ToString();
                    string Folder = string.Empty;
                    int DeptID = 0; string NamaFile = string.Empty;

                    /** Retrieve Folder Department **/
                    ZetroView zl = new ZetroView();
                    zl.QueryType = Operation.CUSTOM;
                    zl.CustomQuery =
                    " select dept DeptID from ISO_UpdDMD where ID=" + ID + " ";
                    SqlDataReader sdr = zl.Retrieve();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            DeptID = Convert.ToInt32(sdr["DeptID"].ToString());                           
                        }
                    }

                    if (DeptID == 4 || DeptID == 5 || DeptID == 18 || DeptID == 19) { Folder = "Maintenance"; }
                    else if (DeptID == 2) { Folder = "BoardMill"; }
                    else if (DeptID == 3) { Folder = "Finishing"; }
                    else if (DeptID == 7) { Folder = "HRD"; }
                    else if (DeptID == 9) { Folder = "QA"; }
                    else if (DeptID == 10) { Folder = "LogistikBB"; }
                    else if (DeptID == 6) { Folder = "LogistikBJ"; }
                    else if (DeptID == 11) { Folder = "PPIC"; }
                    else if (DeptID == 13) { Folder = "Marketing"; }
                    else if (DeptID == 14) { Folder = "IT"; }
                    else if (DeptID == 15) { Folder = "Purchasing"; }
                    else if (DeptID == 26) { Folder = "Transportation"; } else if (DeptID == 23) { Folder = "ISO"; }

                    /** Retrieve SoftCopy UPD **/
                    ZetroView zv = new ZetroView();
                    zv.QueryType = Operation.CUSTOM;
                    string UPDFile = string.Empty;
                    zv.CustomQuery =
                    " select NamaFile UPDFile from ISO_UPDLampiran where IDupd in (select idUPD from ISO_UpdDMD where ID=" + ID + ") and RowStatus>-1 ";
                    SqlDataReader dr = zv.Retrieve();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            UPDFile = dr["UPDFile"].ToString(); Session["UPDFile"] = UPDFile;
                        }
                    }

                    #region Jombang
                    if (users.UnitKerjaID == 13)
                    {
                        /** Koneksi ke Server IIS Citeureup **/
                        NetworkShare.ConnectToShare(@"\\192.168.150.149\UPD_PDF_Temp", "beny.christianto", "Grcboard123");
                        /** Koneksi ke Server IIS Karawang **/
                        NetworkShare.ConnectToShare(@"\\192.168.222.21\UPD_PDF_Temp", "sodikin", "Sayatea1");

                        if (System.IO.File.Exists(@"\\192.168.150.149\UPD_PDF_Temp\\" + filename.Trim()))
                        {
                            //System.IO.File.Replace("D:\\UPD_PDF\\" + filename.Trim(), @"\\123.123.123.129\UPD_PDF_Temp\\" + filename.Trim());                       
                        }
                        else
                        {
                            System.IO.File.Copy("D:\\UPD_PDF\\" + filename.Trim(), @"\\192.168.150.149\UPD_PDF_Temp\\" + filename.Trim() + "");
                            System.IO.File.Copy("D:\\data lampiran purchn\\" + Folder + "\\" + UPDFile.ToString().Trim(), @"\\192.168.150.149\UPD_PDF_Temp\\SoftCopy\\" + UPDFile.ToString().Trim() + "");
                        }

                        if (System.IO.File.Exists(@"\\192.168.222.21\UPD_PDF_Temp\\" + filename.Trim()))
                        {
                            //System.IO.File.Replace("D:\\UPD_PDF\\" + filename.Trim(), @"\\192.168.222.21\UPD_PDF_Temp\\" + filename.Trim() + "",null);
                        }
                        else
                        {
                            System.IO.File.Copy("D:\\UPD_PDF\\" + filename.Trim(), @"\\192.168.222.21\UPD_PDF_Temp\\" + filename.Trim() + "");
                            System.IO.File.Copy("D:\\data lampiran purchn\\" + Folder + "\\" + UPDFile.ToString().Trim(), @"\\192.168.222.21\UPD_PDF_Temp\\SoftCopy\\" + UPDFile.ToString().Trim() + "");
                        }
                    }
                    #endregion
                    #region Citeureup
                    if (users.UnitKerjaID == 1)
                    {
                        /** Koneksi ke Server IIS Jombang **/
                        NetworkShare.ConnectToShare(@"\\192.168.252.2\UPD_PDF_Temp", "sodikin", "Sayatea1");
                        /** Koneksi ke Server IIS Karawang **/
                        NetworkShare.ConnectToShare(@"\\192.168.222.21\UPD_PDF_Temp", "sodikin", "Sayatea1");

                        if (System.IO.File.Exists(@"\\192.168.252.2\UPD_PDF_Temp\\" + filename.Trim()))
                        {
                            //System.IO.File.Replace("D:\\UPD_PDF\\" + filename.Trim(), @"\\192.168.252.2\UPD_PDF_Temp\\" + filename.Trim() + ""+ filename.Trim()+ "");
                        }
                        else
                        {
                            System.IO.File.Copy("D:\\UPD_PDF\\" + filename.Trim(), @"\\192.168.252.2\UPD_PDF_Temp\\" + filename.Trim() + "");
                            System.IO.File.Copy("D:\\data lampiran purchn\\UPD\\" + Folder + "\\" + UPDFile.ToString().Trim(), @"\\192.168.252.2\UPD_PDF_Temp\\SoftCopy\\" + UPDFile.ToString().Trim() + "");
                        }

                        if (System.IO.File.Exists(@"\\192.168.222.21\UPD_PDF_Temp\\" + filename.Trim()))
                        {
                            //System.IO.File.Replace("D:\\UPD_PDF\\" + filename.Trim(), @"\\192.168.222.21\UPD_PDF_Temp\\" + filename.Trim() + "" + filename.Trim() + "");
                        }
                        else
                        {
                            System.IO.File.Copy("D:\\UPD_PDF\\" + filename.Trim(), @"\\192.168.222.21\UPD_PDF_Temp\\" + filename.Trim() + "");
                            System.IO.File.Copy("D:\\data lampiran purchn\\UPD\\" + Folder + "\\" + UPDFile.ToString().Trim(), @"\\192.168.222.21\UPD_PDF_Temp\\SoftCopy\\" + UPDFile.ToString().Trim() + "");
                        }
                    }
                    #endregion
                    #region Karawang
                    if (users.UnitKerjaID == 7)
                    {
                        /** Koneksi ke Server IIS Citeureup **/
                        NetworkShare.ConnectToShare(@"\\192.168.150.149\UPD_PDF_Temp", "beny.christianto", "Grcboard123");
                        /** Koneksi ke Server IIS Jombang **/
                        NetworkShare.ConnectToShare(@"\\192.168.252.2\UPD_PDF_Temp", "sodikin", "Sayatea1");

                        if (System.IO.File.Exists(@"\\192.168.150.149\UPD_PDF_Temp\\" + filename.Trim()))
                        {
                            //System.IO.File.Replace("D:\\UPD_PDF\\" + filename.Trim(), @"\\123.123.123.129\UPD_PDF_Temp\\" + filename.Trim() + "", "");
                        }
                        else
                        {
                            System.IO.File.Copy("D:\\UPD_PDF\\" + filename.Trim(), @"\\192.168.150.149\UPD_PDF_Temp\\" + filename.Trim() + "");
                            System.IO.File.Copy("D:\\data lampiran purchn\\UPD\\" + Folder + "\\" + UPDFile.ToString().Trim(), @"\\192.168.150.149\UPD_PDF_Temp\\SoftCopy\\" + UPDFile.ToString().Trim() + "");
                        }

                        if (System.IO.File.Exists(@"\\192.168.252.2\UPD_PDF_Temp\\" + filename.Trim()))
                        {
                            //System.IO.File.Replace("D:\\UPD_PDF\\" + filename.Trim(), @"\\192.168.252.2\UPD_PDF_Temp\\" + filename.Trim() + "", "");
                        }
                        else
                        {
                            System.IO.File.Copy("D:\\UPD_PDF\\" + filename.Trim(), @"\\192.168.252.2\UPD_PDF_Temp\\" + filename.Trim() + "");
                            System.IO.File.Copy("D:\\data lampiran purchn\\UPD\\" + Folder + "\\" + UPDFile.ToString().Trim(), @"\\192.168.252.2\UPD_PDF_Temp\\SoftCopy\\" + UPDFile.ToString().Trim() + "");
                        }
                    }
                    #endregion

                }

                if (ext.ToLower() == ".pdf")
                {
                    Stream fs = Upload1.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    ISO_UPD2Facade updF = new ISO_UPD2Facade();
                    ISO_UpdDMD files = new ISO_UpdDMD();
                    ArrayList arrUPD = (ArrayList)Session["ListOfDept"];
                    int IDDokumen = int.Parse(Request.QueryString["ba"].ToString());
                    files.IDmaster = int.Parse(Request.QueryString["ba"].ToString());
                    files.Kategory = int.Parse(Request.QueryString["k"].ToString());
                    files.Dept2 = int.Parse(Request.QueryString["c"].ToString());
                    files.FileName = filename.ToString();
                    files.CreatedBy = ((Users)Session["Users"]).UserName;

                    int intResult = 0;

                    intResult = updF.InsertDisFile(files);

                    ArrayList arrDept = (ArrayList)Session["ListOfDept"];
                    int i = 0;
                    foreach (ISO_UpdDMD DeptList in arrDept)
                    {
                        CheckBox cb = (CheckBox)GridView1.Rows[i].Cells[1].FindControl("check");

                        if (cb.Checked)
                        {
                            int DeptIDF = (int.Parse(GridView1.Rows[i].Cells[0].Text)) == 19 ? 4 : int.Parse(GridView1.Rows[i].Cells[0].Text);

                            //files.ID = int.Parse(GridView1.Rows[i].Cells[0].Text);
                            files.ID = DeptIDF;
                            files.IDmaster = int.Parse(Request.QueryString["ba"].ToString());
                            files.Kategory = int.Parse(Request.QueryString["k"].ToString());
                            files.Dept2 = int.Parse(Request.QueryString["c"].ToString());
                            files.LastModifiedBy = ((Users)Session["Users"]).UserName;
                            files.FileName = filename.ToString();
                            files.CreatedBy = ((Users)Session["Users"]).UserName;
                            files.Type = Request.QueryString["k"];
                            files.LinkID = int.Parse(Request.QueryString["ba"].ToString());
                            if (files.Type == "0")
                                files.Type = "1";
                            files.Type = Request.QueryString["t"];
                            if (files.Type == "2")
                                files.Urutan = "2";
                            if (files.Type == "1")
                                files.Urutan = "1";

                            if (PlantID != users.UnitKerjaID && StatusShare == 1)
                            {
                                intResult = updF.InsertD(files);
                                intResult = updF.UpdateMasterStatusShare(files);

                            }
                            else
                            {
                                intResult = updF.InsertD(files);
                                intResult = updF.UpdateMasterStatus(files);
                            }
                           
                            if (updF.Error != string.Empty)
                            {
                                break;
                            }
                        }
                        i = i + 1;
                    }
                                     
                    string Share = string.Empty;

                    ISO_UpdD4 upd5 = new ISO_UpdD4();
                    ISO_UPDFacade4 updf5 = new ISO_UPDFacade4();
                    upd5 = updf5.cekUPDid(IDDokumen);

                    ISO_UpdD4 upd4 = new ISO_UpdD4();
                    ISO_UPDFacade4 updf4 = new ISO_UPDFacade4();
                    int result = 0;

                    if (upd5.StatusShare == 11)
                    {     
                        if (Request.QueryString["t"] == "1")
                        {
                            upd4.Urutan = 4;
                        }
                        else
                        {
                            upd4.Urutan = 5;
                        }

                        Share = "-";                       
                    }
                    else if (upd5.StatusShare == 0)
                    {
                        upd4.Urutan = 3; Share = "Share";
                    }
                   
                    upd4.IPAddress = GetIPAddress();
                    upd4.UserID = users.ID.ToString();
                    upd4.DocShare = Share;

                    upd4.IdUPD = upd5.IdUPD;

                    result = updf4.InsertLogApv(upd4);


                    int IDmaster = int.Parse(Request.QueryString["ba"].ToString());
                    string Query1 = string.Empty;
                    ISO_UPD2Facade updF11 = new ISO_UPD2Facade();
                    ISO_UpdDMD files11 = new ISO_UpdDMD();
                    int SShare = updF11.RetrieveSShare(IDmaster);
                    if (SShare != 11)
                    {
                        Query1 = " (select upd.JenisUPD from ISO_UPDTemp upd where upd.NoDokumen=NoDocument and upd.RowStatus>-1) ";
                    }
                    else
                    {
                        Query1 = " (select jenisupd from iso_upd upd where upd.ID=idupd and rowstatus>-1) ";
                    }

                    ISO_UPD2Facade updF1 = new ISO_UPD2Facade();
                    ISO_UpdDMD files1 = new ISO_UpdDMD();
                    files1 = updF1.AmbilData(int.Parse(Request.QueryString["ba"].ToString()), Query1);

                    Session["NoDocument"] = files1.NoDocument.Trim(); //1
                    Session["DocName"] = files1.DocName.Trim(); //2
                    Session["RevisiNo"] = files1.RevisiNo.Trim(); //3
                    Session["CreatedBy"] = files1.CreatedBy.Trim(); //4
                    Session["CategoryUPD"] = files1.CategoryUPD.Trim(); //5
                    Session["Type"] = files1.Type.Trim(); //6
                    Session["DeptID"] = files1.DeptID.Trim(); //7               
                    Session["FileName"] = filename.ToString().Trim(); //8
                    Session["IDmaster"] = IDmaster; //9
                    Session["JenisUPD"] = files1.JenisUPD; //10              
                    Session["PlantID"] = files1.PlantID; //11
                    Session["Alasan"] = files1.Alasan; //12
                    //Session["TglShare"] = files1.TglShare; //12

                    if (files1.StatusShare == 11 && files1.Aktif == "2")
                    {
                        ISO_UPD2Facade updF4 = new ISO_UPD2Facade();
                        ISO_UpdDMD files4 = new ISO_UpdDMD();
                        int Results = 0;
                        files4.ID = files1.ID;
                        files4.LastModifiedBy = users.UserName;

                        Results = updF4.UpdateDataUPD(files4);

                        try
                        {
                            if (Chk01.Checked == false)
                            {
                                /** WebService **/
                                ShareUPD();
                            }
                        }
                        catch { }

                        if (Chk01.Checked == false)
                        {
                            ISO_UPD2Facade updF3 = new ISO_UPD2Facade();
                            ISO_UpdDMD files3 = new ISO_UpdDMD();

                            int Result = 0;
                            files3.ID = int.Parse(Request.QueryString["ba"].ToString());
                            files3.LastModifiedBy = users.UserName;
                            Result = updF3.UpdateDataShare(files3);
                        }
                    }

                    CloseWindow(this);
                }
            }
        }

        protected void ShareUPD()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("NoDocument", typeof(string)); //1
            dt.Columns.Add("DocName", typeof(string)); //2
            dt.Columns.Add("RevisiNo", typeof(int)); //3
            dt.Columns.Add("CreatedBy", typeof(string)); //4
            dt.Columns.Add("CategoryUPD", typeof(int)); //5
            dt.Columns.Add("Type", typeof(int)); //6
            dt.Columns.Add("DeptID", typeof(int)); //7               
            dt.Columns.Add("FileName", typeof(string)); //8
            dt.Columns.Add("IDmaster", typeof(string)); //9 
            dt.Columns.Add("JenisUPD", typeof(string)); //10         
            dt.Columns.Add("PlantID", typeof(string)); //11
            dt.Columns.Add("Alasan", typeof(string)); //12
            //dt.Columns.Add("TglShare", typeof(DateTime)); //12

            DataRow row = dt.NewRow();
            row["NoDocument"] = Session["NoDocument"].ToString().Trim(); //1
            row["DocName"] = Session["DocName"].ToString().Trim(); //2
            row["RevisiNo"] = Convert.ToInt32(Session["RevisiNo"]); //3
            row["CreatedBy"] = Session["CreatedBy"].ToString().Trim(); //4
            row["CategoryUPD"] = Convert.ToInt32(Session["CategoryUPD"]); //5
            row["Type"] = Convert.ToInt32(Session["Type"]); //6
            row["DeptID"] = Convert.ToInt32(Session["DeptID"]); //7                
            row["FileName"] = Session["FileName"].ToString().Trim(); //8
            row["IDmaster"] = Convert.ToInt32(Session["IDmaster"]); //9
            row["JenisUPD"] = Convert.ToInt32(Session["JenisUPD"]); //10       
            row["PlantID"] = Convert.ToInt32(Session["PlantID"]); //11
            row["Alasan"] = Session["Alasan"].ToString().Trim(); //12
            //row["TglShare"] =Convert.ToDateTime(Session["TglShare"].ToString()); //12

            dt.Rows.Add(row);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);

            if (((Users)Session["Users"]).UnitKerjaID == 1)
            {
                string UPDFile = Session["UPDFile"].ToString();
                string ID = Session["IDmaster"].ToString();

                try
                {
                    WebReference_Krwg.Service1 bpas2 = new WebReference_Krwg.Service1();
                    string intResult = bpas2.InsertShareUPD(dt);

                    if (intResult == "")
                    {
                        ZetroView zll = new ZetroView();
                        zll.QueryType = Operation.CUSTOM;
                        zll.CustomQuery =
                        " update [sqlkrwg.grcboard.com].bpaskrwg.dbo.ISO_UPDTemp set FileName2='" + UPDFile + "' where IDMasterDokumenOP='" + ID + "' and rowstatus>-1";
                        SqlDataReader sdr = zll.Retrieve();
                    }

                    WebReference_Jmb.Service1 bpas3 = new WebReference_Jmb.Service1();
                    string intResult1 = bpas3.InsertShareUPD(dt);

                    if (intResult1 == "")
                    {
                        ZetroView zll = new ZetroView();
                        zll.QueryType = Operation.CUSTOM;
                        zll.CustomQuery =
                        " update [sqljombang.grcboard.com].bpasjombang.dbo.ISO_UPDTemp set FileName2='" + UPDFile + "' where IDMasterDokumenOP='" + ID + "' and rowstatus>-1";
                        SqlDataReader sdr = zll.Retrieve();
                    }
                }
                catch
                {
                    DisplayAJAXMessage(this, "Koneksi Database ke Karawang / Jombang ada masalah");
                }

            }
            else if (((Users)Session["Users"]).UnitKerjaID == 13)
            {
                string UPDFile = Session["UPDFile"].ToString();
                string ID = Session["IDmaster"].ToString();

                try
                {
                    WebReference_Ctrp.Service1 bpas2 = new WebReference_Ctrp.Service1();
                    string intResult = bpas2.InsertShareUPD(dt);

                    if (intResult == "")
                    {
                        ZetroView zll = new ZetroView();
                        zll.QueryType = Operation.CUSTOM;
                        zll.CustomQuery =
                        " update [sqlctrp.grcboard.com].bpasctrp.dbo.ISO_UPDTemp set FileName2='" + UPDFile + "' where IDMasterDokumenOP='" + ID + "' and rowstatus>-1";
                        SqlDataReader sdr = zll.Retrieve();
                    }


                    WebReference_Krwg.Service1 bpas3 = new WebReference_Krwg.Service1();
                    string intResult1 = bpas3.InsertShareUPD(dt);
                    if (intResult1 == "")
                    {
                        ZetroView zll = new ZetroView();
                        zll.QueryType = Operation.CUSTOM;
                        zll.CustomQuery =
                        " update [sqlkrwg.grcboard.com].bpaskrwg.dbo.ISO_UPDTemp set FileName2='" + UPDFile + "' where IDMasterDokumenOP='" + ID + "' and rowstatus>-1";
                        SqlDataReader sdr = zll.Retrieve();
                    }

                }
                catch
                {
                    DisplayAJAXMessage(this, "Koneksi Database ke Citeureup / Karawang ada masalah");
                }
            }
            else if (((Users)Session["Users"]).UnitKerjaID == 7)
            {
                string UPDFile = Session["UPDFile"].ToString();
                string ID = Session["IDmaster"].ToString();

                try
                {
                    WebReference_Ctrp.Service1 bpas2 = new WebReference_Ctrp.Service1();
                    string intResult = bpas2.InsertShareUPD(dt);

                    if (intResult == "")
                    {
                        ZetroView zll = new ZetroView();
                        zll.QueryType = Operation.CUSTOM;
                        zll.CustomQuery =
                        " update [sqlctrp.grcboard.com].bpasctrp.dbo.ISO_UPDTemp set FileName2='" + UPDFile + "' where IDMasterDokumenOP='" + ID + "' and rowstatus>-1";
                        SqlDataReader sdr = zll.Retrieve();
                    }

                    WebReference_Jmb.Service1 bpas3 = new WebReference_Jmb.Service1();
                    string intResult1 = bpas3.InsertShareUPD(dt);
                    if (intResult1 == "")
                    {
                        ZetroView zll = new ZetroView();
                        zll.QueryType = Operation.CUSTOM;
                        zll.CustomQuery =
                        " update [sqljombang.grcboard.com].bpasjombang.dbo.ISO_UPDTemp set FileName2='" + UPDFile + "' where IDMasterDokumenOP='" + ID + "' and rowstatus>-1";
                        SqlDataReader sdr = zll.Retrieve();
                    }

                }
                catch
                {
                    DisplayAJAXMessage(this, "Koneksi Database ke Citeureup / Jombang ada masalah");
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

        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }


        //Tambahan Baru Buat Test
        [DllImport("Mpr.dll", EntryPoint = "WNetAddConnection2", CallingConvention = CallingConvention.Winapi)]
        private static extern int WNetAddConnection2(NETRESOURCE lpNetResource, string lpPassword,
                                      string lpUsername, System.UInt32 dwFlags);

        [StructLayout(LayoutKind.Sequential)]
        private class NETRESOURCE
        {
            public ResourceScope dwScope = 0;
            public ResourceType dwType = 0;
            public ResourceDisplayType dwDisplayType = 0;
            public ResourceUsage dwUsage = 0;
            public string lpLocalName = null;
            public string lpRemoteName = null;
            public string lpComment = null;
            public string lpProvider = null;
        };

        public enum ResourceScope
        {
            RESOURCE_CONNECTED = 1,
            RESOURCE_GLOBALNET,
            RESOURCE_REMEMBERED,
            RESOURCE_RECENT,
            RESOURCE_CONTEXT
        };

        public enum ResourceType
        {
            RESOURCETYPE_ANY,
            RESOURCETYPE_DISK,
            RESOURCETYPE_PRINT,
            RESOURCETYPE_RESERVED
        };

        public enum ResourceUsage
        {
            RESOURCEUSAGE_CONNECTABLE = 0x00000001,
            RESOURCEUSAGE_CONTAINER = 0x00000002,
            RESOURCEUSAGE_NOLOCALDEVICE = 0x00000004,
            RESOURCEUSAGE_SIBLING = 0x00000008,
            RESOURCEUSAGE_ATTACHED = 0x00000010,
            RESOURCEUSAGE_ALL = (RESOURCEUSAGE_CONNECTABLE | RESOURCEUSAGE_CONTAINER | RESOURCEUSAGE_ATTACHED),
        };

        public enum ResourceDisplayType
        {
            RESOURCEDISPLAYTYPE_GENERIC,
            RESOURCEDISPLAYTYPE_DOMAIN,
            RESOURCEDISPLAYTYPE_SERVER,
            RESOURCEDISPLAYTYPE_SHARE,
            RESOURCEDISPLAYTYPE_FILE,
            RESOURCEDISPLAYTYPE_GROUP,
            RESOURCEDISPLAYTYPE_NETWORK,
            RESOURCEDISPLAYTYPE_ROOT,
            RESOURCEDISPLAYTYPE_SHAREADMIN,
            RESOURCEDISPLAYTYPE_DIRECTORY,
            RESOURCEDISPLAYTYPE_TREE,
            RESOURCEDISPLAYTYPE_NDSCONTAINER
        };
    }
}

public class ISO_UpdD4
{
    public string DocShare { get; set; }
    public string IPAddress { get; set; }
    public string UserID { get; set; }
    public DateTime CreatedTime { get; set; }
    public int Rowstatus { get; set; }
    public int Urutan { get; set; }
    public int IdUPD { get; set; }  
    public int StatusShare { get; set; }
}

public class ISO_UPDFacade4
{
    private ISO_UpdD4 objUPD4 = new ISO_UpdD4();
    protected string strError = string.Empty;
    private ArrayList arrData = new ArrayList();
    //private ISO_UpdD2 upd2 = new ISO_UpdD2();
    private List<SqlParameter> sqlListParam;

    public int InsertLogApv(object objDomain)
    {
        try
        {
            objUPD4 = (ISO_UpdD4)objDomain;
            sqlListParam = new List<SqlParameter>();
            sqlListParam.Add(new SqlParameter("@UserID", objUPD4.UserID));
            sqlListParam.Add(new SqlParameter("@IPAddress", objUPD4.IPAddress));
            sqlListParam.Add(new SqlParameter("@Urutan", objUPD4.Urutan));
            sqlListParam.Add(new SqlParameter("@IdUPD", objUPD4.IdUPD));
            sqlListParam.Add(new SqlParameter("@DocShare", objUPD4.DocShare));

            DataAccess da = new DataAccess(Global.ConnectionString());
            int result = da.ProcessData(sqlListParam, "UPD_SP_LogApv");
            strError = da.Error;
            return result;
        }
        catch (Exception ex)
        {
            strError = ex.Message;
            return -1;
        }

    }

    //public int cekUPDid(int ID)
    //{
    //    string StrSql = " select isnull(idUPD,0)idUPD from ISO_UpdDMD where ID=" + ID + " and rowstatus>-1 ";
    //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
    //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
    //    strError = dataAccess.Error;

    //    if (sqlDataReader.HasRows)
    //    {
    //        while (sqlDataReader.Read())
    //        {
    //            return Convert.ToInt32(sqlDataReader["idUPD"]);
    //        }
    //    }

    //    return 0;
    //}

    public ISO_UpdD4 cekUPDid(int ID)
    {
        string strSQL = " select isnull(idUPD,0)idUPD,StatusShare from ISO_UpdDMD where ID=" + ID + " and rowstatus>-1 ";

        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        strError = dataAccess.Error;
        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return GenerateObject_cekUPDid(sqlDataReader);
            }
        }
        return new ISO_UpdD4();
    }

    public ISO_UpdD4 GenerateObject_cekUPDid(SqlDataReader sqlDataReader)
    {
        objUPD4 = new ISO_UpdD4();

        objUPD4.IdUPD = Convert.ToInt32(sqlDataReader["IdUPD"]);
        objUPD4.StatusShare = Convert.ToInt32(sqlDataReader["StatusShare"]);       
        return objUPD4;
    }

}