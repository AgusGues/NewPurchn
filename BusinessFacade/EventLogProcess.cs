using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using Domain;
using BusinessFacade;
using DataAccessLayer;
namespace BusinessFacade
{
    public class EventLogProcess
    {
        private ArrayList arrData = new ArrayList();
        private EventLog hlp = new EventLog();
        private List<SqlParameter> sqlListParam;
        public string Criteria { get; set; }
        private string TableName = "EventApprovalLog";
        public string Pilihan { get; set; }
        public void EventLogInsert(object Data)
        {

            string sp = this.CreateProcedure(Data, "sp" + this.TableName + "_insert");
            if (sp == string.Empty)
            {
                int rst = ProcessData(Data, "sp" + this.TableName + "_insert");
            }
            
        }
        /**
         * Simple proses store data to table
         * added on 05-05-2015
         * added by Iswan Putera
         */
        public int ProcessData(object help, string spName)
        {
            try
            {
                hlp =(EventLog) help;
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
                int result = da.ProcessData(sqlListParam, spName);
                string err = da.Error;
                return result;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return -1;
            }
        }
        public string CreateProcedure(object help,string spName)
        {
            string message = string.Empty;
            hlp = (EventLog)help;
            string[] arrCriteria = this.Criteria.Split(',');
            PropertyInfo[] data = hlp.GetType().GetProperties();
            DataAccess da = new DataAccess(Global.ConnectionString());
            string param = "";
            string value = "";
            string field = "";
            string FieldUpdate="";
            try
            {
                foreach (PropertyInfo items in data)
                {
                    if (arrCriteria.Contains(items.Name))
                    {
                        param += "@" + items.Name.ToString() + " " + GetTypeData(this.TableName, items.Name.ToString()) + ",";
                        value += "@" + items.Name.ToString() + ",";
                        field += items.Name.ToString() + ",";
                        if(items.Name.ToString()!="ID")
                        FieldUpdate += items.Name.ToString() + "=@" + items.Name.ToString() + ",";
                    }
                }
                string strSQL = "CREATE PROCEDURE " + spName + " "+ param.Substring(0, param.Length - 1) +
                                " AS BEGIN SET NOCOUNT ON; " ;
                if (this.Pilihan == "Insert")
                {
                    strSQL += "INSERT INTO " + this.TableName + " (" + field.Substring(0, field.Length - 1).ToString() + ")VALUES(" +
                     value.Substring(0, value.Length - 1) + ") SELECT @@IDENTITY as ID";
                }
                else
                {
                    strSQL += "UPDATE " + this.TableName + " set " + FieldUpdate.Substring(0, FieldUpdate.Length - 1).ToString() + " where ID=@ID SELECT @@ROWCOUNT";
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
            catch(Exception ex)
            {
                message = ex.Message;
                return message;
            }
        }
        private string GetTypeData(string TableName, string ColumName)
        {
            string result = string.Empty;
            string strsql = "select DATA_TYPE,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS IC where "+
                            "TABLE_NAME = '" + TableName + "' and COLUMN_NAME = '" + ColumName + "'";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strsql);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = sdr["DATA_TYPE"].ToString() + " ";
                    result += (sdr["CHARACTER_MAXIMUM_LENGTH"] != DBNull.Value) ? "("+ sdr["CHARACTER_MAXIMUM_LENGTH"].ToString()+")" : "";
                    if (sdr["CHARACTER_MAXIMUM_LENGTH"].ToString() == "-1")
                    {
                        result = result.Replace("-1", "Max");
                    }
                }
            }
            return result;
        }
    }
    

}

