using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using System.Web;
using BusinessFacade;
using DataAccessLayer;
using Dapper;


namespace BusinessFacade
{
    public class LapStockBarangNfFacade : AbstractTransactionFacade
    {
        public override int Delete(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Insert(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }

        public override int Update(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public static List<LapStockBarangNf.ParamTypeItem> GetListTypeItem()
        {
            List<LapStockBarangNf.ParamTypeItem> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "Select ID,TypeDescription from ItemTypePurchn where RowStatus>-1";
                    AllData = connection.Query<LapStockBarangNf.ParamTypeItem>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<LapStockBarangNf.ParamGroupItem> GetListGroupItem()
        {
            List<LapStockBarangNf.ParamGroupItem> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "select ID,GroupDescription from GroupsPurchn where RowStatus >-1";
                    AllData = connection.Query<LapStockBarangNf.ParamGroupItem>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<LapStockBarangNf.ParamData> GetListData(string allQuery)
        {
            List<LapStockBarangNf.ParamData> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = allQuery;
                    AllData = connection.Query<LapStockBarangNf.ParamData>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static string ViewLapBarang(int TypeItem, int valstock, int valgroup, int valaktif)
        {
            string tipeBarang = "Inventory"; int dgItemTypeID = 1;
            if (TypeItem == 2) { tipeBarang = "Asset"; dgItemTypeID = 2; }
            if (TypeItem == 3) { tipeBarang = "Biaya"; dgItemTypeID = 3; }

            string cmdQuery = string.Empty;
            string cmdTipeBarang = string.Empty;
            cmdQuery = "and A.GroupID = " + valgroup + " and A.ItemTypeID = " + dgItemTypeID;    
            if (valstock == 0 && valaktif == 1)
            {
                cmdQuery = "and A.Aktif = 1 and A.GroupID = " + valgroup + 
                    " and A.ItemTypeID = " + dgItemTypeID;
            }
            if (valstock == 0 && valaktif == 2)
            {
                cmdQuery = "and A.Aktif = 0 and A.GroupID = " + valgroup + 
                    " and A.ItemTypeID = " + dgItemTypeID;
            }
            if (valstock == 1 && valaktif == 0)
            {
                cmdQuery = "and A.Stock = 1 and A.GroupID = " + valgroup + 
                    " and A.ItemTypeID = " + dgItemTypeID;
            }
            if (valstock == 1 && valaktif == 1)
            {
                cmdQuery = "and A.Aktif = 1 and A.Stock = 1 and A.GroupID = " + valgroup + 
                    " and A.ItemTypeID = " + dgItemTypeID;
            }
            if (valstock == 1 && valaktif == 2)
            {
                cmdQuery = "and A.Aktif = 0 and A.Stock = 1 and A.GroupID = " + valgroup + 
                    " and A.ItemTypeID = " + dgItemTypeID;
            }
            if (valstock == 2 && valaktif == 0)
            {
                cmdQuery = "and A.Stock = 0 and A.GroupID = " + valgroup + 
                    " and A.ItemTypeID = " + dgItemTypeID;
            }
            if (valstock == 2 && valaktif == 1)
            {
                cmdQuery = "and A.Aktif = 1 and A.Stock = 0 and A.GroupID = " + valgroup + 
                    " and A.ItemTypeID = " + dgItemTypeID;
            }
            if (valstock == 2 && valaktif == 2)
            {
                cmdQuery = "and A.Aktif = 0 and A.Stock = 0 and A.GroupID = " + valgroup + 
                    " and A.ItemTypeID = " + dgItemTypeID;
            }

            cmdTipeBarang = "from Inventory as A, UOM as B, GroupsPurchn as C ";
            if (tipeBarang == "Asset") { cmdTipeBarang = "from Asset as A, UOM as B, GroupsPurchn as C "; }
            if (tipeBarang == "Biaya") { cmdTipeBarang = "from Biaya as A, UOM as B, GroupsPurchn as C "; }

            return "select C.GroupDescription,A.ItemCode,A.ItemName,B.UOMCode,A.Jumlah,A.Jumlah+jmltransit StockGudang," +
                   "case when A.ID > 1 then A.MaxStock ELSE 0 END StockMax, " +
                   "case when A.ID > 1 then A.MinStock ELSE 0 END StockMin, " +
                   "case when A.ID > 1 then A.ReOrder ELSE 0 END ReOrder " + cmdTipeBarang +
                   "where A.UOMID = B.ID and A.GroupID = C.ID and A.RowStatus > -1 and B.RowStatus > -1 and c.RowStatus > -1 and A.Jumlah > 0 " + cmdQuery;
        }
    }
}
