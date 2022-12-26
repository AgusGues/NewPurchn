using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using BusinessFacade;
using DataAccessLayer;

namespace Factory
{
    public class T3_MutasiLokFacade : BusinessFacade.AbstractTransactionFacadeF
    {
        private T3_MutasiLok objT3_MutasiLok = new T3_MutasiLok();
        private ArrayList arrT3_MutasiLok;
        private List<SqlParameter> sqlListParam;

        
        public T3_MutasiLokFacade(object objDomain)
            : base(objDomain)
        {
            objT3_MutasiLok = (T3_MutasiLok)objDomain;
        }
        public T3_MutasiLokFacade()
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
                objT3_MutasiLok = (T3_MutasiLok)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@SerahID", objT3_MutasiLok.SerahID ));
                sqlListParam.Add(new SqlParameter("@LokID1", objT3_MutasiLok.LokID1 ));
                sqlListParam.Add(new SqlParameter("@LokID2", objT3_MutasiLok.LokID2 ));
                sqlListParam.Add(new SqlParameter("@ItemID", objT3_MutasiLok.ItemID));
                sqlListParam.Add(new SqlParameter("@TglML", objT3_MutasiLok.TglML ));
                sqlListParam.Add(new SqlParameter("@Qty", objT3_MutasiLok.Qty ));
                sqlListParam.Add(new SqlParameter("@SA1", objT3_MutasiLok.SA1));
                sqlListParam.Add(new SqlParameter("@SA2", objT3_MutasiLok.SA2));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT3_MutasiLok.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertT3_MutasiLok");
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
            string strSQL = "SELECT I2.PartNo AS Partno,L1.Lokasi as lokasi1 ,  L2.Lokasi AS Lokasi2, B.Qty,B.CreatedBy,B.CreatedTime "+
                "FROM FC_Lokasi AS L2 RIGHT OUTER JOIN FC_Items AS I2 RIGHT OUTER JOIN T3_MutasiLok AS B ON I2.ID = B.ItemID ON L2.ID = B.LokID2 LEFT OUTER JOIN "+
                "FC_Lokasi AS L1 ON B.LokID1 = L1.ID LEFT OUTER JOIN FC_Items AS I1 INNER JOIN T3_Serah AS A ON I1.ID = A.ItemID ON B.SerahID = A.ID "+
                "where convert(varchar,B.tgltrans,112)='" + tgl + "' order by B.ID desc"; 
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_MutasiLok = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_MutasiLok.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT3_MutasiLok.Add(new T3_MutasiLok());

            return arrT3_MutasiLok;
        }
        public T3_MutasiLok GenerateObject(SqlDataReader sqlDataReader)
        {
            objT3_MutasiLok = new T3_MutasiLok();
            objT3_MutasiLok.Partno = sqlDataReader["Partno"].ToString();
            objT3_MutasiLok.Lokasi1  = sqlDataReader["Lokasi1"].ToString();
            objT3_MutasiLok.Qty = Convert.ToInt32(sqlDataReader["Qty"]);
            objT3_MutasiLok.Lokasi2 = (sqlDataReader["Lokasi2"]).ToString();
            objT3_MutasiLok.CreatedBy = (sqlDataReader["CreatedBy"]).ToString();
            objT3_MutasiLok.CreatedTime = Convert.ToDateTime((sqlDataReader["CreatedTime"]).ToString());
            return objT3_MutasiLok;
        }


    }
}
