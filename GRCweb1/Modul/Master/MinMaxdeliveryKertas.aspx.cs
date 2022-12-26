using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Domain;
using BusinessFacade;
using DataAccessLayer;


namespace GRCweb1.Modul.Master
{
    public partial class MinMaxdeliveryKertas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                clearForm();
                LoadDataGridHargaKertas(LoadGridMinMax());
            }
        }

        private ArrayList LoadGridMinMax()
        {
            ArrayList arrMinMaxDlvKertas = new ArrayList();
            MasterMinMaxDlvKertas MinmaxF = new MasterMinMaxDlvKertas();
            Users users = (Users)Session["Users"];
            arrMinMaxDlvKertas = MinmaxF.Retrieve(Int32.Parse(ddlPlant.SelectedValue));
            if (arrMinMaxDlvKertas.Count > 0)
            {
                return arrMinMaxDlvKertas;
            }
            arrMinMaxDlvKertas.Add(new MinMaxDlvKertas());
            return arrMinMaxDlvKertas;
        }
        private ArrayList LoadGridHargaKertasBySuplier()
        {
            ArrayList arrHargaKertas = new ArrayList();
            HargaKertasFacade hargaKertasFacade = new HargaKertasFacade();
            arrHargaKertas = hargaKertasFacade.RetrieveByCriteria("SupplierName", txtCari.Text);
            if (arrHargaKertas.Count > 0)
            {
                return arrHargaKertas;
            }
            arrHargaKertas.Add(new HargaKertas());
            return arrHargaKertas;
        }
        private ArrayList LoadGridHargaKertasBySuplierID()
        {
            ArrayList arrHargaKertas = new ArrayList();
            HargaKertasFacade hargaKertasFacade = new HargaKertasFacade();
            //arrHargaKertas = hargaKertasFacade.RetrieveBySuppID1(Convert.ToInt32(ddlSupplier.SelectedValue), int.Parse(ddlSubCompany.SelectedValue.ToString()));
            if (arrHargaKertas.Count > 0)
            {
                return arrHargaKertas;
            }
            arrHargaKertas.Add(new HargaKertas());
            return arrHargaKertas;
        }
        private void LoadDataGridHargaKertas(ArrayList arrHargaKertas)
        {
            this.GridView1.DataSource = arrHargaKertas;
            this.GridView1.DataBind();
        }
        private void LoadSupplier(string strNaSupp)
        {
            //ddlSupplier.Items.Clear();
            //ArrayList arrItems = new ArrayList();

            //SuppPurchFacade suppPurchFacade = new SuppPurchFacade();
            //arrItems = suppPurchFacade.RetrieveByCriteria("SupplierName", strNaSupp);

            //if (suppPurchFacade.Error == string.Empty && arrItems.Count > 0)
            //{
            //    ddlSupplier.Items.Add(new ListItem("-- Pilih Items --", "0"));

            //    foreach (SuppPurch suppPurch in arrItems)
            //    {
            //        ddlSupplier.Items.Add(new ListItem(suppPurch.SupplierName + " (" + suppPurch.SupplierCode + ")", suppPurch.ID.ToString()));
            //    }
            //}
            ddlSupplier.Items.Clear();
            Users user = (Users)HttpContext.Current.Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            if (ddlPlant.SelectedValue.Trim() == "7")
                zl.CustomQuery = "Select * from SuppPurch where RowStatus>-1 and suppliername like '%" + strNaSupp + "%'";
            if (ddlPlant.SelectedValue.Trim() == "1")
                zl.CustomQuery = "Select * from [sqlctrp.grcboard.com].bpasctrp.dbo.SuppPurch where RowStatus>-1 and suppliername like '%" + strNaSupp + "%'";
            if (ddlPlant.SelectedValue.Trim() == "13")
                zl.CustomQuery = "Select * from [sqlJombang.grcboard.com].bpasJombang.dbo.SuppPurch where RowStatus>-1 and suppliername like '%" + strNaSupp + "%'";
            SqlDataReader sdr = zl.Retrieve();

            ddlSupplier.Items.Add(new ListItem("-- Pilih Items --", "0"));
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlSupplier.Items.Add(new ListItem(sdr["suppliername"].ToString() + " (" + sdr["SupplierCode"].ToString() + ")", sdr["ID"].ToString()));
                }
            }
            txtCariSupplier.Text = string.Empty;
        }
        private void LoadSupplierCode(string strNaSupp, int plantID, int userplantID)
        {
            ddlSupplier.Items.Clear();
            ArrayList arrItems = new ArrayList();

            MasterMinMaxDlvKertas suppPurchFacade = new MasterMinMaxDlvKertas();
            arrItems = suppPurchFacade.RetrieveSupplier("Suppliercode", strNaSupp, plantID, userplantID);

            if (arrItems.Count > 0)
            {
                foreach (SupplierKertas suppPurch in arrItems)
                {
                    ddlSupplier.Items.Add(new ListItem(suppPurch.SupplierName + " (" + suppPurch.SupplierCode + ")", suppPurch.ID.ToString()));
                }
            }
        }

        protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadDataGridHargaKertas(LoadGridHargaKertasBySuplierID());
        }
        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearForm();
        }
        private void clearForm()
        {
            Session["id"] = null;
            txtCariSupplier.Text = string.Empty;
            txtMin.Text = "0";
            txtMax.Text = "0";
            txtGroup.ReadOnly = false;
            txtGroup.Text = string.Empty;
            ddlSupplier.Items.Clear();
            txtCariSupplier.Focus();
        }
        private void clearItem()
        {
            Session["id"] = null;
            txtMin.Text = "0";
            txtMax.Text = "0";
            txtGroup.ReadOnly = false;
            txtGroup.Text = string.Empty;
            ddlSupplier.Items.Clear();
            //ddlSupplier.SelectedItem.Text = string.Empty;
        }
        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            string strValidate = ValidateText();
            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }
            Users users = (Users)Session["Users"];
            MasterMinMaxDlvKertas masterKertasFacade = new MasterMinMaxDlvKertas();
            if (RbEdit.Checked == true)
                masterKertasFacade.edit(users.UnitKerjaID, Int32.Parse(txtMin.Text), Int32.Parse(txtMax.Text), txtGroup.Text);
            else
                masterKertasFacade.tambah(Int32.Parse(ddlSupplier.SelectedValue), txtGroup.Text, Int32.Parse(txtMin.Text),
                    Int32.Parse(txtMax.Text), Int32.Parse(ddlPlant.SelectedValue), users.UnitKerjaID);
            //strError = hargaKertasFacade.Insert(hrgKertas);
            LoadDataGridHargaKertas(LoadGridMinMax());
            clearItem();
        }
        protected void btnHapus_ServerClick(object sender, EventArgs e)
        {
            if (ddlSupplier.Items.Count == 0)
                return;
            Users users = (Users)Session["Users"];
            MasterMinMaxDlvKertas masterKertasFacade = new MasterMinMaxDlvKertas();
            masterKertasFacade.Hapus(Int32.Parse(ddlPlant.SelectedValue), Int32.Parse(ddlSupplier.SelectedValue), users.UnitKerjaID);
            LoadDataGridHargaKertas(LoadGridMinMax());
            clearItem();
        }
        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            LoadDataGridHargaKertas(LoadGridHargaKertasBySuplier());
        }
        protected void txtCariSupplier_TextChanged(object sender, EventArgs e)
        {
            LoadSupplier(txtCariSupplier.Text);
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Users users = (Users)Session["Users"];
            int index = Convert.ToInt32(e.CommandArgument);

            //try
            //{
            GridViewRow row = GridView1.Rows[index];
            ddlPlant.SelectedValue = row.Cells[1].Text;
            txtGroup.Text = row.Cells[4].Text;
            txtGroup.ReadOnly = true;
            txtMin.Text = row.Cells[7].Text;
            txtMax.Text = row.Cells[8].Text;
            LoadSupplierCode(row.Cells[5].Text, Int32.Parse(row.Cells[1].Text), users.UnitKerjaID);
            //}
            //catch
            //{
            //    return;
            //}
        }
        private string ValidateText()
        {

            try
            {
                decimal dec = decimal.Parse(txtMin.Text);
            }
            catch
            {
                return "Harga harus numeric";
            }
            try
            {
                decimal dec = decimal.Parse(txtMax.Text);
            }
            catch
            {
                return "Kadar Air harus numeric";
            }

            return string.Empty;
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlPlant_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataGridHargaKertas(LoadGridMinMax());
        }
    }

    public class MasterMinMaxDlvKertas
    {
        private ArrayList arrData = new ArrayList();
        private MinMaxDlvKertas pm = new MinMaxDlvKertas();
        private SupplierKertas supp = new SupplierKertas();
        public string Criteria { get; set; }
        public string Field { get; set; }
        public string Where { get; set; }
        private string Query()
        {
            string query = string.Empty;
            return query;
        }
        private MinMaxDlvKertas GenerateObject(SqlDataReader sdr)
        {
            pm = new MinMaxDlvKertas();
            pm.ID = Convert.ToInt32(sdr["ID"].ToString());
            pm.PlantID = Convert.ToInt32(sdr["PlantID"].ToString());
            pm.SupplierID = Convert.ToInt32(sdr["SupplierID"].ToString());
            pm.Plant = sdr["Plant"].ToString();
            pm.GroupName = sdr["GroupName"].ToString();
            pm.SupplierCode = sdr["SupplierCode"].ToString();
            pm.SupplierName = sdr["SupplierName"].ToString();
            pm.Min30 = Convert.ToInt32(sdr["Min30"].ToString());
            pm.Max30 = Convert.ToInt32(sdr["Max30"].ToString());
            return pm;
        }
        private SupplierKertas GenerateSupp(SqlDataReader sdr)
        {
            supp = new SupplierKertas();

            supp.ID = Convert.ToInt32(sdr["ID"]);
            supp.SupplierCode = sdr["SupplierCode"].ToString();
            supp.SupplierName = sdr["SupplierName"].ToString();
            supp.Alamat = sdr["Alamat"].ToString();
            supp.UP = sdr["UP"].ToString();
            supp.Telepon = sdr["Telepon"].ToString();
            supp.Fax = sdr["Fax"].ToString();
            supp.Handphone = sdr["Handphone"].ToString();
            supp.NPWP = sdr["NPWP"].ToString();
            supp.JoinDate = (sdr["JoinDate"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(sdr["JoinDate"]);
            supp.RowStatus = Convert.ToInt32(sdr["RowStatus"]);
            supp.CreatedBy = sdr["CreatedBy"].ToString();
            supp.CreatedTime = (sdr["CreatedTime"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(sdr["CreatedTime"]);
            supp.LastModifiedBy = sdr["LastModifiedBy"].ToString();
            supp.LastModifiedTime = (sdr["LastModifiedTime"] == DBNull.Value) ? DateTime.MinValue :
                Convert.ToDateTime(sdr["LastModifiedTime"]);
            supp.EMail = sdr["Email"].ToString();
            supp.PKP = (sdr["PKP"] == DBNull.Value) ? "" : sdr["PKP"].ToString();
            return supp;
        }
        public ArrayList Retrieve(Int32 plantID)
        {
            arrData = new ArrayList();
            string strSQL = string.Empty;
            string unitkerja = string.Empty;
            if (plantID > 0)
                unitkerja = " where plantid=" + plantID + " ";
            // if (plantID==7)
            strSQL = "select * from (select ID, PlantID, SupplierID, " +
               "case A.PlantID when 1 then ('Citeureup') when  7 then ('Karawang') when 13 then ('Jombang') end Plant,GroupName,   " +
               "case A.PlantID when 1 then (select suppliercode from [sqlctrp.grcboard.com].bpasctrp.dbo.suppPurch where ID=A.SupplierID )  " +
               "when 7 then (select suppliercode from SuppPurch where ID=A.supplierID)  " +
               "when 13 then (select suppliercode from [sqlJombang.grcboard.com].bpasJombang.dbo.SuppPurch where ID=A.supplierID) end SupplierCode,  " +
               "case A.PlantID when 1 then (select SupplierName from [sqlctrp.grcboard.com].bpasctrp.dbo.suppPurch where ID=A.SupplierID )  " +
               "when 7 then (select SupplierName from SuppPurch where ID=A.supplierID)  " +
               "when 13 then (select SupplierName from [sqlJombang.grcboard.com].bpasJombang.dbo.SuppPurch where ID=A.supplierID) end SupplierName, " +
               "Min30, Max30 from SuppPurchGroup A where A.rowstatus>-1 )S " + unitkerja + "order by groupname,plant,suppliername";
            //if
            //    (plantID == 1)
            //    strSQL = "select * from (select ID, PlantID, SupplierID, " +
            //    "case when A.PlantID =1 then ('Citeureup')else ('Karawang') end Plant,GroupName,  " +
            //    "case when A.PlantID =1 then (select suppliercode from [sqlkrwg.grcboard.com].bpasctrp.dbo.suppPurch where ID=A.SupplierID ) " +
            //    "else (select suppliercode from SuppPurch where ID=A.supplierID) end SupplierCode, " +
            //    "case when A.PlantID =1 then (select SupplierName from [sqlkrwg.grcboard.com].bpasctrp.dbo.suppPurch where ID=A.SupplierID ) " +
            //    "else (select SupplierName from SuppPurch where ID=A.supplierID) end SupplierName,Min30, Max30 " +
            //    "from SuppPurchGroup A where A.rowstatus>-1 )S order by groupname,plant,suppliername";

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
        public string tambah(Int32 suppID, string groupname, Int32 min30, Int32 max30, Int32 plantID, Int32 unitkerjaID)
        {
            string strerror = string.Empty;
            string plant = string.Empty;
            string strSQL = string.Empty;
            if (unitkerjaID == 7)
                plant = " [sqlctrp.grcboard.com].bpasctrp.dbo.";
            else
                plant = " [sqlkrwg.grcboard.com].bpaskrwg.dbo.";
            strSQL = "insert suppPurchGroup(supplierID,groupname,min30,max30,plantid,rowstatus)values(" + suppID + ",'" + groupname + "'," + min30 + "," + max30 + "," + plantID + ",0)";
            strSQL += " insert " + plant + "suppPurchGroup(supplierID,groupname,min30,max30,plantid,rowstatus)values(" + suppID + ",'" + groupname + "'," + min30 + "," + max30 + "," + plantID + ",0)";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty)
            {
                strerror = da.Error;
            }
            return strerror;
        }
        public string Hapus(Int32 plantID, Int32 suppID, Int32 unitk)
        {
            string strerror = string.Empty;
            string plant = string.Empty;
            string strSQL = string.Empty;
            if (unitk == 7)
                plant = " [sqlctrp.grcboard.com].bpasctrp.dbo.";
            else
                plant = " [sqlkrwg.grcboard.com].bpaskrwg.dbo.";

            strSQL = "update suppPurchGroup set rowstatus=-1 where plantid=" + plantID + " and supplierID=" + suppID;
            strSQL += " update " + plant + "suppPurchGroup set rowstatus=-1 where plantid=" + plantID + " and supplierID=" + suppID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty)
            {
                strerror = da.Error;
            }
            return strerror;
        }
        public string edit(Int32 plantID, Int32 min30, Int32 max30, string groupname)
        {
            string strerror = string.Empty;
            string plant = string.Empty;
            string strSQL = string.Empty;
            if (plantID == 7)
                plant = " [sqlctrp.grcboard.com].bpasctrp.dbo.";
            if (plantID == 13)
                plant = " [sqlkrwg.grcboard.com].bpaskrwg.dbo.";

            strSQL = "update suppPurchGroup set min30=" + min30 + ", max30=" + max30 +
                " where groupname='" + groupname + "'";
            strSQL += " update " + plant + "suppPurchGroup set min30=" + min30 + ", max30=" + max30 +
                " where groupname='" + groupname + "'";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty)
            {
                strerror = da.Error;
            }
            return strerror;
        }

        public ArrayList RetrieveSupplier(string strField, string strValue, Int32 plantID, Int32 userplantID)
        {
            arrData = new ArrayList();
            string strSQL = string.Empty;
            if (plantID == userplantID)
                strSQL = "select A.ID,A.SupplierCode,A.SupplierName,A.Alamat,A.UP,A.Telepon,A.Fax,A.Handphone,A.NPWP,A.JoinDate," +
                             "A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.Email,flag,PKP,ISNULL(Aktif,0)Aktif, " +
                             "ISNULL(A.SubCompanyID,0)SubCompanyID " +
                             "from SuppPurch as A where RowStatus=0 and " + strField + " like '%" + strValue + "%'";
            if (plantID == 1 && userplantID == 7)
                strSQL = "select A.ID,A.SupplierCode,A.SupplierName,A.Alamat,A.UP,A.Telepon,A.Fax,A.Handphone,A.NPWP,A.JoinDate," +
                             "A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.Email,flag,PKP,ISNULL(Aktif,0)Aktif, " +
                             "ISNULL(A.SubCompanyID,0)SubCompanyID " +
                             "from [sqlctrp.grcboard.com].bpasctrp.dbo.SuppPurch as A where RowStatus=0 and " + strField + " like '%" + strValue + "%'";
            if (plantID == 13 && userplantID == 7)
                strSQL = "select A.ID,A.SupplierCode,A.SupplierName,A.Alamat,A.UP,A.Telepon,A.Fax,A.Handphone,A.NPWP,A.JoinDate," +
                             "A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.Email,flag,PKP,ISNULL(Aktif,0)Aktif, " +
                             "ISNULL(A.SubCompanyID,0)SubCompanyID " +
                             "from [sqlJombang.grcboard.com].bpasJombang.dbo.SuppPurch as A where RowStatus=0 and " + strField + " like '%" + strValue + "%'";
            if (plantID == 7 && userplantID == 1)
                strSQL = "select A.ID,A.SupplierCode,A.SupplierName,A.Alamat,A.UP,A.Telepon,A.Fax,A.Handphone,A.NPWP,A.JoinDate," +
                             "A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.Email,flag,PKP,ISNULL(Aktif,0)Aktif, " +
                             "ISNULL(A.SubCompanyID,0)SubCompanyID " +
                             "from [sqlkrwg.grcboard.com].bpaskrwg.dbo.SuppPurch as A where RowStatus=0 and " + strField + " like '%" + strValue + "%'";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateSupp(sdr));
                }
            }
            return arrData;
        }

    }
    public class MinMaxDlvKertas
    {
        public int ID { get; set; }
        public int PlantID { get; set; }
        public int SupplierID { get; set; }
        public string Plant { get; set; }
        public string GroupName { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public int Min30 { get; set; }
        public int Max30 { get; set; }
    }
    public class SupplierKertas
    {
        public int ID { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string Alamat { get; set; }
        public string UP { get; set; }
        public string Telepon { get; set; }
        public string Fax { get; set; }
        public string Handphone { get; set; }
        public DateTime JoinDate { get; set; }
        public int RowStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedTime { get; set; }
        public string NPWP { get; set; }
        public string EMail { get; set; }
        public string PKP { get; set; }
    }
}