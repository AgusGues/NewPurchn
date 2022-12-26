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
    public class MasterProdukFacade : AbstractFacade
    {
        private MasterProduk objMasterProduk = new MasterProduk();
        private ArrayList arrMasterProduk;
        private List<SqlParameter> sqlListParam;

        public MasterProdukFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objMasterProduk = (MasterProduk)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ProdukCode", objMasterProduk.ProdukCode));
                sqlListParam.Add(new SqlParameter("@ProdukName", objMasterProduk.ProdukName));
                //sqlListParam.Add(new SqlParameter("@RowStatus", objMasterDefect.DefectName));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objMasterProduk.CreatedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objMasterProduk.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertdef_MasterProduct");
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
                objMasterProduk = (MasterProduk)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objMasterProduk.ID));
                sqlListParam.Add(new SqlParameter("@ProdukCode", objMasterProduk.ProdukCode));
                sqlListParam.Add(new SqlParameter("@ProdukName", objMasterProduk.ProdukName));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objMasterProduk.CreatedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objMasterProduk.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdatedef_MasterProduk");

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
                objMasterProduk = (MasterProduk)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objMasterProduk.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objMasterProduk.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeletedef_MasterProduk");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from def_ProductMaster where RowStatus > -1 order by ProdukCode");
            strError = dataAccess.Error;
            arrMasterProduk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrMasterProduk.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrMasterProduk.Add(new MasterPlan());

            return arrMasterProduk;
        }

        public MasterProduk RetrieveByCode(string strKode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from def_ProductMaster where PlanCode = '" + strKode + "' ");
            strError = dataAccess.Error;
            arrMasterProduk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new MasterProduk();
        }

        public MasterProduk RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from def_PlanMaster where ID = " + Id);
            strError = dataAccess.Error;
            arrMasterProduk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new MasterProduk();
        }


        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from def_ProductMaster where RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrMasterProduk = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrMasterProduk.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrMasterProduk.Add(new MasterProduk());

            return arrMasterProduk;
        }

        public MasterProduk GenerateObject(SqlDataReader sqlDataReader)
        {
            objMasterProduk = new MasterProduk();
            objMasterProduk.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objMasterProduk.ProdukCode = sqlDataReader["ProdukCode"].ToString();
            objMasterProduk.ProdukName = sqlDataReader["ProdukName"].ToString();
            objMasterProduk.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objMasterProduk.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objMasterProduk.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objMasterProduk.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objMasterProduk.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objMasterProduk;

        }




        public MasterProduk GenerateObject2(SqlDataReader sqlDataReader)
        {
            objMasterProduk = new MasterProduk();
            objMasterProduk.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objMasterProduk.ProdukCode = sqlDataReader["ProdukCode"].ToString();
            objMasterProduk.ProdukName = sqlDataReader["ProdukName"].ToString();
            objMasterProduk.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objMasterProduk.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            //objInventory.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objMasterProduk.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            //objInventory.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);



            return objMasterProduk;

        }

    }

}
