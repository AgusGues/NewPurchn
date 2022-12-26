using Dapper;
using DataAccessLayer;
using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessFacade
{
    public class HistSPPFacade : AbstractFacade
    {
        public override int Delete(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override int Insert(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }

        public override int Update(object objDomain)
        {
            throw new NotImplementedException();
        }

        public static string ItemSPPBiayaNew()
        {
            string strSQL = "CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())< " +
                            " (Select Minta from SPP where SPP.ID=POPurchnDetail.SPPID)) " +
                            " THEN(select ItemName from Biaya where ID=POPurchnDetail.ItemID and Biaya.RowStatus>-1)+' - '+ " +
                            " (Select SPPDetail.Keterangan From SPPDetail where SPPDetail.ID=POPurchnDetail.SPPDetailID) ELSE " +
                            " (select ItemName from biaya where ID=POPurchnDetail.ItemID and biaya.RowStatus>-1) END ";
            return strSQL;
        }

        public static List<HistSPP> ViewHistPO(string strfield, string strvalue, string k)
        {
            List<HistSPP> alldata = new List<HistSPP>();
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

                    string strsql = "SELECT   POPurchn.POPurchnDate, POPurchn.NoPO, SPP.NoSPP, SPP.Minta as 'TglSPP'," +
                                    "CASE POPurchnDetail.ItemTypeID  " +
                                    "when 1 then (select ItemCode  from Inventory where ID =POPurchnDetail.ItemID ) " +
                                    "when 2 then (select ItemCode  from asset where ID =POPurchnDetail.ItemID ) " +
                                    "when 3 then (select ItemCode  from biaya where ID =POPurchnDetail.ItemID ) end ItemCode, " +
                                    "case POPurchnDetail.ItemTypeID  " +
                                    "when 1 then (select Itemname  from Inventory where ID =POPurchnDetail.ItemID ) " +
                                    "when 2 then (select Itemname  from asset where ID =POPurchnDetail.ItemID ) " +
                                    "when 3 then " + ItemSPPBiayaNew() + " end ItemName, " +
                                    "CASE WHEN POPurchnDetail.UOMID > 0 THEN " +
                                    "(SELECT uomcode FROM UOM WHERE ID = POPurchnDetail.UOMID) END AS Satuan, " +
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
                                    "SPP ON POPurchnDetail.SPPID = SPP.ID where " + strField + " " + k +
                                    " and popurchn.status>-1 and popurchndetail.status>-1  ORDER BY POPurchn.POPurchnDate desc,POPurchn.NoPO";
                    alldata = connection.Query<HistSPP>(strsql).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }

            return alldata;
        }

        public static List<HistSPP> ViewHistPO2(string strfield, string strvalue, string k)
        {
            List<HistSPP> alldata = new List<HistSPP>();
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
                        strField =
                                    "popurchndetail.itemtypeid=3 and (POPurchnDetail.SppDetailID in(SELECT SPPDetail.ID from SPPDetail where SPPDetail.Keterangan like '%" + strvalue + "%')" +
                                    "or popurchndetail.itemid in (select id from biaya where itemname like '%" + strvalue + "%') ";
                    }
                    else
                    {
                        strField = inv + " '%" + strvalue + "%' ";
                    }

                    string strsql =
                                    "SELECT  Top 100 POPurchn.POPurchnDate, POPurchn.NoPO, SPP.NoSPP,SPP.Minta as 'TglSPP', " +
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
                                    "POPurchn.Delivery ,POPurchn.Disc,POPurchn.PPN," +
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

                    alldata = connection.Query<HistSPP>(strsql).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    alldata = null;
                }
            }

            return alldata;
        }

        public static List<HistSPP> ViewHistPOByPrice0(string strfield, string strvalue, string k)
        {
            List<HistSPP> alldata = new List<HistSPP>();
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

                    string strsql =
                                    "SELECT  top 200   POPurchn.POPurchnDate, POPurchn.NoPO, SPP.NoSPP,SPP.Minta as 'TglSPP'," +
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
                                    "POPurchn.Delivery ,POPurchn.Disc,POPurchn.PPN," +
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
                    alldata = connection.Query<HistSPP>(strsql).ToList();
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
