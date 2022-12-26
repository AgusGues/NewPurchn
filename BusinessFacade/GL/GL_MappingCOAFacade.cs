using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;

namespace BusinessFacade.GL
{
    public class GL_MappingCOAFacade : AbstractTransactionFacade
    {
        private GL_MappingCOA objGL_MappingCOA = new GL_MappingCOA();
        private ArrayList arrGL_MappingCOA;
        private List<SqlParameter> sqlListParam; 

        public GL_MappingCOAFacade(object objDomain)
            : base(objDomain)
        {
            objGL_MappingCOA = (GL_MappingCOA)objDomain;
        }

        public GL_MappingCOAFacade()
        {
        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                //sqlListParam.Add(new SqlParameter("@Period", objGL_JurHead.Period));
                //sqlListParam.Add(new SqlParameter("@VoucherCode", objGL_JurHead.VoucherCode));
                //sqlListParam.Add(new SqlParameter("@JurNo", objGL_JurHead.JurNo));
                //sqlListParam.Add(new SqlParameter("@JurDate", objGL_JurHead.JurDate));
                //sqlListParam.Add(new SqlParameter("@JurDesc", objGL_JurHead.JurDesc));
                //sqlListParam.Add(new SqlParameter("@CreatedBy", objGL_JurHead.CreatedBy));
                //sqlListParam.Add(new SqlParameter("@CompanyCode", objGL_JurHead.CompanyCode));
                //sqlListParam.Add(new SqlParameter("@PTID", objGL_JurHead.PTID));

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
                //sqlListParam.Add(new SqlParameter("@Period", objGL_JurHead.Period));
                //sqlListParam.Add(new SqlParameter("@VoucherCode", objGL_JurHead.VoucherCode));
                //sqlListParam.Add(new SqlParameter("@JurNo", objGL_JurHead.JurNo));
                //sqlListParam.Add(new SqlParameter("@JurDate", objGL_JurHead.JurDate));
                //sqlListParam.Add(new SqlParameter("@JurDesc", objGL_JurHead.JurDesc));
                //sqlListParam.Add(new SqlParameter("@LastModifiedBy", objGL_JurHead.CreatedBy));
                //sqlListParam.Add(new SqlParameter("@CompanyCode", objGL_JurHead.CompanyCode));

                //sqlListParam.Add(new SqlParameter("@PTID", objGL_JurHead.PTID));

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
                //sqlListParam.Add(new SqlParameter("@JurHeadNum", objGL_JurHead.JurHeadNum));
                //sqlListParam.Add(new SqlParameter("@LastModifiedBy", objGL_JurHead.LastModifiedBy));

                //belum ada SP
                int intResult = transManager.DoTransaction(sqlListParam, "spDelete_JurHead");

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
            arrGL_MappingCOA = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_MappingCOA.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_MappingCOA.Add(new GL_MappingCOA());

            return arrGL_MappingCOA;
        }
        public ArrayList RetrieveGL0ByPeriodForApproval(string Period, string companyCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from GL_JurHead0 where Period='" + Period + "' and RowStatus=0 and CompanyCode='" + companyCode + "' order by JurHeadNum");
            strError = dataAccess.Error;
            arrGL_MappingCOA = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_MappingCOA.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_MappingCOA.Add(new GL_MappingCOA());
            return arrGL_MappingCOA;
        }
        public string TempGetJurnoByPeriod(string Period, string vouchercode, string companyCode)
        {
            string jurno = string.Empty;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 1 jurno from GL_JurHeadTemp where Period='" + Period +
                "' and vouchercode='" + vouchercode + "' and CompanyCode='" + companyCode + "' order by jurno desc");
            strError = dataAccess.Error;
            arrGL_MappingCOA = new ArrayList();

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
        public GL_MappingCOA RetrieveByHeadNum(int GL_JurHeadNum)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from GL_MappingCOA where JurHeadNum = " + GL_JurHeadNum);
            strError = dataAccess.Error;
            arrGL_MappingCOA = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new GL_MappingCOA();
        }
        public GL_MappingCOA RetrieveByTypetrxTableNameDepoid(string companyCode, string typeTrx, string tableName, int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from TrxMappingCOA where CompanyCode='"+companyCode+"' and TypeTrx='"+typeTrx+"' and DepoID="+depoID+" and TableName='"+tableName+"'");
            strError = dataAccess.Error;
            arrGL_MappingCOA = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new GL_MappingCOA();
        }
        public GL_MappingCOA RetrieveGl0ByHeadNum(int GL_JurHeadNum)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from gl_Jurhead0 where JurHeadNum = " + GL_JurHeadNum);
            strError = dataAccess.Error;
            arrGL_MappingCOA = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new GL_MappingCOA();
        }
        public GL_MappingCOA TempRetrieveByHeadNum(string Period, string vouchercode, string companyCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from GL_JurHeadTemp where VoucherCode='" + vouchercode + "' and period='" + Period + "' and CompanyCode='" + companyCode + "'");
            strError = dataAccess.Error;
            arrGL_MappingCOA = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new GL_MappingCOA();
        }
        public DateTime getLastJurDate(string thnbln, string compCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(MAX(JurDate),'') as JurDate from GL_MappingCOA where left(convert(varchar,JurDate,112),6)='" + thnbln + "' and CompanyCode='" + compCode + "'");
            strError = dataAccess.Error;
            arrGL_MappingCOA = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToDateTime(sqlDataReader["JurDate"]);
                }
            }

            return DateTime.Now;
        }
        public GL_MappingCOA GenerateObject(SqlDataReader sqlDataReader)
        {
            objGL_MappingCOA = new GL_MappingCOA();
            objGL_MappingCOA.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objGL_MappingCOA.PTID = Convert.ToInt32(sqlDataReader["PTID"]);
            objGL_MappingCOA.PT = sqlDataReader["PTID"].ToString();
            objGL_MappingCOA.CompanyCode = sqlDataReader["CompanyCode"].ToString();
            objGL_MappingCOA.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"]);
            objGL_MappingCOA.DepoID = Convert.ToInt32(sqlDataReader["DepoID"]);
            objGL_MappingCOA.TableName = sqlDataReader["TableName"].ToString();
            objGL_MappingCOA.ItemIDPlant = Convert.ToInt32(sqlDataReader["ItemIDPlant"]);
            objGL_MappingCOA.ItemIDPusat = Convert.ToInt32(sqlDataReader["ItemIDPusat"]);
            objGL_MappingCOA.TypeTrx = sqlDataReader["TypeTrx"].ToString();
            objGL_MappingCOA.GroupPurchnCodeID = sqlDataReader["GroupPurchnCodeID"].ToString();
            objGL_MappingCOA.CoaID = Convert.ToInt32(sqlDataReader["CoaID"]);
            objGL_MappingCOA.DebetCredit = sqlDataReader["DebetCredit"].ToString();
            objGL_MappingCOA.CoaIDDebit = Convert.ToInt32(sqlDataReader["CoaIDDebet"]);
            objGL_MappingCOA.CoaIDCredit = Convert.ToInt32(sqlDataReader["CoaIDCredit"]);


            return objGL_MappingCOA;
        }
    }

}
