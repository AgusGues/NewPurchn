using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;
using System.Runtime.InteropServices;
using System.Web;
using Dapper;

namespace BusinessFacade
{
    public class DeptFacade : AbstractFacade
    {
        private Dept objDept = new Dept();
        private ArrayList arrDept;
        private List<SqlParameter> sqlListParam;
        //generate construtore
        public DeptFacade()
            : base()
        {

        }
        public override int Insert(object objDomain)
        {
            try
            {
                objDept = (Dept)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@DeptCode", objDept.DeptCode));
                sqlListParam.Add(new SqlParameter("@DeptName", objDept.DeptName));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objDept.CreatedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertDept");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public override int Update(object objDomain)
        {
            try
            {
                objDept = (Dept)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objDept.ID));
                sqlListParam.Add(new SqlParameter("@DeptCode", objDept.DeptCode));
                sqlListParam.Add(new SqlParameter("@DeptName", objDept.DeptName));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objDept.CreatedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateDept");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public override int Delete(object objDomain)
        {
            try
            {
                objDept = (Dept)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objDept.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objDept.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteDept");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public string Criteria { get; set; }

        public override ArrayList Retrieve()
        {
            HttpContext context = HttpContext.Current;
            /** string A = context.Session["ReceiptAsset"].ToString();  string B = context.Session["DeptID"].ToString(); **/
            //string KodeAsset = (string)(Session["ReceiptAsset"]);  

            string A = context.Session["ReceiptAsset"] == null ? "" : context.Session["ReceiptAsset"].ToString();
            string B = context.Session["DeptID"] == null ? "" : context.Session["DeptID"].ToString();

            Users users = (Users)HttpContext.Current.Session["Users"];
            if (A == "ReceiptAsset")
            {
                Criteria = " and ID='" + B + "' ";
            }
            else
            {
                if (users.DeptID == 30 || users.DeptID == 22 || users.DeptID == 6)
                {
                    Criteria = " and ID='" + users.DeptID + "' ";
                }
            }

            string strSQL = "select A.ID,A.DeptCode,A.DeptName,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime," +
                            "A.NamaHead from Dept as A where A.RowStatus = 0 " + this.Criteria + "  order by A.DeptName";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrDept = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDept.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrDept.Add(new Dept());

            return arrDept;
        }

        public static List<Dept> RetrieveNew()
        {
            HttpContext context = HttpContext.Current;
            List<Dept> alldata = new List<Dept>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string A = context.Session["ReceiptAsset"] == null ? "" : context.Session["ReceiptAsset"].ToString();
                    string B = context.Session["DeptID"] == null ? "" : context.Session["DeptID"].ToString();
                    string criteria = "";
                    Users users = (Users)HttpContext.Current.Session["Users"];
                    if (A == "ReceiptAsset")
                    {
                        criteria = " and ID='" + B + "' ";
                    }
                    else
                    {
                        if (users.DeptID == 30 || users.DeptID == 22)
                        {
                            criteria = " and ID='" + users.DeptID + "' ";
                        }
                    }
                    string strSQL = "select A.ID,A.DeptCode,A.DeptName,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime," +
                            "A.NamaHead from Dept as A where A.RowStatus = 0 " + criteria + "  order by A.DeptName";
                    alldata = connection.Query<Dept>(strSQL).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }
        public ArrayList RetrieveDept()
        {
            string strSQL = "select distinct A.Alias from Dept as A where A.RowStatus = 0 " + this.Criteria + "  order by A.Alias";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrDept = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDept.Add(GenerateObjectDept(sqlDataReader));
                }
            }
            else
                arrDept.Add(new Dept());

            return arrDept;
        }

        public ArrayList RetrievePerDept(int DeptID)
        {
            string strSQL = "select A.ID,A.DeptCode,A.DeptName,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime," +
                            "A.NamaHead from Dept as A where A.RowStatus = 0 and A.ID= " + DeptID + "  order by A.DeptName";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrDept = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDept.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrDept.Add(new Dept());

            return arrDept;
        }
        public Dept RetrieveDeptByUserGroup(int Id)
        {
            string strSql = "select distinct Users.ID as UserID,ISO_Bagian.UserGroupID from Users,ISO_Dept,ISO_Bagian " +
                            "where Users.ID = ISO_Dept.UserID and ISO_Dept.DeptID = ISO_Bagian.DeptID and " +
                            "ISO_Dept.UserGroupID = ISO_Bagian.UserGroupID " +
                            "and ISO_Dept.RowStatus>-1 and ISO_Bagian.RowStatus>-1 and iso_dept.DeptID=Users.DeptID and Users.RowStatus>-1 " +
                            "and ISO_Dept.UserID = " + Id;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSql);
            strError = dataAccess.Error;
            arrDept = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new Dept();
        }
        public ArrayList RetrieveMtc()
        {
            string Dept = "and A.DeptName like '%main%'";
            string strSQL = "select A.ID,A.DeptCode,A.DeptName,A.RowStatus,A.CreatedBy," +
                            "A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.NamaHead from Dept as A where A.RowStatus = 0 " +
                            Dept + " order by A.DeptName";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrDept = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDept.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrDept.Add(new Dept());

            return arrDept;
        }
        public ArrayList RetrieveForProject(string inDept)
        {
            string Dept = (inDept == string.Empty) ? "and A.DeptName like '%main%'" : " and A.ID in(" + inDept + ") ";
            string strSQL = "select A.ID,A.DeptCode,A.DeptName,A.RowStatus,A.CreatedBy," +
                            "A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.NamaHead from Dept as A where A.RowStatus = 0 " +
                            Dept + " order by A.DeptName";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrDept = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDept.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrDept.Add(new Dept());

            return arrDept;
        }
        public Dept RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.DeptCode,A.DeptName,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.NamaHead from Dept as A where A.RowStatus = 0 and A.ID = " + Id);
            strError = dataAccess.Error;
            arrDept = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Dept();
        }
        public Dept RetrieveByCode(string deptCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.DeptCode,A.DeptName,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.NamaHead from Dept as A where A.RowStatus = 0 and A.DeptCode = '" + deptCode + "'");
            strError = dataAccess.Error;
            arrDept = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Dept();
        }
        public Dept RetrieveByName(string deptName)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.DeptCode,A.DeptName,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.NamaHead from Dept as A where A.RowStatus = 0 and A.DeptName = '" + deptName + "'");
            strError = dataAccess.Error;
            arrDept = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Dept();
        }
        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.DeptCode,A.DeptName,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.NamaHead from Dept as A where A.RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrDept = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDept.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrDept.Add(new Groups());

            return arrDept;
        }
        public Dept RetrieveDeptByUserID2(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select distinct A.DeptID,A.DeptName,A.RowStatus,A.UserGroupID from ISO_Dept as A where A.RowStatus > -1 " +
                            "and A.UserID=" + Id;
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrDept = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject3(sqlDataReader);
                }
            }

            return new Dept();
        }
        public ArrayList RetrieveForISO(int userID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select A.DeptID,A.DeptName,A.RowStatus,A.UserGroupID from ISO_Dept as A where A.RowStatus > -1 " +
                            " and A.UserID=" + userID;
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrDept = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDept.Add(GenerateObject3(sqlDataReader));
                }
            }
            else
                arrDept.Add(new Dept());
            return arrDept;
        }

        public ArrayList RetrieveByUserID(int userID)
        {
            string strSQL = "select distinct A.DeptID,A.DeptName,A.RowStatus,A.UserGroupID from ISO_Dept as A where A.RowStatus > -1 " +
                            " and A.UserID=" + userID + " and UserGroupID=200 order by DeptName";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrDept = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDept.Add(GenerateObject3(sqlDataReader));
                }
            }
            else
                arrDept.Add(new Dept());

            return arrDept;
        }
        public ArrayList RetrieveByUserID(int userID, bool forManager)
        {
            string strSQL = "select distinct A.DeptID,A.DeptName,A.RowStatus,A.UserGroupID from ISO_Dept as A where A.RowStatus > -1 " +
                            " and A.UserID=" + userID + "/* and UserGroupID=200*/ order by DeptName";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrDept = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDept.Add(GenerateObject3(sqlDataReader));
                }
            }
            else
                arrDept.Add(new Dept());

            return arrDept;
        }
        public ArrayList RetrieveByAll()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select * from (select t.DeptID,'0' as RowStatus,(Select DeptName from Dept where ID=t.DeptID)DeptName,t.UserGroupID " +
                            "from ISO_Task as t where t.RowStatus >-1 Group by t.DeptID)as x Order By x.DeptName";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrDept = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDept.Add(GenerateObject3(sqlDataReader));
                }
            }
            else
                arrDept.Add(new Dept());

            return arrDept;
        }
        public ArrayList RetrieveSection3(string deptName)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select A.DeptID, A.ID as BagianID, A.BagianName from ISO_Bagian as A,ISO_Dept as B " +
                            "where A.DeptID = B.DeptID and A.RowStatus>-1 " +
                            "and A.UserGroupID = B.UserGroupID and B.DeptName='" + deptName + "' " +
                            "Group BY A.DeptID,A.ID,BagianName";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrDept = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDept.Add(GenerateObjectSection(sqlDataReader));
                }
            }
            else
                arrDept.Add(new Dept());

            return arrDept;
        }
        public Dept RetrieveDeptByUserID(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select distinct A.DeptID,A.DeptName,A.RowStatus,A.UserGroupID from ISO_Dept as A where A.RowStatus > -1 " +
                            "and A.UserID=" + Id;
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrDept = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject3(sqlDataReader);
                }
            }

            return new Dept();
        }
        public ArrayList RetrieveBagian(string DeptID)
        {
            arrDept = new ArrayList();
            string strSQL = "Select * from ISO_Bagian where RowStatus>-1 " + this.Criteria;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrDept.Add(SectionObjec(sdr));
                }
            }
            return arrDept;
        }
        public ArrayList RetrieveSection(int userID, string deptName)
        {
            string strSQL = "Select * From ( " +
                            "select distinct A.DeptID, A.ID as BagianID, A.BagianName from ISO_Bagian as A,ISO_Dept as B " +
                            "where A.DeptID = B.DeptID and A.UserGroupID = B.UserGroupID " +
                            "/*and B.UserID=" + userID + "*/" +
                            " and B.DeptID='" + deptName + "' and A.RowStatus >-1 " +
                            // " and B.DeptID in(N'" + deptName + "') and A.RowStatus >-1 and A.UserGroupID=200 " +
                            "UNION ALL " +
                            "Select isnull((Select DeptID from users where ID=" + userID + "),0) DeptID,ID as BagianID,BagianName from ISO_Bagian " +
                            "where DeptID in(Select DeptID from ISO_Dept where UserID=" + userID + ") and UserGroupID=100 " +
                            " and DeptID not in (N'" + deptName + "')) as m order by m.BagianName";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrDept = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDept.Add(GenerateObjectSection(sqlDataReader));
                }
            }
            else
                arrDept.Add(new Dept());

            return arrDept;
        }
        private Dept GenerateObject(SqlDataReader sqlDataReader)
        {
            objDept = new Dept();
            objDept.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objDept.DeptCode = sqlDataReader["DeptCode"].ToString();
            objDept.DeptName = sqlDataReader["DeptName"].ToString();
            objDept.NamaHead = sqlDataReader["NamaHead"].ToString();
            objDept.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objDept.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objDept.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objDept.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objDept.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objDept;
        }
        private Dept GenerateObjectDept(SqlDataReader sqlDataReader)
        {
            objDept = new Dept();
            objDept.Alias = sqlDataReader["Alias"].ToString();
            return objDept;
        }
        private Dept GenerateObject2(SqlDataReader sqlDataReader)
        {
            objDept = new Dept();
            objDept.UserID = Convert.ToInt32(sqlDataReader["UserID"]);
            objDept.UserGroupID = Convert.ToInt32(sqlDataReader["UserGroupID"]);

            return objDept;
        }
        private Dept GenerateObjectSection(SqlDataReader sqlDataReader)
        {
            objDept = new Dept();
            objDept.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            //objDept.SectionName = sqlDataReader["BagianName"].ToString();
            objDept.BagianID = Convert.ToInt32(sqlDataReader["BagianID"]);
            objDept.BagianName = sqlDataReader["BagianName"].ToString();
            //objDept.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            return objDept;
        }
        private Dept GenerateObjectSection2(SqlDataReader sqlDataReader)
        {
            objDept = new Dept();
            objDept.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            //objDept.SectionName = sqlDataReader["BagianName"].ToString();
            objDept.BagianID = Convert.ToInt32(sqlDataReader["BagianID"]);
            //objDept.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            return objDept;
        }
        private Dept GenerateObject3(SqlDataReader sqlDataReader)
        {
            objDept = new Dept();
            objDept.DeptID = Convert.ToInt32(sqlDataReader["DeptID"].ToString());
            objDept.DeptName = sqlDataReader["DeptName"].ToString();
            objDept.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"].ToString());
            objDept.UserGroupID = int.Parse(sqlDataReader["UserGroupID"].ToString());
            return objDept;
        }
        private Dept GenerateObject4(SqlDataReader sqlDataReader)
        {
            objDept = new Dept();
            objDept.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            //objDept.SectionName = sqlDataReader["BagianName"].ToString();
            objDept.DeptName = sqlDataReader["DeptName"].ToString();
            objDept.UserGroupID = Convert.ToInt32(sqlDataReader["UserGroupID"]);
            objDept.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            return objDept;
        }
        /**
         * Added on 29-10-2014
         * ProdukLine SPB BB n BP
         */
        public ArrayList GetLine()
        {
            string strSQL = "Select * from BM_Plant order by ID";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            ArrayList arrDept = new ArrayList();
            if (dataAccess.Error == string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDept.Add(new Plant
                    {
                        ID = Convert.ToInt32(sqlDataReader["ID"].ToString()),
                        PlantCode = sqlDataReader["PlantCode"].ToString()
                    });
                }
            }
            return arrDept;
        }
        //get bagian id for iso_user
        public int GetBagianID(int userid)
        {
            int result = 0;
            string strSQL = "Select ID from ISO_Bagian where ID in(select DeptJabatanID from ISO_Users where ID=" + userid + " and RowStatus>-1) and RowStatus>-1";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToInt32(sdr["ID"].ToString());
                }
            }
            return result;
        }


        public ArrayList RetriveJabatan()
        {
            ArrayList arrUsers = new ArrayList();
            string strSQL = "Select * from ISO_Bagian where DeptID in (select ID from Dept where" + this.Criteria + ") and RowStatus>-1";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrUsers.Add(objectUser(sdr));
                }
            }
            return arrUsers;
        }

        public ArrayList RetrieveAliasDept()
        {
            arrDept = new ArrayList();
            string strSQL = "Select * from Dept where RowStatus>-1 " + this.Criteria;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrDept.Add(GenerateDept(sdr));
                }
            }
            return arrDept;
        }
        public Dept RetrieveAliasDept(int DeptID)
        {
            objDept = new Dept();
            string strSQL = "Select * from Dept where RowStatus>-1 and ID=" + DeptID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    objDept = GenerateDept(sdr);
                }
            }
            return objDept;
        }
        private Dept GenerateDept(SqlDataReader sqlDataReader)
        {
            objDept = new Dept();
            objDept.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objDept.DeptCode = sqlDataReader["DeptCode"].ToString();
            objDept.DeptName = sqlDataReader["DeptName"].ToString();
            objDept.NamaHead = sqlDataReader["NamaHead"].ToString();
            objDept.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objDept.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objDept.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objDept.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objDept.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            objDept.AlisName = sqlDataReader["Alias"].ToString();
            objDept.UserID = (sqlDataReader["HeadID"] != DBNull.Value) ? int.Parse(sqlDataReader["HeadID"].ToString()) : 0;
            return objDept;
        }

        private Dept SectionObjec(SqlDataReader sdr)
        {
            objDept = new Dept();
            objDept.ID = Convert.ToInt32(sdr["ID"].ToString());
            objDept.BagianName = sdr["BagianName"].ToString();
            objDept.DeptID = Convert.ToInt32(sdr["DeptID"].ToString());
            return objDept;
        }

        private Dept objectUser(SqlDataReader sdr)
        {
            objDept = new Dept();
            objDept.BagianName = sdr["BagianName"].ToString();
            return objDept;
        }
        public ArrayList GetDeptFromHead(int UserID)
        {
            arrDept = new ArrayList();
            string strSQL = "SELECT ID FROM Dept where HeadID=" + UserID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrDept.Add(new Dept { DeptID = int.Parse(sdr["ID"].ToString()) });
                }
            }
            return arrDept;
        }
        public ArrayList GetDeptFromMgr(int UserID)
        {
            arrDept = new ArrayList();
            string strSQL = "SELECT ID FROM Dept where MgrID=" + UserID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrDept.Add(new Dept { DeptID = int.Parse(sdr["ID"].ToString()) });
                }
            }
            return arrDept;
        }

        public string RetrieveDeptForApproval(int UserID)
        {
            string result = string.Empty;
            string strSQL = "SELECT ID FROM Dept WHERE HeadID=" + UserID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result += sdr["ID"].ToString() + ",";
                }
            }
            return (result.Length > 0) ? result.Substring(0, result.Length - 1) : result;
        }

        public Dept RetriveDeptName(int DeptID)
        {
            string strSql = "select DeptName from dept where rowstatus > -1 and ID=" + DeptID + "";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSql);
            strError = dataAccess.Error;
            //arrDept = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectDeptName(sqlDataReader);
                }
            }

            return new Dept();
        }

        private Dept GenerateObjectDeptName(SqlDataReader sdr)
        {
            objDept = new Dept();

            objDept.DeptName = sdr["DeptName"].ToString();

            return objDept;
        }

        public ArrayList GetZona()
        {
            string strSQL = "Select * from  MTC_Zona order by ID";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            ArrayList arrDept = new ArrayList();
            if (dataAccess.Error == string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDept.Add(new Plant
                    {
                        ID = Convert.ToInt32(sqlDataReader["ID"].ToString()),
                        ZonaName = sqlDataReader["ZonaName"].ToString()
                    });
                }
            }
            return arrDept;
        }

        public ArrayList RetrievePerDept7(int DeptID)
        {
            string DeptID2 = string.Empty;

            if (DeptID != 19)
            {
                DeptID2 = "19";
            }
            else
            {
                DeptID2 = DeptID.ToString();
            }

            string strSQL =
            " select A.ID,case when A.ID=18 then 'MAINTENANCE UTI' when A.ID=4 then 'MAINTENANCE MKN' when A.ID=5 then 'MAINTENANCE ELK' " +
            " when A.ID=19 then 'MAINTENANCE ENG' else A.Alias end DeptName from (select DISTINCT(DeptID)DeptID from " +
            " MTC_Project where ToDeptID=" + DeptID2 + " and RowStatus>-1) as xx INNER JOIN Dept A ON A.ID=xx.DeptID where A.RowStatus>-1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrDept = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDept.Add(GenerateObject7(sqlDataReader));
                }
            }
            else
                arrDept.Add(new Dept());

            return arrDept;
        }

        private Dept GenerateObject7(SqlDataReader sqlDataReader)
        {
            objDept = new Dept();
            objDept.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objDept.DeptName = sqlDataReader["DeptName"].ToString();
            return objDept;
        }
    }
}
