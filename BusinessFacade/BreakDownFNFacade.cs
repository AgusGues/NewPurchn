using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using BusinessFacade;
using DataAccessLayer;
using Domain;

namespace BusinessFacade
{
    public class BreakDownFNFacade : AbstractFacade
    {
        private BreakDownFN objBdFN = new BreakDownFN();
        private ArrayList arrBdFN;
        private ArrayList arrData = new ArrayList();
        private List<SqlParameter> sqlListParam;

        public BreakDownFNFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objBdFN = (BreakDownFN)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@NoUrut", objBdFN.NoUrut));
                sqlListParam.Add(new SqlParameter("@BreakNo", objBdFN.BreakNo));
                sqlListParam.Add(new SqlParameter("@TanggalBreak", objBdFN.TanggalBreak));
                sqlListParam.Add(new SqlParameter("@OvenID", objBdFN.OvenID));
                sqlListParam.Add(new SqlParameter("@GroupOvenID", objBdFN.GroupOvenID));
                sqlListParam.Add(new SqlParameter("@Uraian", objBdFN.Uraian));
                sqlListParam.Add(new SqlParameter("@Frek", objBdFN.Frek));
                sqlListParam.Add(new SqlParameter("@Waktu", objBdFN.Waktu));
                sqlListParam.Add(new SqlParameter("@NmMasterCatID", objBdFN.NmMasterCatID));
                sqlListParam.Add(new SqlParameter("@WaktuOprsnl", objBdFN.WaktuOprsnl));
                //sqlListParam.Add(new SqlParameter("@Apv", objBdFN.Apv));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objBdFN.CreatedBy));

                int IntResult = dataAccess.ProcessData(sqlListParam, "spInsertBreakDownFN");
                strError = dataAccess.Error;
                return IntResult;
            }
            catch (Exception Ex)
            {
                strError = Ex.Message;
                return -1;
            }
        }

        public override int Update(object objDomain)
        {
            try
            {
                objBdFN = (BreakDownFN)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objBdFN.ID));
                sqlListParam.Add(new SqlParameter("@NoUrut", objBdFN.NoUrut));
                sqlListParam.Add(new SqlParameter("@BreakNo", objBdFN.BreakNo));
                sqlListParam.Add(new SqlParameter("@TanggalBreak", objBdFN.TanggalBreak));
                sqlListParam.Add(new SqlParameter("@OvenID", objBdFN.OvenID));
                sqlListParam.Add(new SqlParameter("@GroupOvenID", objBdFN.GroupOvenID));
                sqlListParam.Add(new SqlParameter("@Uraian", objBdFN.Uraian));
                sqlListParam.Add(new SqlParameter("@Frek", objBdFN.Frek));
                sqlListParam.Add(new SqlParameter("@Waktu", objBdFN.Waktu));
                sqlListParam.Add(new SqlParameter("@NmMasterCatID", objBdFN.NmMasterCatID));
                sqlListParam.Add(new SqlParameter("@Apv", objBdFN.Apv));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objBdFN.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedTime", objBdFN.LastModifiedTime));

                int IntResult = dataAccess.ProcessData(sqlListParam, "spUpdateBreakDownFN");
                strError = dataAccess.Error;
                return IntResult;
            }
            catch (Exception Ex)
            {
                strError = Ex.Message;
                return -1;
            }
        }

        public int UpdateRowStatus(object objDomain)
        {
            int result = 0;
            DataAccess da = new DataAccess(Global.ConnectionString());
            sqlListParam = new List<SqlParameter>();
            objBdFN = (BreakDownFN)objDomain;
            SqlDataReader sdr = da.RetrieveDataByString("Update FnBreakDown set RowStatus = -1 where ID=" + objBdFN.ID);
            result = sdr.RecordsAffected;
            return result;
        }

        public override int Delete(object objDomain)
        {
            try
            {
                objBdFN = (BreakDownFN)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objBdFN.ID));
                sqlListParam.Add(new SqlParameter("@", objBdFN.LastModifiedBy));

                int IntResult = dataAccess.ProcessData(sqlListParam, "spDeleteBreakDownFN");
                strError = dataAccess.Error;
                return IntResult;
            }
            catch (Exception Ex)
            {
                strError = Ex.Message;
                return -1;
            }
        }

        public override ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }

        public ArrayList RetrieveNamaOven()
        {
            DataAccess da = new DataAccess(Global.ConnectionString());
            string strSQL = "select * From FnBreakDown_MasterOven where RowStatus>-1";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrBdFN = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBdFN.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrBdFN.Add(new BreakDownFN());

            return arrBdFN;
        }

        public ArrayList RetrieveCategoryUraian()
        {
            DataAccess da = new DataAccess(Global.ConnectionString());
            string strSQL = "select * From FnBreakDown_NmMasterCat where RowStatus>-1";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrBdFN = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBdFN.Add(GenerateObject5(sqlDataReader));
                }
            }
            else
                arrBdFN.Add(new BreakDownFN());

            return arrBdFN;
        }

        public ArrayList RetrieveSGroup()
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select* From FnBreakDown_MasterGroupOven where RowStatus>-1");
            strError = dataAccess.Error;
            arrBdFN = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBdFN.Add(GenerateObject3(sqlDataReader));
                }
            }
            else
                arrBdFN.Add(new BreakDownFN());

            return arrBdFN;
        }

        public ArrayList RetrieveKategori()
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from FnBreakDown_NmMasterCat where RowStatus>-1");
            strError = dataAccess.Error;
            arrBdFN = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBdFN.Add(GenerateObject4(sqlDataReader));
                }
            }
            else
                arrBdFN.Add(new BreakDownFN());

            return arrBdFN;
        }

        public int Tahun { get; set; }
        public int RetrieveMaxId()
        {
            int result = 0;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select TOP 1 ISNULL(NoUrut,0) NoUrut from FnBreakDown where RowStatus>-1 AND Year(TanggalBreak)=" + this.Tahun + " ORDER BY NOURUT DESC";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrBdFN = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    result = int.Parse(sqlDataReader["NoUrut"].ToString());
                }
            }

            return result;

        }

        public ArrayList RetrieveListBdtFN(string ovenid)
        {
            arrBdFN = new ArrayList();
            string where = (ovenid == "0") ? "" : " where x.OvenID=" + ovenid;
            string strSql = " select  x.OvenID,z.NamaOven from FnBreakDown x left join FnBreakDown_MasterOven z on z.ID=x.OvenID " + where + "  Group by x.OvenID,z.NamaOven order by x.OvenID asc";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSql);
            if (dataAccess.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrBdFN.Add(GenerateObjecttGl(sdr));
                }
            }
            return arrBdFN;
        }

        
        public ArrayList RetrieveListBdtFNAll(string tahun, string bulan, string ovenid, string criteriaid)
        {
            string UnitKerja = ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID.ToString();
            string kriteria = string.Empty;
            
            
            if (ovenid != "0")
                kriteria = kriteria + " and A.OvenID=" + ovenid;

            if (criteriaid != "0")
                kriteria = kriteria + "  and A.NmMasterCatID=" + criteriaid;
            
            //if (ovenid != "0" || criteriaid !="0") 
            //{
            //    kriteria = kriteria + " and R.OvenID=" + ovenid + " and R.NmMasterCatID=" + criteriaid;
            //}
                

            arrBdFN = new ArrayList();
            string strSQL = string.Empty;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());

            strSQL =
                    "EXEC('IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].[TotalWaktu]'') AND type in (N''U'')) DROP TABLE [dbo].[TotalWaktu]')" +
                    "CREATE TABLE TotalWaktu( " +
                    "ID int,NoUrut int, BreakNo varchar(50),TanggalBreak varchar(100),OvenID int,NamaOven varchar(50),NamaGroupOven varchar(10),GroupOvenID int,Uraian varchar(250)," +
                    "Frek int,waktu decimal(18,2),NmMasterCatID int, UraianCat varchar(50),Apv int, RowStatus int)" +

                    //"insert into TotalWaktu " +

                    //"select 0 as ID,0 as NoUrut,'' as BreakNo,'' as TanggalBreak,A.OvenID,'' as NamaOven,'' as NamaGroupOven,0 as GroupOvenID,'' as Uraian," +
                    //"0 as Frek," +

                    //"case A.NmMasterCatID " +
                    //"when (18) then isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) " +
                    //"when (20) then isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) " +
                    //"when (21) then isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) " +
                    //"when (22) then isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) end Waktu, " +

                    //"A.NmMasterCatID,D.UraianCat,0 as Apv,0 as RowStatus from  FnBreakDown A " +
                    //"inner join FnBreakDown_MasterOven B on A.OvenID=B.ID " +
                    //"inner join FnBreakDown_MasterGroupOven C on A.GroupOvenID=C.ID " +
                    //"inner join FnBreakDown_NmMasterCat D on A.NmMasterCatID=D.ID where A.RowStatus >-1 " +
                    //"and MONTH(A.TanggalBreak)='"+bulan+"' and YEAR(TanggalBreak)='"+tahun+"' and A.NmMasterCatID in(18,20,21,22) " +
                    //""+kriteria+" group by A.NmMasterCatID,A.OvenID,D.UraianCat " +

                    //";with q as ( " +
                    //"select count(A.ID) as ID,0 as NoUrut,'' as BreakNo,'' as TanggalBreak,A.OvenID,'' as NamaOven,'' as NamaGroupOven,0 as GroupOvenID, " +
                    //"'' as Uraian,0 as Frek, " +

                    //"case A.NmMasterCatID " +
                    //"when (18) then isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) " +
                    //"when (20) then isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) " +
                    //"when (21) then isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) " +
                    //"when (22) then isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) end Waktu, " +

                    //"A.NmMasterCatID,D.UraianCat,0 as Apv,0 as RowStatus from  FnBreakDown A " +
                    //"inner join FnBreakDown_MasterOven B on A.OvenID=B.ID " +
                    //"inner join FnBreakDown_MasterGroupOven C on A.GroupOvenID=C.ID " +
                    //"inner join FnBreakDown_NmMasterCat D on A.NmMasterCatID=D.ID where A.RowStatus >-1 and MONTH(A.TanggalBreak)='" + bulan + "' and YEAR(TanggalBreak)='" + tahun + "' and A.NmMasterCatID in(18,20,21,22) " +
                    //" "+kriteria+" group by A.NmMasterCatID,A.OvenID,D.UraianCat ) " +

                    //"insert into TotalWaktu " +
                    //"select ID,NoUrut,BreakNo,TanggalBreak,OvenID,NamaOven,NamaGroupOven,GroupOvenID,Uraian,Frek," +

                    
                    //"CASE  DAY(EOMONTH('"+tahun+"-"+bulan+"-"+01+"'))" +
                    //"WHEN 30 THEN isnull(cast(90 as decimal(18,2)),0.00) - isnull(cast(Waktu as decimal(18,2)),0.00) " +
                    //"WHEN 31 THEN isnull(cast(93 as decimal(18,2)),0.00) - isnull(cast(Waktu as decimal(18,2)),0.00) END Waktu, " +

                    //"NmMasterCatID,UraianCat,Apv,RowStatus from q where NmMasterCatID in(18,20,21,22)" +

                    "insert Into TotalWaktu " +
                    "select 0 as ID,1 as NoUrut,'' as BreakNo,'Total Waktu' as TanggalBreak,A.OvenID,'' as NamaOven,'' as NamaGroupOven, 0 as GroupOvenID,'' as Uraian,0 as Frek, " +
                    "isnull(cast(sum(A.Waktu)as decimal(18,2)),0.00) as Waktu,A.NmMasterCatID,D.UraianCat,0 as Apv,0 as RowStatus from  FnBreakDown A " +
                    "inner join FnBreakDown_MasterOven B on A.OvenID=B.ID " +
                    "inner join FnBreakDown_MasterGroupOven C on A.GroupOvenID=C.ID " +
                    "inner join FnBreakDown_NmMasterCat D on A.NmMasterCatID=D.ID where A.RowStatus >-1 and MONTH(A.TanggalBreak)='" + bulan + "' and YEAR(TanggalBreak)='" + tahun + "' " +
                    " " + kriteria + " group by A.NmMasterCatID,A.OvenID,D.UraianCat " +

                    "insert into TotalWaktu " +
                    "select A.ID,A.NoUrut,A.BreakNo,Convert(varchar,A.TanggalBreak,23)TanggalBreak,A.OvenID,B.NamaOven,C.NamaGroupOven,A.GroupOvenID,A.Uraian,A.Frek," +
                    "isnull(cast(A.Waktu as decimal(18,2)),0.00) as Waktu,A.NmMasterCatID,D.UraianCat,A.Apv,A.RowStatus from FnBreakDown A " +
                    "inner join FnBreakDown_MasterOven B on A.OvenID=B.ID " +
                    "inner join FnBreakDown_MasterGroupOven C on A.GroupOvenID=C.ID " +
                    "inner join FnBreakDown_NmMasterCat D on A.NmMasterCatID=D.ID where A.RowStatus >-1 and MONTH(TanggalBreak)='"+bulan+"' and YEAR(TanggalBreak)='"+tahun+"' " +
                    " "+kriteria+" order by A.NmMasterCatID,A.OvenID, TanggalBreak asc " +

                    "select ID,NoUrut,BreakNo,Convert(varchar,TanggalBreak,23)TanggalBreak,OvenID,NamaOven,NamaGroupOven,GroupOvenID,Uraian,Frek,isnull(cast(Waktu as decimal(18,2)),0.00) as Waktu, " +
                    "NmMasterCatID,UraianCat,Apv,RowStatus from TotalWaktu ORDER By OvenID,NmMasterCatID,NoUrut desc " +
                    "EXEC('IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].[TotalWaktu]'') AND type in (N''U'')) DROP TABLE [dbo].[TotalWaktu]') ";

            SqlDataReader sdr = dataAccess.RetrieveDataByString(strSQL);

            if (dataAccess.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrBdFN.Add(GenerateObjecttG2(sdr));
                }
            }
            return arrBdFN;
        }

        public ArrayList RetrieveListBdtFNAllPemotongan(string tahun, string bulan, string ovenid, string criteriaid)
        {
            string UnitKerja = ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID.ToString();
            string kriteria = string.Empty;


            if (ovenid != "0")
                kriteria = kriteria + " and A.OvenID=" + ovenid;

            if (criteriaid != "0")
                kriteria = kriteria + "  and A.NmMasterCatID=" + criteriaid;

            //if (ovenid != "0" || criteriaid !="0") 
            //{
            //    kriteria = kriteria + " and R.OvenID=" + ovenid + " and R.NmMasterCatID=" + criteriaid;
            //}


            arrBdFN = new ArrayList();
            string strSQL = string.Empty;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());

            strSQL =
                    "EXEC('IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].[TotalWaktu]'') AND type in (N''U'')) DROP TABLE [dbo].[TotalWaktu]')" +
                    "CREATE TABLE TotalWaktu( " +
                    "ID int,NoUrut int, BreakNo varchar(50),TanggalBreak varchar(100),OvenID int,NamaOven varchar(50),NamaGroupOven varchar(10),GroupOvenID int,Uraian varchar(250)," +
                    "Frek int,waktu decimal(18,2),NmMasterCatID int, UraianCat varchar(50),Apv int, RowStatus int)" +
                    "CREATE TABLE TotalWaktuBreakDown( ID int,NoUrut int, BreakNo varchar(50),TanggalBreak varchar(100),OvenID int,NamaOven varchar(50),NamaGroupOven varchar(10),GroupOvenID int,Uraian varchar(250),Frek int,waktu decimal(18,2),NmMasterCatID int, UraianCat varchar(50),Apv int, RowStatus int) " +
                    
                    //"insert into TotalWaktu " +

                    //"select 0 as ID,0 as NoUrut,'' as BreakNo,'Total Waktu' as TanggalBreak,A.OvenID,'' as NamaOven,'' as NamaGroupOven,0 as GroupOvenID,'' as Uraian," +
                    //"0 as Frek," +

                    //"case A.NmMasterCatID " +
                    //"when (18) then isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) " +
                    //"when (20) then isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) " +
                    //"when (21) then isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) " +
                    //"when (22) then isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) end Waktu, " +

                    //"A.NmMasterCatID,D.UraianCat,0 as Apv,0 as RowStatus from  FnBreakDown A " +
                    //"inner join FnBreakDown_MasterOven B on A.OvenID=B.ID " +
                    //"inner join FnBreakDown_MasterGroupOven C on A.GroupOvenID=C.ID " +
                    //"inner join FnBreakDown_NmMasterCat D on A.NmMasterCatID=D.ID where A.RowStatus >-1 " +
                    //"and MONTH(A.TanggalBreak)='" + bulan + "' and YEAR(TanggalBreak)='" + tahun + "' and A.NmMasterCatID in(18,20,21,22) " +
                    //"" + kriteria + " group by A.NmMasterCatID,A.OvenID,D.UraianCat " +

                    //";with q as ( " +
                    //"select count(A.ID) as ID,0 as NoUrut,'' as BreakNo,'Total Waktu' as TanggalBreak,A.OvenID,'' as NamaOven,'' as NamaGroupOven,0 as GroupOvenID, " +
                    //"'' as Uraian,0 as Frek, " +

                    //"case A.NmMasterCatID " +
                    //"when (18) then isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) " +
                    //"when (20) then isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) " +
                    //"when (21) then isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) " +
                    //"when (22) then isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) end Waktu, " +

                    //"A.NmMasterCatID,D.UraianCat,0 as Apv,0 as RowStatus from  FnBreakDown A " +
                    //"inner join FnBreakDown_MasterOven B on A.OvenID=B.ID " +
                    //"inner join FnBreakDown_MasterGroupOven C on A.GroupOvenID=C.ID " +
                    //"inner join FnBreakDown_NmMasterCat D on A.NmMasterCatID=D.ID where A.RowStatus >-1 and MONTH(A.TanggalBreak)='" + bulan + "' and YEAR(TanggalBreak)='" + tahun + "' and A.NmMasterCatID in(18,20,21,22) " +
                    //" " + kriteria + " group by A.NmMasterCatID,A.OvenID,D.UraianCat ) " +

                    //"insert into TotalWaktu " +
                    //"select 0 ID,NoUrut,BreakNo,Convert(varchar,TanggalBreak,23)TanggalBreak,OvenID,NamaOven,NamaGroupOven,GroupOvenID,Uraian,Frek," +


                    //"CASE  DAY(EOMONTH('" + tahun + "-" + bulan + "-" + 01 + "'))" +
                    //"WHEN 28 THEN isnull(cast(84 as decimal(18,2)),0.00) - isnull(cast(Waktu as decimal(18,2)),0.00) " +
                    //"WHEN 30 THEN isnull(cast(90 as decimal(18,2)),0.00) - isnull(cast(Waktu as decimal(18,2)),0.00) " +
                    //"WHEN 31 THEN isnull(cast(93 as decimal(18,2)),0.00) - isnull(cast(Waktu as decimal(18,2)),0.00) END Waktu, " +

                    //"NmMasterCatID,UraianCat,Apv,RowStatus from q where NmMasterCatID in(18,20,21,22)" +

                    "insert Into TotalWaktu " +
                    "select 0 as ID,1 as NoUrut,'' as BreakNo,'Total Waktu' as TanggalBreak,A.OvenID,'' as NamaOven,'' as NamaGroupOven, 0 as GroupOvenID,'' as Uraian,0 as Frek, " +
                    "isnull(cast(sum(A.Waktu)as decimal(18,2)),0.00) as Waktu,A.NmMasterCatID,D.UraianCat,0 as Apv,0 as RowStatus from  FnBreakDown A " +
                    "inner join FnBreakDown_MasterOven B on A.OvenID=B.ID " +
                    "inner join FnBreakDown_MasterGroupOven C on A.GroupOvenID=C.ID " +
                    "inner join FnBreakDown_NmMasterCat D on A.NmMasterCatID=D.ID where A.RowStatus >-1 and MONTH(A.TanggalBreak)='" + bulan + "' and YEAR(TanggalBreak)='" + tahun + "' " +
                    " " + kriteria + " and A.NmMasterCatID in(18,20,21,22) group by A.NmMasterCatID,A.OvenID,D.UraianCat " +

                    "insert into TotalWaktu " +
                    "select A.ID,A.NoUrut,A.BreakNo,CONVERT(VARCHAR,A.TanggalBreak,23) TanggalBreak,A.OvenID,B.NamaOven,C.NamaGroupOven,A.GroupOvenID,A.Uraian,A.Frek," +
                    "isnull(cast(A.Waktu as decimal(18,2)),0.00) as Waktu,A.NmMasterCatID,D.UraianCat,A.Apv,A.RowStatus from FnBreakDown A " +
                    "inner join FnBreakDown_MasterOven B on A.OvenID=B.ID " +
                    "inner join FnBreakDown_MasterGroupOven C on A.GroupOvenID=C.ID " +
                    "inner join FnBreakDown_NmMasterCat D on A.NmMasterCatID=D.ID where A.RowStatus >-1 and MONTH(TanggalBreak)='" + bulan + "' and YEAR(TanggalBreak)='" + tahun + "' " +
                    " " + kriteria + " and A.NmMasterCatID in(18,20,21,22) order by A.NmMasterCatID,A.OvenID, TanggalBreak asc " +

                    //tambahan 
                    "insert into TotalWaktuBreakDown "+

                    "select count(A.ID) as ID,0 as NoUrut,'' as BreakNo,'Total Waktu' as TanggalBreak,A.OvenID,'' as NamaOven,'' as NamaGroupOven, "+
                    "0 as GroupOvenID, '' as Uraian,0 as Frek, "+

                    "CASE  DAY(EOMONTH('"+tahun+" - "+bulan+" - 01 ')) "+
                    "WHEN 28 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) " +
                    "WHEN 29 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) " +
                    "WHEN 30 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) " +
                    "WHEN 31 THEN isnull(sum(A.Waktu)/CAST(480 as decimal(18,2)),0.00) END Waktu, " +
                    
                    "A.NmMasterCatID,D.UraianCat,0 as Apv,0 as RowStatus "+
                    "from  FnBreakDown A "+
                    "inner join FnBreakDown_MasterOven B on A.OvenID=B.ID "+
                    "inner join FnBreakDown_MasterGroupOven C on A.GroupOvenID=C.ID "+
                    "inner join FnBreakDown_NmMasterCat D on A.NmMasterCatID=D.ID "+
                    "where A.RowStatus >-1 and MONTH(A.TanggalBreak)='"+ bulan +"' and YEAR(TanggalBreak)='"+ tahun +"' "+

                    "and A.NmMasterCatID in(18,20,21,22) " + kriteria + " group by A.NmMasterCatID,A.OvenID,D.UraianCat "+



                    "SELECT 0 ID, 0 NoUrut,'' BreakNo,'Total Waktu BreakDown' TanggalBreak,0 OvenID,'' NamaOven,'' NamaGroupOven,0 GroupOvenID,'' Uraian,0 Frek, "+
                    //"Sum(waktu) Waktu,
                    "CASE  DAY(EOMONTH('" + tahun + " - " + bulan + " - 01 ')) " +
                    "WHEN 28 THEN isnull(cast(84 as decimal(18,2)),0.00) - isnull(sum(waktu),0) " +
                    "WHEN 29 THEN isnull(cast(87 as decimal(18,2)),0.00) - isnull(sum(waktu),0) " +
                    "WHEN 30 THEN isnull(cast(90 as decimal(18,2)),0.00) - isnull(sum(waktu),0) " +
                    "WHEN 31 THEN isnull(cast(93 as decimal(18,2)),0.00) - isnull(sum(waktu),0) END Waktu, " +
                    "0 NmMasterCatID,'' UraianCat,0 Apv,0 RowStatus FROM TotalWaktuBreakDown "+

                    "union all " +

                    //tambahan

                    "select ID,NoUrut,BreakNo,CONVERT(VARCHAR,TanggalBreak,23) TanggalBreak,OvenID,NamaOven,NamaGroupOven,GroupOvenID,Uraian,Frek,isnull(cast(Waktu as decimal(18,2)),0.00) as Waktu, " +
                    "NmMasterCatID,UraianCat,Apv,RowStatus from TotalWaktu ORDER By OvenID,NmMasterCatID,NoUrut desc " +
                    "EXEC('IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].[TotalWaktu]'') AND type in (N''U'')) DROP TABLE [dbo].[TotalWaktu]') "+
                    "EXEC('IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].[TotalWaktuBreakDown]'') AND type in (N''U'')) DROP TABLE [dbo].[TotalWaktuBreakDown]')";
            SqlDataReader sdr = dataAccess.RetrieveDataByString(strSQL);

            if (dataAccess.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrBdFN.Add(GenerateObjecttGPemotongan(sdr));
                }
            }
            return arrBdFN;
        }

        public BreakDownFN GenerateObjecttGPemotongan(SqlDataReader sqlDataReader)
        {
            objBdFN = new BreakDownFN();
            objBdFN.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objBdFN.NoUrut = Convert.ToInt32(sqlDataReader["NoUrut"]);
            objBdFN.BreakNo = sqlDataReader["BreakNo"].ToString();
            objBdFN.TanggalBreak1 = sqlDataReader["TanggalBreak"].ToString();
            objBdFN.NamaOven = sqlDataReader["NamaOven"].ToString();
            objBdFN.NamaGroupOven = sqlDataReader["NamaGroupOven"].ToString();
            objBdFN.Uraian = sqlDataReader["Uraian"].ToString();
            objBdFN.Frek = Convert.ToInt32(sqlDataReader["Frek"]);
            objBdFN.Waktu = Convert.ToDecimal(sqlDataReader["Waktu"]);
            objBdFN.NmMasterCatID = Convert.ToInt32(sqlDataReader["NmMasterCatID"]);
            objBdFN.UraianCat = sqlDataReader["UraianCat"].ToString();
            return objBdFN;
        }

        public BreakDownFN RetrieveTgl(string periode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = " declare @date datetime  set @date = '" + periode + "'; " +
                            " with DaysInMonth as ( " +
                            " select @date as Date,DATENAME(DD,@date) as [DAY],DATENAME(WEEKDAY,@date) as [DAYNAME] " +
                            " union all " +
                            " select dateadd(dd,1,Date),DATENAME(DD,Date+1) as [DAY],DATENAME(WEEKDAY,Date+1) as [DAYNAME] " +
                            " from DaysInMonth where month(date) = month(@Date) " +
                            " ) " +
                            " select [1]Date1,[2]Date2,[3]Date3,[4]Date4,[5]Date5,[6]Date6,[7]Date7,[8]Date8,[9]Date9,[10]Date10,[11]Date11,[12]Date12,[13]Date13,[14]Date14,[15]Date15," +
                            " [16]Date16,[17]Date17,[18]Date18,[19]Date19,[20]Date20,[21]Date21,[22]Date22,[23]Date23,[24]Date24,[25]Date25," +
                            " [26]Date26,[27]Date27,[28]Date28,ISNULL([29],'-')Date29,ISNULL([30],'-')Date30,ISNULL([31],'-')Date31 " +
                            " from " +
                            " (" +
                            " SELECT [DAY] FROM DaysInMonth where month(date) = month(@Date) " +
                            " ) D " +
                            " PIVOT (MIN([DAY]) FOR [DAY] IN ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16]," +
                            " [17],[18],[19],[20],[21],[22],[23],[24],[25],[26],[27],[28],[29],[30],[31])) AS PVT ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {

                    return GenerateObjectTgl(sqlDataReader);
                }
            }
           
            return new BreakDownFN();
        }

        public BreakDownFN GenerateObjectTgl(SqlDataReader sqlDataReader)
        {
            objBdFN = new BreakDownFN();
            objBdFN.Date1 = sqlDataReader["Date1"].ToString();
            objBdFN.Date2 = sqlDataReader["Date2"].ToString();
            objBdFN.Date3 = sqlDataReader["Date3"].ToString();
            objBdFN.Date4 = sqlDataReader["Date4"].ToString();
            objBdFN.Date5 = sqlDataReader["Date5"].ToString();
            objBdFN.Date6 = sqlDataReader["Date6"].ToString();
            objBdFN.Date7 = sqlDataReader["Date7"].ToString();
            objBdFN.Date8 = sqlDataReader["Date8"].ToString();
            objBdFN.Date9 = sqlDataReader["Date9"].ToString();
            objBdFN.Date10 = sqlDataReader["Date10"].ToString();
            objBdFN.Date11 = sqlDataReader["Date11"].ToString();
            objBdFN.Date12 = sqlDataReader["Date12"].ToString();
            objBdFN.Date13 = sqlDataReader["Date13"].ToString();
            objBdFN.Date14 = sqlDataReader["Date14"].ToString();
            objBdFN.Date15 = sqlDataReader["Date15"].ToString();
            objBdFN.Date16 = sqlDataReader["Date16"].ToString();
            objBdFN.Date17 = sqlDataReader["Date17"].ToString();
            objBdFN.Date18 = sqlDataReader["Date18"].ToString();
            objBdFN.Date19 = sqlDataReader["Date19"].ToString();
            objBdFN.Date20 = sqlDataReader["Date20"].ToString();
            objBdFN.Date21 = sqlDataReader["Date21"].ToString();
            objBdFN.Date22 = sqlDataReader["Date22"].ToString();
            objBdFN.Date23 = sqlDataReader["Date23"].ToString();
            objBdFN.Date24 = sqlDataReader["Date24"].ToString();
            objBdFN.Date25 = sqlDataReader["Date25"].ToString();
            objBdFN.Date26 = sqlDataReader["Date26"].ToString();
            objBdFN.Date27 = sqlDataReader["Date27"].ToString();
            objBdFN.Date28 = sqlDataReader["Date28"].ToString();
            objBdFN.Date29 = sqlDataReader["Date29"].ToString();
            objBdFN.Date30 = sqlDataReader["Date30"].ToString();
            objBdFN.Date31 = sqlDataReader["Date31"].ToString();
            return objBdFN;
        }

        public BreakDownFN GenerateObjecttG2(SqlDataReader sqlDataReader)
        {
            objBdFN = new BreakDownFN();
            objBdFN.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objBdFN.NoUrut = Convert.ToInt32(sqlDataReader["NoUrut"]);
            objBdFN.BreakNo = sqlDataReader["BreakNo"].ToString();
            objBdFN.TanggalBreak1 = sqlDataReader["TanggalBreak"].ToString();
            objBdFN.NamaOven = sqlDataReader["NamaOven"].ToString();
            objBdFN.NamaGroupOven = sqlDataReader["NamaGroupOven"].ToString();
            objBdFN.Uraian = sqlDataReader["Uraian"].ToString();
            objBdFN.Frek = Convert.ToInt32(sqlDataReader["Frek"]);
            objBdFN.Waktu = Convert.ToDecimal(sqlDataReader["Waktu"]);
            objBdFN.NmMasterCatID = Convert.ToInt32(sqlDataReader["NmMasterCatID"]);
            objBdFN.UraianCat = sqlDataReader["UraianCat"].ToString();
            return objBdFN;
        }

        public BreakDownFN GenerateObjecttGl(SqlDataReader sqlDataReader)
        {
            objBdFN = new BreakDownFN();
            objBdFN.OvenID= Convert.ToInt32(sqlDataReader["OvenID"]);
            objBdFN.NamaOven = sqlDataReader["NamaOven"].ToString();
            return objBdFN;
        }

        public BreakDownFN GenerateObject2(SqlDataReader sqlDataReader)
        {
            objBdFN = new BreakDownFN();
            objBdFN.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objBdFN.NamaOven = sqlDataReader["NamaOven"].ToString();
            objBdFN.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objBdFN.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objBdFN.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objBdFN.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objBdFN.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            
            return objBdFN;
        }

        public BreakDownFN GenerateObject3(SqlDataReader sqlDataReader)
        {
            objBdFN = new BreakDownFN();
            objBdFN.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objBdFN.NamaGroupOven = sqlDataReader["NamaGroupOven"].ToString();
            objBdFN.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objBdFN.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objBdFN.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objBdFN.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objBdFN.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objBdFN;
        }

        public BreakDownFN GenerateObject4(SqlDataReader sqlDataReader)
        {
            objBdFN = new BreakDownFN();
            objBdFN.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objBdFN.UraianCat = sqlDataReader["UraianCat"].ToString();
            objBdFN.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objBdFN.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objBdFN.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objBdFN.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objBdFN.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objBdFN;
        }

        public BreakDownFN GenerateObject5(SqlDataReader sqlDataReader)
        {
            objBdFN = new BreakDownFN();
            objBdFN.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objBdFN.UraianCat = sqlDataReader["UraianCat"].ToString();
            return objBdFN;
        }
    }
}
