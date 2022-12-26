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
    public class AdjustFacade : AbstractTransactionFacade
    {
        private Adjust objAdjust = new Adjust();
        private ArrayList arrAdjust;
        private List<SqlParameter> sqlListParam;

        public AdjustFacade(object objDomain)
            : base(objDomain)
        {
            objAdjust = (Adjust)objDomain;
        }

        public AdjustFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@AdjustNo", objAdjust.AdjustNo));
                sqlListParam.Add(new SqlParameter("@AdjustDate", objAdjust.AdjustDate));
                sqlListParam.Add(new SqlParameter("@AdjustType", objAdjust.AdjustType));
                sqlListParam.Add(new SqlParameter("@Status", objAdjust.Status));
                sqlListParam.Add(new SqlParameter("@Keterangan1", objAdjust.Keterangan1));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objAdjust.CreatedBy));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objAdjust.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@NonStok", objAdjust.NonStok));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertAdjust");

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
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objAdjust.ID));
                sqlListParam.Add(new SqlParameter("@AdjustNo", objAdjust.AdjustNo));
                sqlListParam.Add(new SqlParameter("@AdjustDate", objAdjust.AdjustDate));
                sqlListParam.Add(new SqlParameter("@AdjustType", objAdjust.AdjustType));
                sqlListParam.Add(new SqlParameter("@Keterangan1", objAdjust.Keterangan1));
                sqlListParam.Add(new SqlParameter("@AlasanCancel", objAdjust.AlasanCancel));
                sqlListParam.Add(new SqlParameter("@Status", objAdjust.Status));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objAdjust.CreatedBy));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objAdjust.ItemTypeID));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateAdjust");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Delete(TransactionManager transManager)
        {

            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objAdjust.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objAdjust.LastModifiedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spCancelAdjust");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int Cancel(TransactionManager transManager)
        {

            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objAdjust.ID));
                //sqlListParam.Add(new SqlParameter("@LastModifiedBy", objAdjust.LastModifiedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spCancelAdjustDetail");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public string Criteria { get; set; }
        public override ArrayList Retrieve()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from Adjust as A where A.Status>-1 " + this.Criteria + " order by ID");
            strError = dataAccess.Error;
            arrAdjust = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrAdjust.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrAdjust.Add(new Adjust());

            return arrAdjust;
        }

        public ArrayList RetrieveOpenStatus()
        {
            string strSQL = "select top 50 A.ID,A.AdjustNo,A.AdjustDate,A.AdjustType,A.Status,A.CreatedBy,B.Quantity, "+
                            " CASE A.ItemTypeID  " +
                            "     WHEN 1 Then C.ItemCode " +
                            "     WHEN 2 Then (select ItemCode from Asset where ID=ItemID) " +
                            "     WHEN 3 THEN (select ItemCode from Biaya where ID=ItemID) END ItemCode, " +
                            " CASE A.ItemTypeID  " +
                            "     WHEN 1 Then C.ItemName " +
                            "     WHEN 2 Then (select ItemName from Asset where ID=ItemID) " +
                            "     WHEN 3 THEN (select ItemName from Biaya where ID=ItemID) END ItemName " +
                            "from Adjust as A, AdjustDetail as B,Inventory as C where A.Status=0 and A.ID=B.AdjustID and B.ItemID=C.ID "+
                            "and B.RowStatus>-1 order by A.AdjustNo desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrAdjust = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrAdjust.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrAdjust.Add(new Adjust());

            return arrAdjust;
        }

        public ArrayList RetrieveByKurangTgl(int tgl, int bln, int thn, int itemTypeID)
        {
            string strSQL = "select A.ID,A.AdjustNo,A.AdjustDate,A.AdjustType,A.Status,A.CreatedBy,A.ItemTypeID "+
                            "from Adjust as A where A.Status>-1 and day(A.AdjustDate) <" + tgl + " and month(A.AdjustDate) =" + bln +
                            "and year(A.AdjustDate) =" + thn + " and A.ItemTypeID =" + itemTypeID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrAdjust = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrAdjust.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrAdjust.Add(new Adjust());

            return arrAdjust;
        }
        public ArrayList RetrieveBySamaTgl(int tgl, int bln, int thn, int itemTypeID)
        {
            string strSQL = "select A.ID,A.AdjustNo,A.AdjustDate,A.AdjustType,A.Status,A.CreatedBy,A.ItemTypeID "+
                            "from Adjust as A where A.Status>-1 and day(A.AdjustDate) =" + tgl + " and month(A.AdjustDate) =" + bln + 
                            " and year(A.AdjustDate) =" + thn + " and A.ItemTypeID =" + itemTypeID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrAdjust = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrAdjust.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrAdjust.Add(new Adjust());

            return arrAdjust;
        }
        public ArrayList RetrieveByTgl(int tgl, int bln, int thn)
        {
            string strSQL = "select A.ID,A.AdjustNo,A.AdjustDate,A.AdjustType,A.Status,A.CreatedBy,A.ItemTypeID "+
                            "from Adjust as A where A.Status>-1 and day(A.AdjustDate)<=" + tgl + " and month(A.AdjustDate) =" + bln + 
                            " and year(A.AdjustDate) =" + thn;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrAdjust = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrAdjust.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrAdjust.Add(new Adjust());

            return arrAdjust;
        }

        public ArrayList RetrieveByPeriode(string drTgl, string sdTgl, int itemTypeID)
        {
            string strSQL = "select A.ID,A.AdjustNo,A.AdjustDate,A.AdjustType,A.Status,A.CreatedBy,A.ItemTypeID "+
                            "from Adjust as A where A.Status>-1 and convert(varchar,A.AdjustDate,112) >= '" + drTgl + "' "+
                            "and convert(varchar,A.AdjustDate,112) <='" + sdTgl + "' and A.ItemTypeID = " + itemTypeID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrAdjust = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrAdjust.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrAdjust.Add(new Adjust());

            return arrAdjust;
        }

        public ArrayList RetrieveOpenStatusForAll(string thbl, int itemTypeID, int groupID)
        {
            string strSQL = "select distinct A.ID,A.AdjustNo,A.AdjustDate,A.AdjustType,A.Status,A.CreatedBy,B.ItemTypeID " +
                "from Adjust as A, AdjustDetail as B where A.ID=B.AdjustID and A.Status>-1 and B.RowStatus>-1 and B.ItemTypeID=" + itemTypeID + 
                " and B.GroupID=" + groupID +
                " and LEFT(convert(varchar,A.AdjustDate,112),6) = '" + thbl + "' order by A.AdjustNo desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrAdjust = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrAdjust.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrAdjust.Add(new Adjust());

            return arrAdjust;
        }
        
        public ArrayList RetrieveOpenStatusByNo(string adjustNo)
        {
            string strSQL = "select A.ID,A.AdjustNo,A.AdjustDate,A.AdjustType,A.Status,A.CreatedBy,B.Quantity,"+
                            " CASE A.ItemTypeID  " +
                            "     WHEN 1 Then C.ItemCode " +
                            "     WHEN 2 Then (select ItemCode from Asset where ID=ItemID) " +
                            "     WHEN 3 THEN (select ItemCode from Biaya where ID=ItemID) END ItemCode, " +
                            " CASE A.ItemTypeID  " +
                            "     WHEN 1 Then C.ItemName " +
                            "     WHEN 2 Then (select ItemName from Asset where ID=ItemID) " +
                            "     WHEN 3 THEN (select ItemName from Biaya where ID=ItemID) END ItemName " +
                            "from Adjust as A, AdjustDetail as B,Inventory as C where A.Status=0 and A.ID=B.AdjustID and B.ItemID=C.ID "+
                            "and B.RowStatus>-1 and A.AdjustNo='" + adjustNo + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrAdjust = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrAdjust.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrAdjust.Add(new Adjust());

            return arrAdjust;
        }

        public Adjust RetrieveByNo(string adjustNo)
        {
            string strSQL="select A.ID,A.AdjustNo,A.AdjustDate,A.AdjustType,A.Status,A.CreatedBy,B.Quantity, "+
                           " CASE A.ItemTypeID  " +
                            "     WHEN 1 Then C.ItemCode " +
                            "     WHEN 2 Then (select ItemCode from Asset where ID=ItemID) " +
                            "     WHEN 3 THEN (select ItemCode from Biaya where ID=ItemID) END ItemCode, " +
                            " CASE A.ItemTypeID  " +
                            "     WHEN 1 Then C.ItemName " +
                            "     WHEN 2 Then (select ItemName from Asset where ID=ItemID) " +
                            "     WHEN 3 THEN (select ItemName from Biaya where ID=ItemID) END ItemName " +
                            ",A.ItemTypeID from Adjust as A, AdjustDetail as B,Inventory as C "+
                            "where A.Status=0 and A.ID=B.AdjustID and B.ItemID=C.ID and B.RowStatus>-1 and A.AdjustNo='" + adjustNo + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrAdjust = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Adjust();
        }

        public ArrayList RetrieveByKurangTgl2(int tgl, int bln, int thn, int itemTypeID, int groupID)
        {
            string strSQL = "select A.ID,A.AdjustNo,A.AdjustDate,A.AdjustType,A.Status,A.CreatedBy,A.ItemTypeID " +
                            "from Adjust as A,AdjustDetail as B where A.ID=B.AdjustID and B.RowStatus>-1 and " +
                            "A.Status>-1 and day(A.AdjustDate) <" + tgl + " and month(A.AdjustDate) =" + bln + " and year(A.AdjustDate) =" + thn + 
                            " and B.ItemTypeID =" + itemTypeID + " and B.GroupID=" + groupID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrAdjust = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrAdjust.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrAdjust.Add(new Adjust());

            return arrAdjust;
        }

        public ArrayList RetrieveBySamaTgl2(int tgl, int bln, int thn, int itemTypeID, int groupID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select distinct A.ID,A.AdjustNo,A.AdjustDate,A.AdjustType,A.Status,A.CreatedBy,A.ItemTypeID " +
                            "from Adjust as A,AdjustDetail as B where A.ID=B.AdjustID and B.RowStatus>-1 and " +
                            "A.Status>-1 and day(A.AdjustDate) =" + tgl + " and month(A.AdjustDate) =" + bln + 
                            " and year(A.AdjustDate) =" + thn + " and B.ItemTypeID =" + itemTypeID + " and B.GroupID=" + groupID;
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrAdjust = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrAdjust.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrAdjust.Add(new Adjust());

            return arrAdjust;
        }


        public Adjust GenerateObject(SqlDataReader sqlDataReader)
        {
            objAdjust = new Adjust();
            objAdjust.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objAdjust.AdjustNo = sqlDataReader["AdjustNo"].ToString();
            objAdjust.AdjustDate = Convert.ToDateTime(sqlDataReader["AdjustDate"]);
            objAdjust.AdjustType = sqlDataReader["AdjustType"].ToString();
            objAdjust.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objAdjust.CreatedBy = sqlDataReader["CreatedBy"].ToString();

            objAdjust.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"]);
            return objAdjust;
        }


        public Adjust GenerateObjectList(SqlDataReader sqlDataReader)
        {
            objAdjust = new Adjust();
            objAdjust.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objAdjust.AdjustNo = sqlDataReader["AdjustNo"].ToString();
            objAdjust.AdjustDate = Convert.ToDateTime(sqlDataReader["AdjustDate"]);
            objAdjust.AdjustType = sqlDataReader["AdjustType"].ToString();
            objAdjust.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objAdjust.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objAdjust.ItemCode = sqlDataReader["ItemCode"].ToString();
            objAdjust.ItemName = sqlDataReader["ItemName"].ToString();
            objAdjust.Quantity = Convert.ToDecimal(sqlDataReader["Quantity"]);

            return objAdjust;
        }
        private Adjust GenerateObjectList(SqlDataReader sqlDataReader, Adjust adjust)
        {
            objAdjust = (Adjust)adjust;
            objAdjust.Keterangan = sqlDataReader["Keterangan"].ToString();
            return objAdjust;
        }
        private Adjust GenerateObjectList2(SqlDataReader sdr, Adjust adjust)
        {
            objAdjust = (Adjust)adjust;
            objAdjust.Keterangan = sdr["Keterangan"].ToString();
            objAdjust.KodeAsset = sdr["KodeAsset"].ToString();
            objAdjust.AssetID = int.Parse(sdr["AssetID"].ToString());
            objAdjust.NamaAsset = sdr["NamaAsset"].ToString();
            objAdjust.LokasiAsset = sdr["NamaLokasi"].ToString();
            return objAdjust;
        }
        public Adjust RetrieveAdjustDetail()
        {
            objAdjust = new Adjust();
            string strSQL = "select Ad.ID,Ad.ItemID,Ad.ItemTypeID,Ad.Keterangan,ItemCode,ItemName,UOMCode,Apv,Quantity, " +
                          "A.AdjustNo,A.AdjustType,A.Status,A.CreatedBy,A.AdjustDate "+
                          "from AdjustDetail as Ad " +
                          "LEFT JOIN Adjust as A ON A.ID=Ad.AdjustID "+
                          "LEFT JOIN Asset AS ase ON ase.ID=Ad.ItemID " +
                          "LEFT JOIN UOM as Uom ON Uom.ID=Ad.UomID " +
                          "where Ad.RowStatus>-1 " + this.Criteria;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    objAdjust = GenerateObjectList(sdr,GenerateObjectList(sdr));
                }
            }
            return objAdjust;
        }
        public ArrayList RetrieveAdjustDetail(string AdjustID)
        {
            arrAdjust = new ArrayList();
            string strSQL = "select Ad.ID,Ad.ItemID,Ad.ItemTypeID,Ad.Keterangan,ItemCode,ItemName,UOMCode,Apv,Quantity, " +
                          "A.AdjustNo,A.AdjustType,A.Status,A.CreatedBy,A.AdjustDate " +
                          ",ISNULL(ama.ID,0) as AssetID,ama.KodeAsset,ama.NamaAsset,ama.LokasiID,alo.NamaLokasi " +
                          "FROM AdjustDetail as Ad " +
                          "LEFT JOIN Adjust as A ON A.ID=Ad.AdjustID " +
                          "LEFT JOIN Asset AS ase ON ase.ID=Ad.ItemID " +
                          "LEFT JOIN UOM as Uom ON Uom.ID=Ad.UomID " +
                          "LEFt JOIN AM_Asset as ama ON ama.ItemKode=ase.ItemCode " +
                          "LEFT JOIN AM_Lokasi as alo ON alo.ID=ama.LokasiID "+
                          "WHERE Ad.RowStatus>-1 AND ad.AdjustID=" + AdjustID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrAdjust.Add(GenerateObjectList2(sdr, GenerateObjectList(sdr)));
                }
            }
            return arrAdjust;
        }

    }
}
