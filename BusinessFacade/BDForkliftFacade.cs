using Dapper;
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
    public class BDForkliftFacade : AbstractFacade
    {
        private List<SqlParameter> sqlListParam;

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

        public override ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }

        public static List<BD_Forklift> GetFroklift()
        {
            List<BD_Forklift> alldata = new List<BD_Forklift>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string strSQL = "select forklift from masterforklift where rowstatus>-1";
                    alldata = connection.Query<BD_Forklift>(strSQL).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }

        public int Insertbreakdown(DateTime Tanggal, string Forklift, string Start, string Finish, int Total,string Kendala,string Perbaikan, string Keterangan,string Users)
        {
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    sqlListParam = new List<SqlParameter>();
                    sqlListParam.Add(new SqlParameter("@Tanggal", Tanggal));
                    sqlListParam.Add(new SqlParameter("@Forklift", Forklift));
                    sqlListParam.Add(new SqlParameter("@Start", Start));
                    sqlListParam.Add(new SqlParameter("@Finish", Finish));
                    sqlListParam.Add(new SqlParameter("@Total", Total));
                    sqlListParam.Add(new SqlParameter("@Kendala", Kendala));
                    sqlListParam.Add(new SqlParameter("@Perbaikan", Perbaikan));
                    sqlListParam.Add(new SqlParameter("@Keterangan", Keterangan));
                    sqlListParam.Add(new SqlParameter("@Createdby", Users));
                    int intResult = dataAccess.ProcessData(sqlListParam, "spMTC_Break_Forklift_Insert");
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
    }
}
