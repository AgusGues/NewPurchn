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
    public class SuratJalanDetailFacade : AbstractTransactionFacade
    {
        private SuratJalanDetail objSuratJalanDetail = new SuratJalanDetail();
        private ArrayList arrSuratJalanDetail;
        private List<SqlParameter> sqlListParam;
        private string scheduleNo = string.Empty;

        public SuratJalanDetailFacade(object objDomain)
            : base(objDomain)
        {
            objSuratJalanDetail = (SuratJalanDetail)objDomain;
        }

        public SuratJalanDetailFacade()
        {

        }

        public SuratJalanDetailFacade(object objDomain, string strScheduleNo)
        {
            objSuratJalanDetail = (SuratJalanDetail)objDomain;
            scheduleNo = strScheduleNo;
        }


        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();                
                sqlListParam.Add(new SqlParameter("@SuratJalanID", objSuratJalanDetail.SuratJalanId));
                sqlListParam.Add(new SqlParameter("@ItemID", objSuratJalanDetail.ItemID));
                sqlListParam.Add(new SqlParameter("@Qty", objSuratJalanDetail.Qty));
                sqlListParam.Add(new SqlParameter("@ScheduleDetailID", objSuratJalanDetail.ScheduleDetailId));
                sqlListParam.Add(new SqlParameter("@Paket", objSuratJalanDetail.Paket));                
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertSuratJalanDetail");

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
                objSuratJalanDetail = (SuratJalanDetail)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ScheduleDetailID", objSuratJalanDetail.ScheduleDetailId));
                sqlListParam.Add(new SqlParameter("@Qty",objSuratJalanDetail.Qty));
                sqlListParam.Add(new SqlParameter("@Flag", objSuratJalanDetail.Flag));
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateSuratJalanDetail");

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
                objSuratJalanDetail = (SuratJalanDetail)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@SuratJalanID", objSuratJalanDetail.SuratJalanId));

                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteSuratJalanDetail");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanID,A.ItemID,B.ItemCode,B.Description as ItemName,A.Qty,A.ScheduleDetailID,A.Paket from SuratJalanDetail as A,Items as B where A.ItemID = B.ID");
            strError = dataAccess.Error;
            arrSuratJalanDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalanDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalanDetail.Add(new SuratJalanDetail());

            return arrSuratJalanDetail;
        }

        public ArrayList RetrieveById_bak(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanID,A.ItemID,B.ItemCode,B.Description as ItemName,A.Qty,A.ScheduleDetailID,A.Paket from SuratJalanDetail as A,Items as B where A.ItemID = B.ID and A.SuratJalanID = " + Id);
            strError = dataAccess.Error;
            arrSuratJalanDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalanDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalanDetail.Add(new SuratJalanDetail());

            return arrSuratJalanDetail;
        }
        public ArrayList RetrieveByInvNo(string invNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,C.SuratJalanNo,A.SuratJalanID,A.ItemID,B.ItemCode,B.GroupID,B.Description as ItemName,A.Qty,A.ScheduleDetailID,A.Paket " +
            "from SuratJalanDetail as A,Items as B, SuratJalan as C, InvoiceDetail as D, Invoice as E where C.ID=A.SuratJalanID and C.Status>-1 and C.ID=D.SuratJalanID and D.InvoiceID=E.ID and E.Status>-1 and "+
            "A.ItemID = B.ID and InvoiceNo='" + invNo + "' order by suratjalanno,Description");
            strError = dataAccess.Error;
            arrSuratJalanDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalanDetail.Add(GenerateObjectA(sqlDataReader));
                }
            }
            else
                arrSuratJalanDetail.Add(new SuratJalanDetail());

            return arrSuratJalanDetail;
        }
        public ArrayList RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanID,A.ItemID,B.ItemCode,B.Description as ItemName,A.Qty,A.ScheduleDetailID,A.Paket from SuratJalanDetail as A,Items as B where A.ItemID = B.ID and A.SuratJalanID = " + Id);
            //tambahan utk PINKBOARD
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanID,A.ItemID,B.ItemCode,B.GroupID,B.Description as ItemName,A.Qty,A.ScheduleDetailID,A.Paket from SuratJalanDetail as A,Items as B where A.ItemID = B.ID and A.SuratJalanID = " + Id);
            strError = dataAccess.Error;
            arrSuratJalanDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalanDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalanDetail.Add(new SuratJalanDetail());

            return arrSuratJalanDetail;
        }
        public ArrayList RetrieveByIdForPromoHappy(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanID,A.ItemID,B.ItemCode,B.Description as ItemName,A.Qty,A.ScheduleDetailID,A.Paket from SuratJalanDetail as A,Items as B where A.ItemID = B.ID and A.SuratJalanID = " + Id);
            //tambahan utk PINKBOARD
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanID,A.ItemID,B.ItemCode,B.GroupID,B.Description as ItemName,A.Qty,A.ScheduleDetailID,A.Paket from SuratJalanDetail as A,Items as B where A.ItemID = B.ID and A.ItemID not in(2363,2364,3258,4242) and A.SuratJalanID = " + Id);
            strError = dataAccess.Error;
            arrSuratJalanDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalanDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalanDetail.Add(new SuratJalanDetail());

            return arrSuratJalanDetail;
        }
        public ArrayList RetrieveByIdForPrice(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanID,A.ItemID,B.ItemCode,B.Description as ItemName,A.Qty,A.ScheduleDetailID,A.Paket from SuratJalanDetail as A,Items as B where A.ItemID = B.ID and A.SuratJalanID = " + Id);
            //tambahan utk PINKBOARD
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanID,A.ItemID,B.ItemCode,B.GroupID,B.Description as ItemName,A.Qty,A.ScheduleDetailID,A.Paket, "+
                "(select Price from OPDetail as E,ScheduleDetail as F where E.ID=F.DocumentDetailID and F.Status>-1 and F.ID=A.ScheduleDetailID and E.ItemID=A.ItemID) as Price, (select UOMCode from UOM where UOM.ID=B.UOMID) as Uom " +
                "from SuratJalanDetail as A,Items as B where A.ItemID = B.ID and A.ItemID not in(2363,2364,3258,4242) and B.Pajak=0 and B.Description not like '%kaos%' and A.SuratJalanID = " + Id);
            strError = dataAccess.Error;
            arrSuratJalanDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalanDetail.Add(GenerateObject4(sqlDataReader));
                }
            }
            else
                arrSuratJalanDetail.Add(new SuratJalanDetail());

            return arrSuratJalanDetail;
        }
        public ArrayList RetrieveByIdForPriceRetail(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanID,A.ItemID,B.ItemCode,B.Description as ItemName,A.Qty,A.ScheduleDetailID,A.Paket from SuratJalanDetail as A,Items as B where A.ItemID = B.ID and A.SuratJalanID = " + Id);
            //tambahan utk PINKBOARD

            //ada potongan harga jg utk 57 & 58


            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanID,A.ItemID,B.ItemCode,B.GroupID,B.Description as ItemName,A.Qty,A.ScheduleDetailID,A.Paket, " +
                "(select HargaRetail from OPDetail as E,ScheduleDetail as F where E.ID=F.DocumentDetailID and F.Status>-1 and F.ID=A.ScheduleDetailID and E.ItemID=A.ItemID) as Price, (select UOMCode from UOM where UOM.ID=B.UOMID) as Uom " +
                "from SuratJalanDetail as A,Items as B where A.ItemID = B.ID and A.ItemID not in(2363,2364,4242) and B.Pajak=0 and B.Description not like '%kaos%' and B.Description not like '%amplop%' and A.SuratJalanID = " + Id);
            strError = dataAccess.Error;
            arrSuratJalanDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalanDetail.Add(GenerateObject4(sqlDataReader));
                }
            }
            else
                arrSuratJalanDetail.Add(new SuratJalanDetail());

            return arrSuratJalanDetail;
        }
        public SuratJalanDetail RetrieveById2(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanID,A.ItemID,B.ItemCode,B.Description as ItemName,A.Qty,A.ScheduleDetailID,A.Paket from SuratJalanDetail as A,Items as B where A.ItemID = B.ID and A.ID = " + Id);
            strError = dataAccess.Error;


            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return (GenerateObject(sqlDataReader));
                }
            }

            return new SuratJalanDetail();
        }

        public ArrayList RetrieveByOPId(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanID,A.ItemID,B.ItemCode,B.Description as ItemName,A.Qty,A.ScheduleDetailID,A.Paket,B.GroupID from SuratJalanDetail as A,Items as B,SuratJalan as C where A.ItemID = B.ID and A.SuratJalanID = C.ID and C.Status = 3 and C.OPID = " + Id);
            strError = dataAccess.Error;
            arrSuratJalanDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalanDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalanDetail.Add(new SuratJalanDetail());

            return arrSuratJalanDetail;
        }


        public ArrayList RetrieveByOPId2(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanID,A.ItemID,B.ItemCode,B.Description as ItemName,A.Qty,A.ScheduleDetailID,A.Paket from SuratJalanDetail as A,Items as B,SuratJalan as C where A.ItemID = B.ID and A.SuratJalanID = C.ID and C.Status = 2 and C.OPID = " + Id);
            strError = dataAccess.Error;
            arrSuratJalanDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalanDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalanDetail.Add(new SuratJalanDetail());

            return arrSuratJalanDetail;
        }

        public ArrayList RetrieveBySuratJalanDetailId(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.SuratJalanID,A.ItemID,B.ItemCode,B.GroupID,B.Description as ItemName,A.Qty,A.ScheduleDetailID,A.Paket from SuratJalanDetail as A,Items as B where A.ItemID = B.ID and A.ID = " + Id);
            strError = dataAccess.Error;
            arrSuratJalanDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalanDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalanDetail.Add(new SuratJalanDetail());

            return arrSuratJalanDetail;
        }

        public ArrayList RetrieveSuratJalanDetailBySJ(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select SuratJalanDetail.ID,SuratJalanDetail.ItemID,Items.Description as ItemName,UOM.UOMCode,SuratJalanDetail.Qty,Round(OPDetail.Price / 1.1,-1) as Price,case OP.BarangPromosi when 0 then (select Pajak from items where id = SuratJalanDetail.ItemID) when 1 then (select BarangPromosi from OP where ID = opdetail.OPID) end Pajak,ScheduleDetail.ID as ScheduleDetailId from SuratJalanDetail,OPDetail,SuratJalan,OP,Items,UOM,ScheduleDetail where SuratJalan.ID = SuratJalanDetail.SuratJalanID and SuratJalan.OPID = OP.ID and OP.ID = OPDetail.OPID and SuratJalanDetail.ItemID = Items.ID and Items.UOMID = UOM.ID and SuratJalanDetail.ScheduleDetailID = ScheduleDetail.ID and ScheduleDetail.DocumentDetailID = OPDetail.ID and SuratJalan.ID = " + Id);
            strError = dataAccess.Error;
            arrSuratJalanDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalanDetail.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrSuratJalanDetail.Add(new SuratJalanDetail());

            return arrSuratJalanDetail;
        }
        public ArrayList RetrieveSuratJalanDetailBySJforHargaTokoRetail(int Id)
        {
            //string strQuery = "select distinct SuratJalanDetail.ID,SuratJalanDetail.ItemID,Items.Description as ItemName,UOM.UOMCode,SuratJalanDetail.Qty, " +
            //   "Round( ISNULL(CGI_SuratJalanDetail.hargatoko,0) / 1.1,-1) as price  " +
            //   ",case OP.BarangPromosi when 0 then (select Pajak from items where id = SuratJalanDetail.ItemID) when 1 then (select BarangPromosi from OP where ID = opdetail.OPID) end Pajak,ScheduleDetail.ID as ScheduleDetailId  " +
            //   "from SuratJalanDetail,OPDetail,SuratJalan,OP,Items,UOM,ScheduleDetail,CGI_SuratJalanDetail where SuratJalan.ID = SuratJalanDetail.SuratJalanID and SuratJalan.OPID = OP.ID and OP.ID = OPDetail.OPID and SuratJalanDetail.ItemID = Items.ID and Items.UOMID = UOM.ID and SuratJalanDetail.ScheduleDetailID = ScheduleDetail.ID and " +
            //   "ScheduleDetail.DocumentDetailID = OPDetail.ID and SuratJalan.ID=CGI_SuratJalanDetail.SuratJalanID and SuratJalanDetail.ItemID=CGI_SuratJalanDetail.ItemID and SuratJalan.ID = " + Id;
            string strQuery = "select distinct SuratJalanDetail.ID,SuratJalanDetail.ItemID,Items.Description as ItemName,UOM.UOMCode,SuratJalanDetail.Qty, " +
               "Round( ISNULL(CGI_SuratJalanDetail.hargatoko,0) / 1.1,-1) as price  " +
               ",case OP.BarangPromosi when 0 then (select Pajak from items where id = SuratJalanDetail.ItemID) when 1 then (select BarangPromosi from OP where ID = opdetail.OPID) end Pajak,ScheduleDetail.ID as ScheduleDetailId  " +
               "from SuratJalanDetail,OPDetail,SuratJalan,OP,Items,UOM,ScheduleDetail,CGI_SuratJalanDetail,Groups where SuratJalan.ID = SuratJalanDetail.SuratJalanID and SuratJalan.OPID = OP.ID and OP.ID = OPDetail.OPID and SuratJalanDetail.ItemID = Items.ID and Items.UOMID = UOM.ID and SuratJalanDetail.ScheduleDetailID = ScheduleDetail.ID and " +
               "ScheduleDetail.DocumentDetailID = OPDetail.ID and SuratJalan.ID=CGI_SuratJalanDetail.SuratJalanID and SuratJalanDetail.ItemID=CGI_SuratJalanDetail.ItemID and Items.GroupID=Groups.ID AND (Groups.ItemTypeID IN (1, 2)  or (Groups.ItemTypeID=3 and Items.ID in (179)) or (Groups.ItemTypeID=6)) and SuratJalan.ID = " + Id;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strQuery);
            strError = dataAccess.Error;
            arrSuratJalanDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalanDetail.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrSuratJalanDetail.Add(new SuratJalanDetail());

            return arrSuratJalanDetail;
        }
        public ArrayList RetrieveSuratJalanDetailBySJforHargaToko(int Id)
        {
            string strQuery = "select distinct SuratJalanDetail.ID,SuratJalanDetail.ItemID,Items.Description as ItemName,UOM.UOMCode,SuratJalanDetail.Qty, " +
               "Round( " +
                //"case when Items.Paket=1 then CGI_SuratJalanDetail.hargatoko/ OPDetail.Quantity "+
               "case when Items.Paket=1 then ISNULL((CGI_SuratJalanDetail.hargatoko*(OPDetail.Quantity/(select Quantity from PaketItemDetail where PaketItemID=OPDetail.ItemID and ItemID=(case when ItemID=OPDetail.ItemID then OPDetail.ItemID else (select ID from Items where PaketItemIDAcuan LIKE '%'+CONVERT(VARCHAR(19),OPDetail.ItemID)+'%') end ))) ),0)/OPDetail.Quantity " +
               "else ISNULL(CGI_SuratJalanDetail.hargatoko,0) end " +
               "/ 1.1,-1) as price " +
               ",case OP.BarangPromosi when 0 then (select Pajak from items where id = SuratJalanDetail.ItemID) when 1 then (select BarangPromosi from OP where ID = opdetail.OPID) end Pajak,ScheduleDetail.ID as ScheduleDetailId  " +
               "from SuratJalanDetail,OPDetail,SuratJalan,OP,Items,UOM,ScheduleDetail,CGI_SuratJalanDetail where SuratJalan.ID = SuratJalanDetail.SuratJalanID and SuratJalan.OPID = OP.ID and OP.ID = OPDetail.OPID and SuratJalanDetail.ItemID = Items.ID and Items.UOMID = UOM.ID and SuratJalanDetail.ScheduleDetailID = ScheduleDetail.ID and " +
               "ScheduleDetail.DocumentDetailID = OPDetail.ID and SuratJalan.ID=CGI_SuratJalanDetail.SuratJalanID and SuratJalanDetail.ItemID=CGI_SuratJalanDetail.ItemID and Items.ItemPromosi=0 and SuratJalan.ID = " + Id;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strQuery);
            strError = dataAccess.Error;
            arrSuratJalanDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalanDetail.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrSuratJalanDetail.Add(new SuratJalanDetail());

            return arrSuratJalanDetail;
        }
        public ArrayList RetrieveSuratJalanDetailBySJforHargaToko2(string taxno)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Pajak,ItemID,ItemName,UOMCode,price,SUM(Qty) as Qty from (select SuratJalanDetail.ID,SuratJalanDetail.ItemID,Items.Description as ItemName,UOM.UOMCode,SuratJalanDetail.Qty, "+
"Round( case when Items.Paket=1 then ISNULL((CGI_SuratJalanDetail.hargatoko*(OPDetail.Quantity/(select Quantity from PaketItemDetail where PaketItemID=OPDetail.ItemID and ItemID=(case when ItemID=OPDetail.ItemID then OPDetail.ItemID else (select ID from Items where PaketItemIDAcuan LIKE '%'+CONVERT(VARCHAR(19),OPDetail.ItemID)+'%') end ))) ),0)/OPDetail.Quantity "+
"else ISNULL(CGI_SuratJalanDetail.hargatoko,0) end / 1.1,-1) as price,case OP.BarangPromosi when 0 then (select Pajak from items where id = SuratJalanDetail.ItemID) when 1 then (select BarangPromosi from OP where ID = opdetail.OPID) end Pajak "+
"from SuratJalanDetail,OPDetail,SuratJalan,OP,Items,UOM,ScheduleDetail,CGI_SuratJalanDetail, CGI_Invoice, CGI_InvoiceDetail where SuratJalan.ID = SuratJalanDetail.SuratJalanID and SuratJalan.OPID = OP.ID and OP.ID = OPDetail.OPID and SuratJalanDetail.ItemID = Items.ID and Items.UOMID = UOM.ID and SuratJalanDetail.ScheduleDetailID = ScheduleDetail.ID and "+
"ScheduleDetail.DocumentDetailID = OPDetail.ID and SuratJalan.ID=CGI_SuratJalanDetail.SuratJalanID and SuratJalanDetail.ItemID=CGI_SuratJalanDetail.ItemID and "+
"Items.ItemPromosi=0 and SuratJalan.ID=CGI_InvoiceDetail.SuratJalanID and CGI_InvoiceDetail.InvoiceID=CGI_Invoice.ID and CGI_Invoice.Status>-1 and CGI_Invoice.TaxNo='"+taxno+"' ) as AA group by Pajak,ItemID,ItemName,UOMCode,price order by ItemName");
            strError = dataAccess.Error;
            arrSuratJalanDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalanDetail.Add(GenerateObject3(sqlDataReader));
                }
            }
            else
                arrSuratJalanDetail.Add(new SuratJalanDetail());

            return arrSuratJalanDetail;
        }

        public ArrayList RetrieveSuratJalanDetailBySJlama(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select SuratJalanDetail.ID,SuratJalanDetail.ItemID,Items.Description as ItemName,UOM.UOMCode,SuratJalanDetail.Qty,Round(OPDetail.Price / 1.1,0,1) as Price,Items.Pajak from SuratJalanDetail,OPDetail,SuratJalan,OP,Items,UOM,ScheduleDetail where SuratJalan.ID = SuratJalanDetail.SuratJalanID and SuratJalan.OPID = OP.ID and OP.ID = OPDetail.OPID and SuratJalanDetail.ItemID = Items.ID and Items.UOMID = UOM.ID and SuratJalanDetail.ScheduleDetailID = ScheduleDetail.ID and ScheduleDetail.DocumentDetailID = OPDetail.ID and SuratJalan.ID = " + Id);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select SuratJalanDetail.ID,SuratJalanDetail.ItemID,Items.Description as ItemName,UOM.UOMCode,SuratJalanDetail.Qty,(OPDetail.Price / 1.1) as Price,Items.Pajak from SuratJalanDetail,OPDetail,SuratJalan,OP,Items,UOM,ScheduleDetail where SuratJalan.ID = SuratJalanDetail.SuratJalanID and SuratJalan.OPID = OP.ID and OP.ID = OPDetail.OPID and SuratJalanDetail.ItemID = Items.ID and Items.UOMID = UOM.ID and SuratJalanDetail.ScheduleDetailID = ScheduleDetail.ID and ScheduleDetail.DocumentDetailID = OPDetail.ID and SuratJalan.ID = " + Id);  // utk pecahan
            //21 Nov 14
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select SuratJalanDetail.ID,SuratJalanDetail.ItemID,Items.Description as ItemName,UOM.UOMCode,SuratJalanDetail.Qty,Round(OPDetail.Price / 1.1,0,1) as Price,case OP.BarangPromosi when 0 then (select Pajak from items where id = SuratJalanDetail.ItemID) when 1 then (select BarangPromosi from OP where ID = opdetail.OPID) end Pajak,ScheduleDetail.ID as ScheduleDetailId from SuratJalanDetail,OPDetail,SuratJalan,OP,Items,UOM,ScheduleDetail where SuratJalan.ID = SuratJalanDetail.SuratJalanID and SuratJalan.OPID = OP.ID and OP.ID = OPDetail.OPID and SuratJalanDetail.ItemID = Items.ID and Items.UOMID = UOM.ID and SuratJalanDetail.ScheduleDetailID = ScheduleDetail.ID and ScheduleDetail.DocumentDetailID = OPDetail.ID and SuratJalan.ID = " + Id);
            strError = dataAccess.Error;
            arrSuratJalanDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalanDetail.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrSuratJalanDetail.Add(new SuratJalanDetail());

            return arrSuratJalanDetail;
        }

        public ArrayList RetrieveSuratJalanDetailBySJNoJasa(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select SuratJalanDetail.ID,SuratJalanDetail.ItemID,Items.Description as ItemName,UOM.UOMCode,SuratJalanDetail.Qty,Round(OPDetail.Price / 1.1,0,1) as Price,Items.Pajak from SuratJalanDetail,OPDetail,SuratJalan,OP,Items,UOM,ScheduleDetail where SuratJalan.ID = SuratJalanDetail.SuratJalanID and SuratJalan.OPID = OP.ID and OP.ID = OPDetail.OPID and SuratJalanDetail.ItemID = Items.ID and Items.UOMID = UOM.ID and SuratJalanDetail.ScheduleDetailID = ScheduleDetail.ID and ScheduleDetail.DocumentDetailID = OPDetail.ID and SuratJalan.ID = " + Id);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select SuratJalanDetail.ID,SuratJalanDetail.ItemID,Items.Description as ItemName,UOM.UOMCode,SuratJalanDetail.Qty,(OPDetail.Price / 1.1) as Price,Items.Pajak from SuratJalanDetail,OPDetail,SuratJalan,OP,Items,UOM,ScheduleDetail where SuratJalan.ID = SuratJalanDetail.SuratJalanID and SuratJalan.OPID = OP.ID and OP.ID = OPDetail.OPID and SuratJalanDetail.ItemID = Items.ID and Items.UOMID = UOM.ID and SuratJalanDetail.ScheduleDetailID = ScheduleDetail.ID and ScheduleDetail.DocumentDetailID = OPDetail.ID and SuratJalan.ID = " + Id);  // utk pecahan
            //21 Nov 14
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select SuratJalanDetail.ID,SuratJalanDetail.ItemID,Items.Description as ItemName,UOM.UOMCode,SuratJalanDetail.Qty,Round(OPDetail.Price / 1.1,-1) as Price,case OP.BarangPromosi when 0 then (select Pajak from items where id = SuratJalanDetail.ItemID) when 1 then (select BarangPromosi from OP where ID = opdetail.OPID) end Pajak from SuratJalanDetail,OPDetail,SuratJalan,OP,Items,UOM,ScheduleDetail where SuratJalan.ID = SuratJalanDetail.SuratJalanID and SuratJalan.OPID = OP.ID and OP.ID = OPDetail.OPID and SuratJalanDetail.ItemID = Items.ID and Items.UOMID = UOM.ID and SuratJalanDetail.ScheduleDetailID = ScheduleDetail.ID and ScheduleDetail.DocumentDetailID = OPDetail.ID and SuratJalan.ID = " + Id);
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select SuratJalanDetail.ID,SuratJalanDetail.ItemID,Items.Description as ItemName,UOM.UOMCode,SuratJalanDetail.Qty,Round(OPDetail.Price / 1.1,-1) as Price,case OP.BarangPromosi when 0 then (select Pajak from items where id = SuratJalanDetail.ItemID) when 1 then (select BarangPromosi from OP where ID = opdetail.OPID) end Pajak,ScheduleDetail.ID as ScheduleDetailId from SuratJalanDetail,OPDetail,SuratJalan,OP,Items,UOM,ScheduleDetail,Groups where SuratJalan.ID = SuratJalanDetail.SuratJalanID and SuratJalan.OPID = OP.ID and OP.ID = OPDetail.OPID and SuratJalanDetail.ItemID = Items.ID and Items.UOMID = UOM.ID and SuratJalanDetail.ScheduleDetailID = ScheduleDetail.ID and  ScheduleDetail.DocumentDetailID = OPDetail.ID and Items.GroupID=Groups.ID and Groups.ItemTypeID in (1,2) and SuratJalan.ID = " + Id);
            strError = dataAccess.Error;
            arrSuratJalanDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalanDetail.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrSuratJalanDetail.Add(new SuratJalanDetail());

            return arrSuratJalanDetail;
        }
        public ArrayList RetrieveSuratJalanDetailBySJ2(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select SuratJalanDetail.ID,SuratJalanDetail.ItemID,Items.Description as ItemName,UOM.UOMCode,SuratJalanDetail.Qty,OPDetail.Price,Items.Pajak  from SuratJalanDetail,OPDetail,SuratJalan,OP,Items,UOM,ScheduleDetail where SuratJalan.ID = SuratJalanDetail.SuratJalanID and SuratJalan.OPID = OP.ID and OP.ID = OPDetail.OPID and SuratJalanDetail.ItemID = Items.ID and Items.UOMID = UOM.ID and SuratJalanDetail.ScheduleDetailID = ScheduleDetail.ID and ScheduleDetail.DocumentDetailID = OPDetail.ID and SuratJalan.ID = " + Id);
            // 21 Nov 14
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select SuratJalanDetail.ID,SuratJalanDetail.ItemID,Items.Description as ItemName,UOM.UOMCode,SuratJalanDetail.Qty,OPDetail.Price,case OP.BarangPromosi when 0 then (select Pajak from items where id = SuratJalanDetail.ItemID) when 1 then (select BarangPromosi from OP where ID = opdetail.OPID) end Pajak,ScheduleDetail.ID as ScheduleDetailId  from SuratJalanDetail,OPDetail,SuratJalan,OP,Items,UOM,ScheduleDetail where SuratJalan.ID = SuratJalanDetail.SuratJalanID and SuratJalan.OPID = OP.ID and OP.ID = OPDetail.OPID and SuratJalanDetail.ItemID = Items.ID and Items.UOMID = UOM.ID and SuratJalanDetail.ScheduleDetailID = ScheduleDetail.ID and ScheduleDetail.DocumentDetailID = OPDetail.ID and SuratJalan.ID = " + Id);
            strError = dataAccess.Error;
            arrSuratJalanDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalanDetail.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrSuratJalanDetail.Add(new SuratJalanDetail());

            return arrSuratJalanDetail;
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ScheduleID,A.TransferOrderID,A.TransferDetailID,B.TransferOrderNo,A.ItemID,C.ItemCode,C.Description as ItemName,A.Qty,B.FromDepoName,B.ToDepoName,B.TotalKubikasi from SuratJalanDetail as A,TransferOrder as B where A.TransferOrderID = B.ID and A.ItemID = B.ID and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrSuratJalanDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSuratJalanDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSuratJalanDetail.Add(new SuratJalanDetail());

            return arrSuratJalanDetail;
        }
        public int GetOPdetailPrice(int sjDetID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(Price,0) as Price from ScheduleDetail,SuratJalanDetail,OPDetail where SuratJalanDetail.ScheduleDetailID=ScheduleDetail.ID and ScheduleDetail.DocumentDetailID=OPDetail.ID and SuratJalanDetail.ID="+sjDetID);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Price"].ToString());
                }
            }

            return 0;
        }
        public int GetOPdetailPriceRetail(int sjDetID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(HargaRetail,0) as Price from ScheduleDetail,SuratJalanDetail,OPDetail where SuratJalanDetail.ScheduleDetailID=ScheduleDetail.ID and ScheduleDetail.DocumentDetailID=OPDetail.ID and SuratJalanDetail.ID=" + sjDetID);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Price"].ToString());
                }
            }

            return 0;
        }
        public SuratJalanDetail GenerateObject(SqlDataReader sqlDataReader)
        {
            objSuratJalanDetail = new SuratJalanDetail();
            objSuratJalanDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objSuratJalanDetail.SuratJalanId = Convert.ToInt32(sqlDataReader["SuratJalanId"]);
            objSuratJalanDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objSuratJalanDetail.ItemCode = sqlDataReader["ItemCode"].ToString();
            objSuratJalanDetail.ItemName = sqlDataReader["ItemName"].ToString();
            objSuratJalanDetail.Qty = Convert.ToInt32(sqlDataReader["Qty"]);
            objSuratJalanDetail.ScheduleDetailId = Convert.ToInt32(sqlDataReader["ScheduleDetailId"]);
            objSuratJalanDetail.Paket = Convert.ToInt32(sqlDataReader["Paket"]);
            if (objSuratJalanDetail.ItemID == 753 || objSuratJalanDetail.ItemID == 916)
                objSuratJalanDetail.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);

            return objSuratJalanDetail;
        }

        public SuratJalanDetail GenerateObject2(SqlDataReader sqlDataReader)
        {
            objSuratJalanDetail = new SuratJalanDetail();
            objSuratJalanDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);            
            objSuratJalanDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);            
            objSuratJalanDetail.ItemName = sqlDataReader["ItemName"].ToString();
            objSuratJalanDetail.Qty = Convert.ToInt32(sqlDataReader["Qty"]);
            objSuratJalanDetail.UOMCode = sqlDataReader["UOMCode"].ToString();
            objSuratJalanDetail.TotalPrice = Convert.ToDecimal(sqlDataReader["Price"]);
            objSuratJalanDetail.Pajak = Convert.ToInt16(sqlDataReader["Pajak"]);
            objSuratJalanDetail.ScheduleDetailId = Convert.ToInt32(sqlDataReader["ScheduleDetailId"]);

            return objSuratJalanDetail;
        }
        public SuratJalanDetail GenerateObjectA(SqlDataReader sqlDataReader)
        {
            objSuratJalanDetail = new SuratJalanDetail();
            objSuratJalanDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objSuratJalanDetail.SuratJalanNo = sqlDataReader["SuratJalanNo"].ToString();
            objSuratJalanDetail.SuratJalanId = Convert.ToInt32(sqlDataReader["SuratJalanId"]);
            objSuratJalanDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objSuratJalanDetail.ItemCode = sqlDataReader["ItemCode"].ToString();
            objSuratJalanDetail.ItemName = sqlDataReader["ItemName"].ToString();
            objSuratJalanDetail.Qty = Convert.ToInt32(sqlDataReader["Qty"]);
            objSuratJalanDetail.ScheduleDetailId = Convert.ToInt32(sqlDataReader["ScheduleDetailId"]);
            objSuratJalanDetail.Paket = Convert.ToInt32(sqlDataReader["Paket"]);
            if (objSuratJalanDetail.ItemID == 753 || objSuratJalanDetail.ItemID == 916)
                objSuratJalanDetail.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);

            return objSuratJalanDetail;
        }
        public SuratJalanDetail GenerateObject3(SqlDataReader sqlDataReader)
        {
            objSuratJalanDetail = new SuratJalanDetail();
            objSuratJalanDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objSuratJalanDetail.ItemName = sqlDataReader["ItemName"].ToString();
            objSuratJalanDetail.Qty = Convert.ToInt32(sqlDataReader["Qty"]);
            objSuratJalanDetail.UOMCode = sqlDataReader["UOMCode"].ToString();
            objSuratJalanDetail.TotalPrice = Convert.ToDecimal(sqlDataReader["Price"]);
            objSuratJalanDetail.Pajak = Convert.ToInt16(sqlDataReader["Pajak"]);

            return objSuratJalanDetail;
        }
        public SuratJalanDetail GenerateObject4(SqlDataReader sqlDataReader)
        {
            objSuratJalanDetail = new SuratJalanDetail();
            objSuratJalanDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objSuratJalanDetail.SuratJalanId = Convert.ToInt32(sqlDataReader["SuratJalanId"]);
            objSuratJalanDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objSuratJalanDetail.ItemCode = sqlDataReader["ItemCode"].ToString();
            objSuratJalanDetail.ItemName = sqlDataReader["ItemName"].ToString();
            objSuratJalanDetail.Qty = Convert.ToInt32(sqlDataReader["Qty"]);
            objSuratJalanDetail.ScheduleDetailId = Convert.ToInt32(sqlDataReader["ScheduleDetailId"]);
            objSuratJalanDetail.Paket = Convert.ToInt32(sqlDataReader["Paket"]);
            if (objSuratJalanDetail.ItemID == 753 || objSuratJalanDetail.ItemID == 916)
                objSuratJalanDetail.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);

            objSuratJalanDetail.HargaJual = Convert.ToDecimal(sqlDataReader["Price"]);
            objSuratJalanDetail.Uom = sqlDataReader["Uom"].ToString();

            return objSuratJalanDetail;
        }

    }
}

