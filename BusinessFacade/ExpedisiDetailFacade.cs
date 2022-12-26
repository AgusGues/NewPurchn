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
    public class ExpedisiDetailFacade : AbstractTransactionFacade
    {
        private ExpedisiDetail objExpedisiDetail = new ExpedisiDetail();
        private ArrayList arrCarType;
        private List<SqlParameter> sqlListParam;

        public ExpedisiDetailFacade(object objDomain)
            : base(objDomain)
        {
            objExpedisiDetail = (ExpedisiDetail)objDomain;
        }

        public ExpedisiDetailFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ExpedisiID", objExpedisiDetail.ExpedisiID));
                sqlListParam.Add(new SqlParameter("@CarType", objExpedisiDetail.CarType));
                sqlListParam.Add(new SqlParameter("@Kubikasi", objExpedisiDetail.Kubikasi));
                sqlListParam.Add(new SqlParameter("@NoPolisi", objExpedisiDetail.NoPolisi));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertExpedisiDetail");

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
                sqlListParam.Add(new SqlParameter("@ID", objExpedisiDetail.ID));
                sqlListParam.Add(new SqlParameter("@CarType", objExpedisiDetail.CarType));
                sqlListParam.Add(new SqlParameter("@Kubikasi", objExpedisiDetail.Kubikasi));
                sqlListParam.Add(new SqlParameter("@NoPolisi", objExpedisiDetail.NoPolisi));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateExpedisiDetail");

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
                objExpedisiDetail = (ExpedisiDetail)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ExpedisiID", objExpedisiDetail.ExpedisiID));

                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteExpedisiDetail");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ExpedisiDetail ");
            strError = dataAccess.Error;
            arrCarType = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrCarType.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrCarType.Add(new ExpedisiDetail());

            return arrCarType;
        }
        public ArrayList RetrieveSemua()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Expedisi.ID,ExpedisiDetail.ID as ExpedisiDetailID,CarType,DepoID from Expedisi,ExpedisiDetail where RowStatus = 0 and DepoID in (1,7) and Expedisi.ID=ExpedisiDetail.ExpedisiID order by Expedisi.ID, DepoID, CarType ");
            strError = dataAccess.Error;
            arrCarType = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrCarType.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrCarType.Add(new ExpedisiDetail());

            return arrCarType;
        }
        public ExpedisiDetail RetrieveByNoPolisi(String strSched)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select ExpedisiDetail.* from Schedule,ExpedisiDetail where Schedule.ScheduleNo = '" + strSched + "'and Schedule.ExpedisiDetailID = ExpedisiDetail.ID");
            strError = dataAccess.Error;
            arrCarType = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new ExpedisiDetail();
        }

        public ArrayList RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ExpedisiDetail where ExpedisiID = " + Id + " order by CarType");
            strError = dataAccess.Error;
            arrCarType = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrCarType.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrCarType.Add(new ExpedisiDetail());

            return arrCarType;
        }

        public ExpedisiDetail RetrieveKubikasi(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ExpedisiDetail where ID = " + Id);
            strError = dataAccess.Error;
            arrCarType = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new ExpedisiDetail();
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ExpedisiDetail where " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrCarType = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrCarType.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrCarType.Add(new ExpedisiDetail());

            return arrCarType;
        }


        public ExpedisiDetail GenerateObject(SqlDataReader sqlDataReader)
        {
            objExpedisiDetail = new ExpedisiDetail();
            objExpedisiDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objExpedisiDetail.ExpedisiID = Convert.ToInt32(sqlDataReader["ExpedisiID"]);
            objExpedisiDetail.CarType = sqlDataReader["CarType"].ToString();
            objExpedisiDetail.Kubikasi = decimal.Parse(sqlDataReader["Kubikasi"].ToString());
            objExpedisiDetail.NoPolisi = sqlDataReader["NoPolisi"].ToString();
            objExpedisiDetail.MinimalMuatan = decimal.Parse(sqlDataReader["JumlahMuatan"].ToString());

            return objExpedisiDetail;
        }
        public ExpedisiDetail GenerateObject2(SqlDataReader sqlDataReader)
        {
            objExpedisiDetail = new ExpedisiDetail();
            objExpedisiDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objExpedisiDetail.ExpedisiDetailID = Convert.ToInt32(sqlDataReader["ExpedisiDetailID"]);
            objExpedisiDetail.CarType = sqlDataReader["CarType"].ToString();
            objExpedisiDetail.DepoID = Convert.ToInt32(sqlDataReader["DepoID"]);

            return objExpedisiDetail;
        }



    }
}

