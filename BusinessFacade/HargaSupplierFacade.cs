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
    public class HargaSupplierFacade : AbstractFacade
    {
        private HargaSupplier objData = new HargaSupplier();
        private ArrayList arrData;
        private List<SqlParameter> sqlListParam;
        public HargaSupplierFacade()
            : base()
        {

        }

        public override int Delete(object objDomain)
        {
            try
            {
                objData = (HargaSupplier)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@SupplierId", objData.SupplierId));
                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteHargaSupplier");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Insert(object objDomain)
        {
            try
            {
                objData = (HargaSupplier)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@SupplierId", objData.SupplierId));
                sqlListParam.Add(new SqlParameter("@HargaId", objData.IdHarga));
                sqlListParam.Add(new SqlParameter("@PlanId", objData.Plan));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertHargaSupplier");
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
            throw new NotImplementedException();
        }

        public override ArrayList Retrieve()
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(
"WITH q AS ( " +
    "SELECT DISTINCT SupplierID FROM DeliveryKertas WHERE RowStatus > -1 " +
") " +
"SELECT s.ID SupplierId, s.SupplierName FROM q, SuppPurch s " +
"WHERE q.SupplierID = s.id AND s.RowStatus > -1 ORDER BY s.SupplierName ");
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrData.Add(new HargaSupplier());

            return arrData;
        }


        public HargaSupplier GenerateObject(SqlDataReader sqlDatRolesder)
        {
            objData = new HargaSupplier();
            objData.SupplierId = Convert.ToInt32(sqlDatRolesder["SupplierId"]);
            objData.SupplierName = sqlDatRolesder["SupplierName"].ToString();
            return objData;
        }

        public ArrayList LoadDataSup()
        {
            string strSQL =
               "WITH q AS( " +
               "     SELECT DISTINCT CASE PlantID WHEN 1 THEN 'CTEUREUP' WHEN 7 THEN 'KARAWANG' ELSE 'JOMBANG' END PlanName, plantid, SupplierID " +

               "     FROM DeliveryKertas WHERE RowStatus > -1 and year(tglkirim)>= 2020 " +
               " ) , " +
               " c as (select id, suppliercode, suppliername from [sqlctrp.grcboard.com].bpasctrp.dbo.SuppPurch where id in (select supplierid from q)), " +
               " k as (select id, suppliercode, suppliername from SuppPurch where id in (select supplierid from q)), " +
               " j as (select id, suppliercode, suppliername from [sqljombang.grcboard.com].bpasjombang.dbo.SuppPurch where id in (select supplierid from q)) " +

               "SELECT q.PlanName, q.SupplierID, " +
               "case plantid when 1 then(select suppliercode from c where id = q.SupplierID) when 7 then(select suppliercode from k where id = q.SupplierID) when 13 then(select suppliercode from j where id = q.SupplierID) end suppliercode, " +
               "case plantid when 1 then(select suppliername from c where id = q.SupplierID) when 7 then(select suppliername from k where id = q.SupplierID) when 13 then(select suppliername from j where id = q.SupplierID) end suppliername " +
               "FROM q  order by suppliername,PlanName  ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObject4(sqlDataReader));
                }
            }
            else
                arrData.Add(new HargaSupplier());
            return arrData;
        }

        public ArrayList LoadDataSupW(string text)
        {
            string strSQL =
"WITH q AS ( " +
    "SELECT DISTINCT CASE PlantID WHEN 1 THEN 'ctrp' WHEN 7 THEN 'krwg' ELSE 'jmb' END PlanName, SupplierID " +
    "FROM DeliveryKertas WHERE RowStatus > -1 " +
") " +
"SELECT q.PlanName, s.ID SupplierId, s.SupplierCode, s.SupplierName FROM q, SuppPurch s " +
"WHERE q.SupplierID = s.id AND s.RowStatus > -1 "+
"and (q.PlanName like '%"+text+ "%' or s.SupplierCode like '%" + text + "%' or s.SupplierName like '%" + text + "%') "+
"ORDER BY s.SupplierName ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObject4(sqlDataReader));
                }
            }
            else
                arrData.Add(new HargaSupplier());
            return arrData;
        }

        public HargaSupplier GenerateObject4(SqlDataReader sqlDatRolesder)
        {
            objData = new HargaSupplier();
            objData.SupplierId = Convert.ToInt32(sqlDatRolesder["SupplierId"]);
            objData.SupplierCode = sqlDatRolesder["SupplierCode"].ToString();
            objData.SupplierName = sqlDatRolesder["SupplierName"].ToString();
            objData.PlanName = sqlDatRolesder["PlanName"].ToString();
            return objData;
        }

        public ArrayList Retrieve2()
        {
            string strSQL =
                "SELECT h.Id IdHarga, h.ItemCode, i.ItemName, h.Harga FROM HargaKertasDepo h, Inventory i " +
                "WHERE h.ItemCode = i.ItemCode  AND h.RowStatus > -1 AND i.RowStatus > -1 order by i.ItemName, h.Harga";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrData.Add(new HargaSupplier());
            return arrData;
        }

        public HargaSupplier GenerateObject2(SqlDataReader sqlDatRolesder)
        {
            objData = new HargaSupplier();
            objData.IdHarga = int.Parse(sqlDatRolesder["IdHarga"].ToString());
            objData.ItemCode = sqlDatRolesder["ItemCode"].ToString();
            objData.ItemName = sqlDatRolesder["ItemName"].ToString();
            objData.Harga = Decimal.Parse(sqlDatRolesder["Harga"].ToString());
            return objData;
        }

        public ArrayList WhereHargaSup(int Id)
        {
            string strSQL =
            "SELECT h.Id IdHargaD, h.ItemCode ItemCodeD, i.ItemName ItemNameD, h.Harga HargaD " +
            "FROM SuppPurchHrgKertasDepo q, HargaKertasDepo h, Inventory i " +
            "WHERE q.HargaID = h.ID AND h.ItemCode = i.ItemCode AND q.RowStatus > -1 " +
            "AND h.RowStatus > -1 AND i.RowStatus > -1 and q.SupplierID = " + Id;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObject3(sqlDataReader));
                }
            }
            else
                arrData.Add(new HargaSupplier());
            return arrData;
        }

        public HargaSupplier GenerateObject3(SqlDataReader sqlDatRolesder)
        {
            objData = new HargaSupplier();
            objData.IdHargaD = int.Parse(sqlDatRolesder["IdHargaD"].ToString());
            objData.ItemCodeD = sqlDatRolesder["ItemCodeD"].ToString();
            objData.ItemNameD = sqlDatRolesder["ItemNameD"].ToString();
            objData.HargaD = Decimal.Parse(sqlDatRolesder["HargaD"].ToString());
            return objData;
        }


    }
}
