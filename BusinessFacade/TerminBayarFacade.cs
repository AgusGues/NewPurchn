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
    public class TerminBayarFacade : AbstractFacade
    {
        private TerminBayar objTerminBayar = new TerminBayar();
        private ArrayList arrTerminBayar;
        private List<SqlParameter> sqlListParam;

        public TerminBayarFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objTerminBayar = (TerminBayar)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@POID", objTerminBayar.POID ));
                sqlListParam.Add(new SqlParameter("@DP", objTerminBayar.DP ));
                sqlListParam.Add(new SqlParameter("@TerminKe", objTerminBayar.TerminKe));
                sqlListParam.Add(new SqlParameter("@JmlTermin", objTerminBayar.JmlTermin));
                sqlListParam.Add(new SqlParameter("@TotalBayar", objTerminBayar.TotalBayar));
                sqlListParam.Add(new SqlParameter("@Persentase", objTerminBayar.Persentase));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertTerminBayar");

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
            throw new NotImplementedException();
        }

        public override int Delete(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override System.Collections.ArrayList Retrieve()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from terminbayar");
            strError = dataAccess.Error;
            arrTerminBayar = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTerminBayar.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTerminBayar.Add(new TerminBayar());

            return arrTerminBayar;
        }
        public TerminBayar GenerateObject(SqlDataReader sqlDataReader)
        {
            objTerminBayar = new TerminBayar();
            objTerminBayar.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objTerminBayar.DP = Convert.ToDecimal(sqlDataReader["DP"]);
            objTerminBayar.TerminKe = Convert.ToInt32(sqlDataReader["TerminKe"]);
            objTerminBayar.JmlTermin = Convert.ToInt32(sqlDataReader["JmlTermin"]);
            objTerminBayar.TotalBayar = Convert.ToDecimal(sqlDataReader["TotalBayar"]);
            objTerminBayar.POID = Convert.ToInt32(sqlDataReader["POID"]);
            return objTerminBayar;
        }
    }
}
