using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using Dapper;

namespace Factory
{
    public class T3_GroupsFacade : AbstractFacade
    {
        private T3_Groups objT3Groups = new T3_Groups();
        private ArrayList arrT3Groups;
        private List<SqlParameter> sqlListParam;

        public T3_GroupsFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override int Update(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override int Delete(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override System.Collections.ArrayList Retrieve()
        {
            string strSQL = "select * from t3_groupm  order by [groups]";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3Groups = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3Groups.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT3Groups.Add(new KartuStock());

            return arrT3Groups;
        }

        public System.Collections.ArrayList RetrieveJenis()
        {
            string strSQL = "select ROW_NUMBER() OVER(order by groups) ID,* from (select distinct  SUBSTRING(partno,1,3)groups from FC_Items where (partno like '%-w-%' or partno like '%-m-%') and  RowStatus>-1 )A order by groups";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3Groups = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3Groups.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT3Groups.Add(new KartuStock());

            return arrT3Groups;
        }
        public int GetID(string grp)
        {
            int groupID=0;
            string strSQL = "select * from t3_groupm where  rowstatus>-1 and [groups]='" + grp + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3Groups = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    groupID=Convert.ToInt32(sqlDataReader["ID"]);
                }
            }

            return groupID;
        }

        public T3_Groups RetrieveByItem(string partno)
        {
            string strSQL = "SELECT * from t3_groupm where  rowstatus>-1  and id in (select groupid from fc_items where partno='" + partno + "')";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new T3_Groups();
        }

        public T3_Groups  GenerateObject(SqlDataReader sqlDataReader)
        {
            objT3Groups = new T3_Groups();
            objT3Groups.ID  = Convert.ToInt32(sqlDataReader["ID"]);
            objT3Groups.Groups = (sqlDataReader["Groups"]).ToString();
            return objT3Groups;
        }

        public static List<T3_Groups.Jenis> GetJenis()
        {

            List<T3_Groups.Jenis> AllData = new List<T3_Groups.Jenis>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select ROW_NUMBER() OVER(order by groups) ID,* from (select distinct  SUBSTRING(partno,1,3)groups from FC_Items where (partno like '%-w-%' or partno like '%-m-%') and  RowStatus>-1 )A order by groups";
                    AllData = connection.Query<T3_Groups.Jenis>(query).ToList();
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
