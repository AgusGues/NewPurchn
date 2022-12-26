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
    public class UOMFacade : AbstractFacade
    {
        private UOM objUOM = new UOM();
        private ArrayList arrUOM;
        private List<SqlParameter> sqlListParam;

        public UOMFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objUOM = (UOM)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@UOMCode", objUOM.UOMCode));
                sqlListParam.Add(new SqlParameter("@UOMDesc", objUOM.UOMDesc));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objUOM.CreatedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertUOM");

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
            try
            {
                objUOM = (UOM)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objUOM.ID));
                sqlListParam.Add(new SqlParameter("@UOMCode", objUOM.UOMCode));
                sqlListParam.Add(new SqlParameter("@UOMDesc", objUOM.UOMDesc));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUOM.CreatedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateUOM");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Delete(object objDomain)
        {

            try
            {
                objUOM = (UOM)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objUOM.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUOM.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteUOM");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from UOM where RowStatus = 0");
            strError = dataAccess.Error;
            arrUOM = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUOM.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrUOM.Add(new UOM());

            return arrUOM;
        }
        public ArrayList Retrieve1()
        {
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from UOM where RowStatus = 0 order by uomdesc");
            strError = dataAccess.Error;
            arrUOM = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUOM.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrUOM.Add(new UOM());

            return arrUOM;
        }
        public UOM RetrieveByCode(string uomCode)
        {
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from UOM where RowStatus = 0 and UOMCode = '" + uomCode + "'");
            strError = dataAccess.Error;
            arrUOM = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new UOM();
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from UOM where RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrUOM = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUOM.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrUOM.Add(new UOM());

            return arrUOM;
        }

        public UOM RetrieveByID(int id)
        {
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from UOM where RowStatus = 0 and ID = " + id);
            strError = dataAccess.Error;
            arrUOM = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new UOM();
        }
        public UOM GenerateObject(SqlDataReader sqlDataReader)
        {
            objUOM = new UOM();
            objUOM.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objUOM.UOMCode = sqlDataReader["UOMCode"].ToString();
            objUOM.UOMDesc = sqlDataReader["UOMDesc"].ToString();
            objUOM.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objUOM.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objUOM.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objUOM.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objUOM.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objUOM;

        }
    }
}
