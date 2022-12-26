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
namespace DefectFacade
{
    public class DefectDetailFacade : AbstractTransactionFacade
    {
        private DefectDetail objDefectDetail = new DefectDetail();
        private ArrayList arrDefectDetail;
        private List<SqlParameter> sqlListParam;

        public DefectDetailFacade(object objDomain)
            : base(objDomain)
        {
            objDefectDetail = (DefectDetail)objDomain;
        }

        public DefectDetailFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@DefectID", objDefectDetail.DefectID));
                sqlListParam.Add(new SqlParameter("@MasterID", objDefectDetail.MasterID));               
                sqlListParam.Add(new SqlParameter("@Qty", objDefectDetail.Qty));
                sqlListParam.Add(new SqlParameter("@RowStatus", objDefectDetail.RowStatus));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertDef_DefectDetail");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public override int Update(TransactionManager transManager)
        {
            int intResult = Delete(transManager);
            if (strError == string.Empty)
                intResult = Insert(transManager);

            return intResult;
        }

        public override int Delete(TransactionManager transManager)
        {
            try
            {
                objDefectDetail = (DefectDetail)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objDefectDetail.ID));

                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteDef_DefectDetail");

                strError = transManager.Error;

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from Def_DefectDetail where RowStatus>-1");
            strError = dataAccess.Error;
            arrDefectDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDefectDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrDefectDetail.Add(new DefectDetail());

            return arrDefectDetail;
        }
        public ArrayList RetrieveByDefectID(int DefectID)
        {
            string strSQL = "SELECT A.ID, B.DefName, A.Qty FROM Def_DefectDetail AS A LEFT OUTER JOIN Def_MasterDefect AS B "+
                "ON A.MasterID = B.ID where A.RowStatus>-1 and A.DefectID=" + DefectID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrDefectDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDefectDetail.Add(GenerateObjectView(sqlDataReader));
                }
            }
            else
                arrDefectDetail.Add(new DefectDetail());

            return arrDefectDetail;
        }
        public DefectDetail GenerateObjectView(SqlDataReader sqlDataReader)
        {
            objDefectDetail = new DefectDetail();
            objDefectDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objDefectDetail.DefectName = sqlDataReader["DefName"].ToString();
            objDefectDetail.Qty = Convert.ToDecimal(sqlDataReader["Qty"]);
            return objDefectDetail;
        }
        public DefectDetail GenerateObject(SqlDataReader sqlDataReader)
        {
            objDefectDetail = new DefectDetail();
            objDefectDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objDefectDetail.DefectID = Convert.ToInt32(sqlDataReader["DefectID"]);
            objDefectDetail.MasterID = Convert.ToInt32(sqlDataReader["MasterID"]);
            objDefectDetail.Qty = Convert.ToDecimal(sqlDataReader["Qty"]);
            objDefectDetail.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            return objDefectDetail;
        }


    }
}
