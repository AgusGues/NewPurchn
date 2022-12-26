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
using Domain;
using BusinessFacade;
using DataAccessLayer;
using System.Net;
using System.Net.Mail;

namespace GRCweb1.Modul.Mtc
{
    public partial class ApprovalProject_Rev1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Session["FlagSearch"] = "0";
                Session["OpenImprove"] = null;
                ViewState["counter"] = 0;
                LoadOpenImprovement();
            }
        }

        //private void LoadDataHead()
        //{
        //    Users user = (Users)Session["Users"];
        //    MTC_ProjectFacade_Rev1 mtcF01 = new MTC_ProjectFacade_Rev1();
        //    ddlHead.Items.Clear();
        //    ddlHead.Items.Add(new ListItem("-- Pilih Head --", "0"));
        //    ArrayList arrHead = mtcF01.GetHead(user.DeptID);
        //    foreach (MTC_Project_Rev1 head in arrHead)
        //    {
        //        ddlHead.Items.Add(new ListItem(head.AreaName.ToString(), head.IDarea.ToString()));
        //    }

        //    if (arrHead.Count > 0)
        //    {
        //        trHead.Visible = true;
        //    }
        //    else
        //    {
        //        trHead.Visible = false;
        //    }
        //}

        private void LoadOpenImprovement()
        {
            ArrayList arrData = new ArrayList();
            MTC_ProjectFacade_Rev1 mpp = new MTC_ProjectFacade_Rev1();

            MTC_Project_Rev1 md = new MTC_Project_Rev1();
            Users user = (Users)Session["Users"];

            string[] App = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ApvPengajuan", "EngineeringNew").Split(',');
            int LevelApp = Array.IndexOf(App, ((Users)Session["Users"]).ID.ToString());
            //Level -1 : Mgr Pembuat Improvment
            //Level 0  : Head GA
            //Level 1  : Header Eng
            //Level 2  : Mgr Mtn
            //Level 3  : PM
            //Level 4  : Direksi
            //Level 5  : Mgr Hrd    

            string maxBiaya = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("EstimasiLevel", "EngineeringNew");
            string LastModifiedBy = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("LastModifiedBy", "EngineeringNew");

            int UserDept = user.DeptID;
            string UserDept1 = user.DeptID.ToString(); string flag = string.Empty; string tambah5 = string.Empty;

            // Tambahan 
            if (txtCari.Text != "Find by Improvement")
            {
                string NoProject = Session["NoProject"].ToString(); Session["Nomor"] = NoProject; Session["QueryAja"] = " and mp.Nomor='" + Session["Nomor"].ToString() + "' ";
            }
            else if (txtCari.Text == "Find by Improvement")
            {
                string NoProject = ""; Session["Nomor"] = NoProject; Session["QueryAja"] = " "; flag = "0";
            }
            // End Tambahan

            if (user.DeptID == 10 || user.DeptID == 6)
            {
                UserDept1 = " in (6,10)";
            }
            else if (user.DeptID == 4)
            {
                UserDept1 = " in (4,5,18,19)";
            }
            else
            {
                UserDept1 = "in (" + UserDept.ToString() + ")";
            }

            if (LevelApp < 0) // Manager Dept Pemohon
            {
                string tambah = "";
                string tambah2 = "";
                string tambah3 = "(mp.Status=0 and mp.rowstatus=0 and mp.Approval in (1,2) and mp.LastModifiedBy is null and " +
                                 " mp.DeptID " + UserDept1 + " and mp.Rowstatus>-1 " + Session["QueryAja"].ToString() + ") or " +
                                 "(mp.Status=1 and mp.rowstatus=1 and mp.Approval=1 " +
                                 //" and (mp.VerDate is null or mp.VerDate = 0 or mp.VerDate = -2) and mp.LastModifiedBy in ('headEnginering','Dede Wahyu')) " +
                                 " and (mp.VerDate is null or mp.VerDate = 0 or mp.VerDate = -2) and mp.LastModifiedBy in ('" + LastModifiedBy + "') " + Session["QueryAja"].ToString() + ") " +
                                 " and mp.DeptID " + UserDept1 + " and mp.rowstatus>-1 ";
                string tambah4 = "";
                if (flag == "0")
                { tambah5 = ""; }
                else
                { tambah5 = " and mp.Nomor='" + Session["Nomor"].ToString() + "' "; }

                txtTglFinish.Enabled = false;
                txtBiaya.Enabled = false;
                btnCancel.Visible = false;
                btnCancel.Visible = true;

                arrData = mpp.RetrieveOpenProject(LevelApp, tambah, tambah2, tambah3, tambah4, tambah5);

                string A = Session["FlagSearch"].ToString();

                if (A != "0")
                {
                    if (arrData.Count == 0)
                    {
                        DisplayAJAXMessage(this, "Data tidak ditemukan !!");
                        return;
                    }
                }

            }

            else if (LevelApp == 3) //PM
            {
                string tambah = " ";
                string tambah2 = " ";
                string tambah3 = " ((mp.Status =0 and mp.rowstatus=1 and mp.Approval=1 and  (mp.ApvPM is null or mp.ApvPM = 0)) " +
                                 " or (mp.Status =1 and mp.rowstatus=1 and mp.Approval=2 and mp.ApvPM=1) " +
                                 " or (mp.Status =2 and mp.rowstatus=2 and mp.Approval=3 and mp.ApvPM=2 and mp.release=1 and mp.DeptID in (4,5,18)) " +
                                 " or (mp.Status =2 and mp.rowstatus=2 and mp.Approval=4 and mp.ApvPM=2 and mp.release=1)) and mp.rowstatus>-1 /**order by mp.ApvPM **/ ";
                string tambah4 = "";
                //string tambah5 = " and mp.Nomor=" + Session["Nomor"].ToString() + " ";
                if (flag == "0")
                { tambah5 = ""; }
                else
                { tambah5 = " and mp.Nomor='" + Session["Nomor"].ToString() + "'"; }

                txtTglFinish.Enabled = false;
                txtBiaya.Enabled = false;
                btnCancel.Visible = false;
                btnCancel.Visible = true;
                arrData = mpp.RetrieveOpenProject(LevelApp, tambah, tambah2, tambah3, tambah4, tambah5);

                string A = Session["FlagSearch"].ToString();

                if (A != "0")
                {
                    if (arrData.Count == 0)
                    {
                        DisplayAJAXMessage(this, "Data tidak ditemukan !!");
                        return;
                    }
                }
            }
            #region Head Eng

            else if (LevelApp == 1) // Head Enginering
            {
                string nomorP = Session["Nomor"].ToString();

                if (nomorP == "")
                {
                    string tambah2 = " and mp.LastModifiedBy is not null ";
                    Session["tambah2"] = tambah2;
                }
                else
                {
                    string tambah2 = " and mp.nomor='" + nomorP + "' and mp.LastModifiedBy is not null ";
                    Session["tambah2"] = tambah2;
                }
                string tambah = " ";
                string tambah22 = Session["tambah2"].ToString();


                string tambah31 =
                " ((mp.Status = 0 and mp.rowstatus=1 and mp.Approval=1 and mp.ApvPM=1 and mp.ToDate='2000-11-11' and mp.Biaya=0 and (mp.ToDeptID=" + user.DeptID + " or mp.ToDeptID is null)) or " +
                " (mp.Status = 1 and mp.rowstatus=1 and mp.Approval=1 and mp.ApvPM=1 and mp.VerDate=-2 and (mp.ToDeptID=" + user.DeptID + " or mp.ToDeptID is null)) or " +
                " (mp.Status = 1 and mp.rowstatus=1 and mp.Approval=2 and mp.ApvPM=-1 and mp.VerDate=1 and (mp.ToDeptID=" + user.DeptID + " or mp.ToDeptID is null)) or " +
                " (mp.Status = 1 and mp.rowstatus=1 and mp.Approval=2 and mp.ApvPM=-2 and mp.VerDate=1 and (mp.ToDeptID=" + user.DeptID + " or mp.ToDeptID is null))) ";

                string tambah3 = tambah31;
                string tambah4 = "";
                //string tambah5 = " and mp.Nomor=" + Session["Nomor"].ToString() + " ";
                if (flag == "0")
                { tambah5 = ""; }
                else
                { tambah5 = " and mp.Nomor='" + Session["Nomor"].ToString() + "' "; }

                txtTglFinish.Enabled = true;
                txtBiaya.Enabled = true;
                btnCancel.Visible = true;
                arrData = mpp.RetrieveOpenProject(LevelApp, tambah, tambah22, tambah3, tambah4, tambah5);

                string A = Session["FlagSearch"].ToString();

                if (A != "0")
                {
                    if (arrData.Count == 0)
                    {
                        DisplayAJAXMessage(this, "Data tidak ditemukan !!");
                        return;
                    }
                }
            }
            #endregion
            #region Head GA
            else if (LevelApp == 0) // Head GA
            {
                string nomorP = Session["Nomor"].ToString();

                if (nomorP == "")
                {
                    string tambah2 = " and mp.LastModifiedBy is not null ";
                    Session["tambah2"] = tambah2;
                }
                else
                {
                    string tambah2 = " and mp.nomor='" + nomorP + "' and mp.LastModifiedBy is not null ";
                    Session["tambah2"] = tambah2;
                }
                string tambah = " ";
                string tambah22 = Session["tambah2"].ToString();

                string tambah31 =
                " ((mp.Status = 0 and mp.rowstatus=1 and mp.Approval=1 and mp.ApvPM=1 and mp.ToDate='2000-11-11' and mp.Biaya=0 and mp.ToDeptID=" + user.DeptID + ") or " +
                " (mp.Status = 1 and mp.rowstatus=1 and mp.Approval=1 and mp.ApvPM=1 and mp.VerDate=-2 and mp.ToDeptID=" + user.DeptID + ") or " +
                " (mp.Status = 1 and mp.rowstatus=1 and mp.Approval=2 and mp.ApvPM=-1 and mp.VerDate=1 and mp.ToDeptID=" + user.DeptID + ") or " +
                " (mp.Status = 1 and mp.rowstatus=1 and mp.Approval=2 and mp.ApvPM=-2 and mp.VerDate=1 and mp.ToDeptID=" + user.DeptID + ")) ";

                string tambah3 = tambah31;
                string tambah4 = "";
                //string tambah5 = " and mp.Nomor=" + Session["Nomor"].ToString() + " ";
                if (flag == "0")
                { tambah5 = ""; }
                else
                { tambah5 = " and mp.Nomor='" + Session["Nomor"].ToString() + "' "; }

                txtTglFinish.Enabled = true;
                txtBiaya.Enabled = true;
                arrData = mpp.RetrieveOpenProject(LevelApp, tambah, tambah22, tambah3, tambah4, tambah5);

                string A = Session["FlagSearch"].ToString();

                if (A != "0")
                {
                    if (arrData.Count == 0)
                    {
                        DisplayAJAXMessage(this, "Data tidak ditemukan !!");
                        return;
                    }
                }
            }
            #endregion
            #region MGR MTN
            else if (LevelApp == 2)  // MGR MTN 
            {
                string nomorP = Session["Nomor"].ToString();

                if (nomorP == "")
                {
                    string tambah2 = " and mp.LastModifiedBy is not null ";
                    Session["tambah2"] = tambah2;
                }
                else
                {
                    string tambah2 = " and mp.nomor='" + nomorP + "' and mp.LastModifiedBy is not null ";
                    Session["tambah2"] = tambah2;
                }
                //string tambah = " ";
                string tambah22 = Session["tambah2"].ToString();


                string tambah = " ";
                //string tambah2 = Session["tambah2"].ToString();
                string tambah3 =
                " ((mp.Status =1 and mp.rowstatus=1 and mp.Approval=1 and mp.LastModifiedBy is not null and (mp.ToDeptID=19 or mp.ToDeptID is null)) or " +
                " (mp.Status=0 and mp.rowstatus=0 and mp.Approval in (1,2) and mp.DeptID in (4,5,18) and mp.LastModifiedBy is null and (mp.ToDeptID=19 or mp.ToDeptID is null)) or " +
                " (mp.Status=0 and mp.rowstatus=0 and mp.Approval=1 and mp.DeptID in (4,5,18,19) and mp.ToDeptID<>19 and mp.LastModifiedBy is null) ) ";
                string tambah4 = "";
                //string tambah5 = " and mp.Nomor=" + Session["Nomor"].ToString() + " ";
                if (flag == "0")
                { tambah5 = ""; }
                else
                { tambah5 = " and mp.Nomor='" + Session["Nomor"].ToString() + "' "; }

                txtTglFinish.Enabled = true;
                txtBiaya.Enabled = true;
                arrData = mpp.RetrieveOpenProject(LevelApp, tambah, tambah22, tambah3, tambah4, tambah5);

                string A = Session["FlagSearch"].ToString();

                if (A != "0")
                {
                    if (arrData.Count == 0)
                    {
                        DisplayAJAXMessage(this, "Data tidak ditemukan !!");
                        return;
                    }
                }
            }
            #endregion
            #region MGR GA
            else if (LevelApp == 5)  // MGR GA 
            {
                string tambah = " ";
                string tambah2 = "";
                string tambah3 =
                " ((mp.Status =1 and mp.rowstatus=1 and mp.Approval=1 and mp.LastModifiedBy is not null and mp.ToDeptID=7) or " +
                " (mp.Status =1 and mp.rowstatus=1 and mp.Approval=1 and VerDate<>1 and mp.DeptID=7) or " +
                " (mp.Status=0 and mp.rowstatus=0 and mp.Approval=1 and mp.DeptID=7 and mp.ToDeptID<>7 and mp.LastModifiedBy is null) ) ";
                string tambah4 = "";
                //string tambah5 = " and mp.Nomor=" + Session["Nomor"].ToString() + " ";
                if (flag == "0")
                { tambah5 = ""; }
                else
                { tambah5 = " and mp.Nomor='" + Session["Nomor"].ToString() + "' "; }

                txtTglFinish.Enabled = true;
                txtBiaya.Enabled = true;
                arrData = mpp.RetrieveOpenProject(LevelApp, tambah, tambah2, tambah3, tambah4, tambah5);

                if (arrData.Count > 0)
                {
                    bntApprove.Enabled = true;
                }

                string A = Session["FlagSearch"].ToString();

                if (A != "0")
                {
                    if (arrData.Count == 0)
                    {

                        DisplayAJAXMessage(this, "Data tidak ditemukan !!");
                        return;
                    }

                }



                //if (arrData.Count > 0)
                //{
                //    DisplayAJAXMessage(this, "Data tidak ditemukan !!");
                //    return;
                //}
            }
            #endregion
            #region Direksi
            else if (LevelApp == 4) //Direksi
            {
                string tambah = " ";
                string tambah2 = " and mp.LastModifiedBy <> 'Vero' ";
                string tambah3 = " mp.Status =2 and mp.rowstatus=2 and mp.Approval=2 and mp.ApvPM=2 and mp.Biaya > " + decimal.Parse(maxBiaya) + "  ";
                string tambah4 = "";
                //string tambah5 = " and mp.Nomor=" + Session["Nomor"].ToString() + " ";
                if (flag == "0")
                { tambah5 = ""; }
                else
                { tambah5 = " and mp.Nomor='" + Session["Nomor"].ToString() + "' "; }

                txtTglFinish.Enabled = false;
                txtBiaya.Enabled = false;
                arrData = mpp.RetrieveOpenProject(LevelApp, tambah, tambah2, tambah3, tambah4, tambah5);

                string A = Session["FlagSearch"].ToString();

                if (A != "0")
                {
                    if (arrData.Count == 0)
                    {
                        DisplayAJAXMessage(this, "Data tidak ditemukan !!");
                        return;
                    }
                }
            }
            #endregion
            #region Head MTN
            else if (LevelApp < 0 && user.Apv > 0 && user.DeptID == 5
                || LevelApp < 0 && user.Apv > 0 && user.DeptID == 18
                || LevelApp < 0 && user.Apv > 0 && user.DeptID == 4) // Head MTN 
            {
                string tambah = " DeptID=" + UserDept + " and ";
                string tambah2 = " and mp.LastModifiedBy is null ";
                string tambah3 = " and ((mp.Status =0 and mp.rowstatus=0 and mp.Approval in (1) or (mp.Status =1 and mp.rowstatus=1 and mp.Approval in (1))) ";
                string tambah4 = "";
                //string tambah5 = " and mp.Nomor=" + Session["Nomor"].ToString() + " ";
                if (flag == "0")
                { tambah5 = ""; }
                else
                { tambah5 = " and mp.Nomor='" + Session["Nomor"].ToString() + "' "; }

                txtTglFinish.Enabled = true;
                txtBiaya.Enabled = true;
                arrData = mpp.RetrieveOpenProject(LevelApp, tambah, tambah2, tambah3, tambah4, tambah5);

                string A = Session["FlagSearch"].ToString();

                if (A != "0")
                {
                    if (arrData.Count == 0)
                    {
                        DisplayAJAXMessage(this, "Data tidak ditemukan !!");
                        return;
                    }
                }
            }
            #endregion
            Session["OpenImprove"] = arrData;
            txtJmlData.Value = arrData.Count.ToString();
            int Counter = (int)ViewState["counter"];
            //int Counter = Convert.ToInt32(txtJmlData.Value);
            //int Counter = Convert.ToInt32(txtJmlData.Value);

            int Flag = Convert.ToInt32(Session["FlagSearch"]);

            if (Flag == 2)
            {
                LoadOpenImprovement(0);
            }
            else
            {
                LoadOpenImprovement(Counter);
            }

            btnPrev.Enabled = (Counter > 1) ? true : false;
            //btnNext.Enabled = (Counter > 0) ? true : false;
            btnNext.Enabled = (Counter < Convert.ToInt32(txtJmlData.Value)) ? true : false;
            //btnPrev.Enabled = (Counter < Convert.ToInt32(txtJmlData.Value)) ? true : false;
        }

        private void LoadOpenImprovement(int index)
        {
            Users user = (Users)Session["Users"];
            ArrayList arrData = (ArrayList)Session["OpenImprove"];

            if (arrData.Count <= 0 || (index) >= arrData.Count)
            {
                btnNext.Enabled = ((index - 1) > arrData.Count) ? true : false;
                btnPrev.Enabled = ((index - 1) > arrData.Count) ? false : true;
                return;
            }

            MTC_Project_Rev1 mp = (MTC_Project_Rev1)arrData[index];
            MTC_ProjectFacade_Rev1 mpp = new MTC_ProjectFacade_Rev1();
            int Sts = 0;
            Sts = mpp.CheckStatusApprove(mp.ID);
            string[] App = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ApvPengajuan", "EngineeringNew").Split(',');
            int LevelApp = Array.IndexOf(App, ((Users)Session["Users"]).ID.ToString());
            string maxBiaya = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("EstimasiLevel", "EngineeringNew");

            //string[] App2 = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("LastModifiedByID", "EngineeringNew").Split(',');
            //int LevelApp2 = Array.IndexOf(App2, (mp.);

            //Users user = (Users)Session["Users"];
            Session["ApvPM"] = mp.VerPM;
            Session["VerDate"] = mp.VerUser;
            Session["ToDate"] = mp.ToDate;
            Session["NoProject"] = mp.Nomor;
            Session["ToDept"] = mp.ToDept;

            MTC_ProjectFacade_Rev1 ProjectFacade = new MTC_ProjectFacade_Rev1();
            MTC_Project_Rev1 mpD = new MTC_Project_Rev1();

            mpD = ProjectFacade.RetrieveDataLastModif(mp.Nomor);


            #region Biaya masih kosong     
            if (mpD.Biaya == 0)
            {
                #region Approval Manager Dept. Pemohon
                if (LevelApp < 0 || LevelApp == 2 || LevelApp == 5)
                {
                    Session["ApvPM"] = mp.VerPM;
                    txtNoImprovement.Text = mp.Nomor.ToUpper();
                    txtTanggal.Text = Convert.ToDateTime(mp.FromDate2).ToString("dd-MM-yyyy");
                    txtNamaImprovement.Text = mp.NamaProject.ToString();
                    txtDeptPemohon.Text = mp.DeptName.ToString();
                    txtGroupName.Text = mp.GroupName;
                    txtSasaran.Text = mp.Sasaran;

                    if (mp.Approval == 1 && mp.Status == 0 && mp.RowStatus == 0)
                    //{txtStatusApproval.Text = "Open - Dibuat Oleh :"+" "+mp.CreatedBy.Trim();}
                    { txtStatusApproval.Text = "Open - Dibuat Oleh :" + " " + mp.NamaHead.Trim(); }

                    if (mp.ProdLine == 99) { txtArea.Text = "General"; }
                    else if (mp.ProdLine == 97) { txtArea.Text = "Material Preparation"; }
                    else if (mp.ProdLine == 98) { txtArea.Text = "WTP"; }
                    else if (mp.ProdLine == 1) { txtArea.Text = "Zona 1" + "-" + mp.Zona; }
                    else if (mp.ProdLine == 2) { txtArea.Text = "Zona 2" + "-" + mp.Zona; }
                    else if (mp.ProdLine == 3) { txtArea.Text = "Zona 3" + "-" + mp.Zona; }
                    else if (mp.ProdLine == 4) { txtArea.Text = "Zona 4"; }

                    if (mp.Biaya > 0 && mp.ToDate.ToString() != "11/11/2000 0:00:00")
                    { btnRe.Visible = true; btnCancel.Visible = false; }
                    else if (mp.Biaya == 0 && mp.ToDate.ToString() != "11/11/2000 0:00:00")
                    { btnRe.Visible = true; btnCancel.Visible = true; }

                    string target = mp.ToDate.ToString();
                    if (target == "11/11/2000 00:00:00" || target == "11/11/2000 00.00.00") { txtTglFinish.Text = "Target belum ditentukan"; }
                    else if (target != "11/11/2000 00:00:00" || target == "11/11/2000 00.00.00") { txtTglFinish.Text = mp.ToDate.ToString("dd-MM-yyyy"); }

                    if (mp.ToDeptID == 19 || mp.ToDeptID == 0)
                    {
                        txtDept.Visible = true; txtDept.Text = "MAINTENANCE";
                        txtDept.Attributes["style"] = "color:white;";
                        txtDept.BackColor = System.Drawing.Color.Green;
                    }
                    else if (mp.ToDeptID == 7 && mp.Approval == 1 && mp.Status == 0 && mp.RowStatus == 0)
                    {
                        txtDept.Visible = true; txtDept.Text = "HRD GA";
                        txtDept.Attributes["style"] = "color:white;";
                        txtDept.BackColor = System.Drawing.Color.Green;
                        LabelInfo.Visible = true; LabelInfo.Text = "IMPROVMENT di tujukan ke HRD GA";
                        LabelInfo2.Visible = false;
                    }
                    else if (mp.ToDeptID == 19 && mp.Approval == 1 && mp.Status == 0 && mp.RowStatus == 0)
                    {
                        txtDept.Visible = true; txtDept.Text = "MAINTENANCE";
                        txtDept.Attributes["style"] = "color:white;";
                        txtDept.BackColor = System.Drawing.Color.Green;
                        txtTglFinish.Visible = false; LabelInfo.Visible = true; LabelInfo.Text = "IMPROVMENT di tujukan ke MAINTENANCE";
                        LabelInfo2.Visible = false;
                    }

                    txtBiaya.Text = mp.Biaya.ToString("###,##0.#0");
                    txtID.Value = mp.ID.ToString();
                    txtStatus.Text = mp.Status.ToString();
                    txtApproval.Text = mp.Approval.ToString();
                    txtRowstatus.Text = mp.RowStatus.ToString();
                    txtDeptID.Text = mp.DeptID.ToString();
                    txtDetailSasaran.Text = mp.DetailSasaran.ToString();
                    txtQty.Text = Convert.ToDecimal(mp.Quantity).ToString();
                    txtSatuan.Text = mp.UomCode.ToString();
                    string Noted = mp.Noted1.ToString();
                    if (Noted == "1")
                    {
                        RBBekas.Visible = true; RBBekas.Checked = true;
                    }
                    //txtPembuat.Text = mp.CreatedBy;
                    txtPembuat.Text = mp.NamaHead;

                    bntApprove.Enabled = true;
                }
                #endregion

                #region Approval Head HRDGA & Engineering
                else if (LevelApp == 1 || LevelApp == 0)
                {
                    Session["ApvPM"] = mp.VerPM;
                    Session["VerDate"] = mp.VerUser;

                    RBBekas.Visible = true;
                    txtNoImprovement.Text = mp.Nomor.ToUpper();
                    txtTanggal.Text = Convert.ToDateTime(mp.FromDate2).ToString("dd-MM-yyyy");
                    txtNamaImprovement.Text = mp.NamaProject.ToString();
                    txtDeptPemohon.Text = mp.DeptName.ToString();
                    txtGroupName.Text = mp.GroupName;
                    txtSasaran.Text = mp.Sasaran;

                    if (mp.Approval == 1 && mp.Status == 0 && mp.RowStatus == 1 && mp.VerPM == 1 && mp.VerDate == 0)
                    { txtStatusApproval.Text = "Approved PM Pertama - Oleh :" + " " + mp.LastModifiedBy2.Trim(); }
                    else if (mp.Approval == 1 && mp.Status == 1 && mp.RowStatus == 1 && mp.VerPM == 1 && mp.VerDate == -2)
                    { txtStatusApproval.Text = "DiTolak - Oleh :" + " " + mp.LastModifiedBy2.Trim(); }

                    if (mp.ProdLine == 99) { txtArea.Text = "General"; }
                    else if (mp.ProdLine == 97) { txtArea.Text = "Material Preparation"; }
                    else if (mp.ProdLine == 98) { txtArea.Text = "WTP"; }
                    else if (mp.ProdLine == 1) { txtArea.Text = "Zona 1" + "-" + mp.Zona; }
                    else if (mp.ProdLine == 2) { txtArea.Text = "Zona 2" + "-" + mp.Zona; }
                    else if (mp.ProdLine == 3) { txtArea.Text = "Zona 3" + "-" + mp.Zona; }
                    else if (mp.ProdLine == 4) { txtArea.Text = "Zona 4"; }

                    txtDetailSasaran.Text = mp.DetailSasaran.ToString();
                    txtTglFinish.Text = Convert.ToDateTime(mp.ToDate2).ToString("dd-MM-yyyy");
                    string target = mp.ToDate.ToString("dd/MM/yyyy");
                    if (target == "11/11/2000")
                    {
                        txtTglFinish.Text = "Target belum ditentukan";
                    }

                    if (mp.ToDeptID == 19 || mp.ToDeptID == 0)
                    {
                        txtDept.Visible = true; txtDept.Text = "MAINTENANCE";
                        txtDept.Attributes["style"] = "color:white;";
                        txtDept.BackColor = System.Drawing.Color.Green;
                    }
                    else if (mp.ToDeptID == 7)
                    {
                        txtDept.Visible = true; txtDept.Text = "HRD GA";
                        txtDept.Attributes["style"] = "color:white; font-style:calibri";
                        txtDept.BackColor = System.Drawing.Color.Green;
                    }

                    txtBiaya.Text = mp.Biaya.ToString("###,##0.#0");
                    txtID.Value = mp.ID.ToString();
                    txtStatus.Text = mp.Status.ToString();
                    txtApproval.Text = mp.Approval.ToString();
                    txtRowstatus.Text = mp.RowStatus.ToString();
                    txtDeptID.Text = mp.DeptID.ToString();
                    txtQty.Text = Convert.ToDecimal(mp.Quantity).ToString();
                    txtSatuan.Text = mp.UomCode.ToString();
                    txtPembuat.Text = mp.NamaHead;
                    bntApprove.Enabled = true;
                }
                #endregion

                #region PM Filter I Approval
                else if (LevelApp == 3)
                {
                    if (mp.Approval == 1 && mp.Status == 0 && mp.RowStatus == 1) { txtStatusApproval.Text = "Approved - Oleh :" + " " + mp.LastModifiedBy2.Trim(); }
                    string target = mp.ToDate.ToString();
                    if (target == "11/11/2000 0:00:00") { txtTglFinish.Text = "Target belum ditentukan"; }

                    if (mp.ToDeptID == 19 || mp.ToDeptID == 0)
                    {
                        txtDept.Visible = true; txtDept.Text = "MAINTENANCE";
                        txtDept.Attributes["style"] = "color:white;";
                        txtDept.BackColor = System.Drawing.Color.Green;
                    }
                    else if (mp.ToDeptID == 7)
                    {
                        txtDept.Visible = true; txtDept.Text = "HRD GA";
                        txtDept.Attributes["style"] = "color:white; font-style:calibri";
                        txtDept.BackColor = System.Drawing.Color.Green;
                    }

                    txtNoImprovement.Text = mp.Nomor.ToUpper();
                    txtTanggal.Text = Convert.ToDateTime(mp.FromDate2).ToString("dd-MM-yyyy");
                    txtNamaImprovement.Text = mp.NamaProject.ToString();
                    txtDeptPemohon.Text = mp.DeptName.ToString();
                    txtGroupName.Text = mp.GroupName;
                    txtSasaran.Text = mp.Sasaran;

                    if (mp.ProdLine == 99) { txtArea.Text = "General"; }
                    else if (mp.ProdLine == 97) { txtArea.Text = "Material Preparation"; }
                    else if (mp.ProdLine == 98) { txtArea.Text = "WTP"; }
                    else if (mp.ProdLine == 1) { txtArea.Text = "Zona 1" + "-" + mp.Zona; }
                    else if (mp.ProdLine == 2) { txtArea.Text = "Zona 2" + "-" + mp.Zona; }
                    else if (mp.ProdLine == 3) { txtArea.Text = "Zona 3" + "-" + mp.Zona; }
                    else if (mp.ProdLine == 4) { txtArea.Text = "Zona 4"; }

                    txtBiaya.Text = mp.Biaya.ToString("###,##0.#0");
                    txtID.Value = mp.ID.ToString();
                    txtStatus.Text = mp.Status.ToString();
                    txtApproval.Text = mp.Approval.ToString();
                    txtRowstatus.Text = mp.RowStatus.ToString();
                    txtDetailSasaran.Text = mp.DetailSasaran.ToString();
                    txtQty.Text = Convert.ToDecimal(mp.Quantity).ToString();
                    txtSatuan.Text = mp.UomCode.ToString();
                    txtPembuat.Text = mp.NamaHead;
                    bntApprove.Enabled = true;
                    LoadEstimasiMaterial(mp.ID);
                }
                #endregion
            }
            #endregion

            #region Biaya sudah ada
            if (mpD.Biaya > 0)
            {
                if (LevelApp < 0) // Manager Dept Pemohon
                {
                    Session["ApvPM"] = mp.VerPM;
                    Session["ToDate"] = mp.ToDate;

                    txtNoImprovement.Text = mp.Nomor.ToUpper();
                    txtTanggal.Text = Convert.ToDateTime(mp.FromDate2).ToString("dd-MM-yyyy");
                    txtNamaImprovement.Text = mp.NamaProject.ToString();
                    txtDeptPemohon.Text = mp.DeptName.ToString();
                    txtGroupName.Text = mp.GroupName;
                    txtSasaran.Text = mp.Sasaran;

                    //if (mp.Approval == 1 && mp.Status == 1 && mp.RowStatus == 1 && mp.VerPM == 1 && mp.VerDate == 0 && mp.LastModifiedBy == "Dede Wahyu")
                    if (mp.Approval == 1 && mp.Status == 1 && mp.RowStatus == 1 && mp.VerPM == 1 && mp.VerDate == 0)
                    {
                        txtStatusApproval.Text = "Approved - Oleh :" + " " + mp.LastModifiedBy2.Trim();
                        LabelInfo.Visible = true; LabelInfo.Text = "Target Finished Date Sudah Ditentukan !!";
                    }
                    //else if (mp.Approval == 1 && mp.Status == 1 && mp.RowStatus == 1 && mp.VerPM == 1 && mp.VerDate == 0 && mp.LastModifiedBy == "headEnginering")
                    //{ txtStatusApproval.Text = "Approved - Oleh :" + " " + mp.LastModifiedBy.Trim();
                    //LabelInfo.Visible = true; LabelInfo.Text = "Target Finished Date Sudah Ditentukan !!";
                    //}

                    if (mp.ToDeptID == 19 || mp.ToDeptID == 0)
                    {
                        txtDept.Visible = true; txtDept.Text = "MAINTENANCE";
                        txtDept.Attributes["style"] = "color:white;";
                        txtDept.BackColor = System.Drawing.Color.Green;
                    }
                    else if (mp.ToDeptID == 7)
                    {
                        txtDept.Visible = true; txtDept.Text = "HRD GA";
                        txtDept.Attributes["style"] = "color:white; font-style:calibri";
                        txtDept.BackColor = System.Drawing.Color.Green;

                    }

                    if (mp.ProdLine == 99) { txtArea.Text = "General"; }
                    else if (mp.ProdLine == 97) { txtArea.Text = "Material Preparation"; }
                    else if (mp.ProdLine == 98) { txtArea.Text = "WTP"; }
                    else if (mp.ProdLine == 1) { txtArea.Text = "Zona 1" + "-" + mp.Zona; }
                    else if (mp.ProdLine == 2) { txtArea.Text = "Zona 2" + "-" + mp.Zona; }
                    else if (mp.ProdLine == 3) { txtArea.Text = "Zona 3" + "-" + mp.Zona; }
                    else if (mp.ProdLine == 4) { txtArea.Text = "Zona 4"; }

                    if (mp.Biaya > 0 && mp.ToDate.ToString() != "11/11/2000 0:00:00")
                    { btnRe.Visible = true; btnCancel.Visible = false; }

                    string target = mp.ToDate.ToString();
                    if (target == "11/11/2000 0:00:00") { txtTglFinish.Text = "Target belum ditentukan"; }
                    else { txtTglFinish.Text = Convert.ToDateTime(mp.FinishDate).ToString("dd-MM-yyyy"); }

                    txtBiaya.Text = mp.Biaya.ToString("###,##0.#0");
                    txtID.Value = mp.ID.ToString();
                    txtStatus.Text = mp.Status.ToString();
                    txtApproval.Text = mp.Approval.ToString();
                    txtRowstatus.Text = mp.RowStatus.ToString();
                    txtDeptID.Text = mp.DeptID.ToString();
                    txtDetailSasaran.Text = mp.DetailSasaran.ToString();
                    txtQty.Text = Convert.ToDecimal(mp.Quantity).ToString();
                    txtSatuan.Text = mp.UomCode.ToString();
                    txtPembuat.Text = mp.NamaHead;
                    bntApprove.Enabled = true;

                    LoadEstimasiMaterial(mp.ID);
                }

                else if (LevelApp == 1) // Head Enginering
                {
                    //Users user = (Users)Session["Users"];
                    Session["ApvPM"] = mp.VerPM;
                    Session["VerDate"] = mp.VerUser;
                    Session["ToDate"] = mp.ToDate;

                    RBBekas.Visible = true;
                    txtNoImprovement.Text = mp.Nomor.ToUpper();
                    txtTanggal.Text = Convert.ToDateTime(mp.FromDate2).ToString("dd-MM-yyyy");
                    txtNamaImprovement.Text = mp.NamaProject.ToString();
                    txtDeptPemohon.Text = mp.DeptName.ToString();
                    txtGroupName.Text = mp.GroupName;
                    txtSasaran.Text = mp.Sasaran;

                    if (mp.Approval == 1 && mp.Status == 1 && mp.RowStatus == 1 && mp.VerPM == 1 && mp.VerUser == -2)
                    {
                        txtStatusApproval.Text = "Not Approved - Oleh :" + " " + mp.LastModifiedBy2.Trim();
                        LabelInfo.Visible = true; LabelInfo.Text = "Target Finished Date Harap di Rubah !!";
                    }
                    else if (mp.Approval == 2 && mp.Status == 1 && mp.RowStatus == 1 && mp.VerPM == -1 && mp.VerUser == 1)
                    {
                        txtStatusApproval.Text = "Not Approved - Oleh :" + " " + mp.LastModifiedBy2.Trim();
                        LabelInfo.Visible = true; LabelInfo.Text = "Estimasi Material dan Biaya harus di Rubah !!";
                    }
                    else if (mp.Approval == 2 && mp.Status == 1 && mp.RowStatus == 1 && mp.VerPM == -2 && mp.VerUser == 1)
                    {
                        txtStatusApproval.Text = "Not Approved - Oleh :" + " " + mp.LastModifiedBy2.Trim();
                        LabelInfo.Visible = true; LabelInfo.Text = " Target dan tujuan tidak tepat !!"; btnCancel.Visible = true;
                    }


                    if (mp.ProdLine == 99) { txtArea.Text = "General"; }
                    else if (mp.ProdLine == 97) { txtArea.Text = "Material Preparation"; }
                    else if (mp.ProdLine == 98) { txtArea.Text = "WTP"; }
                    else if (mp.ProdLine == 1) { txtArea.Text = "Zona 1" + "-" + mp.Zona; }
                    else if (mp.ProdLine == 2) { txtArea.Text = "Zona 2" + "-" + mp.Zona; }
                    else if (mp.ProdLine == 3) { txtArea.Text = "Zona 3" + "-" + mp.Zona; }
                    else if (mp.ProdLine == 4) { txtArea.Text = "Zona 4"; }

                    txtDetailSasaran.Text = mp.DetailSasaran.ToString();
                    txtTglFinish.Text = Convert.ToDateTime(mp.ToDate2).ToString("dd-MM-yyyy");
                    string target = mp.ToDate.ToString();
                    if (target == "11/11/2000 0:00:00")
                    {
                        txtTglFinish.Text = "Target belum ditentukan";
                    }
                    txtBiaya.Text = mp.Biaya.ToString("###,##0.#0");
                    txtID.Value = mp.ID.ToString();
                    txtStatus.Text = mp.Status.ToString();
                    txtApproval.Text = mp.Approval.ToString();
                    txtRowstatus.Text = mp.RowStatus.ToString();
                    txtDeptID.Text = mp.DeptID.ToString();
                    txtQty.Text = Convert.ToDecimal(mp.Quantity).ToString();
                    txtSatuan.Text = mp.UomCode.ToString();
                    txtPembuat.Text = mp.NamaHead;
                    bntApprove.Enabled = true;

                    LoadEstimasiMaterial(mp.ID);
                }

                //else if (mp.Status == 1 && mp.Approval == 1 && mp.RowStatus == 1 && mp.VerDate == 1 && mp.VerPM == 1) // Manager MTN
                else if (LevelApp == 2 || LevelApp == 5) // Manager MTN
                {
                    MTC_ProjectFacade_Rev1 ProjectFacade2 = new MTC_ProjectFacade_Rev1();
                    MTC_Project_Rev1 mpD2 = new MTC_Project_Rev1();
                    string NomorProject = Session["NoProject"].ToString();

                    int CekEstimasiMaterial = ProjectFacade2.CekData(NomorProject);

                    //Users user = (Users)Session["Users"];
                    Session["ApvPM"] = mp.VerPM;
                    Session["VerDate"] = mp.VerUser;
                    Session["ToDate"] = mp.ToDate;

                    txtNoImprovement.Text = mp.Nomor.ToUpper();
                    txtTanggal.Text = Convert.ToDateTime(mp.FromDate2).ToString("dd-MM-yyyy");
                    txtNamaImprovement.Text = mp.NamaProject.ToString();
                    txtDeptPemohon.Text = mp.DeptName.ToString();
                    txtGroupName.Text = mp.GroupName;
                    txtSasaran.Text = mp.Sasaran;

                    if (mp.Approval == 1 && mp.Status == 1 && mp.RowStatus == 1 && mp.VerPM == 1 && mp.VerUser == 1 && CekEstimasiMaterial == 0)
                    {
                        txtStatusApproval.Text = "Approved - Oleh :" + " " + mp.LastModifiedBy2.Trim();
                        LabelInfo.Visible = true; LabelInfo.Text = "Target Finished Date Sudah Disetujui !!";
                        LabelInfo2.Visible = true; LabelInfo2.Text = "Head Enginering belum membuat Estimasi Material !!";
                        bntApprove.Enabled = false;
                    }
                    else if (mp.Approval == 1 && mp.Status == 1 && mp.RowStatus == 1 && mp.VerPM == 1 && mp.VerUser == 1 && CekEstimasiMaterial > 0)
                    {
                        txtStatusApproval.Text = "Approved - Oleh :" + " " + mp.LastModifiedBy2.Trim();
                        LabelInfo.Visible = true; LabelInfo.Text = "Target Finished Date Sudah Disetujui !!"; LabelInfo2.Visible = false;
                        bntApprove.Enabled = true;
                    }
                    else if (mp.Approval == 1 && mp.Status == 1 && mp.RowStatus == 1 && mp.VerPM == 1 && mp.VerUser == 0 && CekEstimasiMaterial > 0)
                    {
                        txtStatusApproval.Text = "Approved - Oleh :" + " " + mp.LastModifiedBy2.Trim();
                        LabelInfo.Visible = true; LabelInfo.Text = "Target Finished Date blm Disetujui Mgr Dept. Peminta !!"; LabelInfo2.Visible = false;
                        bntApprove.Enabled = true;
                    }
                    else if (mp.Approval == 1 && mp.Status == 1 && mp.RowStatus == 1 && mp.VerPM == 1 && mp.VerUser == 0 && CekEstimasiMaterial == 0)
                    {
                        txtStatusApproval.Text = "Approved - Oleh :" + " " + mp.LastModifiedBy2.Trim();
                        LabelInfo.Visible = true; LabelInfo.Text = "Target Finished Date Belum di Approved oleh Manager Peminta!!";
                        LabelInfo2.Visible = true; LabelInfo2.Text = "Head Enginering belum membuat Estimasi Material !!";
                        bntApprove.Enabled = false;
                    }
                    //else if (mp.Approval == 2 && mp.Status == 1 && mp.RowStatus == 1 && mp.VerPM == 1 && mp.VerUser == 1)
                    //{
                    //    txtStatusApproval.Text = "Approved - Oleh :" + " " + mp.LastModifiedBy.Trim();
                    //    LabelInfo.Visible = true; LabelInfo.Text = "Target Finished Date Sudah Disetujui !!";
                    //    LabelInfo2.Visible = false;
                    //}
                    else
                    {
                        LabelInfo.Visible = false; LabelInfo2.Visible = false;
                    }

                    if (mp.ToDeptID == 19 || mp.ToDeptID == 0)
                    {
                        txtDept.Visible = true; txtDept.Text = "MAINTENANCE";
                        txtDept.Attributes["style"] = "color:white;";
                        txtDept.BackColor = System.Drawing.Color.Green;
                    }
                    else if (mp.ToDeptID == 7)
                    {
                        txtDept.Visible = true; txtDept.Text = "HRD GA";
                        txtDept.Attributes["style"] = "color:white; font-style:calibri";
                        txtDept.BackColor = System.Drawing.Color.Green;
                        LabelInfo.Visible = false; LabelInfo2.Visible = false;

                    }


                    if (mp.ProdLine == 99) { txtArea.Text = "General"; }
                    else if (mp.ProdLine == 97) { txtArea.Text = "Material Preparation"; }
                    else if (mp.ProdLine == 98) { txtArea.Text = "WTP"; }
                    else if (mp.ProdLine == 1) { txtArea.Text = "Zona 1" + "-" + mp.Zona; }
                    else if (mp.ProdLine == 2) { txtArea.Text = "Zona 2" + "-" + mp.Zona; }
                    else if (mp.ProdLine == 3) { txtArea.Text = "Zona 3" + "-" + mp.Zona; }
                    else if (mp.ProdLine == 4) { txtArea.Text = "Zona 4"; }

                    txtTglFinish.Text = Convert.ToDateTime(mp.ToDate2).ToString("dd-MM-yyyy");
                    txtBiaya.Text = mp.Biaya.ToString("###,##0.#0");
                    txtID.Value = mp.ID.ToString();
                    txtStatus.Text = mp.Status.ToString();
                    txtApproval.Text = mp.Approval.ToString();
                    txtRowstatus.Text = mp.RowStatus.ToString();
                    txtDeptID.Text = mp.DeptID.ToString();
                    txtDetailSasaran.Text = mp.DetailSasaran.ToString();
                    txtQty.Text = Convert.ToDecimal(mp.Quantity).ToString();
                    txtSatuan.Text = mp.UomCode.ToString();
                    txtPembuat.Text = mp.NamaHead;
                    if (mpD.VerDate == 0 && mpD.ProjectID != 0 && mp.DeptID != 4 && mp.DeptID != 5 && mp.DeptID != 18 && mp.DeptID != 7
                        || mpD.VerDate == 0 && mpD.ProjectID == 0 && mp.DeptID == 4 && mp.Approval == 1 && mp.Status == 1 && mp.RowStatus == 1
                        || mpD.VerDate == 0 && mpD.ProjectID == 0 && mp.DeptID == 5 && mp.Approval == 1 && mp.Status == 1 && mp.RowStatus == 1
                        || mpD.VerDate == 0 && mpD.ProjectID == 0 && mp.DeptID == 18 && mp.Approval == 1 && mp.Status == 1 && mp.RowStatus == 1
                        || mpD.VerDate == 0 && mpD.ProjectID == 0 && mp.DeptID != 4 && mp.DeptID != 5 && mp.DeptID != 18
                        || mpD.VerDate == 1 && mpD.ProjectID == 0)
                    {
                        bntApprove.Enabled = false;
                    }
                    else if (mpD.VerDate == 0 && mpD.ProjectID == 0 && mp.DeptID == 4 && mp.Approval == 1 && mp.Status == 0 && mp.RowStatus == 0 && CekEstimasiMaterial > 0
                        || mpD.VerDate == 0 && mpD.ProjectID == 0 && mp.DeptID == 5 && mp.Approval == 1 && mp.Status == 0 && mp.RowStatus == 0 && CekEstimasiMaterial > 0
                        || mpD.VerDate == 0 && mpD.ProjectID == 0 && mp.DeptID == 18 && mp.Approval == 1 && mp.Status == 0 && mp.RowStatus == 0 && CekEstimasiMaterial > 0
                        || (mpD.VerDate == 0 && mpD.ProjectID != 0 && mp.DeptID == 18 || mp.DeptID == 5 || mp.DeptID == 4) && CekEstimasiMaterial > 0
                        || mpD.VerDate == 0 && mpD.ProjectID != 0 && mp.DeptID == 4 && CekEstimasiMaterial > 0
                        || mpD.VerDate == 0 && mpD.ProjectID != 0 && mp.DeptID == 5 && CekEstimasiMaterial > 0
                        || mpD.VerDate == 1 && mpD.ProjectID != 0 && CekEstimasiMaterial > 0)
                    {
                        bntApprove.Enabled = true;
                    }

                    LoadEstimasiMaterial(mp.ID);
                }

                else if (LevelApp == 3) // PM
                {
                    //Users user = (Users)Session["Users"];
                    Session["ApvPM"] = mp.VerPM;
                    Session["VerDate"] = mp.VerUser;
                    Session["ToDate"] = mp.ToDate;
                    Session["Biaya"] = mp.Biaya;
                    Session["Nomor"] = mp.Nomor;
                    Session["DeptName"] = mp.DeptName;
                    Session["NamaProject"] = mp.NamaProject;


                    if (mp.Approval == 2 && mp.Status == 1 && mp.RowStatus == 1 && mp.VerPM == 1 && mp.VerUser == 1)
                    {
                        txtStatusApproval.Text = "Approved - Oleh :" + " " + mp.LastModifiedBy2.Trim();
                        LabelInfo.Visible = false;
                        btnApproveEstCancel.Visible = true; btnApproveEst.Visible = false;
                        btnCancel.Visible = false; bntApprove.Visible = true;
                        btnTargetTujuan.Visible = false; btnTargetTujuanCancel.Visible = false;
                    }
                    else if (mp.Approval == 4 && mp.Status == 2 && mp.RowStatus == 2 && mp.VerPM == 2 && mp.VerUser == 1
                        || mp.Approval == 3 && mp.Status == 2 && mp.RowStatus == 2 && mp.VerPM == 2 && mp.VerUser == 1)
                    {
                        txtStatusApproval.Text = "Approved Tahap 2 - Oleh :" + " " + mp.LastModifiedBy2.Trim();
                        LabelInfo.Visible = true; LabelInfo.Text = "Pekerjaan sudah di Serah Terimakan Ke Manager Peminta ";
                        btnTargetTujuan.Visible = false; btnTargetTujuanCancel.Visible = true;
                        bntApprove.Visible = true; btnCancel.Visible = false;
                        btnApproveEstCancel.Visible = false; btnApproveEst.Visible = false;
                    }

                    txtNoImprovement.Text = mp.Nomor.ToUpper();
                    txtTanggal.Text = Convert.ToDateTime(mp.FromDate2).ToString("dd-MM-yyyy");
                    txtNamaImprovement.Text = mp.NamaProject.ToString();
                    txtDeptPemohon.Text = mp.DeptName.ToString();
                    txtGroupName.Text = mp.GroupName;
                    txtSasaran.Text = mp.Sasaran;

                    if (mp.ProdLine == 99) { txtArea.Text = "General"; }
                    else if (mp.ProdLine == 97) { txtArea.Text = "Material Preparation"; }
                    else if (mp.ProdLine == 98) { txtArea.Text = "WTP"; }
                    else if (mp.ProdLine == 1) { txtArea.Text = "Zona 1" + "-" + mp.Zona; }
                    else if (mp.ProdLine == 2) { txtArea.Text = "Zona 2" + "-" + mp.Zona; }
                    else if (mp.ProdLine == 3) { txtArea.Text = "Zona 3" + "-" + mp.Zona; }
                    else if (mp.ProdLine == 4) { txtArea.Text = "Zona 4"; }

                    if (mp.ToDeptID == 19 || mp.ToDeptID == 0)
                    {
                        txtDept.Visible = true; txtDept.Text = "MAINTENANCE";
                        txtDept.Attributes["style"] = "color:white;";
                        txtDept.BackColor = System.Drawing.Color.Green;
                    }
                    else if (mp.ToDeptID == 7)
                    {
                        txtDept.Visible = true; txtDept.Text = "HRD GA";
                        txtDept.Attributes["style"] = "color:white; font-style:calibri";
                        txtDept.BackColor = System.Drawing.Color.Green;

                    }

                    txtTglFinish.Text = Convert.ToDateTime(mp.ToDate2).ToString("dd-MM-yyyy");
                    txtBiaya.Text = mp.Biaya.ToString("###,##0.#0");
                    txtID.Value = mp.ID.ToString();
                    txtStatus.Text = mp.Status.ToString();
                    txtApproval.Text = mp.Approval.ToString();
                    txtRowstatus.Text = mp.RowStatus.ToString();
                    txtDetailSasaran.Text = mp.DetailSasaran.ToString();
                    txtQty.Text = Convert.ToDecimal(mp.Quantity).ToString();
                    txtSatuan.Text = mp.UomCode.ToString();
                    txtPembuat.Text = mp.NamaHead;
                    LoadEstimasiMaterial(mp.ID);

                    bntApprove.Enabled = true;
                }

                else if (mpD.Biaya > decimal.Parse(maxBiaya) && LevelApp == 4) // Direksi
                {
                    Session["ApvPM"] = mp.VerPM;
                    Session["Biaya"] = mp.Biaya;

                    txtNoImprovement.Text = mp.Nomor.ToUpper();
                    txtTanggal.Text = Convert.ToDateTime(mp.FromDate2).ToString("dd-MM-yyyy");
                    txtNamaImprovement.Text = mp.NamaProject.ToString();
                    txtDeptPemohon.Text = mp.DeptName.ToString();
                    txtGroupName.Text = mp.GroupName;
                    txtSasaran.Text = mp.Sasaran;

                    if (mp.ProdLine == 99) { txtArea.Text = "General"; }
                    else if (mp.ProdLine == 97) { txtArea.Text = "Material Preparation"; }
                    else if (mp.ProdLine == 98) { txtArea.Text = "WTP"; }
                    else if (mp.ProdLine == 1) { txtArea.Text = "Zona 1" + "-" + mp.Zona; }
                    else if (mp.ProdLine == 2) { txtArea.Text = "Zona 2" + "-" + mp.Zona; }
                    else if (mp.ProdLine == 3) { txtArea.Text = "Zona 3" + "-" + mp.Zona; }
                    else if (mp.ProdLine == 4) { txtArea.Text = "Zona 4"; }

                    if (mp.Approval == 2 && mp.Status == 2 && mp.RowStatus == 0 && mp.VerPM == 3 && mp.VerUser == 1)
                    {
                        txtStatusApproval.Text = "Approved - Oleh :" + " " + mp.LastModifiedBy2.Trim();
                        LabelInfo.Visible = false;
                        btnApproveEstCancel.Visible = true; btnApproveEst.Visible = false;
                        btnCancel.Visible = false; bntApprove.Visible = true;
                        btnTargetTujuan.Visible = false; btnTargetTujuanCancel.Visible = false;
                    }

                    if (mp.ToDeptID == 19 || mp.ToDeptID == 0)
                    {
                        txtDept.Visible = true; txtDept.Text = "MAINTENANCE";
                        txtDept.Attributes["style"] = "color:white;";
                        txtDept.BackColor = System.Drawing.Color.Green;
                    }
                    else if (mp.ToDeptID == 7)
                    {
                        txtDept.Visible = true; txtDept.Text = "HRD GA";
                        txtDept.Attributes["style"] = "color:white; font-style:calibri";
                        txtDept.BackColor = System.Drawing.Color.Green;
                    }

                    txtTglFinish.Text = Convert.ToDateTime(mp.ToDate2).ToString("dd-MM-yyyy");
                    txtBiaya.Text = mp.Biaya.ToString("###,##0.#0");
                    txtID.Value = mp.ID.ToString();
                    txtStatus.Text = mp.Status.ToString();
                    txtApproval.Text = mp.Approval.ToString();
                    txtRowstatus.Text = mp.RowStatus.ToString();
                    txtDetailSasaran.Text = mp.DetailSasaran.ToString();
                    txtQty.Text = Convert.ToDecimal(mp.Quantity).ToString();
                    txtSatuan.Text = mp.UomCode.ToString();
                    txtPembuat.Text = mp.NamaHead;
                    LoadEstimasiMaterial(mp.ID);

                    bntApprove.Enabled = true;
                }
            }
            #endregion

            #region sudah ada dan sudah approval
            else if (mp.Approval > 0 && mp.Status > 0 && mp.RowStatus > 0 && mp.Biaya == 0)
            {
                MTC_ProjectFacade_Rev1 ProjectFacade2 = new MTC_ProjectFacade_Rev1();
                MTC_Project_Rev1 mpD2 = new MTC_Project_Rev1();
                string NomorProject = Session["NoProject"].ToString();
                //int CekEstimasiMaterial = ProjectFacade2.CekData(NomorProject);         
                Session["ApvPM"] = mp.VerPM;
                Session["VerDate"] = mp.VerUser;
                Session["ToDate"] = mp.ToDate;

                txtNoImprovement.Text = mp.Nomor.ToUpper();
                txtTanggal.Text = Convert.ToDateTime(mp.FromDate2).ToString("dd-MM-yyyy");
                txtNamaImprovement.Text = mp.NamaProject.ToString();
                txtDeptPemohon.Text = mp.DeptName.ToString();
                txtGroupName.Text = mp.GroupName;
                txtSasaran.Text = mp.Sasaran;

                if (mp.Approval == 1 && mp.Status == 0 && mp.RowStatus == 0)
                {
                    txtStatusApproval.Text = "DiBuat - Oleh :" + " " + mp.NamaHead.Trim();
                    LabelInfo.Visible = true; LabelInfo.Text = "IMPROVMENT KE HRD GA";
                    LabelInfo2.Visible = false;
                }
                else if (mp.Biaya == 0 && mp.ToDate.ToString() != "11/11/2000 0:00:00" && mp.Noted1 == 1)
                {
                    bntApprove.Enabled = true; btnCancel.Visible = false;
                    LabelInfo.Visible = true; LabelInfo.Text = "Biaya tidak ada karena menggunakan brg bekas !!";
                    LabelInfo2.Visible = false;
                }

                else
                {
                    LabelInfo.Visible = false; LabelInfo2.Visible = false;
                }

                if (mp.ToDeptID == 7)
                {
                    txtDept.Visible = true; txtDept.Text = "HRD GA";
                    txtDept.Attributes["style"] = "color:white; font-style:calibri";
                    txtDept.BackColor = System.Drawing.Color.Green;
                    //LabelInfo.Visible = false; LabelInfo2.Visible = false;
                    bntApprove.Enabled = true;
                }

                if (mp.ProdLine == 99) { txtArea.Text = "General"; }
                else if (mp.ProdLine == 97) { txtArea.Text = "Material Preparation"; }
                else if (mp.ProdLine == 98) { txtArea.Text = "WTP"; }
                else if (mp.ProdLine == 1) { txtArea.Text = "Zona 1" + "-" + mp.Zona; }
                else if (mp.ProdLine == 2) { txtArea.Text = "Zona 2" + "-" + mp.Zona; }
                else if (mp.ProdLine == 3) { txtArea.Text = "Zona 3" + "-" + mp.Zona; }
                else if (mp.ProdLine == 4) { txtArea.Text = "Zona 4"; }

                txtTglFinish.Text = Convert.ToDateTime(mp.ToDate2).ToString("dd-MM-yyyy");
                txtBiaya.Text = mp.Biaya.ToString("###,##0.#0");
                txtID.Value = mp.ID.ToString();
                txtStatus.Text = mp.Status.ToString();
                txtApproval.Text = mp.Approval.ToString();
                txtRowstatus.Text = mp.RowStatus.ToString();
                txtDeptID.Text = mp.DeptID.ToString();
                txtDetailSasaran.Text = mp.DetailSasaran.ToString();
                txtQty.Text = Convert.ToDecimal(mp.Quantity).ToString();
                txtSatuan.Text = mp.UomCode.ToString();
                txtPembuat.Text = mp.NamaHead;

                string Noted = mp.Noted1.ToString();
                if (Noted == "1")
                {
                    RBBekas.Visible = true; RBBekas.Checked = true;
                }

                LoadEstimasiMaterial(mp.ID);

            }
            #endregion
        }

        private void LoadEstimasiMaterial(int ProjectID)
        {
            ArrayList arrData = new ArrayList();
            MTC_ProjectFacade_Rev1 MPF = new MTC_ProjectFacade_Rev1();
            arrData = MPF.RetrieveEstimasiMaterial(ProjectID);
            lstMaterial.DataSource = arrData;
            lstMaterial.DataBind();
        }

        //protected void btnPrev_Click(object sender, EventArgs e)
        //{
        //    ViewState["counter"] = (int)ViewState["counter"] - 1;
        //    int Counter = (int)ViewState["counter"];
        //    LoadOpenImprovement(Counter);
        //}

        protected void bntApprove_Click(object sender, EventArgs e)
        {
            Users user = (Users)Session["Users"];

            DateTime ToDate3 = Convert.ToDateTime(Session["ToDate"]);
            int APvPM = Convert.ToInt32(Session["ApvPM"]);
            decimal Biaya = Convert.ToDecimal(Session["Biaya"]);

            string maxBiaya = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("EstimasiLevel", "EngineeringNew");
            ViewState["counter"] = (int)ViewState["counter"] + 1;
            int Counter = (int)ViewState["counter"];
            string MinApproval = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("EstimasiLevel", "EngineeringNew");
            string[] App = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ApvPengajuan", "EngineeringNew").Split(',');
            int LevelApp = Array.IndexOf(App, ((Users)Session["Users"]).ID.ToString());
            MTC_ProjectFacade_Rev1 mpp = new MTC_ProjectFacade_Rev1();
            MTC_Project_Rev1 mp = new MTC_Project_Rev1();
            int Approv = 0;

            if (txtApproval.Text != "0")
            {
                if (txtStatus.Text == "0" && txtRowstatus.Text == "0" && txtApproval.Text == "1"
                    && (txtDeptID.Text == "4" || txtDeptID.Text == "5" || txtDeptID.Text == "18" || txtDeptID.Text == "19"))
                { mp.RowStatus = 1; mp.Approval = 1; mp.Status = 0; }

                else if (txtStatus.Text == "0" && txtRowstatus.Text == "0" && txtApproval.Text == "2"
                    && (txtDeptID.Text == "4" || txtDeptID.Text == "5" || txtDeptID.Text == "18" || txtDeptID.Text == "19"))
                { mp.RowStatus = 1; mp.Approval = 2; mp.Status = 1; }

                // Approval Manager Dept
                else if (txtStatus.Text == "0" && txtRowstatus.Text == "0" && txtApproval.Text == "1")
                { mp.RowStatus = 1; mp.Approval = Int32.Parse(txtApproval.Text); mp.FinishDate = Convert.ToDateTime("11/11/2000 4:00:00"); }

                // Approval Manager Dept , Deal target selesai          
                else if (txtStatus.Text == "1" && txtRowstatus.Text == "1" && txtApproval.Text == "1")
                {
                    if (LevelApp == 1 || LevelApp == 0)
                    {
                        mp.RowStatus = 1; mp.Approval = 1; mp.Status = 1; mp.VerPM = APvPM; mp.VerDate = 0; mp.ApvDir = 0;
                    }
                    else if (LevelApp < 0)
                    {
                        mp.VerDate = 1; mp.RowStatus = 1; mp.Approval = 1; mp.Status = 1; mp.VerPM = APvPM; mp.ApvDir = 0;
                    }
                    else if (LevelApp == 2 || LevelApp == 5)
                    {
                        if (APvPM == -1)
                        {
                            mp.VerDate = 2; mp.RowStatus = 2; mp.Approval = 2; mp.Status = 2; mp.VerPM = 1; mp.FinishDate = ToDate3; mp.ApvDir = 0;
                        }
                        else if (APvPM == -2)
                        {
                            mp.VerDate = 2; mp.RowStatus = 2; mp.Approval = 2; mp.Status = 2; mp.VerPM = 2; mp.FinishDate = ToDate3; mp.ApvDir = 0;
                        }
                        else if (APvPM == 1)
                        {
                            mp.VerDate = 1; mp.RowStatus = 1; mp.Approval = 2; mp.Status = 1; mp.VerPM = 1; mp.FinishDate = ToDate3; mp.ApvDir = 0;
                        }
                        else
                        {
                            mp.VerDate = 1; mp.RowStatus = 2; mp.Approval = 2; mp.Status = 2; mp.VerPM = APvPM; mp.FinishDate = ToDate3; mp.ApvDir = 0;
                        }
                    }
                }

                else if (txtStatus.Text == "1" && txtRowstatus.Text == "1" && txtApproval.Text == "2" && APvPM == -1)
                { mp.VerDate = 1; mp.RowStatus = 1; mp.Approval = 2; mp.Status = 1; mp.VerPM = 1; mp.FinishDate = ToDate3; mp.ApvDir = 0; }

                else if (txtStatus.Text == "1" && txtRowstatus.Text == "1" && txtApproval.Text == "2" && APvPM == -2)
                { mp.VerDate = 1; mp.RowStatus = 1; mp.Approval = 2; mp.Status = 1; mp.VerPM = 2; mp.FinishDate = ToDate3; mp.ApvDir = 0; }

                //ApV PM

                //Lama
                //else if (txtStatus.Text == "1" && txtRowstatus.Text == "1" && txtApproval.Text == "2")
                //{ mp.VerDate = 1; mp.Status = 2; mp.RowStatus = 0; mp.Approval = 2; }

                // Apv PM Baru Filter Pertama
                else if (txtStatus.Text == "0" && txtRowstatus.Text == "1" && txtApproval.Text == "1" && APvPM == 0)
                { mp.VerPM = 1; mp.Status = 0; mp.RowStatus = 1; mp.Approval = 1; mp.ApvDir = 0; }

                // Apv PM Baru Filter Kedua ( Realese )
                else if (txtStatus.Text == "1" && txtRowstatus.Text == "1" && txtApproval.Text == "2" && APvPM == 1)
                //{ mp.VerPM = 2; mp.Status = 2; mp.RowStatus = 2; mp.Approval = 2; mp.VerDate = 1; mp.FinishDate = ToDate3; }
                {
                    if (Biaya > Convert.ToDecimal(maxBiaya))
                    {
                        mp.VerPM = 2; mp.Status = 2; mp.RowStatus = 2; mp.Approval = 2; mp.VerDate = 1; mp.FinishDate = ToDate3; mp.Flag = 0; mp.ApvDir = 0;
                    }
                    else if (Biaya < Convert.ToDecimal(maxBiaya))
                    {
                        mp.VerPM = 2; mp.Status = 2; mp.RowStatus = 2; mp.Approval = 2; mp.VerDate = 1; mp.FinishDate = ToDate3; mp.Flag = 1; mp.ApvDir = 0;
                    }
                }


                // Apv PM Baru Filter Ketiga ( Closed )
                else if (txtStatus.Text == "2" && txtRowstatus.Text == "2" && txtApproval.Text == "4" && APvPM == 2
                    || txtStatus.Text == "2" && txtRowstatus.Text == "2" && txtApproval.Text == "3" && APvPM == 2)
                {
                    if (Biaya > Convert.ToDecimal(maxBiaya))
                    {
                        mp.ApvDir = 1;
                    }
                    else
                    {
                        mp.ApvDir = 0;
                    }
                    mp.VerPM = 3; mp.Status = 2; mp.RowStatus = 2; mp.Approval = 5; mp.VerDate = 1;
                    mp.FinishDate = ToDate3; mp.Flag = 1;
                }

                // Apv Direksi
                else if (txtStatus.Text == "2" && txtRowstatus.Text == "2" && txtApproval.Text == "2" && APvPM == 2)
                {
                    if (Biaya > Convert.ToDecimal(maxBiaya))
                    {
                        mp.VerPM = 2; mp.Status = 2; mp.RowStatus = 2; mp.Approval = 2; mp.VerDate = 1;
                        mp.FinishDate = ToDate3; mp.Flag = 1; mp.ApvDir = 1;
                    }
                }

                // Apv Head Eng
                else if (txtStatus.Text == "0" && txtRowstatus.Text == "1" && txtApproval.Text == "1" && APvPM != 0)
                {
                    if (txtBiaya.Text == "0,00" && txtTglFinish.Text == "Target belum ditentukan" && RBBekas.Checked == false)
                    {
                        DisplayAJAXMessage(this, "Estimasi Biaya dan Target Finish Date harus diisi !!");
                        return;
                    }

                    else if (txtTglFinish.Text != "Target belum ditentukan" && txtBiaya.Text == "0,00" && RBBekas.Checked == false)
                    {
                        DisplayAJAXMessage(this, "Estimasi Biaya harus di isi !!");
                        return;
                    }
                    else if (txtTglFinish.Text == "Target belum ditentukan" && txtBiaya.Text != "0,00" && RBBekas.Checked == false)
                    {
                        DisplayAJAXMessage(this, "Target Finish Date harus di isi !!");
                        return;
                    }
                    else if (txtTglFinish.Text == "Target belum ditentukan" && txtBiaya.Text == "0,00" && RBBekas.Checked == true)
                    {
                        DisplayAJAXMessage(this, "Target Finish Date harus di isi !!");
                        return;
                    }
                    mp.Status = 1; mp.RowStatus = Int32.Parse(txtRowstatus.Text); mp.Approval = Int32.Parse(txtApproval.Text);
                    mp.VerPM = APvPM; mp.VerDate = 0; mp.ApvDir = 0;
                }


                else if (Convert.ToDecimal(txtBiaya.Text) > Convert.ToDecimal(MinApproval))
                { mp.VerDate = 1; mp.Status = 2; mp.RowStatus = 2; mp.Approval = 2; }


                //else if (txtStatus.Text == "0" && txtRowstatus.Text == "1" && txtApproval.Text == "1")
                //{ mp.Status = 1; mp.RowStatus = Int32.Parse(txtRowstatus.Text); mp.Approval = Int32.Parse(txtApproval.Text); }

                else
                { mp.Approval = 2; mp.Status = 2; mp.RowStatus = 2; mp.ApvDir = 0; }
            }

            //if (Convert.ToDateTime(txtTglFinish.Text) == Convert.ToDateTime("11/11/2000"))
            //{
            //    mp.ID = int.Parse(txtID.Value);
            //    mp.LastModifiedBy = ((Users)Session["Users"]).UserID;
            //    mp.Biaya = Convert.ToDecimal(txtBiaya.Text);
            //    mp.FinishDate = Convert.ToDateTime(txtTglFinish.Text);
            //}
            //else
            //{
            //    mp.ID = int.Parse(txtID.Value);
            //    mp.LastModifiedBy = ((Users)Session["Users"]).UserID;
            //    mp.Biaya = Convert.ToDecimal(txtBiaya.Text);
            //    mp.FinishDate = Convert.ToDateTime(txtTglFinish.Text);
            //}

            mp.ID = int.Parse(txtID.Value);
            mp.LastModifiedBy = ((Users)Session["Users"]).UserName;
            mp.Biaya = Convert.ToDecimal(txtBiaya.Text);

            if (mp.Approval == 1 && mp.RowStatus == 1 && mp.Status == 1 && mp.VerPM == 1 && mp.VerDate == 0)
            {
                string Target = Convert.ToDateTime(txtTglFinish.Text).ToString("MM/dd/yyyy");
                //mp.FinishDate = Convert.ToDateTime(txtTglFinish.Text);
                //mp.FinishDate = Convert.ToDateTime(Target);
                mp.FinishDate = DateTime.Parse(txtTglFinish.Text);
            }
            else if (mp.Approval == 1 && mp.RowStatus == 1 && mp.Status == 1 && mp.VerPM == 1 && mp.VerDate == 1)
            {
                mp.FinishDate = ToDate3;
            }
            else if (mp.Approval == 2 && mp.RowStatus == 1 && mp.Status == 1 && mp.VerPM == 1
                || mp.Approval == 2 && mp.RowStatus == 2 && mp.Status == 2 && mp.VerPM == 2
                || mp.Approval == 5 && mp.RowStatus == 2 && mp.Status == 2 && mp.VerPM == 3)
            {
                mp.FinishDate = ToDate3;
            }
            else
            {
                mp.FinishDate = Convert.ToDateTime("11/11/2000 00:00:00");
            }

            if (RBBekas.Checked == true)
            {
                int Noted = 1; Session["Noted"] = Noted;
            }
            else if (RBBekas.Checked == false)
            {
                int Noted = 0; Session["Noted"] = Noted;
            }

            mp.Noted1 = Convert.ToInt32(Session["Noted"]);
            int result = mpp.Approval(mp);

            if (result > -1)
            {
                if (mp.Approval == 5)
                {
                    mp.Statuse = "Diketahui";
                    mp.ProjectID = int.Parse(txtID.Value);
                    mp.CreatedBy = ((Users)Session["Users"]).UserName;
                    int rst = mpp.InsertLog(mp);
                }

                Response.Redirect("ApprovalProject_Rev1.aspx");
                txtDeptPemohon.Text = string.Empty;
                txtNamaImprovement.Text = string.Empty;
                txtSasaran.Text = string.Empty;
                txtDetailSasaran.Text = string.Empty;
                txtTglFinish.Text = string.Empty;
                txtArea.Text = string.Empty;
                txtGroupName.Text = string.Empty;
                txtTanggal.Text = string.Empty;

                if (txtStatus.Text == "2" && txtRowstatus.Text == "2" && txtApproval.Text == "2" && APvPM == 2 && Biaya > Convert.ToDecimal(maxBiaya))
                {
                    //KirimEmail();
                }
            }
        }

        public void KirimEmail()
        {
            string Nomor = Session["Nomor"].ToString();
            string NamaDept = Session["DeptName"].ToString();
            string NamaProject = Session["NamaProject"].ToString();

            MailMessage mail = new MailMessage();
            SmtpClient Smtp = new SmtpClient();
            mail.From = new MailAddress("system_support@grcboard.com", "Improvement -System Support-");
            mail.To.Add(new MailAddress("uum.umiyati@grcboard.com"));
            //mail.To.Add(new MailAddress("beny@grcboard.com")); 
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;


            mail.Subject = "Permohonan Approval Improvement Maintenance";
            mail.Body += "Mohon Approval untuk Improvement di bawah ini : \n\r\n\r";
            mail.Body += "Nomor Improvment : " + Nomor.Trim() + "\n\r";
            mail.Body += "Nama Improvment  : " + NamaProject.Trim() + "\n\r";
            mail.Body += "Dept. Peminta    : " + NamaDept.Trim() + "\n\r";

            mail.Body += "Terima Kasih, \n\r\n\r\n\r";
            mail.Body += "PT. BANGUNPERKASA ADHITAMASENTRA \n\r";
            mail.Body += "- Ahlinya Papan Semen - \n\r";


            Smtp.Host = "mail.grcboard.com";
            Smtp.Port = 587;
            System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
            NetworkCred.UserName = "system_support@grcboard.com";
            NetworkCred.Password = "grc123!@#";
            Smtp.EnableSsl = true;
            Smtp.UseDefaultCredentials = false;
            Smtp.Credentials = NetworkCred;
            Smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                Smtp.Send(mail);
            }

            catch (Exception ex)
            { }
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            ViewState["counter"] = (int)ViewState["counter"] - 1;
            int Counter = (int)ViewState["counter"];
            //LoadOpenImprovement(Counter);

            //if (Counter < int.Parse(txtJmlData.Value)-1)
            if (Counter > 0)
            {
                LoadOpenImprovement(Counter);
                btnPrev.Enabled = true;
                btnNext.Enabled = true;
            }
            //else if (Counter >= int.Parse(txtJmlData.Value)-1)
            else if (Counter == 0)
            {
                LoadOpenImprovement(Counter);
                btnPrev.Enabled = false;
                btnNext.Enabled = true;
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            ViewState["counter"] = (int)ViewState["counter"] + 1;
            int Counter = (int)ViewState["counter"];

            if (Counter < int.Parse(txtJmlData.Value) - 1)
            {
                LoadOpenImprovement(Counter);
                btnPrev.Enabled = true;
                btnNext.Enabled = true;
            }
            else if (Counter >= int.Parse(txtJmlData.Value) - 1)
            {
                LoadOpenImprovement(Counter);
                btnPrev.Enabled = true;
                btnNext.Enabled = false;
            }
            //else 
            //{
            //    LoadOpenImprovement();
            //    btnNext.Enabled = false;
            //}
        }
        //protected void btnUpdate_Click(object sender, EventArgs e)
        //{
        //    MTC_Project mtc2 = new MTC_Project();
        //    MTC_ProjectFacade mtcFacade2 = new MTC_ProjectFacade();
        //    //txtID.Value = Convert.ToInt32(mtc2.ID).ToString();
        //    mtc2.ID = int.Parse(txtID.Value);
        //    mtc2.FinishDate = Convert.ToDateTime(txtTglFinish.Text);
        //    mtc2.Biaya = Convert.ToDecimal(txtBiaya.Text);
        //    int result = 0;
        //    result = mtcFacade2.UpdateProject(mtc2);
        //}
        public static void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //ArrayList arrP = new ArrayList();
            //Session["Cancel"] = "no";
            //Session["SubPj"] = "";
            //Session["StProject"] = " and Status >-1 ";
            //Session["Search"] = " " + btnCari.Text + " ";
            //arrP = new MTC_ProjectFacade().Retrieve();
            Session["FlagSearch"] = 2;
            Session["NoProject"] = txtCari.Text;
            //LoadOpenImprovement();

            LoadOpenImprovement();

            btnNext.Enabled = false;
            btnPrev.Enabled = false;
            btnCancel.Enabled = false;

        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApprovalProject_Rev1.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string MinApproval = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("EstimasiLevel", "EngineeringNew");
            string[] App = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ApvPengajuan", "EngineeringNew").Split(',');
            int LevelApp = Array.IndexOf(App, ((Users)Session["Users"]).ID.ToString());
            Users user = (Users)Session["Users"];

            if (LevelApp < 0)
            {
                string CancelBy = "CancelBy-" + user.UserName.Trim(); Session["CancelBy"] = CancelBy;
            }
            else if (LevelApp == 3)
            {
                string CancelBy = "NotApprovedBy-" + user.UserName.Trim(); Session["CancelBy"] = CancelBy;
            }
            else if (LevelApp == 1)
            {
                string CancelBy = "CancelBy-" + user.UserName.Trim(); Session["CancelBy"] = CancelBy;
            }

            MTC_ProjectFacade_Rev1 mppC = new MTC_ProjectFacade_Rev1();
            MTC_Project_Rev1 mdC = new MTC_Project_Rev1();

            int ID = Convert.ToInt32(txtID.Value);

            mdC.LastModifiedBy = Session["CancelBy"].ToString();
            mdC.ID = ID;

            int intResult = 0;
            intResult = mppC.CancelProject(mdC);

            if (intResult > -1)
            {
                Response.Redirect("ApprovalProject_Rev1.aspx");
                txtDeptPemohon.Text = string.Empty;
                txtNamaImprovement.Text = string.Empty;
                txtSasaran.Text = string.Empty;
                txtDetailSasaran.Text = string.Empty;
                txtTglFinish.Text = string.Empty;
                txtArea.Text = string.Empty;
                txtGroupName.Text = string.Empty;
                txtTanggal.Text = string.Empty;
            }

        }

        protected void btnRe_Click(object sender, EventArgs e)
        {
            string MinApproval = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("EstimasiLevel", "EngineeringNew");
            string[] App = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ApvPengajuan", "EngineeringNew").Split(',');
            int LevelApp = Array.IndexOf(App, ((Users)Session["Users"]).ID.ToString());
            Users user = (Users)Session["Users"];

            MTC_ProjectFacade_Rev1 mppC1 = new MTC_ProjectFacade_Rev1();
            MTC_Project_Rev1 mdC1 = new MTC_Project_Rev1();

            int ID = Convert.ToInt32(txtID.Value);
            mdC1.ID = ID;
            mdC1.LastModifiedBy = user.UserName;
            int intResult = 0;

            intResult = mppC1.ReSchProject(mdC1);
            if (intResult > -1)
            {
                Response.Redirect("ApprovalProject_Rev1.aspx");
            }

        }

        protected void btnApproveEst_Click(object sender, EventArgs e)
        {
            MTC_ProjectFacade_Rev1 mppC2 = new MTC_ProjectFacade_Rev1();
            MTC_Project_Rev1 mdC2 = new MTC_Project_Rev1();

            int APvPM = Convert.ToInt32(Session["ApvPM"]);

            if (APvPM == 1)
            {
                mdC2.VerPM = 2;
            }
        }

        protected void btnTargetTujuan_Click(object sender, EventArgs e)
        { }

        protected void btnApproveEstCancel_Click(object sender, EventArgs e)
        {
            Users user = (Users)Session["Users"];

            MTC_ProjectFacade_Rev1 mppC3 = new MTC_ProjectFacade_Rev1();
            MTC_Project_Rev1 mdC3 = new MTC_Project_Rev1();

            int ID = Convert.ToInt32(txtID.Value);
            mdC3.LastModifiedBy = user.UserName.Trim();
            mdC3.ID = ID;

            int intResult = 0;
            intResult = mppC3.CancelProjectPM2(mdC3);

            if (intResult > -1)
            {
                Response.Redirect("ApprovalProject_Rev1.aspx");

                txtDeptPemohon.Text = string.Empty;
                txtNamaImprovement.Text = string.Empty;
                txtSasaran.Text = string.Empty;
                txtDetailSasaran.Text = string.Empty;
                txtTglFinish.Text = string.Empty;
                txtArea.Text = string.Empty;
                txtGroupName.Text = string.Empty;
                txtTanggal.Text = string.Empty;
            }
        }

        protected void btnTargetTujuanCancel_Click(object sender, EventArgs e)
        {
            Users user = (Users)Session["Users"];

            MTC_ProjectFacade_Rev1 mppC4 = new MTC_ProjectFacade_Rev1();
            MTC_Project_Rev1 mdC4 = new MTC_Project_Rev1();

            int ID = Convert.ToInt32(txtID.Value);
            mdC4.LastModifiedBy = user.UserName.Trim();
            mdC4.ID = ID;

            int intResult = 0;
            intResult = mppC4.CancelProjectPM3(mdC4);

            if (intResult > -1)
            {
                Response.Redirect("ApprovalProject_Rev1.aspx");

                txtDeptPemohon.Text = string.Empty;
                txtNamaImprovement.Text = string.Empty;
                txtSasaran.Text = string.Empty;
                txtDetailSasaran.Text = string.Empty;
                txtTglFinish.Text = string.Empty;
                txtArea.Text = string.Empty;
                txtGroupName.Text = string.Empty;
                txtTanggal.Text = string.Empty;
            }
        }

        protected void RBBekas_CheckedChanged(object sender, EventArgs e)
        { }
    }
}