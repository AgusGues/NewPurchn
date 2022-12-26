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
    //class MasterJenisProductFacade
    //{
    //}
    public class MasterJenisProductFacade : AbstractFacade
    {
        private MasterJenisProduct objMasterJenisProduct = new MasterJenisProduct();
        private ArrayList arrMasterJenisProduct;
        private List<SqlParameter> sqlListParam;

        public MasterJenisProductFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objMasterJenisProduct = (MasterJenisProduct)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@JenisProductCode", objMasterJenisProduct.JenisProductCode));
                sqlListParam.Add(new SqlParameter("@JenisProductName", objMasterJenisProduct.JenisProductName));
                //sqlListParam.Add(new SqlParameter("@RowStatus", objMasterJenisProduct.DefectName));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objMasterJenisProduct.CreatedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objMasterJenisProduct.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertdef_JenisProductMaster");
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
                objMasterJenisProduct = (MasterJenisProduct)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objMasterJenisProduct.ID));
                sqlListParam.Add(new SqlParameter("@JenisProductCode", objMasterJenisProduct.JenisProductCode));
                sqlListParam.Add(new SqlParameter("@JenisPRoductName", objMasterJenisProduct.JenisProductName));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objMasterJenisProduct.CreatedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objMasterJenisProduct.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdatedef_JenisProductMaster");

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
                objMasterJenisProduct = (MasterJenisProduct)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objMasterJenisProduct.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objMasterJenisProduct.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeletedef_JenisProductMaster");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from def_JenisProductMaster where RowStatus > -1 order by JenisProductCode");
            strError = dataAccess.Error;
            arrMasterJenisProduct = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrMasterJenisProduct.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrMasterJenisProduct.Add(new MasterJenisProduct());

            return arrMasterJenisProduct;
        }

        public MasterJenisProduct RetrieveByCode(string strKode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from def_JenisProductMaster where JenisProductCode = '" + strKode + "' ");
            strError = dataAccess.Error;
            arrMasterJenisProduct = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new MasterJenisProduct();
        }

        public MasterJenisProduct RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from def_JenisProductMaster where ID = " + Id);
            strError = dataAccess.Error;
            arrMasterJenisProduct = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new MasterJenisProduct();
        }


        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from def_JenisProductMaster where RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrMasterJenisProduct = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrMasterJenisProduct.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrMasterJenisProduct.Add(new MasterJenisProduct());

            return arrMasterJenisProduct;
        }

        public MasterJenisProduct GenerateObject(SqlDataReader sqlDataReader)
        {
            objMasterJenisProduct = new MasterJenisProduct();
            objMasterJenisProduct.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objMasterJenisProduct.JenisProductCode = sqlDataReader["JenisPRoductCode"].ToString();
            objMasterJenisProduct.JenisProductName = sqlDataReader["JenisProductName"].ToString();
            objMasterJenisProduct.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objMasterJenisProduct.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objMasterJenisProduct.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objMasterJenisProduct.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objMasterJenisProduct.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objMasterJenisProduct;

        }


        public MasterJenisProduct GenerateObject2(SqlDataReader sqlDataReader)
        {
            objMasterJenisProduct = new MasterJenisProduct();
            objMasterJenisProduct.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objMasterJenisProduct.JenisProductCode = sqlDataReader["JenisProductCode"].ToString();
            objMasterJenisProduct.JenisProductName = sqlDataReader["JenisProductName"].ToString();
            objMasterJenisProduct.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objMasterJenisProduct.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            //objInventory.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objMasterJenisProduct.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            //objInventory.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);



            return objMasterJenisProduct;

        }

    }
    
}
