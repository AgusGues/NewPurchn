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
    public class DepoFacade : AbstractFacade
    {
        private Depo objDepo = new Depo();
        private ArrayList arrDepo;
        private List<SqlParameter> sqlListParam;

        public DepoFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objDepo = (Depo)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@DepoCode", objDepo.DepoCode));
                sqlListParam.Add(new SqlParameter("@DepoName", objDepo.DepoName));
                sqlListParam.Add(new SqlParameter("@Address", objDepo.Address));
                sqlListParam.Add(new SqlParameter("@CityID", objDepo.CityID));
                sqlListParam.Add(new SqlParameter("@KabupatenID", objDepo.KabupatenID));
                sqlListParam.Add(new SqlParameter("@InitialToko", objDepo.InitialToko));
                sqlListParam.Add(new SqlParameter("@InitialToko2", objDepo.InitialToko2));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objDepo.CreatedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertDepo");

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
                objDepo = (Depo)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objDepo.ID));
                sqlListParam.Add(new SqlParameter("@DepoCode", objDepo.DepoCode));
                sqlListParam.Add(new SqlParameter("@DepoName", objDepo.DepoName));
                sqlListParam.Add(new SqlParameter("@Address", objDepo.Address));
                sqlListParam.Add(new SqlParameter("@CityID", objDepo.CityID));
                sqlListParam.Add(new SqlParameter("@KabupatenID", objDepo.KabupatenID));
                sqlListParam.Add(new SqlParameter("@InitialToko", objDepo.InitialToko));
                sqlListParam.Add(new SqlParameter("@InitialToko2", objDepo.InitialToko2));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objDepo.CreatedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateDepo");

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
                objDepo = (Depo)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objDepo.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objDepo.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteDepo");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.DepoCode,A.DepoName,A.Address,D.NamaPropinsi,A.CityID,B.CityName,A.KabupatenID,C.NamaKabupaten,A.InitialToko,A.InitialToko2,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from Depo as A,City as B,Kabupaten as C,Propinsi as D where A.CityID = B.ID and A.KabupatenID = C.ID and B.PropinsiID = D.ID and A.RowStatus = 0");
            strError = dataAccess.Error;
            arrDepo = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDepo.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrDepo.Add(new Depo());

            return arrDepo;
        }
        public ArrayList Retrieve2()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.DepoCode,A.DepoName,A.Address,D.NamaPropinsi,A.CityID,B.CityName,A.KabupatenID,C.NamaKabupaten,A.InitialToko,A.InitialToko2,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from Depo as A,City as B,Kabupaten as C,Propinsi as D where A.CityID = B.ID and A.KabupatenID = C.ID and B.PropinsiID = D.ID and A.RowStatus = 0 and A.Active>-1");
            strError = dataAccess.Error;
            arrDepo = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDepo.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrDepo.Add(new Depo());

            return arrDepo;
        }

        public Depo RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.DepoCode,A.DepoName,A.Address,D.NamaPropinsi,A.CityID,B.CityName,A.KabupatenID,C.NamaKabupaten,A.InitialToko,A.InitialToko2,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from Depo as A,City as B,Kabupaten as C,Propinsi as D where A.CityID = B.ID and A.KabupatenID = C.ID and B.PropinsiID = D.ID and A.RowStatus = 0 and A.ID = " + Id);
            strError = dataAccess.Error;
            
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Depo();
        }

        public ArrayList RetrieveByIdPusat()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.DepoCode,A.DepoName,A.Address,D.NamaPropinsi,A.CityID,B.CityName,A.KabupatenID,C.NamaKabupaten,A.InitialToko,A.InitialToko2,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from Depo as A,City as B,Kabupaten as C,Propinsi as D where A.CityID = B.ID and A.KabupatenID = C.ID and B.PropinsiID = D.ID and A.RowStatus = 0 and A.ID in (1,7)");
            strError = dataAccess.Error;
            arrDepo = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDepo.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrDepo.Add(new Depo());

            return arrDepo;
        }

        public Depo RetrieveByCode(string depoCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.DepoCode,A.DepoName,A.Address,D.NamaPropinsi,A.CityID,B.CityName,A.KabupatenID,C.NamaKabupaten,A.InitialToko,A.InitialToko2,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from Depo as A,City as B,Kabupaten as C,Propinsi as D where A.CityID = B.ID and A.KabupatenID = C.ID and B.PropinsiID = D.ID and A.RowStatus = 0 and A.DepoCode = '" + depoCode + "'");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Depo();
        }


        public Depo RetrieveByName(string depoName)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.DepoCode,A.DepoName,A.Address,D.NamaPropinsi,A.CityID,B.CityName,A.KabupatenID,C.NamaKabupaten,A.InitialToko,A.InitialToko2,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from Depo as A,City as B,Kabupaten as C,Propinsi as D where A.CityID = B.ID and A.KabupatenID = C.ID and B.PropinsiID = D.ID and A.RowStatus = 0 and A.DepoName = '" + depoName + "'");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Depo();
        }




        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.DepoCode,A.DepoName,A.Address,D.NamaPropinsi,A.CityID,B.CityName,A.KabupatenID,C.NamaKabupaten,A.InitialToko,A.InitialToko2,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from Depo as A,City as B,Kabupaten as C,Propinsi as D where A.CityID = B.ID and A.KabupatenID = C.ID and B.PropinsiID = D.ID and A.RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrDepo = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDepo.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrDepo.Add(new Depo());

            return arrDepo;
        }

        public ArrayList ByDepo2(int id1, int id2)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.DepoCode,A.DepoName,A.Address,D.NamaPropinsi,A.CityID,B.CityName,A.KabupatenID,C.NamaKabupaten,A.InitialToko,A.InitialToko2,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from Depo as A,City as B,Kabupaten as C,Propinsi as D where A.CityID = B.ID and A.KabupatenID = C.ID and B.PropinsiID = D.ID and A.RowStatus = 0 and A.ID = " + id1 + " or A.CityID = B.ID and A.KabupatenID = C.ID and B.PropinsiID = D.ID and A.RowStatus = 0 and A.ID = " + id2);
            strError = dataAccess.Error;
            arrDepo = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDepo.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrDepo.Add(new Depo());

            return arrDepo;
        }

        public Depo GenerateObject(SqlDataReader sqlDataReader)
        {
            objDepo = new Depo();
            objDepo.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objDepo.DepoCode = sqlDataReader["DepoCode"].ToString();
            objDepo.DepoName = sqlDataReader["DepoName"].ToString();
            objDepo.Address = sqlDataReader["Address"].ToString();
            objDepo.NamaPropinsi = sqlDataReader["NamaPropinsi"].ToString();
            objDepo.CityID = Convert.ToInt32(sqlDataReader["CityID"]);
            objDepo.CityName = sqlDataReader["CityName"].ToString();
            objDepo.KabupatenID = Convert.ToInt32(sqlDataReader["KabupatenID"]);
            objDepo.NamaKabupaten = sqlDataReader["NamaKabupaten"].ToString();
            objDepo.InitialToko = sqlDataReader["InitialToko"].ToString();
            objDepo.InitialToko2 = sqlDataReader["InitialToko2"].ToString();
            objDepo.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objDepo.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objDepo.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objDepo.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objDepo.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objDepo;

        }
    }
}

