using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;
using BusinessFacade;

namespace BusinessFacade
{
    public class KasbonDetailFacade : AbstractTransactionFacade
    {
        private KasbonDetail objRDetail= new KasbonDetail();
        private ArrayList arrRDetail;
        private List<SqlParameter> sqlListParam;

        public KasbonDetailFacade(object objDomain)
            : base(objDomain)
        {
            objRDetail = (KasbonDetail)objDomain;
        }
        public KasbonDetailFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@KID", objRDetail.KID));
                sqlListParam.Add(new SqlParameter("@SPPID", objRDetail.SPPID));
                //sqlListParam.Add(new SqlParameter("@POID", objRDetail.POID));
                sqlListParam.Add(new SqlParameter("@SPPDetailID", objRDetail.SPPDetailID));
                sqlListParam.Add(new SqlParameter("@ItemID", objRDetail.ItemID));
                sqlListParam.Add(new SqlParameter("@EstimasiKasbon", objRDetail.EstimasiKasbon));
                //sqlListParam.Add(new SqlParameter("@EstimasiHarga", objRDetail.EstimasiHarga));
                sqlListParam.Add(new SqlParameter("@Qty", objRDetail.Qty));
                //sqlListParam.Add(new SqlParameter("@QtyPO", objRDetail.QtyPO));
                sqlListParam.Add(new SqlParameter("@UOMID", objRDetail.UOMID));
                sqlListParam.Add(new SqlParameter("@NamaBarang", objRDetail.NamaBarang));
                sqlListParam.Add(new SqlParameter("@Status", objRDetail.Status));
                int intResult = transManager.DoTransaction(sqlListParam, "[KasbonDetailInsert]");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override System.Collections.ArrayList Retrieve()
        {
            string strSQL = "";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrRDetail = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrRDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrRDetail.Add(new KasbonDetail());
            return arrRDetail;
        }
        public string Where { get; set; }
        public ArrayList RetrieveItemKID(int kID)
        {
            //DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select " +
            //    "A.ID,A.kID,A.SPPID,A.ItemID,A.EstimasiKasbon,A.Qty,A.UomID,B.UOMCode,A.Status,A.SPPDetailID,A.NoSPP, " +
            //    "case A.ItemTypeID when 1  then (select ItemName from Inventory where ID=A.ItemID and RowStatus > -1) " +
            //    "when 2 then (select ItemName from Asset where ID=A.ItemID and RowStatus > -1), " +
            //    //"when 3 then " + ItemSPPBiayaNew() + " end ItemName, " +
            //    "case A.ItemTypeID when 1  then (select ItemCode from Inventory where ID=A.ItemID and RowStatus > -1) " +
            //    "when 2 then (select ItemCode from Asset where ID=A.ItemID and RowStatus > -1) " +
            //    "else (select ItemCode from Biaya where ID=A.ItemID and RowStatus > -1) end ItemCode, " +
            //    "case A.ItemTypeID when 1  then (select isnull(sum(Jumlah),0) from Inventory where ID=A.ItemID and RowStatus > -1) " +
            //    "when 2 then (select isnull(sum(Jumlah),0) from Asset where ID=A.ItemID and RowStatus > -1) " +
            //    //"else " + StockBiayaNew() + " end Stok " +
            //    "from KasbonDetail as A, UOM as B where A.UomID=B.ID and A.KID = " + kID + this.Where);
            string strSQL ="select " +
                "A.ID,A.kID,A.SPPID,A.ItemID,A.ItemName,A.EstimasiKasbon,A.Qty,A.UomID,B.UOMCode,A.Status,A.SPPDetailID,D.NoSPP,C.ItemTypeID,E.DanaCadangan, " +
                "case C.ItemTypeID when 1  then (select ItemCode from Inventory where ID=A.ItemID and RowStatus > -1) " +
                "when 2 then (select ItemCode from Asset where ID=A.ItemID and RowStatus > -1) else " +
                "(select ItemCode from Biaya where ID=A.ItemID and RowStatus > -1) end ItemCode " +
                "from KasbonDetail as A, UOM as B, SPPDetail as C, SPP as D, Kasbon as E where A.UomID=B.ID and A.SPPDetailID=C.ID " +
                "and A.SPPID=D.ID and A.KID=E.ID and A.KID = " + kID + this.Where;
            //strError = dataAccess.Error;
            arrRDetail = new ArrayList();
            DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = da.RetrieveDataByString(strSQL);

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrRDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrRDetail.Add(new KasbonDetail());

            return arrRDetail;
        }

        //public ArrayList RetrieveItemKIDTopUrgent(int kID)
        //{
        //    string strSQL = "select E.NoPengajuan,e.TglKasbon,e.CreatedTime,e.KasbonType,A.ID,A.kID,A.SPPID,A.ItemID,A.ItemName,A.EstimasiKasbon,A.Qty,A.UomID,A.Status,A.SPPDetailID,E.DanaCadangan " +
        //                    "from Kasbon E left join KasbonDetail A on E.ID=A.KID where A.Status>-1 and A.KID = " + kID + " ";
        //    //strError = dataAccess.Error;
        //    arrRDetail = new ArrayList();
        //    DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = da.RetrieveDataByString(strSQL);

        //    if (da.Error == string.Empty && sdr.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrRDetail.Add(GenerateObject2(sqlDataReader));
        //        }
        //    }

        //    return arrRDetail;
        //}

        public ArrayList RetrieveItemKID2(int kID)
        {
            string strSQL = "select A.ID,A.kID,A.SPPID,A.ItemID,A.ItemName,A.EstimasiKasbon,A.Qty,A.UomID,A.Status,A.SPPDetailID,E.DanaCadangan " +
                            "from KasbonDetail as A, Kasbon as E where A.KID=E.ID and A.KID = " + kID + " and A.Status>-1 ";
            //strError = dataAccess.Error;
            arrRDetail = new ArrayList();
            DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = da.RetrieveDataByString(strSQL);

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrRDetail.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrRDetail.Add(new KasbonDetail());

            return arrRDetail;
        }

        public ArrayList RetrieveKID(string NoKasbon)
        {
            //string strSQL = "select " +
            //    "A.ID,A.kID,A.SPPID,A.ItemID,A.ItemName,A.EstimasiKasbon,A.Qty,A.UomID,B.UOMCode,A.Status,A.SPPDetailID,D.NoSPP,p.NoPO, " +
            //    "A.QtyPO AS QtyPO, A.HargaPO AS Price,p.PPN,p.Ongkos,C.ItemTypeID,E.DanaCadangan, " +
            //    "case C.ItemTypeID when 1  then (select ItemCode from Inventory where ID=A.ItemID and RowStatus > -1) " +
            //    "when 2 then (select ItemCode from Asset where ID=A.ItemID and RowStatus > -1) else " +
            //    "(select ItemCode from Biaya where ID=A.ItemID and RowStatus > -1) end ItemCode " +
            //    "from KasbonDetail as A, UOM as B, SPPDetail as C, SPP as D, Kasbon as E, POPurchnDetail AS pd, POPurchn AS p where A.UomID=B.ID and A.SPPDetailID=C.ID " +
            //    "and A.SPPID=D.ID and A.KID=E.ID AND a.PODetailID=pd.ID AND pd.POID=p.ID and A.KID " + 
            //    "IN (SELECT ID FROM Kasbon WHERE KasbonNo='"+ NoKasbon +"') "+ this.Where ;
            string strSQL = "SELECT DISTINCT k.ID,kd.ID AS KDID,u.UOMCode,s.NoSPP,p.NoPO,p.PPN,kd.ItemName, " +
                            "case C.ItemTypeID when 1  then (select ItemCode from Inventory where ID=kd.ItemID and RowStatus > -1) when 2 " +
                            "then (select ItemCode from Asset where ID=kd.ItemID and RowStatus > -1) else (select ItemCode from Biaya where " +
                            "ID=kd.ItemID and RowStatus > -1) end ItemCode,kd.EstimasiKasbon,kd.Qty,pd.Qty AS QtyPO,pd.Price,k.DanaCadangan,k.Pic, " +
                            "case when p.ongkos<1 then p.ongkos else cast(p.ongkos/(select count(ID) from KasbonDetail where kID=k.ID) as decimal(11,2)) end Ongkos FROM Kasbon as k left join KasbonDetail as kd on k.ID=kd.KID LEFT JOIN POPurchnDetail AS pd ON pd.ID=kd.PODetailID " +
                            "LEFT JOIN POPurchn AS p ON p.ID=pd.POID LEFT JOIN uom AS u ON u.id=kd.UomID left join SPPDetail as C on " +
                            "C.ID=kd.SppDetailID left join spp as s on s.ID=c.SPPID WHERE kd.KID IN (SELECT ID FROM Kasbon WHERE KasbonNo='" + NoKasbon + "') " + this.Where;
            //strError = dataAccess.Error;
            arrRDetail = new ArrayList();
            DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = da.RetrieveDataByString(strSQL);

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrRDetail.Add(GenerateObjectKID(sqlDataReader));
                }
            }
            else
                arrRDetail.Add(new KasbonDetail());

            return arrRDetail;
        }




        public override int Update(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        //public override int Insert1(DataAccessLayer.TransactionManager transManager)
        //{
        //    throw new NotImplementedException();
        //}

        //public override int Update1(DataAccessLayer.TransactionManager transManager)
        //{
        //    throw new NotImplementedException();
        //}

        //public override int Update2(DataAccessLayer.TransactionManager transManager)
        //{
        //    throw new NotImplementedException();
        //}

        public override int Delete(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public KasbonDetail GenerateObject(SqlDataReader sqlDataReader)
        {
            objRDetail = new KasbonDetail();
            objRDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objRDetail.KID = Convert.ToInt32(sqlDataReader["KID"]);
            objRDetail.SPPID = Convert.ToInt32(sqlDataReader["SPPID"]);
            objRDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objRDetail.EstimasiKasbon = Convert.ToDecimal(sqlDataReader["EstimasiKasbon"]);
            objRDetail.Qty = Convert.ToDecimal(sqlDataReader["Qty"]);
            objRDetail.NamaBarang = sqlDataReader["ItemName"].ToString();
            objRDetail.ItemCode = sqlDataReader["ItemCode"].ToString();
            objRDetail.UOMCode = sqlDataReader["UOMCode"].ToString();
            objRDetail.UOMID = Convert.ToInt32(sqlDataReader["UomID"]);
            //objRDetail.Stok = Convert.ToDecimal(sqlDataReader["Stok"]);
            objRDetail.NoSPP = sqlDataReader["NoSPP"].ToString();
            objRDetail.DanaCadangan = Convert.ToDecimal(sqlDataReader["DanaCadangan"]);
            return objRDetail;
        }

        public KasbonDetail GenerateObject2(SqlDataReader sqlDataReader)
        {
            objRDetail = new KasbonDetail();
            objRDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objRDetail.KID = Convert.ToInt32(sqlDataReader["KID"]);
            objRDetail.SPPID = Convert.ToInt32(sqlDataReader["SPPID"]);
            objRDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objRDetail.EstimasiKasbon = Convert.ToDecimal(sqlDataReader["EstimasiKasbon"]);
            objRDetail.Qty = Convert.ToDecimal(sqlDataReader["Qty"]);
            objRDetail.NamaBarang = sqlDataReader["ItemName"].ToString();
            objRDetail.UOMID = Convert.ToInt32(sqlDataReader["UomID"]);
            //objRDetail.Stok = Convert.ToDecimal(sqlDataReader["Stok"]);
            objRDetail.DanaCadangan = Convert.ToDecimal(sqlDataReader["DanaCadangan"]);
            return objRDetail;
        }

        public KasbonDetail GenerateObjectKID(SqlDataReader sqlDataReader)
        {
            objRDetail = new KasbonDetail();
            objRDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objRDetail.KDID = Convert.ToInt32(sqlDataReader["KDID"]);
            //objRDetail.SPPID = Convert.ToInt32(sqlDataReader["SPPID"]);
            //objRDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objRDetail.EstimasiKasbon = Convert.ToDecimal(sqlDataReader["EstimasiKasbon"]);
            objRDetail.Price = (sqlDataReader["Price"] == DBNull.Value) ? 0 : Convert.ToDecimal(sqlDataReader["Price"]);
            objRDetail.Qty = (sqlDataReader["Qty"] == DBNull.Value) ? 0 : Convert.ToDecimal(sqlDataReader["Qty"]);
            objRDetail.QtyPO = (sqlDataReader["QtyPO"] == DBNull.Value) ? 0 : Convert.ToDecimal(sqlDataReader["QtyPO"]);
            objRDetail.NamaBarang = sqlDataReader["ItemName"].ToString();
            objRDetail.ItemCode = sqlDataReader["ItemCode"].ToString();
            objRDetail.UOMCode = sqlDataReader["UOMCode"].ToString();
            //objRDetail.UOMID = Convert.ToInt32(sqlDataReader["UomID"]);
            //objRDetail.Stok = Convert.ToDecimal(sqlDataReader["Stok"]);
            objRDetail.NoSPP = sqlDataReader["NoSPP"].ToString();
            objRDetail.NoPO = sqlDataReader["NoPO"].ToString();
            objRDetail.DanaCadangan = Convert.ToDecimal(sqlDataReader["DanaCadangan"]);
            objRDetail.PPN = (sqlDataReader["PPN"] == DBNull.Value) ? 0 : Convert.ToDecimal(sqlDataReader["PPN"]);
            objRDetail.OngkosKirim = (sqlDataReader["Ongkos"] == DBNull.Value) ? 0 : Convert.ToDecimal(sqlDataReader["Ongkos"]);
            return objRDetail;
        }

    }
}
