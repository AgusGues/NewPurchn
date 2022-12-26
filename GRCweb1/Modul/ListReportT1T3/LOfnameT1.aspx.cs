using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.ListReportT1T3
{
    public partial class LOfnameT11 : System.Web.UI.Page
    {
        private ReportDocument hierarchicalGroupingReport;
        private string exportPath;
        private DiskFileDestinationOptions diskFileDestinationOptions;
        private ExportOptions exportOptions;
        private bool selectedNoFormat = false;


        private ReportDocument objRpt1;
        private ReportDocument objRpt2;
        private ReportDocument objRpt3;
        private ReportDocument objRpt4;
        string STRPRINTERNAME;
        private string papername = "SJ";
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewOfnameT1();
        }
        public void ViewOfnameT1()
        {

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            //try
            //{
            string strSQL = "declare @HMY1 varchar(10)   " +
                    "declare @HMY2 varchar(10)   " +
                    "declare @thnbln varchar(6)   " +
                    "declare @tahun int declare @bulan int declare @tgl int " +
                    "declare @lastmonth datetime " +
                    "set @tahun=year(getdate()) set @bulan=MONTH (getdate()) set @tgl=day(getdate()) " +
                    "select @HMY1=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0') + '01' " +
                    "set @lastmonth=cast(@HMY1 as datetime)-2 " +
                    "select @HMY2=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0') + REPLACE(STR(@tgl, 2), SPACE(1), '0')   " +
                    "set @lastmonth=cast(@HMY1 as datetime)-2 " +
                    "select @thnbln=cast(year(@lastmonth) as char(4)) + REPLACE(STR(MONTH (@lastmonth), 2), SPACE(1), '0') " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempWIP1]') AND type in (N'U')) DROP TABLE [dbo].[tempWIP1] " +
                    "select * into tempWIP1 from vw_KartustockWIP where CONVERT(char, tanggal ,112)>=@HMY1 and CONVERT(char, tanggal ,112)<=@HMY2 " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempWIP2]') AND type in (N'U')) DROP TABLE [dbo].[tempWIP2] " +
                    "select * into tempWIP2 from vw_KartustockWIP2 where CONVERT(char, tanggal ,112)>=@HMY1 and CONVERT(char, tanggal ,112)<=@HMY2 " +
                    "select * from (select itemID,HMY,NoDocument partno,sum(AwalQty) as AwalQty,sum(InProdQty) as InProdQty,sum(InP99) as InP99,sum(InAdjustQty) as InAdjustQty,sum(InI99) as InI99,  " +
                    "sum(outAdjustQty) as outAdjustQty,sum(H99) as H99,sum(OutI99) as I99,sum(B99) as B99,sum(C99) as C99,sum(Q99) as Q99,sum(OutP99) as OutP99," +
                    "(sum(AwalQty)+ sum(InProdQty) +sum(InP99) +sum(InAdjustQty) +sum(InI99) -sum(outAdjustQty) -sum(H99) -sum(OutI99) -sum(B99) -sum(C99) -sum(Q99) -sum(OutP99) )Qty,  " +
                    "(select (Tebal/1000)*(Lebar/1000)*(Panjang/1000) from fc_items where ID= atALL.itemid)as volume,(select Tebal from fc_items where ID= atALL.itemid) Tebal from(  " +
                    "    select itemID,HMY,NoDocument,AwalQty,InProdQty,InP99,InAdjustQty,InI99,outAdjustQty,H99,OutI99,B99,C99,Q99,OutP99 from (    " +
                    "        select itemID,HMY,partno as NoDocument,   " +
                    "        (select isnull(SUM(qty),0) from BM_Destacking where qty>0 and CONVERT(char, tglproduksi ,112)>=@HMY1 and CONVERT(char, tglproduksi ,112)<=@HMY2   " +
                    "        and ItemID=M1.ItemID and RowStatus>-1 and lokasiID not in (select ID from fc_lokasi where lokasi like '%in%')) as InProdQty,0 as InP99,  " +
                    "        (select isnull(SUM(qty),0) from tempWIP1 where qty>0 and ItemID0=M1.ItemID  and idrec like '%in%') as InAdjustQty,  0 as InI99,  " +
                    "        (select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID  and (lokasi='H99' and process<>'lari' and process<>'listplank')) as H99,   " +
                    "        (select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID   and (lokasi='i99' and process<>'lari' and process<>'listplank')) as OutI99,  " +
                    "        (select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID  and lokasi='B99' and process<>'lari' and process<>'listplank' ) as B99,   " +
                    "        (select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID  and lokasi='C99' and process<>'lari' and process<>'listplank') as C99,   " +
                    "        (select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID  and lokasi='Q99' and process<>'lari' and process<>'listplank') as Q99,   " +
                    "        (select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID  and process='lari') as OutP99,  " +
                    "        (select isnull(SUM(qty)*-1,0) from tempWIP1 where qty<0 and ItemID0=M1.ItemID  and idrec like '%ou%') as outAdjustQty,   " +
                    "        (select isnull(SUM(saldo),0) from T1_SaldoPerLokasi where  rowstatus>-1 and thnbln=@thnbln and itemid =M1.ItemID and   " +
                    "        LokID not in (select ID from FC_Lokasi where Lokasi='p99'))+isnull((select SUM(qty) from vw_KartustockWIP where ItemID0=M1.ItemID and process<>'lari' and process<>'listplank' and left(CONVERT(varchar,tanggal, 112),6) = left(@HMY1,6) and CONVERT(varchar,tanggal, 112) <@HMY1),0) as AwalQty   " +
                    "        from(select distinct A.itemid as ItemID,(select partno from fc_items where ID=A.itemid) as partno, @HMY1 as HMY from (  " +
                    "        select distinct itemid  from bm_destacking where tglproduksi>='6/1/2013'  " +
                    "        ) as A   " +
                    "    ) as M1   " +
                    ") as M2 where Awalqty<>0 or InProdQty<>0 or InAdjustQty<>0 or outAdjustQty<>0 or H99<>0 or OutI99<>0  or B99<>0 or C99<>0 or Q99<>0   " +
                    //"union all   " +
                    //"select itemID,HMY,NoDocument,AwalQty,InProdQty,InP99,InAdjustQty,InI99,outAdjustQty,H99,OutI99,B99,C99,Q99,OutP99 from (    " +
                    //"    select itemID,HMY, partno as NoDocument,0 as InProdQty,  " +
                    //"    (select isnull(SUM(qty),0) from tempWIP2 where qty>0  and idrec not like '%IN%' and ItemID0=M1.ItemID and   " +
                    //"    process='lari') as InP99,  " +
                    //"    (select isnull(SUM(qty),0) from tempWIP2 where qty>0 and ItemID0=M1.ItemID and   " +
                    //"    idrec like '%in%'  and process='lari') as InAdjustQty,  0 as InI99,  " +
                    //"    (select isnull(SUM(qty)*-1,0) from tempWIP2 V inner join FC_Items I on V.itemID=I.ID and (I.partno like '%-3-%' or I.PartNo like '%-W-%' or I.PartNo like '%-m-%')   " +
                    //"    where qty<0  and idrec not like '%ou%' and ItemID0=M1.ItemID   and process='lari') as H99,0 as OutI99,  " +
                    //"   (select isnull(SUM(qty)*-1,0) from tempWIP2 V inner join FC_Items I on V.itemID=I.ID and (I.partno  like '%-p-%' or I.partno  like '%-1-%' )   " +
                    //"    where qty<0  and idrec not like '%ou%'  and ItemID0=M1.ItemID   and process='lari') as B99,0 as C99, 0 as Q99, 0 as OutP99,   " +
                    //"    (select isnull(SUM(qty)*-1,0) from tempWIP2 where qty<0 and ItemID0=M1.ItemID    " +
                    //"    and idrec like '%ou%') as outAdjustQty, (select isnull(SUM(saldo),0) from T1_SaldoPerLokasi where  rowstatus>-1 and thnbln=@thnbln   " +
                    //"    and itemid =M1.ItemID and LokID in (select ID from FC_Lokasi where Lokasi='p99'))+isnull((select SUM(qty) from vw_KartustockWIP2 where ItemID0=M1.ItemID and process='lari' and left(CONVERT(varchar,tanggal, 112),6) = left(@HMY1,6) and CONVERT(varchar,tanggal, 112) <@HMY1),0)as AwalQty   " +
                    //"    from(select distinct A.itemid0 as ItemID,(select partno from fc_items where ID=A.itemid0) as partno, @HMY1 as HMY from (  " +
                    //"        select distinct itemid0  from vw_KartuStockWIP2 where tanggal >='6/1/2013'  " +
                    //"        ) as A   " +
                    //"    ) as M1   " +
                    //") as M2 where Awalqty<>0 or InProdQty<>0 or InAdjustQty<>0 or outAdjustQty<>0 or H99<>0 or OutI99<>0 or B99<>0 or C99<>0 or Q99<>0 or InP99<>0 or OutP99<>0   " +
                    ") as atALL group by itemID,HMY,NoDocument )a1 where qty>0 ";
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
            da.SelectCommand.CommandTimeout = 0;
            DataTable dt = new DataTable();
            da.Fill(dt);
            objRpt1 = new ReportDocument();
            objRpt1.Load(this.Server.MapPath("LOfnameT1.rpt"));
            objRpt1.SetDataSource(dt);
            crViewer.ReportSource = objRpt1;
            crViewer.DisplayToolbar = true;
            //SetCurrentValuesForParameterField(objRpt1, Session["periode"].ToString(), "periode");
            //}
            //catch
            //{

            //}
        }
    }
}