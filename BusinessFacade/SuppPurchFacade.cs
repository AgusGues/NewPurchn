using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;
namespace BusinessFacade
{
    public class SuppPurchFacade : AbstractFacade
    {
        private SuppPurch objSuppPurch = new SuppPurch();
        private ArrayList arrSuppPurch;
        private List<SqlParameter> sqlListParam;
        //private DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        public SuppPurchFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objSuppPurch = (SuppPurch)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@SupplierCode", objSuppPurch.SupplierCode));
                sqlListParam.Add(new SqlParameter("@SupplierName", objSuppPurch.SupplierName));
                sqlListParam.Add(new SqlParameter("@Alamat", objSuppPurch.Alamat));
                sqlListParam.Add(new SqlParameter("@UP", objSuppPurch.UP));
                sqlListParam.Add(new SqlParameter("@Telepon", objSuppPurch.Telepon));
                sqlListParam.Add(new SqlParameter("@Fax", objSuppPurch.Fax));
                sqlListParam.Add(new SqlParameter("@Handphone", objSuppPurch.Handphone));
                sqlListParam.Add(new SqlParameter("@NPWP", objSuppPurch.NPWP));
                sqlListParam.Add(new SqlParameter("@JoinDate", objSuppPurch.JoinDate));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objSuppPurch.CreatedBy));
                sqlListParam.Add(new SqlParameter("@EMail", objSuppPurch.EMail));
                sqlListParam.Add(new SqlParameter("@PKP", objSuppPurch.PKP));
                sqlListParam.Add(new SqlParameter("@Flag", objSuppPurch.Flag));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertSuppPurch");

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
                objSuppPurch = (SuppPurch)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objSuppPurch.ID));
                sqlListParam.Add(new SqlParameter("@SupplierCode", objSuppPurch.SupplierCode));
                sqlListParam.Add(new SqlParameter("@SupplierName", objSuppPurch.SupplierName));
                sqlListParam.Add(new SqlParameter("@Alamat", objSuppPurch.Alamat));
                sqlListParam.Add(new SqlParameter("@UP", objSuppPurch.UP));
                sqlListParam.Add(new SqlParameter("@Telepon", objSuppPurch.Telepon));
                sqlListParam.Add(new SqlParameter("@Fax", objSuppPurch.Fax));
                sqlListParam.Add(new SqlParameter("@Handphone", objSuppPurch.Handphone));
                sqlListParam.Add(new SqlParameter("@NPWP", objSuppPurch.NPWP));
                sqlListParam.Add(new SqlParameter("@JoinDate", objSuppPurch.JoinDate));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objSuppPurch.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@EMail", objSuppPurch.EMail ));
                sqlListParam.Add(new SqlParameter("@PKP", objSuppPurch.PKP));
                sqlListParam.Add(new SqlParameter("@flag", objSuppPurch.Flag));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateSuppPurch");
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
                objSuppPurch = (SuppPurch)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objSuppPurch.ID));
                sqlListParam.Add(new SqlParameter("@Aktif", objSuppPurch.Aktif));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objSuppPurch.LastModifiedBy));
                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteSuppPurch");
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
            string strSQL = "select A.ID,A.SupplierCode,A.SupplierName,A.Alamat,A.UP,A.Telepon,A.Fax,A.Handphone,A.NPWP,A.JoinDate," +
                           "A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,Email,flag,PKP,ISNULL(Aktif,0)Aktif, " +
                           "ISNULL(A.SubCompanyID,0)SubCompanyID "+
                           "from SuppPurch as A where RowStatus=0 order by a.SupplierName";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSuppPurch = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuppPurch.Add(GenerateObjectNew(GenerateObject(sqlDataReader), sqlDataReader));
                }
            }
            else
                arrSuppPurch.Add(new Supplier());

            return arrSuppPurch;
        }
        public ArrayList RetrieveAll()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select A.ID,A.SupplierCode,A.SupplierName,A.Alamat,A.UP,A.Telepon,A.Fax,A.Handphone,A.NPWP,A.JoinDate," +
                            "A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,Email,flag,PKP,ISNULL(Aktif,0)Aktif " +
                            "from SuppPurch as A where RowStatus=0 order by A.SupplierName";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSuppPurch = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuppPurch.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuppPurch.Add(new Supplier());

            return arrSuppPurch;
        }

        public ArrayList Retrieve2()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select top 50 ID,SupplierCode,SupplierName,Alamat,UP,Telepon,Fax,Handphone,NPWP,JoinDate,"+
                            "RowStatus,CreatedBy,CreatedTime,LastModifiedBy,LastModifiedTime,Email,flag,PKP,ISNULL(Aktif,0)Aktif, " +
                            "ISNULL(SubCompanyID,0)SubCompanyID " +
                            "from SuppPurch where RowStatus >-1 order by ID desc";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSuppPurch = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuppPurch.Add(GenerateObjectNew(GenerateObject(sqlDataReader), sqlDataReader));
                }
            }
            else
                arrSuppPurch.Add(new SuppPurch());
            return arrSuppPurch;
        }

        public SuppPurch RetrieveById(int Id)
        {

            string strSQL = "select A.ID,A.SupplierCode,A.SupplierName,A.Alamat,A.UP,A.Telepon,A.Fax,A.Handphone,A.NPWP," +
                            "A.JoinDate,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.Email, "+
                            "flag,PKP,ISNULL(Aktif,0)Aktif,ISNULL(SubCompanyID,0)SubCompanyID " +
                            "from SuppPurch as A where RowStatus >-1 and A.ID = " + Id;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSuppPurch = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectNew(GenerateObject(sqlDataReader), sqlDataReader);
                }
            }
            return new SuppPurch();
        }
        
        public int CountSupplier()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT MAX(SUBSTRING(SupplierCode,3,4)) as id from SuppPurch");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Id"]);
                }
            }

            return 0;
        }


        public SuppPurch RetrieveByCode(string strSupplierCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select A.ID,A.SupplierCode,A.SupplierName,A.Alamat,A.UP,A.Telepon,A.Fax,A.Handphone,A.NPWP,A.JoinDate,"+
                            "A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.Email,flag,PKP "+
                            "from SuppPurch as A where RowStatus=0 and A.SupplierCode = '" + strSupplierCode + "'";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSuppPurch = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new SuppPurch();
        }

        public ArrayList RetrieveBySupplierIDBaru()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select Supp.ID,SupplierCode,SupplierName,SaldoSupplier.SupplierID from (select ID,SupplierCode,SupplierName,Email,flag from SuppPurch where RowStatus>-1) as Supp " +
                "left join SaldoSupplier on SaldoSupplier.SupplierID=Supp.ID where SupplierID is null";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSuppPurch = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuppPurch.Add(GenerateObjectNew(sqlDataReader));
                }
            }
            else
                arrSuppPurch.Add(new SuppPurch());

            return arrSuppPurch;
        }

        public SuppPurch GenerateObjectNew(SqlDataReader sqlDataReader)
        {
            objSuppPurch = new SuppPurch();
            objSuppPurch.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objSuppPurch.SupplierCode = sqlDataReader["SupplierCode"].ToString();
            objSuppPurch.SupplierName = sqlDataReader["SupplierName"].ToString();

            return objSuppPurch;
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL="select A.ID,A.SupplierCode,A.SupplierName,A.Alamat,A.UP,A.Telepon,A.Fax,A.Handphone,A.NPWP,A.JoinDate,"+
                          "A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.Email,flag,PKP,ISNULL(Aktif,0)Aktif, "+
                          "ISNULL(A.SubCompanyID,0)SubCompanyID " +
                          "from SuppPurch as A where RowStatus=0 and " + strField + " like '%" + strValue + "%'";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSuppPurch = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuppPurch.Add(GenerateObjectNew(GenerateObject(sqlDataReader), sqlDataReader));
                }
            }
            else
                arrSuppPurch.Add(new SuppPurch ());

            return arrSuppPurch;
        }

        public SuppPurch GenerateObject(SqlDataReader sqlDataReader)
        {
            objSuppPurch = new SuppPurch();
            objSuppPurch.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objSuppPurch.SupplierCode = sqlDataReader["SupplierCode"].ToString();
            objSuppPurch.SupplierName = sqlDataReader["SupplierName"].ToString();
            objSuppPurch.Alamat = sqlDataReader["Alamat"].ToString();
            objSuppPurch.UP = sqlDataReader["UP"].ToString();
            objSuppPurch.Telepon = sqlDataReader["Telepon"].ToString();
            objSuppPurch.Fax = sqlDataReader["Fax"].ToString();
            objSuppPurch.Handphone = sqlDataReader["Handphone"].ToString();
            objSuppPurch.NPWP = sqlDataReader["NPWP"].ToString();
            objSuppPurch.JoinDate = (sqlDataReader["JoinDate"]==DBNull.Value)?DateTime.MinValue: Convert.ToDateTime(sqlDataReader["JoinDate"]);
            objSuppPurch.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objSuppPurch.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objSuppPurch.CreatedTime = (sqlDataReader["CreatedTime"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objSuppPurch.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objSuppPurch.LastModifiedTime =(sqlDataReader["LastModifiedTime"]==DBNull.Value)?DateTime.MinValue:
                Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            objSuppPurch.EMail = sqlDataReader["Email"].ToString();
            objSuppPurch.Flag =(sqlDataReader["flag"]==DBNull.Value)?0: Convert.ToInt32(sqlDataReader["flag"].ToString());
            objSuppPurch.PKP = (sqlDataReader["PKP"] == DBNull.Value) ? "" : sqlDataReader["PKP"].ToString();
            return objSuppPurch;
        }
        private SuppPurch GenerateObjectNew(SuppPurch obj, SqlDataReader sdr)
        {
            objSuppPurch = (SuppPurch)obj;
            objSuppPurch.Aktif = Convert.ToInt32(sdr["Aktif"].ToString());
            objSuppPurch.SubCompanyID = int.Parse(sdr["SubCompanyID"].ToString());
            return objSuppPurch;
        }
        /**
         * Added on 30-06-2015
         * For Default TermOfPay dropdown selected
         **/
        public string TermOfPayment(string SupplierID)
        {
            string result = string.Empty;
            string query = "Select Top 1 Termin from POPurchn where SupplierID=" + SupplierID + " order by ID desc";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(query);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = sdr["Termin"].ToString();
                }
            }
            return result;
        }
        /**
         * Get SubCompanyID for supplier kertas
         * added on 26-01-2016
         */
        public int SubCompanyID(int SupplierID)
        {
            int result = 0;
            string query = "Select ISNULL(SubCompanyID,0) Coi from SuppPurch where ID=" + SupplierID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(query);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = int.Parse(sdr["Coi"].ToString());
                }
            }
            return result;
        }
        public int SubCompanyID(int SupplierID, bool For)
        {
            int result = 0;
            string query = "Select ISNULL(ForDK,0) Coi from SuppPurch where ID=" + SupplierID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(query);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = int.Parse(sdr["Coi"].ToString());
                }
            }
            return result;
        }
    }
}
