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
    public class BiayaFacade : AbstractFacade
    {
        private Biaya objBiaya = new Biaya();
        private ArrayList arrBiaya;
        private List<SqlParameter> sqlListParam;

        public BiayaFacade()
            : base()
        {

        }


        public override int Insert(object objDomain)
        {
            try
            {
                objBiaya = (Biaya)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemCode", objBiaya.ItemCode));
                sqlListParam.Add(new SqlParameter("@ItemName", objBiaya.ItemName));
                sqlListParam.Add(new SqlParameter("@SupplierCode", objBiaya.SupplierCode));
                sqlListParam.Add(new SqlParameter("@UOMID", objBiaya.UOMID));
                sqlListParam.Add(new SqlParameter("@Jumlah", objBiaya.Jumlah));
                sqlListParam.Add(new SqlParameter("@Harga", objBiaya.Harga));
                sqlListParam.Add(new SqlParameter("@MinStock", objBiaya.MinStock));
                sqlListParam.Add(new SqlParameter("@DeptID", objBiaya.DeptID)); ;
                sqlListParam.Add(new SqlParameter("@RakID", objBiaya.RakID));
                sqlListParam.Add(new SqlParameter("@GroupID", objBiaya.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objBiaya.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@Gudang", objBiaya.Gudang));
                sqlListParam.Add(new SqlParameter("@ShortKey", objBiaya.ShortKey));
                sqlListParam.Add(new SqlParameter("@Keterangan", objBiaya.Keterangan));
                sqlListParam.Add(new SqlParameter("@Head", objBiaya.Head));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objBiaya.CreatedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objBiaya.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertBiaya");

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
                objBiaya = (Biaya)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objBiaya.ID));
                sqlListParam.Add(new SqlParameter("@ItemCode", objBiaya.ItemCode));
                sqlListParam.Add(new SqlParameter("@ItemName", objBiaya.ItemName));
                sqlListParam.Add(new SqlParameter("@SupplierCode", objBiaya.SupplierCode));
                sqlListParam.Add(new SqlParameter("@Satuan", objBiaya.Satuan));
                sqlListParam.Add(new SqlParameter("@UOMID", objBiaya.UOMID));
                sqlListParam.Add(new SqlParameter("@Jumlah", objBiaya.Jumlah));
                sqlListParam.Add(new SqlParameter("@Harga", objBiaya.Harga));
                sqlListParam.Add(new SqlParameter("@MinStock", objBiaya.MinStock));
                sqlListParam.Add(new SqlParameter("@DeptID", objBiaya.DeptID)); ;
                sqlListParam.Add(new SqlParameter("@RakID", objBiaya.RakID));
                sqlListParam.Add(new SqlParameter("@GroupID", objBiaya.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objBiaya.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@Gudang", objBiaya.Gudang));
                sqlListParam.Add(new SqlParameter("@ShortKey", objBiaya.ShortKey));
                sqlListParam.Add(new SqlParameter("@Keterangan", objBiaya.Keterangan));
                sqlListParam.Add(new SqlParameter("@Head", objBiaya.Head));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objBiaya.CreatedBy));
                sqlListParam.Add(new SqlParameter("@aktif", objBiaya.Aktif));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateBiaya");

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
                objBiaya = (Biaya)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objBiaya.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objBiaya.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteBiaya");

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
                objBiaya = (Biaya)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objBiaya.ID));
                sqlListParam.Add(new SqlParameter("@Jumlah", objBiaya.Jumlah));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateQtyBiaya");

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
                objBiaya = (Biaya)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objBiaya.ID));
                sqlListParam.Add(new SqlParameter("@Jumlah", objBiaya.Jumlah));

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
                objBiaya = (Biaya)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objBiaya.ID));
                sqlListParam.Add(new SqlParameter("@Jumlah", objBiaya.Jumlah));

                int intResult = dataAccess.ProcessData(sqlListParam, "spMinusQtyBiaya");

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
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from Biaya order by GroupID");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,A.aktif from Inventory as A,UOM as C where A.UOMID = C.ID order by GroupID");
            strError = dataAccess.Error;
            arrBiaya = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBiaya.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrBiaya.Add(new Biaya());

            return arrBiaya;
        }

        public ArrayList Retrieve2()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //gak pake DeptID
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,B.DeptName,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID from Inventory as A,Dept as B,UOM as C where A.DeptID = B.ID and A.UOMID = C.ID order by GroupID");
            string strSQL = "select top 50 A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID from biaya as A,UOM as C where A.UOMID = C.ID order by ID ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrBiaya = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBiaya.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrBiaya.Add(new Biaya());

            return arrBiaya;
        }
        public ArrayList Retrieve3()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //gak pake DeptID
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,B.DeptName,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID from Inventory as A,Dept as B,UOM as C where A.DeptID = B.ID and A.UOMID = C.ID order by GroupID");
            string strSQL = "select top 50 A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID from biaya as A,UOM as C where A.UOMID = C.ID and LEN(A.ItemCode)=15 order by ID ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrBiaya = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBiaya.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrBiaya.Add(new Biaya());

            return arrBiaya;
        }

        public Biaya RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from Biaya where Biaya.ID = " + Id);
            string strSQL = "select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode as UomCode,C.UOMDesc,"+
                            "A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan, "+
                            "A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,"+
                            "A.ItemTypeID,A.Stock from Biaya as A,UOM as C where A.UOMID = C.ID and A.ID = " + Id;
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrBiaya = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Biaya();
        }

        public Biaya RetrieveByName(string strDescription)
        {
            string strSQL = "select Biaya.*,Uom.UOMCode,Uom.UOMDesc from Biaya,Uom where Uom.ID=Biaya.UOMID and Biaya.ItemName = '" + strDescription.Replace("'","''") + "' and aktif=1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrBiaya = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Biaya();
        }

        public ArrayList RetrieveByGroupID(int groupId, int itemType)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.ItemName,A.GroupID,B.GroupCode,B.GroupDescription,A.ShortKey,A.UOMID,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.aktif from Biaya as A,GroupsPurchn as B, UOM as C where A.GroupID = B.ID and A.UOMID = C.ID and A.RowStatus = 0 and A.GroupID = " + groupId + " and A.ItemTypeID = " + itemType + " order by A.ItemName");
            strError = dataAccess.Error;
            arrBiaya = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBiaya.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrBiaya.Add(new Asset());

            return arrBiaya;
        }
        //new 27des
        public ArrayList RetrieveByCriteriaWithGroupID(string strField, string strValue, int groupID)
        {
            string strGroupID = string.Empty;
            if (groupID > 0)
                strGroupID = " and A.GroupID =" + groupID;
            string strSQL = "select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,"+
                            "A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,"+
                            "A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,A.aktif from Biaya as A,UOM as C "+
                            "where  A.aktif =1 and  A.RowStatus>-1 and A.UOMID = C.ID and " + strField + " like '%" + strValue + "%' " + strGroupID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrBiaya = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBiaya.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrBiaya.Add(new Biaya());

            return arrBiaya;
        }
        //new
        public Biaya RetrieveByCategoryID(string groupID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from Biaya where Biaya.groupID = '" + groupID + "'");
            strError = dataAccess.Error;
            arrBiaya = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Biaya();
        }


        //public Inventory RetrieveByShortKey(string shortKey,int itemID)
        //{
        //DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.Description,A.GroupID,B.GroupCode,B.GroupDescription,A.ShortKey,A.GradeID,C.GradeCode,A.SisiID,D.SisiDescription,A.ItemType,A.Tebal,A.Panjang,A.Lebar,A.UOMID,E.UOMCode,A.Berat,A.Ket1,A.Ket2,A.Utuh,A.Paket,A.GroupCategory,A.IsQuota,A.OtherType,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from Items as A,Groups as B, Grades as C,Sisi as D,UOM as E where A.GroupID = B.ID and A.GradeID = C.ID and A.SisiID = D.ID and A.UOMID = E.ID and A.RowStatus = 0 and A.ShortKey = '" + shortKey + "' and A.ID not in (" + itemID + ")");
        //strError = dataAccess.Error;
        //arrInventory = new ArrayList();

        //if (sqlDataReader.HasRows)
        //{
        //    while (sqlDataReader.Read())
        //    {
        //        return GenerateObject(sqlDataReader);
        //    }
        //}

        //return new Inventory();
        //}

        //public ArrayList RetrieveByGroupID(int groupId)
        //{
        //DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.Description,A.GroupID,B.GroupCode,B.GroupDescription,A.ShortKey,A.GradeID,C.GradeCode,A.SisiID,D.SisiDescription,A.ItemType,A.Tebal,A.Panjang,A.Lebar,A.UOMID,E.UOMCode,A.Berat,A.Ket1,A.Ket2,A.Utuh,A.Paket,A.GroupCategory,A.IsQuota,A.OtherType,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from Items as A,Groups as B, Grades as C,Sisi as D,UOM as E where A.GroupID = B.ID and A.GradeID = C.ID and A.SisiID = D.ID and A.UOMID = E.ID and A.RowStatus = 0 and A.GroupID = " + groupId + " order by A.Description");
        //strError = dataAccess.Error;
        //arrInventory = new ArrayList();

        //if (sqlDataReader.HasRows)
        //{
        //    while (sqlDataReader.Read())
        //    {
        //        arrInventory.Add(GenerateObject(sqlDataReader));
        //    }
        //}
        //else
        //    arrInventory.Add(new Inventory());

        //return arrInventory;
        //}


        //public ArrayList RetrieveByGroupID(int groupId,int itemType)
        //{
        //DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.Description,A.GroupID,B.GroupCode,B.GroupDescription,A.ShortKey,A.GradeID,C.GradeCode,A.SisiID,D.SisiDescription,A.ItemType,A.Tebal,A.Panjang,A.Lebar,A.UOMID,E.UOMCode,A.Berat,A.Ket1,A.Ket2,A.Utuh,A.Paket,A.GroupCategory,A.IsQuota,A.OtherType,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from Items as A,Groups as B, Grades as C,Sisi as D,UOM as E where A.GroupID = B.ID and A.GradeID = C.ID and A.SisiID = D.ID and A.UOMID = E.ID and A.RowStatus = 0 and A.GroupID = " + groupId + " and A.ItemType = " + itemType + " order by A.Description");
        //strError = dataAccess.Error;
        //arrInventory = new ArrayList();

        //if (sqlDataReader.HasRows)
        //{
        //    while (sqlDataReader.Read())
        //    {
        //        arrInventory.Add(GenerateObject(sqlDataReader));
        //    }
        //}
        //else
        //    arrInventory.Add(new Inventory());

        //return arrInventory;
        //}

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang," +
                "A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,A.aktif from biaya as A,UOM as C where A.UOMID = C.ID and " + strField + " like '%" + strValue + "%'");
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from Biaya where " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrBiaya = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBiaya.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrBiaya.Add(new Biaya());

            return arrBiaya;
        }

        public int CountItemCode(string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT MAX(SUBSTRING(ItemCode,7,4)) as id from Biaya where left(Biaya.ItemCode,5) = '" + strValue + "'");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT isnull(MAX(substring(itemcode,8,4)),0) as id from biaya where LEFT(itemcode,7) = '" + strValue + "'");
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

        public Biaya RetrieveRecordByID(int id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,A.aktif from Biaya as A,UOM as C where A.UOMID = C.ID and A.ID = '" + id + "'");
            strError = dataAccess.Error;
            arrBiaya = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Biaya();
        }

        

        public Biaya GenerateObject(SqlDataReader sqlDataReader)
        {
            objBiaya = new Biaya();
            objBiaya.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objBiaya.ItemCode = sqlDataReader["ItemCode"].ToString();
            objBiaya.ItemName = sqlDataReader["ItemName"].ToString();
            objBiaya.SupplierCode = sqlDataReader["SupplierCode"].ToString();
            objBiaya.UOMID = Convert.ToInt32(sqlDataReader["UOMID"]);
            objBiaya.UomCode = sqlDataReader["UomCode"].ToString();
            objBiaya.UOMDesc = sqlDataReader["UOMDesc"].ToString();
            objBiaya.Jumlah = Convert.ToDecimal(sqlDataReader["Jumlah"]);
            objBiaya.MaxStock = Convert.ToInt32(sqlDataReader["MaxStock"]);
            objBiaya.Harga = Convert.ToInt32(sqlDataReader["Harga"]);
            objBiaya.MinStock = Convert.ToInt32(sqlDataReader["MinStock"]);
            objBiaya.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objBiaya.RakID = Convert.ToInt32(sqlDataReader["RakID"]);
            objBiaya.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objBiaya.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"]);
            objBiaya.Gudang = Convert.ToInt32(sqlDataReader["Gudang"]);
            objBiaya.ShortKey = sqlDataReader["ShortKey"].ToString();
            objBiaya.Keterangan = sqlDataReader["Keterangan"].ToString();
            objBiaya.Head = Convert.ToInt32(sqlDataReader["Head"]);
            objBiaya.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objBiaya.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            //objInventory.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objBiaya.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            //objInventory.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objBiaya;

        }

        public ArrayList RetrieveByNameTypeID(string strField, string strValue, int typeID)
        {
            int tahun = (HttpContext.Current.Session["tahun"] == null) ? DateTime.Now.Year : int.Parse(HttpContext.Current.Session["tahun"].ToString());
            string kodelama = (tahun < 2014) ? " LEN(ItemCode)<12" : " Aktif=1 ";
            string strTypeID = string.Empty;
            strTypeID = " and A.ItemTypeID =" + typeID;
            string strSQL = "select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,"+
                            "A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,"+
                            "A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,A.aktif from Biaya as A,UOM as C where  A.aktif =1 and  "+
                            "A.RowStatus>-1 and A.UOMID = C.ID and " + strField + " like '%" + strValue + "%' " + strTypeID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrBiaya = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBiaya.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrBiaya.Add(new Inventory());

            return arrBiaya;
        }
        public int GetSatuanID(string UomCode)
        {
            int IDnya = 0;
            string strSQL = "select ID from UOM where UomCode='" + UomCode + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sda = dataAccess.RetrieveDataByString(strSQL);
            if (dataAccess.Error == string.Empty && sda.HasRows)
            {
                while (sda.Read())
                {
                    IDnya= Convert.ToInt32(sda["ID"].ToString());
                }
            }
            return IDnya;
        }

        public static List<Biaya> GetItemBiaya(string item)
        {
            List<Biaya> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "SELECT top 20 * FROM Biaya Where ItemName like '" + item + "%'";

                    AllData = connection.Query<Biaya>(query).ToList();
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
