using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;
using System.Web;
using Dapper;

namespace BusinessFacade
{
    public class MTC_ZonaFacade : AbstractFacade
    {
        private MTC_Zona objMTC_Zona = new MTC_Zona();
        private ArrayList arrMTC_Zona;
        //private List<SqlParameter> sqlListParam;

        public MTC_ZonaFacade()
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
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "select * from MTC_Zona order by zonaname";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrMTC_Zona = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrMTC_Zona.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrMTC_Zona.Add(new MTC_Zona());

            return arrMTC_Zona;
        }
        /**
         * added on 11-03-2013
         * for spp sp
         */

        public ArrayList RetrieveSpGroup()
        {
            Users usr = (Users)HttpContext.Current.Session["Users"];
            string NewSarmut = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("NewSarmutAktif", "SPBMaintenance");
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL =(usr.UnitKerjaID==1 && NewSarmut=="0")? "select ID,GroupName as ZonaName,Kode as ZonaCode from MTC_GroupSarmut order by GroupName":
            "WITH GroupSarmut AS " +
                   " (select ROW_NUMBER() OVER(Partition By vw.ID Order By vw.ID, vw.SarmutID )N, vw.*,m.GroupNaME  " +
                   " FROM vw_SarmutGroup as vw " +
                   " LEFT JOIN MaterialMTCGroup as m ON m.ID=vw.ID " +
                   " WHERE m.RowStatus >-2 " +
                   " )  " +
                   " SELECT SarmutID AS ID,GroupName as ZonaName, ID as ZonaCode FROM GroupSarmut WHERE N=1 ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrMTC_Zona = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrMTC_Zona.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrMTC_Zona.Add(new MTC_Zona());

            return arrMTC_Zona;
        }

        public static List<MTC_Zona> RetriveSPGroupNew()
        {
            List<MTC_Zona> alldata = new List<MTC_Zona>();
            Users usr = (Users)HttpContext.Current.Session["Users"];
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {

                try
                {
                    string strSQL = "";
                    if (usr.UnitKerjaID == 1){
                        strSQL = "select ID,GroupName as ZonaName,Kode as ZonaCode from MTC_GroupSarmut order by GroupName";
                    }
                    else
                    {
                        strSQL= "WITH GroupSarmut AS " +
                                " (select ROW_NUMBER() OVER(Partition By vw.ID Order By vw.ID, vw.SarmutID )N, vw.*,m.GroupNaME  " +
                                " FROM vw_SarmutGroup as vw " +
                                " LEFT JOIN MaterialMTCGroup as m ON m.ID=vw.ID " +
                                " WHERE m.RowStatus >-2 " +
                                " )  " +
                                " SELECT SarmutID AS ID,GroupName as ZonaName, ID as ZonaCode FROM GroupSarmut WHERE N=1 ";
                    }
                    alldata = connection.Query<MTC_Zona>(strSQL).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }

        public MTC_Zona GenerateObject(SqlDataReader sqlDataReader)
        {
            objMTC_Zona = new MTC_Zona();
            objMTC_Zona.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objMTC_Zona.ZonaName = sqlDataReader["ZonaName"].ToString();
            objMTC_Zona.ZonaCode = sqlDataReader["ZonaCode"].ToString();
            return objMTC_Zona;
        }
    }
}
