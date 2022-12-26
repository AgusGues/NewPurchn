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

namespace Factory
{
    public class T1_LR1_ListPlankFacade : BusinessFacade.AbstractTransactionFacadeF
    {
        private T1_LR1_ListPlank objT1_LR1_ListPlank = new T1_LR1_ListPlank();
        private ArrayList arrT1_LR1_ListPlank;
        private List<SqlParameter> sqlListParam;

        public T1_LR1_ListPlankFacade(object objDomain)
            : base(objDomain)
        {
            objT1_LR1_ListPlank = (T1_LR1_ListPlank)objDomain;
        }
        public T1_LR1_ListPlankFacade()
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
                objT1_LR1_ListPlank = (T1_LR1_ListPlank)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@L1ID", objT1_LR1_ListPlank.L1ID));
                sqlListParam.Add(new SqlParameter("@DestID", objT1_LR1_ListPlank.DestID ));
                sqlListParam.Add(new SqlParameter("@ItemID0", objT1_LR1_ListPlank.ItemID0));
                sqlListParam.Add(new SqlParameter("@lokid0", objT1_LR1_ListPlank.LokasiID0));
                sqlListParam.Add(new SqlParameter("@ItemID", objT1_LR1_ListPlank.ItemID));
                sqlListParam.Add(new SqlParameter("@lokid", objT1_LR1_ListPlank.LokasiID));
                sqlListParam.Add(new SqlParameter("@JemurID", objT1_LR1_ListPlank.JemurID));
                sqlListParam.Add(new SqlParameter("@RakID", objT1_LR1_ListPlank.RakID  ));
                sqlListParam.Add(new SqlParameter("@TglTrans", objT1_LR1_ListPlank.TglTrans ));
                sqlListParam.Add(new SqlParameter("@QtyIn", objT1_LR1_ListPlank.QtyIn ));
                sqlListParam.Add(new SqlParameter("@QtyOut", objT1_LR1_ListPlank.QtyOut));
                sqlListParam.Add(new SqlParameter("@HPP", objT1_LR1_ListPlank.HPP ));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT1_LR1_ListPlank.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertT1_LR1_ListPlank");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public override int Update(TransactionManager transManager) //UpdateQtyJemur
        {
            try
            {
                objT1_LR1_ListPlank = (T1_LR1_ListPlank)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Flag", objT1_LR1_ListPlank.Flag));
                sqlListParam.Add(new SqlParameter("@ID", objT1_LR1_ListPlank.ID));
                sqlListParam.Add(new SqlParameter("@QtyOut", objT1_LR1_ListPlank.QtyOut));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT1_LR1_ListPlank.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateQtyT1_LR1_ListPlank");
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
            int intResult=0;
            try
            {
                objT1_LR1_ListPlank = (T1_LR1_ListPlank)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID0", objT1_LR1_ListPlank.ItemID0 ));
	            sqlListParam.Add(new SqlParameter("@lokid0", objT1_LR1_ListPlank.LokasiID0 ));
                sqlListParam.Add(new SqlParameter("@lokid", objT1_LR1_ListPlank.LokasiID ));
                sqlListParam.Add(new SqlParameter("@L1ID", objT1_LR1_ListPlank.L1ID));
                sqlListParam.Add(new SqlParameter("@JemurID", objT1_LR1_ListPlank.ID));
                sqlListParam.Add(new SqlParameter("@DestID", objT1_LR1_ListPlank.DestID));
                sqlListParam.Add(new SqlParameter("@ItemID", objT1_LR1_ListPlank.ItemID));
                sqlListParam.Add(new SqlParameter("@RakID", objT1_LR1_ListPlank.RakID));
                sqlListParam.Add(new SqlParameter("@TglTrans", objT1_LR1_ListPlank.TglTrans));
                sqlListParam.Add(new SqlParameter("@QtyIn", objT1_LR1_ListPlank.QtyIn ));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT1_LR1_ListPlank.CreatedBy));
                intResult = transManager.DoTransaction(sqlListParam, "spInsertT1_LR1_ListPlank");
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
                objT1_LR1_ListPlank = (T1_LR1_ListPlank)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@lokid", objT1_LR1_ListPlank.LokasiID));
                sqlListParam.Add(new SqlParameter("@ID", objT1_LR1_ListPlank.ID));
                sqlListParam.Add(new SqlParameter("@ItemID", objT1_LR1_ListPlank.ItemID));
                sqlListParam.Add(new SqlParameter("@QtyOut", objT1_LR1_ListPlank.QtyOut ));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT1_LR1_ListPlank.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateT1_LR1_ListPlank");
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
                objT1_LR1_ListPlank = (T1_LR1_ListPlank)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objT1_LR1_ListPlank.ID));
                sqlListParam.Add(new SqlParameter("@QtyOut", objT1_LR1_ListPlank.QtyOut));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT1_LR1_ListPlank.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateQtyT1_LR1_ListPlank");
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
                      "FROM BM_Destacking AS A left JOIN " +
                      "BM_Plant AS D ON A.PlantID = D.ID left JOIN " +
                      "BM_PlantGroup AS B ON A.PlantGroupID = B.ID left JOIN " +
                      "BM_Formula AS E ON A.FormulaID = E.ID left JOIN " +
                      "FC_Lokasi AS C ON A.LokasiID = C.ID left JOIN " +
                      "BM_Palet AS F ON A.PaletID = F.ID left JOIN " +
                      "FC_Items AS G ON A.ItemID = G.ID where " + criteria +
                      " A.status=0 and A.Rowstatus>-1 and convert(varchar,A.TglProduksi,112)='" + tglProduksi + "' order by " + sorting;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_LR1_ListPlank = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_LR1_ListPlank.Add(GenerateObjectForCuring(sqlDataReader));
                }
            }
            else
                arrT1_LR1_ListPlank.Add(new T1_LR1_ListPlank());

            return arrT1_LR1_ListPlank;
        }
        public ArrayList RetrieveforJmrLg(string tglProduksi, string criteria,string sortby)
        {
            string strSQL = "SELECT top 15 0 AS Flag, A.ID AS DestID, A.ItemID, H.ID, H.ID AS JemurID, H.TglTrans, G.PartNo,G.Lebar * G.Panjang AS luasA, " +
                "CASE WHEN A.FormulaID > 0 THEN (SELECT formulacode FROM BM_Formula WHERE id = A.FormulaID) END AS FormulaCode,  " +
                "CASE WHEN A.PlantGroupID > 0 THEN (SELECT [GROUp] FROM BM_PlantGroup WHERE id = A.PlantGroupID) END AS gp,  " +
                "CASE WHEN A.PaletID > 0 THEN (SELECT NoPAlet FROM BM_Palet WHERE id = A.PaletID) END AS NoPAlet,  " +
                "CASE WHEN A.LokasiID > 0 THEN (SELECT Lokasi FROM FC_Lokasi WHERE id = A.LokasiID) END AS lokasi,  " +
                "CASE WHEN H.RakID > 0 THEN (SELECT Rak FROM FC_Rak WHERE id = H.RakID) END AS rak, A.TglProduksi, ISNULL(A.HPP, 0) AS HPP, " +
                "H.QtyIn -H.QtyOut as QtyIn, H.RakID, H.QtyOut , ' ' AS lokasiTF " +
                "FROM T1_LR1_ListPlank AS H INNER JOIN BM_Destacking AS A ON H.DestID = A.ID INNER Join FC_Items AS G ON A.ItemID = G.ID " +
                "WHERE " + criteria + " convert(varchar,A.TglProduksi,112)= '" + tglProduksi + "' and H.QtyIn> H.QtyOut and H.rowstatus>-1 order by " + sortby;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_LR1_ListPlank = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_LR1_ListPlank.Add(GenerateObjectforjemurlg(sqlDataReader));
                }
            }
            else
                arrT1_LR1_ListPlank.Add(new T1_LR1_ListPlank());

            return arrT1_LR1_ListPlank;
        }
        public ArrayList RetrieveforSerah(string tglProduksi, string criteria,string sortby)
        {
            string strSQL = "SELECT top 15 0 AS Flag, A.ID AS DestID, A.ItemID, H.ID, H.ID AS JemurID, H.TglTrans, G.PartNo,G.Lebar * G.Panjang AS luasA, "+
                "CASE WHEN A.FormulaID > 0 THEN (SELECT formulacode FROM BM_Formula WHERE id = A.FormulaID) END AS FormulaCode,  "+
                "CASE WHEN A.PlantGroupID > 0 THEN (SELECT [GROUp] FROM BM_PlantGroup WHERE id = A.plantgroupID) END AS gp,  "+
                "CASE WHEN A.PaletID > 0 THEN (SELECT NoPAlet FROM BM_Palet WHERE id = A.PaletID) END AS NoPAlet,  "+
                "CASE WHEN A.LokasiID > 0 THEN (SELECT Lokasi FROM FC_Lokasi WHERE id = A.LokasiID) END AS lokasi,  "+
                "CASE WHEN H.RakID > 0 THEN (SELECT Rak FROM FC_Rak WHERE id = H.RakID) END AS rak, A.TglProduksi, ISNULL(A.HPP, 0) AS HPP, "+
                "H.QtyIn, H.RakID, H.QtyOut, H.HPP AS Expr2,H.QtyIn-H.QtyOut as Sisa   " +
                "FROM T1_LR1_ListPlank AS H INNER JOIN BM_Destacking AS A ON H.DestID = A.ID INNER Join FC_Items AS G ON A.ItemID = G.ID " +
                "WHERE " + criteria + " convert(varchar,A.TglProduksi,112) = '" + tglProduksi + "' and H.QtyIn> H.QtyOut  and H.rowstatus>-1 order by  " + sortby;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_LR1_ListPlank = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_LR1_ListPlank.Add(GenerateObjectforserah(sqlDataReader));
                }
            }
            else
                arrT1_LR1_ListPlank.Add(new T1_LR1_ListPlank());

            return arrT1_LR1_ListPlank;
        }
        public T1_LR1_ListPlank  RetrieveJemur(string tglProduksi, string nopalet)
        {
            string strSQL = "select * from (SELECT  0 AS Flag, A.ID AS DestID, A.ItemID, H.ID, H.ID AS JemurID, H.TglTrans, G.PartNo,G.Lebar * G.Panjang AS luasA, "+
                "CASE WHEN A.FormulaID > 0 THEN (SELECT formulacode FROM BM_Formula WHERE id = A.FormulaID) END AS FormulaCode,   "+
                "CASE WHEN A.PlantGroupID > 0 THEN (SELECT [GROUp] FROM BM_PlantGroup WHERE id = A.plantgroupID) END AS gp,   "+
                "CASE WHEN A.PaletID > 0 THEN (SELECT NoPAlet FROM BM_Palet WHERE id = A.PaletID) END AS NoPAlet,   "+
                "CASE WHEN A.LokasiID > 0 THEN (SELECT Lokasi FROM FC_Lokasi WHERE id = A.LokasiID) END AS lokasi,   "+
                "CASE WHEN H.RakID > 0 THEN (SELECT Rak FROM FC_Rak WHERE id = H.RakID) END AS rak, A.TglProduksi, ISNULL(A.HPP, 0) AS HPP,  "+
                "H.QtyIn, H.RakID, H.QtyOut, H.HPP AS Expr2,H.QtyIn-H.QtyOut as Sisa    " +
                "FROM T1_LR1_ListPlank AS H INNER JOIN BM_Destacking AS A ON H.DestID = A.ID INNER Join FC_Items AS G ON A.ItemID = G.ID  " +
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
            return new T1_LR1_ListPlank() ;
        }
        public T1_LR1_ListPlank RetrieveByID(string ID)
        {
            string strSQL = "select * from (SELECT  0 AS Flag, A.ID AS DestID, H.ItemID, H.ID, H.ID AS JemurID, H.TglTrans, G.PartNo,G.Lebar * G.Panjang AS luasA, " +
                "CASE WHEN A.FormulaID > 0 THEN (SELECT formulacode FROM BM_Formula WHERE id = A.FormulaID) END AS FormulaCode,   " +
                "CASE WHEN A.PlantGroupID > 0 THEN (SELECT [GROUp] FROM BM_PlantGroup WHERE id = A.plantgroupID) END AS gp,   " +
                "CASE WHEN A.PaletID > 0 THEN (SELECT NoPAlet FROM BM_Palet WHERE id = A.PaletID) END AS NoPAlet,   " +
                "CASE WHEN A.LokasiID > 0 THEN (SELECT Lokasi FROM FC_Lokasi WHERE id = A.LokasiID) END AS lokasi,   " +
                "CASE WHEN H.RakID > 0 THEN (SELECT Rak FROM FC_Rak WHERE id = H.RakID) END AS rak, A.TglProduksi, ISNULL(A.HPP, 0) AS HPP,  " +
                "H.QtyIn, H.RakID, H.QtyOut, H.HPP AS Expr2,H.QtyIn-H.QtyOut as Sisa    " +
                "FROM T1_Bevel AS H INNER JOIN BM_Destacking AS A ON H.DestID = A.ID INNER Join FC_Items AS G ON A.ItemID = G.ID  " +
                "WHERE H.rowstatus>-1) as jemur where ID=" + ID;
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
            return new T1_LR1_ListPlank();
        }
        public ArrayList RetrieveforSerah1(string tglProduksi, string criteria)
        {
            string strSQL = "select * from (SELECT  0 AS Flag,H.fail, A.ID AS DestID, A.ItemID, H.ID, H.ID AS JemurID, H.TglTrans, G.PartNo,G.Lebar * G.Panjang AS luasA, " +
                "CASE WHEN A.FormulaID > 0 THEN (SELECT formulacode FROM BM_Formula WHERE id = A.FormulaID) END AS FormulaCode,  " +
                "CASE WHEN A.PlantGroupID > 0 THEN (SELECT [GROUp] FROM BM_PlantGroup WHERE id = A.plantgroupID) END AS gp,  " +
                "CASE WHEN A.PaletID > 0 THEN (SELECT NoPAlet FROM BM_Palet WHERE id = A.PaletID) END AS NoPAlet,  " +
                "CASE WHEN A.LokasiID > 0 THEN (SELECT Lokasi FROM FC_Lokasi WHERE id = A.LokasiID) END AS lokasi,  " +
                "CASE WHEN H.RakID > 0 THEN (SELECT Rak FROM FC_Rak WHERE id = H.RakID) END AS rak, A.TglProduksi, ISNULL(A.HPP, 0) AS HPP, " +
                "H.QtyIn, H.RakID, H.QtyOut, H.HPP AS Expr2,H.QtyIn-H.QtyOut as Sisa   " +
                "FROM T1_LR1_ListPlank AS H INNER JOIN BM_Destacking AS A ON H.DestID = A.ID INNER Join FC_Items AS G ON A.ItemID = G.ID " +
                "WHERE convert(varchar,A.TglProduksi,112) = '" + tglProduksi + "' and H.QtyIn> H.QtyOut and  " + criteria + " H.rowstatus>-1)as jmr order by NoPAlet ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_LR1_ListPlank = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_LR1_ListPlank.Add(GenerateObjectforserah(sqlDataReader));
                }
            }
            else
                arrT1_LR1_ListPlank.Add(new T1_LR1_ListPlank());

            return arrT1_LR1_ListPlank;
        }
        public ArrayList RetrieveforSerah2(string tglProduksi, string criteria, string potong)
        {
            string strSQL =string.Empty;
            if (potong == "1020")
             strSQL = "select * from (SELECT  0 AS Flag,H.fail, A.ID AS DestID, A.ItemID, H.ID, H.ID AS JemurID, H.TglTrans, G.PartNo,G.Lebar * G.Panjang AS luasA, " +
                "CASE WHEN A.FormulaID > 0 THEN (SELECT formulacode FROM BM_Formula WHERE id = A.FormulaID) END AS FormulaCode,  " +
                "CASE WHEN A.PlantGroupID > 0 THEN (SELECT [GROUp] FROM BM_PlantGroup WHERE id = A.plantgroupID) END AS gp,  " +
                "CASE WHEN A.PaletID > 0 THEN (SELECT NoPAlet FROM BM_Palet WHERE id = A.PaletID) END AS NoPAlet,  " +
                "CASE WHEN A.LokasiID > 0 THEN (SELECT Lokasi FROM FC_Lokasi WHERE id = A.LokasiID) END AS lokasi,  " +
                "CASE WHEN H.RakID > 0 THEN (SELECT Rak FROM FC_Rak WHERE id = H.RakID) END AS rak, A.TglProduksi, ISNULL(A.HPP, 0) AS HPP, " +
                "H.QtyIn, H.RakID, H.QtyOut, H.HPP AS Expr2,H.QtyIn-H.QtyOut as Sisa  " +
                "FROM T1_LR1_ListPlank AS H INNER JOIN BM_Destacking AS A ON H.DestID = A.ID INNER Join FC_Items AS G ON A.ItemID = G.ID " +
                "WHERE  A.status=1 and A.itemid in (select ID from fc_items where partno like '%04010202020%') " + tglProduksi + " and H.QtyIn> H.QtyOut and  " + criteria + " H.rowstatus>-1 and A.rowstatus>-1)as jmr order by TglProduksi,NoPAlet ";
            if (potong == "ListPlank2")
                strSQL = "select * from (SELECT  0 AS Flag,H.fail, A.ID AS DestID, A.ItemID, H.ID, H.ID AS JemurID, H.TglTrans, G.PartNo,G.Lebar * G.Panjang AS luasA, " +
                   "CASE WHEN A.FormulaID > 0 THEN (SELECT formulacode FROM BM_Formula WHERE id = A.FormulaID) END AS FormulaCode,  " +
                   "CASE WHEN A.PlantGroupID > 0 THEN (SELECT [GROUp] FROM BM_PlantGroup WHERE id = A.plantgroupID) END AS gp,  " +
                   "CASE WHEN A.PaletID > 0 THEN (SELECT NoPAlet FROM BM_Palet WHERE id = A.PaletID) END AS NoPAlet,  " +
                   "CASE WHEN A.LokasiID > 0 THEN (SELECT Lokasi FROM FC_Lokasi WHERE id = A.LokasiID) END AS lokasi,  " +
                   "CASE WHEN H.RakID > 0 THEN (SELECT Rak FROM FC_Rak WHERE id = H.RakID) END AS rak, A.TglProduksi, ISNULL(A.HPP, 0) AS HPP, " +
                   "H.QtyIn, H.RakID, H.QtyOut, H.HPP AS Expr2,H.QtyIn-H.QtyOut as Sisa  " +
                   "FROM T1_LR1_ListPlank AS H INNER JOIN BM_Destacking AS A ON H.DestID = A.ID INNER Join FC_Items AS G ON A.ItemID = G.ID " +
                   "WHERE A.itemid in (select ID from fc_items where partno like '%090%' or partno like '%080%') " + tglProduksi + " and H.QtyIn> H.QtyOut and  " + criteria + " H.rowstatus>-1 and A.rowstatus>-1)as jmr order by TglProduksi,NoPAlet ";
            if (potong == " ")
                strSQL = "select top 100 * from (SELECT  0 AS Flag,H.fail, A.ID AS DestID, A.ItemID, H.ID, H.ID AS JemurID, H.TglTrans, G.PartNo,G.Lebar * G.Panjang AS luasA, " +
                   "CASE WHEN A.FormulaID > 0 THEN (SELECT formulacode FROM BM_Formula WHERE id = A.FormulaID) END AS FormulaCode,  " +
                   "CASE WHEN A.PlantGroupID > 0 THEN (SELECT [GROUp] FROM BM_PlantGroup WHERE id = A.plantgroupID) END AS gp,  " +
                   "CASE WHEN A.PaletID > 0 THEN (SELECT NoPAlet FROM BM_Palet WHERE id = A.PaletID) END AS NoPAlet,  " +
                   "CASE WHEN A.LokasiID > 0 THEN (SELECT Lokasi FROM FC_Lokasi WHERE id = A.LokasiID) END AS lokasi,  " +
                   "CASE WHEN H.RakID > 0 THEN (SELECT Rak FROM FC_Rak WHERE id = H.RakID) END AS rak, A.TglProduksi, ISNULL(A.HPP, 0) AS HPP, " +
                   "H.QtyIn, H.RakID, H.QtyOut, H.HPP AS Expr2,H.QtyIn-H.QtyOut as Sisa  " +
                   "FROM T1_LR1_ListPlank AS H INNER JOIN BM_Destacking AS A ON H.DestID = A.ID INNER Join FC_Items AS G ON A.ItemID = G.ID " +
                   "WHERE G.partno like '%-1-%' and A.itemid in (select ID from fc_items where partno not like '%080%' and partno not like '%090%' and partno not like '%04010202020%') " + tglProduksi + " and H.QtyIn> H.QtyOut and  " + criteria + " H.rowstatus>-1 and A.rowstatus>-1)as jmr order by TglProduksi,NoPAlet ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_LR1_ListPlank = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_LR1_ListPlank.Add(GenerateObjectforserah(sqlDataReader));
                }
            }
            else
                arrT1_LR1_ListPlank.Add(new T1_LR1_ListPlank());

            return arrT1_LR1_ListPlank;
        }
        public ArrayList RetrieveforSerah3(string tglProduksi, string criteria)
        {
            string strSQL = string.Empty;

            strSQL = "select * from (SELECT  0 AS Flag,H.fail, A.ID AS DestID, A.ItemID, H.ID, H.ID AS JemurID, H.TglTrans, G.PartNo,G.Lebar * G.Panjang AS luasA, " +
               "CASE WHEN A.FormulaID > 0 THEN (SELECT formulacode FROM BM_Formula WHERE id = A.FormulaID) END AS FormulaCode,  " +
               "CASE WHEN A.PlantGroupID > 0 THEN (SELECT [GROUp] FROM BM_PlantGroup WHERE id = A.plantgroupID) END AS gp,  " +
               "CASE WHEN A.PaletID > 0 THEN (SELECT NoPAlet FROM BM_Palet WHERE id = A.PaletID) END AS NoPAlet,  " +
               "CASE WHEN A.LokasiID > 0 THEN (SELECT Lokasi FROM FC_Lokasi WHERE id = A.LokasiID) END AS lokasi,  " +
               "CASE WHEN H.RakID > 0 THEN (SELECT Rak FROM FC_Rak WHERE id = H.RakID) END AS rak, A.TglProduksi, ISNULL(A.HPP, 0) AS HPP, " +
               "H.QtyIn, H.RakID, H.QtyOut, H.HPP AS Expr2,H.QtyIn-H.QtyOut as Sisa   " +
               "FROM T1_LR1_ListPlank AS H INNER JOIN BM_Destacking AS A ON H.DestID = A.ID INNER Join FC_Items AS G ON A.ItemID = G.ID " +
               "WHERE A.itemid in (select ID from fc_items where partno like '%090%') and convert(varchar,A.TglProduksi,112) = '" + tglProduksi + "' and H.QtyIn> H.QtyOut and  " + criteria + " H.rowstatus>-1)as jmr order by NoPAlet ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_LR1_ListPlank = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_LR1_ListPlank.Add(GenerateObjectforserah(sqlDataReader));
                }
            }
            else
                arrT1_LR1_ListPlank.Add(new T1_LR1_ListPlank());

            return arrT1_LR1_ListPlank;
        }
        public ArrayList RetrieveforPelarian(string tglProduksi, string criteria, string sortby)
        {
            string strSQL = "SELECT top 25 0 AS Flag,A.ID AS DestID, A.ItemID, H.ID, H.ID AS JemurID, H.TglTrans, G.PartNo, G.Lebar * G.Panjang AS luasA, " +
                "CASE WHEN A.FormulaID > 0 THEN (SELECT formulacode FROM  BM_Formula WHERE  id = A.FormulaID) END AS FormulaCode, "+
                "CASE WHEN A.PlantGroupID > 0 THEN (SELECT [GROUp] FROM  BM_PlantGroup WHERE  id = A.plantgroupID) END AS gp, "+
                "CASE WHEN A.PaletID > 0 THEN (SELECT NoPAlet FROM  BM_Palet WHERE  id = A.PaletID) END AS NoPAlet, "+
                "CASE WHEN H.LokID0 > 0 THEN (SELECT Lokasi FROM  FC_Lokasi WHERE  id = H.LokID0) END AS lokasi, "+
                "CASE WHEN H.RakID > 0 THEN (SELECT Rak FROM FC_Rak WHERE  id = H.RakID) END AS rak, A.TglProduksi, "+
                "ISNULL(A.HPP, 0) AS HPP, H.QtyIn, H.RakID, H.QtyOut, H.HPP AS HPP,H.QtyIn-H.QtyOut as Sisa  " +
                "FROM T1_LR1_ListPlanklg AS H left JOIN BM_Destacking AS A ON H.DestID = A.ID left JOIN FC_Items AS G ON H.ItemID0 = G.ID " +
                "WHERE " + criteria + " convert(varchar,H.TglTrans,112) = '" + tglProduksi + 
                "' and H.QtyIn> H.QtyOut  and H.status>-1 order by  " + sortby;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_LR1_ListPlank = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_LR1_ListPlank.Add(GenerateObjectforserah(sqlDataReader));
                }
            }
            else
                arrT1_LR1_ListPlank.Add(new T1_LR1_ListPlank());

            return arrT1_LR1_ListPlank;
        }
        public int UpdateFail(int id, int qty)
        {
            string strSQL = "update T1_LR1_ListPlank set qtyout=qtyout+" + qty + " where id=" + id;
            int intError = 0;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (strError != string.Empty)
                intError = 1;
            return intError;
        }
        public ArrayList RetrieveTransJmrLg(string TglTrans)
        {
            string strSQL = "SELECT TOP (25) A.ID AS DestID, H.ID, H.JemurID, E.FormulaCode, B.[Group] AS gp, G.PartNo, F.NoPAlet, C.Lokasi, A.TglProduksi, "+
                          "ISNULL(A.HPP, 0) AS HPP, H.TglTrans, H.QtyIn, H.QtyOut, H.RakID, I.Rak, J.Lokasi AS lokasiTF " +
                          "FROM FC_Lokasi AS J RIGHT OUTER JOIN " +
                          "T1_LR1_ListPlankLg AS H ON J.ID = H.LokID0 LEFT OUTER JOIN " +
                          "FC_Items AS G ON H.ItemID0 = G.ID LEFT OUTER JOIN " +
                          "FC_Rak AS I ON H.RakID = I.ID LEFT OUTER JOIN " +
                          "BM_Destacking AS A LEFT OUTER JOIN " +
                          "BM_Formula AS E ON A.FormulaID = E.ID LEFT OUTER JOIN " +
                          "BM_PlantGroup AS B ON A.PlantGroupID = B.ID LEFT OUTER JOIN " +
                          "FC_Lokasi AS C ON A.LokasiID = C.ID LEFT OUTER JOIN " +
                          "BM_Palet AS F ON A.PaletID = F.ID ON H.DestID = A.ID " +
                          "WHERE A.rowstatus>-1 and H.status>-1 and convert(varchar,H.TglTrans,112) = '" + TglTrans + "' order by H.id desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_LR1_ListPlank = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_LR1_ListPlank.Add(GenerateObjectforjemurlg(sqlDataReader));
                }
            }
            else
                arrT1_LR1_ListPlank.Add(new T1_LR1_ListPlank());

            return arrT1_LR1_ListPlank;
        }
        public ArrayList RetrieveByTglProduksi(string tglProduksi)
        {
            string strSQL = "SELECT A.ID as Destid,H.ID as ID,E.FormulaCode, B.[Group] as gp, G.PartNo, F.NoPAlet, C.Lokasi,A.TglProduksi,H.TglTrans, H.RakID,H.QtyIn,isnull(A.HPP,0) as HPP, I.Rak " +
                      "FROM BM_Destacking AS A INNER JOIN " +
                      "BM_Formula AS E ON A.FormulaID = E.ID INNER JOIN " +
                      "BM_PlantGroup AS B ON A.PlantGroupID = B.ID INNER JOIN " +
                      "FC_Lokasi AS C ON A.LokasiID = C.ID INNER JOIN " +
                      "BM_Palet AS F ON A.PaletID = F.ID INNER JOIN " +
                      "FC_Items AS G ON A.ItemID = G.ID INNER JOIN " +
                      "T1_LR1_ListPlank as H ON A.ID = H.DestID INNER JOIN " +
                      "FC_Rak as I ON H.RakID = I.ID " +
                      "WHERE  A.rowstatus>-1 and convert(varchar,A.TglProduksi,112) = '" + tglProduksi + "' order by H.id desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_LR1_ListPlank = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_LR1_ListPlank.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT1_LR1_ListPlank.Add(new T1_LR1_ListPlank());

            return arrT1_LR1_ListPlank;
        }
        public int ValidasiRak(string rak, string tgl1, string tgl2)
        {
            int jumlah = 0;
            string strSQL = "select COUNT(*) as id from T1_LR1_ListPlank where CONVERT(varchar,TglTrans,112) >='" + tgl1 + "' and CONVERT(varchar,TglTrans,112) <='" + tgl2  +
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
                arrT1_LR1_ListPlank.Add(new T1_LR1_ListPlank());
            return jumlah;
        }
        public int Check(int destid)
        {
            int jumlah = 0;
            string strSQL = "select * from T1_LR1_ListPlank where destid=" + destid ;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    jumlah =1;
                }
            }
            return jumlah;
        }
        public T1_LR1_ListPlank GenerateObject(SqlDataReader sqlDataReader)
        {
            objT1_LR1_ListPlank = new T1_LR1_ListPlank();
            objT1_LR1_ListPlank.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objT1_LR1_ListPlank.DestID = Convert.ToInt32(sqlDataReader["DestID"]);
            objT1_LR1_ListPlank.PlantGroup = (sqlDataReader["gp"]).ToString();
            objT1_LR1_ListPlank.Formula = (sqlDataReader["FormulaCode"]).ToString();
            objT1_LR1_ListPlank.TglProduksi = Convert.ToDateTime(sqlDataReader["TglProduksi"]);
            objT1_LR1_ListPlank.TglTrans  = Convert.ToDateTime(sqlDataReader["TglTrans"]);
            objT1_LR1_ListPlank.QtyIn = Convert.ToInt32(sqlDataReader["Qtyin"]);
            objT1_LR1_ListPlank.RakID = Convert.ToInt32(sqlDataReader["RakID"]);
            objT1_LR1_ListPlank.Rak = (sqlDataReader["Rak"]).ToString();
            objT1_LR1_ListPlank.HPP = Convert.ToDecimal(sqlDataReader["HPP"]);
            objT1_LR1_ListPlank.Partno = (sqlDataReader["PartNo"]).ToString();
            objT1_LR1_ListPlank.Lokasi = (sqlDataReader["Lokasi"]).ToString();
            objT1_LR1_ListPlank.Palet = (sqlDataReader["NoPAlet"]).ToString();
            return objT1_LR1_ListPlank;
        }
        public T1_LR1_ListPlank GenerateObjectForCuring(SqlDataReader sqlDataReader)
        {
            objT1_LR1_ListPlank = new T1_LR1_ListPlank();
            objT1_LR1_ListPlank.DestID = Convert.ToInt32(sqlDataReader["DestID"]);
            objT1_LR1_ListPlank.PlantGroup = (sqlDataReader["Group"]).ToString();
            objT1_LR1_ListPlank.Formula = (sqlDataReader["FormulaCode"]).ToString();
            objT1_LR1_ListPlank.Palet = (sqlDataReader["NoPAlet"]).ToString();
            objT1_LR1_ListPlank.TglProduksi = Convert.ToDateTime(sqlDataReader["TglProduksi"]);
            objT1_LR1_ListPlank.Lokasi = (sqlDataReader["Lokasi"]).ToString();
            objT1_LR1_ListPlank.QtyIn = Convert.ToInt32(sqlDataReader["Qty"]);
            objT1_LR1_ListPlank.HPP  = Convert.ToDecimal(sqlDataReader["HPP"]);
            objT1_LR1_ListPlank.Partno = (sqlDataReader["PartNo"]).ToString();
            return objT1_LR1_ListPlank;
        }
        public T1_LR1_ListPlank GenerateObjectforjemurlg(SqlDataReader sqlDataReader)
        {try {
            objT1_LR1_ListPlank = new T1_LR1_ListPlank();
            objT1_LR1_ListPlank.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objT1_LR1_ListPlank.JemurID = Convert.ToInt32(sqlDataReader["JemurID"]);
            objT1_LR1_ListPlank.DestID = Convert.ToInt32(sqlDataReader["DestID"]);
            objT1_LR1_ListPlank.PlantGroup = (sqlDataReader["gp"]).ToString();
            objT1_LR1_ListPlank.Formula = (sqlDataReader["FormulaCode"]).ToString();
            objT1_LR1_ListPlank.TglProduksi = Convert.ToDateTime(sqlDataReader["TglProduksi"]);
            objT1_LR1_ListPlank.TglTrans = Convert.ToDateTime(sqlDataReader["TglTrans"]);
            objT1_LR1_ListPlank.QtyIn = Convert.ToInt32(sqlDataReader["Qtyin"]);
            objT1_LR1_ListPlank.QtyOut = Convert.ToInt32(sqlDataReader["QtyOut"]);
            objT1_LR1_ListPlank.HPP = Convert.ToDecimal(sqlDataReader["HPP"]);
            objT1_LR1_ListPlank.RakID = Convert.ToInt32(sqlDataReader["RakID"]);
            objT1_LR1_ListPlank.Rak = (sqlDataReader["Rak"]).ToString();
            objT1_LR1_ListPlank.Partno = (sqlDataReader["PartNo"]).ToString();
            objT1_LR1_ListPlank.Lokasi = (sqlDataReader["Lokasi"]).ToString();
            objT1_LR1_ListPlank.LokasiTF = (sqlDataReader["LokasiTF"]).ToString();
            objT1_LR1_ListPlank.Palet = (sqlDataReader["NoPAlet"]).ToString();
            }
            catch
            {
            }
            return objT1_LR1_ListPlank;
        }
        public T1_LR1_ListPlank GenerateObjectforserah(SqlDataReader sqlDataReader)
        {
            try
            {
                objT1_LR1_ListPlank = new T1_LR1_ListPlank();
                objT1_LR1_ListPlank.ID = Convert.ToInt32(sqlDataReader["ID"]);
                objT1_LR1_ListPlank.Flag = Convert.ToInt32(sqlDataReader["Flag"]);
                objT1_LR1_ListPlank.JemurID = Convert.ToInt32(sqlDataReader["JemurID"]);
                objT1_LR1_ListPlank.DestID = Convert.ToInt32(sqlDataReader["DestID"]);
                objT1_LR1_ListPlank.PlantGroup = (sqlDataReader["gp"]).ToString();
                objT1_LR1_ListPlank.Formula = (sqlDataReader["FormulaCode"]).ToString();
                objT1_LR1_ListPlank.TglProduksi = Convert.ToDateTime(sqlDataReader["TglProduksi"]);
                objT1_LR1_ListPlank.TglTrans = Convert.ToDateTime(sqlDataReader["TglTrans"]);
                objT1_LR1_ListPlank.QtyIn = Convert.ToInt32(sqlDataReader["Qtyin"]);
                objT1_LR1_ListPlank.QtyOut = Convert.ToInt32(sqlDataReader["QtyOut"]);
                objT1_LR1_ListPlank.HPP = Convert.ToDecimal(sqlDataReader["HPP"]);
                objT1_LR1_ListPlank.RakID = Convert.ToInt32(sqlDataReader["RakID"]);
                objT1_LR1_ListPlank.Rak = (sqlDataReader["Rak"]).ToString();
                objT1_LR1_ListPlank.Partno = (sqlDataReader["PartNo"]).ToString();
                objT1_LR1_ListPlank.Lokasi = (sqlDataReader["Lokasi"]).ToString();
                objT1_LR1_ListPlank.Palet = (sqlDataReader["NoPAlet"]).ToString();
                objT1_LR1_ListPlank.ItemID = Convert.ToInt32(sqlDataReader["itemid"]);
                objT1_LR1_ListPlank.LuasA = Convert.ToInt32(sqlDataReader["LuasA"]);
                objT1_LR1_ListPlank.Sisa = Convert.ToInt32(sqlDataReader["Sisa"]);
                objT1_LR1_ListPlank.Fail = Convert.ToInt32(sqlDataReader["fail"]);
            }
            catch
            {
            }
            return objT1_LR1_ListPlank;
        }
        public ArrayList RetrieveCountSheet(string lokasi, string tanggal1, string tanggal2,string nopalet)
        {
            string strSQL = string.Empty;
            string strPalet = string.Empty;
            if (nopalet != string.Empty)
                strPalet = " and palet=" + nopalet;
            if (lokasi == "curing")
                strSQL = "select top 100 null as TglTranslg,*,(select SUM(qtyin-qtyout) from T1_LR1_ListPlank where DestID=A.destid)as sisajemur from (select destid,  itemid0 as itemid,partno,Lokasiid,lokasi,tglproduksi,TglTrans,palet,Rak,sum(Qty) as saldo from vw_kartustockWIPDet  " +
                    "where convert(varchar,tglproduksi,112)>='" + tanggal1 + "' and  convert(varchar,tglproduksi,112)<='" + tanggal2 + "' " +
                    "group by partno,lokasi ,tglproduksi,TglTrans,palet,rak,destid,Lokasiid,itemid0  having sum(Qty)<>0 ) as A  where isnull(TglTrans,'1/1/1900')='1/1/1900' " + strPalet + " order by tglproduksi,palet";
            else
                strSQL = "select  top 100  null as TglTranslg,*,(select SUM(qtyin-qtyout) from T1_LR1_ListPlank where DestID=A.destid)as sisajemur from " +
                    "(select destid,  itemid0 as itemid,partno,Lokasiid,lokasi,tglproduksi,TglTrans,palet,Rak,sum(Qty) as saldo from vw_kartustockWIPDet  " +
               "where convert(varchar,tglproduksi,112)>='" + tanggal1 + "' and  convert(varchar,tglproduksi,112)<='" + tanggal2 + "' " +
               "group by partno,lokasi ,tglproduksi,TglTrans,palet,rak,destid,Lokasiid,itemid0  having sum(Qty)<>0 ) as A  where isnull(TglTrans,'1/1/1900')<>'1/1/1900' " + strPalet + " order by tglproduksi,palet";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_LR1_ListPlank = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_LR1_ListPlank.Add(GenerateObjectForCountsheet(sqlDataReader));
                }
            }
            else
                arrT1_LR1_ListPlank.Add(new T1_LR1_ListPlank());
            return arrT1_LR1_ListPlank;
        }
        public T1_LR1_ListPlank GenerateObjectForCountsheet(SqlDataReader sqlDataReader)
        {
            try
            {
                objT1_LR1_ListPlank = new T1_LR1_ListPlank();
                //objT1_LR1_ListPlank.ID = Convert.ToInt32(sqlDataReader["ID"]);
                //objT1_LR1_ListPlank.Flag = Convert.ToInt32(sqlDataReader["Flag"]);
                //objT1_LR1_ListPlank.JemurID = Convert.ToInt32(sqlDataReader["JemurID"]);
                objT1_LR1_ListPlank.DestID = Convert.ToInt32(sqlDataReader["DestID"]);
                //objT1_LR1_ListPlank.PlantGroup = (sqlDataReader["gp"]).ToString();
                objT1_LR1_ListPlank.LokasiID  = Convert.ToInt32(sqlDataReader["LokasiID"]);
                objT1_LR1_ListPlank.TglProduksi = Convert.ToDateTime(sqlDataReader["TglProduksi"]);
                objT1_LR1_ListPlank.Partno = (sqlDataReader["PartNo"]).ToString();
                objT1_LR1_ListPlank.Lokasi = (sqlDataReader["Lokasi"]).ToString();
                objT1_LR1_ListPlank.Palet = (sqlDataReader["PAlet"]).ToString();
                objT1_LR1_ListPlank.QtyIn = Convert.ToInt32(sqlDataReader["saldo"]);
                objT1_LR1_ListPlank.ItemID = Convert.ToInt32(sqlDataReader["itemid"]);
                objT1_LR1_ListPlank.Rak = (sqlDataReader["Rak"]).ToString();
                objT1_LR1_ListPlank.TglTrans = Convert.ToDateTime(sqlDataReader["TglTrans"]);
                objT1_LR1_ListPlank.QtyOut = Convert.ToInt32(sqlDataReader["sisajemur"]);
                //objT1_LR1_ListPlank.HPP = Convert.ToDecimal(sqlDataReader["HPP"]);
                //objT1_LR1_ListPlank.RakID = Convert.ToInt32(sqlDataReader["RakID"]);
                
                //objT1_LR1_ListPlank.LuasA = Convert.ToInt32(sqlDataReader["LuasA"]);
                //objT1_LR1_ListPlank.Fail = Convert.ToInt32(sqlDataReader["fail"]);
            }
            catch
            {
            }
            return objT1_LR1_ListPlank;
        }
    }
}
