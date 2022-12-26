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
    public class LoadingTimeFacade : AbstractFacade
    {
        private LoadingTime objLoading = new LoadingTime();
        private ArrayList arrLoading;
        private List<SqlParameter> sqlListParam;

        public LoadingTimeFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objLoading = (LoadingTime)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@LoadingNo", objLoading.LoadingNo));
                sqlListParam.Add(new SqlParameter("@UrutanNo", objLoading.NoUrut));
                sqlListParam.Add(new SqlParameter("@TglIn", objLoading.TglIn));
                sqlListParam.Add(new SqlParameter("@TimeIn", objLoading.TimeIn));
                if (objLoading.TimeOut.ToShortDateString() == "01/01/0001")
                {
                    sqlListParam.Add(new SqlParameter("@TimeOut", Convert.ToDateTime("01/01/1900")));
                }
                else
                {
                    sqlListParam.Add(new SqlParameter("@TimeOut", objLoading.TimeOut));
                }
                sqlListParam.Add(new SqlParameter("@KendaraanID", objLoading.KendaraanID));
                sqlListParam.Add(new SqlParameter("@MobilSendiri", objLoading.MobilSendiri));
                sqlListParam.Add(new SqlParameter("@EkspedisiID", objLoading.EkspedisiID));
                sqlListParam.Add(new SqlParameter("@EkspedisiName", objLoading.EkspedisiName));
                sqlListParam.Add(new SqlParameter("@NoPolisi", objLoading.NoPolisi));
                sqlListParam.Add(new SqlParameter("@Status", objLoading.Status));
                sqlListParam.Add(new SqlParameter("@Keterangan", objLoading.Keterangan));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objLoading.CreatedBy));

                sqlListParam.Add(new SqlParameter("@Ritase", objLoading.Ritase));
                sqlListParam.Add(new SqlParameter("@CardID", objLoading.CardID));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertLoadingTime");

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
                objLoading = (LoadingTime)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objLoading.ID));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objLoading.CreatedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objLoading.CreatedBy));
                sqlListParam.Add(new SqlParameter("@MobilSendiri", objLoading.MobilSendiri));
                sqlListParam.Add(new SqlParameter("@NoPolisi", objLoading.NoPolisi));
                sqlListParam.Add(new SqlParameter("@KendaraanID", objLoading.KendaraanID));
                sqlListParam.Add(new SqlParameter("@Status", objLoading.Status));
                sqlListParam.Add(new SqlParameter("@Ritase", objLoading.Ritase));
                sqlListParam.Add(new SqlParameter("@Keterangan", objLoading.Keterangan));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateLoadingTime");

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
                objLoading = (LoadingTime)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objLoading.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objLoading.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spCancelLoadingTime");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateFromDevice(object objDomain)
        {
            try
            {
                objLoading = (LoadingTime)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objLoading.ID));
                sqlListParam.Add(new SqlParameter("@TimeOut", objLoading.TimeOut));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateLoadingTimeDevice");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateLoadingtimeBySJ(object objDomain)
        {
            try
            {
                objLoading = (LoadingTime)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objLoading.ID));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objLoading.CreatedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objLoading.CreatedBy));
                sqlListParam.Add(new SqlParameter("@MobilSendiri", objLoading.MobilSendiri));
                sqlListParam.Add(new SqlParameter("@TglOut", objLoading.TglOut));
                sqlListParam.Add(new SqlParameter("@TimeOut", objLoading.TimeOut));
                sqlListParam.Add(new SqlParameter("@NoPolisi", objLoading.NoPolisi));
                sqlListParam.Add(new SqlParameter("@KendaraanID", objLoading.KendaraanID));
                sqlListParam.Add(new SqlParameter("@Status", objLoading.Status));
                sqlListParam.Add(new SqlParameter("@Ritase", objLoading.Ritase));
                sqlListParam.Add(new SqlParameter("@Keterangan", objLoading.Keterangan));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateLoadingTimeBySJ");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateLoadingtimeBySJ1(object objDomain)
        {
            try
            {
                objLoading = (LoadingTime)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objLoading.ID));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objLoading.CreatedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objLoading.CreatedBy));
                sqlListParam.Add(new SqlParameter("@MobilSendiri", objLoading.MobilSendiri));
                sqlListParam.Add(new SqlParameter("@TglOut", objLoading.TglOut));
                sqlListParam.Add(new SqlParameter("@TimeOut", objLoading.TimeOut));
                sqlListParam.Add(new SqlParameter("@UrutanNo", objLoading.NoUrut));
                sqlListParam.Add(new SqlParameter("@TglIn", objLoading.TglIn ));
                sqlListParam.Add(new SqlParameter("@TimeIn", objLoading.TimeIn));                
                sqlListParam.Add(new SqlParameter("@NoPolisi", objLoading.NoPolisi));
                sqlListParam.Add(new SqlParameter("@KendaraanID", objLoading.KendaraanID));
                sqlListParam.Add(new SqlParameter("@Status", objLoading.Status));
                sqlListParam.Add(new SqlParameter("@Ritase", objLoading.Ritase));
                sqlListParam.Add(new SqlParameter("@Keterangan", objLoading.Keterangan));
                sqlListParam.Add(new SqlParameter("@TKirim", objLoading.TKirim));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateLoadingTimeBySJ1");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int InsertLoadingTimeDeviceOut(object objDomain)
        {
            try
            {
                objLoading = (LoadingTime)objDomain;
                sqlListParam = new List<SqlParameter>();
                

                    sqlListParam.Add(new SqlParameter("@CardNo", objLoading.CardNo));
                    sqlListParam.Add(new SqlParameter("@CheckTime", objLoading.Tanggal));
                    sqlListParam.Add(new SqlParameter("@ID", objLoading.ID));
                    sqlListParam.Add(new SqlParameter("@Flag", objLoading.Flag));
                    sqlListParam.Add(new SqlParameter("@CreatedBy", objLoading.CreatedBy));

                    int intResult = dataAccess.ProcessData(sqlListParam, "spInsertLoadingTimeDeviceOut");

                    strError = dataAccess.Error;

                    return intResult;
                }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int InsertLoadingTimeDeviceIn(object objDomain)
        {
            try
            {
                objLoading = (LoadingTime)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@CardNo", objLoading.CardNo));
                sqlListParam.Add(new SqlParameter("@CheckTime", objLoading.Tanggal));

                sqlListParam.Add(new SqlParameter("@CardID", objLoading.CardID));
                sqlListParam.Add(new SqlParameter("@LoadingNo", objLoading.LoadingNo));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objLoading.CreatedBy));
                sqlListParam.Add(new SqlParameter("@Flag", objLoading.Flag));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertLoadingTimeDeviceIn");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int InsertLoadingTime1D(object objDomain)
        {
            try
            {
                objLoading = (LoadingTime)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@CardNo", objLoading.CardNo));
                sqlListParam.Add(new SqlParameter("@TimeIn", objLoading.Tanggal));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objLoading.CreatedBy));
                int result = dataAccess.ProcessData(sqlListParam, "spLoadingTime1D_insert");
                strError = dataAccess.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateLoadingTime1D(object objDomain)
        {
            try
            {
                objLoading = (LoadingTime)objDomain;
                sqlListParam=new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID",objLoading.ID));
                sqlListParam.Add(new SqlParameter("@TimeOut",objLoading.TglOut));
                sqlListParam.Add(new SqlParameter("@RowStatus",objLoading.RowStatus));
                int result=dataAccess.ProcessData(sqlListParam,"spLoadingTime1D_Update");
                strError=dataAccess.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public override ArrayList Retrieve()
        {
            string strSQL = "select * from LoadingTime where Status = 0";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrLoading = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrLoading.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrLoading.Add(new LoadingTime());

            return arrLoading;
        }
        public ArrayList RetrieveAll()
        {
            string strSQL = "SELECT A.ID,A.UrutanNo ,A.NoPolisi,A.MobilSendiri ,A.TglIn,A.TglOut, A.EkspedisiName,B.JenisMobil," +
                     " A.TimeIn, A.TimeOut,B.Target,A.LoadingNo,A.Status,A.Keterangan FROM LoadingTime AS A INNER JOIN MasterKendaraan AS B " +
                     " ON A.KendaraanID = B.ID where isnull(convert(char,A.Timein,112),'19000101') <>'19000101'  and A.Status > -1 and A.TimeOut is null order by ID asc";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrLoading = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrLoading.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrLoading.Add(new LoadingTime());

            return arrLoading;
        }
        public ArrayList RetrieveAll3(string Criteria)
        {
            string Where = (Criteria != string.Empty) ? Criteria + " and Status >-1" : "(TimeOut is null or KendaraanID is null or KendaraanID=0) ad isnull(timein,'1/1/1900')<>'1/1/1900' and status >-1";
            string strSQL = "SELECT top 100 *,(select JenisMobil From MasterKendaraan where ID=KendaraanID) as JenisMobil "+
                            "from LoadingTime as a where "+Where+" order by ID Desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrLoading = new ArrayList();
            DateTime tglOut = DateTime.MinValue;
            int n = 0;
            if (strError==string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    n = n + 1;
                    tglOut = (sqlDataReader["TglOut"] != DBNull.Value) ? Convert.ToDateTime(sqlDataReader["TglOut"].ToString()) : DateTime.MinValue;
                    tglOut = (tglOut.ToShortDateString() == "01/01/1900") ? DateTime.MinValue : tglOut;
                    //arrLoading.Add(GenerateObject(sqlDataReader));
                    arrLoading.Add(new LoadingTime
                    {
                        
                        ID = Convert.ToInt32(sqlDataReader["ID"].ToString()),
                        NoUrut = sqlDataReader["UrutanNo"].ToString(),
                        //TimeIn = Convert.ToDateTime(sqlDataReader["TimeIn"].ToString()),
                        TimeIn = (sqlDataReader["TimeIn"] != DBNull.Value) ? Convert.ToDateTime(sqlDataReader["TimeIn"].ToString()) : DateTime.MinValue,
                        TglIn=  Convert.ToDateTime(sqlDataReader["TglIn"].ToString()),
                        TglKeluar =(tglOut==DateTime.MinValue)? string.Empty:tglOut.ToString(),
                        JenisMobil = (sqlDataReader["JenisMobil"] != DBNull.Value) ? sqlDataReader["JenisMobil"].ToString() : string.Empty,
                        KendaraanID = (sqlDataReader["KendaraanID"] != DBNull.Value) ? Convert.ToInt32(sqlDataReader["KendaraanID"].ToString()) : 0,
                        ExpedisiName = (sqlDataReader["MobilSendiri"] != DBNull.Value) ? sqlDataReader["MobilSendiri"].ToString() : "0",
                        NoPolisi = (sqlDataReader["NoPolisi"] != DBNull.Value) ? sqlDataReader["NoPolisi"].ToString() : string.Empty,
                        Keterangan = (sqlDataReader["Keterangan"] != DBNull.Value) ? sqlDataReader["Keterangan"].ToString() : string.Empty,
                        Flag = n
                    });
                    
                }
            }
            else
                arrLoading.Add(new LoadingTime());

            return arrLoading;
        }
        public ArrayList RetrieveAll2()
        {
            //loading time 2 device
            string strSQL = "select * from (select A3.CardNo,A3.TimeIn,B1.TimeOut from ( " +
                            "select CardNo,MIN(JamMasuk) as JamMasuk,TglPin,MIN(TimeIn) as TimeIn from ( " +
                            "select (datepart(hour, CheckTime)*60)+datepart(MINUTE, CheckTime) as JamMasuk,CardNo, isnull(CheckTime,' ') as TimeIn," +
                            "Convert(varchar,TglProcess,112) as TglPin from LoadingTimeIn " +
                            "where Convert(varchar,TglProcess,112) >= '20140716' and Convert(varchar,TglProcess,112) <= GETDATE()  " +
                            ") as A1 group by TglPin,CardNo) as A3 " +
                            "left join ( select CardNo,isnull(Max(CheckTime),' ') as TimeOut,max(Convert(varchar,TglProcess,112)) as TglPout "+
                            "from LoadingTimeOut " +
                            "where Convert(varchar,TglProcess,112) >= '20140716' and Convert(varchar,TglProcess,112) <= GETDATE() "+
                            "group by CardNo,Convert(varchar,TglProcess,112)) as B1 " +
                            "on B1.CardNo=A3.CardNo and B1.TglPout=A3.TglPin " +
                            "union all " +
                            "select CardNo,null as TimeIn,isnull(Max(CheckTime),' ') as TimeOut " +
                            "from LoadingTimeOut where Convert(varchar,TglProcess,112) >= '20140716' and Convert(varchar,TglProcess,112) <= GETDATE() and " +
                            "CardNo+Convert(varchar,TglProcess,112) not in (select CardNo+Convert(varchar,TglProcess,112) from LoadingTimeIn " +
                            "where Convert(varchar,TglProcess,112) >= '20140716' and Convert(varchar,TglProcess,112) <= GETDATE())  " +
                            "group by CardNo,Convert(varchar,TglProcess,112) ) as C where TimeIn is null or TimeOut is null";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrLoading = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrLoading.Add(GenerateObjectDevice2(sqlDataReader));
                }
            }
            else
                arrLoading.Add(new LoadingTime());

            return arrLoading;
        }
        public ArrayList RetrieveList()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT A.ID, A.NoPolisi,A.MobilSendiri, A.TglIn,A.TglOut, A.EkspedisiName,B.JenisMobil," +
                     " A.TimeIn, A.TimeOut,B.Target,A.LoadingNo,A.Status,A.Keterangan FROM LoadingTime AS A INNER JOIN MasterKendaraan AS B ON A.KendaraanID = B.ID where isnull(convert(char,A.Timein,112),'19000101') <>'19000101'  and A.Status > -1 and A.TimeOut is null order by ID desc");
            strError = dataAccess.Error;
            arrLoading = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrLoading.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrLoading.Add(new LoadingTime());

            return arrLoading;
        }
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
        public LoadingTime RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from LoadingTime where Status >-1 and ID = " + Id);
            strError = dataAccess.Error;
            arrLoading = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new LoadingTime();
        }
        public LoadingTime RetrieveByCardNo(string Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from LoadingTimeCard where CardNo= " + Id);
            strError = dataAccess.Error;
            arrLoading = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectCard(sqlDataReader);
                }
            }

            return new LoadingTime();
        }
        public LoadingTime RetrieveByName(string jenisKendaraan)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from LoadingTime where Status = 0 and JenisMobil = '" + jenisKendaraan + "'");
            strError = dataAccess.Error;
            arrLoading = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new LoadingTime();
        }
        public LoadingTime RetrieveByNoUrut(string noUrut)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from LoadingTime where Status = 0 and isnull(convert(char,Timein,112),'19000101') <>'19000101'  and isnull(convert(char,Timeout,112),'19000101') <>'19000101' and UrutanNo = '" + noUrut + "'");
            strError = dataAccess.Error;
            arrLoading = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new LoadingTime();
        }
        public ArrayList RetrieveByNoUrut3(string noUrut)
        {
            
                string Criteria = (noUrut.Length > 5) ? noUrut : "and A.UrutanNo='" + noUrut + "' and Convert(Char,TglIn,112)>='20140825'  Order By ID Desc";
                string strSQL = "SELECT A.ID,A.UrutanNo,A.MobilSendiri ,A.NoPolisi, A.TglIn,A.TglOut, A.EkspedisiName,B.JenisMobil,A.TimeIn, " +
                                "A.TimeOut,B.Target,A.LoadingNo,A.Status,A.Keterangan,A.LastModifiedTime,A.CreatedTime FROM LoadingTime AS A LEFT JOIN MasterKendaraan AS B " +
                                "ON A.KendaraanID = B.ID where isnull(convert(char,Timein,112),'19000101') <>'19000101'  and A.Status > -1 " + Criteria;
                                
                DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
                strError = dataAccess.Error;
                arrLoading = new ArrayList();

                if (sqlDataReader.HasRows && strError==string.Empty)
                {
                    while (sqlDataReader.Read())
                    {
                        arrLoading.Add(GenerateObject2(sqlDataReader));
                    }
                }
               // else
                    //arrLoading.Add(new LoadingTime());

                return arrLoading;
           
        }
        public ArrayList RetrieveByNoUrut3New(string noUrut,string tgl)
        {

            string Criteria = (noUrut.Length > 5) ? noUrut : "and A.UrutanNo='" + noUrut + "' and Convert(Char,TglIn,112)>='20140825'  Order By ID Desc";
            string strSQL = "SELECT top 1 A.ID,A.UrutanNo,A.MobilSendiri ,A.NoPolisi, A.TglIn,A.TglOut, A.EkspedisiName,B.JenisMobil,A.TimeIn, " +
                            "A.TimeOut,B.Target,A.LoadingNo,A.Status,A.Keterangan,A.LastModifiedTime,A.CreatedTime FROM LoadingTime AS A LEFT JOIN MasterKendaraan AS B " +
                            "ON A.KendaraanID = B.ID where isnull(convert(char,A.Timein,112),'19000101') <>'19000101'  and A.Status > -1 and convert(char,A.tglin,112)=" + tgl + " " + Criteria;

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrLoading = new ArrayList();

            if (sqlDataReader.HasRows && strError == string.Empty)
            {
                while (sqlDataReader.Read())
                {
                    arrLoading.Add(GenerateObject2(sqlDataReader));
                }
            }
            // else
            //arrLoading.Add(new LoadingTime());

            return arrLoading;

        }

        public ArrayList RetrieveByNoUrut3New2(string nosch)
        {
            string strSQL = "SELECT top 1 A.ID,A.UrutanNo,A.MobilSendiri ,A.NoPolisi, A.TglIn,A.TglOut, A.EkspedisiName,B.JenisMobil,A.TimeIn, " +
                            "A.TimeOut,B.Target,A.LoadingNo,A.Status,A.Keterangan,A.LastModifiedTime,A.CreatedTime FROM LoadingTime AS A LEFT JOIN MasterKendaraan AS B " +
                            "ON A.KendaraanID = B.ID where isnull(convert(char,A.Timein,112),'19000101') <>'19000101'  and A.Status > -1 and A.Keterangan='" + nosch + "'";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrLoading = new ArrayList();

            if (sqlDataReader.HasRows && strError == string.Empty)
            {
                while (sqlDataReader.Read())
                {
                    arrLoading.Add(GenerateObject2(sqlDataReader));
                }
            }
            // else
            //arrLoading.Add(new LoadingTime());

            return arrLoading;

        }
        public ArrayList RetrieveByNoSJ(string noSJ)
        {

            string strSQL = "SELECT top 1 A.ID,A.UrutanNo,A.MobilSendiri ,A.NoPolisi, A.TglIn,A.TglOut, A.EkspedisiName,B.JenisMobil,A.TimeIn, " +
                            "A.TimeOut,B.Target,A.LoadingNo,A.Status,A.Keterangan,A.LastModifiedTime,A.CreatedTime FROM LoadingTime AS A LEFT JOIN MasterKendaraan AS B " +
                            "ON A.KendaraanID = B.ID where isnull(convert(char,A.Timein,112),'19000101') <>'19000101'  and A.Status > -1 and keterangan='" + noSJ + "' ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrLoading = new ArrayList();

            if (sqlDataReader.HasRows && strError == string.Empty)
            {
                while (sqlDataReader.Read())
                {
                    arrLoading.Add(GenerateObject2(sqlDataReader));
                }
            }
            // else
            //arrLoading.Add(new LoadingTime());

            return arrLoading;

        }
        public ArrayList RetrieveByNoSchedule(string nochedule)
        {

            string strSQL = "SELECT top 1 A.ID,A.UrutanNo,A.MobilSendiri ,A.NoPolisi, A.TglIn,A.TglOut, A.EkspedisiName,B.JenisMobil,A.TimeIn, " +
                            "A.TimeOut,B.Target,A.LoadingNo,A.Status,A.Keterangan,A.LastModifiedTime,A.CreatedTime FROM LoadingTime AS A LEFT JOIN MasterKendaraan AS B " +
                            "ON A.KendaraanID = B.ID where isnull(convert(char,A.Timein,112),'19000101') <>'19000101'  and A.Status > -1 and keterangan='" + nochedule + "' ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrLoading = new ArrayList();

            if (sqlDataReader.HasRows && strError == string.Empty)
            {
                while (sqlDataReader.Read())
                {
                    arrLoading.Add(GenerateObject2(sqlDataReader));
                }
            }
            // else
            //arrLoading.Add(new LoadingTime());

            return arrLoading;

        }
        public ArrayList RetrieveByTglIn(string tgl)
        {

            string strSQL = "SELECT A.ID,A.UrutanNo,A.MobilSendiri ,A.NoPolisi, A.TglIn,A.TglOut, A.EkspedisiName,B.JenisMobil,A.TimeIn, " +
                            "A.TimeOut,B.Target,A.LoadingNo,A.Status,A.Keterangan,A.LastModifiedTime,A.CreatedTime FROM LoadingTime AS A LEFT JOIN MasterKendaraan AS B " +
                            "ON A.KendaraanID = B.ID where isnull(convert(char,A.Timein,112),'19000101') <>'19000101'  and A.Status > -1 and isnull(keterangan,'')='' and convert(char,A.tglin,112)=" + tgl + "  Order By A.tglin";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrLoading = new ArrayList();

            if (sqlDataReader.HasRows && strError == string.Empty)
            {
                while (sqlDataReader.Read())
                {
                    arrLoading.Add(GenerateObject2(sqlDataReader));
                }
            }
            // else
            //arrLoading.Add(new LoadingTime());

            return arrLoading;

        }
        public ArrayList RetrieveByNoUrut2(string noUrut)
        {
            string strSQL = "SELECT A.ID,A.UrutanNo,A.MobilSendiri ,A.NoPolisi, A.TglIn,A.TglOut, A.EkspedisiName,B.JenisMobil,A.TimeIn, A.TimeOut,B.Target,A.LoadingNo, " +
                            "A.Status,A.Keterangan FROM LoadingTime AS A INNER JOIN MasterKendaraan AS B ON A.KendaraanID = B.ID where isnull(convert(char,A.Timein,112),'19000101') <>'19000101'  and A.Status > -1 and A.TimeOut is null  and A.UrutanNo='" + noUrut + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrLoading = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrLoading.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrLoading.Add(new LoadingTime());

            return arrLoading;
        }
        public LoadingTime RetrieveLoadingTimeDeviceByCardNo(int cardNo, string timeIn)
        {
            
            string strSQL = "select CardNo,min(TimeOutDevice) as TimeOut from LoadingTimeDevice where TimeOutDevice > '" + timeIn + "' and CardNo=" + cardNo + " group by CardNo";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrLoading = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectDevice(sqlDataReader);
                }
            }

            return new LoadingTime();
        }
        public LoadingTime RetrieveByNoUrutOut(string noUrut)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from LoadingTime where Status = 0  and TimeOut is null and UrutanNo = '" + noUrut + "'");
            strError = dataAccess.Error;
            arrLoading = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new LoadingTime();
        }
        public ArrayList RetrieveByNoUrutOut2(string noUrut)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT A.ID,A.UrutanNo,A.MobilSendiri ,A.NoPolisi, A.TglIn,A.TglOut, A.EkspedisiName,B.JenisMobil,A.TimeIn, A.TimeOut,B.Target,A.LoadingNo, " +
                "A.Status,A.Keterangan FROM LoadingTime AS A INNER JOIN MasterKendaraan AS B ON A.KendaraanID = B.ID where isnull(convert(char,A.Timein,112),'19000101') <>'19000101'  and A.Status > -1 and A.TimeOut is null  and A.UrutanNo='" + noUrut + "'");

            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from LoadingTime where Status = 0 and UrutanNo = '" + noUrut + "'");
            strError = dataAccess.Error;
            arrLoading = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrLoading.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrLoading.Add(new LoadingTime());

            return arrLoading;
        }
        public LoadingTime GetLastNumber()
        {
            string strSQL = "SELECT isnull(MAX(loadingno),0) as LoadingNo,isnull(max(TglIn),getdate()) as TglIn from LoadingTime";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrLoading = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectLoad(sqlDataReader);
                }
            }
            return new LoadingTime();
        }
        public LoadingTime GetLastUrutan()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT MAX(UrutanNo) as LoadingNo from LoadingTime");
            strError = dataAccess.Error;
            arrLoading = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectUrutan(sqlDataReader);
                }
            }

            return new LoadingTime();
        }
        public DateTime CekMinuteTglInByCardIDTglIn(int kartuID, string strTglIn)
        {
            string strSQL = "SELECT isnull(max(TglIn), '') as TglIn from LoadingTime where TimeOut is null and CardID=" + kartuID + " and  "+
                            "Convert(varchar,TglIn,112) >= '" + strTglIn + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrLoading = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToDateTime(sqlDataReader["TglIn"]);
                }
            }

            return DateTime.MinValue;
        }
        public DateTime CekMinuteTglOutByCardIDTglIn(int kartuID, string strTglIn)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT isnull(max(TglIn), '') as TglIn from LoadingTime where TimeOut is null and CardID=" + kartuID + " and  Convert(varchar,TglIn,112) >= '" + strTglIn + "'");
            strError = dataAccess.Error;
            arrLoading = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToDateTime(sqlDataReader["TglIn"]);
                }
            }

            return DateTime.MinValue;
        }
        public int CekMinuteTglOutByCardID(int kartuID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT top 1 ID from LoadingTime where CardID="+kartuID+" and TimeOut is null and	DATEDIFF(day,TglIn,GETDATE())<=1 order by ID asc");
            strError = dataAccess.Error;
            arrLoading = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["ID"]);
                }
            }

            return 0;
        }
        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from LoadingTime where Status = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrLoading = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrLoading.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrLoading.Add(new LoadingTime());

            return arrLoading;
        }
        public LoadingTime GenerateObject(SqlDataReader sqlDataReader)
        {
            objLoading = new LoadingTime();
            objLoading.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objLoading.NoUrut = sqlDataReader["UrutanNo"].ToString();
            if (string.IsNullOrEmpty(sqlDataReader["NoPolisi"].ToString()))
                objLoading.NoPolisi = "";
            else
                objLoading.NoPolisi = sqlDataReader["NoPolisi"].ToString();

            objLoading.TglIn = Convert.ToDateTime(sqlDataReader["TglIn"]);
            if (string.IsNullOrEmpty(sqlDataReader["TglOut"].ToString()))
                objLoading.TglOut = DateTime.MinValue;
            else
                objLoading.TglOut = Convert.ToDateTime(sqlDataReader["TglOut"]);

            objLoading.TimeIn = Convert.ToDateTime(sqlDataReader["TimeIn"]);
            if (string.IsNullOrEmpty(sqlDataReader["TimeOut"].ToString()))
                objLoading.TimeOut = DateTime.MinValue;
            else
                objLoading.TimeOut = Convert.ToDateTime(sqlDataReader["TimeOut"]);

            if (string.IsNullOrEmpty(sqlDataReader["KendaraanID"].ToString()))
                objLoading.KendaraanID = 0;
            else
                objLoading.KendaraanID = Convert.ToInt32(sqlDataReader["KendaraanID"]);

            objLoading.Status = Convert.ToInt32(sqlDataReader["Status"]);
            if (string.IsNullOrEmpty(sqlDataReader["Keterangan"].ToString()))
                objLoading.Keterangan = "";
            else
                objLoading.Keterangan = sqlDataReader["Keterangan"].ToString();
            objLoading.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objLoading.CreatedTime = (sqlDataReader["CreatedTime"]!= DBNull.Value)?Convert.ToDateTime(sqlDataReader["CreatedTime"]):DateTime.MinValue;
            objLoading.LastModifiedBy = (sqlDataReader["LastModifiedBy"]!= DBNull.Value)?sqlDataReader["LastModifiedBy"].ToString():string.Empty;
            objLoading.LastModifiedTime = (sqlDataReader["LastModifiedTime"]!=DBNull.Value)?Convert.ToDateTime(sqlDataReader["LastModifiedTime"]):DateTime.MinValue;
            objLoading.Ritase = (sqlDataReader["Ritase"]==DBNull.Value)?0:Convert.ToInt32(sqlDataReader["Ritase"]);

            return objLoading;

        }
        public LoadingTime GenerateObjectCard(SqlDataReader sqlDataReader)
        {
            objLoading = new LoadingTime();
            objLoading.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objLoading.CardNo = sqlDataReader["CardNo"].ToString();
            objLoading.CardID = sqlDataReader["CardID"].ToString();

            return objLoading;

        }
        public LoadingTime GenerateObjectDevice(SqlDataReader sqlDataReader)
        {
            objLoading = new LoadingTime();
            objLoading.CardNo = sqlDataReader["CardNo"].ToString();
            objLoading.TimeOut = Convert.ToDateTime(sqlDataReader["TimeOut"]);

            return objLoading;

        }
        public LoadingTime GenerateObjectDevice2(SqlDataReader sqlDataReader)
        {
            objLoading = new LoadingTime();
            objLoading.CardNo = sqlDataReader["CardNo"].ToString();
            //objLoading.TimeOut = Convert.ToDateTime(sqlDataReader["TimeOut"]);
            if (string.IsNullOrEmpty(sqlDataReader["TimeOut"].ToString()))
                objLoading.TimeOut = DateTime.MinValue;
            else
                objLoading.TimeOut = Convert.ToDateTime(sqlDataReader["TimeOut"]);

            //objLoading.TimeIn = Convert.ToDateTime(sqlDataReader["TimeIn"]);
            if (string.IsNullOrEmpty(sqlDataReader["TimeIn"].ToString()))
                objLoading.TimeIn = DateTime.MinValue;
            else
                objLoading.TimeIn = Convert.ToDateTime(sqlDataReader["TimeIn"]);
            return objLoading;

        }
        public LoadingTime GenerateObjectLoad(SqlDataReader sqlDataReader)
        {
            objLoading = new LoadingTime();
            objLoading.LoadingNo = sqlDataReader["LoadingNo"].ToString();
            objLoading.TglIn = Convert.ToDateTime(sqlDataReader["TglIn"]);

            return objLoading;

        }
        public LoadingTime GenerateObjectUrutan(SqlDataReader sqlDataReader)
        {
            objLoading = new LoadingTime();
            objLoading.NoUrut = sqlDataReader["UrutanNo"].ToString();
            return objLoading;

        }
        public LoadingTime GenerateObject2(SqlDataReader sqlDataReader)
        {
            objLoading = new LoadingTime();
            objLoading.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objLoading.LoadingNo = sqlDataReader["LoadingNo"].ToString();
            objLoading.NoUrut = sqlDataReader["UrutanNo"].ToString();
            objLoading.TglIn = Convert.ToDateTime(sqlDataReader["TglIn"]);
            if (string.IsNullOrEmpty(sqlDataReader["TglOut"].ToString()))
            {
                objLoading.TglOut = DateTime.MinValue;
                objLoading.TglKeluar = string.Empty;
            }
            else
            {
                objLoading.TglOut = Convert.ToDateTime(sqlDataReader["TglOut"]);
                objLoading.TglKeluar = Convert.ToDateTime(sqlDataReader["TglOut"]).ToString();
            }

            objLoading.TimeIn = Convert.ToDateTime(sqlDataReader["TimeIn"]);            
            if (string.IsNullOrEmpty(sqlDataReader["TimeOut"].ToString()))
                objLoading.TimeOut = DateTime.MinValue;
            else
                objLoading.TimeOut = Convert.ToDateTime(sqlDataReader["TimeOut"]);

            objLoading.NoPolisi = (sqlDataReader["NoPolisi"]==DBNull.Value) ? string.Empty : sqlDataReader["NoPolisi"].ToString();
            objLoading.JenisMobil = (sqlDataReader["JenisMobil"]==DBNull.Value)?string.Empty:sqlDataReader["JenisMobil"].ToString();
            //objLoading.ExpedisiName = sqlDataReader["ExpedisiName"].ToString();
            objLoading.MobilSendiri = (sqlDataReader["MobilSendiri"]!=DBNull.Value)?Convert.ToInt32(sqlDataReader["MobilSendiri"]):0;
            objLoading.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objLoading.EkspedisiName =(sqlDataReader["EkspedisiName"]==DBNull.Value)?string.Empty: sqlDataReader["EkspedisiName"].ToString();
            objLoading.Keterangan = (sqlDataReader["Keterangan"]==DBNull.Value) ? string.Empty : sqlDataReader["Keterangan"].ToString();
            //objLoading.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            //objLoading.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            //objLoading.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objLoading.LastModifiedTime =(sqlDataReader["LastModifiedTime"]==DBNull.Value)?DateTime.MinValue: Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objLoading;

        }
        public ArrayList RetrieveAll(int p, string p_2)
        {
            throw new NotImplementedException();
        }
        public ArrayList RetrieveList(int p, string p_2, string p_3)
        {
            throw new NotImplementedException();
        }
        #region LoadingTime Ctrp
        public ArrayList RetrieveLD1(string MaxTime)
        {

            string strSQL = "Select * from LoadingTime1D where /*(DATEDIFF(MI,TimeIn,TimeOut)<=" + MaxTime + " or DATEDIFF(MI,TimeIn,TimeOut)) <= " + MaxTime + " and*/ RowStatus=0 " +
                            "/*and TimeOut is not null*/ order by ID";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = dataAccess.RetrieveDataByString(strSQL);
            arrLoading = new ArrayList();
            if (dataAccess.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrLoading.Add(new LoadingTime
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        CardNo = sdr["CardNo"].ToString(),
                        TglIn = Convert.ToDateTime(sdr["TimeIn"].ToString()),
                        TglKeluar = (sdr["TimeOut"] == DBNull.Value) ? string.Empty : sdr["TimeOut"].ToString()

                    });
                }
            }
            return arrLoading;
        }
        public ArrayList ListCardNo()
        {
            string strSQL = "Select CardNo from LoadingTime1D where TimeOut is null or TimeOut='' group by CardNo";
            DataAccess dta = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = dta.RetrieveDataByString(strSQL);
            arrLoading=new ArrayList();
            if (dta.Error == string.Empty && sdr.HasRows)
            {
                while(sdr.Read())
                {
                    arrLoading.Add(new LoadingTime
                    {
                        CardNo =sdr["CardNo"].ToString()
                    });
                }
            }
            return arrLoading;
        }
        public ArrayList ListTimeLoading(int CardNo)
        {
            string strSQL = "Select ID,CardNo,TimeIn,TimeOut,RowStatus from LoadingTime1D where RowStatus >-1 and CardNo="+CardNo+" order by ID";
            SqlDataReader sdr = sqlDataReader(strSQL);
            arrLoading=new ArrayList();
            DateTime tgl1=new DateTime();
            DateTime tgl2 = new DateTime();
            int no = 0;
            TimeSpan selish;
            if(sdr.HasRows)
            {
                while(sdr.Read())
                {
                    no = no + 1;
                    tgl2 = Convert.ToDateTime(sdr["TimeIn"].ToString());
                    selish = tgl2 - tgl1;
                    if ((no%2)>0)
                    {
                        arrLoading.Add(new LoadingTime
                        {
                            ID = Convert.ToInt32(sdr["ID"].ToString()),
                            CardNo = sdr["CardNo"].ToString(),
                            TglIn = Convert.ToDateTime(sdr["TimeIn"].ToString()),
                            TglKeluar = string.Empty ,
                            RowStatus = Convert.ToInt32(sdr["RowStatus"].ToString())
                        });
                        if (tgl1 == tgl2)
                        {
                            if (arrLoading.Count > 0) arrLoading.RemoveAt(arrLoading.Count - 1);
                        }
                    }
                    else
                    {
                        if (selish.Minutes < 1 && (no % 2 == 0))
                        {
                            arrLoading.Add(new LoadingTime
                            {
                                ID = Convert.ToInt32(sdr["ID"].ToString()),
                                CardNo = sdr["CardNo"].ToString(),
                                TglIn = Convert.ToDateTime(sdr["TimeIn"].ToString()),
                                TglKeluar = string.Empty ,
                                RowStatus = Convert.ToInt32(sdr["RowStatus"].ToString())
                            });
                            if (tgl1 == tgl2)
                            {
                                if (arrLoading.Count > 0) arrLoading.RemoveAt(arrLoading.Count - 1);
                            }  
                        }
                        else
                        {
                            arrLoading.Add(new LoadingTime
                            {
                                ID = Convert.ToInt32(sdr["ID"].ToString()),
                                CardNo = sdr["CardNo"].ToString(),
                                TglIn = tgl1,/*Convert.ToDateTime(sdr["TimeIn"].ToString()),*/
                                TglKeluar = Convert.ToDateTime(sdr["TimeIn"].ToString()).ToString(),
                                RowStatus = Convert.ToInt32(sdr["RowStatus"].ToString())
                            });
                            if(tgl2==tgl1) arrLoading.RemoveAt(arrLoading.Count-1);
                        }
                       // if (tgl2 == tgl1) arrLoading.RemoveAt(arrLoading.Count - 1);
                    }
                    tgl1 = Convert.ToDateTime(sdr["TimeIn"].ToString());
                }
            }
            return arrLoading;
        }
        public LoadingTime CheckData(string CardNo, string tgl,int MinTime)
        {
            //string strSQL = "Select Top 1 ID,DATEDIFF(MI,TimeIN," + tgl + ") Selisih from LoadingTime1D where CardNo='" + CardNo + "' and " +
            //              "DATEDIFF(MI,TimeIN," + tgl + ")<=" + MinTime +" order by ID Desc";
            string strSQL = "Select Top 1 ID,DATEDIFF(MI,TimeIN,'" + tgl + "') Selisih from LoadingTime1D where CardNo='" + CardNo + "' and " +
                            "TimeIN='" + tgl +"' order by ID Desc";            
            DataAccess dta = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = dta.RetrieveDataByString(strSQL);
            objLoading = new LoadingTime();
            if (dta.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    objLoading.ID = Convert.ToInt32(sdr["ID"].ToString());
                    objLoading.Flag=Convert.ToInt32(sdr["Selisih"].ToString());
                }
            }
            return objLoading;
        }
        public LoadingTime GetLastData(string CardNo)
        {
            string strSQL = "Select Top 1 ID,TimeIn,TimeOut from LoadingTime1D where CardNo='" + CardNo + "' and TimeOut is null order by ID Desc";
            SqlDataReader sdr = this.sqlDataReader(strSQL);
            objLoading = new LoadingTime();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    objLoading.ID = Convert.ToInt32(sdr["ID"].ToString());
                    objLoading.TglIn = Convert.ToDateTime(sdr["TimeIN"].ToString());
                }
            }
            return objLoading;

        }
        public LoadingTime GetTimeINisTimeOutIsNull(string CardNo, string TimeIN)
        {
            string strSQL = "Select Top 1 ID,TglIn from LoadingTime where TglIn='" + TimeIN + "' and UrutanNo=" + CardNo;
            SqlDataReader sdr = this.sqlDataReader(strSQL);
            objLoading = new LoadingTime();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    objLoading.ID = Convert.ToInt32(sdr["ID"].ToString());
                    objLoading.TglIn = Convert.ToDateTime(sdr["TglIn"].ToString());
                }
            }
            return objLoading;
        }
        public SqlDataReader sqlDataReader(string strSQL)
        {
            DataAccess dta = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = dta.RetrieveDataByString(strSQL);
            return sdr;
        }
        #endregion
    }

    public class LapLoadingTime
    {
        ArrayList arrData = new ArrayList();
        LoadingTime ld = new LoadingTime();
        public string Criteria { get; set; }
        public string Pilihan { get; set; }
        public string DariTgl { get; set; }
        public string SampaiTgl { get; set; }
        public string StartTime { get; set; }
        public string ArmadaID { get; set; }

        private string Query()
        {
            string query = string.Empty; string query1 = string.Empty;
            switch (this.Pilihan)
            {
                case "Detail":
                    //query1 = " from data_600 order by MobilSendiri,UrutanNo ";
                    //WO Dijombang tidak memunculkan 'Hanya SJ'/urutan = 0
                    query1 = " from data_600 WHERE urutanno ! = '0' order by MobilSendiri,UrutanNo ";
                    query = new ReportFacade().ViewLapLoadingTime(this.DariTgl,this.SampaiTgl,int.Parse(this.ArmadaID),int.Parse(this.StartTime), query1);                   
                    break;
                case "Tahun":
                    query = "Select Distinct Year(tglin) as Tahun from LoadingTime order by Year(tglin) desc";
                    break;
                case "Rekap":
                    query = "if exists (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'laploadingtmp')drop table laploadingtmp; ";
                    //"select * into laploadingtmp from ( ";
                    query1 = " into laploadingtmp from data_600 order by MobilSendiri,UrutanNo ";
                    query += new ReportFacade().ViewLapLoadingTime(this.DariTgl, this.SampaiTgl, int.Parse(this.ArmadaID), int.Parse(this.StartTime), query1).Replace("order by MobilSendiri,TimeIn", "");
                    //query += " )  as XX ";
                    
                    query += "select Cast(Tanggal as datetime)Tanggal,Day(Cast(Tanggal as datetime))Tgl,Targete,(Cast(OK as decimal)/cast(JmlMobil as decimal))*100 as Capai,JmlMobil,OK,Lewat,mBpas,Luar From( " +
                          "  select Tanggal,'95' Targete,SUM(jmlMobil) jmlMobil,SUM(jmlOK)OK,SUM(jmlLewat)Lewat, " +
                          "  SUM(BPAS)mBpas,SUM(Luar)Luar from " +
                          "  ( " +
                          "   select Convert(Char,Timein,112)Tanggal,COUNT(NoPolisi)JmlMobil, " +
                          "   Case when StatusLoading='OK' then COUNT(Nopolisi) else 0 end jmlok, " +
                          "   Case when StatusLoading!='OK' then COUNT(NoPolisi) else 0 end jmllewat, " +
                          "   Case when (StatusLoading!='OK' and Mobilsendiri=0)then Count(NoPolisi) else 0 end BPAS, " +
                          "   case when (StatusLoading!='OK' and Mobilsendiri=1) then Count(NoPolisi) else 0 end Luar " +
                          "   from laploadingtmp  " +
                          "   group by Convert(CHAR,timein,112),statusloading,MobilSendiri " +
                          "   ) as x Group By Tanggal " +
                          "   ) as xx " +
                          "   order by Tanggal";
                    break;
            }
            return query;
        }        
        private LoadingTime CreateObject(SqlDataReader sdr)
        {
            ld = new LoadingTime();
            switch (this.Pilihan)
            {
                case "Detail":
                    ld.NoPolisi = sdr["NoPolisi"].ToString();
                    ld.MobilSendiri = int.Parse(sdr["MobilSendiri"].ToString());
                    ld.Keterangan = sdr["Keterangan"].ToString();
                    ld.JenisMobil = sdr["JenisMobil"].ToString();
                    ld.TimeIn = Convert.ToDateTime(sdr["TimeIn"].ToString());
                    ld.TimeOut = Convert.ToDateTime(sdr["TimeOut"].ToString());
                    ld.Status = Convert.ToInt32(sdr["Status"].ToString());
                    ld.LoadingNo = sdr["StatusLoading"].ToString();
                    //ld.LoadingNo = sdr["Status2"].ToString();
                    ld.NoUrut = sdr["urutanno"].ToString();
                    ld.Tujuan2 = sdr["Tujuan2"].ToString();

                    ld.TimeDaftar = Convert.ToDateTime(sdr["TimeDaftar"].ToString());
                    ld.Noted = sdr["Noted"].ToString();
                    //ld.Target = Convert.ToDecimal(sdr["Target"]);
                    ld.Target = sdr["Target"].ToString();
                    ld.Status2 = Convert.ToInt32(sdr["Status2"].ToString());

                    break;
                case "Tahun":
                    ld.RowStatus = Convert.ToInt32(sdr["Tahun"].ToString());
                    break;
                case "Rekap":
                    ld.ID = Convert.ToInt32(sdr["Tgl"].ToString());
                    ld.TglIn = Convert.ToDateTime(sdr["Tanggal"].ToString());
                    ld.Targete = Convert.ToDecimal(sdr["Targete"].ToString());
                    ld.JmlMobil = Convert.ToInt32(sdr["JmlMobil"].ToString());
                    ld.JmlOK = Convert.ToInt32(sdr["OK"].ToString());
                    ld.JmlLewat = Convert.ToInt32(sdr["Lewat"].ToString());
                    ld.Pencapaian = Convert.ToDecimal(sdr["Capai"].ToString());
                    ld.MobilSendiri = Convert.ToInt32(sdr["mBpas"].ToString());
                    ld.EkspedisiID = Convert.ToInt32(sdr["Luar"].ToString());
                    
                    break;
            }
            return ld;
        }

        private string Query2()
        {
            string query = string.Empty; string query1 = string.Empty;
            switch (this.Pilihan)
            {
                case "Detail":
                    query1 = "";
                    query = new ReportFacade().ViewLapLoadingTime_asli(this.DariTgl, this.SampaiTgl, int.Parse(this.ArmadaID), int.Parse(this.StartTime));
                    break;
                case "Tahun":
                    query = "Select Distinct Year(Timein) as Tahun from LoadingTime order by Year(Timein) desc";
                    break;
                case "Rekap":
                    query = "if exists (select * from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'laploadingtmp')drop table laploadingtmp; " +
                            "select * into laploadingtmp from ( ";
                    query1 = "";
                    query += new ReportFacade().ViewLapLoadingTime_asli(this.DariTgl, this.SampaiTgl, int.Parse(this.ArmadaID), int.Parse(this.StartTime)).Replace("order by MobilSendiri,TimeIn", "");
                    query += " )  as XX ";
                    query += "select Cast(Tanggal as datetime)Tanggal,Day(Cast(Tanggal as datetime))Tgl,Targete,(Cast(OK as decimal)/cast(JmlMobil as decimal))*100 as Capai,JmlMobil,OK,Lewat,mBpas,Luar From( " +
                          "  select Tanggal,'95' Targete,SUM(jmlMobil) jmlMobil,SUM(jmlOK)OK,SUM(jmlLewat)Lewat, " +
                          "  SUM(BPAS)mBpas,SUM(Luar)Luar from " +
                          "  ( " +
                          "   select Convert(Char,Timein,112)Tanggal,COUNT(NoPolisi)JmlMobil, " +
                          "   Case when StatusLoading='OK' then COUNT(Nopolisi) else 0 end jmlok, " +
                          "   Case when StatusLoading!='OK' then COUNT(NoPolisi) else 0 end jmllewat, " +
                          "   Case when (StatusLoading!='OK' and Mobilsendiri=0)then Count(NoPolisi) else 0 end BPAS, " +
                          "   case when (StatusLoading!='OK' and Mobilsendiri=1) then Count(NoPolisi) else 0 end Luar " +
                          "   from laploadingtmp  " +
                          "   group by Convert(CHAR,timein,112),statusloading,MobilSendiri " +
                          "   ) as x Group By Tanggal " +
                          "   ) as xx " +
                          "   order by Tanggal";
                    break;
            }
            return query;
        }
        private LoadingTime CreateObject2(SqlDataReader sdr)
        {
            ld = new LoadingTime();
            switch (this.Pilihan)
            {
                case "Detail":
                    ld.NoPolisi = sdr["NoPolisi"].ToString();
                    ld.MobilSendiri = int.Parse(sdr["MobilSendiri"].ToString());
                    ld.Keterangan = sdr["Ket"].ToString();
                    ld.JenisMobil = sdr["JenisMobil"].ToString();
                    ld.TimeIn = Convert.ToDateTime(sdr["TimeIn"].ToString());
                    ld.TimeOut = Convert.ToDateTime(sdr["TimeOut"].ToString());
                    ld.Status = Convert.ToInt32(sdr["Status2"].ToString());
                    ld.LoadingNo = sdr["StatusLoading"].ToString();
                    ld.NoUrut = sdr["urutanno"].ToString();
                    ld.Tujuan2 = sdr["Tujuan2"].ToString();

                    ld.TimeDaftar = Convert.ToDateTime(sdr["TimeDaftar"].ToString());
                    ld.Noted = sdr["Noted"].ToString();                 
                    ld.Target = sdr["Target"].ToString();

                    break;
                case "Tahun":
                    ld.RowStatus = Convert.ToInt32(sdr["Tahun"].ToString());
                    break;
                case "Rekap":
                    ld.ID = Convert.ToInt32(sdr["Tgl"].ToString());
                    ld.TglIn = Convert.ToDateTime(sdr["Tanggal"].ToString());
                    ld.Targete = Convert.ToDecimal(sdr["Targete"].ToString());
                    ld.JmlMobil = Convert.ToInt32(sdr["JmlMobil"].ToString());
                    ld.JmlOK = Convert.ToInt32(sdr["OK"].ToString());
                    ld.JmlLewat = Convert.ToInt32(sdr["Lewat"].ToString());
                    ld.Pencapaian = Convert.ToDecimal(sdr["Capai"].ToString());
                    ld.MobilSendiri = Convert.ToInt32(sdr["mBpas"].ToString());
                    ld.EkspedisiID = Convert.ToInt32(sdr["Luar"].ToString());

                    break;
            }
            return ld;
        }

        public ArrayList Retrieve()
        {
            arrData = new ArrayList();
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(this.Query());
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(CreateObject(sdr));
                }
            }
            return arrData;
        }

        public ArrayList Retrieve2()
        {
            arrData = new ArrayList();
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(this.Query2());
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(CreateObject2(sdr));
                }
            }
            return arrData;
        }
    }
}
