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
    public class RMM_LocFacade : BusinessFacade.AbstractTransactionFacadeF
    {
        private RMM_Loc objRMMloc = new RMM_Loc();
        private ArrayList arrRMMLoc;
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
            string strSQL = "select * from RMM_Loc as A where A.RowStatus >-1 order by A.ID";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrRMMLoc = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrRMMLoc.Add(GenerateObject(sqlDataReader));

                }
            }
            else
                arrRMMLoc.Add(new RMM_Loc());

            return arrRMMLoc;

        }

        public ArrayList RetrieveBysarDeptId(int intSarDeptId)
        {
            //string strSQL = "select Loc from RMM_Loc  A where SarDeptID in (select ID from SarMut_Departemen where ID= " + intSarDeptId + " )";
            string strSQL = "select A.ID,A.SarDeptID,A.Loc from RMM_Loc as A , RMM_Departemen as B where B.ID=A.SarDeptID and B.ID=" + intSarDeptId + " and A.RowStatus>-1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrRMMLoc = new ArrayList();
           
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrRMMLoc.Add(GenerateObject(sqlDataReader));

                }
            }
            else
                arrRMMLoc.Add(new RMM_Loc());

            return arrRMMLoc;
        }


        private RMM_Loc GenerateObject(SqlDataReader sqlDataReader)
        {
            objRMMloc = new RMM_Loc();
            objRMMloc.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objRMMloc.SarDeptID = Convert.ToInt32(sqlDataReader["SarDeptID"]);
            objRMMloc.Loc = sqlDataReader["Loc"].ToString();
            return objRMMloc;
        }
    }
}
