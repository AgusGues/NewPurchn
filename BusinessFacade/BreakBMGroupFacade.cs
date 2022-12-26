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
    public class BreakBMGroupFacade : AbstractFacade
    {
        private BreakBMGroup objBreakBMGroup = new BreakBMGroup();
        private BreakBMPlant objBreakBMPlant = new BreakBMPlant();
        private ArrayList arrBreakBMGroup;
        private List<SqlParameter> sqlListParam;

        public BreakBMGroupFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objBreakBMGroup = (BreakBMGroup)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ReguCode", objBreakBMGroup.ReguCode));
                sqlListParam.Add(new SqlParameter("@PlanID", objBreakBMGroup.PlanID));
                sqlListParam.Add(new SqlParameter("@PlanName", objBreakBMGroup.PlanName));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objBreakBMGroup.CreatedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertPel_MasterRegu");

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
                objBreakBMGroup = (BreakBMGroup)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objBreakBMGroup.ID));
                sqlListParam.Add(new SqlParameter("@ReguCode", objBreakBMGroup.ReguCode));
                sqlListParam.Add(new SqlParameter("@PlanID", objBreakBMGroup.PlanID));
                sqlListParam.Add(new SqlParameter("@PlaneName", objBreakBMGroup.PlanName));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objBreakBMGroup.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdatePel_MasterRegu");

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
                objBreakBMGroup = (BreakBMGroup)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objBreakBMGroup.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objBreakBMGroup.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeletePel_MasterRegu");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from Pel_MasterRegu where RowStatus > -1");
            strError = dataAccess.Error;
            arrBreakBMGroup = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBreakBMGroup.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrBreakBMGroup.Add(new BreakBMGroup());

            return arrBreakBMGroup;
        }

        public ArrayList RetrievePlanID()
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from MasterPlan where RowStatus > -1");
            strError = dataAccess.Error;
            arrBreakBMGroup = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBreakBMGroup.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrBreakBMGroup.Add(new BreakBMPlant());

            return arrBreakBMGroup;
        }

        public BreakBMGroup RetrieveById(int Id)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from Pel_MasterRegu where RowStatus = 0 and ID = " + Id);
            strError = dataAccess.Error;
            arrBreakBMGroup = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new BreakBMGroup();
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from Pel_MasterRegu where RowStatus = 0 and " + strField + " = '" + strValue + "'");
            strError = dataAccess.Error;
            arrBreakBMGroup = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBreakBMGroup.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrBreakBMGroup.Add(new BreakBMGroup());

            return arrBreakBMGroup;
        }

        public BreakBMGroup GenerateObject(SqlDataReader sqlDataReader)
        {
            objBreakBMGroup = new BreakBMGroup();
            objBreakBMGroup.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objBreakBMGroup.ReguCode = sqlDataReader["ReguCode"].ToString();
            objBreakBMGroup.PlanID = Convert.ToInt32(sqlDataReader["PlanID"]);
            objBreakBMGroup.PlanName = sqlDataReader["PlanName"].ToString();
            objBreakBMGroup.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objBreakBMGroup.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objBreakBMGroup.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objBreakBMGroup.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objBreakBMGroup.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objBreakBMGroup;
        }

        public BreakBMPlant GenerateObject2(SqlDataReader sqlDataReader)
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

    }
}
