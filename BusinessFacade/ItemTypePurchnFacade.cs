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
    public class ItemTypePurchnFacade : AbstractFacade
    {
        private ItemTypePurchn objItemTypePurchn = new ItemTypePurchn();
        private ArrayList arrItemTypePurchn;
        private List<SqlParameter> sqlListParam;


        public ItemTypePurchnFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objItemTypePurchn = (ItemTypePurchn)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@TypeDescription", objItemTypePurchn.TypeDescription));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objItemTypePurchn.CreatedBy));                

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertItemTypePurchn");

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
                objItemTypePurchn = (ItemTypePurchn)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objItemTypePurchn.ID));
                sqlListParam.Add(new SqlParameter("@TypeDescription", objItemTypePurchn.TypeDescription));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objItemTypePurchn.CreatedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateItemType");

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
                objItemTypePurchn = (ItemTypePurchn)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objItemTypePurchn.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objItemTypePurchn.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteItemType");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select top 50 * from ItemTypePurchn where RowStatus in (0,1)");
            strError = dataAccess.Error;
            arrItemTypePurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrItemTypePurchn.Add(GenerateObject(sqlDataReader));
                }
            }
            

            return arrItemTypePurchn;
        }

        public ArrayList Retrieve2()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select top 50 * from ItemTypePurchn where RowStatus in (0)");
            strError = dataAccess.Error;
            arrItemTypePurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrItemTypePurchn.Add(GenerateObject(sqlDataReader));
                }
            }


            return arrItemTypePurchn;
        }

        public ArrayList RetrieveSatuan()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT * FROM UOM WHERE RowStatus>-1");
            strError = dataAccess.Error;
            arrItemTypePurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrItemTypePurchn.Add(GenerateObject2(sqlDataReader));
                }
            }


            return arrItemTypePurchn;
        }

        public ItemTypePurchn RetrieveByCode(string typeDescription)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from ItemTypePurchn where RowStatus = 0 and TypeDescription = '" + typeDescription + "'");
            strError = dataAccess.Error;
            arrItemTypePurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new ItemTypePurchn();
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from ItemTypePurchn where RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrItemTypePurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrItemTypePurchn.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrItemTypePurchn.Add(new ItemTypePurchn());

            return arrItemTypePurchn;
        }

        public ItemTypePurchn GenerateObject(SqlDataReader sqlDataReader)
        {
            objItemTypePurchn = new ItemTypePurchn();
            objItemTypePurchn.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objItemTypePurchn.TypeDescription = sqlDataReader["TypeDescription"].ToString();
            objItemTypePurchn.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objItemTypePurchn.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objItemTypePurchn.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objItemTypePurchn.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objItemTypePurchn.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objItemTypePurchn;

        }

        public ItemTypePurchn GenerateObject2(SqlDataReader sqlDataReader)
        {
            objItemTypePurchn = new ItemTypePurchn();
            objItemTypePurchn.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objItemTypePurchn.UOMCode = sqlDataReader["UOMCode"].ToString();
            objItemTypePurchn.UOMDesc = sqlDataReader["UOMDesc"].ToString();
            return objItemTypePurchn;

        }
    }
}
