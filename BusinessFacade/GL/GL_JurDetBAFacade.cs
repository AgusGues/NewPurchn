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
    public class GL_JurDetBAFacade : AbstractTransactionFacade
    {
        private GL_JurDetBA objGL_JurDetBA = new GL_JurDetBA();
        private ArrayList arrGL_JurDetBA;
        private List<SqlParameter> sqlListParam;

        public GL_JurDetBAFacade(object objDomain)
            : base(objDomain)
        {
            objGL_JurDetBA = (GL_JurDetBA)objDomain;
        }

        public GL_JurDetBAFacade()
        {

        }
        
        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@BgNo", objGL_JurDetBA.BgNo));
                sqlListParam.Add(new SqlParameter("@BankName", objGL_JurDetBA.BankName));
                sqlListParam.Add(new SqlParameter("@DueDate", objGL_JurDetBA.DueDate));
                sqlListParam.Add(new SqlParameter("@JurDetNum", objGL_JurDetBA.JurDetNum));
                sqlListParam.Add(new SqlParameter("@Remark", objGL_JurDetBA.Remark));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objGL_JurDetBA.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "GLInsert_JurBA");
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
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@BgNo", objGL_JurDetBA.BgNo));
                sqlListParam.Add(new SqlParameter("@BankName", objGL_JurDetBA.BankName));
                sqlListParam.Add(new SqlParameter("@DueDate", objGL_JurDetBA.DueDate));
                sqlListParam.Add(new SqlParameter("@JurDetNum", objGL_JurDetBA.JurDetNum));
                sqlListParam.Add(new SqlParameter("@Remark", objGL_JurDetBA.Remark));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objGL_JurDetBA.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateGL_JurDetBA");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Delete(TransactionManager transManager)
        {

            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@JurDetNum", objGL_JurDetBA.JurDetNum));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objGL_JurDetBA.LastModifiedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteGL_JurDetBA");
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
            //string strSQL = "select GL_JurDet.*, GL_ChartofAccount.ChartName from GL_Jurdet inner join GL_CHart on GL_Jurdet.ChartNo = GL_ChartofAccount.ChartNo ";
            //DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            //strError = dataAccess.Error;
            arrGL_JurDetBA = new ArrayList();
            //if (sqlDataReader.HasRows)
            //{
            //    while (sqlDataReader.Read())
            //    {
            //        arrGL_JurDetBA.Add(GenerateObject(sqlDataReader));
            //    }
            //}
            //else
            //    arrGL_JurDetBA.Add(new GL_JurDetBA());
            return arrGL_JurDetBA;
        }
    }
}
