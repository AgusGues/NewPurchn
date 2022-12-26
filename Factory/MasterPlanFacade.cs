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
namespace DefectFacade
{
    //class MasterPlanFacade
    //{
    //}
    public class MasterPlanFacade : AbstractFacade
    {
        private MasterPlan objMasterPlan = new MasterPlan();
        private ArrayList arrMasterPlan;
        private List<SqlParameter> sqlListParam;

        public MasterPlanFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objMasterPlan = (MasterPlan)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@PlanCode", objMasterPlan.PlanCode));
                sqlListParam.Add(new SqlParameter("@PlanName", objMasterPlan.PlanName));
                //sqlListParam.Add(new SqlParameter("@RowStatus", objMasterDefect.DefectName));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objMasterPlan.CreatedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objMasterPlan.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertdef_MasterPlan");
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
                objMasterPlan = (MasterPlan)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objMasterPlan.ID));
                sqlListParam.Add(new SqlParameter("@PlanCode", objMasterPlan.PlanCode));
                sqlListParam.Add(new SqlParameter("@PlanName", objMasterPlan.PlanName));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objMasterPlan.CreatedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objMasterPlan.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdatedef_MasterPlan");

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
                objMasterPlan = (MasterPlan)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objMasterPlan.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objMasterPlan.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeletedef_MasterPlan");

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
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //gak pake DeptID
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,B.DeptName,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID from Inventory as A,Dept as B,UOM as C where A.DeptID = B.ID and A.UOMID = C.ID order by GroupID");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from def_PlanMaster where RowStatus > -1 order by PlanCode");
            strError = dataAccess.Error;
            arrMasterPlan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrMasterPlan.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrMasterPlan.Add(new MasterPlan());

            return arrMasterPlan;
        }

        public MasterPlan RetrieveByCode(string strKode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from def_PlanMaster where PlanCode = '" + strKode + "' ");
            strError = dataAccess.Error;
            arrMasterPlan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new MasterPlan();
        }

        public MasterPlan RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from def_PlanMaster where ID = " + Id);
            strError = dataAccess.Error;
            arrMasterPlan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new MasterPlan();
        }


        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from def_PlanMaster where RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrMasterPlan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrMasterPlan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrMasterPlan.Add(new MasterPlan());

            return arrMasterPlan;
        }

        public MasterPlan GenerateObject(SqlDataReader sqlDataReader)
        {
            objMasterPlan = new MasterPlan();
            objMasterPlan.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objMasterPlan.PlanCode = sqlDataReader["PlanCode"].ToString();
            objMasterPlan.PlanName = sqlDataReader["PlanName"].ToString();
            objMasterPlan.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objMasterPlan.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objMasterPlan.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objMasterPlan.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objMasterPlan.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objMasterPlan;

        }




        public MasterPlan GenerateObject2(SqlDataReader sqlDataReader)
        {
            objMasterPlan = new MasterPlan();
            objMasterPlan.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objMasterPlan.PlanCode = sqlDataReader["PlanCode"].ToString();
            objMasterPlan.PlanName = sqlDataReader["PlanName"].ToString();
            objMasterPlan.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objMasterPlan.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            //objInventory.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objMasterPlan.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            //objInventory.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);



            return objMasterPlan;

        }

    }


}
