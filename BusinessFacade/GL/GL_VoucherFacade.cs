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
    public class GL_VoucherFacade : AbstractFacade
    {
        private GL_Voucher  objGL_Voucher = new GL_Voucher();
        private ArrayList arrGL_Voucher;
        private List<SqlParameter> sqlListParam;

        public GL_VoucherFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objGL_Voucher = (GL_Voucher)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@VoucherCode", objGL_Voucher.VoucherCode));
                sqlListParam.Add(new SqlParameter("@VoucherName", objGL_Voucher.VoucherName));
                sqlListParam.Add(new SqlParameter("@ChartNo", objGL_Voucher.ChartNo));
                sqlListParam.Add(new SqlParameter("@DC", objGL_Voucher.DC));
                sqlListParam.Add(new SqlParameter("@BankAccountID", objGL_Voucher.BankAccountID));
                sqlListParam.Add(new SqlParameter("@TransDocID", objGL_Voucher.TransDocID));
                sqlListParam.Add(new SqlParameter("@TypeTRX", objGL_Voucher.TypeTRX));
                sqlListParam.Add(new SqlParameter("@SignedPerson", objGL_Voucher.SignedPerson));
                sqlListParam.Add(new SqlParameter("@PrintMode", objGL_Voucher.PrintMode));
                sqlListParam.Add(new SqlParameter("@CompanyCode", objGL_Voucher.CompanyCode));
                sqlListParam.Add(new SqlParameter("@PTID", objGL_Voucher.PTID));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objGL_Voucher.CreatedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "GLInsert_Voucher");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Update(object objDomain)
        {
            return 0;
        }

        public override int Delete(object objDomain)
        {

            try
            {
                objGL_Voucher = (GL_Voucher)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objGL_Voucher.ID));
                

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteGL_Voucher");

                strError = dataAccess.Error;

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
            string strSQL = "Select * from GL_Voucher where isnull(RowStatus,0) = 0 order by VoucherName";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGL_Voucher = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_Voucher.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_Voucher.Add(new GL_Voucher());

            return arrGL_Voucher;
        }
        public ArrayList RetrieveWithCompany(string companyCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "Select * from GL_Voucher where isnull(RowStatus,0) = 0 and CompanyCode='"+companyCode+"' order by VoucherName";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGL_Voucher = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_Voucher.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_Voucher.Add(new GL_Voucher());

            return arrGL_Voucher;
        }


        public ArrayList RetrieveById(int ID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from GL_Voucher where isnull(RowStatus,0) and ID = " + ID);
            strError = dataAccess.Error;
            arrGL_Voucher = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_Voucher.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_Voucher.Add(new GL_Voucher());

            return arrGL_Voucher;
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from GL_Voucher where isnull(RowStatus,0) >-1  and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrGL_Voucher = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_Voucher.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_Voucher.Add(new GL_Voucher());

            return arrGL_Voucher;
        }
        public GL_Voucher RetrieveByCriteria1(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from GL_Voucher where isnull(RowStatus,0) >-1  and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrGL_Voucher = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
           
            return new GL_Voucher();
        }
        public GL_Voucher RetrieveByCode(string strCodeVoucher, string companyCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from GL_Voucher where isnull(RowStatus,0) >-1  and VoucherCode='"+strCodeVoucher+"' and CompanyCode='"+companyCode+"'");
            strError = dataAccess.Error;
            arrGL_Voucher = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new GL_Voucher();
        }


        public ArrayList RetrieveByCompanyCodeAndPTID(string strValue, string CompanyCode, int PTID)
        {
            string strSQL = "SELECT vc.ID, vc.VoucherCode, vc.VoucherName, ISNULL(vc.ChartNo, '') ChartNo, ISNULL(coa.ChartName, '') ChartName, vc.DC, ISNULL(vc.BankAccountID, 0) BankAccountID, ISNULL(ba.BankAccountNo, '') BankAccountNo, ISNULL(ba.KeteranganBank, '') KeteranganBank, vc.TypeTrx, vc.SignedPerson, vc.PrintMode FROM GL_Voucher vc LEFT JOIN BankAccount ba ON vc.BankAccountID = ba.ID LEFT JOIN TransDoc td ON vc.TransDocID = td.ID LEFT JOIN GL_ChartOfAccount coa ON vc.ChartNo = coa.ChartNo AND vc.CompanyCode = coa.CompanyCode AND vc.PTID = coa.PTID ";
            strSQL += "WHERE vc.CompanyCode = '" + CompanyCode + "' AND vc.PTID = " + PTID + " ";
            if (strValue != string.Empty)
            {
                strSQL += "AND vc.VoucherCode LIKE '%" + strValue + "%' AND vc.VoucherName LIKE '%" + strValue + "%' ";
            }
            strSQL += "ORDER BY vc.VoucherCode ASC";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGL_Voucher = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_Voucher.Add(GenerateObjectAll(sqlDataReader));
                }
            }
            else
                arrGL_Voucher.Add(new GL_Voucher());

            return arrGL_Voucher;
        }

        public ArrayList RetrieveDistinctTypeTrx()
        {
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader dr = da.RetrieveDataByString("SELECT DISTINCT(TypeTrx) FROM GL_Voucher ORDER BY TypeTrx ASC");
            strError = da.Error;
            arrGL_Voucher = new ArrayList();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    GL_Voucher vc = new GL_Voucher();
                    vc.TypeTRX = dr[0].ToString();
                    arrGL_Voucher.Add(vc);
                }
            }
            else
            {
                arrGL_Voucher.Add(new GL_Voucher());
            }
            return arrGL_Voucher;
        }

        public ArrayList RetreiveBankAccount(int PTID)
        {
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader dr = da.RetrieveDataByString("SELECT ID, BankAccountNo, KeteranganBank FROM BankAccount WHERE PTID = " + PTID + " ORDER BY KeteranganBank ASC");
            strError = da.Error;
            arrGL_Voucher = new ArrayList();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    GL_Voucher vc = new GL_Voucher();
                    vc.BankAccountID = Convert.ToInt32(dr["ID"]);
                    vc.BankAccountNo = dr["BankAccountNo"].ToString();
                    vc.KeteranganBank = dr["KeteranganBank"].ToString();
                    arrGL_Voucher.Add(vc);
                }
            }
            else
            {
                arrGL_Voucher.Add(new GL_Voucher());
            }
            return arrGL_Voucher;
        }

        public GL_Voucher RetreiveVoucherByID(int ID)
        {
            string strSQL = "SELECT vc.ID, vc.VoucherCode, vc.VoucherName, ISNULL(vc.ChartNo, '') ChartNo, ISNULL(coa.ChartName, '') ChartName, vc.DC, ISNULL(vc.BankAccountID, 0) BankAccountID, ISNULL(ba.BankAccountNo, '') BankAccountNo, ISNULL(ba.KeteranganBank, '') KeteranganBank, vc.TypeTrx, vc.SignedPerson, vc.PrintMode FROM GL_Voucher vc LEFT JOIN BankAccount ba ON vc.BankAccountID = ba.ID LEFT JOIN TransDoc td ON vc.TransDocID = td.ID LEFT JOIN GL_ChartOfAccount coa ON vc.ChartNo = coa.ChartNo AND vc.CompanyCode = coa.CompanyCode AND vc.PTID = coa.PTID ";
            strSQL += "WHERE vc.ID = " + ID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGL_Voucher = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectAll(sqlDataReader);
                }
            }
            return new GL_Voucher();
        }

        public GL_Voucher GenerateObject(SqlDataReader sqlDataReader)
        {
            objGL_Voucher = new GL_Voucher();
            objGL_Voucher.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objGL_Voucher.VoucherCode = sqlDataReader["VoucherCode"].ToString();
            objGL_Voucher.VoucherName = sqlDataReader["VoucherName"].ToString();
            objGL_Voucher.SignedPerson = sqlDataReader["SignedPerson"].ToString();
            objGL_Voucher.PrintMode = Convert.ToInt32(sqlDataReader["PrintMode"]);
            objGL_Voucher.ChartNo = sqlDataReader["ChartNo"].ToString();
            objGL_Voucher.DC = sqlDataReader["DC"].ToString();

            return objGL_Voucher;
        }

        public GL_Voucher GenerateObjectAll(SqlDataReader dr)
        {
            objGL_Voucher = new GL_Voucher();
            objGL_Voucher.ID = Convert.ToInt32(dr["ID"]);
            objGL_Voucher.VoucherCode = dr["VoucherCode"].ToString();
            objGL_Voucher.VoucherName = dr["VoucherName"].ToString();
            objGL_Voucher.ChartNo = dr["ChartNo"].ToString();
            objGL_Voucher.ChartName = dr["ChartName"].ToString();
            objGL_Voucher.DC = dr["DC"].ToString();
            objGL_Voucher.BankAccountID = Convert.ToInt32(dr["BankAccountID"]);
            objGL_Voucher.BankAccountNo = dr["BankAccountNo"].ToString();
            objGL_Voucher.KeteranganBank = dr["KeteranganBank"].ToString();
            objGL_Voucher.TypeTRX = dr["TypeTrx"].ToString();
            objGL_Voucher.SignedPerson = dr["SignedPerson"].ToString();
            objGL_Voucher.PrintMode = Convert.ToInt32(dr["PrintMode"]);
            return objGL_Voucher;
        }
    }
}
