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
    public class T3_ReturMasterFacade : BusinessFacade.AbstractTransactionFacadeF
    {
        private T3_ReturMaster objT3_ReturMaster = new T3_ReturMaster();
        private ArrayList arrT3_ReturMaster;
        private List<SqlParameter> sqlListParam;

        public T3_ReturMasterFacade(object objDomain)
            : base(objDomain)
        {
            objT3_ReturMaster = (T3_ReturMaster)objDomain;
        }
        public T3_ReturMasterFacade()
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
                objT3_ReturMaster = (T3_ReturMaster)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ReturNo", objT3_ReturMaster.ReturNo ));
                sqlListParam.Add(new SqlParameter("@ReturDate", objT3_ReturMaster.ReturDate ));
                sqlListParam.Add(new SqlParameter("@Keterangan", objT3_ReturMaster.Keterangan ));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT3_ReturMaster.CreatedBy  ));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertT3_ReturMaster");
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
        public ArrayList RetrieveByTgl(string tgl)
        {
            string strSQL = "select * from T3_ReturMaster where convert(varchar,returdate,112)='" + tgl + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_ReturMaster = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_ReturMaster.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT3_ReturMaster.Add(new T3_ReturMaster());

            return arrT3_ReturMaster;
        }
        public int GetDocNo(DateTime tgltrans)
        {
            int docnocount = 0;
            string strSQL = "select count(returno)as docnocount from ( select distinct returno from T3_ReturMaster where month(returdate) =" +
                tgltrans.Month + " and year(returdate)=" + tgltrans.Year + " ) as a ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_ReturMaster = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    docnocount = Convert.ToInt16(sqlDataReader["docnocount"]);
                }
            }
            return docnocount;
        }
        public T3_ReturMaster GenerateObject(SqlDataReader sqlDataReader)
        {
            objT3_ReturMaster = new T3_ReturMaster();
            objT3_ReturMaster.ReturNo = (sqlDataReader["ReturNo"]).ToString();
            objT3_ReturMaster.ReturDate = Convert.ToDateTime(sqlDataReader["ReturDate"]);
            objT3_ReturMaster.Keterangan = (sqlDataReader["Keterangan"]).ToString();
            objT3_ReturMaster.CreatedBy = (sqlDataReader["CreatedBy"]).ToString();
            return objT3_ReturMaster;
        }

    }
}
