using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/* komponen tambahan */
using DataAccessLayer;
using BusinessFacade;
using Domain;
using System.Data.SqlClient;

/* untuk downlod gridview ke excel */
using System.IO;
using System.Drawing;

namespace GRCweb1.Modul.Purchasing
{
    public partial class PantauBeliMaterialTidakSesuai : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["pbmApproval"] == null)
            {
                LoadUsrApvSession();
            }
        //Response.Write(HttpContext.Current.Request.Url.ToString());
        //Response.Write("<hr>"+Request.RawUrl.ToString());

        /* agar tidak error saat klik download ke excel */
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExportExcel);
            /* agar tidak error saat klik download ke excel */


            Users users = (Users)Session["Users"];

            /*Response.Write(((Users)Session["Users"]).Apv.ToString());
            Response.Write("<hr>");
            if (Session["pbmApproval"] != null)
            {
                Response.Write(Session["pbmApproval"].ToString());
            }*/

            //string xs = string.Empty;

            //foreach (object obj in Session)
            //{
            //Response.Write(obj.ToString() + "  ");
            //    xs += obj.ToString() + "  ";
            //}
            //txtAlasan.Text = xs;

            //foreach (string i in Session.Contents)
            // {
            //   if (Session[i] != null)
            // {
            //txtAlasan.Text += "[" + i + "] = " + Session[i].ToString();
            //}
            //}


            //string SessionVariableName;
            //string SessionVariableValue;

            //foreach (string SessionVariable in Session.Keys)
            //{
            // Find Session variable name
            //SessionVariableName = SessionVariable;
            // Find Session variable value
            //SessionVariableValue = Session[SessionVariableName].ToString();

            // Do something with this data
            //txtAlasan.Text += "[" + SessionVariableName + "] = " + SessionVariableValue;

            //}

            if (!IsPostBack) // It is not a postback aka belum submit
            {



                //unused keep it hide
                ddlSearch.Visible = false; txtSearch.Visible = false; btnSearch.Visible = false;
                btnReset.Visible = false;
                btnDelete.Visible = false;

                //inisialisasi nilai object HTML saat halaman pertama kali di panggil
                if (txtTgl.Text.Length <= 0) { txtTgl.Text = DateTime.Now.ToString("dd-MMM-yyyy"); }
                btnSimpan.Enabled = false;
                TampilDataDropDownBahanBaku();
                TampilSupplier();
                TampilDataPemantauan();
                ddlStatusMaterial();
                TampilBln();
                TampilThn();
                TampilddlNoPo();
                ddlSupplier.Enabled = false;
                TampilAntrianApproval();
                txtKunci.Visible = false;
                txtIdApprove.Visible = false;
                txtTgl.Enabled = false;
                //btnApprove1.Enabled = false;


                TampilGridViewExcel();

                //jika yg login users sbg approver
                //if (((Users)Session["Users"]).Apv > 0) {
                if (Convert.ToInt16(Session["pbmApproval"]) > 0)
                {
                    HideCrudBtn();
                    if (GridView2.Rows.Count == 0) { div_approval.Visible = false; }
                }
                else
                {
                    div_approval.Visible = false;
                }

                if (GridView3.Rows.Count == 0) { btnExportExcel.Enabled = false; }

                /*
                txtAlasan.Text = txtAlasan.Text + ((Users)Session["Users"]).UserName.ToString() + " UserID=";
                txtAlasan.Text = txtAlasan.Text + ((Users)Session["Users"]).UserID.ToString() + " DeptID=";
                txtAlasan.Text = txtAlasan.Text + ((Users)Session["Users"]).DeptID.ToString() + " Apv=";
                txtAlasan.Text = txtAlasan.Text + ((Users)Session["Users"]).Apv.ToString() + " ";
                */

                if (Convert.ToInt16(Session["pbmApproval"]) > 2)
                {
                    btnExportExcel.Visible = true;
                }
                else
                {
                    btnExportExcel.Visible = false;
                }
            }
            else //jika submit/ postback
            {

            }
        }

        protected void HideCrudBtn()
        {
            btnDelete.Visible = false;
            btnNew.Visible = false;
            btnSimpan.Visible = false;
            btnUpdate1.Visible = false;

            TabelInputForm.Visible = false;
        }

        protected void ddlStatusMaterial()
        {
            /*
            ddlBarang.Items.Insert(0, new ListItem("-", "-"));
            ddlBarang.Items.Insert(1, new ListItem("Ditolak", "0"));
            ddlBarang.Items.Insert(2, new ListItem("Diterima", "1"));
            */

            ddlBarang.Items.Insert(0, new ListItem("-", "-"));
            ddlBarang.Items.Insert(1, new ListItem("Ditolak", "Ditolak"));
            ddlBarang.Items.Insert(2, new ListItem("Diterima", "Diterima"));
        }

        //Tombol View
        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(Global.ConnectionString());
            SqlCommand cmd = new SqlCommand();
            Session["ditolak"] = null;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spPantauBeliMaterial";
            cmd.Parameters.Add("@bulan", SqlDbType.VarChar, 5).Value = ddlBulanKejadian.SelectedValue; //txtID.Text.Trim();
            cmd.Parameters.Add("@bulan2", SqlDbType.VarChar, 5).Value = ddlBulanKejadian2.SelectedValue;
            cmd.Parameters.Add("@tahun", SqlDbType.VarChar, 4).Value = ddlTahunKejadian.SelectedValue;
            cmd.Parameters.AddWithValue("@NextApprover", Session["pbmApproval"]);
            cmd.Parameters.AddWithValue("@tampil", "FilterPantauBeliMaterial");
            cmd.Connection = con;
            string nxt = Session["pbmApproval"].ToString();
            //try
            //{
            con.Open();
            //GridView1.EmptyDataText = "<h2>No Records Found (Tidak ada data)</h2>";       

            //biar gridview support paging
            SqlDataAdapter sda = new SqlDataAdapter();
            DataTable dt = new DataTable();

            sda.SelectCommand = cmd;
            sda.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();

            if (GridView1.Rows.Count == 0)
            {
                btnExportExcel.Enabled = false;
            }
            TampilGridViewExcel();
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    con.Close();
            //    con.Dispose();
            //}
            if (GridView1.Rows.Count == 0)
            {
                con = new SqlConnection(Global.ConnectionString());
                cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spPantauBeliMaterial";
                cmd.Parameters.Add("@bulan", SqlDbType.VarChar, 5).Value = ddlBulanKejadian.SelectedValue; //txtID.Text.Trim();
                cmd.Parameters.Add("@bulan2", SqlDbType.VarChar, 5).Value = ddlBulanKejadian2.SelectedValue;
                cmd.Parameters.Add("@tahun", SqlDbType.VarChar, 4).Value = ddlTahunKejadian.SelectedValue;
                cmd.Parameters.AddWithValue("@NextApprover", Session["pbmApproval"]);
                cmd.Parameters.AddWithValue("@tampil", "FilterPantauBeliMaterial0");
                cmd.Connection = con;

                //try
                //{
                con.Open();
                //GridView1.EmptyDataText = "<h2>No Records Found (Tidak ada data)</h2>";       

                //biar gridview support paging
                SqlDataAdapter sda1 = new SqlDataAdapter();
                DataTable dt1 = new DataTable();

                sda1.SelectCommand = cmd;
                sda1.Fill(dt1);
                GridView1.DataSource = dt1;
                GridView1.DataBind();

                //}
                //catch (Exception ex)
                //{
                //    throw ex;
                //}
                //finally
                //{
                //    con.Close();
                //    con.Dispose();
                //}
            }
            int blnSarmut = 0;
            string strSQL = string.Empty;
            if (Convert.ToInt32(ddlBulanKejadian.SelectedValue) >= 1 && Convert.ToInt32(ddlBulanKejadian.SelectedValue) <= 6)
            {
                blnSarmut = 6;
                strSQL = "select count(ID) aktual from(SELECT a.NextApprover, a.ID AS ID,replace(convert(NVARCHAR, TglKejadian, 106), ' ', '-') as TglKejadian,IdSupplier,SupplierName,IdMaterial, " +
                    "ItemName,NoPo,NoSuratJalan,KetTdkSesuaiBrg,Alasan,a.CreatedBy,a.CreatedTime,a.LastModifiedBy,a.LastModifiedTime, a.ApprovalStatus,  " +
                    "CASE a.NextApprover WHEN 1 THEN 'QA Kasie' WHEN 2 THEN 'QA Manager' WHEN 3 THEN 'Purchasing Kasie' WHEN 4 THEN 'Purchasing Manager' WHEN 5 THEN 'Done'  " +
                    "ELSE 'Unknown' END AS NextApprover1,CASE a.KetTdkSesuaiBrg WHEN 'Ditolak' THEN '' ELSE 'x' END AS Diterima,CASE a.KetTdkSesuaiBrg " +
                    "WHEN 'Diterima' THEN '' ELSE 'x' END AS Ditolak, a.TindakanPurchasing, a.KlarifikasiSupplier FROM SuppPurch b, Inventory c, PantauBeliMaterial a  " +
                    "WHERE b.ID=a.IdSupplier AND a.IdMaterial=c.ID AND a.RowStatus>-1 AND month(TglKejadian)>=1 AND month(TglKejadian)<=6 AND year(TglKejadian)=" +
                    ddlTahunKejadian.SelectedValue + " AND a.NextApprover>3)A where KetTdkSesuaiBrg='ditolak'";
            }
            if (Convert.ToInt32(ddlBulanKejadian.SelectedValue) >= 7 && Convert.ToInt32(ddlBulanKejadian.SelectedValue) <= 12)
            {
                blnSarmut = 12;
                strSQL = "select count(ID) aktual from(SELECT a.NextApprover, a.ID AS ID,replace(convert(NVARCHAR, TglKejadian, 106), ' ', '-') as TglKejadian,IdSupplier,SupplierName,IdMaterial, " +
                    "ItemName,NoPo,NoSuratJalan,KetTdkSesuaiBrg,Alasan,a.CreatedBy,a.CreatedTime,a.LastModifiedBy,a.LastModifiedTime, a.ApprovalStatus,  " +
                    "CASE a.NextApprover WHEN 1 THEN 'QA Kasie' WHEN 2 THEN 'QA Manager' WHEN 3 THEN 'Purchasing Kasie' WHEN 4 THEN 'Purchasing Manager' WHEN 5 THEN 'Done'  " +
                    "ELSE 'Unknown' END AS NextApprover1,CASE a.KetTdkSesuaiBrg WHEN 'Ditolak' THEN '' ELSE 'x' END AS Diterima,CASE a.KetTdkSesuaiBrg " +
                    "WHEN 'Diterima' THEN '' ELSE 'x' END AS Ditolak, a.TindakanPurchasing, a.KlarifikasiSupplier FROM SuppPurch b, Inventory c, PantauBeliMaterial a  " +
                    "WHERE b.ID=a.IdSupplier AND a.IdMaterial=c.ID AND a.RowStatus>-1 AND month(TglKejadian)>=7 AND month(TglKejadian)<=12 AND year(TglKejadian)=" +
                    ddlTahunKejadian.SelectedValue + " AND a.NextApprover>3)A where KetTdkSesuaiBrg='ditolak'";
            }
            decimal aktual = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = strSQL;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    aktual = Convert.ToDecimal(sdr["aktual"].ToString());
                }
            }
            string sarmutPrs = "Pembelian Material Ketidaksesuaian Persyaratan Perusahaan (Semesteran)";
            int deptid = getDeptID("purchasing");
            ZetroView zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery =
                "update SPD_TransPrs set actual= " + aktual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddlTahunKejadian.SelectedValue +
                " and Bulan=" + blnSarmut +
                " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "' and deptid=" + deptid + ") ";
            SqlDataReader sdr1 = zl1.Retrieve();
        }
        protected int getDeptID(string deptName)
        {
            int result = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select id from spd_dept where dept like '%" + deptName + "%'";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Int32.Parse(sdr["ID"].ToString());
                }
            }
            return result;
        }
        //contoh cara lama contoh aja ga di pakai
        protected void TampilBlnNonStoredProcedure()
        {
            string strSQL = "SELECT distinct SUBSTRING(CONVERT(nvarchar(6),TglKejadian, 112),5,2) AS idBln, " +
            "DATENAME(MONTH,TglKejadian) nmBln, LEFT(DATENAME(MONTH,TglKejadian),3) nmBln1  FROM popurchn  ORDER BY idBln";
            DataAccess da = new DataAccess(Global.ConnectionString());//selalu pakai ini biar ga lock db nya
            SqlDataReader dr = da.RetrieveDataByString(strSQL);
            string strSQLerror = da.Error;
            int i = 0;
            if (dr.HasRows)
            {
                ddlBulanKejadian.Items.Insert(i, new ListItem("Semua", "Semua"));
                while (dr.Read())
                {
                    //ddlBahanBaku.Items.Insert(nomor_index_nya, new ListItem("text yg tampil", "nilai value combo box"));
                    ddlBulanKejadian.Items.Insert(i, new ListItem(dr["nmBln"].ToString(), dr["idBln"].ToString()));
                    i = i + 1;
                }
            }
        }


        protected void TampilBln()
        {
            int i = 0;

            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Global.ConnectionString());
            SqlCommand SqlCmd = new SqlCommand("spPantauBeliMaterial", con);

            // Configure command and add parameters.
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@tampil", SqlDbType.VarChar, 25).Value = "ddlBulan";
            SqlCmd.Connection = con;

            con.Open();
            SqlDataReader dr = SqlCmd.ExecuteReader();
            if (dr.HasRows)
            {
                ddlBulanKejadian.Items.Insert(i, new ListItem("Semua", "Semua"));
                while (dr.Read())
                {
                    ddlBulanKejadian.Items.Insert(i, new ListItem(dr["nmBln"].ToString(), dr["idBln"].ToString()));
                    ddlBulanKejadian2.Items.Insert(i, new ListItem(dr["nmBln"].ToString(), dr["idBln"].ToString()));
                    i = i + 1;
                }
                con.Close();
            }
        }


        protected void TampilThn()
        {
            /*
            string strSQL = "SELECT DISTINCT year(TglKejadian) AS thn FROM PantauBeliMaterial ORDER BY thn desc";
            DataAccess da = new DataAccess(Global.ConnectionString());//selalu pakai ini biar ga lock db nya

            SqlDataReader dr = da.RetrieveDataByString(strSQL);
            string strSQLerror = da.Error;
            */

            int i = 0;

            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Global.ConnectionString());
            SqlCommand SqlCmd = new SqlCommand("spPantauBeliMaterial", con);

            // Configure command and add parameters.
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@tampil", SqlDbType.VarChar, 25).Value = "ddlTahun";
            SqlCmd.Connection = con;

            con.Open();
            SqlDataReader dr = SqlCmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ddlTahunKejadian.Items.Insert(i, new ListItem(dr["thn"].ToString(), dr["thn"].ToString()));
                    i = i + 1;
                }
                con.Close();
            }
        }


        protected void TampilDataDropDownBahanBaku()
        {
            string NoPo = ddlNoPO.SelectedValue;

            string strSQL = "SELECT ID, ItemCode, ItemName/*+' - '+ItemCode*/ as ItemName FROM Inventory WHERE ID in (SELECT ItemID FROM POPurchnDetail AS pod LEFT JOIN POPurchn AS po ON po.ID=pod.POID WHERE po.NoPO='" + NoPo + "' and pod.Status>-1 AND po.Status>-1) AND RowStatus>-1 ORDER BY ItemName";
            DataAccess da = new DataAccess(Global.ConnectionString());//selalu pakai ini biar ga lock db nya
            SqlDataReader dr = da.RetrieveDataByString(strSQL);

            ddlBahanBaku.Items.Clear();

            string strSQLerror = da.Error;
            int i = 0;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    //result = int.Parse(sqlDataReader["NoUrut"].ToString());

                    ddlBahanBaku.Items.Insert(i, new ListItem(dr["ItemName"].ToString(), dr["ID"].ToString()));
                    i += 1; // i = i + 1; // i++
                }
            }
            else
            {
                ddlBahanBaku.Items.Clear();
                ddlBahanBaku.Items.Insert(0, new ListItem("No Records Found (Tidak ada data)", ""));
            }
        }


        protected void TampilSupplier()
        {
            string strSQL = "SELECT ID, SupplierCode, /*CAST(ID as varchar(10))+' - '+*/SupplierName+' - '+SupplierCode AS nmSupplier FROM SuppPurch WHERE RowStatus>-1 AND ID IN (SELECT IdSupplier1 FROM PantauBeliMaterialSupplier) ORDER BY SupplierName";
            DataAccess da = new DataAccess(Global.ConnectionString());//selalu pakai ini biar ga lock db nya
            SqlDataReader dr = da.RetrieveDataByString(strSQL);
            string strSQLerror = da.Error;
            int i = 1;
            ddlSupplier.Items.Insert(0, new ListItem("-", "-"));
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    //result = int.Parse(sqlDataReader["NoUrut"].ToString());
                    //ddlBahanBaku.Items.Add(dr["ItemName"].ToString());
                    ddlSupplier.Items.Insert(i, new ListItem(dr["nmSupplier"].ToString(), dr["ID"].ToString()));
                    i = i + 1;
                }
            }
        }


        protected void TampilDataPemantauan()
        {
            //old school way :)
            //string strSQL = "SELECT a.ID AS ID,replace(convert(NVARCHAR, TglKejadian, 106), ' ', '-') as TglKejadian,IdSupplier,SupplierName,IdMaterial,ItemName,NoPo,NoSuratJalan,KetTdkSesuaiBrg,Alasan,a.CreatedBy,a.CreatedTime,a.NextApprover,a.LastModifiedBy,a.LastModifiedTime, CASE a.ApprovalStatus WHEN 1 THEN 'QA Kasie' WHEN 2 THEN 'QA Manager' WHEN 3 THEN 'Purchasing Kasie' WHEN 4 THEN 'Purchasing Manager' ELSE 'Open' END AS ApprovalStatus ";
            //strSQL += "FROM SuppPurch b, Inventory c, PantauBeliMaterial a ";
            //strSQL = strSQL + "WHERE b.ID=a.IdSupplier AND a.IdMaterial=c.ID AND c.RowStatus>-1";
            //Response.Write(strSQL);

            /*
            SELECT a.ID AS ID,replace(convert(NVARCHAR, TglKejadian, 106), ' ', '-') as TglKejadian,IdSupplier,SupplierName,IdMaterial,ItemName,NoPo,NoSuratJalan,KetTdkSesuaiBrg,Alasan,a.CreatedBy,a.CreatedTime,a.LastModifiedBy,a.LastModifiedTime, 
            CASE a.ApprovalStatus 
            WHEN 1 THEN 'QA Kasie' 
            WHEN 2 THEN 'QA Manager' 
            WHEN 3 THEN 'Purchasing Kasie' 
            WHEN 4 THEN 'Purchasing Manager' 
            ELSE 'Open' END AS ApprovalStatus
            FROM SuppPurch b, Inventory c, PantauBeliMaterial a 
            WHERE b.ID=a.IdSupplier AND a.IdMaterial=c.ID AND c.RowStatus>-1
            */

            /*
            DataAccess da = new DataAccess(Global.ConnectionString());//selalu pakai ini biar ga lock db nya
            SqlDataReader dr = da.RetrieveDataByString(strSQL);
            string strSQLerror = da.Error;
            */

            SqlConnection con = new SqlConnection(Global.ConnectionString());
            //Response.Write(Global.ConnectionString()); //Response.End();

            //cara singkat --> //SqlCommand cmd = new SqlCommand(strSQL, con);  
            //SqlCommand cmd = new SqlCommand();
            //cmd.CommandText = strSQL;
            //cmd.Connection = con;
            SqlCommand cmd = new SqlCommand("spPantauBeliMaterial", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "spPantauBeliMaterial";
            cmd.Parameters.Add("@bulan", SqlDbType.VarChar, 5).Value = DateTime.Now.ToString("MM"); //txtID.Text.Trim();
            cmd.Parameters.Add("@tahun", SqlDbType.VarChar, 4).Value = DateTime.Now.ToString("yyyy");
            cmd.Parameters.AddWithValue("@NextApprover", Session["pbmApproval"]);
            cmd.Parameters.AddWithValue("@tampil", "FilterPantauBeliMaterial");
            cmd.Connection = con;
            con.Open(); //buka koneksi        

            //SqlDataReader dr = cmd.ExecuteReader();

            //GridView1.EmptyDataText = "<h2>No Records Found (Tidak ada data)</h2>";

            //GridView1.DataSource = dr; //cmd.ExecuteReader();    
            //GridView1.DataSource = cmd.ExecuteReader();    
            //GridView1.AllowPaging = true;
            //GridView1.DataBind();

            SqlDataAdapter sda = new SqlDataAdapter();
            DataTable dt = new DataTable();

            sda.SelectCommand = cmd;
            sda.Fill(dt);
            GridView1.DataSource = dt; //cmd.ExecuteReader();    
            GridView1.AllowPaging = true;
            GridView1.DataBind();

            con.Close();//tutup koneksi
        }

        //agar gridview1 support paging
        protected void OnPaging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView3.PageIndex = e.NewPageIndex;
        }


        protected void TampilddlNoPo()
        {
            int i = 0;

            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Global.ConnectionString());
            SqlCommand SqlCmd = new SqlCommand("spPantauBeliMaterial", con);

            // Configure command and add parameters.
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@tampil", SqlDbType.VarChar, 25).Value = "ddlNoPO";
            //SqlCmd.Parameters.Add("@SupplierId", SqlDbType.Int).Value = ddlSupplier.SelectedValue;
            SqlCmd.Parameters.AddWithValue("@SupplierId", ddlSupplier.SelectedItem.Value);
            SqlCmd.Connection = con;

            con.Open();
            SqlDataReader dr = SqlCmd.ExecuteReader();
            if (dr.HasRows)
            {
                ddlNoPO.Items.Clear();

                while (dr.Read())
                {
                    //ddl.Items.Insert(nomor_index_nya, new ListItem("text yg tampil", "nilai value combo box"));
                    ddlNoPO.Items.Insert(i, new ListItem(dr["NoPo"].ToString(), dr["NoPo"].ToString()));
                    i = i + 1;
                }
                con.Close();

                TampilDataDropDownBahanBaku();
            }
            else
            {
                ddlNoPO.Items.Clear();
                ddlNoPO.Items.Insert(0, new ListItem("No Records Found (Tidak ada data)", ""));
                ddlBahanBaku.Items.Clear();
                ddlBahanBaku.Items.Insert(0, new ListItem("No Records Found (Tidak ada data)", ""));
            }
        }


        //custom function untuk GetColumnIndexByName pada gridview
        int GetColumnIndexByName(GridViewRow row, string columnName)
        {
            int columnIndex = 0;
            int foundIndex = -1;
            foreach (DataControlFieldCell cell in row.Cells)
            {
                if (cell.ContainingField is BoundField)
                {
                    if (((BoundField)cell.ContainingField).DataField.Equals(columnName))
                    {
                        foundIndex = columnIndex;
                        break;
                    }
                }
                columnIndex++; // keep adding 1 while we don't have the correct name
            }
            return foundIndex;
        }


        //hide gridview column tertentu on the fly
        int indexOfColumn = 1; //Note : Index will start with 0 so set this value accordingly
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count > indexOfColumn)
            {
                //hide column @ client side
                //data masih bisa di akses saat klik gridview row command u/ edit

                //--- cara 3 ---//
                int index = GetColumnIndexByName(e.Row, "Alasan");
                if (index > 0)
                {
                    //string columnValue = e.Row.Cells[index].Text;
                    //e.Row.Cells[index].Visible = false;
                }

                string test = "";
                //cara 2 hide column at runtime by data gridview header text
                for (int i = 0; i < GridView1.Columns.Count; i++)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        if (GridView1.Columns[i].HeaderText.Trim() == "Keterangan")
                        {
                            test = (e.Row.Cells[9].Text).ToString();
                        }
                    }
                    if (GridView1.Columns[i].HeaderText == "IdSupplier")
                    {
                        //GridView1.Columns[i].Visible = false;
                        e.Row.Cells[i].Visible = false; //IdSupplier
                    }
                    if (GridView1.Columns[i].HeaderText == "IdMaterial")
                    {
                        e.Row.Cells[i].Visible = false;
                    }
                    if (GridView1.Columns[i].HeaderText == "ApprovalStatus")
                    {
                        e.Row.Cells[i].Visible = false;
                    }
                    if (GridView1.Columns[i].HeaderText == "NextApprover")
                    {
                        e.Row.Cells[i].Visible = false;
                    }
                    if (GridView1.Columns[i].HeaderText == "Aksi")
                    {
                        //hide kolom edit jika approver yg login
                        if (Convert.ToInt16(Session["pbmApproval"]) > 0)
                        //if (Convert.ToInt32(((Users)Session["Users"]).Apv.ToString()) > 0)
                        {
                            e.Row.Cells[i].Visible = false;
                        }
                    }
                    //jika yg login bukan approver purchsing, hide column
                    if (Convert.ToInt16(Session["pbmApproval"]) < 3)
                    //if (Convert.ToInt32(((Users)Session["Users"]).Apv.ToString()) < 3) 
                    {
                        if (GridView1.Columns[i].HeaderText == "Tindakan Purchasing" || GridView1.Columns[i].HeaderText == "Klarifikasi Supplier")
                        {
                            e.Row.Cells[i].Visible = false;
                        }
                    }
                }

                /* cara 1 cara lama by column index tidak konsisten */
                //e.Row.Cells[2].Visible = false; //IdSupplier
                //e.Row.Cells[4].Visible = false; //IdMaterial

                //jika usr yg login sbg approver, hide kolom Aksi (pilih)
                //if (Convert.ToInt32(((Users)Session["Users"]).Apv.ToString()) > 0)
                //{
                //e.Row.Cells[13].Visible = false; //kolom aksi pilih
                //}

                /*if (e.Row.RowType == DataControlRowType.DataRow)
                //{
                    //string ket = string.Empty;
                    //ket = e.Row.Cells[11].Text; //NextApprover
                    //if (e.Row.Cells[10].Text, out ket )
                    //if (ket == "1")
                    //{
                        //Button btnEdit = (Button)e.Row.FindControl("btnEdit");
                        //btnEdit.Visible = false;

                        //CheckBox chkRow = (CheckBox)e.Row.FindControl("chkRow");
                        //chkRow.Visible = false;

                        //LinkButton lnkBtn = (LinkButton)e.Row.FindControl("pilihRow");
                        //lnkBtn.Visible = false;
                    //}
                //}

                //ButtonField btnEdit = (ButtonField)e.Row.FindControl("pilihRow");
                //btnEdit.Visible = false;
                */
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (Convert.ToInt32(e.Row.Cells[12].Text) > 1)
                    {
                        //e.Row.Visible = false; //hide specific row

                        //hide cell value (link edit)
                        LinkButton lnkBtn = (LinkButton)e.Row.FindControl("btnLinkEdit");
                        lnkBtn.Visible = false;
                    }



                }
            }
        }


        /* menambah class css pada gridview row at server side */
        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //on mouse over
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
                e.Row.Attributes.Add("onmouseout", "this.className='normal'");
            }

            //if (e.Row.RowType == DataControlRowType.Header) 
            //{
            //e.Row.Cells[1].CssClass = "hidecontrol"; //css client side
            //}
        }
        /* menambah class css pada gridview row at server side */
        protected void GridView2_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //on mouse over
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.className='highlight'");
                e.Row.Attributes.Add("onmouseout", "this.className='normal'");
            }
        }

        /* event saat row link/ button GridView di klik */
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "pilihRow") //lihat source code di file aspx nya di GridView1
            {
                //Determine the RowIndex of the Row whose LinkButton was clicked.
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                //Reference the GridView Row.
                GridViewRow row = GridView1.Rows[rowIndex];

                string x, y, z, w, o = string.Empty;

                TampilddlNoPo();

                txtKunci.Text = CekString(row.Cells[1].Text);
                txtTgl.Text = CekString(row.Cells[2].Text);
                txtSuratJalan.Text = CekString(row.Cells[8].Text);
                txtAlasan.Text = CekString(row.Cells[10].Text);

                x = CekString(row.Cells[3].Text); //kolom IdSupplier
                y = CekString(row.Cells[5].Text); //kolom IdMaterial
                z = CekString(row.Cells[9].Text); //kolom KetidakSesuaian Barang
                w = CekString(row.Cells[7].Text); //kolom NoPo
                o = CekString(row.Cells[6].Text); //kolom Bahan Baku

                ddlSupplier.ClearSelection();
                //DisplayAJAXMessage(this, x);
                ddlSupplier.Items.FindByValue(x).Selected = true;

                ddlBarang.ClearSelection();
                ddlBarang.Items.FindByValue(z).Selected = true;

                TampilddlNoPo();

                ddlNoPO.ClearSelection();
                ddlNoPO.Items.FindByValue(w).Selected = true;

                ddlBahanBaku.ClearSelection();
                ddlBahanBaku.Items.FindByValue(y).Selected = true;

                if (ddlSupplier.Enabled == false)
                {
                    ddlSupplier.Enabled = true;
                }
                btnUpdate1.Enabled = true;
                btnDelete.Enabled = true;
                btnSimpan.Enabled = false;
                txtTgl.Enabled = true;
            }


        }

        //saat dropdown/ combobox Supplier berubah
        protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtAlasan.Text = ddlSupplier.SelectedItem.Value;
            //txtAlasan.Text = txtAlasan.Text + ddlSupplier.SelectedItem.Text;

            TampilddlNoPo();
            TampilDataPemantauan();

            //DisplayAJAXMessage(this, "Nomor Improvement blm di isi");
        }


        //nampilin alert java script di asp.net c# mesti pakai ginian :)
        //public itu biar function DisplayAJAXMessage nya bisa di pakai di private function yg lain
        public static void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "MyScript", myScript, true);
        }


        private string CekString(string strValue)
        {
            if (strValue == "&nbsp;") { return string.Empty; }
            return strValue;
        }


        protected void btnNew_Click(object sender, EventArgs e)
        {
            ddlSupplier.SelectedIndex = 0;
            ddlSupplier.Enabled = true;

            ddlNoPO.SelectedIndex = 0;
            ddlBahanBaku.SelectedIndex = 0;
            ddlBarang.SelectedIndex = 0; //ketidak sesuaian
            txtSuratJalan.Text = "";
            txtAlasan.Text = "";

            txtTgl.Enabled = true;
            btnSimpan.Enabled = true;
            btnUpdate1.Enabled = false;
            btnDelete.Enabled = false;
        }


        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(Global.ConnectionString());
            //SqlCommand cmd = new SqlCommand();
            SqlCommand cmd = new SqlCommand("spPantauBeliMaterial", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@tampil", SqlDbType.VarChar).Value = "PrsInsert";
            cmd.Parameters.Add("@TglKejadian", SqlDbType.DateTime).Value = DateTime.Parse(txtTgl.Text);
            cmd.Parameters.Add("@IdSupplier", SqlDbType.VarChar).Value = ddlSupplier.SelectedValue.ToString();
            cmd.Parameters.Add("@IdMaterial", SqlDbType.VarChar).Value = ddlBahanBaku.SelectedValue.ToString();
            cmd.Parameters.Add("@NoSuratJalan", SqlDbType.VarChar).Value = txtSuratJalan.Text;
            cmd.Parameters.Add("@KetTdkSesuaiBrg", SqlDbType.VarChar).Value = ddlBarang.SelectedValue.ToString();
            cmd.Parameters.AddWithValue("@NoPo", ddlNoPO.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Alasan", txtAlasan.Text);
            cmd.Parameters.AddWithValue("@CreatedBy", ((Users)Session["Users"]).UserName);

            //cmd.CommandText = "spPantauBeliMaterial";
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Connection = con;

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            //refresh current page
            Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
        }


        protected void TampilDataPemantauanNoSProcedure()
        {
            /*string query = string.Empty;
            SqlConnection conn = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            DataSet ds = null;


            query = "SELECT a.ID AS ID,replace(convert(NVARCHAR, TglKejadian, 106), ' ', '-') as TglKejadian,IdSupplier,SupplierName,IdMaterial,ItemName,NoPo,NoSuratJalan,KetTdkSesuaiBrg,Alasan,a.CreatedBy,a.CreatedTime,a.LastModifiedBy,a.LastModifiedTime, a.ApprovalStatus FROM SuppPurch b, Inventory c, PantauBeliMaterial a WHERE b.ID=a.IdSupplier AND a.IdMaterial=c.ID AND c.RowStatus>-1";
            conn = new SqlConnection(Global.ConnectionString());

            cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("value", SqlDbType.VarChar, 50).Value = "some value";

            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            DataTable dt = new DataTable();
            dt.TableName = "Crystal report example";
            ds.Tables[0].Merge(dt);
            da.Fill(ds);

            if (ds.Tables.Count > 0) {
                GridView1.DataSource = ds.Tables(0);
                GridView1.AllowPaging = true;
                GridView1.DataBind();
            }*/
        }


        /*public DataTable GetDataTable(String connectionString, String query)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                        {
                            dataAdapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch
            {

            }

            return dataTable;
        }*/


        //saat dropdown/ combobox NoPO berubah
        protected void ddlNoPO_SelectedIndexChanged(object sender, EventArgs e)
        {
            TampilDataDropDownBahanBaku();
        }

        protected void btnApprove1_Click(object sender, EventArgs e)
        {
            int jmldicek = 0;
            string txtTindakanPurchasing = string.Empty;
            string txtKlarifikasiSupplier = string.Empty;
            /*
            string query1 = "update PantauBeliMaterial set TindakanPurchasing=@TindakanPurchasing, KlarifikasiSupplier=@KlarifikasiSupplier where ID=@IdPBM";

            SqlConnection con1 = new SqlConnection(Global.ConnectionString());
            SqlCommand cmd1 = new SqlCommand(query1, con1);
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = query1;

            cmd1.Parameters.Add("@TindakanPurchasing", SqlDbType.Text);
            cmd1.Parameters.Add("@KlarifikasiSupplier", SqlDbType.Text);
            cmd1.Parameters.Add("@IdPBM", SqlDbType.Int);
            */


            foreach (GridViewRow row in GridView2.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[11].FindControl("chkRow") as CheckBox);
                    if (chkRow.Checked) //jika checkbox di centang
                    {
                        jmldicek = jmldicek + 1;

                        //string name = row.Cells[1].Text; //ambil nilai kolom di kolom 1
                        //string country = (row.Cells[2].FindControl("lblCountry") as Label).Text;
                        //dt.Rows.Add(name, country);

                        txtIdApprove.Text = txtIdApprove.Text + row.Cells[1].Text + ",";

                        //jika user login = purchasing approver (Di matikan karna Bu ika aprroved aja, tidak lg input)
                        //if (Convert.ToInt16(Session["pbmApproval"]) == 3)
                        //{

                        //    //TextBox txtbox1 = (row.Cells[11].FindControl("txtTindakanPurchasing") as TextBox);    // finding the textbox control inside gridview
                        //    //TextBox txtbox2 = (row.Cells[12].FindControl("txtKlarifikasiSupplier") as TextBox);    // finding the textbox control inside gridview
                        //    //txtTindakanPurchasing = txtbox1.Text;
                        //    //txtKlarifikasiSupplier = txtbox2.Text;

                        //    //cmd1.Parameters.Clear();
                        //    //cmd1.Parameters["@TindakanPurchasing"].Value = txtTindakanPurchasing;
                        //    //cmd1.Parameters["@KlarifikasiSupplier"].Value = txtKlarifikasiSupplier;
                        //    //cmd1.Parameters["@IdPBM"].Value = row.Cells[1].Text;

                        //    /*
                        //    cmd1.Parameters.AddWithValue("@TindakanPurchasing", txtTindakanPurchasing.ToString());
                        //    cmd1.Parameters.AddWithValue("@KlarifikasiSupplier", txtKlarifikasiSupplier.ToString());
                        //    cmd1.Parameters.AddWithValue("@IdPBM", int.Parse(row.Cells[1].Text.ToString()));
                        //    */

                        //    //con1.Open();
                        //    //cmd1.ExecuteNonQuery();
                        //    //con1.Close();

                        //    string f_TindakanPurchasing = ((TextBox)row.FindControl("txtTindakanPurchasing")).Text;
                        //    string f_KlarifikasiSupplier = ((TextBox)row.FindControl("txtKlarifikasiSupplier")).Text;
                        //    int f_IdPBM = int.Parse(row.Cells[1].Text.ToString());
                        //    using (SqlConnection conn = new SqlConnection(Global.ConnectionString()))
                        //    {
                        //        using (SqlCommand cmd3 = new SqlCommand("update PantauBeliMaterial set TindakanPurchasing=@TindakanPurchasing, KlarifikasiSupplier=@KlarifikasiSupplier where ID=@IdPBM", conn))
                        //        {
                        //            cmd3.CommandType = CommandType.Text;
                        //            cmd3.Parameters.AddWithValue("@TindakanPurchasing", f_TindakanPurchasing);
                        //            cmd3.Parameters.AddWithValue("@KlarifikasiSupplier", f_KlarifikasiSupplier);
                        //            cmd3.Parameters.AddWithValue("@IdPBM", f_IdPBM);

                        //            conn.Open();
                        //            cmd3.ExecuteNonQuery();
                        //            conn.Close();
                        //        }
                        //    }
                        //}
                    }
                }
            }

            if (jmldicek > 0)
            {
                string txtIdApprove1 = string.Empty;
                txtIdApprove1 = txtIdApprove.Text;
                txtIdApprove1 = txtIdApprove1.Substring(0, (txtIdApprove1.Length) - 1);
                //Response.End();

                txtIdApprove.Text = "";
                //Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
                //Response.Redirect(Request.RawUrl);            

                SqlConnection con = new SqlConnection(Global.ConnectionString());
                SqlCommand cmd = new SqlCommand("spPantauBeliMaterial", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@tampil", SqlDbType.VarChar).Value = "prsApprove";
                cmd.Parameters.AddWithValue("@txtIdApprove", txtIdApprove1.ToString());
                //cmd.Parameters.AddWithValue("@txtIdApprove1", "'"+txtIdApprove1.ToString()+"'");
                cmd.Parameters.AddWithValue("@NextApprover", Session["pbmApproval"]);
                cmd.Parameters.AddWithValue("@nmApprover", ((Users)Session["Users"]).UserName);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                ////refresh current page
                ////Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
                Response.Redirect(Request.RawUrl.ToString(), true);
            }
        }

        //download gridview3 to excel
        protected void ExportToExcelOK()
        {
            string FileName = "LembarPantauBeliMaterial" + DateTime.Now.ToString() + ".xls";

            Response.Clear();
            Response.Buffer = true;
            //Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                //GridView1.AllowPaging = false;
                GridView1.AllowPaging = false;
                //Button1_Click();
                //TampilDataPemantauan();

                GridView1.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in GridView1.HeaderRow.Cells)
                {
                    cell.BackColor = GridView1.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridView1.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridView1.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                GridView1.RenderControl(hw);

                //style to format numbers to string
                //string style = @"<style> .textmode { } </style>";
                //Response.Write(style);
                Response.Write("LEMBAR PEMANTAUAN<br>");
                Response.Write("PEMBELIAN MATERIAL KETIDAKSESUAIAN PERSYARATAN PERUSAHAAN<br>");
                Response.Write("Bulan : " + ddlBulanKejadian.SelectedItem.Text.ToString() + " Sampai " + ddlBulanKejadian2.SelectedItem.Text.ToString() + " " + ddlTahunKejadian.Text.ToString() + "<br>");
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
            //required to avoid the runtime error "  
            //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
        }

        /*
        private void ExportGridToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "Vithal" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            GridView1.GridLines = GridLines.Both;
            GridView1.HeaderStyle.Font.Bold = true;
            GridView1.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();

        }
        */

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            //ExportGridToExcel();
            //jika yg login = approver purchasing boleh download
            //if (Convert.ToInt16(Session["pbmApproval"]) > 2) {
            ExportToExcelOK();
            //}
            //else
            //{
            //DisplayAJAXMessage(this, "Maaf anda tidak dapat menggunakan fitur ini");
            //}
        }


        protected void TampilAntrianApproval()
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Global.ConnectionString());
            SqlCommand cmd = new SqlCommand();

            // Configure command and add parameters.
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spPantauBeliMaterial";
            cmd.Parameters.Add("@tampil", SqlDbType.VarChar, 25).Value = "QueueApproval";
            //login session user level approval
            //cmd.Parameters.Add("@NextApprover", SqlDbType.Int).Value = ((Users)Session["Users"]).Apv;
            cmd.Parameters.AddWithValue("@NextApprover", Session["pbmApproval"]);
            cmd.Connection = con;
            string nxt = Session["pbmApproval"].ToString();
            try
            {
                con.Open();
                GridView2.EmptyDataText = "<center><span style='color:red;'>Tidak ada data approval</span></center>";

                SqlDataAdapter sda = new SqlDataAdapter();
                DataTable dt = new DataTable();

                sda.SelectCommand = cmd;
                sda.Fill(dt);
                GridView2.DataSource = dt;
                GridView2.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }





        protected void btnUpdate1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(Global.ConnectionString());
            SqlCommand cmd = new SqlCommand("spPantauBeliMaterial", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@tampil", SqlDbType.VarChar).Value = "PrsUpdate";
            cmd.Parameters.Add("@TglKejadian", SqlDbType.DateTime).Value = DateTime.Parse(txtTgl.Text);
            cmd.Parameters.Add("@IdSupplier", SqlDbType.VarChar).Value = ddlSupplier.SelectedValue.ToString();
            cmd.Parameters.Add("@IdMaterial", SqlDbType.VarChar).Value = ddlBahanBaku.SelectedValue.ToString();
            cmd.Parameters.Add("@NoSuratJalan", SqlDbType.VarChar).Value = txtSuratJalan.Text;
            cmd.Parameters.Add("@KetTdkSesuaiBrg", SqlDbType.VarChar).Value = ddlBarang.SelectedValue.ToString();
            cmd.Parameters.AddWithValue("@NoPo", ddlNoPO.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Alasan", txtAlasan.Text);
            cmd.Parameters.AddWithValue("@CreatedBy", ((Users)Session["Users"]).UserName);
            cmd.Parameters.AddWithValue("@txtKunci", txtKunci.Text.ToString());

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            //refresh current page
            Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
        }





        /* event saat row link/ button GridView di klik */
        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "chkRow")
            {
                /*
                CheckBox chkRow = (CheckBox)e.Row.FindControl("chkRow");
                if (chkRow.Checked)
                {
                    btnApprove1.Enabled = true;
                }
                else
                {
                    btnApprove1.Enabled = false;
                }
                */
            }
        }


        protected void chkRowSelected_CheckedChanged(object sender, EventArgs e)
        {

        }


        protected void LoadUsrApvSession()
        {
            // Create ADO.NET objects.
            SqlConnection con = new SqlConnection(Global.ConnectionString());
            SqlCommand SqlCmd = new SqlCommand("spPantauBeliMaterial", con);
            //string sql = "SELECT ID, UserID, Approval FROM PantauBeliMaterialUser WHERE UserID='" + ((Users)Session["Users"]).UserName + "'";
            //Response.Write(sql);
            //Response.End();
            //SqlCommand SqlCmd = new SqlCommand(sql, con);

            // Configure command and add parameters.
            //SqlCmd.CommandType = CommandType.Text;
            SqlCmd.CommandType = CommandType.StoredProcedure;
            SqlCmd.Parameters.Add("@tampil", SqlDbType.VarChar, 25).Value = "GetApprovalID";
            SqlCmd.Parameters.AddWithValue("@nmApprover", ((Users)Session["Users"]).UserName);
            SqlCmd.Connection = con;

            con.Open();
            SqlDataReader dr = SqlCmd.ExecuteReader();
            //if (dr.HasRows)
            if (dr.Read())
            {
                //Response.Write(dr["Approval"].ToString());
                Session["pbmApproval"] = dr["Approval"];
                Session["pbmID"] = dr["ID"];
                //btnExportExcel.Text = Session["pbmApproval"].ToString();

                //Response.Write(Session["pbmApproval"].ToString());

                con.Close();
            }
        }


        private void TampilGridViewExcel()
        {
            SqlConnection con = new SqlConnection(Global.ConnectionString());
            SqlCommand cmd = new SqlCommand();

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spPantauBeliMaterial";
            cmd.Parameters.Add("@bulan", SqlDbType.VarChar, 5).Value = ddlBulanKejadian.SelectedValue; //txtID.Text.Trim();
            cmd.Parameters.Add("@tahun", SqlDbType.VarChar, 4).Value = ddlTahunKejadian.SelectedValue;
            cmd.Parameters.Add("@bulan2", SqlDbType.VarChar, 5).Value = ddlBulanKejadian2.SelectedValue;
            cmd.Parameters.AddWithValue("@NextApprover", Session["pbmApproval"]);
            cmd.Parameters.AddWithValue("@tampil", "PantauBeliMaterialSelesai");
            cmd.Connection = con;
            string nxt = Session["pbmApproval"].ToString();
            try
            {
                con.Open();
                //GridView3.EmptyDataText = "<h2>No Records Found (Tidak ada data)</h2>";

                //biar gridview support paging
                SqlDataAdapter sda = new SqlDataAdapter();
                DataTable dt = new DataTable();

                sda.SelectCommand = cmd;
                sda.Fill(dt);
                GridView3.DataSource = dt;
                GridView3.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }


        //hide kolom Tindakan Purchasing dan Klarifikasi Supplier
        //pada gridview2 approval
        int indexOfColumn1 = 1; //Note : Index will start with 0 so set this value accordingly
        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //jika ada data
            if (e.Row.Cells.Count > indexOfColumn1)
            {
                //cara 2 hide column at runtime by data gridview header text
                for (int i = 0; i < GridView2.Columns.Count; i++)
                {
                    //jika yg login bukan approver purchsing bu ika, hide column
                    if (Convert.ToInt16(Session["pbmApproval"]) > 2)
                    {
                        if (GridView2.Columns[i].HeaderText == "Tindakan Purchasing" || GridView2.Columns[i].HeaderText == "Klarifikasi Supplier")
                        {
                            e.Row.Cells[i].Visible = true;
                        }
                    }
                    else
                    {
                        if (GridView2.Columns[i].HeaderText == "Tindakan Purchasing" || GridView2.Columns[i].HeaderText == "Klarifikasi Supplier")
                        {
                            e.Row.Cells[i].Visible = false;
                        }
                    }
                }

            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //jika yg logn bu aying hide textbox
                if (Convert.ToInt16(Session["pbmApproval"]) > 3)
                {
                    //Hide the Textbox
                    simpan.Visible = false;
                    TextBox txtKlarifikasiSupplier = (TextBox)e.Row.FindControl("txtKlarifikasiSupplier");
                    txtKlarifikasiSupplier.Visible = false;

                    TextBox txtTindakanPurchasing = (TextBox)e.Row.FindControl("txtTindakanPurchasing");
                    txtTindakanPurchasing.Visible = false;
                }
                //jika approver bu ika
                if (e.Row.Cells.Count > indexOfColumn1)
                {
                    //cara 2 hide column at runtime by data gridview header text
                    for (int i = 0; i < GridView2.Columns.Count; i++)
                    {
                        if (Convert.ToInt16(Session["pbmApproval"]) == 3 && Convert.ToInt16(Session["pbmID"]) != 265 && Convert.ToInt16(Session["pbmID"]) != 271 && Convert.ToInt16(Session["pbmID"]) != 91)
                        {
                            simpan.Visible = false;
                            Label lblKlarifikasiSupplier = (Label)e.Row.FindControl("lblKlarifikasiSupplier");
                            Label lblTindakanPurchasing = (Label)e.Row.FindControl("lblTindakanPurchasing");
                            TextBox txtKlarifikasiSupplier = (TextBox)e.Row.FindControl("txtKlarifikasiSupplier");
                            TextBox txtTindakanPurchasing = (TextBox)e.Row.FindControl("txtTindakanPurchasing");
                            if (lblTindakanPurchasing.Text == string.Empty && lblKlarifikasiSupplier.Text == string.Empty)
                            {
                                GridView2.Visible = false;
                            }
                            if (lblTindakanPurchasing.Text == string.Empty && lblKlarifikasiSupplier.Text == string.Empty && txtKlarifikasiSupplier.Text == string.Empty && txtTindakanPurchasing.Text == string.Empty)
                            {
                                btnApprove1.Visible = false;
                                lblKlarifikasiSupplier.Visible = false;
                                lblTindakanPurchasing.Visible = false;
                                txtKlarifikasiSupplier.Visible = false;
                                txtTindakanPurchasing.Visible = false;
                                e.Row.Cells[i].Visible = false;
                            }
                            if (lblTindakanPurchasing.Text != string.Empty && lblKlarifikasiSupplier.Text != string.Empty)
                            {
                                GridView2.Visible = true;
                                btnApprove1.Visible = true;
                                lblKlarifikasiSupplier.Visible = true;
                                lblTindakanPurchasing.Visible = true;
                                txtKlarifikasiSupplier.Visible = false;
                                txtTindakanPurchasing.Visible = false;
                            }
                        }
                    }
                }

                //User staff purchasing input Tindakan&Klarifikasi Purchasing(Bu Dian)
                if (e.Row.Cells.Count > indexOfColumn1)
                {
                    //cara 2 hide column at runtime by data gridview header text
                    for (int i = 0; i < GridView2.Columns.Count; i++)
                    {
                        string pbid = Session["pbmID"].ToString();
                        if (Convert.ToInt16(Session["pbmID"]) == 265 || Convert.ToInt16(Session["pbmID"]) == 271 || Convert.ToInt16(Session["pbmID"]) == 91)
                        {
                            btnApprove1.Visible = false;
                            TextBox txtKlarifikasiSupplier = (TextBox)e.Row.FindControl("txtKlarifikasiSupplier");
                            TextBox txtTindakanPurchasing = (TextBox)e.Row.FindControl("txtTindakanPurchasing");
                            Label lblTindakanPurchasing = (Label)e.Row.FindControl("lblTindakanPurchasing");
                            Label lblKlarifikasiSupplier = (Label)e.Row.FindControl("lblKlarifikasiSupplier");
                            //if (lblTindakanPurchasing.Text != string.Empty && lblKlarifikasiSupplier.Text != string.Empty)
                            //{
                            //    simpan.Visible = false;
                            //    GridView2.Visible = false;
                            //}
                            if (lblTindakanPurchasing.Text == string.Empty && lblKlarifikasiSupplier.Text == string.Empty && txtKlarifikasiSupplier.Text == string.Empty && txtTindakanPurchasing.Text == string.Empty)
                            {
                                txtKlarifikasiSupplier.Visible = true;
                                txtTindakanPurchasing.Visible = true;
                            }
                            if (lblTindakanPurchasing.Text != string.Empty && lblKlarifikasiSupplier.Text != string.Empty)
                            {
                                //simpan.Visible = true;
                                txtKlarifikasiSupplier.Visible = false;
                                txtTindakanPurchasing.Visible = false;
                                e.Row.Cells[i].Visible = false;

                            }
                        }
                    }
                }
            }
        }


        //hide kolom Tindakan Purchasing dan Klarifikasi Supplier
        //pada GridView3 approval
        int indexOfColumn2 = 1; //Note : Index will start with 0 so set this value accordingly
        protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //jika ada data
            if (e.Row.Cells.Count > indexOfColumn2)
            {
                //cara 2 hide column at runtime by data gridview header text
                for (int i = 0; i < GridView3.Columns.Count; i++)
                {
                    if (Convert.ToInt16(Session["pbmApproval"]) < 3)
                    //if (Convert.ToInt32(((Users)Session["Users"]).Apv.ToString()) < 3) 
                    {
                        if (GridView3.Columns[i].HeaderText == "Tindakan Purchasing" || GridView1.Columns[i].HeaderText == "Klarifikasi Supplier")
                        {
                            e.Row.Cells[i].Visible = false;
                        }
                    }

                }

            }
        }

        protected void Simpan_Click(object sender, EventArgs e)
        {
            int jmldicek = 0;
            string txtTindakanPurchasing = string.Empty;
            string txtKlarifikasiSupplier = string.Empty;

            foreach (GridViewRow row in GridView2.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[11].FindControl("chkRow") as CheckBox);
                    if (chkRow.Checked) //jika checkbox di centang
                    {
                        jmldicek = jmldicek + 1;

                        txtIdApprove.Text = txtIdApprove.Text + row.Cells[1].Text + ",";

                        //jika user login = purchasing approver
                        if (Convert.ToInt16(Session["pbmApproval"]) == 3 && Convert.ToInt16(Session["pbmID"]) == 265 || Convert.ToInt16(Session["pbmID"]) == 271 || Convert.ToInt16(Session["pbmID"]) == 91)
                        {
                            string f_TindakanPurchasing = ((TextBox)row.FindControl("txtTindakanPurchasing")).Text;
                            string f_KlarifikasiSupplier = ((TextBox)row.FindControl("txtKlarifikasiSupplier")).Text;
                            int f_IdPBM = int.Parse(row.Cells[1].Text.ToString());
                            using (SqlConnection conn = new SqlConnection(Global.ConnectionString()))
                            {
                                using (SqlCommand cmd3 = new SqlCommand("update PantauBeliMaterial set TindakanPurchasing=@TindakanPurchasing, KlarifikasiSupplier=@KlarifikasiSupplier where ID=@IdPBM", conn))
                                {
                                    cmd3.CommandType = CommandType.Text;
                                    cmd3.Parameters.AddWithValue("@TindakanPurchasing", f_TindakanPurchasing);
                                    cmd3.Parameters.AddWithValue("@KlarifikasiSupplier", f_KlarifikasiSupplier);
                                    cmd3.Parameters.AddWithValue("@IdPBM", f_IdPBM);

                                    conn.Open();
                                    cmd3.ExecuteNonQuery();
                                    conn.Close();
                                }
                            }
                        }
                    }
                }
            }

            if (jmldicek > 0)
            {
                string txtIdApprove1 = string.Empty;
                txtIdApprove1 = txtIdApprove.Text;
                txtIdApprove1 = txtIdApprove1.Substring(0, (txtIdApprove1.Length) - 1);
                //Response.End();

                txtIdApprove.Text = "";

                //SqlConnection con = new SqlConnection(Global.ConnectionString());
                //SqlCommand cmd = new SqlCommand("spPantauBeliMaterial", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.Add("@tampil", SqlDbType.VarChar).Value = "prsApprove";
                //cmd.Parameters.AddWithValue("@txtIdApprove", txtIdApprove1.ToString());
                ////cmd.Parameters.AddWithValue("@txtIdApprove1", "'"+txtIdApprove1.ToString()+"'");
                //cmd.Parameters.AddWithValue("@NextApprover", Session["pbmApproval"]);
                //cmd.Parameters.AddWithValue("@nmApprover", ((Users)Session["Users"]).UserName);
                //con.Open();
                //cmd.ExecuteNonQuery();
                //con.Close();

                //refresh current page
                //Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
                Response.Redirect(Request.RawUrl.ToString(), true);
            }
        }

    }
}