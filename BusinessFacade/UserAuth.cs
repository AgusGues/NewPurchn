using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer;
using Domain;

namespace BusinessFacade
{
    public class UserAuth
    {
        public string Criteria { get; set; }
        public string Option { get; set; }
        public string Field { get; set; }
        private ArrayList arrData = new ArrayList();
        private Auth objData = new Auth();
        private string Query()
        {
            string query = string.Empty;
            switch (this.Option)
            {
                case "All":
                    query = "Select ID,Userid,ISNULL(input,0)Inputed,ISNULL(viewed,0)Viewed,ISNULL(Edit,0)Edited,ISNULL(Price,0)Priced, " +
                          "ISNULL(Printed,0)Printed,DeptID,ModulName,CreatedBy,CreatedTime,ModifiedBy,ModifiedTime,RowStatus " +
                          "from UserDeptAuth where RowStatus >-1 " + this.Criteria;
                    break;
            }
            return query;
        }
        private Auth gObject(SqlDataReader sdr)
        {
            objData = new Auth();
            switch (this.Option)
            {
                case "All":
                    objData.ID = Convert.ToInt32(sdr["ID"].ToString());
                    objData.View = Convert.ToInt32(sdr["Viewed"].ToString());
                    objData.Edit = Convert.ToInt32(sdr["Edited"].ToString());
                    objData.Input = Convert.ToInt32(sdr["Inputed"].ToString());
                    objData.Price = Convert.ToInt32(sdr["Priced"].ToString());
                    objData.Print = Convert.ToInt32(sdr["Printed"].ToString());
                    objData.AuthDept = sdr["DeptID"].ToString();
                    objData.Modul = sdr["ModulName"].ToString();
                    break;
            }
            return objData;
        }
        public ArrayList Retrieve()
        {
            arrData = new ArrayList();
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(this.Query());
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(gObject(sdr));
                }
            }
            return arrData;
        }
        public Auth Retrieve(bool detail)
        {
            objData = new Auth();
            string strsql = this.Query();
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strsql);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    objData = gObject(sdr);
                }
            }
            return objData;
        }

    }

    public class Auth : GRCBaseDomain
    {
        public int View { get; set; }
        public int Edit { get; set; }
        public int Input { get; set; }
        public int Price { get; set; }
        public int Print { get; set; }
        public string Modul { get; set; }
        public string AuthDept { get; set; }
    }
}
