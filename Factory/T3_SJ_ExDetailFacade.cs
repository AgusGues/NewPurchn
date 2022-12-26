using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using BusinessFacade;
using DataAccessLayer;

namespace Factory
{
    public class T3_SJ_ExDetailFacade : AbstractFacade
    {
        private T3_SJ_ExDetail objT3_SJ_ExDetail = new T3_SJ_ExDetail();
        private ArrayList arrT3_SJ_ExDetail;
        private List<SqlParameter> sqlListParam;

        public T3_SJ_ExDetailFacade()
            : base()
        {

        }
        public override int Insert(object objDomain)
        {
            try
            {
                
                objT3_SJ_ExDetail = (T3_SJ_ExDetail)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@SJID", objT3_SJ_ExDetail.SJID));
                sqlListParam.Add(new SqlParameter("@ItemIDSJ", objT3_SJ_ExDetail.ItemIDSJ));
                sqlListParam.Add(new SqlParameter("@ItemName", objT3_SJ_ExDetail.ItemName));
                sqlListParam.Add(new SqlParameter("@Tebal", objT3_SJ_ExDetail.Tebal));
                sqlListParam.Add(new SqlParameter("@Panjang", objT3_SJ_ExDetail.Panjang));
                sqlListParam.Add(new SqlParameter("@Lebar", objT3_SJ_ExDetail.Lebar));
                sqlListParam.Add(new SqlParameter("@Palet", objT3_SJ_ExDetail.Palet));
                sqlListParam.Add(new SqlParameter("@Qty", objT3_SJ_ExDetail.Qty));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT3_SJ_ExDetail.CreatedBy));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertT3_SJ_ExDetail");
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
            throw new NotImplementedException();
        }

        public override int Delete(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override System.Collections.ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }

        public ArrayList RetrieveBySJ(string SJ)
        {
            string strSQL = "select *,0 as QtyP from t3_sj_exDetail where sjid in(select id from t3_sj_ex where suratjalanno='" + SJ + "')";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_SJ_ExDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_SJ_ExDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT3_SJ_ExDetail.Add(new T3_SJ_ExDetail());

            return arrT3_SJ_ExDetail;
        }

        public T3_SJ_ExDetail GenerateObject(SqlDataReader sqlDataReader)
        {
            //SJID, ItemIDSJ, ItemName, Tebal, Panjang, Lebar, Palet, Qty
            objT3_SJ_ExDetail = new T3_SJ_ExDetail();
            objT3_SJ_ExDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objT3_SJ_ExDetail.SJID = Convert.ToInt32(sqlDataReader["SJID"]);
            objT3_SJ_ExDetail.ItemIDSJ = Convert.ToInt32(sqlDataReader["ItemIDSJ"]);
            objT3_SJ_ExDetail.ItemName = sqlDataReader["ItemName"].ToString();
            objT3_SJ_ExDetail.Tebal = Convert.ToDecimal (sqlDataReader["Tebal"]);
            objT3_SJ_ExDetail.Panjang = Convert.ToInt32(sqlDataReader["Panjang"]);
            objT3_SJ_ExDetail.Lebar = Convert.ToInt32(sqlDataReader["Lebar"]);
            objT3_SJ_ExDetail.Palet = Convert.ToInt32(sqlDataReader["Palet"]);
            objT3_SJ_ExDetail.Qty = Convert.ToInt32(sqlDataReader["Qty"]);
            objT3_SJ_ExDetail.QtyP = Convert.ToInt32(sqlDataReader["QtyP"]);
            return objT3_SJ_ExDetail;
        }
    }
}
