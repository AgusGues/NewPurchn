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
    public class BM_PaletFacade : AbstractFacade
    {
        private BM_Palet objBM_Palet = new BM_Palet();
        private ArrayList arrBM_Palet;
        private List<SqlParameter> sqlListParam;


        public BM_PaletFacade()
            : base()
        {

        }
        public override int Insert(object objDomain)
        {
            try
            {
                objBM_Palet = (BM_Palet)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Palet", objBM_Palet.NoPalet ));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertBM_Palet");
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
                objBM_Palet = (BM_Palet)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objBM_Palet.ID));
                sqlListParam.Add(new SqlParameter("@NoPalet", objBM_Palet.NoPalet ));
                sqlListParam.Add(new SqlParameter("@Status", objBM_Palet.Status ));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateBM_Palet");

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
                objBM_Palet = (BM_Palet)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objBM_Palet.ID));
                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteBM_Palet");
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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from bm_palet where rowstatus>-1 order by nopalet");
            strError = dataAccess.Error;
            arrBM_Palet = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBM_Palet.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrBM_Palet.Add(new BM_Palet());
            return arrBM_Palet;
        }

        public ArrayList RetrieveByNo(string strPalet)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from bm_palet where  rowstatus>-1 and NoPalet = '" + strPalet + "'");
            strError = dataAccess.Error;
            arrBM_Palet = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBM_Palet.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrBM_Palet.Add(new BM_Palet());
            return arrBM_Palet;
        }
        public BM_Palet RetrieveByNo1(string strPalet)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from bm_palet where  rowstatus>-1 and NoPalet = '" + strPalet + "'");
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new BM_Palet();
        }
        public int check(string strPalet)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from bm_palet where  rowstatus>-1 and NoPalet = '" + strPalet + "'");
            int ada = 0;
            if (sqlDataReader.HasRows)
            {
                    ada=1;
            }
            
            return ada;
        }

        public BM_Palet GenerateObject(SqlDataReader sqlDataReader)
        {
            objBM_Palet = new BM_Palet();
            objBM_Palet.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objBM_Palet.NoPalet =  sqlDataReader["NoPalet"].ToString();
            return objBM_Palet;

        }
    }
}
