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
    public class T1_SerahFacade : BusinessFacade.AbstractTransactionFacadeF
    {
        private T1_Serah objT1_Serah = new T1_Serah();
        private ArrayList arrT1_Serah;
        private List<SqlParameter> sqlListParam;

        public T1_SerahFacade(object objDomain)
            : base(objDomain)
        {
            objT1_Serah = (T1_Serah)objDomain;
        }
        public T1_SerahFacade()
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
        public override int Delete(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }
        public override int Insert(TransactionManager transManager)
        {
            try
            {
                objT1_Serah = (T1_Serah)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@DestID", objT1_Serah.DestID));
                sqlListParam.Add(new SqlParameter("@JemurID", objT1_Serah.JemurID));
                sqlListParam.Add(new SqlParameter("@LokID", objT1_Serah.LokasiID ));
                sqlListParam.Add(new SqlParameter("@ItemID0", objT1_Serah.ItemIDDest ));
                sqlListParam.Add(new SqlParameter("@ItemID", objT1_Serah.ItemIDSer));
                sqlListParam.Add(new SqlParameter("@TglSerah", objT1_Serah.TglSerah));
                sqlListParam.Add(new SqlParameter("@QtyIn", objT1_Serah.QtyIn));
                sqlListParam.Add(new SqlParameter("@HPP", objT1_Serah.HPP ));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT1_Serah.CreatedBy));
                sqlListParam.Add(new SqlParameter("@SFrom", objT1_Serah.SFrom));
                sqlListParam.Add(new SqlParameter("@Toven", objT1_Serah.Oven));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertT1_SerahWithOven");
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
            //try
            //{
                objT1_Serah = (T1_Serah)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objT1_Serah.ID));
                sqlListParam.Add(new SqlParameter("@QtyOut", objT1_Serah.QtyOut));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT1_Serah.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateQtyT1_Serah");
                strError = transManager.Error;
                return intResult;
            //}
            //catch (Exception ex)
            //{
            //    strError = ex.Message;
            //    return -1;
            //}
        }

        public ArrayList RetrieveByTglProduksi(string tglProduksi,string lokasi, string listBy)
        {
            string strSQL=string.Empty;
            if (listBy=="produksi")
             strSQL = "SELECT  A.ID AS DestID, ' ' AS tgljemur, ' ' AS rak, A.TglProduksi, A1.Lokasi AS Lokasidest, A5.PartNo AS PartNodest, C.ID, C.ItemID AS ItemIDSer, "+
                    "C.TglSerah,C2.PartNo AS PartNoser, C1.Lokasi AS Lokasiser, C.QtyIn AS Qtysin, C.QtyOut AS Qtysout, ISNULL(C.HPP, 0) AS HPP, BM_Palet.NoPAlet "+
                    "FROM BM_Destacking AS A LEFT OUTER JOIN FC_Items AS A5 ON A.ItemID = A5.ID LEFT OUTER JOIN FC_Lokasi AS A1 ON A.LokasiID = A1.ID LEFT OUTER JOIN "+
                    "BM_Palet ON A.PaletID = BM_Palet.ID RIGHT OUTER JOIN FC_Lokasi AS C1 RIGHT OUTER JOIN T1_Serah AS C ON C1.ID = C.LokID LEFT OUTER JOIN "+
                    "FC_Items AS C2 ON C.ItemID = C2.ID ON A.ID = C.DestID "+
                    "WHERE  C.QtyIn > C.QtyOut and " + lokasi + " C.status >-1 and convert(varchar,A.TglProduksi,112) ='" + tglProduksi + "' order by A5.PartNo,BM_Palet.NoPAlet ";
            else
                strSQL = "SELECT   A.ID AS DestID, ' ' AS tgljemur, ' ' AS rak, A.TglProduksi, A1.Lokasi AS Lokasidest, A5.PartNo AS PartNodest, C.ID, C.ItemID AS ItemIDSer, " +
                    "C.TglSerah,C2.PartNo AS PartNoser, C1.Lokasi AS Lokasiser, C.QtyIn AS Qtysin, C.QtyOut AS Qtysout, ISNULL(C.HPP, 0) AS HPP, BM_Palet.NoPAlet " +
                    "FROM BM_Destacking AS A LEFT OUTER JOIN FC_Items AS A5 ON A.ItemID = A5.ID LEFT OUTER JOIN FC_Lokasi AS A1 ON A.LokasiID = A1.ID LEFT OUTER JOIN " +
                    "BM_Palet ON A.PaletID = BM_Palet.ID RIGHT OUTER JOIN FC_Lokasi AS C1 RIGHT OUTER JOIN T1_Serah AS C ON C1.ID = C.LokID LEFT OUTER JOIN " +
                    "FC_Items AS C2 ON C.ItemID = C2.ID ON A.ID = C.DestID " +
                    "WHERE  " + lokasi + "C.status >-1 and   C.QtyIn > C.QtyOut and convert(varchar,C.TglSerah,112)='" + tglProduksi + "' order by A5.PartNo,C.ID ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_Serah.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT1_Serah.Add(new T1_Serah());

            return arrT1_Serah;
        }

        //public ArrayList RetrieveByTglSerah(string tglSerah)
        //{
        //    string strSQL = "SELECT  A.ID AS DestID, A.TglProduksi, A3.FormulaCode AS Formula, A5.PartNo AS PartNodest, C.ID, C.ItemID AS ItemIDSer, C.TglSerah, C2.PartNo AS PartNoser, "+
        //        "C1.Lokasi AS Lokasiser, C.QtyIn AS Qtysin, C.QtyOut AS Qtysout,isnull(C.HPP,0) as HPP  FROM  BM_Formula AS A3 INNER JOIN BM_Destacking AS A ON A3.ID = A.FormulaID " +
        //        "INNER JOIN FC_Items AS A5 ON A.ItemID = A5.ID INNER JOIN FC_Items AS C2 INNER JOIN T1_Serah AS C ON C2.ID = C.ItemID INNER JOIN " +
        //        "FC_Lokasi AS C1 ON C.LokID = C1.ID ON A.ID = C.DestID WHERE C1.Lokasi='H28' and C.QtyIn > C.QtyOut AND C.TglSerah = '" + tglSerah + "'";
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        //    strError = dataAccess.Error;
        //    arrT1_Serah = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrT1_Serah.Add(GenerateObjectSerah(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrT1_Serah.Add(new T1_Serah());

        //    return arrT1_Serah;
        //}
        public ArrayList RetrieveByTglProduksiAll(string tglProduksi, string lokasi, string listBy)
        {
            string strSQL = string.Empty;
            if (listBy == "produksi")
                strSQL = "SELECT A.ID AS DestID, ' ' AS tgljemur, ' ' AS rak, A.TglProduksi, A1.Lokasi AS Lokasidest, A5.PartNo AS PartNodest, C.ID, C.ItemID AS ItemIDSer, " +
                       "C.TglSerah,C2.PartNo AS PartNoser, C1.Lokasi AS Lokasiser, C.QtyIn AS Qtysin, C.QtyOut AS Qtysout, ISNULL(C.HPP, 0) AS HPP, BM_Palet.NoPAlet " +
                       "FROM BM_Destacking AS A LEFT OUTER JOIN FC_Items AS A5 ON A.ItemID = A5.ID LEFT OUTER JOIN FC_Lokasi AS A1 ON A.LokasiID = A1.ID LEFT OUTER JOIN " +
                       "BM_Palet ON A.PaletID = BM_Palet.ID RIGHT OUTER JOIN FC_Lokasi AS C1 RIGHT OUTER JOIN T1_Serah AS C ON C1.ID = C.LokID LEFT OUTER JOIN " +
                       "FC_Items AS C2 ON C.ItemID = C2.ID ON A.ID = C.DestID " +
                       "WHERE   " + lokasi + " C.status >-1 and convert(varchar,A.TglProduksi,112) ='" + tglProduksi + "' order by A5.PartNo,BM_Palet.NoPAlet ";
            else
                strSQL = "SELECT  A.ID AS DestID, ' ' AS tgljemur, ' ' AS rak, A.TglProduksi, A1.Lokasi AS Lokasidest, A5.PartNo AS PartNodest, C.ID, C.ItemID AS ItemIDSer, " +
                    "C.TglSerah,C2.PartNo AS PartNoser, C1.Lokasi AS Lokasiser, C.QtyIn AS Qtysin, C.QtyOut AS Qtysout, ISNULL(C.HPP, 0) AS HPP, BM_Palet.NoPAlet " +
                    "FROM BM_Destacking AS A LEFT OUTER JOIN FC_Items AS A5 ON A.ItemID = A5.ID LEFT OUTER JOIN FC_Lokasi AS A1 ON A.LokasiID = A1.ID LEFT OUTER JOIN " +
                    "BM_Palet ON A.PaletID = BM_Palet.ID RIGHT OUTER JOIN FC_Lokasi AS C1 RIGHT OUTER JOIN T1_Serah AS C ON C1.ID = C.LokID LEFT OUTER JOIN " +
                    "FC_Items AS C2 ON C.ItemID = C2.ID ON A.ID = C.DestID " +
                    "WHERE  " + lokasi + "C.status >-1 and    convert(varchar,C.TglSerah,112)='" + tglProduksi + "' order by C.ID ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_Serah.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT1_Serah.Add(new T1_Serah());

            return arrT1_Serah;
        }
       
        public ArrayList RetrieveForDefect(string tanggal,  string listBy)
        {
            string strSQL = string.Empty;
            if (listBy == "produksi")
                strSQL = "SELECT  A.ID AS DestID, ' ' AS tgljemur, ' ' AS rak, A.TglProduksi, A1.Lokasi AS Lokasidest, A5.PartNo AS PartNodest, C.ID, C.ItemID AS ItemIDSer, " +
                   "C.TglSerah, C2.PartNo as PartNoser,case when C.sfrom='lari' then 'P99' else C1.Lokasi end Lokasiser, C.QtyIn AS Qtysin, C.QtyOut AS Qtysout, ISNULL(C.HPP, 0) AS HPP, BM_Palet.NoPAlet " +
                   "FROM BM_Destacking AS A LEFT OUTER JOIN FC_Items AS A5 ON A.ItemID = A5.ID LEFT OUTER JOIN FC_Lokasi AS A1 ON A.LokasiID = A1.ID LEFT OUTER JOIN " +
                   "BM_Palet ON A.PaletID = BM_Palet.ID RIGHT OUTER JOIN FC_Lokasi AS C1 RIGHT OUTER JOIN T1_Serah AS C ON C1.ID = C.LokID LEFT OUTER JOIN " +
                   "FC_Items AS C2 ON C.ItemID = C2.ID ON A.ID = C.DestID " +
                   "WHERE (C2.partno like '%-P-%') and C.sfrom !='lari' and C.status =0 and    convert(varchar,A.TglProduksi,112)='" + tanggal + "' order by BM_Palet.NoPAlet ";
            else
                strSQL = "select * from (SELECT  A.ID AS DestID, ' ' AS tgljemur, ' ' AS rak, A.TglProduksi, A1.Lokasi AS Lokasidest, A5.PartNo AS PartNodest, C.ID, C.ItemID AS ItemIDSer, " +
                    "C.TglSerah, C2.PartNo as PartNoser, case when C.sfrom='lari' then 'P99' else C1.Lokasi end Lokasiser, C.QtyIn AS Qtysin, C.QtyOut AS Qtysout, ISNULL(C.HPP, 0) AS HPP, BM_Palet.NoPAlet " +
                    "FROM BM_Destacking AS A LEFT OUTER JOIN FC_Items AS A5 ON A.ItemID = A5.ID LEFT OUTER JOIN FC_Lokasi AS A1 ON A.LokasiID = A1.ID LEFT OUTER JOIN " +
                    "BM_Palet ON A.PaletID = BM_Palet.ID RIGHT OUTER JOIN FC_Lokasi AS C1 RIGHT OUTER JOIN T1_Serah AS C ON C1.ID = C.LokID LEFT OUTER JOIN " +
                    "FC_Items AS C2 ON C.ItemID = C2.ID ON A.ID = C.DestID " +
                    "WHERE (C2.partno like '%-P-%' ) and  C.status =0 and    convert(varchar,C.TglSerah,112)='" + tanggal + "') A where Lokasiser='B99' union all " +
                    "select * from (SELECT  A.ID AS DestID, ' ' AS tgljemur, ' ' AS rak, A.TglProduksi, A1.Lokasi AS Lokasidest, A5.PartNo AS PartNodest, C.ID, C.ItemID AS ItemIDSer, " +
                    "C.TglSerah, C2.PartNo as PartNoser, case when C.sfrom='lari' then 'P99' else C1.Lokasi end Lokasiser, C.QtyIn AS Qtysin, C.QtyOut AS Qtysout, ISNULL(C.HPP, 0) AS HPP, BM_Palet.NoPAlet " +
                    "FROM BM_Destacking AS A LEFT OUTER JOIN FC_Items AS A5 ON A.ItemID = A5.ID LEFT OUTER JOIN FC_Lokasi AS A1 ON A.LokasiID = A1.ID LEFT OUTER JOIN " +
                    "BM_Palet ON A.PaletID = BM_Palet.ID RIGHT OUTER JOIN FC_Lokasi AS C1 RIGHT OUTER JOIN T1_Serah AS C ON C1.ID = C.LokID LEFT OUTER JOIN " +
                    "FC_Items AS C2 ON C.ItemID = C2.ID ON A.ID = C.DestID " +
                    "WHERE (C2.partno like '%-P-%' ) and C2.Tebal>=8  and  C.status =0 and    convert(varchar,C.TglSerah,112)='" + tanggal + "') A where  Lokasiser='I99' order by NoPAlet ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_Serah.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT1_Serah.Add(new T1_Serah());

            return arrT1_Serah;
        }

        public ArrayList RetrieveListplankForDefect(string tanggal,string partno)
        {
            string strSQL = string.Empty;
            strSQL = "select * from (SELECT  A.ID AS DestID, ' ' AS tgljemur, ' ' AS rak, A.TglProduksi, A1.Lokasi AS Lokasidest, A5.PartNo AS PartNodest, " +
	        "C.ID, C.ItemID AS ItemIDSer, C.Tgltrans TglSerah, C2.PartNo as PartNoser, C1.Lokasi  Lokasiser,  " +
	        "C.QtyIn AS Qtysin, C.QtyOut AS Qtysout, ISNULL(C.HPP, 0) AS HPP, BM_Palet.NoPAlet  " +
	        "FROM BM_Destacking AS A left JOIN FC_Items AS A5 ON A.ItemID = A5.ID left JOIN FC_Lokasi AS A1 ON A.LokasiID = A1.ID left JOIN  " +
	        "BM_Palet ON A.PaletID = BM_Palet.ID left JOIN FC_Lokasi AS C1 RIGHT OUTER JOIN T3_Rekap AS C ON C1.ID = C.LokID left JOIN  " +
            "FC_Items AS C2 ON C.ItemID = C2.ID ON A.ID = C.DestID  " +
            "WHERE (C2.partno like '%-P-%' ) and  C.status =0  and ISNULL(C.sfrom,'')<>'' and  C.rowstatus =0 and convert(varchar,C.Tgltrans,112)='" + tanggal + "') A where PartNoser='" + partno + "' order by NoPAlet";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_Serah.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT1_Serah.Add(new T1_Serah());

            return arrT1_Serah;
        }
        public Int32 RetrieveTBPListplank(string tanggal, string partno)
        {
            string strSQL = string.Empty;
            int total = 0;
            strSQL = "select isnull(sum(Qtysin),0) as Qtysin from (SELECT  A.ID AS DestID, ' ' AS tgljemur, ' ' AS rak, A.TglProduksi, A1.Lokasi AS Lokasidest, A5.PartNo AS PartNodest, " +
            "C.ID, C.ItemID AS ItemIDSer, C.Tgltrans TglSerah, C2.PartNo as PartNoser, C1.Lokasi  Lokasiser,  " +
            "C.QtyIn AS Qtysin, C.QtyOut AS Qtysout, ISNULL(C.HPP, 0) AS HPP, BM_Palet.NoPAlet  " +
            "FROM BM_Destacking AS A left JOIN FC_Items AS A5 ON A.ItemID = A5.ID left JOIN FC_Lokasi AS A1 ON A.LokasiID = A1.ID left JOIN  " +
            "BM_Palet ON A.PaletID = BM_Palet.ID left JOIN FC_Lokasi AS C1 RIGHT OUTER JOIN T3_Rekap AS C ON C1.ID = C.LokID left JOIN  " +
            "FC_Items AS C2 ON C.ItemID = C2.ID ON A.ID = C.DestID  " +
            "WHERE (C2.partno like '%-P-%' ) and  C.status =0  and ISNULL(C.sfrom,'')<>'' and  C.rowstatus =0 and convert(varchar,C.Tgltrans,112)='" + tanggal + "') A where PartNoser='" + partno + "' ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    total = Convert.ToInt32(sqlDataReader["QtysIn"]);
                }
            }
            return total;
        }
        public Int32 RetrieveTPotongListplank(string tanggal, string partno)
        {
            string strSQL = string.Empty;
            int total = 0;
            strSQL = "declare @partno varchar(25) " +
            "set @partno='" + partno + "' " +
            "select  isnull(sum(Qtysin),0) as Qtysin  from (SELECT  A.ID AS DestID, ' ' AS tgljemur, ' ' AS rak, A.TglProduksi, A1.Lokasi AS Lokasidest,  " +
            "A5.PartNo AS PartNodest, C.ID, C.ItemID AS ItemIDSer, C.Tgltrans TglSerah, C2.PartNo as PartNoser, C1.Lokasi  Lokasiser,  C.QtyIn AS Qtysin,  " +
            "C.QtyOut AS Qtysout, ISNULL(C.HPP, 0) AS HPP, BM_Palet.NoPAlet  FROM BM_Destacking AS A left JOIN FC_Items AS A5 ON A.ItemID = A5.ID  " +
            "left JOIN FC_Lokasi AS A1 ON A.LokasiID = A1.ID left JOIN  BM_Palet ON A.PaletID = BM_Palet.ID left JOIN FC_Lokasi AS C1  " +
            "RIGHT OUTER JOIN T3_Rekap AS C ON C1.ID = C.LokID left JOIN  FC_Items AS C2 ON C.ItemID = C2.ID ON A.ID = C.DestID   " +
            "WHERE ISNULL(C.sfrom,'')<>'' and C.rowstatus =0 and convert(varchar,C.Tgltrans,112)='20161104') A  " +
            "where PartNoser like'%'+ SUBSTRING(@partno,7,11) +'%' and PartNoser like'%'+ SUBSTRING(@partno,0,3) +'%'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    total=Convert.ToInt32(sqlDataReader["QtysIn"]);
                }
            }
            return total;
        }

        public ArrayList RetrievePartnoListplankForDefect(string tanggal)
        {
            string strSQL = string.Empty;
            strSQL = "select distinct partnoser from (SELECT  A.ID AS DestID, ' ' AS tgljemur, ' ' AS rak, A.TglProduksi, A1.Lokasi AS Lokasidest, A5.PartNo AS PartNodest, " +
            "C.ID, C.ItemID AS ItemIDSer, C.Tgltrans TglSerah, C2.PartNo as PartNoser, C1.Lokasi  Lokasiser,  " +
            "C.QtyIn AS Qtysin, C.QtyOut AS Qtysout, ISNULL(C.HPP, 0) AS HPP, BM_Palet.NoPAlet  " +
            "FROM BM_Destacking AS A left JOIN FC_Items AS A5 ON A.ItemID = A5.ID left JOIN FC_Lokasi AS A1 ON A.LokasiID = A1.ID left JOIN  " +
            "BM_Palet ON A.PaletID = BM_Palet.ID left JOIN FC_Lokasi AS C1 RIGHT OUTER JOIN T3_Rekap AS C ON C1.ID = C.LokID left JOIN  " +
            "FC_Items AS C2 ON C.ItemID = C2.ID ON A.ID = C.DestID  " +
            "WHERE (C2.partno like '%-P-%' ) and  C.status =0 and  C.rowstatus =0 and ISNULL(C.sfrom,'')<>'' and convert(varchar,C.Tgltrans,112)='" + tanggal + "') A  order by partnoser ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_Serah.Add(GenerateObjectPartnoPelarian(sqlDataReader));
                }
            }
            else
                arrT1_Serah.Add(new T1_Serah());

            return arrT1_Serah;
        }
        public ArrayList RetrievePartNoPelarianForDefect(string tanggal)
        {
            string strSQL = string.Empty;
            strSQL = "select distinct partnoser from ( " +
                "SELECT  A.ID AS DestID, ' ' AS tgljemur, ' ' AS rak, A.TglProduksi, A1.Lokasi AS Lokasidest, A5.PartNo AS PartNodest, C.ID, C.ItemID AS ItemIDSer,  " +
                "C.TglSerah, C2.PartNo as PartNoser, case when C.sfrom='lari' then 'P99' else C1.Lokasi end Lokasiser, C.QtyIn AS Qtysin, C.QtyOut AS Qtysout,  " +
                "ISNULL(C.HPP, 0) AS HPP, BM_Palet.NoPAlet  " +
                "FROM BM_Destacking AS A LEFT OUTER JOIN FC_Items AS A5 ON A.ItemID = A5.ID LEFT OUTER JOIN FC_Lokasi AS A1 ON A.LokasiID = A1.ID LEFT OUTER JOIN  " +
                "BM_Palet ON A.PaletID = BM_Palet.ID RIGHT OUTER JOIN FC_Lokasi AS C1 RIGHT OUTER JOIN T1_Serah AS C ON C1.ID = C.LokID LEFT OUTER JOIN  " +
                "FC_Items AS C2 ON C.ItemID = C2.ID ON A.ID = C.DestID  " +
                "WHERE (C2.partno like '%-P-%' ) and  C.status =0 and convert(varchar,C.TglSerah,112)='"+tanggal+"') A where Lokasiser='p99' order by partnoser ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_Serah.Add(GenerateObjectPartnoPelarian(sqlDataReader));
                }
            }
            else
                arrT1_Serah.Add(new T1_Serah());

            return arrT1_Serah;
        }
        public ArrayList RetrievePartNoListplankForDefect(string tanggal)
        {
            string strSQL = string.Empty;
            strSQL = "select distinct partnoser from ( " +
                "SELECT  A.ID AS DestID, ' ' AS tgljemur, ' ' AS rak, A.TglProduksi, A1.Lokasi AS Lokasidest, A5.PartNo AS PartNodest, C.ID, C.ItemID AS ItemIDSer,  " +
                "C.TglSerah, C2.PartNo as PartNoser, case when C.sfrom='lari' then 'P99' else C1.Lokasi end Lokasiser, C.QtyIn AS Qtysin, C.QtyOut AS Qtysout,  " +
                "ISNULL(C.HPP, 0) AS HPP, BM_Palet.NoPAlet  " +
                "FROM BM_Destacking AS A LEFT OUTER JOIN FC_Items AS A5 ON A.ItemID = A5.ID LEFT OUTER JOIN FC_Lokasi AS A1 ON A.LokasiID = A1.ID LEFT OUTER JOIN  " +
                "BM_Palet ON A.PaletID = BM_Palet.ID RIGHT OUTER JOIN FC_Lokasi AS C1 RIGHT OUTER JOIN T1_Serah AS C ON C1.ID = C.LokID LEFT OUTER JOIN  " +
                "FC_Items AS C2 ON C.ItemID = C2.ID ON A.ID = C.DestID  " +
                "WHERE (C2.partno like '%-P-%' ) and  C.status =0 and convert(varchar,C.TglSerah,112)='" + tanggal + "') A where Lokasiser='p99' order by partnoser ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_Serah.Add(GenerateObjectPartnoPelarian(sqlDataReader));
                }
            }
            else
                arrT1_Serah.Add(new T1_Serah());

            return arrT1_Serah;
        }

        public ArrayList RetrievePelarianForDefect(string tanggal,string partno)
        {
            string strSQL = string.Empty;
            strSQL = "select * from ( " +
                "SELECT  A.ID AS DestID, ' ' AS tgljemur, ' ' AS rak, A.TglProduksi, A1.Lokasi AS Lokasidest, A5.PartNo AS PartNodest,  " +
                "C.ID, C.ItemID AS ItemIDSer,C.TglSerah, C2.PartNo as PartNoser, case when C.sfrom='lari' then 'P99' else C1.Lokasi end Lokasiser,  " +
                "C.QtyIn AS Qtysin, C.QtyOut AS Qtysout, ISNULL(C.HPP, 0) AS HPP, BM_Palet.NoPAlet FROM BM_Destacking AS A LEFT OUTER JOIN  " +
                "FC_Items AS A5 ON A.ItemID = A5.ID LEFT OUTER JOIN FC_Lokasi AS A1 ON A.LokasiID = A1.ID LEFT OUTER JOIN  " +
                "BM_Palet ON A.PaletID = BM_Palet.ID RIGHT OUTER JOIN FC_Lokasi AS C1 RIGHT OUTER JOIN T1_Serah AS C ON C1.ID = C.LokID LEFT OUTER JOIN  " +
                "FC_Items AS C2 ON C.ItemID = C2.ID ON A.ID = C.DestID  " +
                "WHERE (C2.partno like '%-P-%' ) and  C.status =0 and convert(varchar,C.TglSerah,112)='" + tanggal +
                "') A where Lokasiser='p99' and partnoser='"+partno+"' order by partnoser";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_Serah.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT1_Serah.Add(new T1_Serah());

            return arrT1_Serah;
        }

        public ArrayList RetrieveForDefectPalet(string tanggal, string listBy,string palet)
        {
            string strSQL = string.Empty;
            if (listBy == "produksi")
                strSQL = "SELECT  A.ID AS DestID, ' ' AS tgljemur, ' ' AS rak, A.TglProduksi, A1.Lokasi AS Lokasidest, A5.PartNo AS PartNodest, C.ID, C.ItemID AS ItemIDSer, " +
                   "C.TglSerah, C2.PartNo as PartNoser,case when C.sfrom='lari' then 'P99' else C1.Lokasi end Lokasiser, C.QtyIn AS Qtysin, C.QtyOut AS Qtysout, ISNULL(C.HPP, 0) AS HPP, BM_Palet.NoPAlet " +
                   "FROM BM_Destacking AS A LEFT OUTER JOIN FC_Items AS A5 ON A.ItemID = A5.ID LEFT OUTER JOIN FC_Lokasi AS A1 ON A.LokasiID = A1.ID LEFT OUTER JOIN " +
                   "BM_Palet ON A.PaletID = BM_Palet.ID RIGHT OUTER JOIN FC_Lokasi AS C1 RIGHT OUTER JOIN T1_Serah AS C ON C1.ID = C.LokID LEFT OUTER JOIN " +
                   "FC_Items AS C2 ON C.ItemID = C2.ID ON A.ID = C.DestID " +
                   "WHERE (C2.partno like '%-P-%' or or C.sfrom='lari') and C.status =0 and convert(varchar,A.TglProduksi,112)='" + tanggal + "' and BM_Palet.NoPAlet='"+ palet +"'  order by BM_Palet.NoPAlet ";
            else
                strSQL = "SELECT  A.ID AS DestID, ' ' AS tgljemur, ' ' AS rak, A.TglProduksi, A1.Lokasi AS Lokasidest, A5.PartNo AS PartNodest, C.ID, C.ItemID AS ItemIDSer, " +
                    "C.TglSerah, C2.PartNo as PartNoser, case when C.sfrom='lari' then 'P99' else C1.Lokasi end Lokasiser, C.QtyIn AS Qtysin, C.QtyOut AS Qtysout, ISNULL(C.HPP, 0) AS HPP, BM_Palet.NoPAlet " +
                    "FROM BM_Destacking AS A LEFT OUTER JOIN FC_Items AS A5 ON A.ItemID = A5.ID LEFT OUTER JOIN FC_Lokasi AS A1 ON A.LokasiID = A1.ID LEFT OUTER JOIN " +
                    "BM_Palet ON A.PaletID = BM_Palet.ID RIGHT OUTER JOIN FC_Lokasi AS C1 RIGHT OUTER JOIN T1_Serah AS C ON C1.ID = C.LokID LEFT OUTER JOIN " +
                    "FC_Items AS C2 ON C.ItemID = C2.ID ON A.ID = C.DestID " +
                    "WHERE (C2.partno like '%-P-%' ) and C.status =0 and convert(varchar,C.TglSerah,112)='" + tanggal + "' and BM_Palet.NoPAlet='" + palet + "' and (C1.Lokasi='B99' or C1.Lokasi='I99')  order by BM_Palet.NoPAlet ";
            //"WHERE (C2.partno like '%-P-%' or C.sfrom='lari') and C.status =0 and    convert(varchar,C.TglSerah,112)='" + tanggal + "' and BM_Palet.NoPAlet='" + palet + "' order by BM_Palet.NoPAlet ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_Serah.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT1_Serah.Add(new T1_Serah());

            return arrT1_Serah;
        }

        public ArrayList RetrieveForDefectPel(string tanggal, string listBy)
        {
            string strSQL = string.Empty;

            strSQL = "SELECT 0 DestID, ' ' AS tgljemur, ' ' AS rak, TglPotong TglProduksi, '' Lokasidest,PartnoAsal PartNodest, ID, 0 ItemIDSer, " +
                    "TglPotong TglSerah, PartNoBP PartNoser, 'P99'  Lokasiser, QtyAsal AS Qtysin, QtyAsal AS Qtysout, 0 HPP, NoPAlet  " +
                    "FROM t1_paletpelarian order by NoPAlet";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_Serah.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT1_Serah.Add(new T1_Serah());

            return arrT1_Serah;
        }
        public ArrayList RetrieveForDefectPaletPel(string tanggal, string listBy, string palet)
        {
            string strSQL = string.Empty;
            if (listBy == "produksi")
                strSQL = "SELECT  A.ID AS DestID, ' ' AS tgljemur, ' ' AS rak, A.TglProduksi, A1.Lokasi AS Lokasidest, A5.PartNo AS PartNodest, C.ID, C.ItemID AS ItemIDSer, " +
                   "C.TglSerah, C2.PartNo as PartNoser,case when C.sfrom='lari' then 'P99' else C1.Lokasi end Lokasiser, C.QtyIn AS Qtysin, C.QtyOut AS Qtysout, ISNULL(C.HPP, 0) AS HPP, BM_Palet.NoPAlet " +
                   "FROM BM_Destacking AS A LEFT OUTER JOIN FC_Items AS A5 ON A.ItemID = A5.ID LEFT OUTER JOIN FC_Lokasi AS A1 ON A.LokasiID = A1.ID LEFT OUTER JOIN " +
                   "BM_Palet ON A.PaletID = BM_Palet.ID RIGHT OUTER JOIN FC_Lokasi AS C1 RIGHT OUTER JOIN T1_Serah AS C ON C1.ID = C.LokID LEFT OUTER JOIN " +
                   "FC_Items AS C2 ON C.ItemID = C2.ID ON A.ID = C.DestID " +
                   "WHERE (C2.partno like '%-P-%' or or C.sfrom='lari') and C.status =0 and    convert(varchar,A.TglProduksi,112)='" + tanggal + "' and BM_Palet.NoPAlet='" + palet + "'  order by BM_Palet.NoPAlet ";
            else
                strSQL = "SELECT  A.ID AS DestID, ' ' AS tgljemur, ' ' AS rak, A.TglProduksi, A1.Lokasi AS Lokasidest, A5.PartNo AS PartNodest, C.ID, C.ItemID AS ItemIDSer, " +
                    "C.TglSerah, C2.PartNo as PartNoser, case when C.sfrom='lari' then 'P99' else C1.Lokasi end Lokasiser, C.QtyIn AS Qtysin, C.QtyOut AS Qtysout, ISNULL(C.HPP, 0) AS HPP, BM_Palet.NoPAlet " +
                    "FROM BM_Destacking AS A LEFT OUTER JOIN FC_Items AS A5 ON A.ItemID = A5.ID LEFT OUTER JOIN FC_Lokasi AS A1 ON A.LokasiID = A1.ID LEFT OUTER JOIN " +
                    "BM_Palet ON A.PaletID = BM_Palet.ID RIGHT OUTER JOIN FC_Lokasi AS C1 RIGHT OUTER JOIN T1_Serah AS C ON C1.ID = C.LokID LEFT OUTER JOIN " +
                    "FC_Items AS C2 ON C.ItemID = C2.ID ON A.ID = C.DestID " +
                    "WHERE (C2.partno like '%-P-%' or C.sfrom='lari') and C.status =0 and    convert(varchar,C.TglSerah,112)='" + tanggal + "' and BM_Palet.NoPAlet='" + palet + "' order by BM_Palet.NoPAlet ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_Serah.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT1_Serah.Add(new T1_Serah());

            return arrT1_Serah;
        }
        public ArrayList RetrieveByTglSerah(string tglSerah,string lokasi)
        {
            string strSQL = "SELECT  A.ID AS DestID, A.TglProduksi, A3.FormulaCode AS Formula, A5.PartNo AS PartNodest, C.ID, C.ItemID AS ItemIDSer, C.TglSerah, C2.PartNo AS PartNoser, " +
                "C1.Lokasi AS Lokasiser, C.QtyIn AS Qtysin, C.QtyOut AS Qtysout,isnull(C.HPP,0) as HPP FROM  BM_Formula AS A3 INNER JOIN BM_Destacking AS A ON A3.ID = A.FormulaID " +
                "INNER JOIN FC_Items AS A5 ON A.ItemID = A5.ID INNER JOIN FC_Items AS C2 INNER JOIN T1_Serah AS C ON C2.ID = C.ItemID INNER JOIN " +
                "FC_Lokasi AS C1 ON C.LokID = C1.ID ON A.ID = C.DestID WHERE  " + lokasi + "  C.QtyIn > C.QtyOut AND C.status>-1 and CONVERT(char(8),C.TglSerah  , 112) = '" + tglSerah + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_Serah.Add(GenerateObjectSerah(sqlDataReader));
                }
            }
            else
                arrT1_Serah.Add(new T1_Serah());

            return arrT1_Serah;
        }
        public T1_Serah RetrieveByPaletP(string tanggal, string Palet)
        {
            string strSQL = "SELECT  A.ID AS DestID, ' ' AS tgljemur, ' ' AS rak, A.TglProduksi, A1.Lokasi AS Lokasidest, A5.PartNo AS PartNodest, C.ID, C.ItemID AS ItemIDSer, " +
                   "C.TglSerah,C2.PartNo AS PartNoser, C1.Lokasi AS Lokasiser, C.QtyIn AS Qtysin, C.QtyOut AS Qtysout, ISNULL(C.HPP, 0) AS HPP, BM_Palet.NoPAlet " +
                   "FROM BM_Destacking AS A LEFT OUTER JOIN FC_Items AS A5 ON A.ItemID = A5.ID LEFT OUTER JOIN FC_Lokasi AS A1 ON A.LokasiID = A1.ID LEFT OUTER JOIN " +
                   "BM_Palet ON A.PaletID = BM_Palet.ID RIGHT OUTER JOIN FC_Lokasi AS C1 RIGHT OUTER JOIN T1_Serah AS C ON C1.ID = C.LokID LEFT OUTER JOIN " +
                   "FC_Items AS C2 ON C.ItemID = C2.ID ON A.ID = C.DestID " +
                   "WHERE C2.partno like '%-P-%' and C.status >-1 and    convert(varchar,A.TglProduksi,112)='" + tanggal + "' and BM_Palet.NoPAlet='" + Palet + "' order by C.ID ";
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

            return new T1_Serah();
        }
        public T1_Serah RetrieveByPaletPDefect(string tanggal, string Palet)
        {
            string strSQL = "SELECT  A.ID AS DestID, ' ' AS tgljemur, ' ' AS rak, A.TglProduksi, A1.Lokasi AS Lokasidest, A5.PartNo AS PartNodest, C.ID, C.ItemID AS ItemIDSer, " +
                   "C.TglSerah,C2.PartNo AS PartNoser, C1.Lokasi AS Lokasiser, C.QtyIn AS Qtysin, C.QtyOut AS Qtysout, ISNULL(C.HPP, 0) AS HPP, BM_Palet.NoPAlet " +
                   "FROM BM_Destacking AS A LEFT OUTER JOIN FC_Items AS A5 ON A.ItemID = A5.ID LEFT OUTER JOIN FC_Lokasi AS A1 ON A.LokasiID = A1.ID LEFT OUTER JOIN " +
                   "BM_Palet ON A.PaletID = BM_Palet.ID RIGHT OUTER JOIN FC_Lokasi AS C1 RIGHT OUTER JOIN T1_Serah AS C ON C1.ID = C.LokID LEFT OUTER JOIN " +
                   "FC_Items AS C2 ON C.ItemID = C2.ID ON A.ID = C.DestID " +
                   "WHERE (C2.partno like '%-P-%' or C.sfrom='lari')  and C.status =0 and    convert(varchar,A.TglProduksi,112)='" + tanggal + "' and BM_Palet.NoPAlet='" + Palet + "' order by C.ID ";
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

            return new T1_Serah();
        }
        public T1_Serah RetrieveByPaletS(string tanggal, string Palet)
        {
            string strSQL = "SELECT  A.ID AS DestID, ' ' AS tgljemur, ' ' AS rak, A.TglProduksi, A1.Lokasi AS Lokasidest, A5.PartNo AS PartNodest, C.ID, C.ItemID AS ItemIDSer, " +
                   "C.TglSerah,C2.PartNo AS PartNoser, C1.Lokasi AS Lokasiser, C.QtyIn AS Qtysin, C.QtyOut AS Qtysout, ISNULL(C.HPP, 0) AS HPP, BM_Palet.NoPAlet " +
                   "FROM BM_Destacking AS A LEFT OUTER JOIN FC_Items AS A5 ON A.ItemID = A5.ID LEFT OUTER JOIN FC_Lokasi AS A1 ON A.LokasiID = A1.ID LEFT OUTER JOIN " +
                   "BM_Palet ON A.PaletID = BM_Palet.ID RIGHT OUTER JOIN FC_Lokasi AS C1 RIGHT OUTER JOIN T1_Serah AS C ON C1.ID = C.LokID LEFT OUTER JOIN " +
                   "FC_Items AS C2 ON C.ItemID = C2.ID ON A.ID = C.DestID " +
                   "WHERE C2.partno like '%-P-%' and C.status >-1 and    convert(varchar,C.tglserah,112)='" + tanggal + "' and BM_Palet.NoPAlet='" + Palet + "' order by C.ID ";
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

            return new T1_Serah();
        }
        public T1_Serah RetrieveByPaletSDefect(string tanggal, string Palet)
        {
            string strSQL = "SELECT  A.ID AS DestID, ' ' AS tgljemur, ' ' AS rak, A.TglProduksi, A1.Lokasi AS Lokasidest, A5.PartNo AS PartNodest, C.ID, C.ItemID AS ItemIDSer, " +
                   "C.TglSerah,C2.PartNo AS PartNoser, C1.Lokasi AS Lokasiser, C.QtyIn AS Qtysin, C.QtyOut AS Qtysout, ISNULL(C.HPP, 0) AS HPP, BM_Palet.NoPAlet " +
                   "FROM BM_Destacking AS A LEFT OUTER JOIN FC_Items AS A5 ON A.ItemID = A5.ID LEFT OUTER JOIN FC_Lokasi AS A1 ON A.LokasiID = A1.ID LEFT OUTER JOIN " +
                   "BM_Palet ON A.PaletID = BM_Palet.ID RIGHT OUTER JOIN FC_Lokasi AS C1 RIGHT OUTER JOIN T1_Serah AS C ON C1.ID = C.LokID LEFT OUTER JOIN " +
                   "FC_Items AS C2 ON C.ItemID = C2.ID ON A.ID = C.DestID " +
                   "WHERE (C2.partno like '%-P-%' or C.sfrom='lari') and C.status =0 and    convert(varchar,C.tglserah,112)='" + tanggal + "' and BM_Palet.NoPAlet='" + Palet + "' order by C.ID ";
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

            return new T1_Serah();
        }

        public T1_Serah  RetrieveByID(int ID)
        {
            string strSQL = "SELECT  A.ID AS DestID, A.TglProduksi, A3.FormulaCode AS Formula, A5.PartNo AS PartNodest, C.ID, C.ItemID AS ItemIDSer, C.TglSerah, C2.PartNo AS PartNoser, " +
                "C1.Lokasi AS Lokasiser, C.QtyIn AS Qtysin, C.QtyOut AS Qtysout,isnull(C.HPP,0) as HPP FROM  BM_Formula AS A3 INNER JOIN BM_Destacking AS A ON A3.ID = A.FormulaID " +
                "INNER JOIN FC_Items AS A5 ON A.ItemID = A5.ID INNER JOIN FC_Items AS C2 INNER JOIN T1_Serah AS C ON C2.ID = C.ItemID INNER JOIN " +
                "FC_Lokasi AS C1 ON C.LokID = C1.ID ON A.ID = C.DestID WHERE C.ID=" + ID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectSerah(sqlDataReader);
                }
            }
            return new T1_Serah();
        }
        public T1_Serah RetrieveByIDBevel(string ID)
        {
            string strSQL = "SELECT  A.ID AS DestID, A.TglProduksi, A3.FormulaCode AS Formula, A5.PartNo AS PartNodest, C.ID, C.ItemID AS ItemIDSer, C.TglTrans TglSerah, C2.PartNo AS PartNoser, " +
                "C1.Lokasi AS Lokasiser, C.QtyIn AS Qtysin, C.QtyOut AS Qtysout,isnull(C.HPP,0) as HPP FROM  BM_Formula AS A3 INNER JOIN BM_Destacking AS A ON A3.ID = A.FormulaID " +
                "INNER JOIN FC_Items AS A5 ON A.ItemID = A5.ID INNER JOIN FC_Items AS C2 INNER JOIN T1_Bevel AS C ON C2.ID = C.ItemID INNER JOIN " +
                "FC_Lokasi AS C1 ON C.LokID = C1.ID ON A.ID = C.DestID WHERE C.ID=" + ID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectSerah(sqlDataReader);
                }
            }
            return new T1_Serah();
        }
        public T1_Serah RetrieveByIDLR1Bevel(string ID)
        {
            string strSQL = "SELECT  A.ID AS DestID, A.TglProduksi, A3.FormulaCode AS Formula, A5.PartNo AS PartNodest, C.ID, C.ItemID AS ItemIDSer, C.TglTrans TglSerah, C2.PartNo AS PartNoser, " +
                "C1.Lokasi AS Lokasiser, C.QtyIn AS Qtysin, C.QtyOut AS Qtysout,isnull(C.HPP,0) as HPP FROM  BM_Formula AS A3 INNER JOIN BM_Destacking AS A ON A3.ID = A.FormulaID " +
                "INNER JOIN FC_Items AS A5 ON A.ItemID = A5.ID INNER JOIN FC_Items AS C2 INNER JOIN T1_LR1_Bevel AS C ON C2.ID = C.ItemID INNER JOIN " +
                "FC_Lokasi AS C1 ON C.LokID = C1.ID ON A.ID = C.DestID WHERE C.ID=" + ID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectSerah(sqlDataReader);
                }
            }
            return new T1_Serah();
        }
        public T1_Serah RetrieveByIDPrint(string ID)
        {
            string strSQL = "SELECT  A.ID AS DestID, A.TglProduksi, A3.FormulaCode AS Formula, A5.PartNo AS PartNodest, C.ID, C.ItemID AS ItemIDSer, C.TglTrans TglSerah, C2.PartNo AS PartNoser, " +
                "C1.Lokasi AS Lokasiser, C.QtyIn AS Qtysin, C.QtyOut AS Qtysout,isnull(C.HPP,0) as HPP FROM  BM_Formula AS A3 INNER JOIN BM_Destacking AS A ON A3.ID = A.FormulaID " +
                "INNER JOIN FC_Items AS A5 ON A.ItemID = A5.ID INNER JOIN FC_Items AS C2 INNER JOIN T1_Straping AS C ON C2.ID = C.ItemID INNER JOIN " +
                "FC_Lokasi AS C1 ON C.LokID = C1.ID ON A.ID = C.DestID WHERE C.ID=" + ID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectSerah(sqlDataReader);
                }
            }
            return new T1_Serah();
        }

        public T1_Serah RetrieveByIDListP(string ID)
        {
            string Proses = string.Empty; string Proses0 = string.Empty;
            Proses = HttpContext.Current.Session["Proses"].ToString();
            if (Proses == "i99") { Proses0 = "T1_ListPlank"; }
            else if (Proses == "i99R1") { Proses0 = "T1_LR1_ListPlank"; }
            else if (Proses == "i99R2") { Proses0 = "T1_LR2_ListPlank"; }
            else if (Proses == "i99R3") { Proses0 = "T1_LR3_ListPlank"; }
            else if (Proses == "Bevel") { Proses0 = "T1_Bevel"; }
            else if (Proses == "BevelR1") { Proses0 = "T1_LR1_Bevel"; }
            else if (Proses == "BevelR2") { Proses0 = "T1_LR2_Bevel"; }
            else if (Proses == "BevelR3") { Proses0 = "T1_LR3_Bevel"; }
            else if (Proses == "Rs") { Proses0 = "T1_RuningSaw"; }
            else if (Proses == "RsR1") { Proses0 = "T1_LR1_RuningSaw"; }
            else if (Proses == "RsR2") { Proses0 = "T1_LR2_RuningSaw"; }
            else if (Proses == "RsR3") { Proses0 = "T1_LR3_RuningSaw"; }

            string strSQL = "SELECT  A.ID AS DestID, A.TglProduksi, A3.FormulaCode AS Formula, A5.PartNo AS PartNodest, C.ID, C.ItemID AS ItemIDSer, C.TglTrans TglSerah, C2.PartNo AS PartNoser, " +
                "C1.Lokasi AS Lokasiser, C.QtyIn AS Qtysin, C.QtyOut AS Qtysout,isnull(C.HPP,0) as HPP FROM  BM_Formula AS A3 INNER JOIN BM_Destacking AS A ON A3.ID = A.FormulaID " +
                "INNER JOIN FC_Items AS A5 ON A.ItemID = A5.ID INNER JOIN FC_Items AS C2 INNER JOIN " + Proses0 + " AS C ON C2.ID = C.ItemID INNER JOIN " +
                "FC_Lokasi AS C1 ON C.LokID = C1.ID ON A.ID = C.DestID WHERE C.ID=" + ID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectSerah(sqlDataReader);
                }
            }
            return new T1_Serah();
        }

        public T1_Serah RetrieveByIDLR1_Print(string ID)
        {
            string strSQL = "SELECT  A.ID AS DestID, A.TglProduksi, A3.FormulaCode AS Formula, A5.PartNo AS PartNodest, C.ID, C.ItemID AS ItemIDSer, C.TglTrans TglSerah, C2.PartNo AS PartNoser, " +
                "C1.Lokasi AS Lokasiser, C.QtyIn AS Qtysin, C.QtyOut AS Qtysout,isnull(C.HPP,0) as HPP FROM  BM_Formula AS A3 INNER JOIN BM_Destacking AS A ON A3.ID = A.FormulaID " +
                "INNER JOIN FC_Items AS A5 ON A.ItemID = A5.ID INNER JOIN FC_Items AS C2 INNER JOIN T1_LR1_Straping AS C ON C2.ID = C.ItemID INNER JOIN " +
                "FC_Lokasi AS C1 ON C.LokID = C1.ID ON A.ID = C.DestID WHERE C.ID=" + ID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectSerah(sqlDataReader);
                }
            }
            return new T1_Serah();
        }
        public T1_Serah RetrieveByIDLR2_Print(string ID)
        {
            string strSQL = "SELECT  A.ID AS DestID, A.TglProduksi, A3.FormulaCode AS Formula, A5.PartNo AS PartNodest, C.ID, C.ItemID AS ItemIDSer, C.TglTrans TglSerah, C2.PartNo AS PartNoser, " +
                "C1.Lokasi AS Lokasiser, C.QtyIn AS Qtysin, C.QtyOut AS Qtysout,isnull(C.HPP,0) as HPP FROM  BM_Formula AS A3 INNER JOIN BM_Destacking AS A ON A3.ID = A.FormulaID " +
                "INNER JOIN FC_Items AS A5 ON A.ItemID = A5.ID INNER JOIN FC_Items AS C2 INNER JOIN T1_LR2_Straping AS C ON C2.ID = C.ItemID INNER JOIN " +
                "FC_Lokasi AS C1 ON C.LokID = C1.ID ON A.ID = C.DestID WHERE C.ID=" + ID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectSerah(sqlDataReader);
                }
            }
            return new T1_Serah();
        }
        public T1_Serah RetrieveByIDLR3_Print(string ID)
        {
            string strSQL = "SELECT  A.ID AS DestID, A.TglProduksi, A3.FormulaCode AS Formula, A5.PartNo AS PartNodest, C.ID, C.ItemID AS ItemIDSer, C.TglTrans TglSerah, C2.PartNo AS PartNoser, " +
                "C1.Lokasi AS Lokasiser, C.QtyIn AS Qtysin, C.QtyOut AS Qtysout,isnull(C.HPP,0) as HPP FROM  BM_Formula AS A3 INNER JOIN BM_Destacking AS A ON A3.ID = A.FormulaID " +
                "INNER JOIN FC_Items AS A5 ON A.ItemID = A5.ID INNER JOIN FC_Items AS C2 INNER JOIN T1_LR3_Straping AS C ON C2.ID = C.ItemID INNER JOIN " +
                "FC_Lokasi AS C1 ON C.LokID = C1.ID ON A.ID = C.DestID WHERE C.ID=" + ID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectSerah(sqlDataReader);
                }
            }
            return new T1_Serah();
        }
        public T1_Serah RetrieveByIDLR2_Bevel(string ID)
        {
            string strSQL = "SELECT  A.ID AS DestID, A.TglProduksi, A3.FormulaCode AS Formula, A5.PartNo AS PartNodest, C.ID, C.ItemID AS ItemIDSer, C.TglTrans TglSerah, C2.PartNo AS PartNoser, " +
                "C1.Lokasi AS Lokasiser, C.QtyIn AS Qtysin, C.QtyOut AS Qtysout,isnull(C.HPP,0) as HPP FROM  BM_Formula AS A3 INNER JOIN BM_Destacking AS A ON A3.ID = A.FormulaID " +
                "INNER JOIN FC_Items AS A5 ON A.ItemID = A5.ID INNER JOIN FC_Items AS C2 INNER JOIN T1_LR2_Bevel AS C ON C2.ID = C.ItemID INNER JOIN " +
                "FC_Lokasi AS C1 ON C.LokID = C1.ID ON A.ID = C.DestID WHERE C.ID=" + ID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectSerah(sqlDataReader);
                }
            }
            return new T1_Serah();
        }
        public T1_Serah RetrieveByIDLR3_Bevel(string ID)
        {
            string strSQL = "SELECT  A.ID AS DestID, A.TglProduksi, A3.FormulaCode AS Formula, A5.PartNo AS PartNodest, C.ID, C.ItemID AS ItemIDSer, C.TglTrans TglSerah, C2.PartNo AS PartNoser, " +
                "C1.Lokasi AS Lokasiser, C.QtyIn AS Qtysin, C.QtyOut AS Qtysout,isnull(C.HPP,0) as HPP FROM  BM_Formula AS A3 INNER JOIN BM_Destacking AS A ON A3.ID = A.FormulaID " +
                "INNER JOIN FC_Items AS A5 ON A.ItemID = A5.ID INNER JOIN FC_Items AS C2 INNER JOIN T1_LR3_Bevel AS C ON C2.ID = C.ItemID INNER JOIN " +
                "FC_Lokasi AS C1 ON C.LokID = C1.ID ON A.ID = C.DestID WHERE C.ID=" + ID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectSerah(sqlDataReader);
                }
            }
            return new T1_Serah();
        }
        
        public T1_Serah RetrieveBySerahID(int ID)
        {
            string strSQL = "SELECT  A.ID AS DestID, ' ' AS tgljemur, ' ' AS rak, A.TglProduksi, A1.Lokasi AS Lokasidest, A5.PartNo AS PartNodest, C.ID, C.ItemID AS ItemIDSer, " +
                   "C.TglSerah,C2.PartNo AS PartNoser, C1.Lokasi AS Lokasiser, C.QtyIn AS Qtysin, C.QtyOut AS Qtysout, ISNULL(C.HPP, 0) AS HPP, BM_Palet.NoPAlet " +
                   "FROM BM_Destacking AS A LEFT OUTER JOIN FC_Items AS A5 ON A.ItemID = A5.ID LEFT OUTER JOIN FC_Lokasi AS A1 ON A.LokasiID = A1.ID LEFT OUTER JOIN " +
                   "BM_Palet ON A.PaletID = BM_Palet.ID RIGHT OUTER JOIN FC_Lokasi AS C1 RIGHT OUTER JOIN T1_Serah AS C ON C1.ID = C.LokID LEFT OUTER JOIN " +
                   "FC_Items AS C2 ON C.ItemID = C2.ID ON A.ID = C.DestID " +
                   "WHERE C2.partno like '%-P-%' and C.status >-1 and C.ID=" + ID;
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

            return new T1_Serah();
        }

        public T1_Serah RetrieveBySerahIDDefect(int ID)
        {
            string strSQL = "SELECT  A.ID AS DestID, ' ' AS tgljemur, ' ' AS rak, A.TglProduksi, A1.Lokasi AS Lokasidest, A5.PartNo AS PartNodest, C.ID, C.ItemID AS ItemIDSer, " +
                   "C.TglSerah,C2.PartNo AS PartNoser, C1.Lokasi AS Lokasiser, C.QtyIn AS Qtysin, C.QtyOut AS Qtysout, ISNULL(C.HPP, 0) AS HPP, BM_Palet.NoPAlet " +
                   "FROM BM_Destacking AS A LEFT OUTER JOIN FC_Items AS A5 ON A.ItemID = A5.ID LEFT OUTER JOIN FC_Lokasi AS A1 ON A.LokasiID = A1.ID LEFT OUTER JOIN " +
                   "BM_Palet ON A.PaletID = BM_Palet.ID RIGHT OUTER JOIN FC_Lokasi AS C1 RIGHT OUTER JOIN T1_Serah AS C ON C1.ID = C.LokID LEFT OUTER JOIN " +
                   "FC_Items AS C2 ON C.ItemID = C2.ID ON A.ID = C.DestID " +
                   "WHERE (C2.partno like '%-P-%' or C.sfrom='lari') and C.status =0 and C.ID=" + ID;
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

            return new T1_Serah();
        }
        public T1_Serah RetrieveBySerahIDDefectPel(int ID)
        {
            string strSQL = "SELECT 0 DestID, ' ' AS tgljemur, ' ' AS rak, TglPotong TglProduksi, '' Lokasidest,PartnoAsal PartNodest, ID, 0 ItemIDSer, " +
                    "TglPotong TglSerah, PartNoBP PartNoser, 'P99'  Lokasiser, QtyAsal AS Qtysin, QtyAsal AS Qtysout, 0 HPP, NoPAlet  " +
                    "FROM t1_paletpelarian where ID=" + ID;
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

            return new T1_Serah();
        }

        public ArrayList RetrieveByTglProduksiLok(string tglSerah, string lokasi)
        {
            string strSQL = "SELECT  A.ID AS DestID, A.TglProduksi, A3.FormulaCode AS Formula, A5.PartNo AS PartNodest, C.ID, C.ItemID AS ItemIDSer, C.TglSerah, C2.PartNo AS PartNoser, " +
                "C1.Lokasi AS Lokasiser, C.QtyIn AS Qtysin, C.QtyOut AS Qtysout,isnull(C.HPP,0) as HPP FROM  BM_Formula AS A3 INNER JOIN BM_Destacking AS A ON A3.ID = A.FormulaID " +
                "INNER JOIN FC_Items AS A5 ON A.ItemID = A5.ID INNER JOIN FC_Items AS C2 INNER JOIN T1_Serah AS C ON C2.ID = C.ItemID INNER JOIN " +
                "FC_Lokasi AS C1 ON C.LokID = C1.ID ON A.ID = C.DestID WHERE " + lokasi + "  C.QtyIn > C.QtyOut AND CONVERT(char(8),A.TglProduksi  , 112) = '" + tglSerah + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_Serah.Add(GenerateObjectSerah(sqlDataReader));
                }
            }
            else
                arrT1_Serah.Add(new T1_Serah());

            return arrT1_Serah;
        }

        public ArrayList RetrieveByTglSerahW28(string tglSerah)
        {
            string strSQL = "SELECT  A.ID AS DestID, A.TglProduksi, A3.FormulaCode AS Formula, A5.PartNo AS PartNodest, C.ID, C.ItemID AS ItemIDSer, C.TglSerah, C2.PartNo AS PartNoser, " +
                "C1.Lokasi AS Lokasiser, C.QtyIn AS Qtysin, C.QtyOut AS Qtysout,isnull(C.HPP,0) as HPP FROM  BM_Formula AS A3 INNER JOIN BM_Destacking AS A ON A3.ID = A.FormulaID " +
                "INNER JOIN FC_Items AS A5 ON A.ItemID = A5.ID INNER JOIN FC_Items AS C2 INNER JOIN T1_Serah AS C ON C2.ID = C.ItemID INNER JOIN " +
                "FC_Lokasi AS C1 ON C.LokID = C1.ID ON A.ID = C.DestID WHERE C1.Lokasi='W28' and C.QtyIn > C.QtyOut AND CONVERT(char(8),C.TglSerah  , 112) = '" + tglSerah + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_Serah.Add(GenerateObjectSerah(sqlDataReader));
                }
            }
            else
                arrT1_Serah.Add(new T1_Serah());

            return arrT1_Serah;
        }

        public ArrayList RetrieveByTglSerahB27(string tglSerah)
        {
            string strSQL = "SELECT  A.ID AS DestID, A.TglProduksi, A3.FormulaCode AS Formula, A5.PartNo AS PartNodest, C.ID, C.ItemID AS ItemIDSer, C.TglSerah, C2.PartNo AS PartNoser, " +
                "C1.Lokasi AS Lokasiser, C.QtyIn AS Qtysin, C.QtyOut AS Qtysout,isnull(C.HPP,0) as HPP  FROM  BM_Formula AS A3 INNER JOIN BM_Destacking AS A ON A3.ID = A.FormulaID " +
                "INNER JOIN FC_Items AS A5 ON A.ItemID = A5.ID INNER JOIN FC_Items AS C2 INNER JOIN T1_Serah AS C ON C2.ID = C.ItemID INNER JOIN " +
                "FC_Lokasi AS C1 ON C.LokID = C1.ID ON A.ID = C.DestID WHERE C1.Lokasi='B27' and C.QtyIn > C.QtyOut AND CONVERT(char(8),C.TglSerah  , 112) = '" + tglSerah + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_Serah.Add(GenerateObjectSerah(sqlDataReader));
                }
            }
            else
                arrT1_Serah.Add(new T1_Serah());

            return arrT1_Serah;
        }

        public int GetPartnoID(string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select id from fc_items where partno='" + strValue + "'");
            strError = dataAccess.Error;
            int Partno = 0;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    Partno = Convert.ToInt32(sqlDataReader["id"]);
                }
            }
            else
                Partno = 0;

            return Partno;
        }
        
        public ArrayList RetrieveByTransitPartnoNLokasi(string partno,string lokasi)
        {
            string strSQL = "select Tglserah,tglproduksi,PartNo,Lokasi,Saldo from ( " +
                "select A.tglserah, B.tglproduksi,case when A.ItemID>0 then (select partno from FC_Items where ID=A.ItemID ) end Partno, " +
                "case when A.LokID>0 then (select Lokasi from FC_Lokasi where ID=A.LokID ) end Lokasi, sum(A.QtyIn-A.QtyOut) as Saldo " +
                "from T1_Serah A,BM_destacking B  where A.status>-1 and A.QtyIn>A.QtyOut and A.destid=B.id and  A.itemid>0 " +
                " group by A.TglSerah,B.tglproduksi,A.ItemID,A.LokID ) as T where saldo>0 and T.Lokasi='" + lokasi +
                "' and T.Partno ='" + partno + "' order by T.Lokasi,T.Partno,tglproduksi,Tglserah";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_Serah.Add(GenerateObjectTransit(sqlDataReader));
                }
            }
            else
                arrT1_Serah.Add(new T1_Serah());

            return arrT1_Serah;
        }

        public ArrayList RetrieveByTransit(string strdate)
        {
            string strSQL = "select Tglserah,tglproduksi,PartNo,Lokasi,Saldo from ( " +
                "select A.tglserah, B.tglproduksi,case when A.ItemID>0 then (select partno from FC_Items where ID=A.ItemID ) end Partno, " +
                "case when A.LokID>0 then (select Lokasi from FC_Lokasi where ID=A.LokID ) end Lokasi, sum(A.QtyIn-A.QtyOut) as Saldo " +
                "from T1_Serah A,BM_destacking B  where A.status>-1 and A.QtyIn>A.QtyOut and A.destid=B.id and convert(varchar,A.Tglserah,112)='" + strdate +
                "' group by A.TglSerah,B.tglproduksi,A.ItemID,A.LokID ) as T  order by T.Lokasi,T.Partno";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_Serah.Add(GenerateObjectTransit(sqlDataReader));
                }
            }
            else
                arrT1_Serah.Add(new T1_Serah());

            return arrT1_Serah;
        }

        public ArrayList RetrieveByTotalTransit()
        {
            string strSQL = "select PartNo,Lokasi,Saldo from ( " +
                "select case when A.ItemID>0 then (select partno from FC_Items where ID=A.ItemID ) end Partno, " +
                "case when A.LokID>0 then (select Lokasi from FC_Lokasi where ID=A.LokID ) end Lokasi, sum(A.QtyIn-A.QtyOut) as Saldo " +
                "from T1_Serah A where A.itemid>0 and A.QtyIn>A.QtyOut group by A.ItemID,A.LokID) as T where saldo>0 order by T.Lokasi,T.Partno";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_Serah.Add(GenerateObjectTotalTransit(sqlDataReader));
                }
            }
            else
                arrT1_Serah.Add(new T1_Serah());

            return arrT1_Serah;
        }

        public ArrayList RetrieveByTransitP(string strdate)
        {
            string strSQL = "select Tglserah,tglproduksi,PartNo,Lokasi,Saldo from ( " +
                "select A.tglserah, B.tglproduksi,case when A.ItemID>0 then (select partno from FC_Items where ID=A.ItemID ) end Partno, " +
                "case when A.LokID>0 then (select Lokasi from FC_Lokasi where ID=A.LokID ) end Lokasi, sum(A.QtyIn-A.QtyOut) as Saldo " +
                "from T1_Serah A,BM_destacking B  where A.QtyIn>A.QtyOut and A.destid=B.id and convert(varchar,B.tglproduksi,112)='" + strdate + "' group by A.TglSerah,B.tglproduksi,A.ItemID,A.LokID ) as T";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_Serah.Add(GenerateObjectTransit(sqlDataReader));
                }
            }
            else
                arrT1_Serah.Add(new T1_Serah());

            return arrT1_Serah;
        }
        public ArrayList RetrieveByTglSerahWIP(string strdate,string partno,string partnoBM)
        {
            string strSQL =string.Empty ;
            if (partno.Substring(3, 3).Trim() != "-1-")// || partnoBM.Substring(4, 3).Trim() != "-1-" || partnoBM.Substring(5, 3).Trim() != "-1-")
                strSQL = "SELECT i.PartNo, l.Lokasi, A.QtyIn as saldo FROM T1_Serah AS A INNER JOIN FC_Items AS i ON A.ItemID = i.ID " +
                   "INNER JOIN FC_Lokasi AS l ON A.LokID = l.ID left join BM_Destacking D on A.destid=D.id and D.itemid " +
                   "in(select ID from fc_items where itemtypeid=1 and partno='" + partnoBM +
                   "') where A.status>-1 and D.rowstatus>-1 and convert(varchar,A.tglSerah,112)='" + strdate + "' and i.partno='" + partno + "'";
            else
            {
                string partno2 = partno.Substring(0, partno.Trim().Length - 12);
                string strdate1 = partno.Substring(partno.Trim().Length - 10);
                strSQL = "SELECT B.PartNo,A.QtyIn AS saldo,(select lokasi from FC_Lokasi where ID=A.LokID0 ) as lokasi FROM FC_Items AS B INNER JOIN  "+
                    "T1_JemurLg AS A ON B.ID = A.ItemID0 left join BM_Destacking C on C.ID=A.DestID left join FC_Items D on C.ItemID=D.ID  "+
                    "WHERE A.status>-1 AND (D.PartNo = '" + partnoBM + "')  and B.PartNo='" + partno2 + "'  " +
                    "and convert(varchar,C.TglProduksi,110)='" + strdate1 + "' and convert(varchar,A.TglJemur,112)='" + strdate + "'";
            }
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_Serah.Add(GenerateObjectTotalTransit(sqlDataReader));
                }
            }
            else
                arrT1_Serah.Add(new T1_Serah());

            return arrT1_Serah;
        }
        public ArrayList RetrieveByTglSerahWIP2(string strdate, string partno, string partnoBM)
        {
           string  partno2 = partno.Substring(0, partno.Trim().Length - 12);
           string strdate1 = partno.Substring(partno.Trim().Length - 4) +partno.Substring(partno.Trim().Length - 10,2) + partno.Substring(partno.Trim().Length - 7,2) ;
            //string strSQL = "SELECT i.PartNo, l.Lokasi, A.QtyIn as saldo FROM T1_Serah AS A INNER JOIN FC_Items AS i ON A.ItemID = i.ID " +
            //        "INNER JOIN FC_Lokasi AS l ON A.LokID = l.ID left join T1_jemurlg D on A.destid=D.destid and D.itemid0 " +
            //        "in(select ID from fc_items where  partno='" + partnoBM +
            //        "') where A.status>-1 and D.status>-1 and convert(varchar,A.tglSerah,112)='" + strdate + "' and i.partno='" + partno2 + "'";
           string strSQL = "SELECT  C.PartNo,(select lokasi from FC_Lokasi where ID=A.LokID ) as lokasi,A.QtyIn AS saldo "+
               "FROM T1_Serah AS A INNER JOIN  BM_Destacking AS B ON A.DestID = B.ID " +
               "INNER JOIN FC_Items AS C ON A.ItemID = C.ID INNER JOIN FC_Items AS C1 ON A.ItemID0 = C1.ID  "+
               "WHERE  B.qty>0 and (A.Status > - 1) AND (B.Status > - 1) and " +
               "(CONVERT(varchar, B.TglProduksi, 112) = '" + strdate1 + "' and CONVERT(varchar, A.TglSerah, 112) = '" +
               strdate + "' and C1.PartNo = '" + partnoBM + "') and C.PartNo='" + partno2 + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_Serah.Add(GenerateObjectTotalTransit(sqlDataReader));
                }
            }
            else
                arrT1_Serah.Add(new T1_Serah());

            return arrT1_Serah;
        }
        public ArrayList RetrieveByTglProduksiWIP(string strdate, string partno)
        {
            string strSQL = "SELECT i.PartNo, l.Lokasi, A.Qty as saldo FROM BM_Destacking  AS A INNER JOIN FC_Items AS i ON A.ItemID = i.ID "+
                    "INNER JOIN FC_Lokasi AS l ON A.LokasiID = l.ID where A.rowstatus>-1 and convert(varchar,A.tglproduksi,112)='" + strdate + "' and i.partno='" + partno + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_Serah.Add(GenerateObjectTotalTransit(sqlDataReader));
                }
            }
            else
                arrT1_Serah.Add(new T1_Serah());

            return arrT1_Serah;
        }
        public ArrayList RetrieveByTglProduksiWIP2(string strdate, string partno, string partnoBM)
        {
            string partno2 = partno.Substring(0, partno.Trim().Length - 12);
            string strdate1 = partnoBM.Substring(partnoBM.Trim().Length - 4) + partnoBM.Substring(partnoBM.Trim().Length - 10, 2) + partnoBM.Substring(partnoBM.Trim().Length - 7, 2);
            string strSQL = "SELECT B.PartNo,(select lokasi from FC_Lokasi where ID=A.LokID0 ) as lokasi,A.QtyIn AS saldo  "+
                "FROM FC_Items AS B INNER JOIN T1_JemurLg AS A ON B.ID = A.ItemID0  "+
                "left join BM_Destacking C on C.ID=A.DestID left join FC_Items D on C.ItemID=D.ID "+
                " WHERE (CONVERT(varchar, A.TglJemur, 112)= '" + strdate + "')  " +
                "and A.status>-1 AND (B.PartNo = '" + partno + "') and (CONVERT(varchar,C.TglProduksi, 112)= '" + strdate1 + "')";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_Serah.Add(GenerateObjectTotalTransit(sqlDataReader));
                }
            }
            else
                arrT1_Serah.Add(new T1_Serah());

            return arrT1_Serah;
        }
        
        public string UpdatePelarian(int jemurID, int t1serahID, int lokid, int itemid,string tgltrans)
        {
            string strSQL = "update t1_jemurlg set itemid=" + itemid + " , lokid=" + lokid + ",lastmodifiedtime=getdate() where ID in (select jemurID from t1_serah where ID=" +
                t1serahID + ")  ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }
        public string CancelSerah(string ID)
        {
            string strSQL = "update t1_serah set [status]=-1 where ID=" + ID +
                " update t1_jemur set qtyout=qtyout-(select qtyin from t1_serah where ID=" + ID +
                ") where destid in (select destid from t1_serah where ID=" + ID +
                ") update T1_JemurLg set qtyin=qtyin-(select qtyin from T1_Serah where  sfrom ='lari' and ID=" + ID + "), " + 
                "qtyout=qtyout-(select qtyin from T1_Serah where  sfrom ='lari' and ID=" + ID + 
                ") where ID in (select jemurid from T1_Serah where  sfrom ='lari' and ID=" + ID + ")";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }
        public string CancelSerahByT3(string ID)
        {
            string strSQL = "update t1_serah set [status]=-1 where ID=" + ID +
                " update t1_jemur set qtyout=qtyout-(select qtyin from t1_serah where ID=" + ID + ") " +
                "where destid in (select destid from t1_serah where ID=" + ID + ") "+ 
                "update T1_JemurLg set qtyin=qtyin-(select qtyin from T1_Serah where  sfrom ='lari' and ID=" + ID + "), " +
                "qtyout=qtyout-(select qtyin from T1_Serah where  sfrom ='lari' and ID=" + ID + ") "+ 
                "where ID in (select jemurid from T1_Serah where  sfrom ='lari' and ID=" + ID + ") " +
                "update t3_rekap set rowstatus=-1 where t1serahid=" + ID +
                " update t3_serah set qty=qty-(select qtyin from t3_rekap where t1serahid=" + ID + ")" +
                "where lokid in (select lokid from t3_rekap where t1serahid=" + ID
                + ") and itemid in (select itemid from t3_rekap where t1serahid=" + ID + ")";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }
        public int CekSaldoLokasi(string IDSerah)
        {
            int result = 0;
            string strSQL = "SELECT qty - (select qtyin from t3_rekap where t1serahid=" + IDSerah + 
                ") from t3_serah where lokid in (select lokid from t3_rekap where t1serahid=" + IDSerah 
                + ") and itemid in (select lokid from t3_rekap where t1serahid=" + IDSerah + ")";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    result = Convert.ToInt32(sqlDataReader["qty"]);
                }
            }
            return result;
        }
        public string UpdateserahBypelarianFull(int jemurID, int t1serahID, int lokid, int itemid, int qty, string tgltrans)
        {
            string strSQL = 
                //"update t1_jemurlg set qtyout=qtyin-qtyout+" + qty + ",itemid=" + itemid + " , lokid=" + lokid + 
                //",lastmodifiedtime=getdate() where ID in (select jemurID from t1_serah where ID=" +
                //t1serahID + ")  " +
                "update t1_serah set qtyout=" + qty + ",itemid=" + itemid + " , lokid=" + lokid + ", tglserah='" + tgltrans 
                + "', lastmodifiedtime=getdate() where ID=" + t1serahID + " ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }
        public string UpdateserahBypelarianPartial(int jemurID, int t1serahID, int lokid, int itemid, int qty, string tgltrans)
        {
            string strSQL =
                //"update t1_jemurlg set qtyout=qtyin-qtyout+" + qty +
                //",lastmodifiedtime=getdate() where ID in (select jemurID from t1_serah where ID=" +
                //t1serahID + ")  " +
                "insert T1_Serah (JemurID,DestID,LokID,ItemID0,ItemID,TglSerah,QtyIn,QtyOut,HPP,CreatedTime," +
                "CreatedBy,LastModifiedTime,LastModifiedBy, status,SFrom )" +
                "values ((select jemurid from T1_Serah where ID=" + t1serahID + ")," +
                "(select destid from T1_Serah where ID=" + t1serahID + "),(select lokid from T1_Serah where ID=" + t1serahID + ")," +
                "(select itemid0 from T1_Serah where ID=" + t1serahID + ")," +
                "(select itemid from T1_Serah where ID=" + t1serahID + ")," +
                "(select tglserah from T1_Serah where ID=" + t1serahID + ")," +
                "(select qtyin from T1_Serah where ID=" + t1serahID + ")" + -qty + 
                ",0,(select hpp from T1_Serah where ID=" + t1serahID + ")," +
                "GETDATE(),(select createdby from T1_Serah where ID=" + t1serahID + "),GETDATE()," +
                "(select createdby from T1_Serah where ID=" + t1serahID + "),0,'lari') "+
                "update t1_serah set  lokid=" + lokid + "," + "itemid=" + itemid + "," + " qtyin=" + qty + "," + "qtyout=" + qty + ", tglserah='" +
                tgltrans + "', lastmodifiedtime=getdate() where ID=" + t1serahID + " " ;

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }
        public string UpdateserahBypelarianFullNew(int jemurID, int t1serahID, int lokid, int itemid, int qty, string tgltrans, string Palet)
        {
            string strSQL =
                //"update t1_jemurlg set qtyout=qtyin-qtyout+" + qty + ",itemid=" + itemid + " , lokid=" + lokid + 
                //",lastmodifiedtime=getdate() where ID in (select jemurID from t1_serah where ID=" +
                //t1serahID + ")  " +
                "update t1_serah set qtyout=" + qty + ",itemid=" + itemid + " , lokid=" + lokid + ", tglserah='" + tgltrans
                + "', lastmodifiedtime=getdate() where ID=" + t1serahID ;

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }
        public string UpdateserahBypelarianPartialNew(int jemurID, int t1serahID, int lokid, int itemid, int qty, string tgltrans,string Palet)
        {
            string strSQL =
                //"update t1_jemurlg set qtyout=qtyin-qtyout+" + qty +
                //",lastmodifiedtime=getdate() where ID in (select jemurID from t1_serah where ID=" +
                //t1serahID + ")  " +
                "insert T1_Serah (JemurID,DestID,LokID,ItemID0,ItemID,TglSerah,QtyIn,QtyOut,HPP,CreatedTime," +
                "CreatedBy,LastModifiedTime,LastModifiedBy, status,SFrom )" +
                "values ((select jemurid from T1_Serah where ID=" + t1serahID + ")," +
                "(select destid from T1_Serah where ID=" + t1serahID + "),(select lokid from T1_Serah where ID=" + t1serahID + ")," +
                "(select itemid0 from T1_Serah where ID=" + t1serahID + ")," +
                "(select itemid from T1_Serah where ID=" + t1serahID + ")," +
                "(select tglserah from T1_Serah where ID=" + t1serahID + ")," +
                "(select qtyin from T1_Serah where ID=" + t1serahID + ")" + -qty +
                ",0,(select hpp from T1_Serah where ID=" + t1serahID + ")," +
                "GETDATE(),(select createdby from T1_Serah where ID=" + t1serahID + "),GETDATE()," +
                "(select createdby from T1_Serah where ID=" + t1serahID + "),0,'lari') " +
                "update t1_serah set lokid=" + lokid + "," + "itemid=" + itemid + "," + " qtyin=" + qty + "," + "qtyout=" + qty + ", tglserah='" +
                tgltrans + "', lastmodifiedtime=getdate() where ID=" + t1serahID ;

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }
        public string UpdateAdjustOut(int t1serahID)
        {
            string strSQL = "update t1_serah set qtyout=qtyin where ID=" +t1serahID ;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }

        public string UpdateSerahByDefect(int serahID)
        {
            string strSQL = "update t1_serah set status=1,lastmodifiedtime=getdate() where ID=" + serahID ;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }
        public string UpdateT3RekapByDefect(string tanggal, string partno)
        {
            string strSQL = "update T3_Rekap set status=1 where ID in (" +
                "select ID from (SELECT  A.ID AS DestID, ' ' AS tgljemur, ' ' AS rak, A.TglProduksi, A1.Lokasi AS Lokasidest, A5.PartNo AS PartNodest, " +
            "C.ID, C.ItemID AS ItemIDSer, C.Tgltrans TglSerah, C2.PartNo as PartNoser, C1.Lokasi  Lokasiser,  " +
            "C.QtyIn AS Qtysin, C.QtyOut AS Qtysout, ISNULL(C.HPP, 0) AS HPP, BM_Palet.NoPAlet  " +
            "FROM BM_Destacking AS A left JOIN FC_Items AS A5 ON A.ItemID = A5.ID left JOIN FC_Lokasi AS A1 ON A.LokasiID = A1.ID left JOIN  " +
            "BM_Palet ON A.PaletID = BM_Palet.ID left JOIN FC_Lokasi AS C1 RIGHT OUTER JOIN T3_Rekap AS C ON C1.ID = C.LokID left JOIN  " +
            "FC_Items AS C2 ON C.ItemID = C2.ID ON A.ID = C.DestID  " +
            "WHERE (C2.partno like '%-P-%' ) and  C.status =0 and  C.rowstatus =0 and convert(varchar,C.Tgltrans,112)='" + tanggal + "') A where PartNoser='" + partno + "' )";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }
        public T1_Serah GenerateObjectTotalTransit(SqlDataReader sqlDataReader)
        {
            objT1_Serah = new T1_Serah();
            objT1_Serah.PartnoSer = (sqlDataReader["Partno"]).ToString();
            objT1_Serah.LokasiSer = (sqlDataReader["Lokasi"]).ToString();
            objT1_Serah.QtyOut = Convert.ToInt32(sqlDataReader["Saldo"]);
            return objT1_Serah;
        }

        public T1_Serah GenerateObjectTransit(SqlDataReader sqlDataReader)
        {
            objT1_Serah = new T1_Serah();
            objT1_Serah.TglSerah = Convert.ToDateTime(sqlDataReader["TglSerah"]);
            objT1_Serah.PartnoSer = (sqlDataReader["Partno"]).ToString();
            objT1_Serah.LokasiSer = (sqlDataReader["Lokasi"]).ToString();
            objT1_Serah.QtyOut = Convert.ToInt32(sqlDataReader["Saldo"]);
            objT1_Serah.TglProduksi = Convert.ToDateTime(sqlDataReader["TglProduksi"]);
            return objT1_Serah;
        }

        public ArrayList RetrieveStockTransitV(string criteria, string tglserah, int range, decimal tebal, decimal lebar, decimal panjang, string palet)
        {
            string strtebal=string.Empty;
            if (criteria == string.Empty)
                criteria = " and B.lokasi=' ' ";
            else
                criteria = " and B.lokasi='" + criteria + "' ";
            if (range == 0)
                tglserah = " ";
            else
                tglserah = " convert(char,A.tglserah,112)='" + tglserah + "' and ";
			
            //if (tebal == 4)
            //    strtebal = "C.tebal<=" + tebal.ToString().Replace(",", ".") + " and ";
            //else
			
            // Dirubah tanggal 08 Oktober 2018		
			if (tebal == 4)
            {
                strtebal = "C.tebal in (" + tebal.ToString().Replace(",", ".") + " ,3.5)" + " and " + " C.lebar=" + lebar.ToString().Replace(",", ".") + " and " + " C.Panjang=" + panjang.ToString().Replace(",", ".") + " and ";
            }
            else if (tebal != 4)
            {
                strtebal = "C.tebal=" + tebal.ToString().Replace(",", ".") + " and " + " C.lebar=" + lebar.ToString().Replace(",", ".") + " and " + " C.Panjang=" + panjang.ToString().Replace(",", ".") + " and ";
            }
			// End Tambahan
			
			// Logic Lama
            //strtebal = "C.tebal=" + tebal.ToString().Replace(",", ".") + " and " + " C.lebar=" + lebar.ToString().Replace(",", ".") + " and " + " //C.Panjang=" + panjang.ToString().Replace(",", ".") + " and ";
			
			
            if (tebal == 0)
                strtebal = string.Empty;
            if (palet != string.Empty)
                criteria = criteria + " and P.nopalet='" + palet + "' ";
            string strSQL = "SELECT A.Destid,A.itemID as ItemIDSer,A.LokID,isnull(C.GroupID,0) as GroupID,B.Lokasi, A.QtyIn-A.QtyOut  as QtysIn,A.QtyOut as QtysOut," +
                "A.ID,isnull(A.HPP,0) as HPP,C.tebal,C.lebar,C.panjang,P.nopalet, " +
                "D.tglproduksi,A.TglSerah,C.Partno as PartnoSer,F.formulaName as formula,B1.partno as PartnoDest,B.Lokasi as LokasiSer " +
                "FROM  T1_Serah AS A INNER JOIN FC_Items AS C ON A.ItemID = C.ID INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID left join bm_destacking D on A.destid=D.ID " +
                "left join fc_items B1 on B1.ID= D.itemID left join bm_formula F on D.formulaID=F.ID left join bm_palet P on P.ID=D.paletID where " + strtebal + tglserah + " A.status>-1 and isnull(A.destid,0)>0 and A.QtyIn>A.QtyOut " + criteria + "order by A.ID";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();
            if (sqlDataReader.HasRows)
            { while (sqlDataReader.Read()) { arrT1_Serah.Add(GenerateObjectMutasiTransit(sqlDataReader)); } }
            else arrT1_Serah.Add(new T1_Serah()); 
            return arrT1_Serah;
        }
        public ArrayList RetrieveStockTransit(string criteria, string tglserah, int range, decimal tebal, string palet)
        {
            string strtebal = string.Empty;
            if (criteria == string.Empty)
                criteria = " and B.lokasi=' ' ";
            else
                criteria = " and B.lokasi='" + criteria + "' ";
            if (range == 0)
                tglserah = " ";
            else
                tglserah = " convert(char,A.tglserah,112)='" + tglserah + "' and ";
            //if (tebal == 4)
            //    strtebal = "C.tebal<=" + tebal.ToString().Replace(",", ".") + " and ";
            //else
            strtebal = "C.tebal=" + tebal.ToString().Replace(",", ".") + " and " ;
            if (tebal == 0)
                strtebal = string.Empty;
            if (palet != string.Empty)
                criteria = criteria + " and P.nopalet='" + palet + "' ";
            string strSQL = "SELECT A.Destid,A.itemID as ItemIDSer,A.LokID,isnull(C.GroupID,0) as GroupID,B.Lokasi, A.QtyIn-A.QtyOut  as QtysIn,A.QtyOut as QtysOut," +
                "A.ID,isnull(A.HPP,0) as HPP,C.tebal,C.lebar,C.panjang,P.nopalet, " +
                "D.tglproduksi,A.TglSerah,C.Partno as PartnoSer,F.formulaName as formula,B1.partno as PartnoDest,B.Lokasi as LokasiSer " +
                "FROM  T1_Serah AS A INNER JOIN FC_Items AS C ON A.ItemID = C.ID INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID left join bm_destacking D on A.destid=D.ID " +
                "left join fc_items B1 on B1.ID= D.itemID left join bm_formula F on D.formulaID=F.ID left join bm_palet P on P.ID=D.paletID where " + strtebal + tglserah + " A.status>-1 and isnull(A.destid,0)>0 and A.QtyIn>A.QtyOut " + criteria + "order by A.ID";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();
            if (sqlDataReader.HasRows)
            { while (sqlDataReader.Read()) { arrT1_Serah.Add(GenerateObjectMutasiTransit(sqlDataReader)); } }
            else arrT1_Serah.Add(new T1_Serah());
            return arrT1_Serah;
        }
        public ArrayList RetrieveStockPelarian(string partno)
        {
            if (partno.Trim() == string.Empty)
                partno = "";
            else
                partno = " and C.PartNo='" + partno + "'";
            string strSQL = "SELECT A.Destid,A.itemID as ItemIDSer,A.LokID,isnull(C.GroupID,0) as GroupID,B.Lokasi, A.QtyIn-A.QtyOut  as QtysIn,A.QtyOut as QtysOut, " +
                "A.ID,isnull(A.HPP,0) as HPP,C.tebal,C.lebar,C.panjang,P.nopalet, D.tglproduksi,A.TglSerah,C.Partno as PartnoSer,F.formulaName as formula, " +
                "B1.partno as PartnoDest,B.Lokasi as LokasiSer  " +
                "FROM  T1_serah AS A INNER JOIN FC_Items AS C ON A.ItemID0 = C.ID INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID left join bm_destacking D on A.destid=D.ID  " +
                "left join fc_items B1 on B1.ID= D.itemID left join bm_formula F on D.formulaID=F.ID left join bm_palet P on P.ID=D.paletID  " +
                "where A.QtyIn>A.QtyOut and A.sfrom ='lari' and A.status>-1 and C.tebal in (8,9) and isnull(A.destid,0)>0 " + partno + " order by C.Partno,A.tglserah";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();
            if (sqlDataReader.HasRows)
            { while (sqlDataReader.Read()) { arrT1_Serah.Add(GenerateObjectMutasiTransit(sqlDataReader)); } }
            else arrT1_Serah.Add(new T1_Serah());
            return arrT1_Serah;
        }
        public ArrayList RetrieveStockPelarianByKriteria(string kriteria)
        {
            arrT1_Serah = new ArrayList();
            if (kriteria == string.Empty)
            {
                arrT1_Serah.Add(new T1_Serah());
                return arrT1_Serah;
            }
            string strSQL = "SELECT A.Destid,A.itemID as ItemIDSer,A.LokID,isnull(C.GroupID,0) as GroupID,B.Lokasi, A.QtyIn-A.QtyOut  as QtysIn,A.QtyOut as QtysOut, " +
                "A.ID,isnull(A.HPP,0) as HPP,C.tebal,C.lebar,C.panjang,P.nopalet, D.tglproduksi,A.TglSerah,C.Partno as PartnoSer,F.formulaName as formula, " +
                "B1.partno as PartnoDest,B.Lokasi as LokasiSer  " +
                "FROM  T1_serah AS A INNER JOIN FC_Items AS C ON A.ItemID0 = C.ID INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID left join bm_destacking D on A.destid=D.ID  " +
                "left join fc_items B1 on B1.ID= D.itemID left join bm_formula F on D.formulaID=F.ID left join bm_palet P on P.ID=D.paletID  " +
                "where A.QtyIn>A.QtyOut and A.sfrom ='lari' and A.status>-1 and isnull(A.destid,0)>0 " + kriteria + " order by C.Partno,A.tglserah";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            
            if (sqlDataReader.HasRows)
            { while (sqlDataReader.Read()) { arrT1_Serah.Add(GenerateObjectMutasiTransit(sqlDataReader)); } }
            else arrT1_Serah.Add(new T1_Serah());
            return arrT1_Serah;
        }
        public ArrayList RetrieveStockPelarianListPlank(string partno)
        {
            if (partno.Trim() == string.Empty)
                partno = "";
            else
                partno = " and C.PartNo='" + partno + "'";
            string strSQL = "SELECT A.Destid,A.itemID as ItemIDSer,A.LokID,isnull(C.GroupID,0) as GroupID,B.Lokasi, A.QtyIn-A.QtyOut  as QtysIn,A.QtyOut as QtysOut, " +
                "A.ID,isnull(A.HPP,0) as HPP,C.tebal,C.lebar,C.panjang,P.nopalet, D.tglproduksi,A.TglSerah,C.Partno as PartnoSer,F.formulaName as formula, " +
                "B1.partno as PartnoDest,B.Lokasi as LokasiSer  " +
                "FROM  T1_serah AS A INNER JOIN FC_Items AS C ON A.ItemID0 = C.ID INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID left join bm_destacking D on A.destid=D.ID  " +
                "left join fc_items B1 on B1.ID= D.itemID left join bm_formula F on D.formulaID=F.ID left join bm_palet P on P.ID=D.paletID  " +
                "where A.QtyIn>A.QtyOut and A.sfrom ='lari' and A.status>-1 and isnull(A.destid,0)>0 " + partno + " order by C.Partno,A.tglserah";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();
            if (sqlDataReader.HasRows)
            { while (sqlDataReader.Read()) { arrT1_Serah.Add(GenerateObjectMutasiTransit(sqlDataReader)); } }
            else arrT1_Serah.Add(new T1_Serah());
            return arrT1_Serah;
        }
        public ArrayList RetrieveStockListplank1(string partno)
        {
            //if (partno.Trim() == string.Empty)
            //    partno = "";
            //else
                partno = " and C.PartNo='" + partno + "'";
            string strSQL = "SELECT A.Destid,A.itemID as ItemIDSer,A.LokID,isnull(C.GroupID,0) as GroupID,B.Lokasi, A.QtyIn-A.QtyOut  as QtysIn,A.QtyOut as QtysOut, " +
                "A.ID,isnull(A.HPP,0) as HPP,C.tebal,C.lebar,C.panjang,P.nopalet, D.tglproduksi,A.tglTrans TglSerah,C.Partno as PartnoSer,F.formulaName as formula, " +
                "B1.partno as PartnoDest,B.Lokasi as LokasiSer  " +
                "FROM  T1_ListPlank AS A INNER JOIN FC_Items AS C ON A.ItemID0 = C.ID INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID left join bm_destacking D on A.destid=D.ID  " +
                "left join fc_items B1 on B1.ID= D.itemID left join bm_formula F on D.formulaID=F.ID inner join bm_palet P on P.ID=D.paletID  " +
                "where A.QtyIn>A.QtyOut and A.status>-1 and isnull(A.destid,0)>0 " + partno + " order by C.Partno,A.tglTrans";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();
            if (sqlDataReader.HasRows)
            { while (sqlDataReader.Read()) { arrT1_Serah.Add(GenerateObjectMutasiTransit(sqlDataReader)); } }
            else arrT1_Serah.Add(new T1_Serah());
            return arrT1_Serah;
        }
        public ArrayList RetrieveStockLR1Listplank1(string partno)
        {
            //if (partno.Trim() == string.Empty)
            //    partno = "";
            //else
            partno = " and C.PartNo='" + partno + "'";
            string strSQL = "SELECT A.Destid,A.itemID as ItemIDSer,A.LokID,isnull(C.GroupID,0) as GroupID,B.Lokasi, A.QtyIn-A.QtyOut  as QtysIn,A.QtyOut as QtysOut, " +
                "A.ID,isnull(A.HPP,0) as HPP,C.tebal,C.lebar,C.panjang,isnull(P.nopalet,0)nopalet, isnull(D.tglproduksi,'1/1/1900')tglproduksi,A.tglTrans TglSerah,C.Partno as PartnoSer, "+
                "isnull(F.formulaName,'-') as formula, isnull(B1.partno,'-') as PartnoDest,B.Lokasi as LokasiSer  " +
                "FROM  T1_LR1_Listplank AS A left JOIN FC_Items AS C ON A.ItemID = C.ID left JOIN FC_Lokasi AS B ON A.LokID = B.ID left join bm_destacking D on A.destid=D.ID  " +
                "left join fc_items B1 on B1.ID= D.itemID left join bm_formula F on D.formulaID=F.ID left join bm_palet P on P.ID=D.paletID  " +
                "where A.QtyIn>A.QtyOut and A.status>-1  " + partno + " order by C.Partno,A.tglTrans";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();
            if (sqlDataReader.HasRows)
            { while (sqlDataReader.Read()) { arrT1_Serah.Add(GenerateObjectMutasiTransit(sqlDataReader)); } }
            else arrT1_Serah.Add(new T1_Serah());
            return arrT1_Serah;
        }
        public ArrayList RetrieveStockLR2Listplank1(string partno)
        {
            //if (partno.Trim() == string.Empty)
            //    partno = "";
            //else
            partno = " and C.PartNo='" + partno + "'";
            string strSQL = "SELECT A.Destid,A.itemID as ItemIDSer,A.LokID,isnull(C.GroupID,0) as GroupID,B.Lokasi, A.QtyIn-A.QtyOut  as QtysIn,A.QtyOut as QtysOut,  " +
                "A.ID,isnull(A.HPP,0) as HPP,C.tebal,C.lebar,C.panjang,'-' nopalet, '1/1/1900' tglproduksi,A.tglTrans TglSerah,C.Partno as PartnoSer, '' formula,  " +
                "isnull(B1.partno,'-') as PartnoDest,B.Lokasi as LokasiSer   " +
                "FROM  T1_LR2_Listplank AS A INNER JOIN FC_Items AS C ON A.ItemID = C.ID INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID  " +
                "left join fc_items B1 on B1.ID= A.itemID " +
                "where A.QtyIn>A.QtyOut and A.status>-1 " + partno + " order by C.Partno,A.tglTrans";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();
            if (sqlDataReader.HasRows)
            { while (sqlDataReader.Read()) { arrT1_Serah.Add(GenerateObjectMutasiTransit(sqlDataReader)); } }
            else arrT1_Serah.Add(new T1_Serah());
            return arrT1_Serah;
        }
        public ArrayList RetrieveStockLR3Listplank1(string partno)
        {
            //if (partno.Trim() == string.Empty)
            //    partno = "";
            //else
            partno = " and C.PartNo='" + partno + "'";
            string strSQL = "SELECT A.Destid,A.itemID as ItemIDSer,A.LokID,isnull(C.GroupID,0) as GroupID,B.Lokasi, A.QtyIn-A.QtyOut  as QtysIn,A.QtyOut as QtysOut, " +
                "A.ID,isnull(A.HPP,0) as HPP,C.tebal,C.lebar,C.panjang,isnull(P.nopalet,0)nopalet, isnull(D.tglproduksi,'1/1/1900')tglproduksi,A.tglTrans TglSerah,C.Partno as PartnoSer, " +
                "isnull(F.formulaName,'-') as formula, isnull(B1.partno,'-') as PartnoDest,B.Lokasi as LokasiSer  " +
                "FROM  T1_LR3_Listplank AS A INNER JOIN FC_Items AS C ON A.ItemID = C.ID INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID left join bm_destacking D on A.destid=D.ID  " +
                "left join fc_items B1 on B1.ID= D.itemID left join bm_formula F on D.formulaID=F.ID left join bm_palet P on P.ID=D.paletID  " +
                "where A.QtyIn>A.QtyOut and A.status>-1 " + partno + " order by C.Partno,A.tglTrans";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();
            if (sqlDataReader.HasRows)
            { while (sqlDataReader.Read()) { arrT1_Serah.Add(GenerateObjectMutasiTransit(sqlDataReader)); } }
            else arrT1_Serah.Add(new T1_Serah());
            return arrT1_Serah;
        }
        public ArrayList RetrieveStockRuningSaw(string partno)
        {
            //if (partno.Trim() == string.Empty)
            //    partno = "";
            //else
            partno = " and C.PartNo='" + partno + "'";
            string strSQL = "SELECT A.Destid,A.itemID as ItemIDSer,A.LokID,isnull(C.GroupID,0) as GroupID,'RuningSaw' Lokasi, A.QtyIn-A.QtyOut  as QtysIn,A.QtyOut as QtysOut, " +
                "A.ID,isnull(A.HPP,0) as HPP,C.tebal,C.lebar,C.panjang,P.nopalet, D.tglproduksi,A.tglTrans TglSerah,C.Partno as PartnoSer,F.formulaName as formula, " +
                "B1.partno as PartnoDest,'RuningSaw' LokasiSer  " +
                "FROM  T1_RuningSaw AS A INNER JOIN FC_Items AS C ON A.ItemID = C.ID INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID left join bm_destacking D on A.destid=D.ID " +
                "left join fc_items B1 on B1.ID= A.itemID0 left join bm_formula F on D.formulaID=F.ID inner join bm_palet P on P.ID=D.paletID  " +
                "where A.QtyIn>A.QtyOut and A.rowstatus>-1 and isnull(A.destid,0)>0 " + partno + " order by C.Partno,A.tglTrans";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();
            if (sqlDataReader.HasRows)
            { while (sqlDataReader.Read()) { arrT1_Serah.Add(GenerateObjectMutasiTransit(sqlDataReader)); } }
            else arrT1_Serah.Add(new T1_Serah());
            return arrT1_Serah;
        }
        public ArrayList RetrieveStockLR1RuningSaw(string partno)
        {
            //if (partno.Trim() == string.Empty)
            //    partno = "";
            //else
            partno = " and C.PartNo='" + partno + "'";
            string strSQL = "SELECT A.Destid,A.itemID as ItemIDSer,A.LokID,isnull(C.GroupID,0) as GroupID,'RuningSaw' Lokasi, A.QtyIn-A.QtyOut  as QtysIn,A.QtyOut as QtysOut, " +
                "A.ID,isnull(A.HPP,0) as HPP,C.tebal,C.lebar,C.panjang,isnull(P.nopalet,0)nopalet, isnull(D.tglproduksi,'1/1/1900')tglproduksi,A.tglTrans TglSerah,C.Partno as PartnoSer, " +
                "isnull(F.formulaName,'-') as formula, isnull(B1.partno,'-') as PartnoDest,'RuningSaw'  LokasiSer  " +
                "FROM  T1_LR1_RuningSaw AS A INNER JOIN FC_Items AS C ON A.ItemID = C.ID INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID left join bm_destacking D on A.destid=D.ID " +
                "left join fc_items B1 on B1.ID= A.itemID0 left join bm_formula F on D.formulaID=F.ID left join bm_palet P on P.ID=D.paletID  " +
                "where A.QtyIn>A.QtyOut and A.rowstatus>-1 " + partno + " order by C.Partno,A.tglTrans";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();
            if (sqlDataReader.HasRows)
            { while (sqlDataReader.Read()) { arrT1_Serah.Add(GenerateObjectMutasiTransit(sqlDataReader)); } }
            else arrT1_Serah.Add(new T1_Serah());
            return arrT1_Serah;
        }
        public ArrayList RetrieveStockLR2RuningSaw(string partno)
        {
            //if (partno.Trim() == string.Empty)
            //    partno = "";
            //else
            partno = " and C.PartNo='" + partno + "'";
            string strSQL = "SELECT A.Destid,A.itemID as ItemIDSer,A.LokID,isnull(C.GroupID,0) as GroupID,'RuningSaw' Lokasi, A.QtyIn-A.QtyOut  as QtysIn,A.QtyOut as QtysOut, " + 
            "A.ID,isnull(A.HPP,0) as HPP,C.tebal,C.lebar,C.panjang,''nopalet, '1/1/1900' tglproduksi,A.tglTrans TglSerah, " +
            "C.Partno as PartnoSer,'' as formula, isnull(B1.partno,'-') as PartnoDest,'RuningSaw'  as LokasiSer   " +
            "FROM  T1_LR2_RuningSaw AS A INNER JOIN FC_Items AS C ON A.ItemID = C.ID INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID  " +
            "left join fc_items B1 on B1.ID= A.itemID0   where A.QtyIn>A.QtyOut and A.rowstatus>-1  " + partno + " order by C.Partno,A.tglTrans";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();
            if (sqlDataReader.HasRows)
            { while (sqlDataReader.Read()) { arrT1_Serah.Add(GenerateObjectMutasiTransit(sqlDataReader)); } }
            else arrT1_Serah.Add(new T1_Serah());
            return arrT1_Serah;
        }
        public ArrayList RetrieveStockLR3RuningSaw(string partno)
        {
            //if (partno.Trim() == string.Empty)
            //    partno = "";
            //else
            partno = " and C.PartNo='" + partno + "'";
            string strSQL = "SELECT A.Destid,A.itemID as ItemIDSer,A.LokID,isnull(C.GroupID,0) as GroupID,'RuningSaw' Lokasi, A.QtyIn-A.QtyOut  as QtysIn,A.QtyOut as QtysOut, " +
                "A.ID,isnull(A.HPP,0) as HPP,C.tebal,C.lebar,C.panjang,isnull(P.nopalet,0)nopalet, isnull(D.tglproduksi,'1/1/1900')tglproduksi,A.tglTrans TglSerah,C.Partno as PartnoSer, " +
                "isnull(F.formulaName,'-') as formula, isnull(B1.partno,'-') as PartnoDest,'RuningSaw'  as LokasiSer  " +
                "FROM  T1_LR3_RuningSaw AS A INNER JOIN FC_Items AS C ON A.ItemID = C.ID INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID left join bm_destacking D on A.destid=D.ID " +
                "left join fc_items B1 on B1.ID= A.itemID0 left join bm_formula F on D.formulaID=F.ID left join bm_palet P on P.ID=D.paletID  " +
                "where A.QtyIn>A.QtyOut and A.rowstatus>-1  " + partno + " order by C.Partno,A.tglTrans";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();
            if (sqlDataReader.HasRows)
            { while (sqlDataReader.Read()) { arrT1_Serah.Add(GenerateObjectMutasiTransit(sqlDataReader)); } }
            else arrT1_Serah.Add(new T1_Serah());
            return arrT1_Serah;
        }
        public ArrayList RetrieveStockBevel(string partno)
        {
            //if (partno.Trim() == string.Empty)
            //    partno = "";
            //else
            partno = " and C.PartNo='" + partno + "'";
            string strSQL = "SELECT A.Destid,A.itemID as ItemIDSer,A.LokID,isnull(C.GroupID,0) as GroupID,'Bevel/CNC'  Lokasi, A.QtyIn-A.QtyOut  as QtysIn,A.QtyOut as QtysOut, " +
                "A.ID,isnull(A.HPP,0) as HPP,C.tebal,C.lebar,C.panjang,P.nopalet, D.tglproduksi,A.tglTrans TglSerah,C.Partno as PartnoSer,F.formulaName as formula, " +
                "B1.partno as PartnoDest,'Bevel/CNC' as LokasiSer  " +
                "FROM  T1_Bevel AS A INNER JOIN FC_Items AS C ON A.ItemID = C.ID INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID left join bm_destacking D on A.destid=D.ID  " +
                "left join fc_items B1 on B1.ID= A.itemID0 left join bm_formula F on D.formulaID=F.ID inner join bm_palet P on P.ID=D.paletID  " +
                "where A.QtyIn>A.QtyOut and A.rowstatus>-1 and isnull(A.destid,0)>0 " + partno + " order by C.Partno,A.tglTrans";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();
            if (sqlDataReader.HasRows)
            { while (sqlDataReader.Read()) { arrT1_Serah.Add(GenerateObjectMutasiTransit(sqlDataReader)); } }
            else arrT1_Serah.Add(new T1_Serah());
            return arrT1_Serah;
        }
        public ArrayList RetrieveStockLR1Bevel(string partno)
        {
            //if (partno.Trim() == string.Empty)
            //    partno = "";
            //else
            partno = " and C.PartNo='" + partno + "'";
            string strSQL = "SELECT A.Destid,A.itemID as ItemIDSer,A.LokID,isnull(C.GroupID,0) as GroupID,'Bevel/CNC' Lokasi, A.QtyIn-A.QtyOut  as QtysIn,A.QtyOut as QtysOut, " +
                "A.ID,isnull(A.HPP,0) as HPP,C.tebal,C.lebar,C.panjang,isnull(P.nopalet,0)nopalet, isnull(D.tglproduksi,'1/1/1900')tglproduksi,A.tglTrans TglSerah,C.Partno as PartnoSer, " +
                "isnull(F.formulaName,'-') as formula, isnull(B1.partno,'-') as PartnoDest,'Bevel/CNC' as LokasiSer  " +
                "FROM  T1_LR1_Bevel AS A INNER JOIN FC_Items AS C ON A.ItemID = C.ID INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID left join bm_destacking D on A.destid=D.ID  " +
                "left join fc_items B1 on B1.ID= A.itemID0 left join bm_formula F on D.formulaID=F.ID left join bm_palet P on P.ID=D.paletID  " +
                "where A.QtyIn>A.QtyOut and A.rowstatus>-1  " + partno + " order by C.Partno,A.tglTrans";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();
            if (sqlDataReader.HasRows)
            { while (sqlDataReader.Read()) { arrT1_Serah.Add(GenerateObjectMutasiTransit(sqlDataReader)); } }
            else arrT1_Serah.Add(new T1_Serah());
            return arrT1_Serah;
        }
        public ArrayList RetrieveStockLR2Bevel(string partno)
        {
            //if (partno.Trim() == string.Empty)
            //    partno = "";
            //else
            partno = " and C.PartNo='" + partno + "'";
            string strSQL = "SELECT A.Destid,A.itemID as ItemIDSer,A.LokID,isnull(C.GroupID,0) as GroupID,'Bevel/CNC'  Lokasi, A.QtyIn-A.QtyOut  as QtysIn,A.QtyOut as QtysOut, " +
                "A.ID,isnull(A.HPP,0) as HPP,C.tebal,C.lebar,C.panjang,isnull(P.nopalet,0)nopalet, isnull(D.tglproduksi,'1/1/1900')tglproduksi,A.tglTrans TglSerah,C.Partno as PartnoSer, " +
                "isnull(F.formulaName,'-') as formula, isnull(B1.partno,'-') as PartnoDest,'Bevel/CNC' as LokasiSer  " +
                "FROM  T1_LR2_Bevel AS A INNER JOIN FC_Items AS C ON A.ItemID = C.ID INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID left join bm_destacking D on A.destid=D.ID  " +
                "left join fc_items B1 on B1.ID= A.itemID0 left join bm_formula F on D.formulaID=F.ID left join bm_palet P on P.ID=D.paletID  " +
                "where A.QtyIn>A.QtyOut and A.rowstatus>-1  " + partno + " order by C.Partno,A.tglTrans";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();
            if (sqlDataReader.HasRows)
            { while (sqlDataReader.Read()) { arrT1_Serah.Add(GenerateObjectMutasiTransit(sqlDataReader)); } }
            else arrT1_Serah.Add(new T1_Serah());
            return arrT1_Serah;
        }
        public ArrayList RetrieveStockLR3Bevel(string partno)
        {
            //if (partno.Trim() == string.Empty)
            //    partno = "";
            //else
            partno = " and C.PartNo='" + partno + "'";
            string strSQL = "SELECT A.Destid,A.itemID as ItemIDSer,A.LokID,isnull(C.GroupID,0) as GroupID,'Bevel/CNC' Lokasi, A.QtyIn-A.QtyOut  as QtysIn,A.QtyOut as QtysOut, " +
                "A.ID,isnull(A.HPP,0) as HPP,C.tebal,C.lebar,C.panjang,isnull(P.nopalet,0)nopalet, isnull(D.tglproduksi,'1/1/1900')tglproduksi,A.tglTrans TglSerah,C.Partno as PartnoSer, " +
                "isnull(F.formulaName,'-') as formula, isnull(B1.partno,'-') as PartnoDest,'Bevel/CNC' as LokasiSer  " +
                "FROM  T1_LR3_Bevel AS A INNER JOIN FC_Items AS C ON A.ItemID = C.ID INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID left join bm_destacking D on A.destid=D.ID  " +
                "left join fc_items B1 on B1.ID= A.itemID0 left join bm_formula F on D.formulaID=F.ID left join bm_palet P on P.ID=D.paletID  " +
                "where A.QtyIn>A.QtyOut and A.rowstatus>-1  " + partno + " order by C.Partno,A.tglTrans";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();
            if (sqlDataReader.HasRows)
            { while (sqlDataReader.Read()) { arrT1_Serah.Add(GenerateObjectMutasiTransit(sqlDataReader)); } }
            else arrT1_Serah.Add(new T1_Serah());
            return arrT1_Serah;
        }
        public ArrayList RetrieveStockPrint(string partno)
        {
            //if (partno.Trim() == string.Empty)
            //    partno = "";
            //else
            partno = " and C.PartNo='" + partno + "'";
            string strSQL = "SELECT A.Destid,A.itemID as ItemIDSer,A.LokID,isnull(C.GroupID,0) as GroupID,B.Lokasi, A.QtyIn-A.QtyOut  as QtysIn,A.QtyOut as QtysOut, " +
                "A.ID,isnull(A.HPP,0) as HPP,C.tebal,C.lebar,C.panjang,P.nopalet, D.tglproduksi,A.tglTrans TglSerah,C.Partno as PartnoSer,F.formulaName as formula, " +
                "B1.partno as PartnoDest,B.Lokasi as LokasiSer  " +
                "FROM  T1_Print AS A INNER JOIN FC_Items AS C ON A.ItemID = C.ID INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID left join bm_destacking D on A.destid=D.ID  " +
                "left join fc_items B1 on B1.ID= A.itemID0 left join bm_formula F on D.formulaID=F.ID inner join bm_palet P on P.ID=D.paletID  " +
                "where A.QtyIn>A.QtyOut and A.rowstatus>-1 and isnull(A.destid,0)>0 " + partno + " order by C.Partno,A.tglTrans";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();
            if (sqlDataReader.HasRows)
            { while (sqlDataReader.Read()) { arrT1_Serah.Add(GenerateObjectMutasiTransit(sqlDataReader)); } }
            else arrT1_Serah.Add(new T1_Serah());
            return arrT1_Serah;
        }
        public ArrayList RetrieveStockStraping(string partno)
        {
            //if (partno.Trim() == string.Empty)
            //    partno = "";
            //else
            partno = " and C.PartNo='" + partno + "'";
            string strSQL = "SELECT A.Destid,A.itemID as ItemIDSer,A.LokID,isnull(C.GroupID,0) as GroupID,'Straping' Lokasi, A.QtyIn-A.QtyOut  as QtysIn,A.QtyOut as QtysOut, " +
                "A.ID,isnull(A.HPP,0) as HPP,C.tebal,C.lebar,C.panjang,P.nopalet, D.tglproduksi,A.tglTrans TglSerah,C.Partno as PartnoSer,F.formulaName as formula, " +
                "B1.partno as PartnoDest,'Straping' as LokasiSer  " +
                "FROM  T1_Straping AS A INNER JOIN FC_Items AS C ON A.ItemID = C.ID INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID left join bm_destacking D on A.destid=D.ID  " +
                "left join fc_items B1 on B1.ID= A.itemID0 left join bm_formula F on D.formulaID=F.ID inner join bm_palet P on P.ID=D.paletID  " +
                "where A.QtyIn>A.QtyOut and A.rowstatus>-1 and isnull(A.destid,0)>0 " + partno + " order by C.Partno,A.tglTrans";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();
            if (sqlDataReader.HasRows)
            { while (sqlDataReader.Read()) { arrT1_Serah.Add(GenerateObjectMutasiTransit(sqlDataReader)); } }
            else arrT1_Serah.Add(new T1_Serah());
            return arrT1_Serah;
        }
        public ArrayList RetrieveStockLR1Straping(string partno)
        {
            //if (partno.Trim() == string.Empty)
            //    partno = "";
            //else
            partno = " and C.PartNo='" + partno + "'";
            string strSQL = "SELECT A.Destid,A.itemID as ItemIDSer,A.LokID,isnull(C.GroupID,0) as GroupID,'Straping' Lokasi, A.QtyIn-A.QtyOut  as QtysIn,A.QtyOut as QtysOut, " +
                "A.ID,isnull(A.HPP,0) as HPP,C.tebal,C.lebar,C.panjang,isnull(P.nopalet,0)nopalet, isnull(D.tglproduksi,'1/1/1900')tglproduksi,A.tglTrans TglSerah,C.Partno as PartnoSer, " +
                "isnull(F.formulaName,'-') as formula, isnull(B1.partno,'-') as PartnoDest,'Straping' as LokasiSer  " +
                "FROM  T1_LR1_Straping AS A INNER JOIN FC_Items AS C ON A.ItemID = C.ID INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID left join bm_destacking D on A.destid=D.ID  " +
                "left join fc_items B1 on B1.ID= A.itemID0 left join bm_formula F on D.formulaID=F.ID left join bm_palet P on P.ID=D.paletID  " +
                "where A.QtyIn>A.QtyOut and A.rowstatus>-1  " + partno + " order by C.Partno,A.tglTrans";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();
            if (sqlDataReader.HasRows)
            { while (sqlDataReader.Read()) { arrT1_Serah.Add(GenerateObjectMutasiTransit(sqlDataReader)); } }
            else arrT1_Serah.Add(new T1_Serah());
            return arrT1_Serah;
        }
        public ArrayList RetrieveStockLR2Straping(string partno)
        {
            //if (partno.Trim() == string.Empty)
            //    partno = "";
            //else
            partno = " and C.PartNo='" + partno + "'";
            string strSQL = "SELECT A.Destid,A.itemID as ItemIDSer,A.LokID,isnull(C.GroupID,0) as GroupID,'Straping' Lokasi, A.QtyIn-A.QtyOut  as QtysIn,A.QtyOut as QtysOut, " +
                "A.ID,isnull(A.HPP,0) as HPP,C.tebal,C.lebar,C.panjang,isnull(P.nopalet,0)nopalet, isnull(D.tglproduksi,'1/1/1900')tglproduksi,A.tglTrans TglSerah,C.Partno as PartnoSer, " +
                "isnull(F.formulaName,'-') as formula, isnull(B1.partno,'-') as PartnoDest,'Straping' as LokasiSer  " +
                "FROM  T1_LR2_Straping AS A INNER JOIN FC_Items AS C ON A.ItemID = C.ID INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID left join bm_destacking D on A.destid=D.ID  " +
                "left join fc_items B1 on B1.ID= A.itemID0 left join bm_formula F on D.formulaID=F.ID left join bm_palet P on P.ID=D.paletID  " +
                "where A.QtyIn>A.QtyOut and A.rowstatus>-1 " + partno + " order by C.Partno,A.tglTrans";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();
            if (sqlDataReader.HasRows)
            { while (sqlDataReader.Read()) { arrT1_Serah.Add(GenerateObjectMutasiTransit(sqlDataReader)); } }
            else arrT1_Serah.Add(new T1_Serah());
            return arrT1_Serah;
        }
        public ArrayList RetrieveStockLR3Straping(string partno)
        {
            //if (partno.Trim() == string.Empty)
            //    partno = "";
            //else
            partno = " and C.PartNo='" + partno + "'";
            string strSQL = "SELECT A.Destid,A.itemID as ItemIDSer,A.LokID,isnull(C.GroupID,0) as GroupID,'Straping' Lokasi, A.QtyIn-A.QtyOut  as QtysIn,A.QtyOut as QtysOut, " +
                "A.ID,isnull(A.HPP,0) as HPP,C.tebal,C.lebar,C.panjang,isnull(P.nopalet,0)nopalet, isnull(D.tglproduksi,'1/1/1900')tglproduksi,A.tglTrans TglSerah,C.Partno as PartnoSer, " +
                "isnull(F.formulaName,'-') as formula, isnull(B1.partno,'-') as PartnoDest,'Straping' as LokasiSer  " +
                "FROM  T1_LR3_Straping AS A INNER JOIN FC_Items AS C ON A.ItemID = C.ID INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID left join bm_destacking D on A.destid=D.ID  " +
                "left join fc_items B1 on B1.ID= A.itemID0 left join bm_formula F on D.formulaID=F.ID left join bm_palet P on P.ID=D.paletID  " +
                "where A.QtyIn>A.QtyOut and A.rowstatus>-1 " + partno + " order by C.Partno,A.tglTrans";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();
            if (sqlDataReader.HasRows)
            { while (sqlDataReader.Read()) { arrT1_Serah.Add(GenerateObjectMutasiTransit(sqlDataReader)); } }
            else arrT1_Serah.Add(new T1_Serah());
            return arrT1_Serah;
        }
        public T1_Serah GenerateObjectSerah(SqlDataReader sqlDataReader)
        {
            objT1_Serah = new T1_Serah();
            objT1_Serah.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objT1_Serah.DestID = Convert.ToInt32(sqlDataReader["DestID"]);
            objT1_Serah.ItemIDSer = Convert.ToInt32(sqlDataReader["ItemIDSer"]);
            objT1_Serah.TglProduksi = Convert.ToDateTime(sqlDataReader["TglProduksi"]);
            //objT1_Serah.Palet = (sqlDataReader["NoPalet"]).ToString();
            objT1_Serah.Formula = (sqlDataReader["Formula"]).ToString();
            objT1_Serah.PartnoDest = (sqlDataReader["PartnoDest"]).ToString();
            objT1_Serah.TglSerah = Convert.ToDateTime(sqlDataReader["TglSerah"]);
            objT1_Serah.PartnoSer = (sqlDataReader["PartnoSer"]).ToString();
            objT1_Serah.LokasiSer = (sqlDataReader["LokasiSer"]).ToString();
            objT1_Serah.QtyIn = Convert.ToInt32(sqlDataReader["QtysIn"]);
            objT1_Serah.QtyOut = Convert.ToInt32(sqlDataReader["QtysOut"]);
            objT1_Serah.HPP  = Convert.ToDecimal(sqlDataReader["HPP"]);
            return objT1_Serah;
        }
        public T1_Serah GenerateObjectSerah1(SqlDataReader sqlDataReader)
        {
            objT1_Serah = new T1_Serah();
            objT1_Serah.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objT1_Serah.DestID = Convert.ToInt32(sqlDataReader["DestID"]);
            objT1_Serah.ItemIDSer = Convert.ToInt32(sqlDataReader["ItemIDSer"]);
            objT1_Serah.TglProduksi = Convert.ToDateTime(sqlDataReader["TglProduksi"]);
            //objT1_Serah.Palet = (sqlDataReader["NoPalet"]).ToString();
            objT1_Serah.Formula = (sqlDataReader["Formula"]).ToString();
            objT1_Serah.PartnoDest = (sqlDataReader["PartnoDest"]).ToString();
            objT1_Serah.TglSerah = Convert.ToDateTime(sqlDataReader["TglSerah"]);
            objT1_Serah.PartnoSer = (sqlDataReader["PartnoSer"]).ToString();
            objT1_Serah.LokasiSer = (sqlDataReader["LokasiSer"]).ToString();
            objT1_Serah.QtyIn = Convert.ToInt32(sqlDataReader["QtysIn"]);
            objT1_Serah.QtyOut = Convert.ToInt32(sqlDataReader["QtysOut"]);
            objT1_Serah.HPP = Convert.ToDecimal(sqlDataReader["HPP"]);
            objT1_Serah.Tebal = Convert.ToDecimal(sqlDataReader["Tebal"]);
            objT1_Serah.Lebar = Convert.ToInt32(sqlDataReader["Lebar"]);
            objT1_Serah.Panjang = Convert.ToInt32(sqlDataReader["Panjang"]);
            return objT1_Serah;
        }
        public T1_Serah GenerateObjectMutasiTransit(SqlDataReader sqlDataReader)
        {
            objT1_Serah = new T1_Serah();
            objT1_Serah.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objT1_Serah.DestID = Convert.ToInt32(sqlDataReader["DestID"]);
            objT1_Serah.ItemIDSer = Convert.ToInt32(sqlDataReader["ItemIDSer"]);
            objT1_Serah.TglProduksi = Convert.ToDateTime(sqlDataReader["TglProduksi"]);
            objT1_Serah.Palet = (sqlDataReader["NoPalet"]).ToString();
            objT1_Serah.Formula = (sqlDataReader["Formula"]).ToString();
            objT1_Serah.PartnoDest = (sqlDataReader["PartnoDest"]).ToString();
            objT1_Serah.TglSerah = Convert.ToDateTime(sqlDataReader["TglSerah"]);
            objT1_Serah.PartnoSer = (sqlDataReader["PartnoSer"]).ToString();
            objT1_Serah.LokasiSer = (sqlDataReader["LokasiSer"]).ToString();
            objT1_Serah.QtyIn = Convert.ToInt32(sqlDataReader["QtysIn"]);
            objT1_Serah.QtyOut = Convert.ToInt32(sqlDataReader["QtysOut"]);
            objT1_Serah.HPP = Convert.ToDecimal(sqlDataReader["HPP"]);
            objT1_Serah.Tebal = Convert.ToDecimal(sqlDataReader["Tebal"]);
            objT1_Serah.Lebar = Convert.ToInt32(sqlDataReader["Lebar"]);
            objT1_Serah.Panjang = Convert.ToInt32(sqlDataReader["Panjang"]);
            return objT1_Serah;
        }
        public T1_Serah GenerateObject(SqlDataReader sqlDataReader)
        {
            objT1_Serah = new T1_Serah();
            objT1_Serah.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objT1_Serah.DestID = Convert.ToInt32(sqlDataReader["DestID"]);
            objT1_Serah.ItemIDSer = Convert.ToInt32(sqlDataReader["ItemIDSer"]);
            objT1_Serah.TglProduksi = Convert.ToDateTime(sqlDataReader["TglProduksi"]);
            //objT1_Serah.PlantGroup = (sqlDataReader["PlantGroup"]).ToString();
            //objT1_Serah.Formula = (sqlDataReader["Formula"]).ToString();
            objT1_Serah.LokasiDest = (sqlDataReader["LokasiDest"]).ToString();
            objT1_Serah.PartnoDest = (sqlDataReader["PartnoDest"]).ToString();
            objT1_Serah.Palet = (sqlDataReader["NoPalet"]).ToString();
           // objT1_Serah.TglJemur = Convert.ToDateTime(sqlDataReader["TglJemur"]);
            //objT1_Serah.Rak = (sqlDataReader["Rak"]).ToString();
            objT1_Serah.TglSerah = Convert.ToDateTime(sqlDataReader["TglSerah"]);
            objT1_Serah.PartnoSer = (sqlDataReader["PartnoSer"]).ToString();
            objT1_Serah.LokasiSer = (sqlDataReader["LokasiSer"]).ToString();
            objT1_Serah.QtyIn = Convert.ToInt32(sqlDataReader["QtysIn"]);
            objT1_Serah.QtyOut = Convert.ToInt32(sqlDataReader["QtysOut"]);
            objT1_Serah.HPP = Convert.ToDecimal(sqlDataReader["HPP"]);
            return objT1_Serah;
        }
        public T1_Serah GenerateObjectPartnoPelarian(SqlDataReader sqlDataReader)
        {
            objT1_Serah = new T1_Serah();
            //objT1_Serah.ID = Convert.ToInt32(sqlDataReader["ID"]);
            //objT1_Serah.DestID = Convert.ToInt32(sqlDataReader["DestID"]);
            //objT1_Serah.ItemIDSer = Convert.ToInt32(sqlDataReader["ItemIDSer"]);
            //objT1_Serah.TglProduksi = Convert.ToDateTime(sqlDataReader["TglProduksi"]);
            //objT1_Serah.PlantGroup = (sqlDataReader["PlantGroup"]).ToString();
            //objT1_Serah.Formula = (sqlDataReader["Formula"]).ToString();
            //.LokasiDest = (sqlDataReader["LokasiDest"]).ToString();
            //objT1_Serah.PartnoDest = (sqlDataReader["PartnoDest"]).ToString();
            //objT1_Serah.Palet = (sqlDataReader["NoPalet"]).ToString();
            // objT1_Serah.TglJemur = Convert.ToDateTime(sqlDataReader["TglJemur"]);
            //objT1_Serah.Rak = (sqlDataReader["Rak"]).ToString();
            //objT1_Serah.TglSerah = Convert.ToDateTime(sqlDataReader["TglSerah"]);
            objT1_Serah.PartnoSer = (sqlDataReader["PartnoSer"]).ToString();
            //objT1_Serah.LokasiSer = (sqlDataReader["LokasiSer"]).ToString();
            //objT1_Serah.QtyIn = Convert.ToInt32(sqlDataReader["QtysIn"]);
            //objT1_Serah.QtyOut = Convert.ToInt32(sqlDataReader["QtysOut"]);
            //objT1_Serah.HPP = Convert.ToDecimal(sqlDataReader["HPP"]);
            return objT1_Serah;
        }
        public ArrayList RetrieveByTglSerahForPelarian(string tglSerah, string lokasi)
        {
            string strSQL = "select  A.ID,case when A.DestID>0 then (select TglProduksi from BM_Destacking where BM_Destacking.ID=A.DestID and Status>-1 and Rowstatus>-1) else '' end TglProduksi, " +
                "A.TglSerah,case when A.JemurID>0 then (select convert(varchar,convert(int,FC_Items.Tebal))+' mm x '+convert(varchar,convert(int,FC_Items.Lebar))+' x ' " +
                "+convert(varchar,convert(int,FC_Items.Panjang) ) from FC_Items,T1_JemurLg where T1_JemurLg.ItemID=FC_Items.ID and T1_JemurLg.ID=A.JemurID " +
                "and T1_JemurLg.Status>-1) else '' end Ukuran,A.QtyIn from T1_Serah as A, FC_Lokasi as B " +
                "where A.LokID=B.ID and " + lokasi + " CONVERT(varchar,A.TglSerah,112)='" + tglSerah + "' and A.Status=0";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_Serah.Add(GenerateObjectLari(sqlDataReader));
                }
            }
            else
                arrT1_Serah.Add(new T1_Serah());

            return arrT1_Serah;
        }
        public T1_Serah RetrieveByTglSerahForPelarianByID(int id)
        {
            string strSQL = "select A.ID,case when A.DestID>0 then (select TglProduksi from BM_Destacking where BM_Destacking.ID=A.DestID and Status>-1 and Rowstatus>-1) else '' end TglProduksi, " +
                "A.TglSerah,case when A.JemurID>0 then (select convert(varchar,FC_Items.Tebal)+' mm x '+convert(varchar,convert(int,FC_Items.Lebar))+' x ' " +
                "+convert(varchar,convert(int,FC_Items.Panjang) ) from FC_Items,T1_JemurLg where T1_JemurLg.ItemID=FC_Items.ID and T1_JemurLg.ID=A.JemurID " +
                "and T1_JemurLg.Status>-1) else '' end Ukuran,A.QtyIn,A.ItemID from T1_Serah as A, FC_Lokasi as B " +
                "where A.LokID=B.ID and B.Lokasi='P99' and A.ID=" + id + " and A.Status=0";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectLari(sqlDataReader);
                }
            }

            return new T1_Serah();
        }
        public ArrayList RetrieveByTglProduksiForPelarian(string tglProduksi, string lokasi)
        {
            string strSQL = string.Empty;
            strSQL = "select  * from (select A.ID,case when A.DestID>0 then (select TglProduksi from BM_Destacking where BM_Destacking.ID=A.DestID and Status>-1 and Rowstatus>-1) else '' end TglProduksi, " +
            "A.TglSerah,case when A.JemurID>0 then (select convert(varchar,convert(int,FC_Items.Tebal))+' mm x '+convert(varchar,convert(int,FC_Items.Lebar))+' x ' " +
            "+convert(varchar,convert(int,FC_Items.Panjang) ) from FC_Items,T1_JemurLg where T1_JemurLg.ItemID=FC_Items.ID and T1_JemurLg.ID=A.JemurID " +
            "and T1_JemurLg.Status>-1) else '' end Ukuran,A.QtyIn " +
            "from T1_Serah as A, FC_Lokasi as B " +
            "where A.LokID=B.ID and " + lokasi + " A.Status=0) as A where CONVERT(varchar,TglProduksi,112)='" + tglProduksi + "' ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_Serah = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_Serah.Add(GenerateObjectLari(sqlDataReader));
                }
            }
            else
                arrT1_Serah.Add(new T1_Serah());

            return arrT1_Serah;
        }

        public T1_Serah GenerateObjectLari(SqlDataReader sqlDataReader)
        {
            objT1_Serah = new T1_Serah();
            objT1_Serah.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objT1_Serah.TglProduksi = Convert.ToDateTime(sqlDataReader["TglProduksi"]);
            objT1_Serah.TglSerah = Convert.ToDateTime(sqlDataReader["TglSerah"]);
            objT1_Serah.Ukuran = sqlDataReader["Ukuran"].ToString();
            objT1_Serah.QtyIn = Convert.ToInt32(sqlDataReader["QtyIn"]);

            return objT1_Serah;
        }
        public override ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }
    }
}
