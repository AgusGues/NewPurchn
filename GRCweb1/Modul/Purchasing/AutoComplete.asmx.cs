using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using System.Data;
using System.Data.SqlClient;
using DataAccessLayer;
using System.Text;
using BusinessFacade;
using Domain;

namespace GRCweb1.Modul.Purchasing
{
    /// <summary>
    /// Summary description for AutoComplete
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class AutoComplete : System.Web.Services.WebService
    {

        public AutoComplete()
        {

            //Uncomment the following line if using designed components
            //InitializeComponent();
        }
        [WebMethod]
        public string[] GetItemListBB(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select  top 10  rtrim(ItemName) as ItemName from Inventory Where rowstatus>-1 and groupid=1 and ItemName like @myParameter order by ItemName";
            cmd.Parameters.AddWithValue("@myParameter", "%" + prefixText.Trim() + "% ");
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["ItemName"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }

            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetNoProdukBM(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select  top 10  PartNo from fc_items Where rowstatus>-1 and itemtypeid=1 and PartNo like @myParameter order by PartNo";
            cmd.Parameters.AddWithValue("@myParameter", "%" + prefixText.Trim() + "%");

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["PartNo"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }

            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetNoProdukBM1(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select top 10 PartNo from fc_items Where rowstatus>-1 and (itemtypeid=1 or SUBSTRING(PartNo,4,3)='-1-' or SUBSTRING(PartNo,5,3)='-1-' or SUBSTRING(PartNo,6,3)='-1-')and PartNo like @myParameter order by PartNo";
            cmd.Parameters.AddWithValue("@myParameter", "%" + prefixText.Trim() + "%");

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["PartNo"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }

            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetNoProdukJadi(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "select top 50  PartNo from fc_items Where rowstatus>-1 and (itemtypeid=3) and substring(PartNo,5,1)<>'P' and PartNo like @myParameter  order by kode asc,tebal desc,lebar asc";
            cmd.CommandText = "select top 10  PartNo from fc_items Where rowstatus>-1 /*and (PartNo like '%-S-%' or PartNo like '%-3-%' or partno like '%-w-%' or partno like '%-m-%'  or PartNo like '%-M-%')*/ and PartNo like @myParameter  order by kode asc,tebal desc,lebar asc";
            cmd.Parameters.AddWithValue("@myParameter", "%" + prefixText.Trim() + "%");
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["PartNo"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }

            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetNoProdukOK(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select top 10 PartNo from fc_items Where rowstatus>-1 and itemtypeid=3 and PartNo Like '%-3-%' and PartNo like @myParameter order by PartNo desc";
            cmd.Parameters.AddWithValue("@myParameter", "%" + prefixText.Trim() + "%");

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["PartNo"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }
            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetNoProdukBP(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select top 10 PartNo from fc_items Where rowstatus>-1 and itemtypeid=3 and substring(PartNo,5,1)='P' and substring(PartNo,5,1)<>'1' and PartNo like @myParameter order by PartNo";
            cmd.Parameters.AddWithValue("@myParameter", "%" + prefixText.Trim() + "%");

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["PartNo"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }

            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetNoProdukBS(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select top 10 PartNo from fc_items Where rowstatus>-1 and itemtypeid=3 and PartNo Like '%-S-%' and PartNo like @myParameter order by PartNo desc";
            cmd.Parameters.AddWithValue("@myParameter", "%" + prefixText.Trim() + "%");

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["PartNo"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }
            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetNoProdukBPDirect(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select top 10 PartNo from fc_items Where rowstatus>-1 and ID in (select distinct itemID from t3_rekap where process='direct' and cutqty=0) and PartNo like '%-P-%'  and PartNo like @myParameter order by PartNo";
            cmd.Parameters.AddWithValue("@myParameter", "%" + prefixText.Trim() + "%");

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["PartNo"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }

            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetNoProdukUF(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select top 10 PartNo from fc_items Where rowstatus>-1 and (itemtypeid=3) and substring(PartNo,5,1)='1' and PartNo like @myParameter order by PartNo";
            cmd.Parameters.AddWithValue("@myParameter", "%" + prefixText.Trim() + "%");

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["PartNo"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }

            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetNoProdukAsalListplank(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT distinct top 10 PartNo FROM FC_Items where rowstatus>-1 and ID in (select distinct itemid0 from vw_kartustocklistplank where process='i99') and PartNo like @myParameter order by partno";
            cmd.Parameters.AddWithValue("@myParameter", "%" + prefixText.Trim() + "%");

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["PartNo"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }
            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetNoProduk(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT distinct top 10 PartNo FROM FC_Items where rowstatus>-1 and PartNo like @myParameter order by partno desc";
            cmd.Parameters.AddWithValue("@myParameter", "%" + prefixText.Trim() + "%");

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["PartNo"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }
            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetNoProdukStock(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT distinct top 10 C.PartNo FROM  T3_Serah AS A INNER JOIN FC_Items AS C ON A.ItemID = C.ID INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID where A.Qty>0 and C.rowstatus>-1 and C.PartNo like @myParameter order by C.partno desc";
            cmd.Parameters.AddWithValue("@myParameter", "%" + prefixText.Trim() + "%");

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["PartNo"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }
            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetNoProdukKirim(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT distinct top 10  C.PartNo FROM  T3_Serah AS A INNER JOIN FC_Items AS C ON A.ItemID = C.ID INNER JOIN " +
                "FC_Lokasi AS B ON A.LokID = B.ID where A.Qty>0 and C.rowstatus>-1 and C.PartNo like @myParameter order by C.partno desc";
            cmd.Parameters.AddWithValue("@myParameter", "%" + prefixText.Trim() + "%");
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["PartNo"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }
            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetLokasiStock(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT distinct top 10  B.Lokasi FROM  T3_Serah AS A INNER JOIN FC_Items AS C ON A.ItemID = C.ID " +
                "INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID where B.Lokasi like @myParameter and C.rowstatus>-1  order by B.Lokasi";
            cmd.Parameters.AddWithValue("@myParameter", prefixText.Trim() + "%");
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["Lokasi"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }
            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetLokasiStockBP(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT  distinct top 10  B.Lokasi FROM  T3_Serah AS A INNER JOIN FC_Items AS C ON A.ItemID = C.ID " +
                "INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID where C.rowstatus>-1 and B.Lokasi<> 'H99' and B.Lokasi like @myParameter order by B.Lokasi";
            cmd.Parameters.AddWithValue("@myParameter", prefixText.Trim() + "%");
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["Lokasi"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }
            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetLokasiStockBPTransit(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT  Lokasi FROM  FC_Lokasi where lokasi ='B99' or lokasi ='C99' ";
            cmd.Parameters.AddWithValue("@myParameter", prefixText.Trim() + "%");
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["Lokasi"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }
            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetLokasiStockP(string prefixText, int count, string contextKey)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT  distinct top 10  B.Lokasi FROM  T3_Serah AS A INNER JOIN FC_Items AS C ON A.ItemID = C.ID INNER JOIN FC_Lokasi AS B" +
                " ON A.LokID = B.ID where C.partno='" + contextKey + "' and A.Qty>0 and C.rowstatus>-1 and B.Lokasi like @myParameter order by B.Lokasi";
            cmd.Parameters.AddWithValue("@myParameter", "%" + prefixText.Trim() + "%");
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["Lokasi"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }
            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetLokasiBM(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select  top 10  Lokasi from fc_Lokasi Where loktypeid=1 and Lokasi like @myParameter order by lokasi";
            cmd.Parameters.AddWithValue("@myParameter", "%" + prefixText.Trim() + "%");

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];
            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;
            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["Lokasi"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }

            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetLokasiT1(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select top 10 Lokasi from fc_Lokasi Where ID in(select distinct lokasiID from BM_destacking union " +
                "select distinct lokid from t1_jemurlg)  and Lokasi like @myParameter order by Lokasi";
            cmd.Parameters.AddWithValue("@myParameter", "%" + prefixText.Trim() + "%");
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["Lokasi"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }

            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetLokasiTransT1(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select top 10 Lokasi from fc_Lokasi Where loktypeid=2 and Lokasi like @myParameter order by Lokasi";
            cmd.Parameters.AddWithValue("@myParameter", "%" + prefixText.Trim() + "%");
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["Lokasi"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }

            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetLokasiTransT3(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select top 10 Lokasi from fc_Lokasi Where rowstatus>-1 and loktypeid=3 and Lokasi like @myParameter order by Lokasi";
            cmd.Parameters.AddWithValue("@myParameter", prefixText.Trim() + "%");
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["Lokasi"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }

            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetLokasiTransit(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select top 10 Lokasi from fc_Lokasi Where loktypeid=2 and Lokasi like @myParameter order by Lokasi";
            cmd.Parameters.AddWithValue("@myParameter", prefixText.Trim() + "%");
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["Lokasi"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }

            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetLokasiTransKirim(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select top 10 Lokasi from fc_Lokasi Where loktypeid=3 and Lokasi like @myParameter order by Lokasi";
            cmd.Parameters.AddWithValue("@myParameter", prefixText.Trim() + "%");
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["Lokasi"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }
            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetPaletBM(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select top 10 NoPalet from BM_palet Where rowstatus=0 and NoPalet like @myParameter order by nopalet";
            cmd.Parameters.AddWithValue("@myParameter", prefixText.Trim() + "%");
            //rowstatus=-1:deleted,0:ready,1:used
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["NoPalet"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }
            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetPaletByTglPrd(string prefixText, int count, string contextKey)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select top 10 NoPalet from BM_palet Where rowstatus=0 and " + contextKey + " and NoPalet like @myParameter order by nopalet";
            cmd.Parameters.AddWithValue("@myParameter", prefixText.Trim() + "%");
            //rowstatus=-1:deleted,0:ready,1:used
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["NoPalet"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }
            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetRakFC(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select top 10 Rak from fc_rak Where  Rak like @myParameter order by rak";
            cmd.Parameters.AddWithValue("@myParameter", prefixText.Trim() + "%");
            //rowstatus=-1:deleted,0:ready,1:used
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["Rak"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }
            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetSJNo(string prefixText, int count, string contextKey)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select top 10 sjno from t3_kirim  Where  sjno like @myParameter  order by id desc";
            cmd.Parameters.AddWithValue("@myParameter", prefixText.Trim() + "%");
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["sjno"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }
            return txtItems.ToArray();
        }

        [WebMethod]
        public string[] GetOPNo(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select top 10 OPNo from OP Where  OPNo like @myParameter order by id desc";
            cmd.Parameters.AddWithValue("@myParameter", prefixText.Trim() + "%");
            //rowstatus=-1:deleted,0:ready,1:used
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["OPNo"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }
            return txtItems.ToArray();
        }

        [WebMethod]
        public string[] GetCustomer(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            string query = "select top " + count + " cs as customername from(select distinct(Customer) cs from T3_Kirim  where customer like @myParameter )as x order by cs";
            cmd.CommandText = query;// "select top 10 customername from customer Where  customername like @myParameter order by customername";
            cmd.Parameters.AddWithValue("@myParameter", "%" + prefixText.Trim() + "%");
            //rowstatus=-1:deleted,0:ready,1:used
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["customername"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }
            return txtItems.ToArray();
        }

        [WebMethod]
        public string[] GetNoBA(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select top 10 NoBA from t3_Adjust Where  NoBA like @myParameter order by NoBA";
            cmd.Parameters.AddWithValue("@myParameter", "%" + prefixText.Trim() + "%");
            //rowstatus=-1:deleted,0:ready,1:used
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["NoBA"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }
            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetNoSPB(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select top 10 PakaiNo  from Pakai  where PakaiNo like @myParameter order by PakaiNo";
            cmd.Parameters.AddWithValue("@myParameter", "%" + prefixText.Trim() + "%");
            //rowstatus=-1:deleted,0:ready,1:used
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["PakaiNo"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }
            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetNamaBarang(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select  top 10  ItemName  from BItemName  where ItemName like @myParameter order by ItemName";
            cmd.Parameters.AddWithValue("@myParameter", prefixText.Trim() + "%");
            //rowstatus=-1:deleted,0:ready,1:used
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["ItemName"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }
            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetMerk(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select  top 10  InMerk  from BItemMerk  where InMerk like @myParameter order by InMerk";
            cmd.Parameters.AddWithValue("@myParameter", prefixText.Trim() + "%");
            //rowstatus=-1:deleted,0:ready,1:used
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["InMerk"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }
            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetType(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select  top 10  INType from BItemMerk  where INType like @myParameter order by INType";
            cmd.Parameters.AddWithValue("@myParameter", prefixText.Trim() + "%");
            //rowstatus=-1:deleted,0:ready,1:used
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["INType"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }
            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetSJNofromSiapKirim(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select  top 10  distinct SJNo  from T3_SiapKirim where QtyIn >QtyOut and ISNULL(sjno,'-')<>'-' and sjno like '%" + prefixText + "%' order by SJNo ";
            cmd.Parameters.AddWithValue("@myParameter", prefixText.Trim() + "%");
            //rowstatus=-1:deleted,0:ready,1:used
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["SJNo"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }
            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetReceiptByTagihanSupplier(string prefixText, int count, string contextKey)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            string kriteria = string.Empty;
            if (contextKey.Trim() != "0")
                kriteria = " and receipttype in (select ID from GroupsPurchn where groupdescription like '" + contextKey + "%') ";
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select top 10  suppliername from SuppPurch where rowstatus>-1 and ID in(select SupplierId from Receipt " +
                "where [status]=0" + kriteria + ") and suppliername like '%" + prefixText + "%' order by SupplierName";
            cmd.Parameters.AddWithValue("@myParameter", prefixText.Trim() + "%");

            //rowstatus=-1:deleted,0:ready,1:used
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["suppliername"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }
            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetReceiptByTagihan(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();


            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select  top 10  receiptno  from receipt where [status]=0 and ISNULL(receiptno,'-')<>'-' and receiptno like '%" + prefixText + "%' order by receiptno desc ";
            cmd.Parameters.AddWithValue("@myParameter", prefixText.Trim() + "%");
            //rowstatus=-1:deleted,0:ready,1:used
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["receiptno"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }
            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetItemInventory(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();


            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select  top 10  itemname  from Inventory where [rowstatus]>-1 and aktif=1 and itemname like '" + prefixText + "%' order by itemname  ";
            cmd.Parameters.AddWithValue("@myParameter", prefixText.Trim() + "%");
            //rowstatus=-1:deleted,0:ready,1:used
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["itemname"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }
            return txtItems.ToArray();
        }
        [WebMethod(EnableSession = true)]
        public string[] GetSJNoLo(string prefixText, int count, string contextKey)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            bpas_api.WebService1 api = new bpas_api.WebService1();

            ds = api.GetSJ(prefixText, "GRCBoardPusat", "KR", DateTime.Now.Year.ToString());
            dt = ds.Tables[0];
            List<string> txtItems = new List<string>();
            String dbValues;
            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["SuratJalanNo"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }
            return txtItems.ToArray();
        }
        [WebMethod(EnableSession = true)]
        public string[] GetProjectName(string prefixText, int count)
        {
            string newProject = HttpContext.Current.Session["NewProject"].ToString();
            string Criteria = (newProject == string.Empty) ? string.Empty : " and SubProject is null ";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = BusinessFacade.Global.ConnectionString();
            string strSQL = "Select ProjectName from MTC_Project where ProjectName Like '%" + prefixText + "%' " + Criteria + " order by projectname";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSQL;
            cmd.Parameters.AddWithValue("@myParameter", prefixText.Trim());
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
            }
            catch { }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];
            string dbValues;
            List<string> txtItems = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                dbValues = row["ProjectName"].ToString().ToUpper();
                txtItems.Add(dbValues);
            }

            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetNoProdukOKbyLuas(string prefixText, int count, string contextKey)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select top 10 PartNo from fc_items Where rowstatus>-1 " + contextKey +
                " and itemtypeid=3 and (PartNo Like '%-3-%' or partno like '%-w-%' or partno like '%-m-%'  or PartNo like '%-M-%') and PartNo like @myParameter order by PartNo desc";
            cmd.Parameters.AddWithValue("@myParameter", "%" + prefixText.Trim() + "%");
            string test = cmd.CommandText;
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["PartNo"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }
            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetNoProdukBPbyLuas(string prefixText, int count, string contextKey)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select top 10 PartNo from fc_items Where rowstatus>-1  " + contextKey +
                " and itemtypeid=3 and PartNo like '%-P-%' and PartNo like @myParameter order by PartNo";
            cmd.Parameters.AddWithValue("@myParameter", "%" + prefixText.Trim() + "%");

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["PartNo"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }

            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetNoProdukBSbyLuas(string prefixText, int count, string contextKey)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select top 10 PartNo from fc_items Where rowstatus>-1  " + contextKey + " and itemtypeid=3 and PartNo Like '%-S-%' and PartNo like @myParameter order by PartNo desc";
            cmd.Parameters.AddWithValue("@myParameter", "%" + prefixText.Trim() + "%");

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["PartNo"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }
            return txtItems.ToArray();
        }

        [WebMethod(EnableSession = true)]
        public string[] GetSatuan(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            cn.ConnectionString = BusinessFacade.Global.ConnectionString();
            string strSQL = "Select Top 20 ID,UomCode from UoM where UomDesc like '%" + prefixText.TrimEnd() + "%' order by UomCode";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSQL;
            cmd.Parameters.AddWithValue("@myparameter", "'%" + prefixText.TrimEnd() + "%'");
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
            }
            catch { }
            finally { cn.Close(); }
            dt = ds.Tables[0];
            string dbValues = string.Empty;
            List<string> txtItem = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                txtItem.Add(dr["UomCode"].ToString().ToUpper());
            }
            return txtItem.ToArray();
        }

        [WebMethod]
        public string[] GetNoPO(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = BusinessFacade.Global.ConnectionString();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            string strSQL = "Select top " + count + " ID,NoPO from POPurchn where Approval=2 and Status >-1 and NoPO like '%" + prefixText.TrimEnd() + "%' order by nopo";
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSQL;
            cmd.Parameters.AddWithValue("@myParameter", "'%" + prefixText.TrimEnd() + "%'");
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
            }
            catch { }
            finally { cn.Close(); }
            dt = ds.Tables[0];
            List<string> txtItem = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                txtItem.Add(dr["NoPo"].ToString().ToUpper());
            }
            return txtItem.ToArray();
        }
        [WebMethod]
        public string[] GetBANum(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = BusinessFacade.Global.ConnectionString();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            string strSQL = "Select top " + count + " BANum from BeritaAcara where RowStatus >-1 and BANum like '%" + prefixText.TrimEnd() + "%' order by BANum";
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSQL;
            cmd.Parameters.AddWithValue("@myParameter", "'%" + prefixText.TrimEnd() + "%'");
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
            }
            catch { }
            finally { cn.Close(); }
            dt = ds.Tables[0];
            List<string> txtItem = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                txtItem.Add(dr["BANum"].ToString().ToUpper());
            }
            return txtItem.ToArray();
        }

        [WebMethod]
        public string[] GetMasterPartNoT3(string prefixText)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select top 10 PartNo from fc_items Where rowstatus>-1 and partno like '%" + prefixText.TrimEnd() + "%' and itemtypeid=3 order by PartNo desc";
            cmd.Parameters.AddWithValue("@myParameter", prefixText.Trim() + "%");
            string test = cmd.CommandText;
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["PartNo"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }
            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetListProjectNo(string prefixText, int count)
        {
            List<string> lst = new List<string>();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = BusinessFacade.Global.ConnectionString();
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string strSQL = "Select top " + count + " Nomor from MTC_Project where Nomor like '%" + prefixText + "%' and RowStatus>-1";
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSQL;
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
            }
            catch (Exception e)
            {
                lst.Add(e.Message);
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                lst.Add(dr["Nomor"].ToString().ToUpper());
            }
            return lst.ToArray();
        }

        [WebMethod]
        public string[] GetKetBiaya(string prefixText, int count)
        {
            List<string> lst = new List<string>();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = BusinessFacade.Global.ConnectionString();
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string strSQL = "Select Distinct Keterangan1 from SPPDetail where Keterangan1 like '%" + prefixText + "%' Order by Keterangan1";
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSQL;
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
            }
            catch (Exception e)
            {
                lst.Add(e.Message);
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                lst.Add(dr["Keterangan1"].ToString().ToUpper());
            }
            return lst.ToArray();
        }
        [WebMethod]
        public string[] GetExpedisiTK()
        {
            List<string> lst = new List<string>();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = BusinessFacade.Global.ConnectionString();
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string strSQL = "Select ID,Expedisi from delivexpedisi where rowstatus>-1 order by expedisi ";
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSQL;
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
            }
            catch (Exception e)
            {
                lst.Add(e.Message);
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                //lst.Add(dr["Keterangan1"].ToString().ToUpper());
                string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["Expedisi"].ToString(), dr["ID"].ToString());
                lst.Add(item);
            }
            return lst.ToArray();
        }

        [WebMethod]
        public string[] GetSupplier(string prefixText, int count)
        {
            List<string> lst = new List<string>();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = BusinessFacade.Global.ConnectionString();
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string strSQL = "Select top " + count + " ID,SupplierCode,SupplierName From SuppPurch " +
                            "Where RowStatus>-1 AND (aktif=0 or aktif is null) AND SupplierName like '%" +
                            prefixText + "%' Order by SupplierName";
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSQL;
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
            }
            catch (Exception e)
            {
                lst.Add(e.Message);
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                //lst.Add(dr["Keterangan1"].ToString().ToUpper());
                string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["SupplierName"].ToString(), dr["ID"].ToString());
                lst.Add(item);
            }
            return lst.ToArray();
        }
        [WebMethod]
        public string[] GetExpedisi(string prefixText)
        {
            List<string> lst = new List<string>();
            string[] Expedisi = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Expedisi", "DepoKertas").Split(',');
            for (int i = 0; i < Expedisi.Count(); i++)
            {
                lst.Add(Expedisi[i].ToString());
            }
            return lst.ToArray();
        }

        [WebMethod]
        public string[] GetSupplierKAT(string prefixText, string count)
        {
            /*base on request bu ika hanya kat dan sbr*/
            string Criteria = "Where RowStatus>-1 AND (aktif=0 OR aktif is null) AND SupplierName like '%" +
                            prefixText + "%' and (SubCompanyID in(5,6) OR ForDK=5 )";
            System.Web.SessionState.HttpSessionState session = System.Web.HttpContext.Current.Session;
            //int user = 1;
            //string strCon = (count == "1") ? "GRCBoardCtrp" : "GRCBoardKrwg";
            string strCon = string.Empty;
            if (count == "1") strCon = "GRCBoardCtrp";
            if (count == "7") strCon = "GRCBoardKrwg";
            if (count == "13") strCon = "GRCBoardJmb";
            //string strCon = BusinessFacade.Global.ConnectionString();
            Global2 global = new Global2();
            //bpas_api.WebService1 apiweb = new bpas_api.WebService1();
            List<string> lst = new List<string>();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ds = global.GetDataTable("SuppPurch", "ID,SupplierName,SupplierCode", Criteria, strCon);
            dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                //lst.Add(dr["Keterangan1"].ToString().ToUpper());
                string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["SupplierName"].ToString(), dr["ID"].ToString());
                lst.Add(item);
            }
            return lst.ToArray();
        }
        [WebMethod]
        public string[] GetSupplierKertas(string prefixText, string count)
        {
            string Criteria = "Where RowStatus>-1 AND (aktif=0 OR aktif is null) AND SupplierName like '%" + prefixText + "%'";
            System.Web.SessionState.HttpSessionState session = System.Web.HttpContext.Current.Session;
            //int user = 1;
            string strCon = (count == "1") ? "GRCBoardCtrp" : "GRCBoardKrwg";
            if (count == "1")
                strCon = "GRCBoardCtrp";
            if (count == "7")
                strCon = "GRCBoardKrwg";
            if (count == "13")
                strCon = "GRCBoardJmb";
            //string strCon = BusinessFacade.Global.ConnectionString();
            Global2 apiweb = new Global2();
            //bpas_api.WebService1 apiweb = new bpas_api.WebService1();
            List<string> lst = new List<string>();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            ds = apiweb.GetDataTable("SuppPurch", "ID,SupplierName,SupplierCode", Criteria, strCon);
            dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                //lst.Add(dr["Keterangan1"].ToString().ToUpper());
                string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["SupplierName"].ToString(), dr["ID"].ToString());
                lst.Add(item);
            }
            return lst.ToArray();
        }
        [WebMethod]
        public string[] GetNoProdukCC(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT distinct top " + count + " C.ID, C.PartNo FROM  /*T3_Serah AS A INNER JOIN */FC_Items AS C /*ON A.ItemID = C.ID " +
                              "INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID*/ where C.RowStatus>-1 and C.PartNo like @myParameter order by C.partno";
            cmd.Parameters.AddWithValue("@myParameter", "%" + prefixText.Trim() + "%");

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch { }
            finally { cn.Close(); }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow dr in dt.Rows)
            {
                //String From DataBase(dbValues)
                string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["PartNo"].ToString().TrimEnd(), dr["ID"].ToString());
                txtItems.Add(item);
            }
            return txtItems.ToArray();
        }
        [WebMethod]
        public string[] GetInventory(string prefixText, string count)
        {
            List<string> lst = new List<string>();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = BusinessFacade.Global.ConnectionString();
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string strSQL = "Select top " + count + " ID,ItemCode,ItemName From Inventory " +
                            "Where Aktif=1 AND ItemName like '%" +
                            prefixText + "%' Order by ItemName";
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSQL;
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
            }
            catch (Exception e)
            {
                lst.Add(e.Message);
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                //lst.Add(dr["Keterangan1"].ToString().ToUpper());
                string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["ItemName"].ToString(), dr["ID"].ToString());
                lst.Add(item);
            }
            return lst.ToArray();
        }
        [WebMethod]
        public string[] GetKlausulNo(string prefixText, int count)
        {
            List<string> lst = new List<string>();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = BusinessFacade.Global.ConnectionString();
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string strSQL = "Select Distinct Klausul_No from TPP_Klausul_no where Klausul_No like '" + prefixText + "%' Order by Klausul_No";
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSQL;
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
            }
            catch (Exception e)
            {
                lst.Add(e.Message);
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                lst.Add(dr["Klausul_No"].ToString().ToUpper());
            }
            return lst.ToArray();
        }
        [WebMethod]
        public string[] GetParsialPO(string prefixText, int count)
        {
            string TglMundurSCH = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("TglMundurSch", "Receipt");
            string TglHariINI = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Today", "Receipt");
            List<string> lst = new List<string>();
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = BusinessFacade.Global.ConnectionString();
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string strSQL = "Select Distinct NoPO from POPurchn WHERE ID in(SELECT distinct POID FROM Memoharian_PO  WHERE " +
                            "SchID in(SELECT ID from MemoHarian where DvlDate Between DATEADD(DAY,(-1* ((Select dbo.GetOFFDay(GETDATE()-" +
                            TglMundurSCH + ",GETDATE()))+2)),GETDATE()) AND GETDATE()-" + TglHariINI + ")) AND NoPO like '%" +
                            prefixText + "%' ORDER By NoPO";
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSQL;
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
            }
            catch (Exception e)
            {
                lst.Add(e.Message);
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                lst.Add(dr["NoPO"].ToString().ToUpper());
            }
            return lst.ToArray();
        }
        [WebMethod]
        public string[] GetLokasiStockCC(string prefixText, int count)
        {
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = BusinessFacade.Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT distinct top 10 B.ID,B.Lokasi FROM  T3_Serah AS A INNER JOIN FC_Items AS C ON A.ItemID = C.ID " +
                "INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID where C.rowstatus>-1 and B.Lokasi  like @myParameter  order by B.Lokasi";
            cmd.Parameters.AddWithValue("@myParameter", prefixText.Trim() + "%");
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];

            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;

            foreach (DataRow dr in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(dr["Lokasi"].ToString().TrimEnd(), dr["ID"].ToString());
                txtItems.Add(dbValues);
            }
            return txtItems.ToArray();
        }

        [WebMethod]
        public string[] GetItemATK(string prefixText, int count, string contextKey)
        {
            string OnlyHR = contextKey.ToString();
            SqlConnection cn = new SqlConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataAccess dataAccess = new DataAccess(BusinessFacade.Global.ConnectionString());
            String strCn = Global.ConnectionString();
            cn.ConnectionString = strCn;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select top 20 ItemName,ID,ItemCode from inventory Where GroupID in('3')and LEN(ItemCode)>12 " + OnlyHR + " and (ItemName like @myParameter OR ID like @myParameter) order by ItemName";
            cmd.Parameters.AddWithValue("@myParameter", "%" + prefixText.Trim() + "%");

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch
            {
            }
            finally
            {
                cn.Close();
            }
            dt = ds.Tables[0];
            //Then return List of string(txtItems) as result
            List<string> txtItems = new List<string>();
            String dbValues;
            foreach (DataRow row in dt.Rows)
            {
                //String From DataBase(dbValues)
                dbValues = row["ItemCode"].ToString() + "-" + row["ItemName"].ToString();// +" " + row["ID"].ToString();
                dbValues = dbValues.ToUpper().Trim();
                txtItems.Add(dbValues);
            }

            return txtItems.ToArray();
        }
    }
}
