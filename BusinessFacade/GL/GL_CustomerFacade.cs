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
    public class GL_CustomerFacade : AbstractFacade
    {
        private GL_Customer  objGL_Customer = new GL_Customer();
        private ArrayList arrGL_Customer;
        private List<SqlParameter> sqlListParam;

        public GL_CustomerFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objGL_Customer = (GL_Customer)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ChartNo", objGL_Customer.ChartNo));
                sqlListParam.Add(new SqlParameter("@CustCode", objGL_Customer.CustCode));
                sqlListParam.Add(new SqlParameter("@CustName", objGL_Customer.CustName));
                sqlListParam.Add(new SqlParameter("@Address", objGL_Customer.Address));
                sqlListParam.Add(new SqlParameter("@CCYCode", objGL_Customer.CCYCode));
                sqlListParam.Add(new SqlParameter("@City", objGL_Customer.City));
                sqlListParam.Add(new SqlParameter("@Country", objGL_Customer.Country));
                sqlListParam.Add(new SqlParameter("@Phone", objGL_Customer.Phone));
                sqlListParam.Add(new SqlParameter("@Fax", objGL_Customer.Fax));
                sqlListParam.Add(new SqlParameter("@ContactPerson", objGL_Customer.ContactPerson));
                sqlListParam.Add(new SqlParameter("@VAT", objGL_Customer.VAT));
                sqlListParam.Add(new SqlParameter("@NPWP", objGL_Customer.NPWP));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertGL_Customer");

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
            return 0;
        }

        public override int Delete(object objDomain)
        {

            try
            {
                objGL_Customer = (GL_Customer)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objGL_Customer.ID));
                

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteGL_Customer");

                strError = dataAccess.Error;

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
            string strSQL = "Select * from GL_Customer where RowStatus >-1 order by Custname";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGL_Customer = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_Customer.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_Customer.Add(new GL_Customer());

            return arrGL_Customer;
        }

        public ArrayList RetrieveById(int ID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from GL_Customer where ID = " + ID);
            strError = dataAccess.Error;
            arrGL_Customer = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_Customer.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_Customer.Add(new GL_Customer());

            return arrGL_Customer;
        }

        public GL_Customer RetrieveByNo(string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from GL_Customer where RowStatus >-1  and ChartNo='"  + strValue + "'");
            strError = dataAccess.Error;
            arrGL_Customer = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new GL_Customer();
        }
        public GL_Customer RetrieveByName1(string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from GL_Customer where RowStatus >-1  and custname='" + strValue + "'");
            strError = dataAccess.Error;
            arrGL_Customer = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new GL_Customer();
        }
        public GL_Customer RetrieveByCode(string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from GL_Customer where RowStatus >-1  and custcode='" + strValue + "'");
            strError = dataAccess.Error;
            arrGL_Customer = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new GL_Customer();
        }
        public ArrayList RetrieveByName(string CustomerName)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from GL_Customer where Custname like '%" + CustomerName + "%'");
            strError = dataAccess.Error;
            arrGL_Customer = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_Customer.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_Customer.Add(new GL_Customer());

            return arrGL_Customer;
        }

        public GL_Customer GenerateObject(SqlDataReader sqlDataReader)
        {
            objGL_Customer = new GL_Customer();
            objGL_Customer.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objGL_Customer.ChartNo = sqlDataReader["ChartNo"].ToString();
            objGL_Customer.CustCode = sqlDataReader["CustCode"].ToString();
            objGL_Customer.CustName = sqlDataReader["CustName"].ToString();
            objGL_Customer.Address = sqlDataReader["Address"].ToString();
            objGL_Customer.CCYCode = sqlDataReader["cCYCode"].ToString();
            objGL_Customer.City = sqlDataReader["City"].ToString();
            objGL_Customer.Country = sqlDataReader["Country"].ToString();
            objGL_Customer.Phone = sqlDataReader["Phone"].ToString();
            objGL_Customer.Fax = sqlDataReader["Fax"].ToString();
            objGL_Customer.ContactPerson = sqlDataReader["ContactPerson"].ToString();
            objGL_Customer.ContactPerson = sqlDataReader["VAT"].ToString();
            objGL_Customer.ContactPerson = sqlDataReader["NPWP"].ToString();
            return objGL_Customer;
        }
    }
}
