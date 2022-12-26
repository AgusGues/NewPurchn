using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using BusinessFacade;
using Domain;
using DataAccessLayer;
namespace Cogs
{
    public class ClosingFacade : AbstractFacade3
    {
        private Closper objCls = new Closper();
        private ArrayList arrCls = new ArrayList();
        private List<SqlParameter> sqlListParam;

        public override int Insert(object objDomain)
        {
            try
            {
                int result = 0;
                objCls = (Closper)objDomain;
                sqlListParam=new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Tahun",objCls.Tahun));
                sqlListParam.Add(new SqlParameter("@Bulan",objCls.Bulan));
                sqlListParam.Add(new SqlParameter("@Modul", objCls.ModulName));
                sqlListParam.Add(new SqlParameter("@Status", objCls.RowStatus));
                sqlListParam.Add(new SqlParameter("@Createdby", objCls.CreatedBy));
                result = dataAccess.ProcessData(sqlListParam, "spAccClosingInsert");
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
                objCls = (Closper)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objCls.ID));
                sqlListParam.Add(new SqlParameter("@Status", objCls.RowStatus));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objCls.LastModifiedBy));
                result = dataAccess.ProcessData(sqlListParam, "spAccClosingUpdate");
                strError = dataAccess.Error;
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
            throw new NotImplementedException();
        }

        public override System.Collections.ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }

        public ArrayList RetrieveYear(int Tahun, int Bulan)
        {
            string bln=(Bulan==0)?" order by Bulan":" and Bulan="+Bulan;
            string strSQL = "select * from AccClosingPeriode where Tahun="+Tahun+bln;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrCls = new ArrayList();
            int n = 0;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    n = n + 1;
                    arrCls.Add(new Closper
                    {
                        ID = Convert.ToInt32(sqlDataReader["ID"]),
                        Tahun = Tahun,
                        Bulan = Bulan,
                        nBulan = Global.nBulan(Bulan),
                        RowStatus = Convert.ToInt32(sqlDataReader["Status"].ToString()),

                    });
                }
            }
            else
            {
                arrCls.Add(new Closper());
            }
            return arrCls;
        }

        public int ClosingStatus(int Tahun, int Bulan, string ModulName)
        {
            string strSQL = "select Status from AccClosingPeriode where Tahun=" + Tahun + " and Bulan="+Bulan+" and Modul='"+ModulName+"'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrCls = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Status"].ToString());

                }
            }
            return 0;
        }
        public int LastMonthClosing(int Tahun, string ModulName)
        {
            string strSQL = "select isnull(Max(Bulan),0) as Bulan from AccClosingPeriode where Tahun=" + Tahun + " and Modul='" + ModulName + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrCls = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Bulan"].ToString());

                }
            }
            return 0;
        }
        public int GetIDClosing(int Tahun, int Bulan, string ModulName)
        {
            string strSQL = "select ID from AccClosingPeriode where Tahun=" + Tahun + " and Bulan=" + Bulan + " and Modul='" + ModulName + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrCls = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["ID"].ToString());

                }
            }
            return 0;
        }

        public int GetMonthStatus(int Tahun, int Bulan,string Modul)
        {
            string strSQL = "select Status from AccClosingPeriode where Tahun=" + Tahun + " and Bulan=" + Bulan + " and Modul='" + Modul + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrCls = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Status"].ToString());
                }
            }
            return 0;
        }
        public int GetClosingStatus(string ModulName)
        {
            //menggunakan modul "SystemClosing" = 1 aktif , 0 non aktif
            string strSQL = "select Status from Purchn_Tools where ModulName='" + ModulName + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrCls = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Status"].ToString());
                }
            }
            return 0;
        }
        public string GetCloseingBy(int ID)
        {
            string strSQL = "Select  Case status when 0 then modified_by+' on: '+CONVERT(varchar,lastModified_time,105) "+
	                        "when 1 then created_by+' on: '+ CONVERT(varchar,created_time,105)end CreatedBy "+
                            "From AccClosingPeriode Where ID=" + ID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrCls = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return sqlDataReader["CreatedBy"].ToString();
                }
            }
            return string.Empty;
        }

        /** locking system **/
        public int LockIn(string Data)
        {
            string strSQL = "INSERT INTO AccClosingLock (FromDate,ToDate,FromTime,ToTime,Durasi,Keterangan,LockStatus,CreatedBy,CreatedTime)" +
                            "VALUES ("+Data+")";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return (strError == string.Empty) ? 0 : -1;
        }
    }
}
