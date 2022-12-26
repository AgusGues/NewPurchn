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
    public class LapHarianFacade : AbstractTransactionFacade
    {
        private LapHarian objLapHarian = new LapHarian();
        private ArrayList arrLapHarian;
        private List<SqlParameter> sqlListParam;

        public LapHarianFacade(object objDomain)
            : base(objDomain)
        {
            objLapHarian = (LapHarian)objDomain;
        }

        public LapHarianFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", objLapHarian.ItemID));
                sqlListParam.Add(new SqlParameter("@ItemCode", objLapHarian.ItemCode));
                sqlListParam.Add(new SqlParameter("@ItemName", objLapHarian.ItemName));
                sqlListParam.Add(new SqlParameter("@UomID", objLapHarian.UomID));
                sqlListParam.Add(new SqlParameter("@UomCode", objLapHarian.UomCode));
                sqlListParam.Add(new SqlParameter("@StokAwal", objLapHarian.StokAwal));
                sqlListParam.Add(new SqlParameter("@Pemasukan", objLapHarian.Pemasukan));
                sqlListParam.Add(new SqlParameter("@Retur", objLapHarian.Retur));
                sqlListParam.Add(new SqlParameter("@AdjustTambah", objLapHarian.AdjustTambah));
                sqlListParam.Add(new SqlParameter("@AdjustKurang", objLapHarian.AdjustKurang));
                sqlListParam.Add(new SqlParameter("@Pemakaian", objLapHarian.Pemakaian));
                sqlListParam.Add(new SqlParameter("@DeptID", objLapHarian.DeptID));
                sqlListParam.Add(new SqlParameter("@DeptCode", objLapHarian.DeptCode));
                sqlListParam.Add(new SqlParameter("@UserID", objLapHarian.UserID));
                sqlListParam.Add(new SqlParameter("@TglCetak", objLapHarian.TglCetak));
                sqlListParam.Add(new SqlParameter("@KodeLaporan", objLapHarian.KodeLaporan));
                sqlListParam.Add(new SqlParameter("@GroupID", objLapHarian.GroupID));
                sqlListParam.Add(new SqlParameter("@Urutan", objLapHarian.Urutan));
                sqlListParam.Add(new SqlParameter("@NoDoc", objLapHarian.NoDoc));
                sqlListParam.Add(new SqlParameter("@BikinID", objLapHarian.BikinID));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertLapHarian");

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
            return 0;
        }

        public override int Delete(TransactionManager transManager)
        {

            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@UserID", objLapHarian.UserID));
                sqlListParam.Add(new SqlParameter("@KodeLaporan", objLapHarian.KodeLaporan));

                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteLapHarian");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int InsertReceipt(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", objLapHarian.ItemID));
                sqlListParam.Add(new SqlParameter("@ItemCode", objLapHarian.ItemCode));
                sqlListParam.Add(new SqlParameter("@ItemName", objLapHarian.ItemName));
                sqlListParam.Add(new SqlParameter("@UomID", objLapHarian.UomID));
                sqlListParam.Add(new SqlParameter("@UomCode", objLapHarian.UomCode));
                sqlListParam.Add(new SqlParameter("@StokAwal", objLapHarian.StokAwal));
                sqlListParam.Add(new SqlParameter("@Pemasukan", objLapHarian.Pemasukan));
                sqlListParam.Add(new SqlParameter("@Retur", objLapHarian.Retur));
                sqlListParam.Add(new SqlParameter("@AdjustTambah", objLapHarian.AdjustTambah));
                sqlListParam.Add(new SqlParameter("@AdjustKurang", objLapHarian.AdjustKurang));
                sqlListParam.Add(new SqlParameter("@Pemakaian", objLapHarian.Pemakaian));
                sqlListParam.Add(new SqlParameter("@StokAkhir", objLapHarian.StokAkhir));
                sqlListParam.Add(new SqlParameter("@DeptID", objLapHarian.DeptID));
                sqlListParam.Add(new SqlParameter("@DeptCode", objLapHarian.DeptCode));

                sqlListParam.Add(new SqlParameter("@UserID", objLapHarian.UserID));
                sqlListParam.Add(new SqlParameter("@TglCetak", objLapHarian.TglCetak));
                sqlListParam.Add(new SqlParameter("@GroupID", objLapHarian.GroupID));
                sqlListParam.Add(new SqlParameter("@NoDoc", objLapHarian.NoDoc));
                sqlListParam.Add(new SqlParameter("@KodeLaporan", objLapHarian.KodeLaporan));
                sqlListParam.Add(new SqlParameter("@Urutan", objLapHarian.Urutan));
                sqlListParam.Add(new SqlParameter("@BikinID", objLapHarian.BikinID));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertLapHar2");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateReceipt(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", objLapHarian.ItemID));
                sqlListParam.Add(new SqlParameter("@Pemasukan", objLapHarian.Pemasukan));
                sqlListParam.Add(new SqlParameter("@UserID", objLapHarian.UserID));
                sqlListParam.Add(new SqlParameter("@TglCetak", objLapHarian.TglCetak));
                sqlListParam.Add(new SqlParameter("@GroupID", objLapHarian.GroupID));
                sqlListParam.Add(new SqlParameter("@Urutan", objLapHarian.Urutan));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateLapBulReceiptDetail");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateReceiptKeAwal(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", objLapHarian.ItemID));
                sqlListParam.Add(new SqlParameter("@StokAwal", objLapHarian.StokAwal));
                sqlListParam.Add(new SqlParameter("@UserID", objLapHarian.UserID));
                sqlListParam.Add(new SqlParameter("@TglCetak", objLapHarian.TglCetak));
                sqlListParam.Add(new SqlParameter("@GroupID", objLapHarian.GroupID));
                sqlListParam.Add(new SqlParameter("@Urutan", objLapHarian.Urutan));
                sqlListParam.Add(new SqlParameter("@AdjustType", objLapHarian.AdjustType));
                sqlListParam.Add(new SqlParameter("@NoDoc", objLapHarian.NoDoc));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateLapHarianAdjustKeAwal");
                //krn update ke stok awal
                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int InsertConvertan(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", objLapHarian.ItemID));
                sqlListParam.Add(new SqlParameter("@ItemCode", objLapHarian.ItemCode));
                sqlListParam.Add(new SqlParameter("@ItemName", objLapHarian.ItemName));
                sqlListParam.Add(new SqlParameter("@UomID", objLapHarian.UomID));
                sqlListParam.Add(new SqlParameter("@UomCode", objLapHarian.UomCode));
                sqlListParam.Add(new SqlParameter("@StokAwal", objLapHarian.StokAwal));
                sqlListParam.Add(new SqlParameter("@Pemasukan", objLapHarian.Pemasukan));
                sqlListParam.Add(new SqlParameter("@Retur", objLapHarian.Retur));
                sqlListParam.Add(new SqlParameter("@AdjustTambah", objLapHarian.AdjustTambah));
                sqlListParam.Add(new SqlParameter("@AdjustKurang", objLapHarian.AdjustKurang));
                sqlListParam.Add(new SqlParameter("@Pemakaian", objLapHarian.Pemakaian));
                sqlListParam.Add(new SqlParameter("@StokAkhir", objLapHarian.StokAkhir));
                sqlListParam.Add(new SqlParameter("@DeptID", objLapHarian.DeptID));
                sqlListParam.Add(new SqlParameter("@DeptCode", objLapHarian.DeptCode));

                sqlListParam.Add(new SqlParameter("@UserID", objLapHarian.UserID));
                sqlListParam.Add(new SqlParameter("@TglCetak", objLapHarian.TglCetak));
                sqlListParam.Add(new SqlParameter("@GroupID", objLapHarian.GroupID));
                sqlListParam.Add(new SqlParameter("@NoDoc", objLapHarian.NoDoc));
                sqlListParam.Add(new SqlParameter("@KodeLaporan", objLapHarian.KodeLaporan));
                sqlListParam.Add(new SqlParameter("@Urutan", objLapHarian.Urutan));
                sqlListParam.Add(new SqlParameter("@BikinID", objLapHarian.BikinID));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertLapHar2");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int InsertPakai(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", objLapHarian.ItemID));
                sqlListParam.Add(new SqlParameter("@ItemCode", objLapHarian.ItemCode));
                sqlListParam.Add(new SqlParameter("@ItemName", objLapHarian.ItemName));
                sqlListParam.Add(new SqlParameter("@UomID", objLapHarian.UomID));
                sqlListParam.Add(new SqlParameter("@UomCode", objLapHarian.UomCode));
                sqlListParam.Add(new SqlParameter("@StokAwal", objLapHarian.StokAwal));
                sqlListParam.Add(new SqlParameter("@Pemasukan", objLapHarian.Pemasukan));
                sqlListParam.Add(new SqlParameter("@Retur", objLapHarian.Retur));
                sqlListParam.Add(new SqlParameter("@AdjustTambah", objLapHarian.AdjustTambah));
                sqlListParam.Add(new SqlParameter("@AdjustKurang", objLapHarian.AdjustKurang));
                sqlListParam.Add(new SqlParameter("@Pemakaian", objLapHarian.Pemakaian));
                sqlListParam.Add(new SqlParameter("@UserID", objLapHarian.UserID));
                sqlListParam.Add(new SqlParameter("@TglCetak", objLapHarian.TglCetak));
                sqlListParam.Add(new SqlParameter("@GroupID", objLapHarian.GroupID));
                sqlListParam.Add(new SqlParameter("@DeptID", objLapHarian.DeptID));
                sqlListParam.Add(new SqlParameter("@DeptCode", objLapHarian.DeptCode));
                sqlListParam.Add(new SqlParameter("@NoDoc", objLapHarian.NoDoc));
                sqlListParam.Add(new SqlParameter("@KodeLaporan", objLapHarian.KodeLaporan));
                sqlListParam.Add(new SqlParameter("@Urutan", objLapHarian.Urutan));
                sqlListParam.Add(new SqlParameter("@BikinID", objLapHarian.BikinID));
                sqlListParam.Add(new SqlParameter("@StokAkhir", objLapHarian.StokAkhir));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertLapHar2");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdatePakai(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", objLapHarian.ItemID));
                sqlListParam.Add(new SqlParameter("@Pemakaian", objLapHarian.Pemakaian));
                sqlListParam.Add(new SqlParameter("@UserID", objLapHarian.UserID));
                sqlListParam.Add(new SqlParameter("@TglCetak", objLapHarian.TglCetak));
                sqlListParam.Add(new SqlParameter("@GroupID", objLapHarian.GroupID));
                sqlListParam.Add(new SqlParameter("@DeptID", objLapHarian.DeptID));
                sqlListParam.Add(new SqlParameter("@DeptCode", objLapHarian.DeptCode));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateLapBulPakaiDetail");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdatePakaiKeAwal(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", objLapHarian.ItemID));
                sqlListParam.Add(new SqlParameter("@StokAwal", objLapHarian.StokAwal));
                sqlListParam.Add(new SqlParameter("@UserID", objLapHarian.UserID));
                sqlListParam.Add(new SqlParameter("@TglCetak", objLapHarian.TglCetak));
                sqlListParam.Add(new SqlParameter("@GroupID", objLapHarian.GroupID));
                sqlListParam.Add(new SqlParameter("@Urutan", objLapHarian.Urutan));
                sqlListParam.Add(new SqlParameter("@AdjustType", objLapHarian.AdjustType));
                sqlListParam.Add(new SqlParameter("@NoDoc", objLapHarian.NoDoc));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateLapHarianAdjustKeAwal");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int InsertReturPakai(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", objLapHarian.ItemID));
                sqlListParam.Add(new SqlParameter("@ItemCode", objLapHarian.ItemCode));
                sqlListParam.Add(new SqlParameter("@ItemName", objLapHarian.ItemName));
                sqlListParam.Add(new SqlParameter("@UomID", objLapHarian.UomID));
                sqlListParam.Add(new SqlParameter("@UomCode", objLapHarian.UomCode));
                sqlListParam.Add(new SqlParameter("@StokAwal", objLapHarian.StokAwal));
                sqlListParam.Add(new SqlParameter("@Pemasukan", objLapHarian.Pemasukan));
                sqlListParam.Add(new SqlParameter("@Retur", objLapHarian.Retur));
                sqlListParam.Add(new SqlParameter("@AdjustTambah", objLapHarian.AdjustTambah));
                sqlListParam.Add(new SqlParameter("@AdjustKurang", objLapHarian.AdjustKurang));
                sqlListParam.Add(new SqlParameter("@Pemakaian", objLapHarian.Pemakaian));
                sqlListParam.Add(new SqlParameter("@UserID", objLapHarian.UserID));
                sqlListParam.Add(new SqlParameter("@TglCetak", objLapHarian.TglCetak));
                sqlListParam.Add(new SqlParameter("@GroupID", objLapHarian.GroupID));
                sqlListParam.Add(new SqlParameter("@DeptID", objLapHarian.DeptID));
                sqlListParam.Add(new SqlParameter("@DeptCode", objLapHarian.DeptCode)); 
                sqlListParam.Add(new SqlParameter("@NoDoc", objLapHarian.NoDoc));
                sqlListParam.Add(new SqlParameter("@KodeLaporan", objLapHarian.KodeLaporan));
                sqlListParam.Add(new SqlParameter("@Urutan", objLapHarian.Urutan));
                sqlListParam.Add(new SqlParameter("@BikinID", objLapHarian.BikinID));
                sqlListParam.Add(new SqlParameter("@StokAkhir", objLapHarian.StokAkhir));
                //
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertLapHar2");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateReturPakai(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", objLapHarian.ItemID));
                sqlListParam.Add(new SqlParameter("@Retur", objLapHarian.Retur));
                sqlListParam.Add(new SqlParameter("@UserID", objLapHarian.UserID));
                sqlListParam.Add(new SqlParameter("@TglCetak", objLapHarian.TglCetak));
                sqlListParam.Add(new SqlParameter("@GroupID", objLapHarian.GroupID));
                sqlListParam.Add(new SqlParameter("@DeptID", objLapHarian.DeptID));
                sqlListParam.Add(new SqlParameter("@DeptCode", objLapHarian.DeptCode));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateLapBulReturPakaiDetail");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateReturPakaiKeAwal(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", objLapHarian.ItemID));
                sqlListParam.Add(new SqlParameter("@StokAwal", objLapHarian.StokAwal));
                sqlListParam.Add(new SqlParameter("@UserID", objLapHarian.UserID));
                sqlListParam.Add(new SqlParameter("@TglCetak", objLapHarian.TglCetak));
                sqlListParam.Add(new SqlParameter("@GroupID", objLapHarian.GroupID));
                sqlListParam.Add(new SqlParameter("@Urutan", objLapHarian.Urutan));
                sqlListParam.Add(new SqlParameter("@AdjustType", objLapHarian.AdjustType));
                sqlListParam.Add(new SqlParameter("@NoDoc", objLapHarian.NoDoc));
                //
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateLapHarianAdjustKeAwal");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int InsertAdjust(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", objLapHarian.ItemID));
                sqlListParam.Add(new SqlParameter("@ItemCode", objLapHarian.ItemCode));
                sqlListParam.Add(new SqlParameter("@ItemName", objLapHarian.ItemName));
                sqlListParam.Add(new SqlParameter("@UomID", objLapHarian.UomID));
                sqlListParam.Add(new SqlParameter("@UomCode", objLapHarian.UomCode));
                sqlListParam.Add(new SqlParameter("@StokAwal", objLapHarian.StokAwal));
                sqlListParam.Add(new SqlParameter("@Penyesuaian", objLapHarian.Penyesuaian));
                sqlListParam.Add(new SqlParameter("@Pemasukan", objLapHarian.Pemasukan));
                sqlListParam.Add(new SqlParameter("@Retur", objLapHarian.Retur));
                sqlListParam.Add(new SqlParameter("@AdjustTambah", objLapHarian.AdjustTambah));
                sqlListParam.Add(new SqlParameter("@AdjustKurang", objLapHarian.AdjustKurang));
                sqlListParam.Add(new SqlParameter("@Pemakaian", objLapHarian.Pemakaian));
                sqlListParam.Add(new SqlParameter("@UserID", objLapHarian.UserID));
                sqlListParam.Add(new SqlParameter("@TglCetak", objLapHarian.TglCetak));
                sqlListParam.Add(new SqlParameter("@GroupID", objLapHarian.GroupID));
                sqlListParam.Add(new SqlParameter("@DeptID", objLapHarian.DeptID));
                sqlListParam.Add(new SqlParameter("@DeptCode", objLapHarian.DeptCode));
                sqlListParam.Add(new SqlParameter("@AdjustType", objLapHarian.AdjustType));
                sqlListParam.Add(new SqlParameter("@NoDoc", objLapHarian.NoDoc));
                sqlListParam.Add(new SqlParameter("@KodeLaporan", objLapHarian.KodeLaporan));
                sqlListParam.Add(new SqlParameter("@Urutan", objLapHarian.Urutan));
                sqlListParam.Add(new SqlParameter("@BikinID", objLapHarian.BikinID));
                sqlListParam.Add(new SqlParameter("@StokAkhir", objLapHarian.StokAkhir));
                //
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertLapHarAdjust");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateAdjust(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", objLapHarian.ItemID));
                sqlListParam.Add(new SqlParameter("@Penyesuaian", objLapHarian.Penyesuaian));
                sqlListParam.Add(new SqlParameter("@UserID", objLapHarian.UserID));
                sqlListParam.Add(new SqlParameter("@TglCetak", objLapHarian.TglCetak));
                sqlListParam.Add(new SqlParameter("@GroupID", objLapHarian.GroupID));
                sqlListParam.Add(new SqlParameter("@DeptID", objLapHarian.DeptID));
                sqlListParam.Add(new SqlParameter("@DeptCode", objLapHarian.DeptCode));
                sqlListParam.Add(new SqlParameter("@AdjustType", objLapHarian.AdjustType));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateLapBulAdjust");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateAdjustKeAwal(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", objLapHarian.ItemID));
                sqlListParam.Add(new SqlParameter("@StokAwal", objLapHarian.StokAwal));
                sqlListParam.Add(new SqlParameter("@UserID", objLapHarian.UserID));
                sqlListParam.Add(new SqlParameter("@TglCetak", objLapHarian.TglCetak));
                sqlListParam.Add(new SqlParameter("@GroupID", objLapHarian.GroupID));
                sqlListParam.Add(new SqlParameter("@Urutan", objLapHarian.Urutan));
                sqlListParam.Add(new SqlParameter("@AdjustType", objLapHarian.AdjustType));
                sqlListParam.Add(new SqlParameter("@NoDoc", objLapHarian.NoDoc));
                //
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateLapHarianAdjustKeAwal");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from LapHarian as A where A.Status>-1 order by ID");
            strError = dataAccess.Error;
            arrLapHarian = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrLapHarian.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrLapHarian.Add(new LapHarian());

            return arrLapHarian;
        }

        public ArrayList RetrieveOpenStatus()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 50 A.ID,A.AdjustNo,A.AdjustDate,A.AdjustType,A.Status,A.CreatedBy,B.Quantity,C.ItemCode,C.ItemName from LapHarian as A, AdjustDetail as B,Inventory as C where A.Status=0 and A.ID=B.AdjustID and B.ItemID=C.ID and B.RowStatus>-1 order by A.AdjustNo desc");
            strError = dataAccess.Error;
            arrLapHarian = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrLapHarian.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrLapHarian.Add(new LapHarian());

            return arrLapHarian;
        }

        public ArrayList RetrieveOpenStatusForAll(string thbl)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.AdjustNo,A.AdjustDate,A.AdjustType,A.Status,A.CreatedBy from LapHarian as A where A.Status>-1 and LEFT(convert(varchar,A.AdjustDate,112),6) = '" + thbl + "' order by A.AdjustNo desc");
            strError = dataAccess.Error;
            arrLapHarian = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrLapHarian.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrLapHarian.Add(new LapHarian());

            return arrLapHarian;
        }

        public ArrayList RetrieveOpenStatusByNo(string adjustNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.AdjustNo,A.AdjustDate,A.AdjustType,A.Status,A.CreatedBy,B.Quantity,C.ItemCode,C.ItemName from LapHarian as A, AdjustDetail as B,Inventory as C where A.Status=0 and A.ID=B.AdjustID and B.ItemID=C.ID and B.RowStatus>-1 and A.AdjustNo='" + adjustNo + "'");
            strError = dataAccess.Error;
            arrLapHarian = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrLapHarian.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrLapHarian.Add(new LapHarian());

            return arrLapHarian;
        }

        public LapHarian RetrieveByNo(string adjustNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.AdjustNo,A.AdjustDate,A.AdjustType,A.Status,A.CreatedBy,B.Quantity,C.ItemCode,C.ItemName from LapHarian as A, AdjustDetail as B,Inventory as C where A.Status=0 and A.ID=B.AdjustID and B.ItemID=C.ID and B.RowStatus>-1 and A.AdjustNo='" + adjustNo + "'");
            strError = dataAccess.Error;
            arrLapHarian = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new LapHarian();
        }

        public LapHarian RetrieveForCekStokAkhir(int itemID, int userID, int groupID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "select top 1 isnull(StokAkhir,0) as StokAkhir from LaporanHarian where ItemID=" + itemID + " and UserID=" + userID + " and GroupID=" + groupID + " order by ItemID  desc,Urutan  desc,recid desc";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 1 isnull(StokAkhir,0) as StokAkhir from LaporanHarian where ItemID=" + itemID + " and UserID=" + userID + " and TglCetak='" + tglCetak + "' and GroupID=" + groupID + " order by ItemID desc,Urutan desc,NoDoc desc");
            strError = dataAccess.Error;
            arrLapHarian = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectStokAkhir(sqlDataReader);
                }
            }

            return new LapHarian();
        }

        public LapHarian GenerateObject(SqlDataReader sqlDataReader)
        {
            objLapHarian = new LapHarian();
            objLapHarian.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objLapHarian.ItemCode = sqlDataReader["ItemCode"].ToString();
            objLapHarian.ItemName = sqlDataReader["ItemName"].ToString();
            objLapHarian.UomID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objLapHarian.UomCode = sqlDataReader["UomCode"].ToString();
            objLapHarian.StokAwal = Convert.ToDecimal(sqlDataReader["StokAwal"]);
            objLapHarian.Pemasukan = Convert.ToDecimal(sqlDataReader["Pemasukan"]);
            objLapHarian.Retur = Convert.ToDecimal(sqlDataReader["Retur"]);
            objLapHarian.Penyesuaian = Convert.ToDecimal(sqlDataReader["Penyesuaian"]);
            objLapHarian.Pemakaian = Convert.ToDecimal(sqlDataReader["Pemakaian"]);
            objLapHarian.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objLapHarian.DeptCode = sqlDataReader["DeptCode"].ToString();
            objLapHarian.UserID = Convert.ToInt32(sqlDataReader["UserID"]);
            objLapHarian.TglCetak = sqlDataReader["TglCetak"].ToString();
            objLapHarian.KodeLaporan = sqlDataReader["KodeLaporan"].ToString();

            return objLapHarian;
        }

        public LapHarian GenerateObjectStokAkhir(SqlDataReader sqlDataReader)
        {
            objLapHarian = new LapHarian();
            objLapHarian.StokAkhir = Convert.ToDecimal(sqlDataReader["StokAkhir"]);

            return objLapHarian;
        }

    }
}
