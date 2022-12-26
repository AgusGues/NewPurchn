using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Domain;
using BusinessFacade;
using Cogs;
using System.Collections;
using System.Threading;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace GRCweb1.Modul.COGS
{
    public partial class SystemLock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.link = "../../Default.aspx";
            DariTgl.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            SampaiTgl.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            DariJam.Enabled = false;
            SampaiJam.Enabled = false;
            txtKeterangan.Text = KetClosing(0);
            Session.Remove("pilih");
            UsrList.Visible = false;
            //unLock.Enabled = false;
            unLock.Visible = false;
            LoadLock();
        }

        protected void DariTgl_TextChanged(object sender, EventArgs e)
        {
            SampaiTgl.Text = DariTgl.Text;
        }
        protected void ddlDurasi_SelectedIndexChanged(object sender, EventArgs e)
        {
            DariJam.Enabled = (ddlDurasi.SelectedIndex == 0) ? false : true;
            SampaiJam.Enabled = (ddlDurasi.SelectedIndex == 0) ? false : true;
            txtKeterangan.Text = string.Empty;
            txtKeterangan.Text = KetClosing(ddlDurasi.SelectedIndex);
        }
        protected void SampaiJam_TextChanged(object sender, EventArgs e)
        {
            if (SampaiJam.Text.Length == 5)
            {
                txtKeterangan.Text = KetClosing(1).ToString();
            }
        }
        private string KetClosing(int tipe)
        {
            string Txt = string.Empty;
            switch (tipe)
            {
                case 0:
                    Txt = "Mohon maaf system sedang di lock oleh Accounting Dept. untuk proses Closing Data\n" +
                         "Tanggal :" + DariTgl.Text.ToString() + " \nJam : " + DariJam.Text.ToString() +
                         "Satu Hari Full" +
                         "\n\n\nTerima Kasih";
                    break;
                case 1:
                    Txt = "Mohon maaf system sedang di lock oleh Accounting Dept. untuk proses Closing Data\n" +
                         "Tanggal :" + DariTgl.Text.ToString() + " \nJam : " + DariJam.Text.ToString() +
                         " s/d " + SampaiJam.Text.ToString() +
                         "\n\n\nTerima Kasih";
                    break;
            }
            return Txt;
        }
        protected void Dept_CheckedChanged(object sender, EventArgs e)
        {
            UsrList.Items.Clear();
            UsrList.Visible = true;
            ArrayList arrDept = new ArrayList();
            DeptFacade deptFacade = new DeptFacade();
            arrDept = deptFacade.Retrieve();
            foreach (Dept dpt in arrDept)
            {
                ListItem item = new ListItem();
                item.Text = dpt.DeptName;
                item.Value = dpt.ID.ToString();
                UsrList.Items.Add(item);
            }
        }


        protected void Usr_CheckedChanged(object sender, EventArgs e)
        {
            UsrList.Items.Clear();
            ArrayList arrUsr = new ArrayList();
            UsersFacade usr = new UsersFacade();
            arrUsr = usr.RetrieveAllUser();
            DeptFacade deptFacade = new DeptFacade();

            foreach (Users user in arrUsr)
            {
                Dept objDpt = new Dept();
                objDpt = deptFacade.RetrieveById(user.DeptID);
                ListItem item = new ListItem();
                item.Text = user.UserName + " [" + objDpt.DeptName + " ]";
                item.Value = user.ID.ToString();
                UsrList.Items.Add(item);
            }

        }
        protected void All_CheckedChanged(object sender, EventArgs e)
        {
            UsrList.Items.Clear();
            UsrList.Visible = false;
        }
        protected void UsrList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ckhitems = string.Empty;
            foreach (ListItem lst in UsrList.Items)
            {
                if (lst.Selected == true)
                {
                    ckhitems += lst.Value + ",";
                }
                else
                {
                    ckhitems.Replace(lst.Value + ",", "");
                }
            }
            listLock.Text = ckhitems;
        }
        protected void Lock_Click(object sender, EventArgs e)
        {
            LockSys objL = new LockSys();
            LockingFacade Lo = new LockingFacade();
            objL.DariTgl = DateTime.Parse(DariTgl.Text.ToString());
            objL.SampaiTgl = DateTime.Parse(SampaiTgl.Text.ToString());
            objL.DariJam = DariJam.Text.ToString();
            objL.SampaiJam = SampaiJam.Text.ToString();
            objL.Durasi = ddlDurasi.SelectedValue.ToString();
            objL.Keterangan = txtKeterangan.Text.ToString();
            objL.UserLock = (DeptSelect.Checked == true) ? ListChecked() : "All";
            objL.CreatedBy = ((Users)Session["Users"]).UserName;
            int result = 0;
            if (txtID.Text == string.Empty)
            {
                result = Lo.Insert(objL);
            }
            else
            {
                objL.RowStatus = 1;
                objL.ID = int.Parse(txtID.Text);
                result = Lo.Update(objL);
            }
            if (result > 0 && Lo.Error == string.Empty)
            {
                LoadLock();
                ClearItems();
            }

        }
        protected void unLock_Click(object sender, EventArgs e)
        {
            LockSys objL = new LockSys();
            LockingFacade Lo = new LockingFacade();
            objL.ID = int.Parse(txtID.Text);
            objL.DariTgl = DateTime.Parse(DariTgl.Text.ToString());
            objL.SampaiTgl = DateTime.Parse(SampaiTgl.Text.ToString());
            objL.DariJam = DariJam.Text.ToString();
            objL.SampaiJam = SampaiJam.Text.ToString();
            objL.Durasi = ddlDurasi.SelectedValue.ToString();
            objL.Keterangan = txtKeterangan.Text.ToString();
            objL.UserLock = (DeptSelect.Checked == true) ? ListChecked() : "All";
            objL.LastModifiedBy = ((Users)Session["Users"]).UserName;
            objL.RowStatus = 0;
            int result = Lo.Update(objL);
            if (result > 0 && Lo.Error == string.Empty)
            {
                LoadLock();
                ClearItems();
            }
        }
        private string ListChecked()
        {
            string ckhitems = string.Empty;
            foreach (ListItem lst in UsrList.Items)
            {
                if (lst.Selected == true)
                    ckhitems += lst.Value + ",";
            }
            return ckhitems;
        }
        private string DeptLocked()
        {
            string ckhitems = string.Empty;
            foreach (ListItem lst in UsrList.Items)
            {
                if (lst.Selected == true)
                    ckhitems += lst.Text + "\n\r";
            }
            return ckhitems;
        }
        public void LoadLock()
        {
            ArrayList arrLock = new ArrayList();
            ArrayList arrLc = new ArrayList();
            LockingFacade sysLock = new LockingFacade();
            arrLock = sysLock.Retrieve();
            if (arrLock.Count > 0)
            {
                string dpt = string.Empty;
                string DeptName = string.Empty;
                string[] usrlock = new string[] { };
                foreach (LockSys sysLok in arrLock)
                {
                    #region Departemen Name
                    if (sysLok.UserLock != "All")
                    {
                        usrlock = Regex.Split(sysLok.UserLock.ToString(), ",");
                        for (int i = 0; i < (usrlock.Count() - 1); i++)
                        {
                            DeptName += (i + 1).ToString() + ". " + sysLock.GetDeptName(int.Parse(usrlock[i])) + "<br> ";
                        }
                    }
                    #endregion
                    dpt = (sysLok.UserLock.ToString() == "All") ? "All User" : DeptName;
                    arrLc.Add(new LockSys
                    {
                        ID = sysLok.ID,
                        DariTgl = sysLok.DariTgl,
                        SampaiTgl = sysLok.SampaiTgl,
                        DariJam = sysLok.DariJam,
                        SampaiJam = sysLok.SampaiJam,
                        Durasi = sysLok.Durasi,
                        Keterangan = sysLok.Keterangan,
                        StatusE = sysLok.StatusE,
                        UserLock = dpt
                    });
                }
                lstLock.DataSource = arrLc;
                lstLock.DataBind();
            }
        }
        public void ClearItems()
        {
            Locked.Enabled = false;
        }
        protected void lstLock_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            LockingFacade lfacade = new LockingFacade();
            if (e.CommandName == "Edit")
            {
                LockSys objL = lfacade.RetrieveByID(int.Parse(e.CommandArgument.ToString()));
                DariTgl.Text = objL.DariTgl.ToString("dd-MMM-yyyy");
                SampaiTgl.Text = objL.SampaiTgl.ToString("dd-MMM-yyy");
                DariJam.Text = objL.DariJam;
                SampaiJam.Text = objL.SampaiJam;
                ddlDurasi.SelectedValue = objL.Durasi.ToString();
                txtKeterangan.Text = objL.Keterangan;
                txtID.Text = objL.ID.ToString();
                Allusr.Checked = (objL.UserLock.ToString() == "All") ? true : false;
                DeptSelect.Checked = (objL.UserLock.ToString() == "All") ? false : true;
                #region Checkbox proses
                string dpt = string.Empty;
                string DeptName = string.Empty;
                string[] usrlock = new string[] { };
                if (objL.UserLock != "All")
                {
                    ListDept();
                    UsrList.Visible = true;
                    usrlock = Regex.Split(objL.UserLock.ToString(), ",");
                    for (int c = 0; c < (UsrList.Items.Count); c++)
                    {
                        for (int i = 0; i < (usrlock.Count() - 1); i++)
                        {
                            if (UsrList.Items[c].Value == usrlock[i].ToString())
                            {
                                UsrList.Items[c].Selected = true;
                            }
                        }
                    }
                }
                else
                {
                    UsrList.Items.Clear();
                    UsrList.Visible = false;
                }
                #endregion
                unLock.Enabled = true;
                Locked.Enabled = true;
            }

        }
        private void ListDept()
        {
            UsrList.Items.Clear();
            ArrayList arrDept = new ArrayList();
            DeptFacade deptFacade = new DeptFacade();
            arrDept = deptFacade.Retrieve();
            foreach (Dept dpt in arrDept)
            {
                ListItem item = new ListItem();
                item.Text = dpt.DeptName;
                item.Value = dpt.ID.ToString();
                UsrList.Items.Add(item);
            }
        }
    }
}