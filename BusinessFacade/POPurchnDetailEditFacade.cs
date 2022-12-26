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

    public class POPurchnDetailEditFacade : AbstractFacade
    {
        //private ArrayList arrPOPurchnDetail;
        private List<SqlParameter> sqlListParam;

        public POPurchnDetailEditFacade()
            : base()
        {

        }
        public override int Insert(object objDomain)
        {
             throw new NotImplementedException();
        }

        public override int Update(object objDomain)
        {
            throw new NotImplementedException();
        }

        public  int UpdateDlv(int ID, DateTime DlvDate, string users)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", ID));
                sqlListParam.Add(new SqlParameter("@Users", users));
                sqlListParam.Add(new SqlParameter("@DlvDate", DlvDate));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdatePOPurchnDetail1");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateByReceipt(int ID)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", ID));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateStatusPOPurchnDetailByReceipt");
                strError = dataAccess.Error;
                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int CancelSPPDetail(int ID)
        {
            try
            {
                string Usere = ((Users)System.Web.HttpContext.Current.Session["Users"]).UserID.ToString();
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", ID));
                sqlListParam.Add(new SqlParameter("@AlasanPending", "UnApproved by :" + Usere));
                int intResult = dataAccess.ProcessData(sqlListParam, "spCancelSPPDetail2");
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
            throw new NotImplementedException();
        }

        public override int Delete(object objDomain)
        {
            throw new NotImplementedException();
        }
    }
}
