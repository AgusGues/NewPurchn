using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;
using Dapper;

namespace BusinessFacade
{
    public class MTC_TargetBudgetFacade : AbstractFacade
    {
        private MTC_TargetBudget objTarget = new MTC_TargetBudget();
        private ArrayList arrTarget;
        private List<SqlParameter> sqlListParam;
        private DataAccess dataAccess = new DataAccess(Global.ConnectionString());

        public MTC_TargetBudgetFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objTarget = (MTC_TargetBudget)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Tahun", objTarget.Tahun));
                sqlListParam.Add(new SqlParameter("@Smt", objTarget.Smt));
                sqlListParam.Add(new SqlParameter("@Jumlah", objTarget.Jumlah));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objTarget.CreatedBy));
                int intResult = dataAccess.ProcessData(sqlListParam, "spMTCBudgetInsert");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception e)
            {
                strError = e.Message;
                return -1;
            }
        }

        public override int Update(object objDomain)
        {
            try
            {
                objTarget = (MTC_TargetBudget)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objTarget.ID));
                sqlListParam.Add(new SqlParameter("@RowStatus", objTarget.RowStatus));
                sqlListParam.Add(new SqlParameter("@Jumlah", objTarget.Jumlah));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objTarget.LastModifiedBy));
                int intResult = dataAccess.ProcessData(sqlListParam, "spMTCBudgetUpdate");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception e)
            {
                strError = e.Message;
                return -1;
            }
        }

        public override int Delete(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override System.Collections.ArrayList Retrieve()
        {
            string strSQL = "Select * from MTC_TargetBudget order by Tahun,ID Desc";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrTarget = new ArrayList();
            int n = 0;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    n = n + 1;
                    arrTarget.Add(new MTC_TargetBudget
                    {
                        Nom = n,
                        ID = Convert.ToInt32(sqlDataReader["ID"].ToString()),
                        Tahun = Convert.ToInt32(sqlDataReader["Tahun"].ToString()),
                        Smt = sqlDataReader["Smt"].ToString(),
                        Jumlah = Convert.ToDecimal(sqlDataReader["Jumlah"].ToString()),
                        RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"].ToString())
                    });
                }
            }
            else
                arrTarget.Add(new MTC_TargetBudget());
            return arrTarget;
        }

        public static List<object> Retrivenew()
        {
            List<object> all = new List<object>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string strsql = "Select * from MTC_TargetBudget order by Tahun,ID Desc";
                    all = connection.Query<object>(strsql).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    all = null;
                }
            }
            return all;
        }

        public static List<object> Retrivebyid(string id)
        {
            List<object> all = new List<object>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string strsql = "Select * from MTC_TargetBudget where id=" + id;
                    all = connection.Query<object>(strsql).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    all = null;
                }
            }
            return all;
        }
    }
}
