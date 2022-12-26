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

namespace GRCweb1.Modul.Purchasing
{
    public partial class KasbonRealisasiApproval : System.Web.UI.Page
    {
        public string[] KasbonStatus = new string[] { "OPEN", "Approval Head", "Approval Finance" };
        public string OpenKasbon { get; set; }
        public decimal gHarga = 0;
        public decimal totalall = 0;
        public decimal gTotal = 0;
        public string ItemCD = "";
        public decimal DanaCadangan = 0;
        public decimal ppn = 0;
        public decimal gppn = 0;
        public decimal OngkosKirim = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Users users = ((Users)Session["Users"]);
                if (users.DeptID == 15 || users.DeptID == 12 && users.Apv > 0)
                {
                    LoadOpenKasbon();
                }
                if (noKasbon.Value != "0")
                {
                    string[] ListOpenKasbon = noKasbon.Value.Split(',');
                    string[] ListK = ListOpenKasbon.Distinct().ToArray();
                    int idx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString());
                    LoadOpenKasbon(ListK[idx].ToString());
                    btnApprove.Enabled = true;
                    btnNext.Enabled = (ListK.Count() > 1) ? true : false;
                    ViewState["index"] = idx;

                    int CountWO = ListK.Count();
                    int IndexWO = int.Parse(ViewState["index"].ToString()) + 1;

                    txtCount.Text = IndexWO + "/" + CountWO;
                }
                else
                {
                    btnApprove.Enabled = false;
                    btnNext.Enabled = false;
                }
            }
            string[] ListOpenPOx = noKasbon.Value.Split(',');
            string[] ListOpenPOd = ListOpenPOx.Distinct().ToArray();
            int idxx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString());
            btnNext.Enabled = ((idxx - 1) >= ListOpenPOd.Count()) ? false : true;
            ViewState["index"] = idxx;
            //btnNext.Enabled = ((idxx) == ListOpenKasbonx.Count()) ? false : true;
            txtNotApproved.Attributes.Add("onkeyup", "onKeyUp()");
            if (Request.QueryString["KasbonNo"] != null)
            {
                LoadOpenKasbon(Request.QueryString["KasbonNo"].ToString());
            }
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            //LoadOpenKasbon();
            string[] ListOpenKasbon = noKasbon.Value.Split(',');
            int idx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString()) - 1;
            idx = (idx < 0) ? 0 : idx;
            LoadOpenKasbon(ListOpenKasbon[idx].ToString());
            btnPrev.Enabled = (idx > 0) ? true : false;
            btnNext.Enabled = ((idx + 1) == ListOpenKasbon.Count()) ? false : true;
            ViewState["index"] = idx;
            if (ListOpenKasbon.Count() == 0) Response.Redirect("KasbonApproval.aspx");
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                //LoadOpenKasbon();
                string[] ListSPP = noKasbon.Value.Split(',');
                string[] ListOpenPO = ListSPP.Distinct().ToArray();

                int idx5 = int.Parse(ViewState["index"].ToString()) + 1;

                int idx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString()) + 1;

                try
                {
                    if (idx5 == 1)
                    {
                        int idx2 = 1;
                        ViewState["index"] = idx2;
                    }
                    else if (idx5 > 1)
                    {
                        int idx2 = idx;
                        ViewState["index"] = idx2;
                    }

                    int idx3 = int.Parse(ViewState["index"].ToString());

                    LoadOpenKasbon(ListOpenPO[idx3].ToString());
                    ViewState["index"] = idx;
                    //LoadOpenKasbon(ListOpenPO[idx3].ToString());

                }
                catch
                {
                    LoadOpenKasbon(ListOpenPO[0].ToString());
                    ViewState["index"] = 0;
                }
                btnPrev.Enabled = (idx5 > 0) ? true : false;
            }
            catch { }
        }
        private void LoadOpenKasbon()
        {
            Users users = (Users)Session["Users"];
            KasbonFacade kasbonFacade = new KasbonFacade();
            KasbonRApprovalFacade kasbonApp = new KasbonRApprovalFacade();
            ArrayList arrKasbon = new ArrayList();

            string AppGroup = kasbonApp.GetAppGroup(users.ID);
            string AppPIC = kasbonApp.GetAppPIC(users.ID, users.Apv);

            if (users.DeptID == 15 && users.Apv > 1)
            {
                decimal Total = Convert.ToDecimal(kasbonFacade.GetAppWithTotalRealisasi1(users.ID, users.Apv));
                int Apv = (users.Apv > 2 || users.DeptID == 12) ? 3 : 3;
                kasbonApp.Criteria = " and (AlasanNotApproved='' or AlasanNotApproved is null) and K.Approval =" + Apv + " AND KD.PODetailID!=0 ";
                kasbonApp.GroupApp = (AppPIC != string.Empty) ? " and k.Pic in(" + AppPIC + ")" : string.Empty;
                kasbonApp.GroupApv = (users.DeptID == 15 && Total > 3000000 && users.Apv > 1) ? "and k.ApvMgr=1" : string.Empty;
                kasbonApp.OrderBy = (users.Apv > 2 || users.DeptID == 12) ? "Order By ID Desc" : " Order By K.NoPengajuan desc ";
                arrKasbon = kasbonApp.RetrieveOpenRealisasi2();
            }
            else
            {
                decimal Total = Convert.ToDecimal(kasbonFacade.GetAppWithTotalRealisasi(users.ID, users.Apv));
                decimal Total1 = Convert.ToDecimal(kasbonFacade.GetAppWithTotalRealisasi2(users.ID, users.Apv));
                int Apv = (users.Apv > 2 || users.DeptID == 12) ? 4 : 3;
                kasbonApp.Criteria = (users.DeptID == 12 && Total < 3000000) ? " and (AlasanNotApproved='' or AlasanNotApproved is null) and K.Approval =" + Apv + " AND KD.PODetailID!=0 " : string.Empty;
                kasbonApp.Criteria = (users.DeptID == 12 && Total1 > 3000000) ? "and k.ApvMgr=2" : " and (AlasanNotApproved='' or AlasanNotApproved is null) and K.Approval =" + (Apv - 1) + " AND KD.PODetailID!=0";
                kasbonApp.GroupApp = (AppPIC != string.Empty) ? " and k.Pic in(" + AppPIC + ")" : string.Empty;
                kasbonApp.GroupApv = (users.DeptID == 15 && Total > 3000000 && users.Apv > 1) ? "and k.ApvMgr=1" : string.Empty;
                kasbonApp.OrderBy = (users.Apv > 2 || users.DeptID == 12) ? "Order By ID Desc" : " Order By K.NoPengajuan desc ";
                arrKasbon = kasbonApp.RetrieveOpenRealisasi();
            }
            noKasbon.Value = "";

            foreach (Kasbon kasbon in arrKasbon)
            {
                if (kasbon.NoKasbon != "")
                {
                    noKasbon.Value += kasbon.NoKasbon + ",";
                }
            }
            noKasbon.Value = (noKasbon.Value != string.Empty) ? noKasbon.Value.Substring(0, (noKasbon.Value.Length - 1)) : "0";
        }

        private void LoadOpenKasbon(string NoKasbon)
        {
            Users users = (Users)Session["Users"];
            KasbonRApprovalFacade kasbonApp = new KasbonRApprovalFacade();
            string AppGroup = kasbonApp.GetAppGroup(users.ID);
            string AppPIC = kasbonApp.GetAppPIC(users.ID, users.Apv);
            kasbonApp.Criteria = " and KasbonNo='" + NoKasbon + "'";
            kasbonApp.Criteria += " and (AlasanNotApproved='' or AlasanNotApproved is null)";
            kasbonApp.Criteria += (AppGroup != string.Empty) ? " and k.Pic in(" + AppPIC + ")" : string.Empty;
            ArrayList arrKasbon = new ArrayList();
            arrKasbon = kasbonApp.RetrieveOpenRealisasi();
            //Kasbon kasbon = kasbonApp.RetrieveKasbonNO(NoKasbon);
            if (txtCari.Text != "Find by Nomor Kasbon" && arrKasbon.Count == 0)
            {
                DisplayAJAXMessage(this, "Nomor Kasbon tidak ada / Nomor Kasbon Salah / Nomor Kasbon Sudah di approve");
                return;
            }
            foreach (Kasbon kasbon in arrKasbon)
            {
                if (kasbon.Apv == users.Apv || kasbon.Apv < 0)
                {
                    if (txtCari.Text == kasbon.NoKasbon)
                    {
                        DisplayAJAXMessage(this, "No Kasbon Sudah di Approved");
                        return;
                    }
                    AutoNext();
                }
                txtNoKasbon.Text = NoKasbon;
                txtTglKasbon.SelectedDate = Convert.ToDateTime(kasbon.KasbonDate.ToString("dd-MMM-yyyy"));
                txtDept.Text = "Purchasing";
                txtPic.Text = kasbon.PIC;
                txtDanaCadangan.Text = kasbon.DanaCadangan.ToString("###,##0.#0");
                txtStatus.Text = (kasbon.Apv == 2) ? "Open" : "Approved Head";
                txtNotApproved.Text = kasbon.AlasanNotApproval.ToString();
            }
            LoadDetailKasbon(txtNoKasbon.Text);
        }

        private void LoadDetailKasbon(string NoKasbon)
        {
            KasbonDetailFacade kd = new KasbonDetailFacade();
            kd.Where = " and kd.Status>-1";
            ArrayList arrData = new ArrayList();
            arrData = kd.RetrieveKID(NoKasbon);
            lstItemKasbon.DataSource = arrData;
            lstItemKasbon.DataBind();
        }

        protected void txtNotApproved_Change(object sender, EventArgs e)
        {
            btnApprove.Enabled = (txtNotApproved.Text.Length > 0) ? false : true;
            btnNotApprove.Enabled = (txtNotApproved.Text.Length > 0) ? true : false;
        }

        private void ApprovalKasbon(bool NotApproval)
        {
            if (txtNotApproved.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Alasan Not Approval tidak boleh kosong");
                return;
            }

            string strError = string.Empty;
            KasbonFacade kasbonFacade = new KasbonFacade();
            Kasbon kasbon = kasbonFacade.RetrieveByNo(txtNoKasbon.Text);
            Users users = (Users)Session["Users"];
            if (kasbonFacade.Error == string.Empty && kasbon.ID > 0)
            {
                kasbon.AlasanNotApproval = txtNotApproved.Text;
                kasbon.Apv = 0;
                kasbon.CreatedBy = users.UserName;
                //kembali ke status Head Purchasing

                //KasbonApprovalFacade kasbonApprovalStatusFacade = new KasbonApprovalFacade(kasbon, new SPP());
                if (kasbon.ID > 0)
                {
                    //strError = kasbonApprovalStatusFacade.UpdateNotApproval();
                    ZetroView zl = new ZetroView();
                    zl.QueryType = Operation.CUSTOM;
                    zl.CustomQuery = "UPDATE Kasbon set Status=-1, AlasanaNotApproved=" + kasbon.AlasanNotApproval + " WHERE ID=" + kasbon.ID + " ";
                    SqlDataReader sdr = zl.Retrieve();
                    if (strError == string.Empty)
                    {
                        AutoNext();
                    }
                }
                Response.Redirect("KasbonApproval.aspx");
            }
        }
        protected void lstItemKasbon_DataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                KasbonDetail kd = (KasbonDetail)e.Item.DataItem;
                int id = kd.ID;
                string[] ListOpenKasbon = noKasbon.Value.Split(',');
                decimal tQty = Convert.ToDecimal(kd.Qty.ToString("###,##0.#0"));
                decimal tEstimasi = Convert.ToDecimal(kd.EstimasiKasbon.ToString("###,##0.#0"));
                decimal tharga = tQty * tEstimasi;
                decimal tQtyPO = Convert.ToDecimal(kd.QtyPO.ToString("###,##0.#0"));
                decimal tprice = Convert.ToDecimal(kd.Price.ToString("###,##0.#0"));
                decimal tPO = tQtyPO * tprice;
                decimal Ppn = kd.PPN;
                decimal ongkoskirim = kd.OngkosKirim;
                ((Label)e.Item.FindControl("total")).Text = tharga.ToString("###,##0.#0");
                ((Label)e.Item.FindControl("totalPO")).Text = tPO.ToString("###,##0.#0");
                ((Label)e.Item.FindControl("selisih")).Text = (tharga - tPO).ToString("###,##0.#0");
                gHarga += tharga;
                ppn = Ppn;
                OngkosKirim = ongkoskirim;
                totalall += tPO;
                gppn += tPO * (ppn / 100) + OngkosKirim;
                ItemCD = kd.ItemCode;
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                DanaCadangan = Convert.ToDecimal(txtDanaCadangan.Text);
                ((Label)e.Item.FindControl("gTotal")).Text = gHarga.ToString("###,##0.#0");
                gTotal = gHarga + DanaCadangan;
                ((Label)e.Item.FindControl("gDC")).Text = DanaCadangan.ToString("###,##0.#0");
                ((Label)e.Item.FindControl("grnTotal")).Text = gTotal.ToString("###,##0.#0");

                ((Label)e.Item.FindControl("Ppn")).Text = ppn.ToString("###,##0.#0") + " %";
                ((Label)e.Item.FindControl("OngkosKirim")).Text = OngkosKirim.ToString("###,##0.#0");
                ((Label)e.Item.FindControl("TotalPO")).Text = (totalall + gppn).ToString("###,##0.#0");
                ((Label)e.Item.FindControl("TotalAll")).Text = (gTotal - (totalall + gppn)).ToString("###,##0.#0");
            }
        }

        private void AutoNext()
        {
            if (btnNext.Enabled == true)
            {
                btnNext_Click(null, null);
            }
            else if (btnPrev.Enabled == true)
            {
                btnPrev_Click(null, null);
            }
            else
            {
                Response.Redirect("KasbonApproval.aspx");
            }
        }
        protected void btnNotApprove_Click(object sender, EventArgs e)
        {
            NotApprovalKasbon(true);
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            ApprovalKasbon();
        }
        private void ApprovalKasbon()
        {
            Users users = (Users)Session["Users"];
            KasbonFacade kasbonFacade = new KasbonFacade();
            if (txtNoKasbon.Text != string.Empty)
            {
                //Kasbon kasbon = kasbonFacade.RetrieveNoKasbon(txtNoKasbon.Text);
                Kasbon kasbon = kasbonFacade.RetrieveNoKasbon1(txtNoKasbon.Text);
                string strError = string.Empty;
                if (kasbon.ID > 0)
                {
                    ZetroView zl = new ZetroView();
                    zl.QueryType = Operation.CUSTOM;
                    if (users.DeptID == 15)
                    {
                        if (users.Apv == 1)
                        {
                            zl.CustomQuery = "UPDATE Kasbon set Approval=3, LastModifiedBy='" + users.UserName + "', ApprovedDate3=getdate() WHERE " +
                                             "ID=" + kasbon.ID + " ";
                            SqlDataReader sdr = zl.Retrieve();
                        }
                        if (users.Apv == 2)
                        {
                            zl.CustomQuery = "UPDATE Kasbon set ApvMgr=2, LastModifiedBy='" + users.UserName + "', ApprovedDateMgr2=getdate() WHERE " +
                                             "ID=" + kasbon.ID + " ";
                            SqlDataReader sdr = zl.Retrieve();
                        }
                    }
                    //if (users.Apv == 2)
                    ZetroView zl2 = new ZetroView();
                    zl2.QueryType = Operation.CUSTOM;
                    if (users.DeptID == 12)
                        zl2.CustomQuery = "UPDATE Kasbon set Approval=4, LastModifiedBy='" + users.UserName + "', ApprovedDate4=getdate() WHERE " +
                                          "ID=" + kasbon.ID + " ";
                    SqlDataReader sdr2 = zl2.Retrieve();
                }
                if (strError == string.Empty)
                {
                    if (users.Apv < 2)
                    {
                        //LoadOpenPO();
                        btnNext_Click(null, null);
                    }
                    else
                    {
                        AutoNext();
                    }
                }
                else
                {
                    DisplayAJAXMessage(this, strError);
                }
            }
            Response.Redirect("KasbonRealisasiApproval.aspx");
        }
        private void NotApprovalKasbon(bool NotApproval)
        {
            if (txtNotApproved.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Alasan Not Approval tidak boleh kosong");
                return;
            }

            string strError = string.Empty;
            KasbonFacade kasbonFacade = new KasbonFacade();
            Kasbon kasbon = kasbonFacade.RetrieveNoKasbon(txtNoKasbon.Text);
            Users users = (Users)Session["Users"];
            if (kasbonFacade.Error == string.Empty && kasbon.ID > 0)
            {
                kasbon.AlasanNotApproval = txtNotApproved.Text;
                kasbon.Apv = 0;
                kasbon.CreatedBy = users.UserName;
                //kembali ke status Head Purchasing

                //KasbonApprovalFacade kasbonApprovalStatusFacade = new KasbonApprovalFacade(kasbon, new SPP());
                if (kasbon.ID > 0)
                {
                    //strError = kasbonApprovalStatusFacade.UpdateNotApproval();
                    ZetroView zl = new ZetroView();
                    zl.QueryType = Operation.CUSTOM;
                    zl.CustomQuery = "UPDATE Kasbon set Status=-1, AlasanaNotApproved=" + kasbon.AlasanNotApproval + " WHERE ID=" + kasbon.ID + " ";
                    SqlDataReader sdr = zl.Retrieve();
                    if (strError == string.Empty)
                    {
                        AutoNext();
                    }
                }
                Response.Redirect("KasbonRealisasiApproval.aspx");
            }
        }
        protected void btnCari_Click(object sender, EventArgs e)
        {
            LoadOpenKasbon(txtCari.Text);
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
    }
}

public class KasbonRApprovalFacade : KasbonFacade
{
    Kasbon objKasbon = new Kasbon();
    ArrayList arrData = new ArrayList();
    public string Criteria { get; set; }
    public string OrderBy { get; set; }
    public string GroupApp { get; set; }
    public string GroupApv { get; set; }

    public ArrayList RetrieveOpenRealisasi()
    {
        arrData = new ArrayList();
        string strSQL = "SELECT DISTINCT TOP 100 K.* " +
                      "FROM Kasbon K  " +
                      "LEFT JOIN KasbonDetail KD ON KD.KID=K.ID left join SPP AS s ON s.ID=kd.SPPID " +
                      "WHERE K.Status>-1 and KD.Status>-1 " + this.Criteria + this.GroupApp + this.GroupApv +
                      this.OrderBy;
        DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(strSQL);
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrData.Add(GetObject(sdr, GenerateObjectNodetail(sdr)));
            }
        }
        return arrData;
    }

    public ArrayList RetrieveOpenRealisasi2()
    {
        arrData = new ArrayList();
        string strSQL = "select * from ( " +
                        "SELECT TOP 100 k.ID,k.KasbonNo,k.NoPengajuan,k.DeptID,k.CreatedTime,k.Pic,k.status,k.Approval,k.TglKasbon,k.DanaCadangan, " +
                        "k.KasbonType,case when (sum(kd.Qty * kd.EstimasiKasbon))+k.DanaCadangan = null then 0 " +
                        "else (sum(kd.Qty * kd.EstimasiKasbon))+k.DanaCadangan end as Total FROM Kasbon K  LEFT JOIN KasbonDetail KD ON KD.KID=K.ID " +
                        "LEFT JOIN SPP AS s ON s.ID=kd.SPPID WHERE K.ApvMgr=1 and KD.Status>-1 " + this.Criteria + this.GroupApp + this.GroupApv +
                        "group by k.ID,k.KasbonNo,k.NoPengajuan,k.DeptID,k.CreatedTime,k.Pic,k.DanaCadangan,k.status,k.Approval,k.TglKasbon, " +
                        "k.KasbonType " + this.OrderBy + " ) a where Total>3000000 ";
        DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(strSQL);
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrData.Add(GetObject(sdr, GenerateObjectNodetail(sdr)));
            }
        }
        return arrData;
    }

    public Kasbon RetrieveKasbonNO(string NOKasbon)
    {
        string strSQL =
        "SELECT DISTINCT TOP 100 K.* " +
                      "FROM Kasbon K  " +
                      "LEFT JOIN KasbonDetail KD ON KD.KID=K.ID left join SPP AS s ON s.ID=kd.SPPID " +
                      "WHERE K.KasbonNo='" + NOKasbon + "' and K.Status=0 and KD.Status>-1 ";

        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        strError = dataAccess.Error;
        arrData = new ArrayList();

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return GeneratUserObject(sqlDataReader);
            }
        }

        return new Kasbon();
    }

    public string GetAppGroup(int IDUsers)
    {
        string result = string.Empty;
        try
        {
            string strSQL = "Select AppGroup from UsersApp where RowStatus>-1 and UserID=" + IDUsers.ToString();
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = sdr["AppGroup"].ToString();
                }
            }
            return result;
        }
        catch
        {
            return result;
        }
    }

    public Kasbon GeneratUserObject(SqlDataReader sdr)
    {
        objKasbon = new Kasbon();
        objKasbon.NoKasbon = sdr["KasbonNo"].ToString();
        objKasbon.PIC = sdr["Pic"].ToString();
        objKasbon.DanaCadangan = Convert.ToDecimal(sdr["DanaCadangan"]);
        objKasbon.DeptID = Convert.ToInt32(sdr["DeptID"]);
        objKasbon.Apv = Convert.ToInt32(sdr["Approval"]);
        return objKasbon;
    }
    public Kasbon GetObject(SqlDataReader sdr, Kasbon objP)
    {
        objKasbon = (Kasbon)objP;
        objKasbon.NoKasbon = sdr["KasbonNo"].ToString();
        objKasbon.PIC = sdr["Pic"].ToString();
        objKasbon.DanaCadangan = Convert.ToDecimal(sdr["DanaCadangan"]);
        objKasbon.DeptID = Convert.ToInt32(sdr["DeptID"]);
        objKasbon.Apv = Convert.ToInt32(sdr["Approval"]);
        return objKasbon;
    }

}