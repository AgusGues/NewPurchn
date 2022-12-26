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
    public class BItemTypeFacade : AbstractFacade
    {
        private BItemType objBItemType = new BItemType();
        private ArrayList arrBItemType;
        private List<SqlParameter> sqlListParam;

        public BItemTypeFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objBItemType = (BItemType)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@InCode", objBItemType.InCode));
                sqlListParam.Add(new SqlParameter("@createdby", objBItemType.CreatedBy));
                sqlListParam.Add(new SqlParameter("@InType", objBItemType.InType));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertBItemType");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Update(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override int Delete(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override System.Collections.ArrayList Retrieve()
        {
            string strSQL = "SELECT * from BItemType ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrBItemType = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBItemType.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrBItemType.Add(new BItemType());

            return arrBItemType;
        }

        public BItemType RetrieveBykode(string incode, string kode)
        {
            string strSQL = "SELECT * from BItemType where incode='" + incode + "' and kode='" + kode + "'";
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
            return new BItemType();
        }

        public ArrayList RetrieveByInCode(string incode)
        {
            string strSQL = "SELECT * from BItemType where incode='" + incode + "' order by intype";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrBItemType = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBItemType.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrBItemType.Add(new BItemType());

            return arrBItemType;
        }

        public BItemType GenerateObject(SqlDataReader sqlDataReader)
        {
            objBItemType = new BItemType();
            objBItemType.InCode = sqlDataReader["InCode"].ToString();
            objBItemType.Kode = sqlDataReader["kode"].ToString();
            objBItemType.InType = sqlDataReader["inType"].ToString();
            return objBItemType;
        }
    /**
     * Proces Data ukuran
     */

        public ArrayList RetrieveUkuran(string inCode)
        {
            string strSQL = "SELECT InCode,Kode,Ukuran as InType from BItemUkuran where INCode='" + inCode + "' order by ukuran";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrBItemType = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBItemType.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrBItemType.Add(new BItemType());

            return arrBItemType;
        }

        public int InsertUkuran(object objDomain)
        {
            try
            {
                objBItemType = (BItemType)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@InCode", objBItemType.InCode));
                sqlListParam.Add(new SqlParameter("@createdby", objBItemType.CreatedBy));
                sqlListParam.Add(new SqlParameter("@Ukuran", objBItemType.InType));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertBItemUkuran");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
    }
}

