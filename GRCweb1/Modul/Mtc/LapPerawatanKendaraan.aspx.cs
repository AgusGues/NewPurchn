using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;
using Cogs;
using System.Collections;
using System.Threading;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace GRCweb1.Modul.Mtc
{
    public partial class LapPerawatanKendaraan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadNoPol();
                fromDate.Text = DateTime.Now.Date.AddDays(-(DateTime.Now.Day - 1)).ToString("dd-MMM-yyyy");
                toDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                Print.Enabled = false;
                ArrayList arrTT = new ArrayList();

            }
        }

        private void LoadNoPol()
        {
            DataSet NoPol = new DataSet();
            DataSet AlKend = new DataSet();
            ArrayList arrNopol = new ArrayList();
            ddlNoPolisi.Items.Clear();

            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "  select ID,Kendaraan NamaKendaraan from (select *,NoPol+' - '+NamaKendaraan Kendaraan from MTC_NamaArmada where NoPol not like'%-%' ) as xx3 ";
            SqlDataReader sdr = zl.Retrieve();
            ddlNoPolisi.Items.Clear();
            ddlNoPolisi.Items.Add(new ListItem("---- Pilih NoPol ----", "0"));
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlNoPolisi.Items.Add(new ListItem(sdr["NamaKendaraan"].ToString(), sdr["ID"].ToString()));
                }
            }

            //bpas_api.WebService1 api = new bpas_api.WebService1();
            //Global2 api = new Global2();
            //    string UnitKerja = string.Empty;
            //    string AliasKend = string.Empty;
            //    UnitKerja = (((Users)Session["Users"]).UnitKerjaID == 7) ? "7" : "1";
            //    AliasKend = (((Users)Session["Users"]).UnitKerjaID == 7) ? "GRCBoardKrwg":"GRCBoardCtrp" ;

            //    NoPol = api.GetNoPolByPlant(UnitKerja);
            //    foreach (DataRow nR in NoPol.Tables[0].Rows)
            //    {
            //        string Alias = string.Empty; int xz=0;
            //        AlKend = api.GetAliasKendaraan(nR["KendaraanNo"].ToString(), AliasKend);
            //        foreach (DataRow al in AlKend.Tables[0].Rows)
            //        {
            //            Alias = al["NamaKendaraan"].ToString();
            //        }
            //        string sepasi = " - ";
            //        xz = nR["KendaraanNo"].ToString().Length;
            //        arrNopol.Add(new MTC_Armada
            //        {
            //            ID = Convert.ToInt32(nR["ID"].ToString()),
            //            NoPol = nR["KendaraanNo"].ToString() + sepasi.PadLeft(10) + Alias
            //        });
            //    }

            //ddlNoPolisi.DataSource = arrNopol;
            //ddlNoPolisi.DataValueField = "ID";
            //ddlNoPolisi.DataTextField = "NoPol";
            //ddlNoPolisi.DataBind();
            //ddlNoPolisi.Items.Add(new ListItem("Forklift - ", "1001"));
            //ddlNoPolisi.Items.Add(new ListItem("Umum - ", "1000"));
            //ddlNoPolisi.Items.Add(new ListItem("--Pilih Nopol--", "0"));
            //ddlNoPolisi.SelectedValue = "0";

        }

        protected void Preview_Click(object sender, EventArgs e)
        {
            if (ddlNoPolisi.SelectedValue == "0")
            {
                if (rkpArmada.Checked == true)
                {
                    rekapArmada.Visible = true;
                    detailArmada.Visible = false;
                    detailAllArmada.Visible = false;
                    LoadKendaraan();
                }
                else
                {
                    rekapArmada.Visible = false;
                    detailArmada.Visible = false;
                    detailAllArmada.Visible = true;
                    LoadListKendaraan();
                }
            }
            else
            {
                //.Substring(0, npol.IndexOf(" -"))
                string NoPol = ddlNoPolisi.SelectedItem.ToString().Substring(0, ddlNoPolisi.SelectedItem.ToString().IndexOf(" -"));
                Session["NoPol"] = NoPol;
                rekapArmada.Visible = false;
                detailArmada.Visible = true;
                detailAllArmada.Visible = false;
                LoadDetailKendaraan();
            }
        }
        protected void Print_Click(object sender, EventArgs e)
        {
            ArrayList arrNopol = new ArrayList();
            arrNopol = (ArrayList)Session["Kendaraan"];
            Session["Data"] = arrNopol;
            Session["Periode"] = fromDate.Text.ToString() + " s/d " + toDate.Text.ToString();
            string rpt = string.Empty;
            if (rkpArmada.Checked == true)
            {
                if (ddlNoPolisi.SelectedValue == "0")
                {
                    rpt = "rkpReport";
                }
                else
                {
                    rpt = "dtlReport";
                }
            }
            else
            {
                rpt = "dtlReport";
            }
            Cetak(this, rpt);
        }
        private void LoadDetailKendaraan()
        {
            string NoPol = Session["NoPol"].ToString();

            if (Session["Kendaraan"] != null) { Session.Remove("Kendaraan"); }
            ArrayList arrMada = new ArrayList();
            ArrayList arrTT = new ArrayList();
            MTC_ArmadaFacade Arm = new MTC_ArmadaFacade();
            string frd = DateTime.Parse(fromDate.Text).ToString("yyyyMMdd");
            string tod = DateTime.Parse(toDate.Text).ToString("yyyyMMdd");
            decimal tA = 0; decimal tT = 0; int n = 0;
            ////arrMada = Arm.DetailTransArmada(int.Parse(ddlNoPolisi.SelectedValue), frd, tod);
            arrMada = Arm.DetailTransArmada(NoPol, frd, tod);
            if (arrMada.Count > 0 && Arm.Error == string.Empty)
            {
                dtlArmada.DataSource = arrMada;
                dtlArmada.DataBind();
                foreach (MTC_Armada ar in arrMada)
                {
                    tA = ar.TotAvg;
                    tT = ar.TotalS;
                    /* collect for printing */
                    n = n + 1;
                    arrTT.Add(new MTC_Armada
                    {
                        ID = n,
                        NoPol = ddlNoPolisi.SelectedItem.ToString(),
                        SPBDate = ar.SPBDate,
                        ItemCode = ar.ItemCode,
                        ItemName = ar.ItemName,
                        Satuan = ar.Satuan,
                        Quantity = ar.Quantity,
                        AvgPrice = ar.AvgPrice,
                        Total = ar.Total,
                        DeptName = ar.DeptName
                    });
                }
                ta.InnerHtml = tA.ToString("###,#00.00");
                tt.InnerHtml = tT.ToString("###,#00.00");
            }

            Session["Kendaraan"] = arrTT;
            Print.Enabled = true;
        }
        private void LoadKendaraan()
        {
            if (Session["Kendaraan"] != null) { Session.Remove("Kendaraan"); }
            //bpas_api.WebService1 api = new bpas_api.WebService1();
            Global2 api = new Global2();
            MTC_SarmutFacade armada = new MTC_SarmutFacade();
            MTC_ArmadaFacade arm = new MTC_ArmadaFacade();
            DataSet NoPol = new DataSet();
            NoPol = api.GetNoPolByPlant(((Users)Session["Users"]).UnitKerjaID.ToString());
            ArrayList arrNopol = new ArrayList();
            int N = 0; decimal tot = 0;
            string frd = DateTime.Parse(fromDate.Text).ToString("yyyyMMdd");
            string tod = DateTime.Parse(toDate.Text).ToString("yyyyMMdd");
            decimal totAvg = armada.RetrieveTotal(frd, tod, CheckConfig("only"));
            foreach (DataRow dRow in NoPol.Tables[0].Rows)
            {
                N = N + 1;
                MTC_Sarmut trans = armada.RetriveFoRekap(int.Parse(dRow["ID"].ToString()), frd, tod);
                if (trans.AvgPrice > 0)
                {
                    tot = tot + trans.Qty;
                    arrNopol.Add(new MTC_Armada
                    {
                        ID = N,
                        NoPol = dRow["KendaraanNo"].ToString(),
                        IDKendaraan = Convert.ToInt32(dRow["ID"].ToString()),
                        SPBNo = dRow["Merk"].ToString() + " " + DateTime.Parse(dRow["Tahun"].ToString()).Year.ToString() + " - " +
                        arm.GetAlias(dRow["KendaraanNo"].ToString()),
                        AvgPrice = trans.AvgPrice,
                        Quantity = (trans.AvgPrice == 0) ? 0 : (trans.AvgPrice / totAvg) * 100,
                        TotalS = trans.AvgPrice
                    });

                }
                else
                {
                    N = N - 1;
                }
            }

            arrNopol.Add(getForkliftdanUmum(1001, frd, tod));
            arrNopol.Add(getForkliftdanUmum(1000, frd, tod));
            if (arrNopol.Count > 0)
            {
                lstSarmut.DataSource = arrNopol;
                lstSarmut.DataBind();
                MTC_Sarmut transe = armada.RetriveFoRekap(0, frd, tod);
                ttl.InnerHtml = totAvg.ToString("#,#00.00");
                Print.Enabled = true;
            }
            else
            {
                arrNopol = new ArrayList();
                lstSarmut.DataSource = arrNopol;
                lstSarmut.DataBind();
                ttl.InnerHtml = string.Empty;
                Print.Enabled = false;

            }
            Session["Kendaraan"] = arrNopol;
        }
        private MTC_Armada getForkliftdanUmum(int id, string frd, string tod)
        {
            MTC_Armada objFor = new MTC_Armada();
            decimal tot = 0;
            MTC_Sarmut trans = new MTC_SarmutFacade().RetriveFoRekap(id, frd, tod);
            decimal totAvg = new MTC_SarmutFacade().RetrieveTotal(frd, tod, CheckConfig("notarmada"));
            if (trans.AvgPrice > 0)
            {
                tot = tot + trans.Qty;
                objFor.ID = 99;
                objFor.NoPol = "Forklift";
                objFor.IDKendaraan = id;
                objFor.SPBNo = "";
                objFor.AvgPrice = trans.AvgPrice;
                objFor.Quantity = (trans.AvgPrice == 0) ? 0 : (trans.AvgPrice / totAvg) * 100;
                objFor.TotalS = trans.AvgPrice;


            }
            return objFor;
        }
        private void LoadListKendaraan()
        {
            if (Session["Kendaraan"] != null) { Session.Remove("Kendaraan"); }
            ArrayList arrKend = new ArrayList();

            MTC_SarmutFacade Kend = new MTC_SarmutFacade();
            arrKend = Kend.RetrieveListKendaraan(((Users)Session["Users"]).UnitKerjaID);
            lstNopol.DataSource = arrKend;
            lstNopol.DataBind();
        }
        private string CheckConfig(string Section)
        {
            var Conf = new Inifile(Server.MapPath("~/App_Data/GroupArmadaOnly.ini"));
            return Conf.Read(Section, "Armada");
        }
        protected void lstNopol_ItemDataBound(object source, RepeaterItemEventArgs e)
        {
            ArrayList arrTT = (Session["Kendaraan"] != null) ? (ArrayList)Session["Kendaraan"] : new ArrayList();
            DataRowView lst = e.Item.DataItem as DataRowView;
            var sb = e.Item.FindControl("dtlLstArmada") as Repeater;
            if (sb != null)
            {
                ArrayList arrMada = new ArrayList();
                MTC_ArmadaFacade Arm = new MTC_ArmadaFacade();
                string frd = DateTime.Parse(fromDate.Text).ToString("yyyyMMdd");
                string tod = DateTime.Parse(toDate.Text).ToString("yyyyMMdd");
                int tA = 0;
                MTC_Sarmut np = e.Item.DataItem as MTC_Sarmut;
                //arrMada = Arm.DetailTransArmada(np.IDKendaraan, frd, tod);
                arrMada = Arm.DetailTransArmada(np.NoPol, frd, tod);
                if (arrMada.Count > 0 && Arm.Error == string.Empty && np.IDKendaraan > 0)
                {
                    sb.DataSource = arrMada;
                    sb.DataBind();
                }
                else
                {
                    arrMada = new ArrayList();
                    sb.DataSource = arrMada;
                    sb.DataBind();
                }
                foreach (MTC_Armada ar in arrMada)
                {
                    tA = tA + 1;
                    arrTT.Add(new MTC_Armada
                    {
                        ID = tA,
                        NoPol = np.NoPol,
                        SPBDate = ar.SPBDate,
                        ItemCode = ar.ItemCode,
                        ItemName = ar.ItemName,
                        Satuan = ar.Satuan,
                        Quantity = ar.Quantity,
                        AvgPrice = ar.AvgPrice,
                        Total = ar.Total,
                        DeptName = ar.DeptName
                    });
                }

                Session.Add("Kendaraan", arrTT);
                //Session["Kendaraan"] = arrTT;
                // Session["Kendaraan"] = arrMada;
                Print.Enabled = true;
            }
        }
        static public void Cetak(Control page, string idRpt)
        {
            string myScript = "var wn = window.showModalDialog('../../Report/Report2.aspx?IdReport=" + idRpt + "', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        protected void rkpArmada_CheckedChanged(object sender, EventArgs e)
        {
            Print.Enabled = false;
        }
        protected void lsArmada_CheckedChanged(object sender, EventArgs e)
        {
            Print.Enabled = false;
        }
        protected void ddlNoPolisi_SelectedIndexChanged(object sender, EventArgs e)
        {
            Print.Enabled = false;
        }

    }
}