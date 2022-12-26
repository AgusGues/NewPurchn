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

namespace GRCweb1.Modul.Purchasing
{
    public partial class KertasKadarAirList4PO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                string[] KertasPO = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("POKertas", "DepoKertas").Split(',');
                int boleh = Array.IndexOf(KertasPO, ((Users)Session["Users"]).UserID.ToString());
                if (boleh > -1)
                {
                    LoadKA4PO();
                }
                else
                {
                    rFilter.SelectedIndex = 1;
                    LoadKA4PO(true);
                }
            }

        }

        protected void rFilter_Change(object sender, EventArgs e)
        {
            switch (rFilter.SelectedIndex)
            {
                case 0: LoadKA4PO(); break;
                case 1: LoadKA4PO(true); break;
            }
        }
        private void LoadKA4PO(bool p)
        {
            ArrayList arrData = new ArrayList();
            DepoKertas dk = new DepoKertas();
            dk.Criteria = "WHERE PlantID=" + ((Users)Session["Users"]).UnitKerjaID;
            arrData = dk.Retrieve4PO(true);
            lstKA.DataSource = arrData;
            lstKA.DataBind();
        }

        private void LoadKA4PO()
        {
            ArrayList arrData = new ArrayList();
            DepoKertas dk = new DepoKertas();
            dk.Criteria = "WHERE PlantID=" + ((Users)Session["Users"]).UnitKerjaID;
            arrData = dk.Retrieve4PO();
            lstKA.DataSource = arrData;
            lstKA.DataBind();
        }
        protected void lstKA_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            string cmd = e.CommandName.ToString();
            int id = int.Parse(e.CommandArgument.ToString());
            HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("listed");
            Image po = (Image)e.Item.FindControl("btnPO");
            switch (cmd)
            {
                case "po":
                    if (Session["Result"] != null)
                    {
                        string[] result = Session["Result"].ToString().Split(',');
                        tr.Cells[11].InnerHtml = result[0];
                        tr.Cells[10].InnerHtml = result[1];
                        po.Visible = false;
                        Session["Result"] = null;
                    }
                    else
                    {
                        po.Visible = true;
                    }
                    break;
            }
        }
        protected void lstKA_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Users user = (Users)Session["Users"];
            DeliveryKertas d = (DeliveryKertas)e.Item.DataItem;
            string[] KertasPO = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("POKertas", "DepoKertas").Split(',');
            int boleh = Array.IndexOf(KertasPO, ((Users)Session["Users"]).UserID.ToString());
            Image po = (Image)e.Item.FindControl("btnPO");
            po.Attributes.Add("onclick", "CreatePO('" + d.ID + "-" + d.RowStatus + "-" + d.SchID + "-" + d.Jumlah + "')");
            po.Visible = (d.SupplierID == 0) ? false : true;
            po.Visible = (boleh > -1) ? po.Visible : false;
            po.Visible = (d.POKAID > 0) ? false : po.Visible;
            HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("listed");
            tr.Cells[11].InnerHtml = d.NoPO;
            tr.Cells[10].InnerHtml = d.NOSPP;
            QAKadarAir ka = new QAKadarAir();
            ka = KAPlant("AND NoSJ='" + d.NoSJ + "' AND NOPOL='" + d.NOPOL + "' AND PlantID=" + user.UnitKerjaID + " AND ItemCode='" + d.ItemCode + "'", true);
            tr.Cells[1].InnerHtml = (d.RowStatus == 1) ? ka.TglCheck.ToString("dd/MM/yyyy") : d.TglKirim.ToString("dd/MM/yyyy");
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "MyScript", myScript, true);
        }
        private QAKadarAir KAPlant(string Criteria, bool KAPlant)
        {
            DepoKertasKA d = new DepoKertasKA();
            QAKadarAir dk = new QAKadarAir();
            dk = d.Retrieve(Criteria, true);
            return dk;
        }
    }
}