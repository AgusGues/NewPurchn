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
      public class RevisiPOFacade : AbstractFacade
      {
            private RevisiPO objRevPO = new RevisiPO();
            private ArrayList arrRevPO;
            private List<SqlParameter> sqlListParam;

            public RevisiPOFacade(object objDomain)
                : base()
            {
                objRevPO = (RevisiPO)objDomain;
            }
            public RevisiPOFacade()
            {

            }

            public override int Insert(object objDomain)
            {
                try
                {
                    objRevPO = (RevisiPO)objDomain;
                    sqlListParam = new List<SqlParameter>();
                    sqlListParam.Add(new SqlParameter("@POID", objRevPO.POID));
                    sqlListParam.Add(new SqlParameter("@SPPID", objRevPO.SPPID));
                    sqlListParam.Add(new SqlParameter("@GroupID", objRevPO.GroupID));
                    sqlListParam.Add(new SqlParameter("@ItemID", objRevPO.ItemID));
                    sqlListParam.Add(new SqlParameter("@Price", objRevPO.Price));
                    sqlListParam.Add(new SqlParameter("@Qty", objRevPO.Qty));
                    sqlListParam.Add(new SqlParameter("@ItemTypeID", objRevPO.ItemTypeID));
                    sqlListParam.Add(new SqlParameter("@UOMID", objRevPO.UOMID));
                    sqlListParam.Add(new SqlParameter("@Status", objRevPO.Status));
                    sqlListParam.Add(new SqlParameter("@NoUrut", objRevPO.NoUrut));
                    sqlListParam.Add(new SqlParameter("@SPPDetailID", objRevPO.SPPDetailID));
                    sqlListParam.Add(new SqlParameter("@DocumentNo", objRevPO.DocumentNo));
                    sqlListParam.Add(new SqlParameter("@DlvDate", objRevPO.DlvDate));
                    sqlListParam.Add(new SqlParameter("@LastModifiedBy", objRevPO.LastModifiedBy));
                    int intResult = dataAccess.ProcessData(sqlListParam, "spInsertPOPurchnDetail2");

                    strError = dataAccess.Error;

                    return intResult;

                }
                catch (Exception ex)
                {
                    strError = ex.Message;
                    return -1;
                }
            }

            public override int Update(object objDomain)
            {
                try
                {
                    objRevPO = (RevisiPO)objDomain;
                    sqlListParam = new List<SqlParameter>();
                    sqlListParam.Add(new SqlParameter("@ID", objRevPO.ID));
                    //sqlListParam.Add(new SqlParameter("@ItemID", objRevPO.ItemID));
                    //sqlListParam.Add(new SqlParameter("@Qty", objRevPO.Qty));
                    //sqlListParam.Add(new SqlParameter("@QtyReceived", objRevPO.QtyReceived));
                    sqlListParam.Add(new SqlParameter("@Status", objRevPO.Status));
                    sqlListParam.Add(new SqlParameter("@Users", objRevPO.LastModifiedBy));
                    int intResult = dataAccess.ProcessData(sqlListParam, "spUpdatePOPurchnDetail2");
                    strError = dataAccess.Error;
                    return intResult;
                }
                catch (Exception ex)
                {
                    strError = ex.Message;
                    return -1;
                }
            }

          /**
           * Update Qty PO di SPP
           **/

            public int UpdateQtyPOSPP(object objDomain)
            {
                try
                {
                    objRevPO = (RevisiPO)objDomain;
                    sqlListParam = new List<SqlParameter>();
                    sqlListParam.Add(new SqlParameter("@ID", objRevPO.ID));
                    sqlListParam.Add(new SqlParameter("@Jumlah", objRevPO.Jumlah));
                    sqlListParam.Add(new SqlParameter("@Status", objRevPO.Status));
                    int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateSPPafterPOUpdate");
                    strError = dataAccess.Error;                    
                    return intResult;
                } 
                catch(Exception ex)
                {
                    strError=ex.Message;
                    return -1;
                }
            }

            /**
             * Update Header PO
             * tidak semua field yng di update
             */
            public int UpdateHeaderPO(object objDomain)
            {
                try
                {
                    objRevPO = (RevisiPO)objDomain;
                    sqlListParam = new List<SqlParameter>();
                    sqlListParam.Add(new SqlParameter("@ID", objRevPO.ID));
                    sqlListParam.Add(new SqlParameter("@SupplierID", objRevPO.SupplierID));
                    sqlListParam.Add(new SqlParameter("@Termin", objRevPO.Termin));
                    sqlListParam.Add(new SqlParameter("@PPN", objRevPO.PPN));
                    sqlListParam.Add(new SqlParameter("@Delivery", objRevPO.Delivery));
                    sqlListParam.Add(new SqlParameter("@Crc", objRevPO.Crc));
                    sqlListParam.Add(new SqlParameter("@Keterangan", objRevPO.Keterangan));
                    sqlListParam.Add(new SqlParameter("@Status", objRevPO.Status));
                    sqlListParam.Add(new SqlParameter("@Disc", objRevPO.Disc));
                    sqlListParam.Add(new SqlParameter("@PPH", objRevPO.PPH));
                    sqlListParam.Add(new SqlParameter("@NilaiKurs", objRevPO.NilaiKurs));
                    sqlListParam.Add(new SqlParameter("@Ongkos", objRevPO.Ongkos));
                    sqlListParam.Add(new SqlParameter("@remark", objRevPO.Remark));
                    sqlListParam.Add(new SqlParameter("@Approval", objRevPO.Approval));
                    sqlListParam.Add(new SqlParameter("@LastModifiedBy", objRevPO.LastModifiedBy));
                    int intResult = dataAccess.ProcessData(sqlListParam, "spUpdatePOPurchnRev");
                    strError = dataAccess.Error;
                    return intResult;
                }
                catch (Exception ex)
                {
                    strError = ex.Message;
                    return -1;
                }
            }

            public int UpdateTerbilang(object objDomain)
            {
                try
                {
                    objRevPO = (RevisiPO)objDomain;
                    sqlListParam = new List<SqlParameter>();
                    sqlListParam.Add(new SqlParameter("@ID", objRevPO.ID));
                    sqlListParam.Add(new SqlParameter("@terbilang", objRevPO.Terbilang));
                    int intResult = dataAccess.ProcessData(sqlListParam, "spUpdatePOPurchn3");
                    strError = dataAccess.Error;
                    return intResult;
                }
                catch (Exception ex)
                {
                    strError = ex.Message;
                    return -1;
                }
            }
            public override int Delete(object objDomain)
            {
                try
                {
                    objRevPO = (RevisiPO)objDomain;
                    sqlListParam = new List<SqlParameter>();

                    sqlListParam.Add(new SqlParameter("@POID", objRevPO.POID));
                    sqlListParam.Add(new SqlParameter("@LastModif", objRevPO.LastModifiedBy));
                    sqlListParam.Add(new SqlParameter("@Keterangan", objRevPO.Keterangan));

                    int intResult = dataAccess.ProcessData(sqlListParam, "spDeletePOPurchn");

                    strError = dataAccess.Error;

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
                throw new NotImplementedException();
            }

            public RevisiPO RetrieveByNo(string NoPO)
            {
                DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                string strSQL ="SELECT * FROM POPurchn where NoPO='" + NoPO + "'";
                SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
                strError = dataAccess.Error;
                arrRevPO = new ArrayList();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        return GenerateObject(sqlDataReader);
                    }
                }

                return new RevisiPO();
            }
            public ArrayList RetrievePODetail(int POID, string Users, int ItemTypeID)
            {

                string All = (Users == "admin" || Users == "Admin") ? " " : " and POPurchnDetail.Status >-1";
                DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                string strSQL = "SELECT POPurchnDetail.*, " +
                    "CASE POPurchnDetail.ItemTypeID " +
                    "    WHEN 1 THEN (Select ItemCode from Inventory where Inventory.ID=POPurchnDetail.itemID)" +
                    "    WHEN 2 THEN (select ItemCode from Asset where Asset.ID=POPurchnDetail.ItemID)" +
                    "    WHEN 3 THEN (select ItemCode from Biaya where Biaya.ID=POPurchnDetail.ItemID)" +
	                "    ELSE '' END as ItemCode, "+
                    "CASE POPurchnDetail.ItemTypeID " +
                    "    WHEN 1 THEN (Select ItemName from Inventory where Inventory.ID=POPurchnDetail.itemID) " +
                    "    WHEN 2 THEN (select ItemName from Asset where Asset.ID=POPurchnDetail.ItemID) " +
	                "    WHEN 3 THEN "+ItemSPPBiayaNew() +
	                "    ELSE '' END as ItemName, "+
                    "    s.UomDesc,'' as GroupName " +
                    "FROM POPurchnDetail , Uom as s where  s.ID=POPurchnDetail.UOMID "+
                    "/*and POPurchnDetail.Status >-1 and POPurchnDetail.ItemTypeID="+ItemTypeID +
                    " */ and POPurchnDetail.POID='" + POID + "' " + All + " order by POPurchnDetail.ID";
                SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
                strError = dataAccess.Error;
                arrRevPO = new ArrayList();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        arrRevPO.Add(GenerateObjectViewPO(sqlDataReader));
                    }
                }
                else
                    arrRevPO.Add(new RevisiPO());

                return arrRevPO;
            }

            public RevisiPO RetrieveDetailID(int ID, int ItemType)
            {
                DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                string strSQL = string.Empty;
                if (ItemType == 1)
                {
                     strSQL = "SELECT p.*,v.ItemCode,v.ItemName,v.ItemName as GroupName,v.UOMID,s.UomDesc FROM POPurchnDetail as p,inventory as v, Uom as s where v.ID=p.itemID and s.ID=p.UOMID and p.ID='" + ID + "' order by p.ID";
                }
                else if (ItemType == 3)
                {
                     //strSQL = "SELECT p.*,v.ItemCode,v.ItemName,v.UOMID,s.UomDesc FROM POPurchnDetail as p,biaya as v, Uom as s where v.ID=p.itemID and s.ID=p.UOMID and p.ID='" + ID + "' order by p.ID";
                     strSQL = "SELECT p.*,v.ItemCode,v.ItemName as GroupName, " +
                            "CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())<  " +
                            "(Select CreatedTime from SPP where SPP.ID=p.SPPID))  THEN " +
                            "v.ItemName+' - '+ (Select SPPDetail.Keterangan From SPPDetail where SPPDetail.ID=p.SppDetailID) ELSE  " +
                            "v.ItemName END ItemName, v.UOMID,s.UomDesc FROM POPurchnDetail as p,biaya as v, Uom as s where v.ID=p.itemID " +
                            "and s.ID=p.UOMID and p.ID='" + ID + "'  order by p.ID";
                }
                else if (ItemType == 2)
                {
                    strSQL = "SELECT p.*,v.ItemCode,v.ItemName,v.ItemName as GroupName,v.UOMID,s.UomDesc FROM POPurchnDetail as p,asset as v, Uom as s where v.ID=p.itemID and s.ID=p.UOMID and p.ID='" + ID + "' order by p.ID";
                }

                SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
                strError = dataAccess.Error;
                arrRevPO = new ArrayList();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        return GenerateObjectViewPO(sqlDataReader);
                    }
                }

                return new RevisiPO();
            }
        /**
          * added on 28-04-2014
          * untuk perubahan pada itemname table biaya
          * dan stock per itemnya
          */
            public decimal GetSPPQtyPO(int SPPDetailID,string Field)
            {
                string strSQL = "Select "+Field+" from SPPDetail where ID=" + SPPDetailID;
                DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
                strError = dataAccess.Error;
                arrRevPO = new ArrayList();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        return Convert.ToDecimal(sqlDataReader[Field].ToString());
                    }
                }
                return 0;

            }
        public string ItemSPPBiayaNew()
        {
            string strSQL = "CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())< " +
                " (Select CreatedTime from SPP where SPP.ID=POPurchnDetail.SPPID)) " +
                " THEN(select ItemName from Biaya where ID=POPurchnDetail.ItemID and Biaya.RowStatus>-1)+' - '+ " +
                " (Select SPPDetail.Keterangan From SPPDetail where SPPDetail.ID=POPurchnDetail.SppDetailID) ELSE " +
                " (select ItemName from biaya where ID=POPurchnDetail.ItemID and biaya.RowStatus>-1) END ";
            return strSQL;
        }

        public string StockBiayaNew()
        {
            string strSQL = "CASE WHEN (isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())< " +
                " (Select CreatedTime from SPP where SPP.ID=POPurchnDetail.SPPID)) THEN " +
                " (SELECT isnull(sum(jumlah),0) from Biaya where ItemName=(Select SPPDetail.Keterangan From SPPDetail where SPPDetail.ID=POPurchnDetail.SppDetailID)) " +
                " ELSE (SELECT isnull(sum(Jumlah),0) from Biaya where ID=POPurchnDetail.ItemID and Biaya.RowStatus>-1) END";
            return strSQL;
        }
        public RevisiPO GenerateObject(SqlDataReader sqlDataReader)
        {
            objRevPO = new RevisiPO();
            objRevPO.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objRevPO.NoPO = sqlDataReader["NoPO"].ToString();
            objRevPO.POPurchnDate = Convert.ToDateTime(sqlDataReader["POPurchnDate"]);
            objRevPO.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objRevPO.SupplierID = Convert.ToInt32(sqlDataReader["SupplierID"]);
            objRevPO.Termin = sqlDataReader["Termin"].ToString();
            objRevPO.PPN = Convert.ToDecimal(sqlDataReader["PPN"]);
            objRevPO.Delivery = sqlDataReader["Delivery"].ToString();
            objRevPO.Crc = Convert.ToInt32(sqlDataReader["Crc"]);
            objRevPO.Keterangan = sqlDataReader["Keterangan"].ToString();
            objRevPO.Terbilang = sqlDataReader["Terbilang"].ToString();
            objRevPO.Disc = Convert.ToDecimal(sqlDataReader["Disc"]);
            objRevPO.PPH = Convert.ToDecimal(sqlDataReader["PPH"]);
            objRevPO.NilaiKurs = Convert.ToInt32(sqlDataReader["NilaiKurs"]);
            objRevPO.Cetak = Convert.ToInt32(sqlDataReader["Cetak"]);
            objRevPO.CountPrt = Convert.ToInt32(sqlDataReader["CountPrt"]);
            objRevPO.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objRevPO.Ongkos = Convert.ToInt32(sqlDataReader["Ongkos"]);
            objRevPO.Approval = Convert.ToInt32(sqlDataReader["Approval"]);
            objRevPO.ApproveDate1 = Convert.ToDateTime(sqlDataReader["ApproveDate1"]);
            objRevPO.ApproveDate2 = Convert.ToDateTime(sqlDataReader["ApproveDate2"]);
            objRevPO.AlasanBatal = sqlDataReader["AlasanBatal"].ToString();
            objRevPO.AlasanClose = sqlDataReader["AlasanClose"].ToString();
            objRevPO.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objRevPO.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objRevPO.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objRevPO.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            objRevPO.Indent = sqlDataReader["Indent"].ToString();
            objRevPO.Remark = sqlDataReader["Remark"].ToString();
            return objRevPO;

        }
        public RevisiPO GenerateObjectViewPO(SqlDataReader sqlDataReader)
        {
            objRevPO = new RevisiPO();
            objRevPO.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objRevPO.POID = Convert.ToInt32(sqlDataReader["POID"]);
            objRevPO.Satuan = sqlDataReader["UOMDesc"].ToString();
            objRevPO.SPPID = Convert.ToInt32(sqlDataReader["SPPID"]);
            objRevPO.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objRevPO.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objRevPO.Price = Convert.ToDecimal(sqlDataReader["Price"]);
            objRevPO.Qty = Convert.ToDecimal(sqlDataReader["Qty"]);
            objRevPO.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"]);
            objRevPO.UOMID = Convert.ToInt32(sqlDataReader["UOMID"]);
            objRevPO.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objRevPO.NoUrut = Convert.ToInt32(sqlDataReader["NoUrut"]);
            objRevPO.SPPDetailID = Convert.ToInt32(sqlDataReader["SPPDetailID"]);
            objRevPO.DocumentNo = sqlDataReader["DocumentNo"].ToString();
            objRevPO.NamaBarang = sqlDataReader["ItemName"].ToString();
            objRevPO.ItemCode = sqlDataReader["ItemCode"].ToString();
            objRevPO.DlvDate = Convert.ToDateTime(sqlDataReader["dlvdate"]);
            if(!Convert.IsDBNull(sqlDataReader["LastModifiedTime"])){
            objRevPO.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);}
            objRevPO.GroupName = (Convert.IsDBNull(sqlDataReader["GroupName"].ToString()) == true) ? string.Empty : 
                                sqlDataReader["GroupName"].ToString();

            return objRevPO;

        }
        public ArrayList RetrieveSPPItem(int SPPID)
        {
            string strsql = "select A.ID,A.SPPID,A.GroupID,A.ItemTypeID,A.UOMID,A.ItemID,A.Quantity,C.UOMCode as Satuan,A.QtyPO,A.Status,A.Keterangan, " +
                              "case A.ItemTypeID when 1  then (select ItemName from Inventory where ID=A.ItemID and RowStatus > -1) " +
                              "when 2 then (select ItemName from Asset where ID=A.ItemID and RowStatus > -1) " +
                              "else " + ItemSPPBiayaNew() + " end ItemName, " +
                              "case A.ItemTypeID when 1  then (select ItemCode from Inventory where ID=A.ItemID and RowStatus > -1) " +
                              "when 2 then (select ItemCode from Asset where ID=A.ItemID and RowStatus > -1) " +
                              "else (select ItemCode from Biaya where ID=A.ItemID and RowStatus > -1) end ItemCode,A.tglkirim " +
                              "from SPPDetail as A,UOM as C where A.UOMID = C.ID and A.Status>-1 and A.SPPID = " + SPPID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrRevPO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrRevPO.Add(GenerateObjectViewPO(sqlDataReader));
                }
            }
            else
                arrRevPO.Add(new RevisiPO());

            return arrRevPO;
        }

        public decimal CheckReceiptPO(int PODetailID)
        {
            string strSQL = "Select isnull(SUM(Quantity),0) as Qty from ReceiptDetail where RowStatus >-1 and PODetailID=" + PODetailID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (strError == string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToDecimal(sqlDataReader["Qty"].ToString());
                }
            }
            return 0;
        }
          //end of public class RevisiPOFacade
        public int CheckPOReceipt(string POID)
        {
            int result = 0;
            string strSql = "select COUNT(ID) as Jml from ReceiptDetail where RowStatus>-1 and POID =" + POID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSql);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToInt32(sdr["Jml"].ToString());
                }
            }
            return result;
        }
        public ArrayList DetailPO(string POID)
        {
            arrRevPO = new ArrayList();
            string strSQL = "Select * from POPurchnDetail where Status>-1 and POID=" + POID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrRevPO.Add(new RevisiPO
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString())
                    });
                }
            }
            return arrRevPO;
        }
      }

    //end of namespace
}
