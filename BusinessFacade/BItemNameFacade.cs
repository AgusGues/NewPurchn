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
    public class BItemNameFacade : AbstractFacade
    {
        private BItemName objBItemName = new BItemName();
        private ArrayList arrBItemName;
        private List<SqlParameter> sqlListParam;

        public BItemNameFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objBItemName = (BItemName)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemName", objBItemName.ItemName));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objBItemName.CreatedBy));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertBItemName");
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
            string strSQL = "SELECT * from BItemName order by itemname";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrBItemName = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBItemName.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrBItemName.Add(new BItemName());

            return arrBItemName;
        }

        public BItemName RetrieveByInCode(string incode)
        {
            string strSQL = "SELECT * from BItemName where incode='" + incode + "'";
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
            return new BItemName();
        }

        public BItemName GenerateObject(SqlDataReader sqlDataReader)
        {
            objBItemName = new BItemName();
            objBItemName.InCode = sqlDataReader["InCode"].ToString();
            objBItemName.ItemName = sqlDataReader["ItemName"].ToString();
            return objBItemName;
        }
    }
}
