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
using Dapper;

namespace Factory
{
    public class FC_LokasiFacade : AbstractFacade
    {
        private FC_Lokasi objFC_Lokasi = new FC_Lokasi();
        private ArrayList arrFC_Lokasi;
        private List<SqlParameter> sqlListParam;

        public FC_LokasiFacade()
            : base()
        {

        }
        public override int Insert(object objDomain)
        {
            try
            {
                objFC_Lokasi = (FC_Lokasi)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@LokTypeID", objFC_Lokasi.LokTypeID));
                sqlListParam.Add(new SqlParameter("@Lokasi", objFC_Lokasi.Lokasi));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertFC_Lokasi");
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
                objFC_Lokasi = (FC_Lokasi)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objFC_Lokasi.ID));
                sqlListParam.Add(new SqlParameter("@Lokasi", objFC_Lokasi.Lokasi));
               // sqlListParam.Add(new SqlParameter("@Status", objFC_Lokasi.Status));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateFC_Lokasi");

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

            try
            {
                objFC_Lokasi = (FC_Lokasi)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objFC_Lokasi.ID));
                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteFC_Lokasi");
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
            string strSQL = "SELECT * from FC_Lokasi where rowstatus>-1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrFC_Lokasi = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrFC_Lokasi.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrFC_Lokasi.Add(new FC_Lokasi());

            return arrFC_Lokasi;
        }
        public ArrayList RetrieveTransit()
        {
            string strSQL = "SELECT distinct * from FC_Lokasi where lokasi in('i99','h99','c99','b99','q99','p99','AdjOut','AdjIn') and rowstatus>-1 and ID in (select distinct lokid from t1_serah where qtyout<qtyin and status>-1) order by lokasi desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrFC_Lokasi = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrFC_Lokasi.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrFC_Lokasi.Add(new FC_Lokasi());

            return arrFC_Lokasi;
        }

        public FC_Lokasi RetrieveByItemID(int ID)
        {
            string strSQL = "SELECT * from FC_Lokasi where rowstatus>-1 and ID=" + ID;
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
            return new FC_Lokasi();
        }

        public FC_Lokasi RetrieveByLokasi(string lokasi)
        {
            string strSQL = "SELECT * from FC_Lokasi where rowstatus>-1 and lokasi='" + lokasi + "'";
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
            return new FC_Lokasi();
        }

        public int CekLokasiLoading(string lokasi)
        {
            int ada = 0;
            string strSQL = "SELECT count(lokid) as lokid from t3_siapkirim where rowstatus>-1 and lokid in (select id from fc_lokasi where lokasi='" + lokasi.Trim() + "')";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    ada=Convert.ToInt32(sqlDataReader["LokID"]); 
                }
            }
            return ada;
        }
        public int GetID(string lokasi)
        {
            int ada = 0;
            string strSQL = "SELECT top 1 ID from fc_lokasi where lokasi='" + lokasi.Trim() + "' order by ID desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    ada = Convert.ToInt32(sqlDataReader["ID"]);
                }
            }
            return ada;
        }
        public ArrayList  RetrieveByLokasi1(string lokasi)
        {
            string strSQL = "SELECT * from FC_Lokasi where rowstatus>-1 and lokasi='" + lokasi + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrFC_Lokasi = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrFC_Lokasi.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrFC_Lokasi.Add(new FC_Lokasi());

            return arrFC_Lokasi;
        }
        public FC_Lokasi RetrieveByLokTypeID(string lokasi, int loktypeID)
        {
            string strSQL = "SELECT * from FC_Lokasi where rowstatus>-1 and lokasi='" + lokasi + "' and loktypeID=" + loktypeID;
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
            return new FC_Lokasi();
        }

        public string GetLokasiLoading()
        {
            string Lokasi = string.Empty;
            string strSQL = "select top 1 lokasi from FC_Lokasi where ID in (select top 1 lokid from T3_SiapKirim order by ID desc) order by ID desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    Lokasi = sqlDataReader["Lokasi"].ToString();
                }
            }
            return Lokasi;
        }

        public ArrayList RetrieveByLokTypeID2(int loktypeID)
        {
            string strSQL = "SELECT * from FC_Lokasi where rowstatus>-1 and loktypeID=" + loktypeID + " order by lokasi";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrFC_Lokasi = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrFC_Lokasi.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrFC_Lokasi.Add(new FC_Lokasi());

            return arrFC_Lokasi;
        }
        public int check(string strLokasi)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from fc_Lokasi where rowstatus>-1 and lokasi = '" + strLokasi + "'");
            int ada = 0;
            if (sqlDataReader.HasRows)
            {
                ada = 1;
            }

            return ada;
        }
        public FC_Lokasi GenerateObject(SqlDataReader sqlDataReader)
        {
            objFC_Lokasi = new FC_Lokasi();
            objFC_Lokasi.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objFC_Lokasi.LokTypeID = Convert.ToInt32(sqlDataReader["LokTypeID"]);
            objFC_Lokasi.Lokasi  = sqlDataReader["Lokasi"].ToString();
            
            return objFC_Lokasi;
        }

        public static List<Produk.GetLokasi> CekLokasi(string lokasi)
        {
            List<Produk.GetLokasi> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select * from fc_Lokasi where rowstatus>-1 and lokasi = '" + lokasi + "'";
                    AllData = connection.Query<Produk.GetLokasi>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public int InsertLokasi(int ID, string lokasi)
        {
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    sqlListParam = new List<SqlParameter>();
                    sqlListParam.Add(new SqlParameter("@LokTypeID", ID));
                    sqlListParam.Add(new SqlParameter("@Lokasi", lokasi));
                    int intResult = dataAccess.ProcessData(sqlListParam, "spInsertFC_Lokasi");
                    strError = dataAccess.Error;
                    return 1;
                }
                catch (Exception ex)
                {
                    strError = ex.Message;
                    return -1;
                }
            }
        }

        public int GetStokLokasi(string partno, string lokasi)
        {
            int stok;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select qty from t3_serah where ItemID in (select ID from fc_items where partno='" + partno + "') and LokID in (select ID from fc_lokasi where lokasi='" + lokasi + "')";
                    stok = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {
                    stok = 0;
                }
            }
            return stok;
        }

        public string GetLokIDLokasi(string lokasi)
        {
            string lokid;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "SELECT TOP 1 ID FROM FC_LOKASI WHERE LOKASI ='" + lokasi.Trim() + "' AND ROWSTATUS > -1 ";
                    lokid = connection.QueryFirstOrDefault<string>(query);
                }
                catch (Exception e)
                {
                    lokid = "0";
                }
            }
            return lokid;
        }



        public static List<FC_Lokasi> GetLokasiT1(string lokasi)
        {
            List<FC_Lokasi> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "SELECT TOP 10 Lokasi FROM FC_LOKASI WHERE LOKASI like '" + lokasi + "%' AND ROWSTATUS > -1 ";
                    AllData = connection.Query<FC_Lokasi>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<LokasiStok> GetLokasiStok(string partno, string lokasi, string thnbln, string thnbln0)
        {
            List<LokasiStok> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select  isnull(itemid,0) as itemid, lokid as lokasiID, SUM(saldo) as saldo from ( " +
                    "select itemid , lokid,SUM(saldo) as saldo from T1_SaldoPerLokasi where thnbln='" + thnbln0 + "' group by itemid , lokid " +
                    "union all " +
                    "select itemid0 as itemid, lokid,SUM(qty) as saldo from vw_KartustockWIP2 where LEFT(convert(varchar,tanggal,112),6)='" + thnbln + "' group by itemid0 , lokid " +
                    "union all " +
                    "select itemid0 as itemid, lokid,SUM(qty) as saldo from vw_KartustockWIP where LEFT(convert(varchar,tanggal,112),6)='" + thnbln + "' group by itemid0, lokid )  " +
                    "as A  where itemid in (select ID from fc_items where partno='" + partno + "') and lokid in (select ID from fc_lokasi where lokasi='" + lokasi.Trim() + "') group by itemid, lokid";
                    AllData = connection.Query<LokasiStok>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<FC_Lokasi> GetLokasiTransit()
        {
            List<FC_Lokasi> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "SELECT distinct * from FC_Lokasi where lokasi in('i99','h99','c99','b99','q99','p99','AdjOut','AdjIn') and rowstatus>-1 and ID in (select distinct lokid from t1_serah where qtyout<qtyin and status>-1)";
                    AllData = connection.Query<FC_Lokasi>(query).ToList();
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
