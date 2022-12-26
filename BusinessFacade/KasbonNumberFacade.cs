using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Web;
using Domain;
using DataAccessLayer;

namespace BusinessFacade
{
    public class KasbonNumberFacade : AbstractTransactionFacade
    {
        private Kasbon objKasbon = new Kasbon();
        private ArrayList arrKasbonNumber;
        private List<SqlParameter> sqlListParam;


        public KasbonNumberFacade(object objDomain)
            : base(objDomain)
        {
            objKasbon = (Kasbon)objDomain;
        }
        public KasbonNumberFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {

            return 0;
        }
        public override int Update(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@KasbonCounter", objKasbon.KasbonCounter));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objKasbon.CreatedBy));

                int IntResult = transManager.DoTransaction(sqlListParam, "KasbonNumberCounter");
                strError = transManager.Error;
                return IntResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        //public override int Update(object objDomain)
        //{
        //    try
        //    {
        //        sqlListParam = new List<SqlParameter>();
        //        sqlListParam.Add(new SqlParameter("@KasbonCounter", objKasbon.KasbonCounter));
        //        sqlListParam.Add(new SqlParameter("@LastModifiedBy", objKasbon.LastModifiedBy));

        //        int IntResult = dataAccess.ProcessData(sqlListParam, "KasbonNumberCounter");
        //        strError = dataAccess.Error;
        //        return IntResult;

        //    }
        //    catch (Exception ex)
        //    {
        //        strError = ex.Message;
        //        return -1;
        //    }
        //}
        public override int Delete(TransactionManager transManager)
        {
            return 0;
        }

        public override ArrayList Retrieve()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from SPPNumber");
            strError = dataAccess.Error;
            arrKasbonNumber = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    //arrKasbonNumber.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrKasbonNumber.Add(new SPPNumber());

            return arrKasbonNumber;
        }
    }

}
