using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;


namespace GRCweb1
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Default_PreLoad(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Session.Clear();
            Session.Abandon();

        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnLogin_ServerClick(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(500);
            lblText.Text = "Failed!";

            UsersFacade usersFacade = new UsersFacade();
            Users users = new Users();
            ArrayList arrUsers = usersFacade.Retrieve();
            if (usersFacade.Error == string.Empty)
            {
                users = (Users)arrUsers[0];
                if (users.ID > 0)
                {
                    EncryptPasswordFacade encryptPasswordFacade = new EncryptPasswordFacade();

                    usersFacade = new UsersFacade();
                    users = usersFacade.RetrieveByUserNameAndPassword(txtLogin.Value, encryptPasswordFacade.EncryptToString(txtPassword.Value));
                    if (usersFacade.Error == string.Empty)
                    {
                        if (users.ID > 0)
                        {
                            if (users.CompanyCode.Length == 0)
                            {
                                lblText.Text = "Failed ! Users.CompanyID = 0 ";
                                return;
                            }


                            //GL_ParameterFacade glparamf = new GL_ParameterFacade();
                            //string strperiod = string.Empty;
                            //strperiod = glparamf.retrieveByCode("PERIOD", users.CompanyCode);
                            //if (strperiod.Length > 4)
                            //    users.TmpPeriode = strperiod;
                            //else
                            //{
                            //    //users.TmpPeriode = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0');
                            //    DisplayAJAXMessage(this, "Period Book not define for this Login ");
                            //    return;
                            //}

                            Session["Users"] = users;

                            Response.Redirect("Home.aspx");
                        }
                        else
                            lblText.Text = "Failed !";
                    }
                    else
                        lblText.Text = "Failed !";
                }
            }

        }
        protected void btnLogin1_ServerClick(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(500);
            lblText.Text = "Failed!";

            UsersFacade usersFacade = new UsersFacade();
            Users users = new Users();
            ArrayList arrUsers = usersFacade.Retrieve();
            if (usersFacade.Error == string.Empty)
            {
                users = (Users)arrUsers[0];
                if (users.ID > 0)
                {
                    EncryptPasswordFacade encryptPasswordFacade = new EncryptPasswordFacade();

                    usersFacade = new UsersFacade();
                    users = usersFacade.RetrieveByUserNameAndPassword(txtLogin.Value, encryptPasswordFacade.EncryptToString(txtPassword.Value));
                    if (usersFacade.Error == string.Empty)
                    {
                        if (users.ID > 0)
                        {
                            //if (users.CompanyCode.Length == 0)
                            //{
                            //    lblText.Text = "Failed ! Users.CompanyID = 0 ";
                            //    return;
                            //}

                            
                            Session["Users"] = users;

                            Response.Redirect("Home.aspx");
                        }
                        else
                            lblText.Text = "Failed !";
                    }
                    else
                        lblText.Text = "Failed !";
                }
            }

        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

    }
}