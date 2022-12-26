using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using Domain;
using DataAccessLayer;

namespace BusinessFacade
{
    public class ReceiptDetailFacade : AbstractTransactionFacade
    {
        private ReceiptDetail objReceiptDetail = new ReceiptDetail();
        private ArrayList arrReceiptDetail;
        private List<SqlParameter> sqlListParam;
        //private string scheduleNo = string.Empty;
        private string receiptNo = string.Empty;

        public ReceiptDetailFacade(object objDomain)
            : base(objDomain)
        {
            objReceiptDetail = (ReceiptDetail)objDomain;
        }

        public ReceiptDetailFacade()
        {

        }

        public ReceiptDetailFacade(object objDomain, string strReceiptNo)
        {
            objReceiptDetail = (ReceiptDetail)objDomain;
            receiptNo = strReceiptNo;
        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ReceiptID", objReceiptDetail.ReceiptID));
                sqlListParam.Add(new SqlParameter("@ItemID", objReceiptDetail.ItemID));
                sqlListParam.Add(new SqlParameter("@Quantity", objReceiptDetail.Quantity));
                sqlListParam.Add(new SqlParameter("@Kadarair", objReceiptDetail.Kadarair));
                sqlListParam.Add(new SqlParameter("@Price", objReceiptDetail.Price));
                sqlListParam.Add(new SqlParameter("@Disc", objReceiptDetail.Disc));
                sqlListParam.Add(new SqlParameter("@TotalPrice", objReceiptDetail.TotalPrice));
                sqlListParam.Add(new SqlParameter("@PoID", objReceiptDetail.PoID));
                sqlListParam.Add(new SqlParameter("@PoNo", objReceiptDetail.PoNo));
                sqlListParam.Add(new SqlParameter("@SppID", objReceiptDetail.SppID));
                sqlListParam.Add(new SqlParameter("@SppNo", objReceiptDetail.SppNo));
                sqlListParam.Add(new SqlParameter("@UomID", objReceiptDetail.UomID));
                sqlListParam.Add(new SqlParameter("@PODetailID", objReceiptDetail.PODetailID));
                sqlListParam.Add(new SqlParameter("@RowStatus", objReceiptDetail.RowStatus));
                sqlListParam.Add(new SqlParameter("@GroupID", objReceiptDetail.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objReceiptDetail.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@Keterangan", objReceiptDetail.Keterangan));
                sqlListParam.Add(new SqlParameter("@ItemID2", objReceiptDetail.ItemID2));
                sqlListParam.Add(new SqlParameter("@QtyTimbang", objReceiptDetail.QtyTimbang));
                sqlListParam.Add(new SqlParameter("@KodeTimbang", objReceiptDetail.KodeTimbang));
                sqlListParam.Add(new SqlParameter("@TipeAsset", objReceiptDetail.TipeAsset));


                //sqlListParam.Add(new SqlParameter("@QtyBPAS", objReceiptDetail.TimbanganBPAS));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertReceiptDetail");
                /** updare Reorder Point */
                ROPFacade ropfacade = new ROPFacade();
                ropfacade.UpdateROPByReceipt(objReceiptDetail.ItemID, objReceiptDetail.Quantity);
                /** update qty timbangan untuk gypsum*/
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
            int intResult = Delete(transManager);
            if (strError == string.Empty)
                intResult = Insert(transManager);

            return intResult;
        }
        public void updateAveragePrice(object objreceiptDetail, int bulan, int tahun)
        {
            int intResult = 0;
            ReceiptDetail receiptDetail = new ReceiptDetail();
            receiptDetail = (ReceiptDetail)objreceiptDetail;
            InventoryFacade inventoryFacade = new InventoryFacade();
            Inventory inventory = new Inventory();
            //update average price
            double LastPrice = inventoryFacade.GetLastPrice(receiptDetail.ItemID, receiptDetail.ItemTypeID);
            double Price = Convert.ToDouble(receiptDetail.Price);
            double EndStock = inventoryFacade.GetStock(receiptDetail.ItemID, receiptDetail.ItemTypeID);
            double QtyBeli = Convert.ToDouble(receiptDetail.Quantity);
            double AvgPrice = ((EndStock * LastPrice) + (QtyBeli * Price)) / (EndStock + QtyBeli);
            SaldoInventoryFacade saldoInventoryFacade = new SaldoInventoryFacade();
            SaldoInventory objSaldoInventory = new SaldoInventory();
            objSaldoInventory.ItemID = receiptDetail.ItemID;
            objSaldoInventory.GroupID = receiptDetail.GroupID;
            objSaldoInventory.ItemTypeID = receiptDetail.ItemTypeID;
            objSaldoInventory.AvgPrice = Convert.ToInt32(AvgPrice);
            objSaldoInventory.MonthPeriod = bulan;
            objSaldoInventory.YearPeriod = tahun;
            //cek apakah data SaldoInventory tahun ini sudah ada
            intResult = saldoInventoryFacade.CekRow(receiptDetail.ItemID, tahun, receiptDetail.ItemTypeID);
            if (intResult == 0)
            {
                intResult = saldoInventoryFacade.Insert(objSaldoInventory);
            }
            intResult = saldoInventoryFacade.UpdateSaldoAvgPriceBlnIni(objSaldoInventory);
            //end update average price

        }

        public override int Delete(TransactionManager transManager)
        {
            try
            {
                objReceiptDetail = (ReceiptDetail)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objReceiptDetail.ID));

                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteReceiptDetail");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int CancelReceiptDetail(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objReceiptDetail.ID));
                sqlListParam.Add(new SqlParameter("@ItemID", objReceiptDetail.ItemID));
                sqlListParam.Add(new SqlParameter("@Jumlah", objReceiptDetail.Quantity));
                sqlListParam.Add(new SqlParameter("@POPurchnId", objReceiptDetail.PoID));
                sqlListParam.Add(new SqlParameter("@FlagPO", objReceiptDetail.FlagPO));
                sqlListParam.Add(new SqlParameter("@FlagTipe", objReceiptDetail.FlagTipe));

                int intResult = transManager.DoTransaction(sqlListParam, "spCancelReceiptDetail");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateRMS(object objDomain)
        {
            try
            {
                objReceiptDetail = (ReceiptDetail)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objReceiptDetail.ID));
                sqlListParam.Add(new SqlParameter("@Qty", objReceiptDetail.Quantity));
                sqlListParam.Add(new SqlParameter("@Keterangan", objReceiptDetail.Keterangan));
                DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                SqlDataReader result = dataAccess.RetrieveDataByParameter(sqlListParam, "spReceiptDetailUpdate");
                return (dataAccess.Error == string.Empty) ? 1 : -1;
            }
            catch
            {
                return -1;
            }
        }
        public int CancelInventoryByReceiptDetail(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objReceiptDetail.ID));
                sqlListParam.Add(new SqlParameter("@ItemID", objReceiptDetail.ItemID));
                sqlListParam.Add(new SqlParameter("@Jumlah", objReceiptDetail.Quantity));
                sqlListParam.Add(new SqlParameter("@POPurchnId", objReceiptDetail.PoID));
                sqlListParam.Add(new SqlParameter("@FlagPO", objReceiptDetail.FlagPO));
                sqlListParam.Add(new SqlParameter("@FlagTipe", objReceiptDetail.FlagTipe));

                int intResult = transManager.DoTransaction(sqlListParam, "spCancelInventoryByReceiptDetail");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int InsertMemoHarian(object objDomain)
        {

            try
            {
                objReceiptDetail = (ReceiptDetail)objDomain;
                List<SqlParameter> spa = new List<SqlParameter>();
                spa.Add(new SqlParameter("@SchID", objReceiptDetail.ID));//1
                spa.Add(new SqlParameter("@SchNo", objReceiptDetail.ScheduleNo));//2
                spa.Add(new SqlParameter("@RMSDetailID", objReceiptDetail.ID));//3
                spa.Add(new SqlParameter("@RMSID", objReceiptDetail.ReceiptID));//4
                spa.Add(new SqlParameter("@RMSNo", objReceiptDetail.PoNo));//5
                spa.Add(new SqlParameter("@ItemID", objReceiptDetail.ItemID));//6
                spa.Add(new SqlParameter("@Qty", objReceiptDetail.Quantity));//7
                DataAccess dta = new DataAccess(Global.ConnectionString());
                //SqlDataReader sdr = dta.RetrieveDataByParameter(spa, "spMemoHarianTrans_Insert");
                int rst = dta.ProcessData(spa, "spMemoHarianTrans_Insert", true);
                return rst;// sdr.RecordsAffected;// (dta.Error != string.Empty) ? -1 : 1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public int update_timbangan(int ID, string Qty)
        {
            int result = 0;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString("Update ReceiptDetail set toAsset=" + Qty + " where ID=" + ID);
            if (da.Error == string.Empty)
            {
                result = sdr.RecordsAffected;

            }
            return result;
        }
        //public int DeleteCancelScheduleDetail(TransactionManager transManager)
        //{
        //    try
        //    {
        //        sqlListParam = new List<SqlParameter>();
        //        sqlListParam.Add(new SqlParameter("@ID", objReceiptDetail.ID));
        //        sqlListParam.Add(new SqlParameter("@TypeDoc", objReceiptDetail.TypeDoc));
        //        sqlListParam.Add(new SqlParameter("@DocumentDetailID", objReceiptDetail.DocumentDetailID));
        //        sqlListParam.Add(new SqlParameter("@Qty", objReceiptDetail.Qty));
        //        int intResult = transManager.DoTransaction(sqlListParam, "spDeleteCancelScheduleDetail");

        //        strError = transManager.Error;

        //        return intResult;

        //    }
        //    catch (Exception ex)
        //    {
        //        strError = ex.Message;
        //        return -1;
        //    }
        //}

        //public int DeleteScheduleDetail(TransactionManager transManager)
        //{
        //    try
        //    {
        //        sqlListParam = new List<SqlParameter>();
        //        sqlListParam.Add(new SqlParameter("@ID", objReceiptDetail.ID));
        //        int intResult = transManager.DoTransaction(sqlListParam, "spDeleteScheduleDetailByID");

        //        strError = transManager.Error;

        //        return intResult;

        //    }
        //    catch (Exception ex)
        //    {
        //        strError = ex.Message;
        //        return -1;
        //    }
        //}
        private string Criteria()
        {
            string strQuery = (HttpContext.Current.Session["ReceiptCriteria"] == null) ? string.Empty :
                            HttpContext.Current.Session["ReceiptCriteria"].ToString();
            return strQuery;
        }
        public override ArrayList Retrieve()
        {
            string strSQL = "select A.ID,A.ReceiptID,A.PODetailID,A.ItemID,A.Quantity,A.Disc,A.TotalPrice,A.POID,A.PONo,A.SPPID,A.SPPNo," +
                            "A.UomID,A.GroupID,A.ItemTypeID,A.Keterangan,isnull(A.KadarAir,0)KadarAir,isnull(A.AvgPrice,0) as AvgPrice,A.JenisBiaya,A.Price, " +
                            "Case A.ItemTypeID " +
                            "when 1 Then (Select ItemCode From Inventory where ID=A.ItemID) " +
                            "when 2 Then (select Itemcode From Asset Where ID=A.ItemID) " +
                            "when 3 then (select ItemCode from biaya where ID=A.ItemID) End ItemCode, " +
                            "Case A.ItemTypeID " +
                            "when 1 Then (Select ItemName From Inventory where ID=A.ItemID) " +
                            "when 2 Then (select ItemName From Asset Where ID=A.ItemID) " +
                            "when 3 then (select ItemName from biaya where ID=A.ItemID) End ItemName, " +
                            "(select UomCode from UOM where ID=A.UomID) UOMCode,A.RowStatus,A.QtyTimbang,A.ToAsset " +
                            " from ReceiptDetail as A,Receipt as B where B.ID=A.ReceiptID and A.RowStatus>-1 " + Criteria();
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrReceiptDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceiptDetail.Add(GenerateObject(sqlDataReader));

                }
            }
            else
                arrReceiptDetail.Add(new ReceiptDetail());

            return arrReceiptDetail;
        }
        public decimal SumPOReceiptDetailB(string poNo, int poDetailID)
        {
            string strSql = "select isnull(SUM(B.Quantity),0) as Jumlah from Receipt as A,ReceiptDetail as B where A.ID=B.ReceiptID " +
                            "and A.Status>-1 and B.RowStatus>-1 and B.PODetailID=" + poDetailID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToDecimal(sqlDataReader["Jumlah"]);
                }
            }
            return 0;
        }
        public decimal SumPOReceiptDetail(int itemID, string poNo, int poDetailID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "select isnull(SUM(B.Quantity),0) as Jumlah from Receipt as A,ReceiptDetail as B where A.ID=B.ReceiptID " +
                          "and A.Status>-1 and B.RowStatus>-1 and B.ItemID=" + itemID + " and B.PODetailID=" + poDetailID;
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToDecimal(sqlDataReader["Jumlah"]);
                }
            }

            return 0;
        }

        public ArrayList RetrieveByReceiptIdBakuBantu(int Id)
        {
            string strSQL = "select A.ID,A.ReceiptID,A.ItemID,A.Quantity,A.Price,A.Disc,A.TotalPrice,A.POID,A.PONo,A.SPPID,A.SPPNo,A.UomID," +
                            "A.RowStatus,isnull(A.kadarair,0) as kadarair,B.UOMCode,C.ItemCode,C.ItemName,A.PODetailID,A.GroupID,A.Keterangan," +
                            "A.ItemTypeID,A.QtyTimbang,A.ToAsset " +
                            "from ReceiptDetail as A, UOM as B, Inventory as C where A.RowStatus>-1 AND A.UomID=B.ID and A.ItemID=C.ID and " +
                            "A.GroupID in (1,2) and A.ReceiptID=" + Id;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrReceiptDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceiptDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrReceiptDetail.Add(new ReceiptDetail());

            return arrReceiptDetail;
        }

        private string Criteria(int ID)
        {
            string where = (HttpContext.Current.Session["ReceiptID"] != null) ? HttpContext.Current.Session["ReceiptID"].ToString() : string.Empty;
            return (where == string.Empty) ? " AND A.RowStatus>-1 and A.ReceiptID=" + ID : where;
        }
        public ArrayList RetrieveByReceiptId(int Id)
        {
            string strSQL = "select A.ID,A.ReceiptID,A.ItemID,A.Quantity,A.Price,A.Disc,A.TotalPrice,A.POID,A.PONo,A.SPPID,A.SPPNo,A.UomID,A.RowStatus," +
                            "isnull(A.kadarair,0) as kadarair,B.UOMCode,C.ItemCode,C.ItemName,A.PODetailID,A.GroupID,A.Keterangan,A.ItemTypeID," +
                            "A.QtyTimbang,A.ToAsset " +
                            "from ReceiptDetail as A, " +
                            "UOM as B,Inventory as C where  A.UomID=B.ID and A.ItemID=C.ID " + this.Criteria(Id);
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrReceiptDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceiptDetail.Add(GenerateObject(sqlDataReader));
                }
            }


            return arrReceiptDetail;
        }

        public ArrayList RetrieveByReceiptIdwithGroupID(int Id, int groupID)
        {
            string strSQL = "select A.ID,A.ReceiptID,A.ItemID,A.Quantity,A.Price,A.Disc,A.TotalPrice,A.POID,A.PONo,A.SPPID,A.SPPNo,A.UomID,A.RowStatus," +
                            "isnull(A.kadarair,0) as kadarair,B.UOMCode,C.ItemCode,C.ItemName,A.PODetailID,A.GroupID,A.Keterangan,A.ItemTypeID,A.QtyTimbang" +
                            ",A.ToAsset from ReceiptDetail as A, " +
                            "UOM as B, Inventory as C where A.RowStatus>-1 AND A.UomID=B.ID and A.ItemID=C.ID and A.ReceiptID=" + Id + " and A.GroupID=" + groupID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrReceiptDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceiptDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrReceiptDetail.Add(new ReceiptDetail());

            return arrReceiptDetail;
        }

        public ArrayList RetrieveByReceiptIdForAsset(int Id)
        {
            string strSQL = "select A.ID,A.ReceiptID,A.ItemID,A.Quantity,A.Price,A.Disc,A.TotalPrice,A.POID,A.PONo,A.SPPID,A.SPPNo,A.UomID,A.RowStatus," +
                            "isnull(A.kadarair,0) as kadarair,B.UOMCode,C.ItemCode,C.ItemName,A.PODetailID,A.GroupID,A.Keterangan,A.ItemTypeID,A.QtyTimbang" +
                            ",A.ToAsset from ReceiptDetail as A," +
                            "UOM as B, Asset as C where A.RowStatus>-1 AND A.UomID=B.ID and A.ItemID=C.ID and A.ReceiptID=" + Id;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrReceiptDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceiptDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrReceiptDetail.Add(new ReceiptDetail());

            return arrReceiptDetail;
        }

        public ArrayList RetrieveByReceiptIdForBiaya(int Id)
        {
            string strSQL = "SELECT *,(Select ID From Biaya where ItemCode=C.ItemCode) as ItemID2 from( " +
                          "select A.ID,A.ReceiptID,A.ItemID,A.Quantity,A.Price,A.Disc,A.TotalPrice,A.POID,A.PONo,A.SPPID,A.SPPNo,A.UomID," +
                          "A.RowStatus,isnull(A.kadarair,0) as kadarair,B.UOMCode," + ItemCodeForBiaya("A") + " as ItemCode," +
                          ItemNameFromBiaya("A") +
                          " as ItemName,A.PODetailID,A.GroupID,A.Keterangan,A.ItemTypeID " +
                          "FROM ReceiptDetail as A, UOM as B, Biaya as C where A.RowStatus>-1 AND A.UomID=B.ID and A.ItemID=C.ID and A.ReceiptID=" + Id +
                          ") as C";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrReceiptDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceiptDetail.Add(GenerateObjectNew(sqlDataReader));
                }
            }
            else
                arrReceiptDetail.Add(new ReceiptDetail());

            return arrReceiptDetail;
        }

        //public ArrayList RetrieveByNo(string strReceiptNo)
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ScheduleID,A.DocumentID,A.DocumentDetailID,A.TypeDoc,A.DocumentNo,A.ItemID,C.ItemCode,C.Description as ItemName,F.CityName as KotaTujuan,G.NamaKabupaten as AreaTujuan, A.Qty,(C.Tebal * C.Panjang * C.Lebar * A.Qty) as TotalKubikasi,A.Paket from ReceiptDetail as A,Schedule as B,Items as C,OP as D,Toko as E,City as F,Kabupaten as G where A.ItemID = C.ID and A.DocumentID = D.ID and D.CustomerId = E.ID and E.CityID = F.ID and E.KabupatenID = G.ID and A.ScheduleID = B.ID and A.TypeDoc = 0 and A.Status >= 0 and B.ScheduleNo = '" + strSchedule + "'");
        //    strError = dataAccess.Error;
        //    arrReceiptDetail = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrReceiptDetail.Add(GenerateObject(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrReceiptDetail.Add(new ReceiptDetail());

        //    return arrReceiptDetail;
        //}

        //public ArrayList RetrieveDistinctById(int Id)
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select distinct(DocumentID),TypeDoc from ReceiptDetail where ScheduleID = " + Id + " and Status >= 0");
        //    strError = dataAccess.Error;
        //    arrReceiptDetail = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrReceiptDetail.Add(DataSchedule(sqlDataReader));
        //        }
        //    }

        //    return arrReceiptDetail;
        //}

        //public int RetrieveJumOP(int opId)
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select COUNT(id) as id from ReceiptDetail where DocumentID = " + opId + " and Status >= 0 and TypeDoc = 0");
        //    strError = dataAccess.Error;

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            return Convert.ToInt32(sqlDataReader["id"]);
        //        }
        //    }

        //    return 0;
        //}


        //private int[] DataSchedule(SqlDataReader sqlDataReader)
        //{
        //    int[] intSchedule = new int[2];
        //    intSchedule[0] = Convert.ToInt32(sqlDataReader["DocumentID"]);
        //    intSchedule[1] = Convert.ToInt32(sqlDataReader["TypeDoc"]);
        //    return intSchedule;
        //}

        //public ArrayList RetrieveByNo(string strSchedule)
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ScheduleID,A.DocumentID,A.DocumentDetailID,A.TypeDoc,A.DocumentNo,A.ItemID,C.ItemCode,C.Description as ItemName,F.CityName as KotaTujuan,G.NamaKabupaten as AreaTujuan, A.Qty,(C.Tebal * C.Panjang * C.Lebar * A.Qty) as TotalKubikasi,A.Paket from ReceiptDetail as A,Schedule as B,Items as C,OP as D,Toko as E,City as F,Kabupaten as G where A.ItemID = C.ID and A.DocumentID = D.ID and D.CustomerId = E.ID and E.CityID = F.ID and E.KabupatenID = G.ID and A.ScheduleID = B.ID and A.TypeDoc = 0 and A.Status >= 0 and B.ScheduleNo = '" + strSchedule + "'");
        //    strError = dataAccess.Error;
        //    arrReceiptDetail = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrReceiptDetail.Add(GenerateObject(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrReceiptDetail.Add(new ReceiptDetail());

        //    return arrReceiptDetail;
        //}

        //public ArrayList RetrieveByOPNo(string strSchedule, string strOPNo, int typeCust)
        //{
        //    string strCommmand = string.Empty;
        //    if (typeCust == 1)
        //        strCommmand = "select A.ID,A.ScheduleID,A.DocumentID,A.DocumentDetailID,A.TypeDoc,A.DocumentNo,A.ItemID,C.ItemCode,C.Description as ItemName,F.CityName as KotaTujuan,G.NamaKabupaten as AreaTujuan, A.Qty,(C.Tebal * C.Panjang * C.Lebar * A.Qty) as TotalKubikasi,A.Paket from ReceiptDetail as A,Schedule as B,Items as C,OP as D,Toko as E,City as F,Kabupaten as G where A.ItemID = C.ID and A.DocumentID = D.ID and D.CustomerId = E.ID and E.CityID = F.ID and E.KabupatenID = G.ID and A.ScheduleID = B.ID and A.TypeDoc = 0 and A.Status > -1 and B.ScheduleNo = '" + strSchedule + "' and A.DocumentNo = '" + strOPNo + "'";
        //    else
        //        strCommmand = "select A.ID,A.ScheduleID,A.DocumentID,A.DocumentDetailID,A.TypeDoc,A.DocumentNo,A.ItemID,C.ItemCode,C.Description as ItemName,F.CityName as KotaTujuan,G.NamaKabupaten as AreaTujuan, A.Qty,(C.Tebal * C.Panjang * C.Lebar * A.Qty) as TotalKubikasi,A.Paket from ReceiptDetail as A,Schedule as B,Items as C,OP as D,Customer as E,City as F,Kabupaten as G where A.ItemID = C.ID and A.DocumentID = D.ID and D.CustomerId = E.ID and E.CityID = F.ID and E.KabupatenID = G.ID and A.ScheduleID = B.ID and A.TypeDoc = 0 and A.Status > -1 and B.ScheduleNo = '" + strSchedule + "' and A.DocumentNo = '" + strOPNo + "'";
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strCommmand);
        //    strError = dataAccess.Error;
        //    arrReceiptDetail = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrReceiptDetail.Add(GenerateObject(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrReceiptDetail.Add(new ReceiptDetail());

        //    return arrReceiptDetail;
        //}

        //public ArrayList RetrieveByTONo(string strSchedule, string strTONo)
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ScheduleID,A.DocumentID,A.DocumentDetailID,A.TypeDoc,A.DocumentNo,A.ItemID,C.ItemCode,C.Description as ItemName,F.CityName as KotaTujuan,G.NamaKabupaten as AreaTujuan, A.Qty,(C.Tebal * C.Panjang * C.Lebar * A.Qty) as TotalKubikasi,A.Paket from ReceiptDetail as A,Schedule as B,Items as C,TransferOrder as D,Depo as E,City as F,Kabupaten as G where A.ItemID = C.ID and A.DocumentID = D.ID and D.ToDepoID = E.ID and E.CityID = F.ID and E.KabupatenID = G.ID and A.ScheduleID = B.ID and A.TypeDoc = 1 and B.ScheduleNo = '" + strSchedule + "' and A.Status > -1 and A.DocumentNo = '" + strTONo + "'");
        //    strError = dataAccess.Error;
        //    arrReceiptDetail = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrReceiptDetail.Add(GenerateObject(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrReceiptDetail.Add(new ReceiptDetail());

        //    return arrReceiptDetail;
        //}

        //public int RetrieveByOpDetailId(int documentDetailId)
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(qty,0) from ReceiptDetail where TypeDoc = 0 and DocumentDetailId = " + documentDetailId + " and Status = 2");
        //    strError = dataAccess.Error;

        //    int qty = 0;

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            qty = qty + Convert.ToInt32(sqlDataReader["qty"]);
        //        }
        //    }

        //    return qty;
        //}

        //public ArrayList RetrieveByCriteria(string strField, string strValue)
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ScheduleID,A.DocumentID,A.DocumentDetailID,A.TypeDoc,A.DocumentNo,A.ItemID,C.ItemCode,C.Description as ItemName,F.CityName as KotaTujuan,G.NamaKabupaten as AreaTujuan, A.Qty,(C.Tebal * C.Panjang * C.Lebar * A.Qty) as TotalKubikasi,A.Paket from ReceiptDetail as A,Schedule as B,Items as C,TransferOrder as D,Depo as E,City as F,Kabupaten as G where A.ItemID = C.ID and A.DocumentID = D.ID and D.ToDepoID = E.ID and E.CityID = F.ID and E.KabupatenID = G.ID and A.ScheduleID = B.ID and " + strField + " like '%" + strValue + "%'");
        //    strError = dataAccess.Error;
        //    arrReceiptDetail = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrReceiptDetail.Add(GenerateObject(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrReceiptDetail.Add(new ReceiptDetail());

        //    return arrReceiptDetail;
        //}

        //public ArrayList RetrieveListScheduleDetailOP()
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Toko.TokoCode,Toko.TokoName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status from Schedule,ScheduleDetail,Items,OP,Toko where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 1 and OP.CustomerId = Toko.ID order by Schedule.ID desc");
        //    strError = dataAccess.Error;
        //    arrReceiptDetail = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrReceiptDetail.Add(GenerateObjectList(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrReceiptDetail.Add(new ReceiptDetail());

        //    return arrReceiptDetail;
        //}

        //public ArrayList RetrieveListScheduleDetailOP2()
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Toko.TokoCode,Toko.TokoName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status from Schedule,ScheduleDetail,Items,OP,Toko where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 1 and OP.CustomerId = Toko.ID and Schedule.Status = 1 order by Schedule.ID desc");
        //    strError = dataAccess.Error;
        //    arrReceiptDetail = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrReceiptDetail.Add(GenerateObjectList(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrReceiptDetail.Add(new ReceiptDetail());

        //    return arrReceiptDetail;
        //}

        //public ArrayList RetrieveListScheduleDetailOPCustomer()
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Customer.CustomerCode,Customer.CustomerName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status from Schedule,ScheduleDetail,Items,OP,Customer where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 2 and OP.CustomerId = Customer.ID order by Schedule.ID desc");
        //    strError = dataAccess.Error;
        //    arrReceiptDetail = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrReceiptDetail.Add(GenerateObjectListCustomer(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrReceiptDetail.Add(new ReceiptDetail());

        //    return arrReceiptDetail;
        //}

        //public ArrayList RetrieveListScheduleDetailOPByDepo(int depoID)
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Toko.TokoCode,Toko.TokoName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status from Schedule,ScheduleDetail,Items,OP,Toko where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 1 and OP.CustomerId = Toko.ID and OP.DepoID = " + depoID + " order by Schedule.ID desc");
        //    strError = dataAccess.Error;
        //    arrReceiptDetail = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrReceiptDetail.Add(GenerateObjectList(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrReceiptDetail.Add(new ReceiptDetail());

        //    return arrReceiptDetail;
        //}

        //public ArrayList RetrieveListScheduleDetailOPByDepo2(int depoID)
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Toko.TokoCode,Toko.TokoName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status from Schedule,ScheduleDetail,Items,OP,Toko where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 1 and OP.CustomerId = Toko.ID and Schedule.Status = 1 and OP.DepoID = " + depoID + " order by Schedule.ID desc");
        //    strError = dataAccess.Error;
        //    arrReceiptDetail = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrReceiptDetail.Add(GenerateObjectList(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrReceiptDetail.Add(new ReceiptDetail());

        //    return arrReceiptDetail;
        //}

        //public ArrayList RetrieveListScheduleDetailOPByDepoCustomer(int depoID)
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Customer.CustomerCode,Customer.CustomerName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status from Schedule,ScheduleDetail,Items,OP,Customer where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 2 and OP.CustomerId = Customer.ID and OP.DepoID = " + depoID + " order by Schedule.ID desc");
        //    strError = dataAccess.Error;
        //    arrReceiptDetail = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrReceiptDetail.Add(GenerateObjectListCustomer(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrReceiptDetail.Add(new ReceiptDetail());

        //    return arrReceiptDetail;
        //}

        //public ArrayList RetrieveListScheduleDetailOP(int distributorID)
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Toko.TokoCode,Toko.TokoName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status from Schedule,ScheduleDetail,Items,OP,Toko where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 1 and OP.CustomerId = Toko.ID and Toko.DistributorID = " + distributorID + " order by Schedule.ID desc");
        //    strError = dataAccess.Error;
        //    arrReceiptDetail = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrReceiptDetail.Add(GenerateObjectList(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrReceiptDetail.Add(new ReceiptDetail());

        //    return arrReceiptDetail;
        //}

        //public ArrayList RetrieveListScheduleDetailOP2(int distributorID)
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Toko.TokoCode,Toko.TokoName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status from Schedule,ScheduleDetail,Items,OP,Toko where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 1 and OP.CustomerId = Toko.ID and Schedule.Status = 1 and Toko.DistributorID = " + distributorID + " order by Schedule.ID desc");
        //    strError = dataAccess.Error;
        //    arrReceiptDetail = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrReceiptDetail.Add(GenerateObjectList(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrReceiptDetail.Add(new ReceiptDetail());

        //    return arrReceiptDetail;
        //}


        //public ArrayList RetrieveListScheduleDetailTO()
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,ScheduleDetail.DocumentNo,Items.Description as ItemName,' ' as OpNo2,ScheduleDetail.Qty,ScheduleDetail.Status from Schedule,ScheduleDetail,Items where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 1");
        //    strError = dataAccess.Error;
        //    arrReceiptDetail = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrReceiptDetail.Add(GenerateObjectList(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrReceiptDetail.Add(new ReceiptDetail());

        //    return arrReceiptDetail;
        //}

        //public ArrayList RetrieveListScheduleDetailOPByCriteria(string strField, string strValue)
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Toko.TokoCode,Toko.TokoName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status from Schedule,ScheduleDetail,Items,OP,Toko where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 1 and OP.CustomerId = Toko.ID and " + strField + " like '%" + strValue + "%' order by Schedule.ID desc");
        //    strError = dataAccess.Error;
        //    arrReceiptDetail = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrReceiptDetail.Add(GenerateObjectList(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrReceiptDetail.Add(new ReceiptDetail());

        //    return arrReceiptDetail;
        //}

        //public ArrayList RetrieveListScheduleDetailOPByCriteria2(string strField, string strValue)
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Toko.TokoCode,Toko.TokoName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status from Schedule,ScheduleDetail,Items,OP,Toko where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 1 and Schedule.Status = 1 and OP.CustomerId = Toko.ID and " + strField + " like '%" + strValue + "%' order by Schedule.ID desc");
        //    strError = dataAccess.Error;
        //    arrReceiptDetail = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrReceiptDetail.Add(GenerateObjectList(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrReceiptDetail.Add(new ReceiptDetail());

        //    return arrReceiptDetail;
        //}

        //public ArrayList RetrieveListScheduleDetailOPByCriteriaCustomer(string strField, string strValue)
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Customer.CustomerCode,Customer.CustomerName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status from Schedule,ScheduleDetail,Items,OP,Customer where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 2 and OP.CustomerId = Customer.ID and " + strField + " like '%" + strValue + "%' order by Schedule.ID desc");
        //    strError = dataAccess.Error;
        //    arrReceiptDetail = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrReceiptDetail.Add(GenerateObjectListCustomer(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrReceiptDetail.Add(new ReceiptDetail());

        //    return arrReceiptDetail;
        //}

        //public ArrayList RetrieveListScheduleDetailOPByCriteria(string strField, string strValue, int distributorID)
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Toko.TokoCode,Toko.TokoName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status from Schedule,ScheduleDetail,Items,OP,Toko where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 1 and OP.CustomerId = Toko.ID and Toko.DistributorID = " + distributorID + " and " + strField + " = '" + strValue + "' order by Schedule.ID desc");
        //    strError = dataAccess.Error;
        //    arrReceiptDetail = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrReceiptDetail.Add(GenerateObjectList(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrReceiptDetail.Add(new ReceiptDetail());

        //    return arrReceiptDetail;
        //}


        //public ArrayList RetrieveListScheduleDetailOPByCriteria2(string strField, string strValue, int distributorID)
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Toko.TokoCode,Toko.TokoName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status from Schedule,ScheduleDetail,Items,OP,Toko where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 1 and OP.CustomerId = Toko.ID and Schedule.Status = 1 and Toko.DistributorID = " + distributorID + " and " + strField + " = '" + strValue + "' order by Schedule.ID desc");
        //    strError = dataAccess.Error;
        //    arrReceiptDetail = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrReceiptDetail.Add(GenerateObjectList(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrReceiptDetail.Add(new ReceiptDetail());

        //    return arrReceiptDetail;
        //}
        //public ArrayList RetrieveListScheduleDetailOPByCriteriaByDepo(string strField, string strValue, int depoID)
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Toko.TokoCode,Toko.TokoName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status from Schedule,ScheduleDetail,Items,OP,Toko where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 1 and OP.CustomerId = Toko.ID and OP.DepoID = " + depoID + " and " + strField + " = '" + strValue + "' order by Schedule.ID desc");
        //    strError = dataAccess.Error;
        //    arrReceiptDetail = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrReceiptDetail.Add(GenerateObjectList(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrReceiptDetail.Add(new ReceiptDetail());

        //    return arrReceiptDetail;
        //}

        //public ArrayList RetrieveListScheduleDetailOPByCriteriaByDepo2(string strField, string strValue, int depoID)
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Toko.TokoCode,Toko.TokoName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status from Schedule,ScheduleDetail,Items,OP,Toko where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 1 and OP.CustomerId = Toko.ID and Schedule.Status = 1 and OP.DepoID = " + depoID + " and " + strField + " = '" + strValue + "' order by Schedule.ID desc");
        //    strError = dataAccess.Error;
        //    arrReceiptDetail = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrReceiptDetail.Add(GenerateObjectList(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrReceiptDetail.Add(new ReceiptDetail());

        //    return arrReceiptDetail;
        //}


        //public ArrayList RetrieveListScheduleDetailOPByCriteriaByDepoCustomer(string strField, string strValue, int depoID)
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,Customer.CustomerCode,Customer.CustomerName,ScheduleDetail.DocumentNo,OP.OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status from Schedule,ScheduleDetail,Items,OP,Customer where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 0 and ScheduleDetail.DocumentNo = OP.OPNo and OP.CustomerType = 2 and OP.CustomerId = Customer.ID and OP.DepoID = " + depoID + " and " + strField + " = '" + strValue + "' order by Schedule.ID desc");
        //    strError = dataAccess.Error;
        //    arrReceiptDetail = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrReceiptDetail.Add(GenerateObjectListCustomer(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrReceiptDetail.Add(new ReceiptDetail());

        //    return arrReceiptDetail;
        //}

        //public ArrayList RetrieveListScheduleDetailTOByCriteria(string strField, string strValue)
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Schedule.ScheduleNo,Schedule.ScheduleDate,ScheduleDetail.DocumentNo,' ' as OpNo2,Items.Description as ItemName,ScheduleDetail.Qty,ScheduleDetail.Status from Schedule,ScheduleDetail,Items where Schedule.ID = ScheduleDetail.ScheduleID and Items.ID = ScheduleDetail.ItemID and ScheduleDetail.TypeDoc = 1 and " + strField + " = '" + strValue + "'");
        //    strError = dataAccess.Error;
        //    arrReceiptDetail = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrReceiptDetail.Add(GenerateObjectList(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrReceiptDetail.Add(new ReceiptDetail());

        //    return arrReceiptDetail;
        //}

        public ArrayList RetrieveByGroupIDwithSUM(int groupID, int itemType, string thbl)
        {
            string strSQL = "select ItemID,SUM(Quantity) as Quantity,GroupID from (select B.ItemID,B.Quantity,B.GroupID from Receipt as A," +
                            "ReceiptDetail as B where A.ID=B.ReceiptID and A.Status>-1  and B.RowStatus>-1 and B.GroupID=" + groupID +
                            " and B.ItemTypeID=" + itemType + " and LEFT(convert(varchar,A.ReceiptDate,112),6) = '" + thbl + "') as P " +
                            "group by GroupID,ItemID order by ItemID";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrReceiptDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceiptDetail.Add(GenerateObjectSUM(sqlDataReader));
                }
            }
            else
                arrReceiptDetail.Add(new ReceiptDetail());

            return arrReceiptDetail;
        }

        public ArrayList RetrieveByGroupIDwithAllSUM(int groupID, int itemType, string thbl)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = " " +
            "select C.ID, case when id >0 then ( " +
            "select isnull(sum(quantity),0) from ( " +
            "SELECT isnull(sum(A.Quantity),0) as quantity FROM ReceiptDetail A INNER JOIN Receipt B ON A.ReceiptID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and  A.ItemID=C.ID and  LEFT(convert(varchar,B.ReceiptDate,112),6)=" + thbl + " " +
            "union " +
            "SELECT isnull(sum(A.Quantity * -1),0) as quantity  FROM pakaiDetail A INNER JOIN pakai B ON A.pakaiID = B.ID " +
            "where  A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and  A.ItemID=C.ID and  LEFT(convert(varchar,B.pakaiDate,112),6)=" + thbl + " " +
            "union " +
            "SELECT isnull(sum(A.Quantity),0) as quantity FROM returpakaiDetail A INNER JOIN returpakai B ON A.returID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and  A.ItemID=C.ID and  LEFT(convert(varchar,B.returDate,112),6)=" + thbl + " " +
            "union " +
            "SELECT isnull(sum(A.Quantity),0) as quantity FROM adjustDetail A INNER JOIN adjust B ON A.adjustID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and  B.AdjustType='tambah' and  A.ItemID=C.ID and  LEFT(convert(varchar,B.adjustDate,112),6)=" + thbl + " " +
            "union " +
            "SELECT isnull(sum(A.Quantity * -1),0) as quantity FROM adjustDetail A INNER JOIN adjust B ON A.adjustID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and B.AdjustType='kurang' and  A.ItemID=C.ID and  LEFT(convert(varchar,B.adjustDate,112),6)=" + thbl +
            " ) as transaksi) end total, " +
            "case when id >0 then ( " +
            "SELECT isnull(avg(A.price),0) as price FROM popurchnDetail A INNER JOIN popurchn B ON A.poID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.Status>=0 and A.ItemID=C.ID and  LEFT(convert(varchar,B.popurchnDate,112),6)=" + thbl + " ) end price from Inventory as C where C.GroupID=" + groupID;

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);

            strError = dataAccess.Error;
            arrReceiptDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceiptDetail.Add(GenerateObjectAllSUM(sqlDataReader));
                }
            }
            else
                arrReceiptDetail.Add(new ReceiptDetail());

            return arrReceiptDetail;
        }

        public ArrayList RetrieveByGroupIDwithAllSUMforRepack(int groupID, int itemType, string thbl)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = " " +
            "select C.ID, case when id >0 then ( " +
            "select isnull(sum(quantity),0) from ( " +
            "SELECT isnull(sum(A.ToQty),0) as quantity FROM Convertan A " +
            "where A.RowStatus>=0 and  A.ToItemID=C.ID and  LEFT(convert(varchar,A.CreatedTime,112),6)=" + thbl + " " +
            "union " +
            "SELECT isnull(sum(A.Quantity * -1),0) as quantity  FROM pakaiDetail A INNER JOIN pakai B ON A.pakaiID = B.ID " +
            "where  A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and  A.ItemID=C.ID and  LEFT(convert(varchar,B.pakaiDate,112),6)=" + thbl + " " +
            "union " +
            "SELECT isnull(sum(A.Quantity),0) as quantity FROM returpakaiDetail A INNER JOIN returpakai B ON A.returID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and  A.ItemID=C.ID and  LEFT(convert(varchar,B.returDate,112),6)=" + thbl + " " +
            "union " +
            "SELECT isnull(sum(A.Quantity),0) as quantity FROM adjustDetail A INNER JOIN adjust B ON A.adjustID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and  B.AdjustType='tambah' and  A.ItemID=C.ID and  LEFT(convert(varchar,B.adjustDate,112),6)=" + thbl + " " +
            "union " +
            "SELECT isnull(sum(A.Quantity * -1),0) as quantity FROM adjustDetail A INNER JOIN adjust B ON A.adjustID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and B.AdjustType='kurang' and  A.ItemID=C.ID and  LEFT(convert(varchar,B.adjustDate,112),6)=" + thbl +
            " ) as transaksi) end total, " +
            "case when id >0 then ( " +
            "SELECT isnull(avg(A.price),0) as price FROM popurchnDetail A INNER JOIN popurchn B ON A.poID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.Status>=0 and A.ItemID=C.ID and  LEFT(convert(varchar,B.popurchnDate,112),6)=" + thbl +
            " ) end price from Inventory as C ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);

            strError = dataAccess.Error;
            arrReceiptDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceiptDetail.Add(GenerateObjectAllSUM(sqlDataReader));
                }
            }
            else
                arrReceiptDetail.Add(new ReceiptDetail());

            return arrReceiptDetail;
        }

        public ArrayList RetrieveByGroupIDwithAllSUMforAsset(int groupID, int itemType, string thbl)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = " " +
            "select C.ID, case when id >0 then ( " +
            "select isnull(sum(quantity),0) from ( " +
            "SELECT isnull(sum(A.Quantity),0) as quantity FROM ReceiptDetail A INNER JOIN Receipt B ON A.ReceiptID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and  A.ItemID=C.ID and  LEFT(convert(varchar,B.ReceiptDate,112),6)=" + thbl + " " +
            "union " +
            "SELECT isnull(sum(A.Quantity * -1),0) as quantity  FROM pakaiDetail A INNER JOIN pakai B ON A.pakaiID = B.ID " +
            "where  A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and  A.ItemID=C.ID and  LEFT(convert(varchar,B.pakaiDate,112),6)=" + thbl + " " +
            "union " +
            "SELECT isnull(sum(A.Quantity),0) as quantity FROM returpakaiDetail A INNER JOIN returpakai B ON A.returID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and  A.ItemID=C.ID and  LEFT(convert(varchar,B.returDate,112),6)=" + thbl + " " +
            "union " +
            "SELECT isnull(sum(A.Quantity),0) as quantity FROM adjustDetail A INNER JOIN adjust B ON A.adjustID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and  B.AdjustType='tambah' and  A.ItemID=C.ID and  LEFT(convert(varchar,B.adjustDate,112),6)=" + thbl + " " +
            "union " +
            "SELECT isnull(sum(A.Quantity * -1),0) as quantity FROM adjustDetail A INNER JOIN adjust B ON A.adjustID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and B.AdjustType='kurang' and  A.ItemID=C.ID and  LEFT(convert(varchar,B.adjustDate,112),6)=" + thbl +
            " ) as transaksi) end total, " +
            "case when id >0 then ( " +
            "SELECT isnull(avg(A.price),0) as price FROM popurchnDetail A INNER JOIN popurchn B ON A.poID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.Status>=0 and A.ItemID=C.ID and  LEFT(convert(varchar,B.popurchnDate,112),6)=" + thbl + " ) end price from Asset as C ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);

            strError = dataAccess.Error;
            arrReceiptDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceiptDetail.Add(GenerateObjectAllSUM(sqlDataReader));
                }
            }
            else
                arrReceiptDetail.Add(new ReceiptDetail());

            return arrReceiptDetail;
        }

        public ArrayList RetrieveByGroupIDwithAllSUMforBiaya(int groupID, int itemType, string thbl)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = " " +
            "select C.ID, case when id >0 then ( " +
            "select isnull(sum(quantity),0) from ( " +
            "SELECT isnull(sum(A.Quantity),0) as quantity FROM ReceiptDetail A INNER JOIN Receipt B ON A.ReceiptID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and  A.ItemID=C.ID and  LEFT(convert(varchar,B.ReceiptDate,112),6)=" + thbl + " " +
            "union " +
            "SELECT isnull(sum(A.Quantity * -1),0) as quantity  FROM pakaiDetail A INNER JOIN pakai B ON A.pakaiID = B.ID " +
            "where  A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and A.ItemID=C.ID and  LEFT(convert(varchar,B.pakaiDate,112),6)=" + thbl + " " +
            "union " +
            "SELECT isnull(sum(A.Quantity),0) as quantity FROM returpakaiDetail A INNER JOIN returpakai B ON A.returID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and  A.ItemID=C.ID and  LEFT(convert(varchar,B.returDate,112),6)=" + thbl + " " +
            "union " +
            "SELECT isnull(sum(A.Quantity),0) as quantity FROM adjustDetail A INNER JOIN adjust B ON A.adjustID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + "  and A.RowStatus>=0  and B.AdjustType='tambah' and  A.ItemID=C.ID and  LEFT(convert(varchar,B.adjustDate,112),6)=" + thbl + " " +
            "union " +
            "SELECT isnull(sum(A.Quantity * -1),0) as quantity FROM adjustDetail A INNER JOIN adjust B ON A.adjustID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + "  and A.RowStatus>=0  and B.AdjustType='kurang' and  A.ItemID=C.ID and  LEFT(convert(varchar,B.adjustDate,112),6)=" + thbl +
            " ) as transaksi) end total, " +
            "case when id >0 then ( " +
            "SELECT isnull(avg(A.price),0) as price FROM popurchnDetail A INNER JOIN popurchn B ON A.poID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.Status>=0 and A.ItemID=C.ID and  LEFT(convert(varchar,B.popurchnDate,112),6)=" + thbl + " ) end price from Biaya as C ";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);

            strError = dataAccess.Error;
            arrReceiptDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceiptDetail.Add(GenerateObjectAllSUM(sqlDataReader));
                }
            }
            else
                arrReceiptDetail.Add(new ReceiptDetail());

            return arrReceiptDetail;
        }

        public ArrayList RetrieveByGroupIDwithAllSUM2(int groupID, int itemType, string thbl)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = " " +
            "select ID,Masuk+Pakai+Retur+AdjIn+AdjOut as total,case when ID>0 then isnull((select top 1 AvgPrice from vw_StockPurchn where YM <='" + thbl + "' and itemid=A1.ID  and AvgPrice>0 order by tanggal desc),0) end price from ( " +
            "select C.ID," +
            "case when C.ID>0 then (SELECT isnull(sum(A.Quantity),0) as quantity FROM ReceiptDetail A INNER JOIN Receipt B ON A.ReceiptID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and  A.ItemID=C.ID and  " +
            "LEFT(convert(varchar,B.ReceiptDate,112),6)='" + thbl + "') else 0 end Masuk," +
            "case when C.ID>0 then (SELECT isnull(sum(A.Quantity * -1),0) as quantity  FROM pakaiDetail A INNER JOIN pakai B ON A.pakaiID = B.ID " +
            "where  A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and  A.ItemID=C.ID and  " +
            "LEFT(convert(varchar,B.pakaiDate,112),6)='" + thbl + "') else 0 end Pakai," +
            "case when C.ID>0 then (SELECT isnull(sum(A.Quantity),0) as quantity FROM returpakaiDetail A INNER JOIN returpakai B ON A.returID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and  A.ItemID=C.ID and  " +
            "LEFT(convert(varchar,B.returDate,112),6)='" + thbl + "') else 0 end Retur," +
            "case when C.ID>0 then (SELECT isnull(sum(A.Quantity),0) as quantity FROM adjustDetail A INNER JOIN adjust B ON A.adjustID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and  B.AdjustType='Tambah' and  A.ItemID=C.ID and  " +
            "LEFT(convert(varchar,B.adjustDate,112),6)='" + thbl + "') else 0 end AdjIn," +
            "case when C.ID>0 then (SELECT isnull(sum(A.Quantity * -1),0) as quantity FROM adjustDetail A INNER JOIN adjust B ON A.adjustID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and B.AdjustType='Kurang' and  A.ItemID=C.ID and  " +
            "LEFT(convert(varchar,B.adjustDate,112),6)='" + thbl + "') else 0 end AdjOut," +
            "case when C.ID >0 then ( SELECT isnull(avg(A.price),0) as price FROM popurchnDetail A INNER JOIN popurchn B ON A.poID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.Status>=0 and A.ItemID=C.ID and LEFT(convert(varchar,B.popurchnDate,112),6)='" + thbl + "' ) " +
            "else 0 end price from Inventory as C where C.GroupID=" + groupID + " AND Aktif=1 ) as A1";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);

            strError = dataAccess.Error;
            arrReceiptDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceiptDetail.Add(GenerateObjectAllSUM(sqlDataReader));
                }
            }
            else
                arrReceiptDetail.Add(new ReceiptDetail());

            return arrReceiptDetail;
        }

        public ArrayList RetrieveByGroupIDwithAllSUM2forRepack(int groupID, int itemType, string thbl)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = " " +
            "select ID,Masuk+Pakai+Retur+AdjIn+AdjOut as total,price from ( " +
            "select C.ID," +
            "case when C.ID>0 then (SELECT isnull(sum(A.ToQty),0) as quantity FROM Convertan A " +
            "where A.RowStatus>=0 and  A.ToItemID=C.ID and  LEFT(convert(varchar,A.CreatedTime,112),6)='" + thbl + "') else 0 end Masuk," +
            "case when C.ID>0 then (SELECT isnull(sum(A.Quantity * -1),0) as quantity  FROM pakaiDetail A INNER JOIN pakai B ON A.pakaiID = B.ID " +
            "where  A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and  A.ItemID=C.ID and  " +
            "LEFT(convert(varchar,B.pakaiDate,112),6)='" + thbl + "') else 0 end Pakai," +
            "case when C.ID>0 then (SELECT isnull(sum(A.Quantity),0) as quantity FROM returpakaiDetail A INNER JOIN returpakai B ON A.returID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and  A.ItemID=C.ID and  " +
            "LEFT(convert(varchar,B.returDate,112),6)='" + thbl + "') else 0 end Retur," +
            "case when C.ID>0 then (SELECT isnull(sum(A.Quantity),0) as quantity FROM adjustDetail A INNER JOIN adjust B ON A.adjustID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and  B.AdjustType='Tambah' and  A.ItemID=C.ID and  " +
            "LEFT(convert(varchar,B.adjustDate,112),6)='" + thbl + "') else 0 end AdjIn," +
            "case when C.ID>0 then (SELECT isnull(sum(A.Quantity * -1),0) as quantity FROM adjustDetail A INNER JOIN adjust B ON A.adjustID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and B.AdjustType='Kurang' and  A.ItemID=C.ID and  " +
            "LEFT(convert(varchar,B.adjustDate,112),6)='" + thbl + "') else 0 end AdjOut," +
            "case when C.ID >0 then ( " +
            "SELECT isnull(avg(A.price),0) as price FROM popurchnDetail A INNER JOIN popurchn B ON A.poID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.Status>=0 and A.ItemID=C.ID and  LEFT(convert(varchar,B.popurchnDate,112),6)='" + thbl + "' ) " +
            "else 0 end price from Inventory as C where C.GroupID=" + groupID + " ) as A1";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);

            strError = dataAccess.Error;
            arrReceiptDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceiptDetail.Add(GenerateObjectAllSUM(sqlDataReader));
                }
            }
            else
                arrReceiptDetail.Add(new ReceiptDetail());

            return arrReceiptDetail;
        }

        public ArrayList RetrieveByGroupIDwithAllSUM2forRepackNonGrc(int groupID, int itemType, string thbl)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = " " +
            "select ID,Masuk+Pakai+Retur+AdjIn+AdjOut as total,price from ( " +
            "select C.ID," +
            "case when C.ID>0 then (SELECT isnull(sum(A.ToQty),0) as quantity FROM Convertan A " +
            "where A.RowStatus>=0 and  A.ToItemID=C.ID and  LEFT(convert(varchar,A.CreatedTime,112),6)='" + thbl + "') else 0 end Masuk," +
            "case when C.ID>0 then (SELECT isnull(sum(A.Quantity * -1),0) as quantity  FROM pakaiDetail A INNER JOIN pakai B ON A.pakaiID = B.ID " +
            "where  A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and  A.ItemID=C.ID and  " +
            "LEFT(convert(varchar,B.pakaiDate,112),6)='" + thbl + "') else 0 end Pakai," +
            "case when C.ID>0 then (SELECT isnull(sum(A.Quantity),0) as quantity FROM returpakaiDetail A INNER JOIN returpakai B ON A.returID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and  A.ItemID=C.ID and  " +
            "LEFT(convert(varchar,B.returDate,112),6)='" + thbl + "') else 0 end Retur," +
            "case when C.ID>0 then (SELECT isnull(sum(A.Quantity),0) as quantity FROM adjustDetail A INNER JOIN adjust B ON A.adjustID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and  B.AdjustType='Tambah' and  A.ItemID=C.ID and  " +
            "LEFT(convert(varchar,B.adjustDate,112),6)='" + thbl + "') else 0 end AdjIn," +
            "case when C.ID>0 then (SELECT isnull(sum(A.Quantity * -1),0) as quantity FROM adjustDetail A INNER JOIN adjust B ON A.adjustID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and B.AdjustType='Kurang' and  A.ItemID=C.ID and  " +
            "LEFT(convert(varchar,B.adjustDate,112),6)='" + thbl + "') else 0 end AdjOut," +
            "case when C.ID >0 then ( " +
            "SELECT isnull(avg(A.price),0) as price FROM popurchnDetail A INNER JOIN popurchn B ON A.poID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.Status>=0 and A.ItemID=C.ID and  LEFT(convert(varchar,B.popurchnDate,112),6)='" + thbl + "' ) " +
            "else 0 end price from Inventory as C where C.GroupID=" + groupID + " ) as A1";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);

            strError = dataAccess.Error;
            arrReceiptDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceiptDetail.Add(GenerateObjectAllSUM(sqlDataReader));
                }
            }
            else
                arrReceiptDetail.Add(new ReceiptDetail());

            return arrReceiptDetail;
        }

        public ArrayList RetrieveByGroupIDwithAllSUM2forNonGrc(int groupID, int itemType, string thbl)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = " " +
            "select ID,Masuk+Pakai+Retur+AdjIn+AdjOut as total,price from ( " +
            "select C.ID," +
            "case when C.ID>0 then (select sum(quantity) quantity from (SELECT isnull(sum(A.Quantity),0) as quantity FROM ReceiptDetail A INNER JOIN Receipt B ON A.ReceiptID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and  A.ItemID=C.ID and  " +
            "LEFT(convert(varchar,B.ReceiptDate,112),6)='" + thbl + "' union all SELECT isnull(sum(A.ToQty),0) as quantity FROM Convertan A " +
            "where A.RowStatus>=0 and  A.ToItemID=C.ID and  LEFT(convert(varchar,A.CreatedTime,112),6)='" + thbl + "')M) else 0 end Masuk," +
            "case when C.ID>0 then (SELECT isnull(sum(A.Quantity * -1),0) as quantity  FROM pakaiDetail A INNER JOIN pakai B ON A.pakaiID = B.ID " +
            "where  A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and  A.ItemID=C.ID and  " +
            "LEFT(convert(varchar,B.pakaiDate,112),6)='" + thbl + "') else 0 end Pakai," +
            "case when C.ID>0 then (SELECT isnull(sum(A.Quantity),0) as quantity FROM returpakaiDetail A INNER JOIN returpakai B ON A.returID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and  A.ItemID=C.ID and  " +
            "LEFT(convert(varchar,B.returDate,112),6)='" + thbl + "') else 0 end Retur," +
            "case when C.ID>0 then (SELECT isnull(sum(A.Quantity),0) as quantity FROM adjustDetail A INNER JOIN adjust B ON A.adjustID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and  B.AdjustType='Tambah' and  A.ItemID=C.ID and  " +
            "LEFT(convert(varchar,B.adjustDate,112),6)='" + thbl + "') else 0 end AdjIn," +
            "case when C.ID>0 then (SELECT isnull(sum(A.Quantity * -1),0) as quantity FROM adjustDetail A INNER JOIN adjust B ON A.adjustID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and B.AdjustType='Kurang' and  A.ItemID=C.ID and  " +
            "LEFT(convert(varchar,B.adjustDate,112),6)='" + thbl + "') else 0 end AdjOut," +
            "case when C.ID >0 then ( " +
            "SELECT isnull(avg(A.price),0) as price FROM popurchnDetail A INNER JOIN popurchn B ON A.poID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.Status>=0 and A.ItemID=C.ID and  LEFT(convert(varchar,B.popurchnDate,112),6)='" + thbl + "' ) " +
            "else 0 end price from Inventory as C where C.GroupID=" + groupID + " ) as A1";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);

            strError = dataAccess.Error;
            arrReceiptDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceiptDetail.Add(GenerateObjectAllSUM(sqlDataReader));
                }
            }
            else
                arrReceiptDetail.Add(new ReceiptDetail());

            return arrReceiptDetail;
        }

        public ArrayList RetrieveByGroupIDwithAllSUM2forAsset(int groupID, int itemType, string thbl)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = " " +
            "select ID,Masuk+Pakai+Retur+AdjIn+AdjOut as total,price from ( " +
            "select C.ID," +
            "case when C.ID>0 then (SELECT isnull(sum(A.Quantity),0) as quantity FROM ReceiptDetail A INNER JOIN Receipt B ON A.ReceiptID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and  A.ItemID=C.ID and  " +
            "LEFT(convert(varchar,B.ReceiptDate,112),6)='" + thbl + "') else 0 end Masuk," +
            "case when C.ID>0 then (SELECT isnull(sum(A.Quantity * -1),0) as quantity  FROM pakaiDetail A INNER JOIN pakai B ON A.pakaiID = B.ID " +
            "where  A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and  A.ItemID=C.ID and  " +
            "LEFT(convert(varchar,B.pakaiDate,112),6)='" + thbl + "') else 0 end Pakai," +
            "case when C.ID>0 then (SELECT isnull(sum(A.Quantity),0) as quantity FROM returpakaiDetail A INNER JOIN returpakai B ON A.returID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and  A.ItemID=C.ID and  " +
            "LEFT(convert(varchar,B.returDate,112),6)='" + thbl + "') else 0 end Retur," +
            "case when C.ID>0 then (SELECT isnull(sum(A.Quantity),0) as quantity FROM adjustDetail A INNER JOIN adjust B ON A.adjustID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and  B.AdjustType='Tambah' and  A.ItemID=C.ID and  " +
            "LEFT(convert(varchar,B.adjustDate,112),6)='" + thbl + "') else 0 end AdjIn," +
            "case when C.ID>0 then (SELECT isnull(sum(A.Quantity * -1),0) as quantity FROM adjustDetail A INNER JOIN adjust B ON A.adjustID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and B.AdjustType='Kurang' and  A.ItemID=C.ID and  " +
            "LEFT(convert(varchar,B.adjustDate,112),6)='" + thbl + "') else 0 end AdjOut," +
            "case when C.ID >0 then ( " +
            "SELECT isnull(avg(A.price),0) as price FROM popurchnDetail A INNER JOIN popurchn B ON A.poID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.Status>=0 and A.ItemID=C.ID and  LEFT(convert(varchar,B.popurchnDate,112),6)='" + thbl + "' ) " +
            "else 0 end price from Asset as C where C.GroupID=" + groupID + " ) as A1";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);

            strError = dataAccess.Error;
            arrReceiptDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceiptDetail.Add(GenerateObjectAllSUM(sqlDataReader));
                }
            }
            else
                arrReceiptDetail.Add(new ReceiptDetail());

            return arrReceiptDetail;

        }

        public ArrayList RetrieveByGroupIDwithAllSUM2forBiaya(int groupID, int itemType, string thbl)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = " " +
            "select ID,Masuk+Pakai+Retur+AdjIn+AdjOut as total,price from ( " +
            "select C.ID," +
            "case when C.ID>0 then (SELECT isnull(sum(A.Quantity),0) as quantity FROM ReceiptDetail A INNER JOIN Receipt B ON A.ReceiptID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and  A.ItemID=C.ID and  " +
            "LEFT(convert(varchar,B.ReceiptDate,112),6)='" + thbl + "') else 0 end Masuk," +
            "case when C.ID>0 then (SELECT isnull(sum(A.Quantity * -1),0) as quantity  FROM pakaiDetail A INNER JOIN pakai B ON A.pakaiID = B.ID " +
            "where  A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and  A.ItemID=C.ID and  " +
            "LEFT(convert(varchar,B.pakaiDate,112),6)='" + thbl + "') else 0 end Pakai," +
            "case when C.ID>0 then (SELECT isnull(sum(A.Quantity),0) as quantity FROM returpakaiDetail A INNER JOIN returpakai B ON A.returID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and  A.ItemID=C.ID and  " +
            "LEFT(convert(varchar,B.returDate,112),6)='" + thbl + "') else 0 end Retur," +
            "case when C.ID>0 then (SELECT isnull(sum(A.Quantity),0) as quantity FROM adjustDetail A INNER JOIN adjust B ON A.adjustID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and  B.AdjustType='Tambah' and  A.ItemID=C.ID and  " +
            "LEFT(convert(varchar,B.adjustDate,112),6)='" + thbl + "') else 0 end AdjIn," +
            "case when C.ID>0 then (SELECT isnull(sum(A.Quantity * -1),0) as quantity FROM adjustDetail A INNER JOIN adjust B ON A.adjustID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.RowStatus>=0 and B.AdjustType='Kurang' and  A.ItemID=C.ID and  " +
            "LEFT(convert(varchar,B.adjustDate,112),6)='" + thbl + "') else 0 end AdjOut," +
            "case when C.ID >0 then ( " +
            "SELECT isnull(avg(A.price),0) as price FROM popurchnDetail A INNER JOIN popurchn B ON A.poID = B.ID " +
            "where A.groupid=" + groupID + " and A.itemtypeid=" + itemType + " and A.Status>=0 and A.ItemID=C.ID and  LEFT(convert(varchar,B.popurchnDate,112),6)='" + thbl + "' ) " +
            "else 0 end price from Biaya as C where C.GroupID=" + groupID + " ) as A1";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);

            strError = dataAccess.Error;
            arrReceiptDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceiptDetail.Add(GenerateObjectAllSUM(sqlDataReader));
                }
            }
            else
                arrReceiptDetail.Add(new ReceiptDetail());

            return arrReceiptDetail;
        }

        public ReceiptDetail GenerateObjectSUM(SqlDataReader sqlDataReader)
        {
            objReceiptDetail = new ReceiptDetail();
            objReceiptDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objReceiptDetail.Quantity = Convert.ToDecimal(sqlDataReader["Quantity"]);
            objReceiptDetail.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            return objReceiptDetail;
        }

        public ReceiptDetail GenerateObjectAllSUM(SqlDataReader sqlDataReader)
        {
            objReceiptDetail = new ReceiptDetail();
            objReceiptDetail.ItemID = Convert.ToInt32(sqlDataReader["ID"]);
            objReceiptDetail.Quantity = Convert.ToDecimal(sqlDataReader["total"]);
            objReceiptDetail.Price = Convert.ToDecimal(sqlDataReader["Price"]);
            return objReceiptDetail;

        }

        public ReceiptDetail GenerateObject(SqlDataReader sqlDataReader)
        {
            objReceiptDetail = new ReceiptDetail();
            objReceiptDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objReceiptDetail.ReceiptID = Convert.ToInt32(sqlDataReader["ReceiptID"]);
            objReceiptDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objReceiptDetail.Quantity = Convert.ToDecimal(sqlDataReader["Quantity"]);
            objReceiptDetail.Price = Convert.ToDecimal(sqlDataReader["Price"]);
            objReceiptDetail.Disc = Convert.ToDecimal(sqlDataReader["Disc"]);
            objReceiptDetail.TotalPrice = Convert.ToDecimal(sqlDataReader["TotalPrice"]);
            objReceiptDetail.PoID = Convert.ToInt32(sqlDataReader["POID"]);
            objReceiptDetail.PoNo = sqlDataReader["PONo"].ToString();
            objReceiptDetail.SppID = Convert.ToInt32(sqlDataReader["SPPID"]);
            objReceiptDetail.SppNo = sqlDataReader["SPPNo"].ToString();
            objReceiptDetail.UomID = Convert.ToInt32(sqlDataReader["UomID"]);
            objReceiptDetail.UOMCode = sqlDataReader["UOMCode"].ToString();
            objReceiptDetail.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objReceiptDetail.ItemCode = sqlDataReader["ItemCode"].ToString();
            objReceiptDetail.ItemName = sqlDataReader["ItemName"].ToString();
            objReceiptDetail.PODetailID = Convert.ToInt32(sqlDataReader["PODetailID"]);
            objReceiptDetail.Kadarair = Convert.ToDecimal(sqlDataReader["KadarAir"]);
            objReceiptDetail.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objReceiptDetail.Keterangan = sqlDataReader["Keterangan"].ToString();
            objReceiptDetail.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"].ToString());
            objReceiptDetail.QtyTimbang = (sqlDataReader["QtyTimbang"] != DBNull.Value) ? Convert.ToDecimal(sqlDataReader["QtyTimbang"].ToString()) : 0;
            objReceiptDetail.TimbanganBPAS = (sqlDataReader["ToAsset"] != DBNull.Value) ? Convert.ToDecimal(sqlDataReader["ToAsset"].ToString()) : 0;
            return objReceiptDetail;
        }

        public ReceiptDetail GenerateObjectNew(SqlDataReader sqlDataReader)
        {
            objReceiptDetail = new ReceiptDetail();
            objReceiptDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objReceiptDetail.ReceiptID = Convert.ToInt32(sqlDataReader["ReceiptID"]);
            objReceiptDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objReceiptDetail.Quantity = Convert.ToDecimal(sqlDataReader["Quantity"]);
            objReceiptDetail.Price = Convert.ToDecimal(sqlDataReader["Price"]);
            objReceiptDetail.Disc = Convert.ToDecimal(sqlDataReader["Disc"]);
            objReceiptDetail.TotalPrice = Convert.ToDecimal(sqlDataReader["TotalPrice"]);
            objReceiptDetail.PoID = Convert.ToInt32(sqlDataReader["POID"]);
            objReceiptDetail.PoNo = sqlDataReader["PONo"].ToString();
            objReceiptDetail.SppID = Convert.ToInt32(sqlDataReader["SPPID"]);
            objReceiptDetail.SppNo = sqlDataReader["SPPNo"].ToString();
            objReceiptDetail.UomID = Convert.ToInt32(sqlDataReader["UomID"]);
            objReceiptDetail.UOMCode = sqlDataReader["UOMCode"].ToString();
            objReceiptDetail.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objReceiptDetail.ItemCode = sqlDataReader["ItemCode"].ToString();
            objReceiptDetail.ItemName = sqlDataReader["ItemName"].ToString();
            objReceiptDetail.PODetailID = Convert.ToInt32(sqlDataReader["PODetailID"]);
            objReceiptDetail.Kadarair = Convert.ToDecimal(sqlDataReader["kadarair"]);
            objReceiptDetail.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objReceiptDetail.Keterangan = sqlDataReader["Keterangan"].ToString();
            objReceiptDetail.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"].ToString());
            objReceiptDetail.ItemID2 = (sqlDataReader["ItemID2"] == DBNull.Value) ? 0 : Convert.ToInt32(sqlDataReader["ItemID2"].ToString());
            return objReceiptDetail;
        }
        public ArrayList RetrieveBySupplierIDwithSUM(string thbl)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from (select SupplierID,SUM(Beli) as TotalPrice " +
                "from (select C.ID as SupplierID, E.ItemID,D.ID as receiptID,E.ID as receiptDetailID," +
                "case when B.Disc>0 then (B.Price*E.Quantity)-((B.Price*E.Quantity)*(B.Disc/100)) " +
                "else B.Price*E.Quantity end Beli " +
                "from POPurchn as A,POPurchnDetail as B, SuppPurch as C,Receipt as D,ReceiptDetail as E " +
                "where A.ID=B.POID and A.Status>-1 and B.Status>-1 and Approval>=3 and A.SupplierID=C.ID and A.ID = D.POID and D.Status>-1 " +
                "and E.PODetailID=B.ID and E.RowStatus>-1 and LEFT(convert(varchar,D.ReceiptDate,112),6) = '" + thbl + "')as A1 group by SupplierID) as A2");

            strError = dataAccess.Error;
            arrReceiptDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrReceiptDetail.Add(GenerateObjectSupplierSUM(sqlDataReader));
                }
            }
            else
                arrReceiptDetail.Add(new ReceiptDetail());

            return arrReceiptDetail;
        }

        public ReceiptDetail GenerateObjectSupplierSUM(SqlDataReader sqlDataReader)
        {
            objReceiptDetail = new ReceiptDetail();
            objReceiptDetail.SupplierID = Convert.ToInt32(sqlDataReader["SupplierID"]);
            objReceiptDetail.TotalPrice = Convert.ToDecimal(sqlDataReader["TotalPrice"]);

            return objReceiptDetail;

        }
        //public ReceiptDetail GenerateObjectList(SqlDataReader sqlDataReader)
        //{
        //    objReceiptDetail = new ReceiptDetail();
        //    objReceiptDetail.ScheduleNo = sqlDataReader["ScheduleNo"].ToString();
        //    objReceiptDetail.ScheduleDate = Convert.ToDateTime(sqlDataReader["ScheduleDate"]);
        //    objReceiptDetail.TokoCode = sqlDataReader["TokoCode"].ToString();
        //    objReceiptDetail.TokoName = sqlDataReader["TokoName"].ToString();
        //    objReceiptDetail.DocumentNo = sqlDataReader["DocumentNo"].ToString();
        //    objReceiptDetail.ItemName = sqlDataReader["ItemName"].ToString();
        //    objReceiptDetail.Qty = Convert.ToInt32(sqlDataReader["Qty"]);
        //    objReceiptDetail.Status = Convert.ToInt32(sqlDataReader["Status"]);
        //    objReceiptDetail.OpNo2 = sqlDataReader["OpNo2"].ToString();
        //    return objReceiptDetail;

        //}

        //public ReceiptDetail GenerateObjectListCustomer(SqlDataReader sqlDataReader)
        //{
        //    objReceiptDetail = new ReceiptDetail();
        //    objReceiptDetail.ScheduleNo = sqlDataReader["ScheduleNo"].ToString();
        //    objReceiptDetail.ScheduleDate = Convert.ToDateTime(sqlDataReader["ScheduleDate"]);
        //    objReceiptDetail.CustomerCode = sqlDataReader["CustomerCode"].ToString();
        //    objReceiptDetail.CustomerName = sqlDataReader["CustomerName"].ToString();
        //    objReceiptDetail.DocumentNo = sqlDataReader["DocumentNo"].ToString();
        //    objReceiptDetail.ItemName = sqlDataReader["ItemName"].ToString();
        //    objReceiptDetail.Qty = Convert.ToInt32(sqlDataReader["Qty"]);
        //    objReceiptDetail.Status = Convert.ToInt32(sqlDataReader["Status"]);
        //    objReceiptDetail.OpNo2 = sqlDataReader["OpNo2"].ToString();
        //    return objReceiptDetail;

        //}
        public string ItemNameFromBiaya(string TableName)
        {
            string strSQL = "CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())< " +
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
        public string ItemIDForBiaya(string TableName)
        {
            return "CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())<= " +
                " (Select CreatedTime from SPP where SPP.ID=" + TableName + ".SPPID)) " +
                " THEN(Select ItemID from SPP where SPP.ID=" + TableName + ".SPPID) ELSE " +
                " (select ID from biaya where ID=" + TableName + ".ItemID and biaya.RowStatus>-1) END ";
        }

        public int GroupIDByItemID(int ItemID, int ItemTypeID)
        {
            string Tabel = string.Empty;
            if (ItemTypeID == 1)
            {
                Tabel = "Inventory";
            }
            else if (ItemTypeID == 2)
            {
                Tabel = "Asset";
            }
            else
            {
                Tabel = "Biaya";
            }
            string strSQL = "Select GroupID from " + Tabel + " where ID=" + ItemID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["GroupID"].ToString());
                }
            }

            return 0;
        }

        public Receipt RetrieveByDetailID(int ID)
        {
            string strSQL = "select r.ReceiptNo,r.ReceiptDate,rd.PONo,rd.ItemID,Quantity,rd.ItemTypeID,rd.QtyTimbang,rd.ToAsset, " +
                          " p.Qty as QtyPO,isnull(r.SupplierId,0)SupplierID, " +
                          " rd.PODetailID as ItemPO, " +
                          " CASE rd.ItemTypeID when 1 then v.ItemCode when 2 then a.ItemCode when 3 then b.ItemCode end ItemCode, " +
                          " CASE rd.ItemTypeID when 1 then (Select ItemCode from Inventory where ID=rd.itemID) " +
                          " when 2 then (Select ItemCode from Asset where ID=rd.itemID) " +
                          " when 3 then (Select ItemCode from Biaya where ID=rd.itemID) end ItemCode2, " +
                          " rd.SPPNo,(select UomCode from UOM where ID=rd.UomID) UOMCode,rd.kadarair,rd.Keterangan,r.CreatedBy, " +
                          " (select SuppPurch.SupplierName from SuppPurch where ID =r.SupplierId) SupplierName " +
                          " From ReceiptDetail as rd  left Join Receipt as r  on r.ID=rd.ReceiptID " +
                          " Left Join PoPurchnDetail as p on p.ID=rd.PODetailID " +
                          " Left Join Inventory as v  on v.ID=p.ItemID  Left Join Asset as a  on a.ID=p.ItemID " +
                          " left Join Biaya as b  on b.ID=p.ItemID " +
                          " where  rd.ID=" + ID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            Receipt objRcp = new Receipt();
            if (sqlDataReader.HasRows && strError == string.Empty)
            {
                while (sqlDataReader.Read())
                {
                    objRcp.ReceiptNo = sqlDataReader["ReceiptNo"].ToString();
                    objRcp.ReceiptDate = Convert.ToDateTime(sqlDataReader["ReceiptDate"].ToString());
                    objRcp.PoNo = sqlDataReader["PONo"].ToString();
                    objRcp.SupplierName = sqlDataReader["SupplierName"].ToString();
                    objRcp.CreatedBy = sqlDataReader["CreatedBy"].ToString();
                    objRcp.ID = Convert.ToInt32(sqlDataReader["ItemID"].ToString());
                    objRcp.ItemCode = sqlDataReader["ItemCode"].ToString();
                    objRcp.SppNo = sqlDataReader["SPPNo"].ToString();
                    objRcp.ApprovalBy = sqlDataReader["UomCode"].ToString();
                    objRcp.KursPajak = Convert.ToDecimal(sqlDataReader["KadarAir"].ToString());
                    objRcp.Keterangan1 = sqlDataReader["Keterangan"].ToString();
                    objRcp.QtyPO = Convert.ToDecimal(sqlDataReader["QtyPO"].ToString());
                    objRcp.Quantity = Convert.ToDecimal(sqlDataReader["Quantity"].ToString());
                    objRcp.PoID = Convert.ToInt32(sqlDataReader["ItemPO"].ToString());
                    objRcp.InvoiceNo = sqlDataReader["ItemCode2"].ToString();
                    objRcp.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"].ToString());
                    objRcp.QtyBPAS = (sqlDataReader["ToAsset"] != DBNull.Value) ? Convert.ToInt32(sqlDataReader["ToAsset"].ToString()) : 0;
                    objRcp.QtyTimbang = (sqlDataReader["QtyTimbang"] != DBNull.Value) ? Convert.ToDecimal(sqlDataReader["QtyTimbang"].ToString()) : 0;
                    objRcp.SupplierID = Convert.ToInt32(sqlDataReader["SupplierID"].ToString());
                    return objRcp;
                }
            }
            return new Receipt();
        }
        public int UnLockReceipt(int ReceiptID)
        {
            string unLock = HttpContext.Current.Session["Lock"].ToString();
            string strSQL = "Update Receipt set status=" + unLock + ",cetak=" + unLock + " where ID=" + ReceiptID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return (strError == string.Empty) ? sqlDataReader.RecordsAffected : 0;
        }
        public string Where { get; set; }
        public string Where1 { get; set; }
        public string Where2 { get; set; }
        public string Where3 { get; set; }
        public Receipt GetReceiptDetail()
        {
            string stdKA = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DefaultKadarAir", "PO");
            stdKA = (stdKA == string.Empty) ? "0" : stdKA;
            Receipt objRcp = new Receipt();
            #region old query - not used
            string strSQL = "select rc.ReceiptNo,rc.ReceiptDate,p.SupplierID,s.SupplierName,rcd.* " +
                            ",ItemCode,ItemName,po.Qty as QtyPO,po.ItemID ItemPO,isnull(ka.Gross,0)Gross, " +
                            "isnull(ka.Netto,0)Netto,isnull(ka.AktualKA,0)AktualKA,isnull(ka.StdKA,0)StdKA " +
                            "FROM ReceiptDetail rcd " +
                            "LEFT JOIN Receipt rc ON rc.ID=rcd.ReceiptID " +
                            "LEFT JOIN POPurchnDetail po ON po.ID=rcd.PODetailID " +
                            "LEFT JOIN POPurchn p ON p.ID=rcd.POID " +
                            "LEFT JOIN SuppPurch s ON s.ID=p.SupplierID " +
                            "LEFT JOIN Inventory iv ON iv.ID=rcd.ItemID " +
                            "INNER JOIN POPurchnKadarAir ka ON ka.POID=rcd.POID " +
                            "WHERE rcd.RowStatus>-1 " + this.Where;
            #endregion
            strSQL = "WITH RMSBeritaAcara AS( " +
                   "SELECT rc.ReceiptNo,rc.ReceiptDate,p.SupplierID,s.SupplierName,rcd.* ,ItemCode,ItemName,po.Qty as QtyPO,po.ItemID ItemPO  " +
                   " FROM ReceiptDetail rcd  " +
                   " LEFT JOIN Receipt rc ON rc.ID=rcd.ReceiptID  " +
                   " LEFT JOIN POPurchnDetail po ON po.ID=rcd.PODetailID  " +
                   " LEFT JOIN POPurchn p ON p.ID=rcd.POID  " +
                   " LEFT JOIN SuppPurch s ON s.ID=p.SupplierID  " +
                   " LEFT JOIN Inventory iv ON iv.ID=rcd.ItemID  " +
                   " WHERE rcd.RowStatus>-1 " + this.Where +
                   " ) " +
                   " SELECT RMS.*,isnull(ka.Gross,0)Gross, isnull(ka.Netto,0)Netto,isnull(ka.AktualKA,0)AktualKA," +
                   " isnull(ka.StdKA," + stdKA + ")StdKA," + this.Where2 + ",dk.JmlBal  " +
                //" isnull(ka.StdKA," + stdKA + ")StdKA,dk.DepoID,dk.DepoName,dk.JmlBal  " +
                   " FROM RMSBeritaAcara rms " +
                //" LEFT JOIN POPurchnKadarAir ka ON ka.POID=rms.POID and ka.PODetailID=rms.PODetailID AND ka.Netto=rms.QtyPO" +
                //" LEFT JOIN DeliveryKertas dk ON dk.ID=ka.SchID "+
                   " LEFT JOIN POPurchnKadarAir ka ON ka.POID=rms.POID and ka.PODetailID=rms.PODetailID " + this.Where3 + "" +
                   " LEFT JOIN " + this.Where1 + " dk ON dk.ID=ka.SchID " +
                   " WHERE ka.RowStatus>-1 AND dk.RowStatus>-1";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    objRcp.ReceiptNo = sqlDataReader["ReceiptNo"].ToString();
                    objRcp.ReceiptDate = Convert.ToDateTime(sqlDataReader["ReceiptDate"].ToString());
                    objRcp.PoNo = sqlDataReader["PONo"].ToString();
                    objRcp.SupplierName = sqlDataReader["SupplierName"].ToString();
                    objRcp.ID = Convert.ToInt32(sqlDataReader["ItemID"].ToString());
                    objRcp.ItemCode = sqlDataReader["ItemCode"].ToString();
                    objRcp.SppNo = sqlDataReader["SPPNo"].ToString();
                    objRcp.KursPajak = Convert.ToDecimal(sqlDataReader["KadarAir"].ToString());
                    objRcp.Keterangan1 = sqlDataReader["Keterangan"].ToString();
                    objRcp.QtyPO = Convert.ToDecimal(sqlDataReader["QtyPO"].ToString());
                    objRcp.Quantity = Convert.ToDecimal(sqlDataReader["Quantity"].ToString());
                    objRcp.PoID = Convert.ToInt32(sqlDataReader["ItemPO"].ToString());
                    objRcp.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"].ToString());
                    objRcp.QtyBPAS = (sqlDataReader["ToAsset"] != DBNull.Value) ? Convert.ToInt32(sqlDataReader["ToAsset"].ToString()) : 0;
                    objRcp.QtyTimbang = (sqlDataReader["QtyTimbang"] != DBNull.Value) ? Convert.ToDecimal(sqlDataReader["QtyTimbang"].ToString()) : 0;
                    objRcp.SupplierID = Convert.ToInt32(sqlDataReader["SupplierID"].ToString());
                    objRcp.KadarAir = Convert.ToDecimal(sqlDataReader["AktualKA"].ToString());
                    objRcp.Gross = Convert.ToDecimal(sqlDataReader["Gross"].ToString());
                    objRcp.Netto = Convert.ToDecimal(sqlDataReader["Netto"].ToString());
                    objRcp.StdKadarAir = Convert.ToDecimal(sqlDataReader["StdKA"].ToString());
                    objRcp.DepoID = Convert.ToInt32(sqlDataReader["DepoID"].ToString());
                    objRcp.JmlBal = Convert.ToDecimal(sqlDataReader["JmlBal"].ToString());
                }
            }
            return objRcp;
        }

        public int UpdateParsialDelivert(int PardeID, int ReceiptID)
        {
            int result = 0;
            string strSQL = "Update Memoharian_Armada set Flag=" + ReceiptID + " Where ID=" + PardeID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            result = sdr.RecordsAffected;
            return result;
        }

        public decimal RetrieveKADepo(int p, int p_2)
        {
            throw new NotImplementedException();
        }

        public decimal RetrieveKADepo(Receipt rms)
        {
            decimal result = 0;
            try
            {
                Receipt rm = (Receipt)rms;
                DepoKertas dk = new DepoKertas();
                string poid = this.GetPOIDonPOKA(rm.PoNo);
                string[] sj = rm.Keterangan1.Split(',');
                string Criteria = " AND SupplierID=" + rm.SupplierID + " AND POKAID=" + poid + " AND GrossDepo=" + rm.Gross + " AND NoSJ='" + sj[1] + "'";
                DeliveryKertas d = dk.Retrieve(Criteria, true);
                result = d.KADepo;
            }
            catch { }
            return result;
        }

        private string GetPOIDonPOKA(string NoPO)
        {
            POPurchnDetailFacade pd = new POPurchnDetailFacade();
            int po = pd.GetKadarAirPOID(NoPO.ToString());
            return po.ToString();
        }

        public int RetrieveSupplierName(string NoReceipt)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = " select SupplierID from BeritaAcaraSupplier_ex where SupplierID in " +
                            " (select SupplierID from Receipt where ID=" + NoReceipt + ")";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["SupplierID"]);
                }
            }
            return 0;
        }
        public int RetrieveSupplierName2(string NoReceipt)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = " select SupplierID from Receipt where ID=" + NoReceipt + "";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["SupplierID"]);
                }
            }
            return 0;
        }
    }
}
