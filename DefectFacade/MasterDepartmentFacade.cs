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
   public class MasterDepartmentFacade : AbstractFacade
    {
        private MasterDefect objMasterDefect = new MasterDefect();
        private MasterDeptDefect objMasterDept = new MasterDeptDefect ();
        private ArrayList arrMasterDept;
        private List<SqlParameter> sqlListParam;

        public MasterDepartmentFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objMasterDept = (MasterDeptDefect)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@DeptCode", objMasterDept.DeptCode));
                sqlListParam.Add(new SqlParameter("@DeptName", objMasterDept.DeptName));
                //sqlListParam.Add(new SqlParameter("@RowStatus", objMasterDefect.DefectName));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objMasterDept.CreatedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objMasterDept.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertDef_MasterDept");
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
                objMasterDept = (MasterDeptDefect)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objMasterDept.ID));
                sqlListParam.Add(new SqlParameter("@DeptCode", objMasterDept.DeptCode));
                sqlListParam.Add(new SqlParameter("@DeptName", objMasterDept.DeptName));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objMasterDept.CreatedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objMasterDept.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateDef_MasterDept");

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
                objMasterDept = (MasterDeptDefect)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objMasterDept.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objMasterDept.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeletedef_DeptMaster");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from def_DeptMaster where RowStatus > -1 order by DefectCode");
            strError = dataAccess.Error;
            arrMasterDept = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrMasterDept.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrMasterDept.Add(new MasterDeptDefect());

            return arrMasterDept;
        }

        public MasterDeptDefect RetrieveByCode(string strKode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from def_DeptMaster where DefectCode = '" + strKode + "' " );
            strError = dataAccess.Error;
            arrMasterDept = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new MasterDeptDefect();
        }

        public MasterDeptDefect RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from def_DeptMaster where ID = " + Id  );
            strError = dataAccess.Error;
            arrMasterDept = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new MasterDeptDefect();
        }


        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from def_DeptMaster where RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrMasterDept = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrMasterDept.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrMasterDept.Add(new MasterDeptDefect());

            return arrMasterDept;
        }

        public MasterDeptDefect GenerateObject(SqlDataReader sqlDataReader)
        {
            objMasterDept = new MasterDeptDefect();
            objMasterDept.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objMasterDept.DeptCode = sqlDataReader["DeptCode"].ToString();
            objMasterDept.DeptName = sqlDataReader["DeptName"].ToString();
            objMasterDept.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objMasterDept.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objMasterDept.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objMasterDept.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objMasterDept.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objMasterDept;

        }


       
        public MasterDeptDefect GenerateObject2(SqlDataReader sqlDataReader)
        {
            objMasterDefect = new MasterDefect();
            objMasterDefect.ID = Convert.ToInt32(sqlDataReader["ID"]);
            //objMasterDefect.DefectCode = sqlDataReader["DeptCode"].ToString();
            //objMasterDefect.DefectName = sqlDataReader["DeptName"].ToString();
            objMasterDefect.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objMasterDefect.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            //objInventory.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objMasterDefect.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            //objInventory.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);



            return objMasterDept;

        }

    }

}
