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
    public class GL_ParameterFacade : AbstractFacade
    {
        private GL_Parameter  objGL_Parameter = new GL_Parameter();
        private ArrayList arrGL_Parameter;
        private List<SqlParameter> sqlListParam;

        public GL_ParameterFacade()
            : base()
        {

        }
        public override int Insert(object objDomain)
        {
            try
            {
                objGL_Parameter = (GL_Parameter)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ParamCode", objGL_Parameter.ParamCode));
                sqlListParam.Add(new SqlParameter("@ParamName", objGL_Parameter.ParamName));
                sqlListParam.Add(new SqlParameter("@CharValue", objGL_Parameter.CharValue));
                sqlListParam.Add(new SqlParameter("@CompanyCode", objGL_Parameter.CompanyCode));

                int intResult = dataAccess.ProcessData(sqlListParam, "GLInsert_Parameter");
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

        public override ArrayList Retrieve()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "Select * from GL_Parameter where isnull(RowStatus,0) >-1 order by Custname";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGL_Parameter = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_Parameter.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_Parameter.Add(new GL_Parameter());
            return arrGL_Parameter;
        }
        public ArrayList retrieveByInfo( string companyCode)
        {
            string strSQL = "select * from GL_Parameter where Info is not null and CompanyCode='"+ companyCode + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGL_Parameter = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_Parameter.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_Parameter.Add(new GL_Parameter());

            return arrGL_Parameter;
        }

        public string retrieveByName(string paramname, string companyCode)
        {
            string charvalue = string.Empty;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "Select * from GL_Parameter where isnull(RowStatus,0) >-1 and paramname='" + paramname + "'";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    charvalue = sqlDataReader["CharValue"].ToString(); 
                }
            }
            return charvalue;
        }
        public string retrieveByCode(string paramCode, string companyCode)
        {
            string charvalue = string.Empty;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "Select * from GL_Parameter where isnull(RowStatus,0) >-1 and paramCode='" + paramCode + "' and CompanyCode='"+companyCode+"'";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    charvalue = sqlDataReader["CharValue"].ToString().Trim();
                }
            }
            return charvalue;
        }
        public GL_Parameter retrieveByParamCode(string paramCode, string companyCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from GL_Parameter where isnull(RowStatus,0) >-1 and paramCode='" + paramCode + "' and CompanyCode='" + companyCode + "'");
            strError = dataAccess.Error;
            arrGL_Parameter = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new GL_Parameter();
        }
        public string GetchParam(String cKeyValue, String DefaultName, String DefaultValue, string companyCode)
        {
            string charvalue = string.Empty;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "Select * from GL_Parameter where isnull(RowStatus,0) >-1 and paramCode='" + cKeyValue + "'";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    charvalue = sqlDataReader["CharValue"].ToString();
                }
            }
            else
            {
                GL_Parameter glparameter = new GL_Parameter();
                glparameter.ParamCode = cKeyValue;
                glparameter.ParamName = DefaultName;
                glparameter.CharValue = DefaultValue;
                Insert(glparameter);
                charvalue = DefaultValue;
            }
            return charvalue;
        }

        public ArrayList RetrieveById(int ID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from GL_Parameter where isnull(RowStatus,0) >-1 and ID = " + ID);
            strError = dataAccess.Error;
            arrGL_Parameter = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_Parameter.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_Parameter.Add(new GL_Parameter());
            return arrGL_Parameter;
        }

        public GL_Parameter GenerateObject(SqlDataReader sqlDataReader)
        {
            objGL_Parameter = new GL_Parameter();
            objGL_Parameter.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objGL_Parameter.ParamCode = sqlDataReader["ParamCode"].ToString();
            objGL_Parameter.ParamName = sqlDataReader["ParamName"].ToString();
            objGL_Parameter.IntValue = Convert.ToInt32(sqlDataReader["IntValue"].ToString());
            objGL_Parameter.CharValue = sqlDataReader["CharValue"].ToString();
            objGL_Parameter.Info = sqlDataReader["Info"].ToString();

            return objGL_Parameter;
        }
    }
}
