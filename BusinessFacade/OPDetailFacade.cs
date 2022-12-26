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
    public class OPDetailFacade : AbstractTransactionFacade
    {
        private OPDetail objOPDetail = new OPDetail();
        private ArrayList arrOPDetail;
        private List<SqlParameter> sqlListParam;

        public OPDetailFacade(object objDomain)
            : base(objDomain)
        {
            objOPDetail = (OPDetail)objDomain;
        }

        public OPDetailFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@OPID", objOPDetail.OPID));
                sqlListParam.Add(new SqlParameter("@GroupID", objOPDetail.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemID", objOPDetail.ItemID));
                sqlListParam.Add(new SqlParameter("@ItemIDPaket", objOPDetail.ItemIDPaket));
                sqlListParam.Add(new SqlParameter("@Quantity", objOPDetail.Quantity));
                sqlListParam.Add(new SqlParameter("@Quantity2", objOPDetail.Quantity2));
                sqlListParam.Add(new SqlParameter("@QtyScheduled", objOPDetail.QtyScheduled));
                sqlListParam.Add(new SqlParameter("@QtyReceived", objOPDetail.QtyReceived));
                sqlListParam.Add(new SqlParameter("@Price", objOPDetail.Price));
                sqlListParam.Add(new SqlParameter("@TotalPrice", objOPDetail.TotalPrice));
                sqlListParam.Add(new SqlParameter("@Point", objOPDetail.Point));
                sqlListParam.Add(new SqlParameter("@Tebal", objOPDetail.Tebal));
                sqlListParam.Add(new SqlParameter("@Panjang", objOPDetail.Panjang));
                sqlListParam.Add(new SqlParameter("@Lebar", objOPDetail.Lebar));
                sqlListParam.Add(new SqlParameter("@Berat", objOPDetail.Berat));
                sqlListParam.Add(new SqlParameter("@Quota", objOPDetail.Quota));
                sqlListParam.Add(new SqlParameter("@TokoID", objOPDetail.TokoID));
                sqlListParam.Add(new SqlParameter("@PriceDefault", objOPDetail.PriceDefault));
                sqlListParam.Add(new SqlParameter("@Paket", objOPDetail.Paket));
                sqlListParam.Add(new SqlParameter("@DepoID", objOPDetail.DepoID));
                sqlListParam.Add(new SqlParameter("@PriceList", objOPDetail.PriceList));
                sqlListParam.Add(new SqlParameter("@MBT", objOPDetail.MBT));
                sqlListParam.Add(new SqlParameter("@MbtID", objOPDetail.MbtID));
                sqlListParam.Add(new SqlParameter("@PromoItemID", objOPDetail.PromoItemID));
                sqlListParam.Add(new SqlParameter("@PromoFlag", objOPDetail.PromoFlag));
                sqlListParam.Add(new SqlParameter("@PromoID", objOPDetail.PromoID));
                sqlListParam.Add(new SqlParameter("@GroupCategory", objOPDetail.GroupCategory));
                sqlListParam.Add(new SqlParameter("@QuotaItemID", objOPDetail.QuotaItemID));

                sqlListParam.Add(new SqlParameter("@KodeVoucher", objOPDetail.KodeVoucher));
                sqlListParam.Add(new SqlParameter("@Disc", objOPDetail.Disc));

                sqlListParam.Add(new SqlParameter("@DebtInsurance", objOPDetail.DebtInsurance));
                //yudith minta ada flag / beda warna utk add 500 ini/insurance

                sqlListParam.Add(new SqlParameter("@Point_bak", objOPDetail.Point_bak));
                sqlListParam.Add(new SqlParameter("@Point_Star", objOPDetail.Point_star));

                sqlListParam.Add(new SqlParameter("@PriceRetail", objOPDetail.PriceRetail));

                //jika ada program MBT ada bersama pada 2 zona berbeda ?
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertOPDetail");

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
                objOPDetail = (OPDetail)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@OPID", objOPDetail.OPID));

                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteOPDetail");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdatePricePromo(TransactionManager transManager, int opDetailID, decimal harga)
        {
            try
            {
                objOPDetail = (OPDetail)objDomain;


                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", opDetailID));
                sqlListParam.Add(new SqlParameter("@Price", harga));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdatePricePromoOPDetail");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdatePrice(TransactionManager transManager, int opDetailID, decimal harga)
        {
            try
            {
                objOPDetail = (OPDetail)objDomain;


                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", opDetailID));
                sqlListParam.Add(new SqlParameter("@Price", harga));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdatePriceOPDetail");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdatePoint(TransactionManager transManager, int opDetailID, decimal point)
        {
            try
            {
                objOPDetail = (OPDetail)objDomain;


                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", opDetailID));
                sqlListParam.Add(new SqlParameter("@Point", point));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdatePointOPDetail");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int InsertRencanaTglKirim(TransactionManager transManager, int opDetailID, DateTime rencanaKirim)
        {
            try
            {
                objOPDetail = (OPDetail)objDomain;


                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@opID", opDetailID));
                sqlListParam.Add(new SqlParameter("@RencanaTglKirim", rencanaKirim));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertRencanaTglKirim");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from OPDetail");
            strError = dataAccess.Error;
            arrOPDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOPDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrOPDetail.Add(new OPDetail());

            return arrOPDetail;
        }

        public ArrayList RetrieveApproveStatusByDepo(int depoID, int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.OPID,A.GroupID,D.GroupDescription as GroupName,A.ItemID,B.ItemCode,B.Description as ItemName,B.ItemType,C.UOMCode, A.Quantity,A.QtyScheduled,A.QtyReceived,A.Price,A.Disc,A.TotalPrice,A.Point,A.Tebal,A.Panjang,A.Lebar,A.Berat,B.IsQuota,A.PriceDefault,A.Paket from OPDetail as A,Items as B,UOM as C,Groups as D where A.ItemID = B.ID and B.UOMID = C.ID and A.GroupID = D.ID and A.OPID = " + Id);
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select E.DepoID,A.ID,A.OPID,A.GroupID,D.GroupDescription as GroupName,A.ItemID,B.ItemCode,B.Description as ItemName,B.ItemType,C.UOMCode, A.Quantity,A.QtyScheduled,A.QtyReceived,A.Price,A.Disc,A.TotalPrice,A.Point,A.Tebal,A.Panjang,A.Lebar,A.Berat,B.IsQuota,A.PriceDefault,A.Paket,E.CreatedTime,A.CodeEncrpt,isnull(A.Insurance,0) as Insurance from OPDetail as A,Items as B,UOM as C,Groups as D,OP as E where A.ItemID = B.ID and B.UOMID = C.ID and A.GroupID = D.ID and E.ID=A.opid and E.Status in (2,3) and E.DepoID=" + depoID + " and A.OPID =" + Id);
            strError = dataAccess.Error;
            arrOPDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOPDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrOPDetail.Add(new OPDetail());

            return arrOPDetail;
        }

        public ArrayList RetrieveById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.OPID,A.GroupID,D.GroupDescription as GroupName,A.ItemID,B.ItemCode,B.Description as ItemName,B.ItemType,C.UOMCode, A.Quantity,A.QtyScheduled,A.QtyReceived,A.Price,A.Disc,A.TotalPrice,A.Point,A.Tebal,A.Panjang,A.Lebar,A.Berat,B.IsQuota,A.PriceDefault,A.Paket,cast('1753-1-1' as datetime) as CreatedTime,A.CodeEncrpt,isnull(A.Insurance,0) as Insurance from OPDetail as A,Items as B,UOM as C,Groups as D where A.ItemID = B.ID and B.UOMID = C.ID and A.GroupID = D.ID and A.OPID = " + Id);
            strError = dataAccess.Error;
            arrOPDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOPDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrOPDetail.Add(new OPDetail());

            return arrOPDetail;
        }
        public ArrayList RetrieveByIdRaceToPerth(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.OPID,A.GroupID,D.GroupDescription as GroupName,A.ItemID,B.ItemCode,B.Description as ItemName,B.ItemType,C.UOMCode, A.Quantity,A.QtyScheduled,A.QtyReceived,A.Price,A.Disc,A.TotalPrice,A.Point,A.Tebal,A.Panjang,A.Lebar,A.Berat,B.IsQuota,A.PriceDefault,A.Paket,cast('1753-1-1' as datetime) as CreatedTime,A.CodeEncrpt,isnull(A.Insurance,0) as Insurance,A.Point_Star,A.HargaRetail from OPDetail as A,Items as B,UOM as C,Groups as D where A.ItemID = B.ID and B.UOMID = C.ID and A.GroupID = D.ID and A.OPID = " + Id);
            strError = dataAccess.Error;
            arrOPDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOPDetail.Add(GenerateObjectRace(sqlDataReader));
                }
            }
            else
                arrOPDetail.Add(new OPDetail());

            return arrOPDetail;
        }

        public ArrayList RetrieveById2(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.OPID,A.GroupID,D.GroupDescription as GroupName,A.ItemID,B.ItemCode,B.Description as ItemName,B.ItemType,C.UOMCode, A.Quantity,A.QtyScheduled,A.QtyReceived,A.Price,A.Disc,A.TotalPrice,A.Point,A.Tebal,A.Panjang,A.Lebar,A.Berat,B.IsQuota,B.Ket1 as MBT,A.PriceDefault,A.Paket from OPDetail as A,Items as B,UOM as C,Groups as D where A.ItemID = B.ID and B.UOMID = C.ID and A.GroupID = D.ID and A.OPID = " + Id);
            strError = dataAccess.Error;
            arrOPDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOPDetail.Add(GenerateObjectMBT(sqlDataReader));
                }
            }
            else
                arrOPDetail.Add(new OPDetail());

            return arrOPDetail;
        }
        public string CekCodeEncrpt(int opID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select CodeEncrpt from OPDetail where OPID="+opID+" and LEN(CodeEncrpt)>2");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return sqlDataReader["CodeEncrpt"].ToString();
                }
            }

            return string.Empty;
        }
        public OPDetail RetrieveByItemAndOP(int itemID,int opID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.OPID,A.GroupID,D.GroupDescription as GroupName,A.ItemID,B.ItemCode,B.Description as ItemName,B.ItemType,C.UOMCode, A.Quantity,A.QtyScheduled,A.QtyReceived,A.Price,A.Disc,A.TotalPrice,A.Point,A.Tebal,A.Panjang,A.Lebar,A.Berat,B.IsQuota,A.PriceDefault,A.Paket,cast('1753-1-1' as datetime) as CreatedTime,A.CodeEncrpt,isnull(A.Insurance,0) as Insurance from OPDetail as A,Items as B,UOM as C,Groups as D where A.ItemID = B.ID and B.UOMID = C.ID and A.GroupID = D.ID and A.OPID = " + opID + " and A.ItemID = " + itemID);
            strError = dataAccess.Error;
            arrOPDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new OPDetail();
        }

        public int SumQtyByItemAndOP(int itemID, int opID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT isnull(SUM(Quantity),0) as jumQty from OPDetail where ItemID = " + itemID + " and OPID = " + opID);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["jumQty"]);
                }
            }

            return 0;
        }
        public decimal SumTotalPriceByOPid(int opID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT sum(TotalPrice)  as TotalPrice from OPDetail where OPID = " + opID + " and (select Pajak from Items where ID in(OPDetail.ItemID))=0");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["TotalPrice"]);
                }
            }

            return 0;
        }


        public ArrayList RetrieveByIdNoScheduled(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.OPID,A.GroupID,D.GroupDescription as GroupName,A.ItemID,B.ItemCode,B.Description as ItemName,B.ItemType,C.UOMCode, A.Quantity,A.QtyScheduled,A.QtyReceived,A.Price,A.Disc,A.TotalPrice,A.Point,A.Tebal,A.Panjang,A.Lebar,A.Berat,B.IsQuota,A.PriceDefault,A.Paket,cast('1753-1-1' as datetime) as CreatedTime,A.CodeEncrpt,isnull(A.Insurance,0) as Insurance from OPDetail as A,Items as B,UOM as C,Groups as D where A.ItemID = B.ID and B.UOMID = C.ID and A.GroupID = D.ID and A.QtyScheduled < A.Quantity and A.OPID = " + Id);
            strError = dataAccess.Error;
            arrOPDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOPDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrOPDetail.Add(new OPDetail());

            return arrOPDetail;
        }


        public ArrayList RetrieveNoPaketById(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.OPID,A.GroupID,D.GroupDescription as GroupName,A.ItemID,B.ItemCode,B.Description as ItemName,B.ItemType,C.UOMCode, A.Quantity,A.QtyScheduled,A.QtyReceived,A.Price,A.Disc,A.TotalPrice,A.Point,A.Tebal,A.Panjang,A.Lebar,A.Berat,B.IsQuota,A.PriceDefault,A.Paket,cast('1753-1-1' as datetime) as CreatedTime,A.CodeEncrpt,isnull(A.Insurance,0) as Insurance from OPDetail as A,Items as B,UOM as C,Groups as D where A.ItemID = B.ID and B.UOMID = C.ID and A.GroupID = D.ID and A.Paket = 0 and A.OPID = " + Id);
            strError = dataAccess.Error;
            arrOPDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOPDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrOPDetail.Add(new OPDetail());

            return arrOPDetail;
        }
        public ArrayList RetrieveNoPaketByIdRaceToPerth(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.OPID,A.GroupID,D.GroupDescription as GroupName,A.ItemID,B.ItemCode,B.Description as ItemName,B.ItemType,C.UOMCode, A.Quantity,A.QtyScheduled,A.QtyReceived,A.Price,A.Disc,A.TotalPrice,A.Point,A.Tebal,A.Panjang,A.Lebar,A.Berat,B.IsQuota,A.PriceDefault,A.Paket,cast('1753-1-1' as datetime) as CreatedTime,A.CodeEncrpt,isnull(A.Insurance,0) as Insurance,A.Point_Star,A.HargaRetail from OPDetail as A,Items as B,UOM as C,Groups as D where A.ItemID = B.ID and B.UOMID = C.ID and A.GroupID = D.ID and A.Paket = 0 and A.OPID = " + Id);
            strError = dataAccess.Error;
            arrOPDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOPDetail.Add(GenerateObjectRace(sqlDataReader));
                }
            }
            else
                arrOPDetail.Add(new OPDetail());

            return arrOPDetail;
        }

        public ArrayList RetrieveNoPaketByIdWithMBT(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.OPID,A.GroupID,D.GroupDescription as GroupName,A.ItemID,B.ItemCode,B.Description as ItemName,B.ItemType,B.GroupCategory,C.UOMCode, A.Quantity,A.QtyScheduled,A.QtyReceived,A.Price,A.Disc,A.TotalPrice,A.Point,A.Tebal,A.Panjang,A.Lebar,A.Berat,B.IsQuota,B.Ket1 as MBT,A.PriceDefault,A.Paket,A.HargaRetail from OPDetail as A,Items as B,UOM as C,Groups as D where A.ItemID = B.ID and B.UOMID = C.ID and A.GroupID = D.ID and A.Paket = 0 and A.OPID = " + Id);
            strError = dataAccess.Error;
            arrOPDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOPDetail.Add(GenerateObjectMBT1(sqlDataReader));
                }
            }
            else
                arrOPDetail.Add(new OPDetail());

            return arrOPDetail;
        }
        public ArrayList RetrieveNoPaketByIdWithMBTforDist(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.OPID,A.GroupID,D.GroupDescription as GroupName,A.ItemID,B.ItemCode,B.Description as ItemName,B.ItemType,B.GroupCategory,C.UOMCode, A.Quantity,A.QtyScheduled,A.QtyReceived,A.Price,A.Disc,A.TotalPrice,A.Point,A.Tebal,A.Panjang,A.Lebar,A.Berat,B.IsQuota,B.Ket1 as MBT,A.PriceDefault,A.Paket,A.HargaRetail from OPDetail as A,Items as B,UOM as C,Groups as D where A.ItemID = B.ID and B.UOMID = C.ID and A.GroupID = D.ID and A.Paket = 0 and B.ItemType=0 and A.OPID = " + Id);
            strError = dataAccess.Error;
            arrOPDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOPDetail.Add(GenerateObjectMBT1(sqlDataReader));
                }
            }
            else
                arrOPDetail.Add(new OPDetail());

            return arrOPDetail;
        }

        public ArrayList RetrieveByOPDetailId(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.OPID,A.GroupID,D.GroupDescription as GroupName,A.ItemID,B.ItemCode,B.Description as ItemName,B.ItemType,C.UOMCode, A.Quantity,A.QtyScheduled,A.Price,A.Disc,A.TotalPrice,A.Point,A.Tebal,A.Panjang,A.Lebar,A.Berat,B.IsQuota,A.PriceDefault,A.Paket,cast('1753-1-1' as datetime) as CreatedTime,A.CodeEncrpt,isnull(A.Insurance,0) as Insurance from OPDetail as A,Items as B,UOM as C,Groups as D where A.ItemID = B.ID and B.UOMID = C.ID and A.GroupID = D.ID and A.ID = " + Id);
            strError = dataAccess.Error;
            arrOPDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOPDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrOPDetail.Add(new OPDetail());

            return arrOPDetail;
        }

        public OPDetail RetrieveByOPDetailId2(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.OPID,A.GroupID,D.GroupDescription as GroupName,A.ItemID,B.ItemCode,B.Description as ItemName,B.ItemType,C.UOMCode, A.Quantity,A.QtyScheduled,A.QtyReceived,A.Price,A.Disc,A.TotalPrice,A.Point,A.Tebal,A.Panjang,A.Lebar,A.Berat,B.IsQuota,A.PriceDefault,A.Paket,cast('1753-1-1' as datetime) as CreatedTime,A.CodeEncrpt,isnull(A.Insurance,0) as Insurance from OPDetail as A,Items as B,UOM as C,Groups as D where A.ItemID = B.ID and B.UOMID = C.ID and A.GroupID = D.ID and A.ID = " + Id);
            strError = dataAccess.Error;
            arrOPDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }            

            return new OPDetail();
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ItemCode,A.Description,A.GroupID,B.GroupDescription,A.ShortKey,A.GradeID,C.GradeCode,A.SisiID,D.SisiDescription,A.Tebal,A.Panjang,A.Lebar,A.UOMID,E.UOMCode,A.Berat,A.Ket1,A.Ket2,A.Utuh,A.Paket,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,cast('1753-1-1' as datetime) as CreatedTime,A.CodeEncrpt,isnull(A.Insurance,0) as Insurance from OPDetail as A,Groups as B, Grades as C,Sisi as D,UOM as E where A.GroupID = B.ID and A.GradeID = C.ID and A.SisiID = D.ID and A.UOMID = E.ID and A.RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrOPDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOPDetail.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrOPDetail.Add(new OPDetail());

            return arrOPDetail;
        }
        public ArrayList RetrieveByPricePromo(string drTglOP, string listDistSub)
        {
            string strListDistSub = string.Empty;
            if (listDistSub != string.Empty)
                strListDistSub = "where DistSub in " + listDistSub;

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from (select A.ID,A.OPID,A.GroupID,D.GroupDescription as GroupName,A.ItemID,B.ItemCode,B.Description as ItemName,B.ItemType,C.UOMCode, A.Quantity,"+
                "A.QtyScheduled,A.QtyReceived,A.Price,A.Disc,A.TotalPrice,A.Point,A.Tebal,A.Panjang,A.Lebar,A.Berat,B.IsQuota,A.PriceDefault,A.Paket,"+
                "case when E.TypeDistSub=1 then (select DistributorCode from Distributor where ID = E.DistSubID and RowStatus>-1) "+
                "when E.TypeDistSub=2 then (select Distributor.DistributorCode from Distributor,SubDistributor where Distributor.ID=SubDistributor.DistributorID and "+
                "SubDistributor.ID = E.DistSubID and Distributor.RowStatus>-1 and SubDistributor.RowStatus>-1) "+
                "else '   ' end DistSub,"+
                "case when E.TypeDistSub=1 then (select ID from Distributor where ID = E.DistSubID and RowStatus>-1) "+
                "when E.TypeDistSub=2 then (select Distributor.ID from Distributor,SubDistributor where Distributor.ID=SubDistributor.DistributorID and "+
                "SubDistributor.ID = E.DistSubID and Distributor.RowStatus>-1 and SubDistributor.RowStatus>-1) "+
                "else 0 end DistSubID,F.ZonaID "+
                "from OPDetail as A,Items as B,UOM as C,Groups as D,OP as E,Toko as F "+
                "where E.ID=A.OPID and E.Status>-1 and E.CustomerId=F.ID and A.ItemID = B.ID and B.UOMID = C.ID and A.GroupID = D.ID and E.TypeDistSub>0 " +
                "and A.Price>0 and E.CustomerType=1 and CONVERT(varchar,E.CreatedTime,112)>='"+drTglOP+"' ) as AA "+strListDistSub+" order by ID");
            strError = dataAccess.Error;
            arrOPDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOPDetail.Add(GenerateObjectPricePromo(sqlDataReader));
                }
            }
            else
                arrOPDetail.Add(new OPDetail());

            return arrOPDetail;
        }
        public ArrayList RetrieveOPDetailByPeriod(string drTgl, string sdTgl)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select OP.ID as OPID,OPDetail.ID,OPDetail.ItemID,OPDetail.Quantity,OPDetail.Point,OPDetail.Paket,Items.GroupCategory,OP.DepoID,Toko.TokoCode from OP,OPDetail,Items,Toko " +
                "where OP.ID = OPDetail.OPID and OP.Status>-1 and OPDetail.ItemID=Items.ID and OP.CustomerType=1 and OP.CustomerId=Toko.ID and Toko.GetPoint=0 and "+
                "CONVERT(varchar,OP.CreatedTime,112)>= '" + drTgl + "' and CONVERT(varchar,OP.CreatedTime,112)<='" + sdTgl + "'  ORDER BY OP.OPNo,GroupCategory,Paket ");
            strError = dataAccess.Error;
            arrOPDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOPDetail.Add(GenerateObjectUpdatePoint(sqlDataReader));
                }
            }
            else
                arrOPDetail.Add(new OPDetail());

            return arrOPDetail;
        }
        public ArrayList RetrieveOPDetailByPeriodDistinct(string drTgl, string sdTgl)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select distinct OP.ID from OP,OPDetail,Items,Toko " +
                "where OP.ID = OPDetail.OPID and OP.Status>-1 and OPDetail.ItemID=Items.ID and OP.CustomerType=1 and OP.CustomerId=Toko.ID and Toko.GetPoint=0 and " +
                "CONVERT(varchar,OP.CreatedTime,112)>= '" + drTgl + "' and CONVERT(varchar,OP.CreatedTime,112)<='" + sdTgl + "'  ORDER BY OP.ID ");
            strError = dataAccess.Error;
            arrOPDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOPDetail.Add(GenerateObjectUpdatePoint2(sqlDataReader));
                }
            }
            else
                arrOPDetail.Add(new OPDetail());

            return arrOPDetail;
        }
        public ArrayList RetrieveOPDetailByNoOP(string NoOP)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select OP.ID as OPID,OPDetail.ID,OPDetail.ItemID,OPDetail.Quantity,OPDetail.Point,OPDetail.Paket,Items.GroupCategory,OP.DepoID,Toko.TokoCode from OP,OPDetail,Items,Toko " +
                "where OP.ID = OPDetail.OPID and OP.Status>-1 and OPDetail.ItemID=Items.ID and OP.CustomerType=1 and OP.CustomerId=Toko.ID and Toko.GetPoint=0 and " +
                " OP.OPNo='" + NoOP + "' ORDER BY OP.OPNo,GroupCategory,Paket ");
            strError = dataAccess.Error;
            arrOPDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOPDetail.Add(GenerateObjectUpdatePoint(sqlDataReader));
                }
            }
            else
                arrOPDetail.Add(new OPDetail());

            return arrOPDetail;
        }
        public ArrayList RetrieveOPDetailByNoOPDistinct(string NoOP)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select distinct OP.ID from OP,OPDetail,Items,Toko " +
                "where OP.ID = OPDetail.OPID and OP.Status>-1 and OPDetail.ItemID=Items.ID and OP.CustomerType=1 and OP.CustomerId=Toko.ID and Toko.GetPoint=0 and " +
                " OP.OPNo='" + NoOP + "' ORDER BY OP.ID ");
            strError = dataAccess.Error;
            arrOPDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOPDetail.Add(GenerateObjectUpdatePoint2(sqlDataReader));
                }
            }
            else
                arrOPDetail.Add(new OPDetail());

            return arrOPDetail;
        }

        public OPDetail RetrieveByTokoJmlOrder(string kodetoko)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select COUNT(*) as Jumlah  from (select OP.OPNo from OP,OPDetail,Toko " +
                                                                          "where OP.ID = OPDetail.OPID and OP.Status>-1 and OPDetail.GroupID in (105,109) and OP.CustomerType = 1  " +
                                                                          "and OP.CustomerId = Toko.ID and Toko.TokoCode = '" + kodetoko + "' ) as B");
            strError = dataAccess.Error;
            arrOPDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectJmlOrder(sqlDataReader);
                }
            }

            return new OPDetail();
        }

        public OPDetail RetrieveByTokoLSP214JmlOrder(int tokoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select sum(Quantity) as Jumlah from OP as A, OPDetail as B,Items as C, Toko as D " +
                                                                          "where A.CustomerType = 1 and B.ItemID = C.ID and A.CustomerId = D.ID and A.ID = B.OPID and B.GroupID = 112 and left(D.TokoCode,1) in('T') " +
                                                                          "and A.CreatedTime >= '2014-10-11 00:32:28.470' and A.CreatedTime < '2014-12-02 00:32:28.470' and A.Status >= 0 and A.CustomerId = " + tokoID + " and left(C.GroupCategory,3) in('SMP','SPP','TBP') group by A.CustomerId");
            strError = dataAccess.Error;
            arrOPDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectJmlOrder(sqlDataReader);
                }
            }

            return new OPDetail();
        }

        public OPDetail RetrieveByTokoLSP214JmlOrder2(int tokoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select sum(Quantity) as Jumlah from OP as A, OPDetail as B,Items as C, Toko as D " +
                                                                          "where A.CustomerType = 1 and B.ItemID = C.ID and A.CustomerId = D.ID and A.ID = B.OPID and B.GroupID = 112 and left(D.TokoCode,1) in('T') " +
                                                                          "and A.CreatedTime >= '2014-12-02 00:32:28.470' and A.CreatedTime <= '2014-12-08 23:59:28.470' and A.Status >= 0 and A.CustomerId = " + tokoID + " and left(C.GroupCategory,3) in('SMP','SPP','TBP') group by A.CustomerId");
            strError = dataAccess.Error;
            arrOPDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectJmlOrder(sqlDataReader);
                }
            }

            return new OPDetail();
        }

        public OPDetail RetrieveByTokoLSP214JmlOrder3(string OPNO3)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select sum(Quantity) as Jumlah from OP as A, OPDetail as B,Items as C, Toko as D " +
                                                                          "where A.CustomerType = 1 and B.ItemID = C.ID and A.CustomerId = D.ID and A.ID = B.OPID and B.GroupID = 112 and left(D.TokoCode,1) in('T') and A.status > -1 " +
                                                                          "and A.CreatedTime >= '2014-12-02 00:32:28.470' and A.CreatedTime <= '2014-12-08 23:59:28.470' and A.OpNo = '" + OPNO3 + "' and left(C.GroupCategory,3) in('SMP','SPP','TBP') group by A.CustomerId");
            strError = dataAccess.Error;
            arrOPDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectJmlOrder(sqlDataReader);
                }
            }

            return new OPDetail();
        }



        public OPDetail GenerateObjectJmlOrder(SqlDataReader sqlDataReader)
        {
            objOPDetail = new OPDetail();
            objOPDetail.Quantity = Convert.ToInt32(sqlDataReader["Jumlah"]);

            return objOPDetail;

        }
        public OPDetail RetrieveByTokoLSP214PrdDua(int tokoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select sum(F.Qty) as Jumlah from OP as A, OPDetail as B,Items as C, Toko as D,SuratJalan as E,suratjalandetail as F  " +
                                                                        "where A.CustomerType = 1 and B.ItemID = C.ID and A.CustomerId = D.ID and A.ID = B.OPID and B.GroupID = 112 and left(D.TokoCode,1) in('T')  " +
                                                                        "and E.opid = A.ID and F.ItemID = B.ItemID and F.ItemID = C.ID and E.status in(2,3) and E.id = F.SuratJalanID  " +
                                                                        "and A.CreatedTime >= '2014-12-02 00:32:28.470' and A.CreatedTime < '2015-01-06 00:32:28.470'  and A.CustomerId = " + tokoID + " and left(C.GroupCategory,3) in('SMP','SPP','TBP') group by CustomerId");


            strError = dataAccess.Error;
            arrOPDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectJmlOrder(sqlDataReader);
                }
            }

            return new OPDetail();
        }

        public OPDetail RetrieveByTokoLSP214PrdDua2(int tokoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select sum(Quantity) as Jumlah from OP as A, OPDetail as B,Items as C, Toko as D " +
                                                                          "where A.CustomerType = 1 and B.ItemID = C.ID and A.CustomerId = D.ID and A.ID = B.OPID and B.GroupID = 112 and left(D.TokoCode,1) in('T') " +
                                                                          "and A.CreatedTime >= '2015-01-06 00:32:28.470' and A.Status >= 0 and A.CustomerId = " + tokoID + " and left(C.GroupCategory,3) in('SMP','SPP','TBP') group by A.CustomerId");
            strError = dataAccess.Error;
            arrOPDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectJmlOrder(sqlDataReader);
                }
            }

            return new OPDetail();
        }

        public OPDetail RetrieveByTokoLSP214PrdDua3(string OPNO3)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select sum(Quantity) as Jumlah from OP as A, OPDetail as B,Items as C, Toko as D " +
                                                                          "where A.CustomerType = 1 and B.ItemID = C.ID and A.CustomerId = D.ID and A.ID = B.OPID and B.GroupID = 112 and left(D.TokoCode,1) in('T') and A.status > -1 " +
                                                                          "and A.CreatedTime >= '2015-01-06 00:32:28.470' and A.OpNo = '" + OPNO3 + "' and left(C.GroupCategory,3) in('SMP','SPP','TBP') group by A.CustomerId");
            strError = dataAccess.Error;
            arrOPDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectJmlOrder(sqlDataReader);
                }
            }

            return new OPDetail();
        }

        // Begin Promo Mawar Pasti Untung max 50 % dari pengambilan 1 by Pati 02 Sept 15

        public int SumQtyByGroupTokoID(int groupID, int tokoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select SUM(B.QtyScheduled) as jumQty from OP as A, OPDetail as B " +
                                                                          "where A.ID = B.OPID and A.Status > -1 and A.CustomerType = 1 " +
                                                                          "and A.CustomerId = " + tokoID + " and B.GroupID = " + groupID + " and CONVERT(varchar,A.CreatedTime,112) < '20150831' group by A.CustomerId");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["jumQty"]);
                }
            }

            return 0;
        }

        public int SumQtyByGroupTokoIDCurrent(int groupID, int tokoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select SUM(B.Quantity) as jumQty from OP as A, OPDetail as B " +
                                                                          "where A.ID = B.OPID and A.Status > -1 and A.CustomerType = 1 " +
                                                                          "and A.CustomerId = " + tokoID + " and B.GroupID = " + groupID + " and CONVERT(varchar,A.CreatedTime,112) > '20150831' group by A.CustomerId");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["jumQty"]);
                }
            }

            return 0;
        }
        // End Promo Mawar Pasti Untung max 50 % dari pengambilan 1 by Pati 02 Sept 15
        //promo semarak point 2017
        public int SumQtyByGroupTokoID2(int groupID, int tokoID, string tokoCode, string desc)
        {
            string strToko = string.Empty;
            if (tokoCode == "T")
                strToko = " and left(D.TokoCode,1) in('T') ";
            else if (tokoCode == "S" || tokoCode == "J")
                strToko = " and left(D.TokoCode,1) in('S','J') ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(sum(F.Qty),0) as Jumlah from OP as A, OPDetail as B,Items as C, Toko as D,SuratJalan as E,SuratJalanDetail as F " +
                "where A.CustomerType = 1 and B.ItemID = C.ID and A.CustomerId = D.ID and A.ID = B.OPID and B.GroupID = "+groupID+strToko +
                "and A.CreatedTime >= '2017-09-27 00:00:00.000' and A.CreatedTime < '2017-10-05 00:00:00.000' and A.Status >= 0 and A.CustomerId = "+tokoID+" and Description like '%"+desc+"%' and a.ID=e.OPID and e.Status>=1 and E.ID=F.SuratJalanID and b.ItemID=F.ItemID group by A.CustomerId");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Jumlah"]);
                }
            }

            return 0;
        }
        public int SumQtyByGroupTokoID2Jkt(int groupID, int tokoID, string tokoCode, string desc)
        {
            string strToko = string.Empty;
            if (tokoCode == "T")
                strToko = " and left(D.TokoCode,1) in('T') ";
            else if (tokoCode == "S" || tokoCode == "J")
                strToko = " and left(D.TokoCode,1) in('S','J') ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(sum(F.Qty),0) as Jumlah from OP as A, OPDetail as B,Items as C, Toko as D,SuratJalan as E,SuratJalanDetail as F " +
                "where A.CustomerType = 1 and B.ItemID = C.ID and A.CustomerId = D.ID and A.ID = B.OPID and B.GroupID = " + groupID + strToko +
                "and A.CreatedTime >= '2017-08-28 00:00:00.000' and A.CreatedTime < '2017-10-27 00:00:00.000' and A.Status >= 0 and A.CustomerId = " + tokoID + " and Description like '%" + desc + "%' and a.ID=e.OPID and e.Status>=1 and E.ID=F.SuratJalanID and b.ItemID=F.ItemID group by A.CustomerId");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Jumlah"]);
                }
            }

            return 0;
        }
        public int SumQtyByGroupTokoID3Jkt(int groupID, int tokoID, string tokoCode, string desc)
        {
            string strToko = string.Empty;
            if (tokoCode == "T")
                strToko = " and left(D.TokoCode,1) in('T') ";
            else if (tokoCode == "S" || tokoCode == "J")
                strToko = " and left(D.TokoCode,1) in('S','J') ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(sum(F.Qty),0) as Jumlah from OP as A, OPDetail as B,Items as C, Toko as D,SuratJalan as E,SuratJalanDetail as F " +
                "where A.CustomerType = 1 and B.ItemID = C.ID and A.CustomerId = D.ID and A.ID = B.OPID and B.GroupID = " + groupID + strToko +
                "and A.CreatedTime >= '2017-10-26 00:00:00.000' and A.CreatedTime <= '2017-11-27 00:00:00.000' and A.Status >= 0 and A.CustomerId = " + tokoID + " and Description like '%" + desc + "%' and a.ID=e.OPID and e.Status>=1 and E.ID=F.SuratJalanID and b.ItemID=F.ItemID group by A.CustomerId");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Jumlah"]);
                }
            }

            return 0;
        }
        //promo semarak point 2017
        public OPDetail CekClaimVoucher(string kodevoucher)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select ac.*,c.Nominal from (select CodeEncrpt as KodeVoucher,SUM(NilaiVoucher) as ClaimVoucher from ( "+
                "select CodeEncrpt,abs(Quantity*Price) as NilaiVoucher, case when a.CustomerType=1 then (select top 1 TokoCode from Toko where Toko.ID=a.CustomerID) else (select top 1 CustomerCode from Customer where Customer.ID=a.CustomerID) end KodeToko from OP as a, OPDetail as b where a.ID=b.OPID and a.Status>-1  and YEAR(a.CreatedTime)>=2016 and b.CodeEncrpt='" + kodevoucher + "' ) as ab group by CodeEncrpt) as ac " +
                "left join (select CodeEncrpt,SUM(Nominal) as Nominal from Distributor_PotonganCode where Confirmation>0  group by CodeEncrpt) c on c.CodeEncrpt=ac.KodeVoucher");
            strError = dataAccess.Error;
            arrOPDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectCekVoucher(sqlDataReader);
                }
            }

            return new OPDetail();
        }
        //13Mar
        public ArrayList RetrieveByRencanaKirim(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select ROW_NUMBER() OVER (ORDER BY id) AS Row_Counter,* from OPScheduleKirim where OPID=" + Id + " and RowStatus>-1");
            strError = dataAccess.Error;
            arrOPDetail = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOPDetail.Add(GenerateObjectKirim(sqlDataReader));
                }
            }
            else
                arrOPDetail.Add(new OPDetail());

            return arrOPDetail;
        }

        public OPDetail GenerateObject(SqlDataReader sqlDataReader)
        {
            objOPDetail = new OPDetail();
            objOPDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objOPDetail.OPID = Convert.ToInt32(sqlDataReader["OPID"]);
            objOPDetail.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objOPDetail.GroupName = sqlDataReader["GroupName"].ToString();
            objOPDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objOPDetail.ItemCode = sqlDataReader["ItemCode"].ToString();
            objOPDetail.ItemName = sqlDataReader["ItemName"].ToString();
            objOPDetail.ItemType = Convert.ToInt32(sqlDataReader["ItemType"]);
            objOPDetail.UOMCode = sqlDataReader["UOMCode"].ToString();
            objOPDetail.Quantity = Convert.ToInt32(sqlDataReader["Quantity"]);
            objOPDetail.QtyScheduled = Convert.ToInt32(sqlDataReader["QtyScheduled"]);
            objOPDetail.QtyReceived = Convert.ToInt32(sqlDataReader["QtyReceived"]);
            objOPDetail.Price = Convert.ToDecimal(sqlDataReader["Price"]);
            objOPDetail.Disc = Convert.ToDecimal(sqlDataReader["Disc"]);
            objOPDetail.TotalPrice = Convert.ToDecimal(sqlDataReader["TotalPrice"]);
            objOPDetail.Point = Convert.ToInt32(sqlDataReader["Point"]);
            objOPDetail.Tebal = Convert.ToDecimal(sqlDataReader["Tebal"]);
            objOPDetail.Panjang = Convert.ToDecimal(sqlDataReader["Panjang"]);
            objOPDetail.Lebar = Convert.ToDecimal(sqlDataReader["Lebar"]);
            objOPDetail.Berat = Convert.ToDecimal(sqlDataReader["Berat"]);
            objOPDetail.Quota = Convert.ToInt32(sqlDataReader["IsQuota"]);
            objOPDetail.PriceDefault = Convert.ToDecimal(sqlDataReader["PriceDefault"]);
            objOPDetail.Paket = Convert.ToInt32(sqlDataReader["Paket"]);
            objOPDetail.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objOPDetail.KodeVoucher = sqlDataReader["CodeEncrpt"].ToString();

            objOPDetail.DebtInsurance = Convert.ToDecimal(sqlDataReader["Insurance"]);

            return objOPDetail;

        }
        public OPDetail GenerateObjectRace(SqlDataReader sqlDataReader)
        {
            objOPDetail = new OPDetail();
            objOPDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objOPDetail.OPID = Convert.ToInt32(sqlDataReader["OPID"]);
            objOPDetail.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objOPDetail.GroupName = sqlDataReader["GroupName"].ToString();
            objOPDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objOPDetail.ItemCode = sqlDataReader["ItemCode"].ToString();
            objOPDetail.ItemName = sqlDataReader["ItemName"].ToString();
            objOPDetail.ItemType = Convert.ToInt32(sqlDataReader["ItemType"]);
            objOPDetail.UOMCode = sqlDataReader["UOMCode"].ToString();
            objOPDetail.Quantity = Convert.ToInt32(sqlDataReader["Quantity"]);
            objOPDetail.QtyScheduled = Convert.ToInt32(sqlDataReader["QtyScheduled"]);
            objOPDetail.QtyReceived = Convert.ToInt32(sqlDataReader["QtyReceived"]);
            objOPDetail.Price = Convert.ToDecimal(sqlDataReader["Price"]);
            objOPDetail.Disc = Convert.ToDecimal(sqlDataReader["Disc"]);
            objOPDetail.TotalPrice = Convert.ToDecimal(sqlDataReader["TotalPrice"]);
            objOPDetail.Point = Convert.ToInt32(sqlDataReader["Point"]);
            objOPDetail.Tebal = Convert.ToDecimal(sqlDataReader["Tebal"]);
            objOPDetail.Panjang = Convert.ToDecimal(sqlDataReader["Panjang"]);
            objOPDetail.Lebar = Convert.ToDecimal(sqlDataReader["Lebar"]);
            objOPDetail.Berat = Convert.ToDecimal(sqlDataReader["Berat"]);
            objOPDetail.Quota = Convert.ToInt32(sqlDataReader["IsQuota"]);
            objOPDetail.PriceDefault = Convert.ToDecimal(sqlDataReader["PriceDefault"]);
            objOPDetail.Paket = Convert.ToInt32(sqlDataReader["Paket"]);
            objOPDetail.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objOPDetail.KodeVoucher = sqlDataReader["CodeEncrpt"].ToString();

            objOPDetail.DebtInsurance = Convert.ToDecimal(sqlDataReader["Insurance"]);
            objOPDetail.Point_star = Convert.ToDecimal(sqlDataReader["Point_Star"]);
            objOPDetail.PriceRetail = Convert.ToDecimal(sqlDataReader["HargaRetail"]);

            return objOPDetail;

        }

        public OPDetail GenerateObjectMBT(SqlDataReader sqlDataReader)
        {
            objOPDetail = new OPDetail();
            objOPDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objOPDetail.OPID = Convert.ToInt32(sqlDataReader["OPID"]);
            objOPDetail.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objOPDetail.GroupName = sqlDataReader["GroupName"].ToString();
            objOPDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objOPDetail.ItemCode = sqlDataReader["ItemCode"].ToString();
            objOPDetail.ItemName = sqlDataReader["ItemName"].ToString();
            objOPDetail.ItemType = Convert.ToInt32(sqlDataReader["ItemType"]);
            objOPDetail.UOMCode = sqlDataReader["UOMCode"].ToString();
            objOPDetail.Quantity = Convert.ToInt32(sqlDataReader["Quantity"]);
            objOPDetail.QtyScheduled = Convert.ToInt32(sqlDataReader["QtyScheduled"]);
            objOPDetail.QtyReceived = Convert.ToInt32(sqlDataReader["QtyReceived"]);
            objOPDetail.Price = Convert.ToDecimal(sqlDataReader["Price"]);
            objOPDetail.Disc = Convert.ToDecimal(sqlDataReader["Disc"]);
            objOPDetail.TotalPrice = Convert.ToDecimal(sqlDataReader["TotalPrice"]);
            objOPDetail.Point = Convert.ToInt32(sqlDataReader["Point"]);
            objOPDetail.Tebal = Convert.ToDecimal(sqlDataReader["Tebal"]);
            objOPDetail.Panjang = Convert.ToDecimal(sqlDataReader["Panjang"]);
            objOPDetail.Lebar = Convert.ToDecimal(sqlDataReader["Lebar"]);
            objOPDetail.Berat = Convert.ToDecimal(sqlDataReader["Berat"]);
            objOPDetail.Quota = Convert.ToInt32(sqlDataReader["IsQuota"]);
            objOPDetail.PriceDefault = Convert.ToDecimal(sqlDataReader["PriceDefault"]);
            objOPDetail.Paket = Convert.ToInt32(sqlDataReader["Paket"]);

            objOPDetail.MBT = sqlDataReader["MBT"].ToString();

            return objOPDetail;

        }

        public OPDetail GenerateObjectMBT1(SqlDataReader sqlDataReader)
        {
            objOPDetail = new OPDetail();
            objOPDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objOPDetail.OPID = Convert.ToInt32(sqlDataReader["OPID"]);
            objOPDetail.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objOPDetail.GroupName = sqlDataReader["GroupName"].ToString();
            objOPDetail.GroupCategory = sqlDataReader["GroupCategory"].ToString();
            objOPDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objOPDetail.ItemCode = sqlDataReader["ItemCode"].ToString();
            objOPDetail.ItemName = sqlDataReader["ItemName"].ToString();
            objOPDetail.ItemType = Convert.ToInt32(sqlDataReader["ItemType"]);
            objOPDetail.UOMCode = sqlDataReader["UOMCode"].ToString();
            objOPDetail.Quantity = Convert.ToInt32(sqlDataReader["Quantity"]);
            objOPDetail.QtyScheduled = Convert.ToInt32(sqlDataReader["QtyScheduled"]);
            objOPDetail.QtyReceived = Convert.ToInt32(sqlDataReader["QtyReceived"]);
            objOPDetail.Price = Convert.ToDecimal(sqlDataReader["Price"]);
            objOPDetail.Disc = Convert.ToDecimal(sqlDataReader["Disc"]);
            objOPDetail.TotalPrice = Convert.ToDecimal(sqlDataReader["TotalPrice"]);
            objOPDetail.Point = Convert.ToInt32(sqlDataReader["Point"]);
            objOPDetail.Tebal = Convert.ToDecimal(sqlDataReader["Tebal"]);
            objOPDetail.Panjang = Convert.ToDecimal(sqlDataReader["Panjang"]);
            objOPDetail.Lebar = Convert.ToDecimal(sqlDataReader["Lebar"]);
            objOPDetail.Berat = Convert.ToDecimal(sqlDataReader["Berat"]);
            objOPDetail.Quota = Convert.ToInt32(sqlDataReader["IsQuota"]);
            objOPDetail.PriceDefault = Convert.ToDecimal(sqlDataReader["PriceDefault"]);
            objOPDetail.Paket = Convert.ToInt32(sqlDataReader["Paket"]);

            objOPDetail.MBT = sqlDataReader["MBT"].ToString();
            objOPDetail.PriceRetail = Convert.ToDecimal(sqlDataReader["HargaRetail"]);

            return objOPDetail;

        }

        public OPDetail GenerateObjectPricePromo(SqlDataReader sqlDataReader)
        {
            objOPDetail = new OPDetail();

            objOPDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objOPDetail.OPID = Convert.ToInt32(sqlDataReader["OPID"]);
            objOPDetail.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objOPDetail.GroupName = sqlDataReader["GroupName"].ToString();
            objOPDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objOPDetail.ItemCode = sqlDataReader["ItemCode"].ToString();
            objOPDetail.ItemName = sqlDataReader["ItemName"].ToString();
            objOPDetail.ItemType = Convert.ToInt32(sqlDataReader["ItemType"]);
            objOPDetail.UOMCode = sqlDataReader["UOMCode"].ToString();
            objOPDetail.Quantity = Convert.ToInt32(sqlDataReader["Quantity"]);
            objOPDetail.QtyScheduled = Convert.ToInt32(sqlDataReader["QtyScheduled"]);
            objOPDetail.QtyReceived = Convert.ToInt32(sqlDataReader["QtyReceived"]);
            objOPDetail.Price = Convert.ToDecimal(sqlDataReader["Price"]);
            objOPDetail.Disc = Convert.ToDecimal(sqlDataReader["Disc"]);
            objOPDetail.TotalPrice = Convert.ToDecimal(sqlDataReader["TotalPrice"]);
            objOPDetail.Point = Convert.ToInt32(sqlDataReader["Point"]);
            objOPDetail.Tebal = Convert.ToDecimal(sqlDataReader["Tebal"]);
            objOPDetail.Panjang = Convert.ToDecimal(sqlDataReader["Panjang"]);
            objOPDetail.Lebar = Convert.ToDecimal(sqlDataReader["Lebar"]);
            objOPDetail.Berat = Convert.ToDecimal(sqlDataReader["Berat"]);
            objOPDetail.Quota = Convert.ToInt32(sqlDataReader["IsQuota"]);
            objOPDetail.PriceDefault = Convert.ToDecimal(sqlDataReader["PriceDefault"]);
            objOPDetail.Paket = Convert.ToInt32(sqlDataReader["Paket"]);

            objOPDetail.ZonaID = Convert.ToInt32(sqlDataReader["ZonaID"]);
            objOPDetail.DistSubID = Convert.ToInt32(sqlDataReader["DistSubID"]);

            return objOPDetail;

        }
        public OPDetail GenerateObjectUpdatePoint(SqlDataReader sqlDataReader)
        {
            objOPDetail = new OPDetail();
            objOPDetail.OPID = Convert.ToInt32(sqlDataReader["OPID"]);
            objOPDetail.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objOPDetail.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objOPDetail.Quantity = Convert.ToInt32(sqlDataReader["Quantity"]);
            objOPDetail.Point = Convert.ToInt32(sqlDataReader["Point"]);
            objOPDetail.DepoID = Convert.ToInt32(sqlDataReader["DepoID"]);
            objOPDetail.GroupCategory = sqlDataReader["GroupCategory"].ToString();
            objOPDetail.Paket = Convert.ToInt32(sqlDataReader["Paket"]);


            return objOPDetail;

        }
        public OPDetail GenerateObjectUpdatePoint2(SqlDataReader sqlDataReader)
        {
            objOPDetail = new OPDetail();
            objOPDetail.OPID = Convert.ToInt32(sqlDataReader["ID"]);

            return objOPDetail;
        }
        public OPDetail GenerateObjectCekVoucher(SqlDataReader sqlDataReader)
        {
            objOPDetail = new OPDetail();
            objOPDetail.KodeVoucher = sqlDataReader["KodeVoucher"].ToString();
            objOPDetail.ClaimVoucher = Convert.ToDecimal(sqlDataReader["ClaimVoucher"]);
            objOPDetail.Nominal = Convert.ToDecimal(sqlDataReader["Nominal"]);

            return objOPDetail;
        }
        public OPDetail GenerateObjectKirim(SqlDataReader sqlDataReader)
        {
            objOPDetail = new OPDetail();

            objOPDetail.Flag = Convert.ToInt32(sqlDataReader["Row_Counter"]);
            objOPDetail.RencanaTglKirim = Convert.ToDateTime(sqlDataReader["RencanaTglKirim"]);

            return objOPDetail;
        }

    }
}


