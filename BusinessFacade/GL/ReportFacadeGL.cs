using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessFacade
{
    public class ReportFacadeGL
    {
        public string ListCOA()
        {
            return "select Postable,[Group],[level], ChartNo as [Chart No],Parent,ChartName as [Chart Name],CCYCode as [CCY Code],IsDept,IsCost,ChartType as [Chart Type]  from GL_ChartOfAccount where RowStatus > -1 order by Chartno";
        }

        public string MonthlyBalanceSheet(string periode)
        {
            return "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[neraca]') AND type in (N'U')) DROP TABLE [dbo].neraca " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[neracaT]') AND type in (N'U')) DROP TABLE [dbo].neracaT " +
                "select * into neraca from (select A.[NotesNo] ,A.chartno As chart, " +
                "rtrim(A.ChartNo) as Chartno, A.[Postable], (space(A.[level] *4)) +A.Chartname as Chartname ,A.[Level], A.[Group], A.[ccycode],  " +
                "cast((B.[BegBal] + B.[DebitTrans] + B.[CreditTrans])as  decimal(18,0))  as Amount " +
                "FROM GL_ChartOfAccount A INNER JOIN GL_ChartBal B ON A.[ChartNo] = B.[ChartNo]  " +
                "WHERE B.[Period] = '" + periode + "' AND A.[Level] <= 2 and [Group] <4 AND A.RowStatus > -1 " +
                "union " +
                "select A.[NotesNo] ,A.chartno As chart, " +
                "RTRIM(A.ChartNo)+'zz' as Chartno, A.[Postable], (space(A.[level] *4)) +A.Chartname as Chartname ,A.[Level], A.[Group], A.[ccycode],  " +
                "cast((B.[BegBal] + B.[DebitTrans] + B.[CreditTrans])as  decimal(18,0))  as Amount " +
                "FROM GL_ChartOfAccount A INNER JOIN GL_ChartBal B ON A.[ChartNo] = B.[ChartNo]  " +
                "WHERE B.[Period] = '" + periode + "' AND A.[Level] <= 2 and [Group] <4 and len(rtrim(A.ChartNo))=2 and A.Postable=0 AND A.RowStatus > -1 " +
                "union " +
                "select A.[NotesNo] ,A.chartno As chart, " +
                "RTRIM(A.ChartNo)+'zzzz' as Chartno, A.[Postable], (space(A.[level] *4)) +A.Chartname as Chartname ,A.[Level], A.[Group], A.[ccycode],  " +
                "cast((B.[BegBal] + B.[DebitTrans] + B.[CreditTrans])as  decimal(18,0))as Amount " +
                "FROM GL_ChartOfAccount A INNER JOIN GL_ChartBal B ON A.[ChartNo] = B.[ChartNo]  " +
                "WHERE B.[Period] = '" + periode + "' AND A.[Level] <= 2 and [Group] <4 and len(rtrim(A.ChartNo))=1 and left(A.[ChartNo],1)<> '1%' AND A.RowStatus > -1) as n order by Chartno  " +
                "update neraca set amount=amount*-1 where chartno not like '1%' " +
                "declare @garis varchar(100)  " +
                "select @garis = '--------------------------' " +
                "create table neracaT([No] int ,Name1 varchar(200),NoteNo1 varchar(200),Amount1 varchar(200),Name2 varchar(200),NoteNo2 varchar(200),Amount2 varchar(200)) " +
                "declare @nomor int select @nomor =1 " +
                "declare @nomor1 int select @nomor1 =1 " +
                "declare @Name1 varchar(200) " +
                "declare @NoteNo1 varchar(200) " +
                "declare @Amount1 varchar(200) " +
                "declare @Name2 varchar(200) " +
                "declare @NoteNo2 varchar(200) " +
                "declare @Amount2 varchar(200) " +
                "declare @ChartNo varchar(15) " +
                "declare @ChartName varchar(200) " +
                "declare @Amount  decimal(18,0) " +
                "declare @postable int " +
                "declare @level int " +
                "declare @i int " +
                "declare kursor cursor for " +
                "select Chartno,chartname,amount,postable,[level] from neraca where ltrim(chartno) like '1%' order by chartno " +
                "open kursor " +
                "FETCH NEXT FROM kursor " +
                "INTO @ChartNo,@ChartName,@Amount,@postable,@level " +
                "WHILE @@FETCH_STATUS = 0 " +
	            "    begin " +
	            "    if RIGHT(rtrim(@ChartNo),2)='zz'  " +
		        "        begin " +
			    "            select @Name1='' select @Amount1=@garis  " +
			    "            if (select COUNT(*) from neracaT where [no]=@nomor)=0 " +
				"                begin insert into neracaT ([No],Name1,NoteNo1,Amount1) values(@nomor,@Name1,@NoteNo1,@Amount1) end " +
			    "            else " +
				"                begin update neracaT set Name1=@Name1,Amount1=@Amount1 where [no]=@Nomor end " +
				"                select @nomor=@nomor+1 " +
			    "            select @Name1=(space(@level *4)) + 'TOTAL ' +@ChartName select @Amount1=CAST(@Amount as varchar(200))  " +
			    "            if (select COUNT(*) from neracaT where [no]=@nomor )=0 " +
				"                begin insert into neracaT ([No],Name1,NoteNo1,Amount1) values(@nomor,@Name1,@NoteNo1,@Amount1) end " +
			    "            else " +
				"                begin update neracaT set Name1=@Name1,Amount1=@Amount1 where [no]=@Nomor end " +
				"                select @nomor=@nomor+1 " +
		        "        end " +
	            "    else " +
		        "        begin " +
		        "        if len(rtrim(@ChartNo))>2 " +
			    "            begin " +
			    "            select @Name1=@ChartName select @Amount1=CAST(@Amount as varchar(200)) " +
			    "            if (select COUNT(*) from neracaT where [no]=@nomor )=0 " +
				"                begin insert into neracaT ([No],Name1,NoteNo1,Amount1) values(@nomor,@Name1,@NoteNo1,@Amount1) end " +
			    "            else " +
				"                begin update neracaT set Name1=@Name1,Amount1=@Amount1 where [No]=@nomor end " +
				"                select @nomor=@nomor+1 " +
			    "            end " +
		        "        else " +
			    "            begin " +
			    "            select @Name1=@ChartName select @Amount1='' " +
			    "            if (select COUNT(*) from neracaT where [no]=@nomor )=0 " +
				"                begin insert into neracaT ([No],Name1,NoteNo1,Amount1) values(@nomor,@Name1,@NoteNo1,@Amount1) end " +
			    "            else " +
				"                begin update neracaT set Name1=@Name1,Amount1=@Amount1 where [no]=@Nomor end " +
			    "            select @nomor=@nomor+1 " +
			    "            end " +
		        "        end " +
	            "    FETCH NEXT FROM kursor " +
	            "    INTO @ChartNo,@ChartName,@Amount,@postable,@level " +
	            "    end " +
                "CLOSE kursor " +
                "DEALLOCATE kursor " +
                "declare kursor cursor for " +
                "select Chartno,chartname,amount,postable,[level] from neraca where ltrim(chartno) not like '1%' order by chartno " +
                "open kursor " +
                "FETCH NEXT FROM kursor " +
                "INTO @ChartNo,@ChartName,@Amount,@postable,@level " +
                "WHILE @@FETCH_STATUS = 0 " +
	            "    begin " +
	            "    if RIGHT(rtrim(@ChartNo),2)='zz'  " +
		        "        begin " +
			    "             select @Name2=(space(@level *4)) + 'TOTAL ' +@ChartName select @Amount2=CAST(@Amount as varchar(200))  " +
			    "             if (select COUNT(*) from neracaT where [no]=@nomor1 )=0 " +
			    "	                begin insert into neracaT ([No],Name2,NoteNo2,Amount2) values(@nomor1,@Name2,@NoteNo2,@Amount2) end " +
			    "            else " +
				"                begin update neracaT set Name2=@Name2,Amount2=@Amount2 where [no]=@nomor1 end " +
				"                select @nomor1=@nomor1+1 " +
			    "           select @Name2='' select @Amount2='' " +
			    "            if (select COUNT(*) from neracaT where [no]=@nomor1 )=0 " +
				"                begin insert into neracaT ([No],Name2,NoteNo2,Amount2) values(@nomor1,@Name2,@NoteNo2,@Amount2) end " +
			    "            else " +
				"                update neracaT set Name2=@Name2,Amount2=@Amount2 where [No]=@nomor1 " +
		        "        end " +
	            "    else " +
		        "        begin " +
		        "        if len(rtrim(@ChartNo))>2 " +
			    "            begin " +
			    "            select @Name2=@ChartName select @Amount2=CAST(@Amount as varchar(200)) " +
			    "            if (select COUNT(*) from neracaT where [no]=@nomor1 )=0 " +
				"                begin insert into neracaT ([No],Name2,NoteNo2,Amount2) values(@nomor1,@Name2,@NoteNo2,@Amount2) end " +
			    "            else " +
				"                begin update neracaT set Name2=@Name2,Amount2=@Amount2 where [No]=@nomor1 end " +
				"                select @nomor1=@nomor1+1 " +
			    "            end " +
		        "        else " +
			    "            begin " +
			    "            select @Name2=@ChartName select @Amount2='' " +
			    "            if (select COUNT(*) from neracaT where [no]=@nomor1 )=0 " +
				"                begin insert into neracaT ([No],Name2,NoteNo2,Amount2) values(@nomor1,@Name2,@NoteNo2,@Amount2) end " +
			    "            else " +
				"                begin update neracaT set Name2=@Name2,Amount2=@Amount2 where [no]=@nomor1 end " +
			    "            select @nomor1=@nomor1+1 " +
			    "            end " +
		        "        end " +
	            "    FETCH NEXT FROM kursor " +
	            "    INTO @ChartNo,@ChartName,@Amount,@postable,@level " +
	            "    end " +
                "CLOSE kursor " +
                "DEALLOCATE kursor " +
                "select * from neracaT order by [No] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[neraca]') AND type in (N'U')) DROP TABLE [dbo].neraca " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[neracaT]') AND type in (N'U')) DROP TABLE [dbo].neracaT" ;
        }
        public string JournalReport(string periode, string companyCode)
        {
            return "select A.VoucherCode,A.JurNo,A.JurDate,B.ChartNo,B.[Description],B.IDRAmount , "+   //C.ChartName, " +
                "case when TypeTRX='BELI' and IDRAmount>0 then (select Uraian from BankOutDetail where BankOutDetail.ID = B.IDTableName) else (select Up+', '+KodeBukti+BankOutNo from BankOut where BankOut.ID = B.IDTableName) end ChartName "+
                "from GL_JurHead A inner join GL_JurDet B on A.JurHeadNum=B.JurHeadNum inner join  GL_ChartOfAccount C on B.ChartNo=C.ChartNo and c.CompanyCode=a.CompanyCode " +
                "where A.RowStatus>-1 and B.RowStatus>-1 and A.Period='" + periode + "' and A.CompanyCode='"+companyCode+ "'  and c.RowStatus > -1";
        }
        public string TrialBalance(string TahunBulan, string companyCode)
        {
            return "select * from GL_ChartBal as a inner join GL_ChartOfAccount as b on b.ChartNo = a.ChartNo and b.RowStatus > -1 and b.CompanyCode=a.CompanyCode  where period = '"+TahunBulan+
                "' and a.RowStatus > -1 and (DebitTrans!=0 or CreditTrans!=0) and a.CompanyCode='" + companyCode+ "' and b.ChartNo not in "+
                //"' and a.RowStatus > -1 and (BegBal + DebitTrans + CreditTrans) != 0 and a.CompanyCode='" + companyCode + "' and b.ChartNo not in " +
                " (select CharValue from GL_Parameter where ParamCode='LR') and b.Postable=1 AND B.RowStatus > -1 order by a.ChartNo";
                //" (select CharValue from GL_Parameter where ParamCode='LR')  order by a.ChartNo";
        }
        public string YearlyTrialBalance(string Tahun)
        {
            return " select *,case when [group]>3 then isnull((select SUM(debittrans+credittrans) from GL_ChartBal where chartno=A.chartno and period= A.Tahun +'01'),0) else " +
                "isnull((select SUM(begbal+debittrans+credittrans) from GL_ChartBal where chartno=A.chartno and period=Tahun +'01'),0) end Jan, " +
                "case when [group]>3 then isnull((select SUM(debittrans+credittrans) from GL_ChartBal where chartno=A.chartno and period= A.Tahun +'02'),0) else  " +
                "isnull((select SUM(begbal+debittrans+credittrans) from GL_ChartBal where chartno=A.chartno and period=Tahun +'02'),0) end Feb, " +
                "case when [group]>3 then isnull((select SUM(debittrans+credittrans) from GL_ChartBal where chartno=A.chartno and period= A.Tahun +'03'),0) else  " +
                "isnull((select SUM(begbal+debittrans+credittrans) from GL_ChartBal where chartno=A.chartno and period=Tahun +'03'),0) end Mar, " +
                "case when [group]>3 then isnull((select SUM(debittrans+credittrans) from GL_ChartBal where chartno=A.chartno and period= A.Tahun +'04'),0) else  " +
                "isnull((select SUM(begbal+debittrans+credittrans) from GL_ChartBal where chartno=A.chartno and period=Tahun +'04'),0) end Apr, " +
                "case when [group]>3 then isnull((select SUM(debittrans+credittrans) from GL_ChartBal where chartno=A.chartno and period= A.Tahun +'05'),0) else  " +
                "isnull((select SUM(begbal+debittrans+credittrans) from GL_ChartBal where chartno=A.chartno and period=Tahun +'05'),0) end Mei, " +
                "case when [group]>3 then isnull((select SUM(debittrans+credittrans) from GL_ChartBal where chartno=A.chartno and period= A.Tahun +'06'),0) else  " +
                "isnull((select SUM(begbal+debittrans+credittrans) from GL_ChartBal where chartno=A.chartno and period=Tahun +'06'),0) end Jun, " +
                "case when [group]>3 then isnull((select SUM(debittrans+credittrans) from GL_ChartBal where chartno=A.chartno and period= A.Tahun +'07'),0) else  " +
                "isnull((select SUM(begbal+debittrans+credittrans) from GL_ChartBal where chartno=A.chartno and period=Tahun +'07'),0) end Jul, " +
                "case when [group]>3 then isnull((select SUM(debittrans+credittrans) from GL_ChartBal where chartno=A.chartno and period= A.Tahun +'08'),0) else  " +
                "isnull((select SUM(begbal+debittrans+credittrans) from GL_ChartBal where chartno=A.chartno and period=Tahun +'08'),0) end Agu, " +
                "case when [group]>3 then isnull((select SUM(debittrans+credittrans) from GL_ChartBal where chartno=A.chartno and period= A.Tahun +'09'),0) else  " +
                "isnull((select SUM(begbal+debittrans+credittrans) from GL_ChartBal where chartno=A.chartno and period=Tahun +'09'),0) end Sep, " +
                "case when [group]>3 then isnull((select SUM(debittrans+credittrans) from GL_ChartBal where chartno=A.chartno and period= A.Tahun +'10'),0) else  " +
                "isnull((select SUM(begbal+debittrans+credittrans) from GL_ChartBal where chartno=A.chartno and period=Tahun +'10'),0) end Okt, " +
                "case when [group]>3 then isnull((select SUM(debittrans+credittrans) from GL_ChartBal where chartno=A.chartno and period= A.Tahun +'11'),0) else  " +
                "isnull((select SUM(begbal+debittrans+credittrans) from GL_ChartBal where chartno=A.chartno and period=Tahun +'11'),0) end Nop, " +
                "case when [group]>3 then isnull((select SUM(debittrans+credittrans) from GL_ChartBal where chartno=A.chartno and period= A.Tahun +'12'),0) else  " +
                "isnull((select SUM(begbal+debittrans+credittrans) from GL_ChartBal where chartno=A.chartno and period=Tahun +'12'),0) end [Des], " +
                "case when [group]>3 then isnull((select SUM(debittrans+credittrans) from GL_ChartBal where chartno=A.chartno and period like A.Tahun + '%'),0) else  " +
                "isnull((select SUM(begbal+debittrans+credittrans) from GL_ChartBal where chartno=A.chartno and period like A.Tahun + '%'),0) end Total " +
                "from  (select '" + Tahun + "' as Tahun,[Group],ChartNo,Parent,ChartName  from GL_ChartOfAccount where RowStatus > -1 and Postable=0 and ChartNo  " +
                "not in (select CharValue from GL_Parameter where ParamCode='LR')) as A order by ChartNo ";
        }
        public string GeneralLedger1(string chart1, string chart2, string tgl1, string tgl2, string prdawal, string companyCode)
        {
            string chart = string.Empty;
            if (chart1.Trim().ToUpper() != "0" || chart1.Trim().ToUpper() != "0")
                chart = " C.ChartNo>='" + chart1 + "' and C.ChartNo <='" + chart2 + "' ";
            else
                chart = " C.ChartNo<> '' ";

            return "select * from ( "+
                "select 0 as ID, null as JurDate,'' as VoucherCode, '' as JurNo,'Saldo Awal' as Description, a.ChartNo, c.ChartName,c.CCYCode,period,BegBal as Saldo,0 as Debit, 0 as Credit,'' as Parent, 0 as Posttable, '' as Company, 0 as Level, 0 as Urutan " +
                "from GL_ChartBal as a,GL_ChartOfAccount as c  where c.RowStatus > -1 and a.period = '" + prdawal+"' and a.CompanyCode = '" + companyCode + "' and a.CompanyCode=c.CompanyCode and a.ChartNo = c.ChartNo and a.RowStatus > -1 and c.RowStatus > -1 and + " + chart + " and a.ChartNo in (select x.ChartNo from GL_JurHead as z,GL_JurDet as x where z.JurHeadNum = x.JurHeadNum and CONVERT(char, z.JurDate, 112)>= '" + tgl1 + "' and CONVERT(char, z.JurDate, 112)<= '" + tgl2 + "' and z.CompanyCode = '" + companyCode + "' and z.RowStatus > -1 and x.RowStatus > -1  ) " +
                "union all " +
                "select 0 as ID, null as JurDate,'' as VoucherCode, '' as JurNo,'Saldo Awal' as Description,  a.ChartNo, c.ChartName,c.CCYCode,period,BegBal as Saldo,0 as Debit, 0 as Credit,'' as Parent, 0 as Posttable, '' as Company, 0 as Level, 0 as Urutan " +
                "from GL_ChartBal as a,GL_ChartOfAccount as c where c.RowStatus > -1 and a.period = '" + prdawal + "' and a.CompanyCode = '" + companyCode + "' and a.CompanyCode=c.CompanyCode and a.ChartNo = c.ChartNo and a.RowStatus > -1 and c.RowStatus > -1 and + " + chart + " and a.ChartNo not in (select x.ChartNo from GL_JurHead as z,GL_JurDet as x where z.JurHeadNum = x.JurHeadNum and CONVERT(char, z.JurDate, 112)>= '" + tgl1 + "' and CONVERT(char, z.JurDate, 112)<= '" + tgl2 + "' and z.CompanyCode = '" + companyCode + "' and z.RowStatus > -1 and z.RowStatus > -1) and BegBal!= 0 " +
                "union all " +
                "select a.JurDetNum as ID,b.JurDate,b.VoucherCode,b.JurNo,a.Description,a.ChartNo,c.ChartName,c.CCYCode,b.period, 0 as Saldo,case when IDRAmount >= 0 then IDRAmount else 0 end Debit,case when IDRAmount < 0 then IDRAmount else 0 end Credit, c.Parent,c.Postable,b.CompanyCode,c.Level,1 as Urutan from GL_JurDet as a inner join GL_JurHead as b on a.jurheadnum = b.jurheadnum and a.RowStatus > -1 " +
                "inner join GL_ChartOfAccount as c on c.ChartNo = a.ChartNo and c.RowStatus > -1 and b.CompanyCode=c.CompanyCode and + " + chart +
                "where CONVERT(char, b.JurDate, 112)>= '"+tgl1+ "' and CONVERT(char, b.JurDate, 112)<= '" + tgl2 + "' and b.CompanyCode = '" + companyCode+"' and b.RowStatus > -1 and a.RowStatus > -1 ) as ab  order by ChartNo,Urutan,JurDate,VoucherCode,JurNo";
        }
        public string GeneralLedger(string chart1, string chart2, string tgl1, string tgl2, string prdawal, string companyCode)
        {
            string chart = string.Empty;
            if (chart1.Trim().ToUpper() != "0" || chart1.Trim().ToUpper() != "0")
                chart = " C.ChartNo>='" + chart1 + "' and C.ChartNo <='" + chart2 + "' ";
            else
                chart = " C.ChartNo<> ''";
            return "select * from ( " +
                "select 0 as ID,null as JurDate, null as VoucherCode,null as JurNo,C.ChartNo, C.ChartName,'Saldo Awal' as [Description], '' as CCYCode, 0 as CCYAmount, " +
                "0 as Debit,0 as Credit,isnull((select begbal from GL_ChartBal where period='" + prdawal + "'  and ChartNo=C.ChartNo),0) as saldo " +
                "from  GL_ChartOfAccount C where c.RowStatus > -1 and " + chart +
                "union all " +
                "select B.JurDetNum as ID,A.JurDate, A.VoucherCode,A.JurNo,B.ChartNo, C.ChartName,B.[Description], B.CCYCode,B.CCYAmount, " +
                "case when B.IDRAmount>0 then  B.IDRAmount else 0 end Debit, case when B.IDRAmount<0 then  B.IDRAmount  else 0  end Credit, " +
                "(select isnull(SUM(idramount),0) from GL_JurDet where JurDetNum <=B.JurDetNum and ChartNo=B.ChartNo  )+ " +
                "(select begbal from GL_ChartBal where ChartNo =B.ChartNo and period =left(CONVERT(char, A.JurDate,112),6)) as saldo " +
                "from GL_JurHead A inner join GL_JurDet B on A.JurHeadNum=B.JurHeadNum inner join GL_ChartOfAccount C on B.ChartNo=C.ChartNo " +
                "where c.RowStatus > -1 and " + chart + "  and CONVERT(char,A.JurDate,112)>='" + tgl1 +
                "' and CONVERT(char,A.JurDate,112)<='" + tgl2 + "' and A.CompanyCode='"+companyCode+"' ) as L  " +
                "where ChartNo in (select ChartNo from GL_JurDet where JurHeadNum in (select JurHeadNum from GL_JurHead  " +
                "where CONVERT(char,JurDate,112)>='" + tgl1 + "' and CONVERT(char,JurDate,112)<='" + tgl2 + "' and CompanyCode='"+companyCode+"' )) order by ChartNo,JurDate ";

            //return "select * from ( " +
            //   "select 0 as ID,null as JurDate, null as VoucherCode,null as JurNo,C.ChartNo, C.ChartName,'Saldo Awal' as [Description], '' as CCYCode, 0 as CCYAmount, " +
            //   "0 as Debit,0 as Credit,isnull((select begbal from GL_ChartBal where period='" + prdawal + "'  and ChartNo=C.ChartNo),0) as saldo " +
            //   "from  GL_ChartOfAccount C where " + chart +
            //   "union all " +
            //   "select B.JurDetNum as ID,A.JurDate, A.VoucherCode,A.JurNo,B.ChartNo, C.ChartName,B.[Description], B.CCYCode,B.CCYAmount, " +
            //   "case when B.IDRAmount>0 then  B.IDRAmount else 0 end Debit, case when B.IDRAmount<0 then  B.IDRAmount  else 0  end Credit, " +
            //   "(select isnull(SUM(idramount),0) from GL_JurDet where JurDetNum <=B.JurDetNum and ChartNo=B.ChartNo  )+ " +
            //   "(select begbal from GL_ChartBal where ChartNo =B.ChartNo and period ='" + prdawal + "') as saldo " +
            //   "from GL_JurHead A inner join GL_JurDet B on A.JurHeadNum=B.JurHeadNum inner join GL_ChartOfAccount C on B.ChartNo=C.ChartNo " +
            //   "where" + chart + "  and CONVERT(char,A.JurDate,112)>='" + tgl1 +
            //   "' and CONVERT(char,A.JurDate,112)<='" + tgl2 + "' ) as L  " +
            //   "where ChartNo in (select ChartNo from GL_JurDet where JurHeadNum in (select JurHeadNum from GL_JurHead  " +
            //   "where CONVERT(char,JurDate,112)>='" + tgl1 + "' and CONVERT(char,JurDate,112)<='" + tgl2 + "' )) order by ChartNo,JurDate ";
        }
        public string RugiLaba(string period, string companyCode)
        {
            return "select ChartNoUrut,ChartNo as [Chart No],ChartName as [Chart Name], Amount as Amount1 "+
                "from(SELECT left(a.[ChartNo], 2) as ChartNoUrut, a.ChartNo, space(a.[Level] * 2) + a.[ChartName] as ChartName, a.[Level], a.[Group], a.Postable, a.[CCYCode], " +
                "case when a.ChartNo > '4' and a.ChartNo < '6' then " +
                "(select SUM(DebitTrans + CreditTrans) from GL_ChartBal as c where c.ChartNo = a.ChartNo and c.CompanyCode = '" + companyCode + "' and c.period = '"+period+"') " +
                "when a.ChartNo > '5' and a.ChartNo < '7' then " +
                "(select SUM(DebitTrans + CreditTrans) from GL_ChartBal as c where c.ChartNo = a.ChartNo and c.CompanyCode = '" + companyCode + "' and c.period = '" + period + "') " +
                "when a.ChartNo > '6' and a.ChartNo < '8' then " +
                "(select SUM(DebitTrans + CreditTrans) from GL_ChartBal as c where c.ChartNo = a.ChartNo and c.CompanyCode = '" + companyCode + "' and c.period = '" + period + "') " +
                "else 0 end Amount " +
                "FROM GL_ChartOfAccount as a WHERE a.RowStatus > -1 and a.[Group] >= 4  and a.[Level] <= 2 and a.CompanyCode = '" + companyCode + "' " +
                "union all " +
                "select * from(SELECT left(a.[ChartNo], 1) + 'Z' as ChartNoUrut, '' as ChartNo, space(a.[Level] * 2) + (select CharValue from GL_Parameter where ParamCode = a.ChartNo and CompanyCode = '"+companyCode+"') as ChartName, a.[Level], a.[Group], a.Postable, a.[CCYCode], " +
                "case when a.ChartNo > '3' and a.ChartNo < '6' then " +
                "(select SUM(DebitTrans + CreditTrans) from GL_ChartBal as c where c.ChartNo = a.ChartNo and c.CompanyCode = '" + companyCode + "' and c.period = '" + period + "') " +
                "when a.ChartNo > '5' and a.ChartNo < '7' then " +
                "(select SUM(DebitTrans + CreditTrans) from GL_ChartBal as c where c.ChartNo = a.ChartNo and c.CompanyCode = '" + companyCode + "' and c.period = '" + period + "') " +
                "when a.ChartNo > '6' and a.ChartNo < '8' then " +
                "(select SUM(DebitTrans + CreditTrans) from GL_ChartBal as c where c.ChartNo = a.ChartNo and c.CompanyCode = '" + companyCode + "' and c.period = '" + period + "') " +
                "else 0 end Amount " +
                "FROM GL_ChartOfAccount as a WHERE a.RowStatus > -1 and a.[Group] >= 4  and a.[Level] = 0 and a.CompanyCode = '" + companyCode+"') as a1 where ChartName is not null " +
                ") as aa order by ChartNoUrut";

        }
        public string RugiLabaOld(string prdawal)
        {
            return "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RugiLaba]') AND type in (N'U')) DROP TABLE [dbo].RugiLaba " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LRugiLaba]') AND type in (N'U')) DROP TABLE [dbo].LRugiLaba " +
                "select * into RugiLaba from ( " +
                "SELECT A.ID,A.period,A.ChartNo,space(B.[Level]*5) + B.ChartName  as ChartName, A.BegBal,A.DebitTrans,A.CreditTrans,A.DebitTrans+A.CreditTrans as Amount,B.CCYCode, " +
                "(select top 1 CCYRate from GL_CCYRate where rtrim(CCYCode)=rtrim(B.CCYCode) and left(convert(char,EffectiveDate,112),6)='"+prdawal+"' order by EffectiveDate desc )  " +
                "as CCYRate, B.Postable, B.[Level] " +
                "FROM GL_ChartBal A INNER JOIN GL_Chartofaccount B on A.[ChartNo] = B.[ChartNo] WHERE b.RowStatus > -1 and A.[Period] ='" + prdawal + "' AND B.[Group] > 3  ) as R order by ChartNo " +
                "declare @garis varchar(100)  " +
                "select @garis =  '..................' " +
                "create table LRugiLaba(ChartNo varchar(200),ChartNo1 varchar(200),ChartName varchar(200),Amount1 decimal(18,0),Amount2 decimal(18,0),Amount3 decimal(18,0), " +
                "Amount4 decimal(18,0),Amount5 decimal(18,0), CCYCode varchar(5)) " +
                "declare @ChartNo varchar(15) " +
                "declare @ChartName varchar(200) " +
                "declare @Amount  decimal(18,0) " +
                "declare @postable int " +
                "declare @level int " +
                "declare @CCYCode varchar(5) " +
                "declare @i int " +
                "declare @i1 int " +
                "declare @mlevel int " +
                "declare @pad varchar(5) " +
                "declare kursor cursor for " +
                "select Chartno,ChartName,amount,postable,[level] from RugiLaba  " +
                "select @mlevel = (select top 1 [level] from (select distinct [level] from RugiLaba)  as L order by [level] desc) " +
                "open kursor " +
                "FETCH NEXT FROM kursor " +
                "INTO @ChartNo,@ChartName,@Amount,@postable,@level " +
                "WHILE @@FETCH_STATUS = 0 " +
	            "    begin " +
	            "set @i=@mlevel-@level+1 " +
	            "if @i=@mlevel+1 begin " +
		        "    insert into LRugiLaba (ChartNo,ChartNo1,ChartName ,Amount1,Amount2,Amount3,Amount4,Amount5,CCYCode) " +
		        "    values( @ChartNo,@ChartNo,@ChartName,0,0,0,0,0,@CCYCode)  " +
	            "end " +
	            "if @i=5  " +
	            "begin  " +
	            "set @pad='zzzz'  " +
	            "insert into LRugiLaba (ChartNo,ChartNo1,ChartName ,Amount1,Amount2,Amount3,Amount4,Amount5,CCYCode) " +
		        "    values( @ChartNo+@pad,@ChartNo,@ChartName,0,0,0,0,@Amount,@CCYCode)  " +
	            "end   " +
	            "if @i=4 begin set @pad='zzz'  " +
	            "insert into LRugiLaba (ChartNo,ChartNo1,ChartName ,Amount1,Amount2,Amount3,Amount4,Amount5,CCYCode) " +
		        "    values( @ChartNo+@pad,@ChartNo,@ChartName,0,0,0,@Amount,0,@CCYCode)  " +
	            "end  " +
	            "if @i=3 begin set @pad='zz'  " +
	            "insert into LRugiLaba (ChartNo,ChartNo1,ChartName ,Amount1,Amount2,Amount3,Amount4,Amount5,CCYCode) " +
		        "    values( @ChartNo+@pad,@ChartNo,@ChartName,0,0,@Amount,0,0,@CCYCode)  " +
	            "end  " +
	            "if @i=2 begin set @pad='z'  " +
	            "insert into LRugiLaba (ChartNo,ChartNo1,ChartName ,Amount1,Amount2,Amount3,Amount4,Amount5,CCYCode) " +
		        "    values( @ChartNo+@pad,@ChartNo,@ChartName,0,@Amount,0,0,0,@CCYCode)  " +
	            "end  " +
	            "if @i=1 begin  " +
	            "insert into LRugiLaba (ChartNo,ChartNo1,ChartName ,Amount1,Amount2,Amount3,Amount4,Amount5,CCYCode) " +
		        "    values( @ChartNo,@ChartNo,@ChartName,@Amount,0,0,0,0,@CCYCode)  " +
	            "end  " +
	            "FETCH NEXT FROM kursor " +
                "INTO @ChartNo,@ChartName,@Amount,@postable,@level " +
	            "end " +
                "CLOSE kursor " +
                "DEALLOCATE kursor " +
                "select * from LRugiLaba order by ChartNo  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RugiLaba]') AND type in (N'U')) DROP TABLE [dbo].RugiLaba " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LRugiLaba]') AND type in (N'U')) DROP TABLE [dbo].LRugiLaba";
        }


    }

}
