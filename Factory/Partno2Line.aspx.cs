using DataAccessLayer;
using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.Factory
{
    public partial class Partno2Line : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                int bln = 0; int thn = 0; int blnlalu = 0;
                string bulan = string.Empty; string tahun = string.Empty; string Periode = string.Empty; string Periode2 = string.Empty;
                string bulanlalu = string.Empty; string tahunlalu = string.Empty; RBUK.Checked = true;

                string NamaBulanNow = DateTime.Now.Month.ToString();
                string NamaBulanTemp = string.Empty;
                if (NamaBulanNow == "1") { NamaBulanTemp = "JANUARI"; Session["NamaBulanTemp"] = NamaBulanTemp; }
                else if (NamaBulanNow == "2") { NamaBulanTemp = "FEBRUARI"; Session["NamaBulanTemp"] = NamaBulanTemp; }
                else if (NamaBulanNow == "3") { NamaBulanTemp = "MARET"; Session["NamaBulanTemp"] = NamaBulanTemp; }
                else if (NamaBulanNow == "4") { NamaBulanTemp = "APRIL"; Session["NamaBulanTemp"] = NamaBulanTemp; }
                else if (NamaBulanNow == "5") { NamaBulanTemp = "MEI"; Session["NamaBulanTemp"] = NamaBulanTemp; }
                else if (NamaBulanNow == "6") { NamaBulanTemp = "JUNI"; Session["NamaBulanTemp"] = NamaBulanTemp; }
                else if (NamaBulanNow == "7") { NamaBulanTemp = "JULI"; Session["NamaBulanTemp"] = NamaBulanTemp; }
                else if (NamaBulanNow == "8") { NamaBulanTemp = "AGUSTUS"; Session["NamaBulanTemp"] = NamaBulanTemp; }
                else if (NamaBulanNow == "9") { NamaBulanTemp = "SEPTEMBER"; Session["NamaBulanTemp"] = NamaBulanTemp; }
                else if (NamaBulanNow == "10") { NamaBulanTemp = "OKTOBER"; Session["NamaBulanTemp"] = NamaBulanTemp; }
                else if (NamaBulanNow == "11") { NamaBulanTemp = "NOVEMBER"; Session["NamaBulanTemp"] = NamaBulanTemp; }
                else if (NamaBulanNow == "12") { NamaBulanTemp = "DESEMBER"; Session["NamaBulanTemp"] = NamaBulanTemp; }

                if (DateTime.Now.Month == 1)
                {
                    bln = 12; blnlalu = bln - 1;
                    thn = DateTime.Now.Year - 1;
                    bulan = thn.ToString().Trim() + bln.ToString().Trim();
                    tahun = thn.ToString().Trim();
                    tahunlalu = tahun;
                    Periode = tahun + bulan;
                    Periode2 = tahunlalu + blnlalu.ToString().Trim();
                    Session["Periode"] = Periode; Session["Periode2"] = Periode2;
                }
                else
                {
                    bln = DateTime.Now.Month - 1; blnlalu = bln - 1; thn = DateTime.Now.Year; tahunlalu = thn.ToString().Trim();
                    if (bln == 2)
                    {
                        blnlalu = 12;
                        thn = thn - 1;
                        tahunlalu = thn.ToString().Trim();
                    }

                    if (bln.ToString().Length == 1)
                    {
                        bulan = 0 + bln.ToString().Trim();
                    }
                    else
                    {
                        bulan = bln.ToString().Trim();
                    }

                    if (blnlalu.ToString().Length == 1)
                    {
                        bulanlalu = 0 + blnlalu.ToString().Trim();
                    }
                    else
                    {
                        bulanlalu = blnlalu.ToString().Trim();
                    }

                    tahun = thn.ToString().Trim();
                    Periode = tahun + bulan; Periode2 = tahunlalu + bulanlalu.ToString().Trim();
                    Session["Periode"] = Periode; Session["Periode2"] = Periode2;
                }

                //LoadData(Periode, Periode2);
                LoadBulan();
                LoadTahun();
            }
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            int bl = Convert.ToInt32(ddlBulan.SelectedValue);
            int th = Convert.ToInt32(ddlTahun.SelectedValue);

            int bln = 0; int thn = 0; int blnlalu = 0;
            string bulan = string.Empty; string tahun = string.Empty; string Periode = string.Empty; string Periode2 = string.Empty;
            string bulanlalu = string.Empty; string tahunlalu = string.Empty; RBUK.Checked = true;

            if (bl == 1)
            {
                bln = 12; blnlalu = bln - 1;
                //thn = DateTime.Now.Year - 1;
                thn = Convert.ToInt32(ddlTahun.SelectedValue) - 1;
                bulan = thn.ToString().Trim() + bln.ToString().Trim();
                tahun = thn.ToString().Trim();
                tahunlalu = tahun;
                Periode = tahun + bulan;
                Periode2 = tahunlalu + blnlalu.ToString().Trim();
                Session["Periode"] = Periode; Session["Periode2"] = Periode2;
            }
            else
            {
                bln = Convert.ToInt32(ddlBulan.SelectedValue);
                blnlalu = bln - 1;

                thn = Convert.ToInt32(ddlTahun.SelectedValue);
                tahunlalu = thn.ToString().Trim();

                if (bln == 2)
                {
                    blnlalu = 12;
                    thn = thn - 1;
                    tahunlalu = thn.ToString().Trim();
                }

                if (bln.ToString().Length == 1)
                {
                    bulan = 0 + bln.ToString().Trim();
                }
                else
                {
                    bulan = bln.ToString().Trim();
                }

                if (blnlalu.ToString().Length == 1)
                {
                    bulanlalu = 0 + blnlalu.ToString().Trim();
                }
                else
                {
                    bulanlalu = blnlalu.ToString().Trim();
                }

                tahun = thn.ToString().Trim();
                Periode = tahun + bulan;
                Periode2 = tahunlalu + bulanlalu.ToString().Trim();
                Session["Periode"] = Periode; Session["Periode2"] = Periode2;
            }

            LoadData(Periode, Periode2);
        }

        protected void LoadBulan()
        {
            ArrayList arrBulan = new ArrayList();
            RekapDefectISOFacade data = new RekapDefectISOFacade();
            arrBulan = data.RetrieveBulan();
            ddlBulan.Items.Clear();
            ddlBulan.Items.Add(new ListItem(Session["NamaBulanTemp"].ToString(), DateTime.Now.Month.ToString()));
            foreach (RekapDefectISO bulan in arrBulan)
            {
                ddlBulan.Items.Add(new ListItem(bulan.BulanNama, bulan.Bulan));
            }
        }

        protected void LoadTahun()
        {
            ArrayList arrTahun = new ArrayList();
            RekapDefectISOFacade data = new RekapDefectISOFacade();
            arrTahun = data.RetrieveTahun();
            ddlTahun.Items.Clear();
            ddlTahun.Items.Add(new ListItem(DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString()));
            foreach (RekapDefectISO tahun in arrTahun)
            {
                ddlTahun.Items.Add(new ListItem(tahun.Tahun, tahun.Tahun));
            }
        }

        private void LoadData(string Periode, string Periode2)
        {
            string Query = string.Empty;

            if (RBLP.Checked == true)
            {
                Query = "LP"; RBUK.Checked = false;
            }
            else
            {
                Query = "UK"; RBLP.Checked = false; RBUK.Checked = true;
            }

            RekapDefectISOFacade f0 = new RekapDefectISOFacade();
            ArrayList arrData = new ArrayList();
            arrData = f0.RetrieveData(Query, Periode, Periode2);
            lst.DataSource = arrData;
            lst.DataBind();


        }

        protected void lst_DataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                RekapDefectISO gm = (RekapDefectISO)e.Item.DataItem;
                DropDownList ddl = (DropDownList)e.Item.FindControl("ddlLine");
                Image del = (Image)e.Item.FindControl("del");
                Label Ket = (Label)e.Item.FindControl("txtKet");

                if (Ket.Text == "Manual")
                {
                    del.Visible = true;
                }
                else
                {
                    del.Visible = false;
                }

                RekapDefectISOFacade f1 = new RekapDefectISOFacade();
                ArrayList arrData = f1.RetrieveLine();
                if (arrData.Count > 0)
                {
                    ddl.Items.Clear();
                    ddl.Items.Add(new ListItem("- Line -", "0"));
                    foreach (RekapDefectISO gm1 in arrData)
                    {
                        ddl.Items.Add(new ListItem(gm1.Line.ToString(), gm1.ID.ToString()));
                    }
                }
                ddl.Enabled = false;
            }
        }

        protected void lst_Command(object sender, RepeaterCommandEventArgs e)
        {
            Users users = (Users)Session["Users"];
            int RowNum = e.Item.ItemIndex;
            Image Simpan = (Image)lst.Items[RowNum].FindControl("simpan");
            Image img = (Image)lst.Items[RowNum].FindControl("edit");
            Image add = (Image)lst.Items[RowNum].FindControl("add");
            DropDownList ddlGM = (DropDownList)lst.Items[RowNum].FindControl("ddlLine");
            Label partno = (Label)e.Item.FindControl("txtPartno");
            Label line = (Label)e.Item.FindControl("txtLine");
            string Noted = string.Empty; string LineBefore = string.Empty;

            if (RBUK.Checked == true)
            {
                Noted = "Khusus";
            }
            else
            {
                Noted = "LP";
            }

            switch (e.CommandName)
            {
                case "Edit":
                    Simpan.Visible = true;
                    img.Visible = false;
                    ddlGM.Enabled = true;
                    break;

                case "Save":
                    RekapDefectISO gm2 = new RekapDefectISO();
                    RekapDefectISOFacade fgm2 = new RekapDefectISOFacade();

                    gm2.LineBefore = line.Text.Trim();
                    gm2.Partno = partno.Text.Trim();
                    gm2.Line = ddlGM.SelectedValue.ToString();
                    gm2.Periode = Session["Periode"].ToString();
                    gm2.CreatedBy = users.UserName.ToString();
                    int intResult = 0;

                    intResult = fgm2.Update(gm2);
                    if (intResult > -1)
                    {
                        LoadData(Session["Periode"].ToString(), Session["Periode2"].ToString());
                    }
                    else
                    {
                        DisplayAJAXMessage(this, " Gagal simpan !! "); return;
                    }

                    break;

                case "add":
                    RekapDefectISO gm3 = new RekapDefectISO();
                    RekapDefectISOFacade fgm3 = new RekapDefectISOFacade();

                    gm3.Partno = partno.Text.Trim();
                    gm3.Noted = Noted.Trim();
                    gm3.CreatedBy = users.UserName.ToString();
                    gm3.Periode = Session["Periode"].ToString();

                    int intHasil = 0;
                    intHasil = fgm3.InsertData(gm3);
                    if (intHasil > -1)
                    {
                        LoadData(Session["Periode"].ToString(), Session["Periode2"].ToString());
                    }
                    else
                    {
                        DisplayAJAXMessage(this, " Gagal Insert !! "); return;
                    }

                    break;

                case "del":
                    RekapDefectISO gm4 = new RekapDefectISO();
                    RekapDefectISOFacade fgm4 = new RekapDefectISOFacade();

                    gm4.Partno = partno.Text.Trim();
                    gm4.CreatedBy = users.UserName.ToString();
                    gm4.Periode = Session["Periode"].ToString();
                    gm4.Line = line.Text.Trim();

                    int intHasil1 = 0;

                    intHasil1 = fgm4.HapusData(gm4);
                    if (intHasil1 > -1)
                    {
                        LoadData(Session["Periode"].ToString(), Session["Periode2"].ToString());
                    }
                    else
                    {
                        DisplayAJAXMessage(this, " Gagal Hapus !! "); return;
                    }


                    break;
            }
        }

        protected void ddlLine_SelectedIndexChanged(object sender, EventArgs e)
        { }

        protected void RBUK_CheckedChanged(object sender, EventArgs e)
        {
            if (RBUK.Checked == true) { RBLP.Checked = false; }

            //LoadData(Session["Periode"].ToString(), Session["Periode2"].ToString());
        }

        protected void RBLP_CheckedChanged(object sender, EventArgs e)
        {
            if (RBLP.Checked == true)
            { RBUK.Checked = false; }
            //LoadData(Session["Periode"].ToString(), Session["Periode2"].ToString());
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void ddlBulan_SelectedChange(object sender, EventArgs e)
        {

        }

        protected void ddlTahun_SelectedChange(object sender, EventArgs e)
        {

        }

        public class RekapDefectISO : GRCBaseDomain
        {
            private DateTime tglinput = DateTime.Now.Date;
            public decimal Stok { get; set; }
            public string Partno { get; set; }
            public string Periode { get; set; }
            public string CreatedBy { get; set; }
            public int Approval { get; set; }
            public string Line { get; set; }
            public string LineBefore { get; set; }
            public string Tahun { get; set; }
            public string Bulan { get; set; }
            public string BulanNama { get; set; }
            public string Noted { get; set; }
            public string No { get; set; }
            public string Ket { get; set; }
        }

        public class RekapDefectISOFacade
        {
            private ArrayList arrData = new ArrayList();
            private RekapDefectISO objDF = new RekapDefectISO();
            public string strError = string.Empty;
            private List<SqlParameter> sqlListParam;

            public string UserID { get; set; }
            public string Bulan { get; set; }
            public string Tahun { get; set; }
            public string Field { get; set; }
            public string Where { get; set; }
            public string Bagian { get; set; }
            public string Criteria { get; set; }

            public int Update(object objDomain)
            {
                try
                {
                    objDF = (RekapDefectISO)objDomain;
                    sqlListParam = new List<SqlParameter>();
                    sqlListParam.Add(new SqlParameter("@Partno", objDF.Partno));
                    sqlListParam.Add(new SqlParameter("@Line", objDF.Line));
                    sqlListParam.Add(new SqlParameter("@Periode", objDF.Periode));
                    sqlListParam.Add(new SqlParameter("@CreatedBy", objDF.CreatedBy));
                    sqlListParam.Add(new SqlParameter("@LineBefore", objDF.LineBefore));

                    DataAccess da = new DataAccess(Global.ConnectionString());
                    int result = da.ProcessData(sqlListParam, "update_partno2line");
                    strError = da.Error;
                    return result;
                }
                catch (Exception ex)
                {
                    strError = ex.Message;
                    return -1;
                }

            }

            public int InsertData(object objDomain)
            {
                try
                {
                    objDF = (RekapDefectISO)objDomain;
                    sqlListParam = new List<SqlParameter>();
                    sqlListParam.Add(new SqlParameter("@Partno", objDF.Partno));
                    sqlListParam.Add(new SqlParameter("@Noted", objDF.Noted));
                    sqlListParam.Add(new SqlParameter("@Periode", objDF.Periode));
                    sqlListParam.Add(new SqlParameter("@CreatedBy", objDF.CreatedBy));

                    DataAccess da = new DataAccess(Global.ConnectionString());
                    int result = da.ProcessData(sqlListParam, "insert_partno2line");
                    strError = da.Error;
                    return result;
                }
                catch (Exception ex)
                {
                    strError = ex.Message;
                    return -1;
                }

            }

            public int HapusData(object objDomain)
            {
                try
                {
                    objDF = (RekapDefectISO)objDomain;
                    sqlListParam = new List<SqlParameter>();
                    sqlListParam.Add(new SqlParameter("@Partno", objDF.Partno));
                    sqlListParam.Add(new SqlParameter("@Line", objDF.Line));
                    sqlListParam.Add(new SqlParameter("@Periode", objDF.Periode));
                    sqlListParam.Add(new SqlParameter("@CreatedBy", objDF.CreatedBy));

                    DataAccess da = new DataAccess(Global.ConnectionString());
                    int result = da.ProcessData(sqlListParam, "Hapus_partno2line");
                    strError = da.Error;
                    return result;
                }
                catch (Exception ex)
                {
                    strError = ex.Message;
                    return -1;
                }

            }

            public ArrayList RetrieveLine()
            {
                ArrayList arrData = new ArrayList();
                string strSQL =
                "  select ID,PlantName Line from BM_Plant ";
                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(strSQL);
                if (da.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new RekapDefectISO
                        {
                            Line = sdr["Line"].ToString(),
                            ID = Convert.ToInt32(sdr["ID"])
                        });
                    }
                }
                return arrData;
            }

            public ArrayList RetrieveBulan()
            {
                arrData = new ArrayList();
                string strSQL =
                " select Bulan,case " +
                " when Bulan=1 then 'JANUARI' " +
                " when Bulan=2 then 'FEBRUARI' " +
                " when Bulan=3 then 'MARET' " +
                " when Bulan=4 then 'APRIL' " +
                " when Bulan=5 then 'MEI' " +
                " when Bulan=6 then 'JUNI' " +
                " when Bulan=7 then 'JULI' " +
                " when Bulan=8 then 'AGUSTUS' " +
                " when Bulan=9 then 'SEPTEMBER' " +
                " when Bulan=10 then 'OKTOBER' " +
                " when Bulan=11 then 'NOVEMBER' " +
                " when Bulan=12 then 'DESEMBER' " +
                " end BulanNama from " +
                " (select DISTINCT(MONTH(CreatedTime))Bulan from SPP where LEFT(convert(char,createdtime,112),6)>='201810') as Data1 order by Bulan";
                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(strSQL);

                if (da.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new RekapDefectISO
                        {
                            Bulan = sdr["Bulan"].ToString(),
                            BulanNama = sdr["BulanNama"].ToString()
                        });
                    }
                }
                return arrData;
            }

            public ArrayList RetrieveTahun()
            {
                arrData = new ArrayList();
                string strSQL =
                " select DISTINCT(YEAR(CreatedTime))Tahun from SPP where LEFT(convert(char,createdtime,112),6)>='201810' ";
                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(strSQL);

                if (da.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new RekapDefectISO
                        {
                            Tahun = sdr["Tahun"].ToString()
                        });
                    }
                }
                return arrData;
            }

            public ArrayList RetrieveData(string Flag, string Periode, string Periode2)
            {
                string Query = string.Empty;
                if (Flag == "UK")
                {
                    #region Query Ukuran Khusus
                    Query =
                    ////" with dataUK as ( " +
                    ////" select distinct PartnoAsal Partno from ( " +
                    ////" select x.ItemID,x.PartnoAsal,x.Qty,((A.Tebal*A.Lebar*A.Panjang)/1000000000)*x.Qty M3 " +
                    ////" from (   select PartNo PartnoAsal,ItemID,isnull(SUM(Qty),0)  * -1 Qty from vw_KartuStockBJNew  " +
                    ////" where qty <0 and (process  like '%simetris%' ) and  (Keterangan like '%-P-%' or Keterangan like '%-3-%' or Keterangan like '%-M-%') and " +
                    ////" LokID not In (select ID from FC_Lokasi where lokasi='q99')   and left(convert(char,tanggal,112),6)='" + Periode + "' and ItemID " +
                    ////" in (select ID  from FC_Items where PartNo like'%-1-%') group by ItemID,Keterangan,PartNo,ItemID   ) as x  " +
                    ////" inner join FC_Items A ON A.ID=x.ItemID ) as xx ), "+

                    ////" dataUK1 as (select A.*,isnull((select top 1 B.Line from Def_UKhususProsen B where B.Partno=A.Partno and B.Periode='" + Periode + "' " +
                    ////" and B.Rowstatus>-1 order by ID desc),'?')Line from dataUK A ) " +

                    ////" select * from  dataUK1 order by Partno "; 
                    " with dataP as (select distinct ItemID,PlantID,left(convert(char,tglproduksi,112),6)ThnBln from BM_Destacking where RowStatus>-1 " +
                    " group by ItemID,PlantiD,TglProduksi), " +
                    " dataP1 as (select B.PartNo,PlantID,ThnBln from dataP A inner join FC_Items B ON A.ItemID=B.ID), " +
                    " dataUK as (  select distinct PartnoAsal Partno from (  select x.ItemID,x.PartnoAsal,x.Qty,((A.Tebal*A.Lebar*A.Panjang)/1000000000)*x.Qty M3 " +
                    " from (   select PartNo PartnoAsal,ItemID,isnull(SUM(Qty),0)  * -1 Qty from vw_KartuStockBJNew   " +
                    " where qty <0 and (process  like '%simetris%' ) and  (Keterangan like '%-P-%' or Keterangan like '%-3-%' or Keterangan like '%-M-%') and " +
                    " LokID not In (select ID from FC_Lokasi where lokasi='q99')   and left(convert(char,tanggal,112),6)='" + Periode + "' and ItemID " +
                    " in (select ID  from FC_Items where PartNo like'%-1-%') group by ItemID,Keterangan,PartNo,ItemID   ) as x " +
                    " inner join FC_Items A ON A.ID=x.ItemID ) as xx ),  " +
                    " dataUK01 as (select *,(select top 1 A1.ThnBln from dataP1 A1 where A1.PartNo=A.Partno and A1.ThnBln<=left(convert(char,'" + Periode + "',112),6) order by A1.ThnBln desc)ThnBln from dataUK A) , " +
                    " dataUK02 as (select Partno,ThnBln,PlantID from (select A.*,B.PlantID from dataUK01 A left join dataP1 B ON A.Partno=B.PartNo " +
                    " and A.ThnBln=B.ThnBln ) as x group by Partno,ThnBln,PlantID), " +
                    " dataUK03 as (select *,(select top 1 A1.Line from Def_UKhususProsen A1 where A1.Rowstatus>-1 and  A1.Partno=A.Partno " +
                    " and A1.Periode=left(convert(char,'" + Periode + "',112),6) and A1.Noted is null )PlantID2 from dataUK02 A ), " +
                    " dataUK04 as (select Partno,case when PlantID2 is null then PlantID else PlantID2 end PlantID,case when PlantID2 is null then 'Auto' else 'Manual' end Ket from dataUK03 xx), " +
                    " dataUK1 as (select A.*,isnull((select top 1 B.Line from Def_UKhususProsen B where B.Partno=A.Partno and B.Periode='" + Periode + "' " +
                    " and B.Rowstatus>-1 and B.Noted is null order by ID desc),'?')Line from dataUK A )  " +

                    " select ROW_NUMBER() over (order by Partno asc) as [No],Partno,case when Line is null then 0 else line end Line,Ket from ( " +
                    " select Partno,PlantID Line,Ket from  dataUK04 group by Partno,PlantID,Ket  " +
                    " union all " +
                    " select Partno,Line,''Ket from Def_UKhususProsen where Periode='" + Periode + "' and Noted='Khusus' and Rowstatus>-1 ) as x " +
                    " order by Partno,Line,Ket ";

                    #endregion
                }
                else
                {
                    Query =
                    #region Query ListPlank
                " with dataLP_P as (select distinct ItemID,PlantID,left(convert(char,tglproduksi,112),6)ThnBln from BM_Destacking where RowStatus>-1 group by ItemID,PlantiD,TglProduksi), " +
                    " dataLP_P1 as (select B.PartNo,PlantID,ThnBln from dataLP_P A inner join FC_Items B ON A.ItemID=B.ID), " +
                    " dataLP as ( " +
                    " select distinct PartnoAsal Partno from ( " +
                    " /** 1. Pengeluaran Running Saw **/   " +
                    " select PartnoAsal,isnull((sum(M3)),0)M3  from (  " +
                    " select PartnoAsal,Qty,(((Tebal*Lebar*Panjang)/1000000000)*Qty)  M3  from (  " +
                    " select PartnoAsal,PartNo2,sum(Qty)Qty,Tebal,Lebar,Panjang  from (  " +
                    " select x1.PartNo PartnoAsal,x2.PartNo PartNo2,x2.Tebal,x2.Lebar,x2.Panjang,x.Qty from (  " +
                    " select ItemID0,itemid,Qty * -1 Qty from vw_KartuStockListplank where left(convert(char,tanggal,112),6)='" + Periode + "' and process='runingsaw' " +
                    " and qty<0 ) as x " +
                    " inner join FC_Items x1 ON x.ItemID0=x1.ID " +
                    " inner join FC_Items x2 ON x.ItemID=x2.ID) as A " +
                    " group by PartnoAsal,PartNo2,Tebal,Lebar,Panjang ) as xx ) as xx1  " +
                    " group by PartnoAsal " +
                    " union all " +

                    " /** 2. Saldo Awal Running Saw **/   " +
                    " select PartnoAsal,isnull((sum((Volume*Qty))) ,0) M3  from (  " +
                    " select x2.PartNo PartnoAsal,((x1.Tebal*x1.Lebar*x1.Panjang)/1000000000) Volume,x.Qty  from ( " +
                    " select sum(saldo)Qty ,ItemID,ItemID0 from T1_SaldoListPlank where Process='runingsaw' and thnbln='" + Periode2 + "' and Saldo>0 and ItemID<>ItemID0 " +
                    " group by ItemID,ItemID0 ) as x    " +
                    " inner join FC_Items x1 ON x.ItemID=x1.ID  " +
                    " inner join FC_Items x2 ON x.ItemID0=x2.ID) as A " +
                    " group by PartnoAsal " +
                    " union all " +

                    " /** 3. Saldo Akhir RunningSaw **/   " +
                    " select PartnoAsal,isnull((sum(M3))  * -1,0) M3 from (  " +
                    " select PartnoAsal,Qty,(((Tebal*Lebar*Panjang)/1000000000)*Qty) M3  from ( " +
                    " select PartnoAsal,PartNo2,sum(Qty)Qty,Tebal,Lebar,Panjang  from ( " +
                    " select x1.PartNo PartnoAsal,x2.PartNo PartNo2,x2.Tebal,x2.Lebar,x2.Panjang,x.Qty from (  " +
                    " select sum(Saldo)Qty,ItemID0,ItemID from T1_SaldoListPlank where Process='runingsaw' and thnbln='" + Periode + "' and Saldo>0 and ItemID<>ItemID0  " +
                    " group by ItemID0,ItemID ) as x  " +
                    " inner join FC_Items x1 ON x.ItemID0=x1.ID " +
                    " inner join FC_Items x2 ON x.ItemID=x2.ID) as A " +
                    " group by PartnoAsal,PartNo2,Tebal,Lebar,Panjang ) as xx ) as xx1  " +
                    " group by PartnoAsal " +
                    " union all " +

                    " /** 4. Saldo Awal Bevel **/  " +
                    " select PartnoAsal,isnull((sum(M3)) ,0) M3  from (  " +
                    " select PartnoAsal,Qty,(((Tebal*Lebar*Panjang)/1000000000)*Qty)  M3  from ( " +
                    " select PartnoAsal,PartNo2,sum(Qty)Qty,Tebal,Lebar,Panjang  from (  " +
                    " select x1.PartNo PartnoAsal,x2.PartNo PartNo2,x2.Tebal,x2.Lebar,x2.Panjang,x.Qty from ( " +
                    " select sum(Saldo)Qty,ItemID0,ItemID from T1_SaldoListPlank where Process='bevel' and thnbln='" + Periode2 + "' and Saldo>0 group by ItemID0,ItemID ) as x " +
                    " inner join FC_Items x1 ON x.ItemID0=x1.ID " +
                    " inner join FC_Items x2 ON x.ItemID=x2.ID) as A " +
                    " group by PartnoAsal,PartNo2,Tebal,Lebar,Panjang ) as xx ) as xx1 " +
                    " group by PartnoAsal " +
                    " union all  " +

                    " /** 5. Saldo Akhir Bevel **/   " +
                    " select PartnoAsal,isnull((sum(M3)) * -1,0) M3 from (  " +
                    " select PartnoAsal,Qty,(((Tebal*Lebar*Panjang)/1000000000)*Qty) M3  from ( " +
                    " select PartnoAsal,PartNo2,sum(Qty)Qty,Tebal,Lebar,Panjang  from ( " +
                    " select x1.PartNo PartnoAsal,x2.PartNo PartNo2,x2.Tebal,x2.Lebar,x2.Panjang,x.Qty from ( " +
                    " select sum(Saldo)Qty,ItemID0,ItemID from T1_SaldoListPlank where Process='bevel' and thnbln='" + Periode + "' and Saldo>0 and ItemID<>ItemID0 " +
                    " group by ItemID0,ItemID ) as x  " +
                    " inner join FC_Items x1 ON x.ItemID0=x1.ID " +
                    " inner join FC_Items x2 ON x.ItemID=x2.ID) as A " +
                    " group by PartnoAsal,PartNo2,Tebal,Lebar,Panjang ) as xx ) as xx1  " +
                    " group by PartnoAsal " +
                    " union all " +

                    " /** 6. Saldo Awal Strapping **/   " +
                    " select PartnoAsal,isnull((sum(M3)) ,0) M3 from ( " +
                    " select PartnoAsal,Qty,(((Tebal*Lebar*Panjang)/1000000000)*Qty) M3  from ( " +
                    " select PartnoAsal,PartNo2,sum(Qty)Qty,Tebal,Lebar,Panjang  from (  " +
                    " select x1.PartNo PartnoAsal,x2.PartNo PartNo2,x2.Tebal,x2.Lebar,x2.Panjang,x.Qty from ( " +
                    " select sum(Saldo)Qty,ItemID0,ItemID from T1_SaldoListPlank where Process='straping' and thnbln='" + Periode2 + "' and Saldo>0 " +
                    " group by ItemID0,ItemID ) as x " +
                    " inner join FC_Items x1 ON x.ItemID0=x1.ID " +
                    " inner join FC_Items x2 ON x.ItemID=x2.ID) as A " +
                    " group by PartnoAsal,PartNo2,Tebal,Lebar,Panjang ) as xx ) as xx1 " +
                    " group by PartnoAsal  " +
                    " union all  " +

                    " /** 7. Saldo Akhir Strapping **/   " +
                    " select PartnoAsal,isnull((sum(M3)) * -1,0) M3 from ( " +
                    " select PartnoAsal,Qty,(((Tebal*Lebar*Panjang)/1000000000)*Qty)  M3  from ( " +
                    " select PartnoAsal,PartNo2,sum(Qty)Qty,Tebal,Lebar,Panjang  from (  " +
                    " select x1.PartNo PartnoAsal,x2.PartNo PartNo2,x2.Tebal,x2.Lebar,x2.Panjang,x.Qty from ( " +
                    " select sum(Saldo)Qty,ItemID0,ItemID from T1_SaldoListPlank where Process='straping' and thnbln='" + Periode + "' and Saldo>0 and ItemID<>ItemID0 " +
                    " group by ItemID0,ItemID ) as x  " +
                    " inner join FC_Items x1 ON x.ItemID0=x1.ID " +
                    " inner join FC_Items x2 ON x.ItemID=x2.ID) as A " +
                    " group by PartnoAsal,PartNo2,Tebal,Lebar,Panjang ) as xx ) as xxx " +
                    " group by PartnoAsal " +
                    " union all " +

                    " /** 8. Tahap III OK **/    " +
                    " select PartnoAsal,isnull((sum(M3))*-1 ,0) M3 from ( " +
                    " select PartnoAsal,Qty,(((Tebal*Lebar*Panjang)/1000000000)*Qty) M3  from ( " +
                    " select PartnoAsal,sum(Qty)Qty,Tebal,Lebar,Panjang  from (  " +
                    " select x.PartNo PartnoAsal,x2.Tebal,x2.Lebar,x2.Panjang,x.Qty from ( " +
                    " select sum(Qty)Qty,ItemID,Keterangan Partno from vw_KartuStockBJNew  " +
                    " where sfrom='straping' and left(convert(char,tanggal,112),6)='" + Periode + "' " +
                    " and ItemID in  (select ID from FC_Items where PartNo like'%-M-%' or PartNo like'%-3-%')  group by ItemID,Keterangan ) as x  " +
                    " inner join FC_Items x2 ON x.ItemID=x2.ID) as A " +
                    " group by PartnoAsal,Tebal,Lebar,Panjang ) as xx ) as xx1 " +
                    " group by PartnoAsal " +
                    " union all  " +

                    " /** 9. Tahap III BP **/  " +
                    " select PartnoAsal,isnull((sum(M3)) ,0) M3  from (  " +
                    " select PartnoAsal,Qty,(((Tebal*Lebar*Panjang)/1000000000)*Qty) M3  from (  " +
                    " select PartnoAsal,sum(Qty)Qty,Tebal,Lebar,Panjang  from ( " +
                    " select x.PartNo PartnoAsal,x2.Tebal,x2.Lebar,x2.Panjang,x.Qty from ( " +
                    " select sum(Qty)Qty,ItemID,Keterangan Partno from vw_KartuStockBJNew  " +
                    " where sfrom='straping' and left(convert(char,tanggal,112),6)='" + Periode + "' " +
                    " and  ItemID in (select ID from FC_Items where PartNo like'%-P-%' or PartNo like'%-S-%')  group by ItemID,Keterangan ) as x  " +
                    " inner join FC_Items x2 ON x.ItemID=x2.ID) as A group by PartnoAsal,Tebal,Lebar,Panjang ) as xx ) as xx1 group by PartnoAsal " +
                    " ) as DataFinall " +
                    " group by PartnoAsal ), " +

                    " dataLP01 as (select *,(select top 1 A1.ThnBln from dataLP_P1 A1 where A1.PartNo=A.Partno and A1.ThnBln<=left(convert(char,'" + Periode + "',112),6) order by A1.ThnBln desc)ThnBln from dataLP A) , " +
                    " dataLP02 as (select Partno,ThnBln,PlantID from (select A.*,B.PlantID from dataLP01 A left join dataLP_P1 B ON A.Partno=B.PartNo and A.ThnBln=B.ThnBln ) as x group by Partno,ThnBln,PlantID), " +
                    " dataLP03 as (select *,(select top 1 A1.Line from Def_UKhususProsen A1 " +
                    " where A1.Noted is null and A1.rowstatus>-1 and A1.Partno=A.Partno and A1.Periode=left(convert(char,'" + Periode + "',112),6))PlantID2 from dataLP02 A ), " +
                    " dataLP04 as (select Partno,case when PlantID2 is null then PlantID else PlantID2 end Line,case when PlantID2 is null then 'Auto' else 'Manual' end Ket from dataLP03 xx) " +

                    " select ROW_NUMBER() over (order by Partno asc) as [No],Partno,case when Line is null then 0 else line end Line,Ket from ( " +
                    " select * from  dataLP04 group by Partno,Line,Ket " +
                    " union all " +
                    " select Partno,Line,''Ket from Def_UKhususProsen where Periode='" + Periode + "' and Noted='LP' and Rowstatus>-1 ) as x order by Partno,Line,Ket ";

                    #endregion
                }

                arrData = new ArrayList();
                string strSQL = Query;


                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(strSQL);
                if (da.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new RekapDefectISO
                        {
                            Ket = sdr["Ket"].ToString(),
                            No = sdr["No"].ToString(),
                            Partno = sdr["Partno"].ToString(),
                            Line = sdr["Line"].ToString()
                        });
                    }
                }
                return arrData;
            }
        }
    }
}