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
    public class ISO_SOPScoreFacade : AbstractFacade
    {
        private ISO_SOPScore objSOPScore = new ISO_SOPScore();
        private ArrayList arrUsers;
        private List<SqlParameter> sqlListParam;


        public ISO_SOPScoreFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objSOPScore = (ISO_SOPScore)objDomain;
                sqlListParam = new List<SqlParameter>();
                //sqlListParam.Add(new SqlParameter("@UserID", objSOPScore.UserID));
                //sqlListParam.Add(new SqlParameter("@UserName", objSOPScore.UserName));
                //sqlListParam.Add(new SqlParameter("@Password", objSOPScore.Password));
                //sqlListParam.Add(new SqlParameter("@TypeUnitKerja", objSOPScore.TypeUnitKerja));
                //sqlListParam.Add(new SqlParameter("@UnitKerjaID", objSOPScore.UnitKerjaID));
                //sqlListParam.Add(new SqlParameter("@CreatedBy", objSOPScore.CreatedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertISOSopScore");

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
                objSOPScore = (ISO_SOPScore)objDomain;
                sqlListParam = new List<SqlParameter>();
                //sqlListParam.Add(new SqlParameter("@ID", objSOPScore.ID));
                //sqlListParam.Add(new SqlParameter("@UserID", objSOPScore.UserID));
                //sqlListParam.Add(new SqlParameter("@UserName", objSOPScore.UserName));
                //sqlListParam.Add(new SqlParameter("@Password", objSOPScore.Password));
                //sqlListParam.Add(new SqlParameter("@TypeUnitKerja", objSOPScore.TypeUnitKerja));
                //sqlListParam.Add(new SqlParameter("@UnitKerjaID", objSOPScore.UnitKerjaID));
                //sqlListParam.Add(new SqlParameter("@LastModifiedBy", objSOPScore.CreatedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateISOSopScore");

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
                objSOPScore = (ISO_SOPScore)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objSOPScore.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objSOPScore.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteISOSopScore");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select top 50 * from ISO_SOPScore where RowStatus = 0");
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
                arrUsers.Add(new ISO_SOPScore());

            return arrUsers;
        }

        public ISO_SOPScore RetrieveByID(int id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from ISO_SOPScore where RowStatus > -1 and id= " + id);
            strError = dataAccess.Error;
            arrUsers = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new ISO_SOPScore();
        }
        public ArrayList RetrieveByCategoryID(int catID)
        {
            string strSQL = "Select * from ISO_SOPScore where RowStatus > -1 and CategoryID= " + catID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
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
                arrUsers.Add(new ISO_SOPScore());

            return arrUsers;
        }

        public ISO_SOPScore RetrieveByUserName(string userName)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from ISO_SOPScore where RowStatus = 0 and UserName = '" + userName + "'");
            strError = dataAccess.Error;
            arrUsers = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new ISO_SOPScore();
        }

        //public ISO_SOPScore RetrieveByUserNameAndPassword(string userName, string password)
        //{
        //    using (SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from ISO_SOPScore where RowStatus = 0 and UserName = '" + userName.Replace("'", string.Empty) + "' and Password = '" + password.Replace("'", string.Empty) + "'"))
        //    {
        //        strError = dataAccess.Error;
        //        arrUsers = new ArrayList();

        //        if (sqlDataReader.HasRows)
        //        {
        //            while (sqlDataReader.Read())
        //            {
        //                return GenerateObject(sqlDataReader);
        //            }
        //        }
        //    }

        //    return new ISO_SOPScore();
        //}

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from ISO_SOPScore where RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
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
                arrUsers.Add(new ISO_SOPScore());

            return arrUsers;
        }

        public ISO_SOPScore GenerateObject(SqlDataReader sqlDataReader)
        {
            objSOPScore = new ISO_SOPScore();
            objSOPScore.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objSOPScore.CategoryID = Convert.ToInt32(sqlDataReader["CategoryID"]);
            objSOPScore.PesType = Convert.ToInt32(sqlDataReader["PesType"]);
            objSOPScore.TargetKe = sqlDataReader["TargetKe"].ToString();
            objSOPScore.PointNilai = Convert.ToDecimal(sqlDataReader["PointNilai"]);
            objSOPScore.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);

            return objSOPScore;
        }


    }
}
