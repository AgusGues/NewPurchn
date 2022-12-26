using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Domain;
using BusinessFacade;
using DataAccessLayer;

namespace GRCweb1.ModalDialog
{
    public partial class ArmadaEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                Session["UpdateArmada"] = null;
                string ArmID = (Request.QueryString["p"] != null) ? Request.QueryString["p"].ToString() : "";
                //Judul.Text = (Request.QueryString["p"] != null) ? Request.QueryString["p"].ToString() : "0";// ((Users)Session["Users"]).UnitKerjaID.ToString();
                string[] ArmLok = ArmID.Split(',');
                int Lokasi = (ArmLok.Count() > 1) ? int.Parse(ArmLok[1].ToString()) : ((Users)Session["Users"]).UnitKerjaID;
                LoadDataArmada(Lokasi);
                ddlGetDriver(Lokasi, "1");
                ddlGetDriver(Lokasi, "2");
                LoadDetailSchArmada(int.Parse(ArmLok[0]));
            }

        }

        protected void btnSimpan_CLick(object sender, EventArgs e)
        {
            Session["UpdateArmada"] = null;
            ArrayList arrData = new ArrayList();
            POPurchn ma = new POPurchn();
            ma.ID = int.Parse(txtArmID.Text);
            ma.ArmadaID = int.Parse(ddlNopol.SelectedValue);
            string[] Npol = ddlNopol.SelectedItem.Text.Split('-');
            ma.NoPol = Npol[0].ToString();
            ma.Jam = txtJam.Text;
            ma.Driver = ddlDriver.SelectedValue;
            ma.CoDriver = ddlCoDriver.SelectedValue;
            ma.Keterangan = txtKeterangan.Text;
            arrData.Add(ma);
            Session["UpdateArmada"] = arrData;
            Response.Write("<script language='javascript'>window.close();</script>");
        }
        private void LoadDataArmada(int UnitKerjaID)
        {
            ArrayList arrData = new ArrayList();
            arrData = GetKendaraan(UnitKerjaID);
            ddlNopol.Items.Clear();
            ddlNopol.Items.Add(new ListItem("--NoPol--", "0"));
            foreach (MTC_Armada mt in arrData)
            {
                ddlNopol.Items.Add(new ListItem(mt.NoPol + "-" + mt.ItemName, mt.ID.ToString()));
            }
        }
        private void ddlGetDriver(int Depos, string dType)
        {
            ArrayList arrData = new ArrayList();
            arrData = GetDriver(Depos, dType);
            DropDownList ddl = (dType == "1") ? ddlDriver : ddlCoDriver;
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--Pilih--", "0"));
            foreach (MTC_Armada ar in arrData)
            {
                ddl.Items.Add(new ListItem(ar.DriverName, ar.DriverName));
            }
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
                UnitKerja = (DepoID == 1) ? "1" : "7";
                //AliasKend = (DepoID == 1) ? "GRCBoardCtrp" : "GRCBoardKrwg";

                NoPol = api.GetNoPolByPlant("1");
                foreach (DataRow nR in NoPol.Tables[0].Rows)
                {

                    string Alias = string.Empty;
                    //AlKend = api.GetAliasKendaraan(nR["KendaraanNo"].ToString(), AliasKend);
                    AlKend = api.GetAliasKendaraan(nR["KendaraanNo"].ToString(), "GRCBoardCtrp");
                    foreach (DataRow al in AlKend.Tables[0].Rows)
                    {
                        Alias = al["NamaKendaraan"].ToString();
                    }
                    if (Alias.Length > 2)
                    {
                        if (Alias.Substring(0, Alias.Length - 2) == "HI-BLOW" ||
                            Alias.Substring(0, Alias.Length - 2) == "HIBLOW")
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
                NoPol = api.GetNoPolByPlant("7");
                foreach (DataRow nR in NoPol.Tables[0].Rows)
                {

                    string Alias = string.Empty;
                    AlKend = api.GetAliasKendaraan(nR["KendaraanNo"].ToString(), "GRCBoardKrwg");
                    foreach (DataRow al in AlKend.Tables[0].Rows)
                    {
                        Alias = al["NamaKendaraan"].ToString();
                    }
                    if (Alias.Length > 2)
                    {
                        if (Alias.Substring(0, Alias.Length - 2) == "HI-BLOW" ||
                            Alias.Substring(0, Alias.Length - 2) == "HIBLOW")
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
                NoPol = api.GetNoPolByPlant("13");
                foreach (DataRow nR in NoPol.Tables[0].Rows)
                {

                    string Alias = string.Empty;
                    AlKend = api.GetAliasKendaraan(nR["KendaraanNo"].ToString(), "GRCBoardJmb");
                    foreach (DataRow al in AlKend.Tables[0].Rows)
                    {
                        Alias = al["NamaKendaraan"].ToString();
                    }
                    if (Alias.Length > 2)
                    {
                        if (Alias.Substring(0, Alias.Length - 2) == "HI-BLOW" ||
                            Alias.Substring(0, Alias.Length - 2) == "HIBLOW")
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
        private ArrayList GetDriver(int DepoID, string DriverType)
        {
            DataSet Sopir = new DataSet();
            ArrayList arrData = new ArrayList();
            //bpas_api.WebService1 api = new bpas_api.WebService1();
            Global2 api = new Global2();
            string UnitKerja = string.Empty;
            string AliasKend = string.Empty;
            UnitKerja = (DepoID == 1) ? "1" : "7";
            //AliasKend = (DepoID == 1) ? "GRCBoardCtrp" : "GRCBoardKrwg";
            try
            {
                //Sopir = api.GetDataFromTable("MemoHarian_Driver", "Where RowStatus>-1 and DriverType=" + DriverType + " order by DriverName", AliasKend);
                Sopir = api.GetDataFromTable("MemoHarian_Driver", "Where RowStatus>-1 and DriverType=" + DriverType + " order by DriverName", "GRCBoardCtrp");
                foreach (DataRow dr in Sopir.Tables[0].Rows)
                {
                    arrData.Add(new MTC_Armada
                    {
                        DriverName = dr["DriverName"].ToString(),
                    });
                }
                Sopir = api.GetDataFromTable("MemoHarian_Driver", "Where RowStatus>-1 and DriverType=" + DriverType + " order by DriverName", "GRCBoardKrwg");
                foreach (DataRow dr in Sopir.Tables[0].Rows)
                {
                    arrData.Add(new MTC_Armada
                    {
                        DriverName = dr["DriverName"].ToString(),
                    });
                }
                Sopir = api.GetDataFromTable("MemoHarian_Driver", "Where RowStatus>-1 and DriverType=" + DriverType + " order by DriverName", "GRCBoardJmb");
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
        private void LoadDetailSchArmada(int ID)
        {
            ArmadaUpdate ma = new ArmadaUpdate();
            ma.Criteria = " and ma.ID=" + ID;
            ma.Pilihan = "Detail";
            POPurchn po = ma.RetriveDetail();
            ddlNopol.SelectedValue = po.ArmadaID.ToString();
            ddlDriver.SelectedValue = po.Driver.ToString();
            ddlCoDriver.SelectedValue = po.CoDriver.ToString();
            txtJam.Text = po.Jam.ToString();
            txtKeterangan.Text = po.Keterangan.ToString();
            txtNoPO.Text = po.NoPO.ToString();
            txtSupplierName.Text = po.SupplierName.ToString();
            txtItemName.Text = po.ItemName.ToString();
            txtDoNum.Text = po.DoNum.ToString();
            txtArmID.Text = ID.ToString();

        }
    }
    public class ArmadaUpdate
    {
        ArrayList arrData = new ArrayList();
        MTC_Armada objArm = new MTC_Armada();
        public string Criteria { get; set; }
        public string Pilihan { get; set; }
        public string Field { get; set; }
        private string Query()
        {
            string query = string.Empty;
            switch (this.Pilihan)
            {
                case "Detail":
                    query = "select ma.*,(Select NoPol from POPurchn where ID=mp.POID)NoPO," +
                          "(select dbo.ItemNameInv(mp.ItemID,1))ItemName , " +
                          "(Select SupplierName from SuppPurch where ID=mp.SupplierID)SupplierName " +
                          "from MemoHarian_Armada as ma " +
                          "LEFT JOIN MemoHarian_PO as mp " +
                          "ON mp.ID=ma.SchPOID where ma.RowStatus >-1 " + this.Criteria;
                    break;
            }
            return query;
        }
        public POPurchn RetriveDetail()
        {
            POPurchn p = new POPurchn();
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(this.Query());
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    p.ArmadaID = Convert.ToInt32(sdr["ArmadaID"].ToString());
                    p.ID = Convert.ToInt32(sdr["ID"].ToString());
                    p.Driver = sdr["Driver"].ToString();
                    p.CoDriver = sdr["CoDriver"].ToString();
                    p.Jam = sdr["Jam"].ToString();
                    p.DoNum = sdr["DoNum"].ToString();
                    p.NoPO = sdr["NoPo"].ToString();
                    p.Keterangan = sdr["Keterangan"].ToString();
                    p.ItemName = sdr["ItemName"].ToString();
                    p.SupplierName = sdr["SupplierName"].ToString();

                }
            }
            return p;
        }
        private MTC_Armada gObject(SqlDataReader sdr)
        {
            objArm = new MTC_Armada();

            return objArm;
        }
    }
}