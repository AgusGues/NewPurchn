using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;
/**
 * Class Name : ISO_CategoryFacade
 * Created by : Iswan Putera
 * Created Date : 20-02-2014
 */

namespace BusinessFacade
{
    public class ISO_CategoryFacade:AbstractFacade
    {
        private ISO_Catagory objBagian = new ISO_Catagory();
        private ArrayList arrKat;
        private List<SqlParameter> sqlListParam;
        
        public ISO_CategoryFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objBagian = (ISO_Catagory)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Task", objBagian.Task));
                sqlListParam.Add(new SqlParameter("@Desk", objBagian.Desk));
                sqlListParam.Add(new SqlParameter("@Bobot", objBagian.Bobots));
                sqlListParam.Add(new SqlParameter("@PesType",objBagian.PesType));
                sqlListParam.Add(new SqlParameter("@DeptID",objBagian.DeptID));
                sqlListParam.Add(new SqlParameter("@Plant",objBagian.PlantID));
                sqlListParam.Add(new SqlParameter("@Target",objBagian.Target));
                sqlListParam.Add(new SqlParameter("@Checking", objBagian.Checking));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertISOCategory");

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
                objBagian = (ISO_Catagory)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objBagian.ID));
                sqlListParam.Add(new SqlParameter("@Task", objBagian.Task));
                sqlListParam.Add(new SqlParameter("@Desk", objBagian.Desk));
                sqlListParam.Add(new SqlParameter("@Target", objBagian.Target));
                sqlListParam.Add(new SqlParameter("@RowStatus", objBagian.RowStatus));
                sqlListParam.Add(new SqlParameter("@Checking", objBagian.Checking));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateISOCategory");

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
            throw new NotImplementedException();
        }
        /**
         * added on 25-03-2015
         * added by Iswan Putera
         * PES System
         */
        public string Criteria { get; set; }
        public string OrderBy { get; set; }
        public override ArrayList Retrieve()
        {
            string strSQL = "Select  * from ISO_Category where RowStatus >-1 "+ this.Criteria+" "+this.OrderBy;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrKat = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrKat.Add(GenerateObject(sqlDataReader));
                }
            }
            

            return arrKat;
        }
        public ISO_Catagory RetrieveDetail()
        {
            string strSQL = "Select  * from ISO_Category where RowStatus >-1 " + this.Criteria + " " + this.OrderBy;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            objBagian = new ISO_Catagory();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return (GenerateObject(sqlDataReader));
                }
            }

            return objBagian;
        }
        public ISO_Catagory GenerateObject(SqlDataReader sqlDataReader)
        {
            objBagian = new ISO_Catagory();
            objBagian.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objBagian.Task = sqlDataReader["Category"].ToString();
            objBagian.Desk = sqlDataReader["Description"].ToString();
            objBagian.Bobots = Convert.ToInt32(sqlDataReader["Bobot"]);
            objBagian.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objBagian.DeptID = Convert.ToInt32(sqlDataReader["DeptID"].ToString());
            objBagian.PesType = Convert.ToInt32(sqlDataReader["PesType"].ToString());
            objBagian.Target = sqlDataReader["Target"].ToString();
            objBagian.Checking = sqlDataReader["Checking"].ToString();
            return objBagian;
        }
        public ArrayList RetrieveUC()
        {
            arrKat = new ArrayList();
            string strSQL = "Select su.UserName,ib.BagianName,ic.Description,ic.Category,uc.* " +
                            "from ISO_UserCategory uc " +
                            "left join ISO_Bagian ib " +
                            "on ib.ID=uc.SectionID " +
                            "left join ISO_Users su " +
                            "on su.ID=uc.UserID " +
                            "left join ISO_Category as ic " +
                            "on ic.ID=uc.CategoryID " +
                            "where uc.RowStatus >-1 " + this.Criteria + " " + this.OrderBy;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrKat.Add(new ISO_Catagory
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        CategoryID = Convert.ToInt32(sdr["CategoryID"].ToString()),
                        SectionID = Convert.ToInt32(sdr["SectionID"].ToString()),
                        Bobot = Convert.ToDecimal(sdr["Bobot"].ToString()),
                        PesType = Convert.ToInt32(sdr["PesType"].ToString()),
                        TypeBobot = sdr["TypeBobot"].ToString(),
                        RowStatus = Convert.ToInt32(sdr["RowStatus"].ToString()),
                        UserID = Convert.ToInt32(sdr["UserID"].ToString()),
                        Desk = sdr["Description"].ToString(),
                        Task = sdr["Category"].ToString(),
                        CreatedBy = sdr["UserName"].ToString(),
                        BagianName = sdr["BagianName"].ToString(),
                        Tahun = (sdr["Tahun"] != DBNull.Value) ? Convert.ToInt32(sdr["Tahun"].ToString()) : 0,
                        Bulan = (sdr["Bulan"] != DBNull.Value) ? Convert.ToInt32(sdr["Bulan"].ToString()) : 0
                    });
                }
            }
            return arrKat;
        }
    /** end if class */
    }
/** end of namespace
 * file location :../BussinesFacade/ISO_CategoryFacade.cs
 */
}
public class Cat : ISO_Catagory
{
   
}
