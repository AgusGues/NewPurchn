using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;
using BusinessFacade;
using Dapper;
using System.Collections.Generic;

namespace BusinessFacade
{
    public class HargaBarangFacade : AbstractFacade
    {
        private HargaBarang objHargaBarang = new HargaBarang();
        private ArrayList arrGrade;
        //private List<SqlParameter> sqlListParam;


        public HargaBarangFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            return 0;
        }

        public override int Update(object objDomain)
        {
            return 0;
        }

        public override int Delete(object objDomain)
        {
            return 0;
        }

        public override ArrayList Retrieve()
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 50 ItemID,Price,case ItemTypeID when 1 then (select ItemCode from Inventory where Inventory.ID=A.ItemID)" +
                                                                            "when 2 then (select ItemCode from Asset where Asset.ID=A.ItemID) " +
                                                                            "when 3 then (select ItemCode from Biaya where Biaya.ID=A.ItemID) " +
                                                                            "else '' end ItemCode,case ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=A.ItemID) " +
                                                                            "when 2 then (select ItemName from Asset where Asset.ID=A.ItemID) " +
                                                                            "when 3 then (select ItemName from Biaya where Biaya.ID=A.ItemID) " +
                                                                            "else '' end ItemName,C.SupplierName,C.SupplierCode " +
                                                                            "from POPurchnDetail as A, POPurchn as B, Supplier as C where A.POID=B.ID and B.Status>-1 and A.Status>-1 and B.SupplierID=C.ID " +
                                                                            "order by ItemID,SupplierCode");
            strError = dataAccess.Error;
            arrGrade = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGrade.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGrade.Add(new HargaBarang());

            return arrGrade;
        }

        //public HargaBarang RetrieveById(int Id)
        //{
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemID,B.Description as ItemName,A.ZonaID,C.ZonaCode,A.HargaJual,A.HargaDistributor,A.HargaSubDistributor,A.HargaRetail,A.HargaToko,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from HargaBarang as A,Items as B,Zona as C where A.ItemID = B.ID and A.ZonaID = C.ID and A.RowStatus = 0 and A.ID = " + Id);
        //    strError = dataAccess.Error;
        //    arrGrade = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            return GenerateObject(sqlDataReader);
        //        }
        //    }

        //    return new HargaBarang();
        //}

        //public HargaBarang RetrieveByItemIDAndZonaID(int itemID, int zonaID)
        //{
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemID,B.Description as ItemName,A.ZonaID,C.ZonaCode,A.HargaJual,A.HargaDistributor,A.HargaSubDistributor,A.HargaRetail,A.HargaToko,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from HargaBarang as A,Items as B,Zona as C where A.ItemID = B.ID and A.ZonaID = C.ID and A.RowStatus = 0 and A.ItemID = " + itemID + " and A.ZonaID = " + zonaID);
        //    strError = dataAccess.Error;
        //    arrGrade = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            return GenerateObject(sqlDataReader);
        //        }
        //    }

        //    return new HargaBarang();
        //}

        public ArrayList RetrieveByCriteria(string strField, string strValue, int viewprice)
        {
            string strsql = string.Empty;
            if (viewprice < 2)
                strsql = "select top 50 * from (select A.ID ,ItemID,case when (select isnull(harga,0) from Inventory where ID=A.ItemID)=0 then A.Price else 0 end Price,case ItemTypeID when 1 then (select ItemCode from Inventory where Inventory.ID=A.ItemID)" +
               "when 2 then (select ItemCode from Asset where Asset.ID=A.ItemID) " +
               "when 3 then (select ItemCode from Biaya where Biaya.ID=A.ItemID) " +
               "else '' end ItemCode,case ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=A.ItemID) " +
               "when 2 then (select ItemName from Asset where Asset.ID=A.ItemID) " +
               "when 3 then (select ItemName from Biaya where Biaya.ID=A.ItemID) " +
               "else '' end ItemName,C.SupplierName,C.SupplierCode " +
               "from POPurchnDetail as A, POPurchn as B, SuppPurch as C where A.POID=B.ID and B.Status>-1 and A.Status>-1 and B.SupplierID=C.ID ) as AA where " +
               strField + " like '%" + strValue + "%' order by AA.ID desc,ItemID,SupplierCode";
            else
                strsql = "select top 50 * from (select A.ID ,ItemID, Price,case ItemTypeID when 1 then (select ItemCode from Inventory where Inventory.ID=A.ItemID)" +
                            "when 2 then (select ItemCode from Asset where Asset.ID=A.ItemID) " +
                            "when 3 then (select ItemCode from Biaya where Biaya.ID=A.ItemID) " +
                            "else '' end ItemCode,case ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=A.ItemID) " +
                            "when 2 then (select ItemName from Asset where Asset.ID=A.ItemID) " +
                            "when 3 then (select ItemName from Biaya where Biaya.ID=A.ItemID) " +
                            "else '' end ItemName,C.SupplierName,C.SupplierCode " +
                            "from POPurchnDetail as A, POPurchn as B, SuppPurch as C where A.POID=B.ID and B.Status>-1 and A.Status>-1 and B.SupplierID=C.ID ) as AA where " +
                            strField + " like '%" + strValue + "%' order by AA.ID desc,ItemID,SupplierCode";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrGrade = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGrade.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGrade.Add(new HargaBarang());

            return arrGrade;
        }
        public static List<HargaBarang> RetrieveAll()
        {
            List<HargaBarang> alldata = new List<HargaBarang>();
            string strsql = string.Empty;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    strsql = "select top 50 ItemID,Price,case ItemTypeID when 1 then (select ItemCode from Inventory where Inventory.ID=A.ItemID)" +
                                                                        "when 2 then (select ItemCode from Asset where Asset.ID=A.ItemID) " +
                                                                        "when 3 then (select ItemCode from Biaya where Biaya.ID=A.ItemID) " +
                                                                        "else '' end ItemCode,case ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=A.ItemID) " +
                                                                        "when 2 then (select ItemName from Asset where Asset.ID=A.ItemID) " +
                                                                        "when 3 then (select ItemName from Biaya where Biaya.ID=A.ItemID) " +
                                                                        "else '' end ItemName,C.SupplierName,C.SupplierCode " +
                                                                        "from POPurchnDetail as A, POPurchn as B, Supplier as C where A.POID=B.ID and B.Status>-1 and A.Status>-1 and B.SupplierID=C.ID " +
                                                                        "order by ItemID,SupplierCode";
                    alldata = connection.Query<HargaBarang>(strsql).ToList();
                }
                catch (Exception e)
                {
                    alldata = null;
                }
            }
            return alldata;
        }

        //fungsi dibawah merupakan modifikasi dari fungsi RetriveByCriteria()
        public static List<HargaBarang> RetrieveByCriteria2(string strField, string strValue, int viewprice)
        {
            List<HargaBarang> alldata = new List<HargaBarang>();
            string strsql = string.Empty;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    if (viewprice < 2)
                strsql = "select top 50 * from (select A.ID ,ItemID,case when (select isnull(harga,0) from Inventory where ID=A.ItemID)=0 then A.Price else 0 end Price,case ItemTypeID when 1 then (select ItemCode from Inventory where Inventory.ID=A.ItemID)" +
               "when 2 then (select ItemCode from Asset where Asset.ID=A.ItemID) " +
               "when 3 then (select ItemCode from Biaya where Biaya.ID=A.ItemID) " +
               "else '' end ItemCode,case ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=A.ItemID) " +
               "when 2 then (select ItemName from Asset where Asset.ID=A.ItemID) " +
               "when 3 then (select ItemName from Biaya where Biaya.ID=A.ItemID) " +
               "else '' end ItemName,C.SupplierName,C.SupplierCode " +
               "from POPurchnDetail as A, POPurchn as B, SuppPurch as C where A.POID=B.ID and B.Status>-1 and A.Status>-1 and B.SupplierID=C.ID ) as AA where " +
               strField + " like '%" + strValue + "%' order by AA.ID desc,ItemID,SupplierCode";
            else
                strsql = "select top 50 * from (select A.ID ,ItemID, Price,case ItemTypeID when 1 then (select ItemCode from Inventory where Inventory.ID=A.ItemID)" +
                            "when 2 then (select ItemCode from Asset where Asset.ID=A.ItemID) " +
                            "when 3 then (select ItemCode from Biaya where Biaya.ID=A.ItemID) " +
                            "else '' end ItemCode,case ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=A.ItemID) " +
                            "when 2 then (select ItemName from Asset where Asset.ID=A.ItemID) " +
                            "when 3 then (select ItemName from Biaya where Biaya.ID=A.ItemID) " +
                            "else '' end ItemName,C.SupplierName,C.SupplierCode " +
                            "from POPurchnDetail as A, POPurchn as B, SuppPurch as C where A.POID=B.ID and B.Status>-1 and A.Status>-1 and B.SupplierID=C.ID ) as AA where " +
                            strField + " like '%" + strValue + "%' order by AA.ID desc,ItemID,SupplierCode";
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            //strError = dataAccess.Error;
            //arrGrade = new ArrayList();

            //if (sqlDataReader.HasRows)
            //{
            //    while (sqlDataReader.Read())
            //    {
            //        arrGrade.Add(GenerateObject(sqlDataReader));
            //    }
            //}
            //else
            //    arrGrade.Add(new HargaBarang());

            //return arrGrade;
                    alldata = connection.Query<HargaBarang>(strsql).ToList();
                }
                catch (Exception e)
                {
                    alldata = null;
                }
            }
            return alldata;
        }

        public HargaBarang GenerateObject(SqlDataReader sqlDataReader)
        {
            objHargaBarang = new HargaBarang();
            //objHargaBarang.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objHargaBarang.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objHargaBarang.ItemCode = sqlDataReader["ItemCode"].ToString();
            objHargaBarang.ItemName = sqlDataReader["ItemName"].ToString();
            objHargaBarang.SupplierCode = sqlDataReader["SupplierCode"].ToString();
            objHargaBarang.SupplierName = sqlDataReader["SupplierName"].ToString();
            objHargaBarang.Price = Convert.ToDecimal(sqlDataReader["Price"]);
            return objHargaBarang;
        }

        
    }

}
