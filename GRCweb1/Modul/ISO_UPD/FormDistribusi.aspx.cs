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
using System.ComponentModel;
using System.Web.SessionState;
using System.Configuration;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DataAccessLayer;

namespace GRCweb1.Modul.ISO_UPD
{
    public partial class FormDistribusi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Session["arrdata"] = null;
                LoadListBA();
                LoadDept();
                LoadJenis();

                btnClose.Visible = true;
                btnRefresh.Visible = false;

                RBShare.Visible = false;
                RBNotShare.Visible = false;
                RBShare.Checked = false;
                RBNotShare.Checked = true;
                tr2.Visible = false; tr1.Visible = true; tr.Visible = true; txtCari.Enabled = false;
            }
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('thr', 400, 100 , 21 ,false); </script>", false);
        }

        private void LoadDept()
        {
            ISO_UpdDMD upd = new ISO_UpdDMD();
            ISO_UPD2Facade updF = new ISO_UPD2Facade();
            ArrayList arrData = updF.RetrieveByDept();

            ddlJD.Items.Clear();
            ddlJD.Items.Add(new ListItem("- Pilih -", "0"));
            foreach (ISO_UpdDMD dept in arrData)
            {
                ddlJD.Items.Add(new ListItem(dept.DeptName, dept.DeptID));
            }
        }

        private void LoadDept2()
        {
            ISO_UpdDMD upd = new ISO_UpdDMD();
            ISO_UPD2Facade updF = new ISO_UPD2Facade();
            ArrayList arrData = updF.RetrieveByDept();

            ddlRubah.Items.Clear();
            ddlRubah.Items.Add(new ListItem("- Pilih -", "0"));
            foreach (ISO_UpdDMD dept in arrData)
            {
                ddlRubah.Items.Add(new ListItem(dept.DeptName, dept.DeptID));
            }
        }

        private void LoadJenis()
        {
            ISO_UpdDMD upd = new ISO_UpdDMD();
            ISO_UPD2Facade updF = new ISO_UPD2Facade();
            ArrayList arrData = updF.RetrieveByJenis();

            ddlJ.Items.Clear();
            ddlJ.Items.Add(new ListItem("- Pilih -", "0"));
            foreach (ISO_UpdDMD jenis in arrData)
            {
                ddlJ.Items.Add(new ListItem(jenis.CategoryUPD, jenis.CategoryID.ToString()));
            }
        }

        private void LoadListBA1()
        {
            string Share = string.Empty;

            if (RBShare.Checked == true)
            {
                Share = "1";
            }
            else if (RBNotShare.Checked == true)
            {
                Share = "2";
            }

            ArrayList arrData = new ArrayList();
            ISO_UpdDMD upd = new ISO_UpdDMD();
            ISO_UPD2Facade updF = new ISO_UPD2Facade();
            Session["arrdata"] = null;
            string aa = ddlJD.SelectedValue;
            string bb = ddlJ.SelectedValue;
            arrData = updF.RetrieveByUPD1(aa, bb, Share);
            lstBA.DataSource = arrData;
            lstBA.DataBind();
            Session["arrdata"] = arrData;
        }

        private void LoadListBAShare()
        {
            ArrayList arrDataS = new ArrayList();
            ISO_UpdDMD upd = new ISO_UpdDMD();
            ISO_UPD2Facade updF = new ISO_UPD2Facade();
            Session["arrdata"] = null;
            arrDataS = updF.RetrieveByUPDShare();
            lstBA.DataSource = arrDataS;
            lstBA.DataBind();
            Session["arrdataS"] = arrDataS;
        }

        private void LoadListBA()
        {
            ArrayList arrData = new ArrayList();
            ISO_UpdDMD upd = new ISO_UpdDMD();
            ISO_UPD2Facade updF = new ISO_UPD2Facade();
            Session["arrdata"] = null;
            arrData = updF.RetrieveByUPD();
            lstBA.DataSource = arrData;
            lstBA.DataBind();
            Session["arrdata"] = arrData;
        }

        private void LoadListBASearch(string Nomor)
        {
            if (RBNama.Checked == true && RBNomor.Checked == false)
            {
                string cara = "Nama"; Session["cara"] = cara;
            }
            else if (RBNomor.Checked == true && RBNama.Checked == false)
            {
                string cara = "Nomor"; Session["cara"] = cara;
            }

            string cara1 = Session["cara"].ToString();

            ArrayList arrData22 = new ArrayList();
            ISO_UpdDMD upd22 = new ISO_UpdDMD();
            ISO_UPD2Facade updF22 = new ISO_UPD2Facade();
            Session["arrdata22"] = null;
            arrData22 = updF22.RetrieveByUPDSearch(Nomor, cara1);
            lstBA.DataSource = arrData22;
            lstBA.DataBind();
            Session["arrdata22"] = arrData22;
        }

        protected void lstBA_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ISO_UpdDMD file = (ISO_UpdDMD)e.Item.DataItem;
            Image att = (Image)e.Item.FindControl("att");
            att.Attributes.Add("onclick", "OpenDialog('" + file.ID.ToString() + "&k=" + file.Kategory.ToString() + "&t=" + file.Type + "&c=" + file.Dept2.ToString() + "')");

        }

        protected void lstBA_Command(object sender, RepeaterCommandEventArgs e)
        {
            ArrayList arrData = (ArrayList)Session["arrdata"];
            switch (e.CommandName)
            {
                case "edit":
                    Image EditMaster = (Image)e.Item.FindControl("EditMaster");
                    PanelEdit.Visible = true; tr2.Visible = true; tr1.Visible = false; tr.Visible = false;
                    //txtID.Text = ((ISO_UpdDMD)arrData[int.Parse(e.CommandArgument.ToString())]).ID.ToString();
                    //txtDokumen.Text = ((ISO_UpdDMD)arrData[int.Parse(e.CommandArgument.ToString())]).DocName.ToString();
                    LoadDept2();
                    int IDMaster = Convert.ToInt32(EditMaster.CssClass);
                    ISO_UpdDMD DomainUPD2 = new ISO_UpdDMD();
                    ISO_UPD2Facade FacadeUPD2 = new ISO_UPD2Facade();
                    int Result2 = 0;
                    DomainUPD2 = FacadeUPD2.RetrieveDataEdit(IDMaster);

                    txtID.Text = IDMaster.ToString();
                    txtDokumen.Text = DomainUPD2.DocName.Trim();
                    txtNo.Text = DomainUPD2.NoDocument.Trim();

                    break;

                case "hps":
                    PanelEdit.Visible = false;
                    Image hps = (Image)e.Item.FindControl("hapus");
                    int ID = Convert.ToInt32(hps.CssClass);

                    ISO_UpdDMD DomainUPD = new ISO_UpdDMD();
                    ISO_UPD2Facade FacadeUPD = new ISO_UPD2Facade();
                    int Result = 0;
                    DomainUPD.ID = ID;

                    Result = FacadeUPD.HapusMasterUPD(DomainUPD);
                    LoadListBA();
                    break;
            }
        }

        protected void ddlFilter_Change(object sender, EventArgs e)
        {
            ArrayList arrData = new ArrayList();
            ISO_UpdDMD upd = new ISO_UpdDMD();
            ISO_UPD2Facade updF = new ISO_UPD2Facade();

            string perintah = string.Empty;
            string Share = string.Empty;

            if (RBShare.Checked == true)
            {
                Share = "1";
            }
            else if (RBNotShare.Checked == true)
            {
                Share = "2";
            }

            if (ddlFilter.SelectedValue == "createdtime")
            {
                perintah = "CreatedTime";
            }

            arrData = updF.RetrieveByUPDFilter(perintah, Share);
            lstBA.DataSource = arrData;
            lstBA.DataBind();

            if (ddlFilter.SelectedValue == "dept")
            {
                ddlJD.Visible = true;
            }
        }

        protected void ddlJD_Change(object sender, EventArgs e)
        {
            if (ddlJD.SelectedValue != null)
            {
                ddlJ.Visible = true;
            }

            ArrayList arrData = new ArrayList();
            ISO_UpdDMD upd = new ISO_UpdDMD();
            ISO_UPD2Facade updF = new ISO_UPD2Facade();
            string Share = string.Empty;

            if (RBShare.Checked == true)
            {
                Share = "1";
            }
            else if (RBNotShare.Checked == true)
            {
                Share = "2";
            }

            string perintah = string.Empty;
            string perintah2 = string.Empty;
            Session["arrdata"] = null;
            perintah = ddlJ.SelectedValue;
            perintah2 = ddlJD.SelectedValue;

            arrData = updF.RetrieveByUPDFilterByDept(perintah, perintah2, Share);
            lstBA.DataSource = arrData;
            lstBA.DataBind();
            Session["arrdata"] = arrData;
        }

        protected void ddlRubah_Change(object sender, EventArgs e)
        {
            if (ddlRubah.SelectedValue != null)
            {
                ddlRubah.Visible = true;
            }
        }

        protected void ddlJ_Change(object sender, EventArgs e)
        {
            ArrayList arrData = new ArrayList();
            ISO_UpdDMD upd = new ISO_UpdDMD();
            ISO_UPD2Facade updF = new ISO_UPD2Facade();
            string Share = string.Empty;

            if (RBShare.Checked == true)
            {
                Share = "1";
            }
            else if (RBNotShare.Checked == true)
            {
                Share = "2";
            }

            string perintah = string.Empty;
            string perintah2 = string.Empty;
            Session["arrdata"] = null;
            perintah = ddlJ.SelectedValue;
            perintah2 = ddlJD.SelectedValue;

            arrData = updF.RetrieveByUPDFilter2(perintah, perintah2, Share);
            lstBA.DataSource = arrData;
            lstBA.DataBind();
            Session["arrdata"] = arrData;

        }

        protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnClose_ServerClick(object sender, EventArgs e)
        {
            LoadListBA1();
            //clearForm();
        }

        protected void btnRefresh_ServerClick(object sender, EventArgs e)
        {
            LoadListBA();
            ddlFilter.Items.Clear();
            ddlFilter.Items.Add(new ListItem("---- Pilih ---", "0"));
            ddlFilter.Items.Add(new ListItem("Yang Terbaru", "createdtime"));
            ddlFilter.Items.Add(new ListItem("Department", "dept"));
            clearForm();
        }

        protected void btnSimpan_ServerClick(object sender, EventArgs e)
        {
            string query = string.Empty;

            if (ddlRubah.SelectedValue == "0")
            {
                query = "update ISO_UpdDMD set docname='" + txtDokumen.Text.Trim() + "',NoDocument='" + txtNo.Text.Trim() + "' where id=" + txtID.Text;
            }
            else if (ddlRubah.SelectedValue != "0")
            {
                query = "update ISO_UpdDMD set docname='" + txtDokumen.Text.Trim() + "',NoDocument='" + txtNo.Text.Trim() + "',Dept=" + ddlRubah.SelectedValue + " where id=" + txtID.Text;
            }
            //string query = "update ISO_UpdDMD set docname='"+ txtDokumen.Text.Trim() +"' where id=" + txtID.Text ;
            UPDDMDFacade updf = new UPDDMDFacade();
            updf.Update(query);
            if (ddlJD.Visible == true)
                LoadListBA1();
            else
                LoadListBA();

            PanelEdit.Visible = false; tr.Visible = true; tr2.Visible = false; tr1.Visible = true;

        }

        protected void btnCari_Click(object sender, EventArgs e)
        {
            btnClose.Visible = false;
            btnRefresh.Visible = true;

            LoadListBASearch(txtCari.Text);
        }

        private void clearForm()
        {
            ddlJD.Visible = false;
            ddlJ.Visible = false;
            txtCari.Text = string.Empty;
        }

        protected void RBShare_CheckedChanged(object sender, EventArgs e)
        {
            LoadListBAShare();
            ddlJD.Visible = false; ddlJ.Visible = false; PanelEdit.Visible = false;
            ddlFilter.Items.Clear();
            ddlFilter.Items.Add(new ListItem("- Pilih -", "0"));
            ddlFilter.Items.Add(new ListItem("Yang Terbaru", "createdtime"));
            ddlFilter.Items.Add(new ListItem("Department", "dept"));
        }

        protected void RBNotShare_CheckedChanged(object sender, EventArgs e)
        {
            LoadListBA();
            ddlJD.Visible = false; ddlJ.Visible = false; PanelEdit.Visible = false;
            ddlFilter.Items.Clear();
            ddlFilter.Items.Add(new ListItem("- Pilih -", "0"));
            ddlFilter.Items.Add(new ListItem("Yang Terbaru", "createdtime"));
            ddlFilter.Items.Add(new ListItem("Department", "dept"));
        }

        protected void RBNama_CheckedChanged(object sender, EventArgs e)
        {
            txtCari.Enabled = true;
        }

        protected void RBNomor_CheckedChanged(object sender, EventArgs e)
        {
            txtCari.Enabled = true;
        }
    }
}

public class UPDDMDFacade
{
    private ArrayList arrData = new ArrayList();
    private UPDDMD dst = new UPDDMD();
    public ArrayList RetrieveUPDDMD(string query)
    {
        arrData = new ArrayList();
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(query);

        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrData.Add(GenerateObject(sdr));
            }
        }
        return arrData;
    }

    public void Update(string query)
    {
        arrData = new ArrayList();
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(query);
    }

    private UPDDMD GenerateObject(SqlDataReader sdr)
    {
        dst = new UPDDMD();
        dst.ID = int.Parse(sdr["ID"].ToString());
        dst.DocName = sdr["DocName"].ToString();
        return dst;
    }
}

public class UPDDMD
{
    public int ID { get; set; }
    public string DocName { get; set; }
}