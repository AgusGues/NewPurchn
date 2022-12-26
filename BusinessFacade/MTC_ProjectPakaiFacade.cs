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
    public class MTC_ProjectPakaiFacade : AbstractTransactionFacade
    {
        private MTC_ProjectPakai objSarmut = new MTC_ProjectPakai();
       // private ArrayList arrSarmut;
        private List<SqlParameter> sqlListParam;

        public MTC_ProjectPakaiFacade(object objDomain) : base(objDomain)
        {
            objSarmut = (MTC_ProjectPakai)objDomain;
        }

        public MTC_ProjectPakaiFacade()
        {

        }
        public override int Insert(DataAccessLayer.TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ProjectID", objSarmut.ProjectID));
                sqlListParam.Add(new SqlParameter("@ItemID", objSarmut.ItemID));
                sqlListParam.Add(new SqlParameter("@DeptID", objSarmut.DeptID));
                sqlListParam.Add(new SqlParameter("@Qty", objSarmut.Qty));
                sqlListParam.Add(new SqlParameter("@AvgPrice", objSarmut.AvgPrice));
                sqlListParam.Add(new SqlParameter("@GroupID", objSarmut.GroupID));
                sqlListParam.Add(new SqlParameter("@PakaiDetailID", objSarmut.PakaiID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objSarmut.ItemTypeID));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertMTCProjectPakai");
                strError = transManager.Error;

                return intResult;
            }
            catch (Exception e)
            {
                strError = e.Message;
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
