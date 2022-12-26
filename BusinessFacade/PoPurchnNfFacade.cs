using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using System.Web;
using BusinessFacade;
using DataAccessLayer;
using Dapper;

namespace BusinessFacade
{
    public class PoPurchnNfFacade : AbstractTransactionFacade
    {
        private PoPurchnNf.ParamHead objPOPurchn = new PoPurchnNf.ParamHead();
        private List<SqlParameter> sqlListParam;
        public PoPurchnNfFacade(object objDomain)
            : base(objDomain)
        {
            objPOPurchn = (PoPurchnNf.ParamHead)objDomain;
        }
        public int UpdateSubCompanyID(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objPOPurchn.ID));
                sqlListParam.Add(new SqlParameter("@SubCompanyID", objPOPurchn.SubCompanyID));
                int intResult = transManager.DoTransaction(sqlListParam, "spPoPurchnSubCom_Update");
                strError = transManager.Error;
                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public static int UpdateUangMuka(object objDomain)
        {
            int result = 0;
            DataAccess da = new DataAccess(Global.ConnectionString());
            List<SqlParameter> sqlListParam = new List<SqlParameter>();
            PoPurchnNf.ParamHead objPOPurchn = (PoPurchnNf.ParamHead)objDomain;
            SqlDataReader sdr = da.RetrieveDataByString("Update POPurchn set UangMuka=" + objPOPurchn.UangMuka + " where ID=" + objPOPurchn.ID);
            result = sdr.RecordsAffected;
            return result;
        }
        public override int Delete(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@NoPO", objPOPurchn.NoPO));
                sqlListParam.Add(new SqlParameter("@POPurchnDate", objPOPurchn.CreatedTime));
                sqlListParam.Add(new SqlParameter("@SupplierID", objPOPurchn.SupplierID));
                sqlListParam.Add(new SqlParameter("@Termin", objPOPurchn.Termin));
                sqlListParam.Add(new SqlParameter("@PPN", objPOPurchn.PPN));
                sqlListParam.Add(new SqlParameter("@Delivery", objPOPurchn.Delivery));
                sqlListParam.Add(new SqlParameter("@Crc", objPOPurchn.Crc));
                sqlListParam.Add(new SqlParameter("@Keterangan", objPOPurchn.Keterangan));
                sqlListParam.Add(new SqlParameter("@Terbilang", objPOPurchn.Terbilang));
                sqlListParam.Add(new SqlParameter("@Disc", objPOPurchn.Disc));
                sqlListParam.Add(new SqlParameter("@PPH", objPOPurchn.PPH));
                sqlListParam.Add(new SqlParameter("@NilaiKurs", objPOPurchn.NilaiKurs));
                sqlListParam.Add(new SqlParameter("@Cetak", objPOPurchn.Cetak));
                sqlListParam.Add(new SqlParameter("@CountPrt", objPOPurchn.CountPrt));
                sqlListParam.Add(new SqlParameter("@Status", objPOPurchn.Status));
                sqlListParam.Add(new SqlParameter("@Approval", objPOPurchn.Approval));
                sqlListParam.Add(new SqlParameter("@ApproveDate1", objPOPurchn.ApproveDate1));
                sqlListParam.Add(new SqlParameter("@ApproveDate2", objPOPurchn.ApproveDate2));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objPOPurchn.CreatedBy));
                sqlListParam.Add(new SqlParameter("@AlasanBatal", objPOPurchn.AlasanBatal));
                sqlListParam.Add(new SqlParameter("@AlasanClose", objPOPurchn.AlasanClose));
                sqlListParam.Add(new SqlParameter("@PaymentType", objPOPurchn.PaymentType));
                sqlListParam.Add(new SqlParameter("@ItemFrom", objPOPurchn.ItemFrom));
                sqlListParam.Add(new SqlParameter("@Indent", objPOPurchn.Indent));
                sqlListParam.Add(new SqlParameter("@Ongkos", objPOPurchn.Ongkos));
                sqlListParam.Add(new SqlParameter("@UangMuka", objPOPurchn.UangMuka));
                sqlListParam.Add(new SqlParameter("@CoaID", objPOPurchn.CoaID));
                sqlListParam.Add(new SqlParameter("@Remark", objPOPurchn.Remark));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertPOPurchn");
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
            throw new NotImplementedException();
        }

        public override int Update(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public static List<PoPurchnNf.ParamDtlData> DtlData(int id)
        {
            List<PoPurchnNf.ParamDtlData> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT TOP 300 "+
"pd.ID, s.NoSPP NoSpp, FORMAT(pd.Qty, 'N0') Quantity, u.UOMDesc Satuan, " +
  "FORMAT(pd.Price, 'N0') Harga, " +
"case sp.ItemTypeID " +
"when 1 then(select ItemCode from Inventory where Inventory.ID = sp.ItemID) " +
"when 2 then(select ItemCode from Asset where Asset.ID = sp.ItemID) " +
"when 3 then(select ItemCode from Biaya where Biaya.ID = sp.ItemID) else '' end ItemCode, " +
"case sp.ItemTypeID " +
"when 1 then(select ItemName from Inventory where Inventory.ID = sp.ItemID) " +
"when 2 then(select ItemName from Asset where Asset.ID = sp.ItemID) " +
"when 3 then(select ItemName from Biaya where Biaya.ID = sp.ItemID) + ' - ' + sp.Keterangan else '' end ItemName, " +
"pd.DlvDate " +
"FROM POPurchnDetail pd, POPurchn p, SPPDetail sp, SPP s, UOM u " +
"WHERE pd.POID = p.ID AND pd.SppDetailID = sp.ID AND sp.SPPID = s.ID AND pd.UOMID = u.ID " +
"AND pd.Status > -1 AND p.status > -1 AND sp.Status > -1 AND s.status > -1 " +
"AND p.ID = "+id;
                    AllData = connection.Query<PoPurchnNf.ParamDtlData>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PoPurchnNf.ParamHeadData> HeadData(int id)
        {
            List<PoPurchnNf.ParamHeadData> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT p.Ongkos OngkosKirim," +
"(SELECT Lambang FROM MataUang WHERE ID = p.crc) MataUang, "+
"p.nopo NoPo, p.Termin [TermOfPay], p.PPH, p.PPN, p.Disc Discount, p.delivery TermOfDelivery, " +
"CASE p.paymenttype WHEN 1 THEN 'Kredit' ELSE 'Cash'END Bayar, " +
"s.SupplierName, s.UP UPSupplier, s.Telepon TelpSupplier, s.Fax FaxSupplier, p.POPurchnDate TanggalPo " +
"FROM POPurchn p, SuppPurch s " +
"WHERE p.SupplierID = s.ID AND p.Status > -1 AND s.RowStatus > -1 " +
"AND p.ID = " + id;
                    AllData = connection.Query<PoPurchnNf.ParamHeadData>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PoPurchnNf.ParamDataPo> DataPo(string NoPo, string NoSpp, string CreatedBy)
        {
            List<PoPurchnNf.ParamDataPo> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string Search = "";
                if (NoPo != "") { Search += " and p.NoPo like '%" + NoPo + "%' "; }
                if (NoSpp != "") { Search += " and s.NoSPP like '%" + NoSpp + "%' "; }
                if (CreatedBy != "") { Search += " and s.CreatedBy='" + CreatedBy + "' "; }
                string query;
                try
                {
                    query = "SELECT TOP 300 p.ID, p.NoPO NoPo, s.NoSPP NoSpp, p.POPurchnDate TanggalPo, FORMAT(pd.Qty,'N0') Qty, u.UOMDesc UomDesc, FORMAT(pd.Price,'N0') Price,case sp.ItemTypeID when 1 then(select ItemCode from Inventory where Inventory.ID = sp.ItemID) when 2 then(select ItemCode from Asset where Asset.ID = sp.ItemID) when 3 then(select ItemCode from Biaya where Biaya.ID = sp.ItemID) else '' end ItemCode, case sp.ItemTypeID when 1 then(select ItemName from Inventory where Inventory.ID = sp.ItemID) when 2 then(select ItemName from Asset where Asset.ID = sp.ItemID) when 3 then(select ItemName from Biaya where Biaya.ID = sp.ItemID) + ' - ' + sp.Keterangan else '' end ItemName FROM POPurchnDetail pd, POPurchn p, SPPDetail sp, SPP s, UOM u WHERE pd.POID = p.ID AND pd.SppDetailID = sp.ID AND sp.SPPID = s.ID AND pd.UOMID = u.ID AND pd.Status>-1 AND p.status>-1 AND sp.Status>-1 AND s.status>-1 " + Search+" ORDER BY pd.ID desc";
                    AllData = connection.Query<PoPurchnNf.ParamDataPo>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PoPurchnNf.ParamListNoSpp> ListNoSpp()
        {
            List<PoPurchnNf.ParamListNoSpp> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT DISTINCT s.ID, s.NoSpp FROM SPPDetail sd,SPP s WHERE sd.SPPID = s.ID AND sd.Status > -1  AND s.Status > -1AND sd.QtyPO < sd.Quantity AND s.Approval = 3 ";
                    AllData = connection.Query<PoPurchnNf.ParamListNoSpp>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PoPurchnNf.ParamListSppDetailBySppId> ListSppDetailBySppId(int SppId)
        {
            List<PoPurchnNf.ParamListSppDetailBySppId> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "select A.ID,case A.ItemTypeID when 1  then (select ItemName from Inventory where ID=A.ItemID and RowStatus > -1) when 2 then (select ItemName from Asset where ID=A.ItemID and RowStatus > -1) else  CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())<  (Select CreatedTime from SPP where SPP.ID=A.SPPID))  THEN(select ItemName from biaya where ID=A.ItemID and biaya.RowStatus>-1)+' - '+  (Select SPPDetail.Keterangan From SPPDetail where SPPDetail.ID=A.ID) ELSE  (select ItemName from biaya where ID=A.ItemID and biaya.RowStatus>-1) END  end ItemName from SPPDetail as A,UOM as C where  A.Quantity<>A.QtyPO and (PendingPO=0 or PendingPO is null) and  A.UOMID = C.ID and A.Status>-1 and A.SPPID ="+SppId;
                    AllData = connection.Query<PoPurchnNf.ParamListSppDetailBySppId>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static DateTime SchDelivery(string SPPID, int ItemID)
        {
            POPurchnFacade po = new POPurchnFacade();
            DateTime TglDelive = po.LeadTime(SPPID, ItemID.ToString());
            int CheckHariAPA = OffDayCalender(TglDelive, TglDelive);
            return TglDelive.AddDays(CheckHariAPA);
        }

        public static int OffDayCalender(DateTime StarDate, DateTime EndData)
        {
            int TambahHari = 0;
            POPurchnFacade po = new POPurchnFacade();
            POPurchn Libur = po.DayOffCalender(StarDate.ToString("yyyyMMdd"), EndData.ToString("yyyyMMdd"));
            if (Libur.Status > 0)
            {
                POPurchn HariAPA = po.DayOffCalender(StarDate.ToString("yyyyMMdd"), EndData.ToString("yyyyMMdd"), true);
                switch (HariAPA.Remark.Trim())
                {
                    case "Friday":
                        TambahHari = 2;
                        break;
                    default:
                        TambahHari = 0;
                        break;
                }
            }
            return TambahHari;
        }

        public static int CheckLeadTime(int ItemID, int ItemTypeID)
        {
            Inventory objInv = new Inventory();
            InventoryFacade Inv = new InventoryFacade();
            objInv = Inv.RetrieveByIdNew(ItemID, ItemTypeID);
            return (Inv.Error == string.Empty) ? objInv.LeadTime : 1;
        }

        public static string ItemIDBiayaOld(string BiayaName)
        {
            BiayaFacade biayafacade = new BiayaFacade();
            Biaya obj = biayafacade.RetrieveByName(BiayaName);
            if (biayafacade.Error == string.Empty && obj.ID > 0)
            {
                return obj.ID.ToString();
            }
            return string.Empty;
        }

        public static string GetHargaTerendah(string ItemID, int TypeID, string ItemCode)
        {
            string Msg = "";
            try
            {
                POPurchnFacade po = new POPurchnFacade();
                decimal Harga1 = 0;
                //decimal Harga2 = 0;
                string Plant = string.Empty;
                string Plant2 = string.Empty;
                Users users=(Users)HttpContext.Current.Session["Users"];
                switch (users.UnitKerjaID)
                {
                    case 1:
                        Plant = "Citeureup";
                        Plant2 = "Karawang";
                        break;
                    case 7:
                        Plant = "Karawang";
                        Plant2 = "Citeureup";
                        break;

                }
                string Message1 = string.Empty; string Message2 = string.Empty;
                ArrayList arrData = po.HargaRendah(ItemID, TypeID);
                //GetItemID dari plant  lain
                string ImdID = ItemIDPlantLine(ItemCode, TypeID);
                //ArrayList arrData2 = HargaPlantLine(ImdID, TypeID); ;
                string titikdua = " : ";
                Message1 += "Perolehan Harga Terendah :\\n\\n";
                foreach (POPurchn p1 in arrData)
                {
                    Harga1 = Convert.ToDecimal(p1.Price);
                    Message1 += "Plant " + titikdua.PadLeft((27 - ("Plant").ToString().Length), ' ') + Plant.ToUpper() + "\\n";
                    Message1 += "Harga Terendah " + titikdua.PadLeft((17 - ("Harga Terendah").ToString().Length), ' ') + (Harga1.ToString()) + "\\n";
                    Message1 += "Supplier Name" + titikdua.PadLeft((19 - ("Supplier Name").ToString().Length), ' ') + p1.SupplierName;
                }
                /*foreach (POPurchn p2 in arrData2)
                {
                    Harga2 = Convert.ToDecimal(p2.Price);
                    Message2 += "\\n\\n";
                    Message2 += "Plant " + titikdua.PadLeft((27 - ("Plant").ToString().Length), ' ') + Plant2.ToUpper() + "\\n";
                    Message2 += "Harga Terendah " + titikdua.PadLeft((17 - ("Harga Terendah").ToString().Length), ' ') + (Harga2.ToString()) + "\\n";
                    Message2 += "Supplier Name" + titikdua.PadLeft((19 - ("Supplier Name").ToString().Length), ' ') + p2.SupplierName;
                }*/
                //if (Harga1 > 0 && Harga1 < Harga2)
                //{
                Msg = Message1 + "\\n" + Message2;
                //DisplayAJAXMessage(this, Message1 + "\\n" + Message2);
                //}
                //else if (Harga2 > 0 && Harga2 < Harga1)
                //{
                //    DisplayAJAXMessage(this, Message2);
                //}
               
            }
            catch { }
            return Msg;
        }

        public static string ItemIDPlantLine(string ItemCode, int ItemTypeID)
        {
            string result = string.Empty;
            //bpas_api.WebService1 bpas = new bpas_api.WebService1();
            Global2 bpas = new Global2();
            DataSet ds = new DataSet();
            string con = string.Empty;
            string Tabel = string.Empty;
            string Criteria = string.Empty;
            Users users = (Users)HttpContext.Current.Session["Users"];
            switch (users.UnitKerjaID)
            {
                case 7: con = "GRCBoardCtrp"; break;
                case 1: con = "GRCBoardKrwg"; break;
                default: con = "GRCBoardPurch"; break;
            }
            switch (ItemTypeID)
            {
                case 1: Tabel = "Inventory"; break;
                case 2: Tabel = "Asset"; break;
                case 3: Tabel = "Biaya"; break;
            }
            Criteria = " Where ItemCode='" + ItemCode + "'";
            try
            {
                ds = bpas.GetDataFromTable(Tabel, Criteria, con);
                foreach (DataRow d in ds.Tables[0].Rows)
                {
                    result = d["ID"].ToString();
                }
            }
            catch { }
            return result;
        }

        /*public static ArrayList HargaPlantLine(string ItemID, int ItemTypeID)
        {
            bpas_api.WebService1 bpas = new bpas_api.WebService1();
            ArrayList arrData = new ArrayList();
            DataSet ds = new DataSet();
            string con = string.Empty;
            switch (((Users)Session["Users"]).UnitKerjaID)
            {
                case 7:
                    con = "GRCBoardCtrp";
                    break;
                case 1:
                    con = "GRCBoardKrwg";
                    break;
                default:
                    con = "GRCBoardPurch";
                    break;
            }
            try
            {
                ds = bpas.GetHargaTerendah(ItemID, ItemTypeID.ToString(), con);
                foreach (DataRow d in ds.Tables[0].Rows)
                {
                    arrData.Add(new POPurchn
                    {
                        ID = Convert.ToInt32(d["ID"].ToString()),
                        ItemID = Convert.ToInt32(d["ItemID"].ToString()),
                        Price = Convert.ToDecimal(d["Price"].ToString()),
                        SupplierID = Convert.ToInt32(d["SupplierID"].ToString()),
                        SupplierName = d["SupplierName"].ToString()
                    });
                }
                return arrData;
            }
            catch (Exception)
            {
                return arrData;
            }
        }*/

        public static DateTime SelectCretedTimeBySpp(int SppId)
        {
            DateTime CreatedTime=DateTime.Now;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "SELECT CreatedTime FROM Spp Where RowStatus>-1 And ID="+SppId;
                    CreatedTime = connection.QueryFirstOrDefault<DateTime>(query);
                }
                catch (Exception e)
                {

                }
            }
            return CreatedTime;
        }

        public static PoPurchnNf.ParamSppDetailById SelectAllBySppDetail(int Id)
        {
            PoPurchnNf.ParamSppDetailById AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "select A.ID, A.SPPID, A.GroupID, A.Quantity, A.QtyPO, C.UOMCode as Satuan, A.UOMID, A.ItemTypeID, A.ItemID,A.Keterangan, case A.ItemTypeID when 1  then (select ItemName from Inventory where ID=A.ItemID and RowStatus > -1) when 2 then (select ItemName from Asset where ID=A.ItemID and RowStatus > -1) else  CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())<  (Select CreatedTime from SPP where SPP.ID=A.SPPID))  THEN(select ItemName from biaya where ID=A.ItemID and biaya.RowStatus>-1)+' - '+  (Select SPPDetail.Keterangan From SPPDetail where SPPDetail.ID=A.ID) ELSE  (select ItemName from biaya where ID=A.ItemID and biaya.RowStatus>-1) END  end ItemName,case A.ItemTypeID when 1  then (select ItemCode from Inventory where ID=A.ItemID and RowStatus > -1) when 2 then (select ItemCode from Asset where ID=A.ItemID and RowStatus > -1) else (select ItemCode from Biaya where ID=A.ItemID and RowStatus > -1) end ItemCode from SPPDetail as A, UOM as C where A.UOMID = C.ID and A.Status>-1 and A.ID = " + Id;
                    AllData = connection.QueryFirstOrDefault<PoPurchnNf.ParamSppDetailById>(query);
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static PurchasingNf.ParamDtl SelectAllBySppDetail1(int Id)
        {
            PurchasingNf.ParamDtl AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "select A.ID, A.SPPID, A.GroupID, A.Quantity, A.QtyPO, C.UOMCode as Satuan, A.UOMID, A.ItemTypeID, A.ItemID,A.Keterangan, case A.ItemTypeID when 1  then (select ItemName from Inventory where ID=A.ItemID and RowStatus > -1) when 2 then (select ItemName from Asset where ID=A.ItemID and RowStatus > -1) else  CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())<  (Select CreatedTime from SPP where SPP.ID=A.SPPID))  THEN(select ItemName from biaya where ID=A.ItemID and biaya.RowStatus>-1)+' - '+  (Select SPPDetail.Keterangan From SPPDetail where SPPDetail.ID=A.ID) ELSE  (select ItemName from biaya where ID=A.ItemID and biaya.RowStatus>-1) END  end ItemName,case A.ItemTypeID when 1  then (select ItemCode from Inventory where ID=A.ItemID and RowStatus > -1) when 2 then (select ItemCode from Asset where ID=A.ItemID and RowStatus > -1) else (select ItemCode from Biaya where ID=A.ItemID and RowStatus > -1) end ItemCode from SPPDetail as A, UOM as C where A.UOMID = C.ID and A.Status>-1 and A.ID = " + Id;
                    AllData = connection.QueryFirstOrDefault<PurchasingNf.ParamDtl>(query);
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static int CekLastPrice(int SPPDetailId, string supplier, int viewprice)
        {
            int SelectOne = 0; ;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select COUNT(A.ID) SelectOne from POPurchnDetail A, POPurchn B, MataUang M where B.ID = A.POID AND B.Crc = M.ID and A.ItemID in (select ItemID from SPPDetail where ID = " + SPPDetailId + ")  and B.SupplierId in  (select ID from SuppPurch where SupplierName = '" + supplier + "') ";
                    SelectOne = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return SelectOne;
        }

        public static PoPurchnNf.ParamlastPrice lastPrice(int SPPDetailId, string supplier, int viewprice)
        {
            PoPurchnNf.ParamlastPrice AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string Price = " A.Price,";
                if (viewprice < 2) {
                    Price = " case when (select isnull(harga,0) from Inventory where ID=A.ItemID)=0 then A.Price else 0 end Price,";
                }
                string query;
                try
                {
                    query = "select top 1 "+Price+ " B.Crc, M.Lambang from POPurchnDetail A, POPurchn B, MataUang M where B.ID=A.POID AND B.Crc=M.ID and A.ItemID in (select ItemID from SPPDetail where ID=" + SPPDetailId + ") and B.SupplierId in  (select ID from SuppPurch where SupplierName='" + supplier + "') order by A.ID desc";
                    AllData = connection.QueryFirstOrDefault<PoPurchnNf.ParamlastPrice>(query);
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static DateTime BiayaNewActive()
        {
            DateTime ModifiedTime = DateTime.Now;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "Select ModifiedTime From BiayaNew where RowStatus=1";
                    ModifiedTime = connection.QueryFirstOrDefault<DateTime>(query);
                }
                catch (Exception e)
                {

                }
            }
            return ModifiedTime;
        }

        public static int CekHargaRendah(int Id, int TypeItem)
        {
            int SelectOne = 0; ;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "SELECT count(pd.ID) SelectOne FROM POPurchnDetail pd LEFT JOIN POPurchn AS po ON po.ID = pd.POID LEFT JOIN SuppPurch AS sp ON sp.ID = po.SupplierID WHERE pd.itemid = " + Id + "  AND po.Approval > -1 AND pd.ItemTypeID = " + TypeItem ;
                    SelectOne = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return SelectOne;
        }

        public static PoPurchnNf.ParamHargaRendah HargaRendah(int Id, int TypeItem)
        {
            PoPurchnNf.ParamHargaRendah AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT TOP 1 pd.ID, pd.ItemID, str(pd.Price,10,3) Price,po.SupplierID,sp.SupplierName FROM POPurchnDetail pd LEFT JOIN POPurchn AS po ON po.ID = pd.POID LEFT JOIN SuppPurch AS sp ON sp.ID = po.SupplierID WHERE pd.itemid = " + Id+"  AND po.Approval > -1 AND pd.ItemTypeID = "+ TypeItem + " ORDER BY Price ASC, ID DESC";
                    AllData = connection.QueryFirstOrDefault<PoPurchnNf.ParamHargaRendah>(query);
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static int GetIDBiaya( string ItemName)
        {
            int ID = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "Select top 1 ID from Biaya Where rowStatus>-1 and ItemName='" + ItemName.TrimStart().TrimEnd() + "' order by ID desc";
                    ID = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return ID;
        }

        public static List<PoPurchnNf.ParamListSup> ListSup(string SupplierName)
        {
            List<PoPurchnNf.ParamListSup> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT TOP 10 ID, SupplierName FROM SuppPurch WHERE SupplierName LIKE '%" + SupplierName + "%' ORDER BY SupplierName";
                    AllData = connection.Query<PoPurchnNf.ParamListSup>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PoPurchnNf.ParamInfoSup> InfoSup(int ID)
        {
            List<PoPurchnNf.ParamInfoSup> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT UP UPSupplier, Telepon TelpSupplier, Fax FaxSupplier FROM SuppPurch WHERE rowStatus>-1 and ID="+ID;
                    AllData = connection.Query<PoPurchnNf.ParamInfoSup>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static PoPurchnNf.ParamInfoSupById InfoSupById(int ID)
        {
            PoPurchnNf.ParamInfoSupById AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT UP UPSupplier, Telepon TelpSupplier, Fax FaxSupplier, Npwp, SubCompanyID,ForDK FROM SuppPurch WHERE rowStatus>-1 and ID=" + ID;
                    AllData = connection.QueryFirstOrDefault<PoPurchnNf.ParamInfoSupById>(query);
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static PoPurchnNf.ParamInfoKadarAir InfoKadarAir(int ID)
        {
            PoPurchnNf.ParamInfoKadarAir AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT d.GrossDepo Gross,d.NettDepo Netto,d.KADepo KadarAir,d.NOPOL NoPol from DeliveryKertas d LEFT JOIN SuppPurch s ON s.ID = d.SupplierID LEFT JOIN Inventory x ON x.ItemCode = d.ItemCode Where d.RowStatus > -1 AND d.ID = " + ID;
                    AllData = connection.QueryFirstOrDefault<PoPurchnNf.ParamInfoKadarAir>(query);
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PoPurchnNf.ParamListMataUang> ListMataUang(int ID)
        {
            List<PoPurchnNf.ParamListMataUang> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string Where = ""; if (ID != 0) { Where = "Where ID!="+ID; }
                string query;
                try
                {
                    query = "SELECT ID, Lambang From MataUang " + Where;
                    AllData = connection.Query<PoPurchnNf.ParamListMataUang>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PoPurchnNf.ParamListTermOfPay> ListTermOfPay(int ID)
        {
            List<PoPurchnNf.ParamListTermOfPay> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string Where = ""; if (ID != 0) { Where = "Where ID!=" + ID; }
                string query;
                try
                {
                    query = "SELECT ID, TermPay From TermOfPay " + Where;
                    AllData = connection.Query<PoPurchnNf.ParamListTermOfPay>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PoPurchnNf.ParamListIndent> ListIndent()
        {
            List<PoPurchnNf.ParamListIndent> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "Select ID,Tenggang From Indent";
                    AllData = connection.Query<PoPurchnNf.ParamListIndent>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static int CekTermOfPayment(int ID)
        {
            int SelectOne = 0; ;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select COUNT(p.ID) SelectOne from POPurchn p, TermOfPay t WHERE p.Termin=t.TermPay and p.SupplierID=" + ID;
                    SelectOne = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return SelectOne;
        }

        public static PoPurchnNf.ParamTermOfPayment TermOfPayment(int supID)
        {
            PoPurchnNf.ParamTermOfPayment AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "Select top 1 t.ID, p.Termin from POPurchn p, TermOfPay t WHERE p.Termin=t.TermPay and p.SupplierID=" + supID + " order by p.ID desc";
                    AllData = connection.QueryFirstOrDefault<PoPurchnNf.ParamTermOfPayment>(query);
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PoPurchnNf.ParamListDlvKertas> ListDlvKertas(string ItemCode, int SupId)
        {
            List<PoPurchnNf.ParamListDlvKertas> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "SELECT d.ID, d.NoSj from DeliveryKertas d LEFT JOIN SuppPurch s ON s.ID = d.SupplierID LEFT JOIN Inventory x ON x.ItemCode = d.ItemCode Where d.RowStatus > -1 AND d.PlantID = 13 AND d.SupplierID ="+SupId+" AND(d.POKAID IS NULL OR d.POKAID = '') AND(d.ItemCode = '"+ ItemCode + "' OR d.ItemCode IS NULL)";
                    AllData = connection.Query<PoPurchnNf.ParamListDlvKertas>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static int QtySudahPo(int Id)
        {
            int SelectOne = 0; ;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select SUM(Qty) QtySudahPo from POPurchnDetail where SppDetailID=" + Id + " and Status >-1";
                    SelectOne = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return SelectOne;
        }

        public static decimal HargaKertas(string ItemCode,int SupId)
        {
            decimal SelectOne = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "SELECT Harga FROM HargaKertasBaru WHERE RowStatus>-1 AND ItemCode='"+ ItemCode + "' AND SubCompanyID IN (SELECT SubCompanyID FROM SuppPurch WHERE ID ="+ SupId + ")";
                    SelectOne = connection.QueryFirstOrDefault<decimal>(query);
                }
                catch (Exception e)
                {

                }
            }
            return SelectOne;
        }

        public static string KodeCompany(int UnitKerjaID)
        {
            string SelectOne = "";
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select A.KodeLokasi as SelectOne from Company as A, Depo as B where B.ID = A.DepoID and B.ID = " + UnitKerjaID;
                    SelectOne = connection.QueryFirstOrDefault<string>(query);
                }
                catch (Exception e)
                {

                }
            }
            return SelectOne;
        }

        public static string KodeSPP(int GroupID)
        {
            string SelectOne = "";
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select groupCode SelectOne from GroupsPurchn where ID = " + GroupID;
                    SelectOne = connection.QueryFirstOrDefault<string>(query);
                }
                catch (Exception e)
                {

                }
            }
            return SelectOne;
        }

        public static int IsHargakertas(int ItemID)
        {
            int SelectOne = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select ISNULL(SUM(harga),0) HARGA from inventory where id" + ItemID;
                    SelectOne = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return SelectOne;
        }

        public static int GetHargaKertas(int ItemID, int SupId)
        {
            int SelectOne = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "SELECT B.Harga Harga FROM Inventory AS A INNER JOIN HargaKertas AS B ON A.ID = B.ItemID where B.ItemID = " + ItemID + " and B.SupplierID = " + SupId;
                    SelectOne = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return SelectOne;
        }

        public static void UpdateDeliveryKertas(PoPurchnNf.DeliveryKertas dk)
        {
            ZetroLib zl = new ZetroLib();
            zl.StoreProcedurName = "spDeliveryKertas_UpdatePO";
            zl.TableName = "DeliveryKertas";
            zl.Criteria = "ID,POKAID,GrossPlant,NettPlant,KAPlant,TglReceipt,LastModifiedBy,LastModifiedTime";
            zl.hlp = new PoPurchnNf.DeliveryKertas();
            zl.Option = "Update";
            string rst = zl.CreateProcedure();
            if (rst == string.Empty)
            {
                zl.hlp = dk;
                int rest = zl.ProcessData();
            }
        }
        public static void UpdateKadarAirQC(PoPurchnNf.QAKadarAir ka)
        {
            ZetroLib zl = new ZetroLib();
            zl.StoreProcedurName = "spDeliveryKertasKA_UpdatePO";
            zl.TableName = "DeliveryKertasKA";
            zl.Criteria = "ID,POKAID,LastModifiedBy,LastModifiedTime";
            zl.hlp = new PoPurchnNf.QAKadarAir();
            string rst = zl.CreateProcedure();
            if (rst == string.Empty)
            {
                zl.hlp = ka;
                int r = zl.ProcessData();
            }
        }

        public static PoPurchnNf.ByNo SelectByNo(int id)
        {
            PoPurchnNf.ByNo AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "select A.ID, isnull(C.ID,0) as PODetailID  from POPurchn as A,POPurchnDetail as C where C.POID = A.ID and A.id = '" + id + "' order by A.ID desc";
                    AllData = connection.QueryFirstOrDefault<PoPurchnNf.ByNo>(query);
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }
        public static PoPurchnNf.ByNo SelectByNo1(string nopo)
        {
            PoPurchnNf.ByNo AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "select A.ID, isnull(C.ID,0) as PODetailID  from POPurchn as A,POPurchnDetail as C where C.POID = A.ID and A.nopo = '" + nopo + "' order by A.ID desc";
                    AllData = connection.QueryFirstOrDefault<PoPurchnNf.ByNo>(query);
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static PoPurchnNf.ByID SelectByID(int id)
        {
            PoPurchnNf.ByID AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "select itemId from POPurchnDetail where poid="+id;
                    AllData = connection.QueryFirstOrDefault<PoPurchnNf.ByID>(query);
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static int GetIdPo(string NoPo)
        {
            int val = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select id from POPurchn where NoPO='"+ NoPo + "' and Status > -1";
                    val = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return val;
        }

        public static decimal GettotalPrice(int id)
        {
            decimal SelectOne = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "WITH q AS ( " +
"SELECT sum(totalprice)totalprice, "+ id + " idpo " +
"FROM( " +
"SELECT Qty * Price totalprice FROM POPurchnDetail d, POPurchn p " +
"WHERE d.POID = p.ID AND d.Status > -1 " +
"AND p.id = " + id + " " +
") AS q " +
") " +
"SELECT " +
"q.TotalPrice + ((p.PPH / 100) * q.TotalPrice) + " +
"((p.PPN / 100) * q.TotalPrice) - " +
"((p.Disc / 100) * q.TotalPrice) " +
"AS TotalPrice " +
"FROM q, POPurchn p WHERE q.idpo = p.id ";
                    SelectOne = connection.QueryFirstOrDefault<decimal>(query);
                }
                catch (Exception e)
                {

                }
            }
            return SelectOne;
        }

        public static int cekPo(string NoPo)
        {
            int SelectOne = 0;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select count id SelectOne from POPurchn where status>-1 and nopo='"+ NoPo + "'";
                    SelectOne = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {

                }
            }
            return SelectOne;
        }

        public static List<PoPurchnNf.ParamDataSpp> GetListDataSpp(string NoSpp, string UserHead)
        {
            List<PoPurchnNf.ParamDataSpp> AllData = new List<PoPurchnNf.ParamDataSpp>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string where = "";
                if (NoSpp != "") { where += " and x.KodeSpp='"+ NoSpp + "' "; }
                if (UserHead != "") { where += " and x.UserHead='" + UserHead + "' "; ; }
                string query;
                try
                {
                    query = "Select * from ( "+
    "select s.ID,s.NoSPP KodeSpp, s.Minta TanggalSpp, s.ApproveDate3 ApvDate, u.UserName UserHead, " +
       "case s.PermintaanType when 1 then 'Top Urgent' when 2 then 'Biasa' when 3 then 'Sesuai Schedule' end TypeSpp, " +
       "(select COUNT(ID) from SPPDetail where SPPID = s.ID and Status > -1 and Quantity > QtyPO) Detail " +
            "from SPP as s " +
    "LEFT JOIN Users as U on U.ID = s.HeadID where s.Approval = 2 and status = 0 and Approval = 2 " +
") as x where x.Detail > 0 "+ where + " order by x.ApvDate";
                    AllData = connection.Query<PoPurchnNf.ParamDataSpp>(query).ToList();
                }
                catch (Exception)
                {
                    AllData = null;
                }
            }
            return AllData;
        }
        public static List<PoPurchnNf.ParamDetailSpp> GetListDetailSpp(int SPPID)
        {
            List<PoPurchnNf.ParamDetailSpp> AllData = new List<PoPurchnNf.ParamDetailSpp>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "select ID, (Select nospp from spp where ID=sppid) KodeSpp," +
                          "  Case ItemTypeID  " +
                          "      when 1 then (Select ItemCode from Inventory where ID=ItemID) " +
                          "      when 2 then (Select ItemCode from Asset where ID=ItemID) " +
                          "      when 3 then (select ItemCode from Biaya where ID=ItemID)  " +
                          "  end ItemCode, " +
                          "  Case ItemTypeID  " +
                          "      when 1 then (Select ItemName from Inventory where ID=ItemID) " +
                          "      when 2 then (Select ItemName from Asset where ID=ItemID) " +
                          "      when 3 then " + ItemSPPBiayaNew("SPPDetail") +
                          "  end ItemName, " +
                          "  Case ItemTypeID  " +
                          "      when 1 then (Select isnull(LeadTime,0) from Inventory where ID=ItemID) " +
                          "      when 2 then (Select isnull(LeadTime,0) from Asset where ID=ItemID) " +
                          "      when 3 then (select isnull(LeadTime,0) from Biaya where ID=ItemID)  " +
                          "  end LeadTime, " +
                          "  (Select UomCode from UOM where ID=UOMID)Satuan, " +
                          "  Quantity QtySpp,QtyPo, tglkirim DeliveryDate " +
                          " from SPPDetail where SPPID=" + SPPID + " and status>-1 and (PendingPo=0 or PendingPo is null) order by ID desc";
                    AllData = connection.Query<PoPurchnNf.ParamDetailSpp>(query).ToList();
                }
                catch (Exception)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<PoPurchnNf.ParamDetailSpp> GetListPendingSpp()
        {
            List<PoPurchnNf.ParamDetailSpp> AllData = new List<PoPurchnNf.ParamDetailSpp>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                string query;
                try
                {
                    query = "select ID, (Select nospp from spp where ID=sppid) KodeSpp," +
                          "  Case ItemTypeID  " +
                          "      when 1 then (Select ItemCode from Inventory where ID=ItemID) " +
                          "      when 2 then (Select ItemCode from Asset where ID=ItemID) " +
                          "      when 3 then (select ItemCode from Biaya where ID=ItemID)  " +
                          "  end ItemCode, " +
                          "  Case ItemTypeID  " +
                          "      when 1 then (Select ItemName from Inventory where ID=ItemID) " +
                          "      when 2 then (Select ItemName from Asset where ID=ItemID) " +
                          "      when 3 then " + ItemSPPBiayaNew("SPPDetail") +
                          "  end ItemName, " +
                          "  Case ItemTypeID  " +
                          "      when 1 then (Select isnull(LeadTime,0) from Inventory where ID=ItemID) " +
                          "      when 2 then (Select isnull(LeadTime,0) from Asset where ID=ItemID) " +
                          "      when 3 then (select isnull(LeadTime,0) from Biaya where ID=ItemID)  " +
                          "  end LeadTime, " +
                          "  (Select UomCode from UOM where ID=UOMID) Satuan, " +
                          "  Quantity QtySpp,QtyPo, tglkirim DeliveryDate, AlasanPending " +
                          " from SPPDetail where status>-1 and PendingPo > 0 order by ID desc";
                    AllData = connection.Query<PoPurchnNf.ParamDetailSpp>(query).ToList();
                }
                catch (Exception)
                {
                    AllData = null;
                }
            }
            return AllData;
        }
        public static string ItemSPPBiayaNew(string TableName)
        {
            string strSQL = "CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())< " +
                " (Select CreatedTime from SPP where SPP.ID=" + TableName + ".SPPID)) " +
                " THEN(select ItemName from Biaya where Biaya.ID=" + TableName + ".ItemID and Biaya.RowStatus>-1)+' - '+ " +
                " (Select SPPDetail.Keterangan From SPPDetail where " +
                " SPPDetail.ID=" + TableName + ".ID) ELSE " +
                " (select ItemName from biaya where ID=" + TableName + ".ItemID and biaya.RowStatus>-1) END ";
            return strSQL;
        }

        public static string SaveSpp(int ID, string Alasan)
        {
            string SelectOne = "";
            int PendingPO = 1;
            if (Alasan == "") { PendingPO = 0; }
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "Update SPPDetail set Status=0,PendingPO="+ PendingPO + ",AlasanPending='" + Alasan + "' where ID=" + ID;
                    SelectOne = connection.QueryFirstOrDefault<string>(query);
                }
                catch (Exception e)
                {

                }
            }
            return SelectOne;
        }

    }//________________________________________
}
