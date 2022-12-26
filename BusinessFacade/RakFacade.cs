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
    public class RakFacade : AbstractFacade
    {
        private Rak objRak = new Rak();
        private ArrayList arrRak;
        private List<SqlParameter> sqlListParam;


        public RakFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objRak = (Rak)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@RakNo", objRak.RakNo));
                sqlListParam.Add(new SqlParameter("@LotNo", objRak.LotNo));
                sqlListParam.Add(new SqlParameter("@Keterangan", objRak.Keterangan));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objRak.CreatedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objRak.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertRak");

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
                objRak = (Rak)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objRak.ID));
                sqlListParam.Add(new SqlParameter("@RakNo", objRak.RakNo));
                sqlListParam.Add(new SqlParameter("@LotNo", objRak.LotNo));
                sqlListParam.Add(new SqlParameter("@Keterangan", objRak.Keterangan));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objRak.CreatedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedTime", objRak.LastModifiedTime));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateRak");

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
                objRak = (Rak)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objRak.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objRak.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteRak");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 50 A.ID,A.RakNo,A.LotNo,A.Keterangan,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from Rak as A where A.RowStatus = 0");
            strError = dataAccess.Error;
            arrRak = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrRak.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrRak.Add(new Rak());

            return arrRak;
        }

        public Rak RetrieveById(int Id)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.RakNo,A.LotNo,A.Keterangan,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from Rak as A where A.RowStatus = 0 and A.ID = " + Id);
            strError = dataAccess.Error;
            arrRak = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Rak();
        }

        public Rak RetrieveByCode(string rakNo)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select select A.ID,A.RakNo,A.LotNo,A.Keterangan,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from Rak as A where A.RowStatus = 0 and A.RakNo = '" + rakNo + "'");
            strError = dataAccess.Error;
            arrRak = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Rak();
        }


        public Rak RetrieveByLot(string lotNo)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.RakNo,A.LotNo,A.Keterangan,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from Rak as A where A.RowStatus = 0 and A.LotNo = '" + lotNo + "'");
            strError = dataAccess.Error;
            arrRak = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Rak();
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.RakNo,A.LotNo,A.Keterangan,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from Rak as A where A.RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrRak = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrRak.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrRak.Add(new Rak());

            return arrRak;
        }

        public Rak GenerateObject(SqlDataReader sqlDataReader)
        {
            objRak = new Rak();
            objRak.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objRak.RakNo = sqlDataReader["RakNo"].ToString();
            objRak.LotNo = sqlDataReader["LotNo"].ToString();
            objRak.Keterangan = sqlDataReader["Keterangan"].ToString();
            objRak.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objRak.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objRak.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objRak.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objRak.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objRak;
        }
    }
}
