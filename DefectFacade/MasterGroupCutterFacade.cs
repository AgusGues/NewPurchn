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
   // class MasterGroupCutterFacade
    //{
    //}
    public class MasterGroupCutterFacade : AbstractFacade
    {
        private MasterGroupCutter objMasterGroupCutter = new MasterGroupCutter();
        private ArrayList arrMasterGroupCutter;
        private List<SqlParameter> sqlListParam;
       

        public MasterGroupCutterFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objMasterGroupCutter = (MasterGroupCutter)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@GroupCutCode", objMasterGroupCutter.GroupCutCode));
                sqlListParam.Add(new SqlParameter("@GroupCutName", objMasterGroupCutter.GroupCutName));
                //sqlListParam.Add(new SqlParameter("@RowStatus", objMasterGroupCutter.DefectName));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objMasterGroupCutter.CreatedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objMasterGroupCutter.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertdef_GroupCutterMaster");
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
                objMasterGroupCutter = (MasterGroupCutter)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objMasterGroupCutter.ID));
                sqlListParam.Add(new SqlParameter("@GroupCutCode", objMasterGroupCutter.GroupCutCode));
                sqlListParam.Add(new SqlParameter("@GroupCutName", objMasterGroupCutter.GroupCutName));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objMasterGroupCutter.CreatedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objMasterGroupCutter.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdatedef_GroupCutterMaster");

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
                objMasterGroupCutter = (MasterGroupCutter)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objMasterGroupCutter.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objMasterGroupCutter.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeletedef_GroupCutterMaster");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from Def_GroupCutter where RowStatus > -1 order by GroupCutCode");
            strError = dataAccess.Error;
            arrMasterGroupCutter = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrMasterGroupCutter.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrMasterGroupCutter.Add(new MasterGroupCutter());

            return arrMasterGroupCutter;
        }

        public MasterGroupCutter RetrieveByCode(string strKode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from def_GroupCutMaster where GroupCutCode = '" + strKode + "' ");
            strError = dataAccess.Error;
            arrMasterGroupCutter = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new MasterGroupCutter();
        }

        public MasterGroupCutter RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from def_GroupCutMaster where ID = " + Id);
            strError = dataAccess.Error;
            arrMasterGroupCutter = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new MasterGroupCutter();
        }


        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from def_GroupCutter where RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrMasterGroupCutter = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrMasterGroupCutter.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrMasterGroupCutter.Add(new MasterGroupCutter());

            return arrMasterGroupCutter;
        }

        public MasterGroupCutter GenerateObject(SqlDataReader sqlDataReader)
        {
            objMasterGroupCutter = new MasterGroupCutter();
            objMasterGroupCutter.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objMasterGroupCutter.GroupCutCode = sqlDataReader["GroupCutCode"].ToString();
            objMasterGroupCutter.GroupCutName = sqlDataReader["GroupCutName"].ToString();
            objMasterGroupCutter.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objMasterGroupCutter.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objMasterGroupCutter.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objMasterGroupCutter.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objMasterGroupCutter.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objMasterGroupCutter;

        }




        public MasterGroupCutter GenerateObject2(SqlDataReader sqlDataReader)
        {
            objMasterGroupCutter = new MasterGroupCutter();
            objMasterGroupCutter.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objMasterGroupCutter.GroupCutCode = sqlDataReader["GroupCutCode"].ToString();
            objMasterGroupCutter.GroupCutName = sqlDataReader["GroupCutName"].ToString();
            objMasterGroupCutter.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objMasterGroupCutter.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            //objInventory.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objMasterGroupCutter.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            //objInventory.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);



            return objMasterGroupCutter;

        }

    }
    
}
