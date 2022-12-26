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

    public class MasterUkuranFacade : AbstractFacade
    {

        private MasterUkuran objMasterUkuran = new MasterUkuran();
        private ArrayList arrMasterUkuran;
        private List<SqlParameter> sqlListParam;

        public MasterUkuranFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objMasterUkuran = (MasterUkuran)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Lebar", objMasterUkuran.Lebar));
                sqlListParam.Add(new SqlParameter("@panjang", objMasterUkuran.Panjang));
                sqlListParam.Add(new SqlParameter("@bagi", objMasterUkuran.Bagi));
                //sqlListParam.Add(new SqlParameter("@RowStatus", objMasterGroupCutter.DefectName));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objMasterUkuran.CreatedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objMasterUkuran.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertdef_MasterUkuran");
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
                objMasterUkuran = (MasterUkuran)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objMasterUkuran.ID));
                sqlListParam.Add(new SqlParameter("@Lebar", objMasterUkuran.Lebar));
                sqlListParam.Add(new SqlParameter("@panjang", objMasterUkuran.Panjang));
                sqlListParam.Add(new SqlParameter("@bagi", objMasterUkuran.Bagi));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objMasterUkuran.CreatedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objMasterUkuran.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdatedef_MasterUkuran");

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
                objMasterUkuran = (MasterUkuran)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objMasterUkuran.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objMasterUkuran.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeletedef_MasterUkuran");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from def_Ukuran where RowStatus > -1 order by Description");
            strError = dataAccess.Error;
            arrMasterUkuran = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrMasterUkuran.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrMasterUkuran.Add(new MasterUkuran ());

            return arrMasterUkuran;
        }

        //public MasterGroupJemur RetrieveByCode(string strKode)
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from def_MasterUkuran where GroupJemurCode = '" + strKode + "' ");
        //    strError = dataAccess.Error;
        //    arrMasterGroupJemur = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            return GenerateObject2(sqlDataReader);
        //        }
        //    }

        //    return new MasterGroupJemur();
        //}

        public MasterUkuran RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from def_Ukuran where ID = " + Id);
            strError = dataAccess.Error;
            arrMasterUkuran = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new MasterUkuran();
        }


        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from def_Ukuran where RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrMasterUkuran = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrMasterUkuran.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrMasterUkuran.Add(new MasterGroupJemur());

            return arrMasterUkuran;
        }

        public MasterUkuran GenerateObject(SqlDataReader sqlDataReader)
        {
            objMasterUkuran = new MasterUkuran();
            objMasterUkuran.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objMasterUkuran.Lebar = Convert.ToInt32(sqlDataReader["Lebar"]);
            objMasterUkuran.Panjang = Convert.ToInt32(sqlDataReader["Panjang"]);
            objMasterUkuran.Bagi = Convert.ToInt32(sqlDataReader["Bagi"]);
            objMasterUkuran.Description = sqlDataReader["Description"].ToString();
            objMasterUkuran.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objMasterUkuran.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objMasterUkuran.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objMasterUkuran.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objMasterUkuran.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objMasterUkuran;

        }




        public MasterUkuran GenerateObject2(SqlDataReader sqlDataReader)
        {
            objMasterUkuran = new MasterUkuran();
            objMasterUkuran.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objMasterUkuran.Lebar = Convert.ToInt32(sqlDataReader["Lebar"]);
            objMasterUkuran.Panjang = Convert.ToInt32(sqlDataReader["Panjang"]);
            objMasterUkuran.Bagi = Convert.ToInt32(sqlDataReader["Bagi"]);
            objMasterUkuran.Description = sqlDataReader["Description"].ToString();
            objMasterUkuran.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objMasterUkuran.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            //objInventory.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objMasterUkuran.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            //objInventory.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);



            return objMasterUkuran;

        }

    }
}
