using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using BusinessFacade;
using DataAccessLayer;

namespace Factory
{
    public class T3_SJDetailFacade : AbstractFacade
    {

        private T3_SJDetail objT3_SJDetail = new T3_SJDetail();
        private ArrayList arrT3_SJDetail;
        private List<SqlParameter> sqlListParam;

        public T3_SJDetailFacade()
            : base()
        {

        }
        public override int Insert(object objDomain)
        {
            throw new NotImplementedException();
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

        public ArrayList RetrieveBySJNO(string SJ)
        {
            string strSQL = "SELECT SJ.ID AS SJID, SJD.ID AS SJDID, SJD.ItemID as ItemIDSJ, I.Description ItemName, I.Tebal, I.Panjang, I.Lebar, SJD.Qty, " +
                "case when SJ.ID >0 then(select isnull(sum(B.qty),0) from T3_Kirim A INNER JOIN T3_KirimDetail B ON A.id=B.KirimID WHERE SJNo='" + SJ +
                "'  and B.ItemIDSJ =SJD.ItemID) end QtyP FROM SuratJalan AS SJ INNER JOIN SuratJalanDetail AS SJD ON SJ.ID = SJD.SuratJalanID INNER JOIN  " +
                "Items AS I ON SJD.ItemID = I.ID where SJ.Status>-1 and SJ.SuratJalanNo ='"+ SJ +"'"; 
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_SJDetail = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_SJDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT3_SJDetail.Add(new T3_SJDetail());

            return arrT3_SJDetail;
        }

        public ArrayList RetrieveBySJTONO(string SJ)
        {
            string strSQL = "SELECT SJ.ID AS SJID, SJD.ID AS SJDID, SJD.ItemID as ItemIDSJ, I.Description ItemName, I.Tebal, I.Panjang, I.Lebar, SJD.Qty, " +
                "case when SJ.ID >0 then(select isnull(sum(B.qty),0) from T3_Kirim A INNER JOIN T3_KirimDetail B ON A.id=B.KirimID WHERE SJNo='" + SJ +
                "' and B.ItemIDSJ =SJD.ItemID) end QtyP FROM SuratJalanto AS SJ INNER JOIN SuratJalanDetailto AS SJD ON SJ.ID = SJD.SuratJalantoID INNER JOIN  " +
                "Items AS I ON SJD.ItemID = I.ID where SJ.Status>-1 and SJ.SuratJalanNo ='" + SJ + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_SJDetail = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_SJDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT3_SJDetail.Add(new T3_SJDetail());

            return arrT3_SJDetail;
        }

        public T3_SJDetail GenerateObject(SqlDataReader sqlDataReader)
        {
            objT3_SJDetail = new T3_SJDetail();
            objT3_SJDetail.SJID = Convert.ToInt32(sqlDataReader["SJID"]);
            objT3_SJDetail.SJDID = Convert.ToInt32(sqlDataReader["SJDID"]);
            objT3_SJDetail.ItemIDSJ = Convert.ToInt32(sqlDataReader["ItemIDSJ"]);
            objT3_SJDetail.ItemName = (sqlDataReader["ItemName"]).ToString();
            objT3_SJDetail.Tebal = Convert.ToInt32(sqlDataReader["Tebal"]);
            objT3_SJDetail.Panjang = Convert.ToInt32(sqlDataReader["Panjang"]);
            objT3_SJDetail.Lebar = Convert.ToInt32(sqlDataReader["Lebar"]);
            objT3_SJDetail.Qty  = Convert.ToInt32(sqlDataReader["Qty"]);
            objT3_SJDetail.QtyP = Convert.ToInt32(sqlDataReader["QtyP"]);
            return objT3_SJDetail;
        }
    }
}
