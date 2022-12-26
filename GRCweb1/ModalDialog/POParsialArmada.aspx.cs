using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using BusinessFacade;
using Domain;
using DataAccessLayer;

namespace GRCweb1.ModalDialog
{
    public partial class POParsialArmada : System.Web.UI.Page
    {
        private decimal ttMobil = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadSch();
                LoadPlant();
            }
            ttMobil = decimal.Parse(jmlMobil.Text);
        }

        protected void lstPlant_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater npol = (Repeater)e.Item.FindControl("lstNoPol");
            Company cn = (Company)e.Item.DataItem;
            Armada arm = new Armada();
            arm.Pilihan = "NoPol";
            arm.Criteria = "";
            npol.DataSource = this.GetKendaraan(cn.DepoID);
            //npol.DataSource = arm.Retrieve();
            npol.DataBind();
        }
        protected void lstNoPol_DataBound(object sender, RepeaterItemEventArgs e)
        {
            DropDownList ddlTj = (DropDownList)e.Item.FindControl("ddlTujuan");
            DropDownList ddlTjs = (DropDownList)e.Item.Parent.Parent.FindControl("ddlTujuan");
            Label ritase = (Label)e.Item.FindControl("rts");
            DropDownList dldriver = (DropDownList)e.Item.FindControl("txtDriver");
            #region LoadPlant for dropdown ddlTujuan
            ArrayList arrData = new ArrayList();
            CompanyFacade cp = new CompanyFacade();
            cp.Where = " and depoid=" + ((Users)Session["Users"]).UnitKerjaID.ToString();
            cp.OrderBy = " order by ID";
            arrData = cp.Retrieve();
            ddlTj.Items.Clear();
            foreach (Company c in arrData)
            {
                ddlTj.Items.Add(new ListItem(c.Spv.ToString(), c.DepoID.ToString()));
                ddlTjs.Items.Add(new ListItem(c.Spv.ToString(), c.DepoID.ToString()));
            }
            ddlTj.SelectedValue = ((Users)Session["Users"]).UnitKerjaID.ToString();
            #endregion
            MTC_Armada mtc = (MTC_Armada)e.Item.DataItem;
            Armada arm = new Armada();
            arm.Criteria = " where NoPol='" + mtc.NoPol.ToString() + "'";
            arm.Criteria += " and Convert(char,SchDate,112)='" + DateTime.Parse(schDate.Text).ToString("yyyyMMdd") + "'";
            arm.Criteria += " group by SchDate,NoPOl ";
            ritase.Text = arm.GetRitase().ToString();

        }
        protected void chk_Click(object sender, EventArgs e)
        {
            var chkk = (CheckBox)sender;
            ArrayList arrData = new ArrayList();
            for (int n = 0; n < lstPlant.Items.Count; n++)
            {
                Repeater lst = (Repeater)lstPlant.Items[n].FindControl("lstNoPol");
                TextBox idLok = (TextBox)lstPlant.Items[n].FindControl("idlok");
                for (int i = 0; i < lst.Items.Count; i++)
                {
                    DropDownList ddl = (DropDownList)lst.Items[i].FindControl("ddlTujuan");
                    CheckBox chk = (CheckBox)lst.Items[i].FindControl("chk");
                    DropDownList txt = (DropDownList)lst.Items[i].FindControl("txtDriver");
                    DropDownList cotxt = (DropDownList)lst.Items[i].FindControl("txtCoDriver");
                    Label lbl = (Label)lst.Items[i].FindControl("rts");
                    TextBox txbox = (TextBox)lst.Items[i].FindControl("txtJam");
                    ddl.Enabled = (chk.Checked == true) ? true : false;
                    txt.Enabled = (chk.Checked == true) ? true : false;
                    cotxt.Enabled = (chk.Checked == true) ? true : false;
                    txbox.Enabled = (chk.Checked == true) ? true : false;
                    txt.Focus();
                    jmlMobil.Text = (chkk.Checked == true) ? (ttMobil - 1).ToString() : (ttMobil + 1).ToString();
                    if (decimal.Parse(jmlMobil.Text) == 0) { chk.Enabled = (chk.Checked == false) ? false : true; }
                    else if (decimal.Parse(jmlMobil.Text) > 0) { chk.Enabled = true; }

                    Armada a = new Armada();
                    a.Pilihan = "Driver";
                    a.Criteria = " and DriverType=1";
                    //a.Criteria += " and NOPOL='" + lbl.ToolTip.ToString() + "'";
                    arrData = a.Retrieve();
                    txt.Items.Clear();
                    //txt.Items.Add(new ListItem("--Pilih Driver--", ""));
                    foreach (MTC_Armada m in arrData)
                    {
                        txt.Items.Add(new ListItem(m.DriverName.ToString(), m.DriverName.ToString()));
                    }
                    Armada am = new Armada();
                    am.Pilihan = "Driver";
                    am.Criteria = "and DriverType=2 order by DriverName";
                    arrData = am.Retrieve();
                    cotxt.Items.Clear();
                    cotxt.Items.Add(new ListItem("-Co Driver-", "0"));
                    foreach (MTC_Armada m in arrData)
                    {
                        cotxt.Items.Add(new ListItem(m.DriverName.ToString(), m.DriverName.ToString()));
                    }
                    if (idLok.Text != ((Users)Session["Users"]).UnitKerjaID.ToString())
                    {
                        //driver
                        txt.Items.Clear();
                        int Depo = (((Users)Session["Users"]).UnitKerjaID == 1) ? 7 : 1;
                        ArrayList ad = new ArrayList();
                        ad = this.GetDriver(Depo, "1");

                        foreach (MTC_Armada md in ad)
                        {
                            txt.Items.Add(new ListItem(md.DriverName, md.DriverName.ToString()));
                        }
                        //co driver
                        cotxt.Items.Clear();
                        ArrayList ada = new ArrayList();
                        ada = this.GetDriver(Depo, "2");
                        cotxt.Items.Add(new ListItem("-Co Driver-", "0"));
                        foreach (MTC_Armada mda in ada)
                        {
                            cotxt.Items.Add(new ListItem(mda.DriverName, mda.DriverName.ToString()));
                        }
                    }
                }
            }
        }
        protected void btnSimpan_CLick(object sender, EventArgs e)
        {
            Session["SchArmada"] = null;
            ArrayList arrData = new ArrayList();
            string PlantCode = ((Users)Session["Users"]).KodeLokasi.ToString();
            Armada am = new Armada();
            for (int i = 0; i < lstPlant.Items.Count; i++)
            {
                Repeater lst = (Repeater)lstPlant.Items[i].FindControl("lstNoPol");
                TextBox txtManual = (TextBox)lstPlant.Items[i].FindControl("txtNPol");
                CheckBox c = (CheckBox)lstPlant.Items[i].FindControl("chks");
                DropDownList ddls = (DropDownList)lstPlant.Items[i].FindControl("ddlTujuan");
                int Mobile = (i == 1) ? 7 : 1;
                for (int j = 0; j < lst.Items.Count; j++)
                {
                    POPurchn mtc = new POPurchn();
                    MTC_Armada arm = (MTC_Armada)lst.Items[j].DataItem;
                    DropDownList ddl = (DropDownList)lst.Items[j].FindControl("ddlTujuan");
                    CheckBox chk = (CheckBox)lst.Items[j].FindControl("chk");
                    DropDownList txt = (DropDownList)lst.Items[j].FindControl("txtDriver");
                    DropDownList Cotxt = (DropDownList)lst.Items[j].FindControl("txtCoDriver");
                    Label lbl = (Label)lst.Items[j].FindControl("rts");
                    TextBox txbox = (TextBox)lst.Items[j].FindControl("txtJam");
                    if (chk.Checked == true)
                    {
                        mtc.SchID = int.Parse(Request.QueryString["p"].ToString());

                        mtc.SchPOID = int.Parse(SchID.Text);
                        mtc.NoPol = ddl.ToolTip.ToString();
                        mtc.ArmadaID = int.Parse(chk.ToolTip.ToString());
                        mtc.Unloading = int.Parse(ddl.SelectedValue.ToString());
                        mtc.CreatedBy = ((Users)Session["Users"]).UserID.ToString();
                        mtc.CreatedTime = DateTime.Now;
                        mtc.Driver = txt.SelectedValue.ToString();
                        mtc.CoDriver = Cotxt.SelectedValue.ToString();
                        mtc.Ritase = int.Parse(lbl.Text) + 1;
                        mtc.SchDate = DateTime.Parse(schDate.Text);
                        mtc.Jam = txbox.Text.ToString();
                        mtc.Cetak = Mobile;
                        arrData.Add(mtc);
                    }
                }
                if (c.Checked == true)
                {
                    POPurchn mtc = new POPurchn();
                    mtc.SchID = int.Parse(Request.QueryString["p"].ToString());

                    mtc.SchPOID = int.Parse(SchID.Text);
                    mtc.NoPol = txtManual.Text.ToString();
                    mtc.ArmadaID = 26;// int.Parse(chk.ToolTip.ToString());
                    mtc.Unloading = int.Parse(ddls.SelectedValue.ToString());
                    mtc.CreatedBy = ((Users)Session["Users"]).UserID.ToString();
                    mtc.CreatedTime = DateTime.Now;
                    mtc.Driver = "";// txt.SelectedValue.ToString();
                    mtc.CoDriver = "";// Cotxt.SelectedValue.ToString();
                    mtc.Ritase = 1;
                    mtc.SchDate = DateTime.Parse(schDate.Text);
                    mtc.Jam = "08:00";// txbox.Text.ToString();
                    mtc.Cetak = Mobile;
                    arrData.Add(mtc);
                }
            }
            Session["SchArmada"] = (arrData.Count > 0) ? arrData : null;
            Response.Write("<script language='javascript'>window.close();</script>");
        }
        private ArrayList GetDriver(int DepoID, string DriverType)
        {
            DataSet Sopir = new DataSet();
            ArrayList arrData = new ArrayList();
            //bpas_api.WebService1 api = new bpas_api.WebService1();
            Global2 api = new Global2();
            string UnitKerja = string.Empty;
            string AliasKend = string.Empty;
            UnitKerja = (DepoID == 1) ? "1" : "7";
            AliasKend = (DepoID == 1) ? "GRCBoardCtrp" : "GRCBoardKrwg";
            try
            {
                Sopir = api.GetDataFromTable("MemoHarian_Driver", "Where RowStatus>-1 and DriverType=" + DriverType + " order by DriverName", AliasKend);
                foreach (DataRow dr in Sopir.Tables[0].Rows)
                {
                    arrData.Add(new MTC_Armada
                    {
                        DriverName = dr["DriverName"].ToString(),
                    });
                }
            }
            catch (Exception ex)
            {
                return arrData;
            }
            return arrData;
        }
        private ArrayList GetKendaraan(int DepoID)
        {
            DataSet NoPol = new DataSet();
            DataSet AlKend = new DataSet();
            ArrayList arrNopol = new ArrayList();
            try
            {
                MTC_ArmadaFacade arm = new MTC_ArmadaFacade();
                //bpas_api.WebService1 api = new bpas_api.WebService1();
                Global2 api = new Global2();
                string UnitKerja = string.Empty;
                string AliasKend = string.Empty;
                //UnitKerja = (DepoID == 1) ? "1" : "7";
                //AliasKend = (DepoID == 1) ? "GRCBoardCtrp" : "GRCBoardKrwg";
                UnitKerja = DepoID.ToString();
                if (DepoID == 1) AliasKend = "GRCBoardCtrp";
                if (DepoID == 7) AliasKend = "GRCBoardKrwg";
                if (DepoID == 13) AliasKend = "GRCBoardJmb";
                NoPol = api.GetNoPolByPlant(UnitKerja);
                foreach (DataRow nR in NoPol.Tables[0].Rows)
                {
                    string Alias = string.Empty;
                    try
                    {
                        AlKend = api.GetAliasKendaraan(nR["KendaraanNo"].ToString(), AliasKend);
                        foreach (DataRow al in AlKend.Tables[0].Rows)
                        {
                            Alias = al["NamaKendaraan"].ToString();
                        }
                    }
                    catch { }
                    if (Alias.Length > 2)
                    {
                        if (Alias.Substring(0, Alias.Length - 2) == "HI-BLOW" ||
                            Alias.Substring(0, Alias.Length - 2) == "HIBLOW" ||
                            Alias.Substring(0, 4) == "DUMP")
                        {
                            arrNopol.Add(new MTC_Armada
                            {
                                ID = Convert.ToInt32(nR["ID"].ToString()),
                                NoPol = nR["KendaraanNo"].ToString(),
                                ItemName = Alias
                            });
                        }
                    }

                }
                return arrNopol;
            }
            catch (Exception ex)
            {
                return arrNopol;
            }
        }
        private void LoadPlant()
        {
            ArrayList arrData = new ArrayList();
            CompanyFacade cp = new CompanyFacade();
            //cp.Where = " and depoid=" + ((Users)Session["Users"]).UnitKerjaID.ToString();
            cp.OrderBy = " order by ID";
            arrData = cp.Retrieve();
            lstPlant.DataSource = arrData;
            lstPlant.DataBind();
        }
        private void LoadSch()
        {
            Armada arm = new Armada();
            ArrayList arrData = new ArrayList();
            arm.Pilihan = "SchPO";
            arm.Criteria = " Where ID=" + Request.QueryString["p"].ToString();
            arrData = arm.Retrieve();
            foreach (MTC_Armada am in arrData)
            {
                Judul.Text = am.ItemName.ToString();
                jmlMobil.Text = am.Quantity.ToString();
                SchID.Text = am.ID.ToString();
                schDate.Text = am.SPBDate.ToString("dd-MM-yyyy");
            }
        }
        private void LoadDriver()
        {
            Armada arm = new Armada();
            ArrayList arrData = new ArrayList();
            arm.Pilihan = "Driver";
            arm.Criteria = " and drivertype=1";
            arrData = arm.Retrieve();

        }

    }
    public class Armada
    {
        private ArrayList arrData = new ArrayList();
        private MTC_Armada armd = new MTC_Armada();
        public string Criteria { get; set; }
        public string Pilihan { get; set; }
        public string Query()
        {
            string query = string.Empty;
            switch (this.Pilihan)
            {
                case "NoPol":
                    query = "select * from MTC_NamaArmada where NamaKendaraan like 'Hi-blow%' order by NamaKendaraan";
                    break;
                case "SchPO":
                    query = "select *,(select SupplierName from SuppPurch where ID=SupplierID)SupplierName " +
                            ",(select DvlDate from MemoHarian where ID=SchID)DlvDate " +
                            "from MemoHarian_PO " + this.Criteria;
                    break;
                case "Driver":
                    query = "Select * from MemoHarian_driver where RowStatus>-1 " + this.Criteria;
                    break;
            }

            return query;
        }
        public ArrayList Retrieve()
        {
            arrData = new ArrayList();
            string strSQL = this.Query();
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(this.Query());
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GeneratObject(sdr));
                }
            }
            return arrData;
        }
        private MTC_Armada GeneratObject(SqlDataReader sdr)
        {
            armd = new MTC_Armada();
            switch (this.Pilihan)
            {
                case "NoPol":
                    armd.ID = Convert.ToInt32(sdr["ID"].ToString());
                    armd.NoPol = sdr["NoPol"].ToString();
                    armd.ItemName = sdr["NamaKendaraan"].ToString();
                    break;
                case "SchPO":
                    armd.ItemName = sdr["SupplierName"].ToString();
                    armd.Quantity = Convert.ToDecimal(sdr["QtyMobil"].ToString());
                    armd.SPBDate = Convert.ToDateTime(sdr["DlvDate"].ToString());
                    armd.ID = Convert.ToInt32(sdr["ID"].ToString());
                    break;
                case "Driver":
                    armd.DriverName = sdr["DriverName"].ToString();
                    armd.HPDriver = sdr["HPNumber"].ToString();
                    armd.ID = Convert.ToInt32(sdr["ID"].ToString());
                    armd.DriverType = Convert.ToInt32(sdr["DriverType"].ToString());
                    break;
            }
            return armd;
        }
        public int GetRitase()
        {
            int result = 0;
            string strSQL = "select COUNT(NoPol)Ritase from MemoHarian_Armada " + this.Criteria;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToInt32(sdr["Ritase"].ToString());
                }
            }
            return result;
        }
        public int getDONum()
        {
            int result = 0;
            string strSQL = "Select Count(ID)Jml from MemoHarian_armada where month(CreatedTime)=" + DateTime.Now.Month + " and year(CreatedTime)=" +
                           DateTime.Now.Year.ToString();
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToInt32(sdr["Jml"].ToString());
                }
            }
            return result;
        }
    }

    public class Arm
    {

    }
}
