using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using BusinessFacade;
using Domain;
using DataAccessLayer;
using System.Text.RegularExpressions;

namespace Cogs
{
    public class LockingFacade:AbstractFacade3
    {
        private LockSys objLock = new LockSys();
        private ArrayList arrLock = new ArrayList();
        private List<SqlParameter> sqlListParam;

        public override int Insert(object objDomain)
        {
            try
            {
                int result = 0;
                objLock = (LockSys)objDomain;
                sqlListParam=new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@DariTgl",objLock.DariTgl));
                sqlListParam.Add(new SqlParameter("@SampaiTgl", objLock.SampaiTgl));
                sqlListParam.Add(new SqlParameter("@DariJam", objLock.DariJam));
                sqlListParam.Add(new SqlParameter("@SmpJam", objLock.SampaiJam));
                sqlListParam.Add(new SqlParameter("@Durasi", objLock.Durasi));
                sqlListParam.Add(new SqlParameter("@Keterangan", objLock.Keterangan));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objLock.CreatedBy));
                sqlListParam.Add(new SqlParameter("@UserLock", objLock.UserLock));
                result = dataAccess.ProcessData(sqlListParam, "spAccClosingLockInsert");
                strError = dataAccess.Error;
                return result;
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
                int result = 0;
                objLock = (LockSys)objDomain;
                sqlListParam=new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objLock.ID));
                sqlListParam.Add(new SqlParameter("@DariTgl", objLock.DariTgl));
                sqlListParam.Add(new SqlParameter("@SampaiTgl", objLock.SampaiTgl));
                sqlListParam.Add(new SqlParameter("@DariJam", objLock.DariJam));
                sqlListParam.Add(new SqlParameter("@SmpJam", objLock.SampaiJam));
                sqlListParam.Add(new SqlParameter("@Durasi", objLock.Durasi));
                sqlListParam.Add(new SqlParameter("@Keterangan", objLock.Keterangan));
                sqlListParam.Add(new SqlParameter("@Status", objLock.RowStatus));
                sqlListParam.Add(new SqlParameter("@LastModifiedby", objLock.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@UserLock", objLock.UserLock));
                result=dataAccess.ProcessData(sqlListParam,"spAccClosingLockUpdate");
                strError=dataAccess.Error;
                return result;
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
                int result = 0;
                objLock = (LockSys)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objLock.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedby", objLock.LastModifiedBy));
                result = dataAccess.ProcessData(sqlListParam, "spAccClosingLockDelete");
                strError = dataAccess.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public override ArrayList Retrieve()
        {
            string strSQL = "Select * from AccClosingLock where LockStatus=1 order by FromDate,ID";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error; string DeptName = string.Empty;
            arrLock = new ArrayList();
            int n = 0; string dpt=string.Empty;
            string[] usrlock=new string[]{};
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    n = n + 1;
                    arrLock.Add(new LockSys
                    {
                        ID = Convert.ToInt32(sqlDataReader["ID"].ToString()),
                        DariTgl = DateTime.Parse(sqlDataReader["FromDate"].ToString()),
                        SampaiTgl = DateTime.Parse(sqlDataReader["ToDate"].ToString()),
                        DariJam = sqlDataReader["FromTime"].ToString(),
                        SampaiJam = sqlDataReader["ToTime"].ToString(),
                        Keterangan = sqlDataReader["Keterangan"].ToString(),
                        Durasi = sqlDataReader["Durasi"].ToString(),
                        StatusE = (Convert.ToInt32(sqlDataReader["LockStatus"]) == 1) ? "Lock" : "Unlock",
                        UserLock = sqlDataReader["UserLock"].ToString()
                    });
                }
            }
            else
            {
                arrLock.Add(new LockSys());
            }
            return arrLock;
        }
        public LockSys RetrieveByID(int ID)
        {
            string strSQL = "Select * From AccClosingLock where ID=" + ID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrLock = new ArrayList();
            if(sqlDataReader.HasRows)
            {
                while(sqlDataReader.Read())
                {
                    return objLock = new LockSys
                    {
                        ID = Convert.ToInt32(sqlDataReader["ID"].ToString()),
                        DariTgl = DateTime.Parse(sqlDataReader["FromDate"].ToString()),
                        SampaiTgl = DateTime.Parse(sqlDataReader["ToDate"].ToString()),
                        DariJam = sqlDataReader["FromTime"].ToString(),
                        SampaiJam = sqlDataReader["ToTime"].ToString(),
                        Keterangan = sqlDataReader["Keterangan"].ToString(),
                        Durasi = sqlDataReader["Durasi"].ToString(),
                        StatusE = (Convert.ToInt32(sqlDataReader["LockStatus"]) == 1) ? "Lock" : "Unlock",
                        UserLock = sqlDataReader["UserLock"].ToString()
                    };
                }

            }
            return new LockSys();
        }
        public string GetDeptName(int ID)
        {
            string strSQL = "Select DeptName from Dept where ID=" + ID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrLock = new ArrayList();
            int n = 0;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return sqlDataReader["DeptName"].ToString();
                }

            }
             return string.Empty;

        }
    }
}
