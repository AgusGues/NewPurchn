using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BusinessFacade;
using DataAccessLayer;
using Domain;

namespace GRCweb1.Modul.Purchasing
{
    public partial class SerahTerimaAsset : System.Web.UI.Page
    {
        decimal TotalQty = 0; decimal TotalM3 = 0; decimal GrandTotal = 0; decimal GrandTotalM3 = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadAsset();

            }
        }
        private void LoadAsset()
        {
            Users user = ((Users)Session["Users"]);
            AssetFacadeSerah Fasset = new AssetFacadeSerah();
            AssetDomainSerah Dasset = new AssetDomainSerah();

            AssetFacadeSerah Fasset01 = new AssetFacadeSerah();
            AssetDomainSerah Dasset01 = new AssetDomainSerah();

            #region Dept Project Sipil atau RnD
            if (user.DeptID == 22 || user.DeptID == 30 || user.DeptID == 19 || user.DeptID == 25 || user.DeptID == 4)
            {
                if (user.Apv == 0)
                {
                    ArrayList arrData = Fasset.RetrieveNamaAsset();
                    Session["DataProject"] = arrData;
                    ddlNamaProject.Items.Clear();
                    ddlNamaProject.Items.Add(new ListItem("-- Pilih Nama Asset --", "0"));
                    if (arrData.Count == 0) return;

                    foreach (AssetDomainSerah mpo in arrData)
                    {
                        ddlNamaProject.Items.Add(new ListItem(mpo.ItemCode + " - " + mpo.ItemName, mpo.ItemName));
                    }

                    /** Tidak di pakai lagi **/
                    //Dasset01 = Fasset01.RetrievePelaksana();

                    //if (Dasset01.Urut > 0)
                    //{
                    //    if (Dasset01.DeptID == 22 && Dasset01.Urut == 1)
                    //    {
                    //        ArrayList arrData = (user.DeptID == 22 || user.DeptID == 19) ? Fasset.RetrieveNamaAsset() : new ArrayList();
                    //        Session["DataProject"] = arrData;
                    //        ddlNamaProject.Items.Clear();
                    //        ddlNamaProject.Items.Add(new ListItem("-- Pilih Nama Asset --", "0"));
                    //        if (arrData.Count == 0) return;

                    //        foreach (AssetDomainSerah mpo in arrData)
                    //        {
                    //            ddlNamaProject.Items.Add(new ListItem(mpo.ItemCode + " - " + mpo.ItemName, mpo.ItemName));
                    //        }
                    //    }
                    //    else if (Dasset01.DeptID == 30 && Dasset01.Urut == 2 && Dasset01.Urut != 1)
                    //    {
                    //        ArrayList arrData = (user.DeptID == 30) ? Fasset.RetrieveNamaAsset() : new ArrayList();
                    //        Session["DataProject"] = arrData;
                    //        ddlNamaProject.Items.Clear();
                    //        ddlNamaProject.Items.Add(new ListItem("-- Pilih Nama Asset --", "0"));
                    //        //if (arrData.Count == 0) return;

                    //        foreach (AssetDomainSerah mpo in arrData)
                    //        {
                    //            ddlNamaProject.Items.Add(new ListItem(mpo.ItemCode + " - " + mpo.ItemName, mpo.ItemName));
                    //        }
                    //    }
                    //    else if (Dasset01.DeptID == 19 && Dasset01.Urut != 1 && Dasset01.Urut != 2)
                    //    {
                    //        ArrayList arrData = (user.DeptID == 19) ? Fasset.RetrieveNamaAsset() : new ArrayList();
                    //        Session["DataProject"] = arrData;
                    //        ddlNamaProject.Items.Clear();
                    //        ddlNamaProject.Items.Add(new ListItem("-- Pilih Nama Asset --", "0"));
                    //        if (arrData.Count == 0) return;

                    //        foreach (AssetDomainSerah mpo in arrData)
                    //        {
                    //            ddlNamaProject.Items.Add(new ListItem(mpo.ItemCode + " - " + mpo.ItemName, mpo.ItemName));
                    //        }
                    //    }
                    //}

                    //ArrayList arrData = (user.DeptID == 22 || user.DeptID == 30) ? Fasset.RetrieveNamaAsset() : new ArrayList();
                    //Session["DataProject"] = arrData;
                    //ddlNamaProject.Items.Clear();
                    //ddlNamaProject.Items.Add(new ListItem("-- Pilih Nama Asset --", "0"));
                    //if (arrData.Count == 0) return;               

                    //foreach (AssetDomainSerah mpo in arrData)
                    //{
                    //    ddlNamaProject.Items.Add(new ListItem(mpo.ItemCode + " - " + mpo.ItemName, mpo.ItemName));
                    //}
                }
                else if (user.Apv > 0)
                {

                    ArrayList arrData = Fasset.RetrieveNamaAsset2(user.DeptID);
                    Session["DataProject"] = arrData;
                    ddlNamaProject.Items.Clear();
                    ddlNamaProject.Items.Add(new ListItem("-- Pilih Nama Asset --", "0"));
                    //if (arrData.Count == 0) return;     
                    if (arrData.Count == 0)
                    {
                        ddlNamaProject.Items.Clear();
                        ddlNamaProject.Items.Add(new ListItem("-- Belum ada penyerahan Asset saat ini --", "0"));
                        ddlNamaProject.Enabled = false;
                        txtNomor.Enabled = false;
                        txtTglMulai.Enabled = false;
                        txtTglSelesai.Enabled = false;
                        txtBiaya.Enabled = false; txtStatus.Enabled = false; txtDeptPemohon.Enabled = false; txtNomor0.Enabled = false;
                    }
                    foreach (AssetDomainSerah mpo in arrData)
                    {
                        ddlNamaProject.Items.Add(new ListItem(mpo.ItemCode + " - " + mpo.ItemName, mpo.ItemName));
                    }
                }
            }
            #endregion
            #region Dept Pemilik Asset
            else if (user.DeptID != 22 && user.DeptID != 24 && user.DeptID != 30 && user.DeptID != 0 && user.DeptID != 4 && user.DeptID != 19 && user.DeptID != 25)
            {
                ArrayList arrData = Fasset.RetrieveNamaAsset3(user.DeptID);
                Session["DataProject"] = arrData;
                ddlNamaProject.Items.Clear();
                ddlNamaProject.Items.Add(new ListItem("-- Pilih Nama Asset --", "0"));
                if (arrData.Count == 0)
                {
                    ddlNamaProject.Items.Clear();
                    ddlNamaProject.Items.Add(new ListItem("-- Belum ada penyerahan Asset saat ini --", "0"));
                    ddlNamaProject.Enabled = false;
                    txtNomor.Enabled = false;
                    txtTglMulai.Enabled = false;
                    txtTglSelesai.Enabled = false;
                    txtBiaya.Enabled = false; txtStatus.Enabled = false; txtDeptPemohon.Enabled = false; txtNomor0.Enabled = false;
                }
                foreach (AssetDomainSerah mpo in arrData)
                {
                    ddlNamaProject.Items.Add(new ListItem(mpo.ItemCode + " - " + mpo.ItemName, mpo.ItemName));
                }
            }
            #endregion
            #region Dept Accounting
            else if (user.DeptID == 24)
            {

                if (user.Apv == 1 || user.Apv == 0 && user.DeptID == 24)
                {

                    //if (user.Apv == 0)
                    //{ btnApprove.Enabled = false; btnSerah.Visible = false; }
                    //else
                    //{ btnApprove.Enabled = true; btnSerah.Visible = false; }

                    ArrayList arrData = Fasset.RetrieveNamaAsset4(user.DeptID);
                    Session["DataProject"] = arrData;
                    ddlNamaProject.Items.Clear();
                    ddlNamaProject.Items.Add(new ListItem("-- Pilih Nama Asset --", "0"));
                    if (arrData.Count == 0)
                    {
                        ddlNamaProject.Items.Clear();
                        ddlNamaProject.Items.Add(new ListItem("-- Belum ada penyerahan Asset saat ini --", "0"));
                        ddlNamaProject.Enabled = false;
                        txtNomor.Enabled = false;
                        txtTglMulai.Enabled = false;
                        txtTglSelesai.Enabled = false;
                        txtBiaya.Enabled = false; txtStatus.Enabled = false; txtDeptPemohon.Enabled = false; txtNomor0.Enabled = false;
                    }
                    foreach (AssetDomainSerah mpo in arrData)
                    {
                        ddlNamaProject.Items.Add(new ListItem(mpo.ItemCode + " - " + mpo.ItemName, mpo.ItemName));
                    }
                }
                else if (user.Apv == 2)
                {
                    ArrayList arrData = Fasset.RetrieveNamaAsset5();
                    Session["DataProject"] = arrData;
                    ddlNamaProject.Items.Clear();
                    ddlNamaProject.Items.Add(new ListItem("-- Pilih Nama Asset --", "0"));
                    if (arrData.Count == 0)
                    {
                        ddlNamaProject.Items.Clear();
                        ddlNamaProject.Items.Add(new ListItem("-- Belum ada penyerahan Asset saat ini --", "0"));
                        ddlNamaProject.Enabled = false;
                        txtNomor.Enabled = false;
                        txtTglMulai.Enabled = false;
                        txtTglSelesai.Enabled = false;
                        txtBiaya.Enabled = false; txtStatus.Enabled = false; txtDeptPemohon.Enabled = false; txtNomor0.Enabled = false; btnApprove.Visible = true;
                    }
                    foreach (AssetDomainSerah mpo in arrData)
                    {
                        ddlNamaProject.Items.Add(new ListItem(mpo.ItemCode + " - " + mpo.ItemName, mpo.ItemName));
                    }
                }
            }
            #endregion


        }

        private void LoadKomponenAsset(string KodeAsset)
        {
            ArrayList arrData = new ArrayList();
            AssetFacadeSerah MPF = new AssetFacadeSerah();
            arrData = MPF.RetrieveKomponenAsset(KodeAsset);
            lstMaterial.DataSource = arrData;
            lstMaterial.DataBind();
        }

        //protected void ddlNamaProject_Change(object sender, EventArgs e)
        //{
        //    Users user = (Users)Session["Users"]; string query = string.Empty; string query2 = string.Empty;
        //    if (user.UnitKerjaID.ToString().Length == 1)
        //    {
        //        query = "13"; query2 = "8";
        //    }
        //    else
        //    {
        //        query = "14"; query2 = "9";
        //    }

        //    AssetDomainSerah DomainCekApv = new AssetDomainSerah();
        //    AssetFacadeSerah FacadeCekApv = new AssetFacadeSerah();
        //    DomainCekApv = FacadeCekApv.RetrieveCekApv(ddlNamaProject.SelectedItem.ToString().Trim().Substring(0,Convert.ToInt32(query)));      
        //    Session["AssetID"] = DomainCekApv.ID;
        //    Session["Upgrade"] = DomainCekApv.Upgrade;

        //    AssetDomainSerah mpD = new AssetDomainSerah();
        //    AssetFacadeSerah mpp = new AssetFacadeSerah();
        //    mpD = mpp.RetrieveDataAsset(ddlNamaProject.SelectedItem.ToString().Trim().Substring(0, Convert.ToInt32(query)));

        //    AssetDomainSerah mpD2 = new AssetDomainSerah();
        //    AssetFacadeSerah mpp2 = new AssetFacadeSerah();
        //    mpD2 = mpp2.RetrieveNamaDept(ddlNamaProject.SelectedItem.ToString().Trim().Substring(Convert.ToInt32(query2), 1));

        //    AssetDomainSerah mpD3 = new AssetDomainSerah();
        //    AssetFacadeSerah mpp3 = new AssetFacadeSerah();
        //    decimal TotalPrice = mpp3.RetrieveTotalPrice(ddlNamaProject.SelectedItem.ToString().Trim().Substring(0, Convert.ToInt32(query)));


        //    txtNomor.Text = mpD.ItemCode;
        //    txtTglMulai.Text = mpD.TglMulai;
        //    txtDeptPemohon.Text = mpD2.NamaDept.Trim();

        //    // Login admin Project
        //    if (DomainCekApv.ID <= 0 && user.Apv == 0)
        //    {
        //        txtStatus.Text = "Belum di input Serah";
        //        btnApprove.Visible = false; btnSerah.Visible = true;
        //    }
        //    else if (DomainCekApv.ID > 0 && user.Apv == 0 && DomainCekApv.Apv > 0)
        //    {
        //        if (user.Apv == 0 && user.DeptID == 24)
        //        { btnApprove.Enabled = false; btnSerah.Visible = false;  }
        //        //else
        //        //{ btnApprove.Enabled = true; btnSerah.Visible = false; }
        //        else 
        //        {
        //            btnApprove.Visible = false; btnSerah.Visible = true;
        //        }
        //        txtStatus.Text = "Belum di input Serah";
        //        txtTglSelesai.Text = DomainCekApv.TglSelesai1;
        //        //btnApprove.Visible = false; btnSerah.Visible = true;
        //    }
        //    else if (DomainCekApv.ID > 0 && user.Apv == 0 && DomainCekApv.Apv == 0)
        //    {
        //        txtStatus.Text = "Asset sdh di input Serah - Open";
        //        btnApprove.Visible = false; btnSerah.Visible = false;
        //    }
        //    // Login Mgr Project
        //    else if (DomainCekApv.ID >0 && user.Apv>0 && (user.DeptID == 22 || user.DeptID == 4 || user.DeptID == 30) && DomainCekApv.Apv == 0)
        //    {
        //        txtStatus.Text = "Open";
        //        txtTglSelesai.Text = Convert.ToDateTime(DomainCekApv.TglSelesaiPekerjaan).ToString("dd-MM-yyyy");
        //        btnApprove.Visible = true; btnSerah.Visible = false;
        //    }
        //    // Login Manager Pemilik Asset
        //    else if (DomainCekApv.ID > 0 && user.Apv > 0 && user.DeptID != 22 && DomainCekApv.Apv == 2)
        //    {
        //        txtStatus.Text = "Approved - Mgr Project";
        //        //txtTglSelesai.Text = Convert.ToDateTime(DomainCekApv.TglSelesaiPekerjaan).ToString("dd-MM-yyyy");
        //        txtTglSelesai.Text = DomainCekApv.TglSelesai1;
        //        btnApprove.Visible = true; btnSerah.Visible = false;
        //    }
        //    // Login Supervisor ACC
        //    else if (DomainCekApv.ID > 0 && user.Apv == 1 && user.DeptID == 24 && DomainCekApv.Apv == 3 || DomainCekApv.ID > 0 && user.Apv == 0)
        //    {


        //        txtStatus.Text = "Approved - Mgr Dept Pemilik Asset";
        //        //txtTglSelesai.Text = Convert.ToDateTime(DomainCekApv.TglSelesaiPekerjaan).ToString("dd-MM-yyyy");
        //        txtTglSelesai.Text = DomainCekApv.TglSelesai1;
        //        btnApprove.Visible = true; btnSerah.Visible = false;
        //    }
        //    // Login Manager ACC
        //    else if (DomainCekApv.ID > 0 && user.Apv == 2 && user.DeptID == 24 && DomainCekApv.Apv == 4)
        //    {
        //        txtStatus.Text = "Approved - Spv Acc";
        //        //txtTglSelesai.Text = Convert.ToDateTime(DomainCekApv.TglSelesaiPekerjaan).ToString("dd-MM-yyyy"); 
        //        txtTglSelesai.Text = DomainCekApv.TglSelesai1;
        //        btnApprove.Visible = true; btnSerah.Visible = false;
        //    }
        //    // Approved Manager Acc
        //    //else if (DomainCekApv.ID > 0 && user.Apv > 0 && user.DeptID == 24 && DomainCekApv.Apv == 4)
        //    //{
        //    //    txtStatus.Text = "Approved - Mgr Acc";
        //    //    //txtTglSelesai.Text = Convert.ToDateTime(DomainCekApv.TglSelesaiPekerjaan).ToString("dd-MM-yyyy"); 
        //    //    txtTglSelesai.Text = DomainCekApv.TglSelesai1;
        //    //    btnApprove.Visible = true; btnSerah.Visible = false;
        //    //}


        //    txtBiaya.Text = Convert.ToDecimal(TotalPrice).ToString("N2"); int A = 0;

        //    if (user.UnitKerjaID.ToString().Length == 1)
        //    {
        //        A = 13;
        //    }
        //    else
        //    {
        //        A = 14;
        //    }

        //    LoadKomponenAsset(ddlNamaProject.SelectedItem.ToString().Trim().Substring(0, A));


        //        //txtTglMulai.Text = (txtTMulai.Value.Split(','))[ddlNamaProject.SelectedIndex - 1].ToString();
        //        //txtTglMulai.ReadOnly = true;
        //        //txtNomor.ReadOnly = true;
        //        //txtNomor.Text = (txtNoImp.Value.Split(','))[ddlNamaProject.SelectedIndex - 1].ToString();
        //        //txtBiaya.Text = (txtHarga.Value.Split('|'))[ddlNamaProject.SelectedIndex - 1].ToString();
        //        //txtDeptPemohon.Text = (txtDeptMohon.Value.Split(','))[ddlNamaProject.SelectedIndex - 1].ToString();
        //        //txtTglSelesai.Text = (int.Parse(txtLevel.Value) > 0) ? (txtFinish.Value.Split(','))[ddlNamaProject.SelectedIndex - 1].ToString() : "";
        //        //mpD = mpp.GetDeptID(txtNomor.Text);
        //        //txtDID.Text = mpD.DeptID.ToString();
        //        //string test = ddlNamaProject.SelectedValue;
        //        //txtQty.Text = txtQty1.Value;

        //        //string[] Status = txtSts.Value.Split(',');
        //        //switch (Status[ddlNamaProject.SelectedIndex - 1].ToString())
        //        //switch (Status[ddlNamaProject.SelectedIndex  + 1].ToString())
        //        //{
        //        //    case "0":
        //        //        txtStatus.Text = "Release";
        //        //        break;
        //        //    case "1":
        //        //        txtStatus.Text = "Project Finished";
        //        //        break;
        //        //    case "2":
        //        //        txtStatus.Text = "Project Hand Over";
        //        //        break;
        //        //}

        //        //ArrayList arrData = (ArrayList)Session["DataProject"];
        //        //foreach (MTC_Project_Rev1 mpo in arrData)
        //        //{
        //        //    if (mpo.Status == 2 && mpo.RowStatus == 2 && mpo.Approval == 2)
        //        //    {
        //        //        txtStatus.Text = "Project Finished"; Session["Approval"] = mpo.Approval;
        //        //    }
        //        //    else if (mpo.Status == 2 && mpo.RowStatus == 2 && mpo.Approval == 3)
        //        //    {
        //        //        txtStatus.Text = "Project Hand Over"; Session["Approval"] = mpo.Approval;
        //        //    }

        //        //}

        //        //txtStatus.Text = "Project Finished";
        //        int Approval = Convert.ToInt32(Session["Approval"]);
        //        //if (int.Parse(txtLevel.Value) == 1) txtTglSelesai.Focus();
        //        if (Approval == 2) txtTglSelesai.Focus();

        //       // LoadEstimasiMaterial(txtNomor.Text);
        //    //}
        //    //catch { Response.Redirect("SerahTerimaProject_Rev1.aspx"); }
        //}

        protected void ddlNamaProject_Change(object sender, EventArgs e)
        {
            Users user = (Users)Session["Users"]; string query = string.Empty; string query2 = string.Empty;
            if (user.UnitKerjaID.ToString().Length == 1)
            {
                query = "13"; query2 = "8";
            }
            else
            {
                query = "14"; query2 = "9";
            }

            AssetDomainSerah DomainCekApv = new AssetDomainSerah();
            AssetFacadeSerah FacadeCekApv = new AssetFacadeSerah();
            DomainCekApv = FacadeCekApv.RetrieveCekApv(ddlNamaProject.SelectedItem.ToString().Trim().Substring(0, Convert.ToInt32(query)));
            Session["AssetID"] = DomainCekApv.ID;
            Session["Upgrade"] = DomainCekApv.Upgrade;

            AssetDomainSerah mpD00 = new AssetDomainSerah();
            AssetFacadeSerah mpp00 = new AssetFacadeSerah();
            mpD00 = mpp00.RetrieveDataPICDept(ddlNamaProject.SelectedItem.ToString().Trim().Substring(0, Convert.ToInt32(query)));

            AssetDomainSerah mpD0 = new AssetDomainSerah();
            AssetFacadeSerah mpp0 = new AssetFacadeSerah();
            mpD0 = mpp0.RetrieveDataPIC(ddlNamaProject.SelectedItem.ToString().Trim().Substring(0, Convert.ToInt32(query)));

            AssetDomainSerah mpD = new AssetDomainSerah();
            AssetFacadeSerah mpp = new AssetFacadeSerah();
            mpD = mpp.RetrieveDataAsset(ddlNamaProject.SelectedItem.ToString().Trim().Substring(0, Convert.ToInt32(query)));

            AssetDomainSerah mpD2 = new AssetDomainSerah();
            AssetFacadeSerah mpp2 = new AssetFacadeSerah();
            mpD2 = mpp2.RetrieveNamaDept(ddlNamaProject.SelectedItem.ToString().Trim().Substring(Convert.ToInt32(query2), 1));

            //AssetDomainSerah mpD3 = new AssetDomainSerah();
            //AssetFacadeSerah mpp3 = new AssetFacadeSerah();
            //decimal TotalPrice = mpp3.RetrieveTotalPrice(ddlNamaProject.SelectedItem.ToString().Trim().Substring(0, Convert.ToInt32(query)));


            txtNomor.Text = mpD.ItemCode;
            txtTglMulai.Text = mpD.TglMulai;
            txtDeptPemohon.Text = mpD2.NamaDept.Trim();
            txtPelaksana.Text = mpD00.NamaDept.ToString().Trim();

            // Login admin Project
            if (DomainCekApv.ID <= 0 && user.Apv == 0)
            {
                txtStatus.Text = "Belum di input Serah";
                //btnApprove.Visible = false; btnSerah.Visible = true;
                if (user.DeptID == 30 && mpD0.DeptID == 30)
                {
                    btnApprove.Visible = false; btnSerah.Visible = true;
                }
                else if (user.DeptID == 30 && mpD0.DeptID != 30)
                {
                    btnApprove.Visible = false; btnSerah.Visible = false;
                }
                else if (user.DeptID == 22 && mpD0.DeptID == 22)
                {
                    btnApprove.Visible = false; btnSerah.Visible = true;
                }
                else if (user.DeptID == 19 && mpD0.DeptID == 19)
                {
                    btnApprove.Visible = false; btnSerah.Visible = true;
                }
                else if (user.DeptID == 19 && mpD0.DeptID != 19)
                {
                    btnApprove.Visible = false; btnSerah.Visible = false;
                }
                txtTglSelesai.Text = Convert.ToDateTime(DomainCekApv.TglSelesaiPekerjaan).ToString("dd-MM-yyyy");
                if (txtTglSelesai.Text == "01-01-0001")
                {
                    txtTglSelesai.Text = "-";
                }
            }
            else if (DomainCekApv.ID > 0 && user.Apv == 0 && DomainCekApv.Apv > 0)
            {
                if (user.Apv == 0 && user.DeptID == 24)
                { btnApprove.Enabled = false; btnSerah.Visible = false; }
                //else
                //{ btnApprove.Enabled = true; btnSerah.Visible = false; }
                else
                {
                    btnApprove.Visible = false; btnSerah.Visible = true;
                }

                if (user.Apv == 0 && user.DeptID == 22 && DomainCekApv.Apv > 0)
                {
                    btnApprove.Visible = false; btnSerah.Visible = false;
                }

                if (DomainCekApv.Apv == 0)
                {
                    txtStatus.Text = "Asset sdh di input Serah - Open";
                }
                else if (DomainCekApv.Apv == 2)
                {
                    txtStatus.Text = "Approved - Mgr Project";
                }
                else if (DomainCekApv.Apv == 3)
                {
                    txtStatus.Text = "Approved - Mgr Dept Pemilik Asset";
                }
                else if (DomainCekApv.Apv == 4)
                {
                    txtStatus.Text = "Approved - Spv Acc";
                }
                else if (DomainCekApv.Apv == 5)
                {
                    txtStatus.Text = "Approved - Mgr Acc";
                }

                txtTglSelesai.Text = DomainCekApv.TglSelesai1;

            }
            else if (DomainCekApv.ID > 0 && user.Apv == 0 && DomainCekApv.Apv == 0)
            {
                //txtStatus.Text = "Belum di input Serah";
                txtStatus.Text = "Asset sdh di input Serah - Open";
                btnApprove.Visible = false; btnSerah.Visible = false;

                txtTglSelesai.Text = Convert.ToDateTime(DomainCekApv.TglSelesaiPekerjaan).ToString("dd-MM-yyyy");
            }
            // Login Mgr Project
            else if (DomainCekApv.ID > 0 && user.Apv > 0 && (user.DeptID == 22 || user.DeptID == 4 || user.DeptID == 30 || user.DeptID == 19) && DomainCekApv.Apv == 0)
            {
                txtStatus.Text = "Open";
                txtTglSelesai.Text = Convert.ToDateTime(DomainCekApv.TglSelesaiPekerjaan).ToString("dd-MM-yyyy");
                btnApprove.Visible = true; btnSerah.Visible = false;
            }
            // Login Manager Pemilik Asset
            else if (DomainCekApv.ID > 0 && user.Apv > 0 && user.DeptID != 22 && DomainCekApv.Apv == 2)
            {
                txtStatus.Text = "Approved - Mgr Project";
                //txtTglSelesai.Text = Convert.ToDateTime(DomainCekApv.TglSelesaiPekerjaan).ToString("dd-MM-yyyy");
                txtTglSelesai.Text = DomainCekApv.TglSelesai1;
                btnApprove.Visible = true; btnSerah.Visible = false;
            }
            // Login Supervisor ACC
            else if (DomainCekApv.ID > 0 && user.Apv == 1 && user.DeptID == 24 && DomainCekApv.Apv == 3 || DomainCekApv.ID > 0 && user.Apv == 0)
            {


                txtStatus.Text = "Approved - Mgr Dept Pemilik Asset";
                //txtTglSelesai.Text = Convert.ToDateTime(DomainCekApv.TglSelesaiPekerjaan).ToString("dd-MM-yyyy");
                txtTglSelesai.Text = DomainCekApv.TglSelesai1;
                btnApprove.Visible = true; btnSerah.Visible = false;
            }
            // Login Manager ACC
            else if (DomainCekApv.ID > 0 && user.Apv == 2 && user.DeptID == 24 && DomainCekApv.Apv == 4)
            {
                txtStatus.Text = "Approved - Spv Acc";
                //txtTglSelesai.Text = Convert.ToDateTime(DomainCekApv.TglSelesaiPekerjaan).ToString("dd-MM-yyyy"); 
                txtTglSelesai.Text = DomainCekApv.TglSelesai1;
                btnApprove.Visible = true; btnSerah.Visible = false;
            }
            // Approved Manager Acc
            //else if (DomainCekApv.ID > 0 && user.Apv > 0 && user.DeptID == 24 && DomainCekApv.Apv == 4)
            //{
            //    txtStatus.Text = "Approved - Mgr Acc";
            //    //txtTglSelesai.Text = Convert.ToDateTime(DomainCekApv.TglSelesaiPekerjaan).ToString("dd-MM-yyyy"); 
            //    txtTglSelesai.Text = DomainCekApv.TglSelesai1;
            //    btnApprove.Visible = true; btnSerah.Visible = false;
            //}

            //AssetDomainSerah mpD3 = new AssetDomainSerah();
            //AssetFacadeSerah mpp3 = new AssetFacadeSerah();
            //decimal TotalPrice = mpp3.RetrieveTotalPrice(ddlNamaProject.SelectedItem.ToString().Trim().Substring(0, Convert.ToInt32(query)));

            //txtBiaya.Text = Convert.ToDecimal(TotalPrice).ToString("N2");
            int A = 0;

            if (user.UnitKerjaID.ToString().Length == 1)
            {
                A = 13;
            }
            else
            {
                A = 14;
            }

            LoadKomponenAsset(ddlNamaProject.SelectedItem.ToString().Trim().Substring(0, A));

            AssetDomainSerah mpD3 = new AssetDomainSerah();
            AssetFacadeSerah mpp3 = new AssetFacadeSerah();
            decimal TotalPrice = mpp3.RetrieveTotalPrice(ddlNamaProject.SelectedItem.ToString().Trim().Substring(0, Convert.ToInt32(query)));

            txtBiaya.Text = Convert.ToDecimal(TotalPrice).ToString("N2");


            //txtTglMulai.Text = (txtTMulai.Value.Split(','))[ddlNamaProject.SelectedIndex - 1].ToString();
            //txtTglMulai.ReadOnly = true;
            //txtNomor.ReadOnly = true;
            //txtNomor.Text = (txtNoImp.Value.Split(','))[ddlNamaProject.SelectedIndex - 1].ToString();
            //txtBiaya.Text = (txtHarga.Value.Split('|'))[ddlNamaProject.SelectedIndex - 1].ToString();
            //txtDeptPemohon.Text = (txtDeptMohon.Value.Split(','))[ddlNamaProject.SelectedIndex - 1].ToString();
            //txtTglSelesai.Text = (int.Parse(txtLevel.Value) > 0) ? (txtFinish.Value.Split(','))[ddlNamaProject.SelectedIndex - 1].ToString() : "";
            //mpD = mpp.GetDeptID(txtNomor.Text);
            //txtDID.Text = mpD.DeptID.ToString();
            //string test = ddlNamaProject.SelectedValue;
            //txtQty.Text = txtQty1.Value;

            //string[] Status = txtSts.Value.Split(',');
            //switch (Status[ddlNamaProject.SelectedIndex - 1].ToString())
            //switch (Status[ddlNamaProject.SelectedIndex  + 1].ToString())
            //{
            //    case "0":
            //        txtStatus.Text = "Release";
            //        break;
            //    case "1":
            //        txtStatus.Text = "Project Finished";
            //        break;
            //    case "2":
            //        txtStatus.Text = "Project Hand Over";
            //        break;
            //}

            //ArrayList arrData = (ArrayList)Session["DataProject"];
            //foreach (MTC_Project_Rev1 mpo in arrData)
            //{
            //    if (mpo.Status == 2 && mpo.RowStatus == 2 && mpo.Approval == 2)
            //    {
            //        txtStatus.Text = "Project Finished"; Session["Approval"] = mpo.Approval;
            //    }
            //    else if (mpo.Status == 2 && mpo.RowStatus == 2 && mpo.Approval == 3)
            //    {
            //        txtStatus.Text = "Project Hand Over"; Session["Approval"] = mpo.Approval;
            //    }

            //}

            //txtStatus.Text = "Project Finished";
            int Approval = Convert.ToInt32(Session["Approval"]);
            //if (int.Parse(txtLevel.Value) == 1) txtTglSelesai.Focus();
            if (Approval == 2) txtTglSelesai.Focus();

            // LoadEstimasiMaterial(txtNomor.Text);
            //}
            //catch { Response.Redirect("SerahTerimaProject_Rev1.aspx"); }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            Users user = (Users)Session["Users"];
            string Upgrade = Session["Upgrade"].ToString();

            AssetDomainSerah DSerahEng = new AssetDomainSerah();
            AssetFacadeSerah FSerahEng = new AssetFacadeSerah();
            int DeptID_Eng = FSerahEng.RetrieveDeptID_Eng(txtNomor.Text.Trim());

            AssetDomainSerah DSerah1 = new AssetDomainSerah();
            AssetFacadeSerah FSerah1 = new AssetFacadeSerah();
            DSerah1 = FSerah1.RetrieveSerahAsset(txtNomor.Text.Trim());

            AssetDomainSerah DSerahUpdateApv = new AssetDomainSerah();
            AssetFacadeSerah FSerahUpdateApv = new AssetFacadeSerah();

            // Approve Mgr Project Sipil atau Mgr Project Mekanik atau Mgr Engineering
            if (user.DeptID == 22 && user.Apv > 0 || user.DeptID == 30 && user.Apv > 0 || user.DeptID == 19 && user.Apv > 0 || user.DeptID == 4 && user.Apv > 0 && DSerah1.StatusApv < 2)
            {
                DSerahUpdateApv.Apv = 2;
                //DSerahUpdateApv.Apv = 1;
                DSerahUpdateApv.UserName = user.UserName.Trim();
            }

            // Approve Mgr Pemilik Asset
            else if (
                user.DeptID != 22 && user.DeptID != 24 && user.DeptID != 30 && DSerah1.StatusApv == 2 ||
                user.DeptID != 0 && user.Apv > 0 && DSerah1.StatusApv == 2 ||
                user.DeptID == 4 && user.Apv > 0 && DSerah1.StatusApv == 2 ||
                user.DeptID != 19 && user.Apv > 0 && DSerah1.StatusApv == 2 ||
                user.DeptID != 25 && user.Apv > 0 && DSerah1.StatusApv == 2)
            {
                DSerahUpdateApv.Apv = 3;
                DSerahUpdateApv.UserName = user.UserName.Trim();
            }

            // Approve Spv Accounting
            else if (user.DeptID == 24 && user.Apv == 1)
            {
                DSerahUpdateApv.Apv = 4;
                DSerahUpdateApv.UserName = user.UserName.Trim();
            }

            // Approve Mgr Accounting
            else if (user.DeptID == 24 && user.Apv == 2)
            {
                DSerahUpdateApv.Apv = 5;
                DSerahUpdateApv.UserName = user.UserName.Trim();
            }

            DSerahUpdateApv.AssetID = Convert.ToInt32(Session["AssetID"]);

            int intResult = 0;
            intResult = FSerahUpdateApv.UpdateApvSerah(DSerahUpdateApv);

            if (intResult > 0)
            {
                AssetDomainSerah DSerahLog2 = new AssetDomainSerah();
                AssetFacadeSerah FSerahLog2 = new AssetFacadeSerah();

                DSerahLog2.AssetID = intResult;
                DSerahLog2.UserName = user.UserName.Trim();
                DSerahLog2.AssetID = Convert.ToInt32(Session["AssetID"]);

                int intResultLog2 = 0;
                intResultLog2 = FSerahLog2.InsertSerahLog2(DSerahLog2);

                if (intResultLog2 > 0 && DSerahUpdateApv.Apv < 5)
                {
                    Response.Redirect("SerahTerimaAsset.aspx");
                }
                //else if (DeptID_Eng > 19 || DeptID_Eng < 19)
                else
                {
                    AssetDomainSerah DSerahDataAMAsset = new AssetDomainSerah();
                    AssetFacadeSerah FSerahDataAMAsset = new AssetFacadeSerah();
                    DSerahDataAMAsset = FSerahDataAMAsset.RetrieveDataAsset2(txtNomor.Text.Trim());

                    AssetDomainSerah DSerahBA = new AssetDomainSerah();
                    AssetFacadeSerah FSerahBA = new AssetFacadeSerah();
                    int NoBA_Akhir = FSerahBA.LastAdjustID();
                    string NoBA = (NoBA_Akhir + 1) + "/ADJ/" + Global.BulanRomawi(DateTime.Now.Month) + "/" + DateTime.Now.Year.ToString();

                    AssetDomainSerah DSerahAMAsset = new AssetDomainSerah();
                    AssetFacadeSerah FSerahAMAsset = new AssetFacadeSerah();

                    DSerahAMAsset.AMGroupID = DSerahDataAMAsset.AMGroupID; //1
                    DSerahAMAsset.AMclassID = DSerahDataAMAsset.AMclassID; //2
                    DSerahAMAsset.AMsubClassID = DSerahDataAMAsset.AMsubClassID; //3
                    DSerahAMAsset.AMlokasiID = DSerahDataAMAsset.AMlokasiID; //4
                    DSerahAMAsset.ItemCode = DSerahDataAMAsset.ItemCode.Trim(); //5
                    DSerahAMAsset.ItemName = DSerahDataAMAsset.ItemName.Trim(); //6
                    DSerahAMAsset.AssyDate = DateTime.Now; //7
                    DSerahAMAsset.NilaiAsset = Convert.ToDecimal(txtBiaya.Text); //8
                    DSerahAMAsset.MfgDate = DateTime.Now; //9
                    DSerahAMAsset.MfgYear = DateTime.Now.Year.ToString(); //10
                    DSerahAMAsset.LifeTime = "0"; //11
                    DSerahAMAsset.DepreciatID = 1; //12
                    DSerahAMAsset.StartDeprec = DateTime.Now; //13
                    DSerahAMAsset.ItemKode = DSerahDataAMAsset.ItemCode.Trim(); //14
                    DSerahAMAsset.AssetID = DSerahDataAMAsset.AssetID; //15
                    DSerahAMAsset.CreatedBy = user.UserName.Trim();

                    if (DSerahAMAsset.AMGroupID == 3)
                    {
                        DSerahAMAsset.PicDept = 4; //15
                        DSerahAMAsset.UserName = "Mgr-MTC"; //16
                    }
                    else if (DSerahAMAsset.AMGroupID == 5)
                    {
                        DSerahAMAsset.PicDept = 7; //15
                        DSerahAMAsset.UserName = "Mgr-HRD"; //16
                    }
                    else
                    {
                        DSerahAMAsset.PicDept = 0; //15
                        DSerahAMAsset.UserName = "-"; //16
                    }



                    DSerahAMAsset.PlantID = user.UnitKerjaID; //17
                    DSerahAMAsset.RowStatus = 0; //18
                    DSerahAMAsset.TipeAsset = 2; //19
                    DSerahAMAsset.UomID = DSerahDataAMAsset.UomID; ; //20
                    DSerahAMAsset.OwnerDeptID = DSerahDataAMAsset.DeptID; //21             

                    int intResult2 = 0;
                    intResult2 = FSerahAMAsset.InsertAssetKeAMAsset(DSerahAMAsset);

                    if (intResult2 > 0)
                    {
                        AssetDomainSerah DSerahBA01 = new AssetDomainSerah();
                        AssetFacadeSerah FSerahBA01 = new AssetFacadeSerah();
                        DSerahBA01.NoBA = NoBA;
                        DSerahBA01.Tanggal = DateTime.Now.ToString("yyyy-MM-dd");
                        DSerahBA01.CreatedBy = user.UserName;
                        DSerahBA01.CreatedTime = DateTime.Now;
                        DSerahBA01.ItemID = DSerahDataAMAsset.AssetID;
                        DSerahBA01.UomID = DSerahDataAMAsset.UomID;

                        if (Upgrade == "0")
                        {
                            //string NoBA = FSerahBA.LastAdjustID(DSerahBA);
                            ////string NoBA = (this.LastAdjustID() + 1).ToString().PadLeft(5, '0') + "/ADJ/" + Global.BulanRomawi(dt.Month) + "/" + dt.Year.ToString();
                            intResult = FSerahBA.InsertBA(DSerahBA01);

                            if (intResult > 0)
                            {
                                Response.Redirect("SerahTerimaAsset.aspx");
                            }
                            else
                            {
                                DisplayAJAXMessage(this, "Eror simpan !!"); return;
                            }
                        }


                        Response.Redirect("SerahTerimaAsset.aspx");
                        //DisplayAJAXMessage(this, " Asset berkomponen berhasil masuk ke data asset"); return;
                    }
                }
                //else if (DeptID_Eng == 19)
                //{
                //    AssetDomainSerah DSerahAMAsset01 = new AssetDomainSerah();
                //    AssetFacadeSerah FSerahAMAsset01 = new AssetFacadeSerah();

                //    int intResult3 = 0;
                //    intResult3 = FSerahAMAsset01.InsertAssetKeAMAsset(DSerahAMAsset01);
                //}

            }


        }
        protected void btnSerah_Click(object sender, EventArgs e)
        {
            Users user = (Users)Session["Users"];

            if (txtTglSelesai.Text == string.Empty)
            {
                DisplayAJAXMessage(this, " Tanggal selesai belum ditentukan !!"); return;
            }

            AssetDomainSerah DSerah = new AssetDomainSerah();
            AssetFacadeSerah FSerah = new AssetFacadeSerah();

            AssetDomainSerah DSerah01 = new AssetDomainSerah();
            AssetFacadeSerah FSerah01 = new AssetFacadeSerah();

            //DomainAsset domainSaveAssetKomponen01= new DomainAsset();
            //FacadeAsset facadeSaveAssetKomponen01 = new FacadeAsset();
            int TtlAsset = FSerah01.RetrieveAssetAda(txtNomor.Text.Trim());
            int Upgrade = (TtlAsset == 0) ? 0 : TtlAsset;

            DSerah.NoAsset = txtNomor.Text.Trim();
            DSerah.TglMulaiPekerjaan = Convert.ToDateTime(txtTglMulai.Text);
            DSerah.TglSelesaiPekerjaan = Convert.ToDateTime(txtTglSelesai.Text);
            DSerah.UserName = user.UserName.Trim();
            DSerah.Upgrade = Upgrade;

            int intResult = 0;
            intResult = FSerah.InsertSerahAsset(DSerah);

            if (intResult > 0)
            {
                int intResultDetail = 0;

                for (int i = 0; i < lstMaterial.Items.Count; i++)
                {
                    HtmlTableRow tr = (HtmlTableRow)lstMaterial.Items[i].FindControl("lst");
                    {
                        AssetDomainSerah DSerahDetail = new AssetDomainSerah();
                        AssetFacadeSerah FSerahDetail = new AssetFacadeSerah();

                        DSerahDetail.AssetID = intResult;
                        DSerahDetail.KomponenCode = tr.Cells[1].InnerHtml.Trim();
                        DSerahDetail.KomponenItem = tr.Cells[2].InnerHtml.Trim();
                        DSerahDetail.QtyPakai = Convert.ToDecimal(tr.Cells[4].InnerHtml);
                        DSerahDetail.Nilai = Convert.ToDecimal(tr.Cells[5].InnerHtml);

                        intResultDetail = FSerah.InsertSerahDetailAsset(DSerahDetail);
                    }
                }

                if (intResultDetail > 0)
                {
                    //DisplayAJAXMessage(this, " Data berhasil di system !! "); return;
                    AssetDomainSerah DSerahLog = new AssetDomainSerah();
                    AssetFacadeSerah FSerahLog = new AssetFacadeSerah();

                    DSerahLog.AssetID = intResult;
                    DSerahLog.UserName = user.UserName.Trim();

                    int intResultLog = 0;
                    intResultLog = FSerahLog.InsertSerahLog(DSerahLog);

                    if (intResultLog > 0)
                    {
                        Response.Redirect("SerahTerimaAsset.aspx");
                    }
                }
                else if (intResultDetail < 0)
                {
                    AssetDomainSerah DSerahDel = new AssetDomainSerah();
                    AssetFacadeSerah FSerahDel = new AssetFacadeSerah();

                    int intResultDel = 0;
                    intResultDel = FSerah.InsertSerahDelete(intResult);

                    DisplayAJAXMessage(this, " Gagal Simpan ( Silahkan Hub IT )"); return;
                }


            }


            //string[] App = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ApvPengajuan", "EngineeringNew").Split(',');
            //int LevelApp = Array.IndexOf(App, ((Users)Session["Users"]).ID.ToString());    

            //MTC_ProjectFacade_Rev1 mpp = new MTC_ProjectFacade_Rev1();
            //MTC_Project_Rev1 mp = new MTC_Project_Rev1();
            //mp.ID = int.Parse(ddlNamaProject.SelectedValue.ToString());
            //Users user = ((Users)Session["Users"]);

            //if (LevelApp == 2 )
            //{
            //    mp.RowStatus = 2; mp.Status = 2; mp.Approval = 3;            
            //}
            //else if (LevelApp < 0 )
            //{
            //    mp.RowStatus = 2; mp.Status = 2; mp.Approval = 4;
            //}
            //else
            //{
            //    mp.RowStatus = int.Parse(txtLevel.Value);
            //}

            //mp.Status = (int.Parse(txtLevel.Value) < 2) ? int.Parse(txtLevel.Value) + 1 : 2;
            //mp.FinishDate = DateTime.Parse(txtTglSelesai.Text);

            //int result = mpp.Delete(mp, true);

            //if (result > 0)
            //{
            //    //log insert approval
            //    mp.CreatedBy = ((Users)Session["Users"]).UserID;
            //    //mp.Approval = int.Parse(txtLevel.Value);
            //    //mp.Approval = mp.Approval;

            //    //switch (txtLevel.Value)
            //    if (mp.Approval > 0)
            //    {
            //        if (mp.Approval == 3)
            //        {
            //            mp.Statuse = "Diserahkan [Finish on : " + txtTglSelesai.Text + " ]";
            //        }
            //        else if (mp.Approval == 4)
            //        {
            //            mp.Statuse = "Diterima";
            //        }
            //        else
            //        {
            //            mp.Statuse = "Diketahui";
            //        }
            //        //case 1: mp.Statuse = "Diserahkan [Finish on : " + txtTglSelesai.Text +" ]"; break;
            //        //case 2: mp.Statuse = "Diterima"; break;
            //        //case 3: mp.Statuse = "Diketahui"; break;
            //    }
            //        int rst = mpp.InsertLog(mp);

            //    Response.Redirect("SerahTerimaProject_Rev1.aspx");
            //}
        }
        protected void txtNomor_Change(object sender, EventArgs e)
        {
            MTC_ProjectFacade_Rev1 mpp = new MTC_ProjectFacade_Rev1();
            MTC_Project_Rev1 mp = new MTC_Project_Rev1();
            Users user = ((Users)Session["Users"]);
            string[] SerahTerima = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ApvSerahTerima", "EngineeringNew").Split(',');
            int Level = Array.IndexOf(SerahTerima, ((Users)Session["Users"]).ID.ToString());
            string Levele = string.Empty;

            switch (Level)
            {
                case 0:
                    Levele = " and mp.Approval=2";
                    txtLevel.Value = "2";//di serahkan dari engineering project sudah finish
                    break;
                case 2:
                    txtLevel.Value = "3";
                    Levele = " and mp.RowStatus=2"; //di aprove oleh pm project sudah selesai
                    break;
                default:
                    Levele = " and mp.Approval=3";
                    txtLevel.Value = "3"; // diterima oleh dept pemohon

                    if (user.DeptID == 6 || user.DeptID == 10)
                    {
                        Levele += " and mp.DeptID in (6,10)";
                    }
                    else
                    {
                        Levele += " and mp.DeptID=" + user.DeptID.ToString();
                    }
                    break;
            }

            //string where = " and mp.Status=2 " + Levele + " and mp.Nomor !='' AND Approval>1 order by mp.nomor,mp.ID";
            //string where = " and mp.Status=1 " + Levele + " and mp.Nomor !='' AND Approval>1 and mp.ApvPM=3 order by mp.nomor,mp.ID";
            string where =
            //" and ((mp.Status=1  " + Levele + " AND Approval>1 and mp.Nomor !='')  or  " +
            " and (mp.RowStatus>-1 and mp.Status=2 and mp.RowStatus=2  " + Levele + " and mp.ApvPM=2 and mp.release=1 and mp.Nomor !='') or " +
            " (mp.RowStatus>-1 and mp.Status=2 and mp.RowStatus=2  " + Levele + " and mp.ApvPM=2 and mp.release=1 and mp.Nomor !='') ";

            mp = mpp.RetrieveProject1(where, txtNomor0.Text, true);

            txtProjectID.Value = mp.ID.ToString();
            ddlNamaProject.SelectedValue = txtProjectID.Value;
            ddlNamaProject_Change(null, null);

            //LoadEstimasiMaterial(txtNomor0.Text);
        }

        private void LoadEstimasiMaterial(string ProjectID)
        {
            //ArrayList arrData = new ArrayList();
            //MTC_ProjectFacade_Rev1 MPF = new MTC_ProjectFacade_Rev1();
            //arrData = MPF.RetrieveEstimasiMaterialSerah(ProjectID);
            //lstMaterial.DataSource = arrData;
            //lstMaterial.DataBind();
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void lstMaterial_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Users users = (Users)Session["Users"];
            AssetDomainSerah p = (AssetDomainSerah)e.Item.DataItem;
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("lst");
                    if (tr != null)
                    {
                        TotalQty += p.TotalPrice;
                    }
                }
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    HtmlTableRow tr2 = (HtmlTableRow)e.Item.FindControl("ftr");
                    tr2.Cells[1].InnerText = TotalQty.ToString("N2");
                    //tr2.Cells[2].InnerText = TotalM3.ToString("N2");

                    //lblTotal.Text = TotalQty.ToString("N0"); lblTotal.Visible = true;
                }
                //lblTotal.Text = TotalQty.ToString("N0");
                //lblGrandTotal.Text = GrandTotal.ToString("N0"); lblGrandTotalM3.Text = GrandTotalM3.ToString("N2");
            }
            catch { }
        }

    }

    public class AssetDomainSerah
    {
        public int StatusApv { get; set; }
        public int Urut { get; set; }
        public int Upgrade { get; set; }
        public int ItemID { get; set; }
        public int UnitKerjaID { get; set; }
        public int ID { get; set; }
        public int RowStatus { get; set; }
        public int Apv { get; set; }
        public int AssetID { get; set; }
        public int UomID { get; set; }
        public int AMGroupID { get; set; }
        public int AMclassID { get; set; }
        public int AMsubClassID { get; set; }
        public int AMlokasiID { get; set; }
        public int AM_DeptID { get; set; }
        public int HeadID { get; set; }
        public int TipeAsset { get; set; }
        public int PicDept { get; set; }
        public int PlantID { get; set; }
        public int OwnerDeptID { get; set; }
        public int DepreciatID { get; set; }
        public int DeptID { get; set; }

        //AdjustType,Keterangan1,Status,ItemTypeID,CreatedBy

        public DateTime AdjustDate { get; set; }
        public DateTime TglMulai0 { get; set; }
        public DateTime TglSelesaiPekerjaan { get; set; }
        public DateTime TglMulaiPekerjaan { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime LastModifiedTime { get; set; }
        public DateTime AssyDate { get; set; }
        public DateTime MfgDate { get; set; }
        public DateTime StartDeprec { get; set; }

        public string Tanggal { get; set; }
        public string NoBA { get; set; }
        public string TglSelesai1 { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string TglMulai { get; set; }
        public string NamaDept { get; set; }
        public string UomCode { get; set; }
        public string NoAsset { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public string KomponenCode { get; set; }
        public string UserName { get; set; }
        public string KomponenItem { get; set; }
        public string ItemKode { get; set; }
        public string PicPerson { get; set; }
        public string MfgYear { get; set; }
        public string LifeTime { get; set; }

        public Decimal Price { get; set; }
        public Decimal QtyPakai { get; set; }
        public Decimal TotalPrice { get; set; }
        public Decimal Nilai { get; set; }
        public Decimal NilaiAsset { get; set; }

    }



    public class AssetFacadeSerah
    {
        public string strError = string.Empty;
        private ArrayList arrData = new ArrayList();
        private List<SqlParameter> sqlListParam;
        private AssetDomainSerah objAsset = new AssetDomainSerah();

        public AssetFacadeSerah()
            : base()
        {

        }
        public string Criteria { get; set; }
        public string Field { get; set; }
        public string Where { get; set; }

        public int InsertSerahAsset(object objDomain)
        {
            try
            {
                objAsset = (AssetDomainSerah)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@NoAsset", objAsset.NoAsset));
                sqlListParam.Add(new SqlParameter("@TglMulai", objAsset.TglMulaiPekerjaan));
                sqlListParam.Add(new SqlParameter("@TglSelesai", objAsset.TglSelesaiPekerjaan));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objAsset.UserName));
                sqlListParam.Add(new SqlParameter("@Upgrade", objAsset.Upgrade));

                DataAccess da = new DataAccess(Global.ConnectionString());
                int result = da.ProcessData(sqlListParam, "spInsert_AssetSerah");
                strError = da.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }



        public int InsertSerahDetailAsset(object objDomain)
        {
            try
            {
                objAsset = (AssetDomainSerah)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@AssetID", objAsset.AssetID));
                sqlListParam.Add(new SqlParameter("@KomponenCode", objAsset.KomponenCode));
                sqlListParam.Add(new SqlParameter("@KomponenItem", objAsset.KomponenItem));
                sqlListParam.Add(new SqlParameter("@QtyPakai", objAsset.QtyPakai));
                sqlListParam.Add(new SqlParameter("@Nilai", objAsset.Nilai));

                DataAccess da = new DataAccess(Global.ConnectionString());
                int result = da.ProcessData(sqlListParam, "spInsert_AssetDetailSerah");
                strError = da.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

        public int InsertSerahDelete(int ID)
        {
            try
            {
                //objAsset = (AssetDomainSerah)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@ID", ID));

                DataAccess da = new DataAccess(Global.ConnectionString());

                int result = da.ProcessData(sqlListParam, "spInsert_AssetDelete");
                strError = da.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

        public int InsertSerahLog(object objDomain)
        {
            try
            {
                objAsset = (AssetDomainSerah)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@AssetID", objAsset.AssetID));
                sqlListParam.Add(new SqlParameter("@UserName", objAsset.UserName));

                DataAccess da = new DataAccess(Global.ConnectionString());
                int result = da.ProcessData(sqlListParam, "spInsert_AssetLogSerah");
                strError = da.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

        public int InsertSerahLog2(object objDomain)
        {
            try
            {
                objAsset = (AssetDomainSerah)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@AssetID", objAsset.AssetID));
                sqlListParam.Add(new SqlParameter("@UserName", objAsset.UserName));

                DataAccess da = new DataAccess(Global.ConnectionString());
                int result = da.ProcessData(sqlListParam, "spInsert_AssetLogSerah");
                strError = da.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

        public int UpdateApvSerah(object objDomain)
        {
            try
            {
                objAsset = (AssetDomainSerah)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@AssetID", objAsset.AssetID));
                sqlListParam.Add(new SqlParameter("@UserName", objAsset.UserName));
                sqlListParam.Add(new SqlParameter("@Apv", objAsset.Apv));


                DataAccess da = new DataAccess(Global.ConnectionString());
                int result = da.ProcessData(sqlListParam, "spUpdate_AssetSerah");
                strError = da.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

        public int InsertAssetKeAMAsset(object objDomain)
        {
            try
            {
                objAsset = (AssetDomainSerah)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@GroupID", objAsset.AMGroupID)); //1
                sqlListParam.Add(new SqlParameter("@ClassID", objAsset.AMclassID)); //2
                sqlListParam.Add(new SqlParameter("@SubClassID", objAsset.AMsubClassID)); //3
                sqlListParam.Add(new SqlParameter("@LokasiID", objAsset.AMlokasiID)); //4
                sqlListParam.Add(new SqlParameter("@KodeAsset", objAsset.ItemCode)); //5
                sqlListParam.Add(new SqlParameter("@NamaAsset", objAsset.ItemName)); //6

                sqlListParam.Add(new SqlParameter("@AssyDate", objAsset.AssyDate)); //7
                sqlListParam.Add(new SqlParameter("@NilaiAsset", objAsset.NilaiAsset)); //8
                sqlListParam.Add(new SqlParameter("@MfgDate", objAsset.MfgDate)); //9 
                sqlListParam.Add(new SqlParameter("@MfgYear", objAsset.MfgYear)); //10
                sqlListParam.Add(new SqlParameter("@LifeTime", objAsset.LifeTime)); //11
                sqlListParam.Add(new SqlParameter("@DepreciatID", objAsset.DepreciatID)); //12
                sqlListParam.Add(new SqlParameter("@StartDeprec", objAsset.StartDeprec)); //13

                sqlListParam.Add(new SqlParameter("@ItemKode", objAsset.ItemCode)); //14
                sqlListParam.Add(new SqlParameter("@PicDept", objAsset.PicDept)); //15
                sqlListParam.Add(new SqlParameter("@PicPerson", objAsset.UserName)); //16
                sqlListParam.Add(new SqlParameter("@PlantID", objAsset.PlantID)); //17
                sqlListParam.Add(new SqlParameter("@RowStatus", objAsset.RowStatus)); //18
                sqlListParam.Add(new SqlParameter("@TipeAsset", objAsset.TipeAsset)); //19
                sqlListParam.Add(new SqlParameter("@UomID", objAsset.UomID)); //20
                sqlListParam.Add(new SqlParameter("@OwnerDeptID", objAsset.OwnerDeptID));   //21  

                sqlListParam.Add(new SqlParameter("@AssetID", objAsset.AssetID)); //22   
                sqlListParam.Add(new SqlParameter("@CreatedBy", objAsset.CreatedBy)); //22

                DataAccess da = new DataAccess(Global.ConnectionString());
                int result = da.ProcessData(sqlListParam, "spInsertAM_Asset");
                strError = da.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

        public int InsertBA(object objDomain)
        {
            try
            {
                objAsset = (AssetDomainSerah)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@NoBA", objAsset.NoBA));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objAsset.CreatedBy));
                sqlListParam.Add(new SqlParameter("@Tanggal", objAsset.Tanggal));
                sqlListParam.Add(new SqlParameter("@ItemID", objAsset.ItemID));
                sqlListParam.Add(new SqlParameter("@UomID", objAsset.UomID));

                DataAccess da = new DataAccess(Global.ConnectionString());
                int result = da.ProcessData(sqlListParam, "spInsertAM_Asset2Adjust");
                strError = da.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

        //private int LastAdjustID()
        //{
        //    int result = 0;
        //    ZetroView zw = new ZetroView();
        //    zw.QueryType = Operation.CUSTOM;
        //    zw.CustomQuery = "Select Top 1 ID from Adjust Order By ID Desc";
        //    SqlDataReader sdr = zw.Retrieve();
        //    if (sdr.HasRows)
        //    {
        //        while (sdr.Read())
        //        {
        //            result = Convert.ToInt32(sdr["ID"].ToString());
        //        }
        //    }
        //    return result;
        //}

        public int LastAdjustID()
        {
            string StrSql =
            "Select Top 1 ID from Adjust Order By ID Desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["ID"]);
                }
            }

            return 0;
        }

        public ArrayList RetrieveNamaAsset()
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string query = string.Empty; string query2 = string.Empty;
            if (users.UnitKerjaID.ToString().Length == 1)
            {
                query = "13";
            }
            else
            {
                query = "14";
            }

            string strSQL = "with q as( " +
        "select ItemID from PakaiDetail where ItemTypeID=2 and GroupID=12 " +
        "and PakaiID in (select ID from Pakai where Status>1) " +
    "), " +
    "w as ( " +
        "select DISTINCT(SUBSTRING(ItemCode,8," + query + "))KodeAsset " +
        "from Asset a, q where a.ID=q.ItemID and RowStatus>-1 " +
    "), " +
    "r as ( " +
        "select ItemCode,ItemName from asset a,w where a.itemcode=w.KodeAsset and a.RowStatus>-1 " +
    "), " +
    "t as ( " +
        "select*from r where ItemCode in (select SUBSTRING(ItemCode,8," + query + ") from Asset where Aktif=1 and Head=0) " +
        "and ItemCode not in (select NoAsset from AM_AssetSerah where RowStatus>-1) " +
        "union all " +
        "select a.NoAsset,i.ItemName from AM_AssetSerah a, Asset i " +
        "where a.noasset=i.itemcode and a.RowStatus>-1 and a.apv<5 " +
    ") " +
    "select*from t order by ItemName";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObjectNamaAsset(sqlDataReader));
                }
            }

            return arrData;
        }

        private AssetDomainSerah GenerateObjectNamaAsset(SqlDataReader sdr)
        {
            AssetDomainSerah objAsset = new AssetDomainSerah();
            objAsset.ItemCode = sdr["ItemCode"].ToString();
            objAsset.ItemName = sdr["ItemName"].ToString();
            return objAsset;
        }

        public ArrayList RetrieveNamaAsset2(int DeptID)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string query = string.Empty; string query2 = string.Empty; string Dept0 = string.Empty;
            if (users.UnitKerjaID.ToString().Length == 1)
            { query = "13"; query2 = "9"; }
            else { query = "14"; query2 = "10"; }
            if (DeptID == 25 || DeptID == 19 || DeptID == 4)
            { Dept0 = "19"; }
            else
            { Dept0 = DeptID.ToString(); }

            string Query = string.Empty;
            if (DeptID == 10 || DeptID == 6)
            {
                Query = "DeptID_ID in (10)";
            }
            else if (DeptID == 25 || DeptID == 19 || DeptID == 4)
            {
                Query = "DeptID_ID in (4)";
            }
            else
            {
                Query = "DeptID_ID in (" + DeptID + ")";
            }

            string strSQL =
            " select ItemCode,ItemName from asset where itemcode in ((select DISTINCT(SUBSTRING(ItemCode,8," + query + "))KodeAsset " +
            " from Asset where ID in (select ItemID from PakaiDetail where ItemTypeID=2 and GroupID=12 and PakaiID in (select ID from Pakai where Status>1)) " +
            " and RowStatus>-1)) and ItemCode in (select NoAsset from AM_AssetSerah where RowStatus>-1 and Apv=0  " +
            " and CreatedBy in (select username from users where RowStatus>-1 and DeptID=" + Dept0 + ")) and head=1 and RowStatus>-1 " +

            " union all " +

                " select ItemCode,ItemName from asset where itemcode in ((select DISTINCT(SUBSTRING(ItemCode,8," + query + "))KodeAsset " +
            " from Asset where ID in (select ItemID from PakaiDetail where ItemTypeID=2 and GroupID=12 and PakaiID in (select ID from Pakai where Status>1)) " +
            " and RowStatus>-1)) and ItemCode in (select NoAsset from AM_AssetSerah where RowStatus>-1 and Apv=2) and DeptID in " +
            " (select DeptID from AM_Department where " + Query + " and RowStatus>-1) and head=1 and RowStatus>-1 ";
            //" union all " +
            //" select ItemCode,ItemName from asset where itemcode in ((select DISTINCT(ItemCode)KodeAsset from Asset where LEN(itemcode)=" + query + " and  " +
            //" ID in (select ItemID from PakaiDetail where ItemTypeID=2 and GroupID=4 and PakaiID in (select ID from Pakai where Status>1)) " +
            //" and RowStatus>-1)) and ItemCode in (select NoAsset from AM_AssetSerah where RowStatus>-1 and Apv=0) ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObjectNamaAsset2(sqlDataReader));
                }
            }

            return arrData;
        }

        private AssetDomainSerah GenerateObjectNamaAsset2(SqlDataReader sdr)
        {
            AssetDomainSerah objAsset = new AssetDomainSerah();
            objAsset.ItemCode = sdr["ItemCode"].ToString();
            objAsset.ItemName = sdr["ItemName"].ToString();
            return objAsset;
        }

        public ArrayList RetrieveNamaAsset3(int DeptID_ID)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string query = string.Empty; string query2 = string.Empty;
            if (users.UnitKerjaID.ToString().Length == 1)
            {
                query = "13"; query2 = "9";
            }
            else
            {
                query = "14"; query2 = "10";
            }

            string Query = string.Empty;
            if (DeptID_ID == 10 || DeptID_ID == 6)
            {
                Query = "DeptID_ID in (10)";
            }
            else if (DeptID_ID == 25 || DeptID_ID == 19 || DeptID_ID == 4)
            {
                Query = "DeptID_ID in (4)";
            }
            else
            {
                Query = "DeptID_ID in (" + DeptID_ID + ")";
            }

            string strSQL =
            " select ItemCode,ItemName from asset where itemcode in ((select DISTINCT(SUBSTRING(ItemCode,8," + query + "))KodeAsset " +
            " from Asset where ID in (select ItemID from PakaiDetail where ItemTypeID=2 and GroupID=12 and PakaiID in (select ID from Pakai where Status>1)) " +
            " and RowStatus>-1)) and ItemCode in (select NoAsset from AM_AssetSerah where RowStatus>-1 and Apv=2) and DeptID in " +
            " (select DeptID from AM_Department where " + Query + " and RowStatus>-1) and head=1 and RowStatus>-1 ";
            //" union all " +
            //" select ItemCode,ItemName from asset where itemcode in ((select DISTINCT(ItemCode)KodeAsset from Asset where LEN(itemcode)=" + query + " and  " +
            //" ID in (select ItemID from PakaiDetail where ItemTypeID=2 and GroupID=4 and PakaiID in (select ID from Pakai where Status>1)) " +
            //" and RowStatus>-1)) and ItemCode  in (select NoAsset from AM_AssetSerah where RowStatus>-1 and Apv=1) and SUBSTRING(ItemCode,"+query2+",1) in " +
            //" (select DeptID from AM_Department where " + Query + " and RowStatus>-1) ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObjectNamaAsset3(sqlDataReader));
                }
            }

            return arrData;
        }

        public ArrayList RetrieveNamaAsset4(int DeptID_ID)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string query = string.Empty; string query2 = string.Empty;
            if (users.UnitKerjaID.ToString().Length == 1)
            {
                query = "13"; query2 = "9";
            }
            else
            {
                query = "14"; query2 = "10";
            }

            string strSQL =
            " select ItemCode,ItemName from asset where Head=1 and rowstatus>-1 and itemcode in ((select DISTINCT(SUBSTRING(ItemCode,8," + query + "))KodeAsset " +
            " from Asset where ID in (select ItemID from PakaiDetail where ItemTypeID=2 and GroupID=12 and PakaiID in (select ID from Pakai where Status>1)) " +
            " and RowStatus>-1)) and ItemCode in (select NoAsset from AM_AssetSerah where RowStatus>-1 and Apv=3) and head=1 and RowStatus>-1 ";
            //" union all " +
            //" select ItemCode,ItemName from asset where itemcode in ((select DISTINCT(ItemCode)KodeAsset from Asset where LEN(itemcode)=" + query + " and  " +
            //" ID in (select ItemID from PakaiDetail where ItemTypeID=2 and GroupID=4 and PakaiID in (select ID from Pakai where Status>1)) " +
            //" and RowStatus>-1)) and ItemCode  in (select NoAsset from AM_AssetSerah where RowStatus>-1 and Apv=2) ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObjectNamaAsset3(sqlDataReader));
                }
            }

            return arrData;
        }

        public ArrayList RetrieveNamaAsset5()
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string query = string.Empty; string query2 = string.Empty;
            if (users.UnitKerjaID.ToString().Length == 1)
            {
                query = "13"; query2 = "9";
            }
            else
            {
                query = "14"; query2 = "10";
            }

            string strSQL =
            " select ItemCode,ItemName from asset where itemcode in ((select DISTINCT(SUBSTRING(ItemCode,8," + query + "))KodeAsset " +
            " from Asset where ID in (select ItemID from PakaiDetail where ItemTypeID=2 and GroupID=12 and PakaiID in (select ID from Pakai where Status>1)) " +
            " and RowStatus>-1)) and ItemCode in (select NoAsset from AM_AssetSerah where RowStatus>-1 and Apv=4) and head=1 and RowStatus>-1 ";
            //" union all " +
            //" select ItemCode,ItemName from asset where itemcode in ((select DISTINCT(ItemCode)KodeAsset " +
            //" from Asset where LEN(itemcode)=" + query + " and ID in (select ItemID from PakaiDetail where ItemTypeID=2 and GroupID=4 and PakaiID in (select ID from Pakai where Status>1)) " +
            //" and RowStatus>-1)) and ItemCode in (select NoAsset from AM_AssetSerah where RowStatus>-1 and Apv=2) ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObjectNamaAsset3(sqlDataReader));
                }
            }

            return arrData;
        }

        private AssetDomainSerah GenerateObjectNamaAsset3(SqlDataReader sdr)
        {
            AssetDomainSerah objAsset = new AssetDomainSerah();
            objAsset.ItemCode = sdr["ItemCode"].ToString();
            objAsset.ItemName = sdr["ItemName"].ToString();
            return objAsset;
        }

        public AssetDomainSerah RetrieveDataAsset(string KodeAsset)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string result = string.Empty; string query = string.Empty;
            if (users.UnitKerjaID.ToString().Length == 1)
            {
                query = "13";
            }
            else
            {
                query = "14";
            }
            string StrSql =
            //" select ItemCode,left(convert(char,createdtime,113),20)TglMulai from Asset where ItemCode='" + KodeAsset + "' and rowstatus>-1 ";

            " select SUBSTRING(TglMulai,4,10)TglMulai,'" + KodeAsset + "'ItemCode from (select left(convert(char,xx1.PakaiDate,113),11)TglMulai from " +
            " (select top 1 PakaiID from PakaiDetail where ItemID in (select ID from Asset where ItemCode like'%" + KodeAsset + "%' and Aktif=1 and Head=0) " +
            //" or ItemID in (select ID from Asset where SUBSTRING(ItemCode,1,"+query+")='" + KodeAsset + "') " +
            " and RowStatus>-1 and GroupID in (12) and ItemTypeID=2 order by ID ) as xx inner join Pakai as xx1 ON xx.PakaiID=xx1.ID where xx1.Status>-1 " +
            " and xx1.ItemTypeID=2) as xx2 ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectDataAsset(sqlDataReader);
                }
            }

            return new AssetDomainSerah();
        }

        public AssetDomainSerah RetrieveDataAsset2(string KodeAsset)
        {
            Users users = (Users)HttpContext.Current.Session["Users"]; string query = string.Empty;
            if (users.UnitKerjaID.ToString().Length == 1)
            {
                query = "9";
            }
            else
            {
                query = "10";
            }
            string result = string.Empty;
            string StrSql =
            " select *,(select A.DeptID_ID from AM_Department A where A.DeptID=data.AM_DeptID)DeptID from " +
            " (select ID AssetID,ItemCode,ItemName,UOMID,AMgroupID,AMclassID,AMsubClassID,AMlokasiID,(substring(itemcode," + query + ",1))AM_DeptID," +
            " left(convert(char,createdtime,113),20)TglMulai from Asset where ItemCode='" + KodeAsset + "' and rowstatus>-1 and Head=1 ) as Data ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectDataAsset2(sqlDataReader);
                }
            }

            return new AssetDomainSerah();
        }

        private AssetDomainSerah GenerateObjectDataAsset2(SqlDataReader sdr)
        {
            AssetDomainSerah objAsset = new AssetDomainSerah();

            objAsset.AssetID = Convert.ToInt32(sdr["AssetID"]);
            objAsset.ItemCode = sdr["ItemCode"].ToString();
            objAsset.ItemName = sdr["ItemName"].ToString();
            objAsset.UomID = Convert.ToInt32(sdr["UomID"]);
            objAsset.AMGroupID = Convert.ToInt32(sdr["AMGroupID"]);
            objAsset.AMclassID = Convert.ToInt32(sdr["AMclassID"]);
            objAsset.AMsubClassID = Convert.ToInt32(sdr["AMsubClassID"]);
            objAsset.AMlokasiID = Convert.ToInt32(sdr["AMlokasiID"]);
            objAsset.AM_DeptID = Convert.ToInt32(sdr["AM_DeptID"]);
            objAsset.TglMulai = sdr["TglMulai"].ToString();
            objAsset.DeptID = Convert.ToInt32(sdr["DeptID"]);

            return objAsset;
        }

        private AssetDomainSerah GenerateObjectDataAsset(SqlDataReader sdr)
        {
            AssetDomainSerah objAsset = new AssetDomainSerah();

            objAsset.ItemCode = sdr["ItemCode"].ToString();
            objAsset.TglMulai = sdr["TglMulai"].ToString();

            return objAsset;
        }

        public AssetDomainSerah RetrieveNamaDept(string KodeDept)
        {
            string result = string.Empty;
            string StrSql =
            " select NamaDept from AM_Department where DeptID=" + KodeDept + " and RowStatus>-1 ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectNamaDept(sqlDataReader);
                }
            }

            return new AssetDomainSerah();
        }

        private AssetDomainSerah GenerateObjectNamaDept(SqlDataReader sdr)
        {
            AssetDomainSerah objAsset = new AssetDomainSerah();

            objAsset.NamaDept = sdr["NamaDept"].ToString();
            //objAsset.TglMulai = sdr["TglMulai"].ToString();

            return objAsset;
        }

        public ArrayList RetrieveKomponenAsset0(string KodeAsset)
        {
            string strSQL =
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempSerah]') AND type in (N'U')) DROP TABLE [dbo].[TempSerah]  " +

            " select ItemCode,ItemName,UomCode,QtyPakai,isnull(Price,0)Price,QtyPakai*Price TotalPrice into TempSerah  from (  " +
            " select ID,GroupID,ItemCode,ItemName,UomCode,QtySPP,QtyBeli,QtyAdjust,Price,Price*(QtyBeli+QtyAdjust) TotalPrice,QtyPakai from ( " +
            " select Data.ID,Data.GroupID,ItemCode,ItemName,UomCode,QtySPP,QtyBeli,QtyAdjust,isnull(TotalPrice/(QtyBeli+QtyAdjust),0) Price,SUM(pk.Quantity) QtyPakai from ( " +
            " select ID,GroupID,ItemCode,ItemName,UomCode,sum(QtySPP)QtySPP,sum(QtyBeli)QtyBeli,sum(QtyAdjust)QtyAdjust,sum((QtyBeli+QtyAdjust)*Price) TotalPrice from ( " +
            " select xx.*,sp.Quantity QtySPP,sum(rcp.Quantity)QtyBeli,'0'QtyAdjust, " +
            " case when po.Crc=1 then pod.Price else (select isnull(mk.Kurs*rcp.Price,0) from MataUangKurs mk where mk.MUID=po.Crc and mk.drTgl=rc.ReceiptDate) end Price from ( " +
            " select ID,GroupID,ItemCode,ItemName,(select A.UOMDesc from UOM A where A.ID=UOMID and RowStatus>-1)UomCode  " +
            " from Asset where ItemCode like'%" + KodeAsset + "%' and Head in (0,2) and Aktif=1 ) as xx  " +
            " left join SPPDetail sp ON sp.ItemID=xx.ID and sp.GroupID=xx.GroupID and sp.Status>-1  " +
            " inner join SPP spp ON sp.SPPID=spp.ID and spp.Status>-1 " +
            " left join ReceiptDetail rcp ON rcp.SPPID=spp.ID and rcp.ItemID=sp.ItemID and rcp.RowStatus>-1 " +
            " inner join Receipt rc ON rc.ID=rcp.ReceiptID and rc.Status>-1 " +
            " left Join POPurchnDetail pod ON pod.ID=rcp.PODetailID and pod.Status>-1 and pod.ItemTypeID=sp.ItemTypeID " +
            " inner join POPurchn po ON po.ID=pod.POID and po.Status>-1 " +
            " group by xx.ID,xx.GroupID,xx.ItemCode,xx.ItemName,xx.UomCode, sp.Quantity,rcp.Price,pod.Price,po.Crc,rc.ReceiptDate " +

            " union all " +

            " select B.ItemID,B.GroupID,C.ItemCode,C.ItemName,D.UOMCode,'0'QtySPP,'0'QtyBeli,B.Quantity QtyAdjust,isnull(B.AvgPrice,0)Price " +
            " from Adjust A inner join AdjustDetail B ON A.ID=B.AdjustID inner join Asset C ON C.ID=B.ItemID and C.GroupID=B.GroupID " +
            " inner join UOM D ON D.ID=C.UOMID " +
            " where B.ItemID in (select ID from Asset where ItemCode like'%" + KodeAsset + "%' and Head in (0,2) and Aktif=1 and RowStatus>-1) ) as x " +
            " group by ID,GroupID,ItemCode,ItemName,UomCode   " +
            " ) as Data left join PakaiDetail pk ON pk.ItemID=Data.ID and pk.GroupID=Data.GroupID  where pk.PakaiID in (select ID from Pakai where Status>1) " +
            " and pk.RowStatus>-1 " +
            " group by Data.ID,Data.GroupID,Data.ItemCode,Data.ItemName,Data.UomCode,QtySPP,QtyBeli,QtyAdjust,TotalPrice  ) as DataX " +
            " group by ID,GroupID,ItemCode,ItemName,UomCode,QtyPakai,QtyAdjust,QtySPP ,QtyAdjust,QtyBeli,Price) as DataFinal " +

        //" select * from TempSerah " +
            #region Wo Acc Filter Ret off (Fajri)
        " SELECT itemcode, itemname, uomcode, qtypakaiadj qtypakai, price, (qtypakaiadj * price) totalprice FROM (select *, CASE WHEN qtyadjustoutnonstok IS NOT NULL THEN qtypakai - qtyadjustoutnonstok ELSE qtypakai END QtyPakaiAdj from TempSerah ts " +
            " LEFT JOIN(SELECT ItemCode Itemcodeadj, sum(quantity) QtyAdjustOutNonStok FROM Adjust a, Adjustdetail b, Asset c " +
            " WHERE a.ID = b.AdjustID AND b.itemid = c.id " +
            " AND b.RowStatus > -1 AND status = 1 AND nonstok = 1" +
            " GROUP BY itemcode) adj ON ts.itemcode = adj.itemcodeadj) tempasset " +
            #endregion

     " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempSerah]') AND type in (N'U')) DROP TABLE [dbo].[TempSerah]  ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObjectKomponenAsset(sqlDataReader));
                }
            }

            return arrData;
        }

        public ArrayList RetrieveKomponenAsset(string KodeAsset)
        {
            string query = string.Empty; string querylama = string.Empty;

            #region Rev.0
            querylama =
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempSerah]') AND type in (N'U')) DROP TABLE [dbo].[TempSerah]  " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempSerah_F]') AND type in (N'U')) DROP TABLE [dbo].[TempSerah_F]  " +

            " ;with data_0 as ( " +
            " select ReceiptDetailID,ID,GroupID,ItemCode,ItemName,UomCode,sum(QtySPP)QtySPP,sum(QtyBeli)QtyBeli,sum(QtyAdjust)QtyAdjust,Price from ( " +
            " select ReceiptDetailID,ID,GroupID,ItemCode,ItemName,UomCode,sum(QtySPP)QtySPP,sum(QtyBeli)QtyBeli,QtyAdjust,cast(Price as decimal(18,0))Price " +
            " from (  select xx.*,rcp.ID ReceiptDetailID,sp.Quantity QtySPP,sum(rcp.Quantity)QtyBeli,'0'QtyAdjust,  case when po.Crc=1 then pod.Price " +
            " else (select isnull(mk.Kurs*rcp.Price,0) from MataUangKurs mk where mk.MUID=po.Crc and mk.drTgl=rc.ReceiptDate) end Price " +
            " from (  select ID,GroupID,ItemCode,ItemName,(select A.UOMDesc from UOM A where A.ID=UOMID and RowStatus>-1)UomCode  " +
            " from Asset where ItemCode like'%" + KodeAsset + "%' and Head in (0,2) and Aktif=1 ) as xx   " +
            " left join SPPDetail sp ON sp.ItemID=xx.ID and sp.GroupID=xx.GroupID and sp.Status>-1   " +
            " inner join SPP spp ON sp.SPPID=spp.ID and spp.Status>-1  " +
            " left join ReceiptDetail rcp ON rcp.SPPID=spp.ID and rcp.ItemID=sp.ItemID and rcp.RowStatus>-1 " +
            " inner join Receipt rc ON rc.ID=rcp.ReceiptID and rc.Status>-1  " +
            " left Join POPurchnDetail pod ON pod.ID=rcp.PODetailID and pod.Status>-1 and pod.ItemTypeID=sp.ItemTypeID " +
            " inner join POPurchn po ON po.ID=pod.POID and po.Status>-1  group by xx.ID,xx.GroupID,xx.ItemCode,xx.ItemName,xx.UomCode, " +
            " sp.Quantity,rcp.Price,pod.Price,po.Crc,rc.ReceiptDate,rcp.ID   ) as xa " +
            " group by ID,GroupID,ItemCode,ItemName,UomCode,Price,QtyAdjust,ReceiptDetailID    " +
            " union all    " +
            " select '0'ReceiptDetailID,B.ItemID,B.GroupID,C.ItemCode,C.ItemName,D.UOMCode,'0'QtySPP,'0'QtyBeli,B.Quantity QtyAdjust,isnull(B.AvgPrice,0)Price " +
            " from Adjust A inner join AdjustDetail B ON A.ID=B.AdjustID inner join Asset C ON C.ID=B.ItemID and C.GroupID=B.GroupID " +
            " inner join UOM D ON D.ID=C.UOMID  where B.ItemID in (select ID from Asset where ItemCode like'%" + KodeAsset + "%' and Head in (0,2) and Aktif=1 " +
            " and RowStatus>-1) and A.AdjustType='Tambah') as x  group by ID,GroupID,ItemCode,ItemName,UomCode,Price ,ReceiptDetailID  " +
            " ) , " +

            " data_1 as (select pk.ID PakaiDetailID,A.ItemCode,A.ItemName,pk.Quantity QtyPakai from data_0 A left join PakaiDetail pk ON pk.ItemID=A.ID " +
            " and pk.GroupID=A.GroupID and pk.groupID=12  where pk.PakaiID in (select ID from Pakai where Status>1)  and pk.RowStatus>-1  " +
            " group by A.ItemCode,A.ItemName ,pk.ID,pk.Quantity), " +
            " data_2 as (select ItemCode,ItemName,sum(QtyPakai)QtyPakai from data_1 group by ItemCode,ItemName), " +

            " data_5 as (select Price,itemname from data_0 group by price,itemname), " +

            " data_6 as (select ReceiptDetailID,Price,ItemCode,itemname,UomCode,QtySPP,QtyBeli,QtyAdjust " +
            " from data_0 group by price,itemname,ItemCode,UomCode,QtySPP,QtyBeli,QtyAdjust,ReceiptDetailID), " +

            " data_60 as (select sum(Price*QtyBeli) ttlHarga,ItemCode,ItemName,UomCode,sum(QtySPP)QtySPP,sum(QtyBeli)QtyBeli,sum(QtyAdjust)QtyAdjust,ttl " +
            " from data_6 group by ItemCode,ItemName,UomCode,ttl), " +

            //" data_7 as ( select A.ItemCode,A.ItemName,A.UomCode,A.QtySPP,A.QtyBeli,A.QtyAdjust,(select cast(sum(Price)/count(Price) as decimal(18,0)) " +
            //" from data_5 A1 where A1.ItemName=A.ItemName)Price from data_6 A left join data_5 B ON A.ItemName=B.ItemName " +
            //" group by A.ItemCode,A.ItemName,A.UomCode,A.QtySPP,A.QtyBeli,A.QtyAdjust,A.ReceiptDetailID), " +

            " data_7 as (select ItemCode,ItemName,UomCode,QtySPP,QtyBeli,QtyAdjust,isnull((ttlHarga/nullif(QtyBeli,0)),0)PriceSatuan from data_60), " +

            " data_8 as ( select ItemCode,ItemName,UomCode,sum(QtySPP)QtySPP,sum(QtyBeli)QtyBeli,sum(QtyAdjust)QtyAdjust,Price from data_7 " +
            " group by ItemCode,ItemName,UomCode,Price ), " +

            " data_9 as (select A.*,B.QtyPakai from data_8 A left join data_2 B ON A.ItemCode=B.ItemCode " +
            " group by A.ItemCode,A.ItemName,A.UomCode,A.QtySPP,A.QtyBeli,A.QtyAdjust,A.Price,B.QtyPakai), " +

            " data_10 as (select ItemCode,ItemName,UomCode,QtyPakai,Price,QtyPakai*Price TotalPrice from data_9) " +

            " select * into TempSerah  from data_10 " +

            " select ItemCode,ItemName,UomCode,case when QtyPakaiAdj=0 then 0 else QtyPakaiAdj end QtyPakai, " +
            " case when QtyPakaiAdj=0 then 0 else  Price end Price,totalprice into TempSerah_F " +
            " from (  SELECT itemcode, itemname, uomcode,QtyPakai, qtypakaiadj , price, (qtypakaiadj * price) totalprice " +
            " FROM (select *, CASE WHEN qtyadjustoutnonstok IS NOT NULL THEN qtypakai - qtyadjustoutnonstok ELSE qtypakai END QtyPakaiAdj " +
            " from TempSerah ts  LEFT JOIN(SELECT ItemCode Itemcodeadj, sum(quantity) QtyAdjustOutNonStok FROM Adjust a, Adjustdetail b, Asset c " +
            " WHERE a.ID = b.AdjustID AND b.itemid = c.id  AND b.RowStatus > -1 AND status = 1 AND nonstok = 1 GROUP BY itemcode )  adj " +
            " ON ts.itemcode = adj.itemcodeadj ) tempasset ) as x  " +

            " select ItemCode,ItemName,UomCode,sum(QtyPakai)QtyPakai,Price,sum(totalprice)TotalPrice " +
            " from TempSerah_F group by ItemCode,ItemName,UomCode,Price ";
            #endregion

            #region Rev.1 added by : beny 26 Februari 2022       
            query =
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempSerah]') AND type in (N'U')) DROP TABLE [dbo].[TempSerah] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempSerah_F]') AND type in (N'U')) DROP TABLE [dbo].[TempSerah_F] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempSerah_Final]') AND type in (N'U')) DROP TABLE [dbo].[TempSerah_Final] " +

            " ;with data_0 as (  select ReceiptDetailID,ID,GroupID,ItemCode,ItemName,UomCode,sum(QtySPP)QtySPP,sum(QtyBeli)QtyBeli,sum(QtyAdjust)QtyAdjust, " +
            " Price from (  select ReceiptDetailID,ID,GroupID,ItemCode,ItemName,UomCode,sum(QtySPP)QtySPP,sum(QtyBeli)QtyBeli,QtyAdjust, " +
            " cast(Price as decimal(18,0))Price  " +
            " from (  select xx.*,rcp.ID ReceiptDetailID,sp.Quantity QtySPP,sum(rcp.Quantity)QtyBeli,'0'QtyAdjust,case when po.Crc=1 then pod.Price " +
            " else (select isnull(mk.Kurs*rcp.Price,0) from MataUangKurs mk where mk.MUID=po.Crc and mk.drTgl=rc.ReceiptDate) end Price  " +
            " from (  select ID,GroupID,ItemCode,ItemName,(select A.UOMDesc from UOM A where A.ID=UOMID and RowStatus>-1)UomCode " +
            " from Asset where ItemCode like'%" + KodeAsset + "%' and Head in (0,2) and Aktif=1 ) as xx " +
            " left join SPPDetail sp ON sp.ItemID=xx.ID and sp.GroupID=xx.GroupID and sp.Status>-1 " +
            " inner join SPP spp ON sp.SPPID=spp.ID and spp.Status>-1 " +
            " left join ReceiptDetail rcp ON rcp.SPPID=spp.ID and rcp.ItemID=sp.ItemID and rcp.RowStatus>-1 " +
            " inner join Receipt rc ON rc.ID=rcp.ReceiptID and rc.Status>-1 " +
            " left Join POPurchnDetail pod ON pod.ID=rcp.PODetailID and pod.Status>-1 and pod.ItemTypeID=sp.ItemTypeID " +
            " inner join POPurchn po ON po.ID=pod.POID and po.Status>-1  group by xx.ID,xx.GroupID,xx.ItemCode,xx.ItemName,xx.UomCode, " +
            " sp.Quantity,rcp.Price,pod.Price,po.Crc,rc.ReceiptDate,rcp.ID   ) as xa  " +
            " group by ID,GroupID,ItemCode,ItemName,UomCode,Price,QtyAdjust,ReceiptDetailID " +

            " union all " +

            " select '0'ReceiptDetailID,B.ItemID,B.GroupID,C.ItemCode,C.ItemName,D.UOMCode,'0'QtySPP,'0'QtyBeli,B.Quantity QtyAdjust,isnull(B.AvgPrice,0)Price " +
            " from Adjust A " +
            " inner join AdjustDetail B ON A.ID=B.AdjustID " +
            " inner join Asset C ON C.ID=B.ItemID and C.GroupID=B.GroupID " +
            " inner join UOM D ON D.ID=C.UOMID  where B.ItemID in (select ID from Asset where ItemCode like'%" + KodeAsset + "%' and Head in (0,2) and Aktif=1 " +
            " and RowStatus>-1) and A.AdjustType='Tambah') as x " +
            " group by ID,GroupID,ItemCode,ItemName,UomCode,Price ,ReceiptDetailID   ) , " +

            " data_1 as (select pk.ID PakaiDetailID,A.ItemCode,A.ItemName,pk.Quantity QtyPakai from data_0 A " +
            " left join PakaiDetail pk ON pk.ItemID=A.ID  and pk.GroupID=A.GroupID and pk.groupID=12  " +
            " where pk.PakaiID in (select ID from Pakai where Status>1)  and pk.RowStatus>-1 group by A.ItemCode,A.ItemName ,pk.ID,pk.Quantity), " +

            " data_2 as (select ItemCode,ItemName,sum(QtyPakai)QtyPakai from data_1 group by ItemCode,ItemName), " +

            " data_5 as (select Price,itemname from data_0 group by price,itemname), " +

            " data_6 as (select ReceiptDetailID,Price,ItemCode,itemname,UomCode,QtySPP,QtyBeli,QtyAdjust,(select count(ItemCode)ttl from data_0 b " +
            " where b.ItemCode=a.ItemCode)ttl  from data_0 a group by price,itemname,ItemCode,UomCode,QtySPP,QtyBeli,QtyAdjust,ReceiptDetailID),  " +

            " data_60 as (select /**sum(Price*QtyBeli) ttlHarga**/case when sum(QtyAdjust)>0 then sum(Price*QtyAdjust) else sum(Price*QtyBeli) end ttlHarga,ItemCode,ItemName,UomCode,sum(QtySPP)QtySPP,sum(QtyBeli)QtyBeli,sum(QtyAdjust)QtyAdjust,ttl " +
            " from data_6 group by ItemCode,ItemName,UomCode,ttl), " +

            //" data_7 as ( select A.ItemCode,A.ItemName,A.UomCode,sum(A.QtySPP)QtySPP,sum(A.QtyBeli)QtyBeli,sum(A.QtyAdjust)QtyAdjust, "+
            //" cast(sum(((A.Price/ttl))) as decimal(18,0))PriceSatuan from data_6 A group by A.ItemCode,A.ItemName,A.UomCode), "+

            " data_7 as (select ItemCode,ItemName,UomCode,QtySPP,QtyBeli,QtyAdjust,/**isnull((ttlHarga/nullif(QtyBeli,0)),0)PriceSatuan**/ case when QtyAdjust>0 then isnull((ttlHarga/nullif(QtyAdjust,0)),0) else isnull((ttlHarga/nullif(QtyBeli,0)),0) end PriceSatuan from data_60), " +

            " data_8 as ( select ItemCode,ItemName,UomCode,QtySPP,QtyBeli,QtyAdjust,(nullif(PriceSatuan,2))Price from data_7  " +
            " group by ItemCode,ItemName,UomCode,PriceSatuan,QtySPP,QtyBeli,QtyAdjust ), " +

            " data_9 as (select A.*,A.QtyBeli+A.QtyAdjust QtyStok,B.QtyPakai from data_8 A left join data_2 B ON A.ItemCode=B.ItemCode " +
            " group by A.ItemCode,A.ItemName,A.UomCode,A.QtySPP,A.QtyBeli,A.QtyAdjust,A.Price,B.QtyPakai),  " +

            " data_10 as (select ItemCode,ItemName,UomCode,QtyStok,QtyPakai,Price,/**Price*QtyBeli TotalPrice**/ case when QtyAdjust>0 then Price*QtyAdjust else Price*QtyBeli end TotalPrice from data_9) , " +

            " data_11 as ( " +
            " select ItemCode,ItemName,UomCode,QtyPakai,cast(Price as decimal(18,0))Price,cast(totalprice as decimal(18,0))TotalPrice from data_10) " +

            " select * into TempSerah  from data_11  " +

            //" select ItemCode,ItemName,UomCode,case when QtyPakaiAdj=0 then QtyPakai else QtyPakai - QtyPakaiAdj end QtyPakai,Price,totalprice " +
            //" into TempSerah_F  from (  " +
            //" SELECT itemcode, itemname, uomcode,QtyPakai, qtypakaiadj ,price,totalprice FROM ( " +
            //" select *, CASE WHEN qtyadjustoutnonstok IS NOT NULL THEN QtyPakai - qtyadjustoutnonstok ELSE '0' END QtyPakaiAdj " +
            //" from TempSerah ts  LEFT JOIN (SELECT ItemCode Itemcodeadj, sum(quantity) QtyAdjustOutNonStok FROM Adjust a, Adjustdetail b, Asset c " +
            //" WHERE a.ID = b.AdjustID AND b.itemid = c.id  AND b.RowStatus > -1 AND status = 1 AND nonstok = 1 GROUP BY itemcode,b.AvgPrice ) " +
            //" adj  ON ts.itemcode = adj.itemcodeadj ) tempasset ) as x   " +
            " select ItemCode,ItemName,UomCode,QtyPakai,Price,totalprice  into TempSerah_F  " +
            " from (   SELECT itemcode, itemname, uomcode,case when QtyAdjustOutNonStok>0 then QtyPakai-QtyAdjustOutNonStok else QtyPakai end QtyPakai, " +
            " price,totalprice FROM (  select itemcode, itemname, uomcode,QtyPakai, price,totalprice,isnull(itemcodeadj,'')itemcodeadj, " +
            " isnull(QtyAdjustOutNonStok,0)QtyAdjustOutNonStok from TempSerah ts  LEFT JOIN (SELECT ItemCode Itemcodeadj, isnull(sum(quantity),0) " +
            " QtyAdjustOutNonStok FROM Adjust a, Adjustdetail b, Asset c  WHERE a.ID = b.AdjustID AND b.itemid = c.id and b.GroupID=12  " +
            " AND b.RowStatus > -1 AND status = 1 AND nonstok = 1 GROUP BY itemcode,b.AvgPrice )  adj  ON ts.itemcode = adj.itemcodeadj ) tempasset ) as x " +
            " where QtyPakai>0 " +


            " select ItemCode,ItemName,UomCode,sum(QtyPakai)QtyPakai,Price,TotalPrice into TempSerah_Final " +
            " from TempSerah_F   group by ItemCode,ItemName,UomCode,Price ,TotalPrice " +

            " select * from TempSerah_Final order by ItemCode ";
            #endregion

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(query);
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObjectKomponenAsset(sqlDataReader));
                }
            }

            return arrData;

        }

        public ArrayList RetrieveKomponenAsset_lama(string KodeAsset)
        {

            string strSQL =
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempSerah]') AND type in (N'U')) DROP TABLE [dbo].[TempSerah]  " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempSerah_F]') AND type in (N'U')) DROP TABLE [dbo].[TempSerah_F]  " +

            " select ItemCode,ItemName,UomCode,QtyPakai,isnull(Price,0)Price,QtyPakai*Price TotalPrice into TempSerah  from (  " +
            " select ID,GroupID,ItemCode,ItemName,UomCode,QtySPP,QtyBeli,QtyAdjust,Price,Price*(QtyBeli+QtyAdjust) TotalPrice,QtyPakai from ( " +
            " select Data.ID,Data.GroupID,ItemCode,ItemName,UomCode,QtySPP,QtyBeli,QtyAdjust,isnull(TotalPrice/(QtyBeli+QtyAdjust),0) Price,SUM(pk.Quantity) QtyPakai from ( " +
            " select ID,GroupID,ItemCode,ItemName,UomCode,sum(QtySPP)QtySPP,sum(QtyBeli)QtyBeli,sum(QtyAdjust)QtyAdjust, " +
            " /**sum((QtyBeli+QtyAdjust)*Price) TotalPrice**/ sum((QtyBeli+QtyAdjust))*(cast(sum(Price)/count(ItemName) as decimal(18,2))) TotalPrice from ( " +
            " select ID,GroupID,ItemCode,ItemName,UomCode,sum(QtySPP)QtySPP,sum(QtyBeli)QtyBeli,QtyAdjust,Price from ( " +
            " select xx.*,sp.Quantity QtySPP,sum(rcp.Quantity)QtyBeli,'0'QtyAdjust, " +
            " case when po.Crc=1 then pod.Price else (select isnull(mk.Kurs*rcp.Price,0) from MataUangKurs mk where mk.MUID=po.Crc and mk.drTgl=rc.ReceiptDate) end Price from ( " +
            " select ID,GroupID,ItemCode,ItemName,(select A.UOMDesc from UOM A where A.ID=UOMID and RowStatus>-1)UomCode  " +
            " from Asset where ItemCode like'%" + KodeAsset + "%' and Head in (0,2) and Aktif=1 ) as xx  " +
            " left join SPPDetail sp ON sp.ItemID=xx.ID and sp.GroupID=xx.GroupID and sp.Status>-1  " +
            " inner join SPP spp ON sp.SPPID=spp.ID and spp.Status>-1 " +
            " left join ReceiptDetail rcp ON rcp.SPPID=spp.ID and rcp.ItemID=sp.ItemID and rcp.RowStatus>-1 " +
            " inner join Receipt rc ON rc.ID=rcp.ReceiptID and rc.Status>-1 " +
            " left Join POPurchnDetail pod ON pod.ID=rcp.PODetailID and pod.Status>-1 and pod.ItemTypeID=sp.ItemTypeID " +
            " inner join POPurchn po ON po.ID=pod.POID and po.Status>-1 " +
            " group by xx.ID,xx.GroupID,xx.ItemCode,xx.ItemName,xx.UomCode, sp.Quantity,rcp.Price,pod.Price,po.Crc,rc.ReceiptDate " +
            "  ) as xa  group by ID,GroupID,ItemCode,ItemName,UomCode,Price,QtyAdjust " +

            " union all " +

            " select B.ItemID,B.GroupID,C.ItemCode,C.ItemName,D.UOMCode,'0'QtySPP,'0'QtyBeli,B.Quantity QtyAdjust,isnull(B.AvgPrice,0)Price " +
            " from Adjust A inner join AdjustDetail B ON A.ID=B.AdjustID inner join Asset C ON C.ID=B.ItemID and C.GroupID=B.GroupID " +
            " inner join UOM D ON D.ID=C.UOMID " +
            " where B.ItemID in (select ID from Asset where ItemCode like'%" + KodeAsset + "%' and Head in (0,2) and Aktif=1 and RowStatus>-1) and A.AdjustType='Tambah' ) as x " +
            " group by ID,GroupID,ItemCode,ItemName,UomCode   " +
            " ) as Data left join PakaiDetail pk ON pk.ItemID=Data.ID and pk.GroupID=Data.GroupID  where pk.PakaiID in (select ID from Pakai where Status>1) " +
            " and pk.RowStatus>-1 " +
            " group by Data.ID,Data.GroupID,Data.ItemCode,Data.ItemName,Data.UomCode,QtySPP,QtyBeli,QtyAdjust,TotalPrice  ) as DataX " +
            " group by ID,GroupID,ItemCode,ItemName,UomCode,QtyPakai,QtyAdjust,QtySPP ,QtyAdjust,QtyBeli,Price) as DataFinal " +

        //" select * from TempSerah " +
            #region Wo Acc Filter Ret off (Fajri)
        " /** SELECT itemcode, itemname, uomcode, qtypakaiadj qtypakai, price, (qtypakaiadj * price) totalprice FROM (select *, CASE WHEN qtyadjustoutnonstok IS NOT NULL THEN qtypakai - qtyadjustoutnonstok ELSE qtypakai END QtyPakaiAdj from TempSerah ts " +
            " LEFT JOIN(SELECT ItemCode Itemcodeadj, sum(quantity) QtyAdjustOutNonStok FROM Adjust a, Adjustdetail b, Asset c " +
            " WHERE a.ID = b.AdjustID AND b.itemid = c.id " +
            " AND b.RowStatus > -1 AND status = 1 AND nonstok = 1" +
            " GROUP BY itemcode) adj ON ts.itemcode = adj.itemcodeadj) tempasset **/ " +
            #endregion

            #region beny
        " select ItemCode,ItemName,UomCode,case when QtyPakaiAdj=0 then 0 else QtyPakaiAdj end QtyPakai, " +
            " case when QtyPakaiAdj=0 then 0 else  Price end Price,totalprice into TempSerah_F from ( " +
            " SELECT itemcode, itemname, uomcode,QtyPakai, qtypakaiadj , price, (qtypakaiadj * price) totalprice " +
            " FROM (select *, CASE WHEN qtyadjustoutnonstok IS NOT NULL THEN qtypakai - qtyadjustoutnonstok ELSE qtypakai END QtyPakaiAdj " +
            " from TempSerah ts  LEFT JOIN(SELECT ItemCode Itemcodeadj, sum(quantity) QtyAdjustOutNonStok FROM Adjust a, Adjustdetail b, Asset c  " +
            " WHERE a.ID = b.AdjustID AND b.itemid = c.id  AND b.RowStatus > -1 AND status = 1 AND nonstok = 1 GROUP BY itemcode ) " +
            " adj ON ts.itemcode = adj.itemcodeadj ) tempasset ) as x " +

            " select * from TempSerah_F where QtyPakai>0 ";
            #endregion

            //" IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempSerah]') AND type in (N'U')) DROP TABLE [dbo].[TempSerah]  " +
            //" IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempSerah_F]') AND type in (N'U')) DROP TABLE [dbo].[TempSerah_F]  ";


            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObjectKomponenAsset(sqlDataReader));
                }
            }

            return arrData;
        }

        private AssetDomainSerah GenerateObjectKomponenAsset(SqlDataReader sdr)
        {
            AssetDomainSerah objAsset = new AssetDomainSerah();
            objAsset.ItemCode = sdr["ItemCode"].ToString();
            objAsset.ItemName = sdr["ItemName"].ToString();
            objAsset.ItemName = sdr["ItemName"].ToString();
            objAsset.UomCode = sdr["UomCode"].ToString();
            objAsset.QtyPakai = Convert.ToDecimal(sdr["QtyPakai"]);
            objAsset.TotalPrice = Convert.ToDecimal(sdr["TotalPrice"]);
            objAsset.Price = Convert.ToDecimal(sdr["Price"]);

            return objAsset;
        }

        public decimal RetrieveTotalPrice(string KodeAsset)
        {
            string hasil = string.Empty;
            string StrSql =
            //" IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempSerah1]') AND type in (N'U')) DROP TABLE [dbo].[TempSerah1]  " +
            //" IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempSerah_F1]') AND type in (N'U')) DROP TABLE [dbo].[TempSerah_F1] " +

            //" select ItemCode,ItemName,UomCode,QtyPakai,isnull(Price,0)Price,QtyPakai*Price TotalPrice into TempSerah1  from (  " +
            //" select ID,GroupID,ItemCode,ItemName,UomCode,QtySPP,QtyBeli,QtyAdjust,Price,Price*(QtyBeli+QtyAdjust) TotalPrice,QtyPakai from ( " +
            //" select Data.ID,Data.GroupID,ItemCode,ItemName,UomCode,QtySPP,QtyBeli,QtyAdjust,isnull(TotalPrice/(QtyBeli+QtyAdjust),0) Price,SUM(pk.Quantity) QtyPakai from ( " +
            //" select ID,GroupID,ItemCode,ItemName,UomCode,sum(QtySPP)QtySPP,sum(QtyBeli)QtyBeli,sum(QtyAdjust)QtyAdjust,/**sum((QtyBeli+QtyAdjust)*Price) TotalPrice**/ "+
            ////" sum((QtyBeli+QtyAdjust))*(sum(Price)/count(ItemName)) TotalPrice from ( " +
            //" sum((QtyBeli+QtyAdjust))*(cast(sum(Price)/count(ItemName) as decimal(18,2))) TotalPrice from (  " +
            //" select ID,GroupID,ItemCode,ItemName,UomCode,sum(QtySPP)QtySPP,sum(QtyBeli)QtyBeli,QtyAdjust,Price from ( "+
            //" select xx.*,sp.Quantity QtySPP,sum(rcp.Quantity)QtyBeli,'0'QtyAdjust, " +
            //" case when po.Crc=1 then pod.Price else (select isnull(mk.Kurs*rcp.Price,0) from MataUangKurs mk where mk.MUID=po.Crc and mk.drTgl=rc.ReceiptDate) end Price from ( " +
            //" select ID,GroupID,ItemCode,ItemName,(select A.UOMDesc from UOM A where A.ID=UOMID and RowStatus>-1)UomCode  " +
            //" from Asset where ItemCode like'%" + KodeAsset + "%' and Head in (0,2) and Aktif=1 ) as xx  " +
            //" left join SPPDetail sp ON sp.ItemID=xx.ID and sp.GroupID=xx.GroupID and sp.Status>-1  " +
            //" inner join SPP spp ON sp.SPPID=spp.ID and spp.Status>-1 " +
            //" left join ReceiptDetail rcp ON rcp.SPPID=spp.ID and rcp.ItemID=sp.ItemID and rcp.RowStatus>-1 " +
            //" inner join Receipt rc ON rc.ID=rcp.ReceiptID and rc.Status>-1 " +
            //" left Join POPurchnDetail pod ON pod.ID=rcp.PODetailID and pod.Status>-1 and pod.ItemTypeID=sp.ItemTypeID " +
            //" inner join POPurchn po ON po.ID=pod.POID and po.Status>-1 " +
            //" group by xx.ID,xx.GroupID,xx.ItemCode,xx.ItemName,xx.UomCode, sp.Quantity,rcp.Price,pod.Price,po.Crc,rc.ReceiptDate " +
            //"  ) as xa  group by ID,GroupID,ItemCode,ItemName,UomCode,Price,QtyAdjust "+

            //" union all " +

            //" select B.ItemID,B.GroupID,C.ItemCode,C.ItemName,D.UOMCode,'0'QtySPP,'0'QtyBeli,B.Quantity QtyAdjust,isnull(B.AvgPrice,0)Price " +
            //" from Adjust A inner join AdjustDetail B ON A.ID=B.AdjustID inner join Asset C ON C.ID=B.ItemID and C.GroupID=B.GroupID " +
            //" inner join UOM D ON D.ID=C.UOMID " +
            //" where B.ItemID in (select ID from Asset where ItemCode like'%" + KodeAsset + "%' and Head in (0,2) and Aktif=1 and RowStatus>-1) ) as x " +
            //" group by ID,GroupID,ItemCode,ItemName,UomCode   " +
            //" ) as Data left join PakaiDetail pk ON pk.ItemID=Data.ID and pk.GroupID=Data.GroupID  where pk.PakaiID in (select ID from Pakai where Status>1) " +
            //" and pk.RowStatus>-1 " +
            //" group by Data.ID,Data.GroupID,Data.ItemCode,Data.ItemName,Data.UomCode,QtySPP,QtyBeli,QtyAdjust,TotalPrice  ) as DataX " +
            //" group by ID,GroupID,ItemCode,ItemName,UomCode,QtyPakai,QtyAdjust,QtySPP ,QtyAdjust,QtyBeli,Price) as DataFinal " +

            ////" select sum(TotalPrice)TotalPrice from TempSerah1 " +
            //" select ItemCode,ItemName,UomCode,case when QtyPakaiAdj=0 then 0 else QtyPakaiAdj end QtyPakai, " +
            //" case when QtyPakaiAdj=0 then 0 else  Price end Price,totalprice into TempSerah_F1 from ( " +
            //" SELECT itemcode, itemname, uomcode,QtyPakai, qtypakaiadj , price, (qtypakaiadj * price) totalprice " +
            //" FROM (select *, CASE WHEN qtyadjustoutnonstok IS NOT NULL THEN qtypakai - qtyadjustoutnonstok ELSE qtypakai END QtyPakaiAdj " +
            //" from TempSerah1 ts  LEFT JOIN(SELECT ItemCode Itemcodeadj, sum(quantity) QtyAdjustOutNonStok FROM Adjust a, Adjustdetail b, Asset c " +
            //" WHERE a.ID = b.AdjustID AND b.itemid = c.id  AND b.RowStatus > -1 AND status = 1 AND nonstok = 1 GROUP BY itemcode ) " +
            //" adj ON ts.itemcode = adj.itemcodeadj ) tempasset ) as x " +

            //" select sum(totalprice)TotalPrice from TempSerah_F1 where QtyPakai>0 " +

            " select sum(totalprice)TotalPrice from TempSerah_F where QtyPakai>0 " +

            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempSerah]') AND type in (N'U')) DROP TABLE [dbo].[TempSerah]  " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempSerah_F]') AND type in (N'U')) DROP TABLE [dbo].[TempSerah_F] ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToDecimal(sqlDataReader["TotalPrice"]);
                }
            }

            return 0;
        }

        public AssetDomainSerah RetrieveCekApv(string KodeAsset)
        {
            string result = string.Empty;
            string StrSql =
            " select top 1 *,left(convert(char,createdtime,113),11)TglSelesai1 from AM_AssetSerah where NoAsset='" + KodeAsset + "' and RowStatus>-1 order by ID desc";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectRetrieveCekApv(sqlDataReader);
                }
            }

            return new AssetDomainSerah();
        }

        private AssetDomainSerah GenerateObjectRetrieveCekApv(SqlDataReader sdr)
        {
            AssetDomainSerah objAsset = new AssetDomainSerah();
            objAsset.ID = Convert.ToInt32(sdr["ID"]);
            objAsset.Apv = Convert.ToInt32(sdr["Apv"]);
            objAsset.NoAsset = sdr["NoAsset"].ToString();
            objAsset.TglSelesaiPekerjaan = Convert.ToDateTime(sdr["TglSelesai"]);
            objAsset.TglSelesai1 = sdr["TglSelesai1"].ToString();
            objAsset.Upgrade = Convert.ToInt32(sdr["Upgrade"]);
            return objAsset;
        }

        public AssetDomainSerah RetrievePelaksana()
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string result = string.Empty; string query = string.Empty;
            if (users.UnitKerjaID.ToString().Length == 1)
            {
                query = "13";
            }
            else
            {
                query = "14";
            }

            string StrSql =
            //" select ItemCode,left(convert(char,createdtime,113),20)TglMulai from Asset where ItemCode='" + KodeAsset + "' and rowstatus>-1 ";
            " select distinct x01.DeptID,case when x01.DeptID=22 then '1' when x01.DeptID=30 then '2' when x01.DeptID=19 then '3' else '0' end urut from ( " +
            " select x2.AssetUtama,x22.ItemName,x2.CreatedBy from ( " +
            " select AssetUtama,CreatedBy from ( " +
            " select SUBSTRING(A1.ItemCode,8," + query + ")AssetUtama,x.CreatedBy from (select ItemID,GroupID,A.ItemTypeID,B.CreatedBy " +
            " from pakaidetail A inner join Pakai B ON A.PakaiID=B.ID where A.groupid=12 and A.rowstatus>-1 and A.ItemTypeID=2 and B.Status=3 ) as x " +
            " inner join Asset A1 ON A1.ID=x.ItemID  ) as x1 group by AssetUtama,CreatedBy ) as x2 inner join asset x22 ON x22.ItemCode=x2.AssetUtama " +
            " ) as x33 inner join users x01 ON x01.UserName=x33.CreatedBy order by urut ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectDeptID(sqlDataReader);
                }
            }

            return new AssetDomainSerah();
        }

        private AssetDomainSerah GenerateObjectDeptID(SqlDataReader sdr)
        {
            AssetDomainSerah objAsset = new AssetDomainSerah();
            objAsset.DeptID = Convert.ToInt32(sdr["DeptID"]);
            objAsset.Urut = Convert.ToInt32(sdr["Urut"]);
            return objAsset;
        }

        public int RetrieveDeptID_Eng(string Kode)
        {
            string StrSql =
            "  select B.DeptID from AM_AssetSerah A inner join users B ON A.CreatedBy=B.UserName where NoAsset='" + Kode + "' and A.RowStatus>-1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["DeptID"]);
                }
            }

            return 0;
        }

        public int RetrieveAssetAda(string KodeKompAsset)
        {
            string StrSql =
            //" select COUNT(KodeAsset)NoUrut  from AM_Asset where KodeAsset like'%" + NamaKompAsset + "%' and JenisAsset=2 and RowStatus>-1 ";
            " select sum(Ttl)Ttl  from ( " +
            " select count(ID)Ttl from AM_AssetSerah where NoAsset='" + KodeKompAsset + "' and RowStatus>-1 " +
            " union all " +
            " select 0 Ttl) as x ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Ttl"]);
                }
            }

            return 0;
        }

        public AssetDomainSerah RetrieveSerahAsset(string KodeAsset)
        {
            string StrSql =
            " select Apv StatusApv from AM_AssetSerah where NoAsset='" + KodeAsset + "' and rowstatus>-1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectSerahAsset(sqlDataReader);
                }
            }

            return new AssetDomainSerah();
        }

        private AssetDomainSerah GenerateObjectSerahAsset(SqlDataReader sdr)
        {
            AssetDomainSerah objAsset = new AssetDomainSerah();
            objAsset.StatusApv = Convert.ToInt32(sdr["StatusApv"]);

            return objAsset;
        }

        public AssetDomainSerah RetrieveDataPIC(string KodeAsset)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string result = string.Empty; string query = string.Empty;
            if (users.UnitKerjaID.ToString().Length == 1)
            {
                query = "13";
            }
            else
            {
                query = "14";
            }
            string StrSql =
            " select distinct x01.DeptID,case when x01.DeptID=22 then '1' when x01.DeptID=30 then '2' when x01.DeptID=19 then '3' else '0' end urut " +
            " from (  " +
            " select x2.AssetUtama,x22.ItemName,x2.CreatedBy from (select AssetUtama,CreatedBy " +
            " from (select SUBSTRING(A1.ItemCode,8," + query + ")AssetUtama,x.CreatedBy " +
            " from (select ItemID,GroupID,A.ItemTypeID,B.CreatedBy  from pakaidetail A " +
            " inner join Pakai B ON A.PakaiID=B.ID where A.groupid=12 and A.rowstatus>-1 and A.ItemTypeID=2 and B.Status=3 ) as x " +
            " inner join Asset A1 ON A1.ID=x.ItemID  ) as x1 group by AssetUtama,CreatedBy ) as x2 inner join asset x22 ON x22.ItemCode=x2.AssetUtama " +
            " where x2.AssetUtama='" + KodeAsset + "' " +
            " ) as x33 inner join users x01 ON x01.UserName=x33.CreatedBy order by urut ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectDataPIC(sqlDataReader);
                }
            }

            return new AssetDomainSerah();
        }

        private AssetDomainSerah GenerateObjectDataPIC(SqlDataReader sdr)
        {
            AssetDomainSerah objAsset = new AssetDomainSerah();
            objAsset.DeptID = Convert.ToInt32(sdr["DeptID"]);
            objAsset.Urut = Convert.ToInt32(sdr["Urut"]);
            return objAsset;
        }

        public AssetDomainSerah RetrieveDataPICDept(string KodeAsset)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string result = string.Empty; string query = string.Empty;
            if (users.UnitKerjaID.ToString().Length == 1)
            {
                query = "13";
            }
            else
            {
                query = "14";
            }
            string StrSql =
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp_A]') AND type in (N'U')) DROP TABLE [dbo].[temp_A] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp_B]') AND type in (N'U')) DROP TABLE [dbo].[temp_B] " +

            " select distinct 'A'Flag,  TRIM(A.Alias) DeptName into temp_A from (   " +
            " select x2.AssetUtama,x22.ItemName,x2.CreatedBy from (  select AssetUtama,CreatedBy " +
            " from (  select SUBSTRING(A1.ItemCode,8," + query + ")AssetUtama,x.CreatedBy from (select ItemID,GroupID,A.ItemTypeID,B.CreatedBy " +
            " from pakaidetail A inner join Pakai B ON A.PakaiID=B.ID where A.groupid=12 and A.rowstatus>-1 and A.ItemTypeID=2 and B.Status=3 ) as x " +
            " inner join Asset A1 ON A1.ID=x.ItemID  ) as x1 group by AssetUtama,CreatedBy ) as x2 " +
            " inner join asset x22 ON x22.ItemCode=x2.AssetUtama where x2.AssetUtama='" + KodeAsset + "'  " +
            " ) as x33 inner join users x01 ON x01.UserName=x33.CreatedBy inner join Dept A ON A.ID=x01.DeptID " +

            " select DISTINCT DeptName = STUFF((SELECT DISTINCT '; ' + DeptName  FROM temp_A AS x2  " +
            " WHERE x2.Flag = x3.Flag  FOR XML PATH('')), 1, 1, '') into temp_B from temp_A x3 group by x3.DeptName,Flag " +

            " select REPLACE(DeptName,'&amp;','&')NamaDept from temp_B " +

            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp_A]') AND type in (N'U')) DROP TABLE [dbo].[temp_A] " +
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp_B]') AND type in (N'U')) DROP TABLE [dbo].[temp_B] ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectDataPICDept(sqlDataReader);
                }
            }

            return new AssetDomainSerah();
        }

        private AssetDomainSerah GenerateObjectDataPICDept(SqlDataReader sdr)
        {
            AssetDomainSerah objAsset = new AssetDomainSerah();
            objAsset.NamaDept = sdr["NamaDept"].ToString();

            return objAsset;
        }

    }

}