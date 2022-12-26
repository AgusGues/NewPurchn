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
    public class T3_SerahFacade : BusinessFacade.AbstractTransactionFacadeF
    {
        private T3_Serah objT3_Serah = new T3_Serah();
        private ArrayList arrT3_Serah;
        private List<SqlParameter> sqlListParam;

        public T3_SerahFacade(object objDomain)
            : base(objDomain)
        {
            objT3_Serah = (T3_Serah)objDomain;
        }
        public T3_SerahFacade()
        {
        }
        public override int Insert1(TransactionManager transManager)
        {
            throw new NotImplementedException();
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
        public override int Insert(TransactionManager transManager)
        {
            try
            {
                objT3_Serah = (T3_Serah)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Flag", objT3_Serah.Flag));
                sqlListParam.Add(new SqlParameter("@LokID", objT3_Serah.LokID));
                sqlListParam.Add(new SqlParameter("@ItemID", objT3_Serah.ItemID));
                sqlListParam.Add(new SqlParameter("@GroupID", objT3_Serah.GroupID));
                sqlListParam.Add(new SqlParameter("@Qty", objT3_Serah.Qty));
                sqlListParam.Add(new SqlParameter("@HPP", objT3_Serah.HPP ));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT3_Serah.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertT3_Serah");
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
            throw new NotImplementedException();
        }
        
        public ArrayList RetrieveStock(string criteria)
        {
            if (criteria == string.Empty)
                criteria = " and A.lokid=0 ";
            string strSQL = "SELECT A.itemID,A.LokID,isnull(A.GroupID,0) as GroupID,C.itemdesc as partname,C.PartNo, C.Tebal, C.Lebar, C.Panjang,C.volume, B.Lokasi, A.Qty ,A.ID,isnull(A.HPP,0) as HPP " +
                "FROM  T3_Serah AS A INNER JOIN FC_Items AS C ON A.ItemID = C.ID INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID where A.Qty>0 " + criteria +
                " and A.LokID not in(select distinct lokid from t3_siapkirim) order by C.PartNo,B.Lokasi";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_Serah = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_Serah.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT3_Serah.Add(new T3_Serah());

            return arrT3_Serah;
        }
        
        public T3_Serah RetrieveStockByPartno(string partno, string lokasi)
        {
            string strSQL = "SELECT A.itemID,A.LokID,isnull(A.GroupID,0) as GroupID,C.itemdesc as partname,C.PartNo, C.Tebal, C.Lebar, C.Panjang,C.volume, B.Lokasi, A.Qty ,A.ID,isnull(A.HPP,0) as HPP " +
                "FROM  T3_Serah AS A INNER JOIN FC_Items AS C ON A.ItemID = C.ID INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID where " +
                " C.PartNo='" + partno + "' and   B.Lokasi='" + lokasi + "'order by C.PartNo,B.Lokasi";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new T3_Serah();
        }

        public T3_Serah RetrieveStockByLokIDnItemID(int LokID, int ItemID)
        {
            string strSQL = "SELECT A.itemID,A.LokID,isnull(A.GroupID,0) as GroupID,C.itemdesc as partname,C.PartNo, C.Tebal, C.Lebar, C.Panjang,C.volume, B.Lokasi, A.Qty ,A.ID,isnull(A.HPP,0) as HPP " +
                "FROM  T3_Serah AS A INNER JOIN FC_Items AS C ON A.ItemID = C.ID INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID where " +
                " A.itemID=" + ItemID + " and   A.LokID=" + LokID + "order by C.PartNo,B.Lokasi";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new T3_Serah();
        }
        public T3_Serah RetrieveByID(int ID)
        {
            string strSQL = "SELECT A.itemID,A.LokID,isnull(A.GroupID,0) as GroupID,C.itemdesc as partname,C.PartNo, C.Tebal, C.Lebar, C.Panjang,C.volume, "+
                "B.Lokasi as Lokasi,  Qty ,A.ID,isnull(A.HPP,0) as HPP " +
                "FROM  T3_Serah AS A INNER JOIN FC_Items AS C ON A.ItemID = C.ID INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID where A.ID=" + ID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new T3_Serah();
        }
        public int GetStock(int lokid,int itemID)
        {
            int stock = 0;
            string strSQL = "select isnull(sum(qty),0) as qty from t3_serah where itemid=" + itemID + " and lokid=" + lokid;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    stock = Convert.ToInt32(sqlDataReader["qty"]); ;
                }
            }
            return stock;
        }
        public ArrayList RetrieveFromLoading(string partno)
        {
            string kriteria=string.Empty;
            if (partno.Trim() == string.Empty)
                kriteria = " and I.Partno='-' ";
            else
                kriteria=" and I.Partno='" + partno + "'";
            string strSQL = "SELECT A.ID, A.GroupID,I.ItemDesc as PartName,A.LokID, A.ItemID, A.QtyIn-A.QtyOut as Qty, L.Lokasi, I.PartNo, I.Tebal, I.Lebar, I.Panjang,I.Volume,0 as HPP "+
                "FROM T3_SiapKirim AS A INNER JOIN FC_Lokasi AS L ON A.LokID = L.ID INNER JOIN FC_Items AS I ON A.ItemID = I.ID " +
                "WHERE (A.QtyIn - A.QtyOut > 0)" + kriteria + " order by A.QtyIn-A.QtyOut";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_Serah = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_Serah.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT3_Serah.Add(new T3_Serah());

            return arrT3_Serah;
        }

        public T3_Serah GenerateObject(SqlDataReader sqlDataReader)
        {
            objT3_Serah = new T3_Serah();
            objT3_Serah.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objT3_Serah.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objT3_Serah.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objT3_Serah.Partname = sqlDataReader["partname"].ToString();
            objT3_Serah.Partno = sqlDataReader["partno"].ToString();
            objT3_Serah.Tebal = Convert.ToDecimal(sqlDataReader["Tebal"]);
            objT3_Serah.Lebar = Convert.ToInt32(sqlDataReader["Lebar"]);
            objT3_Serah.Panjang = Convert.ToInt32(sqlDataReader["Panjang"]);
            objT3_Serah.Volume = Convert.ToDecimal(sqlDataReader["Volume"]);
            objT3_Serah.LokID = Convert.ToInt32(sqlDataReader["LokID"]);
            objT3_Serah.Lokasi = (sqlDataReader["Lokasi"]).ToString();
            objT3_Serah.Qty = Convert.ToInt32(sqlDataReader["Qty"]);
            objT3_Serah.HPP  = Convert.ToDecimal(sqlDataReader["HPP"]);
            return objT3_Serah;
        }


    }
}
