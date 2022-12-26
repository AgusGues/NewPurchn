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
    public class ReceiptDocNoFacade : AbstractTransactionFacade
    {
        private ReceiptDocNo objReceiptDocNo = new ReceiptDocNo();
        private ArrayList arrReceiptDocNo ;
        private List<SqlParameter> sqlListParam;

        public ReceiptDocNoFacade(object objDomain)
            : base(objDomain)
        {
            objReceiptDocNo = (ReceiptDocNo)objDomain;
        }
        public ReceiptDocNoFacade()
        {
        }
        public override int Insert(TransactionManager transManager)
        {
            try
            {
                objReceiptDocNo = (ReceiptDocNo)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ReceiptCode", objReceiptDocNo.ReceiptCode));
                sqlListParam.Add(new SqlParameter("@MonthPeriod", objReceiptDocNo.MonthPeriod));
                sqlListParam.Add(new SqlParameter("@YearPeriod", objReceiptDocNo.YearPeriod));
                sqlListParam.Add(new SqlParameter("@NoUrut", objReceiptDocNo.NoUrut));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertReceiptDocNo");
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
                objReceiptDocNo = (ReceiptDocNo)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objReceiptDocNo.ID));
                sqlListParam.Add(new SqlParameter("@NoUrut", objReceiptDocNo.NoUrut));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateReceiptDocNo");

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
            throw new NotImplementedException();
        }

        public override ArrayList Retrieve()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select ID,DC,BankAccountID,TransDocID,MonthPeriod,YearPeriod,NoUrut from TransDocNo");
            strError = dataAccess.Error;
            arrReceiptDocNo = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceiptDocNo .Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrReceiptDocNo .Add(new ReceiptDocNo());

            return arrReceiptDocNo ;
        }

        public ReceiptDocNo RetrieveByReceiptCode(int bln, int thn, string receiptCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ReceiptDocNo where ReceiptCode='"+receiptCode+"' and MonthPeriod="+bln+" and YearPeriod="+thn);
            strError = dataAccess.Error;
            arrReceiptDocNo = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new ReceiptDocNo();
        }

        public ReceiptDocNo GenerateObject(SqlDataReader sqlDataReader)
        {
            objReceiptDocNo = new ReceiptDocNo();
            objReceiptDocNo.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objReceiptDocNo.ReceiptCode = sqlDataReader["ReceiptCode"].ToString();
            objReceiptDocNo.MonthPeriod = Convert.ToInt32(sqlDataReader["MonthPeriod"]);
            objReceiptDocNo.YearPeriod = Convert.ToInt32(sqlDataReader["YearPeriod"]);
            objReceiptDocNo.NoUrut = Convert.ToInt32(sqlDataReader["NoUrut"]);
            return objReceiptDocNo;
        }

        
    }

}
