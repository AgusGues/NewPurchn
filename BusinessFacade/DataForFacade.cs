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
    public class DataForFacade : AbstractFacade
    {
        private DataFor objDataFor = new DataFor();
        private ArrayList arrDataFor;        
        private List<SqlParameter> sqlListParam;


        public DataForFacade()
            : base()
        {
         
        }

        public override int Insert(object objDomain)
        {
            try
            {
                //objDataFor = (DataFor)objDomain;
                //sqlListParam = new List<SqlParameter>();
                //sqlListParam.Add(new SqlParameter("@Nama", objDataFor.Nama));
                //sqlListParam.Add(new SqlParameter("@Alamat", objDataFor.Alamat));
                //sqlListParam.Add(new SqlParameter("@Email", objDataFor.Email));
                //sqlListParam.Add(new SqlParameter("@MobileNo", objDataFor.MobileNo));
                //sqlListParam.Add(new SqlParameter("@UserName", objDataFor.UserName));
                //sqlListParam.Add(new SqlParameter("@Password", objDataFor.Password));
                //sqlListParam.Add(new SqlParameter("@IPAdd", objDataFor.IPAdd));                
                //int intResult = dataAccess.ProcessData(sqlListParam, "spBajuPulsa_InsertUsers");
                //strError = dataAccess.Error;
                //return intResult;
                return -1;
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
                //objDataFor = (DataFor)objDomain;
                //sqlListParam = new List<SqlParameter>();
                //sqlListParam.Add(new SqlParameter("@Nama", objDataFor.Nama));
                //sqlListParam.Add(new SqlParameter("@Alamat", objDataFor.Alamat));
                //sqlListParam.Add(new SqlParameter("@Email", objDataFor.Email));
                //sqlListParam.Add(new SqlParameter("@MobileNo", objDataFor.MobileNo));
                //sqlListParam.Add(new SqlParameter("@UserName", objDataFor.UserName));
                //sqlListParam.Add(new SqlParameter("@Password", objDataFor.Password));
                //sqlListParam.Add(new SqlParameter("@IPAdd", objDataFor.IPAdd));
                //int intResult = dataAccess.ProcessData(sqlListParam, "spBajuPulsa_InsertUsers");
                //strError = dataAccess.Error;
                //return intResult;
                return -1;
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
                //objUsers = (Users)objDomain;
                //sqlListParam = new List<SqlParameter>();
                //sqlListParam.Add(new SqlParameter("@ID", objUsers.ID));
                //sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUsers.LastModifiedBy));                               
                //int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteUsers");
                //strError = dataAccess.Error;
                //return intResult;
                return -1;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int AndroidWeb_LogAktivitas(object objDomain)
        {
            try
            {
                objDataFor = (DataFor)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@UserID", objDataFor.UserID));
                sqlListParam.Add(new SqlParameter("@IPAdd", objDataFor.IPAdd));
                sqlListParam.Add(new SqlParameter("@Keterangan", objDataFor.Keterangan));
                sqlListParam.Add(new SqlParameter("@Modul", objDataFor.Modul));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objDataFor.CreatedBy));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsert_AndroidWeb_LogAktivitas");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int InsertSalesman(object objDomain)
        {
            try
            {
                objDataFor = (DataFor)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@SalesmanId", objDataFor.SalesmanId));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objDataFor.CreatedBy));
                sqlListParam.Add(new SqlParameter("@PasswordAndro", objDataFor.PasswordAndro));
                sqlListParam.Add(new SqlParameter("@NomorHp", objDataFor.NomorHp));
                sqlListParam.Add(new SqlParameter("@SalesmanName", objDataFor.SalesmanName));
                sqlListParam.Add(new SqlParameter("@Distributor", objDataFor.Distributor));
                sqlListParam.Add(new SqlParameter("@UsernameAndro", objDataFor.UsernameAndro));
                sqlListParam.Add(new SqlParameter("@UnitID", objDataFor.UnitID));
                sqlListParam.Add(new SqlParameter("@UnitSalesman", objDataFor.UnitSalesman));
                sqlListParam.Add(new SqlParameter("@Type", objDataFor.Type));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsert_InserSalesman");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int InsertSurveyor(object objDomain)
        {
            try
            {
                objDataFor = (DataFor)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@CreatedBy", objDataFor.CreatedBy));
                sqlListParam.Add(new SqlParameter("@PasswordAndro", objDataFor.PasswordAndro));
                sqlListParam.Add(new SqlParameter("@NomorHp", objDataFor.NomorHp));
                sqlListParam.Add(new SqlParameter("@SurveyorName", objDataFor.SurveyorName));
                sqlListParam.Add(new SqlParameter("@UsernameAndro", objDataFor.UsernameAndro));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsert_InsertSurveyor");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int InsertSalesmanRegSms(object objDomain)
        {
            try
            {
                objDataFor = (DataFor)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@MobileNo", objDataFor.MobileNo));
                sqlListParam.Add(new SqlParameter("@Text", objDataFor.Text));
                //sqlListParam.Add(new SqlParameter("@Status", objDataFor.Status));
                sqlListParam.Add(new SqlParameter("@Prioritas", objDataFor.Prioritas));
                //sqlListParam.Add(new SqlParameter("@JadwalKirim", objDataFor.JadwalKirim));
                sqlListParam.Add(new SqlParameter("@Description", objDataFor.Description));
                sqlListParam.Add(new SqlParameter("@InboxIdSmsReply", objDataFor.InboxIdSmsReply));
                sqlListParam.Add(new SqlParameter("@NamaPromo", objDataFor.NamaPromo));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsert_InsertSalesmanRegSms");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateNoPheSalesman(object objDomain)
        {
            try
            {
                objDataFor = (DataFor)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objDataFor.ID));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objDataFor.CreatedBy));
                sqlListParam.Add(new SqlParameter("@NomorHp", objDataFor.NomorHp));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdate_UpdateNoPheSalesman");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateNoPheSurveyor(object objDomain)
        {
            try
            {
                objDataFor = (DataFor)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objDataFor.ID));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objDataFor.CreatedBy));
                sqlListParam.Add(new SqlParameter("@NomorHp", objDataFor.NomorHp));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdate_UpdateNoPheSurveyor");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateBlockAkunSalesman(object objDomain)
        {
            try
            {
                objDataFor = (DataFor)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objDataFor.ID));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objDataFor.CreatedBy));
                sqlListParam.Add(new SqlParameter("@RowStatus", objDataFor.RowStatus));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdate_UpdateBlockAkunSalesman");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateTypeAppSalesman(object objDomain)
        {
            try
            {
                objDataFor = (DataFor)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objDataFor.ID));
                sqlListParam.Add(new SqlParameter("@Type", objDataFor.Type));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objDataFor.CreatedBy));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdate_UpdateTypeAppSalesman");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateBlockAkunSurveyor(object objDomain)
        {
            try
            {
                objDataFor = (DataFor)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objDataFor.ID));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objDataFor.CreatedBy));
                sqlListParam.Add(new SqlParameter("@RowStatus", objDataFor.RowStatus));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdate_UpdateBlockAkunSurveyor");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateTokoNew(object objDomain)
        {
            try
            {
                objDataFor = (DataFor)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objDataFor.ID));
                sqlListParam.Add(new SqlParameter("@Keterangan", objDataFor.Keterangan));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdate_UpdateTokoNew");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int InsertSalesmanKabupaten(object objDomain)
        {
            try
            {
                objDataFor = (DataFor)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@SalesmanId", objDataFor.SalesmanId));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objDataFor.CreatedBy));
                sqlListParam.Add(new SqlParameter("@KabKotaID", objDataFor.KabKotaID));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsert_InsertSalesmanKabupaten");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int InsertTokoBaruKunjungan(object objDomain)
        {
            try
            {
                objDataFor = (DataFor)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objDataFor.ID));
                sqlListParam.Add(new SqlParameter("@SalesmanID", objDataFor.SalesmanID));
                sqlListParam.Add(new SqlParameter("@Cord_LA", objDataFor.Cord_LA));
                sqlListParam.Add(new SqlParameter("@Cord_LO", objDataFor.Cord_LO));
                sqlListParam.Add(new SqlParameter("@TokoID", objDataFor.TokoID));
                sqlListParam.Add(new SqlParameter("@AlamatToko", objDataFor.AlamatToko));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsert_InsertTokoBaruKunjungan");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int InsertMessageBroadcast(object objDomain)
        {
            try
            {
                objDataFor = (DataFor)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@MessageText", objDataFor.MessageText));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objDataFor.CreatedBy));
                sqlListParam.Add(new SqlParameter("@DepoID", objDataFor.DepoID));
                sqlListParam.Add(new SqlParameter("@ForSales", objDataFor.ForSales));
                sqlListParam.Add(new SqlParameter("@ForSurveyor", objDataFor.ForSurveyor));
                sqlListParam.Add(new SqlParameter("@ForGrcHelp", objDataFor.ForGrcHelp));
                sqlListParam.Add(new SqlParameter("@ForSalesMkt", objDataFor.ForSalesMkt));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsert_InsertMessageBroadcast");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdatePassword(object objDomain)
        {
            try
            {
                objDataFor = (DataFor)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objDataFor.UserID));
                sqlListParam.Add(new SqlParameter("@Password", objDataFor.Password));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdate_UpdatePassword");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateKabNolAdmin(object objDomain)
        {
            try
            {
                objDataFor = (DataFor)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objDataFor.ID));
                sqlListParam.Add(new SqlParameter("@Kabupaten", objDataFor.Kabupaten));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdate_UpdateKabNolAdmin");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateKabNolAdminTokoBaru(object objDomain)
        {
            try
            {
                objDataFor = (DataFor)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objDataFor.ID));
                sqlListParam.Add(new SqlParameter("@Kabupaten", objDataFor.Kabupaten));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdate_UpdateKabNolAdminTokoBaru");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int InsertClosingTokoAsuh(object objDomain)
        {
            try
            {
                objDataFor = (DataFor)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@PeriodeOmsetFrom", objDataFor.PeriodeOmsetFrom.ToString("yyyyMMdd")));
                sqlListParam.Add(new SqlParameter("@PeriodeOmsetTo", objDataFor.PeriodeOmsetTo.ToString("yyyyMMdd")));
                sqlListParam.Add(new SqlParameter("@PeriodeKunjunganFrom", objDataFor.PeriodeKunjunganFrom.ToString("yyyyMMdd")));
                sqlListParam.Add(new SqlParameter("@PeriodeKunjunganTo", objDataFor.PeriodeKunjunganTo.ToString("yyyyMMdd")));
                sqlListParam.Add(new SqlParameter("@TokoLeft", objDataFor.TokoLeft));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertClosingTokoAsuh");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateResetImei(object objDomain)
        {
            try
            {
                objDataFor = (DataFor)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objDataFor.ID));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objDataFor.CreatedBy));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateResetImei");
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
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select top 10 * from Users where RowStatus = 0");
            //strError = dataAccess.Error;
            arrDataFor = new ArrayList();
            //if (sqlDataReader.HasRows)
            //{                
            //    while (sqlDataReader.Read())
            //    {
            //        arrUsers.Add(GenerateObject(sqlDataReader));
            //    }
            //}
            //else
            //    arrUsers.Add(new Users());
            return arrDataFor;
        }

        public DataFor RetrieveByUserNameAndPassword(string UserName, string Passwrd)
        {
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.UserID,B.UserName from UsersWeb as A,GRCBoard.dbo.Users as B where A.UserID=B.ID "+
            //"and A.RowStatus=0 and A.Block=0 and B.RowStatus=0 and B.UserName='" + UserName + "' and B.Password='" + Passwrd + "'");
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from UsersWeb where Username='" + UserName + "' and Password='" + Passwrd + "' and Block=0 and RowStatus=0");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new DataFor();
        }
        public DataFor RetrieveBioUserID(string UserID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from UsersWeb where ID='" + UserID + "' and Block=0 and RowStatus=0");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new DataFor();
        }
        public DataFor Retrieve_Data_Salesman_By_Id(int SalesmanID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select (select Count(id) from Salesman where SalesmanID=" + SalesmanID + ") as AvailbleReg, " +
            "(select LastCountRegSales from SalesmanCountReg where ID=1) as CountReg, "+
            "(select SalesmanName from GRCboard.dbo.Salesman where ID=" + SalesmanID + ") as SalesmanName");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject1(sqlDataReader);
                }
            }
            return new DataFor();
        }
        public DataFor Retrieve_Data_SalesmanKabupaten_By_Id(int SalesmanID,int KabID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select (select Count(id) from SalesmanKabupaten where SalesmanID=" + SalesmanID + " and Indonesia_KabKota_ID=" + KabID + " and RowStatus=0) as AvailbleReg");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject1B(sqlDataReader);
                }
            }
            return new DataFor();
        }
        public DataFor Retrieve_Data_Surveyor()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select (select LastCountRegSurveyor from SurveyorCountReg where ID=1) as CountReg ");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject1A(sqlDataReader);
                }
            }
            return new DataFor();
        }
        public DataFor Retrieve_Data_Salesman_By_Id2(int ID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select NomorHp,UsernameAndro,PasswordAndro from Salesman where ID=" + ID);
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject4(sqlDataReader);
                }
            }
            return new DataFor();
        }
        public DataFor Retrieve_Data_Surveyor_By_Id2(int ID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select NomorHp,UsernameAndro,PasswordAndro from Surveyor where ID=" + ID);
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject4(sqlDataReader);
                }
            }
            return new DataFor();
        }
        public ArrayList RetrieveBy_ListSalesman(int From, int To, string Query, string Query1)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ( "+
			"select ROW_NUMBER()OVER (ORDER BY A.SalesmanName) as Row,B.ID,A.SalesmanName,  "+
            "case when A.UnitSalesman=1 then 'Distributor'  "+
            "else case when A.UnitSalesman=2 then 'Depo' else '' end end  "+
            "as UnitSales,  "+
            "case when A.UnitSalesman=1 then (select DistributorName from GRCBoard.dbo.Distributor where ID=A.UnitID)  " +
            "else case when A.UnitSalesman=2 then (select DepoName from GRCBoard.dbo.Depo where ID=A.UnitID) else '' end end  " +
            "as Unit,B.NomorHp,B.UsernameAndro,B.Photo,B.RowStatus,B.CreatedTime,B.TypeApp,  " +
            "(select STUFF (( SELECT DISTINCT ', ' + quotename((select NamaKabKota from GRCBoard.dbo.Indonesia_KabKota "+
			" where ID=SalesmanKabupaten.Indonesia_KabKota_ID), '') + '' FROM SalesmanKabupaten where RowStatus>=0  "+
			" and SalesmanID=(select ID from Salesman where SalesmanID=A.ID and RowStatus=0) "+
			" FOR XML PATH('')), 1, 1, '' "+
            ") as NamaKabKota) as Area " +
            "from GRCBoard.dbo.SalesMan as A, Salesman as B where A.ID=B.SalesmanID  " +
            Query1 +
            Query +
            ") as A " +
            
            "order by A.SalesmanName");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrDataFor.Add(new DataFor());
            return arrDataFor;
        }
        public ArrayList RetrieveBy_ListSurveyor(int From, int To, string Query)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ( "+
            "select ROW_NUMBER()OVER (ORDER BY A.SurveyorName) as Row,A.ID,A.SurveyorName,  "+ 
			"A.NomorHp,A.UsernameAndro,A.Photo,A.RowStatus,A.CreatedTime   "+
            "from Surveyor as A "+
            ") as A where Row BETWEEN " + From + " and " + To + " " +
            Query +
            "order by A.SurveyorName");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObject2A(sqlDataReader));
                }
            }
            else
                arrDataFor.Add(new DataFor());
            return arrDataFor;
        }
        public ArrayList RetrieveBy_ListKunjungan(string Query,string OrderBy)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select ROW_NUMBER()OVER (ORDER BY Z.CreatedTime) as Row,* from ( "+
            //"select A.SalesmanID, "+
            //"(select SalesmanName from Salesman where ID=A.SalesmanID) as SalesmanName, "+
            //"A.TokoCode,MIN(A.CreatedTime) as CreatedTime, "+
            //"(select TokoName from [192.168.99.3].GRCboard.dbo.Toko where TokoCode=A.TokoCode and Block=0 and RowStatus=0) as TokoName, " +
            //"(select top 1 Latitude from Toko_Location where TokoCode=A.TokoCode order by id desc)  as Cord_LA_Toko, "+
            //"(select top 1 Longitude from Toko_Location where TokoCode=A.TokoCode order by id desc)  as Cord_LO_Toko, "+
            //"(select RIGHT('00'+CAST((X.TotalWaktu/3600) AS VARCHAR(2)),2)+':'+RIGHT('00'+CAST(((X.TotalWaktu%3600)/60) AS VARCHAR(2)),2)+':'+RIGHT('00'+CAST(((X.TotalWaktu%3600)%60) AS VARCHAR(2)),2) from ( " +
            //"select SUM(ISNULL(DATEDIFF(SECOND,Z.TimeStart,Z.TimeEnd),0)) as TotalWaktu  from ( "+
            //"select TokoCode,Time,Tanggal,SalesmanId,TimeStart,MAX(TimeEnd) as TimeEnd "+
            //"from KunjunganTimerToko where TokoCode=A.TokoCode and SalesmanId=A.SalesmanID and convert(varchar,Tanggal,112)=convert(varchar,A.CreatedTime,112) "+
            //"group by TokoCode,Time,Tanggal,SalesmanId,TimeStart "+
            //") as Z) as X ) as TimeSum, '' as Temp_KabKotaUpdate " +
            //"from KunjunganToko as A group by SalesmanID,convert(varchar,CreatedTime,112),TokoID,TokoCode "+
            //"UNION ALL  "+
            //"select A.SalesmanID,  "+
            //"(select SalesmanName from Salesman where ID=A.SalesmanID) as SalesmanName,  "+
            //"A.TokoCode,A.CreatedTime,  "+
            //"(case when (select TokoName from [192.168.99.3].GRCboard.dbo.Toko where TokoCode=A.TokoCode and Block=0 and RowStatus=0)!='' " +
            //"  then "+
            //"  (select TokoName from [192.168.99.3].GRCboard.dbo.Toko where TokoCode=A.TokoCode and Block=0 and RowStatus=0) " +
            //"  else "+
            //"  NamaToko "+
            //"  end) as TokoName, " +
            //"( "+
            //" case when (select top 1 Latitude from Toko_Location where TokoCode=A.TokoCode order by id desc)!='' then  "+
            //" (select top 1 Latitude from Toko_Location where TokoCode=A.TokoCode order by id desc) else "+
            //" Cord_LA "+
            //" end "+
            //")  as Cord_LA_Toko,  "+
            //"( "+
            //" case when (select top 1 Longitude from Toko_Location where TokoCode=A.TokoCode order by id desc)!='' then  "+
            //" (select top 1 Longitude from Toko_Location where TokoCode=A.TokoCode order by id desc) else "+
            //"  Cord_LO end "+
            //")  as Cord_LO_Toko, " +
            //"(select RIGHT('00'+CAST((X.TotalWaktu/3600) AS VARCHAR(2)),2)+':'+RIGHT('00'+CAST(((X.TotalWaktu%3600)/60) AS VARCHAR(2)),2)+':'+RIGHT('00'+CAST(((X.TotalWaktu%3600)%60) AS VARCHAR(2)),2) from ( " +
            //"select SUM(ISNULL(DATEDIFF(SECOND,Z.TimeStart,Z.TimeEnd),0)) as TotalWaktu  from ( "+
            //"select TokoCode,Time,Tanggal,SalesmanId,TimeStart,MAX(TimeEnd) as TimeEnd "+
            //"from KunjunganTimerToko where NamaTokoBaru=A.NamaToko and SalesmanId=A.SalesmanID and convert(varchar,Tanggal,112)=convert(varchar,A.CreatedTime,112) "+
            //"group by TokoCode,Time,Tanggal,SalesmanId,TimeStart "+
            //") as Z) as X ) as TimeDate, Temp_KabKotaUpdate " +
            //"from Toko_Baru as A  where RowStatus>=0 "+
            //") as Z "+
            //Query +
            //"order by " + OrderBy);
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select ROW_NUMBER()OVER (ORDER BY Z.UserTime) as Row,* from ( " +
            "select '' as AlamatToko,A.SalesmanID, " +
            "(select SalesmanName from Salesman where ID=A.SalesmanID) as SalesmanName, " +
            //"A.TokoCode,MIN(A.UserTime) as UserTime, " +
            "A.TokoCode,case when (MIN(A.UserTime) is null or MIN(A.UserTime)='') then MIN(A.CreatedTime) else MIN(A.UserTime) end as UserTime, " +
            "(select TokoName from GRCboard.dbo.Toko where TokoCode=A.TokoCode and Block=0 and RowStatus=0) as TokoName, " +
            "(select top 1 Latitude from Toko_Location where TokoCode=A.TokoCode order by id desc)  as Cord_LA_Toko, " +
            "(select top 1 Longitude from Toko_Location where TokoCode=A.TokoCode order by id desc)  as Cord_LO_Toko, " +
            "(select RIGHT('00'+CAST((X.TotalWaktu/3600) AS VARCHAR(2)),2)+':'+RIGHT('00'+CAST(((X.TotalWaktu%3600)/60) AS VARCHAR(2)),2)+':'+RIGHT('00'+CAST(((X.TotalWaktu%3600)%60) AS VARCHAR(2)),2) from ( " +
            "select SUM(ISNULL(DATEDIFF(SECOND,Z.TimeStart,Z.TimeEnd),0)) as TotalWaktu  from ( " +
            "select TokoCode,Tanggal,SalesmanId,TimeStart,MAX(TimeEnd) as TimeEnd " +
            "from KunjunganTimerToko where TokoCode=A.TokoCode and SalesmanId=A.SalesmanID and convert(varchar,Tanggal,112)=convert(varchar,case when (MIN(A.UserTime) is null or MIN(A.UserTime)='') then MIN(A.CreatedTime) else MIN(A.UserTime) end,112) " +
            "group by TokoCode,Tanggal,SalesmanId,TimeStart " +
            ") as Z) as X ) as TimeSum, '' as Temp_KabKotaUpdate " +
            "from KunjunganToko as A group by SalesmanID,convert(varchar,CreatedTime,112),TokoID,TokoCode " +
            "UNION ALL  " +
            "select CAST(A.AlamatToko AS NVARCHAR(100)) AlamatToko,A.SalesmanID,  " +
            "(select SalesmanName from Salesman where ID=A.SalesmanID) as SalesmanName,  " +
            "A.TokoCode,case when (MIN(A.UserTime) is null or MIN(A.UserTime)='') then MIN(A.CreatedTime) else MIN(A.UserTime) end as UserTime,  " +
            "(case when (select TokoName from GRCboard.dbo.Toko where TokoCode=A.TokoCode and Block=0 and RowStatus=0)!='' " +
            "  then " +
            "  (select TokoName from GRCboard.dbo.Toko where TokoCode=A.TokoCode and Block=0 and RowStatus=0) " +
            "  else " +
            "  NamaToko " +
            "  end) as TokoName, " +
            "( " +
            " case when (select top 1 Latitude from Toko_Location where TokoCode=A.TokoCode order by id desc)!='' then  " +
            " (select top 1 Latitude from Toko_Location where TokoCode=A.TokoCode order by id desc) else " +
            " Cord_LA " +
            " end " +
            ")  as Cord_LA_Toko,  " +
            "( " +
            " case when (select top 1 Longitude from Toko_Location where TokoCode=A.TokoCode order by id desc)!='' then  " +
            " (select top 1 Longitude from Toko_Location where TokoCode=A.TokoCode order by id desc) else " +
            "  Cord_LO end " +
            ")  as Cord_LO_Toko, " +
            "(select RIGHT('00'+CAST((X.TotalWaktu/3600) AS VARCHAR(2)),2)+':'+RIGHT('00'+CAST(((X.TotalWaktu%3600)/60) AS VARCHAR(2)),2)+':'+RIGHT('00'+CAST(((X.TotalWaktu%3600)%60) AS VARCHAR(2)),2) from ( " +
            "select SUM(ISNULL(DATEDIFF(SECOND,Z.TimeStart,Z.TimeEnd),0)) as TotalWaktu  from ( " +
            "select TokoCode,Tanggal,SalesmanId,TimeStart,MAX(TimeEnd) as TimeEnd " +
            "from KunjunganTimerToko where NamaTokoBaru=A.NamaToko and SalesmanId=A.SalesmanID and convert(varchar,Tanggal,112)=convert(varchar,case when (MIN(A.UserTime) is null or MIN(A.UserTime)='') then MIN(A.CreatedTime) else MIN(A.UserTime) end,112) " +
            "group by TokoCode,Tanggal,SalesmanId,TimeStart " +
            ") as Z) as X ) as TimeDate, Temp_KabKotaUpdate " +
            "from Toko_Baru as A  where RowStatus>=0 group by CAST(AlamatToko AS NVARCHAR(100)),SalesmanID,convert(varchar,CreatedTime,112),TokoCode,NamaToko,Cord_LA,Cord_LO,Temp_KabKotaUpdate " +
            ") as Z " +
            Query +
            "order by " + OrderBy);

            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObject5(sqlDataReader));
                }
            }
            else
                arrDataFor.Add(new DataFor());
            return arrDataFor;
        }
        public ArrayList RetrieveBy_ListTokoBaru(string From, string To, string depoid, string ket)
        {
            string ketr = string.Empty;
            if (ket == "DefCoord") { ketr = "Keterangan='' or A.Keterangan IS NULL or A.Keterangan='DefCoord'"; }
            else if (ket == "Baru") { ketr = "A.Keterangan='Baru'"; }
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select ROW_NUMBER()OVER (ORDER BY A.CreatedTime) as Row,A.ID,A.NamaToko,A.SalesmanID, " + 
            //"A.AlamatToko,A.RowStatus,A.CreatedTime,A.Cord_LO,A.Cord_LA, "+
            //"(select SalesmanName from Salesman where ID=A.SalesmanID) as SalesmanName "+
            //"from Toko_Baru as A where A.RowStatus>=0 and (Keterangan='' or A.Keterangan IS NULL or A.Keterangan='DefCoord') " +
            //"and convert(varchar,A.CreatedTime,112)>='" + From + "' and convert(varchar,A.CreatedTime,112)<='" + To + "' order by A.CreatedTime");
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select ROW_NUMBER()OVER (ORDER BY A.CreatedTime) as Row,A.ID,A.NamaToko,A.SalesmanID, "+
            //"A.AlamatToko,A.RowStatus,A.CreatedTime,A.Cord_LO,A.Cord_LA, "+
            //"(select SalesmanName from Salesman where ID=A.SalesmanID) as SalesmanName "+
            //"from Toko_Baru as A where "+
            //"A.SalesmanID in (select ID from Salesman where SalesmanID in (select ID from GRCboard.dbo.SalesMan  "+
            //"where UnitID in (select ID from GRCBoard.dbo.Distributor where DepoID in (" + depoid + ")))) and " +
            //"A.RowStatus>=0 and (Keterangan='' or A.Keterangan IS NULL or A.Keterangan='DefCoord') "+
            //"and convert(varchar,A.CreatedTime,112)>='" + From + "' and convert(varchar,A.CreatedTime,112)<='" + To + "' order by A.CreatedTime");
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select DISTINCT NamaToko,(select SalesmanName from GRCBoard.dbo.SalesMan where ID=(select SalesmanID from Salesman where ID=A.SalesmanID)) as SalesName, " +
            "(select DistributorName from GRCBoard.dbo.Distributor where ID=(select UnitID from GRCBoard.dbo.SalesMan where ID=(select SalesmanID from Salesman where ID=A.SalesmanID)and UnitSalesman=1)) as Distributor, " +
            "Cord_LA as Latitude,Cord_LO as Longitude,CreatedTime,REPLACE(CAST(A.AlamatToko AS varchar(MAX)), char(10), '') as AlamatToko "+
            "from Toko_Baru as A where (" + ketr + ")  and convert(varchar,A.CreatedTime,112)>='" + From + "' and convert(varchar,A.CreatedTime,112)<='" + To + "'  " +
            "and A.SalesmanID in (select ID from Salesman where SalesmanID in (select ID from GRCBoard.dbo.Salesman where RowStatus=0 and UnitSalesman=1 and UnitID in (select ID from GRCBoard.dbo.Distributor where DepoID in (" + depoid + ") and RowStatus=0))) " +
            "order by NamaToko,CreatedTime");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObject6(sqlDataReader));
                }
            }
            else
                arrDataFor.Add(new DataFor());
            return arrDataFor;
        }
        public ArrayList RetrieveBy_ListSalesmanPage(int JmlBaris, string query)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from(select(ROW_NUMBER()OVER (ORDER BY ID)/" + JmlBaris + ")+1 as Page from Salesman " + query + ") as A Group By A.Page");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObject3(sqlDataReader));
                }
            }
            else
                arrDataFor.Add(new DataFor());
            return arrDataFor;
        }
        public ArrayList RetrieveBy_ListSurveyorPage(int JmlBaris)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from(select(ROW_NUMBER()OVER (ORDER BY ID)/" + JmlBaris + ")+1 as Page from Surveyor) as A Group By A.Page");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObject3(sqlDataReader));
                }
            }
            else
                arrDataFor.Add(new DataFor());
            return arrDataFor;
        }
        public ArrayList RetrieveBy_ListTokoBaruPage(int JmlBaris, string query)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from(select(ROW_NUMBER()OVER (ORDER BY ID)/" + JmlBaris + ")+1 as Page from Toko_Baru " + query + ") as A Group By A.Page");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObject3(sqlDataReader));
                }
            }
            else
                arrDataFor.Add(new DataFor());
            return arrDataFor;
        }
        public ArrayList RetrieveBy_ListKunjunganPage(int JmlBaris, string query)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from(select(ROW_NUMBER()OVER (ORDER BY CreatedTime)/" + JmlBaris + ")+1 as Page from KunjunganToko " + query + ") as A Group By A.Page");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObject3(sqlDataReader));
                }
            }
            else
                arrDataFor.Add(new DataFor());
            return arrDataFor;
        }
        public ArrayList RetrieveProvinsi()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from GRCboard.dbo.Indonesia_Propinsi order by NamaPropinsi");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObjectProvinsi(sqlDataReader));
                }
            }
            else
                arrDataFor.Add(new DataFor());
            return arrDataFor;
        }
        public ArrayList RetrieveKabKota(int ProvinsiID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from GRCboard.dbo.Indonesia_KabKota where IndonesiaProvinsiID='" + ProvinsiID + "' order by NamaKabKota");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObjectKabKota(sqlDataReader));
                }
            }
            else
                arrDataFor.Add(new DataFor());
            return arrDataFor;
        }
        public ArrayList RetrieveBy_ListSalesmanKabupaten(string query)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select SalesmanName,(select STUFF (( SELECT DISTINCT ', ' + quotename(NamaKabKota, '') + '' FROM GRCboard.dbo.Indonesia_KabKota where ID in (select Indonesia_KabKota_ID from SalesmanKabupaten where SalesmanID=Salesman.ID)  " +
            "FOR XML PATH('')), 1, 1, '')) as NamaKabKota from Salesman "+
            "where SalesmanID in (select ID from Grcboard.dbo.SalesMan where  " +
            "UnitSalesman=1 and (UnitID in(select ID from Grcboard.dbo.Distributor where RowStatus>=0 and DepoID in(" + query + "))))");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObjectSalesKab(sqlDataReader));
                }
            }
            else
                arrDataFor.Add(new DataFor());
            return arrDataFor;
        }
        public ArrayList RetrieveBy_ListIdBroadcast(string query)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select IdBroadcast from Salesman where RowStatus>=0 and IdBroadcast!='' " + query);
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObjectIdBroadcast(sqlDataReader));
                }
            }
            else
                arrDataFor.Add(new DataFor());
            return arrDataFor;
        }
        public ArrayList RetrieveBy_ListTokoTerdaftar(string namaToko, string areatoko)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.*, "+
            "(select CityName from GRCBoard.dbo.City where ID=A.CityID) as City, " +
            "(select NamaKabupaten from GRCBoard.dbo.Kabupaten where ID=A.KabupatenID) as Kabupaten " +
            "from GRCBoard.dbo.Toko as A " + namaToko + " " + areatoko + " order by A.TokoName");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObject7(sqlDataReader));
                }
            }
            else
                arrDataFor.Add(new DataFor());
            return arrDataFor;
        }
        public ArrayList RetrieveBy_ListMesage(string DepoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from MessageBroadcast where DepoID like '%" + DepoID + "%' and status>=0 order by CreatedTime desc");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObject8(sqlDataReader));
                }
            }
            else
                arrDataFor.Add(new DataFor());
            return arrDataFor;
        }
        public ArrayList RetrieveBy_ListImeiPosition(string query)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select SalesmanName,IMEI, "+
            "Case when (select COUNT(ID) from ((select top 1 * from Imei_Location where IMEI=A.IMEI and CreatedTime>=DATEADD(MINUTE,-5,GETDATE()) order by CreatedTime desc)) as Count)>0 " +
            "then "+
            " (select top 1 Latitude from Imei_Location where IMEI=A.IMEI and CreatedTime>=DATEADD(MINUTE,-5,GETDATE()) order by CreatedTime desc) " +
            "END Cord_LA, "+
            "Case when (select COUNT(ID) from ((select top 1 * from Imei_Location where IMEI=A.IMEI and CreatedTime>=DATEADD(MINUTE,-5,GETDATE()) order by CreatedTime desc)) as Count)>0 " +
            "then "+
            " (select top 1 Longitude from Imei_Location where IMEI=A.IMEI and CreatedTime>=DATEADD(MINUTE,-5,GETDATE()) order by CreatedTime desc) " +
            "END Cord_LO, "+
            "Case when A.ThisProject=1 or A.ThisProject=2 then 2 else " +
			"Case when (select COUNT(ID) from ((select top 1 * from Imei_OnLine where IMEI=A.IMEI and CreatedTime>=DATEADD(MINUTE,-5,GETDATE()) order by CreatedTime desc)) as Count)>0 "+
            "then 1 else 0 END "+
            "END Bounce  "+
            "from Salesman as A "+ query);
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObject9(sqlDataReader));
                }
            }
            else
                arrDataFor.Add(new DataFor());
            return arrDataFor;
        }
        public ArrayList RetrieveBy_ListTracePosition(int salesId, string dateA, string dateB)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ( "+
            //"select MAX(Keterangan) as Keterangan,MAX(Latitude) as Latitude,MAX(Longitude) as Longitude,MAX(convert(varchar,CreatedTime,0)) as Tanggal "+
            //"from Imei_Location where IMEI in (select IMEI from Salesman where ID=" + salesId + " UNION ALL select CodeBefore as IMEI from Imei_Colection where SalesmanID=" + salesId + ") group by convert(varchar,CreatedTime,0)) as A  " +
            //"where  "+
            //"DATEPART(HOUR,CONVERT(datetime, Tanggal, 126))>=8 and DATEPART(HOUR,CONVERT(datetime, Tanggal, 126)) <= 17 and "+
            //"convert(varchar,CONVERT(datetime, Tanggal, 126),112)>='" + dateA + "' and convert(varchar,CONVERT(datetime, Tanggal, 126),112)<='" + dateB + "' " +
            //"order by convert(varchar,CONVERT(datetime, Tanggal, 126),120 )");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ( "+
            "select Keterangan,Latitude,Longitude,convert(varchar,CreatedTime,0) as CreatedTime "+
            "from Imei_Location where IMEI in (select IMEI from Salesman where ID=" + salesId + " UNION ALL select CodeBefore as IMEI from Imei_Colection where SalesmanID=" + salesId + ") " +
            "and  "+
            "DATEPART(HOUR,CONVERT(datetime, CreatedTime, 126))>=8 and DATEPART(HOUR,CONVERT(datetime, CreatedTime, 126)) <= 17 and "+
            "convert(varchar,CONVERT(datetime, CreatedTime, 126),112)>='" + dateA + "' and convert(varchar,CONVERT(datetime, CreatedTime, 126),112)<='" + dateB + "' " +
            ") as A order by convert(varchar,CONVERT(datetime, A.CreatedTime, 126),120 )");

            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObject9Trace(sqlDataReader));
                }
            }
            else
                arrDataFor.Add(new DataFor());
            return arrDataFor;
        }
        public ArrayList RetrieveByPenggunaAplikasi()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select COUNT(A.IMEI) as Jumlah,A.Tanggal from ( "+
             "select IMEI,CONVERT(VARCHAR(10),CreatedTime,111) as Tanggal from Imei_Location " +
             "where Keterangan not in ('GPS turned off [offline]','GPS turned off') "+
             "group by IMEI,CONVERT(VARCHAR(10),CreatedTime,111) ) as A group by A.Tanggal  " +
             "order by A.Tanggal");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObject10(sqlDataReader));
                }
            }
            else
                arrDataFor.Add(new DataFor());
            return arrDataFor;
        }
        public ArrayList RetrieveByPenggunaOnline()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select COUNT(A.IMEI) as Jumlah,A.Tanggal from ( " +
            "select IMEI,CONVERT(VARCHAR(10),CreatedTime,111 ) as Tanggal from Imei_OnLine " +
            "group by IMEI,CONVERT(VARCHAR(10),CreatedTime,111 ) ) as A group by A.Tanggal " +
            "order by A.Tanggal");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObject10(sqlDataReader));
                }
            }
            else
                arrDataFor.Add(new DataFor());
            return arrDataFor;
        }
        public ArrayList RetrieveByKunjunganToko()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select COUNT(A.TokoCode) as Jumlah,A.Tanggal from ( "+
            "select TokoCode,CONVERT(VARCHAR(10),CreatedTime,111) as Tanggal from KunjunganToko "+
            "group by  TokoCode,CONVERT(VARCHAR(10),CreatedTime,111) "+
            "UNION ALL "+
            "select NamaToko,CONVERT(VARCHAR(10),CreatedTime,111) as Tanggal from Toko_Baru where RowStatus=0 "+
            "group by  NamaToko,CONVERT(VARCHAR(10),CreatedTime,111)) as A group by A.Tanggal "+
            "order by A.Tanggal");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObject10(sqlDataReader));
                }
            }
            else
                arrDataFor.Add(new DataFor());
            return arrDataFor;
        }
        public ArrayList RetrieveBy_ListKabNol()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 500 ID,TokoCode,Indonesia_KabKotaID,Latitude,Longitude "+
            "from Toko_Location where TokoCode in ( " +
            "SELECT TokoCode "+
            "FROM (select * from Toko_Location) as A "+
            "WHERE (Temp_KabKotaUpdate is null or Temp_KabKotaUpdate='') and Indonesia_KabKotaID=0 " +
            "GROUP BY TokoCode "+
            "HAVING  COUNT(*) >= 1) "+
            "order by TokoCode");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObject11(sqlDataReader));
                }
            }
            else
                arrDataFor.Add(new DataFor());
            return arrDataFor;
        }
        public ArrayList RetrieveBy_ListKabNolTokoBaru()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 500 * from ( "+
			"select ID, "+
            "( "+
			" case when (select top 1 Latitude from Toko_Location where TokoCode=A.TokoCode order by id desc)!='' then  "+
			" (select top 1 Latitude from Toko_Location where TokoCode=A.TokoCode order by id desc) else "+
			" Cord_LA "+
			" end "+
			")  as Cord_LA_Toko,   "+
            "( "+
			" case when (select top 1 Longitude from Toko_Location where TokoCode=A.TokoCode order by id desc)!='' then  "+
			" (select top 1 Longitude from Toko_Location where TokoCode=A.TokoCode order by id desc) else "+
			"  Cord_LO end "+
			")  as Cord_LO_Toko  "+
            "from Toko_Baru as A  where RowStatus>=0 and (Temp_KabKotaUpdate='' or Temp_KabKotaUpdate is null) and "+
            "(Keterangan='DefProject' or (SalesmanID in (select ID from Salesman where ThisSurveyor=2))) " +
            "and Convert(varchar,CreatedTime,112)>='20170101' " +
			") as Z where Z.Cord_LA_Toko!='' and Z.Cord_LO_Toko!=''");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObject11A(sqlDataReader));
                }
            }
            else
                arrDataFor.Add(new DataFor());
            return arrDataFor;
        }
        public ArrayList RetrieveBy_ListDist(string DepoId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from GRCboard.dbo.Distributor where RowStatus=0 and DepoID in (" + DepoId + ")");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObject12a(sqlDataReader));
                }
            }
            else
                arrDataFor.Add(new DataFor());
            return arrDataFor;
        }
        public ArrayList RetrieveBy_ListSalesman(string DepoId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SalesmanName,(select DistributorCode from GRCBoard.dbo.Distributor where ID=(select UnitID from GRCBoard.dbo.SalesMan where ID=A.SalesmanID)) as DistributorCode " +
            "from Salesman as A where A.SalesmanID in (select ID from GRCBoard.dbo.SalesMan where UnitSalesman=1 and UnitID in (select ID from Grcboard.dbo.Distributor where RowStatus>=0 and DepoID in (" + DepoId + "))) order by A.SalesmanName");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObject12b(sqlDataReader));
                }
            }
            else
                arrDataFor.Add(new DataFor());
            return arrDataFor;
        }

        public ArrayList RetrieveBy_ListSalesmanLangsung()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SalesmanName,'' as DistributorCode from Salesman as A where A.SalesmanID in  "+
            "(select ID from GRCBoard.dbo.SalesMan where UnitSalesman=2 and UnitID=1) "+
            "and ThisSurveyor=2 order by A.SalesmanName");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObject12b(sqlDataReader));
                }
            }
            else
                arrDataFor.Add(new DataFor());
            return arrDataFor;
        }

        public ArrayList RetrieveBy_ListSalesman_Dist(int DistID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SalesmanName,(select DistributorCode from GRCBoard.dbo.Distributor where ID=(select UnitID from GRCBoard.dbo.SalesMan where ID=A.SalesmanID)) as DistributorCode " +
            "from Salesman as A where A.SalesmanID in (select ID from GRCBoard.dbo.SalesMan where UnitSalesman=1 and UnitID in (select ID from Grcboard.dbo.Distributor where RowStatus>=0 and ID=" + DistID + ")) order by A.SalesmanName");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObject12b(sqlDataReader));
                }
            }
            else
                arrDataFor.Add(new DataFor());
            return arrDataFor;
        }

        public ArrayList RetrieveBy_ListTokoLepasAsuh(string TokoCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select *,(select DistributorName from GRCBoard.dbo.Distributor where ID=A.DistributorID) as DistSebelum,  " +
            "(select DistributorName from GRCBoard.dbo.Distributor where ID=A.DistributorIDBerikutnya) as DistBerikut, " +
            "(select TokoName from GRCBoard.dbo.Toko where TokoCode=A.TokoCode and Block=0 and RowStatus>=0) as TokoName, " +
			"'Posting Closing' as Keterangan  "+
            "from Toko_AsuhLepas_Posting as A where (A.StatusToko='Lepas' or A.StatusToko='') and  "+
            "A.StatusTokoBerikutnya='Asuh' and  "+
            "A.TokoCode not in(select TokoCode from Toko_AsuhLepas where TidakIkutClosing=1) and  "+
            "CONVERT(varchar, A.Postingdate, 112)=(select top 1 CONVERT(varchar, PostingDate, 112)  "+
            "from Toko_AsuhLepas_Posting order by PostingDate desc) "+ TokoCode +" "+
			"UNION ALL "+
			"select ID,TokoCode,DistributorID,0 as omset,0 as Kunjungan,'' as PeriodeOmsetFrom,'' as PeriodeOmsetTo,CreatedTime as PostingDate, "+
			"'Lepas' as StatusToko,'Asuh' as StatusTokoBerikutnya,DistributorID as DistributorIDBerikutnya, "+
            "(select DistributorName from GRCBoard.dbo.Distributor where ID=A.DistributorID) as DistSebelum, " +
            "(select DistributorName from GRCBoard.dbo.Distributor where ID=A.DistributorID) as DistBerikut,  " +
            "(select TokoName from GRCBoard.dbo.Toko where TokoCode=A.TokoCode and Block=0 and RowStatus>=0) as TokoName, " +
            "'Auto dari (Kunjungan dan Omset 1 minggu setelahnya)' as Keterangan " +
			"from Toko_AsuhLepas as A where CreatedBy='SysAuto' and MONTH(CreatedTime)=MONTH(DATEADD(MONTH, -1, GETDATE())) and YEAR(CreatedTime)=YEAR(DATEADD(MONTH, -1, GETDATE())) " + TokoCode);
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObject13(sqlDataReader));
                }
            }
            else
            { arrDataFor.Add(new DataFor()); }
            return arrDataFor;
        }
        public ArrayList RetrieveBy_ListTokoAsuhLepas(string TokoCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select *,(select DistributorName from GRCBoard.dbo.Distributor where ID=A.DistributorID) as DistSebelum, " +
            "(select DistributorName from GRCBoard.dbo.Distributor where ID=A.DistributorIDBerikutnya) as DistBerikut, " +
            "(select TokoName from GRCBoard.dbo.Toko where TokoCode=A.TokoCode and Block=0 and RowStatus>=0) as TokoName " +
            "from Toko_AsuhLepas_Posting as A where (A.StatusToko='Asuh' or A.StatusToko='') and "+
            "A.StatusTokoBerikutnya='Lepas' and "+
            "A.TokoCode not in(select TokoCode from Toko_AsuhLepas where TidakIkutClosing=1) and "+
            "CONVERT(varchar, A.Postingdate, 112)=(select top 1 CONVERT(varchar, PostingDate, 112) "+
            "from Toko_AsuhLepas_Posting order by PostingDate desc) " + TokoCode);
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObject14(sqlDataReader));
                }
            }
            else
            { arrDataFor.Add(new DataFor()); }
            return arrDataFor;
        }
        public ArrayList RetrieveBy_ListTokoAsuhAsuh(string TokoCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select *,(select DistributorName from GRCBoard.dbo.Distributor where ID=A.DistributorID) as DistSebelum, " +
            "(select DistributorName from GRCBoard.dbo.Distributor where ID=A.DistributorIDBerikutnya) as DistBerikut, " +
            "(select TokoName from GRCBoard.dbo.Toko where TokoCode=A.TokoCode and Block=0 and RowStatus>=0) as TokoName " +
            "from Toko_AsuhLepas_Posting as A where (A.StatusToko='Asuh') and " +
            "A.StatusTokoBerikutnya='Asuh' and " +
            "A.TokoCode not in(select TokoCode from Toko_AsuhLepas where TidakIkutClosing=1) and " +
            "CONVERT(varchar, A.Postingdate, 112)=(select top 1 CONVERT(varchar, PostingDate, 112) " +
            "from Toko_AsuhLepas_Posting order by PostingDate desc) " + TokoCode);
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObject14(sqlDataReader));
                }
            }
            else
            { arrDataFor.Add(new DataFor()); }
            return arrDataFor;
        }
        public ArrayList RetrieveBy_ListTokoLepasLepas(string TokoCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select *,(select DistributorName from GRCBoard.dbo.Distributor where ID=A.DistributorID) as DistSebelum, " +
            "(select DistributorName from GRCBoard.dbo.Distributor where ID=A.DistributorIDBerikutnya) as DistBerikut, " +
            "(select TokoName from GRCBoard.dbo.Toko where TokoCode=A.TokoCode and Block=0 and RowStatus>=0) as TokoName " +
            "from Toko_AsuhLepas_Posting as A where (A.StatusToko='Lepas') and " +
            "A.StatusTokoBerikutnya='Lepas' and " +
            "A.TokoCode not in(select TokoCode from Toko_AsuhLepas where TidakIkutClosing=1) and " +
            "CONVERT(varchar, A.Postingdate, 112)=(select top 1 CONVERT(varchar, PostingDate, 112) " +
            "from Toko_AsuhLepas_Posting order by PostingDate desc) " + TokoCode);
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObject14(sqlDataReader));
                }
            }
            else
            { arrDataFor.Add(new DataFor()); }
            return arrDataFor;
        }
        public ArrayList RetrieveBy_KunjSalesDetail(string TglMulai,string TglAkhir,string DepoId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,(select DistributorName from GRCBoard.dbo.Distributor where ID=B.UnitID) as Distributor,A.SalesmanName, " +
            "ISNULL( "+
            "(  "+
            "	select COUNT(JumlahToko) as JumlahToko from  "+
            "	( "+
            "		select COUNT(TokoCode) as JumlahToko from "+
            "		( "+
            "			select TokoCode,Convert(varchar,UserTime,112) as TglKunjungan,SalesmanID from KunjunganToko " +
            "			where Convert(varchar,UserTime,112)>='" + TglMulai + "' and Convert(varchar,UserTime,112)<='" + TglAkhir + "' and SalesmanID=A.ID " +
            "			UNION ALL "+
            "			select NamaToko,Convert(varchar,UserTime,112) as TglKunjungan,SalesmanID from Toko_Baru " +
            "			where Convert(varchar,UserTime,112)>='" + TglMulai + "' and Convert(varchar,UserTime,112)<='" + TglAkhir + "' and SalesmanID=A.ID and RowStatus>=0 " +
            "		) as Z "+
            "		group by Z.TokoCode,Z.TglKunjungan,Z.SalesmanID "+
            "	) as Y "+
            "),0) as JmlKunjungan " +
            "from Salesman as A, GRCBoard.dbo.SalesMan as B " +
            "where A.SalesmanID=B.ID and A.ThisSurveyor!=1 and B.UnitSalesman=1 and B.UnitID in  "+
            "(select ID from GRCBoard.dbo.Distributor where DepoID in (" + DepoId + ") and RowStatus>=0) and  " +
            "A.RowStatus>=0 and B.RowStatus>=0 order by Distributor");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObject15(sqlDataReader));
                }
            }
            else
            { arrDataFor.Add(new DataFor()); }
            return arrDataFor;
        }
        public ArrayList RetrieveBy_KunjSurveyorHarian(string TglMulai, string TglAkhir, string DepoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Convert(date,TglKunjungan,112) as TglKunjungan, COUNT(TokoCode) as JumlahToko,SalesmanID, "+
		    "(select SalesmanName from Salesman where ID=Z.SalesmanID) as Name  "+
		    "from   "+
		    "(  "+
            "    select TokoCode,Convert(date,UserTime,112) as TglKunjungan,SalesmanID from KunjunganToko " +
            "    where Convert(varchar,UserTime,112)>='" + TglMulai + "' and Convert(varchar,UserTime,112)<='" + TglAkhir + "' " +
            "	group by TokoCode,Convert(date,UserTime,112),SalesmanID " +
			"    UNION ALL "+
            "    select NamaToko,Convert(date,UserTime,112) as TglKunjungan,SalesmanID from Toko_Baru   " +
            "    where Convert(varchar,UserTime,112)>='" + TglMulai + "' and Convert(varchar,UserTime,112)<='" + TglAkhir + "' " +
            "    and RowStatus>=0 group by NamaToko,Convert(date,UserTime,112),SalesmanID " +
		    ") as Z  "+
            "where Z.SalesmanID in (select ID from Salesman where RowStatus=0 and ThisSurveyor=1) "+
            "group by Convert(date,Z.TglKunjungan,112),Z.SalesmanID");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObject16(sqlDataReader));
                }
            }
            else
            { arrDataFor.Add(new DataFor()); }
            return arrDataFor;
        }
        public ArrayList RetrieveBy_KunjDistDetail(string TglMulai, string TglAkhir, string DepoId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A1.Distributor,MAX(JmlToko) as JmlToko,SUM(A1.JmlKunjungan) as JmlTokoDiKunjungi,SUM(A1.JmlKunjungan2x) as JmlTokoDiKunjungi2x from " +
		    "( "+
            "	select (select DistributorName from GRCBoard.dbo.Distributor where ID=B.UnitID) as Distributor, " +
            "	(select Count(ID) from GRCBoard.dbo.Toko where DistributorID=B.UnitID and Block=0 and RowStatus=0) as JmlToko, " +
            "    ISNULL( "+
            "    ( "+
            "    	select SUM(JumlahToko) as JumlahToko from "+
            "    	( "+
            "    		select COUNT(TokoCode) as JumlahToko from "+
            "    		( "+
            "    			select TokoCode,Convert(varchar,UserTime,112) as TglKunjungan,SalesmanID from KunjunganToko " +
            "    			where Convert(varchar,UserTime,112)>='" + TglMulai + "' and Convert(varchar,UserTime,112)<='" + TglAkhir + "' and SalesmanID=A.ID " +
            "    			UNION ALL "+
            "    			select TokoCode,Convert(varchar,UserTime,112) as TglKunjungan,SalesmanID from Toko_Baru " +
            "    			where Convert(varchar,UserTime,112)>='" + TglMulai + "' and Convert(varchar,UserTime,112)<='" + TglAkhir + "' and SalesmanID=A.ID and Keterangan='TokoTerdaftar' and TokoCode!='' " +
            "    		) as Z "+
            "    		group by Z.TokoCode,Z.TglKunjungan,Z.SalesmanID "+
            "    	) as Y "+
            "    ),0) as JmlKunjungan, "+
            "    ISNULL( "+
            "    ( "+
            "    	select Count(TokoCode) as JumlahToko from "+
            "    	( "+
		    "			select TokoCode,((Convert(varchar,YEAR(TglKunjungan),112)+''+Convert(varchar,MONTH(TglKunjungan),112))) as TglKunjungan,COUNT(TokoCode) as JmlDikunjungi,SalesmanID from "+
		    "			( "+
            "    			select TokoCode,TglKunjungan,SalesmanID from "+
            "    			( "+
            "    				select TokoCode,Convert(varchar,UserTime,112) as TglKunjungan,SalesmanID from KunjunganToko " +
            "    				where Convert(varchar,UserTime,112)>='" + TglMulai + "' and Convert(varchar,UserTime,112)<='" + TglAkhir + "' and SalesmanID=A.ID " +
            "    				UNION ALL "+
            "    				select TokoCode,Convert(varchar,UserTime,112) as TglKunjungan,SalesmanID from Toko_Baru " +
            "    				where Convert(varchar,UserTime,112)>='" + TglMulai + "' and Convert(varchar,UserTime,112)<='" + TglAkhir + "' and SalesmanID=A.ID and Keterangan='TokoTerdaftar' and TokoCode!='' " +
            "    			) as Z "+
            "    			group by Z.TokoCode,Z.TglKunjungan,Z.SalesmanID "+
		    "			) as X group by X.TokoCode,((Convert(varchar,YEAR(X.TglKunjungan),112)+''+Convert(varchar,MONTH(X.TglKunjungan),112))),X.SalesmanID "+
            "    	) as Y where  Y.JmlDikunjungi>=2 "+
            "    ),0) as JmlKunjungan2x  "+
            "    from Salesman as A, GRCBoard.dbo.SalesMan as B " +
            "    where A.SalesmanID=B.ID and A.ThisSurveyor!=1 and B.UnitSalesman=1 and B.UnitID in "+
            "    (select ID from GRCBoard.dbo.Distributor where DepoID in (" + DepoId + ") and RowStatus>=0) and " +
            "    A.RowStatus>=0 and B.RowStatus>=0 "+
            ") as A1 Group by A1.Distributor order by Distributor");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObject17(sqlDataReader));
                }
            }
            else
            { arrDataFor.Add(new DataFor()); }
            return arrDataFor;
        }

        public ArrayList RetrieveBy_ListRpt(int SalesmanID, string keygen)
        {
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select D.TanggalKunj,D.TokoCodeKunj,D.TokoName,ISNULL(D.Time,'0:00') as Time,'Tap' as Keterangan from( " +
            //"select DISTINCT * from ( "+
            //"select CONVERT(varchar,CreatedTime,112) as TanggalKunj,(A.TokoCode) as TokoCodeKunj, "+
            //"(select TokoName from GRCboard.dbo.Toko where TokoCode=A.TokoCode and Block=0 and RowStatus=0) as TokoName,(SalesmanID) as SalesmanIDKunj "+
            //"from KunjunganToko as A  where SalesmanID=" + SalesmanID + " and (select COUNT(ID) from Salesman where ID=" + SalesmanID + " and KeyGenerateAccess='" + keygen + "' and RowStatus >= 0)>0 " +
            //"and CreatedTime>=DATEADD(DAY,-14,GETDATE()) "+
            //") as B "+
            //"FULL OUTER JOIN KunjunganTimerToko as C "+
            //"ON CONVERT(varchar,C.CreatedTime,112)=CONVERT(varchar,B.TanggalKunj,112) and C.TokoCode=B.TokoCodeKunj "+
            //"where B.SalesmanIDKunj=" + SalesmanID + " and (C.SalesmanId=" + SalesmanID + " or C.SalesmanId IS NULL) " +
            //") as D "+
            //"where D.TokoCodeKunj is not null and D.TanggalKunj>=DATEADD(DAY,-14,GETDATE()) "+
            //"UNION ALL "+
            //"select * from ( "+
            //"select CONVERT(date,A.CreatedTime) as Tanggal,A.TokoCode,A.NamaToko,'' as Time,'Rev. Coord' as Keterangan  "+
            //"from Toko_Baru as A  where Keterangan in ('TokoTerdaftar','DefCoord','Baru') and RowStatus>=0 and "+
            //"A.SalesmanID=" + SalesmanID + " and (select COUNT(ID) from Salesman where ID=" + SalesmanID + " and KeyGenerateAccess='" + keygen + "' and RowStatus >= 0)>0 " +
            //"and A.RowStatus>=0 ) as D where D.Tanggal>=DATEADD(DAY,-14,GETDATE()) group by D.Tanggal,D.TokoCode,D.NamaToko,D.Time,D.Keterangan "+
            //"order by TanggalKunj desc");
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select D.Date,D.TanggalKunj,D.TokoCodeKunj,D.TokoName,ISNULL(D.Time,'0:00') as Time,'Tap' as Keterangan from( "+
            //"select Distinct B.*,C.TokoCode,C.Time,C.Tanggal,C.SalesmanId,C.RowStatus,C.TimeStart,C.TimeEnd, "+
            //"Case when C.CreatedTime IS NULL Then B.Date1 else C.CreatedTime End Date from (  "+
            //"select CreatedTime as Date1,CONVERT(varchar,CreatedTime,112) as TanggalKunj,(A.TokoCode) as TokoCodeKunj,  "+
            //"(select TokoName from GRCboard.dbo.Toko where TokoCode=A.TokoCode and Block=0 and RowStatus=0) as TokoName,(SalesmanID) as SalesmanIDKunj  "+
            //"from KunjunganToko as A  where SalesmanID=" + SalesmanID + " and (select COUNT(ID) from Salesman where ID=" + SalesmanID + " and KeyGenerateAccess='" + keygen + "' and RowStatus >= 0)>0  " +
            //"and CreatedTime>=DATEADD(DAY,-14,GETDATE()) and A.TokoCode!='' "+
            //") as B  "+
            //"FULL OUTER JOIN KunjunganTimerToko as C  "+
            //"ON CONVERT(varchar,C.CreatedTime,112)=CONVERT(varchar,B.TanggalKunj,112) and C.TokoCode=B.TokoCodeKunj  "+
            //"where B.SalesmanIDKunj=" + SalesmanID + " and (C.SalesmanId=" + SalesmanID + " or C.SalesmanId IS NULL)  " +
            //") as D  "+
            //"where D.TokoCodeKunj is not null and D.TanggalKunj>=DATEADD(DAY,-14,GETDATE()) "+
            //"UNION ALL  "+
            //"select D.Date,D.TanggalKunj,D.TokoCodeKunj,D.TokoName,ISNULL(D.Time,'0:00') as Time,'Rev. Coord' as Keterangan from( "+
            //"select Distinct B.*,C.TokoCode,C.Time,C.Tanggal,C.SalesmanId,C.RowStatus,C.TimeStart,C.TimeEnd,  "+
            //"Case when C.CreatedTime IS NULL Then B.Date1 else C.CreatedTime End Date from (   "+
            //"select CreatedTime as Date1,CONVERT(varchar,CreatedTime,112) as TanggalKunj,(A.TokoCode) as TokoCodeKunj,   "+
            //"(A.NamaToko) as TokoName,(SalesmanID) as SalesmanIDKunj   "+
            //"from Toko_Baru as A  where A.Keterangan in ('TokoTerdaftar','DefCoord','Baru') and A.RowStatus>=0 and "+
            //"A.SalesmanID=" + SalesmanID + " and (select COUNT(ID) from Salesman where ID=" + SalesmanID + " and KeyGenerateAccess='" + keygen + "' and RowStatus >= 0)>0   " +
            //"and CreatedTime>=DATEADD(DAY,-14,GETDATE())   "+
            //") as B   "+
            //"FULL OUTER JOIN KunjunganTimerToko as C   "+
            //"ON CONVERT(varchar,C.CreatedTime,112)=CONVERT(varchar,B.TanggalKunj,112) and C.NamaTokoBaru=B.TokoName and (C.TokoCode='' or C.TokoCode='...') "+
            //"where B.SalesmanIDKunj=" + SalesmanID + " and (C.SalesmanId=" + SalesmanID + " or C.SalesmanId IS NULL)   " +
            //") as D   "+
            //"where D.TokoCodeKunj is not null and D.TanggalKunj>=DATEADD(DAY,-14,GETDATE())  " +
            //"order by Date desc");
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select MAX(B.Date1) as Date1,B.TanggalKunj,B.TokoCodeKunj,B.TokoName,B.Time,B.Keterangan from ( "+
            "select case when (A.UserTime is null or A.UserTime='') then A.CreatedTime else A.UserTime end as Date1, "+
            "CONVERT(varchar,case when (A.UserTime is null or A.UserTime='') then A.CreatedTime else A.UserTime end,112) as TanggalKunj,(A.TokoCode) as TokoCodeKunj,  " +
            "(select TokoName from GRCboard.dbo.Toko where TokoCode=A.TokoCode and Block=0 and RowStatus=0) as TokoName, " +
            " ISNULL(( "+
            "  select RIGHT('00'+CAST((X.TotalWaktu/3600) AS VARCHAR(2)),2)+':'+RIGHT('00'+CAST(((X.TotalWaktu%3600)/60) AS VARCHAR(2)),2)+':'+RIGHT('00'+CAST(((X.TotalWaktu%3600)%60) AS VARCHAR(2)),2) from ( "+
            "  select Z.TokoCode,SUM(ISNULL(DATEDIFF(SECOND,Z.TimeStart,Z.TimeEnd),0)) as TotalWaktu from ( "+
            "  select TokoCode,Tanggal,TimeStart,MAX(TimeEnd) as TimeEnd from KunjunganTimerToko where salesmanID=A.SalesmanId and TokoCode=A.TokoCode Group By TokoCode,Tanggal,TimeStart "+
            "  ) as Z where Z.Tanggal=convert(varchar,case when (A.UserTime is null or A.UserTime='') then A.CreatedTime else A.UserTime end,112) and Z.TOkoCode=A.TokoCode Group By TokoCode " +
            "  ) as X),'0:00' "+
            " ) as Time,'Tap' as Keterangan "+
            "from KunjunganToko as A  where SalesmanID=" + SalesmanID + " and (select COUNT(ID) from Salesman where ID=" + SalesmanID + " and KeyGenerateAccess='" + keygen + "' and RowStatus >= 0)>0   " +
            "and A.TokoCode!='' "+
            ") as B where B.Date1<datediff(DAY, -7, GETDATE()) group by B.TanggalKunj,B.TokoCodeKunj,B.TokoName,B.Time,B.Keterangan " +
            "UNION ALL "+
            "select MAX(B.Date1) as Date1,B.TanggalKunj,B.TokoCodeKunj,B.NamaToko,B.Time,B.Keterangan from ( " +
            "select case when (A.UserTime is null or A.UserTime='') then A.CreatedTime else A.UserTime end as Date1, "+
            "CONVERT(varchar,case when (A.UserTime is null or A.UserTime='') then A.CreatedTime else A.UserTime end,112) as TanggalKunj,(A.TokoCode) as TokoCodeKunj,A.NamaToko, " +
            "ISNULL(( "+
            "  select RIGHT('00'+CAST((X.TotalWaktu/3600) AS VARCHAR(2)),2)+':'+RIGHT('00'+CAST(((X.TotalWaktu%3600)/60) AS VARCHAR(2)),2)+':'+RIGHT('00'+CAST(((X.TotalWaktu%3600)%60) AS VARCHAR(2)),2) from ( " +
            "  select Z.NamaTokoBaru,SUM(ISNULL(DATEDIFF(SECOND,Z.TimeStart,Z.TimeEnd),0)) as TotalWaktu from ( "+
            "  select NamaTokoBaru,Tanggal,TimeStart,MAX(TimeEnd) as TimeEnd from KunjunganTimerToko where salesmanID=A.SalesmanId and NamaTokoBaru=A.NamaToko "+
            "  and (TokoCode='' or TokoCode like '%...%' or TokoCode like '%|%') "+
            "  Group By NamaTokoBaru,Tanggal,TimeStart "+
            "  ) as Z where Z.Tanggal=convert(varchar,case when (A.UserTime is null or A.UserTime='') then A.CreatedTime else A.UserTime end,112) and Z.NamaTokoBaru=A.NamaToko Group By NamaTokoBaru " +
            "  ) as X),'0:00') as Time,'Rev. Coord' as Keterangan  "+
            "from Toko_Baru as A  where A.Keterangan in ('TokoTerdaftar','DefCoord','Baru') and A.RowStatus>=0 and  "+
            "A.SalesmanID=" + SalesmanID + " and (select COUNT(ID) from Salesman where ID=" + SalesmanID + " and KeyGenerateAccess='" + keygen + "' and RowStatus >= 0)>0    " +
            "   "+
            ") as B where B.Date1<datediff(DAY, -7, GETDATE()) group by B.TanggalKunj,B.TokoCodeKunj,B.NamaToko,B.Time,B.Keterangan " +
            "UNION ALL "+
            "select MAX(B.Date1) as Date1,B.TanggalKunj,B.TokoCodeKunj,B.NamaToko,B.Time,B.Keterangan from ( " +
            "select case when (A.UserTime is null or A.UserTime='') then A.CreatedTime else A.UserTime end as Date1, "+
            "CONVERT(varchar,case when (A.UserTime is null or A.UserTime='') then A.CreatedTime else A.UserTime end,112) as TanggalKunj,(A.TokoCode) as TokoCodeKunj,A.NamaToko, " +
            "ISNULL(( "+
            "  select RIGHT('00'+CAST((X.TotalWaktu/3600) AS VARCHAR(2)),2)+':'+RIGHT('00'+CAST(((X.TotalWaktu%3600)/60) AS VARCHAR(2)),2)+':'+RIGHT('00'+CAST(((X.TotalWaktu%3600)%60) AS VARCHAR(2)),2) from ( " +
            "  select Z.NamaTokoBaru,SUM(ISNULL(DATEDIFF(SECOND,Z.TimeStart,Z.TimeEnd),0)) as TotalWaktu from ( "+
            "  select NamaTokoBaru,Tanggal,TimeStart,MAX(TimeEnd) as TimeEnd from KunjunganTimerToko where salesmanID=A.SalesmanId and NamaTokoBaru=A.NamaToko "+
            "  Group By NamaTokoBaru,Tanggal,TimeStart "+
            "  ) as Z where Z.Tanggal=convert(varchar,case when (A.UserTime is null or A.UserTime='') then A.CreatedTime else A.UserTime end,112) and Z.NamaTokoBaru=A.NamaToko Group By NamaTokoBaru " +
            "  ) as X),'0:00') as Time,'Project' as Keterangan  "+
            "from Toko_Baru as A  where A.Keterangan in ('DefProject') and A.RowStatus>=0 and  "+
            "A.SalesmanID=" + SalesmanID + " and (select COUNT(ID) from Salesman where ID=" + SalesmanID + " and KeyGenerateAccess='" + keygen + "' and RowStatus >= 0)>0    " +
            " "+
            ") as B  where B.Date1<datediff(DAY, -7, GETDATE()) group by B.TanggalKunj,B.TokoCodeKunj,B.NamaToko,B.Time,B.Keterangan " +
            "order by Date1 desc ");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObject18(sqlDataReader));
                }
            }
            else
            { arrDataFor.Add(new DataFor()); }
            return arrDataFor;
        }

        public ArrayList RetrieveBy_ListImgBaner(int SalesmanID, string keygen)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ImageBanner where RowStatus=0 and DepoID in (select DepoID from GRCBoard.dbo.Distributor where ID in " +
            "(select UnitID from GRCBoard.dbo.SalesMan where ID= " +
            "(select SalesmanID from Salesman where ID='" + SalesmanID + "' and KeyGenerateAccess='" + keygen + "'))) order by PeriodeStart desc");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObject19(sqlDataReader));
                }
            }
            else
            { arrDataFor.Add(new DataFor()); }
            return arrDataFor;
        }
        public ArrayList RetrieveBy_ListMsg(int SalesmanID, string keygen, string field)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(
			"select * from MessageBroadcast where ((DepoID like '%'+Convert(varchar,(  "+
            "select DepoID from GRCBoard.dbo.Distributor where ID=(select UnitID from GRCBoard.dbo.SalesMan where ID=  " +
            "(select SalesmanID from SalesMan where ID='" + SalesmanID + "' and KeyGenerateAccess='" + keygen + "')))  " +
            ")+'%' and SalesmanId=0) or SalesmanID=(select ID from SalesMan where ID='" + SalesmanID + "' and KeyGenerateAccess='" + keygen + "')) " +
            "and Status>=0 and " + field + "=1 order by CreatedTime desc");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObject20(sqlDataReader));
                }
            }
            else
            { arrDataFor.Add(new DataFor()); }
            return arrDataFor;
        }
        public ArrayList RetrieveBy_SalesBayar(string DepoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SalesmanID,(select SalesmanName from GRCBoard.dbo.Salesman where ID=A.SalesmanID) as SalesmanName, " +
            "(Select DistributorName from GRCBoard.dbo.Distributor where ID=(select UnitID from GRCBoard.dbo.Salesman " +
            "where ID=A.SalesmanID)) as DistributorName,(select NamaKabKota from GRCBoard.dbo.Indonesia_KabKota where ID=B.Indonesia_KabKota_ID) as Kabupaten, " +
            "(B.CreatedTime) as Tanggal "+
            "from Salesman as A,SalesmanKabupaten as B where A.ID=B.SalesmanID and "+
            "A.TypeApp='Bayar' and A.ThisSurveyor=0 and A.SalesmanID in ( "+
            "select ID from GRCBoard.dbo.Salesman where UnitSalesman=1 and UnitID in  " +
            "(select ID from GRCBoard.dbo.Distributor where RowStatus=0 and DepoID in (" + DepoID + "))) " +
            "and A.RowStatus=0 and B.RowStatus=0 order by A.SalesmanName " +
            "");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObject21(sqlDataReader));
                }
            }
            else
            { arrDataFor.Add(new DataFor()); }
            return arrDataFor;
        }
        public ArrayList RetrieveBy_KunjSurveyorPemantauan(string TglMulai, string TglAkhir, string DepoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ( "+
            "select D.SalesmanID,(select SalesmanName from Salesman where id=D.SalesmanID) as Name,SUM(D.Jumlah) as Jumlah,D.Time  from ( "+
            "select SalesmanID,Count(TokoCode) as Jumlah, '<9.15' as Time from ( "+
            "select B.SalesmanID,B.TokoCode,CONVERT(varchar,B.Time,112) as Tanggal from ( "+
            "select A.SalesmanID,A.TokoCode, case when Time IS NULL then A.CreatedTime else A.Time end as Time from ( "+
            "select *,(select MIN(TimeStart) from KunjunganTimerToko where TokoCode=A.TokoCode and SalesmanID=A.SalesmanID and  "+
            "CONVERT(varchar,Tanggal,112)=CONVERT(varchar,A.CreatedTime,112)) as Time from KunjunganToko as A "+
            "where A.SalesmanID in (select ID from Salesman where ThisSurveyor=1)) as A where  "+
            "((DATEPART(HOUR,A.Time)<=9 and DATEPART(MINUTE,A.Time)<=15) or (DATEPART(HOUR,A.Time)<=8 and DATEPART(MINUTE,A.Time)<=60)) and "+
            "CONVERT(varchar,A.Time,112)>='" + TglMulai + "' and CONVERT(varchar,A.Time,112)<='" + TglAkhir + "' ) as B group by B.SalesmanID,B.TokoCode, " +
            "CONVERT(varchar,B.Time,112)) as C group by C.SalesmanID "+
            "UNION ALL "+
            "select SalesmanID,Count(TokoCode) as Jumlah,'>9.15' as Time from ( "+
            "select B.SalesmanID,B.TokoCode,CONVERT(varchar,B.Time,112) as Tanggal from ( "+
            "select A.SalesmanID,A.TokoCode, case when Time IS NULL then A.CreatedTime else A.Time end as Time from ( "+
            "select *,(select MIN(TimeStart) from KunjunganTimerToko where TokoCode=A.TokoCode and SalesmanID=A.SalesmanID and  "+
            "CONVERT(varchar,Tanggal,112)=CONVERT(varchar,A.CreatedTime,112)) as Time from KunjunganToko as A "+
            "where A.SalesmanID in (select ID from Salesman where ThisSurveyor=1)) as A where  "+
            "((DATEPART(HOUR,A.Time)>9 and DATEPART(MINUTE,A.Time)>15) or (DATEPART(HOUR,A.Time)>=10)) and "+
            "CONVERT(varchar,A.Time,112)>='" + TglMulai + "' and CONVERT(varchar,A.Time,112)<='" + TglAkhir + "' ) as B group by B.SalesmanID,B.TokoCode, " +
            "CONVERT(varchar,B.Time,112)) as C group by C.SalesmanID "+
            "UNION ALL "+
            "select SalesmanID,Count(TokoCode) as Jumlah,'<9.15' as Time from ( "+
            "select B.SalesmanID,B.TokoCode,CONVERT(varchar,B.Time,112) as Tanggal from ( "+
            "select A.SalesmanID,A.TokoCode, case when Time IS NULL then A.CreatedTime else A.Time end as Time from ( "+
            "select *,(select MIN(TimeStart) from KunjunganTimerToko where NamaToko=A.NamaToko and SalesmanID=A.SalesmanID and  "+
            "CONVERT(varchar,Tanggal,112)=CONVERT(varchar,A.CreatedTime,112)) as Time from Toko_Baru as A "+
            "where A.SalesmanID in (select ID from Salesman where ThisSurveyor=1)) as A where  "+
            "((DATEPART(HOUR,A.Time)<=9 and DATEPART(MINUTE,A.Time)<=15) or (DATEPART(HOUR,A.Time)<=8 and DATEPART(MINUTE,A.Time)<=60)) and "+
            "CONVERT(varchar,A.Time,112)>='" + TglMulai + "' and CONVERT(varchar,A.Time,112)<='" + TglAkhir + "' ) as B group by B.SalesmanID,B.TokoCode, " +
            "CONVERT(varchar,B.Time,112)) as C group by C.SalesmanID "+
            "UNION ALL "+
            "select SalesmanID,Count(TokoCode) as Jumlah,'>9.15' as Time from ( "+
            "select B.SalesmanID,B.TokoCode,CONVERT(varchar,B.Time,112) as Tanggal from ( "+
            "select A.SalesmanID,A.TokoCode, case when Time IS NULL then A.CreatedTime else A.Time end as Time from ( "+
            "select *,(select MIN(TimeStart) from KunjunganTimerToko where NamaToko=A.NamaToko and SalesmanID=A.SalesmanID and  "+
            "CONVERT(varchar,Tanggal,112)=CONVERT(varchar,A.CreatedTime,112)) as Time from Toko_Baru as A "+
            "where A.SalesmanID in (select ID from Salesman where ThisSurveyor=1)) as A where  "+
            "((DATEPART(HOUR,A.Time)>9 and DATEPART(MINUTE,A.Time)>15) or (DATEPART(HOUR,A.Time)>=10)) and "+
            "CONVERT(varchar,A.Time,112)>='" + TglMulai + "' and CONVERT(varchar,A.Time,112)<='" + TglAkhir + "' ) as B group by B.SalesmanID,B.TokoCode, " +
            "CONVERT(varchar,B.Time,112)) as C group by C.SalesmanID ) as D "+
            "Group By D.SalesmanID,D.Time "+
            ")src "+
            "pivot "+
            "( "+
            "  sum(Jumlah) "+
            "  for Time in ([<9.15], [>9.15]) "+
            ") piv");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObject22(sqlDataReader));
                }
            }
            else
            { arrDataFor.Add(new DataFor()); }
            return arrDataFor;
        }
        public ArrayList RetrieveBy_ListStokToko(string query1)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.CreatedTime,C.SalesmanName,D.TokoCode+' '+D.TokoName as TokoName,A.Merk,A.Qty " +
            "from Stok_Toko as A,Salesman as B,GRCboard.dbo.Salesman as C,GRCboard.dbo.Toko as D " +
            "where A.SalesID=B.id and B.SalesmanID=C.ID and A.KodeToko=D.TokoCode "+query1+" order by CreatedTime");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObject23(sqlDataReader));
                }
            }
            else
            { arrDataFor.Add(new DataFor()); }
            return arrDataFor;
        }
        public ArrayList RetrieveBy_ListRptPerth(int SalesmanID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Rank,Point,TokoCode, "+
            "(  "+
            " case when (select TokoID from GRCboard.dbo.Lomba_DtToko where TokoId=A.TokoID and TokoCode=A.TokoCode)!=0  " +
            " then (select TokoName from GRCboard.dbo.Toko where TokoId=A.TokoID and TokoCode=A.TokoCode and RowStatus>=0)  " +
            " else  "+
            "  case when (select SubDistID from GRCboard.dbo.Lomba_DtSubDist where SubDistID=A.TokoID and SubDistCode=A.TokoCode)!=0  " +
            "  then (select SubDistName from GRCboard.dbo.Lomba_DtSubDist where SubDistID=A.TokoID and SubDistCode=A.TokoCode)  " +
            "  else  "+
            "  ''  "+
            "  End  "+
            " End  "+
            ") as TokoName,PostingDate " +
            "from GRCboard.dbo.Lomba_DtPostingRankingToko as A where  " +
            "convert(varchar,A.PostingDate,112)=convert(varchar,(select top 1 PostingDate from  "+
            "GRCboard.dbo.Lomba_DtPostingRankingToko order by PostingDate desc),112)  " +
            "and A.TokoCode in (select TokoCode from TokoMapToPerth where salesmanId=" + SalesmanID + ") and A.Point>=25 " +
            "Order By A.Rank");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObject24(sqlDataReader));
                }
            }
            else
            { arrDataFor.Add(new DataFor()); }
            return arrDataFor;
        }
        public DataFor RetrieveUserSales(int ID,string Keygen)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from Salesman where ID='" + ID + "' and KeyGenerateAccess='" + Keygen + "'");
            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject25(sqlDataReader);
                }
            }
            return new DataFor();
        }
        public ArrayList RetrieveBy_ListReportOrder(string tgl1, string tgl2)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT Z.SalesID,Z.TokoCode,Z.TokoName,Z.ItemsName,Z.ItemsIdCpd,Z.ItemsIsPaket,Z.ItemsIsQuota,Z.ItemsQty, "+
	        "Convert(datetime,Z.Tanggal,112) as Tanggal,Z.Status,Z.OpNoCpd,Z.FlagBundleOP,Z.SumBundleOP,Z.ItemsSatuan,Z.SalesIDDreams,Z.Distributor,Z.SalesmanName from ( "+
	        "select (select SalesmanID from Salesman where ID=A.SalesID) as SalesID,A.TokoCode,(select TokoName from GRCboard.dbo.Toko where TokoCode=A.TokoCode and A.RowStatus=0 and A.Status=0) as TokoName, "+
	        "A.ItemsName,A.ItemsIdCpd,A.ItemsIsPaket,A.ItemsIsQuota,A.ItemsQty,Convert(varchar,A.TanggalUser,112) as Tanggal, "+
	        "case when A.Status='' then 'Server CPD' else A.Status end Status,A.OpNoCpd,A.FlagBundleOP,A.SumBundleOP, "+
            " case when A.ItemsIsPaket=1 then 'Paket' else (select UOMCODE from GRCboard.dbo.UOM where ID=(select UOMID from GRCboard.dbo.Items where ID=A.ItemsIdCpd)) end ItemsSatuan, A.SalesID as SalesIDDreams, " +
	        "(select DistributorName from GRCboard.dbo.Distributor where ID in(select UnitID from GRCboard.dbo.salesman where UnitSalesman=1 and ID=(select SalesmanID from Salesman where ID=A.SalesID))) as Distributor, "+
	        "(select SalesmanName from GRCboard.dbo.salesman where UnitSalesman=1 and ID=(select SalesmanID from Salesman where ID=A.SalesID)) as SalesmanName "+
	        "from Toko_OP as A,Salesman as B where A.SalesID=B.ID "+
	        "and A.RowStatus=0 and B.RowStatus=0 and (A.FlagBundleOP IS NOT NULL or A.FlagBundleOP!='') and (A.SumBundleOP IS NOT NULL or A.SumBundleOP>0) "+
            "and convert(varchar,A.TanggalUser,112)>='" + tgl1 + "' and convert(varchar,A.TanggalUser,112)<='" + tgl2 + "' " +
	        //"---- CekBundlingOpSudahLengkapAtauBelum "+
	        "and	A.SumBundleOP=	"+
	        "( "+
			    "select COUNT(A2.FlagBundleOP) as Jumlah from ( "+
			    "SELECT A1.SalesID,A1.TokoCode,A1.TokoName,A1.ItemsName,A1.ItemsIdCpd,A1.ItemsIsPaket,A1.ItemsIsQuota,A1.ItemsQty, "+
			    "Convert(datetime,A1.Tanggal,112) as Tanggal,A1.Status,A1.OpNoCpd,A1.FlagBundleOP,A1.SumBundleOP,A1.ItemsSatuan,A1.SalesIDDreams from ( "+
			    "select (select SalesmanID from Salesman where ID=B1.SalesID) as SalesID,B1.TokoCode,(select TokoName from GRCboard.dbo.Toko where TokoCode=B1.TokoCode and RowStatus=0 and Status=0) as TokoName, "+
                "B1.ItemsName,B1.ItemsIdCpd,B1.ItemsIsPaket,B1.ItemsIsQuota,B1.ItemsQty,Convert(varchar,B1.TanggalUser,112) as Tanggal, " +
			    "case when B1.Status='' then 'Server CPD' else B1.Status end Status,B1.OpNoCpd,B1.FlagBundleOP,B1.SumBundleOP, "+
			    "(select UOMCODE from GRCboard.dbo.UOM where ID=(select UOMID from GRCboard.dbo.Items where ID=B1.ItemsIdCpd)) as ItemsSatuan, B1.SalesID as SalesIDDreams "+
			    "from Toko_OP as B1,Salesman as B2 where B1.SalesID=B2.ID "+
			    "and B1.RowStatus=0 and B2.RowStatus=0 and (B1.FlagBundleOP IS NOT NULL or B1.FlagBundleOP!='') and (B1.SumBundleOP IS NOT NULL or B1.SumBundleOP>0) "+
			    "and B1.FlagBundleOP=A.FlagBundleOP "+
                "and convert(varchar,B1.TanggalUser,112)>='" + tgl1 + "' and convert(varchar,B1.TanggalUser,112)<='" + tgl2 + "' " +
			    ") as A1 Group by A1.SalesID,A1.TokoCode,A1.TokoName,A1.ItemsName,A1.ItemsIdCpd,A1.ItemsIsPaket,A1.ItemsIsQuota,A1.ItemsQty,A1.Tanggal, "+
			    "A1.Status,A1.OpNoCpd,A1.FlagBundleOP,A1.SumBundleOP,A1.ItemsSatuan,A1.SalesIDDreams) as A2 Group By A2.FlagBundleOP "+
			    ") "+
			//"---- CekBundlingOpSudahLengkapAtauBelum "+
			") as Z Group by Z.SalesID,Z.TokoCode,Z.TokoName,Z.ItemsName,Z.ItemsIdCpd,Z.ItemsIsPaket,Z.ItemsIsQuota,Z.ItemsQty,Z.Tanggal, "+
			"Z.Status,Z.OpNoCpd,Z.FlagBundleOP,Z.SumBundleOP,Z.ItemsSatuan,Z.SalesIDDreams,Z.Distributor,Z.SalesmanName "+
            "order by Z.Distributor,Z.SalesmanName,Z.TokoName,Z.ItemsName");

            strError = dataAccess.Error;
            arrDataFor = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDataFor.Add(GenerateObject26(sqlDataReader));
                }
            }
            else
                arrDataFor.Add(new DataFor());
            return arrDataFor;
        }

        public string ViewStokPabrikperOPid(int opid, string tglSche)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from View_StokPabrikPerOPid("+opid+",'"+ tglSche + "')");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return sqlDataReader["Description"].ToString();
                }
            }

            return string.Empty;
        }
        public int Cek_StokPabrikPerItemID(int itemid, string tglSche)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from Cek_StokPabrikPerItemID("+itemid+",'"+ tglSche + "')");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return int.Parse(sqlDataReader["Saldo"].ToString());
                }
            }

            return 0;
        }
        public int CekRecordStokPabrik(string tglSche)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select COUNT(ID) as Jml from StokPlantByDay where CONVERT(varchar,TglStok,112)='" + tglSche + "'");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return int.Parse(sqlDataReader["Jml"].ToString());
                }
            }

            return 0;
        }


        public DataFor GenerateObjectIdBroadcast(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            objDataFor.IdBroadcast = Convert.ToString(sqlDataReader["IdBroadcast"]);
            return objDataFor;
        }
        public DataFor GenerateObjectProvinsi(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            objDataFor.PropinsiID = Convert.ToInt32(sqlDataReader["ID"]);
            objDataFor.Propinsi = Convert.ToString(sqlDataReader["NamaPropinsi"]);
            return objDataFor;
        }
        public DataFor GenerateObjectKabKota(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            objDataFor.KabKotaID = Convert.ToInt32(sqlDataReader["ID"]);
            objDataFor.NamaKabKota = Convert.ToString(sqlDataReader["NamaKabKota"]);
            return objDataFor;
        }
        public DataFor GenerateObject(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            objDataFor.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objDataFor.UserName = Convert.ToString(sqlDataReader["UserName"]);
            objDataFor.Password = Convert.ToString(sqlDataReader["Password"]);
            objDataFor.Type = Convert.ToString(sqlDataReader["Type"]);
            objDataFor.DistID = Convert.ToInt32(sqlDataReader["DistID"]);
            objDataFor.DistIDStr = Convert.ToString(sqlDataReader["DistIDStr"]);
            objDataFor.DepoID = Convert.ToString(sqlDataReader["DepoID"]);
            objDataFor.SubDistID = Convert.ToInt32(sqlDataReader["SubDistAccess"]);
            return objDataFor;
        }
        public DataFor GenerateObject1(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            objDataFor.AvailbleReg = Convert.ToInt32(sqlDataReader["AvailbleReg"]);
            objDataFor.CountReg = Convert.ToInt32(sqlDataReader["CountReg"]);
            objDataFor.SalesmanName = Convert.ToString(sqlDataReader["SalesmanName"]);
            return objDataFor;
        }
        public DataFor GenerateObject1A(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            objDataFor.CountReg = Convert.ToInt32(sqlDataReader["LastCountRegSurveyor"]);
            return objDataFor;
        }
        public DataFor GenerateObject1B(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            objDataFor.AvailbleReg = Convert.ToInt32(sqlDataReader["AvailbleReg"]);
            return objDataFor;
        }
        public DataFor GenerateObject2(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            objDataFor.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objDataFor.Row = Convert.ToInt32(sqlDataReader["Row"]);
            objDataFor.SalesmanName = Convert.ToString(sqlDataReader["SalesmanName"]);
            objDataFor.UnitSales = Convert.ToString(sqlDataReader["UnitSales"]);
            objDataFor.Unit = Convert.ToString(sqlDataReader["Unit"]);
            objDataFor.NomorHp = Convert.ToString(sqlDataReader["NomorHp"]);
            objDataFor.UsernameAndro = Convert.ToString(sqlDataReader["UsernameAndro"]);
            objDataFor.Photo = Convert.ToString(sqlDataReader["Photo"]);
            objDataFor.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            if (objDataFor.RowStatus < 0) { objDataFor.Status = "Non Aktif"; }
            else if (objDataFor.RowStatus >= 0) { objDataFor.Status = "Aktif"; }
            objDataFor.Type = Convert.ToString(sqlDataReader["TypeApp"]);
            objDataFor.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objDataFor.Area = Convert.ToString(sqlDataReader["Area"]);
            return objDataFor;
        }
        public DataFor GenerateObject2A(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            objDataFor.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objDataFor.Row = Convert.ToInt32(sqlDataReader["Row"]);
            objDataFor.SurveyorName = Convert.ToString(sqlDataReader["SurveyorName"]);
            objDataFor.NomorHp = Convert.ToString(sqlDataReader["NomorHp"]);
            objDataFor.UsernameAndro = Convert.ToString(sqlDataReader["UsernameAndro"]);
            objDataFor.Photo = Convert.ToString(sqlDataReader["Photo"]);
            objDataFor.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            if (objDataFor.RowStatus < 0) { objDataFor.Status = "Non Aktif"; }
            else if (objDataFor.RowStatus >= 0) { objDataFor.Status = "Aktif"; }
            objDataFor.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            return objDataFor;
        }
        public DataFor GenerateObject3(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            objDataFor.Page = Convert.ToInt32(sqlDataReader["Page"]);
            return objDataFor;
        }
        public DataFor GenerateObject4(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            objDataFor.NomorHp = Convert.ToString(sqlDataReader["NomorHp"]);
            objDataFor.UsernameAndro = Convert.ToString(sqlDataReader["UsernameAndro"]);
            objDataFor.PasswordAndro = Convert.ToString(sqlDataReader["PasswordAndro"]);
            return objDataFor;
        }
        public DataFor GenerateObjectSalesKab(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            objDataFor.SalesmanName = Convert.ToString(sqlDataReader["SalesmanName"]);
            objDataFor.NamaKabKota = Convert.ToString(sqlDataReader["NamaKabKota"]);
            return objDataFor;
        }
        public DataFor GenerateObject5(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            //objDataFor.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objDataFor.Row = Convert.ToInt32(sqlDataReader["Row"]);
            objDataFor.SalesmanName = Convert.ToString(sqlDataReader["SalesmanName"]);
            objDataFor.AlamatToko = Convert.ToString(sqlDataReader["AlamatToko"]);
            //objDataFor.Cord_LO = Convert.ToString(sqlDataReader["Cord_LO"]);
            objDataFor.Cord_LA_Toko = Convert.ToString(sqlDataReader["Cord_LA_Toko"]);
            objDataFor.Cord_LO_Toko = Convert.ToString(sqlDataReader["Cord_LO_Toko"]);
            objDataFor.TokoCode = Convert.ToString(sqlDataReader["TokoCode"]);
            objDataFor.CreatedTime = Convert.ToDateTime(sqlDataReader["UserTime"]);
            objDataFor.TokoName = Convert.ToString(sqlDataReader["TokoName"]);
            if (Convert.ToString(sqlDataReader["TimeSum"]) != "00:00")
            {objDataFor.TimeDate = Convert.ToString(sqlDataReader["TimeSum"]);}
            ////objDataFor.Text = "<iframe ID=\"ifrm\" width=\"400\" height=\"200\" frameborder=\"0\" scrolling=\"no\" marginheight=\"0\" marginwidth=\"0\" src=\"http://maps.google.com/maps?q=" + objDataFor.Cord_LA + "%2C" + objDataFor.Cord_LO + "&ie=UTF8&t=&z=17&iwloc=B&output=embed\"></iframe>";
            ////objDataFor.Text = "<iframe width=\"400\" height=\"200\" frameborder=\"0\" style=\"border:0\" src=\"https://www.google.com/maps/embed/v1/place?key=AIzaSyAcxBKCCNlgVzLdXb8EGZAdOI-9gtUacKU&q=" + objDataFor.Cord_LA + "," + objDataFor.Cord_LO + "&zoom=17\" allowfullscreen></iframe>";
            //objDataFor.Text = "<img width=\"400\" height=\"200\" src=\"http://maps.google.com/maps/api/staticmap?center=" + objDataFor.Cord_LA + "," + objDataFor.Cord_LO + "&zoom=17&size=400x200&maptype=roadmap " +
            //"&markers=color:blue|label:S|" + objDataFor.Cord_LA + "," + objDataFor.Cord_LO + "&markers=color:green|label:T|" + objDataFor.Cord_LA_Toko + "," + objDataFor.Cord_LO_Toko + " " +
            //"&sensor=false&key=AIzaSyAcxBKCCNlgVzLdXb8EGZAdOI-9gtUacKU\" />";
            objDataFor.Kabupaten = Convert.ToString(sqlDataReader["Temp_KabKotaUpdate"]);
            return objDataFor;
        }
        public DataFor GenerateObject6(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            //objDataFor.ID = Convert.ToInt32(sqlDataReader["ID"]);
            //objDataFor.SalesmanID = Convert.ToInt32(sqlDataReader["SalesmanID"]);
            //objDataFor.Row = Convert.ToInt32(sqlDataReader["Row"]);
            objDataFor.NamaToko = Convert.ToString(sqlDataReader["NamaToko"]);
            objDataFor.AlamatToko = Convert.ToString(sqlDataReader["AlamatToko"]);
            //objDataFor.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objDataFor.SalesmanName = Convert.ToString(sqlDataReader["SalesName"]);
            objDataFor.DistributorName = Convert.ToString(sqlDataReader["Distributor"]);
            objDataFor.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            //objDataFor.Location = Convert.ToString(sqlDataReader["Cord_LA"] + "," + sqlDataReader["Cord_LO"]);
            objDataFor.Cord_LO = Convert.ToString(sqlDataReader["Longitude"]);
            objDataFor.Cord_LA = Convert.ToString(sqlDataReader["Latitude"]);
            return objDataFor;
        }
        public DataFor GenerateObject7(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            objDataFor.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objDataFor.TokoCode = Convert.ToString(sqlDataReader["TokoCode"]);
            objDataFor.TokoName = Convert.ToString(sqlDataReader["TokoName"]);
            objDataFor.City = Convert.ToString(sqlDataReader["City"]);
            objDataFor.Kabupaten = Convert.ToString(sqlDataReader["Kabupaten"]);
            return objDataFor;
        }
        public DataFor GenerateObject8(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            objDataFor.MessageText = Convert.ToString(sqlDataReader["Message"]);
            objDataFor.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            return objDataFor;
        }
        public DataFor GenerateObject9(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            objDataFor.SalesmanName = Convert.ToString(sqlDataReader["SalesmanName"]);
            objDataFor.Cord_LA = Convert.ToString(sqlDataReader["Cord_LA"]);
            objDataFor.Cord_LO = Convert.ToString(sqlDataReader["Cord_LO"]);
            objDataFor.Bounce = Convert.ToInt32(sqlDataReader["Bounce"]);
            return objDataFor;
        }
        public DataFor GenerateObject9Trace(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            objDataFor.Keterangan = Convert.ToString(sqlDataReader["Keterangan"]);
            objDataFor.Cord_LA = Convert.ToString(sqlDataReader["Latitude"]);
            objDataFor.Cord_LO = Convert.ToString(sqlDataReader["Longitude"]);
            objDataFor.Tanggal = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            return objDataFor;
        }
        public DataFor GenerateObject10(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            objDataFor.Jumlah = Convert.ToInt32(sqlDataReader["Jumlah"]);
            objDataFor.Tanggal = Convert.ToDateTime(sqlDataReader["Tanggal"]);
            return objDataFor;
        }
        public DataFor GenerateObject11(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            objDataFor.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objDataFor.TokoCode = Convert.ToString(sqlDataReader["TokoCode"]);
            objDataFor.KabKotaID = Convert.ToInt32(sqlDataReader["Indonesia_KabKotaID"]);
            objDataFor.Cord_LA = Convert.ToString(sqlDataReader["Latitude"]);
            objDataFor.Cord_LO = Convert.ToString(sqlDataReader["Longitude"]);
            return objDataFor;
        }
        public DataFor GenerateObject11A(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            objDataFor.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objDataFor.Cord_LA = Convert.ToString(sqlDataReader["Cord_LA_Toko"]);
            objDataFor.Cord_LO = Convert.ToString(sqlDataReader["Cord_LO_Toko"]);
            return objDataFor;
        }
        public DataFor GenerateObject12a(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            objDataFor.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objDataFor.DistributorName = Convert.ToString(sqlDataReader["DistributorName"]);
            objDataFor.DistributorCode = Convert.ToString(sqlDataReader["DistributorCode"]);
            return objDataFor;
        }
        public DataFor GenerateObject12b(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            objDataFor.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objDataFor.DistributorCode = Convert.ToString(sqlDataReader["DistributorCode"]);
            objDataFor.SalesmanName = Convert.ToString(sqlDataReader["SalesmanName"]);
            return objDataFor;
        }
        public DataFor GenerateObject13(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            objDataFor.TokoCode = Convert.ToString(sqlDataReader["TokoCode"]);
            objDataFor.TokoName = Convert.ToString(sqlDataReader["TokoName"]);
            objDataFor.DistributorName = Convert.ToString(sqlDataReader["DistBerikut"]);
            objDataFor.Omset = Convert.ToDecimal(sqlDataReader["Omset"]);
            objDataFor.Kunjungan = Convert.ToInt32(sqlDataReader["Kunjungan"]);
            objDataFor.Keterangan = Convert.ToString(sqlDataReader["Keterangan"]);
            return objDataFor;
        }
        public DataFor GenerateObject14(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            objDataFor.TokoCode = Convert.ToString(sqlDataReader["TokoCode"]);
            objDataFor.TokoName = Convert.ToString(sqlDataReader["TokoName"]);
            objDataFor.DistributorName = Convert.ToString(sqlDataReader["DistBerikut"]);
            objDataFor.Omset = Convert.ToDecimal(sqlDataReader["Omset"]);
            objDataFor.Kunjungan = Convert.ToInt32(sqlDataReader["Kunjungan"]);
            return objDataFor;
        }
        public DataFor GenerateObject15(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            objDataFor.Distributor = Convert.ToString(sqlDataReader["Distributor"]);
            objDataFor.SalesmanName = Convert.ToString(sqlDataReader["SalesmanName"]);
            objDataFor.Jumlah = Convert.ToInt32(sqlDataReader["JmlKunjungan"]);
            return objDataFor;
        }
        public DataFor GenerateObject16(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            objDataFor.SalesmanName = Convert.ToString(sqlDataReader["Name"]);
            objDataFor.Jumlah = Convert.ToInt32(sqlDataReader["JumlahToko"]);
            objDataFor.Tanggal = Convert.ToDateTime(sqlDataReader["TglKunjungan"]);
            return objDataFor;
        }
        public DataFor GenerateObject17(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            objDataFor.Distributor = Convert.ToString(sqlDataReader["Distributor"]);
            objDataFor.JmlToko = Convert.ToInt32(sqlDataReader["JmlToko"]);
            objDataFor.JmlTokoDiKunjungi = Convert.ToInt32(sqlDataReader["JmlTokoDiKunjungi"]);
            objDataFor.JmlTokoDiKunjungi2x = Convert.ToInt32(sqlDataReader["JmlTokoDiKunjungi2x"]);
            return objDataFor;
        }
        public DataFor GenerateObject18(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            objDataFor.Tanggal = Convert.ToDateTime(sqlDataReader["Date1"]);
            objDataFor.TokoCode = Convert.ToString(sqlDataReader["TokoCodeKunj"]);
            objDataFor.TokoName = Convert.ToString(sqlDataReader["TokoName"]);
            objDataFor.Time = Convert.ToString(sqlDataReader["Time"]);
            objDataFor.Keterangan = Convert.ToString(sqlDataReader["Keterangan"]);
            return objDataFor;
        }
        public DataFor GenerateObject19(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            objDataFor.Images = Convert.ToString(sqlDataReader["Img"]);
            return objDataFor;
        }
        public DataFor GenerateObject20(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            objDataFor.MessageText = Convert.ToString(sqlDataReader["Message"]);
            objDataFor.Tanggal = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            return objDataFor;
        }
        public DataFor GenerateObject21(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            objDataFor.SalesmanName = Convert.ToString(sqlDataReader["SalesmanName"]);
            objDataFor.DistributorName = Convert.ToString(sqlDataReader["DistributorName"]);
            objDataFor.Kabupaten = Convert.ToString(sqlDataReader["Kabupaten"]);
            objDataFor.Tanggal = Convert.ToDateTime(sqlDataReader["Tanggal"]);
            return objDataFor;
        }
        public DataFor GenerateObject22(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            objDataFor.SalesmanName = Convert.ToString(sqlDataReader["Name"]);
            objDataFor.Ket1 = Convert.ToString(sqlDataReader["<9.15"]);
            objDataFor.Ket2 = Convert.ToString(sqlDataReader[">9.15"]);

            return objDataFor;
        }
        public DataFor GenerateObject23(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            objDataFor.Tanggal = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objDataFor.SalesmanName = Convert.ToString(sqlDataReader["SalesmanName"]);
            objDataFor.TokoName = Convert.ToString(sqlDataReader["TokoName"]);
            objDataFor.Merk = Convert.ToString(sqlDataReader["Merk"]);
            objDataFor.JumlahString = Convert.ToString(sqlDataReader["Qty"]);
            return objDataFor;
        }
        public DataFor GenerateObject24(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            objDataFor.Tanggal = Convert.ToDateTime(sqlDataReader["PostingDate"]);
            objDataFor.Rank = Convert.ToInt32(sqlDataReader["Rank"]);
            objDataFor.PointDecimal = Convert.ToDecimal(sqlDataReader["Point"]);
            objDataFor.TokoCode = Convert.ToString(sqlDataReader["TokoCode"]);
            objDataFor.TokoName = Convert.ToString(sqlDataReader["TokoName"]);
            return objDataFor;
        }
        public DataFor GenerateObject25(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            objDataFor.ForSurveyor = Convert.ToInt32(sqlDataReader["ThisSurveyor"]);
            objDataFor.ForGrcHelp = Convert.ToInt32(sqlDataReader["ThisProject"]);
            return objDataFor;
        }
        public DataFor GenerateObject26(SqlDataReader sqlDataReader)
        {
            objDataFor = new DataFor();
            objDataFor.Distributor = Convert.ToString(sqlDataReader["Distributor"]);
            objDataFor.SalesmanName = Convert.ToString(sqlDataReader["SalesmanName"]);
            objDataFor.TokoName = Convert.ToString(sqlDataReader["TokoName"]);
            objDataFor.TokoCode = Convert.ToString(sqlDataReader["TokoCode"]);
            objDataFor.ItemsName = Convert.ToString(sqlDataReader["ItemsName"]);
            objDataFor.ItemsQty = Convert.ToString(sqlDataReader["ItemsQty"]);
            objDataFor.ItemsSatuan = Convert.ToString(sqlDataReader["ItemsSatuan"]);
            return objDataFor;
        }
    }
}
