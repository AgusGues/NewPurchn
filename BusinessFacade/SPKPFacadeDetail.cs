using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Data.SqlClient;
using Domain;
using System.Collections;

namespace BusinessFacade
{
    public class SPKPFacadeDetail : AbstractTransactionFacade
    {
        private List<SqlParameter> sqlListParam;
        private SPKP_Dtl.insert_dtl spkpdtl = new SPKP_Dtl.insert_dtl();
        
        public SPKPFacadeDetail(object objDomain) : base(objDomain)
        {
            spkpdtl = (SPKP_Dtl.insert_dtl)objDomain;
        }

        public SPKPFacadeDetail()
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

                sqlListParam.Add(new SqlParameter("@spkpid", spkpdtl.spkpid));
                sqlListParam.Add(new SqlParameter("@Nospkp", spkpdtl.NoSpkp));
                sqlListParam.Add(new SqlParameter("@Tanggal", spkpdtl.Tanggal));
                sqlListParam.Add(new SqlParameter("@Line", spkpdtl.Line));
                sqlListParam.Add(new SqlParameter("@Shift", spkpdtl.Shift));
                sqlListParam.Add(new SqlParameter("@Kategori", spkpdtl.Kategori));
                sqlListParam.Add(new SqlParameter("@Ukuran", spkpdtl.Ukuran));
                sqlListParam.Add(new SqlParameter("@Tebal", spkpdtl.Tebal));
                sqlListParam.Add(new SqlParameter("@Target", spkpdtl.Target));
                sqlListParam.Add(new SqlParameter("@CreatedBy", spkpdtl.CreatedBy));
                sqlListParam.Add(new SqlParameter("@Keterangan", spkpdtl.Keterangan));
                int intResult = transManager.DoTransaction(sqlListParam, "[spInsert_spkpdetail]");
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
            try
            {
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@id", spkpdtl.id));
                sqlListParam.Add(new SqlParameter("@Kategori", spkpdtl.Kategori));
                sqlListParam.Add(new SqlParameter("@Ukuran", spkpdtl.Ukuran));
                sqlListParam.Add(new SqlParameter("@Tebal", spkpdtl.Tebal));
                sqlListParam.Add(new SqlParameter("@Target", spkpdtl.Target));
                sqlListParam.Add(new SqlParameter("@Keterangan", spkpdtl.Keterangan));
                int intResult = transManager.DoTransaction(sqlListParam, "[spupdate_spkpdetail]");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
       
    }
}
