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
    public class MTC_MacSysPartFacade : AbstractFacade
    {
       private MTC_MacSysPart objMTC_MacSysPart = new MTC_MacSysPart();
        private ArrayList arrMTC_MacSysPart;
        //private List<SqlParameter> sqlListParam;

        public MTC_MacSysPartFacade()
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

        public ArrayList  RetrieveByID(int MacSysID,int zonaid)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "select * from MTC_MacSysPart where MacSysID=" + MacSysID + " and (zonaID=0 or zonaID="+zonaid+")";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrMTC_MacSysPart = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrMTC_MacSysPart.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrMTC_MacSysPart.Add(new MTC_MacSysPart());
            return  arrMTC_MacSysPart;
        }

        public MTC_MacSysPart GenerateObject(SqlDataReader sqlDataReader)
        {
            objMTC_MacSysPart = new MTC_MacSysPart();
            objMTC_MacSysPart.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objMTC_MacSysPart.MacSysID = Convert.ToInt32(sqlDataReader["MacSysID"]);
            objMTC_MacSysPart.MacSysPartName = sqlDataReader["MacSysPartName"].ToString();
            objMTC_MacSysPart.MacSysPartCode = sqlDataReader["MacSysPartCode"].ToString();
            return objMTC_MacSysPart;
        }
    }
}
