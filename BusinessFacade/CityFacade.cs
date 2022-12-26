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
    public class CityFacade : AbstractFacade
    {
        private City objCity = new City();
        private ArrayList arrCity;
        private List<SqlParameter> sqlListParam;


        public CityFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objCity = (City)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@PropinsiID", objCity.PropinsiID));
                sqlListParam.Add(new SqlParameter("@CityName", objCity.CityName));
                sqlListParam.Add(new SqlParameter("@AreaDistribusiID", objCity.AreaDistribusiID));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objCity.CreatedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertCity");

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
                objCity = (City)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objCity.ID));
                sqlListParam.Add(new SqlParameter("@PropinsiID", objCity.PropinsiID));
                sqlListParam.Add(new SqlParameter("@CityName", objCity.CityName));
                sqlListParam.Add(new SqlParameter("@AreaDistribusiID", objCity.AreaDistribusiID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objCity.CreatedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateCity");

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
                objCity = (City)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objCity.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objCity.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteCity");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.PropinsiID,B.NamaPropinsi,A.CityName,A.AreaDistribusiID,C.AreaDistribusiName,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from City as A,Propinsi as B,AreaDistribusi as C where A.PropinsiID = B.ID and A.AreaDistribusiID = C.ID and A.RowStatus = 0 and A.ID > 0");
            strError = dataAccess.Error;
            arrCity = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrCity.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrCity.Add(new City());

            return arrCity;
        }

        public City RetrieveByCode(string strNamaCity)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.PropinsiID,B.NamaPropinsi,A.CityName,A.AreaDistribusiID,C.AreaDistribusiName,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from City as A,Propinsi as B,AreaDistribusi as C where A.PropinsiID = B.ID and A.AreaDistribusiID = C.ID and A.RowStatus = 0 and A.ID > 0 and A.CityName = '" + strNamaCity + "'");
            strError = dataAccess.Error;
            arrCity = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new City();
        }

        public ArrayList RetrieveByPropinsiId(int intPropinsiId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.PropinsiID,B.NamaPropinsi,A.CityName,A.AreaDistribusiID,C.AreaDistribusiName,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from City as A,Propinsi as B,AreaDistribusi as C where A.PropinsiID = B.ID and A.AreaDistribusiID = C.ID and A.RowStatus = 0 and A.ID > 0 and A.PropinsiId = " + intPropinsiId);
            strError = dataAccess.Error;
            arrCity = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrCity.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrCity.Add(new City());

            return arrCity;
        }

        public ArrayList RetrieveByAllCity()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select distinct ID,CityName from City where RowStatus>-1 order by CityName");
            strError = dataAccess.Error;
            arrCity = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrCity.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrCity.Add(new City());

            return arrCity;
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.PropinsiID,B.NamaPropinsi,A.CityName,A.AreaDistribusiID,C.AreaDistribusiName,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from City as A,Propinsi as B,AreaDistribusi as C where A.PropinsiID = B.ID and A.AreaDistribusiID = C.ID and A.RowStatus = 0 and A.ID > 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrCity = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrCity.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrCity.Add(new City());

            return arrCity;
        }

        public ArrayList RetrieveByAreaDistribusi(int areaDistribusiID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.PropinsiID,B.NamaPropinsi,A.CityName,A.AreaDistribusiID,C.AreaDistribusiName,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from City as A,Propinsi as B,AreaDistribusi as C where A.PropinsiID = B.ID and A.AreaDistribusiID = C.ID and A.RowStatus = 0 and A.ID > 0 and A.AreaDistribusiID=" + areaDistribusiID);
            strError = dataAccess.Error;
            arrCity = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrCity.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrCity.Add(new City());

            return arrCity;
        }
        public ArrayList RetrieveByWilayahHBM()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.PropinsiID,B.NamaPropinsi,A.CityName,A.AreaDistribusiID,C.AreaDistribusiName,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from City as A,Propinsi as B,AreaDistribusi as C where A.PropinsiID = B.ID and A.AreaDistribusiID = C.ID and A.RowStatus = 0 and A.ID > 0 and A.WilayahHBM=1 order by CityName");
            strError = dataAccess.Error;
            arrCity = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrCity.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrCity.Add(new City());

            return arrCity;
        }
        public City RetrieveByID(int id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.PropinsiID,B.NamaPropinsi,A.CityName,A.AreaDistribusiID,C.AreaDistribusiName,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from City as A,Propinsi as B,AreaDistribusi as C where A.PropinsiID = B.ID and A.AreaDistribusiID = C.ID and A.RowStatus = 0 and A.ID > 0 and A.ID = " + id );
            strError = dataAccess.Error;
            arrCity = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new City();
        }
        public City GenerateObject(SqlDataReader sqlDataReader)
        {
            objCity = new City();
            objCity.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objCity.PropinsiID = Convert.ToInt32(sqlDataReader["PropinsiID"]);
            objCity.NamaPropinsi = sqlDataReader["NamaPropinsi"].ToString();
            objCity.CityName = sqlDataReader["CityName"].ToString();
            objCity.AreaDistribusiID = Convert.ToInt32(sqlDataReader["AreaDistribusiID"]);
            objCity.AreaDistribusiName = sqlDataReader["AreaDistribusiName"].ToString();
            objCity.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objCity.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objCity.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objCity.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objCity.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objCity;

        }
        public City GenerateObject2(SqlDataReader sqlDataReader)
        {
            objCity = new City();
            objCity.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objCity.CityName = sqlDataReader["CityName"].ToString();
            return objCity;

        }

    }
}
