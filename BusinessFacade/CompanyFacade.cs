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
    public class CompanyFacade : AbstractFacade
    {
        private Company objCompany = new Company();
        private ArrayList arrCompany;
        private List<SqlParameter> sqlListParam;


        public CompanyFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objCompany = (Company)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@KodeLokasi", objCompany.KodeLokasi));
                sqlListParam.Add(new SqlParameter("@Lokasi", objCompany.Lokasi));
                sqlListParam.Add(new SqlParameter("@Nama", objCompany.Nama));
                sqlListParam.Add(new SqlParameter("@Alamat1", objCompany.Alamat1));
                sqlListParam.Add(new SqlParameter("@Alamat2", objCompany.Alamat2));
                sqlListParam.Add(new SqlParameter("@Manager", objCompany.Manager));
                sqlListParam.Add(new SqlParameter("@spv", objCompany.Spv));
                sqlListParam.Add(new SqlParameter("@DepoID", objCompany.DepoID));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objCompany.CreatedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objCompany.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertCompany");

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
                objCompany = (Company)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objCompany.ID));
                sqlListParam.Add(new SqlParameter("@KodeLokasi", objCompany.KodeLokasi));
                sqlListParam.Add(new SqlParameter("@Lokasi", objCompany.Lokasi));
                sqlListParam.Add(new SqlParameter("@Nama", objCompany.Nama));
                sqlListParam.Add(new SqlParameter("@Alamat1", objCompany.Alamat1));
                sqlListParam.Add(new SqlParameter("@Alamat2", objCompany.Alamat2));
                sqlListParam.Add(new SqlParameter("@Manager", objCompany.Manager));
                sqlListParam.Add(new SqlParameter("@spv", objCompany.Spv));
                sqlListParam.Add(new SqlParameter("@DepoID", objCompany.DepoID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objCompany.CreatedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedTime", objCompany.LastModifiedTime));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateCompany");

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
                objCompany = (Company)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objCompany.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objCompany.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteCompany");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public string OrderBy { get; set; }
        public string Where { get; set; }
        public override ArrayList Retrieve()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select * from Company where Company.RowStatus = 0 " + this.Where + " " + this.OrderBy;
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrCompany = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrCompany.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrCompany.Add(new Company());

            return arrCompany;
        }
        //add start by Razib 16-11-2022
        public int RetrieveTotalSHareUPD(int ExiD, int deptID)
        {
            string where = (deptID == 23) ? "" : " and Dept=" + deptID;
            string StrSql = "select count(id)TotalShare from ISO_UpdDMD where PlantID <>" + ExiD + " and StatusShare=1 and RowStatus>-1 " + where + " ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["TotalShare"]);
                }
            }

            return 0;
        }
        //end
        public Company RetrieveById(int Id)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from Company where Company.depoID = " + Id);
            strError = dataAccess.Error;
            arrCompany = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Company();
        }

        public Company RetrieveByDepoIdNonGrc(int SubCompanyId)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from Company where ID = " + SubCompanyId);
            strError = dataAccess.Error;
            arrCompany = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Company();
        }

        public Company RetrieveByDepoIdNonGRC(int Id)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from Company where Company.ID = " + Id);
            strError = dataAccess.Error;
            arrCompany = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Company();
        }

        public Company RetrieveByDepoId(int Id)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from Company where Company.DepoID = " + Id);
            strError = dataAccess.Error;
            arrCompany = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Company();
        }

        public Company RetrieveByCode(string kodeLokasi)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from Company where Company.KodeLokasi = '" + kodeLokasi + "'");
            strError = dataAccess.Error;
            arrCompany = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Company();
        }


        
        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from Company where Company.RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrCompany = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrCompany.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrCompany.Add(new Company());

            return arrCompany;
        }

        public string GetKodeCompany(int depoid)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.KodeLokasi as kodeLokasi from Company as A, Depo as B where B.ID = A.DepoID and B.ID = " + depoid);

            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return sqlDataReader["kodeLokasi"].ToString();
                }
            }

            return string.Empty;
        }

        public Company GenerateObject(SqlDataReader sqlDataReader)
        {
            objCompany = new Company();
            objCompany.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objCompany.KodeLokasi = sqlDataReader["KodeLokasi"].ToString();
            objCompany.Lokasi = sqlDataReader["Lokasi"].ToString();
            objCompany.Nama = sqlDataReader["Nama"].ToString();
            objCompany.Alamat1 = sqlDataReader["Alamat1"].ToString();
            objCompany.Alamat2 = sqlDataReader["Alamat2"].ToString();
            objCompany.Manager = sqlDataReader["Manager"].ToString();
            objCompany.Spv = sqlDataReader["Spv"].ToString();
            objCompany.DepoID = Convert.ToInt32(sqlDataReader["DepoID"]);
            objCompany.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objCompany.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objCompany.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objCompany.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objCompany;
        }
                
    }
}
