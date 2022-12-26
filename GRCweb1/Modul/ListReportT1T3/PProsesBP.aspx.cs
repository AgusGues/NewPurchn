using System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BusinessFacade;
using Domain;
using System.IO;
using System.Data.SqlClient;

namespace GRCweb1.Modul.ISO
{
    public partial class PProsesBP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadTahun();
                if (Request.QueryString["t"] != null)
                {
                    ddlTahun.SelectedValue = Request.QueryString["t"].ToString();
                }
            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('thr', 450, 99 , 70 ,false); </script>", false);
        }
        public decimal TotalPES = 0;
        public decimal TotalBobot = 0;
        
        private void LoadTahun()
        {
            ISO_PES p = new ISO_PES();
            ArrayList arrData = new ArrayList();
            arrData = p.LoadTahun();
            ddlTahun.Items.Clear();
            if (arrData.Count == 0)
            {
                ddlTahun.Items.Add(new ListItem(DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString()));
            }
            foreach (PES2016 ps in arrData)
            {
                ddlTahun.Items.Add(new ListItem(ps.Tahun.ToString(), ps.Tahun.ToString()));
            }
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();

        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=PantauProsesBP.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<b>Pemantauan Proses Produk BP</b>";
            Html += "<br>Periode : " + ddlTahun.SelectedValue.ToString();
            string HtmlEnd = "";
            //lstForPrint.RenderControl(hw);
            lstr.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0", "border=\"1");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpProsesBP_in]') AND type in (N'U')) DROP TABLE [dbo].tmpProsesBP_in " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpProsesBP_out]') AND type in (N'U')) DROP TABLE [dbo].tmpProsesBP_out " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpAdjustBP_in]') AND type in (N'U')) DROP TABLE [dbo].tmpAdjustBP_in " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpAdjustBP_out]') AND type in (N'U')) DROP TABLE [dbo].tmpAdjustBP_out " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpProsesBP_SA]') AND type in (N'U')) DROP TABLE [dbo].tmpProsesBP_SA " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DataJadiprosesbp]') AND type in (N'U')) DROP TABLE [dbo].DataJadiprosesbp " +
                " " +
                "declare @thbl varchar(6),@th0 varchar(4) " +
                "set @thbl='"+ ddlTahun.SelectedItem.Text  +"' " +
                "set @th0=cast(cast(@thbl as int)-1 as char) " +
                " " +
                "select * into tmpProsesBP_SA from ( " +
                "	select SUBSTRING( I.PartNo ,7,11) ukuran,1 bln,sum(desqty) QtySA from SaldoInventoryBJ  A inner join fc_items I on A.itemid =I.id  where YearPeriod=@th0 and  " +
                "	itemid in (select id from fc_items where partno like '%-p-%' and panjang*lebar>1000*2000 and panjang<3000 and tebal <=4) group by SUBSTRING( I.PartNo ,7,11)  " +
                "	union all " +
                "	select SUBSTRING( I.PartNo ,7,11) ukuran,2 bln,sum(janqty) QtySA from SaldoInventoryBJ  A inner join fc_items I on A.itemid =I.id  where YearPeriod=@thbl and  " +
                "	itemid in (select id from fc_items where partno like '%-p-%' and panjang*lebar>1000*2000 and panjang<3000 and tebal <=4) group by SUBSTRING( I.PartNo ,7,11)  " +
                "	union all " +
                "	select SUBSTRING( I.PartNo ,7,11) ukuran,3 bln,sum(febqty) QtySA from SaldoInventoryBJ  A inner join fc_items I on A.itemid =I.id  where YearPeriod=@thbl and  " +
                "	itemid in (select id from fc_items where partno like '%-p-%' and panjang*lebar>1000*2000 and panjang<3000 and tebal <=4) group by SUBSTRING( I.PartNo ,7,11)  " +
                "	union all " +
                "	select SUBSTRING( I.PartNo ,7,11) ukuran,4 bln,sum(marqty) QtySA from SaldoInventoryBJ  A inner join fc_items I on A.itemid =I.id  where YearPeriod=@thbl and  " +
                "	itemid in (select id from fc_items where partno like '%-p-%' and panjang*lebar>1000*2000 and panjang<3000 and tebal <=4) group by SUBSTRING( I.PartNo ,7,11)  " +
                "	union all " +
                "	select SUBSTRING( I.PartNo ,7,11) ukuran,5 bln,sum(aprqty) QtySA from SaldoInventoryBJ  A inner join fc_items I on A.itemid =I.id  where YearPeriod=@thbl and  " +
                "	itemid in (select id from fc_items where partno like '%-p-%' and panjang*lebar>1000*2000 and panjang<3000 and tebal <=4) group by SUBSTRING( I.PartNo ,7,11)  " +
                "	union all " +
                "	select SUBSTRING( I.PartNo ,7,11) ukuran,6 bln,sum(meiqty) QtySA from SaldoInventoryBJ  A inner join fc_items I on A.itemid =I.id  where YearPeriod=@thbl and  " +
                "	itemid in (select id from fc_items where partno like '%-p-%' and panjang*lebar>1000*2000 and panjang<3000 and tebal <=4) group by SUBSTRING( I.PartNo ,7,11)  " +
                "	union all " +
                "	select SUBSTRING( I.PartNo ,7,11) ukuran,7 bln,sum(junqty) QtySA from SaldoInventoryBJ  A inner join fc_items I on A.itemid =I.id  where YearPeriod=@thbl and  " +
                "	itemid in (select id from fc_items where partno like '%-p-%' and panjang*lebar>1000*2000 and panjang<3000 and tebal <=4) group by SUBSTRING( I.PartNo ,7,11)  " +
                "	union all " +
                "	select SUBSTRING( I.PartNo ,7,11) ukuran,8 bln,sum(julqty) QtySA from SaldoInventoryBJ  A inner join fc_items I on A.itemid =I.id  where YearPeriod=@thbl and  " +
                "	itemid in (select id from fc_items where partno like '%-p-%' and panjang*lebar>1000*2000 and panjang<3000 and tebal <=4) group by SUBSTRING( I.PartNo ,7,11)  " +
                "	union all " +
                "	select SUBSTRING( I.PartNo ,7,11) ukuran,9 bln,sum(aguqty) QtySA from SaldoInventoryBJ  A inner join fc_items I on A.itemid =I.id  where YearPeriod=@thbl and  " +
                "	itemid in (select id from fc_items where partno like '%-p-%' and panjang*lebar>1000*2000 and panjang<3000 and tebal <=4) group by SUBSTRING( I.PartNo ,7,11)  " +
                "	union all " +
                "	select SUBSTRING( I.PartNo ,7,11) ukuran,10 bln,sum(sepqty) QtySA from SaldoInventoryBJ  A inner join fc_items I on A.itemid =I.id  where YearPeriod=@thbl and  " +
                "	itemid in (select id from fc_items where partno like '%-p-%' and panjang*lebar>1000*2000 and panjang<3000 and tebal <=4) group by SUBSTRING( I.PartNo ,7,11)  " +
                "	union all " +
                "	select SUBSTRING( I.PartNo ,7,11) ukuran,11 bln,sum(oktqty) QtySA from SaldoInventoryBJ  A inner join fc_items I on A.itemid =I.id  where YearPeriod=@thbl and  " +
                "	itemid in (select id from fc_items where partno like '%-p-%' and panjang*lebar>1000*2000 and panjang<3000 and tebal <=4) group by SUBSTRING( I.PartNo ,7,11)  " +
                "	union all " +
                "	select SUBSTRING( I.PartNo ,7,11) ukuran,12 bln,sum(novqty) QtySA from SaldoInventoryBJ  A inner join fc_items I on A.itemid =I.id  where YearPeriod=@thbl and  " +
                "	itemid in (select id from fc_items where partno like '%-p-%' and panjang*lebar>1000*2000 and panjang<3000 and tebal <=4) group by SUBSTRING( I.PartNo ,7,11)  " +
                ")S where QtySA>0 order by ukuran ,bln " +
                " " +
                "if (select substring(@thbl,5,2))='01' begin set @th0= cast(cast((select substring(@thbl,1,4)) as int) -1 as char) end else begin set @th0=(select substring(@thbl,1,4))  end " +
                " " +
                "select * into tmpProsesBP_in from ( " +
                "	select SUBSTRING( I.PartNo ,7,11) ukuran,month(tgltrans) bln, A.QtyIn,Keterangan,Process  from T3_Rekap A inner join fc_items I on A.itemid =I.id  " +
                "	where A.rowstatus>-1 and year(tgltrans)=@thbl and itemid in (select id from fc_items where partno like '%-p-%' and panjang*lebar>1000*2000 and panjang<3000 and tebal <=4) and Qtyout=0   " +
                "	union all " +
                "	select SUBSTRING( I.PartNo ,7,11) ukuran,month(tgltrans) bln, A.Qty QtyIn,I.PartNo Keterangan,'Retur' Process  from t3_retur A inner join fc_items I on A.itemid =I.id  " +
                "	where A.rowstatus>-1 and year(tgltrans)=@thbl and itemid in (select id from fc_items where partno like '%-p-%' and panjang*lebar>1000*2000 and panjang<3000 and tebal <=4)  " +
                ")P_in  order by ukuran,process,Keterangan  " +
                " " +
                "select SUBSTRING( I.PartNo ,7,11) ukuran,month(A.tgltrans) bln, A.Qtyout,  Keterangan,Process,L.Lokasi  into tmpProsesBP_out from T3_Rekap A inner join fc_items I on A.itemid =I.id  " +
                "inner join t3_simetris S on A.cutid=S.ID inner join fc_lokasi L on S.lokid=L.ID where A.rowstatus>-1 and year(A.tgltrans)=@thbl  " +
                "and A.itemid in (select id from fc_items where partno like '%-p-%' and panjang*lebar>1000*2000 and panjang<3000 and tebal <=4) and A.Qtyin=0  order by ukuran,process,Keterangan  " +
                " " +
                "select SUBSTRING( I.PartNo ,7,11) ukuran,month(A.AdjustDate)bln,B.QtyIn,I.PartNo Keterangan,'Adj_Out' Process into  tmpAdjustBP_in from T3_Adjust A inner join T3_AdjustDetail B on A.id=B.AdjustID  " +
                "inner join fc_items i on i.id=B.ItemID  where YEAR(A.adjustdate)=@thbl and I.partno like '%-p-%' and I.panjang*lebar>1000*2000 and panjang<3000 and tebal <=4 " +
                "select SUBSTRING( I.PartNo ,7,11) ukuran,month(A.AdjustDate)bln,B.QtyOut,I.PartNo Keterangan,'Adj_Out' Process into  tmpAdjustBP_out from T3_Adjust A inner join T3_AdjustDetail B on A.id=B.AdjustID  " +
                "inner join fc_items i on i.id=B.ItemID  where YEAR(A.adjustdate)=@thbl and I.partno like '%-p-%' and I.panjang*lebar>1000*2000 and panjang<3000 and tebal <=4 " +
                " " +
                "select substring(cast(cast(substring(ukuran,1,3) as decimal(8,1) )/10 as char),1,3)+' X '+substring(ukuran,4,4)+' X '+substring(ukuran,9,4)ukuran ,bln,case bln when 1 then 'Januari' when 2 then 'Februari' " +
                "when 3 then 'Maret'when 4 then 'april'when 5 then 'Mai'when 6 then 'Juni'when 7 then 'Juli'when 8 then 'Agustus' when 9 then 'September'when 10 then 'Oktober'when 11 then 'November'when 12 then 'Desember' end bln1, " + 
                "cast(SA as int) SA,qtyin1,qtyin2,qtyin3,qtyin4,qtyin5,qtyout6,qtyout7,qtyout8,qtyout10,qtyout11,qtyout12,qtyout13,qtyout15, " +
                "qtyout16,qtyout17,qtyout18,qtyout20,qtyout21,qtyout22,qtyout23,qtyout25,qtyout26,qtyout27,qtyout28,qtyout30,qtyout31,(qtyout10+qtyout15+qtyout20+qtyout25+qtyout30+qtyout31) qtyout32, " +
                "SA+qtyin5-(qtyout10+qtyout15+qtyout20+qtyout25+qtyout30+qtyout31) Saldo into DataJadiprosesbp from( " +
                "	select ukuran,bln,SA,qtyin1,qtyin2,qtyin3,(select isnull(sum(qtyin),0) from tmpAdjustBP_in where ukuran=X1.ukuran and bln=X1.bln )qtyin4,(qtyin1+qtyin2+qtyin3+qtyin4)qtyin5,qtyout6,qtyout7,qtyout8, " +
                "	(qtyout6+qtyout7+qtyout8)qtyout10,qtyout11,qtyout12,qtyout13,(qtyout11+qtyout12+qtyout13)qtyout15, " +
                "	qtyout16,qtyout17,qtyout18,(qtyout16+qtyout17+qtyout18)qtyout20,qtyout21,qtyout22,qtyout23,(qtyout21+qtyout22+qtyout23)qtyout25,qtyout26,qtyout27,qtyout28,(qtyout26+qtyout27+qtyout28)qtyout30, " +
                "	(select isnull(sum(qtyout),0) from tmpAdjustBP_out where ukuran=X1.ukuran and bln=X1.bln )qtyout31 from( " +
                "		select ukuran,bln,isnull((select qtysa from tmpProsesBP_SA where ukuran=X.ukuran and bln=X.bln ),0) SA,isnull(qtyin1,0)qtyin1,isnull(qtyin2,0)qtyin2,isnull(qtyin3,0)qtyin3,  " +
                "		isnull(qtyin4,0)qtyin4,0 qtyin5,isnull(qtyout6,0)qtyout6,isnull(qtyout7,0)qtyout7,isnull(qtyout8,0)qtyout8,isnull(qtyout10,0)qtyout10,isnull(qtyout11,0)qtyout11,isnull(qtyout12,0)qtyout12, " +
                "		isnull(qtyout13,0)qtyout13,isnull(qtyout15,0)qtyout15,isnull(qtyout16,0)qtyout16,isnull(qtyout17,0)qtyout17,isnull(qtyout18,0)qtyout18,isnull(qtyout20,0)qtyout20, " +
                "		isnull(qtyout21,0)qtyout21,isnull(qtyout22,0)qtyout22,isnull(qtyout23,0)qtyout23,isnull(qtyout25,0)qtyout25,isnull(qtyout26,0)qtyout26,isnull(qtyout27,0)qtyout27, " +
                "		isnull(qtyout28,0)qtyout28,isnull(qtyout30,0) qtyout30 from ( " +
                "			select * from ( " +
                "				select ukuran,bln,0 SA,sum(qtyin1) qtyin1,sum(qtyin2) qtyin2,sum(qtyin3) qtyin3,sum(qtyin4) qtyin4 from ( " +
                "				select ukuran,bln,0 SA,sum(qtyin) qtyin1,0 qtyin2,0 qtyin3,0 qtyin4 from tmpProsesBP_in where Process ='direct' group by ukuran,bln  " +
                "				union all " +
                "				select ukuran,bln,0 SA,0 qtyin1,sum(qtyin) qtyin2,0 qtyin3,0 qtyin4 from tmpProsesBP_in where Process <>'direct'  " +
                "				and (Keterangan like '%-3-%' or Keterangan like '%-M-%')  group by ukuran,bln  " +
                "				union all " +
                "				select ukuran,bln,0 SA,0 qtyin1,0 qtyin2,sum(qtyin) qtyin3,0 qtyin4 from tmpProsesBP_in where Process <>'direct'  " +
                "				and Keterangan like '%-P-%' group by ukuran,bln )a group by ukuran,bln)A1 " +
                "			left join( " +
                "			select ukuran ukuran1,bln bln1,sum(qtyout6) qtyout6,sum(qtyout7) qtyout7,sum(qtyout8) qtyout8,sum(qtyout9) qtyout9,sum(qtyout10) qtyout10 from ( " +
                "				select ukuran,bln,sum(qtyout) qtyout6,0 qtyout7,0 qtyout8,0 qtyout9,0 qtyout10 from tmpProsesBP_out where Process <> 'direct' and keterangan like '%12002400%'  " +
                "				and (Keterangan like '%-3-%' or Keterangan like '%-M-%') group by ukuran,bln  " +
                "				union all " +
                "				select ukuran,bln,0 qtyout6,sum(qtyout) qtyout7,0 qtyout8,0 qtyout9,0 qtyout10 from tmpProsesBP_out where Process <>'direct' and keterangan like '%12002400%'  " +
                "				and Keterangan like '%-P-%' group by ukuran,bln  " +
                "				union all " +
                "				select ukuran,bln,0 qtyout6,0 qtyout7,sum(qtyout) qtyout8,0 qtyout9,0 qtyout10 from tmpProsesBP_out where Process <>'direct' and keterangan like '%12002400%'  " +
                "				and Keterangan like '%-S-%'  group by ukuran,bln  " +
                "			)a group by ukuran,bln)A2 on A1.ukuran=A2.ukuran1 and A1.bln=A2.bln1  " +
                "			left join ( " +
                "				select ukuran ukuran2,bln bln2,sum(qtyout11) qtyout11,sum(qtyout12) qtyout12,sum(qtyout13) qtyout13,sum(qtyout14) qtyout14,sum(qtyout15) qtyout15 from ( " +
                "				select ukuran,bln,sum(qtyout) qtyout11,0 qtyout12,0 qtyout13,0 qtyout14,0 qtyout15 from tmpProsesBP_out where Process <> 'direct' and keterangan like '%10001000%'  " +
                "				and (Keterangan like '%-3-%' or Keterangan like '%-M-%') group by ukuran,bln  " +
                "				union all " +
                "				select ukuran,bln,0 qtyout11,sum(qtyout) qtyout12,0 qtyout13,0 qtyout14,0 qtyout15 from tmpProsesBP_out where Process <>'direct' and keterangan like '%10001000%'  " +
                "				and Keterangan like '%-P-%' group by ukuran,bln  " +
                "				union all " +
                "				select ukuran,bln,0 qtyout11,0 qtyout12,sum(qtyout) qtyout13,0 qtyout14,0 qtyout15 from tmpProsesBP_out where Process <>'direct' and keterangan like '%10001000%'  " +
                "				and Keterangan like '%-S-%'   group by ukuran,bln)a  " +
                "				group by ukuran,bln " +
                "			)A3 on A1.ukuran=A3.ukuran2 and A1.bln=A3.bln2  " +
                "			left join( " +
                "				select ukuran ukuran3,bln bln3,sum(qtyout16) qtyout16,sum(qtyout17) qtyout17,sum(qtyout18) qtyout18,sum(qtyout19) qtyout19,sum(qtyout20) qtyout20 from ( " +
                "				select ukuran,bln,sum(qtyout) qtyout16,0 qtyout17,0 qtyout18,0 qtyout19,0 qtyout20 from tmpProsesBP_out where Process <> 'direct' and keterangan like '%10002000%'  " +
                "				and (Keterangan like '%-3-%' or Keterangan like '%-M-%') group by ukuran,bln  " +
                "				union all " +
                "				select ukuran,bln,0 qtyout16,sum(qtyout) qtyout17,0 qtyout18,0 qtyout19,0 qtyout20 from tmpProsesBP_out where Process <>'direct' and keterangan like '%10002000%'  " +
                "				and Keterangan like '%-P-%' group by ukuran,bln  " +
                "				union all " +
                "				select ukuran,bln,0 qtyout16,0 qtyout17,sum(qtyout) qtyout18,0 qtyout19,0 qtyout20 from tmpProsesBP_out where Process <>'direct' and keterangan like '%10002000%'  " +
                "				and Keterangan like '%-S-%'  group by ukuran,bln)a  " +
                "				group by ukuran,bln " +
                "			)A4 on A1.ukuran=A4.ukuran3 and A1.bln=A4.bln3 " +
                "			left join( " +
                "				select ukuran ukuran4,bln bln4,sum(qtyout21) qtyout21,sum(qtyout22) qtyout22,sum(qtyout23) qtyout23,sum(qtyout24) qtyout24,sum(qtyout25) qtyout25 from ( " +
                "				select ukuran,bln,sum(qtyout) qtyout21,0 qtyout22,0 qtyout23,0 qtyout24,0 qtyout25 from tmpProsesBP_out where Process <> 'direct' and keterangan like '%6002400%'  " +
                "				and (Keterangan like '%-3-%' or Keterangan like '%-M-%') group by ukuran,bln  " +
                "				union all " +
                "				select ukuran,bln,0 qtyout21,sum(qtyout) qtyout22,0 qtyout23,0 qtyout24,0 qtyout25 from tmpProsesBP_out where Process <>'direct' and keterangan like '%6002400%'  " +
                "				and Keterangan like '%-P-%' group by ukuran,bln  " +
                "				union all " +
                "				select ukuran,bln,0 qtyout21,0 qtyout22,sum(qtyout) qtyout23,0 qtyout24,0 qtyout25 from tmpProsesBP_out where Process <>'direct' and keterangan like '%6002400%'  " +
                "				and Keterangan like '%-S-%'  group by ukuran,bln)a  " +
                "				group by ukuran,bln " +
                "			)A5 on A1.ukuran=A5.ukuran4 and A1.bln=A5.bln4  " +
                "			left join( " +
                "				select ukuran ukuran5,bln bln5,sum(qtyout26) qtyout26,sum(qtyout27) qtyout27,sum(qtyout28) qtyout28,sum(qtyout29) qtyout29,sum(qtyout30) qtyout30 from ( " +
                "				select ukuran,bln,sum(qtyout) qtyout26,0 qtyout27,0 qtyout28,0 qtyout29,0 qtyout30 from tmpProsesBP_out where Process <> 'direct'  " +
                "				and keterangan not like '%12002400%' and keterangan not like '%10001000%'and keterangan not like '%10002000%'  and keterangan not like '%6002400%'  " +
                "				and (Keterangan like '%-3-%' or Keterangan like '%-M-%') group by ukuran,bln  " +
                "				union all " +
                "				select ukuran,bln,0 qtyout26,sum(qtyout) qtyout27,0 qtyout28,0 qtyout29,0 qtyout30 from tmpProsesBP_out where Process <>'direct'  " +
                "				and keterangan not like '%12002400%' and keterangan not like '%10001000%'and keterangan not like '%10002000%'  and keterangan not like '%6002400%'  " +
                "				and Keterangan like '%-P-%' group by ukuran,bln  " +
                "				union all " +
                "				select ukuran,bln,0 qtyout26,0 qtyout27,sum(qtyout) qtyout28,0 qtyout29,0 qtyout30 from tmpProsesBP_out where Process <>'direct'  " +
                "				and keterangan not like '%12002400%' and keterangan not like '%10001000%'and keterangan not like '%10002000%'  and keterangan not like '%6002400%'  " +
                "				and Keterangan like '%-S-%'  group by ukuran,bln)a  " +
                "				group by ukuran,bln " +
                "			)A6 on A1.ukuran=A6.ukuran5 and A1.bln=A6.bln5  " +
                "		)X  " +
                "	)X1  " +
                ")X2 order by  ukuran,bln " +
                " " +
                "select distinct ukuran from DataJadiprosesbp ";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new UProsesBP
                    {
                        Ukuran =  sdr["ukuran"].ToString()

                    });
                }
            }
            lstDept.DataSource = arrData;
            lstDept.DataBind();
        }
        protected void lstDept_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ArrayList arrData = new ArrayList();
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                UProsesBP p = (UProsesBP)e.Item.DataItem;
                string ukuran = p.Ukuran;
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "select * from DataJadiprosesbp where ukuran='" + ukuran + "'";
                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new DProsesBP
                        {
                            Bln = sdr["bln"].ToString(),
                            Bln1 = sdr["bln1"].ToString(),
                            SA = Convert.ToInt32(sdr["SA"].ToString()),
                            qtyin1 = Convert.ToInt32(sdr["qtyin1"].ToString()),
                            qtyin2 = Convert.ToInt32(sdr["qtyin2"].ToString()),
                            qtyin3 = Convert.ToInt32(sdr["qtyin3"].ToString()),
                            qtyin4 = Convert.ToInt32(sdr["qtyin4"].ToString()),
                            qtyin5 = Convert.ToInt32(sdr["qtyin5"].ToString()),
                            qtyout6 = Convert.ToInt32(sdr["qtyout6"].ToString()),
                            qtyout7 = Convert.ToInt32(sdr["qtyout7"].ToString()),
                            qtyout8 = Convert.ToInt32(sdr["qtyout8"].ToString()),
                            qtyout10 = Convert.ToInt32(sdr["qtyout10"].ToString()),
                            qtyout11 = Convert.ToInt32(sdr["qtyout11"].ToString()),
                            qtyout12 = Convert.ToInt32(sdr["qtyout12"].ToString()),
                            qtyout13 = Convert.ToInt32(sdr["qtyout13"].ToString()),
                            qtyout15 = Convert.ToInt32(sdr["qtyout15"].ToString()),
                            qtyout16 = Convert.ToInt32(sdr["qtyout16"].ToString()),
                            qtyout17 = Convert.ToInt32(sdr["qtyout17"].ToString()),
                            qtyout18 = Convert.ToInt32(sdr["qtyout18"].ToString()),
                            qtyout20 = Convert.ToInt32(sdr["qtyout20"].ToString()),
                            qtyout21 = Convert.ToInt32(sdr["qtyout21"].ToString()),
                            qtyout22 = Convert.ToInt32(sdr["qtyout22"].ToString()),
                            qtyout23 = Convert.ToInt32(sdr["qtyout23"].ToString()),
                            qtyout25 = Convert.ToInt32(sdr["qtyout25"].ToString()),
                            qtyout26 = Convert.ToInt32(sdr["qtyout26"].ToString()),
                            qtyout27 = Convert.ToInt32(sdr["qtyout27"].ToString()),
                            qtyout28 = Convert.ToInt32(sdr["qtyout28"].ToString()),
                            qtyout30 = Convert.ToInt32(sdr["qtyout30"].ToString()),
                            qtyout31 = Convert.ToInt32(sdr["qtyout31"].ToString()),
                            qtyout32 = Convert.ToInt32(sdr["qtyout32"].ToString()),
                            saldo = Convert.ToInt32(sdr["saldo"].ToString())
                            });
                    }
                }
            }
            Repeater lstPES = (Repeater)e.Item.FindControl("lstPES");
            lstPES.DataSource = arrData;
            lstPES.DataBind();
        }
    }
}

public class UProsesBP : GRCBaseDomain
{
    public string Ukuran { get; set; }
}
    public class DProsesBP : GRCBaseDomain
{
    public string Bln { get; set; }
    public string Bln1 { get; set; }
    public int SA { get; set; }
    public int qtyin1 { get; set; }
    public int qtyin2 { get; set; }
    public int qtyin3 { get; set; }
    public int qtyin4 { get; set; }
    public int qtyin5 { get; set; }
    public int qtyout6 { get; set; }
    public int qtyout7 { get; set; }
    public int qtyout8 { get; set; }
    public int qtyout10{ get; set; }
    public int qtyout11{ get; set; }
    public int qtyout12{ get; set; }
    public int qtyout13{ get; set; }
    public int qtyout15{ get; set; }
    public int qtyout16{ get; set; }
    public int qtyout17{ get; set; }
    public int qtyout18{ get; set; }
    public int qtyout20{ get; set; }
    public int qtyout21{ get; set; }
    public int qtyout22{ get; set; }
    public int qtyout23{ get; set; }
    public int qtyout25{ get; set; }
    public int qtyout26{ get; set; }
    public int qtyout27{ get; set; }
    public int qtyout28{ get; set; }
    public int qtyout30{ get; set; }
    public int qtyout31{ get; set; }
    public int qtyout32{ get; set; }
    public int saldo { get; set; }
}