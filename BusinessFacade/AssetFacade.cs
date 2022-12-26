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
using Dapper;

namespace BusinessFacade
{
    public class AssetFacade : AbstractFacade
    {
        private Asset objInventory = new Asset();
        private ArrayList arrInventory;
        private List<SqlParameter> sqlListParam;

        public AssetFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objInventory = (Asset)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemCode", objInventory.ItemCode));
                sqlListParam.Add(new SqlParameter("@ItemName", objInventory.ItemName));
                sqlListParam.Add(new SqlParameter("@SupplierCode", objInventory.SupplierCode));
                sqlListParam.Add(new SqlParameter("@UOMID", objInventory.UOMID));
                sqlListParam.Add(new SqlParameter("@Jumlah", objInventory.Jumlah));
                sqlListParam.Add(new SqlParameter("@Harga", objInventory.Harga));
                sqlListParam.Add(new SqlParameter("@MinStock", objInventory.MinStock));
                sqlListParam.Add(new SqlParameter("@DeptID", objInventory.DeptID)); 
                sqlListParam.Add(new SqlParameter("@RakID", objInventory.RakID));
                sqlListParam.Add(new SqlParameter("@GroupID", objInventory.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objInventory.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@Gudang", objInventory.Gudang));
                sqlListParam.Add(new SqlParameter("@ShortKey", objInventory.ShortKey));
                sqlListParam.Add(new SqlParameter("@Keterangan", objInventory.Keterangan));
                sqlListParam.Add(new SqlParameter("@Head", objInventory.Head));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objInventory.CreatedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objInventory.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertAsset");

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
                objInventory = (Asset)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objInventory.ID));
                //sqlListParam.Add(new SqlParameter("@ItemCode", objInventory.ItemCode));
                //sqlListParam.Add(new SqlParameter("@ItemName", objInventory.ItemName));
                //sqlListParam.Add(new SqlParameter("@SupplierCode", objInventory.SupplierCode));
                //sqlListParam.Add(new SqlParameter("@UOMID", objInventory.UOMID));
                //sqlListParam.Add(new SqlParameter("@Jumlah", objInventory.Jumlah));
                //sqlListParam.Add(new SqlParameter("@Harga", objInventory.Harga));
                sqlListParam.Add(new SqlParameter("@MinStock", objInventory.MinStock));
                //sqlListParam.Add(new SqlParameter("@DeptID", objInventory.DeptID)); 
                sqlListParam.Add(new SqlParameter("@RakID", objInventory.RakID));
                //sqlListParam.Add(new SqlParameter("@GroupID", objInventory.GroupID));
                //sqlListParam.Add(new SqlParameter("@ItemTypeID", objInventory.ItemTypeID));
                //sqlListParam.Add(new SqlParameter("@Gudang", objInventory.Gudang));
                //sqlListParam.Add(new SqlParameter("@ShortKey", objInventory.ShortKey));
                //sqlListParam.Add(new SqlParameter("@Keterangan", objInventory.Keterangan));
                sqlListParam.Add(new SqlParameter("@Reorder", "0"));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objInventory.CreatedBy));
                sqlListParam.Add(new SqlParameter("@aktif", objInventory.Aktif ));
            
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateAsset");

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
                objInventory = (Asset)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objInventory.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objInventory.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteAsset");

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
                objInventory = (Asset)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objInventory.ID));
                sqlListParam.Add(new SqlParameter("@Jumlah", objInventory.Jumlah));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateQtyAsset");

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
                objInventory = (Asset)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objInventory.ID));
                sqlListParam.Add(new SqlParameter("@Jumlah", objInventory.Jumlah));

                int intResult = dataAccess.ProcessData(sqlListParam, "spMinusQtyTransitAsset");

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
                objInventory = (Asset)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objInventory.ID));
                sqlListParam.Add(new SqlParameter("@Jumlah", objInventory.Jumlah));

                int intResult = dataAccess.ProcessData(sqlListParam, "spMinusQtyAsset");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int InsertUpdateInvAsset(object objDomain)
        {

            try
            {
                objInventory = (Asset)objDomain;
                sqlListParam = new List<SqlParameter>();
                //sqlListParam.Add(new SqlParameter("@ID", objInventory.ID));
                // flag 0 = insert, 1 = update
                sqlListParam.Add(new SqlParameter("@AssetID", objInventory.AssetID));
                sqlListParam.Add(new SqlParameter("@Jumlah", objInventory.Jumlah));
                sqlListParam.Add(new SqlParameter("@DeptID", objInventory.DeptID)); 
                sqlListParam.Add(new SqlParameter("@Gudang", objInventory.Gudang));
                sqlListParam.Add(new SqlParameter("@RowStatus", objInventory.RowStatus));
                sqlListParam.Add(new SqlParameter("@Flag", objInventory.Flag));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertUpdateInvAsset");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int MinusQtyInvAsset(object objDomain)
        {

            try
            {
                objInventory = (Asset)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objInventory.ID));
                sqlListParam.Add(new SqlParameter("@Jumlah", objInventory.Jumlah));
                sqlListParam.Add(new SqlParameter("@DeptID", objInventory.DeptID));

                int intResult = dataAccess.ProcessData(sqlListParam, "spMinusQtyInvAsset");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override ArrayList Retrieve()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.stock,A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,A.AMgroupID,A.AMclassID,A.AMsubClassID,A.AMlokasiID from Asset as A,UOM as C where A.UOMID = C.ID order by GroupID");
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
                arrInventory.Add(new Asset());

            return arrInventory;
        }

        public ArrayList Retrieve2()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select top 50 A.stock,A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,A.Aktif,A.Stock,A.AMgroupID,A.AMclassID,A.AMsubClassID,A.AMlokasiID from asset as A,UOM as C where A.UOMID = C.ID order by ID desc";

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
                arrInventory.Add(new Asset());

            return arrInventory;
        }

        public Asset RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.stock,A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,A.Aktif,A.AMgroupID,A.AMclassID,A.AMsubClassID,A.AMlokasiID from Asset as A,UOM as C where A.UOMID = C.ID and A.ID = " + Id);
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

            return new Asset();
        }

        public ArrayList RetrieveByCriteriaWithGroupID(string strField, string strValue, int groupID)
        {
            string strGroupID = string.Empty;
            if (groupID > 0)
                strGroupID = " and A.GroupID =" + groupID+" and A.GroupID not in(6)";
                
            DataAccess da = new DataAccess(Global.ConnectionString());
            string strSQL =
            " select A.stock,A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock, " +
            " A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime, " +
            " A.GroupID,A.ItemTypeID,A.Aktif,A.AMgroupID,A.AMclassID,A.AMsubClassID,A.AMlokasiID " +
            " from Asset as A,UOM as C where A.ItemCode not like'%C%' and  A.aktif =1 and A.RowStatus>-1 and A.UOMID = C.ID and " + strField + " like '%" + strValue + "%' " + strGroupID ;
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.DeptID,B.DeptName,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID from Inventory as A,Dept as B,UOM as C where A.DeptID = B.ID and A.UOMID = C.ID and " + strField + " like '%" + strValue + "%' "+ strGroupID);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.stock,A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,A.Aktif,A.AMgroupID,A.AMclassID,A.AMsubClassID,A.AMlokasiID from Asset as A,UOM as C where  A.aktif =1 and A.RowStatus>-1 and A.UOMID = C.ID and " + strField + " like '%" + strValue + "%' " + strGroupID);
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrInventory = new ArrayList();

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrInventory.Add(GenerateObject(sdr));
                }
            }
            //else
            //    arrInventory.Add(new Inventory());

            return arrInventory;
        }

        public Asset InvAssetRetrieveByAssetID(int assetID,int deptID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.AssetID,A.Jumlah,A.DeptID,A.Gudang,A.RowStatus from InventoryAsset as A where A.RowStatus>-1 and A.AssetID=" + assetID + " and A.DeptID=" + deptID);
            strError = dataAccess.Error;
            arrInventory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectListInvAsset(sqlDataReader);
                }
            }

            return new Asset();
        }

        public Asset RetrieveByName(string strDescription)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.stock,A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.DeptID,B.DeptName,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,A.Aktif,A.AMgroupID,A.AMclassID,A.AMsubClassID,A.AMlokasiID from Asset as A,Dept as B,UOM as C where A.DeptID = B.ID and A.UOMID = C.ID and InventoryItemName = '" + strDescription + "'");
            strError = dataAccess.Error;
            arrInventory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Asset();
        }

        public ArrayList RetrieveByGroupID(int groupId, int itemType)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.stock,A.ID,A.ItemCode,A.ItemName,A.GroupID,B.GroupCode,B.GroupDescription,A.ShortKey,A.UOMID,C.UOMCode,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.Aktif,A.AMgroupID,A.AMclassID,A.AMsubClassID,A.AMlokasiID from Asset as A,GroupsPurchn as B, UOM as C where A.GroupID = B.ID and A.UOMID = C.ID and A.RowStatus = 0 and A.GroupID = " + groupId + " and A.ItemTypeID = " + itemType + " order by A.ItemName");
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
                arrInventory.Add(new Asset());

            return arrInventory;
        }

        public static List<Asset> RetrieveByCriteriaWithGroupIDNew(string strField, string strValue, int groupID)
        {
            List<Asset> alldata = new List<Asset>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string strGroupID = string.Empty;
                    if (groupID > 0)
                        strGroupID = " and A.GroupID =" + groupID + " and A.GroupID not in(6)";

                    string strSQL = "select A.stock,A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,A.Aktif,A.AMgroupID,A.AMclassID,A.AMsubClassID,A.AMlokasiID from Asset as A,UOM as C where  A.aktif =1 and A.RowStatus>-1 and A.UOMID = C.ID and " + strField + " like '%" + strValue + "%' " + strGroupID;
                    alldata = connection.Query<Asset>(strSQL).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }


        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select A.stock,A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.DeptID,A.RakID," +
                "A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,"+
                "A.Aktif,A.AMgroupID,A.AMclassID,A.AMsubClassID,A.AMlokasiID from Asset as A,UOM as C where  A.UOMID = C.ID and " + strField + " like '%" + strValue + "%'";
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
                arrInventory.Add(new Asset());

            return arrInventory;
        }

        public ArrayList RetrieveByCriteriaSJ(string strField, string strValue, string Jenis)
        {

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select A.stock,A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.DeptID,A.RakID," +
                "A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID," +
                "A.Aktif from " + Jenis + " as A,UOM as C where  A.UOMID = C.ID and " + strField + " like '%" + strValue + "%'";
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
                arrInventory.Add(new Asset());

            return arrInventory;
        }

        public Asset RetrieveRecordByID(int id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.stock,A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,A.Aktif,A.AMgroupID,A.AMclassID,A.AMsubClassID,A.AMlokasiID from Asset as A,UOM as C where A.UOMID = C.ID and A.ID = '" + id + "'");
            strError = dataAccess.Error;
            arrInventory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Asset();
        }

        public int CountItemCode(string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT MAX(SUBSTRING(ItemCode,7,4)) as id from Asset where left(Asset.ItemCode,5) = '" + strValue + "'");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT isnull(MAX(substring(itemcode,8,4)),0) as id from asset where LEFT(itemcode,7) = '" + strValue + "'");
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


        public Asset GenerateObject(SqlDataReader sqlDataReader)
        {
            try
            {
                objInventory = new Asset();
                objInventory.ID = Convert.ToInt32(sqlDataReader["ID"]);
                objInventory.ItemCode = sqlDataReader["ItemCode"].ToString();
                objInventory.ItemName = sqlDataReader["ItemName"].ToString();
                objInventory.SupplierCode = sqlDataReader["SupplierCode"].ToString();
                objInventory.UOMID = Convert.ToInt32(sqlDataReader["UOMID"]);
                objInventory.UOMDesc = sqlDataReader["UOMDesc"].ToString();
                objInventory.UomCode = sqlDataReader["UomCode"].ToString();
                objInventory.Jumlah = Convert.ToDecimal(sqlDataReader["Jumlah"]);
                //objInventory.MaxStock = Convert.ToInt32(sqlDataReader["MaxStock"]);
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
                objInventory.Stock = Convert.ToInt32(sqlDataReader["Stock"].ToString());
                objInventory.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
                objInventory.Aktif = Convert.ToInt32(sqlDataReader["Aktif"]);

                //iko
                objInventory.AmGroupID = Convert.ToInt32(sqlDataReader["AmGroupID"]);
                objInventory.AmClassID = Convert.ToInt32(sqlDataReader["AmClassID"]);
                objInventory.AmSubClassID = Convert.ToInt32(sqlDataReader["AmSubClassID"]);
                objInventory.AmLokasiID = Convert.ToInt32(sqlDataReader["AmLokasiID"]);
                //iko
            }
            catch { }
            return objInventory;
        }
        public Asset GenerateObjectListInvAsset(SqlDataReader sqlDataReader)
        {
            objInventory = new Asset();
            objInventory.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objInventory.AssetID = Convert.ToInt32(sqlDataReader["AssetID"]);
            objInventory.Jumlah = Convert.ToDecimal(sqlDataReader["Jumlah"]);
            objInventory.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objInventory.Gudang = Convert.ToInt32(sqlDataReader["Gudang"]);
            objInventory.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            return objInventory;
        }
        public ArrayList RetrieveByNameTypeID(string strField, string strValue, int typeID)
        {
            int tahun = (HttpContext.Current.Session["tahun"] == null) ? DateTime.Now.Year : int.Parse(HttpContext.Current.Session["tahun"].ToString());
            string kodelama = (tahun < 2014) ? " LEN(ItemCode)<12" : " Aktif=1 ";
            string strTypeID = string.Empty;
            strTypeID = " and A.ItemTypeID =" + typeID;
            string strSQL = "select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,"+
                            "A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,"+
                            "A.LastModifiedTime,A.GroupID,A.ItemTypeID,A.aktif,0 as Stock,A.AMgroupID,A.AMclassID,A.AMsubClassID,A.AMlokasiID from Asset as A,UOM as C where  A.aktif =1 and A.RowStatus>-1 and " +
                            "A.UOMID = C.ID and " + strField + " like '%" + strValue + "%' " + strTypeID;
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

        public static List<Asset> GetItemAsset(string item)
        {
            List<Asset> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "SELECT top 20 * FROM Asset Where ItemName like '" + item + "%'";

                    AllData = connection.Query<Asset>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

    }
}
