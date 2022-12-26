using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using System.Collections;
using System.Data.SqlClient;
using DataAccessLayer;

namespace BusinessFacade
{
    public class PlanningFacade : AbstractFacade
    {
        private Planning objPlanning = new Planning();
        private ArrayList arrData;
        private List<SqlParameter> ListParam;
        private Users user = (Users)System.Web.HttpContext.Current.Session["Users"];
        public PlanningFacade()
            : base()
        {

        }
        public override int Insert(object objDomain)
        {
            try
            {
                objPlanning = (Planning)objDomain;
                ListParam = new List<SqlParameter>();
                ListParam.Add(new SqlParameter("@Bulan", objPlanning.Bulan));
                ListParam.Add(new SqlParameter("@Tahun", objPlanning.Tahun));
                ListParam.Add(new SqlParameter("@RunningLine", objPlanning.RunningLine));
                ListParam.Add(new SqlParameter("@CreatedBy", user.UserID));
                ListParam.Add(new SqlParameter("@UnitKerjaID", user.UnitKerjaID));
                ListParam.Add(new SqlParameter("@Keterangan", objPlanning.Keterangan));
                ListParam.Add(new SqlParameter("@Revision", objPlanning.Revision));
                int result = dataAccess.ProcessData(ListParam, "spMaterialPP_Insert");
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
                objPlanning = (Planning)objDomain;
                ListParam = new List<SqlParameter>();
                ListParam.Add(new SqlParameter("@ID", objPlanning.ID.ToString()));
                ListParam.Add(new SqlParameter("@RunningLine", objPlanning.RunningLine));
                ListParam.Add(new SqlParameter("@CreatedBy", objPlanning.CreatedBy));
                ListParam.Add(new SqlParameter("@Keterangan", objPlanning.Keterangan));
                int result = dataAccess.ProcessData(ListParam, "spMaterialPP_Update");
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
                objPlanning = (Planning)objDomain;
                ListParam = new List<SqlParameter>();
                ListParam.Add(new SqlParameter("@ID", objPlanning.ID.ToString()));
                ListParam.Add(new SqlParameter("@RowStatus", objPlanning.RowStatus));
                ListParam.Add(new SqlParameter("@CreatedBy", objPlanning.CreatedBy));
                ListParam.Add(new SqlParameter("@Keterangan", objPlanning.Keterangan));
                int result = dataAccess.ProcessData(ListParam, "spMaterialPP_Delete");
                return result;
            }
            catch
            {
                return -1;
            }
        }
        public override ArrayList Retrieve()
        {
            string strSQL = "SELECT *,(DATENAME(MONTH,DateADD(Month,bulan,0)-1)+' '+ CAST(Tahun as CHAR(4)))Periode FROM MaterialPP where RowStatus >-1 Order By Tahun,Bulan,Revision Desc";
            return ProcesQuery(strSQL);
        }
        public ArrayList Retrieve(string Periode)
        {
            string strSQL = "SELECT *,(DATENAME(MONTH,DateADD(Month,bulan,0)-1)+' '+ CAST(Tahun as CHAR(4)))Periode FROM MaterialPP where RowStatus >-1 " + Periode + " Order By ID desc, Revision Desc";
            return ProcesQuery(strSQL);
        }
        public ArrayList Retrieve(string Periode, bool Aktif)
        {
            string strSQL = "SELECT *,(DATENAME(MONTH,DateADD(Month,bulan,0)-1)+' '+ CAST(Tahun as CHAR(4)))Periode FROM MaterialPP where RowStatus >-1 " + Periode + " Order By Tahun Desc,bulan desc, Revision Desc";
            strSQL = (Aktif == true) ? strSQL.Replace("*", "Top 1 *") : strSQL;
            return ProcesQuery(strSQL);
        }

        public ArrayList RetrieveTahun()
        {
            string strSQL = "SELECT DISTINCT(Tahun) Tahun FROM MaterialPP where RowStatus=0 Order By Tahun";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            arrData = new ArrayList();
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new Planning
                    {
                        Tahun = int.Parse(sdr["Tahun"].ToString())
                    });
                }
            }
            return arrData;
        }
        public int ClosingPlaning(int ID, string tahun, string bulan)
        {
            int result = 0;
            string strSQL = "update pakai set PlanningID=" + ID + " where status>-1 and year(pakaidate)=" + tahun + " and month(pakaidate)=" + bulan +
                " select @@ROWCOUNT result";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            arrData = new ArrayList();
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = int.Parse(sdr["result"].ToString());
                }
            }
            return result;
        }
        private ArrayList ProcesQuery(string Query)
        {
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(Query);
            arrData = new ArrayList();
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateObject(sdr));
                }
            }
            return arrData;
        }
        private Planning ProcesQuery(string Query, bool Detail)
        {
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(Query);
            objPlanning = new Planning();
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    objPlanning = (GenerateObject(sdr));
                }
            }
            return objPlanning;

        }
        private Planning GenerateObject(SqlDataReader sdr)
        {
            objPlanning = new Planning();
            objPlanning.ID = int.Parse(sdr["ID"].ToString());
            objPlanning.Periode = sdr["Periode"].ToString();
            objPlanning.Bulan = int.Parse(sdr["Bulan"].ToString());
            objPlanning.Tahun = int.Parse(sdr["Tahun"].ToString());
            objPlanning.RunningLine = sdr["Planning"].ToString();
            objPlanning.Revision = int.Parse(sdr["Revision"].ToString());
            objPlanning.CreatedBy = sdr["CreatedBy"].ToString();
            objPlanning.CreatedTime = DateTime.Parse(sdr["CreatedTime"].ToString());
            objPlanning.Keterangan = sdr["Keterangan"].ToString();
            return objPlanning;
        }
        public bool isMaterialBudgetKhusus(string ItemID, string DeptID)
        {
            bool result = false;
            string strSQL = "SELECT ItemID FROM MaterialPPKhusus WHERE RowStatus>-1 AND ItemID=" + ItemID + " AND DeptID=" + DeptID + " ORDER BY ID DESC";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                result = true;
            }
            return result;

        }
        public decimal MaterialBudgetKhusus(string ItemID, string DeptID)
        {
            decimal result = 0;
            string strSQL = "SELECT Top 1 Qty FROM MaterialPPKhusus WHERE RowStatus>-1 AND ItemID=" + ItemID + " AND DeptID=" + DeptID + " ORDER BY ID DESC";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty || sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = decimal.Parse(sdr["Qty"].ToString());
                }
            }
            return result;
        }
        public decimal MaterialBudgetBM(string ItemID, int RunnIngLine)
        {
            decimal result = 0;
            string strsql = "SELECT Top 1 BudgetQty FROM MaterialPPBudgetBM WHERE RowStatus>-1 AND ItemID=" + ItemID + " AND RunningLine=" + RunnIngLine + " Order by ID desc";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strsql);
            if (da.Error == string.Empty || sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = decimal.Parse(sdr["BudgetQty"].ToString());
                }
            }
            return result;
        }
        public decimal MaterialBudgetPrj(string ItemID, string ProjectID)
        {
            decimal result = 0;
            string strsql = "SELECT jumlah BudgetQty FROM MTC_ProjectMaterial where ProjectID=" + ProjectID + "  AND ItemID=" + ItemID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strsql);
            if (da.Error == string.Empty || sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = decimal.Parse(sdr["BudgetQty"].ToString());
                }
            }
            return result;
        }
    }
}
