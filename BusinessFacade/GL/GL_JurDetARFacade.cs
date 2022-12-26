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
    public class GL_JurDetARFacade : AbstractTransactionFacade
    {
       private GL_JurDetAR objGL_JurDetAR = new GL_JurDetAR();
        private ArrayList arrGL_JurDetAR;
        private List<SqlParameter> sqlListParam;

        public GL_JurDetARFacade(object objDomain)
            : base(objDomain)
        {
            objGL_JurDetAR = (GL_JurDetAR)objDomain;
        }

        public GL_JurDetARFacade()
        {

        }
        
        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@InvNo", objGL_JurDetAR.InvNo ));
                sqlListParam.Add(new SqlParameter("@InvDate", objGL_JurDetAR.InvDate));
                sqlListParam.Add(new SqlParameter("@DueDate", objGL_JurDetAR.DueDate));
                sqlListParam.Add(new SqlParameter("@JurDetNum", objGL_JurDetAR.JurDetNum));
                sqlListParam.Add(new SqlParameter("@OurRef", objGL_JurDetAR.OurRef));
                sqlListParam.Add(new SqlParameter("@Remark", objGL_JurDetAR.Remark));
                sqlListParam.Add(new SqlParameter("@CustCode", objGL_JurDetAR.CustCode));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objGL_JurDetAR.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "GLInsert_JurAR");
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
                sqlListParam.Add(new SqlParameter("@InvNo", objGL_JurDetAR.InvNo));
                sqlListParam.Add(new SqlParameter("@InvDate", objGL_JurDetAR.InvDate));
                sqlListParam.Add(new SqlParameter("@DueDate", objGL_JurDetAR.DueDate));
                sqlListParam.Add(new SqlParameter("@JurDetNum", objGL_JurDetAR.JurDetNum));
                sqlListParam.Add(new SqlParameter("@OurRef", objGL_JurDetAR.OurRef));
                sqlListParam.Add(new SqlParameter("@Remark", objGL_JurDetAR.Remark));
                sqlListParam.Add(new SqlParameter("@CustCode", objGL_JurDetAR.CustCode));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objGL_JurDetAR.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateGL_JurDetAR");
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
                sqlListParam.Add(new SqlParameter("@JurDetNum", objGL_JurDetAR.JurDetNum));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objGL_JurDetAR.LastModifiedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteGL_JurDetAR");
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
            arrGL_JurDetAR = new ArrayList();
            //if (sqlDataReader.HasRows)
            //{
            //    while (sqlDataReader.Read())
            //    {
            //        arrGL_JurDetAR.Add(GenerateObject(sqlDataReader));
            //    }
            //}
            //else
            //    arrGL_JurDetAR.Add(new GL_JurDetAR());
            return arrGL_JurDetAR;
        }
    }
}
