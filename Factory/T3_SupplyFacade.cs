using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;
using BusinessFacade;

namespace Factory
{
    public class T3_SupplyFacade : BusinessFacade.AbstractTransactionFacadeF
    {
        private T3_Supply objT3_Supply = new T3_Supply();
        private ArrayList arrT3_Supply;
        private List<SqlParameter> sqlListParam;

        
        public T3_SupplyFacade(object objDomain)
            : base(objDomain)
        {
            objT3_Supply = (T3_Supply)objDomain;
        }
        public T3_SupplyFacade()
        {
        }
        public override int Insert1(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }
        public override int Update1(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }
        public override int Update2(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }
        public override int Update(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Delete(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }
        public override int Insert(TransactionManager transManager)
        {
            try
            {
                objT3_Supply = (T3_Supply)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@LokID", objT3_Supply.LokasiID ));
                sqlListParam.Add(new SqlParameter("@ItemID", objT3_Supply.ItemID));
                sqlListParam.Add(new SqlParameter("@TglTrans", objT3_Supply.TglTrans));
                sqlListParam.Add(new SqlParameter("@Qty", objT3_Supply.Qty ));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT3_Supply.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertT3_Supply");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        
        public override System.Collections.ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }
        public ArrayList RetrieveBytgl(string tgl)
        {
            string strSQL = "SELECT B.TglTrans , I2.PartNo AS Partno,L1.Lokasi ,B.Qty,B.CreatedBy, "+
                "B.CreatedTime FROM  FC_Items AS I2 RIGHT OUTER JOIN T3_Supply  AS B ON I2.ID = B.ItemID  LEFT OUTER JOIN  " +
                "FC_Lokasi AS L1 ON B.LokID = L1.ID where convert(varchar,B.tgltrans,112)='"+tgl+"' order by B.ID desc"; 
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_Supply = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_Supply.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT3_Supply.Add(new T3_Supply());

            return arrT3_Supply;
        }
        public T3_Supply GenerateObject(SqlDataReader sqlDataReader)
        {
            objT3_Supply = new T3_Supply();
            objT3_Supply.TglTrans = Convert.ToDateTime((sqlDataReader["TglTrans"]).ToString()) ;
            objT3_Supply.Partno = sqlDataReader["Partno"].ToString();
            objT3_Supply.Lokasi  = sqlDataReader["Lokasi"].ToString();
            objT3_Supply.Qty = Convert.ToInt32(sqlDataReader["Qty"]);
            objT3_Supply.CreatedBy = (sqlDataReader["CreatedBy"]).ToString();
            objT3_Supply.CreatedTime = Convert.ToDateTime((sqlDataReader["CreatedTime"]).ToString());
            return objT3_Supply;
        }
    }
}
