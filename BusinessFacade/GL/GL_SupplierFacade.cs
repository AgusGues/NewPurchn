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
    public class GL_SupplierFacade : AbstractFacade
    {
        private GL_Supplier  objGL_Supplier = new GL_Supplier();
        private ArrayList arrGL_Supplier;
        private List<SqlParameter> sqlListParam;

        public GL_SupplierFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objGL_Supplier = (GL_Supplier)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ChartNo", objGL_Supplier.ChartNo));
                sqlListParam.Add(new SqlParameter("@SupCode", objGL_Supplier.SupCode));
                sqlListParam.Add(new SqlParameter("@SuppName", objGL_Supplier.SuppName));
                sqlListParam.Add(new SqlParameter("@Address", objGL_Supplier.Address));
                sqlListParam.Add(new SqlParameter("@CCYCode", objGL_Supplier.CCYCode));
                sqlListParam.Add(new SqlParameter("@City", objGL_Supplier.City));
                sqlListParam.Add(new SqlParameter("@Country", objGL_Supplier.Country));
                sqlListParam.Add(new SqlParameter("@Phone", objGL_Supplier.Phone));
                sqlListParam.Add(new SqlParameter("@Fax", objGL_Supplier.Fax));
                sqlListParam.Add(new SqlParameter("@ContactPerson", objGL_Supplier.ContactPerson));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertGL_Supplier");

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
                objGL_Supplier = (GL_Supplier)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objGL_Supplier.ID));
                

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteGL_Supplier");

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
            string strSQL = "Select * from GL_Supplier where RowStatus >-1 order by Suppname";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGL_Supplier = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_Supplier.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_Supplier.Add(new GL_Supplier());

            return arrGL_Supplier;
        }

        public ArrayList RetrieveById(int ID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from GL_Supplier where ID = " + ID);
            strError = dataAccess.Error;
            arrGL_Supplier = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_Supplier.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_Supplier.Add(new GL_Supplier());

            return arrGL_Supplier;
        }
        public ArrayList RetrieveByDepo(int depoID, int subComID, string typeTrx)
        {
            string strQ = string.Empty;
            if (depoID == 1)
                strQ = "select a.*,T.ID,isnull(ChartNo,'') as ChartNo from (SELECT[ID],[SupplierCode],[SupplierName],[Alamat],[UP] FROM[123.123.123.130].[bpasctrp].[dbo].[SuppPurch] where subCompanyID = " + subComID+") as a "+
                       "left join TrxMappingCOA as T on T.PTID="+ subComID + " and T.ItemIDPlant=a.ID and ltrim(rtrim(TypeTrx))='"+typeTrx+ "' and T.RowStatus>-1 and T.DepoID=" + depoID +
                       " left join GL_ChartOfAccount as C on C.ID= T.CoaIDCredit " ;
            else if (depoID == 7)
                strQ = "select a.*,T.ID,T.ItemIDPlant,isnull(ChartNo,'') as ChartNo from (SELECT [ID],[SupplierCode],[SupplierName],[Alamat],[UP] FROM [10.0.0.254].[bpaskrwg].[dbo].[SuppPurch] where subCompanyID = " + subComID+") as a "+
                       "left join TrxMappingCOA as T on T.PTID = "+ subComID + " and T.ItemIDPlant = a.ID and ltrim(rtrim(TypeTrx)) = '"+typeTrx+"' and T.RowStatus > -1 and T.DepoID = " + depoID +
                       " left join GL_ChartOfAccount as C on C.ID = T.CoaIDCredit " ;

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strQ);
            strError = dataAccess.Error;
            arrGL_Supplier = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_Supplier.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrGL_Supplier.Add(new GL_Supplier());

            return arrGL_Supplier;
        }

        public GL_Supplier RetrieveByNo(string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from GL_Supplier where RowStatus >-1  and ChartNo='"  + strValue + "'");
            strError = dataAccess.Error;
            arrGL_Supplier = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new GL_Supplier();
        }
        public GL_Supplier RetrieveByName1(string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select top 1 * from GL_Supplier where RowStatus >-1  and suppname='" + strValue + "'");
            strError = dataAccess.Error;
            arrGL_Supplier = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new GL_Supplier();
        }
        public GL_Supplier RetrieveByCode(string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select top 1 * from GL_Supplier where RowStatus >-1  and SupCode='" + strValue + "'");
            strError = dataAccess.Error;
            arrGL_Supplier = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new GL_Supplier();
        }

        public ArrayList RetrieveByName(string SupplierName)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from GL_Supplier where Suppname like '%" + SupplierName + "%'");
            strError = dataAccess.Error;
            arrGL_Supplier = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_Supplier.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_Supplier.Add(new GL_Supplier());
            return arrGL_Supplier;
        }

        public GL_Supplier GenerateObject(SqlDataReader sqlDataReader)
        {
            objGL_Supplier = new GL_Supplier();
            objGL_Supplier.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objGL_Supplier.ChartNo = sqlDataReader["ChartNo"].ToString();
            objGL_Supplier.SupCode = sqlDataReader["SupCode"].ToString();
            objGL_Supplier.SuppName = sqlDataReader["SuppName"].ToString();
            objGL_Supplier.Address = sqlDataReader["Address"].ToString();
            objGL_Supplier.CCYCode = sqlDataReader["cCYCode"].ToString();
            objGL_Supplier.City = sqlDataReader["City"].ToString();
            objGL_Supplier.Country = sqlDataReader["Country"].ToString();
            objGL_Supplier.Phone = sqlDataReader["Phone"].ToString();
            objGL_Supplier.Fax = sqlDataReader["Fax"].ToString();
            objGL_Supplier.ContactPerson = sqlDataReader["ContactPerson"].ToString();
            objGL_Supplier.CompanyCode = sqlDataReader["CompanyCode"].ToString();

            return objGL_Supplier;
        }
        public GL_Supplier GenerateObject2(SqlDataReader sqlDataReader)
        {
            objGL_Supplier = new GL_Supplier();
            objGL_Supplier.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objGL_Supplier.SupCode = sqlDataReader["SupplierCode"].ToString();
            objGL_Supplier.SuppName = sqlDataReader["SupplierName"].ToString();
            objGL_Supplier.Address = sqlDataReader["Alamat"].ToString();
            objGL_Supplier.ContactPerson = sqlDataReader["UP"].ToString();
            objGL_Supplier.ChartNo = sqlDataReader["ChartNo"].ToString();

            return objGL_Supplier;
        }


    }
}
