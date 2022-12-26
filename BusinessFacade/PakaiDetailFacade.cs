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
    public class PakaiDetailFacade : AbstractTransactionFacade
    {
        private PakaiDetail objPakaiDetail = new PakaiDetail();
        private ArrayList arrPakaiDetail;
        private List<SqlParameter> sqlListParam;
        //private string receiptNo = string.Empty;
        private string pakaiNo = string.Empty;

        public PakaiDetailFacade(object objDomain)
            : base(objDomain)
        {
            objPakaiDetail = (PakaiDetail)objDomain;
        }

        public PakaiDetailFacade()
        {

        }

        public PakaiDetailFacade(object objDomain, string strPakaiNo)
        {
            objPakaiDetail = (PakaiDetail)objDomain;
            pakaiNo = strPakaiNo;
        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@PakaiID", objPakaiDetail.PakaiID));
                sqlListParam.Add(new SqlParameter("@ItemID", objPakaiDetail.ItemID));
                sqlListParam.Add(new SqlParameter("@Quantity", objPakaiDetail.Quantity));
                sqlListParam.Add(new SqlParameter("@UomID", objPakaiDetail.UomID));
                sqlListParam.Add(new SqlParameter("@Keterangan", objPakaiDetail.Keterangan));
                sqlListParam.Add(new SqlParameter("@RowStatus", objPakaiDetail.RowStatus));
                sqlListParam.Add(new SqlParameter("@GroupID", objPakaiDetail.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objPakaiDetail.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@avgPrice", objPakaiDetail.AvgPrice));
                sqlListParam.Add(new SqlParameter("@IDBiaya", objPakaiDetail.IDJenisBiaya));
                sqlListParam.Add(new SqlParameter("@ProdLine", objPakaiDetail.LineNo));
                sqlListParam.Add(new SqlParameter("@BudgetQty", objPakaiDetail.BudgetQty));
                sqlListParam.Add(new SqlParameter("@NoPol", objPakaiDetail.NoPol));

                /** Beny : 03 Agustus 2022 **/
                if (objPakaiDetail.Kelompok == null || objPakaiDetail.Kelompok == "")
                {
                    sqlListParam.Add(new SqlParameter("@Kelompok", objPakaiDetail.Kelompok == "0"));
                }
                else
                {
                    sqlListParam.Add(new SqlParameter("@Kelompok", objPakaiDetail.Kelompok));
                }

                if (objPakaiDetail.Press == null || objPakaiDetail.Press == "")
                {
                    sqlListParam.Add(new SqlParameter("@Press", objPakaiDetail.Press == "0"));
                }
                else
                {
                    sqlListParam.Add(new SqlParameter("@Press", objPakaiDetail.Press));
                }
                /** End **/                                              

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertPakaiDetail2");

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
            // perlu di test dulu

            int intResult = Delete(transManager);
            if (strError == string.Empty)
                intResult = Insert(transManager);

            return intResult;
        }

        public override int Delete(TransactionManager transManager)
        {
            try
            {
                objPakaiDetail = (PakaiDetail)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objPakaiDetail.ID));

                int intResult = transManager.DoTransaction(sqlListParam, "spDeletePakaiDetail");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int CancelPakaiDetail(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objPakaiDetail.ID));
                sqlListParam.Add(new SqlParameter("@ItemID", objPakaiDetail.ItemID));
                sqlListParam.Add(new SqlParameter("@Jumlah", objPakaiDetail.Quantity));
                sqlListParam.Add(new SqlParameter("@FlagTipe", objPakaiDetail.FlagTipe));

                int intResult = transManager.DoTransaction(sqlListParam, "spCancelPakaiDetail");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int CancelInventoryByPakaiDetail(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objPakaiDetail.ID ));
                sqlListParam.Add(new SqlParameter("@ItemID", objPakaiDetail.ItemID));
                sqlListParam.Add(new SqlParameter("@Jumlah", objPakaiDetail.Quantity));
                sqlListParam.Add(new SqlParameter("@FlagTipe", objPakaiDetail.FlagTipe));

                int intResult = transManager.DoTransaction(sqlListParam, "spCancelInventoryByPakaiDetail");

                strError = transManager.Error;
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
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from PakaiDetail and RowStatus>-1");
            strError = dataAccess.Error;
            arrPakaiDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPakaiDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrPakaiDetail.Add(new PakaiDetail());

            return arrPakaiDetail;
        }

        public ArrayList RetrieveByPakaiIdForBakuBantu(int Id)
        {
            string strSQL = "select A.ID,A.PakaiID,A.ItemID,A.Quantity,A.UomID,A.RowStatus,A.GroupID,A.Keterangan,B.UOMCode,C.ItemCode,C.ItemName,"+
                            "A.ItemTypeID,A.ProdLine from PakaiDetail as A, UOM as B, Inventory as C where A.RowStatus>-1 AND A.UomID=B.ID and A.ItemID=C.ID "+
                            "and A.GroupID in (1,2) and A.PakaiID=" + Id;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPakaiDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPakaiDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrPakaiDetail.Add(new PakaiDetail());

            return arrPakaiDetail;
        }

        public ArrayList RetrieveByPakaiId(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select A.ID,A.PakaiID,A.ItemID,A.ItemTypeID,A.Quantity,A.UomID,A.RowStatus,A.GroupID,A.Keterangan,B.UOMCode,"+
                            "C.ItemCode,C.ItemName,A.ProdLine from PakaiDetail as A, UOM as B, Inventory as C where A.RowStatus>-1 AND A.UomID=B.ID and "+
                            "A.ItemID=C.ID and A.PakaiID=" + Id;
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPakaiDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPakaiDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrPakaiDetail.Add(new PakaiDetail());

            return arrPakaiDetail;
        }

        public ArrayList RetrieveByPakaiIdwithGroupID(int Id, int groupID)
        {
            string strSQL = "select A.ID,A.PakaiID,A.ItemID,A.ItemTypeID,A.Quantity,A.UomID,A.RowStatus,A.GroupID,A.Keterangan,B.UOMCode,C.ItemCode,"+
                            "C.ItemName,A.ProdLine from PakaiDetail as A, UOM as B, Inventory as C where A.RowStatus>-1 AND A.UomID=B.ID and A.ItemID=C.ID "+
                            "and A.PakaiID=" + Id + " and A.GroupID=" + groupID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPakaiDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPakaiDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrPakaiDetail.Add(new PakaiDetail());

            return arrPakaiDetail;
        }

        public ArrayList RetrieveByPakaiIdForAsset(int Id)
        {
            string strSQL = "select A.ID,A.PakaiID,A.ItemID,A.Quantity,A.UomID,A.RowStatus,A.GroupID,A.Keterangan,A.itemtypeid,B.UOMCode,C.ItemCode,"+
                            "C.ItemName,A.ProdLine from PakaiDetail as A, UOM as B, Asset as C where A.RowStatus>-1 AND A.UomID=B.ID and A.ItemID=C.ID "+
                            "and A.PakaiID=" + Id;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPakaiDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPakaiDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrPakaiDetail.Add(new PakaiDetail());

            return arrPakaiDetail;
        }

        public ArrayList RetrieveByPakaiIdForBiaya(int Id)
        {
            string strSQL = "select A.ID,A.PakaiID,A.ItemID,A.Quantity,A.UomID,A.RowStatus,A.GroupID,A.Keterangan,B.UOMCode,C.ItemCode,"+
                            "C.ItemName,A.itemtypeID,A.ProdLine from PakaiDetail as A, UOM as B, Biaya as C where A.RowStatus>-1 AND A.UomID=B.ID "+
                            "and A.ItemID=C.ID and A.PakaiID=" + Id;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPakaiDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPakaiDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrPakaiDetail.Add(new PakaiDetail());
            return arrPakaiDetail;
        }

        public ArrayList RetrieveByPakaiIdForAll(int Id)
        {
            string strSQL = "select PakaiDetail.itemid, "+
                "case PakaiDetail.ItemTypeID "+
                "when 1 then (select Inventory.ItemName from Inventory where Inventory.ID=PakaiDetail.ItemID) " +
                "when 2 then (select Asset.ItemName from Asset where Asset.ID=PakaiDetail.ItemID) " +
                "when 3 then (select Biaya.ItemName from Biaya where Biaya.ID=PakaiDetail.ItemID) " +
                "else '' end ItemName," +
                "case PakaiDetail.ItemTypeID "+
                "when 1 then (select Inventory.ID from Inventory where Inventory.ID=PakaiDetail.ItemID) " +
                "when 2 then (select Asset.ID from Asset where Asset.ID=PakaiDetail.ItemID) " +
                "when 3 then (select Biaya.ID from Biaya where Biaya.ID=PakaiDetail.ItemID) " +
                "else '' end ID from PakaiDetail where PakaiDetail.RowStatus>-1 and PakaiDetail.PakaiID=" + Id;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPakaiDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPakaiDetail.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrPakaiDetail.Add(new PakaiDetail());

            return arrPakaiDetail;
        }
        
        public ArrayList RetrieveByPakaiNoForAll(string PakaiNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());

            string strSQL = "select PakaiDetail.itemid, case PakaiDetail.ItemTypeID when 1 then (select Inventory.ItemName from Inventory where Inventory.ID=PakaiDetail.ItemID) " +
            "when 2 then (select Asset.ItemName from Asset where Asset.ID=PakaiDetail.ItemID) " +
            "when 3 then (select Biaya.ItemName from Biaya where Biaya.ID=PakaiDetail.ItemID) " +
            "else '' end ItemName," +
            "case PakaiDetail.ItemTypeID when 1 then (select Inventory.ID from Inventory where Inventory.ID=PakaiDetail.ItemID) " +
            "when 2 then (select Asset.ID from Asset where Asset.ID=PakaiDetail.ItemID) " +
            "when 3 then (select Biaya.ID from Biaya where Biaya.ID=PakaiDetail.ItemID) " +
            "else '' end ID from PakaiDetail where PakaiDetail.RowStatus>-1 and PakaiDetail.PakaiID in (select id from pakai where pakaino='" + PakaiNo + "')";
                SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL );
            strError = dataAccess.Error;
            arrPakaiDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPakaiDetail.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrPakaiDetail.Add(new PakaiDetail());

            return arrPakaiDetail;
        }
        public int GetDetailID(string noSPB, string itemID)
        {
            int ID = 0;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select id from PakaiDetail where PakaiID in(select ID from Pakai " +
                "where PakaiNo='" + noSPB + "') and ItemID =" + itemID);
            strError = dataAccess.Error;
            arrPakaiDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    ID = int.Parse(sqlDataReader["id"].ToString());
                }
            }
            return ID;
        }
        public decimal getQuantity(string PakaiNo,int itemID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(
                "select PakaiDetail.Quantity, case PakaiDetail.ItemTypeID when 1 then " +
                "(select Inventory.ItemName from Inventory where Inventory.ID=PakaiDetail.ItemID) " +
                "when 2 then (select Asset.ItemName from Asset where Asset.ID=PakaiDetail.ItemID) " +
                "when 3 then (select Biaya.ItemName from Biaya where Biaya.ID=PakaiDetail.ItemID) " +
                "else '' end ItemName," +
                "case PakaiDetail.ItemTypeID when 1 then (select Inventory.ID from Inventory where " +
                "Inventory.ID=PakaiDetail.ItemID) " +
                "when 2 then (select Asset.ID from Asset where Asset.ID=PakaiDetail.ItemID) " +
                "when 3 then (select Biaya.ID from Biaya where Biaya.ID=PakaiDetail.ItemID) " +
                "else '' end ID from PakaiDetail where PakaiDetail.RowStatus>-1 and PakaiDetail.PakaiID in " +
                "(select id from pakai where pakaino='" + PakaiNo + "') and PakaiDetail.ItemID=" + itemID);
            decimal Quantity = 0;
            strError = dataAccess.Error;
            arrPakaiDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    Quantity = decimal.Parse(sqlDataReader["Quantity"].ToString());
                }
            }
            return Quantity;
        }
        public ArrayList RetrieveByGroupIDwithSUM(int groupID, int itemType, string thbl)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select ItemID,SUM(Quantity) as Quantity,GroupID from ( " +
                    "select B.ItemID,B.Quantity,B.GroupID from Pakai as A,PakaiDetail as B where A.ID=B.PakaiID and A.Status>-1  and B.RowStatus>-1 and " +
                    "B.GroupID=" + groupID + " and B.ItemTypeID=" + itemType + " and LEFT(convert(varchar,A.PakaiDate,112),6) = '" + thbl + "') as P group by GroupID,ItemID order by ItemID");
            strError = dataAccess.Error;
            arrPakaiDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPakaiDetail.Add(GenerateObjectSUM(sqlDataReader));
                }
            }
            else
                arrPakaiDetail.Add(new PakaiDetail());

            return arrPakaiDetail;
        }

        public PakaiDetail GenerateObjectSUM(SqlDataReader sqlDataReader)
        {
            objPakaiDetail = new PakaiDetail();
            objPakaiDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objPakaiDetail.Quantity = Convert.ToDecimal(sqlDataReader["Quantity"]);
            objPakaiDetail.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);

            return objPakaiDetail;
        }

        public PakaiDetail GenerateObject(SqlDataReader sqlDataReader)
        {
           
            objPakaiDetail = new PakaiDetail();
            objPakaiDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objPakaiDetail.PakaiID = Convert.ToInt32(sqlDataReader["PakaiID"]);
            objPakaiDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            //
            objPakaiDetail.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"]);
            objPakaiDetail.Quantity = Convert.ToDecimal(sqlDataReader["Quantity"]);
            objPakaiDetail.Keterangan = sqlDataReader["Keterangan"].ToString();
            objPakaiDetail.UomID = Convert.ToInt32(sqlDataReader["UomID"]);
            objPakaiDetail.UOMCode = sqlDataReader["UOMCode"].ToString();
            objPakaiDetail.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);

            objPakaiDetail.ItemCode = sqlDataReader["ItemCode"].ToString();
            objPakaiDetail.ItemName = sqlDataReader["ItemName"].ToString();
            objPakaiDetail.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objPakaiDetail.LineNo = (sqlDataReader["ProdLine"] == DBNull.Value) ? 0 : Convert.ToInt32(sqlDataReader["ProdLine"].ToString());
            return objPakaiDetail;
        }
        public PakaiDetail GenerateObject2(SqlDataReader sqlDataReader)
        {
            objPakaiDetail = new PakaiDetail();
            objPakaiDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objPakaiDetail.ItemName = sqlDataReader["ItemName"].ToString();
            objPakaiDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            return objPakaiDetail;
        }
        public PakaiDetail GetDateilSPB(int ID)
        {
            objPakaiDetail = new PakaiDetail();
            string strSQL = "Select *,(select dbo.ItemNameInv(ItemID,1))ItemName From PakaiDetail where RowStatus >-1 and ID=" + ID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    return GenerateObject2(sdr);
                }
            }
            return objPakaiDetail;
        }
        public int UpdateZonaMTC(object objDomain)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            int result = 0;
            try
            {
                objPakaiDetail = (PakaiDetail)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objPakaiDetail.ID));
                sqlListParam.Add(new SqlParameter("@ZonaMTC", objPakaiDetail.Zona));
                //result = dataAccess.ProcessData(sqlListParam, "spPakaiDetail_UpdateZona");
                result = dataAccess.ProcessData(sqlListParam, "spPakaiDetail_UpdateZona");
                return result;

            }
            catch { result = 0; }
            return result;
        }
        public PakaiDetail DetailMaterialBudget(string Periode, int ItemID, int ItemTypeID, int DeptID)
        {
            objPakaiDetail = new PakaiDetail();
            string strSQL = "SELECT * FROM BudgetSP WHERE RowStatus>-1 " +
                            "AND (CAST(Tahun AS CHAR(4))+RIGHT('0'+CAST(Bulan AS CHAR),2))='" + Periode + "'" +
                            "AND DeptID=" + DeptID + " AND ItemID=" + ItemID + " AND ItemTypeID=" + ItemTypeID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    objPakaiDetail.ID = Convert.ToInt32(sdr["ID"].ToString());
                    objPakaiDetail.ItemID = Convert.ToInt32(sdr["ItemID"].ToString());
                    objPakaiDetail.ItemTypeID = Convert.ToInt32(sdr["ItemTypeID"].ToString());
                    objPakaiDetail.DeptID = Convert.ToInt32(sdr["DeptID"].ToString());
                    objPakaiDetail.Quantity = Convert.ToDecimal(sdr["MaxQty"].ToString());
                    objPakaiDetail.Tahun = Convert.ToInt32(sdr["Tahun"].ToString());
                    objPakaiDetail.Bulan = Convert.ToInt32(sdr["Bulan"].ToString());
                    objPakaiDetail.AddQty = (sdr["AddQty"] != DBNull.Value) ? Convert.ToDecimal(sdr["AddQty"].ToString()) : 0;
                }
            }
            return objPakaiDetail;
        }
        public decimal TotalQtySPB(string Periode, int ItemID, int ItemTypeID, int DeptID)
        {
            decimal result = 0;
            string strSQL = "select ISNULL(SUM(Quantity),0) as Jumlah from PakaiDetail where PakaiID in(" +
                            "Select ID from Pakai where RowStatus>-1 and LEFT(CONVERT(CHAR,PakaiDate,112),6) between '" + Periode + "'" +
                            "and DeptID=" + DeptID + ") and ItemID=" + ItemID + " and ItemTypeID=" + ItemTypeID + " and RowStatus>-1 ";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToDecimal(sdr["Jumlah"].ToString());
                }
            }
            return result;
        }
        public decimal TotalQtySPBPrj(string project, int ItemID, int ItemTypeID, int deptid)
        {
            decimal result = 0;
            string strSQL = "select ISNULL(SUM(Qty),0) as Jumlah from MTC_Project_Pakai where ItemID=" + ItemID +
                " and ItemTypeID=" + ItemTypeID + " and RowStatus>-1 and projectID=" + project;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToDecimal(sdr["Jumlah"].ToString());
                }
            }
            return result;
        }
        public decimal MaxQtySPB(string Periode, int ItemID, int ItemTypeID,int DeptID)
        {
            decimal result = 0;
            string strSQL = "SELECT CASE WHEN RuleCalc=1 and ISNULL(MasaBerlaku,0) >= MONTH(GETDATE()) then(MaxQty+(ISNULL(AddQty,0)))ELSE (MaxQty)END MaxQty "+
                            "FROM BudgetSP " +
                            "where RowStatus>-1 and (CAST(Tahun as CHAR(4))+RIGHT('0'+RTRIM(LTRIM(CAST(Bulan as CHAR))),2))='" + Periode + "'" +
                            "and DeptID=" + DeptID + " and ItemID=" + ItemID + " and ItemTypeID=" + ItemTypeID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToDecimal(sdr["MaxQty"].ToString());
                }
            }
            return result;
        }
        public decimal AddQtyBudget(string Periode, int ItemID, int ItemTypeID, int DeptID)
        {
            decimal result = 0;
            string strSQL = "SELECT CASE WHEN RuleCalc=1 and ISNULL(MasaBerlaku,0) >= MONTH(GETDATE()) then((ISNULL(AddQty,0)))ELSE 0 END MaxQty FROM BudgetSP " +
                            "where RowStatus>-1 and (CAST(Tahun as CHAR(4))+RIGHT('0'+RTRIM(LTRIM(CAST(Bulan as CHAR))),2))='" + Periode + "'" +
                            "and DeptID=" + DeptID + " and ItemID=" + ItemID + " and ItemTypeID=" + ItemTypeID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToDecimal(sdr["MaxQty"].ToString());
                }
            }
            return result;
        }
		public int RuleCalc(string Periode, int ItemID, int ItemTypeID,int DeptID)
        {
            int result = 0;
            string strSQL = "SELECT RuleCalc FROM BudgetSP " +
                            "where RowStatus>-1 and (CAST(Tahun as CHAR(4))+RIGHT('0'+RTRIM(LTRIM(CAST(Bulan as CHAR))),2))='" + Periode + "'" +
                            "and DeptID=" + DeptID + " and ItemID=" + ItemID + " and ItemTypeID=" + ItemTypeID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToInt32(sdr["RuleCalc"].ToString());
                }
            }
            return result;
        }

        public int ConvertToAsset(int DeptID)
        {
            int result = 0;
            string strSQL = "SELECT Count(pd.ProdLine)Jml FROM PakaiDetail AS pd " +
                         "LEFT JOIN Pakai AS p ON p.ID=pd.PakaiID " +
                         "WHERE pd.Quantity < ISNULL(pd.ProdLine,0) AND RowStatus>-1 AND GroupID=4 AND ItemTypeID=2 " +
                         "AND p.DeptID=" + DeptID + " AND p.Status>1 ";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = int.Parse(sdr["Jml"].ToString());
                }
            }
            return result;
        }

        public PakaiDetail CekItemFlooculant(string itemID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL =
            "select ItemID from BM_PFloculantItem where RowStatus>-1 and NamaBarang in (select ItemName from Inventory where ID=" + itemID + ") ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectFloo(sqlDataReader);
                }
            }

            return new PakaiDetail();
        }

        public PakaiDetail GenerateObjectFloo(SqlDataReader sqlDataReader)
        {
            objPakaiDetail = new PakaiDetail();
            objPakaiDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            return objPakaiDetail;
        }
    }
}
