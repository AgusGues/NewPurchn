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
    public class TransaksiSPP3Facade : AbstractTransactionFacade
    {
        private PurchasingNf.ParamHead objSPP = new PurchasingNf.ParamHead();
        private List<SqlParameter> sqlListParam;
        public TransaksiSPP3Facade(object objDomain)
            : base(objDomain)
        {
            objSPP = (PurchasingNf.ParamHead)objDomain;
        }

        public int UpdateStatusSPP(TransactionManager transManager)
        {

            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objSPP.ID));
                sqlListParam.Add(new SqlParameter("@Status", objSPP.Status));
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateStatusSPP");
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
                sqlListParam.Add(new SqlParameter("@NoSPP", objSPP.NoSPP));
                sqlListParam.Add(new SqlParameter("@Minta", objSPP.Minta));
                sqlListParam.Add(new SqlParameter("@PermintaanType", objSPP.PermintaanType));
                sqlListParam.Add(new SqlParameter("@SatuanID", objSPP.SatuanID));
                sqlListParam.Add(new SqlParameter("@GroupID", objSPP.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objSPP.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@Jumlah", objSPP.Jumlah));
                sqlListParam.Add(new SqlParameter("@JumlahSisa", objSPP.JumlahSisa));
                sqlListParam.Add(new SqlParameter("@Keterangan", objSPP.Keterangan));
                sqlListParam.Add(new SqlParameter("@Sudah", objSPP.Sudah));
                sqlListParam.Add(new SqlParameter("@FCetak", objSPP.FCetak));
                sqlListParam.Add(new SqlParameter("@UserID", objSPP.UserID));
                sqlListParam.Add(new SqlParameter("@Pending", objSPP.Pending));
                sqlListParam.Add(new SqlParameter("@Inden", objSPP.Inden));
                sqlListParam.Add(new SqlParameter("@AlasanBatal", objSPP.AlasanBatal));
                sqlListParam.Add(new SqlParameter("@AlasanCLS", objSPP.AlasanCLS));
                sqlListParam.Add(new SqlParameter("@Status", objSPP.Status));
                sqlListParam.Add(new SqlParameter("@Approval", objSPP.Approval));
                sqlListParam.Add(new SqlParameter("@DepoID", objSPP.DepoID));
                sqlListParam.Add(new SqlParameter("@ApproveDate1", objSPP.ApproveDate1));
                sqlListParam.Add(new SqlParameter("@ApproveDate2", objSPP.ApproveDate2));
                sqlListParam.Add(new SqlParameter("@ApproveDate3", objSPP.ApproveDate3));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objSPP.CreatedBy));
                sqlListParam.Add(new SqlParameter("@HeadID", objSPP.HeadID));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertSPP");
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

        public override int Update(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Delete(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        /*public static List<PurchasingNf.ParamNoPol> ListNoPol(string Plant)
        {
            List<PurchasingNf.ParamNoPol> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT ID, isnull(KendaraanNo,'-')KendaraanNo, Type from [sql1.grcboard.com].GRCBOARD.dbo.Ex_MasterKendaraan where rowstatus> -1 and  DepoID in(" + Plant + ") order by KendaraanNo";
                    if (Plant.Length == 1)
                    {
                        query = "SELECT ID, isnull(KendaraanNo,'-')KendaraanNo,Type from [sql1.grcboard.com].GRCBOARD.dbo.Ex_MasterKendaraan where rowstatus> -1 and DepoID=" + Plant + " order by KendaraanNo";
                    }
                    AllData = connection.Query<PurchasingNf.ParamNoPol>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }*/

        public static List<PurchasingNf.ParamNoPol> ListNoPol(string Plant)
        {
            List<PurchasingNf.ParamNoPol> AllData = null;
            using (var connection = new SqlConnection(GetConnection("GRCBOARDPUSAT")))
            {
                string query;
                try
                {
                    query = (Plant.Length == 1) ?
                "SELECT ID, isnull(KendaraanNo,'-')KendaraanNo, Merk, Tahun, Tonase, DepresiasiPerHari, Type, TypeKendaraanID, " +
                "DepoID, DepoName, RowStatus, DefaultNamaSupirID, DefaultNamaKenekID, CreatedBy, CreatedTime, LastModifiedBy, LastModifiedTime  " +
                " from Ex_MasterKendaraan where rowstatus> -1 and DepoID=" + Plant + " order by KendaraanNo" :
                "SELECT ID, isnull(KendaraanNo,'-')KendaraanNo, Merk, Tahun, Tonase, DepresiasiPerHari, Type, TypeKendaraanID, " +
                "DepoID, DepoName, RowStatus, DefaultNamaSupirID, DefaultNamaKenekID, CreatedBy, CreatedTime, LastModifiedBy, LastModifiedTime  " +
                "from Ex_MasterKendaraan where rowstatus> -1 and  DepoID in(" + Plant + ") order by KendaraanNo";
                    AllData = connection.Query<PurchasingNf.ParamNoPol>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        } 

        private static string GetConnection(string strCon)
        {
            //string strCon1 = Global.ConnectionString();
            string strCon1 = string.Empty;
            string GRCBOARDPUSAT = "Initial Catalog=GRCBOARD;Data Source=sql1.grcboard.com;User ID=sa;Password=Passw0rd;Max Pool Size=10000000";
            string GRCBOARDLAPAK = "Initial Catalog=AgenLapak;Data Source=sql1.grcboard.com;User ID=sa;Password=Passw0rd;Max Pool Size=10000000";
            string GRCBOARDKRWG = "Initial Catalog=bpaskrwg;Data Source=sqlkrwg.grcboard.com;User ID=sa;Password=Passw0rd;Max Pool Size=10000000";
            string GRCBOARDCTRP = "Initial Catalog=bpasctrp;Data Source=sqlctrp.grcboard.com;User ID=sa;Password=Passw0rd;Max Pool Size=10000000";
            string GRCBOARDJMB = "Initial Catalog=bpasjombang;Data Source=sqljombang.grcboard.com;User ID=sa;Password=Passw0rd;Max Pool Size=10000000";
            string GRCBOARDPURCH = Global.ConnectionString();
            if (strCon.Trim().ToUpper() == "GRCBOARDPUSAT") strCon1 = GRCBOARDPUSAT;
            if (strCon.Trim().ToUpper() == "GRCBOARDLAPAK") strCon1 = GRCBOARDLAPAK;
            if (strCon.Trim().ToUpper() == "GRCBOARDKRWG") strCon1 = GRCBOARDKRWG;
            if (strCon.Trim().ToUpper() == "GRCBOARDCTRP") strCon1 = GRCBOARDCTRP;
            if (strCon.Trim().ToUpper() == "GRCBOARDJMB") strCon1 = GRCBOARDJMB;
            if (strCon.Trim().ToUpper() == "GRCBOARDPURCH") strCon1 = GRCBOARDPURCH;
            return strCon1;
        }

        public static List<PurchasingNf.ParamHeadData> HeadData(int id)
        {
            List<PurchasingNf.ParamHeadData> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "select (SELECT username FROM Users WHERE ID=p.userid) CreateUser, "+
"(SELECT username FROM Users WHERE ID = p.headid) HeadUser, " +
"CASE ItemTypeId "+
"WHEN 1 THEN 'Inventory' " +
"WHEN 2 THEN 'Asset' " +
"ELSE 'Biaya' END TypeItem, " +
"CASE PermintaanType " +
"WHEN 1 THEN 'Biasa' " +
"WHEN 2 THEN 'Sesuai Schedule' " +
"Else 'Top Urgent' end TypeMinta, " +
"CASE SatuanId " +
"WHEN 1 THEN 'Public' " +
"ELSE 'Private' END MultiGudangName, " +
"CASE p.Status " +
"WHEN 0 THEN 'Open' " +
"WHEN 1 THEN 'Parsial' " +
"else 'FullPo' END StatusSpp, " +
"CASE p.Approval " +
"WHEN 0 THEN 'Admin' " +
"WHEN 1 THEN 'Head' " +
"WHEN 2 THEN 'Manager' " +
"ELSE 'Pm' END ApprovalSpp, NoSpp, Minta Tanggal " +
"FROM SPP p where p.status>-1 and p.id="+id;
                    AllData = connection.Query<PurchasingNf.ParamHeadData>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PurchasingNf.ParamDtlData> DtlData(int id)
        {
            List<PurchasingNf.ParamDtlData> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT "+
"case when ItemTypeID != 3 then Keterangan else Keterangan1 end Keterangan, " +
"case ItemTypeID " +
"when 1 then(select ItemCode from Inventory where Inventory.ID = ItemID) " +
"when 2 then(select ItemCode from Asset where Asset.ID = ItemID) " +
"when 3 then(select ItemCode from Biaya where Biaya.ID = ItemID) else '' end ItemCode, " +
"case ItemTypeID " +
"when 1 then(select ItemName from Inventory where Inventory.ID = ItemID) " +
"when 2 then(select ItemName from Asset where Asset.ID = ItemID) " +
"when 3 then(select ItemName from Biaya where Biaya.ID = ItemID) + ' - ' + Keterangan else '' end ItemName, " +
"(SELECT uomcode FROM UOM WHERE ID = uomid) satuan,Quantity,TglKirim " +
"FROM SPPDetail WHERE Status > -1 and SPPID = " + id;
                    AllData = connection.Query<PurchasingNf.ParamDtlData>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PurchasingNf.ParamDataSPP> DataSpp(string NoSPP, string CreatedBy)
        {
            List<PurchasingNf.ParamDataSPP> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string Search = "";
                if (NoSPP != "") { Search += " and A.NoSPP like '%" + NoSPP + "%' "; }
                if (CreatedBy != "") { Search += " and A.CreatedBy='" + CreatedBy + "' "; }
                string query;
                try
                {
                    query = "select TOP 300  A.ID,A.NoSPP,A.HeadID,A.Minta,A.Status,A.Approval,A.CreatedBy,B.ItemID,B.Quantity, case when B.ItemTypeID != 3 then B.Keterangan else B.Keterangan1 end Keterangan, B.ItemTypeID,B.GroupID,case B.ItemTypeID when 1 then(select ItemCode from Inventory where Inventory.ID = B.ItemID) when 2 then(select ItemCode from Asset where Asset.ID = B.ItemID) when 3 then(select ItemCode from Biaya where Biaya.ID = B.ItemID) else '' end ItemCode, case B.ItemTypeID when 1 then(select ItemName from Inventory where Inventory.ID = B.ItemID) when 2 then(select ItemName from Asset where Asset.ID = B.ItemID) when 3 then(select ItemName from Biaya where Biaya.ID = B.ItemID) + ' - ' + B.Keterangan else '' end ItemName, C.UOMCode,C.UOMDesc from SPP as A, SPPDetail as B, UOM as C where A.ID = B.SPPID and B.Status > -1 and B.UOMID = C.ID and A.Status > -1 " + Search + " order by B.ID Desc";
                    AllData = connection.Query<PurchasingNf.ParamDataSPP>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PurchasingNf.ParamDeptUser> InfoUser()
        {
            List<PurchasingNf.ParamDeptUser> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                Users users = (Users)HttpContext.Current.Session["Users"];
                int deptID = users.DeptID;
                string query;
                try
                {
                    query = "SELECT ID,DeptName FROM Dept WHERE ID="+ deptID;
                    AllData = connection.Query<PurchasingNf.ParamDeptUser>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PurchasingNf.ParamListTypeItem> ListTypeItem()
        {
            List<PurchasingNf.ParamListTypeItem> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT ID, TypeDescription FROM ItemTypePurchn WHERE RowStatus>-1 AND ID!=5";
                    AllData = connection.Query<PurchasingNf.ParamListTypeItem>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PurchasingNf.ParamListTypeSpp> ListTypeSpp( int ItemTypeID)
        {
            List<PurchasingNf.ParamListTypeSpp> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                Users users = (Users)HttpContext.Current.Session["Users"];
                int deptID = users.DeptID;
                string strGroupID = " and ItemTypeID=" + ItemTypeID;
                if (ItemTypeID == 2)
                {
                    strGroupID = " and ItemTypeID=" + ItemTypeID + " and ID in (4) ";
                    if (deptID == 19 || deptID == 22 || deptID == 30)
                    {
                        strGroupID = " and ItemTypeID=" + ItemTypeID + " and ID in (4,12) ";
                    }
                }
                string query;
                try
                {
                    query = "select ID,GroupDescription from GroupsPurchn Where RowStatus >-1 " + strGroupID;
                    AllData = connection.Query<PurchasingNf.ParamListTypeSpp>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PurchasingNf.ParamListGroupSarmut> ListGroupSarmut()
        {
            List<PurchasingNf.ParamListGroupSarmut> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "WITH GroupSarmut AS (select ROW_NUMBER() OVER(Partition By vw.ID Order By vw.ID, vw.SarmutID)N, vw.*, m.GroupNaME FROM vw_SarmutGroup as vw LEFT JOIN MaterialMTCGroup as m ON m.ID = vw.ID WHERE m.RowStatus > -2) SELECT SarmutID AS ID, GroupName as ZonaName, ID as ZonaCode FROM GroupSarmut WHERE N = 1";
                    AllData = connection.Query<PurchasingNf.ParamListGroupSarmut>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PurchasingNf.ParamListForklif> ListForklif()
        {
            List<PurchasingNf.ParamListForklif> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT ID, Forklift FROM MasterForklift WHERE rowstatus=0";
                    AllData = connection.Query<PurchasingNf.ParamListForklif>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PurchasingNf.ParamListGroupAsset> ListGroupAsset()
        {
            List<PurchasingNf.ParamListGroupAsset> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT ID, NamaGroup FROM AM_Group WHERE RowStatus>-1";
                    AllData = connection.Query<PurchasingNf.ParamListGroupAsset>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PurchasingNf.ParamListGroupEfisien> ListGroupEfisien()
        {
            List<PurchasingNf.ParamListGroupEfisien> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT ID, GroupName FROM MaterialMTCGroup WHERE RowStatus>-1";
                    AllData = connection.Query<PurchasingNf.ParamListGroupEfisien>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PurchasingNf.ParamListKelasAsset> ListKelasAsset(int GroupAsset)
        {
            List<PurchasingNf.ParamListKelasAsset> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT ID, NamaClass FROM AM_Class WHERE RowStatus>-1 and GroupID="+ GroupAsset;
                    AllData = connection.Query<PurchasingNf.ParamListKelasAsset>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PurchasingNf.ParamListAsset> ListAssetTunggal()
        {
            List<PurchasingNf.ParamListAsset> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "select xx1.ID,xx1.NamaAsset,xx1.KodeProjectAsset,xx1.NamaProjectAsset from (select B.ItemName NamaAsset,xx.* from (select ID,substring(ItemCode,1,13)KodeAsset,ItemCode as KodeProjectAsset, ItemName as  NamaProjectAsset from Asset where Head=2 and RowStatus>-1 and LEN(ItemCode)>=13) as xx inner join Asset B ON B.ItemCode=xx.KodeAsset ) as xx1 where xx1.KodeProjectAsset not in (select KodeAsset from AM_Asset where RowStatus>-1) ";
                    AllData = connection.Query<PurchasingNf.ParamListAsset>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PurchasingNf.ParamListAsset> ListAssetUtama()
        {
            List<PurchasingNf.ParamListAsset> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                Users users = (Users)HttpContext.Current.Session["Users"];
                int UnitKerjaID = users.UnitKerjaID;
                string LongCaracter = "14";
                if (users.UnitKerjaID.ToString().Length == 1) { LongCaracter = "13"; }
                string query;
                try
                {
                    query = "select xx1.ID,xx1.NamaAsset,xx1.KodeProjectAsset,xx1.NamaProjectAsset from (select B.ItemName NamaAsset, xx.*from( select ID, substring(ItemCode, 1, "+ LongCaracter + ")KodeAsset, ItemCode as KodeProjectAsset, ItemName as NamaProjectAsset from Asset where Head = 1 and RowStatus > -1  and LEN(ItemCode) >= "+ LongCaracter + " ) as xx inner join Asset B ON B.ItemCode = xx.KodeAsset ) as xx1 where xx1.KodeProjectAsset not in (select KodeAsset from AM_Asset where LEN(KodeAsset) >= 13 and RowStatus> -1) ";
                    AllData = connection.Query<PurchasingNf.ParamListAsset>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PurchasingNf.ParamListKomponen> ListKomponen(string AssetItem)
        {
            List<PurchasingNf.ParamListKomponen> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = " select ID, ItemName from Asset where ItemCode like'%" + AssetItem + "%' and RowStatus>-1 and Head=0 ";
                    AllData = connection.Query<PurchasingNf.ParamListKomponen>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PurchasingNf.ParamListKomponen> ListKomponenTunggal(string AssetItem)
        {
            List<PurchasingNf.ParamListKomponen> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = " select ID,ItemName from Asset where ItemCode like'%" + AssetItem + "%' and RowStatus>-1 and Head=2 ";
                    AllData = connection.Query<PurchasingNf.ParamListKomponen>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static int CheckStatusBiaya()
        {
            AccClosingFacade cls = new AccClosingFacade();
            AccClosing stat = cls.CheckBiayaAktif();
            return stat.Status;
        }

        public static List<PurchasingNf.ParamDataBiaya> DataBiaya(int TypeSpp)
        {
            List<PurchasingNf.ParamDataBiaya> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string strField = "len(itemcode) = 15 and ItemTypeID";
                string strValue= "3";
                string strGroupID = " and A.GroupID =" + TypeSpp;
                string query;
                try
                {
                    query = "select A.ID,A.ItemCode, A.ItemName,A.SupplierCode, A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah, A.Harga,A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head, A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID, A.aktif from Biaya as A,UOM as C where  A.aktif =1 and  A.RowStatus>-1 and A.UOMID = C.ID and " + strField + " like '%" + strValue + "%' " + strGroupID;
                    AllData = connection.Query<PurchasingNf.ParamDataBiaya>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PurchasingNf.ParamListBiaya> ListBiaya(string Keterangan, int ItemId)
        {
            List<PurchasingNf.ParamListBiaya> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT top 10 ID, ItemName FROM Biaya where ItemName like '%"+ Keterangan + "%' and aktif=1 and rowStatus>-1 AND len(ItemCode) != 15 and keterangan in (select itemcode from biaya where id="+ItemId+")";
                    AllData = connection.Query<PurchasingNf.ParamListBiaya>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static int CheckDeptID(int UserID)
        {
            int status = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select DeptID from users where ID=" + UserID;
                    status = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return status;
        }

        public static int CheckReorderPointSPP(string TypeSpp, int UserID)
        {
            int status = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select * from (SELECT A.*, B.SPPQty,B.status,B.userid  FROM Inventory A left JOIN ROP B ON A.ID = B.ItemID " +
                "where (A.Jumlah <= A.ReOrder) AND (A.ReOrder > 0) AND A.Aktif=1 AND (A.GroupID in (" + TypeSpp + "))) as tblreorder " +
                "where ISNULL(sppqty,0)+Jumlah<=reorder and userid=" + UserID;
                    status = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return status;
        }

        public static List<PurchasingNf.ParamListItem> ListItemInventory(string NameItem, int TypeSpp)
        {
            List<PurchasingNf.ParamListItem> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                Users users = (Users)HttpContext.Current.Session["Users"];
                int UserID = users.ID;
                string GroupID = " and A.GroupID =" + TypeSpp + " and A.GroupID not in(6)";
                if (TypeSpp == 7) { GroupID = " and A.GroupID in (7,10)"; }
                if (TypeSpp == 6) { GroupID = " and A.GroupID in(6)"; }
                int Rop = 0;
                if (TypeSpp == 1 || TypeSpp == 2 || TypeSpp == 6 || TypeSpp == 8 || TypeSpp == 9)
                {

                    Rop = (CheckDeptID(UserID) == 10) ? CheckReorderPointSPP(TypeSpp.ToString(), UserID) : 0;
                }
                string query;
                try
                {
                    if (Rop == 0)
                    {
                        query = "select A.ID,A.ItemCode,A.ItemName from Inventory as A,UOM as C where A.aktif =1 and A.RowStatus>-1 and A.UOMID = C.ID and ItemName like '%" + NameItem + "%' " + GroupID + " order by A.itemname ";
                    }
                    else
                    {
                        query = "select ID,ItemCode,ItemName from ( select A.ID,A.ItemCode,A.ItemName, isnull((select SUM(quantity) from vw_StockPurchn where ItemID = A.ID),0)Jumlah, A.ReOrder,  B.SPPQty FROM Inventory A left JOIN ROP B ON A.ID = B.ItemID inner join UOM as C on A.UOMID = C.ID where(A.Jumlah <= A.ReOrder) AND(A.ReOrder > 0) AND(A.GroupID = " + TypeSpp + ") and ISNULL(status,0)= 0) as tblreorder where ISNULL(sppqty,0)+Jumlah <= reorder  order by tblreorder.itemname";
                    }
                    AllData = connection.Query<PurchasingNf.ParamListItem>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PurchasingNf.ParamListItem> ListItemAsset(string NameItem, int TypeSpp)
        {
            List<PurchasingNf.ParamListItem> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string GroupID = " and GroupID =" + TypeSpp + " and GroupID not in(6)";
                string query;
                try
                {
                    query = "select stock,ID,ItemCode,ItemName from Asset where aktif = 1 and RowStatus > -1 and ItemName like '%" + NameItem + "%' " + GroupID;
                    AllData = connection.Query<PurchasingNf.ParamListItem>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static int CekBiaya(string Keterangan)
        {
            int ID = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "SELECT sum(ID) ID FROM Biaya Where RowStatus>-1 And ItemName='"+ Keterangan + "'";
                    ID = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return ID;
        }

        public static int CheckBiayaAktif()
        {
            int status = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select RowStatus as Status,Year(ModifiedTime) as Tahun, Month(ModifiedTime) as Bulan from BiayaNew";
                    status = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return status;
        }

        public static List<PurchasingNf.ParamListItem> ListItemBiaya(string NameItem, int TypeSpp)
        {
            List<PurchasingNf.ParamListItem> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                //AccClosingFacade cls = new AccClosingFacade();
                //AccClosing stat = cls.CheckBiayaAktif();
                string GroupID = " and A.GroupID =" + TypeSpp;
                int Stat = CheckBiayaAktif();
                string query;
                try
                {
                    if (Stat != 1)
                    {
                        query = "select A.ID,A.ItemCode,A.ItemName,A.Keterangan from Biaya as A,UOM as C where  A.aktif =1 and  A.RowStatus>-1 and A.UOMID = C.ID and ItemName like '%" + NameItem + "%' " + GroupID;
                    }
                    else
                    {
                        query = "select A.ID,A.ItemCode,A.ItemName, A.Keterangan from Biaya as A,UOM as C where  A.aktif =1 and  A.RowStatus>-1 and A.UOMID = C.ID and len(itemcode)=15 and ItemTypeID=3 " + GroupID;
                    }
                        
                    AllData = connection.Query<PurchasingNf.ParamListItem>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PurchasingNf.ParamDeadStock> LoadDeadStockLocal(string ItemCode, string Tanggal)
        {
            List<PurchasingNf.ParamDeadStock> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string strtanggal = string.Empty;
                string plant = string.Empty;
                Users users = (Users)HttpContext.Current.Session["Users"];
                if (users.UnitKerjaID == 1)
                {
                    plant = "'Citeureup'";
                }
                if (users.UnitKerjaID == 7)
                {
                    plant = "'Karawang'";
                }
                if (users.UnitKerjaID == 13)
                {
                    plant = "'Jombang'";
                }
                string query;
                try
                {
                    query = "exec getdatadeadstock1  '" + Tanggal + "',24,0,'" + ItemCode + "'" +
                    "select top 1 " + plant + " Plant,Itemcode,Stock from tempdeadstock where itemcode='" + ItemCode + "'";
                    AllData = connection.Query<PurchasingNf.ParamDeadStock>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PurchasingNf.ParamDeadStock> LoadDeadStock1(string ItemCode, string Tanggal)
        {
            List<PurchasingNf.ParamDeadStock> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                Users users = (Users)HttpContext.Current.Session["Users"];
                string strtanggal = string.Empty;
                string Server1 = string.Empty;
                string TbServer1 = string.Empty;
                string plant = string.Empty;
                if (users.UnitKerjaID == 1)
                {
                    plant = "'Karawang'";
                    Server1 = "sqlKrwg.grcboard.com";
                    TbServer1 = "[sqlKrwg.grcboard.com].bpasKrwg.dbo.";
                }
                if (users.UnitKerjaID == 7)
                {
                    plant = "'Citeureup'";
                    Server1 = "sqlctrp.grcboard.com";
                    TbServer1 = "[sqlctrp.grcboard.com].bpasctrp.dbo.";
                }
                if (users.UnitKerjaID == 13)
                {
                    plant = "'Citeureup'";
                    Server1 = "sqlctrp.grcboard.com";
                    TbServer1 = "[sqlctrp.grcboard.com].bpasctrp.dbo.";
                }
                string query;
                try
                {
                    query = "declare @srvrctrp nvarchar(128), @retvalctrp int " +
                    "set @srvrctrp = '" + Server1 + "'; " +
                    "begin try " +
                    "    exec @retvalctrp = sys.sp_testlinkedserver @srvrctrp; " +
                    "end try " +
                    "begin catch " +
                    "    set @retvalctrp = sign(@@error); " +
                    "end catch; " +
                    "exec " + TbServer1 + "getdatadeadstock1  '" + Tanggal + "',24,0,'" + ItemCode + "'" +
                    "select top 1 " + plant + " Plant,Itemcode,Stock from " + TbServer1 + "tempdeadstock where itemcode='" + Tanggal + "'";
                    AllData = connection.Query<PurchasingNf.ParamDeadStock>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PurchasingNf.ParamDeadStock> LoadDeadStock2(string ItemCode, string Tanggal)
        {
            List<PurchasingNf.ParamDeadStock> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                Users users = (Users)HttpContext.Current.Session["Users"];
                string strtanggal = string.Empty;
                string Server1 = string.Empty;
                string TbServer1 = string.Empty;
                string plant = string.Empty;
                if (users.UnitKerjaID == 1)
                {
                    plant = "'Jombang'";
                    Server1 = "sqlJombang.grcboard.com";
                    TbServer1 = "[sqlJombang.grcboard.com].bpasJombang.dbo.";
                }
                if (users.UnitKerjaID == 7)
                {
                    plant = "'Jombang'";
                    Server1 = "sqlJombang.grcboard.com";
                    TbServer1 = "[sqlJombang.grcboard.com].bpasJombang.dbo.";
                }
                if (users.UnitKerjaID == 13)
                {
                    plant = "'Karawang'";
                    Server1 = "sqlKrwg.grcboard.com";
                    TbServer1 = "[sqlKrwg.grcboard.com].bpasKrwg.dbo.";
                }
                string query;
                try
                {
                    query = "declare @srvrctrp nvarchar(128), @retvalctrp int " +
                    "set @srvrctrp = '" + Server1 + "'; " +
                    "begin try " +
                    "    exec @retvalctrp = sys.sp_testlinkedserver @srvrctrp; " +
                    "end try " +
                    "begin catch " +
                    "    set @retvalctrp = sign(@@error); " +
                    "end catch; " +
                    "exec " + TbServer1 + "getdatadeadstock1  '" + Tanggal + "',24,0,'" + ItemCode + "'" +
                    "select top 1 " + plant + " Plant,Itemcode,Stock from " + TbServer1 + "tempdeadstock where itemcode='" + ItemCode + "'";
                    AllData = connection.Query<PurchasingNf.ParamDeadStock>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PurchasingNf.ParamInfoItem> InfoItem(int ItemId)
        {
            List<PurchasingNf.ParamInfoItem> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = " select ID,ItemName from Asset where ItemCode like'%" + ItemId + "%' and RowStatus>-1 and Head=2 ";
                    AllData = connection.Query<PurchasingNf.ParamInfoItem>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static bool LockUserSPP(int ItemID, int TypeItem, string Keterangan)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            ArrayList arrSPP = new SPPDetailFacade().RetrievePending(users.ID);
            return (arrSPP.Count > 0) ? true : false;
        }

        public static string CheckLastReceipt(int ItemID, string ItemName, int TypeItem, int Qty, string Keterangan)
        {

            /**
             * Skenario blok material untuk di spp :
             * 1. Jika material yang akan di spp adalah Stock 
             * 2. Jika material tersebut sudah di buat spp dan totalnya (qty SPP + stock per item) melebihi maxstock
             * 3. Jika Total Stok per item dan jml PO yang blm di receipt sudah melebihi MaxStock
             * 4. Jika Material tersebut statusnya adalah Pending PO karena ada proses pending dari Purchasing
             * 5. Jika Material tersebut adalah material yng dibudgetkan (18/02/2016)
             */
            string Message = string.Empty;
            Inventory inv = new InventoryFacade().RetrieveByIdNew(ItemID, TypeItem);
            #region Jika dikarenakan proses stock (Point 1,2,3)
            if (inv.Stock > 0)
            {
                #endregion
                decimal TotalStock = 0;
                decimal MaxStock = inv.MaxStock;
                ROPFacade rop = new ROPFacade();
                decimal sppBlmPo = rop.JumlahSPPBlmPO(inv.ID, inv.ItemTypeID);
                decimal poBlmReceipt = rop.JumlahPOblmReceipt(inv.ID, inv.ItemTypeID);
                decimal jmlSPB = (Global.IsNumeric(Qty)) ? Qty : 0;
                TotalStock = sppBlmPo + poBlmReceipt + jmlSPB + inv.Jumlah;

                if (MaxStock < TotalStock)
                {
                    //jika sudah melebihi max stock
                    Message = "Permintaan Sudah melebihi MaxStock (" + MaxStock.ToString("###,##0.#0") + ")\\n";
                    Message += "Stock Saat ini : " + inv.Jumlah.ToString("###,##0.#0");
                    if (poBlmReceipt > 0)
                    {
                        Message += "\\nOut Standing PO : " + poBlmReceipt.ToString("###,##0.#0");
                    }
                    if (sppBlmPo > 0)
                    {
                        Message += "\\nOut Standing SPP : " + sppBlmPo.ToString("###,##0.00");
                    }
                    Message += "\\nQuantity SPP Baru : " + jmlSPB.ToString("###,##0.00");
                    Message += "\\nTotal   : " + TotalStock.ToString("###,##0.00");
                    Message += "\\n";
                }

            }
            #region jika ada material yang spp nya di pending pembuatan PO nya oleh Purchasing
            if (LockUserSPP(ItemID, TypeItem, Keterangan) == true)
            {
                Message += (TypeItem == 3) ? Keterangan : ItemName;
                Message += "Ini Statusanya Pending PO tidak bisa di SPP lagi sementara\\n";
            }
            #endregion
            #region Jika Material tersebut adalah material yng dibudgetkan
            string BudgetBlock = "1";
            if ((inv.Head == 1 || inv.Head == 2) && BudgetBlock == "1")
            {
                Message += inv.ItemName + "\\n masuk di master budget. Tidak Bisa di SPP Manual\\n";
            }
            #endregion
            return Message;
        }

        /*
        public static int checkNonStock(string itemname, int itemtypeid)
        {
            int Stock = 0;
            Users users = (Users)HttpContext.Current.Session["Users"];
            if (rbOn.Checked == true)
            {
                if (users.UnitKerjaID == 7)
                {
                    Panel2.Visible = true;
                    WebReference_Ctrp.Service1 webServiceCTP = new WebReference_Ctrp.Service1();
                    DataSet dsListReceipt = webServiceCTP.GetItemByName(itemname, itemtypeid);
                    GridView2.DataSource = dsListReceipt;
                    GridView2.DataBind();
                    LblStock.Visible = true;
                    LblStock.Text = "Stock Barang Citeureup";
                }
                else
                {
                    Panel2.Visible = true;
                    WebReference_Krwg.Service1 webServiceKRW = new WebReference_Krwg.Service1();
                    DataSet dsListReceipt = webServiceKRW.GetItemByName(itemname, itemtypeid);
                    GridView2.DataSource = dsListReceipt;
                    GridView2.DataBind();
                    LblStock.Visible = true;
                    LblStock.Text = "Stock Barang Karawang";
                }
            }
            return Stock;
        }
        */

        public static int checkLockATK(string itemid, int userid)
        {
            int locking = 0;
            SPPFacade sppf = new SPPFacade();
            locking = sppf.GetLockingATK(userid, itemid);
            return locking;
        }

        public static int GetHariLibur(string frDate, string tDate)
        {
            POPurchnFacade DayOff = new POPurchnFacade();
            POPurchn JmlHari = DayOff.DayOffCalender(frDate, tDate);
            return JmlHari.Status;
        }

        public static List<PurchasingNf.ParamPurchnTools> PurchnTools()
        {
            List<PurchasingNf.ParamPurchnTools> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "Select Status From Purchn_tools where ModulName='BiayaAutoSave'";
                    AllData = connection.Query<PurchasingNf.ParamPurchnTools>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static string HitungNoAkhir(string strPrefix)
        {
            string NoAkhir = string.Empty;
            if (strPrefix.Length == 7)
            {
                BiayaFacade biayaFacade = new BiayaFacade();
                int noUrut = biayaFacade.CountItemCode(strPrefix);
                noUrut = noUrut + 1;
                //supplier.SupplierCode = "C" + noUrut.ToString().PadLeft(4, '0');
                NoAkhir = strPrefix + noUrut.ToString().PadLeft(4, '0');
                return NoAkhir;
            }
            return NoAkhir;
        }

        public class FacadeAsset
        {
            public string strError = string.Empty;
            private ArrayList arrData = new ArrayList();
            private List<SqlParameter> sqlListParam;
            private PurchasingNf.DomainAsset objAsset = new PurchasingNf.DomainAsset();

            public FacadeAsset()
                : base()
            {

            }
            public string Criteria { get; set; }
            public string Field { get; set; }
            public string Where { get; set; }

            public ArrayList RetrieveAssetUtama()
            {
                //string strSQL = " select ID,KodeAsset,NamaAsset from AM_Asset where JenisAsset=1 and RowStatus>-1 order by ID desc ";
                //string strSQL = "select ID, ItemCode as  KodeAsset, ItemName as  NamaAsset from Asset where Head=1 and RowStatus>-1 and LEN(ItemCode)>11 order by ID desc";
                //string strSQL =
                //" select xx1.ID,xx1.NamaAsset,xx1.KodeProjectAsset,xx1.NamaProjectAsset from " +
                //" (select B.ItemName NamaAsset,xx.* from (select ID,substring(ItemCode,1,13)KodeAsset,ItemCode as  KodeProjectAsset, ItemName as  NamaProjectAsset " +
                //" from Asset where Head=1 and RowStatus>-1  and LEN(ItemCode)>=13) as xx inner join Asset B ON B.ItemCode=xx.KodeAsset ) as xx1 " +
                //" where xx1.KodeProjectAsset  not in (select KodeAsset from AM_Asset where RowStatus>-1) ";
                //DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
                //strError = dataAccess.Error;
                //arrData = new ArrayList();

                //if (sqlDataReader.HasRows)
                //{
                //    while (sqlDataReader.Read())
                //    {
                //        arrData.Add(GenerateObjectAssetUtama(sqlDataReader));
                //    }
                //}

                //return arrData;
                Users users = (Users)HttpContext.Current.Session["Users"];
                string query = string.Empty;
                if (users.UnitKerjaID.ToString().Length == 1)
                {
                    query = "13";
                }
                else
                {
                    query = "14";
                }

                string strSQL =
                " select xx1.ID,xx1.NamaAsset,xx1.KodeProjectAsset,xx1.NamaProjectAsset from " +
                " (select B.ItemName NamaAsset,xx.* from (select ID,substring(ItemCode,1," + query + ")KodeAsset,ItemCode as  KodeProjectAsset, ItemName as  NamaProjectAsset " +
                " from Asset where Head=1 and RowStatus>-1  and LEN(ItemCode)>=" + query + ") as xx inner join Asset B ON B.ItemCode=xx.KodeAsset ) as xx1 " +
                " where xx1.KodeProjectAsset  not in (select KodeAsset from AM_Asset where LEN(KodeAsset)>=13 and RowStatus>-1) ";
                DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
                strError = dataAccess.Error;
                arrData = new ArrayList();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        arrData.Add(GenerateObjectAssetUtama(sqlDataReader));
                    }
                }

                return arrData;
            }

            public ArrayList RetrieveAssetTunggal()
            {
                //string strSQL = " select ID,KodeAsset,NamaAsset from AM_Asset where JenisAsset=1 and RowStatus>-1 order by ID desc ";
                //string strSQL = "select ID, ItemCode as  KodeAsset, ItemName as  NamaAsset from Asset where Head=1 and RowStatus>-1 and LEN(ItemCode)>11 order by ID desc";
                string strSQL =
                " select xx1.ID,xx1.NamaAsset,xx1.KodeProjectAsset,xx1.NamaProjectAsset from " +
                " (select B.ItemName NamaAsset,xx.* from (select ID,substring(ItemCode,1,13)KodeAsset,ItemCode as  KodeProjectAsset, ItemName as  NamaProjectAsset " +
                " from Asset where Head=2 and RowStatus>-1  and LEN(ItemCode)>=13) as xx inner join Asset B ON B.ItemCode=xx.KodeAsset ) as xx1 " +
                " where xx1.KodeProjectAsset  not in (select KodeAsset from AM_Asset where RowStatus>-1) ";
                DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
                strError = dataAccess.Error;
                arrData = new ArrayList();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        arrData.Add(GenerateObjectAssetUtama(sqlDataReader));
                    }
                }

                return arrData;
            }

            private PurchasingNf.DomainAsset GenerateObjectAssetUtama(SqlDataReader sdr)
            {
                PurchasingNf.DomainAsset objAsset = new PurchasingNf.DomainAsset();

                objAsset.ID = Convert.ToInt32(sdr["ID"]);
                objAsset.NamaAsset = sdr["NamaAsset"].ToString();
                objAsset.KodeProjectAsset = sdr["KodeProjectAsset"].ToString();
                objAsset.NamaProjectAsset = sdr["NamaProjectAsset"].ToString();

                return objAsset;
            }

            public ArrayList RetrieveAssetKomponen(string KodeAssetKomponen)
            {
                //string strSQL = " select ID,KodeAsset,NamaAsset from AM_Asset where JenisAsset=1 and RowStatus>-1 order by ID desc ";
                //string strSQL = "select ID, ItemCode as  KodeAsset, ItemName as  NamaAsset from Asset where Head=1 and RowStatus>-1 and LEN(ItemCode)>11 order by ID desc";
                string strSQL =
                " select ID,ItemCode KodeProjectAsset,ItemName NamaProjectAsset from Asset where ItemCode like'%" + KodeAssetKomponen + "%' and RowStatus>-1 and Head=0 ";
                DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
                strError = dataAccess.Error;
                arrData = new ArrayList();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        arrData.Add(GenerateObjectAssetKomponen(sqlDataReader));
                    }
                }

                return arrData;
            }

            public ArrayList RetrieveAssetTunggal(string KodeAssetKomponen)
            {
                //string strSQL = " select ID,KodeAsset,NamaAsset from AM_Asset where JenisAsset=1 and RowStatus>-1 order by ID desc ";
                //string strSQL = "select ID, ItemCode as  KodeAsset, ItemName as  NamaAsset from Asset where Head=1 and RowStatus>-1 and LEN(ItemCode)>11 order by ID desc";
                string strSQL =
                " select ID,ItemCode KodeProjectAsset,ItemName NamaProjectAsset from Asset where ItemCode like'%" + KodeAssetKomponen + "%' and RowStatus>-1 and Head=2 ";
                DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
                strError = dataAccess.Error;
                arrData = new ArrayList();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        arrData.Add(GenerateObjectAssetKomponen(sqlDataReader));
                    }
                }

                return arrData;
            }

            private PurchasingNf.DomainAsset GenerateObjectAssetKomponen(SqlDataReader sdr)
            {
                PurchasingNf.DomainAsset objAsset = new PurchasingNf.DomainAsset();

                objAsset.ID = Convert.ToInt32(sdr["ID"]);
                objAsset.KodeProjectAsset = sdr["KodeProjectAsset"].ToString();
                objAsset.NamaProjectAsset = sdr["NamaProjectAsset"].ToString();

                return objAsset;
            }

            public int RetrieveClassID(string KodeClass, int Group)
            {
                string StrSql =
                //" select COUNT(KodeAsset)NoUrut  from AM_Asset where KodeAsset like'%" + NamaKompAsset + "%' and JenisAsset=2 and RowStatus>-1 ";
                " select ID from AM_Class where GroupID=" + Group + " and RowStatus>-1 and KodeClass='" + KodeClass + "' ";

                DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
                strError = dataAccess.Error;

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        return Convert.ToInt32(sqlDataReader["ID"]);
                    }
                }

                return 0;
            }

            public int RetrieveSubClassID(string NamaClass, int Group)
            {
                string StrSql =
                //" select COUNT(KodeAsset)NoUrut  from AM_Asset where KodeAsset like'%" + NamaKompAsset + "%' and JenisAsset=2 and RowStatus>-1 ";
                " select ID from AM_SubClass where ClassID in (select ID from AM_Class where GroupID=" + Group + " and RowStatus>-1 ) " +
                " and RowStatus>-1 and NamaClass='" + NamaClass + "' ";

                DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
                strError = dataAccess.Error;

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        return Convert.ToInt32(sqlDataReader["ID"]);
                    }
                }

                return 0;
            }

        }

        public void InsertLog(string eventName, string NoSpp)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Input SPP";
            eventLog.EventName = eventName;
            eventLog.DocumentNo = NoSpp;
            eventLog.CreatedBy = users.UserName;
            EventLogFacade eventLogFacade = new EventLogFacade();
            int intResult = eventLogFacade.Insert(eventLog);

        }

        public static string GetItemCode(int ID)
        {
            string val = "";
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "SELECT ItemCode FROM Inventory WHERE ID=" + ID;
                    val = connection.QueryFirstOrDefault<string>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static string CekNoTrn(int ID)
        {
            string NoSPP = "";
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "SELECT NoSPP FROM SPP WHERE ID="+ID;
                    NoSPP = connection.QueryFirstOrDefault<string>(query);
                }
                catch (Exception e)
                {

                }
            }
            return NoSPP;
        }

        public static decimal JumlahSPPBlmPO(int id,int type)
        {
            decimal val = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select ISNULL((SUM(Quantity) - SUM(QtyPO)), 0)Jumlah from SPPDetail sp " +
                            "LEFT JOIN SPP s ON s.ID =sp.SPPID " +
                            "where sp.status>-1 and s.Status>-1 and sp.itemid=" + id + " and sp.ItemTypeID=" + type +
                            "and Quantity >QtyPO";
                    val = connection.QueryFirstOrDefault<decimal>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }
        public static decimal JumlahPOblmReceipt(int id, int type)
        {
            decimal val = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select ISNULL(SUM(PO.Qty),0)AS Jumlah from POPurchnDetail AS PO " +
                            "LEFT JOIN PoPurchn as P on P.ID=PO.POID " +
                            "WHERE PO.Status>-1 and PO.ItemTypeID=" + type + " and PO.itemid =" + id + " and " +
                            "PO.ID not in(select PODetailID from ReceiptDetail AS RD where RD.RowStatus>-1 and RD.ItemID=" +
                            id + " and RD.ItemTypeID=" + type + ") " +
                            "AND P.Status>-1";
                    val = connection.QueryFirstOrDefault<decimal>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static string UpdateROPBySPP(int itemid, int sppid, decimal sppqty)
        {
            string val = "";
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "update rop set sppid="+ sppid + ",sppqty="+ sppqty + ",ropdate=GETDATE() where itemid="+ itemid;
                    val = connection.QueryFirstOrDefault<string>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static int CekSppInAtk(int spp, int item)
        {
            int val = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select count (id) id from BudgetATKDetail where sppid=" + spp + " and ItemID=" + item + "and RowStatus>-1";
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static int CekSppInRop(int spp, int item)
        {
            int val = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select count (id) id from rop where sppid="+spp+ " and ItemID=" + item + " and Status>-1";
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

    }//_____________________________
}
