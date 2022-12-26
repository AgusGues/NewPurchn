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
    public class MasterSectionNfFacade : AbstractTransactionFacade
    {
        private MasterSectionNf.ParamHead obj = new MasterSectionNf.ParamHead();
        private List<SqlParameter> sqlListParam;
        public MasterSectionNfFacade(object objDomain)
            : base(objDomain)
        {
            obj = (MasterSectionNf.ParamHead)objDomain;
        }
        public override int Delete(TransactionManager transManager)
        {
            int result = 0;
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", obj.Id));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", obj.LastModifiedBy));
                result = transManager.DoTransaction(sqlListParam, "spDeleteISOBagian");
                strError = transManager.Error;
            }
            catch
            {
                result = -1;
            }
            return result;
        }

        public override int Insert(TransactionManager transManager)
        {
            int result = 0;
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@BagianName", obj.BagianName));
                sqlListParam.Add(new SqlParameter("@DeptID", obj.DeptId));
                sqlListParam.Add(new SqlParameter("@UserGroup", obj.UserGroupId));
                result = transManager.DoTransaction(sqlListParam, "spInsertISOBagian");
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
            int result = 0;
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", obj.Id));
                sqlListParam.Add(new SqlParameter("@BagianName", obj.BagianName));
                sqlListParam.Add(new SqlParameter("@DeptID", obj.DeptId));
                sqlListParam.Add(new SqlParameter("@UserGroup", obj.UserGroupId));
                sqlListParam.Add(new SqlParameter("@RowStatus", obj.RowStatus));
                result = transManager.DoTransaction(sqlListParam, "spUpdateISOBagian");
                strError = transManager.Error;
            }
            catch
            {
                result = -1;
            }
            return result;
        }

        public override ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }

        public static int AddBobotPes(int Kerja, int Dept, int Bagian, string user, decimal Kpi, decimal Sop, decimal Task, decimal Disiplin, int BerlakuBulan, int BerlakuTahun)
        {
            //PlantID, DeptID, BagianID, PesType, Bobot, RowStatus, Createdby, CreatedTime, Activebulan, Activetahun,
            int val = 0;
            DateTime today = DateTime.Today;

            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "" +
"INSERT INTO ISO_BobotPes values(" + Kerja + "," + Dept + "," + Bagian + ",1," + Kpi + ",0,'" + user + "','" + today + "'," + BerlakuBulan + "," + BerlakuTahun + " ) " +
"INSERT INTO ISO_BobotPes values(" + Kerja + "," + Dept + "," + Bagian + ",2," + Sop + ",0,'" + user + "','" + today + "'," + BerlakuBulan + "," + BerlakuTahun + " ) " +
"INSERT INTO ISO_BobotPes values(" + Kerja + "," + Dept + "," + Bagian + ",3," + Task + ",0,'" + user + "','" + today + "'," + BerlakuBulan + "," + BerlakuTahun + " ) " +
"INSERT INTO ISO_BobotPes values(" + Kerja + "," + Dept + "," + Bagian + ",4," + Disiplin + ",0,'" + user + "','" + today + "'," + BerlakuBulan + "," + BerlakuTahun + " ) "
                        ;
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static int EditBobotPes(int Kerja, int Dept, int Bagian, string user, decimal Kpi, decimal Sop, decimal Task, decimal Disiplin, int BerlakuBulan, int BerlakuTahun)
        {
            //PlantID, DeptID, BagianID, PesType, Bobot, RowStatus, Createdby, CreatedTime, Activebulan, Activetahun,
            int val = 0;
            DateTime today = DateTime.Today;
            int bln = today.Month;
            int thn = today.Year;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "" +
"update iso_BobotPes set Activebulan=" + BerlakuBulan + ",Activetahun=" + BerlakuTahun + ", DeptID=" + Dept + ", Bobot=" + Kpi + ", CreatedBy='" + user + "'  where BagianId=" + Bagian + " and PesType=1 and rowStatus>-1 " +
"update iso_BobotPes set Activebulan=" + BerlakuBulan + ",Activetahun=" + BerlakuTahun + ", DeptID=" + Dept + ", Bobot=" + Sop + ", CreatedBy='" + user + "'  where BagianId=" + Bagian + " and PesType=2 and rowStatus>-1 " +
"update iso_BobotPes set Activebulan=" + BerlakuBulan + ",Activetahun=" + BerlakuTahun + ", DeptID=" + Dept + ", Bobot=" + Task + ", CreatedBy='" + user + "'  where BagianId=" + Bagian + " and PesType=3 and rowStatus>-1 " +
"update iso_BobotPes set Activebulan=" + BerlakuBulan + ",Activetahun=" + BerlakuTahun + ", DeptID=" + Dept + ", Bobot=" + Disiplin + ", CreatedBy='" + user + "'  where BagianId=" + Bagian + " and PesType=4 and rowStatus>-1 "
                        ;
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static int DelBobotPes(string user, int Bagian)
        {
            //PlantID, DeptID, BagianID, PesType, Bobot, RowStatus, Createdby, CreatedTime, Activebulan, Activetahun,
            int val = 0;
            DateTime today = DateTime.Today;
            int bln = today.Month;
            int thn = today.Year;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "" +
"update iso_bagian set rowStatus=-1 where Id=" + Bagian + " " +
"update iso_BobotPes set rowStatus=-1, CreatedBy='" + user + "'  where BagianId=" + Bagian + " and PesType=1 and rowStatus>-1 " +
"update iso_BobotPes set rowStatus=-1, CreatedBy='" + user + "'  where BagianId=" + Bagian + " and PesType=2 and rowStatus>-1 " +
"update iso_BobotPes set rowStatus=-1, CreatedBy='" + user + "'  where BagianId=" + Bagian + " and PesType=3 and rowStatus>-1 " +
"update iso_BobotPes set rowStatus=-1, CreatedBy='" + user + "'  where BagianId=" + Bagian + " and PesType=4 and rowStatus>-1 "
                        ;
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static List<MasterSectionNf.ParamDepartment> ListDepartment()
        {
            List<MasterSectionNf.ParamDepartment> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "Select DeptId Id,DeptName from ISO_Dept_m WHERE RowStatus>-1";
                    AllData = connection.Query<MasterSectionNf.ParamDepartment>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<MasterSectionNf.ParamData> ListData(int dept)
        {
            List<MasterSectionNf.ParamData> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "" +
"WITH q AS ( " +
    "SELECT Id, BagianName, UserGroupId, " +
    "CASE UserGroupID WHEN 200 THEN 'Yang Di Aproval' ELSE 'Yang Aproval' END UserGroupName, " +
    "DeptId, (SELECT DeptName FROM Dept d WHERE d.ID=deptid) DeptName " +
    "FROM ISO_Bagian WHERE RowStatus> -1 AND DeptID = " + dept +
"), " +
"w AS ( " +
    "SELECT " +
    "isnull(( " +
        "SELECT TOP 1 b.Bobot FROM ISO_BobotPES b " +
        "WHERE b.BagianID = q.Id AND b.PesType = 1 AND b.RowStatus > -1 ORDER BY b.ID DESC " +
    "), 0)*100 BobotKpi, " +
    "isnull(( " +
        "SELECT TOP 1 b.Bobot FROM ISO_BobotPES b " +
        "WHERE b.BagianID = q.Id AND b.PesType = 2 AND b.RowStatus > -1 ORDER BY b.ID DESC " +
    "),0)*100 BobotSop, " +
    "isnull(( " +
        "SELECT TOP 1 b.Bobot FROM ISO_BobotPES b " +
        "WHERE b.BagianID = q.Id AND b.PesType = 3 AND b.RowStatus > -1 ORDER BY b.ID DESC " +
    "),0)*100 BobotTask, " +
    "isnull(( " +
        "SELECT TOP 1 b.Bobot FROM ISO_BobotPES b " +
        "WHERE b.BagianID = q.Id AND b.PesType = 4 AND b.RowStatus > -1 ORDER BY b.ID DESC " +
    "),0)*100 BobotDisiplin, " +
    "( SELECT TOP 1 b.Activebulan FROM ISO_BobotPES b WHERE b.BagianID = q.Id AND b.RowStatus > -1 ORDER BY b.ID DESC ) BerlakuBulan, " +
    "(SELECT TOP 1 b.Activetahun FROM ISO_BobotPES b WHERE b.BagianID = q.Id AND b.RowStatus > -1 ORDER BY b.ID DESC) BerlakuTahun, " +
    "* FROM q " +
") " +
"SELECT " +
"case BerlakuBulan " +
"when 1 then 'Januari' " +
"when 2 then 'Februari' " +
"when 3 then 'Maret' " +
"when 4 then 'April' " +
"when 5 then 'Mei' " +
"when 6 then 'Juni' " +
"when 7 then 'Juli' " +
"when 8 then 'Agustus' " +
"when 9 then 'September' " +
"when 10 then 'Oktober' " +
"when 11 then 'November' " +
"else 'Desember' end BerlakuBulanName, " +
"* FROM w";
                    AllData = connection.Query<MasterSectionNf.ParamData>(query).ToList();
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
