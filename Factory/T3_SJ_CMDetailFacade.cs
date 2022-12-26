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
    public class T3_SJ_CMDetailFacade : AbstractFacade
    {
         private T3_SJ_CMDetail objT3_SJ_CMDetail = new T3_SJ_CMDetail();
        private ArrayList arrT3_SJ_CMDetail;
        private List<SqlParameter> sqlListParam;

        public T3_SJ_CMDetailFacade()
            : base()
        {

        }
        public override int Insert(object objDomain)
        {
            try
            {
                
                objT3_SJ_CMDetail = (T3_SJ_CMDetail)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@SJID", objT3_SJ_CMDetail.SJID));
                sqlListParam.Add(new SqlParameter("@ItemIDSJ", objT3_SJ_CMDetail.ItemIDSJ));
                sqlListParam.Add(new SqlParameter("@ItemName", objT3_SJ_CMDetail.ItemName));
                sqlListParam.Add(new SqlParameter("@Tebal", objT3_SJ_CMDetail.Tebal));
                sqlListParam.Add(new SqlParameter("@Panjang", objT3_SJ_CMDetail.Panjang));
                sqlListParam.Add(new SqlParameter("@Lebar", objT3_SJ_CMDetail.Lebar));
                sqlListParam.Add(new SqlParameter("@Palet", objT3_SJ_CMDetail.Palet));
                sqlListParam.Add(new SqlParameter("@Qty", objT3_SJ_CMDetail.Qty));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT3_SJ_CMDetail.CreatedBy));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertT3_SJ_CMDetail");
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
            string strSQL = "select *,0 as QtyP from T3_SJ_CMDetail where rowstatus>-1 and sjid in(select id from T3_SJ_CM where suratjalanno='" + SJ + "')";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_SJ_CMDetail = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_SJ_CMDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT3_SJ_CMDetail.Add(new T3_SJ_CMDetail());
            return arrT3_SJ_CMDetail;
        }

        public T3_SJ_CMDetail GenerateObject(SqlDataReader sqlDataReader)
        {
            //SJID, ItemIDSJ, ItemName, Tebal, Panjang, Lebar, Palet, Qty
            objT3_SJ_CMDetail = new T3_SJ_CMDetail();
            objT3_SJ_CMDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objT3_SJ_CMDetail.SJID = Convert.ToInt32(sqlDataReader["SJID"]);
            objT3_SJ_CMDetail.ItemIDSJ = Convert.ToInt32(sqlDataReader["ItemIDSJ"]);
            objT3_SJ_CMDetail.ItemName = sqlDataReader["ItemName"].ToString();
            objT3_SJ_CMDetail.Tebal = Convert.ToDecimal (sqlDataReader["Tebal"]);
            objT3_SJ_CMDetail.Panjang = Convert.ToInt32(sqlDataReader["Panjang"]);
            objT3_SJ_CMDetail.Lebar = Convert.ToInt32(sqlDataReader["Lebar"]);
            objT3_SJ_CMDetail.Palet = Convert.ToInt32(sqlDataReader["Palet"]);
            objT3_SJ_CMDetail.Qty = Convert.ToInt32(sqlDataReader["Qty"]);
            objT3_SJ_CMDetail.QtyP = Convert.ToInt32(sqlDataReader["QtyP"]);
            return objT3_SJ_CMDetail;
        }
    }
}
