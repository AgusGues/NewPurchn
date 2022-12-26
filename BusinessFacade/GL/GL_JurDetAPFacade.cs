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
    public class GL_JurDetAPFacade : AbstractTransactionFacade
    {
        private GL_JurDetAP objGL_JurDetAP = new GL_JurDetAP();
        private ArrayList arrGL_JurDetAP;
        private List<SqlParameter> sqlListParam;

        public GL_JurDetAPFacade(object objDomain)
            : base(objDomain)
        {
            objGL_JurDetAP = (GL_JurDetAP)objDomain;
        }

        public GL_JurDetAPFacade()
        {

        }
        
        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@InvNo", objGL_JurDetAP.InvNo ));
                sqlListParam.Add(new SqlParameter("@InvDate", objGL_JurDetAP.InvDate));
                sqlListParam.Add(new SqlParameter("@DueDate", objGL_JurDetAP.DueDate));
                sqlListParam.Add(new SqlParameter("@JurDetNum", objGL_JurDetAP.JurDetNum));
                sqlListParam.Add(new SqlParameter("@OurRef", objGL_JurDetAP.OurRef));
                sqlListParam.Add(new SqlParameter("@Remark", objGL_JurDetAP.Remark));
                sqlListParam.Add(new SqlParameter("@SupCode", objGL_JurDetAP.SupCode));
                sqlListParam.Add(new SqlParameter("@PPH", objGL_JurDetAP.PPH));
                sqlListParam.Add(new SqlParameter("@Freight", objGL_JurDetAP.Freight));
                sqlListParam.Add(new SqlParameter("@PPnBM", objGL_JurDetAP.PPnBM));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objGL_JurDetAP.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "GLInsert_JurAP");
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
                sqlListParam.Add(new SqlParameter("@InvNo", objGL_JurDetAP.InvNo));
                sqlListParam.Add(new SqlParameter("@InvDate", objGL_JurDetAP.InvDate));
                sqlListParam.Add(new SqlParameter("@DueDate", objGL_JurDetAP.DueDate));
                sqlListParam.Add(new SqlParameter("@JurDetNum", objGL_JurDetAP.JurDetNum));
                sqlListParam.Add(new SqlParameter("@OurRef", objGL_JurDetAP.OurRef));
                sqlListParam.Add(new SqlParameter("@Remark", objGL_JurDetAP.Remark));
                sqlListParam.Add(new SqlParameter("@SupCode", objGL_JurDetAP.SupCode));
                sqlListParam.Add(new SqlParameter("@PPH", objGL_JurDetAP.PPH));
                sqlListParam.Add(new SqlParameter("@Freight", objGL_JurDetAP.Freight));
                sqlListParam.Add(new SqlParameter("@PPnBM", objGL_JurDetAP.PPnBM));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objGL_JurDetAP.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateGL_JurDetAP");
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
                sqlListParam.Add(new SqlParameter("@JurDetNum", objGL_JurDetAP.JurDetNum));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objGL_JurDetAP.LastModifiedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteGL_JurDetAP");
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
            arrGL_JurDetAP = new ArrayList();
            //if (sqlDataReader.HasRows)
            //{
            //    while (sqlDataReader.Read())
            //    {
            //        arrGL_JurDetAP.Add(GenerateObject(sqlDataReader));
            //    }
            //}
            //else
            //    arrGL_JurDetAP.Add(new GL_JurDetAP());
            return arrGL_JurDetAP;
        }
    }
}
