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
    public class FormPakaiSparePartFacade : AbstractTransactionFacade
    {
        public override int Delete(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Insert(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }

        public override int Update(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public static List<PakaiSparePartNf.ParamData> GetListData(string PakaiNo, string ItemName, string CreatedBy)
        {
            List<PakaiSparePartNf.ParamData> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                Users users = (Users)HttpContext.Current.Session["Users"];
                string Search = "";
                if (PakaiNo != "") { Search += "and p.PakaiNo='" + PakaiNo + "'"; }
                if (ItemName != "") { Search += "and i.ItemName like'%" + ItemName + "%'"; }
                if (CreatedBy != "") { Search += "and p.CreatedBy='" + CreatedBy + "'"; }
                string query;
                try
                {
                    query = "SELECT top 5000 d.ID,p.PakaiNo,p.CreatedTime,i.ItemCode,i.ItemName,d.Quantity, u.UOMDesc,d.Keterangan,p.Status FROM PakaiDetail d, Pakai p, Inventory i, UOM u WHERE d.PakaiID = p.ID AND d.ItemID = i.ID and d.UOMID=u.ID and d.RowStatus > -1 and i.rowStatus>-1 " + Search + " and p.CompanyID="+users.UnitKerjaID+" order BY d.ID desc";
                    AllData = connection.Query<PakaiSparePartNf.ParamData>(query).ToList();
                }
                catch (Exception)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PakaiSparePartNf.ParamDept> GetListDept()
        {
            List<PakaiSparePartNf.ParamDept> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    Users users = (Users)HttpContext.Current.Session["Users"];
                    int deptID = users.DeptID;
                    if (deptID == 10)
                    {
                        query = "select ID,DeptName from dept";
                    }
                    else
                    {
                        query = "select ID,DeptName from dept where id='" + deptID + "'";
                    }
                    AllData = connection.Query<PakaiSparePartNf.ParamDept>(query).ToList();
                }
                catch (Exception)
                {
                    AllData = null;
                }
            }
            return AllData;
        }
        public static List<PakaiSparePartNf.ParamDeptCode> GetGetDeptCode(int DllDept)
        {
            List<PakaiSparePartNf.ParamDeptCode> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "select DeptCode from dept where id='" + DllDept + "'";
                    AllData = connection.Query<PakaiSparePartNf.ParamDeptCode>(query).ToList();
                }
                catch (Exception)
                {
                    AllData = null;
                }
            }
            return AllData;
        }
        public static List<PakaiSparePartNf.ParamCekProject> GetListProject(string KodeProject)
        {
            List<PakaiSparePartNf.ParamCekProject> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                Users users = (Users)HttpContext.Current.Session["Users"];
                string query;
                try
                {
                    query = "Select ID ProjectID,ProjectName from MTC_Project where rowStatus>-1 and nomor='" + KodeProject + "' and CompanyID="+users.UnitKerjaID;
                    AllData = connection.Query<PakaiSparePartNf.ParamCekProject>(query).ToList();
                }
                catch (Exception)
                {
                    AllData = null;
                }
            }
            return AllData;
        }
        public static List<PakaiSparePartNf.ParamEstimasiMaterial> GetRetrieveEstimasiMaterialList(string KodeProject)
        {
            List<PakaiSparePartNf.ParamEstimasiMaterial> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                Users users = (Users)HttpContext.Current.Session["Users"];
                int CompanyID = users.UnitKerjaID;
                string query;
                try
                {
                    query = "SELECT ID,ItemName,Jumlah FROM (SELECT mpm.*, (SELECT dbo.ItemCodeInv(mpm.ItemID,mpm.ItemTypeID)) ItemCode,(SELECT dbo.ItemNameInv(mpm.ItemID,mpm.ItemTypeID)) ItemName, UomCode,mp.Nomor,mp.ProjectName,(mpm.Jumlah*mpm.Harga)TotalHarga FROM MTC_ProjectMaterial mpm LEFT JOIN Inventory inv on inv.ID=mpm.ItemID LEFT JOIN UOM u on u.ID=inv.UOMID LEFT JOIN MTC_Project mp on mp.ID=mpm.ProjectID WHERE mpm.RowStatus>-1 and inv.RowStatus>-1 and u.RowStatus>-1 and mp.RowStatus>-1 AND ProjectID IN(select ID from MTC_Project where nomor = '" + KodeProject + "') and inv.CompanyID ="+ CompanyID + " and mp.CompanyID ="+ CompanyID + " ) as x Order by ItemName,ItemCode";
                    AllData = connection.Query<PakaiSparePartNf.ParamEstimasiMaterial>(query).ToList();
                }
                catch (Exception)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PakaiSparePartNf.ParamMaterialBudget> GetMaterialBudgetPrj(int ddlItem, string KodeProject)
        {
            List<PakaiSparePartNf.ParamMaterialBudget> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT jumlah JumlahMaterial FROM MTC_ProjectMaterial where ItemID=" + ddlItem + " AND ProjectID in (select ID from MTC_Project where nomor='" + KodeProject + "')";
                    AllData = connection.Query<PakaiSparePartNf.ParamMaterialBudget>(query).ToList();
                }
                catch (Exception)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PakaiSparePartNf.ParamCekSPB> GetRetrieveOpenStatus()
        {
            List<PakaiSparePartNf.ParamCekSPB> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                Users users = (Users)HttpContext.Current.Session["Users"];
                int deptID = users.DeptID;
                string query;
                try
                {
                    query = "SELECT PakaiNo,Convert(Char,PakaiDate,112)PakaiDate,Status FROM Pakai where Status between 0 and 2 AND DATEDIFF(DAY,PakaiDate,GETDATE())>2 AND DeptID=" + deptID+" and CompanyID="+users.UnitKerjaID;
                    AllData = connection.Query<PakaiSparePartNf.ParamCekSPB>(query).ToList();
                }
                catch (Exception)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PakaiSparePartNf.ParamGroup> GetRetrieveSpGroup()
        {
            List<PakaiSparePartNf.ParamGroup> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = " WITH GroupSarmut AS  (select ROW_NUMBER() OVER(Partition By vw.ID Order By vw.ID, vw.SarmutID )N, vw.*,m.GroupNaME   FROM vw_SarmutGroup as vw  LEFT JOIN MaterialMTCGroup as m ON m.ID=vw.ID  WHERE m.RowStatus >-2  )   SELECT SarmutID AS ID,GroupName as ZonaName, ID as ZonaCode FROM GroupSarmut WHERE N=1 ";
                    AllData = connection.Query<PakaiSparePartNf.ParamGroup>(query).ToList();
                }
                catch (Exception)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PakaiSparePartNf.ParammBarang> GetListBarang(string TextBarang)
        {
            List<PakaiSparePartNf.ParammBarang> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                Users users = (Users)HttpContext.Current.Session["Users"];
                string query;
                try
                {
                    query = "Select ID,ItemCode,ItemName from Inventory where rowStatus>-1 and ItemName like '%" + TextBarang + "%' and CompanyID="+users.UnitKerjaID;
                    AllData = connection.Query<PakaiSparePartNf.ParammBarang>(query).ToList();
                }
                catch (Exception)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PakaiSparePartNf.ParamInventoryFacade1> GetRetrieveById(int ddlItem)
        {
            List<PakaiSparePartNf.ParamInventoryFacade1> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                Users users = (Users)HttpContext.Current.Session["Users"];
                string query;
                try
                {
                    query = "select A.ID,A.ItemCode,A.ItemName,A.SupplierCode,A.UOMID,C.UOMCode,C.UOMDesc,A.Jumlah,A.Harga,A.MinStock,A.MaxStock,A.DeptID,A.RakID,A.Gudang,A.ShortKey,A.Keterangan,A.Head,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.GroupID,A.ItemTypeID,aktif,A.Stock,A.LeadTime from Inventory as A,UOM as C where A.RowStatus > -1 and aktif = 1  and A.UOMID = C.ID and A.ID like '%" + ddlItem + "%'  and A.GroupID in (8,9) and CompanyID="+users.UnitKerjaID;
                    AllData = connection.Query<PakaiSparePartNf.ParamInventoryFacade1>(query).ToList();
                }
                catch (Exception)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PakaiSparePartNf.ParamMonthAvgPrice> GetGetPrice(int ddlItem, string Tanggal, int ItemTypeID)
        {
            List<PakaiSparePartNf.ParamMonthAvgPrice> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                Users users = (Users)HttpContext.Current.Session["Users"];
                int bln = int.Parse(DateTime.Parse(Tanggal).Month.ToString());
                int thn = int.Parse(DateTime.Parse(Tanggal).Year.ToString());
                string strAvgPrice = "janAvgPrice";
                if (bln == 2) { strAvgPrice = "febAvgPrice"; }
                if (bln == 3) { strAvgPrice = "marAvgPrice"; }
                if (bln == 4) { strAvgPrice = "aprAvgPrice"; }
                if (bln == 5) { strAvgPrice = "meiAvgPrice"; }
                if (bln == 6) { strAvgPrice = "junAvgPrice"; }
                if (bln == 7) { strAvgPrice = "julAvgPrice"; }
                if (bln == 8) { strAvgPrice = "aguAvgPrice"; }
                if (bln == 9) { strAvgPrice = "sepAvgPrice"; }
                if (bln == 10) { strAvgPrice = "oktAvgPrice"; }
                if (bln == 11) { strAvgPrice = "novAvgPrice"; }
                if (bln == 12) { strAvgPrice = "desAvgPrice"; }
                string query;
                try
                {
                    query = "select " + strAvgPrice + " as MonthAvgPrice from SaldoInventory where ItemId = " + ddlItem +
                          " and YearPeriod=" + thn + " and ItemTypeID=" + ItemTypeID;
                    AllData = connection.Query<PakaiSparePartNf.ParamMonthAvgPrice>(query).ToList();
                }
                catch (Exception)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PakaiSparePartNf.ParamStockOtherDept> GetRetrieveByStock(int DllDept, int ddlItem, int ItemTypeID)
        {
            List<PakaiSparePartNf.ParamStockOtherDept> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "Select isnull(SUM(QtyReceipt-QtyPakai),0) as StockOtherDept from sppmultigudang where aktif = 1 and gudangId <> " + DllDept + " and itemID=" + ddlItem + " and itemtypeid=" + ItemTypeID;
                    AllData = connection.Query<PakaiSparePartNf.ParamStockOtherDept>(query).ToList();
                }
                catch (Exception)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PakaiSparePartNf.ParamPendingSPB> GetGetPendingSPB(int ddlItem, int ItemTypeID)
        {
            List<PakaiSparePartNf.ParamPendingSPB> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT ISNULL(SUM(Quantity),0) as PendingSPB FROM PakaiDetail WHERE ItemID=" + ddlItem + " AND ItemTypeID=" + ItemTypeID + "AND PakaiID IN(SELECT ID FROM Pakai WHERE Status in (0,1,2)) AND RowStatus >-1";
                    AllData = connection.Query<PakaiSparePartNf.ParamPendingSPB>(query).ToList();
                }
                catch (Exception)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PakaiSparePartNf.ParamStockAkhir> GetGetStockAkhir(int ddlItem, string Tanggal, int ItemTypeID)
        {
            List<PakaiSparePartNf.ParamStockAkhir> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                int bln = int.Parse(DateTime.Parse(Tanggal).Month.ToString());
                int thn = int.Parse(DateTime.Parse(Tanggal).Year.ToString());
                string Field = "DesQty"; int Period = thn - 1;
                if (bln == 2) { Field = "JanQty"; Period = thn; }
                if (bln == 3) { Field = "FebQty"; Period = thn; }
                if (bln == 4) { Field = "MarQty"; Period = thn; }
                if (bln == 5) { Field = "AprQty"; Period = thn; }
                if (bln == 6) { Field = "MeiQty"; Period = thn; }
                if (bln == 7) { Field = "JunQty"; Period = thn; }
                if (bln == 8) { Field = "JulQty"; Period = thn; }
                if (bln == 9) { Field = "AguQty"; Period = thn; }
                if (bln == 10) { Field = "SepQty"; Period = thn; }
                if (bln == 11) { Field = "OktQty"; Period = thn; }
                if (bln == 12) { Field = "NovQty"; Period = thn; }
                string jmlTransit = "UNION ALL 	SELECT (-1 * JmlTransit) FROM Inventory WHERE JmlTransit>0 AND ID=" + ddlItem;
                if (ItemTypeID == 2) { jmlTransit = "UNION ALL 	SELECT (-1 * JmlTransit) FROM Asset WHERE JmlTransit>0 AND ID=" + ddlItem; }
                if (ItemTypeID == 3) { jmlTransit = "UNION ALL 	SELECT (-1 * JmlTransit) FROM Biaya WHERE JmlTransit>0 AND ID=" + ddlItem; }
                string query;
                try
                {
                    query = " WITH ItemSaldo AS ( SELECT SUM(ISNULL(Quantity,0)) Quantity FROM vw_StockPurchn WHERE ItemID=" + ddlItem + " AND ItemTypeID=" + ItemTypeID + " AND YM=" + thn + bln.ToString().PadLeft(2, '0') + "    GROUP BY ItemID" + " UNION ALL " + "SELECT ISNULL(" + Field + ",0) Quantity FROM SaldoInventory WHERE ItemID=" + ddlItem + " AND ItemTypeID=" + ItemTypeID + " AND YearPeriod=" + Period + ") SELECT ISNULL(SUM(Quantity),0) StockAkhir FROM ItemSaldo";
                    AllData = connection.Query<PakaiSparePartNf.ParamStockAkhir>(query).ToList();
                }
                catch (Exception)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PakaiSparePartNf.ParamPlanning> GetPlanningProd(int ddlItem)
        {
            List<PakaiSparePartNf.ParamPlanning> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                Users users = (Users)HttpContext.Current.Session["Users"];
                string query;
                try
                {
                    query = " SELECT Top 1 Planning,(DATENAME(MONTH,DateADD(Month,bulan,0)-1)+' '+ CAST(Tahun as CHAR(4)))Periode FROM MaterialPP where RowStatus >-1 and UnitKerjaID="+users.UnitKerjaID+"  Order By Tahun Desc,bulan desc, Revision Desc";
                    AllData = connection.Query<PakaiSparePartNf.ParamPlanning>(query).ToList();
                }
                catch (Exception)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PakaiSparePartNf.ParamCheckCost> GetMaterialBudgetBM(int ddlItem, int Planning)
        {
            List<PakaiSparePartNf.ParamCheckCost> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = " SELECT Top 1 BudgetQty as CheckCost FROM MaterialPPBudgetBM WHERE RowStatus>-1 AND ItemID=" + ddlItem + " AND RunningLine=" + Planning + " Order by ID desc";
                    AllData = connection.Query<PakaiSparePartNf.ParamCheckCost>(query).ToList();
                }
                catch (Exception)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PakaiSparePartNf.ParamMaxSPB> GetMaxQtySPB(int DllDept, int ddlItem, string Tanggal, int ItemTypeID)
        {
            List<PakaiSparePartNf.ParamMaxSPB> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                int Tahun = DateTime.Parse(Tanggal).Year;
                int Bulan = DateTime.Parse(Tanggal).Month;
                string Periode = Tahun.ToString();
                Periode += (Bulan <= 6) ? "01" : "07";
                string query;
                try
                {
                    query = "SELECT CASE WHEN RuleCalc=1 and ISNULL(MasaBerlaku,0) >= MONTH(GETDATE()) then(MaxQty+(ISNULL(AddQty,0)))ELSE (MaxQty)END MaxSPB " +
                            "FROM BudgetSP " +
                            "where RowStatus>-1 and (CAST(Tahun as CHAR(4))+RIGHT('0'+RTRIM(LTRIM(CAST(Bulan as CHAR))),2))='" + Periode + "'" +
                            "and DeptID=" + DllDept + " and ItemID=" + ddlItem + " and ItemTypeID=" + ItemTypeID;
                    AllData = connection.Query<PakaiSparePartNf.ParamMaxSPB>(query).ToList();
                }
                catch (Exception)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PakaiSparePartNf.ParamAddBudget> GetAddQtyBudget(int DllDept, int ddlItem, string Tanggal, int ItemTypeID)
        {
            List<PakaiSparePartNf.ParamAddBudget> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                int Tahun = DateTime.Parse(Tanggal).Year;
                int Bulan = DateTime.Parse(Tanggal).Month;
                string Periode = Tahun.ToString();
                Periode += (Bulan <= 6) ? "01" : "07";
                string query;
                try
                {
                    query = "SELECT CASE WHEN RuleCalc=1 and ISNULL(MasaBerlaku,0) >= MONTH(GETDATE()) then((ISNULL(AddQty,0)))ELSE 0 END AddBudget FROM BudgetSP " +
                            "where RowStatus>-1 and (CAST(Tahun as CHAR(4))+RIGHT('0'+RTRIM(LTRIM(CAST(Bulan as CHAR))),2))='" + Periode + "'" +
                            "and DeptID=" + DllDept + " and ItemID=" + ddlItem + " and ItemTypeID=" + ItemTypeID;
                    AllData = connection.Query<PakaiSparePartNf.ParamAddBudget>(query).ToList();
                }
                catch (Exception)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PakaiSparePartNf.ParamRuleBudget> GetRuleCalc(int DllDept, int ddlItem, string Tanggal, int ItemTypeID)
        {
            List<PakaiSparePartNf.ParamRuleBudget> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                int Tahun = DateTime.Parse(Tanggal).Year;
                int Bulan = DateTime.Parse(Tanggal).Month;
                string Periode = Tahun.ToString();
                Periode += (Bulan <= 6) ? "01" : "07";
                string query;
                try
                {
                    query = "SELECT RuleCalc FROM BudgetSP where RowStatus>-1 and (CAST(Tahun as CHAR(4))+RIGHT('0'+RTRIM(LTRIM(CAST(Bulan as CHAR))),2))='" + Periode + "'" +
                            "and DeptID=" + DllDept + " and ItemID=" + ddlItem + " and ItemTypeID=" + ItemTypeID; ;
                    AllData = connection.Query<PakaiSparePartNf.ParamRuleBudget>(query).ToList();
                }
                catch (Exception)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PakaiSparePartNf.ParamTotalSPB> GetTotalQtySPB(int DllDept, int ddlItem, string Tanggal, int ItemTypeID, int RuleBudget)
        {
            List<PakaiSparePartNf.ParamTotalSPB> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                Users users = (Users)HttpContext.Current.Session["Users"];
                string tahun = DateTime.Parse(Tanggal).ToString("yyyy");
                string bulan = DateTime.Parse(Tanggal).ToString("MM");
                string PeriodeRule = "'" + tahun + bulan + "' and '" + tahun + bulan + "'";
                if (bulan == "06")
                {
                    PeriodeRule = "'" + tahun + "01' and '" + tahun + "06'";
                    if (RuleBudget > 6) { PeriodeRule = "'" + tahun + "07' and '" + tahun + "12'"; }

                }
                if (bulan == "12") { PeriodeRule = "'" + tahun + "01' and '" + tahun + "12'"; }
                string query;
                try
                {
                    query = "select ISNULL(SUM(Quantity),0) as TotalSPB from PakaiDetail where PakaiID in(Select ID from Pakai where RowStatus>-1 and LEFT(CONVERT(CHAR,PakaiDate,112),6 and CompanyID="+users.UnitKerjaID+") between " + PeriodeRule + " and DeptID=" + DllDept + ") and ItemID=" + ddlItem + " and ItemTypeID = " + ItemTypeID + " and RowStatus>-1 ";
                    AllData = connection.Query<PakaiSparePartNf.ParamTotalSPB>(query).ToList();
                }
                catch (Exception)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PakaiSparePartNf.ParamTotalSPBPrj> GetTotalQtySPBPrj(string KodeProject, int ddlItem, int ItemTypeID)
        {
            List<PakaiSparePartNf.ParamTotalSPBPrj> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                Users users = (Users)HttpContext.Current.Session["Users"];
                string query;
                try
                {
                    query = "select ISNULL(SUM(Qty),0) as Jumlah TotalSPBPrj from MTC_Project_Pakai where ItemID=" + ddlItem +
                " and ItemTypeID=" + ItemTypeID + " and RowStatus>-1 and ProjectID IN (select ID from MTC_Project where nomor='" + KodeProject + "' and CompanyID="+users.UnitKerjaID+")";
                    AllData = connection.Query<PakaiSparePartNf.ParamTotalSPBPrj>(query).ToList();
                }
                catch (Exception)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PakaiSparePartNf.ParamItemIDKhusus> GetIsMaterialBudgetKhusus(int DllDept, int ddlItem)
        {
            List<PakaiSparePartNf.ParamItemIDKhusus> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT ItemID as ItemIDKhusus FROM MaterialPPKhusus WHERE RowStatus>-1 AND ItemID=" + ddlItem + " AND DeptID=" + DllDept + " ORDER BY ID DESC";
                    AllData = connection.Query<PakaiSparePartNf.ParamItemIDKhusus>(query).ToList();
                }
                catch (Exception)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PakaiSparePartNf.ParamForklift> GetListForklift()
        {
            List<PakaiSparePartNf.ParamForklift> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                Users users = (Users)HttpContext.Current.Session["Users"];
                string query;
                try
                {
                    query = " Select ID, Forklift from masterforklift where RowStatus>-1 and CompanyID="+users.UnitKerjaID+" order by Forklift";
                    AllData = connection.Query<PakaiSparePartNf.ParamForklift>(query).ToList();
                }
                catch (Exception)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PakaiSparePartNf.ParamStatus> GetRetrieveByStatus(string Tanggal)
        {
            List<PakaiSparePartNf.ParamStatus> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                int bln = int.Parse(DateTime.Parse(Tanggal).Month.ToString());
                int thn = int.Parse(DateTime.Parse(Tanggal).Year.ToString());
                string query;
                try
                {
                    query = " Select top 1 Status from AccClosingPeriode where Bulan=" + bln + " and Tahun=" + thn + " and Modul='Purchn' order by ID Desc";
                    AllData = connection.Query<PakaiSparePartNf.ParamStatus>(query).ToList();
                }
                catch (Exception)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        
    }//_________________________
}
