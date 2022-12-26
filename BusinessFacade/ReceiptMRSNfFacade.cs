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
    public class ReceiptMRSNfFacade : AbstractTransactionFacade
    {
        private ReceiptMRSNf.ParamHead obj = new ReceiptMRSNf.ParamHead();
        private List<SqlParameter> sqlListParam;
        public ReceiptMRSNfFacade(object objDomain)
            : base(objDomain)
        {
            obj = (ReceiptMRSNf.ParamHead)objDomain;
        }
        public override int Delete(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ReceiptNo", obj.ReceiptNo));
                sqlListParam.Add(new SqlParameter("@ReceiptDate", obj.ReceiptDate));
                sqlListParam.Add(new SqlParameter("@SupplierID", obj.SupplierID));
                sqlListParam.Add(new SqlParameter("@DepoID", obj.DepoID));
                sqlListParam.Add(new SqlParameter("@Status", obj.Status));
                sqlListParam.Add(new SqlParameter("@POID", obj.PoID));
                sqlListParam.Add(new SqlParameter("@PONo", obj.PoNo));
                sqlListParam.Add(new SqlParameter("@CreatedBy", obj.CreatedBy));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", obj.ItemTypeID));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertReceipt");
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
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", obj.ID));
                sqlListParam.Add(new SqlParameter("@ReceiptNo", obj.ReceiptNo));
                sqlListParam.Add(new SqlParameter("@ReceiptDate", obj.ReceiptDate));
                sqlListParam.Add(new SqlParameter("@SupplierID", obj.SupplierID));
                sqlListParam.Add(new SqlParameter("@SupplierType", obj.SupplierType));
                sqlListParam.Add(new SqlParameter("@ReceiptType", obj.ReceiptType));
                sqlListParam.Add(new SqlParameter("@DepoID", obj.DepoID));
                sqlListParam.Add(new SqlParameter("@Status", obj.Status));
                sqlListParam.Add(new SqlParameter("@POID", obj.PoID));
                sqlListParam.Add(new SqlParameter("@PONo", obj.PoNo));
                sqlListParam.Add(new SqlParameter("@AlasanCancel", obj.AlasanCancel));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", obj.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@TTagihanDate", obj.TTagihanDate));
                sqlListParam.Add(new SqlParameter("@JTempoDate", obj.JTempoDate));
                sqlListParam.Add(new SqlParameter("@fakturpajak", obj.FakturPajak));
                sqlListParam.Add(new SqlParameter("@keteranganpay", obj.Keteranganpay));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", obj.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@FakturPajakDate", obj.FakturPajakDate));
                sqlListParam.Add(new SqlParameter("@InvoiceNo", obj.InvoiceNo));
                sqlListParam.Add(new SqlParameter("@kurspajak", obj.KursPajak));
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateReceipt");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public static List<ReceiptMRSNf.ParamHeadData> HeadData(int id)
        {
            List<ReceiptMRSNf.ParamHeadData> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT ReceiptNo KodeReceipt, ReceiptDate Tanggal FROM Receipt WHERE ID=" + id;
                    AllData = connection.Query<ReceiptMRSNf.ParamHeadData>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<ReceiptMRSNf.ParamDtlData> DtlData(int id)
        {
            List<ReceiptMRSNf.ParamDtlData> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "select B.ID, B.Keterangan, " +
"A.PONo KodePO, B.SPPNo KodeSpp, FORMAT(B.Quantity, 'N0') Quantity, " +
"(select ItemCode from Inventory where ID = B.ItemID) ItemCode, " +
"(select ItemName from Inventory where ID = B.ItemID) ItemName, " +
"(select UOMDesc from uom where ID = B.UomID) Satuan " +
"from Receipt as A, ReceiptDetail as B,POPurchnDetail as E " +
"where B.PODetailID = E.ID and A.ID = B.ReceiptID " +
"and A.Status > -1 and B.RowStatus > -1 and A.ReceiptType = 6 and A.id="+id+" order by A.id desc";
                    AllData = connection.Query<ReceiptMRSNf.ParamDtlData>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<ReceiptMRSNf.ParamData> ListData(string KodeReceipt, string KodePo, string KodeSpp)
        {
            List<ReceiptMRSNf.ParamData> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string Search = "";
                if (KodeReceipt != "") { Search += " AND A.ReceiptNo like'%" + KodeReceipt + "%' "; }
                if (KodePo != "") { Search += " AND A.PONo like'%" + KodePo + "%' "; }
                if (KodeSpp != "") { Search += " AND B.SPPNo like'%" + KodeSpp + "%' "; }
                string query;
                try
                {
                    query = "select top 300 A.ID,A.ReceiptNo KodeReceipt,A.ReceiptDate TanggalReceipt, A.Status, " +
"A.PONo KodePO, B.SPPNo KodeSpp, FORMAT(B.Quantity, 'N0') Quantity, " +
"(select ItemCode from Inventory where ID = B.ItemID) ItemCode, " +
"(select ItemName from Inventory where ID = B.ItemID) ItemName, " +
"(select UOMDesc from uom where ID = B.UomID) Satuan " +
"from Receipt as A, ReceiptDetail as B,POPurchnDetail as E " +
"where B.PODetailID = E.ID and A.ID = B.ReceiptID " +
"and A.Status > -1 and B.RowStatus > -1 and A.ReceiptType = 6 "+ Search + " order by A.id desc";
                    AllData = connection.Query<ReceiptMRSNf.ParamData>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<ReceiptMRSNf.ParamPo> GetListPo(string Type)
        {
            List<ReceiptMRSNf.ParamPo> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string where = " AND d.GroupID IN (8,9) ";
                if (Type == "2") { where = " AND d.GroupID IN (6) "; }
                if (Type == "3") { where = " AND d.GroupID IN (13) "; }
                string query;
                try
                {
                    query = "WITH q AS ( "+
    "SELECT d.id, d.Qty, d.POID,p.NoPO FROM POPurchnDetail d, POPurchn p " +
     "WHERE d.POID = p.ID and d.Status > -1 AND p.Status > -1 " +
    "AND p.Approval > 1 " + where +
"), " +
"w AS ( " +
    "SELECT q.id, sum(r.quantity) totqty FROM q, ReceiptDetail r " +
    "WHERE q.id = r.PODetailID AND r.RowStatus > -1 GROUP BY q.id " +
"), " +
"r AS ( " +
    "SELECT id FROM q WHERE id NOT IN(SELECT PODetailID FROM ReceiptDetail) " +
"), " +
"t AS ( " +
    "SELECT q.id FROM q, w WHERE q.id = w.id AND q.qty > w.totqty UNION ALL " +
    "SELECT * FROM r " +
") " +
"SELECT DISTINCT d.POID,p.NoPO FROM t, POPurchnDetail d, POPurchn p WHERE t.id = d.id AND d.POID = p.ID";
                    AllData = connection.Query<ReceiptMRSNf.ParamPo>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<ReceiptMRSNf.ParamItem> GetListItem(int Id)
        {
            List<ReceiptMRSNf.ParamItem> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "select A.ID, "+
"( " +
    "case A.ItemTypeID  " +
    "when 1 then(select ItemName from Inventory where ID = A.ItemID and Inventory.RowStatus > -1)  " +
    "when 2 then(select ItemName from asset where ID = A.ItemID and Asset.RowStatus > -1)  " +
    "when 3 then  " +
        "CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus = 1), GETDATE()) < " +
        "(Select CreatedTime from SPP where SPP.ID = A.SPPID))  " +
        "THEN(select ItemName from biaya where ID = A.ItemID and biaya.RowStatus > -1) + ' - ' + " +
        "(Select SPPDetail.Keterangan From SPPDetail where SPPDetail.ID = A.SPPDetailID)  " +
        "ELSE  " +
        "(select ItemName from biaya where ID = A.ItemID and biaya.RowStatus > -1)  " +
        "END  " +
    "else '' " +
    "end " +
") ItemName " +
"from POPurchnDetail as A where A.Status >= 0 and A.POID ="+Id;
                    AllData = connection.Query<ReceiptMRSNf.ParamItem>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<ReceiptMRSNf.ParamInfoItem> GetInfoItem(int Id)
        {
            List<ReceiptMRSNf.ParamInfoItem> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT "+
"case A.ItemTypeID " +
"when 1 then(select ItemCode from Inventory where ID = A.ItemID and Inventory.RowStatus > -1) " +
"when 2 then(select ItemCode from asset where ID = A.ItemID and Asset.RowStatus > -1) " +
"when 3 then(select ItemCode from biaya where ID = A.ItemID and biaya.RowStatus > -1) " +
"else '' end ItemCode, " +
"case A.ItemTypeID " +
"when 1 then(select isnull(sum(Jumlah), 0) from Inventory where ID = A.ItemID and Inventory.RowStatus > -1) " +
"when 2 then(select isnull(sum(Jumlah), 0) from asset where ID = A.ItemID and Asset.RowStatus > -1) " +
"when 3 then " +
    "CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus = 1), GETDATE()) < " +
    "(Select CreatedTime from SPP where SPP.ID = A.SPPID)) " +
    "THEN(SELECT isnull(sum(jumlah), 0) from Biaya where ItemName = (Select SPPDetail.Keterangan From SPPDetail  where " + "SPPDetail.ID = A.SPPDetailID))  " +
    "ELSE(select isnull(sum(Jumlah), 0) from Biaya where ID = A.ItemID and Biaya.RowStatus > -1) " +
    "END " +
"else '' end stock, " +
"B.UOMCode Satuan, (select sum(Quantity) from ReceiptDetail where PODetailID=a.id and RowStatus>-1) QtyPo, "+
"A.DocumentNo NoSpp, s.SupplierName Suplier, " +
  "(select satuanid from spp where id = a.sppid) Status " +
    "from POPurchnDetail as A, POPurchn p, SuppPurch s, UOM as B " +
"where A.UOMID = B.ID and a.POID = p.ID and p.SupplierID = s.ID " +
"and a.Status > -1 and p.Status > -1 and b.RowStatus > -1 " +
"and A.ID ="+Id;
                    AllData = connection.Query<ReceiptMRSNf.ParamInfoItem>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }
        public static DateTime CekDate(int Id)
        {
            DateTime CreatedTime = DateTime.Now;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "Select POPurchnDate from POPurchn where status>-1" ;
                    CreatedTime = connection.QueryFirstOrDefault<DateTime>(query);
                }
                catch (Exception e)
                {

                }
            }
            return CreatedTime;
        }
        public static DateTime CekLastDate(string Code)
        {
            DateTime CreatedTime = DateTime.Now;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "Select isnull(max(ReceiptDate),GETDATE()) as ReceiptDate from Receipt where Status>-1 and left(ReceiptNo,4)='"+Code+"'";
                    CreatedTime = connection.QueryFirstOrDefault<DateTime>(query);
                }
                catch (Exception e)
                {

                }
            }
            return CreatedTime;
        }

        public static int CekCountClose(int bln, int thn)
        {
            int CreatedTime = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "SELECT count(tahun) t from AccClosingPeriode where Bulan=" + bln + " and Tahun=" + thn + " and Modul='Purchn'";
                    CreatedTime = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return CreatedTime;
        }

        public static ReceiptMRSNf.ParamCekClosing RetrieveByStatus(int bln, int thn)
        {
            ReceiptMRSNf.ParamCekClosing AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT Tahun, Bulan, Status from AccClosingPeriode where Bulan="+bln+" and Tahun="+thn+" and Modul='Purchn' order by ID Desc";
                    AllData = connection.QueryFirstOrDefault<ReceiptMRSNf.ParamCekClosing>(query);
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<ReceiptMRSNf.ParamInfoDetailPo> InfoDetailPo(int Id)
        {
            List<ReceiptMRSNf.ParamInfoDetailPo> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT Price, ItemID, SppID, DocumentNo, PoID, UomID, GroupID, ItemTypeID  FROM POPurchnDetail WHERE ID="+Id+" AND Status>-1";
                    AllData = connection.Query<ReceiptMRSNf.ParamInfoDetailPo>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static string GetKompany(int depoid)
        {
            string CreatedTime = "";
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select A.KodeLokasi as kodeLokasi from Company as A, Depo as B where B.ID = A.DepoID and B.ID = " + depoid;
                    CreatedTime = connection.QueryFirstOrDefault<string>(query);
                }
                catch (Exception e)
                {

                }
            }
            return CreatedTime;
        }

        public static int CekDocNo(int bln, int thn, string kd)
        {
            int val = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "SELECT count(ID) jumID from ReceiptDocNo where ReceiptCode='" + kd + "' and MonthPeriod=" + bln + " and YearPeriod=" + thn;
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static ReceiptMRSNf.ParamDocNo GetDocNo(int bln, int thn, string kd)
        {
            ReceiptMRSNf.ParamDocNo AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT ID, ReceiptCode, MonthPeriod, YearPeriod, NoUrut from ReceiptDocNo where ReceiptCode='" + kd + "' and MonthPeriod=" + bln + " and YearPeriod=" + thn;
                    AllData = connection.QueryFirstOrDefault<ReceiptMRSNf.ParamDocNo>(query);
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static int SupPo(int Id)
        {
            int val = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "SELECT SupplierID FROM POPurchn WHERE Status>-1 AND ID=" + Id;
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static ReceiptMRSNf.ParamSisaPo GetSisaPo(int id)
        {
            ReceiptMRSNf.ParamSisaPo AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "select ID,isnull(sum(QtyPO),0) as QtyPO,isnull(sum(QtyReceipt),0) as QtyReceipt from(select A.ID,B.Qty as QtyPO," +
                           "case when A.ID>0 then (select isnull(sum(C.Quantity),0) from ReceiptDetail as C where C.PODetailID=B.ID and C.RowStatus>-1 and C.ItemID=B.ItemID) else 0 end QtyReceipt " +
                           "from POPurchn as A,POPurchnDetail as B where A.ID=B.POID and A.Status>-1 and B.Status>-1 and A.ID='" + id + "') as X group by ID";
                    AllData = connection.QueryFirstOrDefault<ReceiptMRSNf.ParamSisaPo>(query);
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static ReceiptMRSNf.ParamSppIdDtl GetSppIdDtl(int id)
        {
            ReceiptMRSNf.ParamSppIdDtl AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "select ID,SatuanID,UserID from SPP where status>-1 and ID = " + id;
                    AllData = connection.QueryFirstOrDefault<ReceiptMRSNf.ParamSppIdDtl>(query);
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }
        public static int GetDept(int Id)
        {
            int val = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "SELECT DeptId FROM Users WHERE RowStatus>-1 AND ID=" + Id;
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static int CekSaldoInvetory(int Id, int thn, int type)
        {
            int val = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select count(ItemId) itemid from SaldoInventory where ItemId=" + Id + " and YearPeriod=" + thn + " and ItemTypeID=" + type;
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static int LastPrice(int Id, int type)
        {
            int val = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "SELECT top 1 price from popurchndetail where status>-1 and itemID =" + Id + " and itemtypeid=" + type + " order by id desc";
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static int GetStock(int Id, int type)
        {
            int val = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "SELECT jumlah as stock from Inventory where rowstatus> -1 and ID = " + Id;
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static string UpdateStatusPo(int Status, int PoID)
        {
            string val = "";
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "update POPurchn set Status=" + Status + " where ID = " + PoID;
                    val = connection.QueryFirstOrDefault<string>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }
        public static string UpdateStatusPoDtl(int id)
        {
            string val = "";
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "update POPurchnDetail set Status=4 where ID = " + id;
                    val = connection.QueryFirstOrDefault<string>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static string UpdateJumInventory(decimal qty, int id)
        {
            string val = "";
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "update Inventory set Jumlah = Jumlah + " + qty + " where ID =" + id;
                    val = connection.QueryFirstOrDefault<string>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static string UpdateTimbangRcpDtl(int qty, int id)
        {
            string val = "";
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "Update ReceiptDetail set toAsset=" + qty + " where ID=" + id;
                    val = connection.QueryFirstOrDefault<string>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }
    }
}
