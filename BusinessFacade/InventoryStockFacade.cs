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
    public class InventoryStockFacade : AbstractFacade
    {
        private InventoryStock objInventoryStock = new InventoryStock();
        private ArrayList arrInventoryStock;
        private List<SqlParameter> sqlListParam;


        public InventoryStockFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objInventoryStock = (InventoryStock)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", objInventoryStock.ItemID));
                sqlListParam.Add(new SqlParameter("@Quantity", objInventoryStock.Quantity));
                sqlListParam.Add(new SqlParameter("@TempQuantity", objInventoryStock.TempQuantity));
                sqlListParam.Add(new SqlParameter("@TransitQuantity", objInventoryStock.TransitQuantity));
                sqlListParam.Add(new SqlParameter("@ReceiveQuantity", objInventoryStock.ReceiveQuantity));
                sqlListParam.Add(new SqlParameter("@DepoID", objInventoryStock.DepoID));
                sqlListParam.Add(new SqlParameter("@TypeKondisi", objInventoryStock.TypeKondisi));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertInventoryStock");

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
                objInventoryStock = (InventoryStock)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Status", objInventoryStock.Status));
                sqlListParam.Add(new SqlParameter("@ItemID", objInventoryStock.ItemID));
                sqlListParam.Add(new SqlParameter("@Quantity", objInventoryStock.Quantity));
                sqlListParam.Add(new SqlParameter("@DepoID", objInventoryStock.DepoID));
                sqlListParam.Add(new SqlParameter("@TypeKondisi", objInventoryStock.TypeKondisi));
                sqlListParam.Add(new SqlParameter("@FromDepoID", objInventoryStock.FromDepoID));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateInventoryStock");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateStock(object objDomain)
        {
            try
            {
                objInventoryStock = (InventoryStock)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objInventoryStock.ID));
                sqlListParam.Add(new SqlParameter("@Quantity", objInventoryStock.Quantity));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateStock");

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

            return 0;
        }

        public override ArrayList Retrieve()
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select InventoryStock.ID,InventoryStock.ItemID,Items.ItemCode,Items.Description as ItemName,InventoryStock.Quantity,InventoryStock.TempQuantity,InventoryStock.TransitQuantity,InventoryStock.ReceiveQuantity,InventoryStock.DepoID,Depo.DepoName,InventoryStock.TypeKondisi from InventoryStock,Items,Depo where InventoryStock.ItemID = Items.ID and InventoryStock.DepoID = Depo.ID");
            strError = dataAccess.Error;
            arrInventoryStock = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrInventoryStock.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrInventoryStock.Add(new InventoryStock());

            return arrInventoryStock;
        }
        public InventoryStock RetrieveByItemCodeAndDepo(int itemID, int depoID)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select InventoryStock.ID,InventoryStock.ItemID,Items.ItemCode,Items.Description as ItemName,InventoryStock.Quantity,InventoryStock.TempQuantity,InventoryStock.TransitQuantity,InventoryStock.ReceiveQuantity,InventoryStock.DepoID,Depo.DepoName,InventoryStock.TypeKondisi from InventoryStock,Items,Depo where InventoryStock.ItemID = Items.ID and InventoryStock.DepoID = Depo.ID  and InventoryStock.ItemID = " + itemID + " and InventoryStock.DepoID = " + depoID);
            strError = dataAccess.Error;
            arrInventoryStock = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new InventoryStock();
        }
        public InventoryStock RetrieveByItemCodeAndDepoAndTypeKondisi(int itemID, int depoID, int kondisi)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select InventoryStock.ID,InventoryStock.ItemID,Items.ItemCode,Items.Description as ItemName,InventoryStock.Quantity,InventoryStock.TempQuantity,InventoryStock.TransitQuantity,InventoryStock.ReceiveQuantity,InventoryStock.DepoID,Depo.DepoName,InventoryStock.TypeKondisi from InventoryStock,Items,Depo where InventoryStock.ItemID = Items.ID and InventoryStock.DepoID = Depo.ID  and InventoryStock.ItemID = " + itemID + " and InventoryStock.DepoID = " + depoID + " and TypeKondisi="+kondisi);
            strError = dataAccess.Error;
            arrInventoryStock = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new InventoryStock();
        }
        public InventoryStock RetrieveByItemCodeAndDepoAndKondisi(int itemID, int depoID, int typeKondisi)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select InventoryStock.ID,InventoryStock.ItemID,Items.ItemCode,Items.Description as ItemName,InventoryStock.Quantity,InventoryStock.TempQuantity,InventoryStock.TransitQuantity,InventoryStock.ReceiveQuantity,InventoryStock.DepoID,Depo.DepoName,InventoryStock.TypeKondisi from InventoryStock,Items,Depo where InventoryStock.ItemID = Items.ID and InventoryStock.DepoID = Depo.ID  and InventoryStock.ItemID = " + itemID + " and InventoryStock.DepoID = " + depoID + " and InventoryStock.TypeKondisi=" + typeKondisi);
            strError = dataAccess.Error;
            arrInventoryStock = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new InventoryStock();
        }

        public ArrayList RetrieveByDepo(int depoID)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select InventoryStock.ID,InventoryStock.ItemID,Items.ItemCode,Items.Description as ItemName,InventoryStock.Quantity,InventoryStock.TempQuantity,InventoryStock.TransitQuantity,InventoryStock.ReceiveQuantity,InventoryStock.DepoID,Depo.DepoName,InventoryStock.TypeKondisi from InventoryStock,Items,Depo where InventoryStock.ItemID = Items.ID and InventoryStock.DepoID = Depo.ID and InventoryStock.DepoID = " + depoID +" order by ItemID");
            strError = dataAccess.Error;
            arrInventoryStock = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrInventoryStock.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrInventoryStock.Add(new InventoryStock());

            return arrInventoryStock;
        }
        public ArrayList RetrieveByDepoDescription(int depoID, string desc)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select InventoryStock.ID,InventoryStock.ItemID,Items.ItemCode,Items.Description as ItemName,InventoryStock.Quantity,InventoryStock.TempQuantity,InventoryStock.TransitQuantity,InventoryStock.ReceiveQuantity,InventoryStock.DepoID,Depo.DepoName,InventoryStock.TypeKondisi from InventoryStock,Items,Depo where InventoryStock.ItemID = Items.ID and InventoryStock.DepoID = Depo.ID and InventoryStock.DepoID = " + depoID + " and Items.Description like '%"+desc+"%' order by ItemID");
            strError = dataAccess.Error;
            arrInventoryStock = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrInventoryStock.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrInventoryStock.Add(new InventoryStock());

            return arrInventoryStock;
        }

        public ArrayList RetrieveByItem(int itemID)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select InventoryStock.ID,InventoryStock.ItemID,Items.ItemCode,Items.Description as ItemName,InventoryStock.Quantity,InventoryStock.TempQuantity,InventoryStock.TransitQuantity,InventoryStock.ReceiveQuantity,InventoryStock.DepoID,Depo.DepoName,InventoryStock.TypeKondisi from InventoryStock,Items,Depo where InventoryStock.ItemID = Items.ID and InventoryStock.DepoID = Depo.ID and InventoryStock.ItemID = " + itemID);
            strError = dataAccess.Error;
            arrInventoryStock = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrInventoryStock.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrInventoryStock.Add(new InventoryStock());

            return arrInventoryStock;
        }

        public InventoryStock GenerateObject(SqlDataReader sqlDataReader)
        {
            objInventoryStock = new InventoryStock();
            objInventoryStock.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objInventoryStock.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objInventoryStock.ItemCode = sqlDataReader["ItemCode"].ToString();
            objInventoryStock.ItemName = sqlDataReader["ItemName"].ToString();
            objInventoryStock.Quantity = Convert.ToInt32(sqlDataReader["Quantity"]);
            objInventoryStock.TempQuantity = Convert.ToInt32(sqlDataReader["TempQuantity"]);
            objInventoryStock.TransitQuantity = Convert.ToInt32(sqlDataReader["TransitQuantity"]);
            objInventoryStock.ReceiveQuantity = Convert.ToInt32(sqlDataReader["ReceiveQuantity"]);
            objInventoryStock.DepoID = Convert.ToInt32(sqlDataReader["DepoID"]);
            objInventoryStock.DepoName = sqlDataReader["DepoName"].ToString();
            objInventoryStock.TypeKondisi = Convert.ToInt32(sqlDataReader["TypeKondisi"]);
            return objInventoryStock;

        }
    }
}

//namespace BusinessFacade
//{
//    public class InventoryStockFacade : AbstractFacade
//    {
//        private InventoryStock objInventoryStock = new InventoryStock();
//        private ArrayList arrInventoryStock;
//        private List<SqlParameter> sqlListParam;


//        public InventoryStockFacade()
//            : base()
//        {

//        }

//        public override int Insert(object objDomain)
//        {
//            try
//            {
//                objInventoryStock = (InventoryStock)objDomain;
//                sqlListParam = new List<SqlParameter>();
//                sqlListParam.Add(new SqlParameter("@ItemID", objInventoryStock.ItemID));
//                sqlListParam.Add(new SqlParameter("@Quantity", objInventoryStock.Quantity));
//                sqlListParam.Add(new SqlParameter("@TempQuantity", objInventoryStock.TempQuantity));
//                sqlListParam.Add(new SqlParameter("@TransitQuantity", objInventoryStock.TransitQuantity));
//                sqlListParam.Add(new SqlParameter("@ReceiveQuantity", objInventoryStock.ReceiveQuantity));
//                sqlListParam.Add(new SqlParameter("@DepoID", objInventoryStock.DepoID));

//                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertInventoryStock");

//                strError = dataAccess.Error;

//                return intResult;

//            }
//            catch (Exception ex)
//            {
//                strError = ex.Message;
//                return -1;
//            }
//        }

//        public override int Update(object objDomain)
//        {
//            try
//            {
//                objInventoryStock = (InventoryStock)objDomain;
//                sqlListParam = new List<SqlParameter>();
//                sqlListParam.Add(new SqlParameter("@Status", objInventoryStock.Status));
//                sqlListParam.Add(new SqlParameter("@ItemID", objInventoryStock.ItemID));
//                sqlListParam.Add(new SqlParameter("@Quantity", objInventoryStock.Quantity));
//                sqlListParam.Add(new SqlParameter("@DepoID", objInventoryStock.DepoID));

//                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateInventoryStock");

//                strError = dataAccess.Error;

//                return intResult;

//            }
//            catch (Exception ex)
//            {
//                strError = ex.Message;
//                return -1;
//            }
//        }

//        public int UpdateStock(object objDomain)
//        {
//            try
//            {
//                objInventoryStock = (InventoryStock)objDomain;
//                sqlListParam = new List<SqlParameter>();
//                sqlListParam.Add(new SqlParameter("@ID", objInventoryStock.ID));
//                sqlListParam.Add(new SqlParameter("@Quantity", objInventoryStock.Quantity));

//                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateStock");

//                strError = dataAccess.Error;

//                return intResult;

//            }
//            catch (Exception ex)
//            {
//                strError = ex.Message;
//                return -1;
//            }
//        }


//        public override int Delete(object objDomain)
//        {

//            return 0;
//        }

//        public override ArrayList Retrieve()
//        {
//            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select InventoryStock.ID,InventoryStock.ItemID,Items.ItemCode,Items.Description as ItemName,InventoryStock.Quantity,InventoryStock.TempQuantity,InventoryStock.TransitQuantity,InventoryStock.ReceiveQuantity,InventoryStock.DepoID,Depo.DepoName from InventoryStock,Items,Depo where InventoryStock.ItemID = Items.ID and InventoryStock.DepoID = Depo.ID");
//            strError = dataAccess.Error;
//            arrInventoryStock = new ArrayList();

//            if (sqlDataReader.HasRows)
//            {
//                while (sqlDataReader.Read())
//                {
//                    arrInventoryStock.Add(GenerateObject(sqlDataReader));
//                }
//            }
//            else
//                arrInventoryStock.Add(new InventoryStock());

//            return arrInventoryStock;
//        }


//        public InventoryStock RetrieveByItemCodeAndDepo(int itemID, int depoID)
//        {
//            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select InventoryStock.ID,InventoryStock.ItemID,Items.ItemCode,Items.Description as ItemName,InventoryStock.Quantity,InventoryStock.TempQuantity,InventoryStock.TransitQuantity,InventoryStock.ReceiveQuantity,InventoryStock.DepoID,Depo.DepoName from InventoryStock,Items,Depo where InventoryStock.ItemID = Items.ID and InventoryStock.DepoID = Depo.ID  and InventoryStock.ItemID = " + itemID + " and InventoryStock.DepoID = " + depoID);
//            strError = dataAccess.Error;
//            arrInventoryStock = new ArrayList();

//            if (sqlDataReader.HasRows)
//            {
//                while (sqlDataReader.Read())
//                {
//                    return GenerateObject(sqlDataReader);
//                }
//            }

//            return new InventoryStock();
//        }

//        public ArrayList RetrieveByDepo(int depoID)
//        {
//            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select InventoryStock.ID,InventoryStock.ItemID,Items.ItemCode,Items.Description as ItemName,InventoryStock.Quantity,InventoryStock.TempQuantity,InventoryStock.TransitQuantity,InventoryStock.ReceiveQuantity,InventoryStock.DepoID,Depo.DepoName from InventoryStock,Items,Depo where InventoryStock.ItemID = Items.ID and InventoryStock.DepoID = Depo.ID and InventoryStock.DepoID = " + depoID);
//            strError = dataAccess.Error;
//            arrInventoryStock = new ArrayList();

//            if (sqlDataReader.HasRows)
//            {
//                while (sqlDataReader.Read())
//                {
//                    arrInventoryStock.Add(GenerateObject(sqlDataReader));
//                }
//            }
//            else
//                arrInventoryStock.Add(new InventoryStock());

//            return arrInventoryStock;
//        }

//        public ArrayList RetrieveByItem(int itemID)
//        {
//            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select InventoryStock.ID,InventoryStock.ItemID,Items.ItemCode,Items.Description as ItemName,InventoryStock.Quantity,InventoryStock.TempQuantity,InventoryStock.TransitQuantity,InventoryStock.ReceiveQuantity,InventoryStock.DepoID,Depo.DepoName from InventoryStock,Items,Depo where InventoryStock.ItemID = Items.ID and InventoryStock.DepoID = Depo.ID and InventoryStock.ItemID = " + itemID);
//            strError = dataAccess.Error;
//            arrInventoryStock = new ArrayList();

//            if (sqlDataReader.HasRows)
//            {
//                while (sqlDataReader.Read())
//                {
//                    arrInventoryStock.Add(GenerateObject(sqlDataReader));
//                }
//            }
//            else
//                arrInventoryStock.Add(new InventoryStock());

//            return arrInventoryStock;
//        }

//        public InventoryStock GenerateObject(SqlDataReader sqlDataReader)
//        {
//            objInventoryStock = new InventoryStock();
//            objInventoryStock.ID = Convert.ToInt32(sqlDataReader["ID"]);
//            objInventoryStock.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
//            objInventoryStock.ItemCode = sqlDataReader["ItemCode"].ToString();
//            objInventoryStock.ItemName = sqlDataReader["ItemName"].ToString();
//            objInventoryStock.Quantity = Convert.ToInt32(sqlDataReader["Quantity"]);
//            objInventoryStock.TempQuantity = Convert.ToInt32(sqlDataReader["TempQuantity"]);
//            objInventoryStock.TransitQuantity = Convert.ToInt32(sqlDataReader["TransitQuantity"]);
//            objInventoryStock.ReceiveQuantity = Convert.ToInt32(sqlDataReader["ReceiveQuantity"]);
//            objInventoryStock.DepoID = Convert.ToInt32(sqlDataReader["DepoID"]);
//            objInventoryStock.DepoName = sqlDataReader["DepoName"].ToString();
//            return objInventoryStock;

//        }
//    }
//}
