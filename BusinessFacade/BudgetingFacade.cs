using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer;
using Domain;
namespace BusinessFacade
{
    public class BudgetingFacade : AbstractFacade
    {
        public BudgetingFacade()
            : base()
        {

        }
        private ArrayList arrData = new ArrayList();
        private List<SqlParameter> sqlListParam;
        private Budget budget = new Budget();
        public string Criteria { get; set; }
        public string Criteria2 { get; set; }
        public string Pilihan { get; set; }
        public string Field { get; set; }
        public string Prefix { get; set; }
        public bool HeaderProces { get; set; }
        public string LinkFrom { get; set; }
        public override int Insert(object objDomain)
        {
            try
            {
                int result = 0;
                budget = (Budget)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@BudgetNo", budget.BudgetNo));
                sqlListParam.Add(new SqlParameter("@Tahun", budget.Tahun));
                sqlListParam.Add(new SqlParameter("@Bulan", budget.Bulan));
                sqlListParam.Add(new SqlParameter("@CreatedBy", budget.CreatedBy));
                sqlListParam.Add(new SqlParameter("@HeadID", budget.HeadID));
                sqlListParam.Add(new SqlParameter("@DeptID", budget.DeptID));
                sqlListParam.Add(new SqlParameter("@UserID", budget.UserID));
                result = dataAccess.ProcessData(sqlListParam, "spBudgetATK_Insert");
                return result;
            }
            catch
            {
                return -1;
            }
        }
        public int insert_detail(object objDomain)
        {
            try
            {
                int result = 0;
                budget = (Budget)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@BudgetID",budget.BudgetID));
                sqlListParam.Add(new SqlParameter("@ItemID", budget.ItemID));
                sqlListParam.Add(new SqlParameter("@UomCode", budget.UomCode));
                sqlListParam.Add(new SqlParameter("@Quantity", budget.Quantity));
                sqlListParam.Add(new SqlParameter("@AppvQty", budget.AppvQty));
                sqlListParam.Add(new SqlParameter("@CreatedBy", budget.CreatedBy));
                sqlListParam.Add(new SqlParameter("@Keterangan", budget.Keterangan));
                result = dataAccess.ProcessData(sqlListParam, "spBudgetATKDetail_Insert");
                return result;
            }
            catch
            {
                return -1;
            }

        }
        public override int Update(object objDomain)
        {
            int result = 0;
            try
            {
                budget = (Budget)objDomain;
                sqlListParam = new List<SqlParameter>();
                if (this.HeaderProces == false)
                {
                    sqlListParam.Add(new SqlParameter("@ID", budget.ID));
                    sqlListParam.Add(new SqlParameter("@Qty", budget.RowStatus));
                    sqlListParam.Add(new SqlParameter("@Keterangan", budget.Keterangan));
                    result = dataAccess.ProcessData(sqlListParam, "spBudgetATKDetail_Update");
                }

            }
            catch
            {
                result = -1;
            }
            return result;
        }
        public int UpdateStatus(object objDomain)
        {
            int result = 0;
            budget = (Budget)objDomain;
            sqlListParam = new List<SqlParameter>();
            sqlListParam.Add(new SqlParameter("@ItemID", budget.ItemID));
            sqlListParam.Add(new SqlParameter("@Tahun", budget.Tahun));
            sqlListParam.Add(new SqlParameter("@Bulan", budget.Bulan));
            sqlListParam.Add(new SqlParameter("@SPPID", budget.BudgetID));
            sqlListParam.Add(new SqlParameter("@Approve", budget.Approval));
            result = dataAccess.ProcessData(sqlListParam, "spBudgetATKDetail_UpdStatus");
            return result;

        }
        public override int Delete(object objDomain)
        {
            try
            {
                int result = 0;
                budget = (Budget)objDomain;
                sqlListParam = new List<SqlParameter>();
                if (this.HeaderProces == false)
                {
                    sqlListParam.Add(new SqlParameter("@ID", budget.ID));
                    sqlListParam.Add(new SqlParameter("@RowStatus", budget.RowStatus));
                    sqlListParam.Add(new SqlParameter("@Keterangan", budget.Keterangan));
                    result = dataAccess.ProcessData(sqlListParam, "spBudgetATKDetail_delete");
                }
                else if (this.HeaderProces == true)
                {
                    sqlListParam.Add(new SqlParameter("@ID", budget.ID));
                    sqlListParam.Add(new SqlParameter("@RowStatus", budget.RowStatus));
                    sqlListParam.Add(new SqlParameter("@Keterangan", budget.Keterangan));
                    sqlListParam.Add(new SqlParameter("@lastModifiedby", budget.LastModifiedBy));
                    result = dataAccess.ProcessData(sqlListParam, "spBudgetATK_Delete");
                }
                return result;
            }
            catch
            {
                return -1;
            }
        }
        public int InsertMaster(object objDomain)
        {
            try
            {
                int result = 0;
                budget = (Budget)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", budget.ItemID));
                sqlListParam.Add(new SqlParameter("@DeptID", budget.DeptID));
                sqlListParam.Add(new SqlParameter("@Tahun", budget.Tahun));
                sqlListParam.Add(new SqlParameter("@Quantity", budget.Quantity));
                result = dataAccess.ProcessData(sqlListParam, "spBudgetATKMaster_insert");
                return result;
            }catch
            {
                return -1;
            }
        }
        public int UpdateMaster(object objDomain)
        {
            try
            {
                int result = 0;
                budget = (Budget)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", budget.ID));
                sqlListParam.Add(new SqlParameter("@RowStatus", budget.Tahun));
                sqlListParam.Add(new SqlParameter("@Quantity", budget.Quantity));
                result = dataAccess.ProcessData(sqlListParam, "spBudgetATKMaster_Update");
                return result;
            }
            catch
            {
                return -1;
            }
        }
        public override ArrayList Retrieve()
        {
            arrData = new ArrayList();
            string strSQL = this.Query();
            DataAccess da = new DataAccess(Global.ConnectionString());

            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateObject(sdr));
                }
            }
            return arrData;
        }
        public int Approval(object objDomain)
        {
            int result = 0;
            try
            {
                budget = (Budget)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@DetailID", budget.ID));
                sqlListParam.Add(new SqlParameter("@ID", budget.BudgetID));
                sqlListParam.Add(new SqlParameter("@Approval", budget.Approval));
                sqlListParam.Add(new SqlParameter("@ApprovedBy", budget.ApprovalBy));
                sqlListParam.Add(new SqlParameter("@ApvQty", budget.AppvQty));
                result = dataAccess.ProcessData(sqlListParam, "spBudgetATK_Aproval");
                return result;
            }
            catch
            {
                return -1;
            }
        }
        public Budget Retrieve(bool detail)
        {
            budget = new Budget();
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(this.Query());
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    return (GenerateObject(sdr));
                }
            }
            return budget;
        }
        private string Query()
        {
            string query = string.Empty;
            switch (this.Pilihan)
            {
                case "Tahun":
                    query = "Select Distinct" + this.Prefix + "  (year(PakaiDate))Tahun from Pakai " + this.Criteria + " Order by year(PakaiDate) desc";
                    break;
                case "Material":
                    query = "Select ID,ItemCode,ItemName,UOMID,(select UomCode from UOM where ID=UOMID)Unit From " +
                          "inventory where aktif=1" + this.Criteria;
                    break;
                case "History":
                    #region History
                    query = this.Prefix + "SELECT Tahun,ItemID,SUM(isnull([1],0))Jan,SUM(isnull([2],0))Feb,SUM(isnull([3],0))Mar,SUM(isnull([4],0))Apr,SUM(isnull([5],0))Mei, " +
                            "SUM(isnull([6],0))Jun,SUM(isnull([7],0))Jul,SUM(isnull([8],0))Ags,SUM(isnull([9],0))Sep,SUM(isnull([10],0))Okt, " +
                            "SUM(isnull([11],0))Nov,SUM(isnull([12],0))Dese FROM( " +
                            "SELECT pk.ID AS HeaderID,pk.PakaiDate,pk.DeptID,pd.*,(MONTH(pk.PakaiDate))Bln,(YEAR(pk.PakaiDate))Tahun FROM pakaidetail AS pd " +
                            "LEFT JOIN Pakai AS pk ON pk.ID=pd.PakaiID " +
                            "WHERE  pd.RowStatus>-1 " + this.Criteria +
                            ") AS x  " +
                            "PIVOT (sum(Quantity) FOR Bln in([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12])) AS pvt " +
                            "GROUP BY Tahun,ItemID";
                    #endregion
                    break;
                case "HistoryBudget":
                    #region History
                    query = "SELECT Tahun,ItemID,SUM(isnull([1],0))Jan,SUM(isnull([2],0))Feb,SUM(isnull([3],0))Mar,SUM(isnull([4],0))Apr,SUM(isnull([5],0))Mei," +
                            "SUM(isnull([6],0))Jun,SUM(isnull([7],0))Jul,SUM(isnull([8],0))Ags,SUM(isnull([9],0))Sep,SUM(isnull([10],0))Okt, SUM(isnull([11],0))Nov," +
                            "SUM(isnull([12],0))Dese FROM( " +
                            "SELECT Tahun,Bulan,bd.ItemID, bd.QtyApv from BudgetATK AS b " +
                            "LEFT JOIN BudgetATKDetail AS bd " +
                            "ON bd.BudgetID=b.ID " +
                            "WHERE bd.RowStatus>-1 " + this.Criteria +
                            ") AS x " +
                            "PIVOT(sum(QtyApv) for Bulan in([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12])) AS pvt " +
                            "GROUP BY Tahun,ItemID";
                    #endregion
                    break;
                case "HistMonth":
                    #region Histori bulanan
                    query = "SELECT " + this.Field + " FROM (SELECT Tahun,ItemID,SUM(isnull([1],0))Jan,SUM(isnull([2],0))Feb,SUM(isnull([3],0))Mar,SUM(isnull([4],0))Apr,SUM(isnull([5],0))Mei, " +
                            "SUM(isnull([6],0))Jun,SUM(isnull([7],0))Jul,SUM(isnull([8],0))Ags,SUM(isnull([9],0))Sep,SUM(isnull([10],0))Okt, " +
                            "SUM(isnull([11],0))Nov,SUM(isnull([12],0))Dese FROM( " +
                            "SELECT pk.ID AS HeaderID,pk.PakaiDate,pk.DeptID,pd.*,(MONTH(pk.PakaiDate))Bln,(YEAR(pk.PakaiDate))Tahun FROM pakaidetail AS pd " +
                            "LEFT JOIN Pakai AS pk ON pk.ID=pd.PakaiID " +
                            "WHERE  pd.RowStatus>-1 " + this.Criteria +
                            ") AS x  " +
                            "PIVOT (sum(Quantity) FOR Bln in([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12])) AS pvt " +
                            "GROUP BY Tahun,ItemID ) as xx";
                    break;
                    #endregion
                case "HeadDept":
                    query = "SELECT Top 1 HeadID from ListUserHead where RowStatus >-1 "+this.Criteria;
                    break;
                case "Nomor":
                    query = "SELECT COUNT(ID) Jml from BudgetATK " + this.Criteria;
                    break;
                case "DetailBudget":
                case "BulanBudget":

                case "DeptBudget":
                    #region detailBudget
                    query = this.Field + " SELECT bg.*,bad.ID as DetailID,ItemID,Unit,Qty,QtyApv,bad.Keterangan," +
                            "(select dbo.ItemCodeInv(ItemID,1))ItemCode,(select dbo.ItemNameInv(itemID,1))ItemName, " +
                            "bad.SPPID,(Select NoSPP from SPP where ID=bad.SPPID)NoSPP,isnull(bad.PakaiDetailID,0)PakaiDetailID, " +
                            "(select UomID from Inventory where ID=bad.ItemID)UomID,bad.Approval as Aprov, " +
                            "(select Head from Inventory where ID=ItemID)Grp " +
                            ",CAST(CAST(Tahun as CHAR(4))+''+ CASE WHEN Bulan <10 Then '0'+CAST(Bulan as CHAR(2))ELSE CAST(Bulan as CHAR(2)) END as INT)Thn " +
                            "FROM BudgetATK bg " +
                            "LEFT JOIN BudgetATKDetail AS bad ON bad.BudgetID=bg.ID " +
                            "where bg.RowStatus >-1 and bad.RowStatus>-1 " +
                            this.Criteria;
                    #endregion
                    break;
                case "DetailBudget2":
                case "BulanBudget2":

                case "DeptBudget2":
                    #region detailBudget
                    query = this.Field + " select *,case when qty=pakai then 'yes' else 'no' end as matikan from ( " +
                            " SELECT bg.*,bad.ID as DetailID,ItemID,Unit,Qty," +
                            "(select top 1 B.quantity from pakai A inner join pakaidetail B on A.ID=B.pakaiid " +
                            "where B.itemid=bad.itemid  and A.[status]>-1 and B.rowstatus>-1 " + this.Criteria2 + ") pakai, " +
                            "QtyApv,bad.Keterangan,(select dbo.ItemCodeInv(ItemID,1))ItemCode,(select dbo.ItemNameInv(itemID,1))ItemName, " +
                            "bad.SPPID,(Select NoSPP from SPP where ID=bad.SPPID)NoSPP,isnull(bad.PakaiDetailID,0)PakaiDetailID, " +
                            "(select UomID from Inventory where ID=bad.ItemID)UomID,bad.Approval as Aprov, " +
                            "(select Head from Inventory where ID=ItemID)Grp " +
                            ",CAST(CAST(Tahun as CHAR(4))+''+ CASE WHEN Bulan <10 Then '0'+CAST(Bulan as CHAR(2))ELSE CAST(Bulan as CHAR(2)) END as INT)Thn " +
                            "FROM BudgetATK bg " +
                            "LEFT JOIN BudgetATKDetail AS bad ON bad.BudgetID=bg.ID " +
                            "where bg.RowStatus >-1 and bad.RowStatus>-1 " + this.Criteria + " )S ";
                    #endregion
                    break;
                case "ToSPP":
                    query = "select distinct ItemID,(select dbo.ItemCodeInv(itemid,1))ItemCode,(select dbo.ItemNameInv(ItemID,1))Itemname," +
                          " Unit,(select Top 1 ID from UOM where UOMCode=Unit)UomID,SUM(QtyApv)Qty, "+
                          "(Select Jumlah from inventory where ID=ItemID)Jumlah "+
                          " from BudgetATKDetail where RowStatus>-1 and BudgetID in(select ID from BudgetATK where  " +
                          " approval>0 and RowStatus>-1 " + this.Criteria + ")" + this.Field + " group by ItemID,Unit " +
                          this.Prefix;
                    break;
                case "ListBudget":
                    query = "select " + this.Prefix + " " + this.Field + " from BudgetATK where RowStatus>-1 " + this.Criteria;
                    break;
                case "UpdateStatus":
                    query = "Update BudgetATKDetail " + this.Field + " where ID=" + this.Criteria;
                    break;
                case "MasterBudget":
                    query = "Select * from BudgetATKMaster where RowStatus>-1 " + this.Criteria;
                    break;
                case "MasterBudgetSP":
                    query = "Select * from BudgetSP Where RowStatus>-1 " + this.Criteria;
                    break;
                case "MasterBudgetTotal":
                    query = "Select ISNULL(SUM(Quantity),0)Quantity from BudgetATKMaster where RowStatus>-1 " + this.Criteria;
                    break;
            }
            return query;
        }
        private Budget GenerateObject(SqlDataReader sdr)
        {
            budget = new Budget();
            switch (this.Pilihan)
            {
                case "Tahun":
                    budget.Tahun = Convert.ToInt32(sdr["Tahun"].ToString());
                    break;
                case "Material":
                    budget.ID = Convert.ToInt32(sdr["ID"].ToString());
                    budget.ItemCode = sdr["ItemCode"].ToString();
                    budget.ItemName = sdr["ItemName"].ToString();
                    budget.UomCode = sdr["Unit"].ToString();
                    break;
                case "History":
                case "HistoryBudget":
                    #region History data
                    budget.ItemID = Convert.ToInt32(sdr["ItemID"].ToString());
                    budget.Tahun = Convert.ToInt32(sdr["Tahun"].ToString());
                    budget.Jan = Convert.ToDecimal(sdr["Jan"].ToString());
                    budget.Feb = Convert.ToDecimal(sdr["Feb"].ToString());
                    budget.Mar = Convert.ToDecimal(sdr["Mar"].ToString());
                    budget.Apr = Convert.ToDecimal(sdr["Apr"].ToString());
                    budget.Mei = Convert.ToDecimal(sdr["Mei"].ToString());
                    budget.Jun = Convert.ToDecimal(sdr["Jun"].ToString());
                    budget.Jul = Convert.ToDecimal(sdr["Jul"].ToString());
                    budget.Ags = Convert.ToDecimal(sdr["Ags"].ToString());
                    budget.Sep = Convert.ToDecimal(sdr["Sep"].ToString());
                    budget.Okt = Convert.ToDecimal(sdr["Okt"].ToString());
                    budget.Nov = Convert.ToDecimal(sdr["Nov"].ToString());
                    budget.Des = Convert.ToDecimal(sdr["Dese"].ToString());
                    #endregion
                    break;
                case "HistMonth":
                    budget.Jan = Convert.ToDecimal(sdr["Jan"].ToString());
                    break;
                case "HeadDept":
                    budget.HeadID = Convert.ToInt32(sdr["HeadID"].ToString());
                    break;
                case "Nomor":
                    budget.BudgetID = Convert.ToInt32(sdr["Jml"].ToString());
                    break;
                case "DetailBudget":
                    #region DetailBudget
                    budget.BudgetID = Convert.ToInt32(sdr["ID"].ToString());
                    budget.BudgetNo = sdr["BudgetNo"].ToString();
                    budget.Tahun = Convert.ToInt32(sdr["Tahun"].ToString());
                    budget.Bulan = Convert.ToInt32(sdr["Bulan"].ToString());
                    budget.UserID = Convert.ToInt32(sdr["UserID"].ToString());
                    budget.DeptID = Convert.ToInt32(sdr["DeptID"].ToString());
                    budget.HeadID = Convert.ToInt32(sdr["HeadID"].ToString());
                    budget.Approval = Convert.ToInt32(sdr["Aprov"].ToString());
                    budget.ID = Convert.ToInt32(sdr["DetailID"].ToString());
                    budget.ItemID = Convert.ToInt32(sdr["ItemID"].ToString());
                    budget.UomCode = sdr["Unit"].ToString();
                    budget.Quantity = Convert.ToDecimal(sdr["Qty"].ToString());
                    budget.AppvQty = Convert.ToDecimal(sdr["QtyApv"].ToString());
                    budget.Keterangan = sdr["Keterangan"].ToString();
                    budget.ItemCode = sdr["ItemCode"].ToString();
                    budget.ItemName = sdr["ItemName"].ToString();
                    budget.PakaiDetailID = Convert.ToInt32(sdr["PakaiDetailID"].ToString());
                    budget.UomID = Convert.ToInt32(sdr["UomID"].ToString());
                    if (sdr["Aprov"].ToString() == "1")
                    {
                        budget.ApprovalBy = "Head";
                    }
                    else if (sdr["Aprov"].ToString() == "2")
                    {
                        budget.ApprovalBy = (sdr["Grp"].ToString() == "2") ? "HRD Mgr" : "LOG Mgr";
                    }
                    else if (sdr["SPPID"] != DBNull.Value)
                    {
                        budget.ApprovalBy = "SPP No. : " + sdr["NoSPP"].ToString();
                    }
                    else
                    {
                        budget.ApprovalBy = "Open";
                    }
                    #endregion
                    break;
                case "DetailBudget2":
                    #region DetailBudget2
                    budget.BudgetID = Convert.ToInt32(sdr["ID"].ToString());
                    budget.BudgetNo = sdr["BudgetNo"].ToString();
                    budget.Tahun = Convert.ToInt32(sdr["Tahun"].ToString());
                    budget.Bulan = Convert.ToInt32(sdr["Bulan"].ToString());
                    budget.UserID = Convert.ToInt32(sdr["UserID"].ToString());
                    budget.DeptID = Convert.ToInt32(sdr["DeptID"].ToString());
                    budget.HeadID = Convert.ToInt32(sdr["HeadID"].ToString());
                    budget.Approval = Convert.ToInt32(sdr["Aprov"].ToString());
                    budget.ID = Convert.ToInt32(sdr["DetailID"].ToString());
                    budget.ItemID = Convert.ToInt32(sdr["ItemID"].ToString());
                    budget.UomCode = sdr["Unit"].ToString();
                    budget.Matikan = sdr["matikan"].ToString();
                    budget.Quantity = Convert.ToDecimal(sdr["Qty"].ToString());
                    budget.AppvQty = Convert.ToDecimal(sdr["QtyApv"].ToString());
                    budget.Keterangan = sdr["Keterangan"].ToString();
                    budget.ItemCode = sdr["ItemCode"].ToString();
                    budget.ItemName = sdr["ItemName"].ToString();
                    budget.PakaiDetailID = Convert.ToInt32(sdr["PakaiDetailID"].ToString());
                    budget.UomID = Convert.ToInt32(sdr["UomID"].ToString());
                    if (sdr["Aprov"].ToString() == "1")
                    {
                        budget.ApprovalBy = "Head";
                    }
                    else if (sdr["Aprov"].ToString() == "2")
                    {
                        budget.ApprovalBy = (sdr["Grp"].ToString() == "2") ? "HRD Mgr" : "LOG Mgr";
                    }
                    else if (sdr["SPPID"] != DBNull.Value)
                    {
                        budget.ApprovalBy = "SPP No. : " + sdr["NoSPP"].ToString();
                    }
                    else
                    {
                        budget.ApprovalBy = "Open";
                    }
                    #endregion
                    break;
                case "BulanBudget":
                    budget.Bulan = Convert.ToInt32(sdr["Bulan"].ToString());
                    budget.Tahun = Convert.ToInt32(sdr["Tahun"].ToString());
                    budget.ItemName = (sdr["Bulan"] != DBNull.Value) ? Global.nBulan(Convert.ToInt32(sdr["Bulan"].ToString())) : "";
                    break;
                case "DeptBudget":
                    budget.DeptID = Convert.ToInt32(sdr["DeptID"].ToString());
                    budget.DeptName = sdr["DeptName"].ToString();
                    if (this.LinkFrom != "3")
                    {
                        //budget.Approval = Convert.ToInt32(sdr["Approval"].ToString());
                    }
                    break;
                case "ToSPP":
                    budget.ItemID = Convert.ToInt32(sdr["ItemID"].ToString());
                    budget.ItemCode = sdr["ItemCode"].ToString();
                    budget.ItemName = sdr["ItemName"].ToString();
                    budget.UomCode = sdr["Unit"].ToString();
                    budget.Quantity = Convert.ToDecimal(sdr["Qty"].ToString());
                    budget.UomID = Convert.ToInt32(sdr["UomID"].ToString());
                    budget.Stock = Convert.ToDecimal(sdr["Jumlah"].ToString());
                    //budget.PakaiDetailID = Convert.ToInt32(sdr["PakaiDetail"].ToString());
                    break;
                case "ListBudget":
                    budget.Bulan = Convert.ToInt32(sdr[this.Field].ToString());
                    //budget.Tahun = Convert.ToInt32(sdr["Tahun"].ToString());
                    break;
                case "MasterBudget":
                    budget.ID = Convert.ToInt32(sdr["ID"].ToString());
                    budget.DeptID = Convert.ToInt32(sdr["DeptID"].ToString());
                    budget.ItemID = Convert.ToInt32(sdr["ItemID"].ToString());
                    budget.Quantity = Convert.ToDecimal(sdr["Quantity"].ToString());
                    budget.Tahun = Convert.ToInt32(sdr["Tahun"].ToString());
                    budget.RuleCalc = Convert.ToDecimal(sdr["RuleCalc"].ToString());
                    break;
                case "MasterBudgetSP":
                    budget.ID = Convert.ToInt32(sdr["ID"].ToString());
                    budget.DeptID = Convert.ToInt32(sdr["DeptID"].ToString());
                    budget.ItemID = Convert.ToInt32(sdr["ItemID"].ToString());
                    budget.Quantity = Convert.ToDecimal(sdr["MaxQty"].ToString());
                    budget.Tahun = Convert.ToInt32(sdr["Tahun"].ToString());
                    budget.Bulan = Convert.ToInt32(sdr["Bulan"].ToString());
                    budget.ItemTypeID = Convert.ToInt32(sdr["ItemTypeID"].ToString());
                    break;
                case "MasterBudgetTotal":
                    budget.Quantity = Convert.ToDecimal(sdr["Quantity"].ToString());
                    break;
            }
            return budget;
        }
        public int InsertMasterSP(object objDomain)
        {
            try
            {
                int result = 0;
                budget = (Budget)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", budget.ItemID));
                sqlListParam.Add(new SqlParameter("@MaxQty", budget.MaxQty));
                sqlListParam.Add(new SqlParameter("@DeptID", budget.DeptID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", budget.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@Tahun", budget.Tahun));
                sqlListParam.Add(new SqlParameter("@Bulan", budget.Bulan));
                sqlListParam.Add(new SqlParameter("@RowStatus", budget.RowStatus));
                sqlListParam.Add(new SqlParameter("@CreatedBy", budget.CreatedBy));
                result = dataAccess.ProcessData(sqlListParam, "spBudgetSP_Insert");
                return result;
            }
            catch { return -1; }
            
        }
        public int UpdateMasterSP(object objDomain)
        {
            try
            {
                int result = 0;
                budget = (Budget)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", budget.ID));
                sqlListParam.Add(new SqlParameter("@MaxQty", budget.MaxQty));
                sqlListParam.Add(new SqlParameter("@RowStatus", budget.RowStatus));
                sqlListParam.Add(new SqlParameter("@CreatedBy", budget.CreatedBy));
                result = dataAccess.ProcessData(sqlListParam, "spBudgetSP_Update");
                return result;
            }
            catch { return -1; }
            
        }
        public int AddMaterialToMasterBudget(object objDomain)
        {
            int result = 0;
            budget = (Budget)objDomain;
            string strSQL = "Update Inventory set Head=" + budget.Quantity + " where ID=" + budget.ItemID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            result = sdr.RecordsAffected;
            return result;
        }

        public string cekkode(string Kode)
        {
            string result = "0";
            string StrSql = " select Head from Inventory where ItemCode='"+Kode+"' and RowStatus > -1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    result = sqlDataReader["Head"].ToString();
                }
            }

            return result;
        }
    }

    public class Budget : Pakai
    {
        public decimal Jan { get; set; }
        public decimal Feb { get; set; }
        public decimal Mar { get; set; }
        public decimal Apr { get; set; }
        public decimal Mei { get; set; }
        public decimal Jun { get; set; }
        public decimal Jul { get; set; }
        public decimal Ags { get; set; }
        public decimal Sep { get; set; }
        public decimal Okt { get; set; }
        public decimal Nov { get; set; }
        public decimal Des { get; set; }
        public int Bulan { get; set; }
        public string BudgetNo { get; set; }
        public decimal AppvQty { get; set; }
        public int UserID { get; set; }
        public int HeadID { get; set; }
        public int BudgetID { get; set; }
        public int Approval { get; set; }
        public string SPPNo { get; set; }
        public decimal Stock { get; set; }
        public int PakaiDetailID { get; set; }
        public string Ket { get; set; }
        public decimal MaxQty { get; set; }
    }
}
