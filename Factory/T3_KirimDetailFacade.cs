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
    public class T3_KirimDetailFacade : BusinessFacade.AbstractTransactionFacadeF
    {
        private T3_KirimDetail objT3_KirimDetail = new T3_KirimDetail();
        private ArrayList arrT3_KirimDetail;
        private List<SqlParameter> sqlListParam;

       
        public T3_KirimDetailFacade(object objDomain)
                    : base(objDomain)
                {
                    objT3_KirimDetail = (T3_KirimDetail)objDomain;
                }

        public T3_KirimDetailFacade()
                {
                }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                objT3_KirimDetail = (T3_KirimDetail)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@KirimID", objT3_KirimDetail.KirimID ));
                sqlListParam.Add(new SqlParameter("@SerahID", objT3_KirimDetail.SerahID));
                sqlListParam.Add(new SqlParameter("@LokID", objT3_KirimDetail.LokasiID));
                sqlListParam.Add(new SqlParameter("@ItemID", objT3_KirimDetail.ItemIDSer));
                sqlListParam.Add(new SqlParameter("@ItemIDSJ", objT3_KirimDetail.ItemIDSJ ));
                sqlListParam.Add(new SqlParameter("@TglTrans", objT3_KirimDetail.Tgltrans ));
                sqlListParam.Add(new SqlParameter("@Qty", objT3_KirimDetail.Qty));
                sqlListParam.Add(new SqlParameter("@HPP", objT3_KirimDetail.HPP ));
                sqlListParam.Add(new SqlParameter("@GroupID", objT3_KirimDetail.GroupID));
                sqlListParam.Add(new SqlParameter("@LoadingID", objT3_KirimDetail.T3siapKirimID  ));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT3_KirimDetail.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertT3_KirimDetail");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public override int Insert1(TransactionManager transManager)
        {
            try
            {
                objT3_KirimDetail = (T3_KirimDetail)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@KirimID", objT3_KirimDetail.KirimID));
                sqlListParam.Add(new SqlParameter("@SerahID", objT3_KirimDetail.SerahID));
                sqlListParam.Add(new SqlParameter("@LokID", objT3_KirimDetail.LokasiID));
                sqlListParam.Add(new SqlParameter("@ItemID", objT3_KirimDetail.ItemIDSer));
                sqlListParam.Add(new SqlParameter("@ItemIDSJ", objT3_KirimDetail.ItemIDSJ));
                sqlListParam.Add(new SqlParameter("@TglTrans", objT3_KirimDetail.Tgltrans));
                sqlListParam.Add(new SqlParameter("@Qty", objT3_KirimDetail.Qty));
                sqlListParam.Add(new SqlParameter("@HPP", objT3_KirimDetail.HPP));
                sqlListParam.Add(new SqlParameter("@GroupID", objT3_KirimDetail.GroupID));
                sqlListParam.Add(new SqlParameter("@LoadingID", objT3_KirimDetail.T3siapKirimID));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT3_KirimDetail.CreatedBy));
                sqlListParam.Add(new SqlParameter("@pengiriman", objT3_KirimDetail.Pengiriman));
                sqlListParam.Add(new SqlParameter("@jenispalet", objT3_KirimDetail.JenisPalet));
                sqlListParam.Add(new SqlParameter("@jmlpalet", objT3_KirimDetail.JmlPalet));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertT3_KirimDetail1");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public override int Update1(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }
        public override int Update2(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }
        public override int Update(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Delete(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }
        public override System.Collections.ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }
        public ArrayList RetrieveByTgl(string tgl)
        {
            string strSQL = "SELECT A.ID,A.SerahID,A.itemID,A.groupID,A.TglTrans, G.Groups AS groupdesc, I1.PartNo AS partnoKrm, "+
                "L2.Lokasi AS Lokasiser, A.Qty,isnull(A.HPP,0) as HPP, L1.Lokasi AS Lokasikrm, C.Customer, C.SJNo, C.OPNo, " +
                "isnull(A.Pengiriman,'')Pengiriman,isnull(A.JenisPalet,'')JenisPalet,isnull(A.JmlPalet,0)JmlPalet " +
                      "FROM T3_KirimDetail AS A left JOIN "+
                      "T3_Serah AS B ON A.SerahID = B.ID left JOIN "+
                      "FC_Lokasi AS L2 ON B.LokID = L2.ID left JOIN "+
                      "FC_Items AS I1 ON A.ItemID = I1.ID left JOIN "+
                      "FC_Lokasi AS L1 ON A.LokID = L1.ID left JOIN "+
                      "T3_GroupM AS G ON A.GroupID = G.ID left JOIN " +
                      "T3_Kirim AS C ON A.KirimID = C.ID where A.rowstatus>-1 and CONVERT(varchar, A.tgltrans, 112)='" + tgl + "' order by A.ID desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_KirimDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_KirimDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT3_KirimDetail.Add(new T3_KirimDetail());

            return arrT3_KirimDetail;
        }

        public ArrayList RetrieveBySJ(string sjno,string itemID,string qty)
        {
            string strSQL = "SELECT A.ID,A.SerahID,A.itemID,A.groupID, A.TglTrans, G.Groups AS groupdesc, I1.PartNo AS partnoKrm, " +
                "L2.Lokasi AS Lokasiser, A.Qty,isnull(A.HPP,0) as HPP, L1.Lokasi AS Lokasikrm, C.Customer, C.SJNo, C.OPNo, " +
                "isnull(A.Pengiriman,'')Pengiriman,isnull(A.JenisPalet,'')JenisPalet,isnull(A.JmlPalet,0)JmlPalet " +
                      "FROM T3_KirimDetail AS A left JOIN " +
                      "T3_Serah AS B ON A.SerahID = B.ID left JOIN " +
                      "FC_Lokasi AS L2 ON B.LokID = L2.ID left JOIN " +
                      "FC_Items AS I1 ON A.ItemID = I1.ID left JOIN " +
                      "FC_Lokasi AS L1 ON A.LokID = L1.ID left JOIN " +
                      "T3_GroupM AS G ON A.GroupID = G.ID left JOIN " +
                      "T3_Kirim AS C ON A.KirimID = C.ID where  A.rowstatus>-1 and C.sjno='" + sjno + "' and A.itemidSJ=" + itemID + " order by A.ID desc";
            //"T3_Kirim AS C ON A.KirimID = C.ID where C.sjno='" + sjno + "' and A.itemidSJ=" + itemID + " and A.qty=" + qty + " order by A.ID desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_KirimDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_KirimDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT3_KirimDetail.Add(new T3_KirimDetail());

            return arrT3_KirimDetail;
        }

        public int RetrieveBySJQty(string sjno, string itemID, string qty)
        {
            int Qty = 0;
            string strSQL = "select isnull(SUM(qty),0) as qty from T3_KirimDetail where ItemIDSJ=" + itemID +
                " and KirimID in(select ID from T3_Kirim where rowstatus>-1 and SJNo='" + sjno + "')";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_KirimDetail = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    Qty = Convert.ToInt32(sqlDataReader["qty"]);
                }
            }
            return Qty;
        }
        public int RetrieveBySJQtyNew(string sjno, string itemID)
        {
            int Qty = 0;
            string strSQL = "select isnull(SUM(qty),0) as qty from T3_KirimDetail where ItemIDSJ=" + itemID +
                " and KirimID in(select ID from T3_Kirim where rowstatus>-1 and SJNo='" + sjno + "')";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_KirimDetail = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    Qty = Convert.ToInt32(sqlDataReader["qty"]);
                }
            }
            return Qty;
        }
        public T3_KirimDetail GenerateObject(SqlDataReader sqlDataReader)
        {
            objT3_KirimDetail = new T3_KirimDetail();
            objT3_KirimDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objT3_KirimDetail.SerahID = Convert.ToInt32(sqlDataReader["serahID"]);
            objT3_KirimDetail.SJNO = (sqlDataReader["SJNO"]).ToString();
            objT3_KirimDetail.OPNO = (sqlDataReader["OPNO"]).ToString();
            objT3_KirimDetail.Customer = (sqlDataReader["Customer"]).ToString();
            objT3_KirimDetail.LokasiSer = (sqlDataReader["LokasiSer"]).ToString();
            objT3_KirimDetail.GroupDesc = (sqlDataReader["GroupDesc"]).ToString();
            objT3_KirimDetail.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objT3_KirimDetail.ItemIDSer = Convert.ToInt32(sqlDataReader["ItemID"]);
            objT3_KirimDetail.PartnoKrm = (sqlDataReader["PartnoKrm"]).ToString();
            objT3_KirimDetail.Qty = Convert.ToInt32(sqlDataReader["Qty"]);
            objT3_KirimDetail.HPP = Convert.ToDecimal(sqlDataReader["HPP"]);
            objT3_KirimDetail.LokasiKrm = (sqlDataReader["LokasiKrm"]).ToString();
            objT3_KirimDetail.PartnoKrm = (sqlDataReader["PartnoKrm"]).ToString();
            objT3_KirimDetail.Tgltrans = Convert.ToDateTime(sqlDataReader["TglTrans"]);
            objT3_KirimDetail.Pengiriman = (sqlDataReader["Pengiriman"]).ToString();
            objT3_KirimDetail.JenisPalet = (sqlDataReader["JenisPalet"]).ToString();
            objT3_KirimDetail.JmlPalet = Convert.ToInt32(sqlDataReader["JmlPalet"]);
            return objT3_KirimDetail;
        }




    }
}
