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
    public class JobDeskDetailFacade : AbstractTransactionFacade
    {
        private JobdeskDetail objJobdeskDetail = new JobdeskDetail();
        private ArrayList arrJobdeskDetail;
        private List<SqlParameter> sqlListParam;
        //private string scheduleNo = string.Empty;

        public JobDeskDetailFacade(object objDomain)
            : base(objDomain)
        {
            objJobdeskDetail = (JobdeskDetail)objDomain;
        }

        public JobDeskDetailFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {

                List<SqlParameter> sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@JOBDESKID", objJobdeskDetail.JOBDESKID));
                //sqlListParam.Add(new SqlParameter("@TUJabatan", objJobdeskDetail.TUJabatan));
                //sqlListParam.Add(new SqlParameter("@TPJabatan", objJobdeskDetail.TPJabatan));
                //sqlListParam.Add(new SqlParameter("@HubunganKerja", objJobdeskDetail.HubunganKerja));
                //sqlListParam.Add(new SqlParameter("@TanggungJawab", objJobdeskDetail.TanggungJawab));
                //sqlListParam.Add(new SqlParameter("@Wewenang", objJobdeskDetail.Wewenang));
                ////sqlListParam.Add(new SqlParameter("@Pendidikan", objJobdeskDetail.Pendidikan));
                //sqlListParam.Add(new SqlParameter("@Pengetahuan", objJobdeskDetail.Pengetahuan));
                //sqlListParam.Add(new SqlParameter("@Keterampilan", objJobdeskDetail.Keterampilan));
                //sqlListParam.Add(new SqlParameter("@Fisik", objJobdeskDetail.Fisik));
                //sqlListParam.Add(new SqlParameter("@NonFisik", objJobdeskDetail.NonFisik));
                sqlListParam.Add(new SqlParameter("@Bawahan", objJobdeskDetail.Bawahan));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertJobDeskDetail");

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
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@tampil", "update"));
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

        //private string Criteria()
        //{
        //    string jobdeskDetailID = (System.Web.HttpContext.Current.Session["where"] != null) ?
        //     System.Web.HttpContext.Current.Session["where"].ToString() : string.Empty;
        //    return jobdeskDetailID;
        //}
        //public override ArrayList RetrieveBatal()
        //{
        //    string strSQL = "SELECT * FROM JobDesk AS jd LEFT JOIN JobDeskDetail AS jdd ON jd.ID=jdd.JOBDESKID WHERE " +
        //                    "jd.Status=0 AND jd.RowStatus>-1 AND jdd.RowStatus>-1 " + Criteria();
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        //    strError = dataAccess.Error;
        //    arrJobdeskDetail = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrJobdeskDetail.Add(GenerateObject(sqlDataReader));
        //        }
        //    }

        //    return arrJobdeskDetail;
        //}
                
        public int CancelJOBDESKDetail(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objJobdeskDetail.ID));

                int intResult = transManager.DoTransaction(sqlListParam, "spCancelJOBDESKDetail");

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
            string strSQL = "select * from JobDeskDetail";
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
               
        public ArrayList RetrieveByJOBDESKID(int Id)
        {
            string strSQL = "SELECT * FROM JobDesk AS jd LEFT JOIN JobDeskDetail AS jdd ON jd.ID=jdd.JOBDESKID WHERE " +
                            "jd.Status=0 AND jd.RowStatus>-1 AND jdd.RowStatus>-1 AND jdd.JOBDESKID= " + Id + " ORDER BY jd.ID DESC ";
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

        public ArrayList RetrieveByJOBDESKID2(int Id2)
        {
            string strSQL = "SELECT * FROM JobDesk AS jd LEFT JOIN JobDeskDetail AS jdd ON jd.ID=jdd.JOBDESKID WHERE " +
                            "jd.Status=0 AND jd.RowStatus>-1 AND jdd.RowStatus>-1 AND jdd.JOBDESKID= " + Id2 + " ORDER BY jd.ID DESC ";
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

        public ArrayList RetrieveByID(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //string strSQL = "SELECT * FROM JobDesk AS jd LEFT JOIN JobDeskDetail AS jdd ON jd.ID=jdd.JOBDESKID WHERE " +
            //                "jd.RowStatus>-1 AND jdd.RowStatus>-1 AND jdd.JOBDESKID= " + Id + " ";

            string strSQL = "SELECT jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdbw.Bawahan, jdbw.ID, " +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskBawahan AS jdbw ON jd.ID=jdbw.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdbw.RowStatus>-1 and jd.ID='" + Id + "' ";

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
            else
                arrJobdeskDetail.Add(new JobdeskDetail());

            return arrJobdeskDetail;
        }

        public ArrayList RetrieveByIDTUJ(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "SELECT jdbw.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdbw.TujuanUmumJabatan," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskTUJabatan AS jdbw ON jd.ID=jdbw.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdbw.RowStatus>-1 and jd.ID='" + Id + "' ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrJobdeskDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobdeskDetail.Add(GenerateObjectTUJ(sqlDataReader));
                }
            }
            else
                arrJobdeskDetail.Add(new JobdeskDetail());

            return arrJobdeskDetail;
        }

        public ArrayList RetrieveByIDTPJ(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "SELECT jdbw.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdbw.TugasPokokJabatan," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskTPJabatan AS jdbw ON jd.ID=jdbw.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdbw.RowStatus>-1 and jd.ID='" + Id + "' ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrJobdeskDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobdeskDetail.Add(GenerateObjectTPJ(sqlDataReader));
                }
            }
            else
                arrJobdeskDetail.Add(new JobdeskDetail());

            return arrJobdeskDetail;
        }

        public ArrayList RetrieveByIDHK(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "SELECT jdbw.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdbw.HubunganKerja," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskHK AS jdbw ON jd.ID=jdbw.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdbw.RowStatus>-1 and jd.ID='" + Id + "' ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrJobdeskDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobdeskDetail.Add(GenerateObjectHK(sqlDataReader));
                }
            }
            else
                arrJobdeskDetail.Add(new JobdeskDetail());

            return arrJobdeskDetail;
        }

        public ArrayList RetrieveByIDTJ(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "SELECT jdbw.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdbw.TanggungJawab," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskTJ AS jdbw ON jd.ID=jdbw.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdbw.RowStatus>-1 and jd.ID='" + Id + "' ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrJobdeskDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobdeskDetail.Add(GenerateObjectTJ(sqlDataReader));
                }
            }
            else
                arrJobdeskDetail.Add(new JobdeskDetail());

            return arrJobdeskDetail;
        }

        public ArrayList RetrieveByIDW(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "SELECT jdbw.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdbw.Wewenang," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskWewenang AS jdbw ON jd.ID=jdbw.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdbw.RowStatus>-1 and jd.ID='" + Id + "' ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrJobdeskDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobdeskDetail.Add(GenerateObjectW(sqlDataReader));
                }
            }
            else
                arrJobdeskDetail.Add(new JobdeskDetail());

            return arrJobdeskDetail;
        }

        public ArrayList RetrieveByIDPend(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "SELECT jdbw.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdbw.Pendidikan," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskPendidikan AS jdbw ON jd.ID=jdbw.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdbw.RowStatus>-1 and jd.ID='" + Id + "' ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrJobdeskDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobdeskDetail.Add(GenerateObjectPend(sqlDataReader));
                }
            }
            else
                arrJobdeskDetail.Add(new JobdeskDetail());

            return arrJobdeskDetail;
        }

        public ArrayList RetrieveByIDPeng(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "SELECT jdbw.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdbw.Pengalaman," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskPengalaman AS jdbw ON jd.ID=jdbw.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdbw.RowStatus>-1 and jd.ID='" + Id + "' ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrJobdeskDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobdeskDetail.Add(GenerateObjectPeng(sqlDataReader));
                }
            }
            else
                arrJobdeskDetail.Add(new JobdeskDetail());

            return arrJobdeskDetail;
        }

        public ArrayList RetrieveByIDPengt(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "SELECT jdbw.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdbw.Pengetahuan," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskPengetahuan AS jdbw ON jd.ID=jdbw.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdbw.RowStatus>-1 and jd.ID='" + Id + "' ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrJobdeskDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobdeskDetail.Add(GenerateObjectPengt(sqlDataReader));
                }
            }
            else
                arrJobdeskDetail.Add(new JobdeskDetail());

            return arrJobdeskDetail;
        }

        public ArrayList RetrieveByIDKet(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "SELECT jdbw.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdbw.Keterampilan," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskKeterampilan AS jdbw ON jd.ID=jdbw.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdbw.RowStatus>-1 and jd.ID='" + Id + "' ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrJobdeskDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobdeskDetail.Add(GenerateObjectKet(sqlDataReader));
                }
            }
            else
                arrJobdeskDetail.Add(new JobdeskDetail());

            return arrJobdeskDetail;
        }

        public ArrayList RetrieveByIDFisik(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "SELECT jdbw.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdbw.Fisik," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskFisik AS jdbw ON jd.ID=jdbw.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdbw.RowStatus>-1 and jd.ID='" + Id + "' ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrJobdeskDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobdeskDetail.Add(GenerateObjectFisik(sqlDataReader));
                }
            }
            else
                arrJobdeskDetail.Add(new JobdeskDetail());

            return arrJobdeskDetail;
        }

        public ArrayList RetrieveByIDNonFisik(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "SELECT jdbw.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdbw.NonFisik," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskNonFisik AS jdbw ON jd.ID=jdbw.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdbw.RowStatus>-1 and jd.ID='" + Id + "' ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrJobdeskDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobdeskDetail.Add(GenerateObjectNonFisik(sqlDataReader));
                }
            }
            else
                arrJobdeskDetail.Add(new JobdeskDetail());

            return arrJobdeskDetail;
        }

        public ArrayList RetrieveByIDUsia(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "SELECT jdbw.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdbw.Usia," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskUsia AS jdbw ON jd.ID=jdbw.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdbw.RowStatus>-1 and jd.ID='" + Id + "' ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrJobdeskDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobdeskDetail.Add(GenerateObjectUsia(sqlDataReader));
                }
            }
            else
                arrJobdeskDetail.Add(new JobdeskDetail());

            return arrJobdeskDetail;
        }

        public JobdeskDetail GenerateObject(SqlDataReader sqlDataReader)
        {
            objJobdeskDetail = new JobdeskDetail();
            objJobdeskDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            //objJobdeskDetail.JOBDESKID = Convert.ToInt32(sqlDataReader["JOBDESKID"]);
            objJobdeskDetail.BagianName = sqlDataReader["Jabatan"].ToString();
            objJobdeskDetail.TglSusun = Convert.ToDateTime(sqlDataReader["TglSusun"]);
            objJobdeskDetail.Approval = sqlDataReader["Apv"].ToString();
            objJobdeskDetail.Atasan = sqlDataReader["Atasan"].ToString();
            objJobdeskDetail.Bawahan = sqlDataReader["Bawahan"].ToString();
            objJobdeskDetail.Revisi = Convert.ToInt32(sqlDataReader["Revisi"]);
            objJobdeskDetail.Status = Convert.ToInt32(sqlDataReader["Status"]);
            //objJobdeskDetail.Approval = Convert.ToInt32(sqlDataReader["Approval"]);
            //objJobdeskDetail.Bawahan = sqlDataReader["Bawahan"].ToString();

            return objJobdeskDetail;
        }
        public JobdeskDetail GenerateObjectTUJ(SqlDataReader sqlDataReader)
        {
            objJobdeskDetail = new JobdeskDetail();
            objJobdeskDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objJobdeskDetail.BagianName = sqlDataReader["Jabatan"].ToString();
            objJobdeskDetail.TglSusun = Convert.ToDateTime(sqlDataReader["TglSusun"]);
            objJobdeskDetail.Approval = sqlDataReader["Apv"].ToString();
            objJobdeskDetail.Atasan = sqlDataReader["Atasan"].ToString();
            objJobdeskDetail.TUJabatan = sqlDataReader["TujuanUmumJabatan"].ToString();
            objJobdeskDetail.Revisi = Convert.ToInt32(sqlDataReader["Revisi"]);
            objJobdeskDetail.Status = Convert.ToInt32(sqlDataReader["Status"]);

            return objJobdeskDetail;
        }
        public JobdeskDetail GenerateObjectTPJ(SqlDataReader sqlDataReader)
        {
            objJobdeskDetail = new JobdeskDetail();
            objJobdeskDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objJobdeskDetail.BagianName = sqlDataReader["Jabatan"].ToString();
            objJobdeskDetail.TglSusun = Convert.ToDateTime(sqlDataReader["TglSusun"]);
            objJobdeskDetail.Approval = sqlDataReader["Apv"].ToString();
            objJobdeskDetail.Atasan = sqlDataReader["Atasan"].ToString();
            objJobdeskDetail.TPJabatan = sqlDataReader["TugasPokokJabatan"].ToString();
            objJobdeskDetail.Revisi = Convert.ToInt32(sqlDataReader["Revisi"]);
            objJobdeskDetail.Status = Convert.ToInt32(sqlDataReader["Status"]);

            return objJobdeskDetail;
        }
        public JobdeskDetail GenerateObjectHK(SqlDataReader sqlDataReader)
        {
            objJobdeskDetail = new JobdeskDetail();
            objJobdeskDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objJobdeskDetail.BagianName = sqlDataReader["Jabatan"].ToString();
            objJobdeskDetail.TglSusun = Convert.ToDateTime(sqlDataReader["TglSusun"]);
            objJobdeskDetail.Approval = sqlDataReader["Apv"].ToString();
            objJobdeskDetail.Atasan = sqlDataReader["Atasan"].ToString();
            objJobdeskDetail.HubunganKerja = sqlDataReader["HubunganKerja"].ToString();
            objJobdeskDetail.Revisi = Convert.ToInt32(sqlDataReader["Revisi"]);
            objJobdeskDetail.Status = Convert.ToInt32(sqlDataReader["Status"]);

            return objJobdeskDetail;
        }
        public JobdeskDetail GenerateObjectTJ(SqlDataReader sqlDataReader)
        {
            objJobdeskDetail = new JobdeskDetail();
            objJobdeskDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objJobdeskDetail.BagianName = sqlDataReader["Jabatan"].ToString();
            objJobdeskDetail.TglSusun = Convert.ToDateTime(sqlDataReader["TglSusun"]);
            objJobdeskDetail.Approval = sqlDataReader["Apv"].ToString();
            objJobdeskDetail.Atasan = sqlDataReader["Atasan"].ToString();
            objJobdeskDetail.TanggungJawab = sqlDataReader["TanggungJawab"].ToString();
            objJobdeskDetail.Revisi = Convert.ToInt32(sqlDataReader["Revisi"]);
            objJobdeskDetail.Status = Convert.ToInt32(sqlDataReader["Status"]);

            return objJobdeskDetail;
        }
        public JobdeskDetail GenerateObjectW(SqlDataReader sqlDataReader)
        {
            objJobdeskDetail = new JobdeskDetail();
            objJobdeskDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objJobdeskDetail.BagianName = sqlDataReader["Jabatan"].ToString();
            objJobdeskDetail.TglSusun = Convert.ToDateTime(sqlDataReader["TglSusun"]);
            objJobdeskDetail.Approval = sqlDataReader["Apv"].ToString();
            objJobdeskDetail.Atasan = sqlDataReader["Atasan"].ToString();
            objJobdeskDetail.Wewenang = sqlDataReader["Wewenang"].ToString();
            objJobdeskDetail.Revisi = Convert.ToInt32(sqlDataReader["Revisi"]);
            objJobdeskDetail.Status = Convert.ToInt32(sqlDataReader["Status"]);

            return objJobdeskDetail;
        }
        public JobdeskDetail GenerateObjectPend(SqlDataReader sqlDataReader)
        {
            objJobdeskDetail = new JobdeskDetail();
            objJobdeskDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objJobdeskDetail.BagianName = sqlDataReader["Jabatan"].ToString();
            objJobdeskDetail.TglSusun = Convert.ToDateTime(sqlDataReader["TglSusun"]);
            objJobdeskDetail.Approval = sqlDataReader["Apv"].ToString();
            objJobdeskDetail.Atasan = sqlDataReader["Atasan"].ToString();
            objJobdeskDetail.Pendidikan = sqlDataReader["Pendidikan"].ToString();
            objJobdeskDetail.Revisi = Convert.ToInt32(sqlDataReader["Revisi"]);
            objJobdeskDetail.Status = Convert.ToInt32(sqlDataReader["Status"]);

            return objJobdeskDetail;
        }
        public JobdeskDetail GenerateObjectPeng(SqlDataReader sqlDataReader)
        {
            objJobdeskDetail = new JobdeskDetail();
            objJobdeskDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objJobdeskDetail.BagianName = sqlDataReader["Jabatan"].ToString();
            objJobdeskDetail.TglSusun = Convert.ToDateTime(sqlDataReader["TglSusun"]);
            objJobdeskDetail.Approval = sqlDataReader["Apv"].ToString();
            objJobdeskDetail.Atasan = sqlDataReader["Atasan"].ToString();
            objJobdeskDetail.Pengalaman = sqlDataReader["Pengalaman"].ToString();
            objJobdeskDetail.Revisi = Convert.ToInt32(sqlDataReader["Revisi"]);
            objJobdeskDetail.Status = Convert.ToInt32(sqlDataReader["Status"]);

            return objJobdeskDetail;
        }
        public JobdeskDetail GenerateObjectPengt(SqlDataReader sqlDataReader)
        {
            objJobdeskDetail = new JobdeskDetail();
            objJobdeskDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objJobdeskDetail.BagianName = sqlDataReader["Jabatan"].ToString();
            objJobdeskDetail.TglSusun = Convert.ToDateTime(sqlDataReader["TglSusun"]);
            objJobdeskDetail.Approval = sqlDataReader["Apv"].ToString();
            objJobdeskDetail.Atasan = sqlDataReader["Atasan"].ToString();
            objJobdeskDetail.Pengetahuan = sqlDataReader["Pengetahuan"].ToString();
            objJobdeskDetail.Revisi = Convert.ToInt32(sqlDataReader["Revisi"]);
            objJobdeskDetail.Status = Convert.ToInt32(sqlDataReader["Status"]);

            return objJobdeskDetail;
        }
        public JobdeskDetail GenerateObjectKet(SqlDataReader sqlDataReader)
        {
            objJobdeskDetail = new JobdeskDetail();
            objJobdeskDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objJobdeskDetail.BagianName = sqlDataReader["Jabatan"].ToString();
            objJobdeskDetail.TglSusun = Convert.ToDateTime(sqlDataReader["TglSusun"]);
            objJobdeskDetail.Approval = sqlDataReader["Apv"].ToString();
            objJobdeskDetail.Atasan = sqlDataReader["Atasan"].ToString();
            objJobdeskDetail.Keterampilan = sqlDataReader["Keterampilan"].ToString();
            objJobdeskDetail.Revisi = Convert.ToInt32(sqlDataReader["Revisi"]);
            objJobdeskDetail.Status = Convert.ToInt32(sqlDataReader["Status"]);

            return objJobdeskDetail;
        }
        public JobdeskDetail GenerateObjectFisik(SqlDataReader sqlDataReader)
        {
            objJobdeskDetail = new JobdeskDetail();
            objJobdeskDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objJobdeskDetail.BagianName = sqlDataReader["Jabatan"].ToString();
            objJobdeskDetail.TglSusun = Convert.ToDateTime(sqlDataReader["TglSusun"]);
            objJobdeskDetail.Approval = sqlDataReader["Apv"].ToString();
            objJobdeskDetail.Atasan = sqlDataReader["Atasan"].ToString();
            objJobdeskDetail.Fisik = sqlDataReader["Fisik"].ToString();
            objJobdeskDetail.Revisi = Convert.ToInt32(sqlDataReader["Revisi"]);
            objJobdeskDetail.Status = Convert.ToInt32(sqlDataReader["Status"]);

            return objJobdeskDetail;
        }
        public JobdeskDetail GenerateObjectNonFisik(SqlDataReader sqlDataReader)
        {
            objJobdeskDetail = new JobdeskDetail();
            objJobdeskDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objJobdeskDetail.BagianName = sqlDataReader["Jabatan"].ToString();
            objJobdeskDetail.TglSusun = Convert.ToDateTime(sqlDataReader["TglSusun"]);
            objJobdeskDetail.Approval = sqlDataReader["Apv"].ToString();
            objJobdeskDetail.Atasan = sqlDataReader["Atasan"].ToString();
            objJobdeskDetail.NonFisik = sqlDataReader["NonFisik"].ToString();
            objJobdeskDetail.Revisi = Convert.ToInt32(sqlDataReader["Revisi"]);
            objJobdeskDetail.Status = Convert.ToInt32(sqlDataReader["Status"]);

            return objJobdeskDetail;
        }
        public JobdeskDetail GenerateObjectUsia(SqlDataReader sqlDataReader)
        {
            objJobdeskDetail = new JobdeskDetail();
            objJobdeskDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objJobdeskDetail.BagianName = sqlDataReader["Jabatan"].ToString();
            objJobdeskDetail.TglSusun = Convert.ToDateTime(sqlDataReader["TglSusun"]);
            objJobdeskDetail.Approval = sqlDataReader["Apv"].ToString();
            objJobdeskDetail.Atasan = sqlDataReader["Atasan"].ToString();
            objJobdeskDetail.Usia = sqlDataReader["Usia"].ToString();
            objJobdeskDetail.Revisi = Convert.ToInt32(sqlDataReader["Revisi"]);
            objJobdeskDetail.Status = Convert.ToInt32(sqlDataReader["Status"]);

            return objJobdeskDetail;
        }

        //public override int Update(DataAccessLayer.TransactionManager transManager)
        //{
        //    throw new NotImplementedException();
        //}

        public override int Delete(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }
    }
}
