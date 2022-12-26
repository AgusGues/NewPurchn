using Dapper;
using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BusinessFacade
{
    public class AccountingReportFacade : AbstractFacade
    {
        public override int Delete(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override int Insert(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }

        public override int Update(object objDomain)
        {
            throw new NotImplementedException();
        }

        public static List<LapMutasiWIP> GetLapMutasiWIP(int thnbln, int lastperiode, string periode)
        {
            List<LapMutasiWIP> alldata = new List<LapMutasiWIP>();
            string strField = string.Empty;
            string strsql = "";

            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {

                    if (thnbln <= Convert.ToInt32(periode))
                    {
                        strsql = "declare @HMY varchar(6) " +
                            "declare @thnbln varchar(6) " +
                            "select @HMY='" + thnbln + "' " +
                            "select @thnbln='" + lastperiode + "' " +
                            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempWIP1]') AND type in (N'U')) DROP TABLE [dbo].[tempWIP1] " +
                            "select * into tempWIP1 from vw_KartustockWIPOld where left(CONVERT(char, tanggal ,112),6)=@HMY " +
                            "select itemID,HMY,NoDocument,AwalQty, " + "InProdQty,InAdjustQty,outAdjustQty,H99,B99,C99,Q99,InP99,OutP99,(select (Tebal/1000)*(Lebar/1000)*(Panjang/1000) from fc_items where ID= M2.itemid)as volume from (  " +
                            "select itemID,HMY,partno as NoDocument, " +
                            "(select isnull(SUM(qty),0) from BM_Destacking where qty>0 and LEFT(CONVERT(varchar,TglProduksi, 112), 6) = M1.HMY " +
                            "and ItemID=M1.ItemID and RowStatus>-1 and lokasiID not in (select ID from fc_lokasi where lokasi like '%adj%')) as InProdQty," +
                            "(select isnull(SUM(qty),0) from tempWIP1 where qty>0 and ItemID0=M1.ItemID and left(CONVERT(char, tanggal ,112),6)=M1.HMY and idrec like '%in%') as InAdjustQty, " +
                            "(select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID and left(CONVERT(char, tanggal ,112),6)=M1.HMY and (lokasi='H99' or lokasi like '%in%')) as H99, " +
                            "(select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID and left(CONVERT(char, tanggal ,112),6)=M1.HMY and lokasi='B99') as B99, " +
                            "(select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID and left(CONVERT(char, tanggal ,112),6)=M1.HMY and lokasi='C99') as C99, " +
                            "(select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID and left(CONVERT(char, tanggal ,112),6)=M1.HMY and lokasi='Q99') as Q99, " +
                            "(select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID and left(CONVERT(char, tanggal ,112),6)=M1.HMY and idrec like '%ou%') as outAdjustQty,0 as InP99, 0 as OutP99, " +
                            "(select isnull(SUM(saldo),0) from T1_SaldoPerLokasi where  rowstatus>-1 and thnbln=@thnbln and itemid =M1.ItemID and lokid not in (select ID from fc_lokasi where lokasi like '%p99%'))as AwalQty " +
                            "from(select distinct A.itemid as ItemID,(select partno from fc_items where ID=A.itemid) as partno, @HMY as HMY from (  " +
                            "select distinct itemid  from bm_destacking where rowstatus>-1 and tglproduksi>='6/1/2013' " +
                            ") as A " +
                            ") as M1 " +
                            ") as M2 where Awalqty<>0 or InProdQty<>0 or InAdjustQty<>0 or outAdjustQty<>0 or H99<>0 or B99<>0 or C99<>0 or Q99<>0 order by M2.NoDocument ";
                    }
                    else
                    {
                        strsql = "declare @HMY varchar(6) " +
                            "declare @thnbln varchar(6)" +
                            "select @HMY = " + thnbln + " " +
                            "select @thnbln = " + lastperiode + " " +
                            "IF EXISTS(SELECT* FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempWIP1]') AND type in (N'U')) DROP TABLE[dbo].[tempWIP1] " +
                            "select * into tempWIP1 from vw_KartustockWIP where left(CONVERT(char, tanggal, 112), 6) = @HMY " +
                            "IF EXISTS(SELECT* FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempWIP2]') AND type in (N'U')) DROP TABLE[dbo].[tempWIP2] " +
                            "select* into tempWIP2 from vw_KartustockWIP2 where left(CONVERT(char, tanggal, 112), 6) = @HMY " +
                            "SELECT a.*, (AwalQty + InProdQty + InP99 + InAdjustQty + InI99 - H99 - I99 - B99 - C99 - Q99 - OutP99 - OutAdjustQty) AkhirQty, Round((AwalQty + InProdQty + InP99 + InAdjustQty + InI99 - H99 - I99 - B99 - C99 - Q99 - OutP99 - OutAdjustQty) * Volume, 2) M3, Tebal* InProdQty M3Produksi " +
                            "FROM(select itemID, HMY, NoDocument, sum(AwalQty) as AwalQty, sum(InProdQty) as InProdQty, sum(InP99) as InP99, sum(InAdjustQty) as InAdjustQty, sum(InI99) as InI99, sum(outAdjustQty) as outAdjustQty, sum(H99) as H99, sum(OutI99) as I99, sum(B99) as B99, sum(C99) as C99, sum(Q99) as Q99, sum(OutP99) as OutP99, (select(Tebal / 1000) * (Lebar / 1000) * (Panjang / 1000) from fc_items where ID = atALL.itemid) as volume,(select Tebal from fc_items where ID = atALL.itemid) Tebal from(select itemID, HMY, NoDocument, AwalQty, InProdQty, InP99, InAdjustQty, InI99, outAdjustQty, H99, OutI99, B99, C99, Q99, OutP99 from (select itemID, HMY,'A' + partno as NoDocument,           (select isnull(SUM(qty), 0) from BM_Destacking where qty > 0 and LEFT(CONVERT(varchar, TglProduksi, 112), 6) = M1.HMY           and ItemID = M1.ItemID and RowStatus> -1 and lokasiID not in (select ID from fc_lokasi where lokasi like '%in%')) as InProdQty,0 as InP99,          (select isnull(SUM(qty), 0) from tempWIP1 where qty > 0 and ItemID0 = M1.ItemID  and idrec like '%in%') as InAdjustQty,  0 as InI99,          (select isnull(SUM(qty) * -1, 0) from tempWIP1 where qty < 0 and ItemID0 = M1.ItemID  and(lokasi = 'H99' and process <> 'lari' and process <> 'listplank')) as H99,           (select isnull(SUM(qty) * -1, 0) from tempWIP1 where qty < 0 and ItemID0 = M1.ItemID   and(lokasi = 'i99' and process <> 'lari' and process <> 'listplank')) as OutI99,           (select isnull(SUM(qty) * -1, 0) from tempWIP1 where qty < 0 and ItemID0 = M1.ItemID  and lokasi = 'B99' and process<>'lari' and process<>'listplank' ) as B99,           (select isnull(SUM(qty) * -1, 0) from tempWIP1 where qty < 0 and ItemID0 = M1.ItemID  and lokasi = 'C99' and process<>'lari' and process<>'listplank') as C99,           (select isnull(SUM(qty) * -1, 0) from tempWIP1 where qty < 0 and ItemID0 = M1.ItemID  and lokasi = 'Q99' and process<>'lari' and process<>'listplank') as Q99,           (select isnull(SUM(qty) * -1, 0) from tempWIP1 where qty < 0 and ItemID0 = M1.ItemID  and process = 'lari') as OutP99,          (select isnull(SUM(qty) * -1, 0) from tempWIP1 where qty < 0 and ItemID0 = M1.ItemID  and idrec like '%ou%') as outAdjustQty,           (select isnull(SUM(saldo), 0) from T1_SaldoPerLokasi where rowstatus > -1 and thnbln = @thnbln and itemid = M1.ItemID and LokID not in (select ID from FC_Lokasi where Lokasi = 'p99') )as AwalQty           from(select distinct A.itemid as ItemID, (select partno from fc_items where ID = A.itemid) as partno, @HMY as HMY from(select distinct itemid  from bm_destacking where rowstatus > -1 and tglproduksi >= '6/1/2013') as A       ) as M1   ) as M2 where Awalqty<>0 or InProdQty<>0 or InAdjustQty<>0 or outAdjustQty<>0 or H99<>0 or OutI99<>0  or B99<>0 or C99<>0 or Q99<>0 " +
                            "union all " +
                            "select itemID, HMY, NoDocument, AwalQty, InProdQty, InP99, InAdjustQty, InI99, outAdjustQty, H99, OutI99, B99, C99, Q99, OutP99 from(select itemID, HMY, 'PP:' + partno as NoDocument, 0 as InProdQty, (select isnull(SUM(qty), 0) from tempWIP2 where qty > 0  and idrec not like '%IN%' and ItemID0 = M1.ItemID and       left(CONVERT(char, tanggal, 112), 6) = M1.HMY and process = 'lari') as InP99,      (select isnull(SUM(qty), 0) from tempWIP2 where qty > 0 and ItemID0 = M1.ItemID and left(CONVERT(char, tanggal,112),6)= M1.HMY and idrec like '%in%'  and process = 'lari') as InAdjustQty, 0 as InI99,      (select isnull(SUM(qty) * -1, 0) from tempWIP2 V inner join FC_Items I on V.itemID = I.ID and(I.partno like '%-3-%' or I.PartNo like '%-W-%' or I.PartNo like '%-m-%') where qty < 0  and idrec not like '%ou%'  and ItemID0 = M1.ItemID   and process = 'lari') as H99,    (select isnull(SUM(qty) * -1, 0) from tempWIP2 V inner join FC_Items I on V.itemID = I.ID and(I.partno like '%-p-%' or I.partno  like '%-1-%') where qty < 0  and idrec not like '%ou%'  and ItemID0 = M1.ItemID   and process = 'lari'and lokid1  in (select ID from fc_lokasi where lokasi = 'I99')) as OutI99,      (select isnull(SUM(qty) * -1, 0) from tempWIP2 V inner join FC_Items I on V.itemID = I.ID and(I.partno  like '%-p-%') where qty < 0  and idrec not like '%ou%'  and ItemID0 = M1.ItemID   and process = 'lari'and lokid1 not in (select ID from fc_lokasi where lokasi = 'I99')) as B99,    (select isnull(SUM(qty) * -1, 0) from tempWIP2 V inner join FC_Items I on V.itemID = I.ID and(I.partno  like '%-1-%') where qty < 0  and idrec not like '%ou%'  and ItemID0 = M1.ItemID   and process = 'lari'and lokid1 not in (select ID from fc_lokasi where lokasi = 'I99') and V.LastModifiedBy <> 'adjust' ) as C99, 0 as Q99, 0 as OutP99,       (select isnull(SUM(qty) * -1, 0) from tempWIP2 where qty < 0 and ItemID0 = M1.ItemID and idrec like '%ou%') as outAdjustQty,       (select isnull(SUM(saldo), 0) from T1_SaldoPerLokasi where rowstatus > -1 and thnbln = @thnbln       and itemid = M1.ItemID and LokID in (select ID from FC_Lokasi where Lokasi = 'p99'))as AwalQty       from(select distinct A.itemid0 as ItemID, (select partno from fc_items where ID = A.itemid0) as partno, @HMY as HMY from(select distinct itemid0  from vw_KartuStockWIP2 where tanggal >= '6/1/2013'          ) as A       ) as M1   ) as M2 where Awalqty<>0 or InProdQty<>0 or InAdjustQty<>0 or outAdjustQty<>0 or H99<>0 or OutI99<>0 or B99<>0 or C99<>0 or Q99<>0 or InP99<>0 or OutP99<>0   ) as atALL group by itemID,HMY,NoDocument)a order by NoDocument " +
                            "IF EXISTS(SELECT* FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempWIP1]') AND type in (N'U')) DROP TABLE[dbo].[tempWIP1] " +
                            "IF EXISTS(SELECT* FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempWIP2]') AND type in (N'U')) DROP TABLE[dbo].[tempWIP2]";
                    }
                    alldata = connection.Query<LapMutasiWIP>(strsql).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }

        public static List<LapMutasiWIP> GetLapMutasiWIPHarian(int thnbln, int lastpriode, int thnbln0, string periode)
        {
            List<LapMutasiWIP> alldata = new List<LapMutasiWIP>();
            string strField = string.Empty;
            string strsql = "";
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    strsql = "declare @HMY1 varchar(10)  " +
                        "declare @HMY2 varchar(10) " +
                        "declare @thnbln varchar(6)  " +
                        "select @HMY1='" + thnbln + "' + '01' " +
                        "select @HMY2=" + thnbln0 + "" +
                        "select @thnbln=" + lastpriode + "" +
                        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempWIP1]') AND type in (N'U')) DROP TABLE [dbo].[tempWIP1] " +
                        "select * into tempWIP1 from vw_KartustockWIP where CONVERT(char, tanggal ,112)>=@HMY1 and CONVERT(char, tanggal ,112)<=@HMY2 IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempWIP2]') AND type in (N'U')) DROP TABLE [dbo].[tempWIP2] select * into tempWIP2 from vw_KartustockWIP2 where CONVERT(char, tanggal ,112)>=@HMY1 and CONVERT(char, tanggal ,112)<=@HMY2 " +
                        "SELECT a.*, (AwalQty + InProdQty + InP99 + InAdjustQty + InI99 - H99 - I99 - B99 - C99 - Q99 - OutP99 - OutAdjustQty) AkhirQty, Round((AwalQty + InProdQty + InP99 + InAdjustQty + InI99 - H99 - I99 - B99 - C99 - Q99 - OutP99 - OutAdjustQty) * Volume, 2) M3, Tebal* InProdQty M3Produksi  FROM (select itemID,HMY,NoDocument,sum(AwalQty) as AwalQty,sum(InProdQty) as InProdQty,sum(InP99) as InP99,sum(InAdjustQty) as InAdjustQty,sum(InI99) as InI99,  sum(outAdjustQty) as outAdjustQty,sum(H99) as H99,sum(OutI99) as I99,sum(B99) as B99,sum(C99) as C99,sum(Q99) as Q99,sum(OutP99) as OutP99,  (select (Tebal/1000)*(Lebar/1000)*(Panjang/1000) from fc_items where ID= atALL.itemid)as volume,(select Tebal from fc_items where ID= atALL.itemid) Tebal from(      select itemID,HMY,NoDocument,AwalQty,InProdQty,InP99,InAdjustQty,InI99,outAdjustQty,H99,OutI99,B99,C99,Q99,OutP99 from (            select itemID,HMY,'A' + partno as NoDocument,           (select isnull(SUM(qty),0) from BM_Destacking where qty>0 and CONVERT(char, tglproduksi ,112)>=@HMY1 and CONVERT(char, tglproduksi ,112)<=@HMY2           and ItemID=M1.ItemID and RowStatus>-1 and lokasiID not in (select ID from fc_lokasi where lokasi like '%in%')) as InProdQty,0 as InP99,          (select isnull(SUM(qty),0) from tempWIP1 where qty>0 and ItemID0=M1.ItemID  and idrec like '%in%') as InAdjustQty,  0 as InI99,          (select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID  and (lokasi='H99' and process<>'lari' and process<>'listplank')) as H99,           (select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID   and (lokasi='i99' and process<>'lari' and process<>'listplank')) as OutI99,           (select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID  and lokasi='B99' and process<>'lari' and process<>'listplank' ) as B99,           (select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID  and lokasi='C99' and process<>'lari' and process<>'listplank') as C99,           (select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID  and lokasi='Q99' and process<>'lari' and process<>'listplank') as Q99,           (select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID  and process='lari') as OutP99,          (select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID  and idrec like '%ou%') as outAdjustQty,           (select isnull(SUM(saldo),0) from T1_SaldoPerLokasi where  rowstatus>-1 and thnbln=@thnbln and itemid =M1.ItemID and           LokID not in (select ID from FC_Lokasi where Lokasi='p99'))+isnull((select SUM(qty) from vw_KartustockWIP where ItemID0=M1.ItemID and process<>'lari' and process<>'listplank' and left(CONVERT(varchar,tanggal, 112),6) = left(@HMY1,6) and CONVERT(varchar,tanggal, 112) <@HMY1),0) as AwalQty           from(select distinct A.itemid as ItemID,(select partno from fc_items where ID=A.itemid) as partno, @HMY1 as HMY from (          select distinct itemid  from bm_destacking where tglproduksi>='6/1/2013'          ) as A       ) as M1   ) as M2 where Awalqty<>0 or InProdQty<>0 or InAdjustQty<>0 or outAdjustQty<>0 or H99<>0 or OutI99<>0  or B99<>0 or C99<>0 or Q99<>0   union all   select itemID,HMY,NoDocument,AwalQty,InProdQty,InP99,InAdjustQty,InI99,outAdjustQty,H99,OutI99,B99,C99,Q99,OutP99 from (        select itemID,HMY,'PP:' + partno as NoDocument,0 as InProdQty,      (select isnull(SUM(qty),0) from tempWIP2 where qty>0  and idrec not like '%IN%' and ItemID0=M1.ItemID and       process='lari') as InP99,      (select isnull(SUM(qty),0) from tempWIP2 where qty>0 and ItemID0=M1.ItemID and       idrec like '%in%'  and process='lari') as InAdjustQty,  0 as InI99,      (select isnull(SUM(qty)*-1,0) from tempWIP2 V inner join FC_Items I on V.itemID=I.ID and (I.partno like '%-3-%' or I.PartNo like '%-W-%' or I.PartNo like '%-m-%')       where qty<0  and idrec not like '%ou%' and ItemID0=M1.ItemID   and process='lari') as H99,0 as OutI99,     (select isnull(SUM(qty)*-1,0) from tempWIP2 V inner join FC_Items I on V.itemID=I.ID and (I.partno  like '%-p-%' or I.partno  like '%-1-%' )       where qty<0  and idrec not like '%ou%'  and ItemID0=M1.ItemID   and process='lari') as B99,0 as C99, 0 as Q99, 0 as OutP99,       (select isnull(SUM(qty)*-1,0) from tempWIP2 where qty<0 and ItemID0=M1.ItemID        and idrec like '%ou%') as outAdjustQty, (select isnull(SUM(saldo),0) from T1_SaldoPerLokasi where  rowstatus>-1 and thnbln=@thnbln       and itemid =M1.ItemID and LokID in (select ID from FC_Lokasi where Lokasi='p99'))+isnull((select SUM(qty) from vw_KartustockWIP2 where ItemID0=M1.ItemID and process='lari' and left(CONVERT(varchar,tanggal, 112),6) = left(@HMY1,6) and CONVERT(varchar,tanggal, 112) <@HMY1),0)as AwalQty       from(select distinct A.itemid0 as ItemID,(select partno from fc_items where ID=A.itemid0) as partno, @HMY1 as HMY from (          select distinct itemid0  from vw_KartuStockWIP2 where tanggal >='6/1/2013'          ) as A       ) as M1   ) as M2 where Awalqty<>0 or InProdQty<>0 or InAdjustQty<>0 or outAdjustQty<>0 or H99<>0 or OutI99<>0 or B99<>0 or C99<>0 or Q99<>0 or InP99<>0 or OutP99<>0   ) as atALL group by itemID,HMY,NoDocument) a order by NoDocument";
                    alldata = connection.Query<LapMutasiWIP>(strsql).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }

        public static List<LapMutasiBJ> GetLapMutasiBJ(int thnbln, string strQtyLastMonth, int thn, string periode)
        {
            List<LapMutasiBJ> alldata = new List<LapMutasiBJ>();
            string strField = string.Empty;
            string strsql = "";
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    if (thnbln <= Convert.ToInt32(periode))
                    {
                        strsql = "declare @periode varchar(10) " +
                            "select @periode='" + thnbln + "'" +
                            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJ]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJ] " +
                            "select * into tempKartuStockBJ from vw_KartuStockBJ where left(CONVERT(char, tanggal ,112),6)=@periode " +
                            "select *,Awal+TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                            "isnull((select distinct  isnull(sum(" + strQtyLastMonth + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + thn + "),0) as Awal from (select *,  " +
                            "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJ where process='direct' and ItemID=A.ItemID ) as TWIP,  " +
                            "0 as HTWIP,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJ where  qty>0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and (Keterangan like '%-p-%' or Keterangan like '%-1-%')) as TBP,  " +
                            "0 as HTBP,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJ where qty>0 and ItemID=A.ItemID  " +
                            " and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%')) as TBJ,  " +
                            "0 as HTBJ,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJ where ItemID=A.ItemID and process='retur') as TRETUR,  " +
                            "0 as HTRETUR,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJ where ItemID=A.ItemID and process='Adjust-in') as TAdjust,  " +
                            "0 as HTAdjust,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where ItemID=A.ItemID and process='kirim') as KKirim,  " +
                            "0 as HKKirim,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where qty <0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and Keterangan like '%-p-%' " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBP,  " +
                            "0 as HKBP,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where qty <0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%') " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBJ,  " +
                            "0 as HKBJ,  " +
                            "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJ where qty <0 and  ItemID=A.ItemID   " +
                            "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                            "0 as HKSample,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where qty <0 and ItemID=A.ItemID   and Keterangan like '%-s-%' " +
                            "and (process  like '%simetris%' )  and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBS,  " +
                            "0 as HKBS,  " +
                            "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJ where ItemID=A.ItemID and process='Adjust-out'  ) as KAdjust,  " +
                            "0 as HKAdjust from (  " +
                            "select @Periode as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume  from fc_items where (partno like '%-3-%' or partno like '%-w-%' or partno like '%-m-%')) as B0) as A  ) as B  " +
                            ") as C where  awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
                    }
                    else if (thnbln >= 201906)
                    {
                        strsql = "declare @periode varchar(10) " +
                            "select @periode='" + thnbln + "'" +
                            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew] " +
                            "select * into tempKartuStockBJNew from vw_KartuStockBJNew where left(CONVERT(char, tanggal ,112),6)=@periode  " +
                            "select *,Awal+TWIP+TBP+TBJ+TBS+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                            "isnull((select distinct  isnull(sum(" + strQtyLastMonth + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + thn + "),0) as Awal from (select *,  " +
                            "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and (isnull(sfrom,'-')='-' " +
                            " or isnull(sfrom,'-')='i99R1' or isnull(sfrom,'-')='i99R2' or isnull(sfrom,'-')='i99R3' or isnull(sfrom,'-')='bevel' or isnull(sfrom,'-')='straping' or isnull(sfrom,'-')='RsR1')and ItemID=A.ItemID )+ " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and (Keterangan like '%-1-%')) as TWIP,  " +
                            "0 as HTWIP,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and ( " +
                            "isnull(sfrom,'-') like 'bevel%' or isnull(sfrom,'-') like 'strapingr%')and ItemID=A.ItemID )+ " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and (Keterangan like '%-p-%')) as TBP,  " +
                            "0 as HTBP,  " + "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID  " +
                            " and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%' or Keterangan like '%-m-%')) as TBJ,  " +
                            "0 as HTBJ,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID  " +
                            " and (process  like '%simetris%' ) and (Keterangan like '%-S-%')) as TBS,  " +
                            "0 as HTBS,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='retur') as TRETUR,  " +
                            "0 as HTRETUR,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-in') as TAdjust,  " +
                            "0 as HTAdjust,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='kirim') as KKirim,  " +
                            "0 as HKKirim,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and Keterangan like '%-p-%' " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBP,  " +
                            "0 as HKBP,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%' or Keterangan like '%-m-%') " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBJ,  " +
                            "0 as HKBJ,  " +
                            "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where qty <0 and  ItemID=A.ItemID   " +
                            "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                            "0 as HKSample,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID   and Keterangan like '%-s-%' " +
                            "and (process  like '%simetris%' )  and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBS,  " +
                            "0 as HKBS,  " +
                            "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out'  ) as KAdjust,  " +
                            "0 as HKAdjust from (  " +
                            "select @Periode as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume  from fc_items " +
                            "where (partno like '%-3-%' or partno like '%-w-%' or partno like '%-m-%')) as B0) as A  ) as B  " +
                            ") as C where awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TBS<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
                    }
                    else
                    {
                        strsql = "declare @periode varchar(10) " +
                            "select @periode='" + thnbln + "'" +
                            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew] " +
                            "select * into tempKartuStockBJNew from vw_KartuStockBJNew where left(CONVERT(char, tanggal ,112),6)=@periode  " +
                            "select *,Awal+TWIP+TBP+TBJ+TBS+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                            "isnull((select distinct  isnull(sum(" + strQtyLastMonth + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + thn + "),0) as Awal from (select *,  " +
                            "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and ItemID=A.ItemID )+ " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and (Keterangan like '%-1-%')) as TWIP,  " +
                            "0 as HTWIP,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and (Keterangan like '%-p-%')) as TBP,  " +
                            "0 as HTBP,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID  " +
                            " and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%' or Keterangan like '%-m-%')) as TBJ,  " +
                            "0 as HTBJ,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID  " +
                            " and (process  like '%simetris%' ) and (Keterangan like '%-S-%')) as TBS,  " +
                            "0 as HTBS,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='retur') as TRETUR,  " +
                            "0 as HTRETUR,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-in') as TAdjust,  " +
                            "0 as HTAdjust,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='kirim') as KKirim,  " +
                            "0 as HKKirim,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and Keterangan like '%-p-%' " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBP,  " +
                            "0 as HKBP,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%' or Keterangan like '%-m-%') " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBJ,  " +
                            "0 as HKBJ,  " +
                            "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where qty <0 and  ItemID=A.ItemID   " +
                            "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                            "0 as HKSample,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID   and Keterangan like '%-s-%' " +
                            "and (process  like '%simetris%' )  and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBS,  " +
                            "0 as HKBS,  " +
                            "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out'  ) as KAdjust,  " +
                            "0 as HKAdjust from (  " +
                            "select @Periode as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume  from fc_items " +
                            "where (partno like '%-3-%' or partno like '%-w-%' or partno like '%-m-%')) as B0) as A  ) as B  " +
                            ") as C where awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TBS<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
                    }

                    alldata = connection.Query<LapMutasiBJ>(strsql).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }

        public static List<LapMutasiBJ> GetLapMutasiBJHarian(int thnbln, string strQtyLastMonth, int thn, int thnbln0, string periode)
        {
            List<LapMutasiBJ> alldata = new List<LapMutasiBJ>();
            string strField = string.Empty;
            string strsql = "";
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    if (thnbln <= Convert.ToInt32(periode))
                    {
                        strsql = "select *,Awal+TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                            "isnull((select distinct  isnull(sum(" + strQtyLastMonth + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + thn + ") +" +
                            "(select isnull(SUM(Qty),0) from vw_KartuStockBJNew where  ItemID=B.ItemID " +
                            "and convert(varchar,tanggal ,112)>= (SUBSTRING(B.HMY1,1,6)+'01') and convert(varchar,tanggal ,112)<B.HMY1),0) as Awal from (select *,  " +
                            "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                            "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where process='direct' and ItemID=A.ItemID and  convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 ) as TWIP,  " +
                            "0 as HTWIP,  " +
                            "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where  qty>0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2  " +
                            "and (process  like '%simetris%' ) and (Keterangan like '%-p-%' or Keterangan like '%-1-%')) as TBP,  " +
                            "0 as HTBP,  " +
                            "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where qty>0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                            " and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%')) as TBJ,  " +
                            "0 as HTBJ,  " +
                            "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 and process='retur') as TRETUR,  " +
                            "0 as HTRETUR,  " +
                            "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 and process='Adjust-in') as TAdjust,  " +
                            "0 as HTAdjust,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 and process='kirim') as KKirim,  " +
                            "0 as HKKirim,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where qty <0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                            "and (process  like '%simetris%' ) and Keterangan like '%-p-%' " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBP,  " +
                            "0 as HKBP,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where qty <0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                            "and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%') " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBJ,  " +
                            "0 as HKBJ,  " +
                            "(select isnull(SUM(Qty),0) * -1 from vw_KartuStockBJ where qty <0 and  ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                            "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                            "0 as HKSample,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where qty <0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 and Keterangan like '%-s-%' " +
                            "and (process  like '%simetris%' )  and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBS,  " +
                            "0 as HKBS,  " +
                            "(select isnull(SUM(Qty),0) * -1 from vw_KartuStockBJ where ItemID=A.ItemID and process='Adjust-out' and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 ) as KAdjust,  " +
                            "0 as HKAdjust from (  " +
                            "select  '" + thnbln + "' as HMY1,'" + thnbln0 + "' as HMY2,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume  from fc_items where (partno like '%-3-%' or partno like '%-w-%' or partno like '%-m-%')) as B0) as A  ) as B  " +
                            ") as C where awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
                    }
                    else if (thnbln >= 201906)
                    {
                        strsql = "declare @periode1 varchar(10) " +
                         "declare @periode2 varchar(10) " +
                         "select @periode1='" + thnbln + "' + '01' " +
                         "select @periode2='" + thnbln0 + "' " +
                         "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew] " +
                         "select * into tempKartuStockBJNew from vw_KartuStockBJNew where CONVERT(char, tanggal ,112)>=@periode1 and CONVERT(char, tanggal ,112)<=@periode2 and ItemID in (select ID from FC_Items where PartNo like '%-3-%' or partno like '%-w-%' or PartNo like '%-M-%') " +
                         "select *,Awal+TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                         "isnull((select distinct  isnull(sum(" + strQtyLastMonth + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + thn + ") +" +
                         "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  ItemID=B.ItemID " +
                         "and convert(varchar,tanggal ,112)>= (SUBSTRING(B.HMY1,1,6)+'01') and convert(varchar,tanggal ,112)<B.HMY1),0) as Awal from (select *,  " +
                         "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                         "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and (isnull(sfrom,'-')='-' " +
                         "or isnull(sfrom,'-')='bevel' or isnull(sfrom,'-')='straping') and ItemID=A.ItemID and " +
                         "convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2) + " +
                         "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 " +
                         "and convert(varchar,tanggal ,112)<=A.HMY2  and (process  like '%simetris%' ) and (Keterangan like '%-1-%'))as TWIP,  " +
                         "0 as HTWIP,  " +
                         "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and ( " +
                         "isnull(sfrom,'-') like 'bevelr%' or isnull(sfrom,'-') like 'strapingr%') and ItemID=A.ItemID and " +
                         "convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2) + " +
                         "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                         "and (process  like '%simetris%' ) and (Keterangan like '%-p-%')) as TBP,  " +
                         "0 as HTBP,  " +
                         "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                         " and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%' or Keterangan like '%-m-%')) as TBJ,  " +
                         "0 as HTBJ,  " +
                         "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                         " and (process  like '%simetris%' ) and (Keterangan like '%-S-%')) as TBS,  " +
                         "0 as HTBS,  " +
                         "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 and process='retur') as TRETUR,  " +
                         "0 as HTRETUR,  " +
                         "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 and process='Adjust-in') as TAdjust,  " +
                         "0 as HTAdjust,  " +
                         "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 and process='kirim') as KKirim,  " +
                         "0 as HKKirim,  " +
                         "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                         "and (process  like '%simetris%' ) and Keterangan like '%-p-%' " +
                         "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBP,  " +
                         "0 as HKBP,  " +
                         "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                         "and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%' or Keterangan like '%-m-%') " +
                         "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBJ,  " +
                         "0 as HKBJ,  " +
                         "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where qty <0 and  ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2  " +
                         "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                         "0 as HKSample,  " +
                         "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2  and Keterangan like '%-s-%' " +
                         "and (process  like '%simetris%' )  and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBS,  " +
                         "0 as HKBS,  " +
                         "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out' and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 ) as KAdjust,  " +
                         "0 as HKAdjust from (  " +
                         "select  '" + thnbln + "' + '01' as HMY1,'" + thnbln0 + "' as HMY2,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume  from fc_items where (partno like '%-3-%' or partno like '%-w-%' or partno like '%-m-%')) as B0) as A  ) as B  " +
                         ") as C where awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
                    }
                    else
                    {
                        strsql = "declare @periode1 varchar(10) " +
                            "declare @periode2 varchar(10) " +
                            "select @periode1='" + thnbln + "' + '01' " +
                            "select @periode2='" + thnbln + "' " +
                            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew] " +
                            "select * into tempKartuStockBJNew from vw_KartuStockBJNew where CONVERT(char, tanggal ,112)>=@periode1 and CONVERT(char, tanggal ,112)<=@periode2 and ItemID in (select ID from FC_Items where PartNo like '%-3-%' or partno like '%-w-%' or PartNo like '%-M-%') " +
                            "select *,TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                            "isnull((select distinct  isnull(sum(" + strQtyLastMonth + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + thn + ") +" +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  ItemID=B.ItemID " +
                            "and convert(varchar,tanggal ,112)>= (SUBSTRING(B.HMY1,1,6)+'01') and convert(varchar,tanggal ,112)<B.HMY1),0) as Awal from (select *,  " +
                            "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2) + (select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                            "and (process  like '%simetris%' ) and (Keterangan like '%-1-%'))as TWIP,  " +
                            "0 as HTWIP,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                            "and (process  like '%simetris%' ) and (Keterangan like '%-p-%')) as TBP,  " +
                            "0 as HTBP,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                            " and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%' or Keterangan like '%-m-%')) as TBJ,  " +
                            "0 as HTBJ,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                            " and (process  like '%simetris%' ) and (Keterangan like '%-S-%')) as TBS,  " +
                            "0 as HTBS,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 and process='retur') as TRETUR,  " +
                            "0 as HTRETUR,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 and process='Adjust-in') as TAdjust,  " +
                            "0 as HTAdjust,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 and process='kirim') as KKirim,  " +
                            "0 as HKKirim,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                            "and (process  like '%simetris%' ) and Keterangan like '%-p-%' " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBP,  " +
                            "0 as HKBP,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                            "and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%' or Keterangan like '%-m-%') " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBJ,  " +
                            "0 as HKBJ,  " +
                            "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where qty <0 and  ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2  " +
                            "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                            "0 as HKSample,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2  and Keterangan like '%-s-%' " +
                            "and (process  like '%simetris%' )  and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBS,  " +
                            "0 as HKBS,  " +
                            "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out' and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 ) as KAdjust,  " +
                            "0 as HKAdjust from (  " +
                            "select  '" + thnbln + "' + '01' as HMY1,'" + thnbln0 + "' as HMY2,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume  from fc_items where (partno like '%-3-%' or partno like '%-w-%' or partno like '%-m-%')) as B0) as A  ) as B  " +
                            ") as C where awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
                    }

                    alldata = connection.Query<LapMutasiBJ>(strsql).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }

        public static List<LapMutasiBJ> GetLapMutasiBJKonversi(int thnbln, string strQtyLastMonth, int thn, string periode)
        {
            List<LapMutasiBJ> alldata = new List<LapMutasiBJ>();
            string strField = string.Empty;
            string strsql = "";
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    if (thnbln <= Convert.ToInt32(periode))
                    {
                        strsql = "select *,Awal+TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                            "round(isnull((select distinct  isnull(sum(" + strQtyLastMonth + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + thn + "),0) *(Luas/LuasUtuh),0) as Awal from (select *,  " +
                            "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                            "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where process='direct' and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY) *(Luas/LuasUtuh) as TWIP,  " +
                            "0 as HTWIP,  " +
                            "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where  qty>0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                            "and (process  like '%simetris%' ) and (Keterangan like '%-p-%' or Keterangan like '%-1-%')) *(Luas/LuasUtuh) as TBP,  " +
                            "0 as HTBP,  " +
                            "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where qty>0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                            " and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%')) *(Luas/LuasUtuh) as TBJ,  " +
                            "0 as HTBJ,  " +
                            "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='retur') *(Luas/LuasUtuh) as TRETUR,  " +
                            "0 as HTRETUR,  " +
                            "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='Adjust-in') *(Luas/LuasUtuh) as TAdjust,  " +
                            "0 as HTAdjust,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='kirim') *(Luas/LuasUtuh) as KKirim,  " +
                            "0 as HKKirim,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                            "and (process  like '%simetris%' ) and Keterangan like '%-p-%' " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) *(Luas/LuasUtuh) as KBP,  " +
                            "0 as HKBP,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                            "and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%') " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) *(Luas/LuasUtuh) as KBJ,  " +
                            "0 as HKBJ,  " +
                            "(select isnull(SUM(Qty),0) * -1 from vw_KartuStockBJ where qty <0 and  ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY  " +
                            "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) *(Luas/LuasUtuh) as KSample,  " +
                            "0 as HKSample,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY  and Keterangan like '%-s-%' " +
                            "and (process  like '%simetris%' )  and LokID not In (select ID from FC_Lokasi where lokasi='q99')) *(Luas/LuasUtuh) as KBS,  " +
                            "0 as HKBS,  " +
                            "(select isnull(SUM(Qty),0) * -1 from vw_KartuStockBJ where ItemID=A.ItemID and process='Adjust-out' and left(convert(varchar,tanggal ,112),6)=A.HMY ) *(Luas/LuasUtuh) as KAdjust,  " +
                            "0 as HKAdjust from (  " +
                            "select  '" + thnbln + "' as HMY,ItemID,Luas,(1.220 * 2.440) as LuasUtuh,volume/(Luas/(1.220 * 2.440)) as volume from (select ID as itemid,(Lebar/1000)*(Panjang/1000)as Luas,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume  from fc_items where (partno like '%-3-%' or partno like '%-w-%' or partno like '%-m-%')) as B0) as A  ) as B  " +
                            ") as C where awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
                    }
                    else
                    {
                        strsql = "declare @periode varchar(10) " +
                            "select @periode='" + thnbln + "'" +
                            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew] " +
                            "select * into tempKartuStockBJNew from vw_KartuStockBJNew where left(CONVERT(char, tanggal ,112),6)=@periode and itemid in (select ID from fc_items where (partno like '%-3-%' or partno like '%-w-%' or partno like '%-m-%'))" +
                            "select *,Awal+TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                            "round(isnull((select distinct  isnull(sum(" + strQtyLastMonth + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + thn + "),0) *(Luas/LuasUtuh),0) as Awal from (select *,  " +
                            "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY) *(Luas/LuasUtuh) as TWIP,  " +
                            "0 as HTWIP,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                            "and (process  like '%simetris%' ) and (Keterangan like '%-p-%' or Keterangan like '%-1-%')) *(Luas/LuasUtuh) as TBP,  " +
                            "0 as HTBP,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                            " and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%' or Keterangan like '%-m-%')) *(Luas/LuasUtuh) as TBJ,  " +
                            "0 as HTBJ,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='retur') *(Luas/LuasUtuh) as TRETUR,  " +
                            "0 as HTRETUR,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='Adjust-in') *(Luas/LuasUtuh) as TAdjust,  " +
                            "0 as HTAdjust,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='kirim') *(Luas/LuasUtuh) as KKirim,  " +
                            "0 as HKKirim,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                            "and (process  like '%simetris%' ) and Keterangan like '%-p-%' " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) *(Luas/LuasUtuh) as KBP,  " +
                            "0 as HKBP,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                            "and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%' or Keterangan like '%-m-%') " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) *(Luas/LuasUtuh) as KBJ,  " +
                            "0 as HKBJ,  " +
                            "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where qty <0 and  ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY  " +
                            "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99'))  *(Luas/LuasUtuh) as KSample,  " +
                            "0 as HKSample,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY  and Keterangan like '%-s-%' " +
                            "and (process  like '%simetris%' )  and LokID not In (select ID from FC_Lokasi where lokasi='q99'))*(Luas/LuasUtuh) as KBS,  " +
                            "0 as HKBS,  " +
                            "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out' and left(convert(varchar,tanggal ,112),6)=A.HMY )*(Luas/LuasUtuh) as KAdjust,  " +
                            "0 as HKAdjust from (  " +
                            "select  @periode as HMY,ItemID,Luas,(1.220 * 2.440) as LuasUtuh,volume/(Luas/(1.220 * 2.440)) as volume from (select ID as itemid,(Lebar/1000)*(Panjang/1000)as Luas," +
                            "(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume  from fc_items where (partno like '%-3-%' or partno like '%-w-%' or partno like '%-m-%')) as B0) as A  ) as B  " +
                            ") as C where  awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
                    }

                    alldata = connection.Query<LapMutasiBJ>(strsql).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }

        public static List<LapMutasiBJ> GetLapMutasiBJT1(int thnbln, string strQtyLastMonth, int thn, string periode)
        {
            List<LapMutasiBJ> alldata = new List<LapMutasiBJ>();
            string strField = string.Empty;
            string strsql = "";
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    if (thnbln <= Convert.ToInt32(periode))
                    {
                        strsql = "declare @periode varchar(10) " +
                            "select @periode='" + thnbln + "'" +
                            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJ]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJ] " +
                            "select * into tempKartuStockBJ from vw_KartuStockBJ where left(CONVERT(char, tanggal ,112),6)=@periode " +
                            "select *,Awal+TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                            "isnull((select distinct  isnull(sum(" + strQtyLastMonth + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + thn + "),0) as Awal from (select *,  " +
                            "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJ where process='direct' and ItemID=A.ItemID ) as TWIP,  " +
                            "0 as HTWIP,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJ where  qty>0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and (Keterangan like '%-p-%' or Keterangan like '%-1-%')) as TBP,  " +
                            "0 as HTBP,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJ where qty>0 and ItemID=A.ItemID  " +
                            " and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%')) as TBJ,  " +
                            "0 as HTBJ,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJ where ItemID=A.ItemID and process='retur') as TRETUR,  " +
                            "0 as HTRETUR,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJ where ItemID=A.ItemID and process='Adjust-in') as TAdjust,  " +
                            "0 as HTAdjust,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where ItemID=A.ItemID and process='kirim') as KKirim,  " +
                            "0 as HKKirim,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where qty <0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and Keterangan like '%-p-%' " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBP,  " +
                            "0 as HKBP,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where qty <0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%') " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBJ,  " +
                            "0 as HKBJ,  " +
                            "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJ where qty <0 and  ItemID=A.ItemID   " +
                            "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                            "0 as HKSample,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where qty <0 and ItemID=A.ItemID   and Keterangan like '%-s-%' " +
                            "and (process  like '%simetris%' )  and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBS,  " +
                            "0 as HKBS,  " +
                            "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJ where ItemID=A.ItemID and process='Adjust-out'  ) as KAdjust,  " +
                            "0 as HKAdjust from (  " +
                            "select @Periode as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume  from fc_items where (partno like '%-3-%' or partno like '%-w-%' or partno like '%-m-%')) as B0) as A  ) as B  " +
                            ") as C where  awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
                    }
                    else
                    {
                        strsql = "declare @periode varchar(10) " +
                            "select @periode='" + thnbln + "'" +
                            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew] " +
                            "select * into tempKartuStockBJNew from vw_KartuStockBJNew where left(CONVERT(char, tanggal ,112),6)=@periode  " +
                            "select *,Awal + TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                            "isnull((select distinct  isnull(sum(" + strQtyLastMonth + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + thn + "),0) as Awal from (select *,  " +
                            "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and ItemID=A.ItemID ) as TWIP,  " +
                            "0 as HTWIP,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and (Keterangan like '%-p-%' or Keterangan like '%-1-%')) as TBP,  " +
                            "0 as HTBP,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID  " +
                            " and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%' or Keterangan like '%-m-%')) as TBJ,  " +
                            "0 as HTBJ,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='retur') as TRETUR,  " +
                            "0 as HTRETUR,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-in') as TAdjust,  " +
                            "0 as HTAdjust,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='kirim') as KKirim,  " +
                            "0 as HKKirim,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and (Keterangan like '%-p-%' or Keterangan like '%-1-%') " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBP,  " +
                            "0 as HKBP,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%' or Keterangan like '%-m-%') " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBJ,  " +
                            "0 as HKBJ,  " +
                            "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where qty <0 and  ItemID=A.ItemID   " +
                            "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                            "0 as HKSample,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID   and Keterangan like '%-s-%' " +
                            "and (process  like '%simetris%' )  and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBS,  " +
                            "0 as HKBS,  " +
                            "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out'  ) as KAdjust,  " +
                            "0 as HKAdjust from (  " +
                            "select @Periode as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume  from fc_items where (partno like '%-1-%' )) as B0) as A  ) as B  " +
                            ") as C where awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
                    }
                    alldata = connection.Query<LapMutasiBJ>(strsql).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }

        public static List<LapMutasiBJ> GetLapMutasiBJT1Harian(int thnbln, string strQtyLastMonth, int thn, string periode, int thnbln0)
        {
            List<LapMutasiBJ> alldata = new List<LapMutasiBJ>();
            string strField = string.Empty;
            string strsql = "";
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    if (thnbln <= Convert.ToInt32(periode))
                    {
                        strsql = "select *,Awal+TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                            "isnull((select distinct  isnull(sum(" + strQtyLastMonth + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + thn + ") +" +
                            "(select isnull(SUM(Qty),0) from vw_KartuStockBJNew where  ItemID=B.ItemID " +
                            "and convert(varchar,tanggal ,112)>= (SUBSTRING(B.HMY1,1,6)+'01') and convert(varchar,tanggal ,112)<B.HMY1),0) as Awal from (select *,  " +
                            "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                            "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where process='direct' and ItemID=A.ItemID and  convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 ) as TWIP,  " +
                            "0 as HTWIP,  " +
                            "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where  qty>0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2  " +
                            "and (process  like '%simetris%' ) and (Keterangan like '%-p-%' or Keterangan like '%-1-%')) as TBP,  " +
                            "0 as HTBP,  " +
                            "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where qty>0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                            " and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%')) as TBJ,  " +
                            "0 as HTBJ,  " +
                            "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 and process='retur') as TRETUR,  " +
                            "0 as HTRETUR,  " +
                            "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 and process='Adjust-in') as TAdjust,  " +
                            "0 as HTAdjust,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 and process='kirim') as KKirim,  " +
                            "0 as HKKirim,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where qty <0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                            "and (process  like '%simetris%' ) and Keterangan like '%-p-%' " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBP,  " +
                            "0 as HKBP,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where qty <0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                            "and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%') " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBJ,  " +
                            "0 as HKBJ,  " +
                            "(select isnull(SUM(Qty),0) * -1 from vw_KartuStockBJ where qty <0 and  ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                            "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                            "0 as HKSample,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where qty <0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 and Keterangan like '%-s-%' " +
                            "and (process  like '%simetris%' )  and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBS,  " +
                            "0 as HKBS,  " +
                            "(select isnull(SUM(Qty),0) * -1 from vw_KartuStockBJ where ItemID=A.ItemID and process='Adjust-out' and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 ) as KAdjust,  " +
                            "0 as HKAdjust from (  " +
                            "select  '" + thnbln + "' as HMY1,'" + thnbln0 + "' as HMY2,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume  from fc_items where (partno like '%-1-%' )) as B0) as A  ) as B  " +
                            ") as C where awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
                    }
                    else
                    {
                        strsql = "declare @periode1 varchar(10) " +
                            "declare @periode2 varchar(10) " +
                            "select @periode1='" + thnbln + "' + '01' " +
                            "select @periode2='" + thnbln + "' " +
                            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew] " +
                            "select * into tempKartuStockBJNew from vw_KartuStockBJNew where CONVERT(char, tanggal ,112)>=@periode1 and CONVERT(char, tanggal ,112)<=@periode2 and ItemID in (select ID from FC_Items where PartNo like '%-3-%' or partno like '%-w-%' or partno like '%-m-%' OR PartNo  like '%-M-%') " +
                            "select *,Awal+TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                            "isnull((select distinct  isnull(sum(" + strQtyLastMonth + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + thn + ") +" +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  ItemID=B.ItemID " +
                            "and convert(varchar,tanggal ,112)>= (SUBSTRING(B.HMY1,1,6)+'01') and convert(varchar,tanggal ,112)<B.HMY1),0) as Awal from (select *,  " +
                            "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2) as TWIP,  " +
                            "0 as HTWIP,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                            "and (process  like '%simetris%' ) and (Keterangan like '%-p-%' or Keterangan like '%-1-%')) as TBP,  " +
                            "0 as HTBP,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                            " and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%' or Keterangan like '%-m-%' or Keterangan like '%-m-%')) as TBJ,  " +
                            "0 as HTBJ,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 and process='retur') as TRETUR,  " +
                            "0 as HTRETUR,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 and process='Adjust-in') as TAdjust,  " +
                            "0 as HTAdjust,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 and process='kirim') as KKirim,  " +
                            "0 as HKKirim,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                            "and (process  like '%simetris%' ) and Keterangan like '%-p-%' " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBP,  " +
                            "0 as HKBP,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 " +
                            "and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%' or Keterangan like '%-m-%' or Keterangan like '%-m-%') " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBJ,  " +
                            "0 as HKBJ,  " +
                            "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where qty <0 and  ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2  " +
                            "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                            "0 as HKSample,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2  and Keterangan like '%-s-%' " +
                            "and (process  like '%simetris%' )  and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBS,  " +
                            "0 as HKBS,  " +
                            "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out' and convert(varchar,tanggal ,112)>=A.HMY1 and convert(varchar,tanggal ,112)<=A.HMY2 ) as KAdjust,  " +
                            "0 as HKAdjust from (  " +
                            "select  '" + thnbln + "' + '01' as HMY1,'" + thnbln + "' as HMY2,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume  from fc_items where (partno like '%-1-%' )) as B0) as A  ) as B  " +
                            ") as C where awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
                    }
                    alldata = connection.Query<LapMutasiBJ>(strsql).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }

        public static List<LapMutasiBJ> GetLapMutasiBJT1Konversi(int thnbln, string strQtyLastMonth, int thn, string periode)
        {
            List<LapMutasiBJ> alldata = new List<LapMutasiBJ>();
            string strField = string.Empty;
            string strsql = "";
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    if (thnbln <= Convert.ToInt32(periode))
                    {
                        strsql = "select *,Awal+TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                            "round(isnull((select distinct  isnull(sum(" + strQtyLastMonth + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + thn + "),0) *(Luas/LuasUtuh) ,0) as Awal from (select *,  " +
                            "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                            "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where process='direct' and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY) *(Luas/LuasUtuh) as TWIP,  " +
                            "0 as HTWIP,  " +
                            "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where  qty>0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                            "and (process  like '%simetris%' ) and (Keterangan like '%-p-%' or Keterangan like '%-1-%')) *(Luas/LuasUtuh) as TBP,  " +
                            "0 as HTBP,  " +
                            "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where qty>0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                            " and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%')) *(Luas/LuasUtuh) as TBJ,  " +
                            "0 as HTBJ,  " +
                            "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='retur') *(Luas/LuasUtuh) as TRETUR,  " +
                            "0 as HTRETUR,  " +
                            "(select isnull(SUM(Qty),0) from vw_KartuStockBJ where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='Adjust-in') *(Luas/LuasUtuh) as TAdjust,  " +
                            "0 as HTAdjust,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='kirim') *(Luas/LuasUtuh) as KKirim,  " +
                            "0 as HKKirim,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                            "and (process  like '%simetris%' ) and Keterangan like '%-p-%' " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) *(Luas/LuasUtuh) as KBP,  " +
                            "0 as HKBP,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                            "and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%') " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) *(Luas/LuasUtuh) as KBJ,  " +
                            "0 as HKBJ,  " +
                            "(select isnull(SUM(Qty),0) * -1 from vw_KartuStockBJ where qty <0 and  ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY  " +
                            "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) *(Luas/LuasUtuh) as KSample,  " +
                            "0 as HKSample,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from vw_KartuStockBJ where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY  and Keterangan like '%-s-%' " +
                            "and (process  like '%simetris%' )  and LokID not In (select ID from FC_Lokasi where lokasi='q99')) *(Luas/LuasUtuh) as KBS,  " +
                            "0 as HKBS,  " +
                            "(select isnull(SUM(Qty),0) * -1 from vw_KartuStockBJ where ItemID=A.ItemID and process='Adjust-out' and left(convert(varchar,tanggal ,112),6)=A.HMY ) *(Luas/LuasUtuh) as KAdjust,  " +
                            "0 as HKAdjust from (  " +
                            "select  '" + thnbln + "' as HMY,ItemID,Luas,(1.220 * 2.440) as LuasUtuh,volume/(Luas/(1.220 * 2.440)) as volume from (select ID as itemid,(Lebar/1000)*(Panjang/1000)as Luas,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume  from fc_items where (partno like '%-3-%' or partno like '%-w-%' or partno like '%-m-%')) as B0) as A  ) as B  " +
                            ") as C where awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
                    }
                    else
                    {
                        strsql = "declare @periode varchar(10) " +
                            "select @periode='" + thnbln + "'" +
                            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew] " +
                            "select * into tempKartuStockBJNew from vw_KartuStockBJNew where left(CONVERT(char, tanggal ,112),6)=@periode and itemid in (select ID from fc_items where (partno like '%-3-%' or partno like '%-w-%' or partno like '%-m-%'))" +
                            "select *,Awal+TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                            " round(isnull((select distinct  isnull(sum(" + strQtyLastMonth + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + thn + "),0) *(Luas/LuasUtuh) ,0) as Awal from (select *,  " +
                            "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY) *(Luas/LuasUtuh) as TWIP,  " +
                            "0 as HTWIP,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                            "and (process  like '%simetris%' ) and (Keterangan like '%-p-%' or Keterangan like '%-1-%')) *(Luas/LuasUtuh) as TBP,  " +
                            "0 as HTBP,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                            " and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%' or Keterangan like '%-m-%')) *(Luas/LuasUtuh) as TBJ,  " +
                            "0 as HTBJ,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='retur') *(Luas/LuasUtuh) as TRETUR,  " +
                            "0 as HTRETUR,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='Adjust-in') *(Luas/LuasUtuh) as TAdjust,  " +
                            "0 as HTAdjust,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY and process='kirim') *(Luas/LuasUtuh) as KKirim,  " +
                            "0 as HKKirim,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                            "and (process  like '%simetris%' ) and Keterangan like '%-p-%' " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) *(Luas/LuasUtuh) as KBP,  " +
                            "0 as HKBP,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY " +
                            "and (process  like '%simetris%' ) and (Keterangan like '%-3-%' or Keterangan like '%-w-%'  or Keterangan like '%-m-%') " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99')) *(Luas/LuasUtuh) as KBJ,  " +
                            "0 as HKBJ,  " +
                            "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where qty <0 and  ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY  " +
                            "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99'))  *(Luas/LuasUtuh) as KSample,  " +
                            "0 as HKSample,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID and left(convert(varchar,tanggal ,112),6)=A.HMY  and Keterangan like '%-s-%' " +
                            "and (process  like '%simetris%' )  and LokID not In (select ID from FC_Lokasi where lokasi='q99'))*(Luas/LuasUtuh) as KBS,  " +
                            "0 as HKBS,  " +
                            "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out' and left(convert(varchar,tanggal ,112),6)=A.HMY )*(Luas/LuasUtuh) as KAdjust,  " +
                            "0 as HKAdjust from (  " +
                            "select  @periode as HMY,ItemID,Luas,(1.220 * 2.440) as LuasUtuh,volume/(Luas/(1.220 * 2.440)) as volume from (select ID as itemid,(Lebar/1000)*(Panjang/1000)as Luas,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume  from fc_items where (partno like '%-1-%' )) as B0) as A  ) as B  " +
                            ") as C where  awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
                    }
                    alldata = connection.Query<LapMutasiBJ>(strsql).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }

        public static List<LapMutasiBJ> GetLapMutasiBP(int thnbln, string strQtyLastMonth, int thn, int dept, string periode)
        {
            List<LapMutasiBJ> alldata = new List<LapMutasiBJ>();
            string strField = string.Empty;
            string strsql = "";
            string strdept = "";
            if (dept == 0)
            {
                strdept = "";
            }
            if (dept == 3)
            {
                strdept = " and dept='FINISHING' ";
            }
            if (dept == 6)
            {
                strdept = " and isnull(dept,'-')<>'FINISHING' ";
            }
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    if (thnbln <= Convert.ToInt32(periode))
                    {
                        strsql =
                            "declare @periode varchar(10) " +
                            "select @periode='" + thnbln + "'" +
                            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJ]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJ] " +
                            "select * into tempKartuStockBJ from vw_KartuStockBJ where left(CONVERT(char, tanggal ,112),6)=@periode " +
                            "select *,Awal+TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                            "(select distinct  isnull(sum(" + strQtyLastMonth + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + thn + ") as Awal from (select *,  " +
                            "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJ where process='direct' and ItemID=A.ItemID   ) as TWIP,  " +
                            "0 as HTWIP,  " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJ where  qty>0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' )  and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%-s-%' or substring(Keterangan,1,21) like '%-1-%')) as TBP,  " +
                            "0 as HTBP,  " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJ where qty>0 and ItemID=A.ItemID  " +
                            " and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%')) as TBJ,  " +
                            "0 as HTBJ,  " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJ where ItemID=A.ItemID    and process='retur') as TRETUR,  " +
                            "0 as HTRETUR,  " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJ where ItemID=A.ItemID  and process='Adjust-in') as TAdjust,  " +
                            "0 as HTAdjust,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJ where ItemID=A.ItemID  and process='kirim') as KKirim,  " +
                            "0 as HKKirim,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJ where qty <0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%') " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99')) as KBP,  " +
                            "0 as HKBP,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJ where qty <0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%') " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99')) as KBJ,  " +
                            "0 as HKBJ,  " +
                            "(select isnull(SUM(Qty),0)* -1 from TempKartuStockBJ where qty <0 and  ItemID=A.ItemID   " +
                            "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                            "0 as HKSample,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJ where qty <0 and ItemID=A.ItemID   " +
                            "and (process  like '%simetris%' ) and  substring(Keterangan,1,21) like '%-s-%' and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBS,  " +
                            "0 as HKBS,  " +
                            "(select isnull(SUM(Qty),0) * -1 from TempKartuStockBJ where ItemID=A.ItemID and process='Adjust-out'  ) as KAdjust,  " +
                            "0 as HKAdjust from (  " +
                            "select @Periode as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume   from fc_items where partno like '%-p-%' " + strdept + ") as B0) as A  ) as B  " +
                            ") as C where awal+TWIP+TBP+TBJ+TRETUR+TAdjust+KKirim+KBP+KBJ+KSample+KBS+KAdjust <>0";
                    }
                    else if (thnbln >= 201906)
                    {
                        strsql = "declare @periode varchar(10) " +
                            "select @periode='" + thnbln + "'" +
                            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew] " +
                            "select * into tempKartuStockBJNew from vw_KartuStockBJNew where left(CONVERT(char, tanggal ,112),6)=@periode " +
                            "select *,Awal+TWIP+TBP+TBJ+TBS+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                            "(select distinct  isnull(sum(" + strQtyLastMonth + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + thn + ") as Awal from (select *,  " +
                            "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where process='direct' and (isnull(sfrom,'-')='-' " +
                            "or isnull(sfrom,'-')='bevel' or isnull(sfrom,'-')='straping' or isnull(sfrom,'-')='i99R1' or isnull(sfrom,'-')='i99R2' " +
                            "or isnull(sfrom,'-')='i99R3' or isnull(sfrom,'-')='i99R1' or isnull(sfrom,'-')='Rs' or isnull(sfrom,'-')='RsR1' " +
                            "or isnull(sfrom,'-')='RsR2' or isnull(sfrom,'-')='RsR3') and ItemID=A.ItemID ) + " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' )  and (substring(Keterangan,1,21) like '%-1-%'))as TWIP,  " +
                            "0 as HTWIP,  " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where process='direct' and " +
                            "(isnull(sfrom,'-') like 'bevelr%' or isnull(sfrom,'-') like 'strapingr%') and ItemID=A.ItemID ) + " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' )  and (substring(Keterangan,1,21) like '%-p-%')) as TBP,  " +
                            "0 as HTBP,  " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where qty>0 and ItemID=A.ItemID and (process  like '%simetris%' ) and  " +
                            "(substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%')) as TBJ, " +
                            "0 as HTBJ,  " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where qty>0 and ItemID=A.ItemID  " +
                            " and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-S-%')) as TBS,  " +
                            "0 as HTBS,  " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where ItemID=A.ItemID    and process='retur') as TRETUR,  " +
                            "0 as HTRETUR,  " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where ItemID=A.ItemID  and process='Adjust-in') as TAdjust,  " +
                            "0 as HTAdjust,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJNew where ItemID=A.ItemID  and process='kirim') as KKirim,  " +
                            "0 as HKKirim,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' or process  like '%Supply%') and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%Supply%') " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' )) as KBP,  " +
                            "0 as HKBP,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%') " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' )) as KBJ,  " +
                            "0 as HKBJ,  " +
                            "(select isnull(SUM(Qty),0)* -1 from TempKartuStockBJNew where qty <0 and  ItemID=A.ItemID   " +
                            "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                            "0 as HKSample,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJNew where qty <0 and ItemID=A.ItemID   " +
                            "and (process  like '%simetris%' ) and  substring(Keterangan,1,21) like '%-s-%' and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBS,  " +
                            "0 as HKBS,  " +
                            "(select isnull(SUM(Qty),0) * -1 from TempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out'  ) as KAdjust,  " +
                            "0 as HKAdjust from (  " +
                            "select @Periode as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume   from fc_items where partno like '%-p-%' " + strdept + ") as B0) as A  ) as B  " +
                            ") as C where awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TBS<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
                    }
                    else
                    {
                        strsql = "declare @periode varchar(10) " +
                            "select @periode='" + thnbln + "'" +
                            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew] " +
                            "select * into tempKartuStockBJNew from vw_KartuStockBJNew where left(CONVERT(char, tanggal ,112),6)=@periode " +
                            "select *,Awal+TWIP+TBP+TBJ+TBS+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                            "(select distinct  isnull(sum(" + strQtyLastMonth + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + thn + ") as Awal from (select *,  " +
                            "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where process='direct' and ItemID=A.ItemID ) + " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' )  and (substring(Keterangan,1,21) like '%-1-%'))as TWIP,  " +
                            "0 as HTWIP,  " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' )  and (substring(Keterangan,1,21) like '%-p-%')) as TBP,  " +
                            "0 as HTBP,  " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where qty>0 and ItemID=A.ItemID  " +
                            " and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%')) as TBJ,  " +
                            "0 as HTBJ,  " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where qty>0 and ItemID=A.ItemID  " +
                            " and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-S-%')) as TBS,  " +
                            "0 as HTBS,  " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where ItemID=A.ItemID    and process='retur') as TRETUR,  " +
                            "0 as HTRETUR,  " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where ItemID=A.ItemID  and process='Adjust-in') as TAdjust,  " +
                            "0 as HTAdjust,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJNew where ItemID=A.ItemID  and process='kirim') as KKirim,  " +
                            "0 as HKKirim,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' or process  like '%Supply%') and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%Supply%') " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' )) as KBP,  " +
                            "0 as HKBP,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%') " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99')) as KBJ,  " +
                            "0 as HKBJ,  " +
                            "(select isnull(SUM(Qty),0)* -1 from TempKartuStockBJNew where qty <0 and  ItemID=A.ItemID   " +
                            "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                            "0 as HKSample,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJNew where qty <0 and ItemID=A.ItemID   " +
                            "and (process  like '%simetris%' ) and  substring(Keterangan,1,21) like '%-s-%' and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBS,  " +
                            "0 as HKBS,  " +
                            "(select isnull(SUM(Qty),0) * -1 from TempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out'  ) as KAdjust,  " +
                            "0 as HKAdjust from (  " +
                            "select @Periode as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume   from fc_items where partno like '%-p-%' " + strdept + ") as B0) as A  ) as B  " +
                            ") as C where awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TBS<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
                    }
                    alldata = connection.Query<LapMutasiBJ>(strsql).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }

        public static List<LapMutasiBJ> GetLapMutasiBPHarian(int thnbln, string strQtyLastMonth, int thn, int thnblnhari, int dept, string periode)
        {
            List<LapMutasiBJ> alldata = new List<LapMutasiBJ>();
            string strField = string.Empty;
            string strsql = "";
            string strdept = "";
            if (dept == 0)
            {
                strdept = "";
            }
            if (dept == 3)
            {
                strdept = " and dept='FINISHING' ";
            }
            if (dept == 6)
            {
                strdept = " and isnull(dept,'-')<>'FINISHING' ";
            }
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    if (thnbln <= Convert.ToInt32(periode))
                    {
                        strsql = "declare @periode varchar(10) " +
                            "select @periode='" + thnbln + "'" +
                            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJ]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJ] " +
                            "select * into tempKartuStockBJ from vw_KartuStockBJ where left(CONVERT(char, tanggal ,112),6)=@periode " +
                            "select *,Awal+TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                            "(select distinct  isnull(sum(" + strQtyLastMonth + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + thn + ") as Awal from (select *,  " +
                            "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJ where process='direct' and ItemID=A.ItemID   ) as TWIP,  " +
                            "0 as HTWIP,  " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJ where  qty>0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' )  and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%-s-%' or substring(Keterangan,1,21) like '%-1-%')) as TBP,  " +
                            "0 as HTBP,  " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJ where qty>0 and ItemID=A.ItemID  " +
                            " and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%')) as TBJ,  " +
                            "0 as HTBJ,  " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJ where ItemID=A.ItemID    and process='retur') as TRETUR,  " +
                            "0 as HTRETUR,  " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJ where ItemID=A.ItemID  and process='Adjust-in') as TAdjust,  " +
                            "0 as HTAdjust,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJ where ItemID=A.ItemID  and process='kirim') as KKirim,  " +
                            "0 as HKKirim,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJ where qty <0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%') " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99')) as KBP,  " +
                            "0 as HKBP,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJ where qty <0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%') " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99')) as KBJ,  " +
                            "0 as HKBJ,  " +
                            "(select isnull(SUM(Qty),0)* -1 from TempKartuStockBJ where qty <0 and  ItemID=A.ItemID   " +
                            "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                            "0 as HKSample,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJ where qty <0 and ItemID=A.ItemID   " +
                            "and (process  like '%simetris%' ) and  substring(Keterangan,1,21) like '%-s-%' and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBS,  " +
                            "0 as HKBS,  " +
                            "(select isnull(SUM(Qty),0) * -1 from TempKartuStockBJ where ItemID=A.ItemID and process='Adjust-out'  ) as KAdjust,  " +
                            "0 as HKAdjust from (  " +
                            "select @Periode as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume   from fc_items where partno like '%-p-%' " + strdept + ") as B0) as A  ) as B  " +
                            ") as C where awal+TWIP+TBP+TBJ+TRETUR+TAdjust+KKirim+KBP+KBJ+KSample+KBS+KAdjust <>0";
                    }
                    else if (thnbln >= 201906)
                    {
                        strsql = "declare @periode1 varchar(10) " +
                            "declare @periode2 varchar(10) " +
                            "select @periode1='" + thnbln + "' + '01' " +
                            "select @periode2='" + thnblnhari + "' " +
                            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew] " +
                            "select * into tempKartuStockBJNew from vw_KartuStockBJNew where CONVERT(char, tanggal ,112)>=@periode1 and CONVERT(char, tanggal ,112)<=@periode2 and ItemID in (select ID from FC_Items where PartNo like '%-P-%' ) " +
                            "select *,Awal+TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                            "(select distinct  isnull(sum(" + strQtyLastMonth + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + thn + ") + " +
                            "isnull((select SUM(qty) from vw_KartuStockBJNew where itemid=B.ItemID  and CONVERT(char, tanggal ,112)>= (left(@periode1,6)+'01') " +
                            "and CONVERT(char, tanggal ,112)<@periode1),0) as Awal from (select *,  " +
                            "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where process='direct' and (isnull(sfrom,'-')='-' " +
                            "or isnull(sfrom,'-')='bevel' or isnull(sfrom,'-')='straping' or isnull(sfrom,'-')='i99R1' or isnull(sfrom,'-')='i99R2' " +
                            "or isnull(sfrom,'-')='i99R3' or isnull(sfrom,'-')='i99R1' or isnull(sfrom,'-')='Rs' or isnull(sfrom,'-')='RsR1' " +
                            "or isnull(sfrom,'-')='RsR2' or isnull(sfrom,'-')='RsR3') and ItemID=A.ItemID ) + " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' )  and (substring(Keterangan,1,21) like '%-1-%'))as TWIP,  " +
                            "0 as HTWIP,  " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where process='direct' and " +
                            "(isnull(sfrom,'-') like 'bevelr%' or isnull(sfrom,'-') like 'strapingr%') and ItemID=A.ItemID ) + " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' )  and (substring(Keterangan,1,21) like '%-p-%')) as TBP,  " +
                            "0 as HTBP,  " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where qty>0 and ItemID=A.ItemID  " +
                            " and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%')) as TBJ,  " +
                            "0 as HTBJ,  " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where qty>0 and ItemID=A.ItemID  " +
                            " and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-S-%')) as TBS,  " +
                            "0 as HTBS,  " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where ItemID=A.ItemID and process='retur') as TRETUR,  " +
                            "0 as HTRETUR,  " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where ItemID=A.ItemID  and process='Adjust-in') as TAdjust,  " +
                            "0 as HTAdjust,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJNew where ItemID=A.ItemID  and process='kirim') as KKirim,  " +
                            "0 as HKKirim,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%') " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' )) as KBP,  " +
                            "0 as HKBP,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%') " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' )) as KBJ,  " +
                            "0 as HKBJ,  " +
                            "(select isnull(SUM(Qty),0)* -1 from TempKartuStockBJNew where qty <0 and  ItemID=A.ItemID   " +
                            "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                            "0 as HKSample,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJNew where qty <0 and ItemID=A.ItemID   " +
                            "and (process  like '%simetris%' ) and  substring(Keterangan,1,21) like '%-s-%' and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBS,  " +
                            "0 as HKBS,  " +
                            "(select isnull(SUM(Qty),0) * -1 from TempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out'  ) as KAdjust,  " +
                            "0 as HKAdjust from (  " +
                            "select @Periode1 as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume   from fc_items where partno like '%-p-%' " + strdept + ") as B0) as A  ) as B  " +
                            ") as C where awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
                    }
                    else
                    {
                        strsql = "declare @periode1 varchar(10) " +
                            "declare @periode2 varchar(10) " +
                            "select @periode1='" + thnbln + "' + '01' " +
                            "select @periode2='" + thnblnhari + "' " +
                            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew] " +
                            "select * into tempKartuStockBJNew from vw_KartuStockBJNew where CONVERT(char, tanggal ,112)>=@periode1 and CONVERT(char, tanggal ,112)<=@periode2 and ItemID in (select ID from FC_Items where PartNo like '%-P-%' ) " +
                            "select *,Awal+TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                            "(select distinct  isnull(sum(" + strQtyLastMonth + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + thn + ") + " +
                            "isnull((select SUM(qty) from vw_KartuStockBJNew where itemid=B.ItemID  and CONVERT(char, tanggal ,112)>= (left(@periode1,6)+'01') " +
                            "and CONVERT(char, tanggal ,112)<@periode1),0) as Awal from (select *,  " +
                            "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where process='direct' and ItemID=A.ItemID )+(select isnull(SUM(Qty),0) from TempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' )  and (substring(Keterangan,1,21) like '%-1-%' )) as TWIP,  " +
                            "0 as HTWIP,  " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' )  and (substring(Keterangan,1,21) like '%-p-%' )) as TBP,  " +
                            "0 as HTBP,  " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where qty>0 and ItemID=A.ItemID  " +
                            " and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%')) as TBJ,  " +
                            "0 as HTBJ,  " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where qty>0 and ItemID=A.ItemID  " +
                            " and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-S-%')) as TBS,  " +
                            "0 as HTBS,  " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where ItemID=A.ItemID and process='retur') as TRETUR,  " +
                            "0 as HTRETUR,  " +
                            "(select isnull(SUM(Qty),0) from TempKartuStockBJNew where ItemID=A.ItemID  and process='Adjust-in') as TAdjust,  " +
                            "0 as HTAdjust,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJNew where ItemID=A.ItemID  and process='kirim') as KKirim,  " +
                            "0 as HKKirim,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%') " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' )) as KBP,  " +
                            "0 as HKBP,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%') " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' )) as KBJ,  " +
                            "0 as HKBJ,  " +
                            "(select isnull(SUM(Qty),0)* -1 from TempKartuStockBJNew where qty <0 and  ItemID=A.ItemID   " +
                            "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                            "0 as HKSample,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from TempKartuStockBJNew where qty <0 and ItemID=A.ItemID   " +
                            "and (process  like '%simetris%' ) and  substring(Keterangan,1,21) like '%-s-%' and LokID not In (select ID from FC_Lokasi where lokasi='q99')) as KBS,  " +
                            "0 as HKBS,  " +
                            "(select isnull(SUM(Qty),0) * -1 from TempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out'  ) as KAdjust,  " +
                            "0 as HKAdjust from (  " +
                            "select @Periode1 as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume   from fc_items where partno like '%-p-%' " + strdept + ") as B0) as A  ) as B  " +
                            ") as C where awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
                    }
                    alldata = connection.Query<LapMutasiBJ>(strsql).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }

        public static List<LapMutasiBJ> GetLapMutasiBS(int thnbln, string strQtyLastMonth, int thn, string bs, string flag, string periode)
        {
            List<LapMutasiBJ> alldata = new List<LapMutasiBJ>();
            string strsql = "";
            string query1 = "";
            string query2 = "";
            if (flag == "lama" && bs == "BSAUTO")
            {
                query1 = "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and ItemID=A.ItemID and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TWIP, ";
                query2 = "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or  " +
                    "substring(Keterangan,1,21) like '%-1-%')  and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TBP, ";
            }
            else if (flag == "baru" && bs == "BSAUTO")
            {
                query1 = "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where (process='direct' or process like'%Simetris%' ) and substring(Keterangan,1,21) like '%-1-%' and ItemID=A.ItemID and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TWIP, ";
                query2 = "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%')  and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TBP, ";
            }
            else if (flag == "lama" && bs == "Normal")
            {
                query1 = "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and (isnull(sfrom,'-')='-' or isnull(sfrom,'bevel')='-' or isnull(sfrom,'-')='straping') " +
                    "and ItemID=A.ItemID and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TWIP, ";

                query2 = "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and (isnull(sfrom,'-')like 'bevelr%' or isnull(sfrom,'-') like 'strapingr%') " +
                    "and ItemID=A.ItemID and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) + " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or  " +
                    "substring(Keterangan,1,21) like '%-1-%')  and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TBP, ";
            }
            else if (flag == "baru" && bs == "Normal")
            {
                query1 = "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where (process='direct' or process like'%Simetris%') and (isnull(sfrom,'-')='-' or isnull(sfrom,'bevel')='-' or isnull(sfrom,'-')='straping') " +
                    "and ItemID=A.ItemID and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO') and substring(Keterangan,1,21) like '%-1-%') as TWIP, ";

                query2 = "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and (isnull(sfrom,'-')like 'bevelr%' or isnull(sfrom,'-') like 'strapingr%') " +
                    "and ItemID=A.ItemID and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) + " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%'   " +
                    " )  and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TBP, ";
            }

            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    if (thnbln <= Convert.ToInt32(periode))
                    {
                        strsql = "declare @periode varchar(10) " +
                            "select @periode='" + thnbln + "'" +
                            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJ]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJ] " +
                            "select * into tempKartuStockBJ from vw_KartuStockBJ where left(CONVERT(char, tanggal ,112),6)=@periode " +
                            "select *,Awal+TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                            "isnull((select distinct  isnull(sum(" + strQtyLastMonth + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + thn + "),0) as Awal from (select *,  " +
                            "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                            "0 as HTWIP,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJ where  qty>0 and ItemID=A.ItemID " +
                            "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%-s-%' or substring(Keterangan,1,21) like '%-1-%')) as TBP,  " +
                            "0 as HTBP,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJ where qty>0 and ItemID=A.ItemID " +
                            " and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%')) as TBJ,  " +
                            "0 as HTBJ,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJ where ItemID=A.ItemID and process='retur') as TRETUR,  " +
                            "0 as HTRETUR,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJ where ItemID=A.ItemID and process='Adjust-in') as TAdjust,  " +
                            "0 as HTAdjust,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where ItemID=A.ItemID and process='kirim') as KKirim,  " +
                            "0 as HKKirim,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where qty <0 and ItemID=A.ItemID " +
                            "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%-s-%') " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99')) as KBP,  " +
                            "0 as HKBP,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where qty <0 and ItemID=A.ItemID " +
                            "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%') " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99')) as KBJ,  " +
                            "0 as HKBJ,  " +
                            "(select isnull(SUM(Qty),0)* -1 from tempKartuStockBJ where qty <0 and  ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                            "0 as HKSample,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where qty <0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and (LokID In (select ID from FC_Lokasi where lokasi='s99' or lokasi='z99'))) as KBS,  " +
                            "0 as HKBS,  " +
                            "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJ where ItemID=A.ItemID and process='Adjust-out' ) as KAdjust,  " +
                            "0 as HKAdjust from (  " +
                            "select @periode as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume from fc_items where (partno like '%-s-%')) as B0) as A  ) as B  " +
                            ") as C where  awal+TWIP+TBP+TBJ+TRETUR+TAdjust+KKirim+KBP+KBJ+KSample+KBS+KAdjust <>0";
                    }
                    else
                    {
                        if (thnbln < 201708)
                        {
                            strsql =
                                "declare @periode varchar(10) " +
                                "select @periode='" + thnbln + "'" +
                                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew] " +
                                "select * into tempKartuStockBJNew from vw_KartuStockBJNew where left(CONVERT(char, tanggal ,112),6)=@periode " +
                                "select *,Awal+TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                                "isnull((select distinct  isnull(sum(" + strQtyLastMonth + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + thn + "),0) as Awal from (select *,  " +
                                "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                                "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and ItemID=A.ItemID) as TWIP,  " +
                                "0 as HTWIP,  " +
                                "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID " +
                                "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%-1-%')) as TBP,  " +
                                "0 as HTBP,  " +
                                "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID " +
                                "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-s-%')) as TBS,  " +
                                "0 as HTBS,  " +
                                "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID " +
                                " and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%')) as TBJ,  " +
                                "0 as HTBJ,  " +
                                "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='retur') as TRETUR,  " +
                                "0 as HTRETUR,  " +
                                "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-in') as TAdjust,  " +
                                "0 as HTAdjust,  " +
                                "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='kirim') as KKirim,  " +
                                "0 as HKKirim,  " +
                                "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID " +
                                "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%-s-%') " +
                                "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99')) as KBP,  " +
                                "0 as HKBP,  " +
                                "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID " +
                                "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%') " +
                                "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99')) as KBJ,  " +
                                "0 as HKBJ,  " +
                                "(select isnull(SUM(Qty),0)* -1 from tempKartuStockBJNew where qty <0 and  ItemID=A.ItemID  " +
                                "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                                "0 as HKSample,  " +
                                "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                                "and (process  like '%simetris%' ) and (LokID In (select ID from FC_Lokasi where lokasi='s99' or lokasi='z99'))) as KBS,  " +
                                "0 as HKBS,  " +
                                "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out' ) as KAdjust,  " +
                                "0 as HKAdjust from (  " +
                                "select @periode as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume from fc_items where (partno like '%-s-%')) as B0) as A  ) as B  " +
                                ") as C where  awal<>0 or TWIP<>0 or TBP<>0 or TBS<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
                        }
                        else
                        {
                            if (bs == "BSAUTO")
                                strsql =
                                    "declare @periode varchar(10)  " +
                                    "select @periode='" + thnbln + "'" +
                                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempitembsauto]') AND type in (N'U')) DROP TABLE [dbo].[tempitembsauto]  " +
                                    "select distinct itemid into tempitembsauto from vw_KartuStockBJNew where itemid in (select ID from fc_items where partno like '%-s-%') and LokID in (select ID from FC_Lokasi where Lokasi='bsauto') " +
                                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew]  " +
                                    "select * into tempKartuStockBJNew from vw_KartuStockBJNew where left(CONVERT(char, tanggal ,112),6)=@periode and itemID in  (select itemid from tempitembsauto) " +
                                    "select *,Awal+TWIP+TBP+TBS+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total  " +
                                    "from (select *,  " +
                                    "isnull((select distinct  isnull(sum( " + strQtyLastMonth + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + thn + "),0) as Awal  " +
                                    "from (select *,   " +
                                    "(select partno from FC_Items where ID=A.ItemID ) as PartNo,  " +
                                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and ItemID=A.ItemID and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TWIP, 0 as HTWIP,   " +
                                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or  " +
                                    "substring(Keterangan,1,21) like '%-1-%')  and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TBP,  0 as HTBP,   " +
                                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                                    "and (process  like '%simetris%' ) and ( substring(Keterangan,1,21) like '%-s-%' " +
                                    ")  and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TBS,  0 as HTBS,   " +
                                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID  " +
                                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%') " +
                                    " and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TBJ,0 as HTBJ,   " +
                                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='retur'  and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TRETUR, 0 as HTRETUR,   " +
                                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-in'  and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TAdjust, 0 as HTAdjust,   " +
                                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='kirim' and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KKirim, 0 as HKKirim,   " +
                                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%-s-%')  " +
                                    "and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO') ) as KBP,  0 as HKBP,   " +
                                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%')  " +
                                    " and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KBJ,  0 as HKBJ,   " +
                                    "(select isnull(SUM(Qty),0)* -1 from tempKartuStockBJNew where qty <0 and  ItemID=A.ItemID   " +
                                    "and (process  like '%simetris%' )  and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KSample,  0 as HKSample,   " +
                                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID   " +
                                    "and (process  like '%simetris%' ) and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KBS,  0 as HKBS,   " +
                                    "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out'  " +
                                    "and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KAdjust, 0 as HKAdjust  " +
                                    "from (   " +
                                    "select @periode as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume from fc_items  " +
                                    "where ID  in  (select itemid from tempitembsauto) ) as B0) as A  ) as B   " +
                                    ") as C where  awal<>0 or TWIP<>0 or TBP<>0 or TBS<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0 " +
                                    " " +
                                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempitembsauto]') AND type in (N'U')) DROP TABLE [dbo].[tempitembsauto]  " +
                                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew]  ";
                            else
                                strsql =
                                    "declare @periode varchar(10)  " +
                                    "select @periode='" + thnbln + "'" +
                                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempitembsauto]') AND type in (N'U')) DROP TABLE [dbo].[tempitembsauto]  " +
                                    "select distinct itemid into tempitembsauto from vw_KartuStockBJNew where itemid in (select ID from fc_items where partno like '%-s-%') and LokID in (select ID from FC_Lokasi where Lokasi='bsauto') " +
                                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew]  " +
                                    "select * into tempKartuStockBJNew from vw_KartuStockBJNew where left(CONVERT(char, tanggal ,112),6)=@periode and itemID not in  (select itemid from tempitembsauto) " +
                                    "select *,Awal+TWIP+TBP+TBS+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total  " +
                                    "from (select *,  " +
                                    "isnull((select distinct  isnull(sum( " + strQtyLastMonth + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + thn + "),0) as Awal  " +
                                    "from (select *,   " +
                                    "(select partno from FC_Items where ID=A.ItemID ) as PartNo,  " +
                                    " 0 as HTWIP,   " +
                                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or  " +
                                    "substring(Keterangan,1,21) like '%-1-%')  and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TBP,  " +
                                    "0 as HTBP, " +
                                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                                    "and (process  like '%simetris%' ) and ( substring(Keterangan,1,21) like '%-s-%'  " +
                                    ")  and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TBS,  0 as HTBS, " +
                                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID  " +
                                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%') " +
                                    " and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TBJ,0 as HTBJ,   " +
                                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='retur'  and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TRETUR, 0 as HTRETUR,   " +
                                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-in'  and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TAdjust, 0 as HTAdjust,   " +
                                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='kirim' and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KKirim, 0 as HKKirim,   " +
                                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' )  " +
                                    "and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO') ) as KBP,  0 as HKBP,   " +
                                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%')  " +
                                    " and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KBJ,  0 as HKBJ,   " +
                                    "(select isnull(SUM(Qty),0)* -1 from tempKartuStockBJNew where qty <0 and  ItemID=A.ItemID   " +
                                    "and (process  like '%simetris%' )  and lokid in (select ID from FC_Lokasi where Lokasi='q99')) as KSample,  0 as HKSample,   " +
                                    "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID   " +
                                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-S-%' )  and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KBS,  0 as HKBS,   " +
                                    "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out'  " +
                                    "and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KAdjust, 0 as HKAdjust  " +
                                    "from (   " +
                                    "select @periode as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume from fc_items  " +
                                    "where partno like '%-s-%' and ID  not in  (select itemid from tempitembsauto) ) as B0) as A  ) as B   " +
                                    ") as C where  awal<>0 or TWIP<>0 or TBP<>0 or TBS<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0 " +
                                    " " +
                                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempitembsauto]') AND type in (N'U')) DROP TABLE [dbo].[tempitembsauto]  " +
                                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew]  ";
                        }
                    }
                    if (thnbln >= 201906)
                    {
                        if (bs == "BSAUTO")
                            strsql =
                                       "declare @periode varchar(10)  " +
                                       "select @periode='" + thnbln + "'" +
                                       "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempitembsauto]') AND type in (N'U')) DROP TABLE [dbo].[tempitembsauto]  " +
                                       "select distinct itemid into tempitembsauto from vw_KartuStockBJNew where itemid in (select ID from fc_items where partno like '%-s-%') and LokID in (select ID from FC_Lokasi where Lokasi='bsauto') " +
                                       "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew]  " +
                                       "select * into tempKartuStockBJNew from vw_KartuStockBJNew where left(CONVERT(char, tanggal ,112),6)=@periode and itemID in  (select itemid from tempitembsauto) " +
                                       "select *,TWIP+TBP+TBS+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total  " +
                                       "from (select *,  " +
                                       "isnull((select distinct  isnull(sum( " + strQtyLastMonth + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + thn + "),0) as Awal  " +
                                       "from (select *,   " +
                                       "(select partno from FC_Items where ID=A.ItemID ) as PartNo,  " +
                                       " " + query1 + " " +
                                       " 0 as HTWIP,   " +
                                       " " + query2 + " " +
                                       "  0 as HTBP,   " +
                                        "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                                       "and (process  like '%simetris%' ) and ( substring(Keterangan,1,21) like '%-s-%' " +
                                       ")  and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TBS,  0 as HTBS,   " +
                                       "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID  " +
                                       "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%') " +
                                       " and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TBJ,0 as HTBJ,   " +
                                       "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='retur'  and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TRETUR, 0 as HTRETUR,   " +
                                       "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-in'  and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TAdjust, 0 as HTAdjust,   " +
                                       "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='kirim' and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KKirim, 0 as HKKirim,   " +
                                       "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                                       "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%-s-%')  " +
                                       "and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO') ) as KBP,  0 as HKBP,   " +
                                       "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                                       "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%')  " +
                                       " and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KBJ,  0 as HKBJ,   " +
                                       "(select isnull(SUM(Qty),0)* -1 from tempKartuStockBJNew where qty <0 and  ItemID=A.ItemID   " +
                                       "and (process  like '%simetris%' )  and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KSample,  0 as HKSample,   " +
                                       "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID   " +
                                       "and (process  like '%simetris%' ) and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KBS,  0 as HKBS,   " +
                                       "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out'  " +
                                       "and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KAdjust, 0 as HKAdjust  " +
                                       "from (   " +
                                       "select @periode as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume from fc_items  " +
                                       "where ID  in  (select itemid from tempitembsauto) ) as B0) as A  ) as B   " +
                                       ") as C where  awal<>0 or TWIP<>0 or TBP<>0 or TBS<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0 " +
                                       " " +
                                       "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempitembsauto]') AND type in (N'U')) DROP TABLE [dbo].[tempitembsauto]  " +
                                       "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew]  ";
                        else
                            strsql =
                            "declare @periode varchar(10)  " +
                            "select @periode='" + thnbln + "'" +
                            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempitembsauto]') AND type in (N'U')) DROP TABLE [dbo].[tempitembsauto]  " +
                            "select distinct itemid into tempitembsauto from vw_KartuStockBJNew where itemid in (select ID from fc_items where partno like '%-s-%') and LokID in (select ID from FC_Lokasi where Lokasi='bsauto') " +
                            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew]  " +
                            "select * into tempKartuStockBJNew from vw_KartuStockBJNew where left(CONVERT(char, tanggal ,112),6)=@periode and itemID not in  (select itemid from tempitembsauto) " +
                            "select *,Awal+TWIP++TBP+TBS+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total  " +
                            "from (select *,  " +
                            "isnull((select distinct  isnull(sum( " + strQtyLastMonth + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + thn + "),0) as Awal  " +
                            "from (select *,   " +
                            "(select partno from FC_Items where ID=A.ItemID ) as PartNo,  " +
                            "" + query1 + "" +
                            "0 as HTWIP,   " +
                            "" + query2 + "" +
                            " 0 as HTBP, " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and ( substring(Keterangan,1,21) like '%-s-%'  " +
                            ")  and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TBS,  0 as HTBS, " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%') " +
                            " and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TBJ,0 as HTBJ,   " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='retur'  and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TRETUR, 0 as HTRETUR,   " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-in'  and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TAdjust, 0 as HTAdjust,   " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='kirim' and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KKirim, 0 as HKKirim,   " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' )  " +
                            "and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO') ) as KBP,  0 as HKBP,   " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%')  " +
                            " and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KBJ,  0 as HKBJ,   " +
                            "(select isnull(SUM(Qty),0)* -1 from tempKartuStockBJNew where qty <0 and  ItemID=A.ItemID   " +
                            "and (process  like '%simetris%' )  and lokid in (select ID from FC_Lokasi where Lokasi='q99')) as KSample,  0 as HKSample,   " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID   " +
                            "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-S-%' )  and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KBS,  0 as HKBS,   " +
                            "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out'  " +
                            "and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KAdjust, 0 as HKAdjust  " +
                            "from (   " +
                            "select @periode as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume from fc_items  " +
                            "where partno like '%-s-%' and ID  not in  (select itemid from tempitembsauto) ) as B0) as A  ) as B   " +
                            ") as C where  awal<>0 or TWIP<>0 or TBP<>0 or TBS<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0 " +
                            " " +
                            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempitembsauto]') AND type in (N'U')) DROP TABLE [dbo].[tempitembsauto]  " +
                            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew]  ";
                    }

                    alldata = connection.Query<LapMutasiBJ>(strsql).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }

        public static List<LapMutasiBJ> GetLapMutasiBSHarian(int thnbln, string strQtyLastMonth, int thn, int thnblnhari, string periode, string bs, string flag)
        {
            List<LapMutasiBJ> alldata = new List<LapMutasiBJ>();
            string strsql = "";
            string query1 = "";
            string query2 = "";
            if (flag == "lama" && bs == "BSAUTO")
            {
                query1 = "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and (isnull(sfrom,'-')='-' or isnull(sfrom,'bevel')='-' or isnull(sfrom,'-')='straping') " +
                    "and ItemID=A.ItemID and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TWIP, ";

                query2 = "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and (isnull(sfrom,'-')like 'bevelr%' or isnull(sfrom,'-') like 'strapingr%') " +
                    "and ItemID=A.ItemID and lokid  in (select ID from FC_Lokasi where Lokasi='BSAUTO')) + " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or  " +
                    "substring(Keterangan,1,21) like '%-1-%')  and lokid  in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TBP, ";
            }
            else if (flag == "baru" && bs == "BSAUTO")
            {
                query1 = "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where (process='direct' or process='Simetris') and (isnull(sfrom,'-')='-' or isnull(sfrom,'bevel')='-' or isnull(sfrom,'-')='straping') " +
                    "and ItemID=A.ItemID and lokid in (select ID from FC_Lokasi where Lokasi='BSAUTO') and substring(Keterangan,1,21) like '%-1-%') as TWIP, ";

                query2 = "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and (isnull(sfrom,'-')like 'bevelr%' or isnull(sfrom,'-') like 'strapingr%') " +
                    "and ItemID=A.ItemID and lokid  in (select ID from FC_Lokasi where Lokasi='BSAUTO')) + " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%'   " +
                    " )  and lokid  in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TBP, ";
            }
            else if (flag == "lama" && bs == "Normal")
            {
                query1 = "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and (isnull(sfrom,'-')='-' or isnull(sfrom,'bevel')='-' or isnull(sfrom,'-')='straping') " +
                    "and ItemID=A.ItemID and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TWIP, ";

                query2 = "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and (isnull(sfrom,'-')like 'bevelr%' or isnull(sfrom,'-') like 'strapingr%') " +
                    "and ItemID=A.ItemID and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) + " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or  " +
                    "substring(Keterangan,1,21) like '%-1-%')  and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TBP, ";
            }
            else if (flag == "baru" && bs == "Normal")
            {
                query1 = "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where (process='direct' or process='Simetris') and (isnull(sfrom,'-')='-' or isnull(sfrom,'bevel')='-' or isnull(sfrom,'-')='straping') " +
                    "and ItemID=A.ItemID and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO') and substring(Keterangan,1,21) like '%-1-%') as TWIP, ";

                query2 = "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where process='direct' and (isnull(sfrom,'-')like 'bevelr%' or isnull(sfrom,'-') like 'strapingr%') " +
                    "and ItemID=A.ItemID and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) + " +
                    "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID  " +
                    "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' " +
                    " )  and lokid not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as TBP, ";
            }
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    if (thnbln <= Convert.ToInt32(periode))
                    {
                        strsql = "declare @periode varchar(10) " +
                            "select @periode='" + thnbln + "'" +
                            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJ]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJ] " +
                            "select * into tempKartuStockBJ from vw_KartuStockBJ where left(CONVERT(char, tanggal ,112),6)=@periode " +
                            "select *,TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                            "isnull((select distinct  isnull(sum(" + strQtyLastMonth + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + thn + "),0) as Awal from (select *,  " +
                            "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJ where process='direct' and ItemID=A.ItemID) as TWIP,  " +
                            "0 as HTWIP,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJ where  qty>0 and ItemID=A.ItemID " +
                            "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%-s-%' or substring(Keterangan,1,21) like '%-1-%')) as TBP,  " +
                            "0 as HTBP,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJ where qty>0 and ItemID=A.ItemID " +
                            " and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%')) as TBJ,  " +
                            "0 as HTBJ,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJ where ItemID=A.ItemID and process='retur') as TRETUR,  " +
                            "0 as HTRETUR,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJ where ItemID=A.ItemID and process='Adjust-in') as TAdjust,  " +
                            "0 as HTAdjust,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where ItemID=A.ItemID and process='kirim') as KKirim,  " +
                            "0 as HKKirim,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where qty <0 and ItemID=A.ItemID " +
                            "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%-s-%') " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99')) as KBP,  " +
                            "0 as HKBP,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where qty <0 and ItemID=A.ItemID " +
                            "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%') " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99')) as KBJ,  " +
                            "0 as HKBJ,  " +
                            "(select isnull(SUM(Qty),0)* -1 from tempKartuStockBJ where qty <0 and  ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                            "0 as HKSample,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where qty <0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and (LokID In (select ID from FC_Lokasi where lokasi='s99' or lokasi='z99'))) as KBS,  " +
                            "0 as HKBS,  " +
                            "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJ where ItemID=A.ItemID and process='Adjust-out' ) as KAdjust,  " +
                            "0 as HKAdjust from (  " +
                            "select @periode as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume from fc_items where (partno like '%-s-%')) as B0) as A  ) as B  " +
                            ") as C where  awal+TWIP+TBP+TBJ+TRETUR+TAdjust+KKirim+KBP+KBJ+KSample+KBS+KAdjust <>0";
                    }
                    else
                    {
                        if (bs == "BSAUTO")
                            strsql =
                                "declare @periode1 varchar(10) " +
                                "declare @periode2 varchar(10) " +
                                "select @periode1='" + thnbln + "' + '01' " +
                                "select @periode2='" + thnblnhari + "' " +
                                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempitembsauto]') AND type in (N'U')) DROP TABLE [dbo].[tempitembsauto]  " +
                                "select distinct itemid into tempitembsauto from vw_KartuStockBJNew where itemid in (select ID from fc_items where partno like '%-s-%') and LokID in (select ID from FC_Lokasi where Lokasi='bsauto') " +
                                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew] " +
                                "select * into tempKartuStockBJNew from vw_KartuStockBJNew where CONVERT(char, tanggal ,112)>=@periode1 and CONVERT(char, tanggal ,112)<=@periode2 and itemID in  (select itemid from tempitembsauto) " +
                                "select *,awal+TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                                "(select distinct  isnull(sum(" + strQtyLastMonth + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + thn + ") + " +
                                "isnull((select SUM(qty) from vw_KartuStockBJNew where itemid=B.ItemID  and CONVERT(char, tanggal ,112)>= (left(@periode1,6)+'01') " +
                                "and CONVERT(char, tanggal ,112)<@periode1),0) as Awal from (select *,  " +
                                "(select partno from FC_Items where ID=A.ItemID ) as PartNo,  " +
                                "" + query1 + "" +
                                "0 as HTWIP,   " +
                                "" + query2 + "" +
                                " 0 as HTBP, " +
                                "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID " +
                                "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-s-%' )) as TBS,  " +
                                "0 as HTBS,  " +
                                "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID " +
                                " and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%')) as TBJ,  " +
                                "0 as HTBJ,  " +
                                "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='retur') as TRETUR,  " +
                                "0 as HTRETUR,  " +
                                "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-in') as TAdjust,  " +
                                "0 as HTAdjust,  " +
                                "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='kirim') as KKirim,  " +
                                "0 as HKKirim,  " +
                                "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID " +
                                "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' ) " +
                                "and LokID  in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KBP,  " +
                                "0 as HKBP,  " +
                                "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID " +
                                "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%') " +
                                "and LokID  in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KBJ,  " +
                                "0 as HKBJ,  " +
                                "(select isnull(SUM(Qty),0)* -1 from tempKartuStockBJNew where qty <0 and  ItemID=A.ItemID  " +
                                "and (process  like '%simetris%' ) and LokID  in (select ID from FC_Lokasi where Lokasi='q99')) as KSample,  " +
                                "0 as HKSample,  " +
                                "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                                "and (process  like '%simetris%' )  and (substring(Keterangan,1,21) like '%-S-%') and (LokID  in (select ID from FC_Lokasi where Lokasi='BSAUTO'))) as KBS,  " +
                                "0 as HKBS,  " +
                                "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out' ) as KAdjust,  " +
                                "0 as HKAdjust from (  " +
                                "select @periode1 as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume from fc_items where ID  in  (select itemid from tempitembsauto)) as B0) as A  ) as B  " +
                                ") as C where  awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0" +
                                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempitembsauto]') AND type in (N'U')) DROP TABLE [dbo].[tempitembsauto]  " +
                                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew]  ";
                        else
                            strsql =
                                "declare @periode1 varchar(10) " +
                                "declare @periode2 varchar(10) " +
                                "select @periode1='" + thnbln + "' + '01' " +
                                "select @periode2='" + thnblnhari + "' " +
                                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempitembsauto]') AND type in (N'U')) DROP TABLE [dbo].[tempitembsauto]  " +
                                "select distinct itemid into tempitembsauto from vw_KartuStockBJNew where itemid in (select ID from fc_items where partno like '%-s-%') and LokID in (select ID from FC_Lokasi where Lokasi='bsauto') " +
                                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew] " +
                                "select * into tempKartuStockBJNew from vw_KartuStockBJNew where CONVERT(char, tanggal ,112)>=@periode1 and CONVERT(char, tanggal ,112)<=@periode2 and itemID not in  (select itemid from tempitembsauto) " +
                                "select *,awal+TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                                "(select distinct  isnull(sum(" + strQtyLastMonth + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + thn + ") + " +
                                "isnull((select SUM(qty) from vw_KartuStockBJNew where itemid=B.ItemID  and CONVERT(char, tanggal ,112)>= (left(@periode1,6)+'01') " +
                                "and CONVERT(char, tanggal ,112)<@periode1),0) as Awal from (select *,  " +
                                "(select partno from FC_Items where ID=A.ItemID ) as PartNo,  " +
                                "" + query1 + "" +
                                " 0 as HTWIP,   " +
                                "" + query2 + "" +
                                " 0 as HTBP, " +
                                "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID " +
                                "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-s-%' )) as TBS,  " +
                                "0 as HTBS,  " +
                                "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID " +
                                " and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%')) as TBJ,  " +
                                "0 as HTBJ,  " +
                                "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='retur') as TRETUR,  " +
                                "0 as HTRETUR,  " +
                                "(select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-in') as TAdjust,  " +
                                "0 as HTAdjust,  " +
                                "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='kirim') as KKirim,  " +
                                "0 as HKKirim,  " +
                                "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID " +
                                "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' ) " +
                                "and LokID not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KBP,  " +
                                "0 as HKBP,  " +
                                "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID " +
                                "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%') " +
                                "and LokID not in (select ID from FC_Lokasi where Lokasi='BSAUTO')) as KBJ,  " +
                                "0 as HKBJ,  " +
                                "(select isnull(SUM(Qty),0)* -1 from tempKartuStockBJNew where qty <0 and  ItemID=A.ItemID  " +
                                "and (process  like '%simetris%' ) and LokID  in (select ID from FC_Lokasi where Lokasi='q99')) as KSample,  " +
                                "0 as HKSample,  " +
                                "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID  " +
                                "and (process  like '%simetris%' )  and (substring(Keterangan,1,21) like '%-S-%') and (LokID not in (select ID from FC_Lokasi where Lokasi='BSAUTO'))) as KBS,  " +
                                "0 as HKBS,  " +
                                "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out' ) as KAdjust,  " +
                                "0 as HKAdjust from (  " +
                                "select @periode1 as HMY,ItemID,volume from (select ID as itemid,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume from fc_items where partno like '%-s-%' and ID not in  (select itemid from tempitembsauto)) as B0) as A  ) as B  " +
                                ") as C where  awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0 " +
                                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempitembsauto]') AND type in (N'U')) DROP TABLE [dbo].[tempitembsauto]  " +
                                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew]  ";
                    }
                    alldata = connection.Query<LapMutasiBJ>(strsql).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }

        public static List<LapMutasiBJ> GetLapMutasiBSKonversi(int thnbln, string strQtyLastMonth, int thn, string periode, string flag)
        {
            List<LapMutasiBJ> alldata = new List<LapMutasiBJ>();
            string strsql = "";
            string query1 = "";
            string query2 = "";

            if (flag == "lama")
            {
                query1 = "round((select isnull(SUM(Qty),0) from tempKartuStockBJ where process='direct' and ItemID=A.ItemID )*(Luas/LuasUtuh),0) as TWIP,  ";

                query2 = "round((select isnull(SUM(Qty),0) from tempKartuStockBJ where  qty>0 and ItemID=A.ItemID and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%-s-%' or substring(Keterangan,1,21) like '%-1-%'))*(Luas/LuasUtuh),0) as TBP,  ";
            }
            else if (flag == "baru")
            {
                query1 = "round((select isnull(SUM(Qty),0) from tempKartuStockBJNew where substring(Keterangan,1,21) like '%-1-%'  and(process='direct' or process='Simetris') and ItemID=A.ItemID )*(Luas/LuasUtuh),0) as TWIP, ";

                query2 = "round((select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%-s-%' ))*(Luas/LuasUtuh),0) as TBP,  ";
            }
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    if (thnbln <= Convert.ToInt32(periode))
                    {
                        strsql = "declare @periode varchar(10) " +
                            "select @periode='" + thnbln + "'" +
                            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJ]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJ] " +
                            "select * into tempKartuStockBJ from vw_KartuStockBJ where left(CONVERT(char, tanggal ,112),6)=@periode " +
                            "select *,Awal+TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                            "(select distinct  isnull(sum(" + strQtyLastMonth + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + thn + ")*(Luas/LuasUtuh) as Awal from (select *,  " +
                            "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJ where process='direct' and ItemID=A.ItemID )*(Luas/LuasUtuh) as TWIP,  " +
                            "0 as HTWIP,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJ where  qty>0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%-s-%' or substring(Keterangan,1,21) like '%-1-%'))*(Luas/LuasUtuh) as TBP,  " +
                            "0 as HTBP,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJ where qty>0 and ItemID=A.ItemID  " +
                            " and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%'))*(Luas/LuasUtuh) as TBJ,  " +
                            "0 as HTBJ,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJ where ItemID=A.ItemID  and process='retur')*(Luas/LuasUtuh) as TRETUR,  " +
                            "0 as HTRETUR,  " +
                            "(select isnull(SUM(Qty),0) from tempKartuStockBJ where ItemID=A.ItemID  and process='Adjust-in')*(Luas/LuasUtuh) as TAdjust,  " +
                            "0 as HTAdjust,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where ItemID=A.ItemID  and process='kirim')*(Luas/LuasUtuh) as KKirim,  " +
                            "0 as HKKirim,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where qty <0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%-s-%') " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99'))*(Luas/LuasUtuh) as KBP,  " +
                            "0 as HKBP,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where qty <0 and ItemID=A.ItemID  " +
                            "and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%') " +
                            "and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99'))*(Luas/LuasUtuh) as KBJ,  " +
                            "0 as HKBJ,  " +
                            "(select isnull(SUM(Qty),0)* -1 from tempKartuStockBJ where qty <0 and  ItemID=A.ItemID   " +
                            "and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')) as KSample,  " +
                            "0 as HKSample,  " +
                            "(select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJ where qty <0 and ItemID=A.ItemID   " +
                            "and (process  like '%simetris%' ) and (LokID In (select ID from FC_Lokasi where lokasi='s99' or lokasi='z99')))*(Luas/LuasUtuh) as KBS,  " +
                            "0 as HKBS,  " +
                            "(select isnull(SUM(Qty),0) * -1 from tempKartuStockBJ where ItemID=A.ItemID and process='Adjust-out'  )*(Luas/LuasUtuh) as KAdjust,  " +
                            "0 as HKAdjust from (  " +
                            "select  @Periode as HMY,ItemID,Luas,(1.220 * 2.440) as LuasUtuh,volume/(Luas/(1.220 * 2.440)) as volume from (select ID as itemid,(Lebar/1000)*(Panjang/1000)as Luas,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume from fc_items where (partno like '%-s-%')) as B0) as A  ) as B  " +
                            ") as C where awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
                    }
                    else
                    {
                        strsql = "declare @periode varchar(10) " +
                            "select @periode='" + thnbln + "'" +
                            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempKartuStockBJNew]') AND type in (N'U')) DROP TABLE [dbo].[tempKartuStockBJNew] " +
                            "select * into tempKartuStockBJNew from vw_KartuStockBJNew where left(CONVERT(char, tanggal ,112),6)=@periode " +
                            "select *,Awal+TWIP+TBP+TBJ+TRETUR+TAdjust-KKirim-KBP-KBJ-KSample-KBS-KAdjust  as Total from (select *, " +
                            "round(isnull((select distinct  isnull(sum(" + strQtyLastMonth + "),0) from SaldoInventoryBJ where  rowstatus>-1 and ItemID=B.ItemID and YearPeriod=" + thn + "),0) *(Luas/LuasUtuh) ,0) as Awal from (select *,  " +
                            "(select partno from FC_Items where ID=A.ItemID ) as PartNo, " +
                            "" + query1 + "" +
                            "0 as HTWIP,  " +
                            "" + query2 + "" +
                            "0 as HTBP,  " +
                            "round((select isnull(SUM(Qty),0) from tempKartuStockBJNew where  qty>0 and ItemID=A.ItemID and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-s-%' )),0) as TBS,  " +
                            "0 as HTBS,  " +
                            "round((select isnull(SUM(Qty),0) from tempKartuStockBJNew where qty>0 and ItemID=A.ItemID and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%'))*(Luas/LuasUtuh),0) as TBJ,  " +
                            "0 as HTBJ,  " +
                            "round((select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID  and process='retur')*(Luas/LuasUtuh),0) as TRETUR,  " +
                            "0 as HTRETUR,  " +
                            "round((select isnull(SUM(Qty),0) from tempKartuStockBJNew where ItemID=A.ItemID  and process='Adjust-in')*(Luas/LuasUtuh),0) as TAdjust,  " +
                            "0 as HTAdjust,  " +
                            "round((select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where ItemID=A.ItemID  and process='kirim')*(Luas/LuasUtuh),0) as KKirim,  " +
                            "0 as HKKirim,  " +
                            "round((select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-p-%' or substring(Keterangan,1,21) like '%-s-%') and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99'))*(Luas/LuasUtuh),0) as KBP,  " +
                            "0 as HKBP,  " +
                            "round((select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID and (process  like '%simetris%' ) and (substring(Keterangan,1,21) like '%-3-%' or substring(Keterangan,1,21) like '%-w-%' or substring(Keterangan,1,21) like '%-m-%') and LokID not In (select ID from FC_Lokasi where lokasi='q99' OR lokasi='s99' or lokasi='z99'))*(Luas/LuasUtuh),0) as KBJ,  " +
                            "0 as HKBJ,  " +
                            "round((select isnull(SUM(Qty),0)* -1 from tempKartuStockBJNew where qty <0 and  ItemID=A.ItemID and (process  like '%simetris%' ) and LokID In (select ID from FC_Lokasi where lokasi='q99')),0) as KSample,  " +
                            "0 as HKSample,  " +
                            "round((select isnull(SUM(Qty),0)  * -1 from tempKartuStockBJNew where qty <0 and ItemID=A.ItemID and (process  like '%simetris%' ) and (LokID In (select ID from FC_Lokasi where lokasi='s99' or lokasi='z99')))*(Luas/LuasUtuh),0) as KBS,  " +
                            "0 as HKBS,  " +
                            "round((select isnull(SUM(Qty),0) * -1 from tempKartuStockBJNew where ItemID=A.ItemID and process='Adjust-out'  )*(Luas/LuasUtuh),0) as KAdjust,  " +
                            "0 as HKAdjust from (  " +
                            "select  @Periode as HMY,ItemID,Luas,(1.220 * 2.440) as LuasUtuh,volume/(Luas/(1.220 * 2.440)) as volume from (select ID as itemid,(Lebar/1000)*(Panjang/1000)as Luas,(Tebal/1000)*(Lebar/1000)*(Panjang/1000)as volume from fc_items where (partno like '%-s-%')) as B0) as A  ) as B  " +
                            ") as C where  awal<>0 or TWIP<>0 or TBP<>0 or TBJ<>0 or TRETUR<>0 or TAdjust<>0 or KKirim<>0 or KBP<>0 or KBJ<>0 or KSample<>0 or KBS<>0 or KAdjust <>0";
                    }
                    alldata = connection.Query<LapMutasiBJ>(strsql).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }

        public static List<LapAdjustment> GetAdjustmentInventori(string fromdate, string todate)
        {
            List<LapAdjustment> alldata = new List<LapAdjustment>();
            string strsql = "";
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    strsql = "SELECT * FROM (SELECT AdjustDate,AdjustNo,a.Keterangan1,ad.ItemTypeID,ad.ItemID, " +
                            "CASE ad.ItemTypeID " +
                            "    WHEN 1 THEN (Select ItemCode From Inventory where Inventory.ID=Ad.ItemID /* AND Aktif=*/)" +
                            "    WHEN 2 THEN (Select ItemCode From Asset where Asset.ID=Ad.ItemID /* AND Aktif=*/)" +
                            "    WHEN 3 THEN (Select ItemCode From Biaya where Biaya.ID=Ad.ItemID /* AND Aktif=*/)" +
                            "END as ItemCode," +
                            "CASE ad.ItemTypeID " +
                            "    WHEN 1 THEN (Select ItemName From Inventory where Inventory.ID=Ad.ItemID /* AND Aktif=*/)" +
                            "    WHEN 2 THEN (Select ItemName From Asset where Asset.ID=Ad.ItemID /* AND Aktif=*/)" +
                            "    WHEN 3 THEN (Select ItemName From Biaya where Biaya.ID=Ad.ItemID /* AND Aktif=*/)" +
                            "END as ItemName, " +
                            "CASE WHEN a.AdjustType='Tambah' THEN ad.quantity else 0 END AdjustIn," +
                            "CASE WHEN a.AdjustType='Kurang' or a.AdjustType='disposal' Then ad.Quantity else 0 END AdjustOut," +
                            "(SELECT UOMCode from UOM where ID=ad.UomID) as Unit," +
                            "Ad.Keterangan, " +
                            "(select top 1 DeptID from Users where UserName=a.CreatedBy and Users.RowStatus >-1) as DeptID," +
                            "a.CreatedBy as Username " +
                            "FROM Adjust as a " +
                            "INNER JOIN AdjustDetail as ad " +
                            "on ad.AdjustID=a.ID " +
                            "WHERE ad.RowStatus >-1) AS W " +
                            "WHERE CONVERT(Varchar,W.AdjustDate,112) BETWEEN '" + fromdate + "' and '" + todate + "' ORDER BY W.AdjustNo,W.AdjustDate";
                    alldata = connection.Query<LapAdjustment>(strsql).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }

        public static List<LapAdjustment> GetAdjustmentT1T3(string fromdate, string todate, int tipe)
        {
            List<LapAdjustment> alldata = new List<LapAdjustment>();
            string strsql = "";
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    if (tipe == 1)
                    {
                        strsql = "select A.AdjustDate,A.AdjustNo,A.NoBA as Keterangan, " +
                            "(Select FC_Items.PartNo from FC_Items where ID=T1.ItemID) as ItemCode, " +
                            "(Select FC_Items.ItemDesc from FC_Items where ID=T1.ItemID) as ItemName, " +
                            " T1.QtyIn as AdjustIn,T1.QtyOut as AdjustOut,'LBR' As Unit " +
                            "from T1_AdjustDetail as T1 " +
                            "left Join T1_Adjust as A " +
                            "on A.ID=t1.AdjustID " +
                            "WHERE CONVERT(VARCHAR,A.AdjustDate,112) between '" + fromdate + "' and '" + todate + "' " +
                            "and A.RowStatus >-1 and T1.Apv >0 and T1.RowStatus>-1 order by A.AdjustDate";
                    }
                    else
                    {
                        strsql = "select A.AdjustDate,A.AdjustNo,A.NoBA as Keterangan, " +
                            "(Select FC_Items.PartNo from FC_Items where ID=T1.ItemID) as ItemCode, " +
                            "(Select FC_Items.ItemDesc from FC_Items where ID=T1.ItemID) as ItemName, " +
                            " T1.QtyIn as AdjustIn,T1.QtyOut as AdjustOut,'LBR' As Unit, " +
                            "A.Keterangan Keterangan1 from T3_AdjustDetail as T1 " +
                            "left Join T3_Adjust as A " +
                            "on A.ID=t1.AdjustID " +
                            "WHERE CONVERT(VARCHAR,A.AdjustDate,112) between '" + fromdate + "' and '" + todate + "' " +
                            " and A.RowStatus >-1 and T1.Apv >0 and T1.RowStatus>-1 order by A.AdjustDate";
                    }
                    alldata = connection.Query<LapAdjustment>(strsql).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }

        public static List<TipeSPP> GetTipeSPP()
        {
            List<TipeSPP> alldata = new List<TipeSPP>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string strsql = "select A.ID,A.CodeID,A.GroupCode,A.GroupDescription,A.ItemTypeID,A.DeptID,B.TypeDescription,C.DeptName,A.RowStatus,A.CreatedBy, " +
                        "A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from GroupsPurchn as A,ItemTypePurchn as B,Dept as C where A.ItemTypeID = B.ID and " +
                        "A.DeptID = C.ID and A.RowStatus >-1 ";
                    alldata = connection.Query<TipeSPP>(strsql).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }

        public static string GetHargaBiaya(string Dari, string Sampai)
        {
            string query = string.Empty;
            query = "SELECT 1 as Tipe,Convert(Char,R.ReceiptDate,112)Tanggal,R.ReceiptNo,RD.ItemID,Po.Price," +
                    "RD.Quantity,((RD.Quantity*PO.Price)/RD.Quantity)AvgPrice, " +
                    "(RD.Quantity*PO.Price)TotalPrice  " +
                    "FROM ReceiptDetail AS RD " +
                    "LEFT JOIN Receipt AS R ON R.ID=RD.ReceiptID " +
                    "LEFT JOIN POPurchnDetail AS PO ON PO.ID=RD.PODetailID " +
                    "LEFT JOIN Biaya AS B ON B.ID=RD.ItemID " +
                    "LEFT JOIN UOM AS U ON U.ID=RD.UomID " +
                    "LEFT JOIN SPPDetail AS S ON S.ID=PO.SppDetailID " +
                    "WHERE RD.RowStatus>-1 AND Rd.ItemID>0 AND RD.ItemTypeID=3 AND RD.GroupID=5" +
                    "AND (CONVERT(Char,R.ReceiptDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "')";
            return query;
        }

        public static string GetHargaReceipt(string Dari, string Sampai, string GroupPurch, string Tahun)
        {
            /**
             * Harga Kertas di rubah pengambilan datanya
             * dari TableHargaBankOut ke Field Price2 di table PurchnDetail
             * Yang isi Price2 di table PurchnDetail adalah accounting
             * Dirubah per 13-05-2016
             **/
            string ItemTypeIDs = "";
            switch (int.Parse(GroupPurch))
            {
                case 4: ItemTypeIDs = "2"; break;
                case 5: ItemTypeIDs = "3"; break;
                default: ItemTypeIDs = "1"; break;
            }
            string periodeTahun = Dari.Substring(0, 6).ToString();
            string isRekap = (Tahun == "A") ? "(" + GetItemID(periodeTahun, GroupPurch) + " )" : "('" + Tahun + "')";
            string strSQL = (int.Parse(GroupPurch) == 5) ? GetHargaBiaya(Dari, Sampai) :
                 " /* Check Curency dan harga Kertas */ " +
                 "SELECT Tipe,CONVERT(CHAR,Tanggal,105) AS Tanggal,ReceiptNo,ItemID, " +
                 "CASE WHEN x.Price >0 THEN  " +
                 "   CASE WHEN  x.crc >1 then  " +
                 "       CASE WHEN x.Flag =2 Then  " +
                 /** untuk data po mulai dari 092016 kur di ambil dari kolom nilai kurs di table POPurchn */
                 "        CASE WHEN LEFT(CONVERT(CHAR,x.POPurchnDate,112),6)>='201609' THEN " +
                 "          ISNULL((x.NilaiKurs * x.Price),0) ELSE " +
                 "       ((select top 1 isnull(kurs,1)kurs from matauangkurs where  muID=x.crc and drTgl =x.Tanggal /*<= x.Tanggal and sdTgl >=x.Tanggal*/ )* x.Price) " +
                 "       END ELSE  " +
                 "           CASE WHEN x.NilaiKurs >1 THEN (x.NilaiKurs * x.Price) ELSE " +
                 "           ((select top 1 isnull(kurs,1)kurs from matauangkurs where  muID=x.crc and drTgl =x.Tanggal /*<= x.Tanggal and sdTgl >=x.Tanggal*/)* x.Price) " +
                 "           END    " +
                 "       END   " +
                 "   ELSE x.Price   " +
                 " END  " +
                 "   ELSE " +
                 "       CASE WHEN x.Crc <=1 THEN " +
                 "       x.HargaSatuan	 " +
                 "       END  " +
                 "   END Price,Quantity,  " +
                 "   /*Average price */ " +
                 "   CASE  WHEN x.Price > 0 THEN   " +
                 "       CASE  WHEN (x.Crc >1 ) THEN  " +
                 "           CASE WHEN x.flag=2 THEN  " +
                 "               CASE WHEN LEFT(CONVERT(CHAR,x.POPurchnDate,112),6)>='201609' THEN " +
                 "                ISNULL((x.NilaiKurs * x.Price),0) ELSE " +
                 "                ((select top 1 isnull(kurs,1)kurs from MataUangKurs where  muID=x.crc and drTgl =x.Tanggal /*<= x.Tanggal and sdTgl >=x.Tanggal*/)*(isnull((x.Price),0))) " +
                 "            END ELSE " +
                 "               CASE WHEN x.NilaiKurs>1 THEN (x.NilaiKurs * x.Price) ELSE " +
                 "               ((select top 1 isnull(kurs,1)kurs from MataUangKurs where  muID=x.crc and drTgl =x.Tanggal /*<= x.Tanggal and sdTgl >=x.Tanggal*/)*(isnull((x.Price),0))) " +
                 "               END " +
                 "           END " +
                 "       ELSE isnull((x.Price),0)  " +
                 "       END  " +
                 "   ELSE " +
                 "       CASE WHEN(x.Crc <=1) THEN " +
                 "       x.HargaSatuan END  " +
                 "   END AvgPrice,   " +
                 "   /*Total Price*/ " +
                 "   CASE  WHEN(x.Crc >1 ) THEN " +
                 "      CASE WHEN x.flag=2 THEN " +
                 /** untuk data po mulai dari 092016 kur di ambil dari kolom nilai kurs di table POPurchn */
                 "        CASE WHEN LEFT(CONVERT(CHAR,x.POPurchnDate,112),6)>='201609' AND x.NilaiKurs >1 THEN " +
                 "          ISNULL((x.NilaiKurs * x.Price*x.Quantity),0) ELSE " +
                 "       ((select top 1 isnull(kurs,1)kurs from MataUangKurs where muID=x.crc and drTgl =x.Tanggal)* (isnull((x.Price*x.Quantity),0)))" +
                 "         END  " +
                 "        ELSE " +
                 "          CASE WHEN x.NilaiKurs>1 THEN (x.NilaiKurs * x.Price*x.Quantity) ELSE " +
                 "          ((select top 1 isnull(kurs,1)kurs from MataUangKurs where  muID=x.crc and drTgl = x.Tanggal)*(isnull((x.Price*x.Quantity),0)))" +
                 "          END " +
                 "      END " +
                 "   ELSE " +
                 "       CASE WHEN x.Price>0 THEN isnull((x.Price*x.Quantity),0)  " +
                 "       ELSE (x.HargaSatuan * x.Quantity)  " +
                 "       END " +
                 "   END TotalPrice FROM(   " +
                 "   SELECT '1' as Tipe, p.ReceiptDate as Tanggal,p.ReceiptNo ,rd.ItemID, Case When pod.Price=0 then pod.Price2 else pod.Price end Price,rd.Quantity,  " +
                 "   pod.crc,pod.flag,pod.NilaiKurs,ISNULL(pod.SubCompanyID,0)SubCompanyID,p.SupplierID,  " +
                 "   (pod.Price*rd.Quantity) as TotalPrice,rd.POID,rd.ID as ReceiptID,pod.Price2 as HargaSatuan,bo.Qty,pod.POPurchnDate " +
                 "   FROM Receipt as p   " +
                 "   LEFT JOIN ReceiptDetail as rd  on rd.ReceiptID=p.ID    " +
                 "   LEFT JOIN vw_PObukanRP as pod on pod.PODetailID=rd.PODetailID " +
                 "   LEFT JOIN TabelHargaBankOut AS bo ON bo.ReceiptDetailID=rd.ID " +
                 "  WHERE  (convert(varchar,p.ReceiptDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "')" +
                 "  AND rd.ItemID IN " +
                  isRekap +
                 "  AND rd.ItemTypeID=" + ItemTypeIDs +
                 "  AND p.Status >-1 AND rd.RowStatus >-1 ) " +
                 "  as x /* sampai di sini check harga nya */";
            return strSQL;
        }

        public static string GetItemID(string BlnTahun, string GroupID)
        {
            return "select distinct ItemID from vw_StockPurchn where YM=" + BlnTahun + " and GroupID=" + GroupID.ToString() + " group by itemid";
        }

        public static List<LapMutasiBB> GetMutasiStock(string Dari, string Sampai, string periodeBlnThn, string periodeTahun, string GroupPurch, string SaldoLaluQty, string SaldoLaluPrice, string NextSaldo)
        {
            #region Chenge Log

            /**
             * Last Update : 10-02-2016
             * 1. Harga Kertas dikembalikan lagi ke TableHargaBankOut base on ReceiptDetailID return HargaSatuan
             * 2. Kode id table hargabankout HO :2 BPAS,100 CPD,6 SBR
             *
             * Last Update : 04-02-2016
             * 1. Perubahan pada konversi matauang
             *      1. Jika vendor luar SuppPurch.flag=2, maka ambil kurs dari kurst tengah BI (JISDOR) base on receipt date
             *      2. Jika vendor dalam negeri tetapi menggunakan matauang asing ambil kurs dar POPurchn.NilaiKurs
             * 2. Perubahan Harga Kertas
             *      1. Untuk Supplier Kertas yang sudah tergabung dalam group Perusahaan Baru
             *         Nilai / Harga di ambil dari Table HargaKertas base on SubCompanyID
             *      2. Untuk Supplier Kertas yang belum tergabung dengan group perusahaan baru masih seperti biasa
             * on publis on 07-02-2016
             * -----------------------------------------------------------------
             * Last Update  :03-02-2015
             * Change log
             * 1. Pengambilan nilai avgprice di table ReceiptDetail dari field avgprice sebelumnya dari Price
             * 2. Penambahan query pada sessi update mengambil nilai avg price dari table saldoinventory bulan sebelumnya jika
             *    ditabel tidak ada saldo awal
             *    new
             *    Add Get itemid from data transaksi
             */

            #endregion Chenge Log

            #region Prepared Data

            string GrpID = GroupPurch;
            string strSQL = string.Empty;
            string ItemTypeIDs = string.Empty;
            int fldBln = Convert.ToInt16(Dari.Substring(4, 2).ToString());
            string frm = Dari.Substring(6, 2).ToString() + "-" + Dari.Substring(4, 2).ToString() + "-" + Dari.Substring(0, 4).ToString();
            switch (int.Parse(GroupPurch))
            {
                case 4: ItemTypeIDs = "2"; break;
                case 5: ItemTypeIDs = "3"; break;
                default: ItemTypeIDs = "1"; break;
            }

            #endregion Prepared Data

            string created = ((Users)HttpContext.Current.Session["Users"]).ID.ToString();
            List<LapMutasiBB> alldata = new List<LapMutasiBB>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    #region Query proses - hapus tabel temporary

                    strSQL = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[z_" + created + "_lapmutasitmp]') AND type in (N'U')) DROP TABLE [dbo].[z_" + created + "_lapmutasitmp] " +
                            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[z_" + created + "_lapmutasitmpx]') AND type in (N'U')) DROP TABLE [dbo].[z_" + created + "_lapmutasitmpx] " +
                            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[z_" + created + "_lapmutasitmpxx]') AND type in (N'U')) DROP TABLE [dbo].[z_" + created + "_lapmutasitmpxx] " +
                            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[z_" + created + "_lapmutasireport]') AND type in (N'U')) DROP TABLE [dbo].[z_" + created + "_lapmutasireport] " +
                            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[z_" + created + "_mutasireport]') AND type in (N'U')) DROP TABLE [dbo].[z_" + created + "_mutasireport] " +
                            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[z_" + created + "_mutasisaldo]') AND type in (N'U')) DROP TABLE [dbo].[z_" + created + "_mutasisaldo] " +
                            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[z_" + created + "_lapsaldoawal]') AND type in (N'U')) DROP TABLE [dbo].[z_" + created + "_lapsaldoawal] " +
                            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[z_" + created + "_mutasireport1]') AND type in (N'U')) DROP TABLE [dbo].[z_" + created + "_mutasireport1] /**/ ";

                    #endregion Query proses - hapus tabel temporary

                    #region Kumpulkan Data dari saldoawal,receipt, pemakain,return, adjustmen

                    strSQL +=
                            "SELECT * INTO z_" + created + "_lapmutasitmp FROM( " +
                            "     (SELECT '0' AS Tipe,'" + frm + "' AS Tanggal,'Saldo Awal' AS DocNo,si.ItemID,ROUND(si." + SaldoLaluPrice + ",4) AS SaldoHS," +
                            "     si." + SaldoLaluQty + " AS SaldoQty,CASE WHEN si." + SaldoLaluPrice +
                            "     !=0  THEN ROUND(si." + SaldoLaluPrice + ",4) ELSE ROUND(si." + SaldoLaluPrice + ",4) END " +
                            "     AvgPrice,(si." + SaldoLaluQty + "* ROUND(si." + SaldoLaluPrice + ",4)) AS TotalPrice " +
                            "     FROM SaldoInventory AS si " +
                            "     WHERE si.YearPeriod='" + periodeTahun + "'" +
                            "     AND si.ItemTypeID='" + ItemTypeIDs + "' AND GroupID=" + GrpID + " AND (si." + SaldoLaluQty +
                            "    !=0 OR si.ItemID IN(" + GetItemID(periodeBlnThn, GrpID) + ")) " +
                            "  ) UNION ALL ( " +
                                  GetHargaReceipt(Dari, Sampai, GrpID, "A") +
                            "   ) UNION ALL ( " +
                            "       SELECT '2' AS Tipe,Convert(VARCHAR,k.PakaiDate,105) AS Tanggal,k.PakaiNo,pk.ItemID,ROUND(pk.AvgPrice,4)AvgPrice,pk.Quantity," +
                            "       ROUND(pk.AvgPrice,4) AS AvgPrice,(pk.Quantity*ROUND(pk.AvgPrice,4)) AS TotalPrice FROM Pakai AS k " +
                            "       LEFT JOIN PakaiDetail AS pk " +
                            "       ON pk.PakaiID=k.ID " +
                            "       WHERE  (cONvert(VARCHAR,k.PakaiDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') " +
                            "       AND pk.ItemID IN(" + GetItemID(periodeBlnThn, GrpID) + " ) AND k.Status >-1 AND pk.RowStatus >-1" +
                            "       AND pk.ItemTypeID=" + ItemTypeIDs +
                            "   ) UNION ALL  ( " +
                            "        SELECT '3' AS Tipe, CONVERT(VARCHAR,rp.ReturDate,105) AS Tanggal,rp.ReturNo,rpd.ItemID,ROUND(rpd.AvgPrice,4)AvgPrice,rpd.Quantity, " +
                            "        ROUND(rpd.AvgPrice,4) AS AvgPrice,(rpd.Quantity*ROUND(rpd.AvgPrice,4)) AS TotalPrice FROM ReturPakai AS rp " +
                            "        LEFT JOIN ReturPakaiDetail AS rpd " +
                            "        ON rpd.ReturID=rp.ID " +
                            "        WHERE  (cONvert(VARCHAR,rp.ReturDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') " +
                            "        AND rpd.ItemID IN(" + GetItemID(periodeBlnThn, GrpID) + " ) AND rp.Status >-1 AND rpd.RowStatus >-1 " +
                            "        AND rpd.ItemTypeID=" + ItemTypeIDs +
                            "   ) UNION ALL ( " +
                            "       SELECT '4' AS Tipe,CONVERT(VARCHAR,a.AdjustDate,105) AS Tanggal, " +
                            "       CASE when a.AdjustType='Tambah' THEN (a.AdjustNo)ELSE a.AdjustNo END NoM  ,ad.ItemID,ROUND(ad.AvgPrice,4) As SaldoHS, " +
                            "       CASE When a.AdjustType='Tambah' THEN ad.Quantity ELSE ad.Quantity END AdjQty,'0' AS AvgPrice, " +
                            "       (ad.Quantity*ROUND(ad.AvgPrice,4)) AS TotalPrice " +
                            "       FROM Adjust AS a " +
                            "       LEFT JOIN AdjustDetail AS ad " +
                            "       ON ad.AdjustID=a.ID " +
                            "       WHERE  (cONvert(VARCHAR,a.AdjustDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') AND a.AdjustType='Tambah'" +
                            "       AND ad.ItemID IN(" + GetItemID(periodeBlnThn, GrpID) + ") AND a.Status >-1 AND ad.RowStatus >-1" +
                            "       AND ad.ItemTypeID=" + ItemTypeIDs +
                            "    )UNION ALL ( " +
                            "       SELECT '5' AS Tipe,CONVERT(VARCHAR,a.AdjustDate,105) AS Tanggal, " +
                            "       CASE when a.AdjustType='Kurang' THEN (a.AdjustNo)ELSE a.AdjustNo END NoM  ,ad.ItemID,ROUND(ad.AvgPrice,4)AvgPrice, " +
                            "       CASE When a.AdjustType='Kurang' THEN ad.Quantity ELSE ad.Quantity END AdjQty,'0' AS AvgPriceK, " +
                            "       (ad.Quantity*ROUND(ad.AvgPrice,4)) AS TotalPriceK " +
                            "       FROM Adjust AS a " +
                            "       LEFT JOIN AdjustDetail AS ad " +
                            "       ON ad.AdjustID=a.ID " +
                            "       WHERE  (cONvert(VARCHAR,a.AdjustDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') AND a.AdjustType='Kurang' " +
                            "       AND ad.ItemID IN(" + GetItemID(periodeBlnThn, GrpID) + ") AND a.Status >-1 AND ad.RowStatus >-1" +
                            "       AND ad.ItemTypeID=" + ItemTypeIDs +
                            "   )UNION ALL ( " +
                            "   SELECT '6' AS Tipe, " +
                            "    CONVERT(VARCHAR,rs.ReturDate,105) as Tanggal,rs.ReceiptNo,rsd.ItemID,rsd.Quantity as AvgPrice,rsd.Quantity, " +
                            "    CAST('0' AS Decimal(20,2)) AS AvgPrice ,CAST('0' AS Decimal(20,2)) AS Totalprice " +
                            "    FROM ReturSupplier AS rs " +
                            "    LEFT JOIN ReturSupplierDetail AS rsd " +
                            "    ON rsd.ReturID=rs.ID " +
                            "    where  (convert(varchar,rs.ReturDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') " +
                            "    AND rsd.ItemID IN(" + GetItemID(periodeBlnThn, GrpID) + ") AND rs.Status >-1  " +
                            "    AND rsd.RowStatus >-1 AND rsd.ItemTypeID=" + ItemTypeIDs + " ) " +
                            ") as K /**/";

                    #endregion Kumpulkan Data dari saldoawal,receipt, pemakain,return, adjustmen

                    #region Susun sesuai kolom

                    strSQL += "/** susun sesuai dengan kolom laporan */ " +
                            "SELECT ID,Tipe,ItemID,Tanggal,DocNo,SaldoAwalQty,HPP,(SaldoAwalQty*HPP) AS SaAmt, " +
                            "    BeliQty,BeliHS,TotalPB as BeliAmt, " +
                            "    AdjustQty,AdjustHS AS AdjustHS,(AdjustQty*HPP) as AdjAmt, " +
                            "    ReturnQty,ReturHS AS ReturHS,(ReturnQty*HPP) as RetAmt, " +
                            "    ProdQty,ProdHS AS ProdHS,(ProdQty*HPP) as ProdAmt, " +
                            "    AdjProdQty,AdjProdHS AS AdjProdHS,(AdjProdQty*HPP) as AdjPAmt,RetSupQty, " +
                            "    RetSupHS,(RetSupQty*RetSupHS) as RetSupAmt, " +
                            "    (SaldoAwalQty+BeliQty+AdjustQty-ProdQty-AdjProdQty+ReturnQty-RetSupQty) as TotalQty, " +
                            "    ((SaldoAwalQty*HPP)+TotalPB+(ReturnQty*ReturHS )+(AdjustQty*AdjustHS)- " +
                            "   (ProdQty*ProdHS)-(AdjProdQty*AdjProdHS)-(RetSupQty*RetSupHs)) as TotalAmt  " +
                            "   INTO z_" + created + "_lapmutasitmpx " +
                            " FROM ( " +
                            " SELECT ROW_NUMBER() OVER(ORDER By Tanggal,Tipe,DocNo) as ID,Tipe,itemID,Tanggal,DocNo, " +
                            "     CASE WHEN Tipe='0' THEN ISNULL(SaldoQty,0)  ELSE 0 END SaldoAwalQty, " +
                            "     CASE WHEN Tipe='0' THEN CAST(ISNULL(SaldoHS,0) AS Decimal(20,2))  ELSE 0 END HPP, " +
                            "     CASE WHEN Tipe='0' THEN ISNULL(TotalPrice,0) ELSE 0 END TotalPa, " +
                            "     CASE WHEN Tipe='1' THEN ISNULL(SaldoQty,0)  ELSE 0 END BeliQty, " +
                            "     CASE WHEN Tipe='1' THEN CAST(ISNULL(SaldoHS,0) AS Decimal(20,2))  ELSE 0 END BeliHS, " +
                            "     CASE WHEN Tipe='1' THEN ISNULL(TotalPrice,0)  ELSE 0 END TotalPB, " +
                            "     CASE WHEN Tipe='2' THEN ISNULL(SaldoQty,0)  ELSE 0 END ProdQty, " +
                            "     CASE WHEN Tipe='2' THEN CAST(ISNULL(SaldoHS,0)  AS Decimal(20,2)) ELSE 0 END ProdHS, " +
                            "     CASE WHEN Tipe='2' THEN ISNULL(TotalPrice,0)  ELSE 0 END TotalPAd, " +
                            "     CASE WHEN Tipe='3' THEN ISNULL(SaldoQty,0)  ELSE 0 END ReturnQty, " +
                            "     CASE WHEN Tipe='3' THEN CAST(ISNULL(SaldoHS,0) AS Decimal(20,2))  ELSE 0 END ReturHS, " +
                            "     CASE WHEN Tipe='3' THEN ISNULL(TotalPrice,0)  ELSE 0 END TotalPP, " +
                            "     CASE WHEN Tipe='4' THEN ISNULL(SaldoQty,0)  ELSE 0 END AdjustQty, " +
                            "     CASE WHEN Tipe='4' THEN CAST(ISNULL(SaldoHS,0) AS Decimal(20,2))  ELSE 0 END AdjustHS, " +
                            "     CASE WHEN Tipe='4' THEN ISNULL(TotalPrice,0)  ELSE 0 END TotalPR, " +
                            "     CASE WHEN Tipe='5' THEN ISNULL(SaldoQty,0)  ELSE 0 END AdjProdQty, " +
                            "     CASE WHEN Tipe='5' THEN CAST(ISNULL(SaldoHS,0) AS Decimal(20,2))  ELSE 0 END AdjProdHS, " +
                            "     CASE WHEN Tipe='5' THEN ISNULL(TotalPrice,0)  ELSE 0 END TotalPAdjP, " +
                            "     CASE WHEN Tipe='6' THEN ISNULL(SaldoQty,0)  ELSE 0 END RetSupQty, " +
                            "     CASE WHEN Tipe='6' THEN CAST(ISNULL(SaldoHS,0) AS Decimal(20,2))  ELSE 0 END RetSupHS, " +
                            "     CASE WHEN Tipe='6' THEN ISNULL(TotalPrice,0)  ELSE 0 END TotalRetSup " +
                            "  FROM z_" + created + "_lapmutasitmp as x /*where ItemID in(Select ID from Inventory where GroupID=" + GrpID + ")*/" +
                            "  ) AS Z ORDER BY z.Tanggal ";

                    #endregion Susun sesuai kolom

                    #region susun data berdasarkan item id dan bentuk id baru

                    strSQL +=
                            "   SELECT ROW_NUMBER() OVER(PARTITION BY itemID ORDER BY ID,Tanggal,Tipe,DocNo) as IDn,* INTO z_" + created + "_lapmutasitmpxx " +
                            "   FROM z_" + created + "_lapmutasitmpx " +
                            "   /**Susun  data tabular */ " +
                            "    SELECT ROW_NUMBER() OVER(Partition BY itemID ORDER BY ID,Tanggal,Tipe,DocNo) AS IDn,ID,Tipe,itemID,Tanggal,DocNo, " +
                            "    BeliQty,BeliHS, BeliAmt,AdjustQty, " +
                            "    CASE WHEN A.ID>1 AND A.AdjustQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM z_" + created + "_lapMutasitmpxx WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END AdjustHS, " +
                            "    ProdQty, " +
                            "    CASE WHEN A.ID>1 AND A.ProdQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM z_" + created + "_lapMutasitmpxx WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END ProdHS, " +
                            "    AdjProdQty, " +
                            "    CASE WHEN A.ID>1 AND A.AdjProdQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM z_" + created + "_lapMutasitmpxx WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END AdjProdHS, " +
                            "    A.ReturnQty, " +
                            "    CASE WHEN A.ID>1 AND A.ReturnQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM z_" + created + "_lapMutasitmpxx WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END ReturnHS, " +
                            "    A.RetSupQty, " +
                            "    CASE WHEN A.ID>1 AND A.RetSupQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM z_" + created + "_lapMutasitmpxx WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END RetSupHS, " +
                            "    CASE WHEN A.ID>1 THEN (SELECT SUM(totalqty) FROM z_" + created + "_lapMutasitmpxx WHERE ID <=A.ID AND ItemID=A.ItemID )ELSE TotalQty END SaldoAwalQty, " +
                            "    CASE WHEN A.ID>1 THEN  " +
                            "       CASE WHEN (SELECT SUM(totalqty)FROM z_" + created + "_lapMutasitmpxx WHERE ID <=A.ID AND ItemID=A.ItemID )>0 THEN " +
                            "        ((SELECT SUM(totalamt) FROM z_" + created + "_lapMutasitmpxx WHERE ID <=A.ID AND ItemID=A.ItemID )/ " +
                            "        (ABS((SELECT SUM(totalqty)FROM z_" + created + "_lapMutasitmpxx WHERE ID <=A.ID AND ItemID=A.ItemID ))))ELSE 0 END ELSE HPP END HS, " +
                            "    CASE WHEN A.ID>1 THEN (SELECT SUM(totalamt) FROM z_" + created + "_lapMutasitmpxx WHERE ID <=A.ID  AND ItemID=A.ItemID)ELSE Totalamt END TotalAmt " +
                            "    INTO z_" + created + "_lapmutasireport " +
                            "    FROM z_" + created + "_lapMutasitmpxx as A " +
                            "    ORDER by itemID,A.Tanggal,A.IDn,A.Tipe,A.DocNo  ";

                    #endregion susun data berdasarkan item id dan bentuk id baru

                    #region Generate Detail Report without saldo akhir

                    strSQL += "  SELECT ROW_NUMBER() OVER(Partition BY itemID ORDER BY Tanggal,Tipe)as ID, l.ItemID,l.Tanggal,l.DocNo,l.BeliQty,l.BeliHS, " +
                            "           l.BeliAmt,l.AdjustQty, " +
                            "           CASE WHEN L.IDn >1 AND l.AdjustQty >0 THEN(SELECT ISNULL(HS,0) FROM z_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END AdjustHS, " +
                            "           CASE WHEN L.IDn >1 AND l.AdjustQty >0 THEN(SELECT ISNULL(HS,0) FROM z_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.AdjustQty ELSE 0 END AdjustAmt, " +
                            "           l.ProdQty, " +
                            "           CASE WHEN L.IDn >1 AND l.ProdQty >0 THEN (SELECT ISNULL(HS,0) FROM z_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END ProdHS, " +
                            "           CASE WHEN L.IDn >1 AND l.ProdQty >0 THEN(SELECT ISNULL(HS,0) FROM z_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.ProdQty ELSE 0 END ProdAmt, " +
                            "           l.AdjProdQty, " +
                            "           CASE WHEN L.IDn >1 AND l.AdjProdQty >0 THEN (SELECT ISNULL(HS,0) FROM z_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END AdjProdHS, " +
                            "           CASE WHEN L.IDn >1 AND l.AdjProdQty >0 THEN(SELECT ISNULL(HS,0) FROM z_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.AdjProdQty ELSE 0 END AdjProdAmt, " +
                            "           l.ReturnQty, " +
                            "           CASE WHEN L.IDn >1 AND l.ReturnQty >0 THEN (SELECT ISNULL(HS,0) FROM z_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END ReturnHS, " +
                            "           CASE WHEN L.IDn >1 AND l.ReturnQty >0 THEN(SELECT ISNULL(HS,0) FROM z_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.ReturnQty ELSE 0 END returnAmt, " +
                            "           l.RetSupQty, " +
                            "           CASE WHEN L.IDn >1 AND l.RetSupQty >0 THEN (SELECT ISNULL(HS,0) FROM z_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END RetSupHS, " +
                            "           CASE WHEN L.IDn >1 AND l.RetSupQty >0 THEN(SELECT ISNULL(HS,0) FROM z_" + created + "_lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.RetSupQty ELSE 0 END RetSupAmt, " +
                            "           l.SaldoAwalQty,l.HS,l.TotalAmt " +
                            "    INTO z_" + created + "_mutasireport " +
                            "  FROM z_" + created + "_lapmutasireport AS L " +
                            "  ORDER BY L.itemID,L.Tipe,L.Tanggal ";

                    #endregion Generate Detail Report without saldo akhir

                    #region update colom amt dan colom hs

                    strSQL += "  select row_number() over(order by itemID) as IDn,itemid into z_" + created + "_mutasireport1 " +
                             "  from z_" + created + "_mutasireport group by itemID order by itemid " +
                             "   declare @i int " +
                             "   declare @b int " +
                             "   declare @hs decimal(20,6) " +
                             "   declare @amt decimal(20,6) " +
                             "   declare @avgp decimal(20,6) " +
                             "   declare @itemTypeID int " +
                             "   declare @c int " +
                             "   declare @itm int " +
                             "   declare @itmID int " +
                             "   set @c=1 " +
                             "   set @itm=(select COUNT(IDn) from z_" + created + "_mutasireport1) " +
                             "   set @itemTypeID=" + GrpID +
                             "   IF @itemTypeID<>9 " +
                             "   BEGIN   " +
                             "   While @c <=@itm " +
                             "   Begin " +
                             "   set @itmID=(select itemID from z_" + created + "_mutasireport1 where IDn=@c) " +
                             "       set @b=1 " +
                             //"       set @avgp=(select top 1 " + SaldoLaluPrice + " from SaldoInventory where ItemID = @itmID and YearPeriod=" + periodeTahun + ") " +
                             "       set @avgp=(SELECT HPP from z_" + created + "_lapmutasitmpx where ItemID=@itmID AND DocNo='Saldo Awal' AND Tipe=0) " +
                             "       IF ISNULL(@avgp,0)=0 OR @avgp=0 " +
                                        "begin " +
                                        "set @avgp=(select top 1 HS from z_" + created + "_mutasireport where itemid=@itmID and HS>0 ) " +
                                        "end " +
                             "       set @i=(select COUNT(id) from z_" + created + "_mutasireport where itemid=@itmID) " +
                             "       While @b<=@i  " +
                             "       Begin " +
                             "       set @hs=CASE WHEN @b >1 THEN (select hs from z_" + created + "_mutasireport where ID=(@b) and itemid=@itmID)  " +
                             "               ELSE CASE WHEN(SELECT hs from z_" + created + "_mutasireport where ID=1 and itemid=@itmID)>0 THEN " +
                             "		         (SELECT hs from z_" + created + "_mutasireport where ID=1 and itemid=@itmID)ELSE @avgp END " +
                             "		         END  " +
                             "       set @amt=CASE WHEN @b >1 THEN (select TotalAmt from z_" + created + "_mutasireport where ID=(@b) and itemid=@itmID)  " +
                             "                ELSE (SELECT TotalAmt from z_" + created + "_mutasireport where ID=1 and itemid=@itmID)  " +
                             "                END  " +
                             "       /** update semua hs */ " +
                             "       update z_" + created + "_mutasireport  " +
                             "       set AdjustHS	=CASE WHEN (SELECT AdjustQty FROM z_" + created + "_mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                             "           ProdHS		=CASE WHEN (SELECT ProdQty FROM z_" + created + "_mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                             "           ReturnHS	=CASE WHEN (SELECT ReturnQty FROM z_" + created + "_mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                             "           AdjProdHS	=CASE WHEN (SELECT AdjProdQty FROM z_" + created + "_mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                             "           RetSupHS	=CASE WHEN (SELECT RetSupQty FROM z_" + created + "_mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                             "           ProdAmt    =(ProdQty*@hs), " +
                             "           AdjustAmt  =(AdjustQty*@hs), " +
                             "           AdjProdAmt =(AdjProdQty*@hs), " +
                             "           returnAmt  =(ReturnQty*@hs), " +
                             "           RetSupAmt  =(RetSupQty*@hs), " +
                             "           totalamt   =ROUND((Beliamt+(adjustQty*@hs)+(returnQty*@hs)-(ProdQty*@hs)-(AdjprodQty*@hs)+(@amt)),4), " +
                             "           hs=case when abs(SaldoAwalQty)>0 then " +
                             "             ROUND(ABS(((beliQty*BeliHs)+(adjustQty*@hs)+(returnQty*@hs)-(ProdQty*@hs)-(AdjprodQty*@hs)+@amt )/SaldoAwalQty),4) else ROUND(@avgp,4) end  " +
                             "              where ID=(@b+1) and itemid=@itmID " +
                             "       set @b=@b+1 " +
                             "       END " +
                             "   set @c=@c+1 " +
                             "   END " +
                             " END ";

                    #endregion update colom amt dan colom hs

                    #region Generate Saldo Awal

                    strSQL += "  SELECT ItemID,SaldoAwalQty,HS,TotalAmt INTO z_" + created + "_lapsaldoawal FROM z_" + created + "_mutasireport as m " +
                             "  WHERE m.DocNo='Saldo Awal'" +

                             "   /** Generate Saldo Akhir */ " +
                             "       SELECT (COUNT(m.itemID)+1)as ID,m.itemID, ' ' as Tanggal,'Total' as DocNo, " +
                             "           /** pembelian */ " +
                             "             (SUM(m.BeliQty)) As BeliQty, " +
                             "             CASE WHEN SUM(m.BeliAmt) > 0 THEN SUM(m.BeliAmt)/SUM(m.BeliQty) ELSE 0 END BeliHS, " +
                             "             (SUM(m.BeliAmt)) As BeliAmt, " +
                             "           /** Ajdut Plust */ " +
                             "             (SUM(m.AdjustQty)) As AdjustQty, " +
                             "             CASE WHEN SUM(m.AdjustAmt) > 0 THEN SUM(m.AdjustAmt)/SUM(m.AdjustQty)ELSE 0 END AdjustHS, " +
                             "             (SUM(m.AdjustAmt)) As AdjustAmt, " +
                             "           /** Pemakaian Produksi */ " +
                             "             (SUM(m.ProdQty)) As ProdQty, " +
                             "             CASE WHEN SUM(m.ProdAmt) > 0 THEN SUM(m.ProdAmt)/SUM(m.ProdQty)ELSE 0 END ProdHS, " +
                             "             (SUM(m.ProdAmt)) As ProdAmt, " +
                             "           /** Adjut minus */ " +
                             "             (SUM(m.AdjProdQty)) As AdjProdQty, " +
                             "             CASE WHEN SUM(m.AdjProdAmt) > 0 THEN SUM(m.AdjProdAmt)/SUM(m.AdjProdQty) ELSE 0 END AdjProdHS, " +
                             "             (SUM(m.AdjProdAmt)) As AdjProdAmt, " +
                             "           /** Return */ " +
                             "             (SUM(m.ReturnQty)) As ReturnQty, " +
                             "             CASE WHEN SUM(m.returnAmt) > 0 THEN SUM(m.returnAmt)/SUM(m.ReturnQty) ELSE 0 END ReturnHS, " +
                             "             (SUM(m.returnAmt)) As returnAmt, " +
                             "            /** Return Supplier */ " +
                             "            (SUM(m.RetSupQty)) As RetSupQty, " +
                             "            CASE WHEN SUM(m.RetSupQty) > 0 THEN SUM(m.RetSupAmt)/ABS(SUM(m.RetSupQty))ELSE 0 END RetSupHS, " +
                             "            (SUM(m.RetSupAmt)) As RetSupAmt, " +
                             "  /** Saldo Akhir */ " +
                             "  (SELECT TOP 1 SaldoAwalQty FROM z_" + created + "_mutASireport AS b WHERE b.itemID=m.itemID ORDER BY b.ID  DESC) As SaldoAwalQty, " +
                             "  (SELECT TOP 1 CAST(ISNULL(HS,0) AS Decimal(20,2)) FROM z_" + created + "_mutASireport AS b WHERE b.itemID=m.itemID ORDER BY b.ID DESC) As HS, " +

                             "  CASE  when (SELECT  TOP  1 SaldoAwalQty FROM  z_" + created + "_mutASireport AS  b WHERE  b.itemID=m.itemID ORDER  BY  b.ID  DESC)!=0 then  " +
                             "  (SELECT  TOP  1 TotalAmt FROM  z_" + created + "_mutASireport AS  b WHERE  b.itemID=m.itemID ORDER  BY  b.ID DESC) " +
                             "  ELSE  0 END  As  TotalAmt " +
                             "  INTO z_" + created + "_mutasisaldo  " +
                             "  FROM z_" + created + "_mutasireport AS m  " +
                             "  GROUP BY m.ItemID /**/ ";

                    #endregion Generate Saldo Awal

                    #region generate report final

                    strSQL += "/*select isnull((SUM(ProdAmt)+SUM(AdjProdAmt)-SUM(AdjustAmt)),0) as Harga from z_" + created + "_mutasisaldo */" +
                            "/** generate report final */" +
                            " SELECT mds.ID,(SELECT dbo.ItemCodeInv(sa.ItemID," + ItemTypeIDs + "))+' - '+(SELECT dbo.ItemNameInv(sa.ItemID," + ItemTypeIDs + ")) as ItemID," +
                            "    sa.SaldoAwalQty,sa.HS as SaldoAwalHS,sa.TotalAmt as SaldoAwalAmt,mds.BeliQty,mds.BeliHS,mds.BeliAmt,mds.AdjustQty, " +
                            "    mds.AdjustHS,mds.AdjustAmt,mds.ProdQty,mds.ProdHS,mds.ProdAmt, " +
                            "    mds.AdjProdQty,mds.AdjProdHS,mds.AdjProdAmt,mds.ReturnQty,mds.ReturnHS,mds.returnAmt, " +
                            "    mds.RetSupQty,mds.RetSupHS,mds.RetSupAmt, " +
                            "    mds.SaldoAwalQty as SaldoAkhirQty,mds.HS As SaldoAkhirHS,mds.TotalAmt As SaldoAkhirAmt " +
                            "    FROM z_" + created + "_mutASisaldo AS mds " +
                            "    INNER JOIN z_" + created + "_lapsaldoawal as sa " +
                            "    ON mds.ItemID=sa.ItemID " +
                            "    WHERE sa.SaldoAwalQty != 0 OR mds.BeliQty > 0 OR mds.AdjustQty >0 OR mds.ProdQty >0 OR mds.AdjProdQty >0 OR mds.ReturnQty >0 " +
                            "    Order By itemid ";

                    #endregion generate report final

                    #region update SaldoAwal Bulan berikut nya

                    strSQL += (((Users)HttpContext.Current.Session["Users"]).UserName == "admin") ? "declare @sqlS nvarchar(max) " +
                             "  set @sqlS ='update A set A." + NextSaldo + "=ROUND(ISNULL(B.HS,0),4)   from SaldoInventory  A inner join z_" + created + "_mutASisaldo B  on A.ItemID=B.itemid " +
                             "  where A.YearPeriod=" + periodeTahun + "' exec sp_executesql @sqlS, N'' " : "";

                    #endregion update SaldoAwal Bulan berikut nya

                    #region Hapus table temporari

                    strSQL += "/** return supplier  no avgprice*/" +
                        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[z_" + created + "_lapmutasitmp]') AND type in (N'U')) DROP TABLE [dbo].[z_" + created + "_lapmutasitmp] " +
                        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[z_" + created + "_lapmutasitmpx]') AND type in (N'U')) DROP TABLE [dbo].[z_" + created + "_lapmutasitmpx] " +
                        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[z_" + created + "_lapmutasitmpxx]') AND type in (N'U')) DROP TABLE [dbo].[z_" + created + "_lapmutasitmpxx] " +
                        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[z_" + created + "_lapmutasireport]') AND type in (N'U')) DROP TABLE [dbo].[z_" + created + "_lapmutasireport] " +
                        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[z_" + created + "_mutasireport]') AND type in (N'U')) DROP TABLE [dbo].[z_" + created + "_mutasireport] " +
                        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[z_" + created + "_mutasireport1]') AND type in (N'U')) DROP TABLE [dbo].[z_" + created + "_mutasireport1] " +
                        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[z_" + created + "_mutasisaldo]') AND type in (N'U')) DROP TABLE [dbo].[z_" + created + "_mutasisaldo] " +
                        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[z_" + created + "_lapsaldoawal]') AND type in (N'U')) DROP TABLE [dbo].[z_" + created + "_lapsaldoawal] ";

                    #endregion Hapus table temporari

                    alldata = connection.Query<LapMutasiBB>(strSQL).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }

        public static List<LapMutasiBB> GetMutasiRepack(string Dari, string Sampai, string periodeTahun, string GroupPurch, string thn, string SaldoLaluQty, string SaldoLaluPrice, string NextSaldo)
        {
            /**
             * Last Update  :17-04-2013
             * Change log
             * 1. Pengambilan nilai avgprice di table Convertan dari field avgprice sebelumnya dari Price
             * 2. Penambahan query pada sessi update mengambil nilai avg price dari table saldoinventory bulan sebelumnya jika
             *    ditabel tidak ada saldo awal
             */

            #region Prepared Data

            string GrpID = GroupPurch;
            string strSQL = string.Empty;
            int fldBln = Convert.ToInt16(Dari.Substring(4, 2).ToString());
            string frm = Dari.Substring(6, 2).ToString() + "-" + Dari.Substring(4, 2).ToString() + "-" + Dari.Substring(0, 4).ToString();
            string periodeBlnThn = Dari.Substring(0, 6).ToString();

            #endregion Prepared Data

            List<LapMutasiBB> alldata = new List<LapMutasiBB>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    #region Query String

                    strSQL = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapmutasitmp]') AND type in (N'U')) DROP TABLE [dbo].[lapmutasitmp] " +
                       " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapmutasitmpx]') AND type in (N'U')) DROP TABLE [dbo].[lapmutasitmpx] " +
                       " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapmutasitmpxx]') AND type in (N'U')) DROP TABLE [dbo].[lapmutasitmpxx] " +
                       " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapmutasireport]') AND type in (N'U')) DROP TABLE [dbo].[lapmutasireport] " +
                       " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mutasireport]') AND type in (N'U')) DROP TABLE [dbo].[mutasireport] " +
                       " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mutasisaldo]') AND type in (N'U')) DROP TABLE [dbo].[mutasisaldo] " +
                       " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapsaldoawal]') AND type in (N'U')) DROP TABLE [dbo].[lapsaldoawal] " +
                       " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mutasireport1]') AND type in (N'U')) DROP TABLE [dbo].[mutasireport1] " +
                       "/** Kumpulkan data dari beberapa tabel dengan UNION ALL all */ " +
                       "SELECT * INTO lapmutASitmp FROM( " +
                       "     (SELECT '0' AS Tipe,'" + frm + "' AS Tanggal,'Saldo Awal' AS DocNo,si.ItemID,si." + SaldoLaluPrice + " AS SaldoHS," +
                       "     si." + SaldoLaluQty + " AS SaldoQty,CASE WHEN si." + SaldoLaluQty + " >0  THEN si." + SaldoLaluPrice + " ELSE 0 END " +
                       "     AvgPrice,(si." + SaldoLaluQty + "*si." + SaldoLaluPrice + ") AS TotalPrice " +
                       "     FROM SaldoInventory AS si " +
                       "     WHERE si.YearPeriod='" + periodeTahun + "' /*AND ) */" +
                       "     AND si.ItemTypeID='1' and (si." + SaldoLaluQty + ">=0 or si.ItemID IN(" + GetItemID(periodeBlnThn, GrpID) + ")) " +
                       "  ) UNION ALL ( " +
                       "     (SELECT '1' AS Tipe, cONvert(VARCHAR,p.CreatedTime,105) AS Tanggal,p.RepackNo ,p.ToItemID as ItemID, " +
                       "     isnull((select top 1 ReceiptDetail.AvgPrice from ReceiptDetail where ItemID=p.FromItemID and ReceiptDetail.AvgPrice>0 order by ID desc),0) as Price ,p.ToQty, " +
                       "     isnull((select top 1 ReceiptDetail.AvgPrice from ReceiptDetail where ItemID=p.FromItemID and ReceiptDetail.AvgPrice>0 order by ID desc),0) AS AvgPrice,  " +
                       "     (p.ToQty * isnull((select top 1 ReceiptDetail.AvgPrice from ReceiptDetail where ItemID=p.FromItemID and ReceiptDetail.AvgPrice>0 order by ID desc),0)) AS TotalPrice  " +
                       "     FROM Convertan AS p  " +
                       "     /*LEFT JOIN ReceiptDetail AS rd  " +
                       "     ON rd.ItemID=p.FromItemID " +
                       "     INNER JOIN Receipt as r " +
                       "     ON r.ID=rd.ReceiptID  */" +
                       "     WHERE  (cONvert(VARCHAR,p.CreatedTime,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') " +
                       "     AND p.ToItemID IN(" + GetItemID(periodeBlnThn, GrpID) + ") AND p.RowStatus >-1 /*AND rd.RowStatus >-1*/ " +
                       "     ) " +
                       "   ) UNION ALL ( " +
                       "       SELECT '2' AS Tipe,cONvert(VARCHAR,k.PakaiDate,105) AS Tanggal,k.PakaiNo,pk.ItemID,pk.AvgPrice,pk.Quantity," +
                       "       (pk.AvgPrice) AS AvgPrice,(pk.Quantity*pk.AvgPrice) AS TotalPrice FROM Pakai AS k " +
                       "       LEFT JOIN PakaiDetail AS pk " +
                       "       ON pk.PakaiID=k.ID " +
                       "       WHERE  (cONvert(VARCHAR,k.PakaiDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') " +
                       "       AND pk.ItemID IN(" + GetItemID(periodeBlnThn, GrpID) + ")  AND k.Status >-1 AND pk.RowStatus >-1" +
                       "   ) UNION ALL  ( " +
                       "        SELECT '3' AS Tipe, CONVERT(VARCHAR,rp.ReturDate,105) AS Tanggal,rp.ReturNo,rpd.ItemID,rpd.AvgPrice,rpd.Quantity, " +
                       "        (rpd.AvgPrice) AS AvgPrice,(rpd.Quantity*rpd.AvgPrice) AS TotalPrice FROM ReturPakai AS rp " +
                       "        LEFT JOIN ReturPakaiDetail AS rpd " +
                       "        ON rpd.ReturID=rp.ID " +
                       "        WHERE  (cONvert(VARCHAR,rp.ReturDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') " +
                       "        AND rpd.ItemID IN(" + GetItemID(periodeBlnThn, GrpID) + ")  AND rp.Status >-1 AND rpd.RowStatus >-1 " +
                       "   ) UNION ALL ( " +
                       "       SELECT '4' AS Tipe,CONVERT(VARCHAR,a.AdjustDate,105) AS Tanggal, " +
                       "       CASE when a.AdjustType='Tambah' THEN (a.AdjustNo)ELSE a.AdjustNo END NoM  ,ad.ItemID,ad.AvgPrice As SaldoHS, " +
                       "       CASE When a.AdjustType='Tambah' THEN ad.Quantity ELSE ad.Quantity END AdjQty,'0' AS AvgPrice, " +
                       "       (ad.Quantity*ad.AvgPrice) AS TotalPrice " +
                       "       FROM Adjust AS a " +
                       "       LEFT JOIN AdjustDetail AS ad " +
                       "       ON ad.AdjustID=a.ID " +
                       "       WHERE  (cONvert(VARCHAR,a.AdjustDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') AND a.AdjustType='Tambah'" +
                       "       AND ad.ItemID IN(" + GetItemID(periodeBlnThn, GrpID) + ")  AND a.Status >-1 AND ad.RowStatus >-1" +
                       "    )UNION ALL ( " +
                       "       SELECT '5' AS Tipe,CONVERT(VARCHAR,a.AdjustDate,105) AS Tanggal, " +
                       "       CASE when a.AdjustType='Kurang' THEN (a.AdjustNo)ELSE a.AdjustNo END NoM  ,ad.ItemID,ad.AvgPrice, " +
                       "       CASE When a.AdjustType='Kurang' THEN ad.Quantity ELSE ad.Quantity END AdjQty,'0' AS AvgPriceK, " +
                       "       (ad.Quantity*ad.AvgPrice) AS TotalPriceK " +
                       "       FROM Adjust AS a " +
                       "       LEFT JOIN AdjustDetail AS ad " +
                       "       ON ad.AdjustID=a.ID " +
                       "       WHERE  (cONvert(VARCHAR,a.AdjustDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') AND a.AdjustType='Kurang' " +
                       "       AND ad.ItemID IN(" + GetItemID(periodeBlnThn, GrpID) + ")  AND a.Status >-1 AND ad.RowStatus >-1" +
                       "   )UNION ALL ( " +
                       "   SELECT '6' AS Tipe, " +
                       "    CONVERT(VARCHAR,rs.ReturDate,105) as Tanggal,rs.ReceiptNo,rsd.ItemID,rsd.Quantity as AvgPrice,rsd.Quantity, " +
                       "    CAST('0' AS Decimal(20,2)) AS AvgPrice ,CAST('0' AS Decimal(20,2)) AS Totalprice " +
                       "    FROM ReturSupplier AS rs " +
                       "    LEFT JOIN ReturSupplierDetail AS rsd " +
                       "    ON rsd.ReturID=rs.ID " +
                       "    where  (convert(varchar,rs.ReturDate,112) BETWEEN '" + Dari + "' AND '" + Sampai + "') " +
                       "    AND rsd.ItemID IN(" + GetItemID(periodeBlnThn, GrpID) + ")  AND rs.Status >-1  " +
                       "    AND rsd.RowStatus >-1 ) " +
                       ") as K " +
                       "/** susun sesuai dengan kolom laporan */ " +
                       "SELECT ID,Tipe,ItemID,Tanggal,DocNo,SaldoAwalQty,HPP,(SaldoAwalQty*HPP) AS SaAmt, " +
                       "    BeliQty,BeliHS,(BeliQty*BeliHS) as BeliAmt, " +
                       "    AdjustQty,AdjustHS AS AdjustHS,(AdjustQty*HPP) as AdjAmt, " +
                       "    ReturnQty,ReturHS AS ReturHS,(ReturnQty*HPP) as RetAmt, " +
                       "    ProdQty,ProdHS AS ProdHS,(ProdQty*HPP) as ProdAmt, " +
                       "    AdjProdQty,AdjProdHS AS AdjProdHS,(AdjProdQty*HPP) as AdjPAmt,RetSupQty, " +
                       "    RetSupHS,(RetSupQty*RetSupHS) as RetSupAmt, " +
                       "    (SaldoAwalQty+BeliQty+AdjustQty-ProdQty-AdjProdQty+ReturnQty-RetSupQty) as TotalQty, " +
                       "    ((SaldoAwalQty*HPP)+(BeliQty*BeliHS)+(ReturnQty*ReturHS )+(AdjustQty*AdjustHS)- " +
                       "   (ProdQty*ProdHS)-(AdjProdQty*AdjProdHS)-(RetSupQty*RetSupHs)) as TotalAmt  " +
                       "   INTO lapmutasitmpx " +
                       " FROM ( " +
                       " SELECT ROW_NUMBER() OVER(ORDER By Tanggal,Tipe,DocNo) as ID,Tipe,itemID,Tanggal,DocNo, " +
                       "     CASE WHEN Tipe='0' THEN ISNULL(SaldoQty,0)  ELSE 0 END SaldoAwalQty, " +
                       "     CASE WHEN Tipe='0' THEN ISNULL(SaldoHS,0)  ELSE 0 END HPP, " +
                       "     CASE WHEN Tipe='0' THEN ISNULL(TotalPrice,0) ELSE 0 END TotalPa, " +
                       "     CASE WHEN Tipe='1' THEN ISNULL(SaldoQty,0)  ELSE 0 END BeliQty, " +
                       "     CASE WHEN Tipe='1' THEN ISNULL(SaldoHS,0)  ELSE 0 END BeliHS, " +
                       "     CASE WHEN Tipe='1' THEN ISNULL(TotalPrice,0)  ELSE 0 END TotalPB, " +
                       "     CASE WHEN Tipe='2' THEN ISNULL(SaldoQty,0)  ELSE 0 END ProdQty, " +
                       "     CASE WHEN Tipe='2' THEN ISNULL(SaldoHS,0)  ELSE 0 END ProdHS, " +
                       "     CASE WHEN Tipe='2' THEN ISNULL(TotalPrice,0)  ELSE 0 END TotalPAd, " +
                       "     CASE WHEN Tipe='3' THEN ISNULL(SaldoQty,0)  ELSE 0 END ReturnQty, " +
                       "     CASE WHEN Tipe='3' THEN ISNULL(SaldoHS,0)  ELSE 0 END ReturHS, " +
                       "     CASE WHEN Tipe='3' THEN ISNULL(TotalPrice,0)  ELSE 0 END TotalPP, " +
                       "     CASE WHEN Tipe='4' THEN ISNULL(SaldoQty,0)  ELSE 0 END AdjustQty, " +
                       "     CASE WHEN Tipe='4' THEN ISNULL(SaldoHS,0)  ELSE 0 END AdjustHS, " +
                       "     CASE WHEN Tipe='4' THEN ISNULL(TotalPrice,0)  ELSE 0 END TotalPR, " +
                       "     CASE WHEN Tipe='5' THEN ISNULL(SaldoQty,0)  ELSE 0 END AdjProdQty, " +
                       "     CASE WHEN Tipe='5' THEN ISNULL(SaldoHS,0)  ELSE 0 END AdjProdHS, " +
                       "     CASE WHEN Tipe='5' THEN ISNULL(TotalPrice,0)  ELSE 0 END TotalPAdjP, " +
                       "     CASE WHEN Tipe='6' THEN ISNULL(SaldoQty,0)  ELSE 0 END RetSupQty, " +
                       "     CASE WHEN Tipe='6' THEN ISNULL(SaldoHS,0)  ELSE 0 END RetSupHS, " +
                       "     CASE WHEN Tipe='6' THEN ISNULL(TotalPrice,0)  ELSE 0 END TotalRetSup " +
                       "  FROM lapmutasitmp as x where ItemID in(Select ID from Inventory where GroupID=" + GrpID + ")) AS Z ORDER BY z.Tanggal " +
                       "   /** susun data berdasarkan item id dan bentuk id baru */ " +
                       "   SELECT ROW_NUMBER() OVER(PARTITION BY itemID ORDER BY itemID,ID,DocNo) as IDn,* INTO lapmutasitmpxx " +
                       "   FROM lapmutasitmpx " +
                       "   /**Susun  data tabular */ " +
                       "    SELECT ROW_NUMBER() OVER(Partition BY itemID ORDER BY ID,Tanggal,Tipe,DocNo) AS IDn,ID,Tipe,itemID,Tanggal,DocNo, " +
                       "    BeliQty,BeliHS,(BeliQty*BeliHS) As BeliAmt,AdjustQty, " +
                       "    CASE WHEN A.ID>1 AND A.AdjustQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM lapMutasitmpxx WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END AdjustHS, " +
                       "    ProdQty, " +
                       "    CASE WHEN A.ID>1 AND A.ProdQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM lapMutasitmpxx WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END ProdHS, " +
                       "    AdjProdQty, " +
                       "    CASE WHEN A.ID>1 AND A.AdjProdQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM lapMutasitmpxx WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END AdjProdHS, " +
                       "    A.ReturnQty, " +
                       "    CASE WHEN A.ID>1 AND A.ReturnQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM lapMutasitmpxx WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END ReturnHS, " +
                       "    A.RetSupQty, " +
                       "    CASE WHEN A.ID>1 AND A.RetSupQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM lapMutasitmpxx WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END RetSupHS, " +
                       "    CASE WHEN A.ID>1 THEN (SELECT SUM(totalqty) FROM lapMutasitmpxx WHERE ID <=A.ID AND ItemID=A.ItemID )ELSE TotalQty END SaldoAwalQty, " +
                       "    CASE WHEN A.ID>1 THEN  " +
                       "       CASE WHEN (SELECT SUM(totalqty)FROM lapMutasitmpxx WHERE ID <=A.ID AND ItemID=A.ItemID )>0 THEN " +
                       "        ((SELECT SUM(totalamt) FROM lapMutasitmpxx WHERE ID <=A.ID AND ItemID=A.ItemID )/ " +
                       "        (ABS((SELECT SUM(totalqty)FROM lapMutasitmpxx WHERE ID <=A.ID AND ItemID=A.ItemID ))))ELSE 0 END ELSE HPP END HS, " +
                       "    CASE WHEN A.ID>1 THEN (SELECT SUM(totalamt) FROM lapMutasitmpxx WHERE ID <=A.ID  AND ItemID=A.ItemID)ELSE Totalamt END TotalAmt " +
                       "    INTO lapmutasireport " +
                       "    FROM lapMutasitmpxx as A " +
                       "    ORDER by itemID,A.Tanggal,a.Tipe,a.DocNo  " +

                       "   /** Generate Detail Report without saldo akhir */ " +
                       "  SELECT ROW_NUMBER() OVER(Partition BY itemID ORDER BY Tanggal,Tipe)as ID, l.ItemID,l.Tanggal,l.DocNo,l.BeliQty,l.BeliHS, " +
                       "           (l.BeliQty*l.BeliHS) as BeliAmt,l.AdjustQty, " +
                       "           CASE WHEN L.IDn >1 AND l.AdjustQty >0 THEN(SELECT HS FROM lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END AdjustHS, " +
                       "           CASE WHEN L.IDn >1 AND l.AdjustQty >0 THEN(SELECT HS FROM lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.AdjustQty ELSE 0 END AdjustAmt, " +
                       "           l.ProdQty, " +
                       "           CASE WHEN L.IDn >1 AND l.ProdQty >0 THEN (SELECT HS FROM lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END ProdHS, " +
                       "           CASE WHEN L.IDn >1 AND l.ProdQty >0 THEN(SELECT HS FROM lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.ProdQty ELSE 0 END ProdAmt, " +
                       "           l.AdjProdQty, " +
                       "           CASE WHEN L.IDn >1 AND l.AdjProdQty >0 THEN (SELECT HS FROM lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END AdjProdHS, " +
                       "           CASE WHEN L.IDn >1 AND l.AdjProdQty >0 THEN(SELECT HS FROM lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.AdjProdQty ELSE 0 END AdjProdAmt, " +
                       "           l.ReturnQty, " +
                       "           CASE WHEN L.IDn >1 AND l.ReturnQty >0 THEN (SELECT HS FROM lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END ReturnHS, " +
                       "           CASE WHEN L.IDn >1 AND l.ReturnQty >0 THEN(SELECT HS FROM lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.ReturnQty ELSE 0 END returnAmt, " +
                       "           l.RetSupQty, " +
                       "           CASE WHEN L.IDn >1 AND l.RetSupQty >0 THEN (SELECT HS FROM lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END RetSupHS, " +
                       "           CASE WHEN L.IDn >1 AND l.RetSupQty >0 THEN(SELECT HS FROM lapmutasireport WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.RetSupQty ELSE 0 END RetSupAmt, " +
                       "           l.SaldoAwalQty,l.HS,l.TotalAmt " +
                       "    INTO mutasireport " +
                       "  FROM lapmutasireport AS L " +
                       "  /*WHERE l.ItemID IN (" + GetItemID(periodeBlnThn, GrpID) + ")*/ ORDER BY L.itemID,L.Tipe,L.Tanggal " +

                       "  /** update colom amt dan colom hs */ " +
                        "  select row_number() over(order by itemID) as IDn,itemid into mutasireport1 " +
                        "  from mutasireport group by itemID order by itemid " +
                        "   declare @i int " +
                        "   declare @b int " +
                        "   declare @hs decimal(20,2) " +
                        "   declare @amt decimal(20,2) " +
                        "   declare @avgp decimal(20,2) " +
                        "   declare @itemTypeID int " +
                        "   declare @c int " +
                        "   declare @itm int " +
                        "   declare @itmID int " +
                        "   set @c=1 " +
                        "   set @itm=(select COUNT(IDn) from mutasireport1) " +
                        "   set @itemTypeID=(select GroupID from Inventory where ID=(select top 1 ItemID from mutasireport))" +
                        "  IF @itemTypeID<>9 " +
                        "   BEGIN   " +
                        "   While @c <=@itm " +
                        "   Begin " +
                        "   set @itmID=(select itemID from mutasireport1 where IDn=@c) " +
                        "       set @b=1 " +
                        "       set @avgp=(select top 1 " + SaldoLaluPrice + " from SaldoInventory where ItemID = @itmID and YearPeriod=" + periodeTahun + ") " +
                        "       set @i=(select COUNT(id) from mutasireport where itemid=@itmID) " +
                        "       While @b<=@i  " +
                        "       Begin " +
                        "       set @hs=CASE WHEN @b >1 THEN (select hs from mutasireport where ID=(@b) and itemid=@itmID)  " +
                        "               ELSE CASE WHEN(SELECT hs from mutasireport where ID=1 and itemid=@itmID)>0 THEN " +
                        "		         (SELECT hs from mutasireport where ID=1 and itemid=@itmID)ELSE @avgp END " +
                        "		         END  " +
                        "       set @amt=CASE WHEN @b >1 THEN (select TotalAmt from mutasireport where ID=(@b) and itemid=@itmID)  " +
                        "                ELSE (SELECT TotalAmt from mutasireport where ID=1 and itemid=@itmID)  " +
                        "                END  " +
                        "       /** update semua hs */ " +
                        "       update mutasireport  " +
                        "       set AdjustHS	=CASE WHEN (SELECT AdjustQty FROM mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                        "           ProdHS		=CASE WHEN (SELECT ProdQty FROM mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                        "           ReturnHS	=CASE WHEN (SELECT ReturnQty FROM mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                        "           AdjProdHS	=CASE WHEN (SELECT AdjProdQty FROM mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                        "           RetSupHS	=CASE WHEN (SELECT RetSupQty FROM mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                        "           ProdAmt    =(ProdQty*@hs), " +
                        "           AdjustAmt  =(AdjustQty*@hs), " +
                        "           AdjProdAmt =(AdjProdQty*@hs), " +
                        "           returnAmt  =(ReturnQty*@hs), " +
                        "           RetSupAmt  =(RetSupQty*@hs), " +
                        "           totalamt   =((BeliQty*BeliHs)+(adjustQty*@hs)+(returnQty*@hs)-(ProdQty*@hs)-(AdjprodQty*@hs)+(@amt)), " +
                        "           hs=case when abs(SaldoAwalQty)>0 then " +
                        "             (((beliQty*BeliHs)+(adjustQty*@hs)+(returnQty*@hs)-(ProdQty*@hs)-(AdjprodQty*@hs)+@amt )/SaldoAwalQty) else @avgp end  " +
                        "              where ID=(@b+1) and itemid=@itmID " +
                        "       set @b=@b+1 " +
                        "       END " +
                        "   set @c=@c+1 " +
                        "   END " +
                        " END " +

                       "  /** Generate Saldo Awal */ " +
                       "  SELECT ItemID,SaldoAwalQty,HS,TotalAmt " +
                       "  INTO lapsaldoawal " +
                       "  FROM mutasireport as m " +
                       "  WHERE m.DocNo='Saldo Awal' /*AND m.ItemID in(" + GetItemID(periodeBlnThn, GrpID) + ")*/ " +

                        "   /** Generate Saldo Akhir */ " +
                        "       SELECT (COUNT(m.itemID)+1)as ID,m.itemID, ' ' as Tanggal,'Total' as DocNo, " +
                        "           /** pembelian */ " +
                        "             (SUM(m.BeliQty)) As BeliQty, " +
                        "             CASE WHEN SUM(m.BeliAmt) > 0 THEN (SUM(m.BeliAmt)/SUM(m.BeliQty))ELSE 0 END BeliHS, " +
                        "             (SUM(m.BeliAmt)) As BeliAmt, " +
                        "           /** Ajdut Plust */ " +
                        "             (SUM(m.AdjustQty)) As AdjustQty, " +
                        "             CASE WHEN SUM(m.AdjustAmt) > 0 THEN (SUM(m.AdjustAmt)/SUM(m.AdjustQty))ELSE 0 END AdjustHS, " +
                        "             (SUM(m.AdjustAmt)) As AdjustAmt, " +
                        "           /** Pemakaian Produksi */ " +
                        "             (SUM(m.ProdQty)) As ProdQty, " +
                        "             CASE WHEN SUM(m.ProdAmt) > 0 THEN (SUM(m.ProdAmt)/SUM(m.ProdQty))ELSE 0 END ProdHS, " +
                        "             (SUM(m.ProdAmt)) As ProdAmt, " +
                        "           /** Adjut minus */ " +
                        "             (SUM(m.AdjProdQty)) As AdjProdQty, " +
                        "             CASE WHEN SUM(m.AdjProdAmt) > 0 THEN (SUM(m.AdjProdAmt)/SUM(m.AdjProdQty))ELSE 0 END AdjProdHS, " +
                        "             (SUM(m.AdjProdAmt)) As AdjProdAmt, " +
                        "           /** Return */ " +
                        "             (SUM(m.ReturnQty)) As ReturnQty, " +
                        "             CASE WHEN SUM(m.returnAmt) > 0 THEN (SUM(m.returnAmt)/SUM(m.ReturnQty))ELSE 0 END ReturnHS, " +
                        "             (SUM(m.returnAmt)) As returnAmt, " +
                        "            /** Return Supplier */ " +
                        "            (SUM(m.RetSupQty)) As RetSupQty, " +
                        "            CASE WHEN SUM(m.RetSupQty) > 0 THEN (SUM(m.RetSupAmt)/ABS(SUM(m.RetSupQty)))ELSE 0 END RetSupHS, " +
                        "            (SUM(m.RetSupAmt)) As RetSupAmt, " +
                        "  /** Saldo Akhir */ " +
                        "  (SELECT TOP 1 SaldoAwalQty FROM mutASireport AS b WHERE b.itemID=m.itemID ORDER BY b.ID  DESC) As SaldoAwalQty, " +
                        "  (SELECT TOP 1 HS FROM mutASireport AS b WHERE b.itemID=m.itemID ORDER BY b.ID DESC) As HS, " +

                        "  CASE  when (SELECT  TOP  1 SaldoAwalQty FROM  mutASireport AS  b WHERE  b.itemID=m.itemID ORDER  BY  b.ID  DESC)>0 then  " +
                        "  (SELECT  TOP  1 TotalAmt FROM  mutASireport AS  b WHERE  b.itemID=m.itemID ORDER  BY  b.ID DESC) " +
                        "  ELSE  0 END  As  TotalAmt " +
                        "  INTO mutasisaldo  " +
                        "  FROM mutasireport AS m  " +
                        "  /*WHERE m.ItemID in(" + GetItemID(periodeBlnThn, GrpID) + ")*/ " +
                        "  GROUP BY m.ItemID  " +

               "  /** generate report final */ " +

               "    SELECT mds.ID,(SELECT (ItemCode +' '+ ItemName) as ItemName From Inventory WHERE ID=mds.ItemID) as ItemID,sa.SaldoAwalQty,sa.HS as SaldoAwalHS,sa.TotalAmt as SaldoAwalAmt,mds.BeliQty,mds.BeliHS,mds.BeliAmt,mds.AdjustQty, " +
               "    mds.AdjustHS,mds.AdjustAmt,mds.ProdQty,mds.ProdHS,mds.ProdAmt, " +
               "    mds.AdjProdQty,mds.AdjProdHS,mds.AdjProdAmt,mds.ReturnQty,mds.ReturnHS,mds.returnAmt, " +
               "    mds.RetSupQty,mds.RetSupHS,mds.RetSupAmt, " +
               "    mds.SaldoAwalQty as SaldoAkhirQty,mds.HS As SaldoAkhirHS,mds.TotalAmt As SaldoAkhirAmt " +
               "    FROM mutASisaldo AS mds " +
               "    INNER JOIN lapsaldoawal as sa " +
               "    ON mds.ItemID=sa.ItemID " +
               "    WHERE sa.SaldoAwalQty > 0 OR mds.BeliQty > 0 OR mds.AdjustQty >0 OR mds.ProdQty >0 OR mds.AdjProdQty >0 OR mds.ReturnQty >0 " +
               "    Order By itemid " +
               "/** return supplier  no avgprice*/" +
               "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapmutasitmp]') AND type in (N'U')) DROP TABLE [dbo].[lapmutasitmp] " +
               "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapmutasitmpx]') AND type in (N'U')) DROP TABLE [dbo].[lapmutasitmpx] " +
               "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapmutasitmpxx]') AND type in (N'U')) DROP TABLE [dbo].[lapmutasitmpxx] " +
               "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapmutasireport]') AND type in (N'U')) DROP TABLE [dbo].[lapmutasireport] " +
               "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mutasireport]') AND type in (N'U')) DROP TABLE [dbo].[mutasireport] " +
               "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mutasisaldo]') AND type in (N'U')) DROP TABLE [dbo].[mutasisaldo] " +
               "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapsaldoawal]') AND type in (N'U')) DROP TABLE [dbo].[lapsaldoawal] ";

                    #endregion Query String

                    alldata = connection.Query<LapMutasiBB>(strSQL).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }

        public static string FromProsesPosting { get; set; }
        public static List<LapMutasiBB> GetMutasiStockByName(string FirstPeriod, string LastPeriod, string MaterialGroup, string periodeTahun, string SaldoLaluQty, string SaldoLaluPrice, string NextSaldo, string Tahun, string bln, string IDiTems, string LockPeriode, string periodeBlnThn)
        {

            string strSQL = string.Empty;
            List<LapMutasiBB> alldata = new List<LapMutasiBB>();
            string created = ((Users)HttpContext.Current.Session["Users"]).ID.ToString();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    #region Query String

                    strSQL = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zd_" + created + "_lapmutasitmp]') AND type in (N'U')) DROP TABLE [dbo].[zd_" + created + "_lapmutasitmp] " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zd_" + created + "_lapmutasitmpx]') AND type in (N'U')) DROP TABLE [dbo].[zd_" + created + "_lapmutasitmpx] " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zd_" + created + "_lapmutasireport]') AND type in (N'U')) DROP TABLE [dbo].[zd_" + created + "_lapmutasireport] " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zd_" + created + "_mutasireport]') AND type in (N'U')) DROP TABLE [dbo].[zd_" + created + "_mutasireport] " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zd_" + created + "_mutasisaldo]') AND type in (N'U')) DROP TABLE [dbo].[zd_" + created + "_mutasisaldo] " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zd_" + created + "_mutasireport1]') AND type in (N'U')) DROP TABLE [dbo].[zd_" + created + "_mutasireport1] " +
                    "SELECT * INTO zd_" + created + "_lapmutasitmp FROM(  " +
                    "(SELECT '0' as Tipe,'01-" + bln + '-' + Tahun + "' as Tanggal,'Saldo Awal' as DocNo,si.ItemID,si." + SaldoLaluPrice + " as SaldoHS, " +
                    "    si." + SaldoLaluQty + " as SaldoQty,si." + SaldoLaluPrice + " as AvgPrice,(si." + SaldoLaluQty + "*si." + SaldoLaluPrice + ") as TotalPrice " +
                    "    from SaldoInventory as si " +
                    "    where si.YearPeriod='" + periodeTahun + "' AND si.ItemID IN(SELECT ID FROM Inventory WHERE  ID='" + IDiTems + "') " +
                    "    AND si.ItemTypeID='1' " +

                    ") UNION ALL  (" +
                    GetHargaReceipt(FirstPeriod, LastPeriod, MaterialGroup, IDiTems) +
                    ") UNION ALL ( " +
                    "SELECT '2' as Tipe,convert(varchar,k.PakaiDate,105) as Tanggal,k.PakaiNo,pk.ItemID,pk.AvgPrice,pk.Quantity, " +
                    "    (pk.AvgPrice) as AvgPrice,(pk.Quantity*pk.AvgPrice) as TotalPrice from Pakai as k  " +
                    "    left join PakaiDetail as pk  " +
                    "    on pk.PakaiID=k.ID " +
                    "    where  (convert(varchar,k.PakaiDate,112) BETWEEN '" + FirstPeriod + "' AND '" + LastPeriod + "') " +
                    "    AND pk.ItemID IN(SELECT ID FROM Inventory WHERE  ID='" + IDiTems + "') " +
                    "    AND k.Status >-1 AND pk.RowStatus >-1 AND pk.ItemTypeID=1" +
                    ") UNION ALL  ( " +
                    "SELECT '3' as Tipe, CONVERT(varchar,rp.ReturDate,105) as Tanggal,rp.ReturNo,rpd.ItemID,rpd.AvgPrice,rpd.Quantity, " +
                    "    (rpd.AvgPrice) as AvgPrice,(rpd.Quantity*rpd.AvgPrice) as TotalPrice from ReturPakai as rp " +
                    "    left join ReturPakaiDetail as rpd " +
                    "    on rpd.ReturID=rp.ID " +
                    "    where  (convert(varchar,rp.ReturDate,112) BETWEEN '" + FirstPeriod + "' AND '" + LastPeriod + "') " +
                    "    AND rpd.ItemID IN(SELECT ID FROM Inventory WHERE  ID='" + IDiTems + "') " +
                    "    AND rp.Status >-1 AND rpd.RowStatus>-1 AND rp.ItemTypeID=1" +
                    ") UNION ALL ( " +
                    "SELECT '4' as Tipe,CONVERT(VARCHAR,a.AdjustDate,105) as Tanggal, " +
                    "    case when a.AdjustType='Tambah' then (a.AdjustNo)else a.AdjustNo end NoM  ,ad.ItemID,ad.AvgPrice, " +
                    "    Case When a.AdjustType='Tambah' Then ad.Quantity else ad.Quantity END AdjQty,'0' as AvgPrice, " +
                    "    (ad.Quantity*ad.AvgPrice) as TotalPrice " +
                    "    from Adjust as a  " +
                    "    left join AdjustDetail as ad " +
                    "    on ad.AdjustID=a.ID " +
                    "    where  (convert(varchar,a.AdjustDate,112) BETWEEN '" + FirstPeriod + "' AND '" + LastPeriod + "') " +
                    "    AND ad.ItemID IN(SELECT ID FROM Inventory WHERE  ID='" + IDiTems + "') AND a.Status >-1 " +
                    "    AND ad.RowStatus >-1 AND a.AdjustType='Tambah' AND ad.ItemTypeID=1" +
                    ")UNION ALL ( " +
                    "SELECT '5' as Tipe,CONVERT(VARCHAR,a.AdjustDate,105) as Tanggal, " +
                    "    case when a.AdjustType='Kurang' then (a.AdjustNo)else a.AdjustNo end NoM  ,ad.ItemID,ad.AvgPrice, " +
                    "    Case When a.AdjustType='Kurang' Then ad.Quantity else ad.Quantity END AdjQty,'0' as AvgPrice, " +
                    "    (ad.Quantity*ad.AvgPrice) as TotalPrice " +
                    "    from Adjust as a  " +
                    "    left join AdjustDetail as ad " +
                    "    on ad.AdjustID=a.ID " +
                    "    where  (convert(varchar,a.AdjustDate,112) BETWEEN '" + FirstPeriod + "' AND '" + LastPeriod + "') " +
                    "    AND ad.ItemID IN(SELECT ID FROM Inventory WHERE  ID='" + IDiTems + "') AND a.Status >-1 " +
                    "    AND ad.RowStatus >-1 AND a.AdjustType='Kurang' AND ad.ItemTypeID=1" +
                    ")UNION ALL( " +

                    "SELECT '4' as Tipe,CONVERT(VARCHAR,a.AdjustDate,105) as Tanggal, " +
                    "    case when a.AdjustType='TambahHarga' then (a.AdjustNo)else a.AdjustNo end NoM  ,ad.ItemID,ad.AvgPrice, " +
                    "    Case When a.AdjustType='TambahHarga' Then ad.Quantity else ad.Quantity END AdjQty,ad.AvgPrice as AvgPrice, " +
                    "    (ad.AvgPrice) as TotalPrice " +
                    "    from Adjust as a  " +
                    "    left join AdjustDetail as ad " +
                    "    on ad.AdjustID=a.ID " +
                    "    where  (convert(varchar,a.AdjustDate,112) BETWEEN '" + FirstPeriod + "' AND '" + LastPeriod + "') " +
                    "    AND ad.ItemID IN(SELECT ID FROM Inventory WHERE  ID='" + IDiTems + "') AND a.Status >-1 " +
                    "    AND ad.RowStatus >-1 AND a.AdjustType='TambahHarga' AND ad.ItemTypeID=1" +
                    ")UNION ALL ( " +
                    "SELECT '5' as Tipe,CONVERT(VARCHAR,a.AdjustDate,105) as Tanggal, " +
                    "    case when a.AdjustType='KurangHarga' then (a.AdjustNo)else a.AdjustNo end NoM  ,ad.ItemID,ad.AvgPrice, " +
                    "    Case When a.AdjustType='KurangHarga' Then ad.Quantity else ad.Quantity END AdjQty,ad.AvgPrice  as AvgPrice, " +
                    "    (ad.AvgPrice) as TotalPrice " +
                    "    from Adjust as a  " +
                    "    left join AdjustDetail as ad " +
                    "    on ad.AdjustID=a.ID " +
                    "    where  (convert(varchar,a.AdjustDate,112) BETWEEN '" + FirstPeriod + "' AND '" + LastPeriod + "') " +
                    "    AND ad.ItemID IN(SELECT ID FROM Inventory WHERE  ID='" + IDiTems + "') AND a.Status >-1 " +
                    "    AND ad.RowStatus >-1 AND a.AdjustType='KurangHarga' AND ad.ItemTypeID=1" +
                    ")UNION ALL( " +

                    "SELECT '6' AS Tipe, " +
                    "    CONVERT(VARCHAR,rs.ReturDate,105) as Tanggal,rs.ReceiptNo,rsd.ItemID,rsd.Quantity as AvgPrice,rsd.Quantity, " +
                    "    CAST('0' AS Decimal(18,6)) AS AvgPrice ,CAST('0' AS Decimal(18,6)) AS Totalprice " +
                    "    FROM ReturSupplier AS rs " +
                    "    LEFT JOIN ReturSupplierDetail AS rsd " +
                    "    ON rsd.ReturID=rs.ID " +
                    "    where  (convert(varchar,rs.ReturDate,112) BETWEEN '" + FirstPeriod + "' AND '" + LastPeriod + "') " +
                    "    AND rsd.ItemID IN(SELECT ID FROM Inventory WHERE  ID='" + IDiTems + "') AND rs.Status >-1  " +
                    "    AND rsd.RowStatus >-1  AND rsd.ItemTypeID=1)" +
                    ") as K " +

                    "/** susun sesuai dengan kolom laporan */ " +
                    "SELECT ID,Tipe,ItemID,Tanggal,DocNo,SaldoAwalQty,HPP,(SaldoAwalQty*HPP) AS SaAmt, " +
                    "    BeliQty,BeliHS,TotalPB as BeliAmt, " +
                    "    AdjustQty,AdjustHS AS AdjustHS,case when docno<>'Adjust Harga' then (AdjustQty*HPP) else (HPP) end AdjAmt, " +
                    "    ReturnQty,ReturHS AS ReturHS,(ReturnQty*HPP) as RetAmt, " +
                    "    ProdQty,ProdHS AS ProdHS,(ProdQty*HPP) as ProdAmt, " +
                    "    AdjProdQty,AdjProdHS AS AdjProdHS,case when docno<>'Adjust Harga' then (AdjProdQty*HPP) else (HPP) end AdjPAmt,RetSupQty, " +
                    "    RetSupHS,(RetSupQty*RetSupHS) as RetSupAmt, " +
                    "    ((SaldoAwalQty+BeliQty+AdjustQty-ProdQty-AdjProdQty+ReturnQty-RetSupQty)) as TotalQty, " +
                    "    case when Docno<>'Adjust Harga' then ((SaldoAwalQty*HPP)+(BeliQty*BeliHS)+(ReturnQty*ReturHS )+(AdjustQty*AdjustHS)- " +
                    "   (ProdQty*ProdHS)-(AdjProdQty*AdjProdHS)-(RetSupQty*RetSupHs)) else " +
                    "   ((SaldoAwalQty*HPP)+(BeliQty*BeliHS)+(ReturnQty*ReturHS )+(AdjustHS)-(ProdQty*ProdHS)-(AdjProdHS)-(RetSupQty*RetSupHs)) end TotalAmt  " +
                    "   INTO zd_" + created + "_lapmutasitmpx " +
                    " FROM ( " +
                    " SELECT ROW_NUMBER() OVER(ORDER By Tanggal,Tipe) as ID,Tipe,itemID,Tanggal,DocNo, " +
                    "     CASE WHEN Tipe='0' THEN ISNULL(SaldoQty,0)  ELSE 0 END SaldoAwalQty, " +
                    "     CASE WHEN Tipe='0' THEN ISNULL(SaldoHS,0)  ELSE 0 END HPP, " +
                    "     CASE WHEN Tipe='0' THEN ISNULL(TotalPrice,0) ELSE 0 END TotalPa, " +
                    "     CASE WHEN Tipe='1' THEN ISNULL(SaldoQty,0)  ELSE 0 END BeliQty, " +
                    "     CASE WHEN Tipe='1' THEN ISNULL(SaldoHS,0)  ELSE 0 END BeliHS, " +
                    "     CASE WHEN Tipe='1' THEN ISNULL(TotalPrice,0)  ELSE 0 END TotalPB, " +
                    "     CASE WHEN Tipe='2' THEN ISNULL(SaldoQty,0)  ELSE 0 END ProdQty, " +
                    "     CASE WHEN Tipe='2' THEN ISNULL(SaldoHS,0)  ELSE 0 END ProdHS, " +
                    "     CASE WHEN Tipe='2' THEN ISNULL(TotalPrice,0)  ELSE 0 END TotalPAd, " +
                    "     CASE WHEN Tipe='3' THEN ISNULL(SaldoQty,0)  ELSE 0 END ReturnQty, " +
                    "     CASE WHEN Tipe='3' THEN ISNULL(SaldoHS,0)  ELSE 0 END ReturHS, " +
                    "     CASE WHEN Tipe='3' THEN ISNULL(TotalPrice,0)  ELSE 0 END TotalPP, " +
                    "     CASE WHEN Tipe='4' THEN ISNULL(SaldoQty,0)  ELSE 0 END AdjustQty, " +
                    "     CASE WHEN Tipe='4' THEN ISNULL(SaldoHS,0)  ELSE 0 END AdjustHS, " +
                    "     CASE WHEN Tipe='4' THEN ISNULL(TotalPrice,0)  ELSE 0 END TotalPR, " +
                    "     CASE WHEN Tipe='5' THEN ISNULL(SaldoQty,0)  ELSE 0 END AdjProdQty, " +
                    "     CASE WHEN Tipe='5' THEN ISNULL(SaldoHS,0)  ELSE 0 END AdjProdHS, " +
                    "     CASE WHEN Tipe='5' THEN ISNULL(TotalPrice,0)  ELSE 0 END TotalPAdjP, " +
                    "     CASE WHEN Tipe='6' THEN ISNULL(SaldoQty,0)  ELSE 0 END RetSupQty, " +
                    "     CASE WHEN Tipe='6' THEN ISNULL(SaldoHS,0)  ELSE 0 END RetSupHS, " +
                    "     CASE WHEN Tipe='6' THEN ISNULL(TotalPrice,0)  ELSE 0 END TotalRetSup " +
                    "  FROM zd_" + created + "_lapmutasitmp as x) AS Z ORDER BY z.Tanggal,z.Tipe " +

                    "   /**Susun  data tabular */ " +
                    "    SELECT ROW_NUMBER() OVER(Partition BY itemID order by Tanggal,Tipe) AS ID,Tipe,itemID,Tanggal,DocNo, " +
                    "    BeliQty,BeliHS, BeliAmt,AdjustQty, " +
                    "    CASE WHEN A.ID>1 AND A.AdjustQty >0 THEN (SELECT CASE WHEN SUM(TotalQty)>0 THEN (SUM(TotalAmt)/SUM(totalqty)) ELSE 0 END FROM zd_" + created + "_lapmutasitmpx WHERE ID<A.ID) when A.ID>1 AND A.DocNo='Adjust Harga' THEN A.AdjustHS ELSE A.HPP END AdjustHS, " +
                    "    ProdQty, " +
                    "    CASE WHEN A.ID>1 AND A.ProdQty >0 THEN (SELECT CASE WHEN SUM(TotalQty)>0 THEN (SUM(TotalAmt)/SUM(totalqty)) ELSE 0 END FROM zd_" + created + "_lapmutasitmpx WHERE ID<A.ID) ELSE A.HPP END ProdHS, " +
                    "    AdjProdQty, " +
                    "    CASE WHEN A.ID>1 AND A.AdjProdQty >0 THEN (SELECT CASE WHEN SUM(TotalQty)>0 THEN (SUM(TotalAmt)/SUM(totalqty)) ELSE 0 END FROM zd_" + created + "_lapmutasitmpx WHERE ID<A.ID)  when A.ID>1 AND A.DocNo='Adjust Harga' THEN A.AdjProdHS ELSE A.HPP END AdjProdHS, " +
                    "    A.ReturnQty, " +
                    "    CASE WHEN A.ID>1 AND A.ReturnQty >0 THEN (SELECT CASE WHEN SUM(TotalQty)>0 THEN (SUM(TotalAmt)/SUM(totalqty)) ELSE 0 END FROM zd_" + created + "_lapmutasitmpx WHERE ID<A.ID) ELSE A.HPP END ReturHS, " +
                    "    A.RetSupQty, " +
                    "    CASE WHEN A.ID>1 AND A.RetSupQty >0 THEN (SELECT CASE WHEN SUM(TotalQty)>0 THEN (SUM(TotalAmt)/SUM(totalqty)) ELSE 0 END FROM zd_" + created + "_lapmutasitmpx WHERE ID<A.ID) ELSE A.HPP END RetSupHS, " +
                    "    CASE WHEN A.ID>1 THEN (SELECT SUM(totalqty) FROM zd_" + created + "_lapmutasitmpx WHERE ID <=A.ID )ELSE TotalQty END SaldoAwalQty, " +
                    "    CASE WHEN A.ID>1 THEN  " +
                    "       CASE WHEN (SELECT SUM(totalqty)FROM zd_" + created + "_lapmutasitmpx WHERE ID <=A.ID )>=1 THEN " +
                    "        ((SELECT SUM(totalamt) FROM zd_" + created + "_lapmutasitmpx WHERE ID <=A.ID )/ " +
                    "        (ABS((SELECT SUM(totalqty)FROM zd_" + created + "_lapmutasitmpx WHERE ID <=A.ID ))))ELSE 0 END ELSE HPP END HS, " +
                    "    CASE WHEN A.ID>1 THEN (SELECT SUM(totalamt) FROM zd_" + created + "_lapmutasitmpx WHERE ID <=A.ID )ELSE Totalamt END TotalAmt " +
                    "    INTO zd_" + created + "_lapmutasireport " +
                    "    FROM zd_" + created + "_lapmutasitmpx as A " +
                    "    order by A.Tanggal,a.Tipe " +

                    "   /** Generate Detail Report without saldo akhir */ " +
                    "  SELECT ROW_NUMBER() OVER(Partition BY itemID order by Tanggal,Tipe)as ID, l.ItemID,l.Tanggal,l.DocNo,l.BeliQty,l.BeliHS, " +
                    "           l.BeliAmt,l.AdjustQty, " +
                    "           CASE WHEN L.ID >1 AND l.AdjustQty >0 THEN(SELECT ISNULL(HS,0) FROM zd_" + created + "_lapmutasireport WHERE ID=(L.ID-1) AND ItemID=L.ItemID) when L.ID>1 AND L.DocNo='Adjust Harga' THEN L.AdjustHS ELSE 0 END AdjustHS, " +
                    "           CASE WHEN L.ID >1 AND l.AdjustQty >0 THEN(SELECT ISNULL(HS,0) FROM zd_" + created + "_lapmutasireport WHERE ID=(L.ID-1) AND ItemID=L.ItemID)* L.AdjustQty when L.ID>1 AND L.DocNo='Adjust Harga' THEN L.AdjustHS ELSE 0 END AdjustAmt, " +
                    "           l.ProdQty, " +
                    "           CASE WHEN L.ID >1 AND l.ProdQty >0 THEN (SELECT ISNULL(HS,0) FROM zd_" + created + "_lapmutasireport WHERE ID=(L.ID-1) AND ItemID=L.ItemID)ELSE 0 END ProdHS, " +
                    "           CASE WHEN L.ID >1 AND l.ProdQty >0 THEN(SELECT ISNULL(HS,0) FROM zd_" + created + "_lapmutasireport WHERE ID=(L.ID-1) AND ItemID=L.ItemID)* L.ProdQty ELSE 0 END ProdAmt, " +
                    "           l.AdjProdQty, " +
                    "           CASE WHEN L.ID >1 AND l.AdjProdQty >0 THEN (SELECT ISNULL(HS,0) FROM zd_" + created + "_lapmutasireport WHERE ID=(L.ID-1) AND ItemID=L.ItemID)  when L.ID>1 AND L.DocNo='Adjust Harga' THEN L.AdjProdHS  ELSE 0 END AdjProdHS, " +
                    "           CASE WHEN L.ID >1 AND l.AdjProdQty >0 THEN(SELECT ISNULL(HS,0) FROM zd_" + created + "_lapmutasireport WHERE ID=(L.ID-1) AND ItemID=L.ItemID)* L.AdjProdQty  when L.ID>1 AND L.DocNo='Adjust Harga' THEN L.AdjProdHS ELSE 0 END AdjProdAmt, " +
                    "           l.ReturnQty, " +
                    "           CASE WHEN L.ID >1 AND l.ReturnQty >0 THEN (SELECT ISNULL(HS,0) FROM zd_" + created + "_lapmutasireport WHERE ID=(L.ID-1) AND ItemID=L.ItemID) when L.ID>1 AND L.DocNo='Adjust Harga' THEN L.AdjustHS  ELSE 0 END ReturnHS, " +
                    "           CASE WHEN L.ID >1 AND l.ReturnQty >0 THEN(SELECT ISNULL(HS,0) FROM zd_" + created + "_lapmutasireport WHERE ID=(L.ID-1) AND ItemID=L.ItemID)* L.ReturnQty ELSE 0 END returnAmt, " +
                    "           l.RetSupQty, " +
                    "           CASE WHEN L.ID >1 AND l.RetSupQty >0 THEN (SELECT ISNULL(HS,0) FROM zd_" + created + "_lapmutasireport WHERE ID=(L.ID-1) AND ItemID=L.ItemID)ELSE 0 END RetSupHS, " +
                    "           CASE WHEN L.ID >1 AND l.RetSupQty >0 THEN(SELECT ISNULL(HS,0) FROM zd_" + created + "_lapmutasireport WHERE ID=(L.ID-1) AND ItemID=L.ItemID)* L.RetSupQty ELSE 0 END RetSupAmt, " +
                    "           l.SaldoAwalQty,l.HS,l.TotalAmt " +
                    "    INTO zd_" + created + "_mutasireport " +
                    "    FROM zd_" + created + "_lapmutasireport AS L  " +
                    "    WHERE l.ItemID IN (SELECT ID FROM Inventory WHERE  ID='" + IDiTems + "') ORDER BY L.Tanggal,L.Tipe " +

                    #region

                    "  /** update colom amt dan colom hs */ " +
                     "  select row_number() over(order by itemID) as IDn,itemid into zd_" + created + "_mutasireport1 " +
                     "  from zd_" + created + "_mutasireport group by itemID order by itemid " +
                     "   declare @i int " +
                     "   declare @b int " +
                     "   declare @hs decimal(18,6) " +
                     "   declare @amt decimal(18,6) " +
                     "   declare @avgp decimal(18,6) " +
                     "   declare @itemTypeID int " +
                     "   declare @c int " +
                     "   declare @itm int " +
                     "   declare @itmID int " +
                     "   set @c=1 " +
                     "   set @itm=(select COUNT(IDn) from zd_" + created + "_mutasireport1) " +
                     "   set @itemTypeID=(select GroupID from Inventory where ID=(select top 1 ItemID from zd_" + created + "_mutasireport))" +
                     "  IF @itemTypeID<>9 " +
                     "   BEGIN   " +
                     "   While @c <=@itm " +
                     "   Begin " +
                     "   set @itmID=(select itemID from zd_" + created + "_mutasireport1 where IDn=@c) " +
                     "       set @b=1 " +
                     "       set @avgp=(select top 1 " + SaldoLaluPrice + " from SaldoInventory where ItemID = @itmID and YearPeriod=" + periodeTahun + ") " +
                     "       IF ISNULL(@avgp,0)=0 OR @avgp=0 " +
                                "begin " +
                                "set @avgp=(select top 1 HS from zd_" + created + "_mutasireport where itemid=@itmID and HS>0 ) " +
                                "end " +
                     "       set @i=(select COUNT(id) from zd_" + created + "_mutasireport where itemid=@itmID) " +
                     "       While @b<=@i  " +
                     "       Begin " +
                     "       set @hs=CASE WHEN @b >1 THEN (select hs from zd_" + created + "_mutasireport where ID=(@b) and itemid=@itmID)  " +
                     "               ELSE CASE WHEN(SELECT hs from zd_" + created + "_mutasireport where ID=1 and itemid=@itmID)>0 THEN " +
                     "		         (SELECT hs from zd_" + created + "_mutasireport where ID=1 and itemid=@itmID)ELSE @avgp END " +
                     "		         END  " +
                     "       set @amt=CASE WHEN @b >1 THEN (select TotalAmt from zd_" + created + "_mutasireport where ID=(@b) and itemid=@itmID)  " +
                     "                ELSE (SELECT TotalAmt from zd_" + created + "_mutasireport where ID=1 and itemid=@itmID)  " +
                     "                END  " +
                     "       /** update semua hs */ " +
                     "       update zd_" + created + "_mutasireport  " +
                     "       set AdjustHS	=CASE WHEN (SELECT AdjustQty FROM zd_" + created + "_mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                     "           ProdHS		=CASE WHEN (SELECT ProdQty FROM zd_" + created + "_mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                     "           ReturnHS	=CASE WHEN (SELECT ReturnQty FROM zd_" + created + "_mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                     "           AdjProdHS	=CASE WHEN (SELECT AdjProdQty FROM zd_" + created + "_mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                     "           RetSupHS	=CASE WHEN (SELECT RetSupQty FROM zd_" + created + "_mutasireport WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END, " +
                     "           ProdAmt    =(ProdQty*@hs), " +
                     "           AdjustAmt  =(AdjustQty*@hs), " +
                     "           AdjProdAmt =(AdjProdQty*@hs), " +
                     "           returnAmt  =(ReturnQty*@hs), " +
                     "           RetSupAmt  =(RetSupQty*@hs), " +
                     "           totalamt   =(Beliamt+(adjustQty*@hs)+(returnQty*@hs)-(ProdQty*@hs)-(AdjprodQty*@hs)+(@amt)), " +
                     "           hs=case when abs(SaldoAwalQty)>0 then " +
                     "             ABS(((beliQty*BeliHs)+(adjustQty*@hs)+(returnQty*@hs)-(ProdQty*@hs)-(AdjprodQty*@hs)+@amt )/SaldoAwalQty) else @avgp end  " +
                     "              where ID=(@b+1) and itemid=@itmID " +
                     "       set @b=@b+1 " +
                     "       END " +
                     "   set @c=@c+1 " +
                     "   END " +
                     " END " +

                    #endregion Query String

                     "   /** Generate Saldo Akhir */ " +
                     "       SELECT (COUNT(m.itemID)+1)as ID,m.itemID, ' ' as Tanggal,'Total' as DocNo, " +
                     "           /** pembelian */ " +
                     "             (SUM(m.BeliQty)) As BeliQty, " +
                     "             CASE WHEN SUM(m.BeliAmt) > 0 THEN (SUM(m.BeliAmt)/SUM(m.BeliQty))ELSE 0 END BeliHS, " +
                     "             (SUM(m.BeliAmt)) As BeliAmt, " +
                     "           /** Ajdut Plust */ " +
                     "             (SUM(m.AdjustQty)) As AdjustQty, " +
                     "             CASE WHEN SUM(m.AdjustQty) > 0 THEN (SUM(m.AdjustAmt)/SUM(m.AdjustQty)) WHEN SUM(m.AdjustQty)=0 and SUM(m.AdjustAmt)>0 THEN (SUM(m.AdjustAmt)) ELSE 0 END AdjustHS, " +
                     "             (SUM(m.AdjustAmt)) As AdjustAmt, " +
                     "           /** Pemakaian Produksi */ " +
                     "             (SUM(m.ProdQty)) As ProdQty, " +
                     "             CASE WHEN SUM(m.ProdAmt) > 0 THEN (SUM(m.ProdAmt)/SUM(m.ProdQty))ELSE 0 END ProdHS, " +
                     "             (SUM(m.ProdAmt)) As ProdAmt, " +
                     "           /** Adjut minus */ " +
                     "             (SUM(m.AdjProdQty)) As AdjProdQty, " +
                     "             CASE WHEN  SUM(m.AdjProdQty) > 0 THEN (SUM(m.AdjProdAmt)/SUM(m.AdjProdQty)) WHEN  SUM(m.AdjProdQty) = 0 and SUM(m.AdjProdAmt)>0 THEN (SUM(m.AdjProdAmt)) ELSE 0 END AdjProdHS, " +
                     "             (SUM(m.AdjProdAmt)) As AdjProdAmt, " +
                     "           /** Return */ " +
                     "             (SUM(m.ReturnQty)) As ReturnQty, " +
                     "             CASE WHEN SUM(m.returnAmt) > 0 THEN (SUM(m.returnAmt)/SUM(m.ReturnQty))ELSE 0 END ReturnHS, " +
                     "             (SUM(m.returnAmt)) As returnAmt, " +
                     "            /** Return Supplier */ " +
                     "            (SUM(m.RetSupQty)) As RetSupQty, " +
                     "            CASE WHEN SUM(m.RetSupQty) > 0 THEN (SUM(m.RetSupAmt)/ABS(SUM(m.RetSupQty)))ELSE 0 END RetSupHS, " +
                     "            (SUM(m.RetSupAmt)) As RetSupAmt, " +
                     "           /** Saldo Akhir */ " +
                     "             (SELECT TOP 1 SaldoAwalQty FROM zd_" + created + "_mutasireport as b WHERE b.itemID=m.itemID ORDER BY b.ID  DESC) As SaldoAwalQty, " +
                     "             (SELECT TOP 1 HS FROM zd_" + created + "_mutasireport as b WHERE b.itemID=m.itemID ORDER BY b.ID DESC) As HS, " +
                     "             (SELECT TOP 1 TotalAmt FROM zd_" + created + "_mutasireport as b WHERE b.itemID=m.itemID ORDER BY b.ID DESC) As TotalAmt " +
                     "       INTO zd_" + created + "_mutasisaldo " +
                     "       FROM zd_" + created + "_mutasireport as m " +
                     "       GROUP BY m.ItemID ";
                    if (int.Parse(periodeBlnThn) > int.Parse(LockPeriode))
                    {
                        strSQL += (FromProsesPosting == string.Empty || FromProsesPosting == null) ? "" :
                         "/*Update table transaksi */ " +
                          "UPDATE p  " +
                          "SET p.AvgPrice=a.AvgPrice " +
                          "FROM PakaiDetail as p " +
                          "INNER JOIN UpdateAvgPrice as a " +
                          "ON P.PakaiID=a.ID " +
                          "WHERE a.Tabel='PakaiDetail' and p.ItemID=a.itemID " +
                          "/** penerimaan*/ " +
                          "UPDATE p  " +
                          "SET p.AvgPrice=a.AvgPrice " +
                          "FROM ReceiptDetail as p " +
                          "INNER JOIN UpdateAvgPrice as a " +
                          "ON P.ReceiptID=a.ID " +
                          "WHERE a.Tabel='ReceiptDetail' and p.ItemID=a.itemID " +
                          "/**penyesuaian produksi */ " +
                          "UPDATE p  " +
                          "SET p.AvgPrice=a.AvgPrice " +
                          "FROM ReturPakaiDetail as p " +
                          "INNER JOIN UpdateAvgPrice as a " +
                          "ON P.ReturID=a.ID " +
                          "WHERE a.Tabel='ReturPakaiDetail' and p.ItemID=a.itemID " +
                          "/** adjust Tambah */ " +
                          "UPDATE p  " +
                          "SET p.AvgPrice=a.AvgPrice " +
                          "FROM AdjustDetail as p " +
                          "INNER JOIN UpdateAvgPrice as a " +
                          "ON P.AdjustID=a.ID " +
                          "WHERE a.Tabel='AdjustDetailT' and p.ItemID=a.itemID  " +
                          "/** Adjust Kurang */ " +
                          "UPDATE p " +
                          "SET p.AvgPrice=a.AvgPrice " +
                          "FROM AdjustDetail as p " +
                          "INNER JOIN UpdateAvgPrice as a " +
                          "ON P.AdjustID=a.ID " +
                          "WHERE a.Tabel='AdjustDetailK' and p.ItemID=a.itemID ";
                    }
                    strSQL += " /** generate final report */ " +
                             "   SELECT XZ.*, ROW_NUMBER() OVER(ORDER BY Tanggal) AS ID FROM ( " +
                             "       (SELECT md.ID,md.Tanggal,md.DocNo,md.BeliQty,md.BeliHS,md.BeliAmt,md.AdjustQty,md.AdjustHS,md.AdjustAmt,md.ProdQty,md.ProdHS,md.ProdAmt, " +
                             "               md.RetSupQty,md.RetSupHS,md.RetSupAmt, " +
                             "               md.AdjProdQty,md.AdjProdHS,md.AdjProdAmt,md.ReturnQty,md.ReturnHS,md.returnAmt,md.SaldoAwalQty,md.HS As SaldoAwalHS,md.TotalAmt As SaldoAwalAmt " +
                             "               FROM zd_" + created + "_mutasireport as md " +
                             "       )UNION ALL ( " +
                             "       SELECT dm.ID,dm.Tanggal,dm.DocNo,dm.BeliQty,dm.BeliHS,dm.BeliAmt,dm.AdjustQty,dm.AdjustHS,dm.AdjustAmt,dm.ProdQty," +
                             "              dm.ProdHS,dm.ProdAmt,dm.RetSupQty,dm.RetSupHS,dm.RetSupAmt,dm.AdjProdQty,dm.AdjProdHS,dm.AdjProdAmt," +
                             "              dm.ReturnQty,dm.ReturnHS,dm.returnAmt,dm.SaldoAwalQty,dm.HS As SaldoAwalHS,dm.TotalAmt As SaldoAwalQty " +
                             "              FROM zd_" + created + "_mutasisaldo as dm " +
                             "       ) " +
                             "   ) AS XZ " +
                             "   ORDER BY XZ.ID " +
                             " /** Delete temporary table */ " +
                            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zd_" + created + "_lapmutasitmp]') AND type in (N'U')) DROP TABLE [dbo].[zd_" + created + "_lapmutasitmp] " +
                            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zd_" + created + "_lapmutasitmpx]') AND type in (N'U')) DROP TABLE [dbo].[zd_" + created + "_lapmutasitmpx] " +
                            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zd_" + created + "_lapmutasireport]') AND type in (N'U')) DROP TABLE [dbo].[zd_" + created + "_lapmutasireport] " +
                            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zd_" + created + "_mutasireport]') AND type in (N'U')) DROP TABLE [dbo].[zd_" + created + "_mutasireport] " +
                            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zd_" + created + "_mutasireport1]') AND type in (N'U')) DROP TABLE [dbo].[zd_" + created + "_mutasireport1] " +
                            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[zd_" + created + "_mutasisaldo]') AND type in (N'U')) DROP TABLE [dbo].[zd_" + created + "_mutasisaldo] ";

                    #endregion
                    alldata = connection.Query<LapMutasiBB>(strSQL).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }
    }
}