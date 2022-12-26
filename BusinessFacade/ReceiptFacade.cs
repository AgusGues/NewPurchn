using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;
using Domain;
using DataAccessLayer;

namespace BusinessFacade
{
    public class ReceiptFacade : AbstractTransactionFacade
    {
        private Receipt objReceipt = new Receipt();
        private ArrayList arrReceipt;
        private List<SqlParameter> sqlListParam;

        public ReceiptFacade(object objDomain)
            : base(objDomain)
        {
            objReceipt = (Receipt)objDomain;
        }

        public ReceiptFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ReceiptNo", objReceipt.ReceiptNo));
                sqlListParam.Add(new SqlParameter("@ReceiptDate", objReceipt.ReceiptDate));
                sqlListParam.Add(new SqlParameter("@SupplierID", objReceipt.SupplierID));
                sqlListParam.Add(new SqlParameter("@DepoID", objReceipt.DepoID));
                sqlListParam.Add(new SqlParameter("@Status", objReceipt.Status));
                sqlListParam.Add(new SqlParameter("@POID", objReceipt.PoID));
                sqlListParam.Add(new SqlParameter("@PONo", objReceipt.PoNo));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objReceipt.CreatedBy));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objReceipt.ItemTypeID));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertReceipt");

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
                sqlListParam.Add(new SqlParameter("@ID", objReceipt.ID));
                sqlListParam.Add(new SqlParameter("@ReceiptNo", objReceipt.ReceiptNo));
                sqlListParam.Add(new SqlParameter("@ReceiptDate", objReceipt.ReceiptDate));
                sqlListParam.Add(new SqlParameter("@SupplierID", objReceipt.SupplierID));
                sqlListParam.Add(new SqlParameter("@SupplierType", objReceipt.SupplierType));
                sqlListParam.Add(new SqlParameter("@ReceiptType", objReceipt.ReceiptType));
                sqlListParam.Add(new SqlParameter("@DepoID", objReceipt.DepoID));
                sqlListParam.Add(new SqlParameter("@Status", objReceipt.Status));
                sqlListParam.Add(new SqlParameter("@POID", objReceipt.PoID));
                sqlListParam.Add(new SqlParameter("@PONo", objReceipt.PoNo));
                sqlListParam.Add(new SqlParameter("@AlasanCancel", objReceipt.AlasanCancel));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objReceipt.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@TTagihanDate", objReceipt.TTagihanDate ));
                sqlListParam.Add(new SqlParameter("@JTempoDate", objReceipt.JTempoDate ));
                sqlListParam.Add(new SqlParameter("@fakturpajak", objReceipt.FakturPajak ));
                sqlListParam.Add(new SqlParameter("@keteranganpay", objReceipt.Keteranganpay ));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objReceipt.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@FakturPajakDate", objReceipt.FakturPajakDate));
                sqlListParam.Add(new SqlParameter("@InvoiceNo", objReceipt.InvoiceNo));
                sqlListParam.Add(new SqlParameter("@kurspajak", objReceipt.KursPajak));
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateReceipt");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

       
        //public override int Delete(TransactionManager transManager)
        //{

        //try
        //{
        //    sqlListParam = new List<SqlParameter>();
        //    sqlListParam.Add(new SqlParameter("@ID", objReceipt.ID));
        //    sqlListParam.Add(new SqlParameter("@LastModifiedBy", objReceipt.LastModifiedBy));

        //    int intResult = transManager.DoTransaction(sqlListParam, "spDeleteSchedule");

        //    strError = transManager.Error;

        //    return intResult;

        //}
        //catch (Exception ex)
        //{
        //    strError = ex.Message;
        //    return -1;
        //}

        //    return 0;
        //}

        public override int Delete(TransactionManager transManager)
        {

            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objReceipt.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objReceipt.LastModifiedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spCancelReceipt");

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
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 50 A.ID,A.ScheduleNo,A.ScheduleDate,A.ExpedisiDetailID,B.ExpedisiName,C.CarType,A.TotalKubikasi,A.Status,A.Rate,A.Keterangan,A.DepoID,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from Receipt as A,Expedisi as B,ExpedisiDetail as C where A.ExpedisiDetailID = C.ID and C.ExpedisiID = B.ID and A.RowStatus = 0 order by A.ID");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 50 A.ID,A.ReceiptNo,A.ReceiptDate,A.POID,A.PONo,A.SupplierId,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ItemTypeID from Receipt as A where A.Status>-1 order by ID");
            strError = dataAccess.Error;
            arrReceipt = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceipt.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrReceipt.Add(new Receipt());

            return arrReceipt;
        }

        public ArrayList RetrieveBySamaTgl(int tgl, int bln, int thn, int itemTypeID, int pakaiTipe)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ReceiptNo,A.ReceiptDate,A.POID,A.PONo,A.SupplierId,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.Status,A.ReceiptType,A.ItemTypeID,A.DepoID from Receipt as A where A.Status>-1 and day(A.ReceiptDate) =" + tgl + " and month(A.ReceiptDate) =" + bln + " and year(A.ReceiptDate) =" + thn + " and A.ItemTypeID =" + itemTypeID + " and A.ReceiptType=" + pakaiTipe);
            strError = dataAccess.Error;
            arrReceipt = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceipt.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrReceipt.Add(new Receipt());

            return arrReceipt;
        }
        public ArrayList RetrieveByKurangTgl(int tgl, int bln, int thn, int itemTypeID, int pakaiTipe)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ReceiptNo,A.ReceiptDate,A.POID,A.PONo,A.SupplierId,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.Status,A.ReceiptType,A.ItemTypeID,A.DepoID from Receipt as A where A.Status>-1 and day(A.ReceiptDate) <" + tgl + " and month(A.ReceiptDate) =" + bln + " and year(A.ReceiptDate) =" + thn + " and A.ItemTypeID =" + itemTypeID + " and A.ReceiptType=" + pakaiTipe);
            strError = dataAccess.Error;
            arrReceipt = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceipt.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrReceipt.Add(new Receipt());

            return arrReceipt;
        }
        public ArrayList RetrieveByTgl(int tgl, int bln, int thn)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ReceiptNo,A.ReceiptDate,A.POID,A.PONo,A.SupplierId,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.Status,A.ReceiptType,A.ItemTypeID from Receipt as A where A.Status>-1 and day(A.ReceiptDate) <=" + tgl + " and month(A.ReceiptDate) =" + bln + " and year(A.ReceiptDate) =" + thn);
            strError = dataAccess.Error;
            arrReceipt = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceipt.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrReceipt.Add(new Receipt());

            return arrReceipt;
        }
        public ArrayList RetrieveByPeriode(string drTgl, string sdTgl, int itemTypeID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ReceiptNo,A.ReceiptDate,A.POID,A.PONo,A.SupplierId,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.Status,A.ReceiptType,A.ItemTypeID,A.DepoID from Receipt as A where A.Status>-1 and convert(varchar,A.ReceiptDate,112) >= '" + drTgl + "' and convert(varchar,A.ReceiptDate,112) <='" + sdTgl + "' and A.ItemTypeID = " + itemTypeID);
            strError = dataAccess.Error;
            arrReceipt = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceipt.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrReceipt.Add(new Receipt());

            return arrReceipt;
        }
        public ArrayList RetrieveOpenStatusForAll(string thbl, int itemTypeID, int groupID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ReceiptNo,A.ReceiptDate,A.POID,A.PONo,A.DepoID,A.SupplierId,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.Status,A.ReceiptType,A.ItemTypeID from Receipt as A where A.Status>-1 and LEFT(convert(varchar,A.ReceiptDate,112),6) = '" + thbl + "' order by ID");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select distinct A.ID,A.ReceiptNo,A.ReceiptDate,A.POID,A.PONo,A.DepoID,A.SupplierId,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime," +
                "A.Status,A.ReceiptType,B.ItemTypeID from Receipt as A,ReceiptDetail as B where A.ID=B.ReceiptID and A.Status>-1  and B.RowStatus>-1 and B.GroupID=" + groupID + " and B.ItemTypeID=" + itemTypeID + " and " +
                "LEFT(convert(varchar,A.ReceiptDate,112),6) = '" + thbl + "' order by ID");
            strError = dataAccess.Error;
            arrReceipt = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceipt.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrReceipt.Add(new Receipt());

            return arrReceipt;
        }
        public ArrayList RetrieveOpenStatusApprove(string Approve)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select A.ID,A.ReceiptNo,A.ReceiptDate,A. SupplierId,C.SupplierName," +
                "C.SupplierCode,A.Status,A.CreatedBy,A.POID,A.PONo as PoNo,A.ItemTypeID from Receipt as A,SuppPurch as C " +
                " where  A.SupplierId = C.ID  and A.Status = " + Approve + " order by A.ReceiptNo";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrReceipt = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceipt.Add(GenerateObjectListbyapprove(sqlDataReader));
                }
            }
            else
                arrReceipt.Add(new Receipt());

            return arrReceipt;
        }
        private string Criteria()
        {
            string txt = (HttpContext.Current.Session["RowStatus"].ToString() != null) ? HttpContext.Current.Session["RowStatus"].ToString() : string.Empty;
            return txt;
        }
        public ArrayList RetrieveByTagihan()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            #region 
            //string strSQL = "select  top 200  *,Total+PPN as Tagihan from ( " +
            //    "SELECT  ID,POID,ReceiptType,(select GroupDescription from groupspurchn where ID=A.ReceiptType ) as [Group],SupplierId, "+
            //    "(select SupplierName from supppurch where ID=A.SupplierId) as Supplier,(select case when len(rtrim(NPWP))<20 or ISNULL(NPWP,'-')='-' "+
            //    "then 'NPWP belum lengkap' else NPWP end NPWP from supppurch where ID=A.SupplierId) as NPWP,InvoiceNo,ReceiptNo,ReceiptDate, "+
            //    "dateadd(day,(select hari from TermOfPay where TermPay in (select Termin  from POPurchn where ID=A.POID)),ReceiptDate) as tglJatuhTempo, "+
            //    "(select nama from MataUang where ID in (select crc from POPurchn where ID=A.POID)) as Currency, "+
            //    "(select SUM(total) from (select RCD.receiptID,RCD.ID,(RCD.quantity * POD.Price)-(RCD.quantity * POD.Price * (PO.Disc /100)) as total from ReceiptDetail RCD, "+
            //    "POPurchnDetail POD,POPurchn PO where RCD.PODetailID = POD.ID and PO.ID=POD.POID "+this.Criteria()+") as Pr where ReceiptID=A.ID) as Total, "+
            //    "(select SUM(total1) from (select RCD.receiptID,RCD.ID,((RCD.quantity * POD.Price)-((RCD.quantity * POD.Price) * (PO.Disc /100)))*(PO.PPN/100)  as total1 from ReceiptDetail RCD, "+
            //    "POPurchnDetail POD,POPurchn PO where RCD.PODetailID = POD.ID and PO.ID=POD.POID " + this.Criteria() + ") as Pr1 where ReceiptID=A.ID) as PPN " +
            //    "FROM Receipt A where [status]=0) as receipt  order by ID desc /*receiptno*/ ";
            #endregion
            string strSQL = "SELECT  top 200 *,Total+PPN AS Tagihan FROM ( " +
                    "SELECT A.ID,A.POID,A.ReceiptType,(SELECT GroupDescription FROM groupspurchn WHERE ID=A.ReceiptType ) AS [Group],A.SupplierId, " +
                    "Case When PO.SubCompanyID>0 then (SELECT SupplierName FROM SuppPurch where CoID=PO.SubCompanyID) ELSE  " +
                    "SU.SupplierName END  AS Supplier,CASE WHEN PO.SubCompanyID>0 THEN " +
                    "(SELECT NPWP FROM SuppPurch WHERE CoID=PO.SubCompanyID)ELSE " +
                    "CASE WHEN LEN(RTRIM(SU.NPWP))<20 OR SU.NPWP IS NULL THEN 'NPWP Belum Lengkap' ELSE SU.NPWP END END NPWP, " +
                    "A.InvoiceNo,A.ReceiptNo,A.ReceiptDate, " +
                    "DATEADD(DAY,(SELECT hari FROm TermOfPay Where TermPay =PO.Termin),ReceiptDate)AS TglJatuhTempo, " +
                    "(SELECT nama FROM MataUang where ID=PO.Crc)Currency, " +
                    "(SELECT SUM(Total) FROM(SELECT RCD.ReceiptID, (RCD.Quantity*POD.Price)-(RCD.Quantity*POD.Price*(PO.Disc/100)) AS Total " +
                    " FROM ReceiptDetail RCD,POPurchnDetail POD WHERE RCD.PODetailID=POD.ID " + this.Criteria() + ") AS X WHERE X.ReceiptID=A.ID) AS Total, " +
                    "(SELECT SUM(Total) FROM(SELECT RCD.ReceiptID, ((RCD.Quantity*POD.Price)-(RCD.Quantity*POD.Price*(PO.Disc/100)))*(PO.PPN/100) AS Total " +
                    " FROM ReceiptDetail RCD,POPurchnDetail POD WHERE RCD.PODetailID=POD.ID" + this.Criteria() + ") AS X WHERE X.ReceiptID=A.ID) AS PPN " +
                    "FROM Receipt A " +
                    "LEFT JOIN POPurchn PO ON PO.ID=A.POID " +
                    "LEFT JOIN SuppPurch SU ON SU.ID=A.SupplierId " +
                    "WHERE A.Status=0 " +
                    ") as X ORDER BY X.ID Desc";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrReceipt = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceipt.Add(GenerateObjectByTagihan(sqlDataReader));
                }
            }
            else
                arrReceipt.Add(new Receipt());

            return arrReceipt;
        }
        public ArrayList RetrieveByTagihanGroup(string group,string notinreceiptno)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            #region
            string strSQL = "select  top 200 *,Total+PPN as Tagihan from ( " +
                "SELECT  ID,POID,ReceiptType,(select GroupDescription from groupspurchn where ID=A.ReceiptType ) as [Group],SupplierId, " +
                "(select SupplierName from supppurch where ID=A.SupplierId) as Supplier,(select case when len(rtrim(NPWP))<20 or ISNULL(NPWP,'-')='-' " +
                "then 'NPWP belum lengkap' else NPWP end NPWP from supppurch where ID=A.SupplierId) as NPWP,InvoiceNo,ReceiptNo,ReceiptDate, " +
                "dateadd(day,(select hari from TermOfPay where TermPay in (select Termin  from POPurchn where ID=A.POID)),ReceiptDate) as tglJatuhTempo, " +
                "(select nama from MataUang where ID in (select crc from POPurchn where ID=A.POID)) as Currency, " +
                "(select SUM(total) from (select RCD.receiptID,RCD.ID,(RCD.quantity * POD.Price)-(RCD.quantity * POD.Price * (PO.Disc /100)) as total from ReceiptDetail RCD, " +
                "POPurchnDetail POD,POPurchn PO where RCD.PODetailID = POD.ID and PO.ID=POD.POID " + this.Criteria() + ") as Pr where ReceiptID=A.ID) as Total, " +
                "(select SUM(total1) from (select RCD.receiptID,RCD.ID,((RCD.quantity * POD.Price)-((RCD.quantity * POD.Price) * (PO.Disc /100)))*(PO.PPN/100)  as total1 from ReceiptDetail RCD, " +
                "POPurchnDetail POD,POPurchn PO where RCD.PODetailID = POD.ID and PO.ID=POD.POID " + this.Criteria() + ") as Pr1 where ReceiptID=A.ID) as PPN " +
                "FROM Receipt A where [status]=0) as receipt where [Group]='" + group + "' and receiptno not in (" + notinreceiptno + ") order by /*receiptno,*/ID desc ";
            #endregion
            strSQL = "SELECT  top 200 *,Total+PPN AS Tagihan FROM ( " +
                    "SELECT A.ID,A.POID,A.ReceiptType,(SELECT GroupDescription FROM groupspurchn WHERE ID=A.ReceiptType ) AS [Group],A.SupplierId, " +
                    "Case When PO.SubCompanyID>0 then (SELECT SupplierName FROM SuppPurch where CoID=PO.SubCompanyID) ELSE  " +
                    "SU.SupplierName END  AS Supplier,CASE WHEN PO.SubCompanyID>0 THEN " +
                    "(SELECT NPWP FROM SuppPurch WHERE CoID=PO.SubCompanyID)ELSE " +
                    "CASE WHEN LEN(RTRIM(SU.NPWP))<20 OR SU.NPWP IS NULL THEN 'NPWP Belum Lengkap' ELSE SU.NPWP END END NPWP, " +
                    "A.InvoiceNo,A.ReceiptNo,A.ReceiptDate, " +
                    "DATEADD(DAY,(SELECT hari FROm TermOfPay Where TermPay =PO.Termin),ReceiptDate)AS TglJatuhTempo, " +
                    "(SELECT nama FROM MataUang where ID=PO.Crc)Currency, " +
                    "(SELECT SUM(Total) FROM(SELECT RCD.ReceiptID, (RCD.Quantity*POD.Price)-(RCD.Quantity*POD.Price*(PO.Disc/100)) AS Total " +
                    " FROM ReceiptDetail RCD,POPurchnDetail POD WHERE RCD.PODetailID=POD.ID " + this.Criteria() + ") AS X WHERE X.ReceiptID=A.ID) AS Total, " +
                    "(SELECT SUM(Total) FROM(SELECT RCD.ReceiptID, ((RCD.Quantity*POD.Price)-(RCD.Quantity*POD.Price*(PO.Disc/100)))*(PO.PPN/100) AS Total " +
                    " FROM ReceiptDetail RCD,POPurchnDetail POD WHERE RCD.PODetailID=POD.ID" + this.Criteria() + ") AS X WHERE X.ReceiptID=A.ID) AS PPN " +
                    "FROM Receipt A " +
                    "LEFT JOIN POPurchn PO ON PO.ID=A.POID " +
                    "LEFT JOIN SuppPurch SU ON SU.ID=A.SupplierId " +
                    "WHERE A.Status=0 and receiptno not in (" + notinreceiptno + ") "+
                    ") as X WHERE X.[Group]='" + group + "' " +
                    "ORDER BY X.ID Desc";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrReceipt = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceipt.Add(GenerateObjectByTagihan(sqlDataReader));
                }
            }
            else
                arrReceipt.Add(new Receipt());

            return arrReceipt;
        }
        public ArrayList RetrieveByTagihanReceiptNo(string ReceiptNo, string notinreceiptno)
        {
            /*
             * Penambahan RCD.RowStatus >-1
             */
            
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            #region query depreciated
            string strSQL = "select  top 200 *,Total+PPN as Tagihan from ( " +
                "SELECT  ID,POID,ReceiptType,(select GroupDescription from groupspurchn where ID=A.ReceiptType ) as [Group],SupplierId, " +
                "(select SupplierName from supppurch where ID=A.SupplierId) as Supplier,(select case when len(rtrim(NPWP))<20 or ISNULL(NPWP,'-')='-' " +
                "then 'NPWP belum lengkap' else NPWP end NPWP from supppurch where ID=A.SupplierId) as NPWP,InvoiceNo,ReceiptNo,ReceiptDate, " +
                "dateadd(day,(select hari from TermOfPay where TermPay in (select Termin  from POPurchn where ID=A.POID)),ReceiptDate) as tglJatuhTempo, " +
                "(select nama from MataUang where ID in (select crc from POPurchn where ID=A.POID)) as Currency, " +
                "(select SUM(total) from (select RCD.receiptID,RCD.ID,(RCD.quantity * POD.Price)-(RCD.quantity * POD.Price * (PO.Disc /100)) as total from ReceiptDetail RCD, " +
                "POPurchnDetail POD,POPurchn PO where RCD.PODetailID = POD.ID and PO.ID=POD.POID " + this.Criteria() + ") as Pr where ReceiptID=A.ID) as Total, " +
                "(select SUM(total1) from (select RCD.receiptID,RCD.ID,((RCD.quantity * POD.Price)-((RCD.quantity * POD.Price) * (PO.Disc /100)))*(PO.PPN/100)  as total1 from ReceiptDetail RCD, " +
                "POPurchnDetail POD,POPurchn PO where RCD.PODetailID = POD.ID and PO.ID=POD.POID " + this.Criteria() + ") as Pr1 where ReceiptID=A.ID) as PPN " +
                "FROM Receipt A where [status]=0) as receipt where ReceiptNo='" + ReceiptNo + "'  and receiptno not in (" + notinreceiptno + ") order by receiptno ";
            #endregion
            strSQL = "SELECT  top 200 *,Total+PPN AS Tagihan FROM ( " +
                    "SELECT A.ID,A.POID,A.ReceiptType,(SELECT GroupDescription FROM groupspurchn WHERE ID=A.ReceiptType ) AS [Group],A.SupplierId, " +
                    "Case When PO.SubCompanyID>0 then (SELECT SupplierName FROM SuppPurch where CoID=PO.SubCompanyID) ELSE  " +
                    "SU.SupplierName END  AS Supplier,CASE WHEN PO.SubCompanyID>0 THEN " +
                    "(SELECT NPWP FROM SuppPurch WHERE CoID=PO.SubCompanyID)ELSE " +
                    "CASE WHEN LEN(RTRIM(SU.NPWP))<20 OR SU.NPWP IS NULL THEN 'NPWP Belum Lengkap' ELSE SU.NPWP END END NPWP, " +
                    "A.InvoiceNo,A.ReceiptNo,A.ReceiptDate, " +
                    "DATEADD(DAY,(SELECT hari FROm TermOfPay Where TermPay =PO.Termin),ReceiptDate)AS TglJatuhTempo, " +
                    "(SELECT nama FROM MataUang where ID=PO.Crc)Currency, " +
                    "(SELECT SUM(Total) FROM(SELECT RCD.ReceiptID, (RCD.Quantity*POD.Price)-(RCD.Quantity*POD.Price*(PO.Disc/100)) AS Total " +
                    " FROM ReceiptDetail RCD,POPurchnDetail POD WHERE RCD.PODetailID=POD.ID "+ this.Criteria()+") AS X WHERE X.ReceiptID=A.ID) AS Total, " +
                    "(SELECT SUM(Total) FROM(SELECT RCD.ReceiptID, ((RCD.Quantity*POD.Price)-(RCD.Quantity*POD.Price*(PO.Disc/100)))*(PO.PPN/100) AS Total " +
                    " FROM ReceiptDetail RCD,POPurchnDetail POD WHERE RCD.PODetailID=POD.ID" + this.Criteria() + ") AS X WHERE X.ReceiptID=A.ID) AS PPN " +
                    "FROM Receipt A " +
                    "LEFT JOIN POPurchn PO ON PO.ID=A.POID " +
                    "LEFT JOIN SuppPurch SU ON SU.ID=A.SupplierId " +
                    "WHERE A.Status=0 and ReceiptNo='" + ReceiptNo + "'  and receiptno not in (" + notinreceiptno + ") " +
                    ") as X ORDER BY X.ReceiptNo";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrReceipt = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceipt.Add(GenerateObjectByTagihan(sqlDataReader));
                }
            }
            else
                arrReceipt.Add(new Receipt());

            return arrReceipt;
        }
        public ArrayList RetrieveByTagihanReceiptNoSupplier(string Supplier, string notinreceiptno, string groupdescription)
        {
            string kriteria=string.Empty;
            if (groupdescription.Trim()!=string.Empty)
                kriteria = " AND A.receipttype IN (SELECT ID FROM groupspurchn WHERE groupdescription='" + groupdescription + "')";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            #region depreciated
            string strSQL = "select top 200 *,Total+PPN as Tagihan from ( " +
                "SELECT  ID,POID,ReceiptType,(select GroupDescription from groupspurchn where ID=A.ReceiptType ) as [Group],SupplierId, " +
                "(select SupplierName from supppurch where ID=A.SupplierId) as Supplier,(select case when len(rtrim(NPWP))<20 or ISNULL(NPWP,'-')='-' " +
                "then 'NPWP belum lengkap' else NPWP end NPWP from supppurch where ID=A.SupplierId) as NPWP,InvoiceNo,ReceiptNo,ReceiptDate, " +
                "dateadd(day,(select hari from TermOfPay where TermPay in (select Termin  from POPurchn where ID=A.POID)),ReceiptDate) as tglJatuhTempo, " +
                "(select nama from MataUang where ID in (select crc from POPurchn where ID=A.POID)) as Currency, " +
                "(select SUM(total) from (select RCD.receiptID,RCD.ID,(RCD.quantity * POD.Price)-(RCD.quantity * POD.Price * (PO.Disc /100)) as total from ReceiptDetail RCD, " +
                "POPurchnDetail POD,POPurchn PO where RCD.PODetailID = POD.ID and PO.ID=POD.POID " + this.Criteria() + ") as Pr where ReceiptID=A.ID) as Total, " +
                "(select SUM(total1) from (select RCD.receiptID,RCD.ID,((RCD.quantity * POD.Price)-((RCD.quantity * POD.Price) * (PO.Disc /100)))*(PO.PPN/100)  as total1 from ReceiptDetail RCD, " +
                "POPurchnDetail POD,POPurchn PO where RCD.PODetailID = POD.ID and PO.ID=POD.POID " + this.Criteria() + ") as Pr1 where ReceiptID=A.ID) as PPN " +
                "FROM Receipt A where A.[status]=0 "+kriteria+" and supplierID in(select ID from SuppPurch where rowstatus>-1 and suppliername like '%" + Supplier + "%')) as receipt where receiptno not in (" + notinreceiptno + ") order by receiptno,ID desc ";
            #endregion
            strSQL = "SELECT  top 200 *,Total+PPN AS Tagihan FROM ( " +
                    "SELECT A.ID,A.POID,A.ReceiptType,(SELECT GroupDescription FROM groupspurchn WHERE ID=A.ReceiptType ) AS [Group],A.SupplierId, " +
                    "Case When PO.SubCompanyID>0 then (SELECT SupplierName FROM SuppPurch where CoID=PO.SubCompanyID) ELSE  " +
                    "SU.SupplierName END  AS Supplier,CASE WHEN PO.SubCompanyID>0 THEN " +
                    "(SELECT NPWP FROM SuppPurch WHERE CoID=PO.SubCompanyID)ELSE " +
                    "CASE WHEN LEN(RTRIM(SU.NPWP))<20 OR SU.NPWP IS NULL THEN 'NPWP Belum Lengkap' ELSE SU.NPWP END END NPWP, " +
                    "A.InvoiceNo,A.ReceiptNo,A.ReceiptDate, " +
                    "DATEADD(DAY,(SELECT hari FROm TermOfPay Where TermPay =PO.Termin),ReceiptDate)AS TglJatuhTempo, " +
                    "(SELECT nama FROM MataUang where ID=PO.Crc)Currency, " +
                    "(SELECT SUM(Total) FROM(SELECT RCD.ReceiptID, (RCD.Quantity*POD.Price)-(RCD.Quantity*POD.Price*(PO.Disc/100)) AS Total " +
                    " FROM ReceiptDetail RCD,POPurchnDetail POD WHERE RCD.PODetailID=POD.ID " + this.Criteria() + ") AS X WHERE X.ReceiptID=A.ID) AS Total, " +
                    "(SELECT SUM(Total) FROM(SELECT RCD.ReceiptID, ((RCD.Quantity*POD.Price)-(RCD.Quantity*POD.Price*(PO.Disc/100)))*(PO.PPN/100) AS Total " +
                    " FROM ReceiptDetail RCD,POPurchnDetail POD WHERE RCD.PODetailID=POD.ID" + this.Criteria() + ") AS X WHERE X.ReceiptID=A.ID) AS PPN " +
                    "FROM Receipt A " +
                    "LEFT JOIN POPurchn PO ON PO.ID=A.POID " +
                    "LEFT JOIN SuppPurch SU ON SU.ID=A.SupplierId " +
                    "WHERE A.Status=0 " + kriteria + "   and receiptno not in (" + notinreceiptno + ") " +
                    ") as X WHERE  X.Supplier like '%" + Supplier + "%' ORDER BY X.ReceiptNo";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrReceipt = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceipt.Add(GenerateObjectByTagihan(sqlDataReader));
                }
            }
            else
                arrReceipt.Add(new Receipt());

            return arrReceipt;
        }
        public ArrayList RetrieveByTagihanNotInReceiptNo(string ReceiptNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            #region Depreciated
            string strSQL = "select  top 200  *,Total+PPN as Tagihan from ( " +
                "SELECT  ID,POID,ReceiptType,(select GroupDescription from groupspurchn where ID=A.ReceiptType ) as [Group],SupplierId, " +
                "(select SupplierName from supppurch where ID=A.SupplierId) as Supplier,(select case when len(rtrim(NPWP))<20 or ISNULL(NPWP,'-')='-' " +
                "then 'NPWP belum lengkap' else NPWP end NPWP from supppurch where ID=A.SupplierId) as NPWP,InvoiceNo,ReceiptNo,ReceiptDate, " +
                "dateadd(day,(select hari from TermOfPay where TermPay in (select Termin  from POPurchn where ID=A.POID)),ReceiptDate) as tglJatuhTempo, " +
                "(select nama from MataUang where ID in (select crc from POPurchn where ID=A.POID)) as Currency, " +
                "(select SUM(total) from (select RCD.receiptID,RCD.ID,(RCD.quantity * POD.Price)-(RCD.quantity * POD.Price * (PO.Disc /100)) as total from ReceiptDetail RCD, " +
                "POPurchnDetail POD,POPurchn PO where RCD.PODetailID = POD.ID and PO.ID=POD.POID " + this.Criteria() + ") as Pr where ReceiptID=A.ID) as Total, " +
                "(select SUM(total1) from (select RCD.receiptID,RCD.ID,((RCD.quantity * POD.Price)-((RCD.quantity * POD.Price) * (PO.Disc /100)))*(PO.PPN/100)  as total1 from ReceiptDetail RCD, " +
                "POPurchnDetail POD,POPurchn PO where RCD.PODetailID = POD.ID and PO.ID=POD.POID " + this.Criteria() + ") as Pr1 where ReceiptID=A.ID) as PPN " +
                "FROM Receipt A where [status]=0) as receipt where ReceiptNo not in(" + ReceiptNo + ") order by receiptno,ID desc ";
            #endregion
            strSQL = "SELECT  top 200 *,Total+PPN AS Tagihan FROM ( " +
                    "SELECT A.ID,A.POID,A.ReceiptType,(SELECT GroupDescription FROM groupspurchn WHERE ID=A.ReceiptType ) AS [Group],A.SupplierId, " +
                    "Case When PO.SubCompanyID>0 then (SELECT SupplierName FROM SuppPurch where CoID=PO.SubCompanyID) ELSE  " +
                    "SU.SupplierName END  AS Supplier,CASE WHEN PO.SubCompanyID>0 THEN " +
                    "(SELECT NPWP FROM SuppPurch WHERE CoID=PO.SubCompanyID)ELSE " +
                    "CASE WHEN LEN(RTRIM(SU.NPWP))<20 OR SU.NPWP IS NULL THEN 'NPWP Belum Lengkap' ELSE SU.NPWP END END NPWP, " +
                    "A.InvoiceNo,A.ReceiptNo,A.ReceiptDate, " +
                    "DATEADD(DAY,(SELECT ISNULL(hari,0) FROm TermOfPay Where TermPay =PO.Termin),ReceiptDate)AS TglJatuhTempo, " +
                    "(SELECT nama FROM MataUang where ID=PO.Crc)Currency, " +
                    "(SELECT SUM(Total) FROM(SELECT RCD.ReceiptID, (RCD.Quantity*POD.Price)-(RCD.Quantity*POD.Price*(PO.Disc/100)) AS Total " +
                    " FROM ReceiptDetail RCD,POPurchnDetail POD WHERE RCD.PODetailID=POD.ID " + this.Criteria() + ") AS X WHERE X.ReceiptID=A.ID) AS Total, " +
                    "(SELECT SUM(Total) FROM(SELECT RCD.ReceiptID, ((RCD.Quantity*POD.Price)-(RCD.Quantity*POD.Price*(PO.Disc/100)))*(PO.PPN/100) AS Total " +
                    " FROM ReceiptDetail RCD,POPurchnDetail POD WHERE RCD.PODetailID=POD.ID" + this.Criteria() + ") AS X WHERE X.ReceiptID=A.ID) AS PPN " +
                    "FROM Receipt A " +
                    "LEFT JOIN POPurchn PO ON PO.ID=A.POID " +
                    "LEFT JOIN SuppPurch SU ON SU.ID=A.SupplierId " +
                    "WHERE A.Status=0  and receiptno not in (" + ReceiptNo + ") " +
                    ") as X ORDER BY X.ReceiptNo";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrReceipt = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceipt.Add(GenerateObjectByTagihan(sqlDataReader));
                }
            }
            else
                arrReceipt.Add(new Receipt());

            return arrReceipt;
        }
        public ArrayList RetrieveByTagihanInvoiceNo(string InvoiceNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            #region Depreciated
            string strSQL = "select  top 200  *,Total+PPN as Tagihan from ( " +
                "SELECT  ID,POID,ReceiptType,(select GroupDescription from groupspurchn where ID=A.ReceiptType ) as [Group],SupplierId, " +
                "(select SupplierName from supppurch where ID=A.SupplierId) as Supplier,(select case when len(rtrim(NPWP))<20 or ISNULL(NPWP,'-')='-' " +
                "then 'NPWP belum lengkap' else NPWP end NPWP from supppurch where ID=A.SupplierId) as NPWP,InvoiceNo,ReceiptNo,ReceiptDate, " +
                "dateadd(day,(select hari from TermOfPay where TermPay in (select Termin  from POPurchn where ID=A.POID)),ReceiptDate) as tglJatuhTempo, " +
                "(select nama from MataUang where ID in (select crc from POPurchn where ID=A.POID)) as Currency, " +
                "(select SUM(total) from (select RCD.receiptID,RCD.ID,(RCD.quantity * POD.Price)-(RCD.quantity * POD.Price * (PO.Disc /100)) as total from ReceiptDetail RCD, " +
                "POPurchnDetail POD,POPurchn PO where RCD.PODetailID = POD.ID and PO.ID=POD.POID " + this.Criteria() + ") as Pr where ReceiptID=A.ID) as Total, " +
                "(select SUM(total1) from (select RCD.receiptID,RCD.ID,((RCD.quantity * POD.Price)-((RCD.quantity * POD.Price) * (PO.Disc /100)))*(PO.PPN/100)  as total1 from ReceiptDetail RCD, " +
                "POPurchnDetail POD,POPurchn PO where RCD.PODetailID = POD.ID and PO.ID=POD.POID " + this.Criteria() + ") as Pr1 where ReceiptID=A.ID) as PPN " +
                "FROM Receipt A where invoiceno='" + InvoiceNo + "') as receipt order by receiptno,ID desc ";
            #endregion
            strSQL = "SELECT  top 200 *,Total+PPN AS Tagihan FROM ( " +
                    "SELECT A.ID,A.POID,A.ReceiptType,(SELECT GroupDescription FROM groupspurchn WHERE ID=A.ReceiptType ) AS [Group],A.SupplierId, " +
                    "Case When PO.SubCompanyID>0 then (SELECT SupplierName FROM SuppPurch where CoID=PO.SubCompanyID) ELSE  " +
                    "SU.SupplierName END  AS Supplier,CASE WHEN PO.SubCompanyID>0 THEN " +
                    "(SELECT NPWP FROM SuppPurch WHERE CoID=PO.SubCompanyID)ELSE " +
                    "CASE WHEN LEN(RTRIM(SU.NPWP))<20 OR SU.NPWP IS NULL THEN 'NPWP Belum Lengkap' ELSE SU.NPWP END END NPWP, " +
                    "A.InvoiceNo,A.ReceiptNo,A.ReceiptDate, " +
                    "DATEADD(DAY,(SELECT hari FROm TermOfPay Where TermPay =PO.Termin),ReceiptDate)AS TglJatuhTempo, " +
                    "(SELECT nama FROM MataUang where ID=PO.Crc)Currency, " +
                    "(SELECT SUM(Total) FROM(SELECT RCD.ReceiptID, (RCD.Quantity*POD.Price)-(RCD.Quantity*POD.Price*(PO.Disc/100)) AS Total " +
                    " FROM ReceiptDetail RCD,POPurchnDetail POD WHERE RCD.PODetailID=POD.ID " + this.Criteria() + ") AS X WHERE X.ReceiptID=A.ID) AS Total, " +
                    "(SELECT SUM(Total) FROM(SELECT RCD.ReceiptID, ((RCD.Quantity*POD.Price)-(RCD.Quantity*POD.Price*(PO.Disc/100)))*(PO.PPN/100) AS Total " +
                    " FROM ReceiptDetail RCD,POPurchnDetail POD WHERE RCD.PODetailID=POD.ID" + this.Criteria() + ") AS X WHERE X.ReceiptID=A.ID) AS PPN " +
                    "FROM Receipt A " +
                    "LEFT JOIN POPurchn PO ON PO.ID=A.POID " +
                    "LEFT JOIN SuppPurch SU ON SU.ID=A.SupplierId " +
                    "WHERE A.Status=0 AND invoiceno='" + InvoiceNo + "' " +
                    ") as X ORDER BY X.ReceiptNo";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrReceipt = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceipt.Add(GenerateObjectByTagihan(sqlDataReader));
                }
            }
            else
                arrReceipt.Add(new Receipt());

            return arrReceipt;
        }
        public int GetJTP(int POID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select hari from TermOfPay where TermPay in (select Termin  from POPurchn where ID=" + POID + ")";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            int JTP=0;
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    JTP = Convert.ToInt32(sqlDataReader["hari"]); ;
                }
            }
            return JTP;
        }
        public ArrayList RetrieveOpenStatus(string strTipeReceipt)
        {
            string tipeReceipt = string.Empty;

            if (strTipeReceipt!=string.Empty)
                tipeReceipt = "and left(A.ReceiptNo,2)='" + strTipeReceipt + "'";
            //utk MRS
            if (strTipeReceipt == "KS")
                tipeReceipt = "and left(A.ReceiptNo,2) in ('KO','KS','KK')";
            if (strTipeReceipt == "CS")
                tipeReceipt = "and left(A.ReceiptNo,2) in ('CO','CS','CK')";
            if (strTipeReceipt == "JS")
                tipeReceipt = "and left(A.ReceiptNo,2) in ('JO','JS','JK')";
            //utk RMS
            if (strTipeReceipt == "KP")
                tipeReceipt = "and left(A.ReceiptNo,2) in ('KP','KM')";
            if (strTipeReceipt == "CP")
                tipeReceipt = "and left(A.ReceiptNo,2) in ('CP','CM')";
            if (strTipeReceipt == "JP")
                tipeReceipt = "and left(A.ReceiptNo,2) in ('JP','JM')";
            //utk ARS
            if (strTipeReceipt == "KA")
                tipeReceipt = "and left(A.ReceiptNo,2) in ('KA')";
            if (strTipeReceipt == "CA")
                tipeReceipt = "and left(A.ReceiptNo,2) in ('CA')";
            if (strTipeReceipt == "JA")
                tipeReceipt = "and left(A.ReceiptNo,2) in ('JA')";
            //utk Biaya
            if (strTipeReceipt == "KB")
                tipeReceipt = "and left(A.ReceiptNo,2) in ('KB')";
            if (strTipeReceipt == "CB")
                tipeReceipt = "and left(A.ReceiptNo,2) in ('CB')";
            if (strTipeReceipt == "JB")
                tipeReceipt = "and left(A.ReceiptNo,2) in ('JB')";
            //utk Asset
            if (strTipeReceipt == "KC")
                tipeReceipt = "and left(A.ReceiptNo,2) in ('KC')";
            if (strTipeReceipt == "CC")
                tipeReceipt = "and left(A.ReceiptNo,2) in ('CC','CE')";
            if (strTipeReceipt == "JC")
                tipeReceipt = "and left(A.ReceiptNo,2) in ('JC')";
            


            string strSQL = "select top 200 A.ID,A.ReceiptNo,A.ReceiptDate,A.PONo,B.SPPNo,A.POID,A.SupplierId,A.Status,A.CreatedBy,B.ItemID,B.Quantity,"+
                "E.Qty as QtyPO, case when B.ItemTypeID =1 then (select ItemCode from Inventory where ID =B.ItemID ) "+
	            "when B.ItemTypeID =2 then (select ItemCode from asset where ID =B.ItemID ) "+
                "when B.ItemTypeID =3 then "+ItemCodeForBiaya("B")+" end ItemCode, " +
                "case when B.ItemTypeID =1 then (select ItemName from Inventory where ID =B.ItemID ) "+
                "when B.ItemTypeID =2 then (select ItemName from asset where ID =B.ItemID ) " +
	            "when B.ItemTypeID =3 then /*(select ItemName from biaya where ID =B.ItemID )*/ "+
                 ItemNameFromBiaya("B")+"end ItemName,"+
                "D.SupplierName,D.SupplierCode from Receipt as A, ReceiptDetail as B,Inventory as C,SuppPurch as D,"+
                "POPurchnDetail as E where B.PODetailID = E.ID and A.Status>-1 and A.ID=B.ReceiptID and B.ItemID=C.ID and "+
                "B.RowStatus>-1 and A.SupplierId = D.ID " + tipeReceipt + " order by A.id desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrReceipt = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceipt.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrReceipt.Add(new Receipt());

            return arrReceipt;
        }
        public ArrayList RetrieveOpenStatusForAsset(string strTipeReceipt)
        {
            string tipeReceipt = string.Empty;

            //if (strTipeReceipt == "KC")
                tipeReceipt = "and left(A.ReceiptNo,2)='" + strTipeReceipt + "'";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ReceiptNo,A.ReceiptDate,A.PONo,B.SPPNo,A.POID,A.SupplierId,A.Status,A.CreatedBy,B.ItemID,B.Quantity,C.ItemCode,C.ItemName from Receipt as A, ReceiptDetail as B,Asset as C where A.Status>-1 and A.ID=B.ReceiptID and B.ItemID=C.ID and B.RowStatus>-1 " + tipeReceipt + " order by A.ReceiptNo desc");
            strError = dataAccess.Error;
            arrReceipt = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceipt.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrReceipt.Add(new Receipt());

            return arrReceipt;
        }
        public Receipt CekLastDate(string strReceiptCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(max(ReceiptDate),GETDATE()) as ReceiptDate from Receipt where Status>-1 and left(ReceiptNo,4)='" + strReceiptCode + "'");
            strError = dataAccess.Error;
            arrReceipt = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectLastEntry(sqlDataReader);
                }
            }

            return new Receipt();
        }
        public Receipt GetLastReceipt(int itemID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select top 1 A.ID,A.ReceiptNo,A.ReceiptDate,A.PONo,B.SPPNo,A.POID,A.SupplierId," +
                "A.Status,A.CreatedBy,B.ItemID,B.Quantity from Receipt as A, " +
                "ReceiptDetail as B where A.Status>-1 and A.ID=B.ReceiptID and B.ItemID=" +
                itemID + "  and B.RowStatus>-1 order by A.ReceiptDate desc";     
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectLastEntry(sqlDataReader);
                }
            }

            return new Receipt();
        }

        public Receipt GetLastReceiptAsset(int itemID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());           
            string strSQL =
            " select top 1 A.ReceiptDate ReceiptDate" +
            " from Receipt A " +
            " INNER JOIN ReceiptDetail B ON A.ID=B.ReceiptID " +
            " where B.RowStatus>-1 and A.Status>-1 and B.ItemTypeID=2 and B.ItemID=" + itemID + " order by A.ID desc";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectLastEntry(sqlDataReader);
                }
            }

            return new Receipt();
        }

        public Receipt RetrieveByID(int ID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select * from receipt where ID=" + ID;
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Receipt();
        }
        public ArrayList RetrieveOpenStatusForBiaya(string strTipeReceipt)
        {
            string tipeReceipt = string.Empty;

            //if (strTipeReceipt == "KB")
                tipeReceipt = "and left(A.ReceiptNo,2)='"+strTipeReceipt+"'";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ReceiptNo,A.ReceiptDate,A.PONo,B.SPPNo,A.POID,A.SupplierId,A.Status,A.CreatedBy,B.ItemID,B.Quantity,C.ItemCode,C.ItemName from Receipt as A, ReceiptDetail as B,Biaya as C where A.Status>-1 and A.ID=B.ReceiptID and B.ItemID=C.ID and B.RowStatus>-1 " + tipeReceipt + " order by A.ReceiptNo desc");
            strError = dataAccess.Error;
            arrReceipt = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceipt.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrReceipt.Add(new Receipt());

            return arrReceipt;
        }
        public ArrayList RetrieveOpenStatusCriteria(string receiptNo,string strTipeReceipt)
        {
            string tipeReceipt = string.Empty;

            //ambil user.company dulu
            if (strTipeReceipt != string.Empty)
                tipeReceipt = "and left(A.ReceiptNo,2)='" + strTipeReceipt + "'";
            //utk MRS
            if (strTipeReceipt == "KS")
                tipeReceipt = "and left(A.ReceiptNo,2) in ('KO','KS','KK')";
            if (strTipeReceipt == "CS")
                tipeReceipt = "and left(A.ReceiptNo,2) in ('CO','CS','CK')";
            //utk RMS
            if (strTipeReceipt == "KP")
                tipeReceipt = "and left(A.ReceiptNo,2) in ('KP','KM')";
            if (strTipeReceipt == "CP")
                tipeReceipt = "and left(A.ReceiptNo,2) in ('CP','CM')";
            //utk ARS
            if (strTipeReceipt == "KA")
                tipeReceipt = "and left(A.ReceiptNo,2) in ('KA')";
            if (strTipeReceipt == "CA")
                tipeReceipt = "and left(A.ReceiptNo,2) in ('CA')";
            //utk Biaya
            if (strTipeReceipt == "KB")
                tipeReceipt = "and left(A.ReceiptNo,2) in ('KB')";
            if (strTipeReceipt == "CB")
                tipeReceipt = "and left(A.ReceiptNo,2) in ('CB')";
            //utk Aset
            if (strTipeReceipt == "KC")
                tipeReceipt = "and left(A.ReceiptNo,2) in ('KC')";
            if (strTipeReceipt == "CC")
                tipeReceipt = "and left(A.ReceiptNo,2) in ('CC')";
            if (strTipeReceipt == "X0")
                tipeReceipt = " ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ReceiptNo,A.ReceiptDate,A.PONo,B.SPPNo,A.POID,A.SupplierId,A.Status,A.CreatedBy,B.ItemID,B.Quantity,C.ItemCode,C.ItemName,C.ItemName,D.SupplierName,D.SupplierCode,E.Qty as QtyPO from Receipt as A, ReceiptDetail as B,Inventory as C,SuppPurch as D,POPurchnDetail as E where A.Status>-1 and A.ID=B.ReceiptID and B.ItemID=C.ID and B.RowStatus>-1 and A.SupplierId = D.ID and B.PODetailID = E.ID and A.ReceiptNo like '%" + receiptNo + "%' " + tipeReceipt);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ScheduleNo,A.ScheduleDate,A.ExpedisiDetailID,B.ExpedisiName,C.CarType,A.TotalKubikasi,A.Status,A.Rate,A.Keterangan,A.DepoID,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from Receipt as A,Expedisi as B,ExpedisiDetail as C where A.ExpedisiDetailID = C.ID and C.ExpedisiID = B.ID and A.RowStatus = 0 and A.Status = 0 and A.ScheduleNo like '%" + scheduleNo + "%'");
            strError = dataAccess.Error;
            arrReceipt = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceipt.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrReceipt.Add(new Receipt());

            return arrReceipt;
        }
        public ArrayList RetrieveOpenStatusCriteriaForAsset(string receiptNo, string strTipeReceipt)
        {
            string tipeReceipt = string.Empty;

            if (strTipeReceipt == "KC")
                tipeReceipt = "and left(A.ReceiptNo,2)='KC'";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ReceiptNo,A.ReceiptDate,A.PONo,B.SPPNo,A.POID,A.SupplierId,A.Status,A.CreatedBy,B.ItemID,B.Quantity,C.ItemCode,C.ItemName,D.SupplierName,D.SupplierCode,E.Qty as QtyPO from Receipt as A, ReceiptDetail as B,Asset as C,SuppPurch as D,POPurchnDetail as E where A.Status=0 and A.ID=B.ReceiptID and B.ItemID=C.ID and B.RowStatus>-1 and A.SupplierId = D.ID and B.PODetailID = E.ID and A.ReceiptNo like '%" + receiptNo + "%' " + tipeReceipt);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ScheduleNo,A.ScheduleDate,A.ExpedisiDetailID,B.ExpedisiName,C.CarType,A.TotalKubikasi,A.Status,A.Rate,A.Keterangan,A.DepoID,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from Receipt as A,Expedisi as B,ExpedisiDetail as C where A.ExpedisiDetailID = C.ID and C.ExpedisiID = B.ID and A.RowStatus = 0 and A.Status = 0 and A.ScheduleNo like '%" + scheduleNo + "%'");
            strError = dataAccess.Error;
            arrReceipt = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceipt.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrReceipt.Add(new Receipt());

            return arrReceipt;
        }
        public ArrayList RetrieveOpenStatusCriteriaForBiaya(string receiptNo, string strTipeReceipt)
        {
            string tipeReceipt = string.Empty;

            if (strTipeReceipt == "KB")
                tipeReceipt = "and left(A.ReceiptNo,2)='KB'";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ReceiptNo,A.ReceiptDate,A.PONo,B.SPPNo,A.POID,A.SupplierId,A.Status,A.CreatedBy,B.ItemID,B.Quantity,C.ItemCode,C.ItemName,D.SupplierName,D.SupplierCode,E.Qty as QtyPO from Receipt as A, ReceiptDetail as B,Biaya as C,SuppPurch as D,POPurchnDetail as E where A.Status=0 and A.ID=B.ReceiptID and B.ItemID=C.ID and B.RowStatus>-1 and A.SupplierId = D.ID and B.PODetailID = E.ID and A.ReceiptNo like '%" + receiptNo + "%' " + tipeReceipt);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ScheduleNo,A.ScheduleDate,A.ExpedisiDetailID,B.ExpedisiName,C.CarType,A.TotalKubikasi,A.Status,A.Rate,A.Keterangan,A.DepoID,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from Receipt as A,Expedisi as B,ExpedisiDetail as C where A.ExpedisiDetailID = C.ID and C.ExpedisiID = B.ID and A.RowStatus = 0 and A.Status = 0 and A.ScheduleNo like '%" + scheduleNo + "%'");
            strError = dataAccess.Error;
            arrReceipt = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceipt.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrReceipt.Add(new Receipt());

            return arrReceipt;
        }
        public Receipt RetrieveByNo(string strReceiptNo)
        {
            string strSQL = "select ID,ReceiptNo,ReceiptDate,ReceiptType,ReceiptType,PONo,POID,DepoID,SupplierId,Status," +
                            "CreatedTime,CreatedBy,LastModifiedBy,LastModifiedTime,ItemTypeID from Receipt where Status>-1 " +
                            "and ReceiptNo='" + strReceiptNo + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrReceipt = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Receipt();
        }
        public Receipt RetrieveByNoWithStatus(string strReceiptNo, string strTipeReceipt)
        {
            string tipeReceipt = string.Empty;

            if (strTipeReceipt != string.Empty)
                tipeReceipt = "and left(ReceiptNo,2)='" + strTipeReceipt + "'";

            //utk MRS
            if (strTipeReceipt == "KS")
                tipeReceipt = "and left(ReceiptNo,2) in ('KO','KS','KK')";
            if (strTipeReceipt == "CS")
                tipeReceipt = "and left(ReceiptNo,2) in ('CO','CS','CK')";
            if (strTipeReceipt == "JS")
                tipeReceipt = "and left(ReceiptNo,2) in ('JO','JS','JK')";

            //utk RMS
            if (strTipeReceipt == "KP")
                tipeReceipt = "and left(ReceiptNo,2) in ('KP','KM')";
            if (strTipeReceipt == "CP")
                tipeReceipt = "and left(ReceiptNo,2) in ('CP','CM')";
            if (strTipeReceipt == "JP")
                tipeReceipt = "and left(ReceiptNo,2) in ('JP','JM')";

            //utk ARS
            if (strTipeReceipt == "KA")
                tipeReceipt = "and left(ReceiptNo,2) in ('KA')";
            if (strTipeReceipt == "CA")
                tipeReceipt = "and left(ReceiptNo,2) in ('CA')";
            if (strTipeReceipt == "JA")
                tipeReceipt = "and left(ReceiptNo,2) in ('JA')";

            //utk Biaya
            if (strTipeReceipt == "KB")
                tipeReceipt = "and left(ReceiptNo,2) in ('KB')";
            if (strTipeReceipt == "CB")
                tipeReceipt = "and left(ReceiptNo,2) in ('CB')";
            if (strTipeReceipt == "JB")
                tipeReceipt = "and left(ReceiptNo,2) in ('JB')";

            //utk Aset Tunggal
            if (strTipeReceipt == "KC")
                tipeReceipt = "and left(ReceiptNo,2) in ('KC')";
            if (strTipeReceipt == "CC")
                tipeReceipt = "and left(ReceiptNo,2) in ('CC')";
            if (strTipeReceipt == "JC")
                tipeReceipt = "and left(ReceiptNo,2) in ('JC')";

            //utk Aset Komponen
            if (strTipeReceipt == "CE")
                tipeReceipt = "and left(ReceiptNo,2) in ('CE')";
            if (strTipeReceipt == "KE")
                tipeReceipt = "and left(ReceiptNo,2) in ('KE')";
            if (strTipeReceipt == "JE")
                tipeReceipt = "and left(ReceiptNo,2) in ('JE')";

            string strSQL = "select ID,ReceiptNo,ReceiptDate,ReceiptType,PONo,POID,SupplierId,Status,CreatedTime,CreatedBy,LastModifiedBy,"+
                            "LastModifiedTime,ItemTypeID,DepoID from Receipt where Status>-1 and ReceiptNo='" + strReceiptNo + 
                            "' " + tipeReceipt;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrReceipt = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Receipt();
        }
        public ArrayList RetrieveBySamaTgl2(int tgl, int bln, int thn, int itemTypeID, int groupID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "select distinct A.ID,A.ReceiptNo,A.ReceiptDate,A.POID,A.PONo,A.SupplierId,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.Status," +
                "A.ReceiptType,A.ItemTypeID,A.DepoID from Receipt as A,ReceiptDetail as B where A.ID=B.ReceiptID and B.RowStatus>-1 and A.Status>-1 and day(A.ReceiptDate)=" + tgl + " and " +
                "month(A.ReceiptDate) =" + bln + " and year(A.ReceiptDate) =" + thn + " and B.ItemTypeID =" + itemTypeID + " and B.GroupID=" + groupID;
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrReceipt = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceipt.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrReceipt.Add(new Receipt());

            return arrReceipt;
        }
        public ArrayList RetrieveByKurangTgl2(int tgl, int bln, int thn, int itemTypeID, int groupID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select distinct A.ID,A.ReceiptNo,A.ReceiptDate,A.POID,A.PONo,A.SupplierId,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.Status," +
                "A.ReceiptType,A.ItemTypeID,A.DepoID from Receipt as A,ReceiptDetail as B where A.ID=B.ReceiptID and B.RowStatus>-1 and A.Status>-1 and day(A.ReceiptDate)<" + tgl + " and " +
                "month(A.ReceiptDate) =" + bln + " and year(A.ReceiptDate) =" + thn + " and B.ItemTypeID =" + itemTypeID + " and B.GroupID=" + groupID);
            strError = dataAccess.Error;
            arrReceipt = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceipt.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrReceipt.Add(new Receipt());

            return arrReceipt;
        }
        public Receipt GenerateObject(SqlDataReader sqlDataReader)
        {
            objReceipt = new Receipt();
            objReceipt.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objReceipt.ReceiptNo = sqlDataReader["ReceiptNo"].ToString();
            objReceipt.ReceiptDate = Convert.ToDateTime(sqlDataReader["ReceiptDate"]);
            objReceipt.ReceiptType = Convert.ToInt32(sqlDataReader["ReceiptType"]);
            objReceipt.SupplierID = Convert.ToInt32(sqlDataReader["SupplierID"]);
            objReceipt.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objReceipt.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objReceipt.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objReceipt.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objReceipt.LastModifiedTime =(sqlDataReader["LastModifiedTime"]!=DBNull.Value)? Convert.ToDateTime(sqlDataReader["LastModifiedTime"]):DateTime.MinValue;
            objReceipt.PoID = Convert.ToInt32(sqlDataReader["PoID"]);
            objReceipt.DepoID = Convert.ToInt32(sqlDataReader["DepoID"]);
            objReceipt.PoNo = sqlDataReader["PoNo"].ToString();
            objReceipt.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"]);
            return objReceipt;
        }
        public Receipt GenerateObjectLastEntry(SqlDataReader sqlDataReader)
        {
            objReceipt = new Receipt();
            objReceipt.ReceiptDate = Convert.ToDateTime(sqlDataReader["ReceiptDate"]);
            return objReceipt;
        }
        public Receipt GenerateObjectList(SqlDataReader sqlDataReader)
        {
            objReceipt = new Receipt();
            objReceipt.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objReceipt.ReceiptNo = sqlDataReader["ReceiptNo"].ToString();
            objReceipt.ReceiptDate = Convert.ToDateTime(sqlDataReader["ReceiptDate"]);
            objReceipt.SupplierID = Convert.ToInt32(sqlDataReader["SupplierID"]);
            objReceipt.SupplierCode = sqlDataReader["SupplierCode"].ToString();
            objReceipt.SupplierName = sqlDataReader["SupplierName"].ToString();
            objReceipt.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objReceipt.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objReceipt.PoID = Convert.ToInt32(sqlDataReader["PoID"]);
            objReceipt.PoNo = sqlDataReader["PoNo"].ToString();
            objReceipt.SppNo = sqlDataReader["SppNo"].ToString();
            objReceipt.ItemCode = sqlDataReader["ItemCode"].ToString();
            objReceipt.ItemName = sqlDataReader["ItemName"].ToString();
            objReceipt.Quantity = Convert.ToDecimal(sqlDataReader["Quantity"]);
            objReceipt.QtyPO = Convert.ToDecimal(sqlDataReader["QtyPO"]);

            return objReceipt;
        }
        public Receipt GenerateObjectListbyapprove(SqlDataReader sqlDataReader)
        {
            objReceipt = new Receipt();
            objReceipt.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objReceipt.ReceiptNo = sqlDataReader["ReceiptNo"].ToString();
            objReceipt.ReceiptDate = Convert.ToDateTime(sqlDataReader["ReceiptDate"]);
            objReceipt.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"]);
            objReceipt.SupplierID = Convert.ToInt32(sqlDataReader["SupplierID"]);
            objReceipt.SupplierCode = sqlDataReader["SupplierCode"].ToString();
            objReceipt.SupplierName = sqlDataReader["SupplierName"].ToString();
            objReceipt.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objReceipt.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objReceipt.PoID = Convert.ToInt32(sqlDataReader["PoID"]);
            objReceipt.PoNo = sqlDataReader["PoNo"].ToString();
            //objReceipt.SppNo = sqlDataReader["SppNo"].ToString();
            //objReceipt.ItemCode = sqlDataReader["ItemCode"].ToString();
            //objReceipt.ItemName = sqlDataReader["ItemName"].ToString();
            //objReceipt.Quantity = Convert.ToDecimal(sqlDataReader["Quantity"]);
            //objReceipt.QtyPO = Convert.ToDecimal(sqlDataReader["QtyPO"]);

            return objReceipt;
        }
        public Receipt GenerateObjectByTagihan(SqlDataReader sqlDataReader)
        {
            try
            {
                objReceipt = new Receipt();
                objReceipt.ID = Convert.ToInt32(sqlDataReader["ID"]);
                objReceipt.ReceiptNo = sqlDataReader["ReceiptNo"].ToString();
                objReceipt.ReceiptDate = Convert.ToDateTime(sqlDataReader["ReceiptDate"]);
                objReceipt.SupplierName = sqlDataReader["Supplier"].ToString();
                objReceipt.NPWP = sqlDataReader["npwp"].ToString();
                objReceipt.Currency = sqlDataReader["currency"].ToString();
                objReceipt.Total = Convert.ToDecimal(sqlDataReader["total"]);
                objReceipt.PPN = Convert.ToDecimal(sqlDataReader["ppn"]);
                objReceipt.Tagihan = Convert.ToDecimal(sqlDataReader["tagihan"]);
                objReceipt.JTempoDate = Convert.ToDateTime(sqlDataReader["tglJatuhTempo"]);
                objReceipt.PoID = Convert.ToInt32(sqlDataReader["PoID"]);
            }
            catch
            { }
            return objReceipt;
        }
        /**
         * for item biaya new
         */
        public string ItemNameFromBiaya(string TableName)
        {
            string strSQL = "CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())<= " +
                " (Select CreatedTime from SPP where SPP.ID=" + TableName + ".SPPID)) " +
                " THEN(select ItemName from Biaya where Biaya.ID=" + TableName + ".JenisBiaya and Biaya.RowStatus>-1)+' - '+ " +
                " (Select SPPDetail.Keterangan From SPPDetail where SPPDetail.ID=(SELECT SPPDetailID From POPurchnDetail where ID=" + TableName + ".PODetailID) and " +
                " SPPDetail.SPPID=" + TableName + ".SPPID) ELSE " +
                " (select ItemName from biaya where ID=" + TableName + ".ItemID and biaya.RowStatus>-1) END ";
            return strSQL;
        }
        public string ItemCodeForBiaya(string TableName)
        {
            return "CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())<= " +
                " (Select CreatedTime from SPP where SPP.ID=" + TableName + ".SPPID)) " +
                " THEN(select ItemCode from Biaya where Biaya.ID=" + TableName + ".JenisBiaya and Biaya.RowStatus>-1) ELSE " +
                " (select ItemCode from biaya where ID=" + TableName + ".ItemID and biaya.RowStatus>-1) END ";

        }
        /**
         * Parsial Delivery Schedule
         */
        public ArrayList GetSchedule(int POID,int ItemID)
        {
            string TglMundurSCH = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("TglMundurSch", "Receipt");
            string TglHariINI = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Today", "Receipt");
            ArrayList arrData = new ArrayList();
            string strSql = "Select * from MemoHarian_Armada where SchID in(Select ID from MemoHarian_PO where POID=" + POID + 
                            " and ItemID=" + ItemID+") and flag=0 and rowstatus>-1 ";
            strSql = "SET DATEFIRST 1;SELECT * FROM ( " +
                   " SELECT ma.ID,ma.DoNum,mp.CreatedTime,mp.POID,mp.ItemID,ma.SchDate,CONVERT(CHAR,ma.SchDate,112)Sch," +
                   " /*CASE WHEN ma.NoPol='FRANCO' THEN (Select CONVERT(CHAR,DvlDate,112) From MemoHarian m where m.ID=mp.SchID)" +
                   " ELSE ma.NoPol END*/ NoPol " +
                   " FROM MemoHarian_Armada ma " +
                   " LEFT JOIN MemoHarian_PO mp on mp.ID=ma.SchPOID " +
                   " LEFT JOIN POPurchn p on p.ID=mp.POID " +
                   " WHERE ma.rowstatus>-1 and ma.Flag=0 /*AND POID=" + POID + "*/ and ItemID=" + ItemID +
                   " AND SchDate Between DATEADD(DAY,(-1* ((Select dbo.GetOFFDay(GETDATE()-" + TglMundurSCH + ",GETDATE()))+2)),GETDATE()) AND GETDATE()-" + TglHariINI +
                   " )AS X " +
                   " WHERE  Year(CreatedTime)>2016 " +
                   " ORDER BY ID,CreatedTime";
            DataAccess dta = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = dta.RetrieveDataByString(strSql);
            if (dta.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new Receipt
                    {
                        InvoiceNo = sdr["DoNum"].ToString() + " - " + sdr["NoPol"].ToString() + " - " + sdr["Sch"].ToString(),
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        NPWP = sdr["NoPol"].ToString(),
                        ReceiptDate=DateTime.Parse(sdr["SchDate"].ToString())
                    });
                }
            }
            
            return arrData;
        }

        private ArrayList GetSchedule(int POID, int ItemID, bool p)
        {
            ArrayList arrData = new ArrayList();
            string strSql = "Select *,(Select CONVERT(CHAR,DvlDate,112) From MemoHarian m where m.ID=SchID)SchDate from MemoHarian_PO where POID=" + POID +
                            " and ItemID=" + ItemID + " and RowStatus>-1 and ID not in(Select SchPOID from memoharian_armada where RowStatus>-1) " +
                            "and  EstQty > RowStatus and RowStatus>-1 and RowStatus>-1 and DocNo is nOt Null ORDER BY ID";
            DataAccess dta = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = dta.RetrieveDataByString(strSql);
            if (dta.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new Receipt
                    {
                        InvoiceNo = sdr["DocNo"].ToString() + sdr["ID"].ToString() + "-" + sdr["SchDate"].ToString(),
                        ID = Convert.ToInt32(sdr["ID"].ToString())
                    });
                }
            }
            return arrData;
        }
        /**
         * Agen Lapak
         * Update SJ Number ke table receipt
         */

        public int UpdateSJNumber(int ReceiptID, string SJNo)
        {
            int result = 0;
            string strsql = "Update Receipt set SJSupplier='" + SJNo + "' where ID=" + ReceiptID.ToString();
            DataAccess da= new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strsql);
            if (da.Error == string.Empty)
            {
                result = sdr.RecordsAffected;
            }
            
            return result;
        }
        public int UpdateStatusReprint(string ReceiptID)
        {
            int result = 0;
            string strSQL = "Update Receipt set cetak=0 where ID=" + ReceiptID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr=da.RetrieveDataByString(strSQL);
            if(sdr.RecordsAffected>0)
            {
                //catat ke eventlog
                EventLog evn = new EventLog();
                evn.EventName = "Reprint receipt";
                evn.ModulName = "Receipt";
                evn.DocumentNo = ReceiptID.ToString();
                evn.CreatedBy = ((Users)System.Web.HttpContext.Current.Session["Users"]).UserID.ToString();
                EventLogFacade ef = new EventLogFacade();
                 result = ef.Insert(evn);
            }
            return result;
        }

        public ArrayList RetrieveTahun()
        {
            arrReceipt = new ArrayList();
            string strSQL = "SELECT DISTINCT YEAR(ReceiptDate) Tahun FROM Receipt WHERE YEAR(ReceiptDate)>2005 ORDER BY YEAR(ReceiptDate) Desc";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrReceipt.Add(GenerateObject(sdr, true));
                }
            }
            return arrReceipt;
        }

        private BeritaAcara GenerateObject(SqlDataReader sdr, bool p)
        {
            BeritaAcara rd = new BeritaAcara();
            rd.Tahun = int.Parse(sdr["Tahun"].ToString());
            return rd;
        }
        public int Bulan { get; set; }
        public int Tahun { get; set; }
        public string FieldSaldoAwal { get; set; }
        public string YearPeriode { get; set; }
        public ArrayList RetrieveRekapAssetNew()
        {
            arrReceipt = new ArrayList();
            string strSQL = "EXEC dbo.RekapAssetNew " + this.Bulan + "," + this.Tahun + ",'" + this.FieldSaldoAwal + "','" + this.YearPeriode + "'";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrReceipt.Add(GenerateObjectAsset(sdr));
                }
            }
            return arrReceipt;
        }

        public ArrayList RetrieveRekapAssetNew_Dept(string A, string UnitKerja)
        {
            string query = string.Empty; string query2 = string.Empty; string query3 = string.Empty;
            if (UnitKerja.Length == 1)
            {
                query2 = "9"; query3 = "13";
            }
            else
            {
                query2 = "10"; query3 = "14";
            }

            if (A != "0")
            {
                if (A == "1")
                {
                    query =
                    " select *  from temp1  where SUBSTRING(ItemCode," + query2 + ",1)='" + A + "' and LEN(ItemCode)='" + query3 + "' " +
                    " union all " +
                    " select *  from temp1  where SUBSTRING(ItemCode,4,1)='B' and LEN(ItemCode)='11' ";
                }
                else if (A == "2")
                {
                    query =
                        " select *  from temp1  where SUBSTRING(ItemCode," + query2 + ",1)='" + A + "' and LEN(ItemCode)='" + query3 + "' " +
                        " union all " +
                        " select *  from temp1  where SUBSTRING(ItemCode,4,1)='F' and LEN(ItemCode)='11' ";
                }
                else if (A == "3")
                {
                    query =
                        " select *  from temp1  where SUBSTRING(ItemCode," + query2 + ",1)='" + A + "' and LEN(ItemCode)='" + query3 + "' " +
                        " union all " +
                        " select *  from temp1  where SUBSTRING(ItemCode,4,1)='M' and LEN(ItemCode)='11' ";
                }
                else if (A == "4")
                {
                    query =
                        " select *  from temp1  where SUBSTRING(ItemCode," + query2 + ",1)='" + A + "' and LEN(ItemCode)='" + query3 + "' " +
                        " union all " +
                        " select *  from temp1  where SUBSTRING(ItemCode,4,1)='L' and LEN(ItemCode)='11' ";
                }
                else if (A == "5")
                {
                    query =
                        " select *  from temp1  where SUBSTRING(ItemCode," + query2 + ",1)='" + A + "' and LEN(ItemCode)='" + query3 + "' " +
                        " union all " +
                        " select *  from temp1  where SUBSTRING(ItemCode,4,1)='G' and LEN(ItemCode)='11' ";
                }
                else if (A == "6")
                {
                    query =
                        " select *  from temp1  where SUBSTRING(ItemCode," + query2 + ",1)='" + A + "' and LEN(ItemCode)='" + query3 + "' " +
                        " union all " +
                        " select *  from temp1  where SUBSTRING(ItemCode,4,1)='Q' and LEN(ItemCode)='11' ";
                }
                else if (A == "7")
                {
                    query =
                        " select *  from temp1  where SUBSTRING(ItemCode," + query2 + ",1)='" + A + "' and LEN(ItemCode)='" + query3 + "' " +
                        " union all " +
                        " select *  from temp1  where SUBSTRING(ItemCode,4,1)='E' and LEN(ItemCode)='11' ";
                }
                else if (A == "8")
                {
                    query =
                        " select *  from temp1  where SUBSTRING(ItemCode," + query2 + ",1)='" + A + "' and LEN(ItemCode)='" + query3 + "' ";
                    //" union all " +
                    //" select *  from temp1  where SUBSTRING(ItemCode,4,1)='E' and LEN(ItemCode)='11' ";
                }
                else if (A == "9")
                {
                    query =
                        " select *  from temp1  where SUBSTRING(ItemCode," + query2 + ",1)='" + A + "' and LEN(ItemCode)='" + query3 + "' ";
                    //" union all " +
                    //" select *  from temp1  where SUBSTRING(ItemCode,4,1)='E' and LEN(ItemCode)='11' ";
                }
            }
            else
            { query = " select * from temp1 "; }

            arrReceipt = new ArrayList();
            string strSQL =
                //"EXEC dbo.RekapAssetNew " + this.Bulan + "," + this.Tahun + ",'" + this.FieldSaldoAwal + "','" + this.YearPeriode + "'";
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp1]') AND type in (N'U')) DROP TABLE [dbo].[temp1] " +

            " declare @temp table " +
            " ( " +
            " ItemCode varchar(255), " +
            " ItemName varchar(MAX), " +
            " Unit varchar(255), " +
            " SaldoAwal decimal, " +
            " Pembelian decimal, " +
            " AdjustIN decimal, " +
            " AdjustOut decimal, " +
            " SaldoAkhir decimal, " +
            " SPB decimal, " +
            " StockGudang decimal, " +
            " Kategori varchar(100) " +
            " ); " +
            " INSERT @temp  Exec dbo.RekapAssetNew ; " +
            " select * into temp1 from @temp  " +
            " " + query + " ";

            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrReceipt.Add(GenerateObjectAsset(sdr));
                }
            }
            return arrReceipt;
        }


        private RekapAsset GenerateObjectAsset(SqlDataReader sdr)
        {
            RekapAsset rk = new RekapAsset();
            rk.ItemCode = sdr["ItemCode"].ToString();
            rk.ItemName = sdr["ItemName"].ToString();
            rk.Unit = sdr["Unit"].ToString();
            rk.SaldoAwal = decimal.Parse(sdr["SaldoAwal"].ToString());
            rk.SaldoAkhir = decimal.Parse(sdr["SaldoAkhir"].ToString());
            rk.Pembelian = decimal.Parse(sdr["Pembelian"].ToString());
            rk.AdjustIN = decimal.Parse(sdr["AdjustIN"].ToString());
            rk.AdjustOut = decimal.Parse(sdr["AdjustOut"].ToString());
            rk.SPB = decimal.Parse(sdr["SPB"].ToString());
            rk.StockGudang = decimal.Parse(sdr["StockGudang"].ToString());
            rk.Kategori = sdr["Kategori"].ToString();
            return rk;
        }

        /** Beny **/
        public Receipt CekItemFlooculant(int itemID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL =
            "select ItemID from BM_PFloculantItem where RowStatus>-1 and NamaBarang in (select ItemName from Inventory where ID=" + itemID + ") ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectFloo(sqlDataReader);
                }
            }

            return new Receipt();
        }
        /** end Beny **/

        public Receipt GenerateObjectFloo(SqlDataReader sqlDataReader)
        {
            objReceipt = new Receipt();
            objReceipt.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            return objReceipt;
        }
    }

}
