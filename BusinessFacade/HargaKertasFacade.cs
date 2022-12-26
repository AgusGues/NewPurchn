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
    public class HargaKertasFacade : AbstractFacade
    {
        private HargaKertas objHargaKertas = new HargaKertas();
        private ArrayList arrKertas;
        private List<SqlParameter> sqlListParam;


        public HargaKertasFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objHargaKertas = (HargaKertas)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@SupplierID", objHargaKertas.SupplierID));
                sqlListParam.Add(new SqlParameter("@Harga", objHargaKertas.Harga));
                sqlListParam.Add(new SqlParameter("@ItemID", objHargaKertas.ItemID));
                sqlListParam.Add(new SqlParameter("@AddPrice", objHargaKertas.AddPrice));
                sqlListParam.Add(new SqlParameter("@MinPrice", objHargaKertas.MinPrice));
                sqlListParam.Add(new SqlParameter("@KadarAir", objHargaKertas.Kadarair));
                sqlListParam.Add(new SqlParameter("@Aktif", objHargaKertas.Aktif));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objHargaKertas.CreatedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objHargaKertas.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@PriceList", objHargaKertas.PriceList));
                sqlListParam.Add(new SqlParameter("@PriceBeli", objHargaKertas.PriceBeli));
                sqlListParam.Add(new SqlParameter("@SubComp", objHargaKertas.SubComp));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertHargaKertas");
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
            return 0;
        }

        public override int Delete(object objDomain)
        {
            try
            {
                objHargaKertas = (HargaKertas)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@SupplierID", objHargaKertas.SupplierID));
                sqlListParam.Add(new SqlParameter("@ItemID", objHargaKertas.ItemID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objHargaKertas.LastModifiedBy));
                int intResult = dataAccess.ProcessData(sqlListParam, "spNonAktifHargaKertas");

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
            string strSQL = "select A.ID,A.ItemID,C.ItemCode,C.itemname,A.SupplierID,B.SupplierCode,B.SupplierName," +
                "A.Harga,A.PriceList,A.minprice,A.kadarair,A.Aktif,isnull(A.PriceBeli,0) PriceBeli,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from HargaKertas as A," +
                "SuppPurch as B, Inventory C where A.itemid=c.id and A.SupplierID = B.ID   ORDER by B.SupplierName,C.itemname ";//and A.Aktif > 0
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrKertas = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrKertas.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrKertas.Add(new HargaKertas());

            return arrKertas;
        }
        public ArrayList RetrieveByCompany(int id)
        {
            string strSQL = "select A.ID,A.ItemID,C.ItemCode,C.itemname,A.SupplierID,B.SupplierCode,B.SupplierName," +
                "A.Harga,A.PriceList,A.minprice,A.kadarair,A.Aktif,isnull(A.PriceBeli,0) PriceBeli,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from HargaKertas as A," +
                "SuppPurch as B, Inventory C where A.itemid=c.id and A.SupplierID = B.ID and A.SubCompanyID="+id+"   ORDER by B.SupplierName,C.itemname ";//and A.Aktif > 0
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrKertas = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrKertas.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrKertas.Add(new HargaKertas());

            return arrKertas;
        }

        public  ArrayList Retrieve1()
        {
            string strSQL = "select A.ID,A.ItemID,C.ItemCode,C.itemname,A.SupplierID,B.SupplierCode,B.SupplierName," +
                "A.Harga,A.PriceList,A.minprice,A.kadarair,A.Aktif,isnull(A.PriceBeli,0) PriceBeli,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from HargaKertas as A," +
                "SuppPurch as B, Inventory C where A.itemid=c.id and A.SupplierID = B.ID   ORDER by B.SupplierName,C.itemname ";//and A.Aktif > 0
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrKertas = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrKertas.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrKertas.Add(new HargaKertas());

            return arrKertas;
        }
        public HargaKertas RetrieveBySuppID(int suppID)
        {
            string strSQL = "select top 50 A.ID,A.ItemID,C.ItemCode,C.itemname,A.SupplierID,B.SupplierCode,B.SupplierName," +
                "A.Harga,A.PriceList,A.minprice,A.kadarair,A.Aktif,isnull(A.PriceBeli,0) PriceBeli,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from HargaKertas as A," +
                "SuppPurch as B, Inventory C where A.itemid=c.id and A.SupplierID = B.ID and A.SupplierID = " + suppID + "  ORDER by ID DESC";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);//and A.Aktif > 0
             strError = dataAccess.Error;
            //arrGrade = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new HargaKertas();
        }

        public ArrayList RetrieveBySuppID1(int supp, int subComp)
        {
            string strSQL = "select top 50 A.ID,A.ItemID,C.ItemCode,C.itemname,A.SupplierID,B.SupplierCode,B.SupplierName," +
                "A.Harga,A.PriceList,A.minprice,A.kadarair,A.Aktif,isnull(A.PriceBeli,0) PriceBeli,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from HargaKertas as A," +
                "SuppPurch as B, Inventory C where A.itemid=c.id and A.SupplierID = B.ID and B.ID = " + supp + " and A.SubCompanyID="+subComp+" ORDER by ID DESC";//and A.Aktif > 0 
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrKertas = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrKertas.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrKertas.Add(new HargaKertas());

            return arrKertas;
        }
        
        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            strField = "B." + strField;
            string strSQL = "select top 50 A.ID,A.ItemID,C.ItemCode,C.itemname,A.SupplierID,B.SupplierCode,B.SupplierName," +
                "A.Harga,A.PriceList,A.minprice,A.kadarair,A.Aktif,isnull(A.PriceBeli,0) PriceBeli,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from HargaKertas as A," +
                "SuppPurch as B, Inventory C where A.itemid=c.id and A.SupplierID = B.ID and " + strField + " like '%" + strValue + "%' order by ID DESC";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrKertas = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrKertas.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrKertas.Add(new HargaKertas());

            return arrKertas;
        }

        public HargaKertas GenerateObject(SqlDataReader sqlDataReader)
        {
            objHargaKertas = new HargaKertas();
            objHargaKertas.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objHargaKertas.SupplierID = Convert.ToInt32(sqlDataReader["SupplierID"]);
            objHargaKertas.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objHargaKertas.ItemName  = sqlDataReader["ItemName"].ToString();
            objHargaKertas.ItemCode = sqlDataReader["ItemCode"].ToString();
            objHargaKertas.SupplierName = sqlDataReader["SupplierName"].ToString();
            objHargaKertas.SupplierCode = sqlDataReader["SupplierCode"].ToString();
            objHargaKertas.PriceBeli = Convert.ToDecimal(sqlDataReader["PriceBeli"].ToString());
            objHargaKertas.PriceList = Convert.ToDecimal(sqlDataReader["PriceList"].ToString());
            objHargaKertas.MinPrice = Convert.ToDecimal(sqlDataReader["minprice"].ToString());
            objHargaKertas.Kadarair = decimal.Parse(sqlDataReader["kadarair"].ToString());
            objHargaKertas.Harga = Convert.ToDecimal(sqlDataReader["Harga"].ToString());
            objHargaKertas.Aktif = Convert.ToInt32(sqlDataReader["Aktif"].ToString());
            objHargaKertas.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objHargaKertas.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objHargaKertas.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objHargaKertas.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
           
            return objHargaKertas;
        }
    }
}
