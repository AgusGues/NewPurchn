using Dapper;
using DataAccessLayer;
using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessFacade
{
   public class SPKPFacade : AbstractTransactionFacade
    {
        private List<SqlParameter> sqlListParam;
        private SPKP_Dtl spkpd = new SPKP_Dtl();

        public SPKPFacade(object objDomain) : base(objDomain)
        {
            spkpd = (SPKP_Dtl)objDomain;
            
        }

        public SPKPFacade()
        {

        }
        public override int Delete(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@NoSpkp", spkpd.NoSpkp));
                sqlListParam.Add(new SqlParameter("@CreatedBy", spkpd.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "[spinsert_spkp]");
                strError = transManager.Error;
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
            throw new NotImplementedException();
        }

        public override int Update(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }
        public static List<SPKP_Dtl.ddl1> Retrivekategori()
        {
            List<SPKP_Dtl.ddl1> alldata = new List<SPKP_Dtl.ddl1>();
            string strField = string.Empty;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string strsql = "SELECT DISTINCT substring(partno,1,3)Kategori  FROM FC_Items WHERE RowStatus>-1 AND PartNo like'%-1-%'";
                        alldata = connection.Query<SPKP_Dtl.ddl1>(strsql).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }

        public static List<SPKP_Dtl.ddl2> Retrivetebal()
        {
            List<SPKP_Dtl.ddl2> alldata = new List<SPKP_Dtl.ddl2>();
            string strField = string.Empty;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string strsql = "SELECT DISTINCT tebal FROM FC_Items  WHERE RowStatus>-1  AND PartNo like'%-1-%'";
                    alldata = connection.Query<SPKP_Dtl.ddl2>(strsql).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }

        public static List<SPKP_Dtl.ddl3> Retriveukuran()
        {
            List<SPKP_Dtl.ddl3> alldata = new List<SPKP_Dtl.ddl3>();
            string strField = string.Empty;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string strsql = "select distinct CONVERT(varchar,cast(lebar as int), 112) + ' X ' + RTRIM(CAST(cast(panjang as int) AS varchar(10)))ukuran from fc_items where PartNo like '%-1-%' and RowStatus>-1";
                    alldata = connection.Query<SPKP_Dtl.ddl3>(strsql).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }

        public static List<SPKP_Dtl> RetriveList()
        {
            List<SPKP_Dtl> alldata = new List<SPKP_Dtl>();
            string strField = string.Empty;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string strsql = "select ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS no,id, nospkp from spkp where RowStatus>-1 order by id desc";
                    alldata = connection.Query<SPKP_Dtl>(strsql).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }
        public static List<SPKP_Dtl.insert_dtl> Retrivespkppilih(string nospkp,string line)
        {
            List<SPKP_Dtl.insert_dtl> alldata = new List<SPKP_Dtl.insert_dtl>();
            string strField = string.Empty;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string strsql = "select * from spkp_detail where idspkp in(select id from spkp where nospkp='" + nospkp + "')and line='" + line + "' and RowStatus>-1";
                    alldata = connection.Query<SPKP_Dtl.insert_dtl>(strsql).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }

        public static List<SPKP_Dtl.insert_dtl> Retrivespkpdetail(string id)
        {
            List<SPKP_Dtl.insert_dtl> alldata = new List<SPKP_Dtl.insert_dtl>();
            string strField = string.Empty;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string strsql = "select * from spkp_detail where id=" + id ;
                    alldata = connection.Query<SPKP_Dtl.insert_dtl>(strsql).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }

        public static string Retrivenospkp(string id)
        {
            DataAccess dataaccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqldatareader = dataaccess.RetrieveDataByString("select nospkp from spkp where id=" + id);

            //strError = dataaccess.Error;
            if (sqldatareader.HasRows)
            {
                while (sqldatareader.Read())
                {
                    return sqldatareader["nospkp"].ToString();
                }
            }
            return string.Empty;
        }

        
    }
}
