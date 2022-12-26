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
 public class MasterReguFacade : AbstractFacade
    {
        private MasterRegu  objGrade = new MasterRegu();
        private MasterPlan  objPlan = new MasterPlan();
        private ArrayList arrGrade;
        private List<SqlParameter> sqlListParam;


        public MasterReguFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {            
            try
            {
                objGrade = (MasterRegu)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ReguCode", objGrade.ReguCode));
                sqlListParam.Add(new SqlParameter("@PlanID", objGrade.PlanID));
                sqlListParam.Add(new SqlParameter("@PlanName", objGrade.PlanName));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objGrade.CreatedBy));

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
                objGrade = (MasterRegu)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objGrade.ID));
                sqlListParam.Add(new SqlParameter("@ReguCode", objGrade.ReguCode));
                sqlListParam.Add(new SqlParameter("@PlanID", objGrade.PlanID));
                sqlListParam.Add(new SqlParameter("@PlaneName", objGrade.PlanName));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objGrade.LastModifiedBy));

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
                objGrade = (MasterRegu)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objGrade.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objGrade.LastModifiedBy));

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
            arrGrade = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGrade.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGrade.Add(new MasterRegu());

            return arrGrade;
        }

        public ArrayList RetrievePlanID()
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from MasterPlan where RowStatus > -1");
            strError = dataAccess.Error;
            arrGrade = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGrade.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrGrade.Add(new MasterPlan());

            return arrGrade;
        }

        public MasterRegu RetrieveById(int Id)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from Pel_MasterRegu where RowStatus = 0 and ID = " + Id);
            strError = dataAccess.Error;
            arrGrade = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new MasterRegu();
        }


        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from Pel_MasterRegu where RowStatus = 0 and " + strField + " = '" + strValue + "'" );
            strError = dataAccess.Error;
            arrGrade = new ArrayList();

           if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGrade.Add(GenerateObject(sqlDataReader));
               }
            }
            else
                arrGrade.Add(new MasterRegu());

          return arrGrade;
        }

        public MasterRegu GenerateObject(SqlDataReader sqlDataReader)
        {
            objGrade = new MasterRegu();
            objGrade.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objGrade.ReguCode = sqlDataReader["ReguCode"].ToString();
            objGrade.PlanID = Convert.ToInt32(sqlDataReader["PlanID"]);
            objGrade.PlanName = sqlDataReader["PlanName"].ToString();
            objGrade.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objGrade.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objGrade.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objGrade.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objGrade.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objGrade;
        }

        public MasterPlan GenerateObject2(SqlDataReader sqlDataReader)
        {
            objPlan = new MasterPlan();
            objPlan.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objPlan.PlanCode = sqlDataReader["PlanCode"].ToString();
            objPlan.PlanName = sqlDataReader["PlanName"].ToString();
            //objPlan.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            //objPlan.DeptName = sqlDataReader["DeptName"].ToString();
            objPlan.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objPlan.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objPlan.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objPlan.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objPlan.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objPlan;
        }

    }
}
