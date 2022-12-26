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
    public class GL_ChartBalFacade : AbstractTransactionFacade
    {
        private GL_ChartBal objGL_ChartBal = new GL_ChartBal();
        private ArrayList arrGL_ChartBal;
        private List<SqlParameter> sqlListParam;

        public GL_ChartBalFacade(object objDomain)
            : base(objDomain)
        {
            objGL_ChartBal = (GL_ChartBal)objDomain;
        }

        public GL_ChartBalFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@period", objGL_ChartBal.Period));
                sqlListParam.Add(new SqlParameter("@ChartNo", objGL_ChartBal.ChartNo));
                sqlListParam.Add(new SqlParameter("@DeptCode", objGL_ChartBal.DeptCode));
                sqlListParam.Add(new SqlParameter("@CostCode", objGL_ChartBal.CostCode));
                sqlListParam.Add(new SqlParameter("@BegBal", objGL_ChartBal.BegBal));
                sqlListParam.Add(new SqlParameter("@DebitTrans", objGL_ChartBal.DebitTrans));
                sqlListParam.Add(new SqlParameter("@CreditTrans", objGL_ChartBal.CreditTrans));
                sqlListParam.Add(new SqlParameter("@CCYBegBal", objGL_ChartBal.CCYBegBal));
                sqlListParam.Add(new SqlParameter("@CCYDebitTrans", objGL_ChartBal.CCYDebitTrans));
                sqlListParam.Add(new SqlParameter("@CCYCreditTrans", objGL_ChartBal.CCYCreditTrans));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objGL_ChartBal.CreatedBy));
                sqlListParam.Add(new SqlParameter("@CompanyCode", objGL_ChartBal.CompanyCode));
                sqlListParam.Add(new SqlParameter("@PeriodType", objGL_ChartBal.PeriodType));
                //sqlListParam.Add(new SqlParameter("@Parent", objGL_ChartBal.Parent));

                //int intResult = transManager.DoTransaction(sqlListParam, "GLInsert_ChartBal");
                int intResult = transManager.DoTransaction(sqlListParam, "GLInsert_ChartBal_Period1");
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
                sqlListParam.Add(new SqlParameter("@period", objGL_ChartBal.Period));
                sqlListParam.Add(new SqlParameter("@ChartNo", objGL_ChartBal.ChartNo));
                sqlListParam.Add(new SqlParameter("@DeptCode", objGL_ChartBal.DeptCode));
                sqlListParam.Add(new SqlParameter("@CostCode", objGL_ChartBal.CostCode));
                sqlListParam.Add(new SqlParameter("@BegBal", objGL_ChartBal.BegBal));
                sqlListParam.Add(new SqlParameter("@DebitTrans", objGL_ChartBal.DebitTrans));
                sqlListParam.Add(new SqlParameter("@CreditTrans", objGL_ChartBal.CreditTrans));
                sqlListParam.Add(new SqlParameter("@CCYBegBal", objGL_ChartBal.CCYBegBal));
                sqlListParam.Add(new SqlParameter("@CCYDebitTrans", objGL_ChartBal.CCYDebitTrans));
                sqlListParam.Add(new SqlParameter("@CCYCreditTrans", objGL_ChartBal.CCYCreditTrans));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objGL_ChartBal.CreatedBy));
                sqlListParam.Add(new SqlParameter("@CompanyCode", objGL_ChartBal.CompanyCode));

                int intResult = transManager.DoTransaction(sqlListParam, "GLInsert_ChartBal_Period2");
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
                sqlListParam.Add(new SqlParameter("@period", objGL_ChartBal.Period));
                sqlListParam.Add(new SqlParameter("@ChartNo", objGL_ChartBal.ChartNo));
                sqlListParam.Add(new SqlParameter("@DeptCode", objGL_ChartBal.DeptCode));
                sqlListParam.Add(new SqlParameter("@CostCode", objGL_ChartBal.CostCode));
                sqlListParam.Add(new SqlParameter("@BegBal", objGL_ChartBal.BegBal));
                sqlListParam.Add(new SqlParameter("@DebitTrans", objGL_ChartBal.DebitTrans));
                sqlListParam.Add(new SqlParameter("@CreditTrans", objGL_ChartBal.CreditTrans));
                sqlListParam.Add(new SqlParameter("@CCYBegBal", objGL_ChartBal.CCYBegBal));
                sqlListParam.Add(new SqlParameter("@CCYDebitTrans", objGL_ChartBal.CCYDebitTrans));
                sqlListParam.Add(new SqlParameter("@CCYCreditTrans", objGL_ChartBal.CCYCreditTrans));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objGL_ChartBal.CreatedBy));
                sqlListParam.Add(new SqlParameter("@CompanyCode", objGL_ChartBal.CompanyCode));

                //belum ada SP-nya
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateGL_ChartBal");
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
                //sqlListParam.Add(new SqlParameter("@JurDetNum", objGL_ChartBal.Period));
                //sqlListParam.Add(new SqlParameter("@JurDetNum", objGL_ChartBal.SupCode));
                //sqlListParam.Add(new SqlParameter("@LastModifiedBy", objGL_ChartBal.LastModifiedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteGL_ChartBal");
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
            arrGL_ChartBal = new ArrayList();
            //if (sqlDataReader.HasRows)
            //{
            //    while (sqlDataReader.Read())
            //    {
            //        arrGL_ChartBal.Add(GenerateObject(sqlDataReader));
            //    }
            //}
            //else
            //    arrGL_ChartBal.Add(new GL_ChartBal());
            return arrGL_ChartBal;
        }
        public ArrayList RetrieveByPeriod(string Period, string companyCode)
        {
            string strSQL = "select A.ID,A.period,A.ChartNo,isnull(A.BegBal,0) as BegBal,isnull(A.DebitTrans,0) as DebitTrans," +
                "isnull(A.CreditTrans,0) as CreditTrans,isnull(A.CCYBegBal,0) as CCYBegBal,isnull(A.CCYDebitTrans,0) as CCYDebitTrans,isnull(A.CCYCreditTrans,0) as CCYCreditTrans, " +
                "A.DeptCode,A.CostCode,B.[Group],B.Postable,A.CreatedBy,A.CompanyCode from gl_chartbal A inner join GL_ChartOfAccount B on A.ChartNo=B.ChartNo and A.CompanyCode=B.CompanyCode where A.period=" + Period + " and A.CompanyCode='"+companyCode+"'";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGL_ChartBal = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_ChartBal.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_ChartBal.Add(new GL_ChartBal());
            return arrGL_ChartBal;
        }
        public ArrayList ForClosingByPeriod(string Period, string companyCode)
        {
            string strSQL = "select a.ChartNo,b.period,sum(IDRAmount) as Amount,SUM(CCYAmount) as CCYAmount,c.Parent,c.Postable,b.CompanyCode,c.Level from GL_JurDet as a inner join GL_JurHead as b on a.jurheadnum = b.jurheadnum and a.RowStatus>-1 " +
                            "inner join GL_ChartOfAccount as c on c.ChartNo = a.ChartNo and c.RowStatus > -1 "+
                            "where b.period = '"+Period+"' and b.CompanyCode = '"+companyCode+"' and b.RowStatus > -1 group by b.CompanyCode,a.ChartNo,b.period,Parent,Postable,Level";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGL_ChartBal = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_ChartBal.Add(GenerateObjectForChartBal(sqlDataReader));
                }
            }
            else
                arrGL_ChartBal.Add(new GL_ChartBal());
            return arrGL_ChartBal;
        }

        public ArrayList RetrieveByPeriodChartNo(string Period,string chartno, string companyCode)
        {
            string strSQL = "select A.ID,A.period,A.ChartNo,(select ISNULL(sum(begbal),0) from gl_chartbal " +
                "where period=A.period and gl_chartbal.CompanyCode=A.CompanyCode and ChartNo like rtrim(A.ChartNo) + '%') as BegBal,  " +
                "(select ISNULL(sum(IDRAmount),0) from gl_jurdet where JurHeadNum in (select JurHeadNum from GL_JurHead where period=A.period and GL_JurHead.CompanyCode=A.CompanyCode) " +
                "and ChartNo like rtrim(A.ChartNo) + '%' and IDRAmount>0) as DebitTrans,   " +
                "(select ISNULL(sum(IDRAmount),0) from gl_jurdet where JurHeadNum in (select JurHeadNum from GL_JurHead where period=A.period and GL_JurHead.CompanyCode=A.CompanyCode) " +
                "and ChartNo like rtrim(A.ChartNo) + '%' and IDRAmount<0) as CreditTrans,   " +
                "isnull(A.CCYBegBal,0) as CCYBegBal, isnull(A.CCYDebitTrans,0) as CCYDebitTrans,isnull(A.CCYCreditTrans,0) as CCYCreditTrans,  " +
                "case when A.DeptCode='' then 'ALL' else A.DeptCode end DeptCode,case when A.CostCode='' then 'ALL' else A.CostCode end CostCode, " +
                "B.[Group],B.Postable,A.CreatedBy,A.CompanyCode from gl_chartbal A inner join GL_ChartOfAccount B on A.ChartNo=B.ChartNo and A.CompanyCode=B.CompanyCode where A.period='" +
                Period + "' and A.CompanyCode='"+companyCode+"'";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGL_ChartBal = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_ChartBal.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_ChartBal.Add(new GL_ChartBal());

            return arrGL_ChartBal;
        }
        public ArrayList RetrieveByRLMonth(string Period, string rlmonth, string companyCode)
        {
            string strSQL = "select A.ID,A.period,A.ChartNo,isnull(A.BegBal,0) as BegBal,isnull(A.DebitTrans,0) as DebitTrans," +
                "isnull(A.CreditTrans,0) as CreditTrans,isnull(A.CCYBegBal,0) as CCYBegBal,isnull(A.CCYDebitTrans,0) as CCYDebitTrans,isnull(A.CCYCreditTrans,0) as CCYCreditTrans, " +
                "A.DeptCode,A.CostCode,B.[Group],B.Postable,A.CreatedBy,A.CompanyCode from gl_chartbal A inner join GL_ChartOfAccount B on A.ChartNo=B.ChartNo and A.CompanyCode=B.CompanyCode where A.period=" +
                Period + "and chartno='" + rlmonth + "' and A.CompanyCode='"+companyCode+"'";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGL_ChartBal = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_ChartBal.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_ChartBal.Add(new GL_ChartBal());
            return arrGL_ChartBal;
        }
        public GL_ChartBal RetrieveForCalcBal(string Period, string chartno, string companyCode)
        {
            string strSQL = "select isnull(sum(A.BegBal),0) as BegBal,isnull(sum(A.DebitTrans),0) as DebitTrans,isnull(sum(A.CreditTrans),0) as CreditTrans," +
                "isnull(sum(A.CCYBegBal),0) as CCYBegBal,isnull(sum(A.CCYDebitTrans),0) as CCYDebitTrans,isnull(sum(A.CCYCreditTrans),0) as CCYCreditTrans " +
                " from gl_chartbal A inner join GL_ChartOfAccount B on A.ChartNo=B.ChartNo and A.CompanyCode=B.CompanyCode where A.rowstatus>-1 and A.period='" +
                Period + "' and B.chartno like '" + chartno + "%' and B.postable=1 and A.CompanyCode='"+companyCode+"'" ;
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
            return new GL_ChartBal();
        }
        //public int GetLR(string Period)
        public decimal GetLR(string Period, string companyCode)
        {
            string strSQL = "select  isnull(SUM(begbal+debittrans+credittrans),0) as LR from GL_ChartBal where ChartNo in "+
                "(select ChartNo from GL_ChartOfAccount where [level]=0 and [group]<4) and period='" + Period + "' and CompanyCode='"+companyCode+"'";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToDecimal( sqlDataReader["LR"]);
                }
            }
            return 0;
        }
        public string CleanUp(string Period, string MLR, string companyCode)
        {
            //string strSQL = "pake rowstatus=-1 aja boleh kah";
            string strSQL = "delete gl_chartbal where isnull(begbal,0)=0 and isnull(debittrans,0)=0 and isnull(credittrans,0)=0 and period='" + Period + "' and CompanyCode='" + companyCode + "' " +
                "delete GL_JurDet where ChartNo='" + MLR + "' and JurHeadNum in (select JurHeadNum from GL_JurHead where period="+ Period + " and GL_JurHead.CompanyCode='"+companyCode+"')" +
                "delete GL_JurHead where JurHeadNum in (select JurHeadNum from GL_JurDet where ChartNo='" + MLR + "' and JurHeadNum in (select JurHeadNum from GL_JurHead where period=" + Period + " and GL_JurHead.CompanyCode='" + companyCode + "') ) and CompanyCode='"+companyCode+"'";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }

        public int GetTotalBalance(string Kriteria,string periode, string companyCode)
        {
            string strSQL = "select isnull(SUM(amount),0) as amount from ( " +
                "select A.[NotesNo] ,A.chartno As chart, " +
                "rtrim(A.ChartNo) as Chartno, A.[Postable], (space(A.[level] *4)) +A.Chartname as Chartname ,A.[Level], A.[Group], A.[ccycode],  " +
                "cast((B.[BegBal] + B.[DebitTrans] + B.[CreditTrans])as int)  as Amount " +
                "FROM GL_ChartOfAccount A INNER JOIN GL_ChartBal B ON A.[ChartNo] = B.[ChartNo]  " +
                "WHERE B.[Period] = '" + periode + "' AND A.[Level] <= 2 and [Group] <4 and A.CompanyCode=B.CompanyCode and A.CompanyCode='"+ companyCode + "' " +

                "union all " +
                "select A.[NotesNo] ,A.chartno As chart, " +
                "RTRIM(A.ChartNo)+'zz' as Chartno, A.[Postable], (space(A.[level] *4)) +A.Chartname as Chartname ,A.[Level], A.[Group], A.[ccycode],  " +
                "cast((B.[BegBal] + B.[DebitTrans] + B.[CreditTrans])as int)  as Amount " +
                "FROM GL_ChartOfAccount A INNER JOIN GL_ChartBal B ON A.[ChartNo] = B.[ChartNo]  " +
                "WHERE B.[Period] = '" + periode + "' AND A.[Level] <= 2 and [Group] <4 and len(rtrim(A.ChartNo))=2 and A.Postable=0 and A.CompanyCode=B.CompanyCode and A.CompanyCode='" + companyCode + "' " +

                "union all " +
                "select A.[NotesNo] ,A.chartno As chart, " +
                "RTRIM(A.ChartNo)+'zzzz' as Chartno, A.[Postable], (space(A.[level] *4)) +A.Chartname as Chartname ,A.[Level], A.[Group], A.[ccycode],  " +
                "cast((B.[BegBal] + B.[DebitTrans] + B.[CreditTrans])as int)as Amount " +
                "FROM GL_ChartOfAccount A INNER JOIN GL_ChartBal B ON A.[ChartNo] = B.[ChartNo]  " +
                "WHERE B.[Period] = '" + periode + "' AND A.[Level] <= 2 and [Group] <4 and len(rtrim(A.ChartNo))=1 and left(A.[ChartNo],1)<> '1%' and A.CompanyCode=B.CompanyCode and A.CompanyCode='" + companyCode + "' ) as N  " +
                "where " + Kriteria;

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["amount"]);
                }
            }
            return 0;
        }
        public ArrayList RetrieveJurnalByPeriod(string drPeriod, string sdPeriod, string companyCode)
        {
            string strSQL = "select a.period,b.ChartNo,DeptCode,CostCode,0 as BegBal,case when b.IDRAmount>0 then b.IDRAmount else 0 end DebitTrans,case when b.IDRAmount<0 then b.IDRAmount else 0 end CreditTrans,case when b.IDRAmount>0 then b.IDRAmount else 0 end CCYDebitTrans,case when b.IDRAmount<0 then b.IDRAmount else 0 end CCYCreditTrans, 0 as CCYBegBal, c.[Group], c.Postable,A.CreatedBy,A.CompanyCode " +
                "from GL_JurHead as a, GL_JurDet as b, GL_ChartOfAccount as c where a.JurHeadNum=b.JurHeadNum and a.RowStatus>-1 and b.RowStatus>-1 and b.ChartNo=c.ChartNo and CONVERT(varchar,JurDate,112) >='"+drPeriod+"' and CONVERT(varchar,JurDate,112) <='"+sdPeriod+"' and A.CompanyCode='"+companyCode+"'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGL_ChartBal = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_ChartBal.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_ChartBal.Add(new GL_ChartBal());
            return arrGL_ChartBal;
        }

        public GL_ChartBal GenerateObject(SqlDataReader sqlDataReader)
        {
            try
            {
                objGL_ChartBal = new GL_ChartBal();
                objGL_ChartBal.ID = Convert.ToInt32(sqlDataReader["ID"]);
                objGL_ChartBal.Period = sqlDataReader["Period"].ToString();
                objGL_ChartBal.ChartNo = sqlDataReader["ChartNo"].ToString();
                objGL_ChartBal.DeptCode = sqlDataReader["DeptCode"].ToString();
                objGL_ChartBal.CostCode = sqlDataReader["CostCode"].ToString();
                objGL_ChartBal.Group = sqlDataReader["Group"].ToString();
                objGL_ChartBal.BegBal = Convert.ToDecimal(sqlDataReader["BegBal"]);
                objGL_ChartBal.DebitTrans = Convert.ToDecimal(sqlDataReader["DebitTrans"]);
                objGL_ChartBal.CreditTrans = Convert.ToDecimal(sqlDataReader["CreditTrans"]);
                objGL_ChartBal.CCYBegBal = Convert.ToDecimal(sqlDataReader["CCYBegBal"]);
                objGL_ChartBal.CCYDebitTrans = Convert.ToDecimal(sqlDataReader["CCYDebitTrans"]);
                objGL_ChartBal.CCYCreditTrans = Convert.ToDecimal(sqlDataReader["CCYCreditTrans"]);
                objGL_ChartBal.Postable = Convert.ToInt32(sqlDataReader["Postable"]);
                objGL_ChartBal.CreateBy = sqlDataReader["CreatedBy"].ToString();
                objGL_ChartBal.CompanyCode = sqlDataReader["CompanyCode"].ToString();
            }
            catch { }
            return objGL_ChartBal;
        }
        public GL_ChartBal GenerateObjectForSum(SqlDataReader sqlDataReader)
        {
            objGL_ChartBal = new GL_ChartBal();
            objGL_ChartBal.BegBal = Convert.ToDecimal (sqlDataReader["BegBal"]);
            objGL_ChartBal.DebitTrans = Convert.ToDecimal(sqlDataReader["DebitTrans"]);
            objGL_ChartBal.CreditTrans = Convert.ToDecimal(sqlDataReader["CreditTrans"]);
            objGL_ChartBal.CCYBegBal = Convert.ToDecimal(sqlDataReader["CCYBegBal"]);
            objGL_ChartBal.CCYDebitTrans = Convert.ToDecimal(sqlDataReader["CCYDebitTrans"]);
            objGL_ChartBal.CCYCreditTrans = Convert.ToDecimal(sqlDataReader["CCYCreditTrans"]);
            return objGL_ChartBal;
        }
        public GL_ChartBal GenerateObjectForChartBal(SqlDataReader sqlDataReader)
        {
            try
            {
                objGL_ChartBal = new GL_ChartBal();
                objGL_ChartBal.Period = sqlDataReader["Period"].ToString();
                objGL_ChartBal.ChartNo = sqlDataReader["ChartNo"].ToString();
                objGL_ChartBal.Amount = Convert.ToDecimal(sqlDataReader["Amount"]);
                objGL_ChartBal.CCYAmount = Convert.ToDecimal(sqlDataReader["CCYAmount"]);
                objGL_ChartBal.Postable = Convert.ToInt32(sqlDataReader["Postable"]);
                objGL_ChartBal.Parent = sqlDataReader["Parent"].ToString();
                objGL_ChartBal.CompanyCode = sqlDataReader["CompanyCode"].ToString();
                objGL_ChartBal.Level = Convert.ToInt32(sqlDataReader["Level"]);

                //objGL_ChartBal.DeptCode = sqlDataReader["DeptCode"].ToString();
                //objGL_ChartBal.CostCode = sqlDataReader["CostCode"].ToString();
                //objGL_ChartBal.Group = sqlDataReader["Group"].ToString();
                //objGL_ChartBal.CreditTrans = Convert.ToDecimal(sqlDataReader["CreditTrans"]);
                //objGL_ChartBal.CCYBegBal = Convert.ToDecimal(sqlDataReader["CCYBegBal"]);
                //objGL_ChartBal.CCYDebitTrans = Convert.ToDecimal(sqlDataReader["CCYDebitTrans"]);
                //objGL_ChartBal.CCYCreditTrans = Convert.ToDecimal(sqlDataReader["CCYCreditTrans"]);
                //objGL_ChartBal.CreateBy = sqlDataReader["CreatedBy"].ToString();
            }
            catch { }
            return objGL_ChartBal;
        }



    }
}
