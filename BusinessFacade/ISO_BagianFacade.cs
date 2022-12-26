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
    public class ISO_BagianFacade : AbstractFacade
    {
        private ISO_Bagian objBagian = new ISO_Bagian();
        private ArrayList arrUsers;
        private List<SqlParameter> sqlListParam;


        public ISO_BagianFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objBagian = (ISO_Bagian)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@BagianName", objBagian.BagianName));
                sqlListParam.Add(new SqlParameter("@DeptID", objBagian.DeptID));
                sqlListParam.Add(new SqlParameter("@UserGroup", objBagian.UserGroupID));
                sqlListParam.Add(new SqlParameter("@PlandId", objBagian.Plant));
                sqlListParam.Add(new SqlParameter("@UserName", objBagian.UserName));
                sqlListParam.Add(new SqlParameter("@BobotKpi", objBagian.BobotKpi));
                sqlListParam.Add(new SqlParameter("@BobotSop", objBagian.BobotSop));
                sqlListParam.Add(new SqlParameter("@BobotTask", objBagian.BobotTask));
                sqlListParam.Add(new SqlParameter("@BobotDisiplin", objBagian.BobotDisiplin));
                sqlListParam.Add(new SqlParameter("@Bulan", objBagian.Bulan));
                sqlListParam.Add(new SqlParameter("@Tahun", objBagian.Tahun));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertISOBagian");

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
                objBagian = (ISO_Bagian)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objBagian.ID));
                sqlListParam.Add(new SqlParameter("@BagianName", objBagian.BagianName));
                sqlListParam.Add(new SqlParameter("@DeptID", objBagian.DeptID));
                sqlListParam.Add(new SqlParameter("@UserGroup", objBagian.UserGroupID));

                sqlListParam.Add(new SqlParameter("@PlandId", objBagian.Plant));
                sqlListParam.Add(new SqlParameter("@UserName", objBagian.UserName));
                sqlListParam.Add(new SqlParameter("@BobotKpi", objBagian.BobotKpi));
                sqlListParam.Add(new SqlParameter("@BobotSop", objBagian.BobotSop));
                sqlListParam.Add(new SqlParameter("@BobotTask", objBagian.BobotTask));
                sqlListParam.Add(new SqlParameter("@BobotDisiplin", objBagian.BobotDisiplin));
                sqlListParam.Add(new SqlParameter("@Bulan", objBagian.Bulan));
                sqlListParam.Add(new SqlParameter("@Tahun", objBagian.Tahun));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateISOBagian");

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
                objBagian = (ISO_Bagian)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objBagian.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objBagian.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteISOBagian");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select top 50 * from ISO_Bagian where RowStatus = 0");
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
                arrUsers.Add(new ISO_Bagian());

            return arrUsers;
        }

        public ISO_Bagian RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from ISO_Bagian where RowStatus = 0 and Id = " + Id);
            strError = dataAccess.Error;
            arrUsers = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new ISO_Bagian();
        }

        public ISO_Bagian RetrieveByUserID(string userId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from ISO_Bagian where RowStatus = 0 and UserId = '" + userId + "'");
            strError = dataAccess.Error;
            arrUsers = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new ISO_Bagian();
        }

        public ISO_Bagian RetrieveByUserName(string userName)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from ISO_Bagian where RowStatus = 0 and UserName = '" + userName + "'");
            strError = dataAccess.Error;
            arrUsers = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new ISO_Bagian();
        }

 

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "Select Ib.*,D.ID as dID,D.DeptName from ISO_Bagian as Ib LEFT JOIN Dept as D on Ib.DeptID=D.ID where Ib.RowStatus = 0 and " + strField + " like '%" + strValue + "%'";
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
                arrUsers.Add(new ISO_Bagian());

            return arrUsers;
        }

        public ISO_Bagian GenerateObject(SqlDataReader sqlDataReader)
        {
            objBagian = new ISO_Bagian();
            objBagian.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objBagian.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objBagian.BagianName = sqlDataReader["BagianName"].ToString();
            objBagian.Urutan = sqlDataReader["Urutan"].ToString();
            objBagian.UserGroupID = Convert.ToInt32(sqlDataReader["UserGroupID"]);
            //objBagian.Plant = Convert.ToInt32(sqlDataReader["Plant"]);
            objBagian.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);

            return objBagian;
        }
    
        /**
         * Added on 17-02-2014
         * Added by Iswan Putera
         */
        public ArrayList RetrieveByDept(int Dept)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string sqlQuery="Select Ib.*,D.ID as dID,D.DeptName from ISO_Bagian as Ib LEFT JOIN Dept as D on Ib.DeptID=D.ID where Ib.RowStatus=0 and Ib.DeptID="+Dept;
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(sqlQuery);
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
                arrUsers.Add(new ISO_Bagian());

            return arrUsers;
        }
        public ISO_Bagian GenerateObject2(SqlDataReader sqlDataReader)
        {
            objBagian = new ISO_Bagian();
            objBagian.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objBagian.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objBagian.BagianName = sqlDataReader["BagianName"].ToString();
            objBagian.DeptName = sqlDataReader["DeptName"].ToString();
            objBagian.UserGroupID = Convert.ToInt32(sqlDataReader["UserGroupID"]);
            objBagian.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);

            return objBagian;
        }
    }
}
