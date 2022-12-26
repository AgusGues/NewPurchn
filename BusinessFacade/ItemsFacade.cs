using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;


namespace BusinessFacade
{
    public class ItemsFacade : AbstractTransactionFacade 
    {
        private Items objItems = new Items();
        private ArrayList arrItems;
        private List<SqlParameter> sqlListParam;

        public ItemsFacade(object objDomain)
            : base(objDomain)
        {
            objItems = (Items)objDomain;
        }

        public ItemsFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {                
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemCode", objItems.ItemCode));
                sqlListParam.Add(new SqlParameter("@Description", objItems.Description));
                sqlListParam.Add(new SqlParameter("@GroupID", objItems.GroupID));
                sqlListParam.Add(new SqlParameter("@ShortKey", objItems.ShortKey));
                sqlListParam.Add(new SqlParameter("@GradeID", objItems.GradeID));
                sqlListParam.Add(new SqlParameter("@SisiID", objItems.SisiID));
                sqlListParam.Add(new SqlParameter("@ItemType", objItems.ItemType));
                sqlListParam.Add(new SqlParameter("@Tebal", objItems.Tebal)); ;
                sqlListParam.Add(new SqlParameter("@Panjang", objItems.Panjang));
                sqlListParam.Add(new SqlParameter("@Lebar", objItems.Lebar));
                sqlListParam.Add(new SqlParameter("@UOMID", objItems.UOMID));
                sqlListParam.Add(new SqlParameter("@Berat", objItems.Berat));
                sqlListParam.Add(new SqlParameter("@Ket1", objItems.Ket1));
                sqlListParam.Add(new SqlParameter("@Ket2", objItems.Ket2));
                sqlListParam.Add(new SqlParameter("@Utuh", objItems.Utuh));
                sqlListParam.Add(new SqlParameter("@Paket", objItems.Paket));
                sqlListParam.Add(new SqlParameter("@GroupCategory", objItems.GroupCategory));
                sqlListParam.Add(new SqlParameter("@IsQuota", objItems.IsQuota));
                sqlListParam.Add(new SqlParameter("@OtherType", objItems.OtherType));
                sqlListParam.Add(new SqlParameter("@Head", objItems.Head));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objItems.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertItems");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Update(TransactionManager transManager)
        {
            try
            {                
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objItems.ID));
                sqlListParam.Add(new SqlParameter("@ItemCode", objItems.ItemCode));
                sqlListParam.Add(new SqlParameter("@Description", objItems.Description));
                sqlListParam.Add(new SqlParameter("@GroupID", objItems.GroupID));
                sqlListParam.Add(new SqlParameter("@ShortKey", objItems.ShortKey));
                sqlListParam.Add(new SqlParameter("@GradeID", objItems.GradeID));
                sqlListParam.Add(new SqlParameter("@SisiID", objItems.SisiID));
                sqlListParam.Add(new SqlParameter("@ItemType", objItems.ItemType));
                sqlListParam.Add(new SqlParameter("@Tebal", objItems.Tebal)); ;
                sqlListParam.Add(new SqlParameter("@Panjang", objItems.Panjang));
                sqlListParam.Add(new SqlParameter("@Lebar", objItems.Lebar));
                sqlListParam.Add(new SqlParameter("@UOMID", objItems.UOMID));
                sqlListParam.Add(new SqlParameter("@Berat", objItems.Berat));
                sqlListParam.Add(new SqlParameter("@Ket1", objItems.Ket1));
                sqlListParam.Add(new SqlParameter("@Ket2", objItems.Ket2));
                sqlListParam.Add(new SqlParameter("@Utuh", objItems.Utuh));
                sqlListParam.Add(new SqlParameter("@Paket", objItems.Paket));
                sqlListParam.Add(new SqlParameter("@GroupCategory", objItems.GroupCategory));
                sqlListParam.Add(new SqlParameter("@IsQuota", objItems.IsQuota));
                sqlListParam.Add(new SqlParameter("@OtherType", objItems.OtherType));
                sqlListParam.Add(new SqlParameter("@Head", objItems.Head));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objItems.CreatedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateItems");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Delete(TransactionManager transManager)
        {

            try
            {               
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objItems.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objItems.LastModifiedBy));
                
                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteItems");

                strError = transManager.Error;

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.Description,A.GroupID,B.GroupCode,B.GroupDescription,A.ShortKey,A.GradeID,C.GradeCode,A.SisiID,D.SisiDescription,A.ItemType,A.Tebal,A.Panjang,A.Lebar,A.UOMID,E.UOMCode,A.Berat,A.Ket1,A.Ket2,A.Utuh,A.Paket,A.GroupCategory,A.IsQuota,A.OtherType,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.FlagReport from Items as A,Groups as B, Grades as C,Sisi as D,UOM as E where A.GroupID = B.ID and A.GradeID = C.ID and A.SisiID = D.ID and A.UOMID = E.ID and A.RowStatus = 0 order by A.GroupID");
            strError = dataAccess.Error;
            arrItems = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrItems.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrItems.Add(new Items());

            return arrItems;
        }


        public Items RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.Description,A.GroupID,B.GroupCode,B.GroupDescription,A.ShortKey,A.GradeID,C.GradeCode,A.SisiID,D.SisiDescription,A.ItemType,A.Tebal,A.Panjang,A.Lebar,A.UOMID,E.UOMCode,A.Berat,A.Ket1,A.Ket2,A.Utuh,A.Paket,A.GroupCategory,A.IsQuota,A.OtherType,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.FlagReport from Items as A,Groups as B, Grades as C,Sisi as D,UOM as E where A.GroupID = B.ID and A.GradeID = C.ID and A.SisiID = D.ID and A.UOMID = E.ID and A.RowStatus = 0 and A.ID = " + Id);
            strError = dataAccess.Error;
            arrItems = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Items();
        }

        public Items RetrieveByDesc(string strDescription)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.Description,A.GroupID,B.GroupCode,B.GroupDescription,A.ShortKey,A.GradeID,C.GradeCode,A.SisiID,D.SisiDescription,A.ItemType,A.Tebal,A.Panjang,A.Lebar,A.UOMID,E.UOMCode,A.Berat,A.Ket1,A.Ket2,A.Utuh,A.Paket,A.GroupCategory,A.IsQuota,A.OtherType,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.FlagReport from Items as A,Groups as B, Grades as C,Sisi as D,UOM as E where A.GroupID = B.ID and A.GradeID = C.ID and A.SisiID = D.ID and A.UOMID = E.ID and A.RowStatus = 0 and A.Description = '" + strDescription + "'");
            strError = dataAccess.Error;
            arrItems = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Items();
        }

         public Items RetrieveByGroupCode(string groupCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.Description,A.GroupID,B.GroupCode,B.GroupDescription,A.ShortKey,A.GradeID,C.GradeCode,A.SisiID,D.SisiDescription,A.ItemType,A.Tebal,A.Panjang,A.Lebar,A.UOMID,E.UOMCode,A.Berat,A.Ket1,A.Ket2,A.Utuh,A.Paket,A.GroupCategory,A.IsQuota,A.OtherType,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.FlagReport from Items as A,Groups as B, Grades as C,Sisi as D,UOM as E where A.GroupID = B.ID and A.GradeID = C.ID and A.SisiID = D.ID and A.UOMID = E.ID and A.RowStatus = 0 and A.GroupCategory  = '" + groupCode + "' and A.Head = 1");
            strError = dataAccess.Error;
            arrItems = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Items();
        }


        public Items RetrieveByShortKey(string shortKey,int itemID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.Description,A.GroupID,B.GroupCode,B.GroupDescription,A.ShortKey,A.GradeID,C.GradeCode,A.SisiID,D.SisiDescription,A.ItemType,A.Tebal,A.Panjang,A.Lebar,A.UOMID,E.UOMCode,A.Berat,A.Ket1,A.Ket2,A.Utuh,A.Paket,A.GroupCategory,A.IsQuota,A.OtherType,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.FlagReport from Items as A,Groups as B, Grades as C,Sisi as D,UOM as E where A.GroupID = B.ID and A.GradeID = C.ID and A.SisiID = D.ID and A.UOMID = E.ID and A.RowStatus = 0 and A.ShortKey = '" + shortKey + "' and A.ID not in (" + itemID + ")");
            strError = dataAccess.Error;
            arrItems = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Items();
        }

        public ArrayList RetrieveByGroupID(int groupId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.Description,A.GroupID,B.GroupCode,B.GroupDescription,A.ShortKey,A.GradeID,C.GradeCode,A.SisiID,D.SisiDescription,A.ItemType,A.Tebal,A.Panjang,A.Lebar,A.UOMID,E.UOMCode,A.Berat,A.Ket1,A.Ket2,A.Utuh,A.Paket,A.GroupCategory,A.IsQuota,A.OtherType,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.FlagReport from Items as A,Groups as B, Grades as C,Sisi as D,UOM as E where A.GroupID = B.ID and A.GradeID = C.ID and A.SisiID = D.ID and A.UOMID = E.ID and A.RowStatus = 0 and A.GroupID = " + groupId + " order by A.Description");
            strError = dataAccess.Error;
            arrItems = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrItems.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrItems.Add(new Items());

            return arrItems;
        }


        public ArrayList RetrieveByGroupID(int groupId,int itemType)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.Description,A.GroupID,B.GroupCode,B.GroupDescription,A.ShortKey,A.GradeID,C.GradeCode,A.SisiID,D.SisiDescription,A.ItemType,A.Tebal,A.Panjang,A.Lebar,A.UOMID,E.UOMCode,A.Berat,A.Ket1,A.Ket2,A.Utuh,A.Paket,A.GroupCategory,A.IsQuota,A.OtherType,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.FlagReport from Items as A,Groups as B, Grades as C,Sisi as D,UOM as E where A.GroupID = B.ID and A.GradeID = C.ID and A.SisiID = D.ID and A.UOMID = E.ID and A.RowStatus = 0 and A.GroupID = " + groupId + " and A.ItemType = " + itemType + " order by A.Description");
            strError = dataAccess.Error;
            arrItems = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrItems.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrItems.Add(new Items());

            return arrItems;
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.Description,A.GroupID,B.GroupCode,B.GroupDescription,A.ShortKey,A.GradeID,C.GradeCode,A.SisiID,D.SisiDescription,A.ItemType,A.Tebal,A.Panjang,A.Lebar,A.UOMID,E.UOMCode,A.Berat,A.Ket1,A.Ket2,A.Utuh,A.Paket,A.GroupCategory,A.IsQuota,A.OtherType,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.FlagReport from Items as A,Groups as B, Grades as C,Sisi as D,UOM as E where A.GroupID = B.ID and A.GradeID = C.ID and A.SisiID = D.ID and A.UOMID = E.ID and A.RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrItems = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrItems.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrItems.Add(new Items());

            return arrItems;
        }

        public int HitungItem(int intGroupID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select count(ItemCode) as JumItem from Items where GroupID = " + intGroupID);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["JumItem"]);
                }
            }

            return 0;
        }

        public Items GenerateObject(SqlDataReader sqlDataReader)
        {
            objItems = new Items();
            objItems.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objItems.ItemCode = sqlDataReader["ItemCode"].ToString();
            objItems.Description = sqlDataReader["Description"].ToString();
            objItems.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objItems.GroupCode = sqlDataReader["GroupCode"].ToString();
            objItems.GroupDescription = sqlDataReader["GroupDescription"].ToString();
            objItems.ShortKey = sqlDataReader["ShortKey"].ToString();
            objItems.GradeID = Convert.ToInt32(sqlDataReader["GradeID"]);
            objItems.GradeCode = sqlDataReader["GradeCode"].ToString();
            objItems.SisiID = Convert.ToInt32(sqlDataReader["SisiID"]);
            objItems.SisiDescription = sqlDataReader["SisiDescription"].ToString();
            objItems.ItemType = Convert.ToInt32(sqlDataReader["ItemType"]);
            objItems.Tebal = Convert.ToDecimal(sqlDataReader["Tebal"]);
            objItems.Panjang = Convert.ToDecimal(sqlDataReader["Panjang"]);
            objItems.Lebar = Convert.ToDecimal(sqlDataReader["Lebar"]);
            objItems.UOMID = Convert.ToInt32(sqlDataReader["UOMID"]);
            objItems.UOMCode = sqlDataReader["UOMCode"].ToString();
            objItems.Berat = Convert.ToDecimal(sqlDataReader["Berat"]);
            objItems.Ket1 = sqlDataReader["Ket1"].ToString();
            objItems.Ket2 = sqlDataReader["Ket2"].ToString();
            objItems.Utuh = Convert.ToInt32(sqlDataReader["Utuh"]);
            objItems.Paket = Convert.ToInt32(sqlDataReader["Paket"]);
            objItems.GroupCategory = sqlDataReader["GroupCategory"].ToString();
            objItems.IsQuota = Convert.ToInt32(sqlDataReader["IsQuota"]);
            objItems.OtherType = Convert.ToInt32(sqlDataReader["OtherType"]);
            objItems.Head = Convert.ToInt32(sqlDataReader["Head"]);
            objItems.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objItems.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objItems.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objItems.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objItems.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            // add flagreport
            objItems.FlagReport = Convert.ToInt32(sqlDataReader["FlagReport"]);

            return objItems;

        }

       
    }
}

