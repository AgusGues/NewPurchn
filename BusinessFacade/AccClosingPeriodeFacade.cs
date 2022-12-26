using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;
using Dapper;

namespace BusinessFacade
{
    public class AccClosingPeriodeFacade : AbstractFacade
    {
        private AccClosingPeriode objAcc = new AccClosingPeriode();
        private ArrayList arrAcc = new ArrayList();
        private List<SqlParameter> sqlListParam;
        public DataAccess dataaccess = new DataAccess(Global.ConnectionString());

        public AccClosingPeriodeFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objAcc = (AccClosingPeriode)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Tahun", objAcc.Tahun));
                sqlListParam.Add(new SqlParameter("@Bulan", objAcc.Bulan));
                sqlListParam.Add(new SqlParameter("@Status", objAcc.Status));
                sqlListParam.Add(new SqlParameter("@ModulName", objAcc.ModulName));


                int intResult = dataaccess.ProcessData(sqlListParam, "spInsertAccClosingPeriode");

                strError = dataaccess.Error;

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
                objAcc = (AccClosingPeriode)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objAcc.ID));
                sqlListParam.Add(new SqlParameter("@Status", objAcc.Status));

                int intResult = dataaccess.ProcessData(sqlListParam, "spUpdateAccClosingPeriode");

                strError = dataaccess.Error;

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
            throw new NotImplementedException();
        }

        public override ArrayList Retrieve()
        {
            DataAccess dataaccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataaccess.RetrieveDataByString("select * from AccClosingPeriode order by Tahun desc,Bulan desc ");
            strError = dataaccess.Error;
            arrAcc = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrAcc.Add(GenerateObject3(sqlDataReader));
                }
            }
            else
                arrAcc.Add(new AccClosingPeriode());

            return arrAcc;
        }

        public AccClosingPeriode RetrieveByStatus(int Bulan, int Tahun, string strModul)
        {
            DataAccess dataaccess = new DataAccess(Global.ConnectionString());

            string strSQL = "Select top 1 * from AccClosingPeriode where Bulan=" + Bulan + " and Tahun=" + Tahun + " and Modul='" + strModul + "' order by ID Desc";
            SqlDataReader sqlDataReader = dataaccess.RetrieveDataByString(strSQL);
            strError = dataaccess.Error;
            arrAcc = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject3(sqlDataReader);
                }
            }
            return new AccClosingPeriode();
        }

        public AccClosingPeriode GenerateObject3(SqlDataReader sqlDataReader)
        {
            objAcc = new AccClosingPeriode();

            objAcc.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objAcc.Tahun = int.Parse(sqlDataReader["Tahun"].ToString());
            objAcc.Bulan = int.Parse(sqlDataReader["Bulan"].ToString());
            objAcc.Status = int.Parse(sqlDataReader["Status"].ToString());
            objAcc.ModulName = sqlDataReader["Modul"].ToString();

            if (string.IsNullOrEmpty(sqlDataReader["DariTgl"].ToString()))
                objAcc.DariTgl = DateTime.MinValue;
            else
                objAcc.DariTgl = Convert.ToDateTime(sqlDataReader["DariTgl"]);

            if (string.IsNullOrEmpty(sqlDataReader["SampaiTgl"].ToString()))
                objAcc.SampaiTgl = DateTime.MinValue;
            else
                objAcc.SampaiTgl = Convert.ToDateTime(sqlDataReader["SampaiTgl"]);

            return objAcc;
        }


        public static int GetMonthStatus(int tahun, int bulan, string modul)
        {
            int status = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select Status from AccClosingPeriode where Tahun=" + tahun + " and Bulan=" + bulan + " and Modul='" + modul + "'";
                    status = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {
                    
                }
            }
            return status;
        }

        public static int GetClosingStatus(string modulname)
        {
            int status = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select Status from Purchn_Tools where ModulName='" + modulname + "'";
                    status = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return status;
        }
    }
}
