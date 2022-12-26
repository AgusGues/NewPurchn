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
    public class EstimasiMaterialNfFacade : AbstractTransactionFacade
    {
        private EstimasiMaterialNf.ParamHead obj = new EstimasiMaterialNf.ParamHead();
        private List<SqlParameter> sqlListParam;
        public EstimasiMaterialNfFacade(object objDomain)
            : base(objDomain)
        {
            obj = (EstimasiMaterialNf.ParamHead)objDomain;
        }
        public override int Delete(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Insert(TransactionManager transManager)
        {
            int result = 0;
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", obj.ItemID));
                sqlListParam.Add(new SqlParameter("@ProjectID", obj.ProjectID));
                sqlListParam.Add(new SqlParameter("@Jumlah", obj.Jumlah));
                sqlListParam.Add(new SqlParameter("@Harga", obj.Harga));
                sqlListParam.Add(new SqlParameter("@Schedule", obj.Schedule));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", obj.ItemTypeId));
                sqlListParam.Add(new SqlParameter("@CreatedBy", ((Users)HttpContext.Current.Session["Users"]).UserID));
                sqlListParam.Add(new SqlParameter("@RowStatus", obj.RowStatus));
                result = transManager.DoTransaction(sqlListParam, "spMTC_ProjectMaterial_Insert1");
                strError = transManager.Error;
            }
            catch
            {
                result = -1;
            }
            return result;
        }

        public override int Update(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", obj.Id));
                sqlListParam.Add(new SqlParameter("@Jumlah", obj.Jumlah));
                sqlListParam.Add(new SqlParameter("@Schedule", obj.Schedule));
                sqlListParam.Add(new SqlParameter("@Harga", obj.Harga));
                sqlListParam.Add(new SqlParameter("@RowStatus", obj.RowStatus));
                sqlListParam.Add(new SqlParameter("@CreatedBy", ((Users)HttpContext.Current.Session["Users"]).UserID));
                int intResult = transManager.DoTransaction(sqlListParam, "spMTC_ProjectMaterial_Update");
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

        public static List<EstimasiMaterialNf.ParamDeptPemohon> ListDeptPemohon()
        {
            List<EstimasiMaterialNf.ParamDeptPemohon> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT Id, DeptName FROM Dept WHERE RowStatus>-1 and Id in (2,3,4,5,9,6,10,7,14,18,19,26)";
                    AllData = connection.Query<EstimasiMaterialNf.ParamDeptPemohon>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<EstimasiMaterialNf.ParamData> GetListData(string status, int dept, string nomor)
        {
            List<EstimasiMaterialNf.ParamData> AllData = new List<EstimasiMaterialNf.ParamData>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string where = "";
                if (status != "")
                {
                    where = " and mp.Status=" + status + "";
                    if (status == "21") { where = " and mp.Status=2 and mp.RowStatus >=1"; }
                    if (status == "2") { where = "and mp.Status=2 And mp.Approval=2 and mp.RowStatus!=1"; }
                    if (status == "0") { where = " and mp.Approval=0"; }

                }
                if (dept != 0) { where = " and mp.DeptID = " + dept; }
                if (nomor != "") { where = " and mp.Nomor = '" + nomor + "'"; }
                string query;
                try
                {
                    query = "SELECT top 100 " +
    "mp.Id, d.Alias DeptName, mp.ProjectName,mp.FromDate,mp.ToDate, " +
    "mp.Biaya EstimasiBiaya,mp.Sasaran,mp.Nomor KodeProject, " +
    "case " +
"when mp.Approval = 0 then 'Admin' " +
"when mp.Approval = 1 then 'Pm' " +
"else " +
    "case when mp.Biaya > 50000000 then 'Direksi' else 'Pm' end " +
  "end Approval, " +
"case  " +
"when mp.Status = 0 or mp.Status = 1 then 'Open' " +
"when mp.Status = 3 then 'Close' " +
"when mp.Status = 4 then 'Pending' " +
"when mp.Status = 2 then " +
    "case  " +
    "when mp.RowStatus = 1 then 'Finish' " +
    "when mp.RowStatus = 2 then 'Hand Over' " +
    "when mp.RowStatus = 3 then 'Close' " +
    "else 'Release' end " +
"else 'Cancel' end Status, mp.RowStatus, " +
    "(select isnull(cast(SUM(Harga) as decimal(11,2)), 0) from vw_mtcproject where ProjectID = mp.ID and RowStatus > -1 ) BiayaActual " +
    "FROM MTC_Project mp " +
    "LEFT JOIN Dept d on d.ID = mp.DeptID " +
    "where mp.RowStatus > -1 " + where + " order by mp.Id desc";
                    AllData = connection.Query<EstimasiMaterialNf.ParamData>(query).ToList();
                }
                catch (Exception)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<EstimasiMaterialNf.ParamDetail> GetListDetail(int Id)
        {
            List<EstimasiMaterialNf.ParamDetail> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "WITH q AS ( "+
    "select ProjectID, ItemID, ItemtypeID, Jumlah, Harga,(Jumlah * Harga)TotalHarga, " +
    "0 QuanTity, 0 AvgPrice, 0 AktualHarga, 1 Planning " +
    "from MTC_ProjectMaterial where ProjectID = 746 And RowStatus > -2 " +
    "Union All " +
    "select ProjectID, ItemID, ItemTypeID, 0 Jumlah, 0 Harga, 0 TotalHarga, " +
    "QuanTity, AvgPrice, (Quantity * AvgPrice)AktualHarga, 2 Aktual " +
    "from vw_mtcproject where ProjectID = " + Id +
"), " +
"w AS( " +
    "SELECT ProjectID, " +
    "(Select dbo.ItemCodeInv(ItemID, ItemTypeID))ItemCode, " +
    "(Select dbo.ItemNameInv(ItemID, ItemTypeID))ItemName, " +
    "(Select dbo.SatuanInv(ItemID, ItemTypeID))UomCode, " +
    "SUM(Jumlah)Jumlah,Avg(Harga)Harga,Sum(TotalHarga)TotalHarga, " +
    "SUM(Quantity)QtyAktual,avg(AvgPrice)HargaAktual,isnull(Sum(AktualHarga), 0)TotalAktual " +
    "From q Group By ItemID, ItemTypeID, ProjectID " +
") " +
"SELECT b.*,(TotalAktual - TotalHarga)Selisih " +
"FROM w b " +
"LEFT JOIN MTC_Project p ON p.ID = b.ProjectID order by ItemName";
                    AllData = connection.Query<EstimasiMaterialNf.ParamDetail>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<EstimasiMaterialNf.ParamKodeProject> ListKodeProject()
        {
            List<EstimasiMaterialNf.ParamKodeProject> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT Id, Nomor FROM MTC_Project WHERE Status=0 AND ApvPM=1 AND RowStatus>-1 "+
                        "UNION ALL " +
 "SELECT Id, Nomor FROM MTC_Project WHERE Status = 1 AND Approval = 1 AND ApvPM = 1 AND RowStatus = 1";
                    AllData = connection.Query<EstimasiMaterialNf.ParamKodeProject>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<EstimasiMaterialNf.ParamTypeItem> ListTypeItem()
        {
            List<EstimasiMaterialNf.ParamTypeItem> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT ID, TypeDescription Name FROM ItemTypePurchn WHERE RowStatus>-1";
                    AllData = connection.Query<EstimasiMaterialNf.ParamTypeItem>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<EstimasiMaterialNf.ParamNameItem> ListNameItem( int type, string name)
        {
            List<EstimasiMaterialNf.ParamNameItem> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string table = "biaya";
                if (type == 1) { table = "inventory"; }
                if (type == 2) { table = "asset"; }
                string query;
                try
                {
                    query = "SELECT TOP 100 Id, itemCode+' - '+ItemName ItemName FROM " + table + 
                        " WHERE RowStatus>-1 AND ItemName LIKE '%"+name+"%' ORDER BY ID DESC";
                    AllData = connection.Query<EstimasiMaterialNf.ParamNameItem>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<EstimasiMaterialNf.ParamInfoItem> InfoNameItem(int item, int type)
        {
            List<EstimasiMaterialNf.ParamInfoItem> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT TOP 1 Price, (SELECT POPurchnDate FROM POPurchn WHERE ID=POID) Tanggal "+
"FROM POPurchnDetail WHERE ItemID = 3594 AND ItemTypeID = 3 AND Status> -1 ORDER BY ID DESC";
                    AllData = connection.Query<EstimasiMaterialNf.ParamInfoItem>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<EstimasiMaterialNf.ParamListMaterial> ListMaterial(int Id)
        {
            List<EstimasiMaterialNf.ParamListMaterial> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "WITH q AS( "+
    "SELECT ID, ProjectID, ItemID, ItemTypeID, " +
    "(SELECT dbo.ItemCodeInv(ItemID, ItemTypeID))ItemCode, " +
    "(SELECT dbo.ItemNameInv(ItemID, ItemTypeID))ItemName, " +
    "(SELECT dbo.SatuanInv(ItemID, ItemTypeID))UomCode,  " +
    "Jumlah,Harga,(Jumlah * Harga)TotalHarga,Schedule " +
    "FROM MTC_ProjectMaterial WHERE RowStatus > -1 AND ProjectID = "+ Id +
"), " +
"w AS ( " +
    "SELECT *, 1 Urutan FROM q " +
    "UNION ALL " +
    "SELECT 999 ID, ProjectID, 0 ItemD, 1 ItemTypeID, ''ItemCode, 'TOTAL ' ItemName, ''UomCode, 0 Jumlah, 0 " +
    "Harga, Sum(TotalHarga), GetDate() Schedule, 2 Urutan " +
    "FROM q GROUP BY ProjectID " +
") " +
"SELECT* FROM w order by Urutan, ItemName";
                    AllData = connection.Query<EstimasiMaterialNf.ParamListMaterial>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<EstimasiMaterialNf.ParamInfoKodeProject> InfoKodeProject(int Id)
        {
            List<EstimasiMaterialNf.ParamInfoKodeProject> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT ProjectName FROM MTC_Project WHERE ID="+ Id + " AND RowStatus>-1";
                    AllData = connection.Query<EstimasiMaterialNf.ParamInfoKodeProject>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static int DeleteData(int Id)
        {
            int val = 0; ;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "UPDATE MTC_Project SET RowStatus=-1 WHERE ID="+ Id;
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

    }
}
