using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using System.Collections;
using Domain;
using BusinessFacade;
using Factory;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Diagnostics;
using System.ComponentModel;
using Microsoft.Win32;
using System.Management;
using System.IO;

namespace GRCweb1.Modul.SarMut
{
    public partial class LapSmtPelarian : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                getYear();
                LoadUkuran();
                LoadGroup(ddlLine.SelectedItem.Text);
            }
        }
        private void getYear()
        {
            /**
             * Fill dropdown items with data tahun from database
             */
            ddTahun.Items.Clear();
            ArrayList arrTahun = new ArrayList();
            T3_MutasiWIPFacade T3_MutasiWIPFacade = new T3_MutasiWIPFacade();
            arrTahun = T3_MutasiWIPFacade.BM_Tahun();
            ddTahun.Items.Clear();
            ddTahun.Items.Add(new ListItem("-- Pilih Tahun --", "0"));
            foreach (T3_MutasiWIP bmTahun in arrTahun)
            {
                ddTahun.Items.Add(new ListItem(bmTahun.Tahune.ToString(), bmTahun.Tahune.ToString()));
            }
            ddTahun.SelectedValue = DateTime.Now.Year.ToString();
        }
        private void LoadGroup(string plant)
        {
            /**
             * Fill dropdown items with data tahun from database
             */
            ddlGroup.Items.Clear();
            ddlGroup.Items.Add(new ListItem("Pilih Group", "0"));
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;

            zl.CustomQuery = "select [group] from BM_PlantGroup where plantid=" + plant;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlGroup.Items.Add(new ListItem(sdr["group"].ToString(), sdr["group"].ToString()));
                }
            }
        }

        private void LoadUkuran()
        {
            /**
             * Fill dropdown items with data tahun from database
             */
            ddlUkuran.Items.Clear();
            ddlUkuran.Items.Add(new ListItem("Pilih Ukuran", "0"));
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;

            zl.CustomQuery = "declare @thnbln char(6) " +
                "declare @Line char(8)  " +
                "set @Line='Line " + ddlLine.SelectedItem.Text + "' " +
                "set @thnbln='" + ddTahun.SelectedValue + ddlBulan.SelectedValue.ToString().PadLeft(2, '0') + "'  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ItemLari]') AND type in (N'U')) DROP TABLE [dbo].ItemLari  " +
                "select distinct itemid0 into ItemLari from vw_kartustockwip K inner join BM_Destacking D on K.destid=D.ID  inner join BM_Plant P on D.PlantID =P.ID   " +
                "where left(convert(char,tanggal,112),6)=@thnbln and lokasi='p99'  " +
                "select distinct tebal,lebar,panjang, ukuran from (select I.Tebal,I.lebar,I.panjang,I.PartNo,P.PlantName,(sum(K.qty*-1*(I.tebal*I.lebar*I.Panjang)/1000000000)) qty,  " +
                "rtrim(cast(cast(I.Tebal as decimal(8,1)) as char)) + ' mm x ' + rtrim(cast(I.lebar as int)) + ' x ' + rtrim(cast(I.Panjang as int)) Ukuran  from vw_kartustockwip K    " +
                "inner join fc_items I on K.itemid0=I.id  inner join BM_Destacking D on K.destid=D.ID  inner join BM_Plant P on D.PlantID =P.ID   " +
                "where left(convert(char,tanggal,112),6)=@thnbln  and P.PlantName=@Line and K.qty<0 and lokasi<>'p99' and  " +
                "itemid0 in (select itemid0 from ItemLari) group by I.PartNo,P.PlantName,I.Tebal,I.lebar,I.Panjang)a order by tebal,lebar,panjang " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ItemLari]') AND type in (N'U')) DROP TABLE [dbo].ItemLari ";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlUkuran.Items.Add(new ListItem(sdr["ukuran"].ToString(), sdr["ukuran"].ToString()));
                }
            }
        }
        protected void Priview(object sender, EventArgs e)
        {
            string ukuran = string.Empty;
            if (ddlUkuran.SelectedIndex > 0)
                ukuran = " where ukuran='" + ddlUkuran.SelectedItem.Text.Trim() + "'";
            if (ddlGroup.SelectedIndex > 0)
            {
                if (ukuran == string.Empty)
                    ukuran = " where [group]='" + ddlGroup.SelectedItem.Text.Trim() + "'";
                else
                    ukuran = ukuran + " and [group]='" + ddlGroup.SelectedItem.Text.Trim() + "'";
            }

            string StrQuery =
                "declare @thnbln char(6) " +
                "declare @Line char(8) " +
                "set @Line='Line " + ddlLine.SelectedItem.Text + "' " +
                "set @thnbln='" + ddTahun.SelectedValue + ddlBulan.SelectedValue.ToString().PadLeft(2, '0') + "' " +
                //"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SmtLari]') AND type in (N'U')) DROP TABLE [dbo].SmtLari " +
                ////"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SmtLari1]') AND type in (N'U')) DROP TABLE [dbo].SmtLari1 " +
                //"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ItemLari]') AND type in (N'U')) DROP TABLE [dbo].ItemLari " +
                //"select distinct itemid0 into ItemLari from vw_kartustockwip K inner join BM_Destacking D on K.destid=D.ID  inner join BM_Plant P on D.PlantID =P.ID  " +
                //"where left(convert(char,tanggal,112),6)=@thnbln and P.PlantName=@Line and lokasi='p99' " +
                //"select * into SmtLari1 from ( " +
                //"select I.Tebal,I.PartNo,P.PlantName,(sum(K.qty*-1*(I.tebal*I.lebar*I.Panjang)/1000000000)) qty, " +
                //"rtrim(cast(cast(I.Tebal as decimal(8,1)) as char)) + ' mm x ' + rtrim(cast(I.lebar as int)) + ' x ' + rtrim(cast(I.Panjang as int)) Ukuran  from vw_kartustockwip K   " +
                //"inner join fc_items I on K.itemid0=I.id  inner join BM_Destacking D on K.destid=D.ID  inner join BM_Plant P on D.PlantID =P.ID  " +
                //"where left(convert(char,tanggal,112),6)=@thnbln and P.PlantName=@Line and K.qty<0 and lokasi<>'p99' and " +
                //"itemid0 in (select itemid0 from ItemLari) group by I.PartNo,P.PlantName,I.Tebal,I.lebar,I.Panjang )A " +
                //"select  P.PlantName,PG.[Group],  rtrim(cast(cast(I1.Tebal as decimal(8,1)) as char)) + ' mm x ' + rtrim(cast(I1.lebar as int)) + ' x ' + rtrim(cast(I1.Panjang as int)) Ukuran, " +
                //"I1.partno partno0, I2.partno,cast(I2.Tebal as decimal(8,1)) lari, I1.tebal,sum(J.QtyIn *(I2.tebal*I2.lebar*I2.Panjang)/1000000000) QtyIn " +
                //"into SmtLari from T1_JemurLg J inner join BM_Destacking D on J.DestID=D.ID inner join fc_items I1 on D.ItemID=I1.ID inner join fc_items I2 on J.ItemID=I2.ID  " +
                //"inner join BM_Plant P on D.PlantID=P.ID inner join BM_PlantGroup PG on D.PlantGroupID=PG.ID  " +
                //"where left(convert(char,tgljemur,112),6)=@thnbln and P.PlantName=@Line group by P.PlantName,PG.[Group],I1.Tebal,I1.lebar,I1.Panjang,I1.partno,I2.partno,I2.Tebal,I1.id order by P.PlantName  " +
                //"select *,([3A]+[3B]+[4A]+[4B]+[4C]+[5B]+[5C]+[6]+[7]+[8]+[9]+[10]+[11]+[12]+[13]+[14]+[15]+[16]+[17]+[18]+[19]+[20]+[21]+[22])+TPotong TLari from ( " +
                //"select Tebal,[group],ukuran, sum([3A])[3A],sum([3B])[3B],sum([4A])[4A],sum([4B])[4B],sum([4C])[4C],sum([5B])[5B],sum([5C])[5C],sum([6])[6],sum([7])[7],sum([8])[8], " +
                //"sum([9])[9],sum([10])[10],sum([11])[11],sum([12])[12],sum([13])[13],sum([14])[14],sum([15])[15],sum([16])[16],sum([17])[17],sum([18])[18], " +
                //"sum([19])[19],sum([20])[20],sum([21])[21],sum([22])[22],sum(TPotong) TPotong from ( select * from ( " +
                //"select Tebal,[group],ukuran, sum([3A])[3A],sum([3B])[3B],sum([4A])[4A],sum([4B])[4B],sum([4C])[4C],sum([5B])[5B],sum([5C])[5C],sum([6])[6],sum([7])[7],sum([8])[8], " +
                //"sum([9])[9],sum([10])[10],sum([11])[11],sum([12])[12],sum([13])[13],sum([14])[14],sum([15])[15],sum([16])[16],sum([17])[17],sum([18])[18], " +
                //"sum([19])[19],sum([20])[20],sum([21])[21],sum([22])[22],0 TPotong from ( " +
                //"select *,case when lari=3 then QtyIn else 0 end [3A],0.0 [3B],case when lari=3.5 then QtyIn else 0 end [4A],case when lari=4 then QtyIn else 0 end  [4B],case when lari>4 and lari<5 then QtyIn else 0 end  [4C], " +
                //"case when lari=5 then QtyIn else 0 end [5B],case when lari>5 and lari<6 then QtyIn else 0 end [5C],case when lari=6 then QtyIn else 0 end [6], " +
                //"case when lari=7 then QtyIn else 0 end [7],case when lari=8 then QtyIn else 0 end [8],case when lari=9 then QtyIn else 0 end [9],case when lari=10 then QtyIn else 0 end [10], " +
                //"case when lari=11 then QtyIn else 0 end [11],case when lari=12 then QtyIn else 0 end [12],case when lari=13 then QtyIn else 0 end [13],case when lari=14 then QtyIn else 0 end [14], " +
                //"case when lari=15 then QtyIn else 0 end [15],case when lari=16 then QtyIn else 0 end [16],case when lari=17 then QtyIn else 0 end [17],case when lari=18 then QtyIn else 0 end [18], " +
                //"case when lari=19 then QtyIn else 0 end [19],case when lari=20 then QtyIn else 0 end [20],case when lari=21 then QtyIn else 0 end [21],case when lari=22 then QtyIn else 0 end [22] " +
                //"from SmtLari where plantname=@line  " +
                //")A group by [group],ukuran,tebal,Partno0)B  " +
                //"union all " +
                //"select Tebal,[Group],Ukuran,[3A],[3B],[4A],[4B],[4C],[5B],[5C],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],qty TPotong from smtlari1 A   " +
                //"inner join ( " +
                //" select P.PlantName,G.[Group],0.0 [3A],0.0 [3B],0.0 [4A],0.0 [4B],0.0 [4C],0.0 [5B],0.0 [5C],0.0 [6],0.0 [7],0.0 [8],0.0 [9],0.0 [10],0.0 [11],0.0 [12],0.0 [13],0.0 [14], " +
                //"0.0 [15],0.0 [16],0.0 [17],0.0 [18],0.0 [19],0.0 [20],0.0 [21],0.0 [22] from BM_PlantGroup G inner join BM_Plant P on G.PlantID =P.ID where  P.PlantName=@Line )A0  " +
                //"on A.PlantName=A0.PlantName)C group by Tebal,[group],ukuran)D " + ukuran + " order by [group],tebal,ukuran " +
                //"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ItemLari]') AND type in (N'U')) DROP TABLE [dbo].ItemLari " +
                //"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SmtLari1]') AND type in (N'U')) DROP TABLE [dbo].SmtLari1 " +
                //"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SmtLari]') AND type in (N'U')) DROP TABLE [dbo].SmtLari";

                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SmtLari]') AND type in (N'U')) DROP TABLE [dbo].SmtLari  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SmtLari1]') AND type in (N'U')) DROP TABLE [dbo].SmtLari1  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ItemLari]') AND type in (N'U')) DROP TABLE [dbo].ItemLari  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DataLari]') AND type in (N'U')) DROP TABLE [dbo].DataLari  " +

                "select distinct itemid0 into ItemLari from vw_kartustockwip K inner join BM_Destacking D on K.destid=D.ID  inner join BM_Plant P on D.PlantID =P.ID   " +
                "where left(convert(char,tanggal,112),6)=@thnbln  and lokasi='p99'  " +
                "select * into SmtLari1 from (  " +
                "select I.Tebal,I.PartNo,P.PlantName,PG.[Group],(sum(K.qty*-1*(I.tebal*I.lebar*I.Panjang)/1000000000)) qty,  " +
                "rtrim(cast(cast(I.Tebal as decimal(8,1)) as char)) + ' mm x ' + rtrim(cast(I.lebar as int)) + ' x ' + rtrim(cast(I.Panjang as int)) Ukuran  from vw_kartustockwip K    " +
                "inner join fc_items I on K.itemid0=I.id  inner join BM_Destacking D on K.destid=D.ID  inner join BM_Plant P on D.PlantID =P.ID  " +
                "inner join BM_PlantGroup PG on D.PlantGroupID=PG.ID    " +
                "where left(convert(char,tanggal,112),6)=@thnbln and P.PlantName=@Line and K.qty<0 and lokasi<>'p99' and  " +
                "itemid0 in (select itemid0 from ItemLari) group by I.PartNo,P.PlantName,PG.[Group],I.Tebal,I.lebar,I.Panjang )A  " +
                "select  P.PlantName,PG.[Group],  rtrim(cast(cast(I1.Tebal as decimal(8,1)) as char)) + ' mm x ' + rtrim(cast(I1.lebar as int)) + ' x ' + rtrim(cast(I1.Panjang as int)) Ukuran,  " +
                "I1.partno partno0, I2.partno,cast(I2.Tebal as decimal(8,1)) lari, I1.tebal,sum(J.QtyIn *(I2.tebal*I2.lebar*I2.Panjang)/1000000000) QtyIn  " +
                "into SmtLari from T1_JemurLg J inner join BM_Destacking D on J.DestID=D.ID inner join fc_items I1 on D.ItemID=I1.ID inner join fc_items I2 on J.ItemID=I2.ID   " +
                "inner join BM_Plant P on D.PlantID=P.ID inner join BM_PlantGroup PG on D.PlantGroupID=PG.ID   " +
                "where left(convert(char,tgljemur,112),6)=@thnbln and P.PlantName=@Line group by P.PlantName,PG.[Group],I1.Tebal,I1.lebar,I1.Panjang, " +
                "I1.partno,I2.partno,I2.Tebal,I1.id order by P.PlantName   " +

                //"select *,([3A]+[3B]+[4A]+[4B]+[4C]+[5B]+[5C]+[6]+[7]+[8]+[9]+[10]+[11]+[12]+[13]+[14]+[15]+[16]+[17]+[18]+[19]+[20]+[21]+[22])+TPotong TLari from (  " +

                "select *,([3A]+[3B]+[4A]+[4B]+[4C]+[5B]+[5C]+[6]+[7]+[8]+[9]+[10]+[11]+[12]+[13]+[14]+[15]+[16]+[17]+[18]+[19]+[20]+[21]+[22])+TPotong TLari into DataLari from (  " +
                "select Tebal,[group],ukuran, sum([3A])[3A],sum([3B])[3B],sum([4A])[4A],sum([4B])[4B],sum([4C])[4C],sum([5B])[5B],sum([5C])[5C],sum([6])[6],sum([7])[7],sum([8])[8],  " +
                "sum([9])[9],sum([10])[10],sum([11])[11],sum([12])[12],sum([13])[13],sum([14])[14],sum([15])[15],sum([16])[16],sum([17])[17],sum([18])[18],  " +
                "sum([19])[19],sum([20])[20],sum([21])[21],sum([22])[22],sum(TPotong) TPotong from (  " +
                "select * from (  " +
                "select Tebal,[group],ukuran, sum([3A])[3A],sum([3B])[3B],sum([4A])[4A],sum([4B])[4B],sum([4C])[4C],sum([5B])[5B],sum([5C])[5C],sum([6])[6],sum([7])[7],sum([8])[8],  " +
                "sum([9])[9],sum([10])[10],sum([11])[11],sum([12])[12],sum([13])[13],sum([14])[14],sum([15])[15],sum([16])[16],sum([17])[17],sum([18])[18],  " +
                "sum([19])[19],sum([20])[20],sum([21])[21],sum([22])[22],0 TPotong from (  " +
                "select *,case when lari=3 then QtyIn else 0 end [3A],0.000000 [3B],case when lari=3.5 then QtyIn else 0 end [4A],case when lari=4 then QtyIn else 0 end  [4B],case when lari>4 and lari<5 then QtyIn else 0 end  [4C],  " +
                "case when lari=5 then QtyIn else 0 end [5B],case when lari>5 and lari<6 then QtyIn else 0 end [5C],case when lari=6 then QtyIn else 0 end [6],  " +
                "case when lari=7 then QtyIn else 0 end [7],case when lari=8 then QtyIn else 0 end [8],case when lari=9 then QtyIn else 0 end [9],case when lari=10 then QtyIn else 0 end [10],  " +
                "case when lari=11 then QtyIn else 0 end [11],case when lari=12 then QtyIn else 0 end [12],case when lari=13 then QtyIn else 0 end [13],case when lari=14 then QtyIn else 0 end [14], " +
                "case when lari=15 then QtyIn else 0 end [15],case when lari=16 then QtyIn else 0 end [16],case when lari=17 then QtyIn else 0 end [17],case when lari=18 then QtyIn else 0 end [18],  " +
                "case when lari=19 then QtyIn else 0 end [19],case when lari=20 then QtyIn else 0 end [20],case when lari=21 then QtyIn else 0 end [21],case when lari=22 then QtyIn else 0 end [22]  " +
                "from SmtLari where plantname=@line   " +
                ")A group by [group],ukuran,tebal,Partno0)B   " +
                "union all  " +
                "select Tebal,A.[Group],Ukuran,[3A],[3B],[4A],[4B],[4C],[5B],[5C],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],qty TPotong from smtlari1 A    " +
                "inner join (  " +
                "select P.PlantName,G.[Group],0.0 [3A],0.0 [3B],0.0 [4A],0.0 [4B],0.0 [4C],0.0 [5B],0.0 [5C],0.0 [6],0.0 [7],0.0 [8],0.0 [9],0.0 [10],0.0 [11],0.0 [12],0.0 [13],0.0 [14],  " +
                "0.0 [15],0.0 [16],0.0 [17],0.0 [18],0.0 [19],0.0 [20],0.0 [21],0.0 [22] from BM_PlantGroup G inner join BM_Plant P on G.PlantID =P.ID where  P.PlantName=@Line )A0   " +
                "on A.PlantName=A0.PlantName and A.[Group]=A0.[group]" +
                ")C group by Tebal,[group],ukuran)D  " + ukuran + " order by [group],tebal,ukuran  " +

                //query update sarmut add hasan  tgl 16 nov 21
                "update SPD_Trans set actual=( " +
                "select isnull(round(actual,2),0)actual from( " +
                "select (Isnull(trk6 /  NULLIF((trk6+trb6), 0), 0)*tpk)+(Isnull(trb6 /  NULLIF((trk6+trb6), 0), 0)*tpb)/8 actual from( " +
                "select (Isnull(trk6 /  NULLIF(trlk, 0), 0)*100)tpk,(Isnull(trb6 /  NULLIF(trlb, 0), 0)*100)tpb, trk6,trb6,trlk,trlb from(" +
                "select sum(trk6)trk6,sum(trb6)trb6, sum(tlk)trlk,sum(tlb)trlb from ( " +
                "select case when tebal<=6 then total else 0 end trk6,case when tebal>6 then total else 0 end trb6, " +
                "case when tebal<=6 then TLari else 0 end tlk,case when tebal>6 then tlari else 0 end tlb from ( " +
                "select Tebal, [3A]+ [3B]+ [4A]+ [4B]+ [4C]+ [5B]+ [5C]+ [6]+[7]+ [8]+ [9]+ [10]+ [11]+ [12]+ [13]+ [14]+ " +
                "[15]+ [16]+ [17]+ [18]+ [19]+ [20]+ [21]+ [22] total , TLari " +
                "from DataLari )A )B )C )D )E )" +
                " WHERE SarmutDeptID IN (select id from SPD_Departemen where SarmutDepartemen='Line " + ddlLine.SelectedItem.Text + "' " +
                "and SarmutPID=72) and Tahun=" + ddTahun.SelectedValue + " AND Bulan=" + ddlBulan.SelectedValue + "and approval=0 " +
                //end query update sarmut

                "select * from Datalari " +

                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ItemLari]') AND type in (N'U')) DROP TABLE [dbo].ItemLari  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SmtLari1]') AND type in (N'U')) DROP TABLE [dbo].SmtLari1  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SmtLari]') AND type in (N'U')) DROP TABLE [dbo].SmtLari " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DataLari]') AND type in (N'U')) DROP TABLE [dbo].DataLari  ";

            Session["Query"] = StrQuery;
            Session["periode"] = ddlBulan.SelectedItem.Text + " " + ddTahun.SelectedValue;
            Session["line"] = ddlLine.SelectedValue;
            Cetak(this);
        }
        static public void Cetak(Control page)
        {
            string myScript = "var wn = window.showModalDialog('../ReportT1T3/Report.aspx?IdReport=smtpelarian', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 1200px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        protected void ddlBulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadUkuran();
        }
        protected void ddlLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGroup(ddlLine.SelectedItem.Text);
            LoadUkuran();
        }
    }
}