using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using DataAccessLayer;
using Domain;

namespace BusinessFacade
{
    public class BudgetAtkFacade : AbstractFacade
    {
        private BudgetAtk objBudgetAtk = new BudgetAtk();
        private Dept objDept = new Dept();
        private ArrayList arrBudgetSP;
        public BudgetAtkFacade()
            : base()
        {

        }
        private ArrayList arrData = new ArrayList();
        private List<SqlParameter> sqllistParam;
        private BudgetAtk budgetAtk = new BudgetAtk();
        public override int Insert(object objDomain)
        {
            try
            {
                int result = 0;
                budgetAtk = (BudgetAtk)objDomain;
                sqllistParam = new List<SqlParameter>();
                sqllistParam.Add(new SqlParameter("@ItemID", budgetAtk.ItemID));
                sqllistParam.Add(new SqlParameter("@DeptID", budgetAtk.DeptID));
                sqllistParam.Add(new SqlParameter("@Quantity", budgetAtk.Quantity));
                sqllistParam.Add(new SqlParameter("@Tahun", budgetAtk.Tahun));
                sqllistParam.Add(new SqlParameter("@CreatedBy", budgetAtk.CreatedBy));
                sqllistParam.Add(new SqlParameter("@Bulan", budgetAtk.Bulan));
                sqllistParam.Add(new SqlParameter("@ItemTypeID", budgetAtk.ItemTypeID));
                sqllistParam.Add(new SqlParameter("@RuleCalc", budgetAtk.RuleCalc));
                result = dataAccess.ProcessData(sqllistParam, "spInsertBudgetAtkMaster");
                return result;

            }
            catch
            {
                return -1;
            }
        }

        public override int Update(object objDomain)
        {
            try
            {
                //untuk proses revisi budget
                int result = 0;
                budgetAtk = (BudgetAtk)objDomain;
                sqllistParam = new List<SqlParameter>();
                sqllistParam.Add(new SqlParameter("@ID", budgetAtk.ID));
                sqllistParam.Add(new SqlParameter("@Bulan", budgetAtk.Bulan));
                sqllistParam.Add(new SqlParameter("@Tahun", budgetAtk.Tahun));
                sqllistParam.Add(new SqlParameter("@DeptID", budgetAtk.DeptID));
                sqllistParam.Add(new SqlParameter("@ItemID", budgetAtk.ItemID));
                sqllistParam.Add(new SqlParameter("@Quantity", budgetAtk.Quantity));
                sqllistParam.Add(new SqlParameter("@ItemTypeID", budgetAtk.ItemTypeID));
                sqllistParam.Add(new SqlParameter("@CreatedBy", ((Users)HttpContext.Current.Session["Users"]).UserID));
                sqllistParam.Add(new SqlParameter("@RuleCalc", budgetAtk.RuleCalc));
                result = dataAccess.ProcessData(sqllistParam, "spUpdateBudgetAtkMaster");
                return result;
            }
            catch
            {
                return -1;
            }

        }
        public int UpdateQty(object objDomain, bool baru)
        {
            try
            {
                //untuk proses revisi budget
                int result = 0;
                budgetAtk = (BudgetAtk)objDomain;
                sqllistParam = new List<SqlParameter>();
                sqllistParam.Add(new SqlParameter("@ID", budgetAtk.ID));
                sqllistParam.Add(new SqlParameter("@Bulan", budgetAtk.Bulan));
                sqllistParam.Add(new SqlParameter("@Tahun", budgetAtk.Tahun));
                sqllistParam.Add(new SqlParameter("@DeptID", budgetAtk.DeptID));
                sqllistParam.Add(new SqlParameter("@ItemID", budgetAtk.ItemID));
                sqllistParam.Add(new SqlParameter("@Quantity", budgetAtk.Quantity));
                sqllistParam.Add(new SqlParameter("@RuleCalc", budgetAtk.RuleCalc));
                sqllistParam.Add(new SqlParameter("@CreatedBy", ((Users)HttpContext.Current.Session["Users"]).UserID));
                sqllistParam.Add(new SqlParameter("@MasaBerlaku", budgetAtk.MasaBerlaku));
                result = dataAccess.ProcessData(sqllistParam, "spUpdateBudgetAtkMaster_Tambahan");
                return result;
            }
            catch
            {
                return -1;
            }

        }
        public int Insert(object objDomain, bool Tambahan)
        {
            try
            {
                int result = 0;
                budgetAtk = (BudgetAtk)objDomain;
                sqllistParam = new List<SqlParameter>();
                sqllistParam.Add(new SqlParameter("@ItemID", budgetAtk.ItemID));
                sqllistParam.Add(new SqlParameter("@DeptID", budgetAtk.DeptID));
                sqllistParam.Add(new SqlParameter("@Tahun", budgetAtk.Tahun));
                sqllistParam.Add(new SqlParameter("@BudgetAwal", budgetAtk.BudgetAwal));
                sqllistParam.Add(new SqlParameter("@Revisi", budgetAtk.Revisi));
                sqllistParam.Add(new SqlParameter("@Tambahan", budgetAtk.Tambahan));
                sqllistParam.Add(new SqlParameter("@MasaBerlaku", budgetAtk.MasaBerlaku));
                sqllistParam.Add(new SqlParameter("@CreatedBy", ((Users)HttpContext.Current.Session["Users"]).UserID));
                result = dataAccess.ProcessData(sqllistParam, "spBudgetAtkMasterTambahan_Insert");
                return result;
            }
            catch
            {
                return -1;
            }
        }

        public override int Delete(object objDomain)
        {
            try
            {
                int result = 0;
                budgetAtk = (BudgetAtk)objDomain;
                sqllistParam = new List<SqlParameter>();
                sqllistParam.Add(new SqlParameter("@ID", budgetAtk.ID));
                sqllistParam.Add(new SqlParameter("@CreatedBy", budgetAtk.CreatedBy));
                result = dataAccess.ProcessData(sqllistParam, "spDeleteBudgetAtkMaster");
                return result;
            }
            catch
            {
                return -1;
            }

        }

        public override ArrayList Retrieve()
        {
            string strSQL = "select * from BudgetATKMaster where RowStatus >-1 order by id desc";
            //selalu gunakan baris ini biar tidak nyangkut query nya
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrBudgetSP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBudgetSP.Add(GenerateObject(sqlDataReader));
                }
            }

            return arrBudgetSP;
        }

        public BudgetAtk Retrieve(int BudgetID)
        {
            string strSQL = "select bp.*,D.DeptCode,D.DeptName,D.NamaHead,v.ItemCode,v.ItemName," +
                            "u.UomCode " +
                            "from BudgetATKMaster bp " +
                            "Left Join Dept as D on D.ID=bp.DeptID " +
                            "Left Join Inventory as v on v.ID=bp.ItemID " +
                            "Left Join UOM as u on U.ID=v.UomID " +
                            "where bp.RowStatus >-1 and bp.ID=" + BudgetID;

            objBudgetAtk = new BudgetAtk();
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrBudgetSP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    objBudgetAtk = GenerateObject(sqlDataReader, GenerateObject(sqlDataReader));
                }
            }

            return objBudgetAtk;
        }

        public ArrayList Retrieve(int ItemID, int Tahun)
        {
            string strSQL = "select bp.*,D.DeptCode,D.DeptName,D.NamaHead,v.ItemCode,v.ItemName," +
                            "u.UomCode " +
                            "from BudgetATKMaster bp " +
                            "Left Join Dept as D on D.ID=bp.DeptID " +
                            "Left Join Inventory as v on v.ID=bp.ItemID " +
                            "Left Join UOM as u on U.ID=v.UomID " +
                            "where bp.RowStatus >-1 and bp.ItemID=" + ItemID + " and bp.Tahun=" + Tahun +
                            "Order by d.DeptName";
            //selalu gunakan baris ini biar tidak nyangkut query nya
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrBudgetSP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBudgetSP.Add(GenerateObject(sqlDataReader, GenerateObject(sqlDataReader)));
                }
            }

            return arrBudgetSP;
        }
        public ArrayList Retrieve(int ItemID, int Tahun, int DeptID)
        {
            string strSQL = "select bp.*,D.DeptCode,D.DeptName,D.NamaHead,v.ItemCode,v.ItemName," +
                            "u.UomCode " +
                            "from BudgetATKMaster bp " +
                            "Left Join Dept as D on D.ID=bp.DeptID " +
                            "Left Join Inventory as v on v.ID=bp.ItemID " +
                            "Left Join UOM as u on U.ID=v.UomID " +
                            "where bp.RowStatus >-1 and bp.ItemID=" + ItemID + " and bp.Tahun=" + Tahun +
                            " and bp.DeptID=" + DeptID +
                            "Order by d.DeptName";
            //selalu gunakan baris ini biar tidak nyangkut query nya
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrBudgetSP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBudgetSP.Add(GenerateObject(sqlDataReader, GenerateObject(sqlDataReader)));
                }
            }

            return arrBudgetSP;
        }
        public ArrayList RetrieveDept()
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from Dept where RowStatus > -1");
            strError = dataAccess.Error;
            arrBudgetSP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBudgetSP.Add(GenerateObject1(sqlDataReader));
                }
            }
            else
                arrBudgetSP.Add(new Dept());

            return arrBudgetSP;
        }

        public Dept GenerateObject1(SqlDataReader sqlDataReader)
        {
            objDept = new Dept();
            objDept.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objDept.DeptCode = sqlDataReader["DeptCode"].ToString();
            objDept.DeptName = sqlDataReader["DeptName"].ToString();
            objDept.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objDept.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objDept.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objDept.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objDept.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objDept;
        }

        public BudgetAtk GenerateObject(SqlDataReader sqlDataReader)
        {
            objBudgetAtk = new BudgetAtk();
            objBudgetAtk.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objBudgetAtk.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objBudgetAtk.Quantity = Convert.ToDecimal(sqlDataReader["Quantity"]);
            objBudgetAtk.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objBudgetAtk.Tahun = Convert.ToInt32(sqlDataReader["Tahun"]);
            objBudgetAtk.Bulan = Convert.ToInt32(sqlDataReader["Bulan"]);
            objBudgetAtk.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"]);
            return objBudgetAtk;
        }
        public BudgetAtk GenerateObject(SqlDataReader sqlDataReader, BudgetAtk bdg)
        {
            objBudgetAtk = (BudgetAtk)bdg;
            objBudgetAtk.DeptName = sqlDataReader["DeptName"].ToString();
            objBudgetAtk.ItemCode = sqlDataReader["ItemCode"].ToString();
            objBudgetAtk.ItemName = sqlDataReader["ItemName"].ToString();
            objBudgetAtk.UomCode = sqlDataReader["UomCode"].ToString();
            objBudgetAtk.Jumlah = (sqlDataReader["AddQty"] == DBNull.Value) ? 0 : Convert.ToDecimal(sqlDataReader["AddQty"].ToString());
            objBudgetAtk.RuleCalc = (sqlDataReader["RuleCalc"] != DBNull.Value) ? Convert.ToInt32(sqlDataReader["RuleCalc"].ToString()) : 0;
            return objBudgetAtk;
        }
        /// <summary>
        /// Update Material inventory yang masuk ke dalam program budget
        /// </summary>
        /// <param name="ItemID">Field ID</param>
        /// <param name="TipeBudget"> Tipe Budget 1: ATK Umum, 2: ATK Ga, 5:Consumable</param>
        /// <returns></returns>
        public int UpdateHeadForBudget(int ItemID, int TipeBudget)
        {
            int result = 0;
            string strSQL = "Update Inventory set Head=" + TipeBudget + " where ID=" + ItemID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            result = sdr.RecordsAffected;
            return result;
        }
    }

    public class BudgetAtk : Inventory
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
        public int Tahun { get; set; }
        public decimal Quantity { get; set; }
        public int RuleCalc { get; set; }
        public int MasaBerlaku { get; set; }
        public decimal BudgetAwal { get; set; }
        public decimal Revisi { get; set; }
        public decimal Tambahan { get; set; }
    }
}
