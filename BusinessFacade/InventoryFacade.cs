using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using Domain;
using DataAccessLayer;

namespace BusinessFacade
{
     public class InventoryFacade : AbstractFacade
    {
        private Inventory objInventory = new Inventory();
        private KartStock objKs = new KartStock();
        private ArrayList arrInventory;
        private List<SqlParameter> sqlListParam;
        public string Limit { get; set; }
        public InventoryFacade()
            : base()
        {
           
        }

        public override int Insert(object objDomain)
        {
            try
            {
                objInventory = (Inventory)objDomain;             
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemCode", objInventory.ItemCode));
                sqlListParam.Add(new SqlParameter("@ItemName", objInventory.ItemName));
                sqlListParam.Add(new SqlParameter("@SupplierCode", objInventory.SupplierCode));
                sqlListParam.Add(new SqlParameter("@UOMID", objInventory.UOMID));
                sqlListParam.Add(new SqlParameter("@Jumlah", objInventory.Jumlah));
                sqlListParam.Add(new SqlParameter("@Harga", objInventory.Harga));
                sqlListParam.Add(new SqlParameter("@MinStock", objInventory.MinStock));
                sqlListParam.Add(new SqlParameter("@DeptID", objInventory.DeptID)); ;
                sqlListParam.Add(new SqlParameter("@RakID", objInventory.RakID));
                sqlListParam.Add(new SqlParameter("@GroupID", objInventory.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objInventory.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@Gudang", objInventory.Gudang));
                sqlListParam.Add(new SqlParameter("@ShortKey", objInventory.ShortKey));
                sqlListParam.Add(new SqlParameter("@Keterangan", objInventory.Keterangan));
                sqlListParam.Add(new SqlParameter("@Head", objInventory.Head));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objInventory.CreatedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objInventory.LastModifiedBy));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertInventory");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int InsertNew(object objDomain)
        {
            try
            {
                objInventory = (Inventory)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemCode", objInventory.ItemCode));
                sqlListParam.Add(new SqlParameter("@ItemName", objInventory.ItemName));
                sqlListParam.Add(new SqlParameter("@SupplierCode", objInventory.SupplierCode));
                sqlListParam.Add(new SqlParameter("@UOMID", objInventory.UOMID));
                sqlListParam.Add(new SqlParameter("@Jumlah", objInventory.Jumlah));
                sqlListParam.Add(new SqlParameter("@Harga", objInventory.Harga));
                sqlListParam.Add(new SqlParameter("@MinStock", objInventory.MinStock));
                sqlListParam.Add(new SqlParameter("@DeptID", objInventory.DeptID)); ;
                sqlListParam.Add(new SqlParameter("@RakID", objInventory.RakID));
                sqlListParam.Add(new SqlParameter("@GroupID", objInventory.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objInventory.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@Gudang", objInventory.Gudang));
                sqlListParam.Add(new SqlParameter("@ShortKey", objInventory.ShortKey));
                sqlListParam.Add(new SqlParameter("@Keterangan", objInventory.Keterangan));
                sqlListParam.Add(new SqlParameter("@Head", objInventory.Head));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objInventory.CreatedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objInventory.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@Nama", objInventory.Nama));
                sqlListParam.Add(new SqlParameter("@Type", objInventory.Type));
                sqlListParam.Add(new SqlParameter("@Ukuran", objInventory.Ukuran));
                sqlListParam.Add(new SqlParameter("@Merk", objInventory.Merk));
                sqlListParam.Add(new SqlParameter("@Jenis", objInventory.Jenis));
                sqlListParam.Add(new SqlParameter("@PartNum", objInventory.Partnum));
                sqlListParam.Add(new SqlParameter("@LeadTime", objInventory.LeadTime));
                //iko
                sqlListParam.Add(new SqlParameter("@AmGroupID", objInventory.AmGroupID));
                sqlListParam.Add(new SqlParameter("@AmClassID", objInventory.AmClassID));
                sqlListParam.Add(new SqlParameter("@AmSubClassID", objInventory.AmSubClassID));
                sqlListParam.Add(new SqlParameter("@AmLokasiID", objInventory.AmLokasiID));
                sqlListParam.Add(new SqlParameter("@AmKodeAsset", objInventory.AmKodeAsset));
                //iko

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertInventory1");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        
         public override int Update(object objDomain)
        {
            try
            {
                objInventory = (Inventory)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objInventory.ID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objInventory.ItemTypeID ));
                sqlListParam.Add(new SqlParameter("@MinStock", objInventory.MinStock));
                sqlListParam.Add(new SqlParameter("@Reorder", objInventory.ReOrder));
                sqlListParam.Add(new SqlParameter("@RakID", objInventory.RakID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objInventory.CreatedBy));
                sqlListParam.Add(new SqlParameter("@aktif", objInventory.Aktif ));
                sqlListParam.Add(new SqlParameter("@stock", objInventory.Stock));
                sqlListParam.Add(new SqlParameter("@alasannonaktif", objInventory.Alasannonaktif ));
                sqlListParam.Add(new SqlParameter("@maxstock", objInventory.MaxStock));
                sqlListParam.Add(new SqlParameter("@leadtime", objInventory.LeadTime));
                sqlListParam.Add(new SqlParameter("@UomID", objInventory.UOMID));
                sqlListParam.Add(new SqlParameter("@GudangID", objInventory.Gudang));
                sqlListParam.Add(new SqlParameter("@shortkey", objInventory.ShortKey));
                //iko
                //boleh diubah slama belum di-receipt ya...
                sqlListParam.Add(new SqlParameter("@AmGroupID", objInventory.AmGroupID));
                sqlListParam.Add(new SqlParameter("@AmClassID", objInventory.AmClassID));
                sqlListParam.Add(new SqlParameter("@AmSubClassID", objInventory.AmSubClassID));
                sqlListParam.Add(new SqlParameter("@AmLokasiID", objInventory.AmLokasiID));
                sqlListParam.Add(new SqlParameter("@AmKodeAsset", objInventory.AmKodeAsset));
                //iko


                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateInventory");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

         public  int UpdateNew(object objDomain)
         {
             try
             {
                 objInventory = (Inventory)objDomain;
                 sqlListParam = new List<SqlParameter>();
                 sqlListParam.Add(new SqlParameter("@ID", objInventory.ID));
                 sqlListParam.Add(new SqlParameter("@MinStock", objInventory.MinStock));
                 sqlListParam.Add(new SqlParameter("@Reorder", objInventory.ReOrder));
                 sqlListParam.Add(new SqlParameter("@RakID", objInventory.RakID));
                 sqlListParam.Add(new SqlParameter("@LastModifiedBy", objInventory.CreatedBy));
                 sqlListParam.Add(new SqlParameter("@aktif", objInventory.Aktif));
                 sqlListParam.Add(new SqlParameter("@stock", objInventory.Stock));
                 sqlListParam.Add(new SqlParameter("@LeadTime", objInventory.LeadTime));
                 int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateInventory1");
                 strError = dataAccess.Error;

                 return intResult;

             }
             catch (Exception ex)
             {
                 strError = ex.Message;
                 return -1;
             }
         }
        public override int Delete(object objDomain)
        {
            try
            {
                objInventory = (Inventory)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objInventory.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objInventory.LastModifiedBy));
                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteInventory");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateQty(object objDomain)
        {
            try
            {
                objInventory = (Inventory)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objInventory.ID));
                sqlListParam.Add(new SqlParameter("@Jumlah", objInventory.Jumlah));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateQtyInventory");
                strError = dataAccess.Error;
                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
         
        public int MinusQtyTransit(object objDomain)
        {

            try
            {
                objInventory = (Inventory)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objInventory.ID));
                sqlListParam.Add(new SqlParameter("@Jumlah", objInventory.Jumlah));

                int intResult = dataAccess.ProcessData(sqlListParam, "spMinusQtyTransitInventory");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int MinusQty(object objDomain)
        {

            try
            {
                objInventory = (Inventory)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objInventory.ID));
                sqlListParam.Add(new SqlParameter("@Jumlah", objInventory.Jumlah));

                int intResult = dataAccess.ProcessData(sqlListParam, "spMinusQtyInventory");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int MinusQtyForRepack(object objDomain)
        {

            try
            {
                objInventory = (Inventory)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", objInventory.ID));
                sqlListParam.Add(new SqlParameter("@Jumlah", objInventory.Jumlah));
                int intResult = dataAccess.ProcessData(sqlListParam, "spMinusQtyInventoryRePack");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int GetLastPrice(int itemID,int itemTypeID)
        {
            int lastPrice = 0;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "SELECT top 1 price from popurchndetail where status>-1 and itemID =" + itemID + " and itemtypeid=" + itemTypeID + " order by id desc";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString( strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    lastPrice = Convert.ToInt32(sqlDataReader["price"]);
                    return lastPrice;
                }
            }

            return 0;
        }
        public int IsHargaKertas(int itemID)
        {
            int lastPrice = 0;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select ISNULL(SUM(harga),0) HARGA from inventory where id=" + itemID ;
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    lastPrice = Convert.ToInt32(sqlDataReader["Harga"]);
                    return lastPrice;
                }
            }
            return 0;
        }
        public int GetHargaKertas(int itemID,int supID)
        {
            int lastPrice = 0;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select top 1 * from (SELECT B.ID,B.ItemID, B.Harga, B.SupplierID FROM  Inventory AS A INNER JOIN HargaKertas AS B "+
                " ON A.ID = B.ItemID) as AA where ItemID=" + itemID + " and SupplierID=" +  supID;
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    lastPrice = Convert.ToInt32(sqlDataReader["Harga"]);
                    return lastPrice;
                }
            }
            return 0;
        }
        private string Criteria()
        {
            string field = (HttpContext.Current.Session["Field"] != null) ? HttpContext.Current.Session["Field"].ToString() : string.Empty;

            return (field == string.Empty) ? " jumlah+jmltransit" : field;
        }
        public int GetStock(int itemID, int itemTypeID)
        {
            if (itemID != 0 && itemTypeID != 0)
            {
                try
                {
                    string strTable = string.Empty;
                    switch (itemTypeID)
                    {
                        case 1:
                            strTable = "inventory";
                            break;
                        case 2:
                            strTable = "asset";
                            break;
                        case 3:
                            strTable = "biaya";
                            break;
                    }
                    int lastPrice = 0;
                    string strSQL = "SELECT " + this.Criteria() + " as stock from " + strTable + "  where rowstatus>-1 and ID =" + itemID;
                    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
                    strError = dataAccess.Error;

                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            lastPrice = Convert.ToInt32(sqlDataReader["stock"]);
                            return lastPrice;
                        }
                    }
                }
                catch
                {
                    return 0;
                }
            }
            return 0;
        }

        public int UpdateQtyForRepack(object objDomain)
        {

            try
            {
                objInventory = (Inventory)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", objInventory.ID));
                sqlListParam.Add(new SqlParameter("@Jumlah", objInventory.Jumlah));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateQtyInventoryRePack");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int InsertForRePack(object objDomain)
        {

            try
            {
                objInventory = (Inventory)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", objInventory.ID));
                sqlListParam.Add(new SqlParameter("@Jumlah", objInventory.Jumlah));
                sqlListParam.Add(new SqlParameter("@UOMID", objInventory.UOMID));
                sqlListParam.Add(new SqlParameter("@RowStatus", objInventory.RowStatus));
                sqlListParam.Add(new SqlParameter("@GroupID", objInventory.GroupID));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertInventoryRePack");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

         public int UpdateMinMax(object objDomain)
        {

            try
            {
                objInventory = (Inventory)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objInventory.ID));
                sqlListParam.Add(new SqlParameter("@MinStock", objInventory.MinStock));
                sqlListParam.Add(new SqlParameter("@MaxStock", objInventory.MaxStock));
                sqlListParam.Add(new SqlParameter("@ReOrder", objInventory.ReOrder));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateQtyMinMax");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
         private string ctr = string.Empty;
         public string Criteriane { get { return ctr; } set { ctr = value; } }
        public override ArrayList Retrieve()
        {
            string strSQL = "select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc," +
                            "A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan," +
                            "A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID," +
                            "A.ItemTypeID,A.Aktif,A.Stock,A.LeadTime,A.Head from Inventory as A,UOM as C where A.UOMID = C.ID " +
                            this.Criteriane + " order by GroupID";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrInventory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrInventory.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrInventory.Add(new Inventory());

            return arrInventory;
        }

        public ArrayList Retrieve2()
        {
            string strSQL = "select top 50 A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,"+
                            "A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,"+
                            "A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,A.aktif,A.stock,A.LeadTime "+
                            "from Inventory as A,UOM as C where A.UOMID = C.ID order by ID desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrInventory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrInventory.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrInventory.Add(new Inventory());

            return arrInventory;
        }

        public ArrayList RetrieveForAsset()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,aktif from Asset as A, UOM as C where A.UOMID = C.ID order by GroupID");
            strError = dataAccess.Error;
            arrInventory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrInventory.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrInventory.Add(new Inventory());

            return arrInventory;
        }
        public ArrayList RetrieveForBiaya()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,aktif from Biaya as A,UOM as C where A.UOMID = C.ID order by GroupID");
            strError = dataAccess.Error;
            arrInventory = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrInventory.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrInventory.Add(new Inventory());
            return arrInventory;
        }

        public Inventory RepackRetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemID,A.UOMID,A.Jumlah,A.RowStatus,A.GroupID from InventoryRepack as A,UOM as C where A.UOMID = C.ID and A.ItemID =" + Id);
            strError = dataAccess.Error;
            arrInventory = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectRePack(sqlDataReader);
                }
            }
            return new Inventory();
        }
        public ArrayList RetrieveBarang(string itemtypeID)
        {
            string select = string.Empty;
            if (itemtypeID == "1")
                select = "SELECT  TOP (100) A.ID, A.ItemCode, A.ItemName, A.SupplierCode, A.UOMID, A.Jumlah, A.Harga, A.MinStock, A.DeptID, A.RakID, A.Gudang, A.ShortKey, A.Keterangan, A.Head, " +
                    "A.RowStatus, A.CreatedBy, A.CreatedTime, A.LastModifiedBy, A.LastModifiedTime, A.GroupID, A.ItemTypeID, A.Stock, A.Aktif, A.JmlTransit, A.MaxStock, A.ReOrder,  " +
                    "A.NAMA, A.ITYPE, A.UKURAN, A.MERK, A.JENIS, A.PARTNUM, A.Leadtime, A.BadStock, A.AlasanNonAktif, B.GroupDescription, C.UOMDesc " +
                    "FROM Inventory AS A INNER JOIN GroupsPurchn AS B ON A.GroupID = B.ID INNER JOIN UOM AS C ON A.UOMID = C.ID order by B.GroupDescription,A.itemname";
            else
                select = "SELECT  TOP (100) A.ID, A.ItemCode, A.ItemName, A.SupplierCode, A.UOMID, A.Jumlah, A.Harga, A.MinStock, A.DeptID, A.RakID, A.Gudang, A.ShortKey, A.Keterangan, A.Head, " +
                    "A.RowStatus, A.CreatedBy, A.CreatedTime, A.LastModifiedBy, A.LastModifiedTime, A.GroupID, A.ItemTypeID, A.Stock, A.Aktif, A.JmlTransit, A.MaxStock, A.ReOrder,  " +
                    "A.NAMA, A.ITYPE, A.UKURAN, A.MERK, A.JENIS, A.PARTNUM, A.Leadtime, A.BadStock, A.AlasanNonAktif, B.GroupDescription, C.UOMDesc " +
                    "FROM Asset AS A INNER JOIN GroupsPurchn AS B ON A.GroupID = B.ID INNER JOIN UOM AS C ON A.UOMID = C.ID order by B.GroupDescription,A.itemname";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(select);
            strError = dataAccess.Error;
            arrInventory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrInventory.Add(GenerateBarang(sqlDataReader));
                }
            }
            else
                arrInventory.Add(new Inventory());

            return arrInventory;
        }
        public ArrayList RetrieveUnApv(int UnitKerjaID)
        {
            string select = string.Empty; string query = string.Empty;
            if (UnitKerjaID == 7)
            {
                query = "";
            }
           
                select = 
                    "SELECT  TOP (100) A.ID, A.ItemCode, A.ItemName, A.SupplierCode, A.UOMID, A.Jumlah, A.Harga, A.MinStock, A.DeptID, A.RakID, A.Gudang, A.ShortKey, A.Keterangan, A.Head, " +
                    "A.RowStatus, A.CreatedBy, A.CreatedTime, A.LastModifiedBy, A.LastModifiedTime, A.GroupID, A.ItemTypeID, A.Stock, A.Aktif, A.JmlTransit, A.MaxStock, A.ReOrder,  " +
                    "A.NAMA, A.ITYPE, A.UKURAN, A.MERK, A.JENIS, A.PARTNUM, A.Leadtime, A.BadStock, A.AlasanNonAktif, B.GroupDescription, C.UOMDesc " +
                    "FROM Inventory AS A INNER JOIN GroupsPurchn AS B ON A.GroupID = B.ID INNER JOIN UOM AS C ON A.UOMID = C.ID order by B.GroupDescription,A.itemname";
           
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(select);
            strError = dataAccess.Error;
            arrInventory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrInventory.Add(GenerateBarang(sqlDataReader));
                }
            }
            else
                arrInventory.Add(new Inventory());

            return arrInventory;
        }
        public ArrayList RetrieveBarangByNama(string itemtypeID,string nama)
        {
            string select = string.Empty;
            if (itemtypeID == "1")
            {
                select = "SELECT   A.ID, A.ItemCode, A.ItemName, A.SupplierCode, A.UOMID, A.Jumlah, A.Harga, A.MinStock, A.DeptID, A.RakID, A.Gudang, A.ShortKey, A.Keterangan, A.Head, " +
                    "A.RowStatus, A.CreatedBy, A.CreatedTime, A.LastModifiedBy, A.LastModifiedTime, A.GroupID, A.ItemTypeID, A.Stock, A.Aktif, A.JmlTransit, A.MaxStock, A.ReOrder,  " +
                    "A.NAMA, A.ITYPE, A.UKURAN, A.MERK, A.JENIS, A.PARTNUM, A.Leadtime, A.BadStock, A.AlasanNonAktif, B.GroupDescription, C.UOMDesc " +
                    "FROM Inventory AS A INNER JOIN GroupsPurchn AS B ON A.GroupID = B.ID INNER JOIN UOM AS C ON A.UOMID = C.ID where A.itemname like '%" + nama + "%' order by B.GroupDescription,A.itemname";
            }
            else if (itemtypeID == "2")
            {
                select = "SELECT   A.ID, A.ItemCode, A.ItemName, A.SupplierCode, A.UOMID, A.Jumlah, A.Harga, A.MinStock, A.DeptID, A.RakID, A.Gudang, A.ShortKey, A.Keterangan, A.Head, " +
                    "A.RowStatus, A.CreatedBy, A.CreatedTime, A.LastModifiedBy, A.LastModifiedTime, A.GroupID, A.ItemTypeID, A.Stock, A.Aktif, A.JmlTransit, A.MaxStock, A.ReOrder,  " +
                    "A.NAMA, A.ITYPE, A.UKURAN, A.MERK, A.JENIS, A.PARTNUM, A.Leadtime, A.BadStock, A.AlasanNonAktif, B.GroupDescription, C.UOMDesc " +
                    "FROM Asset AS A INNER JOIN GroupsPurchn AS B ON A.GroupID = B.ID INNER JOIN UOM AS C ON A.UOMID = C.ID  where A.itemname like '%" + nama + "%' order by B.GroupDescription,A.itemname";
            }
            else if (itemtypeID == "3")
            {
                select = "SELECT  A.ID, A.ItemCode, A.ItemName, A.SupplierCode, A.UOMID, A.Jumlah, A.Harga, A.MinStock, A.DeptID, A.RakID, A.Gudang, A.ShortKey, A.Keterangan, A.Head, " +
                    "A.RowStatus, A.CreatedBy, A.CreatedTime, A.LastModifiedBy, A.LastModifiedTime, A.GroupID, A.ItemTypeID, A.Stock, A.Aktif, A.JmlTransit, A.MaxStock, A.ReOrder,  " +
                    "A.NAMA, A.ITYPE, A.UKURAN, A.MERK, A.JENIS, A.PARTNUM, A.Leadtime, A.BadStock, A.AlasanNonAktif, B.GroupDescription, C.UOMDesc " +
                    "FROM Biaya AS A INNER JOIN GroupsPurchn AS B ON A.GroupID = B.ID INNER JOIN UOM AS C ON A.UOMID = C.ID  where A.itemname like '%" + nama + "%' order by B.GroupDescription,A.itemname";

            }
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(select);
            strError = dataAccess.Error;
            arrInventory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrInventory.Add(GenerateBarang(sqlDataReader));
                }
            }
            else
                arrInventory.Add(new Inventory());

            return arrInventory;
        }
        public Inventory RetrieveById(int Id)
        {
            string strSQL = "select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMCode,C.UOMDesc,A.Jumlah,"+
                            "A.Harga,A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,"+
                            "A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,A.aktif,"+
                            "isnull(A.LeadTime,0) as LeadTime,A.Stock   from Inventory as A,UOM as C where A.UOMID = C.ID and " +
                            "A.ID = " + Id;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrInventory = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new Inventory();
        }
        
         public Inventory RetrieveByIdNew(int Id,int itemtype)
        {
             string strSQL=string.Empty;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            if (itemtype == 1)
            {
                strSQL = "select A.ID, A.ItemCode, A.ItemName, A.SupplierCode, A.UOMID, A.Jumlah, A.Harga, A.MinStock, A.DeptID, A.RakID, A.Gudang, A.ShortKey, A.Keterangan, A.Head, A.RowStatus, A.CreatedBy," +
                          "A.CreatedTime, A.LastModifiedBy, A.LastModifiedTime, A.GroupID, A.ItemTypeID, A.Stock, A.Aktif, A.JmlTransit, A.MaxStock, A.ReOrder, A.Nama, A.IType, A.Ukuran, A.Merk, A.Jenis," +
                          "A.PartNum, isnull(A.LeadTime ,0) as LeadTime,A.alasannonaktif,C.UOMCode,C.UOMCode,C.UOMDesc from Inventory as A,UOM as C where A.UOMID = C.ID and A.ID = " + Id;
            }
            else if (itemtype == 2)
            {
                strSQL = "select A.ID, A.ItemCode, A.ItemName, A.SupplierCode, A.UOMID, A.Jumlah, A.Harga, A.MinStock, A.DeptID, A.RakID, A.Gudang, A.ShortKey, A.Keterangan, A.Head, A.RowStatus, A.CreatedBy," +
                          "A.CreatedTime, A.LastModifiedBy, A.LastModifiedTime, A.GroupID, A.ItemTypeID, A.Stock, A.Aktif, A.JmlTransit, A.MaxStock, A.ReOrder, A.Nama, A.IType, A.Ukuran, A.Merk, A.Jenis," +
                          "A.PartNum, isnull(A.LeadTime ,0) as LeadTime,A.alasannonaktif,C.UOMCode,C.UOMCode,C.UOMDesc from Asset as A,UOM as C where A.UOMID = C.ID and A.ID = " + Id;

            }
            else if (itemtype == 3)
            {
                strSQL = "select A.ID, A.ItemCode, A.ItemName, A.SupplierCode, A.UOMID, A.Jumlah, A.Harga, A.MinStock, A.DeptID, A.RakID, A.Gudang, A.ShortKey, A.Keterangan, A.Head, A.RowStatus, A.CreatedBy," +
                          "A.CreatedTime, A.LastModifiedBy, A.LastModifiedTime, A.GroupID, A.ItemTypeID, A.Stock, A.Aktif, A.JmlTransit, A.MaxStock, A.ReOrder, A.Nama, A.IType, A.Ukuran, A.Merk, A.Jenis," +
                          "A.PartNum, isnull(A.LeadTime ,0) as LeadTime,A.alasannonaktif,C.UOMCode,C.UOMCode,C.UOMDesc from Biaya as A,UOM as C where A.UOMID = C.ID and A.ID = " + Id;

            }
             SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrInventory = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectnew(sqlDataReader);
                }
            }
            return new Inventory();
        }

        public Inventory RetrieveByCode(string strItemCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.ReOrder,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,A.Aktif from Inventory as A,UOM as C where A.UOMID = C.ID and A.ItemCode = '" + strItemCode + "'");
            strError = dataAccess.Error;
            arrInventory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }
            return new Inventory();
        }
        public Inventory RetrieveByCode2(string strItemCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMCode,C.UOMDesc," +
                "A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.ReOrder,A.DeptID,A.RakID,A.Gudang,A.ShortKey," +
                "A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime," +
                "A.GroupID,A.ItemTypeID,A.Aktif from Inventory as A,UOM as C where A.UOMID = C.ID and " +
                "A.ItemCode = '" + strItemCode +
                "' union " +
            "select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMCode,C.UOMDesc," +
                "A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.ReOrder,A.DeptID,A.RakID,A.Gudang,A.ShortKey," +
                "A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime," +
                "A.GroupID,A.ItemTypeID,A.Aktif from asset as A,UOM as C where A.UOMID = C.ID and " +
                "A.ItemCode = '" + strItemCode +
                "' union " +
            "select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMCode,C.UOMDesc," +
                "A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.ReOrder,A.DeptID,A.RakID,A.Gudang,A.ShortKey," +
                "A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime," +
                "A.GroupID,A.ItemTypeID,A.Aktif from Biaya as A,UOM as C where A.UOMID = C.ID and " +
                "A.ItemCode = '" + strItemCode + "'";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrInventory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }
            return new Inventory();
        }

        public Inventory RetrieveById2(int Id)
        {
            string strSQL = "select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMCode,C.UOMDesc,A.Jumlah,"+
                            "A.Harga,A.MinStock,A.MaxStock,A.ReOrder,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,"+
                            "A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,"+
                            "A.Aktif from Inventory as A,UOM as C where A.UOMID = C.ID and A.ID = " + Id;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.DeptID,B.DeptName,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID from Inventory as A,Dept as B,UOM as C where A.DeptID = B.ID and A.UOMID = C.ID and A.ID = " + Id);
            //blom ada tabel dept
            strError = dataAccess.Error;
            arrInventory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }
            return new Inventory();
        }
        public Inventory BiayaRetrieveById(int Id)
        {
            string strSQL = "select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,"+
                            "A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,"+
                            "A.GroupID,A.ItemTypeID,A.Aktif,A.Stock,A.LeadTime from Biaya as A,UOM as C where A.UOMID = C.ID and A.ID = " + Id;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrInventory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new Inventory();
        }

        public Inventory AssetRetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,A.Aktif,A.Stock,A.LeadTime from Asset as A,UOM as C where A.UOMID = C.ID and A.ID = " + Id);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.DeptID,B.DeptName,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID from Inventory as A,Dept as B,UOM as C where A.DeptID = B.ID and A.UOMID = C.ID and A.ID = " + Id);
            //blom ada tabel dept
            strError = dataAccess.Error;
            arrInventory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new Inventory();
        }

        public Inventory RetrieveByName(string strDescription)
        {
            string strSQL = "select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,"+
                            "A.MaxStock,A.DeptID,B.DeptName,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,"+
                            "A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,A.Aktif,A.Stock,A.LeadTime from Inventory as A,Dept as B,UOM as C " +
                            "where A.DeptID = B.ID and A.UOMID = C.ID and A.ItemName = '" + strDescription + "' and Aktif=1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrInventory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new Inventory();
        }

        public ArrayList RetrieveByGroupIDForBakuBantu(int groupId, int itemType)
        {
            string strGroupID = " and A.GroupID in (1,2) ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,A.Aktif,A.Stock,A.LeadTime from Inventory as A,GroupsPurchn as B, UOM as C where A.GroupID = B.ID and A.UOMID = C.ID and A.RowStatus = 0 " + strGroupID + " and A.ItemTypeID = " + itemType + " order by A.ItemCode");
            strError = dataAccess.Error;
            arrInventory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrInventory.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrInventory.Add(new Inventory());
            return arrInventory;
        }

        public ArrayList RetrieveByItemIDBaru(int groupId, int itemType, string yearperiod)
        {
            string strJenisBrg = string.Empty;

            if (groupId == 5)
            {
                strJenisBrg = "Biaya";
            }
            else
            {
                if (groupId == 4 || groupId == 12)
                {
                    strJenisBrg = "Asset";
                }
                else
                {
                    strJenisBrg = "Inventory";
                }
            }
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "select Inv.ID,isnull(SaldoInventory.ItemID,0) as ItemID, UOMID,Jumlah,inv.GroupID,RowStatus " +
                "from (select ID,UOMID,GroupID,Jumlah,RowStatus from " + strJenisBrg + " where ItemTypeID=" + itemType + 
                " and GroupID=" + groupId + " and RowStatus>-1) as Inv " +
                "left join SaldoInventory on SaldoInventory.ItemID=Inv.ID and SaldoInventory.ItemTypeID=" + itemType + 
                " and SaldoInventory.GroupID=" + groupId + " and SaldoInventory.yearperiod=" + yearperiod + " where ItemID is null";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql );
            strError = dataAccess.Error;
            arrInventory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrInventory.Add(GenerateObjectRePack(sqlDataReader));
                }
            }
            else
                arrInventory.Add(new Inventory());
            return arrInventory;
        }
       
       public ArrayList RetrieveByGroupID(string groupId, int itemType)
        {
           string strSQL="select "+this.Limit+" A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,"+
                         "A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,ISNULL(A.Head,0)Head,A.RowStatus,A.CreatedBy,A.CreatedTime, "+
                         "A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,aktif,A.Stock,A.LeadTime from Inventory as A,GroupsPurchn as B, UOM as C " +
                         "where A.GroupID = B.ID and A.UOMID = C.ID and A.RowStatus = 0 and A.GroupID in( " + groupId + ")"+
                         " and A.ItemTypeID = " + itemType + this.Criteriane+ " order by A.ItemCode";
           DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrInventory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrInventory.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrInventory.Add(new Inventory());
            return arrInventory;
        }
       public ArrayList RetrieveByGroupID(string groupId, int itemType,bool type)
       {
           string strSQL = "select " + this.Limit + " A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock," +
                         "A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,ISNULL(A.Head,0)Head,A.RowStatus,A.CreatedBy,A.CreatedTime, " +
                         "A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,aktif,A.Stock,A.LeadTime from Inventory as A,GroupsPurchn as B, UOM as C " +
                         "where A.GroupID = B.ID and A.UOMID = C.ID and A.RowStatus = 0 and A.GroupID in( " + groupId + ")" +
                         " and A.ItemTypeID = " + itemType + this.Criteriane + " order by A.ItemCode";
           DataAccess dataAccess = new DataAccess(Global.ConnectionString());
           SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
           strError = dataAccess.Error;
           arrInventory = new ArrayList();

           if (sqlDataReader.HasRows)
           {
               while (sqlDataReader.Read())
               {
                   arrInventory.Add(GenerateObject(sqlDataReader));
               }
           }
           else
               arrInventory.Add(new Inventory());
           return arrInventory;
       }
       public ArrayList RetrieveByGroupIDForAsset(int groupId, int itemType)
       {
           DataAccess dataAccess = new DataAccess(Global.ConnectionString());
           SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,aktif,A.Stock,A.LeadTime from Asset as A,GroupsPurchn as B, UOM as C where A.GroupID = B.ID and A.UOMID = C.ID and A.RowStatus = 0 and A.GroupID = " + groupId + " and A.ItemTypeID = " + itemType + " order by A.ItemCode");
           strError = dataAccess.Error;
           arrInventory = new ArrayList();

           if (sqlDataReader.HasRows)
           {
               while (sqlDataReader.Read())
               {
                   arrInventory.Add(GenerateObject(sqlDataReader));
               }
           }
           else
               arrInventory.Add(new Inventory());
           return arrInventory;
       }
       public ArrayList RetrieveByGroupIDForBiaya(int groupId, int itemType)
       {
           DataAccess dataAccess = new DataAccess(Global.ConnectionString());
           SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,aktif,A.Stock,A.LeadTime from Biaya as A,GroupsPurchn as B, UOM as C where A.GroupID = B.ID and A.UOMID = C.ID and A.RowStatus = 0 and A.GroupID = " + groupId + " and A.ItemTypeID = " + itemType + " order by A.ItemCode");
           strError = dataAccess.Error;
           arrInventory = new ArrayList();

           if (sqlDataReader.HasRows)
           {
               while (sqlDataReader.Read())
               {
                   arrInventory.Add(GenerateObject(sqlDataReader));
               }
           }
           else
               arrInventory.Add(new Inventory());

           return arrInventory;
       }

       public ArrayList RetrieveByCriteriaWithGroupID2(string strField, string strValue, int groupID)
       {
           string strGroupID = string.Empty;
           //if (groupID > 0)
           //    strGroupID = " and A.GroupID =" + groupID;

           if (groupID == 1 || groupID == 2)
           {
               strGroupID = " and A.GroupID in (1,2)";
           }
           if (groupID == 3)
           {
               strGroupID = " and A.GroupID in (3)";
           }
           if (groupID == 4)
           {
               strGroupID = " and A.GroupID in (4)";
           }
           if (groupID == 5)
           {
               strGroupID = " and A.GroupID in (5)";
           }
           if (groupID == 6)
           {
               strGroupID = " and A.GroupID in (6)";
           }
           if (groupID ==7)
           {
               strGroupID = " and A.GroupID in (7)";
           }
           if (groupID == 8 || groupID == 9)
           {
               strGroupID = " and A.GroupID in (8,9)";
           }
           if (groupID == 10)
           {
               strGroupID = " and A.GroupID in (10)";
           }
           if (groupID == 12)
           {
               strGroupID = " and A.GroupID in (12)";
           }

           string strSQL = "select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,"+
                           "A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,"+
                           "A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,aktif,A.Stock,A.LeadTime from Inventory as A,UOM as C " +
                           "where A.RowStatus>-1 and aktif =1  and A.UOMID = C.ID and " + strField + " like '%" + strValue + "%' " + strGroupID;
           DataAccess dataAccess = new DataAccess(Global.ConnectionString());
           SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
           strError = dataAccess.Error;
           arrInventory = new ArrayList();

           if (sqlDataReader.HasRows)
           {
               while (sqlDataReader.Read())
               {
                   arrInventory.Add(GenerateObject(sqlDataReader));
               }
           }
           else
               arrInventory.Add(new Inventory());

           return arrInventory;
       }

       public ArrayList RetrieveByCriteriaForRePack(string strField, string strValue, string Flag)
       {
           string strGroupID = string.Empty; 
          
           //strGroupID = " and A.GroupID in (7)";

           strGroupID = Flag;

           DataAccess dataAccess = new DataAccess(Global.ConnectionString());
           SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,aktif,A.Stock,A.LeadTime from Inventory as A,UOM as C where A.RowStatus>-1 and A.UOMID = C.ID and " + strField + " like '%" + strValue + "%' " + strGroupID);
           strError = dataAccess.Error;
           arrInventory = new ArrayList();

           if (sqlDataReader.HasRows)
           {
               while (sqlDataReader.Read())
               {
                   arrInventory.Add(GenerateObject(sqlDataReader));
               }
           }
           else
               arrInventory.Add(new Inventory());
           return arrInventory;
       }
       public ArrayList RetrieveByCriteriaWithGroupID(string strField, string strValue, int groupID)
       {
           string strGroupID = string.Empty;
           string strGroupID1 = string.Empty;
           string strMarketing = string.Empty;
           if (groupID > 0)
           {
               if (groupID == 7)
               {
                   strGroupID = " and A.GroupID =10 ";
                   strGroupID1 = "and (A.GroupID=7 OR A.ID in (select ItemID from ItemsOther where GroupID=7 and RowStatus > -1))";
               }
               else
               {
                   strGroupID = " and A.GroupID =" + groupID;
               }
           }

           strMarketing = (groupID == 7) ?
                "Union All " +
                   "select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock," +
                   "A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime," +
                   "A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,aktif,A.Stock,A.LeadTime from Inventory as A,UOM as C where A.aktif =1 " +
                   "and A.RowStatus>-1 and A.UOMID = C.ID and " + strField + " like '%" + strValue + "%' " + strGroupID1 : "";

           DataAccess dataAccess = new DataAccess(Global.ConnectionString());

           string strSQL;
           
               strSQL =(groupID!=6)? "select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock," +
                   "A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime," +
                   "A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,aktif,A.Stock,A.LeadTime from Inventory as A,UOM as C where A.aktif =1 " +
                   "and A.RowStatus>-1 and A.UOMID = C.ID and " + strField + " like '%" + strValue + "%' " + strGroupID + strMarketing +
                   " order by A.ItemName,A.ItemCode" :

                   "Select * from ( "+
                   "select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock," +
                   "A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime," +
                   "A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,aktif,A.Stock,A.LeadTime from Inventory as A,UOM as C where A.aktif =1 " +
                   "and A.RowStatus>-1 and A.UOMID = C.ID and " + strField + " like '%" + strValue + "%' and GroupID in(8,9) "+
                   "Union All "+
                   "(select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock," +
                   "A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime," +
                   "A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,aktif,A.Stock,A.LeadTime from Inventory as A,UOM as C where A.aktif =1 " +
                   "and A.RowStatus>-1 and A.UOMID = C.ID and " + strField + " like '%" + strValue + "%' and GroupID=6 And A.Jumlah >0)) as w "+
                   "order by w.itemname ";
           
           SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
           strError = dataAccess.Error;
           arrInventory = new ArrayList();

           if (sqlDataReader.HasRows)
           {
               while (sqlDataReader.Read())
               {
                   arrInventory.Add(GenerateObject(sqlDataReader));
               }
           }

           return arrInventory;
       }

       public ArrayList RetrieveByCriteriaWithGroupID1(string strField, string strValue, int groupID)
       {
           string strGroupID = string.Empty;
           if (groupID > 0)
           {
               if (groupID == 7)
               {
                   strGroupID = " and A.GroupID in (7,10)";
               }
               else
               {
                   strGroupID = " and A.GroupID =" + groupID;
               }
           }

           //if (groupID>0)
           //    strGroupID = " and A.GroupID ="+ groupID;

           //if (groupID == 3)
           //{
           //    strGroupID = " and A.GroupID in (3)";
           //}
           //if (groupID == 4)
           //{
           //    strGroupID = " and A.GroupID in (4)";
           //}
           //if (groupID == 5)
           //{
           //    strGroupID = " and A.GroupID in (5)";
           //}
           //if (groupID == 6 || groupID == 7 || groupID == 8 || groupID == 9)
           //{
           //    strGroupID = " and A.GroupID in (6,7,8,9)";
           //}
           //if (groupID == 10)
           //{
           //    strGroupID = " and A.GroupID in (10)";
           //}


           DataAccess dataAccess = new DataAccess(Global.ConnectionString());
           //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.DeptID,B.DeptName,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID from Inventory as A,Dept as B,UOM as C where A.DeptID = B.ID and A.UOMID = C.ID and " + strField + " like '%" + strValue + "%' "+ strGroupID);

           string strSQL;

           strSQL = "select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock," +
               "A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime," +
               "A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,aktif,A.Stock,A.LeadTime from Inventory as A,UOM as C where " +
               "A.RowStatus>-1 and A.UOMID = C.ID and " + strField + " like '%" + strValue + "%' " + strGroupID + " order by A.itemname ";

           SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
           strError = dataAccess.Error;
           arrInventory = new ArrayList();

           if (sqlDataReader.HasRows)
           {
               while (sqlDataReader.Read())
               {
                   arrInventory.Add(GenerateObject(sqlDataReader));
               }
           }
           else
               arrInventory.Add(new Inventory());

           return arrInventory;
       }
       public ArrayList RetrieveByReceipt(string ReceiptNo, int itemTypeID)
       {
           string strTable = string.Empty;
           if (itemTypeID == 1)
               strTable = "Inventory";
           if (itemTypeID == 2)
               strTable = "Asset";
           if (itemTypeID == 3)
               strTable = "Biaya";

           string strGroupID = string.Empty;
           DataAccess dataAccess = new DataAccess(Global.ConnectionString());
           string strSQL;
           strSQL = "SELECT A.ID, A.ItemCode, A.ItemName, A.SupplierCode, A.UOMID, A.Jumlah, A.Harga, A.MinStock, A.DeptID, A.RakID, A.Gudang, A.ShortKey, A.Keterangan,  " +
                "A.Head, A.RowStatus, A.CreatedBy, A.CreatedTime, A.LastModifiedBy, A.LastModifiedTime, A.GroupID, A.ItemTypeID, A.Stock, A.Aktif, A.JmlTransit,A.LeadTime,   " +
                "A.MaxStock, A.ReOrder, C.UOMCode, C.UOMDesc  " +
                "FROM Receipt INNER JOIN ReceiptDetail ON Receipt.ID = ReceiptDetail.ReceiptID INNER JOIN " + strTable + " AS A ON ReceiptDetail.ItemID = A.ID INNER JOIN UOM AS C ON A.UOMID = C.ID " +
                "where Receipt.receiptno='" + ReceiptNo + "'";

           SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
           strError = dataAccess.Error;
           arrInventory = new ArrayList();

           if (sqlDataReader.HasRows)
           {
               while (sqlDataReader.Read())
               {
                   arrInventory.Add(GenerateObject(sqlDataReader));
               }
           }
           else
               arrInventory.Add(new Inventory());

           return arrInventory;
       }

       public ArrayList RetrieveByCriteriaWithGroupIDROP(string strField, string strValue, int groupID,int userid)
       {
           string strGroupID = string.Empty;
           if (groupID > 0)
           {
               if (groupID == 7)
               {
                   strGroupID = " and A.GroupID in (7,10)";
               }
               else if (groupID == 6)
               {
                   strGroupID = " and A.GroupID in(6)";
               }
               else
               {
                   strGroupID = " and A.GroupID =" + groupID+" and A.GroupID not in(6)";
               }
           }

           //if (groupID>0)
           //    strGroupID = " and A.GroupID ="+ groupID;
           int rop = 0;
           if (groupID == 1 || groupID == 2 || groupID == 6 || groupID == 8 || groupID == 9)
           {
              
               rop = (CheckDeptID(userid)==10)?CheckReorderPointSPP(groupID.ToString(),userid ):0;
           }
           #region
           //if (groupID == 3)
           //{
           //    strGroupID = " and A.GroupID in (3)";
           //}
           //if (groupID == 4)
           //{
           //    strGroupID = " and A.GroupID in (4)";
           //}
           //if (groupID == 5)
           //{
           //    strGroupID = " and A.GroupID in (5)";
           //}
           //if (groupID == 6 || groupID == 7 || groupID == 8 || groupID == 9)
           //{
           //    strGroupID = " and A.GroupID in (6,7,8,9)";
           //}
           //if (groupID == 10)
           //{
           //    strGroupID = " and A.GroupID in (10)";
           //}
           #endregion

           DataAccess dataAccess = new DataAccess(Global.ConnectionString());
           //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.DeptID,B.DeptName,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID from Inventory as A,Dept as B,UOM as C where A.DeptID = B.ID and A.UOMID = C.ID and " + strField + " like '%" + strValue + "%' "+ strGroupID);
          
           string strSQL;
           if (rop == 0)
           {
               strSQL = "select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,isnull((select SUM(quantity) from vw_StockPurchn where ItemID =A.ID),0)Jumlah,A.Harga,A.MinStock," +
                   "A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,"+
                   "A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,aktif,A.Stock,A.LeadTime from Inventory as A,UOM as C where A.aktif =1 " +
                   "and A.RowStatus>-1 and A.UOMID = C.ID and " + strField + " like '%" + strValue + "%' " + strGroupID +" order by A.itemname ";
           }
           else
           {
               strSQL=" select * from ( "+
                "select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc, "+
                "/**isnull((select SUM(quantity) from vw_StockPurchn where ItemID =A.ID),0)Jumlah**/ "+
                "A.Jumlah,A.Harga, A.MinStock," +
                "A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus, A.CreatedBy,A.CreatedTime,"+
                "A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,A.aktif ,A.ReOrder,  " +
                "B.SPPQty,B.status,B.userid,A.Stock,A.LeadTime  FROM Inventory A left JOIN ROP B ON A.ID = B.ItemID inner join UOM as C " +
                "on A.UOMID = C.ID where (A.Jumlah <= A.ReOrder) AND (A.ReOrder > 0) AND (A.GroupID = " + groupID + 
                ") and ISNULL(status,0)=0) as tblreorder  " +
                "where ISNULL(sppqty,0)+Jumlah<=reorder  order by tblreorder.itemname ";
           }

           SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
           strError = dataAccess.Error;
           arrInventory = new ArrayList();

           if (sqlDataReader.HasRows)
           {
               while (sqlDataReader.Read())
               {
                   arrInventory.Add(GenerateObject(sqlDataReader));
               }
           }
           else
               arrInventory.Add(new Inventory());

           return arrInventory;
       }
//here
       public ArrayList RepackRetrieveByCriteria(string strField, string strValue)
       {
           DataAccess dataAccess = new DataAccess(Global.ConnectionString());
           SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemID,A.UOMID,A.Jumlah,A.RowStatus,A.GroupID from InventoryRepack as A,UOM as C where A.UOMID = C.ID and " + strField + " like '%" + strValue + "%'");
           strError = dataAccess.Error;
           arrInventory = new ArrayList();

           if (sqlDataReader.HasRows)
           {
               while (sqlDataReader.Read())
               {
                   arrInventory.Add(GenerateObjectRePack(sqlDataReader));
               }
           }
           else
               arrInventory.Add(new Inventory());
           return arrInventory;
       }

       public int CheckReorderPoint(string groupID,int userID)
       {
           DataAccess dataAccess = new DataAccess(Global.ConnectionString());
           string strSQL = "select * from (SELECT A.*, B.SPPQty,B.status,B.userid  FROM Inventory A left JOIN ROP B ON A.ID = B.ItemID " +
                "where (A.Jumlah <= A.ReOrder) AND (A.ReOrder > 0) AND Aktif=1 AND (A.GroupID in (" + groupID + ")) and ISNULL(status,0)=0) as tblreorder " +
                "where ISNULL(sppqty,0)+Jumlah<=reorder ";
           SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
           strError = dataAccess.Error;
           int CheckROP = 0;

           if (sqlDataReader.HasRows)
           {
               CheckROP = 1;
               #region
               //for automatic create SPP
               //CheckROP = 0;
               //ArrayList arrSPPDetail = new ArrayList();
               //int ItemTypeID = 0;
               //while (sqlDataReader.Read())
               //{
               //    SPPDetail sPPDetail = new SPPDetail();
               //    sPPDetail.ItemID = Convert.ToInt32(sqlDataReader["ID"]);
               //    sPPDetail.GroupID = Convert.ToInt32(groupID);
               //    sPPDetail.Quantity = Convert.ToDecimal(sqlDataReader["maxstock"]) - Convert.ToDecimal(sqlDataReader["jumlah"]);
               //    sPPDetail.ItemTypeID = Convert.ToInt32(sqlDataReader["itemtypeID"]);
               //    ItemTypeID = Convert.ToInt32(sqlDataReader["itemtypeID"]);
               //    sPPDetail.UOMID = Convert.ToInt32(sqlDataReader["UOMID"]); 
               //    sPPDetail.ItemCode = sqlDataReader["ItemCode"].ToString(); 
               //    sPPDetail.ItemName = sqlDataReader["Itemname"].ToString();
               //    sPPDetail.Satuan = " ";
               //    sPPDetail.Status = 0;
               //    sPPDetail.QtyPO = 0;
               //    sPPDetail.Keterangan = "Automatic by system ROP";
               //    arrSPPDetail.Add(sPPDetail);
               //}
               //SPPFacade sPPFacade = new SPPFacade();
               
               //UsersFacade usersFacade = new UsersFacade();
               //Users users = new Users();
               //users = usersFacade.RetrieveById(userID);

               //int depoid = users.UnitKerjaID;
               //Company company = new Company();
               //CompanyFacade companyFacade = new CompanyFacade();
               //GroupsPurchn groupsPurchn = new GroupsPurchn();
               //GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
               //string kd = companyFacade.GetKodeCompany(depoid) + groupsPurchnFacade.GetKodeSPP(Convert.ToInt32(groupID));
               //SPP sPP = new SPP();
               //UsersHeadFacade usersHeadFacade = new UsersHeadFacade();
               //UsersHead usersHead = usersHeadFacade.RetrieveByUserID(users.ID.ToString());
               //sPP.CreatedTime = DateTime.Now;
               //sPP.ItemTypeID = ItemTypeID;
               //sPP.GroupID = Convert.ToInt32(groupID);
               //sPP.NoSPP = " ";
               //sPP.Minta = DateTime.Now.AddDays(14);
               //sPP.SatuanID = 0;
               //sPP.JumlahSisa = 0;
               //sPP.Sudah = 0;
               //sPP.FCetak = 0;
               ////SPP.Keterangan = txtKeterangan.Text;
               //sPP.UserID = users.ID;
               //sPP.Pending = 0;
               //sPP.Inden = 0;
               //sPP.AlasanBatal = string.Empty;
               //sPP.AlasanCLS = string.Empty;
               //sPP.Status = 0;
               //sPP.CreatedBy = users.UserName;
               //sPP.CreatedTime = DateTime.Now;
               //sPP.LastModifiedBy = users.UserName;
               //sPP.LastModifiedTime = DateTime.Now;
               //sPP.TglBuat = DateTime.Now;
               //sPP.PermintaanType = 3;
               //sPP.DepoID = users.UnitKerjaID;
               //SPPNumberFacade sPPNumberFacade = new SPPNumberFacade();
               //SPPNumber sPPNumber = new SPPNumber();
               //sPPNumber = sPPNumberFacade.RetrieveByGroupsID(Convert.ToInt32(groupID));
               //if (sPPNumberFacade.Error == string.Empty)
               //{
               //    if (sPPNumber.ID > 0)
               //    {
               //        sPPNumber.SPPCounter = sPPNumber.SPPCounter + 1;
               //        sPPNumber.KodeCompany = kd.Substring(0, 1);
               //        sPPNumber.KodeSPP = kd.Substring(1, 1);
               //        sPPNumber.LastModifiedBy = users.UserName;
               //    }
               //}
               //SPPProcessFacade sPPProsessFacade = new SPPProcessFacade(sPP, arrSPPDetail, sPPNumber);
               //strError = sPPProsessFacade.Insert();
               //end of automatic create SPP
               #endregion
           }
           else
               CheckROP = 0;
           return CheckROP;
       }

       public int CheckReorderPointSPP(string groupID,int userID)
       {
           DataAccess dataAccess = new DataAccess(Global.ConnectionString());
           string strSQL = "select * from (SELECT A.*, B.SPPQty,B.status,B.userid  FROM Inventory A left JOIN ROP B ON A.ID = B.ItemID " +
                "where (A.Jumlah <= A.ReOrder) AND (A.ReOrder > 0) AND A.Aktif=1 AND (A.GroupID in (" + groupID + ")) /*and ISNULL(status,0)=0*/) as tblreorder " +
                "where ISNULL(sppqty,0)+Jumlah<=reorder and userid=" + userID ;

           SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
           strError = dataAccess.Error;
           int CheckROP = 0;

           if (sqlDataReader.HasRows)
               CheckROP = 1;
           else
               CheckROP = 0;

           return CheckROP;
       }

       public ArrayList ListReorderPoint(int groupID)
       {
           DataAccess dataAccess = new DataAccess(Global.ConnectionString());
           string strSQL = "select * from (SELECT A.*, B.SPPQty,B.status,B.userid  FROM Inventory A left JOIN ROP B ON A.ID = B.ItemID " +
                "where (A.Jumlah <= A.ReOrder) AND (A.ReOrder > 0) AND (A.GroupID = " + groupID + ") and ISNULL(status,0)=0) as tblreorder " +
                "where ISNULL(sppqty,0)+Jumlah<=reorder ";

           SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
           strError = dataAccess.Error;
           arrInventory = new ArrayList();
           if (sqlDataReader.HasRows)
               while (sqlDataReader.Read())
               {
                   arrInventory.Add(GenerateObjectROP(sqlDataReader));
               }
           else
               arrInventory.Add(new Inventory());

           return  arrInventory;
       }
       private string dept = string.Empty;
       public string Dept { get { return dept; } set { dept = value; } }
       public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            string[] arrDpt = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ShowNonAktif", "MasterInventory").Split(',');
            string dept = (arrDpt.Contains(this.Dept)) ? "and len(ItemCode)=15" : " and Aktif=1";
            string strSQL = "select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock, " +
                            "A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime, " +
                            "A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,A.Aktif,A.Stock,A.LeadTime from Inventory as A,UOM as C where A.RowStatus>-1  " +
                            "and A.UOMID = C.ID  and " + strField + " like '%" + strValue + "%'" + dept +
                            " Order by A.ItemCode";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrInventory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrInventory.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrInventory.Add(new Inventory());

            return arrInventory;
        }

       public ArrayList AssetRetrieveByCriteria(string strField, string strValue)
       {
           string strSQL = "select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga," +
                         "A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy," +
                         "A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,aktif,A.Stock,A.LeadTime from Asset as A,UOM as C " +
                         "where A.RowStatus > -1 and A.UOMID = C.ID and " + strField + " like '%" + strValue + "%' and aktif=1";
           DataAccess dataAccess = new DataAccess(Global.ConnectionString());
           SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
           strError = dataAccess.Error;
           arrInventory = new ArrayList();

           if (sqlDataReader.HasRows)
           {
               while (sqlDataReader.Read())
               {
                   arrInventory.Add(GenerateObject(sqlDataReader));
               }
           }
           else
               arrInventory.Add(new Inventory());

           return arrInventory;
       }
       public ArrayList BiayaRetrieveByCriteria(string strField, string strValue)
       {
           string strSQL = "select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga," +
                         "A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy," +
                         "A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,aktif,A.Stock,A.LeadTime from Biaya as A,UOM as C " +
                         "where A.RowStatus > -1 and A.UOMID = C.ID and " + strField + " like '%" + strValue + "%' and aktif=1";
           DataAccess dataAccess = new DataAccess(Global.ConnectionString());
           SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
           //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID from Biaya as A,UOM as C where A.DeptID = B.ID and A.RowStatus>-1 and " + strField + " like '%" + strValue + "%'");
           strError = dataAccess.Error;
           arrInventory = new ArrayList();

           if (sqlDataReader.HasRows)
           {
               while (sqlDataReader.Read())
               {
                   arrInventory.Add(GenerateObject(sqlDataReader));
               }
           }
           else
               arrInventory.Add(new Inventory());

           return arrInventory;
       }

       public ArrayList BiayaRetrieveByCriteriaWithGroupID(string strField, string strValue, int groupID)
       {
           DataAccess dataAccess = new DataAccess(Global.ConnectionString());
           SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,B.DeptName,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,aktif,A.Stock,A.LeadTime from Biaya as A,Dept as B,UOM as C where A.DeptID = B.ID and A.UOMID = C.ID and A.GroupID=" + groupID + " and " + strField + " like '%" + strValue + "%'");
           strError = dataAccess.Error;
           arrInventory = new ArrayList();

           if (sqlDataReader.HasRows)
           {
               while (sqlDataReader.Read())
               {
                   arrInventory.Add(GenerateObject(sqlDataReader));
               }
           }
           else
               arrInventory.Add(new Inventory());

           return arrInventory;
       }

       public ArrayList AssetRetrieveByCriteriaWithGroupID(string strField, string strValue, int groupID)
       {
           DataAccess dataAccess = new DataAccess(Global.ConnectionString());
           SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,B.DeptName,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,aktif,A.Stock,A.LeadTime from Asset as A,Dept as B,UOM as C where A.DeptID = B.ID and A.UOMID = C.ID and A.GroupID=" + groupID + " and " + strField + " like '%" + strValue + "%'");
           strError = dataAccess.Error;
           arrInventory = new ArrayList();

           if (sqlDataReader.HasRows)
           {
               while (sqlDataReader.Read())
               {
                   arrInventory.Add(GenerateObject(sqlDataReader));
               }
           }
           else
               arrInventory.Add(new Inventory());

           return arrInventory;
       }

       public Inventory RetrieveRecordByID(int id)
       {
           DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.DeptID,B.DeptName,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID from Inventory as A,Dept as B,UOM as C where A.DeptID = B.ID and A.UOMID = C.ID and A.ID = '" + id + "'");
           SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,aktif,A.Stock,A.LeadTime from Inventory as A,UOM as C where A.UOMID = C.ID and A.ID = '" + id + "'");
            strError = dataAccess.Error;
            arrInventory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Inventory();
       }

       public Inventory RetrieveRecordByID2(int id)
       {
           DataAccess dataAccess = new DataAccess(Global.ConnectionString());
           //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.DeptID,B.DeptName,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID from Inventory as A,Dept as B,UOM as C where A.DeptID = B.ID and A.UOMID = C.ID and A.ID = '" + id + "'");
           SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMDesc,C.UOMCode,A.Jumlah,A.Harga,A.MinStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,A.MaxStock,A.ReOrder,aktif from Inventory as A,UOM as C where A.UOMID = C.ID and A.ID = '" + id + "'");
           strError = dataAccess.Error;
           arrInventory = new ArrayList();

           if (sqlDataReader.HasRows)
           {
               while (sqlDataReader.Read())
               {
                   return GenerateObject2(sqlDataReader);
               }
           }

           return new Inventory();
       }
       protected string _strError = string.Empty;
       protected SqlCommand sqlCommand;
       //public SqlDataReader RetrieveDataByString(string strQuery)
       //{
       //    System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection();
       //    sqlConnection.ConnectionString = "Initial Catalog=bpaskrwg;Data Source=it;User ID=sa;Password=Passw0rd;Max Pool Size=1000000";
       //    try
       //    {
       //        sqlCommand = new SqlCommand(strQuery, sqlConnection);
       //        return sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
       //    }
       //    catch (Exception ex)
       //    {
       //        _strError = ex.Message;
       //        SqlDataReader sqlDataReader = null;
       //        return sqlDataReader;
       //    }
       //}
        public int CountItemCode(string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT MAX(SUBSTRING(ItemCode,7,4)) as id from Inventory where left(Inventory.ItemCode,5) = '" + strValue + "'");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT isnull(MAX(substring(itemcode,8,4)),0) as id from Inventory where LEFT(itemcode,7) = '" + strValue + "'");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Id"]);
                }
            }

            return 0;
        }
        public int CountNewItemCode(string ItemCode)
        {
            string strSQL = "Select ID from Inventory where ItemCode='" + ItemCode + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["ID"]);
                }
            }

            return 0;
        }
        public Inventory GenerateObject(SqlDataReader sqlDataReader)
        {
            //try
            //{
                objInventory = new Inventory();
                objInventory.ID = Convert.ToInt32(sqlDataReader["ID"]);
                objInventory.ItemCode = sqlDataReader["ItemCode"].ToString();
                objInventory.ItemName = sqlDataReader["ItemName"].ToString();
                objInventory.SupplierCode = sqlDataReader["SupplierCode"].ToString();
                objInventory.UOMID = Convert.ToInt32(sqlDataReader["UOMID"]);
                objInventory.UOMDesc = sqlDataReader["UOMDesc"].ToString();
                objInventory.UomCode = sqlDataReader["UomCode"].ToString();
                objInventory.Jumlah = Convert.ToDecimal(sqlDataReader["Jumlah"].ToString());
                objInventory.MaxStock = Convert.ToDecimal(sqlDataReader["MaxStock"].ToString());
                //objInventory.Harga = Convert.ToInt32(sqlDataReader["Harga"].ToString());
                objInventory.MinStock = Convert.ToDecimal(sqlDataReader["MinStock"].ToString());
                objInventory.DeptID = Convert.ToInt32(sqlDataReader["DeptID"].ToString());
                objInventory.RakID = Convert.ToInt32(sqlDataReader["RakID"].ToString());
                objInventory.GroupID = Convert.ToInt32(sqlDataReader["GroupID"].ToString());
                objInventory.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"].ToString());
                objInventory.Gudang = Convert.ToInt32(sqlDataReader["Gudang"].ToString());
                objInventory.ShortKey = sqlDataReader["ShortKey"].ToString();
                objInventory.Keterangan = sqlDataReader["Keterangan"].ToString();
                objInventory.Head = Convert.ToInt32(sqlDataReader["Head"]);
                objInventory.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
                objInventory.CreatedBy = sqlDataReader["CreatedBy"].ToString();
                //objInventory.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
                objInventory.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
                //objInventory.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
                objInventory.Aktif = Convert.ToInt32(sqlDataReader["aktif"].ToString());
                objInventory.Stock = (sqlDataReader["stock"] == DBNull.Value) ? 0 : Convert.ToInt32(sqlDataReader["stock"].ToString());
                objInventory.LeadTime = (sqlDataReader["LeadTime"]==DBNull.Value)?0: Convert.ToInt32(sqlDataReader["LeadTime"].ToString());
            //}
            //catch(Exception ex)
            //{
            //    strError = ex.Message;
            //}
            return objInventory;

        }
        public Inventory GenerateObject(SqlDataReader sdr, Inventory inventory)
        {
            objInventory = (Inventory)inventory;
            objInventory.ReOrder = Convert.ToDecimal(sdr["Reorder"].ToString());
            return objInventory;
        }
        public Inventory GenerateObject2(SqlDataReader sqlDataReader)
        {
            objInventory = new Inventory();
            objInventory.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objInventory.ItemCode = sqlDataReader["ItemCode"].ToString();
            objInventory.ItemName = sqlDataReader["ItemName"].ToString();
            objInventory.SupplierCode = sqlDataReader["SupplierCode"].ToString();
            objInventory.UOMID = Convert.ToInt32(sqlDataReader["UOMID"]);
            objInventory.UOMDesc = sqlDataReader["UOMDesc"].ToString();
            objInventory.UomCode = sqlDataReader["UOMCode"].ToString();
            objInventory.Jumlah = Convert.ToDecimal(sqlDataReader["Jumlah"]);
            objInventory.Harga = Convert.ToInt32(sqlDataReader["Harga"]);
            objInventory.MinStock = Convert.ToInt32(sqlDataReader["MinStock"]);
            objInventory.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            //objInventory.DeptName = sqlDataReader["DeptName"].ToString();
            objInventory.RakID = Convert.ToInt32(sqlDataReader["RakID"]);
            objInventory.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objInventory.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"]);
            objInventory.Gudang = Convert.ToInt32(sqlDataReader["Gudang"]);
            objInventory.ShortKey = sqlDataReader["ShortKey"].ToString();
            objInventory.Keterangan = sqlDataReader["Keterangan"].ToString();
            objInventory.Head = Convert.ToInt32(sqlDataReader["Head"]);
            objInventory.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objInventory.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            //objInventory.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objInventory.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            //objInventory.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);

            objInventory.MaxStock = Convert.ToDecimal(sqlDataReader["MaxStock"]);
            objInventory.ReOrder = Convert.ToDecimal(sqlDataReader["ReOrder"]);

            return objInventory;

        }

         public Inventory GenerateObjectRePack(SqlDataReader sqlDataReader)
        {
            try
            {
                objInventory = new Inventory();
                objInventory.ID = Convert.ToInt32(sqlDataReader["ID"]);
                objInventory.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
                objInventory.UOMID = Convert.ToInt32(sqlDataReader["UOMID"]);
                objInventory.Jumlah = Convert.ToDecimal(sqlDataReader["Jumlah"]);
                objInventory.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
                objInventory.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            }
            catch
            {
            }
            return objInventory;

        }
         public Inventory GenerateObjectROP(SqlDataReader sqlDataReader)
         {
             objInventory = new Inventory();
             objInventory.ItemID = Convert.ToInt32(sqlDataReader["ID"]);
             objInventory.UOMID = Convert.ToInt32(sqlDataReader["UOMID"]);
             objInventory.Jumlah = Convert.ToDecimal(sqlDataReader["Jumlah"]);
             objInventory.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
             objInventory.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
             return objInventory;

         }

         public ArrayList RetrieveByNameTypeID(string strField, string strValue, int typeID)
         {
             int tahun = (HttpContext.Current.Session["tahun"] == null) ? DateTime.Now.Year : int.Parse(HttpContext.Current.Session["tahun"].ToString());
             string kodelama = "";// (tahun < 2014) ? " LEN(ItemCode)<12" : " Aktif=1 ";
             string strTypeID = string.Empty;
             strTypeID = " and A.ItemTypeID =" + typeID;
             string strSQL = "select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock," +
                             "A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime," +
                             "A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,A.aktif,A.Stock,A.LeadTime from Inventory as A,UOM as C where " +
                             "A.RowStatus>-1 and A.UOMID = C.ID and " + strField + " like '%" + strValue + "%' " + strTypeID + kodelama;
             DataAccess dataAccess = new DataAccess(Global.ConnectionString());
             SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
             strError = dataAccess.Error;
             arrInventory = new ArrayList();

             if (sqlDataReader.HasRows)
             {
                 while (sqlDataReader.Read())
                 {
                     arrInventory.Add(GenerateObject(sqlDataReader));
                 }
             }
             else
                 arrInventory.Add(new Inventory());

             return arrInventory;
         }

         public ArrayList RetrieveByKartuStock(string strValue)
         {
             DataAccess dataAccess = new DataAccess(Global.ConnectionString());
             string strsql = " " +
                 "SELECT A.ID,A.ItemCode,'(Inventory)' + A.ItemName as ItemName,A.Reorder,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,A.aktif,A.Stock,ISNULL(A.LeadTime,0)LeadTime from Inventory as A,UOM as C where A.aktif =1 and A.RowStatus>-1 and A.UOMID = C.ID and ItemName LIKE '%" + strValue + "%' " +
                 "UNION " +
                 " SELECT A.ID,A.ItemCode,'(Asset)'+ A.ItemName as ItemName,A.Reorder,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,A.aktif,A.Stock,ISNULL(A.LeadTime,0)LeadTime from Asset as A,UOM as C where A.aktif >=1 and A.RowStatus>-1 and A.UOMID = C.ID and ItemName LIKE '%" + strValue + "%' " +
                 "UNION " +
                 " SELECT A.ID,A.ItemCode,'(Biaya)'+A.ItemName as ItemName,A.Reorder,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,A.aktif,A.Stock,ISNULL(A.LeadTime,0)LeadTime from Biaya as A,UOM as C where A.aktif =1 and A.RowStatus>-1 and A.UOMID = C.ID and ItemName LIKE '%" + strValue + "%' order by itemname";
             SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql); 
             strError = dataAccess.Error;
             arrInventory = new ArrayList();

             if (sqlDataReader.HasRows)
             {
                 while (sqlDataReader.Read())
                 {
                     arrInventory.Add(GenerateObject(sqlDataReader,GenerateObject(sqlDataReader)));
                 }
             }
             else
                 arrInventory.Add(new Inventory());

             return arrInventory;
         }

         public ArrayList RetrieveByKartuStockWithNonAktif(string strValue)
         {
             DataAccess dataAccess = new DataAccess(Global.ConnectionString());
             string strsql = " " +
                 "SELECT A.ID,A.ItemCode,'(Inventory)' + A.ItemName as ItemName,A.Reorder,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,A.aktif,A.Stock,ISNULL(A.LeadTime,0)LeadTime from Inventory as A,UOM as C where  A.RowStatus>-1 and A.UOMID = C.ID and ItemName LIKE '%" + strValue + "%' " +
                 "UNION " +
                 " SELECT A.ID,A.ItemCode,'(Asset)'+ A.ItemName as ItemName,A.Reorder,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,A.aktif,A.Stock,ISNULL(A.LeadTime,0)LeadTime from Asset as A,UOM as C where A.RowStatus>-1 and A.UOMID = C.ID and ItemName LIKE '%" + strValue + "%' " +
                 "UNION " +
                 " SELECT A.ID,A.ItemCode,'(Biaya)'+A.ItemName as ItemName,A.Reorder,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,A.aktif,A.Stock,ISNULL(A.LeadTime,0)LeadTime from Biaya as A,UOM as C where  A.RowStatus>-1 and A.UOMID = C.ID and ItemName LIKE '%" + strValue + "%' order by itemname";
             SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql); 
             strError = dataAccess.Error;
             arrInventory = new ArrayList();

             if (sqlDataReader.HasRows)
             {
                 while (sqlDataReader.Read())
                 {
                     arrInventory.Add(GenerateObject(sqlDataReader,GenerateObject(sqlDataReader)));
                 }
             }
             else
                 arrInventory.Add(new Inventory());

             return arrInventory;
         }
         public Inventory  RetrieveByKartuStockItemID(string strValue)
         {
             DataAccess dataAccess = new DataAccess(Global.ConnectionString());
             string strsql = " " +
                 "SELECT A.ID,A.ItemCode,A.ItemName ,A.Reorder,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,A.aktif,A.Stock,A.LeadTime from Inventory as A,UOM as C where A.aktif =1 and A.RowStatus>-1 and A.UOMID = C.ID and " + strValue + "'" +
                 "UNION " +
                 " SELECT A.ID,A.ItemCode, A.ItemName,A.Reorder,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,A.aktif,A.Stock,A.LeadTime from Asset as A,UOM as C where A.aktif >=1 and A.RowStatus>-1 and A.UOMID = C.ID and  " + strValue + "'" +
                 "UNION " +
                 " SELECT A.ID,A.ItemCode,A.ItemName ,A.Reorder,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,A.aktif,A.Stock,A.LeadTime from Biaya as A,UOM as C where A.aktif =1 and A.RowStatus>-1 and A.UOMID = C.ID and " + strValue + "'";
             SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
             strError = dataAccess.Error;
             arrInventory = new ArrayList();

             if (sqlDataReader.HasRows)
             {
                 while (sqlDataReader.Read())
                 {
                     return GenerateObject(sqlDataReader,GenerateObject(sqlDataReader));
                 }
             }

             return new Inventory();
         }

         public Inventory GenerateObjectnew(SqlDataReader sqlDataReader)
         {
             objInventory = new Inventory();
             objInventory.ID = Convert.ToInt32(sqlDataReader["ID"]);
             objInventory.ItemCode = sqlDataReader["ItemCode"].ToString();
             objInventory.ItemName = sqlDataReader["ItemName"].ToString();
             objInventory.SupplierCode = sqlDataReader["SupplierCode"].ToString();
             objInventory.UOMID = Convert.ToInt32(sqlDataReader["UOMID"]);
             objInventory.UOMDesc = sqlDataReader["UOMDesc"].ToString();
             objInventory.UomCode = sqlDataReader["UOMCode"].ToString();
             objInventory.Jumlah = Convert.ToDecimal(sqlDataReader["Jumlah"]);
             objInventory.Harga = Convert.ToInt32(sqlDataReader["Harga"]);
             objInventory.MinStock = Convert.ToInt32(sqlDataReader["MinStock"]);
             objInventory.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
             //objInventory.DeptName = sqlDataReader["DeptName"].ToString();
             objInventory.RakID = Convert.ToInt32(sqlDataReader["RakID"]);
             objInventory.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
             objInventory.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"]);
             objInventory.Gudang = Convert.ToInt32(sqlDataReader["Gudang"]);
             objInventory.ShortKey = sqlDataReader["ShortKey"].ToString();
             objInventory.Keterangan = sqlDataReader["Keterangan"].ToString();
             objInventory.Head = Convert.ToInt32(sqlDataReader["Head"]);
             objInventory.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
             objInventory.CreatedBy = sqlDataReader["CreatedBy"].ToString();
             objInventory.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
             objInventory.MaxStock = Convert.ToDecimal(sqlDataReader["MaxStock"]);
             objInventory.ReOrder = Convert.ToDecimal(sqlDataReader["ReOrder"]);
             objInventory.Aktif = Convert.ToInt32(sqlDataReader["aktif"]);
             objInventory.Stock = Convert.ToInt32(sqlDataReader["stock"]);
             objInventory.Nama = sqlDataReader["Nama"].ToString();
             objInventory.Type = sqlDataReader["IType"].ToString();
             objInventory.Merk = sqlDataReader["Merk"].ToString();
             objInventory.Jenis = sqlDataReader["Jenis"].ToString();
             objInventory.Ukuran = sqlDataReader["Ukuran"].ToString();
             objInventory.Partnum = sqlDataReader["Partnum"].ToString();
             objInventory.LeadTime = Convert.ToInt32(sqlDataReader["LeadTime"]);
             objInventory.Alasannonaktif = sqlDataReader["alasannonaktif"].ToString();
             return objInventory;
         }
         public Inventory GenerateBarang(SqlDataReader sqlDataReader)
         {
             objInventory = new Inventory();
             objInventory.ID = Convert.ToInt32(sqlDataReader["ID"]);
             objInventory.ItemCode = sqlDataReader["ItemCode"].ToString();
             objInventory.ItemName = sqlDataReader["ItemName"].ToString();
             objInventory.SupplierCode = sqlDataReader["SupplierCode"].ToString();
             objInventory.UOMID = Convert.ToInt32(sqlDataReader["UOMID"]);
             objInventory.UOMDesc = sqlDataReader["UOMDesc"].ToString();
             //objInventory.UomCode = sqlDataReader["UOMCode"].ToString();
             objInventory.Jumlah = Convert.ToDecimal(sqlDataReader["Jumlah"]);
             objInventory.Harga = Convert.ToInt32(sqlDataReader["Harga"]);
             objInventory.MinStock = Convert.ToInt32(sqlDataReader["MinStock"]);
             objInventory.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
             //objInventory.DeptName = sqlDataReader["DeptName"].ToString();
             objInventory.RakID = Convert.ToInt32(sqlDataReader["RakID"]);
             objInventory.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
             objInventory.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"]);
             objInventory.Gudang = Convert.ToInt32(sqlDataReader["Gudang"]);
             objInventory.ShortKey = sqlDataReader["ShortKey"].ToString();
             objInventory.Keterangan = sqlDataReader["Keterangan"].ToString();
             objInventory.Head = Convert.ToInt32(sqlDataReader["Head"]);
             objInventory.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
             objInventory.CreatedBy = sqlDataReader["CreatedBy"].ToString();
             objInventory.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
             objInventory.MaxStock = Convert.ToDecimal(sqlDataReader["MaxStock"]);
             objInventory.ReOrder = Convert.ToDecimal(sqlDataReader["ReOrder"]);
             objInventory.Aktif = Convert.ToInt32(sqlDataReader["aktif"]);
             objInventory.Stock = Convert.ToInt32(sqlDataReader["stock"]); 
             objInventory.Nama = sqlDataReader["Nama"].ToString();
             objInventory.Type = sqlDataReader["IType"].ToString();
             objInventory.Merk = sqlDataReader["Merk"].ToString();
             objInventory.Jenis = sqlDataReader["Jenis"].ToString();
             objInventory.Ukuran = sqlDataReader["Ukuran"].ToString();
             objInventory.Partnum = sqlDataReader["Partnum"].ToString();
             //objInventory.LeadTime = Convert.ToInt32(sqlDataReader["LeadTime"]);
             objInventory.Alasannonaktif = sqlDataReader["alasannonaktif"].ToString();
             objInventory.GroupDescription = sqlDataReader["GroupDescription"].ToString();
             return objInventory;
         }
         /**
          * added on 19-05-2014
          * Inventory Stock Overview
          */
         public ArrayList RetrieveForStockView(string where,string Table,string OrdBy)
         {
             string ordBy = (OrdBy == string.Empty) ? "ItemCode" : OrdBy;
             string strSQL = "select ID,ItemCode,ItemName,UOMDesc,Jumlah,ISNULL(w.Stock,0) as Stock,Aktif,JmlTransit/*,(select DeptName from Dept where ID=w.GudangID) as DeptName*/ from( " +
                             "select ID,ItemCode,ItemName,(select UOMDesc from UOM where ID=i.UOMID) as UOMDesc,Jumlah,JmlTransit, GroupID,Aktif," +
                             "(SELECT (SUM(mg.QtyReceipt)-SUM(mg.QtyPakai)) from SPPMultiGudang as mg where ItemID=i.ID and mg.ItemTypeID=i.ItemTypeID and mg.Aktif=1) as Stock " +
                             "/*,(SELECT GudangID from SPPMultiGudang as mg where ItemID=i.ID and mg.ItemTypeID=i.ItemTypeID and mg.Aktif=1) as GudangID*/ "+
                             "FROM "+Table+" as i /*where i.Aktif=1 and LEN(ItemCode)=15*/ ) as w "+
                             " where " + where + " " +
                             "order by " + ordBy;
             SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
             strError = dataAccess.Error;
             arrInventory = new ArrayList();

             if (sqlDataReader.HasRows)
             {
                 while (sqlDataReader.Read())
                 {
                     arrInventory.Add(GenerateObjectForStock(sqlDataReader));
                 }
             }
             else
                 arrInventory.Add(new Inventory());

             return arrInventory;
         }
         public Inventory DetailStockView(string ItemCode)
         {
             string strSQL = "select ID,ItemCode,ItemName,Satuan,Jumlah,ISNULL(w.Stock,0) as Stock from( " +
                             "select ID,ItemCode,ItemName,(select UOMDesc from UOM where ID=i.UOMID) as Satuan,Jumlah,i.LeadTime," +
                             "(SELECT (SUM(mg.QtyReceipt)-SUM(mg.QtyPakai)) from SPPMultiGudang as mg where ItemID=i.ID and mg.ItemTypeID=i.ItemTypeID and mg.Aktif=1) as Stock " +
                             "FROM Inventory as i where i.ItemCode='" + ItemCode + "' and i.Aktif=1 and LEN(ItemCode)=15 ) as w " +
                             "order by w.ItemName";
             SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
             strError = dataAccess.Error;
             arrInventory = new ArrayList();

             if (sqlDataReader.HasRows)
             {
                 while (sqlDataReader.Read())
                 {
                     return GenerateObject(sqlDataReader);
                 }
             }

             return new Inventory();
         }

         public Inventory GenerateObjectForStock(SqlDataReader sqlDataReader)
         {
             objInventory = new Inventory();
             objInventory.ID = Convert.ToInt32(sqlDataReader["ID"]);
             objInventory.ItemCode = sqlDataReader["ItemCode"].ToString();
             objInventory.ItemName = sqlDataReader["ItemName"].ToString();
             objInventory.UOMDesc = sqlDataReader["UOMDesc"].ToString();
             objInventory.Jumlah = Convert.ToDecimal(sqlDataReader["Jumlah"]);
             objInventory.Harga = Convert.ToInt32(sqlDataReader["JmlTransit"]);
             //objInventory.DeptName = sqlDataReader["DeptName"].ToString();
             return objInventory;
         }

         public int CheckDeptID(int UserID)
         {
             string strSQL = "select DeptID from users where ID=" + UserID;
             SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
             strError = dataAccess.Error;
             arrInventory = new ArrayList();

             if (sqlDataReader.HasRows)
             {
                 while (sqlDataReader.Read())
                 {
                     return int.Parse(sqlDataReader["DeptID"].ToString());
                 }
             }

             return 0;
         }
         public int GetItemID(string ItemName, string TabelName)
         {
             string strSQL = "select top 1 ID from " + TabelName + " where ItemName='" + ItemName + "' and aktif=1";
             SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
             strError = dataAccess.Error;
             arrInventory = new ArrayList();

             if (sqlDataReader.HasRows)
             {
                 while (sqlDataReader.Read())
                 {
                     return int.Parse(sqlDataReader["ID"].ToString());
                 }
             }

             return 0;
         }
         public string TglPeriode { get; set; }

         public decimal GetStockAkhir(string ItemID, int ItemTypeID, string Bulan, string Tahun)
         {
             #region proses saldoinventory
             string Field = string.Empty;
             string Period = string.Empty;
             switch (int.Parse(Bulan))
             {
                 case 1: Field = "DesQty"; Period = (int.Parse(Tahun) - 1).ToString(); break;
                 case 2: Field = "JanQty"; Period = Tahun; break;
                 case 3: Field = "FebQty"; Period = Tahun; break;
                 case 4: Field = "MarQty"; Period = Tahun; break;
                 case 5: Field = "AprQty"; Period = Tahun; break;
                 case 6: Field = "MeiQty"; Period = Tahun; break;
                 case 7: Field = "JunQty"; Period = Tahun; break;
                 case 8: Field = "JulQty"; Period = Tahun; break;
                 case 9: Field = "AguQty"; Period = Tahun; break;
                 case 10: Field = "SepQty"; Period = Tahun; break;
                 case 11: Field = "OktQty"; Period = Tahun; break;
                 case 12: Field = "NovQty"; Period = Tahun; break;
             }
             #endregion
             decimal result = 0;
             string jmlTransit = string.Empty; string ast = string.Empty; string ast2 = string.Empty;
             switch (ItemTypeID)
             {
                 case 1: jmlTransit = "UNION ALL 	SELECT (-1 * JmlTransit) FROM Inventory WHERE JmlTransit>0 AND ID=" + ItemID; break;
                 //case 2: jmlTransit = "UNION ALL 	SELECT (-1 * JmlTransit) FROM Asset WHERE JmlTransit>0 AND ID=" + ItemID; break;
                 case 2: jmlTransit = "UNION ALL 	SELECT (-1 * JmlTransit) FROM Asset WHERE GroupID=12 and JmlTransit>0 AND ID=" + ItemID; break;
                 case 3: jmlTransit = "UNION ALL 	SELECT (-1 * JmlTransit) FROM Biaya WHERE JmlTransit>0 AND ID=" + ItemID; break;
             }

             if (ItemTypeID == 2)
             {
                 ast = " and GroupID=4 ";
                
             }
             else if (ItemTypeID == 4)
             {
                 ast = " and GroupID=12 ";
                
             }
             else
             {
                 ast = " "; 
             }
             string strSQL = "WITH ItemSaldo AS " +
                            "( " +
                            "    SELECT SUM(ISNULL(Quantity,0)) Quantity FROM vw_StockPurchn WHERE ItemID=" + ItemID + " AND ItemTypeID=" + ItemTypeID +
                            "  "+ast+" AND YM=" + Tahun + Bulan.PadLeft(2, '0') + this.TglPeriode+
                            "    GROUP BY ItemID " +
                            "    UNION ALL " +
                            "    SELECT ISNULL(" + Field + ",0)Quantity FROM SaldoInventory WHERE ItemID=" + ItemID + " AND ItemTypeID=" +
                            ItemTypeID + " AND YearPeriod=" + Period + " " + ast + "" +
                            //jmlTransit +
                            ") " +
                            "SELECT ISNULL(SUM(Quantity),0) SaldoAkhir FROM ItemSaldo";
             DataAccess da = new DataAccess(Global.ConnectionString());
             SqlDataReader sdr = da.RetrieveDataByString(strSQL);
             if (da.Error == string.Empty && sdr.HasRows)
             {
                 while (sdr.Read())
                 {
                     result = decimal.Parse(sdr["SaldoAkhir"].ToString());
                 }
             }
             return result;
         }

         public decimal GetStockAkhirAsset_Lama(string ItemID, int ItemTypeID, string Bulan, string Tahun)
         {
             #region proses saldoinventory
             string Field = string.Empty;
             string Period = string.Empty;
             switch (int.Parse(Bulan))
             {
                 case 1: Field = "DesQty"; Period = (int.Parse(Tahun) - 1).ToString(); break;
                 case 2: Field = "JanQty"; Period = Tahun; break;
                 case 3: Field = "FebQty"; Period = Tahun; break;
                 case 4: Field = "MarQty"; Period = Tahun; break;
                 case 5: Field = "AprQty"; Period = Tahun; break;
                 case 6: Field = "MeiQty"; Period = Tahun; break;
                 case 7: Field = "JunQty"; Period = Tahun; break;
                 case 8: Field = "JulQty"; Period = Tahun; break;
                 case 9: Field = "AguQty"; Period = Tahun; break;
                 case 10: Field = "SepQty"; Period = Tahun; break;
                 case 11: Field = "OktQty"; Period = Tahun; break;
                 case 12: Field = "NovQty"; Period = Tahun; break;
             }
             #endregion
             decimal result = 0;
             string jmlTransit = string.Empty; string ast = string.Empty; string ast2 = string.Empty;             

             if (ItemTypeID == 4)
             {
                 ast = " and GroupID=4 ";
                 ast2 = " 2 ";
             }
             else if (ItemTypeID == 12)
             {
                 ast = " and GroupID=12 ";
                 ast2 = " 2 ";
             }
             
             string strSQL = "WITH ItemSaldo AS " +
                            "( " +
                            "    SELECT SUM(ISNULL(Quantity,0)) Quantity FROM vw_StockPurchn WHERE ItemID=" + ItemID + " AND ItemTypeID=" + ast2 +
                            "  " + ast + " AND YM=" + Tahun + Bulan.PadLeft(2, '0') + this.TglPeriode +
                            "    GROUP BY ItemID " +
                            "    UNION ALL " +
                            "    SELECT ISNULL(" + Field + ",0)Quantity FROM SaldoInventory WHERE ItemID=" + ItemID + " AND ItemTypeID=" +
                            ast2 + " AND YearPeriod=" + Period + " " + ast + "" +
                 //jmlTransit +
                            ") " +
                            "SELECT ISNULL(SUM(Quantity),0) SaldoAkhir FROM ItemSaldo";
             DataAccess da = new DataAccess(Global.ConnectionString());
             SqlDataReader sdr = da.RetrieveDataByString(strSQL);
             if (da.Error == string.Empty && sdr.HasRows)
             {
                 while (sdr.Read())
                 {
                     result = decimal.Parse(sdr["SaldoAkhir"].ToString());
                 }
             }
             return result;
         }

         public decimal GetStockAkhirAsset(string ItemID, int GroupAsset, string Bulan, string Tahun)
         {
             #region proses saldoinventory
             string Field = string.Empty;
             string Period = string.Empty;
             switch (int.Parse(Bulan))
             {
                 case 1: Field = "DesQty"; Period = (int.Parse(Tahun) - 1).ToString(); break;
                 case 2: Field = "JanQty"; Period = Tahun; break;
                 case 3: Field = "FebQty"; Period = Tahun; break;
                 case 4: Field = "MarQty"; Period = Tahun; break;
                 case 5: Field = "AprQty"; Period = Tahun; break;
                 case 6: Field = "MeiQty"; Period = Tahun; break;
                 case 7: Field = "JunQty"; Period = Tahun; break;
                 case 8: Field = "JulQty"; Period = Tahun; break;
                 case 9: Field = "AguQty"; Period = Tahun; break;
                 case 10: Field = "SepQty"; Period = Tahun; break;
                 case 11: Field = "OktQty"; Period = Tahun; break;
                 case 12: Field = "NovQty"; Period = Tahun; break;
             }
             #endregion
             decimal result = 0;
             string jmlTransit = string.Empty; string ast = string.Empty; string ast2 = string.Empty;
             string query = string.Empty;

             if (GroupAsset == 4)
             {
                 ast = " and GroupID=4 and process<>'pakaiDetail' ";
                 ast2 = " 2 ";
                 query =
                 " SELECT SUM(ISNULL(Quantity,0)) Quantity FROM vw_StockPurchn WHERE ItemID=" + ItemID + " AND ItemTypeID=" + ast2 +
                 " " + ast + " " +
                 " GROUP BY ItemID " +
                 " union all  select sum(Quantity) * -1 from AdjustDetail where  ItemID=" + ItemID + " AND ItemTypeID= 2   and GroupID=4 " +
                 " and AdjustID in (select ID from Adjust where AdjustType='Disposal' and RowStatus>-1)  and RowStatus>-1 " +
                 " union all select JanQty from SaldoInventory where ItemID=" + ItemID + " AND ItemTypeID=2 AND JanQty>0 AND YearPeriod=2010 ";
             }
             else if (GroupAsset == 12)
             {
                 ast = " and GroupID=12 ";
                 ast2 = " 2 ";
                 query =
                 " SELECT SUM(ISNULL(Quantity,0)) Quantity FROM vw_StockPurchn WHERE ItemID=" + ItemID + " AND ItemTypeID=" + ast2 +
                 " " + ast + " AND YM=" + Tahun + Bulan.PadLeft(2, '0') + this.TglPeriode +
                 " GROUP BY ItemID " +
                 " UNION ALL " +
                 " SELECT ISNULL(" + Field + ",0)Quantity FROM SaldoInventory WHERE ItemID=" + ItemID + " AND ItemTypeID=" +
                 ast2 + " AND YearPeriod=" + Period + " " + ast + "";
             }

             string strSQL = " WITH ItemSaldo AS " +
                            " ( " +
                            " " + query + " " +
                            " ) " +
                            " SELECT ISNULL(SUM(Quantity),0) SaldoAkhir FROM ItemSaldo ";

             DataAccess da = new DataAccess(Global.ConnectionString());
             SqlDataReader sdr = da.RetrieveDataByString(strSQL);
             if (da.Error == string.Empty && sdr.HasRows)
             {
                 while (sdr.Read())
                 {
                     result = decimal.Parse(sdr["SaldoAkhir"].ToString());
                 }
             }
             return result;
         }

         public decimal GetPendingSPB(string ItemID, int ItemTypeID)
         {
             decimal result = 0;
             string strSQL = "SELECT ISNULL(SUM(Quantity),0) SaldoAkhir FROM PakaiDetail WHERE ItemID=" + ItemID + " AND ItemTypeID=" + ItemTypeID +
                            "AND PakaiID IN(SELECT ID FROM Pakai WHERE Status in (0,1,2)) AND RowStatus >-1";
             DataAccess da = new DataAccess(Global.ConnectionString());
             SqlDataReader sdr = da.RetrieveDataByString(strSQL);
             if (da.Error == string.Empty && sdr.HasRows)
             {
                 while (sdr.Read())
                 {
                     result = decimal.Parse(sdr["SaldoAkhir"].ToString());
                 }
             }
             return result;
         }
         public ArrayList RetrieveMaterialTrans(string strQuery)
         {
             ArrayList arrData = new ArrayList();
             DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
             SqlDataReader sdr = da.RetrieveDataByString(strQuery);
             if (da.Error == string.Empty && sdr.HasRows)
             {
                 while (sdr.Read())
                 {
                     arrData.Add(ksObject(sdr));
                 }
             }
             return arrData;
         }
         private KartStock ksObject(SqlDataReader sdr)
         {
             objKs = new KartStock();
             objKs.Tanggal = DateTime.Parse(sdr["Tanggal"].ToString());
             objKs.BonNo = sdr["Faktur"].ToString();
             objKs.Uraian = sdr["Keterangan"].ToString();
             objKs.Masuk = decimal.Parse(sdr["Masuk"].ToString());
             objKs.Keluar = decimal.Parse(sdr["Keluar"].ToString());
             objKs.Saldo = decimal.Parse(sdr["Saldo"].ToString());
             objKs.Tipe = int.Parse(sdr["Tipe"].ToString());
             return objKs;   
         }

         public ArrayList BiayaRetrieveByCriteriaAssetKomponen(string strField, string strValue)
         {
             string strSQL = "select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga," +
                           "A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy," +
                           "A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,aktif,A.Stock,A.LeadTime from Asset as A,UOM as C " +
                           "where A.RowStatus > -1 and A.UOMID = C.ID and " + strField + " like '%" + strValue + "%' and aktif=1";
             DataAccess dataAccess = new DataAccess(Global.ConnectionString());
             SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
             //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID from Biaya as A,UOM as C where A.DeptID = B.ID and A.RowStatus>-1 and " + strField + " like '%" + strValue + "%'");
             strError = dataAccess.Error;
             arrInventory = new ArrayList();

             if (sqlDataReader.HasRows)
             {
                 while (sqlDataReader.Read())
                 {
                     arrInventory.Add(GenerateObject(sqlDataReader));
                 }
             }
             else
                 arrInventory.Add(new Inventory());

             return arrInventory;
         }

         public Inventory RetrieveByIdAssetBerKomponen(int Id)
         {
             string strSQL = "select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID," +
                             "A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime," +
                             "A.GroupID,A.ItemTypeID,A.Aktif,A.Stock,A.LeadTime from Asset as A,UOM as C where A.UOMID = C.ID and A.ID = " + Id;
             DataAccess dataAccess = new DataAccess(Global.ConnectionString());
             SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
             strError = dataAccess.Error;
             arrInventory = new ArrayList();

             if (sqlDataReader.HasRows)
             {
                 while (sqlDataReader.Read())
                 {
                     return GenerateObject(sqlDataReader);
                 }
             }
             return new Inventory();
         }
    }
}
