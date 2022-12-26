using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using Domain;
using DataAccessLayer;

namespace BusinessFacade
{
    public class ScrubFacade : AbstractFacade
    {

        private Scrub objScrub = new Scrub();
        private ArrayList arrScrub;
        private List<SqlParameter> sqlListParam;
        public string Limit { get; set; }
        public ScrubFacade()
            : base()
        {
           
        }

        public override int Insert(object objDomain)
        {
            try
            {
                objScrub = (Scrub)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@tglinput", objScrub.TglInput));
                sqlListParam.Add(new SqlParameter("@typescrab", objScrub.Typescrab));
                sqlListParam.Add(new SqlParameter("@typeinput", objScrub.Typeinput));
                sqlListParam.Add(new SqlParameter("@jumlah", objScrub.Jumlah));
                sqlListParam.Add(new SqlParameter("@berat", objScrub.Kg));
                sqlListParam.Add(new SqlParameter("@m3", objScrub.M3));
                sqlListParam.Add(new SqlParameter("@createdby", objScrub.Createdby));
                sqlListParam.Add(new SqlParameter("@keterangan", objScrub.Keterangan));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertScrub");
                strError = dataAccess.Error;
                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int InsertLampiran(object objDomain)
        {
            try
            {
                objScrub = (Scrub)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ScrubID", objScrub.ScrubID));
                sqlListParam.Add(new SqlParameter("@filename", objScrub.filename));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertScrub_Lampiran");
                strError = dataAccess.Error;
                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int InsertApproval(object objDomain)
        {
            try
            {
                objScrub = (Scrub)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Bulan", objScrub.Bulan));
                sqlListParam.Add(new SqlParameter("@Tahun", objScrub.Tahun));
                sqlListParam.Add(new SqlParameter("@UserCreate", objScrub.Createdby));

                
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertScrubApproval");

                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public Scrub GenerateObject(SqlDataReader sqlDataReader)
        {
            objScrub = new Scrub();
            objScrub.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objScrub.Tanggal = sqlDataReader["Tanggal"].ToString();
            objScrub.JenisScrab = sqlDataReader["TypeScrab"].ToString();
            objScrub.SatuanBerat = sqlDataReader["Satuan"].ToString();
            objScrub.Jumlah = Convert.ToDecimal(sqlDataReader["Jumlah"].ToString());
            objScrub.Kg = Convert.ToDecimal(sqlDataReader["Berat/Kg"].ToString());
            objScrub.M3 = Convert.ToDecimal(sqlDataReader["M3"].ToString());
            objScrub.Keterangan = sqlDataReader["Keterangan"].ToString();
            objScrub.Createdby = sqlDataReader["Created By"].ToString();

            return objScrub;

        }

        public ArrayList RetrieveScrab()
        {
            string strSQL =
                "select top(1) ID, convert(varchar,tglinput,105)as Tanggal, "+
                "case typescrab "+
                "when Convert(varchar, 1,112) then 'Scrab Kering' "+
                "when Convert(varchar, 2,112) then 'Scrab Basah' end 'TypeScrab', "+
                "case typeinput " +
                "when Convert(varchar, 1,112) then 'Palet' "+
                "when Convert(varchar, 2,112) then 'Bin' end Satuan, "+
                "jumlah Jumlah, berat as 'Berat/Kg',isnull(cast(cast(berat as decimal)/cast(1596 as decimal) as decimal(18,1)),0) M3,keterangan Keterangan, createdby as 'Created By' "+
                "from Scrab where RowStatus >-1 order by id desc";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrScrub = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScrub.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                
                arrScrub.Add (new Scrub());
            return arrScrub;
        }


        public ArrayList RetrieveScabTipe(int pilihan, string tDari, string tSampai) 
        {
            string strSQL = Querypemantauanscrab(pilihan,tDari,tSampai);
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrScrub = new ArrayList();

            int n = 0;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    n = n + 1;
                    arrScrub.Add(new Scrub
                    {
                        Nom = n,
                        Id = Convert.ToInt32(sqlDataReader["ID"].ToString()),
                        Tanggal = sqlDataReader["Tanggal"].ToString(),
                        JenisScrab = sqlDataReader["TypeScrab"].ToString(),
                        Palet = sqlDataReader["Palet"].ToString(),
                        PaletJumlah = Convert.ToInt32(sqlDataReader["Jumlah(Palet)"].ToString()),
                        BeratPalet = Convert.ToDecimal(sqlDataReader["Berat(Palet)Kg"].ToString()),
                        M3Palet = Convert.ToDecimal(sqlDataReader["M3/Palet"].ToString()),
                        KeteranganPalet = sqlDataReader["Keterangan(Palet)"].ToString(),
                        TangglKedua =sqlDataReader["Tanggal1"].ToString(),
                        JenisScrabkedua = sqlDataReader["TypeScrab1"].ToString(),
                        Bin = sqlDataReader["Bin"].ToString(),
                        BinJumlah = Convert.ToInt32(sqlDataReader["Jumlah(Bin)"].ToString()),
                        BeratBin = Convert.ToDecimal(sqlDataReader["Berat(Bin)Kg"].ToString()),
                        M3Bin = Convert.ToDecimal(sqlDataReader["M3/Bin"].ToString()),
                        KeteranganBin = sqlDataReader["Keterangan(Bin)"].ToString(),
                        TotalM3 = Convert.ToDecimal(sqlDataReader["Total M3"].ToString()),
                        Lampiran = sqlDataReader["Lampiran"].ToString()

                        
                        
                    });
                }
            }
            else
            {
                arrScrub.Add(new Scrub());
            }
            return arrScrub;

        }

        

        private string Querypemantauanscrab(int pilihan, string tDari, string tSampai)
        {
            string pilihanscrab = string.Empty;
            pilihanscrab = (pilihan == 0) ? "" : " and typescrab =" + pilihan;
            return
                ";with "+
                "hasil as ( "+
                "select * from ( " +
                "select ID, isnull(convert(varchar,tglinput,105),0)as Tanggal, "+
                "case typescrab "+
                "when isnull(1,0) then 'Scrab Kering' "+ 
                "when isnull(2,0) then 'Scrab Basah' end 'TypeScrab', "+ 

                "case typeinput "+ 
                "when isnull(1,0) then 'Palet' else '' end Palet, "+
                "isnull(jumlah,0) as 'Jumlah(Palet)', "+ 
                "isnull(berat,0) as 'Berat(Palet)Kg', "+
                //"isnull(m3,0) as 'M3/Palet', "+
                "isnull((CAST(berat as decimal))/CAST(1596 as decimal),0) as 'M3/Palet', "+
                "keterangan as 'Keterangan(Palet)' from Scrab where tglinput between '"+tDari+"' and '"+tSampai+"' and RowStatus >-1  and typeinput=1 "+pilihanscrab+") A "+

                "full outer join ( "+
                "select isnull(convert(varchar,tglinput,105),0) as Tanggal1, "+
                "case typescrab "+
                "when isnull(1,0) then 'Scrab Kering' "+ 
                "when isnull(2,0) then 'Scrab Basah' end 'TypeScrab1', "+ 
                "case typeinput "+
                "when isnull(2,0) then 'Bin' else '' end Bin, "+ 
                "isnull(jumlah,0) as 'Jumlah(Bin)', "+
                "isnull(berat,0) as 'Berat(Bin)Kg', "+
                //"isnull(m3,0) as 'M3/Bin', "+
                "isnull(CAST(berat as decimal)/CAST(1595 as decimal),0) as 'M3/Bin', "+
                "keterangan 'Keterangan(Bin)' from Scrab where tglinput between '"+tDari+"' and '"+tSampai+"' and RowStatus >-1  and typeinput=2 "+pilihanscrab+") "+
                "B on A.Tanggal =B.Tanggal1) "+
              
                "select Tanggal,isnull(a.ID,0)as ID,TypeScrab,Palet,isnull([Jumlah(Palet)],0) as 'Jumlah(Palet)', isnull([Berat(Palet)Kg],0) as 'Berat(Palet)Kg' ,isnull([M3/Palet],0) as 'M3/Palet', "+
                "[Keterangan(Palet)],Tanggal1,TypeScrab1,Bin,isnull([Jumlah(Bin)],0) as 'Jumlah(Bin)',isnull([Berat(Bin)Kg],0) as 'Berat(Bin)Kg',isnull([M3/Bin],0) as 'M3/Bin', "+
                "[Keterangan(Bin)],isnull([M3/Palet],0) + isnull([M3/Bin],0) as 'Total M3',isnull(b.NamaFile,'-')Lampiran  from hasil a "+
                "left join Scrab_Lampiran b ON a.ID=b.ScrabID and b.RowStatus>-1 ";



        }

       

        public Scrub RetrieveByUnitKerjaID(int UnitKerjaID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select BeratPalet,BeratBin from Master_Berat where UnitKerjaID = " + UnitKerjaID +" ");
            strError = dataAccess.Error;
            arrScrub = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject1(sqlDataReader);
                }
            }

            return new Scrub();
        }

        
        public Scrub GenerateObject1(SqlDataReader sqlDataReader)
        {
            objScrub = new Scrub();
            
            objScrub.BeratBin = Convert.ToDecimal(sqlDataReader["beratBin"].ToString());
            objScrub.BeratPalet = Convert.ToDecimal(sqlDataReader["beratPalet"].ToString());
            
            return objScrub;

        }

        public Scrub GenerateObjectStatusApproval(SqlDataReader sqlDataReader)
        {
            objScrub = new Scrub();

            objScrub.StatusApprovalSemua = sqlDataReader["StatusApproval"].ToString();
            
            return objScrub;

        }

        

        public override int Update(object objDomain)
        {
            try 
            {
                objScrub = (Scrub)objDomain;
                sqlListParam = new List<SqlParameter>();
                
                sqlListParam.Add(new SqlParameter("@Bulan", objScrub.Bulan));
                sqlListParam.Add(new SqlParameter("@Tahun", objScrub.Tahun));
                sqlListParam.Add(new SqlParameter("@Apv", objScrub.Level));


                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateScrubApprovalUser");

                strError = dataAccess.Error;

                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Delete(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }

        public string statusScrubAppovalTahun()
        {
            string hasilTahun = string.Empty;
         
            //string strsql = "select Tahun from Scrab_Approval Where Tahun="+tahun+" and  RowStatus >-1";
            string strsql = "select Tahun from Scrab_Approval Where Tahun in(select Tahun from Scrab_Approval  where StatusApv in(0,1,2,3,4) and RowStatus >-1)  and RowStatus >-1";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strsql);
            
            if (da.Error == string.Empty || sdr.HasRows)
            {
                while (sdr.Read())
                {
                    hasilTahun = sdr["Tahun"].ToString();
                    
                }
            }
            
            return hasilTahun;
        }

        public string cekTahunSimpan(string tahun)
        {
            string hasilTahun = string.Empty;

            //string strsql = "select Tahun from Scrab_Approval Where Tahun="+tahun+" and  RowStatus >-1";
            string strsql = "select Tahun from Scrab_Approval Where Tahun in(select Tahun from Scrab_Approval  where StatusApv in(0,1,2,3,4) and tahun='"+tahun+"' and RowStatus >-1)  and RowStatus >-1";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strsql);

            if (da.Error == string.Empty || sdr.HasRows)
            {
                while (sdr.Read())
                {
                    hasilTahun = sdr["Tahun"].ToString();

                }
            }

            return hasilTahun;
        }

        public string cekBulanSimpan(string bulan)
        {
            string hasilTahun = string.Empty;

            //string strsql = "select Tahun from Scrab_Approval Where Tahun="+tahun+" and  RowStatus >-1";
            string strsql = "select Bulan from Scrab_Approval Where Bulan in(select Bulan from Scrab_Approval  where StatusApv in(0,1,2,3,4) and Bulan='" + bulan + "' and RowStatus >-1)  and RowStatus >-1";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strsql);

            if (da.Error == string.Empty || sdr.HasRows)
            {
                while (sdr.Read())
                {
                    hasilTahun = sdr["bulan"].ToString();

                }
            }

            return hasilTahun;
        }

        public string statusScrubAppovalBulan()
        {
            string hasilBulan = string.Empty;

            //string strsql = "select Bulan from Scrab_Approval Where Bulan=" + bulan + " and  RowStatus >-1";
            string strsql = "select Bulan from Scrab_Approval Where Bulan in(select Bulan from Scrab_Approval  where StatusApv in(0,1,2,3,4) and RowStatus >-1)  and RowStatus >-1";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strsql);
            if (da.Error == string.Empty || sdr.HasRows)
            {
                while (sdr.Read())
                {
                    hasilBulan = sdr["Bulan"].ToString();
                }
            }

            return hasilBulan;
        }

        public string statusScrubAppovalTahunBulan(string bulan, string tahun)
        {
            string statusApproval = string.Empty;

            string strsql =
            "select " +
            "case StatusApv "+
            "when 0 then 'Open' "+
            "when 1 then 'Head' "+
            "when 2 then 'Mgr' "+
            "when 3 then 'Plant Mgr' end StatusApproval "+
            "from Scrab_Approval Where Bulan='"+bulan+"' and Tahun='"+tahun+"' and  RowStatus >-1";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strsql);
            if (da.Error == string.Empty || sdr.HasRows)
            {
                while (sdr.Read())
                {
                    statusApproval = sdr["StatusApproval"].ToString();
                }
            }

            return statusApproval;
        }

        public string statusApproval(string Bulan, string Tahun)
        {
            string hasil = string.Empty;

            string strsql =
            "select " +
            "case StatusApv " +
            "when 0 then 'Open' " +
            "when 1 then 'Head' " +
            "when 2 then 'Mgr' " +
            "when 3 then 'Plant Mgr' end StatusApproval " +
            "from Scrab_Approval Where Bulan='"+Bulan+"' and Tahun='" + Tahun + "' and  RowStatus >-1";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strsql);
            if (da.Error == string.Empty || sdr.HasRows)
            {
                while (sdr.Read())
                {
                    hasil = sdr["StatusApproval"].ToString();
                }
            }

            return hasil;
        }



        public string CekUserApproval(string NamaUser)
        {
            string statusApproval = string.Empty;

            string strsql =
            "select UserName from Scrab_AppUser where username='" + NamaUser + "' ";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strsql);
            if (da.Error == string.Empty || sdr.HasRows)
            {
                while (sdr.Read())
                {
                    statusApproval = sdr["UserName"].ToString();
                }
            }

            return statusApproval;
        }

        public string levelApprove(string NamaUser)
        {
            string statusApproval = string.Empty;

            string strsql =
            "select LevelApv from Scrab_AppUser where username='" + NamaUser + "' ";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strsql);
            if (da.Error == string.Empty || sdr.HasRows)
            {
                while (sdr.Read())
                {
                    statusApproval = sdr["LevelApv"].ToString();
                }
            }

            return statusApproval;
        }

        public string OpenApproval(string Approval)
        {
            string hasil = string.Empty;

            string strsql =
            "select " +
            "case StatusApv " +
            "when 0 then 'Open' " +
            "when 1 then 'Head' " +
            "when 2 then 'Mgr' " +
            "when 3 then 'Plant Mgr' end StatusApproval " +
            "from Scrab_Approval Where StatusApv=" + Approval + " and  RowStatus >-1";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strsql);
            if (da.Error == string.Empty || sdr.HasRows)
            {
                while (sdr.Read())
                {
                    hasil = sdr["StatusApproval"].ToString();
                }
            }

            return hasil;
        }

        public ArrayList RetrievePDF(string ba)
        {
            string strSQL = " select ID,NamaFile filename from Scrab_Lampiran where ScrabID='" + ba + "' and RowStatus>-1 ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrScrub = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrScrub.Add(GenerateObject6(sqlDataReader));
                }
            }
            else
                arrScrub.Add(new Scrub());

            return arrScrub;
        }

        public Scrub GenerateObject6(SqlDataReader sqlDataReader)
        {
            objScrub = new Scrub();
            objScrub.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objScrub.filename = sqlDataReader["filename"].ToString();
            //objUPD2.attachfile = (byte[])sqlDataReader["AttachFile"];
            return objScrub;
        }

    }
}
