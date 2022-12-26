using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;
using BusinessFacade;
using System.Web;
using System.Web.UI;

using Cogs;

namespace Factory
{
    public class T1_SaldoPerLokasiFacade : AbstractFacade
    {
        private T1_SaldoPerLokasi objT1_SaldoPerLokasi = new T1_SaldoPerLokasi();
        private ArrayList arrT1_SaldoPerLokasi;
        private List<SqlParameter> sqlListParam;

        public T1_SaldoPerLokasiFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objT1_SaldoPerLokasi = (T1_SaldoPerLokasi)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@thnbln", objT1_SaldoPerLokasi.ThnBln ));
                sqlListParam.Add(new SqlParameter("@ItemID", objT1_SaldoPerLokasi.ItemID));
                sqlListParam.Add(new SqlParameter("@LokID", objT1_SaldoPerLokasi.LokasiID ));
                sqlListParam.Add(new SqlParameter("@Saldo", objT1_SaldoPerLokasi.Saldo ));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT1_SaldoPerLokasi.CreatedBy ));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertT1_SaldoPerLokasi");
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
                objT1_SaldoPerLokasi = (T1_SaldoPerLokasi)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@thnbln", objT1_SaldoPerLokasi.ThnBln));
                sqlListParam.Add(new SqlParameter("@ItemID", objT1_SaldoPerLokasi.ItemID));
                sqlListParam.Add(new SqlParameter("@LokasiID", objT1_SaldoPerLokasi.LokasiID));
                sqlListParam.Add(new SqlParameter("@Saldo", objT1_SaldoPerLokasi.Saldo));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateT1_SaldoPerLokasi");
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

            return -1;
        }

        public int KosongkanSaldo(string thnbln)
        {

            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@thnbln", thnbln));
                int intResult = dataAccess.ProcessData(sqlListParam, "spKosongkanT1_SaldoPerLokasi");
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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from T1_SaldoPerLokasi");
            strError = dataAccess.Error;
            arrT1_SaldoPerLokasi = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_SaldoPerLokasi.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT1_SaldoPerLokasi.Add(new T1_SaldoPerLokasi());

            return arrT1_SaldoPerLokasi;

        }

        public ArrayList RetrieveByThnBln(string thnbln0,string thnbln)
        {
            string strSQL =string.Empty;
            int depo = ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID;
            string periode = new Inifile(HttpContext.Current.Server.MapPath("~/App_Data/Factory.ini")).Read("AwalPeriode" + depo.ToString(), "Report");
            if (Convert.ToInt32(thnbln) >= Convert.ToInt32(periode))
             strSQL = "select  isnull(itemid,0) as itemid, lokid as lokasiID, SUM(saldo) as saldo from ( " +
                "select itemid , lokid,SUM(saldo) as saldo from T1_SaldoPerLokasi where rowstatus>-1 and thnbln='" + thnbln0 + "' group by itemid , lokid " +
                "union all " +
                "select itemid, lokid,sum(pengeluaran)as saldo from ( select LEFT(convert(varchar,tanggal,112),6) as thnbln, itemid0 as itemid, " +
                "(select ID from fc_lokasi where lokasi='p99')as lokid,0 as penerimaan,isnull(qty,0) as pengeluaran  from vw_KartustockWIP2  " +
                "where LEFT(convert(varchar,tanggal,112),6)='" + thnbln + "') as A  group by itemid, lokid " +
                "union All " +
                "select itemid0 as itemid, lokid,SUM(qty) as saldo from vw_KartustockWIP where LEFT(convert(varchar,tanggal,112),6)='" + thnbln + "' group by itemid0, lokid )  " +
                "as A  group by itemid, lokid";
            else
                strSQL = "select  isnull(itemid,0) as itemid, lokid as lokasiID, SUM(saldo) as saldo from ( " +
               "select itemid , lokid,SUM(saldo) as saldo from T1_SaldoPerLokasi where rowstatus>-1 and thnbln='" + thnbln0 + "' group by itemid , lokid " +
               //"union all " +
               //"select itemid, lokid,sum(pengeluaran)as saldo from ( select LEFT(convert(varchar,tanggal,112),6) as thnbln, itemid0 as itemid, " +
               //"(select ID from fc_lokasi where lokasi='p99')as lokid,0 as penerimaan,isnull(qty,0) as pengeluaran  from vw_KartustockWIP2  " +
               //"where LEFT(convert(varchar,tanggal,112),6)='" + thnbln + "') as A  group by itemid, lokid " +
               "union All " +
               "select itemid0 as itemid, lokid,SUM(qty) as saldo from vw_KartustockWIPOld where LEFT(convert(varchar,tanggal,112),6)='" + thnbln + "' group by itemid0, lokid )  " +
               "as A  group by itemid, lokid";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_SaldoPerLokasi = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_SaldoPerLokasi.Add(GenerateObjectPosting(sqlDataReader));
                }
            }
            else
                arrT1_SaldoPerLokasi.Add(new T1_SaldoPerLokasi());

            return arrT1_SaldoPerLokasi;
        }

        public T1_SaldoPerLokasi RetrieveBylokasi(string thnbln0, string thnbln, string partno, string lokasi)
        {
            try
            {
                string strSQL = "select  isnull(itemid,0) as itemid, lokid as lokasiID, SUM(saldo) as saldo from ( " +
                    "select itemid , lokid,SUM(saldo) as saldo from T1_SaldoPerLokasi where thnbln='" + thnbln0 + "' group by itemid , lokid " +
                    "union all" +
                    "select itemid0 as itemid, lokid,SUM(qty) as saldo from vw_KartustockWIP2 where LEFT(convert(varchar,tanggal,112),6)='" + thnbln + "' group by itemid0 , lokid " +
                    "union all " +
                    "select itemid0 as itemid, lokid,SUM(qty) as saldo from vw_KartustockWIP where LEFT(convert(varchar,tanggal,112),6)='" + thnbln + "' group by itemid0, lokid )  " +
                    "as A  where itemid in (select ID from fc_items where partno='" + partno + "') and lokid in (select ID from fc_lokasi where lokasi='" + lokasi + "') group by itemid, lokid";
                DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
                strError = dataAccess.Error;
                arrT1_SaldoPerLokasi = new ArrayList();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        return GenerateObjectPosting(sqlDataReader);
                    }
                }
                
            }
            catch
            { }
            return new T1_SaldoPerLokasi();
        }

        public T1_SaldoPerLokasi RetrieveByItemID(int itemID, int thn, int itemtypeID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from T1_SaldoPerLokasi where ItemId=" + itemID + 
                " and YearPeriod=" + thn + " and ItemTypeID=" + itemtypeID);
            strError = dataAccess.Error;
            arrT1_SaldoPerLokasi = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new T1_SaldoPerLokasi();
        }
        public T1_SaldoPerLokasi GenerateObjectPosting(SqlDataReader sqlDataReader)
        {
            objT1_SaldoPerLokasi = new T1_SaldoPerLokasi();
            objT1_SaldoPerLokasi.LokasiID = Convert.ToInt32(sqlDataReader["LokasiID"]);
            objT1_SaldoPerLokasi.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objT1_SaldoPerLokasi.Saldo = Convert.ToInt32(sqlDataReader["Saldo"]);
            return objT1_SaldoPerLokasi;
        }
        public T1_SaldoPerLokasi GenerateObject(SqlDataReader sqlDataReader)
        {
            objT1_SaldoPerLokasi = new T1_SaldoPerLokasi();
            objT1_SaldoPerLokasi.ThnBln = sqlDataReader["ThnBln"].ToString();
            objT1_SaldoPerLokasi.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objT1_SaldoPerLokasi.LokasiID = Convert.ToInt32(sqlDataReader["LokasiID"]);
            objT1_SaldoPerLokasi.Lokasi = sqlDataReader["Lokasi"].ToString();
            objT1_SaldoPerLokasi.Partno = sqlDataReader["Partno"].ToString(); ;
            objT1_SaldoPerLokasi.Saldo = Convert.ToInt32(sqlDataReader["Saldo"]);
            return objT1_SaldoPerLokasi;
        }
    }
}
