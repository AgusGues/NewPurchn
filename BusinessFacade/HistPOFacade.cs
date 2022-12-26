using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;
using Dapper;

namespace BusinessFacade
{
    public class HistPOFacade : AbstractTransactionFacade
    {
        private HistPO objHistPO = new HistPO();
        private ArrayList arrHistPO;
       // private List<SqlParameter> sqlListParam;

        public HistPOFacade(object objDomain)
            : base(objDomain)
        {
            objHistPO = (HistPO)objDomain;
        }

        public HistPOFacade()
        {

        }

        public override int Insert(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Update(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Delete(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override System.Collections.ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }

        public ArrayList ViewHistPO(string strfield, string strvalue, string k)
        {
            string strField = string.Empty;
            if (strfield == "3")
            {
                strField = 
                "popurchndetail.itemtypeid=3 and (POPurchnDetail.SppDetailID in(SELECT SPPDetail.ID from SPPDetail where SPPDetail.Keterangan like '%"+ strvalue +"%')"+
                "or popurchndetail.itemid in (select id from biaya where itemname like '%" + strvalue + "%') ";
            }
            else
            {
                strField=strfield+" '%" + strvalue + "%' ";
            }
            string strsql = "SELECT   POPurchn.POPurchnDate, POPurchn.NoPO, SPP.NoSPP, " +
            "CASE POPurchnDetail.ItemTypeID  " +
            "when 1 then (select ItemCode  from Inventory where ID =POPurchnDetail.ItemID ) " +
            "when 2 then (select ItemCode  from asset where ID =POPurchnDetail.ItemID ) " +
            "when 3 then (select ItemCode  from biaya where ID =POPurchnDetail.ItemID ) end ItemCode, " +
            "case POPurchnDetail.ItemTypeID  " +
            "when 1 then (select Itemname  from Inventory where ID =POPurchnDetail.ItemID ) " +
            "when 2 then (select Itemname  from asset where ID =POPurchnDetail.ItemID ) " +
            "when 3 then "+ItemSPPBiayaNew()+" end ItemName, " +
            "CASE WHEN POPurchnDetail.UOMID > 0 THEN " +
            "(SELECT uomcode FROM UOM WHERE ID = POPurchnDetail.UOMID) END AS Satuan, "+
            " POPurchnDetail.Price as Price,  " +
            "case when POPurchn.Crc >0 then (select nama from MataUang where ID=POPurchn.Crc) end CRC, " +
            "POPurchnDetail.Qty, SuppPurch.SupplierName, SuppPurch.Telepon,POPurchn.Termin,isnull(POPurchnDetail.DlvDate,'1/1/1900') as DlvDate,  " +
            "POPurchn.Delivery ,POPurchn.Disc,POPurchn.PPN, " +
            "case POPurchn.Approval  " +
            "when 0 then 'Open' " +
            "when 1 then 'Head' " +
            "when 2 then 'Manager' " +
            "when 3 then 'Purchasing' " +
            "end Approval " +
            "FROM POPurchn INNER JOIN " +
            "POPurchnDetail ON POPurchn.ID = POPurchnDetail.POID INNER JOIN " +
            "SuppPurch ON POPurchn.SupplierID = SuppPurch.ID INNER JOIN " +
            "SPP ON POPurchnDetail.SPPID = SPP.ID where " + strField +" "+  k +
            " and popurchn.status>-1 and popurchndetail.status>-1  ORDER BY POPurchn.POPurchnDate desc,POPurchn.NoPO";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);

            strError = dataAccess.Error;
            arrHistPO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrHistPO.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrHistPO.Add(new HistPO());

            return arrHistPO;
        }

        public string ViewHistPORpt(string strfield, string strvalue, string k)
        {
            string strField = string.Empty;
            if (strfield == "3")
            {
                strField =
                "popurchndetail.itemtypeid=3 and (POPurchnDetail.SppDetailID in(SELECT SPPDetail.ID from SPPDetail where SPPDetail.Keterangan like '%" + strvalue + "%')" +
                "or popurchndetail.itemid in (select id from biaya where itemname like '%" + strvalue + "%') ";
            }
            else
            {
                strField = strfield + " '%" + strvalue + "%' ";
            }
            string strSQL = "select * from (SELECT  top 200   POPurchnDetail.ID, POPurchn.POPurchnDate, POPurchn.NoPO, SPP.NoSPP, " +
            "CASE POPurchnDetail.ItemTypeID  " +
            "when 1 then (select ItemCode  from Inventory where ID =POPurchnDetail.ItemID ) " +
            "when 2 then (select ItemCode  from asset where ID =POPurchnDetail.ItemID ) " +
            "when 3 then (select ItemCode  from biaya where ID =POPurchnDetail.ItemID ) end ItemCode, " +
            "case POPurchnDetail.ItemTypeID  " +
            "when 1 then (select Itemname  from Inventory where ID =POPurchnDetail.ItemID ) " +
            "when 2 then (select Itemname  from asset where ID =POPurchnDetail.ItemID ) " +
            "when 3 then " + ItemSPPBiayaNew() + " end ItemName, " +
            "CASE WHEN POPurchnDetail.UOMID > 0 THEN " +
            "(SELECT uomcode FROM UOM WHERE ID = POPurchnDetail.UOMID) END AS Satuan, case when (select isnull(harga,0) from Inventory where ID=POPurchnDetail.ItemID)=0 then POPurchnDetail.Price else 0 end Price,  " +
            "case when POPurchn.Crc >0 then (select nama from MataUang where ID=POPurchn.Crc) end CRC, " +
            "POPurchnDetail.Qty, SuppPurch.SupplierName, SuppPurch.Telepon,POPurchn.Termin,isnull(POPurchnDetail.DlvDate,'1/1/1900') as DlvDate,  " +
            "POPurchn.Delivery ,POPurchn.Disc,POPurchn.PPN, " +
            "case POPurchn.Approval  " +
            "when 0 then 'Open' " +
            "when 1 then 'Head' " +
            "when 2 then 'Manager' " +
            "when 3 then 'Purchasing' " +
            "end Approval " +
            "FROM POPurchn INNER JOIN " +
            "POPurchnDetail ON POPurchn.ID = POPurchnDetail.POID INNER JOIN " +
            "SuppPurch ON POPurchn.SupplierID = SuppPurch.ID INNER JOIN " +
            "SPP ON POPurchnDetail.SPPID = SPP.ID where " + strField+" " + k +
            " and popurchn.status>-1 and popurchndetail.status>-1 ) as A1 left join  " +
            "(SELECT RD.PODetailID,  R.ReceiptNo, R.ReceiptDate, RD.Quantity  " +
            "FROM   Receipt AS R INNER JOIN ReceiptDetail AS RD ON R.ID = RD.ReceiptID where R.Status>=0 and RD.RowStatus>=0) A2   " +
            "on A1.ID =A2.PODetailID";
            return strSQL;
        }

        public ArrayList ViewHistPO2(string strfield, string strvalue, string k)
        {
            string strField = string.Empty;
            string inv = string.Empty;
            if (strfield == "1")
            {
                inv = "popurchndetail.itemtypeid=1 and popurchndetail.itemid in (select id from inventory where itemname like";
            }
            else if (strfield == "2")
            {
                inv = "popurchndetail.itemtypeid=2 and popurchndetail.itemid in (select id from asset where itemname like";
            }
            else if (strfield == "3")
            {
                inv = "popurchndetail.itemtypeid=3 and popurchndetail.itemid in (select id from biaya where itemname like";
            }
            else
            {
                inv = strfield;
            }
            if (strfield == "3")
            {
                strField =
                "popurchndetail.itemtypeid=3 and (POPurchnDetail.SppDetailID in(SELECT SPPDetail.ID from SPPDetail where SPPDetail.Keterangan like '%" + strvalue + "%')" +
                "or popurchndetail.itemid in (select id from biaya where itemname like '%" + strvalue + "%') ";
            }
            else
            {
                strField = inv + " '%" + strvalue + "%' ";
            }
            string strsql = "SELECT  Top 100 POPurchn.POPurchnDate, POPurchn.NoPO, SPP.NoSPP, " +
            "CASE POPurchnDetail.ItemTypeID  " +
            "when 1 then (select ItemCode  from Inventory where ID =POPurchnDetail.ItemID ) " +
            "when 2 then (select ItemCode  from asset where ID =POPurchnDetail.ItemID ) " +
            "when 3 then (select ItemCode  from biaya where ID =POPurchnDetail.ItemID ) end ItemCode, " +
            "case POPurchnDetail.ItemTypeID  " +
            "when 1 then (select Itemname  from Inventory where ID =POPurchnDetail.ItemID ) " +
            "when 2 then (select Itemname  from asset where ID =POPurchnDetail.ItemID ) " +
            "when 3 then " + ItemSPPBiayaNew() + " end ItemName, " +
            "CASE WHEN POPurchnDetail.UOMID > 0 THEN " +
            "(SELECT uomcode FROM UOM WHERE ID = POPurchnDetail.UOMID) END AS Satuan, POPurchnDetail.Price,  " +
            "case when POPurchn.Crc >0 then (select nama from MataUang where ID=POPurchn.Crc) end CRC, " +
            "POPurchnDetail.Qty, SuppPurch.SupplierName, SuppPurch.Telepon,POPurchn.Termin,isnull(POPurchnDetail.DlvDate,'1/1/1900') as DlvDate,  " +
            "POPurchn.Delivery ,POPurchn.Disc,POPurchn.PPN, " +
            "case POPurchn.Approval  " +
            "when 0 then 'Open' " +
            "when 1 then 'Head' " +
            "when 2 then 'Manager' " +
            "when 3 then 'Purchasing' " +
            "end Approval " +
            "FROM POPurchn INNER JOIN " +
            "POPurchnDetail ON POPurchn.ID = POPurchnDetail.POID INNER JOIN " +
            "SuppPurch ON POPurchn.SupplierID = SuppPurch.ID INNER JOIN " +
            "SPP ON POPurchnDetail.SPPID = SPP.ID where " + strField + " " + k +
            " and popurchn.status>-1 and popurchndetail.status>-1  ORDER BY POPurchn.POPurchnDate desc,POPurchn.NoPO";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);

            strError = dataAccess.Error;
            arrHistPO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrHistPO.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrHistPO.Add(new HistPO());

            return arrHistPO;
        }

        public string ViewHistPORpt2(string strfield, string strvalue, string k)
        {
            string strField = string.Empty;
            if (strfield == "3")
            {
                strField =
                "popurchndetail.itemtypeid=3 and (POPurchnDetail.SppDetailID in(SELECT SPPDetail.ID from SPPDetail where SPPDetail.Keterangan like '%" + strvalue + "%')" +
                "or popurchndetail.itemid in (select id from biaya where itemname like '%" + strvalue + "%') ";
            }
            else
            {
                strField = strfield + " '%" + strvalue + "%' ";
            }
            string strSQL = "select * from (SELECT  top 200   POPurchnDetail.ID, POPurchn.POPurchnDate, POPurchn.NoPO, SPP.NoSPP, " +
            "CASE POPurchnDetail.ItemTypeID  " +
            "when 1 then (select ItemCode  from Inventory where ID =POPurchnDetail.ItemID ) " +
            "when 2 then (select ItemCode  from asset where ID =POPurchnDetail.ItemID ) " +
            "when 3 then (select ItemCode  from biaya where ID =POPurchnDetail.ItemID ) end ItemCode, " +
            "case POPurchnDetail.ItemTypeID  " +
            "when 1 then (select Itemname  from Inventory where ID =POPurchnDetail.ItemID ) " +
            "when 2 then (select Itemname  from asset where ID =POPurchnDetail.ItemID ) " +
            "when 3 then " + ItemSPPBiayaNew() + " end ItemName, " +
            "CASE WHEN POPurchnDetail.UOMID > 0 THEN " +
            "(SELECT uomcode FROM UOM WHERE ID = POPurchnDetail.UOMID) END AS Satuan, POPurchnDetail.Price,  " +
            "case when POPurchn.Crc >0 then (select nama from MataUang where ID=POPurchn.Crc) end CRC, " +
            "POPurchnDetail.Qty, SuppPurch.SupplierName, SuppPurch.Telepon,POPurchn.Termin,isnull(POPurchnDetail.DlvDate,'1/1/1900') as DlvDate,  " +
            "POPurchn.Delivery ,POPurchn.Disc,POPurchn.PPN, " +
            "case POPurchn.Approval  " +
            "when 0 then 'Open' " +
            "when 1 then 'Head' " +
            "when 2 then 'Manager' " +
            "when 3 then 'Purchasing' " +
            "end Approval " +
            "FROM POPurchn INNER JOIN " +
            "POPurchnDetail ON POPurchn.ID = POPurchnDetail.POID INNER JOIN " +
            "SuppPurch ON POPurchn.SupplierID = SuppPurch.ID INNER JOIN " +
            "SPP ON POPurchnDetail.SPPID = SPP.ID where " + strField + " " + k +
            " and popurchn.status>-1 and popurchndetail.status>-1 ) as A1 left join  " +
            "(SELECT RD.PODetailID,  R.ReceiptNo, R.ReceiptDate, RD.Quantity  " +
            "FROM   Receipt AS R INNER JOIN ReceiptDetail AS RD ON R.ID = RD.ReceiptID where R.Status>=0 and RD.RowStatus>=0) A2   " +
            "on A1.ID =A2.PODetailID";
            return strSQL;
        }

        public ArrayList ViewHistPOByPrice0(string strfield, string strvalue, string k)
        {
            string strField = string.Empty;
            if (strfield == "3")
            {
                strField =
                "popurchndetail.itemtypeid=3 and (POPurchnDetail.SppDetailID in(SELECT SPPDetail.ID from SPPDetail where SPPDetail.Keterangan like '%" + strvalue + "%')" +
                "or popurchndetail.itemid in (select id from biaya where itemname like '%" + strvalue + "%') ";
            }
            else
            {
                strField = strfield + " '%" + strvalue + "%' ";
            }
            string strsql = "SELECT  top 200   POPurchn.POPurchnDate, POPurchn.NoPO, SPP.NoSPP, " +
            "CASE POPurchnDetail.ItemTypeID  " +
            "when 1 then (select ItemCode  from Inventory where ID =POPurchnDetail.ItemID ) " +
            "when 2 then (select ItemCode  from asset where ID =POPurchnDetail.ItemID ) " +
            "when 3 then (select ItemCode  from biaya where ID =POPurchnDetail.ItemID ) end ItemCode, " +
            "case POPurchnDetail.ItemTypeID  " +
            "when 1 then (select Itemname  from Inventory where ID =POPurchnDetail.ItemID ) " +
            "when 2 then (select Itemname  from asset where ID =POPurchnDetail.ItemID ) " +
            "when 3 then " + ItemSPPBiayaNew() + " end ItemName, " +
            "CASE WHEN POPurchnDetail.UOMID > 0 THEN " +
            "(SELECT uomcode FROM UOM WHERE ID = POPurchnDetail.UOMID) END AS Satuan, 0 as Price,  " +
            "case when POPurchn.Crc >0 then (select nama from MataUang where ID=POPurchn.Crc) end CRC, " +
            "POPurchnDetail.Qty, SuppPurch.SupplierName, SuppPurch.Telepon,POPurchn.Termin, isnull(POPurchnDetail.DlvDate,'1/1/1900') as DlvDate, " +
            "POPurchn.Delivery ,POPurchn.Disc,POPurchn.PPN, " +
            "case POPurchn.Approval  " +
            "when 0 then 'Open' " +
            "when 1 then 'Head' " +
            "when 2 then 'Manager' " +
            "when 3 then 'Purchasing' " +
            "end Approval " +
            "FROM POPurchn INNER JOIN " +
            "POPurchnDetail ON POPurchn.ID = POPurchnDetail.POID INNER JOIN " +
            "SuppPurch ON POPurchn.SupplierID = SuppPurch.ID INNER JOIN " +
            "SPP ON POPurchnDetail.SPPID = SPP.ID where " + strField + " " + k +
            " and popurchn.status>-1 and popurchndetail.status>-1  ORDER BY POPurchn.POPurchnDate desc,POPurchn.NoPO";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql );

            strError = dataAccess.Error;
            arrHistPO = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrHistPO.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrHistPO.Add(new HistPO());

            return arrHistPO;
        }
        public string  ViewHistPOByPrice0Rpt(string strfield, string strvalue, string k)
        {
            string strField = string.Empty;
            if (strfield == "3")
            {
                strField =
                "popurchndetail.itemtypeid=3 and (POPurchnDetail.SppDetailID in(SELECT SPPDetail.ID from SPPDetail where SPPDetail.Keterangan like '%" + strvalue + "%')" +
                "or popurchndetail.itemid in (select id from biaya where itemname like '%" + strvalue + "%') ";
            }
            else
            {
                strField = strfield + " '%" + strvalue + "%' ";
            }
            string strSQL = "select * from (SELECT  top 200   POPurchnDetail.ID, POPurchn.POPurchnDate, POPurchn.NoPO, SPP.NoSPP, " +
            "CASE POPurchnDetail.ItemTypeID  " +
            "when 1 then (select ItemCode  from Inventory where ID =POPurchnDetail.ItemID ) " +
            "when 2 then (select ItemCode  from asset where ID =POPurchnDetail.ItemID ) " +
            "when 3 then (select ItemCode  from biaya where ID =POPurchnDetail.ItemID ) end ItemCode, " +
            "case POPurchnDetail.ItemTypeID  " +
            "when 1 then (select Itemname  from Inventory where ID =POPurchnDetail.ItemID ) " +
            "when 2 then (select Itemname  from asset where ID =POPurchnDetail.ItemID ) " +
            "when 3 then " + ItemSPPBiayaNew() + " end ItemName, " +
            "CASE WHEN POPurchnDetail.UOMID > 0 THEN " +
            "(SELECT uomcode FROM UOM WHERE ID = POPurchnDetail.UOMID) END AS Satuan, 0 as Price,  " +
            "case when POPurchn.Crc >0 then (select nama from MataUang where ID=POPurchn.Crc) end CRC, " +
            "POPurchnDetail.Qty, SuppPurch.SupplierName, SuppPurch.Telepon,POPurchn.Termin,  " +
            "POPurchn.Delivery ,POPurchn.Disc,POPurchn.PPN,isnull(POPurchnDetail.DlvDate,'1/1/1900') as DlvDate, " +
            "case POPurchn.Approval  " +
            "when 0 then 'Open' " +
            "when 1 then 'Head' " +
            "when 2 then 'Manager' " +
            "when 3 then 'Purchasing' " +
            "end Approval " +
            "FROM POPurchn INNER JOIN " +
            "POPurchnDetail ON POPurchn.ID = POPurchnDetail.POID INNER JOIN " +
            "SuppPurch ON POPurchn.SupplierID = SuppPurch.ID INNER JOIN " +
            "SPP ON POPurchnDetail.SPPID = SPP.ID where " + strField + " " + k +
            " and popurchn.status>-1 and popurchndetail.status>-1  ) as A1 left join  " +
            "(SELECT RD.PODetailID,  R.ReceiptNo, R.ReceiptDate, RD.Quantity  " +
            "FROM   Receipt AS R INNER JOIN ReceiptDetail AS RD ON R.ID = RD.ReceiptID where R.Status>=0 and RD.RowStatus>=0) A2   " +
            "on A1.ID =A2.PODetailID";
            return strSQL;
        }
        public HistPO GenerateObject(SqlDataReader sqlDataReader)
        {
            objHistPO = new HistPO();
            objHistPO.POPurchnDate = Convert.ToDateTime(sqlDataReader["POPurchnDate"]);
            objHistPO.DlvDate = Convert.ToDateTime(sqlDataReader["DlvDate"]);
            objHistPO.NoPO = sqlDataReader["NoPO"].ToString();
            objHistPO.NoSPP = sqlDataReader["NOSPP"].ToString();
            objHistPO.ItemCode = sqlDataReader["ItemCode"].ToString();
            objHistPO.ItemName	=sqlDataReader["ItemName"].ToString();
            objHistPO.Satuan	=sqlDataReader["Satuan"].ToString();
            objHistPO.Price = Convert.ToDecimal(sqlDataReader["Price"]);
            objHistPO.CRC	=sqlDataReader["CRC"].ToString();
            objHistPO.Qty	=Convert.ToDecimal(sqlDataReader["Qty"]);
            objHistPO.SupplierName = sqlDataReader["SupplierName"].ToString();
            objHistPO.Telepon = sqlDataReader["Telepon"].ToString();
            objHistPO.Termin = sqlDataReader["Termin"].ToString();
            objHistPO.Delivery = sqlDataReader["Delivery"].ToString();
            objHistPO.Disc = Convert.ToDecimal(sqlDataReader["Disc"]);
            objHistPO.PPN = Convert.ToDecimal(sqlDataReader["PPN"]);
            objHistPO.Approval = sqlDataReader["Approval"].ToString();
            return objHistPO ;
        }
        /**
          * added on 28-04-2014
          * untuk perubahan pada itemname table biaya
          * dan stock per itemnya
          */

        public string ItemSPPBiayaNew()
        {
            string strSQL = "CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())< " +
                " (Select Minta from SPP where SPP.ID=POPurchnDetail.SPPID)) " +
                " THEN(select ItemName from Biaya where ID=POPurchnDetail.ItemID and Biaya.RowStatus>-1)+' - '+ " +
                " (Select SPPDetail.Keterangan From SPPDetail where SPPDetail.ID=POPurchnDetail.SPPDetailID) ELSE " +
                " (select ItemName from biaya where ID=POPurchnDetail.ItemID and biaya.RowStatus>-1) END ";
            return strSQL;
        }

        public string StockBiayaNew()
        {
            string strSQL = "CASE WHEN (isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())< " +
                " (Select Minta from SPP where SPP.ID=POPurchnDetail.SPPID)) THEN " +
                " (SELECT isnull(sum(jumlah),0) from Biaya where ItemName=(Select SPPDetail.Keterangan From SPPDetail where SPPDetail.ID=POPurchnDetail.SPPDetailID)) " +
                " ELSE (SELECT isnull(sum(Jumlah),0) from Biaya where ID=POPurchnDetail.ItemID and Biaya.RowStatus>-1) END";
            return strSQL;
        }
        public int CheckSPPBiaya(string strValue)
        {
            int sppid = 0;
            string strSQL = "Select TOP 1 SPPDetail.ID from SPPDetail LEFT JOIN SPP ON SPP.ID=SPPDetail.SPPID " +
                            "where SPPDetail.Keterangan like '%" + strValue + "%' and SPP.CreatedTime <=" +
                            "(select BiayaNew.ModifiedTime from BiayaNew where RowStatus=1)";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    sppid= Convert.ToInt32(sqlDataReader["ID"].ToString());
                }
            }
            
                return sppid;
            
        }
        public static string ItemSPPBiayaNewNF()
        {
            string strSQL = "CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())< " +
                " (Select Minta from SPP where SPP.ID=POPurchnDetail.SPPID)) " +
                " THEN(select ItemName from Biaya where ID=POPurchnDetail.ItemID and Biaya.RowStatus>-1)+' - '+ " +
                " (Select SPPDetail.Keterangan From SPPDetail where SPPDetail.ID=POPurchnDetail.SPPDetailID) ELSE " +
                " (select ItemName from biaya where ID=POPurchnDetail.ItemID and biaya.RowStatus>-1) END ";
            return strSQL;
        }
        public static List<HistPO> ViewHistPONF(string strfield, string strvalue, string k)
        {
            List<HistPO> alldata = new List<HistPO>();

            string strField = string.Empty;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    if (strfield == "3")
                    {
                        strField = "popurchndetail.itemtypeid=3 and (POPurchnDetail.SppDetailID in(SELECT SPPDetail.ID from SPPDetail where SPPDetail.Keterangan like '%" + strvalue + "%')" +
                                   "or popurchndetail.itemid in (select id from biaya where itemname like '%" + strvalue + "%') ";
                    }
                    else
                    {
                        strField = strfield + " '%" + strvalue + "%' ";
                    }
                    //string strsql = "SELECT   POPurchn.POPurchnDate, POPurchn.NoPO, SPP.NoSPP, " +
                    //                "CASE POPurchnDetail.ItemTypeID  " +
                    //                "when 1 then (select ItemCode  from Inventory where ID =POPurchnDetail.ItemID ) " +
                    //                "when 2 then (select ItemCode  from asset where ID =POPurchnDetail.ItemID ) " +
                    //                "when 3 then (select ItemCode  from biaya where ID =POPurchnDetail.ItemID ) end ItemCode, " +
                    //                "case POPurchnDetail.ItemTypeID  " +
                    //                "when 1 then (select Itemname  from Inventory where ID =POPurchnDetail.ItemID ) " +
                    //                "when 2 then (select Itemname  from asset where ID =POPurchnDetail.ItemID ) " +
                    //                "when 3 then " + ItemSPPBiayaNew() + " end ItemName, " +
                    //                "CASE WHEN POPurchnDetail.UOMID > 0 THEN " +
                    //                "(SELECT uomcode FROM UOM WHERE ID = POPurchnDetail.UOMID) END AS Satuan, " +
                    //                " POPurchnDetail.Price as Price,  " +
                    //                "case when POPurchn.Crc >0 then (select nama from MataUang where ID=POPurchn.Crc) end CRC, " +
                    //                "POPurchnDetail.Qty, SuppPurch.SupplierName, SuppPurch.Telepon,POPurchn.Termin,isnull(POPurchnDetail.DlvDate,'1/1/1900') as DlvDate,  " +
                    //                "POPurchn.Delivery ,POPurchn.Disc,POPurchn.PPN, " +
                    //                "case POPurchn.Approval  " +
                    //                "when 0 then 'Open' " +
                    //                "when 1 then 'Head' " +
                    //                "when 2 then 'Manager' " +
                    //                "when 3 then 'Purchasing' " +
                    //                "end Approval " +
                    //                "FROM POPurchn INNER JOIN " +
                    //                "POPurchnDetail ON POPurchn.ID = POPurchnDetail.POID INNER JOIN " +
                    //                "SuppPurch ON POPurchn.SupplierID = SuppPurch.ID INNER JOIN " +
                    //                "SPP ON POPurchnDetail.SPPID = SPP.ID where " + strField + " " + k +
                    //                " and popurchn.status>-1 and popurchndetail.status>-1  ORDER BY POPurchn.POPurchnDate desc,POPurchn.NoPO";

                    string strsql = "select * from (SELECT  top 200   POPurchnDetail.ID, POPurchn.POPurchnDate, POPurchn.NoPO, SPP.NoSPP, " +
                                    "CASE POPurchnDetail.ItemTypeID  " +
                                    "when 1 then (select ItemCode  from Inventory where ID =POPurchnDetail.ItemID ) " +
                                    "when 2 then (select ItemCode  from asset where ID =POPurchnDetail.ItemID ) " +
                                    "when 3 then (select ItemCode  from biaya where ID =POPurchnDetail.ItemID ) end ItemCode, " +
                                    "case POPurchnDetail.ItemTypeID  " +
                                    "when 1 then (select Itemname  from Inventory where ID =POPurchnDetail.ItemID ) " +
                                    "when 2 then (select Itemname  from asset where ID =POPurchnDetail.ItemID ) " +
                                    "when 3 then " + ItemSPPBiayaNewNF() + " end ItemName, " +
                                    "CASE WHEN POPurchnDetail.UOMID > 0 THEN " +
                                    "(SELECT uomcode FROM UOM WHERE ID = POPurchnDetail.UOMID) END AS Satuan, case when (select isnull(harga,0) from Inventory where ID=POPurchnDetail.ItemID)=0 then POPurchnDetail.Price else 0 end Price,  " +
                                    "case when POPurchn.Crc >0 then (select nama from MataUang where ID=POPurchn.Crc) end CRC, " +
                                    "POPurchnDetail.Qty, SuppPurch.SupplierName, SuppPurch.Telepon,POPurchn.Termin,isnull(POPurchnDetail.DlvDate,'1/1/1900') as DlvDate,  " +
                                    "POPurchn.Delivery ,POPurchn.Disc,POPurchn.PPN, " +
                                    "case POPurchn.Approval  " +
                                    "when 0 then 'Open' " +
                                    "when 1 then 'Head' " +
                                    "when 2 then 'Manager' " +
                                    "when 3 then 'Purchasing' " +
                                    "end Approval " +
                                    "FROM POPurchn INNER JOIN " +
                                    "POPurchnDetail ON POPurchn.ID = POPurchnDetail.POID INNER JOIN " +
                                    "SuppPurch ON POPurchn.SupplierID = SuppPurch.ID INNER JOIN " +
                                    "SPP ON POPurchnDetail.SPPID = SPP.ID where " + strField + " " + k +
                                    " and popurchn.status>-1 and popurchndetail.status>-1 ) as A1 left join  " +
                                    "(SELECT RD.PODetailID,  R.ReceiptNo, R.ReceiptDate, RD.Quantity  " +
                                    "FROM   Receipt AS R INNER JOIN ReceiptDetail AS RD ON R.ID = RD.ReceiptID where R.Status>=0 and RD.RowStatus>=0) A2   " +
                                    "on A1.ID =A2.PODetailID order by popurchndate desc";
                    alldata = connection.Query<HistPO>(strsql).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }

        public static List<HistPO> ViewHistPO2NF(string strfield, string strvalue, string k)
        {
            List<HistPO> alldata = new List<HistPO>();

            string strField = string.Empty;
            string inv = string.Empty;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    if (strfield == "1")
                    {
                        inv = "popurchndetail.itemtypeid=1 and popurchndetail.itemid in (select id from inventory where itemname like";
                    }
                    else if (strfield == "2")
                    {
                        inv = "popurchndetail.itemtypeid=2 and popurchndetail.itemid in (select id from asset where itemname like";
                    }
                    else if (strfield == "3")
                    {
                        inv = "popurchndetail.itemtypeid=3 and popurchndetail.itemid in (select id from biaya where itemname like";
                    }
                    else
                    {
                        inv = strfield;
                    }
                    if (strfield == "3")
                    {
                        strField = "popurchndetail.itemtypeid=3 and (POPurchnDetail.SppDetailID in(SELECT SPPDetail.ID from SPPDetail " +
                                    "where SPPDetail.Keterangan like '%" + strvalue + "%')" +
                                    "or popurchndetail.itemid in (select id from biaya where itemname like '%" + strvalue + "%') ";
                    }
                    else
                    {
                        strField = inv + " '%" + strvalue + "%' ";
                    }
                    //string strsql = "SELECT  Top 100 POPurchn.POPurchnDate, POPurchn.NoPO, SPP.NoSPP, " +
                    //                "CASE POPurchnDetail.ItemTypeID  " +
                    //                "when 1 then (select ItemCode  from Inventory where ID =POPurchnDetail.ItemID ) " +
                    //                "when 2 then (select ItemCode  from asset where ID =POPurchnDetail.ItemID ) " +
                    //                "when 3 then (select ItemCode  from biaya where ID =POPurchnDetail.ItemID ) end ItemCode, " +
                    //                "case POPurchnDetail.ItemTypeID  " +
                    //                "when 1 then (select Itemname  from Inventory where ID =POPurchnDetail.ItemID ) " +
                    //                "when 2 then (select Itemname  from asset where ID =POPurchnDetail.ItemID ) " +
                    //                "when 3 then " + ItemSPPBiayaNew() + " end ItemName, " +
                    //                "CASE WHEN POPurchnDetail.UOMID > 0 THEN " +
                    //                "(SELECT uomcode FROM UOM WHERE ID = POPurchnDetail.UOMID) END AS Satuan, POPurchnDetail.Price,  " +
                    //                "case when POPurchn.Crc >0 then (select nama from MataUang where ID=POPurchn.Crc) end CRC, " +
                    //                "POPurchnDetail.Qty, SuppPurch.SupplierName, SuppPurch.Telepon,POPurchn.Termin,isnull(POPurchnDetail.DlvDate,'1/1/1900') as DlvDate,  " +
                    //                "POPurchn.Delivery ,POPurchn.Disc,POPurchn.PPN, " +
                    //                "case POPurchn.Approval  " +
                    //                "when 0 then 'Open' " +
                    //                "when 1 then 'Head' " +
                    //                "when 2 then 'Manager' " +
                    //                "when 3 then 'Purchasing' " +
                    //                "end Approval " +
                    //                "FROM POPurchn INNER JOIN " +
                    //                "POPurchnDetail ON POPurchn.ID = POPurchnDetail.POID INNER JOIN " +
                    //                "SuppPurch ON POPurchn.SupplierID = SuppPurch.ID INNER JOIN " +
                    //                "SPP ON POPurchnDetail.SPPID = SPP.ID where " + strField + " " + k +
                    //                " and popurchn.status>-1 and popurchndetail.status>-1  ORDER BY POPurchn.POPurchnDate desc,POPurchn.NoPO";
                    string strsql = "select * from (SELECT  top 200   POPurchnDetail.ID, POPurchn.POPurchnDate, POPurchn.NoPO, SPP.NoSPP, " +
                                    "CASE POPurchnDetail.ItemTypeID  " +
                                    "when 1 then (select ItemCode  from Inventory where ID =POPurchnDetail.ItemID ) " +
                                    "when 2 then (select ItemCode  from asset where ID =POPurchnDetail.ItemID ) " +
                                    "when 3 then (select ItemCode  from biaya where ID =POPurchnDetail.ItemID ) end ItemCode, " +
                                    "case POPurchnDetail.ItemTypeID  " +
                                    "when 1 then (select Itemname  from Inventory where ID =POPurchnDetail.ItemID ) " +
                                    "when 2 then (select Itemname  from asset where ID =POPurchnDetail.ItemID ) " +
                                    "when 3 then " + ItemSPPBiayaNewNF() + " end ItemName, " +
                                    "CASE WHEN POPurchnDetail.UOMID > 0 THEN " +
                                    "(SELECT uomcode FROM UOM WHERE ID = POPurchnDetail.UOMID) END AS Satuan, POPurchnDetail.Price,  " +
                                    "case when POPurchn.Crc >0 then (select nama from MataUang where ID=POPurchn.Crc) end CRC, " +
                                    "POPurchnDetail.Qty, SuppPurch.SupplierName, SuppPurch.Telepon,POPurchn.Termin,isnull(POPurchnDetail.DlvDate,'1/1/1900') as DlvDate,  " +
                                    "POPurchn.Delivery ,POPurchn.Disc,POPurchn.PPN, " +
                                    "case POPurchn.Approval  " +
                                    "when 0 then 'Open' " +
                                    "when 1 then 'Head' " +
                                    "when 2 then 'Manager' " +
                                    "when 3 then 'Purchasing' " +
                                    "end Approval " +
                                    "FROM POPurchn INNER JOIN " +
                                    "POPurchnDetail ON POPurchn.ID = POPurchnDetail.POID INNER JOIN " +
                                    "SuppPurch ON POPurchn.SupplierID = SuppPurch.ID INNER JOIN " +
                                    "SPP ON POPurchnDetail.SPPID = SPP.ID where " + strField + " " + k +
                                    " and popurchn.status>-1 and popurchndetail.status>-1 ) as A1 left join  " +
                                    "(SELECT RD.PODetailID,  R.ReceiptNo, R.ReceiptDate, RD.Quantity  " +
                                    "FROM   Receipt AS R INNER JOIN ReceiptDetail AS RD ON R.ID = RD.ReceiptID where R.Status>=0 and RD.RowStatus>=0) A2   " +
                                    "on A1.ID =A2.PODetailID";
                    alldata = connection.Query<HistPO>(strsql).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }

        public static List<HistPO> ViewHistPOByPrice0NF(string strfield, string strvalue, string k)
        {
            List<HistPO> alldata = new List<HistPO>();

            string strField = string.Empty;

            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    if (strfield == "3")
                    {
                        strField =
                        "popurchndetail.itemtypeid=3 and (POPurchnDetail.SppDetailID in(SELECT SPPDetail.ID from SPPDetail where SPPDetail.Keterangan like '%" + strvalue + "%')" +
                        "or popurchndetail.itemid in (select id from biaya where itemname like '%" + strvalue + "%') ";
                    }
                    else
                    {
                        strField = strfield + " '%" + strvalue + "%' ";
                    }
                    //string strsql = "SELECT  top 200   POPurchn.POPurchnDate, POPurchn.NoPO, SPP.NoSPP, " +
                    //                "CASE POPurchnDetail.ItemTypeID  " +
                    //                "when 1 then (select ItemCode  from Inventory where ID =POPurchnDetail.ItemID ) " +
                    //                "when 2 then (select ItemCode  from asset where ID =POPurchnDetail.ItemID ) " +
                    //                "when 3 then (select ItemCode  from biaya where ID =POPurchnDetail.ItemID ) end ItemCode, " +
                    //                "case POPurchnDetail.ItemTypeID  " +
                    //                "when 1 then (select Itemname  from Inventory where ID =POPurchnDetail.ItemID ) " +
                    //                "when 2 then (select Itemname  from asset where ID =POPurchnDetail.ItemID ) " +
                    //                "when 3 then " + ItemSPPBiayaNew() + " end ItemName, " +
                    //                "CASE WHEN POPurchnDetail.UOMID > 0 THEN " +
                    //                "(SELECT uomcode FROM UOM WHERE ID = POPurchnDetail.UOMID) END AS Satuan, 0 as Price,  " +
                    //                "case when POPurchn.Crc >0 then (select nama from MataUang where ID=POPurchn.Crc) end CRC, " +
                    //                "POPurchnDetail.Qty, SuppPurch.SupplierName, SuppPurch.Telepon,POPurchn.Termin, isnull(POPurchnDetail.DlvDate,'1/1/1900') as DlvDate, " +
                    //                "POPurchn.Delivery ,POPurchn.Disc,POPurchn.PPN, " +
                    //                "case POPurchn.Approval  " +
                    //                "when 0 then 'Open' " +
                    //                "when 1 then 'Head' " +
                    //                "when 2 then 'Manager' " +
                    //                "when 3 then 'Purchasing' " +
                    //                "end Approval " +
                    //                "FROM POPurchn INNER JOIN " +
                    //                "POPurchnDetail ON POPurchn.ID = POPurchnDetail.POID INNER JOIN " +
                    //                "SuppPurch ON POPurchn.SupplierID = SuppPurch.ID INNER JOIN " +
                    //                "SPP ON POPurchnDetail.SPPID = SPP.ID where " + strField + " " + k +
                    //                " and popurchn.status>-1 and popurchndetail.status>-1  ORDER BY POPurchn.POPurchnDate desc,POPurchn.NoPO";
                    string strsql = "select * from (SELECT  top 200   POPurchnDetail.ID, POPurchn.POPurchnDate, POPurchn.NoPO, SPP.NoSPP, " +
                                    "CASE POPurchnDetail.ItemTypeID  " +
                                    "when 1 then (select ItemCode  from Inventory where ID =POPurchnDetail.ItemID ) " +
                                    "when 2 then (select ItemCode  from asset where ID =POPurchnDetail.ItemID ) " +
                                    "when 3 then (select ItemCode  from biaya where ID =POPurchnDetail.ItemID ) end ItemCode, " +
                                    "case POPurchnDetail.ItemTypeID  " +
                                    "when 1 then (select Itemname  from Inventory where ID =POPurchnDetail.ItemID ) " +
                                    "when 2 then (select Itemname  from asset where ID =POPurchnDetail.ItemID ) " +
                                    "when 3 then " + ItemSPPBiayaNewNF() + " end ItemName, " +
                                    "CASE WHEN POPurchnDetail.UOMID > 0 THEN " +
                                    "(SELECT uomcode FROM UOM WHERE ID = POPurchnDetail.UOMID) END AS Satuan, 0 as Price,  " +
                                    "case when POPurchn.Crc >0 then (select nama from MataUang where ID=POPurchn.Crc) end CRC, " +
                                    "POPurchnDetail.Qty, SuppPurch.SupplierName, SuppPurch.Telepon,POPurchn.Termin,  " +
                                    "POPurchn.Delivery ,POPurchn.Disc,POPurchn.PPN,isnull(POPurchnDetail.DlvDate,'1/1/1900') as DlvDate, " +
                                    "case POPurchn.Approval  " +
                                    "when 0 then 'Open' " +
                                    "when 1 then 'Head' " +
                                    "when 2 then 'Manager' " +
                                    "when 3 then 'Purchasing' " +
                                    "end Approval " +
                                    "FROM POPurchn INNER JOIN " +
                                    "POPurchnDetail ON POPurchn.ID = POPurchnDetail.POID INNER JOIN " +
                                    "SuppPurch ON POPurchn.SupplierID = SuppPurch.ID INNER JOIN " +
                                    "SPP ON POPurchnDetail.SPPID = SPP.ID where " + strField + " " + k +
                                    " and popurchn.status>-1 and popurchndetail.status>-1  ) as A1 left join  " +
                                    "(SELECT RD.PODetailID,  R.ReceiptNo, R.ReceiptDate, RD.Quantity  " +
                                    "FROM   Receipt AS R INNER JOIN ReceiptDetail AS RD ON R.ID = RD.ReceiptID where R.Status>=0 and RD.RowStatus>=0) A2   " +
                                    "on A1.ID =A2.PODetailID";
                    alldata = connection.Query<HistPO>(strsql).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }
            return alldata;
        }
    }
}
