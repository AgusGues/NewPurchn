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
    public class SPPPrivateFacade : AbstractTransactionFacade
    {
        private SPPPrivate objSPPPrivate = new SPPPrivate();
        private ArrayList arrSPPPrivate;
        private List<SqlParameter> sqlListParam;

        public SPPPrivateFacade(object objDomain)
            : base(objDomain)
        {
            objSPPPrivate = (SPPPrivate)objDomain;
        }
        public SPPPrivateFacade()
        {
        }

        public override int Insert(DataAccessLayer.TransactionManager transManager)
        {
            try
            {
                objSPPPrivate = (SPPPrivate)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@SPPID", objSPPPrivate.SPPID));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertSPPPrivate");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Update(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Delete(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override System.Collections.ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }
    }
}
