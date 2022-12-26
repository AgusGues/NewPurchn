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
    public class HariLiburFacade : AbstractFacade
    {
        private HariLibur objHariLibur = new HariLibur();
        private ArrayList arrHariLibur;
        private List<SqlParameter> sqlListParam;

        public HariLiburFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objHariLibur = (HariLibur)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@TglLibur", objHariLibur.TglLibur));
                sqlListParam.Add(new SqlParameter("@Keterangan", objHariLibur.Keterangan));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objHariLibur.CreatedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertHariLibur");

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
                objHariLibur = (HariLibur)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objHariLibur.ID));
                sqlListParam.Add(new SqlParameter("@TglLibur", objHariLibur.TglLibur));
                sqlListParam.Add(new SqlParameter("@Keterangan", objHariLibur.Keterangan));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objHariLibur.CreatedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateHariLibur");

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
                objHariLibur = (HariLibur)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objHariLibur.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objHariLibur.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteHariLibur");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from HariLibur");
            strError = dataAccess.Error;
            arrHariLibur = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrHariLibur.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrHariLibur.Add(new HariLibur());

            return arrHariLibur;
        }

        public HariLibur RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from HariLibur where ID = " + Id);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new HariLibur();
        }

        public HariLibur RetrieveByCode(string tglLibur)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from HariLibur where convert(varchar,TglLibur,112) = '" + tglLibur + "'");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new HariLibur();
        }


        public HariLibur RetrieveByName(string keterangan)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from HariLibur where Keterangan = '" + keterangan + "'");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new HariLibur();
        }




        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from HariLibur where RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrHariLibur = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrHariLibur.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrHariLibur.Add(new HariLibur());

            return arrHariLibur;
        }

        public HariLibur GenerateObject(SqlDataReader sqlDataReader)
        {
            objHariLibur = new HariLibur();
            objHariLibur.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objHariLibur.TglLibur = Convert.ToDateTime(sqlDataReader["TglLibur"]);
            objHariLibur.Keterangan = sqlDataReader["Keterangan"].ToString();
            objHariLibur.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objHariLibur.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objHariLibur.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objHariLibur.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objHariLibur.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objHariLibur;

        }
    }
}

