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
    public class GL_CcyRateFacade : AbstractFacade
    {
        private GL_CcyRate  objGL_CcyRate = new GL_CcyRate();
        private ArrayList arrGL_CcyRate;
        private List<SqlParameter> sqlListParam;

        public GL_CcyRateFacade()
            : base()
        {

        }
        public override int Insert(object objDomain)
        {
            throw new NotImplementedException();
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
            string strSQL = "Selectisnull(ID,0) as ID, CcyCode, CcyRate,EffectiveDate  from GL_CcyRate where isnull(RowStatus,0) >-1 order by EffectiveDate";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGL_CcyRate = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_CcyRate.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_CcyRate.Add(new GL_CcyRate());

            return arrGL_CcyRate;
        }

        public ArrayList RetrieveById(int ID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select isnull(ID,0) as ID, CcyCode, CcyRate,EffectiveDate from GL_CcyRate where isnull(RowStatus,0) >-1 and ID = " + ID);
            strError = dataAccess.Error;
            arrGL_CcyRate = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_CcyRate.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_CcyRate.Add(new GL_CcyRate());
            return arrGL_CcyRate;
        }
        public GL_CcyRate RetrieveByCcyCode(string CcyCode,string efectivedate)
        {
            string strSQL = "Select isnull(ID,0) as ID, CcyCode, CcyRate,EffectiveDate from GL_CcyRate where isnull(RowStatus,0) >-1 and CcyCode = '" + CcyCode +
                "' and convert(char,EffectiveDate,112)='" + efectivedate + "'";
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
            else
            {
                strSQL = "Select top 1 isnull(ID,0) as ID, CcyCode, CcyRate,EffectiveDate from GL_CcyRate where isnull(RowStatus,0) >-1 and CcyCode = '" + CcyCode +
                "' order by EffectiveDate desc";
                 dataAccess = new DataAccess(Global.ConnectionString());
                 sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
                 strError = dataAccess.Error;
                 if (sqlDataReader.HasRows)
                 {
                     while (sqlDataReader.Read())
                     {
                         return GenerateObject(sqlDataReader);
                     }
                 }
            }
            return new GL_CcyRate();
        }

        public GL_CcyRate GenerateObject(SqlDataReader sqlDataReader)
        {
            objGL_CcyRate = new GL_CcyRate();
            objGL_CcyRate.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objGL_CcyRate.CcyCode = sqlDataReader["CcyCode"].ToString();
            objGL_CcyRate.CcyRate = Convert.ToInt32(sqlDataReader["CcyRate"]);
            objGL_CcyRate.EfectiveDate =Convert.ToDateTime(sqlDataReader["EffectiveDate"].ToString());
            return objGL_CcyRate;
        }
    }
}
