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
    public class GL_JurHeadFacade : AbstractTransactionFacade
    {
        private GL_JurHead objGL_JurHead = new GL_JurHead();
        private ArrayList arrGL_JurHead;
        private List<SqlParameter> sqlListParam;

        public GL_JurHeadFacade(object objDomain)
            : base(objDomain)
        {
            objGL_JurHead = (GL_JurHead)objDomain;
        }

        public GL_JurHeadFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Period", objGL_JurHead.Period));
                sqlListParam.Add(new SqlParameter("@VoucherCode", objGL_JurHead.VoucherCode));
                sqlListParam.Add(new SqlParameter("@JurNo", objGL_JurHead.JurNo));
                sqlListParam.Add(new SqlParameter("@JurDate", objGL_JurHead.JurDate));
                sqlListParam.Add(new SqlParameter("@JurDesc", objGL_JurHead.JurDesc));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objGL_JurHead.CreatedBy));
                sqlListParam.Add(new SqlParameter("@CompanyCode", objGL_JurHead.CompanyCode));
                sqlListParam.Add(new SqlParameter("@PTID", objGL_JurHead.PTID));

                int intResult = transManager.DoTransaction(sqlListParam, "GLInsert_JurHead");
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
                sqlListParam.Add(new SqlParameter("@Period", objGL_JurHead.Period));
                sqlListParam.Add(new SqlParameter("@VoucherCode", objGL_JurHead.VoucherCode));
                sqlListParam.Add(new SqlParameter("@JurNo", objGL_JurHead.JurNo));
                sqlListParam.Add(new SqlParameter("@JurDate", objGL_JurHead.JurDate));
                sqlListParam.Add(new SqlParameter("@JurDesc", objGL_JurHead.JurDesc));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objGL_JurHead.CreatedBy));
                sqlListParam.Add(new SqlParameter("@CompanyCode", objGL_JurHead.CompanyCode));

                sqlListParam.Add(new SqlParameter("@PTID", objGL_JurHead.PTID));

                //belum ada SP
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateGL_JurHead");

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
                sqlListParam.Add(new SqlParameter("@JurHeadNum", objGL_JurHead.JurHeadNum));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objGL_JurHead.LastModifiedBy));

                //belum ada SP
                int intResult = transManager.DoTransaction(sqlListParam, "GLDelete_Journal");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int InsertToTemp(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Period", objGL_JurHead.Period));
                sqlListParam.Add(new SqlParameter("@VoucherCode", objGL_JurHead.VoucherCode));
                sqlListParam.Add(new SqlParameter("@JurNo", objGL_JurHead.JurNo));
                sqlListParam.Add(new SqlParameter("@JurDate", objGL_JurHead.JurDate));
                sqlListParam.Add(new SqlParameter("@JurDesc", objGL_JurHead.JurDesc));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objGL_JurHead.CreatedBy));
                sqlListParam.Add(new SqlParameter("@CompanyCode", objGL_JurHead.CompanyCode));
                sqlListParam.Add(new SqlParameter("@PTID", objGL_JurHead.PTID));
                sqlListParam.Add(new SqlParameter("@JurHeadNumID", objGL_JurHead.JurHeadNumID));
                //sqlListParam.Add(new SqlParameter("@FlagTipe", objGL_JurHead.flagTipe));

                int intResult = transManager.DoTransaction(sqlListParam, "GLInsert_JurHeadTemp");

                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int DeleteToTemp(TransactionManager transManager)
        {

            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@JurHeadNum", objGL_JurHead.JurHeadNum));

                int intResult = transManager.DoTransaction(sqlListParam, "GLDelete_JurHeadTemp");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        #region Add By Anang 13-09-2018 Delete Temp
        public int DeleteTemp(TransactionManager transactionManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@JurHeadNum", objGL_JurHead.JurHeadNum));
                int intResult = transactionManager.DoTransaction(sqlListParam, "GLDelete_Temp");
                strError = transactionManager.Error;

                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        #endregion
        public int ClearToTemp(TransactionManager transManager)
        {

            try
            {
                sqlListParam = new List<SqlParameter>();

                int intResult = transManager.DoTransaction(sqlListParam, "GLClear_JurHeadTemp");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int SaveJournal(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Period", objGL_JurHead.Period));
                sqlListParam.Add(new SqlParameter("@JurNo", objGL_JurHead.JurNo));
                sqlListParam.Add(new SqlParameter("@VoucherCode", objGL_JurHead.VoucherCode));
                sqlListParam.Add(new SqlParameter("@JurDate", objGL_JurHead.JurDate));
                sqlListParam.Add(new SqlParameter("@JurDesc", objGL_JurHead.JurDesc));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objGL_JurHead.CreatedBy));
                sqlListParam.Add(new SqlParameter("@CompanyCode", objGL_JurHead.CompanyCode));
                sqlListParam.Add(new SqlParameter("@JurHeadNum", objGL_JurHead.JurHeadNum));
                //sqlListParam.Add(new SqlParameter("@PTID", objGL_JurHead.PTID));

                int intResult = transManager.DoTransaction(sqlListParam, "GLSave_Journal");

                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int ApprovalJournal(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Period", objGL_JurHead.Period));
                sqlListParam.Add(new SqlParameter("@JurNo", objGL_JurHead.JurNo));
                sqlListParam.Add(new SqlParameter("@VoucherCode", objGL_JurHead.VoucherCode));
                sqlListParam.Add(new SqlParameter("@JurDate", objGL_JurHead.JurDate));
                //sqlListParam.Add(new SqlParameter("@JurDesc", objGL_JurHead.JurDesc));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objGL_JurHead.CreatedBy));
                sqlListParam.Add(new SqlParameter("@CompanyCode", objGL_JurHead.CompanyCode));
                sqlListParam.Add(new SqlParameter("@JurHeadNum", objGL_JurHead.JurHeadNum));
                //sqlListParam.Add(new SqlParameter("@PTID", objGL_JurHead.PTID));
                sqlListParam.Add(new SqlParameter("@VoucherCode0", objGL_JurHead.VoucherCode0));

                int intResult = transManager.DoTransaction(sqlListParam, "GLApproval_0ke1");

                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int NoApprovalJournal(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@JurHeadNum0", objGL_JurHead.JurHeadNum));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objGL_JurHead.CreatedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "GLNoApproval_0ke1");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from GL_JurHead");
            strError = dataAccess.Error;
            arrGL_JurHead = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_JurHead.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_JurHead.Add(new GL_JurHead());

            return arrGL_JurHead;
        }
        public ArrayList RetrieveGL0ByPeriodForApproval(string Period, string companyCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from GL_JurHead0 where Period='" + Period + "' and RowStatus=0 and CompanyCode='" + companyCode + "' order by JurHeadNum");
            strError = dataAccess.Error;
            arrGL_JurHead = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_JurHead.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_JurHead.Add(new GL_JurHead());
            return arrGL_JurHead;
        }
        public ArrayList RetrieveGL0ByPeriod(string Period, string companyCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from GL_JurHead0 where Period='" + Period + "' and RowStatus>-1 and CompanyCode='" + companyCode + "' order by JurHeadNum");
            strError = dataAccess.Error;
            arrGL_JurHead = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_JurHead.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_JurHead.Add(new GL_JurHead());
            return arrGL_JurHead;
        }
        //public ArrayList RetrieveByPeriod(string Period, string companyCode)
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from GL_JurHead where Period='" + Period + "' and RowStatus>-1 and CompanyCode='"+companyCode+"' order by JurHeadNum Desc");
        //    strError = dataAccess.Error;
        //    arrGL_JurHead = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrGL_JurHead.Add(GenerateObject(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrGL_JurHead.Add(new GL_JurHead());
        //    return arrGL_JurHead;
        //}
        public ArrayList RetrieveByPeriod(string Period, string companyCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select TOP 100 * from GL_JurHead where Period='" + Period + "' and RowStatus>-1 and CompanyCode='" + companyCode + "' order by JurHeadNum Desc");
            strError = dataAccess.Error;
            arrGL_JurHead = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_JurHead.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_JurHead.Add(new GL_JurHead());
            return arrGL_JurHead;
        }
        public ArrayList RetrieveByPeriod2(string drPeriod, string sdPeriod)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from GL_JurHead where RowStatus>-1 and CONVERT(varchar,JurDate,112) >='"+drPeriod+"' and CONVERT(varchar,JurDate,112) <='"+sdPeriod+"'");
            strError = dataAccess.Error;
            arrGL_JurHead = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_JurHead.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_JurHead.Add(new GL_JurHead());

            return arrGL_JurHead;
        }
        public string GetJurnoByPeriod(string Period, string vouchercode, string companyCode)
        {
            string jurno = string.Empty;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 1 jurno from GL_JurHead where Period='" + Period +
                "' and vouchercode='" + vouchercode + "' and CompanyCode='"+companyCode+"' order by jurno desc" );
            strError = dataAccess.Error;
            arrGL_JurHead = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    jurno = sqlDataReader["JurNo"].ToString();
                }
            }
            else
                jurno = "0";
            return jurno;
        }
        public string TempGetJurnoByPeriod(string Period, string vouchercode, string companyCode)
        {
            string jurno = string.Empty;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 1 jurno from GL_JurHeadTemp where Period='" + Period +
                "' and vouchercode='" + vouchercode + "' and CompanyCode='" + companyCode + "' order by jurno desc");
            strError = dataAccess.Error;
            arrGL_JurHead = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    jurno = sqlDataReader["JurNo"].ToString();
                }
            }
            else
                jurno = "0";
            return jurno;
        }
        public GL_JurHead RetrieveByHeadNum(int GL_JurHeadNum)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from gl_Jurhead where JurHeadNum = " + GL_JurHeadNum );
            strError = dataAccess.Error;
            arrGL_JurHead = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new GL_JurHead();
        }
        public GL_JurHead RetrieveGl0ByHeadNum(int GL_JurHeadNum)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from gl_Jurhead0 where JurHeadNum = " + GL_JurHeadNum);
            strError = dataAccess.Error;
            arrGL_JurHead = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new GL_JurHead();
        }
        public GL_JurHead TempRetrieveByHeadNum(string Period, string vouchercode, string companyCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from GL_JurHeadTemp where VoucherCode='"+vouchercode+"' and period='"+Period+"' and CompanyCode='"+companyCode+"'");
            strError = dataAccess.Error;
            arrGL_JurHead = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new GL_JurHead();
        }
        public DateTime getLastJurDate(string thnbln, string compCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(MAX(JurDate),'') as JurDate from GL_JurHead where left(convert(varchar,JurDate,112),6)='" + thnbln + "' and CompanyCode='"+compCode+"'");
            strError = dataAccess.Error;
            arrGL_JurHead = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToDateTime(sqlDataReader["JurDate"]);
                }
            }

            return DateTime.Now;
        }
        public ArrayList GetAllJurnal(string period, string companycode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from GL_JurHead where Period='" + period + "' and RowStatus>-1 and CompanyCode='" + companycode + "' order by JurHeadNum Desc");
            strError = dataAccess.Error;
            arrGL_JurHead = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_JurHead.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_JurHead.Add(new GL_JurHead());
            return arrGL_JurHead;
        }
        public GL_JurHead GenerateObject(SqlDataReader sqlDataReader)
        {
            objGL_JurHead = new GL_JurHead();
            objGL_JurHead.ID = Convert.ToInt32(sqlDataReader["JurHeadNum"]);
            objGL_JurHead.Period = sqlDataReader["Period"].ToString();
            objGL_JurHead.VoucherCode = sqlDataReader["VoucherCode"].ToString();
            objGL_JurHead.JurNo = sqlDataReader["JurNo"].ToString();
            objGL_JurHead.JurDate = Convert.ToDateTime(sqlDataReader["JurDate"]);
            objGL_JurHead.JurDesc = sqlDataReader["JurDesc"].ToString();
            objGL_JurHead.JurHeadNum = Convert.ToInt32(sqlDataReader["JurHeadNum"]);
            objGL_JurHead.CompanyCode= sqlDataReader["CompanyCode"].ToString();
            objGL_JurHead.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objGL_JurHead.CreatedBy = sqlDataReader["CreatedBy"].ToString();

            return objGL_JurHead;
        }
    }
}
