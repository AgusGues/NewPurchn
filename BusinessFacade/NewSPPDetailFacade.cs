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
    public class NewSPPDetailFacade : AbstractTransactionFacade
    {
        private NewSPPDetail objSPPDetail = new NewSPPDetail();
        private ArrayList arrSPPDetail;
        private List<SqlParameter> sqlListParam;
        //private string scheduleNo = string.Empty;

        public NewSPPDetailFacade(object objDomain)
            : base(objDomain)
        {
            objSPPDetail = (NewSPPDetail)objDomain;
        }

        public NewSPPDetailFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                
                List<SqlParameter> sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@SPPID", objSPPDetail.SPPID));
                sqlListParam.Add(new SqlParameter("@GroupID", objSPPDetail.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemID", objSPPDetail.ItemID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objSPPDetail.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@UOMID", objSPPDetail.UOMID));
                sqlListParam.Add(new SqlParameter("@Quantity", objSPPDetail.Quantity));
                sqlListParam.Add(new SqlParameter("@QtyPO", objSPPDetail.QtyPO));
                sqlListParam.Add(new SqlParameter("@Status", objSPPDetail.Status));
                sqlListParam.Add(new SqlParameter("@Keterangan", objSPPDetail.Keterangan));
                sqlListParam.Add(new SqlParameter("@tglkirim", objSPPDetail.TglKirim));
                sqlListParam.Add(new SqlParameter("@TypeBiaya", objSPPDetail.TypeBiaya));
                sqlListParam.Add(new SqlParameter("@Keterangan1", objSPPDetail.Keterangan1));
                /**
                 * ROPFacade ropfacade = new ROPFacade();                
                 * Update SPPQty di table ROP dengan JmlSPP yng blm di Buat PO dan JmlPO yang blm di Receipt + 
                 * jmlSPP yng baru diBuat
                 * Taroh di sini proses error, pindah ke btnUpdate_click > transaksiSPP3.aspx.cs
                decimal SPPnoPO = ropfacade.JumlahSPPBlmPO(objSPPDetail.ItemID, objSPPDetail.ItemTypeID);
                decimal POnoReceipt = ropfacade.JumlahPOblmReceipt(objSPPDetail.ItemID, objSPPDetail.ItemTypeID);
                ropfacade.UpdateROPBySPP(objSPPDetail.ItemID, objSPPDetail.SPPID, (objSPPDetail.Quantity+SPPnoPO + POnoReceipt));*/
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertSPPDetail_New1");
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
                objSPPDetail = (NewSPPDetail)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@QtyPO", objSPPDetail.QtyPO));
                sqlListParam.Add(new SqlParameter("@SPPID", objSPPDetail.SPPID));
                sqlListParam.Add(new SqlParameter("@ItemID", objSPPDetail.ItemID));
                sqlListParam.Add(new SqlParameter("@Status", objSPPDetail.Status));
                sqlListParam.Add(new SqlParameter("@Keterangan", objSPPDetail.Keterangan));
                //sqlListParam.Add(new SqlParameter("@Keterangan1", objSPPDetail.Keterangan1));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateSPPDetail");

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

                objSPPDetail = (NewSPPDetail)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@SPPID", objSPPDetail.SPPID));

                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteSPPDetail");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int CancelSPPDetail(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objSPPDetail.ID));

                int intResult = transManager.DoTransaction(sqlListParam, "spCancelSPPDetail");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateStatusSPP(object objDomain)
        {
            try
            {
                objSPPDetail = (NewSPPDetail)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objSPPDetail.ID));
                sqlListParam.Add(new SqlParameter("@PendingPO", objSPPDetail.PendingPO));
                sqlListParam.Add(new SqlParameter("@AlasangPending", objSPPDetail.AlasanPending));
                sqlListParam.Add(new SqlParameter("@Status", objSPPDetail.Status));
                DataAccess dataAccess=new DataAccess(Global.ConnectionString());
                int result = dataAccess.ProcessData(sqlListParam, "spUpdateStatusSPPDetail_new");
                return result;
            }catch(Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int MinusQtyPOSPPDetail(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objSPPDetail.ID));
                sqlListParam.Add(new SqlParameter("@Jumlah", objSPPDetail.QtyPO));
                int intResult = transManager.DoTransaction(sqlListParam, "spMinQtyPOSPPDetail");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateSPPDetail(object objDomain)
        {
            try
            {
                objSPPDetail = (NewSPPDetail)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objSPPDetail.ID));
                sqlListParam.Add(new SqlParameter("@Qty", objSPPDetail.Quantity));
                sqlListParam.Add(new SqlParameter("@kete", objSPPDetail.Keterangan));
                sqlListParam.Add(new SqlParameter("@Keterangan1", objSPPDetail.Keterangan1));
                DataAccess dataAccess=new DataAccess(Global.ConnectionString());
                int result = dataAccess.ProcessData(sqlListParam, "spUpdateSPPDetail_New");
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return 0;
            }
        }
        private string Criteria()
        {
            string sppDetailID = (System.Web.HttpContext.Current.Session["where"] != null) ?
             System.Web.HttpContext.Current.Session["where"].ToString() : string.Empty;
            return sppDetailID;
        }
        public override ArrayList Retrieve()
        {
            string strSQL = "select A.ID,A.ItemID,A.UomID,A.ItemTypeID,A.GroupID,A.Quantity,A.QtyPO,A.Status,A.SPPID,ISNULL(A.PendingPO,0)PendingPO,"+
                            "A.AlasanPending,A.Keterangan,A.tglkirim,A.TypeBiaya,B.ItemCode,B.ItemName as ItemName,U.UomCode Satuan "+
                            "from SPPDetail as A," +
                            "Inventory as B,UOM as U where A.ItemID = B.ID and U.ID=A.UomID and A.Status>-1 " + Criteria();
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSPPDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSPPDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            
            return arrSPPDetail;
        }
        public ArrayList RetrievePending(int UserID)
        {
            string strQuery = "Select A.ID, S.NoSPP,S.Minta,A.PendingPO,A.AlasanPending,A.ItemID,A.Quantity,isnull(A.QtyPO,0)QtyPO,A.Status," +
                            "case A.ItemTypeID when 1  then (select ItemName from Inventory where ID=A.ItemID and RowStatus > -1) " +
                            "when 2 then (select ItemName from Asset where ID=A.ItemID and RowStatus > -1) " +
                            "else " + ItemSPPBiayaNew() + " end ItemName, case when A.ItemTypeID<>3 Then A.Keterangan else '' end Keterangan  " +
                            "From SPP as S,SPPDetail as A where S.ID=A.SPPID and A.PendingPO >=0 and A.Status in (0,1,3) and A.AlasanPending != '' and (S.UserID=" + UserID + " or S.HeadID=" + UserID + ")" +
                            Criteria() + " Order by A.ID";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strQuery);
            arrSPPDetail = new ArrayList();
            try
            {
                if (dataAccess.Error == string.Empty && sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        arrSPPDetail.Add(new NewSPPDetail
                        {
                            ID = Convert.ToInt32(sqlDataReader["ID"].ToString()),
                            Quantity = Convert.ToDecimal(sqlDataReader["Quantity"].ToString()),
                            QtyPO = Convert.ToDecimal(sqlDataReader["QtyPO"].ToString()),
                            Status = Convert.ToInt32(sqlDataReader["Status"].ToString()),
                            ItemID = Convert.ToInt32(sqlDataReader["ItemID"].ToString()),
                            GroupID = Convert.ToInt32(sqlDataReader["PendingPO"].ToString()),
                            ItemName = sqlDataReader["ItemName"].ToString(),
                            ItemCode = sqlDataReader["Keterangan"].ToString(),
                            Keterangan = sqlDataReader["Keterangan"].ToString(),
                            CariItemName = sqlDataReader["AlasanPending"].ToString(),
                            Satuan = sqlDataReader["NoSPP"].ToString(),
                            TglKirim = Convert.ToDateTime(sqlDataReader["Minta"].ToString())
                        });
                    }
                }
            }
            catch { }
            return arrSPPDetail;
        }

        public ArrayList RetrieveTPP(int UserID2)
        {
            string strQuery = "select distinct * from tpp as t left join TPP_Tindakan as tt on t.ID=tt.TPP_ID where tt.Jadwal_Selesai<>'' and tt.Verifikasi=0 and t.User_ID=" + UserID2 + " " +
                            Criteria() + "order by t.id desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strQuery);
            arrSPPDetail = new ArrayList();
            if (dataAccess.Error == string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSPPDetail.Add(new NewSPPDetail
                    {
                        Laporan_No = sqlDataReader["Laporan_No"].ToString(),
                        Jadwal_Selesai = Convert.ToDateTime(sqlDataReader["Jadwal_Selesai"].ToString())
                    });
                }
            }
            return arrSPPDetail;
        }
        public ArrayList RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select A.ID,A.SPPID,A.GroupID,A.ItemTypeID,A.UOMID,A.ItemID,A.Quantity," +
                            "case A.ItemTypeID when 1 then (select ItemCode from Inventory where ID=A.ItemID) " +
                            "when 2 then (select ItemCode from Asset where ID=A.ItemID) " +
                            "when 3 then (select ItemCode from Biaya where ID=A.ItemID) else '' end ItemCode," +
                            "case A.ItemTypeID when 1 then (select ItemName from Inventory where ID=A.ItemID) " +
                            "when 2 then (select ItemName from Asset where ID=A.ItemID) " +
                            "when 3 then (select ItemName from Biaya where ID=A.ItemID)+ ' - ' + A.Keterangan else '' end ItemName," +
                            "C.UOMCode as Satuan,A.QtyPO,A.Status,A.Keterangan,isnull(A.PendingPO,0)PendingPO,A.AlasanPending, "+
                            "case when (select spp.PermintaanType from spp where ID=A.SPPID)=2 Then "+
	                        "DATEADD(day,isnull((select LeadTime from Inventory where ID=A.ItemID),7),(select SPP.ApproveDate3 from SPP where ID=A.SPPID)) "+
	                        "else A.tglkirim END as tglkirim from SPPDetail as A,UOM as C where A.UOMID = C.ID and A.Status>-1 and A.SPPID =" + Id;
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSPPDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSPPDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSPPDetail.Add(new NewSPPDetail());

            return arrSPPDetail;
        }
        private string ForPO()
        {
            string stat =(System.Web.HttpContext.Current.Session["ForPO"]!=null)? System.Web.HttpContext.Current.Session["ForPO"].ToString():string.Empty;
            return (stat!=string.Empty)?" A.Quantity<>A.QtyPO and (PendingPO=0 or PendingPO is null) and ":"";
        }
        public ArrayList RetrieveBySPPID(int Id)
        {
            string strsql = "select A.ID,A.SPPID,A.GroupID,A.ItemTypeID,A.UOMID,A.ItemID,A.Quantity,C.UOMCode as Satuan,A.QtyPO,A.Status,A.Keterangan, " +
                            "case A.ItemTypeID when 1  then (select ItemName from Inventory where ID=A.ItemID and RowStatus > -1) " +
                            "when 2 then (select ItemName from Asset where ID=A.ItemID and RowStatus > -1) " +
                            "else " + ItemSPPBiayaNew() + " end ItemName, " +
                            "case A.ItemTypeID when 1  then (select ItemCode from Inventory where ID=A.ItemID and RowStatus > -1) " +
                            "when 2 then (select ItemCode from Asset where ID=A.ItemID and RowStatus > -1) " +
                            "else (select ItemCode from Biaya where ID=A.ItemID and RowStatus > -1) end ItemCode,A.tglkirim, " +
                            "isnull(PendingPO,0)PendingPO,AlasanPending,TypeBiaya,Keterangan1 " +
                            "from SPPDetail as A,UOM as C where " + this.ForPO() + " A.UOMID = C.ID and A.Status>-1 and A.SPPID = " + Id;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            
            strError = dataAccess.Error;
            arrSPPDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSPPDetail.Add(GenerateObject(sqlDataReader,GenerateObject(sqlDataReader)));
                }
            }
            
            return arrSPPDetail;
        }
        public NewSPPDetail RetrieveBySPPDetailID(int Id)
        {
            string strSQL = "select A.ID,A.SPPID,A.GroupID,A.ItemTypeID,A.UOMID,A.ItemID,A.Quantity,C.UOMCode as " +
                "Satuan,A.QtyPO,A.Status,A.Keterangan,ISNULL(A.PendingPO,0)PendingPO,A.AlasanPending,A.TypeBiaya, " +
                "case A.ItemTypeID when 1  then (select ItemName from Inventory where ID=A.ItemID and RowStatus > -1) " +
                "when 2 then (select ItemName from Asset where ID=A.ItemID and RowStatus > -1) " +
                "else (select ItemName from Biaya where ID=A.ItemID and RowStatus > -1) end ItemName, " +
                "case A.ItemTypeID when 1  then (select ItemCode from Inventory where ID=A.ItemID and RowStatus > -1) " +
                "when 2 then (select ItemCode from Asset where ID=A.ItemID and RowStatus > -1) " +
                "else (select ItemCode from Biaya where ID=A.ItemID and RowStatus > -1) end ItemCode,A.tglkirim,A.Keterangan1 " +
                "from SPPDetail as A,UOM as C where A.UOMID = C.ID and A.Status>-1 and A.ID = " + Id;

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrSPPDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader, GenerateObject(sqlDataReader));
                }
            }

            return new NewSPPDetail();
        }
        public NewSPPDetail RetrieveBySPPDetailID(int Id,bool DetailID)
        {
            string strSQL = "select A.ID,A.SPPID,A.GroupID,A.ItemTypeID,A.UOMID,A.ItemID,A.Quantity,C.UOMCode as " +
                "Satuan,A.QtyPO,A.Status,A.Keterangan,ISNULL(A.PendingPO,0)PendingPO,A.AlasanPending,A.TypeBiaya, " +
                "case A.ItemTypeID when 1  then (select ItemName from Inventory where ID=A.ItemID and RowStatus > -1) " +
                "when 2 then (select ItemName from Asset where ID=A.ItemID and RowStatus > -1) " +
                "else (select ItemName from Biaya where ID=A.ItemID and RowStatus > -1) end ItemName, " +
                "case A.ItemTypeID when 1  then (select ItemCode from Inventory where ID=A.ItemID and RowStatus > -1) " +
                "when 2 then (select ItemCode from Asset where ID=A.ItemID and RowStatus > -1) " +
                "else (select ItemCode from Biaya where ID=A.ItemID and RowStatus > -1) end ItemCode,A.tglkirim,A.Keterangan1 " +
                "from SPPDetail as A,UOM as C where A.UOMID = C.ID and A.Status>-1 and A.SPPID = " + Id;

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrSPPDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader, GenerateObject(sqlDataReader));
                }
            }

            return new NewSPPDetail();
        }
        public ArrayList RetrieveBySPPIDNoSPP(string nospp)
        {
            string strsql = "select A.ID,A.SPPID,A.GroupID,A.ItemTypeID,A.UOMID,A.ItemID,A.Quantity,C.UOMCode as Satuan,A.QtyPO,A.Status,A.Keterangan, " +
                            "case A.ItemTypeID when 1  then (select ItemName from Inventory where ID=A.ItemID and RowStatus > -1) " +
                            "when 2 then (select ItemName from Asset where ID=A.ItemID and RowStatus > -1) " +
                            "else (select ItemName from Biaya where ID=A.ItemID and RowStatus > -1) end ItemName, " +
                            "case A.ItemTypeID when 1  then (select ItemCode from Inventory where ID=A.ItemID and RowStatus > -1) " +
                            "when 2 then (select ItemCode from Asset where ID=A.ItemID and RowStatus > -1) " +
                            "else (select ItemCode from Biaya where ID=A.ItemID and RowStatus > -1) end ItemCode,ISNULL(A.PendingPO,0)PendingPO,A.AlasanPending " +
                            "from SPPDetail as A,UOM as C where quantity <> qtyPO and A.UOMID = C.ID and A.Status>-1 and A.SPPID in(select id from spp where nospp='" + nospp + "')";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);

            strError = dataAccess.Error;
            arrSPPDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSPPDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            
            return arrSPPDetail;
        }
        
        public NewSPPDetail GenerateObject(SqlDataReader sqlDataReader)
        {
            try
            {
                objSPPDetail = new NewSPPDetail();
                objSPPDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
                objSPPDetail.SPPID = Convert.ToInt32(sqlDataReader["SPPID"]);
                objSPPDetail.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
                objSPPDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
                objSPPDetail.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"]);
                objSPPDetail.Quantity = Convert.ToDecimal(sqlDataReader["Quantity"]);
                objSPPDetail.UOMID = Convert.ToInt32(sqlDataReader["UOMID"]);
                objSPPDetail.ItemCode = sqlDataReader["ItemCode"].ToString();
                objSPPDetail.ItemName = sqlDataReader["ItemName"].ToString();
                objSPPDetail.Satuan = sqlDataReader["Satuan"].ToString();
                objSPPDetail.Status = Convert.ToInt32(sqlDataReader["Status"]);
                objSPPDetail.QtyPO = Convert.ToDecimal(sqlDataReader["QtyPO"]);
                objSPPDetail.Keterangan = sqlDataReader["Keterangan"].ToString();
                objSPPDetail.TglKirim = Convert.ToDateTime(sqlDataReader["tglkirim"]);
                objSPPDetail.PendingPO = Convert.ToInt32(sqlDataReader["PendingPO"]);
                objSPPDetail.AlasanPending = sqlDataReader["AlasanPending"].ToString();
            }
            catch
            {
            }
            return objSPPDetail;
        }
        private NewSPPDetail GenerateObject(SqlDataReader sdr, NewSPPDetail arrSppDetail)
        {
            objSPPDetail = (NewSPPDetail)arrSppDetail;
            objSPPDetail.TypeBiaya = sdr["TypeBiaya"].ToString();
            objSPPDetail.Keterangan1 = sdr["Keterangan1"].ToString();
            return objSPPDetail;
        }
        

        /**
         * Added on 06-05-2014
         * Info Barang di spp Last PO dan Last RMS
         */
        public NewSPPDetail GetLastPORMS(int ItemID, int ItemTypeID)
        {
            string strSQL="select top 1 (select nopo from POPurchn where ID=POID) as NoPO, "+
                   " (select ReceiptNo from Receipt where Receipt.ID= "+
                   " (select top 1 ReceiptID from ReceiptDetail where ReceiptDetail.ItemID=po.ItemID and  "+
                   " ReceiptDetail.ItemTypeID=po.ItemTypeID order by ReceiptDetail.ID desc )  "+
                   " ) as Rms  " +
                   " from POPurchnDetail as po where po.ItemID="+ItemID+" and po.ItemTypeID="+ItemTypeID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrSPPDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectInfo(sqlDataReader);
                }
            }

            return new NewSPPDetail();
        }

        public NewSPPDetail GenerateObjectInfo(SqlDataReader sqlDataReader)
        {
            try
            {
                objSPPDetail = new NewSPPDetail();
               
                objSPPDetail.Satuan = sqlDataReader["NoPO"].ToString();
                objSPPDetail.Keterangan = sqlDataReader["Rms"].ToString();
            }
            catch
            {
            }
            return objSPPDetail;
        }
        public string ItemSPPBiayaNew()
        {
            string strSQL = "CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())< " +
                " (Select CreatedTime from SPP where SPP.ID=A.SPPID)) " +
                " THEN(select ItemName from biaya where ID=A.ItemID and biaya.RowStatus>-1)+' - '+ " +
                " (Select SPPDetail.Keterangan From SPPDetail where SPPDetail.ID=A.ID) ELSE " +
                " (select ItemName from biaya where ID=A.ItemID and biaya.RowStatus>-1) END ";
            return strSQL;
        }
        public int GetLeadTime(int ItemCode,int ItemType)
        {
            int ldTime = 0; string Tabel=string.Empty;
            switch (ItemType) { case 1: Tabel = "Inventory"; break; case 2: Tabel = "Asset"; break; case 3: Tabel = "Biaya"; break; }
            string strSQL = "Select LeadTime From " + Tabel + " where ItemCode=" + ItemCode;
            DataAccess dta = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = dta.RetrieveDataByString(strSQL);
            if (dta.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ldTime = Convert.ToInt32(sdr["LeadTime"].ToString());
                }
            }
            return ldTime;

        }

        public int UpdateSPPDetail(object objDomain, bool p)
        {
            objSPPDetail = (NewSPPDetail)objDomain;
            sqlListParam = new List<SqlParameter>();
            sqlListParam.Add(new SqlParameter("@ID", objSPPDetail.ID));
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            int result = dataAccess.ProcessData(sqlListParam, "spSPPDetail_UpdateStatusPO");
            return result;
        }
    }
}
