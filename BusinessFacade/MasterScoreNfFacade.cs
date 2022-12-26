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
    public class MasterScoreNfFacade : AbstractTransactionFacade
    {
        private MasterScoreNf.ParamHead obj = new MasterScoreNf.ParamHead();
        private List<SqlParameter> sqlListParam;
        public MasterScoreNfFacade(object objDomain)
            : base(objDomain)
        {
            obj = (MasterScoreNf.ParamHead)objDomain;
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
                sqlListParam.Add(new SqlParameter("@CategoryID", obj.CategoryId));
                sqlListParam.Add(new SqlParameter("@PesType", obj.PesType));
                sqlListParam.Add(new SqlParameter("@Target", obj.TargetKe));
                sqlListParam.Add(new SqlParameter("@Nilai", obj.PointNilai));
                result = transManager.DoTransaction(sqlListParam, "spISO_SOPScore_insert");
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
            throw new NotImplementedException();
        }

        public override ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }

        public static List<MasterScoreNf.ParamDepartment> ListDepartment()
        {
            List<MasterScoreNf.ParamDepartment> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "Select Id,DeptName from Dept WHERE RowStatus>-1 order by DeptName";
                    AllData = connection.Query<MasterScoreNf.ParamDepartment>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<MasterScoreNf.ParamSection> ListSection(int dept)
        {
            List<MasterScoreNf.ParamSection> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT Id, BagianName FROM ISO_Bagian WHERE RowStatus>-1 and DeptID=" + dept + " ORDER BY BagianName";
                    AllData = connection.Query<MasterScoreNf.ParamSection>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<MasterScoreNf.ParamCategory> ListCategory(int Section, int PesType)
        {
            List<MasterScoreNf.ParamCategory> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = 
"select distinct uc.CategoryID,ic.Description,CAST(ic.KodeUrutan as int)Urutan "+
"from ISO_UserCategory as uc " +
"left join ISO_Category as ic on ic.ID = uc.CategoryID " +
"where ic.RowStatus > -1 and uc.RowStatus > -1 " +
"and uc.SectionID = "+ Section + " and uc.PesType = "+ PesType + " order by Urutan,CategoryID";
                    AllData = connection.Query<MasterScoreNf.ParamCategory>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<MasterScoreNf.ParamData> ListData(int Category)
        {
            List<MasterScoreNf.ParamData> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = 
"SELECT DISTINCT sp.Id, ct.PesType,ct.Category, "+
"ct.DeptId, (SELECT DeptName FROM Dept d WHERE d.ID = ct.DeptID) DeptName, " +
"uc.SectionId, b.BagianName, " +
"sp.CategoryId, ct.Description, sp.TargetKe,sp.PointNilai " +
"from ISO_SOPScore sp, ISO_Category ct, ISO_UserCategory uc, ISO_Bagian b " +
"WHERE ct.PesType = sp.PesType and ct.ID = sp.CategoryID AND sp.CategoryID = uc.CategoryID AND uc.SectionID = b.ID " +
"and ct.RowStatus > -1 and sp.RowStatus > -1 " +
"and sp.CategoryID = "+ Category;
                    AllData = connection.Query<MasterScoreNf.ParamData>(query).ToList();
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
            int val = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "UPDATE ISO_SOPScore SET RowStatus=-1 WHERE ID=" + Id;
                    ;
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static int UpdateData(int Id, int PesType, int CategoryId, string TargetKe, int PointNilai)
        {
            int val = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "UPDATE ISO_SOPScore SET CategoryID="+ CategoryId + ",PesType=" + PesType + ",PointNilai=" + PointNilai + ",TargetKe='"+ TargetKe + "' WHERE ID=" + Id;
                    ;
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
