using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using BusinessFacade;
using DataAccessLayer;
using Domain;
namespace BusinessFacade
{
    public enum TypeAuth { INPUT, UPDATE, VIEW, VIEWPRICE, PRINT, ALL }
    public enum Operation {INPUT,SELECT, UPDATE, DELETE,CUSTOM }
    public class ZetroLib
    {
        //Proces data with sp
        private bool returnid = false;
        public Object hlp { get; set; }
        public string Criteria { get; set; }
        public string Option { get; set; }
        private List<SqlParameter> sqlListParam;
        public string TableName { get; set; }
        public string Key { get; set; }
        public string Where { get; set; }
        public string Field{get;set;}
        public string StoreProcedurName { get; set; }
        public bool ReturnID { get { return returnid; } set { returnid = value; } }
        public int ProcessData()
        {
            try
            {
                string[] arrCriteria = this.Criteria.Split(',');
                PropertyInfo[] data = hlp.GetType().GetProperties();
                DataAccess da = new DataAccess(Global.ConnectionString());
                var equ = new List<string>();
                sqlListParam = new List<SqlParameter>();
                foreach (PropertyInfo items in data)
                {
                    if (items.GetValue(hlp, null) != null && arrCriteria.Contains(items.Name))
                    {
                        sqlListParam.Add(new SqlParameter("@" + items.Name.ToString(), items.GetValue(hlp, null)));
                    }

                }
                //if (ReturnID == true)
                //{
                //    SqlParameter param=new SqlParameter("@ID", SqlDbType.Int);
                //    param.Direction=ParameterDirection.Output;
                //    sqlListParam.Add(param);
                    
                //}
                int result = da.ProcessData(sqlListParam, this.StoreProcedurName);
                string err = da.Error;
                return result;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return -1;
            }
        }
        public string CreateProcedure()
        {
            string message = string.Empty;
            string[] arrCriteria = this.Criteria.Split(',');
            PropertyInfo[] data = hlp.GetType().GetProperties();
            DataAccess da = new DataAccess(Global.ConnectionString());
            string param = "";
            string value = "";
            string field = "";
            string FieldUpdate = "";
            try
            {
                foreach (PropertyInfo items in data)
                {
                    if (arrCriteria.Contains(items.Name))
                    {
                        
                        param += "@" + items.Name.ToString() + " " + GetTypeData(this.TableName, items.Name.ToString()) + ",";
                        value += (items.Name.ToString() == "CreatedTime" || items.Name.ToString() == "LastModifiedTime") ? 
                                "GETDATE()," :"@" + items.Name.ToString() + ",";
                        field += items.Name.ToString() + ",";
                        if (items.Name.ToString() != "ID")
                            FieldUpdate += items.Name.ToString() + "=@" + items.Name.ToString() + ",";
                    }
                    
                }
                param += (ReturnID == true) ? "@ID int Output," : "";
                string strSQL = "CREATE PROCEDURE " + this.StoreProcedurName + " " + param.Substring(0, param.Length - 1) +
                                " AS BEGIN SET NOCOUNT ON; ";
                if (this.Option == "Insert")
                {
                    strSQL += "INSERT INTO " + this.TableName + " (" + field.Substring(0, field.Length - 1).ToString() + ")VALUES(" +
                     value.Substring(0, value.Length - 1) + ") ";
                    strSQL += (ReturnID == true) ? "SET @ID=SCOPE_IDENTITY()" : "SELECT @@IDENTITY as ID";
                }
                else if (this.Option == "Update")
                {
                    strSQL += "UPDATE " + this.TableName + " set " + FieldUpdate.Substring(0, FieldUpdate.Length - 1).ToString() +
                            " where ID=@ID SELECT @@ROWCOUNT";
                }
                else if (this.Option == "UpdateWithHeaderID")
                {
                    FieldUpdate.Replace(this.Key + "=@" + this.Key + ",", "");
                    strSQL += "UPDATE " + this.TableName + " set " + FieldUpdate.Substring(0, FieldUpdate.Length - 1).ToString() +
                            " where " + this.Key + "=@" + this.Key + " SELECT @@ROWCOUNT";
                }
                strSQL += " END";
                SqlDataReader result = da.RetrieveDataByString(strSQL);
                if (result != null)
                {
                    message = string.Empty;
                }
                else
                {
                    message = "";
                }
                return message;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return message;
            }
        }
        private string GetTypeData(string TableName, string ColumName)
        {
            string result = string.Empty;
            string strsql = "select DATA_TYPE,CHARACTER_MAXIMUM_LENGTH,NUMERIC_PRECISION,NUMERIC_SCALE from INFORMATION_SCHEMA.COLUMNS IC where " +
                            "TABLE_NAME = '" + TableName + "' and COLUMN_NAME = '" + ColumName + "'";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strsql);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = sdr["DATA_TYPE"].ToString() + " ";
                    result += (sdr["CHARACTER_MAXIMUM_LENGTH"] != DBNull.Value) ? "(" + sdr["CHARACTER_MAXIMUM_LENGTH"].ToString() + ")" : "";
                    result += (sdr["DATA_TYPE"].ToString() == "decimal") ? "(" + sdr["NUMERIC_PRECISION"].ToString() + "," + sdr["NUMERIC_SCALE"] + ")" : "";
                    if (sdr["CHARACTER_MAXIMUM_LENGTH"].ToString() == "-1")
                    {
                        result = result.Replace("-1", "Max");
                    }
                }
            }
            return result;
        }
        public Users UserAccount(int UserID)
        {
            int Userse = (UserID == 314) ? 314 : UserID;
            Users user = new Users();
            string query = "Select  top 1 * from UserAccount where RowStatus>-2 And UserID=" +
                           "(select Top 1 ID from ISO_Users where UserID=" + Userse + ")";
            ZetroView zw=new ZetroView();
            zw.CustomQuery=query;
            zw.QueryType=Operation.CUSTOM;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows && sdr != null)
            {
                while (sdr.Read())
                {
                    user.UserName = sdr["UserName"].ToString();
                    user.DeptID = int.Parse(sdr["DeptID"].ToString());
                    user.BagianID = int.Parse(sdr["BagianID"].ToString());
                    user.UserID = sdr["UserID"].ToString();
                }
            }
            return user;
        }
        public int DeleteRecords(int RecordID)
        {
            string TableName = this.TableName;
            DataAccess da = new DataAccess(Global.ConnectionString());
            string strSQL = "DECLARE @max INT " +
                           "DELETE FROM " + TableName + " WHERE ID=" + RecordID +
                           "SELECT @max = max([ID])FROM[" + TableName + "] " +
                           " IF @max IS NUll  " +  //check when max is returned as null
                           "   SET @max = 0 " +
                           " DBCC CHECKIDENT ('[" + TableName + "]', RESEED, @max)";
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            return sdr.RecordsAffected;
        }
    }
    public class ZetroView:ZetroLib
    {
        ArrayList arrData = new ArrayList();
        public string CustomQuery { get; set; }
        public Operation QueryType { get; set; }
        private string Query()
        {
            string query = "";
            switch (QueryType)
            {
                case Operation.SELECT:
                    query = "select " + this.Field + " from " + this.TableName + " " + this.Where + " " + this.Criteria;
                    break;
                case Operation.UPDATE:
                    break;
                case Operation.DELETE:
                    break;
                case Operation.CUSTOM:
                    query = this.CustomQuery;
                    break;
               
            }
            return query;
        }

        public SqlDataReader Retrieve()
        {
            DataAccess da = new DataAccess(Global.ConnectionString());

            SqlDataReader sdr = da.RetrieveDataByString(this.Query());
            return sdr;

        }
    }
    public class ZetroAuth : ZetroView
    {
        private int UserID { get; set; }
        public int DeptID { get; set; }
        private string ModulName { get; set; }
        private TypeAuth Authorize { get; set; }
        private bool GetAuth()
        {
            bool tp = false;
            string Fld="*";
            ZetroView zv = new ZetroView();
            zv.TableName = "UserDeptAuth";
            zv.QueryType = Operation.SELECT;
            switch (Authorize)
            {
                case TypeAuth.INPUT: zv.Field = "Input"; Fld = "Input"; break;
                case TypeAuth.UPDATE: zv.Field = "Edit"; Fld = "Edit"; break;
                case TypeAuth.VIEW: zv.Field = "Viewed"; Fld = "Viewed"; break;
                case TypeAuth.PRINT: zv.Field = "Printed"; Fld = "Printed"; break;
                case TypeAuth.ALL: zv.Field = "*"; Fld = "Input,Edit,Viewed,Printed"; break;
            }
            zv.Where = "Where UserID=" + this.UserID;
            zv.Where += " and ModulName='" + this.ModulName + "'";
            SqlDataReader sdr = zv.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        string[] str = Fld.Split(',');
                        int auth = 0;
                        for (int i = 0; i < str.Count(); i++)
                        {
                            auth += int.Parse(sdr[str[i]].ToString());
                            if (auth == 1 || auth == 4)
                            {
                                tp = true;
                            }
                        }
                    }
                }
            }
            return tp;
        }
        public bool UserAuth(TypeAuth Authorize, int UserID, string ModulName)
        {
            bool auth = false;
            this.Authorize = Authorize;
            this.UserID = UserID;
            this.ModulName = ModulName;
            auth = GetAuth();
            return auth;
        }
    }
}
