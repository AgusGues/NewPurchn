using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;
using BusinessFacade;
using Dapper;

namespace Factory
{
    public class FormulaFacade : AbstractFacade
    {
        private Formula objFormula = new Formula();
        private ArrayList arrFormula;
        private List<SqlParameter> sqlListParam;

        public FormulaFacade()
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

        public override int Delete(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override System.Collections.ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }

        public ArrayList Retrieve1()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from bm_Formula order by formulacode ");
            strError = dataAccess.Error;
            arrFormula = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrFormula.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrFormula.Add(new Asset());

            return arrFormula;
        }
        public int GetID(string formula)
        {
            int ID = 0;
            string strSQL = "select * from bm_Formula where formulacode='" + formula + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrFormula = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    ID = Convert.ToInt32(sqlDataReader["ID"]);
                }
            }

            return ID;
        }
        public Formula GenerateObject(SqlDataReader sqlDataReader)
        {
            objFormula = new Formula();
            objFormula.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objFormula.FormulaCode = sqlDataReader["FormulaCode"].ToString();

            return objFormula;
        }



        public static List<Formula.BmFormula> GetListFormula()
        {
            List<Formula.BmFormula> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "SELECT * FROM BM_FORMULA ORDER BY FORMULACODE";
                    AllData = connection.Query<Formula.BmFormula>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }
    }
}
