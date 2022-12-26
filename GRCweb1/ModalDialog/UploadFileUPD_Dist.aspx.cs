using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using System.Net;

using System.Runtime.InteropServices;

namespace GRCweb1.ModalDialog
{
    public partial class UploadFileUPD_Dist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Users users = (Users)Session["Users"];
                string IDmaster = (Request.QueryString["ba"] != null) ? Request.QueryString["ba"].ToString() : "";
                Session["IDmaster"] = IDmaster;
                LoadDept();
                LoadDistribusi();
            }
        }

        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            ISO_UPD2Facade facade01 = new ISO_UPD2Facade();
            ISO_UpdDMD domain01 = new ISO_UpdDMD();

            ISO_UPD2Facade facade02 = new ISO_UPD2Facade();
            ISO_UpdDMD domain02 = new ISO_UpdDMD();
            domain02 = facade02.RetrieveDataDistribusiLama(Session["IDmaster"].ToString());
            Session["DeptID"] = domain02.DeptID;
            Session["CategoryUPD"] = domain02.CategoryUPD;
            Session["Urutan"] = domain02.Urutan;

            int intResult01 = 0;
            domain01.IDmaster = Convert.ToInt32(Session["IDmaster"]);
            intResult01 = facade01.HapusRecord(domain01);


            ArrayList arrDept = (ArrayList)Session["ListOfDept"];
            int i = 0;
            foreach (ISO_UpdDMD DeptList in arrDept)
            {
                CheckBox cb = (CheckBox)GridView1.Rows[i].Cells[1].FindControl("check");
                if (cb.Checked)
                {
                    int DeptIDF = (int.Parse(GridView1.Rows[i].Cells[0].Text)) == 19 ? 4 : int.Parse(GridView1.Rows[i].Cells[0].Text);

                    domain01.ID = DeptIDF;
                    domain01.IDmaster = Convert.ToInt32(Session["IDmaster"]);
                    domain01.Kategory = Convert.ToInt32(Session["CategoryUPD"]);
                    domain01.Dept2 = Convert.ToInt32(Session["DeptID"]);
                    domain01.Urutan = Session["Urutan"].ToString();

                    int intResult = 0;
                    intResult = facade01.InsertD(domain01);

                    if (facade01.Error != string.Empty)
                    {
                        break;
                    }
                }

                i = i + 1;
            }

            CloseWindow(this);
        }

        private void LoadDistribusi()
        {
            ISO_UPD2Facade facadeDist = new ISO_UPD2Facade();
            ArrayList arrData = facadeDist.RetrieveData(Session["IDmaster"].ToString());

            if (arrData.Count > 0)
            {
                LoadDistribusi(arrData);
            }

        }

        private void LoadDistribusi(ArrayList arrData)
        {
            ArrayList arrListData = (ArrayList)Session["ListOfDept"];

            foreach (ISO_UpdDMD List in arrData)
            {
                int i = 0;
                foreach (ISO_UpdDMD List2 in arrListData)
                {
                    CheckBox cb = (CheckBox)GridView1.Rows[i].FindControl("check");
                    if (List2.ID == Convert.ToInt32(List.Deptm))
                    {
                        cb.Checked = true;
                        break;
                    }

                    i = i + 1;
                }
            }
        }

        private void LoadDept()
        {
            ISO_UPD2Facade updF = new ISO_UPD2Facade();
            ArrayList arrUPD = updF.RetrieveDept();
            if (updF.Error == string.Empty)
            {
                Session["ListOfDept"] = arrUPD;
                GridView1.DataSource = arrUPD;
                GridView1.DataBind();
            }
        }

        protected void ChkAll_CheckedChanged(object sender, EventArgs e)
        {
            ArrayList arrUPD = (ArrayList)Session["ListOfDept"];
            int i = 0;
            if (ChkAll.Checked == true)
            {
                foreach (ISO_UpdDMD updDMD in arrUPD)
                {
                    CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("check");
                    chk.Checked = true;
                    i = i + 1;
                }

            }
            else
            {
                foreach (ISO_UpdDMD updDMD in arrUPD)
                {
                    CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("check");
                    chk.Checked = false;
                    i = i + 1;
                }
            }

        }

        protected void Chk01_CheckedChanged(object sender, EventArgs e)
        {
            //if (Chk01.Checked == true)
            //{
            //    Chk01.Checked = true;
            //}
            //else if (Chk01.Checked == false)
            //{
            //    Chk01.Checked = false;
            //}
        }

        private void LoadDeptList()
        {
            ArrayList arrUPD = (ArrayList)Session["ListOfDept"];

            int i = 0;

            foreach (ISO_UpdDMD DeptList in arrUPD)
            {
                CheckBox cb = (CheckBox)GridView1.Rows[i].FindControl("check");
                if (DeptList.ID > 0)
                {
                    cb.Checked = true;
                    break;
                }

                i = i + 1;
            }
        }





        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }


        static public void CloseWindow(Control page)
        {
            string myScript = "window.close();";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "MyScript", myScript, true);
        }
    }
}