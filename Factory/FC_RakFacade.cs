using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;
using BusinessFacade;
namespace Factory
{
    public class FC_RakFacade : AbstractFacade
    {
        private FC_Rak objFC_Rak = new FC_Rak();
        private ArrayList arrFC_Rak;
        private List<SqlParameter> sqlListParam;

        public FC_RakFacade()
            : base()
        {

        }
        public override int Insert(object objDomain)
        {
            try
            {
                objFC_Rak = (FC_Rak)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Rak", objFC_Rak.Rak));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertFC_Rak");
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
                objFC_Rak = (FC_Rak)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objFC_Rak.ID));
                sqlListParam.Add(new SqlParameter("@Rak", objFC_Rak.Rak));
                sqlListParam.Add(new SqlParameter("@Status", objFC_Rak.Status));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateFC_Rak");

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
                objFC_Rak = (FC_Rak)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objFC_Rak.ID));
                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteFC_Rak");
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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from FC_Rak where rowstatus>-1 order by Rak");
            strError = dataAccess.Error;
            arrFC_Rak = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrFC_Rak.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrFC_Rak.Add(new FC_Rak());
            return arrFC_Rak;
        }

        public ArrayList RetrieveByNo(string strRak)
        {
            string strSQL = "select * from FC_Rak where  rowstatus>-1 and Rak = '" + strRak + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrFC_Rak = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrFC_Rak.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrFC_Rak.Add(new FC_Rak());
            return arrFC_Rak;
        }
        public int check(string strRak)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from FC_Rak where  rowstatus>-1 and Rak = '" + strRak + "'");
            int ada = 0;
            if (sqlDataReader.HasRows)
            {
                ada = 1;
            }

            return ada;
        }

        public FC_Rak GenerateObject(SqlDataReader sqlDataReader)
        {
            objFC_Rak = new FC_Rak();
            objFC_Rak.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objFC_Rak.Rak = sqlDataReader["Rak"].ToString();
            return objFC_Rak;

        }
    }
}
