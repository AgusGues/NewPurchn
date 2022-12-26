using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using BusinessFacade;
using Domain;
using DataAccessLayer;

namespace GRCweb1.Modul.ISO
{
    public partial class FormInputDisiplin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadTahun();
                LoadBulan();
                LoadDept();
                ZetroAuth za = new ZetroAuth();
                bool auth = za.UserAuth(TypeAuth.INPUT, ((Users)Session["Users"]).ID, "Input Disiplin");
                btnSimpan.Visible = auth;
                btnExport.Visible = false;
                //aktif saat data sudah terload
                //new ZetroAuth().UserAuth(TypeAuth.PRINT, ((Users)Session["Users"]).ID, "Input Disiplin");
            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }

        private void LoadTahun()
        {
            Disiplin p = new Disiplin();
            p.GetTahun(ddlTahun);
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();

        }
        private void LoadBulan()
        {
            ddlBulan.Items.Clear();
            ddlBulan.Items.Add(new ListItem("--All--", "0"));
            for (int i = 1; i <= 12; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i).ToString(), i.ToString()));
            }
            ddlBulan.SelectedValue = (DateTime.Now.Month == 1) ? "12" : (DateTime.Now.Month - 1).ToString();
            // ddlTahun.SelectedValue = (ddlBulan.SelectedValue == "1") ? (int.Parse(ddlTahun.SelectedValue) - 1).ToString() : ddlTahun.SelectedValue;
        }
        private void LoadDept()
        {
            DeptFacade deptFacade = new DeptFacade();
            Users user = (Users)Session["Users"];
            deptFacade.Criteria = " and ID not in(1,4,5,18) order by Alias";
            ArrayList arrDept = new ArrayList();
            switch (user.UnitKerjaID)
            {
                case 1:
                case 7:
                    deptFacade.Criteria = " and ID not in(1,4,5,18) order by Alias";

                    break;
                default:
                    deptFacade.Criteria = " order by Alias";
                    //arrDept = this.LoadDept(string.Empty);
                    break;
            }
            arrDept = deptFacade.RetrieveAliasDept();
            Auth DeptAuth = new Auth();
            UserAuth oto = new UserAuth();
            oto.Option = "All";
            oto.Criteria = " and UserID=" + ((Users)Session["Users"]).ID.ToString();
            oto.Criteria += " and ModulName='Input Disiplin' ";
            DeptAuth = oto.Retrieve(true);
            string UserDept = ((Users)Session["Users"]).DeptID.ToString() + ",";
            string[] arrDepts = (DeptAuth.AuthDept != null) ? DeptAuth.AuthDept.Split(',') : UserDept.ToString().Split(',');
            ddlDept.Items.Clear();
            if (deptFacade.Error == string.Empty)
            {
                ddlDept.Items.Add(new ListItem("-- All Dept --", "0"));

                foreach (Dept dept in arrDept)
                {
                    if (arrDepts.Contains(dept.ID.ToString()))
                    {
                        ddlDept.Items.Add(new ListItem(dept.AlisName, dept.ID.ToString()));
                    }
                }
            }

            ddlDept.SelectedIndex = 0;// ((Users)Session["Users"]).DeptID.ToString();
        }
        public ArrayList LoadDept(string Criteria)
        {
            ArrayList arrData = new ArrayList();
            //bpas_api.WebService1 api = new bpas_api.WebService1();
            Global2 api = new Global2();
            DataSet da = new DataSet();
            da = api.GetDataTable("Dept", "*", "Where RowStatus>-1 " + Criteria + " Order By DeptName", "GRCBoardPurch");
            foreach (DataRow d in da.Tables[0].Rows)
            {
                arrData.Add(new Dept
                {
                    ID = Convert.ToInt32(d["ID"].ToString()),
                    AlisName = d["DeptName"].ToString()
                });
            }
            return arrData;
        }
        public ArrayList LoadDept(string Criteria, bool list)
        {
            ArrayList arrData = new ArrayList();
            //bpas_api.WebService1 api = new bpas_api.WebService1();
            Global2 api = new Global2();
            DataSet da = new DataSet();
            da = api.GetDataTable("Dept", "*", "Where RowStatus>-1 " + Criteria + " Order By DeptName", "GRCBoardPurch");
            foreach (DataRow d in da.Tables[0].Rows)
            {
                arrData.Add(new DispObj
                {
                    ID = Convert.ToInt32(d["ID"].ToString()),
                    AlisName = d["DeptName"].ToString(),
                    DeptName = d["DeptName"].ToString()
                });
            }
            return arrData;
        }
        private Auth DeptAuths()
        {
            Auth DeptAuth = new Auth();
            UserAuth oto = new UserAuth();
            oto.Option = "All";
            oto.Criteria = " and UserID=" + ((Users)Session["Users"]).ID.ToString();
            oto.Criteria += " and ModulName='Input Disiplin' ";
            DeptAuth = oto.Retrieve(true);
            return DeptAuth;
        }
        protected void btnExport_OnClick(object sender, EventArgs e)
        {
            //LoadData4Export();
            Export2Excel();
        }
        private void LoadData()
        {
            ArrayList arrData = new ArrayList();
            Disiplin dsp = new Disiplin();
            dsp.Criteria = (ddlDept.SelectedIndex == 0) ? " and ID in(" + ((Auth)DeptAuths()).AuthDept.ToString() + ")" : " and ID=" + ddlDept.SelectedValue.ToString();
            Users user = (Users)Session["Users"];
            //dsp.Criteria += " and ID not in(1,4,5,18)";
            dsp.Pilihan = "List";
            switch (user.UnitKerjaID)
            {
                case 1:
                case 7:
                    dsp.Criteria += " and ID not in(1,4,5,18)";
                    break;
                default:
                    dsp.Criteria += "";
                    break;
            }
            arrData = dsp.RetrieveDept();
            lstDept.DataSource = arrData;
            lstDept.DataBind();
        }
        private void LoadData4Export()
        {
            Users user = (Users)Session["Users"];
            ArrayList arrData = new ArrayList();
            Disiplin dsp = new Disiplin();
            dsp.Criteria = (ddlDept.SelectedIndex == 0) ? " and ID in(" + ((Auth)DeptAuths()).AuthDept.ToString() + ")" : " and ID=" + ddlDept.SelectedValue.ToString();
            //dsp.Criteria += " and ID not in(1,4,5,18,8)";
            dsp.Pilihan = "List";
            switch (user.UnitKerjaID)
            {
                case 1:
                case 7:
                    dsp.Criteria += " and ID not in(1,4,5,18)";
                    break;
                default:
                    dsp.Criteria += "";
                    break;
            }

            arrData = dsp.RetrieveDept();
            lstDeptC.DataSource = arrData;
            lstDeptC.DataBind();
        }
        protected void lstDeptC_DataBound(object sender, RepeaterItemEventArgs e)
        {
            DispObj obj = (DispObj)e.Item.DataItem;
            Repeater lst = (Repeater)e.Item.FindControl("lstPrs");
            Disiplin dsp = new Disiplin();
            dsp.Criteria = " and DeptID=" + obj.DeptID;
            dsp.Criteria += " and Bulan=" + ddlBulan.SelectedValue.ToString();
            dsp.Criteria += " and Tahun=" + ddlTahun.SelectedValue.ToString();
            dsp.Pilihan = "Export";
            lst.DataSource = dsp.Retrieve();
            lst.DataBind();
        }
        private void Export2Excel()
        {
            LoadData4Export();
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=RekapDataDisiplin.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<b>REKAP DATA DISIPLIN</b>";
            Html += "<br>Periode : " + ddlBulan.SelectedItem.Text + "  " + ddlTahun.SelectedValue.ToString();
            Html += "<br>Departement :" + ddlDept.SelectedItem.Text;
            string HtmlEnd = "";
            lste.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0", "border=\"1");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }
        protected void btnBulan_Change(object sender, EventArgs e)
        {
            ddlDept_Change(null, null);
        }
        protected void btnTahun_Change(object sender, EventArgs e)
        {
            ddlDept_Change(null, null);
        }
        protected void ddlDept_Change(object sender, EventArgs e)
        {
            LoadData();
        }
        protected void lstDept_DataBound(object sender, RepeaterItemEventArgs e)
        {
            DispObj obj = (DispObj)e.Item.DataItem;
            Repeater lst = (Repeater)e.Item.FindControl("lstPerson");
            Disiplin dsp = new Disiplin();
            dsp.Criteria = " and ua.DeptID=" + obj.DeptID;
            dsp.Where = " and Bulan=" + ddlBulan.SelectedValue;
            dsp.Where += " and Tahun=" + ddlTahun.SelectedValue;
            dsp.Pilihan = "Detail";
            lst.DataSource = dsp.Retrieve();
            lst.DataBind();
        }
        private decimal NilaiDisiplin(string UserID, string BagianID, string DisType, TextBox txt)
        {
            decimal rst = 0;
            Disiplin dsp = new Disiplin();
            dsp.Pilihan = "Data";
            dsp.Criteria = " and UserID=" + UserID;
            //dsp.Criteria += " and BagianID=" + BagianID;
            dsp.Criteria += " and Tahun=" + ddlTahun.SelectedValue;
            dsp.Criteria += " and Bulan=" + ddlBulan.SelectedValue;
            dsp.Criteria += " and DisiplinType='" + DisType + "'";
            DispObj dis = dsp.Retrieve(true);
            rst = dis.Nilai;
            txt.ReadOnly = (dis.ID > 0) ? true : false;
            return rst;
        }
        private decimal NilaiDisiplin(string UserID, string BagianID, string DisType, bool score)
        {
            decimal rst = 100;
            Disiplin dsp = new Disiplin();
            dsp.Pilihan = "Data";
            dsp.Criteria = " and UserID=" + UserID;
            //dsp.Criteria += " and BagianID=" + BagianID;
            dsp.Criteria += " and Tahun=" + ddlTahun.SelectedValue;
            dsp.Criteria += " and Bulan=" + ddlBulan.SelectedValue;
            dsp.Criteria += " and DisiplinType='" + DisType + "'";
            DispObj dis = dsp.Retrieve(true);
            rst = (dis.Score == 0) ? 100 : dis.Score;
            return rst;
        }
        protected void lstPerson_DataBound(object sender, RepeaterItemEventArgs e)
        {
            DispObj ds = (DispObj)e.Item.DataItem;
            #region Control variable
            string[] arrDept = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("InputDisiplin", "PES").Split(',');
            TextBox nSakit = (TextBox)e.Item.FindControl("txtNilSakit");
            TextBox nIjin = (TextBox)e.Item.FindControl("txtNilIjin");
            TextBox nAlpha = (TextBox)e.Item.FindControl("txtNilAlpha");
            TextBox nLambat = (TextBox)e.Item.FindControl("txtNilLate");
            TextBox nTP = (TextBox)e.Item.FindControl("txtNilTP");
            TextBox nSP = (TextBox)e.Item.FindControl("txtNilSP");
            TextBox nTotal = (TextBox)e.Item.FindControl("txtNilTotal");
            TextBox nSCTotal = (TextBox)e.Item.FindControl("txtSCTotal");
            TextBox dSakit = (TextBox)e.Item.FindControl("ddlSCSakit");
            TextBox dIjin = (TextBox)e.Item.FindControl("ddlSCIjin");
            TextBox dAlpha = (TextBox)e.Item.FindControl("ddlSCAlpha");
            TextBox dLambat = (TextBox)e.Item.FindControl("ddlSCLate");
            TextBox dTP = (TextBox)e.Item.FindControl("ddlSCTP");
            TextBox dSP = (TextBox)e.Item.FindControl("ddlSCSP");
            TextBox Uide = (TextBox)e.Item.FindControl("txtUserID");
            TextBox bagID = (TextBox)e.Item.FindControl("txtBagianID");
            Image edit = (Image)e.Item.FindControl("edit");
            Image save = (Image)e.Item.FindControl("simpan");
            CheckBox chk = (CheckBox)e.Item.FindControl("chkN");
            //chk.Visible = (edit.Visible == true) ? false : true;
            #endregion
            #region Update Attributs
            nSakit.AutoPostBack = true; nIjin.AutoPostBack = true;
            nAlpha.AutoPostBack = true; nLambat.AutoPostBack = true;
            nTP.AutoPostBack = true; nSP.AutoPostBack = true;
            nSakit.ToolTip += "-" + ds.UserID.ToString() + "-" + ds.BagianID.ToString();
            nIjin.ToolTip += "-" + ds.UserID.ToString() + "-" + ds.BagianID.ToString();
            nAlpha.ToolTip += "-" + ds.UserID.ToString() + "-" + ds.BagianID.ToString();
            nLambat.ToolTip += "-" + ds.UserID.ToString() + "-" + ds.BagianID.ToString();
            nTP.ToolTip += "-" + ds.UserID.ToString() + "-" + ds.BagianID.ToString();
            nSP.ToolTip += "-" + ds.UserID.ToString() + "-" + ds.BagianID.ToString();
            nSakit.Enabled = (arrDept.Contains(((Users)Session["Users"]).DeptID.ToString())) ? true : false;
            nIjin.Enabled = (arrDept.Contains(((Users)Session["Users"]).DeptID.ToString())) ? true : false;
            nAlpha.Enabled = (arrDept.Contains(((Users)Session["Users"]).DeptID.ToString())) ? true : false;
            nLambat.Enabled = (arrDept.Contains(((Users)Session["Users"]).DeptID.ToString())) ? true : false;
            nTP.Enabled = (arrDept.Contains(((Users)Session["Users"]).DeptID.ToString())) ? true : false;
            nSP.Enabled = (arrDept.Contains(((Users)Session["Users"]).DeptID.ToString())) ? true : false;
            dSakit.Enabled = (arrDept.Contains(((Users)Session["Users"]).DeptID.ToString())) ? true : false;
            dIjin.Enabled = (arrDept.Contains(((Users)Session["Users"]).DeptID.ToString())) ? true : false;
            dAlpha.Enabled = (arrDept.Contains(((Users)Session["Users"]).DeptID.ToString())) ? true : false;
            dLambat.Enabled = (arrDept.Contains(((Users)Session["Users"]).DeptID.ToString())) ? true : false;
            dSP.Enabled = (arrDept.Contains(((Users)Session["Users"]).DeptID.ToString())) ? true : false;
            dTP.Enabled = (arrDept.Contains(((Users)Session["Users"]).DeptID.ToString())) ? true : false;
            #endregion
            #region FillData

            nSakit.Text = NilaiDisiplin(ds.UserID.ToString(), ds.BagianID.ToString(), "Sakit", nSakit).ToString();
            dSakit.Text = NilaiDisiplin(ds.UserID.ToString(), ds.BagianID.ToString(), "Sakit", true).ToString("##0");
            nIjin.Text = NilaiDisiplin(ds.UserID.ToString(), ds.BagianID.ToString(), "Ijin", nIjin).ToString();
            dIjin.Text = NilaiDisiplin(ds.UserID.ToString(), ds.BagianID.ToString(), "Ijin", true).ToString("##0");
            nAlpha.Text = NilaiDisiplin(ds.UserID.ToString(), ds.BagianID.ToString(), "Alpa", nIjin).ToString();
            dAlpha.Text = NilaiDisiplin(ds.UserID.ToString(), ds.BagianID.ToString(), "Alpa", true).ToString("##0");
            nLambat.Text = NilaiDisiplin(ds.UserID.ToString(), ds.BagianID.ToString(), "Terlambat", nLambat).ToString();
            dLambat.Text = NilaiDisiplin(ds.UserID.ToString(), ds.BagianID.ToString(), "Terlambat", true).ToString("##0");
            nSP.Text = NilaiDisiplin(ds.UserID.ToString(), ds.BagianID.ToString(), "SP", nSP).ToString();
            dSP.Text = NilaiDisiplin(ds.UserID.ToString(), ds.BagianID.ToString(), "SP", true).ToString("##0");
            nTP.Text = NilaiDisiplin(ds.UserID.ToString(), ds.BagianID.ToString(), "ST", nTP).ToString();
            dTP.Text = NilaiDisiplin(ds.UserID.ToString(), ds.BagianID.ToString(), "ST", true).ToString("##0");
            edit.Visible = (HasBeenInput(ds.UserID).Count > 0 && arrDept.Contains(((Users)Session["Users"]).DeptID.ToString())) ? true : false;
            chk.Visible = (HasBeenInput(ds.UserID).Count > 0 && new ZetroAuth().UserAuth(TypeAuth.UPDATE, ((Users)Session["Users"]).ID, "Input Disiplin") == false) ? false : true;
            //chk.Visible = new ZetroAuth().UserAuth(TypeAuth.UPDATE, ((Users)Session["Users"]).ID, "Input Disiplin");
            edit.Visible = new ZetroAuth().UserAuth(TypeAuth.UPDATE, ((Users)Session["Users"]).ID, "Input Disiplin");
            /* * */
            bool auth = new ZetroAuth().UserAuth(TypeAuth.INPUT, ((Users)Session["Users"]).ID, "Input Disiplin");
            btnSimpan.Visible = (HasBeenInput(ds.UserID).Count > 0 || auth == false) ? false : true;

            btnExport.Visible = new ZetroAuth().UserAuth(TypeAuth.PRINT, ((Users)Session["Users"]).ID, "Input Disiplin");
            btnExport.Visible = (HasBeenInput(ds.UserID).Count > 0) ? btnExport.Visible : false;
            #endregion
            #region HighLiht if not zero
            nSakit.BackColor = (nSakit.Text != "0") ? System.Drawing.Color.LightSkyBlue : System.Drawing.Color.Transparent;
            nAlpha.BackColor = (nAlpha.Text != "0") ? System.Drawing.Color.LightSkyBlue : System.Drawing.Color.Transparent;
            nIjin.BackColor = (nIjin.Text != "0") ? System.Drawing.Color.LightSkyBlue : System.Drawing.Color.Transparent;
            nLambat.BackColor = (nLambat.Text != "0") ? System.Drawing.Color.LightSkyBlue : System.Drawing.Color.Transparent;
            nSP.BackColor = (nSP.Text != "0") ? System.Drawing.Color.LightSkyBlue : System.Drawing.Color.Transparent;
            nTP.BackColor = (nTP.Text != "0") ? System.Drawing.Color.LightSkyBlue : System.Drawing.Color.Transparent;
            #endregion
            #region TotalNilai
            nTotal.Text = (NilaiDisiplin(ds.UserID.ToString(), ds.BagianID.ToString(), "Sakit", true) +
                         NilaiDisiplin(ds.UserID.ToString(), ds.BagianID.ToString(), "Ijin", true) +
                         NilaiDisiplin(ds.UserID.ToString(), ds.BagianID.ToString(), "Alpa", true) +
                         NilaiDisiplin(ds.UserID.ToString(), ds.BagianID.ToString(), "Terlambat", true) +
                         NilaiDisiplin(ds.UserID.ToString(), ds.BagianID.ToString(), "SP", true) +
                         NilaiDisiplin(ds.UserID.ToString(), ds.BagianID.ToString(), "ST", true)).ToString("##0");
            nSCTotal.Text = ((NilaiDisiplin(ds.UserID.ToString(), ds.BagianID.ToString(), "Sakit", true) +
                         NilaiDisiplin(ds.UserID.ToString(), ds.BagianID.ToString(), "Ijin", true) +
                         NilaiDisiplin(ds.UserID.ToString(), ds.BagianID.ToString(), "Alpa", true) +
                         NilaiDisiplin(ds.UserID.ToString(), ds.BagianID.ToString(), "Terlambat", true) +
                         NilaiDisiplin(ds.UserID.ToString(), ds.BagianID.ToString(), "SP", true) +
                         NilaiDisiplin(ds.UserID.ToString(), ds.BagianID.ToString(), "ST", true)) / 6).ToString("##0.#0");
            #endregion
            #region Lock Control
            //if (ddlDept.SelectedIndex == 0)
            //{
            foreach (Control ctl in e.Item.Controls)
            {
                if (ctl is TextBox)
                {
                    TextBox tx = (TextBox)ctl;
                    tx.ReadOnly = true;
                }

            }
            // }
            #endregion
        }
        protected void chkn_CheckedChange(object sender, EventArgs e)
        {
            for (int i = 0; i < lstDept.Items.Count; i++)
            {
                Repeater lstPerson = (Repeater)lstDept.Items[i].FindControl("lstPerson");
                for (int n = 0; n < lstPerson.Items.Count; n++)
                {
                    CheckBox chk = (CheckBox)lstPerson.Items[n].FindControl("chkN");

                    foreach (Control ctl in lstPerson.Items[n].Controls)
                    {
                        if (ctl is TextBox)
                        {
                            TextBox tx = (TextBox)ctl;
                            tx.ReadOnly = (chk.Checked == true) ? false : true;
                        }
                    }
                }
            }


        }
        protected void lstPerson_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            int iDx = int.Parse(e.CommandArgument.ToString());
            ArrayList arrData = new ArrayList();
            ArrayList arrTotal = new ArrayList();
            switch (e.CommandName)
            {
                case "Edit":
                    for (int i = 0; i < lstDept.Items.Count; i++)
                    {
                        Repeater person = (Repeater)lstDept.Items[i].FindControl("lstPerson");
                        Image save = (Image)person.Items[iDx].FindControl("simpan");
                        Image edit = (Image)person.Items[iDx].FindControl("edit");
                        save.Visible = true;
                        edit.Visible = false;
                        foreach (Control ct in person.Items[iDx].Controls)
                        {
                            if (ct is TextBox)
                            {
                                TextBox tx = (TextBox)ct;
                                tx.ReadOnly = false;
                            }
                        }
                    }
                    break;
                case "Save":
                    for (int i = 0; i < lstDept.Items.Count; i++)
                    {
                        Repeater person = (Repeater)lstDept.Items[i].FindControl("lstPerson");
                        Image save = (Image)person.Items[iDx].FindControl("simpan");
                        Image edit = (Image)person.Items[iDx].FindControl("edit");
                        string txtID = "";
                        foreach (Control ct in person.Items[iDx].Controls)
                        {
                            if (ct is TextBox)
                            {
                                TextBox tx = (TextBox)ct;
                                txtID += tx.ID.ToString() + ",";
                                tx.ReadOnly = true;
                            }
                        }
                        string BagianID = ((TextBox)person.Items[iDx].FindControl("txtBagianID")).Text;
                        DispObj Sakit = new DispObj();
                        Sakit.ID = GetIDData(int.Parse(save.ToolTip.ToString()), "Sakit", BagianID);
                        Sakit.Nilai = int.Parse(((TextBox)person.Items[iDx].FindControl("txtNilSakit")).Text);
                        Sakit.Score = decimal.Parse(((TextBox)person.Items[iDx].FindControl("ddlSCSakit")).Text);
                        Sakit.LastModifiedBy = ((Users)Session["Users"]).UserID.ToString();
                        Sakit.LastModifiedTime = DateTime.Now.ToLocalTime();
                        arrData.Add(Sakit);

                        DispObj Alpa = new DispObj();
                        Alpa.ID = GetIDData(int.Parse(save.ToolTip.ToString()), "Alpa", BagianID);
                        Alpa.Nilai = int.Parse(((TextBox)person.Items[iDx].FindControl("txtNilAlpha")).Text);
                        Alpa.Score = decimal.Parse(((TextBox)person.Items[iDx].FindControl("ddlSCAlpha")).Text);
                        Alpa.LastModifiedBy = ((Users)Session["Users"]).UserID.ToString();
                        Alpa.LastModifiedTime = DateTime.Now.ToLocalTime();
                        arrData.Add(Alpa);

                        DispObj Ijin = new DispObj();
                        Ijin.ID = GetIDData(int.Parse(save.ToolTip.ToString()), "Ijin", BagianID);
                        Ijin.Nilai = int.Parse(((TextBox)person.Items[iDx].FindControl("txtNilIjin")).Text);
                        Ijin.Score = decimal.Parse(((TextBox)person.Items[iDx].FindControl("ddlSCIjin")).Text);
                        Ijin.LastModifiedBy = ((Users)Session["Users"]).UserID.ToString();
                        Ijin.LastModifiedTime = DateTime.Now.ToLocalTime();
                        arrData.Add(Ijin);

                        DispObj lambat = new DispObj();
                        lambat.ID = GetIDData(int.Parse(save.ToolTip.ToString()), "Terlambat", BagianID);
                        lambat.Nilai = int.Parse(((TextBox)person.Items[iDx].FindControl("txtNilLate")).Text);
                        lambat.Score = decimal.Parse(((TextBox)person.Items[iDx].FindControl("ddlSCLate")).Text);
                        arrData.Add(lambat);

                        DispObj SP = new DispObj();
                        SP.ID = GetIDData(int.Parse(save.ToolTip.ToString()), "SP", BagianID);
                        SP.Nilai = int.Parse(((TextBox)person.Items[iDx].FindControl("txtNilSP")).Text);
                        SP.Score = decimal.Parse(((TextBox)person.Items[iDx].FindControl("ddlSCSP")).Text);
                        SP.LastModifiedBy = ((Users)Session["Users"]).UserID.ToString();
                        SP.LastModifiedTime = DateTime.Now.ToLocalTime();
                        arrData.Add(SP);

                        DispObj ST = new DispObj();
                        ST.ID = GetIDData(int.Parse(save.ToolTip.ToString()), "ST", BagianID);
                        ST.Nilai = int.Parse(((TextBox)person.Items[iDx].FindControl("txtNilTP")).Text);
                        ST.Score = decimal.Parse(((TextBox)person.Items[iDx].FindControl("ddlSCTP")).Text);
                        ST.LastModifiedBy = ((Users)Session["Users"]).UserID.ToString();
                        ST.LastModifiedTime = DateTime.Now.ToLocalTime();
                        arrData.Add(ST);

                        //total
                        DispObj ttl = new DispObj();
                        ttl.ID = GetIDData(int.Parse(save.ToolTip.ToString()), BagianID);
                        ttl.PointNilai = decimal.Parse(((TextBox)person.Items[iDx].FindControl("txtSCTotal")).Text);
                        arrTotal.Add(ttl);
                        /**
                         * proses data ke table
                         */
                        ZetroLib slb = new ZetroLib();
                        slb.hlp = new DispObj();
                        slb.Option = "Update";
                        slb.Criteria = "ID,Nilai,Score,LastmodifiedBy,LastMopdifiedTime";
                        slb.TableName = "ISO_DisiplinData";
                        slb.StoreProcedurName = "spISO_DisiplinData_Update";
                        string rst = slb.CreateProcedure();
                        if (rst == string.Empty)
                        {
                            foreach (DispObj dsp in arrData)
                            {
                                if (dsp.ID > 0)
                                {
                                    DispObj d = new DispObj();
                                    d.ID = dsp.ID;
                                    d.Nilai = dsp.Nilai;
                                    d.Score = dsp.Score;
                                    d.LastModifiedBy = dsp.LastModifiedBy;
                                    d.LastModifiedTime = dsp.LastModifiedTime;
                                    slb.hlp = d;
                                    int rest = slb.ProcessData();
                                }
                                else
                                {
                                    SimpanData();
                                }
                            }
                        }
                        ZetroLib slc = new ZetroLib();
                        slc.hlp = new DispObj();
                        slc.Option = "Update";
                        slc.Criteria = "ID,PointNilai";
                        slc.TableName = "ISO_DisiplinScore";
                        slc.StoreProcedurName = "spISO_DisiplinScore_Update";
                        string res = slc.CreateProcedure();
                        if (res == string.Empty)
                        {
                            foreach (DispObj dis in arrTotal)
                            {
                                DispObj ds = new DispObj();
                                ds.ID = dis.ID;
                                ds.PointNilai = dis.PointNilai;
                                slc.hlp = ds;
                                int var = slc.ProcessData();
                                if (var > 0)
                                {
                                    save.Visible = false;
                                    edit.Visible = true;
                                    ddlDept_Change(null, null);
                                }
                            }
                        }
                    }
                    break;
            }
        }
        protected void btnSimpan_OnClick(object sender, EventArgs e)
        {
            SimpanData();
        }
        protected void txtNilSakit_Change(object sender, EventArgs e)
        {
            this.Scoring(sender, "ddlSCSakit", "Sakit");
        }
        protected void txtNilIjin_Change(object sender, EventArgs e)
        {
            this.Scoring(sender, "ddlSCIjin", "Ijin");
        }
        protected void txtNilAlpha_Change(object sender, EventArgs e)
        {
            this.Scoring(sender, "ddlSCAlpha", "Alpha");
        }
        protected void txtNilLate_Change(object sender, EventArgs e)
        {
            this.Scoring(sender, "ddlSCLate", "Lambat");
        }
        protected void txtNilTP_Change(object sender, EventArgs e)
        {
            this.Scoring(sender, "ddlSCTP", "ST");
        }
        protected void txtNilSP_Change(object sender, EventArgs e)
        {
            this.Scoring(sender, "ddlSCSP", "SP");
        }
        private void Scoring(object sender, string Field, string Tipe)
        {
            string[] Score = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Score", "PES").Split(',');
            string[] pSakit = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read(Tipe, "PES").Split(',');
            TextBox idx = (TextBox)sender;
            string[] Key = idx.ToolTip.Split('-');
            int userID = int.Parse(Key[0]);
            for (int i = 0; i < lstDept.Items.Count; i++)
            {
                Repeater lst = (Repeater)lstDept.Items[i].FindControl("lstPerson");
                TextBox dSakit = (TextBox)lst.Items[userID].FindControl(Field);
                TextBox dTotal = (TextBox)lst.Items[userID].FindControl("txtNilTotal");
                TextBox sTotal = (TextBox)lst.Items[userID].FindControl("txtSCTotal");
                for (int n = 0; n < pSakit.Count(); n++)
                {
                    string[] nil = pSakit[n].Split('-');
                    if (nil.Contains(idx.Text))
                    {
                        dSakit.Text = Score[n].ToString();
                        break;
                    }
                    else
                    {
                        dSakit.Text = "20";
                    }
                }
                int nilai = 0;
                foreach (Control ct in lst.Items[userID].Controls)
                {
                    if (ct is TextBox)
                    {
                        switch (ct.ID)
                        {
                            case "ddlSCSakit":
                            case "ddlSCIjin":
                            case "ddlSCAlpha":
                            case "ddlSCLate":
                            case "ddlSCTP":
                            case "ddlSCSP":
                                nilai += int.Parse(((TextBox)ct).Text);
                                break;
                        }
                    }
                }
                int total = nilai;
                dTotal.Text = nilai.ToString();// (int.Parse(dSakit.Text) < 100) ? (int.Parse(dTotal.Text) - (100 - int.Parse(dSakit.Text))).ToString() : dTotal.Text;
                sTotal.Text = (decimal.Parse(dTotal.Text) / 6).ToString("###,##0.00");
            }
        }
        private void SimpanData()
        {
            ArrayList arrData = new ArrayList();
            ArrayList arrScore = new ArrayList();
            for (int i = 0; i < lstDept.Items.Count; i++)
            {
                Repeater e = (Repeater)lstDept.Items[i].FindControl("lstPerson");
                for (int n = 0; n < e.Items.Count; n++)
                {
                    #region variable data
                    TextBox Uid = (TextBox)e.Items[n].FindControl("txtUserID");
                    TextBox NIK = (TextBox)e.Items[n].FindControl("txtNIK");
                    TextBox Nama = (TextBox)e.Items[n].FindControl("txtNama");
                    TextBox nSakit = (TextBox)e.Items[n].FindControl("txtNilSakit");
                    TextBox nIjin = (TextBox)e.Items[n].FindControl("txtNilIjin");
                    TextBox nAlpha = (TextBox)e.Items[n].FindControl("txtNilAlpha");
                    TextBox nLambat = (TextBox)e.Items[n].FindControl("txtNilLate");
                    TextBox nTP = (TextBox)e.Items[n].FindControl("txtNilTP");
                    TextBox nSP = (TextBox)e.Items[n].FindControl("txtNilSP");
                    TextBox nTotal = (TextBox)e.Items[n].FindControl("txtNilTotal");
                    TextBox nSCTotal = (TextBox)e.Items[n].FindControl("txtSCTotal");
                    TextBox dSakit = (TextBox)e.Items[n].FindControl("ddlSCSakit");
                    TextBox dIjin = (TextBox)e.Items[n].FindControl("ddlSCIjin");
                    TextBox dAlpha = (TextBox)e.Items[n].FindControl("ddlSCAlpha");
                    TextBox dLambat = (TextBox)e.Items[n].FindControl("ddlSCLate");
                    TextBox dTP = (TextBox)e.Items[n].FindControl("ddlSCTP");
                    TextBox dSP = (TextBox)e.Items[n].FindControl("ddlSCSP");
                    TextBox BagID = (TextBox)e.Items[n].FindControl("txtBagianID");
                    CheckBox chk = (CheckBox)e.Items[n].FindControl("chkN");
                    #endregion
                    if (chk.Checked == true)
                    {
                        #region Data Sakit
                        DispObj sakit = new DispObj();
                        sakit.NIK = NIK.Text;
                        sakit.Nama = Nama.Text;
                        sakit.DisiplinType = "Sakit";
                        sakit.DeptID = int.Parse(ddlDept.SelectedValue.ToString());
                        sakit.UserID = int.Parse(Uid.Text);//int.Parse(nSakit.ToolTip.Split('-')[1].ToString());
                        sakit.BagianID = int.Parse(nSakit.ToolTip.Split('-')[2].ToString());
                        sakit.Bobot = 10;
                        sakit.Nilai = int.Parse(nSakit.Text);
                        sakit.Score = decimal.Parse(dSakit.Text);
                        sakit.RowStatus = 0;
                        sakit.Bulan = int.Parse(ddlBulan.SelectedValue.ToString());
                        sakit.Tahun = int.Parse(ddlTahun.SelectedValue.ToString());
                        sakit.CreatedBy = ((Users)Session["Users"]).UserID.ToString();
                        sakit.CreatedTime = DateTime.Now.ToLocalTime();
                        arrData.Add(sakit);
                        #endregion
                        #region Data Ijin
                        DispObj ijin = new DispObj();
                        ijin.NIK = NIK.Text;
                        ijin.Nama = Nama.Text;
                        ijin.DisiplinType = "Ijin";
                        ijin.DeptID = int.Parse(ddlDept.SelectedValue.ToString());
                        ijin.UserID = int.Parse(Uid.Text);// int.Parse(nIjin.ToolTip.Split('-')[1].ToString());
                        ijin.BagianID = int.Parse(nIjin.ToolTip.Split('-')[2].ToString());
                        ijin.Bobot = 10;
                        ijin.Nilai = int.Parse(nIjin.Text);
                        ijin.Score = decimal.Parse(dIjin.Text);
                        ijin.RowStatus = 0;
                        ijin.Bulan = int.Parse(ddlBulan.SelectedValue.ToString());
                        ijin.Tahun = int.Parse(ddlTahun.SelectedValue.ToString());
                        ijin.CreatedBy = ((Users)Session["Users"]).UserID.ToString();
                        ijin.CreatedTime = DateTime.Now.ToLocalTime();
                        arrData.Add(ijin);
                        #endregion
                        #region Data Alpa
                        DispObj alpa = new DispObj();
                        alpa.NIK = NIK.Text;
                        alpa.Nama = Nama.Text;
                        alpa.DisiplinType = "Alpa";
                        alpa.DeptID = int.Parse(ddlDept.SelectedValue.ToString());
                        alpa.UserID = int.Parse(Uid.Text);//int.Parse(nAlpha.ToolTip.Split('-')[1].ToString());
                        alpa.BagianID = int.Parse(nAlpha.ToolTip.Split('-')[2].ToString());
                        alpa.Bobot = 10;
                        alpa.Nilai = int.Parse(nAlpha.Text);
                        alpa.Score = decimal.Parse(dAlpha.Text);
                        alpa.RowStatus = 0;
                        alpa.Bulan = int.Parse(ddlBulan.SelectedValue.ToString());
                        alpa.Tahun = int.Parse(ddlTahun.SelectedValue.ToString());
                        alpa.CreatedBy = ((Users)Session["Users"]).UserID.ToString();
                        alpa.CreatedTime = DateTime.Now.ToLocalTime();
                        arrData.Add(alpa);
                        #endregion
                        #region Data Terlambat
                        DispObj Terlambat = new DispObj();
                        Terlambat.NIK = NIK.Text;
                        Terlambat.Nama = Nama.Text;
                        Terlambat.DisiplinType = "Terlambat";
                        Terlambat.DeptID = int.Parse(ddlDept.SelectedValue.ToString());
                        Terlambat.UserID = int.Parse(Uid.Text);//int.Parse(nLambat.ToolTip.Split('-')[1].ToString());
                        Terlambat.BagianID = int.Parse(nLambat.ToolTip.Split('-')[2].ToString());
                        Terlambat.Bobot = 10;
                        Terlambat.Nilai = int.Parse(nLambat.Text);
                        Terlambat.Score = decimal.Parse(dLambat.Text);
                        Terlambat.RowStatus = 0;
                        Terlambat.Bulan = int.Parse(ddlBulan.SelectedValue.ToString());
                        Terlambat.Tahun = int.Parse(ddlTahun.SelectedValue.ToString());
                        Terlambat.CreatedBy = ((Users)Session["Users"]).UserID.ToString();
                        Terlambat.CreatedTime = DateTime.Now.ToLocalTime();
                        arrData.Add(Terlambat);
                        #endregion

                        #region Data Surat Peringatan
                        DispObj sp = new DispObj();
                        sp.NIK = NIK.Text;
                        sp.Nama = Nama.Text;
                        sp.DisiplinType = "SP";
                        sp.DeptID = int.Parse(ddlDept.SelectedValue.ToString());
                        sp.UserID = int.Parse(Uid.Text);//int.Parse(nTP.ToolTip.Split('-')[1].ToString());
                        sp.BagianID = int.Parse(nTP.ToolTip.Split('-')[2].ToString());
                        sp.Bobot = 10;
                        //sp.Nilai = int.Parse(nTP.Text);
                        //sp.Score = decimal.Parse(dTP.Text);
                        sp.Nilai = int.Parse(nSP.Text);
                        sp.Score = decimal.Parse(dSP.Text);

                        sp.RowStatus = 0;
                        sp.Bulan = int.Parse(ddlBulan.SelectedValue.ToString());
                        sp.Tahun = int.Parse(ddlTahun.SelectedValue.ToString());
                        sp.CreatedBy = ((Users)Session["Users"]).UserID.ToString();
                        sp.CreatedTime = DateTime.Now.ToLocalTime();
                        arrData.Add(sp);
                        #endregion

                        #region Data Surat Teguran
                        DispObj st = new DispObj();
                        st.NIK = NIK.Text;
                        st.Nama = Nama.Text;
                        st.DisiplinType = "ST";
                        st.DeptID = int.Parse(ddlDept.SelectedValue.ToString());
                        st.UserID = int.Parse(Uid.Text);//int.Parse(nSP.ToolTip.Split('-')[1].ToString());
                        st.BagianID = int.Parse(nSP.ToolTip.Split('-')[2].ToString());
                        st.Bobot = 10;
                        st.Nilai = int.Parse(nTP.Text);
                        st.Score = decimal.Parse(dTP.Text);
                        st.RowStatus = 0;
                        st.Bulan = int.Parse(ddlBulan.SelectedValue.ToString());
                        st.Tahun = int.Parse(ddlTahun.SelectedValue.ToString());
                        st.CreatedBy = ((Users)Session["Users"]).UserID.ToString();
                        st.CreatedTime = DateTime.Now.ToLocalTime();
                        arrData.Add(st);
                        #endregion
                        #region Total Score
                        DispObj scr = new DispObj();
                        scr.UserID = int.Parse(Uid.Text);
                        scr.PICName = Nama.Text;
                        scr.DeptID = int.Parse(ddlDept.SelectedValue.ToString());
                        scr.BagianID = int.Parse(BagID.Text);
                        scr.Bulan = int.Parse(ddlBulan.SelectedValue.ToString());
                        scr.Tahun = int.Parse(ddlTahun.SelectedValue.ToString());
                        scr.PointNilai = decimal.Parse(nSCTotal.Text);
                        scr.CreatedBy = ((Users)Session["Users"]).UserID.ToString();
                        scr.CreatedTime = DateTime.Now.ToLocalTime();
                        arrScore.Add(scr);
                        #endregion
                    }

                }
            }
            //Session["Data"] = arrData;
            ZetroLib slb = new ZetroLib();
            slb.hlp = new DispObj();
            slb.Option = "Insert";
            slb.Criteria = "UserID,NIK,Nama,Bulan,Tahun,DisiplinType,DeptID,BagianID,Bobot,Nilai,Score,RowStatus,CreatedTime,CreatedBy";
            slb.TableName = "ISO_DisiplinData";
            slb.StoreProcedurName = "spISO_DisiplinData_Insert";
            string rst = slb.CreateProcedure();
            if (rst == string.Empty)
            {
                #region Proses data detail
                int rest = 0;
                foreach (DispObj ds in arrData)
                {

                    DispObj dsp = new DispObj();
                    dsp.UserID = ds.UserID;
                    dsp.NIK = ds.NIK;
                    dsp.Nama = ds.Nama;
                    dsp.DeptID = ds.DeptID;
                    dsp.Bulan = ds.Bulan;
                    dsp.Tahun = ds.Tahun;
                    dsp.DisiplinType = ds.DisiplinType;
                    dsp.BagianID = ds.BagianID;
                    dsp.Bobot = ds.Bobot;
                    dsp.Nilai = ds.Nilai;
                    dsp.Score = ds.Score;
                    dsp.RowStatus = 0;
                    dsp.CreatedTime = ds.CreatedTime;
                    dsp.CreatedBy = ds.CreatedBy;
                    slb.hlp = dsp;
                    ArrayList hb = HasBeenInput(ds.UserID, ds.DisiplinType);
                    if (hb.Count == 0)
                    {
                        rest = slb.ProcessData();
                        if (rest > 0)
                        {

                        }
                    }
                    #endregion
                }
                if (rest >= 0)
                {
                    //string rd = CreateProc();
                    DispObj sc = new DispObj();
                    //ZetroLib sdl=new ZetroLib();
                    slb.hlp = new DispObj();
                    slb.Option = "Insert";
                    slb.Criteria = "DeptID,UserID,PICName,BagianID,Bulan,Tahun,PointNilai,RowStatus,CreatedTime,CreatedBy";
                    slb.TableName = "ISO_DisiplinScore";
                    slb.StoreProcedurName = "spISO_DisiplinScore_Insert";
                    rst = slb.CreateProcedure();
                    foreach (DispObj ds in arrScore)
                    {
                        sc.UserID = ds.UserID;
                        sc.PICName = ds.PICName;
                        sc.DeptID = ds.DeptID;
                        sc.BagianID = ds.BagianID;
                        sc.PointNilai = ds.PointNilai;
                        sc.Bulan = ds.Bulan;
                        sc.Tahun = ds.Tahun;
                        sc.CreatedBy = ds.CreatedBy;
                        sc.CreatedTime = ds.CreatedTime;
                        sc.RowStatus = 0;
                        slb.hlp = sc;
                        ArrayList arrSc = HasBeenInput(ds.UserID);
                        if (arrSc.Count == 0)
                        {
                            int rist = slb.ProcessData();
                        }
                    }
                    ddlDept_Change(null, null);
                }
            }
        }
        private string CreateProc()
        {
            string rst = string.Empty;
            ZetroLib slb = new ZetroLib();
            slb.hlp = new DispObj();
            slb.Option = "Insert";
            slb.Criteria = "DeptID,UserID,PICName,Bulan,Tahun,PointNilai,RowStatus,CreatedTime,CreatedBy";
            slb.TableName = "ISO_DisiplinScore";
            slb.StoreProcedurName = "spISO_DisiplinScore_Insert";
            rst = slb.CreateProcedure();
            return rst;
        }
        private ArrayList HasBeenInput(int UserID, string DisiplinTipe)
        {
            ArrayList arrData = new ArrayList();
            Disiplin dsp = new Disiplin();
            dsp.Pilihan = "Data";
            dsp.Criteria = " and UserID=" + UserID;
            dsp.Criteria += " and DisiplinType='" + DisiplinTipe + "'";
            dsp.Criteria += " and Bulan=" + ddlBulan.SelectedValue;
            dsp.Criteria += " and Tahun=" + ddlTahun.SelectedValue;
            arrData = dsp.Retrieve();
            return arrData;
        }
        private ArrayList HasBeenInput(int UserID)
        {
            ArrayList arrData = new ArrayList();
            Disiplin dsp = new Disiplin();
            dsp.Pilihan = "Rekap";
            dsp.Criteria = " and UserID=" + UserID;
            dsp.Criteria += " and Bulan=" + ddlBulan.SelectedValue;
            dsp.Criteria += " and Tahun=" + ddlTahun.SelectedValue;
            arrData = dsp.Retrieve();
            return arrData;
        }
        private int GetIDData(int UserID, string DisType, string BagianID)
        {
            int rst = 0;
            Disiplin dsp = new Disiplin();
            dsp.Pilihan = "Data";
            dsp.Criteria = " and UserID=" + UserID;
            dsp.Criteria += " and BagianID=" + BagianID;
            dsp.Criteria += " and Tahun=" + ddlTahun.SelectedValue;
            dsp.Criteria += " and Bulan=" + ddlBulan.SelectedValue;
            dsp.Criteria += " and DisiplinType='" + DisType + "'";
            DispObj dis = dsp.Retrieve(true);
            rst = dis.ID;
            return rst;
        }
        private int GetIDData(int UserID, string BagianID)
        {
            int rst = 0;
            Disiplin dsp = new Disiplin();
            dsp.Pilihan = "Rekap";
            dsp.Criteria = " and UserID=" + UserID;
            dsp.Criteria += " and BagianID=" + BagianID;
            dsp.Criteria += " and Tahun=" + ddlTahun.SelectedValue;
            dsp.Criteria += " and Bulan=" + ddlBulan.SelectedValue;
            //dsp.Criteria += " and DisiplinType='" + DisType + "'";
            DispObj dis = dsp.Retrieve(true);
            rst = dis.ID;
            return rst;
        }
    }

    public class Disiplin
    {
        private ArrayList arrData = new ArrayList();
        private DispObj obj = new DispObj();
        public string Criteria { get; set; }
        public string Pilihan { get; set; }
        public string OrderBy { get; set; }
        public string Where { get; set; }
        public ArrayList Retrieve()
        {
            arrData = new ArrayList();
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(this.Query());
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(this.GenerateObj(sdr));
                }
            }
            return arrData;
        }
        public DispObj Retrieve(bool detail)
        {
            obj = new DispObj();
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(this.Query());
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    return this.GenerateObj(sdr);
                }
            }
            return obj;
        }
        private DispObj GenerateObj(SqlDataReader sdr)
        {
            obj = new DispObj();
            switch (this.Pilihan)
            {
                case "Detail":
                    obj.NIK = sdr["NIK"].ToString();
                    obj.Nama = sdr["UserName"].ToString();
                    obj.BagianName = sdr["BagianName"].ToString();
                    obj.UserID = Convert.ToInt32(sdr["UserID"].ToString());
                    obj.BagianID = Convert.ToInt32(sdr["BagianID"].ToString());
                    obj.DeptID = Convert.ToInt32(sdr["DeptID"].ToString());
                    break;
                case "Data":
                    obj.ID = Convert.ToInt32(sdr["ID"].ToString());
                    obj.DisiplinType = sdr["DisiplinType"].ToString();
                    obj.UserID = Convert.ToInt32(sdr["UserID"].ToString());
                    obj.Nama = sdr["Nama"].ToString();
                    obj.NIK = sdr["NIK"].ToString();
                    obj.DeptID = Convert.ToInt32(sdr["DeptID"].ToString());
                    obj.BagianID = Convert.ToInt32(sdr["BagianID"].ToString());
                    obj.DisiplinType = (sdr["DisiplinType"].ToString());
                    obj.Bobot = Convert.ToInt32(sdr["Bobot"].ToString());
                    obj.Nilai = Convert.ToInt32(sdr["Nilai"].ToString());
                    obj.Score = Convert.ToDecimal(sdr["Score"].ToString());
                    break;
                case "Rekap":
                    obj.ID = Convert.ToInt32(sdr["ID"].ToString());
                    obj.UserID = Convert.ToInt32(sdr["userID"].ToString());
                    obj.PICName = sdr["PICName"].ToString();
                    obj.DeptID = Convert.ToInt32(sdr["DeptID"].ToString());
                    obj.Bulan = Convert.ToInt32(sdr["Bulan"].ToString());
                    obj.Tahun = Convert.ToInt32(sdr["Tahun"].ToString());
                    obj.PointNilai = Convert.ToDecimal(sdr["PointNilai"].ToString());

                    break;
                case "Export":
                    obj.NIK = sdr["NIK"].ToString();
                    obj.Nama = sdr["Nama"].ToString();
                    obj.BagianName = sdr["BagianName"].ToString();
                    obj.JmlSakit = Convert.ToInt32(sdr["JmlSakit"].ToString());
                    obj.ScoreSakit = Convert.ToDecimal(sdr["ScoreSakit"].ToString());
                    obj.JmlIjin = Convert.ToInt32(sdr["JmlIjin"].ToString());
                    obj.JmlAlpa = Convert.ToInt32(sdr["JmlAlpa"].ToString());
                    obj.JmlLambat = Convert.ToInt32(sdr["JmlLambat"].ToString());
                    obj.JmlSP = Convert.ToInt32(sdr["JmlSP"].ToString());
                    obj.JmlST = Convert.ToInt32(sdr["JmlST"].ToString());
                    obj.ScoreIjin = Convert.ToDecimal(sdr["ScoreIjin"].ToString());
                    obj.ScoreAlpa = Convert.ToDecimal(sdr["ScoreAlpa"].ToString());
                    obj.ScoreLambat = Convert.ToDecimal(sdr["ScoreLambat"].ToString());
                    obj.ScoreSP = Convert.ToDecimal(sdr["ScoreSP"].ToString());
                    obj.ScoreST = Convert.ToDecimal(sdr["ScoreST"].ToString());
                    obj.Total = Convert.ToDecimal(sdr["Total"].ToString());
                    obj.Score = Convert.ToDecimal(sdr["TotalScore"].ToString());
                    break;
            }
            return obj;

        }
        private string Query()
        {
            string query = string.Empty;
            switch (this.Pilihan)
            {
                case "Detail":
                    query = "select ua.NIK,ua.UserName,ua.UserID,D.DeptName,ib.BagianName,ib.ID as BagianID,ua.DeptID " +
                          " from UserAccount AS ua " +
                          " LEFT JOIN Dept as D on D.ID=ua.DeptID " +
                          " LEFT JOIN ISO_Bagian as ib " +
                          " ON ib.ID=ua.BagianID WHERE ua.RowStatus>-2 " + this.Criteria;
                    query = "SELECT  * FROM( " +
                          " SELECT ua.NIK,ua.UserName,ua.UserID,D.DeptName,ib.BagianName,ib.ID as BagianID,ua.DeptID,isnull(ib.Urutan,0)Urutan  " +
                          " FROM UserAccount AS ua   " +
                          " LEFT JOIN Dept as D on D.ID=ua.DeptID   " +
                          "  LEFT JOIN ISO_Bagian as ib  ON ib.ID=ua.BagianID WHERE ua.RowStatus>-1   " +
                          "  " + this.Criteria +
                          "  UNION ALL " +
                          "  (SELECT ua.NIK,ua.UserName,ua.UserID,D.DeptName,ib.BagianName,ib.ID as BagianID,ua.DeptID,isnull(ib.Urutan,0)Urutan  " +
                          " FROM UserAccount AS ua   " +
                          "  LEFT JOIN Dept as D on D.ID=ua.DeptID  " +
                          "  LEFT JOIN ISO_Bagian as ib  ON ib.ID=ua.BagianID WHERE ua.RowStatus>-2  " +
                             this.Criteria + " AND ua.UserID in( " +
                          " SELECT UserID from ISO_DisiplinData where RowStatus>-1 " + this.Criteria + this.Where + ")) " +
                          "  ) as x  " +
                          "  group by NIK,UserName,UserID,DeptName,BagianName,BagianID,DeptID,Urutan " +
                          "  order by  Cast(Urutan as int),UserName";
                    break;
                case "Data":
                    query = "select * from ISO_DisiplinData where RowStatus>-1 " + this.Criteria;
                    break;
                case "Rekap":
                    query = "select * from ISO_DisiplinScore where RowStatus>-1 " + this.Criteria;
                    break;
                case "Export":
                    query = "SELECT NIK,Nama,(SELECT BagianName FROM ISO_Bagian WHERE ID=BagianID)BagianName, " +
                          "  SUM(Sakit)JmlSakit,SUM(ns)ScoreSakit,SUM(Ijin)JmlIjin,SUM(ni)ScoreIjin, " +
                          "  SUM(Alpa)JmlAlpa,Sum(na)ScoreAlpa,Sum(Terlambat)JmlLambat,Sum(nt)ScoreLambat, " +
                          "  SUM(SP)JmlSP,SUM(nsp)ScoreSP,SUM(ST)JmlST,SUM(nst)ScoreST,SUM(Score)TotalScore " +
                          ",(SUM(ns)+SUM(ni)+Sum(na)+Sum(nt)+sum(nsp)+sum(nst))Total " +
                          "  FROM( " +
                          "  select NIK,NAMA,BagianID,SUM([Sakit])Sakit,SUM([Ijin])Ijin,SUM([Alpa])Alpa, " +
                          "  SUM([Terlambat])Terlambat,SUM([SP])SP,SUM([ST])ST,(SUM(Score)/6)Score, " +
                          "  0 ns,0 ni,0 na,0 nt,0 nsp,0 nst " +
                          "  FROM (SELECT * FROM ISO_DisiplinData WHERE RowStatus>-1 " + this.Criteria + ") as x " +
                          "  pivot(Max(Nilai) FOR DisiplinType in([Sakit],[Alpa],[Ijin],[Terlambat],[SP],[ST])) as xx " +
                          "  GROUP By Nama,NIK,BagianID " +
                          "  UNION ALL ( " +
                          "  SELECT NIK,NAMA,BagianID,0 ss,0 si, 0 sa, 0 st, 0 sp, 0 st,0 score,SUM([Sakit])Sakit, " +
                          "  SUM([Ijin])Ijin,SUM([Alpa])Alpa,SUM([Terlambat])Terlambat,SUM([SP])SP,SUM([ST])ST " +
                          "  FROM (select * FROM ISO_DisiplinData WHERE RowStatus>-1 " + this.Criteria + ") as x " +
                          "  pivot(Max(Score) FOR DisiplinType in([Sakit],[Alpa],[Ijin],[Terlambat],[SP],[ST])) as xx " +
                          "  GROUP By Nama,NIK,BagianID) " +
                          "  ) as xx " +
                          "  GROUP By Nama,NIK,BagianID " +
                          "  ORDER By Nama,NIK ";
                    break;
            }
            return query;
        }
        public void GetTahun(DropDownList ddl)
        {
            arrData = new ArrayList();
            string strSQL = "select distinct YEAR(tglMulai) Tahun from ISO_Task order by year(tglMulai)";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            int Tahun = 0;
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddl.Items.Add(new ListItem(sdr["Tahun"].ToString(), sdr["Tahun"].ToString()));
                    Tahun = (sdr["Tahun"] != DBNull.Value) ? Convert.ToInt32(sdr["Tahun"].ToString()) : 0;
                }
            }
            if (Tahun < DateTime.Now.Year)
            {
                ddl.Items.Add(new ListItem(DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString()));
            }
        }
        public ArrayList RetrieveDept()
        {
            arrData = new ArrayList();
            string strSQL = "Select * from Dept Where RowStatus>-1 " + this.Criteria + " order by Alias";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new DispObj
                    {
                        DeptID = Convert.ToInt32(sdr["ID"].ToString()),
                        DeptName = sdr["Alias"].ToString()
                    });
                }
            }
            return arrData;
        }
    }

    public class DispObj : GRCBaseDomain
    {
        public int DeptID { get; set; }
        public string DeptName { get; set; }
        public int Bulan { get; set; }
        public int Tahun { get; set; }
        public string NIK { get; set; }
        public string Nama { get; set; }
        public int BagianID { get; set; }
        public int Bobot { get; set; }
        public string DisiplinType { get; set; }
        public int Nilai { get; set; }
        public decimal Score { get; set; }
        public string BagianName { get; set; }
        public int UserID { get; set; }
        public string PICName { get; set; }
        public decimal PointNilai { get; set; }

        public int JmlSakit { get; set; }
        public decimal ScoreSakit { get; set; }
        public int JmlIjin { get; set; }
        public decimal ScoreIjin { get; set; }
        public int JmlAlpa { get; set; }
        public decimal ScoreAlpa { get; set; }
        public int JmlLambat { get; set; }
        public decimal ScoreLambat { get; set; }
        public int JmlSP { get; set; }
        public decimal ScoreSP { get; set; }
        public int JmlST { get; set; }
        public decimal ScoreST { get; set; }
        public decimal Total { get; set; }
        public string AlisName { get; set; }
    }
}
