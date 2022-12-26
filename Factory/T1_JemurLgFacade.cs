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
using System.Web;

namespace Factory
{
    public class T1_JemurLgFacade : BusinessFacade.AbstractTransactionFacadeF
    {
        private T1_Jemur objT1_Jemur = new T1_Jemur();
        private t1jemur T1_Jemur = new t1jemur();
        private ArrayList arrT1_Jemur;
        private List<SqlParameter> sqlListParam;

        public T1_JemurLgFacade(object objDomain)
            : base(objDomain)
        {
            objT1_Jemur = (T1_Jemur)objDomain;
            //T1_Jemur = (t1jemur)objDomain;
        }
        public T1_JemurLgFacade()
        {
        }
       
        public override int Delete(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }
        
        public override int Insert(TransactionManager transManager)
        {
            try
            {
                objT1_Jemur = (T1_Jemur)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@DestID", objT1_Jemur.DestID ));
                sqlListParam.Add(new SqlParameter("@RakID", objT1_Jemur.RakID  ));
                sqlListParam.Add(new SqlParameter("@TglJemur", objT1_Jemur.TglJemur ));
                sqlListParam.Add(new SqlParameter("@QtyIn", objT1_Jemur.QtyIn ));
                sqlListParam.Add(new SqlParameter("@HPP", objT1_Jemur.HPP));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT1_Jemur.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertT1_Jemur");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public override int Update(TransactionManager transManager)//UpdateQtyJemur
        {
            try
            {
                objT1_Jemur = (T1_Jemur)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Flag", objT1_Jemur.Flag));
                sqlListParam.Add(new SqlParameter("@ID", objT1_Jemur.ID));
                sqlListParam.Add(new SqlParameter("@QtyOut", objT1_Jemur.QtyOut));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT1_Jemur.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateQtyT1_Jemur");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public override int Insert1(TransactionManager transManager) //insert jemurlg
        {
            int intResult = 0;
            try
            {
                objT1_Jemur = (T1_Jemur)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID0", objT1_Jemur.ItemID0));
                sqlListParam.Add(new SqlParameter("@lokid0", objT1_Jemur.LokasiID0));
                sqlListParam.Add(new SqlParameter("@lokid", objT1_Jemur.LokasiID));
                sqlListParam.Add(new SqlParameter("@JemurID", objT1_Jemur.ID));
                sqlListParam.Add(new SqlParameter("@DestID", objT1_Jemur.DestID));
                sqlListParam.Add(new SqlParameter("@ItemID", objT1_Jemur.ItemID));
                sqlListParam.Add(new SqlParameter("@RakID", objT1_Jemur.RakID));
                sqlListParam.Add(new SqlParameter("@TglJemur", objT1_Jemur.TglJemur));
                sqlListParam.Add(new SqlParameter("@QtyIn", objT1_Jemur.QtyIn));
                sqlListParam.Add(new SqlParameter("@HPP", objT1_Jemur.HPP));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT1_Jemur.CreatedBy));
                intResult = transManager.DoTransaction(sqlListParam, "spInsertT1_Jemurlg1");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Update1(TransactionManager transManager) //Update QtyJemurlg
        {
            try
            {
                objT1_Jemur = (T1_Jemur)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@lokid", objT1_Jemur.LokasiID));
                sqlListParam.Add(new SqlParameter("@ID", objT1_Jemur.ID));
                sqlListParam.Add(new SqlParameter("@ItemID", objT1_Jemur.ItemID));
                sqlListParam.Add(new SqlParameter("@QtyOut", objT1_Jemur.QtyOut ));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT1_Jemur.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateT1_Jemurlg");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        
        public override int Update2(TransactionManager transManager) //UpdateQtyJemurfromserah
        {
            try
            {
                objT1_Jemur = (T1_Jemur)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objT1_Jemur.ID));
                sqlListParam.Add(new SqlParameter("@QtyOut", objT1_Jemur.QtyOut));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT1_Jemur.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateQtyT1_Jemur");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        //public override int Update(object objDomain)
        //{
        //    throw new NotImplementedException();
        //}

        //public override int Delete(object objDomain)
        //{
        //    throw new NotImplementedException();
        //}

        public override ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }

        public int UpdateRak(int ID, int status, TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", ID));
                sqlListParam.Add(new SqlParameter("@Status", status));
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateFC_Rak");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int GetRakID(string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select id from FC_Rak where Rak='" + strValue + "'");
            strError = dataAccess.Error;
            int RakID = 0;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    RakID = Convert.ToInt32(sqlDataReader["id"]);
                }
            }
            else
                RakID = 0;

            return RakID;
        }

        public ArrayList RetrieveCuring(string tglProduksi, string criteria,string sorting )
        {
            string strSQL = "SELECT top 25 A.ID as DestID,D.PlantName, B.[Group], E.FormulaCode, F.NoPAlet, A.TglProduksi, A.Qty, A.Status,isnull(A.HPP,0) as HPP, C.Lokasi, G.PartNo " +
                      "FROM BM_Destacking AS A INNER JOIN " +
                      "BM_Plant AS D ON A.PlantID = D.ID INNER JOIN " +
                      "BM_PlantGroup AS B ON A.PlantGroupID = B.ID INNER JOIN " +
                      "BM_Formula AS E ON A.FormulaID = E.ID INNER JOIN " +
                      "FC_Lokasi AS C ON A.LokasiID = C.ID INNER JOIN " +
                      "BM_Palet AS F ON A.PaletID = F.ID INNER JOIN " +
                      "FC_Items AS G ON A.ItemID = G.ID where " + criteria +
                      " A.status=0 and A.Rowstatus>-1 and convert(varchar,A.TglProduksi,112)='" + tglProduksi + "' order by " + sorting;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Jemur = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_Jemur.Add(GenerateObjectForCuring(sqlDataReader));
                }
            }
            else
                arrT1_Jemur.Add(new T1_Jemur());

            return arrT1_Jemur;
        }

        public ArrayList RetrieveforJmrLg(string tglProduksi, string criteria,string sortby)
        {
            string strSQL = "SELECT top 15 0 AS Flag, A.ID AS DestID, A.ItemID, H.ID, H.ID AS JemurID, H.TglJemur, G.PartNo,G.Lebar * G.Panjang AS luasA, " +
                "CASE WHEN A.FormulaID > 0 THEN (SELECT formulacode FROM BM_Formula WHERE id = A.FormulaID) END AS FormulaCode,  " +
                "CASE WHEN A.PlantGroupID > 0 THEN (SELECT [GROUp] FROM BM_PlantGroup WHERE id = A.PlantGroupID) END AS gp,  " +
                "CASE WHEN A.PaletID > 0 THEN (SELECT NoPAlet FROM BM_Palet WHERE id = A.PaletID) END AS NoPAlet,  " +
                "CASE WHEN A.LokasiID > 0 THEN (SELECT Lokasi FROM FC_Lokasi WHERE id = A.LokasiID) END AS lokasi,  " +
                "CASE WHEN H.RakID > 0 THEN (SELECT Rak FROM FC_Rak WHERE id = H.RakID) END AS rak, A.TglProduksi, ISNULL(A.HPP, 0) AS HPP, " +
                "H.QtyIn -H.QtyOut as QtyIn, H.RakID, H.QtyOut , ' ' AS lokasiTF " +
                "FROM T1_Jemurlg AS H INNER JOIN BM_Destacking AS A ON H.DestID = A.ID INNER Join FC_Items AS G ON A.ItemID = G.ID " +
                "WHERE " + criteria + " convert(varchar,A.TglProduksi,112)= '" + tglProduksi + "' and H.QtyIn> H.QtyOut and H.status>-1 order by " + sortby;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Jemur = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_Jemur.Add(GenerateObjectforjemurlg(sqlDataReader));
                }
            }
            else
                arrT1_Jemur.Add(new T1_Jemur());

            return arrT1_Jemur;
        }

        public ArrayList RetrieveforSerah(string tglProduksi, string criteria,string sortby)
        {
            string strSQL = "SELECT top 15 0 AS Flag, A.ID AS DestID, A.ItemID, H.ID, H.ID AS JemurID, H.TglJemur, G.PartNo,G.Lebar * G.Panjang AS luasA, "+
                "CASE WHEN A.FormulaID > 0 THEN (SELECT formulacode FROM BM_Formula WHERE id = A.FormulaID) END AS FormulaCode,  "+
                "CASE WHEN A.PlantGroupID > 0 THEN (SELECT [GROUp] FROM BM_PlantGroup WHERE id = A.plantgroupID) END AS gp,  "+
                "CASE WHEN A.PaletID > 0 THEN (SELECT NoPAlet FROM BM_Palet WHERE id = A.PaletID) END AS NoPAlet,  "+
                "CASE WHEN A.LokasiID > 0 THEN (SELECT Lokasi FROM FC_Lokasi WHERE id = A.LokasiID) END AS lokasi,  "+
                "CASE WHEN H.RakID > 0 THEN (SELECT Rak FROM FC_Rak WHERE id = H.RakID) END AS rak, A.TglProduksi, ISNULL(A.HPP, 0) AS HPP, "+
                "H.QtyIn, H.RakID, H.QtyOut, H.HPP AS Expr2  " +
                "FROM T1_Jemur AS H INNER JOIN BM_Destacking AS A ON H.DestID = A.ID INNER Join FC_Items AS G ON A.ItemID = G.ID " +
                "WHERE " + criteria + " convert(varchar,A.TglProduksi,112) = '" + tglProduksi + "' and H.QtyIn> H.QtyOut  and H.rowstatus>-1 order by  " + sortby;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Jemur = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_Jemur.Add(GenerateObjectforserah(sqlDataReader));
                }
            }
            else
                arrT1_Jemur.Add(new T1_Jemur());

            return arrT1_Jemur;
        }
        public T1_Jemur  RetrieveJemur(string tglProduksi, string nopalet)
        {
            string strSQL = "select * from (SELECT  0 AS Flag, A.ID AS DestID, A.ItemID, H.ID, H.ID AS JemurID, H.TglJemur, G.PartNo,G.Lebar * G.Panjang AS luasA, "+
                "CASE WHEN A.FormulaID > 0 THEN (SELECT formulacode FROM BM_Formula WHERE id = A.FormulaID) END AS FormulaCode,   "+
                "CASE WHEN A.PlantGroupID > 0 THEN (SELECT [GROUp] FROM BM_PlantGroup WHERE id = A.plantgroupID) END AS gp,   "+
                "CASE WHEN A.PaletID > 0 THEN (SELECT NoPAlet FROM BM_Palet WHERE id = A.PaletID) END AS NoPAlet,   "+
                "CASE WHEN A.LokasiID > 0 THEN (SELECT Lokasi FROM FC_Lokasi WHERE id = A.LokasiID) END AS lokasi,   "+
                "CASE WHEN H.RakID > 0 THEN (SELECT Rak FROM FC_Rak WHERE id = H.RakID) END AS rak, A.TglProduksi, ISNULL(A.HPP, 0) AS HPP,  "+
                "H.QtyIn, H.RakID, H.QtyOut, H.HPP AS Expr2   "+
                "FROM T1_Jemur AS H INNER JOIN BM_Destacking AS A ON H.DestID = A.ID INNER Join FC_Items AS G ON A.ItemID = G.ID  " +
                "WHERE H.rowstatus>-1) as jemur where convert(varchar,TglProduksi,112) = '" + tglProduksi + "' and NoPAlet='" + nopalet + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectforserah(sqlDataReader);
                }
            }
            return new T1_Jemur() ;
        }
        public ArrayList RetrieveforSerah1(string tglProduksi, string criteria)
        {
            string strSQL = "select * from (SELECT  0 AS Flag,H.fail, A.ID AS DestID, A.ItemID, H.ID, H.ID AS JemurID, H.TglJemur, G.PartNo,G.Lebar * G.Panjang AS luasA, " +
                "CASE WHEN A.FormulaID > 0 THEN (SELECT formulacode FROM BM_Formula WHERE id = A.FormulaID) END AS FormulaCode,  " +
                "CASE WHEN A.PlantGroupID > 0 THEN (SELECT [GROUp] FROM BM_PlantGroup WHERE id = A.plantgroupID) END AS gp,  " +
                "CASE WHEN A.PaletID > 0 THEN (SELECT NoPAlet FROM BM_Palet WHERE id = A.PaletID) END AS NoPAlet,  " +
                "CASE WHEN A.LokasiID > 0 THEN (SELECT Lokasi FROM FC_Lokasi WHERE id = A.LokasiID) END AS lokasi,  " +
                "CASE WHEN H.RakID > 0 THEN (SELECT Rak FROM FC_Rak WHERE id = H.RakID) END AS rak, A.TglProduksi, ISNULL(A.HPP, 0) AS HPP, " +
                "H.QtyIn, H.RakID, H.QtyOut, H.HPP AS Expr2  " +
                "FROM T1_Jemur AS H INNER JOIN BM_Destacking AS A ON H.DestID = A.ID INNER Join FC_Items AS G ON A.ItemID = G.ID " +
                "WHERE convert(varchar,A.TglProduksi,112) = '" + tglProduksi + "' and H.QtyIn> H.QtyOut and  " + criteria + " H.rowstatus>-1)as jmr order by NoPAlet ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Jemur = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_Jemur.Add(GenerateObjectforserah(sqlDataReader));
                }
            }
            else
                arrT1_Jemur.Add(new T1_Jemur());

            return arrT1_Jemur;
        }
        public ArrayList RetrieveforPelarian(string tglProduksi, string criteria, string sortby)
        {
            string strSQL = "SELECT top 25 0 AS Flag,A.ID AS DestID, A.ItemID, H.ID, H.ID AS JemurID, H.TglJemur, G.PartNo, G.Lebar * G.Panjang AS luasA, " +
                "CASE WHEN A.FormulaID > 0 THEN (SELECT formulacode FROM  BM_Formula WHERE  id = A.FormulaID) END AS FormulaCode, "+
                "CASE WHEN A.PlantGroupID > 0 THEN (SELECT [GROUp] FROM  BM_PlantGroup WHERE  id = A.plantgroupID) END AS gp, "+
                "CASE WHEN A.PaletID > 0 THEN (SELECT NoPAlet FROM  BM_Palet WHERE  id = A.PaletID) END AS NoPAlet, "+
                "CASE WHEN H.LokID0 > 0 THEN (SELECT Lokasi FROM  FC_Lokasi WHERE  id = H.LokID0) END AS lokasi, "+
                "CASE WHEN H.RakID > 0 THEN (SELECT Rak FROM FC_Rak WHERE  id = H.RakID) END AS rak, A.TglProduksi, "+
                "ISNULL(A.HPP, 0) AS HPP, H.QtyIn, H.RakID, H.QtyOut, H.HPP AS HPP " +
                "FROM T1_Jemurlg AS H left JOIN BM_Destacking AS A ON H.DestID = A.ID left JOIN FC_Items AS G ON H.ItemID0 = G.ID " +
                "WHERE " + criteria + " convert(varchar,H.TglJemur,112) = '" + tglProduksi + 
                "' and H.QtyIn> H.QtyOut  and H.status>-1 order by  " + sortby;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Jemur = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_Jemur.Add(GenerateObjectforserah(sqlDataReader));
                }
            }
            else
                arrT1_Jemur.Add(new T1_Jemur());

            return arrT1_Jemur;
        }
        public int UpdateFail(int id)
        {
            string strSQL = "update t1_jemur set fail=1 where id=" + id;
            int intError = 0;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (strError != string.Empty)
                intError = 1;
            return intError;
        }

        public ArrayList RetrieveTransJmrLg(string TglJemur)
        {
            string strSQL = "SELECT TOP (25) A.ID AS DestID, H.ID, H.JemurID, E.FormulaCode, B.[Group] AS gp, G.PartNo, F.NoPAlet, C.Lokasi, A.TglProduksi, "+
                          "ISNULL(A.HPP, 0) AS HPP, H.TglJemur, H.QtyIn, H.QtyOut, H.RakID, I.Rak, J.Lokasi AS lokasiTF " +
                          "FROM FC_Lokasi AS J RIGHT OUTER JOIN " +
                          "T1_JemurLg AS H ON J.ID = H.LokID0 LEFT OUTER JOIN " +
                          "FC_Items AS G ON H.ItemID0 = G.ID LEFT OUTER JOIN " +
                          "FC_Rak AS I ON H.RakID = I.ID LEFT OUTER JOIN " +
                          "BM_Destacking AS A LEFT OUTER JOIN " +
                          "BM_Formula AS E ON A.FormulaID = E.ID LEFT OUTER JOIN " +
                          "BM_PlantGroup AS B ON A.PlantGroupID = B.ID LEFT OUTER JOIN " +
                          "FC_Lokasi AS C ON A.LokasiID = C.ID LEFT OUTER JOIN " +
                          "BM_Palet AS F ON A.PaletID = F.ID ON H.DestID = A.ID " +
                          "WHERE A.rowstatus>-1 and H.status>-1 and convert(varchar,H.TglJemur,112) = '" + TglJemur + "' order by H.id desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Jemur = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_Jemur.Add(GenerateObjectforjemurlg(sqlDataReader));
                }
            }
            else
                arrT1_Jemur.Add(new T1_Jemur());

            return arrT1_Jemur;
        }

        public ArrayList RetrieveByTglProduksi(string tglProduksi)
        {
            string strSQL = "SELECT A.ID as Destid,H.ID as ID,E.FormulaCode, B.[Group] as gp, G.PartNo, F.NoPAlet, C.Lokasi,A.TglProduksi,H.TglJemur, H.RakID,H.QtyIn,isnull(A.HPP,0) as HPP, I.Rak " +
                      "FROM BM_Destacking AS A INNER JOIN " +
                      "BM_Formula AS E ON A.FormulaID = E.ID INNER JOIN " +
                      "BM_PlantGroup AS B ON A.PlantGroupID = B.ID INNER JOIN " +
                      "FC_Lokasi AS C ON A.LokasiID = C.ID INNER JOIN " +
                      "BM_Palet AS F ON A.PaletID = F.ID INNER JOIN " +
                      "FC_Items AS G ON A.ItemID = G.ID INNER JOIN " +
                      "T1_Jemur as H ON A.ID = H.DestID INNER JOIN " +
                      "FC_Rak as I ON H.RakID = I.ID " +
                      "WHERE  A.rowstatus>-1 and convert(varchar,A.TglProduksi,112) = '" + tglProduksi + "' order by H.id desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Jemur = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_Jemur.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT1_Jemur.Add(new T1_Jemur());

            return arrT1_Jemur;
        }
        public int ValidasiRak(string rak, string tgl1, string tgl2)
        {
            int jumlah = 0;
            string strSQL = "select COUNT(*) as id from t1_jemur where CONVERT(varchar,tgljemur,112) >='" + tgl1 + "' and CONVERT(varchar,tgljemur,112) <='" + tgl2  +
                "' and RakID in(select ID from FC_Rak where Rak='" + rak + "')";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    jumlah = Convert.ToInt32(sqlDataReader["ID"]);
                }
            }
            else
                arrT1_Jemur.Add(new T1_Jemur());
            return jumlah;
        }

        public T1_Jemur GenerateObject(SqlDataReader sqlDataReader)
        {
            objT1_Jemur = new T1_Jemur();
            objT1_Jemur.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objT1_Jemur.DestID = Convert.ToInt32(sqlDataReader["DestID"]);
            objT1_Jemur.PlantGroup = (sqlDataReader["gp"]).ToString();
            objT1_Jemur.Formula = (sqlDataReader["FormulaCode"]).ToString();
            objT1_Jemur.TglProduksi = Convert.ToDateTime(sqlDataReader["TglProduksi"]);
            objT1_Jemur.TglJemur  = Convert.ToDateTime(sqlDataReader["TglJemur"]);
            objT1_Jemur.QtyIn = Convert.ToInt32(sqlDataReader["Qtyin"]);
            objT1_Jemur.RakID = Convert.ToInt32(sqlDataReader["RakID"]);
            objT1_Jemur.Rak = (sqlDataReader["Rak"]).ToString();
            objT1_Jemur.HPP = Convert.ToDecimal(sqlDataReader["HPP"]);
            objT1_Jemur.Partno = (sqlDataReader["PartNo"]).ToString();
            objT1_Jemur.Lokasi = (sqlDataReader["Lokasi"]).ToString();
            objT1_Jemur.Palet = (sqlDataReader["NoPAlet"]).ToString();
            return objT1_Jemur;
        }

        public T1_Jemur GenerateObjectForCuring(SqlDataReader sqlDataReader)
        {
            objT1_Jemur = new T1_Jemur();
            objT1_Jemur.DestID = Convert.ToInt32(sqlDataReader["DestID"]);
            objT1_Jemur.PlantGroup = (sqlDataReader["Group"]).ToString();
            objT1_Jemur.Formula = (sqlDataReader["FormulaCode"]).ToString();
            objT1_Jemur.Palet = (sqlDataReader["NoPAlet"]).ToString();
            objT1_Jemur.TglProduksi = Convert.ToDateTime(sqlDataReader["TglProduksi"]);
            objT1_Jemur.Lokasi = (sqlDataReader["Lokasi"]).ToString();
            objT1_Jemur.QtyIn = Convert.ToInt32(sqlDataReader["Qty"]);
            objT1_Jemur.HPP  = Convert.ToDecimal(sqlDataReader["HPP"]);
            objT1_Jemur.Partno = (sqlDataReader["PartNo"]).ToString();
            return objT1_Jemur;
        }

        public T1_Jemur GenerateObjectforjemurlg(SqlDataReader sqlDataReader)
        {try {
            objT1_Jemur = new T1_Jemur();
            objT1_Jemur.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objT1_Jemur.JemurID = Convert.ToInt32(sqlDataReader["JemurID"]);
            objT1_Jemur.DestID = Convert.ToInt32(sqlDataReader["DestID"]);
            objT1_Jemur.PlantGroup = (sqlDataReader["gp"]).ToString();
            objT1_Jemur.Formula = (sqlDataReader["FormulaCode"]).ToString();
            objT1_Jemur.TglProduksi = Convert.ToDateTime(sqlDataReader["TglProduksi"]);
            objT1_Jemur.TglJemur = Convert.ToDateTime(sqlDataReader["TglJemur"]);
            objT1_Jemur.QtyIn = Convert.ToInt32(sqlDataReader["Qtyin"]);
            objT1_Jemur.QtyOut = Convert.ToInt32(sqlDataReader["QtyOut"]);
            objT1_Jemur.HPP = Convert.ToDecimal(sqlDataReader["HPP"]);
            objT1_Jemur.RakID = Convert.ToInt32(sqlDataReader["RakID"]);
            objT1_Jemur.Rak = (sqlDataReader["Rak"]).ToString();
            objT1_Jemur.Partno = (sqlDataReader["PartNo"]).ToString();
            objT1_Jemur.Lokasi = (sqlDataReader["Lokasi"]).ToString();
            objT1_Jemur.LokasiTF = (sqlDataReader["LokasiTF"]).ToString();
            objT1_Jemur.Palet = (sqlDataReader["NoPAlet"]).ToString();
            }
            catch
            {
            }
            return objT1_Jemur;
        }
        public T1_Jemur GenerateObjectforserah(SqlDataReader sqlDataReader)
        {
            try
            {
                objT1_Jemur = new T1_Jemur();
                objT1_Jemur.ID = Convert.ToInt32(sqlDataReader["ID"]);
                objT1_Jemur.Flag = Convert.ToInt32(sqlDataReader["Flag"]);
                objT1_Jemur.JemurID = Convert.ToInt32(sqlDataReader["JemurID"]);
                objT1_Jemur.DestID = Convert.ToInt32(sqlDataReader["DestID"]);
                objT1_Jemur.PlantGroup = (sqlDataReader["gp"]).ToString();
                objT1_Jemur.Formula = (sqlDataReader["FormulaCode"]).ToString();
                objT1_Jemur.TglProduksi = Convert.ToDateTime(sqlDataReader["TglProduksi"]);
                objT1_Jemur.TglJemur = Convert.ToDateTime(sqlDataReader["TglJemur"]);
                objT1_Jemur.QtyIn = Convert.ToInt32(sqlDataReader["Qtyin"]);
                objT1_Jemur.QtyOut = Convert.ToInt32(sqlDataReader["QtyOut"]);
                objT1_Jemur.HPP = Convert.ToDecimal(sqlDataReader["HPP"]);
                objT1_Jemur.RakID = Convert.ToInt32(sqlDataReader["RakID"]);
                objT1_Jemur.Rak = (sqlDataReader["Rak"]).ToString();
                objT1_Jemur.Partno = (sqlDataReader["PartNo"]).ToString();
                objT1_Jemur.Lokasi = (sqlDataReader["Lokasi"]).ToString();
                objT1_Jemur.Palet = (sqlDataReader["NoPAlet"]).ToString();
                objT1_Jemur.ItemID = Convert.ToInt32(sqlDataReader["itemid"]);
                objT1_Jemur.LuasA = Convert.ToInt32(sqlDataReader["LuasA"]);
                objT1_Jemur.Fail = Convert.ToInt32(sqlDataReader["fail"]);
            }
            catch
            {
            }
            return objT1_Jemur;
        }
    }
}
