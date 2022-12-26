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
    public class GL_ClosePeriodFacade : AbstractTransactionFacade
    {
        private GL_ClosePeriod objClosePeriod = new GL_ClosePeriod();
        private ArrayList arrClosePeriod;
        private List<SqlParameter> sqlListParam;

        public GL_ClosePeriodFacade(object objDomain)
            : base(objDomain)
        {
            objClosePeriod = (GL_ClosePeriod)objDomain;
        }

        public GL_ClosePeriodFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@period1", objClosePeriod.Period1));
                sqlListParam.Add(new SqlParameter("@period2", objClosePeriod.Period2));
                sqlListParam.Add(new SqlParameter("@CompanyCode", objClosePeriod.CompanyCode));
                sqlListParam.Add(new SqlParameter("@DeptCode", objClosePeriod.DeptCode));
                sqlListParam.Add(new SqlParameter("@CostCode", objClosePeriod.CostCode));
                sqlListParam.Add(new SqlParameter("@mRLmonth", objClosePeriod.mRLmonth));
                sqlListParam.Add(new SqlParameter("@mRLyear", objClosePeriod.mRLyear));
                sqlListParam.Add(new SqlParameter("@mRLretain", objClosePeriod.mRLretain));

                int intResult = transManager.DoTransaction(sqlListParam, "GLClosingPeriod");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int Insert_Period2(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                //sqlListParam.Add(new SqlParameter("@period", objClosePeriod.Period));
                //sqlListParam.Add(new SqlParameter("@ChartNo", objClosePeriod.ChartNo));
                //sqlListParam.Add(new SqlParameter("@DeptCode", objClosePeriod.DeptCode));
                //sqlListParam.Add(new SqlParameter("@CostCode", objClosePeriod.CostCode));
                //sqlListParam.Add(new SqlParameter("@BegBal", objClosePeriod.BegBal));
                //sqlListParam.Add(new SqlParameter("@DebitTrans", objClosePeriod.DebitTrans));
                //sqlListParam.Add(new SqlParameter("@CreditTrans", objClosePeriod.CreditTrans));
                //sqlListParam.Add(new SqlParameter("@CCYBegBal", objClosePeriod.CCYBegBal));
                //sqlListParam.Add(new SqlParameter("@CCYDebitTrans", objClosePeriod.CCYDebitTrans));
                //sqlListParam.Add(new SqlParameter("@CCYCreditTrans", objClosePeriod.CCYCreditTrans));
                //sqlListParam.Add(new SqlParameter("@CreatedBy", objClosePeriod.CreatedBy));
                //sqlListParam.Add(new SqlParameter("@CompanyCode", objClosePeriod.CompanyCode));

                int intResult = transManager.DoTransaction(sqlListParam, "");
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
                //sqlListParam.Add(new SqlParameter("@period", objClosePeriod.Period));
                //sqlListParam.Add(new SqlParameter("@ChartNo", objClosePeriod.ChartNo));
                //sqlListParam.Add(new SqlParameter("@DeptCode", objClosePeriod.DeptCode));
                //sqlListParam.Add(new SqlParameter("@CostCode", objClosePeriod.CostCode));
                //sqlListParam.Add(new SqlParameter("@BegBal", objClosePeriod.BegBal));
                //sqlListParam.Add(new SqlParameter("@DebitTrans", objClosePeriod.DebitTrans));
                //sqlListParam.Add(new SqlParameter("@CreditTrans", objClosePeriod.CreditTrans));
                //sqlListParam.Add(new SqlParameter("@CCYBegBal", objClosePeriod.CCYBegBal));
                //sqlListParam.Add(new SqlParameter("@CCYDebitTrans", objClosePeriod.CCYDebitTrans));
                //sqlListParam.Add(new SqlParameter("@CCYCreditTrans", objClosePeriod.CCYCreditTrans));
                //sqlListParam.Add(new SqlParameter("@CreatedBy", objClosePeriod.CreatedBy));
                //sqlListParam.Add(new SqlParameter("@CompanyCode", objClosePeriod.CompanyCode));

                //belum ada SP-nya
                int intResult = transManager.DoTransaction(sqlListParam, "");
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
                //sqlListParam.Add(new SqlParameter("@JurDetNum", objClosePeriod.Period));
                //sqlListParam.Add(new SqlParameter("@JurDetNum", objClosePeriod.SupCode));
                //sqlListParam.Add(new SqlParameter("@LastModifiedBy", objClosePeriod.LastModifiedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "");
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
            arrClosePeriod = new ArrayList();
            //if (sqlDataReader.HasRows)
            //{
            //    while (sqlDataReader.Read())
            //    {
            //        arrGL_ChartBal.Add(GenerateObject(sqlDataReader));
            //    }
            //}
            //else
            //    arrGL_ChartBal.Add(new GL_ClosePeriod());
            return arrClosePeriod;
        }

        public GL_ClosePeriod GenerateObject(SqlDataReader sqlDataReader)
        {
            try
            {
                objClosePeriod = new GL_ClosePeriod();
                objClosePeriod.Period1 = sqlDataReader["Period"].ToString();
                objClosePeriod.Period2 = sqlDataReader["Period2"].ToString();
                objClosePeriod.DeptCode = sqlDataReader["DeptCode"].ToString();
                objClosePeriod.CostCode = sqlDataReader["CostCode"].ToString();
                objClosePeriod.CompanyCode = sqlDataReader["CompanyCode"].ToString();
                objClosePeriod.mRLmonth = sqlDataReader["mRLmonth"].ToString();
                objClosePeriod.mRLyear = sqlDataReader["mRLyear"].ToString();
                objClosePeriod.mRLretain = sqlDataReader["mRLretain"].ToString();
            }
            catch { }
            return objClosePeriod;
        }



    }



}
