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
    public class T3_RekapFacade : BusinessFacade.AbstractTransactionFacadeF
    {
        private T3_Rekap objT3_Rekap = new T3_Rekap();
        private ArrayList arrT3_Rekap;
        private List<SqlParameter> sqlListParam;

        
        public T3_RekapFacade(object objDomain)
            : base(objDomain)
        {
            objT3_Rekap = (T3_Rekap)objDomain;
        }
        public T3_RekapFacade()
        {
        }
        public override int Insert1(TransactionManager transManager)
        {
            try
            {
                objT3_Rekap = (T3_Rekap)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Keterangan", objT3_Rekap.Keterangan));
                sqlListParam.Add(new SqlParameter("@DestID", objT3_Rekap.DestID));
                sqlListParam.Add(new SqlParameter("@GroupID", objT3_Rekap.GroupID));
                sqlListParam.Add(new SqlParameter("@T1SLokID", objT3_Rekap.T1SLokID));
                sqlListParam.Add(new SqlParameter("@t1SerahID", objT3_Rekap.T1serahID));
                sqlListParam.Add(new SqlParameter("@SerahID", objT3_Rekap.SerahID));
                sqlListParam.Add(new SqlParameter("@LokID", objT3_Rekap.LokasiID));
                sqlListParam.Add(new SqlParameter("@ItemID", objT3_Rekap.ItemIDSer));
                sqlListParam.Add(new SqlParameter("@TglTrans", objT3_Rekap.TglTrm));
                sqlListParam.Add(new SqlParameter("@QtyIn", objT3_Rekap.QtyInTrm));
                sqlListParam.Add(new SqlParameter("@QtyOut", objT3_Rekap.QtyOutTrm));
                sqlListParam.Add(new SqlParameter("@T1SItemID", objT3_Rekap.T1sItemID));
                sqlListParam.Add(new SqlParameter("@HPP", objT3_Rekap.HPP));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT3_Rekap.CreatedBy));
                sqlListParam.Add(new SqlParameter("@Process", objT3_Rekap.Process));
                sqlListParam.Add(new SqlParameter("@SA", objT3_Rekap.SA));
                sqlListParam.Add(new SqlParameter("@CutID", objT3_Rekap.CutID));
                sqlListParam.Add(new SqlParameter("@CutQty", objT3_Rekap.CutQty));
                sqlListParam.Add(new SqlParameter("@CutLevel", objT3_Rekap.CutLevel));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertT3_Rekap1A");
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
            try
            {
                objT3_Rekap = (T3_Rekap)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Keterangan", objT3_Rekap.Keterangan));
                sqlListParam.Add(new SqlParameter("@DestID", objT3_Rekap.DestID));
                sqlListParam.Add(new SqlParameter("@GroupID", objT3_Rekap.GroupID));
                sqlListParam.Add(new SqlParameter("@T1SLokID", objT3_Rekap.T1SLokID));
                sqlListParam.Add(new SqlParameter("@t1SerahID", objT3_Rekap.T1serahID));
                sqlListParam.Add(new SqlParameter("@SerahID", objT3_Rekap.SerahID));
                sqlListParam.Add(new SqlParameter("@LokID", objT3_Rekap.LokasiID));
                sqlListParam.Add(new SqlParameter("@ItemID", objT3_Rekap.ItemIDSer));
                sqlListParam.Add(new SqlParameter("@TglTrans", objT3_Rekap.TglTrm));
                sqlListParam.Add(new SqlParameter("@QtyIn", objT3_Rekap.QtyInTrm));
                sqlListParam.Add(new SqlParameter("@QtyOut", objT3_Rekap.QtyOutTrm));
                sqlListParam.Add(new SqlParameter("@T1SItemID", objT3_Rekap.T1sItemID));
                sqlListParam.Add(new SqlParameter("@HPP", objT3_Rekap.HPP));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT3_Rekap.CreatedBy));
                sqlListParam.Add(new SqlParameter("@Process", objT3_Rekap.Process));
                sqlListParam.Add(new SqlParameter("@SA", objT3_Rekap.SA));
                sqlListParam.Add(new SqlParameter("@CutID", objT3_Rekap.CutID));
                sqlListParam.Add(new SqlParameter("@CutQty", objT3_Rekap.CutQty));
                sqlListParam.Add(new SqlParameter("@CutLevel", objT3_Rekap.CutLevel));
                sqlListParam.Add(new SqlParameter("@sfrom", objT3_Rekap.Sfrom ));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertT3_Rekaplistplank");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Delete(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }
        public override int Insert(TransactionManager transManager)
        {
            try
            {
                objT3_Rekap = (T3_Rekap)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Keterangan", objT3_Rekap.Keterangan));
                sqlListParam.Add(new SqlParameter("@DestID", objT3_Rekap.DestID));
                sqlListParam.Add(new SqlParameter("@GroupID", objT3_Rekap.GroupID));
                sqlListParam.Add(new SqlParameter("@T1SLokID", objT3_Rekap.T1SLokID ));
                sqlListParam.Add(new SqlParameter("@t1SerahID", objT3_Rekap.T1serahID ));
                sqlListParam.Add(new SqlParameter("@SerahID", objT3_Rekap.SerahID));
                sqlListParam.Add(new SqlParameter("@LokID", objT3_Rekap.LokasiID));
                sqlListParam.Add(new SqlParameter("@ItemID", objT3_Rekap.ItemIDSer));
                sqlListParam.Add(new SqlParameter("@TglTrans", objT3_Rekap.TglTrm));
                sqlListParam.Add(new SqlParameter("@QtyIn", objT3_Rekap.QtyInTrm));
                sqlListParam.Add(new SqlParameter("@QtyOut", objT3_Rekap.QtyOutTrm));
                sqlListParam.Add(new SqlParameter("@T1SItemID", objT3_Rekap.T1sItemID));
                sqlListParam.Add(new SqlParameter("@HPP", objT3_Rekap.HPP ));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT3_Rekap.CreatedBy));
                sqlListParam.Add(new SqlParameter("@Process", objT3_Rekap.Process ));
                sqlListParam.Add(new SqlParameter("@SA", objT3_Rekap.SA ));
                sqlListParam.Add(new SqlParameter("@CutID", objT3_Rekap.CutID));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertT3_Rekap1");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int InsertFromListplank(TransactionManager transManager)
        {
            try
            {
                objT3_Rekap = (T3_Rekap)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Keterangan", objT3_Rekap.Keterangan));
                sqlListParam.Add(new SqlParameter("@DestID", objT3_Rekap.DestID));
                sqlListParam.Add(new SqlParameter("@GroupID", objT3_Rekap.GroupID));
                sqlListParam.Add(new SqlParameter("@T1SLokID", objT3_Rekap.T1SLokID));
                sqlListParam.Add(new SqlParameter("@t1SerahID", objT3_Rekap.T1serahID));
                sqlListParam.Add(new SqlParameter("@SerahID", objT3_Rekap.SerahID));
                sqlListParam.Add(new SqlParameter("@LokID", objT3_Rekap.LokasiID));
                sqlListParam.Add(new SqlParameter("@ItemID", objT3_Rekap.ItemIDSer));
                sqlListParam.Add(new SqlParameter("@TglTrans", objT3_Rekap.TglTrm));
                sqlListParam.Add(new SqlParameter("@QtyIn", objT3_Rekap.QtyInTrm));
                sqlListParam.Add(new SqlParameter("@QtyOut", objT3_Rekap.QtyOutTrm));
                sqlListParam.Add(new SqlParameter("@T1SItemID", objT3_Rekap.T1sItemID));
                sqlListParam.Add(new SqlParameter("@HPP", objT3_Rekap.HPP));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT3_Rekap.CreatedBy));
                sqlListParam.Add(new SqlParameter("@Process", objT3_Rekap.Process));
                sqlListParam.Add(new SqlParameter("@SA", objT3_Rekap.SA));
                sqlListParam.Add(new SqlParameter("@CutID", objT3_Rekap.CutID));
                sqlListParam.Add(new SqlParameter("@Sfrom", objT3_Rekap.Sfrom));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertT3_Rekaplistplank");
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
        
        
        public ArrayList RetrieveByTglSerah(string tglSerah)
        {
            string strSQL = "SELECT     C.ID, C.DestID, G.[Group] AS plantGroup, F.FormulaCode AS formula, BM_Palet.NoPAlet AS palet, IA.PartNo AS partnoDest, "+
                      "IB.PartNo AS partnoSer,A.TglProduksi, B.ID AS serahID, B.TglSerah, LB.Lokasi AS lokasiSer, B.QtyIn as qtyInSer,  "+
                      "B.ItemID as itemIDSer,C.TglTrans as tglTrm, C.LokID as lokasiID, LC.Lokasi as lokasiTrm, C.QtyIn AS qtyInTrm, C.QtyOut as qtyOutTrm "+
                      "FROM BM_Destacking AS A INNER JOIN "+
                      "T1_Serah AS B ON A.ID = B.DestID INNER JOIN "+
                      "T3_Rekap AS C ON B.DestID = C.DestID  AND B.ID = C.T1SerahID INNER JOIN " +
                      "FC_Lokasi AS LB ON B.LokID = LB.ID INNER JOIN "+
                      "FC_Items AS IB ON B.ItemID = IB.ID INNER JOIN "+
                      "FC_Lokasi AS LC ON C.LokID = LC.ID INNER JOIN "+
                      "FC_Items AS IC ON C.ItemID = IC.ID INNER JOIN "+
                      "FC_Items AS IA ON A.ItemID = IA.ID INNER JOIN "+
                      "FC_Lokasi AS LA ON A.LokasiID = LA.ID INNER JOIN "+
                      "BM_Formula AS F ON A.FormulaID = F.ID INNER JOIN "+
                      "BM_PlantGroup AS G ON A.PlantGroupID = G.ID INNER JOIN " +
                      "BM_Palet ON A.PaletID = BM_Palet.ID where B.tglserah='" + tglSerah + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_Rekap = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_Rekap.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT3_Rekap.Add(new T3_Rekap());

            return arrT3_Rekap;
        }
        public ArrayList RetrieveByTglTerima(string tglTerima)
        {
            string strSQL = "SELECT A.ID, A.DestID, A.TglTrans AS tglTrm, A.LokID AS lokasiID, LA.Lokasi AS lokasiTrm, A.QtyIn AS qtyInTrm, A.QtyOut AS qtyOutTrm, "+
                "IA.PartNo AS PartnoTrm, A.Keterangan, A.Process, B.TglSerah, IB.PartNo AS partnoSer, LB.Lokasi as lokasiSer, G.Groups "+
                "from FC_Items AS IB RIGHT OUTER JOIN T1_Serah AS B ON IB.ID = B.itemID0 LEFT OUTER JOIN FC_Lokasi AS LB ON B.LokID = LB.ID RIGHT OUTER JOIN "+
                "T3_GroupM AS G RIGHT OUTER JOIN T3_Rekap AS A ON G.ID = A.GroupID LEFT OUTER JOIN FC_Lokasi AS LA ON A.LokID = LA.ID LEFT OUTER JOIN " +
                "FC_Items AS IA ON A.ItemID = IA.ID ON B.ID = A.T1SerahID  WHERE (A.QtyIn > 0) AND (CONVERT(char(8), A.TglTrans, 112) = '"+ tglTerima + "')";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_Rekap = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_Rekap.Add(GenerateObjectGridTrm(sqlDataReader));
                }
            }
            else
                arrT3_Rekap.Add(new T3_Rekap());

            return arrT3_Rekap;
        }
        public ArrayList RetrieveByTglTerimaLisplankL2(string tglTerima, string likepartno,string palet)
        {
            string strSQL = "select * from (SELECT A.ID, A.DestID,(select tglproduksi from BM_Destacking where ID=A.DestID ) as tglproduksi, " +
                "(select nopalet from BM_Palet where ID in (select PaletID from BM_Destacking where ID=A.DestID))as Palet, A.TglTrans AS tglTrm, A.LokID AS lokasiID, LA.Lokasi AS lokasiTrm, A.QtyIn-A.cutqty AS qtyInTrm, A.QtyOut AS qtyOutTrm, " +
                "IA.PartNo AS PartnoTrm, A.Keterangan, A.Process, B.TglSerah, IB.PartNo AS partnoSer, LB.Lokasi as lokasiSer, G.Groups,A.cutqty,A.cutlevel " +
                "from FC_Items AS IB RIGHT OUTER JOIN T1_Serah AS B ON IB.ID = B.itemID0 LEFT OUTER JOIN FC_Lokasi AS LB ON B.LokID = LB.ID RIGHT OUTER JOIN " +
                "T3_GroupM AS G RIGHT OUTER JOIN T3_Rekap AS A ON G.ID = A.GroupID LEFT OUTER JOIN FC_Lokasi AS LA ON A.LokID = LA.ID LEFT OUTER JOIN " +
                "FC_Items AS IA ON A.ItemID = IA.ID ON B.ID = A.T1SerahID) as A  WHERE process='direct' and A.qtyInTrm-A.cutqty > 0 " + tglTerima + 
                "and A.PartnoTrm like '%" + likepartno + "%'" + palet +" order by tglproduksi,palet";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_Rekap = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_Rekap.Add(GenerateObjectPotong(sqlDataReader));
                }
            }
            else
                arrT3_Rekap.Add(new T3_Rekap());

            return arrT3_Rekap;
        }
        public ArrayList RetrieveByTglTerimaBP(string tglProduksi,string partno,string lokasi)
        {
            string strLokasi = string.Empty;
            if (lokasi!=string.Empty)
             strLokasi ="and  A.Lokasitrm='" + lokasi + "' "  ;
            string strSQL = "select * from (SELECT A.ID, A.DestID,(select tglproduksi from BM_Destacking where ID=A.DestID ) as tglproduksi, " +
                "(select nopalet from BM_Palet where ID in (select PaletID from BM_Destacking where ID=A.DestID))as Palet, A.TglTrans AS tglTrm,"+
                " A.LokID AS lokasiID, LA.Lokasi AS lokasiTrm, A.QtyIn-A.cutqty AS qtyInTrm, A.QtyOut AS qtyOutTrm, " +
                "IA.PartNo AS PartnoTrm, A.Keterangan, A.Process, case when Process='Direct' then B.TglSerah else A.TglTrans end TglSerah,  " +
                "case when Process='Direct' then  IB.PartNo else (select PartNo from FC_Items where ID in (select itemID from T3_Serah where ID=A.SerahID )) end  partnoSer,  " +
                "case when Process='Direct' then  LB.Lokasi else (select Lokasi from FC_Lokasi where ID in (select Lokid from T3_Serah where ID=A.SerahID )) end lokasiSer,  " +
                "isnull(G.Groups,' ') as Groups,A.cutqty,A.cutlevel " +
                "from FC_Items AS IB RIGHT OUTER JOIN T1_Serah AS B ON IB.ID = B.itemID0 LEFT OUTER JOIN FC_Lokasi AS LB ON B.LokID = LB.ID RIGHT OUTER JOIN " +
                "T3_GroupM AS G RIGHT OUTER JOIN T3_Rekap AS A ON G.ID = A.GroupID LEFT OUTER JOIN FC_Lokasi AS LA ON A.LokID = LA.ID LEFT OUTER JOIN " +
                "FC_Items AS IA ON A.ItemID = IA.ID ON B.ID = A.T1SerahID) as A  WHERE A.qtyInTrm - A.cutqty> 0 " + tglProduksi + strLokasi+
                " and A.PartNotrm='" + partno + "' order by tglproduksi,palet";
            //A.PartnoTrm like '%-P-%' " + " and
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_Rekap = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_Rekap.Add(GenerateObjectPotong(sqlDataReader));
                }
            }
            else
                arrT3_Rekap.Add(new T3_Rekap());

            return arrT3_Rekap;
        }
        public ArrayList RetrieveByTglTerimaKS(string tglTerima,string group, string tebal, string lebar, string panjang,string keterangan)
        {
            string strSQL = "SELECT  A.ID, A.DestID, A.TglTrans AS tglTrm, A.LokID AS lokasiID, LA_1.Lokasi AS lokasiTrm, A.QtyIn AS qtyInTrm," +
                    "A.QtyOut AS qtyOutTrm,IA.PartNo as PartnoSer,A.Keterangan, 'trm dari ' + A.Keterangan As Process " +
                    "FROM FC_Lokasi AS LA_1 INNER JOIN T3_Rekap AS A ON LA_1.ID = A.LokID INNER JOIN FC_Items AS IA ON A.ItemID = IA.ID Inner join t3_groupM G on IA.groupid=G.ID " +
                    "WHERE A.qtyin>0 and CONVERT(char(8), A.createdtime  , 112)= '" + tglTerima + "'  and G.groups='" + group + "' and IA.tebal=" + tebal +
                    " and IA.lebar=" + lebar + " and IA.Panjang=" + panjang + " and  ( SUBSTRING(IA.partno,5,1) ='3' or SUBSTRING(IA.partno,5,1) ='W') and A.process='" + keterangan + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_Rekap = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_Rekap.Add(GenerateObjectTrm(sqlDataReader));
                }
            }
            else
                arrT3_Rekap.Add(new T3_Rekap());

            return arrT3_Rekap;
        }
        public ArrayList RetrieveByTglKeluarKS(string tglTerima, string group, string tebal, string lebar, string panjang, string keterangan)
        {
            string strSQL = "SELECT  A.ID, A.DestID, A.TglTrans AS tglTrm, A.LokID AS lokasiID, LA_1.Lokasi AS lokasiTrm, A.QtyIn AS qtyInTrm," +
                    "A.QtyOut AS qtyOutTrm,IA.PartNo as PartnoSer,A.Keterangan, 'trans ke ' + A.Keterangan As Process " +
                    "FROM FC_Lokasi AS LA_1 INNER JOIN T3_Rekap AS A ON LA_1.ID = A.LokID INNER JOIN FC_Items AS IA ON A.ItemID = IA.ID Inner join t3_groupM G on IA.groupid=G.ID " +
                    "WHERE A.qtyin=0 and CONVERT(char(8), A.createdtime  , 112)= '" + tglTerima + "'  and G.groups='" + group + "' and IA.tebal=" + tebal + " and IA.lebar=" + lebar +
                    " and IA.Panjang=" + panjang + " and  ( SUBSTRING(IA.partno,5,1) ='3' or SUBSTRING(IA.partno,5,1) ='W') and A.process='" + keterangan + "' ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_Rekap = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_Rekap.Add(GenerateObjectTrm(sqlDataReader));
                }
            }
            else
                arrT3_Rekap.Add(new T3_Rekap());

            return arrT3_Rekap;
        }
        public ArrayList RetrieveByTglKirimKS(string tglTerima, string group,string tebal,string lebar,string panjang)
        {
            string strSQL = "SELECT  A.ID, 0 as DestID, A.TglTrans AS tglTrm, A.LokID AS lokasiID, LA_1.Lokasi AS lokasiTrm, 0 AS qtyInTrm,A.Qty AS qtyOutTrm, " +
                    "IA.PartNo as PartnoSer, '' as Keterangan,B.sjno as Process FROM FC_Lokasi AS LA_1 INNER JOIN T3_KirimDetail AS A ON LA_1.ID = A.LokID " + 
                    "INNER JOIN FC_Items AS IA ON A.ItemID = IA.ID LEFT OUTER JOIN T3_Kirim AS B ON A.KirimID = B.ID Inner join t3_groupM G on IA.groupid=G.ID " +
                    "WHERE  ( SUBSTRING(IA.partno,5,1) ='3' or SUBSTRING(IA.partno,5,1) ='W') and CONVERT(char(8), A.createdtime  , 112)= '" + tglTerima + "' and G.groups='" + group + "' and IA.tebal=" + tebal + " and IA.lebar=" + lebar + " and IA.Panjang=" + panjang;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_Rekap = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_Rekap.Add(GenerateObjectTrm(sqlDataReader));
                }
            }
            else
                arrT3_Rekap.Add(new T3_Rekap());

            return arrT3_Rekap;
        }
        public ArrayList RetrieveByTglReturKS(string tglRetur, string group, string tebal, string lebar, string panjang)
        {
            string strSQL = "SELECT  A.ID, 0 as DestID, A.TglTrans AS tglTrm, A.LokID AS lokasiID, LA_1.Lokasi AS lokasiTrm, A.Qty AS qtyInTrm,0 AS qtyOutTrm, " +
                    "IA.PartNo as PartnoSer, '' as Keterangan,A.sjno as Process FROM FC_Lokasi AS LA_1 INNER JOIN T3_Retur  AS A ON LA_1.ID = A.LokID  " +
                    "INNER JOIN FC_Items AS IA ON A.ItemID = IA.ID  Inner join t3_groupM G on IA.groupid=G.ID WHERE  CONVERT(char(8), A.createdtime  , 112)= '" +
                    tglRetur + "'  and  ( SUBSTRING(IA.partno,5,1) ='3' or SUBSTRING(IA.partno,5,1) ='W') and G.groups='" + group + "' and IA.tebal=" + tebal + " and IA.lebar=" + lebar + " and IA.Panjang=" + panjang;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_Rekap = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_Rekap.Add(GenerateObjectTrm(sqlDataReader));
                }
            }
            else
                arrT3_Rekap.Add(new T3_Rekap());

            return arrT3_Rekap;
        }
        public string UpdateCutLevel1(int RekapID)
        {
            string strSQL = "update t3_rekap set cutID=0,cutqty=0,cutlevel=1 where ID=" + RekapID;

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }
        public string UpdateCutLevel2(int RekapID,int qty)
        {
            string strSQL = "update t3_rekap set lastmodifiedtime=getdate(), cutqty=cutqty+" + qty + " where ID=" + RekapID;

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }
        public T3_Rekap GenerateObjectTrm(SqlDataReader sqlDataReader)
        {
            objT3_Rekap = new T3_Rekap();
            objT3_Rekap.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objT3_Rekap.DestID = Convert.ToInt32(sqlDataReader["DestID"]);
            objT3_Rekap.PartnoSer = (sqlDataReader["PartnoSer"]).ToString();
            objT3_Rekap.LokasiID = Convert.ToInt32(sqlDataReader["LokasiID"]);
            objT3_Rekap.LokasiTrm = (sqlDataReader["LokasiTrm"]).ToString();
            objT3_Rekap.TglTrm = Convert.ToDateTime(sqlDataReader["TglTrm"]);
            objT3_Rekap.QtyInTrm = Convert.ToInt32(sqlDataReader["QtyInTrm"]);
            objT3_Rekap.QtyOutTrm = Convert.ToInt32(sqlDataReader["QtyOutTrm"]);
            objT3_Rekap.Process = (sqlDataReader["Process"]).ToString();
            objT3_Rekap.Keterangan = (sqlDataReader["Keterangan"]).ToString();
           // objT3_Rekap.Groups = (sqlDataReader["Groups"]).ToString();
            return objT3_Rekap;
        }
        public T3_Rekap GenerateObjectGridTrm(SqlDataReader sqlDataReader)
        {
            try
            {
                objT3_Rekap = new T3_Rekap();

                objT3_Rekap.ID = Convert.ToInt32(sqlDataReader["ID"]);
                objT3_Rekap.DestID = Convert.ToInt32(sqlDataReader["DestID"]);
                objT3_Rekap.TglSerah = Convert.ToDateTime(sqlDataReader["TglSerah"]);
                objT3_Rekap.LokasiSer = (sqlDataReader["LokasiSer"]).ToString();
                objT3_Rekap.PartnoSer = (sqlDataReader["PartnoSer"]).ToString();
                objT3_Rekap.PartnoTrm = (sqlDataReader["PartnoTrm"]).ToString();
                objT3_Rekap.LokasiID = Convert.ToInt32(sqlDataReader["LokasiID"]);
                objT3_Rekap.LokasiTrm = (sqlDataReader["LokasiTrm"]).ToString();
                objT3_Rekap.TglTrm = Convert.ToDateTime(sqlDataReader["TglTrm"]);
                objT3_Rekap.QtyInTrm = Convert.ToInt32(sqlDataReader["QtyInTrm"]);
                objT3_Rekap.QtyOutTrm = Convert.ToInt32(sqlDataReader["QtyOutTrm"]);
                objT3_Rekap.Process = (sqlDataReader["Process"]).ToString();
                objT3_Rekap.Keterangan = (sqlDataReader["Keterangan"]).ToString();
                objT3_Rekap.Groups = (sqlDataReader["Groups"]).ToString();
                
            }
            catch
            { }
            return objT3_Rekap;
        }
        public T3_Rekap GenerateObjectPotong(SqlDataReader sqlDataReader)
        {
            try
            {
                objT3_Rekap = new T3_Rekap();
                objT3_Rekap.ID = Convert.ToInt32(sqlDataReader["ID"]);
                objT3_Rekap.DestID = Convert.ToInt32(sqlDataReader["DestID"]);
                objT3_Rekap.TglProduksi = Convert.ToDateTime(sqlDataReader["TglProduksi"]);
                objT3_Rekap.TglSerah = Convert.ToDateTime(sqlDataReader["TglSerah"]);
                objT3_Rekap.Palet = (sqlDataReader["Palet"]).ToString();
                objT3_Rekap.LokasiSer = (sqlDataReader["LokasiSer"]).ToString();
                objT3_Rekap.PartnoSer = (sqlDataReader["PartnoSer"]).ToString();
                objT3_Rekap.PartnoTrm = (sqlDataReader["PartnoTrm"]).ToString();
                objT3_Rekap.LokasiID = Convert.ToInt32(sqlDataReader["LokasiID"]);
                objT3_Rekap.LokasiTrm = (sqlDataReader["LokasiTrm"]).ToString();
                objT3_Rekap.TglTrm = Convert.ToDateTime(sqlDataReader["TglTrm"]);
                objT3_Rekap.QtyInTrm = Convert.ToInt32(sqlDataReader["QtyInTrm"]);
                objT3_Rekap.QtyOutTrm = Convert.ToInt32(sqlDataReader["QtyOutTrm"]);
                objT3_Rekap.CutLevel = Convert.ToInt32(sqlDataReader["CutLevel"]);
                objT3_Rekap.Process = (sqlDataReader["Process"]).ToString();
                objT3_Rekap.Keterangan = (sqlDataReader["Keterangan"]).ToString();
                objT3_Rekap.Groups = (sqlDataReader["Groups"]).ToString();

            }
            catch
            { }
            return objT3_Rekap;
        }
        public T3_Rekap GenerateObject(SqlDataReader sqlDataReader)
        {
            objT3_Rekap = new T3_Rekap();
            objT3_Rekap.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objT3_Rekap.DestID = Convert.ToInt32(sqlDataReader["DestID"]);
            objT3_Rekap.PlantGroup = (sqlDataReader["PlantGroup"]).ToString();
            objT3_Rekap.Formula = (sqlDataReader["Formula"]).ToString();
            objT3_Rekap.Palet = (sqlDataReader["Palet"]).ToString();
            objT3_Rekap.PartnoDest = (sqlDataReader["PartnoDest"]).ToString();
            objT3_Rekap.TglProduksi = Convert.ToDateTime(sqlDataReader["TglProduksi"]);
            objT3_Rekap.PartnoSer = (sqlDataReader["PartnoSer"]).ToString();
            objT3_Rekap.SerahID = Convert.ToInt32(sqlDataReader["SerahID"]);
            objT3_Rekap.ItemIDSer = Convert.ToInt32(sqlDataReader["ItemIDSer"]);
            objT3_Rekap.TglSerah = Convert.ToDateTime(sqlDataReader["TglSerah"]);
            objT3_Rekap.LokasiSer = (sqlDataReader["LokasiSer"]).ToString();
            objT3_Rekap.QtyInSer = Convert.ToInt32(sqlDataReader["QtyInSer"]);
            objT3_Rekap.LokasiID = Convert.ToInt32(sqlDataReader["LokasiID"]);
            objT3_Rekap.LokasiTrm = (sqlDataReader["LokasiTrm"]).ToString();
            objT3_Rekap.TglTrm = Convert.ToDateTime(sqlDataReader["TglTrm"]);
            objT3_Rekap.QtyInTrm = Convert.ToInt32(sqlDataReader["QtyInTrm"]);
            objT3_Rekap.QtyOutTrm = Convert.ToInt32(sqlDataReader["QtyOutTrm"]);
            return objT3_Rekap;
        }


    }
}
