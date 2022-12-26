using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;
using Dapper;

namespace BusinessFacade
{
    public class POPurchnFacade : AbstractTransactionFacade
    {
        private POPurchn objPOPurchn = new POPurchn();
        private RekapPoKertas objRekapPoKertas = new RekapPoKertas();
        private ArrayList arrPOPurchn;
        private List<SqlParameter> sqlListParam;
       public POPurchnFacade(object objDomain)
            : base(objDomain)
        {
            objPOPurchn = (POPurchn)objDomain;
        }

        public POPurchnFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@NoPO", objPOPurchn.NoPO));
                sqlListParam.Add(new SqlParameter("@POPurchnDate", objPOPurchn.POPurchnDate));
                sqlListParam.Add(new SqlParameter("@SupplierID", objPOPurchn.SupplierID));
                sqlListParam.Add(new SqlParameter("@Termin", objPOPurchn.Termin));
                sqlListParam.Add(new SqlParameter("@PPN", objPOPurchn.PPN));
                sqlListParam.Add(new SqlParameter("@Delivery", objPOPurchn.Delivery));
                sqlListParam.Add(new SqlParameter("@Crc", objPOPurchn.Crc));
                sqlListParam.Add(new SqlParameter("@Keterangan", objPOPurchn.Keterangan));
                sqlListParam.Add(new SqlParameter("@Terbilang", objPOPurchn.Terbilang));
                sqlListParam.Add(new SqlParameter("@Disc", objPOPurchn.Disc));
                sqlListParam.Add(new SqlParameter("@PPH", objPOPurchn.PPH));
                sqlListParam.Add(new SqlParameter("@NilaiKurs", objPOPurchn.NilaiKurs));
                sqlListParam.Add(new SqlParameter("@Cetak", objPOPurchn.Cetak));
                sqlListParam.Add(new SqlParameter("@CountPrt", objPOPurchn.CountPrt));
                sqlListParam.Add(new SqlParameter("@Status", objPOPurchn.Status));
                sqlListParam.Add(new SqlParameter("@Approval", objPOPurchn.Approval));
                sqlListParam.Add(new SqlParameter("@ApproveDate1", objPOPurchn.ApproveDate1));
                sqlListParam.Add(new SqlParameter("@ApproveDate2", objPOPurchn.ApproveDate2));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objPOPurchn.CreatedBy));
                sqlListParam.Add(new SqlParameter("@AlasanBatal", objPOPurchn.AlasanBatal));
                sqlListParam.Add(new SqlParameter("@AlasanClose", objPOPurchn.AlasanClose));
                sqlListParam.Add(new SqlParameter("@PaymentType", objPOPurchn.PaymentType));
                sqlListParam.Add(new SqlParameter("@ItemFrom", objPOPurchn.ItemFrom));
                sqlListParam.Add(new SqlParameter("@Indent", objPOPurchn.Indent));
                sqlListParam.Add(new SqlParameter("@Ongkos", objPOPurchn.Ongkos ));
                sqlListParam.Add(new SqlParameter("@UangMuka", objPOPurchn.UangMuka));
                sqlListParam.Add(new SqlParameter("@CoaID", objPOPurchn.CoaID));
                sqlListParam.Add(new SqlParameter("@Remark", objPOPurchn.Remark));
                //added on 03-07-2015
                //sqlListParam.Add(new SqlParameter("@NilaiKurs", objPOPurchn.NilaiKurs));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertPOPurchn");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        // Pak, knapa pada Update dibawah ini gak ada sqlListParam.Add(new SqlParameter("@Ongkos", objPOPurchn.Ongkos ));
        // so saya blom tambah utk UangMuka


        public override int Update(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objPOPurchn.ID));
                sqlListParam.Add(new SqlParameter("@NoPO", objPOPurchn.NoPO));
                sqlListParam.Add(new SqlParameter("@POPurchnDate", objPOPurchn.POPurchnDate));
                sqlListParam.Add(new SqlParameter("@SupplierID", objPOPurchn.SupplierID));
                sqlListParam.Add(new SqlParameter("@Termin", objPOPurchn.Termin));
                sqlListParam.Add(new SqlParameter("@PPN", objPOPurchn.PPN));
                sqlListParam.Add(new SqlParameter("@Delivery", objPOPurchn.Delivery));
                sqlListParam.Add(new SqlParameter("@Crc", objPOPurchn.Crc));
                sqlListParam.Add(new SqlParameter("@Keterangan", objPOPurchn.Keterangan));
                sqlListParam.Add(new SqlParameter("@Terbilang", objPOPurchn.Terbilang));
                sqlListParam.Add(new SqlParameter("@Disc", objPOPurchn.Disc));
                sqlListParam.Add(new SqlParameter("@PPH", objPOPurchn.PPH));
                sqlListParam.Add(new SqlParameter("@NilaiKurs", objPOPurchn.NilaiKurs));
                sqlListParam.Add(new SqlParameter("@Cetak", objPOPurchn.Cetak));
                sqlListParam.Add(new SqlParameter("@CountPrt", objPOPurchn.CountPrt));
                sqlListParam.Add(new SqlParameter("@Status", objPOPurchn.Status));
                sqlListParam.Add(new SqlParameter("@Approval", objPOPurchn.Approval));
                
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objPOPurchn.LastModifiedBy));
                //sqlListParam.Add(new SqlParameter("@LastModifiedTime", objPOPurchn.LastModifiedTime));
                sqlListParam.Add(new SqlParameter("@PaymentType", objPOPurchn.PaymentType));
                sqlListParam.Add(new SqlParameter("@ItemFrom", objPOPurchn.ItemFrom));
                sqlListParam.Add(new SqlParameter("@Indent", objPOPurchn.Indent));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdatePOPurchn");

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
                sqlListParam.Add(new SqlParameter("@ID", objPOPurchn.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objPOPurchn.LastModifiedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spDeletePOPurchn");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        
        public int UpdateStatusPO(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objPOPurchn.ID));
                sqlListParam.Add(new SqlParameter("@Status", objPOPurchn.Status));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateStatusPOPurchn");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

       
        public int UpdateSubCompanyID(object objDomain)
        {
            int result = 0;
            DataAccess da = new DataAccess(Global.ConnectionString());
            sqlListParam = new List<SqlParameter>();
            objPOPurchn = (POPurchn)objDomain;
            sqlListParam.Add(new SqlParameter("@ID", objPOPurchn.ID));
            sqlListParam.Add(new SqlParameter("@SubCompanyID", objPOPurchn.SubCompanyID));
            result = da.ProcessData(sqlListParam, "spPoPurchnSubCom_Update");
            return result;
        }

        public int UpdateAlasanPrint(object objDomain)
        {
            int result = 0;
            DataAccess da = new DataAccess(Global.ConnectionString());
            sqlListParam = new List<SqlParameter>();
            objPOPurchn = (POPurchn)objDomain;
            sqlListParam.Add(new SqlParameter("@NoPO", objPOPurchn.NoPO));
            sqlListParam.Add(new SqlParameter("@AlasanReprint", objPOPurchn.AlasanReprint));
            result = da.ProcessData(sqlListParam, "spInsertReprintPO");
            return result;
        }

        public int UpdateUangMuka(object objDomain)
        {
            int result = 0;
            DataAccess da = new DataAccess(Global.ConnectionString());
            sqlListParam = new List<SqlParameter>();
            objPOPurchn = (POPurchn)objDomain;
            SqlDataReader sdr = da.RetrieveDataByString("Update POPurchn set UangMuka=" + objPOPurchn.UangMuka + " where ID=" + objPOPurchn.ID);
            result = sdr.RecordsAffected;
            return result;
        }



        public int UpdateApprovePO1(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objPOPurchn.ID));
                sqlListParam.Add(new SqlParameter("@Approval", objPOPurchn.Approval));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objPOPurchn.LastModifiedBy));
                //sqlListParam.Add(new SqlParameter("@LastModifiedTime", objPOPurchn.LastModifiedTime));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateApprovePO1");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateApprovePO2(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objPOPurchn.ID));
                sqlListParam.Add(new SqlParameter("@Approval", objPOPurchn.Approval));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objPOPurchn.LastModifiedBy));
                //sqlListParam.Add(new SqlParameter("@@ApproveDate1", objPOPurchn.ApproveDate1));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateApprovePO2");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateApprovePO3(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objPOPurchn.ID));
                sqlListParam.Add(new SqlParameter("@Approval", objPOPurchn.Approval));
                //sqlListParam.Add(new SqlParameter("@ApproveDate2", objPOPurchn.ApproveDate2));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateApprovePO3");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateApprovePO4(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objPOPurchn.ID));
                sqlListParam.Add(new SqlParameter("@Approval", objPOPurchn.Approval));
                //sqlListParam.Add(new SqlParameter("@LastModifiedTime", objPOPurchn.LastModifiedTime));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateApprovePO4");

                strError = transManager.Error;

                return intResult;

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
                sqlListParam.Add(new SqlParameter("@ID", objPOPurchn.ID));
                sqlListParam.Add(new SqlParameter("@Approval", objPOPurchn.Approval));
                sqlListParam.Add(new SqlParameter("@lastModifiedBy", objPOPurchn.CreatedBy));
                sqlListParam.Add(new SqlParameter("@AlasanNotApproval", objPOPurchn.AlasanNotApproval));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateStatusPOPurchnNotApproval");

                strError = transManager.Error;
                
                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        //agus 2022-07-15
		public int CetakUlangPO(object objDomain)
        {
            int result = 0;
            DataAccess da = new DataAccess(Global.ConnectionString());
            sqlListParam = new List<SqlParameter>();
            objPOPurchn = (POPurchn)objDomain;
            SqlDataReader sdr = da.RetrieveDataByString("Update POPurchn set Cetak= 0 where NoPO='" + objPOPurchn.NoPO + "' ");
            result = sdr.RecordsAffected;
            return result;
        }
		
		//agus 2022-07-15

        public override ArrayList Retrieve()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select top 50 A.ID,A.NoPO,A.POPurchnDate,A.CreatedTime,A.SupplierID,A.Termin,A.PPN,A.Delivery,A.Crc," +
                          "A.Keterangan,A.Terbilang,A.Disc,A.PPH,A.NilaiKurs,A.Cetak,A.CountPrt,A.Status,A.Approval,A.ApproveDate1," +
                          "A.ApproveDate2,A.CreatedBy,A.LastModifiedBy,A.LastModifiedTime,A.AlasanBatal,A.AlasanClose,A.Indent,A.Ongkos," +
                          "A.uangmuka,A.coaID,A.remark from POPurchn as A order by A.ID desc";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPOPurchn.Add(GenerateObjectNodetail(sqlDataReader));
                }
            }
            else
                arrPOPurchn.Add(new POPurchn());

            return arrPOPurchn;
        }

        //new
        public ArrayList ViewGridPO(int id,int viewprice)
        {
            string inputKursAktif = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("InputNilaiKurs", "PO").ToString();
            string JmlPrice = (inputKursAktif == "Aktif") ? "CASE WHEN E.flag=0 and B.NilaiKurs>0 and B.Crc>1 THEN (A.Qty*(A.Price*B.NilaiKurs))ELSE(A.Qty*A.Price) END" : "(A.Qty*A.Price)";
            string Price = (inputKursAktif == "Aktif") ? " CASE WHEN E.flag=0 and B.NilaiKurs>0 and B.Crc>1 THEN (B.NilaiKurs*A.Price) ELSE A.Price END" : "A.Price";

            string strSQL = string.Empty;
            /*if (viewprice < 2)
            {
                strSQL = "select B.Approval, A.ID,A.POID,B.NoPO,B.Termin,B.Delivery,B.Disc,B.PPH,B.PPN,B.Crc,B.itemfrom,C.NoSPP,D.UOMCode as Satuan,E.SupplierName,E.UP,E.Telepon,(A.Qty * A.Price) as Jumlah, " +
                         "E.Fax,A.SPPID,A.GroupID,A.ItemID,case when (select isnull(harga,0) from Inventory where ID=A.ItemID)=0 then A.Price else 0 end price,A.Qty,A.ItemTypeID,A.UOMID,A.Status,A.NoUrut,A.SPPDetailID,A.DocumentNo,B.alasanNotApproval, " +
                         "case A.ItemTypeID when 1  then (select ItemName from Inventory where ID=A.ItemID and RowStatus > -1) " +
                         "when 2 then (select ItemName from Asset where ID=A.ItemID and RowStatus > -1) else " +
                         ItemSPPBiayaNew("A") + " end NamaBarang, " +
                         "case A.ItemTypeID when 1  then (select ItemCode from Inventory where ID=A.ItemID and RowStatus > -1) " +
                         "when 2 then (select ItemCode from Asset where ID=A.ItemID and RowStatus > -1) " +
                         "else (select ItemCode from Biaya where ID=A.ItemID and RowStatus > -1) end ItemCode,isnull(A.dlvdate,'1/1/1900') as dlvdate,isnull(B.ongkos,0) as ongkos,isnull(remark,'')as remark   " +
                         "from POPurchnDetail as A,POPurchn as B,SPP as C,UOM as D,SuppPurch as E " +
                         "where A.POID = B.ID and A.SPPID = C.ID and A.UOMID = D.ID and B.SupplierID = E.ID and A.Status >-1 and A.POID = " + id;
            }
            else
            {
               */
            if (inputKursAktif == "")
            {
                strSQL = "select B.Approval, A.ID,A.POID,B.NoPO,B.Termin,B.Delivery,B.Disc,B.PPH,B.PPN,B.Crc,B.itemfrom,C.NoSPP,D.UOMCode as Satuan,E.SupplierName,E.UP,E.Telepon,(A.Qty * A.Price) as Jumlah, " +
                         "E.Fax,A.SPPID,A.GroupID,A.ItemID,A.Price,A.Qty,A.ItemTypeID,A.UOMID,A.Status,A.NoUrut,A.SPPDetailID,A.DocumentNo,B.alasanNotApproval, " +
                         "case A.ItemTypeID when 1  then (select ItemName from Inventory where ID=A.ItemID and RowStatus > -1) " +
                         "when 2 then (select ItemName from Asset where ID=A.ItemID and RowStatus > -1) " +
                         "else " + ItemSPPBiayaNew("A") + " end NamaBarang, " +
                         "case A.ItemTypeID when 1  then (select ItemCode from Inventory where ID=A.ItemID and RowStatus > -1) " +
                         "when 2 then (select ItemCode from Asset where ID=A.ItemID and RowStatus > -1) " +
                         "else (select ItemCode from Biaya where ID=A.ItemID and RowStatus > -1) end ItemCode,isnull(A.dlvdate,'1/1/1900') as dlvdate,isnull(B.ongkos,0) as ongkos,isnull(remark,'')as remark   " +
                         "from POPurchnDetail as A,POPurchn as B,SPP as C,UOM as D,SuppPurch as E " +
                         "where A.POID = B.ID and A.SPPID = C.ID and A.UOMID = D.ID and B.SupplierID = E.ID and A.Status >-1 and A.POID = " + id;
            }
            else
            {
                strSQL = "select B.Approval, A.ID,A.POID,B.NoPO,B.Termin,B.Delivery,B.Disc,B.PPH,B.PPN,B.Crc,B.itemfrom,C.NoSPP,D.UOMCode as Satuan,E.SupplierName,E.UP,E.Telepon," +
                          JmlPrice + " as Jumlah, " +
                         "E.Fax,A.SPPID,A.GroupID,A.ItemID," + Price + " as Price,A.Qty,A.ItemTypeID,A.UOMID,A.Status,A.NoUrut,A.SPPDetailID,A.DocumentNo,B.alasanNotApproval, " +
                         "case A.ItemTypeID when 1  then (select ItemName from Inventory where ID=A.ItemID and RowStatus > -1) " +
                         "when 2 then (select ItemName from Asset where ID=A.ItemID and RowStatus > -1) " +
                         "else " + ItemSPPBiayaNew("A") + " end NamaBarang, " +
                         "case A.ItemTypeID when 1  then (select ItemCode from Inventory where ID=A.ItemID and RowStatus > -1) " +
                         "when 2 then (select ItemCode from Asset where ID=A.ItemID and RowStatus > -1) " +
                         "else (select ItemCode from Biaya where ID=A.ItemID and RowStatus > -1) end ItemCode,isnull(A.dlvdate,'1/1/1900') as dlvdate,isnull(B.ongkos,0) as ongkos,isnull(remark,'')as remark   " +
                         "from POPurchnDetail as A,POPurchn as B,SPP as C,UOM as D,SuppPurch as E " +
                         "where A.POID = B.ID and A.SPPID = C.ID and A.UOMID = D.ID and B.SupplierID = E.ID and A.Status >-1 and A.POID = " + id;
            }
            //}
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
           
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPOPurchn.Add(GenerateObjectViewPO(sqlDataReader));
                }
            }
            else
                arrPOPurchn.Add(new POPurchn());

            return arrPOPurchn;
        }

        public ArrayList ViewGridPOWithApproval(int approval, int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select B.Approval, A.ID,A.POID,B.NoPO,B.Termin,B.Delivery,B.Disc,B.PPH,B.PPN,B.Crc,B.itemfrom,C.NoSPP,D.UOMCode as Satuan,E.SupplierName,E.UP,E.Telepon,(A.Qty * A.Price) as Jumlah," +
                "E.Fax,A.SPPID,A.GroupID,A.ItemID,A.Price,A.Qty,A.ItemTypeID,A.UOMID,B.Status,A.NoUrut,A.SPPDetailID,A.DocumentNo, B.alasanNotApproval, " +
                "case A.ItemTypeID when 1  then (select ItemName from Inventory where ID=A.ItemID and RowStatus > -1) "+
                "when 2 then (select ItemName from Asset where ID=A.ItemID and RowStatus > -1) "+
                "else (select ItemName from Biaya where ID=A.ItemID and RowStatus > -1) end NamaBarang, "+
                "case A.ItemTypeID when 1  then (select ItemCode from Inventory where ID=A.ItemID and RowStatus > -1) "+
                "when 2 then (select ItemCode from Asset where ID=A.ItemID and RowStatus > -1) "+
                "else (select ItemCode from Biaya where ID=A.ItemID and RowStatus > -1) end ItemCode,isnull(A.dlvdate,'1/1/1900') as dlvdate,isnull(B.ongkos,0) as ongkos ,isnull(remark,'')as remark   " +
                "from POPurchnDetail as A,POPurchn as B,SPP as C,UOM as D,SuppPurch as E "+
                "where A.POID = B.ID and A.SPPID = C.ID and A.UOMID = D.ID and B.SupplierID = E.ID and B.Status=0 "+
                "and B.Approval=" + approval + " and C.DepoID=" + depoID + " and A.Status>-1 order by B.LastModifiedTime desc");

            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPOPurchn.Add(GenerateObjectViewPO(sqlDataReader));
                }
            }
            else
                arrPOPurchn.Add(new POPurchn());

            return arrPOPurchn;
        }

        public ArrayList ViewGridPOWithApprovalByNo(int approval, int depoID, string noPO)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select B.Approval, A.ID,A.POID,B.NoPO,B.Termin,B.Delivery,B.Disc,B.PPH,B.PPN,B.Crc,B.itemfrom,C.NoSPP,D.UOMCode as Satuan,E.SupplierName,E.UP,E.Telepon,(A.Qty * A.Price) as Jumlah," +
                "E.Fax,A.SPPID,A.GroupID,A.ItemID,A.Price,A.Qty,A.ItemTypeID,A.UOMID,B.Status,A.NoUrut,A.SPPDetailID,A.DocumentNo,B.alasanNotApproval, " +
                "case A.ItemTypeID when 1  then (select ItemName from Inventory where ID=A.ItemID and RowStatus > -1) " +
                "when 2 then (select ItemName from Asset where ID=A.ItemID and RowStatus > -1) " +
                "else (select ItemName from Biaya where ID=A.ItemID and RowStatus > -1) end NamaBarang, " +
                "case A.ItemTypeID when 1  then (select ItemCode from Inventory where ID=A.ItemID and RowStatus > -1) " +
                "when 2 then (select ItemCode from Asset where ID=A.ItemID and RowStatus > -1) " +
                "else (select ItemCode from Biaya where ID=A.ItemID and RowStatus > -1) end ItemCode,isnull(A.dlvdate,'1/1/1900') as dlvdate,isnull(B.ongkos,0) as ongkos,isnull(remark,'')as remark     " +
                "from POPurchnDetail as A,POPurchn as B,SPP as C,UOM as D,SuppPurch as E " +
                "where A.POID = B.ID and A.SPPID = C.ID and A.UOMID = D.ID and B.SupplierID = E.ID and B.Status=0" +
                "and B.Approval=" + approval + " and C.DepoID=" + depoID + " and B.NoPO='"+noPO+"'");

            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPOPurchn.Add(GenerateObjectViewPO(sqlDataReader));
                }
            }
            else
                arrPOPurchn.Add(new POPurchn());

            return arrPOPurchn;
        }

        public POPurchn ViewPO(int id,int viewprice)
        {
             string strSQL = string.Empty;
            if (viewprice<2)
                strSQL = "select B.Approval, A.ID,A.POID,B.NoPO,B.Termin,B.Delivery,B.Disc,B.PPH,B.PPN,B.Crc,B.itemfrom,C.NoSPP,D.UOMCode as Satuan,E.SupplierName,E.UP,E.Telepon,case when (select isnull(harga,0) from Inventory where ID=A.ItemID)=0 then(A.Qty * A.Price) else 0 end  Jumlah, " +
                    "E.Fax,A.SPPID,A.GroupID,A.ItemID,case when (select isnull(harga,0) from Inventory where ID=A.ItemID)=0 then A.Price else 0 end Price,A.Qty,A.ItemTypeID,A.UOMID,A.Status,A.NoUrut,A.SPPDetailID,A.DocumentNo,B.alasanNotApproval, " +
                    "case A.ItemTypeID when 1  then (select ItemName from Inventory where ID=A.ItemID and RowStatus > -1) " +
                    "when 2 then (select ItemName from Asset where ID=A.ItemID and RowStatus > -1) " +
                    "else (select ItemName from Biaya where ID=A.ItemID and RowStatus > -1) end NamaBarang, " +
                    "case A.ItemTypeID when 1  then (select ItemCode from Inventory where ID=A.ItemID and RowStatus > -1) " +
                    "when 2 then (select ItemCode from Asset where ID=A.ItemID and RowStatus > -1) " +
                    "else (select ItemCode from Biaya where ID=A.ItemID and RowStatus > -1) end ItemCode,isnull(A.dlvdate,'1/1/1900') as dlvdate,isnull(B.ongkos,0) as ongkos,UPPER ( isnull(remark,'') + '. ' + isnull(AlasanNotApproval,'')  ) as remark     " +
                    "from POPurchnDetail as A,POPurchn as B,SPP as C,UOM as D,SuppPurch as E " +
                    "where A.Status>-1 and A.POID = B.ID and A.SPPID = C.ID and A.UOMID = D.ID and B.SupplierID = E.ID and A.POID = " + id;
            else
                strSQL = "select B.Approval, A.ID,A.POID,B.NoPO,B.Termin,B.Delivery,B.Disc,B.PPH,B.PPN,B.Crc,B.itemfrom,C.NoSPP,D.UOMCode as Satuan,E.SupplierName,E.UP,E.Telepon,(A.Qty * A.Price) as Jumlah, " +
                    "E.Fax,A.SPPID,A.GroupID,A.ItemID,A.Price,A.Qty,A.ItemTypeID,A.UOMID,A.Status,A.NoUrut,A.SPPDetailID,A.DocumentNo,B.alasanNotApproval, " +
                    "case A.ItemTypeID when 1  then (select ItemName from Inventory where ID=A.ItemID and RowStatus > -1) " +
                    "when 2 then (select ItemName from Asset where ID=A.ItemID and RowStatus > -1) " +
                    "else (select ItemName from Biaya where ID=A.ItemID and RowStatus > -1) end NamaBarang, " +
                    "case A.ItemTypeID when 1  then (select ItemCode from Inventory where ID=A.ItemID and RowStatus > -1) " +
                    "when 2 then (select ItemCode from Asset where ID=A.ItemID and RowStatus > -1) " +
                    "else (select ItemCode from Biaya where ID=A.ItemID and RowStatus > -1) end ItemCode,isnull(A.dlvdate,'1/1/1900') as dlvdate,isnull(B.ongkos,0) as ongkos,UPPER ( isnull(remark,'') + '. ' + isnull(AlasanNotApproval,'')  ) as remark     " +
                    "from POPurchnDetail as A,POPurchn as B,SPP as C,UOM as D,SuppPurch as E " +
                    "where A.Status >-1 and A.POID = B.ID and A.SPPID = C.ID and A.UOMID = D.ID and B.SupplierID = E.ID and A.POID = " + id;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectViewPO(sqlDataReader);
                }
            }

            return new POPurchn();
        }
        //new

        public ArrayList RetrieveOpenStatus()
        {
            string strSQL = "select top 50 A.ID,A.NoPO,A.POPurchnDate,A.CreatedTime,A.SupplierID,A.Termin,A.PPN,A.Delivery,A.Crc,"+
                            "A.Keterangan,A.Terbilang,A.Disc,A.PPH,A.NilaiKurs,A.Cetak,A.CountPrt,A.Status,A.Approval,A.ApproveDate1,"+
                            "A.ApproveDate2,A.CreatedBy,A.LastModifiedBy,A.LastModifiedTime,A.AlasanBatal,A.AlasanClose,A.Ongkos,A.uangmuka,"+
                            "A.coaID,A.remark from POPurchn as A where A.Approval = 0 and A.Status = 0 order by A.ID desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPOPurchn.Add(GenerateObjectNodetail(sqlDataReader));
                }
            }
            else
                arrPOPurchn.Add(new POPurchn());

            return arrPOPurchn;
        }


        public ArrayList RetrieveOpenStatusByDepo(int depoID)
        {
            string strSQL = "select A.ID,A.NoPO,A.POPurchnDate,A.CreatedTime,A.SupplierID,A.Termin,A.PPN,A.Delivery,A.Crc,A.Keterangan,"+
                            "A.Terbilang,A.Disc,A.PPH,A.NilaiKurs,A.Cetak,A.CountPrt,A.Status,A.Approval,A.ApproveDate1,A.ApproveDate2,"+
                            "A.CreatedBy,A.LastModifiedBy,A.LastModifiedTime,A.AlasanBatal,A.AlasanClose,B.DepoID,A.Indent,"+
                            "isnull(C.dlvdate,'1/1/1900') as dlvdate  from POPurchn as A,SPP as B,POPurchnDetail as C "+
                            "where C.SPPID = B.ID and C.POID = A.ID and A.Approval = 0 and A.Status = 0 and B.DepoID = " + depoID + " "+
                            "order by A.ID desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPOPurchn.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrPOPurchn.Add(new POPurchn());

            return arrPOPurchn;
        }

        

        public ArrayList RetrieveAllByDepo(int depoID)
        {
            string strSQL = "select top 50 A.ID,A.NoPO,A.POPurchnDate,A.CreatedTime,A.SupplierID,A.Termin,A.PPN,A.Delivery,A.Crc,"+
                            "A.Keterangan,A.Terbilang,A.Disc,A.PPH,A.NilaiKurs,A.Cetak,A.CountPrt,A.Status,A.Approval,A.ApproveDate1,"+
                            "A.ApproveDate2,A.CreatedBy,A.LastModifiedBy,A.LastModifiedTime,A.AlasanBatal,A.AlasanClose,B.DepoID,A.Indent,"+
                            "isnull(C.dlvdate,'1/1/1900') as dlvdate  from POPurchn as A,SPP as B,POPurchnDetail as C "+
                            "where C.SPPID = B.ID and C.POID = A.ID and B.DepoID = " + depoID + " order by A.ID desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPOPurchn.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrPOPurchn.Add(new POPurchn());

            return arrPOPurchn;
        }

        public ArrayList RetrieveAllPO(int groupID, string strField, string strNoPo)
        {
            string strGroupID = string.Empty;
            if (groupID == 1 || groupID == 2)
            {
                strGroupID = " and A.GroupID in (1,2)";
            }
            if (groupID == 3)
            {
                strGroupID = " and A.GroupID in (3)";
            }
            if (groupID == 4)
            {
                strGroupID = " and A.GroupID in (4)";
            }
            if (groupID == 5)
            {
                strGroupID = " and A.GroupID in (5)";
            }
            if (groupID == 6 || groupID == 7 || groupID == 8 || groupID == 9)
            {
                strGroupID = " and A.GroupID in (6,7,8,9)";
            }
            Users users = (Users)HttpContext.Current.Session["Users"];
            string strCariNoPO = string.Empty;
            if (strNoPo != string.Empty)
            {
                strCariNoPO = strField + "'%" + strNoPo + "%'";
            }
            else
            {
                strCariNoPO = (users.Apv == 0) ? " and A.CreatedBy='" + users.UserID.ToString() + "'" : "";
            }

            string limit = (strGroupID == string.Empty) ? "top 500 " : "";
            string strSQL = "select "+limit+" A.NoPO,C.NoSPP," +
                "case B.ItemTypeID when 1 then (select ItemCode from Inventory where Inventory.ID=B.ItemID) " +
                "when 2 then (select ItemCode from Asset where Asset.ID=B.ItemID) " +
                "when 3 then  (select ItemCode from Biaya where Biaya.ID=B.ItemID) ELSE '' end ItemCode, " +
                "case B.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=B.ItemID) " +
                "when 2 then (select ItemName from Asset where Asset.ID=B.ItemID) " +
                "when 3 then " + ItemSPPBiayaNew("B")+
                " end NamaBarang,B.Qty, E.UOMCode as Satuan,B.Price,isnull(B.dlvdate,'1/1/1900') as dlvdate  " +
                "from POPurchn as A,POPurchnDetail as B, SPP as C, UOM as E  where B.POID = A.ID and B.SPPID = C.ID " +
                "and B.UOMID = E.ID and B.Status>-1 and A.Status=0 " + strCariNoPO + " order by A.ID Desc";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPOPurchn.Add(GenerateObjectAllPO(sqlDataReader));
                }
            }
            else
                arrPOPurchn.Add(new POPurchn());

            return arrPOPurchn;
        }

        public ArrayList RetrieveApproveStatus()
        {
            string strSQL = "select A.ID,A.NoPO,A.POPurchnDate,A.CreatedTime,A.SupplierID,A.Termin,A.PPN,A.Delivery,A.Crc,A.Keterangan,"+
                            "A.Terbilang,A.Disc,A.PPH,A.NilaiKurs,A.Cetak,A.CountPrt,A.Status,A.Approval,A.ApproveDate1,A.ApproveDate2,"+
                            "A.CreatedBy,A.LastModifiedBy,A.LastModifiedTime,A.AlasanBatal,A.AlasanClose,A.Indent,A.Ongkos,A.uangmuka,"+
                            "A.coaID,A.remark from POPurchn as A where A.Approval > 0 and A.Status > 0 order by A.ID desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPOPurchn.Add(GenerateObjectNodetail(sqlDataReader));
                }
            }
            else
                arrPOPurchn.Add(new POPurchn());

            return arrPOPurchn;
        }

        public ArrayList RetrieveOpenStatusByDepo(int Approval, int depoID)
        {
            string strSQL = "select A.ID,A.NoPO,A.POPurchnDate,A.CreatedTime,A.SupplierID,A.Termin,A.PPN,A.Delivery,A.Crc,A.Keterangan,"+
                            "A.Terbilang,A.Disc,A.PPH,A.NilaiKurs,A.Cetak,A.CountPrt,A.Status,A.Approval,A.ApproveDate1,A.ApproveDate2,"+
                            "A.CreatedBy,A.LastModifiedBy,A.LastModifiedTime,A.AlasanBatal,A.AlasanClose,B.DepoID,A.Indent,"+
                            "isnull(C.dlvdate,'1/1/1900') as dlvdate  from POPurchn as A,SPP as B,POPurchnDetail as C "+
                            "where C.SPPID = B.ID and C.POID = A.ID and A.Approval = " + Approval + " and A.Status = 0 and B.DepoID = " + depoID + " "+
                            "order by A.ID desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPOPurchn.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrPOPurchn.Add(new POPurchn());

            return arrPOPurchn;
        }

        public string GroupApp { get; set; }
        public ArrayList RetrieveOpenApproval(int Approval)
        {
            string strSQL = "select distinct A.ID,A.NoPO,A.POPurchnDate,A.CreatedTime,A.SupplierID,A.Termin,A.PPN,A.Delivery,A.Crc,A.Keterangan," +
                "A.Terbilang,A.Disc,A.PPH,A.NilaiKurs,A.Cetak,A.CountPrt,A.Status,A.Approval,A.ApproveDate1,A.ApproveDate2,A.CreatedBy," +
                "A.LastModifiedBy,A.LastModifiedTime,A.AlasanBatal,A.AlasanClose,A.Indent,A.Ongkos,A.uangmuka,A.coaID,A.remark " +
                "from POPurchn as A INNER JOIN POPurchnDetail ON A.ID = POPurchnDetail.POID where A.Approval =" + Approval +
                " and A.Status = 0 and POPurchnDetail.status>-1 and A.NoPO<>'' " + this.GroupApp +
                " order by A.ID desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPOPurchn.Add(GenerateObjectNodetail(sqlDataReader));
                }
            }
            else
                arrPOPurchn.Add(new POPurchn());

            return arrPOPurchn;
        }

        public ArrayList RetrieveOpenApprovalByNo(int Approval, string noPO)
        {
            string strSQL = "select A.ID,A.NoPO,A.POPurchnDate,A.CreatedTime,A.SupplierID,A.Termin,A.PPN,A.Delivery,A.Crc,A.Keterangan,"+
                            "A.Terbilang,A.Disc,A.PPH,A.NilaiKurs,A.Cetak,A.CountPrt,A.Status,A.Approval,A.ApproveDate1,A.ApproveDate2,"+
                            "A.CreatedBy,A.LastModifiedBy,A.LastModifiedTime,A.AlasanBatal,A.AlasanClose,A.Indent,A.Ongkos,A.uangmuka,A.coaID,"+
                            "A.remark from POPurchn as A where A.Approval =" + Approval + " and A.Status = 0 and A.NoPO='" + noPO + "' "+
                            "order by A.ID desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPOPurchn.Add(GenerateObjectNodetail(sqlDataReader));
                }
            }
            else
                arrPOPurchn.Add(new POPurchn());

            return arrPOPurchn;
        }

        //public ArrayList RetrieveCekStatus(string poNo)
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.NoPO,A.POPurchnDate,A.CreatedTime,A.SupplierID,A.Termin,A.PPN,A.Delivery,A.Crc,A.Keterangan,A.Terbilang,A.Disc,A.PPH,A.NilaiKurs,A.Cetak,A.CountPrt,A.Status,A.Approval,A.ApproveDate1,A.ApproveDate2,A.CreatedBy,A.LastModifiedBy,A.LastModifiedTime,A.AlasanBatal,A.AlasanClose from POPurchn as A where A.Approval > 0 and A.Status > 0 order by A.ID desc");
        //    strError = dataAccess.Error;
        //    arrPOPurchn = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrPOPurchn.Add(GenerateObject(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrPOPurchn.Add(new POPurchn());

        //    return arrPOPurchn;
        //}

        public ArrayList RetrieveApproveStatusByDepo(int depoID)
        {
            string strSQL = "select A.ID,A.NoPO,A.POPurchnDate,A.CreatedTime,A.SupplierID,A.Termin,A.PPN,A.Delivery,A.Crc,A.Keterangan,"+
                            "A.Terbilang,A.Disc,A.PPH,A.NilaiKurs,A.Cetak,A.CountPrt,A.Status,A.Approval,A.ApproveDate1,A.ApproveDate2,"+
                            "A.CreatedBy,A.LastModifiedBy,A.LastModifiedTime,A.AlasanBatal,A.AlasanClose,B.DepoID,A.Indent,"+
                            "isnull(C.dlvdate,'1/1/1900') as dlvdate  from POPurchn as A,SPP as B,POPurchnDetail as C where A.Approval > 0 and A.Status > 0 and B.DepoID = " + depoID + " order by A.ID desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPOPurchn.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrPOPurchn.Add(new POPurchn());

            return arrPOPurchn;
        }

        public POPurchn RetrieveByNo(string noPO)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "select A.ID,A.NoPO,A.POPurchnDate,A.CreatedTime,A.SupplierID,A.Termin,A.PPN,A.Delivery,A.Crc,"+
                            "A.Keterangan,A.Terbilang,A.Disc,A.PPH,A.NilaiKurs,A.Cetak,A.CountPrt,A.Status,A.Approval,A.ApproveDate1,"+
                            "A.ApproveDate2,A.CreatedBy,A.LastModifiedBy,A.LastModifiedTime,A.AlasanBatal,A.AlasanClose,A.Indent,"+
                            "isnull(C.dlvdate,'1/1/1900') as dlvdate,isnull(C.ID,0) as PODetailID  from POPurchn as A,POPurchnDetail as C "+
                            "where C.POID = A.ID and A.NoPO = '" + noPO + "' order by A.ID desc";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.NoPO,A.POPurchnDate,A.CreatedTime,A.SupplierID,A.Termin,A.PPN,A.Delivery,A.Crc,A.Keterangan,A.Terbilang,A.Disc,A.PPH,A.NilaiKurs,A.Cetak,A.CountPrt,A.Status,A.Approval,A.ApproveDate1,A.ApproveDate2,A.CreatedBy,A.LastModifiedBy,A.LastModifiedTime,A.AlasanBatal,A.AlasanClose,B.DepoID from POPurchn as A,SPP as B,POPurchnDetail as C where C.SPPID = B.ID and C.POID = A.ID and A.NoPO = " + noPO + " order by A.ID desc");
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectNew(sqlDataReader, GenerateObject(sqlDataReader));
                }
            }

            return new POPurchn();
        }

        public POPurchn RetrieveByNoWithDepo(string noPO, int depoID)
        {
            string strSQL = "select A.ID,A.NoPO,A.POPurchnDate,A.CreatedTime,A.SupplierID,A.Termin,A.PPN,A.Delivery,A.Crc,"+
                            "A.Keterangan,A.Terbilang,A.Disc,A.PPH,A.NilaiKurs,A.Cetak,A.CountPrt,A.Status,A.Approval,A.ApproveDate1,"+
                            "A.ApproveDate2,A.CreatedBy,A.LastModifiedBy,A.LastModifiedTime,A.AlasanBatal,A.AlasanClose,B.DepoID,A.Indent,"+
                            "isnull(C.dlvdate,'1/1/1900') as dlvdate  from POPurchn as A,SPP as B,POPurchnDetail as C "+
                            "where C.SPPID = B.ID and C.POID = A.ID and A.Status > -1 and A.NoPO = '" + noPO + "'  and B.DepoID = " + depoID + 
                            " order by A.ID desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new POPurchn();
        }

        public POPurchn RetrieveByNoWithDepo2(string noPO, int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "select A.ID,A.NoPO,A.POPurchnDate,A.CreatedTime,A.SupplierID,A.Termin,A.PPN,A.Delivery,A.Crc,"+
                            "A.Keterangan,A.Terbilang,A.Disc,A.PPH,A.NilaiKurs,A.Cetak,A.CountPrt,A.Status,A.Approval,A.ApproveDate1,"+
                            "A.ApproveDate2,A.CreatedBy,A.LastModifiedBy,A.LastModifiedTime,A.AlasanBatal,A.AlasanClose,B.DepoID,"+
                            "A.Indent,C.SppDetailID,C.ItemTypeID,C.ItemID,isnull(C.dlvdate,'1/1/1900') as dlvdate  from POPurchn as A,"+
                            "SPP as B,POPurchnDetail as C where C.SPPID = B.ID and C.POID = A.ID and A.Status > -1 and A.NoPO = '" + noPO + "' "+
                            "/*and B.DepoID = " + depoID + " */and C.Status >-1 order by A.ID desc";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new POPurchn();
        }



        public POPurchn RetrieveByID(int id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select *,(select useralias from users where username=POPurchn.createdby) useralias from POPurchn where ID = " + id );
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateSubCompanyID(sqlDataReader, GenerateObjectNodetail(sqlDataReader));
                }
            }

            return new POPurchn();
        }

        //
        public POPurchn RetrieveByNoCheckStatus(string noPO)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "select *,(select useralias from users where username=POPurchn.createdby) useralias from POPurchn where NoPO = '" + noPO + "' and status in (0,1) and Approval>=2 order by ID desc";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateSubCompanyID(sqlDataReader, GenerateObjectNodetail(sqlDataReader));
                }
            }

            return new POPurchn();
        }

        public POPurchn RetrieveUserDeptID(string noPO)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql =
            " select top 1 E.DeptID,case when LEN((select UnitKerjaID from Users usr where usr.ID=D.UserID))=1 "+
            " then (select top 1 ItemName from Asset where ItemCode=(SUBSTRING(F.ItemCode,8,13)) and RowStatus>-1) "+
            " when LEN((select UnitKerjaID from Users usr where usr.ID=D.UserID))=2 "+
            " then (select top 1 ItemName from Asset where ItemCode=(SUBSTRING(F.ItemCode,8,14)) and RowStatus>-1) else '' end MasterAssetKomponen  from POPurchn A " +
            " inner join POPurchnDetail B ON A.ID=B.POID and B.Status>-1 " +
            " inner join SPPDetail C ON C.ID=B.SppDetailID and C.Status>-1 " +
            " inner join SPP D on D.ID=C.SPPID and D.Status>-1 " +
            " inner join Users E ON E.ID=D.UserID and E.RowStatus>-1 " +
            " inner join Asset F ON F.ID=B.ItemID  "+
            " where A.NoPO='"+noPO+"' and A.Status>-1 ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateUserDeptID(sqlDataReader);
                }
            }

            return new POPurchn();
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            string strSQL="select A.ID,A.NoPO,A.POPurchnDate,A.CreatedTime,A.SupplierID,A.Termin,A.PPN,A.Delivery,A.Crc,A.Keterangan,"+
                          "A.Terbilang,A.Disc,A.PPH,A.NilaiKurs,A.Cetak,A.CountPrt,A.Status,A.Approval,A.ApproveDate1,A.ApproveDate2,"+
                          "A.CreatedBy,A.LastModifiedBy,A.LastModifiedTime,A.AlasanBatal,A.AlasanClose,B.DepoID,A.Indent,"+
                          "isnull(C.dlvdate,'1/1/1900') as dlvdate  from POPurchn as A,SPP as B,POPurchnDetail as C where C.SPPID = B.ID "+
                          "and C.POID = A.ID and A.Status > -1 and " + strField + " like '%" + strValue + "%'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPOPurchn.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrPOPurchn.Add(new POPurchn());

            return arrPOPurchn;
        }

        public ArrayList RetrieveHistPOByNo(string NoPO)
        {
            string strSQL = "select A.POPurchnDate as POPurchnDate,A.NoPO,C.NoSPP, " +
                    "case B.ItemTypeID " +
                    "when 1 then (select ItemCode from Inventory where ID = B.ItemID) " +
                    "when 2 then (select ItemCode from Asset where ID = B.ItemID) " +
                    "when 3 then (select ItemCode from Biaya where ID = B.ItemID) end ItemCode, " +
                    "case B.ItemTypeID " +
                    "when 1 then (select ItemName from Inventory where ID = B.ItemID) " +
                    "when 2 then (select ItemName from Asset where ID = B.ItemID) " +
                    "when 3 then "+ItemSPPBiayaNew("B")+" end ItemName, " +
                    "B.Price,B.Qty,D.UOMCode,E.SupplierCode,E.SupplierName,E.Telepon,A.Crc,A.Termin, " +
                    "A.Delivery,A.Disc,A.PPN,A.PPH,A.Status,A.AlasanBatal,A.AlasanClose,isnull(B.dlvdate,'1/1/1900') as dlvdate  " +
                    "from POPurchn as A, POPurchnDetail as B, SPP as C,UOM as D, SuppPurch as E " +
                    "where A.ID = B.POID and B.SPPID = C.ID and B.UOMID = D.ID " +
                    "and E.ID = A.SupplierID and A.NoPO = '" + NoPO + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPOPurchn.Add(GenerateObjectHistPO(sqlDataReader));
                }
            }
            else
                arrPOPurchn.Add(new POPurchn());

            return arrPOPurchn;
        }

        public ArrayList RetrieveHistPOBySPP(string NoPO)
        {
            string strSQL = "select A.POPurchnDate as POPurchnDate,A.NoPO,C.NoSPP, " +
                        "case B.ItemTypeID " +
                        "when 1 then (select ItemCode from Inventory where ID = B.ItemID) " +
                        "when 2 then (select ItemCode from Asset where ID = B.ItemID) " +
                        "when 3 then (select ItemCode from Biaya where ID = B.ItemID) end ItemCode, " +
                        "case B.ItemTypeID " +
                        "when 1 then (select ItemName from Inventory where ID = B.ItemID) " +
                        "when 2 then (select ItemName from Asset where ID = B.ItemID) " +
                        "when 3 then "+ItemSPPBiayaNew("B")+" end ItemName, " +
                        "B.Price,B.Qty,D.UOMCode,E.SupplierCode,E.SupplierName,E.Telepon,A.Crc,A.Termin, " +
                        "A.Delivery,A.Disc,A.PPN,A.PPH,A.Status,A.AlasanBatal,A.AlasanClose,isnull(B.dlvdate,'1/1/1900') as dlvdate  " +
                        "from POPurchn as A, POPurchnDetail as B, SPP as C,UOM as D, SuppPurch as E " +
                        "where A.ID = B.POID and B.SPPID = C.ID and B.UOMID = D.ID " +
                        "and E.ID = A.SupplierID and C.NoSPP = '" + NoPO + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPOPurchn.Add(GenerateObjectHistPO(sqlDataReader));
                }
            }
            else
                arrPOPurchn.Add(new POPurchn());

            return arrPOPurchn;
        }

        public ArrayList RetrieveHistPOBySupp(string NoPO)
        {
            string strSQL = "select A.POPurchnDate as POPurchnDate,A.NoPO,C.NoSPP, " +
                    "case B.ItemTypeID " +
                    "when 1 then (select ItemCode from Inventory where ID = B.ItemID) " +
                    "when 2 then (select ItemCode from Asset where ID = B.ItemID) " +
                    "when 3 then (select ItemCode from Biaya where ID = B.ItemID) end ItemCode, " +
                    "case B.ItemTypeID " +
                    "when 1 then (select ItemName from Inventory where ID = B.ItemID) " +
                    "when 2 then (select ItemName from Asset where ID = B.ItemID) " +
                    "when 3 then "+ItemSPPBiayaNew("B")+" end ItemName, " +
                    "B.Price,B.Qty,D.UOMCode,E.SupplierCode,E.SupplierName,E.Telepon,A.Crc,A.Termin, " +
                    "A.Delivery,A.Disc,A.PPN,A.PPH,A.Status,A.AlasanBatal,A.AlasanClose,isnull(B.dlvdate,'1/1/1900') as dlvdate " +
                    "from POPurchn as A, POPurchnDetail as B, SPP as C,UOM as D, SuppPurch as E " +
                    "where A.ID = B.POID and B.SPPID = C.ID and B.UOMID = D.ID " +
                    "and E.ID = A.SupplierID and E.SupplierName like '%" + NoPO + "%'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPOPurchn.Add(GenerateObjectHistPO(sqlDataReader));
                }
            }
            else
                arrPOPurchn.Add(new POPurchn());

            return arrPOPurchn;
        }

        public ArrayList RetrieveHistPOByNameInventory(string NoPO)
        {
            string strSQL = "select A.POPurchnDate as POPurchnDate,A.NoPO,C.NoSPP, " +
                "F.ItemCode,F.ItemName,B.Price,B.Qty,D.UOMCode,E.SupplierCode,E.SupplierName,E.Telepon,A.Crc,A.Termin, " +
                "A.Delivery,A.Disc,A.PPN,A.PPH,A.Status,A.AlasanBatal,A.AlasanClose,isnull(B.dlvdate,'1/1/1900') as dlvdate " +
                "from POPurchn as A, POPurchnDetail as B, SPP as C,UOM as D, SuppPurch as E,Inventory as F " +
                "where A.ID = B.POID and B.SPPID = C.ID and B.UOMID = D.ID " +
                "and E.ID = A.SupplierID and B.ItemID = F.ID and B.ItemTypeID = 1 and F.ItemName like '%" + NoPO + "%'";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPOPurchn.Add(GenerateObjectHistPO(sqlDataReader));
                }
            }
            else
                arrPOPurchn.Add(new POPurchn());

            return arrPOPurchn;
        }

        public ArrayList RetrieveHistPOByCodeInventory(string NoPO)
        {
            string strSQL = "select A.POPurchnDate as POPurchnDate,A.NoPO,C.NoSPP, " +
                "F.ItemCode,F.ItemName,B.Price,B.Qty,D.UOMCode,E.SupplierCode,E.SupplierName,E.Telepon,A.Crc,A.Termin, " +
                "A.Delivery,A.Disc,A.PPN,A.PPH,A.Status,A.AlasanBatal,A.AlasanClose,isnull(B.dlvdate,'1/1/1900') as dlvdate  " +
                "from POPurchn as A, POPurchnDetail as B, SPP as C,UOM as D, SuppPurch as E,Inventory as F " +
                "where A.ID = B.POID and B.SPPID = C.ID and B.UOMID = D.ID " +
                "and E.ID = A.SupplierID and B.ItemID = F.ID and B.ItemTypeID = 1 and F.ItemCode = '" + NoPO + "'";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPOPurchn.Add(GenerateObjectHistPO(sqlDataReader));
                }
            }
            else
                arrPOPurchn.Add(new POPurchn());

            return arrPOPurchn;
        }

        public ArrayList RetrieveHistPOByNameAsset(string NoPO)
        {
            string strSQL = "select A.POPurchnDate as POPurchnDate,A.NoPO,C.NoSPP, " +
                "F.ItemCode,F.ItemName,B.Price,B.Qty,D.UOMCode,E.SupplierCode,E.SupplierName,E.Telepon,A.Crc,A.Termin, " +
                "A.Delivery,A.Disc,A.PPN,A.PPH,A.Status,A.AlasanBatal,A.AlasanClose,isnull(B.dlvdate,'1/1/1900') as dlvdate  " +
                "from POPurchn as A, POPurchnDetail as B, SPP as C,UOM as D, SuppPurch as E,Inventory as F " +
                "where A.ID = B.POID and B.SPPID = C.ID and B.UOMID = D.ID " +
                "and E.ID = A.SupplierID and B.ItemID = F.ID and B.ItemTypeID = 2 and F.ItemName like '%" + NoPO + "%'";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPOPurchn.Add(GenerateObjectHistPO(sqlDataReader));
                }
            }
            else
                arrPOPurchn.Add(new POPurchn());

            return arrPOPurchn;
        }

        public ArrayList RetrieveHistPOByCodeAsset(string NoPO)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL="select A.POPurchnDate as POPurchnDate,A.NoPO,C.NoSPP, " +
                "F.ItemCode,F.ItemName,B.Price,B.Qty,D.UOMCode,E.SupplierCode,E.SupplierName,E.Telepon,A.Crc,A.Termin, " +
                "A.Delivery,A.Disc,A.PPN,A.PPH,A.Status,A.AlasanBatal,A.AlasanClose,isnull(B.dlvdate,'1/1/1900') as dlvdate  " +
                "from POPurchn as A, POPurchnDetail as B, SPP as C,UOM as D, SuppPurch as E,Inventory as F " +
                "where A.ID = B.POID and B.SPPID = C.ID and B.UOMID = D.ID " +
                "and E.ID = A.SupplierID and B.ItemID = F.ID and B.ItemTypeID = 2 and F.ItemCode = '" + NoPO + "'";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPOPurchn.Add(GenerateObjectHistPO(sqlDataReader));
                }
            }
            else
                arrPOPurchn.Add(new POPurchn());

            return arrPOPurchn;
        }

        public ArrayList RetrieveHistPOByNameBiaya(string NoPO)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL="select A.POPurchnDate as POPurchnDate,A.NoPO,C.NoSPP, " +
                "F.ItemCode,F.ItemName,B.Price,B.Qty,D.UOMCode,E.SupplierCode,E.SupplierName,E.Telepon,A.Crc,A.Termin, " +
                "A.Delivery,A.Disc,A.PPN,A.PPH,A.Status,A.AlasanBatal,A.AlasanClose,isnull(B.dlvdate,'1/1/1900') as dlvdate  " +
                "from POPurchn as A, POPurchnDetail as B, SPP as C,UOM as D, SuppPurch as E,Inventory as F " +
                "where A.ID = B.POID and B.SPPID = C.ID and B.UOMID = D.ID " +
                "and E.ID = A.SupplierID and B.ItemID = F.ID and B.ItemTypeID = 3 and F.ItemName like '%" + NoPO + "%'";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPOPurchn.Add(GenerateObjectHistPO(sqlDataReader));
                }
            }
            else
                arrPOPurchn.Add(new POPurchn());

            return arrPOPurchn;
        }

        public ArrayList RetrieveHistPOByCodeBiaya(string NoPO)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL="select A.POPurchnDate as POPurchnDate,A.NoPO,C.NoSPP, " +
                "F.ItemCode,F.ItemName,B.Price,B.Qty,D.UOMCode,E.SupplierCode,E.SupplierName,E.Telepon,A.Crc,A.Termin, " +
                "A.Delivery,A.Disc,A.PPN,A.PPH,A.Status,A.AlasanBatal,A.AlasanClose,isnull(B.dlvdate,'1/1/1900') as dlvdate  " +
                "from POPurchn as A, POPurchnDetail as B, SPP as C,UOM as D, SuppPurch as E,Inventory as F " +
                "where A.ID = B.POID and B.SPPID = C.ID and B.UOMID = D.ID " +
                "and E.ID = A.SupplierID and B.ItemID = F.ID and B.ItemTypeID = 3 and F.ItemCode = '" + NoPO + "'";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPOPurchn.Add(GenerateObjectHistPO(sqlDataReader));
                }
            }
            else
                arrPOPurchn.Add(new POPurchn());

            return arrPOPurchn;
        }

        public ArrayList RetrieveHistPOAll()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 50 A.POPurchnDate as POPurchnDate,A.NoPO,C.NoSPP, " +
                    "case B.ItemTypeID " +
                    "when 1 then (select ItemCode from Inventory where ID = B.ItemID) " +
                    "when 2 then (select ItemCode from Asset where ID = B.ItemID) " +
                    "when 3 then (select ItemCode from Biaya where ID = B.ItemID) end ItemCode, " +
                    "case B.ItemTypeID " +
                    "when 1 then (select ItemName from Inventory where ID = B.ItemID) " +
                    "when 2 then (select ItemName from Asset where ID = B.ItemID) " +
                    "when 3 then "+ItemSPPBiayaNew("B")+" end ItemName, " +
                    "B.Price,B.Qty,D.UOMCode,E.SupplierCode,E.SupplierName,E.Telepon,A.Crc,A.Termin, " +
                    "A.Delivery,A.Disc,A.PPN,A.PPH,A.Status,A.AlasanBatal,A.AlasanClose,isnull(B.dlvdate,'1/1/1900') as dlvdate  " +
                    "from POPurchn as A, POPurchnDetail as B, SPP as C,UOM as D, SuppPurch as E " +
                    "where A.ID = B.POID and B.SPPID = C.ID and B.UOMID = D.ID " +
                    "and E.ID = A.SupplierID order by POPurchnDate desc");
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPOPurchn.Add(GenerateObjectHistPO(sqlDataReader));
                }
            }
            else
                arrPOPurchn.Add(new POPurchn());

            return arrPOPurchn;
        }

        public POPurchn CekSisaPO(string poNo)
        {
            
            string strSQL="select ID,isnull(sum(QtyPO),0) as QtyPO,isnull(sum(QtyReceipt),0) as QtyReceipt from(select A.ID,B.Qty as QtyPO,"+
                           "case when A.ID>0 then (select isnull(sum(C.Quantity),0) from ReceiptDetail as C where C.PODetailID=B.ID and C.RowStatus>-1 and C.ItemID=B.ItemID) else 0 end QtyReceipt " +
                           "from POPurchn as A,POPurchnDetail as B where A.ID=B.POID and A.Status>-1 and B.Status>-1 and A.NoPO='"+poNo+"') as X group by ID";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString()); 
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectCek(sqlDataReader);
                }
            }

            return new POPurchn();
        }

        //public decimal CekSisaPO(string poNo)
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,isnull(sum(B.Qty),0)-isnull(sum(D.Quantity),0) as SisaPO from POPurchn as A, POPurchnDetail as B,Receipt as C, ReceiptDetail as D where A.ID=B.POID and A.ID=C.POID and C.ID=D.ReceiptID and B.ID=D.PODetailID and B.ItemID=D.ItemID and A.Status>-1 and B.Status>-1 and C.Status>-1 and D.RowStatus>-1 and A.NoPO= '" + poNo + "' group by A.ID");
        //    strError = dataAccess.Error;

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            return Convert.ToDecimal(sqlDataReader["SisaPO"]);
        //        }
        //    }

        //    return 0;
        //}

        public POPurchn GenerateObject(SqlDataReader sqlDataReader)
        {
            objPOPurchn = new POPurchn();
            objPOPurchn.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objPOPurchn.NoPO = sqlDataReader["NoPO"].ToString();
            objPOPurchn.POPurchnDate = Convert.ToDateTime(sqlDataReader["POPurchnDate"]);
            objPOPurchn.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objPOPurchn.SupplierID = Convert.ToInt32(sqlDataReader["SupplierID"]);
            objPOPurchn.Termin = sqlDataReader["Termin"].ToString();
            objPOPurchn.PPN = Convert.ToDecimal(sqlDataReader["PPN"]);
            objPOPurchn.Delivery = sqlDataReader["Delivery"].ToString();
            objPOPurchn.Crc = Convert.ToInt32(sqlDataReader["Crc"]);
            objPOPurchn.Keterangan = sqlDataReader["Keterangan"].ToString();
            objPOPurchn.Terbilang = sqlDataReader["Terbilang"].ToString();
            objPOPurchn.Disc = Convert.ToDecimal(sqlDataReader["Disc"]);
            objPOPurchn.PPH = Convert.ToDecimal(sqlDataReader["PPH"]);
            objPOPurchn.NilaiKurs = Convert.ToInt32(sqlDataReader["NilaiKurs"]);
            objPOPurchn.Cetak = Convert.ToInt32(sqlDataReader["Cetak"]);
            objPOPurchn.CountPrt = Convert.ToInt32(sqlDataReader["CountPrt"]);
            objPOPurchn.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objPOPurchn.Approval = Convert.ToInt32(sqlDataReader["Approval"]);
            objPOPurchn.ApproveDate1 =(sqlDataReader["ApproveDate1"]==DBNull.Value)?DateTime.MinValue: Convert.ToDateTime(sqlDataReader["ApproveDate1"]);
            objPOPurchn.ApproveDate2 = (sqlDataReader["ApproveDate2"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(sqlDataReader["ApproveDate2"]);
            objPOPurchn.AlasanBatal = sqlDataReader["AlasanBatal"].ToString();
            objPOPurchn.AlasanClose = sqlDataReader["AlasanClose"].ToString();
            objPOPurchn.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objPOPurchn.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objPOPurchn.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objPOPurchn.LastModifiedTime = (sqlDataReader["lastModifiedTime"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            objPOPurchn.Indent = sqlDataReader["Indent"].ToString();
            objPOPurchn.DlvDate = Convert.ToDateTime(sqlDataReader["dlvdate"]);
            return objPOPurchn;

        }

        public POPurchn GenerateObject2(SqlDataReader sqlDataReader)
        {
            objPOPurchn = new POPurchn();
            objPOPurchn.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objPOPurchn.NoPO = sqlDataReader["NoPO"].ToString();
            objPOPurchn.POPurchnDate = Convert.ToDateTime(sqlDataReader["POPurchnDate"]);
            objPOPurchn.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objPOPurchn.SupplierID = Convert.ToInt32(sqlDataReader["SupplierID"]);
            objPOPurchn.Termin = sqlDataReader["Termin"].ToString();
            objPOPurchn.PPN = Convert.ToDecimal(sqlDataReader["PPN"]);
            objPOPurchn.Delivery = sqlDataReader["Delivery"].ToString();
            objPOPurchn.Crc = Convert.ToInt32(sqlDataReader["Crc"]);
            objPOPurchn.Keterangan = sqlDataReader["Keterangan"].ToString();
            objPOPurchn.Terbilang = sqlDataReader["Terbilang"].ToString();
            objPOPurchn.Disc = Convert.ToDecimal(sqlDataReader["Disc"]);
            objPOPurchn.PPH = Convert.ToDecimal(sqlDataReader["PPH"]);
            objPOPurchn.NilaiKurs = Convert.ToInt32(sqlDataReader["NilaiKurs"]);
            objPOPurchn.Cetak = Convert.ToInt32(sqlDataReader["Cetak"]);
            objPOPurchn.CountPrt = Convert.ToInt32(sqlDataReader["CountPrt"]);
            objPOPurchn.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objPOPurchn.Approval = Convert.ToInt32(sqlDataReader["Approval"]);
            objPOPurchn.ApproveDate1 =(sqlDataReader["ApproveDate1"]==DBNull.Value)?DateTime.MaxValue: Convert.ToDateTime(sqlDataReader["ApproveDate1"]);
            objPOPurchn.ApproveDate2 = (sqlDataReader["ApproveDate2"] == DBNull.Value) ? DateTime.MaxValue : Convert.ToDateTime(sqlDataReader["ApproveDate2"]);
            objPOPurchn.AlasanBatal = sqlDataReader["AlasanBatal"].ToString();
            objPOPurchn.AlasanClose = sqlDataReader["AlasanClose"].ToString();
            objPOPurchn.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objPOPurchn.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objPOPurchn.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objPOPurchn.LastModifiedTime = (sqlDataReader["LastModifiedTime"] == DBNull.Value) ? DateTime.MaxValue : Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            objPOPurchn.Indent = sqlDataReader["Indent"].ToString();
            objPOPurchn.SPPDetailID = Convert.ToInt32(sqlDataReader["SPPDetailID"]);
            objPOPurchn.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"]);
            objPOPurchn.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objPOPurchn.DlvDate = Convert.ToDateTime(sqlDataReader["dlvdate"]);
            return objPOPurchn;

        }

        public POPurchn GenerateObjectList(SqlDataReader sqlDataReader)
        {
            objPOPurchn = new POPurchn();
            objPOPurchn.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objPOPurchn.NoPO = sqlDataReader["NoPO"].ToString();
            objPOPurchn.POPurchnDate = Convert.ToDateTime(sqlDataReader["POPurchnDate"]);
            objPOPurchn.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objPOPurchn.SupplierID = Convert.ToInt32(sqlDataReader["SupplierID"]);
            objPOPurchn.Termin = sqlDataReader["Termin"].ToString();
            objPOPurchn.PPN = Convert.ToDecimal(sqlDataReader["PPN"]);
            objPOPurchn.Delivery = sqlDataReader["Delivery"].ToString();
            objPOPurchn.Crc = Convert.ToInt32(sqlDataReader["Crc"]);
            objPOPurchn.Keterangan = sqlDataReader["Keterangan"].ToString();
            objPOPurchn.Terbilang = sqlDataReader["Terbilang"].ToString();
            objPOPurchn.Disc = Convert.ToDecimal(sqlDataReader["Disc"]);
            objPOPurchn.PPH = Convert.ToDecimal(sqlDataReader["PPH"]);
            objPOPurchn.NilaiKurs = Convert.ToInt32(sqlDataReader["NilaiKurs"]);
            objPOPurchn.Cetak = Convert.ToInt32(sqlDataReader["Cetak"]);
            objPOPurchn.CountPrt = Convert.ToInt32(sqlDataReader["CountPrt"]);
            objPOPurchn.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objPOPurchn.Approval = Convert.ToInt32(sqlDataReader["Approval"]);
            objPOPurchn.ApproveDate1 = (sqlDataReader["ApproveDate1"] == DBNull.Value) ? DateTime.MaxValue : Convert.ToDateTime(sqlDataReader["ApproveDate1"]);
            objPOPurchn.ApproveDate2 = (sqlDataReader["ApproveDate2"] == DBNull.Value) ? DateTime.MaxValue : Convert.ToDateTime(sqlDataReader["ApproveDate2"]);
            objPOPurchn.AlasanBatal = sqlDataReader["AlasanBatal"].ToString();
            objPOPurchn.AlasanClose = sqlDataReader["AlasanClose"].ToString();
            objPOPurchn.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objPOPurchn.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objPOPurchn.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objPOPurchn.LastModifiedTime = (sqlDataReader["LastModifiedTime"] == DBNull.Value) ? DateTime.MaxValue : Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            objPOPurchn.DlvDate = Convert.ToDateTime(sqlDataReader["dlvdate"]);
            return objPOPurchn;

        }


        public POPurchn GenerateObjectAllPO(SqlDataReader sqlDataReader)
        {
            objPOPurchn = new POPurchn();
            objPOPurchn.NoPO = sqlDataReader["NoPO"].ToString();
            objPOPurchn.NOSPP = sqlDataReader["NOSPP"].ToString();
            objPOPurchn.ItemCode = sqlDataReader["ItemCode"].ToString();
            objPOPurchn.NamaBarang = sqlDataReader["NamaBarang"].ToString();
            objPOPurchn.Qty = Convert.ToDecimal(sqlDataReader["Qty"]);
            objPOPurchn.Satuan = sqlDataReader["Satuan"].ToString();
            objPOPurchn.Price = Convert.ToInt64(sqlDataReader["Price"]);
            objPOPurchn.DlvDate = Convert.ToDateTime(sqlDataReader["dlvdate"]);
            return objPOPurchn;

        }


        public POPurchn GenerateObjectCek(SqlDataReader sqlDataReader)
        {
            objPOPurchn = new POPurchn();
            objPOPurchn.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objPOPurchn.QtyPO = Convert.ToDecimal(sqlDataReader["QtyPO"]);
            objPOPurchn.QtyReceipt = Convert.ToDecimal(sqlDataReader["QtyReceipt"]);

            return objPOPurchn;

        }
        private POPurchn GenerateSubCompanyID(SqlDataReader sqlDataReader, POPurchn popurchn)
        {
            objPOPurchn = (POPurchn)popurchn;
            objPOPurchn.SubCompanyID = (sqlDataReader["SubCompanyID"] != DBNull.Value) ? int.Parse(sqlDataReader["SubCompanyID"].ToString()) : 0;
            objPOPurchn.CreatedBy = sqlDataReader["UserAlias"].ToString();
            return objPOPurchn;
        }
        public POPurchn GenerateObjectViewPO(SqlDataReader sqlDataReader)
        {
            objPOPurchn = new POPurchn();
            objPOPurchn.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objPOPurchn.POID = Convert.ToInt32(sqlDataReader["POID"]);
            objPOPurchn.NoPO = sqlDataReader["NoPO"].ToString();
            objPOPurchn.Termin = sqlDataReader["Termin"].ToString();
            objPOPurchn.Delivery = sqlDataReader["Delivery"].ToString();
            objPOPurchn.NOSPP = sqlDataReader["NOSPP"].ToString();
            objPOPurchn.Satuan = sqlDataReader["Satuan"].ToString();
            objPOPurchn.SupplierName = sqlDataReader["SupplierName"].ToString();
            objPOPurchn.UP = sqlDataReader["UP"].ToString();
            objPOPurchn.Telepon = sqlDataReader["Telepon"].ToString();
            objPOPurchn.Jumlah = Convert.ToDecimal(sqlDataReader["Jumlah"]);
            objPOPurchn.Fax = sqlDataReader["Fax"].ToString();
            objPOPurchn.SPPID = Convert.ToInt32(sqlDataReader["SPPID"]);
            objPOPurchn.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objPOPurchn.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objPOPurchn.Price = Convert.ToDecimal(sqlDataReader["Price"]);
            objPOPurchn.Qty = Convert.ToDecimal(sqlDataReader["Qty"]);
            objPOPurchn.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"]);
            objPOPurchn.UOMID = Convert.ToInt32(sqlDataReader["UOMID"]);
            objPOPurchn.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objPOPurchn.NoUrut = Convert.ToInt32(sqlDataReader["NoUrut"]);
            objPOPurchn.SPPDetailID = Convert.ToInt32(sqlDataReader["SPPDetailID"]);
            objPOPurchn.DocumentNo = sqlDataReader["DocumentNo"].ToString();
            objPOPurchn.NamaBarang = sqlDataReader["NamaBarang"].ToString();
            objPOPurchn.ItemCode = sqlDataReader["ItemCode"].ToString();
            objPOPurchn.Disc = Convert.ToDecimal(sqlDataReader["Disc"]);
            objPOPurchn.PPN = Convert.ToDecimal(sqlDataReader["PPN"]);
            objPOPurchn.PPH = Convert.ToDecimal(sqlDataReader["PPH"]);
            objPOPurchn.Crc = Convert.ToInt32(sqlDataReader["Crc"]);
            objPOPurchn.ItemFrom = Convert.ToInt32(sqlDataReader["Itemfrom"]);
            objPOPurchn.DlvDate = Convert.ToDateTime(sqlDataReader["dlvdate"]);
            objPOPurchn.Ongkos = Convert.ToDecimal(sqlDataReader["ongkos"]);
            objPOPurchn.Remark = (sqlDataReader["remark"]).ToString();
            objPOPurchn.AlasanNotApproval = (sqlDataReader["alasannotapproval"]).ToString();
            objPOPurchn.Approval = Convert.ToInt32(sqlDataReader["Approval"].ToString());
            return objPOPurchn;

        }

        public POPurchn GenerateObjectHistPO(SqlDataReader sqlDataReader)
        {
            objPOPurchn = new POPurchn();
            objPOPurchn.POPurchnDate = Convert.ToDateTime(sqlDataReader["POPurchnDate"]);
            objPOPurchn.NoPO = sqlDataReader["NoPO"].ToString();
            objPOPurchn.NOSPP = sqlDataReader["NOSPP"].ToString();
            objPOPurchn.ItemCode = sqlDataReader["ItemCode"].ToString();
            objPOPurchn.ItemName = sqlDataReader["ItemName"].ToString();
            objPOPurchn.Price = Convert.ToDecimal(sqlDataReader["Price"]);
            objPOPurchn.Qty = Convert.ToDecimal(sqlDataReader["Qty"]);
            objPOPurchn.UOMCode = sqlDataReader["UOMCode"].ToString();
            objPOPurchn.SupplierCode = sqlDataReader["SupplierCode"].ToString();
            objPOPurchn.SupplierName = sqlDataReader["SupplierName"].ToString();
            objPOPurchn.Telepon = sqlDataReader["Telepon"].ToString();
            objPOPurchn.Crc = Convert.ToInt32(sqlDataReader["Crc"]);
            objPOPurchn.Delivery = sqlDataReader["Delivery"].ToString();
            objPOPurchn.Termin = sqlDataReader["Termin"].ToString();
            objPOPurchn.Disc = Convert.ToDecimal(sqlDataReader["Disc"]);
            objPOPurchn.PPN = Convert.ToDecimal(sqlDataReader["PPN"]);
            objPOPurchn.PPH = Convert.ToDecimal(sqlDataReader["PPH"]);
            objPOPurchn.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objPOPurchn.AlasanBatal = sqlDataReader["AlasanBatal"].ToString();
            objPOPurchn.AlasanClose = sqlDataReader["AlasanClose"].ToString();
            objPOPurchn.DlvDate = Convert.ToDateTime(sqlDataReader["dlvdate"]);
            return objPOPurchn;

        }
        private POPurchn GenerateObjectHistPO(SqlDataReader sdr, POPurchn PoDomain)
        {
            objPOPurchn = (POPurchn)PoDomain;
            objPOPurchn.ID = int.Parse(sdr["ID"].ToString());
            objPOPurchn.ReceiptNo = sdr["ReceiptNo"].ToString();
            return objPOPurchn;
        }
        public POPurchn GenerateObjectNodetail(SqlDataReader sqlDataReader)
        {
            objPOPurchn = new POPurchn();
            objPOPurchn.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objPOPurchn.NoPO = sqlDataReader["NoPO"].ToString();
            objPOPurchn.POPurchnDate = Convert.ToDateTime(sqlDataReader["POPurchnDate"]);
            objPOPurchn.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objPOPurchn.SupplierID = Convert.ToInt32(sqlDataReader["SupplierID"]);
            objPOPurchn.Termin = sqlDataReader["Termin"].ToString();
            objPOPurchn.PPN = Convert.ToDecimal(sqlDataReader["PPN"]);
            objPOPurchn.Delivery = sqlDataReader["Delivery"].ToString();
            objPOPurchn.Crc = Convert.ToInt32(sqlDataReader["Crc"]);
            objPOPurchn.Keterangan = sqlDataReader["Keterangan"].ToString();
            objPOPurchn.Terbilang = sqlDataReader["Terbilang"].ToString();
            objPOPurchn.Disc = Convert.ToDecimal(sqlDataReader["Disc"]);
            objPOPurchn.PPH = Convert.ToDecimal(sqlDataReader["PPH"]);
            objPOPurchn.NilaiKurs = Convert.ToInt32(sqlDataReader["NilaiKurs"]);
            objPOPurchn.Cetak = Convert.ToInt32(sqlDataReader["Cetak"]);
            objPOPurchn.CountPrt = Convert.ToInt32(sqlDataReader["CountPrt"]);
            objPOPurchn.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objPOPurchn.Approval = Convert.ToInt32(sqlDataReader["Approval"]);
            objPOPurchn.ApproveDate1 = (sqlDataReader["ApproveDate1"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(sqlDataReader["ApproveDate1"]);
            objPOPurchn.ApproveDate2 = (sqlDataReader["ApproveDate2"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(sqlDataReader["ApproveDate2"]);
            objPOPurchn.AlasanBatal = sqlDataReader["AlasanBatal"].ToString();
            objPOPurchn.AlasanClose = sqlDataReader["AlasanClose"].ToString();
            objPOPurchn.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objPOPurchn.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objPOPurchn.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objPOPurchn.LastModifiedTime =(sqlDataReader["LastModifiedTime"]==DBNull.Value)?DateTime.MinValue: Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            objPOPurchn.Indent = sqlDataReader["Indent"].ToString();
            objPOPurchn.Ongkos = Convert.ToDecimal(sqlDataReader["ongkos"]);
            objPOPurchn.UangMuka = Convert.ToDecimal(sqlDataReader["UangMuka"]);
            objPOPurchn.CoaID = Convert.ToInt32(sqlDataReader["CoaID"]);
            objPOPurchn.Remark  = (sqlDataReader["remark"]).ToString() ;
            return objPOPurchn;

        }
           /**
         * added on 28-04-2014
         * untuk perubahan pada itemname table biaya
         * dan stock per itemnya
         */
        private POPurchn GenerateObjectNew(SqlDataReader sdr, POPurchn Obj)
        {
            POPurchn objPOPurchn = (POPurchn)Obj;
            objPOPurchn.PODetailID = Convert.ToInt32(sdr["PODetailID"].ToString());
            return objPOPurchn;
        }
        public string ItemSPPBiayaNew(string TableName)
        {
            string strSQL = "CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())< "+
                " (Select CreatedTime from SPP where SPP.ID="+TableName+".SPPID)) " +
                " THEN(select ItemName from Biaya where ID="+ TableName +".ItemID and Biaya.RowStatus>-1)+' - '+ " +
                " (Select SPPDetail.Keterangan From SPPDetail where SPPDetail.ID=" + TableName + ".SPPDetailID) ELSE " +
                " (select ItemName from biaya where ID=" + TableName + ".ItemID and biaya.RowStatus>-1) END ";
            return strSQL;
        }
        public string CodeSPPBiayaNew(string TableName)
        {
            string strSQL = "CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())< " +
                " (Select CreatedTime from SPP where SPP.ID=" + TableName + ".SPPID)) " +
                " THEN(select TOP 1 ItemCode from Biaya where ItemName="+
                " (Select SPPDetail.Keterangan From SPPDetail where SPPDetail.ID=" + TableName + ".SPPDetailID)) ELSE " +
                " (select ItemCode from biaya where ID=" + TableName + ".ItemID and biaya.RowStatus>-1) END ";
            return strSQL;
        }
        public string StockBiayaNew()
        {
            string strSQL = "CASE WHEN (isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())< "+
                " (Select CreatedTime from SPP where SPP.ID=POPurchnDetail.SPPID)) THEN " +
                " (SELECT isnull(sum(jumlah),0) from Biaya where ItemName=(Select SPPDetail.Keterangan From SPPDetail where SPPDetail.ItemID=POPurchnDetail.ItemID and SPPDetail.SPPID=POPurchnDetail.SPPID)) " +
                " ELSE (SELECT isnull(sum(Jumlah),0) from Biaya where ID=POPurchnDetail.ItemID and Biaya.RowStatus>-1) END";
            return strSQL;
        }
        public POPurchn PurchnTools(string ModulName)
        {
            string strSQL = "Select Status From Purchn_tools where ModulName='" + ModulName + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectPurchTools(sqlDataReader);
                }
            }

            return new POPurchn();

        }

        public POPurchn DayOffCalender(string FromDate, string ToDate)
        {
            POPurchn pp = new POPurchn();
            string strSQL = "Select count(HariLibur) as Status from CalenderOffDay where  HariLibur Between '"+FromDate+"' and '"+ToDate+"'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    pp= GenerateObjectPurchTools(sqlDataReader);
                }
            }

            return pp;
        }
        public POPurchn DayOffWeekEnd(string FromDate, string ToDate)
        {
            POPurchn pp = new POPurchn();
            string strSQL = "select dbo.GetOFFDay('" + FromDate + "','" + ToDate + "') AS Status";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    pp = GenerateObjectPurchTools(sqlDataReader);
                }
            }

            return pp;
        }
        public POPurchn DayOffCalender(string StartDate, string ToDate, bool CheckHariSabtuMinggu)
        {
            POPurchn pp = new POPurchn();
            string strSQL = "select *,(SELECT Datename(dw,HariLibur))NamaHari from CalenderOffDay where  Convert(CHAR,HariLibur,112) " +
                          "Between '" + StartDate + "' and '" + ToDate + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    pp= GenerateObjectPurchTools(sqlDataReader,CheckHariSabtuMinggu);
                }
            }
            return pp;
        }
        public decimal GetHargaKertas(int SupplierID,int ItemID)
        {
            Users user = (Users)System.Web.HttpContext.Current.Session["Users"];
            decimal result=0;
            string strSQL = "SELECT Top 1 * FROM HargaKertasBaru h " +
                          "  WHERE h.RowStatus>-1 AND h.SubCompanyID=(SELECT SubCompanyID FROM SuppPurch s WHERE s.ID=" + SupplierID +") "+
                          "  AND h.ItemCode=(SELECT ItemCode FROM Inventory where ID=" + ItemID + ")" +
                          "  AND h.PlantID=" + user.UnitKerjaID.ToString()+
                          "  ORDER By h.ID Desc";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = decimal.Parse(sdr["Harga"].ToString());
                }
            }
            return result;
        }
        private POPurchn GenerateObjectPurchTools(SqlDataReader sqlDataReader)
        {
            objPOPurchn = new POPurchn();
            objPOPurchn.Status = Convert.ToInt32(sqlDataReader["Status"]);
            return objPOPurchn;
        }
        private POPurchn GenerateObjectPurchTools(SqlDataReader sqlDataReader, bool mode2)
        {
            objPOPurchn = new POPurchn();
            objPOPurchn.DlvDate = DateTime.Parse(sqlDataReader["HariLibur"].ToString());
            objPOPurchn.Remark =sqlDataReader["NamaHari"].ToString();
            return objPOPurchn;
        }
        public string PoTermin(string Termin)
        {
            string strSQL = "select ID from TermOfPay where termpay='" + Termin + "'";
            return strSQL;
        }
        public DateTime DeliverySCHonPO(string SCH)
        {
            DateTime sch = DateTime.MinValue;
            string strSQL = "DECLARE @Sch date " +
                           "SET DATEFIRST 1; " +
                           "SET @Sch='" + SCH + "'; " +
                           "SELECT CASE WHEN (SELECT dbo.GetOFFDay(poDel,@Sch))>0 THEN " +
                           "DATEADD(DAY,(SELECT dbo.GetOFFDay(poDel,@Sch)),poDel) ELSE poDel END NewDel FROM ( " +
                           "SELECT CASE ( " +
                           "SELECT DATEPART(WEEKDAY,(DATEADD(DAY,-2,@Sch))))  " +
                           "WHEN 7 THEN DATEADD(DAY,-1,@Sch) ELSE DATEADD(DAY,-2,@Sch) END poDel ) AS X";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = dataAccess.RetrieveDataByString(strSQL);
            if (dataAccess.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    sch = Convert.ToDateTime(sdr["NewDel"].ToString());
                }
            }
            return sch;
        }
        public DateTime LeadTime(string SPPDetailID, string ItemID)
        {
            DateTime leadTime = DateTime.MinValue;
            string strSQL = "SET DATEFIRST 1; " +
                            "select *,case when (ReceiptDate is not null and ReceiptDate > Sch) then (select dbo.GetWorkingDay(Sch,ReceiptDate)) else 0 end Late from  " +
                            "(select ID,SPPID,ItemName,Quantity,QtyPO,Keterangan,LeadTime,AppDate,tglkirim, " +
                            "((case when Deliv is null and PermintaanType=2 then (select dbo.GetDateLeadTime(convert(char,AppDate,112),LeadTime)) " +
                            "when (Deliv is null and PermintaanType=1) then AppDate " +
	                        "when (Deliv is null and PermintaanType=3) then tglkirim  else Deliv  end) )  " +
                            " Sch,ReceiptDate,ReceiptNo,Status,PendingPO,PermintaanType,case when (Status=0 and (PendingPO=0 or PendingPO is null)) then ''   " +
                            "when (status=0 and PendingPO=1) then 'Spesifikasi tidak lengkap'   when (status=1) then 'Menunggu Perbandingan Harga'   " +
                            "when (status=2) Then 'Status PO' end Stat,ItemID from( select *,Case (select ItemTypeID from SPP where ID=SppDetail.SPPID)   " +
                            "When 1 then (select ItemName From Inventory where ID=ItemID)  " +
                            "When 2 then (select ItemName From Asset where ID=ItemID)   " +
                            "when 3 then SPPDetail.Keterangan end ItemName,  " +
                            "Case (select ItemTypeID from SPP where ID=SppDetail.SPPID)  " +
                            " when 1 then isnull((select LeadTime From Inventory where ID=ItemID),0)  " +
                            " when 2 then isnull((select LeadTime From Asset where ID=ItemID),0)   " +
                            " when 3 then isnull((select LeadTime From Biaya where ItemName=SPPDetail.Keterangan),0) end LeadTime,  " +
                            "(select ApproveDate3 from SPP where ID=SPPID)AppDate,   " +
                            "(select top 1 r.ReceiptDate from ReceiptDetail as rd  Left Join Receipt as r  on r.ID=rd.ReceiptID   " +
                            "where PODetailID in(select ID from POPurchnDetail where SppDetailID=SPPDetail.ID and Status >-1)   " +
                            "and rd.RowStatus>-1 and r.Status>-1) as ReceiptDate,  " +
                            "(select top 1 r.ReceiptNo from ReceiptDetail as rd    " +
                            "Left Join Receipt as r  on r.ID=rd.ReceiptID    " +
                            "where PODetailID in(select ID from POPurchnDetail where SppDetailID=SPPDetail.ID and Status >-1)   " +
                            "and rd.RowStatus>-1 and r.Status>-1) as ReceiptNo,   " +
                            "(select top 1 DlvDate from POPurchnDetail where SppDetailID=SPPDetail.ID and Status>-1 and ItemID=SPPDetail.ItemID) Deliv,  " +
                            "(select PermintaanType From SPP where ID=SPPDetail.SPPID) PermintaanType   " +
                            "from SPPDetail where Status >-1 and  SPPID=" + SPPDetailID + " ) as x ) as m  " +
                            "where m.ItemID=" + ItemID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = dataAccess.RetrieveDataByString(strSQL);
            if (dataAccess.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    leadTime = Convert.ToDateTime(sdr["Sch"].ToString());
                }
            }
            return leadTime;
        }

        public int GetPORevision(int POID)
        {
            int result = 0;
            string strSQL = "Select isnull(Max(RevisiKe),0)RevisiKe from POPurchnRevisi where POID=" + POID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToInt32(sdr["RevisiKe"].ToString());
                }
            }
            return result;
        }

        //add by razib
        public decimal GetJumlah(int POID)
        {
            decimal result = 0;
            string strSQL = " select Jumlah,Price from ( " +
                            " select A.ID,A.POID,B.NoPO,Convert(varchar,B.POPurchnDate,106) as CreatedTime,B.Termin,B.Delivery,B.Crc,C.NoSPP,UPPER(SUBSTRING(D.UOMCode,1,1)) + lower(SUBSTRING(D.UOMCode,2,LEN(D.UOMCode)-1)) as UOMCode, E.SupplierName,E.UP,E.Telepon,CASE WHEN E.flag=0 and B.NilaiKurs>0 and B.Crc>1 THEN (A.Qty*(A.Price*B.NilaiKurs))ELSE(A.Qty*A.Price) END as Jumlah, " +
                            " E.Fax,A.SPPID,A.GroupID,A.ItemID, CASE WHEN E.flag=0 and B.NilaiKurs>0 and B.Crc>1 THEN (B.NilaiKurs*A.Price) ELSE A.Price END as Price," +
                            " A.Qty,A.ItemTypeID,A.UOMID,A.Status,A.NoUrut,A.SPPDetailID,A.DocumentNo, case A.ItemTypeID when 1  " +
                            " then (select UPPER(SUBSTRING(ItemName,1,1)) + lower(SUBSTRING(ItemName,2,LEN(ItemName)-1)) as ItemName  from Inventory where ID=A.ItemID and RowStatus > -1) when 2 then (select UPPER(SUBSTRING(ItemName,1,1)) + lower(SUBSTRING(ItemName,2,LEN(ItemName)-1)) as ItemName  from Asset where ID=A.ItemID and RowStatus > -1) " +
                            " else CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())<=  (Select CreatedTime from SPP where SPP.ID=A.SPPID))  THEN (Select UPPER(SUBSTRING(SPPDetail.Keterangan,1,1))+ Lower(SUBSTRING(SPPDetail.Keterangan,2,LEN(SPPDetail.Keterangan)-1)) From SPPDetail where SPPDetail.ID=A.SPPDetailID /*and  SPPDetail.SPPID=A.SPPID*/) ELSE " +
                            " (select ItemName from biaya where ID=A.ItemID and biaya.RowStatus>-1) END  end ItemName, case A.ItemTypeID when 1  then (select ItemCode from Inventory where ID=A.ItemID and RowStatus > -1) when 2 then (select ItemCode from Asset where ID=A.ItemID and RowStatus > -1) else (select ItemCode from Biaya where ID=A.ItemID and RowStatus > -1) end ItemCode," +
                            " convert(varchar,isnull(dlvdate,'1/1/1900'),103)  as dlvdate ,Convert(varchar,(Select dbo.GetSchDeliveryOnPO(DlvDate)),103)SchOnPO,C.PermintaanType from POPurchnDetail as A,POPurchn as B,SPP as C,UOM as D,SuppPurch as E where A.status>-1 " +
                            " and A.POID = B.ID and A.SPPID = C.ID and A.UOMID = D.ID and B.SupplierID = E.ID and A.POID =" + POID + " ) as zxc";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToDecimal(sdr["Jumlah"].ToString());
                }
            }
            return result;
        }

        //add by razib
        public decimal GetPriceX(int POID)
        {
            decimal result = 0;
            string strSQL = " select Jumlah,Price from ( " +
                            " select A.ID,A.POID,B.NoPO,Convert(varchar,B.POPurchnDate,106) as CreatedTime,B.Termin,B.Delivery,B.Crc,C.NoSPP,UPPER(SUBSTRING(D.UOMCode,1,1)) + lower(SUBSTRING(D.UOMCode,2,LEN(D.UOMCode)-1)) as UOMCode, E.SupplierName,E.UP,E.Telepon,CASE WHEN E.flag=0 and B.NilaiKurs>0 and B.Crc>1 THEN (A.Qty*(A.Price*B.NilaiKurs))ELSE(A.Qty*A.Price) END as Jumlah, " +
                            " E.Fax,A.SPPID,A.GroupID,A.ItemID, CASE WHEN E.flag=0 and B.NilaiKurs>0 and B.Crc>1 THEN (B.NilaiKurs*A.Price) ELSE A.Price END as Price," +
                            " A.Qty,A.ItemTypeID,A.UOMID,A.Status,A.NoUrut,A.SPPDetailID,A.DocumentNo, case A.ItemTypeID when 1  " +
                            " then (select UPPER(SUBSTRING(ItemName,1,1)) + lower(SUBSTRING(ItemName,2,LEN(ItemName)-1)) as ItemName  from Inventory where ID=A.ItemID and RowStatus > -1) when 2 then (select UPPER(SUBSTRING(ItemName,1,1)) + lower(SUBSTRING(ItemName,2,LEN(ItemName)-1)) as ItemName  from Asset where ID=A.ItemID and RowStatus > -1) " +
                            " else CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())<=  (Select CreatedTime from SPP where SPP.ID=A.SPPID))  THEN (Select UPPER(SUBSTRING(SPPDetail.Keterangan,1,1))+ Lower(SUBSTRING(SPPDetail.Keterangan,2,LEN(SPPDetail.Keterangan)-1)) From SPPDetail where SPPDetail.ID=A.SPPDetailID /*and  SPPDetail.SPPID=A.SPPID*/) ELSE " +
                            " (select ItemName from biaya where ID=A.ItemID and biaya.RowStatus>-1) END  end ItemName, case A.ItemTypeID when 1  then (select ItemCode from Inventory where ID=A.ItemID and RowStatus > -1) when 2 then (select ItemCode from Asset where ID=A.ItemID and RowStatus > -1) else (select ItemCode from Biaya where ID=A.ItemID and RowStatus > -1) end ItemCode," +
                            " convert(varchar,isnull(dlvdate,'1/1/1900'),103)  as dlvdate ,Convert(varchar,(Select dbo.GetSchDeliveryOnPO(DlvDate)),103)SchOnPO,C.PermintaanType from POPurchnDetail as A,POPurchn as B,SPP as C,UOM as D,SuppPurch as E where A.status>-1 " +
                            " and A.POID = B.ID and A.SPPID = C.ID and A.UOMID = D.ID and B.SupplierID = E.ID and A.POID =" + POID + " ) as zxc";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToDecimal(sdr["Price"].ToString());
                }
            }
            return result;
        }

        public string GetAppGroup(int IDUsers)
        {
            string result = string.Empty;
            try
            {
                string strSQL = "Select AppGroup from UsersApp where RowStatus>-1 and UserID=" + IDUsers.ToString();
                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(strSQL);
                if (da.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        result = sdr["AppGroup"].ToString();
                    }
                }
                return result;
            }
            catch
            {
                return result;
            }
        }
        public string GetIDBiaya(string ItemName)
        {
            string result = string.Empty;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString("Select top 1 ID from Biaya Where ItemName='" + ItemName.TrimStart().TrimEnd() + "' order by ID desc");
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = sdr["ID"].ToString();
                }
            }
            return result;
        }
        public ArrayList HargaRendah(string ItemID, int ItemTypeID)
        {
            arrPOPurchn = new ArrayList();
            string strSQL = "SELECT TOP 1 pd.ID, pd.ItemID,pd.Price,po.SupplierID,sp.SupplierName " +
                            "FROM POPurchnDetail pd " +
                            "LEFT JOIN POPurchn AS po ON po.ID=pd.POID " +
                            "LEFT JOIN SuppPurch AS sp ON sp.ID=po.SupplierID " +
                            "WHERE itemid=" + ItemID + "  AND po.Approval>-1 AND pd.ItemTypeID=" + ItemTypeID +
                            " ORDER BY Price ASC, ID DESC";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrPOPurchn.Add(new POPurchn
                    {
                        ItemID = Convert.ToInt32(sdr["ItemID"].ToString()),
                        Price = Convert.ToDecimal(sdr["Price"].ToString()),
                        SupplierName = sdr["SupplierName"].ToString()
                    });
                }
            }
            return arrPOPurchn;
        }
        public ArrayList RetrieveUnApprove(string CreatedBy)
        {
            arrPOPurchn = new ArrayList();
            string strSql = "SELECT top 1000 * FROM POPurchn where CreatedBy='" + CreatedBy + "' and Status>-1 and Approval=0 and AlasanNotApproval!=''";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSql);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrPOPurchn.Add(new POPurchn
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        NoPO = sdr["NoPO"].ToString(),
                        AlasanNotApproval = sdr["AlasanNotApproval"].ToString(),
                        LastModifiedBy = sdr["LastModifiedBy"].ToString(),
                        LastModifiedTime = Convert.ToDateTime(sdr["LastModifiedTime"].ToString()),
                        CreatedBy = sdr["CreatedBy"].ToString()
                    });
                }
            }
            return arrPOPurchn;
        }
        public void GetTahun(DropDownList ddl)
        {
            ArrayList arrData = new ArrayList();
            string strSQL = "select distinct YEAR(CreatedTime) Tahun from POPurchn order by year(CreatedTime)";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddl.Items.Add(new ListItem(sdr["Tahun"].ToString(), sdr["Tahun"].ToString()));
                }
            }
        }
        public ArrayList RetrieveHistPOAll2(string Bulan, string Tahun)
        {
            string strSQl = "select B.ID, A.POPurchnDate as POPurchnDate,A.NoPO,C.NoSPP, " +
                            "case B.ItemTypeID " +
                            "when 1 then (select ItemCode from Inventory where ID = B.ItemID) " +
                            "when 2 then (select ItemCode from Asset where ID = B.ItemID) " +
                            "when 3 then (select ItemCode from Biaya where ID = B.ItemID) end ItemCode, " +
                            "case B.ItemTypeID " +
                            "when 1 then (select ItemName from Inventory where ID = B.ItemID) " +
                            "when 2 then (select ItemName from Asset where ID = B.ItemID) " +
                            "when 3 then " + ItemSPPBiayaNew("B") + " end ItemName, " +
                            "B.Price,B.Qty,D.UOMCode,E.SupplierCode,E.SupplierName,E.Telepon,F.ReceiptNo,A.Crc,A.Termin, " +
                            "A.Delivery,A.Disc,A.PPN,A.PPH,A.Status,A.AlasanBatal,A.AlasanClose,isnull(B.dlvdate,'1/1/1900') as dlvdate  " +
                            "from POPurchn as A, POPurchnDetail as B, SPP as C,UOM as D, SuppPurch as E, Receipt as F " +
                            "where A.ID = B.POID and B.SPPID = C.ID and B.UOMID = D.ID " +
                            "and E.ID = A.SupplierID and B.Price = 0 and (B.Price2 = 0 or B.Price2 is Null) and A.ID = F.POID " +
                            "and Month(A.POPurchnDate)=" + Bulan + " and Year(A.POPurchnDate)=" + Tahun +
                            " and B.Status>-1 and A.Status>-1 and F.Status >-1 " +
                            " order by POPurchnDate desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQl);
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPOPurchn.Add(GenerateObjectHistPO(sqlDataReader, GenerateObjectHistPO(sqlDataReader)));
                }
            }
            //else
            //    arrPOPurchn.Add(new POPurchn());

            return arrPOPurchn;
        }
        /**
         * menampilkan data po RetrievePOKertas
         * untuk di serahkan ke purchasing
         * po kertas
         * added on 03-11-2016
         * by Razib
         */
        public ArrayList RetrievePOKertas(string Bulan, string Tahun)
        {
            string where = string.Empty;
            //where = (SupplierName == "0") ? "where " : " where  SupplierName=" + SupplierName + " and ";
            string strSQL = "WITH TerimaKertas AS ( " +
                            "SELECT  rd.ID, r.ReceiptNo,Convert(Char,ReceiptDate,103)TglRMS,rd.PONo,s.SupplierName, rd.ItemID,rd.POID,rd.PODetailID, " +
                            "(Select dbo.ItemCodeInv (rd.ItemID,1))ItemCode,(select dbo.ItemNameInv(rd.itemid,1))ItemName,'KG' Unit,rd.Quantity " +
                            "FROM ReceiptDetail rd " +
                            "LEFT JOIN Receipt r ON r.ID=rd.ReceiptID " +
                            "LEFT JOIN POPurchn AS p ON p.ID=rd.POID AND p.Status>-1 " +
                            "LEFT JOIN SuppPurch AS s ON s.ID=p.SupplierID " +
                            "WHERE rd.ItemID IN(SELECT ItemID FROM POPurchnKadarAir WHERE RowStatus>-1 AND GROUPID=1 GROUP BY ItemID)" +
                            "AND r.Status>-1 AND rd.RowStatus>-1 AND " +
                            " MONTH(r.ReceiptDate)=" + Bulan + "  AND YEAR(r.ReceiptDate)=" + Tahun + " and p.Status>-1 AND r.ItemTypeID=1" +
                            "), " +
                            "TerimaKertas1 AS ( " +
                            "SELECT t.ID,t.ReceiptNo,TglRMS,PONo,SupplierName,ItemCode,Itemname,Unit, " +
                            "ISNULL(pk.Gross,0) BeratKotor,ISNULL(pk.StdKA,0)StdKA, " +
                            "ISNULL(pk.AktualKA,0)AktualKA,ISNULL(t.Quantity,0)Quantity,ISNULL(pk.Sampah,0)Sampah,ISNULL(pk.Netto,0) BeratBersih " +
                            "FROM TerimaKertas AS t " +
                            "LEFT JOIN PoPurchnKadarAir AS pk " +
                            "ON pk.POID=t.POID AND pk.PODetailID=t.PODetailID AND pk.RowStatus>-1 AND Gross>0 " +
                            ") " +
                            "SELECT * FROM TerimaKertas1 Order by tglRMS,ID ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPOPurchn.Add(GenerateObjectRekapPKertas(sqlDataReader));
                }
            }
            return arrPOPurchn;
        }
        private RekapPoKertas GenerateObjectRekapPKertas(SqlDataReader sqlDataReader)
        {
            objRekapPoKertas = new RekapPoKertas();
            objRekapPoKertas.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objRekapPoKertas.ReceiptNo = sqlDataReader["ReceiptNo"].ToString();
            objRekapPoKertas.TglRMS = Convert.ToDateTime(sqlDataReader["TglRMS"]);
            objRekapPoKertas.PONo = sqlDataReader["PONo"].ToString();
            objRekapPoKertas.SupplierName = sqlDataReader["SupplierName"].ToString();
            objRekapPoKertas.ItemCode = sqlDataReader["ItemCode"].ToString();
            objRekapPoKertas.ItemName = sqlDataReader["ItemName"].ToString();
            objRekapPoKertas.Unit = sqlDataReader["Unit"].ToString();
            objRekapPoKertas.BeratKotor = Convert.ToDecimal(sqlDataReader["BeratKotor"]);
            objRekapPoKertas.StdKA = Convert.ToDecimal(sqlDataReader["StdKA"]);
            objRekapPoKertas.AktualKA = Convert.ToDecimal(sqlDataReader["AktualKA"]);
            objRekapPoKertas.Quantity = Convert.ToDecimal(sqlDataReader["Quantity"]);
            objRekapPoKertas.Sampah = Convert.ToDecimal(sqlDataReader["Sampah"]);
            objRekapPoKertas.BeratBersih = Convert.ToDecimal(sqlDataReader["BeratBersih"]);
            return objRekapPoKertas;

        }



        public int InsertDetailPO(POPurchnDetail objPOPurchnDetail)
        {
            int result = 0;
            sqlListParam = new List<SqlParameter>();
            sqlListParam.Add(new SqlParameter("@POID", objPOPurchnDetail.POID));
            sqlListParam.Add(new SqlParameter("@SPPID", objPOPurchnDetail.SPPID));
            sqlListParam.Add(new SqlParameter("@GroupID", objPOPurchnDetail.GroupID));
            sqlListParam.Add(new SqlParameter("@ItemID", objPOPurchnDetail.ItemID));
            sqlListParam.Add(new SqlParameter("@Price", objPOPurchnDetail.Price));
            sqlListParam.Add(new SqlParameter("@Qty", objPOPurchnDetail.Qty));
            sqlListParam.Add(new SqlParameter("@ItemTypeID", objPOPurchnDetail.ItemTypeID));
            sqlListParam.Add(new SqlParameter("@UOMID", objPOPurchnDetail.UOMID));
            sqlListParam.Add(new SqlParameter("@Status", objPOPurchnDetail.Status));
            sqlListParam.Add(new SqlParameter("@NoUrut", objPOPurchnDetail.NoUrut));
            sqlListParam.Add(new SqlParameter("@SPPDetailID", objPOPurchnDetail.SPPDetailID));
            sqlListParam.Add(new SqlParameter("@DocumentNo", objPOPurchnDetail.DocumentNo));
            sqlListParam.Add(new SqlParameter("@DlvDate", objPOPurchnDetail.DlvDate));
            DataAccess da = new DataAccess(Global.ConnectionString());
            result = da.ProcessData(sqlListParam, "spInsertPOPurchnDetail");
            return result;
        }

        public int InsertHeaderPO(POPurchn objPOPurchn)
        {
            int result = 0;
            sqlListParam = new List<SqlParameter>();
            sqlListParam.Add(new SqlParameter("@NoPO", objPOPurchn.NoPO));
            sqlListParam.Add(new SqlParameter("@POPurchnDate", objPOPurchn.POPurchnDate));
            sqlListParam.Add(new SqlParameter("@SupplierID", objPOPurchn.SupplierID));
            sqlListParam.Add(new SqlParameter("@Termin", objPOPurchn.Termin));
            sqlListParam.Add(new SqlParameter("@PPN", objPOPurchn.PPN));
            sqlListParam.Add(new SqlParameter("@Delivery", objPOPurchn.Delivery));
            sqlListParam.Add(new SqlParameter("@Crc", objPOPurchn.Crc));
            sqlListParam.Add(new SqlParameter("@Keterangan", objPOPurchn.Keterangan));
            sqlListParam.Add(new SqlParameter("@Terbilang", objPOPurchn.Terbilang));
            sqlListParam.Add(new SqlParameter("@Disc", objPOPurchn.Disc));
            sqlListParam.Add(new SqlParameter("@PPH", objPOPurchn.PPH));
            sqlListParam.Add(new SqlParameter("@NilaiKurs", objPOPurchn.NilaiKurs));
            sqlListParam.Add(new SqlParameter("@Cetak", objPOPurchn.Cetak));
            sqlListParam.Add(new SqlParameter("@CountPrt", objPOPurchn.CountPrt));
            sqlListParam.Add(new SqlParameter("@Status", objPOPurchn.Status));
            sqlListParam.Add(new SqlParameter("@Approval", objPOPurchn.Approval));
            sqlListParam.Add(new SqlParameter("@ApproveDate1", objPOPurchn.ApproveDate1));
            sqlListParam.Add(new SqlParameter("@ApproveDate2", objPOPurchn.ApproveDate2));
            sqlListParam.Add(new SqlParameter("@CreatedBy", objPOPurchn.CreatedBy));
            sqlListParam.Add(new SqlParameter("@AlasanBatal", objPOPurchn.AlasanBatal));
            sqlListParam.Add(new SqlParameter("@AlasanClose", objPOPurchn.AlasanClose));
            sqlListParam.Add(new SqlParameter("@PaymentType", objPOPurchn.PaymentType));
            sqlListParam.Add(new SqlParameter("@ItemFrom", objPOPurchn.ItemFrom));
            sqlListParam.Add(new SqlParameter("@Indent", objPOPurchn.Indent));
            sqlListParam.Add(new SqlParameter("@Ongkos", objPOPurchn.Ongkos));
            sqlListParam.Add(new SqlParameter("@UangMuka", objPOPurchn.UangMuka));
            sqlListParam.Add(new SqlParameter("@CoaID", objPOPurchn.CoaID));
            sqlListParam.Add(new SqlParameter("@Remark", objPOPurchn.Remark));
            //added on 07-05-2017
            DataAccess da = new DataAccess(Global.ConnectionString());
             result = da.ProcessData(sqlListParam, "spInsertPOPurchn");
            
            return result;
        }
        //created by razib
        public ArrayList RetrieveMonOutPO(string Bulan, string Tahun, string GroupID)
        {
            string where = string.Empty;
            where = (GroupID == "0") ? "" : " where GroupID=" + GroupID;
            string strSQL = " WITH YngSudahDiRMS AS ( " +
                            " SELECT POID,PODetailID,Quantity FROM ReceiptDetail WHERE RowStatus>-1 " +
                            " ) " +
                            ",POAkanDatang AS ( " +
                            " SELECT p.ID,pd.ID PODetailID,p.NoPO,p.POPurchnDate,(Select SupplierName from SuppPurch where ID=p.SupplierID) as SupplierName ,pd.DocumentNo,pd.SppDetailID, " +
                            " pd.ItemID,(SELECT dbo.ItemCodeInv(ItemID,ItemTypeID))ItemCode " +
                            " ,(SELECT dbo.ItemNameInv(ItemID,ItemTypeID))ItemName,pd.GroupID " +
                            " ,pd.ItemTypeID,(Select UOMCode from UOM where ID = pd.UOMID) as Satuan,pd.Qty,(SELECT SUM(Quantity) FROM YngSudahDiRMS WHERE POID=p.ID AND PODetailID=pd.ID GROUP By PODetailID,POID)RMSQty " +
                            " ,pd.Price,pd.Price2,DlvDate " +
                            " ,DATEDIFF(DAY,GETDATE(),DlvDate)Interval " +
                            " FROM POPurchnDetail pd " +
                            " LEFT JOIN POPurchn p ON p.ID=pd.POID " +
                            " WHERE pd.Status>-1 AND p.Approval>1 AND (DATEDIFF(DAY,GETDATE(),DlvDate)between 0 AND 3) " +
                            " ) " +
                            " ,POSudahLewat AS ( " +
                            " SELECT p.ID,pd.ID PODetailID, p.NoPO,p.POPurchnDate,(Select SupplierName from SuppPurch where ID=p.SupplierID) as SupplierName,pd.DocumentNo,pd.SppDetailID,pd.ItemID," +
                            " (SELECT dbo.ItemCodeInv(ItemID,ItemTypeID))ItemCode " +
                            " ,(SELECT dbo.ItemNameInv(ItemID,ItemTypeID))ItemName,pd.GroupID " +
                            " ,pd.ItemTypeID,(Select UOMCode from UOM where ID = pd.UOMID) as Satuan,pd.Qty,(SELECT SUM(Quantity) FROM YngSudahDiRMS WHERE POID=p.ID AND PODetailID=pd.ID GROUP By PODetailID,POID)RMSQty " +
                            " ,pd.Price,pd.Price2,DlvDate " +
                            " ,DATEDIFF(DAY,GETDATE(),DlvDate)Interval " +
                            " FROM POPurchnDetail pd " +
                            " LEFT JOIN POPurchn p ON p.ID=pd.POID " +
                            " WHERE p.Status>-1 AND pd.Status>-1 AND p.Approval>1 " +
                            " AND p.ID not IN(SELECT PODetailID FROM YngSudahDiRMS) AND DlvDate<GETDATE() AND YEAR(p.POPurchnDate)>=" + Tahun +
                            " ) " +
                            " SELECT * FROM ( " +
                            " SELECT * FROM POAkanDatang WHERE (Qty- ISNULL(RMSQty,0))>0 " +
                            " UNION " +
                            " SELECT * FROM POSudahLewat pd WHERE (Qty- ISNULL(RMSQty,0))>0 ) as x  " + where;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPOPurchn = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPOPurchn.Add(GenerateObjectMonOutPO(sqlDataReader));
                }
            }
            else
                arrPOPurchn.Add(new MonOutPO());

            return arrPOPurchn;
        }

        public MonOutPO GenerateObjectMonOutPO(SqlDataReader sqlDataReader)
        {
            MonOutPO objMonOutPO = new MonOutPO();
            objMonOutPO.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objMonOutPO.NoPO = sqlDataReader["NoPO"].ToString();
            objMonOutPO.POPurchnDate = Convert.ToDateTime(sqlDataReader["POPurchnDate"]);
            objMonOutPO.SupplierName = sqlDataReader["SupplierName"].ToString();
            objMonOutPO.DocumentNo = sqlDataReader["DocumentNo"].ToString();
            objMonOutPO.ItemCode = sqlDataReader["ItemCode"].ToString();
            objMonOutPO.ItemName = sqlDataReader["ItemName"].ToString();
            objMonOutPO.Satuan = sqlDataReader["Satuan"].ToString();
            objMonOutPO.Qty = Convert.ToDecimal(sqlDataReader["Qty"]);
            //objMonOutPO.RMSQty = Convert.ToDecimal(sqlDataReader["RMSQty"]);
            objMonOutPO.Price = Convert.ToDecimal(sqlDataReader["Price"]);
            objMonOutPO.DlvDate = Convert.ToDateTime(sqlDataReader["DlvDate"]);
            return objMonOutPO;
        }

        public POPurchn GenerateUserDeptID(SqlDataReader sqlDataReader)
        {
            objPOPurchn = new POPurchn();
            objPOPurchn.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objPOPurchn.MasterAssetKomponen = sqlDataReader["MasterAssetKomponen"].ToString();
            return objPOPurchn;

        }

        public static List<POHead> RetrieveOpenPO(string Criteria, string OrderBy)
        {

            List<POHead> AllData = new List<POHead>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "SELECT DISTINCT TOP 100 PO.*,SP.SupplierName,SP.UP, MU.Nama MataUang " +
                          "FROM MataUang MU, POPurchn PO  " +
                          "LEFT JOIN POPurchnDetail POD ON POD.POID=PO.ID " +
                          "LEFT JOIN SuppPurch SP ON SP.ID=PO.SupplierID " +
                          "WHERE PO.Status =0 and POD.Status>-1 AND PO.Crc = MU.ID " + Criteria + OrderBy;
                    AllData = connection.Query<POHead>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PODetail> GetDetailPO(int ID)
        {

            List<PODetail> AllData = new List<PODetail>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select A.ID,A.POID,A.SPPID,A.GroupID,A.ItemID,A.Price,A.Qty,A.ItemTypeID,A.UOMID,B.UOMCode,A.Status,A.NoUrut,A.SPPDetailID,A.DocumentNo,isnull(dlvdate,'1/1/1900') as dlvdate, " +
                        "case A.ItemTypeID when 1  then(select ItemName from Inventory where ID = A.ItemID and RowStatus > -1) when 2 then " +
                        "(select ItemName from Asset where ID = A.ItemID and RowStatus > -1) when 3 then " +
                        "CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus = 1), GETDATE()) < (Select CreatedTime from SPP where SPP.ID = A.SPPID))  THEN " +
                        "(select ItemName from biaya where ID = A.ItemID and biaya.RowStatus > -1) + ' - ' + (Select SPPDetail.Keterangan From SPPDetail where SPPDetail.ID = A.SPPDetailID) ELSE(select ItemName from biaya where ID = A.ItemID and biaya.RowStatus > -1) END end ItemName, case A.ItemTypeID when 1  then " +
                        "(select ItemCode from Inventory where ID = A.ItemID and RowStatus > -1) when 2 then " +
                        "(select ItemCode from Asset where ID = A.ItemID and RowStatus > -1) else " +
                        "(select ItemCode from Biaya where ID = A.ItemID and RowStatus > -1) end ItemCode, case A.ItemTypeID when 1  then " +
                        "(select isnull(sum(Jumlah), 0) from Inventory where ID = A.ItemID and RowStatus > -1) when 2 then " +
                        "(select isnull(sum(Jumlah), 0) from Asset where ID = A.ItemID and RowStatus > -1) else CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus = 1),GETDATE())< (Select CreatedTime from SPP where SPP.ID = A.SPPID)) THEN(SELECT isnull(sum(jumlah), 0) from Biaya where ItemName = (Select SPPDetail.Keterangan From SPPDetail  where SPPDetail.ID = A.SPPDetailID))  ELSE(select isnull(sum(Jumlah), 0) from Biaya where ID = A.ItemID and Biaya.RowStatus > -1) END end Stok from POPurchnDetail as A, UOM as B where A.UOMID = B.ID and A.Status > -1 and A.POID = " + ID + "";
                    AllData = connection.Query<PODetail>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<RekapPO> GetRekapPO(string awal, string akhir, int viewprice)
        {

            List<RekapPO> AllData = new List<RekapPO>();
            string query = "";
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    if (viewprice == 0)
                    {
                        query = "SELECT POPurchn.ID,POPurchn.NoPO,POPurchn.Remark, SuppPurch.SupplierName,POPurchn.Approval Apv, case when POPurchn.Approval=0 then 'Open' when POPurchn.Approval=1 then 'Head' when POPurchn.Approval=2 then 'Manager Corp.' end Approval, POPurchn.PPN,POPurchn.PPH, MataUang.Nama M_Uang, " +
                            "CASE POPurchnDetail.ItemTypeID WHEN 1 THEN (SELECT ItemName FROM Inventory WHERE ID = POPurchnDetail.ItemID AND RowStatus > - 1) WHEN 2 THEN (SELECT ItemName FROM Asset WHERE ID = POPurchnDetail.ItemID AND RowStatus > - 1) ELSE CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())<=  (Select CreatedTime from SPP where SPP.ID=POPurchnDetail.SPPID))  THEN(select ItemName from Biaya where Biaya.ID=POPurchnDetail.ItemID and Biaya.RowStatus>-1)+' - '+  " +
                            "(Select SPPDetail.Keterangan From SPPDetail where SPPDetail.ID=POPurchnDetail.SPPDetailID and  SPPDetail.SPPID=POPurchnDetail.SPPID) ELSE  (select ItemName from biaya where ID=POPurchnDetail.ItemID and biaya.RowStatus>-1) END  END AS ItemName, SPP.NoSPP,  POPurchnDetail.Qty,  UOM.UOMCode Satuan, POPurchn.Disc, 0 Price,0 as Total, POPurchnDetail.Price * POPurchnDetail.Qty AS TOT2, POPurchn.POPurchnDate, GROUPsPURCHN.groupdescription as groupdesc,POPurchn.Cetak FROM POPurchn INNER JOIN POPurchnDetail ON POPurchn.ID = POPurchnDetail.POID INNER JOIN SPP ON POPurchnDetail.SPPID = SPP.ID INNER JOIN UOM ON POPurchnDetail.UOMID = UOM.ID INNER JOIN SuppPurch ON POPurchn.SupplierID = SuppPurch.ID INNER JOIN MataUang ON POPurchn.Crc = MataUang.ID INNER JOIN GROUPsPURCHN ON POPurchnDetail.groupid = GROUPsPURCHN.id where  POPurchn.status>-1 and convert(varchar,POPurchn.POPurchndate,112)>='" + awal + "' and  convert(varchar,POPurchn.POPurchndate,112)<='" + akhir + "' and POPurchnDetail.Status >-1 order by groupdesc,POPurchn.NoPO";
                    }
                    else if (viewprice == 1)
                    {
                        query = "select ID,NoPO,SupplierName,Approval, apv, PPN,PPH,M_Uang,ItemName,NoSPP,Qty,UOMCode Satuan,Disc,case when (select isnull(harga,0) from Inventory where ID=A.ItemID)=0  then Price else 0 end Price,case when (select isnull(harga,0) from Inventory where ID=A.ItemID)=0  then Total else 0 end Total,POPurchnDate,groupdesc,Cetak from ( SELECT POPurchn.ID,popurchndetail.itemid, POPurchn.NoPO, SuppPurch.SupplierName,POPurchn.Cetak,POPurchn.Approval Apv,case when POPurchn.Approval=0 then 'Open' when POPurchn.Approval=1 then 'Head' when POPurchn.Approval=2 then 'Manager Corp.' end  Approval, POPurchn.PPN,POPurchn.PPH, MataUang.Nama M_Uang, " +
                            "CASE POPurchnDetail.ItemTypeID WHEN 1 THEN (SELECT ItemName FROM Inventory WHERE ID = POPurchnDetail.ItemID AND RowStatus > - 1) WHEN 2 THEN (SELECT ItemName FROM Asset WHERE ID = POPurchnDetail.ItemID AND RowStatus > - 1) ELSE CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())<=  (Select CreatedTime from SPP where SPP.ID=POPurchnDetail.SPPID))  THEN(select ItemName from Biaya where Biaya.ID=POPurchnDetail.ItemID and Biaya.RowStatus>-1)+' - '+  (Select SPPDetail.Keterangan From SPPDetail where SPPDetail.ID=POPurchnDetail.SPPDetailID and  SPPDetail.SPPID=POPurchnDetail.SPPID) ELSE  (select ItemName from biaya where ID=POPurchnDetail.ItemID and biaya.RowStatus>-1) END  END AS ItemName, SPP.NoSPP,POPurchnDetail.Qty,  UOM.UOMCode, POPurchn.Disc, " +
                            "case when POPurchn.Disc>0 then (POPurchnDetail.Price-(POPurchnDetail.Price*POPurchn.Disc/100)) else POPurchnDetail.Price end Price,(case when POPurchn.Disc>0 then (POPurchnDetail.Price-(POPurchnDetail.Price*POPurchn.Disc/100)) else POPurchnDetail.Price end)*POPurchnDetail.Qty as Total,  POPurchn.POPurchnDate, GROUPsPURCHN.groupdescription as groupdesc FROM POPurchn INNER JOIN POPurchnDetail ON POPurchn.ID = POPurchnDetail.POID INNER JOIN SPP ON POPurchnDetail.SPPID = SPP.ID INNER JOIN UOM ON POPurchnDetail.UOMID = UOM.ID INNER JOIN SuppPurch ON POPurchn.SupplierID = SuppPurch.ID INNER JOIN MataUang ON POPurchn.Crc = MataUang.ID INNER JOIN GROUPsPURCHN ON POPurchnDetail.groupid = GROUPsPURCHN.id where  POPurchn.status>-1 and convert(varchar,POPurchn.POPurchndate,112)>='" + awal + "' and  convert(varchar,POPurchn.POPurchndate,112)<='" + akhir + "' and POPurchnDetail.Status >-1 )as A order by groupdesc,NoPO";
                    }
                    else if (viewprice == 2)
                    {
                        query = "SELECT POPurchn.ID,POPurchn.NoPO,POPurchn.Remark, SuppPurch.SupplierName,POPurchn.Approval Apv, case when POPurchn.Approval=0 then 'Open' when POPurchn.Approval=1 then 'Head' when POPurchn.Approval=2 then 'Manager Corp.' end  Approval, POPurchn.PPN,POPurchn.PPH, MataUang.Nama M_Uang, " +
                            "CASE POPurchnDetail.ItemTypeID WHEN 1 THEN (SELECT ItemName FROM Inventory WHERE ID = POPurchnDetail.ItemID AND RowStatus > - 1) WHEN 2 THEN (SELECT ItemName FROM Asset WHERE ID = POPurchnDetail.ItemID AND RowStatus > - 1) ELSE " +
                            "CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())<=  (Select CreatedTime from SPP where SPP.ID=POPurchnDetail.SPPID))  THEN(select ItemName from Biaya " +
                            "where Biaya.ID=POPurchnDetail.ItemID and Biaya.RowStatus>-1)+' - '+  (Select SPPDetail.Keterangan From SPPDetail where SPPDetail.ID=POPurchnDetail.SPPDetailID and  SPPDetail.SPPID=POPurchnDetail.SPPID) ELSE  (select ItemName from biaya where ID=POPurchnDetail.ItemID and biaya.RowStatus>-1) END  END AS ItemName, SPP.NoSPP,POPurchnDetail.Qty,  UOM.UOMCode Satuan, POPurchn.Disc, " +
                            "case when POPurchn.Disc>0 then (POPurchnDetail.Price-(POPurchnDetail.Price*POPurchn.Disc/100)) else POPurchnDetail.Price end Price,(case when POPurchn.Disc>0 then (POPurchnDetail.Price-(POPurchnDetail.Price*POPurchn.Disc/100)) else POPurchnDetail.Price end)*POPurchnDetail.Qty as Total,  POPurchn.POPurchnDate, GROUPsPURCHN.groupdescription as groupdesc,POPurchn.Cetak " +
                            "FROM POPurchn INNER JOIN POPurchnDetail ON POPurchn.ID = POPurchnDetail.POID INNER JOIN SPP ON POPurchnDetail.SPPID = SPP.ID INNER JOIN UOM ON POPurchnDetail.UOMID = UOM.ID INNER JOIN SuppPurch ON POPurchn.SupplierID = SuppPurch.ID INNER JOIN MataUang ON POPurchn.Crc = MataUang.ID INNER JOIN GROUPsPURCHN ON POPurchnDetail.groupid = GROUPsPURCHN.id where  POPurchn.status>-1 and convert(varchar,POPurchn.POPurchndate,112)>='" + awal + "' and  convert(varchar,POPurchn.POPurchndate,112)<='" + akhir + "' and POPurchnDetail.Status >-1  order by groupdesc,POPurchn.NoPO";
                    }

                    AllData = connection.Query<RekapPO>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }


        public static List<Domain.RekapPO> GetDetailPO(string nopo, int depoid)
        {

            List<Domain.RekapPO> AllData = new List<Domain.RekapPO>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "SELECT a.NoPo, a.PPN, a.PPH, a.Disc, a.Remark, b.Qty, b.Price, a.Termin, a.Delivery, " + depoid + " DepoID," +
                        "(CASE WHEN b.ItemTypeID = 1 THEN(SELECT Itemcode FROM Inventory WHERE ID = b.ItemID AND RowStatus > -1)" +
                        "WHEN b.ItemTypeID = 2 THEN(SELECT Itemcode FROM asset WHERE ID = b.ItemID AND RowStatus > -1) " +
                        "WHEN b.ItemTypeID = 3 THEN(SELECT Itemcode FROM biaya WHERE ID = b.ItemID AND RowStatus > -1) END) ItemCode, " +
                        "(CASE WHEN b.ItemTypeID = 1 THEN(SELECT ItemName FROM Inventory WHERE ID = b.ItemID AND RowStatus > -1) " +
                        "WHEN b.ItemTypeID = 2 THEN(SELECT ItemName FROM asset WHERE ID = b.ItemID AND RowStatus > -1) " +
                        "WHEN b.ItemTypeID = 3 THEN(SELECT ItemName FROM biaya WHERE ID = b.ItemID AND RowStatus > -1) END) ItemName, " +
                        "c.UOMCode Satuan, d.nama MataUang, d.Lambang, b.dlvdate, e.SupplierName, e.Alamat, e.UP, e.Telepon, e.fax FROM POPurchn a, POPurchnDetail b, UOM c, MataUang d, SuppPurch e WHERE a.ID = b.POID AND b.uomid = c.id AND a.crc = d.id AND a.Supplierid = e.id AND a.nopo ='" + nopo + "'";
                    AllData = connection.Query<Domain.RekapPO>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }


    }
}
