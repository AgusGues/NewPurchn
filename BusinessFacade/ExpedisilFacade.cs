using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;
using System.Web;

namespace BusinessFacade
{
    public class ExpedisiFacade : AbstractTransactionFacade
    {
        private Expedisi objExpedisi = new Expedisi();
        private ArrayList arrExpedisi;
        private List<SqlParameter> sqlListParam;

        public ExpedisiFacade(object objDomain)
            : base(objDomain)
        {
            objExpedisi = (Expedisi)objDomain;
        }

        public ExpedisiFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ExpedisiName", objExpedisi.ExpedisiName));
                sqlListParam.Add(new SqlParameter("@Address", objExpedisi.Address));
                sqlListParam.Add(new SqlParameter("@Telp", objExpedisi.Telp));
                sqlListParam.Add(new SqlParameter("@Handphone", objExpedisi.Handphone));
                sqlListParam.Add(new SqlParameter("@UPName", objExpedisi.UPName));
                sqlListParam.Add(new SqlParameter("@DepoID", objExpedisi.DepoID));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objExpedisi.CreatedBy));                
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertExpedisi");

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
                sqlListParam.Add(new SqlParameter("@ID", objExpedisi.ID));
                sqlListParam.Add(new SqlParameter("@ExpedisiName", objExpedisi.ExpedisiName));
                sqlListParam.Add(new SqlParameter("@Address", objExpedisi.Address));
                sqlListParam.Add(new SqlParameter("@Telp", objExpedisi.Telp));
                sqlListParam.Add(new SqlParameter("@Handphone", objExpedisi.Handphone));
                sqlListParam.Add(new SqlParameter("@UPName", objExpedisi.UPName));
                sqlListParam.Add(new SqlParameter("@DepoID", objExpedisi.DepoID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objExpedisi.CreatedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateExpedisi");

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
                sqlListParam.Add(new SqlParameter("@ID", objExpedisi.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objExpedisi.LastModifiedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteExpedisi");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from Expedisi where RowStatus = 0");
            strError = dataAccess.Error;
            arrExpedisi = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrExpedisi.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrExpedisi.Add(new Expedisi());

            return arrExpedisi;
        }

        public ArrayList RetrieveByDepo(int depoID)
        {
            string strQuery = "select * from Expedisi where RowStatus = 0 and DepoID = " + depoID + " order by ExpedisiName ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strQuery);
            strError = dataAccess.Error;
            arrExpedisi = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrExpedisi.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrExpedisi.Add(new Expedisi());

            return arrExpedisi;
        }
        public ArrayList RetrieveByDepoPusat()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from Expedisi where RowStatus = 0 and DepoID in (1,7) order by ExpedisiName");
            strError = dataAccess.Error;
            arrExpedisi = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrExpedisi.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrExpedisi.Add(new Expedisi());

            return arrExpedisi;
        }

        public Expedisi RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from Expedisi where RowStatus = 0 and ID = " + Id);
            strError = dataAccess.Error;
            arrExpedisi = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Expedisi();
        }
        public int CekSMS(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select SMS from Expedisi where ID= " + Id);
            strError = dataAccess.Error;
            arrExpedisi = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["SMS"]);
                }
            }

            return -2;
        }
        
        public Expedisi RetrieveByName(string ekspedisiName)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from Expedisi where RowStatus = 0 and ExpedisiName = '" + ekspedisiName + "'");
            strError = dataAccess.Error;
            arrExpedisi = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Expedisi();
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from Expedisi where RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrExpedisi = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrExpedisi.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrExpedisi.Add(new Expedisi());

            return arrExpedisi;
        }


        public Expedisi RetrieveByCompanyId(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from Expedisi where RowStatus = 0 and ID = " + Id);
            strError = dataAccess.Error;
            arrExpedisi = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new Expedisi();
        }
        public ArrayList RetrieveRecapMuatan(string Armada)
        {
            string UnitKerja = ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID.ToString();
            arrExpedisi = new ArrayList();
            string where = string.Empty;
            if (UnitKerja == "1")
            {
                where = (Armada == "0") ? "where A.ID in (5,6,7,8,9,78,79,143,277,348,350,399,410) " : " where A.ID=" + Armada;
            }
            else
            {
                where = (Armada == "0") ? "where A.ID in (130,131,132,148,154,155,167,168,202,203,206,242,243,244,359,360,398,400,414,415,416,417,418,419) " :
                    " where A.ID=" + Armada;
            }

            string strSql = "select A.ID,A.ExpedisiID ,A.Cartype from [sql1.grcboard.com].GRCboard.dbo.ExpedisiDetail A left join " +
                "[sql1.grcboard.com].GRCboard.dbo.Expedisi as B on A.ExpedisiID=B.ID " + where + " Group by A.ID,A.ExpedisiID,A.Cartype ";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSql);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrExpedisi.Add(GenObject(sdr));
                }
            }
            return arrExpedisi;
        }
        public ArrayList RetrieveRecapMuatan1(string rit, string Armada, string tgl)
        {
            string UnitKerja = ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID.ToString();
            arrExpedisi = new ArrayList();
            string strSql = string.Empty;
            string where = string.Empty;
            if (rit != "-1")
                where = " and Rtrim(S.rate)='" + rit + "'";
            strSql = "select distinct ED.id,E.ID expedisiID,case when  rtrim(S.rate)='' then 'Rit = 0' else 'Rit = '+rtrim(S.rate) end Cartype " +
                "from [sql1.grcboard.com].GRCboard.dbo.Schedule S inner join [sql1.grcboard.com].GRCboard.dbo.ExpedisiDetail ED on  " +
                "S.ExpedisiDetailID=ED.ID  inner join [sql1.grcboard.com].GRCboard.dbo.expedisi E on ED.ExpedisiID =E.ID " +
                "where convert(char,scheduledate,112)='" + tgl + "' and S.DepoID=" + UnitKerja + " and E.ID=" + Armada + where + "  order by E.id";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSql);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrExpedisi.Add(GenObject(sdr));
                }
            }
            return arrExpedisi;
        }
        public ArrayList RetrieveAllMuatan(string tglMuat, string plnt, string Armada)
        {
            string strhodb = string.Empty;
            strhodb = " [sql1.grcboard.com].GRCboard.dbo.";
            string kriteria = string.Empty;
            string depo = string.Empty;
            if (plnt == "1")
            {
                if (Armada != "0")
                    kriteria = kriteria + " and ExpedisiDetail.ID=" + Armada;
                if (Armada == "0")
                    kriteria = kriteria + " and ExpedisiDetail.ID in (5,6,7,8,9,78,79,143,277,348,350,399,410) ";
            }
            else
            {
                if (Armada != "0")
                    kriteria = kriteria + " and ExpedisiDetail.ID=" + Armada;
                if (Armada == "0")
                    kriteria = kriteria + " and ExpedisiDetail.ID in (130,131,132,148,154,155,167,168,202,203,206,242,243,244,359,360,398, " +
                "400,414,415,416,417,418,419) ";
            }
            //if (Armada != "0")
            //    kriteria = kriteria + " and ExpedisiDetail.ID=" + Armada;
            //if (Armada == "0")
            //    kriteria = kriteria + " and ExpedisiDetail.ID in (130,131,132,148,154,155,167,168,202,203,206,242,243,244,359,360,398,400,414,415,416,417,418,419) ";
            if (plnt == "1")
                depo = "1";
            if (plnt == "74")
                depo = "7";

            string strSQL = " select ID,scheduleNo,ScheduleDate,CarType,[Description],sum(Qty)Qty from ( " +
                " Select * from ( select ScheduleDetail.ID as ScheduleDetailID,ScheduleDetail.TypeDoc, " +
                " Schedule.ScheduleNo,convert(varchar,Schedule.ScheduleDate,103) as ScheduleDate,Schedule.DepoID,Schedule.Keterangan as KetSchedule," +
                "Schedule.TotalKubikasi, items.Description, left(Expedisi.ExpedisiName,30) as ExpedisiName,left(Expedisi.UPName,20) as UPName, " +
                " Expedisi.Handphone, ExpedisiDetail.CarType,ExpedisiDetail.ID,ScheduleDetail.Qty," +
                " case ScheduleDetail.TypeDoc when 0 then case (select CustomerType from " + strhodb + " OP where ID = ScheduleDetail.DocumentID) when 1 " +
                " then (select left(TokoName,20) as TokoName from " + strhodb + " Toko where ID = (select CustomerId from " +
                strhodb + " op where ID = ScheduleDetail.DocumentID)) " +
                " else (select left(CustomerName,20) as CustomerName from " + strhodb + " Customer where ID = (select CustomerId from " + strhodb +
                " op where ID = ScheduleDetail.DocumentID)) " + " end " + " else " +
                " (select left(DepoName,20) as DepoName from " + strhodb + " Depo where ID = (select ToDepoID from " + strhodb +
                " TransferOrder where ID = ScheduleDetail.DocumentID)) " + " end " + " TempatTujuan, case ScheduleDetail.TypeDoc when 0 " +
                " then case (select CustomerType from " + strhodb + " OP where ID = ScheduleDetail.DocumentID) when 1 " + " then " +
                " case (select SubDistributorID from " + strhodb + " Toko where ID = (select CustomerID from " + strhodb +
                " OP where ID = ScheduleDetail.DocumentID)) when 0 " + " then " +
                " (select DistributorCode from " + strhodb + " Distributor where ID = (select DistributorID from " + strhodb +
                " Toko where ID = (select CustomerID from " + strhodb + " OP where ID = ScheduleDetail.DocumentID)))" + " else  " +
                " (select SubDistributorCode from " + strhodb + " SubDistributor where ID = (select SubDistributorID from " + strhodb +
                " Toko where ID = (select CustomerID from " + strhodb + " OP where ID = ScheduleDetail.DocumentID))) " + " end " + " else " + " '-'  " +
                " end " + " else " + " '-' " + " end Agen, " +
                " case ScheduleDetail.TypeDoc when 0 then (select AlamatLain from " + strhodb + " op where ID = ScheduleDetail.DocumentID) " +
                " else (select Address as Address from " + strhodb + " Depo where ID = (select ToDepoID from " + strhodb + " TransferOrder " +
                " where ID = ScheduleDetail.DocumentID)) end Alamat, " + " case ScheduleDetail.TypeDoc when 0 then " +
                " (select NamaKabupaten from " + strhodb + " Kabupaten where ID = (select KabupatenID from " + strhodb +
                " op where ID = ScheduleDetail.DocumentID)) else " +
                " (select NamaKabupaten from " + strhodb + " Kabupaten where ID = (select KabupatenID from " + strhodb + " Depo where ID = (select ToDepoID from " +
                strhodb + " TransferOrder where ID = ScheduleDetail.DocumentID))) " + " end Kabupaten, " +
                " case ScheduleDetail.TypeDoc when 0 then (select OPNo from " + strhodb + " OP where ID = ScheduleDetail.DocumentID) else " +
                " (select TransferOrderNo from " + strhodb + " TransferOrder where ID = ScheduleDetail.DocumentID) end NoDoc, case ScheduleDetail.TypeDoc when 0 " +
                " then (select convert(varchar,CreatedTime,103) as CreatedTime from " + strhodb + " OP where ID = ScheduleDetail.DocumentID) " +
                " else (select convert(varchar,TransferOrderDate,103) as TransferOrderDate from " + strhodb +
                " TransferOrder where ID = ScheduleDetail.DocumentID) end TglDoc, " +
                " case ScheduleDetail.TypeDoc when 0 then (select Keterangan2 from " + strhodb +
                " OP where ID = ScheduleDetail.DocumentID) else (select Keterangan from " + strhodb +
                " TransferOrder where ID = ScheduleDetail.DocumentID) end Keterangan2, 'Rit : ' + convert	(varchar,Schedule.Rate) as Reat," +
                " case when isNumeric(Schedule.Rate)=0 then 0 else convert(int,Schedule.Rate) end Rit," +
                " case ScheduleDetail.TypeDoc when 0 then (select SalesManName from " + strhodb + " SalesMan where ID = (select top 1 SalesID from " +
                strhodb + " OP where OPNo = ScheduleDetail.DocumentNo and Status>=0))" + " else " + " '-' " +
                " end SalesmanName from " + strhodb + " Schedule," + strhodb + " ScheduleDetail," + strhodb + " Items, " + strhodb + " Expedisi, " +
                strhodb + " ExpedisiDetail where ScheduleDetail.ScheduleID=Schedule.ID and ScheduleDetail.ItemID=Items.ID " +
                " and Schedule.ExpedisiDetailID=ExpedisiDetail.ID and ExpedisiDetail.ExpedisiID=Expedisi.ID and convert(varchar,Schedule.ScheduleDate,112)= '" +
                tglMuat + "' and ScheduleDetail.Status > -1  and Schedule.Status in (1,2) and Schedule.DepoID= '" + depo + "' and Expedisi.ID = '" +
                plnt + "'" + kriteria + ") as A where Description not like'%pot%' and Description not like'%ongk%' )as xx Group by ID,scheduleNo,ScheduleDate,Description,CarType  ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrExpedisi = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrExpedisi.Add(GenObject1(sqlDataReader));
                }
            }
            else
                arrExpedisi.Add(new Expedisi());
            return arrExpedisi;

        }
        public ArrayList RetrieveAllMuatan1(string tglMuat, string plnt, string Armada, string ArmadaID)
        {
            string strhodb = string.Empty;
            strhodb = " [sql1.grcboard.com].GRCboard.dbo.";
            string kriteria = string.Empty;
            string depo = string.Empty;
            if (plnt == "1")
            {
                if (Armada != "0")
                    kriteria = kriteria + " and ExpedisiDetail.ID=" + Armada;
                if (Armada == "0")
                    kriteria = kriteria + " and ExpedisiDetail.ID in (5,6,7,8,9,78,79,143,277,348,350,399,410) ";
            }
            else
            {
                if (Armada != "0")
                    kriteria = kriteria + " and ExpedisiDetail.ID=" + Armada;
                if (Armada == "0")
                    kriteria = kriteria + " and ExpedisiDetail.ID in (130,131,132,148,154,155,167,168,202,203,206,242,243,244,359,360,398, " +
                "400,414,415,416,417,418,419) ";
            }

            depo = ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID.ToString(); ;

            string strSQL = "select ID,scheduleNo,ScheduleDate,CarType,[Description],sum(Qty)Qty " +
                "from (  " +
                " Select * from ( select ScheduleDetail.ID as ScheduleDetailID,ScheduleDetail.TypeDoc,  " +
                " Schedule.ScheduleNo,convert(varchar,Schedule.ScheduleDate,103) as ScheduleDate,Schedule.DepoID,Schedule.Keterangan as KetSchedule,Schedule.TotalKubikasi,  " +
                " items.Description, left(Expedisi.ExpedisiName,30) as ExpedisiName, left(Expedisi.UPName,20) as UPName, Expedisi.Handphone, " +
                " case when rtrim(Schedule.rate)='' then 'Rit : 0' else 'Rit : ' + rtrim(Schedule.rate)  end  CarType, " +
                " ExpedisiDetail.ID, ScheduleDetail.Qty, " +
                " case ScheduleDetail.TypeDoc  " +
                " when 0 then case (select CustomerType from  " + strhodb + "OP where ID = ScheduleDetail.DocumentID)  " +
                " when 1 then (select left(TokoName,20) as TokoName from  " + strhodb + "Toko where ID = (select CustomerId from  " + strhodb + "op  " +
                " where ID = ScheduleDetail.DocumentID))  " +
                " else (select left(CustomerName,20) as CustomerName from  " + strhodb + "Customer where ID =  " +
                " (select CustomerId from  " + strhodb + "op where ID = ScheduleDetail.DocumentID)) end  " +
                " else  " +
                " (select left(DepoName,20) as DepoName from  " + strhodb + "Depo where ID = (select ToDepoID from  " + strhodb + "TransferOrder  " +
                " where ID = ScheduleDetail.DocumentID)) end TempatTujuan,  " +
                " case ScheduleDetail.TypeDoc  " +
                " when 0 then case (select CustomerType from  " + strhodb + "OP where ID = ScheduleDetail.DocumentID)  " +
                " when 1 then case (select SubDistributorID from  " + strhodb + "Toko where ID = (select CustomerID from  " + strhodb + "OP  " +
                " where ID = ScheduleDetail.DocumentID)) when 0 then (select DistributorCode from  " + strhodb + "Distributor where ID =  " +
                " (select DistributorID from  " + strhodb + "Toko where ID = (select CustomerID from  " + strhodb + "OP where ID = ScheduleDetail.DocumentID))) " +
                " else   " +
                " (select SubDistributorCode from  " + strhodb + "SubDistributor where ID = (select SubDistributorID from  " + strhodb + "Toko  " +
                " where ID = (select CustomerID from  " + strhodb + "OP where ID = ScheduleDetail.DocumentID))) end  " +
                " else '-'  end else '-' end Agen,  " +
                " case ScheduleDetail.TypeDoc when 0 then (select AlamatLain from  " + strhodb + "op where ID = ScheduleDetail.DocumentID)  " +
                " else (select Address as Address from  " + strhodb + "Depo where ID = (select ToDepoID from  " + strhodb + "TransferOrder  " +
                " where ID = ScheduleDetail.DocumentID)) end Alamat,  " +
                " case ScheduleDetail.TypeDoc  when 0 then  " +
                " (select NamaKabupaten from  " + strhodb + "Kabupaten where ID = (select KabupatenID from  " + strhodb + "op where ID = ScheduleDetail.DocumentID))  " +
                " else  " +
                " (select NamaKabupaten from  " + strhodb + "Kabupaten where ID = (select KabupatenID from  " + strhodb + "Depo  " +
                " where ID = (select ToDepoID from  " + strhodb + "TransferOrder where ID = ScheduleDetail.DocumentID)))  " +
                " end Kabupaten,  " +
                " case ScheduleDetail.TypeDoc when 0 then (select OPNo from  " + strhodb + "OP where ID = ScheduleDetail.DocumentID) else  " +
                " (select TransferOrderNo from  " + strhodb + "TransferOrder where ID = ScheduleDetail.DocumentID) end NoDoc, case ScheduleDetail.TypeDoc when 0  " +
                " then (select convert(varchar,CreatedTime,103) as CreatedTime from  " + strhodb + "OP where ID = ScheduleDetail.DocumentID)  " +
                " else (select convert(varchar,TransferOrderDate,103) as TransferOrderDate from  " + strhodb + "TransferOrder where ID = ScheduleDetail.DocumentID) end TglDoc,  " +
                " case ScheduleDetail.TypeDoc when 0 then (select Keterangan2 from  " + strhodb + "OP where ID = ScheduleDetail.DocumentID) else  " +
                " (select Keterangan from  " + strhodb + "TransferOrder where ID = ScheduleDetail.DocumentID) end Keterangan2, 'Rit : ' + convert	(varchar,Schedule.Rate) as Reat, " +
                " case when isNumeric(Schedule.Rate)=0 then 0 else convert(int,Schedule.Rate) end Rit, " +
                " case ScheduleDetail.TypeDoc when 0 then (select SalesManName from  " + strhodb + "SalesMan where ID =  " +
                " (select top 1 SalesID from  " + strhodb + "OP where OPNo = ScheduleDetail.DocumentNo and Status>=0)) " +
                " else  '-'  end SalesmanName from  " + strhodb + "Schedule, " + strhodb + "ScheduleDetail, " + strhodb + "Items,   " +
                " " + strhodb + "Expedisi,  " + strhodb + "ExpedisiDetail where ScheduleDetail.ScheduleID=Schedule.ID and ScheduleDetail.ItemID=Items.ID  " +
                " and Schedule.ExpedisiDetailID=ExpedisiDetail.ID and ExpedisiDetail.ExpedisiID=Expedisi.ID and convert(varchar,Schedule.ScheduleDate,112)= '" + tglMuat + "' and ScheduleDetail.Status > -1   " +
                " and Schedule.Status in (1,2) and Schedule.DepoID=" + depo + " and Expedisi.ID = " + ArmadaID +
                " ) as A where Description not like'%pot%' and Description not like'%ongk%' )as xx where " + Armada +
                " Group by ID,scheduleNo,ScheduleDate,Description,CarType  order by cartype";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrExpedisi = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrExpedisi.Add(GenObject1(sqlDataReader));
                }
            }
            else
                arrExpedisi.Add(new Expedisi());
            return arrExpedisi;

        }
        private Expedisi GenObject(SqlDataReader sdr)
        {
            Expedisi b = new Expedisi();
            b.ID = int.Parse(sdr["ID"].ToString());
            b.CarType = sdr["CarType"].ToString();
            return b;
        }
        public Expedisi GenerateObject2(SqlDataReader sqlDataReader)
        {
            objExpedisi = new Expedisi();
            objExpedisi.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objExpedisi.ExpedisiName = sqlDataReader["ExpedisiName"].ToString();
            objExpedisi.Address = sqlDataReader["Address"].ToString();
            objExpedisi.Telp = sqlDataReader["Telp"].ToString();
            objExpedisi.Handphone = sqlDataReader["Handphone"].ToString();
            objExpedisi.UPName = sqlDataReader["UPName"].ToString();
            objExpedisi.DepoID = Convert.ToInt32(sqlDataReader["DepoID"]);
            objExpedisi.CompanyID = Convert.ToInt32(sqlDataReader["CompanyID"]);
            objExpedisi.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objExpedisi.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objExpedisi.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objExpedisi.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objExpedisi.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objExpedisi;

        }

        public Expedisi GenerateObject(SqlDataReader sqlDataReader)
        {
            objExpedisi = new Expedisi();
            objExpedisi.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objExpedisi.ExpedisiName = sqlDataReader["ExpedisiName"].ToString();
            objExpedisi.Address = sqlDataReader["Address"].ToString();
            objExpedisi.Telp = sqlDataReader["Telp"].ToString();
            objExpedisi.Handphone = sqlDataReader["Handphone"].ToString();
            objExpedisi.UPName = sqlDataReader["UPName"].ToString();
            objExpedisi.DepoID = Convert.ToInt32(sqlDataReader["DepoID"]);
            objExpedisi.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objExpedisi.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objExpedisi.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objExpedisi.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objExpedisi.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objExpedisi;

        }
        public Expedisi GenObject1(SqlDataReader sqlDataReader)
        {
            objExpedisi = new Expedisi();
            objExpedisi.ID = Convert.ToInt32(sqlDataReader["ID"]);
            //objExpedisi.IDx = Convert.ToInt32(sqlDataReader["IDx"]);
            objExpedisi.ScheduleDate = Convert.ToDateTime(sqlDataReader["ScheduleDate"]);
            objExpedisi.Description = sqlDataReader["Description"].ToString();
            objExpedisi.Qty = Convert.ToDecimal(sqlDataReader["Qty"]);
            objExpedisi.CarType = sqlDataReader["CarType"].ToString();
            objExpedisi.ExScheduleNo = sqlDataReader["ScheduleNo"].ToString();
            return objExpedisi;
        }
    }
}

