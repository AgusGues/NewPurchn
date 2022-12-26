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
    public class GL_CustBalFacade : BusinessFacade.AbstractTransactionFacade
    {
        private GL_CustBal objGL_CustBal = new GL_CustBal();
        private ArrayList arrGL_CustBal;
        private List<SqlParameter> sqlListParam;

        public GL_CustBalFacade(object objDomain)
            : base(objDomain)
        {
            objGL_CustBal = (GL_CustBal)objDomain;
        }

        public GL_CustBalFacade()
        {

        }
        
        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@period", objGL_CustBal.Period));
                sqlListParam.Add(new SqlParameter("@CustCode", objGL_CustBal.CustCode));
                sqlListParam.Add(new SqlParameter("@BegBal", objGL_CustBal.BegBal));
                sqlListParam.Add(new SqlParameter("@DebitTrans", objGL_CustBal.DebitTrans));
                sqlListParam.Add(new SqlParameter("@CreditTrans", objGL_CustBal.CreditTrans));
                sqlListParam.Add(new SqlParameter("@CCYBegBal", objGL_CustBal.CCYBegBal));
                sqlListParam.Add(new SqlParameter("@CCYDebitTrans", objGL_CustBal.CCYDebitTrans));
                sqlListParam.Add(new SqlParameter("@CCYCreditTrans", objGL_CustBal.CCYCreditTrans));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objGL_CustBal.CreatedBy));
                sqlListParam.Add(new SqlParameter("@CompanyCode", objGL_CustBal.CompanyCode));

                int intResult = transManager.DoTransaction(sqlListParam, "GLInsert_CustBal");
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
                sqlListParam.Add(new SqlParameter("@period", objGL_CustBal.Period));
                sqlListParam.Add(new SqlParameter("@CustCode", objGL_CustBal.CustCode));
                sqlListParam.Add(new SqlParameter("@BegBal", objGL_CustBal.BegBal));
                sqlListParam.Add(new SqlParameter("@DebitTrans", objGL_CustBal.DebitTrans));
                sqlListParam.Add(new SqlParameter("@CreditTrans", objGL_CustBal.CreditTrans));
                sqlListParam.Add(new SqlParameter("@CCYBegBal", objGL_CustBal.CCYBegBal));
                sqlListParam.Add(new SqlParameter("@CreditTrans", objGL_CustBal.CreditTrans));
                sqlListParam.Add(new SqlParameter("@CCYDebitTrans", objGL_CustBal.CCYDebitTrans));
                sqlListParam.Add(new SqlParameter("@CCYCreditTrans", objGL_CustBal.CCYCreditTrans));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objGL_CustBal.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateGL_CustBal");
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
                sqlListParam.Add(new SqlParameter("@JurDetNum", objGL_CustBal.Period ));
                sqlListParam.Add(new SqlParameter("@JurDetNum", objGL_CustBal.CustCode));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objGL_CustBal.LastModifiedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteGL_CustBal");
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
            arrGL_CustBal = new ArrayList();
            //if (sqlDataReader.HasRows)
            //{
            //    while (sqlDataReader.Read())
            //    {
            //        arrGL_CustBal.Add(GenerateObject(sqlDataReader));
            //    }
            //}
            //else
            //    arrGL_CustBal.Add(new GL_CustBal());
            return arrGL_CustBal;
        }
        public ArrayList RetrieveByPeriod(string period, string companyCode)
        {
            string strSQL = "select * from gl_custbal where period='" + period + "' and CompanyCode='"+companyCode+"'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGL_CustBal = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_CustBal.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_CustBal.Add(new GL_CustBal());
            return arrGL_CustBal;
        }
        public GL_CustBal GenerateObject(SqlDataReader sqlDataReader)
        {
            try
            {
                objGL_CustBal = new GL_CustBal();
                objGL_CustBal.ID = Convert.ToInt32(sqlDataReader["ID"]);
                objGL_CustBal.Period = sqlDataReader["Period"].ToString();
                objGL_CustBal.CustCode = sqlDataReader["CustCode"].ToString();
                objGL_CustBal.BegBal = Convert.ToInt32(sqlDataReader["BegBal"]);
                objGL_CustBal.DebitTrans = Convert.ToInt32(sqlDataReader["DebitTrans"]);
                objGL_CustBal.CreditTrans = Convert.ToInt32(sqlDataReader["CreditTrans"]);
                objGL_CustBal.CCYBegBal = Convert.ToInt32(sqlDataReader["CCYBegBal"]);
                objGL_CustBal.CCYDebitTrans = Convert.ToInt32(sqlDataReader["CCYDebitTrans"]);
                objGL_CustBal.CCYCreditTrans = Convert.ToInt32(sqlDataReader["CCYCreditTrans"]);
                objGL_CustBal.CompanyCode = sqlDataReader["CompanyCode"].ToString();

            }
            catch { }
            return objGL_CustBal;
        }
    }

    }

