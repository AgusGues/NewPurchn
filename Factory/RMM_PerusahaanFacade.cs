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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Factory
{
    public class RMM_PerusahaanFacade : BusinessFacade.AbstractTransactionFacadeF
    {
        private RMM_Perusahaan objSarMutPerusahaan = new RMM_Perusahaan();
        private ArrayList arrSarMutPerusahaan;
        private List<SqlParameter> sqlListParam;

        public override int Insert(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Update(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Insert1(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Update1(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Update2(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Delete(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override ArrayList Retrieve()
        {
            string strSQL = "select * from RMM_Perusahaan as A where A.RowStatus >-1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSarMutPerusahaan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSarMutPerusahaan.Add(GenerateObject(sqlDataReader));

                }
            }
            else
                arrSarMutPerusahaan.Add(new RMM_Perusahaan());

            return arrSarMutPerusahaan;

        }

        public ArrayList RetrieveByUserID(int userID)
        {
            string strSQL = "select A.*  from RMM_Perusahaan A inner join RMM_Users B on A.DeptID=B.Dept_ID  where B.User_ID=" + userID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSarMutPerusahaan = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSarMutPerusahaan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSarMutPerusahaan.Add(new RMM_Perusahaan());
            return arrSarMutPerusahaan;
        }

       

        //public ArrayList Retrieve2()
        //{
        //    string strSQL = "select * from RMM_Perusahaan as A where A.RowStatus >-1";
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        //    strError = dataAccess.Error;
        //    arrSarMutPerusahaan = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrSarMutPerusahaan.Add(GenerateObject1(sqlDataReader));

        //        }
        //    }
        //    else
        //        arrSarMutPerusahaan.Add(new RMM_Perusahaan());

        //    return arrSarMutPerusahaan;

        //}


        private RMM_Perusahaan GenerateObject(SqlDataReader sqlDataReader)
        {
            objSarMutPerusahaan = new RMM_Perusahaan();
            objSarMutPerusahaan.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objSarMutPerusahaan.Dimensi = sqlDataReader["Dimensi"].ToString();
            objSarMutPerusahaan.SarMutPerusahaan = sqlDataReader["SarMutPerusahaan"].ToString();
            objSarMutPerusahaan.DepoID = Convert.ToInt32(sqlDataReader["DepoID"]);
            objSarMutPerusahaan.Urutan = Convert.ToInt32(sqlDataReader["Urutan"]);
            return objSarMutPerusahaan;
        }

        //private RMM_Perusahaan GenerateObject1(SqlDataReader sqlDataReader)
        //{
        //    objSarMutPerusahaan = new RMM_Perusahaan();
        //    //objSarMutPerusahaan.ID = Convert.ToInt32(sqlDataReader["ID"]);
        //    objSarMutPerusahaan.Dimensi = sqlDataReader["Dimensi"].ToString();
        //    //objSarMutPerusahaan.SarMutPerusahaan = sqlDataReader["SarMutPerusahaan"].ToString();
        //    //objSarMutPerusahaan.DepoID = Convert.ToInt32(sqlDataReader["DepoID"]);
        //    //objSarMutPerusahaan.Urutan = Convert.ToInt32(sqlDataReader["Urutan"]);
        //    return objSarMutPerusahaan;
        //}
    }
}
