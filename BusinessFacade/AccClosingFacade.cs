using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;

namespace BusinessFacade
{
    public class AccClosingFacade : AbstractFacade
    {
        private AccClosing objAcc = new AccClosing();
        private ArrayList arrAcc = new ArrayList();
        private List<SqlParameter> sqlListParam;
        public DataAccess dataaccess = new DataAccess(Global.ConnectionString());
        public AccClosingFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override int Update(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override int Delete(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }

        public AccClosing RetrieveByStatus(int Bulan,int Tahun)
        {
            
            string strSQL = "Select top 1 * from AccClosingPeriode where Bulan="+Bulan+" and Tahun="+Tahun+" and Modul='Purchn' order by ID Desc";
            //DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrAcc = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject3(sqlDataReader);
                }
            }

            return new AccClosing();
        }
        public AccClosing CheckBiayaAktif()
        {
            string strSQL = "select RowStatus as Status,Year(ModifiedTime) as Tahun, Month(ModifiedTime) as Bulan from BiayaNew";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrAcc = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new AccClosing();
        }

        public string CheckClosing(int bulan,int tahun)
        {
            string clsPeriod = string.Empty;
            AccClosing objCls = this.RetrieveByStatus(bulan, tahun);
            string Bulan = (objCls.Bulan == 0) ? string.Empty : objCls.Bulan.ToString();
            string Tahun = (objCls.Tahun == 0) ? string.Empty : objCls.Tahun.ToString();
            clsPeriod=(Bulan==string.Empty)?string.Empty:Bulan+Tahun;
            return clsPeriod;
        }
        public AccClosing BiayaNewActive()
        {
            string strSQL = "Select * From BiayaNew where RowStatus=1 ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrAcc = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new AccClosing();
        }

        public int UpdateInfoStatus(int UserID, string ModulName)
        {
            try
            {
            sqlListParam = new List<SqlParameter>();
            sqlListParam.Add(new SqlParameter("@UserID", UserID));
            sqlListParam.Add(new SqlParameter("@ModulName", ModulName));
            int intResult =dataAccess.ProcessData(sqlListParam, "spEventLogInfoInsert");

                strError =dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public string nBulan(int Bulan)
        {
            string[] arrBln = new string[] { "", "January", "February", "Maret", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            return arrBln[Bulan];
        }

        public AccClosing GenerateObject2(SqlDataReader sqlDataReader)
        {
            objAcc = new AccClosing();
            objAcc.Status = int.Parse(sqlDataReader["RowStatus"].ToString());
            objAcc.CreatedTime = DateTime.Parse(sqlDataReader["ModifiedTime"].ToString());
            return objAcc;
        }

        public AccClosing GenerateObject(SqlDataReader sqlDataReader)
        {
            objAcc = new AccClosing();
            objAcc.Tahun = int.Parse(sqlDataReader["Tahun"].ToString());
            objAcc.Bulan = int.Parse(sqlDataReader["Bulan"].ToString());
            objAcc.Status = int.Parse(sqlDataReader["Status"].ToString());
            //objAcc.ModulName = sqlDataReader["Modul"].ToString();
            return objAcc;
        }
        public AccClosing GenerateObject3(SqlDataReader sqlDataReader)
        {
            objAcc = new AccClosing();
            objAcc.Tahun = int.Parse(sqlDataReader["Tahun"].ToString());
            objAcc.Bulan = int.Parse(sqlDataReader["Bulan"].ToString());
            objAcc.Status = int.Parse(sqlDataReader["Status"].ToString());
            objAcc.ModulName = sqlDataReader["Modul"].ToString();
            return objAcc;
        }
    }
}
