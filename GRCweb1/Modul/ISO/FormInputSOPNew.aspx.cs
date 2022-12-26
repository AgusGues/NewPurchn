using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using System.Reflection;
using System.IO;

namespace GRCweb1.Modul.ISO
{
    public partial class FormInputSOPNew : System.Web.UI.Page
    {
        public string Bulan = Global.nBulan(DateTime.Now.Month);
        private decimal scr = 0;
        int total1 = 0;
        int total2 = 0;
        private string InputanBaruAktif = string.Empty;
        public string Judul = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadDept();
                LoadBulan();
                LoadTahun();
                LoadPICNew();
                txtProses.Text = string.Empty;
                txtPES.Text = ((Request.QueryString["tp"] == null) ? "3" : Request.QueryString["tp"].ToString());
                txtNamaPES.Text = ((Request.QueryString["tp"].ToString() == "3") ? "SOP" : "KPI");
                GetIedulFitri();
                //Panel2.Visible = false;
            }
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }

        //penambahan (agus) target kpi berubah ketika idul fitri 03-08-2022
        protected int GetIedulFitri()
        {
            int ads = 0;
            ZetroView zv = new ZetroView();
            zv.QueryType = Operation.CUSTOM;
            zv.CustomQuery =
                "select count(harilibur) total from CalenderOffDay where keterangan like '%fitri%' and year(harilibur)=" + ddlTahun.SelectedItem.Text +
                " and month(harilibur)=" + ddlBulan.SelectedValue;
            SqlDataReader dr = zv.Retrieve();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ads = Int32.Parse(dr["Total"].ToString());
                }
            }
            return ads;
        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPICNew();
            //LoadSection();
            int LastDay = DateTime.DaysInMonth(int.Parse(ddlTahun.SelectedValue.ToString()), int.Parse(ddlBulan.SelectedValue.ToString()));
            //txtTglMulai.Text = LastDay.ToString().PadLeft(2, '0') + "-" + ddlBulan.SelectedValue.PadLeft(2, '0') + "-" + ddlTahun.SelectedValue.ToString();
            txtTglMulai.Text = LastDay.ToString().PadLeft(2, '0') + "-" + ddlBulan.SelectedValue.PadLeft(2, '0') + "-" + ddlTahun.SelectedValue.ToString();
        }
        protected void ddlPIC_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSection();
            ddlBagian.SelectedValue = new DeptFacade().GetBagianID(int.Parse(ddlPIC.SelectedValue.ToString())).ToString();
            int bagID = new DeptFacade().GetBagianID(int.Parse(ddlPIC.SelectedValue.ToString()));
            txtPICName.Text = new ISO_UserFacade().RetrieveByISOuserID(ddlPIC.SelectedValue.ToString()).UserName.ToString();
            ddlBagian.SelectedValue = bagID.ToString();
            ddlBagian_SelectedIndexChanged(null, null);
            btnSimpan.Enabled = true;
            txtTaskNo.Text = string.Empty;
        }
        protected void ddlBagian_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBagian.SelectedIndex > 0)
            {
                ISO_BagianFacade iso_BagianFacade = new ISO_BagianFacade();
                ISO_Bagian isoBagian = iso_BagianFacade.RetrieveById(int.Parse(ddlBagian.SelectedValue));
                if (isoBagian.ID > 0)
                {
                    LoadList();
                }
            }
            else
            {
                DisplayAJAXMessage(this, "Pilih Section ");
                ddlBagian.Focus();
            }
        }
        private void LoadTahun()
        {
            ddlTahun.Items.Clear();
            ddlTahun.Items.Add(new ListItem((DateTime.Now.Year - 1).ToString(), (DateTime.Now.Year - 1).ToString()));
            ddlTahun.Items.Add(new ListItem(DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString()));
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();

        }
        protected void ddlBulan_Change(object sender, EventArgs e)
        {
            try
            {
                int LastDay = DateTime.DaysInMonth(int.Parse(ddlTahun.SelectedValue.ToString()), int.Parse(ddlBulan.SelectedValue.ToString()));
                txtTglMulai.Text = LastDay.ToString().PadLeft(2, '0') + "-" + ddlBulan.SelectedValue.PadLeft(2, '0') + "-" + ddlTahun.SelectedValue.ToString();
                LoadList();
            }
            catch { }
        }
        protected void ddlTahun_Change(object sender, EventArgs e)
        {
            int LastDay = DateTime.DaysInMonth(int.Parse(ddlTahun.SelectedValue.ToString()), int.Parse(ddlBulan.SelectedValue.ToString()));
            txtTglMulai.Text = LastDay.ToString().PadLeft(2, '0') + "-" + ddlBulan.SelectedValue.PadLeft(2, '0') + "-" + ddlTahun.SelectedValue.ToString();
            LoadList();
        }
        private void LoadBulan()
        {
            ddlBulan.Items.Clear();
            ddlBulan.Items.Add(new ListItem("--pilih bulan--", "0"));
            int n = 0;
            for (int i = 0; i < 12; i++)
            {
                n = i + 1;
                ddlBulan.Items.Add(new ListItem(Global.nBulan(n).ToString(), n.ToString()));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
        }
        private void LoadPICNew()
        {
            ArrayList arrUser = new ArrayList();
            UsersFacade usFac = new UsersFacade();
            usFac.Criteria = " and DeptID=" + ddlDept.SelectedValue.ToString();
            usFac.Criteria += " order by UserName";
            arrUser = usFac.RetriveUserAccount();
            ddlPIC.Items.Clear();
            ddlPIC.Items.Add(new ListItem("--Pilih PIC--", "0"));
            foreach (Users user in arrUser)
            {
                ddlPIC.Items.Add(new ListItem(user.UserName, user.ID.ToString()));
            }
            //ddlPIC.SelectedValue = ((Users)Session["Users"]).ID.ToString();
        }
        private bool Contains(string Data, string Obj)
        {
            string[] d = Data.Split(',');
            int posx = Array.IndexOf(d, Obj);
            return (posx > -1) ? true : false;
        }
        private void LoadPIC()
        {
            UsersFacade usersFacade = new UsersFacade();
            Users users2 = usersFacade.RetrieveByUserName(((Users)Session["Users"]).UserName);
            string inDeptID = usersFacade.GetDeptOto(users2.ID);
            string[] onlydept = inDeptID.Split(',');

            ISO_UserFacade isoUserFacade = new ISO_UserFacade();
            isoUserFacade.TypePES = (Contains(inDeptID, "25") || Contains(inDeptID, "27")) ? "" : "3";
            string inDeptids = string.Empty;
            if (onlydept.Count() > 1)
            {
                string inDeptid = (Contains(inDeptID, "25")) ? inDeptID.Replace("25", "") : inDeptID;
                string[] trimDept = inDeptid.Split(',');
                inDeptids = (trimDept[0] == string.Empty) ? inDeptid.Substring(1, inDeptid.Length - 1) : inDeptid.Substring(0, inDeptid.Length - 1);
            }
            else
            {
                inDeptids = inDeptID;
            }
            ArrayList arrIsoUser = isoUserFacade.RetrievePIC(users2.DeptID.ToString());

            if (arrIsoUser.Count > 0)
            {
                ddlPIC.Items.Clear();
                ddlPIC.Items.Add(new ListItem("--Choose PIC--", "0"));

                foreach (ISO_Users iso_User in arrIsoUser)
                {
                    ddlPIC.Items.Add(new ListItem(iso_User.UserName, iso_User.ID.ToString()));
                }

                ddlPIC.ClearSelection();
                foreach (ListItem item in ddlPIC.Items)
                {
                    if (item.Text == ((Users)Session["Users"]).UserName)
                    {
                        item.Selected = true;
                        return;
                    }
                }
            }

        }
        private void LoadDept()
        {
            UsersFacade usersFacade = new UsersFacade();
            Users users2 = usersFacade.RetrieveByUserName(((Users)Session["Users"]).UserName);

            DeptFacade deptFacade = new DeptFacade();
            deptFacade.Criteria = " order by Alias";
            ArrayList arrDept = new ArrayList();
            switch (users2.UnitKerjaID)
            {
                case 1:
                case 7:
                    arrDept = deptFacade.RetrieveAliasDept();
                    break;
                default:
                    arrDept = deptFacade.RetrieveAliasDept();
                    break;
            }

            Auth DeptAuth = new Auth();
            UserAuth oto = new UserAuth();
            oto.Option = "All";
            oto.Criteria = " and UserID=" + ((Users)Session["Users"]).ID.ToString();
            oto.Criteria += " and ModulName='Input SOP' ";
            DeptAuth = oto.Retrieve(true);
            string UserDept = ((Users)Session["Users"]).DeptID.ToString() + ",0";
            string[] arrDepts = (DeptAuth.AuthDept != null) ? DeptAuth.AuthDept.Split(',') : UserDept.ToString().Split(',');
            ddlDept.Items.Clear();
            List<ListItem> lte = new List<ListItem>();
            if (deptFacade.Error == string.Empty)
            {
                ddlDept.Items.Add(new ListItem("-- Choose Dept --", "0"));

                for (int i = 0; i < arrDepts.Count(); i++)
                {
                    foreach (Dept dept in arrDept)
                    {
                        if (int.Parse(arrDepts[i].ToString()) == dept.ID)
                        {
                            ddlDept.Items.Add(new ListItem(dept.AlisName, dept.ID.ToString()));
                        }
                    }
                }
            }
            //sorting dropdwonlist
            foreach (ListItem lt in ddlDept.Items)
            {
                lte.Add(lt);
            }
            List<ListItem> sorted = lte.OrderBy(b => b.Text).ToList();
            ddlDept.Items.Clear();
            foreach (ListItem l in sorted)
            {
                ddlDept.Items.Add(l);
            }
        }
        public ArrayList LoadDept(string Criteria)
        {
            ArrayList arrData = new ArrayList();
            //bpas_api.WebService1 api = new bpas_api.WebService1();
            Global2 api = new Global2();
            DataSet da = new DataSet();
            da = api.GetDataTable("Dept", "*", "Where RowStatus>-1 " + Criteria + " Order By DeptName", "GRCBOARDPURCH");
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
        private void LoadSection()
        {
            ArrayList arrSec = new ArrayList();
            DeptFacade dep = new DeptFacade();
            dep.Criteria = " and DeptID=" + ddlDept.SelectedValue.ToString();
            dep.Criteria += " order by cast(Urutan as int) ";
            arrSec = dep.RetrieveBagian(string.Empty);
            ddlBagian.Items.Clear();
            if (ddlDept.SelectedIndex > 0)
            {
                ddlBagian.Items.Add(new ListItem("--Pilih Section--", "0"));
                foreach (Dept dp in arrSec)
                {
                    ddlBagian.Items.Add(new ListItem(dp.BagianName, dp.ID.ToString()));
                }
            }
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        private void LoadList()
        {
            Session["DataPes"] = null;
            ArrayList arrData = new ArrayList();
            arrData = this.HasBeenInput();
            if (arrData.Count == 0)
            {
                arrData = new ArrayList();
                SOPNew sp = new SOPNew();
                sp.Pilihan = "CAT";
                sp.Criteria = " and iu.PesType=" + txtPES.Text;
                sp.Criteria += " and iu.UserID=" + ddlPIC.SelectedValue.ToString();
                sp.Criteria += " and iu.SectionID=" + ddlBagian.SelectedValue.ToString();
                sp.Criteria += " order by cast(ic.KodeUrutan as int)";
                arrData = sp.Retrieve();
            }
            lstSOP.DataSource = arrData;
            lstSOP.DataBind();
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            Bulan = ddlBulan.SelectedItem.Text;
            ListPIC();
            Panel2.Visible = true;
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=RekapSOPKPI.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "Departement :" + ddlDept.SelectedItem.Text;
            Html += "<br>Periode : " + ddlBulan.SelectedItem.Text + " " + ddlTahun.SelectedValue.ToString();
            Html += "<br>PIC     : " + ddlPIC.SelectedItem.Text;
            Bulan = ddlBulan.SelectedItem.Text;
            string HtmlEnd = "";
            //lstForPrint.RenderControl(hw);
            Panel2.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0\"", "border=\"1\"");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
            Panel2.Visible = false;
        }
        public override void VerifyRenderingInServerForm(Control control) { }
        protected void lstSOP_DataBound(object sender, RepeaterItemEventArgs e)
        {
            

            string NamaPES = txtNamaPES.Text; Users users = (Users)Session["Users"];
            InputanBaruAktif = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("OtoRebobotAktif", "PES");
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ISO_SOP ip = (ISO_SOP)e.Item.DataItem;
                TextBox capai = (TextBox)e.Item.FindControl("txtKeterangan");
                //TextBox kete = e.Item.FindControl("txtKeterangan") as TextBox;
                DropDownList ddl = (DropDownList)e.Item.FindControl("ddlStatus");
                Label score = (Label)e.Item.FindControl("txtPointNilai");
                Label point2 = (Label)e.Item.FindControl("txtPoint");
                Label tglinput = (Label)e.Item.FindControl("txttglinput");
                Label tglapproved = (Label)e.Item.FindControl("txttglapproved");
                Image img = (Image)e.Item.FindControl("edit");
                Label appStat = (Label)e.Item.FindControl("txtStatus");
                //CheckBox chk = (CheckBox)e.Item.FindControl("chkRbb");
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("lstP");

                /** New 18 Juli 2021 **/
                Label LSOP = (Label)e.Item.FindControl("LabelSOP");
                Label LPencapaian = (Label)e.Item.FindControl("LabelCapai");
                /** End New 18 Juli 2021 **/

                tr.Attributes["title"] = "";
                
                string target1 = "100 % (800 ton) Min";
                string target2 = "100 % (400 ton) Min";

                if (LSOP.Text == "Pemasukan kertas kantong semen Jabobar")
                {
                    if (GetIedulFitri() > 0 || ip.Target == target1)
                    {
                        tr.Cells[1].Attributes.Add("title", target2);
                    }
                    else
                    {
                        tr.Cells[1].Attributes.Add("title", target1);
                    }
                    
                }
                else
                {
                    tr.Cells[1].Attributes.Add("title", ip.Target.ToString());
                }

                
                //tr.Cells[1].Attributes.Add("title", ip.Target.ToString());

                SOPNew sp = new SOPNew();
                sp.Pilihan = "Score";
                sp.PES = NamaPES;
                sp.Criteria = (txtProses.Text == "Insert") ? " and RowStatus=0 and CategoryID=" + ip.ID.ToString() :
                               " and CategoryID=(Select CategoryID from ISO_UserCategory Where ID=" + ip.CategoryID.ToString() + ")";
                ddl.Items.Clear();
                ddl.Items.Add(new ListItem("", "0"));
                foreach (ISO_SOP iso in sp.Retrieve())
                {
                    ddl.Items.Add(new ListItem(iso.Targete.ToString(), iso.ID.ToString()));
                }
                ddl.Enabled = false; //kunci ddl target 
                ddl.ToolTip = "Isi Kolom Aktual terlebih dulu";
                /** ambil data detail untuk scoreid*/
                SOPNew sd = new SOPNew();
                sd.Pilihan = "Detail";
                sd.PES = NamaPES;
                sd.Criteria = " and " + NamaPES + "ID=" + ip.ID.ToString();
                ISO_SOP sdp = sd.Retrieve(true);
                ddl.SelectedValue = sdp.SopScoreID.ToString();
                ddl.BorderStyle = BorderStyle.None;
                ddl.BorderStyle = BorderStyle.NotSet;
                capai.Text = ip.Pencapaian;
                /**
                 * Retrieve sopdetail
                 */
                SOPNew sn = new SOPNew();
                ISO_SOP isp = new ISO_SOP();
                if (ip.SopNo != "")
                {
                    sn.Pilihan = "Detail";
                    sn.PES = NamaPES;
                    sn.Criteria = " and " + NamaPES + "ID =" + ip.ID.ToString();

                    isp = sn.Retrieve(true);
                }
                SOPNew st = new SOPNew();
                st.Pilihan = "Header2";
                st.PES = NamaPES;
                st.Criteria = " and sop.DeptID=" + ddlDept.SelectedValue.ToString();
                st.Criteria += " and ISO_UserID=" + ddlPIC.SelectedValue.ToString();
                st.Criteria += " and sop.BagianID=" + ddlBagian.SelectedValue.ToString();
                st.Criteria = " and " + NamaPES + "ID =" + ip.ID.ToString();
                st.Criteria += " and Month(TglMulai)=" + ddlBulan.SelectedValue.ToString();
                st.Criteria += " and Year(TglMulai)=" + ddlTahun.SelectedValue.ToString();
                //st.Criteria += " Order by ID ";
                //st.Criteria2 = "" + NamaPES + "ID";
                //ddl.Items.Clear();
                //ddl.Items.Add(new ListItem("", "0"));
                foreach (ISO_SOP isd in st.Retrieve())
                {
                    point2.Text = isd.Point.ToString();
                }
                ISO_SOP stq = st.Retrieve(true);
                point2.Text = ip.Point.ToString();
                SOPNew sm = new SOPNew();
                ISO_SOP isq = new ISO_SOP();
                ArrayList arrData = new ArrayList();
                if (ip.SopNo != "")
                {
                    sm.Pilihan = "Header2";
                    sm.PES = NamaPES;
                    sm.Criteria = " and sop.DeptID=" + ddlDept.SelectedValue.ToString();
                    sm.Criteria += " and ISO_UserID=" + ddlPIC.SelectedValue.ToString();
                    sm.Criteria += " and sop.BagianID=" + ddlBagian.SelectedValue.ToString();
                    sm.Criteria = " and " + NamaPES + "ID =" + ip.ID.ToString();
                    sm.Criteria += " and Month(TglMulai)=" + ddlBulan.SelectedValue.ToString();
                    sm.Criteria += " and Year(TglMulai)=" + ddlTahun.SelectedValue.ToString();
                    //sm.Criteria += " Order by ID ";
                    //sm.Criteria2 = "" + NamaPES + "ID";
                    //ddl.Items.Clear();
                    //ddl.Items.Add(new ListItem("", "0"));
                    foreach (ISO_SOP isw in sm.Retrieve())
                    {
                        point2.Text = isw.Point.ToString();
                    }
                    isq = sm.Retrieve(true);
                }

                ddl.SelectedValue = (txtProses.Text == "Insert") ? "0" : isp.SopScoreID.ToString();
                score.Text = (txtProses.Text == "Insert") ? "" : isp.PointNilai.ToString("##0");
                point2.Text = (txtProses.Text == "Insert") ? "" : isq.Point.ToString("##0");
                tglinput.Text = (txtProses.Text == "Insert") ? string.Empty : isq.TglInput.ToString("dd-MMM-yyyy");
                //tglapproved.Text = (txtProses.Text == "Update") ? "" : isq.TglApproved.ToString("dd-MMM-yyyy");
                tglapproved.Text = (isp.Approval == 2 && isq.TglApproved != DateTime.MinValue) ? isq.TglApproved.ToString("dd-MMM-yyyy") : "";
                #region edit by razib
                //appStat.Text = (isp.Status == 0) ? "Open" : "Approved";
                #endregion
                if (isp.Status == 0)
                {
                    appStat.Text = "Open";
                }
                else if (isp.Status == 1)
                {
                    appStat.Text = "UnApproved";
                    appStat.ToolTip = isq.AlasanUnApprove;
                }
                else
                {
                    appStat.Text = "Approved";
                }
                appStat.Text = (txtProses.Text == "Insert") ? "" : appStat.Text;
                img.Visible = (ip.SopNo != string.Empty) ? true : false;
                #region edit by Razib
                //img.Visible = (isp.Status > 0) ? false : img.Visible;
                #endregion
                if (isp.Status > 0)
                {
                    img.Visible = false;
                }
                if (isp.Status == 1)
                {
                    img.Visible = true;
                }
                capai.ReadOnly = (isp.Status > 0 || ip.SopNo != "") ? true : false;
                ddl.Enabled = (isp.Status > 0 || ip.SopNo != "") ? false : ddl.Enabled;
                scr += ip.BobotNilai;
                capai.CssClass = "txtongrid tengah";
                if (InputanBaruAktif == "1")
                {
                    //check nilai bulan sebelum nya jika ada
                    if (ip.Penilaian > 1)
                    {
                        foreach (ISO_SOP iss in NilaiSebelumNya(ip.CategoryID.ToString(), ip.Penilaian.ToString(), NamaPES))
                        {
                            score.Text = iss.PointNilai.ToString();
                            //point2.Text = iss.Point.ToString();
                            ddl.SelectedValue = iss.SopScoreID.ToString();
                            capai.Text = iss.Pencapaian.ToUpper();

                            //chk.Enabled = (iss.IdDetail > 0) ? false : true;
                        }
                    }
                    //chk.Visible = (ip.Penilaian > 1) ? true : false;
                    //chk.ToolTip = "Klik checkbox jika akan mengisi nilai item ini,\nNilai ini otomatis menjadi nilai rebobot di periode ini.";
                    //chk.ToolTip += "\nDan jika sudah pernah ada nilai di bulan sebelum nya \ndalam satu periode maka akan di lakukan Average otomatis ";
                    capai.ReadOnly = (ip.Penilaian > 1) ? true : false;
                    ddl.Enabled = (ip.Penilaian > 1) ? false : ddl.Enabled;
                    //chk.Visible = (sdp.Rebobot > 0) ? true : chk.Visible;
                    //chk.Enabled = (sdp.Rebobot > 0) ? true : chk.Enabled;
                }
                total1 += Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Point"));

                /* WO-IT-K0030221 Sodikin*/
                #region lock link auto pes
                ZetroView zv = new ZetroView();
                zv.QueryType = Operation.CUSTOM;
                string sarmut = string.Empty;
                zv.CustomQuery = "SELECT isnull(sarmut,'')sarmut from iso_usercategory where id=" + ip.CategoryID;
                SqlDataReader dr = zv.Retrieve();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        sarmut = dr["sarmut"].ToString();
                    }
                }

                if (sarmut.Trim() == string.Empty)
                {
                    if (img.Visible == true) img.Visible = true;
                    capai.ReadOnly = false;
                }
                else
                {
                    if (img.Visible == true) img.Visible = false;
                    capai.ReadOnly = true;
                    //WO link to PES
                    if (appStat.Text != "Approved")
                    {
                        string stractual = GetActualValueAotuPES(ip.CategoryID);

                        string logic = string.Empty;
                        string logic2 = string.Empty;

                        if (users.UnitKerjaID == 7)
                        {
                            logic2 = "";
                            logic = " and NamaPlant in ('Karawang','Depo Cirebon')";
                        }
                        else if (users.UnitKerjaID == 13)
                        {
                            logic2 = "";
                            logic = " and NamaPlant in ('Jombang','DEPO JEMBER')";
                        }
                        else if (users.UnitKerjaID == 1)
                        {
                            logic2 = "";
                            logic = "";
                        }
                        else
                        {
                            logic2 = "[sqlctrp.grcboard.com].bpasctrp.dbo.";
                            logic = " and NamaPlant in ('DEPO SEMARANG','DEPO SOLO','DEPO JOGJA','DEPO PURWOKERTO') ";
                        }
                        /** New 18 Juli 2021 **/
                        string stractual2 = GetActualValueAotuPES2(ip.CategoryID, logic, logic2);
                        /** End New 18 Juli 2021 **/


                        if (stractual != string.Empty)
                        {
                            /** New 18 Juli 2021 **/
                            if (LSOP.Text.Trim() == "Pemantauan Budget Dept" || LSOP.Text.Trim() == "Pencapaian Budgeting Dept")
                            {
                                capai.Text = stractual2;
                                //Session["stractual"] = stractual2;
                            }

                            else
                            {
                                capai.Text = stractual;
                                //Session["stractual"] = stractual;
                            }

                            /** End New 18 Juli 2021 **/

                            /**add hasan 23 nov 2021**/
                            TrScore tscr;
                            if (LSOP.Text.Trim() == "Customer complaint")
                            {
                                tscr = GetScoreAutoPES2(ip.CategoryID, stractual);
                            }
                            else
                            {
                                tscr = GetScoreAutoPES(ip.CategoryID, stractual);
                            }
                            //end add hasan 23 nov 2021

                            //capai.Text = stractual;

                            //tambahan satuan (fajri)
                            if (tscr.satuan != null || tscr.satuan != "")
                            {
                                if (LSOP.Text.Trim() == "Pemantauan Budget Dept" || LSOP.Text.Trim() == "Pencapaian Budgeting Dept")
                                {
                                    capai.Text = stractual2;
                                }
                                else
                                {
                                    capai.Text = stractual + tscr.satuan;
                                }
                            }
                            else
                            {
                                capai.Text = stractual;
                            }

                            score.Text = tscr.pointnilai.ToString();
                            point2.Text = ((ip.BobotNilai * Convert.ToInt32(score.Text)) / 100).ToString("N0");
                            ddl.SelectedItem.Text = tscr.tragetke;

                            //if (txtNamaPES.Text == "KPI")
                            //    UpdateKPI(stractual, ip.ID, ddl.SelectedItem.Text, score.Text, tscr.ID);
                            //else
                            //    UpdateSOP(stractual, ip.ID, ddl.SelectedItem.Text, score.Text);

                            if (txtNamaPES.Text == "KPI")
                            {
                                if (LSOP.Text.Trim() == "Pemantauan Budget Dept" || LSOP.Text.Trim() == "Pencapaian Budgeting Dept")
                                {
                                    UpdateKPI(stractual2, ip.ID, ddl.SelectedItem.Text, score.Text, tscr.ID);
                                }
                                else
                                {
                                    #region link auto untuk semesteran
                                    string nilais = string.Empty;
                                    nilais = GetBulanSemesteran2(ip.ID);

                                    //jika penilain di iso_usercategory (6,12)
                                    if (int.Parse(nilais) == 12 && stractual != "")
                                    {
                                        UpdateKPI(stractual, ip.ID, ddl.SelectedItem.Text, score.Text, tscr.ID);
                                    }
                                    else if (int.Parse(nilais) == 6 && stractual != "")
                                    {
                                        UpdateKPI(stractual, ip.ID, ddl.SelectedItem.Text, score.Text, tscr.ID);
                                    }
                                    else
                                    {
                                        //jika penilaian di iso_usercategory (0,1)
                                        if (int.Parse(nilais) == 0)
                                        {
                                            UpdateKPI(stractual, ip.ID, ddl.SelectedItem.Text, score.Text, tscr.ID);
                                        }
                                        else if (int.Parse(nilais) == 1)
                                        {
                                            UpdateKPI(stractual, ip.ID, ddl.SelectedItem.Text, score.Text, tscr.ID);
                                        }
                                        
                                    }

                                    #endregion

                                    //UpdateKPI(stractual, ip.ID, ddl.SelectedItem.Text, score.Text, tscr.ID);
                                }
                            }
                            else
                            {
                                UpdateSOP(stractual, ip.ID, ddl.SelectedItem.Text, score.Text, tscr.ID);
                            }

                        }
                    }
                    //end WO link to PES
                }
                #endregion
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lblTotal = (Label)e.Item.FindControl("lblTotal");
                lblTotal.Text = scr.ToString("###,##0.00") + "%";
                //Label lblPoint = (Label)e.Item.FindControl("lblPoint");
                //lblPoint.Text = scr.ToString("###,##0.00") + "%";
                Label lblPoint = (Label)e.Item.FindControl("lblPoint");
                lblPoint.Text = total1.ToString();

            }
        }


        protected string GetBulanSemesteran(int ID)
        {
            string nilaisemesteran = string.Empty;
            //string thnbln = ddlTahun.SelectedValue.Trim() + ddlBulan.SelectedValue.Trim().PadLeft(2, '0');
            ZetroView zv = new ZetroView();
            zv.QueryType = Operation.CUSTOM;
            string strSQL = string.Empty;
            zv.CustomQuery = "select Penilaian from ISO_UserCategory where id=" + ID;
            SqlDataReader dr = zv.Retrieve();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    nilaisemesteran = dr["penilaian"].ToString();
                }
            }
            return nilaisemesteran;
        }

        protected string GetBulanSemesteran2(int ID)
        {
            string nilaisemesteran = string.Empty;
            //string thnbln = ddlTahun.SelectedValue.Trim() + ddlBulan.SelectedValue.Trim().PadLeft(2, '0');
            ZetroView zv = new ZetroView();
            zv.QueryType = Operation.CUSTOM;
            string strSQL = string.Empty;
            zv.CustomQuery = "select Penilaian from ISO_UserCategory where id in(select CategoryID from ISO_KPI where ID="+ID+")";
            SqlDataReader dr = zv.Retrieve();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    nilaisemesteran = dr["penilaian"].ToString();
                }
            }
            return nilaisemesteran;
        }
		
        protected string GetActualValueAotuPES(int ID)
        {
            string thnbln = ddlTahun.SelectedValue.Trim() + ddlBulan.SelectedValue.Trim().PadLeft(2, '0');

            string actual = string.Empty;
            ZetroView zv = new ZetroView();
            zv.QueryType = Operation.CUSTOM;
            string strSQL = string.Empty;
            zv.CustomQuery = "SELECT isnull(QueryAutoPES,'')QueryAutoPES from iso_usercategory where id=" + ID;
            SqlDataReader dr = zv.Retrieve();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    strSQL = dr["QueryAutoPES"].ToString().Trim();
                }
            }
            if (strSQL != string.Empty)
            {
                string strparameter = "declare @thnbln int set @thnbln='" + thnbln + " '";
                ZetroView zv1 = new ZetroView();
                zv1.QueryType = Operation.CUSTOM;
                zv1.CustomQuery = strparameter + strSQL;
                SqlDataReader dr1 = zv1.Retrieve();
                if (dr1.HasRows)
                {
                    while (dr1.Read())
                    {
                        actual = dr1["actual"].ToString().Trim();
                    }
                }
                actual = actual.Replace(",", ".");
                //if (actual == "0" )
                //    actual = "100";
                //if (actual == "")
                //    actual = "100";
            }

            return actual;
        }

        /** New 18 Juli 2021 **/
        protected string GetActualValueAotuPES2(int ID, string logic, string logic2)
        {
            string thnbln = ddlTahun.SelectedValue.Trim() + ddlBulan.SelectedValue.Trim().PadLeft(2, '0');
            string strparameter2 = "declare @thnbln int set @thnbln=" + thnbln + " ";

            string actual = string.Empty;
            ZetroView zv0 = new ZetroView();
            zv0.QueryType = Operation.CUSTOM;
            string strSQL = string.Empty;
            if (cekArmada(ID) == 0)
                zv0.CustomQuery = strparameter2 +
                    " select ActualValue+' ( '+Persen+'%'+' )'actual from ( " +
                    " select ltrim(left(CONVERT(VARCHAR,convert(money,Actual_Value),12),len(CONVERT(VARCHAR,convert(money,Actual_Value),12))-3)) as ActualValue,ltrim(left(CONVERT(VARCHAR,convert(money,Persen),12),len(CONVERT(VARCHAR,convert(money,Persen),12))-3)) as Persen " +
                    " from PES_Nilai where Periode=@thnbln and ItemSarmut in (select top 1 Sarmut from ISO_UserCategory where ID='" + ID + "') and RowStatus>-1 ) as x ";
            else
                zv0.CustomQuery = strparameter2 +
                " select ActualValue+' ( '+Persen+'%'+' )'actual from ( " +
                " select ltrim(left(CONVERT(VARCHAR,convert(money,actual),12),len(CONVERT(VARCHAR,convert(money,actual),12))-3)) as ActualValue, " +
                " ltrim(left(CONVERT(VARCHAR,convert(money,Persen),12),len(CONVERT(VARCHAR,convert(money,Persen),12))-1)) as Persen from ( " +
                " select cast((Actual/Budget)*100 as decimal(18,1))Persen,actual from ( " +
                " select cast(sum(Actual) as decimal(18,1))Actual,sum(Budget)Budget from " + logic2 + "MaterialBudgetArmada_Nilai_Temp where Periode=@thnbln " + logic + ") " +
                " as x  ) as xx ) as xxx ";
            SqlDataReader dr0 = zv0.Retrieve();
            if (dr0.HasRows)
            {
                while (dr0.Read())
                {
                    actual = dr0["actual"].ToString().Trim();
                }
            }
            return actual;
        }
        /** End New 18 Juli 2021 **/

        protected int cekArmada(int usercategoryID)
        {
            int actual = 0;
            ZetroView zv0 = new ZetroView();
            zv0.QueryType = Operation.CUSTOM;
            string strSQL = string.Empty;
            zv0.CustomQuery =
                "select id,case when id=(select DeptID from ISO_Category where id in (select CategoryID  " +
                "from ISO_UserCategory where id=" + usercategoryID + ")) then 1 else 0 end armada  " +
                "from dept where DeptName like '%armada%' and RowStatus>-1 ";
            SqlDataReader dr0 = zv0.Retrieve();
            if (dr0.HasRows)
            {
                while (dr0.Read())
                {
                    actual = Convert.ToInt32(dr0["armada"].ToString());
                }
            }
            return actual;
        }

        protected string GetTargetScoreAutoPES(int categoryid, string actual)
        {
            string target = string.Empty;
            ZetroView zv = new ZetroView();
            zv.QueryType = Operation.CUSTOM;
            string strSQL = string.Empty;
            zv.CustomQuery = "select targetke from ISO_SOPScore  where RowStatus>-1 and  CategoryID in (select Categoryid  " +
                "from ISO_usercategory where id=" + categoryid + " ) and " + actual + ">=parameter1 and " + actual + "<=parameter2";
            SqlDataReader dr = zv.Retrieve();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    target = dr["targetke"].ToString();
                }
            }

            return target;
        }
        protected TrScore GetScoreAutoPES(int categoryid, string actual)
        {
            //int score = 0;
            TrScore scr = new TrScore();
            ZetroView zv = new ZetroView();
            zv.QueryType = Operation.CUSTOM;
            string strSQL = string.Empty;
            zv.CustomQuery =
            "select * from ISO_SOPScore  where RowStatus>-1 and  CategoryID in (select Categoryid  " +
            "from ISO_usercategory where id=" + categoryid + " ) and " + actual + ">=parameter1 and  " + actual + "<=parameter2";
            SqlDataReader dr = zv.Retrieve();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    scr.ID = Convert.ToInt32(dr["ID"].ToString().Trim());
                    scr.pointnilai = Convert.ToInt32(dr["pointnilai"].ToString().Trim());
                    scr.tragetke = dr["targetke"].ToString().Trim();
                    scr.satuan = dr["satuan"].ToString().Trim();
                }
            }
            return scr;
        }

        /** add hasan 23 nov 2021 **/
        protected TrScore GetScoreAutoPES2(int categoryid, string actual)
        {
            string thnbln = ddlTahun.SelectedValue.Trim() + ddlBulan.SelectedValue.Trim().PadLeft(2, '0');

            //int score = 0;
            TrScore scr = new TrScore();
            ZetroView zv = new ZetroView();
            zv.QueryType = Operation.CUSTOM;
            string strSQL = string.Empty;
            zv.CustomQuery =
            " ;with sarmut as( " +
            " select  case when actual-[target]<=0 then '1' else '0' end actual from SPD_TransPrs  where SarmutPID in (select id from SPD_Perusahaan " +
            " where SarMutPerusahaan like '%Customer complaint') and (cast(tahun as varchar(4))+ REPLACE(STR(bulan, 2), SPACE(1),'0') )='" + thnbln + "') " +

            " select * from ISO_SOPScore a, sarmut b   where a.RowStatus>-1 and  a.CategoryID in " +
            " (select Categoryid  from ISO_usercategory where id='" + categoryid + "') and " +
            " b.actual>=parameter1 and b.actual <=parameter2";
            SqlDataReader dr = zv.Retrieve();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    scr.ID = Convert.ToInt32(dr["ID"].ToString().Trim());
                    scr.pointnilai = Convert.ToInt32(dr["pointnilai"].ToString().Trim());
                    scr.tragetke = dr["targetke"].ToString().Trim();
                    scr.satuan = dr["satuan"].ToString().Trim();
                }
            }
            return scr;
        }
        //end add hasan 23 nov 2021


        protected void UpdateKPI(string ket, int kpiid, string targetke, string point, int idscore)
        {
            ZetroView zv = new ZetroView();
            zv.QueryType = Operation.CUSTOM;
            string strSQL = string.Empty;
            zv.CustomQuery = "update iso_kpi set keterangan='" + ket + "' where id =" + kpiid +
                "update iso_kpidetail set kettargetke='" + targetke + "',pointnilai=" + point + ",sopscoreid=" + idscore + " where kpiid=" + kpiid;
            SqlDataReader dr = zv.Retrieve();
        }
        protected void UpdateSOP(string ket, int sopid, string targetke, string point, int idscore)
        {
            ZetroView zv = new ZetroView();
            zv.QueryType = Operation.CUSTOM;
            string strSQL = string.Empty;
            zv.CustomQuery = "update iso_SOP set keterangan='" + ket + "' where id =" + sopid +
                "update iso_SOPdetail set SopScoreID=" + idscore + ",kettargetke='" + targetke + "',pointnilai=" + point + " where SOPid=" + sopid;
            SqlDataReader dr = zv.Retrieve();
        }

        protected string GetBulanSemesteranSOP(int ID)
        {
            string nilaisemesteran = string.Empty;
            ZetroView zv = new ZetroView();
            zv.QueryType = Operation.CUSTOM;
            string strSQL = string.Empty;
            zv.CustomQuery = "select Penilaian from ISO_UserCategory where id in(select CategoryID from ISO_SOP where ID=" + ID + ")";
            SqlDataReader dr = zv.Retrieve();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    nilaisemesteran = dr["penilaian"].ToString();
                }
            }
            return nilaisemesteran;
        }
        protected void lstSOP_Command(object sender, RepeaterCommandEventArgs e)
        {
            string NamaPES = txtNamaPES.Text;
            string IDx = e.CommandArgument.ToString();
            int RowNum = e.Item.ItemIndex;
            TextBox txtPencapaian = (TextBox)lstSOP.Items[RowNum].FindControl("txtKeterangan");
            DropDownList ddlScore = (DropDownList)lstSOP.Items[RowNum].FindControl("ddlStatus");
            Label score = (Label)e.Item.FindControl("txtPointNilai");
            Label point2 = (Label)e.Item.FindControl("txtPoint");
            Image Simpan = (Image)lstSOP.Items[RowNum].FindControl("simpan");
            Image img = (Image)lstSOP.Items[RowNum].FindControl("edit");
            //CheckBox chkReBobot = (CheckBox)lstSOP.Items[RowNum].FindControl("chkRbb");
            switch (e.CommandName)
            {
                case "Edit":
                    txtPencapaian.Enabled = true;
                    txtPencapaian.ReadOnly = false;
                    ddlScore.Enabled = true;
                    Simpan.Visible = true;
                    img.Visible = false;
                    break;
                case "Save":
                    SOPNew sop = new SOPNew();
                    ISO_SOP isp = new ISO_SOP();
                    ArrayList arrSOPO = new ArrayList();
                    isp.ID = int.Parse(IDx.ToString());
                    isp.SopScoreID = int.Parse(ddlScore.SelectedValue.ToString());
                    isp.PointNilai = decimal.Parse(score.Text);
                    isp.Point2 = decimal.Parse(point2.Text);
                    isp.Keterangan = txtPencapaian.Text;
                    isp.LastModifiedBy = ((Users)Session["Users"]).UserID.ToString();
                    isp.LastModifiedTime = DateTime.Now.ToLocalTime();
                    isp.KetTargetKe = ddlScore.SelectedItem.Text;
                    isp.TargetKe = ddlScore.SelectedIndex + 1;

                    //penambahan agus 21-09-2022
                    string nilaiBulan2 = string.Empty;
                    string bulanIni2 = ddlBulan.SelectedValue.TrimStart(new Char[] { '0' });
                    //penambahan agus 21-09-2022

                    if (txtPES.Text == "3")
                    {
                        isp.SOPID = int.Parse(IDx.ToString());
                        nilaiBulan2 = GetBulanSemesteranSOP(isp.ID);
                    }
                    else
                    {
                        isp.KPIID = int.Parse(IDx.ToString());
                        nilaiBulan2 = GetBulanSemesteran2(isp.ID);
                    }

                    //penambahan agus 21-09-2022

                    #region cek penilaian SOP

                    if (int.Parse(nilaiBulan2) > 0)
                    {
                        if (Int32.Parse(bulanIni2) >= Int32.Parse(nilaiBulan2))
                        {
                            isp.Rebobot = 1;
                        }
                        else if (Int32.Parse(bulanIni2) <= Int32.Parse(nilaiBulan2))
                        {
                            isp.Rebobot = 1;

                        }
                        else if (nilaiBulan2 == bulanIni2 || nilaiBulan2 == bulanIni2)
                        {
                            isp.Rebobot = 1;
                        }
                        else
                        {
                            isp.Rebobot = 0;
                        }
                    }
                    else
                    {

                    }

                    #endregion

                    //if (  nilaiBulan2 == bulanIni2 || nilaiBulan2==bulanIni2)
                    //{
                    //    isp.Rebobot = 1;
                    //}
                    //else
                    //{
                    //    isp.Rebobot = 0;
                    //}

                    //penambahan agus 21-09-2022

                    //if (chkReBobot != null)
                    //{
                    //    isp.Rebobot = (chkReBobot.Checked == true) ? 1 : 0;
                    //}
                    //else
                    //{
                    //    isp.Rebobot = 0;
                    //}

                    sop.Pilihan = "Update";
                    sop.PES = NamaPES;
                    sop.Criteria = "ID,Keterangan,LastmodifiedBy,LastmodifiedTime";
                    sop.TableName = "ISO_" + NamaPES;
                    string rst = sop.CreateProcedure(isp, "spISO_" + NamaPES + "UpdateCapai");
                    if (rst == string.Empty)
                    {
                        int rest = sop.ProcessData(isp, "spISO_" + NamaPES + "UpdateCapai");
                        if (rest > 0)
                        {
                            SOPNew spn = new SOPNew();
                            spn.Pilihan = "UpdateWithHeaderID";
                            spn.PES = NamaPES;
                            spn.Criteria = NamaPES + "ID,SopScoreID,TargetKe,KetTargetKE,PointNilai,Rebobot";
                            spn.TableName = "ISO_" + NamaPES + "Detail";
                            spn.Key = "" + NamaPES + "ID";
                            string rs = spn.CreateProcedure(isp, "spISO_" + NamaPES + "Detail_UpdatePoint1");
                            if (rs == string.Empty)
                            {
                                int rss = spn.ProcessData(isp, "spISO_" + NamaPES + "Detail_UpdatePoint1");
                                if (rss > 0)
                                {
                                    LoadList();
                                }
                            }
                        }
                    }
                    break;
            }
        }
        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < lstSOP.Items.Count; i++)
            {
                DropDownList ddlStatus = (DropDownList)lstSOP.Items[i].FindControl("ddlStatus");
                Label txtPointNilai = (Label)lstSOP.Items[i].FindControl("txtPointNilai");
                if (ddlStatus.SelectedIndex > 0)
                {
                    ISO_SOPScoreFacade sopScoreFacade = new ISO_SOPScoreFacade();
                    ISO_SOPScore sopScore = sopScoreFacade.RetrieveByID(int.Parse(ddlStatus.SelectedValue));
                    if (sopScore.ID > 0)
                        txtPointNilai.Text = sopScore.PointNilai.ToString("N0");
                }
                else
                {
                    txtPointNilai.Text = "";
                }
            }
        }
        //protected void chkRbb_Checked(object sender, EventArgs e)
        //{
        //    CheckBox chk = (CheckBox)sender;
        //    int idx = int.Parse(chk.CssClass);
        //    ((TextBox)lstSOP.Items[idx].FindControl("txtKeterangan")).ReadOnly = (chk.Checked == true) ? false : true;
        //    ((DropDownList)lstSOP.Items[idx].FindControl("ddlStatus")).Enabled = (chk.Checked == true) ? true : false;
        //}
        protected void txtKeterangan_TextChanged(object sender, EventArgs e)
        {
            TextBox txts = (TextBox)sender;
            int n = int.Parse(txts.ToolTip);
            TextBox txtKeterangan = (TextBox)lstSOP.Items[n].FindControl("txtKeterangan");
            DropDownList ddlStatus = (DropDownList)lstSOP.Items[n].FindControl("ddlStatus");
            Label txtPointNilai = (Label)lstSOP.Items[n].FindControl("txtPointNilai");
            Label txtPoint = (Label)lstSOP.Items[n].FindControl("txtPoint");
            string aktual = txtKeterangan.Text.TrimStart().TrimEnd();
            ListItem lst = ddlStatus.Items.FindByText(aktual);
            ddlStatus.Enabled = (txts.Text.Length > 0) ? true : false;
            if (lst != null)
            {
                ddlStatus.ClearSelection();
                lst.Selected = true;
            }
            else
            {
                int i = 0;
                foreach (ListItem lsItem in ddlStatus.Items)
                {
                    if (i == 1)
                    {
                        Char[] itm = lsItem.Text.ToUpper().ToCharArray();
                        List<string> itmn = new List<string>();
                        foreach (Char c in itm)
                        {
                            itmn.Add(c.ToString());
                        }
                        itmn.ToArray();
                        if (itmn.Contains(aktual.ToString().ToUpper()))
                        {
                            if (itmn.Contains("<"))
                            {
                                int idx = itmn.IndexOf("<");
                                int txt = string.Compare(aktual.ToUpper(), itmn[idx + 1].ToUpper(), true);
                                if (txt < 0)
                                {
                                    ddlStatus.ClearSelection();
                                    lsItem.Selected = true;
                                    //return;
                                }
                            }
                            else if (itmn.Contains("<="))
                            {
                                int idx = itmn.IndexOf("<=");
                                int txt = string.Compare(aktual.ToUpper(), itmn[idx + 1].ToUpper(), true);
                                if (txt <= 0)
                                {
                                    ddlStatus.ClearSelection();
                                    lsItem.Selected = true;
                                    //return;
                                }
                            }
                            else
                            {
                                ddlStatus.ClearSelection();
                                lsItem.Selected = true;
                                //return;
                            }
                        }
                    }
                    else if (i > 1 && i < ddlStatus.Items.Count - 1)
                    {
                        string[] item = lsItem.Text.Replace("%", "").Split('-');
                        if (item.Count() > 1)
                        {
                            int cmp = string.Compare(txtKeterangan.Text.ToString().Replace("%", "").TrimStart().TrimEnd().ToUpper(), item[0].ToString().ToUpper().TrimStart().TrimEnd());
                            int ocm = string.Compare(txtKeterangan.Text.ToString().Replace("%", "").TrimStart().TrimEnd().ToUpper(), item[1].ToString().ToUpper().TrimStart().TrimEnd());
                            if (cmp >= 0 && ocm <= 0)
                            {
                                ddlStatus.ClearSelection();
                                lsItem.Selected = true;
                                //return;
                            }
                        }
                    }
                    i++;
                }
            }
            if (ddlStatus.SelectedIndex > 0)
            {
                ISO_SOPScoreFacade sopScoreFacade = new ISO_SOPScoreFacade();
                ISO_SOPScore sopScore = sopScoreFacade.RetrieveByID(int.Parse(ddlStatus.SelectedValue));
                if (sopScore.ID > 0)
                    txtPointNilai.Text = sopScore.PointNilai.ToString("N0");
            }
        }
        private ArrayList HasBeenInput()
        {
            string NamaPES = txtNamaPES.Text;
            ArrayList arrData = new ArrayList();
            SOPNew sp = new SOPNew();
            SOPNew st = new SOPNew();
            ISO_SOP ip = new ISO_SOP();
            st.PES = NamaPES;
            sp.Pilihan = "Header";
            sp.PES = txtNamaPES.Text;
            sp.Criteria = " and sop.DeptID=" + ddlDept.SelectedValue.ToString();
            sp.Criteria += " and ISO_UserID=" + ddlPIC.SelectedValue.ToString();
            sp.Criteria += " and sop.BagianID=" + ddlBagian.SelectedValue.ToString();
            st.Criteria = " and " + NamaPES + "ID =" + ip.ID.ToString();
            sp.Criteria += " and Month(TglMulai)=" + ddlBulan.SelectedValue.ToString();
            sp.Criteria += " and Year(TglMulai)=" + ddlTahun.SelectedValue.ToString();
            //sp.Criteria += " Order by ID ";
            //sp.Criteria2 = "" + txtNamaPES.Text + "ID";
            arrData = sp.Retrieve();
            txtProses.Text = (arrData.Count > 0) ? "Update" : "Insert";
            btnSimpan.Visible = (arrData.Count > 0) ? false : true;
            return arrData;
        }
        private int Penilaian(string CategoryID)
        {
            int nilai = 0;
            SOPNew sp = new SOPNew();
            sp.Pilihan = "CAT";
            sp.Criteria = " AND iu.ID=" + CategoryID;
            foreach (ISO_SOP isp in sp.Retrieve())
            {
                nilai = isp.Penilaian;
            }
            return nilai;
        }
        private int RebobotHasInputed(string CategoryID, string Penilaian)
        {
            int result = 0;
            SOPNew sp = new SOPNew();
            sp.Pilihan = "CekRebobot";
            sp.Criteria = " and iu.ID=" + CategoryID;
            switch (Penilaian)
            {
                case "12":
                    sp.Criteria += " and Year(sop.TglMulai)=" + ddlTahun.SelectedValue;
                    break;
                case "6":
                    switch (ddlBulan.SelectedValue)
                    {
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                            sp.Criteria += "and Month(sop.TglMulai) between '1' and '6'";
                            break;
                        case "7":
                        case "8":
                        case "9":
                        case "10":
                        case "11":
                        case "12":
                            sp.Criteria += "and Month(sop.TglMulai) between '7' and '12'";
                            break;
                    }
                    sp.Criteria += " and Year(sop.TglMulai)=" + ddlTahun.SelectedValue;
                    break;
            }
            return result;
        }
        private ArrayList NilaiSebelumNya(string CategoryID, string Penilaian, string PESe)
        {
            ArrayList arrData = new ArrayList();
            SOPNew sp = new SOPNew();
            sp.PES = PESe;
            sp.Pilihan = "RebobotNilai";
            sp.Criteria = " and iu.ID=" + CategoryID;
            //sp.Criteria +=" and sd.RowStatus>-1";
            if (Penilaian == "12")
            {
                if (int.Parse(ddlBulan.SelectedValue) > 1)
                {
                    //sp.Criteria += "and Month(sop.TglMulai)=" + (int.Parse(ddlBulan.SelectedValue) - 1);
                    sp.Criteria += " and sd.Rebobot=1";
                    sp.Criteria += " and Year(sop.TglMulai)=" + ddlTahun.SelectedValue;
                }
                else
                {
                    //sp.Criteria += "and Month(sop.TglMulai)=" + (int.Parse(ddlBulan.SelectedValue));
                    sp.Criteria += " and sd.Rebobot=1";
                    sp.Criteria += " and Year(sop.TglMulai)=" + ddlTahun.SelectedValue;
                }
            }
            else if (Penilaian == "6")
            {
                //switch (int.Parse(ddlBulan.SelectedValue))
                //{
                //    case 1:
                //        break;
                //    case 7:
                //        //sp.Criteria += " and sd.Rebobot=1 and Month(sop.TglMulai)=" + (int.Parse(ddlBulan.SelectedValue));
                //        //sp.Criteria += " and Year(sop.TglMulai)=" + ddlTahun.SelectedValue;
                //        break;
                //    default:
                //sp.Criteria += "and Month(sop.TglMulai)=" + (int.Parse(ddlBulan.SelectedValue) - 1);
                if (int.Parse(ddlBulan.SelectedValue) <= 6 && int.Parse(ddlBulan.SelectedValue) >= 1)
                {
                    sp.Criteria += " and Month(sop.TglMulai)>=1 and Month(sop.TglMulai)<=6 ";
                }
                else
                {
                    sp.Criteria += " and Month(sop.TglMulai)>=7 and Month(sop.TglMulai)<=12 ";
                }
                sp.Criteria += " and sd.Rebobot=1";
                sp.Criteria += " and Year(sop.TglMulai)=" + ddlTahun.SelectedValue;
                //        break;
                //}
            }
            arrData = sp.Retrieve();
            return arrData;
        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string strError = "";
            ArrayList arrData = new ArrayList();
            for (int n = 0; n < lstSOP.Items.Count; n++)
            {
                ISO_SOP isp = new ISO_SOP();
                TextBox txtKeterangan = (TextBox)lstSOP.Items[n].FindControl("txtKeterangan");
                TextBox txtCatID = (TextBox)lstSOP.Items[n].FindControl("txtCatID");//ISO_Category.ID
                TextBox txtBobot = (TextBox)lstSOP.Items[n].FindControl("txtBobot");
                //TextBox txtPoint = (TextBox)lstSOP.Items[n].FindControl("txtPoint");
                TextBox txtItemName = (TextBox)lstSOP.Items[n].FindControl("txtItemName");
                TextBox txtID = (TextBox)lstSOP.Items[n].FindControl("txtID");//ISO_UserCategory.ID
                DropDownList ddlStatus = (DropDownList)lstSOP.Items[n].FindControl("ddlStatus");
                Label txtPointNilai = (Label)lstSOP.Items[n].FindControl("txtPointNilai");
                //CheckBox chkReBobot = (CheckBox)lstSOP.Items[n].FindControl("chkRbb");
                string aktual = txtKeterangan.Text.TrimStart().TrimEnd();
                if (txtPES.Text == "3")
                {
                    isp.NewSop = txtItemName.Text;
                }
                else
                {
                    isp.NewKpi = txtItemName.Text;
                }
                isp.PesType = int.Parse(txtPES.Text);
                isp.DeptID = int.Parse(ddlDept.SelectedValue.ToString());
                int pjgDept = ddlDept.SelectedItem.ToString().Length;
                isp.DeptName = (pjgDept >= 4) ? ddlDept.SelectedItem.ToString().Substring(0, 3) : ddlDept.SelectedItem.ToString().Substring(0, pjgDept);
                isp.BagianID = int.Parse(ddlBagian.SelectedValue.ToString());
                isp.TglMulai = DateTime.ParseExact(txtTglMulai.Text.Replace("-", "/"), "dd/MM/yyyy", null);
                isp.CategoryID = int.Parse(txtID.Text.ToString());
                isp.BobotNilai = decimal.Parse(txtBobot.Text) / 100;
                //isp.Point2 = decimal.Parse(txtPoint.Text);
                isp.Ket = txtKeterangan.Text;
                isp.Iso_UserID = int.Parse(ddlPIC.SelectedValue.ToString());
                isp.Pic = txtPICName.Text;
                //detail process
                isp.CreatedBy = (((Users)Session["Users"]).UserName);
                UsersFacade usersFacade = new UsersFacade();
                Users users2 = usersFacade.RetrieveByUserName(((Users)Session["Users"]).UserName);
                DeptFacade deptFacade3 = new DeptFacade();
                Dept dept2 = deptFacade3.RetrieveDeptByUserGroup(users2.ID);
                isp.UserID = users2.ID;
                isp.UserGroupID = dept2.UserGroupID;
                isp.TargetKe = ddlStatus.SelectedIndex;
                isp.KetTargetKe = ddlStatus.SelectedItem.ToString();
                isp.SopScoreID = int.Parse(ddlStatus.SelectedValue);
                isp.PointNilai = (txtPointNilai.Text == string.Empty) ? 0 : decimal.Parse(txtPointNilai.Text);

                //penambahan pengaturan rebobot, jika check box is checked

                //penambahan agus 22-09-2022
                string bulanSekarang = ddlBulan.SelectedValue.TrimStart(new Char[] { '0' });
                string nilaiBulan = GetBulanSemesteran(isp.CategoryID);

                if (int.Parse(nilaiBulan) > 0)
                {
                    if (Int32.Parse(bulanSekarang) >= Int32.Parse(nilaiBulan))
                    {
                        isp.Rebobot = 1;
                    }
                    else if (Int32.Parse(bulanSekarang) <= Int32.Parse(nilaiBulan))
                    {
                        isp.Rebobot = 1;

                    }
                    else if (bulanSekarang == nilaiBulan || bulanSekarang == nilaiBulan)
                    {
                        isp.Rebobot = 1;
                    }
                    else
                    {
                        isp.Rebobot = 0;
                    }
                }
                else
                {

                }

                //penambahan agus 22-09-2022

                //if (chkReBobot != null)
                //{
                //    isp.Rebobot = (chkReBobot.Checked == true) ? 1 : 0;
                //}

                isp.App = 0;
                arrData.Add(isp);
                strError = SimpanSOP(isp);

            }
            if (strError == string.Empty)
            {
                LoadList();
                btnSimpan.Enabled = false;
            }
        }
        private string SimpanSOP(ISO_SOP sop)
        {
            string strEvent = "Insert";
            ISO_DocumentNoFacade docNoFacade = new ISO_DocumentNoFacade();
            ISO_DocumentNo docNo = docNoFacade.RetrieveByDept(3, Convert.ToInt32(ddlDept.SelectedValue), DateTime.ParseExact(txtTglMulai.Text.Replace("-", "/"), "dd/MM/yyyy", null).Year);
            if (docNo.ID > 0)
            {
                docNo.DocNo = docNo.DocNo + 1;
                docNo.PesType = int.Parse(txtPES.Text); //sop
                docNo.DeptID = Convert.ToInt32(ddlDept.SelectedValue);
                docNo.Tahun = DateTime.ParseExact(txtTglMulai.Text.Replace("-", "/"), "dd/MM/yyyy", null).Year;
                Company cp = new CompanyFacade().RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                docNo.Plant = cp.KodeLokasi;
            }
            else
            {
                docNo.DocNo = 1;
                docNo.PesType = int.Parse(txtPES.Text); //sop
                docNo.DeptID = Convert.ToInt32(ddlDept.SelectedValue);
                docNo.Tahun = DateTime.ParseExact(txtTglMulai.Text.Replace("-", "/"), "dd/MM/yyyy", null).Year;
                //HO ikut C dulu
                Company cp = new CompanyFacade().RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                docNo.Plant = cp.KodeLokasi;
            }


            ISO_SOPProcessFacade sopProcessFacade = new ISO_SOPProcessFacade(sop, docNo);
            string strError = string.Empty;
            strError = sopProcessFacade.Insert();
            if (strError == string.Empty)
            {
                InsertLog(strEvent);
                txtTaskNo.Text = "Doc No. : " + sopProcessFacade.sopNonya;
            }
            return strError;
        }
        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Input " + txtNamaPES.Text;
            eventLog.EventName = eventName;
            eventLog.DocumentNo = txtTaskNo.Text;
            eventLog.CreatedBy = ((Users)Session["Users"]).UserName;

            EventLogFacade eventLogFacade = new EventLogFacade();
            int intResult = eventLogFacade.Insert(eventLog);
        }
        protected void lstH_DataBound(object sender, RepeaterItemEventArgs e)
        {
            PES pes = new PES();
            ArrayList arrData = new ArrayList();
            ArrayList arrTot = new ArrayList();
            Repeater lst = (Repeater)e.Item.FindControl("lstRkp");
            Repeater upLst = (Repeater)e.Item.FindControl("lstTot");
            PES p = (PES)e.Item.DataItem;
            PesFacade ps = new PesFacade();
            //ps.Criteria = " where xx.UserID=" + p.ISOUserID.ToString();
            ps.UserID = p.ISOUserID.ToString();
            ps.Bulan = ddlBulan.SelectedValue.ToString();
            ps.Tahun = ddlTahun.SelectedValue.ToString();
            ps.Bagian = p.BagianID.ToString();
            ps.Where = "ItemSOP";
            if (Request.QueryString["tp"].ToString() == "3")
            {
                ps.Field = (ps.isHasbeenInput("ISO_SOP") > 0) ? "ItemSOPNew" : "ItemSOP";
            }
            else
            {
                ps.Field = (ps.isHasbeenInput("ISO_KPI") > 0) ? "ItemKPINew" : "ItemKPI";
            }
            arrData = ps.Retrieve();
            foreach (PES pp in arrData)
            {
                pes.TotalBobot += pp.BobotNilai;
                pes.TotalNilai += pp.Nilai;
            }
            arrTot.Add(pes);
            upLst.DataSource = arrTot;
            upLst.DataBind();
            lst.DataSource = arrData;
            lst.DataBind();


        }
        protected void lstPES_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lbl = (Label)e.Item.FindControl("sc");
            PES p = (PES)e.Item.DataItem;
            lbl.Text = (p.Pencapaian == "N/A") ? "N/A" : p.Score.ToString("##0");

        }
        private void ListPIC()
        {
            ArrayList arrPIC = new ArrayList();
            PesFacade p = new PesFacade();
            Auth DeptAuth = new Auth();
            UserAuth oto = new UserAuth();
            oto.Option = "All";
            oto.Criteria = " and UserID=" + ((Users)Session["Users"]).ID.ToString();
            oto.Criteria += " and ModulName='Rekap " + Judul + "' ";
            DeptAuth = oto.Retrieve(true);
            string UserDept = ((Users)Session["Users"]).DeptID.ToString();
            string arrDepts = (DeptAuth.AuthDept != null) ? DeptAuth.AuthDept : UserDept.ToString();
            p.Criteria = (ddlDept.SelectedIndex > 0) ? " and DeptID=" + ddlDept.SelectedValue.ToString() : " and DeptID in(" + arrDepts + ")";
            p.Criteria += (ddlPIC.SelectedIndex > 0) ? " and userid='" + ddlPIC.SelectedValue + "'" : "";
            //p.Criteria += (ddlPIC.SelectedIndex > 0) ? "" : "or UserID in(select ISOuserID from ISO_BagianHead where DeptApp  like '%" + ddlDept.SelectedValue + "%')";
            p.Field = "PICSop";
            p.Where = "PICSop";
            arrPIC = p.Retrieve();
            lstH.DataSource = arrPIC;
            lstH.DataBind();
        }
    }
}

public class SOPNew
{
    public string Criteria { get; set; }
    public string Criteria2 { get; set; }
    public string Pilihan { get; set; }
    public string Field { get; set; }
    public string Key { get; set; }
    public string PES { get; set; }
    private ArrayList arrData = new ArrayList();
    private ISO_SOP oSOP = new ISO_SOP();

    private string Query()
    {
        string query = string.Empty;
        switch (this.Pilihan)
        {
            case "CAT":
                query = "select iu.ID,(iu.Bobot*100)Bobot,Description,KodeUrutan,iu.CategoryID,ic.Target,ISNULL(iu.Penilaian,0)Penilaian " +
                        "from ISO_UserCategory as iu " +
                        "left join ISO_Category as ic " +
                        "on ic.ID=iu.CategoryID " +
                        "where ic.Description != '' and iu.RowStatus>-1 " + this.Criteria;
                break;
            case "Score":
                query = "select * from ISO_SOPScore where RowStatus>-1 and PesType>0 " + this.Criteria;
                break;
            case "Header":
                query = "select *,Case When PointNilai!=0 then (PointNilai*NilaiBobot) else 0 end Nilai from (select sop.ID,sop." + this.PES + "Name," +
                        "sop." + this.PES + "No,sop.DeptID,sop.BagianID,sop.TglMulai,sop.CategoryID,NilaiBobot,Keterangan, sop.ApprovedDate, sop.CreatedTime, sop.Status as StatusH, sop.PIC, sop.RowStatus,iuc.Penilaian, sod.KetTargetKe,ic.ID as CatID," +
                        "ic.Target,sod.PointNilai,sod.SopScoreID,sod.Status, sod.TargetKe,iuc.UserID,iuc.Bobot,iuc.PesType,sod.Approval,sod.Rebobot FROM " +
                        "ISO_" + this.PES + " as sop LEFT JOIN ISO_" + this.PES + "Detail as sod ON sod." + this.PES + "ID=sop.ID LEFT JOIN ISO_UserCategory as iuc ON iuc.ID=sop.CategoryID " +
                        "LEFT JOIN ISO_Category as ic on ic.ID=iuc.CategoryID where sod.RowStatus>-1 and sop.RowStatus>-1 " + this.Criteria +
                        ") as x";
                break;
            case "Header2":
                query = "select *,Case When PointNilai!=0 then (PointNilai*NilaiBobot) else 0 end Nilai from (select sop.ID,sop." + this.PES + "Name," +
                        "sop.CategoryID,NilaiBobot,Keterangan, sop.ApprovedDate, sop.CreatedTime, sop.Status as StatusH,  sod.KetTargetKe,ic.ID as CatID," +
                        "ic.Target,sod.PointNilai,sod.SopScoreID,sod.Status, sod.TargetKe,iuc.UserID,iuc.Bobot,iuc.PesType,sod.Approval FROM " +
                        "ISO_" + this.PES + " as sop LEFT JOIN ISO_" + this.PES + "Detail as sod ON sod." + this.PES + "ID=sop.ID LEFT JOIN ISO_UserCategory as iuc ON iuc.ID=sop.CategoryID " +
                        "LEFT JOIN ISO_Category as ic on ic.ID=iuc.CategoryID where sod.RowStatus>-1 and sop.RowStatus>-1 " + this.Criteria +
                        ") as x";
                break;
            case "Detail":
                query = "select * from ISO_" + this.PES + "Detail where RowStatus>-1" + this.Criteria;
                break;
            case "RebobotNilai":
                query = "SELECT sd.*,sop.keterangan, iu.Penilaian FROM ISO_" + this.PES + " sop " +
                      "LEFT JOIN ISO_" + this.PES + "Detail sd ON sd." + this.PES + "ID = sop.ID " +
                      "LEFT JOIN ISO_UserCategory AS iu ON iu.ID = sop.CategoryID " +
                      "WHERE sd.RowStatus>-1 " + Criteria;
                break;
        }

        return query;
    }
    public ArrayList Retrieve()
    {
        arrData = new ArrayList();
        string strSQL = this.Query();
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(this.Query());
        string strquery = this.Query();
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrData.Add(gObject(sdr));
            }
        }
        return arrData;
    }
    public ISO_SOP Retrieve(bool detail)
    {
        oSOP = new ISO_SOP();
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(this.Query());
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                return (gObject(sdr));
            }
        }
        return oSOP;
    }
    private ISO_SOP gObject(SqlDataReader sdr)
    {
        oSOP = new ISO_SOP();
        switch (this.Pilihan)
        {
            case "CAT":
                oSOP.CategoryID = Convert.ToInt32(sdr["ID"].ToString());
                oSOP.Description = sdr["Description"].ToString();
                oSOP.BobotNilai = Convert.ToDecimal(sdr["Bobot"].ToString());
                oSOP.ID = Convert.ToInt32(sdr["CategoryID"].ToString());
                oSOP.Target = sdr["Target"].ToString();
                oSOP.Penilaian = int.Parse(sdr["Penilaian"].ToString());
                break;
            case "Score":
                oSOP.ID = Convert.ToInt32(sdr["ID"].ToString());
                oSOP.Targete = sdr["TargetKe"].ToString();
                oSOP.PointNilai = Convert.ToDecimal(sdr["PointNilai"].ToString());
                break;
            case "Header":
                oSOP.ID = Convert.ToInt32(sdr["ID"].ToString());
                oSOP.SopNo = sdr[this.PES + "No"].ToString();
                oSOP.Description = sdr[this.PES + "Name"].ToString();
                oSOP.DeptID = Convert.ToInt32(sdr["DeptID"].ToString());
                oSOP.BagianID = Convert.ToInt32(sdr["BagianID"].ToString());
                oSOP.TglMulai = Convert.ToDateTime(sdr["TglMulai"].ToString());
                oSOP.CategoryID = Convert.ToInt32(sdr["CategoryID"].ToString());
                oSOP.BobotNilai = Convert.ToDecimal(sdr["NilaiBobot"].ToString()) * 100;
                oSOP.Point = Convert.ToDecimal(sdr["Nilai"].ToString());
                oSOP.Pencapaian = sdr["Keterangan"].ToString();
                oSOP.Status = Convert.ToInt32(sdr["StatusH"].ToString());
                oSOP.Pic = sdr["PIC"].ToString();
                oSOP.RowStatus = Convert.ToInt32(sdr["RowStatus"].ToString());
                oSOP.Target = sdr["Target"].ToString();
                oSOP.Penilaian = int.Parse(sdr["Penilaian"].ToString());
                break;
            case "Header2":
                oSOP.Point = Convert.ToDecimal(sdr["Nilai"].ToString());
                oSOP.TglInput = DateTime.Parse(sdr["CreatedTime"].ToString());
                //oSOP.TglApproved = DateTime.Parse(sdr["ApprovedDate"].ToString());
                if (string.IsNullOrEmpty(sdr["ApprovedDate"].ToString()))
                {
                    oSOP.TglApproved = DateTime.MinValue;
                }
                else
                {
                    oSOP.TglApproved = Convert.ToDateTime(sdr["ApprovedDate"]);
                }
                break;
            case "Detail":
                oSOP.IdDetail = int.Parse(sdr["ID"].ToString());
                oSOP.TargetKe = int.Parse(sdr["TargetKe"].ToString());
                oSOP.SopScoreID = int.Parse(sdr["SopScoreID"].ToString());
                oSOP.PointNilai = decimal.Parse(sdr["PointNilai"].ToString());
                oSOP.App = int.Parse(sdr["Approval"].ToString());
                oSOP.Status = int.Parse(sdr["Status"].ToString());
                oSOP.Approval = int.Parse(sdr["Approval"].ToString());
                oSOP.Rebobot = (sdr["Rebobot"] != DBNull.Value) ? Int32.Parse(sdr["Rebobot"].ToString()) : 0;
                break;
            case "RebobotNilai":
                oSOP.IdDetail = int.Parse(sdr["ID"].ToString());
                oSOP.TargetKe = int.Parse(sdr["TargetKe"].ToString());
                oSOP.SopScoreID = int.Parse(sdr["SopScoreID"].ToString());
                oSOP.PointNilai = decimal.Parse(sdr["PointNilai"].ToString());
                oSOP.App = int.Parse(sdr["Approval"].ToString());
                oSOP.Status = int.Parse(sdr["Status"].ToString());
                oSOP.Penilaian = (sdr["Penilaian"] != DBNull.Value) ? int.Parse(sdr["Penilaian"].ToString()) : 0;
                oSOP.Rebobot = (sdr["Rebobot"] != DBNull.Value) ? Int32.Parse(sdr["Rebobot"].ToString()) : 0;
                oSOP.Pencapaian = sdr["Keterangan"].ToString();
                break;
        }
        return oSOP;
    }

    //Proces data with sp
    private ISO_SOP hlp = new ISO_SOP();
    private List<SqlParameter> sqlListParam;
    public string TableName { get; set; }
    public string Where { get; set; }
    public int ProcessData(object help, string spName)
    {
        try
        {
            hlp = (ISO_SOP)help;
            string[] arrCriteria = this.Criteria.Split(',');
            PropertyInfo[] data = hlp.GetType().GetProperties();
            DataAccess da = new DataAccess(Global.ConnectionString());
            var equ = new List<string>();
            sqlListParam = new List<SqlParameter>();
            foreach (PropertyInfo items in data)
            {
                if (items.GetValue(hlp, null) != null && arrCriteria.Contains(items.Name))
                {
                    sqlListParam.Add(new SqlParameter("@" + items.Name.ToString(), items.GetValue(hlp, null)));
                }
            }
            int result = da.ProcessData(sqlListParam, spName);
            string err = da.Error;
            return result;
        }
        catch (Exception ex)
        {
            string er = ex.Message;
            return -1;
        }
    }
    public string CreateProcedure(object help, string spName)
    {
        string message = string.Empty;
        hlp = (ISO_SOP)help;
        string[] arrCriteria = this.Criteria.Split(',');
        PropertyInfo[] data = hlp.GetType().GetProperties();
        DataAccess da = new DataAccess(Global.ConnectionString());
        string param = "";
        string value = "";
        string field = "";
        string FieldUpdate = "";
        try
        {
            foreach (PropertyInfo items in data)
            {
                if (arrCriteria.Contains(items.Name))
                {
                    param += "@" + items.Name.ToString() + " " + GetTypeData(this.TableName, items.Name.ToString()) + ",";
                    value += "@" + items.Name.ToString() + ",";
                    field += items.Name.ToString() + ",";
                    if (items.Name.ToString() != "ID")
                        FieldUpdate += items.Name.ToString() + "=@" + items.Name.ToString() + ",";
                }
            }
            string strSQL = "CREATE PROCEDURE " + spName + " " + param.Substring(0, param.Length - 1) +
                            " AS BEGIN SET NOCOUNT ON; ";
            if (this.Pilihan == "Insert")
            {
                strSQL += "INSERT INTO " + this.TableName + " (" + field.Substring(0, field.Length - 1).ToString() + ")VALUES(" +
                 value.Substring(0, value.Length - 1) + ") SELECT @@IDENTITY as ID";
            }
            else if (this.Pilihan == "Update")
            {
                strSQL += "UPDATE " + this.TableName + " set " + FieldUpdate.Substring(0, FieldUpdate.Length - 1).ToString() +
                        " where ID=@ID SELECT @@ROWCOUNT";
            }
            else if (this.Pilihan == "UpdateWithHeaderID")
            {
                FieldUpdate.Replace(this.Key + "=@" + this.Key + ",", "");
                strSQL += "UPDATE " + this.TableName + " set " + FieldUpdate.Substring(0, FieldUpdate.Length - 1).ToString() +
                        " where " + this.Key + "=@" + this.Key + " SELECT @@ROWCOUNT";
            }
            strSQL += " END";
            SqlDataReader result = da.RetrieveDataByString(strSQL);
            if (result != null)
            {
                message = string.Empty;
            }
            else
            {
                message = "";
            }
            return message;
        }
        catch (Exception ex)
        {
            message = ex.Message;
            return message;
        }
    }
    private string GetTypeData(string TableName, string ColumName)
    {
        string result = string.Empty;
        string strsql = "select DATA_TYPE,CHARACTER_MAXIMUM_LENGTH,NUMERIC_PRECISION,NUMERIC_SCALE from INFORMATION_SCHEMA.COLUMNS IC where " +
                        "TABLE_NAME = '" + TableName + "' and COLUMN_NAME = '" + ColumName + "'";
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(strsql);
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                result = sdr["DATA_TYPE"].ToString() + " ";
                result += (sdr["CHARACTER_MAXIMUM_LENGTH"] != DBNull.Value) ? "(" + sdr["CHARACTER_MAXIMUM_LENGTH"].ToString() + ")" : "";
                result += (sdr["DATA_TYPE"].ToString() == "decimal") ? "(" + sdr["NUMERIC_PRECISION"].ToString() + "," + sdr["NUMERIC_SCALE"] + ")" : "";
                if (sdr["CHARACTER_MAXIMUM_LENGTH"].ToString() == "-1")
                {
                    result = result.Replace("-1", "Max");
                }
            }
        }
        return result;
    }
}
////////////////////////
public class TrScore : GRCBaseDomain
{
    public string satuan { get; set; }
    public string tragetke { get; set; }
    public decimal pointnilai { get; set; }
}
public class PES : GRCBaseDomain
{
    private DateTime tglinput = DateTime.Now.Date;
    private DateTime tglapproved = DateTime.Now.Date;
   // public int ID { get; set; }
    public string Desk { get; set; }
    public string TypeBobot { get; set; }
    public int TypePes { get; set; }
    public int ISOUserID { get; set; }
    public string Target { get; set; }
    public string SOPNo { get; set; }
    public string SOPName { get; set; }
    public decimal BobotNilai { get; set; }
    public string PIC { get; set; }
    public string Tahun { get; set; }
    public int Approval { get; set; }
    public decimal Jan { get; set; }
    public decimal JanS { get; set; }
    public decimal Feb { get; set; }
    public decimal FebS { get; set; }
    public decimal Mar { get; set; }
    public decimal MarS { get; set; }
    public decimal Apr { get; set; }
    public decimal AprS { get; set; }
    public decimal Mei { get; set; }
    public decimal MeiS { get; set; }
    public decimal Jun { get; set; }
    public decimal JunS { get; set; }
    public decimal Jul { get; set; }
    public decimal JulS { get; set; }
    public decimal Ags { get; set; }
    public decimal AgsS { get; set; }
    public decimal Sep { get; set; }
    public decimal SepS { get; set; }
    public decimal Okt { get; set; }
    public decimal OktS { get; set; }
    public decimal Nop { get; set; }
    public decimal NopS { get; set; }
    public decimal Des { get; set; }
    public decimal DesS { get; set; }
    public decimal Score { get; set; }
    public decimal TotalBobot { get; set; }
    public decimal TotalNilai { get; set; }
    public string Pencapaian { get; set; }
    public decimal Nilai { get; set; }
    public int DeptID { get; set; }
    public int BagianID { get; set; }
    public string DeptName { get; set; }
    public string BagianName { get; set; }
    public string UserID { get; set; }
    public string UserName { get; set; }
    public DateTime TglInput
    {
        get { return tglinput; }
        set { tglinput = value; }
    }
    public DateTime TglApproved
    {
        get { return tglapproved; }
        set { tglapproved = value; }
    }
    public string StatusApv { get; set; }
}
public class PesFacade
{
    private ArrayList arrData = new ArrayList();
    public string UserID { get; set; }
    public string Bulan { get; set; }
    public string Tahun { get; set; }
    public string Field { get; set; }
    public string Where { get; set; }
    public string Bagian { get; set; }
    public string Criteria { get; set; }
    public string Query()
    {
        string query = ""; string query1 = "";
        string Periode = (this.Tahun != null && this.Bulan != null) ? this.Tahun + this.Bulan.PadLeft(2, '0') + DateTime.DaysInMonth(int.Parse(this.Tahun), int.Parse(this.Bulan)) : string.Empty;
        switch (this.Field)
        {
            case "SOP":
                #region query SOP
                query = "/*select Tahun,SopName,NilaiBobot,PIC,Approval, " +
                           "SUM(Jan)Jan,SUM(janS)JanS,(SUM(jan)*SUM(janS)/100)JanN, " +
                           " SUM(Feb)Feb,SUM(febS)FebS,(SUM(feb)*SUM(febS)/100)FebN, " +
                           " SUM(Mar)Mar,SUM(marS)MarS,(SUM(Mar)*SUM(marS)/100)MarN, " +
                           " SUM(Apr)Apr,SUM(aprS)AprS,(SUM(Apr)*SUM(aprS)/100)AprN from ( */" +
                           "select Tahun,SOPNo,SOPName,(NilaiBobot*100)NilaiBobot,CategoryID,PIC,Approval, " +
                           " ISNULL([1],0)Jan,ISNULL([2],0)Feb,ISNULL([3],0)Mar,ISNULL([4],0)Apr,ISNULL([5],0)Mei, " +
                           " ISNULL([6],0)Jun,ISNULL([7],0)Jul,ISNULL([8],0)Ags,ISNULL([9],0)Sep,ISNULL([10],0)Okt, " +
                           " ISNULL([11],0)Nop,ISNULL([12],0)Desm, " +
                           " (NilaiBobot*ISNULL([1],0)) janS,(NilaiBobot*ISNULL([2],0)) febS,(NilaiBobot*ISNULL([3],0)) marS, " +
                           " (NilaiBobot*ISNULL([4],0)) aprS,(NilaiBobot*ISNULL([5],0)) meiS,(NilaiBobot*ISNULL([6],0)) junS, " +
                           " (NilaiBobot*ISNULL([7],0)) julS,(NilaiBobot*ISNULL([8],0)) agsS,(NilaiBobot*ISNULL([9],0)) sepS, " +
                           " (NilaiBobot*ISNULL([10],0)) oktS,(NilaiBobot*ISNULL([11],0)) nopS,(NilaiBobot*ISNULL([12],0)) desS  " +
                           " from ( " +
                           " select sp.ID,SOPNo,SOPName,NilaiBobot,CategoryID,PIC,sd.Approval, CAST(MONTH(TglMulai) as varchar) as Bulan," +
                           " YEAR(tglMulai)Tahun,sd.PointNilai " +
                           " from ISO_SOP as sp " +
                           " left Join ISO_SOPDetail as sd " +
                           " on sd.SOPID=sp.ID " +
                           " where sp.RowStatus>-1 and sd.RowStatus>-1 " + this.Criteria +
                           " Group by Month(tglMulai),YEAR(tglMulai),sp.ID,SOPNo,SOPName,NilaiBobot,CategoryID,PIC,sd.Approval,sd.PointNilai) " +
                           " up pivot (SUM(PointNilai) for Bulan in([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12])) as pv1 " +
                           " order by RIGHT(SOPNo,4),Tahun";
                #endregion
                break;

            case "KPI":
                #region query KPI
                query = "/*select Tahun,SopName,NilaiBobot,PIC,Approval, " +
                           "SUM(Jan)Jan,SUM(janS)JanS,(SUM(jan)*SUM(janS)/100)JanN, " +
                           " SUM(Feb)Feb,SUM(febS)FebS,(SUM(feb)*SUM(febS)/100)FebN, " +
                           " SUM(Mar)Mar,SUM(marS)MarS,(SUM(Mar)*SUM(marS)/100)MarN, " +
                           " SUM(Apr)Apr,SUM(aprS)AprS,(SUM(Apr)*SUM(aprS)/100)AprN from ( */" +
                           "select Tahun,KPINo as SOPNo,KPIName as SOPName,(NilaiBobot*100)NilaiBobot,CategoryID,PIC,Approval, " +
                           " ISNULL([1],0)Jan,ISNULL([2],0)Feb,ISNULL([3],0)Mar,ISNULL([4],0)Apr,ISNULL([5],0)Mei, " +
                           " ISNULL([6],0)Jun,ISNULL([7],0)Jul,ISNULL([8],0)Ags,ISNULL([9],0)Sep,ISNULL([10],0)Okt, " +
                           " ISNULL([11],0)Nop,ISNULL([12],0)Desm, " +
                           " (NilaiBobot*ISNULL([1],0)) janS,(NilaiBobot*ISNULL([2],0)) febS,(NilaiBobot*ISNULL([3],0)) marS, " +
                           " (NilaiBobot*ISNULL([4],0)) aprS,(NilaiBobot*ISNULL([5],0)) meiS,(NilaiBobot*ISNULL([6],0)) junS, " +
                           " (NilaiBobot*ISNULL([7],0)) julS,(NilaiBobot*ISNULL([8],0)) agsS,(NilaiBobot*ISNULL([9],0)) sepS, " +
                           " (NilaiBobot*ISNULL([10],0)) oktS,(NilaiBobot*ISNULL([11],0)) nopS,(NilaiBobot*ISNULL([12],0)) desS  " +
                           " from ( " +
                           " select KPINo,KPIName,NilaiBobot,CategoryID,PIC,sd.Approval, CAST(MONTH(TglMulai) as varchar) as Bulan,YEAR(tglMulai)Tahun,sd.PointNilai " +
                           " from ISO_KPI as sp " +
                           " left Join ISO_KPIDetail as sd " +
                           " on sd.KPIID=sp.ID " +
                           " where sp.RowStatus>-1 and sd.RowStatus>-1 " + this.Criteria +
                           " Group by Month(tglMulai),YEAR(tglMulai),KPINo,KPIName,NilaiBobot,CategoryID,PIC,sd.Approval,sd.PointNilai) " +
                           " up pivot (SUM(PointNilai) for Bulan in([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12])) as pv1 " +
                           " order by RIGHT(KPINo,4),Tahun";
                //"/*) as x " +
                //" group by x.Tahun,x.SOPName,x.NilaiBobot,x.PIC,x.Approval " +
                //" order by Tahun*/ ";
                #endregion
                break;
            case "ItemSOP":
                #region old query
                query1 = "select *,case when Approval=2 then xxx.Bobot*PointNilai/100 else 0 end Nilai from ( " +
                        "select iu.ID,iu.UserID,iu.CategoryID,ic.Description,iu.PesType, Case when iu.TypeBobot='%' " +
                        "then iu.Bobot *100 else iu.Bobot end Bobot,iu.TypeBobot,ic.Target,CAST(ic.KodeUrutan as int)KodeUrutan," +
                        "isnull(xx.NilaiBobot,0)NilaiBobot," +
                        "xx.Keterangan,xx.KetTargetKe,isnull(xx.PointNilai,0)PointNilai,isnull(xx.Approval,0)Approval  " +
                        "from ISO_UserCategory iu   LEFT Join ISO_Category as ic on ic.ID=iu.CategoryID  " +
                        "LEFT JOIN " +
                        "( " +
                        "select sp.ID,sp.CategoryID,sp.Keterangan,sp.Status,sp.CreatedTime,sp.ApprovedDate,sp.NilaiBobot,sd.KetTargetKe,sd.PointNilai,sd.Approval " +
                        "from ISO_SOP as sp " +
                        "LEFT JOIN ISO_SOPDetail as sd " +
                        "on sd.SOPID=sp.ID " +
                        "where sp.RowStatus>-1 and sd.RowStatus>-1 and MONTH(sp.TglMulai)=" + this.Bulan + " and YEAR(sp.TglMulai)= " + this.Tahun +
                        "and sp.PIC=(Select Top 1 UserName from ISO_Users where ID=" + this.UserID + " and RowStatus>-1) ) as xx on xx.CategoryID=iu.ID " +
                        "where  iu.RowStatus>-1 and iu.CategoryID >0 and iu.PesType=3 and iu.UserID=" + this.UserID +
                        " ) as xxx " + this.Criteria +
                        "order by xxx.KodeUrutan";
                #endregion
                #region new query
                query = "select *,case when Approval=2 then" +
                      " case when xd.Penilaian=0 AND (xd.KetTargetKe='' OR xd.KetTargetKe is null) then 0 else (xd.Bobot*xd.PointNilai) end  else 0 end Nilai from ( " +
                      " select x.*,PIC,isnull(SopScoreID,0)ScoreID, isnull(xx.PointNilai,0) PointNilai,isnull((select dbo.GetTotalBobotSOP(" + this.UserID + "," + this.Bulan + "," + this.Bagian + ")),0)Bbt," +
                      " xx.Keterangan,isnull(xx.Approval,0)Approval,xx.KetTargetKe,isnull(xx.IDx,0)Inputed from ( " +
                      "  select iu.ID,ic.ID as CatID,iu.UserID,ic.Description,ic.calc,iu.Penilaian,iu.Bobot,Cast(ic.KodeUrutan as int)KodeUrutan,iu.PesType,ic.Target  " +
                      "  from ISO_UserCategory as iu " +
                      "  LEFT JOIN ISO_Category as ic ON ic.ID=iu.CategoryID " +
                      "  /*LEFT JOIN ISO_SOPScore as sp ON sp.CategoryID=iu.CategoryID */ " +
                      "  where UserID=" + this.UserID + " and iu.PesType=3 and iu.RowStatus>-1 and iu.SectionID= " + this.Bagian +
                      "  ) as x " +
                      "  LEFT JOIN  " +
                      "  (select ip.ID,ip.CategoryID ,ip.ISO_UserID,ip.BagianID,sd.SopScoreID,sd.PointNilai,ip.PIC,ip.Keterangan,sd.Approval,sd.KetTargetKe " +
                      "     ,count(ip.ID) over(partition by ip.ID) IDx " +
                      "      FROM ISO_SOP as ip " +
                      "      LEFT JOIN ISO_SOPDetail as sd ON sd.SOPID=ip.ID " +
                      "      Where ip.RowStatus>-1 and sd.RowStatus>-1 and RTRIM(CONVERT(Char,TglMulai,112))='" + Periode + "'  " +
                      "      and PIC=(select Username from ISO_Users where ID=" + this.UserID + ")) " +
                      "      as xx ON xx.CategoryID=x.ID " +
                      "  LEFT JOIN ISO_SOPScore AS sc ON sc.ID=xx.SopScoreID " +
                      "  ) as xd " +
                      "  order by xd.KodeUrutan";
                #endregion
                break;
            case "ItemSOPNew":
                query = "select *,Case When Approval=2 then (PointNilai*NilaiBobot) else 0 end Nilai, Case When Status=0 then 'Open' else 'Approved' end StatusApv from (  " +
                        "select sop.ID,SOPName as Description,sop.CategoryID,sop.ApprovedDate,NilaiBobot,Keterangan,sop.Status as StatusH,sop.CreatedTime,  " +
                        "sod.KetTargetKe,ic.ID as CatID,ic.Target,isnull((select dbo.GetTotalBobotSOP(" + this.UserID + "," + this.Bulan + "," + this.Bagian + ")),0)Bbt,   " +
                        " sod.PointNilai,sod.SopScoreID,sod.Status,sod.TargetKe,iuc.UserID,iuc.Bobot,iuc.PesType,sod.Approval   " +
                        " FROM ISO_SOP as sop    " +
                        " LEFT JOIN ISO_SOPDetail as sod ON sod.SOPID=sop.ID   " +
                        " LEFT JOIN ISO_UserCategory as iuc ON iuc.ID=sop.CategoryID  " +
                        " LEFT JOIN ISO_Category as ic on ic.ID=iuc.CategoryID  " +
                        " where sod.RowStatus>-1 and sop.RowStatus>-1 and MONTH(TglMulai)=" + this.Bulan + " AND YEAR(TglMulai)=" + this.Tahun + " and   " +
                        " PIC=(select Username from ISO_Users where ID=" + this.UserID + ") and sop.BagianID=" + this.Bagian +
                        " ) as x " + UnionItemSOP("3", "SOP");

                break;
            case "PICSopOld":
                query = "select Distinct UserName,isnull(UserID,0) UserID,isnull(BagianID,0)BagianID,(select BagianName from ISO_Bagian where ID=BagianID)BagianName " +
                        "from UserAccount where RowStatus>-1 " + this.Criteria + " order by userName";
                break;
            case "PICSop":
                query = "select * from (select Distinct UserName,isnull(UserID,0) UserID,isnull(BagianID,0)BagianID, " +
                      "(select BagianName from ISO_Bagian where ID=BagianID)BagianName,(select Urutan from ISO_Bagian where ID=BagianID)Urutan " +
                      "from UserAccount where RowStatus>-1 " + this.Criteria +
                      ")as x order by Urutan,UserName";
                break;
            case "ItemKPI":
                #region OldQuery
                query1 = "select *,case when Approval=2 then xxx.Bobot*PointNilai/100 else 0 end Nilai from ( " +
                       "select iu.ID,iu.UserID,iu.CategoryID,ic.Description,iu.PesType, Case when iu.TypeBobot='%' " +
                       "then iu.Bobot *100 else iu.Bobot end Bobot,iu.TypeBobot,ic.Target,CAST(ic.KodeUrutan as int)KodeUrutan," +
                       "isnull(xx.NilaiBobot,0)NilaiBobot," +
                       "xx.Keterangan,xx.KetTargetKe,isnull(xx.PointNilai,0)PointNilai,isnull(xx.Approval,0)Approval  " +
                       "from ISO_UserCategory iu   LEFT Join ISO_Category as ic on ic.ID=iu.CategoryID  " +
                       "LEFT JOIN " +
                       "( " +
                       "select sp.ID,sp.CategoryID,sp.Keterangan,sp.Status,sp.CreatedTime,sp.ApprovedDate,sp.NilaiBobot,sd.KetTargetKe,sd.PointNilai,sd.Approval " +
                       "from ISO_KPI as sp " +
                       "LEFT JOIN ISO_KPIDetail as sd " +
                       "on sd.KPIID=sp.ID " +
                       "where sp.RowStatus>-1 and sd.RowStatus>-1 and MONTH(sp.TglMulai)=" + this.Bulan + " and YEAR(sp.TglMulai)= " + this.Tahun +
                       "and sp.PIC=(Select Top 1 UserName from ISO_Users where ID=" + this.UserID + " and RowStatus>-1) ) as xx on xx.CategoryID=iu.ID " +
                       "where  iu.RowStatus>-1 and iu.CategoryID >0 and iu.PesType=1 and iu.UserID=" + this.UserID +
                       " ) as xxx " + this.Criteria +
                       "order by xxx.KodeUrutan";
                #endregion
                #region new query
                query = "select *,case when Approval=2 then " +
                      " case when xd.Penilaian=0 AND (xd.KetTargetKe='' OR xd.KetTargetKe is null) then 0 else (xd.Bobot*xd.PointNilai) end  else 0 end Nilai from ( " +
                      " select x.*,PIC,isnull(SopScoreID,0)ScoreID,isnull(xx.PointNilai,0)PointNilai," +
                      " isnull((select dbo.GetTotalBobotKPI(" + this.UserID + "," + this.Bulan + "," + this.Bagian + ")),0)Bbt," +
                      " xx.Keterangan,isnull(xx.Approval,0)Approval,xx.KetTargetKe,isnull(xx.IDx,0)Inputed from ( " +
                      "  select iu.ID,ic.ID as CatID,iu.UserID,ic.Description,ic.calc,iu.Penilaian,iu.Bobot,Cast(ic.KodeUrutan as int)KodeUrutan,iu.PesType,ic.Target  " +
                      "  from ISO_UserCategory as iu " +
                      "  LEFT JOIN ISO_Category as ic ON ic.ID=iu.CategoryID " +
                      "  /*LEFT JOIN ISO_SOPScore as sp ON sp.CategoryID=iu.CategoryID */ " +
                      "  where UserID=" + this.UserID + " and iu.PesType=1 and iu.RowStatus>-1 and iu.SectionID= " + this.Bagian +
                      "  ) as x " +
                      "  LEFT JOIN  " +
                      "  (select ip.ID,ip.CategoryID ,ip.ISO_UserID,ip.BagianID,sd.SopScoreID,sd.PointNilai,ip.PIC,ip.Keterangan,sd.Approval,sd.KetTargetKe " +
                      "     ,count(ip.ID) over(partition by ip.ID) IDx " +
                      "      FROM ISO_KPI as ip " +
                      "      LEFT JOIN ISO_KPIDetail as sd ON sd.KPIID=ip.ID " +
                      "      Where ip.RowStatus>-1 and sd.RowStatus>-1 and CONVERT(Char,TglMulai,112)='" + Periode + "'  " +
                      "      and PIC=(select Username from ISO_Users where ID=" + this.UserID + ")) " +
                      "      as xx ON xx.CategoryID=x.ID " +
                      "  LEFT JOIN ISO_SOPScore AS sc ON sc.ID=xx.SopScoreID " +
                      "  ) as xd " +
                      "  order by xd.KodeUrutan";
                #endregion
                break;
            case "ItemKPINew":
                query = "select *,Case When Approval=2 then (PointNilai*NilaiBobot) else 0 end Nilai, Case When Status=0 then 'Open' else 'Approved' end StatusApv from (  " +
                        "select sop.ID,KPIName as Description,sop.CategoryID,sop.ApprovedDate,NilaiBobot,Keterangan,sop.Status as StatusH,sop.CreatedTime,  " +
                        "sod.KetTargetKe,ic.ID as CatID,ic.Target,isnull((select dbo.GetTotalBobotKPI(" + this.UserID + "," + this.Bulan + "," + this.Bagian + ")),0)Bbt,   " +
                        " sod.PointNilai,sod.SopScoreID,sod.Status,sod.TargetKe,iuc.UserID,iuc.Bobot,iuc.PesType,sod.Approval   " +
                        " FROM ISO_KPI as sop    " +
                        " LEFT JOIN ISO_KPIDetail as sod ON sod.KPIID=sop.ID   " +
                        " LEFT JOIN ISO_UserCategory as iuc ON iuc.ID=sop.CategoryID  " +
                        " LEFT JOIN ISO_Category as ic on ic.ID=iuc.CategoryID  " +
                        " where sod.RowStatus>-1 and sop.RowStatus>-1 and MONTH(TglMulai)=" + this.Bulan + " AND YEAR(TglMulai)=" + this.Tahun + " and   " +
                        " PIC=(select Username from ISO_Users where ID=" + this.UserID + ") and sop.BagianID=" + this.Bagian +
                        " ) as x " + UnionItemSOP("1", "KPI");
                break;
            case "TotalSOP":
                query = "";
                break;
        }
        return query;
    }
    private string UnionItemSOP(string PesType, string PES)
    {
        int check = CheckInputan(PES);
        string query = (CheckInputan(PES) < 201509) ? "union all " +
                       "select iso.ID,ic.Description,iso.CategoryID,iso.Bobot NilaiBobot,''Keterangan,0,'',ic.ID as CatID " +
                       " ,ic.Target,iso.Bobot as Bbt,0 PoinNilai,0 SopScoreID,0 Status,'' TargetKe,iso.UserID,iso.Bobot, " +
                       " iso.PesType,0 Approval,0 Nilai from ISO_UserCategory iso  " +
                       " LEFT JOIN ISO_Category as ic ON ic.ID=iso.CategoryID " +
                       " where iso.ID not in( Select CategoryID  " +
                       " FROM ISO_" + PES + " as sop     " +
                       " where sop.RowStatus>-1  " +
                       " and MONTH(TglMulai)=" + this.Bulan + " AND YEAR(TglMulai)=" + this.Tahun + " and    " +
                       " PIC=(select Username from ISO_Users where ID=" + this.UserID + ")  " +
                       " and sop.BagianID=" + this.Bagian + " ) and iso.RowStatus >-2" +
                       " and iso.UserID=" + this.UserID + " and iso.SectionID=" + this.Bagian + " and iso.PesType= " + PesType +
                       " and iso.bulan <=" + this.Bulan + " and iso.Tahun<=" + this.Tahun : "";
        return query;
    }
    private int CheckInputan(string Pes)
    {
        int bulantahun = 0;
        string query = "SELECT (CAST(YEAR(CreatedTime) as CHAR(4))+''+RIGHT('0'+RTRIM(CAST(MONTH(CreatedTime) as CHAR(2))),2)) as YM " +
                        "FROM ISO_" + Pes + " where PIC =(SELECT UserName From ISO_Users Where ID=" + this.UserID + " and BagianID=" + this.Bagian + ")" +
                        " AND MONTH(TglMulai)=" + this.Bulan + " AND YEAR(TglMulai)=" + this.Tahun;
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(query);
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                bulantahun = (sdr["YM"] != DBNull.Value || sdr["YM"].ToString() != "") ? Convert.ToInt32(sdr["YM"].ToString()) : 0;
            }
        }
        return bulantahun;
    }
    public ArrayList Retrieve()
    {
        arrData = new ArrayList();
        string strsql = this.Query();
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(this.Query());
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                int app = 0;

                switch (this.Where)
                {
                    case "Detail":
                        #region Detail
                        app = Convert.ToInt32(sdr["Approval"].ToString());
                        arrData.Add(new PES
                        {
                            SOPName = sdr["SOPName"].ToString(),
                            SOPNo = sdr["SOPNo"].ToString(),
                            BobotNilai = Convert.ToDecimal(sdr["NilaiBobot"].ToString()),
                            PIC = sdr["PIC"].ToString(),
                            Approval = Convert.ToInt32(sdr["Approval"].ToString()),
                            Jan = Convert.ToDecimal(sdr["Jan"].ToString()),
                            JanS = (app < 2) ? 0 : Convert.ToDecimal(sdr["janS"].ToString()),
                            Feb = Convert.ToDecimal(sdr["Feb"].ToString()),
                            FebS = (app < 2) ? 0 : Convert.ToDecimal(sdr["FebS"].ToString()),
                            Mar = Convert.ToDecimal(sdr["Mar"].ToString()),
                            MarS = (app < 2) ? 0 : Convert.ToDecimal(sdr["MarS"].ToString()),
                            Apr = Convert.ToDecimal(sdr["Apr"].ToString()),
                            AprS = (app < 2) ? 0 : Convert.ToDecimal(sdr["AprS"].ToString()),
                            Mei = Convert.ToDecimal(sdr["Mei"].ToString()),
                            MeiS = (app < 2) ? 0 : Convert.ToDecimal(sdr["MeiS"].ToString()),
                            Jun = Convert.ToDecimal(sdr["Jun"].ToString()),
                            JunS = (app < 2) ? 0 : Convert.ToDecimal(sdr["JunS"].ToString()),
                            Jul = Convert.ToDecimal(sdr["Jul"].ToString()),
                            JulS = (app < 2) ? 0 : Convert.ToDecimal(sdr["JulS"].ToString()),
                            Ags = Convert.ToDecimal(sdr["Ags"].ToString()),
                            AgsS = (app < 2) ? 0 : Convert.ToDecimal(sdr["AgsS"].ToString()),
                            Okt = Convert.ToDecimal(sdr["Okt"].ToString()),
                            OktS = (app < 2) ? 0 : Convert.ToDecimal(sdr["OktS"].ToString()),
                            Nop = Convert.ToDecimal(sdr["Nop"].ToString()),
                            NopS = (app < 2) ? 0 : Convert.ToDecimal(sdr["NopS"].ToString()),
                            Des = Convert.ToDecimal(sdr["Desm"].ToString()),
                            DesS = (app < 2) ? 0 : Convert.ToDecimal(sdr["DesS"].ToString()),
                            Sep = Convert.ToDecimal(sdr["Sep"].ToString()),
                            SepS = (app < 2) ? 0 : Convert.ToDecimal(sdr["SepS"].ToString())
                        });
                        #endregion
                        break;
                    case "Rekap":
                        #region rekap
                        app = Convert.ToInt32(sdr["Approval"].ToString());
                        arrData.Add(new PES
                        {
                            SOPName = sdr["SOPName"].ToString(),
                            BobotNilai = Convert.ToDecimal(sdr["NilaiBobot"].ToString()),
                            PIC = sdr["PIC"].ToString(),
                            Approval = Convert.ToInt32(sdr["Approval"].ToString()),
                            Jan = Convert.ToDecimal(sdr["Jan"].ToString()),
                            JanS = (app < 2) ? 0 : Convert.ToDecimal(sdr["janS"].ToString()),
                            Feb = Convert.ToDecimal(sdr["Feb"].ToString()),
                            FebS = (app < 2) ? 0 : Convert.ToDecimal(sdr["FebS"].ToString()),
                            Mar = Convert.ToDecimal(sdr["Mar"].ToString()),
                            MarS = (app < 2) ? 0 : Convert.ToDecimal(sdr["MarS"].ToString()),
                            Apr = Convert.ToDecimal(sdr["Apr"].ToString()),
                            AprS = (app < 2) ? 0 : Convert.ToDecimal(sdr["AprS"].ToString())
                            /*,Mei = Convert.ToDecimal(sdr["Mei"].ToString()),
                            MeiS = (app < 2) ? 0 : Convert.ToDecimal(sdr["MeiS"].ToString()),
                            Jun = Convert.ToDecimal(sdr["Jun"].ToString()),
                            JunS = (app < 2) ? 0 : Convert.ToDecimal(sdr["JunS"].ToString()),
                            Jul = Convert.ToDecimal(sdr["Jul"].ToString()),
                            JulS = (app < 2) ? 0 : Convert.ToDecimal(sdr["JulS"].ToString()),
                            Ags = Convert.ToDecimal(sdr["Ags"].ToString()),
                            AgsS = (app < 2) ? 0 : Convert.ToDecimal(sdr["AgsS"].ToString()),
                            Okt = Convert.ToDecimal(sdr["Okt"].ToString()),
                            OktS = (app < 2) ? 0 : Convert.ToDecimal(sdr["OktS"].ToString()),
                            Nop = Convert.ToDecimal(sdr["Nop"].ToString()),
                            NopS = (app < 2) ? 0 : Convert.ToDecimal(sdr["NopS"].ToString()),
                            Des = Convert.ToDecimal(sdr["Desm"].ToString()),
                            DesS = (app < 2) ? 0 : Convert.ToDecimal(sdr["DesS"].ToString()),
                            Sep = Convert.ToDecimal(sdr["Sep"].ToString()),
                            SepS = (app < 2) ? 0 : Convert.ToDecimal(sdr["SepS"].ToString())*/
                        });
                        #endregion
                        break;
                    //int sts = 0;
                    case "ItemSOP":
                        #region NewReport
                        string[] scr = new string[] { };
                        //sts = Convert.ToInt32(sdr["Status"].ToString());
                        scr = CheckNA(Convert.ToInt32(sdr["CatID"].ToString())).Split(',');
                        arrData.Add(new PES
                        {
                            ID = Convert.ToInt32(sdr["ID"].ToString()),
                            ISOUserID = Convert.ToInt32(sdr["UserID"].ToString()),
                            SOPName = sdr["Description"].ToString(),
                            BobotNilai = Convert.ToDecimal(sdr["Bobot"].ToString()) * 100,
                            TypePes = Convert.ToInt32(sdr["PesType"].ToString()),
                            Target = sdr["Target"].ToString(),
                            Pencapaian = (scr.Contains(this.Bulan) || scr.Contains("0")) ? sdr["Keterangan"].ToString() : "N/A",
                            Score = Convert.ToDecimal(sdr["PointNilai"].ToString()),
                            Nilai = Convert.ToDecimal(sdr["Nilai"].ToString()),
                            TotalBobot = Convert.ToDecimal(sdr["Bbt"].ToString()) * 100,
                            TglInput = DateTime.Parse(sdr["CreatedTime"].ToString()),
                            TglApproved = (string.IsNullOrEmpty(sdr["ApprovedDate"].ToString())) ? DateTime.MinValue : Convert.ToDateTime(sdr["ApprovedDate"]),
                            StatusApv = sdr["StatusApv"].ToString()
                        });
                        #endregion
                        break;
                    case "PICSop":
                        arrData.Add(new PES
                        {
                            PIC = sdr["UserName"].ToString(),
                            ISOUserID = Convert.ToInt32(sdr["UserID"].ToString()),
                            BagianID = Convert.ToInt32(sdr["BagianID"].ToString()),
                            BagianName = sdr["BagianName"].ToString()
                        });
                        break;
                }
            }
        }
        return arrData;
    }
    public void GetTahun(DropDownList ddl)
    {
        arrData = new ArrayList();
        string strSQL = "select distinct YEAR(tglMulai) Tahun from ISO_Task order by year(tglMulai)";
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(strSQL);
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                ddl.Items.Add(new ListItem(sdr["Tahun"].ToString(), sdr["Tahun"].ToString()));
            }
        }
    }
    public ArrayList RetrieveDept()
    {
        arrData = new ArrayList();
        string strSQL = "Select * from Dept Where RowStatus>-1 " + this.Criteria + " order by DeptName";
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(strSQL);
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrData.Add(new PES
                {
                    DeptID = Convert.ToInt32(sdr["ID"].ToString()),
                    DeptName = sdr["Alias"].ToString()
                });
            }
        }
        return arrData;
    }
    private string CheckNA(int CatID)
    {
        string na = string.Empty;
        string strSQL = "Select calc from ISO_Category where ID=" + CatID;
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(strSQL);
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                na = sdr["Calc"].ToString();
            }
        }
        return na;
    }
    public int isHasbeenInput(string Tabled)
    {
        int result = 0;
        string strSQL = "Select Count(ID)ID from " + Tabled + " where Month(tglMulai)=" + this.Bulan + " and Year(tglMulai)=" + this.Tahun +
                      " and PIC=(Select UserName from ISO_Users where ID=" + this.UserID + ") and BagianID=" + this.Bagian +
                      " and RowStatus>-1";
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(strSQL);
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                result = Convert.ToInt32(sdr["ID"].ToString());
            }
        }
        return result;
    }
}