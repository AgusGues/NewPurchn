using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;

namespace BusinessFacade
{
    //class ISO_UserFacade
    //{
    //}
    public class ISO_UserFacade : AbstractFacade
    {
        private ISO_Users objUsers = new ISO_Users();
        private ArrayList arrUsers;
        private List<SqlParameter> sqlListParam;

        DataAccess dataAccess = new DataAccess(Global.ConnectionString());

        public ISO_UserFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objUsers = (ISO_Users)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@UserID", objUsers.UserID));
                sqlListParam.Add(new SqlParameter("@UserName", objUsers.UserName));
                sqlListParam.Add(new SqlParameter("@Password", objUsers.Password));
                sqlListParam.Add(new SqlParameter("@TypeUnitKerja", objUsers.TypeUnitKerja));
                sqlListParam.Add(new SqlParameter("@UnitKerjaID", objUsers.UnitKerjaID));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objUsers.CreatedBy));
                sqlListParam.Add(new SqlParameter("@CompanyID", objUsers.CompanyID));
                sqlListParam.Add(new SqlParameter("@PlantID", objUsers.Plant));
                sqlListParam.Add(new SqlParameter("@BagianID", objUsers.bagian));
                sqlListParam.Add(new SqlParameter("@DeptID", objUsers.DeptID));
                sqlListParam.Add(new SqlParameter("@NIK", objUsers.NIK));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertISOUsers1");

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
                objUsers = (ISO_Users)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@UserID", objUsers.UserID));
                sqlListParam.Add(new SqlParameter("@UserName", objUsers.UserName));
                sqlListParam.Add(new SqlParameter("@Password", objUsers.Password));
                sqlListParam.Add(new SqlParameter("@TypeUnitKerja", objUsers.TypeUnitKerja));
                sqlListParam.Add(new SqlParameter("@UnitKerjaID", objUsers.UnitKerjaID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUsers.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@CompanyID", objUsers.CompanyID));
                sqlListParam.Add(new SqlParameter("@PlantID", objUsers.Plant));
                sqlListParam.Add(new SqlParameter("@BagianID", objUsers.bagian));
                sqlListParam.Add(new SqlParameter("@DeptID", objUsers.DeptID));
                sqlListParam.Add(new SqlParameter("@NIK", objUsers.NIK));
                sqlListParam.Add(new SqlParameter("@ID", objUsers.ID));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateISOUsers");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public string GetNo(string nama)
        {
            string strQuery = "select ID from ISO_Users where UserName ='" + nama + "' and RowStatus >-1";
            DataAccess dta = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = dta.RetrieveDataByString(strQuery);
            int hasil = 0;
            if (dta.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    // hasil = Convert.ToInt32(sdr["ID"].ToString());
                    hasil = Convert.ToInt32(sdr["ID"].ToString());
                }
            }

            return hasil.ToString();
        }

        public override int Delete(object objDomain)
        {

            try
            {
                objUsers = (ISO_Users)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objUsers.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUsers.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteISOUsers");

                strError = dataAccess.Error;

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
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select top 50 * from ISO_Users where RowStatus = 0");
            strError = dataAccess.Error;
            arrUsers = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUsers.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrUsers.Add(new ISO_Users());

            return arrUsers;
        }

        public ISO_Users RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from ISO_Users where RowStatus = 0 and Id = " + Id);
            strError = dataAccess.Error;
            arrUsers = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }
            return new ISO_Users();
        }

        public ISO_Users RetrieveByISOuserID(string UserID)
        {
            /**
             * created on : 13-03-2014
             * purpose    : grid view list iso user modul master iso user
             */
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "SELECT a.NIK,u.* FROM ISO_Users u, UserAccount a WHERE u.ID=a.UserID AND u.id='" + UserID + "'";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUsers = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new ISO_Users();
        }
        public ISO_Users RetrieveByUserID(string userId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "SELECT a.NIK,u.* FROM ISO_Users u, UserAccount a WHERE u.ID=a.UserID AND u.UserID = '" + userId + "'";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUsers = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new ISO_Users();
        }

        public ISO_Users RetrieveByUserName(string userName)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from ISO_Users where RowStatus = 0 and UserName = '" + userName + "'");
            strError = dataAccess.Error;
            arrUsers = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new ISO_Users();
        }

        public ISO_Users RetrieveByUserNameAndPassword(string userName, string password)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            using (SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from ISO_Users where RowStatus = 0 and UserName = '" + userName.Replace("'", string.Empty) + "' and Password = '" + password.Replace("'", string.Empty) + "'"))
            {
                strError = dataAccess.Error;
                arrUsers = new ArrayList();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        return GenerateObject(sqlDataReader);
                    }
                }
            }

            return new ISO_Users();
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            /**
             * Update on 05/03/2014
             */

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string va = (strField == "DeptID") ? " = "+ strValue : " like '%" + strValue + "%'";
            string strSQL = "Select * from vw_ISO_Users where RowStatus = 0 and " + strField + va ;
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUsers = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUsers.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrUsers.Add(new ISO_Users());

            return arrUsers;
        }

        public ISO_Users GenerateObject(SqlDataReader sqlDataReader)
        {
            objUsers = new ISO_Users();
            objUsers.NIK = sqlDataReader["NIK"].ToString();
            objUsers.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objUsers.UserID = sqlDataReader["UserID"].ToString();
            objUsers.UserName = sqlDataReader["UserName"].ToString();
            objUsers.Password = sqlDataReader["Password"].ToString();
            objUsers.TypeUnitKerja = Convert.ToInt32(sqlDataReader["TypeUnitKerja"]);
            objUsers.UnitKerjaID = Convert.ToInt32(sqlDataReader["UnitKerjaID"]);
            objUsers.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objUsers.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objUsers.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objUsers.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objUsers.LastModifiedTime = (sqlDataReader["LastModifiedTime"] == DBNull.Value) ? DateTime.MaxValue : Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            objUsers.CompanyID=Convert.ToInt32(sqlDataReader["CompanyID"]);
            objUsers.Plant = Convert.ToInt32(sqlDataReader["Plant"]);
            objUsers.bagian = Convert.ToInt32(sqlDataReader["DeptJabatanID"]);
            objUsers.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);

            return objUsers;

        }
       
        /**
         * added on 17-02-2014 by Iswan Putera
         */

        public ArrayList RetrieveForGrid()
        {
            /**
             * get List user for data grid
             */
            string strSQL = "Select * from vw_ISO_Users where RowStatus = 0 " +
                            "ORDER BY UserName";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUsers = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUsers.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrUsers.Add(new ISO_Users());

            return arrUsers;
        }
        public ArrayList RetrieveByDeptID(int DeptID)
        {
            string strSQL = "Select * from vw_ISO_Users where RowStatus = 0 " +
                            "and DeptID="+DeptID+"" +
                            "ORDER BY UserName";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUsers = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUsers.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrUsers.Add(new ISO_Users());

            return arrUsers;
        }
        public ISO_Users GenerateObject2(SqlDataReader sqlDataReader)
        {
            try
            {
                objUsers = new ISO_Users();
                objUsers.ID = Convert.ToInt32(sqlDataReader["ID"]);
                objUsers.UserID = sqlDataReader["UserID"].ToString();
                objUsers.UserName = sqlDataReader["UserName"].ToString();
                objUsers.Password = sqlDataReader["Password"].ToString();
                objUsers.TypeUnitKerja = Convert.ToInt32(sqlDataReader["TypeUnitKerja"]);
                objUsers.UnitKerjaID = Convert.ToInt32(sqlDataReader["UnitKerjaID"]);
                objUsers.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
                objUsers.CreatedBy = sqlDataReader["CreatedBy"].ToString();
                objUsers.CreatedTime = (Convert.IsDBNull(sqlDataReader["CreatedTime"])) ? DateTime.Now : Convert.ToDateTime(sqlDataReader["CreatedTime"]);
                objUsers.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
                objUsers.LastModifiedTime = (Convert.IsDBNull(sqlDataReader["LastModifiedTime"])) ? DateTime.Now : Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
                objUsers.Nom = Convert.ToInt32(sqlDataReader["Nom"]);
                objUsers.Plant = Convert.ToInt32(sqlDataReader["Plant"]);
                objUsers.CompanyID = Convert.ToInt32(sqlDataReader["CompanyID"]);
                objUsers.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
                objUsers.bagianname = sqlDataReader["BagianName"].ToString();
                objUsers.companyname = sqlDataReader["CompanyName"].ToString();
                objUsers.UnitKerjaName = sqlDataReader["UnitKerjaName"].ToString();
                objUsers.bagian = Convert.ToInt32(sqlDataReader["DeptJabatanID"]);
                objUsers.TypeUnitKerjaName = sqlDataReader["TypeUnitKerjaName"].ToString();
                objUsers.deptname = sqlDataReader["DeptName"].ToString();
            }
            catch { }
            return objUsers;
        }

        /*
         * Added on 20-04-2014
         * adopt PES System Manager input by dept masing2
         */
        public string TypePES { get; set; }
        public ArrayList RetrievePIC(string DeptID)
        {
            arrUsers = new ArrayList();
            string MgrInput = new Inifiles(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ManagerInput", "PES");
            string[] dpt = DeptID.Split(',');
            string strISO=string.Empty;
            string Manager = (dpt.Contains("25")) ? "and u.BagianName!='admin'" :
                (MgrInput == "yes") ? "" : " and u.BagianName!='Manager'  ";
            string OnlyPes = (TypePES == string.Empty || TypePES!="2") ?
                          " Select * from (Select ISOuserID as ID,(Select UserName from iso_users where id=ISOUserID)UserName,(select top 1 Item from dbo.fnSplit(DeptApp,','))DeptID "+
                          " from ISO_BagianHead) as h where h.DeptID in(N'" + DeptID + "')" +
                          " Union all " : "";
            if (DeptID=="25")
                strISO= "union all select ID,UserName,CAST(DeptID as varchar(50))DeptID from ISO_Users u where DeptID =23 and RowStatus>-1 ";

            string strSQL = "select * from ( " +
                           OnlyPes +
                          " select ID,UserName,CAST(DeptID as varchar(50))DeptID from vw_ISO_Users u where DeptID in(N'" + DeptID +"') "+
                          Manager + strISO+
                          " /*order by UserName*/" +
                          " ) as d " +
                          " where DeptID in(N'" + DeptID + "',N'23') order by Username ";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrUsers.Add(new ISO_Users
                    {
                        ID=Convert.ToInt32(sdr["ID"].ToString()),
                        UserName=sdr["UserName"].ToString(),
                        DeptID=Convert.ToInt32(sdr["DeptID"].ToString())
                    });
                }
            }
            return arrUsers;
        }
        public ArrayList RetrieveUserAccount(string DeptID)
        {
            ArrayList arrData = new ArrayList();
            string strSQL = "Select * FROM UserAccount where RowStatus>-1 and DeptID=" + DeptID + " order by UserName";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new ISO_Users
                    {
                        UserID = sdr["UserID"].ToString(),
                        UserName = sdr["UserName"].ToString(),
                        DeptID = Convert.ToInt32(sdr["DeptID"].ToString())

                    });
                }
            }
            return arrData;
        }
    }

}
