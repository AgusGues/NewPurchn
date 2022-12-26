using BusinessFacade;
using Dapper;
using DataAccessLayer;
using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace BusinessFacade
{
    public class SarmutFacade
    {
        public static List<SPD_Sarmut> GetSarmutKrwg(int Tahun)
        {

            List<SPD_Sarmut> AllData = new List<SPD_Sarmut>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select ID, PlantID, Tipe, Urutan, Tahun, NO, Dimensi, SarMutPerusahaan, Dept, SarMutDepartemen, ParameterTerukur, " +
                    "case when ISNUMERIC(Jan) = 1 then  cast(cast(REPLACE(Jan,',','.') as decimal(18, 2)) as char) else jan end jan,  " +
                    "case when ISNUMERIC(Feb) = 1 then  cast(cast(REPLACE(Feb,',','.') as decimal(18, 2)) as char) else Feb end Feb,  " +
                    "case when ISNUMERIC(Mar) = 1 then  cast(cast(REPLACE(Mar,',','.') as decimal(18, 2)) as char) else Mar end Mar,  " +
                    "case when ISNUMERIC(Apr) = 1 then  cast(cast(REPLACE(Apr,',','.') as decimal(18, 2)) as char) else Apr end Apr,  " +
                    "case when ISNUMERIC(Mei) = 1 then  cast(cast(REPLACE(Mei,',','.') as decimal(18, 2)) as char) else Mei end Mei,  " +
                    "case when ISNUMERIC(Jun) = 1 then  cast(cast(REPLACE(Jun,',','.') as decimal(18, 2)) as char) else Jun end Jun,  " +
                    "case when ISNUMERIC(SMI) = 1 then  cast(cast(REPLACE(SMI,',','.') as decimal(18, 2)) as char) else SMI end SMI,  " +
                    "case when ISNUMERIC(Jul) = 1 then  cast(cast(REPLACE(Jul,',','.') as decimal(18, 2)) as char) else Jul end Jul,  " +
                    "case when ISNUMERIC(Agu) = 1 then  cast(cast(REPLACE(Agu,',','.') as decimal(18, 2)) as char) else Agu end Agu,  " +
                    "case when ISNUMERIC(Sep) = 1 then  cast(cast(REPLACE(Sep,',','.') as decimal(18, 2)) as char) else Sep end Sep,  " +
                    "case when ISNUMERIC(Okt) = 1 then  cast(cast(REPLACE(Okt,',','.') as decimal(18, 2)) as char) else Okt end Okt,  " +
                    "case when ISNUMERIC(Nov) = 1 then  cast(cast(REPLACE(Nov,',','.') as decimal(18, 2)) as char) else Nov end Nov,  " +
                    "case when ISNUMERIC(Des) = 1 then  cast(cast(REPLACE(Des,',','.') as decimal(18, 2)) as char) else Des end Des,  " +
                    "case when ISNUMERIC(SMII) = 1 then  cast(cast(REPLACE(SMII,',','.') as decimal(18, 2)) as char) else SMII end SMII, " +
                    "RowStatus from SPD_Pencapaiannilai where tahun=" + Tahun + " order by urutan asc";
                    AllData = connection.Query<SPD_Sarmut>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }


        public static List<SPD_Sarmut> GetSarmutCtrp(int Tahun)
        {

            List<SPD_Sarmut> AllData = new List<SPD_Sarmut>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select ID, PlantID, Tipe, Urutan, Tahun, NO, Dimensi, SarMutPerusahaan, Dept, SarMutDepartemen, ParameterTerukur, " +
                    "case when ISNUMERIC(Jan) = 1 then  cast(cast(REPLACE(Jan,',','.') as decimal(18, 2)) as char) else jan end jan,  " +
                    "case when ISNUMERIC(Feb) = 1 then  cast(cast(REPLACE(Feb,',','.') as decimal(18, 2)) as char) else Feb end Feb,  " +
                    "case when ISNUMERIC(Mar) = 1 then  cast(cast(REPLACE(Mar,',','.') as decimal(18, 2)) as char) else Mar end Mar,  " +
                    "case when ISNUMERIC(Apr) = 1 then  cast(cast(REPLACE(Apr,',','.') as decimal(18, 2)) as char) else Apr end Apr,  " +
                    "case when ISNUMERIC(Mei) = 1 then  cast(cast(REPLACE(Mei,',','.') as decimal(18, 2)) as char) else Mei end Mei,  " +
                    "case when ISNUMERIC(Jun) = 1 then  cast(cast(REPLACE(Jun,',','.') as decimal(18, 2)) as char) else Jun end Jun,  " +
                    "case when ISNUMERIC(SMI) = 1 then  cast(cast(REPLACE(SMI,',','.') as decimal(18, 2)) as char) else SMI end SMI,  " +
                    "case when ISNUMERIC(Jul) = 1 then  cast(cast(REPLACE(Jul,',','.') as decimal(18, 2)) as char) else Jul end Jul,  " +
                    "case when ISNUMERIC(Agu) = 1 then  cast(cast(REPLACE(Agu,',','.') as decimal(18, 2)) as char) else Agu end Agu,  " +
                    "case when ISNUMERIC(Sep) = 1 then  cast(cast(REPLACE(Sep,',','.') as decimal(18, 2)) as char) else Sep end Sep,  " +
                    "case when ISNUMERIC(Okt) = 1 then  cast(cast(REPLACE(Okt,',','.') as decimal(18, 2)) as char) else Okt end Okt,  " +
                    "case when ISNUMERIC(Nov) = 1 then  cast(cast(REPLACE(Nov,',','.') as decimal(18, 2)) as char) else Nov end Nov,  " +
                    "case when ISNUMERIC(Des) = 1 then  cast(cast(REPLACE(Des,',','.') as decimal(18, 2)) as char) else Des end Des,  " +
                    "case when ISNUMERIC(SMII) = 1 then  cast(cast(REPLACE(SMII,',','.') as decimal(18, 2)) as char) else SMII end SMII, " +
                    "RowStatus from [sqlctrp.grcboard.com].bpasctrp.dbo.SPD_Pencapaiannilai where tahun=" + Tahun + " order by urutan asc";
                    AllData = connection.Query<SPD_Sarmut>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }


        public static List<SPD_Sarmut> GetSarmutJmng(int Tahun)
        {

            List<SPD_Sarmut> AllData = new List<SPD_Sarmut>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select ID, PlantID, Tipe, Urutan, Tahun, NO, Dimensi, SarMutPerusahaan, Dept, SarMutDepartemen, ParameterTerukur, " +
                    "case when ISNUMERIC(Jan) = 1 then  cast(cast(REPLACE(Jan,',','.') as decimal(18, 2)) as char) else jan end jan,  " +
                    "case when ISNUMERIC(Feb) = 1 then  cast(cast(REPLACE(Feb,',','.') as decimal(18, 2)) as char) else Feb end Feb,  " +
                    "case when ISNUMERIC(Mar) = 1 then  cast(cast(REPLACE(Mar,',','.') as decimal(18, 2)) as char) else Mar end Mar,  " +
                    "case when ISNUMERIC(Apr) = 1 then  cast(cast(REPLACE(Apr,',','.') as decimal(18, 2)) as char) else Apr end Apr,  " +
                    "case when ISNUMERIC(Mei) = 1 then  cast(cast(REPLACE(Mei,',','.') as decimal(18, 2)) as char) else Mei end Mei,  " +
                    "case when ISNUMERIC(Jun) = 1 then  cast(cast(REPLACE(Jun,',','.') as decimal(18, 2)) as char) else Jun end Jun,  " +
                    "case when ISNUMERIC(SMI) = 1 then  cast(cast(REPLACE(SMI,',','.') as decimal(18, 2)) as char) else SMI end SMI,  " +
                    "case when ISNUMERIC(Jul) = 1 then  cast(cast(REPLACE(Jul,',','.') as decimal(18, 2)) as char) else Jul end Jul,  " +
                    "case when ISNUMERIC(Agu) = 1 then  cast(cast(REPLACE(Agu,',','.') as decimal(18, 2)) as char) else Agu end Agu,  " +
                    "case when ISNUMERIC(Sep) = 1 then  cast(cast(REPLACE(Sep,',','.') as decimal(18, 2)) as char) else Sep end Sep,  " +
                    "case when ISNUMERIC(Okt) = 1 then  cast(cast(REPLACE(Okt,',','.') as decimal(18, 2)) as char) else Okt end Okt,  " +
                    "case when ISNUMERIC(Nov) = 1 then  cast(cast(REPLACE(Nov,',','.') as decimal(18, 2)) as char) else Nov end Nov,  " +
                    "case when ISNUMERIC(Des) = 1 then  cast(cast(REPLACE(Des,',','.') as decimal(18, 2)) as char) else Des end Des,  " +
                    "case when ISNUMERIC(SMII) = 1 then  cast(cast(REPLACE(SMII,',','.') as decimal(18, 2)) as char) else SMII end SMII, " +
                    "RowStatus from [sqljombang.grcboard.com].bpasjombang.dbo.SPD_Pencapaiannilai where tahun=" + Tahun + " order by urutan asc";
                    AllData = connection.Query<SPD_Sarmut>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }
    }
}
