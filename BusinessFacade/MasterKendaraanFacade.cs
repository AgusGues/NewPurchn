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
    public class MasterKendaraanFacade : AbstractTransactionFacade
    {
        private MasterKendaraan objKendaraan = new MasterKendaraan();
        private ArrayList arrKendaraan;
        private List<SqlParameter> sqlListParam;

        public MasterKendaraanFacade(object objDomain)
            : base(objDomain)
        {
            objKendaraan = (MasterKendaraan)objDomain;
        }

        public MasterKendaraanFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@NoPolisi", objKendaraan.NoPolisi));
                sqlListParam.Add(new SqlParameter("@JenisMobil", objKendaraan.JenisMobil));
                sqlListParam.Add(new SqlParameter("@ExpedisiID", objKendaraan.ExpedisiID));
                sqlListParam.Add(new SqlParameter("@ExpedisiName", objKendaraan.ExpedisiName));
                sqlListParam.Add(new SqlParameter("@ExpedisiDetailID", objKendaraan.ExpedisiDetailID));
                sqlListParam.Add(new SqlParameter("@Target", objKendaraan.Target));
                sqlListParam.Add(new SqlParameter("@Status", objKendaraan.Status));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objKendaraan.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertMasterKendaraan");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public override int Update(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objKendaraan.ID));
                sqlListParam.Add(new SqlParameter("@NoPolisi", objKendaraan.NoPolisi));
                sqlListParam.Add(new SqlParameter("@JenisMobil", objKendaraan.JenisMobil));
                sqlListParam.Add(new SqlParameter("@ExpedisiID", objKendaraan.ExpedisiID));
                sqlListParam.Add(new SqlParameter("@ExpedisiName", objKendaraan.ExpedisiName));
                sqlListParam.Add(new SqlParameter("@ExpedisiDetailID", objKendaraan.ExpedisiDetailID));
                sqlListParam.Add(new SqlParameter("@Target", objKendaraan.Target));
                sqlListParam.Add(new SqlParameter("@Status", objKendaraan.Status));
                // sqlListParam.Add(new SqlParameter("@AlasanCancel", objLoading.AlasanCancel));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objKendaraan.CreatedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateMasterKendaraan");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Delete(TransactionManager transManager)
        {

            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objKendaraan.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objKendaraan.LastModifiedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spCancelMasterKendaraan");

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
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ExpedisiName from MasterKendaraan as A where Status>-1");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from MasterKendaraan where Status>-1 order by JenisMobil ");
            strError = dataAccess.Error;
            arrKendaraan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrKendaraan.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrKendaraan.Add(new MasterKendaraan());

            return arrKendaraan;
        }
        //public override ArrayList RetrieveAll()
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ExpedisiName from MasterKendaraan as A where Status>-1");
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from MasterKendaraan where Status>-1");
        //    strError = dataAccess.Error;
        //    arrKendaraan = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrKendaraan.Add(GenerateObject(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrKendaraan.Add(new MasterKendaraan());

        //    return arrKendaraan;
        //}

        //public ArrayList RetrieveByDepo(int depoID)
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from Expedisi where RowStatus = 0 and DepoID = " + depoID);
        //    strError = dataAccess.Error;
        //    arrExpedisi = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrExpedisi.Add(GenerateObject(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrExpedisi.Add(new Expedisi());

        //    return arrExpedisi;
        //}

        public MasterKendaraan RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from MasterKendaraan where Status > -1 0 and ID = " + Id);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT B.ID, B.LoadingNo, B.Tanggal, B.NoPolisi, A.JenisMobil, B.EkspedisiName, B.TimeIn, B.TimeOut, A.Target, B.KendaraanID, B.Keterangan, B.CreatedBy," + 
            //" B.CreatedTime, B.LastModifiedBy, B.LastModifiedTime FROM MasterKendaraan AS A INNER JOIN LoadingTime AS B ON A.ID = B.KendaraanID and B.ID = " + Id);
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT A.ID, A.JenisMobil, A.Target, A.Status, A.CreatedBy, A.CreatedTime, A.LastModifiedBy, A.LastModifiedTime, A.NoPolisi, A.ExpedisiID, A.ExpedisiName, "+
            "A.ExpedisiDetailID, B.ExpedisiName AS Expr2, C.ExpedisiID AS Expr3 FROM MasterKendaraan AS A INNER JOIN Expedisi AS B ON A.ExpedisiID = B.ID "+
            "INNER JOIN ExpedisiDetail AS C ON B.ID = C.ID where A.Status>-1 and A.ID=" + Id);

            strError = dataAccess.Error;
            arrKendaraan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new MasterKendaraan();
        }

        public MasterKendaraan RetrieveByName(string jenisKendaraan)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from MasterKendaraan where Status > -1 and ExpedisiName = '" + jenisKendaraan + "'");
            strError = dataAccess.Error;
            arrKendaraan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new MasterKendaraan();
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.NoPolisi,A.Target,A.JenisMobil,A.ExpedisiID,A.ExpedisiDetailID,A.ExpedisiName,A.Status,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from MasterKendaraan as A where Status = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrKendaraan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrKendaraan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrKendaraan.Add(new MasterKendaraan());

            return arrKendaraan;
        }

        public MasterKendaraan GenerateObject(SqlDataReader sqlDataReader)
        {
            objKendaraan = new MasterKendaraan();
            objKendaraan.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objKendaraan.NoPolisi = sqlDataReader["NoPolisi"].ToString();
            objKendaraan.JenisMobil = sqlDataReader["JenisMobil"].ToString();
            objKendaraan.ExpedisiID = Convert.ToInt32(sqlDataReader["ExpedisiID"]);
            objKendaraan.ExpedisiName = sqlDataReader["ExpedisiName"].ToString();
            objKendaraan.ExpedisiDetailID = Convert.ToInt32(sqlDataReader["ExpedisiDetailID"]);
            objKendaraan.Target = Convert.ToDecimal(sqlDataReader["Target"]);
            objKendaraan.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objKendaraan.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objKendaraan.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objKendaraan.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objKendaraan.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objKendaraan;

        }

        public MasterKendaraan GenerateObject2(SqlDataReader sqlDataReader)
        {
            objKendaraan = new MasterKendaraan();
            objKendaraan.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objKendaraan.NoPolisi = sqlDataReader["NoPolisi"].ToString();
            objKendaraan.JenisMobil = sqlDataReader["JenisMobil"].ToString();
            objKendaraan.ExpedisiID = Convert.ToInt32(sqlDataReader["ExpedisiID"]);
            objKendaraan.ExpedisiName = sqlDataReader["ExpedisiName"].ToString();
            objKendaraan.ExpedisiDetailID = Convert.ToInt32(sqlDataReader["ExpedisiDetailID"]);
            objKendaraan.Target = Convert.ToDecimal(sqlDataReader["Target"]);
            objKendaraan.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objKendaraan.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objKendaraan.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objKendaraan.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objKendaraan.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objKendaraan;

        }



       
    }

}
