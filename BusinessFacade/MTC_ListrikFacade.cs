using Dapper;
using DataAccessLayer;
using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BusinessFacade
{
    public class MTC_ListrikFacade : AbstractFacade
    {
        private HistPO objHistPO = new HistPO();
        //private ArrayList arrHistPO;
        private List<SqlParameter> sqlListParam;

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
        public int InsertPemakaian(DateTime Tanggal,int Line, decimal kWhPJT, decimal kVarhPJT,decimal kWhPLN,decimal kVarhPLN,string Keterangan)
        {
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    sqlListParam = new List<SqlParameter>();
                    sqlListParam.Add(new SqlParameter("@Tanggal",Tanggal));
                    sqlListParam.Add(new SqlParameter("@Line",Line ));
                    sqlListParam.Add(new SqlParameter("@kWhPJT",kWhPJT ));
                    sqlListParam.Add(new SqlParameter("@kVarhPJT",kVarhPJT ));
                    sqlListParam.Add(new SqlParameter("@kWhPLN",kWhPLN ));
                    sqlListParam.Add(new SqlParameter("@kVarhPLN",kVarhPLN ));
                    sqlListParam.Add(new SqlParameter("@Keterangan", Keterangan));
                    int intResult = dataAccess.ProcessData(sqlListParam, "spMTC_Listrik_Insert");
                    strError = dataAccess.Error;
                    return 1;
                }
                catch (Exception ex)
                {
                    strError = ex.Message;
                    return -1;
                }
            }
        }
        public static List<MTC_Listrik>RetrivePemakaian(string tgl, int depo)
        {
            List<MTC_Listrik> alldata = new List<MTC_Listrik>();
            string strField = string.Empty;
            string strsql = string.Empty;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    if (depo == 1)
                    {
                        strsql = "declare @tgl varchar(6)" +
                                    "set @tgl = " + tgl + " " +
                                    ";WITH sums AS " +
                                    "(SELECT Tanggal, kWhPJT," +
                                    "min(kWhPJT) over(" +
                                    "ORDER BY tanggal ROWS BETWEEN 1 following AND 1 following) - kWhPJT AS result1," +
                                    "kVarhPJT," +
                                    "min(kVarhPJT) over(" +
                                    "ORDER BY tanggal ROWS BETWEEN 1 following AND 1 following) - kVarhPJT AS result2," +
                                    "kWhPLN," +
                                    "min(kWhPLN) over(" +
                                    "ORDER BY tanggal ROWS BETWEEN 1 following AND 1 following) - kWhPLN AS result3," +
                                    "kVarhPLN," +
                                    "min(kVarhPLN) over(" +
                                    "ORDER BY tanggal ROWS BETWEEN 1 following AND 1 following) - kVarhPLN AS result4, " +
                                    "kWhPJT2,kVarhPJT2,kWhPLN2,kVarhPLN2 " +
                                    "FROM MTC_Rekap_Listrik where convert(char,Tanggal,112)>=@tgl " +
                                    "+'01' and convert(char,Tanggal,112)<= left(@tgl,4) +(DATEADD(month,1,@tgl+'01' )))" +
                                    "select * from (" +
                                    "select Tanggal,kWhPJT,result1,kVarhPJT,result2,kWhPLN,result3,kVarhPLN,result4," +
                                    "(result1+result3)as totalkwh,(result2+result4)as totalkvarh from(" +
                                    "select Tanggal,cast(kWhPJT as decimal(10, 2)) as kWhPJT," +
                                    "case when kWhPJT2 is not null then (result1*1000)+kWhPJT2 when kWhPJT2 is null then (result1*1000)end as result1," +
                                    "cast(kVarhPJT as decimal(10, 2)) as kVarhPJT, " +
                                    "case when kVarhPJT2 is not null then (result2*1000)+kVarhPJT2 when kVarhPJT2 is null then (result2*1000)end as result2," +
                                    "cast(kWhPLN as decimal(10, 3)) as kWhPLN, " +
                                    "case when kWhPLN2 is not null then (result3*3*1000) + kWhPLN2 when kWhPLN2 is null then (result3*6*1000)end as result3," +
                                    "cast(kVarhPLN as decimal(10, 3)) as kVarhPLN, " +
                                    "case when kVarhPLN2 is not null then (result4*3*1000)+kVarhPLN2 when kVarhPLN2 is null then (result4*6*1000)end as result4," +
                                    "result1+result3 as totalkwh,result2 + result4 as totalkvarh from sums)x" +
                                    ")a where left(convert(char,Tanggal,112),6)=@tgl";
                    }
                    else if (depo == 7)
                    {
                        strsql = "declare @tgl varchar(6)" +
                                    "set @tgl = " + tgl + " " +
                                    ";WITH sums AS " +
                                    "(SELECT Tanggal, kWhPJT," +
                                    "min(kWhPJT) over(" +
                                    "ORDER BY tanggal ROWS BETWEEN 1 following AND 1 following) - kWhPJT AS result1," +
                                    "kVarhPJT," +
                                    "min(kVarhPJT) over(" +
                                    "ORDER BY tanggal ROWS BETWEEN 1 following AND 1 following) - kVarhPJT AS result2," +
                                    "kWhPLN," +
                                    "min(kWhPLN) over(" +
                                    "ORDER BY tanggal ROWS BETWEEN 1 following AND 1 following) - kWhPLN AS result3," +
                                    "kVarhPLN," +
                                    "min(kVarhPLN) over(" +
                                    "ORDER BY tanggal ROWS BETWEEN 1 following AND 1 following) - kVarhPLN AS result4, " +
                                    "kWhPJT2,kVarhPJT2,kWhPLN2,kVarhPLN2 " +
                                    "FROM MTC_Rekap_Listrik where convert(char,Tanggal,112)>=@tgl " +
                                    "+'01' and convert(char,Tanggal,112)<= left(@tgl,4) +(DATEADD(month,1,@tgl+'01' )))" +
                                    "select * from (" +
                                    "select Tanggal,kWhPJT,result1,kVarhPJT,result2,kWhPLN,result3,kVarhPLN,result4," +
                                    "(result1+result3)as totalkwh,(result2+result4)as totalkvarh from(" +
                                    "select Tanggal,cast(kWhPJT as decimal(10, 2)) as kWhPJT," +
                                    "case when kWhPJT2 is not null then (result1*1000)+kWhPJT2 when kWhPJT2 is null then (result1*1000)end as result1," +
                                    "cast(kVarhPJT as decimal(10, 2)) as kVarhPJT, " +
                                    "case when kVarhPJT2 is not null then (result2*1000)+kVarhPJT2 when kVarhPJT2 is null then (result2*1000)end as result2," +
                                    "cast(kWhPLN as decimal(10, 3)) as kWhPLN, " +
                                    "case when kWhPLN2 is not null then (result3*6*1000) + kWhPLN2 when kWhPLN2 is null then (result3*6*1000)end as result3," +
                                    "cast(kVarhPLN as decimal(10, 3)) as kVarhPLN, " +
                                    "case when kVarhPLN2 is not null then (result4*6*1000)+kVarhPLN2 when kVarhPLN2 is null then (result4*6*1000)end as result4," +
                                    "result1+result3 as totalkwh,result2 + result4 as totalkvarh from sums)x" +
                                    ")a where left(convert(char,Tanggal,112),6)=@tgl";
                    }
                    else
                    {
                        strsql = "declare @tgl varchar(6)" +
                                    "set @tgl = " + tgl + " " +
                                    ";WITH sums AS " +
                                    "(SELECT Tanggal, kWhPJT," +
                                    "min(kWhPJT) over(" +
                                    "ORDER BY tanggal ROWS BETWEEN 1 following AND 1 following) - kWhPJT AS result1," +
                                    "kVarhPJT," +
                                    "min(kVarhPJT) over(" +
                                    "ORDER BY tanggal ROWS BETWEEN 1 following AND 1 following) - kVarhPJT AS result2," +
                                    "kWhPLN," +
                                    "min(kWhPLN) over(" +
                                    "ORDER BY tanggal ROWS BETWEEN 1 following AND 1 following) - kWhPLN AS result3," +
                                    "kVarhPLN," +
                                    "min(kVarhPLN) over(" +
                                    "ORDER BY tanggal ROWS BETWEEN 1 following AND 1 following) - kVarhPLN AS result4, " +
                                    "kWhPJT2,kVarhPJT2,kWhPLN2,kVarhPLN2 " +
                                    "FROM MTC_Rekap_Listrik where convert(char,Tanggal,112)>=@tgl " +
                                    "+'01' and convert(char,Tanggal,112)<= left(@tgl,4) +(DATEADD(month,1,@tgl+'01' )))" +
                                    "select * from (" +
                                    "select Tanggal,kWhPJT,result1,kVarhPJT,result2,kWhPLN,result3,kVarhPLN,result4," +
                                    "(result1+result3)as totalkwh,(result2+result4)as totalkvarh from(" +
                                    "select Tanggal,cast(kWhPJT as decimal(10, 2)) as kWhPJT," +
                                    "case when kWhPJT2 is not null then (result1*1000)+kWhPJT2 when kWhPJT2 is null then (result1*1000)end as result1," +
                                    "cast(kVarhPJT as decimal(10, 2)) as kVarhPJT, " +
                                    "case when kVarhPJT2 is not null then (result2*1000)+kVarhPJT2 when kVarhPJT2 is null then (result2*1000)end as result2," +
                                    "cast(kWhPLN as decimal(10, 3)) as kWhPLN, " +
                                    "case when kWhPLN2 is not null then (result3*3*1000) + kWhPLN2 when kWhPLN2 is null then (result3*3*1000)end as result3," +
                                    "cast(kVarhPLN as decimal(10, 3)) as kVarhPLN, " +
                                    "case when kVarhPLN2 is not null then (result4*3*1000)+kVarhPLN2 when kVarhPLN2 is null then (result4*3*1000)end as result4," +
                                    "result1+result3 as totalkwh,result2 + result4 as totalkvarh from sums)x" +
                                    ")a where left(convert(char,Tanggal,112),6)=@tgl";
                    }
                    alldata = connection.Query<MTC_Listrik>(strsql).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }

        public int UpdatePemakaian(DateTime Tanggal, int Line, decimal kWhPJT, decimal kVarhPJT, decimal kWhPLN, decimal kVarhPLN,string Keterangan)
        {
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    sqlListParam = new List<SqlParameter>();
                    sqlListParam.Add(new SqlParameter("@Tanggal", Tanggal));
                    sqlListParam.Add(new SqlParameter("@Line", Line));
                    sqlListParam.Add(new SqlParameter("@kWhPJT", kWhPJT));
                    sqlListParam.Add(new SqlParameter("@kVarhPJT", kVarhPJT));
                    sqlListParam.Add(new SqlParameter("@kWhPLN", kWhPLN));
                    sqlListParam.Add(new SqlParameter("@kVarhPLN", kVarhPLN));
                    sqlListParam.Add(new SqlParameter("@Keterangan", Keterangan));
                    int intResult = dataAccess.ProcessData(sqlListParam, "spMTC_Listrik_Update");
                    strError = dataAccess.Error;
                    return 1;
                }
                catch (Exception ex)
                {
                    strError = ex.Message;
                    return -1;
                }
            }
        }

        public int Penyesuaian(DateTime Tanggal, decimal kWhPJT, decimal kVarhPJT, decimal kWhPLN, decimal kVarhPLN)
        {
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    sqlListParam = new List<SqlParameter>();
                    sqlListParam.Add(new SqlParameter("@Tanggal", Tanggal));
                    sqlListParam.Add(new SqlParameter("@kWhPJT", kWhPJT));
                    sqlListParam.Add(new SqlParameter("@kVarhPJT", kVarhPJT));
                    sqlListParam.Add(new SqlParameter("@kWhPLN", kWhPLN));
                    sqlListParam.Add(new SqlParameter("@kVarhPLN", kVarhPLN));
                    int intResult = dataAccess.ProcessData(sqlListParam, "spMTC_Listrik_penyesuaian");
                    strError = dataAccess.Error;
                    return 1;
                }
                catch (Exception ex)
                {
                    strError = ex.Message;
                    return -1;
                }
            }
        }

        public static List<MTC_Listrik> RetriveEfisiensi(string tgl, int depo)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string Line = "0";
            if (users.UnitKerjaID == 7)
            {
                Line = "5";
            }else if (users.UnitKerjaID == 1)
            {
                Line = "3";
            }
            else if (users.UnitKerjaID == 13)
            {
                Line = "10";
            }
            
            List<MTC_Listrik> alldata = new List<MTC_Listrik>();
            string strField = string.Empty;
            string strsql = "";
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    if (depo == 1)
                    {
                        strsql = "declare @tgl varchar(6)" +
                                        "set @tgl = " + tgl + " " +
                                        ";WITH rekap AS " +
                                        "(SELECT Tanggal, kWhPJT, " +
                                        "min(kWhPJT) over(" +
                                        "ORDER BY tanggal ROWS BETWEEN 1 following AND 1 following) - kWhPJT AS result1, " +
                                        "kVarhPJT, " +
                                        "min(kVarhPJT) over(" +
                                        "ORDER BY tanggal ROWS BETWEEN 1 following AND 1 following) - kVarhPJT AS result2, " +
                                        "kWhPLN, " +
                                        "min(kWhPLN) over(" +
                                        "ORDER BY tanggal ROWS BETWEEN 1 following AND 1 following) - kWhPLN AS result3, " +
                                        "kVarhPLN, " +
                                        "min(kVarhPLN) over(" +
                                        "ORDER BY tanggal ROWS BETWEEN 1 following AND 1 following) - kVarhPLN AS result4, " +
                                        "Keterangan,case when line < " + Line + " then 110 when Line >= " + Line + " then 100 end as linefix, " +
                                        "kWhPJT2,kVarhPJT2,kWhPLN2,kVarhPLN2 " +
                                        "FROM MTC_Rekap_Listrik where convert(char,Tanggal,112)>=@tgl " +
                                        "+'01' and convert(char,Tanggal,112)<= left(@tgl,4) +(DATEADD(month,1,@tgl+'01' ))and rowstatus>-1)," +
                                        "hitung_output as(" +
                                        "select a.TglProduksi,a.qty, a.itemid, b.Volume,a.Qty* (b.Lebar*b.Tebal*b.Panjang)/1000000000 as hasil " +
                                        "from BM_Destacking a inner " +
                                        "join FC_Items b on a.ItemID = b.id left " +
                                        "join MTC_Rekap_Listrik c " +
                                        "on Convert(varchar, a.TglProduksi, 112) = Convert(varchar, c.Tanggal, 112) where a.rowstatus>-1 and left(convert(char,a.tglproduksi,112),6)=@tgl)" +
                                        "select * from ( " +
                                        "select 'a' urt, Tanggal, totalkwh, totalkvarh,cast(output as decimal(10, 2)) as output, cast(kwhm3 as decimal(10, 2)) as kwhm3, " +
                                        "cast(prosentase as decimal(10, 2)) as prosentase,keterangan from( " +
                                        "select Tanggal, totalkwh, totalkvarh, output, kwhm3, (kwhm3 / linefix) * 100 as prosentase, keterangan from( " +
                                        "select Tanggal, totalkwh, totalkvarh, output, (Isnull(totalkwh / NULLIF(output, 0), 0)) as kwhm3, keterangan, linefix from( " +
                                        "select Tanggal, " +
                                        "case when kWhPJT2 is not null then(result1 * 1000) + kWhPJT2 when kWhPJT2 is null then(result1 * 1000)end + " +
                                        "case when kWhPLN2 is not null then(result3 * 6 * 1000) + kWhPLN2 when kWhPLN2 is null then(result3 * 6 * 1000)end as totalkwh, " +
                                        "case when kVarhPJT2 is not null then(result2 * 1000) + kVarhPJT2 when kVarhPJT2 is null then(result2 * 1000)end + " +
                                        "case when kVarhPLN2 is not null then(result4 * 6 * 1000) + kVarhPLN2 when kVarhPLN2 is null then(result4 * 6 * 1000)end as totalkvarh, " +
                                        "(select sum(hasil)from hitung_output where convert(varchar, TglProduksi, 112) = convert(varchar, rekap.Tanggal, 112)) as output, " +
                                        "keterangan, linefix from rekap)x)y)z where left(convert(char, Tanggal, 112), 6) = @tgl " +
                                        "union all " +
                                        "select 'b' urt,'1900-01-01' Tanggal, totalkwh,totalkvarh,cast(output as decimal(10,2))output,cast(kwhm3 as decimal(10,2))kwhm3, " +
                                        "cast(prosentase as decimal(10, 2)) as prosentase,keterangan from( " +
                                        "select sum(totalkwh)totalkwh,sum(totalkvarh)totalkvarh,sum(output)output,(sum(totalkwh) / sum(output))kwhm3, " +
                                        "(sum(totalkwh) / sum(output))prosentase,Keterangan from( " +
                                        "select totalkwh, totalkvarh, output, kwhm3, (kwhm3 / linefix) * 100 as prosentase, keterangan from( " +
                                        "select  totalkwh, totalkvarh, output, (Isnull(totalkwh / NULLIF(output, 0), 0)) as kwhm3, keterangan, linefix from( " +
                                        "select  Tanggal, " +
                                        "case when kWhPJT2 is not null then(result1 * 1000) + kWhPJT2 when kWhPJT2 is null then(result1 * 1000)end + " +
                                        "case when kWhPLN2 is not null then(result3 * 6 * 1000) + kWhPLN2 when kWhPLN2 is null then(result3 * 6 * 1000)end as totalkwh, " +
                                        "case when kVarhPJT2 is not null then(result2 * 1000) + kVarhPJT2 when kVarhPJT2 is null then(result2 * 1000)end + " +
                                        "case when kVarhPLN2 is not null then(result4 * 6 * 1000) + kVarhPLN2 when kVarhPLN2 is null then(result4 * 6 * 1000)end as totalkvarh, " +
                                        "(select sum(hasil)from hitung_output where convert(varchar, TglProduksi, 112) = convert(varchar, rekap.Tanggal, 112)) as output, " +
                                        "keterangan, linefix from rekap " +
                                        ")x where left(convert(char, Tanggal, 112), 6) = @tgl group by Tanggal, totalkwh, totalkvarh, output, Keterangan, linefix " +
                                        ")y group by totalkwh, output, totalkvarh, kwhm3, linefix, Keterangan " +
                                        ")aa group by Keterangan)ab)a ";
                    }
                    else if (depo == 7)
                    {
                        strsql = "declare @tgl varchar(6)" +
                                    "set @tgl = " + tgl + " " +
                                    ";WITH rekap AS " +
                                    "(SELECT Tanggal, kWhPJT, " +
                                    "min(kWhPJT) over(" +
                                    "ORDER BY tanggal ROWS BETWEEN 1 following AND 1 following) - kWhPJT AS result1, " +
                                    "kVarhPJT, " +
                                    "min(kVarhPJT) over(" +
                                    "ORDER BY tanggal ROWS BETWEEN 1 following AND 1 following) - kVarhPJT AS result2, " +
                                    "kWhPLN, " +
                                    "min(kWhPLN) over(" +
                                    "ORDER BY tanggal ROWS BETWEEN 1 following AND 1 following) - kWhPLN AS result3, " +
                                    "kVarhPLN, " +
                                    "min(kVarhPLN) over(" +
                                    "ORDER BY tanggal ROWS BETWEEN 1 following AND 1 following) - kVarhPLN AS result4, " +
                                    "Keterangan,case when line < " + Line + " then 110 when Line >= " + Line + " then 100 end as linefix, " +
                                    "kWhPJT2,kVarhPJT2,kWhPLN2,kVarhPLN2 " +
                                    "FROM MTC_Rekap_Listrik where convert(char,Tanggal,112)>=@tgl " +
                                    "+'01' and convert(char,Tanggal,112)<= left(@tgl,4) +(DATEADD(month,1,@tgl+'01' ))and rowstatus>-1)," +
                                    "hitung_output as(" +
                                    "select a.TglProduksi,a.qty, a.itemid, b.Volume,a.Qty* (b.Lebar*b.Tebal*b.Panjang)/1000000000 as hasil " +
                                    "from BM_Destacking a inner " +
                                    "join FC_Items b on a.ItemID = b.id left " +
                                    "join MTC_Rekap_Listrik c " +
                                    "on Convert(varchar, a.TglProduksi, 112) = Convert(varchar, c.Tanggal, 112) where a.rowstatus>-1 and left(convert(char,a.tglproduksi,112),6)=@tgl)" +
                                    "select * from ( " +
                                    "select 'a' urt, Tanggal, totalkwh, totalkvarh,cast(output as decimal(10, 2)) as output, cast(kwhm3 as decimal(10, 2)) as kwhm3, " +
                                    "cast(prosentase as decimal(10, 2)) as prosentase,keterangan from( " +
                                    "select Tanggal, totalkwh, totalkvarh, output, kwhm3, (kwhm3 / linefix) * 100 as prosentase, keterangan from( " +
                                    "select Tanggal, totalkwh, totalkvarh, output, (Isnull(totalkwh / NULLIF(output, 0), 0)) as kwhm3, keterangan, linefix from( " +
                                    "select Tanggal, " +
                                    "case when kWhPJT2 is not null then(result1 * 1000) + kWhPJT2 when kWhPJT2 is null then(result1 * 1000)end + " +
                                    "case when kWhPLN2 is not null then(result3 * 6 * 1000) + kWhPLN2 when kWhPLN2 is null then(result3 * 6 * 1000)end as totalkwh, " +
                                    "case when kVarhPJT2 is not null then(result2 * 1000) + kVarhPJT2 when kVarhPJT2 is null then(result2 * 1000)end + " +
                                    "case when kVarhPLN2 is not null then(result4 * 6 * 1000) + kVarhPLN2 when kVarhPLN2 is null then(result4 * 6 * 1000)end as totalkvarh, " +
                                    "(select sum(hasil)from hitung_output where convert(varchar, TglProduksi, 112) = convert(varchar, rekap.Tanggal, 112)) as output, " +
                                    "keterangan, linefix from rekap)x)y)z where left(convert(char, Tanggal, 112), 6) = @tgl " +
                                    "union all " +
                                    "select 'b' urt,'1900-01-01' Tanggal, totalkwh,totalkvarh,cast(output as decimal(10,2))output,cast(kwhm3 as decimal(10,2))kwhm3,  " +
                                    "cast(prosentase as decimal(10, 2)) as prosentase,keterangan from( " +
                                    "select sum(totalkwh) totalkwh, sum(totalkvarh) totalkvarh, sum(output) output, (sum(totalkwh) / sum(output))kwhm3,  " +
                                    "((sum(totalkwh) / sum(output)) / linefixtotal) * 100 prosentase,Keterangan from( " +
                                    "select totalkwh, totalkvarh, output, kwhm3, linefixtotal, keterangan from( " +
                                    "select totalkwh, totalkvarh, output, (Isnull(totalkwh / NULLIF(output, 0), 0)) as kwhm3,linefixtotal, keterangan from( " +
                                    "select Tanggal, " +
                                    "case when kWhPJT2 is not null then(result1 * 1000) + kWhPJT2 when kWhPJT2 is null then(result1 * 1000)end +  " +
                                    "case when kWhPLN2 is not null then(result3 * 6 * 1000) + kWhPLN2 when kWhPLN2 is null then(result3 * 6 * 1000)end as totalkwh,  " +
                                    "case when kVarhPJT2 is not null then(result2 * 1000) + kVarhPJT2 when kVarhPJT2 is null then(result2 * 1000)end + " +
                                    "case when kVarhPLN2 is not null then(result4 * 6 * 1000) + kVarhPLN2 when kVarhPLN2 is null then(result4 * 6 * 1000)end as totalkvarh,  " +
                                    "(select sum(hasil)from hitung_output where convert(varchar, TglProduksi, 112) = convert(varchar, rekap.Tanggal, 112)) as output,  " +
                                    "(select case when(select top 1 line from(select line, count(id) id from MTC_Rekap_Listrik where left(convert(char, tanggal, 112), 6) = @tgl group by Line)a order by id desc) = 4 then 110 else 100 end linefixtotal)as linefixtotal, " +
                                    "keterangan from rekap " +
                                    ")x where left(convert(char, Tanggal, 112), 6) = @tgl group by Tanggal, totalkwh, totalkvarh, output,linefixtotal, Keterangan " +
                                    ")y group by totalkwh, output, totalkvarh, kwhm3, linefixtotal, Keterangan )aa group by Keterangan, linefixtotal)ab)a";
                    }
                    else
                    {
                        strsql = "declare @tgl varchar(6)" +
                                    "set @tgl = " + tgl + " " +
                                    ";WITH rekap AS " +
                                    "(SELECT Tanggal, kWhPJT, " +
                                    "min(kWhPJT) over(" +
                                    "ORDER BY tanggal ROWS BETWEEN 1 following AND 1 following) - kWhPJT AS result1, " +
                                    "kVarhPJT, " +
                                    "min(kVarhPJT) over(" +
                                    "ORDER BY tanggal ROWS BETWEEN 1 following AND 1 following) - kVarhPJT AS result2, " +
                                    "kWhPLN, " +
                                    "min(kWhPLN) over(" +
                                    "ORDER BY tanggal ROWS BETWEEN 1 following AND 1 following) - kWhPLN AS result3, " +
                                    "kVarhPLN, " +
                                    "min(kVarhPLN) over(" +
                                    "ORDER BY tanggal ROWS BETWEEN 1 following AND 1 following) - kVarhPLN AS result4, " +
                                    "Keterangan,case when line < " + Line + " then 110 when Line >= " + Line + " then 100 end as linefix, " +
                                    "kWhPJT2,kVarhPJT2,kWhPLN2,kVarhPLN2 " +
                                    "FROM MTC_Rekap_Listrik where convert(char,Tanggal,112)>=@tgl " +
                                    "+'01' and convert(char,Tanggal,112)<= left(@tgl,4) +(DATEADD(month,1,@tgl+'01' ))and rowstatus>-1)," +
                                    "hitung_output as(" +
                                    "select a.TglProduksi,a.qty, a.itemid, b.Volume,a.Qty* (b.Lebar*b.Tebal*b.Panjang)/1000000000 as hasil " +
                                    "from BM_Destacking a inner " +
                                    "join FC_Items b on a.ItemID = b.id left " +
                                    "join MTC_Rekap_Listrik c " +
                                    "on Convert(varchar, a.TglProduksi, 112) = Convert(varchar, c.Tanggal, 112) where a.rowstatus>-1 and shift > 0 and left(convert(char,a.tglproduksi,112),6)=@tgl)" +
                                    "select * from ( " +
                                    "select 'a' urt, Tanggal, totalkwh, totalkvarh,cast(output as decimal(10, 2)) as output, cast(kwhm3 as decimal(10, 2)) as kwhm3, " +
                                    "cast(prosentase as decimal(10, 2)) as prosentase,keterangan from( " +
                                    "select Tanggal, totalkwh, totalkvarh, output, kwhm3, (kwhm3 / linefix) * 100 as prosentase, keterangan from( " +
                                    "select Tanggal, totalkwh, totalkvarh, output, (Isnull(totalkwh / NULLIF(output, 0), 0)) as kwhm3, keterangan, linefix from( " +
                                    "select Tanggal, " +
                                    "case when kWhPJT2 is not null then(result1 * 1000) + kWhPJT2 when kWhPJT2 is null then(result1 * 1000)end + " +
                                    "case when kWhPLN2 is not null then(result3 * 3 * 1000) + kWhPLN2 when kWhPLN2 is null then(result3 * 3 * 1000)end as totalkwh, " +
                                    "case when kVarhPJT2 is not null then(result2 * 1000) + kVarhPJT2 when kVarhPJT2 is null then(result2 * 1000)end + " +
                                    "case when kVarhPLN2 is not null then(result4 * 3 * 1000) + kVarhPLN2 when kVarhPLN2 is null then(result4 * 3 * 1000)end as totalkvarh, " +
                                    "(select sum(hasil)from hitung_output where convert(varchar, TglProduksi, 112) = convert(varchar, rekap.Tanggal, 112)) as output, " +
                                    "keterangan, linefix from rekap)x)y)z where left(convert(char, Tanggal, 112), 6) = @tgl " +
                                    "union all " +
                                    "select 'b' urt,'1900-01-01' Tanggal, totalkwh,totalkvarh,cast(output as decimal(10,2))output,cast(kwhm3 as decimal(10,2))kwhm3, " +
                                    "cast(prosentase as decimal(10, 2)) as prosentase,keterangan from( " +
                                    "select sum(totalkwh)totalkwh,sum(totalkvarh)totalkvarh,sum(output)output,(sum(totalkwh) / sum(output))kwhm3, " +
                                    "(sum(totalkwh) / sum(output))prosentase,Keterangan from( " +
                                    "select totalkwh, totalkvarh, output, kwhm3, (kwhm3 / linefix) * 100 as prosentase, keterangan from( " +
                                    "select  totalkwh, totalkvarh, output, (Isnull(totalkwh / NULLIF(output, 0), 0)) as kwhm3, keterangan, linefix from( " +
                                    "select  Tanggal, " +
                                    "case when kWhPJT2 is not null then(result1 * 1000) + kWhPJT2 when kWhPJT2 is null then(result1 * 1000)end + " +
                                    "case when kWhPLN2 is not null then(result3 * 3 * 1000) + kWhPLN2 when kWhPLN2 is null then(result3 * 3 * 1000)end as totalkwh, " +
                                    "case when kVarhPJT2 is not null then(result2 * 1000) + kVarhPJT2 when kVarhPJT2 is null then(result2 * 1000)end + " +
                                    "case when kVarhPLN2 is not null then(result4 * 3 * 1000) + kVarhPLN2 when kVarhPLN2 is null then(result4 * 3 * 1000)end as totalkvarh, " +
                                    "(select sum(hasil)from hitung_output where convert(varchar, TglProduksi, 112) = convert(varchar, rekap.Tanggal, 112)) as output, " +
                                    "keterangan, linefix from rekap " +
                                    ")x where left(convert(char, Tanggal, 112), 6) = @tgl group by Tanggal, totalkwh, totalkvarh, output, Keterangan, linefix " +
                                    ")y group by totalkwh, output, totalkvarh, kwhm3, linefix, Keterangan " +
                                    ")aa group by Keterangan)ab)a ";
                    }
                    alldata = connection.Query<MTC_Listrik>(strsql).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }

        public string PlanningProdLine(string tahun,string bulan)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string Line = (users.UnitKerjaID == 7) ? "5" : "4";
            Line = (users.UnitKerjaID == 13) ? "1" : "4";
            string result = Line;
            string strSQL = "SELECT top 1 PlanningID,COUNT(PakaiDate) Fequensi,(Select Planning From MaterialPP Where ID=PlanningID)Line FROM " +
                            "( " +
                            "    SELECT PakaiDate,PlanningID FROM pakai  " +
                            "    WHERE Month(PakaiDate)=" + tahun + " and Year(PakaiDate)=" +
                                bulan + " AND PlanningID>0 AND DeptID=2 AND Status>-1 " +
                            "    GROUP BY PakaiDate,PlanningID " +
                            ") AS x " +
                            "GROUP By PlanningID " +
                            "HAVING COUNT(PakaiDate)>7 " +
                            "ORDER BY Fequensi DESC";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = sdr["Line"].ToString();
                }
            }
            else
            {
               // result = PlanningProdLine(true);
            }
            return result;
        }

        public string updatesarmut(string tahun,string bulan)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string line = PlanningProdLine(tahun, bulan);
            string sarmutPrs = string.Empty;
            if (users.UnitKerjaID == 7  )
            {
                if(line == "4")
                {
                    sarmutPrs = "Efisiensi Pemakaian Listrik ( untuk 1 - 4 line )";
                }
                else if(line == "5")
                {
                    sarmutPrs = "Efisiensi Pemakaian Listrik ( untuk 5 - 6 line )";
                }
                
            }
            decimal aktual = 0;
            int Line = 0;
            string tgl = tahun + bulan;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            if (users.UnitKerjaID == 1)
            {
                zl.CustomQuery = "declare @tgl varchar(6) " +
                             "set @tgl = '" + tgl + "' " +
                             "; WITH rekap AS " +
                             "(SELECT Tanggal, kWhPJT, " +
                             "min(kWhPJT) over( " +
                             "ORDER BY tanggal ROWS BETWEEN 1 following AND 1 following) - kWhPJT AS result1, " +
                             "kWhPLN, " +
                             "min(kWhPLN) over( " +
                             "ORDER BY tanggal ROWS BETWEEN 1 following AND 1 following) - kWhPLN AS result3, " +
                             "kWhPJT2, kWhPLN2 " +
                             "FROM MTC_Rekap_Listrik where convert(char, Tanggal, 112) >= @tgl " +
                             "+ '01' and convert(char, Tanggal, 112) <= left(@tgl, 4) + (DATEADD(month, 1, @tgl + '01'))and rowstatus > -1), " +
                             "hitung_output as( " +
                             "select a.TglProduksi,a.qty, a.itemid, b.Volume,a.Qty * (b.Lebar * b.Tebal * b.Panjang) / 1000000000 as hasil " +
                             "from BM_Destacking a inner " +
                             "join FC_Items b on a.ItemID = b.id left " +
                             "join MTC_Rekap_Listrik c " +
                             "on Convert(varchar, a.TglProduksi, 112) = Convert(varchar, c.Tanggal, 112) where a.RowStatus > -1 and LEFT(convert(CHAR, tglproduksi, 112), 6)= @tgl) " +
                             "select cast(kwhm3 as decimal(10, 2))actual from( " +
                             "select sum(totalkwh)totalkwh, sum(output)output, (sum(totalkwh) / sum(output))kwhm3 from( " +
                             "select totalkwh, output, kwhm3 from( " +
                             "select  totalkwh, output, (Isnull(totalkwh / NULLIF(output, 0), 0)) as kwhm3 from( " +
                             "select tanggal, " +
                             "case when kWhPJT2 is not null then(result1 * 1000) + kWhPJT2 when kWhPJT2 is null then(result1 * 1000)end + " +
                             "case when kWhPLN2 is not null then(result3 * 6 * 1000) + kWhPLN2 when kWhPLN2 is null then(result3 * 6 * 1000)end as totalkwh, " +
                             "(select sum(hasil)from hitung_output where convert(varchar, TglProduksi, 112) = convert(varchar, rekap.Tanggal, 112)) as output from rekap " +
                             ")x where left(convert(char, Tanggal, 112), 6) = @tgl group by Tanggal, totalkwh, output)y)aa )a";
            }
            else if (users.UnitKerjaID == 7)
            {
                zl.CustomQuery = "declare @tgl varchar(6) " +
                             "set @tgl = '" + tgl + "' " +
                             "; WITH rekap AS " +
                             "(SELECT Tanggal, kWhPJT, " +
                             "min(kWhPJT) over( " +
                             "ORDER BY tanggal ROWS BETWEEN 1 following AND 1 following) - kWhPJT AS result1, " +
                             "kWhPLN, " +
                             "min(kWhPLN) over( " +
                             "ORDER BY tanggal ROWS BETWEEN 1 following AND 1 following) - kWhPLN AS result3, " +
                             "kWhPJT2, kWhPLN2 " +
                             "FROM MTC_Rekap_Listrik where convert(char, Tanggal, 112) >= @tgl " +
                             "+ '01' and convert(char, Tanggal, 112) <= left(@tgl, 4) + (DATEADD(month, 1, @tgl + '01'))and rowstatus > -1), " +
                             "hitung_output as( " +
                             "select a.TglProduksi,a.qty, a.itemid, b.Volume,a.Qty * (b.Lebar * b.Tebal * b.Panjang) / 1000000000 as hasil " +
                             "from BM_Destacking a inner " +
                             "join FC_Items b on a.ItemID = b.id left " +
                             "join MTC_Rekap_Listrik c " +
                             "on Convert(varchar, a.TglProduksi, 112) = Convert(varchar, c.Tanggal, 112) where a.RowStatus > -1 and LEFT(convert(CHAR, tglproduksi, 112), 6)= @tgl) " +
                             "select cast(kwhm3 as decimal(10, 2))actual from( " +
                             "select sum(totalkwh)totalkwh, sum(output)output, (sum(totalkwh) / sum(output))kwhm3 from( " +
                             "select totalkwh, output, kwhm3 from( " +
                             "select  totalkwh, output, (Isnull(totalkwh / NULLIF(output, 0), 0)) as kwhm3 from( " +
                             "select tanggal, " +
                             "case when kWhPJT2 is not null then(result1 * 1000) + kWhPJT2 when kWhPJT2 is null then(result1 * 1000)end + " +
                             "case when kWhPLN2 is not null then(result3 * 6 * 1000) + kWhPLN2 when kWhPLN2 is null then(result3 * 6 * 1000)end as totalkwh, " +
                             "(select sum(hasil)from hitung_output where convert(varchar, TglProduksi, 112) = convert(varchar, rekap.Tanggal, 112)) as output from rekap " +
                             ")x where left(convert(char, Tanggal, 112), 6) = @tgl group by Tanggal, totalkwh, output)y)aa )a";

            }
            else
            {
                zl.CustomQuery = "declare @tgl varchar(6) " +
                             "set @tgl = '" + tgl + "' " +
                             "; WITH rekap AS " +
                             "(SELECT Tanggal, kWhPJT, " +
                             "min(kWhPJT) over( " +
                             "ORDER BY tanggal ROWS BETWEEN 1 following AND 1 following) - kWhPJT AS result1, " +
                             "kWhPLN, " +
                             "min(kWhPLN) over( " +
                             "ORDER BY tanggal ROWS BETWEEN 1 following AND 1 following) - kWhPLN AS result3, " +
                             "kWhPJT2, kWhPLN2 " +
                             "FROM MTC_Rekap_Listrik where convert(char, Tanggal, 112) >= @tgl " +
                             "+ '01' and convert(char, Tanggal, 112) <= left(@tgl, 4) + (DATEADD(month, 1, @tgl + '01'))and rowstatus > -1), " +
                             "hitung_output as( " +
                             "select a.TglProduksi,a.qty, a.itemid, b.Volume,a.Qty * (b.Lebar * b.Tebal * b.Panjang) / 1000000000 as hasil " +
                             "from BM_Destacking a inner " +
                             "join FC_Items b on a.ItemID = b.id left " +
                             "join MTC_Rekap_Listrik c " +
                             "on Convert(varchar, a.TglProduksi, 112) = Convert(varchar, c.Tanggal, 112) where a.RowStatus > -1 and LEFT(convert(CHAR, tglproduksi, 112), 6)= @tgl) " +
                             "select cast(kwhm3 as decimal(10, 2))actual from( " +
                             "select sum(totalkwh)totalkwh, sum(output)output, (sum(totalkwh) / sum(output))kwhm3 from( " +
                             "select totalkwh, output, kwhm3 from( " +
                             "select  totalkwh, output, (Isnull(totalkwh / NULLIF(output, 0), 0)) as kwhm3 from( " +
                             "select tanggal, " +
                             "case when kWhPJT2 is not null then(result1 * 1000) + kWhPJT2 when kWhPJT2 is null then(result1 * 1000)end + " +
                             "case when kWhPLN2 is not null then(result3 * 3 * 1000) + kWhPLN2 when kWhPLN2 is null then(result3 * 3 * 1000)end as totalkwh, " +
                             "(select sum(hasil)from hitung_output where convert(varchar, TglProduksi, 112) = convert(varchar, rekap.Tanggal, 112)) as output from rekap " +
                             ")x where left(convert(char, Tanggal, 112), 6) = @tgl group by Tanggal, totalkwh, output)y)aa )a";
            }

            
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    aktual = Convert.ToDecimal(sdr["actual"].ToString());

                }
            }
            /* tunggu perintah untuk diaktifin
            else if (users.UnitKerjaID == 1)
            {
                if (line == "1")
                {
                    sarmutPrs = "Efisiensi Pemakaian Listrik ( untuk 1 ~ 2 line )";
                }
                else if (line == "4")
                {
                    sarmutPrs = "Efisiensi Pemakaian Listrik ( untuk 3 ~ 4 line )";
                }
            }
            else if (users.UnitKerjaID == 13)
            {
                sarmutPrs = "Efisiensi Pemakaian Listrik";
            }*/

            if (aktual <= 0)
                aktual = 0;
            
            ZetroView zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery =
                "update SPD_TransPrs set actual= " + aktual.ToString("N2").Replace(",", ".") + " where approval=0 and Tahun =" + tahun +
                " and Bulan=" + bulan +
                " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "' ) ";
            SqlDataReader sdr1 = zl1.Retrieve();
            string result = line;
            return result;
        }
    }
}
