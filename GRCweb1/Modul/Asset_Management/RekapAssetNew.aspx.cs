using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.IO;
using DataAccessLayer;
using System.Web.UI.HtmlControls;

namespace GRCweb1.Modul.Asset_Management
{
    public partial class RekapAssetNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx"; ddlUrutan.Enabled = false;
                FillItems("AM_Group", " where RowStatus>-1 order by ID", ddlGroupAsset);
                FillItems("AM_Lokasi", " where RowStatus>-1 order by namalokasi", ddlLokasiAsset);
                btnBack.Visible = (Request.QueryString["f"] != null) ? true : false;
                if (Request.QueryString["f"] != null)
                {
                    if (Request.QueryString["f"].ToString() == "cr")
                    {
                        btnBack.Visible = true;
                    }
                    else
                    {
                        btnBack.Visible = false;
                    }
                }
                if (Request.QueryString["gp"] != null)
                {
                    ddlGroupAsset.SelectedValue = Request.QueryString["gp"].ToString();
                    ddlGroupAsset_Change(null, null);
                    ddlKelasAsset.SelectedValue = (Request.QueryString["sc"] != null) ? Request.QueryString["sc"].ToString() : "";
                    btnPreview_Click(null, null);
                }
            }
       ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(exPort);
            ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(opname);

        }
        protected void FillItems(string Tabel, string where, DropDownList Fld)
        {
            /**
            * Tampilkan data group asset ke dalam dropdown
            */

            Fld.Items.Clear();
            ArrayList arrData = new ArrayList();
            AssetManagementFacade dataFacade = new AssetManagementFacade();
            arrData = dataFacade.Retrieve(Tabel, where);
            Fld.Items.Add("");
            foreach (AssetManagement ListData in arrData)
            {
                switch (Tabel)
                {
                    case "AM_Group": Fld.Items.Add(new ListItem(ListData.NamaGroup, ListData.ID.ToString())); break;
                    case "AM_Class":
                        Fld.Items.Add(new ListItem(ListData.NamaClass, ListData.ID.ToString())); break;
                    case "AM_SubClass": Fld.Items.Add(new ListItem(ListData.NamaGroup, ListData.ID.ToString())); break;
                    case "AM_Lokasi": Fld.Items.Add(new ListItem(ListData.NamaGroup, ListData.ID.ToString())); break;
                    case "Dept": Fld.Items.Add(new ListItem(ListData.NamaGroup, ListData.ID.ToString())); break;

                }
            }

            Fld.SelectedIndex = 0;
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("AssetCreated.aspx");
        }
        public ArrayList LoadData()
        {
            string where = string.Empty;
            where = " where RowStatus >-1";
            where += (ddlGroupAsset.SelectedValue == string.Empty) ? "" : " and GroupID=" + ddlGroupAsset.SelectedValue.ToString();
            where += (ddlKelasAsset.SelectedValue == string.Empty) ? "" : " and ClassID=" + ddlKelasAsset.SelectedValue.ToString();
            where += (ddlLokasiAsset.SelectedValue == string.Empty) ? "" : " and LokasiID=" + ddlLokasiAsset.SelectedValue.ToString();
            ArrayList arrData = new ArrayList();
            arrData = new AssetManagementFacade().Retrieve("AM_Asset_v", where);
            return arrData;
        }
        private void LoadGroup()
        {
            string where = string.Empty; string query = string.Empty;
            query = " ";
            where = " where RowStatus>-1";
            where += (ddlGroupAsset.SelectedValue == string.Empty) ? "" :
                   " and ID=" + ddlGroupAsset.SelectedValue;
            where += " order by ID";
            ArrayList arrData = new ArrayList();
            arrData = new AssetManagementFacade().Retrieve("AM_Group", where);
            lstKateg.DataSource = arrData;
            lstKateg.DataBind();
        }
        private ArrayList LoadKelas()
        {
            string where = string.Empty; string query = string.Empty;
            query = " ";
            where = " where RowStatus>-1";
            where += (ddlGroupAsset.SelectedValue == string.Empty) ? "" :
                   " and GroupID=" + ddlGroupAsset.SelectedValue;
            where += (ddlKelasAsset.SelectedValue == string.Empty) ? "" :
                    " and ID=" + ddlKelasAsset.SelectedValue;
            where += " order by CAST(KodeClass as int)";
            ArrayList arrData = new ArrayList();
            arrData = new AssetManagementFacade().Retrieve("AM_Class", where);
            return arrData;
        }
        protected void lstKateg_DataBound(object sender, RepeaterItemEventArgs e)
        {
            //string where = string.Empty; string query = string.Empty; string query2 = string.Empty;
            //string GroupID = ddlGroupAsset.SelectedValue;
            //string SubClassID = ddlSubKelasAsset.SelectedValue;

            //if (GroupID == "5")
            //{
            //    query2 = "  ";
            //}
            //else
            //{
            //    query2 = "";
            //}

            //AssetManagement asm = (AssetManagement)e.Item.DataItem;
            //Repeater lstKlass = (Repeater)e.Item.FindControl("lstKelas");        
            ////query = " ,(select COUNT(B.NamaClass) from AM_SubClass B where B.ClassID=A.ID and B.RowStatus>-1)JumlahAsset ";
            //where = " where A.RowStatus>-1";
            //where += " and A.GroupID=" + asm.ID;
            //where += (ddlKelasAsset.SelectedValue == string.Empty) ? "" : " and A.ID=" + ddlKelasAsset.SelectedValue;
            //where += " order by CAST(A.KodeClass as int)";
            //ArrayList arrData = new ArrayList();
            //arrData = new AssetManagementFacade().Retrieve("AM_Class", where);
            //lstKlass.DataSource = arrData;
            //lstKlass.DataBind();


            string where = string.Empty; string query = string.Empty; string query2 = string.Empty;
            string GroupID = ddlGroupAsset.SelectedValue;
            string SubClassID = ddlSubKelasAsset.SelectedValue;

            if (GroupID == "5")
            {
                AssetManagement asm = (AssetManagement)e.Item.DataItem;
                where = " INNER JOIN AM_SubClass B ON A.ID=B.ClassID where A.RowStatus>-1 and A.GroupID=" + asm.ID;
                //where += " and A.GroupID=" + asm.ID;
                where += (ddlKelasAsset.SelectedValue == string.Empty) ? "" : " and B.RowStatus>-1 and A.ID=" + ddlKelasAsset.SelectedValue + " and B.ID=" + ddlSubKelasAsset.SelectedValue;
                where += " order by CAST(A.KodeClass as int)";


            }
            else
            {
                //query2 = "";
                AssetManagement asm = (AssetManagement)e.Item.DataItem;
                where = " where A.RowStatus>-1";
                where += " and A.GroupID=" + asm.ID;
                where += (ddlKelasAsset.SelectedValue == string.Empty) ? "" : " and A.ID=" + ddlKelasAsset.SelectedValue;
                where += " order by CAST(A.KodeClass as int)";
            }

            //AssetManagement asm = (AssetManagement)e.Item.DataItem;
            Repeater lstKlass = (Repeater)e.Item.FindControl("lstKelas");
            //query = " ,(select COUNT(B.NamaClass) from AM_SubClass B where B.ClassID=A.ID and B.RowStatus>-1)JumlahAsset ";
            //where = " where A.RowStatus>-1";
            //where += " and A.GroupID=" + asm.ID;
            //where += (ddlKelasAsset.SelectedValue == string.Empty) ? "" : " and A.ID=" + ddlKelasAsset.SelectedValue;
            //where += " order by CAST(A.KodeClass as int)";
            ArrayList arrData = new ArrayList();
            arrData = new AssetManagementFacade().Retrieve("AM_Class", where);
            lstKlass.DataSource = arrData;
            lstKlass.DataBind();

        }
        protected void lstKelas_Databound(object sender, RepeaterItemEventArgs e)
        {
            #region Logic Lama
            //AssetManagement asm = (AssetManagement)e.Item.DataItem;
            //Repeater lstKlass = (Repeater)e.Item.FindControl("lstSubClass");
            //string where = string.Empty;
            //where = " where RowStatus>-1";
            //where += (ddlKelasAsset.SelectedValue == string.Empty) ? " and ClassID="+asm.ID : " and ClassID=" + ddlKelasAsset.SelectedValue;
            //where += " order by CAST(KodeClass as int)";
            //ArrayList arrData = new ArrayList();
            //arrData = new AssetManagementFacade().Retrieve("AM_SubClass", where);
            //lstKlass.DataSource = arrData;
            //lstKlass.DataBind();
            #endregion
            #region Logic Baru
            //LinkButton lnk = (LinkButton)e.Item.FindControl("grp");
            //AssetManagement file = (AssetManagement)e.Item.DataItem;
            //LinkButton att = (LinkButton)e.Item.FindControl("link");
            //att.Attributes.Add("onclick", "OpenDialog('" + file.ID + "')");
            string pic = string.Empty;
            AssetManagementFacade amFacade = new AssetManagementFacade();
            if (ddlKelasAsset.SelectedValue != string.Empty)
                pic = amFacade.GetPICperson(int.Parse(ddlKelasAsset.SelectedValue));

            AssetManagement asm = (AssetManagement)e.Item.DataItem;
            Repeater lstKlass = (Repeater)e.Item.FindControl("lstSubClass");
            string where = string.Empty; string where2 = string.Empty;
            //where = " where RowStatus>-1";
            //where += (ddlKelasAsset.SelectedValue == string.Empty) ? " and ClassID="+asm.ID : " and ClassID=" + ddlKelasAsset.SelectedValue;
            //where += " order by CAST(KodeClass as int)";
            //test ini dulu nanti bedakan yg maintenance & hrd
            string strSelect = string.Empty; string A = string.Empty;
            //update AM_Asset_v set PICPerson='%Maintenance Manager%' where PICPerson='adminsp'
            //if (pic.ToUpper().Contains("MAIN"))
            string classasset = string.Empty;
            if (ddlKelasAsset.SelectedValue == "")
                classasset = "0";
            else
                classasset = ddlKelasAsset.SelectedValue;
            if (pic.ToUpper().Contains("MTC") || ddlGroupAsset.SelectedValue == "3")
            {
                //where = " A LEFT JOIN AM_SubClass B ON A.NamaAsset=B.NamaClass where A.RowStatus>-1 and A.PICPerson like '%MTC%' and A.ClassID=" + ddlKelasAsset.SelectedValue;
                where = " A LEFT JOIN AM_Asset_v B ON B.NamaAsset=A.NamaClass where A.RowStatus>-1 and B.RowStatus>-1 and B.PICPerson like '%MTC%' and B.ClassID=" + asm.ID;
                strSelect = "MTC";
                A = " AM_SubClass ";
                //ArrayList arrData = new ArrayList();
                //arrData = new AssetManagementFacade().Retrieve("AM_SubClass", where);
                //arrData = new AssetManagementFacade().Retrieve_New1(strSelect, "AM_SubClass", where, where2);
                //lstKlass.DataSource = arrData;
                //lstKlass.DataBind();
            }
            else
            {
                where2 = " AM_SubClass A where ClassID=" + asm.ID + " and ID=" + ddlSubKelasAsset.SelectedValue;
                where =
                " where ClassID=" + asm.ID + " and SubClassID=" + ddlSubKelasAsset.SelectedValue + " group by NamaClass,SubClassID,NewKodeAsset,NamaAsset ) as Data1 " +
                " group by Data1.NamaClass,Data1.KodeAsset,Data1.Qty,Data1.Urutan,Data1.ValAsset ,Data1.GroupID,Data1.SubClassID,Data1.NamaClass ) " +
                " as Data2 ) as DataAkhir order by SubClassID,Urutan,KodeAsset ";

                strSelect = "HRD";
                A = " AM_Asset_v ";
                //ArrayList arrData = new ArrayList();
                //arrData = new AssetManagementFacade().Retrieve("AM_SubClass", where);
                //arrData = new AssetManagementFacade().Retrieve_New1(strSelect, "AM_Asset_v", where, where2);
                //lstKlass.DataSource = arrData;
                //lstKlass.DataBind();
            }

            ArrayList arrData = new ArrayList();
            ////arrData = new AssetManagementFacade().Retrieve("AM_SubClass", where);
            arrData = new AssetManagementFacade().Retrieve_New1(strSelect, A, where, where2);
            lstKlass.DataSource = arrData;
            lstKlass.DataBind();
            #endregion
        }

        protected void lstKelas_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            LinkButton lnk = (LinkButton)e.Item.FindControl("link");

            switch (e.CommandName)
            {
                case "save":
                    break;
            }
        }
        protected void lstSubClass_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "pilih":

                    break;
            }
        }
        protected void lstSubClass_DataBound(object sender, RepeaterItemEventArgs e)
        {
            AssetManagement file = (AssetManagement)e.Item.DataItem;
            Repeater lstSubClass = (Repeater)e.Item.FindControl("lstSubClass");
            HtmlTableRow tr0 = (HtmlTableRow)e.Item.FindControl("lst");

            tr0.Cells[2].Attributes.Add("onclick", "return loadHistory('" + file.KodeAsset.Trim() + " " + "-" + file.NamaGroup.Trim() + "')");
            //tr0.Cells[3].Attributes.Add("onclick", "return loadHistory('" + file.NamaSubClass.Trim() + "')");
            //tr1.Cells[2].Attributes.Add("onclick", "return loadHistory('" + file.KodeAsset.Trim() + "')");
            tr0.Cells[2].Attributes.Add("title", "Click for detail komponen asset");
        }

        protected void lstDetails_DataBound(object sender, RepeaterItemEventArgs e)
        {
            AssetManagement file = (AssetManagement)e.Item.DataItem;
            //LinkButton att = (LinkButton)e.Item.FindControl("link");
            //att.Attributes.Add("onclick", "OpenDialog('" + file.ID + "')");
        }

        protected void lstAsset_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());
            switch (e.CommandName)
            {
                case "edit":
                    Response.Redirect("AssetMaster.aspx?id=" + id + "&gp=" + ddlGroupAsset.SelectedValue + "&sc=" + ddlKelasAsset.SelectedValue);
                    break;
            }
        }
        protected void lstAsset_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Image edit = (Image)e.Item.FindControl("edit");
            Label info = (Label)e.Item.FindControl("lbl");
            AssetManagement asm = (AssetManagement)e.Item.DataItem;
            string[] AllDept = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ClassEdt", "AsetMGM").Split(',');
            info.Visible = (asm.ID > 0) ? true : false;
            info.Text = (AllDept.Contains(((Users)Session["Users"]).DeptID.ToString())) ?
                "  [ Nilai Asset : " + asm.NilaiAsset.ToString("###,##0.00") + ". Tanggal Perolehan : " + asm.TglAsset.ToString("dd/MM/yyyy") +
                " PIC : " + asm.PicPerson.ToString() + " ]" : "";
            info.ForeColor = System.Drawing.Color.Red;
            //edit.Visible = (((Users)Session["Users"]).DeptID == asm.PicDept) ? true : false;
            edit.Visible = false;
            //edit.Visible = (AllDept.Contains(((Users)Session["Users"]).DeptID.ToString())) ? true : edit.Visible;
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            if (ddlGroupAsset.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Group Asset Harus di pilih");
                return;
            }
            if (ddlUrutan.SelectedValue == "Lokasi")
            {
                lst.Visible = false;
                lstOpname.Visible = true;
                LoadBaseLokasi();
                lstOpname.Attributes.Add("style", "display:block;height:370px;overflow:auto");
            }
            else
            {
                lst.Visible = true;
                lstOpname.Visible = false;
                lstOpname.Attributes.Add("style", "display:none");
                LoadGroup();
            }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem grp in lstKateg.Items)
            {
                Repeater cs = (Repeater)grp.FindControl("lstKelas");
                foreach (RepeaterItem css in cs.Items)
                {
                    Repeater scs = (Repeater)css.FindControl("lstSubClass");
                    foreach (RepeaterItem scl in scs.Items)
                    {
                        Repeater lstA = (Repeater)scl.FindControl("lstAsset");
                        foreach (RepeaterItem ls in lstA.Items)
                        {
                            Image img = (Image)ls.FindControl("edit");
                            img.Visible = false;
                        }
                    }
                }

            }
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=RekapAsset.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "Asset Group :" + ddlGroupAsset.SelectedItem.ToString().ToUpper();
            //Html += "Periode : " + txtDrTgl.Text + " s/d " + txtSdTgl.Text;
            //Html += "<form id='frm1' runat='server' method='post'>";
            string HtmlEnd = "";// "</form>";
            lst.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("collapse\">", "collapse\" border=\"1\">");
            Contents = Contents.Replace("cls\">", "\">'");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        protected void ddlGroupAsset_Change(object sender, EventArgs e)
        {
            //if (ddlGroupAsset.SelectedValue == "5")
            //{
            //    SubClassID.Visible = true;
            //    LoadSubClass("5");
            //}
            //else
            //{
            //    SubClassID.Visible = false;
            //}

            if (ddlGroupAsset.SelectedIndex > 0)
            {
                FillItems("AM_Class", " where RowStatus>-1 and GroupID=" + ddlGroupAsset.SelectedValue + " order by NamaClass,Cast(KodeClass as int)", ddlKelasAsset);
                //FillItems("AM_SubClass", " where RowStatus>-1 and ClassID in (select ID from AM_Class where GroupID=" + ddlGroupAsset.SelectedValue + " and RowStatus>-1)  order by Cast(KodeClass as int), NamaClass", ddlKelasAsset);
            }
            else
            {
                ddlKelasAsset.SelectedIndex = 0;
            }
        }

        protected void ddlKelasAsset_Change(object sender, EventArgs e)
        {
            if (ddlGroupAsset.SelectedValue == "5")
            {
                string GroupAsset = ddlGroupAsset.SelectedValue;
                string ClassID = ddlKelasAsset.SelectedValue;

                SubClassID.Visible = true;
                LoadSubClass(GroupAsset, ClassID);
            }
            else
            {
                SubClassID.Visible = false;
            }
        }

        private void LoadSubClass(string Group, string ClassID)
        {
            //AssetManagement DAsset = new AssetManagement();
            ArrayList arrAs = new ArrayList();
            AssetManagementFacade FAsset = new AssetManagementFacade();
            arrAs = FAsset.RetrieveSubCLass(Group, ClassID);
            if (arrAs.Count > 0)
            {
                ddlSubKelasAsset.Items.Clear();
                ddlSubKelasAsset.Items.Add(new ListItem("---- Pilih Nama SubClass ----", "0"));
                foreach (AssetManagement ass in arrAs)
                {
                    ddlSubKelasAsset.Items.Add(new ListItem(ass.NamaSubClass, ass.ID.ToString()));
                }
            }

        }

        //ArrayList arrWOP = new ArrayList();
        //        WorkOrderFacade_New WOF1 = new WorkOrderFacade_New();
        //        arrWOP = WOF1.GetPermintaan(Permintaan.Trim());

        //        if (Permintaan == "Hardware")
        //        {
        //            ddlHardware.Items.Clear();
        //            ddlHardware.Items.Add(new ListItem("--Pilih --", "0"));
        //            foreach (WorkOrder_New pl in arrWOP)
        //            {
        //                ddlHardware.Items.Add(new ListItem(pl.Permintaan, pl.ID.ToString()));
        //            }
        //        }
        //asset list for opname
        private void LoadBaseLokasi()
        {
            ArrayList arrData = new ArrayList();
            OpnameAsset opm = new OpnameAsset();
            opm.Field = "LokasiID,NamaLokasi,KodeLokasi";
            opm.Criteria = " and GroupID=" + ddlGroupAsset.SelectedValue.ToString();
            opm.Option = OpnameAsset.Operasi.Lokasi;
            opm.GroupBy = " Group By LokasiID,NamaLokasi,KodeLokasi";
            opm.OrderBy = " Order by KodeLokasi,NamaLokasi";
            arrData = opm.Retrieve();
            lstLokasi.DataSource = arrData;
            lstLokasi.DataBind();
        }
        protected void lstLokasi_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater lstLokasi = (Repeater)e.Item.FindControl("lstGroupAsset");
            AssetManagement asm = (AssetManagement)e.Item.DataItem;
            ArrayList arrData = new ArrayList();
            OpnameAsset opm = new OpnameAsset();
            opm.Field = "GroupID,KodeGroup,NamaGroup";
            opm.Criteria = " and GroupID=" + ddlGroupAsset.SelectedValue.ToString();
            opm.Criteria += " and LokasiID=" + asm.LokasiID;
            opm.Option = OpnameAsset.Operasi.Group;
            opm.GroupBy = " Group By GroupID,KodeGroup,NamaGroup";
            opm.OrderBy = " Order by KodeGroup,NamaGroup";
            arrData = opm.Retrieve();
            lstLokasi.DataSource = arrData;
            lstLokasi.DataBind();
        }
        protected void lstGroupAsset_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater lstLokasi = (Repeater)e.Item.FindControl("lstKelas");
            AssetManagement asm = (AssetManagement)e.Item.DataItem;
            AssetManagement lok = (AssetManagement)((RepeaterItem)e.Item.Parent.Parent).DataItem;
            ArrayList arrData = new ArrayList();
            OpnameAsset opm = new OpnameAsset();
            opm.Field = "ClassID,KodeClass,NamaClass";
            opm.Criteria = " and GroupID=" + ddlGroupAsset.SelectedValue.ToString();
            opm.Criteria += " and LokasiID=" + lok.LokasiID;
            opm.Option = OpnameAsset.Operasi.Kelas;
            opm.GroupBy = " Group By ClassID,KodeClass,NamaClass";
            opm.OrderBy = " Order by KodeClass,NamaClass";
            arrData = opm.Retrieve();
            lstLokasi.DataSource = arrData;
            lstLokasi.DataBind();
        }
        protected void lstKelas_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater lstLokasi = (Repeater)e.Item.FindControl("lstSubKlass");
            AssetManagement asm = (AssetManagement)e.Item.DataItem;
            AssetManagement lok = (AssetManagement)((RepeaterItem)e.Item.Parent.Parent.Parent.Parent).DataItem;
            AssetManagement klas = (AssetManagement)((RepeaterItem)e.Item.Parent.Parent).DataItem;
            ArrayList arrData = new ArrayList();
            OpnameAsset opm = new OpnameAsset();
            opm.Field = "SubClassID,KodeSubClass,NamaSubClass";
            opm.Criteria = " and GroupID=" + ddlGroupAsset.SelectedValue.ToString();
            opm.Criteria += " and LokasiID=" + lok.LokasiID;
            opm.Criteria += " and ClassID=" + asm.ClassID;
            opm.Option = OpnameAsset.Operasi.SubKelas;
            opm.GroupBy = " Group By SubClassID,KodeSubClass,NamaSubClass";
            opm.OrderBy = " Order by KodeSubClass,NamaSubClass";
            arrData = opm.Retrieve();
            lstLokasi.DataSource = arrData;
            lstLokasi.DataBind();
        }
        protected void lstSubKlass_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater lstLokasi = (Repeater)e.Item.FindControl("lstDetails");
            AssetManagement asm = (AssetManagement)e.Item.DataItem;
            AssetManagement lok = (AssetManagement)((RepeaterItem)e.Item.Parent.Parent.Parent.Parent.Parent.Parent).DataItem;
            AssetManagement klas = (AssetManagement)((RepeaterItem)e.Item.Parent.Parent).DataItem;
            ArrayList arrData = new ArrayList();
            OpnameAsset opm = new OpnameAsset();
            opm.Field = "*";
            opm.Criteria = " and GroupID=" + ddlGroupAsset.SelectedValue.ToString();
            opm.Criteria += " and LokasiID=" + lok.LokasiID;
            opm.Criteria += " and ClassID=" + klas.ClassID;
            opm.Criteria += " and SubClassID=" + asm.SubClassID;
            opm.Option = OpnameAsset.Operasi.Detail;
            opm.GroupBy = "";
            opm.OrderBy = " Order by KodeAsset,GroupID,ClassID,SubClassID";
            arrData = opm.Retrieve();
            lstLokasi.DataSource = arrData;
            lstLokasi.DataBind();
        }
        protected void btnOpname_Click(object sender, EventArgs e)
        {
            if (ddlGroupAsset.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Group Asset Harus Dipilih dahulu");
                return;
            }
            LoadBaseLokasi();
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=CountSheetAsset.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "Asset Group :" + ddlGroupAsset.SelectedItem.Text.ToString().ToUpper();
            string HtmlEnd = "";
            lstOpname.RenderControl(hw);
            string Contents = sw.ToString();
            //Contents = Contents.Replace("collapse\">", "collapse\" border=\"1\">");
            Contents = Contents.Replace("cls\">", "\">'");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }
    }

    public class OpnameAsset
    {
        ArrayList arrData = new ArrayList();
        public enum Operasi { Lokasi, Group, Kelas, SubKelas, Detail }
        public string Field { get; set; }
        public string Criteria { get; set; }
        public string GroupBy { get; set; }
        public Operasi Option { get; set; }
        public string OrderBy { get; set; }

        private string Query()
        {
            string result = string.Empty;
            result = "select " + this.Field + " from AM_Asset_v where RowStatus>-1 " + this.Criteria + this.GroupBy + this.OrderBy;
            return result;
        }
        private AssetManagement gObject(SqlDataReader sdr)
        {
            AssetManagement asm = new AssetManagement();
            switch (this.Option)
            {
                case Operasi.Lokasi:
                    asm.LokasiID = (sdr["LokasiID"] != DBNull.Value) ? Convert.ToInt32(sdr["LokasiID"].ToString()) : 0;
                    asm.NamaLokasi = sdr["NamaLokasi"].ToString();
                    asm.KodeLokasi = sdr["KodeLokasi"].ToString();
                    break;
                case Operasi.Group:
                    asm.GroupID = Convert.ToInt32(sdr["GroupID"].ToString());
                    asm.KodeGroup = sdr["KodeGroup"].ToString();
                    asm.NamaGroup = sdr["NamaGroup"].ToString();
                    break;
                case Operasi.Kelas:
                    asm.ClassID = Convert.ToInt32(sdr["ClassID"].ToString());
                    asm.NamaClass = sdr["NamaClass"].ToString();
                    asm.KodeClass = sdr["KodeClass"].ToString();
                    break;
                case Operasi.SubKelas:
                    asm.SubClassID = Convert.ToInt32(sdr["SubClassID"].ToString());
                    asm.NamaSubClass = sdr["NamaSubClass"].ToString();
                    asm.KodeSubClass = sdr["KodeSubClass"].ToString();
                    break;
                case Operasi.Detail:
                    asm.ID = (sdr["ID"] != DBNull.Value) ? Convert.ToInt32(sdr["ID"].ToString()) : 0;
                    asm.KodeAsset = sdr["KodeAsset"].ToString();
                    asm.ItemKode = sdr["ItemKode"].ToString();
                    asm.Deskripsi = sdr["NamaAsset"].ToString();
                    asm.NilaiAsset = (sdr["ValAsset"] != DBNull.Value) ? Convert.ToDecimal(sdr["ValAsset"].ToString()) : 0;
                    asm.TglAsset = (sdr["AssyDate"] != DBNull.Value) ? Convert.ToDateTime(sdr["AssyDate"].ToString()) : DateTime.MinValue;
                    asm.PicPerson = sdr["PICPerson"].ToString();
                    break;
            }
            return asm;
        }
        public ArrayList Retrieve()
        {
            try
            {
                arrData = new ArrayList();
                DataAccess da = new DataAccess(Global.ConnectionString());
                string strsql = this.Query();
                SqlDataReader sdr = da.RetrieveDataByString(this.Query());
                if (da.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(gObject(sdr));
                    }
                }
                return arrData;
            }
            catch (Exception ex)
            {
                string rst = ex.Message;
                return arrData;
            }
        }

    }
}