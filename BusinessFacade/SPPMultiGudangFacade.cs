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
    public class SPPMultiGudangFacade : AbstractTransactionFacade
    {
        private SPPMultiGudang objSPPMultiGudang = new SPPMultiGudang();
        // ArrayList arrSPPPrivate;
        private List<SqlParameter> sqlListParam;

        public SPPMultiGudangFacade(object objDomain)
            : base(objDomain)
        {
            objSPPMultiGudang = (SPPMultiGudang)objDomain;
        }
        public SPPMultiGudangFacade()
        {
        }
        public override int Insert(DataAccessLayer.TransactionManager transManager)
        {
            try
            {
                objSPPMultiGudang = (SPPMultiGudang)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@SPPID", objSPPMultiGudang.SPPID));
                sqlListParam.Add(new SqlParameter("@GudangID", objSPPMultiGudang.GudangID));
                sqlListParam.Add(new SqlParameter("@ItemID", objSPPMultiGudang.ItemID));
                sqlListParam.Add(new SqlParameter("@GroupID", objSPPMultiGudang.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objSPPMultiGudang.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@QtyReceipt", objSPPMultiGudang.QtyReceipt));
                sqlListParam.Add(new SqlParameter("@QtyPakai", objSPPMultiGudang.QtyPakai));
                sqlListParam.Add(new SqlParameter("@Aktif", objSPPMultiGudang.Aktif));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objSPPMultiGudang.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertSPPMultiGudang");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Update(DataAccessLayer.TransactionManager transManager)
        {
            try
            {
                objSPPMultiGudang = (SPPMultiGudang)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@GudangID", objSPPMultiGudang.GudangID));
                sqlListParam.Add(new SqlParameter("@ItemID", objSPPMultiGudang.ItemID));
                sqlListParam.Add(new SqlParameter("@GroupID", objSPPMultiGudang.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objSPPMultiGudang.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@QtyPakai", objSPPMultiGudang.QtyPakai));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objSPPMultiGudang.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateSPPMultiGudang");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Delete(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override System.Collections.ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }
        public SPPMultiGudang RetrieveById(int DeptId,int itemID)
        {
            string strSQL = "Select * from sppmultigudang where aktif = 1 and gudangId = " + DeptId + " and itemID=" + itemID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new SPPMultiGudang();
        }

        public decimal RetrieveByStock(int DeptId, int itemID,int groupID,int itemtypeid)
        {
            string strSQL = "Select isnull(SUM(QtyReceipt-QtyPakai),0) as stock from sppmultigudang where aktif = 1 and gudangId <> " +
                DeptId + " and itemID=" + itemID + " /*and groupID=" + groupID + " */and itemtypeid=" + itemtypeid;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            decimal  stock = 0;
            try
            {
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        stock = Convert.ToDecimal(sqlDataReader["stock"]);
                    }
                }
            }
            catch { }
            return stock;
        }

        //tambahan agus 11-05-2022
        public decimal RetrieveByStock1(int itemID, int groupID, int itemtypeid)
        {
            string strSQL = "Select isnull(SUM(QtyReceipt-QtyPakai),0) as stock from sppmultigudang where aktif = 1 and gudangId not in(4,5,18) and itemID=" + itemID + " /*and groupID=" + groupID + " */and itemtypeid=" + itemtypeid;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            decimal stock = 0;
            try
            {
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        stock = Convert.ToDecimal(sqlDataReader["stock"]);
                    }
                }
            }
            catch { }
            return stock;
        }
        //tambahan agus 11-05-2022

        public SPPMultiGudang GenerateObject(SqlDataReader sqlDataReader)
        {
            //  ID, SPPID, GudangID, ItemID, GroupID, ItemTypeID, QtyReceipt, QtyPakai, Akti
            objSPPMultiGudang = new SPPMultiGudang();
            objSPPMultiGudang.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objSPPMultiGudang.SPPID = Convert.ToInt32(sqlDataReader["SPPID"]);
            objSPPMultiGudang.GudangID = Convert.ToInt32(sqlDataReader["GudangID"]);
            objSPPMultiGudang.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objSPPMultiGudang.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objSPPMultiGudang.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"]);
            objSPPMultiGudang.QtyReceipt = Convert.ToDecimal(sqlDataReader["QtyReceipt"]);
            objSPPMultiGudang.QtyPakai = Convert.ToDecimal(sqlDataReader["QtyPakai"]);
            objSPPMultiGudang.Aktif = Convert.ToInt32(sqlDataReader["Aktif"]);
            return objSPPMultiGudang;
        }
    }
}
