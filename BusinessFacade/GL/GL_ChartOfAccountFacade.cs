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
    public class GL_ChartOfAccountFacade : AbstractFacade
    {
        private GL_ChartofAccount  objGL_ChartofAccount = new GL_ChartofAccount();
        private ArrayList arrGL_ChartofAccount;
        private List<SqlParameter> sqlListParam;

        public GL_ChartOfAccountFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objGL_ChartofAccount = (GL_ChartofAccount)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ChartNo", objGL_ChartofAccount.ChartNo));
                sqlListParam.Add(new SqlParameter("@ChartName", objGL_ChartofAccount.ChartName));
                sqlListParam.Add(new SqlParameter("@Group", objGL_ChartofAccount.Group));
                sqlListParam.Add(new SqlParameter("@Postable", objGL_ChartofAccount.Postable));
                sqlListParam.Add(new SqlParameter("@CCYCode", objGL_ChartofAccount.CCYCode));
                sqlListParam.Add(new SqlParameter("@Level", objGL_ChartofAccount.Level));
                sqlListParam.Add(new SqlParameter("@Parent", objGL_ChartofAccount.Parent));
                sqlListParam.Add(new SqlParameter("@IsDept", objGL_ChartofAccount.IsDept));
                sqlListParam.Add(new SqlParameter("@IsCost", objGL_ChartofAccount.IsCost));
                sqlListParam.Add(new SqlParameter("@ChartType", objGL_ChartofAccount.ChartType));
                sqlListParam.Add(new SqlParameter("@NotesNo", objGL_ChartofAccount.NotesNo));
                sqlListParam.Add(new SqlParameter("@RowStatus", objGL_ChartofAccount.RowStatus));
                sqlListParam.Add(new SqlParameter("@CompanyCode", objGL_ChartofAccount.CompanyCode));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objGL_ChartofAccount.CreatedBy));

                //int intResult = dataAccess.ProcessData(sqlListParam, "spInsertGL_ChartofAccount");
                int intResult = dataAccess.ProcessData(sqlListParam, "GLInsert_ChartOfAccount");
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
            return 0;
        }

        public override int Delete(object objDomain)
        {

            try
            {
                objGL_ChartofAccount = (GL_ChartofAccount)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objGL_ChartofAccount.ID));
                

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteGL_ChartofAccount");

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
            string strSQL = "Select * from GL_ChartofAccount where RowStatus >-1 order by chartno sengaja bikin error dulu add companyCode";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGL_ChartofAccount = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_ChartofAccount.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_ChartofAccount.Add(new GL_ChartofAccount());
            return arrGL_ChartofAccount;
        }
        public ArrayList RetrieveById(int ID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from GL_ChartofAccount where ID = " + ID);
            strError = dataAccess.Error;
            arrGL_ChartofAccount = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_ChartofAccount.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_ChartofAccount.Add(new GL_ChartofAccount());

            return arrGL_ChartofAccount;
        }
        public GL_ChartofAccount RetrieveByNo(string strValue, string companyCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from GL_ChartofAccount where RowStatus >-1  and ChartNo='"  + strValue + "' and CompanyCode='"+companyCode+"'");
            strError = dataAccess.Error;
            arrGL_ChartofAccount = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new GL_ChartofAccount();
        }
        public ArrayList RetrieveByNo2(string strValue, string companyCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from GL_ChartofAccount where RowStatus >-1  and ChartNo like '%" + strValue + "%' and CompanyCode='" + companyCode + "'");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from GL_ChartofAccount where RowStatus >-1  and ChartNo like '%" + strValue + "%' and CompanyCode='" + companyCode + "' ORDER BY ChartNo ASC");
            strError = dataAccess.Error;
            arrGL_ChartofAccount = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_ChartofAccount.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_ChartofAccount.Add(new GL_ChartofAccount());

            return arrGL_ChartofAccount;
        }
        public GL_ChartofAccount RetrieveByParent(string parentChartNo, string companyCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from GL_ChartofAccount where RowStatus >-1 and Postable=0 and ChartNo like '%" + parentChartNo + "%' and CompanyCode='" + companyCode + "'");
            strError = dataAccess.Error;
            arrGL_ChartofAccount = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new GL_ChartofAccount();
        }
        public ArrayList RetrieveByLevel(string Level, string companyCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from GL_ChartofAccount where RowStatus >-1  and [Level]='" + Level + "' and CompanyCode='"+companyCode+"'");
            strError = dataAccess.Error;
            arrGL_ChartofAccount = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_ChartofAccount.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_ChartofAccount.Add(new GL_ChartofAccount());

            return arrGL_ChartofAccount;
        }
        public ArrayList RetrieveByPostableLevel(int group, int level, string companyCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from GL_ChartofAccount where RowStatus >-1 and Postable=" + 0 + " and [Level]='" + level + "' and CompanyCode='" + companyCode + "'");
            strError = dataAccess.Error;
            arrGL_ChartofAccount = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_ChartofAccount.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_ChartofAccount.Add(new GL_ChartofAccount());

            return arrGL_ChartofAccount;
        }
        public ArrayList RetrieveByCompanyCode(string companyCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from GL_ChartofAccount where RowStatus >-1  and CompanyCode='" + companyCode + "' order by ChartNo");
            strError = dataAccess.Error;
            arrGL_ChartofAccount = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_ChartofAccount.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_ChartofAccount.Add(new GL_ChartofAccount());

            return arrGL_ChartofAccount;
        }
        public string GetCOAType(string strValue, string companyCode)
        {
            string strtype = string.Empty;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select charttype from GL_ChartofAccount where RowStatus >-1  and ChartNo='" + strValue + "' and CompanyCode='"+companyCode+ "' order by ChartNo");
            strError = dataAccess.Error;
            arrGL_ChartofAccount = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    strtype = sqlDataReader["ChartType"].ToString(); 
                }
            }
            else
                strtype = "00";

            return strtype;
        }
        public GL_ChartofAccount RetrieveByName0(string strValue, string companyCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from GL_ChartofAccount where RowStatus >-1  and ChartName='" + strValue + "' and CompanyCode='"+companyCode+"'");
            strError = dataAccess.Error;
            arrGL_ChartofAccount = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new GL_ChartofAccount();
        }
        public ArrayList RetrieveByName(string ChartName, string companyCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from GL_ChartofAccount where chartname like '%" + ChartName + "%' and CompanyCode='"+companyCode+"'");
            strError = dataAccess.Error;
            arrGL_ChartofAccount = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_ChartofAccount.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_ChartofAccount.Add(new GL_ChartofAccount());

            return arrGL_ChartofAccount;
        }
        public ArrayList GetChartNotInChartBal(string period, string companyCode)
        {
            string strSQL = "Select * from GL_ChartofAccount where chartno not in (select chartno from gl_chartbal where period ='" + period + "' and CompanyCode='"+companyCode+"') and CompanyCode='"+companyCode+"'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGL_ChartofAccount = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_ChartofAccount.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_ChartofAccount.Add(new GL_ChartofAccount());

            return arrGL_ChartofAccount;
        }
        public string  GetChartName(string kriteria, string companyCode)
        {
            string strSQL = "Select * from GL_ChartofAccount where " + kriteria + " and CompanyCode='"+companyCode+"'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrGL_ChartofAccount = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return sqlDataReader["ChartName"].ToString();
                }
            }

            return string.Empty ;
        }
        public GL_ChartofAccount RetrieveWithBankAccountID(int coaID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select c.ID,c.ChartNo,c.ChartName from BankAccount as a,GL_ChartOfAccount as c where a.coaID=c.ID and a.id="+ coaID);
            strError = dataAccess.Error;
            arrGL_ChartofAccount = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new GL_ChartofAccount();
        }
        public string Get_Max_ChartNo(string Value, int Level, int CompanyCode)
        {
            string result = string.Empty;
            string Count = string.Empty;
            try
            {
                DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT MAX(ChartNo) AS ChartNo FROM GL_ChartOfAccount WHERE CompanyCode = " + CompanyCode + " AND [Level] = " + Level + " AND Parent = '" + Value + "'");
                strError = dataAccess.Error;
                if (strError == string.Empty)
                {
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            result = sqlDataReader["ChartNo"].ToString();
                        }

                        if (result.Length != 0)
                        {
                            if (Level == 1)
                            {
                                if (int.Parse(result.Substring(1, 1)) == 9)
                                {
                                    Count = result.Substring(1, 1) + (int.Parse(result.Substring(2, 1)) + 1).ToString();
                                }
                                else
                                {
                                    Count = (int.Parse(result.Substring(1, 1)) + 1).ToString() + "0";
                                }
                                if (Count.Length >= 3)
                                {
                                    result = "Max";
                                }
                                else
                                {
                                    result = result.Substring(0, 1) + Count + "0000" + "-000";
                                }
                            }
                            else if (Level == 2)
                            {
                                if (int.Parse(result.Substring(3, 1)) == 9)
                                {
                                    Count = result.Substring(3, 1) + (int.Parse(result.Substring(4, 1)) + 1).ToString();
                                }
                                else
                                {
                                    Count = (int.Parse(result.Substring(3, 1)) + 1).ToString() + "0";
                                }
                                if (Count.Length >= 3)
                                {
                                    result = "Max";
                                }
                                else
                                {
                                    result = result.Substring(0, 3) + Count + "00" + "-000";
                                }
                            }
                            else if (Level == 3)
                            {
                                Count = "00000" + (int.Parse(result.Substring(5, 2)) + 1).ToString();

                                if (int.Parse(Count) >= 100)
                                {
                                    result = "Max";
                                }
                                else
                                {
                                    result = result.Substring(0, 5) + Count.Substring(Count.Length - 2, 2) + "-000";
                                }
                            }
                            else if (Level == 4)
                            {
                                Count = "000000" + (int.Parse(result.Substring(result.Length - 3, 3)) + 1).ToString();
                                if (int.Parse(Count) >= 1000)
                                {
                                    result = "Max";
                                }
                                else
                                {
                                    result = result.Substring(0, 8) + Count.Substring(Count.Length - 3, 3);
                                }
                            }
                        }
                        else
                        {
                            if (Level == 1)
                            {
                                result = Value.Substring(0, 1) + "010000-000";
                            }
                            else if (Level == 2)
                            {
                                result = Value.Substring(0, 3) + "1000-000";
                            }
                            else if (Level == 3)
                            {
                                result = Value.Substring(0, 5) + "01-000";
                            }
                            else if (Level == 4)
                            {
                                result = Value.Substring(0, 8) + "001";
                            }
                        }

                    }
                }
            }
            catch (Exception)
            {
                result = string.Empty;
            }
            return result;
        }
        public bool COA_Exists(string NoCOA, string CompanyCode)
        {
            DataAccess data = new DataAccess(Global.ConnectionString());
            SqlDataReader sql = data.RetrieveDataByString("SELECT ChartNo FROM GL_ChartOfAccount WHERE RowStatus > -1 AND ChartNo = '" + NoCOA + "' AND CompanyCode = '" + CompanyCode + "'");
            string strError = data.Error;
            if (!sql.HasRows)
            {
                return false;
            }
            return true;
        }
        public ArrayList RetrieveByGroupAndLevel(int group, int level, string companyCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string sql = string.Empty;
            if (group == 0)
            {
                sql = "Select * from GL_ChartofAccount where RowStatus >-1 and [Level]='" + level + "' and CompanyCode='" + companyCode + "' ORDER BY [Group] ASC";
            }
            else
            {
                string strgroup = group.ToString();
                if (strgroup.Length > 1)
                {
                    sql = "Select * from GL_ChartofAccount where RowStatus >-1 and SUBSTRING([ChartNo], 1, " + strgroup.Length + ") = '" + group + "' AND [Level]='" + level + "' and CompanyCode='" + companyCode + "' ORDER BY [Group] ASC";
                }
                else
                {
                    sql = "Select * from GL_ChartofAccount where RowStatus >-1 and [Group] = '" + group + "' AND [Level]='" + level + "' and CompanyCode='" + companyCode + "' ORDER BY [Group] ASC";
                }
            }

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(sql);
            strError = dataAccess.Error;
            arrGL_ChartofAccount = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGL_ChartofAccount.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGL_ChartofAccount.Add(new GL_ChartofAccount());

            return arrGL_ChartofAccount;
        }
        public GL_ChartofAccount GenerateObject2(SqlDataReader sqlDataReader)
        {
            objGL_ChartofAccount = new GL_ChartofAccount();
            objGL_ChartofAccount.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objGL_ChartofAccount.ChartNo = sqlDataReader["ChartNo"].ToString();
            objGL_ChartofAccount.ChartName = sqlDataReader["ChartName"].ToString();

            return objGL_ChartofAccount;
        }
        public GL_ChartofAccount GenerateObject(SqlDataReader sqlDataReader)
        {
            objGL_ChartofAccount = new GL_ChartofAccount();
            objGL_ChartofAccount.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objGL_ChartofAccount.ChartNo = sqlDataReader["ChartNo"].ToString();
            objGL_ChartofAccount.ChartName = sqlDataReader["ChartName"].ToString();
            objGL_ChartofAccount.Group = Convert.ToInt32(sqlDataReader["Group"]);
            objGL_ChartofAccount.Postable = Convert.ToInt32(sqlDataReader["Postable"]);
            objGL_ChartofAccount.CCYCode = sqlDataReader["cCYCode"].ToString();
            objGL_ChartofAccount.Parent = sqlDataReader["Parent"].ToString();
            objGL_ChartofAccount.IsDept = Convert.ToInt32(sqlDataReader["IsDept"]);
            objGL_ChartofAccount.IsCost = Convert.ToInt32(sqlDataReader["IsCost"]);
            objGL_ChartofAccount.Level = Convert.ToInt32(sqlDataReader["Level"]); // Add By Anang 23-08-2018
            objGL_ChartofAccount.ChartType = sqlDataReader["ChartType"].ToString();
            objGL_ChartofAccount.NotesNo = sqlDataReader["NotesNo"].ToString();
            objGL_ChartofAccount.CompanyCode = sqlDataReader["CompanyCode"].ToString();
            if (Convert.ToInt32(sqlDataReader["Postable"]) == 0)
                objGL_ChartofAccount.strPostAble = sqlDataReader["ChartName"].ToString().PadRight(20, ' ') + " - [False]";
            else
                objGL_ChartofAccount.strPostAble = sqlDataReader["ChartName"].ToString().PadRight(20, ' ') + " - [True ]";

            return objGL_ChartofAccount;
        }
    }
}
