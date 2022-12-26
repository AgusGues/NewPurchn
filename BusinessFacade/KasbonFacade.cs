using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Web;
using Domain;
using DataAccessLayer;
using Dapper;

namespace BusinessFacade
{
    public class KasbonFacade : AbstractTransactionFacade
    {
        private Kasbon objKasbon = new Kasbon();
        private ArrayList arrKasbon;
        private List<SqlParameter> sqlListParam;

        public KasbonFacade(object objDomain)
            : base(objDomain)
        {
            objKasbon = (Kasbon)objDomain;
        }

        public KasbonFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@NoPengajuan", objKasbon.NoPengajuan));
                sqlListParam.Add(new SqlParameter("@KasbonDate", objKasbon.KasbonDate));
                sqlListParam.Add(new SqlParameter("@DeptID", objKasbon.DeptID));
                sqlListParam.Add(new SqlParameter("@PIC", objKasbon.PIC));
                sqlListParam.Add(new SqlParameter("@Status", objKasbon.Status));
                sqlListParam.Add(new SqlParameter("@Apv", objKasbon.Apv));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objKasbon.CreatedBy));
                sqlListParam.Add(new SqlParameter("@DanaCadangan", objKasbon.DanaCadangan));
                sqlListParam.Add(new SqlParameter("@KasbonType", objKasbon.KasbonType));

                int IntResult = transManager.DoTransaction(sqlListParam, "KasbonInsert");
                strError = transManager.Error;
                return IntResult;
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
                sqlListParam.Add(new SqlParameter("@ID", objKasbon.ID));
                sqlListParam.Add(new SqlParameter("@DeptID", objKasbon.DeptID));
                sqlListParam.Add(new SqlParameter("@Apv", objKasbon.Apv));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objKasbon.LastModifiedBy));

                int IntResult = transManager.DoTransaction(sqlListParam, "KasbonUpdateApv");
                strError = transManager.Error;
                return IntResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateApprovKasbon1(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objKasbon.ID));
                //sqlListParam.Add(new SqlParameter("@DeptID", objKasbon.DeptID));
                sqlListParam.Add(new SqlParameter("@Apv", objKasbon.Apv));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objKasbon.LastModifiedBy));

                int IntResult = transManager.DoTransaction(sqlListParam, "KasbonUpdateApv1");
                strError = transManager.Error;
                return IntResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateApprovKasbon2(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objKasbon.ID));
                //sqlListParam.Add(new SqlParameter("@DeptID", objKasbon.DeptID));
                sqlListParam.Add(new SqlParameter("@Apv", objKasbon.Apv));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objKasbon.LastModifiedBy));

                int IntResult = transManager.DoTransaction(sqlListParam, "KasbonUpdateApv2");
                strError = transManager.Error;
                return IntResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateStatusNotApproval(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objKasbon.ID));
                sqlListParam.Add(new SqlParameter("@Approval", objKasbon.Apv));
                sqlListParam.Add(new SqlParameter("@lastModifiedBy", objKasbon.CreatedBy));
                sqlListParam.Add(new SqlParameter("@AlasanNotApproval", objKasbon.AlasanNotApproval));

                int intResult = transManager.DoTransaction(sqlListParam, "KasbonNotApproval");

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
            string strSQL = "SELECT * FROM KasbonUser WHERE DeptID in (select ID from dept where deptname like '%purchasing%') AND RowStatus>-1 order by UserName";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrKasbon = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrKasbon.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrKasbon.Add(new Kasbon());

            return arrKasbon;
        }
        public ArrayList RetrievePIC(int deptid)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "SELECT * FROM KasbonUser WHERE DeptID=" + deptid + " AND RowStatus>-1 order by UserName";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrKasbon = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrKasbon.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrKasbon.Add(new Kasbon());

            return arrKasbon;
        }
        public ArrayList RetrieveKasbon()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //string strSQL = "SELECT DISTINCT ID, KasbonNo FROM Kasbon WHERE Approval=2 AND Status>-1 AND QtyPO=0";
            string strSQL = "SELECT DISTINCT k.ID, k.KasbonNo FROM Kasbon AS k LEFT JOIN KasbonDetail AS kd ON k.ID=kd.KID " +
                            "WHERE k.Approval=2 AND k.Status>-1 AND kd.PODetailID=0";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrKasbon = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrKasbon.Add(GenerateObjectKasbon(sqlDataReader));
                }
            }
            else
                arrKasbon.Add(new Kasbon());

            return arrKasbon;
        }

        public Kasbon RetrieveByNoKasbon(string id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "SELECT * FROM Kasbon as k left join KasbonDetail as kd on k.ID=kd.KID WHERE k.ID=" + id + " AND k.Status>-1";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrKasbon = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new Kasbon();
        }

        public Kasbon RetrieveByNoPo(string nopo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "SELECT pd.ID p.NoPO, kd.ItemName, pd.Qty, pd.Price FROM POPurchn AS p LEFT JOIN POPurchnDetail AS pd ON p.ID=pd.POID LEFT JOIN" +
                            "KasbonDetail AS kd ON pd.SppDetailID=kd.SppDetailID WHERE p.NoPO='" + nopo + "' AND kd.Status>-1 ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrKasbon = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectNoPO(sqlDataReader);
                }
            }

            return new Kasbon();
        }

        public ArrayList ViewGridKasbon(int id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //string strSQL = "SELECT distinct k.ID,kd.ID AS KDID,k.DeptID,u.UOMDesc,k.KasbonNo,k.NoPengajuan,s.NoSPP,p.NoPO,kd.ItemName,kd.ItemID,kd.UomID, " +
            //                "kd.EstimasiKasbon,kd.Qty,(kd.EstimasiKasbon*kd.Qty) AS Total,k.DanaCadangan,k.Pic, k.CreatedTime,k.TglKasbon,k.Approval,k.Status,k.AlasanNotApproved FROM Kasbon as k " +
            //                "left join KasbonDetail as kd on k.ID=kd.KID left join SPP as s on s.ID=kd.SPPID LEFT JOIN POPurchnDetail AS pd " +
            //                "ON s.ID=pd.SPPID LEFT JOIN POPurchn AS p ON p.ID=pd.POID LEFT JOIN uom AS u ON u.id=kd.UomID WHERE k.ID=" + id + " AND " +
            //                "pd.ItemID=kd.ItemID and kd.Status>-1 AND kd.EstimasiKasbon=pd.Price";
            string strSQL = "SELECT distinct k.ID,kd.ID AS KDID,k.DeptID,u.UOMDesc,k.KasbonNo,k.NoPengajuan,s.NoSPP,kd.ItemName,kd.ItemID,kd.UomID,k.KasbonType, " +
                            "kd.EstimasiKasbon,kd.Qty,(kd.EstimasiKasbon*kd.Qty) AS Total,k.DanaCadangan,k.Pic, k.CreatedTime,k.TglKasbon,k.Approval,k.Status,k.AlasanNotApproved FROM Kasbon as k " +
                            "left join KasbonDetail as kd on k.ID=kd.KID left join SPP as s on s.ID=kd.SPPID LEFT JOIN POPurchnDetail AS pd " +
                            "ON s.ID=pd.SPPID LEFT JOIN uom AS u ON u.id=kd.UomID WHERE k.ID=" + id + " AND " +
                            "pd.ItemID=kd.ItemID and kd.Status>-1";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrKasbon = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrKasbon.Add(GenerateObjectViewKasbon(sqlDataReader));
                }
            }
            else
                arrKasbon.Add(new Kasbon());

            return arrKasbon;
        }

        public ArrayList ViewGridKasbon2(int id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "SELECT distinct k.ID,kd.ID AS KDID,k.DeptID,k.KasbonNo,k.NoPengajuan,kd.ItemName,k.KasbonType,kd.EstimasiKasbon, " +
                            "kd.Qty,(kd.EstimasiKasbon*kd.Qty) AS Total,k.DanaCadangan,k.Pic, k.CreatedTime,k.TglKasbon,k.Approval,k.Status, " +
                            "k.AlasanNotApproved FROM Kasbon as k left join KasbonDetail as kd on k.ID=kd.KID WHERE k.ID=" + id + " and kd.Status>-1 ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrKasbon = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrKasbon.Add(GenerateObjectViewKasbon2(sqlDataReader));
                }
            }
            else
                arrKasbon.Add(new Kasbon());

            return arrKasbon;
        }

        public ArrayList ViewGridR(int id)
        {
            string strSQL = "SELECT distinct k.ID,kd.ID AS KDID,k.DeptID,u.UOMDesc,k.KasbonNo,k.NoPengajuan,pd.DocumentNo,p.NoPO,kd.ItemName,kd.ItemID,kd.UomID, " +
                            "kd.EstimasiKasbon,kd.Qty,(kd.EstimasiKasbon*kd.Qty) AS Total,pd.Qty AS QtyPO,pd.Price,(pd.Qty*pd.Price) AS TotalPO, " +
                            "k.DanaCadangan,k.Pic, k.CreatedTime,k.TglKasbon,k.Approval,k.Status,k.AlasanNotApproved FROM Kasbon as k " +
                            "left join KasbonDetail as kd on k.ID=kd.KID LEFT JOIN POPurchnDetail AS pd " +
                            "ON pd.ID=kd.PODetailID LEFT JOIN POPurchn AS p ON p.ID=pd.POID LEFT JOIN uom AS u ON u.id=kd.UomID WHERE k.ID=" + id + " AND " +
                            "k.Status>-1 and kd.Status>-1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrKasbon = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrKasbon.Add(GenerateObjectViewR(sqlDataReader));
                }
            }
            else
                arrKasbon.Add(new Kasbon());

            return arrKasbon;
        }

        public ArrayList RetrieveIDByPO(int id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "SELECT p.ID,p.NoPO,kd.ItemName,pd.Qty, pd.Price,kd.EstimasiKasbon,(pd.Price*pd.Qty) AS TotalPO FROM POPurchn AS p LEFT JOIN POPurchnDetail pd ON p.ID=pd.POID " +
                            "LEFT JOIN KasbonDetail AS kd ON pd.SppDetailID=kd.SppDetailID LEFT JOIN inventory AS i ON i.ID=pd.ItemID WHERE pd.ID=" + id + " ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrKasbon = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrKasbon.Add(GenerateObjectID(sqlDataReader));
                }
            }
            else
                arrKasbon.Add(new Kasbon());

            return arrKasbon;
        }

        public ArrayList RetrieveIDByPOKertas(int id, int kid)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select distinct pd.ID,p.NoPO,pd.DocumentNo as NoSPP,kd.ItemName,u.UOMDesc,kbd.HasilTimbang,kbd.Harga,kd.Qty,kd.EstimasiKasbon,(kd.Qty*kd.EstimasiKasbon) as Total,(kbd.Harga*kbd.HasilTimbang) AS TotalPO " +
                            "from [sql2.grcboard.com].GRCboard_Android.dbo.KertasBeli as kb left join [sql2.grcboard.com].GRCboard_Android.dbo.KertasBeliDetail as kbd " +
                            "on kb.id=kbd.KertasBeliID left join DeliveryKertas as dk on dk.NoSJ=kb.nosj left join POPurchn as p on p.ID=dk.POKAID left join POPurchnDetail as pd " +
                            "on pd.POID=p.ID left join KasbonDetail AS kd ON pd.SppDetailID=kd.SppDetailID left join UOM as u on u.ID=pd.UOMID where pd.ID=" + id + " and kd.KID=" + kid + " ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrKasbon = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrKasbon.Add(GenerateObjectIDItemKertas(sqlDataReader));
                }
            }
            else
                arrKasbon.Add(new Kasbon());

            return arrKasbon;
        }

        public ArrayList RetrieveIDByInputPO(string nopo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "SELECT pd.ID,p.NoPO,pd.Qty, pd.Price,(pd.Price*pd.Qty) AS TotalPO,pd.ItemTypeID, " +
                            "case pd.ItemTypeID when 1  then (select ItemName from Inventory where ID=pd.ItemID " +
                            "and RowStatus > -1) when 2 then (select ItemName from Asset where ID=pd.ItemID and RowStatus > -1) else (select ItemName from Biaya where " +
                            "ID=pd.ItemID and RowStatus > -1) end ItemName FROM POPurchn AS p LEFT JOIN POPurchnDetail pd ON p.ID=pd.POID " +
                            "WHERE pd.POID in (select id from POPurchn where NoPO='" + nopo + "') ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrKasbon = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrKasbon.Add(GenerateObjectIDInputPO(sqlDataReader));
                }
            }
            else
                arrKasbon.Add(new Kasbon());

            return arrKasbon;
        }

        public ArrayList RetrieveByNamaBarangPO(string nopo, string KasbonNO)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "SELECT pd.ID,pd.DocumentNo as NoSPP,p.NoPO,i.ItemName,pd.Qty as QtyPO, pd.Price,(pd.Price*pd.Qty) AS TotalPO,kd.Qty, " +
                            "kd.EstimasiKasbon,(kd.EstimasiKasbon*kd.Qty) AS Total,k.kasbonType FROM POPurchn AS p LEFT JOIN POPurchnDetail pd " +
                            "ON p.ID=pd.POID LEFT JOIN inventory AS i ON i.ID=pd.ItemID left join KasbonDetail as kd on kd.ItemName=i.ItemName left join Kasbon as k on k.ID=kd.KID " +
                            "WHERE pd.POID in (select id from POPurchn where NoPO='" + nopo + "') and KasbonNo='" + KasbonNO + "' and kd.Status>-1";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.NoPO,A.POPurchnDate,A.CreatedTime,A.SupplierID,A.Termin,A.PPN,A.Delivery,A.Crc,A.Keterangan,A.Terbilang,A.Disc,A.PPH,A.NilaiKurs,A.Cetak,A.CountPrt,A.Status,A.Approval,A.ApproveDate1,A.ApproveDate2,A.CreatedBy,A.LastModifiedBy,A.LastModifiedTime,A.AlasanBatal,A.AlasanClose,B.DepoID from POPurchn as A,SPP as B,POPurchnDetail as C where C.SPPID = B.ID and C.POID = A.ID and A.NoPO = " + noPO + " order by A.ID desc");
            strError = dataAccess.Error;
            arrKasbon = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrKasbon.Add(GenerateObjectNamaBarangPO(sqlDataReader));
                }
            }
            else
                arrKasbon.Add(new Kasbon());

            return arrKasbon;
        }

        public Kasbon RetrieveByNamaBarangPO2(string nopo, string KasbonNO, string NamaBarangPO, string ItemTypeID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //string strsql = "SELECT pd.ID,pd.DocumentNo as NoSPP,p.NoPO,case pd.ItemTypeID when 1  then (select ItemName from Inventory where ID=pd.ItemID " +
            //                "and RowStatus > -1) when 2 then (select ItemName from Asset where ID=pd.ItemID and RowStatus > -1) else (select ItemName from Biaya where " +
            //                "ID=pd.ItemID and RowStatus > -1) end ItemName, pd.Qty as QtyPO, pd.Price,(pd.Price*pd.Qty) AS TotalPO,kd.Qty, kd.EstimasiKasbon, " +
            //                "(kd.EstimasiKasbon*kd.Qty) AS Total,k.kasbonType FROM POPurchn AS p LEFT JOIN POPurchnDetail pd ON p.ID=pd.POID left join KasbonDetail as kd " +
            //                "on kd.ItemName=ItemName left join Kasbon as k on k.ID=kd.KID WHERE pd.POID in (select id from POPurchn where NoPO='" + nopo + "') " +
            //                "and KasbonNo='" + KasbonNO + "' and kd.ItemName='" + NamaBarangPO + "' and kd.Status>-1 ";
            if (Convert.ToInt32(ItemTypeID) == 1)
            {
                string strsql = "SELECT pd.ID,pd.DocumentNo as NoSPP,p.NoPO,i.ItemName,pd.Qty as QtyPO, pd.Price,(pd.Price*pd.Qty) AS TotalPO,kd.Qty, kd.EstimasiKasbon, " +
                                "(kd.EstimasiKasbon*kd.Qty) AS Total,k.kasbonType FROM POPurchn AS p LEFT JOIN POPurchnDetail pd ON p.ID=pd.POID LEFT JOIN inventory AS i " +
                                "ON i.ID=pd.ItemID left join KasbonDetail as kd on kd.ItemName=i.ItemName left join Kasbon as k on k.ID=kd.KID WHERE pd.POID in (select id from POPurchn where NoPO='" + nopo + "') " +
                                "and KasbonNo='" + KasbonNO + "' and kd.ItemName='" + NamaBarangPO + "' and kd.Status>-1 ";
                SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
                strError = dataAccess.Error;
                arrKasbon = new ArrayList();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        return GenerateObjectNamaBarangPO(sqlDataReader);
                    }
                }

                return new Kasbon();
            }
            else if (Convert.ToInt32(ItemTypeID) == 2)
            {
                string strsql = "SELECT pd.ID,pd.DocumentNo as NoSPP,p.NoPO,i.ItemName,pd.Qty as QtyPO, pd.Price,(pd.Price*pd.Qty) AS TotalPO,kd.Qty, kd.EstimasiKasbon, " +
                                "(kd.EstimasiKasbon*kd.Qty) AS Total,k.kasbonType FROM POPurchn AS p LEFT JOIN POPurchnDetail pd ON p.ID=pd.POID LEFT JOIN asset AS i " +
                                "ON i.ID=pd.ItemID left join KasbonDetail as kd on kd.ItemName=i.ItemName left join Kasbon as k on k.ID=kd.KID WHERE pd.POID in (select id from POPurchn where NoPO='" + nopo + "') " +
                                "and KasbonNo='" + KasbonNO + "' and kd.ItemName='" + NamaBarangPO + "' and kd.Status>-1 ";
                SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
                strError = dataAccess.Error;
                arrKasbon = new ArrayList();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        return GenerateObjectNamaBarangPO(sqlDataReader);
                    }
                }

                return new Kasbon();
            }
            else
            {
                string strsql = "SELECT pd.ID,pd.DocumentNo as NoSPP,p.NoPO,i.ItemName,pd.Qty as QtyPO, pd.Price,(pd.Price*pd.Qty) AS TotalPO,kd.Qty, kd.EstimasiKasbon, " +
                                   "(kd.EstimasiKasbon*kd.Qty) AS Total,k.kasbonType FROM POPurchn AS p LEFT JOIN POPurchnDetail pd ON p.ID=pd.POID LEFT JOIN biaya AS i " +
                                   "ON i.ID=pd.ItemID left join KasbonDetail as kd on kd.ItemName=i.ItemName left join Kasbon as k on k.ID=kd.KID WHERE pd.POID in (select id from POPurchn where NoPO='" + nopo + "') " +
                                   "and KasbonNo='" + KasbonNO + "' and kd.ItemName='" + NamaBarangPO + "' and kd.Status>-1 ";
                SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
                strError = dataAccess.Error;
                arrKasbon = new ArrayList();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        return GenerateObjectNamaBarangPO(sqlDataReader);
                    }
                }

                return new Kasbon();
            }
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.NoPO,A.POPurchnDate,A.CreatedTime,A.SupplierID,A.Termin,A.PPN,A.Delivery,A.Crc,A.Keterangan,A.Terbilang,A.Disc,A.PPH,A.NilaiKurs,A.Cetak,A.CountPrt,A.Status,A.Approval,A.ApproveDate1,A.ApproveDate2,A.CreatedBy,A.LastModifiedBy,A.LastModifiedTime,A.AlasanBatal,A.AlasanClose,B.DepoID from POPurchn as A,SPP as B,POPurchnDetail as C where C.SPPID = B.ID and C.POID = A.ID and A.NoPO = " + noPO + " order by A.ID desc");

        }

        public Kasbon RetrieveByKasbon(string id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "SELECT sum(b.EstimasiKasbon * b.Qty) AS TotalEstimasi FROM Kasbon AS a LEFT JOIN KasbonDetail AS b ON a.ID=b.KID " +
                            "WHERE a.ID=" + id + " AND a.Status>-1 and b.Status>-1 ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.NoPO,A.POPurchnDate,A.CreatedTime,A.SupplierID,A.Termin,A.PPN,A.Delivery,A.Crc,A.Keterangan,A.Terbilang,A.Disc,A.PPH,A.NilaiKurs,A.Cetak,A.CountPrt,A.Status,A.Approval,A.ApproveDate1,A.ApproveDate2,A.CreatedBy,A.LastModifiedBy,A.LastModifiedTime,A.AlasanBatal,A.AlasanClose,B.DepoID from POPurchn as A,SPP as B,POPurchnDetail as C where C.SPPID = B.ID and C.POID = A.ID and A.NoPO = " + noPO + " order by A.ID desc");
            strError = dataAccess.Error;
            arrKasbon = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectID2(sqlDataReader);
                }
            }

            return new Kasbon();
        }

        public Kasbon RetrieveByPO(string id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "SELECT case when sum(pd.Price * pd.Qty) = null then 0 else isnull(sum(pd.Price * pd.Qty),0) end AS TotalAllPO FROM POPurchn AS p LEFT JOIN POPurchnDetail AS pd ON p.ID=pd.POID LEFT JOIN " +
                            "KasbonDetail AS kd ON pd.SppDetailID=kd.SppDetailID WHERE kd.KID=" + id + " and kd.ItemID=pd.ItemID and pd.Qty=kd.Qty AND kd.Status>-1 AND pd.Status>-1 ";
            //string strsql = "SELECT sum(pd.Price * pd.Qty) AS TotalAllPO FROM POPurchnDetail AS pd WHERE pd.ID=" + id + " AND pd.Status>-1 ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.NoPO,A.POPurchnDate,A.CreatedTime,A.SupplierID,A.Termin,A.PPN,A.Delivery,A.Crc,A.Keterangan,A.Terbilang,A.Disc,A.PPH,A.NilaiKurs,A.Cetak,A.CountPrt,A.Status,A.Approval,A.ApproveDate1,A.ApproveDate2,A.CreatedBy,A.LastModifiedBy,A.LastModifiedTime,A.AlasanBatal,A.AlasanClose,B.DepoID from POPurchn as A,SPP as B,POPurchnDetail as C where C.SPPID = B.ID and C.POID = A.ID and A.NoPO = " + noPO + " order by A.ID desc");
            strError = dataAccess.Error;
            arrKasbon = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectID3(sqlDataReader);
                }
            }

            return new Kasbon();
        }

        public Kasbon RetrieveByNamaBarangPO(string id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "SELECT case when sum(pd.Price * pd.Qty) = null then 0 else isnull(sum(pd.Price * pd.Qty),0) end AS TotalAllPO FROM POPurchn AS p " +
                            "LEFT JOIN POPurchnDetail AS pd ON p.ID=pd.POID LEFT JOIN inventory AS i ON i.ID=pd.ItemID " +
                            "LEFT JOIN KasbonDetail AS kd ON kd.ItemName=i.ItemName WHERE " +
                            "kd.KID=" + id + " and pd.Qty=kd.Qty AND kd.Status>-1 AND pd.Status>-1  ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrKasbon = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectID3(sqlDataReader);
                }
            }

            return new Kasbon();
        }

        public Kasbon RetrieveTotalPO(string KasbonNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "SELECT sum(kd.HargaPO * kd.QtyPO) AS TotalAllPO FROM KasbonDetail AS kd WHERE kd.KID IN (SELECT ID FROM Kasbon WHERE " +
                            "KasbonNo='" + KasbonNo + "') AND kd.Status>-1 ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.NoPO,A.POPurchnDate,A.CreatedTime,A.SupplierID,A.Termin,A.PPN,A.Delivery,A.Crc,A.Keterangan,A.Terbilang,A.Disc,A.PPH,A.NilaiKurs,A.Cetak,A.CountPrt,A.Status,A.Approval,A.ApproveDate1,A.ApproveDate2,A.CreatedBy,A.LastModifiedBy,A.LastModifiedTime,A.AlasanBatal,A.AlasanClose,B.DepoID from POPurchn as A,SPP as B,POPurchnDetail as C where C.SPPID = B.ID and C.POID = A.ID and A.NoPO = " + noPO + " order by A.ID desc");
            strError = dataAccess.Error;
            arrKasbon = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectID3(sqlDataReader);
                }
            }

            return new Kasbon();
        }

        public int GetLastUrutan(int tahun)
        {
            int urutan = 0;
            string strSQL = "select top 1 urutan from (select CAST(SUBSTRING(NoPengajuan,1,4) as int) urutan from Kasbon where YEAR(TglKasbon)=" + tahun + ")A order by urutan desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    urutan = Convert.ToInt32(sqlDataReader["urutan"]);
                }
            }
            return urutan;
        }
        public Kasbon RetrieveByNo(string noPengajuan)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "select A.ID,A.NoPengajuan,A.TglKasbon,A.CreatedTime," +
                            "A.DeptID,A.Pic,A.DanaCadangan,A.Status,A.Approval,A.ApprovedDate1," +
                            "A.ApprovedDate2,A.CreatedBy,A.LastModifiedBy,A.LastModifiedTime,A.AlasanNotApproved," +
                            "isnull(C.ID,0) as KasbonDetailID  from Kasbon as A,KasbonDetail as C " +
                            "where C.KID = A.ID and A.NoPengajuan = '" + noPengajuan + "' order by A.ID desc";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrKasbon = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectNew2(sqlDataReader);
                }
            }

            return new Kasbon();
        }

        public Kasbon RetrieveByNo1(string noPengajuan)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "select top 100 k.ID, k.Approval, k.LastModifiedBy, case when (sum(kd.Qty * kd.EstimasiKasbon))+k.DanaCadangan = null then " +
                            "0 else (sum(kd.Qty * kd.EstimasiKasbon))+k.DanaCadangan end as Total, k.NoPengajuan from kasbon as k left join " +
                            "KasbonDetail as kd on k.ID=kd.KID where k.NoPengajuan='" + noPengajuan + "' " +
                            "group by k.ID,k.Approval, k.LastModifiedBy, k.DanaCadangan, k.NoPengajuan order by k.NoPengajuan, Total desc";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrKasbon = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectNo1(sqlDataReader);
                }
            }

            return new Kasbon();
        }
        public Kasbon RetrieveNoKasbon(string noKasbon)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "select A.ID,A.KasbonNo,A.TglKasbon,A.CreatedTime," +
                            "A.DeptID,A.Pic,A.DanaCadangan,A.Status,A.Approval,A.ApprovedDate1," +
                            "A.ApprovedDate2,A.CreatedBy,A.LastModifiedBy,A.LastModifiedTime,A.AlasanNotApproved," +
                            "isnull(C.ID,0) as KasbonDetailID  from Kasbon as A,KasbonDetail as C " +
                            "where C.KID = A.ID and A.KasbonNo = '" + noKasbon + "' order by A.ID desc";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.NoPO,A.POPurchnDate,A.CreatedTime,A.SupplierID,A.Termin,A.PPN,A.Delivery,A.Crc,A.Keterangan,A.Terbilang,A.Disc,A.PPH,A.NilaiKurs,A.Cetak,A.CountPrt,A.Status,A.Approval,A.ApproveDate1,A.ApproveDate2,A.CreatedBy,A.LastModifiedBy,A.LastModifiedTime,A.AlasanBatal,A.AlasanClose,B.DepoID from POPurchn as A,SPP as B,POPurchnDetail as C where C.SPPID = B.ID and C.POID = A.ID and A.NoPO = " + noPO + " order by A.ID desc");
            strError = dataAccess.Error;
            arrKasbon = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectNew3(sqlDataReader);
                }
            }

            return new Kasbon();
        }

        public Kasbon RetrieveNoKasbon1(string noKasbon)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "select top 100 k.ID, k.Approval, k.LastModifiedBy, case when (sum(kd.Qty * kd.EstimasiKasbon))+k.DanaCadangan = null then " +
                            "0 else (sum(kd.Qty * kd.EstimasiKasbon))+k.DanaCadangan end as Total, k.KasbonNo from kasbon as k left join " +
                            "KasbonDetail as kd on k.ID=kd.KID where k.KasbonNo='" + noKasbon + "' " +
                            "group by k.ID,k.Approval, k.LastModifiedBy, k.DanaCadangan, k.KasbonNo order by k.KasbonNo, Total desc";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrKasbon = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectNoKasbon(sqlDataReader);
                }
            }

            return new Kasbon();
        }

        public Kasbon RetrieveByID(int id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //string strsql = "SELECT p.ID,p.NoPO,kd.ItemName,pd.Qty, pd.Price,kd.EstimasiKasbon,(pd.Price*pd.Qty) AS TotalPO FROM POPurchn AS p LEFT JOIN POPurchnDetail pd ON p.ID=pd.POID " +
            //                "LEFT JOIN KasbonDetail AS kd ON pd.SppDetailID=kd.SppDetailID LEFT JOIN inventory AS i ON i.ID=pd.ItemID WHERE pd.ID=" + id + " ";
            string strsql = "SELECT p.ID,p.NoPO,kd.ItemName,pd.Qty, pd.Price,kd.EstimasiKasbon,(pd.Price*pd.Qty) AS TotalPO FROM POPurchn AS p " +
                            "LEFT JOIN POPurchnDetail pd ON p.ID=pd.POID LEFT JOIN inventory AS i ON i.ID=pd.ItemID " +
                            "LEFT JOIN KasbonDetail AS kd ON kd.ItemName=i.ItemName WHERE pd.ID=" + id + " ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrKasbon = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectID(sqlDataReader);
                }
            }

            return new Kasbon();
        }

        public Kasbon RetrieveByItemKertas(int id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "select distinct pd.ID,p.NoPO,pd.DocumentNo as NoSPP,kd.ItemName,u.UOMDesc,kbd.HasilTimbang,kbd.Harga,kd.Qty,kd.EstimasiKasbon,(kd.Qty*kd.EstimasiKasbon) as Total,(kbd.Harga*kbd.HasilTimbang) AS TotalPO from " +
                            "[sql2.grcboard.com].GRCboard_Android.dbo.KertasBeli as kb left join [sql2.grcboard.com].GRCboard_Android.dbo.KertasBeliDetail as kbd " +
                            "on kb.id=kbd.KertasBeliID left join DeliveryKertas as dk on dk.NoSJ=kb.nosj left join POPurchn as p on p.ID=dk.POKAID " +
                            "left join POPurchnDetail as pd on pd.POID=p.ID left join KasbonDetail AS kd ON pd.SppDetailID=kd.SppDetailID left join UOM as u on u.ID=pd.UOMID " +
                            "where pd.ID=" + id + " ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.NoPO,A.POPurchnDate,A.CreatedTime,A.SupplierID,A.Termin,A.PPN,A.Delivery,A.Crc,A.Keterangan,A.Terbilang,A.Disc,A.PPH,A.NilaiKurs,A.Cetak,A.CountPrt,A.Status,A.Approval,A.ApproveDate1,A.ApproveDate2,A.CreatedBy,A.LastModifiedBy,A.LastModifiedTime,A.AlasanBatal,A.AlasanClose,B.DepoID from POPurchn as A,SPP as B,POPurchnDetail as C where C.SPPID = B.ID and C.POID = A.ID and A.NoPO = " + noPO + " order by A.ID desc");
            strError = dataAccess.Error;
            arrKasbon = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectIDItemKertas(sqlDataReader);
                }
            }

            return new Kasbon();
        }

        public ArrayList RetrieveNoPO(string id)
        {
            string strSQL = "SELECT distinct pd.ID, p.NoPO, kd.ItemName, pd.Qty, pd.Price FROM POPurchn AS p LEFT JOIN POPurchnDetail AS pd ON p.ID=pd.POID LEFT JOIN " +
                            "KasbonDetail AS kd ON pd.SppDetailID=kd.SppDetailID WHERE kd.KID=" + id + " AND kd.Status>-1 AND pd.Status>-1 order by p.NoPO desc";
            //string strSQL = "SELECT k.ID,k.DeptID,k.KasbonNo,k.NoPengajuan,s.NoSPP,kd.ItemName,kd.ItemID,kd.UomID,kd.EstimasiKasbon,kd.Qty,k.Pic, " +
            //                "k.CreatedTime,k.TglKasbon,k.Approval,k.Status,k.AlasanNotApproved FROM Kasbon as k left join KasbonDetail as kd " +
            //                "on k.ID=kd.KID left join SPP as s on s.ID=kd.SPPID WHERE k.ID=" + id + "  AND k.Status>-1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrKasbon = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrKasbon.Add(GenerateObjectNoPO(sqlDataReader));
                }
            }
            else
                arrKasbon.Add(new Kasbon());

            return arrKasbon;
        }

        public ArrayList RetrieveNoPOTeamKhusus(string id)
        {
            string strSQL = "select distinct pd.ID,p.NoPO,kd.ItemID,kbd.HasilTimbang,kbd.Harga from [sql2.grcboard.com].GRCboard_Android.dbo.KertasBeli as kb " +
                            "left join [sql2.grcboard.com].GRCboard_Android.dbo.KertasBeliDetail as kbd on kb.id=kbd.KertasBeliID left join " +
                            "DeliveryKertas as dk on dk.NoSJ=kb.nosj left join POPurchn as p on p.ID=dk.POKAID left join POPurchnDetail as pd " +
                            "on pd.POID=p.ID left join KasbonDetail AS kd ON pd.SppDetailID=kd.SppDetailID WHERE kd.KID=" + id + " AND kd.Status>-1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrKasbon = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrKasbon.Add(GenerateObjectTeamKhusus(sqlDataReader));
                }
            }
            else
                arrKasbon.Add(new Kasbon());

            return arrKasbon;
        }

        public ArrayList RetrievePONamaBarang(string id)
        {
            string strSQL = "SELECT distinct pd.ID, p.NoPO, kd.ItemName, pd.Qty, pd.Price FROM POPurchn AS p LEFT JOIN POPurchnDetail AS pd ON p.ID=pd.POID " +
                            "LEFT JOIN inventory AS i ON i.ID=pd.ItemID LEFT JOIN " +
                            "KasbonDetail AS kd ON kd.ItemName=i.ItemName WHERE kd.KID=" + id + " AND kd.Status>-1 AND pd.Status>-1 order by p.NoPO desc";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrKasbon = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrKasbon.Add(GenerateObjectNoPO(sqlDataReader));
                }
            }
            else
                arrKasbon.Add(new Kasbon());

            return arrKasbon;
        }

        public Kasbon RetrieveByNoWithKasbonNO(string noPengajuan)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "SELECT DISTINCT TOP 500 k.ID,kd.ID AS KDID,k.DeptID,u.UOMDesc,k.KasbonNo,k.NoPengajuan,s.NoSPP,p.NoPO,kd.ItemName, " +
                            "kd.ItemID,kd.UomID,kd.EstimasiKasbon,kd.Qty,k.DanaCadangan,k.Pic, k.CreatedTime,k.TglKasbon,k.Approval,k.Status, " +
                            "k.AlasanNotApproved FROM Kasbon as k left join KasbonDetail as kd on k.ID=kd.KID left join SPP as s on s.ID=kd.SPPID " +
                            "LEFT JOIN POPurchnDetail AS pd ON s.ID=pd.SPPID LEFT JOIN POPurchn AS p ON p.ID=pd.POID LEFT JOIN uom AS u " +
                            "ON u.id=kd.UomID WHERE k.NoPengajuan='" + noPengajuan + "' ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrKasbon = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectK(sqlDataReader);
                }
            }

            return new Kasbon();
        }

        public ArrayList RetrieveAllK(int groupID, string strField, string strNoKasbon)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string strCariNoKasbon = string.Empty;
            if (strNoKasbon != string.Empty)
            {
                strCariNoKasbon = strField + "'%" + strNoKasbon + "%' and ";
            }
            else
            {
                strCariNoKasbon = (users.Apv == 0) ? " k.CreatedBy='" + users.UserID.ToString() + "' and " : "";
            }
            string strSQL = "SELECT DISTINCT TOP 500 k.ID,kd.ID AS KDID,k.DeptID,u.UOMDesc,k.KasbonNo,k.NoPengajuan,s.NoSPP,kd.ItemName, " +
                            "kd.ItemID,kd.UomID,kd.EstimasiKasbon,kd.Qty,k.DanaCadangan,k.Pic, k.CreatedTime,k.TglKasbon,k.Approval,k.Status, " +
                            "k.AlasanNotApproved FROM Kasbon as k left join KasbonDetail as kd on k.ID=kd.KID left join SPP as s on s.ID=kd.SPPID " +
                            "LEFT JOIN POPurchnDetail AS pd ON s.ID=pd.SPPID LEFT JOIN uom AS u " +
                            "ON u.id=kd.UomID WHERE " + strCariNoKasbon + " kd.Status>-1 ORDER BY k.ID DESC";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrKasbon = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrKasbon.Add(GenerateObjectAllKas(sqlDataReader));
                }
            }
            else
                arrKasbon.Add(new Kasbon());

            return arrKasbon;
        }

        public ArrayList RetrieveAllR(int groupID, string strField, string strNoKasbon)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string strCariNoKasbon = string.Empty;
            if (strNoKasbon != string.Empty)
            {
                strCariNoKasbon = strField + "'%" + strNoKasbon + "%'";
            }
            else
            {
                strCariNoKasbon = (users.Apv == 0) ? " k.CreatedBy='" + users.UserID.ToString() + "' and " : "";
            }
            string strSQL = "SELECT DISTINCT TOP 500 k.ID,kd.ID AS KDID,k.DeptID,u.UOMDesc,k.KasbonNo,k.NoPengajuan,pd.DocumentNo,p.NoPO,kd.ItemName, " +
                            "kd.ItemID,kd.UomID,kd.EstimasiKasbon,kd.Qty,k.DanaCadangan,k.Pic, k.CreatedTime,k.TglKasbon,k.Approval,k.Status, " +
                            "k.AlasanNotApproved FROM Kasbon as k left join KasbonDetail as kd on k.ID=kd.KID " +
                            "LEFT JOIN POPurchnDetail AS pd ON pd.ID=kd.PODetailID LEFT JOIN POPurchn AS p ON p.ID=pd.POID LEFT JOIN uom AS u " +
                            "ON u.id=kd.UomID WHERE " + strCariNoKasbon + " k.Status>-1 AND kd.Status>-1and k.Approval>=2 AND kd.PODetailID<>0 ORDER BY k.ID DESC";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrKasbon = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrKasbon.Add(GenerateObjectAllR(sqlDataReader));
                }
            }
            else
                arrKasbon.Add(new Kasbon());

            return arrKasbon;
        }

        public Kasbon RetrieveByRevisi(string NoPengajuan)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "SELECT * FROM Kasbon where NoPengajuan='" + NoPengajuan + "' AND Status>-1";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrKasbon = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectRevisi(sqlDataReader);
                }
            }

            return new Kasbon();
        }

        public ArrayList RetrieveRevisiDetail(int KID, string Users)
        {

            string All = (Users == "admin" || Users == "Admin") ? " " : " and KasbonDetail.Status >-1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "SELECT kd.ID,kd.KID,kd.Qty,kd.EstimasiKasbon,sd.SPPID,sd.GroupID,sd.ItemID,sd.Quantity,sd.ItemID,sd.Status,sd.QtyPO, " +
                            "CASE sd.ItemTypeID WHEN 1 THEN (Select ItemCode from Inventory where Inventory.ID=sd.itemID) " +
                            "WHEN 2 THEN (select ItemCode from Asset where Asset.ID=sd.ItemID) " +
                            "WHEN 3 THEN (select ItemCode from Biaya where Biaya.ID=sd.ItemID) " +
                            "ELSE '' END as ItemCode, CASE sd.ItemTypeID  " +
                            "WHEN 1 THEN (Select ItemName from Inventory where Inventory.ID=sd.itemID) " +
                            "WHEN 2 THEN (select ItemName from Asset where Asset.ID=sd.ItemID) " +
                            "WHEN 3 THEN CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus=1), " +
                            "GETDATE())<  (Select CreatedTime from SPP where SPP.ID=sd.SPPID)) " +
                            "THEN(select ItemName from Biaya where ID=sd.ItemID and Biaya.RowStatus>-1)+' - '+ " +
                            "(select ItemName from biaya where ID=sd.ItemID and biaya.RowStatus>-1) END     ELSE '' END as ItemName, " +
                            "s.UomDesc,kd.SppDetailID FROM SPPDetail AS sd , Uom as s, KasbonDetail AS kd where  s.ID=sd.UOMID " +
                            "AND kd.SppDetailID=sd.ID and kd.KID='" + KID + "' AND kd.Status>-1 and sd.Status >-1 order by sd.ID ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrKasbon = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrKasbon.Add(GenerateObjectViewRevisi(sqlDataReader));
                }
            }
            else
                arrKasbon.Add(new RevisiPO());

            return arrKasbon;
        }

        public ArrayList RetrieveOSKasbon(string Tglawal, string Tglakhir, string Criteria)
        {
            string strSQL = "SELECT DISTINCT k.ID,kd.ID AS KDID,k.DeptID,u.UOMDesc,k.KasbonNo,k.NoPengajuan,s.NoSPP,kd.ItemName, " +
                            "kd.ItemID,kd.UomID,kd.EstimasiKasbon,kd.Qty,sd.Quantity,k.DanaCadangan,k.Pic, k.CreatedTime,k.TglKasbon, " +
                            "k.Approval,k.Status,k.AlasanNotApproved,(EstimasiKasbon*Qty) as Total FROM Kasbon as k left join KasbonDetail as kd on k.ID=kd.KID " +
                            "LEFT JOIN SPPDetail as sd on sd.ID=kd.SppDetailID LEFT JOIN SPP as s on s.ID=sd.SPPID LEFT JOIN uom AS u " +
                            "ON u.id=kd.UomID WHERE k.TglKasbon between '" + Tglawal + "' and '" + Tglakhir + "' and k.Status>-1 AND kd.Status>-1 and k.Approval!=4 " +
                            "" + Criteria + " ORDER BY k.ID DESC ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrKasbon = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrKasbon.Add(GenerateObjectOSKasbon(sqlDataReader));
                }
            }
            else
                arrKasbon.Add(new Kasbon());

            return arrKasbon;
        }

        public Kasbon RetrieveNumberID()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from KasbonNumber");
            strError = dataAccess.Error;
            arrKasbon = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectNumber(sqlDataReader);
                }
            }

            return new Kasbon();
        }

        public string GetAppPIC(int IDUsers, int apv)
        {
            string result = string.Empty;
            try
            {
                string strSQL = "Select AppPIC from KasbonApp where RowStatus>-1 and UserID=" + IDUsers.ToString() + " and AppLevel=" + apv.ToString();
                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(strSQL);
                if (da.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        result = sdr["AppPIC"].ToString();
                    }
                }
                return result;
            }
            catch
            {
                return result;
            }
        }

        public string GetAppWithTotal(int IDUsers, int Apv)
        {
            decimal result = 0;
            try
            {
                string strSQL = "select * from ( " +
                                "select top 100 case when (cast(sum(kd.Qty * kd.EstimasiKasbon) as decimal(18,2)))+cast(k.DanaCadangan as decimal(18,2)) = null then 0 else (cast(sum(kd.Qty * kd.EstimasiKasbon)as decimal(18,2)) )+cast(k.DanaCadangan as decimal(18,2)) end as Total, k.NoPengajuan from kasbon as k left join " +
                                "KasbonDetail as kd on k.ID=kd.KID where k.Pic in (Select AppPIC from KasbonApp where UserID=" + IDUsers.ToString() + " and AppLevel=0) " +
                                "and k.Approval=" + (Apv - 1) + " group by k.DanaCadangan, k.NoPengajuan order by k.NoPengajuan, Total desc ) a where Total < 3000000 ";
                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(strSQL);
                if (da.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        result = (sdr["Total"] == DBNull.Value) ? 0 : Convert.ToDecimal(sdr["Total"]);
                        //result = Convert.ToInt32(sdr["Approval"].ToString());
                    }
                }
                return result.ToString();
            }
            catch
            {
                return result.ToString();
            }
        }

        public string GetAppWithTotal1(int IDUsers, int Apv)
        {
            decimal result = 0;
            try
            {
                string strSQL = "select * from ( " +
                                "select top 100 case when (cast(sum(kd.Qty * kd.EstimasiKasbon) as decimal(18,2)))+ cast(k.DanaCadangan as decimal(18,2)) = null then 0 else (cast(sum(kd.Qty * kd.EstimasiKasbon) as decimal(18,2)))+ cast(k.DanaCadangan as decimal(18,2)) end as Total, k.NoPengajuan,k.ApvMgr from kasbon as k left join " +
                                "KasbonDetail as kd on k.ID=kd.KID where k.Pic in (Select AppPIC from KasbonApp where UserID=" + IDUsers.ToString() + " and AppLevel=0) " +
                                "and k.Approval=" + (Apv - 1) + " group by k.DanaCadangan, k.NoPengajuan,k.ApvMgr order by k.NoPengajuan, Total desc ) a where Total > 3000000 and ApvMgr=1 ";
                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(strSQL);
                if (da.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        result = (sdr["Total"] == DBNull.Value) ? 0 : Convert.ToDecimal(sdr["Total"]);
                        //result = Convert.ToInt32(sdr["Approval"].ToString());
                    }
                }
                return result.ToString();
            }
            catch
            {
                return result.ToString();
            }
        }
        public string GetAppWithTotalRealisasi(int IDUsers, int Apv)
        {
            decimal result = 0;
            try
            {
                string strSQL = "select * from ( " +
                                "select top 100 case when (sum(kd.Qty * kd.EstimasiKasbon))+k.DanaCadangan = null then 0 else (sum(kd.Qty * kd.EstimasiKasbon))+k.DanaCadangan end as Total, k.NoPengajuan from kasbon as k left join " +
                                "KasbonDetail as kd on k.ID=kd.KID where k.Pic in (Select AppPIC from KasbonApp where UserID=" + IDUsers.ToString() + " and AppLevel=0) " +
                                "and k.Approval=" + (Apv + 1) + " AND kd.PODetailID!=0 group by k.DanaCadangan, k.NoPengajuan order by k.NoPengajuan, Total desc ) a where Total < 3000000 ";
                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(strSQL);
                if (da.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        result = (sdr["Total"] == DBNull.Value) ? 0 : Convert.ToDecimal(sdr["Total"]);
                        //result = Convert.ToInt32(sdr["Approval"].ToString());
                    }
                }
                return result.ToString();
            }
            catch
            {
                return result.ToString();
            }
        }

        public string GetAppWithTotalRealisasi2(int IDUsers, int Apv)
        {
            decimal result = 0;
            try
            {
                string strSQL = "select * from ( " +
                                "select top 100 case when (sum(kd.Qty * kd.EstimasiKasbon))+k.DanaCadangan = null then 0 else (sum(kd.Qty * kd.EstimasiKasbon))+k.DanaCadangan end as Total, k.NoPengajuan,k.ApvMgr from kasbon as k left join " +
                                "KasbonDetail as kd on k.ID=kd.KID where k.Pic in (Select AppPIC from KasbonApp where UserID=" + IDUsers.ToString() + " and AppLevel=0) " +
                                "and k.Approval=" + (Apv + 1) + " AND kd.PODetailID!=0 group by k.DanaCadangan, k.NoPengajuan,k.ApvMgr order by k.NoPengajuan, Total desc ) a where Total > 3000000 and ApvMgr=2 ";
                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(strSQL);
                if (da.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        result = (sdr["Total"] == DBNull.Value) ? 0 : Convert.ToDecimal(sdr["Total"]);
                        //result = Convert.ToInt32(sdr["Approval"].ToString());
                    }
                }
                return result.ToString();
            }
            catch
            {
                return result.ToString();
            }
        }

        public string GetAppWithTotal2(int IDUsers, int Apv)
        {
            decimal result = 0;
            try
            {
                string strSQL = "select * from (select top 5 case when (sum(kd.Qty * kd.EstimasiKasbon))+k.DanaCadangan < 3000000 then 0 " +
                                "else (sum(kd.Qty * kd.EstimasiKasbon))+k.DanaCadangan end as Total, k.NoPengajuan from kasbon as k left join " +
                                "KasbonDetail as kd on k.ID=kd.KID where k.Pic in (Select AppPIC from KasbonApp where UserID=" + IDUsers.ToString() + " and AppLevel=0) " +
                                "and k.Approval=" + (Apv - 1) + " group by k.DanaCadangan, k.NoPengajuan order by k.NoPengajuan, Total desc) a where Total > 0 ";
                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(strSQL);
                if (da.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        result = (sdr["Total"] == DBNull.Value) ? 0 : Convert.ToDecimal(sdr["Total"]);
                        //result = Convert.ToInt32(sdr["Approval"].ToString());
                    }
                }
                return result.ToString();
            }
            catch
            {
                return result.ToString();
            }
        }

        public string GetAppWithTotalRealisasi1(int IDUsers, int Apv)
        {
            decimal result = 0;
            try
            {
                string strSQL = "select case when (sum(kd.Qty * kd.EstimasiKasbon))+k.DanaCadangan < 3000000 then 0 else (sum(kd.Qty * kd.EstimasiKasbon))+k.DanaCadangan end as Total, k.NoPengajuan from kasbon as k left join " +
                                "KasbonDetail as kd on k.ID=kd.KID where k.Pic in (Select AppPIC from KasbonApp where UserID=" + IDUsers.ToString() + " and AppLevel=0) " +
                                "and k.Approval=" + (Apv + 1) + " group by k.DanaCadangan, k.NoPengajuan order by k.NoPengajuan, Total desc ";
                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(strSQL);
                if (da.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        result = (sdr["Total"] == DBNull.Value) ? 0 : Convert.ToDecimal(sdr["Total"]);
                        //result = Convert.ToInt32(sdr["Approval"].ToString());
                    }
                }
                return result.ToString();
            }
            catch
            {
                return result.ToString();
            }
        }

        public Kasbon GenerateObjectOSKasbon(SqlDataReader sqlDataReader)
        {
            objKasbon = new Kasbon();
            objKasbon.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objKasbon.KDID = Convert.ToInt32(sqlDataReader["KDID"]);
            objKasbon.UOMDesc = sqlDataReader["UOMDesc"].ToString();
            objKasbon.NoKasbon = sqlDataReader["KasbonNo"].ToString();
            objKasbon.NoPengajuan = sqlDataReader["NoPengajuan"].ToString();
            objKasbon.NoSPP = sqlDataReader["NoSPP"].ToString();
            objKasbon.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objKasbon.DanaCadangan = Convert.ToDecimal(sqlDataReader["DanaCadangan"]);
            objKasbon.EstimasiKasbon = Convert.ToDecimal(sqlDataReader["EstimasiKasbon"]);
            objKasbon.Qty = Convert.ToDecimal(sqlDataReader["Qty"]);
            objKasbon.UOMID = Convert.ToInt32(sqlDataReader["UomID"]);
            objKasbon.NamaBarang = sqlDataReader["ItemName"].ToString();
            objKasbon.PIC = sqlDataReader["Pic"].ToString();
            objKasbon.KasbonDate = Convert.ToDateTime(sqlDataReader["TglKasbon"]);
            objKasbon.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objKasbon.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objKasbon.Apv = Convert.ToInt32(sqlDataReader["Approval"]);
            objKasbon.AlasanNotApproval = sqlDataReader["AlasanNotApproved"].ToString();
            objKasbon.Total = Convert.ToDecimal(sqlDataReader["Total"]);

            return objKasbon;
        }
        public Kasbon GenerateObjectNumber(SqlDataReader sqlDataReader)
        {
            objKasbon = new Kasbon();
            objKasbon.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objKasbon.KasbonCounter = Convert.ToInt32(sqlDataReader["KasbonCounter"]);
            objKasbon.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objKasbon.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objKasbon;
        }

        public Kasbon GenerateObjectViewRevisi(SqlDataReader sqlDataReader)
        {
            objKasbon = new Kasbon();
            objKasbon.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objKasbon.KDID = Convert.ToInt32(sqlDataReader["KDID"]);
            objKasbon.EstimasiKasbon = Convert.ToDecimal(sqlDataReader["EstimasiKasbon"]);
            objKasbon.Qty = Convert.ToDecimal(sqlDataReader["Qty"]);
            objKasbon.Satuan = sqlDataReader["UOMDesc"].ToString();
            objKasbon.NamaBarang = sqlDataReader["ItemName"].ToString();
            objKasbon.KodeBarang = sqlDataReader["ItemCode"].ToString();
            objKasbon.Status = Convert.ToInt32(sqlDataReader["Status"]);

            return objKasbon;
        }

        public Kasbon GenerateObjectRevisi(SqlDataReader sqlDataReader)
        {
            objKasbon = new Kasbon();
            objKasbon.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objKasbon.NoKasbon = sqlDataReader["KasbonNo"].ToString();
            objKasbon.NoPengajuan = sqlDataReader["NoPengajuan"].ToString();
            objKasbon.PIC = sqlDataReader["Pic"].ToString();
            objKasbon.NamaBarang = sqlDataReader["ItemName"].ToString();
            objKasbon.KasbonDate = Convert.ToDateTime(sqlDataReader["TglKasbon"]);
            objKasbon.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objKasbon.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objKasbon.Apv = Convert.ToInt32(sqlDataReader["Approval"]);
            objKasbon.DanaCadangan = Convert.ToDecimal(sqlDataReader["DanaCadangan"]);
            objKasbon.AlasanNotApproval = sqlDataReader["AlasanNotApproved"].ToString();

            return objKasbon;
        }

        public Kasbon GenerateObjectAllKas(SqlDataReader sqlDataReader)
        {
            objKasbon = new Kasbon();
            objKasbon.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objKasbon.KDID = Convert.ToInt32(sqlDataReader["KDID"]);
            objKasbon.NoKasbon = sqlDataReader["KasbonNo"].ToString();
            objKasbon.NoSPP = sqlDataReader["NoSPP"].ToString();
            //objKasbon.NoPo = sqlDataReader["NoPO"].ToString();
            objKasbon.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objKasbon.EstimasiKasbon = Convert.ToDecimal(sqlDataReader["EstimasiKasbon"]);
            objKasbon.Qty = Convert.ToDecimal(sqlDataReader["Qty"]);
            objKasbon.Satuan = sqlDataReader["UOMDesc"].ToString();
            objKasbon.NoPengajuan = sqlDataReader["NoPengajuan"].ToString();
            objKasbon.NamaBarang = sqlDataReader["ItemName"].ToString();
            objKasbon.KasbonDate = Convert.ToDateTime(sqlDataReader["TglKasbon"]);
            objKasbon.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objKasbon.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objKasbon.Apv = Convert.ToInt32(sqlDataReader["Approval"]);
            objKasbon.DanaCadangan = Convert.ToDecimal(sqlDataReader["DanaCadangan"]);
            objKasbon.AlasanNotApproval = sqlDataReader["AlasanNotApproved"].ToString();

            return objKasbon;
        }

        public Kasbon GenerateObjectAllR(SqlDataReader sqlDataReader)
        {
            objKasbon = new Kasbon();
            objKasbon.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objKasbon.KDID = Convert.ToInt32(sqlDataReader["KDID"]);
            objKasbon.NoKasbon = sqlDataReader["KasbonNo"].ToString();
            objKasbon.NoSPP = sqlDataReader["DocumentNo"].ToString();
            objKasbon.NoPo = sqlDataReader["NoPO"].ToString();
            objKasbon.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objKasbon.EstimasiKasbon = Convert.ToDecimal(sqlDataReader["EstimasiKasbon"]);
            objKasbon.Qty = Convert.ToDecimal(sqlDataReader["Qty"]);
            objKasbon.Satuan = sqlDataReader["UOMDesc"].ToString();
            objKasbon.NoPengajuan = sqlDataReader["NoPengajuan"].ToString();
            objKasbon.NamaBarang = sqlDataReader["ItemName"].ToString();
            objKasbon.KasbonDate = Convert.ToDateTime(sqlDataReader["TglKasbon"]);
            objKasbon.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objKasbon.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objKasbon.Apv = Convert.ToInt32(sqlDataReader["Approval"]);
            objKasbon.DanaCadangan = Convert.ToDecimal(sqlDataReader["DanaCadangan"]);
            objKasbon.AlasanNotApproval = sqlDataReader["AlasanNotApproved"].ToString();

            return objKasbon;
        }

        public Kasbon GenerateObjectK(SqlDataReader sqlDataReader)
        {
            objKasbon = new Kasbon();
            objKasbon.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objKasbon.KDID = Convert.ToInt32(sqlDataReader["KDID"]);
            objKasbon.NoKasbon = sqlDataReader["KasbonNo"].ToString();
            objKasbon.NoSPP = sqlDataReader["NoSPP"].ToString();
            objKasbon.NoPo = sqlDataReader["NoPO"].ToString();
            objKasbon.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objKasbon.EstimasiKasbon = Convert.ToDecimal(sqlDataReader["EstimasiKasbon"]);
            objKasbon.Qty = Convert.ToDecimal(sqlDataReader["Qty"]);
            objKasbon.PIC = sqlDataReader["PIC"].ToString();
            objKasbon.Satuan = sqlDataReader["UOMDesc"].ToString();
            objKasbon.NoPengajuan = sqlDataReader["NoPengajuan"].ToString();
            objKasbon.NamaBarang = sqlDataReader["ItemName"].ToString();
            objKasbon.KasbonDate = Convert.ToDateTime(sqlDataReader["TglKasbon"]);
            objKasbon.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objKasbon.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objKasbon.Apv = Convert.ToInt32(sqlDataReader["Approval"]);
            objKasbon.DanaCadangan = Convert.ToDecimal(sqlDataReader["DanaCadangan"]);
            objKasbon.AlasanNotApproval = sqlDataReader["AlasanNotApproved"].ToString();

            return objKasbon;
        }


        public Kasbon GenerateObjectNew(SqlDataReader sqlDataReader)
        {
            objKasbon = new Kasbon();
            objKasbon.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objKasbon.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            //objKasbon.UserName = sqlDataReader["UserName"].ToString();
            objKasbon.NoPengajuan = sqlDataReader["NoPengajuan"].ToString();
            objKasbon.KasbonDate = Convert.ToDateTime(sqlDataReader["TglKasbon"]);
            objKasbon.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objKasbon.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objKasbon.Apv = Convert.ToInt32(sqlDataReader["Approval"]);
            objKasbon.ApproveDate1 = (sqlDataReader["ApprovedDate1"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(sqlDataReader["ApprovedDate1"]);
            objKasbon.ApproveDate2 = (sqlDataReader["ApprovedDate2"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(sqlDataReader["ApprovedDate2"]);
            objKasbon.AlasanNotApproval = sqlDataReader["AlasanNotApproved"].ToString();
            objKasbon.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objKasbon.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objKasbon.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objKasbon.LastModifiedTime = (sqlDataReader["lastModifiedTime"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);

            return objKasbon;
        }

        public Kasbon GenerateObject2(SqlDataReader sqlDataReader)
        {
            objKasbon = new Kasbon();
            objKasbon.ID = Convert.ToInt32(sqlDataReader["ID"]);
            //objKasbon.NoSPP = sqlDataReader["NoSPP"].ToString();
            objKasbon.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objKasbon.DanaCadangan = Convert.ToDecimal(sqlDataReader["DanaCadangan"]);
            objKasbon.EstimasiKasbon = Convert.ToDecimal(sqlDataReader["EstimasiKasbon"]);
            objKasbon.Qty = Convert.ToDecimal(sqlDataReader["Qty"]);
            objKasbon.UOMID = Convert.ToInt32(sqlDataReader["UomID"]);
            objKasbon.NoPengajuan = sqlDataReader["NoPengajuan"].ToString();
            objKasbon.NamaBarang = sqlDataReader["ItemName"].ToString();
            objKasbon.PIC = sqlDataReader["Pic"].ToString();
            objKasbon.KasbonDate = Convert.ToDateTime(sqlDataReader["TglKasbon"]);
            objKasbon.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objKasbon.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objKasbon.Apv = Convert.ToInt32(sqlDataReader["Approval"]);
            objKasbon.ApproveDate1 = (sqlDataReader["ApprovedDate1"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(sqlDataReader["ApprovedDate1"]);
            objKasbon.ApproveDate2 = (sqlDataReader["ApprovedDate2"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(sqlDataReader["ApprovedDate2"]);
            objKasbon.AlasanNotApproval = sqlDataReader["AlasanNotApproved"].ToString();
            objKasbon.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objKasbon.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objKasbon.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objKasbon.LastModifiedTime = (sqlDataReader["lastModifiedTime"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            objKasbon.KasbonType = Convert.ToInt32(sqlDataReader["KasbonType"]);
            return objKasbon;
        }

        public Kasbon GenerateObjectViewKasbon(SqlDataReader sqlDataReader)
        {
            objKasbon = new Kasbon();
            objKasbon.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objKasbon.KDID = Convert.ToInt32(sqlDataReader["KDID"]);
            objKasbon.NoSPP = sqlDataReader["NoSPP"].ToString();
            objKasbon.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objKasbon.EstimasiKasbon = Convert.ToDecimal(sqlDataReader["EstimasiKasbon"]);
            objKasbon.Total = Convert.ToDecimal(sqlDataReader["Total"]);
            objKasbon.Qty = Convert.ToDecimal(sqlDataReader["Qty"]);
            objKasbon.Satuan = sqlDataReader["UOMDesc"].ToString();
            objKasbon.NoPengajuan = sqlDataReader["NoPengajuan"].ToString();
            objKasbon.NamaBarang = sqlDataReader["ItemName"].ToString();
            objKasbon.KasbonDate = Convert.ToDateTime(sqlDataReader["TglKasbon"]);
            objKasbon.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objKasbon.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objKasbon.Apv = Convert.ToInt32(sqlDataReader["Approval"]);
            objKasbon.DanaCadangan = Convert.ToDecimal(sqlDataReader["DanaCadangan"]);
            objKasbon.AlasanNotApproval = sqlDataReader["AlasanNotApproved"].ToString();
            objKasbon.KasbonType = Convert.ToInt32(sqlDataReader["KasbonType"]);

            return objKasbon;
        }

        public Kasbon GenerateObjectViewKasbon2(SqlDataReader sqlDataReader)
        {
            objKasbon = new Kasbon();
            objKasbon.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objKasbon.KDID = Convert.ToInt32(sqlDataReader["KDID"]);
            objKasbon.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objKasbon.EstimasiKasbon = Convert.ToDecimal(sqlDataReader["EstimasiKasbon"]);
            objKasbon.Total = Convert.ToDecimal(sqlDataReader["Total"]);
            objKasbon.Qty = Convert.ToDecimal(sqlDataReader["Qty"]);
            objKasbon.NoPengajuan = sqlDataReader["NoPengajuan"].ToString();
            objKasbon.NamaBarang = sqlDataReader["ItemName"].ToString();
            objKasbon.KasbonDate = Convert.ToDateTime(sqlDataReader["TglKasbon"]);
            objKasbon.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objKasbon.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objKasbon.Apv = Convert.ToInt32(sqlDataReader["Approval"]);
            objKasbon.DanaCadangan = Convert.ToDecimal(sqlDataReader["DanaCadangan"]);
            objKasbon.AlasanNotApproval = sqlDataReader["AlasanNotApproved"].ToString();
            objKasbon.KasbonType = Convert.ToInt32(sqlDataReader["KasbonType"]);
            return objKasbon;
        }

        public Kasbon GenerateObjectViewR(SqlDataReader sqlDataReader)
        {
            objKasbon = new Kasbon();
            objKasbon.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objKasbon.KDID = Convert.ToInt32(sqlDataReader["KDID"]);
            objKasbon.NoSPP = sqlDataReader["DocumentNo"].ToString();
            objKasbon.NoPo = sqlDataReader["NoPO"].ToString();
            objKasbon.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objKasbon.EstimasiKasbon = Convert.ToDecimal(sqlDataReader["EstimasiKasbon"]);
            objKasbon.Total = Convert.ToDecimal(sqlDataReader["Total"]);
            objKasbon.Qty = Convert.ToDecimal(sqlDataReader["Qty"]);
            objKasbon.QtyPO = (sqlDataReader["QtyPO"] == DBNull.Value) ? 0 : Convert.ToDecimal(sqlDataReader["QtyPO"]);
            objKasbon.Price = (sqlDataReader["Price"] == DBNull.Value) ? 0 : Convert.ToDecimal(sqlDataReader["Price"]);
            objKasbon.TotalPO = (sqlDataReader["TotalPO"] == DBNull.Value) ? 0 : Convert.ToDecimal(sqlDataReader["TotalPO"]);
            objKasbon.Satuan = sqlDataReader["UOMDesc"].ToString();
            objKasbon.NoPengajuan = sqlDataReader["NoPengajuan"].ToString();
            objKasbon.NamaBarang = sqlDataReader["ItemName"].ToString();
            objKasbon.KasbonDate = Convert.ToDateTime(sqlDataReader["TglKasbon"]);
            objKasbon.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objKasbon.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objKasbon.Apv = Convert.ToInt32(sqlDataReader["Approval"]);
            objKasbon.DanaCadangan = Convert.ToDecimal(sqlDataReader["DanaCadangan"]);
            objKasbon.AlasanNotApproval = sqlDataReader["AlasanNotApproved"].ToString();

            return objKasbon;
        }
        public Kasbon GenerateObjectNoPO(SqlDataReader sqlDataReader)
        {
            objKasbon = new Kasbon();
            objKasbon.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objKasbon.NoPo = sqlDataReader["NoPO"].ToString();
            objKasbon.NamaBarang = sqlDataReader["ItemName"].ToString();
            objKasbon.Price = Convert.ToDecimal(sqlDataReader["Price"]);
            objKasbon.Qty = Convert.ToDecimal(sqlDataReader["Qty"]);
            return objKasbon;
        }
        public Kasbon GenerateObjectTeamKhusus(SqlDataReader sqlDataReader)
        {
            objKasbon = new Kasbon();
            objKasbon.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objKasbon.NoPo = sqlDataReader["NoPO"].ToString();
            objKasbon.NamaBarang = sqlDataReader["ItemName"].ToString();
            objKasbon.Price = Convert.ToDecimal(sqlDataReader["Harga"]);
            objKasbon.Qty = Convert.ToDecimal(sqlDataReader["HasilTimbang"]);
            return objKasbon;
        }

        private Kasbon GenerateObject(SqlDataReader sdr)
        {
            objKasbon = new Kasbon();
            objKasbon.ID = Convert.ToInt32(sdr["ID"]);
            objKasbon.UserName = sdr["UserName"].ToString();
            return objKasbon;
        }
        private Kasbon GenerateObjectKasbon(SqlDataReader sqlDataReader)
        {
            objKasbon = new Kasbon();
            objKasbon.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objKasbon.NoKasbon = sqlDataReader["KasbonNo"].ToString();
            return objKasbon;
        }
        public Kasbon GenerateObjectNew2(SqlDataReader sqlDataReader)
        {
            objKasbon = new Kasbon();
            objKasbon.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objKasbon.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            //objKasbon.UserName = sqlDataReader["UserName"].ToString();
            objKasbon.NoPengajuan = sqlDataReader["NoPengajuan"].ToString();
            objKasbon.KasbonDate = Convert.ToDateTime(sqlDataReader["TglKasbon"]);
            objKasbon.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objKasbon.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objKasbon.Apv = Convert.ToInt32(sqlDataReader["Approval"]);
            objKasbon.ApproveDate1 = (sqlDataReader["ApprovedDate1"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(sqlDataReader["ApprovedDate1"]);
            objKasbon.ApproveDate2 = (sqlDataReader["ApprovedDate2"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(sqlDataReader["ApprovedDate2"]);
            objKasbon.AlasanNotApproval = sqlDataReader["AlasanNotApproved"].ToString();
            objKasbon.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objKasbon.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objKasbon.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objKasbon.LastModifiedTime = (sqlDataReader["lastModifiedTime"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);

            return objKasbon;
        }

        public Kasbon GenerateObjectNo1(SqlDataReader sqlDataReader)
        {
            objKasbon = new Kasbon();
            objKasbon.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objKasbon.Apv = Convert.ToInt32(sqlDataReader["Approval"]);
            objKasbon.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objKasbon.Total = Convert.ToDecimal(sqlDataReader["Total"]);
            objKasbon.NoPengajuan = sqlDataReader["NoPengajuan"].ToString();
            return objKasbon;
        }
        public Kasbon GenerateObjectNew3(SqlDataReader sqlDataReader)
        {
            objKasbon = new Kasbon();
            objKasbon.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objKasbon.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            //objKasbon.UserName = sqlDataReader["UserName"].ToString();
            objKasbon.NoKasbon = sqlDataReader["KasbonNo"].ToString();
            objKasbon.KasbonDate = Convert.ToDateTime(sqlDataReader["TglKasbon"]);
            objKasbon.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objKasbon.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objKasbon.Apv = Convert.ToInt32(sqlDataReader["Approval"]);
            objKasbon.ApproveDate1 = (sqlDataReader["ApprovedDate1"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(sqlDataReader["ApprovedDate1"]);
            objKasbon.ApproveDate2 = (sqlDataReader["ApprovedDate2"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(sqlDataReader["ApprovedDate2"]);
            objKasbon.AlasanNotApproval = sqlDataReader["AlasanNotApproved"].ToString();
            objKasbon.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objKasbon.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objKasbon.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objKasbon.LastModifiedTime = (sqlDataReader["lastModifiedTime"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);

            return objKasbon;
        }
        public Kasbon GenerateObjectNoKasbon(SqlDataReader sqlDataReader)
        {
            objKasbon = new Kasbon();
            objKasbon.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objKasbon.Apv = Convert.ToInt32(sqlDataReader["Approval"]);
            objKasbon.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objKasbon.Total = Convert.ToDecimal(sqlDataReader["Total"]);
            objKasbon.NoKasbon = sqlDataReader["KasbonNo"].ToString();
            return objKasbon;
        }
        private Kasbon GenerateObjectID(SqlDataReader sqlDataReader)
        {
            objKasbon = new Kasbon();
            objKasbon.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objKasbon.Qty = Convert.ToDecimal(sqlDataReader["Qty"]);
            objKasbon.Price = Convert.ToDecimal(sqlDataReader["Price"]);
            objKasbon.EstimasiKasbon = (sqlDataReader["EstimasiKasbon"] == DBNull.Value) ? 0 : Convert.ToDecimal(sqlDataReader["TotalPO"]);
            objKasbon.TotalPO = Convert.ToDecimal(sqlDataReader["TotalPO"]);
            objKasbon.NamaBarang = sqlDataReader["ItemName"].ToString();
            objKasbon.NoPo = sqlDataReader["NoPO"].ToString();

            return objKasbon;
        }
        private Kasbon GenerateObjectIDItemKertas(SqlDataReader sqlDataReader)
        {
            objKasbon = new Kasbon();
            objKasbon.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objKasbon.QtyPO = Convert.ToDecimal(sqlDataReader["HasilTimbang"]);
            objKasbon.Price = Convert.ToDecimal(sqlDataReader["Harga"]);
            objKasbon.EstimasiKasbon = Convert.ToDecimal(sqlDataReader["EstimasiKasbon"]);
            objKasbon.TotalPO = Convert.ToDecimal(sqlDataReader["TotalPO"]);
            objKasbon.NamaBarang = sqlDataReader["ItemName"].ToString();
            objKasbon.NoPo = sqlDataReader["NoPO"].ToString();
            objKasbon.NoSPP = sqlDataReader["NoSPP"].ToString();
            objKasbon.Qty = Convert.ToDecimal(sqlDataReader["Qty"]);
            objKasbon.Total = Convert.ToDecimal(sqlDataReader["Total"]);
            objKasbon.Satuan = sqlDataReader["UOMDesc"].ToString();

            return objKasbon;
        }
        private Kasbon GenerateObjectIDInputPO(SqlDataReader sqlDataReader)
        {
            objKasbon = new Kasbon();
            objKasbon.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objKasbon.NoPo = sqlDataReader["NoPO"].ToString();
            objKasbon.NamaBarang = sqlDataReader["ItemName"].ToString();
            objKasbon.QtyPO = Convert.ToDecimal(sqlDataReader["Qty"]);
            objKasbon.Price = Convert.ToDecimal(sqlDataReader["Price"]);
            objKasbon.TotalPO = Convert.ToDecimal(sqlDataReader["TotalPO"]);
            objKasbon.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"]);

            return objKasbon;
        }
        private Kasbon GenerateObjectNamaBarangPO(SqlDataReader sqlDataReader)
        {
            objKasbon = new Kasbon();
            objKasbon.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objKasbon.NoSPP = sqlDataReader["NoSPP"].ToString();
            objKasbon.NoPo = sqlDataReader["NoPO"].ToString();
            objKasbon.NamaBarang = sqlDataReader["ItemName"].ToString();
            objKasbon.QtyPO = Convert.ToDecimal(sqlDataReader["QtyPO"]);
            objKasbon.Price = Convert.ToDecimal(sqlDataReader["Price"]);
            objKasbon.TotalPO = Convert.ToDecimal(sqlDataReader["TotalPO"]);
            objKasbon.Qty = Convert.ToDecimal(sqlDataReader["Qty"]);
            objKasbon.EstimasiKasbon = Convert.ToDecimal(sqlDataReader["EstimasiKasbon"]);
            objKasbon.Total = Convert.ToDecimal(sqlDataReader["Total"]);
            objKasbon.KasbonType = Convert.ToInt32(sqlDataReader["KasbonType"]);

            return objKasbon;
        }
        private Kasbon GenerateObjectID2(SqlDataReader sqlDataReader)
        {
            objKasbon = new Kasbon();
            objKasbon.TotalEstimasi = Convert.ToDecimal(sqlDataReader["TotalEstimasi"]);

            return objKasbon;
        }
        private Kasbon GenerateObjectID3(SqlDataReader sqlDataReader)
        {
            objKasbon = new Kasbon();
            objKasbon.TotalAllPO = Convert.ToDecimal(sqlDataReader["TotalAllPO"]);

            return objKasbon;
        }
        public Kasbon GenerateObjectNodetail(SqlDataReader sqlDataReader)
        {
            objKasbon = new Kasbon();
            objKasbon.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objKasbon.NoPengajuan = sqlDataReader["NoPengajuan"].ToString();
            objKasbon.KasbonDate = Convert.ToDateTime(sqlDataReader["TglKasbon"]);
            objKasbon.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objKasbon.KasbonType = Convert.ToInt32(sqlDataReader["KasbonType"]);
            return objKasbon;

        }



        public override int Delete(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }
        //public override int Update(DataAccessLayer.TransactionManager transManager)
        //{
        //    throw new NotImplementedException();
        //}

        //update hasan
        public static List<Kasbon> RetrieveNew()
        {

            List<Kasbon> alldata = new List<Kasbon>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string strSQL = "SELECT * FROM KasbonUser WHERE DeptID in (select ID from dept where deptname like '%purchasing%') AND RowStatus>-1 order by UserName";
                    alldata = connection.Query<Kasbon>(strSQL).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }

        public static List<Kasbon> RetrieveOSKasbonNew(string Tglawal, string Tglakhir, string Criteria)
        {
            List<Kasbon> alldata = new List<Kasbon>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string strSQL = "SELECT DISTINCT k.ID,kd.ID AS KDID,k.DeptID,u.UOMDesc,k.KasbonNo,k.NoPengajuan,s.NoSPP,kd.ItemName, " +
                            "kd.ItemID,kd.UomID,kd.EstimasiKasbon,kd.Qty,sd.Quantity,k.DanaCadangan,k.Pic, k.CreatedTime,k.TglKasbon, " +
                            "k.Approval,k.Status,k.AlasanNotApproved,(EstimasiKasbon*Qty) as Total FROM Kasbon as k left join KasbonDetail as kd on k.ID=kd.KID " +
                            "LEFT JOIN SPPDetail as sd on sd.ID=kd.SppDetailID LEFT JOIN SPP as s on s.ID=sd.SPPID LEFT JOIN uom AS u " +
                            "ON u.id=kd.UomID WHERE k.TglKasbon between '" + Tglawal + "' and '" + Tglakhir + "' and k.Status>-1 AND kd.Status>-1 and k.Approval!=4 " +
                            "" + Criteria + " ORDER BY k.ID DESC ";
                    alldata = connection.Query<Kasbon>(strSQL).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }
        //end update hasan
    }
}
