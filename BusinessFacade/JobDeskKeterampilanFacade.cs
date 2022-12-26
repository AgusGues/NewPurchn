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
    public class JobDeskKeterampilanFacade : AbstractTransactionFacade
    {
        private JobdeskDetail objJobdeskDetail = new JobdeskDetail();
        private ArrayList arrJobdeskDetail;
        //private List<SqlParameter> sqlListParam;
        //private string scheduleNo = string.Empty;

        public JobDeskKeterampilanFacade(object objDomain)
            : base(objDomain)
        {
            objJobdeskDetail = (JobdeskDetail)objDomain;
        }

        public JobDeskKeterampilanFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {

                List<SqlParameter> sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@JOBDESKID", objJobdeskDetail.JOBDESKID));
                sqlListParam.Add(new SqlParameter("@Keterampilan", objJobdeskDetail.Keterampilan));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertJobDeskKeterampilan");

                strError = transManager.Error;

                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Update(TransactionManager transManager)
        {
            try
            {

                List<SqlParameter> sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@tampil", "updateKeterampilan"));
                sqlListParam.Add(new SqlParameter("@ID", objJobdeskDetail.ID));
                sqlListParam.Add(new SqlParameter("@Bawahan", objJobdeskDetail.Bawahan));

                sqlListParam.Add(new SqlParameter("@BagianName", objJobdeskDetail.BagianName));
                sqlListParam.Add(new SqlParameter("@Atasan", objJobdeskDetail.Atasan));
                sqlListParam.Add(new SqlParameter("@TglSusun", objJobdeskDetail.TglSusun));
                sqlListParam.Add(new SqlParameter("@Revisi", objJobdeskDetail.Revisi));
                sqlListParam.Add(new SqlParameter("@TUJabatan", objJobdeskDetail.TUJabatan));
                sqlListParam.Add(new SqlParameter("@TPJabatan", objJobdeskDetail.TPJabatan));
                sqlListParam.Add(new SqlParameter("@HubunganKerja", objJobdeskDetail.HubunganKerja));
                sqlListParam.Add(new SqlParameter("@TanggungJawab", objJobdeskDetail.TanggungJawab));
                sqlListParam.Add(new SqlParameter("@Wewenang", objJobdeskDetail.Wewenang));
                sqlListParam.Add(new SqlParameter("@Pendidikan", objJobdeskDetail.Pendidikan));
                sqlListParam.Add(new SqlParameter("@Pengalaman", objJobdeskDetail.Pengalaman));
                sqlListParam.Add(new SqlParameter("@Usia", objJobdeskDetail.Usia));
                sqlListParam.Add(new SqlParameter("@Pengetahuan", objJobdeskDetail.Pengetahuan));
                sqlListParam.Add(new SqlParameter("@Keterampilan", objJobdeskDetail.Keterampilan));
                sqlListParam.Add(new SqlParameter("@Fisik", objJobdeskDetail.Fisik));
                sqlListParam.Add(new SqlParameter("@NonFisik", objJobdeskDetail.NonFisik));


                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateJobDeskInputan");

                strError = transManager.Error;

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
            string strSQL = "select * from JobDeskKeterampilan";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrJobdeskDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobdeskDetail.Add(GenerateObject(sqlDataReader));
                }
            }

            return arrJobdeskDetail;
        }

        public JobdeskDetail GenerateObject(SqlDataReader sqlDataReader)
        {
            objJobdeskDetail = new JobdeskDetail();
            objJobdeskDetail.JOBDESKID = Convert.ToInt32(sqlDataReader["JOBDESKID"]);
            objJobdeskDetail.Keterampilan = sqlDataReader["Keterampilan"].ToString();

            return objJobdeskDetail;
        }

        public override int Delete(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

    }
}
