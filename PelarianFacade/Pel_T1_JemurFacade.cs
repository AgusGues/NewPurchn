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

namespace BusinessFacade
{
    public class Pel_T1_JemurFacade : AbstractTransactionFacade
    {
        private Pel_T1_Jemur objPel_T1_Jemur = new Pel_T1_Jemur();
        private ArrayList arrPel_T1_Jemur;
        private List<SqlParameter> sqlListParam;

        public Pel_T1_JemurFacade(object objDomain)
            : base(objDomain)
        {
            objPel_T1_Jemur = (Pel_T1_Jemur)objDomain;
        }
        public Pel_T1_JemurFacade()
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
                objPel_T1_Jemur = (Pel_T1_Jemur)objDomain;
                if (objPel_T1_Jemur.DestID == 0)
                    return -1;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@DestID", objPel_T1_Jemur.DestID ));
                sqlListParam.Add(new SqlParameter("@RakID", objPel_T1_Jemur.RakID  ));
                sqlListParam.Add(new SqlParameter("@TglJemur", objPel_T1_Jemur.TglJemur ));
                sqlListParam.Add(new SqlParameter("@QtyIn", objPel_T1_Jemur.QtyIn ));
                sqlListParam.Add(new SqlParameter("@HPP", objPel_T1_Jemur.HPP));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objPel_T1_Jemur.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertPel_T1_Jemur");
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
                objPel_T1_Jemur = (Pel_T1_Jemur)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Flag", objPel_T1_Jemur.Flag));
                sqlListParam.Add(new SqlParameter("@ID", objPel_T1_Jemur.ID));
                sqlListParam.Add(new SqlParameter("@QtyOut", objPel_T1_Jemur.QtyOut));
                sqlListParam.Add(new SqlParameter("@Oven", objPel_T1_Jemur.Oven));
                sqlListParam.Add(new SqlParameter("@TglJemur", objPel_T1_Jemur.TglJemur));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objPel_T1_Jemur.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateQtyPel_T1_Jemur1");
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
            arrPel_T1_Jemur = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPel_T1_Jemur.Add(GenerateObjectForCuring(sqlDataReader));
                }
            }
            else
                arrPel_T1_Jemur.Add(new Pel_T1_Jemur());

            return arrPel_T1_Jemur;
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
                "FROM Pel_T1_Jemur AS H INNER JOIN BM_Destacking AS A ON H.DestID = A.ID INNER Join FC_Items AS G ON A.ItemID = G.ID " +
                "WHERE " + criteria + " convert(varchar,A.TglProduksi,112)= '" + tglProduksi + "' and H.QtyIn> H.QtyOut and H.rowstatus>-1 order by " + sortby;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPel_T1_Jemur = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPel_T1_Jemur.Add(GenerateObjectforjemurlg(sqlDataReader));
                }
            }
            else
                arrPel_T1_Jemur.Add(new Pel_T1_Jemur());

            return arrPel_T1_Jemur;
        }

        public ArrayList RetrieveforSerah(string tglProduksi, string criteria,string sortby)
        {
            string strSQL = "SELECT top 15 0 AS Flag, A.ID AS DestID, A.ItemID, H.ID, H.ID AS JemurID, H.TglJemur, G.PartNo,G.Lebar * G.Panjang AS luasA, "+
                "CASE WHEN A.FormulaID > 0 THEN (SELECT formulacode FROM BM_Formula WHERE id = A.FormulaID) END AS FormulaCode,  "+
                "CASE WHEN A.PlantGroupID > 0 THEN (SELECT [GROUp] FROM BM_PlantGroup WHERE id = A.plantgroupID) END AS gp,  "+
                "CASE WHEN A.PaletID > 0 THEN (SELECT NoPAlet FROM BM_Palet WHERE id = A.PaletID) END AS NoPAlet,  "+
                "CASE WHEN A.LokasiID > 0 THEN (SELECT Lokasi FROM FC_Lokasi WHERE id = A.LokasiID) END AS lokasi,  "+
                "CASE WHEN H.RakID > 0 THEN (SELECT Rak FROM FC_Rak WHERE id = H.RakID) END AS rak, A.TglProduksi, ISNULL(A.HPP, 0) AS HPP, "+
                "H.QtyIn, H.RakID, H.QtyOut, H.HPP AS Expr2,H.QtyIn-H.QtyOut as Sisa   " +
                "FROM Pel_T1_Jemur AS H INNER JOIN BM_Destacking AS A ON H.DestID = A.ID INNER Join FC_Items AS G ON A.ItemID = G.ID " +
                "WHERE " + criteria + " convert(varchar,A.TglProduksi,112) = '" + tglProduksi + "' and H.QtyIn> H.QtyOut  and H.rowstatus>-1 order by  " + sortby;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPel_T1_Jemur = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPel_T1_Jemur.Add(GenerateObjectforserah(sqlDataReader));
                }
            }
            else
                arrPel_T1_Jemur.Add(new Pel_T1_Jemur());

            return arrPel_T1_Jemur;
        }
        public Pel_T1_Jemur  RetrieveJemur(string tglProduksi, string nopalet)
        {
            string strSQL = "select * from (SELECT  0 AS Flag, A.ID AS DestID, A.ItemID, H.ID, H.ID AS JemurID, H.TglJemur, G.PartNo,G.Lebar * G.Panjang AS luasA, "+
                "CASE WHEN A.FormulaID > 0 THEN (SELECT formulacode FROM BM_Formula WHERE id = A.FormulaID) END AS FormulaCode,   "+
                "CASE WHEN A.PlantGroupID > 0 THEN (SELECT [GROUp] FROM BM_PlantGroup WHERE id = A.plantgroupID) END AS gp,   "+
                "CASE WHEN A.PaletID > 0 THEN (SELECT NoPAlet FROM BM_Palet WHERE id = A.PaletID) END AS NoPAlet,   "+
                "CASE WHEN A.LokasiID > 0 THEN (SELECT Lokasi FROM FC_Lokasi WHERE id = A.LokasiID) END AS lokasi,   "+
                "CASE WHEN H.RakID > 0 THEN (SELECT Rak FROM FC_Rak WHERE id = H.RakID) END AS rak, A.TglProduksi, ISNULL(A.HPP, 0) AS HPP,  "+
                "H.QtyIn, H.RakID, H.QtyOut, H.HPP AS Expr2,H.QtyIn-H.QtyOut as Sisa    " +
                "FROM Pel_T1_Jemur AS H INNER JOIN BM_Destacking AS A ON H.DestID = A.ID INNER Join FC_Items AS G ON A.ItemID = G.ID  " +
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
            return new Pel_T1_Jemur() ;
        }
        public Pel_T1_Jemur RetrieveJemurByID(string ID)
        {
            string strSQL = "select * from (SELECT  0 AS Flag, A.ID AS DestID, A.ItemID, H.ID, H.ID AS JemurID, H.TglJemur, G.PartNo,G.Lebar * G.Panjang AS luasA, " +
                "CASE WHEN A.FormulaID > 0 THEN (SELECT formulacode FROM BM_Formula WHERE id = A.FormulaID) END AS FormulaCode,   " +
                "CASE WHEN A.PlantGroupID > 0 THEN (SELECT [GROUp] FROM BM_PlantGroup WHERE id = A.plantgroupID) END AS gp,   " +
                "CASE WHEN A.PaletID > 0 THEN (SELECT NoPAlet FROM BM_Palet WHERE id = A.PaletID) END AS NoPAlet,   " +
                "CASE WHEN A.LokasiID > 0 THEN (SELECT Lokasi FROM FC_Lokasi WHERE id = A.LokasiID) END AS lokasi,   " +
                "CASE WHEN H.RakID > 0 THEN (SELECT Rak FROM FC_Rak WHERE id = H.RakID) END AS rak, A.TglProduksi, ISNULL(A.HPP, 0) AS HPP,  " +
                "H.QtyIn, H.RakID, H.QtyOut, H.HPP AS Expr2,H.QtyIn-H.QtyOut as Sisa    " +
                "FROM Pel_T1_Jemur AS H INNER JOIN BM_Destacking AS A ON H.DestID = A.ID INNER Join FC_Items AS G ON A.ItemID = G.ID  " +
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
            return new Pel_T1_Jemur();
        }

        public ArrayList RetrieveforSerah1(string tglProduksi, string criteria)
        {
            string strSQL = "select * from (SELECT  0 AS Flag,H.fail, A.ID AS DestID, A.ItemID, H.ID, H.ID AS JemurID, H.TglJemur, G.PartNo,G.Lebar * G.Panjang AS luasA, " +
                "CASE WHEN A.FormulaID > 0 THEN (SELECT formulacode FROM BM_Formula WHERE id = A.FormulaID) END AS FormulaCode,  " +
                "CASE WHEN A.PlantGroupID > 0 THEN (SELECT [GROUp] FROM BM_PlantGroup WHERE id = A.plantgroupID) END AS gp,  " +
                "CASE WHEN A.PaletID > 0 THEN (SELECT NoPAlet FROM BM_Palet WHERE id = A.PaletID) END AS NoPAlet,  " +
                "CASE WHEN A.LokasiID > 0 THEN (SELECT Lokasi FROM FC_Lokasi WHERE id = A.LokasiID) END AS lokasi,  " +
                "CASE WHEN H.RakID > 0 THEN (SELECT Rak FROM FC_Rak WHERE id = H.RakID) END AS rak, A.TglProduksi, ISNULL(A.HPP, 0) AS HPP, " +
                "H.QtyIn, H.RakID, H.QtyOut, H.HPP AS Expr2,H.QtyIn-H.QtyOut as Sisa   " +
                "FROM Pel_T1_Jemur AS H INNER JOIN BM_Destacking AS A ON H.DestID = A.ID INNER Join FC_Items AS G ON A.ItemID = G.ID " +
                "WHERE convert(varchar,A.TglProduksi,112) = '" + tglProduksi + "' and H.QtyIn> H.QtyOut and  " + criteria + " H.rowstatus>-1)as jmr order by NoPAlet ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPel_T1_Jemur = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPel_T1_Jemur.Add(GenerateObjectforserah(sqlDataReader));
                }
            }
            else
                arrPel_T1_Jemur.Add(new Pel_T1_Jemur());

            return arrPel_T1_Jemur;
        }
        public ArrayList RetrieveforSerah2(string tglProduksi, string criteria, string potong)
        {
            string strSQL =string.Empty;
            #region hide
            if (potong == "1020")
             strSQL = "select * from (SELECT  0 AS Flag,H.fail, A.ID AS DestID, A.ItemID, H.ID, H.ID AS JemurID, H.TglJemur, G.PartNo,G.Lebar * G.Panjang AS luasA, " +
                "CASE WHEN A.FormulaID > 0 THEN (SELECT formulacode FROM BM_Formula WHERE id = A.FormulaID) END AS FormulaCode,  " +
                "CASE WHEN A.PlantGroupID > 0 THEN (SELECT [GROUp] FROM BM_PlantGroup WHERE id = A.plantgroupID) END AS gp,  " +
                "CASE WHEN A.PaletID > 0 THEN (SELECT NoPAlet FROM BM_Palet WHERE id = A.PaletID) END AS NoPAlet,  " +
                "CASE WHEN A.LokasiID > 0 THEN (SELECT Lokasi FROM FC_Lokasi WHERE id = A.LokasiID) END AS lokasi,  " +
                "CASE WHEN H.RakID > 0 THEN (SELECT Rak FROM FC_Rak WHERE id = H.RakID) END AS rak, A.TglProduksi, ISNULL(A.HPP, 0) AS HPP, " +
                "H.QtyIn, H.RakID, H.QtyOut, H.HPP AS Expr2,H.QtyIn-H.QtyOut as Sisa  " +
                "FROM T1_Jemur AS H INNER JOIN BM_Destacking AS A ON H.DestID = A.ID INNER Join FC_Items AS G ON A.ItemID = G.ID " +
                "WHERE  A.status=1 and A.itemid in (select ID from fc_items where partno like '%04010202020%') " + tglProduksi + " and H.QtyIn> H.QtyOut and  " + criteria + " H.rowstatus>-1 and A.rowstatus>-1)as jmr order by TglProduksi,NoPAlet ";
            if (potong == "listplank")
                strSQL = "select * from (SELECT  0 AS Flag,H.fail, A.ID AS DestID, A.ItemID, H.ID, H.ID AS JemurID, H.TglJemur, G.PartNo,G.Lebar * G.Panjang AS luasA, " +
                   "CASE WHEN A.FormulaID > 0 THEN (SELECT formulacode FROM BM_Formula WHERE id = A.FormulaID) END AS FormulaCode,  " +
                   "CASE WHEN A.PlantGroupID > 0 THEN (SELECT [GROUp] FROM BM_PlantGroup WHERE id = A.plantgroupID) END AS gp,  " +
                   "CASE WHEN A.PaletID > 0 THEN (SELECT NoPAlet FROM BM_Palet WHERE id = A.PaletID) END AS NoPAlet,  " +
                   "CASE WHEN A.LokasiID > 0 THEN (SELECT Lokasi FROM FC_Lokasi WHERE id = A.LokasiID) END AS lokasi,  " +
                   "CASE WHEN H.RakID > 0 THEN (SELECT Rak FROM FC_Rak WHERE id = H.RakID) END AS rak, A.TglProduksi, ISNULL(A.HPP, 0) AS HPP, " +
                   "H.QtyIn, H.RakID, H.QtyOut, H.HPP AS Expr2,H.QtyIn-H.QtyOut as Sisa  " +
                   "FROM T1_Jemur AS H INNER JOIN BM_Destacking AS A ON H.DestID = A.ID INNER Join FC_Items AS G ON A.ItemID = G.ID " +
                   "WHERE A.itemid in (select ID from fc_items where partno like '%090%' or partno like '%080%') " + tglProduksi + " and H.QtyIn> H.QtyOut and  " + criteria + " H.rowstatus>-1 and A.rowstatus>-1)as jmr order by TglProduksi,NoPAlet ";
            if (potong == "listplank2")
                strSQL = "select * from (SELECT  0 AS Flag,H.fail, A.ID AS DestID, A.ItemID, H.ID, H.ID AS JemurID, H.TglJemur, G.PartNo,G.Lebar * G.Panjang AS luasA, " +
                    "CASE WHEN A.FormulaID > 0 THEN (SELECT formulacode FROM BM_Formula WHERE id = A.FormulaID) END AS FormulaCode,   " +
                    "CASE WHEN A.PlantGroupID > 0 THEN (SELECT [GROUp] FROM BM_PlantGroup WHERE id = A.plantgroupID) END AS gp,   " +
                    "CASE WHEN A.PaletID > 0 THEN (SELECT NoPAlet FROM BM_Palet WHERE id = A.PaletID) END AS NoPAlet,  " +
                    "CASE WHEN A.LokasiID > 0 THEN (SELECT Lokasi FROM FC_Lokasi WHERE id = A.LokasiID) END AS lokasi,   " +
                    "CASE WHEN H.RakID > 0 THEN (SELECT Rak FROM FC_Rak WHERE id = H.RakID) END AS rak, A.TglProduksi, ISNULL(A.HPP, 0) AS HPP,  " +
                    "L.QtyIn, H.RakID, L.QtyOut, H.HPP AS Expr2,L.QtyIn-L.QtyOut as Sisa   " +
                    "FROM T1_Jemur AS H INNER JOIN BM_Destacking AS A ON H.DestID = A.ID  inner join T1_ListPlank L on L.DestID=A.ID INNER Join FC_Items AS G ON L.ItemID = G.ID " +
                    "WHERE  A.rowstatus>-1 and A.rowstatus>-1)as jmr order by TglProduksi,NoPAlet";
            if (potong == " ")
                //strSQL = "select top 100 * from (SELECT  0 AS Flag,H.fail, A.ID AS DestID, A.ItemID, H.ID, H.ID AS JemurID, H.TglJemur, G.PartNo,G.Lebar * G.Panjang AS luasA, " +
                //   "CASE WHEN A.FormulaID > 0 THEN (SELECT formulacode FROM BM_Formula WHERE id = A.FormulaID) END AS FormulaCode,  " +
                //   "CASE WHEN A.PlantGroupID > 0 THEN (SELECT [GROUp] FROM BM_PlantGroup WHERE id = A.plantgroupID) END AS gp,  " +
                //   "CASE WHEN A.PaletID > 0 THEN (SELECT NoPAlet FROM BM_Palet WHERE id = A.PaletID) END AS NoPAlet,  " +
                //   "CASE WHEN A.LokasiID > 0 THEN (SELECT Lokasi FROM FC_Lokasi WHERE id = A.LokasiID) END AS lokasi,  " +
                //   "CASE WHEN H.RakID > 0 THEN (SELECT Rak FROM FC_Rak WHERE id = H.RakID) END AS rak, A.TglProduksi, ISNULL(A.HPP, 0) AS HPP, " +
                //   "H.QtyIn, H.RakID, H.QtyOut, H.HPP AS Expr2,H.QtyIn-H.QtyOut as Sisa  " +
                //   "FROM T1_Jemur AS H INNER JOIN BM_Destacking AS A ON H.DestID = A.ID INNER Join FC_Items AS G ON A.ItemID = G.ID " +
                //   "WHERE G.partno like '%-1-%' and A.itemid in (select ID from fc_items where partno not like '%080%' and partno not like '%090%' and partno not like '%04010202020%') " + tglProduksi + " and H.QtyIn> H.QtyOut and  " + criteria + " H.rowstatus>-1 and A.rowstatus>-1)as jmr order by TglProduksi,NoPAlet ";
#endregion
                strSQL = "select  * from (SELECT  0 AS Flag,0 fail, A.ID AS DestID, A.ItemID, H.ID, H.ID AS JemurID, H.tglserah TglJemur, " +
                    "G.PartNo PartnoAsal,I.Partno,G.Lebar * G.Panjang AS luasA,Li.PlantName Line, BF.FormulaCode,BG.[Group]  gp,BP.NoPAlet, 'P99' lokasi,    " +
                    "'0' AS rak, A.TglProduksi, ISNULL(A.HPP, 0) AS HPP, H.QtyIn, 0 RakID, A.qty QtyOut  " +
                    "FROM T1_Serah AS H INNER JOIN BM_Destacking AS A ON H.DestID = A.ID INNER Join FC_Items AS G ON A.ItemID = G.ID   " +
                    "inner join FC_Items I on H.ItemID=I.ID  " +
                    "inner join BM_Plant Li on  A.PlantID =Li.ID inner join BM_PlantGroup BG on A.PlantGroupID=BG.ID   " +
                    "inner join BM_Formula BF on A.FormulaID=BF.ID inner join BM_Palet BP on A.PaletID=BP.ID   " +
                    "WHERE H.ID not in (select serahID from Pel_Transaksi where convert(char,tglproduksi,112)='" + tglProduksi + "'" + 
                    ") and SFrom='lari' and H.[status]>-1 "+ 
                    "and A.rowstatus>-1)as jmr where convert(char,tglproduksi,112)='" + tglProduksi + "'" + criteria + " order by nopalet";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPel_T1_Jemur = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPel_T1_Jemur.Add(GenerateObjectforserah(sqlDataReader));
                }
            }
            else
                arrPel_T1_Jemur.Add(new Pel_T1_Jemur());

            return arrPel_T1_Jemur;
        }
        public ArrayList RetrieveforSerah3(string tglProduksi, string criteria, string potong)
        {
            string strSQL = string.Empty;
            strSQL = "select * from (SELECT  0 AS Flag,H.fail, A.ID AS DestID, A.ItemID, L.ID, H.ID AS JemurID, H.TglJemur, G.PartNo,G.Lebar * G.Panjang AS luasA, " +
                    "CASE WHEN A.FormulaID > 0 THEN (SELECT formulacode FROM BM_Formula WHERE id = A.FormulaID) END AS FormulaCode,   " +
                    "CASE WHEN A.PlantGroupID > 0 THEN (SELECT [GROUp] FROM BM_PlantGroup WHERE id = A.plantgroupID) END AS gp,   " +
                    "CASE WHEN A.PaletID > 0 THEN (SELECT NoPAlet FROM BM_Palet WHERE id = A.PaletID) END AS NoPAlet,  " +
                    "CASE WHEN A.LokasiID > 0 THEN (SELECT Lokasi FROM FC_Lokasi WHERE id = A.LokasiID) END AS lokasi,   " +
                    "CASE WHEN H.RakID > 0 THEN (SELECT Rak FROM FC_Rak WHERE id = H.RakID) END AS rak, A.TglProduksi, ISNULL(A.HPP, 0) AS HPP,  " +
                    "L.QtyIn, H.RakID, L.QtyOut, H.HPP AS Expr2,L.QtyIn-L.QtyOut as Sisa   " +
                    "FROM Pel_T1_Jemur AS H INNER JOIN BM_Destacking AS A ON H.DestID = A.ID  inner join T1_ListPlank L on L.DestID=A.ID INNER Join FC_Items AS G ON L.ItemID = G.ID " +
                    "WHERE A.rowstatus>-1 and  " + criteria + " A.rowstatus>-1)as jmr where sisa>0  order by TglProduksi,NoPAlet";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPel_T1_Jemur = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPel_T1_Jemur.Add(GenerateObjectforserah(sqlDataReader));
                }
            }
            else
                arrPel_T1_Jemur.Add(new Pel_T1_Jemur());

            return arrPel_T1_Jemur;
        }
        public ArrayList RetrieveforSerah3(string tglProduksi, string criteria)
        {
            string strSQL = string.Empty;

            strSQL = "select * from (SELECT  0 AS Flag,H.fail, A.ID AS DestID, A.ItemID, H.ID, H.ID AS JemurID, H.TglJemur, G.PartNo,G.Lebar * G.Panjang AS luasA, " +
               "CASE WHEN A.FormulaID > 0 THEN (SELECT formulacode FROM BM_Formula WHERE id = A.FormulaID) END AS FormulaCode,  " +
               "CASE WHEN A.PlantGroupID > 0 THEN (SELECT [GROUp] FROM BM_PlantGroup WHERE id = A.plantgroupID) END AS gp,  " +
               "CASE WHEN A.PaletID > 0 THEN (SELECT NoPAlet FROM BM_Palet WHERE id = A.PaletID) END AS NoPAlet,  " +
               "CASE WHEN A.LokasiID > 0 THEN (SELECT Lokasi FROM FC_Lokasi WHERE id = A.LokasiID) END AS lokasi,  " +
               "CASE WHEN H.RakID > 0 THEN (SELECT Rak FROM FC_Rak WHERE id = H.RakID) END AS rak, A.TglProduksi, ISNULL(A.HPP, 0) AS HPP, " +
               "H.QtyIn, H.RakID, H.QtyOut, H.HPP AS Expr2,H.QtyIn-H.QtyOut as Sisa   " +
               "FROM Pel_T1_Jemur AS H INNER JOIN BM_Destacking AS A ON H.DestID = A.ID INNER Join FC_Items AS G ON A.ItemID = G.ID " +
               "WHERE A.itemid in (select ID from fc_items where partno like '%090%') and convert(varchar,A.TglProduksi,112) = '" + tglProduksi + "' and H.QtyIn> H.QtyOut and  " + criteria + " H.rowstatus>-1)as jmr order by NoPAlet ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPel_T1_Jemur = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPel_T1_Jemur.Add(GenerateObjectforserah(sqlDataReader));
                }
            }
            else
                arrPel_T1_Jemur.Add(new Pel_T1_Jemur());

            return arrPel_T1_Jemur;
        }
        public ArrayList RetrieveforPelarian(string tglProduksi, string criteria, string sortby)
        {
            string strSQL = "SELECT top 25 0 AS Flag,A.ID AS DestID, A.ItemID, H.ID, H.ID AS JemurID, H.TglJemur, G.PartNo, G.Lebar * G.Panjang AS luasA, " +
                "CASE WHEN A.FormulaID > 0 THEN (SELECT formulacode FROM  BM_Formula WHERE  id = A.FormulaID) END AS FormulaCode, "+
                "CASE WHEN A.PlantGroupID > 0 THEN (SELECT [GROUp] FROM  BM_PlantGroup WHERE  id = A.plantgroupID) END AS gp, "+
                "CASE WHEN A.PaletID > 0 THEN (SELECT NoPAlet FROM  BM_Palet WHERE  id = A.PaletID) END AS NoPAlet, "+
                "CASE WHEN H.LokID0 > 0 THEN (SELECT Lokasi FROM  FC_Lokasi WHERE  id = H.LokID0) END AS lokasi, "+
                "CASE WHEN H.RakID > 0 THEN (SELECT Rak FROM FC_Rak WHERE  id = H.RakID) END AS rak, A.TglProduksi, "+
                "ISNULL(A.HPP, 0) AS HPP, H.QtyIn, H.RakID, H.QtyOut, H.HPP AS HPP,H.QtyIn-H.QtyOut as Sisa  " +
                "FROM Pel_T1_Jemurlg AS H left JOIN BM_Destacking AS A ON H.DestID = A.ID left JOIN FC_Items AS G ON H.ItemID0 = G.ID " +
                "WHERE " + criteria + " convert(varchar,H.TglJemur,112) = '" + tglProduksi + 
                "' and H.QtyIn> H.QtyOut  and H.status>-1 order by  " + sortby;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPel_T1_Jemur = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPel_T1_Jemur.Add(GenerateObjectforserah(sqlDataReader));
                }
            }
            else
                arrPel_T1_Jemur.Add(new Pel_T1_Jemur());

            return arrPel_T1_Jemur;
        }
        public int UpdateFail(int id)
        {
            string strSQL = "update Pel_T1_Jemur set fail=1 where id=" + id;
            int intError = 0;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (strError != string.Empty)
                intError = 1;
            return intError;
        }
        public int UpdateTglOven(int id, string tgl)
        {
            string strSQL = "update Pel_T1_Jemur set tgljemur=" + tgl + " where id=" + id;
            int intError = 0;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (strError != string.Empty)
                intError = 1;
            return intError;
        }
        public int UpdateOven(int id, string oven)
        {
            string strSQL = "update Pel_T1_Jemur set oven='" + oven + "' where id=" + id;
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
                          "Pel_T1_JemurLg AS H ON J.ID = H.LokID0 LEFT OUTER JOIN " +
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
            arrPel_T1_Jemur = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPel_T1_Jemur.Add(GenerateObjectforjemurlg(sqlDataReader));
                }
            }
            else
                arrPel_T1_Jemur.Add(new Pel_T1_Jemur());

            return arrPel_T1_Jemur;
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
                      "Pel_T1_Jemur as H ON A.ID = H.DestID INNER JOIN " +
                      "FC_Rak as I ON H.RakID = I.ID " +
                      "WHERE  A.rowstatus>-1 and convert(varchar,A.TglProduksi,112) = '" + tglProduksi + "' order by H.id desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPel_T1_Jemur = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPel_T1_Jemur.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrPel_T1_Jemur.Add(new Pel_T1_Jemur());

            return arrPel_T1_Jemur;
        }
        public ArrayList RetrieveByTglJemur(string tglJemur)
        {
            string strSQL = "SELECT A.ID as Destid,H.ID as ID,E.FormulaCode, B.[Group] as gp, G.PartNo, F.NoPAlet, C.Lokasi,A.TglProduksi,H.TglJemur, H.RakID,H.QtyIn,isnull(A.HPP,0) as HPP, I.Rak " +
                      "FROM BM_Destacking AS A INNER JOIN " +
                      "BM_Formula AS E ON A.FormulaID = E.ID INNER JOIN " +
                      "BM_PlantGroup AS B ON A.PlantGroupID = B.ID INNER JOIN " +
                      "FC_Lokasi AS C ON A.LokasiID = C.ID INNER JOIN " +
                      "BM_Palet AS F ON A.PaletID = F.ID INNER JOIN " +
                      "FC_Items AS G ON A.ItemID = G.ID INNER JOIN " +
                      "Pel_T1_Jemur as H ON A.ID = H.DestID INNER JOIN " +
                      "FC_Rak as I ON H.RakID = I.ID " +
                      "WHERE  A.rowstatus>-1 and convert(varchar,H.TglJemur,112) = '" + tglJemur + "' order by H.id desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPel_T1_Jemur = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPel_T1_Jemur.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrPel_T1_Jemur.Add(new Pel_T1_Jemur());

            return arrPel_T1_Jemur;
        }
        public ArrayList RetrieveByOven(string tglJemur)
        {
            string strSQL = "SELECT A.ID as Destid,H.ID as ID,E.FormulaCode, B.[Group] as gp, G.PartNo, F.NoPAlet, C.Lokasi,A.TglProduksi,H.TglJemur, H.RakID,H.QtyIn,isnull(A.HPP,0) as HPP, I.Rak,H.Oven " +
                      "FROM BM_Destacking AS A INNER JOIN " +
                      "BM_Formula AS E ON A.FormulaID = E.ID INNER JOIN " +
                      "BM_PlantGroup AS B ON A.PlantGroupID = B.ID INNER JOIN " +
                      "FC_Lokasi AS C ON A.LokasiID = C.ID INNER JOIN " +
                      "BM_Palet AS F ON A.PaletID = F.ID INNER JOIN " +
                      "FC_Items AS G ON A.ItemID = G.ID INNER JOIN " +
                      "Pel_T1_Jemur as H ON A.ID = H.DestID INNER JOIN " +
                      "FC_Rak as I ON H.RakID = I.ID " +
                      "WHERE  isnull(H.Oven,'')<>'' and A.rowstatus>-1 and I.rak='00' and A.tglproduksi<>H.tgljemur and convert(varchar,A.TglProduksi,112) = '" + tglJemur + "' order by A.TglProduksi, F.NoPAlet ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPel_T1_Jemur = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPel_T1_Jemur.Add(GenerateObjectOven(sqlDataReader));
                }
            }
            else
                arrPel_T1_Jemur.Add(new Pel_T1_Jemur());

            return arrPel_T1_Jemur;
        }
        public ArrayList RetrieveByOvenByPalet(string tglJemur,string palet)
        {
            string strSQL = "SELECT A.ID as Destid,H.ID as ID,E.FormulaCode, B.[Group] as gp, G.PartNo, F.NoPAlet, C.Lokasi,A.TglProduksi,H.TglJemur, "+
                      "H.RakID,H.QtyIn,isnull(A.HPP,0) as HPP, I.Rak,H.Oven  " +
                      "FROM BM_Destacking AS A INNER JOIN " +
                      "BM_Formula AS E ON A.FormulaID = E.ID INNER JOIN " +
                      "BM_PlantGroup AS B ON A.PlantGroupID = B.ID INNER JOIN " +
                      "FC_Lokasi AS C ON A.LokasiID = C.ID INNER JOIN " +
                      "BM_Palet AS F ON A.PaletID = F.ID INNER JOIN " +
                      "FC_Items AS G ON A.ItemID = G.ID INNER JOIN " +
                      "Pel_T1_Jemur as H ON A.ID = H.DestID INNER JOIN " +
                      "FC_Rak as I ON H.RakID = I.ID " +
                      "WHERE  A.rowstatus>-1 and I.rak='00' and A.tglproduksi<>H.tgljemur and convert(varchar,A.TglProduksi,112) = '" +
                      tglJemur + "' and F.NoPAlet ='"+ palet +"' order by H.id desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPel_T1_Jemur = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPel_T1_Jemur.Add(GenerateObjectOven(sqlDataReader));
                }
            }
            else
                arrPel_T1_Jemur.Add(new Pel_T1_Jemur());

            return arrPel_T1_Jemur;
        }
        public int ValidasiRak(string rak, string tgl1, string tgl2)
        {
            int jumlah = 0;
            string strSQL = "select COUNT(*) as id from Pel_T1_Jemur where CONVERT(varchar,tgljemur,112) >='" + tgl1 + "' and CONVERT(varchar,tgljemur,112) <='" + tgl2  +
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
                arrPel_T1_Jemur.Add(new Pel_T1_Jemur());
            return jumlah;
        }
        public int Check(int destid)
        {
            int jumlah = 0;
            string strSQL = "select * from Pel_T1_Jemur where destid=" + destid ;
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

        public Pel_T1_Jemur GenerateObject(SqlDataReader sqlDataReader)
        {
            objPel_T1_Jemur = new Pel_T1_Jemur();
            objPel_T1_Jemur.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objPel_T1_Jemur.DestID = Convert.ToInt32(sqlDataReader["DestID"]);
            objPel_T1_Jemur.PlantGroup = (sqlDataReader["gp"]).ToString();
            objPel_T1_Jemur.Formula = (sqlDataReader["FormulaCode"]).ToString();
            objPel_T1_Jemur.TglProduksi = Convert.ToDateTime(sqlDataReader["TglProduksi"]);
            objPel_T1_Jemur.TglJemur  = Convert.ToDateTime(sqlDataReader["TglJemur"]);
            objPel_T1_Jemur.QtyIn = Convert.ToInt32(sqlDataReader["Qtyin"]);
            objPel_T1_Jemur.RakID = Convert.ToInt32(sqlDataReader["RakID"]);
            objPel_T1_Jemur.Rak = (sqlDataReader["Rak"]).ToString();
            objPel_T1_Jemur.HPP = Convert.ToDecimal(sqlDataReader["HPP"]);
            objPel_T1_Jemur.Partno = (sqlDataReader["PartNo"]).ToString();
            objPel_T1_Jemur.Lokasi = (sqlDataReader["Lokasi"]).ToString();
            objPel_T1_Jemur.Palet = (sqlDataReader["NoPAlet"]).ToString();
            return objPel_T1_Jemur;
        }

        public Pel_T1_Jemur GenerateObjectOven(SqlDataReader sqlDataReader)
        {
            objPel_T1_Jemur = new Pel_T1_Jemur();
            objPel_T1_Jemur.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objPel_T1_Jemur.DestID = Convert.ToInt32(sqlDataReader["DestID"]);
            objPel_T1_Jemur.PlantGroup = (sqlDataReader["gp"]).ToString();
            objPel_T1_Jemur.Formula = (sqlDataReader["FormulaCode"]).ToString();
            objPel_T1_Jemur.TglProduksi = Convert.ToDateTime(sqlDataReader["TglProduksi"]);
            objPel_T1_Jemur.TglJemur = Convert.ToDateTime(sqlDataReader["TglJemur"]);
            objPel_T1_Jemur.QtyIn = Convert.ToInt32(sqlDataReader["Qtyin"]);
            objPel_T1_Jemur.RakID = Convert.ToInt32(sqlDataReader["RakID"]);
            objPel_T1_Jemur.Rak = (sqlDataReader["Rak"]).ToString();
            objPel_T1_Jemur.HPP = Convert.ToDecimal(sqlDataReader["HPP"]);
            objPel_T1_Jemur.Partno = (sqlDataReader["PartNo"]).ToString();
            objPel_T1_Jemur.Lokasi = (sqlDataReader["Lokasi"]).ToString();
            objPel_T1_Jemur.Palet = (sqlDataReader["NoPAlet"]).ToString();
            objPel_T1_Jemur.Oven = (sqlDataReader["Oven"]).ToString();
            return objPel_T1_Jemur;
        }
        public Pel_T1_Jemur GenerateObjectForCuring(SqlDataReader sqlDataReader)
        {
            objPel_T1_Jemur = new Pel_T1_Jemur();
            objPel_T1_Jemur.DestID = Convert.ToInt32(sqlDataReader["DestID"]);
            objPel_T1_Jemur.PlantGroup = (sqlDataReader["Group"]).ToString();
            objPel_T1_Jemur.Formula = (sqlDataReader["FormulaCode"]).ToString();
            objPel_T1_Jemur.Palet = (sqlDataReader["NoPAlet"]).ToString();
            objPel_T1_Jemur.TglProduksi = Convert.ToDateTime(sqlDataReader["TglProduksi"]);
            objPel_T1_Jemur.Lokasi = (sqlDataReader["Lokasi"]).ToString();
            objPel_T1_Jemur.QtyIn = Convert.ToInt32(sqlDataReader["Qty"]);
            objPel_T1_Jemur.HPP  = Convert.ToDecimal(sqlDataReader["HPP"]);
            objPel_T1_Jemur.Partno = (sqlDataReader["PartNo"]).ToString();
            return objPel_T1_Jemur;
        }

        public Pel_T1_Jemur GenerateObjectforjemurlg(SqlDataReader sqlDataReader)
        {try {
            objPel_T1_Jemur = new Pel_T1_Jemur();
            objPel_T1_Jemur.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objPel_T1_Jemur.JemurID = Convert.ToInt32(sqlDataReader["JemurID"]);
            objPel_T1_Jemur.DestID = Convert.ToInt32(sqlDataReader["DestID"]);
            objPel_T1_Jemur.PlantGroup = (sqlDataReader["gp"]).ToString();
            objPel_T1_Jemur.Formula = (sqlDataReader["FormulaCode"]).ToString();
            objPel_T1_Jemur.TglProduksi = Convert.ToDateTime(sqlDataReader["TglProduksi"]);
            objPel_T1_Jemur.TglJemur = Convert.ToDateTime(sqlDataReader["TglJemur"]);
            objPel_T1_Jemur.QtyIn = Convert.ToInt32(sqlDataReader["Qtyin"]);
            objPel_T1_Jemur.QtyOut = Convert.ToInt32(sqlDataReader["QtyOut"]);
            objPel_T1_Jemur.HPP = Convert.ToDecimal(sqlDataReader["HPP"]);
            objPel_T1_Jemur.RakID = Convert.ToInt32(sqlDataReader["RakID"]);
            objPel_T1_Jemur.Rak = (sqlDataReader["Rak"]).ToString();
            objPel_T1_Jemur.Partno = (sqlDataReader["PartNo"]).ToString();
            objPel_T1_Jemur.Lokasi = (sqlDataReader["Lokasi"]).ToString();
            objPel_T1_Jemur.LokasiTF = (sqlDataReader["LokasiTF"]).ToString();
            objPel_T1_Jemur.Palet = (sqlDataReader["NoPAlet"]).ToString();
            }
            catch
            {
            }
            return objPel_T1_Jemur;
        }
        public Pel_T1_Jemur GenerateObjectforserah(SqlDataReader sqlDataReader)
        {
            //try
            //{
                objPel_T1_Jemur = new Pel_T1_Jemur();
                objPel_T1_Jemur.ID = Convert.ToInt32(sqlDataReader["ID"]);
                objPel_T1_Jemur.Flag = Convert.ToInt32(sqlDataReader["Flag"]);
                objPel_T1_Jemur.JemurID = Convert.ToInt32(sqlDataReader["JemurID"]);
                objPel_T1_Jemur.DestID = Convert.ToInt32(sqlDataReader["DestID"]);
                objPel_T1_Jemur.PLine = (sqlDataReader["Line"]).ToString();
                objPel_T1_Jemur.PlantGroup = (sqlDataReader["gp"]).ToString();
                objPel_T1_Jemur.Formula = (sqlDataReader["FormulaCode"]).ToString();
                objPel_T1_Jemur.TglProduksi = Convert.ToDateTime(sqlDataReader["TglProduksi"]);
                objPel_T1_Jemur.TglJemur = Convert.ToDateTime(sqlDataReader["TglJemur"]);
                objPel_T1_Jemur.QtyIn = Convert.ToInt32(sqlDataReader["Qtyin"]);
                objPel_T1_Jemur.QtyOut = Convert.ToInt32(sqlDataReader["QtyOut"]);
                objPel_T1_Jemur.HPP = Convert.ToDecimal(sqlDataReader["HPP"]);
                objPel_T1_Jemur.RakID = Convert.ToInt32(sqlDataReader["RakID"]);
                objPel_T1_Jemur.Rak = (sqlDataReader["Rak"]).ToString();
                objPel_T1_Jemur.Partno = (sqlDataReader["PartNo"]).ToString();
                objPel_T1_Jemur.Lokasi = (sqlDataReader["Lokasi"]).ToString();
                objPel_T1_Jemur.Palet = (sqlDataReader["NoPAlet"]).ToString();
                objPel_T1_Jemur.ItemID = Convert.ToInt32(sqlDataReader["itemid"]);
                objPel_T1_Jemur.LuasA = Convert.ToInt32(sqlDataReader["LuasA"]);
                //objPel_T1_Jemur.Sisa = Convert.ToInt32(sqlDataReader["Sisa"]);
                //objPel_T1_Jemur.Fail = Convert.ToInt32(sqlDataReader["fail"]);
                objPel_T1_Jemur.PartnoAsal = (sqlDataReader["PartNoAsal"]).ToString();
            //}
            //catch
            //{
            //}
            return objPel_T1_Jemur;
        }
        public ArrayList RetrieveCountSheet(string lokasi, string tanggal1, string tanggal2,string nopalet)
        {
            string strSQL = string.Empty;
            string strPalet = string.Empty;
            if (nopalet != string.Empty)
                strPalet = " and palet=" + nopalet;
            if (lokasi == "curing")
                strSQL = "select top 100 null as tgljemurlg,*,(select SUM(qtyin-qtyout) from Pel_T1_Jemur where DestID=A.destid)as sisajemur from (select destid,  itemid0 as itemid,partno,Lokasiid,lokasi,tglproduksi,tgljemur,palet,Rak,sum(Qty) as saldo from vw_kartustockWIPDet  " +
                    "where convert(varchar,tglproduksi,112)>='" + tanggal1 + "' and  convert(varchar,tglproduksi,112)<='" + tanggal2 + "' " +
                    "group by partno,lokasi ,tglproduksi,tgljemur,palet,rak,destid,Lokasiid,itemid0  having sum(Qty)<>0 ) as A  where isnull(tgljemur,'1/1/1900')='1/1/1900' " + strPalet + " order by tglproduksi,palet";
            else
                strSQL = "select  top 100  null as tgljemurlg,*,(select SUM(qtyin-qtyout) from Pel_T1_Jemur where DestID=A.destid)as sisajemur from " +
                    "(select destid,  itemid0 as itemid,partno,Lokasiid,lokasi,tglproduksi,tgljemur,palet,Rak,sum(Qty) as saldo from vw_kartustockWIPDet  " +
               "where convert(varchar,tglproduksi,112)>='" + tanggal1 + "' and  convert(varchar,tglproduksi,112)<='" + tanggal2 + "' " +
               "group by partno,lokasi ,tglproduksi,tgljemur,palet,rak,destid,Lokasiid,itemid0  having sum(Qty)<>0 ) as A  where isnull(tgljemur,'1/1/1900')<>'1/1/1900' " + strPalet + " order by tglproduksi,palet";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPel_T1_Jemur = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPel_T1_Jemur.Add(GenerateObjectForCountsheet(sqlDataReader));
                }
            }
            else
                arrPel_T1_Jemur.Add(new Pel_T1_Jemur());
            return arrPel_T1_Jemur;
        }
        public Pel_T1_Jemur GenerateObjectForCountsheet(SqlDataReader sqlDataReader)
        {
            try
            {
                objPel_T1_Jemur = new Pel_T1_Jemur();
                //objPel_T1_Jemur.ID = Convert.ToInt32(sqlDataReader["ID"]);
                //objPel_T1_Jemur.Flag = Convert.ToInt32(sqlDataReader["Flag"]);
                //objPel_T1_Jemur.JemurID = Convert.ToInt32(sqlDataReader["JemurID"]);
                objPel_T1_Jemur.DestID = Convert.ToInt32(sqlDataReader["DestID"]);
                //objPel_T1_Jemur.PlantGroup = (sqlDataReader["gp"]).ToString();
                objPel_T1_Jemur.LokasiID  = Convert.ToInt32(sqlDataReader["LokasiID"]);
                objPel_T1_Jemur.TglProduksi = Convert.ToDateTime(sqlDataReader["TglProduksi"]);
                objPel_T1_Jemur.Partno = (sqlDataReader["PartNo"]).ToString();
                objPel_T1_Jemur.Lokasi = (sqlDataReader["Lokasi"]).ToString();
                objPel_T1_Jemur.Palet = (sqlDataReader["PAlet"]).ToString();
                objPel_T1_Jemur.QtyIn = Convert.ToInt32(sqlDataReader["saldo"]);
                objPel_T1_Jemur.ItemID = Convert.ToInt32(sqlDataReader["itemid"]);
                objPel_T1_Jemur.Rak = (sqlDataReader["Rak"]).ToString();
                objPel_T1_Jemur.TglJemur = Convert.ToDateTime(sqlDataReader["TglJemur"]);
                objPel_T1_Jemur.QtyOut = Convert.ToInt32(sqlDataReader["sisajemur"]);
                //objPel_T1_Jemur.HPP = Convert.ToDecimal(sqlDataReader["HPP"]);
                //objPel_T1_Jemur.RakID = Convert.ToInt32(sqlDataReader["RakID"]);
                
                //objPel_T1_Jemur.LuasA = Convert.ToInt32(sqlDataReader["LuasA"]);
                //objPel_T1_Jemur.Fail = Convert.ToInt32(sqlDataReader["fail"]);
            }
            catch
            {
            }
            return objPel_T1_Jemur;
        }
        
    }
}
