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
    public class TPP_Penyebab_Detail_Facade : BusinessFacade.AbstractTransactionFacadeF
    {
        
        private TPP_Penyebab_Detail objTPP = new TPP_Penyebab_Detail();
        private ArrayList arrTPP;
        private List<SqlParameter> sqlListParam;

        public TPP_Penyebab_Detail_Facade(object objDomain)
            : base(objDomain)
        {
            objTPP = (TPP_Penyebab_Detail)objDomain;
        }
        public TPP_Penyebab_Detail_Facade()
        {
        }
        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Penyebab_ID", objTPP.Penyebab_ID));
                sqlListParam.Add(new SqlParameter("@TPP_ID", objTPP.TPP_ID));
                sqlListParam.Add(new SqlParameter("@Rowstatus", objTPP.Rowstatus));
                sqlListParam.Add(new SqlParameter("@Uraian", objTPP.Uraian));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objTPP.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "[TPP_Penyebab_Detail_Insert]");
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
            string strSQL = "";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrTPP = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTPP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTPP.Add(new TPP());
            return arrTPP;
        }
        public ArrayList RetrieveByNo(string No)
        {
            string strSQL = "SELECT A.Penyebab_ID, A.TPP_ID, A.Uraian,B.Penyebab  " +
                "FROM TPP_Penyebab_Detail A inner join TPP_Penyebab B on A.Penyebab_ID=B.ID inner join TPP C on A.TPP_ID=C.ID " +
                "where A.rowstatus>-1 and C.Laporan_No='"+No+"'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrTPP = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTPP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTPP.Add(new TPP_Penyebab_Detail());
            return arrTPP;
        }
        public TPP_Penyebab_Detail GenerateObject(SqlDataReader sqlDataReader)
        {
            objTPP = new TPP_Penyebab_Detail();
            objTPP.Penyebab_ID = Convert.ToInt32(sqlDataReader["Penyebab_ID"]);
            objTPP.TPP_ID = Convert.ToInt32(sqlDataReader["TPP_ID"]);
            objTPP.Uraian = sqlDataReader["Uraian"].ToString();
            objTPP.Penyebab = sqlDataReader["Penyebab"].ToString();
            return objTPP;
        }

        public override int Update(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Insert1(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Update1(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Update2(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Delete(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }
    }
}
