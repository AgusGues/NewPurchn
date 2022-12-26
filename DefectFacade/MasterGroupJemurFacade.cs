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
 
    public class MasterGroupJemurFacade : AbstractFacade
    {
        
        private MasterGroupJemur objMasterGroupJemur = new MasterGroupJemur(); 
        private ArrayList arrMasterGroupJemur;
        private List<SqlParameter> sqlListParam;

        public MasterGroupJemurFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objMasterGroupJemur = (MasterGroupJemur)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@GroupJemurCode", objMasterGroupJemur.GroupJemurCode));
                sqlListParam.Add(new SqlParameter("@GroupJemurName", objMasterGroupJemur.GroupJemurName));
                //sqlListParam.Add(new SqlParameter("@RowStatus", objMasterGroupCutter.DefectName));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objMasterGroupJemur.CreatedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objMasterGroupJemur.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertdef_GroupJemurMaster");
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
                objMasterGroupJemur = (MasterGroupJemur)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objMasterGroupJemur.ID));
                sqlListParam.Add(new SqlParameter("@GroupJemurCode", objMasterGroupJemur.GroupJemurCode));
                sqlListParam.Add(new SqlParameter("@GroupJemurName", objMasterGroupJemur.GroupJemurName));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objMasterGroupJemur.CreatedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objMasterGroupJemur.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdatedef_GroupJemurMaster");

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
                objMasterGroupJemur = (MasterGroupJemur)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objMasterGroupJemur.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objMasterGroupJemur.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeletedef_GroupJemurMaster");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from Def_GroupJemur where RowStatus > -1 order by GroupJemurCode");
            strError = dataAccess.Error;
            arrMasterGroupJemur = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrMasterGroupJemur.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrMasterGroupJemur.Add(new MasterGroupJemur());

            return arrMasterGroupJemur;
        }

        public MasterGroupJemur RetrieveByCode(string strKode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from def_GroupJemur where GroupJemurCode = '" + strKode + "' ");
            strError = dataAccess.Error;
            arrMasterGroupJemur = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new MasterGroupJemur();
        }

        public MasterGroupJemur RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from def_GroupJemur where ID = " + Id);
            strError = dataAccess.Error;
            arrMasterGroupJemur = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new MasterGroupJemur();
        }


        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from def_GroupJemur where RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrMasterGroupJemur = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrMasterGroupJemur.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrMasterGroupJemur.Add(new MasterGroupJemur());

            return arrMasterGroupJemur;
        }

        public MasterGroupJemur GenerateObject(SqlDataReader sqlDataReader)
        {
            objMasterGroupJemur = new MasterGroupJemur();
            objMasterGroupJemur.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objMasterGroupJemur.GroupJemurCode = sqlDataReader["GroupJemurCode"].ToString();
            objMasterGroupJemur.GroupJemurName = sqlDataReader["GroupJemurName"].ToString();
            objMasterGroupJemur.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objMasterGroupJemur.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objMasterGroupJemur.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objMasterGroupJemur.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objMasterGroupJemur.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objMasterGroupJemur;

        }




        public MasterGroupJemur GenerateObject2(SqlDataReader sqlDataReader)
        {
            objMasterGroupJemur = new MasterGroupJemur();
            objMasterGroupJemur.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objMasterGroupJemur.GroupJemurCode = sqlDataReader["GroupJemurCode"].ToString();
            objMasterGroupJemur.GroupJemurName = sqlDataReader["GroupJemurName"].ToString();
            objMasterGroupJemur.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objMasterGroupJemur.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            //objInventory.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objMasterGroupJemur.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            //objInventory.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);



            return objMasterGroupJemur;

        }

    }
}

