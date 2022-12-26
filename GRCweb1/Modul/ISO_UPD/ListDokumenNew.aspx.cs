using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using BusinessFacade;
using Domain;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace GRCweb1.Modul.ISO_UPD
{
    public partial class ListDokumenNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Users users = (Users)Session["Users"];

                ISO_UpdMasterDoc domain1 = new ISO_UpdMasterDoc();
                ISO_UpdMasterDocFacade facade1 = new ISO_UpdMasterDocFacade();
                domain1 = facade1.RetrieveDataListReport(users.ID);

                if (domain1.DeptIDstring != null)
                {
                    ISO_UpdMasterDoc domain = new ISO_UpdMasterDoc();
                    ISO_UpdMasterDocFacade facade = new ISO_UpdMasterDocFacade();
                    domain = facade.RetrieveDataDept(domain1.DeptIDstring);

                    if (users.DeptID > 0)
                    {
                        if (users.DeptID == 27 || users.DeptID == 23 || users.DeptID == 14 || users.DeptID == 7 || users.DeptID == 0)
                        {
                            LoadDept();
                            LoadDept0();
                        }
                        else if (domain1.DeptIDstring != string.Empty)
                        {
                            LoadDeptNew(domain1.DeptIDstring.Trim().ToString());
                            LoadDeptNew0(domain1.DeptIDstring.Trim().ToString());
                        }
                        else
                        {
                            LoadDept2();
                            LoadDept0();
                        }
                    }
                }
                else
                {
                    if (users.DeptID > 0 || users.DeptID == 0 && users.Apv > 2)
                    {
                        if (users.DeptID == 27 || users.DeptID == 23 || users.DeptID == 14 || users.DeptID == 7 || users.DeptID == 0)
                        {
                            LoadDept();
                            LoadDept0();
                        }
                        else
                        {
                            LoadDept2();
                            LoadDept0();
                        }
                    }
                }

                //if (users.DeptID > 0)
                //{
                //    if (users.DeptID == 23 || users.DeptID == 14 || users.DeptID == 7)
                //    {
                //        LoadDept();
                //        LoadDept0();
                //    }
                //    else if (domain1.DeptIDstring != string.Empty)
                //    {
                //        LoadDeptNew(domain1.DeptIDstring.Trim().ToString());
                //        LoadDeptNew0(domain1.DeptIDstring.Trim().ToString());
                //    }
                //    else 
                //    { 
                //        LoadDept2();
                //        LoadDept0();
                //    }
                //}

                LoadJenis();
            }
            try
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('thr', 400, 100 , 20 ,false); </script>", false);
            }
            catch { }
        }

        private void LoadDept2()
        {
            ArrayList arrDept = new ArrayList();
            ISO_UpdMasterDocFacade masterDFacade = new ISO_UpdMasterDocFacade();
            arrDept = masterDFacade.GetNamaDept(((Users)Session["Users"]).DeptID);
            foreach (ISO_UpdMasterDoc dept in arrDept)
            {
                ddlDept.Items.Add(new ListItem(dept.namaDept, dept.idDept.ToString()));
            }
        }
        private void LoadDept20()
        {
            ArrayList arrDept = new ArrayList();
            ISO_UpdMasterDocFacade masterDFacade = new ISO_UpdMasterDocFacade();
            arrDept = masterDFacade.GetNamaDept(((Users)Session["Users"]).DeptID);
            foreach (ISO_UpdMasterDoc dept in arrDept)
            {
                ddlDept0.Items.Add(new ListItem(dept.namaDept, dept.idDept.ToString()));
            }
        }

        private void LoadDept()
        {
            ArrayList arrDept = new ArrayList();
            ISO_UpdMasterDocFacade masterDFacade = new ISO_UpdMasterDocFacade();
            arrDept = masterDFacade.RetrieveDept();
            ddlDept.Items.Add(new ListItem("---- Pilih ----", "0"));
            foreach (ISO_UpdMasterDoc dept in arrDept)
            {
                ddlDept.Items.Add(new ListItem(dept.namaDept, dept.idDept.ToString()));
            }
        }

        private void LoadDeptNew(string DeptID)
        {
            ArrayList arrDept = new ArrayList();
            ISO_UpdMasterDocFacade masterDFacade = new ISO_UpdMasterDocFacade();
            arrDept = masterDFacade.RetrieveDeptNew(DeptID);
            ddlDept.Items.Add(new ListItem("---- Pilih ----", "0"));
            foreach (ISO_UpdMasterDoc dept in arrDept)
            {
                ddlDept.Items.Add(new ListItem(dept.namaDept, dept.idDept.ToString()));
            }
        }

        private void LoadDept0()
        {
            ArrayList arrDept = new ArrayList();
            ISO_UpdMasterDocFacade masterDFacade = new ISO_UpdMasterDocFacade();
            arrDept = masterDFacade.RetrieveDept();
            ddlDept0.Items.Add(new ListItem("---- Pilih ----", "0"));
            foreach (ISO_UpdMasterDoc dept in arrDept)
            {
                ddlDept0.Items.Add(new ListItem(dept.namaDept, dept.idDept.ToString()));
            }
        }

        private void LoadDeptNew0(string DeptID)
        {
            ArrayList arrDept = new ArrayList();
            ISO_UpdMasterDocFacade masterDFacade = new ISO_UpdMasterDocFacade();
            arrDept = masterDFacade.RetrieveDeptNew(DeptID);
            ddlDept0.Items.Add(new ListItem("---- Pilih ----", "0"));
            foreach (ISO_UpdMasterDoc dept in arrDept)
            {
                ddlDept0.Items.Add(new ListItem(dept.namaDept, dept.idDept.ToString()));
            }
        }

        private void LoadJenis()
        {
            ArrayList arrJenis = new ArrayList();
            ISO_UpdMasterDocFacade masterDFacade = new ISO_UpdMasterDocFacade();
            arrJenis = masterDFacade.RetrieveJenis();
            ddlJenis.Items.Add(new ListItem("---- Pilih ----", "0"));
            foreach (ISO_UpdMasterDoc jn in arrJenis)
            {
                ddlJenis.Items.Add(new ListItem(jn.DocCategory, jn.ID.ToString()));
            }
        }

        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            string strError = string.Empty;
            string Deptm1 = ((Users)Session["Users"]).DeptID.ToString();
            string Deptm2 = ddlDept.SelectedValue;
            string Deptm = string.Empty;
            string jenis = ddlJenis.SelectedValue;
            Users users = (Users)Session["Users"];

            if (ddlJenis.SelectedValue == "10" || ddlJenis.SelectedValue == "11")
            {
                //string Query = " order by xx.Urutan,xx.DocName "; Session["Query"] = Query;
                string Query = " ) as DataList ) as DataList2   order by DataList2.NoUrut,DataList2.NoDocument "; Session["Query"] = Query;
                //string QueryNomor = " select ROW_NUMBER() over (order by xx.urutan,xx.DocName asc) as No "; Session["QueryNomor"] = QueryNomor;
                string QueryNomor =
                " select   ROW_NUMBER() over (order by DataList2.CategoryUPD asc) as No,* from (select case  when CategoryUPD='Instruksi Kerja' " +
                " then SUBSTRING(NoDocument,4,3)+SUBSTRING(cast(RIGHT(NoDocument,2) as nchar),1,2)  when CategoryUPD='Bagan Alir' " +
                " then SUBSTRING(cast(RIGHT(NoDocument,2) as nchar),1,2)  when CategoryUPD is NULL then NoDocument else '' end NoUrut,* from ( ";
                Session["QueryNomor"] = QueryNomor;
            }
            else
            {
                //string Query = " order by xx.Urutan,xx.NoDocument "; Session["Query"] = Query;
                string Query = " ) as DataList ) as DataList2   order by DataList2.NoUrut,DataList2.NoDocument "; Session["Query"] = Query;
                //string QueryNomor = " select ROW_NUMBER() over (order by xx.urutan,xx.NoDocument asc) as No "; Session["QueryNomor"] = QueryNomor;
                string QueryNomor =
                " select   ROW_NUMBER() over (order by DataList2.NoUrut,DataList2.NoDocument asc) as No,* from (select case  when CategoryUPD='Instruksi Kerja' " +
                " then SUBSTRING(NoDocument,4,3)+SUBSTRING(cast(RIGHT(NoDocument,2) as nchar),1,2)  when CategoryUPD='Bagan Alir' " +
                " then SUBSTRING(cast(RIGHT(NoDocument,2) as nchar),1,2)  when CategoryUPD is NULL then NoDocument else '' end NoUrut,* from ( ";
                Session["QueryNomor"] = QueryNomor;
            }

            string QueryF = Session["Query"].ToString();
            string QueryNo = Session["QueryNomor"].ToString();

            if (users.DeptID == 23 || users.DeptID == 14 || users.ID == 326 && users.UnitKerjaID == 1 || users.ID == 428 && users.UnitKerjaID == 7)
            { Deptm = Deptm2; }
            else
            { Deptm = Deptm1; }
            string deptid = ddlDept0.SelectedValue;
            ISO_UPD2Facade updF = new ISO_UPD2Facade();
            ArrayList arrUPD = updF.RetrieveDok2(Deptm, jenis, users.DeptID, deptid, QueryF, QueryNo);
            ISO_UpdDMD up = new ISO_UpdDMD();

            Session["ListOfDokumen"] = arrUPD;
            lstBA.DataSource = arrUPD;
            lstBA.DataBind();
        }

        protected void lstBA_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Users users = (Users)Session["Users"];

            ISO_UpdDMD ba = (ISO_UpdDMD)e.Item.DataItem;
            Image view = (Image)e.Item.FindControl("view");
            Image att = (Image)e.Item.FindControl("att");
            Image hapus = (Image)e.Item.FindControl("hapus");
            Image edit = (Image)e.Item.FindControl("edit");
            Image reUpload = (Image)e.Item.FindControl("ReUpload");

            if (users.DeptID == 23 && ba.ID != 0)
            { att.Visible = true; hapus.Visible = true; }
            else
            { att.Visible = false; hapus.Visible = false; }


            view.Attributes.Add("onclick", "PreviewPDF('" + ba.ID.ToString() + "')");
            att.Attributes.Add("onclick", "OpenDialog('" + ba.ID.ToString() + "')");
            edit.Attributes.Add("onclick", "OpenDialog2('" + ba.IDm.ToString() + "')");
            reUpload.Attributes.Add("onclick", "OpenDialog3('" + ba.IDm.ToString() + "')");

            //LinkButton lbn1 = FindControl("LinkButton1") as LinkButton;
            //ScriptManager.GetCurrent(this).RegisterPostBackControl(lbn1);


        }

        protected void lstBA_Command(object sender, RepeaterCommandEventArgs e)
        {
            try
            {
                Repeater rpt = (Repeater)sender;
                Image pre = (Image)rpt.Items[int.Parse(e.CommandArgument.ToString())].FindControl("hapus");
                string deptid = ddlDept0.SelectedValue;
                switch (e.CommandName)
                {
                    case "pre":
                        break;

                    case "hps":

                        ISO_UpdDMD wo = new ISO_UpdDMD();
                        ISO_UPD2Facade wof = new ISO_UPD2Facade();
                        wo.ID = Convert.ToInt32(pre.CssClass);
                        int ID = wo.ID;
                        int intResult = 0;

                        intResult = wof.Hapus(wo);
                        //LoadListAttachment(woid, rpt);
                        if (intResult > 0)
                        {
                            string Query = " order by xx.Urutan,xx.NoDocument ";
                            string QueryNomor = " ROW_NUMBER() over (order by xx.urutan,xx.NoDocument asc) as No ";

                            ArrayList arrData = new ArrayList();
                            Users users = (Users)Session["Users"];
                            ISO_UpdDMD upd1 = new ISO_UpdDMD();
                            ISO_UPD2Facade updF1 = new ISO_UPD2Facade();

                            string Deptm = string.Empty;
                            string jenis = string.Empty;

                            Deptm = ddlDept.SelectedValue;
                            jenis = ddlJenis.SelectedValue;

                            arrData = updF1.RetrieveDok2(Deptm, jenis, users.DeptID, deptid, Query, QueryNomor);
                            lstBA.DataSource = arrData;
                            lstBA.DataBind();

                        }

                        break;
                }
            }
            catch { }
        }

        protected void ddlDept_Change(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];

            ArrayList arrData = new ArrayList();
            ISO_UpdDMD upd = new ISO_UpdDMD();
            ISO_UPD2Facade updF = new ISO_UPD2Facade();

            ISO_UpdDMD upd2 = new ISO_UpdDMD();
            ISO_UPD2Facade updF2 = new ISO_UPD2Facade();
            int CekUser = updF2.RetrieveUser(users.ID); Session["CekUser"] = CekUser;


            string deptid = ddlDept.SelectedValue;

            //string Query = " order by xx.Urutan,xx.NoDocument ";
            string Query =
            " ) as DataList ) as DataList2 order by CategoryUPD,DeptID,NoUrut,DocName ";
            //string QueryNomor = " ROW_NUMBER() over (order by xx.urutan,xx.NoDocument asc) as No "; 
            string QueryNomor =
            " select ROW_NUMBER() over (order by DataList2.CategoryUPD asc) as No,* from (select case " +
            " when CategoryUPD='Instruksi Kerja' then SUBSTRING(NoDocument,4,3)+SUBSTRING(cast(RIGHT(NoDocument,2) as nchar),1,2) " +
            " when CategoryUPD='Bagan Alir' then SUBSTRING(cast(RIGHT(NoDocument,2) as nchar),1,2) " +
            " when CategoryUPD is NULL then NoDocument else '' end NoUrut,* from ( ";

            if (users.DeptID == 23 && CekUser == 0 || users.DeptID == 14 && CekUser == 0 || users.DeptID == 7 && CekUser == 0 || CekUser > 0
                || users.DeptID == 0 && CekUser == 0)
            {
                string Deptm = string.Empty;
                string jenis = string.Empty;

                Deptm = ddlDept.SelectedValue;
                jenis = ddlJenis.SelectedValue;

                arrData = updF.RetrieveDok2(Deptm, jenis, users.DeptID, deptid, Query, QueryNomor);
                lstBA.DataSource = arrData;
                lstBA.DataBind();
            }
            else
            {
                string Deptm = string.Empty;
                string jenis = string.Empty;

                Deptm = users.DeptID.ToString();
                jenis = ddlJenis.SelectedValue;

                arrData = updF.RetrieveDok2(Deptm, jenis, users.DeptID, deptid, Query, QueryNomor);
                lstBA.DataSource = arrData;
                lstBA.DataBind();
            }
        }

        protected void ddlJenis_Change(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];

            ArrayList arrData = new ArrayList();
            ISO_UpdDMD upd = new ISO_UpdDMD();
            ISO_UPD2Facade updF = new ISO_UPD2Facade();

            ISO_UpdDMD upd2 = new ISO_UpdDMD();
            ISO_UPD2Facade updF2 = new ISO_UPD2Facade();
            int CekUser = updF2.RetrieveUser(users.ID);
            Session["CekUser"] = CekUser;

            string deptid2 = ddlDept0.SelectedValue;
            string deptid = ddlDept.SelectedValue;

            if (ddlJenis.SelectedValue == "10" || ddlJenis.SelectedValue == "11")
            {
                //string Query = " order by xx.Urutan,xx.DocName "; Session["Query"] = Query;
                string Query =
                " ) as DataList ) as DataList2 order by CategoryUPD,DeptID,NoUrut,DocName "; Session["Query"] = Query;
                //string QueryNomor = " ROW_NUMBER() over (order by xx.urutan,xx.DocName asc) as No "; Session["QueryNomor"] = QueryNomor;
                string QueryNomor =
                " select ROW_NUMBER() over (order by DataList2.CategoryUPD asc) as No,* from (select case " +
                " when CategoryUPD='Instruksi Kerja' then SUBSTRING(NoDocument,4,3)+SUBSTRING(cast(RIGHT(NoDocument,2) as nchar),1,2) " +
                " when CategoryUPD='Bagan Alir' then SUBSTRING(cast(RIGHT(NoDocument,2) as nchar),1,2) " +
                " when CategoryUPD is NULL then NoDocument else '' end NoUrut,* from ( ";
                ; Session["QueryNomor"] = QueryNomor;
            }
            else
            {
                //string Query = " order by xx.Urutan,xx.NoDocument "; Session["Query"] = Query;
                string Query =
                " ) as DataList ) as DataList2 order by CategoryUPD,DeptID,NoUrut,DocName "; Session["Query"] = Query;
                //string QueryNomor = " ROW_NUMBER() over (order by xx.urutan,xx.NoDocument asc) as No "; Session["QueryNomor"] = QueryNomor;
                string QueryNomor =
                " select ROW_NUMBER() over (order by DataList2.CategoryUPD asc) as No,* from (select case " +
                " when CategoryUPD='Instruksi Kerja' then SUBSTRING(NoDocument,4,3)+SUBSTRING(cast(RIGHT(NoDocument,2) as nchar),1,2) " +
                " when CategoryUPD='Bagan Alir' then SUBSTRING(cast(RIGHT(NoDocument,2) as nchar),1,2) " +
                " when CategoryUPD is NULL then NoDocument else '' end NoUrut,* from ( ";
                ; Session["QueryNomor"] = QueryNomor;
            }

            string QueryF = Session["Query"].ToString();
            string QueryNo = Session["QueryNomor"].ToString();

            if (users.DeptID == 23 && ddlDept0.SelectedValue == "0" && CekUser == 0
                || users.DeptID == 14 && ddlDept0.SelectedValue == "0" && CekUser == 0
                || users.DeptID == 7 && ddlDept0.SelectedValue == "0" && CekUser == 0 || CekUser > 0
                || users.DeptID == 0 && ddlDept0.SelectedValue == "0" && CekUser == 0)
            {
                string Deptm = string.Empty;
                string jenis = string.Empty;

                Deptm = ddlDept.SelectedValue;
                jenis = ddlJenis.SelectedValue;

                arrData = updF.RetrieveDok2(Deptm, jenis, users.DeptID, deptid2, QueryF, QueryNo);
                lstBA.DataSource = arrData;
                lstBA.DataBind();
            }

            else if (ddlDept0.SelectedValue != "0" && deptid2 == users.DeptID.ToString())
            {
                string Deptm = string.Empty;
                string jenis = string.Empty;

                Deptm = users.DeptID.ToString();
                jenis = ddlJenis.SelectedValue;

                arrData = updF.RetrieveDok2(Deptm, jenis, users.DeptID, deptid2, QueryF, QueryNo);
                lstBA.DataSource = arrData;
                lstBA.DataBind();
            }

            else if (ddlDept0.SelectedValue != "0" && deptid2 != users.DeptID.ToString())
            {
                string Deptm = string.Empty;
                string jenis = string.Empty;

                //Deptm = deptid2;
                //jenis = ddlJenis.SelectedValue;

                Deptm = ddlDept.SelectedValue;
                jenis = ddlJenis.SelectedValue;

                arrData = updF.RetrieveDok2(Deptm, jenis, users.DeptID, deptid2, QueryF, QueryNo);
                lstBA.DataSource = arrData;
                lstBA.DataBind();
            }

            else
            {
                string Deptm = string.Empty;
                string jenis = string.Empty;

                Deptm = users.DeptID.ToString();
                jenis = ddlJenis.SelectedValue;

                arrData = updF.RetrieveDok2(Deptm, jenis, users.DeptID, deptid2, QueryF, QueryNo);
                lstBA.DataSource = arrData;
                lstBA.DataBind();

            }
        }
    }
}

