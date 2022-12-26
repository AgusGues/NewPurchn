using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using System.Web;
using BusinessFacade;
using DataAccessLayer;
using Dapper;

namespace BusinessFacade
{
    public class PoPurchnDtlNfFacade : AbstractTransactionFacade
    {
        private PoPurchnNf.ParamDtl objPOPurchnDetail = new PoPurchnNf.ParamDtl();
        private List<SqlParameter> sqlListParam;
        public PoPurchnDtlNfFacade(object objDomain)
            : base(objDomain)
        {
            objPOPurchnDetail = (PoPurchnNf.ParamDtl)objDomain;
        }
        public override int Delete(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@POID", objPOPurchnDetail.POID));
                sqlListParam.Add(new SqlParameter("@SPPID", objPOPurchnDetail.SPPID));
                sqlListParam.Add(new SqlParameter("@GroupID", objPOPurchnDetail.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemID", objPOPurchnDetail.ItemID));
                sqlListParam.Add(new SqlParameter("@Price", objPOPurchnDetail.Price));
                sqlListParam.Add(new SqlParameter("@Qty", objPOPurchnDetail.Qty));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objPOPurchnDetail.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@UOMID", objPOPurchnDetail.UOMID));
                sqlListParam.Add(new SqlParameter("@Status", objPOPurchnDetail.Status));
                sqlListParam.Add(new SqlParameter("@NoUrut", objPOPurchnDetail.NoUrut));
                sqlListParam.Add(new SqlParameter("@SPPDetailID", objPOPurchnDetail.SPPDetailID));
                sqlListParam.Add(new SqlParameter("@DocumentNo", objPOPurchnDetail.DocumentNo));
                sqlListParam.Add(new SqlParameter("@DlvDate", objPOPurchnDetail.DlvDate));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertPOPurchnDetail");
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
            throw new NotImplementedException();
        }

        public override int Update(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }
    }
}
