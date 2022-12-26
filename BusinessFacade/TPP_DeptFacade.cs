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
    public class TPP_DeptFacade : BusinessFacade.AbstractTransactionFacadeF
    {
        private TPP_Dept objDept = new TPP_Dept();
        private ArrayList arrDept;
        private List<SqlParameter> sqlListParam;
        public override int Insert(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
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
              
        public override ArrayList Retrieve()
        {
            string strSQL = "select * from tpp_Dept as A where A.RowStatus >-1   order by A.Departemen";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrDept = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDept.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrDept.Add(new Dept());

            return arrDept;
        }
        public  ArrayList RetrieveMasalah()
        {
            string strSQL = "select A.ID,A.Asal_Masalah departemen,A.kode from TPP_Asal_Masalah as A where A.RowStatus >-1   order by A.Asal_Masalah";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrDept = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDept.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrDept.Add(new Dept());

            return arrDept;
        }
        public TPP_Dept GetUserDept(int userid)
        {
            string strSQL = "select A.*  from TPP_Dept  A inner join TPP_Users B on A.ID=B.Dept_ID  where A.rowstatus>-1 and  B.rowstatus>-1 and B.User_ID=" + userid;
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
            return new TPP_Dept();
        }
		
		//penambahan agus 2022-05-31
        public string GetUserAkserTPP(int userid1)
        {
            string hasil = string.Empty;
            string strSQL = "select userid from UserDeptAuth where ModulName='Input TPP' and userid=" + userid1;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty || sdr.HasRows)
            {
                while (sdr.Read())
                {
                    hasil = sdr["userid"].ToString();
                }
            }

            return hasil;
        }

        public string GetUserAkserTPP2(int userid2)
        {
            string hasil2 = string.Empty;
            string strSQL = "select User_ID from TPP_Users where User_ID=" + userid2;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty || sdr.HasRows)
            {
                while (sdr.Read())
                {
                    hasil2 = sdr["User_ID"].ToString();
                }
            }

            return hasil2;
        }

        //penambahan agus 2022-05-31
		
        private TPP_Dept GenerateObject(SqlDataReader sqlDataReader)
        {
            objDept = new TPP_Dept();
            objDept.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objDept.Kode = sqlDataReader["Kode"].ToString();
            objDept.Departemen = sqlDataReader["Departemen"].ToString();
            return objDept;
        }
    }
}
