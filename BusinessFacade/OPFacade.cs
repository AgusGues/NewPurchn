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
    public class OPFacade : AbstractTransactionFacade
    {
        private OP objOP = new OP();
        private ArrayList arrOP;
        private List<SqlParameter> sqlListParam;

        public OPFacade(object objDomain)
            : base(objDomain)
        {
            objOP = (OP)objDomain;
        }

        public OPFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID2", objOP.ID2));
                sqlListParam.Add(new SqlParameter("@OPNo2", objOP.OPNo2));
                sqlListParam.Add(new SqlParameter("@ShipmentDate", objOP.ShipmentDate));
                sqlListParam.Add(new SqlParameter("@CustomerType", objOP.CustomerType));
                sqlListParam.Add(new SqlParameter("@CustomerID", objOP.CustomerID));
                sqlListParam.Add(new SqlParameter("@SalesID", objOP.SalesID));
                sqlListParam.Add(new SqlParameter("@AlamatLain", objOP.AlamatLain));
                sqlListParam.Add(new SqlParameter("@TypeOP", objOP.TypeOP));
                sqlListParam.Add(new SqlParameter("@Proyek", objOP.Proyek));
                sqlListParam.Add(new SqlParameter("@DepoID", objOP.DepoID));
                sqlListParam.Add(new SqlParameter("@DiambilSendiri", objOP.DiambilSendiri));
                sqlListParam.Add(new SqlParameter("@Status", objOP.Status));
                sqlListParam.Add(new SqlParameter("@Keterangan1", objOP.Keterangan1));
                sqlListParam.Add(new SqlParameter("@Keterangan2", objOP.Keterangan2));
                sqlListParam.Add(new SqlParameter("@TotalKubikasi", objOP.TotalKubikasi));
                sqlListParam.Add(new SqlParameter("@KabupatenID", objOP.KabupatenID));
                sqlListParam.Add(new SqlParameter("@NoDO2", objOP.NoDO2));
                sqlListParam.Add(new SqlParameter("@DistSubID", objOP.DistSubID));
                sqlListParam.Add(new SqlParameter("@TypeDistSub", objOP.TypeDistSub));
                sqlListParam.Add(new SqlParameter("@AlasanCancel", objOP.AlasanCancel));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objOP.CreatedBy));
                sqlListParam.Add(new SqlParameter("@AgenID", objOP.AgenID));
                sqlListParam.Add(new SqlParameter("@DeptID", objOP.DeptID));
                sqlListParam.Add(new SqlParameter("@InfoHBM", objOP.InfoHBM));
                sqlListParam.Add(new SqlParameter("@HbmCityID", objOP.HbmCityID));
                sqlListParam.Add(new SqlParameter("@BarangPromosi", objOP.BarangPromosi));
                sqlListParam.Add(new SqlParameter("@PTID", objOP.PTID));
                sqlListParam.Add(new SqlParameter("@ShipmentDateType", objOP.ShipmentDateType));
                sqlListParam.Add(new SqlParameter("@JenisCustomer", objOP.JenisCustomer));
                sqlListParam.Add(new SqlParameter("@LamaPembayaran", objOP.LamaPembayaran));
                sqlListParam.Add(new SqlParameter("@OpRetur", objOP.OpRetur));
                sqlListParam.Add(new SqlParameter("@CaraPembayaran", objOP.CaraPembayaran));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertOP");

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
                sqlListParam.Add(new SqlParameter("@ID", objOP.ID));
                sqlListParam.Add(new SqlParameter("@OPNo", objOP.OPNo));
                sqlListParam.Add(new SqlParameter("@ShipmentDate", objOP.ShipmentDate));
                sqlListParam.Add(new SqlParameter("@CustomerType", objOP.CustomerType));
                sqlListParam.Add(new SqlParameter("@CustomerID", objOP.CustomerID));
                sqlListParam.Add(new SqlParameter("@SalesID", objOP.SalesID));
                sqlListParam.Add(new SqlParameter("@AlamatLain", objOP.AlamatLain));
                sqlListParam.Add(new SqlParameter("@TypeOP", objOP.TypeOP));
                sqlListParam.Add(new SqlParameter("@Proyek", objOP.Proyek));
                sqlListParam.Add(new SqlParameter("@DepoID", objOP.DepoID));
                sqlListParam.Add(new SqlParameter("@DiambilSendiri", objOP.DiambilSendiri));
                sqlListParam.Add(new SqlParameter("@Status", objOP.Status));
                sqlListParam.Add(new SqlParameter("@Keterangan1", objOP.Keterangan1));
                sqlListParam.Add(new SqlParameter("@Keterangan2", objOP.Keterangan2));
                sqlListParam.Add(new SqlParameter("@TotalKubikasi", objOP.TotalKubikasi));
                sqlListParam.Add(new SqlParameter("@KabupatenID", objOP.KabupatenID));
                sqlListParam.Add(new SqlParameter("@NoDO", objOP.NoDO));
                sqlListParam.Add(new SqlParameter("@DistSubID", objOP.DistSubID));
                sqlListParam.Add(new SqlParameter("@TypeDistSub", objOP.TypeDistSub));
                sqlListParam.Add(new SqlParameter("@AlasanCancel", objOP.AlasanCancel));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objOP.CreatedBy));
                sqlListParam.Add(new SqlParameter("@LastApproveBy", objOP.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@DeptID", objOP.DeptID));
                sqlListParam.Add(new SqlParameter("@AgenID", objOP.AgenID));
                sqlListParam.Add(new SqlParameter("@InfoHBM", objOP.InfoHBM));
                sqlListParam.Add(new SqlParameter("@HbmCityID", objOP.HbmCityID));
                sqlListParam.Add(new SqlParameter("@BarangPromosi", objOP.BarangPromosi));
                sqlListParam.Add(new SqlParameter("@PTID", objOP.PTID));
                sqlListParam.Add(new SqlParameter("@ShipmentDateType", objOP.ShipmentDateType));
                sqlListParam.Add(new SqlParameter("@JenisCustomer", objOP.JenisCustomer));
                sqlListParam.Add(new SqlParameter("@LamaPembayaran", objOP.LamaPembayaran));
                sqlListParam.Add(new SqlParameter("@OpRetur", objOP.OpRetur));
                sqlListParam.Add(new SqlParameter("@CaraPembayaran", objOP.CaraPembayaran));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateOP");

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
                sqlListParam.Add(new SqlParameter("@ID", objOP.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objOP.LastModifiedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteOP");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateApproveOnly(TransactionManager transManager)
        {

            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objOP.ID));
                sqlListParam.Add(new SqlParameter("@Status", objOP.Status));
                sqlListParam.Add(new SqlParameter("@LastApproveBy", objOP.LastModifiedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateApproveOnly");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateApproveDate(TransactionManager transManager,int flag)
        {

            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objOP.ID));
                sqlListParam.Add(new SqlParameter("@Flag", flag));
                sqlListParam.Add(new SqlParameter("@ShipmentDateType", objOP.ShipmentDateType));


                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateApproveDate");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateUnApproveBy(TransactionManager transManager)
        {

            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objOP.ID));
                sqlListParam.Add(new SqlParameter("@Status", objOP.Status));
                sqlListParam.Add(new SqlParameter("@LastApproveBy", objOP.LastModifiedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateUnApproveBy");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateKeteranganOP(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objOP.ID));
                sqlListParam.Add(new SqlParameter("@AlamatLain", objOP.AlamatLain));
                sqlListParam.Add(new SqlParameter("@Keterangan2", objOP.Keterangan2));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateOPKeterangan");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateNilaiVoucher(TransactionManager transManager, string codeEncrpt)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@CodeEncrpt", codeEncrpt));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateDistributor_PotonganCode");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int InsertVoucherAmpau(TransactionManager transManager)
        {

            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@OPID", objOP.ID));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertPotonganVoucher");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 200 * from OP order by ID desc");
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrOP.Add(new OP());

            return arrOP;
        }
        public int RetrieveStrukAkang(string OPNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select sum(G.kupon) as JumKupon from( " +
            //                            "select A.ID as OPID, A.OPNo,C.Description,B.Quantity, " +
            //                            "case C.GroupCategory " +
            //                            "when 'FFT1220' then B.Quantity/10 " +
            //                            "when 'GB5' then B.Quantity/8 " +
            //                            "when 'GBD' then B.Quantity/7 " +
            //                            "when 'GB8' then B.Quantity/5 " +
            //                            "when 'SPANEL9' then B.Quantity/5 " +
            //                            "when 'SPP151220' then B.Quantity/3 " +
            //                            "when 'TBP100' then B.Quantity/60 " +
            //                            "when 'SPP100' then B.Quantity/60 " +
            //                            "when 'SMP100' then B.Quantity/60 " +
            //                            "when 'TBP200' then B.Quantity/30 " +
            //                            "when 'SPP200' then B.Quantity/30 " +
            //                            "when 'SMP200' then B.Quantity/30 " +
            //                            "when 'TBP300' then B.Quantity/20 " +
            //                            "when 'SPP300' then B.Quantity/20 " +
            //                            "when 'SMP300' then B.Quantity/20 end kupon, " +
            //                            "D.ID as TokoID, D.TokoCode,D.TokoName,A.ApproveSCDate from OP as A, OPDetail as B,Items as C, Toko as D " +
            //                            "where A.ID = B.OPID and B.ItemID = C.ID and A.CustomerType = 1 and A.Status in(0,1,2) " +
            //                            "and A.CustomerId = D.ID and D.PrioritasKirim = 1 and C.GroupCategory in('FFT1220','GB5','GBD','GB8','SPANEL9','SPP151220','TBP100','TBP200','TBP300', " +
            //                            "'SPP100','SPP200','SPP300','SMP100','SMP200','SMP300') and A.OPNo = '" + OPNo + "') as G " +
            //                            "group by OPID");

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select ISNULL(sum(L.Kupon),0) as JumKupon from( " +
                                        "select TokoID,akangitem,Qty, " +
                                        "case K.akangitem " +
                                        "when 10 then K.Qty/10  " +
                                        "when 8 then K.Qty/8  " +
                                        "when 7 then K.Qty/7  " +
                                        "when 5 then K.Qty/5  " +
                                        "when 3 then K.Qty/3  " +
                                        "when 60 then K.Qty/60  " +
                                        "when 30 then K.Qty/30  " +
                                        "when 3 then K.Qty/3  " +
                                        "when 20 then K.Qty/20 end Kupon " +
                                        " from ( " +
                                        "select akangitem,sum(Quantity) as Qty,TokoID from( " +
                                        "select A.ID as OPID, A.OPNo,C.Description, C.akangitem,B.Quantity, " +
                                        "case C.akangitem  " +
                                        "when 10 then B.Quantity/10  " +
                                        "when 8 then B.Quantity/8  " +
                                        "when 7 then B.Quantity/7  " +
                                        "when 5 then B.Quantity/5  " +
                                        "when 3 then B.Quantity/3  " +
                                        "when 60 then B.Quantity/60  " +
                                        "when 30 then B.Quantity/30  " +
                                        "when 3 then B.Quantity/3  " +
                                        "when 20 then B.Quantity/20  " +
                                        " end kupon,  " +
                                        "D.ID as TokoID, D.TokoCode,D.TokoName,A.ApproveSCDate from OP as A, OPDetail as B,Items as C, Toko as D  " +
                                        "where A.ID = B.OPID and B.ItemID = C.ID and A.CustomerType = 1 and A.Status in(0,1,2)  " +
                                        "and A.CustomerId = D.ID and D.PrioritasKirim = 1 and C.akangitem>0 and A.OPNo = '" + OPNo + "') as G " +
                                        "group by akangitem,TokoID) as K) as L");


            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["JumKupon"]);
                }
            }

            return 0;
        }
        public ArrayList RetrieveByDistributor(int distributorID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 50 A.ID,A.PTID,A.ID2,A.OPNo,A.OpNo2,A.ShipmentDate,A.JenisCustomer,A.CustomerType,A.CustomerID,A.SalesID,A.AlamatLain,A.TypeOP,A.Proyek,A.DepoID,A.DiambilSendiri,A.Status,A.Keterangan1,A.Keterangan2,A.TotalKubikasi,A.KabupatenID,A.NoDO,A.NoDO2,A.ApproveDistDate,A.ApproveSCDate,A.DistSubID,A.TypeDistSub,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.AgenID,A.DeptID,A.InfoHBM,A.HBMcityID,A.ShipmentDateType,A.ImsID,A.CaraPembayaran from OP as A,Toko as B where A.CustomerId = B.ID and A.CustomerType = 1 and ((A.TypeDistSub=1 and A.DistSubID = " + distributorID + ") or (A.TypeDistSub=2 and A.DistSubID in (select ID from SubDistributor where DistributorID=" + distributorID + "))) order by A.ID desc");            
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrOP.Add(new OP());

            return arrOP;
        }
        public OP RetrieveByCustID(int CustomerID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 1 * from OP where Status > -1 and CustomerType = 2 and CustomerID = " + CustomerID + " order by ID desc");
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new OP();
        }
        public ArrayList RetrieveByDistributor2(int distributorID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 50 A.ID,A.PTID,A.ID2,A.OPNo,A.OpNo2,A.ShipmentDate,A.JenisCustomer,A.CustomerType,A.CustomerID,A.SalesID,A.AlamatLain,A.TypeOP,A.Proyek,A.DepoID,A.DiambilSendiri,A.Status,A.Keterangan1,A.Keterangan2,A.TotalKubikasi,A.KabupatenID,A.NoDO,A.NoDO2,A.ApproveDistDate,A.ApproveSCDate,A.DistSubID,A.TypeDistSub,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.DeptID,A.InfoHBM,A.HBMcityID,A.ShipmentDateType,A.ImsID,A.CaraPembayaran from OP as A,Toko as B where A.CustomerId = B.ID and A.CustomerType = 1 and B.DistributorID = " + distributorID + " order by A.ID desc");
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrOP.Add(new OP());

            return arrOP;
        }
        public ArrayList RetrieveByDistAndPeriodAndActualShipmentDate(int distributorID, int CustomerType, int distSub, string thBl)
        {
            string strQuery = "select top 200 A.ID,A.ID2,A.OPNo,A.OpNo2,A.ShipmentDate,A.CustomerType,A.CustomerID,A.SalesID,A.AlamatLain,A.TypeOP,A.Proyek,A.DepoID," +
                "A.DiambilSendiri,A.Status,A.Keterangan1,A.Keterangan2,A.TotalKubikasi,A.KabupatenID,A.NoDO,A.NoDO2,A.ApproveDistDate,A.ApproveSCDate," +
                "A.DistSubID,A.TypeDistSub,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.AgenID, C.ID as SuratJalanID, C.SuratJalanNo " +
                "from OP as A,Toko as B, SuratJalan as C where A.CustomerId = B.ID and A.CustomerType = " + CustomerType + " and A.TypeDistSub=" + distSub + " and B.DistributorID = " + distributorID + " and " +
                "A.ID=C.OPID and C.Status>-1 and left( CONVERT(varchar,C.ActualShipmentDate,112),6)='" + thBl + "' order by ActualShipmentDate desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strQuery);
            //and A.Pajak=0 --> knapa pake ini 01/08/2013
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOP.Add(GenerateObjectSJ(sqlDataReader));
                }
            }
            else
                arrOP.Add(new OP());

            return arrOP;
        }
        public ArrayList RetrieveOPtunaiDist(int distributorID, int CustomerType, int distSub)
        {
            string strQuery = "select * from (select A.ID,A.OPNo +' - '+ TokoName as OPno, (select sum(TotalPrice ) + 6000 from OPDetail where OPID=a.ID)  as Harga, " +
                "(select isnull(sum(Debet),0) as Bayar from BankIn as a, BankInDetail as b where a.ID=b.BankInID and a.PTID=100 and b.OPID=A.ID) as UangMasuk "+
                "from OP as A,Toko as B where A.CustomerId = B.ID and A.CustomerType = "+CustomerType+" and A.TypeDistSub="+distSub+" and B.DistributorID = "+distributorID+" and A.CaraPembayaran=1 and left( CONVERT(varchar,ApproveSCDate,112),6)>='201712' "+
                ") as z where Harga-UangMasuk>0 order by ID";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strQuery);
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOP.Add(GenerateObject5(sqlDataReader));
                }
            }
            else
                arrOP.Add(new OP());

            return arrOP;
        }
        public ArrayList RetrieveByDistAndPeriodCBDonly(int custID, int CustomerType, int distSub, string thBl)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 20 A.ID,A.ID2,A.OPNo,A.OpNo2,A.ShipmentDate,A.CustomerType,A.CustomerID,A.SalesID,A.AlamatLain,A.TypeOP,A.Proyek,A.DepoID,"+
                "A.DiambilSendiri,A.Status,A.Keterangan1,A.Keterangan2,A.TotalKubikasi,A.KabupatenID,A.NoDO,A.NoDO2,A.ApproveDistDate,A.ApproveSCDate,"+
                "A.DistSubID,A.TypeDistSub,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.AgenID, 0 as SuratJalanID, '' as SuratJalanNo "+
                "from OP as A where A.CustomerType = " + CustomerType + " and A.CustomerId=" + custID + " and Status=1");
            //and A.Pajak=0 --> knapa pake ini 01/08/2013
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOP.Add(GenerateObjectSJ(sqlDataReader));
                }
            }
            else
                arrOP.Add(new OP());

            return arrOP;
        }
        public ArrayList RetrieveByCustomerUtkCekDiSJ(int custID, int CustomerType, int distSub, string thBl)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 100 A.ID,A.ID2,A.OPNo,A.OpNo2,A.ShipmentDate,A.CustomerType,A.CustomerID,A.SalesID,A.AlamatLain,A.TypeOP,A.Proyek,A.DepoID," +
                "A.DiambilSendiri,A.Status,A.Keterangan1,A.Keterangan2,A.TotalKubikasi,A.KabupatenID,A.NoDO,A.NoDO2,A.ApproveDistDate,A.ApproveSCDate," +
                "A.DistSubID,A.TypeDistSub,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.AgenID, 0 as SuratJalanID, '' as SuratJalanNo " +
                "from OP as A where A.CustomerType = " + CustomerType + " and A.CustomerId=" + custID + " and Status in (3,4) order by ID desc ");
            //and A.Pajak=0 --> knapa pake ini 01/08/2013
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOP.Add(GenerateObjectSJ(sqlDataReader));
                }
            }
            else
                arrOP.Add(new OP());

            return arrOP;
        }
        public ArrayList RetrieveByDistAndPeriod(int distributorID, int CustomerType, int distSub, string thBl)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 20 A.ID,A.ID2,A.OPNo,A.OpNo2,A.ShipmentDate,A.CustomerType,A.CustomerID,A.SalesID,A.AlamatLain,A.TypeOP,A.Proyek,A.DepoID,"+
                "A.DiambilSendiri,A.Status,A.Keterangan1,A.Keterangan2,A.TotalKubikasi,A.KabupatenID,A.NoDO,A.NoDO2,A.ApproveDistDate,A.ApproveSCDate,"+
                "A.DistSubID,A.TypeDistSub,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.AgenID, C.ID as SuratJalanID, C.SuratJalanNo " +
                "from OP as A,Toko as B, SuratJalan as C where A.CustomerId = B.ID and A.CustomerType = "+CustomerType+" and A.TypeDistSub="+distSub+" and B.DistributorID = "+distributorID+" and "+
                "A.ID=C.OPID and C.Status>-1 and left( CONVERT(varchar,A.ApproveSCDate,112),6)='" + thBl + "' order by ActualShipmentDate desc");
            //and A.Pajak=0 --> knapa pake ini 01/08/2013
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOP.Add(GenerateObjectSJ(sqlDataReader));
                }
            }
            else
                arrOP.Add(new OP());

            return arrOP;
        }
        public ArrayList RetrieveByCustomerAndPeriod(int distributorID, int CustomerType, string thBl)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 50 A.ID,A.ID2,A.OPNo,A.OpNo2,A.ShipmentDate,A.CustomerType,A.CustomerID,A.SalesID,A.AlamatLain,A.TypeOP,A.Proyek,A.DepoID,"+
            "A.DiambilSendiri,A.Status,A.Keterangan1,A.Keterangan2,A.TotalKubikasi,A.KabupatenID,A.NoDO,A.NoDO2,A.ApproveDistDate,A.ApproveSCDate,"+
            "A.DistSubID,A.TypeDistSub,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.AgenID, 0 as SuratJalanID, '' as SuratJalanNo "+
            "from OP as A,Customer as B where A.CustomerId = B.ID and A.CustomerType = " + CustomerType + "  and A.CustomerId = " + distributorID + " and A.Status>=1 and " +
            "left( CONVERT(varchar,A.ApproveDistDate,112),6)='" + thBl + "' ");
            //and A.Pajak=0 --> knapa pake ini 01/08/2013
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOP.Add(GenerateObjectSJ(sqlDataReader));
                }
            }
            else
                arrOP.Add(new OP());

            return arrOP;
        }

        public ArrayList RetrieveByDistAndPeriod2(int agenId, int CustomerType, string thBl)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 50 A.ID,A.ID2,A.OPNo,A.OpNo2,A.ShipmentDate,A.CustomerType,A.CustomerID,A.SalesID,A.AlamatLain,A.TypeOP,A.Proyek,A.DepoID," +
                "A.DiambilSendiri,A.Status,A.Keterangan1,A.Keterangan2,A.TotalKubikasi,A.KabupatenID,A.NoDO,A.NoDO2,A.ApproveDistDate,A.ApproveSCDate," +
                "A.DistSubID,A.TypeDistSub,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.AgenID, C.ID as SuratJalanID, C.SuratJalanNo " +
                "from OP as A,Toko as B, SuratJalan as C where A.CustomerId = B.ID and A.CustomerType = " + CustomerType + " and A.AgenID='" + agenId + "' and " +
                "A.ID=C.OPID and C.Status>-1 and left( CONVERT(varchar,A.ApproveSCDate,112),6)='" + thBl + "' order by A.ID");
            //and A.Pajak=0 --> knapa pake ini 01/08/2013
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOP.Add(GenerateObjectSJ(sqlDataReader));
                }
            }
            else
                arrOP.Add(new OP());

            return arrOP;
        }

        public ArrayList RetrieveByDepo(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 200 * from OP where DepoID = " + depoID + " order by id desc");
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrOP.Add(new OP());

            return arrOP;
        }


        public ArrayList RetrieveOpenStatus()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from OP where Status = 0");
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrOP.Add(new OP());

            return arrOP;
        }

        public ArrayList RetrieveApprovalOpenStatusByDepo(int depoId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from OP where Status = 1 and DepoID = " + depoId +" order by OPNo");
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrOP.Add(new OP());

            return arrOP;
        }
        public ArrayList RetrieveApprovalOpenStatusByDepoTokoRetailJatim(int depoId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from OP where Status = 1 and DepoID = "+depoId+" and TypeDistSub=1 and DistSubID=57  order by OPNo");
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrOP.Add(new OP());

            return arrOP;
        }
        public ArrayList RetrieveApprovalOpenStatusByDepoTokoRetailJatim2(int depoId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from OP where Status = 1 and DepoID = " + depoId + " and DistSubID!=57  order by OPNo");
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrOP.Add(new OP());

            return arrOP;
        }
        public ArrayList RetrieveApprovalOpenStatusByDept(int deptId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from OP where Status = 0 and DeptID = " + deptId + " order by OPNo");
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrOP.Add(new OP());

            return arrOP;
        }
        public ArrayList RetrieveApprovalOpenStatusBy2Dept(string deptId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from OP where Status = 0 and DeptID in (" + deptId + ") order by OPNo");
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrOP.Add(new OP());

            return arrOP;
        }
        public ArrayList RetrieveOpenStatusByDistributor(int distributorId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.PTID,A.ID2,A.OPNo,A.OpNo2,A.ShipmentDate,A.JenisCustomer,A.CustomerType,A.CustomerID,A.SalesID,A.AlamatLain,A.TypeOP,A.Proyek,A.DepoID,A.DiambilSendiri,A.Status,A.Keterangan1,A.Keterangan2,A.TotalKubikasi,A.KabupatenID,A.NoDO,A.NoDO2,A.ApproveDistDate,A.ApproveSCDate,A.DistSubID,A.TypeDistSub,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.AgenID,A.DeptID,A.InfoHBM,A.HBMcityID,A.ShipmentDateType,A.ImsID,A.CaraPembayaran from OP as A,Toko as B where A.CustomerId = B.ID and A.CustomerType = 1 and A.Status = 0 and B.DistributorID = " + distributorId + " order by OPNo");
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrOP.Add(new OP());

            return arrOP;
        }
        public ArrayList RetrieveOpenStatusByDistributor2(int distributorId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ID2,A.OPNo,A.OpNo2,A.ShipmentDate,A.CustomerType,A.CustomerID,A.SalesID,A.AlamatLain,A.TypeOP,A.Proyek,A.DepoID,A.DiambilSendiri,A.Status,A.Keterangan1,A.Keterangan2,A.TotalKubikasi,A.KabupatenID,A.NoDO,A.NoDO2,A.ApproveDistDate,A.ApproveSCDate,A.DistSubID,A.TypeDistSub,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.AgenID,A.UnApprove from OP as A,Toko as B where A.CustomerId = B.ID and A.CustomerType = 1 and A.Status = 0 and ((A.TypeDistSub=1 and A.DistSubID = " + distributorId + ") or (A.TypeDistSub=2 and A.DistSubID in (select ID from SubDistributor where DistributorID=" + distributorId + "))) order by OPNo");
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOP.Add(GenerateObjectLastApp(sqlDataReader));
                }
            }
            else
                arrOP.Add(new OP());

            return arrOP;
        }
        //tambahan
        public ArrayList RetrieveApprovalOpenStatusByDepo2(int depoId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from OP where Status = 0 and DepoID = " + depoId + " order by OPNo");
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrOP.Add(new OP());

            return arrOP;
        }
        //
        //tambahan
        public ArrayList RetrieveApprovalOpenStatusBy2Dept2(string deptId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from OP where Status = 1 and DeptID in (" + deptId + ") order by OPNo");
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrOP.Add(new OP());

            return arrOP;
        }
        //

        public ArrayList RetrieveOpenStatusByDepo(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from OP where Status = 0 and DepoID = " + depoID);
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrOP.Add(new OP());

            return arrOP;
        }


        public ArrayList RetrieveApproveStatus()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT A.ID,A.PTID,A.OPNo,A.ID2,A.OpNo2,A.ShipmentDate,A.JenisCustomer,A.CustomerType,A.CustomerId,A.SalesID,A.AlamatLain,A.TypeOP,A.Proyek,A.DepoID,A.DiambilSendiri,A.Status,A.Keterangan1,A.Keterangan2,A.TotalKubikasi,A.KabupatenID,B.NamaKabupaten,C.CityName,A.NoDO,A.NoDO2,A.ApproveDistDate,A.ApproveSCDate,A.DistSubID,A.TypeDistSub,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.DeptID,A.InfoHBM,A.HBMcityID,A.ShipmentDateType,A.ImsID,A.CaraPembayaran FROM OP as A,Kabupaten as B,City as C where A.KabupatenID = B.ID and B.CityID = C.ID and A.Status in (2,3) order by B.NamaKabupaten,C.CityName");
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrOP.Add(new OP());

            return arrOP;
        }

        //public ArrayList RetrieveApproveStatusByDepo(int depoID)
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT A.ID,A.OPNo,A.ID2,A.OpNo2,A.ShipmentDate,A.CustomerType,A.CustomerId,A.SalesID,A.AlamatLain,A.TypeOP,A.Proyek,A.DepoID,A.DiambilSendiri,A.Status,A.Keterangan1,A.Keterangan2,A.TotalKubikasi,A.KabupatenID,B.NamaKabupaten,C.CityName,A.NoDO,A.NoDO2,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime FROM OP as A,Kabupaten as B,City as C where A.KabupatenID = B.ID and B.CityID = C.ID and A.Status in (2,3) and A.DepoID = " + depoID + " order by C.CityName,B.NamaKabupaten");
        //    strError = dataAccess.Error;
        //    arrOP = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrOP.Add(GenerateObject(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrOP.Add(new OP());

        //    return arrOP;
        //}

        public ArrayList RetrieveSJinvCPDbyDepo(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT A.ID,A.OPNo,A.ID2,A.OpNo2,A.ShipmentDate,A.SalesID,A.CustomerType,A.CustomerId,case A.CustomerType when 1 then (select Distributor.Urutan from Distributor,Toko where Toko.ID = A.CustomerId and Toko.DistributorID = Distributor.id) else 2 end Urutan,A.AlamatLain,A.TypeOP,A.Proyek,A.DepoID,A.DiambilSendiri,A.Status,case A.CustomerType when 1 then (select Keterangan from Toko where ID = A.CustomerId) else '-' end Keterangan,A.Keterangan1,A.Keterangan2,case (select sum(QtyScheduled) from OPDetail where OPID = A.ID) when 0 then (select sum(OpDetail.Berat * OPDetail.Quantity) from OPDetail where OPID = A.ID) else (select sum(OpDetail.Berat * (OPDetail.Quantity - OPDetail.QtyScheduled)) from OPDetail where OPID = A.ID) end TotalKubikasi,A.KabupatenID,B.NamaKabupaten,C.CityName,A.NoDO,A.NoDO2,A.ApproveDistDate,A.ApproveSCDate,A.DistSubID,A.TypeDistSub,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime, 0 as UmurOP FROM OP as A,Kabupaten as B,City as C,SalesMan D where A.KabupatenID = B.ID and B.CityID = C.ID and A.Status in (2,3) and A.DepoID = 1 and D.ID = A.SalesID order by Urutan,B.NamaKabupaten,A.ID");
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOP.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrOP.Add(new OP());

            return arrOP;
        }
        public ArrayList RetrieveApproveStatusByDepo(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT A.ID,A.OPNo,A.ID2,A.OpNo2,A.ShipmentDate,A.SalesID,A.CustomerType,A.CustomerId,case A.CustomerType when 1 then (select Distributor.Urutan from Distributor,Toko where Toko.ID = A.CustomerId and Toko.DistributorID = Distributor.id) else 2 end Urutan,A.AlamatLain,A.TypeOP,A.Proyek,A.DepoID,A.DiambilSendiri,A.Status,case A.CustomerType when 1 then (select Keterangan from Toko where ID = A.CustomerId) else '-' end Keterangan,A.Keterangan1,A.Keterangan2,case (select sum(QtyScheduled) from OPDetail where OPID = A.ID) when 0 then (select sum(OpDetail.Berat * OPDetail.Quantity) from OPDetail where OPID = A.ID) else (select sum(OpDetail.Berat * (OPDetail.Quantity - OPDetail.QtyScheduled)) from OPDetail where OPID = A.ID) end TotalKubikasi,A.KabupatenID,B.NamaKabupaten,C.CityName,A.NoDO,A.NoDO2,A.ApproveDistDate,A.ApproveSCDate,A.DistSubID,A.TypeDistSub,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime, 0 as UmurOP FROM OP as A,Kabupaten as B,City as C,SalesMan D where A.KabupatenID = B.ID and B.CityID = C.ID and A.Status in (2,3) and A.DepoID = 1 and D.ID = A.SalesID order by Urutan,B.NamaKabupaten,A.ID");
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOP.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrOP.Add(new OP());

            return arrOP;
        }
        public ArrayList RetrieveApproveStatusByDepoPerTgl(int depoID, string drT, string sdT)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT A.ID,A.OPNo,A.ID2,A.OpNo2,A.ShipmentDate,A.SalesID,A.CustomerType,A.CustomerId,case A.CustomerType when 1 then (select Distributor.Urutan from Distributor,Toko where Toko.ID = A.CustomerId and Toko.DistributorID = Distributor.id) else 2 end Urutan,A.AlamatLain,A.TypeOP,A.Proyek,A.DepoID,A.DiambilSendiri,A.Status,case A.CustomerType when 1 then (select Keterangan from Toko where ID = A.CustomerId) else '-' end Keterangan,A.Keterangan1,A.Keterangan2,case (select sum(QtyScheduled) from OPDetail where OPID = A.ID) when 0 then (select sum(OpDetail.Berat * OPDetail.Quantity) from OPDetail where OPID = A.ID) else (select sum(OpDetail.Berat * (OPDetail.Quantity - OPDetail.QtyScheduled)) from OPDetail where OPID = A.ID) end TotalKubikasi,A.KabupatenID,B.NamaKabupaten,C.CityName,A.NoDO,A.NoDO2,A.ApproveDistDate,A.ApproveSCDate,A.DistSubID,A.TypeDistSub,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime, 0 as UmurOP FROM OP as A,Kabupaten as B,City as C,SalesMan D where A.KabupatenID = B.ID and B.CityID = C.ID and A.Status in (2,3) and A.DepoID = 1 and D.ID = A.SalesID and convert(varchar,A.ApproveSCDate,112)>='" + drT + "' and convert(varchar,A.ApproveSCDate,112)<='" + sdT + "' order by Urutan,B.NamaKabupaten,A.ID");
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOP.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrOP.Add(new OP());

            return arrOP;
        }
        public ArrayList RetrieveApproveStatusByDepoPerCity(string strCity1, string strCity2, string strCity3, string strCity4)
        {
            string listCity = string.Empty;

            if (strCity1 != string.Empty && strCity2 == string.Empty && strCity3 == string.Empty && strCity4 == string.Empty)
                listCity = " and C.CityName in ('"+strCity1+"') ";
            else if (strCity1 != string.Empty && strCity2 != string.Empty && strCity3 == string.Empty && strCity4 == string.Empty)
                listCity = " and C.CityName in ('" + strCity1 + "','"+ strCity2 + "') ";
            else if (strCity1 != string.Empty && strCity2 != string.Empty && strCity3 != string.Empty && strCity4 == string.Empty)
                listCity = " and C.CityName in ('" + strCity1 + "','" + strCity2 + "','" + strCity3 + "') ";
            else if (strCity1 != string.Empty && strCity2 != string.Empty && strCity3 != string.Empty && strCity4 != string.Empty)
                listCity = " and C.CityName in ('" + strCity1 + "','" + strCity2 + "','" + strCity3 + "','" + strCity4 + "') ";
            else
                listCity = string.Empty;

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT A.ID,A.OPNo,A.ID2,A.OpNo2,A.ShipmentDate,A.SalesID,A.CustomerType,A.CustomerId,case A.CustomerType when 1 then (select Distributor.Urutan from Distributor,Toko where Toko.ID = A.CustomerId and Toko.DistributorID = Distributor.id) else 2 end Urutan,A.AlamatLain,A.TypeOP,A.Proyek,A.DepoID,A.DiambilSendiri,A.Status,case A.CustomerType when 1 then (select Keterangan from Toko where ID = A.CustomerId) else '-' end Keterangan,A.Keterangan1,A.Keterangan2,case (select sum(QtyScheduled) from OPDetail where OPID = A.ID) when 0 then (select sum(OpDetail.Berat * OPDetail.Quantity) from OPDetail where OPID = A.ID) else (select sum(OpDetail.Berat * (OPDetail.Quantity - OPDetail.QtyScheduled)) from OPDetail where OPID = A.ID) end TotalKubikasi,A.KabupatenID,B.NamaKabupaten,C.CityName,A.NoDO,A.NoDO2,A.ApproveDistDate,A.ApproveSCDate,A.DistSubID,A.TypeDistSub,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime FROM OP as A,Kabupaten as B,City as C,SalesMan D where A.KabupatenID = B.ID and B.CityID = C.ID and A.Status in (2,3) and A.DepoID = 1 and D.ID = A.SalesID " + listCity + " order by Urutan,C.CityName,B.NamaKabupaten,A.ID");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT A.ID,A.OPNo,A.ID2,A.OpNo2,A.ShipmentDate,A.SalesID,A.CustomerType,A.CustomerId,case A.CustomerType when 1 then (select Distributor.Urutan from Distributor,Toko where Toko.ID = A.CustomerId and Toko.DistributorID = Distributor.id) else 2 end Urutan,A.AlamatLain,A.TypeOP,A.Proyek,A.DepoID,A.DiambilSendiri,A.Status,case A.CustomerType when 1 then (select Keterangan from Toko where ID = A.CustomerId) else '-' end Keterangan,A.Keterangan1,A.Keterangan2,case (select sum(QtyScheduled) from OPDetail where OPID = A.ID) when 0 then (select sum(OpDetail.Berat * OPDetail.Quantity) from OPDetail where OPID = A.ID) else (select sum(OpDetail.Berat * (OPDetail.Quantity - OPDetail.QtyScheduled)) from OPDetail where OPID = A.ID) end TotalKubikasi,A.KabupatenID,B.NamaKabupaten,C.CityName,A.NoDO,A.NoDO2,A.ApproveDistDate,A.ApproveSCDate,A.DistSubID,A.TypeDistSub,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime, 0 as UmurOP FROM OP as A,Kabupaten as B,City as C,SalesMan D where A.KabupatenID = B.ID and B.CityID = C.ID and A.Status in (2,3) and A.DepoID = 1 and D.ID = A.SalesID " + listCity +                 
                //" order by Urutan,C.CityName,B.NamaKabupaten,A.ID");
                //request leni 28mei2018 add ShipmentDate
                " order by ShipmentDate,A.Prioritas desc,PrioritasKirim desc,CityName,NamaKabupaten" );
            
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOP.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrOP.Add(new OP());

            return arrOP;
        }
        public ArrayList RetrieveApproveStatusByDepoPerCityPerPrioritasKirimAying(string strCity1, string strCity2, string strCity3, string strCity4, string tglKirim)
        {
            string listCity = string.Empty;

            if (strCity1 != string.Empty && strCity2 == string.Empty && strCity3 == string.Empty && strCity4 == string.Empty)
                listCity = " and C.CityName in ('" + strCity1 + "') ";
            else if (strCity1 != string.Empty && strCity2 != string.Empty && strCity3 == string.Empty && strCity4 == string.Empty)
                listCity = " and C.CityName in ('" + strCity1 + "','" + strCity2 + "') ";
            else if (strCity1 != string.Empty && strCity2 != string.Empty && strCity3 != string.Empty && strCity4 == string.Empty)
                listCity = " and C.CityName in ('" + strCity1 + "','" + strCity2 + "','" + strCity3 + "') ";
            else if (strCity1 != string.Empty && strCity2 != string.Empty && strCity3 != string.Empty && strCity4 != string.Empty)
                listCity = " and C.CityName in ('" + strCity1 + "','" + strCity2 + "','" + strCity3 + "','" + strCity4 + "') ";
            else
                listCity = string.Empty;
            
            //24mei2018, leni request utk op toko jika tgl kirim standart maka pada list ini -2 (only)
            string strQuery =
                "select * from ( "+
                "SELECT A.Prioritas,A.ID,A.OPNo+case A.ShipmentDateType when 2 then ' (OnRequest)' when 3 then ' (HBM)' else '' end OPNo,A.ID2,A.OpNo2,case when A.CustomerType=1 and A.ShipmentDateType=1 then case when DATENAME(dw, DATEADD(day,-2,ShipmentDate) )='Sunday' then DATEADD(day,-1,ShipmentDate) else DATEADD(day,-2,ShipmentDate) end else A.ShipmentDate end ShipmentDate,A.SalesID,A.CustomerType,A.CustomerId,case A.CustomerType when 1 then (select Distributor.Urutan " +
                "from Distributor,Toko where Toko.ID = A.CustomerId and Toko.DistributorID = Distributor.id) else 2 end Urutan,A.AlamatLain,A.TypeOP,A.Proyek,A.DepoID," +
                "A.DiambilSendiri,A.Status,case A.CustomerType when 1 then case when (select COUNT(ID)from OP where Prioritas=1 and ID=A.ID)>0 then ' [ AGEN SETIA ] ' else '' end +  (select Keterangan from Toko where ID = A.CustomerId)+  " +
                "case when (select COUNT(KodeVoucher)from AkangVoucher where OPID=A.ID)>0 then ' ->(Akang Voucher='+ rtrim(cast((select COUNT(KodeVoucher) from AkangVoucher where OPID=A.ID) as CHAR))+')<-' else '' end else '-' end Keterangan,A.Keterangan1," +
                "A.Keterangan2,case (select sum(QtyScheduled) from OPDetail where OPID = A.ID) when 0 then (select sum(OpDetail.Berat * OPDetail.Quantity) " +
                "from OPDetail where OPID = A.ID) else (select sum(OpDetail.Berat * (OPDetail.Quantity - OPDetail.QtyScheduled)) from OPDetail where OPID = A.ID) " +
                "end TotalKubikasi,A.KabupatenID,B.NamaKabupaten,C.CityName,A.NoDO,A.NoDO2,A.ApproveDistDate,A.ApproveSCDate,A.DistSubID,A.TypeDistSub,A.CreatedBy," +
                "A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime, case A.CustomerType when 1 then (select PrioritasKirim from Toko where Toko.ID = A.CustomerID) else 0 end PrioritasKirim,DATEDIFF(DAY,A.ApproveSCDate,GETDATE()) as UmurOP, " +
                //"case when isnull((select OPID from OPScheduleKirim where OPScheduleKirim.OPID=A.ID and OPScheduleKirim.RowStatus>-1 and CONVERT(VARCHAR, OPScheduleKirim.RencanaTglKirim, 112) <=CONVERT(VARCHAR, '" + tglKirim + "', 112)),0) > 1 " +
                //"then 0 else 1 end IDrencanaKirim "+
                "isnull((select top 1 OPID from OPScheduleKirim where OPScheduleKirim.OPID=A.ID and OPScheduleKirim.RowStatus>-1 order by id desc),0) as IDrencanaKirim " +
                "FROM OP as A,Kabupaten as B,City as C,SalesMan D where A.KabupatenID = B.ID and B.CityID = C.ID and A.Status in (2,3) and A.DepoID = 1 and D.SalesmanType!='HO' and D.ID = A.SalesID " + listCity + " " +
                ") as aa where IDrencanaKirim=0 " +
                "union all " +
                "select * from (SELECT A.Prioritas,A.ID,A.OPNo+case A.ShipmentDateType when 2 then ' (OnRequest)' when 3 then ' (HBM)' else '' end OPNo,A.ID2,A.OpNo2,case when A.CustomerType=1 and A.ShipmentDateType=1 then case when DATENAME(dw, DATEADD(day,-2,ShipmentDate) )='Sunday' then DATEADD(day,-1,ShipmentDate) else DATEADD(day,-2,ShipmentDate) end else A.ShipmentDate end ShipmentDate,A.SalesID,A.CustomerType,A.CustomerId,case A.CustomerType when 1 then (select Distributor.Urutan " +
                "from Distributor,Toko where Toko.ID = A.CustomerId and Toko.DistributorID = Distributor.id) else 2 end Urutan,A.AlamatLain,A.TypeOP,A.Proyek,A.DepoID," +
                "A.DiambilSendiri,A.Status,case A.CustomerType when 1 then case when (select COUNT(ID)from OP where Prioritas=1 and ID=A.ID)>0 then ' [ AGEN SETIA ] ' else '' end +  (select Keterangan from Toko where ID = A.CustomerId)+  " +
                "case when (select COUNT(KodeVoucher)from AkangVoucher where OPID=A.ID)>0 then ' ->(Akang Voucher='+ rtrim(cast((select COUNT(KodeVoucher) from AkangVoucher where OPID=A.ID) as CHAR))+')<-' else '' end else '-' end Keterangan,A.Keterangan1," +
                "A.Keterangan2,case (select sum(QtyScheduled) from OPDetail where OPID = A.ID) when 0 then (select sum(OpDetail.Berat * OPDetail.Quantity) " +
                "from OPDetail where OPID = A.ID) else (select sum(OpDetail.Berat * (OPDetail.Quantity - OPDetail.QtyScheduled)) from OPDetail where OPID = A.ID) " +
                "end TotalKubikasi,A.KabupatenID,B.NamaKabupaten,C.CityName,A.NoDO,A.NoDO2,A.ApproveDistDate,A.ApproveSCDate,A.DistSubID,A.TypeDistSub,A.CreatedBy," +
                "A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime, case A.CustomerType when 1 then (select PrioritasKirim from Toko where Toko.ID = A.CustomerID) else 0 end PrioritasKirim,DATEDIFF(DAY,A.ApproveSCDate,GETDATE()) as UmurOP,isnull((select top 1 OPID from OPScheduleKirim where OPScheduleKirim.OPID=A.ID and OPScheduleKirim.RowStatus>-1 and OPScheduleKirim.RencanaTglKirim <=DATEADD(day,1,cast('" + tglKirim + "' as datetime)) ),0) as IDrencanaKirim " +
                "FROM OP as A,Kabupaten as B,City as C,SalesMan D where A.KabupatenID = B.ID and B.CityID = C.ID and A.Status in (2,3) and A.DepoID = 1 and D.SalesmanType!='HO' and D.ID = A.SalesID " + listCity + " ) as aa where IDrencanaKirim>0" +
                //" order by PrioritasKirim desc,Urutan,CityName,NamaKabupaten,ID";
                //hasil meeting jumat 30Sept
                " order by Prioritas desc,PrioritasKirim desc,CityName,NamaKabupaten";


            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strQuery);

            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOP.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrOP.Add(new OP());

            return arrOP;
        }

        public ArrayList RetrieveApproveStatusByDepoPerCityPerPrioritasKirim(string strCity1, string strCity2, string strCity3, string strCity4)
        {
            string listCity = string.Empty;

            if (strCity1 != string.Empty && strCity2 == string.Empty && strCity3 == string.Empty && strCity4 == string.Empty)
                listCity = " and C.CityName in ('" + strCity1 + "') ";
            else if (strCity1 != string.Empty && strCity2 != string.Empty && strCity3 == string.Empty && strCity4 == string.Empty)
                listCity = " and C.CityName in ('" + strCity1 + "','" + strCity2 + "') ";
            else if (strCity1 != string.Empty && strCity2 != string.Empty && strCity3 != string.Empty && strCity4 == string.Empty)
                listCity = " and C.CityName in ('" + strCity1 + "','" + strCity2 + "','" + strCity3 + "') ";
            else if (strCity1 != string.Empty && strCity2 != string.Empty && strCity3 != string.Empty && strCity4 != string.Empty)
                listCity = " and C.CityName in ('" + strCity1 + "','" + strCity2 + "','" + strCity3 + "','" + strCity4 + "') ";
            else
                listCity = string.Empty;

            string strQuery = "SELECT A.ID,A.OPNo+case A.ShipmentDateType when 2 then ' (OnRequest)' when 3 then ' (HBM)' else '' end OPNo,A.ID2,A.OpNo2,case when A.CustomerType=1 and A.ShipmentDateType=1 then case when DATENAME(dw, DATEADD(day,-2,ShipmentDate) )='Sunday' then DATEADD(day,-1,ShipmentDate) else DATEADD(day,-2,ShipmentDate) end else A.ShipmentDate end ShipmentDate,A.SalesID,A.CustomerType,A.CustomerId,case A.CustomerType when 1 then (select Distributor.Urutan " +
                "from Distributor,Toko where Toko.ID = A.CustomerId and Toko.DistributorID = Distributor.id) else 2 end Urutan,A.AlamatLain,A.TypeOP,A.Proyek,A.DepoID," +
                "A.DiambilSendiri,A.Status,case A.CustomerType when 1 then case when (select COUNT(ID)from OP where Prioritas=1 and ID=A.ID)>0 then ' [ AGEN SETIA ] ' else '' end +  (select Keterangan from Toko where ID = A.CustomerId)+  " +
                "case when (select COUNT(KodeVoucher)from AkangVoucher where OPID=A.ID)>0 then ' ->(Akang Voucher='+ rtrim(cast((select COUNT(KodeVoucher) from AkangVoucher where OPID=A.ID) as CHAR))+')<-' else '' end else '-' end Keterangan,A.Keterangan1," +
                "A.Keterangan2,case (select sum(QtyScheduled) from OPDetail where OPID = A.ID) when 0 then (select sum(OpDetail.Berat * OPDetail.Quantity) " +
                "from OPDetail where OPID = A.ID) else (select sum(OpDetail.Berat * (OPDetail.Quantity - OPDetail.QtyScheduled)) from OPDetail where OPID = A.ID) " +
                "end TotalKubikasi,A.KabupatenID,B.NamaKabupaten,C.CityName,A.NoDO,A.NoDO2,A.ApproveDistDate,A.ApproveSCDate,A.DistSubID,A.TypeDistSub,A.CreatedBy," +
                "A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime, case A.CustomerType when 1 then (select PrioritasKirim from Toko where Toko.ID = A.CustomerID) else 0 end PrioritasKirim,DATEDIFF(DAY,A.ApproveSCDate,GETDATE()) as UmurOP " +
                "FROM OP as A,Kabupaten as B,City as C,SalesMan D where A.KabupatenID = B.ID and B.CityID = C.ID and A.Status in (2,3) and A.DepoID = 1 and D.SalesmanType!='HO' and D.ID = A.SalesID " + listCity +
                //" order by PrioritasKirim desc,Urutan,CityName,NamaKabupaten,ID";
                //hasil meeting jumat 30Sept
                //" order by A.Prioritas desc,PrioritasKirim desc,CityName,NamaKabupaten";

                //request leni 28mei2018 add ShipmentDate
                //" order by ShipmentDate,A.Prioritas desc,PrioritasKirim desc,CityName,NamaKabupaten";

            //request leni 28mei2018 add ShipmentDate
            " order by ShipmentDate,A.Prioritas desc,PrioritasKirim desc,CityName,NamaKabupaten";


            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strQuery);

            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOP.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrOP.Add(new OP());

            return arrOP;
        }
        public ArrayList RetrieveApproveStatusByDepoPerCityPerPrioritasKirimAndMarketingOnly(string strCity1, string strCity2, string strCity3, string strCity4)
        {
            string listCity = string.Empty;

            if (strCity1 != string.Empty && strCity2 == string.Empty && strCity3 == string.Empty && strCity4 == string.Empty)
                listCity = " and C.CityName in ('" + strCity1 + "') ";
            else if (strCity1 != string.Empty && strCity2 != string.Empty && strCity3 == string.Empty && strCity4 == string.Empty)
                listCity = " and C.CityName in ('" + strCity1 + "','" + strCity2 + "') ";
            else if (strCity1 != string.Empty && strCity2 != string.Empty && strCity3 != string.Empty && strCity4 == string.Empty)
                listCity = " and C.CityName in ('" + strCity1 + "','" + strCity2 + "','" + strCity3 + "') ";
            else if (strCity1 != string.Empty && strCity2 != string.Empty && strCity3 != string.Empty && strCity4 != string.Empty)
                listCity = " and C.CityName in ('" + strCity1 + "','" + strCity2 + "','" + strCity3 + "','" + strCity4 + "') ";
            else
                listCity = string.Empty;

            string strQuery = "SELECT A.ID,A.OPNo,A.ID2,A.OpNo2,A.ShipmentDate,A.SalesID,A.CustomerType,A.CustomerId,case A.CustomerType when 1 then (select Distributor.Urutan " +
                "from Distributor,Toko where Toko.ID = A.CustomerId and Toko.DistributorID = Distributor.id) else 2 end Urutan,A.AlamatLain,A.TypeOP,A.Proyek,A.DepoID," +
                "A.DiambilSendiri,A.Status,case A.CustomerType when 1 then (select Keterangan from Toko where ID = A.CustomerId) else '-' end Keterangan,A.Keterangan1," +
                "A.Keterangan2,case (select sum(QtyScheduled) from OPDetail where OPID = A.ID) when 0 then (select sum(OpDetail.Berat * OPDetail.Quantity) " +
                "from OPDetail where OPID = A.ID) else (select sum(OpDetail.Berat * (OPDetail.Quantity - OPDetail.QtyScheduled)) from OPDetail where OPID = A.ID) " +
                "end TotalKubikasi,A.KabupatenID,B.NamaKabupaten,C.CityName,A.NoDO,A.NoDO2,A.ApproveDistDate,A.ApproveSCDate,A.DistSubID,A.TypeDistSub,A.CreatedBy," +
                "A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime, case A.CustomerType when 1 then (select PrioritasKirim from Toko where Toko.ID = A.CustomerID) else 0 end PrioritasKirim, 0 as UmurOP " +
                "FROM OP as A,Kabupaten as B,City as C,SalesMan D where A.KabupatenID = B.ID and B.CityID = C.ID and A.Status in (2,3) and A.DepoID = 1 and D.SalesmanType='HO' and D.ID = A.SalesID " + listCity +
                //" order by PrioritasKirim desc,Urutan,CityName,NamaKabupaten,ID");
                " order by A.Prioritas desc,PrioritasKirim desc,CityName,NamaKabupaten";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strQuery);

            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOP.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrOP.Add(new OP());

            return arrOP;
        }
        public int CountOPMarketingOnly()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select COUNT(OPNo) as JmlOP from OP as A,Kabupaten as B,City as C,SalesMan D where A.KabupatenID = B.ID and B.CityID = C.ID and A.Status in (2,3) and A.DepoID = 1 and D.ID = A.SalesID and D.SalesmanType='HO'");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Jumlah1 as JmlOP from ScheduleInfo WHERE KodeInfo='A04'");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["JmlOP"]);
                }
            }

            return 0;
        }
        public int CountOPPrioritas()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select COUNT(OPNo) as JmlOP from OP as A,Kabupaten as B,City as C,SalesMan D where A.KabupatenID = B.ID and B.CityID = C.ID and A.Status in (2,3) and A.DepoID = 1 and D.ID = A.SalesID and A.Prioritas>0 and YEAR(A.CreatedTime)>=2017");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Jumlah1 as JmlOP from ScheduleInfo WHERE KodeInfo='A05'");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["JmlOP"]);
                }
            }

            return 0;
        }

        public ArrayList RetrieveOPByPeriodPosting(string fromDate, string toDate, int TokoId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from OP where CONVERT(varchar,createdtime,112) between '" + fromDate + "' and '" + toDate + "' and CustomerType = 1 and CustomerId = " + TokoId);
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrOP.Add(new OP());

            return arrOP;
        }

        public OP RetrieveByNo(string OPNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from OP where OPNo = '" + OPNo + "'");
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new OP();
        }

        public OP RetrieveByNoCheckStatus(string OPNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from OP where OPNo = '" + OPNo + "' and Status in (2,3)");
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new OP();
        }

        public OP RetrieveByNoWithDepo(string OPNo,int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from OP where OPNo = '" + OPNo + "' and DepoID = " + depoID);
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new OP();
        }

        public OP RetrieveByNoWithDistributor(string OPNo, int distributorID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.PTID,A.ID2,A.OPNo,A.OpNo2,A.ShipmentDate,A.JenisCustomer,A.CustomerType,A.CustomerID,A.SalesID,A.AlamatLain,A.TypeOP,A.Proyek,A.DepoID,A.DiambilSendiri,A.Status,A.Keterangan1,A.Keterangan2,A.TotalKubikasi,A.KabupatenID,A.NoDO,A.NoDO2,A.ApproveDistDate,A.ApproveSCDate,A.DistSubID,A.TypeDistSub,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.AgenID,A.DeptID,A.InfoHBM,A.HBMcityID,A.ShipmentDateType,A.ImsID,A.CaraPembayaran from OP as A,Toko as B where A.CustomerId = B.ID and A.CustomerType = 1 and A.OpNo2 = '" + OPNo + "' and ((A.TypeDistSub=1 and A.DistSubID = "+distributorID+") or (A.TypeDistSub=2 and A.DistSubID in (select ID from SubDistributor where DistributorID="+distributorID+")))  order by A.ID desc");
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new OP();
        }

        public OP RetrieveByNo2(string OPNo2)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from OP where OPNo2 = '" + OPNo2 + "'");
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new OP();
        }

        public OP RetrieveByID(int id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from OP where ID = " + id);
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new OP();
        }
        public OP RetrieveKasus1(int id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select distinct top 1 * from Invoice as a, InvoiceDetail as b,SuratJalan as c,OP where a.ID=B.InvoiceID and a.CustomerType=1 and a.CustomerID in (5,52) and convert(varchar,a.TglPenerimaan,112) between '20111001' and '20111031' and a.Status>-1 and b.RowStatus>-1 and b.SuratJalanID=c.ID and c.Status>-1 and c.OPID=OP.id");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new OP();
        }

        public ArrayList RetrieveByID2(int id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from OP where ID = " + id);
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrOP.Add(new OP());

            return arrOP;
        }
        public OP RetrieveByNoDO(string noDO)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from OP where NoDO = '" + noDO + "'");
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new OP();
        }

        public int getLastId(int id,string typeid)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(max(A.id2),0) as id2 from op as A, Toko as B,Distributor as C where CustomerType = 1 and SUBSTRING(OPNo2,1,2) = '" + typeid + "' and A.CustomerId = B.ID and B.DistributorID = C.ID and C.ID = " + id);
            strError = dataAccess.Error;
            
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["id2"]);
                }
            }

            return 0;
        }
        public DateTime getTanggalServer()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Getdate() as TglServer");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToDateTime(sqlDataReader["TglServer"]);
                }
            }

            return DateTime.MinValue;
        }
        public int cekPromoToko529(int tokoID, int itemPromoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select OP.ID from OP,OPDetail where OP.ID=OPDetail.OPID and OP.Status>-1 and OP.CustomerType=1 and OP.CustomerId="+tokoID+" and OPDetail.ItemID="+itemPromoID);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["ID"]);
                }
            }

            return 0;
        }
        public int cekPromoTokoCiluk(string opno)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Count(OP.ID) as ID from OP,OPDetail where OP.ID=OPDetail.OPID  and OP.CustomerType=1 and "+
            "OP.OPNo='" + opno + "' and OPDetail.GroupID=245");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["ID"]);
                }
            }

            return 0;
        }
        public int cekPromoTokoMBTsby(int tokoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select OP.ID from OP,OPDetail where OP.ID=OPDetail.OPID and OP.Status>-1 and OP.CustomerType=1 and OP.CustomerId=" + tokoID + " and OPDetail.ItemID in (979,978)");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["ID"]);
                }
            }

            return 0;
        }
        public int cekPromoTokoMBTJateng(int tokoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select OP.ID from OP,OPDetail where OP.ID=OPDetail.OPID and OP.Status>-1 and OP.CustomerType=1 and OP.CustomerId=" + tokoID + " and OPDetail.ItemID in (1632,1633)");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["ID"]);
                }
            }

            return 0;
        }

         public int cekKawinKontrak(int tokoID, int Bln, int Thn, int ItemID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select OP.ID from OP,OPDetail where OP.ID=OPDetail.OPID and OP.Status>-1 " +
                "and OP.CustomerType=1 and OP.CustomerId=" + tokoID + " and month(OP.CreatedTime) = " + Bln + " AND YEAR(OP.CreatedTime) = " + Thn + "and OPDetail.ItemID = " + ItemID );
             
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["ID"]);
                }
            }

            return 0;
        }

        public int cekPromoDoubleHeboh(int tokoID, int groupID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select OP.ID from OP,OPDetail where OP.ID=OPDetail.OPID and OP.Status>-1 and OP.CustomerType=1 and OP.CustomerId=" + tokoID + " and OPDetail.GroupID=" + groupID);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["ID"]);
                }
            }

            return 0;
        }
        public int cekPromoLebaran(int tokoID, int groupID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select COUNT(OPNo) as JmlOrder from (select distinct OP.OPNo from OP,OPDetail where OP.ID=OPDetail.OPID and OP.Status>-1 and OP.CustomerType=1 and OP.CustomerId="+tokoID+" and OPDetail.GroupID="+groupID+" ) as aa ");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["JmlOrder"]);
                }
            }

            return 0;
        }
        public int cekKabupatenPromoMawar(int tokoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select COUNT(KabupatenID) as KabupatenID from Toko,Kabupaten where LEFT(TokoCode,1)='Y' and Toko.KabupatenID=Kabupaten.ID and NamaKabupaten in ('banyuwangi','jember','situbondo','bondowoso','lumajang','PROBOLINGGO') and Toko.ID=" + tokoID);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["KabupatenID"]);
                }
            }

            return 0;
        }

        public int cekSeragamKaosJBDTBKJBR(int tokoID, string query)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select OP.ID from OP,OPDetail where OP.ID=OPDetail.OPID and OP.Status>-1 and OP.CustomerType=1 and OP.CustomerId=" + tokoID + "  " + query);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["ID"]);
                }
            }
            return 0;
        }
        public int CekBarangSpanduk(int tokoID, int itemID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(SUM(OP.ID),0) as JmlSpanduk from OP,OPDetail where OP.ID=OPDetail.OPID and OP.Status>-1 and OP.CustomerType=1 and OP.CustomerId="+tokoID+" and ItemID="+itemID);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["JmlSpanduk"]);
                }
            }
            return 0;
        }
        public ArrayList getOpDetailInfo(int suratJalanId)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.Quantity,A.QtyReceived,A.Point from OPDetail as A,SuratJalanDetail As B,ScheduleDetail as C,SuratJalan as D where B.ScheduleDetailID = C.ID and C.DocumentDetailID = A.ID and D.ID = B.SuratJalanID and D.ID = " + suratJalanId);
            strError = dataAccess.Error;

            ArrayList arrList = new ArrayList();
            decimal[] intQty = new decimal[3];

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    intQty = new decimal[3];
                    intQty[0] = Convert.ToDecimal(sqlDataReader["Quantity"]);
                    intQty[1] = Convert.ToDecimal(sqlDataReader["QtyReceived"]);
                    intQty[2] = Convert.ToDecimal(sqlDataReader["Point"]);

                    arrList.Add(intQty);
                }
            }

            return arrList;
        }
        public OP CekSaldoVoucherDist2(int distID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 1 isnull(Nominal,0) as Nominal,isnull(Saldo,0) as Saldo,isnull(NominalPotong,0) as NominalPotong,CodeEncrpt,isnull(Persen,0) as Persen from Distributor_PotonganCode where DistID=" + distID + "  and SubDistID=0 and Confirmation>0 and Saldo>0 order by ConfirmationDate");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 1 isnull(Nominal,0) as Nominal,isnull(Saldo,0) as Saldo,isnull(NominalPotong,0) as NominalPotong,CodeEncrpt,isnull(Persen,0) as Persen from Distributor_PotonganCode where DistID=" + distID + " and Confirmation>0 and Saldo>0 order by ConfirmationDate");
            strError = dataAccess.Error;
            arrOP = new ArrayList();
            OP op = new OP();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    op.Persen = Convert.ToInt16(sqlDataReader["Persen"]);
                    op.NominalVoucher = Convert.ToDecimal(sqlDataReader["Nominal"]);
                    op.SaldoVoucher = Convert.ToDecimal(sqlDataReader["Saldo"]);
                    op.NominalPotong = Convert.ToDecimal(sqlDataReader["NominalPotong"]);
                    if (string.IsNullOrEmpty(sqlDataReader["CodeEncrpt"].ToString()))
                        op.KodeVoucher = string.Empty;
                    else
                        op.KodeVoucher = sqlDataReader["CodeEncrpt"].ToString();

                    return op;
                }
            }

            return new OP();
        }
        public OP CekSaldoVoucherDist2perTokoCode(int distID, string tokoCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 1 isnull(Nominal,0) as Nominal,isnull(Saldo,0) as Saldo,isnull(NominalPotong,0) as NominalPotong,CodeEncrpt,isnull(Persen,0) as Persen from Distributor_PotonganCode where DistID=" + distID + "  and SubDistID=0 and Confirmation>0 and Saldo>0 order by ConfirmationDate");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 1 isnull(Nominal,0) as Nominal,isnull(Saldo,0) as Saldo,isnull(NominalPotong,0) as NominalPotong,CodeEncrpt,isnull(Persen,0) as Persen from Distributor_PotonganCode where DistID=" + distID + " and Confirmation>0 and Saldo>0 and TokoCode='" + tokoCode + "' order by ConfirmationDate");
            strError = dataAccess.Error;
            arrOP = new ArrayList();
            OP op = new OP();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    op.Persen = Convert.ToInt16(sqlDataReader["Persen"]);
                    op.NominalVoucher = Convert.ToDecimal(sqlDataReader["Nominal"]);
                    op.SaldoVoucher = Convert.ToDecimal(sqlDataReader["Saldo"]);
                    op.NominalPotong = Convert.ToDecimal(sqlDataReader["NominalPotong"]);
                    if (string.IsNullOrEmpty(sqlDataReader["CodeEncrpt"].ToString()))
                        op.KodeVoucher = string.Empty;
                    else
                        op.KodeVoucher = sqlDataReader["CodeEncrpt"].ToString();

                    return op;
                }
            }

            return new OP();
        }
        public OP CekSaldoVoucherSubDist2(int distID, int subDistID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 1 isnull(Nominal,0) as Nominal,isnull(Saldo,0) as Saldo,isnull(NominalPotong,0) as NominalPotong,CodeEncrpt from Distributor_PotonganCode where DistID=0 and SubDistID=" + subDistID + "  and Confirmation>0 and Saldo>0 order by ConfirmationDate");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 1 isnull(Nominal,0) as Nominal,isnull(Saldo,0) as Saldo,isnull(NominalPotong,0) as NominalPotong,CodeEncrpt,isnull(Persen,0) as Persen from Distributor_PotonganCode where DistID="+distID+" and SubDistID=" + subDistID + "  and Confirmation>0 and Saldo>0 order by ConfirmationDate");
            strError = dataAccess.Error;
            arrOP = new ArrayList();
            OP op = new OP();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    op.Persen = Convert.ToInt16(sqlDataReader["Persen"]);
                    op.NominalVoucher = Convert.ToDecimal(sqlDataReader["Nominal"]);
                    op.SaldoVoucher = Convert.ToDecimal(sqlDataReader["Saldo"]);
                    op.NominalPotong = Convert.ToDecimal(sqlDataReader["NominalPotong"]);
                    if (string.IsNullOrEmpty(sqlDataReader["CodeEncrpt"].ToString()))
                        op.KodeVoucher = string.Empty;
                    else
                        op.KodeVoucher = sqlDataReader["CodeEncrpt"].ToString();

                    return op;
                }
            }

            return new OP();
        }
        public decimal CekJumlahTotalVoucher2(string kodeVoucher)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(SUM(abs(Price*Quantity)),0) as Potongan from OPDetail,OP,Toko where CodeEncrpt='" + kodeVoucher + "' and OP.ID=OPDetail.OPID and OP.Status>-1 and YEAR(OP.CreatedTime)>=2016 and OP.CustomerType=1 and OP.CustomerId=Toko.ID ");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToDecimal(sqlDataReader["Potongan"]);
                }
            }
            return 0;
        }
        public int CekOPpenggantiRetur(string opno)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select OPretur from OP where OPNo='"+opno+"'");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt16(sqlDataReader["OPretur"]);
                }
            }
            return 0;
        }
        public OP CekSaldoVoucherDist(int distID, string tokocode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 1 isnull(Nominal,0) as Nominal,isnull(Saldo,0) as Saldo,isnull(NominalPotong,0) as NominalPotong,CodeEncrpt from Distributor_PotonganCode where DistID=" + distID + " and TokoCode='"+tokocode+"' and SubDistID=0 and Confirmation>0 and Saldo>0 order by ConfirmationDate");
            strError = dataAccess.Error;
            arrOP = new ArrayList();
            OP op = new OP();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    op.NominalVoucher = Convert.ToDecimal(sqlDataReader["Nominal"]);
                    op.SaldoVoucher = Convert.ToDecimal(sqlDataReader["Saldo"]);
                    op.NominalPotong = Convert.ToDecimal(sqlDataReader["NominalPotong"]);
                    if (string.IsNullOrEmpty(sqlDataReader["CodeEncrpt"].ToString()))
                        op.KodeVoucher = string.Empty;
                    else
                        op.KodeVoucher = sqlDataReader["CodeEncrpt"].ToString();

                    return op;
                }
            }

            return new OP();
        }
        public OP CekSaldoVoucherSubDist(int subDistID, string tokocode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 1 isnull(Nominal,0) as Nominal,isnull(Saldo,0) as Saldo,isnull(NominalPotong,0) as NominalPotong,CodeEncrpt from Distributor_PotonganCode where DistID=0 and SubDistID=" + subDistID + " and TokoCode='" + tokocode + "' and Confirmation>0 and Saldo>0 order by ConfirmationDate");
            strError = dataAccess.Error;
            arrOP = new ArrayList();
            OP op = new OP();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    op.NominalVoucher = Convert.ToDecimal(sqlDataReader["Nominal"]);
                    op.SaldoVoucher = Convert.ToDecimal(sqlDataReader["Saldo"]);
                    op.NominalPotong = Convert.ToDecimal(sqlDataReader["NominalPotong"]);
                    if (string.IsNullOrEmpty(sqlDataReader["CodeEncrpt"].ToString()))
                        op.KodeVoucher = string.Empty;
                    else
                        op.KodeVoucher = sqlDataReader["CodeEncrpt"].ToString();

                    return op;
                }
            }

            return new OP();
        }
        public decimal CekJumlahTotalVoucher(string kodeVoucher, string tokocode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(SUM(abs(Price)),0) as Potongan from OPDetail,OP where CodeEncrpt='"+kodeVoucher+"' and OP.ID=OPDetail.OPID and OP.Status>-1");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(SUM(abs(Price)),0) as Potongan from OPDetail,OP,Toko where CodeEncrpt='"+kodeVoucher+"' and OP.ID=OPDetail.OPID and OP.Status>-1 and YEAR(OP.CreatedTime)>=2016 and OP.CustomerType=1 and OP.CustomerId=Toko.ID and Toko.TokoCode='"+tokocode+"'");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToDecimal(sqlDataReader["Potongan"]);
                }
            }
            return 0;
        }
        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.PTID,A.ID2,A.ItemCode,A.JenisCustomer,A.Description,A.GroupID,B.GroupCode,B.GroupDescription,A.ShortKey,A.GradeID,C.GradeCode,A.SisiID,D.SisiDescription,A.Tebal,A.Panjang,A.Lebar,A.UOMID,E.UOMCode,A.Berat,A.Ket1,A.Ket2,A.Utuh,A.Paket,A.RowStatus,A.ApproveDistDate,A.ApproveSCDate,A.DistSubID,A.TypeDistSub,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.DeptID,A.InfoHBM,A.HBMcityID,A.ShipmentDateType,A.ImsID,A.CaraPembayaran from OP as A,Groups as B, Grades as C,Sisi as D,UOM as E where A.GroupID = B.ID and A.GradeID = C.ID and A.SisiID = D.ID and A.UOMID = E.ID and A.RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrOP.Add(new OP());

            return arrOP;
        }

        public OP RetrieveByNoTipeCust(string OPNo3, int cType)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select DistSubID from OP where CustomerType = " + cType + " and OPNo = '" + OPNo3 + "'");
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject3(sqlDataReader);
                }
            }

            return new OP();
        }

        public OP RetrieveByNoTipeCust2(string OPNo3, int cType)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select CustomerId from OP where CustomerType = " + cType + " and OPNo = '" + OPNo3 + "'");
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject4(sqlDataReader);
                }
            }

            return new OP();
        }

        public OP RetrieveByNoNilai(string OPNo4)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.OPNo,SUM(B.TotalPrice) as Harga from OP as A,OPDetail as B where A.ID = B.OPID and A.OPNo = '" + OPNo4 + "' group by A.ID,A.OPNo");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.OPNo,SUM(B.TotalPrice) as Harga from OP as A,OPDetail as B where A.ID = B.OPID and "+
            "A.OPNo = '" + OPNo4 + "' and (select Pajak from Items where ID in(B.ItemID))=0 group by A.ID,A.OPNo");
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject5(sqlDataReader);
                }
            }

            return new OP();
        }

        public OP RetrieveAgenID(string taxNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.AgenID from OP as A,SuratJalan as B,Invoice as C,InvoiceDetail as D where C.ID = D.InvoiceID and B.ID = D.SuratJalanID and A.ID = B.OPID and C.TaxNo = '" + taxNo + "'");
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject6(sqlDataReader);
                }
            }

            return new OP();
        }
        //Promo LSP214 ke-2
        public OP RetrievePromoLSP214ke1(int tokoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Toko.ID,(select isnull(SUM(OPDetail.Quantity),0) from OP,OPDetail,Items "+
            "where OP.ID=OPDetail.OPID and OP.Status>-1 and OPDetail.ItemID=Items.ID and Items.Ket1='LSP214' and CONVERT(char(8),OP.CreatedTime, 112) <= '20141231' "+
            "and OP.CustomerType=1 and OP.CustomerId=Toko.ID) as QtyOrder, (select isnull(SUM(OPDetail.Quantity),0) from OP,OPDetail,Items "+
            "where OP.ID=OPDetail.OPID and OP.Status>-1 and OPDetail.ItemID=Items.ID and Items.Ket1='LSP214' and CONVERT(char(8),OP.CreatedTime, 112) > '20141231' "+
            "and OP.CustomerType=1 and OP.CustomerId=Toko.ID) as QtyNew from Toko where ID = "+tokoID+" and Toko.RowStatus>-1");
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectLSP214(sqlDataReader);
                }
            }

            return new OP();
        }
        //Promo LSP214 ke-2
        //Promo Paket Hemat Jatim 2015
        public OP cekPromoPaheJatim(int tokoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Count(OP.ID) as Jumlah from OP,OPDetail,Items "+
            "where OP.ID=OPDetail.OPID and OP.Status>-1 and OPDetail.ItemID=Items.ID "+
			"and (OPDetail.ItemID in(1806,1807,1821,1822,1823,1824) or OPDetail.ItemID in(1808,1809,1810, "+
            "1811,1812,1813,1814,1815,1816)) "+
            "and OP.CustomerType=1 and OP.CustomerId=" + tokoID);
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectPAHE2015(sqlDataReader);
                }
            }

            return new OP();
        }
        //Promo Paket Hemat Jatim 2015

        //Promo Paket Hemat Jateng 2015
        public OP cekPromoPaheJateng(int tokoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Count(OP.ID) as Jumlah from OP,OPDetail,Items " +
            "where OP.ID=OPDetail.OPID and OP.Status>-1 and OPDetail.ItemID=Items.ID " +
            "and (OPDetail.ItemID in (1830,1831,1832,1833,1834) or OPDetail.ItemID in (1835,1836,1837, " +
            "1838,1839,1840,1841,1842)) " +
            "and OP.CustomerType=1 and OP.CustomerId=" + tokoID);
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectPAHE2015(sqlDataReader);
                }
            }

            return new OP();
        }
        //Promo Paket Hemat Jateng 2015
        //Promo Paket MP Jateng 2015
        public OP cekPromoMPJateng(int tokoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Count(OP.ID) as Jumlah from OP,OPDetail,Items " +
            "where OP.ID=OPDetail.OPID and OP.Status>-1 and OPDetail.ItemID=Items.ID " +
            "and (OPDetail.ItemID in (1962,1963,1964,1965,1966,1967,1968,1969,1970,1971)) " +
            "and OP.CustomerType=1 and OP.CustomerId=" + tokoID);
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectPAHE2015(sqlDataReader);
                }
            }

            return new OP();
        }
        //Promo Paket MP Jateng 2015

        ////// Promo Kejutan GRC News //////
        public int CekPermitPromoGRCNews(string strTokoCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Count(B.ID) as Jumlah from Toko as A, Toko_KejutanGRCNews as B where A.ID=B.TokoID and A.RowStatus = 0 and A.TokoCode = '" + strTokoCode + "' and B.Permit=1");
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
        public int CekPermitPromoGRCNewsJumlahAmbil(string strTokoCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Count(B.ID) as Jumlah from Toko as A, Toko_KejutanGRCNews as B where " +
            "A.ID=B.TokoID and A.RowStatus = 0 and A.TokoCode = '" + strTokoCode + "' and B.PakaiKejutanGRCNews=0");
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
        ////// Promo Kejutan GRC News //////

        ////// Promo MBTB //////
        public int CekPermitPromoMBTB(string strTokoCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select Count(B.ID) as Jumlah from Toko as A, Toko_MBTB as B where A.ID=B.TokoID and A.RowStatus = 0 and A.TokoCode = '" + strTokoCode + "' and B.Permit=1");
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
        ////// Promo MBTB //////

        /// cek promo duet grc & pink
        public int CekOutBrgGRC4mm(int tokoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select sum(Quantity-QtySJ) as QtyOut from (select b.ItemID,b.Quantity,b.QtyScheduled,b.QtyReceived,f.Qty as QtySJ,Description,GroupCategory "+
            //    "from OP as a, OPDetail as b, Schedule as c, ScheduleDetail as d, SuratJalan as e, SuratJalanDetail as f,Items where a.ID=b.OPID and a.Status>-1 and c.Status>-1 and d.Status>-1 and a.id=d.DocumentID and b.ID=d.DocumentDetailID and c.ID=e.ScheduleID and a.ID=e.OPID and e.Status>=0 and e.ID=f.SuratJalanID and d.ID=f.ScheduleDetailID and a.CustomerType=1 and a.CustomerId="+tokoID+" and b.GroupID=170 and b.ItemID=Items.ID and GroupCategory in ('FFT1220','FFT1200') ) as aa group by ItemID");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select ISNULL(SUM(QtyOP-QtySJ),0) as QtyOut from ( "+
            "select MAX(A.OPNO) as OPNO,MAX(b.Quantity) as QtyOP,SUM(f.Qty) as QtySJ  "+
            "from OP as a, OPDetail as b, Schedule as c, ScheduleDetail as d, SuratJalan as e, SuratJalanDetail as f,Items "+
            "where a.ID=b.OPID and a.Status>-1 and c.Status>-1 and d.Status>-1 "+
            "and a.id=d.DocumentID and b.ID=d.DocumentDetailID and c.ID=e.ScheduleID and a.ID=e.OPID and e.Status>=0 and "+
            "e.ID=f.SuratJalanID and d.ID=f.ScheduleDetailID and a.CustomerType=1 and a.CustomerId=" + tokoID + " and b.GroupID=170 and " +
            "b.ItemID=Items.ID and GroupCategory in ('FFT1220','FFT1200') group by A.OPNO "+
            ") as aa");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["QtyOut"]);
                }
            }
            return 0;
        }
        /// cek promo duet grc & pink

        ////// Promo Seragam Toko 2016 //////
        public OP CekPromoSeragamToko(string strTokoCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from SeragamToko where KodeToko='" + strTokoCode + "' and JmlSeragam > 0 and ConfirmAddToOP = 0 and ConfirmDateTime is null and RowStatus = 0");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectPromoSeragamToko(sqlDataReader);
                }
            }

            return new OP();
        }
        public int CekSeragamTokoSudahDiOPkan(int IdCust, int ItemID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select COUNT(B.ID) as Jumlah from OP as A, OPDetail as B where A.ID=B.OPID and A.Status>=0 and B.ItemID='" + ItemID + "' and A.CustomerId=" + IdCust);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                { return Convert.ToInt32(sqlDataReader["Jumlah"]); }
            }
            return 0;
        }
        ////// Promo Seragam Toko 2016 //////
        public OP CekPromoSampleAcyrlicDecoplank(int intDepoID, int intItemID, string strTokoCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select b.* from PromoToko as a, PromoTokoDetail as b "+
                "where a.ID=b.PromoTokoID and a.Status>-1 and a.DepoID=" + intDepoID + " and b.ItemID="+intItemID+" and b.TokoCode='"+strTokoCode+"' and b.Saldo>0 and b.ConfirmAddToOP = 0 and b.ConfirmDate is null and RowStatus = 0");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectPromoToko(sqlDataReader);
                }
            }

            return new OP();
        }

        // Tambahan 11 Maret 2016
        public String getTokoCode(string OPNo)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select case OP.CustomerType when 1 then (select TokoCode from Toko where ID = OP.CustomerId) " +
                                                                            "when 2 then (select CustomerCode from Customer where ID = OP.CustomerId) end KodeToko from OP where OPNo = '" + OPNo + "'");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return (sqlDataReader["KodeToko"].ToString());
                }
            }

            return string.Empty;
        }
        public int CekDateServer(int idOP)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(DATEDIFF(DD,CreatedTime,GETDATE()),0) as LamaOP  from OP where ID=" + idOP);
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select (DATEDIFF(dd,op.CreatedTime, getdate()))    "+
                "- (DATEDIFF(ww,op.CreatedTime, DATEADD(dd,-1, getdate())) * 2) "+
                "- (CASE WHEN DATENAME(dw, op.CreatedTime) = 'Sunday' THEN 1 else 0 end) "+
                "- (CASE WHEN DATENAME(dw, getdate()) = 'Sunday' THEN 1 else 0 end) "+
                "- (CASE WHEN DATENAME(dw, op.CreatedTime) = 'Saturday' THEN 1 else 0 end) "+
                "- (CASE WHEN DATENAME(dw, getdate()) = 'Saturday' THEN 1 else 0 end) as LamaOP " +
                "from OP where ID=" + idOP);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                { return Convert.ToInt32(sqlDataReader["LamaOP"]); }
            }
            return 0;
        }
        public ArrayList RetrieveOPTunai(int custType, int typeDistSub, int distID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from (select A.ID,A.OPNo +' - '+ TokoName as OPno,A.CreatedTime,DATEDIFF(DAY,A.CreatedTime,GETDATE()) as JmlHari,A.ApproveDistDate,A.ApproveSCDate, (select sum(TotalPrice ) + 6000 from OPDetail where OPID=a.ID)  as Harga, "+
            "(select isnull(sum(Debet),0) as Bayar from BankIn as a, BankInDetail as b where a.ID=b.BankInID and a.PTID=100 and b.OPID=A.ID) as UangMasuk "+
            "from OP as A,Toko as B where A.CustomerId = B.ID and A.CustomerType = "+custType+" and A.TypeDistSub="+typeDistSub+" and B.DistributorID = "+distID+" and A.CaraPembayaran=1 and A.Status>=0 and left( CONVERT(varchar,ApproveSCDate,112),6)>='201712' "+
            ") as z where Harga-UangMasuk>0 order by ID");
            strError = dataAccess.Error;
            arrOP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrOP.Add(GenerateObjectOpTunai(sqlDataReader));
                }
            }
            else
                arrOP.Add(new OP());

            return arrOP;
        }

        public OP GenerateObjectPromoSeragamToko(SqlDataReader sqlDataReader)
        {
            objOP = new OP();
            objOP.TokoKode = Convert.ToString(sqlDataReader["KodeToko"]);
            objOP.Jumlah = Convert.ToInt32(sqlDataReader["JmlSeragam"]);
            return objOP;
        }
        public OP GenerateObjectPromoToko(SqlDataReader sqlDataReader)
        {
            objOP = new OP();
            objOP.TokoKode = Convert.ToString(sqlDataReader["TokoCode"]);
            objOP.Jumlah = Convert.ToInt32(sqlDataReader["Saldo"]);
            return objOP;
        }

        public OP GenerateObjectPAHE2015(SqlDataReader sqlDataReader)
        {
            objOP = new OP();
            objOP.Jumlah = Convert.ToInt32(sqlDataReader["Jumlah"]);
            return objOP;
        }

        public OP GenerateObjectLSP214(SqlDataReader sqlDataReader)
        {
            objOP = new OP();
            objOP.CustomerID = Convert.ToInt32(sqlDataReader["ID"]);
            objOP.QtyOrder = Convert.ToInt32(sqlDataReader["QtyOrder"]);
            objOP.QtyNew = Convert.ToInt32(sqlDataReader["QtyNew"]);

            return objOP;
        }
        public OP GenerateObject6(SqlDataReader sqlDataReader)
        {
            objOP.AgenID = Convert.ToInt32(sqlDataReader["AgenID"]);
            return objOP;
        }
        public OP GenerateObject5(SqlDataReader sqlDataReader)
        {
            objOP = new OP();
            objOP.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objOP.OPNo = sqlDataReader["OPNo"].ToString();
            objOP.Harga = Convert.ToDecimal(sqlDataReader["Harga"]);


            return objOP;
        }

        public OP GenerateObject4(SqlDataReader sqlDataReader)
        {
            objOP.CustomerID = Convert.ToInt32(sqlDataReader["CustomerID"]);
            return objOP;
        }

        public OP GenerateObject3(SqlDataReader sqlDataReader)
        {
            objOP = new OP();
            objOP.DistSubID = Convert.ToInt32(sqlDataReader["DistSubID"]);

            return objOP;
        }

        public OP GenerateObject(SqlDataReader sqlDataReader)
        {
            objOP = new OP();
            objOP.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objOP.OPNo = sqlDataReader["OPNo"].ToString();
            objOP.ID2 = Convert.ToInt32(sqlDataReader["ID2"]);
            objOP.OPNo2 = sqlDataReader["OPNo2"].ToString();
            objOP.ShipmentDate = Convert.ToDateTime(sqlDataReader["ShipmentDate"]);
            objOP.CustomerType = Convert.ToInt32(sqlDataReader["CustomerType"]);
            objOP.CustomerID = Convert.ToInt32(sqlDataReader["CustomerID"]);
            //objOP.CustomerCode = sqlDataReader["CustomerCode"].ToString();
            //objOP.CustomerName = sqlDataReader["CustomerName"].ToString();
            //objOP.Address = sqlDataReader["Address"].ToString();            
            objOP.SalesID = Convert.ToInt32(sqlDataReader["SalesID"]);
            objOP.AlamatLain = sqlDataReader["AlamatLain"].ToString();
            objOP.TypeOP = Convert.ToInt32(sqlDataReader["TypeOP"]);
            objOP.Proyek = Convert.ToInt32(sqlDataReader["Proyek"]);
            objOP.DepoID = Convert.ToInt32(sqlDataReader["DepoID"]);
            objOP.DiambilSendiri = Convert.ToInt32(sqlDataReader["DiambilSendiri"]);
            objOP.Status = Convert.ToInt32(sqlDataReader["Status"]);   
            objOP.Keterangan1 = sqlDataReader["Keterangan1"].ToString();
            objOP.Keterangan2 = sqlDataReader["Keterangan2"].ToString();
            objOP.TotalKubikasi = Convert.ToDecimal(sqlDataReader["TotalKubikasi"]);
            objOP.KabupatenID = Convert.ToInt32(sqlDataReader["KabupatenID"]);
            objOP.NoDO = sqlDataReader["NoDO"].ToString();
            objOP.NoDO2 = sqlDataReader["NoDO2"].ToString();
            if(string.IsNullOrEmpty(sqlDataReader["ApproveDistDate"].ToString()))
                objOP.ApproveDistDate = DateTime.MinValue;
            else
                objOP.ApproveDistDate = Convert.ToDateTime(sqlDataReader["ApproveDistDate"]);

            if (string.IsNullOrEmpty(sqlDataReader["ApproveSCDate"].ToString()))
                objOP.ApproveSCDate = DateTime.MinValue;
            else
                objOP.ApproveSCDate = Convert.ToDateTime(sqlDataReader["ApproveSCDate"]);

            if (string.IsNullOrEmpty(sqlDataReader["DistSubID"].ToString()))
                objOP.DistSubID = 0;
            else
                objOP.DistSubID = Convert.ToInt32(sqlDataReader["DistSubID"]);

            if (string.IsNullOrEmpty(sqlDataReader["TypeDistSub"].ToString()))
                objOP.TypeDistSub = 0;
            else
                objOP.TypeDistSub = Convert.ToInt32(sqlDataReader["TypeDistSub"]);

            objOP.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objOP.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objOP.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objOP.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);

            objOP.AgenID = Convert.ToInt16(sqlDataReader["AgenID"]);
            if (string.IsNullOrEmpty(sqlDataReader["DeptID"].ToString()))
                objOP.DeptID = 0;
            else
                objOP.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);

            if (string.IsNullOrEmpty(sqlDataReader["InfoHBM"].ToString()))
                objOP.InfoHBM = 0;
            else
                objOP.InfoHBM = Convert.ToInt32(sqlDataReader["InfoHBM"]);
            if (string.IsNullOrEmpty(sqlDataReader["HbmCityID"].ToString()))
                objOP.HbmCityID = 0;
            else
                objOP.HbmCityID = Convert.ToInt32(sqlDataReader["HbmCityID"]);

            objOP.PTID = Convert.ToInt32(sqlDataReader["PTID"]);
            objOP.ShipmentDateType = Convert.ToInt32(sqlDataReader["ShipmentDateType"]);
            objOP.ImsID = Convert.ToInt32(sqlDataReader["imsID"]);
            objOP.CaraPembayaran = Convert.ToInt32(sqlDataReader["CaraPembayaran"]);

            return objOP;

        }
        public OP GenerateObjectLastApp(SqlDataReader sqlDataReader)
        {
            objOP = new OP();
            objOP.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objOP.OPNo = sqlDataReader["OPNo"].ToString();
            objOP.ID2 = Convert.ToInt32(sqlDataReader["ID2"]);
            objOP.OPNo2 = sqlDataReader["OPNo2"].ToString();
            objOP.ShipmentDate = Convert.ToDateTime(sqlDataReader["ShipmentDate"]);
            objOP.CustomerType = Convert.ToInt32(sqlDataReader["CustomerType"]);
            objOP.CustomerID = Convert.ToInt32(sqlDataReader["CustomerID"]);
            //objOP.CustomerCode = sqlDataReader["CustomerCode"].ToString();
            //objOP.CustomerName = sqlDataReader["CustomerName"].ToString();
            //objOP.Address = sqlDataReader["Address"].ToString();            
            objOP.SalesID = Convert.ToInt32(sqlDataReader["SalesID"]);
            objOP.AlamatLain = sqlDataReader["AlamatLain"].ToString();
            objOP.TypeOP = Convert.ToInt32(sqlDataReader["TypeOP"]);
            objOP.Proyek = Convert.ToInt32(sqlDataReader["Proyek"]);
            objOP.DepoID = Convert.ToInt32(sqlDataReader["DepoID"]);
            objOP.DiambilSendiri = Convert.ToInt32(sqlDataReader["DiambilSendiri"]);
            objOP.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objOP.Keterangan1 = sqlDataReader["Keterangan1"].ToString();
            objOP.Keterangan2 = sqlDataReader["Keterangan2"].ToString();
            objOP.TotalKubikasi = Convert.ToDecimal(sqlDataReader["TotalKubikasi"]);
            objOP.KabupatenID = Convert.ToInt32(sqlDataReader["KabupatenID"]);
            objOP.NoDO = sqlDataReader["NoDO"].ToString();
            objOP.NoDO2 = sqlDataReader["NoDO2"].ToString();
            if (string.IsNullOrEmpty(sqlDataReader["ApproveDistDate"].ToString()))
                objOP.ApproveDistDate = DateTime.MinValue;
            else
                objOP.ApproveDistDate = Convert.ToDateTime(sqlDataReader["ApproveDistDate"]);

            if (string.IsNullOrEmpty(sqlDataReader["ApproveSCDate"].ToString()))
                objOP.ApproveSCDate = DateTime.MinValue;
            else
                objOP.ApproveSCDate = Convert.ToDateTime(sqlDataReader["ApproveSCDate"]);

            if (string.IsNullOrEmpty(sqlDataReader["DistSubID"].ToString()))
                objOP.DistSubID = 0;
            else
                objOP.DistSubID = Convert.ToInt32(sqlDataReader["DistSubID"]);

            if (string.IsNullOrEmpty(sqlDataReader["TypeDistSub"].ToString()))
                objOP.TypeDistSub = 0;
            else
                objOP.TypeDistSub = Convert.ToInt32(sqlDataReader["TypeDistSub"]);


            objOP.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objOP.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objOP.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objOP.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);           
            objOP.AgenID = Convert.ToInt16(sqlDataReader["AgenID"]);
            objOP.UnApprove = sqlDataReader["UnApprove"].ToString();

            return objOP;

        }

        public OP GenerateObject2(SqlDataReader sqlDataReader)
        {
            objOP = new OP();
            objOP.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objOP.OPNo = sqlDataReader["OPNo"].ToString();
            objOP.ID2 = Convert.ToInt32(sqlDataReader["ID2"]);
            objOP.OPNo2 = sqlDataReader["OPNo2"].ToString();
            objOP.ShipmentDate = Convert.ToDateTime(sqlDataReader["ShipmentDate"]);
            objOP.CustomerType = Convert.ToInt32(sqlDataReader["CustomerType"]);
            objOP.CustomerID = Convert.ToInt32(sqlDataReader["CustomerID"]);
            //objOP.CustomerCode = sqlDataReader["CustomerCode"].ToString();
            //objOP.CustomerName = sqlDataReader["CustomerName"].ToString();
            //objOP.Address = sqlDataReader["Address"].ToString();            
            objOP.SalesID = Convert.ToInt32(sqlDataReader["SalesID"]);
            objOP.AlamatLain = sqlDataReader["AlamatLain"].ToString();
            objOP.TypeOP = Convert.ToInt32(sqlDataReader["TypeOP"]);
            objOP.Proyek = Convert.ToInt32(sqlDataReader["Proyek"]);
            objOP.DepoID = Convert.ToInt32(sqlDataReader["DepoID"]);
            objOP.DiambilSendiri = Convert.ToInt32(sqlDataReader["DiambilSendiri"]);
            objOP.Status = Convert.ToInt32(sqlDataReader["Status"]);
            if (string.IsNullOrEmpty(sqlDataReader["Keterangan"].ToString()))
                objOP.Keterangan = string.Empty;
            else
                objOP.Keterangan = sqlDataReader["Keterangan"].ToString();
            objOP.Keterangan1 = sqlDataReader["Keterangan1"].ToString();
            objOP.Keterangan2 = sqlDataReader["Keterangan2"].ToString();
            objOP.TotalKubikasi = Convert.ToDecimal(sqlDataReader["TotalKubikasi"]);
            objOP.KabupatenID = Convert.ToInt32(sqlDataReader["KabupatenID"]);
            objOP.NoDO = sqlDataReader["NoDO"].ToString();
            objOP.NoDO2 = sqlDataReader["NoDO2"].ToString();
            if (string.IsNullOrEmpty(sqlDataReader["ApproveDistDate"].ToString()))
                objOP.ApproveDistDate = DateTime.MinValue;
            else
                objOP.ApproveDistDate = Convert.ToDateTime(sqlDataReader["ApproveDistDate"]);

            if (string.IsNullOrEmpty(sqlDataReader["ApproveSCDate"].ToString()))
                objOP.ApproveSCDate = DateTime.MinValue;
            else
                objOP.ApproveSCDate = Convert.ToDateTime(sqlDataReader["ApproveSCDate"]);

            if (string.IsNullOrEmpty(sqlDataReader["DistSubID"].ToString()))
                objOP.DistSubID = 0;
            else
                objOP.DistSubID = Convert.ToInt32(sqlDataReader["DistSubID"]);

            if (string.IsNullOrEmpty(sqlDataReader["TypeDistSub"].ToString()))
                objOP.TypeDistSub = 0;
            else
                objOP.TypeDistSub = Convert.ToInt32(sqlDataReader["TypeDistSub"]);

            objOP.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objOP.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objOP.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objOP.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);

            objOP.UmurOP = Convert.ToInt32(sqlDataReader["UmurOP"]);

            //objOP.AgenID = Convert.ToInt16(sqlDataReader["AgenID"]);

            return objOP;

        }

        public OP GenerateObjectSJ(SqlDataReader sqlDataReader)
        {
            objOP = new OP();
            objOP.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objOP.OPNo = sqlDataReader["OPNo"].ToString();
            if (string.IsNullOrEmpty(sqlDataReader["ApproveSCDate"].ToString()))
                objOP.ApproveSCDate = DateTime.MinValue;
            else
                objOP.ApproveSCDate = Convert.ToDateTime(sqlDataReader["ApproveSCDate"]);

            objOP.SjID = Convert.ToInt32(sqlDataReader["SuratJalanID"]);
            objOP.SjNO = sqlDataReader["SuratJalanNo"].ToString();

            return objOP;
        }
        public OP GenerateObjectOpTunai(SqlDataReader sqlDataReader)
        {
            objOP = new OP();
            objOP.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objOP.OPNo = sqlDataReader["OPNo"].ToString();
            if (string.IsNullOrEmpty(sqlDataReader["CreatedTime"].ToString()))
                objOP.CreatedTime = DateTime.MinValue;
            else
                objOP.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            if (string.IsNullOrEmpty(sqlDataReader["ApproveDistDate"].ToString()))
                objOP.ApproveDistDate = DateTime.MinValue;
            else
                objOP.ApproveDistDate = Convert.ToDateTime(sqlDataReader["ApproveDistDate"]);
            if (string.IsNullOrEmpty(sqlDataReader["ApproveSCDate"].ToString()))
                objOP.ApproveSCDate = DateTime.MinValue;
            else
                objOP.ApproveSCDate = Convert.ToDateTime(sqlDataReader["ApproveSCDate"]);

            objOP.Harga = Convert.ToDecimal(sqlDataReader["Harga"]);
            objOP.UmurOP = Convert.ToInt32(sqlDataReader["JmlHari"]);
            objOP.NominalVoucher = Convert.ToDecimal(sqlDataReader["UangMasuk"]);


            return objOP;
        }



    }
}

