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
    public class BItemMerkFacade : AbstractFacade
    {
        private BItemMerk objBItemMerk = new BItemMerk();
        private ArrayList arrBItemMerk;
        private List<SqlParameter> sqlListParam;

        public BItemMerkFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objBItemMerk = (BItemMerk)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@InCode", objBItemMerk.InCode));
                sqlListParam.Add(new SqlParameter("@InMerk", objBItemMerk.InMerk));
                sqlListParam.Add(new SqlParameter("@createdBy", objBItemMerk.CreatedBy));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertBItemMerk");
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
            string strSQL = "SELECT * from BItemMerk ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrBItemMerk = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBItemMerk.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrBItemMerk.Add(new BItemMerk());

            return arrBItemMerk;
        }

        public BItemMerk RetrieveBykode(string incode, string kode)
        {
            string strSQL = "SELECT * from BItemMerk where incode='" + incode + "' and kode='" + kode + "'";
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
            return new BItemMerk();
        }

        public ArrayList RetrieveByInCode(string incode)
        {
            string strSQL = "SELECT * from BItemMerk where incode='" + incode + "' order by inmerk";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrBItemMerk = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBItemMerk.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrBItemMerk.Add(new BItemMerk());

            return arrBItemMerk;
        }
        public BItemMerk GenerateObject(SqlDataReader sqlDataReader)
        {
            objBItemMerk = new BItemMerk();
            objBItemMerk.InCode = sqlDataReader["InCode"].ToString();
            objBItemMerk.Kode = sqlDataReader["kode"].ToString();
            objBItemMerk.InMerk = sqlDataReader["inMerk"].ToString();
            return objBItemMerk;
        }
    }
}
