using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Web;
using Domain;
using DataAccessLayer;

namespace BusinessFacade
{
    public class JobDeskFacade : AbstractTransactionFacade
    {
        private JobDesk objJobDesk = new JobDesk();
        //private JobdeskDetail objJobdeskDetail = new JobdeskDetail();
        private ArrayList arrJobDesk;
        private List<SqlParameter> sqlListParam;
        private ArrayList arrData = new ArrayList();

        public JobDeskFacade(object objDomain)
            : base(objDomain)
        {
            objJobDesk = (JobDesk)objDomain;
            //objJobdeskDetail = (JobdeskDetail)objDomain;
        }

        public JobDeskFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@DeptID", objJobDesk.DeptID));
                sqlListParam.Add(new SqlParameter("@BagianName", objJobDesk.BagianName));
                sqlListParam.Add(new SqlParameter("@Atasan", objJobDesk.Atasan));
                sqlListParam.Add(new SqlParameter("@TglSusun", objJobDesk.TglSusun));
                sqlListParam.Add(new SqlParameter("@Revisi", objJobDesk.Revisi));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objJobDesk.CreatedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertJobDesk");

                strError = transManager.Error;

                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        //public override int Update0(TransactionManager transManager)
        //{
        //    try
        //    {
        //        sqlListParam = new List<SqlParameter>();
        //        sqlListParam.Add(new SqlParameter("@ID", objJobDesk.ID));
        //        sqlListParam.Add(new SqlParameter("@BagianName", objJobDesk.BagianName));
        //        sqlListParam.Add(new SqlParameter("@Atasan", objJobDesk.Atasan));
        //        sqlListParam.Add(new SqlParameter("@TglSusun", objJobDesk.TglSusun));
        //        sqlListParam.Add(new SqlParameter("@Revisi", objJobDesk.Revisi));

        //        int intResult = transManager.DoTransaction(sqlListParam, "spUpdateJobDeskInputan");

        //        strError = transManager.Error;

        //        return intResult;
        //    }
        //    catch (Exception ex)
        //    {
        //        strError = ex.Message;
        //        return -1;
        //    }
        //}

        public override int Update(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objJobDesk.ID));
                sqlListParam.Add(new SqlParameter("@Approval", objJobDesk.Approval));
                sqlListParam.Add(new SqlParameter("@ApprovedDate", objJobDesk.ApprovedDate));
                //sqlListParam.Add(new SqlParameter("@DeptID", objJobDesk.DeptID));
                //sqlListParam.Add(new SqlParameter("@Jabatan", objJobDesk.Jabatan));
                sqlListParam.Add(new SqlParameter("@Atasan", objJobDesk.Atasan));
                //sqlListParam.Add(new SqlParameter("@Bawahan", objJobDesk.Bawahan));
                //sqlListParam.Add(new SqlParameter("@TglSusun", objJobDesk.TglSusun));
                //sqlListParam.Add(new SqlParameter("@Revisi", objJobDesk.Revisi));
                //sqlListParam.Add(new SqlParameter("@Status", objJobDesk.Status));
                //sqlListParam.Add(new SqlParameter("@Approval", objJobDesk.Approval));
                //sqlListParam.Add(new SqlParameter("@ApprovedDate", objJobDesk.ApprovedDate));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateJobDesk");

                strError = transManager.Error;

                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Delete(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objJobDesk.ID));
                sqlListParam.Add(new SqlParameter("@DeptID", objJobDesk.DeptID));
                sqlListParam.Add(new SqlParameter("@Jabatan", objJobDesk.Jabatan));
                sqlListParam.Add(new SqlParameter("@Atasan", objJobDesk.Atasan));
                sqlListParam.Add(new SqlParameter("@Bawahan", objJobDesk.Bawahan));
                sqlListParam.Add(new SqlParameter("@TglSusun", objJobDesk.TglSusun));
                sqlListParam.Add(new SqlParameter("@Revisi", objJobDesk.Revisi));
                sqlListParam.Add(new SqlParameter("@Status", objJobDesk.Status));
                sqlListParam.Add(new SqlParameter("@Approval", objJobDesk.Approval));
                sqlListParam.Add(new SqlParameter("@ApprovedDate", objJobDesk.ApprovedDate));

                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteJobDesk");

                strError = transManager.Error;

                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public string Criteria { get; set; }
        public override ArrayList Retrieve()
        {
            string strSQL = "select * from JobDesk where RowStatus=0" + this.Criteria;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateObject(sqlDataReader));
                }
            }

            return arrJobDesk;
        }

        public ArrayList RetrieveByAll2()
        {
            string strSQL = "select * from JobDesk where RowStatus=0" + this.Criteria;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateObjectList(sqlDataReader));
                }
            }
            return arrJobDesk;
        }

        public JobDesk RetrieveByNo(string ID)
        {
            string strSQL = "SELECT * FROM JobDesk WHERE " +
                            "RowStatus>-1 and ID= '" + ID + "' ORDER BY ID DESC ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectList(sqlDataReader);
                    //arrJobDesk.Add(GenerateObjectList(sqlDataReader));
                }
            }
            return new JobDesk();
        }

        //public JobDesk RetrieveByNo2(string JOBDESK2)
        //{
        //    string strSQL = "SELECT * FROM JobDesk AS jd LEFT JOIN JobDeskDetail AS jdd ON jd.ID=jdd.JOBDESKID WHERE " +
        //                    "jd.Status=0 AND jd.RowStatus>-1 AND jdd.RowStatus>-1 and jd.JOBDESK_No= '" + JOBDESK2 + "' ORDER BY jd.ID DESC ";
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

        //    strError = dataAccess.Error;
        //    arrJobDesk = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            return GenerateObjectList(sqlDataReader);
        //            //arrJobDesk.Add(GenerateObjectList(sqlDataReader));
        //        }
        //    }
        //    return new JobDesk();
        //}

        public JobDesk RetrieveByCriteriaJOBDESK(string strField, string strValue)
        {
            //string strSQL = "select A.ID,A.NoSPP,A.HeadID,A.Minta,A.PermintaanType,A.SatuanID,A.GroupID,A.ItemTypeID,A.Jumlah,A.JumlahSisa," +
            //                "A.Keterangan,A.Sudah,A.FCetak,A.UserID,A.Pending,A.Inden,A.AlasanBatal,A.AlasanCLS,A.Status,A.Status," +
            //                "A.CreatedBy,A.CreatedTime as Tanggal,A.LastModifiedBy,A.LastModifiedTime,A.Approval,A.DepoID," +
            //                "A.Approvedate1,A.Approvedate2,A.Approvedate3 from SPP as A " +
            //                "where " + strField + " like '%" + strValue + "%'";
            string strSQL = "SELECT * FROM JobDesk AS jd LEFT JOIN JobDeskDetail AS jdd ON jd.ID=jdd.JOBDESKID WHERE " +
                            "jd.RowStatus>-1 AND jdd.RowStatus>-1 and " + strField + " like '%" + strValue + "%' ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            //pake A.Status
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.NoSPP,A.Minta,A.PermintaanType,A.SatuanID,A.GroupID,A.ItemTypeID,A.Jumlah,A.JumlahSisa,A.Keterangan,A.Sudah,A.FCetak,A.UserID,A.Pending,A.Inden,A.AlasanBatal,A.AlasanCLS,A.Status,A.Status,A.CreatedBy,A.CreatedTime as Tanggal,A.LastModifiedBy,A.LastModifiedTime,A.Approval,A.DepoID from SPP as A where A.Status = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectList(sqlDataReader);
                }
            }

            return new JobDesk();
        }

        public string without { get; set; }
        public ArrayList RetrieveBagian()
        {
            //string strSQL = "SELECT * FROM JobDesk AS jd LEFT JOIN JobDeskDetail AS jdd ON jd.ID=jdd.JOBDESKID WHERE " +
            //                "jd.Status=0 AND jd.RowStatus>-1 AND jdd.RowStatus>-1 ORDER BY jd.ID DESC ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT * FROM JobDesk AS jd LEFT JOIN JobDeskDetail AS jdd ON jd.ID=jdd.JOBDESKID WHERE jd.Status=0 AND jd.RowStatus>-1 AND jdd.RowStatus>-1" + this.without);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            return arrJobDesk;
        }

        public int GetLastUrutan(int tgl)
        {
            int urutan = 0;
            //string strSQL = "select top 1 urutan from (select CAST(SUBSTRING(JOBDESK_No,1,3) as int) urutan from JobDesk where YEAR(TglSusun)=" + tahun + ")A order by urutan desc";
            string strSQL = "select top 1 ID from JobDesk where DAY(TglSusun)=" + tgl + " order by ID desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    urutan = Convert.ToInt32(sqlDataReader["ID"]);
                }
            }
            return urutan;
        }

        public JobDesk GenerateObject(SqlDataReader sqlDataReader)
        {
            objJobDesk = new JobDesk();
            objJobDesk.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objJobDesk.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objJobDesk.BagianName = sqlDataReader["Jabatan"].ToString();
            objJobDesk.Atasan = sqlDataReader["Atasan"].ToString();
            objJobDesk.Bawahan = sqlDataReader["Bawahan"].ToString();
            objJobDesk.Revisi = Convert.ToInt32(sqlDataReader["Revisi"]);
            objJobDesk.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objJobDesk.Approval = Convert.ToInt32(sqlDataReader["Approval"]);

            return objJobDesk;
        }

        //public ArrayList RetrieveByAllWithStatus(int headID, string strField, string strValue)
        //{

        //}

        
        public ArrayList RetrieveByAll()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //string strSQL = "SELECT jd.ID, jdd.JOBDESKID, jd.JOBDESK_No, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.Pendidikan, jd.Pengalaman," +
            //                "jdd.TujuanUmumJabatan, jdd.TugasPokokJabatan, jdd.HubunganKerja, jdd.TanggungJawab, jdd.Wewenang, jdd.PendidikanPengalaman," +
            //                "jdd.Pengetahuan,jdd.Keterampilan, jdd.Fisik, jdd.NonFisik, jdd.Bawahan FROM JobDesk AS jd LEFT JOIN JobDeskDetail AS jdd ON jd.ID=jdd.JOBDESKID WHERE" +
            //                "" + this.Criteria + " AND jd.RowStatus>-1 AND jdd.RowStatus>-1 ORDER BY jd.ID DESC ";

            string strSQL = "SELECT * FROM JobDesk WHERE" +
                            "" + this.Criteria + " AND RowStatus>-1 ORDER BY ID DESC ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateObjectList(sqlDataReader));
                }
            }
            return arrJobDesk;
        }

        public ArrayList RetrieveForApproval(int AppLevel)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "SELECT * FROM JobDesk AS jd LEFT JOIN JobDeskDetail AS jdd ON jd.ID=jdd.JOBDESKID WHERE " +
                            "jd.Status=" + AppLevel + " AND jd.RowStatus>-1 AND jdd.RowStatus>-1 ORDER BY jd.ID DESC ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateObjectListApproval(sqlDataReader));
                }
            }
            return arrJobDesk;

            //arrJobDesk = new ArrayList();
            //int UserID = users.ID;

            //arrJobDesk = RetrieveForOpenJOBDESK(UserID, UserInput, Apv);
            //return arrJobDesk;
        }

        public ArrayList RetrieveForApproval2(int AppLevel)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "SELECT * FROM JobDesk AS jd LEFT JOIN JobDeskDetail AS jdd ON jd.ID=jdd.JOBDESKID WHERE " +
                            "jd.Status=" + AppLevel + " AND jd.RowStatus>-1 AND jdd.RowStatus>-1 and jd.Approval=1 ORDER BY jd.ID DESC ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateObjectListApproval(sqlDataReader));
                }
            }
            return arrJobDesk;

            //arrJobDesk = new ArrayList();
            //int UserID = users.ID;

            //arrJobDesk = RetrieveForOpenJOBDESK(UserID, UserInput, Apv);
            //return arrJobDesk;
        }

        public ArrayList RetriveJabatan()
        {
            ArrayList arrJobDesk = new ArrayList();
            string strSQL = "Select * from JobDeskBagian where DeptID in (select ID from Dept where" + this.Criteria + ") and RowStatus>-1 order by BagianName";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrJobDesk.Add(GenerateObjectBagian(sdr));
                }
            }
            return arrJobDesk;
        }

        public JobDesk GenerateObjectBagian(SqlDataReader sdr)
        {
            objJobDesk = new JobDesk();
            objJobDesk.BagianName = sdr["BagianName"].ToString();
            return objJobDesk;
        }

        public JobDesk GenerateObjectList(SqlDataReader sqlDataReader)
        {
            objJobDesk = new JobDesk();
            //objJobdeskDetail = new JobdeskDetail();

            objJobDesk.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objJobDesk.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objJobDesk.BagianName = sqlDataReader["Jabatan"].ToString();
            objJobDesk.Atasan = sqlDataReader["Atasan"].ToString();
            objJobDesk.TglSusun = Convert.ToDateTime(sqlDataReader["TglSusun"]);
            //objJobDesk.Bawahan = sqlDataReader["Bawahan"].ToString();
            //objJobDesk.Pendidikan = sqlDataReader["Pendidikan"].ToString();
            //objJobDesk.Pengalaman = sqlDataReader["Pengalaman"].ToString();
            objJobDesk.Revisi = Convert.ToInt32(sqlDataReader["Revisi"]);
            objJobDesk.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objJobDesk.Approval = Convert.ToInt32(sqlDataReader["Approval"]);
            objJobDesk.AlasanCancel = sqlDataReader["AlasanCancel"].ToString();
            //objJobDesk.TUJabatan = sqlDataReader["TujuanUmumJabatan"].ToString();
            //objJobDesk.TPJabatan = sqlDataReader["TugasPokokJabatan"].ToString();
            //objJobDesk.HubunganKerja = sqlDataReader["HubunganKerja"].ToString();
            //objJobDesk.TanggungJawab = sqlDataReader["TanggungJawab"].ToString();
            //objJobDesk.Wewenang = sqlDataReader["Wewenang"].ToString();
            //objJobDesk.Pengetahuan = sqlDataReader["Pengetahuan"].ToString();

            return objJobDesk;
        }

        public JobDesk GenerateObjectListApproval(SqlDataReader sqlDataReader)
        {
            objJobDesk = new JobDesk();
            //objJobdeskDetail = new JobdeskDetail();

            objJobDesk.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objJobDesk.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objJobDesk.BagianName = sqlDataReader["Jabatan"].ToString();
            objJobDesk.Atasan = sqlDataReader["Atasan"].ToString();
            objJobDesk.Bawahan = sqlDataReader["Bawahan"].ToString();
            objJobDesk.TglSusun = Convert.ToDateTime(sqlDataReader["TglSusun"].ToString());
            objJobDesk.Revisi = Convert.ToInt32(sqlDataReader["Revisi"]);
            objJobDesk.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objJobDesk.Approval = Convert.ToInt32(sqlDataReader["Approval"]);
            objJobDesk.TUJabatan = sqlDataReader["TujuanUmumJabatan"].ToString();
            objJobDesk.TPJabatan = sqlDataReader["TugasPokokJabatan"].ToString();
            objJobDesk.HubunganKerja = sqlDataReader["HubunganKerja"].ToString();
            objJobDesk.TanggungJawab = sqlDataReader["TanggungJawab"].ToString();
            objJobDesk.Wewenang = sqlDataReader["Wewenang"].ToString();

            return objJobDesk;
        }

        public string GetApv(string UserID)
        {
            string strsql = "select Approval as Apv from JobDeskUsers where User_ID=" + UserID + " and rowstatus>-1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return sqlDataReader["Apv"].ToString();
                }
            }

            return string.Empty;
        }

        public string GetStatusApv(string UserID)
        {
            string strsql = "select Approval from JobDesk where RowStatus > -1 and DeptID in (select Dept_ID from JobDeskUsers where RowStatus > -1 " +
            "and User_ID=" + UserID + ")";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return sqlDataReader["Approval"].ToString();
                }
            }

            return string.Empty;
        }

        public string GetUserI(string UserID)
        {
            string strsql = "select User_ID from RMM where RMM_DeptID in (select Dept_ID from RMM_Users where User_ID=" + UserID + ") and apv=0 and RowStatus>-1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return sqlDataReader["User_ID"].ToString();
                }
            }

            return string.Empty;
        }

        public string GetUserIGrid(string UserID)
        {
            string strsql = "select User_ID from RMM where RMM_DeptID in (select Dept_ID from RMM_Users where User_ID=" + UserID + ") and RowStatus>-1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return sqlDataReader["User_ID"].ToString();
                }
            }

            return string.Empty;
        }

        public ArrayList RetrieveOpenJOBDESKHeader(string UserInput, string Apv)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            //Users user = ((Users)Session["Users"]);

            arrJobDesk = new ArrayList();
            int UserID = users.ID;

            //if (Convert.ToInt32(user.Apv.ToString()) == 3 && Convert.ToInt32(user.UserLevel.ToString()) == 2)
            //{
            //    arrJobDesk = RetrieveForOpenJOBDESK0(UserID, UserInput, Apv);
            //    return arrJobDesk;
            //}
            //else
            //{
            if (Convert.ToInt32(Apv) == 1)
            {
                arrJobDesk = RetrieveForOpenJOBDESK2(UserID, UserInput, Apv);
                return arrJobDesk;
            }
            else
            {
                arrJobDesk = RetrieveForOpenJOBDESK(UserID, UserInput, Apv);
                return arrJobDesk;
            }
            //}
        }

        public ArrayList RetrieveForOpenJOBDESK(int UserID, string UserInput, string Apv)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());

            //string strSQL = "SELECT jd.ID, jd.JOBDESK_No, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jd.Pendidikan, jd.Pengalaman, jdd.TujuanUmumJabatan," +
            //                "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
            //                "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv, jdd.TugasPokokJabatan, jdd.HubunganKerja, jdd.TanggungJawab, jdd.Wewenang, jdd.PendidikanPengalaman, jdd.Pengetahuan, " +
            //                "jdd.Keterampilan, jdd.Fisik, jdd.NonFisik, jdd.Bawahan FROM JobDesk AS jd LEFT JOIN JobDeskDetail AS jdd ON jd.ID=jdd.JOBDESKID WHERE jd.RowStatus>-1 AND " +
            //                "jdd.RowStatus>-1 and jd.Approval in (SELECT Approval FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ") ORDER BY jd.ID DESC";

            string strSQL = "SELECT ID, DeptID, Jabatan, Atasan, TglSusun, Revisi, Status, Approval, RowStatus, " +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk WHERE RowStatus>-1 AND " +
                            "Approval in (SELECT Approval FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ") ORDER BY ID DESC";

            //string strSQL = "SELECT jd.ID, jd.JOBDESK_No, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jd.Pendidikan, jd.Pengalaman, "+
            //                "jdTUJ.TujuanUmumJabatan,case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 "+
            //                "then 'PM / Corp Manager'  when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv, jdTPJ.TugasPokokJabatan, "+
            //                "jdHK.HubunganKerja, jdTJ.TanggungJawab, jdW.Wewenang, jdPend.Pendidikan, jdPeng.Pengalaman, jdPengt.Pengetahuan, jdKet.Keterampilan, jdF.Fisik, jdNF.NonFisik, jdBW.Bawahan "+
            //                "FROM JobDesk AS jd LEFT JOIN JobDeskTUJabatan AS jdTUJ ON jd.ID=jdTUJ.JOBDESKID left join JobDeskTPJabatan as jdTPJ on jd.ID=jdTPJ.JOBDESKID left join "+
            //                "JobDeskHK as jdHK on jd.ID=jdHK.JOBDESKID left join JobDeskTJ as jdTJ on jd.ID=jdTJ.JOBDESKID left join JobDeskWewenang as jdW on jd.ID=jdW.JOBDESKID "+
            //                "left join JobDeskPendidikan as jdPend on jd.ID=jdPend.JOBDESKID left join JobDeskPengalaman as jdPeng on jd.ID=jdPeng.JOBDESKID left join JobDeskPengetahuan as jdPengt "+
            //                "on jd.ID=jdPengt.JOBDESKID left join JobDeskKeterampilan as jdKet on jd.ID=jdKet.JOBDESKID left join JobDeskFisik as jdF on jd.ID=jdF.JOBDESKID "+
            //                "left join JobDeskNonFisik as jdNF on jd.ID=jdNF.JOBDESKID left join JobDeskBawahan as jdBW on jd.ID=jdBW.JOBDESKID WHERE jd.RowStatus>-1 AND jdTUJ.RowStatus>-1 and jd.Approval in " +
            //                "(SELECT Approval FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ") ORDER BY jd.ID DESC";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrJobDesk = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateObjectHeaderJOBDESK(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            return arrJobDesk;
        }

        public ArrayList RetrieveForOpenJOBDESK2(int UserID, string UserInput, string Apv)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //string strSQL = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, "+
            //                "jdTUJ.TujuanUmumJabatan,case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 "+
            //                "then 'PM / Corp Manager'  when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv, jdTPJ.TugasPokokJabatan, "+
            //                "jdHK.HubunganKerja, jdTJ.TanggungJawab, jdW.Wewenang, jdPend.Pendidikan, jdPeng.Pengalaman, jdPengt.Pengetahuan, jdKet.Keterampilan, jdF.Fisik, jdNF.NonFisik, jdBW.Bawahan "+
            //                "FROM JobDesk AS jd LEFT JOIN JobDeskTUJabatan AS jdTUJ ON jd.ID=jdTUJ.JOBDESKID left join JobDeskTPJabatan as jdTPJ on jd.ID=jdTPJ.JOBDESKID left join "+
            //                "JobDeskHK as jdHK on jd.ID=jdHK.JOBDESKID left join JobDeskTJ as jdTJ on jd.ID=jdTJ.JOBDESKID left join JobDeskWewenang as jdW on jd.ID=jdW.JOBDESKID "+
            //                "left join JobDeskPendidikan as jdPend on jd.ID=jdPend.JOBDESKID left join JobDeskPengalaman as jdPeng on jd.ID=jdPeng.JOBDESKID left join JobDeskPengetahuan as jdPengt "+
            //                "on jd.ID=jdPengt.JOBDESKID left join JobDeskKeterampilan as jdKet on jd.ID=jdKet.JOBDESKID left join JobDeskFisik as jdF on jd.ID=jdF.JOBDESKID "+
            //                "left join JobDeskNonFisik as jdNF on jd.ID=jdNF.JOBDESKID left join JobDeskBawahan as jdBW on jd.ID=jdBW.JOBDESKID WHERE jd.RowStatus>-1 AND jdTUJ.RowStatus>-1 and jd.Approval in " +
            //                "(SELECT Approval FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ") ORDER BY jd.ID DESC";

            string strSQL = "SELECT ID, DeptID, Jabatan, Atasan, TglSusun, Revisi, Status, Approval, RowStatus, "+
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 "+
                            "then 'PM / Corp Manager'  when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' end Apv "+
                            "FROM JobDesk WHERE RowStatus>-1 AND Approval in (SELECT Approval FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1 and Approval=1) " + 
                            "ORDER BY ID DESC ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrJobDesk = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateObjectHeaderJOBDESK(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            return arrJobDesk;
        }

        //public ArrayList RetrieveForOpenJOBDESK0(int UserID, string UserInput, string Apv)
        //{
        //    string strSQL = "SELECT jd.ID, jd.JOBDESK_No, jd.DeptID, jd.Jabatan, jd.Atasan, jd.Bawahan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdd.TujuanUmumJabatan," +
        //                    "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Mgr Mgr' when Approval=3 then 'Corp ISO'  " +
        //                    "when Status=1 then 'Close' end Apv, jdd.TugasPokokJabatan, jdd.HubunganKerja, jdd.TanggungJawab, jdd.Wewenang, jdd.PendidikanPengalaman, jdd.Pengetahuan, " +
        //                    "jdd.Keterampilan, jdd.Fisik, jdd.NonFisik FROM JobDesk AS jd LEFT JOIN JobDeskDetail AS jdd ON jd.ID=jdd.JOBDESKID WHERE jd.RowStatus>-1 AND " +
        //                    "jdd.RowStatus>-1 and jd.Approval in (SELECT Approval FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ") ORDER BY jd.ID DESC";

        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        //    strError = dataAccess.Error;
        //    arrJobDesk = new ArrayList();
        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrJobDesk.Add(GenerateObjectHeaderJOBDESK(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrJobDesk.Add(new JobDesk());
        //    return arrJobDesk;
        //}

        public JobDesk RetrieveJOBDESKNum(string ID)
        {
            //string strSQL = "SELECT jd.ID, jd.JOBDESK_No, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jd.Pendidikan, jd.Pengalaman, jdd.TujuanUmumJabatan," +
            //                "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
            //                "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv, jdd.TugasPokokJabatan, jdd.HubunganKerja, jdd.TanggungJawab, jdd.Wewenang, jdd.PendidikanPengalaman, jdd.Pengetahuan, " +
            //                "jdd.Keterampilan, jdd.Fisik, jdd.NonFisik, jdd.Bawahan FROM JobDesk AS jd LEFT JOIN JobDeskDetail AS jdd ON jd.ID=jdd.JOBDESKID WHERE jd.Status>-1 AND jd.RowStatus>-1 AND " +
            //                "jdd.RowStatus>-1 and jd.JOBDESK_No= '" + NoJOBDESK + " '";

            string strSQL = "SELECT ID, DeptID, Jabatan, Atasan, TglSusun, Revisi, Status, Approval, RowStatus, " +
                           "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'Approved HRD' when Approval=2 then 'Approved Manager' when Approval=3 then 'Approved PM / Corp Manager'  " +
                           "when Approval=4 then 'Approved Corp HRD Manager' when Approval=5 then 'Approved ISO' when Status=1 then 'Close' end Apv " +
                           "FROM JobDesk WHERE Status>-1 AND RowStatus>-1 AND " +
                           "ID= '" + ID + " '";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectHeaderJOBDESK(sqlDataReader);
                }
            }

            return new JobDesk();
        }

        public ArrayList RetrieveJOBDESK(string UserID, string Apv)
        {
            //string strsql = " select A.ID as idRMM ,A.RMM_No, A.Tgl_RMM, '' DeptFrom,case when Apv=0 then 'Open' when apv is null then 'Open'" +
            //                            " when apv=1 then 'Mgr Dept' when Apv=2 then 'Plant Mgr/Corp Mgr' when Apv=3 then 'Corp ISO' when Status=1 then 'Close' end Approval , " +
            //                            "  (select Penyebab from  RMM_Penyebab where ID=C.RMM_SumberDayaID)as SumberDaya, " +
            //                            " C.Aktivitas " +
            //                            " from RMM A inner join RMM_Detail C on A.ID=C.RMM_ID where A.RowStatus > -1 and A.RMM_DeptID in (select Dept_ID from RMM_Users where RowStatus > -1 and User_ID=" + UserHead + ") " +
            //                            " and Apv=(select top 1 Approval  from RMM_Users where RowStatus > -1 and User_ID=" + UserHead + ")-1";
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdd.TujuanUmumJabatan," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv, jdd.TugasPokokJabatan, jdd.HubunganKerja, jdd.TanggungJawab, jdd.Wewenang, jdd.PendidikanPengalaman, jdd.Pengetahuan, " +
                            "jdd.Keterampilan, jdd.Fisik, jdd.NonFisik, jdd.Bawahan FROM JobDesk AS jd LEFT JOIN JobDeskDetail AS jdd ON jd.ID=jdd.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdd.RowStatus>-1 and jd.Approval in (SELECT Approval FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ") "+
                            "and jd.DeptID in (SELECT Dept_ID FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1) ORDER BY jd.ID DESC";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESK(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }

        public ArrayList RetrieveJOBDESK2(int Id, int Apv, int UserID)
        {
            //string strsql = "SELECT jd.ID, jd.JOBDESK_No, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdd.TujuanUmumJabatan," +
            //                "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
            //                "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv, jdd.TugasPokokJabatan, jdd.HubunganKerja, jdd.TanggungJawab, jdd.Wewenang, jdd.PendidikanPengalaman, jdd.Pengetahuan, " +
            //                "jdd.Keterampilan, jdd.Fisik, jdd.NonFisik, jdd.Bawahan FROM JobDesk AS jd LEFT JOIN JobDeskDetail AS jdd ON jd.ID=jdd.JOBDESKID WHERE jd.RowStatus>-1 AND " +
            //                "jdd.RowStatus>-1 and jd.Approval in (SELECT Approval FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ") " +
            //                "and jd.DeptID in (SELECT Dept_ID FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1) and jdd.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ORDER BY jd.ID DESC";

            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdtuj.TujuanUmumJabatan," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskTUJabatan AS jdtuj ON jd.ID=jdtuj.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdtuj.RowStatus>-1 and jd.Approval in (SELECT Approval FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ") " +
                            "and jd.DeptID in (SELECT Dept_ID FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1) and jdtuj.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ORDER BY jd.ID DESC";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKTUJ(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }

        public ArrayList RetrieveJOBDESK3(int Id, int Apv, int UserID)
        {
            //string strsql = "SELECT jd.ID, jd.JOBDESK_No, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdd.TujuanUmumJabatan," +
            //                "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
            //                "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv, jdd.TugasPokokJabatan, jdd.HubunganKerja, jdd.TanggungJawab, jdd.Wewenang, jdd.PendidikanPengalaman, jdd.Pengetahuan, " +
            //                "jdd.Keterampilan, jdd.Fisik, jdd.NonFisik, jdd.Bawahan FROM JobDesk AS jd LEFT JOIN JobDeskDetail AS jdd ON jd.ID=jdd.JOBDESKID WHERE jd.RowStatus>-1 AND " +
            //                "jdd.RowStatus>-1 and jd.Approval in (SELECT Approval FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ") " +
            //                "and jdd.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ORDER BY jd.ID DESC";

            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdtuj.TujuanUmumJabatan," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskTUJabatan AS jdtuj ON jd.ID=jdtuj.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdtuj.RowStatus>-1 and jd.Approval in (SELECT Approval FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ") " +
                            "and jdtuj.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ORDER BY ID DESC";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKTUJ(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }

        public ArrayList RetrieveLapJOBDESK(string Tahun, string DeptID)
        {
            string strsql = "SELECT ID, DeptID, Jabatan, Atasan, TglSusun, Revisi, Status, Approval, RowStatus, AlasanTidakIkutRevisi, " +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk WHERE RowStatus>-1 " +
                            "and DeptID= '" + DeptID + " ' and Year(CreatedTime)= '" + Tahun + " ' ORDER BY ID DESC";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESK(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }

        public ArrayList RetrieveLapJOBDESK2(string Dept)
        {
            arrData = new ArrayList();
            string where = (Dept == "0") ? "" : " where DeptID=" + Dept + " and s.RowStatus>-1  ";
            string strSQL = "select s.DeptID,d.Alias from JobDesk s left join Dept d on d.ID=s.DeptID " + where + "  Group by s.DeptID,d.Alias ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateObjectDept(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }

        public string GetUserType(string userID)
        {
            string strSQL = "select Approval from JobDeskUsers where User_ID=" + userID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            string usertype = string.Empty;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    usertype = sqlDataReader["Approval"].ToString();
                }
            }
            return usertype;
        }

        public ArrayList RetrieveDept()
        {
            string strSQL = "select distinct A.Alias, A.ID from Dept as A where A.RowStatus = 0 order by A.Alias";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateObjectDept2(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());

            return arrJobDesk;
        }

        public ArrayList LoadTahun()
        {
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select Distinct(Year(TglSusun))Tahun From JobDesk WHERE Year(TglSusun) IS NOT NULL Order By Year(TglSusun)";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateObjectThn(sdr));
                }
            }
            return arrData;
        }

        public ArrayList RetrieveJOBDESKBawahan(int Id, int Apv, int UserID)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdbw.Bawahan," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskBawahan AS jdbw ON jd.ID=jdbw.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdbw.RowStatus>-1 and jd.Approval in (SELECT Approval FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ") " +
                            "and jd.DeptID in (SELECT Dept_ID FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1) and jdbw.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ORDER BY jd.ID DESC";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKBawahan(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }

        public ArrayList RetrieveJOBDESKBawahan2(int Id, int Apv, int UserID)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdbw.Bawahan," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskBawahan AS jdbw ON jd.ID=jdbw.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdbw.RowStatus>-1 and jd.Approval in (SELECT Approval FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ") " +
                            "and jdbw.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ORDER BY ID DESC";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKBawahan(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }

        public ArrayList RetrieveJOBDESKTPJ(int Id, int Apv, int UserID)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdtpj.TugasPokokJabatan," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskTPJabatan AS jdtpj ON jd.ID=jdtpj.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdtpj.RowStatus>-1 and jd.Approval in (SELECT Approval FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ") " +
                            "and jd.DeptID in (SELECT Dept_ID FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1) and jdtpj.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ORDER BY jd.ID DESC";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKTPJ(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }

        public ArrayList RetrieveJOBDESKTPJ2(int Id, int Apv, int UserID)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdtpj.TugasPokokJabatan," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskTPJabatan AS jdtpj ON jd.ID=jdtpj.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdtpj.RowStatus>-1 and jd.Approval in (SELECT Approval FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ") " +
                            "and jdtpj.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ORDER BY ID DESC";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKTPJ(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }

        public ArrayList RetrieveJOBDESKHK(int Id, int Apv, int UserID)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdhk.HubunganKerja," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskHK AS jdhk ON jd.ID=jdhk.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdhk.RowStatus>-1 and jd.Approval in (SELECT Approval FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ") " +
                            "and jd.DeptID in (SELECT Dept_ID FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1) and jdhk.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ORDER BY jd.ID DESC";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKHK(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }

        public ArrayList RetrieveJOBDESKHK2(int Id, int Apv, int UserID)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdhk.HubunganKerja," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskHK AS jdhk ON jd.ID=jdhk.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdhk.RowStatus>-1 and jd.Approval in (SELECT Approval FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ") " +
                            "and jdhk.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ORDER BY ID DESC";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKHK(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }

        public ArrayList RetrieveJOBDESKTJ(int Id, int Apv, int UserID)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdtj.TanggungJawab," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskTJ AS jdtj ON jd.ID=jdtj.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdtj.RowStatus>-1 and jd.Approval in (SELECT Approval FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ") " +
                            "and jd.DeptID in (SELECT Dept_ID FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1) and jdtj.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ORDER BY jd.ID DESC";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKTJ(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }

        public ArrayList RetrieveJOBDESKTJ2(int Id, int Apv, int UserID)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdtj.TanggungJawab," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskTJ AS jdtj ON jd.ID=jdtj.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdtj.RowStatus>-1 and jd.Approval in (SELECT Approval FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ") " +
                            "and jdtj.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ORDER BY ID DESC";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKTJ(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }

        public ArrayList RetrieveJOBDESKWewenang(int Id, int Apv, int UserID)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdw.Wewenang," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskWewenang AS jdw ON jd.ID=jdw.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdw.RowStatus>-1 and jd.Approval in (SELECT Approval FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ") " +
                            "and jd.DeptID in (SELECT Dept_ID FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1) and jdw.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ORDER BY jd.ID DESC";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKWewenang(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }

        public ArrayList RetrieveJOBDESKWewenang2(int Id, int Apv, int UserID)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdw.Wewenang," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskWewenang AS jdw ON jd.ID=jdw.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdw.RowStatus>-1 and jd.Approval in (SELECT Approval FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ") " +
                            "and jdw.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ORDER BY ID DESC";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKWewenang(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }

        public ArrayList RetrieveJOBDESKPendidikan(int Id, int Apv, int UserID)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdpend.Pendidikan," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskPendidikan AS jdpend ON jd.ID=jdpend.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdpend.RowStatus>-1 and jd.Approval in (SELECT Approval FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ") " +
                            "and jd.DeptID in (SELECT Dept_ID FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1) and jdpend.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ORDER BY jd.ID DESC";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKPendidikan(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }

        public ArrayList RetrieveJOBDESKPendidikan2(int Id, int Apv, int UserID)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdpend.Pendidikan," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskPendidikan AS jdpend ON jd.ID=jdpend.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdpend.RowStatus>-1 and jd.Approval in (SELECT Approval FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ") " +
                            "and jdpend.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ORDER BY ID DESC";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKPendidikan(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }

        public ArrayList RetrieveJOBDESKPengalaman(int Id, int Apv, int UserID)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdpeng.Pengalaman," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskPengalaman AS jdpeng ON jd.ID=jdpeng.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdpeng.RowStatus>-1 and jd.Approval in (SELECT Approval FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ") " +
                            "and jd.DeptID in (SELECT Dept_ID FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1) and jdpeng.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ORDER BY jd.ID DESC";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKPengalaman(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }

        public ArrayList RetrieveJOBDESKPengalaman2(int Id, int Apv, int UserID)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdpeng.Pengalaman," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskPengalaman AS jdpeng ON jd.ID=jdpeng.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdpeng.RowStatus>-1 and jd.Approval in (SELECT Approval FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ") " +
                            "and jdpeng.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ORDER BY ID DESC";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKPengalaman(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }

        public ArrayList RetrieveJOBDESKPengetahuan(int Id, int Apv, int UserID)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdpengt.Pengetahuan," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskPengetahuan AS jdpengt ON jd.ID=jdpengt.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdpengt.RowStatus>-1 and jd.Approval in (SELECT Approval FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ") " +
                            "and jd.DeptID in (SELECT Dept_ID FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1) and jdpengt.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ORDER BY jd.ID DESC";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKPengetahuan(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }

        public ArrayList RetrieveJOBDESKPengetahuan2(int Id, int Apv, int UserID)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdpengt.Pengetahuan," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskPengetahuan AS jdpengt ON jd.ID=jdpengt.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdpengt.RowStatus>-1 and jd.Approval in (SELECT Approval FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ") " +
                            "and jdpengt.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ORDER BY ID DESC";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKPengetahuan(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }

        public ArrayList RetrieveJOBDESKKeterampilan(int Id, int Apv, int UserID)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdket.Keterampilan," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskKeterampilan AS jdket ON jd.ID=jdket.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdket.RowStatus>-1 and jd.Approval in (SELECT Approval FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ") " +
                            "and jd.DeptID in (SELECT Dept_ID FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1) and jdket.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ORDER BY jd.ID DESC";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKKeterampilan(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }

        public ArrayList RetrieveJOBDESKKeterampilan2(int Id, int Apv, int UserID)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdket.Keterampilan," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskKeterampilan AS jdket ON jd.ID=jdket.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdket.RowStatus>-1 and jd.Approval in (SELECT Approval FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ") " +
                            "and jdket.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ORDER BY ID DESC";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKKeterampilan(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }

        public ArrayList RetrieveJOBDESKFisik(int Id, int Apv, int UserID)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdf.Fisik," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskFisik AS jdf ON jd.ID=jdf.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdf.RowStatus>-1 and jd.Approval in (SELECT Approval FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ") " +
                            "and jd.DeptID in (SELECT Dept_ID FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1) and jdf.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ORDER BY jd.ID DESC";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKFisik(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }

        public ArrayList RetrieveJOBDESKFisik2(int Id, int Apv, int UserID)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdf.Fisik," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskFisik AS jdf ON jd.ID=jdf.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdf.RowStatus>-1 and jd.Approval in (SELECT Approval FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ") " +
                            "and jdf.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ORDER BY ID DESC";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKFisik(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }

        public ArrayList RetrieveJOBDESKNonFisik(int Id, int Apv, int UserID)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdnf.NonFisik," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskNonFisik AS jdnf ON jd.ID=jdnf.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdnf.RowStatus>-1 and jd.Approval in (SELECT Approval FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ") " +
                            "and jd.DeptID in (SELECT Dept_ID FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1) and jdnf.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ORDER BY jd.ID DESC";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKNonFisik(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }

        public ArrayList RetrieveJOBDESKNonFisik2(int Id, int Apv, int UserID)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdnf.NonFisik," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskNonFisik AS jdnf ON jd.ID=jdnf.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdnf.RowStatus>-1 and jd.Approval in (SELECT Approval FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ") " +
                            "and jdnf.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ORDER BY ID DESC";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKNonFisik(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }

        public ArrayList RetrieveJOBDESKUsia(int Id, int Apv, int UserID)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdnf.Usia," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskUsia AS jdnf ON jd.ID=jdnf.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdnf.RowStatus>-1 and jd.Approval in (SELECT Approval FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ") " +
                            "and jd.DeptID in (SELECT Dept_ID FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1) and jdnf.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ORDER BY jd.ID DESC";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKUsia(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }

        public ArrayList RetrieveJOBDESKUsia2(int Id, int Apv, int UserID)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdnf.Usia," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskUsia AS jdnf ON jd.ID=jdnf.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdnf.RowStatus>-1 and jd.Approval in (SELECT Approval FROM JobDeskUsers where User_ID='" + UserID + "' and RowStatus>-1 and Approval=" + Apv + ") " +
                            "and jdnf.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ORDER BY ID DESC";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKUsia(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }


        public ArrayList RetrieveOpenJobDeskDistribusi()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "SELECT ID, DeptID, Jabatan, Atasan, TglSusun, Revisi, Status, Approval, RowStatus, " +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk WHERE Approval=5 and RowStatus>-1 ORDER BY ID DESC";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrJobDesk = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateObjectHeaderJOBDESK(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            return arrJobDesk;
        }
        public ArrayList RetrieveJOBDESKTUJDistribusi(int Id)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdnf.TujuanUmumJabatan," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskTUJabatan AS jdnf ON jd.ID=jdnf.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdnf.RowStatus>-1 and jd.Approval >= 5 and jdnf.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKTUJ(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }
        public ArrayList RetrieveJOBDESKBawahanDistribusi(int Id)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdnf.Bawahan," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskBawahan AS jdnf ON jd.ID=jdnf.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdnf.RowStatus>-1 and jd.Approval >= 5 and jdnf.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKBawahan(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }
        public ArrayList RetrieveJOBDESKTPJDistribusi(int Id)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdnf.TugasPokokJabatan," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskTPJabatan AS jdnf ON jd.ID=jdnf.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdnf.RowStatus>-1 and jd.Approval >= 5 and jdnf.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKTPJ(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }
        public ArrayList RetrieveJOBDESKHKDistribusi(int Id)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdnf.HubunganKerja," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskHK AS jdnf ON jd.ID=jdnf.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdnf.RowStatus>-1 and jd.Approval >= 5 and jdnf.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKHK(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }
        public ArrayList RetrieveJOBDESKTJDistribusi(int Id)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdnf.TanggungJawab," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskTJ AS jdnf ON jd.ID=jdnf.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdnf.RowStatus>-1 and jd.Approval >= 5 and jdnf.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKTJ(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }
        public ArrayList RetrieveJOBDESKWewenangDistribusi(int Id)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdnf.Wewenang," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskWewenang AS jdnf ON jd.ID=jdnf.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdnf.RowStatus>-1 and jd.Approval >= 5 and jdnf.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKWewenang(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }
        public ArrayList RetrieveJOBDESKPendidikanDistribusi(int Id)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdnf.Pendidikan," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskPendidikan AS jdnf ON jd.ID=jdnf.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdnf.RowStatus>-1 and jd.Approval >= 5 and jdnf.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKPendidikan(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }
        public ArrayList RetrieveJOBDESKPengalamanDistribusi(int Id)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdnf.Pengalaman," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskPengalaman AS jdnf ON jd.ID=jdnf.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdnf.RowStatus>-1 and jd.Approval >= 5 and jdnf.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKPengalaman(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }
        public ArrayList RetrieveJOBDESKUsiaDistribusi(int Id)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdnf.Usia," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskUsia AS jdnf ON jd.ID=jdnf.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdnf.RowStatus>-1 and jd.Approval >= 5 and jdnf.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKUsia(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }
        public ArrayList RetrieveJOBDESKPengetahuanDistribusi(int Id)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdnf.Pengetahuan," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskPengetahuan AS jdnf ON jd.ID=jdnf.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdnf.RowStatus>-1 and jd.Approval >= 5 and jdnf.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKPengetahuan(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }
        public ArrayList RetrieveJOBDESKKeterampilanDistribusi(int Id)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdnf.Keterampilan," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskKeterampilan AS jdnf ON jd.ID=jdnf.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdnf.RowStatus>-1 and jd.Approval >= 5 and jdnf.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKKeterampilan(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }
        public ArrayList RetrieveJOBDESKFisikDistribusi(int Id)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdnf.Fisik," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskFisik AS jdnf ON jd.ID=jdnf.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdnf.RowStatus>-1 and jd.Approval >= 5 and jdnf.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKFisik(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }
        public ArrayList RetrieveJOBDESKNonFisikDistribusi(int Id)
        {
            string strsql = "SELECT jd.ID, jd.DeptID, jd.Jabatan, jd.Atasan, jd.TglSusun, jd.Revisi, jd.Status, jd.Approval, jd.RowStatus, jdnf.NonFisik," +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM JobDesk AS jd LEFT JOIN JobDeskNonFisik AS jdnf ON jd.ID=jdnf.JOBDESKID WHERE jd.RowStatus>-1 AND " +
                            "jdnf.RowStatus>-1 and jd.Approval >= 5 and jdnf.JOBDESKID in (select id from JobDesk where ID='" + Id + "') ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESKNonFisik(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }

        public ArrayList RetrieveJOBDESKKrwgCtrp(string Tahun)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string Plant = (users.UnitKerjaID == 7 || users.UnitKerjaID == 13) ? "[sqlctrp.grcboard.com].bpasctrp.dbo.JobDesk" : "JobDesk";
            string strsql = "SELECT ID, DeptID, Jabatan, Atasan, TglSusun, Revisi, Status, Approval, RowStatus, AlasanTidakIkutRevisi, " +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM "+ Plant +" WHERE RowStatus>-1 and status=1 " +
                            "and Year(CreatedTime)= '" + Tahun + " ' ORDER BY ID DESC";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESK(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }

        public ArrayList RetrieveJOBDESKCtrpKrwg(string Tahun)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string Plant = (users.UnitKerjaID == 7) ? "JobDesk" : "[sqlkrwg.grcboard.com].bpaskrwg.dbo.JobDesk";
            string strsql = "SELECT ID, DeptID, Jabatan, Atasan, TglSusun, Revisi, Status, Approval, RowStatus, AlasanTidakIkutRevisi, " +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM "+ Plant +" WHERE RowStatus>-1 and status=1 " +
                            "and Year(CreatedTime)= '" + Tahun + " ' ORDER BY ID DESC";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESK(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }

        public ArrayList RetrieveJOBDESKKrwgJombang(string Tahun)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string Plant = (users.UnitKerjaID == 7 || users.UnitKerjaID == 1) ? "[sqljombang.grcboard.com].bpasjombang.dbo.JobDesk" : "JobDesk";
            string strsql = "SELECT ID, DeptID, Jabatan, Atasan, TglSusun, Revisi, Status, Approval, RowStatus, AlasanTidakIkutRevisi, " +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM " + Plant + " WHERE RowStatus>-1 and status=2 " +
                            "and Year(CreatedTime)= '" + Tahun + " ' ORDER BY ID DESC";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESK(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }

        public ArrayList RetrieveJOBDESKJombangKrwg(string Tahun)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string Plant = (users.UnitKerjaID == 7) ? "JobDesk" : "[sqlkrwg.grcboard.com].bpaskrwg.dbo.JobDesk";
            string strsql = "SELECT ID, DeptID, Jabatan, Atasan, TglSusun, Revisi, Status, Approval, RowStatus, AlasanTidakIkutRevisi, " +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM " + Plant + " WHERE RowStatus>-1 and status=2 " +
                            "and Year(CreatedTime)= '" + Tahun + " ' ORDER BY ID DESC";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESK(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }

        public ArrayList RetrieveJOBDESKCtrpJombang(string Tahun)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string Plant = (users.UnitKerjaID == 7 || users.UnitKerjaID == 1) ? "[sqljombang.grcboard.com].bpasjombang.dbo.JobDesk" : "JobDesk";
            string strsql = "SELECT ID, DeptID, Jabatan, Atasan, TglSusun, Revisi, Status, Approval, RowStatus, AlasanTidakIkutRevisi, " +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM " + Plant + " WHERE RowStatus>-1 and status=3 " +
                            "and Year(CreatedTime)= '" + Tahun + " ' ORDER BY ID DESC";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESK(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }

        public ArrayList RetrieveJOBDESKJombangCtrp(string Tahun)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string Plant = (users.UnitKerjaID == 1) ? "JobDesk" : "[sqlctrp.grcboard.com].bpasctrp.dbo.JobDesk";
            string strsql = "SELECT ID, DeptID, Jabatan, Atasan, TglSusun, Revisi, Status, Approval, RowStatus, AlasanTidakIkutRevisi, " +
                            "case when Approval=0 then 'Open' when Approval is null then 'Open' when Approval=1 then 'HRD' when Approval=2 then 'Manager' when Approval=3 then 'PM / Corp Manager'  " +
                            "when Approval=4 then 'Corp HRD Manager' when Approval=5 then 'ISO' when Status=1 then 'Close' end Apv " +
                            "FROM " + Plant + " WHERE RowStatus>-1 and status=3 " +
                            "and Year(CreatedTime)= '" + Tahun + " ' ORDER BY ID DESC";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            arrJobDesk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrJobDesk.Add(GenerateGridOJOBDESK(sqlDataReader));
                }
            }
            else
                arrJobDesk.Add(new JobDesk());
            //}
            //catch { }
            return arrJobDesk;
        }

        private JobDesk GenerateObjectDept(SqlDataReader sqlDataReader)
        {
            objJobDesk = new JobDesk();
            objJobDesk.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objJobDesk.AliasDept = sqlDataReader["Alias"].ToString();
            return objJobDesk;
        }

        private JobDesk GenerateObjectDept2(SqlDataReader sqlDataReader)
        {
            objJobDesk = new JobDesk();
            objJobDesk.DeptID = Convert.ToInt32(sqlDataReader["ID"]);
            objJobDesk.AliasDept = sqlDataReader["Alias"].ToString();
            return objJobDesk;
        }

        private JobDesk GenerateObjectThn(SqlDataReader sqlDataReader)
        {
            objJobDesk = new JobDesk();
            objJobDesk.Tahun = int.Parse(sqlDataReader["Tahun"].ToString());
            return objJobDesk;
        }

        public JobDesk GenerateObjectHeaderJOBDESK(SqlDataReader sqlDataReader)
        {
            objJobDesk = new JobDesk();
            string test = sqlDataReader["TglSusun"].ToString();
            objJobDesk.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objJobDesk.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objJobDesk.BagianName = sqlDataReader["Jabatan"].ToString();
            ////objTPP.PIC = sqlDataReader["PIC"].ToString();
            objJobDesk.TglSusun = Convert.ToDateTime(sqlDataReader["TglSusun"]);
            ////objTPP.DeptFrom = sqlDataReader["DeptFrom"].ToString();
            ////objTPP.Apv = Convert.ToInt32(sqlDataReader["Apv"]);
            objJobDesk.Approval2 = sqlDataReader["Apv"].ToString();
            objJobDesk.Atasan = sqlDataReader["Atasan"].ToString();
            //objJobDesk.Bawahan = sqlDataReader["Bawahan"].ToString();
            objJobDesk.Revisi = Convert.ToInt32(sqlDataReader["Revisi"]);
            objJobDesk.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objJobDesk.Approval = Convert.ToInt32(sqlDataReader["Approval"]);
            //objJobDesk.TUJabatan = sqlDataReader["TujuanUmumJabatan"].ToString();
            //objJobDesk.TPJabatan = sqlDataReader["TugasPokokJabatan"].ToString();
            //objJobDesk.HubunganKerja = sqlDataReader["HubunganKerja"].ToString();
            //objJobDesk.TanggungJawab = sqlDataReader["TanggungJawab"].ToString();
            //objJobDesk.Wewenang = sqlDataReader["Wewenang"].ToString();
            //objJobDesk.Pendidikan = sqlDataReader["Pendidikan"].ToString();
            //objJobDesk.Pengalaman = sqlDataReader["Pengalaman"].ToString();
            //objJobDesk.Pengetahuan = sqlDataReader["Pengetahuan"].ToString();
            return objJobDesk;
        }

        public JobDesk GenerateGridOJOBDESK(SqlDataReader sqlDataReader)
        {
            objJobDesk = new JobDesk();
            objJobDesk.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objJobDesk.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objJobDesk.BagianName = sqlDataReader["Jabatan"].ToString();
            objJobDesk.TglSusun = Convert.ToDateTime(sqlDataReader["TglSusun"]);
            objJobDesk.Approval2 = sqlDataReader["Apv"].ToString();
            objJobDesk.Atasan = sqlDataReader["Atasan"].ToString();
            objJobDesk.Revisi = Convert.ToInt32(sqlDataReader["Revisi"]);
            objJobDesk.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objJobDesk.Approval = Convert.ToInt32(sqlDataReader["Approval"]);
            objJobDesk.AlasanTidakIkutRevisi = sqlDataReader["AlasanTidakIkutRevisi"].ToString();

            return objJobDesk;
        }
        public JobDesk GenerateGridOJOBDESKBawahan(SqlDataReader sqlDataReader)
        {
            objJobDesk = new JobDesk();
            objJobDesk.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objJobDesk.BagianName = sqlDataReader["Jabatan"].ToString();
            objJobDesk.TglSusun = Convert.ToDateTime(sqlDataReader["TglSusun"]);
            objJobDesk.Approval2 = sqlDataReader["Apv"].ToString();
            objJobDesk.Atasan = sqlDataReader["Atasan"].ToString();
            objJobDesk.Bawahan = sqlDataReader["Bawahan"].ToString();
            objJobDesk.Revisi = Convert.ToInt32(sqlDataReader["Revisi"]);
            objJobDesk.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objJobDesk.Approval = Convert.ToInt32(sqlDataReader["Approval"]);

            return objJobDesk;
        }
        public JobDesk GenerateGridOJOBDESKTUJ(SqlDataReader sqlDataReader)
        {
            objJobDesk = new JobDesk();
            objJobDesk.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objJobDesk.BagianName = sqlDataReader["Jabatan"].ToString();
            objJobDesk.TglSusun = Convert.ToDateTime(sqlDataReader["TglSusun"]);
            objJobDesk.Approval2 = sqlDataReader["Apv"].ToString();
            objJobDesk.Atasan = sqlDataReader["Atasan"].ToString();
            objJobDesk.Revisi = Convert.ToInt32(sqlDataReader["Revisi"]);
            objJobDesk.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objJobDesk.Approval = Convert.ToInt32(sqlDataReader["Approval"]);
            objJobDesk.TUJabatan = sqlDataReader["TujuanUmumJabatan"].ToString();

            return objJobDesk;
        }
        public JobDesk GenerateGridOJOBDESKTPJ(SqlDataReader sqlDataReader)
        {
            objJobDesk = new JobDesk();
            objJobDesk.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objJobDesk.BagianName = sqlDataReader["Jabatan"].ToString();
            objJobDesk.TglSusun = Convert.ToDateTime(sqlDataReader["TglSusun"]);
            objJobDesk.Approval2 = sqlDataReader["Apv"].ToString();
            objJobDesk.Atasan = sqlDataReader["Atasan"].ToString();
            objJobDesk.Revisi = Convert.ToInt32(sqlDataReader["Revisi"]);
            objJobDesk.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objJobDesk.Approval = Convert.ToInt32(sqlDataReader["Approval"]);
            objJobDesk.TPJabatan = sqlDataReader["TugasPokokJabatan"].ToString();

            return objJobDesk;
        }
        public JobDesk GenerateGridOJOBDESKHK(SqlDataReader sqlDataReader)
        {
            objJobDesk = new JobDesk();
            objJobDesk.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objJobDesk.BagianName = sqlDataReader["Jabatan"].ToString();
            objJobDesk.TglSusun = Convert.ToDateTime(sqlDataReader["TglSusun"]);
            objJobDesk.Approval2 = sqlDataReader["Apv"].ToString();
            objJobDesk.Atasan = sqlDataReader["Atasan"].ToString();
            objJobDesk.Revisi = Convert.ToInt32(sqlDataReader["Revisi"]);
            objJobDesk.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objJobDesk.Approval = Convert.ToInt32(sqlDataReader["Approval"]);
            objJobDesk.HubunganKerja = sqlDataReader["HubunganKerja"].ToString();

            return objJobDesk;
        }
        public JobDesk GenerateGridOJOBDESKTJ(SqlDataReader sqlDataReader)
        {
            objJobDesk = new JobDesk();
            objJobDesk.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objJobDesk.BagianName = sqlDataReader["Jabatan"].ToString();
            objJobDesk.TglSusun = Convert.ToDateTime(sqlDataReader["TglSusun"]);
            objJobDesk.Approval2 = sqlDataReader["Apv"].ToString();
            objJobDesk.Atasan = sqlDataReader["Atasan"].ToString();
            objJobDesk.Revisi = Convert.ToInt32(sqlDataReader["Revisi"]);
            objJobDesk.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objJobDesk.Approval = Convert.ToInt32(sqlDataReader["Approval"]);
            objJobDesk.TanggungJawab = sqlDataReader["TanggungJawab"].ToString();

            return objJobDesk;
        }
        public JobDesk GenerateGridOJOBDESKWewenang(SqlDataReader sqlDataReader)
        {
            objJobDesk = new JobDesk();
            objJobDesk.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objJobDesk.BagianName = sqlDataReader["Jabatan"].ToString();
            objJobDesk.TglSusun = Convert.ToDateTime(sqlDataReader["TglSusun"]);
            objJobDesk.Approval2 = sqlDataReader["Apv"].ToString();
            objJobDesk.Atasan = sqlDataReader["Atasan"].ToString();
            objJobDesk.Revisi = Convert.ToInt32(sqlDataReader["Revisi"]);
            objJobDesk.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objJobDesk.Approval = Convert.ToInt32(sqlDataReader["Approval"]);
            objJobDesk.Wewenang = sqlDataReader["Wewenang"].ToString();

            return objJobDesk;
        }
        public JobDesk GenerateGridOJOBDESKPendidikan(SqlDataReader sqlDataReader)
        {
            objJobDesk = new JobDesk();
            objJobDesk.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objJobDesk.BagianName = sqlDataReader["Jabatan"].ToString();
            objJobDesk.TglSusun = Convert.ToDateTime(sqlDataReader["TglSusun"]);
            objJobDesk.Approval2 = sqlDataReader["Apv"].ToString();
            objJobDesk.Atasan = sqlDataReader["Atasan"].ToString();
            objJobDesk.Revisi = Convert.ToInt32(sqlDataReader["Revisi"]);
            objJobDesk.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objJobDesk.Approval = Convert.ToInt32(sqlDataReader["Approval"]);
            objJobDesk.Pendidikan = sqlDataReader["Pendidikan"].ToString();

            return objJobDesk;
        }
        public JobDesk GenerateGridOJOBDESKPengalaman(SqlDataReader sqlDataReader)
        {
            objJobDesk = new JobDesk();
            objJobDesk.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objJobDesk.BagianName = sqlDataReader["Jabatan"].ToString();
            objJobDesk.TglSusun = Convert.ToDateTime(sqlDataReader["TglSusun"]);
            objJobDesk.Approval2 = sqlDataReader["Apv"].ToString();
            objJobDesk.Atasan = sqlDataReader["Atasan"].ToString();
            objJobDesk.Revisi = Convert.ToInt32(sqlDataReader["Revisi"]);
            objJobDesk.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objJobDesk.Approval = Convert.ToInt32(sqlDataReader["Approval"]);
            objJobDesk.Pengalaman = sqlDataReader["Pengalaman"].ToString();

            return objJobDesk;
        }
        public JobDesk GenerateGridOJOBDESKPengetahuan(SqlDataReader sqlDataReader)
        {
            objJobDesk = new JobDesk();
            objJobDesk.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objJobDesk.BagianName = sqlDataReader["Jabatan"].ToString();
            objJobDesk.TglSusun = Convert.ToDateTime(sqlDataReader["TglSusun"]);
            objJobDesk.Approval2 = sqlDataReader["Apv"].ToString();
            objJobDesk.Atasan = sqlDataReader["Atasan"].ToString();
            objJobDesk.Revisi = Convert.ToInt32(sqlDataReader["Revisi"]);
            objJobDesk.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objJobDesk.Approval = Convert.ToInt32(sqlDataReader["Approval"]);
            objJobDesk.Pengetahuan = sqlDataReader["Pengetahuan"].ToString();

            return objJobDesk;
        }
        public JobDesk GenerateGridOJOBDESKKeterampilan(SqlDataReader sqlDataReader)
        {
            objJobDesk = new JobDesk();
            objJobDesk.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objJobDesk.BagianName = sqlDataReader["Jabatan"].ToString();
            objJobDesk.TglSusun = Convert.ToDateTime(sqlDataReader["TglSusun"]);
            objJobDesk.Approval2 = sqlDataReader["Apv"].ToString();
            objJobDesk.Atasan = sqlDataReader["Atasan"].ToString();
            objJobDesk.Revisi = Convert.ToInt32(sqlDataReader["Revisi"]);
            objJobDesk.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objJobDesk.Approval = Convert.ToInt32(sqlDataReader["Approval"]);
            objJobDesk.Keterampilan = sqlDataReader["Keterampilan"].ToString();

            return objJobDesk;
        }
        public JobDesk GenerateGridOJOBDESKFisik(SqlDataReader sqlDataReader)
        {
            objJobDesk = new JobDesk();
            objJobDesk.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objJobDesk.BagianName = sqlDataReader["Jabatan"].ToString();
            objJobDesk.TglSusun = Convert.ToDateTime(sqlDataReader["TglSusun"]);
            objJobDesk.Approval2 = sqlDataReader["Apv"].ToString();
            objJobDesk.Atasan = sqlDataReader["Atasan"].ToString();
            objJobDesk.Revisi = Convert.ToInt32(sqlDataReader["Revisi"]);
            objJobDesk.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objJobDesk.Approval = Convert.ToInt32(sqlDataReader["Approval"]);
            objJobDesk.Fisik = sqlDataReader["Fisik"].ToString();

            return objJobDesk;
        }
        public JobDesk GenerateGridOJOBDESKNonFisik(SqlDataReader sqlDataReader)
        {
            objJobDesk = new JobDesk();
            objJobDesk.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objJobDesk.BagianName = sqlDataReader["Jabatan"].ToString();
            objJobDesk.TglSusun = Convert.ToDateTime(sqlDataReader["TglSusun"]);
            objJobDesk.Approval2 = sqlDataReader["Apv"].ToString();
            objJobDesk.Atasan = sqlDataReader["Atasan"].ToString();
            objJobDesk.Revisi = Convert.ToInt32(sqlDataReader["Revisi"]);
            objJobDesk.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objJobDesk.Approval = Convert.ToInt32(sqlDataReader["Approval"]);
            objJobDesk.NonFisik = sqlDataReader["NonFisik"].ToString();

            return objJobDesk;
        }
        public JobDesk GenerateGridOJOBDESKUsia(SqlDataReader sqlDataReader)
        {
            objJobDesk = new JobDesk();
            objJobDesk.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objJobDesk.BagianName = sqlDataReader["Jabatan"].ToString();
            objJobDesk.TglSusun = Convert.ToDateTime(sqlDataReader["TglSusun"]);
            objJobDesk.Approval2 = sqlDataReader["Apv"].ToString();
            objJobDesk.Atasan = sqlDataReader["Atasan"].ToString();
            objJobDesk.Revisi = Convert.ToInt32(sqlDataReader["Revisi"]);
            objJobDesk.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objJobDesk.Approval = Convert.ToInt32(sqlDataReader["Approval"]);
            objJobDesk.Usia = sqlDataReader["Usia"].ToString();

            return objJobDesk;
        }
    }
}
