using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;

namespace GRCweb1.Modul.ISO_UPD
{
    public partial class LapMasterDok : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.link = "~/Default.aspx";
        }

        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            string strError = string.Empty;
            string Deptm = ddlDept.SelectedValue;
            string pilihMaster = ddlMaster.SelectedValue;


            ReportUPDFacade reportFacade = new ReportUPDFacade();
            string strQuery = string.Empty; string Deptm2 = string.Empty; string DeptNama = string.Empty;
            if (ddlDept.SelectedItem.ToString() == "Direksi")
            {
                DeptNama = "Direksi"; Deptm = "23";
            }
            else if (ddlDept.SelectedItem.ToString() == "Plant Manager")
            {
                DeptNama = "Plant Manager"; Deptm = "23";
            }
            else
            {
                DeptNama = "";
            }

            strQuery = reportFacade.ViewLapMasterDok(Deptm, pilihMaster, DeptNama);

            Session["Query"] = strQuery;
            //if (ddlDept.SelectedValue == "100")
            //{
            //    Session["Deptm"] = "DIREKSI";
            //}
            //else if (ddlDept.SelectedValue == "101")
            //{
            //    Session["Deptm"] = "PLANT MANAGER";
            //}
            //else
            //{
            //    Session["Deptm"] = ddlDept.SelectedItem.Text;
            //}
            Session["Deptm"] = ddlDept.SelectedItem.Text;
            Session["pilihMaster"] = ddlMaster.SelectedItem.Text;

            Cetak(this);
        }

        static public void Cetak(Control page)
        {
            string myScript = "var wn = window.showModalDialog('../Report/ReportUPD.aspx?IdReport=LapMasterDok', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

    }
}