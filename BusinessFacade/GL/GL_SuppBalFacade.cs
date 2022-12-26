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
    public class GL_SuppBalFacade : AbstractTransactionFacade
    {
        private GL_SuppBal objGL_SuppBal = new GL_SuppBal();
        private ArrayList arrGL_SuppBal;
        private List<SqlParameter> sqlListParam;

        public GL_SuppBalFacade(object objDomain)
            : base(objDomain)
        {
            objGL_SuppBal = (GL_SuppBal)objDomain;
        }

        public GL_SuppBalFacade()
        {

        }
        
       
        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@period", objGL_SuppBal.Period));
                sqlListParam.Add(new SqlParameter("@SupCode", objGL_SuppBal.SupCode));
                sqlListParam.Add(new SqlParameter("@BegBal", objGL_SuppBal.BegBal));
                sqlListParam.Add(new SqlParameter("@DebitTrans", objGL_SuppBal.DebitTrans));
                sqlListParam.Add(new SqlParameter("@CreditTrans", objGL_SuppBal.CreditTrans));
                sqlListParam.Add(new SqlParameter("@CCYBegBal", objGL_SuppBal.CCYBegBal));
                sqlListParam.Add(new SqlParameter("@CCYDebitTrans", objGL_SuppBal.CCYDebitTrans));
                sqlListParam.Add(new SqlParameter("@CCYCreditTrans", objGL_SuppBal.CCYCreditTrans));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objGL_SuppBal.CreatedBy));
                sqlListParam.Add(new SqlParameter("@CompanyCode", objGL_SuppBal.CompanyCode));

                int intResult = transManager.DoTransaction(sqlListParam, "GLInsert_SuppBal");
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
                sqlListParam.Add(new SqlParameter("@period", objGL_SuppBal.Period));
                sqlListParam.Add(new SqlParameter("@supCode", objGL_SuppBal.SupCode));
                sqlListParam.Add(new SqlParameter("@BegBal", objGL_SuppBal.BegBal));
                sqlListParam.Add(new SqlParameter("@DebitTrans", objGL_SuppBal.DebitTrans));
                sqlListParam.Add(new SqlParameter("@CreditTrans", objGL_SuppBal.CreditTrans));
                sqlListParam.Add(new SqlParameter("@CCYBegBal", objGL_SuppBal.CCYBegBal));
                sqlListParam.Add(new SqlParameter("@CCYDebitTrans", objGL_SuppBal.CCYDebitTrans));
                sqlListParam.Add(new SqlParameter("@CCYCreditTrans", objGL_SuppBal.CCYCreditTrans));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objGL_SuppBal.CreatedBy));

                //belum ada kayaknya
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateGL_SuppBal");
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
                sqlListParam.Add(new SqlParameter("@JurDetNum", objGL_SuppBal.Period));
                sqlListParam.Add(new SqlParameter("@JurDetNum", objGL_SuppBal.SupCode));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objGL_SuppBal.LastModifiedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteGL_SuppBal");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public GL_SuppBal RetrieveForSum(string Period, string suppcode, string companyCode)
        {
            string strSQL = "select isnull(sum(A.BegBal),0) as BegBal,isnull(sum(A.DebitTrans),0) as DebitTrans,isnull(sum(A.CreditTrans),0) as CreditTrans," +
                "isnull(sum(A.CCYBegBal),0) as CCYBegBal,isnull(sum(A.CCYDebitTrans),0) as CCYDebitTrans,isnull(sum(A.CCYCreditTrans),0) as CCYCreditTrans " +
                " from gl_suppbal A  where A.rowstatus>-1 and A.period='" + Period + "' and A.supcode = '" + suppcode + "' and A.CompanyCode='"+companyCode+"'";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectForSum(sqlDataReader);
                }
            }
            return new GL_SuppBal();
        }
        public override ArrayList Retrieve()
        {
            //string strSQL = "select GL_JurDet.*, GL_ChartofAccount.ChartName from GL_Jurdet inner join GL_CHart on GL_Jurdet.ChartNo = GL_ChartofAccount.ChartNo ";
            //DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            //strError = dataAccess.Error;
            arrGL_SuppBal = new ArrayList();
            //if (sqlDataReader.HasRows)
            //{
            //    while (sqlDataReader.Read())
            //    {
            //        arrGL_SuppBal.Add(GenerateObject(sqlDataReader));
            //    }
            //}
            //else
            //    arrGL_SuppBal.Add(new GL_SuppBal());
            return arrGL_SuppBal;
        }
        public string ClearSuppBalFrom_InActiveSupp(string period, string companyCode)
        {
            string strSQL = "delete GL_suppbal where SupCode not in (select SupCode from GL_Supplier and CompanyCode='"+companyCode+"' ) and period='" + period + "' and CompanyCode='"+companyCode+"'";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }
        public string ClearDebitCredit(string period, string companyCode)
        {
            string strSQL = "update GL_suppbal set debittrans=0,credittrans=0,ccydebittrans=0,ccycredittrans=0 where SupCode in (select SupCode from GL_Supplier and CompanyCode='" + companyCode + "' ) and period='" + period + "' and CompanyCode='" + companyCode + "'";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }
        public GL_SuppBal GenerateObject(SqlDataReader sqlDataReader)
        {
            try
            {
                objGL_SuppBal = new GL_SuppBal();
                objGL_SuppBal.ID = Convert.ToInt32(sqlDataReader["ID"]);
                objGL_SuppBal.Period = sqlDataReader["Period"].ToString();
                objGL_SuppBal.SupCode = sqlDataReader["SupCode"].ToString();
                objGL_SuppBal.BegBal = Convert.ToInt32(sqlDataReader["BegBal"]);
                objGL_SuppBal.DebitTrans = Convert.ToInt32(sqlDataReader["DebitTrans"]);
                objGL_SuppBal.CreditTrans = Convert.ToInt32(sqlDataReader["CreditTrans"]);
                objGL_SuppBal.CCYBegBal = Convert.ToInt32(sqlDataReader["CCYBegBal"]);
                objGL_SuppBal.CCYDebitTrans = Convert.ToInt32(sqlDataReader["CCYDebitTrans"]);
                objGL_SuppBal.CCYCreditTrans = Convert.ToInt32(sqlDataReader["CCYCreditTrans"]);
            }
            catch { }
            return objGL_SuppBal;
        }

        public GL_SuppBal GenerateObjectForSum(SqlDataReader sqlDataReader)
        {
            objGL_SuppBal = new GL_SuppBal();
            objGL_SuppBal.BegBal = Convert.ToInt32(sqlDataReader["BegBal"]);
            objGL_SuppBal.DebitTrans = Convert.ToInt32(sqlDataReader["DebitTrans"]);
            objGL_SuppBal.CreditTrans = Convert.ToInt32(sqlDataReader["CreditTrans"]);
            objGL_SuppBal.CCYBegBal = Convert.ToInt32(sqlDataReader["CCYBegBal"]);
            objGL_SuppBal.CCYDebitTrans = Convert.ToInt32(sqlDataReader["CCYDebitTrans"]);
            objGL_SuppBal.CCYCreditTrans = Convert.ToInt32(sqlDataReader["CCYCreditTrans"]);
            return objGL_SuppBal;
        }
    }

}
