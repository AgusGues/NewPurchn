using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Net;

namespace BusinessFacade
{
    public class WebserviceFacade : AbstractFacade
    {
        public override int Insert(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override int Update(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override int Delete(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override System.Collections.ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }
        

        public DataSet GetSuratJalan(string SJNo)
        {
            System.Data.SqlClient.SqlConnection sqlConnection1 = new System.Data.SqlClient.SqlConnection();
            //sqlConnection1.ConnectionString = "Initial Catalog=GRCBoard;Data Source=AITI;User ID=sa;Password=280225";
            sqlConnection1.ConnectionString = Global.ConnectionString();
            string select = "select A.*,B.*,C.OPNo,C.TypeDistSub,C.DistSubID, " +
                "case C.CustomerType when 1 then case C.TypeDistSub when 1 then (select DistributorCode from Distributor where ID = C.DistSubID) " +
                "else (select SubDistributorCode from SubDistributor where ID = C.DistSubID) end " +
                "else (select CustomerCode from Customer where ID = C.CustomerId) end DistSubCustCode," +
                "case C.CustomerType when 1 then case C.TypeDistSub when 1 then (select DistributorName from Distributor where ID = C.DistSubID) " +
                "else (select SubDistributorName from SubDistributor where ID = C.DistSubID) end " +
                "else (select CustomerName from Customer where ID = C.CustomerId) " +
                "end DistSubCustName from SuratJalan as A, SuratJalanDetail as B,OP as C where A.ID=B.SuratJalanID and A.Status=1 and A.OPID=C.ID and C.Status>-1 and A.SuratJalanNo='" + SJNo + "'";
            SqlDataAdapter da = new SqlDataAdapter(select, sqlConnection1);
            DataSet ds = new DataSet();
            da.Fill(ds, "BankOut1");
            return (ds);
        }

        public DataTable sjTO_CetakSJto(string SJNO)
        {
            System.Data.SqlClient.SqlConnection sqlConnection1 = new System.Data.SqlClient.SqlConnection();
            sqlConnection1.ConnectionString = "Initial Catalog=GRCBoard;Data Source=sql1.grcboard.com;User ID=sa;Password=Passw0rd";
            //sqlConnection1.ConnectionString = Global.ConnectionString();
            string strSQL = "select A.ID,A.SuratJalanNo as SuratJalanNo,A.TransferOrderID,C.TransferOrderNo,A.ScheduleID," +
            "B.ScheduleNo,A.PoliceCarNo,A.DriverName,A.Status,A.Cetak,A.ReceiveDate," +
            "A.PostingShipmentDate,A.PostingReceiveDate,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime " +
            ", case when D.ExpedisiID > 0 then (select ExpedisiName from Expedisi where ID = D.ExpedisiID) end NamaExpedisi," +
            "FromDepoName as InitialToko,ToDepoName as NamaToko,ToDepoAddress as Alamat,TransferOrderDate as CreatedTime," +
            "A.CreatedTime as ScheduleDate, TransferOrderNo as NoTO,FromDepoName as AtasOrder,C.Keterangan," +
            "case when B.DepoID=1 then 'FAJAR RAHARJO' when B.DepoID=7 then 'ADI SUFIANDI' else '' end KepalaGudang," +
            "case when B.DepoID=1 then 'NASSARUDIN' when B.DepoID=7 then 'M NUR CHABIB' else '' end StaffGudang,Description, E.Qty,UOMCode, " +
            "(select count(ID) as jumID from SuratJalanTO where TransferOrderID = A.TransferOrderID and Status > -1)  jumOP,B.depoID,F.ID Itemid,F.itemCode,F.[Description] Itemname  " +
            "from SuratJalanTO as A,Schedule as B,TransferOrder as C, ExpedisiDetail as D , SuratJalanDetailTO as E, Items as F,UOM as G " +
            "where A.ScheduleID = B.ID and A.TransferOrderID = C.ID and  B.ExpedisiDetailID = D.ID and A.ID=E.SuratJalanTOID " +
            "and E.ItemID=F.ID and F.UOMID=G.ID and A.SuratJalanNo='" + SJNO + "'";
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlConnection1);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dt.TableName = "sj_CetakSJto";

            sqlConnection1.Close();
            return (dt);
        }
        //[WebMethod]
        public DataSet GetItems()
        {
            System.Data.SqlClient.SqlConnection sqlConnection1 = new System.Data.SqlClient.SqlConnection();
            //sqlConnection1.ConnectionString = "Initial Catalog=GRCBoard;Data Source=AITI;User ID=sa;Password=280225";
            sqlConnection1.ConnectionString = Global.ConnectionString();
            string select = "select * from items where rowstatus>-1 and panjang*lebar>0 order by description";
            SqlDataAdapter da = new SqlDataAdapter(select, sqlConnection1);
            DataSet ds = new DataSet();
            da.Fill(ds, "items");
            return (ds);
        }

        public DataSet GetItemsByName(string itemname)
        {
            System.Data.SqlClient.SqlConnection sqlConnection1 = new System.Data.SqlClient.SqlConnection();
            //sqlConnection1.ConnectionString = "Initial Catalog=GRCBoard;Data Source=AITI;User ID=sa;Password=280225";
            sqlConnection1.ConnectionString = Global.ConnectionString();
            string select = "select * from items where rowstatus>-1 and panjang*lebar>0 and description like '" + itemname + "%' order by description";
            SqlDataAdapter da = new SqlDataAdapter(select, sqlConnection1);
            DataSet ds = new DataSet();
            da.Fill(ds, "items");
            return (ds);
        }

        //[WebMethod]
        public DataSet GetSjNoBySchNo(string ScheduleNo)
        {
            System.Data.SqlClient.SqlConnection sqlConnection1 = new System.Data.SqlClient.SqlConnection();
            //sqlConnection1.ConnectionString = "Initial Catalog=GRCBoard;Data Source=AITI;User ID=sa;Password=280225";
            sqlConnection1.ConnectionString = Global.ConnectionString();
            string select = "select distinct top 200 B.ID,A.ScheduleNo,B.SuratJalanNo,B.CreatedTime from Schedule A, SuratJalan B " +
                "where B.Status>-1 and A.ID=B.ScheduleID and A.ScheduleNo like '%" + ScheduleNo + "%'";
            SqlDataAdapter da = new SqlDataAdapter(select, sqlConnection1);
            DataSet ds = new DataSet();
            da.Fill(ds, "SuratJalan");
            return (ds);
        }

        //[WebMethod]
        public DataSet GetSjNoBySchNoTO(string ScheduleNo)
        {
            System.Data.SqlClient.SqlConnection sqlConnection1 = new System.Data.SqlClient.SqlConnection();
            //sqlConnection1.ConnectionString = "Initial Catalog=GRCBoard;Data Source=AITI;User ID=sa;Password=280225";
            sqlConnection1.ConnectionString = Global.ConnectionString();
            string select = "select B.ID,A.ScheduleNo,B.SuratJalanNo,B.CreatedTime from Schedule A, SuratJalanTO B " +
                "where B.Status>-1 and A.ID=B.ScheduleID and A.ScheduleNo like '%" + ScheduleNo + "%'";
            SqlDataAdapter da = new SqlDataAdapter(select, sqlConnection1);
            DataSet ds = new DataSet();
            da.Fill(ds, "SuratJalan");
            return (ds);
        }

        //[WebMethod]
        public DataSet GetSjDetail(string SJ)
        {
            System.Data.SqlClient.SqlConnection sqlConnection1 = new System.Data.SqlClient.SqlConnection();
            //sqlConnection1.ConnectionString = "Initial Catalog=GRCBoard;Data Source=AITI;User ID=sa;Password=280225";
            sqlConnection1.ConnectionString = Global.ConnectionString();
            string select = "SELECT SJ.ID AS SJID, SJD.ID AS SJDID, SJD.ItemID as ItemIDSJ, I.Description ItemName, I.Tebal, I.Panjang, I.Lebar, SJD.Qty, " +
                "0 as QtyP FROM SuratJalan AS SJ INNER JOIN SuratJalanDetail AS SJD ON SJ.ID = SJD.SuratJalanID INNER JOIN  " +
                "Items AS I ON SJD.ItemID = I.ID where SJ.Status>-1 and SJ.SuratJalanNo ='" + SJ + "'";
            SqlDataAdapter da = new SqlDataAdapter(select, sqlConnection1);
            DataSet ds = new DataSet();
            da.Fill(ds, "SJDetail");
            return (ds);
        }

        //[WebMethod]
        public DataSet GetSjDetailTO(string SJ)
        {
            System.Data.SqlClient.SqlConnection sqlConnection1 = new System.Data.SqlClient.SqlConnection();
            //sqlConnection1.ConnectionString = "Initial Catalog=GRCBoard;Data Source=AITI;User ID=sa;Password=280225";
            sqlConnection1.ConnectionString = Global.ConnectionString();
            string select = "SELECT SJ.ID AS SJID, SJD.ID AS SJDID, SJD.ItemID as ItemIDSJ, I.Description ItemName, I.Tebal, I.Panjang, I.Lebar, SJD.Qty, " +
                "0 as QtyP FROM SuratJalanTO AS SJ INNER JOIN SuratJalanDetailTO AS SJD ON SJ.ID = SJD.SuratJalantoID INNER JOIN  " +
                "Items AS I ON SJD.ItemID = I.ID where SJ.Status>-1 and SJ.SuratJalanNo ='" + SJ + "'";
            SqlDataAdapter da = new SqlDataAdapter(select, sqlConnection1);
            DataSet ds = new DataSet();
            da.Fill(ds, "SJDetail");
            return (ds);
        }

        //[WebMethod]
        public DataSet GetSJInfo(string SJNo)
        {
            System.Data.SqlClient.SqlConnection sqlConnection1 = new System.Data.SqlClient.SqlConnection();
            //sqlConnection1.ConnectionString = "Initial Catalog=GRCBoard;Data Source=AITI;User ID=sa;Password=280225";
            sqlConnection1.ConnectionString = Global.ConnectionString();
            string select = "SELECT     A.SuratJalanNo AS sjno, B.OPNo, 0 AS total, A.CreatedTime AS tglkirim, CASE B.CustomerType WHEN 1 THEN (SELECT TokoName FROM Toko WHERE ID = B.CustomerId) ELSE " +
                "(SELECT CustomerName FROM Customer WHERE ID = B.CustomerId) END AS CUSTOMER FROM  OP AS B RIGHT OUTER JOIN SuratJalan AS A ON B.ID = A.OPID " +
                "WHERE A.SuratJalanNo = '" + SJNo + "'";
            SqlDataAdapter da = new SqlDataAdapter(select, sqlConnection1);
            DataSet ds = new DataSet();
            da.Fill(ds, "SJInfo");
            return (ds);
        }

        //[WebMethod]
        public DataSet GetSJInfoTO(string SJNo)
        {
            System.Data.SqlClient.SqlConnection sqlConnection1 = new System.Data.SqlClient.SqlConnection();
            //sqlConnection1.ConnectionString = "Initial Catalog=GRCBoard;Data Source=AITI;User ID=sa;Password=280225";
            sqlConnection1.ConnectionString = Global.ConnectionString();
            string select = "SELECT A.SuratJalanNo as sjno, B.TransferOrderNo as OPNo, C.DepoName  as customer,0 as total,A.createdtime as tglkirim " +
                "FROM Depo  AS C RIGHT OUTER JOIN TransferOrder AS B ON C.ID = B.ToDepoID  RIGHT OUTER JOIN SuratJalanTO AS A ON B.ID = A.TransferOrderID  " +
                "WHERE A.SuratJalanNo = '" + SJNo + "'";
            SqlDataAdapter da = new SqlDataAdapter(select, sqlConnection1);
            DataSet ds = new DataSet();
            da.Fill(ds, "SJInfo");
            return (ds);
        }

    }
}
