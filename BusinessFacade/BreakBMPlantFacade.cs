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
    public class BreakBMPlantFacade : AbstractFacade
    {
        private BreakBMPlant objBreakBMPlant = new BreakBMPlant();
        private Dept objDept = new Dept();
        private ArrayList arrBreakBMPlant;
        private List<SqlParameter> sqlListParam;


        public BreakBMPlantFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objBreakBMPlant = (BreakBMPlant)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@PlanCode", objBreakBMPlant.PlanCode));
                sqlListParam.Add(new SqlParameter("@PlanName", objBreakBMPlant.PlanName));
                sqlListParam.Add(new SqlParameter("@DeptID", objBreakBMPlant.DeptID));
                sqlListParam.Add(new SqlParameter("@DeptName", objBreakBMPlant.DeptName));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objBreakBMPlant.CreatedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertMasterPlan");

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
                objBreakBMPlant = (BreakBMPlant)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objBreakBMPlant.ID));
                sqlListParam.Add(new SqlParameter("@PlanCode", objBreakBMPlant.PlanCode));
                sqlListParam.Add(new SqlParameter("@PlanName", objBreakBMPlant.PlanName));
                sqlListParam.Add(new SqlParameter("@DeptID", objBreakBMPlant.DeptID));
                sqlListParam.Add(new SqlParameter("@DeptName", objBreakBMPlant.DeptName));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objBreakBMPlant.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateMasterPlan");

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
                objBreakBMPlant = (BreakBMPlant)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objBreakBMPlant.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objBreakBMPlant.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteMasterPlan");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from MasterPlan where RowStatus > -1");
            strError = dataAccess.Error;
            arrBreakBMPlant = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBreakBMPlant.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrBreakBMPlant.Add(new BreakBMPlant());

            return arrBreakBMPlant;
        }

        public BreakBMPlant RetrieveById(int Id)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from MasterPlan where RowStatus = 0 and ID = " + Id);
            strError = dataAccess.Error;
            arrBreakBMPlant = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new BreakBMPlant();
        }

        public ArrayList RetrieveDept()
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from Dept where RowStatus > -1");
            strError = dataAccess.Error;
            arrBreakBMPlant = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBreakBMPlant.Add(GenerateObject3(sqlDataReader));
                }
            }
            else
                arrBreakBMPlant.Add(new Dept());

            return arrBreakBMPlant;
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from MasterPlan where RowStatus = 0 and " + strField + " = " + strValue + "");
            strError = dataAccess.Error;
            arrBreakBMPlant = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBreakBMPlant.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrBreakBMPlant.Add(new BreakBMPlant());

            return arrBreakBMPlant;
        }

        public BreakBMPlant GenerateObject(SqlDataReader sqlDataReader)
        {
            objBreakBMPlant = new BreakBMPlant();
            objBreakBMPlant.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objBreakBMPlant.PlanCode = sqlDataReader["PlanCode"].ToString();
            objBreakBMPlant.PlanName = sqlDataReader["PlanName"].ToString();
            objBreakBMPlant.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objBreakBMPlant.DeptName = sqlDataReader["DeptName"].ToString();
            objBreakBMPlant.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objBreakBMPlant.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objBreakBMPlant.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objBreakBMPlant.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objBreakBMPlant.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objBreakBMPlant;
        }

        public Dept GenerateObject3(SqlDataReader sqlDataReader)
        {
            objDept = new Dept();
            objDept.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objDept.DeptCode = sqlDataReader["DeptCode"].ToString();
            objDept.DeptName = sqlDataReader["DeptName"].ToString();
            objDept.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objDept.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objDept.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objDept.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objDept.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objDept;
        }



    }
}
