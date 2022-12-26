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
using BusinessFacade;
using Domain;

namespace GRCweb1.Modul.Budgeting
{
    public partial class MaterialCCgroup1 : System.Web.UI.Page
    {
        private CostCenterFacade b = new CostCenterFacade();
        private string linkID { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";

                LoadDeptCost();
                Session["MatGroup"] = null;
                Users user = (Users)Session["Users"];
                if ((Users)Session["Users"] == null) { Response.Redirect("~/Default.aspx"); }
                switch (user.DeptID)
                {
                    case 2: ddlDept.SelectedValue = "1"; ddlDept.Enabled = false; break;
                    case 3: ddlDept.Enabled = false; ddlDept.SelectedValue = "2"; break;
                    case 4:
                    case 5:
                    case 18:
                    case 19: ddlDept.Enabled = false; ddlDept.SelectedValue = "4"; break;
                    case 10: ddlDept.Enabled = false; ddlDept.SelectedValue = "3"; break;
                    case 14: ddlDept.Enabled = true; break;
                }
                string mode = (Request.QueryString["id"] != null) ? Request.QueryString["id"].ToString() : "";
                linkID = mode;
                switch (mode)
                {
                    case "2":
                        forInv.Visible = false;
                        forPJ.Visible = true;
                        forPJ1.Visible = true;
                        ddlDept.SelectedValue = "2";
                        ddlDept_Change(null, null);
                        forPJ2.Visible = true;
                        txtJudul.Text = "PARTNO COST CENTER GROUP";
                        break;
                    default:
                        forInv.Visible = true;
                        forPJ.Visible = false;
                        forPJ1.Visible = false;
                        ddlDept.SelectedValue = "3";
                        ddlDept_Change(null, null);
                        forPJ2.Visible = false;
                        txtJudul.Text = "MATERIAL COST CENTER GROUP";
                        break;
                }
            }
        }

        private void LoadDeptCost()
        {
            try
            {
                b.GetCostCenter(ddlDept);
                //ddlDept.SelectedValue = ((Users)Session["Users"]).DeptID.ToString();
            }
            catch { }
        }
        private void LoadMaterialCC()
        {
            ArrayList arrData = new ArrayList();
            arrData = b.GetMaterialGroup(ddlDept.SelectedValue);
            ddlMatGroupCC.Items.Clear();
            ddlMatGroupCC.Items.Add(new ListItem("--Pilih--", "0"));
            foreach (CostCenter c in arrData)
            {
                ddlMatGroupCC.Items.Add(new ListItem(c.GroupName, c.ID.ToString()));
            }
        }
        protected void ddlMatGroupCC_Change(object sender, EventArgs e)
        {
            string mode = (Request.QueryString["id"] != null) ? Request.QueryString["id"].ToString() : "";
            ArrayList arrData = (ArrayList)Session["MatGroup"];
            arrData = b.GetMaterialPPGroup(ddlDept.SelectedValue, ddlMatGroupCC.SelectedValue);
            switch (mode)
            {
                case "1":
                    lstItem.DataSource = arrData;
                    lstItem.DataBind();
                    break;
                default:
                    ddlItemName.Items.Clear();
                    ddlItemName.Items.Add(new ListItem("--Pilih Item Code--", "0"));
                    foreach (CostCenter c in arrData)
                    {
                        ddlItemName.Items.Add(new ListItem(c.ItemName, c.ID.ToString()));
                    }
                    break;
            }
        }
        protected void ddlDept_Change(object sender, EventArgs e)
        {
            LoadMaterialCC();
        }
        protected void ddlItemName_Change(object sender, EventArgs e)
        {
            string mode = (Request.QueryString["id"] != null) ? Request.QueryString["id"].ToString() : "";
            CostCenterFacade cfd = new CostCenterFacade();
            switch (mode)
            {
                case "2":
                    ArrayList arrD = new ArrayList();
                    cfd.Criteria = " AND MatCCMatrixFINId=" + ddlItemName.SelectedValue + " Order By Lokasi,PartNo";
                    arrD = cfd.RetrievePartNo();
                    lstPartNo.Visible = true;
                    lstPartNo.DataSource = arrD;
                    lstPartNo.DataBind();
                    lPartNo.Visible = true;
                    lstItem.Visible = false;
                    lInven.Visible = false;
                    break;
            }
        }
        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            string mode = (Request.QueryString["id"] != null) ? Request.QueryString["id"].ToString() : "";
            ArrayList arrD = (Session["MatGroup"] != null) ? (ArrayList)Session["MatGroup"] : new ArrayList(); ;
            CostCenter cc = new CostCenter();
            CostFIN cf = new CostFIN();
            Inventory inv = new Inventory();
            switch (mode)
            {
                case "2":
                    cf.MatCCMatrixFinID = int.Parse(ddlItemName.SelectedValue);
                    cf.PartNo = txtPathNo.Text;
                    cf.PartNoID = int.Parse(txtPartNoID.Value);
                    cf.Lokasi = txtLokasi.Text;
                    cf.LokasiID = int.Parse(txtLokasiID.Value);
                    cf.CreatedBy = ((Users)Session["Users"]).UserName;
                    cf.CreatedTime = DateTime.Now;
                    arrD.Add(cf);
                    lstPartNo.Visible = true;
                    lPartNo.Visible = true;
                    lInven.Visible = false;
                    lstItem.Visible = false;
                    lstPartNo.DataSource = arrD;
                    lstPartNo.DataBind();
                    break;
                default:
                    if (txtItemID.Value == string.Empty || txtItemID.Value == "0") { txtMaterialName.Focus(); return; }
                    inv = new InventoryFacade().RetrieveById(int.Parse(txtItemID.Value));
                    cc.ItemID = int.Parse(txtItemID.Value);
                    cc.MaterialCCID = int.Parse(ddlDept.SelectedValue.ToString());
                    cc.MaterialGroupID = int.Parse(ddlMatGroupCC.SelectedValue.ToString());
                    cc.ItemTypeID = 1;
                    cc.RowStatus = 0;
                    cc.CreatedBy = ((Users)Session["Users"]).UserName;
                    cc.ItemName = txtMaterialName.Text;
                    cc.ItemCode = inv.ItemCode;// txtItemCode.Text;
                    arrD.Add(cc);
                    lstPartNo.Visible = false;
                    lstItem.Visible = true;
                    lPartNo.Visible = false;
                    lInven.Visible = true;
                    lstItem.DataSource = arrD;
                    lstItem.DataBind();
                    break;
            }
            Session["MatGroup"] = arrD;

            txtMaterialName.Text = "";
            txtPathNo.Text = "";
            txtPartNoID.Value = "0";
            txtItemID.Value = "0";
            txtLokasi.Text = "";
            txtLokasiID.Value = "0";
        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            int res = 0;
            string mode = (Request.QueryString["id"] != null) ? Request.QueryString["id"].ToString() : "";
            if (Session["MatGroup"] != null)
            {
                switch (mode)
                {
                    case "2":
                        ArrayList arrData = (ArrayList)Session["MatGroup"];
                        foreach (CostFIN cfn in arrData)
                        {
                            CostFIN cn = new CostFIN();
                            cn = cfn;
                            res += b.InsertPartNo(cn);
                        }
                        break;
                    default:
                        ArrayList arrDatax = (ArrayList)Session["MatGroup"];
                        foreach (CostCenter cc in arrDatax)
                        {
                            CostCenter c = new CostCenter();
                            c = cc;
                            res += b.Insert(c);
                        }
                        if (res == arrDatax.Count)
                        {
                            txtMaterialName.Text = "";
                            Session["MatGroup"] = null;
                            ddlMatGroupCC_Change(null, null);
                        }
                        break;
                }
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Session["MatGroup"] = null;
            Response.Redirect("MaterialCCGroup.aspx");
        }
        protected void lstItem_DataBound(object sender, RepeaterItemEventArgs e)
        {
            CostCenter cc = (CostCenter)e.Item.DataItem;
            ((Image)e.Item.FindControl("hpus")).Visible = (cc.ItemID == 0) ? true : false;
            ((Image)e.Item.FindControl("del")).Visible = (cc.ItemID > 0) ? true : false;
        }
        protected void lstItem_Command(object sender, RepeaterCommandEventArgs e)
        {
            int idx = int.Parse(e.CommandArgument.ToString());
            CostCenterFacade cfd = new CostCenterFacade();
            switch (e.CommandName)
            {
                case "hps":
                    CostPJ cf = new CostPJ();
                    cf.ID = idx;
                    cf.RowStatus = -1;
                    cf.LastModifiedBy = ((Users)Session["Users"]).UserName;
                    cf.LastModifiedTime = DateTime.Now;
                    int res = cfd.Update(cf);
                    break;
                case "hpse":
                    ArrayList arr = (ArrayList)Session["MatGroup"];
                    arr.RemoveAt(idx);
                    lstPartNo.DataSource = arr;
                    lstPartNo.DataBind();
                    break;
            }
        }
        protected void lstPartNo_DataBound(object sender, RepeaterItemEventArgs e)
        {
            CostFIN cc = (CostFIN)e.Item.DataItem;
            ((Image)e.Item.FindControl("hpus")).Visible = (cc.ID == 0) ? true : false;
            ((Image)e.Item.FindControl("del")).Visible = (cc.ID > 0) ? true : false;
        }
        protected void lstPartNo_Command(object sender, RepeaterCommandEventArgs e)
        {
            int idx = int.Parse(e.CommandArgument.ToString());
            CostCenterFacade cfd = new CostCenterFacade();
            switch (e.CommandName)
            {
                case "hps":
                    CostFIN cf = new CostFIN();
                    cf.ID = idx;
                    cf.RowStatus = -1;
                    cf.CreatedBy = ((Users)Session["Users"]).UserName;
                    cf.CreatedTime = DateTime.Now;
                    int res = cfd.InsertPartNo(cf, true);
                    if (res > 0)
                    {
                        ArrayList arrD = new ArrayList();
                        cfd.Criteria = " AND MatCCMatrixFINID=" + ddlItemName.SelectedValue + " Order By Lokasi,PartNo";
                        arrD = cfd.RetrievePartNo();
                        lstPartNo.Visible = true;
                        lstPartNo.DataSource = arrD;
                        lstPartNo.DataBind();
                        lPartNo.Visible = true;
                    }
                    break;
                case "hpse":
                    ArrayList arr = (ArrayList)Session["MatGroup"];
                    arr.RemoveAt(idx);
                    lstPartNo.DataSource = arr;
                    lstPartNo.DataBind();
                    break;
            }
        }

    }
}