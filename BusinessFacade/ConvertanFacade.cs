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
    public class ConvertanFacade : AbstractTransactionFacade
    {
        private Convertan objConvertan = new Convertan();
        private ArrayList arrConvertan;
        private List<SqlParameter> sqlListParam;

        public ConvertanFacade(object objDomain)
            : base(objDomain)
        {
            objConvertan = (Convertan)objDomain;
        }

        public ConvertanFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@RepackNo", objConvertan.RepackNo));
                sqlListParam.Add(new SqlParameter("@FromItemID", objConvertan.FromItemID));
                sqlListParam.Add(new SqlParameter("@FromQty", objConvertan.FromQty));
                sqlListParam.Add(new SqlParameter("@FromUomID", objConvertan.FromUomID));
                sqlListParam.Add(new SqlParameter("@ToItemID", objConvertan.ToItemID));
                sqlListParam.Add(new SqlParameter("@ToQty", objConvertan.ToQty));
                sqlListParam.Add(new SqlParameter("@ToUomID", objConvertan.ToUomID));
                sqlListParam.Add(new SqlParameter("@RowStatus", objConvertan.RowStatus));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objConvertan.CreatedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertConvertan");

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
                sqlListParam.Add(new SqlParameter("@ID", objConvertan.ID));
                sqlListParam.Add(new SqlParameter("@RepackNo", objConvertan.RepackNo));
                sqlListParam.Add(new SqlParameter("@FromItemID", objConvertan.FromItemID));
                sqlListParam.Add(new SqlParameter("@FromQty", objConvertan.FromQty));
                sqlListParam.Add(new SqlParameter("@FromUomID", objConvertan.FromUomID));

                sqlListParam.Add(new SqlParameter("@ToItemID", objConvertan.ToItemID));
                sqlListParam.Add(new SqlParameter("@ToQty", objConvertan.ToQty));
                sqlListParam.Add(new SqlParameter("@ToUomID", objConvertan.ToUomID));
                sqlListParam.Add(new SqlParameter("@RowStatus", objConvertan.RowStatus));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objConvertan.CreatedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateConvertan");

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
                sqlListParam.Add(new SqlParameter("@ID", objConvertan.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objConvertan.LastModifiedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spCancelConvertan");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from Convertan as A where A.RowStatus>-1 order by ID");
            strError = dataAccess.Error;
            arrConvertan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrConvertan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrConvertan.Add(new Convertan());

            return arrConvertan;
        }

        public ArrayList RetrieveOpenStatus()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select ID,RepackNo,FromItemID,FromQty,FromUomID,ToItemID,ToQty,ToUomID,CreatedBy, " +
                "case when FromItemID>0 then (select ItemCode From Inventory where TabelConversi.FromItemID=Inventory.ID) else '' end FromItemCode," +
                "case when ToItemID>0 then (select ItemCode From Inventory where TabelConversi.ToItemID=Inventory.ID) else '' end ToItemCode," +
                "case when FromUomID>0 then (select UOMCode From UOM where TabelConversi.FromUomID=UOM.ID) else '' end FromUomCode," +
                "case when ToUomID>0 then (select UOMCode From UOM where TabelConversi.ToUomID=UOM.ID) else '' end ToUomCode " +
                "from Convertan where RowStatus>-1 order by RepackNo desc");
            strError = dataAccess.Error;
            arrConvertan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrConvertan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrConvertan.Add(new Convertan());

            return arrConvertan;
        }

        public ArrayList RetrieveOpenStatusByNo(string RepackNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select ID,RepackNo,FromItemID,FromQty,FromUomID,ToItemID,ToQty,ToUomID,CreatedBy, " +
                "case when FromItemID>0 then (select ItemCode From Inventory where TabelConversi.FromItemID=Inventory.ID) else '' end FromItemCode," +
                "case when ToItemID>0 then (select ItemCode From Inventory where TabelConversi.ToItemID=Inventory.ID) else '' end ToItemCode," +
                "case when FromUomID>0 then (select UOMCode From UOM where TabelConversi.FromUomID=UOM.ID) else '' end FromUomCode," +
                "case when ToUomID>0 then (select UOMCode From UOM where TabelConversi.ToUomID=UOM.ID) else '' end ToUomCode " +
                "from Convertan where RowStatus>-1 and  RepackNo='" + RepackNo + "' order by RepackNo desc");
            strError = dataAccess.Error;
            arrConvertan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrConvertan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrConvertan.Add(new Convertan());

            return arrConvertan;
        }

        public Convertan RetrieveByNo(string RepackNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select ID,RepackNo,FromItemID,FromQty,FromUomID,ToItemID,ToQty,ToUomID,CreatedBy," +
                "case when FromItemID>0 then (select ItemCode From Inventory where TabelConversi.FromItemID=Inventory.ID) else '' end FromItemCode," +
                "case when ToItemID>0 then (select ItemCode From Inventory where TabelConversi.ToItemID=Inventory.ID) else '' end ToItemCode," +
                "case when FromUomID>0 then (select UOMCode From UOM where TabelConversi.FromUomID=UOM.ID) else '' end FromUomCode," +
                "case when ToUomID>0 then (select UOMCode From UOM where TabelConversi.ToUomID=UOM.ID) else '' end ToUomCode " +
                "from Convertan where RowStatus>-1 and RepackNo='" + RepackNo + "'");
            strError = dataAccess.Error;
            arrConvertan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Convertan();
        }

        public ArrayList RetrieveByNo2(string RepackNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select ID,RepackNo,FromItemID,FromQty,FromUomID,ToItemID,ToQty,ToUomID,CreatedBy," +
                "case when FromItemID>0 then (select ItemCode From Inventory where TabelConversi.FromItemID=Inventory.ID) else '' end FromItemCode," +
                "case when ToItemID>0 then (select ItemCode From Inventory where TabelConversi.ToItemID=Inventory.ID) else '' end ToItemCode," +
                "case when FromUomID>0 then (select UOMCode From UOM where TabelConversi.FromUomID=UOM.ID) else '' end FromUomCode," +
                "case when ToUomID>0 then (select UOMCode From UOM where TabelConversi.ToUomID=UOM.ID) else '' end ToUomCode " +
                "from Convertan where RowStatus>-1 and RepackNo='" + RepackNo + "'");
            strError = dataAccess.Error;
            arrConvertan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrConvertan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrConvertan.Add(new Convertan());

            return arrConvertan;
        }

        public Convertan RetrieveByNo3(string RepackNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select ID,RepackNo,FromItemID,FromQty,FromUomID,ToItemID,ToQty,ToUomID,CreatedBy, " +
                                        "case when FromItemID>0 then (select ItemCode From Inventory where Convertan.FromItemID=Inventory.ID) else '' end FromItemCode, " +
                                        "case when ToItemID>0 then (select ItemCode From Inventory where Convertan.ToItemID=Inventory.ID) else '' end ToItemCode, " +
                                        "case when FromUomID>0 then (select UOMCode From UOM where Convertan.FromUomID=UOM.ID) else '' end FromUomCode, " +
                                        "case when ToUomID>0 then (select UOMCode From UOM where Convertan.ToUomID=UOM.ID) else '' end ToUomCode  " +
                                        "from Convertan where RowStatus>-1 and RepackNo= '" + RepackNo + "'");
            arrConvertan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Convertan();
        }

        public Convertan CekTabel(int fromItemID, int fromUomID, int toItemID, int toUomID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select ID,RepackNo,FromItemID,FromQty,FromUomID,ToItemID,ToQty,ToUomID,CreatedBy," +
                "case when FromItemID>0 then (select ItemCode From Inventory where TabelConversi.FromItemID=Inventory.ID) else '' end FromItemCode," +
                "case when ToItemID>0 then (select ItemCode From Inventory where TabelConversi.ToItemID=Inventory.ID) else '' end ToItemCode," +
                "case when FromUomID>0 then (select UOMCode From UOM where TabelConversi.FromUomID=UOM.ID) else '' end FromUomCode," +
                "case when ToUomID>0 then (select UOMCode From UOM where TabelConversi.ToUomID=UOM.ID) else '' end ToUomCode " +
                "from Convertan where RowStatus>-1 and FromItemID=" + fromItemID + " and FromUomID=" + fromUomID + " and ToItemID=" + toItemID + " and ToUomID=" + toUomID);
            strError = dataAccess.Error;
            arrConvertan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Convertan();
        }

        public ArrayList RetrieveByGroupIDwithSUM(int groupID, int itemType, string thbl)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select ToItemID,ISNULL(SUM(ToQty),0) as ToQty from Convertan where Convertan.RowStatus>-1 and LEFT(convert(varchar,Convertan.CreatedTime,112),6) = '"+thbl+"' group by ToItemID");

            strError = dataAccess.Error;
            arrConvertan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrConvertan.Add(GenerateObjectSUM(sqlDataReader));
                }
            }
            else
                arrConvertan.Add(new Convertan());

            return arrConvertan;
        }

        public ArrayList RetrieveBySamaTgl2(int tgl, int bln, int thn, int itemTypeID, int groupID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select A.RepackNo,A.CreatedTime,A.ToItemID,A.ToQty,A.ToUomID,A.RowStatus,B.ItemCode as ToItemCode,B.ItemName as ToItemName from convertan as A, Inventory as B " +
                    "where A.ToItemID=B.ID and DAY(A.CreatedTime)=" + tgl + " and MONTH(A.CreatedTime)=" + bln + " and YEAR(A.CreatedTime)=" + thn + " and A.RowStatus>-1";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrConvertan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrConvertan.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrConvertan.Add(new Convertan());

            return arrConvertan;
        }
        public Convertan GenerateObjectSUM(SqlDataReader sqlDataReader)
        {
            objConvertan = new Convertan();
            objConvertan.ToItemID = Convert.ToInt32(sqlDataReader["ToItemID"]);
            objConvertan.ToQty = Convert.ToDecimal(sqlDataReader["ToQty"]);
            //gak ada groupID

            return objConvertan;
        }

        public Convertan GenerateObject(SqlDataReader sqlDataReader)
        {
            objConvertan = new Convertan();
            objConvertan.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objConvertan.RepackNo = sqlDataReader["RepackNo"].ToString();
            objConvertan.FromItemID = Convert.ToInt32(sqlDataReader["FromItemID"]);
            objConvertan.FromQty = Convert.ToDecimal(sqlDataReader["FromQty"]);
            objConvertan.FromUomID = Convert.ToInt32(sqlDataReader["FromUomID"]);
            objConvertan.ToItemID = Convert.ToInt32(sqlDataReader["ToItemID"]);
            objConvertan.ToQty = Convert.ToDecimal(sqlDataReader["ToQty"]);
            objConvertan.ToUomID = Convert.ToInt32(sqlDataReader["ToUomID"]);

            objConvertan.FromItemCode = sqlDataReader["FromItemCode"].ToString();
            objConvertan.ToItemCode = sqlDataReader["ToItemCode"].ToString();
            objConvertan.FromUomCode = sqlDataReader["FromUomCode"].ToString();
            objConvertan.ToUomCode = sqlDataReader["ToUomCode"].ToString();

            objConvertan.CreatedBy = sqlDataReader["CreatedBy"].ToString();

            return objConvertan;
        }

        public Convertan GenerateObject2(SqlDataReader sqlDataReader)
        {
            objConvertan = new Convertan();
            objConvertan.RepackNo = sqlDataReader["RepackNo"].ToString();
            objConvertan.ToItemID = Convert.ToInt32(sqlDataReader["ToItemID"]);
            objConvertan.ToQty = Convert.ToDecimal(sqlDataReader["ToQty"]);
            objConvertan.ToUomID = Convert.ToInt32(sqlDataReader["ToUomID"]);
            objConvertan.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objConvertan.ToItemCode = sqlDataReader["ToItemCode"].ToString();
            objConvertan.ToItemName = sqlDataReader["ToItemName"].ToString();

            return objConvertan;
        }
    }
}
