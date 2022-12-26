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
    public partial class POSerahTerima : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Users usr = (Users)Session["Users"];
                LoadBulan();
                LoadTahun();
                string[] UserSerah = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("POUserSerah", "SerahTerimaDocument").Split(',');
                string[] Userterima = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("POUserTerima", "SerahTerimaDocument").Split(',');
                string[] DeptView = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("PODeptView", "SerahTerimaDocument").Split(',');
                if ((Posisi(DeptView, usr.DeptID.ToString()) > -1 || Posisi(Userterima, usr.UserID.ToString()) > -1) &&
                    Posisi(UserSerah, usr.UserID.ToString()) == -1)
                {
                    ListDocKirim();
                    btnExport.Visible = true;
                    frm10.Visible = (Posisi(Userterima, usr.UserID) > -1) ? false : true;
                    string heights = (Posisi(Userterima, usr.UserID) > -1) ? "height:450px" : "height:485px";
                    lst.Attributes.Add("style", heights);
                    btnList.Visible = (Posisi(Userterima, usr.UserID) > -1) ? true : false;
                }
                else if (Posisi(UserSerah, usr.UserID.ToString()) > -1)
                {
                    ListDocSiapKirim();
                    btnExport.Visible = true;
                    frm10.Visible = false;
                    btnList.Visible = true;
                }

            }
         ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }
        private decimal total;
        private decimal ok;
        private decimal noke;
        private decimal prosen;
        private void ListDocKirim()
        {
            // total = 0; 
            string[] ViewSummary = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("PODeptView", "SerahTerimaDocument").Split(',');
            int dept = Array.IndexOf(ViewSummary, ((Users)Session["Users"]).DeptID.ToString());
            ok = 0; noke = 0;
            Session["summary"] = (dept > -1) ? "Tampil" : "Tidak";
            ArrayList arrData = new ArrayList();
            arrData = new PantauSerah().Retrieve(int.Parse(ddlBulan.SelectedValue), int.Parse(ddlTahun.SelectedValue), btnBack.Visible);
            total = arrData.Count;
            lstSerah.DataSource = arrData;
            lstSerah.DataBind();

        }
        private void ListDocSiapKirim()
        {
            Session["summary"] = "Tidak";
            ArrayList arrData = new ArrayList();
            arrData = new PantauSerah().Retrieve();
            total = arrData.Count;
            lstSerah.DataSource = arrData;
            lstSerah.DataBind();
        }
        protected void lstSerah_DataBound(object sender, RepeaterItemEventArgs e)
        {

            string[] UserSerah = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("POUserSerah", "SerahTerimaDocument").Split(',');
            string[] Userterima = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("POUserTerima", "SerahTerimaDocument").Split(',');
            string SelisihHari = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("SelisihHari", "SerahTerimaDocument");
            Pantau p = (Pantau)e.Item.DataItem;
            Users usr = (Users)Session["Users"];
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TextBox txtKet = (TextBox)e.Item.FindControl("txtKet");
                Image Simpan = (Image)e.Item.FindControl("btnSimpan");
                Image Kirim = (Image)e.Item.FindControl("btnKirim");
                Image Terima = (Image)e.Item.FindControl("btnterima");
                Image btnUpdate = (Image)e.Item.FindControl("btnUpd");
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
                txtKet.Text = ((p.TglTerima.Year < (DateTime.Now.Year - 1))) ? "" : p.Keterangan;
                if (p.TglKirim.Year < (DateTime.Now.Year - 1))
                {
                    double libur = new PantauSerah().cekHariLibur(p.POPurchnDate.ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd"));
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
                    double libure = new PantauSerah().cekHariLibur(p.POPurchnDate.ToString("yyyyMMdd"), p.TglKirim.ToString("yyyyMMdd"));
                    double Selisih = Math.Floor((p.TglKirim - p.POPurchnDate).TotalDays);
                    //libure = (libure);
                    ((Label)e.Item.FindControl("txtSesuai")).Text = ((Selisih - libure) >= int.Parse(SelisihHari)) ? "" : "X";
                    ((Label)e.Item.FindControl("txtTidak")).Text = ((Selisih - libure) >= int.Parse(SelisihHari)) ? "X" : "";
                    lblKet.Text = (Selisih - libure >= int.Parse(SelisihHari)) ? txtKet.Text : "";
                    lKirim.ToolTip = "Selisih : " + (Selisih - libure).ToString();
                    ((Label)e.Item.FindControl("txtSelisih")).Text = (Selisih - libure).ToString();
                }
                //total = total + 1;
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
                e.Item.Visible = (Session["summary"].ToString() == "Tampil") ? true : false;
            }
        }
        protected void lstSerah_Command(object sender, RepeaterCommandEventArgs e)
        {
            string index = e.CommandArgument.ToString();
            Label tidak = (Label)e.Item.FindControl("txtTidak");
            PantauSerah ps = new PantauSerah();
            TextBox txtKet = (TextBox)e.Item.FindControl("txtKet");
            switch (e.CommandName)
            {
                case "simpan":
                    break;
                case "kirim":
                    if (tidak.Text == "X" && txtKet.Text == string.Empty)
                    {
                        DisplayAJAXMessage(this, "Tanggal Kirim Document lebih dari H+2, keterangan harus di isi");
                        txtKet.Focus();
                        return;
                    }
                    else
                    {
                        ArrayList arrData = new ArrayList();
                        arrData = ps.Retrieve(int.Parse(index));
                        foreach (Pantau p in arrData)
                        {
                            Pantau pn = new Pantau();
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
                    Pantau pun = new Pantau();
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
                    Pantau edit = new Pantau();
                    edit.LastModifiedBy = ((Users)Session["Users"]).UserID.ToString();
                    edit.ID = int.Parse(index);
                    edit.Keterangan = txtKet.Text;
                    int result2 = ps.UpdateKet(edit);
                    if (result2 > 0)
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
            frm10.Visible = true;
            btnList.Visible = false;
            btnBack.Visible = true;
            lst.Attributes.Add("style", "height:425px");
            ListDocKirim();
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            lst.Attributes.Add("style", "height:485px");
            Response.Redirect("POSerahTerima.aspx?t=po");
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
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
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
        }
        private void LoadTahun()
        {
            ArrayList arrData = new ArrayList();
            arrData = new PantauSerah().GetTahun();
            ddlTahun.Items.Clear();
            foreach (Pantau p in arrData)
            {
                ddlTahun.Items.Add(new ListItem(p.Tahun.ToString(), p.Tahun.ToString()));
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }
    }
}

public class PantauSerah : AbstractFacade
{
    private ArrayList arrData = new ArrayList();
    private Pantau panto = new Pantau();
    private List<SqlParameter> sqlListParam;
    public PantauSerah() : base()
    {

    }
    public override int Insert(object objDomain)
    {
        int result = 0;
        try
        {
            panto = (Pantau)objDomain;
            sqlListParam = new List<SqlParameter>();
            sqlListParam.Add(new SqlParameter("@NoPO", panto.NoPO));
            sqlListParam.Add(new SqlParameter("@NoSPP", panto.NoSPP));
            sqlListParam.Add(new SqlParameter("@TglPO", panto.POPurchnDate));
            sqlListParam.Add(new SqlParameter("@NoRMS", panto.ReceiptNo));
            sqlListParam.Add(new SqlParameter("@ReceiptID", panto.ReceiptID));
            sqlListParam.Add(new SqlParameter("@ItemID", panto.ItemID));
            sqlListParam.Add(new SqlParameter("@TglRMS", panto.TglRMS));
            sqlListParam.Add(new SqlParameter("@Keterangan", panto.Keterangan));
            sqlListParam.Add(new SqlParameter("@CreatedBy", panto.CreatedBy));
            sqlListParam.Add(new SqlParameter("@SupplierName", panto.SupplierName));
            result = dataAccess.ProcessData(sqlListParam, "spSerahTerima_Insert");
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
            panto = (Pantau)objDomain;
            sqlListParam = new List<SqlParameter>();
            sqlListParam.Add(new SqlParameter("@ID", panto.ID));
            sqlListParam.Add(new SqlParameter("@ModifiedBy", panto.LastModifiedBy));
            sqlListParam.Add(new SqlParameter("@RowStatus", panto.RowStatus));
            result = dataAccess.ProcessData(sqlListParam, "spSerahTerima_Update");
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
            panto = (Pantau)objDomain;
            sqlListParam = new List<SqlParameter>();
            sqlListParam.Add(new SqlParameter("@ID", panto.ID));
            sqlListParam.Add(new SqlParameter("@ModifiedBy", panto.LastModifiedBy));
            sqlListParam.Add(new SqlParameter("@Keterangan", panto.Keterangan));
            result = dataAccess.ProcessData(sqlListParam, "spSerahTerima_Update_Ket");
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
        string TglMulai = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("POAwalMulai", "SerahTerimaDocument");
        string ItemCode = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ItemCheckedCode", "PO");
        ItemCode = ItemCode.Replace(",", "','");
        string strSQL = "SELECT rd.ID, p.POPurchnDate TglPO,p.NoPO,pd.DocumentNo NoSPP,s.SupplierName,rd.ReceiptID,rd.ItemID," +
                        "r.ReceiptDate tglRMS,r.ReceiptNo NoRMS,rd.Keterangan " +
                        "FROM ReceiptDetail  rd " +
                        "LEFT JOIN Receipt AS r ON r.ID=rd.ReceiptID " +
                        "LEFT JOIN POPurchn AS p ON p.ID=rd.POID " +
                        "LEFT JOIN POPurchnDetail AS pd ON pd.ID=rd.PODetailID " +
                        "LEFT JOIN SuppPurch AS s ON s.ID=p.SupplierID " +
                        "WHERE ReceiptID  " +
                        "in(SELECT ID FROM Receipt WHERE CONVERT(CHAR,r.ReceiptDate,112)>='" + TglMulai + "'  " +
                        "and DATEDIFF(DAY,p.POPurchnDate,GETDATE())>=0 and ID not in( " +
                        "(SELECT ReceiptID FROM SerahTerima WHERE RowStatus>-1)) " +
                        ") and rd.ItemID in(Select Distinct ItemID from POPurchnKadarAir) and rd.RowStatus>-1 " +
                        " AND rd.ItemTypeID=1 AND rd.GroupID in(1,2,11) and CONVERT(CHAR,p.POPurchnDate,112)>'20180917' " +
                        "ORDER BY rd.ID ";
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
    public ArrayList Retrieve(int ID)
    {
        arrData = new ArrayList();
        string strSQL = "SELECT rd.ID, p.POPurchnDate TglPO,p.NoPO,pd.DocumentNo NoSPP,s.SupplierName,rd.ReceiptID, " +
                        "r.ReceiptDate tglRMS,rd.ItemID,r.ReceiptNo NoRMS,rd.Keterangan " +
                        "FROM ReceiptDetail  rd " +
                        "LEFT JOIN Receipt AS r ON r.ID=rd.ReceiptID " +
                        "LEFT JOIN POPurchn AS p ON p.ID=rd.POID " +
                        "LEFT JOIN POPurchnDetail AS pd ON pd.ID=rd.PODetailID " +
                        "LEFT JOIN SuppPurch AS s ON s.ID=p.SupplierID " +
                        "WHERE rd.ID=" + ID +
                        "ORDER BY rd.ID ";
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
    public ArrayList Retrieve(int Bulan, int Tahun, bool List)
    {
        string[] Userterima = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("POUserTerima", "SerahTerimaDocument").Split(',');
        arrData = new ArrayList();
        int pos = Array.IndexOf(Userterima, ((Users)HttpContext.Current.Session["Users"]).UserID);
        string strSQL = (pos == -1 || List == true) ? "Select * from SerahTerima where RowStatus>-1 and Month(TglPO)=" + Bulan + " and YEAR(TglPO)=" + Tahun +
            " order by TglPO" :
            "Select * from SerahTerima where RowStatus >-1 and (Tglterima is null or TglTerima='') order by TglPO";
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
        string strSql = "Select Distinct YEAR(ReceiptDate)Tahun FROM Receipt Where YEAR(ReceiptDate)is not null order by YEAR(ReceiptDate)";
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(strSql);
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrData.Add(new Pantau { Tahun = int.Parse(sdr["Tahun"].ToString()) });
            }
        }
        return arrData;
    }
    private Pantau GenerateObject(SqlDataReader sdr)
    {
        panto = new Pantau();
        panto.ID = int.Parse(sdr["ID"].ToString());
        panto.NoPO = sdr["NoPO"].ToString();
        panto.Keterangan = sdr["Keterangan"].ToString();
        panto.SupplierName = sdr["SupplierName"].ToString();
        panto.NoSPP = sdr["NoSPP"].ToString();
        panto.POPurchnDate = DateTime.Parse(sdr["TglPO"].ToString());
        panto.ReceiptNo = sdr["NoRMS"].ToString();
        panto.ReceiptID = int.Parse(sdr["ReceiptID"].ToString());
        panto.ItemID = int.Parse(sdr["ItemID"].ToString());
        panto.TglRMS = DateTime.Parse(sdr["TglRMS"].ToString());
        return panto;
    }
    private Pantau GenerateObject(SqlDataReader sdr, Pantau objDomain)
    {
        panto = (Pantau)objDomain;
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
public class Pantau : GRCBaseDomain
{
    public DateTime POPurchnDate { get; set; }
    public string NoPO { get; set; }
    public string NoSPP { get; set; }
    public string Keterangan { get; set; }
    public string ReceiptNo { get; set; }
    public string SupplierName { get; set; }
    public int ItemID { get; set; }
    public int ReceiptID { get; set; }
    public DateTime TglKirim { get; set; }
    public DateTime TglTerima { get; set; }
    public string Sesuai { get; set; }
    public string Tidak { get; set; }
    public DateTime TglRMS { get; set; }
    public int Tahun { get; set; }
    public int Bulan { get; set; }
}