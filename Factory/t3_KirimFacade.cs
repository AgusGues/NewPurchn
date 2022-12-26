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
    public class T3_KirimFacade : BusinessFacade.AbstractTransactionFacadeF
    {
        private T3_Kirim objT3_Kirim = new T3_Kirim();
        private ArrayList arrT3_Kirim;
        private List<SqlParameter> sqlListParam;

        
        public T3_KirimFacade(object objDomain)
                    : base(objDomain)
                {
                    objT3_Kirim = (T3_Kirim)objDomain;
                }

        public T3_KirimFacade()
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
                objT3_Kirim = (T3_Kirim)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@SJNo", objT3_Kirim.SJNo ));
                sqlListParam.Add(new SqlParameter("@OPNo", objT3_Kirim.OPNo ));
                sqlListParam.Add(new SqlParameter("@Customer", objT3_Kirim.Customer ));
                sqlListParam.Add(new SqlParameter("@TglKirim", objT3_Kirim.TglKirim  ));
                sqlListParam.Add(new SqlParameter("@Total", objT3_Kirim.Total ));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT3_Kirim.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertT3_Kirim");
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

        public T3_Kirim RetrieveBySJNo(string SJNo)
        {
            string strSQL = "select  ID, Customer, SJNo, OPNo, TglKirim, Total, Status, CreatedBy, CreatedTime, LastModifiedBy, LastModifiedTime, RowStatus from t3_kirim where sjno='" + SJNo + "'";
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

            return new T3_Kirim();
        }
        public T3_Kirim  RetrieveBySJInfo(string SJNo)
        {
            string strSQL = "SELECT A.SuratJalanNo as sjno, B.OPNo, C.CustomerName as customer,0 as total,A.createdtime as tglkirim "+
                "FROM Customer AS C RIGHT OUTER JOIN OP AS B ON C.ID = B.CustomerId RIGHT OUTER JOIN SuratJalan AS A ON B.ID = A.OPID "+
                "WHERE A.SuratJalanNo = '"+ SJNo + "'";
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

            return new T3_Kirim();
        }
        public T3_Kirim RetrieveBySJTOInfo(string SJNo)
        {
            string strSQL = "SELECT A.SuratJalanNo as sjno, '-' as OPNo, 'DEPO' as customer,0 as total,A.createdtime as tglkirim " +
                "FROM  SuratJalanto A WHERE A.SuratJalanNo = '" + SJNo + "'";
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

            return new T3_Kirim();
        }
        
        public T3_Kirim GenerateObject(SqlDataReader sqlDataReader)
        {
            objT3_Kirim = new T3_Kirim();
            objT3_Kirim.SJNo = (sqlDataReader["SJNo"]).ToString();
            objT3_Kirim.OPNo = (sqlDataReader["OPNo"]).ToString();
            objT3_Kirim.Customer = (sqlDataReader["Customer"]).ToString();
            objT3_Kirim.Total = Convert.ToInt32(sqlDataReader["Total"]);
            objT3_Kirim.TglKirim = Convert.ToDateTime(sqlDataReader["TglKirim"]);
            return objT3_Kirim;
        }


    }
}
