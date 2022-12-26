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
    public class MataUangKursFacade : AbstractFacade
    {
        private MataUangKurs objMataUangKurs = new MataUangKurs();
        private ArrayList arrMataUangKurs;
        private List<SqlParameter> sqlListParam;


        public MataUangKursFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objMataUangKurs = (MataUangKurs)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@MUID", objMataUangKurs.MuId ));
                sqlListParam.Add(new SqlParameter("@KURS", objMataUangKurs.Kurs ));
                sqlListParam.Add(new SqlParameter("@drtgl", objMataUangKurs.DrTgl  ));
                sqlListParam.Add(new SqlParameter("@sdtgl", objMataUangKurs.SdTgl ));
                sqlListParam.Add(new SqlParameter("@typekurs", objMataUangKurs.TypeKurs ));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertMataUangKurs");

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
                objMataUangKurs = (MataUangKurs)objDomain;
                sqlListParam = new List<SqlParameter>();
                //sqlListParam.Add(new SqlParameter("@Lambang", objMataUangKurs.Lambang));
                //sqlListParam.Add(new SqlParameter("@Nama", objMataUangKurs.Nama));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateMataUang");

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
                objMataUangKurs = (MataUangKurs)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objMataUangKurs.Id));
                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteMataUangKurs");
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
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT TOP (50) ID, MUID, Kurs, drTgl, sdTgl FROM MataUangKurs where rowstatus>=0");
            strError = dataAccess.Error;
            arrMataUangKurs = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrMataUangKurs.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrMataUangKurs.Add(new MataUangKurs());
            return arrMataUangKurs;
        }

        public MataUangKurs RetrieveByID(int ID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT TOP (50) ID, MUID, Kurs, drTgl, sdTgl FROM MataUangKurs where rowstatus>=0 and D=" + ID);
            strError = dataAccess.Error;
            arrMataUangKurs = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new MataUangKurs();
        }

        public MataUangKurs RetrieveByLast(int ID,int typekurs)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT TOP (1) ID, MUID, Kurs, drTgl, sdTgl FROM MataUangKurs where rowstatus>=0 and MUID=" + ID +
                " and typekurs=" + typekurs + " order by id desc");
            strError = dataAccess.Error;
            arrMataUangKurs = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new MataUangKurs();
        }

        public ArrayList  RetrieveByMUID(int MUID,int typekurs)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "SELECT TOP (50) ID, MUID, Kurs, drTgl, sdTgl FROM MataUangKurs where rowstatus>=0 and MUID=" + MUID + 
                " and typekurs=" + typekurs + " order by id desc";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrMataUangKurs = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrMataUangKurs.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrMataUangKurs.Add(new MataUangKurs());
            return arrMataUangKurs;
        }

        public MataUangKurs RetrieveByMataUangKurs(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("");
            strError = dataAccess.Error;
            arrMataUangKurs = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new MataUangKurs();
        }
        
        public MataUangKurs GenerateObject(SqlDataReader sqlDataReader)
        {
            objMataUangKurs = new MataUangKurs();
            objMataUangKurs.Id  = Convert.ToInt32(sqlDataReader["ID"]);
            objMataUangKurs.MuId = Convert.ToInt32(sqlDataReader["muid"]);
            objMataUangKurs.Kurs = Convert.ToDecimal(sqlDataReader["Kurs"]);
            objMataUangKurs.DrTgl = Convert.ToDateTime(sqlDataReader["DrTgl"]);
            objMataUangKurs.SdTgl = Convert.ToDateTime(sqlDataReader["SdTgl"]);
            return objMataUangKurs;
        }

        public int GetKurs(string TglKurs)
        {
            int kurs = 0;
            string strSQL = "select top 1 Kurs from MataUangKurs where CONVERT(char,drTgl,112)>='20140930' " +
                          "and CONVERT(char,sdTgl,112) <='20140930' order by ID desc ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Kurs"]);
                }
            }
            return kurs;
        }
    }
}
