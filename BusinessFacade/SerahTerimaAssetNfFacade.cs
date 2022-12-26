using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using System.Web;
using BusinessFacade;
using DataAccessLayer;
using Dapper;

namespace BusinessFacade
{
    public class SerahTerimaAssetNfFacade : AbstractTransactionFacade
    {
        private SerahTerimaAssetNf.ParamHead obj = new SerahTerimaAssetNf.ParamHead();
        private List<SqlParameter> sqlListParam;
        public SerahTerimaAssetNfFacade(object objDomain)
            : base(objDomain)
        {
            obj = (SerahTerimaAssetNf.ParamHead)objDomain;
        }

        public override int Delete(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@NoBA", obj.NoBA));
                sqlListParam.Add(new SqlParameter("@CreatedBy", obj.CreatedBy));
                sqlListParam.Add(new SqlParameter("@Tanggal", obj.Tanggal));
                sqlListParam.Add(new SqlParameter("@ItemID", obj.ItemID));
                sqlListParam.Add(new SqlParameter("@UomID", obj.UomID));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertAM_Asset2Adjust");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@NoAsset", obj.NoAsset));
                sqlListParam.Add(new SqlParameter("@TglMulai", obj.TglMulaiPekerjaan));
                sqlListParam.Add(new SqlParameter("@TglSelesai", obj.TglSelesaiPekerjaan));
                sqlListParam.Add(new SqlParameter("@CreatedBy", obj.UserName));
                sqlListParam.Add(new SqlParameter("@Upgrade", obj.Upgrade));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsert_AssetSerah");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Update(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@GroupID", obj.AMGroupID));
                sqlListParam.Add(new SqlParameter("@ClassID", obj.AMclassID));
                sqlListParam.Add(new SqlParameter("@SubClassID", obj.AMsubClassID));
                sqlListParam.Add(new SqlParameter("@LokasiID", obj.AMlokasiID));
                sqlListParam.Add(new SqlParameter("@KodeAsset", obj.ItemCode));
                sqlListParam.Add(new SqlParameter("@NamaAsset", obj.ItemName));

                sqlListParam.Add(new SqlParameter("@AssyDate", obj.AssyDate));
                sqlListParam.Add(new SqlParameter("@NilaiAsset", obj.NilaiAsset));
                sqlListParam.Add(new SqlParameter("@MfgDate", obj.MfgDate));
                sqlListParam.Add(new SqlParameter("@MfgYear", obj.MfgYear));
                sqlListParam.Add(new SqlParameter("@LifeTime", obj.LifeTime));
                sqlListParam.Add(new SqlParameter("@DepreciatID", obj.DepreciatID));
                sqlListParam.Add(new SqlParameter("@StartDeprec", obj.StartDeprec));

                sqlListParam.Add(new SqlParameter("@ItemKode", obj.ItemCode));
                sqlListParam.Add(new SqlParameter("@PicDept", obj.PicDept));
                sqlListParam.Add(new SqlParameter("@PicPerson", obj.UserName));
                sqlListParam.Add(new SqlParameter("@PlantID", obj.PlantID));
                sqlListParam.Add(new SqlParameter("@RowStatus", obj.RowStatus));
                sqlListParam.Add(new SqlParameter("@TipeAsset", obj.TipeAsset));
                sqlListParam.Add(new SqlParameter("@UomID", obj.UomID));
                sqlListParam.Add(new SqlParameter("@OwnerDeptID", obj.OwnerDeptID));

                sqlListParam.Add(new SqlParameter("@AssetID", obj.AssetID));
                sqlListParam.Add(new SqlParameter("@CreatedBy", obj.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertAM_Asset");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }        

        public static string RetrieveNamaAsset()
        {
            string strSQL = "";
            Users users = (Users)HttpContext.Current.Session["Users"];
            string query = string.Empty; string query2 = string.Empty;
            if (users.UnitKerjaID.ToString().Length == 1) { query = "13"; }
            else{ query = "14"; }
            strSQL = 
    "with q as( " +
        "select ItemID from PakaiDetail where ItemTypeID=2 and GroupID=12 " +
        "and PakaiID in (select ID from Pakai where Status>1) " +
    "), " +
    "w as ( " +
        "select DISTINCT(SUBSTRING(ItemCode,8," + query + "))KodeAsset " +
        "from Asset a, q where a.ID=q.ItemID and RowStatus>-1 " +
    "), " +
    "r as ( " +
        "select ItemCode,ItemName from asset a,w where a.itemcode=w.KodeAsset and a.RowStatus>-1 " +
    "), " +
    "t as ( " +
        "select*from r where ItemCode in (select SUBSTRING(ItemCode,8," + query + ") from Asset where Aktif=1 and Head=0) " +
        "and ItemCode not in (select NoAsset from AM_AssetSerah where RowStatus>-1) " +
        "union all " +
        "select a.NoAsset ItemCode,i.ItemName from AM_AssetSerah a, Asset i " +
        "where a.noasset=i.itemcode and a.RowStatus>-1 and a.apv<5 " +
    ") " +
    "select*from t order by ItemName";
            return strSQL;
        }

        public static string RetrieveNamaAsset2()
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            int DeptID = users.DeptID;
            string query = string.Empty; string query2 = string.Empty; string Dept0 = string.Empty;
            if (users.UnitKerjaID.ToString().Length == 1){ query = "13"; query2 = "9"; }
            else { query = "14"; query2 = "10"; }
            if (DeptID == 25 || DeptID == 19 || DeptID == 4){ Dept0 = "19"; }
            else{ Dept0 = DeptID.ToString(); }
            string Query = string.Empty;
            if (DeptID == 10 || DeptID == 6) { Query = "DeptID_ID in (10)"; }
            else if (DeptID == 25 || DeptID == 19 || DeptID == 4) { Query = "DeptID_ID in (4)"; }
            else{ Query = "DeptID_ID in (" + DeptID + ")"; }
            string strSQL =
            " select ItemCode,ItemName from asset where itemcode in ((select DISTINCT(SUBSTRING(ItemCode,8," + query + "))KodeAsset " +
            " from Asset where ID in (select ItemID from PakaiDetail where ItemTypeID=2 and GroupID=12 and PakaiID in (select ID from Pakai where Status>1)) " +
            " and RowStatus>-1)) and ItemCode in (select NoAsset from AM_AssetSerah where RowStatus>-1 and Apv=0  " +
            " and CreatedBy in (select username from users where RowStatus>-1 and DeptID=" + Dept0 + ")) and head=1 and RowStatus>-1 " +
            " union all " +
            " select ItemCode,ItemName from asset where itemcode in ((select DISTINCT(SUBSTRING(ItemCode,8," + query + "))KodeAsset " +
            " from Asset where ID in (select ItemID from PakaiDetail where ItemTypeID=2 and GroupID=12 and PakaiID in (select ID from Pakai where Status>1)) " +
            " and RowStatus>-1)) and ItemCode in (select NoAsset from AM_AssetSerah where RowStatus>-1 and Apv=2) and DeptID in " +
            " (select DeptID from AM_Department where " + Query + " and RowStatus>-1) and head=1 and RowStatus>-1 ";
            return strSQL;
        }

        public static string RetrieveNamaAsset3()
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            int DeptID_ID = users.DeptID;
            string query = string.Empty; string query2 = string.Empty;
            if (users.UnitKerjaID.ToString().Length == 1) { query = "13"; query2 = "9"; }
            else{ query = "14"; query2 = "10"; }
            string Query = string.Empty;
            if (DeptID_ID == 10 || DeptID_ID == 6) { Query = "DeptID_ID in (10)"; }
            else if (DeptID_ID == 25 || DeptID_ID == 19 || DeptID_ID == 4) { Query = "DeptID_ID in (4)"; }
            else{ Query = "DeptID_ID in (" + DeptID_ID + ")"; }
            string strSQL =
            " select ItemCode,ItemName from asset where itemcode in ((select DISTINCT(SUBSTRING(ItemCode,8," + query + "))KodeAsset " +
            " from Asset where ID in (select ItemID from PakaiDetail where ItemTypeID=2 and GroupID=12 and PakaiID in (select ID from Pakai where Status>1)) " +
            " and RowStatus>-1)) and ItemCode in (select NoAsset from AM_AssetSerah where RowStatus>-1 and Apv=2) and DeptID in " +
            " (select DeptID from AM_Department where " + Query + " and RowStatus>-1) and head=1 and RowStatus>-1 ";
            return strSQL;
        }

        public static string RetrieveNamaAsset4()
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string query = string.Empty; string query2 = string.Empty;
            if (users.UnitKerjaID.ToString().Length == 1) { query = "13"; query2 = "9"; }
            else{ query = "14"; query2 = "10"; }
            string strSQL =
            " select ItemCode,ItemName from asset where Head=1 and rowstatus>-1 and itemcode in ((select DISTINCT(SUBSTRING(ItemCode,8," + query + "))KodeAsset " +
            " from Asset where ID in (select ItemID from PakaiDetail where ItemTypeID=2 and GroupID=12 and PakaiID in (select ID from Pakai where Status>1)) " +
            " and RowStatus>-1)) and ItemCode in (select NoAsset from AM_AssetSerah where RowStatus>-1 and Apv=3) and head=1 and RowStatus>-1 ";
            return strSQL;
        }

        public static string RetrieveNamaAsset5()
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string query = string.Empty; string query2 = string.Empty;
            if (users.UnitKerjaID.ToString().Length == 1) { query = "13"; query2 = "9"; }
            else{ query = "14"; query2 = "10"; }
            string strSQL =
            " select ItemCode,ItemName from asset where itemcode in ((select DISTINCT(SUBSTRING(ItemCode,8," + query + "))KodeAsset " +
            " from Asset where ID in (select ItemID from PakaiDetail where ItemTypeID=2 and GroupID=12 and PakaiID in (select ID from Pakai where Status>1)) " +
            " and RowStatus>-1)) and ItemCode in (select NoAsset from AM_AssetSerah where RowStatus>-1 and Apv=4) and head=1 and RowStatus>-1 ";
            return strSQL;
        }

        public static List<SerahTerimaAssetNf.ParamAsset> ListAsset(string qry)
        {
            List<SerahTerimaAssetNf.ParamAsset> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = qry;
                    AllData = connection.Query<SerahTerimaAssetNf.ParamAsset>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static int CekIdSerah(string ItemCode)
        {
            int val = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select top 1 Id from AM_AssetSerah where NoAsset='"+ ItemCode + "' and RowStatus>-1 order by ID desc";
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static int UpgradeCekAPv(string ItemCode)
        {
            int val = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select top 1 Upgrade from AM_AssetSerah where NoAsset='"+ ItemCode + "' and RowStatus>-1 order by ID desc";
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static int CekApvSerah(string ItemCode)
        {
            int val = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select top 1 Apv from AM_AssetSerah where NoAsset='" + ItemCode + "' and RowStatus>-1 order by ID desc";
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static DateTime TglSelesaiPekerjaan(string ItemCode)
        {
            DateTime val= DateTime.Now;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select top 1 CONVERT(date, TglSelesai) TglSelesaiPekerjaan from AM_AssetSerah where NoAsset='" + ItemCode + "' and RowStatus>-1 order by ID desc";
                    val = connection.QueryFirstOrDefault<DateTime>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static DateTime TglSelesai(string ItemCode)
        {
            DateTime val = DateTime.Now;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select top 1 CONVERT(date, createdtime) TglSelesai1 from AM_AssetSerah where NoAsset='" + ItemCode + "' and RowStatus>-1 order by ID desc";
                    val = connection.QueryFirstOrDefault<DateTime>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static DateTime TglMulai(string ItemCode)
        {
            DateTime val = DateTime.Now;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = 
"select TglMulai "+
"from(  " +
    "select CONVERT(date, xx1.PakaiDate) TglMulai  " +
    "from(  " +
        "select top 1 PakaiID " +
        "from PakaiDetail " +
        "where ItemID in (select ID from Asset where ItemCode like'%"+ ItemCode + "%' and Aktif = 1 and Head = 0) " +
        "and RowStatus > -1 and GroupID in (12) and ItemTypeID = 2 order by ID " +
    ") as xx " +
    "inner join Pakai as xx1 ON xx.PakaiID = xx1.ID " +
    "where xx1.Status > -1 and xx1.ItemTypeID = 2 " +
") as xx2";
                    val = connection.QueryFirstOrDefault<DateTime>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static string DeptPemilikAsset(string ItemCode)
        {
            string val = "";
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query ="select NamaDept from AM_Department where DeptID=" + ItemCode + " and RowStatus>-1";
                    val = connection.QueryFirstOrDefault<string>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static string DeptPembuatAsset(string ItemCode,string qry)
        {
            string val = "";
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query =
"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp_A]') AND type in (N'U')) " +
"DROP TABLE [dbo].[temp_A] " +
"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp_B]') AND type in (N'U')) " +
"DROP TABLE [dbo].[temp_B] " +
"select distinct 'A'Flag,  TRIM(A.Alias) DeptName " +
"into temp_A " +
"from ( " +
    "select x2.AssetUtama,x22.ItemName,x2.CreatedBy " +
    "from ( " +
        "select AssetUtama,CreatedBy " +
        "from ( " +
            "select SUBSTRING(A1.ItemCode,8,"+qry+")AssetUtama,x.CreatedBy " +
            "from ( " +
                "select ItemID,GroupID,A.ItemTypeID,B.CreatedBy " +
                "from pakaidetail A " +
                "inner join Pakai B ON A.PakaiID=B.ID " +
                "where A.groupid=12 and A.rowstatus>-1 and A.ItemTypeID=2 and B.Status=3 " +
            ") as x " +
            "inner join Asset A1 ON A1.ID=x.ItemID " +
        ") as x1 group by AssetUtama,CreatedBy " +
    ") as x2 " +
    "inner join asset x22 ON x22.ItemCode=x2.AssetUtama " +
    "where x2.AssetUtama='"+ ItemCode + "' " +
") as x33 " +
"inner join users x01 ON x01.UserName=x33.CreatedBy " +
"inner join Dept A ON A.ID=x01.DeptID " +
"select DISTINCT DeptName = STUFF( " +
    "(SELECT DISTINCT '; ' + DeptName  FROM temp_A AS x2   WHERE x2.Flag = x3.Flag  FOR XML PATH('') " +
"), 1, 1, '') " +
"into temp_B " +
"from temp_A x3 " +
"group by x3.DeptName,Flag " +
"select REPLACE(DeptName,'&amp;','&')NamaDept " +
"from temp_B " +
"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp_A]') AND type in (N'U')) " +
"DROP TABLE [dbo].[temp_A] " +
"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp_B]') AND type in (N'U')) " +
"DROP TABLE [dbo].[temp_B]  ";
                    val = connection.QueryFirstOrDefault<string>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static int DeptPic(string ItemCode, string qry)
        {
            int val = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query =
"with q as ( " +
    "select distinct x01.DeptID, " +
    "case when x01.DeptID=22 then '1' when x01.DeptID=30 then '2' when x01.DeptID=19 then '3' else '0' end urut " +
    "from ( " +
        "select x2.AssetUtama,x22.ItemName,x2.CreatedBy " +
        "from ( " +
            "select AssetUtama,CreatedBy " +
            "from ( " +
            "select SUBSTRING(A1.ItemCode,8,"+qry+")AssetUtama,x.CreatedBy " +
                "from ( " +
                    "select ItemID,GroupID,A.ItemTypeID,B.CreatedBy " +
                    "from pakaidetail A " +
                    "inner join Pakai B ON A.PakaiID=B.ID " +
                    "where A.groupid=12 and A.rowstatus>-1 and A.ItemTypeID=2 and B.Status=3 " +
                ") as x " +
                "inner join Asset A1 ON A1.ID=x.ItemID " +
            ") as x1 group by AssetUtama,CreatedBy " +
        ") as x2 " +
        "inner join asset x22 ON x22.ItemCode=x2.AssetUtama " +
        "where x2.AssetUtama='"+ ItemCode + "' " +
    ") as x33 " +
    "inner join users x01 ON x01.UserName=x33.CreatedBy " +
") " +
",w as ( " +
    "select top 1 DeptId from q order by urut " +
") " +
"select*from w ";
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static List<SerahTerimaAssetNf.ParamInfoDetail> InfoDetail(string code)
        {
            List<SerahTerimaAssetNf.ParamInfoDetail> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query =
"IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempSerah]') AND type in (N'U')) " +
"DROP TABLE [dbo].[TempSerah] " +
"IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempSerah_F]') AND type in (N'U')) " +
"DROP TABLE [dbo].[TempSerah_F] " +
";with data_0 as ( " +
    "select ReceiptDetailID,ID,GroupID,ItemCode,ItemName,UomCode, " +
    "sum(QtySPP)QtySPP,sum(QtyBeli)QtyBeli,sum(QtyAdjust)QtyAdjust,Price " +
    "from ( " +
        "select ReceiptDetailID,ID,GroupID,ItemCode,ItemName,UomCode, " +
        "sum(QtySPP)QtySPP,sum(QtyBeli)QtyBeli,QtyAdjust,cast(Price as decimal(18,0))Price " +
        "from ( " +
            "select xx.*,rcp.ID ReceiptDetailID,sp.Quantity QtySPP,sum(rcp.Quantity)QtyBeli,'0'QtyAdjust, " +
            "case when po.Crc=1 then pod.Price " +
            "else (select isnull(mk.Kurs*rcp.Price,0) from MataUangKurs mk where mk.MUID=po.Crc and mk.drTgl=rc.ReceiptDate) " +
            "end Price " +
            "from ( " +
                "select ID,GroupID,ItemCode,ItemName, " +
                "(select A.UOMDesc from UOM A where A.ID=UOMID and RowStatus>-1)UomCode " +
                "from Asset where ItemCode like'%" + code + "%' and Head in (0,2) and Aktif=1 " +
            ") as xx " +
            "left join SPPDetail sp ON sp.ItemID=xx.ID and sp.GroupID=xx.GroupID and sp.Status>-1 " +
            "inner join SPP spp ON sp.SPPID=spp.ID and spp.Status>-1 " +
            "left join ReceiptDetail rcp ON rcp.SPPID=spp.ID and rcp.ItemID=sp.ItemID and rcp.RowStatus>-1 " +
            "inner join Receipt rc ON rc.ID=rcp.ReceiptID and rc.Status>-1 " +
            "left Join POPurchnDetail pod ON pod.ID=rcp.PODetailID and pod.Status>-1 and pod.ItemTypeID=sp.ItemTypeID " +
            "inner join POPurchn po ON po.ID=pod.POID and po.Status>-1 " +
            "group by xx.ID,xx.GroupID,xx.ItemCode,xx.ItemName, " +
            "xx.UomCode,sp.Quantity,rcp.Price,pod.Price,po.Crc,rc.ReceiptDate,rcp.ID " +
        ") as xa group by ID,GroupID,ItemCode,ItemName,UomCode,Price,QtyAdjust,ReceiptDetailID " +
        "union all " +
        "select '0'ReceiptDetailID,B.ItemID,B.GroupID,C.ItemCode,C.ItemName, " +
        "D.UOMCode,'0'QtySPP,'0'QtyBeli,B.Quantity QtyAdjust,isnull(B.AvgPrice,0)Price " +
        "from Adjust A " +
        "inner join AdjustDetail B ON A.ID=B.AdjustID " +
        "inner join Asset C ON C.ID=B.ItemID and C.GroupID=B.GroupID " +
        "inner join UOM D ON D.ID=C.UOMID " +
        "where B.ItemID in ( " +
            "select ID " +
            "from Asset " +
            "where ItemCode like'%" + code + "%' and Head in (0,2) and Aktif=1 and RowStatus>-1 " +
        ") " +
        "and A.AdjustType='Tambah' " +
    ") as x group by ID,GroupID,ItemCode,ItemName,UomCode,Price ,ReceiptDetailID " +
") " +
",data_1 as ( " +
    "select pk.ID PakaiDetailID,A.ItemCode,A.ItemName,pk.Quantity QtyPakai " +
    "from data_0 A " +
    "left join PakaiDetail pk ON pk.ItemID=A.ID and pk.GroupID=A.GroupID and pk.groupID=12 " +
    "where pk.PakaiID in (select ID from Pakai where Status>1) " +
    "and pk.RowStatus>-1 " +
    "group by A.ItemCode,A.ItemName ,pk.ID,pk.Quantity " +
") " +
",data_2 as ( " +
    "select ItemCode,ItemName,sum(QtyPakai)QtyPakai " +
    "from data_1 group by ItemCode,ItemName " +
") " +
",data_5 as ( " +
    "select Price,itemname " +
    "from data_0 group by price,itemname " +
") " +
",data_6 as ( " +
    "select ReceiptDetailID,Price,ItemCode,itemname,UomCode,QtySPP,QtyBeli,QtyAdjust " +
    "from data_0 group by price,itemname,ItemCode,UomCode,QtySPP,QtyBeli,QtyAdjust,ReceiptDetailID " +
") " +
",data_7 as ( " +
    "select A.ItemCode,A.ItemName,A.UomCode,A.QtySPP,A.QtyBeli,A.QtyAdjust, " +
    "(select cast(sum(Price)/count(Price) as decimal(18,0)) from data_5 A1 where A1.ItemName=A.ItemName) Price " +
    "from data_6 A " +
    "left join data_5 B ON A.ItemName=B.ItemName " +
    "group by A.ItemCode,A.ItemName,A.UomCode,A.QtySPP,A.QtyBeli,A.QtyAdjust,A.ReceiptDetailID " +
") " +
",data_8 as ( " +
    "select ItemCode,ItemName,UomCode,sum(QtySPP)QtySPP,sum(QtyBeli)QtyBeli,sum(QtyAdjust)QtyAdjust,Price " +
    "from data_7  group by ItemCode,ItemName,UomCode,Price " +
") " +
",data_9 as ( " +
    "select A.*,B.QtyPakai " +
    "from data_8 A " +
    "left join data_2 B ON A.ItemCode=B.ItemCode " +
    "group by A.ItemCode,A.ItemName,A.UomCode,A.QtySPP,A.QtyBeli,A.QtyAdjust,A.Price,B.QtyPakai " +
") " +
",data_10 as ( " +
    "select ItemCode,ItemName,UomCode,QtyPakai,Price,QtyPakai*Price TotalPrice from data_9 " +
") " +
"select * " +
"into TempSerah " +
"from data_10 " +
"select ItemCode,ItemName,UomCode, " +
"case when QtyPakaiAdj=0 then 0 else QtyPakaiAdj end QtyPakai, " +
"case when QtyPakaiAdj=0 then 0 else  Price end Price, " +
"totalprice " +
"into TempSerah_F " +
"from ( " +
    "SELECT itemcode, itemname, uomcode,QtyPakai, qtypakaiadj , price, (qtypakaiadj * price) totalprice " +
    "FROM ( " +
        "select *, " +
        "CASE WHEN qtyadjustoutnonstok IS NOT NULL THEN qtypakai - qtyadjustoutnonstok " +
        "ELSE qtypakai " +
        "END QtyPakaiAdj " +
        "from TempSerah ts " +
        "LEFT JOIN( " +
            "SELECT ItemCode Itemcodeadj, sum(quantity) QtyAdjustOutNonStok " +
            "FROM Adjust a, Adjustdetail b, Asset c " +
            "WHERE a.ID = b.AdjustID AND b.itemid = c.id AND b.RowStatus > -1 AND status = 1 AND nonstok = 1 " +
            "GROUP BY itemcode " +
        ") adj ON ts.itemcode = adj.itemcodeadj " +
    ") tempasset " +
") as x " +
"select ItemCode,ItemName,UomCode,sum(QtyPakai)QtyPakai,Price,sum(totalprice) TotalPrice " +
"from TempSerah_F group by ItemCode,ItemName,UomCode,Price ";
                    AllData = connection.Query<SerahTerimaAssetNf.ParamInfoDetail>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static int CekSerah(string ItemCode)
        {
            int val = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select count(id) countId from AM_AssetSerah where rowStatus>-1 and NoAsset='"+ ItemCode + "'";
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static int DeptID_Eng(string ItemCode)
        {
            int val = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "  select B.DeptID from AM_AssetSerah A inner join users B ON A.CreatedBy=B.UserName where NoAsset='" + ItemCode + "' and A.RowStatus>-1 ";
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static int UpdateApvSerah(int Apv, string Username,string ItemCode)
        {
            int val = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "UPDATE AM_AssetSerah SET Apv="+ Apv + ", LastModifiedBy='"+ Username + "', LastModifiedTime=getDate() WHERE NoAsset='"+ ItemCode + "' AND RowStatus>-1";
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static int InsertSerahDetail(int AssetId, string ItemCode, string ItemName, decimal Qty,decimal Nilai)
        {
            int val = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "INSERT INTO AM_AssetSerahDetail VALUES (" + AssetId + ", '" + ItemCode + "', '" + ItemName + "', " + Qty + ", " + Nilai + ", 0)";
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static int LastAdjustID()
        {
            int val = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "Select Top 1 ID from Adjust Order By ID Desc";
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static SerahTerimaAssetNf.ParamGetAsset GetAsset(string ItemCode, string qr)
        {
            SerahTerimaAssetNf.ParamGetAsset AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query =
"select*,(select A.DeptID_ID from AM_Department A where A.DeptID=data.AM_DeptID) DeptID " +
"from( " +
    "select ID AssetID,ItemCode,ItemName,UOMID,AMgroupID,AMclassID, " +
    "AMsubClassID,AMlokasiID,(substring(itemcode,"+qr+" ,1)) AM_DeptID, " +
    "left(convert(char,createdtime,113),20) TglMulai " +
    "from Asset where ItemCode='"+ ItemCode + "' and rowstatus>-1 and Head=1 " +
") as Data ";
                    AllData = connection.QueryFirstOrDefault<SerahTerimaAssetNf.ParamGetAsset>(query);
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
