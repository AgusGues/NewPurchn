using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using System.Web;
using BusinessFacade;
using DataAccessLayer;
using Dapper;

namespace BusinessFacade
{
    public class RekapPakaiNfFacade : AbstractTransactionFacade
    {
        public override int Delete(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Insert(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }

        public override int Update(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public static List<RekapPakaiNf.ParamGroupItem> GetListGroupItem()
        {
            List<RekapPakaiNf.ParamGroupItem> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "select ID,GroupDescription from GroupsPurchn where RowStatus >-1";
                    AllData = connection.Query<RekapPakaiNf.ParamGroupItem>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<RekapPakaiNf.ParamDept> GetListDept()
        {
            List<RekapPakaiNf.ParamDept> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "select DISTINCT Alias from dept where RowStatus >-1 order by Alias";
                    AllData = connection.Query<RekapPakaiNf.ParamDept>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<RekapPakaiNf.ParamDataDetail> GetListDataDetail(string strSQuery)
        {
            List<RekapPakaiNf.ParamDataDetail> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = strSQuery;
                    AllData = connection.Query<RekapPakaiNf.ParamDataDetail>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<RekapPakaiNf.ParamDataRekap> GetListDataRekap(string strSQuery)
        {
            List<RekapPakaiNf.ParamDataRekap> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = strSQuery;
                    AllData = connection.Query<RekapPakaiNf.ParamDataRekap>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }       
    }
}
