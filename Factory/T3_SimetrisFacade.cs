using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using Dapper;

namespace Factory
{
    public class T3_SimetrisFacade : BusinessFacade.AbstractTransactionFacadeF
    {
        private T3_Simetris objT3_Simetris = new T3_Simetris();
        private ArrayList arrT3_Simetris;
        private List<SqlParameter> sqlListParam;

        public T3_SimetrisFacade(object objDomain)
            : base(objDomain)
        {
            objT3_Simetris = (T3_Simetris)objDomain;
        }
        public T3_SimetrisFacade()
        {
        }
        public override int Insert1(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }
        public override int Update1(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }
        public override int Update2(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }
        public override int Update(TransactionManager transManager)
        {
            throw new NotImplementedException();
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
                sqlListParam.Add(new SqlParameter("@rekapID", objT3_Simetris.RekapID));
                sqlListParam.Add(new SqlParameter("@SerahID", objT3_Simetris.SerahID));
                sqlListParam.Add(new SqlParameter("@GroupID", objT3_Simetris.GroupID));
                sqlListParam.Add(new SqlParameter("@LokID", objT3_Simetris.LokasiID));
                sqlListParam.Add(new SqlParameter("@ItemID", objT3_Simetris.ItemID));
                sqlListParam.Add(new SqlParameter("@TglSm", objT3_Simetris.TglSm));
                sqlListParam.Add(new SqlParameter("@QtyIn", objT3_Simetris.QtyInSm));
                sqlListParam.Add(new SqlParameter("@QtyOut", objT3_Simetris.QtyOutSm));
                sqlListParam.Add(new SqlParameter("@NCH", objT3_Simetris.NCH));
                sqlListParam.Add(new SqlParameter("@NCSS", objT3_Simetris.NCSS));
                sqlListParam.Add(new SqlParameter("@NCSE", objT3_Simetris.NCSE));
                sqlListParam.Add(new SqlParameter("@BS", objT3_Simetris.BS));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT3_Simetris.CreatedBy));
                sqlListParam.Add(new SqlParameter("@Defect", objT3_Simetris.Defect));
                sqlListParam.Add(new SqlParameter("@MCutter", objT3_Simetris.MCutter));
                sqlListParam.Add(new SqlParameter("@TglProd", objT3_Simetris.TglProduksi));
                //int intResult = transManager.DoTransaction(sqlListParam, "[spInsertT3_Simetris]");
                //int intResult = transManager.DoTransaction(sqlListParam, "[spInsertT3_SimetrisNC]");
                int intResult = transManager.DoTransaction(sqlListParam, "[spInsertT3_SimetrisNCBS]");
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
        public int GetMaxCut250(int unitkerjaID)
        {
            int cut = 0;
            string strcut = string.Empty;
            string strSQL = "SELECT * from t3_options where  plantID=" + unitkerjaID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_Simetris = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    strcut = sqlDataReader["cutmax250"].ToString();
                }
            }
            if (strcut.ToUpper().Trim() == "TRUE")
                cut = 1;
            else
                cut = 0;
            return cut;
        }
        public decimal GetTotalLembarOK(string thnbln)
        {
            decimal lembar = 0;
            string strSQL = "declare @thnbln varchar(6) "+
                "set @thnbln='" + thnbln + "' " +
                "select SUM(qty) qty from ( " +
                "select isnull(SUM(qty),0) qty from vw_KartuStockBJNew A where LEFT(convert(char,tanggal,112),6)=@thnbln and " +
                "A.qty>0 and A.Process  in ('direct') and ItemID  in (select ID from FC_Items where PartNo like '%-3-%' or partno like '%-w-%' or partno like '%-m-%' ) " +
                "union all " +
                "select isnull(SUM(qty),0) qty from vw_KartuStockBJNew A where LEFT(convert(char,tanggal,112),6)=@thnbln and " +
                "A.qty>0 and A.Process  not in ('adjust-IN','retur') and ItemID  in (select ID from FC_Items where PartNo like '%-3-%' or partno like '%-w-%' or partno like '%-m-%' ) " +
                "and  (Keterangan like '%-P-%' or Keterangan like '%-1-%' ) and Process like '%simetris%')S";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_Simetris = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    lembar =Convert.ToDecimal(sqlDataReader["qty"]);
                }
            }
            return lembar;
        }
        public decimal GetTotalKubikOK(string thnbln)
        {
            decimal Kubik = 0;
            string strSQL = "declare @thnbln varchar(6) " +
                "set @thnbln='" + thnbln + "' " +
                //"select isnull(SUM(A.qty*I.Volume),0) kubik from vw_KartuStockBJNew A inner join fc_items I on A.itemid=I.ID " +
                //"where LEFT(convert(char,A.tanggal,112),6)=@thnbln and A.qty>0 and A.Process not in ('adjust-IN','retur') and  " +
                //"(I.PartNo not like '%-3-%' or I.PartNo not like '%-W-%')";

                "select SUM(kubik)kubik from ( " +
                "select isnull(SUM(A.qty*((I.tebal*I.panjang*I.lebar)/1000000000)),0) kubik from vw_KartuStockBJNew A inner join fc_items I on A.itemid=I.ID  " +
                "where LEFT(convert(char,A.tanggal,112),6)=@thnbln and A.qty>0 and A.Process not in ('adjust-IN','retur','direct') and   " +
                "(I.PartNo  like '%-3-%' or I.PartNo  like '%-W-%' or I.PartNo  like '%-M-%') and (Keterangan like '%-P-%' or Keterangan like '%-1-%' )" +
                "union all " +
                "select isnull(SUM(A.qty*((I.tebal*I.panjang*I.lebar)/1000000000)),0) kubik from vw_KartuStockBJNew A inner join fc_items I on A.itemid=I.ID  " +
                "where LEFT(convert(char,A.tanggal,112),6)=@thnbln and A.qty>0 and A.Process in ('direct') and   " +
                "(I.PartNo  like '%-3-%' or I.PartNo  like '%-W-%' or I.PartNo  like '%-M-%'))S";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_Simetris = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    Kubik = Convert.ToDecimal(sqlDataReader["kubik"]);
                }
            }
            return Kubik;
        }
        public int  HapusData(string ID)
        {
            int hasil= 0;
            if (ID.Trim() == "")
                return hasil;
            if (Int32.Parse( ID )== 0)
                return hasil;
            string strSQL = "declare @cutID int " +
                "set @cutid= " + ID +
                "update T3_Simetris set rowstatus=-1 where ID=@cutid " +
                "update T3_Rekap set rowstatus=-1 where CutID=@cutid " +
                "create table #tempSerah(lokid int,itemid int,qtyin int,qtyout int) " +
                " declare @lokid int   " +
                " declare @itemid int " +
                " declare @qtyin int " +
                " declare @qtyout int " +
                " declare kursor cursor for " + 
                " select lokid,itemid,qtyin,qtyout from T3_Rekap  where  CutID=@cutid " +
                " open kursor " +
                " FETCH NEXT FROM kursor " +
                " INTO  @lokid ,@itemid ,@qtyin,@qtyout " +
                " WHILE @@FETCH_STATUS = 0 " +
                " begin " +
                " if @qtyin >0 " +
                "begin " +
                "update t3_serah set qty=qty-@qtyin where lokid=@lokid and itemid=@itemid " +
                "end " +
                "else " +
                "begin " +
                "update t3_serah set qty=qty+@qtyout where lokid=@lokid and itemid=@itemid " +
                "end  " +
                " FETCH NEXT FROM kursor  " +
                " INTO @lokid ,@itemid ,@qtyin,@qtyout " +
                " END  " +
                " CLOSE kursor  " +
                " DEALLOCATE kursor " +
                " drop table #tempSerah " +
                " select * from t3_simetris where ID=@cutid and rowstatus>-1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_Simetris = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                hasil = 1;
            }
            else
                hasil = 0;
            return hasil;
        }
        public ArrayList RetrieveBytgl(string tgl)
        {
            string strSQL =
                "SELECT B.ID,I1.PartNo as PartnoSer, L1.Lokasi LokasiSer, B.QtyIn as QtyInSm, I2.PartNo AS PartnoSm, L2.Lokasi AS LokasiSm, B.QtyOut as QtyOutSm, D.Groups,ISNULL(B.MCutter,'-')MCutter,B.CreatedBy " +
                "FROM T3_Serah AS A INNER JOIN T3_Simetris AS B ON A.ID = B.SerahID INNER JOIN " +
                "FC_Lokasi AS L1 ON A.LokID = L1.ID INNER JOIN FC_Items AS I1 ON A.ItemID = I1.ID INNER JOIN  " +
                "FC_Lokasi AS L2 ON B.LokID = L2.ID INNER JOIN FC_Items AS I2 ON B.ItemID = I2.ID LEFT JOIN T3_GroupM AS D ON B.GroupID = D.ID "+
                "where B.Rowstatus>-1 and convert(varchar,B.tgltrans,112)='" + tgl + "' order by B.ID desc"; 
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_Simetris = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_Simetris.Add(GenerateObject1(sqlDataReader));
                }
            }
            else
                arrT3_Simetris.Add(new T3_Simetris());

            return arrT3_Simetris;
        }
        public T3_Simetris GenerateObject(SqlDataReader sqlDataReader)
        {
            objT3_Simetris = new T3_Simetris();
            objT3_Simetris.PartnoSer = sqlDataReader["PartnoSer"].ToString();
            objT3_Simetris.LokasiSer = sqlDataReader["LokasiSer"].ToString();
            objT3_Simetris.QtyInSm = Convert.ToInt32(sqlDataReader["QtyInSm"]);
            objT3_Simetris.PartnoSm = sqlDataReader["PartnoSm"].ToString();
            objT3_Simetris.LokasiSm = (sqlDataReader["LokasiSm"]).ToString();
            objT3_Simetris.QtyOutSm = Convert.ToInt32(sqlDataReader["QtyOutSm"]);
            objT3_Simetris.GroupName = (sqlDataReader["groups"]).ToString();
            return objT3_Simetris;
        }

        public T3_Simetris GenerateObject1(SqlDataReader sqlDataReader)
        {
            objT3_Simetris = new T3_Simetris();
            objT3_Simetris.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objT3_Simetris.PartnoSer = sqlDataReader["PartnoSer"].ToString();
            objT3_Simetris.LokasiSer = sqlDataReader["LokasiSer"].ToString();
            objT3_Simetris.QtyInSm = Convert.ToInt32(sqlDataReader["QtyInSm"]);
            objT3_Simetris.PartnoSm = sqlDataReader["PartnoSm"].ToString();
            objT3_Simetris.LokasiSm = (sqlDataReader["LokasiSm"]).ToString();
            objT3_Simetris.QtyOutSm = Convert.ToInt32(sqlDataReader["QtyOutSm"]);
            objT3_Simetris.GroupName = (sqlDataReader["groups"]).ToString();

            objT3_Simetris.MCutter = (sqlDataReader["MCutter"]).ToString();
            objT3_Simetris.CreatedBy = (sqlDataReader["CreatedBy"]).ToString();

            return objT3_Simetris;
        }

public static List<Produk.PartnoStok> GetNoProdukStok(string partno, string groupid)
        {
            List<Produk.PartnoStok> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query;
                    if (groupid != "")
                    {
                        query = "SELECT DISTINCT top 10 TRIM(PartNo) PartNo, Tebal, Panjang, Lebar, A.GroupID, A.ItemID FROM T3_Serah AS A INNER JOIN FC_Items AS C ON A.ItemID = C.ID INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID where A.Qty > 0 and PartNo like '" + partno + "%' and a.groupid = '"+ groupid+"'";
                    }else
                    {
                        query = "SELECT DISTINCT top 10 TRIM(PartNo) PartNo, Tebal, Panjang, Lebar, A.GroupID, A.ItemID FROM T3_Serah AS A INNER JOIN FC_Items AS C ON A.ItemID = C.ID INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID where A.Qty > 0 and PartNo like '" + partno + "%'";
                    }
                   
                    AllData = connection.Query<Produk.PartnoStok>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<Produk.PartnoStok> GetListPartno(string partno)
        {
            List<Produk.PartnoStok> Partno = new List<Produk.PartnoStok>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    Partno = connection.Query<Produk.PartnoStok>("SELECT TRIM(PartNo) PartNo, Tebal, Panjang, Lebar, Volume, Qty, Lokasi, TRIM(ItemDesc) PartName, a.GroupID, A.itemID, A.LokID, a.ID FROM T3_Serah AS A INNER JOIN FC_Items AS C ON A.ItemID = C.ID INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID where A.Qty > 0 and PartNo = '" + partno + "'").ToList();
                }
                catch (Exception e)
                {
                    Partno = null;
                }
               
            }
            return Partno;
        }


        public static List<Produk.PartnoStok> GetNoProdukJadi(string partno, string groupid)
        {
            List<Produk.PartnoStok> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query;
                    if (groupid != "")
                    {
                        query = "SELECT top 10 TRIM(PartNo) PartNo, Tebal, Panjang, Lebar, TRIM(ItemDesc) PartName, id from fc_items Where rowstatus> -1 and partno not like '%-1-%' and PartNo like '" + partno + "%' and groupid = '" + groupid + "'";
                    }
                    else
                    {
                        query = "SELECT top 10 TRIM(PartNo) PartNo, Tebal, Panjang, Lebar, TRIM(ItemDesc) PartName, id from fc_items Where rowstatus> -1 and partno not like '%-1-%' and PartNo like '" + partno + "%'";
                    }

                    AllData = connection.Query<Produk.PartnoStok>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<Produk.GroupMarketing> GetListProdukMarketing()
        {
            List<Produk.GroupMarketing> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select ID, TRIM(Groups) Groups from t3_groupm  order by [groups]";
                    AllData = connection.Query<Produk.GroupMarketing>(query).ToList();
                }
                catch (Exception)
                {
                    AllData = null;
                }
            }
            return AllData;
        }


        public static List<Produk.Defect> GetListJenisDefect()
        {
            List<Produk.Defect> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select ID,DefName JenisDefect from Def_MasterDefect where RowStatus >-1 order by Urutan";
                    AllData = connection.Query<Produk.Defect>(query).ToList();
                }
                catch (Exception)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<Produk.Cutter> GetListCutter()
        {
            List<Produk.Cutter> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select ID, NamaMesin from T3_MesinCutter where RowStatus>-1 ";
                    AllData = connection.Query<Produk.Cutter>(query).ToList();
                }
                catch (Exception)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<Produk.GetLokasi> GetListLokasi(string lokasi)
        {
            List<Produk.GetLokasi> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "SELECT top 10 ID, TRIM(Lokasi) Lokasi from FC_Lokasi where rowstatus>-1 and lokasi like '" + lokasi + "%'";
                    AllData = connection.Query<Produk.GetLokasi>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static string GetStokLokasiAkhir(string lokasi, string partno)
        {
            string stok;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "SELECT Qty " +
                "FROM  T3_Serah AS A INNER JOIN FC_Items AS C ON A.ItemID = C.ID INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID where " +
                " C.PartNo='" + partno + "' and   B.Lokasi='" + lokasi + "'order by C.PartNo,B.Lokasi";
                    stok = connection.QueryFirstOrDefault<string>(query);
                }
                catch (Exception e)
                {
                    stok = null;
                }
            }
            return stok;
        }

        public static List<Produk.NoPartnoListPlank> GetPartnoBukanListplank(int tebal, int lebar, int panjang)
        {
            List<Produk.NoPartnoListPlank> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select id, lock from No_PartnoListPlank where Tebal='" + tebal+ "' and Lebar='" + lebar + "' and Panjang='" + panjang + "' and RowStatus>-1";
                    AllData = connection.Query<Produk.NoPartnoListPlank>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<Produk.Simetris> GetSimetris(string tgl)
        {
      
            List<Produk.Simetris> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "SELECT B.ID,I1.PartNo as PartnoSer, L1.Lokasi LokasiSer, B.QtyIn as QtyInSm, I2.PartNo AS PartnoSm, L2.Lokasi AS LokasiSm, B.QtyOut as QtyOutSm, D.Groups,ISNULL(B.MCutter,'-')MCutter,B.CreatedBy FROM T3_Serah AS A INNER JOIN T3_Simetris AS B ON A.ID = B.SerahID INNER JOIN FC_Lokasi AS L1 ON A.LokID = L1.ID INNER JOIN FC_Items AS I1 ON A.ItemID = I1.ID INNER JOIN  FC_Lokasi AS L2 ON B.LokID = L2.ID INNER JOIN FC_Items AS I2 ON B.ItemID = I2.ID LEFT JOIN T3_GroupM AS D ON B.GroupID = D.ID where B.Rowstatus>-1 and convert(varchar,B.tgltrans,112)='"+tgl+"' order by B.ID desc";
                    AllData = connection.Query<Produk.Simetris>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }
    }
}
