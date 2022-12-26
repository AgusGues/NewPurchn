using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;
using System.Text;
using System.Text.RegularExpressions;

namespace GRCweb1.Modul.Master
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";

                //txtPassword.Attributes.Add("value", string.Empty);
                LoadUsers();
            }
        }

        protected void btnChange_ServerClick(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];
            UsersFacade usersFacade = new UsersFacade();
            EncryptPasswordFacade encryptPasswordFacade = new EncryptPasswordFacade();
            string strValidate = ValidateText();
            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }

            users.Password = encryptPasswordFacade.EncryptToString(txtPassword.Text);
            users.UsrMail = txtUsrEmail.Text.Trim();
            users.PssMail = encryptPasswordFacade.EncryptToString(txtPassEmail.Text.Trim());
            int intResult = usersFacade.Update(users);
            if (usersFacade.Error == string.Empty)
            {
                if (intResult > 0)
                    DisplayAJAXMessage(this, "Ubah Data berhasil");
                else
                    DisplayAJAXMessage(this, "Error");
            }
            else
            {
                DisplayAJAXMessage(this, usersFacade.Error);
            }
        }

        public const string MatchEmailPattern =
                 @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
          + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
          + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
          + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

        public static bool IsEmail(string email)
        {
            if (email != null) return Regex.IsMatch(email, MatchEmailPattern);
            else return false;
        }
        private string ValidateText()
        {
            //if (IsEmail(txtUsrEmail.Text) == false)
            //    return "Alamat Email salah";
            if (txtPassword.Text == string.Empty)
                return "Password tidak boleh kosong";
            //if (txtUsrEmail.Text == string.Empty)
            //    return "Alamat E-Mail tidak boleh kosong";
            //if (txtPassEmail.Text == string.Empty)
            //    return "Password Alamat E-Mail tidak boleh kosong";
            return string.Empty;
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private void LoadUsers()
        {
            Users users = (Users)Session["Users"];
            UsersFacade usersFacade = new UsersFacade();
            EncryptPasswordFacade encryptPasswordFacade = new EncryptPasswordFacade();
            //if (users.Password.Trim() != string.Empty)
            //    return;
            //string pasw = encryptPasswordFacade.DecryptString(users.Password.Trim());
            txtUserID.Text = users.UserID;
            txtUserName.Text = users.UserName;
            //txtPassword.Text = pasw;
            txtUsrEmail.Text = users.UsrMail.Trim();
            //string paswm = encryptPasswordFacade.DecryptString(users.PssMail.Trim());
            //txtPassEmail.Text = paswm;
        }

        protected void txtUsrEmail_TextChanged(object sender, EventArgs e)
        {
            if (IsEmail(txtUsrEmail.Text) == false)
            {
                DisplayAJAXMessage(this, "Alamat Email salah");
                txtUsrEmail.Text = string.Empty;
                txtUsrEmail.Focus();
            }
        }

    }
}