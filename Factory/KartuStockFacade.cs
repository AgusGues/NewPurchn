using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;
using System.Web;
using System.Web.UI;
using Cogs;

namespace BusinessFacade
{
    public class KartuStockFacade : AbstractFacade
    {
        private KartuStock objKartuStock = new KartuStock();
        private ArrayList arrKartuStock;
        private List<SqlParameter> sqlListParam;

        public KartuStockFacade()
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

        public ArrayList RetrieveByPartNoBM(string partno,string periode,string strawal)
        {
            int thawal = 0;
            int bulan = 0;
            if (strawal == "12")
            {
                thawal = Convert.ToInt16(periode.Substring(0, 4)) - 1;
                bulan = 12;
            }
            else
            {
                thawal = Convert.ToInt16(periode.Substring(0, 4));
            }
            string tglA=periode.Substring(4,2) + "/1/" +periode.Substring(0,4) ;
            arrKartuStock = new ArrayList();
            if (partno.Trim() == string.Empty)
                return arrKartuStock;
            //string strSQL = "create table #adminkswip(idrec varchar(100),tanggal datetime,itemid0 int,qty int,lokid int) " +
            //    "declare @idrec varchar(100) " +
            //    "declare @tanggal datetime " +
            //    "declare @itemid0 int " +
            //    "declare @qty int " +
            //    "declare @lokid int " +
            //    "declare kursor cursor for " +
            //    "select  idrec,tanggal,itemid0,qty,lokid  from vw_KartustockWIP where   LEFT(CONVERT(varchar, tanggal , 112), 6) = '" + periode + "' and  itemid0  " +
            //    "in(select id from fc_items where PartNo = '" + partno + "') " +
            //    "open kursor " +
            //    "FETCH NEXT FROM kursor " +
            //    "INTO @idrec,@tanggal,@itemid0,@qty,@lokid " +
            //    "WHILE @@FETCH_STATUS = 0 " +
            //    "begin " +
            //    "	insert into #adminkswip(idrec,tanggal,itemid0,qty,lokid)values(@idrec,@tanggal,@itemid0,@qty,@lokid) " +
            //    "	FETCH NEXT FROM kursor " +
            //    "	INTO @idrec,@tanggal,@itemid0,@qty,@lokid " +
            //    "END " +
            //    " CLOSE kursor " +
            //    "DEALLOCATE kursor " +
            //    "select ID, tanggal, keterangan ,awal, Penerimaan,pengeluaran, awal+Penerimaan-pengeluaran as saldo,isnull(hpp,0) as HPP from ( " +
            //    "select ID, tanggal,keterangan, case when ID<>' ' then (select isnull(SUM(qty),0) as total from ( " +
            //    "select sum(Saldo) as qty  from t1_saldoperlokasi A inner join FC_Items B on A.ItemID=B.ID   where B.PartNo='" + partno + "' " +
            //    "	and A.thnbln='" + thawal + strawal + "'" +
            //    "	union all " +
            //    "    SELECT SUM(qty) as qty from #adminkswip where itemid0=ain1.itemid and idrec<ain1.ID and LEFT(CONVERT(varchar, tanggal , 112), 6) = '" + periode + "' " +
            //    "     )as ain0) end awal, Penerimaan,pengeluaran,hpp   " +
            //    "	from (  " +
            //    "    select '0'as ID ,ItemID ,'" + tglA + "' as tanggal, '-' as partno,'saldo awal' as keterangan, " +
            //    "	0  as penerimaan,0 as pengeluaran,0 as hpp  from T1_saldoperlokasi A inner join FC_Items B on A.ItemID=B.ID   where B.PartNo='" + partno + "' " +
            //    "	and A.thnbln='" + thawal + strawal + "' group by itemid" +
            //    "	union all " +
            //    "    SELECT CONVERT(varchar, A.TglProduksi, 112) + 'A' + RTRIM(CAST(A.ID AS varchar(10))) + RTRIM(CAST(A.ItemID AS varchar(10))) + RTRIM((SELECT NoPAlet FROM dbo.BM_Palet WHERE (ID = A.PaletID))) + RTRIM(L.Lokasi) AS ID,A.itemID as itemid,A.TglProduksi as tanggal" +
            //    "    ,B.PartNo, case when L.Lokasi like '%adj%' then 'Adjust ' + (select top 1 rtrim(noba) from T1_Adjust where ID in (select top 1 adjustID from T1_AdjustDetail where destid=A.ID)) else 'dr produksi ' + RTRIM(B.PartNo) end AS keterangan, sum(A.Qty) AS Penerimaan, 0 AS Pengeluaran, avg(A.HPP) AS hpp FROM FC_Items AS B INNER JOIN  " +
            //    "    BM_Destacking AS A ON B.ID = A.ItemID left join FC_Lokasi L on A.LokasiID=L.ID WHERE (LEFT(CONVERT(varchar, A.TglProduksi, 112), 6) = '" + periode + "') " +
            //    "    and A.rowstatus>-1 AND (B.PartNo = '" + partno + "') group by A.TglProduksi,A.ItemID,B.PartNo,A.PaletID, A.ID,L.Lokasi " +
            //    "union  all " +
            //    "SELECT  CONVERT(varchar, A.TglSerah, 112) + RTRIM(CAST(B.ItemID AS varchar(10))) + 'B' + RTRIM(CAST(A.ID AS varchar(10))) + RTRIM(L.Lokasi) AS ID,B.itemID as itemid,  " +
            //    "A.TglSerah as tanggal, C.PartNo, case when L.Lokasi like '%adj%' then 'Adjust ' + (select top 1 rtrim(noba) from T1_Adjust where ID in (select top 1 adjustID from T1_AdjustDetail where destid=A.DestID)) else  'trans ke ' + C.PartNo end AS Keterangan,   " +
            //    "0 AS Penerimaan, ISNULL(sum(A.QtyIn), 0) AS Pengeluaran, avg(A.HPp) AS HPP FROM Vw_T1_Serah AS A INNER JOIN  " +
            //    "BM_Destacking AS B ON A.DestID = B.ID INNER JOIN FC_Items AS C ON A.ItemID = C.ID INNER JOIN FC_Items AS C1 ON B.ItemID = C1.ID  " +
            //    " left join FC_Lokasi L on A.LokID=L.ID WHERE  B.qty>0 and  (A.Status > - 1) AND (B.RowStatus > - 1) and   " +
            //    "(LEFT(CONVERT(varchar, A.TglSerah, 112), 6) = '" + periode + "' and C1.PartNo = '" + partno + "')  " +
            //    "GROUP BY  A.TglSerah, B.ItemID, A.ItemID, B.LokasiID, A.DestID, L.Lokasi, A.ID, A.LokID,C.PartNo " +

            //    "union all " +
            //    "SELECT    CONVERT(varchar, A.TglJemur, 112) + RTRIM(CAST(B.ItemID AS varchar(10))) + 'B' + RTRIM(CAST(A.DestID AS varchar(10))) + RTRIM(CAST(A.ID AS varchar(10))) + RTRIM(L.Lokasi) AS ID,B.itemID as itemid,    " +
            //    "A.TglJemur as tanggal, C.PartNo, case when L.Lokasi like '%adj%' then 'Adjust ' + (select top 1 rtrim(noba) from T1_Adjust where ID in   " +
            //    "(select top 1 adjustID from T1_AdjustDetail where destid=A.DestID)) else  'trans ke ' + C.PartNo end AS Keterangan,     " +
            //    "0 AS Penerimaan, ISNULL(sum(A.QtyIn), 0) AS Pengeluaran, avg(A.HPp) AS HPP   " +
            //    "FROM t1_jemurlg AS A INNER JOIN  BM_Destacking AS B ON A.DestID = B.ID   " +
            //    "INNER JOIN FC_Items AS C ON A.ItemID0 = C.ID INNER JOIN FC_Items AS C1 ON B.ItemID = C1.ID    " +
            //    "left join FC_Lokasi L on A.LokID=L.ID WHERE  B.qty>0 and  (A.Status > - 1) AND (B.RowStatus > - 1) and     " +
            //    "(LEFT(CONVERT(varchar, A.TglJemur, 112), 6) = '" + periode + "' and C1.PartNo = '" + partno + "')   " +
            //    "GROUP BY  A.TglJemur, B.ItemID, A.ItemID, B.LokasiID, A.DestID, L.Lokasi, A.ID, A.LokID,C.PartNo  " +
            //    ") as ain1 ) as atALL  order by ID  " +
            //    "drop table #adminkswip";
            string strSQL ="create table #adminkswip(idrec varchar(100),tanggal datetime,itemid0 int,itemid int,qty int,lokasi varchar(100)) "+
                "declare @idrec varchar(100)  "+
                "declare @tanggal datetime  "+
                "declare @itemid0 int  "+
                "declare @itemid int  "+
                "declare @qty int  "+
                "declare @lokasi varchar(100)  "+
                "declare kursor cursor for  "+
                "select  idrec,tanggal,itemid0,itemid,sum(qty) as qty,Lokasi from ( "+
	            "    select CONVERT(varchar, tanggal, 112)+ RTRIM(CAST(itemID0 AS varchar(10)))+ RTRIM(CAST(itemID AS varchar(10)))+ RTRIM(Lokasi) as idrec, "+
                "    tanggal,itemid0,itemid,qty,Lokasi  from vw_KartustockWIP where   LEFT(CONVERT(varchar, tanggal , 112), 6) = '" + periode + "'  " +
                "    and  itemid0  in(select id from fc_items where PartNo = '" + partno + "')  " +
	            "    ) as A group by idrec,tanggal,itemid0,itemid,itemid,Lokasi "+
                "open kursor  "+
                "FETCH NEXT FROM kursor  "+
                "INTO @idrec,@tanggal,@itemid0,@itemid,@qty,@lokasi  "+
                "WHILE @@FETCH_STATUS = 0  "+
                "begin  "+
	            "    insert into #adminkswip(idrec,tanggal,itemid0,itemid,qty,lokasi)values(@idrec,@tanggal,@itemid0,@itemid,@qty,@lokasi )  "+
	            "    FETCH NEXT FROM kursor  "+
	            "    INTO @idrec,@tanggal,@itemid0,@itemid,@qty,@lokasi  "+
                "END  "+
                " CLOSE kursor  "+
                "DEALLOCATE kursor "+ 
                "select ID, tanggal, keterangan ,awal, Penerimaan,pengeluaran, awal+Penerimaan-pengeluaran as saldo,HPP from (  "+
	            "    select ID, tanggal,keterangan, case when ID<>' ' then ( "+
		        "        select isnull(SUM(qty),0) as total from (  "+
			    "            select sum(Saldo) as qty  from t1_saldoperlokasi A inner join FC_Items B on A.ItemID=B.ID    "+
                "            where A.lokID not in (select ID from fc_lokasi where lokasi='p99') and B.PartNo='" + partno + "' and A.thnbln='" + thawal + strawal + "' " +
			    "            union all  "+
                "            SELECT SUM(qty) as qty from #adminkswip where itemid0=ain1.itemid and idrec<ain1.ID and LEFT(CONVERT(varchar, tanggal , 112), 6) = '" + periode + "'  " +
		        "        )as ain0 "+
                "    ) end awal, Penerimaan,pengeluaran,HPP " +
	            "    from (   "+
                "        select '0'as ID ,ItemID ,'" + tglA + "' as tanggal, '-' as partno,'saldo awal' as keterangan,  " +
                "        0  as penerimaan,0 as pengeluaran,0 as HPP  from T1_saldoperlokasi A inner join FC_Items B on A.ItemID=B.ID   where B.PartNo='" + partno + "'  " +
                "        and A.thnbln='" + thawal + strawal + "' group by ItemID  " +
		        "        union all  "+
		        "        SELECT CONVERT(varchar, A.TglProduksi, 112) + RTRIM(CAST(A.ItemID AS varchar(10))) +'0' +  RTRIM(L.Lokasi) AS ID, "+
		        "        A.itemID as itemid,A.TglProduksi as tanggal,B.PartNo,  "+
                "        case when L.Lokasi like '%adj%' then 'Adjust ' else 'dr produksi ' + RTRIM(B.PartNo) end AS keterangan, sum(A.Qty) AS Penerimaan, 0 AS Pengeluaran,A.HPP   " +
		        "        FROM FC_Items AS B INNER JOIN BM_Destacking AS A ON B.ID = A.ItemID left join FC_Lokasi L on A.LokasiID=L.ID  "+
                "        WHERE (LEFT(CONVERT(varchar, A.TglProduksi, 112), 6) = '" + periode + "') and A.rowstatus>-1 AND (B.PartNo = '" + partno + "')  " +
                "        group by A.TglProduksi,A.ItemID,B.PartNo,L.Lokasi,A.HPP " +
		        "        union  all  "+
		        "        SELECT  CONVERT(varchar, A.TglSerah, 112) + RTRIM(CAST(B.ItemID AS varchar(10))) + RTRIM(CAST(A.ItemID AS varchar(10))) + RTRIM(L.Lokasi) AS ID, "+
		        "        B.itemID as itemid,A.TglSerah as tanggal, C.PartNo, case when L.Lokasi like '%adj%' then 'Adjust ' else  'trans ke ' + C.PartNo end AS Keterangan,  "+
                "        0 AS Penerimaan, ISNULL(sum(A.QtyIn), 0) AS Pengeluaran,A.HPP   " +
		        "        FROM T1_Serah AS A INNER JOIN  BM_Destacking AS B ON A.DestID = B.ID INNER JOIN FC_Items AS C ON A.ItemID = C.ID  "+
		        "        INNER JOIN FC_Items AS C1 ON B.ItemID = C1.ID left join FC_Lokasi L on A.LokID=L.ID  "+
                "        WHERE  A.SFrom<>'lari' and B.qty>0 and  (A.Status > - 1) AND (B.RowStatus > - 1) and (LEFT(CONVERT(varchar, A.TglSerah, 112), 6) = '" + periode + "'  " +
                "        and C1.PartNo = '" + partno + "')   " +
                "        GROUP BY  A.TglSerah, B.ItemID, A.ItemID,L.Lokasi,C.PartNo,A.HPP   " +
                "        union all   " +
                "        SELECT  CONVERT(varchar, A.TglJemur, 112) + RTRIM(CAST(B.ItemID AS varchar(10))) + RTRIM(CAST(A.ItemID AS varchar(10))) + RTRIM(L.Lokasi) AS ID, " +
                "        B.itemID as itemid,A.TglJemur as tanggal, C.PartNo, case when L.Lokasi like '%adj%' then 'Adjust ' else  'Pelarian ke ' + C.PartNo end AS Keterangan,    " +
                "        0 AS Penerimaan, ISNULL(sum(A.QtyIn), 0) AS Pengeluaran,A.HPP   " +
                "        FROM t1_jemurlg AS A INNER JOIN  BM_Destacking AS B ON A.DestID = B.ID INNER JOIN FC_Items AS C ON A.ItemID = C.ID  " +
                "        INNER JOIN FC_Items AS C1 ON B.ItemID = C1.ID left join FC_Lokasi L on A.LokID0=L.ID  " +
                "        WHERE  B.qty>0 and  (A.Status > - 1) AND (B.RowStatus > - 1) and (LEFT(CONVERT(varchar, A.TglJemur, 112), 6) = '" + periode + "'  " +
                "        and C1.PartNo = '" + partno + "')   " +
                "        GROUP BY  A.TglJemur, B.ItemID,  A.ItemID,L.Lokasi,C.PartNo,A.HPP  " +
	            "    ) as ain1  "+
                ") as atALL  order by ID   "+
                "drop table #adminkswip" ;

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrKartuStock.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrKartuStock.Add(new KartuStock());

            return arrKartuStock;
        }

        public ArrayList RetrieveByPartNoP(string partno, string periode, string strawal)
        {
            int thawal = 0;
            int bulan = 0;
            if (strawal == "12")
            {
                thawal = Convert.ToInt16(periode.Substring(0, 4)) - 1;
                bulan = 12;
            }
            else
            {
                thawal = Convert.ToInt16(periode.Substring(0, 4));
            }
            string tglA = periode.Substring(4, 2) + "/1/" + periode.Substring(0, 4);
            arrKartuStock = new ArrayList();
            if (partno.Trim() == string.Empty)
                return arrKartuStock;
            string strSQL = "create table #adminkswip(idrec varchar(100),tanggal datetime,itemid0 int,qty int,lokid int) " +
                "declare @idrec varchar(100) " +
                "declare @tanggal datetime " +
                "declare @itemid0 int " +
                "declare @qty int " +
                "declare @lokid int " +
                "declare kursor cursor for " +
                "select idrec,tanggal,itemid0,qty,lokid from vw_KartustockWIP2 where   LEFT(CONVERT(varchar, tanggal , 112), 6) = '" + periode + "' and  itemid0  " +
                "in(select id from fc_items where PartNo = '" + partno + "') " +
                "open kursor " +
                "FETCH NEXT FROM kursor " +
                "INTO @idrec,@tanggal,@itemid0,@qty,@lokid " +
                "WHILE @@FETCH_STATUS = 0 " +
                "begin " +
                "	insert into #adminkswip(idrec,tanggal,itemid0,qty,lokid)values(@idrec,@tanggal,@itemid0,@qty,@lokid) " +
                "	FETCH NEXT FROM kursor " +
                "	INTO @idrec,@tanggal,@itemid0,@qty,@lokid " +
                "END " +
                " CLOSE kursor " +
                "DEALLOCATE kursor " +
                "select ID, tanggal, keterangan ,awal, Penerimaan,pengeluaran, awal+Penerimaan-pengeluaran as saldo,isnull(hpp,0) as HPP from ( " +
                "select ID, tanggal,keterangan, case when ID<>' ' then (select isnull(SUM(qty),0) as total from ( " +
                "select sum(saldo) as qty  from t1_saldoperlokasi A inner join FC_Items B on A.ItemID=B.ID inner join fc_lokasi C on A.lokid=C.ID  where C.lokasi='p99' and B.PartNo='" + partno + "' " +
                "	and A.thnbln='" + thawal + strawal + "'" +
                "	union " +
                "    SELECT SUM(qty) as qty from #adminkswip where itemid0=ain1.itemid and idrec<ain1.ID and LEFT(CONVERT(varchar, tanggal , 112), 6) = '" + periode + "' " +
                "     )as ain0) end awal, Penerimaan,pengeluaran,hpp   " +
                "	from (  " +
                "    select '0'as ID ,ItemID ,'" + tglA + "' as tanggal, '-' as partno,'saldo awal' as keterangan, " +
                "	0  as penerimaan,0 as pengeluaran,0 as hpp  from T1_saldoperlokasi A inner join FC_Items B on A.ItemID=B.ID   where B.PartNo='" + partno + "' " +
                "	and A.thnbln="  + thawal + strawal +  " group by itemid" +
                "	union " +
                "SELECT CONVERT(varchar, TglJemur, 112) + '0'+  RTRIM(CAST(A.ItemID0 AS varchar(10)))  + CONVERT(varchar, C.TglProduksi, 112) +RTRIM(CAST(A.ItemID AS varchar(10))) +rtrim((select nopalet from bm_palet where id =C.paletID)) AS ID, A.itemID0 as itemid, " +
                "A.TglJemur as tanggal,B.PartNo,   'dr produksi ' + RTRIM(D.PartNo) + ' -' +rtrim(convert(varchar,C.TglProduksi,110)) AS keterangan,   " +
                "sum(A.QtyIn) AS Penerimaan, 0 AS Pengeluaran, avg(A.HPP) AS hpp FROM FC_Items AS B INNER JOIN T1_JemurLg AS A ON B.ID = A.ItemID0   " +
                "inner join BM_Destacking C on C.ID=A.DestID inner join FC_Items D on C.ItemID=D.ID WHERE (LEFT(CONVERT(varchar, A.TglJemur, 112), 6) = '" + periode + "')  " +
                " and A.status>-1 AND (B.PartNo = '" + partno + "') and C.paletID not IN(select ID from BM_palet where nopalet like '%adj%')  group by C.paletID,C.TglProduksi,A.TglJemur,A.itemID0,A.ItemID,D.PartNo,B.PartNo          " +
                " union   " +
                " SELECT  CONVERT(varchar, TglSerah, 112)+ '1'  + CONVERT(varchar, B.TglProduksi, 112)+ rtrim(CAST(B.itemID AS varchar(10))) + rtrim(CAST(A.itemID0 AS varchar(10))) + CAST(A.ItemID AS varchar(10)) AS ID,A.itemID0 as itemid,   " +
                " A.TglSerah as tanggal, C.PartNo, 'trasf ke ' + rtrim(C.PartNo) + ' -' +rtrim(convert(varchar,B.TglProduksi,110)) AS Keterangan,    " +
                " 0 AS Penerimaan, ISNULL(sum(A.QtyOut), 0) AS Pengeluaran, avg(A.HPp) AS HPP FROM T1_Serah AS A INNER JOIN  BM_Destacking AS B ON A.DestID = B.ID   " +
                " INNER JOIN FC_Items AS C ON A.ItemID = C.ID INNER JOIN FC_Items AS C1 ON A.ItemID0 = C1.ID  WHERE A.qtyout>0 and A.JemurID  in(select distinct ID from T1_JemurLg ) and B.qty>0 and  (A.Status > - 1) AND (B.Status > - 1) and    " +
                " (LEFT(CONVERT(varchar, A.TglSerah, 112), 6) = '" + periode + "' and C1.PartNo = '" + partno + "')  GROUP BY B.TglProduksi, " +
                " A.TglSerah,B.itemid,A.itemID0,A.itemid, C1.PartNo, C.PartNo "+
                //"union " +
                //    "SELECT CONVERT(varchar, A.AdjustDate , 112) + '3' + rtrim(CAST(B.itemID AS varchar(10)))+ rtrim(B.AdjustType) AS ID,B.itemID as itemid,   " +
                //    "A.AdjustDate as tanggal, C.PartNo, 'adjustin' AS Keterangan,   ISNULL(sum(B.QtyIn ), 0) AS Penerimaan, 0 AS Pengeluaran, avg(B.HPp) AS HPP  " +
                //    "FROM T1_Adjust AS A INNER JOIN T1_AdjustDetail AS B ON A.ID = B.AdjustID INNER JOIN FC_Items AS C ON B.ItemID = C.ID " +
                //    "where C.PartNo = '" + partno + "' and B.RowStatus > - 1 and LEFT(CONVERT(varchar, A.AdjustDate, 112), 6) = '" + periode + "' and B.AdjustType='In'  " +
                //    "group by A.AdjustDate,B.itemid, C.PartNo,B.AdjustType " +
                //    "union " +
                //    "SELECT CONVERT(varchar, A.AdjustDate , 112) + '4' + rtrim(CAST(B.itemID AS varchar(10)))+ rtrim(B.AdjustType) AS ID,B.itemID as itemid,   " +
                //    "A.AdjustDate as tanggal, C.PartNo, 'adjustout' AS Keterangan,   0 AS Penerimaan, ISNULL(sum(B.QtyOut ), 0) AS Pengeluaran, avg(B.HPp) AS HPP " +
                //    "FROM T1_Adjust AS A INNER JOIN T1_AdjustDetail AS B ON A.ID = B.AdjustID INNER JOIN FC_Items AS C ON B.ItemID = C.ID " +
                //    "where C.PartNo = '" + partno + "' and B.RowStatus > - 1 and LEFT(CONVERT(varchar, A.AdjustDate, 112), 6) = '" + periode + "' and B.AdjustType='out'  " +
                //    "group by A.AdjustDate,B.itemid, C.PartNo,B.AdjustType " +
                ") as ain1 ) as atALL  order by ID  " +
                "drop table #adminkswip";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrKartuStock.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrKartuStock.Add(new KartuStock());

            return arrKartuStock;
        }

        public ArrayList RetrieveByPartNoBJ(string partno, string periode, string strawal)
        {
            int thawal = 0;
            string strSQL = string.Empty;
            if (strawal == "DesQty")
                thawal = Convert.ToInt16(periode.Substring(0, 4)) - 1;
            else
                thawal = Convert.ToInt16(periode.Substring(0, 4));
            string tglA = periode.Substring(4, 2) + "/1/" + periode.Substring(0, 4);
            if (periode==string.Empty)
                return arrKartuStock;
            arrKartuStock = new ArrayList();
            if (partno.Trim() == string.Empty)
                return arrKartuStock;
            int depo = ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID;
            string period = new Inifile(HttpContext.Current.Server.MapPath("~/App_Data/Factory.ini")).Read("AwalPeriode" + depo.ToString(), "Report");
            if (Convert.ToInt32(period) >= Convert.ToInt32(periode))
            {
                 strSQL = "create table #adminksbj(idrec varchar(100),tanggal datetime,itemid int,qty int) " +
                    "declare @idrec varchar(100) " +
                    "declare @tanggal datetime " +
                    "declare @itemid int " +
                    "declare @qty int " +
                    "declare kursor cursor for " +
                    "select idrec,tanggal,itemid,qty from vw_Kartustockbj where   LEFT(CONVERT(varchar, tanggal , 112), 6) = '" + periode + "' and  itemid  " +
                    "in(select id from fc_items where PartNo = '" + partno + "') " +
                    "open kursor " +
                    "FETCH NEXT FROM kursor " +
                    "INTO @idrec,@tanggal,@itemid,@qty " +
                    "WHILE @@FETCH_STATUS = 0 " +
                    "begin " +
                    "	insert into #adminksbj(idrec,tanggal,itemid,qty)values(@idrec,@tanggal,@itemid,@qty) " +
                    "	FETCH NEXT FROM kursor " +
                    "	INTO @idrec,@tanggal,@itemid,@qty " +
                    "END " +
                    "CLOSE kursor " +
                    "DEALLOCATE kursor " +
                    "select distinct '0' as ID,'" + periode.Substring(4, 2) + "/1/' + cast(" + thawal + " as varchar) as tanggal,'Saldo Awal' as keterangan,0 as awal,0 as Penerimaan,0 as pengeluaran, " + strawal + " as saldo,0 as hpp,' ' as process  from SaldoInventorybj A inner join FC_Items B on A.ItemID=B.ID   where B.PartNo='" + partno + "' " +
                    "	and A.YearPeriod=" + thawal + " union " +
                    "select ID, tanggal,case  when penerimaan>0 then 'trm dr ' + keterangan else  'trans ke ' + keterangan end keterangan ,awal, " +
                    "Penerimaan,pengeluaran, awal+Penerimaan-pengeluaran as saldo,isnull(HPP,0) as HPP,process from ( " +
                    "select ID, tanggal,partno,keterangan,  " +
                    "case when ID<>' ' then (select isnull(SUM(qty),0) as total from ( " +
                    "select " + strawal + " as qty  from SaldoInventorybj A inner join FC_Items B on A.ItemID=B.ID   where B.PartNo='" + partno + "' " +
                    "	and A.YearPeriod=" + thawal + " " +
                    "	union " +
                    "   SELECT SUM(qty) as qty from #adminksbj where itemid=ain1.itemid and idrec<ain1.ID and LEFT(CONVERT(varchar, tanggal , 112), 6) = '" + periode + "' " +
                    "     )as ain0) end  awal,  " +
                    "Penerimaan,pengeluaran,HPP,process from ( " +
                    "SELECT convert(varchar,A.CreatedTime,112)+ rtrim(CAST( A.ID AS CHAR(10)))+ 'A'+ rtrim(A.ItemID)+rtrim(A.Keterangan) as ID,A.ItemID, B.PartNo,A.Keterangan, A.CreatedTime  as tanggal,  " +
                    "A.Qtyin as Penerimaan,0 as Pengeluaran,A.hpp as hpp,A.Process as process  " +
                    "FROM FC_Items AS B INNER JOIN T3_Rekap  AS A ON B.ID = A.ItemID  where  A.process not like 'ex%'  and A.rowstatus>-1 and B.partno='" + partno + "' and A.Qtyin>0  " +
                    "and LEFT(convert(varchar,A.CreatedTime,112),6) ='" + periode + "' " +
                    " union  " +
                    "SELECT convert(varchar,A.CreatedTime,112)+ rtrim(CAST( A.ID AS CHAR(10)))+ 'B'+rtrim(A.ItemID)+rtrim(A.Keterangan)as ID,A.ItemID,B.PartNo,A.Keterangan, A.CreatedTime  as tanggal,  " +
                    "0 as Penerimaan,A.QtyOut as Pengeluaran,A.hpp as hpp,A.Process as process  " +
                    "FROM FC_Items AS B INNER JOIN T3_Rekap  AS A ON B.ID = A.ItemID  where   A.process not like 'ex%' and A.rowstatus>-1 and B.partno='" + partno + "' and A.QtyOut>0 and  " +
                    "LEFT(convert(varchar,A.CreatedTime,112),6) ='" + periode + "'" +
                    " union " +
                    "SELECT convert(varchar,A.CreatedTime,112)+ rtrim(CAST(A.ID AS CHAR(10)))+'C'+ rtrim(A.ItemID) as ID,A.ItemID,B.PartNo,'pengiriman' as Keterangan, convert(varchar,A.CreatedTime,112) as tanggal, " +
                    "0 as Penerimaan, isnull(A.Qty,0) as Pengeluaran,A.hpp as hpp,case when A.ID>0 then (select top 1 sjno from t3_kirim where id in (select kirimID from t3_kirimdetail where id=A.ID)) end process  " +
                    "FROM T3_KirimDetail  AS A INNER JOIN FC_Items AS B ON A.ItemID = B.ID  where   A.kirimID>0 and A.rowstatus>-1 and B.partno='" + partno + "' and  " +
                    "LEFT(convert(varchar,A.CreatedTime,112),6) ='" + periode + "'" +
                    " union " +
                    "SELECT convert(varchar,A.CreatedTime,112)+ rtrim(CAST(A.ID AS CHAR(10)))+ 'D'+rtrim(A.ItemID) as ID,A.ItemID,B.PartNo,'retur' as Keterangan, " +
                    "convert(varchar,A.CreatedTime,112) as tanggal,isnull(A.Qty,0) as Penerimaan, 0 as Pengeluaran,A.hpp as hpp,A.SJno as process " +
                    "FROM T3_Retur AS A INNER JOIN FC_Items AS B ON A.ItemID = B.ID  where  A.rowstatus>-1 and B.partno='" + partno + "' and " +
                    "LEFT(convert(varchar,A.CreatedTime,112),6) ='" + periode + "' " +
                    " union " +
                    "SELECT  convert(varchar,C.CreatedTime ,112)+ rtrim(CAST(A.ID AS CHAR(10)))+'E'+ rtrim(A.ItemID) as ID,A.ItemID,B.PartNo,'adjust-In' as Keterangan,  " +
                    "convert(varchar,A.CreatedTime,112) as tanggal,isnull(A.QtyIn,0) as Penerimaan, 0 as Pengeluaran,A.hpp as hpp,case when A.ID>0 then (select top 1 AdjustNo  from t3_Adjust where id in (select AdjustID from t3_Adjustdetail where id=A.ID)) end process  " +
                    "FROM T3_AdjustDetail  AS A INNER JOIN FC_Items AS B ON A.ItemID = B.ID  inner join T3_Adjust as C on C.ID=A.AdjustID " +
                    "where  A.rowstatus>-1 and A.apv>0 and A.QtyIn >0 and  B.partno='" + partno + "' and " +
                    "LEFT(convert(varchar,C.CreatedTime,112),6) ='" + periode + "'" +
                    "union " +
                    "SELECT convert(varchar,C.CreatedTime ,112)+ rtrim(CAST(A.ID AS CHAR(10)))+ 'F'+rtrim(A.ItemID) as ID,A.ItemID,B.PartNo,'adjust-Out' as Keterangan,  " +
                    "convert(varchar,A.CreatedTime,112) as tanggal,isnull(A.QtyIn,0) as Penerimaan, isnull(A.QtyOut,0) as Pengeluaran,A.hpp as hpp,case when A.ID>0 then (select top 1 AdjustNo  from t3_Adjust where id in (select AdjustID from t3_Adjustdetail where id=A.ID)) end process " +
                    "FROM T3_AdjustDetail  AS A INNER JOIN FC_Items AS B ON A.ItemID = B.ID  inner join T3_Adjust as C on C.ID=A.AdjustID " +
                    "where  A.rowstatus>-1 and A.apv>0 and A.QtyOut >0  and B.partno='" + partno + "' and " +
                    "LEFT(convert(varchar,A.CreatedTime,112),6) ='" + periode + "'" +
                    ") as ain1 ) as atALL order by ID " +
                    "drop table #adminksbj";
            }
            else
            {
                strSQL = "create table #adminksbj(idrec varchar(100),tanggal datetime,itemid int,qty int) " +
                    "declare @idrec varchar(100) " +
                    "declare @tanggal datetime " +
                    "declare @itemid int " +
                    "declare @qty int " +
                    "declare kursor cursor for " +
                    "select idrec,tanggal,itemid,qty from vw_Kartustockbjnew where   LEFT(CONVERT(varchar, tanggal , 112), 6) = '" + periode + "' and  itemid  " +
                    "in(select id from fc_items where PartNo = '" + partno + "') " +
                    "open kursor " +
                    "FETCH NEXT FROM kursor " +
                    "INTO @idrec,@tanggal,@itemid,@qty " +
                    "WHILE @@FETCH_STATUS = 0 " +
                    "begin " +
                    "	insert into #adminksbj(idrec,tanggal,itemid,qty)values(@idrec,@tanggal,@itemid,@qty) " +
                    "	FETCH NEXT FROM kursor " +
                    "	INTO @idrec,@tanggal,@itemid,@qty " +
                    "END " +
                    "CLOSE kursor " +
                    "DEALLOCATE kursor " +
                    "select distinct '0' as ID,'" + periode.Substring(4, 2) + "/1/' + cast(" + thawal + " as varchar) as tanggal,'Saldo Awal' as keterangan,0 as awal,0 as Penerimaan,0 as pengeluaran, " + strawal + " as saldo,0 as hpp,' ' as process  from SaldoInventorybj A inner join FC_Items B on A.ItemID=B.ID   where B.PartNo='" + partno + "' " +
                    "	and A.YearPeriod=" + thawal + " union " +
                    "select ID, tanggal,case  when penerimaan>0 then 'trm dr ' + keterangan else  'trans ke ' + keterangan end keterangan ,awal, " +
                    "Penerimaan,pengeluaran, awal+Penerimaan-pengeluaran as saldo,isnull(HPP,0) as HPP,process from ( " +
                    "select ID, tanggal,partno,keterangan,  " +
                    "case when ID<>' ' then (select isnull(SUM(qty),0) as total from ( " +
                    "select " + strawal + " as qty  from SaldoInventorybj A inner join FC_Items B on A.ItemID=B.ID   where B.PartNo='" + partno + "' " +
                    "	and A.YearPeriod=" + thawal + " " +
                    "	union " +
                    "   SELECT SUM(qty) as qty from #adminksbj where itemid=ain1.itemid and idrec<ain1.ID and LEFT(CONVERT(varchar, tanggal , 112), 6) = '" + periode + "' " +
                    "     )as ain0) end  awal,  " +
                    "Penerimaan,pengeluaran,HPP,process from ( " +
                    "SELECT convert(varchar,A.CreatedTime,112)+ rtrim(CAST( A.ID AS CHAR(10)))+ 'A'+ rtrim(A.ItemID)+rtrim(A.Keterangan) as ID,A.ItemID, B.PartNo,A.Keterangan, A.tgltrans  as tanggal,  " +
                    "A.Qtyin as Penerimaan,0 as Pengeluaran,A.hpp as hpp,A.Process as process  " +
                    "FROM FC_Items AS B INNER JOIN T3_Rekap  AS A ON B.ID = A.ItemID  where  A.process not like 'ex%'  and A.rowstatus>-1 and B.partno='" + partno + "' and A.Qtyin>0  " +
                    "and LEFT(convert(varchar,A.tgltrans,112),6) ='" + periode + "' " +
                    " union  " +
                    "SELECT convert(varchar,A.CreatedTime,112)+ rtrim(CAST( A.ID AS CHAR(10)))+ 'B'+rtrim(A.ItemID)+rtrim(A.Keterangan)as ID,A.ItemID,B.PartNo,A.Keterangan, A.tgltrans  as tanggal,  " +
                    "0 as Penerimaan,A.QtyOut as Pengeluaran,A.hpp as hpp,A.Process as process  " +
                    "FROM FC_Items AS B INNER JOIN T3_Rekap  AS A ON B.ID = A.ItemID  where   A.process not like 'ex%' and A.rowstatus>-1 and B.partno='" + partno + "' and A.QtyOut>0 and  " +
                    "LEFT(convert(varchar,A.tgltrans,112),6) ='" + periode + "'" +
                    " union " +
                    "SELECT convert(varchar,A.CreatedTime,112)+ rtrim(CAST(A.ID AS CHAR(10)))+'C'+ rtrim(A.ItemID) as ID,A.ItemID,B.PartNo,'pengiriman' as Keterangan, convert(varchar,A.tgltrans,112) as tanggal, " +
                    "0 as Penerimaan, isnull(A.Qty,0) as Pengeluaran,A.hpp as hpp,case when A.ID>0 then (select top 1 sjno from t3_kirim where id in (select kirimID from t3_kirimdetail where id=A.ID)) end process  " +
                    "FROM T3_KirimDetail  AS A INNER JOIN FC_Items AS B ON A.ItemID = B.ID  where   A.kirimID>0 and A.rowstatus>-1 and B.partno='" + partno + "' and  " +
                    "LEFT(convert(varchar,A.tgltrans,112),6) ='" + periode + "'" +
                    " union " +
                    "SELECT convert(varchar,A.CreatedTime,112)+ rtrim(CAST(A.ID AS CHAR(10)))+ 'D'+rtrim(A.ItemID) as ID,A.ItemID,B.PartNo,'retur' as Keterangan, " +
                    "convert(varchar,A.tgltrans,112) as tanggal,isnull(A.Qty,0) as Penerimaan, 0 as Pengeluaran,A.hpp as hpp,A.SJno as process " +
                    "FROM T3_Retur AS A INNER JOIN FC_Items AS B ON A.ItemID = B.ID  where  A.rowstatus>-1 and B.partno='" + partno + "' and " +
                    "LEFT(convert(varchar,A.tgltrans,112),6) ='" + periode + "' " +
                    " union " +
                    "SELECT  convert(varchar,C.CreatedTime ,112)+ rtrim(CAST(A.ID AS CHAR(10)))+'E'+ rtrim(A.ItemID) as ID,A.ItemID,B.PartNo,'adjust-In' as Keterangan,  " +
                    "convert(varchar,C.AdjustDate,112) as tanggal,isnull(A.QtyIn,0) as Penerimaan, 0 as Pengeluaran,A.hpp as hpp,case when A.ID>0 then (select top 1 AdjustNo  from t3_Adjust where id in (select AdjustID from t3_Adjustdetail where id=A.ID)) end process  " +
                    "FROM T3_AdjustDetail  AS A INNER JOIN FC_Items AS B ON A.ItemID = B.ID  inner join T3_Adjust as C on C.ID=A.AdjustID " +
                    "where  A.rowstatus>-1 and A.apv>0 and A.QtyIn >0 and  B.partno='" + partno + "' and " +
                    "LEFT(convert(varchar,C.AdjustDate,112),6) ='" + periode + "'" +
                    "union " +
                    "SELECT convert(varchar,C.CreatedTime ,112)+ rtrim(CAST(A.ID AS CHAR(10)))+ 'F'+rtrim(A.ItemID) as ID,A.ItemID,B.PartNo,'adjust-Out' as Keterangan,  " +
                    "convert(varchar,C.AdjustDate,112) as tanggal,isnull(A.QtyIn,0) as Penerimaan, isnull(A.QtyOut,0) as Pengeluaran,A.hpp as hpp,case when A.ID>0 then (select top 1 AdjustNo  from t3_Adjust where id in (select AdjustID from t3_Adjustdetail where id=A.ID)) end process " +
                    "FROM T3_AdjustDetail  AS A INNER JOIN FC_Items AS B ON A.ItemID = B.ID  inner join T3_Adjust as C on C.ID=A.AdjustID " +
                    "where  A.rowstatus>-1 and A.apv>0 and A.QtyOut >0  and B.partno='" + partno + "' and " +
                    "LEFT(convert(varchar,C.AdjustDate,112),6) ='" + periode + "'" +
                    ") as ain1 ) as atALL order by ID " +
                    "drop table #adminksbj";
            }
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrKartuStock.Add(GenerateObject1(sqlDataReader));
                }
            }
            else
                arrKartuStock.Add(new KartuStock());

            return arrKartuStock;
        }

        public ArrayList RetrieveByGroup(string periode0, string periode, string strawal, string groups, string tebal, string lebar, string panjang)
        {
            string intebal = string.Empty;
            string k = string.Empty;
            for (int i = 0; i <= tebal.Length-1 ; i++)
            {
                k = tebal.Substring(i, 1);
                if (tebal.Substring(i, 1)==",")
                    k = ".";
                else
                    k = tebal.Substring(i, 1);
                intebal = intebal + k;
            }
            tebal = intebal;
            int thawal = 0;
            if (strawal == "DesQty")
                thawal = Convert.ToInt16(periode.Substring(0, 4)) - 1;
            else
                thawal = Convert.ToInt16(periode.Substring(0, 4));
            string tglA = periode.Substring(4, 2) + "/1/" + periode.Substring(0, 4);
            arrKartuStock = new ArrayList();
            if (groups.Trim() == string.Empty)
                return arrKartuStock;
            string strSQL = "create table #adminksbjgroup(id varchar(100),tanggal datetime,groups varchar,qty int,tebal decimal(18,2),lebar int,panjang int,process varchar) " +
                "declare @id varchar(100)  " +
                "declare @tanggal datetime  " +
                "declare @groups varchar  " +
                "declare @qty int  " +
                "declare @tebal decimal(18,2) " +
                "declare @lebar int  " +
                "declare @panjang int  " +
                "declare @process varchar " +
                "declare kursor cursor for  " +
                "select id,convert(datetime,tanggal) as tanggal,groups,qty,tebal,lebar,panjang,process from vw_Kartustockbj_bygroup where left(convert(varchar,tanggal,112),6) = '" + periode + "' and   " +
                "Groups='"+ groups +"' and tebal="+tebal+" and lebar="+lebar+" and panjang="+panjang+" " +
                "open kursor  " +
                "FETCH NEXT FROM kursor  " +
                "INTO @id,@tanggal,@groups,@qty,@tebal,@lebar,@panjang,@process " +
                "WHILE @@FETCH_STATUS = 0  " +
                "begin  " +
                "    insert into #adminksbjgroup(id,tanggal,groups,qty,tebal,lebar,panjang,process)values(@id,@tanggal,@groups,@qty,@tebal,@lebar,@panjang,@process)  " +
                "    FETCH NEXT FROM kursor  " +
                "    INTO @id,@tanggal,@groups,@qty,@tebal,@lebar,@panjang,@process " +
                "END  " +
                "CLOSE kursor  " +
                "DEALLOCATE kursor  " +
                "select ID, tanggal,awal,Penerimaan,pengeluaran, awal+Penerimaan-pengeluaran as saldo,process from (  " +
                "select ID, tanggal,Groups,case when ID<>' ' then (select isnull(SUM(qty),0) as total from (  " +
                "select  sum(" + strawal + ")   as qty  from ( " +
                "select distinct itemid," + strawal + " from SaldoInventorybj A inner join FC_Items B on A.ItemID=B.ID inner join T3_GroupM C on B.GroupID=C.ID " +
                "where  ( SUBSTRING(B.partno,5,1) ='3' or SUBSTRING(B.partno,5,1) ='W')  and C.Groups='" + groups + "' and B.tebal=" + tebal + " and B.lebar=" + lebar + " and B.panjang=" + panjang + " and A.YearPeriod=" + thawal + "  " +
                "and B.ID in (select ItemID from vw_KartuStockBJ where LEFT(convert(varchar,tanggal ,112),6)='" + periode + "' and ItemID in( "+
                "select id from FC_Items where Tebal=" + tebal + "  and Lebar=" + lebar + " and Panjang=" + panjang + " and GroupID in (select ID from T3_GroupM where Groups='" + groups + "')))) as A " +
                "union  " +
                "SELECT SUM(qty) as qty from #adminksbjgroup where id<ain1.ID and LEFT(CONVERT(varchar, tanggal , 112), 6) = '" + periode + "'  " +
                ")as ain0) end  awal, Penerimaan,pengeluaran ,process from (  " +
                "select ID, Groups, tanggal, sum(Penerimaan) as Penerimaan,sum(pengeluaran)as Pengeluaran ,process from( " +
                "SELECT convert(varchar,A.CreatedTime,112)+ rtrim(CAST( C.Groups AS CHAR(10)))+ 'A'+ rtrim(CAST( B.Tebal AS CHAR(10)))+ " +
                "rtrim(CAST( B.Lebar AS CHAR(10)))+rtrim(CAST( B.Panjang AS CHAR(10)))+RTRIM(A.Process ) as ID,C.Groups, convert(date,A.CreatedTime,110) as tanggal,  " +
                "A.Qtyin as Penerimaan,0 as Pengeluaran,A.Process as process   " +
                "FROM FC_Items AS B INNER JOIN T3_Rekap  AS A ON B.ID = A.ItemID inner join T3_GroupM C on B.GroupID=C.ID  " +
                "where  A.process not like 'ex%'  and A.rowstatus>-1 and A.Qtyin>0  and LEFT(convert(varchar,A.CreatedTime,112),6) ='" + periode + "' " +
                "and ( SUBSTRING(B.partno,5,1) ='3' or SUBSTRING(B.partno,5,1) ='W')  and C.Groups='" + groups + "' and B.tebal=" + tebal + " and B.lebar=" + lebar + " and B.panjang=" + panjang + ") as A " +
                "group by  ID,Groups,tanggal,process " +
                "union " +
                "select ID, Groups, tanggal, sum(Penerimaan) as Penerimaan,sum(pengeluaran)as Pengeluaran ,process from( " +
                "SELECT convert(varchar,A.CreatedTime,112)+ rtrim(CAST( C.Groups AS CHAR(10)))+ 'B'+ rtrim(CAST( B.Tebal AS CHAR(10)))+ " +
                "rtrim(CAST( B.Lebar AS CHAR(10)))+rtrim(CAST( B.Panjang AS CHAR(10)))+RTRIM(A.Process ) as ID,C.Groups, convert(date,A.CreatedTime,110) as tanggal,  " +
                "0 as Penerimaan,A.QtyOut as Pengeluaran,A.Process as process   " +
                "FROM FC_Items AS B INNER JOIN T3_Rekap  AS A ON B.ID = A.ItemID inner join T3_GroupM C on B.GroupID=C.ID  " +
                "where  A.process not like 'ex%'  and A.rowstatus>-1 and A.Qtyout>0  and LEFT(convert(varchar,A.CreatedTime,112),6) ='" + periode + "'  " +
                "and  ( SUBSTRING(B.partno,5,1) ='3' or SUBSTRING(B.partno,5,1) ='W')  and C.Groups='" + groups + "' and B.tebal=" + tebal + " and B.lebar=" + lebar + " and B.panjang=" + panjang + ") as B " +
                "group by  ID,Groups,tanggal,process " +
                "union  " +
                "select ID, Groups, tanggal, sum(Penerimaan) as Penerimaan,sum(pengeluaran)as Pengeluaran ,process from( " +
                "SELECT convert(varchar,A.CreatedTime,112)+ rtrim(CAST( C.Groups AS CHAR(10)))+ 'C'+ rtrim(CAST( B.Tebal AS CHAR(10)))+ " +
                "rtrim(CAST( B.Lebar AS CHAR(10)))+rtrim(CAST( B.Panjang AS CHAR(10))) as  ID,C.Groups, convert(date,A.CreatedTime,110) as tanggal,  " +
                "0 as Penerimaan, A.Qty as Pengeluaran,'pengiriman' as process   " +
                "FROM T3_KirimDetail  AS A INNER JOIN FC_Items AS B ON A.ItemID = B.ID  inner join T3_GroupM C on B.GroupID=C.ID  " +
                "where   A.kirimID>0 and A.rowstatus>-1 and  LEFT(convert(varchar,A.CreatedTime,112),6) ='" + periode + "' " +
                "and  ( SUBSTRING(B.partno,5,1) ='3' or SUBSTRING(B.partno,5,1) ='W')  and C.Groups='" + groups + "' and B.tebal=" + tebal + " and B.lebar=" + lebar + " and B.panjang=" + panjang + ") as C " +
                "group by  ID,Groups,tanggal,process " +
                " union  " +
                "select ID, Groups, tanggal, sum(Penerimaan) as Penerimaan,sum(pengeluaran)as Pengeluaran ,process from( " +
                "SELECT convert(varchar,A.CreatedTime,112)+ rtrim(CAST( C.Groups AS CHAR(10)))+ 'D'+ rtrim(CAST( B.Tebal AS CHAR(10)))+ " +
                "rtrim(CAST( B.Lebar AS CHAR(10)))+rtrim(CAST( B.Panjang AS CHAR(10))) as  ID,C.Groups, convert(date,A.CreatedTime,110) as tanggal, " +
                "A.Qty as Penerimaan, 0 as Pengeluaran,'Retur' as process  " +
                "FROM T3_Retur AS A INNER JOIN FC_Items AS B ON A.ItemID = B.ID  inner join T3_GroupM C on B.GroupID=C.ID  " +
                "where  A.rowstatus>-1 and LEFT(convert(varchar,A.CreatedTime,112),6) ='" + periode + "' " +
                "and  ( SUBSTRING(B.partno,5,1) ='3' or SUBSTRING(B.partno,5,1) ='W')  and C.Groups='" + groups + "' and B.tebal=" + tebal + " and B.lebar=" + lebar + " and B.panjang=" + panjang + ") as C " +
                "group by  ID,Groups,tanggal,process " +
                " union  " +
                "select ID, Groups, tanggal, sum(Penerimaan) as Penerimaan,sum(pengeluaran)as Pengeluaran ,process from( " +
                "SELECT  convert(varchar,A.CreatedTime ,112)+ rtrim(CAST( C.Groups AS CHAR(10)))+ 'E'+ rtrim(CAST( B.Tebal AS CHAR(10)))+ " +
                "rtrim(CAST( B.Lebar AS CHAR(10)))+rtrim(CAST( B.Panjang AS CHAR(10))) as  ID,C.Groups,   " +
                "convert(date ,A.CreatedTime,110) as tanggal,A.QtyIn as Penerimaan, 0 as Pengeluaran,'adjust-In' as process    " +
                "FROM T3_AdjustDetail  AS A INNER JOIN FC_Items AS B ON A.ItemID = B.ID  inner join T3_Adjust as D on D.ID=A.AdjustID  " +
                "inner join T3_GroupM C on B.GroupID=C.ID  " +
                "where  A.rowstatus>-1 and A.apv>0 and A.QtyIn >0 and LEFT(convert(varchar,D.CreatedTime,112),6) ='" + periode + "' " +
                "and  ( SUBSTRING(B.partno,5,1) ='3' or SUBSTRING(B.partno,5,1) ='W')  and C.Groups='" + groups + "' and B.tebal=" + tebal + " and B.lebar=" + lebar + " and B.panjang=" + panjang + ") as C " +
                "group by  ID,Groups,tanggal,process " +
                " union  " +
                "select ID, Groups, tanggal, sum(Penerimaan) as Penerimaan,sum(pengeluaran)as Pengeluaran ,process from( " +
                "SELECT  convert(varchar,A.CreatedTime ,112)+ rtrim(CAST( C.Groups AS CHAR(10)))+ 'F'+ rtrim(CAST( B.Tebal AS CHAR(10)))+ " +
                "rtrim(CAST( B.Lebar AS CHAR(10)))+rtrim(CAST( B.Panjang AS CHAR(10))) as  ID,C.Groups,   " +
                "convert(date ,A.CreatedTime,110) as tanggal,0 as Penerimaan, A.QtyOut as Pengeluaran,'adjust-Out' as process    " +
                "FROM T3_AdjustDetail  AS A INNER JOIN FC_Items AS B ON A.ItemID = B.ID  inner join T3_Adjust as D on D.ID=A.AdjustID  " +
                "inner join T3_GroupM C on B.GroupID=C.ID  " +
                "where  A.rowstatus>-1 and A.apv>0 and A.QtyOut >0 and LEFT(convert(varchar,D.CreatedTime,112),6) ='" + periode + "' " +
                "and  ( SUBSTRING(B.partno,5,1) ='3' or SUBSTRING(B.partno,5,1) ='W')  and C.Groups='" + groups + "' and B.tebal=" + tebal + " and B.lebar=" + lebar + " and B.panjang=" + panjang + ") as C " +
                "group by  ID,Groups,tanggal,process " +
                ") as ain1 ) as atALL order by ID  " +
                "drop table #adminksbjgroup";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrKartuStock.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrKartuStock.Add(new KartuStock());

            return arrKartuStock;
        }

        public string  UpdateHPPbyDestacking(string thnbln)
        {
            string strSQL = "declare @itemid int "+
                "declare @HPP decimal(18,6) "+
                "declare @bln int "+
                "declare @thn int "+
                "declare kursor cursor for "+
                "select itemid,HPPLbr as HPP from ( "+
                "select  itemid,case when ItemID>0 then (select partno from FC_Items where ID=E.ItemID ) end partno,volume/32 as tebal,volume,qty,volqty,totalvolqty,persenvolqty,biaya,biaya*persenvolqty/100 as HPPTotal, (biaya*persenvolqty/100)/qty as HPPLbr from ( "+
                "select itemid,volume,qty,volqty,totalvolqty,(volqty/totalvolqty)*100 as persenvolqty, "+
                "case when ItemID>0 then (select SUM(biaya) from T3_Biaya "+
                "where CAST(tahun AS VARCHAR(4))+RIGHT(REPLICATE('0',2)+CAST(bulan AS VARCHAR(2)),2) ='"+ thnbln +"') end biaya from ( "+
                "select itemid,volume,qty,volqty,case when ItemID >0 then (select SUM(volqty) from ( "+
                "select itemid,volume,qty,qty*volume as volqty from ( "+
                "select itemid,case when ItemID >0 then (select tebal * 32 from FC_Items where ID=A0.ItemID  ) end volume, qty from( "+
                "select ItemID ,SUM(qty) as qty from BM_Destacking where left(convert(char,TglProduksi,112),6)='"+ thnbln +"'  "+
                "group by itemid) as A0) as B0) as C0) end totalvolqty from( "+
                "select itemid,volume,qty,qty*volume as volqty from ( "+
                "select itemid,case when ItemID >0 then (select tebal * 32 from FC_Items where ID=A.ItemID  ) end volume, qty from( "+
                "select ItemID ,SUM(qty) as qty from BM_Destacking where left(convert(char,TglProduksi,112),6)='"+ thnbln +"' group by itemid)  "+
                "as A) as B) as C)as D) as E ) as F "+
                "open kursor "+
                "FETCH NEXT FROM kursor "+
                "INTO @itemid,@HPP "+
                "WHILE @@FETCH_STATUS = 0 "+
                "begin "+
	            "    update BM_Destacking set HPP=@HPP where ItemID = @itemid and left(convert(char,TglProduksi,112),6)='"+ thnbln +"' "+
	            "    select @bln=RIGHT('"+ thnbln +"',2),@thn=LEFT('"+ thnbln +"',4) "+
	            "    exec spupdatesaldoinventoryt1price "+ 
		        "        @ItemID=@itemid, "+
		        "        @AvgPrice =@HPP, "+
		        "        @MonthPeriod =@bln, "+
		        "        @YearPeriod =@thn  "+
	            "    update T1_Serah set HPP=@HPP where ID in (select  id from ( "+
	            "    SELECT A.id, B.ItemID AS itemid, A.TglSerah AS tanggal, A.QtyIn AS Pengeluaran "+
	            "    FROM  T1_Serah AS A INNER JOIN BM_Destacking AS B ON A.DestID = B.ID "+
	            "    WHERE (LEFT(CONVERT(varchar, A.TglSerah, 112), 6) = '"+ thnbln +"')) as a where itemid=@itemid) "+
	            "    FETCH NEXT FROM kursor	INTO @itemid,@HPP "+
                "END "+
                "CLOSE kursor "+
                "DEALLOCATE kursor";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }

        public string UpdateHPPbyTransitOut(string thnbln)
        {
            string strSQL = "declare @thn int "+
                "declare @bln int "+
                "declare @Cthn int "+
                "declare @Cbln int "+
                "declare @itemid int "+
                "declare @EndHPP decimal(18,6) "+
                "select @thn=CAST(left('"+ thnbln +"',4) as int ) "+
                "select @bln=CAST(right('"+ thnbln +"',2)as int) -1 "+
                "if @bln=0 "+
	            "    begin  "+
		        "        select @thn=@thn-1   "+
		        "        select @bln=12 "+
	            "    end  "+
                "select @Cthn=CAST(left('"+ thnbln +"',4) as int ) "+
                "select @Cbln=CAST(right('"+ thnbln +"',2)as int) "+
                "declare kursor cursor for "+
                "select itemid,((totalQty*HPP)+(lastTotalQty*LastHPP))/(totalQty+lastTotalQty) as EndHPP from ( "+
                "select itemid,SUM(qtyin )as totalQty, avg(HPP) as HPP, "+
                "case when ItemID >0 then ( "+
	            "    case @bln  when 1 then (select janqty from SaldoInventoryBJ where yearperiod=@thn and ItemID=A.ItemID) "+
		        "        when 2 then (select febqty from SaldoInventoryBJ where yearperiod=@thn and ItemID=A.ItemID)  "+
		        "        when 3 then (select Marqty from SaldoInventoryBJ where yearperiod=@thn and ItemID=A.ItemID)  "+
		        "        when 4 then (select Aprqty from SaldoInventoryBJ where yearperiod=@thn and ItemID=A.ItemID)  "+
		        "        when 5 then (select Meiqty from SaldoInventoryBJ where yearperiod=@thn and ItemID=A.ItemID)  "+
		        "        when 6 then (select Junqty from SaldoInventoryBJ where yearperiod=@thn and ItemID=A.ItemID)  "+
		        "        when 7 then (select Julqty from SaldoInventoryBJ where yearperiod=@thn and ItemID=A.ItemID)  "+
		        "        when 8 then (select Aguqty from SaldoInventoryBJ where yearperiod=@thn and ItemID=A.ItemID)  "+
		        "        when 9 then (select Sepqty from SaldoInventoryBJ where yearperiod=@thn and ItemID=A.ItemID)  "+
		        "        when 10 then (select Oktqty from SaldoInventoryBJ where yearperiod=@thn and ItemID=A.ItemID)  "+
		        "        when 11 then (select Novqty from SaldoInventoryBJ where yearperiod=@thn and ItemID=A.ItemID)  "+
		        "        when 12 then (select Desqty from SaldoInventoryBJ where yearperiod=@thn and ItemID=A.ItemID)  "+
		        "        end   "+
                ") end lastTotalQty, "+
                "case when ItemID >0 then ( "+
	            "    case @bln  when 1 then (select janavgprice from SaldoInventoryBJ where yearperiod=@thn and ItemID=A.ItemID) "+
		        "        when 2 then (select febavgprice from SaldoInventoryBJ where yearperiod=@thn and ItemID=A.ItemID)  "+
		        "        when 3 then (select MarAvgPrice from SaldoInventoryBJ where yearperiod=@thn and ItemID=A.ItemID)  "+
		        "        when 4 then (select AprAvgPrice from SaldoInventoryBJ where yearperiod=@thn and ItemID=A.ItemID)  "+
		        "        when 5 then (select MeiAvgPrice from SaldoInventoryBJ where yearperiod=@thn and ItemID=A.ItemID)  "+
		        "        when 6 then (select JunAvgPrice from SaldoInventoryBJ where yearperiod=@thn and ItemID=A.ItemID)  "+
		        "        when 7 then (select JulAvgPrice from SaldoInventoryBJ where yearperiod=@thn and ItemID=A.ItemID)  "+
		        "        when 8 then (select AguAvgPrice from SaldoInventoryBJ where yearperiod=@thn and ItemID=A.ItemID)  "+
		        "        when 9 then (select SepAvgPrice from SaldoInventoryBJ where yearperiod=@thn and ItemID=A.ItemID)  "+
		        "        when 10 then (select OktAvgPrice from SaldoInventoryBJ where yearperiod=@thn and ItemID=A.ItemID)  "+
		        "        when 11 then (select NovAvgPrice from SaldoInventoryBJ where yearperiod=@thn and ItemID=A.ItemID)  "+
		        "        when 12 then (select DesAvgPrice from SaldoInventoryBJ where yearperiod=@thn and ItemID=A.ItemID)  "+
		        "        end   "+
                ") end lastHPP from T1_Serah A where LEFT(convert(char,tglserah,112),6)='"+ thnbln +"' group by ItemID )B "+
                "open kursor "+
                "FETCH NEXT FROM kursor INTO @itemid,@EndHPP "+
                "WHILE @@FETCH_STATUS = 0 "+
                "begin "+
	            "    update T3_Rekap set hpp=@EndHPP where Process='direct' and ItemID=@itemid and LEFT(convert(char,tgltrans,112),6)='"+ thnbln +"' "+
	            "    update T3_Rekap set hpp=@EndHPP where Process<>'direct' and QtyOut >0 and ItemID=@itemid and LEFT(convert(char,tgltrans,112),6)='"+ thnbln +"' "+
                "    update T3_Retur  set hpp=@EndHPP where ItemID=@itemid and LEFT(convert(char,tgltrans,112),6)='"+ thnbln +"' "+
                "    update T3_KirimDetail set hpp=@EndHPP where ItemID=@itemid and LEFT(convert(char,tgltrans,112),6)='" + thnbln + "' " +
	            "    exec spupdatesaldoinventoryt3price  "+
		        "        @ItemID=@itemid, "+
		        "        @AvgPrice =@EndHPP, "+
		        "        @MonthPeriod =@Cbln, "+
		        "        @YearPeriod =@Cthn  "+
	            "    FETCH NEXT FROM kursor	INTO @itemid,@EndHPP "+
                "END "+
                "CLOSE kursor "+
                "DEALLOCATE kursor" ;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }

        public string UpdateHPPbySimetris(string thnbln)
        {
            string strSQL ="declare @thn int "+
                "declare @bln int "+
                "declare @itemid int "+
                "declare @EndHPP decimal(18,6) "+
                "select @thn=CAST(left('"+ thnbln +"',4) as int ) "+
                "select @bln=CAST(right('"+ thnbln +"',2)as int) "+
                "declare kursor cursor for "+
                "select itemid,EndHPP from ( "+
                "select itemid,partno,luas,fromitemid,keterangan as frompartno,fromluas,fromHPP,cast(fromluas/luas as int ) as endLuas, "+
                "fromHPP/(cast(fromluas/luas as int ))as endHPP from ( "+
                "select distinct ItemID,case when ItemID >0 then(select panjang*lebar from fc_items where id=A.itemid )end luas , "+
                "case when ItemID >0 then(select PartNo  from fc_items where id=A.itemid )end partno , "+
                "Keterangan,case when Keterangan<>' ' then (select ID from FC_Items where PartNo=A.Keterangan ) end fromitemid, "+
                "case when Keterangan<>' ' then(select panjang*lebar from fc_items where PartNo=A.Keterangan )end fromluas, "+
                "case when Keterangan<>' ' then(select top 1 HPP from T3_Rekap InA  "+
                "where InA.ItemID=(select ID from FC_Items where PartNo=A.Keterangan) order by ID desc)end fromHPP   "+
                "from T3_Rekap A  where Process <>'direct' and QtyIn >0 and LEFT(convert(char,tgltrans,112),6)='"+ thnbln +"') as B)as C "+
                "open kursor "+
                "FETCH NEXT FROM kursor INTO @itemid,@EndHPP "+
                "WHILE @@FETCH_STATUS = 0 "+
                "begin "+
	            "    update T3_Rekap set hpp=@EndHPP where Process<>'direct' and ItemID=@itemid and LEFT(convert(char,tgltrans,112),6)='"+ thnbln +"' "+
	            "    exec spupdatesaldoinventoryt3price  "+
		        "        @ItemID=@itemid, "+
		        "        @AvgPrice =@EndHPP, "+
		        "        @MonthPeriod =@bln, "+
		        "        @YearPeriod =@thn  "+
	            "    FETCH NEXT FROM kursor	INTO @itemid,@EndHPP "+
                "END "+
                "CLOSE kursor "+
                "DEALLOCATE kursor";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return strError;
        }

        public ArrayList RetrieveByPartNoBJ2(string partno, string periode, string strawal)
        {
            int thawal = 0;
            string strSQL = string.Empty;
            if (strawal == "DesQty")
                thawal = Convert.ToInt16(periode.Substring(0, 4)) - 1;
            else
                thawal = Convert.ToInt16(periode.Substring(0, 4));
            string tglA = periode.Substring(4, 2) + "/1/" + periode.Substring(0, 4);
            if (periode == string.Empty)
                return arrKartuStock;
            arrKartuStock = new ArrayList();
            if (partno.Trim() == string.Empty)
                return arrKartuStock;
            int depo = ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID;
            string period = new Inifile(HttpContext.Current.Server.MapPath("~/App_Data/Factory.ini")).Read("AwalPeriode" + depo.ToString(), "Report");
            if (Convert.ToInt32(period) >= Convert.ToInt32(periode))
            {
                strSQL = "create table #adminksbj(idrec varchar(100),tanggal datetime,itemid int,qty int) " +
                   "declare @idrec varchar(100) " +
                   "declare @tanggal datetime " +
                   "declare @itemid int " +
                   "declare @qty int " +
                   "declare kursor cursor for " +
                   "select idrec,tanggal,itemid,qty from vw_Kartustockbj where   LEFT(CONVERT(varchar, tanggal , 112), 6) = '" + periode + "' and  itemid  " +
                   "in(select id from fc_items where PartNo = '" + partno + "') " +
                   "open kursor " +
                   "FETCH NEXT FROM kursor " +
                   "INTO @idrec,@tanggal,@itemid,@qty " +
                   "WHILE @@FETCH_STATUS = 0 " +
                   "begin " +
                   "	insert into #adminksbj(idrec,tanggal,itemid,qty)values(@idrec,@tanggal,@itemid,@qty) " +
                   "	FETCH NEXT FROM kursor " +
                   "	INTO @idrec,@tanggal,@itemid,@qty " +
                   "END " +
                   "CLOSE kursor " +
                   "DEALLOCATE kursor " +
                   "select distinct '0' as ID,'" + periode.Substring(4, 2) + "/1/' + cast(" + thawal + " as varchar) as tanggal,'Saldo Awal' as keterangan,0 as awal,0 as Penerimaan,0 as pengeluaran, " + strawal + " as saldo,0 as hpp,' ' as process  from SaldoInventorybj A inner join FC_Items B on A.ItemID=B.ID   where B.PartNo='" + partno + "' " +
                   "	and A.YearPeriod=" + thawal + " union " +
                   "select ID, tanggal,case  when penerimaan>0 then 'trm dr ' + keterangan else  'trans ke ' + keterangan end keterangan ,awal, " +
                   "Penerimaan,pengeluaran, awal+Penerimaan-pengeluaran as saldo,isnull(HPP,0) as HPP,process from ( " +
                   "select ID, tanggal,partno,keterangan,  " +
                   "case when ID<>' ' then (select isnull(SUM(qty),0) as total from ( " +
                   "select " + strawal + " as qty  from SaldoInventorybj A inner join FC_Items B on A.ItemID=B.ID   where B.PartNo='" + partno + "' " +
                   "	and A.YearPeriod=" + thawal + " " +
                   "	union " +
                   "   SELECT SUM(qty) as qty from #adminksbj where itemid=ain1.itemid and idrec<ain1.ID and LEFT(CONVERT(varchar, tanggal , 112), 6) = '" + periode + "' " +
                   "     )as ain0) end  awal,  " +
                   "Penerimaan,pengeluaran,HPP,process from ( " +
                   "SELECT convert(varchar,A.CreatedTime,112)+ rtrim(CAST( A.ID AS CHAR(10)))+ 'A'+ rtrim(A.ItemID)+rtrim(A.Keterangan) as ID,A.ItemID, B.PartNo,A.Keterangan, A.CreatedTime  as tanggal,  " +
                   "A.Qtyin as Penerimaan,0 as Pengeluaran,A.hpp as hpp,A.Process as process  " +
                   "FROM FC_Items AS B INNER JOIN T3_Rekap  AS A ON B.ID = A.ItemID  where  A.process not like 'ex%'  and A.rowstatus>-1 and B.partno='" + partno + "' and A.Qtyin>0  " +
                   "and LEFT(convert(varchar,A.CreatedTime,112),6) ='" + periode + "' " +
                   " union  " +
                   "SELECT convert(varchar,A.CreatedTime,112)+ rtrim(CAST( A.ID AS CHAR(10)))+ 'B'+rtrim(A.ItemID)+rtrim(A.Keterangan)as ID,A.ItemID,B.PartNo,A.Keterangan, A.CreatedTime  as tanggal,  " +
                   "0 as Penerimaan,A.QtyOut as Pengeluaran,A.hpp as hpp,A.Process as process  " +
                   "FROM FC_Items AS B INNER JOIN T3_Rekap  AS A ON B.ID = A.ItemID  where   A.process not like 'ex%' and A.rowstatus>-1 and B.partno='" + partno + "' and A.QtyOut>0 and  " +
                   "LEFT(convert(varchar,A.CreatedTime,112),6) ='" + periode + "'" +
                   " union " +
                   "SELECT convert(varchar,A.CreatedTime,112)+ rtrim(CAST(A.ID AS CHAR(10)))+'C'+ rtrim(A.ItemID) as ID,A.ItemID,B.PartNo,'pengiriman' as Keterangan, convert(varchar,A.CreatedTime,112) as tanggal, " +
                   "0 as Penerimaan, isnull(A.Qty,0) as Pengeluaran,A.hpp as hpp,case when A.ID>0 then (select top 1 sjno from t3_kirim where id in (select kirimID from t3_kirimdetail where id=A.ID)) end process  " +
                   "FROM T3_KirimDetail  AS A INNER JOIN FC_Items AS B ON A.ItemID = B.ID  where   A.kirimID>0 and A.rowstatus>-1 and B.partno='" + partno + "' and  " +
                   "LEFT(convert(varchar,A.CreatedTime,112),6) ='" + periode + "'" +
                   " union " +
                   "SELECT convert(varchar,A.CreatedTime,112)+ rtrim(CAST(A.ID AS CHAR(10)))+ 'D'+rtrim(A.ItemID) as ID,A.ItemID,B.PartNo,'retur' as Keterangan, " +
                   "convert(varchar,A.CreatedTime,112) as tanggal,isnull(A.Qty,0) as Penerimaan, 0 as Pengeluaran,A.hpp as hpp,A.SJno as process " +
                   "FROM T3_Retur AS A INNER JOIN FC_Items AS B ON A.ItemID = B.ID  where  A.rowstatus>-1 and B.partno='" + partno + "' and " +
                   "LEFT(convert(varchar,A.CreatedTime,112),6) ='" + periode + "' " +
                   " union " +
                   "SELECT  convert(varchar,C.CreatedTime ,112)+ rtrim(CAST(A.ID AS CHAR(10)))+'E'+ rtrim(A.ItemID) as ID,A.ItemID,B.PartNo,'adjust-In' as Keterangan,  " +
                   "convert(varchar,A.CreatedTime,112) as tanggal,isnull(A.QtyIn,0) as Penerimaan, 0 as Pengeluaran,A.hpp as hpp,case when A.ID>0 then (select top 1 AdjustNo  from t3_Adjust where id in (select AdjustID from t3_Adjustdetail where id=A.ID)) end process  " +
                   "FROM T3_AdjustDetail  AS A INNER JOIN FC_Items AS B ON A.ItemID = B.ID  inner join T3_Adjust as C on C.ID=A.AdjustID " +
                   "where  A.rowstatus>-1 and A.apv>0 and A.QtyIn >0 and  B.partno='" + partno + "' and " +
                   "LEFT(convert(varchar,C.CreatedTime,112),6) ='" + periode + "'" +
                   "union " +
                   "SELECT convert(varchar,C.CreatedTime ,112)+ rtrim(CAST(A.ID AS CHAR(10)))+ 'F'+rtrim(A.ItemID) as ID,A.ItemID,B.PartNo,'adjust-Out' as Keterangan,  " +
                   "convert(varchar,A.CreatedTime,112) as tanggal,isnull(A.QtyIn,0) as Penerimaan, isnull(A.QtyOut,0) as Pengeluaran,A.hpp as hpp,case when A.ID>0 then (select top 1 AdjustNo  from t3_Adjust where id in (select AdjustID from t3_Adjustdetail where id=A.ID)) end process " +
                   "FROM T3_AdjustDetail  AS A INNER JOIN FC_Items AS B ON A.ItemID = B.ID  inner join T3_Adjust as C on C.ID=A.AdjustID " +
                   "where  A.rowstatus>-1 and A.apv>0 and A.QtyOut >0  and B.partno='" + partno + "' and " +
                   "LEFT(convert(varchar,A.CreatedTime,112),6) ='" + periode + "'" +
                   ") as ain1 ) as atALL order by ID " +
                   "drop table #adminksbj";
            }
            else
            {
                strSQL =
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempData_Rekap]') AND type in (N'U')) DROP TABLE [dbo].[tempData_Rekap] " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempData_Rekap1]') AND type in (N'U')) DROP TABLE [dbo].[tempData_Rekap1] " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempData_Rekap2]') AND type in (N'U')) DROP TABLE [dbo].[tempData_Rekap2]; " +

                    "create table #adminksbj(idrec varchar(100),tanggal datetime,itemid int,qty int) " +
                    "declare @idrec varchar(100) " +
                    "declare @tanggal datetime " +
                    "declare @itemid int " +
                    "declare @qty int " +
                    "declare kursor cursor for " +
                    "select idrec,tanggal,itemid,qty from vw_Kartustockbjnew where   LEFT(CONVERT(varchar, tanggal , 112), 6) = '" + periode + "' and  itemid  " +
                    "in(select id from fc_items where PartNo = '" + partno + "') " +
                    "open kursor " +
                    "FETCH NEXT FROM kursor " +
                    "INTO @idrec,@tanggal,@itemid,@qty " +
                    "WHILE @@FETCH_STATUS = 0 " +
                    "begin " +
                    "	insert into #adminksbj(idrec,tanggal,itemid,qty)values(@idrec,@tanggal,@itemid,@qty) " +
                    "	FETCH NEXT FROM kursor " +
                    "	INTO @idrec,@tanggal,@itemid,@qty " +
                    "END " +
                    "CLOSE kursor " +
                    "DEALLOCATE kursor " +

                    "select * into tempData_Rekap  from ( " +
                    "select distinct '0' as ID,'" + periode.Substring(4, 2) + "/1/' + cast(" + thawal + " as varchar) as tanggal,'Saldo Awal' as keterangan,0 as awal,0 as Penerimaan,0 as pengeluaran, " + strawal + " as saldo,0 as hpp,' ' as process  from SaldoInventorybj A inner join FC_Items B on A.ItemID=B.ID   where B.PartNo='" + partno + "' " +
                    "	and A.YearPeriod=" + thawal + " union " +
                    "select ID, tanggal,case  when penerimaan>0 then 'trm dr ' + keterangan else  'trans ke ' + keterangan end keterangan ,awal, " +
                    "Penerimaan,pengeluaran, awal+Penerimaan-pengeluaran as saldo,isnull(HPP,0) as HPP,process from ( " +
                    "select ID, tanggal,partno,keterangan,  " +
                    "case when ID<>' ' then (select isnull(SUM(qty),0) as total from ( " +
                    "select " + strawal + " as qty  from SaldoInventorybj A inner join FC_Items B on A.ItemID=B.ID   where B.PartNo='" + partno + "' " +
                    "	and A.YearPeriod=" + thawal + " " +
                    "	union " +
                    "   SELECT SUM(qty) as qty from #adminksbj where itemid=ain1.itemid and idrec<ain1.ID and LEFT(CONVERT(varchar, tanggal , 112), 6) = '" + periode + "' " +
                    "     )as ain0) end  awal,  " +
                    "Penerimaan,pengeluaran,HPP,process from ( " +
                    "SELECT convert(varchar,A.CreatedTime,112)+ rtrim(CAST( A.ID AS CHAR(10)))+ 'A'+ rtrim(A.ItemID)+rtrim(A.Keterangan) as ID,A.ItemID, B.PartNo,A.Keterangan, A.tgltrans  as tanggal,  " +
                    "A.Qtyin as Penerimaan,0 as Pengeluaran,A.hpp as hpp,A.Process as process  " +
                    "FROM FC_Items AS B INNER JOIN T3_Rekap  AS A ON B.ID = A.ItemID  where  A.process not like 'ex%'  and A.rowstatus>-1 and B.partno='" + partno + "' and A.Qtyin>0  " +
                    "and LEFT(convert(varchar,A.tgltrans,112),6) ='" + periode + "' " +
                    " union  " +
                    "SELECT convert(varchar,A.CreatedTime,112)+ rtrim(CAST( A.ID AS CHAR(10)))+ 'B'+rtrim(A.ItemID)+rtrim(A.Keterangan)as ID,A.ItemID,B.PartNo,A.Keterangan, A.tgltrans  as tanggal,  " +
                    "0 as Penerimaan,A.QtyOut as Pengeluaran,A.hpp as hpp,A.Process as process  " +
                    "FROM FC_Items AS B INNER JOIN T3_Rekap  AS A ON B.ID = A.ItemID  where   A.process not like 'ex%' and A.rowstatus>-1 and B.partno='" + partno + "' and A.QtyOut>0 and  " +
                    "LEFT(convert(varchar,A.tgltrans,112),6) ='" + periode + "'" +
                    " union " +
                    "SELECT convert(varchar,A.CreatedTime,112)+ rtrim(CAST(A.ID AS CHAR(10)))+'C'+ rtrim(A.ItemID) as ID,A.ItemID,B.PartNo,'pengiriman' as Keterangan, convert(varchar,A.tgltrans,112) as tanggal, " +
                    "0 as Penerimaan, isnull(A.Qty,0) as Pengeluaran,A.hpp as hpp,case when A.ID>0 then (select top 1 sjno from t3_kirim where id in (select kirimID from t3_kirimdetail where id=A.ID)) end process  " +
                    "FROM T3_KirimDetail  AS A INNER JOIN FC_Items AS B ON A.ItemID = B.ID  where   A.kirimID>0 and A.rowstatus>-1 and B.partno='" + partno + "' and  " +
                    "LEFT(convert(varchar,A.tgltrans,112),6) ='" + periode + "'" +
                    " union " +
                    "SELECT convert(varchar,A.CreatedTime,112)+ rtrim(CAST(A.ID AS CHAR(10)))+ 'D'+rtrim(A.ItemID) as ID,A.ItemID,B.PartNo,'retur' as Keterangan, " +
                    "convert(varchar,A.tgltrans,112) as tanggal,isnull(A.Qty,0) as Penerimaan, 0 as Pengeluaran,A.hpp as hpp,A.SJno as process " +
                    "FROM T3_Retur AS A INNER JOIN FC_Items AS B ON A.ItemID = B.ID  where  A.rowstatus>-1 and B.partno='" + partno + "' and " +
                    "LEFT(convert(varchar,A.tgltrans,112),6) ='" + periode + "' " +
                    " union " +
                    "SELECT  convert(varchar,C.CreatedTime ,112)+ rtrim(CAST(A.ID AS CHAR(10)))+'E'+ rtrim(A.ItemID) as ID,A.ItemID,B.PartNo,'adjust-In' as Keterangan,  " +
                    "convert(varchar,C.AdjustDate,112) as tanggal,isnull(A.QtyIn,0) as Penerimaan, 0 as Pengeluaran,A.hpp as hpp,case when A.ID>0 then (select top 1 AdjustNo  from t3_Adjust where id in (select AdjustID from t3_Adjustdetail where id=A.ID)) end process  " +
                    "FROM T3_AdjustDetail  AS A INNER JOIN FC_Items AS B ON A.ItemID = B.ID  inner join T3_Adjust as C on C.ID=A.AdjustID " +
                    "where  A.rowstatus>-1 and A.apv>0 and A.QtyIn >0 and  B.partno='" + partno + "' and " +
                    "LEFT(convert(varchar,C.AdjustDate,112),6) ='" + periode + "'" +
                    "union " +
                    "SELECT convert(varchar,C.CreatedTime ,112)+ rtrim(CAST(A.ID AS CHAR(10)))+ 'F'+rtrim(A.ItemID) as ID,A.ItemID,B.PartNo,'adjust-Out' as Keterangan,  " +
                    "convert(varchar,C.AdjustDate,112) as tanggal,isnull(A.QtyIn,0) as Penerimaan, isnull(A.QtyOut,0) as Pengeluaran,A.hpp as hpp,case when A.ID>0 then (select top 1 AdjustNo  from t3_Adjust where id in (select AdjustID from t3_Adjustdetail where id=A.ID)) end process " +
                    "FROM T3_AdjustDetail  AS A INNER JOIN FC_Items AS B ON A.ItemID = B.ID  inner join T3_Adjust as C on C.ID=A.AdjustID " +
                    "where  A.rowstatus>-1 and A.apv>0 and A.QtyOut >0  and B.partno='" + partno + "' and " +
                    "LEFT(convert(varchar,C.AdjustDate,112),6) ='" + periode + "'" +
                    ") as ain1 ) as atALL " +
                    ") as x order by ID " +

                    "drop table #adminksbj ; " +

                    "select * into tempData_Rekap1 from ( " +
                    "select ROW_NUMBER() over (order by Flag asc ) as ID,* from ( " +
                    "select 'A'Flag,keterangan,'0'T,'0'K,saldo from tempData_Rekap where keterangan='saldo awal' " +
                    "union " +
                    "select case when keterangan like'%trans ke%' then 'C' else 'B' end Flag,keterangan,sum(Penerimaan)T,sum(pengeluaran)K,'0'saldo " +
                    "from tempData_Rekap where keterangan<>'saldo awal' group by keterangan " +
                    ") as x " +
                    ") as xx " +

                    "SELECT  Flag,Keterangan,saldo Awal, " +
                    "T Penerimaan, " +
                    "K Pengeluaran, " +
                    "SUM( isnull(saldo,0) + isnull(T,0) - isnull(K,0) ) OVER (ORDER BY ID ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) as Saldo " +
                    "into tempData_Rekap2 " +
                    "FROM tempData_Rekap1 " +
                    "order by ID " +

                    "select keterangan,Awal,Penerimaan,Pengeluaran,Saldo from ( " +
                    "select * from tempData_Rekap2 union " +
                    "select urut,Keterangan,Awal,Penerimaan,Pengeluaran,((Awal+Penerimaan)-Pengeluaran)Saldo from ( " +
                    "select 'D'urut,'Grand Total'Keterangan,sum(Awal)Awal,sum(Penerimaan)Penerimaan,sum(Pengeluaran)Pengeluaran " +
                    "from tempData_Rekap2  ) as x ) as xx order by Flag " +

                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempData_Rekap]') AND type in (N'U')) DROP TABLE [dbo].[tempData_Rekap] " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempData_Rekap1]') AND type in (N'U')) DROP TABLE [dbo].[tempData_Rekap1] " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempData_Rekap2]') AND type in (N'U')) DROP TABLE [dbo].[tempData_Rekap2] ";

            }
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrKartuStock.Add(GenerateObjectRekap(sqlDataReader));
                }
            }
            else
                arrKartuStock.Add(new KartuStock());

            return arrKartuStock;
        }

        public KartuStock GenerateObject(SqlDataReader sqlDataReader)
        {
            objKartuStock = new KartuStock();
            objKartuStock.Keterangan = (sqlDataReader["Keterangan"]).ToString();
            objKartuStock.Tanggal = Convert.ToDateTime (sqlDataReader["Tanggal"]);
            objKartuStock.Awal = Convert.ToInt32(sqlDataReader["Awal"]);
            objKartuStock.Penerimaan  = Convert.ToInt32(sqlDataReader["Penerimaan"]);
            objKartuStock.Pengeluaran  = Convert.ToInt32(sqlDataReader["Pengeluaran"]);
            objKartuStock.Saldo  = Convert.ToInt32(sqlDataReader["Saldo"]);
            objKartuStock.HPP = (sqlDataReader["HPP"] != DBNull.Value) ? Convert.ToDecimal(sqlDataReader["HPP"]) : 0;
            return objKartuStock;
        }
        public KartuStock GenerateObject1(SqlDataReader sqlDataReader)
        {
            objKartuStock = new KartuStock();
            objKartuStock.Keterangan = (sqlDataReader["Keterangan"]).ToString();
            objKartuStock.Tanggal = Convert.ToDateTime(sqlDataReader["Tanggal"]);
            objKartuStock.Awal = Convert.ToInt32(sqlDataReader["Awal"]);
            objKartuStock.Penerimaan = Convert.ToInt32(sqlDataReader["Penerimaan"]);
            objKartuStock.Pengeluaran = Convert.ToInt32(sqlDataReader["Pengeluaran"]);
            objKartuStock.Saldo = Convert.ToInt32(sqlDataReader["Saldo"]);
            objKartuStock.HPP = (sqlDataReader["HPP"] != DBNull.Value) ? Convert.ToDecimal(sqlDataReader["HPP"]) : 0;
            objKartuStock.Process = (sqlDataReader["process"]).ToString();
            return objKartuStock;
        }
        public KartuStock GenerateObject2(SqlDataReader sqlDataReader)
        {
            objKartuStock = new KartuStock();
            //objKartuStock.Keterangan = (sqlDataReader["Keterangan"]).ToString();
            objKartuStock.Tanggal = Convert.ToDateTime(sqlDataReader["Tanggal"]);
            objKartuStock.Awal = Convert.ToInt32(sqlDataReader["Awal"]);
            objKartuStock.Penerimaan = Convert.ToInt32(sqlDataReader["Penerimaan"]);
            objKartuStock.Pengeluaran = Convert.ToInt32(sqlDataReader["Pengeluaran"]);
            objKartuStock.Saldo = Convert.ToInt32(sqlDataReader["Saldo"]);
            //objKartuStock.HPP = (sqlDataReader["HPP"] != DBNull.Value) ? Convert.ToDecimal(sqlDataReader["HPP"]) : 0;
            objKartuStock.Process = (sqlDataReader["process"]).ToString();
            return objKartuStock;
        }
        public KartuStock GenerateObjectRekap(SqlDataReader sqlDataReader)
        {
            objKartuStock = new KartuStock();
            objKartuStock.Keterangan = (sqlDataReader["Keterangan"]).ToString();
            //objKartuStock.Tanggal = Convert.ToDateTime(sqlDataReader["Tanggal"]);
            objKartuStock.Awal = Convert.ToInt32(sqlDataReader["Awal"]);
            objKartuStock.Penerimaan = Convert.ToInt32(sqlDataReader["Penerimaan"]);
            objKartuStock.Pengeluaran = Convert.ToInt32(sqlDataReader["Pengeluaran"]);
            objKartuStock.Saldo = Convert.ToInt32(sqlDataReader["Saldo"]);
            //objKartuStock.HPP = (sqlDataReader["HPP"] != DBNull.Value) ? Convert.ToDecimal(sqlDataReader["HPP"]) : 0;
            //objKartuStock.Process = (sqlDataReader["process"]).ToString();
            return objKartuStock;
        }
    }
}
