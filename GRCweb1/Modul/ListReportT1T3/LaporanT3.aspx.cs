using BusinessFacade;
using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.ListReportT1T3
{
    public partial class LaporanT3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ddlBulan.SelectedIndex = int.Parse(DateTime.Now.Month.ToString()) - 1;
                ddlTahun.Text = DateTime.Now.Year.ToString();
                txtTgl.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                txtCut.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                btn1.Disabled = false;
                //btn2.Disabled = false;
                ddlS.Enabled = false;
                txtPartnoA.Enabled = false;
                ddlBulan.Enabled = false;
                ddlTahun.Enabled = false;
                LoadStoker();
            }
        }
        private void LoadStoker()
        {
            Users users = (Users)Session["Users"];
            ArrayList arrStoker = new ArrayList();
            StokerFacade SFacade = new StokerFacade();
            arrStoker = SFacade.RetrieveByStoker(users.DeptID);
            ddlS.Items.Add(new ListItem("-- Pilih Stoker --", "-"));
            foreach (Stoker stok in arrStoker)
            {
                ddlS.Items.Add(new ListItem(stok.Nama));
            }
        }

        protected void btn1_ServerClick(object sender, EventArgs e)
        {

            ReportOpnameFacade ROF = new ReportOpnameFacade();
            string strQuery = string.Empty;
            string Tgl = string.Empty;
            string CutOff = string.Empty;
            string Nama = ddlS.SelectedValue;
            string TglOpname = string.Empty;
            string tahun = string.Empty;
            string Partno = txtPartnoA.Text;
            string BulanTahun = string.Empty;

            Tgl = DateTime.Parse(txtTgl.Text).ToString("dd-MM-yyyy");
            CutOff = DateTime.Parse(txtCut.Text).ToString("dd MMMM yyyy");
            TglOpname = DateTime.Parse(txtTgl.Text).ToString("dd MMMM yyyy");
            tahun = DateTime.Now.Year.ToString();

            BulanTahun = ddlBulan.SelectedItem.Text + " " + tahun;
            TglOpname = DateTime.Parse(txtTgl.Text).ToString("dd MMMM yyyy");
            CutOff = DateTime.Parse(txtCut.Text).ToString("dd MMMM yyyy");

            Users users = (Users)Session["Users"];
            string DeptID = users.DeptID.ToString();
            //Session["NamaPlant"] = (users.UnitKerjaID == 1) ? "Citeureup" : "Karawang";
            string NamaPabrik = string.Empty;
            StokerFacade SFacade = new StokerFacade();
            string deptidstocker = SFacade.retriveDeptbyStocker(Nama);

            if (users.UnitKerjaID == 1) NamaPabrik = "Citeureup";
            if (users.UnitKerjaID == 7) NamaPabrik = "Karawang";
            if (users.UnitKerjaID == 13) NamaPabrik = "Jombang";

            if (DeptID != "0")
            {
                if (DeptID == "6")
                {
                    if (RB1.Checked == true)
                    {
                        strQuery = ROF.ViewLaporanT3(Nama, users.DeptID);

                        Session["NamaPlant"] = NamaPabrik;
                        Session["Tgl"] = Tgl;
                        Session["Query"] = strQuery;
                        Session["CutOff"] = CutOff;
                        Session["tgl"] = TglOpname;
                        Session["Tahun"] = tahun;
                        Cetak1(this);
                    }
                    if (RB2.Checked == true)
                    {
                        strQuery = ROF.ViewLaporanT3opname(Partno, users.DeptID);
                        Session["NamaPlant"] = NamaPabrik;
                        Session["Query"] = strQuery;
                        Session["CutOff"] = CutOff;
                        Session["blnT"] = BulanTahun;
                        Session["tgl"] = TglOpname;
                        Cetak(this);
                    }
                }

                else if (DeptID == "3")
                {
                    if (RB1.Checked == true)
                    {
                        strQuery = ROF.ViewLaporanT3fin(Nama);
                        Session["NamaPlant"] = NamaPabrik;
                        Session["Tgl"] = Tgl;
                        Session["Query"] = strQuery;
                        Session["CutOff"] = CutOff;
                        Session["tgl"] = TglOpname;
                        Session["Tahun"] = tahun;
                        Cetak1(this);
                    }
                    if (RB2.Checked == true)
                    {
                        strQuery = ROF.ViewLaporanT3opnamefin(Partno);

                        Session["NamaPlant"] = NamaPabrik;
                        Session["Query"] = strQuery;
                        Session["CutOff"] = CutOff;
                        Session["blnT"] = BulanTahun;
                        Session["tgl"] = TglOpname;
                        Cetak(this);
                    }

                }
                else if( DeptID == "24" || DeptID == "14")
                {
                    if (deptidstocker == "3")
                    {
                        if (RB1.Checked == true)
                        {
                            strQuery = ROF.ViewLaporanT3fin(Nama);
                            Session["NamaPlant"] = NamaPabrik;
                            Session["Tgl"] = Tgl;
                            Session["Query"] = strQuery;
                            Session["CutOff"] = CutOff;
                            Session["tgl"] = TglOpname;
                            Session["Tahun"] = tahun;
                            Cetak1(this);
                        }
                        if (RB2.Checked == true)
                        {
                            strQuery = ROF.ViewLaporanT3opnamefin(Partno);

                            Session["NamaPlant"] = NamaPabrik;
                            Session["Query"] = strQuery;
                            Session["CutOff"] = CutOff;
                            Session["blnT"] = BulanTahun;
                            Session["tgl"] = TglOpname;
                            Cetak(this);
                        }
                    }
                    else if (deptidstocker == "6")
                    {
                        if (RB1.Checked == true)
                        {
                            strQuery = ROF.ViewLaporanT3(Nama, users.DeptID);

                            Session["NamaPlant"] = NamaPabrik;
                            Session["Tgl"] = Tgl;
                            Session["Query"] = strQuery;
                            Session["CutOff"] = CutOff;
                            Session["tgl"] = TglOpname;
                            Session["Tahun"] = tahun;
                            Cetak1(this);
                        }
                        if (RB2.Checked == true)
                        {
                            strQuery = ROF.ViewLaporanT3opname(Partno, users.DeptID);
                            Session["NamaPlant"] = NamaPabrik;
                            Session["Query"] = strQuery;
                            Session["CutOff"] = CutOff;
                            Session["blnT"] = BulanTahun;
                            Session["tgl"] = TglOpname;
                            Cetak(this);
                        }
                    }
                }

            }



        }

        protected void txtPartnoA_TextChanged(object sender, EventArgs e)
        {
            AutoCompleteExtender1.ContextKey = txtPartnoA.Text;
            txtPartnoA.Focus();
        }

        static public void Cetak(Control page)
        {
            string myScript = "Cetak();";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        static public void Cetak1(Control page)
        {
            string myScript = "Cetak2();";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void RB1_CheckedChanged(object sender, EventArgs e)
        {
            if (RB1.Checked == true)
            {
                btn1.Disabled = false;
                ddlS.Enabled = true;
                txtTgl.Enabled = true;
                txtCut.Enabled = true;
                txtPartnoA.Enabled = false;
                txtPartnoA.Text = string.Empty;
            }
        }

        protected void RB2_CheckedChanged(object sender, EventArgs e)
        {
            if (RB2.Checked == true)
            {
                //btn2.Disabled = false;
                btn1.Disabled = false;
                ddlS.Enabled = false;
                txtTgl.Enabled = true;
                txtCut.Enabled = true;
                txtPartnoA.Enabled = true;
                //ddlS.Text = string.Empty;
                ddlS.SelectedValue = "-";
            }
        }

        protected void ddlS_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}