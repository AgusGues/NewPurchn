using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using System.IO;

namespace GRCweb1.Modul.Purchasing
{
    public partial class PenyerahanPO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Users usr = (Users)Session["Users"];
                string strFirst = "1/1/" + DateTime.Now.Year.ToString();
                DateTime dateFirst = DateTime.Parse(strFirst);
                LoadBulan();
                LoadTahun();
                Session["Flag"] = "harian";
                Session["Flag1"] = "-";
                txtTgl01.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
                txtTgl1.Text = dateFirst.AddMonths(Convert.ToUInt16(ddlBulan0.SelectedIndex - 1)).Date.ToString("dd-MMM-yyyy");
                txtTgl2.Text = (dateFirst.AddMonths(Convert.ToUInt16(ddlBulan0.SelectedValue))).AddDays(-1).Date.ToString("dd-MMM-yyyy");
                string[] UserSerah = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("POUserSerah", "SerahTerimaDocument").Split(',');
                string[] Userterima = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("POUserTerima", "SerahTerimaDocument").Split(',');
                string[] DeptView = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("PODeptView", "SerahTerimaDocument").Split(',');
                if ((Posisi(DeptView, usr.DeptID.ToString()) > -1 || Posisi(Userterima, usr.UserID.ToString()) > -1) &&
                    Posisi(UserSerah, usr.UserID.ToString()) == -1)
                {
                    Session["tanda"] = "ListNo"; Session["Flag4"] = "-";
                    ListDocKirim();
                    btnExport.Visible = true;
                    frm10.Visible = (Posisi(Userterima, usr.UserID) > -1) ? false : true;
                    string heights = (Posisi(Userterima, usr.UserID) > -1) ? "height:450px" : "height:485px";
                    lst.Attributes.Add("style", heights);
                    btnList.Visible = (Posisi(Userterima, usr.UserID) > -1) ? true : false;
                }
                else if (Posisi(UserSerah, usr.UserID.ToString()) > -1)
                {
                    //ListDocSiapKirim();
                    Session["tanda"] = "ListNo"; Session["Flag4"] = "-";
                    btnExport.Visible = true;
                    frm10.Visible = false;
                    btnList.Visible = true; RPONoSerah.Visible = false; RPOSerah.Visible = false;
                    RPONoSerah.Checked = false; RPOSerah.Checked = false;
                }

            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }
        private decimal total;
        private decimal ok;
        private decimal noke;
        private decimal prosen;

        protected void RHarian_CheckedChanged(object sender, EventArgs e)
        {
            Session["Flag"] = "harian";
            harian.Visible = true; bulan.Visible = false;
            RHarian.Checked = true; RBulan.Checked = false;
        }
        protected void RBulan_CheckedChanged(object sender, EventArgs e)
        {
            Session["Flag"] = "bulanan";
            harian.Visible = false; bulan.Visible = true;
            RHarian.Checked = false; RBulan.Checked = true;
        }
        protected void RPOSerah_CheckedChanged(object sender, EventArgs e)
        {
            RPOSerah.Checked = true; RPONoSerah.Checked = false;
        }
        protected void RPONoSerah_CheckedChanged(object sender, EventArgs e)
        {
            RPOSerah.Checked = false; RPONoSerah.Checked = true;
        }

        private void ListDocKirim()
        {
            // total = 0; 
            string Flag = Session["Flag"].ToString();
            string Flag1 = Session["Flag1"].ToString();
            string Flag4 = Session["Flag4"].ToString();

            string tgl = string.Empty;
            tgl = Convert.ToDateTime(txtTgl01.Text).ToString("yyyyMMdd");

            string[] ViewSummary = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("PODeptView", "SerahTerimaDocument").Split(',');
            int dept = Array.IndexOf(ViewSummary, ((Users)Session["Users"]).DeptID.ToString());
            ok = 0; noke = 0;
            Session["summary"] = (dept > -1) ? "Tampil" : "Tidak";
            //ArrayList arrData = new ArrayList();
            //arrData = new PantauSerah2().Retrieve(int.Parse(ddlBulan0.SelectedValue), int.Parse(ddlTahun0.SelectedValue), btnBack.Visible, tgl);
            //total = arrData.Count;
            //lstSerah.DataSource = arrData;
            //lstSerah.DataBind();
            if (Flag == "bulanan")
            {
                if (Flag4 != "Preview")
                {
                    ArrayList arrData = new ArrayList();
                    arrData = new PantauSerah2().Retrieve(int.Parse(ddlBulan0.SelectedValue), int.Parse(ddlTahun0.SelectedValue), btnBack.Visible, tgl);
                    total = arrData.Count;
                    lstSerah.DataSource = arrData;
                    lstSerah.DataBind();
                }
                else
                {
                    ArrayList arrData = new ArrayList();
                    arrData = new PantauSerah2().Retrieve(int.Parse(ddlBulan.SelectedValue), int.Parse(ddlTahun.SelectedValue), btnBack.Visible, tgl);
                    total = arrData.Count;
                    lstSerah.DataSource = arrData;
                    lstSerah.DataBind();
                }
            }
            else if (Flag == "harian")
            {
                ArrayList arrData = new ArrayList();
                arrData = new PantauSerah2().Retrieve(int.Parse(ddlBulan0.SelectedValue), int.Parse(ddlTahun0.SelectedValue), btnBack.Visible, tgl);
                total = arrData.Count;
                lstSerah.DataSource = arrData;
                lstSerah.DataBind();
            }

        }
        private void ListDocSiapKirim()
        {
            Session["summary"] = "Tidak";
            //ArrayList arrData = new ArrayList();
            //arrData = new PantauSerah2().Retrieve();
            //total = arrData.Count;
            //lstSerah.DataSource = arrData;
            //lstSerah.DataBind();
            StatusPO Sts = new StatusPO();
            Sts.Criteria = "and convert(varchar,p.POPurchndate,112)>= '" + DateTime.Parse(txtTgl1.Text).ToString("yyyyMMdd") + "' and convert(varchar,p.POPurchndate,112)<= '" + DateTime.Parse(txtTgl2.Text).ToString("yyyyMMdd") + "' ";
            ArrayList arrData = Sts.Retrieve();
            lstSerah.DataSource = arrData;
            lstSerah.DataBind();
        }

        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            //string drTgl = string.Empty;
            //string sdTgl = string.Empty;
            //string PdrTgl = string.Empty;
            //string PsdTgl = string.Empty;
            //Users usr = (Users)Session["Users"];
            //drTgl = DateTime.Parse(txtTgl1.Text).ToString("yyyyMMdd");
            //sdTgl = DateTime.Parse(txtTgl2.Text).ToString("yyyyMMdd");
            //PdrTgl = DateTime.Parse(txtTgl1.Text).ToString("dd/MM/yyyy");
            //PsdTgl = DateTime.Parse(txtTgl2.Text).ToString("dd/MM/yyyy");
            //Session["drTgl"] = PdrTgl;
            //Session["sdTgl"] = PsdTgl;
            //SerahPO();
            //string Flag = Session["Flag"].ToString();

            //Session["Flag1"] = string.Empty; Session["Flag"] = string.Empty;

            string Flag = Session["Flag"].ToString();
            Session["Flag1"] = "-";
            StatusPO Sts = new StatusPO();
            //Sts.Criteria = "and convert(varchar,p.POPurchndate,112)>= '" + DateTime.Parse(txtTgl1.Text).ToString("yyyyMMdd") + "' and convert(varchar,p.POPurchndate,112)<= '" + DateTime.Parse(txtTgl2.Text).ToString("yyyyMMdd") + "' ";

            if (Flag == "bulanan" && RPOSerah.Checked == false && RPONoSerah.Checked == false)
            {
                Sts.Criteria = "and convert(varchar,p.POPurchndate,112)>= '" + DateTime.Parse(txtTgl1.Text).ToString("yyyyMMdd") + "' and convert(varchar,p.POPurchndate,112)<= '" + DateTime.Parse(txtTgl2.Text).ToString("yyyyMMdd") + "' ";
                ArrayList arrData = Sts.Retrieve();
                lstSerah.DataSource = arrData;
                lstSerah.DataBind();
            }
            else if (Flag == "harian" && RPOSerah.Checked == false && RPONoSerah.Checked == false)
            {
                Sts.Criteria = "and left(convert(char,p.POPurchndate,112),8)= '" + DateTime.Parse(txtTgl01.Text).ToString("yyyyMMdd") + "' ";
                ArrayList arrData = Sts.Retrieve();
                lstSerah.DataSource = arrData;
                lstSerah.DataBind();
            }
            else if (Flag == "bulanan" && RPOSerah.Checked == true && RPONoSerah.Checked == false)
            {
                //string Flag = "Bulanan"; Session["Flag"] = Flag; Session["Flag3"] = "Terima";
                Session["Flag1"] = "bulananserah";
                ListDocKirim();
            }
            else if (Flag == "bulanan" && RPOSerah.Checked == false && RPONoSerah.Checked == true)
            {
                Sts.Criteria = "and convert(varchar,p.POPurchndate,112)>= '" + DateTime.Parse(txtTgl1.Text).ToString("yyyyMMdd") + "' and convert(varchar,p.POPurchndate,112)<= '" + DateTime.Parse(txtTgl2.Text).ToString("yyyyMMdd") + "' ";
                ArrayList arrData = Sts.Retrieve();
                lstSerah.DataSource = arrData;
                lstSerah.DataBind();
            }
            else if (Flag == "harian" && RPOSerah.Checked == true && RPONoSerah.Checked == false)
            {
                //Session["Flag1"] = "bulananserah";
                ListDocKirim();
            }
            else if (Flag == "harian" && RPOSerah.Checked == false && RPONoSerah.Checked == true)
            {
                Sts.Criteria = "and left(convert(char,p.POPurchndate,112),8)= '" + DateTime.Parse(txtTgl01.Text).ToString("yyyyMMdd") + "' ";
                ArrayList arrData = Sts.Retrieve();
                lstSerah.DataSource = arrData;
                lstSerah.DataBind();
            }

            //ArrayList arrData = Sts.Retrieve();
            //lstSerah.DataSource = arrData;
            //lstSerah.DataBind();

        }

        //private void SerahPO()
        //{
        //    string drTgl = string.Empty;
        //    string sdTgl = string.Empty;
        //    string PdrTgl = string.Empty;
        //    string PsdTgl = string.Empty;
        //    Users usr = (Users)Session["Users"];
        //    drTgl = DateTime.Parse(txtTgl1.Text).ToString("yyyyMMdd");
        //    sdTgl = DateTime.Parse(txtTgl2.Text).ToString("yyyyMMdd");
        //    PdrTgl = DateTime.Parse(txtTgl1.Text).ToString("dd/MM/yyyy");
        //    PsdTgl = DateTime.Parse(txtTgl2.Text).ToString("dd/MM/yyyy");
        //    Session["drTgl"] = PdrTgl;
        //    Session["sdTgl"] = PsdTgl;
        //    Pantau2 Sts = new Pantau2();
        //    Sts.Criteria = "and convert(varchar,POPurchn.POPurchndate,112)>= '" + DateTime.Parse(txtTgl1.Text).ToString("yyyyMMdd") + "' and convert(varchar,POPurchn.POPurchndate,112)<= '" + DateTime.Parse(txtTgl2.Text).ToString("yyyyMMdd") + "' ";
        //    ArrayList arrData = Sts.Retrieve();
        //    //arrData = new PantauSerah2().Retrieve();
        //    total = arrData.Count;
        //    lstSerah.DataSource = arrData;
        //    lstSerah.DataBind();

        //}

        public class StatusPO
        {
            private Pantau2 panto = new Pantau2();
            public string Criteria { get; set; }
            ArrayList arrData = new ArrayList();
            private string Query()
            {
                //    string strQuery = "SELECT POPurchnDetail.ID,POPurchn.NoPO, SuppPurch.SupplierName,POPurchn.Cetak,POPurchn.Approval, POPurchn.Termin, POPurchn.PPN,POPurchn.PPH, " +
                //"MataUang.Nama, CASE POPurchnDetail.ItemTypeID WHEN 1 THEN (SELECT ItemName FROM Inventory WHERE ID = POPurchnDetail.ItemID AND RowStatus > - 1) " +
                //"WHEN 2 THEN (SELECT ItemName FROM Asset WHERE ID = POPurchnDetail.ItemID AND RowStatus > - 1) ELSE CASE " +
                //"WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())<=  (Select CreatedTime from SPP where SPP.ID=POPurchnDetail.SPPID))" +
                //"THEN(select ItemName from Biaya where Biaya.ID=POPurchnDetail.ItemID and Biaya.RowStatus>-1)+' - '+ " +
                //"(Select SPPDetail.Keterangan From SPPDetail where SPPDetail.ID=POPurchnDetail.SPPDetailID and SPPDetail.SPPID=POPurchnDetail.SPPID) ELSE  " +
                //"(select ItemName from biaya where ID=POPurchnDetail.ItemID and biaya.RowStatus>-1) END  END AS ItemName, SPP.NoSPP, POPurchnDetail.Price " +
                //"as Price2, POPurchnDetail.Qty,  UOM.UOMCode, POPurchn.Disc, case when POPurchn.Disc>0 then " +
                //"(POPurchnDetail.Price-(POPurchnDetail.Price*POPurchn.Disc/100)) else POPurchnDetail.Price end Price,(case when POPurchn.Disc>0 then " +
                //"(POPurchnDetail.Price-(POPurchnDetail.Price*POPurchn.Disc/100)) else POPurchnDetail.Price end)*POPurchnDetail.Qty as Total, " +
                //"POPurchnDetail.Price * POPurchnDetail.Qty AS TOT2, POPurchn.POPurchnDate, GROUPsPURCHN.groupdescription as groupdesc FROM POPurchn INNER JOIN " +
                //"POPurchnDetail ON POPurchn.ID = POPurchnDetail.POID INNER JOIN SPP ON POPurchnDetail.SPPID = SPP.ID INNER JOIN UOM " +
                //"ON POPurchnDetail.UOMID = UOM.ID INNER JOIN SuppPurch ON POPurchn.SupplierID = SuppPurch.ID INNER JOIN MataUang ON POPurchn.Crc = MataUang.ID " +
                //"INNER JOIN GROUPsPURCHN ON POPurchnDetail.groupid = GROUPsPURCHN.id where  POPurchn.status>-1 " + this.Criteria + " " +
                //"and POPurchnDetail.Status >-1 and POPurchn.Termin<>'Cash' and POPurchn.Cetak=1 and POPurchnDetail.POID " +
                //"in(SELECT ID FROM POPurchn WHERE POPurchnDetail.ID not in((SELECT PODetailID FROM SerahTerimaPO WHERE RowStatus>-1))) order by POPurchn.NoPO";
                //    return strQuery;

                string strQuery = "select distinct p.ID, p.NoPO, p.Termin, p.Cetak, p.POPurchnDate, s.SupplierName " +
                "from POPurchn p left join POPurchnDetail pd on p.ID = pd.POID " +
                "LEFT JOIN SuppPurch As s ON p.SupplierID=s.ID " +
                "where p.Status>-1 " + this.Criteria + " " +
                "and p.Termin<>'cash' and p.Cetak=1 " +
                "and p.ID not in(SELECT PODetailID FROM SerahTerimaPO WHERE RowStatus>-1) " +
                "order by p.POPurchnDate";
                return strQuery;
            }
            public ArrayList Retrieve()
            {
                DatabaseLib dbLib = new DatabaseLib();
                dbLib.strConn = Global.ConnectionString();
                SqlDataReader sdr = dbLib.DataReader(this.Query());
                arrData = new ArrayList();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new Pantau2
                        {
                            //ID = (sdr["ID"] == DBNull.Value) ? 0 : Convert.ToInt32(sdr["ID"].ToString()),
                            //ApproveDate2 = (sdr["ApproveDate3"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(sdr["ApproveDate3"].ToString()),
                            ID = int.Parse(sdr["ID"].ToString()),
                            //NoSPP = (sdr["NoSPP"] == DBNull.Value) ? "" : sdr["NoSPP"].ToString(),
                            POPurchnDate = DateTime.Parse(sdr["POPurchnDate"].ToString()),
                            NoPO = sdr["NoPO"].ToString(),
                            SupplierName = sdr["SupplierName"].ToString()
                            //ItemID = int.Parse(sdr["ItemID"].ToString())
                            //ItemTypeID = Convert.ToInt32(sdr["ItemTypeID"].ToString())
                        });
                        //arrData.Add(GenerateObject(sdr));
                    }
                }

                return arrData;
            }

            public ArrayList Retrieve(int ID)
            {
                arrData = new ArrayList();
                //string strSQL = "SELECT rd.ID, p.POPurchnDate TglPO,p.NoPO,pd.DocumentNo NoSPP,s.SupplierName,rd.ReceiptID, " +
                //                "r.ReceiptDate tglRMS,rd.ItemID,r.ReceiptNo NoRMS,rd.Keterangan " +
                //                "FROM ReceiptDetail  rd " +
                //                "LEFT JOIN Receipt AS r ON r.ID=rd.ReceiptID " +
                //                "LEFT JOIN POPurchn AS p ON p.ID=rd.POID " +
                //                "LEFT JOIN POPurchnDetail AS pd ON pd.ID=rd.PODetailID " +
                //                "LEFT JOIN SuppPurch AS s ON s.ID=p.SupplierID " +
                //                "WHERE rd.ID=" + ID +
                //                "ORDER BY rd.ID ";
                string strSQL = "SELECT p.ID, p.POPurchnDate TglPO,p.NoPO,s.SupplierName " +
                                "FROM POPurchn p LEFT JOIN SuppPurch AS s ON s.ID=p.SupplierID WHERE p.ID=" + ID + " " +
                                "ORDER BY p.ID  ";
                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(strSQL);
                if (da.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        //arrData.Add(new Pantau2
                        //{
                        //    ID = int.Parse(sdr["ID"].ToString())
                        //});
                        arrData.Add(GenerateObject(sdr));
                    }
                }
                return arrData;
            }

            private Pantau2 GenerateObject(SqlDataReader sdr)
            {
                //panto = new Pantau2();
                Pantau2 panto = new Pantau2();
                panto.ID = int.Parse(sdr["ID"].ToString());
                panto.NoPO = sdr["NoPO"].ToString();
                
                //keterangan ditampilkan atas permintaan purchasing
                //panto.Keterangan = sdr["Keterangan"].ToString();

                panto.SupplierName = sdr["SupplierName"].ToString();
                //panto.NoSPP = sdr["NoSPP"].ToString();
                panto.POPurchnDate = DateTime.Parse(sdr["TglPO"].ToString());
                //panto.ItemID = int.Parse(sdr["ItemID"].ToString());
                //panto.ReceiptNo = sdr["NoRMS"].ToString();
                //panto.ReceiptID = int.Parse(sdr["ReceiptID"].ToString());
                //panto.TglRMS = DateTime.Parse(sdr["TglRMS"].ToString());
                return panto;
            }
        }

        public class DatabaseLib
        {
            public string strConn { get; set; }
            public SqlDataReader DataReader(string Query)
            {
                DataAccess dta = new DataAccess(this.strConn);
                SqlDataReader sdr = dta.RetrieveDataByString(Query);
                return sdr;
            }
        }

        protected void lstSerah_DataBound(object sender, RepeaterItemEventArgs e)
        {
            string tanda = Session["tanda"].ToString();
            string[] UserSerah = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("POUserSerah", "SerahTerimaDocument").Split(',');
            string[] Userterima = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("POUserTerima", "SerahTerimaDocument").Split(',');
            string SelisihHari = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("SelisihHari2", "SerahTerimaDocument");
            Pantau2 p = (Pantau2)e.Item.DataItem;
            Users usr = (Users)Session["Users"];
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TextBox txtKet = (TextBox)e.Item.FindControl("txtKet");
                Image Simpan = (Image)e.Item.FindControl("btnSimpan");
                Image Kirim = (Image)e.Item.FindControl("btnKirim");
                Image Terima = (Image)e.Item.FindControl("btnterima");
                Image btnUpdate = (Image)e.Item.FindControl("btnUpd");
                Image hapus = (Image)e.Item.FindControl("hapus");
                Image hapus2 = (Image)e.Item.FindControl("hapus2");
                Label lKirim = (Label)e.Item.FindControl("txtTglKirim");
                Label lTerima = (Label)e.Item.FindControl("txtTglTerima");
                Label lblKet = (Label)e.Item.FindControl("exKeterangan");
                lKirim.Text = (p.TglKirim.Year < (DateTime.Now.Year - 1)) ? ""/*DateTime.Now.ToString("dd/MM/yyyy HH:mm")*/ : p.TglKirim.ToString("dd/MM/yyyy HH:mm");
                lTerima.Text = (p.TglTerima.Year < (DateTime.Now.Year - 1)) ? "" : p.TglTerima.ToString("dd/MM/yyyy HH:mm");
                Simpan.Visible = false;

                txtKet.Visible = (Posisi(UserSerah, usr.UserID.ToString()) > -1) ? true : false;
                lblKet.Visible = (Posisi(UserSerah, usr.UserID.ToString()) == -1) ? true : false;

                Kirim.Visible = (Posisi(UserSerah, ((Users)Session["Users"]).UserID.ToString()) > -1) ? true : false;
                Kirim.Visible = (btnBack.Visible == true) ? false : Kirim.Visible;

                txtKet.Visible = (btnBack.Visible == true) ? false : txtKet.Visible;

                btnUpdate.Visible = false;

                lblKet.Visible = (btnBack.Visible == true) ? true : lblKet.Visible;
                Kirim.ToolTip = "Klik untuk proses status kirim";
                Terima.Visible = (Posisi(Userterima, ((Users)Session["Users"]).UserID.ToString()) > -1) ? true : false;
                Terima.Visible = (p.TglTerima.Year >= (DateTime.Now.Year)) ? false : Terima.Visible;
                //Simpan.Visible = ((DateTime.Now - p.POPurchnDate).TotalDays >= 2) ? true : Simpan.Visible;

                //keterangan ditampilkan permintaan Purchasing
                //txtKet.Text = ((p.TglTerima.Year < (DateTime.Now.Year - 1))) ? "" : p.Keterangan;
                txtKet.Text = p.Keterangan;

                string POkirim = p.TglKirim.Year.ToString();
                string POterima = p.TglTerima.Year.ToString();

                if (POkirim == "1")
                {
                    Terima.Visible = false;
                }
                if (tanda == "List" && usr.DeptID != 24)
                {
                    Terima.Visible = false; hapus2.Visible = false; hapus.Visible = false;
                }
                if (tanda == "List" && usr.DeptID == 24 && POterima != "1" && POkirim != "1")
                {
                    Terima.Visible = false; hapus2.Visible = false; hapus.Visible = true;
                }
                else if (tanda == "List" && usr.DeptID == 24 && POterima == "1" && POkirim != "1")
                {
                    Terima.Visible = false; hapus2.Visible = true; hapus.Visible = false;
                }
                else if (tanda != "List" && usr.DeptID != 24)
                {
                    Terima.Visible = false; hapus2.Visible = false; hapus.Visible = false;
                }
                else if (tanda != "List" && usr.DeptID == 24)
                {
                    Terima.Visible = true; hapus2.Visible = false; hapus.Visible = false;
                }

                if (p.TglKirim.Year < (DateTime.Now.Year - 1))
                {
                    double libur = new PantauSerah2().cekHariLibur(p.POPurchnDate.ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd"));
                    //libur = (libur);
                    double Selisih = Math.Floor((p.TglKirim - DateTime.Now).TotalDays);
                    ((Label)e.Item.FindControl("txtSesuai")).Text = (Math.Floor((DateTime.Now - p.POPurchnDate).TotalDays) - libur >= int.Parse(SelisihHari)) ? "" : "X";
                    ((Label)e.Item.FindControl("txtTidak")).Text = (Math.Floor((DateTime.Now - p.POPurchnDate).TotalDays) - libur >= int.Parse(SelisihHari)) ? "X" : "";
                    lKirim.ToolTip = "Selisih : " + (Math.Floor((DateTime.Now - p.POPurchnDate).TotalDays) - libur).ToString();
                    double selisihPO = (Math.Floor((DateTime.Now - p.POPurchnDate).TotalDays));
                    ((Label)e.Item.FindControl("txtSelisih")).Text = (selisihPO - libur).ToString();
                }
                else
                {
                    double libure = new PantauSerah2().cekHariLibur(p.POPurchnDate.ToString("yyyyMMdd"), p.TglKirim.ToString("yyyyMMdd"));
                    double Selisih = Math.Floor((p.TglKirim - p.POPurchnDate).TotalDays);
                    //libure = (libure);
                    ((Label)e.Item.FindControl("txtSesuai")).Text = ((Selisih - libure) >= int.Parse(SelisihHari)) ? "" : "X";
                    ((Label)e.Item.FindControl("txtTidak")).Text = ((Selisih - libure) >= int.Parse(SelisihHari)) ? "X" : "";
                    lblKet.Text = (Selisih - libure >= int.Parse(SelisihHari)) ? txtKet.Text : "";
                    lKirim.ToolTip = "Selisih : " + (Selisih - libure).ToString();
                    ((Label)e.Item.FindControl("txtSelisih")).Text = (Selisih - libure).ToString();
                }
                total = total + 1;
                if (((Label)e.Item.FindControl("txtSesuai")).Text == "X")
                {
                    ok = (ok + 1);///total;
                }
                if (((Label)e.Item.FindControl("txtTidak")).Text == "X")
                {
                    noke = noke + 1;
                    txtKet.Visible = (p.Keterangan == string.Empty) ? true : txtKet.Visible;
                    btnUpdate.Visible = (p.Keterangan == string.Empty && btnBack.Visible == true) ? true : false;
                }
                prosen = ok / total;
                btnUpdate.Visible = (Posisi(UserSerah, usr.UserID.ToString()) > -1) ? btnUpdate.Visible : false;
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {

                ((Label)e.Item.FindControl("stxtSesuai")).Text = (prosen * 100).ToString("###,###.#0");
                ((Label)e.Item.FindControl("stxtTidak")).Text = noke.ToString("###,###.#0");
                ((Label)e.Item.FindControl("sTotalData")).Text = total.ToString("###,###.#0");
                //e.Item.Visible = (Session["summary"].ToString() == "Tampil") ? true : false;  
            }
        }
        protected void lstSerah_Command(object sender, RepeaterCommandEventArgs e)
        {
            string index = e.CommandArgument.ToString();
            Label tidak = (Label)e.Item.FindControl("txtTidak");
            PantauSerah2 ps = new PantauSerah2();
            StatusPO ss = new StatusPO();
            TextBox txtKet = (TextBox)e.Item.FindControl("txtKet");
            switch (e.CommandName)
            {
                case "simpan":
                    break;
                case "kirim":
                    if (tidak.Text == "X" && txtKet.Text == string.Empty)
                    {
                        DisplayAJAXMessage(this, "Tanggal Kirim Document lebih dari H+3, keterangan harus di isi");
                        txtKet.Focus();
                        return;
                    }
                    else
                    {
                        ArrayList arrData = new ArrayList();
                        arrData = ss.Retrieve(int.Parse(index));
                        foreach (Pantau2 p in arrData)
                        {
                            Pantau2 pn = new Pantau2();
                            pn = p;
                            pn.CreatedBy = ((Users)Session["Users"]).UserID.ToString();
                            pn.Keterangan = txtKet.Text;
                            int rst = ps.Insert(pn);
                            if (rst > 0)
                            {
                                ListDocSiapKirim();
                            }
                        }
                    }
                    break;
                case "terima":
                    Pantau2 pun = new Pantau2();
                    pun.LastModifiedBy = ((Users)Session["Users"]).UserID.ToString();
                    pun.ID = int.Parse(index);
                    pun.RowStatus = 0;
                    int result = ps.Update(pun);
                    if (result > 0)
                    {
                        ListDocKirim();
                    }
                    break;
                case "upd":
                    Pantau2 edit = new Pantau2();
                    edit.LastModifiedBy = ((Users)Session["Users"]).UserID.ToString();
                    edit.ID = int.Parse(index);
                    edit.Keterangan = txtKet.Text;
                    int result2 = ps.UpdateKet(edit);
                    if (result2 > 0)
                    {
                        ListDocKirim();
                    }
                    break;
                case "hps":
                    Pantau2 hps = new Pantau2();
                    hps.LastModifiedBy = ((Users)Session["Users"]).UserID.ToString();
                    hps.ID = int.Parse(index);

                    int result3 = ps.Cancel(hps);
                    if (result3 > 0)
                    {
                        ListDocKirim();
                    }
                    break;
                case "hps2":
                    Pantau2 hps2 = new Pantau2();
                    hps2.LastModifiedBy = ((Users)Session["Users"]).UserID.ToString();
                    hps2.ID = int.Parse(index);

                    int result4 = ps.Cancel2(hps2);
                    if (result4 > 0)
                    {
                        ListDocKirim();
                    }
                    break;
            }
        }
        protected void txtKet_Change(object sender, EventArgs e)
        {
            for (int i = 0; i < lstSerah.Items.Count; i++)
            {
                HiddenField hdKet = (HiddenField)lstSerah.Items[i].FindControl("txtKetOri");
                TextBox txtKet = (TextBox)lstSerah.Items[i].FindControl("txtKet");
                //((Image)lstSerah.Items[i].FindControl("btnSimpan")).Visible = (hdKet.Value != txtKet.Text) ? true : false;
            }
        }
        private int Posisi(Array arr, string Data)
        {
            return Array.IndexOf(arr, Data);
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "MyScript", myScript, true);
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem rpt in lstSerah.Items)
            {
                ((HiddenField)rpt.FindControl("txtKetOri")).Visible = false;
                ((Image)rpt.FindControl("btnSimpan")).Visible = false;
                ((Image)rpt.FindControl("btnKirim")).Visible = false;
                ((Image)rpt.FindControl("btnterima")).Visible = false;
                ((TextBox)rpt.FindControl("txtKet")).Visible = false;
            }
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=SerahTerimaDocument.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<b>SERAH TERIMA DOKUMENT KERTAS</b>";
            Html += "<br>Periode : " + ddlBulan.SelectedItem.Text + "  " + ddlTahun.SelectedValue.ToString();
            string HtmlEnd = "";
            lst.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0", "border=\"1");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }
        protected void lstData_Click(object sender, EventArgs e)
        {
            Session["tanda"] = "List";
            frm10.Visible = true;
            frm11.Visible = false;
            btnList.Visible = false;
            btnBack.Visible = true;
            lst.Attributes.Add("style", "height:425px");
            ListDocKirim();
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            lst.Attributes.Add("style", "height:485px");
            Response.Redirect("PenyerahanPO.aspx?t=po");
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            Session["Flag"] = "bulanan"; Session["Flag4"] = "Preview";
            ListDocKirim();
        }
        private void LoadBulan()
        {
            ddlBulan.Items.Clear();
            ddlBulan.Items.Add(new ListItem("--Pilih Bulan--", "0"));
            for (int i = 1; i <= 12; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i), i.ToString()));
            }
            ddlBulan0.SelectedValue = DateTime.Now.Month.ToString();
            ddlBulan0.Items.Clear();
            ddlBulan0.Items.Add(new ListItem("--Pilih Bulan--", "0"));
            for (int i = 1; i <= 12; i++)
            {
                ddlBulan0.Items.Add(new ListItem(Global.nBulan(i), i.ToString()));
            }
            ddlBulan0.SelectedValue = DateTime.Now.Month.ToString();
        }
        private void LoadTahun()
        {
            ArrayList arrData = new ArrayList();
            arrData = new PantauSerah2().GetTahun();
            ddlTahun.Items.Clear();
            foreach (Pantau2 p in arrData)
            {
                ddlTahun.Items.Add(new ListItem(p.Tahun.ToString(), p.Tahun.ToString()));
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();

            ddlTahun0.Items.Clear();
            foreach (Pantau2 p in arrData)
            {
                ddlTahun0.Items.Add(new ListItem(p.Tahun.ToString(), p.Tahun.ToString()));
            }
            ddlTahun0.SelectedValue = DateTime.Now.Year.ToString();
        }
        protected void ddlBulan0_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strFirst = "1/1/" + ddlTahun0.SelectedItem.Text;
            DateTime dateFirst = DateTime.Parse(strFirst);

            txtTgl1.Text = dateFirst.AddMonths(Convert.ToUInt16(ddlBulan0.SelectedIndex - 1)).Date.ToString("dd-MMM-yyyy");
            txtTgl2.Text = (dateFirst.AddMonths(Convert.ToUInt16(ddlBulan0.SelectedValue))).AddDays(-1).Date.ToString("dd-MMM-yyyy");
        }
        protected void ddlTahun0_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strFirst = "1/1/" + ddlTahun0.SelectedItem.Text;
            DateTime dateFirst = DateTime.Parse(strFirst);

            txtTgl1.Text = dateFirst.AddMonths(Convert.ToUInt16(ddlBulan0.SelectedIndex - 1)).Date.ToString("dd-MMM-yyyy");
            txtTgl2.Text = (dateFirst.AddMonths(Convert.ToUInt16(ddlBulan0.SelectedValue))).AddDays(-1).Date.ToString("dd-MMM-yyyy");
        }
    }
}

public class PantauSerah2 : AbstractFacade
{
    private ArrayList arrData = new ArrayList();
    private Pantau2 panto = new Pantau2();
    private List<SqlParameter> sqlListParam;
    public PantauSerah2()
        : base()
    {

    }
    public override int Insert(object objDomain)
    {
        int result = 0;
        try
        {
            panto = (Pantau2)objDomain;
            sqlListParam = new List<SqlParameter>();
            sqlListParam.Add(new SqlParameter("@NoPO", panto.NoPO));
            //sqlListParam.Add(new SqlParameter("@NoSPP", panto.NoSPP));
            sqlListParam.Add(new SqlParameter("@TglPO", panto.POPurchnDate));
            //sqlListParam.Add(new SqlParameter("@ItemID", panto.ItemID));
            sqlListParam.Add(new SqlParameter("@Keterangan", panto.Keterangan));
            sqlListParam.Add(new SqlParameter("@CreatedBy", panto.CreatedBy));
            sqlListParam.Add(new SqlParameter("@SupplierName", panto.SupplierName));
            sqlListParam.Add(new SqlParameter("@ID", panto.ID));
            result = dataAccess.ProcessData(sqlListParam, "spSerahTerimaPO_Insert");
        }
        catch
        {
            result = -1;
        }
        return result;
    }
    public override int Update(object objDomain)
    {
        int result = 0;
        try
        {
            panto = (Pantau2)objDomain;
            sqlListParam = new List<SqlParameter>();
            sqlListParam.Add(new SqlParameter("@ID", panto.ID));
            sqlListParam.Add(new SqlParameter("@ModifiedBy", panto.LastModifiedBy));
            sqlListParam.Add(new SqlParameter("@RowStatus", panto.RowStatus));
            result = dataAccess.ProcessData(sqlListParam, "spSerahTerimaPO_Update");
        }
        catch
        {
            result = -1;
        }
        return result;
    }
    public int UpdateKet(object objDomain)
    {
        int result = 0;
        try
        {
            panto = (Pantau2)objDomain;
            sqlListParam = new List<SqlParameter>();
            sqlListParam.Add(new SqlParameter("@ID", panto.ID));
            sqlListParam.Add(new SqlParameter("@ModifiedBy", panto.LastModifiedBy));
            sqlListParam.Add(new SqlParameter("@Keterangan", panto.Keterangan));
            result = dataAccess.ProcessData(sqlListParam, "spSerahTerimaPO_Update_Ket");
        }
        catch
        {
            result = -1;
        }
        return result;
    }
    public int Cancel(object objDomain)
    {
        int result = 0;
        try
        {
            panto = (Pantau2)objDomain;
            sqlListParam = new List<SqlParameter>();
            sqlListParam.Add(new SqlParameter("@ID", panto.ID));
            sqlListParam.Add(new SqlParameter("@ModifiedBy", panto.LastModifiedBy));
            result = dataAccess.ProcessData(sqlListParam, "spSerahTerimaPO_Cancel");
        }
        catch
        {
            result = -1;
        }
        return result;
    }
    public int Cancel2(object objDomain)
    {
        int result = 0;
        try
        {
            panto = (Pantau2)objDomain;
            sqlListParam = new List<SqlParameter>();
            sqlListParam.Add(new SqlParameter("@ID", panto.ID));
            sqlListParam.Add(new SqlParameter("@ModifiedBy", panto.LastModifiedBy));
            result = dataAccess.ProcessData(sqlListParam, "spSerahTerimaPO_Cancel2");
        }
        catch
        {
            result = -1;
        }
        return result;
    }
    public override int Delete(object objDomain)
    {
        throw new NotImplementedException();
    }
    public override ArrayList Retrieve()
    {
        arrData = new ArrayList();
        //string TglMulai = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("POAwalMulai", "SerahTerimaDocument");
        //string ItemCode = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ItemCheckedCode", "PO");
        //ItemCode = ItemCode.Replace(",", "','");
        //string drTgl = string.Empty;
        //string sdTgl = string.Empty;
        //string PdrTgl = string.Empty;
        //string PsdTgl = string.Empty;
        //Users usr = (Users)Session["Users"];
        //drTgl = DateTime.Parse(txtTgl1.Text).ToString("yyyyMMdd");
        //sdTgl = DateTime.Parse(txtTgl2.Text).ToString("yyyyMMdd");
        //PdrTgl = DateTime.Parse(txtTgl1.Text).ToString("dd/MM/yyyy");
        //PsdTgl = DateTime.Parse(txtTgl2.Text).ToString("dd/MM/yyyy");
        //Session["drTgl"] = PdrTgl;
        //Session["sdTgl"] = PsdTgl;
        string strSQL = "SELECT POPurchn.NoPO, SuppPurch.SupplierName,POPurchn.Cetak,POPurchn.Approval, POPurchn.Termin, POPurchn.PPN,POPurchn.PPH, " +
        "MataUang.Nama, CASE POPurchnDetail.ItemTypeID WHEN 1 THEN (SELECT ItemName FROM Inventory WHERE ID = POPurchnDetail.ItemID AND RowStatus > - 1) " +
        "WHEN 2 THEN (SELECT ItemName FROM Asset WHERE ID = POPurchnDetail.ItemID AND RowStatus > - 1) ELSE CASE " +
        "WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())<=  (Select CreatedTime from SPP where SPP.ID=POPurchnDetail.SPPID))" +
        "THEN(select ItemName from Biaya where Biaya.ID=POPurchnDetail.ItemID and Biaya.RowStatus>-1)+' - '+ " +
        "(Select SPPDetail.Keterangan From SPPDetail where SPPDetail.ID=POPurchnDetail.SPPDetailID and SPPDetail.SPPID=POPurchnDetail.SPPID) ELSE  " +
        "(select ItemName from biaya where ID=POPurchnDetail.ItemID and biaya.RowStatus>-1) END  END AS ItemName, SPP.NoSPP, POPurchnDetail.Price " +
        "as Price2, POPurchnDetail.Qty,  UOM.UOMCode, POPurchn.Disc, case when POPurchn.Disc>0 then " +
        "(POPurchnDetail.Price-(POPurchnDetail.Price*POPurchn.Disc/100)) else POPurchnDetail.Price end Price,(case when POPurchn.Disc>0 then " +
        "(POPurchnDetail.Price-(POPurchnDetail.Price*POPurchn.Disc/100)) else POPurchnDetail.Price end)*POPurchnDetail.Qty as Total, " +
        "POPurchnDetail.Price * POPurchnDetail.Qty AS TOT2, POPurchn.POPurchnDate, GROUPsPURCHN.groupdescription as groupdesc FROM POPurchn INNER JOIN " +
        "POPurchnDetail ON POPurchn.ID = POPurchnDetail.POID INNER JOIN SPP ON POPurchnDetail.SPPID = SPP.ID INNER JOIN UOM " +
        "ON POPurchnDetail.UOMID = UOM.ID INNER JOIN SuppPurch ON POPurchn.SupplierID = SuppPurch.ID INNER JOIN MataUang ON POPurchn.Crc = MataUang.ID " +
        "INNER JOIN GROUPsPURCHN ON POPurchnDetail.groupid = GROUPsPURCHN.id where  POPurchn.status>-1 " +
        "and POPurchnDetail.Status >-1 and POPurchn.Termin<>'Cash' and POPurchn.Cetak=1 order by POPurchn.NoPO";
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(strSQL);
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrData.Add(GenerateObject(sdr));
            }
        }
        return arrData;
    }
    //public ArrayList Retrieve(int ID)
    //{
    //    arrData = new ArrayList();
    //    //string strSQL = "SELECT rd.ID, p.POPurchnDate TglPO,p.NoPO,pd.DocumentNo NoSPP,s.SupplierName,rd.ReceiptID, " +
    //    //                "r.ReceiptDate tglRMS,rd.ItemID,r.ReceiptNo NoRMS,rd.Keterangan " +
    //    //                "FROM ReceiptDetail  rd " +
    //    //                "LEFT JOIN Receipt AS r ON r.ID=rd.ReceiptID " +
    //    //                "LEFT JOIN POPurchn AS p ON p.ID=rd.POID " +
    //    //                "LEFT JOIN POPurchnDetail AS pd ON pd.ID=rd.PODetailID " +
    //    //                "LEFT JOIN SuppPurch AS s ON s.ID=p.SupplierID " +
    //    //                "WHERE rd.ID=" + ID +
    //    //                "ORDER BY rd.ID ";
    //    string strSQL = "SELECT p.POPurchnDate TglPO,p.NoPO,pd.DocumentNo NoSPP,s.SupplierName " +
    //                    "FROM POPurchn p LEFT JOIN POPurchnDetail AS pd ON pd.POID=p.ID " +
    //                    "LEFT JOIN SuppPurch AS s ON s.ID=p.SupplierID WHERE pd.ID=" + ID + 
    //                    "ORDER BY p.ID  ";
    //    DataAccess da = new DataAccess(Global.ConnectionString());
    //    SqlDataReader sdr = da.RetrieveDataByString(strSQL);
    //    if (da.Error == string.Empty && sdr.HasRows)
    //    {
    //        while (sdr.Read())
    //        {
    //            arrData.Add(new Pantau2
    //                {
    //                    ID = int.Parse(sdr["ID"].ToString())
    //                });
    //            //arrData.Add(GenerateObject(sdr));
    //        }
    //    }
    //    return arrData;
    //}
    public ArrayList Retrieve(int Bulan, int Tahun, bool List, string tgl)
    {
        HttpContext context = HttpContext.Current; string Flag1 = string.Empty; string Flag = string.Empty; string Query = string.Empty;
        Flag1 = context.Session["Flag1"].ToString();
        Flag = context.Session["Flag"].ToString();

        if (List == true)
        {
            Query = "  order by TglKirim ";
        }
        else
        {
            Query = " and (Tglterima is null or TglTerima='') order by TglPO ";
        }

        string[] Userterima = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("POUserTerima", "SerahTerimaDocument").Split(',');
        arrData = new ArrayList();
        int pos = Array.IndexOf(Userterima, ((Users)HttpContext.Current.Session["Users"]).UserID);

        string strSQL = (pos == -1 || List == true || Flag1 == "bulananserah") ?

            "Select * from SerahTerimaPO where RowStatus>-1 and Month(TglKirim)=" + Bulan + " and YEAR(TglKirim)=" + Tahun +
            " " + Query + " " :
            "Select * from SerahTerimaPO where RowStatus >-1 and left(convert(char,tglkirim,112),8)='" + tgl + "' and (Tglterima is null or TglTerima='') order by TglPO";

        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(strSQL);
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrData.Add(GenerateObject(sdr, GenerateObject(sdr)));
            }
        }
        return arrData;
    }
    public ArrayList GetTahun()
    {
        arrData = new ArrayList();
        string strSql = "Select Distinct YEAR(POPurchnDate)Tahun FROM POPurchn Where YEAR(POPurchnDate)is not null order by YEAR(POPurchnDate)";
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(strSql);
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrData.Add(new Pantau2 { Tahun = int.Parse(sdr["Tahun"].ToString()) });
            }
        }
        return arrData;
    }
    private Pantau2 GenerateObject(SqlDataReader sdr)
    {
        panto = new Pantau2();
        panto.ID = int.Parse(sdr["ID"].ToString());
        panto.NoPO = sdr["NoPO"].ToString();
        //Menampilkan keterangan permintaan purchasing
        panto.Keterangan = sdr["Keterangan"].ToString();
        panto.SupplierName = sdr["SupplierName"].ToString();
        //panto.NoSPP = sdr["NoSPP"].ToString();
        panto.POPurchnDate = DateTime.Parse(sdr["TglPO"].ToString());
        //panto.ReceiptNo = sdr["NoRMS"].ToString();
        //panto.ReceiptID = int.Parse(sdr["ReceiptID"].ToString());
        //panto.ItemID = int.Parse(sdr["ItemID"].ToString());
        //panto.TglRMS = DateTime.Parse(sdr["TglRMS"].ToString());
        return panto;
    }
    private Pantau2 GenerateObject(SqlDataReader sdr, Pantau2 objDomain)
    {
        panto = (Pantau2)objDomain;
        panto.TglKirim = DateTime.Parse(sdr["TglKirim"].ToString());
        panto.TglTerima = (sdr["TglTerima"] != DBNull.Value) ? DateTime.Parse(sdr["TglTerima"].ToString()) : DateTime.MinValue;
        panto.LastModifiedBy = sdr["ModifiedBy"].ToString();
        return panto;
    }
    public double cekHariLibur(string fromDate, string ToDate)
    {
        double result = 0;
        string strSQL = "set datefirst 1;select dbo.GetOFFDay('" + fromDate + "','" + ToDate + "') as Libur ";
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(strSQL);
        if (sdr.HasRows && da.Error == string.Empty)
        {
            while (sdr.Read())
            {
                result = double.Parse(sdr["Libur"].ToString());
            }
        }
        return result;
    }
}
public class Pantau2 : GRCBaseDomain
{
    public DateTime POPurchnDate { get; set; }
    public string NoPO { get; set; }
    private string noPO = string.Empty;
    public string NoSPP { get; set; }
    public string Keterangan { get; set; }
    public string ReceiptNo { get; set; }
    public string SupplierName { get; set; }
    public int ItemID { get; set; }
    public int ID { get; set; }
    public int ReceiptID { get; set; }
    public DateTime TglKirim { get; set; }
    public DateTime TglTerima { get; set; }
    public string Sesuai { get; set; }
    public string Tidak { get; set; }
    public DateTime TglRMS { get; set; }
    public int Tahun { get; set; }
    public int Bulan { get; set; }
    public string Criteria { get; set; }

}