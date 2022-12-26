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
    public class TabelConversiFacade : AbstractTransactionFacade
    {
        private TabelConversi objTabelConversi = new TabelConversi();
        private ArrayList arrTabelConversi;
        private List<SqlParameter> sqlListParam;

        public TabelConversiFacade(object objDomain)
            : base(objDomain)
        {
            objTabelConversi = (TabelConversi)objDomain;
        }

        public TabelConversiFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ConversiNo", objTabelConversi.ConversiNo));
                sqlListParam.Add(new SqlParameter("@FromItemID", objTabelConversi.FromItemID));
                sqlListParam.Add(new SqlParameter("@FromQty", objTabelConversi.FromQty));
                sqlListParam.Add(new SqlParameter("@FromUomID", objTabelConversi.FromUomID));
                sqlListParam.Add(new SqlParameter("@ToItemID", objTabelConversi.ToItemID));
                sqlListParam.Add(new SqlParameter("@ToQty", objTabelConversi.ToQty));
                sqlListParam.Add(new SqlParameter("@ToUomID", objTabelConversi.ToUomID));
                sqlListParam.Add(new SqlParameter("@RowStatus", objTabelConversi.RowStatus));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objTabelConversi.CreatedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertTabelConversi");

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
                sqlListParam.Add(new SqlParameter("@ID", objTabelConversi.ID));
                sqlListParam.Add(new SqlParameter("@ConversiNo", objTabelConversi.ConversiNo));
                sqlListParam.Add(new SqlParameter("@FromItemID", objTabelConversi.FromItemID));
                sqlListParam.Add(new SqlParameter("@FromQty", objTabelConversi.FromQty));
                sqlListParam.Add(new SqlParameter("@FromUomID", objTabelConversi.FromUomID));
                sqlListParam.Add(new SqlParameter("@ToItemID", objTabelConversi.ToItemID));
                sqlListParam.Add(new SqlParameter("@ToQty", objTabelConversi.ToQty));
                sqlListParam.Add(new SqlParameter("@ToUomID", objTabelConversi.ToUomID));
                sqlListParam.Add(new SqlParameter("@RowStatus", objTabelConversi.RowStatus));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objTabelConversi.CreatedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateTabelConversi");

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
                sqlListParam.Add(new SqlParameter("@ID", objTabelConversi.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objTabelConversi.LastModifiedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spCancelTabelConversi");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from TabelConversi as A where A.RowStatus>-1 order by ID");
            strError = dataAccess.Error;
            arrTabelConversi = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTabelConversi.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTabelConversi.Add(new TabelConversi());

            return arrTabelConversi;
        }

        public ArrayList RetrieveOpenStatus()
        {
            string strSQL = "select FromItemName,ToItemName,ID,ConversiNo,FromItemID,FromQty,FromUomID,ToItemID,ToQty,ToUomID,CreatedBy,FromItemCode,ToItemCode,FromUomCode,ToUomCode, " +
                "isnull(Jumlah,0) as Jumlah from ( " +
                "select ID,ConversiNo,FromItemID,FromQty,FromUomID,ToItemID,ToQty,ToUomID,CreatedBy, " +
                "case when FromItemID>0 then (select ItemCode From Inventory where TabelConversi.FromItemID=Inventory.ID) else '' end FromItemCode," +
                "case when FromItemID>0 then (select ItemName From Inventory where TabelConversi.FromItemID=Inventory.ID) else '' end FromItemName," +
                "case when ToItemID>0 then (select ItemCode From Inventory where TabelConversi.ToItemID=Inventory.ID) else '' end ToItemCode," +
                "case when ToItemID>0 then (select ItemName From Inventory where TabelConversi.ToItemID=Inventory.ID) else '' end ToItemName," +
                "case when FromUomID>0 then (select UOMCode From UOM where TabelConversi.FromUomID=UOM.ID) else '' end FromUomCode, " +
                "case when ToUomID>0 then (select UOMCode From UOM where TabelConversi.ToUomID=UOM.ID) else '' end ToUomCode, " +
                "case when FromQty>0 then (select top 1 Jumlah from InventoryRepack where InventoryRepack.ItemID=TabelConversi.FromItemID) else 0 end Jumlah  " +
                "from TabelConversi where RowStatus>-1) as A order by ConversiNo desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrTabelConversi = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTabelConversi.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTabelConversi.Add(new TabelConversi());

            return arrTabelConversi;
        }

        public ArrayList RetrieveOpenStatusByNo(string conversiNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select  FromItemName,ToItemName,ID,ConversiNo,FromItemID,FromQty,FromUomID,ToItemID,ToQty,ToUomID,CreatedBy,FromItemCode,ToItemCode,FromUomCode,ToUomCode," +
                "isnull(Jumlah,0) as Jumlah from ("+
                "select ID,ConversiNo,FromItemID,FromQty,FromUomID,ToItemID,ToQty,ToUomID,CreatedBy, " +
               "case when FromItemID>0 then (select ItemCode From Inventory where TabelConversi.FromItemID=Inventory.ID) else '' end FromItemCode," +
                "case when FromItemID>0 then (select ItemName From Inventory where TabelConversi.FromItemID=Inventory.ID) else '' end FromItemName," +
                "case when ToItemID>0 then (select ItemCode From Inventory where TabelConversi.ToItemID=Inventory.ID) else '' end ToItemCode," +
                "case when ToItemID>0 then (select ItemName From Inventory where TabelConversi.ToItemID=Inventory.ID) else '' end ToItemName," +
                "case when FromUomID>0 then (select UOMCode From UOM where TabelConversi.FromUomID=UOM.ID) else '' end FromUomCode," +
                "case when ToUomID>0 then (select UOMCode From UOM where TabelConversi.ToUomID=UOM.ID) else '' end ToUomCode, " +
                "case when FromQty>0 then (select Jumlah from InventoryRepack where InventoryRepack.ItemID=TabelConversi.FromItemID) else 0 end Jumlah " +
                "from TabelConversi where RowStatus>-1 and  ConversiNo='" + conversiNo + "' ) as A order by ConversiNo desc");
            strError = dataAccess.Error;
            arrTabelConversi = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTabelConversi.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrTabelConversi.Add(new TabelConversi());

            return arrTabelConversi;
        }

        public TabelConversi RetrieveByNo(string conversiNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select  ID,ConversiNo,FromItemID,FromQty,FromUomID,ToItemID,ToQty,ToUomID,CreatedBy," +
                "case when FromItemID>0 then (select ItemCode From Inventory where TabelConversi.FromItemID=Inventory.ID) else '' end FromItemCode," +
                "case when FromItemID>0 then (select ItemName From Inventory where TabelConversi.FromItemID=Inventory.ID) else '' end FromItemName," +
                "case when ToItemID>0 then (select ItemCode From Inventory where TabelConversi.ToItemID=Inventory.ID) else '' end ToItemCode," +
                "case when ToItemID>0 then (select ItemName From Inventory where TabelConversi.ToItemID=Inventory.ID) else '' end ToItemName," +
                "case when FromUomID>0 then (select UOMCode From UOM where TabelConversi.FromUomID=UOM.ID) else '' end FromUomCode," +
                "case when ToUomID>0 then (select UOMCode From UOM where TabelConversi.ToUomID=UOM.ID) else '' end ToUomCode, " +
                "case when FromQty>0 then (select Jumlah from InventoryRepack where InventoryRepack.ItemID=TabelConversi.FromItemID) else 0 end Jumlah " +
                "from TabelConversi where RowStatus>-1 and ConversiNo='" + conversiNo + "'");
            strError = dataAccess.Error;
            arrTabelConversi = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new TabelConversi();
        }

        public ArrayList RetrieveByNo2(string conversiNo)
        {
            string strSQL = "select  ID,ConversiNo,FromItemID,FromQty,FromUomID,ToItemID,ToQty,ToUomID,CreatedBy," +
                "case when FromItemID>0 then (select ItemCode From Inventory where TabelConversi.FromItemID=Inventory.ID) else '' end FromItemCode," +
                "case when FromItemID>0 then (select ItemName From Inventory where TabelConversi.FromItemID=Inventory.ID) else '' end FromItemName," +
                "case when ToItemID>0 then (select ItemCode From Inventory where TabelConversi.ToItemID=Inventory.ID) else '' end ToItemCode," +
                "case when ToItemID>0 then (select ItemName From Inventory where TabelConversi.ToItemID=Inventory.ID) else '' end ToItemName," +
                "case when FromUomID>0 then (select UOMCode From UOM where TabelConversi.FromUomID=UOM.ID) else '' end FromUomCode," +
                "case when ToUomID>0 then (select UOMCode From UOM where TabelConversi.ToUomID=UOM.ID) else '' end ToUomCode, " +
                "case when FromQty>0 then (select top 1 isnull(Jumlah,0) from InventoryRepack where InventoryRepack.ItemID=TabelConversi.FromItemID and InventoryRepack.UOMID=TabelConversi.FromUomID) else 0 end Jumlah " +
                "from TabelConversi where RowStatus>-1 and ConversiNo='" + conversiNo + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrTabelConversi = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTabelConversi.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTabelConversi.Add(new TabelConversi());

            return arrTabelConversi;
        }

        public TabelConversi CekTabel(int fromItemID,int fromUomID,int toItemID,int toUomID)
        {
            string strSQL = "select  ID,ConversiNo,FromItemID,FromQty,FromUomID,ToItemID,ToQty,ToUomID,CreatedBy," +
                "case when FromItemID>0 then (select ItemCode From Inventory where TabelConversi.FromItemID=Inventory.ID) else '' end FromItemCode," +
                "case when FromItemID>0 then (select ItemName From Inventory where TabelConversi.FromItemID=Inventory.ID) else '' end FromItemName," +
                "case when ToItemID>0 then (select ItemCode From Inventory where TabelConversi.ToItemID=Inventory.ID) else '' end ToItemCode," +
                "case when ToItemID>0 then (select ItemName From Inventory where TabelConversi.ToItemID=Inventory.ID) else '' end ToItemName," +
                "case when FromUomID>0 then (select UOMCode From UOM where TabelConversi.FromUomID=UOM.ID) else '' end FromUomCode," +
                "case when ToUomID>0 then (select UOMCode From UOM where TabelConversi.ToUomID=UOM.ID) else '' end ToUomCode, " +
                "case when FromQty>0 then (select isnull(sum(Jumlah),0) from InventoryRepack where InventoryRepack.ItemID=TabelConversi.FromItemID) else 0 end Jumlah " +
                "from TabelConversi where RowStatus>-1 and FromItemID=" + fromItemID + " and FromUomID=" + fromUomID + " and ToItemID=" + toItemID + " and ToUomID=" + toUomID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrTabelConversi = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new TabelConversi();
        }

        public TabelConversi GenerateObject(SqlDataReader sqlDataReader)
        {
            objTabelConversi = new TabelConversi();
            objTabelConversi.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objTabelConversi.ConversiNo = sqlDataReader["ConversiNo"].ToString();
            objTabelConversi.FromItemID = Convert.ToInt32(sqlDataReader["FromItemID"]);
            objTabelConversi.FromQty = Convert.ToDecimal(sqlDataReader["FromQty"]);
            objTabelConversi.FromUomID = Convert.ToInt32(sqlDataReader["FromUomID"]);
            objTabelConversi.ToItemID = Convert.ToInt32(sqlDataReader["ToItemID"]);
            objTabelConversi.ToQty = Convert.ToDecimal(sqlDataReader["ToQty"]);
            objTabelConversi.ToUomID = Convert.ToInt32(sqlDataReader["ToUomID"]);

            objTabelConversi.FromItemCode = sqlDataReader["FromItemCode"].ToString();
            objTabelConversi.ToItemCode = sqlDataReader["ToItemCode"].ToString();
            objTabelConversi.FromItemName = sqlDataReader["FromItemName"].ToString();
            objTabelConversi.ToItemName = sqlDataReader["ToItemName"].ToString();
            objTabelConversi.FromUomCode = sqlDataReader["FromUomCode"].ToString();
            objTabelConversi.ToUomCode = sqlDataReader["ToUomCode"].ToString();
            objTabelConversi.Jumlah = Convert.ToDecimal(sqlDataReader["Jumlah"]);

            objTabelConversi.CreatedBy = sqlDataReader["CreatedBy"].ToString();

            return objTabelConversi;
        }

        public TabelConversi GenerateObjectList(SqlDataReader sqlDataReader)
        {
            objTabelConversi = new TabelConversi();
            //objTabelConversi.ID = Convert.ToInt32(sqlDataReader["ID"]);
            //objTabelConversi.AdjustNo = sqlDataReader["AdjustNo"].ToString();
            //objTabelConversi.AdjustDate = Convert.ToDateTime(sqlDataReader["AdjustDate"]);
            //objTabelConversi.AdjustType = sqlDataReader["AdjustType"].ToString();
            //objTabelConversi.Status = Convert.ToInt32(sqlDataReader["Status"]);
            //objTabelConversi.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            //objTabelConversi.ItemCode = sqlDataReader["ItemCode"].ToString();
            //objTabelConversi.ItemName = sqlDataReader["ItemName"].ToString();
            //objTabelConversi.Quantity = Convert.ToDecimal(sqlDataReader["Quantity"]);

            return objTabelConversi;
        }

    }
}
