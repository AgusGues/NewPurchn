using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;

namespace BusinessFacade
{
    public class MTC_MacSysFacade : AbstractFacade
    {
        private MTC_MacSys objMTC_MacSys = new MTC_MacSys();
        private ArrayList arrMTC_MacSys;
       // private List<SqlParameter> sqlListParam;

        public MTC_MacSysFacade()
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
            throw new NotImplementedException();
        }

        public ArrayList  RetrieveByDept(int DeptID,int Plant)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "select * from MTC_MacSys where (deptid=0 or deptid=" + DeptID + ") and (plantID=0 or plantID=" + Plant + ")";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrMTC_MacSys = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrMTC_MacSys.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrMTC_MacSys.Add(new MTC_MacSys());

            return  arrMTC_MacSys;
        }

        public MTC_MacSys GenerateObject(SqlDataReader sqlDataReader)
        {
            objMTC_MacSys = new MTC_MacSys();
            objMTC_MacSys.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objMTC_MacSys.MacSysName = sqlDataReader["MacSysName"].ToString();
            objMTC_MacSys.MacSysCode = sqlDataReader["MacSysCode"].ToString();
            return objMTC_MacSys;
        }
    }
}
