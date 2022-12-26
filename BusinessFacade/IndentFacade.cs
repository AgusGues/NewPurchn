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
    public class IndentFacade : AbstractFacade
    {
        private Indent objIndent = new Indent();
        private ArrayList arrIndent;
        private List<SqlParameter> sqlListParam;


        public IndentFacade()
            : base()
        {

        }

     
        public override ArrayList Retrieve()
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.Tenggang from Indent as A");
            strError = dataAccess.Error;
            arrIndent = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrIndent.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrIndent.Add(new Indent());

            return arrIndent;
        }

        public override int Insert(object objDomain)
        {
            try
            {
                objIndent = (Indent)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Tenggang", objIndent.Tenggang));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertIndent");

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
            try
            {
                objIndent = (Indent)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Tenggang", objIndent.Tenggang));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateTIndent");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Delete(object objDomain)
        {
            try
            {
                objIndent = (Indent)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objIndent.ID));


                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteIndent");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public Indent RetrieveById(int Id)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.Tenggang from Indent as A where A.ID = " + Id);
            strError = dataAccess.Error;
            arrIndent = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Indent();
        }

        public Indent GenerateObject(SqlDataReader sqlDataReader)
        {
            objIndent = new Indent();
            objIndent.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objIndent.Tenggang = sqlDataReader["Tenggang"].ToString();


            return objIndent;
        }
    }
}
