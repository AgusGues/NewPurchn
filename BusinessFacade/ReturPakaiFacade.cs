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
    public class ReturPakaiFacade : AbstractTransactionFacade
    {
        private ReturPakai objReturPakai = new ReturPakai();
        private ArrayList arrReturPakai;
        private List<SqlParameter> sqlListParam;

        public ReturPakaiFacade(object objDomain)
            : base(objDomain)
        {
            objReturPakai = (ReturPakai)objDomain;
        }

        public ReturPakaiFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ReturNo", objReturPakai.ReturNo));
                sqlListParam.Add(new SqlParameter("@PakaiID", objReturPakai.PakaiID));
                sqlListParam.Add(new SqlParameter("@PakaiNo", objReturPakai.PakaiNo));
                sqlListParam.Add(new SqlParameter("@ReturDate", objReturPakai.ReturDate));
                sqlListParam.Add(new SqlParameter("@DeptID", objReturPakai.DeptID));
                sqlListParam.Add(new SqlParameter("@DepoID", objReturPakai.DepoID));
                sqlListParam.Add(new SqlParameter("@Status", objReturPakai.Status));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objReturPakai.CreatedBy));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objReturPakai.ItemTypeID));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertReturPakai");

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
                sqlListParam.Add(new SqlParameter("@ID", objReturPakai.ID));
                sqlListParam.Add(new SqlParameter("@ReturNo", objReturPakai.ReturNo));
                sqlListParam.Add(new SqlParameter("@PakaiID", objReturPakai.PakaiID));
                sqlListParam.Add(new SqlParameter("@PakaiNo", objReturPakai.PakaiNo));
                sqlListParam.Add(new SqlParameter("@ReturDate", objReturPakai.ReturDate));
                sqlListParam.Add(new SqlParameter("@DeptID", objReturPakai.DeptID));
                sqlListParam.Add(new SqlParameter("@DepoID", objReturPakai.DepoID));
                sqlListParam.Add(new SqlParameter("@Status", objReturPakai.Status));
                sqlListParam.Add(new SqlParameter("@AlasanCancel", objReturPakai.AlasanCancel));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objReturPakai.CreatedBy));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objReturPakai.ItemTypeID));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateReturPakai");

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
                sqlListParam.Add(new SqlParameter("@ID", objReturPakai.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objReturPakai.LastModifiedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spCancelReturPakai");

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
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 50 A.ID,A.ScheduleNo,A.ScheduleDate,A.ExpedisiDetailID,B.ExpedisiName,C.CarType,A.TotalKubikasi,A.Status,A.Rate,A.Keterangan,A.DepoID,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from ReturPakai as A,Expedisi as B,ExpedisiDetail as C where A.ExpedisiDetailID = C.ID and C.ExpedisiID = B.ID and A.RowStatus = 0 order by A.ID");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 50 * from ReturPakai as A where A.Status>-1 order by ID");
            strError = dataAccess.Error;
            arrReturPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReturPakai.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrReturPakai.Add(new ReturPakai());

            return arrReturPakai;
        }

        public ArrayList RetrieveOpenStatus(string strTipePakai)
        {
            string tipeReturPakai = string.Empty;

            if (strTipePakai.Substring(1, 1) == "P")
                tipeReturPakai = "and A.PakaiTipe=2";
            else if (strTipePakai.Substring(1, 1) == "S")
                tipeReturPakai = "and A.PakaiTipe=1";
            else if (strTipePakai.Substring(1, 1) == "M")
                tipeReturPakai = "and A.PakaiTipe=3";
            else if (strTipePakai.Substring(1, 1) == "A")
                tipeReturPakai = "and A.PakaiTipe=4";
            else if (strTipePakai.Substring(1, 1) == "O")
                tipeReturPakai = "and A.PakaiTipe=7";
            else if (strTipePakai.Substring(1, 1) == "K")
                tipeReturPakai = "and A.PakaiTipe=8";

            //else if (strTipeReturPakai == "KC")
            //    tipeReturPakai = "and A.PakaiTipe=5";
            //else if (strTipeReturPakai == "KB")
            //    tipeReturPakai = "and A.PakaiTipe=6";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.PakaiNo,A.PakaiDate,A.DeptID,A.DepoID,A.PakaiTipe,A.CreatedBy,A.Status,B.ItemID,B.Quantity,B.Keterangan,B.UomID,C.ItemCode,C.ItemName,D.UOMCode from ReturPakai as A, PakaiDetail as B,Inventory as C, UOM as D where A.Status=0 and A.ID=B.PakaiID and B.ItemID=C.ID and B.RowStatus>-1 and B.UomID=D.ID " + tipeReturPakai + " order by A.PakaiNo desc");
            strError = dataAccess.Error;
            arrReturPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReturPakai.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrReturPakai.Add(new ReturPakai());

            return arrReturPakai;
        }

        public ArrayList AssetRetrieveOpenStatus(string strTipePakai)
        {
            string tipeReturPakai = string.Empty;

            if (strTipePakai.Substring(1, 1) == "C")
                tipeReturPakai = "and A.PakaiTipe=5";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.PakaiNo,A.PakaiDate,A.DeptID,A.DepoID,A.PakaiTipe,A.CreatedBy,A.Status,B.ItemID,B.Quantity,B.Keterangan,B.UomID,C.ItemCode,C.ItemName,D.UOMCode from ReturPakai as A, PakaiDetail as B,Asset as C, UOM as D where A.Status=0 and A.ID=B.PakaiID and B.ItemID=C.ID and B.RowStatus>-1 and B.UomID=D.ID " + tipeReturPakai + " order by A.PakaiNo desc");
            strError = dataAccess.Error;
            arrReturPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReturPakai.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrReturPakai.Add(new ReturPakai());

            return arrReturPakai;
        }

        public ArrayList BiayaRetrieveOpenStatus(string strTipePakai)
        {
            string tipeReturPakai = string.Empty;

            if (strTipePakai.Substring(1, 1) == "B")
                tipeReturPakai = "and A.PakaiTipe=6";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.PakaiNo,A.PakaiDate,A.DeptID,A.DepoID,A.PakaiTipe,A.CreatedBy,A.Status,B.ItemID,B.Quantity,B.Keterangan,B.UomID,C.ItemCode,C.ItemName,D.UOMCode from ReturPakai as A, PakaiDetail as B,Biaya as C, UOM as D where A.Status=0 and A.ID=B.PakaiID and B.ItemID=C.ID and B.RowStatus>-1 and B.UomID=D.ID " + tipeReturPakai + " order by A.PakaiNo desc");
            strError = dataAccess.Error;
            arrReturPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReturPakai.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrReturPakai.Add(new ReturPakai());

            return arrReturPakai;
        }

        public ArrayList RetrieveOpenStatusCriteria(string pakaiNo, string strTipePakai)
        {
            string tipeReturPakai = string.Empty;

            if (strTipePakai.Substring(1, 1) == "P")
                tipeReturPakai = "and A.PakaiTipe=2";
            else if (strTipePakai.Substring(1, 1) == "S")
                tipeReturPakai = "and A.PakaiTipe=1";
            else if (strTipePakai.Substring(1, 1) == "M")
                tipeReturPakai = "and A.PakaiTipe=3";
            else if (strTipePakai.Substring(1, 1) == "A")
                tipeReturPakai = "and A.PakaiTipe=4";
            else if (strTipePakai.Substring(1, 1) == "O")
                tipeReturPakai = "and A.PakaiTipe=7";
            else if (strTipePakai.Substring(1, 1) == "K")
                tipeReturPakai = "and A.PakaiTipe=8";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.PakaiNo,A.PakaiDate,A.DeptID,A.DepoID,A.PakaiTipe,A.CreatedBy,A.Status,B.ItemID,B.Quantity,B.Keterangan,B.UomID,C.ItemCode,C.ItemName,D.UOMCode from ReturPakai as A, PakaiDetail as B,Inventory as C, UOM as D where A.Status=0 and A.ID=B.PakaiID and B.ItemID=C.ID and B.RowStatus>-1 and B.UomID=D.ID " + tipeReturPakai + " and A.PakaiNo like '%" + pakaiNo + "%'");
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ReceiptNo,A.ReceiptDate,A.PONo,B.SPPNo,A.POID,A.SupplierId,A.Status,A.CreatedBy,B.ItemID,B.Quantity,C.ItemCode,C.ItemName from ReturPakai as A, ReceiptDetail as B,Inventory as C where A.Status=0 and A.ID=B.ReceiptID and B.ItemID=C.ID and A.ReceiptNo like '%" + pakaiNo + "%' " + tipePakai);
            strError = dataAccess.Error;
            arrReturPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReturPakai.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrReturPakai.Add(new ReturPakai());

            return arrReturPakai;
        }

        public ArrayList AssetRetrieveOpenStatusCriteria(string pakaiNo, string strTipePakai)
        {
            string tipeReturPakai = string.Empty;

            if (strTipePakai.Substring(1, 1) == "C")
                tipeReturPakai = "and A.PakaiTipe=5";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.PakaiNo,A.PakaiDate,A.DeptID,A.DepoID,A.PakaiTipe,A.CreatedBy,A.Status,B.ItemID,B.Quantity,B.Keterangan,B.UomID,C.ItemCode,C.ItemName,D.UOMCode from ReturPakai as A, PakaiDetail as B,Asset as C, UOM as D where A.Status=0 and A.ID=B.PakaiID and B.ItemID=C.ID and B.RowStatus>-1 and B.UomID=D.ID " + tipeReturPakai + " and A.PakaiNo like '%" + pakaiNo + "%'");
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ReceiptNo,A.ReceiptDate,A.PONo,B.SPPNo,A.POID,A.SupplierId,A.Status,A.CreatedBy,B.ItemID,B.Quantity,C.ItemCode,C.ItemName from ReturPakai as A, ReceiptDetail as B,Inventory as C where A.Status=0 and A.ID=B.ReceiptID and B.ItemID=C.ID and A.ReceiptNo like '%" + pakaiNo + "%' " + tipePakai);
            strError = dataAccess.Error;
            arrReturPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReturPakai.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrReturPakai.Add(new ReturPakai());

            return arrReturPakai;
        }

        public ArrayList BiayaRetrieveOpenStatusCriteria(string pakaiNo, string strTipePakai)
        {
            string tipeReturPakai = string.Empty;

            if (strTipePakai.Substring(1, 1) == "B")
                tipeReturPakai = "and A.PakaiTipe=6";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.PakaiNo,A.PakaiDate,A.DeptID,A.DepoID,A.PakaiTipe,A.CreatedBy,A.Status,B.ItemID,B.Quantity,B.Keterangan,B.UomID,C.ItemCode,C.ItemName,D.UOMCode from ReturPakai as A, PakaiDetail as B,Biaya as C, UOM as D where A.Status=0 and A.ID=B.PakaiID and B.ItemID=C.ID and B.RowStatus>-1 and B.UomID=D.ID " + tipeReturPakai + " and A.PakaiNo like '%" + pakaiNo + "%'");
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ReceiptNo,A.ReceiptDate,A.PONo,B.SPPNo,A.POID,A.SupplierId,A.Status,A.CreatedBy,B.ItemID,B.Quantity,C.ItemCode,C.ItemName from ReturPakai as A, ReceiptDetail as B,Inventory as C where A.Status=0 and A.ID=B.ReceiptID and B.ItemID=C.ID and A.ReceiptNo like '%" + pakaiNo + "%' " + tipePakai);
            strError = dataAccess.Error;
            arrReturPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReturPakai.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrReturPakai.Add(new ReturPakai());

            return arrReturPakai;
        }

        public ReturPakai CekLastDate(string strPakaiCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(max(ReturDate),GETDATE()) as ReturDate from ReturPakai where Status>-1 and left(ReturNo,4)='" + strPakaiCode + "'");
            strError = dataAccess.Error;
            arrReturPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectLastEntry(sqlDataReader);
                }
            }

            return new ReturPakai();
        }

        //public ReturPakai RetrieveByNo(string strPakaiNo)
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select ID,ReceiptNo,ReceiptDate,ReceiptType,ReceiptType,PONo,POID,SupplierId,Status,CreatedTime,CreatedBy,LastModifiedBy,LastModifiedTime from ReturPakai where Status>-1 and PakaiNo='" + strPakaiNo + "'");
        //    strError = dataAccess.Error;
        //    arrReturPakai = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            return GenerateObject(sqlDataReader);
        //        }
        //    }

        //    return new ReturPakai();
        //}

        public ArrayList RetrieveByKurangTgl(int tgl, int bln, int thn, int itemTypeID)
        {

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ReturNo,A.ReturDate,A.DeptID,A.DepoID,A.Status,A.CreatedBy,B.DeptCode,B.DeptName,A.ItemTypeID,A.ItemTypeID from ReturPakai as A, Dept as B where A.Status>-1 and A.DeptID=B.ID and day(A.ReturDate) <" + tgl + " and month(A.ReturDate) =" + bln + " and year(A.ReturDate) =" + thn + " and A.ItemTypeID =" + itemTypeID);
            strError = dataAccess.Error;
            arrReturPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReturPakai.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrReturPakai.Add(new ReturPakai());

            return arrReturPakai;
        }
        public ArrayList RetrieveBySamaTgl(int tgl, int bln, int thn, int itemTypeID)
        {

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ReturNo,A.ReturDate,A.DeptID,A.DepoID,A.Status,A.CreatedBy,B.DeptCode,B.DeptName,A.ItemTypeID,A.ItemTypeID from ReturPakai as A, Dept as B where A.Status>-1 and A.DeptID=B.ID and day(A.ReturDate) =" + tgl + " and month(A.ReturDate) =" + bln + " and year(A.ReturDate) =" + thn + " and A.ItemTypeID =" + itemTypeID);
            strError = dataAccess.Error;
            arrReturPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReturPakai.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrReturPakai.Add(new ReturPakai());

            return arrReturPakai;
        }
        public ArrayList RetrieveByTgl(int tgl, int bln, int thn)
        {

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ReturNo,A.ReturDate,A.DeptID,A.DepoID,A.Status,A.CreatedBy,B.DeptCode,B.DeptName,A.ItemTypeID,A.ItemTypeID from ReturPakai as A, Dept as B where A.Status>-1 and A.DeptID=B.ID and day(A.ReturDate) <=" + tgl + " and month(A.ReturDate) =" + bln + " and year(A.ReturDate) =" + thn);
            strError = dataAccess.Error;
            arrReturPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReturPakai.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrReturPakai.Add(new ReturPakai());

            return arrReturPakai;
        }

        public ArrayList RetrieveByPeriode(string drTgl, string sdTgl, int itemTypeID)
        {

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ReturNo,A.ReturDate,A.DeptID,A.DepoID,A.Status,A.CreatedBy,B.DeptCode,B.DeptName,A.ItemTypeID,A.ItemTypeID from ReturPakai as A, Dept as B where A.Status>-1 and A.DeptID=B.ID and convert(varchar,A.ReturDate,112) >= '" + drTgl + "' and convert(varchar,A.ReturDate,112) <= '" + sdTgl + "' and A.ItemTypeID = " + itemTypeID);
            strError = dataAccess.Error;
            arrReturPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReturPakai.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrReturPakai.Add(new ReturPakai());

            return arrReturPakai;
        }

        public ArrayList RetrieveOpenStatusForAll(string thbl, int itemTypeID)
        {

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ReturNo,A.PakaiNo, A.ReturDate,A.DeptID,A.DepoID,A.Status,A.CreatedBy,B.DeptCode,B.DeptName,A.ItemTypeID from ReturPakai as A, Dept as B where A.Status>-1 and A.DeptID=B.ID and LEFT(convert(varchar,A.ReturDate,112),6) = '" + thbl + "' and A.ItemTypeID=" + itemTypeID);
            strError = dataAccess.Error;
            arrReturPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReturPakai.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrReturPakai.Add(new ReturPakai());

            return arrReturPakai;
        }

        public ReturPakai RetrieveByNoWithStatus(string strPakaiNo, string strPakaiTipe)
        {
            string pakaiTipe = string.Empty;

            if (strPakaiTipe.Substring(1, 1) == "P")
                pakaiTipe = "and A.PakaiTipe=1";
            else if (strPakaiTipe.Substring(1, 1) == "S")
                pakaiTipe = "and A.PakaiTipe in (8,9)";
            else if (strPakaiTipe.Substring(1, 1) == "M")
                pakaiTipe = "and A.PakaiTipe=2";
            else if (strPakaiTipe.Substring(1, 1) == "A")
                pakaiTipe = "and A.PakaiTipe=3";
            else if (strPakaiTipe.Substring(1, 1) == "O")
                pakaiTipe = "and A.PakaiTipe=6";
            else if (strPakaiTipe.Substring(1, 1) == "K")
                pakaiTipe = "and A.PakaiTipe in (7,10)";
            else if (strPakaiTipe.Substring(1, 1) == "C")
                pakaiTipe = "and A.PakaiTipe=4";
            else if (strPakaiTipe.Substring(1, 1) == "B")
                pakaiTipe = "and A.PakaiTipe=5";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,a.PakaiNo,A.PakaiDate,A.DeptID,A.DepoID,A.PakaiTipe,A.Status,A.AlasanCancel,A.CreatedBy,B.DeptCode,B.DeptName,A.ItemTypeID from ReturPakai as A,Dept as B where A.Status=0 and A.PakaiNo='" + strPakaiNo + "' " + pakaiTipe);

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ReturNo,A.PakaiNo,A.ReturDate, " +
            "A.DeptID,A.DepoID,A.Status,A.AlasanCancel,A.CreatedBy,B.DeptCode,B.DeptName,A.ItemTypeID " +
            "from ReturPakai as A,Dept as B,ReturPakaiDetail as C where A.ID = C.ReturID and A.DeptID = B.ID " +
            "and A.Status=0 and A.ReturNo='" + strPakaiNo + "' " + pakaiTipe) ;
            strError = dataAccess.Error;
            arrReturPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new ReturPakai();
        }

        public ReturPakai RetrieveByNoWithStatus2(string strPakaiNo, string strPakaiTipe)
        {
            string pakaiTipe = string.Empty;

            if (strPakaiTipe.Substring(1, 1) == "P")
                pakaiTipe = "and C.GroupID=1";
            else if (strPakaiTipe.Substring(1, 1) == "S")
                pakaiTipe = "and C.GroupID in (8,9)";
            else if (strPakaiTipe.Substring(1, 1) == "M")
                pakaiTipe = "and C.GroupID=2";
            else if (strPakaiTipe.Substring(1, 1) == "A")
                pakaiTipe = "and C.GroupID=3";
            else if (strPakaiTipe.Substring(1, 1) == "O")
                pakaiTipe = "and C.GroupID=6";
            else if (strPakaiTipe.Substring(1, 1) == "K")
                pakaiTipe = "and C.GroupID in (7,10)";
            else if (strPakaiTipe.Substring(1, 1) == "C")
                pakaiTipe = "and C.GroupID=4";
            else if (strPakaiTipe.Substring(1, 1) == "B")
                pakaiTipe = "and C.GroupID=5";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ReturNo,A.PakaiNo,A.ReturDate, " +
            "A.DeptID,A.DepoID,A.Status,A.AlasanCancel,A.CreatedBy,B.DeptCode,B.DeptName,A.ItemTypeID " +
            "from ReturPakai as A,Dept as B,ReturPakaiDetail as C where A.ID = C.ReturID and A.DeptID = B.ID " +
            "and A.Status=0 and A.ReturNo='" + strPakaiNo + "' " + pakaiTipe);
            strError = dataAccess.Error;
            arrReturPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new ReturPakai();
        }

        public ArrayList RetrieveByKurangTgl2(int tgl, int bln, int thn, int itemTypeID)
        {

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ReturNo,A.ReturDate,A.DeptID,A.DepoID,A.Status,A.CreatedBy,B.DeptCode,B.DeptName,A.ItemTypeID,A.ItemTypeID from ReturPakai as A, Dept as B where A.Status>-1 and A.DeptID=B.ID and day(A.ReturDate) <" + tgl + " and month(A.ReturDate) =" + bln + " and year(A.ReturDate) =" + thn + " and A.ItemTypeID =" + itemTypeID);
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select distinct A.ID,A.ReturNo,A.ReturDate,A.DeptID,A.DepoID,A.Status,A.CreatedBy,C.DeptCode,C.DeptName,A.ItemTypeID,A.ItemTypeID " +
                "from ReturPakai as A, ReturDetail as B,Dept as C where A.ID=B.ReturID and A.Status>-1 and A.DeptID=C.ID and " +
                "day(A.ReturDate) <" + tgl + " and month(A.ReturDate) =" + bln + " and year(A.ReturDate) =" + thn + " and A.ItemTypeID =" + itemTypeID);
            strError = dataAccess.Error;
            arrReturPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReturPakai.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrReturPakai.Add(new ReturPakai());

            return arrReturPakai;
        }

        public ArrayList RetrieveBySamaTgl2(int tgl, int bln, int thn, int itemTypeID)
        {

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ReturNo,A.ReturDate,A.DeptID,A.DepoID,A.Status,A.CreatedBy,B.DeptCode,B.DeptName,A.ItemTypeID,A.ItemTypeID from ReturPakai as A, Dept as B where A.Status>-1 and A.DeptID=B.ID and day(A.ReturDate) =" + tgl + " and month(A.ReturDate) =" + bln + " and year(A.ReturDate) =" + thn + " and A.ItemTypeID =" + itemTypeID);
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select distinct A.ID,A.ReturNo,A.PakaiNo,A.ReturDate,A.DeptID,A.DepoID,A.Status,A.CreatedBy,C.DeptCode,C.DeptName,A.ItemTypeID,A.ItemTypeID " +
                "from ReturPakai as A, ReturDetail as B,Dept as C where A.ID=B.ReturID and A.Status>-1 and A.DeptID=C.ID and " +
                "day(A.ReturDate)=" + tgl + " and month(A.ReturDate) =" + bln + " and year(A.ReturDate) =" + thn + " and A.ItemTypeID =" + itemTypeID);
            strError = dataAccess.Error;
            arrReturPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReturPakai.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrReturPakai.Add(new ReturPakai());

            return arrReturPakai;
        }


        public ReturPakai GenerateObject(SqlDataReader sqlDataReader)
        {
            objReturPakai = new ReturPakai();
            objReturPakai.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objReturPakai.PakaiNo = sqlDataReader["PakaiNo"].ToString();
            objReturPakai.ReturNo = sqlDataReader["ReturNo"].ToString();
            objReturPakai.ReturDate = Convert.ToDateTime(sqlDataReader["ReturDate"]);
            objReturPakai.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objReturPakai.DepoID = Convert.ToInt32(sqlDataReader["DepoID"]);
            objReturPakai.Status = Convert.ToInt32(sqlDataReader["Status"]);
            //objReturPakai.PakaiTipe = Convert.ToInt32(sqlDataReader["PakaiTipe"]);
            objReturPakai.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objReturPakai.DeptCode = sqlDataReader["DeptCode"].ToString();
            objReturPakai.DeptName = sqlDataReader["DeptName"].ToString();

            objReturPakai.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"]);

            return objReturPakai;
        }

        public ReturPakai GenerateObjectLastEntry(SqlDataReader sqlDataReader)
        {
            objReturPakai = new ReturPakai();
            objReturPakai.ReturDate = Convert.ToDateTime(sqlDataReader["ReturDate"]);
            return objReturPakai;
        }

        public ReturPakai GenerateObjectList(SqlDataReader sqlDataReader)
        {
            objReturPakai = new ReturPakai();
            objReturPakai.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objReturPakai.PakaiNo = sqlDataReader["PakaiNo"].ToString();
            objReturPakai.ReturDate = Convert.ToDateTime(sqlDataReader["ReturDate"]);
            objReturPakai.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objReturPakai.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objReturPakai.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objReturPakai.DepoID = Convert.ToInt32(sqlDataReader["DepoID"]);
            objReturPakai.PakaiTipe = Convert.ToInt32(sqlDataReader["PakaiTipe"]);
            objReturPakai.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objReturPakai.Quantity = Convert.ToDecimal(sqlDataReader["Quantity"]);
            objReturPakai.Keterangan = sqlDataReader["Keterangan"].ToString();
            objReturPakai.UomID = Convert.ToInt32(sqlDataReader["UomID"]);
            objReturPakai.UomCode = sqlDataReader["UomCode"].ToString();
            objReturPakai.ItemCode = sqlDataReader["ItemCode"].ToString();
            objReturPakai.ItemName = sqlDataReader["ItemName"].ToString();

            return objReturPakai;
        }

    }
}
