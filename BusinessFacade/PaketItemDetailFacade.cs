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
    public class PaketItemDetailFacade : AbstractTransactionFacade
    {
        private PaketItemDetail objPaketItemDetail = new PaketItemDetail();
        private ArrayList arrPaketItemDetail;
        private List<SqlParameter> sqlListParam;

        public PaketItemDetailFacade(object objDomain)
            : base(objDomain)
        {
            objPaketItemDetail = (PaketItemDetail)objDomain;
        }

        public PaketItemDetailFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@PaketItemID", objPaketItemDetail.PaketItemID));
                sqlListParam.Add(new SqlParameter("@ItemID", objPaketItemDetail.ItemID));
                sqlListParam.Add(new SqlParameter("@Quantity", objPaketItemDetail.Quantity));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objPaketItemDetail.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertPaketItemDetail");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Update(TransactionManager transManager)
        {
            int intResult = Delete(transManager);
            if(strError == string.Empty)
                intResult = Insert(transManager);

            return intResult;
        }

        public override int Delete(TransactionManager transManager)
        {
            try
            {
                objPaketItemDetail = (PaketItemDetail)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@PaketItemID", objPaketItemDetail.PaketItemID));

                int intResult = transManager.DoTransaction(sqlListParam, "spDeletePaketItemDetail");

                strError = transManager.Error;

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.Description,A.GroupID,B.GroupDescription,A.ShortKey,A.GradeID,C.GradeCode,A.SisiID,D.SisiDescription,A.Tebal,A.Panjang,A.Lebar,A.UOMID,E.UOMCode,A.Berat,A.Ket1,A.Ket2,A.Utuh,A.Paket,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from PaketItemDetail as A,Groups as B, Grades as C,Sisi as D,UOM as E where A.GroupID = B.ID and A.GradeID = C.ID and A.SisiID = D.ID and A.UOMID = E.ID and A.RowStatus = 0");
            strError = dataAccess.Error;
            arrPaketItemDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPaketItemDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrPaketItemDetail.Add(new PaketItemDetail());

            return arrPaketItemDetail;
        }


        public ArrayList RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.PaketItemID,A.ItemID,B.Description as ItemName,A.Quantity,A.CreatedBy,A.CreatedTime,A.FlagReport from PaketItemDetail as A,Items as B where A.PaketItemID = " + Id + " and A.ItemID = B.ID");
            strError = dataAccess.Error;
            arrPaketItemDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPaketItemDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrPaketItemDetail.Add(new PaketItemDetail());

            return arrPaketItemDetail;
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.Description,A.GroupID,B.GroupDescription,A.ShortKey,A.GradeID,C.GradeCode,A.SisiID,D.SisiDescription,A.Tebal,A.Panjang,A.Lebar,A.UOMID,E.UOMCode,A.Berat,A.Ket1,A.Ket2,A.Utuh,A.Paket,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from PaketItemDetail as A,Groups as B, Grades as C,Sisi as D,UOM as E where A.GroupID = B.ID and A.GradeID = C.ID and A.SisiID = D.ID and A.UOMID = E.ID and A.RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrPaketItemDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPaketItemDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrPaketItemDetail.Add(new PaketItemDetail());

            return arrPaketItemDetail;
        }

        public PaketItemDetail GenerateObject(SqlDataReader sqlDataReader)
        {
            objPaketItemDetail = new PaketItemDetail();
            objPaketItemDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objPaketItemDetail.PaketItemID = Convert.ToInt32(sqlDataReader["PaketItemID"]);
            objPaketItemDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objPaketItemDetail.ItemName = sqlDataReader["ItemName"].ToString();
            objPaketItemDetail.Quantity = Convert.ToInt32(sqlDataReader["Quantity"]);            
            objPaketItemDetail.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objPaketItemDetail.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            //flagreport
            objPaketItemDetail.FlagReport = Convert.ToInt32(sqlDataReader["FlagReport"]);

            return objPaketItemDetail;

        }
    }
}

