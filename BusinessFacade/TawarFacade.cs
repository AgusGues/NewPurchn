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
    public class TawarFacade : AbstractTransactionFacade
    {
        private Tawar objTawar = new Tawar();
        private ArrayList arrTawar;
        private List<SqlParameter> sqlListParam;

        public TawarFacade(object objDomain)
            : base(objDomain)
        {
            objTawar = (Tawar)objDomain;
        }

        public TawarFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@NoTawar", objTawar.NoPO));
                sqlListParam.Add(new SqlParameter("@TglTawar", objTawar.POPurchnDate));
                sqlListParam.Add(new SqlParameter("@SupplierID", objTawar.SupplierID));
                sqlListParam.Add(new SqlParameter("@SPPID", objTawar.SPPID));
                sqlListParam.Add(new SqlParameter("@SPPDetailID", objTawar.SPPDetailID));
                sqlListParam.Add(new SqlParameter("@ItemID", objTawar.ItemID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objTawar.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@Qty", objTawar.Qty));
                sqlListParam.Add(new SqlParameter("@GroupID", objTawar.GroupID));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objTawar.CreatedBy));
               
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertTawar");

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
                sqlListParam.Add(new SqlParameter("@ID", objTawar.ID));
                sqlListParam.Add(new SqlParameter("@NoTawar", objTawar.NoPO));
                sqlListParam.Add(new SqlParameter("@TglTawar", objTawar.POPurchnDate));
                sqlListParam.Add(new SqlParameter("@SupplierID", objTawar.SupplierID));
                sqlListParam.Add(new SqlParameter("@SPPID", objTawar.SPPID));
                sqlListParam.Add(new SqlParameter("@SPPDetailID", objTawar.SPPDetailID));
                sqlListParam.Add(new SqlParameter("@ItemID", objTawar.ItemID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objTawar.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@Qty", objTawar.Qty));
                sqlListParam.Add(new SqlParameter("@GroupID", objTawar.GroupID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objTawar.LastModifiedBy));
                
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateTawar");

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
                sqlListParam.Add(new SqlParameter("@ID", objTawar.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objTawar.LastModifiedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteTawar");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 50 A.ID,A.NoPO,A.POPurchnDate,A.CreatedTime,A.SupplierID,A.Termin,A.PPN,A.Delivery,A.Crc,A.Keterangan,A.Terbilang,A.Disc,A.PPH,A.NilaiKurs,A.Cetak,A.CountPrt,A.Status,A.Approval,A.ApproveDate1,A.ApproveDate2,A.CreatedBy,A.LastModifiedBy,A.LastModifiedTime,A.AlasanBatal,A.AlasanClose,A.Indent from POPurchn as A order by A.ID desc");
            strError = dataAccess.Error;
            arrTawar = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTawar.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTawar.Add(new Tawar());

            return arrTawar;
        }

        //new
        public ArrayList ViewGridPO(int id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "select B.ID,A.ID as TawarID,A.NoTawar,A.TglTawar,C.SupplierName,D.NoSPP, " +
            "case B.ItemTypeID  " +
            "when 1 then (select ItemCode from Inventory where ID = B.ItemID) " +
            "when 2 then (select ItemCode from Asset where ID = B.ItemID) " +
            "when 3 then (select ItemCode from Biaya where ID = B.ItemID) end ItemCode, " +
            "case B.ItemTypeID " +
            "when 1 then (select ItemName from Inventory where ID = B.ItemID) " +
            "when 2 then (select ItemName from Asset where ID = B.ItemID) " +
            "when 3 then (select ItemName from Biaya where ID = B.ItemID) end ItemName, " +
            "B.Qty,F.UOMCode " +
            "from Tawar as A,TawarDetail as B,SuppPurch as C,SPP as D,SPPDetail as E, UOM as F " +
            "where A.ID = B.TawarID and A.SupplierID = C.ID and B.SPPID = D.ID " +
            "and B.SPPDetailID = E.ID and B.UOMID = F.ID and A.ID = " + id;
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);

            strError = dataAccess.Error;
            arrTawar = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTawar.Add(GenerateObjectViewPO(sqlDataReader));
                }
            }
            else
                arrTawar.Add(new Tawar());

            return arrTawar;
        }

        public ArrayList ViewGridPOWithApproval(int approval, int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.POID,B.NoPO,B.Termin,B.Delivery,B.Disc,B.PPH,B.PPN,B.Crc,C.NoSPP,D.UOMCode as Satuan,E.SupplierName,E.UP,E.Telepon,(A.Qty * A.Price) as Jumlah," +
                "E.Fax,A.SPPID,A.GroupID,A.ItemID,A.Price,A.Qty,A.ItemTypeID,A.UOMID,B.Status,A.NoUrut,A.SPPDetailID,A.DocumentNo, " +
                "case A.ItemTypeID when 1  then (select ItemName from Inventory where ID=A.ItemID and RowStatus > -1) " +
                "when 2 then (select ItemName from Asset where ID=A.ItemID and RowStatus > -1) " +
                "else (select ItemName from Biaya where ID=A.ItemID and RowStatus > -1) end NamaBarang, " +
                "case A.ItemTypeID when 1  then (select ItemCode from Inventory where ID=A.ItemID and RowStatus > -1) " +
                "when 2 then (select ItemCode from Asset where ID=A.ItemID and RowStatus > -1) " +
                "else (select ItemCode from Biaya where ID=A.ItemID and RowStatus > -1) end ItemCode " +
                "from POPurchnDetail as A,POPurchn as B,SPP as C,UOM as D,SuppPurch as E " +
                "where A.POID = B.ID and A.SPPID = C.ID and A.UOMID = D.ID and B.SupplierID = E.ID and B.Status=0" +
                "and B.Approval=" + approval + " and C.DepoID=" + depoID);

            strError = dataAccess.Error;
            arrTawar = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTawar.Add(GenerateObjectViewPO(sqlDataReader));
                }
            }
            else
                arrTawar.Add(new Tawar());

            return arrTawar;
        }

        public ArrayList ViewGridPOWithApprovalByNo(int approval, int depoID, string noPO)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.POID,B.NoPO,B.Termin,B.Delivery,B.Disc,B.PPH,B.PPN,B.Crc,C.NoSPP,D.UOMCode as Satuan,E.SupplierName,E.UP,E.Telepon,(A.Qty * A.Price) as Jumlah," +
                "E.Fax,A.SPPID,A.GroupID,A.ItemID,A.Price,A.Qty,A.ItemTypeID,A.UOMID,B.Status,A.NoUrut,A.SPPDetailID,A.DocumentNo, " +
                "case A.ItemTypeID when 1  then (select ItemName from Inventory where ID=A.ItemID and RowStatus > -1) " +
                "when 2 then (select ItemName from Asset where ID=A.ItemID and RowStatus > -1) " +
                "else (select ItemName from Biaya where ID=A.ItemID and RowStatus > -1) end NamaBarang, " +
                "case A.ItemTypeID when 1  then (select ItemCode from Inventory where ID=A.ItemID and RowStatus > -1) " +
                "when 2 then (select ItemCode from Asset where ID=A.ItemID and RowStatus > -1) " +
                "else (select ItemCode from Biaya where ID=A.ItemID and RowStatus > -1) end ItemCode " +
                "from POPurchnDetail as A,POPurchn as B,SPP as C,UOM as D,SuppPurch as E " +
                "where A.POID = B.ID and A.SPPID = C.ID and A.UOMID = D.ID and B.SupplierID = E.ID and B.Status=0" +
                "and B.Approval=" + approval + " and C.DepoID=" + depoID + " and B.NoPO='" + noPO + "'");

            strError = dataAccess.Error;
            arrTawar = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTawar.Add(GenerateObjectViewPO(sqlDataReader));
                }
            }
            else
                arrTawar.Add(new Tawar());

            return arrTawar;
        }

        public Tawar ViewPO(int id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.POID,B.NoPO,B.Termin,B.Delivery,B.Disc,B.PPH,B.PPN,B.Crc,C.NoSPP,D.UOMCode as Satuan,E.SupplierName,E.UP,E.Telepon,(A.Qty * A.Price) as Jumlah, " +
                                                                          "E.Fax,A.SPPID,A.GroupID,A.ItemID,A.Price,A.Qty,A.ItemTypeID,A.UOMID,A.Status,A.NoUrut,A.SPPDetailID,A.DocumentNo, " +
                                                                          "case A.ItemTypeID when 1  then (select ItemName from Inventory where ID=A.ItemID and RowStatus > -1) " +
                                                                          "when 2 then (select ItemName from Asset where ID=A.ItemID and RowStatus > -1) " +
                                                                          "else (select ItemName from Biaya where ID=A.ItemID and RowStatus > -1) end NamaBarang, " +
                                                                          "case A.ItemTypeID when 1  then (select ItemCode from Inventory where ID=A.ItemID and RowStatus > -1) " +
                                                                          "when 2 then (select ItemCode from Asset where ID=A.ItemID and RowStatus > -1) " +
                                                                          "else (select ItemCode from Biaya where ID=A.ItemID and RowStatus > -1) end ItemCode " +
                                                                          "from POPurchnDetail as A,POPurchn as B,SPP as C,UOM as D,SuppPurch as E " +
                                                                          "where A.POID = B.ID and A.SPPID = C.ID and A.UOMID = D.ID and B.SupplierID = E.ID and A.POID = " + id);
            strError = dataAccess.Error;
            arrTawar = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectViewPO(sqlDataReader);
                }
            }

            return new Tawar();
        }
        //new

        public ArrayList RetrieveOpenStatus()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 50 A.ID,A.NoPO,A.POPurchnDate,A.CreatedTime,A.SupplierID,A.Termin,A.PPN,A.Delivery,A.Crc,A.Keterangan,A.Terbilang,A.Disc,A.PPH,A.NilaiKurs,A.Cetak,A.CountPrt,A.Status,A.Approval,A.ApproveDate1,A.ApproveDate2,A.CreatedBy,A.LastModifiedBy,A.LastModifiedTime,A.AlasanBatal,A.AlasanClose from POPurchn as A where A.Approval = 0 and A.Status = 0 order by A.ID desc");
            strError = dataAccess.Error;
            arrTawar = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTawar.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTawar.Add(new Tawar());

            return arrTawar;
        }


        public ArrayList RetrieveOpenStatusByDepo(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.NoPO,A.POPurchnDate,A.CreatedTime,A.SupplierID,A.Termin,A.PPN,A.Delivery,A.Crc,A.Keterangan,A.Terbilang,A.Disc,A.PPH,A.NilaiKurs,A.Cetak,A.CountPrt,A.Status,A.Approval,A.ApproveDate1,A.ApproveDate2,A.CreatedBy,A.LastModifiedBy,A.LastModifiedTime,A.AlasanBatal,A.AlasanClose,B.DepoID,A.Indent from POPurchn as A,SPP as B,POPurchnDetail as C where C.SPPID = B.ID and C.POID = A.ID and A.Approval = 0 and A.Status = 0 and B.DepoID = " + depoID + " order by A.ID desc");
            strError = dataAccess.Error;
            arrTawar = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTawar.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTawar.Add(new Tawar());

            return arrTawar;
        }

        public ArrayList RetrieveAllByDepo(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 50 A.ID,A.NoPO,A.POPurchnDate,A.CreatedTime,A.SupplierID,A.Termin,A.PPN,A.Delivery,A.Crc,A.Keterangan,A.Terbilang,A.Disc,A.PPH,A.NilaiKurs,A.Cetak,A.CountPrt,A.Status,A.Approval,A.ApproveDate1,A.ApproveDate2,A.CreatedBy,A.LastModifiedBy,A.LastModifiedTime,A.AlasanBatal,A.AlasanClose,B.DepoID,A.Indent from POPurchn as A,SPP as B,POPurchnDetail as C where C.SPPID = B.ID and C.POID = A.ID and B.DepoID = " + depoID + " order by A.ID desc");
            strError = dataAccess.Error;
            arrTawar = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTawar.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTawar.Add(new Tawar());

            return arrTawar;
        }

        public ArrayList RetrieveAllPO()
        {
            string strGroupID = string.Empty;
            //if (groupID == 1 || groupID == 2)
            //{
            //    strGroupID = " and A.GroupID in (1,2)";
            //}
            //if (groupID == 3)
            //{
            //    strGroupID = " and A.GroupID in (3)";
            //}
            //if (groupID == 4)
            //{
            //    strGroupID = " and A.GroupID in (4)";
            //}
            //if (groupID == 5)
            //{
            //    strGroupID = " and A.GroupID in (5)";
            //}
            //if (groupID == 6 || groupID == 7 || groupID == 8 || groupID == 9)
            //{
            //    strGroupID = " and A.GroupID in (6,7,8,9)";
            //}

            //string strCariNoPO = string.Empty;
            //if (strNoPo != string.Empty)
            //    strCariNoPO = strField + "'%" + strNoPo + "%'";


            string strsql = "select CONVERT(varchar(11),TglTawar,113) as TglPenawaran,C.SupplierName as NamaSupplier,C.UP,C.Fax,C.Telepon as Telp, " +
                "A.NoTawar as NoPO,DATENAME(month,A.TglTawar)+' '+DATENAME(YEAR,A.TglTawar) as BulanKirim,D.NoSPP, " +
                "case B.ItemTypeID " +
                "when 1 then(select ItemName from Inventory where ID = B.ItemID) " +
                "when 2 then(select ItemName from Asset where ID = B.ItemID) " +
                "when 3 then(select ItemName from Biaya where ID = B.ItemID) end NamaBarang, " +
                "case B.ItemTypeID " +
                "when 1 then(select ItemCode from Inventory where ID = B.ItemID) " +
                "when 2 then(select ItemCode from Asset where ID = B.ItemID) " +
                "when 3 then(select ItemCode from Biaya where ID = B.ItemID) end ItemCode, " +
                "B.Qty,E.UOMCode as Satuan,0 as price " +
                "from Tawar as A,TawarDetail as B,SuppPurch as C,SPP as D,UOM as E " +
                "where A.ID = B.TawarID and A.SupplierID = C.ID and B.SPPID = D.ID and B.UOMID = E.ID and A.SupplierID>0 order by A.ID desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrTawar = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTawar.Add(GenerateObjectAllPO(sqlDataReader));
                }
            }
            else
                arrTawar.Add(new Tawar());

            return arrTawar;
        }

        public ArrayList RetrieveApproveStatus()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.NoPO,A.POPurchnDate,A.CreatedTime,A.SupplierID,A.Termin,A.PPN,A.Delivery,A.Crc,A.Keterangan,A.Terbilang,A.Disc,A.PPH,A.NilaiKurs,A.Cetak,A.CountPrt,A.Status,A.Approval,A.ApproveDate1,A.ApproveDate2,A.CreatedBy,A.LastModifiedBy,A.LastModifiedTime,A.AlasanBatal,A.AlasanClose,A.Indent from POPurchn as A where A.Approval > 0 and A.Status > 0 order by A.ID desc");
            strError = dataAccess.Error;
            arrTawar = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTawar.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTawar.Add(new Tawar());

            return arrTawar;
        }

        public ArrayList RetrieveOpenStatusByDepo(int Approval, int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.NoPO,A.POPurchnDate,A.CreatedTime,A.SupplierID,A.Termin,A.PPN,A.Delivery,A.Crc,A.Keterangan,A.Terbilang,A.Disc,A.PPH,A.NilaiKurs,A.Cetak,A.CountPrt,A.Status,A.Approval,A.ApproveDate1,A.ApproveDate2,A.CreatedBy,A.LastModifiedBy,A.LastModifiedTime,A.AlasanBatal,A.AlasanClose,B.DepoID,A.Indent from POPurchn as A,SPP as B,POPurchnDetail as C where C.SPPID = B.ID and C.POID = A.ID and A.Approval = " + Approval + " and A.Status = 0 and B.DepoID = " + depoID + " order by A.ID desc");
            strError = dataAccess.Error;
            arrTawar = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTawar.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTawar.Add(new Tawar());

            return arrTawar;
        }

        public ArrayList RetrieveOpenApproval(int Approval)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.NoPO,A.POPurchnDate,A.CreatedTime,A.SupplierID,A.Termin,A.PPN,A.Delivery,A.Crc,A.Keterangan,A.Terbilang,A.Disc,A.PPH,A.NilaiKurs,A.Cetak,A.CountPrt,A.Status,A.Approval,A.ApproveDate1,A.ApproveDate2,A.CreatedBy,A.LastModifiedBy,A.LastModifiedTime,A.AlasanBatal,A.AlasanClose,A.Indent from POPurchn as A where A.Approval =" + Approval + " and A.Status = 0 order by A.ID desc");
            strError = dataAccess.Error;
            arrTawar = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTawar.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTawar.Add(new Tawar());

            return arrTawar;
        }

        public ArrayList RetrieveOpenApprovalByNo(int Approval, string noPO)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.NoPO,A.POPurchnDate,A.CreatedTime,A.SupplierID,A.Termin,A.PPN,A.Delivery,A.Crc,A.Keterangan,A.Terbilang,A.Disc,A.PPH,A.NilaiKurs,A.Cetak,A.CountPrt,A.Status,A.Approval,A.ApproveDate1,A.ApproveDate2,A.CreatedBy,A.LastModifiedBy,A.LastModifiedTime,A.AlasanBatal,A.AlasanClose,A.Indent from POPurchn as A where A.Approval =" + Approval + " and A.Status = 0 and A.NoPO='" + noPO + "' order by A.ID desc");
            strError = dataAccess.Error;
            arrTawar = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTawar.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTawar.Add(new Tawar());

            return arrTawar;
        }

        public ArrayList RetrieveApproveStatusByDepo(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.NoPO,A.POPurchnDate,A.CreatedTime,A.SupplierID,A.Termin,A.PPN,A.Delivery,A.Crc,A.Keterangan,A.Terbilang,A.Disc,A.PPH,A.NilaiKurs,A.Cetak,A.CountPrt,A.Status,A.Approval,A.ApproveDate1,A.ApproveDate2,A.CreatedBy,A.LastModifiedBy,A.LastModifiedTime,A.AlasanBatal,A.AlasanClose,B.DepoID,A.Indent from POPurchn as A,SPP as B,POPurchnDetail as C where A.Approval > 0 and A.Status > 0 and B.DepoID = " + depoID + " order by A.ID desc");
            strError = dataAccess.Error;
            arrTawar = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTawar.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTawar.Add(new Tawar());

            return arrTawar;
        }

        public Tawar RetrieveByNo(string noPO)
        {
            string strSQL = "select A.ID,A.NoTawar,A.TglTawar,A.SupplierID from Tawar as A where A.NoTawar = '" + noPO + "' order by ID desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.NoPO,A.POPurchnDate,A.CreatedTime,A.SupplierID,A.Termin,A.PPN,A.Delivery,A.Crc,A.Keterangan,A.Terbilang,A.Disc,A.PPH,A.NilaiKurs,A.Cetak,A.CountPrt,A.Status,A.Approval,A.ApproveDate1,A.ApproveDate2,A.CreatedBy,A.LastModifiedBy,A.LastModifiedTime,A.AlasanBatal,A.AlasanClose,B.DepoID from POPurchn as A,SPP as B,POPurchnDetail as C where C.SPPID = B.ID and C.POID = A.ID and A.NoPO = " + noPO + " order by A.ID desc");
            strError = dataAccess.Error;
            arrTawar = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new Tawar();
        }

        public Tawar RetrieveByNoWithDepo(string noPO, int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.NoPO,A.POPurchnDate,A.CreatedTime,A.SupplierID,A.Termin,A.PPN,A.Delivery,A.Crc,A.Keterangan,A.Terbilang,A.Disc,A.PPH,A.NilaiKurs,A.Cetak,A.CountPrt,A.Status,A.Approval,A.ApproveDate1,A.ApproveDate2,A.CreatedBy,A.LastModifiedBy,A.LastModifiedTime,A.AlasanBatal,A.AlasanClose,B.DepoID,A.Indent from POPurchn as A,SPP as B,POPurchnDetail as C where C.SPPID = B.ID and C.POID = A.ID and A.NoPO = '" + noPO + "'  and B.DepoID = " + depoID + " order by A.ID desc");
            strError = dataAccess.Error;
            arrTawar = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Tawar();
        }

        public Tawar RetrieveByID(int id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.NoPO,A.POPurchnDate,A.CreatedTime,A.SupplierID,A.Termin,A.PPN,A.Delivery,A.Crc,A.Keterangan,A.Terbilang,A.Disc,A.PPH,A.NilaiKurs,A.Cetak,A.CountPrt,A.Status,A.Approval,A.ApproveDate1,A.ApproveDate2,A.CreatedBy,A.LastModifiedBy,A.LastModifiedTime,A.AlasanBatal,A.AlasanClose,B.DepoID from POPurchn as A,SPP as B,POPurchnDetail as C where C.SPPID = B.ID and C.POID = A.ID and A.ID = " + id + " order by A.ID desc");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from Tawar where ID = " + id);
            strError = dataAccess.Error;
            arrTawar = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Tawar();
        }

        //
        public Tawar RetrieveByNoCheckStatus(string noPO)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from POPurchn where NoPO = '" + noPO + "' and status in (0,1) and Approval>=3 order by ID desc");
            strError = dataAccess.Error;
            arrTawar = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Tawar();
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.NoPO,A.POPurchnDate,A.CreatedTime,A.SupplierID,A.Termin,A.PPN,A.Delivery,A.Crc,A.Keterangan,A.Terbilang,A.Disc,A.PPH,A.NilaiKurs,A.Cetak,A.CountPrt,A.Status,A.Approval,A.ApproveDate1,A.ApproveDate2,A.CreatedBy,A.LastModifiedBy,A.LastModifiedTime,A.AlasanBatal,A.AlasanClose,B.DepoID,A.Indent from POPurchn as A,SPP as B,POPurchnDetail as C where C.SPPID = B.ID and C.POID = A.ID and A.Status > -1 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrTawar = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTawar.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrTawar.Add(new Tawar());

            return arrTawar;
        }

        public Tawar CekSisaPO(string poNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,isnull(sum(B.Qty),0)-isnull(sum(D.Quantity),0) as SisaPO from POPurchn as A, POPurchnDetail as B,Receipt as C, ReceiptDetail as D where A.ID=B.POID and A.Status>-1 and B.Status>-1 and A.ID=C.POID and C.ID=D.ReceiptID and C. Status>-1 and B.ID=D.PODetailID and B.ItemID=D.ItemID and A.NoPO='" + poNo + "' group by A.ID");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select ID,isnull(sum(QtyPO),0) as QtyPO,isnull(sum(QtyReceipt),0) as QtyReceipt from(select A.ID,B.Qty as QtyPO," +
                                                                            "case when A.ID>0 then (select isnull(sum(C.Quantity),0) from ReceiptDetail as C where C.PODetailID=B.ID and C.RowStatus>-1 and C.ItemID=B.ItemID) else 0 end QtyReceipt " +
                                                                            "from POPurchn as A,POPurchnDetail as B where A.ID=B.POID and A.Status>-1 and B.Status>-1 and A.NoPO='" + poNo + "') as X group by ID");
            strError = dataAccess.Error;
            arrTawar = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectCek(sqlDataReader);
                }
            }

            return new Tawar();
        }

        public Tawar GenerateObject(SqlDataReader sqlDataReader)
        {
            objTawar = new Tawar();
            objTawar.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objTawar.NoPO = sqlDataReader["NoPO"].ToString();
            objTawar.POPurchnDate = Convert.ToDateTime(sqlDataReader["POPurchnDate"]);
            objTawar.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objTawar.SupplierID = Convert.ToInt32(sqlDataReader["SupplierID"]);
            objTawar.Termin = sqlDataReader["Termin"].ToString();
            objTawar.PPN = Convert.ToDecimal(sqlDataReader["PPN"]);
            objTawar.Delivery = sqlDataReader["Delivery"].ToString();
            objTawar.Crc = Convert.ToInt32(sqlDataReader["Crc"]);
            objTawar.Keterangan = sqlDataReader["Keterangan"].ToString();
            objTawar.Terbilang = sqlDataReader["Terbilang"].ToString();
            objTawar.Disc = Convert.ToInt32(sqlDataReader["Disc"]);
            objTawar.PPH = Convert.ToInt32(sqlDataReader["PPH"]);
            objTawar.NilaiKurs = Convert.ToInt32(sqlDataReader["NilaiKurs"]);
            objTawar.Cetak = Convert.ToInt32(sqlDataReader["Cetak"]);
            objTawar.CountPrt = Convert.ToInt32(sqlDataReader["CountPrt"]);
            objTawar.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objTawar.Approval = Convert.ToInt32(sqlDataReader["Approval"]);
            objTawar.ApproveDate1 = Convert.ToDateTime(sqlDataReader["ApproveDate1"]);
            objTawar.ApproveDate2 = Convert.ToDateTime(sqlDataReader["ApproveDate2"]);
            objTawar.AlasanBatal = sqlDataReader["AlasanBatal"].ToString();
            objTawar.AlasanClose = sqlDataReader["AlasanClose"].ToString();
            objTawar.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objTawar.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objTawar.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objTawar.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            objTawar.Indent = sqlDataReader["Indent"].ToString();
            return objTawar;

        }

        public Tawar GenerateObject2(SqlDataReader sqlDataReader)
        {
            objTawar = new Tawar();
            objTawar.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objTawar.NoPO = sqlDataReader["NoTawar"].ToString();
            objTawar.POPurchnDate = Convert.ToDateTime(sqlDataReader["TglTawar"]);
            objTawar.SupplierID = Convert.ToInt32(sqlDataReader["SupplierID"]);
            
            return objTawar;

        }

        public Tawar GenerateObjectViewTawar(SqlDataReader sqlDataReader)
        {
            objTawar = new Tawar();
            objTawar.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objTawar.POID = Convert.ToInt32(sqlDataReader["POID"]);
            objTawar.NoPO = sqlDataReader["NoPO"].ToString();
            objTawar.Termin = sqlDataReader["Termin"].ToString();
            objTawar.Delivery = sqlDataReader["Delivery"].ToString();
            objTawar.NOSPP = sqlDataReader["NOSPP"].ToString();
            objTawar.Satuan = sqlDataReader["Satuan"].ToString();
            objTawar.SupplierName = sqlDataReader["SupplierName"].ToString();
            objTawar.UP = sqlDataReader["UP"].ToString();
            objTawar.Telepon = sqlDataReader["Telepon"].ToString();
            objTawar.Jumlah = Convert.ToDecimal(sqlDataReader["Jumlah"]);
            objTawar.Fax = sqlDataReader["Fax"].ToString();
            objTawar.SPPID = Convert.ToInt32(sqlDataReader["SPPID"]);
            objTawar.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objTawar.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objTawar.Price = Convert.ToDecimal(sqlDataReader["Price"]);
            objTawar.Qty = Convert.ToDecimal(sqlDataReader["Qty"]);
            objTawar.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"]);
            objTawar.UOMID = Convert.ToInt32(sqlDataReader["UOMID"]);
            objTawar.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objTawar.NoUrut = Convert.ToInt32(sqlDataReader["NoUrut"]);
            objTawar.SPPDetailID = Convert.ToInt32(sqlDataReader["SPPDetailID"]);
            objTawar.DocumentNo = sqlDataReader["DocumentNo"].ToString();
            objTawar.NamaBarang = sqlDataReader["NamaBarang"].ToString();
            objTawar.ItemCode = sqlDataReader["ItemCode"].ToString();
            objTawar.Disc = Convert.ToInt32(sqlDataReader["Disc"]);
            objTawar.PPN = Convert.ToInt32(sqlDataReader["PPN"]);
            objTawar.PPH = Convert.ToInt32(sqlDataReader["PPH"]);
            objTawar.Crc = Convert.ToInt32(sqlDataReader["Crc"]);

            return objTawar;

        }

        public Tawar GenerateObjectList(SqlDataReader sqlDataReader)
        {
            objTawar = new Tawar();
            objTawar.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objTawar.NoPO = sqlDataReader["NoPO"].ToString();
            objTawar.POPurchnDate = Convert.ToDateTime(sqlDataReader["POPurchnDate"]);
            objTawar.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objTawar.SupplierID = Convert.ToInt32(sqlDataReader["SupplierID"]);
            objTawar.Termin = sqlDataReader["Termin"].ToString();
            objTawar.PPN = Convert.ToDecimal(sqlDataReader["PPN"]);
            objTawar.Delivery = sqlDataReader["Delivery"].ToString();
            objTawar.Crc = Convert.ToInt32(sqlDataReader["Crc"]);
            objTawar.Keterangan = sqlDataReader["Keterangan"].ToString();
            objTawar.Terbilang = sqlDataReader["Terbilang"].ToString();
            objTawar.Disc = Convert.ToInt32(sqlDataReader["Disc"]);
            objTawar.PPH = Convert.ToInt32(sqlDataReader["PPH"]);
            objTawar.NilaiKurs = Convert.ToInt32(sqlDataReader["NilaiKurs"]);
            objTawar.Cetak = Convert.ToInt32(sqlDataReader["Cetak"]);
            objTawar.CountPrt = Convert.ToInt32(sqlDataReader["CountPrt"]);
            objTawar.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objTawar.Approval = Convert.ToInt32(sqlDataReader["Approval"]);
            objTawar.ApproveDate1 = Convert.ToDateTime(sqlDataReader["ApproveDate1"]);
            objTawar.ApproveDate2 = Convert.ToDateTime(sqlDataReader["ApproveDate2"]);
            objTawar.AlasanBatal = sqlDataReader["AlasanBatal"].ToString();
            objTawar.AlasanClose = sqlDataReader["AlasanClose"].ToString();
            objTawar.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objTawar.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objTawar.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objTawar.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objTawar;

        }

        public Tawar GenerateObjectAllPO(SqlDataReader sqlDataReader)
        {
            objTawar = new Tawar();
            objTawar.NoPO = sqlDataReader["NoPO"].ToString();
            objTawar.NOSPP = sqlDataReader["NOSPP"].ToString();
            objTawar.ItemCode = sqlDataReader["ItemCode"].ToString();
            objTawar.NamaBarang = sqlDataReader["NamaBarang"].ToString();
            objTawar.Qty = Convert.ToInt32(sqlDataReader["Qty"]);
            objTawar.Satuan = sqlDataReader["Satuan"].ToString();
            objTawar.Price = Convert.ToInt32(sqlDataReader["Price"]);

            return objTawar;

        }

        public Tawar GenerateObjectCek(SqlDataReader sqlDataReader)
        {
            objTawar = new Tawar();
            objTawar.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objTawar.QtyPO = Convert.ToDecimal(sqlDataReader["QtyPO"]);
            objTawar.QtyReceipt = Convert.ToDecimal(sqlDataReader["QtyReceipt"]);

            return objTawar;

        }

        public Tawar GenerateObjectViewPO(SqlDataReader sqlDataReader)
        {
            objTawar = new Tawar();
            objTawar.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objTawar.POID = Convert.ToInt32(sqlDataReader["TawarID"]);
            objTawar.NoPO = sqlDataReader["NoTawar"].ToString();
            objTawar.POPurchnDate = Convert.ToDateTime(sqlDataReader["TglTawar"]);
            objTawar.SupplierName = sqlDataReader["SupplierName"].ToString();
            objTawar.NOSPP = sqlDataReader["NoSPP"].ToString();
            objTawar.ItemCode = sqlDataReader["ItemCode"].ToString();
            objTawar.ItemName = sqlDataReader["ItemName"].ToString();
            objTawar.Qty = Convert.ToDecimal(sqlDataReader["Qty"]);
            objTawar.Satuan = sqlDataReader["UOMCode"].ToString();
            objTawar.NamaBarang = sqlDataReader["ItemName"].ToString();
            
           
            return objTawar;

        }


    }

}
