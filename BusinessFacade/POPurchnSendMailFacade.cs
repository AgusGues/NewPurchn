using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;


namespace BusinessFacade
{
    public class POPurchnSendMailFacade : AbstractFacade
    {
        private PoPurchMail objPoPurchMail = new PoPurchMail();
        private ArrayList arrPoPurchMail;
        private List<SqlParameter> sqlListParam;

        public POPurchnSendMailFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objPoPurchMail = (PoPurchMail)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@TglKirim", objPoPurchMail.TglKirim));
                sqlListParam.Add(new SqlParameter("@NoPO", objPoPurchMail.NoPO));
                sqlListParam.Add(new SqlParameter("@SupplierName", objPoPurchMail.SupplierName));
                sqlListParam.Add(new SqlParameter("@Email", objPoPurchMail.Email));
                sqlListParam.Add(new SqlParameter("@Report", objPoPurchMail.Report));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objPoPurchMail.CreatedBy));
                sqlListParam.Add(new SqlParameter("@BodyMail", objPoPurchMail.Keterangan));
                int IntResult = dataAccess.ProcessData(sqlListParam, "SPInsertPOMailReport");

                strError = dataAccess.Error;

                return IntResult;
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
                objPoPurchMail = (PoPurchMail)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objPoPurchMail.ID));
                sqlListParam.Add(new SqlParameter("@TglKirim", objPoPurchMail.TglKirim));
                sqlListParam.Add(new SqlParameter("@NoPO", objPoPurchMail.NoPO));
                sqlListParam.Add(new SqlParameter("@SupplierName", objPoPurchMail.SupplierName));
                sqlListParam.Add(new SqlParameter("@Email", objPoPurchMail.Email));
                sqlListParam.Add(new SqlParameter("@Report", objPoPurchMail.Report));

                int IntResult = dataAccess.ProcessData(sqlListParam, "SPUpdatePOMailReport");

                strError = dataAccess.Error;

                return IntResult;
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
                objPoPurchMail = (PoPurchMail)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objPoPurchMail.ID));

                int IntResult = dataAccess.ProcessData(sqlListParam, "SPDeletePOMailReport");

                strError = dataAccess.Error;

                return IntResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public bool KirimanHariIni { get; set; }
        public ArrayList RetrieveMailSend(string Bulan, string Tahun)
        {
            string where = (this.KirimanHariIni == true) ? " AND CONVERT(CHAR,TglKirim,112)=CONVERT(CHAR,GETDATE(),112)" : " AND Month(TglKirim)=" + Bulan + "  and YEAR(TglKirim)=" + Tahun;
            string strSQL = " SELECT * from PoPurchnMail " +
                             " WHERE RowStatus>-1 " + where + "  order by ID Desc ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPoPurchMail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPoPurchMail.Add(GenerateObject(sqlDataReader));
                }
            }

            return arrPoPurchMail;

        }

        public override ArrayList Retrieve()
        {
            string strSQL = "select * from PoPurchnMail order by ID Desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPoPurchMail = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPoPurchMail.Add(GenerateObject(sqlDataReader));
                }
            }

            return arrPoPurchMail;
        }

        public PoPurchMail GenerateObject(SqlDataReader sqlDataReader)
        {
            objPoPurchMail = new PoPurchMail();
            objPoPurchMail.ID = Convert.ToInt32(sqlDataReader["ID"].ToString());
            objPoPurchMail.TglKirim = Convert.ToDateTime(sqlDataReader["TglKirim"].ToString());
            objPoPurchMail.NoPO = sqlDataReader["NoPO"].ToString();
            objPoPurchMail.SupplierName = sqlDataReader["SupplierName"].ToString();
            objPoPurchMail.Email = sqlDataReader["Email"].ToString();
            objPoPurchMail.Report = sqlDataReader["Report"].ToString();
            objPoPurchMail.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"].ToString());
            objPoPurchMail.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objPoPurchMail.CreatedTime = DateTime.Parse(sqlDataReader["CreatedTime"].ToString());
            return objPoPurchMail;
        }

    }
}
