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
    public class MasterDefectFacade : AbstractFacade
    {
        private MasterDefect objMasterDefect = new MasterDefect();
        private ArrayList arrMasterDefect;
        private List<SqlParameter> sqlListParam;

        public MasterDefectFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objMasterDefect = (MasterDefect)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@DefCode", objMasterDefect.DefCode));
                sqlListParam.Add(new SqlParameter("@DefName", objMasterDefect.DefName));
         //       sqlListParam.Add(new SqlParameter("@DeptID", objMasterDefect.DeptID));
                //sqlListParam.Add(new SqlParameter("@RowStatus", objMasterDefect.DefectName));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objMasterDefect.CreatedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objMasterDefect.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertDef_MasterDefect");
               //sqlListParam.Add(new SqlParameter("@DefectCode", objMasterDefect.DefectCode));
                //sqlListParam.Add(new SqlParameter("@DefectName", objMasterDefect.DefectName));
                ////sqlListParam.Add(new SqlParameter("@RowStatus", objMasterDefect.DefectName));
                //sqlListParam.Add(new SqlParameter("@CreatedBy", objMasterDefect.CreatedBy));
                //sqlListParam.Add(new SqlParameter("@LastModifiedBy", objMasterDefect.LastModifiedBy));

                //int intResult = dataAccess.ProcessData(sqlListParam, "spInsertdef_DefectMaster");
                //strError = dataAccess.Error;

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
                objMasterDefect = (MasterDefect)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objMasterDefect.ID));
                sqlListParam.Add(new SqlParameter("@DefCode", objMasterDefect.DefCode));
                sqlListParam.Add(new SqlParameter("@DefName", objMasterDefect.DefName));
           //     sqlListParam.Add(new SqlParameter("@DeptID", objMasterDefect.DeptID));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objMasterDefect.CreatedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objMasterDefect.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateDef_MasterDefect");

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
                objMasterDefect = (MasterDefect)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objMasterDefect.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objMasterDefect.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteDef_MasterDefect");

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
//            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from def_DefectMaster where RowStatus > -1 order by DefectCode");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from Def_MasterDefect where RowStatus > -1 order by urutan");
            strError = dataAccess.Error;
            arrMasterDefect = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrMasterDefect.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrMasterDefect.Add(new MasterDefect());

            return arrMasterDefect;
        }

        public MasterDefect RetrieveByCode(string strKode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from Def_MasterDefect where DefCode = '" + strKode + "' " );
            strError = dataAccess.Error;
            arrMasterDefect = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new MasterDefect();
        }

        public MasterDefect RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from Def_MasterDefect where ID = " + Id  );
            strError = dataAccess.Error;
            arrMasterDefect = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new MasterDefect();
        }

        public MasterDefect RetrieveByUrutan(int Urutan)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from Def_MasterDefect where rowstatus>-1 and Urutan = " + Urutan);
            strError = dataAccess.Error;
            arrMasterDefect = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new MasterDefect();
        }
        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from Def_MasterDefect where RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrMasterDefect = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrMasterDefect.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrMasterDefect.Add(new MasterDefect());

            return arrMasterDefect;
        }

        public MasterDefect GenerateObject(SqlDataReader sqlDataReader)
        {
            objMasterDefect = new MasterDefect();
            objMasterDefect.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objMasterDefect.DefCode = sqlDataReader["DefCode"].ToString();
            objMasterDefect.DefName = sqlDataReader["DefName"].ToString();
            objMasterDefect.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objMasterDefect.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objMasterDefect.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objMasterDefect.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objMasterDefect.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objMasterDefect.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objMasterDefect;

        }




        public MasterDefect GenerateObject2(SqlDataReader sqlDataReader)
        {
            objMasterDefect = new MasterDefect();
            objMasterDefect.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objMasterDefect.DefCode = sqlDataReader["DefCode"].ToString();
            objMasterDefect.Urutan = Convert.ToInt32(sqlDataReader["urutan"]);
            objMasterDefect.DefName = sqlDataReader["DefName"].ToString();
            objMasterDefect.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objMasterDefect.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objMasterDefect.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            //objInventory.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objMasterDefect.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            //objInventory.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);



            return objMasterDefect;

        }

        public ArrayList ViewGridMaster(int id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select ItemName from Def_MasterDefect");

            strError = dataAccess.Error;
            arrMasterDefect = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrMasterDefect.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrMasterDefect.Add(new MasterDefect());

            return arrMasterDefect;

        }



        
    }
    
}
    

