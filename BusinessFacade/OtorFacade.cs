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
    public class OtorFacade : AbstractFacade
    {
        private Otor objOtor = new Otor();
        private ArrayList arrOtor;
        private List<SqlParameter> sqlListParam;


        public OtorFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objOtor = (Otor)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Nama1", objOtor.Nama1));
                sqlListParam.Add(new SqlParameter("@Nama2", objOtor.Nama2));
                sqlListParam.Add(new SqlParameter("@Nama3", objOtor.Nama3));
                sqlListParam.Add(new SqlParameter("@NPWP", objOtor.NPWP));
                sqlListParam.Add(new SqlParameter("@BAPP", objOtor.BAPP));
                sqlListParam.Add(new SqlParameter("@SPV1", objOtor.SPV1));
                sqlListParam.Add(new SqlParameter("@ADM1", objOtor.ADM1));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objOtor.CreatedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objOtor.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertOtor");

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
                objOtor = (Otor)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objOtor.ID));
                sqlListParam.Add(new SqlParameter("@Nama1", objOtor.Nama1));
                sqlListParam.Add(new SqlParameter("@Nama2", objOtor.Nama2));
                sqlListParam.Add(new SqlParameter("@Nama3", objOtor.Nama3));
                sqlListParam.Add(new SqlParameter("@NPWP", objOtor.NPWP));
                sqlListParam.Add(new SqlParameter("@BAPP", objOtor.BAPP));
                sqlListParam.Add(new SqlParameter("@SPV1", objOtor.SPV1));
                sqlListParam.Add(new SqlParameter("@ADM1", objOtor.ADM1));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objOtor.CreatedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedTime", objOtor.LastModifiedTime));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateOtor");

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
                objOtor = (Otor)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objOtor.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objOtor.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteOtor");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from Otor");
            strError = dataAccess.Error;
            arrOtor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOtor.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrOtor.Add(new Otor());

            return arrOtor;
        }

        public Otor RetrieveById(int Id)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from Otor where Otor.ID = " + Id);
            strError = dataAccess.Error;
            arrOtor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Otor();
        }

        public Otor RetrieveByCode(string nama1)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 1 * from Otor where Otor.Nama1 = '" + nama1 + "' order by id");
            strError = dataAccess.Error;
            arrOtor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Otor();
        }
        public Otor RetrieveByCompanyID(int CID)
        {
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 1 * from Otor where Otor.ID = " + CID ) ;
            //Ada Penambahan Untuk Kebutuhan Di HO
            int CID2 = 0;
            if (CID == 8)
            {
                CID2 = CID;
            }
            else if (CID == 9)
            {
                CID2 = 9;
            }
            else if (CID == 3)
            {
                CID2 = 3;
            }
            else if (CID == 4)
            {
                CID2 = 4;
            }
            else
            {
                CID2 = 1;
            }
            //Ada Penambahan Untuk Kebutuhan Di HO

            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 1 * from Otor where CompanyID = " + CID);
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 1 * from Otor where CompanyID = " + CID2);
            strError = dataAccess.Error;
            arrOtor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Otor();
        }
        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from Otor where Otor.RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrOtor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOtor.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrOtor.Add(new Otor());

            return arrOtor;
        }

        public Otor GenerateObject(SqlDataReader sqlDataReader)
        {
            objOtor = new Otor();
            objOtor.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objOtor.Nama1 = sqlDataReader["Nama1"].ToString();
            objOtor.Nama2 = sqlDataReader["Nama2"].ToString();
            objOtor.Nama3 = sqlDataReader["Nama3"].ToString();
            objOtor.NPWP = sqlDataReader["NPWP"].ToString();
            objOtor.BAPP = Convert.ToInt32(sqlDataReader["BAPP"]);
            objOtor.SPV1 = sqlDataReader["SPV1"].ToString();
            objOtor.ADM1 = sqlDataReader["ADM1"].ToString();
            objOtor.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objOtor.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objOtor.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objOtor.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            objOtor.SPPKP = sqlDataReader["SPPKP"].ToString();
            return objOtor;
        }
    }
}
