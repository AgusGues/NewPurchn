using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;
using Dapper;

namespace BusinessFacade
{
    public class GroupsPurchnFacade : AbstractFacade
    {
        private GroupsPurchn objGroupsPurchn = new GroupsPurchn();
        private ArrayList arrGroupsPurchn;
        private List<SqlParameter> sqlListParam;
        

        public GroupsPurchnFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objGroupsPurchn = (GroupsPurchn)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@GroupCode", objGroupsPurchn.GroupCode));
                sqlListParam.Add(new SqlParameter("@GroupDescription", objGroupsPurchn.GroupDescription));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objGroupsPurchn.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@DeptID", objGroupsPurchn.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objGroupsPurchn.CreatedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertGroupsPurchn");

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
                objGroupsPurchn = (GroupsPurchn)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objGroupsPurchn.ID));
                sqlListParam.Add(new SqlParameter("@GroupCode", objGroupsPurchn.GroupCode));
                sqlListParam.Add(new SqlParameter("@GroupDescription", objGroupsPurchn.GroupDescription));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objGroupsPurchn.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@DeptID", objGroupsPurchn.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objGroupsPurchn.CreatedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateGroupsPurchn");

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
                objGroupsPurchn = (GroupsPurchn)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objGroupsPurchn.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objGroupsPurchn.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteGroups");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public string without { get; set; }
        public override ArrayList Retrieve()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.CodeID,A.GroupCode,A.GroupDescription,A.ItemTypeID,A.DeptID,B.TypeDescription,C.DeptName,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from GroupsPurchn as A,ItemTypePurchn as B,Dept as C where A.ItemTypeID = B.ID and A.DeptID = C.ID and A.RowStatus = 0" + this.without);
            strError = dataAccess.Error;
            arrGroupsPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGroupsPurchn.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGroupsPurchn.Add(new GroupsPurchn());

            return arrGroupsPurchn;
        }

        public  ArrayList RetrieveByItemTypeID(string itemtypeID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.CodeID,A.CodeID,A.GroupCode,A.GroupDescription,A.ItemTypeID,A.DeptID,B.TypeDescription,C.DeptName,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from GroupsPurchn as A,ItemTypePurchn as B,Dept as C where A.ItemTypeID = B.ID and A.DeptID = C.ID and A.RowStatus = 0 and itemtypeID=" + itemtypeID);
            strError = dataAccess.Error;
            arrGroupsPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGroupsPurchn.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGroupsPurchn.Add(new GroupsPurchn());

            return arrGroupsPurchn;
        }
        public static List<GroupsPurchn> RetrieveByItemTypeIDNew(string itemtypeID)
        {
            List<GroupsPurchn> alldata = new List<GroupsPurchn>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string strSQL = "select A.ID,A.CodeID,A.CodeID,A.GroupCode,A.GroupDescription,A.ItemTypeID,A.DeptID,B.TypeDescription,C.DeptName,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from GroupsPurchn as A,ItemTypePurchn as B,Dept as C where A.ItemTypeID = B.ID and A.DeptID = C.ID and A.RowStatus = 0 and itemtypeID=" + itemtypeID;
                    alldata = connection.Query<GroupsPurchn>(strSQL).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }
        public  ArrayList RetrieveCode()
        {
            string strSQL = "select distinct 0 as ID,A.CodeID,A.GroupCode,' ' as GroupDescription,0 as ItemTypeID,0 as DeptID,"+
                            "0 as TypeDescription,0 as RowStatus,' ' as CreatedBy,'1/1/1900' as CreatedTime,'1/1/1900' as LastModifiedBy,"+
                            "A.LastModifiedTime  from GroupsPurchn as A where  A.RowStatus = 0";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGroupsPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGroupsPurchn.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGroupsPurchn.Add(new GroupsPurchn());

            return arrGroupsPurchn;
        }

        public ArrayList RetrieveByGroupID(int deptID, int groupID, int itemTypeID)
        {
            string strGroupID = string.Empty; string query = string.Empty;
            #region
            //if (deptID == 16 && groupID == 8 && itemTypeID == 1)  //Sparepart
            //{
            //    strGroupID = " and A.ID in (8,9,6)"; //Mekanik,Elektrik,Project
            //}
            //if (deptID == 16 && groupID == 8 && itemTypeID == 2)  //Sparepart
            //{
            //    strGroupID = " and A.ID in (4)"; //Asset
            //}
            //if (deptID == 16 && groupID == 8 && itemTypeID == 3)  //Sparepart
            //{
            //    strGroupID = " and A.ID in (5)"; //Biaya
            //}
            //if (deptID == 13 && groupID == 7 && itemTypeID == 1)  //Sparepart
            //{
            //    strGroupID = " and A.ID in (7)"; //Marketing
            //}
            //if (deptID == 13 && groupID == 7 && itemTypeID == 2)  //Asset
            //{
            //    strGroupID = " and A.ID in (4)"; //Marketing
            //}
            //if (deptID == 13 && groupID == 7 && itemTypeID == 3)  //Biaya
            //{
            //    strGroupID = " and A.ID in (5)"; //Marketing
            //}
            //if (deptID == 10 && groupID == 1 && itemTypeID == 1)  //Logistik Bahan BAku
            //{
            //    strGroupID = " and A.ID in (1,2)"; //Bahan Baku dan Pembantu
            //}
            //if (deptID == 10 && groupID == 1 && itemTypeID == 2)  //Logistik Bahan BAku
            //{
            //    strGroupID = " and A.ID in (4)"; //Asset
            //}
            //if (deptID == 10 && groupID == 1 && itemTypeID == 3)  //Logistik Bahan BAku
            //{
            //    strGroupID = " and A.ID in (5)"; //Biaya
            //}

            //if (deptID == 6 && groupID == 9 && itemTypeID == 1)   // Logistik Brg Jadi
            //{
            //    strGroupID = " and A.ID in (8,9,6)";  // Sparepart
            //}
            //if (deptID == 6 && groupID == 9 && itemTypeID == 2)   // Logistik Brg Jadi
            //{
            //    strGroupID = " and A.ID in (4)";  // Asset
            //}
            //if (deptID == 6 && groupID == 9 && itemTypeID == 3)   // Logistik Brg Jadi
            //{
            //    strGroupID = " and A.ID in (5)";  // Biaya
            //}

            //if (deptID == 7 && groupID == 3 && itemTypeID == 1)   // HRD & GA
            //{
            //    strGroupID = " and A.ID in (8,9,3)";  // Sparepart & ATK
            //}
            //if (deptID == 7 && groupID == 3 && itemTypeID == 2)   // HRD & GA
            //{
            //    strGroupID = " and A.ID in (4)";  // Asset
            //}
            //if (deptID == 7 && groupID == 3 && itemTypeID == 3)   // HRD & GA
            //{
            //    strGroupID = " and A.ID in (5)";  // Biaya
            //}


            //if (deptID == 4 && groupID == 9 && itemTypeID == 1)  // Maintenance Mekanik
            //{
            //    strGroupID = " and A.ID in (8,9,6)";  // sparepart project
            //}
            //if (deptID == 4 && groupID == 9 && itemTypeID == 2)  // Maintenance Mekanik
            //{
            //    strGroupID = " and A.ID in (4)";  // Asset
            //}
            //if (deptID == 4 && groupID == 9 && itemTypeID == 3)  // Maintenance Mekanik
            //{
            //    strGroupID = " and A.ID in (5)";  // Biaya
            //}

            //if (deptID == 5 && groupID == 8 && itemTypeID == 1)  // Maintenance Elektrik
            //{
            //    strGroupID = " and A.ID in (8,9,6)";  // sparepart project
            //}
            //if (deptID == 5 && groupID == 8 && itemTypeID == 2)  // Maintenance Elektrik
            //{
            //    strGroupID = " and A.ID in (4)";  // Asset
            //}
            //if (deptID == 5 && groupID == 8 && itemTypeID == 3)  // Maintenance Elektrik
            //{
            //    strGroupID = " and A.ID in (5)";  // Biaya
            //}


            //if (deptID == 19 && groupID == 8 && itemTypeID == 1)  // Maintenance Engineering
            //{
            //    strGroupID = " and A.ID in (8,9,6)";  // sparepart project
            //}
            //if (deptID == 19 && groupID == 8 && itemTypeID == 2)  // Maintenance Engineering
            //{
            //    strGroupID = " and A.ID in (4)";  // Asset
            //}
            //if (deptID == 19 && groupID == 8 && itemTypeID == 3)  // Maintenance Engineering
            //{
            //    strGroupID = " and A.ID in (5)";  // Biaya
            //}

            //if (deptID == 18 && groupID == 8 && itemTypeID == 1)  // Maintenance Utility
            //{
            //    strGroupID = " and A.ID in (8,9,6)";  // sparepart project
            //}
            //if (deptID == 18 && groupID == 8 && itemTypeID == 2)  // Maintenance Utility
            //{
            //    strGroupID = " and A.ID in (4)";  // Asset
            //}
            //if (deptID == 18 && groupID == 8 && itemTypeID == 3)  // Maintenance Utility
            //{
            //    strGroupID = " and A.ID in (5)";  // Biaya
            //}

            //if (deptID == 3 && groupID == 9 && itemTypeID == 1)  // Finishing
            //{
            //    strGroupID = " and A.ID in (8,9,6)"; // Sparepart project
            //}
            //if (deptID == 3 && groupID == 9 && itemTypeID == 2)  // Finishing
            //{
            //    strGroupID = " and A.ID in (4)"; // Asset
            //}
            //if (deptID == 3 && groupID == 9 && itemTypeID == 3)  // Finishing
            //{
            //    strGroupID = " and A.ID in (5)"; // Biaya
            //}

            //if (deptID == 2 && groupID == 8 && itemTypeID == 1)  // PRODUKSI-PROSES
            //{
            //    strGroupID = " and A.ID in (8,9)"; // Sparepart 
            //}
            //if (deptID == 2 && groupID == 8 && itemTypeID == 2)  // PRODUKSI-PROSES
            //{
            //    strGroupID = " and A.ID in (4)"; // Asset
            //}
            //if (deptID == 2 && groupID == 8 && itemTypeID == 3)  // PRODUKSI-PROSES
            //{
            //    strGroupID = " and A.ID in (5)"; // Biaya
            //}

            //if (deptID == 14 && groupID == 8 && itemTypeID == 1)   // EDP
            //{
            //    strGroupID = " and A.ID in (3,8,9)"; // ATK,Elektrik,Mekanik
            //}
            //if (deptID == 14 && groupID == 8 && itemTypeID == 2)   // EDP
            //{
            //    strGroupID = " and A.ID in (4)"; // Asset
            //}
            //if (deptID == 14 && groupID == 8 && itemTypeID == 3)   // EDP
            //{
            //    strGroupID = " and A.ID in (5)"; // Biaya
            //}
           
            //if (deptID == 9 && groupID == 8 && itemTypeID == 1)   // QA KURNIA
            //{
            //    strGroupID = " and A.ID in (8,9)"; // ATK,Elektrik,Mekanik
            //}
            //if (deptID == 9 && groupID == 8 && itemTypeID == 2)   // QA KURNIA
            //{
            //    strGroupID = " and A.ID in (4)"; // Asset
            //}
            //if (deptID == 9 && groupID == 8 && itemTypeID == 3)   // QA KURNIA
            //{
            //    strGroupID = " and A.ID in (5)"; // Biaya
            //}

            //if (deptID == 17 && groupID == 8 && itemTypeID == 1)   // QA TRIWAHYU
            //{
            //    strGroupID = " and A.ID in (8,9)"; // ATK,Elektrik,Mekanik
            //}
            //if (deptID == 17 && groupID == 8 && itemTypeID == 2)   // QA TRIWAHYU
            //{
            //    strGroupID = " and A.ID in (4)"; // Asset
            //}
            //if (deptID == 17 && groupID == 8 && itemTypeID == 3)   // QA TRIWAHYU
            //{
            //    strGroupID = " and A.ID in (5)"; // Biaya
            //}
            #endregion
            if (itemTypeID == 2)
            {
                if (deptID == 19 || deptID == 22 || deptID == 30)
                {
                    strGroupID = " and A.itemtypeID=" + itemTypeID + " and A.ID in (4,12) ";
                }
                else
                {
                    strGroupID = " and A.itemtypeID=" + itemTypeID + " and A.ID in (4) ";
                }
                //strGroupID = " and A.itemtypeID=" + itemTypeID + " and A.ID in (4) ";
            }
            else
            {
                strGroupID = " and A.itemtypeID=" + itemTypeID;
            }
            string strSQL = string.Empty;
            strSQL = "select A.ID,A.CodeID,A.GroupCode,A.GroupDescription,A.ItemTypeID,A.DeptID,B.TypeDescription,C.DeptName,A.RowStatus,A.CreatedBy,"+
                "A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from GroupsPurchn as A,ItemTypePurchn as B,Dept as C where A.ItemTypeID = B.ID and "+
                "A.DeptID = C.ID and A.RowStatus >-1 " + strGroupID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGroupsPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGroupsPurchn.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGroupsPurchn.Add(new GroupsPurchn());

            return arrGroupsPurchn;
        }
        public ArrayList RetrieveByGroupID(int deptID, int groupID, string itemTypeID, bool withasset)
        {
            string strGroupID = string.Empty;

            strGroupID = " and A.itemtypeID in(" + itemTypeID + ")";
            string strSQL = string.Empty;
            strSQL = "select A.ID,A.CodeID,A.GroupCode,A.GroupDescription,A.ItemTypeID,A.DeptID,B.TypeDescription,C.DeptName,A.RowStatus,A.CreatedBy," +
                "A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from GroupsPurchn as A,ItemTypePurchn as B,Dept as C where A.ItemTypeID = B.ID and " +
                "A.DeptID = C.ID and A.RowStatus >-1 " + strGroupID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGroupsPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGroupsPurchn.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGroupsPurchn.Add(new GroupsPurchn());

            return arrGroupsPurchn;
        }
        public GroupsPurchn RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.CodeID,A.GroupCode,A.GroupDescription,A.ItemTypeID,A.DeptID,B.TypeDescription,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from GroupsPurchn as A,ItemTypePurchn as B where A.ItemTypeID = B.ID and A.RowStatus = 0 and A.ID = " + Id);
            strError = dataAccess.Error;
            arrGroupsPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new GroupsPurchn();
        }

        public GroupsPurchn RetrieveByCode(string groupsCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.CodeID,A.GroupCode,A.GroupDescription,A.ItemTypeID,A.DeptID,B.TypeDescription,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from GroupsPurchn as A,ItemTypePurchn as B where A.ItemTypeID = B.ID and A.RowStatus = 0 and A.GroupCode = '" + groupsCode + "'");
            strError = dataAccess.Error;
            arrGroupsPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new GroupsPurchn();
        }


        public GroupsPurchn RetrieveByName(string groupsName)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.CodeID,A.GroupCode,A.GroupDescription,A.ItemTypeID,A.DeptID,B.TypeDescription,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from GroupsPurchn as A,ItemTypePurchn as B where A.ItemTypeID = B.ID and A.RowStatus = 0 and A.GroupDescription = '" + groupsName + "'");
            strError = dataAccess.Error;
            arrGroupsPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new GroupsPurchn();
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.CodeID,A.GroupCode,A.GroupDescription,A.ItemTypeID,A.DeptID,B.TypeDescription,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from GroupsPurchn as A,ItemTypePurchn as B where A.ItemTypeID = B.ID and A.RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrGroupsPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGroupsPurchn.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGroupsPurchn.Add(new GroupsPurchn());

            return arrGroupsPurchn;
        }


        public string GetKodeSPP(int id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.CodeID,A.GroupCode as groupCode from GroupsPurchn as A where ID = " + id);
            
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return sqlDataReader["groupCode"].ToString();
                }
            }

            return string.Empty;
        }

        public GroupsPurchn GenerateObject(SqlDataReader sqlDataReader)
        {
            objGroupsPurchn = new GroupsPurchn();
            objGroupsPurchn.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objGroupsPurchn.GroupCode = sqlDataReader["GroupCode"].ToString();
            objGroupsPurchn.GroupDescription = sqlDataReader["GroupDescription"].ToString();
            objGroupsPurchn.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"].ToString());
            objGroupsPurchn.DeptID = Convert.ToInt32(sqlDataReader["DeptID"].ToString());
            objGroupsPurchn.TypeDescription = sqlDataReader["TypeDescription"].ToString();
            objGroupsPurchn.CodeID = sqlDataReader["CodeID"].ToString();

            //objGroupsPurchn.DeptName = sqlDataReader["DeptName"].ToString();
            objGroupsPurchn.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objGroupsPurchn.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objGroupsPurchn.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objGroupsPurchn.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objGroupsPurchn.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objGroupsPurchn;
        }
    }
}
