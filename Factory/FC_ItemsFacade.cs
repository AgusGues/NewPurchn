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
using System.Web;
using System.Web.UI;
using Cogs;
namespace Factory
{
    public class FC_ItemsFacade : AbstractFacade
    {
        private FC_Items objFC_Items = new FC_Items();
        private ArrayList arrFC_Items;
        private List<SqlParameter> sqlListParam;

        public FC_ItemsFacade()
            : base()
        {

        }
        public override int Insert(object objDomain)
        {
            try
            {
                objFC_Items = (FC_Items)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemtypeID", objFC_Items.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@GroupID", objFC_Items.GroupID));
                sqlListParam.Add(new SqlParameter("@SisiID", objFC_Items.SisiID));
                sqlListParam.Add(new SqlParameter("@QID", objFC_Items.QID));
                sqlListParam.Add(new SqlParameter("@Kode", objFC_Items.Kode ));
                sqlListParam.Add(new SqlParameter("@Tebal", objFC_Items.Tebal ));
                sqlListParam.Add(new SqlParameter("@Panjang", objFC_Items.Panjang ));
                sqlListParam.Add(new SqlParameter("@Lebar", objFC_Items.Lebar ));
                sqlListParam.Add(new SqlParameter("@Volume", objFC_Items.Volume ));
                sqlListParam.Add(new SqlParameter("@Partno", objFC_Items.Partno));
                sqlListParam.Add(new SqlParameter("@itemdesc", objFC_Items.ItemDesc ));
                sqlListParam.Add(new SqlParameter("@Stock", objFC_Items.Stock ));
                //sqlListParam.Add(new SqlParameter("@rowstatus", 0));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertFC_Items");
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
            try
            {
                objFC_Items = (FC_Items)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objFC_Items.ID));
                sqlListParam.Add(new SqlParameter("@ItemtypeID", objFC_Items.ItemTypeID ));
                sqlListParam.Add(new SqlParameter("@Kode", objFC_Items.Kode ));
                sqlListParam.Add(new SqlParameter("@Tebal", objFC_Items.Tebal ));
                sqlListParam.Add(new SqlParameter("@Panjang", objFC_Items.Panjang ));
                sqlListParam.Add(new SqlParameter("@Lebar", objFC_Items.Lebar ));
                sqlListParam.Add(new SqlParameter("@Volume", objFC_Items.Volume ));
                sqlListParam.Add(new SqlParameter("@Partno", objFC_Items.Partno ));
                sqlListParam.Add(new SqlParameter("@itemdesc", objFC_Items.ItemDesc ));
                sqlListParam.Add(new SqlParameter("@rowStatus", objFC_Items.Status));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateFC_Items");

                strError = dataAccess.Error;
                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Delete(object objDomain)
        {

            try
            {
                objFC_Items = (FC_Items)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objFC_Items.ID));
                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteFC_Items");
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
            string strSQL = "SELECT * from fc_items where rowstatus>-1 order by ITEMTYPEID,partno";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrFC_Items = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrFC_Items.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrFC_Items.Add(new FC_Items());

            return arrFC_Items;
        }
        public  ArrayList RetrieveBySize(int size,int itemtype)
        {
            string strSQL = "SELECT * from fc_items where rowstatus>-1 and panjang*lebar=" + size + " and itemtypeid=" + itemtype +
                " and SUBSTRING(partno,5,1)='3'  order by tebal, partno";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrFC_Items = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrFC_Items.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrFC_Items.Add(new FC_Items());

            return arrFC_Items;
        }
        public  ArrayList RetrieveByItemTypeID(int itemtypeID)
        {
            string strSQL = "SELECT * from fc_items where rowstatus>-1 and itemtypeID=" + itemtypeID + " order by id desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrFC_Items = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrFC_Items.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrFC_Items.Add(new FC_Items());

            return arrFC_Items;
        }

        public ArrayList RetrieveByKSWIP(string thnbln,string awalQty)
        {
            string strSQL = "select distinct * from FC_Items where ID in " +
                "( select distinct  itemid0 from ( "+
                "select distinct itemid0 from vw_kartustockwip where LEFT(convert(char(8), tanggal,112),6)='" + thnbln +
                "' union all select distinct itemid from t1_saldoperlokasi where lokid not in(select ID from fc_lokasi where lokasi='p99') and  "+
                "saldo>0 and thnbln='" + thnbln + "'"+
                " ) as A) order by tebal,itemtypeID,partno ";
            //string strSQL = "select * from FC_Items where itemtypeID=1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrFC_Items = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrFC_Items.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrFC_Items.Add(new FC_Items());

            return arrFC_Items;
        }
        public ArrayList RetrieveByKSLari(string thnbln, string awalQty)
        {
            string strSQL = "select  distinct * from FC_Items where ID in " +
                "( select distinct  itemid0 from ( " +
                "select distinct itemid0 from vw_kartustockwip2 where LEFT(convert(char(8), tanggal,112),6)='" + thnbln +
                "' union all select distinct itemid from t1_saldoperlokasi where lokid in (select ID from fc_lokasi where lokasi='p99') and saldo>0 and thnbln='" + thnbln + "'" +
                " ) as A) order by tebal,itemtypeID,partno ";
            //string strSQL = "select * from FC_Items where itemtypeID=1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrFC_Items = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrFC_Items.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrFC_Items.Add(new FC_Items());

            return arrFC_Items;
        }
        public ArrayList RetrieveByItemIDMkt(int itemtypeID,int itemidmkt)
        {
            string strSQL = "SELECT * from fc_items where rowstatus>-1 and itemtypeID=" + itemtypeID +
                " and id in(select itemidfc from FC_LinkItemMkt where itemidmkt=" + itemidmkt + ") order by partno";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrFC_Items = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrFC_Items.Add(GenerateObject(sqlDataReader));
                }
            }
            //else
            //    arrFC_Items.Add(new FC_Items());

            return arrFC_Items;
        }

        public FC_Items RetrieveByItemID(int ItemID)
        {
            string strSQL = "SELECT * from fc_items where rowstatus>-1 and ItemID=" + ItemID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new FC_Items();
        }

        public FC_Items RetrieveByPartNo(string PartNo)
        {
            string strSQL = "SELECT top 1 * from fc_items where  Partno='" + PartNo + "' order by rowstatus desc ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new FC_Items();
        }

        public FC_Items RetrievePartnoStandar(string jenis, decimal tebal, int lebar, int panjang)
        {
            string strSQL = "SELECT * from T3_ProdukStd where RowStatus > -1 and Jenis='" + jenis + "' and tebal=" + tebal.ToString().Replace(',','.') + " and lebar=" + lebar + " and panjang=" + panjang + "";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectStd(sqlDataReader);
                }
            }
            return new FC_Items();
        }

        public ArrayList RetrieveByPartNo1(string PartNo)
        {
            string strSQL = "SELECT * from fc_items where  Partno='" + PartNo + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrFC_Items = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrFC_Items.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrFC_Items.Add(new FC_Items());

            return arrFC_Items;
        }
        public FC_Items RetrieveByPartNoIn(string PartNo)
        {
            string strSQL = "SELECT * from fc_items where rowstatus>-1 and  Partno='" + PartNo + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new FC_Items();
        }
        public ArrayList RetrieveByPartNodestacking(string PartNo)
        {
            string strSQL = "SELECT * from fc_items where rowstatus>-1 and itemtypeid=1 and PartNo like '%-1-%' and rowstatus >-1 order by "+
                "SUBSTRING(partno,1,3),tebal,Lebar,Panjang ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrFC_Items = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrFC_Items.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrFC_Items.Add(new FC_Items());

            return arrFC_Items;
        }
        public ArrayList RetrieveByTebal()
        {
            string strSQL = "SELECT distinct tebal from fc_items where rowstatus>-1 order by tebal";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrFC_Items = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrFC_Items.Add(GenerateObjectTebal(sqlDataReader));
                }
            }
            else
                arrFC_Items.Add(new FC_Items());

            return arrFC_Items;
        }
		
		public ArrayList RetrieveByStockBJ(string  periode)
        {
            string strSQL = "select distinct * from (SELECT * from fc_items where "+
                    "ID in (select distinct itemid from T3_Rekap where rowstatus>-1 and LEFT(convert(varchar,TglTrans,112),6) ='" + periode + "')  " +
                    //"and partno not like '%-P-%' "+
                    "and partno like '%-3-%' or partno like '%-M-%' "+
                    "union "+
                    "SELECT * from fc_items where  "+
                    "ID in (select distinct itemid from T3_AdjustDetail B,T3_Adjust A where A.ID=B.AdjustID and B.rowstatus>-1 and  "+
                    "LEFT(convert(varchar,A.AdjustDate ,112),6) ='" + periode + "') "+
                    //"and partno not like '%-P-%' "+
                    "and partno like '%-3-%' or partno like '%-M-%' " +
                    "union " +
                    "SELECT * from fc_items where  "+
                    "ID in (select distinct itemid from T3_KirimDetail where rowstatus>-1 and LEFT(convert(varchar,TglTrans,112),6) ='" + periode + "') "+
                    //"and partno not like '%-P-%'  "+
                    "and partno like '%-3-%' or partno like '%-M-%' " +
                    "union " +
                    "SELECT * from fc_items where  "+
                    "ID in (select distinct itemid from T3_Retur where rowstatus>-1 and LEFT(convert(varchar,TglTrans,112),6) ='" + periode + "') "+
                    //"and partno not like '%-P-%' "+
                    "and partno like '%-3-%' or partno like '%-M-%' " +
                    ") as A order by PartNo ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrFC_Items = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrFC_Items.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrFC_Items.Add(new FC_Items());

            return arrFC_Items;
        }
		
        public ArrayList RetrieveByStockBJ0(string  periode)
        {
            string strSQL = "select distinct * from (SELECT * from fc_items where "+
                    "ID in (select distinct itemid from T3_Rekap where rowstatus>-1 and LEFT(convert(varchar,TglTrans,112),6) ='" + periode + "')  " +
                    "and partno not like '%-P-%' union "+
                    "SELECT * from fc_items where  "+
                    "ID in (select distinct itemid from T3_AdjustDetail B,T3_Adjust A where A.ID=B.AdjustID and B.rowstatus>-1 and  "+
                    "LEFT(convert(varchar,A.AdjustDate ,112),6) ='" + periode + "') and partno not like '%-P-%' union " +
                    "SELECT * from fc_items where  "+
                    "ID in (select distinct itemid from T3_KirimDetail where rowstatus>-1 and LEFT(convert(varchar,TglTrans,112),6) ='" + periode + "') and partno not like '%-P-%'  union " +
                    "SELECT * from fc_items where  "+
                    "ID in (select distinct itemid from T3_Retur where rowstatus>-1 and LEFT(convert(varchar,TglTrans,112),6) ='" + periode + "') and partno not like '%-P-%' ) as A order by PartNo ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrFC_Items = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrFC_Items.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrFC_Items.Add(new FC_Items());

            return arrFC_Items;
        }
        public ArrayList RetrieveByStockBP(string periode)
        {
            string strSQL = "select distinct * from (SELECT * from fc_items where " +
                    "ID in (select distinct itemid from T3_Rekap where rowstatus>-1 and LEFT(convert(varchar,TglTrans,112),6) ='" + periode + "')  " +
                    "and partno like '%-P-%' union " +
                    "SELECT * from fc_items where  " +
                    "ID in (select distinct itemid from T3_AdjustDetail B,T3_Adjust A where A.ID=B.AdjustID and B.rowstatus>-1 and  " +
                    "LEFT(convert(varchar,A.AdjustDate ,112),6) ='" + periode + "') and partno like '%-P-%' union " +
                    "SELECT * from fc_items where  " +
                    "ID in (select distinct itemid from t3_serah where qty>0) and partno like '%-P-%' union " +
                    "SELECT * from fc_items where  " +
                    "ID in (select distinct itemid from T3_KirimDetail where rowstatus>-1 and LEFT(convert(varchar,TglTrans,112),6) ='" + periode + "') and partno like '%-P-%'union " +
                    "SELECT * from fc_items where  " +
                    "ID in (select distinct itemid from T3_Retur where rowstatus>-1 and LEFT(convert(varchar,TglTrans,112),6) ='" + periode + "') and partno like '%-P-%' ) as A order by PartNo ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrFC_Items = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrFC_Items.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrFC_Items.Add(new FC_Items());

            return arrFC_Items;
        }
        public ArrayList RetrieveByStockBS(string periode)
        {
            string strSQL = "select distinct * from (SELECT * from fc_items where " +
                    "ID in (select distinct itemid from T3_Rekap where rowstatus>-1 and LEFT(convert(varchar,TglTrans,112),6) ='" + periode + "')  " +
                    "and partno like '%-S-%' union " +
                    "SELECT * from fc_items where  " +
                    "ID in (select distinct itemid from T3_AdjustDetail B,T3_Adjust A where A.ID=B.AdjustID and B.rowstatus>-1 and  " +
                    "LEFT(convert(varchar,A.AdjustDate ,112),6) ='" + periode + "') and partno like '%-S-%' union " +
                    "SELECT * from fc_items where  " +
                    "ID in (select distinct itemid from t3_serah where qty>0) and partno like '%-S-%' union " +
                    "SELECT * from fc_items where  " +
                    "ID in (select distinct itemid from T3_KirimDetail where rowstatus>-1 and LEFT(convert(varchar,TglTrans,112),6) ='" + periode + "') and partno like '%-P-%'union " +
                    "SELECT * from fc_items where  " +
                    "ID in (select distinct itemid from T3_Retur where rowstatus>-1 and LEFT(convert(varchar,TglTrans,112),6) ='" + periode + "') and partno like '%-S-%' ) as A order by PartNo ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrFC_Items = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrFC_Items.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrFC_Items.Add(new FC_Items());

            return arrFC_Items;
        }
        public ArrayList RetrieveByStockBJpartno(string periode, string partno)
        {
            string strSQL = "SELECT * from fc_items where partno like '%" + partno + "%' and ID in (select distinct itemid "+
                "from t3_rekap where rowstatus>-1 and LEFT(convert(varchar,TglTrans,112),6) ='" + periode + "' union select distinct itemid from t3_serah where qty>0) order by partno";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrFC_Items = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrFC_Items.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrFC_Items.Add(new FC_Items());

            return arrFC_Items;
        }
        public ArrayList RetrieveByItemIDBaru(string yearperiod)
        {
            string strJenisBrg = string.Empty;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "select ID,isnull(SaldoInventoryT1.ItemID,0) as ItemID,inv.GroupID,inv.itemtypeid,RowStatus " +
                "from (select ID,GroupID,itemtypeid,RowStatus from fc_items where RowStatus>-1) as Inv " +
                "left join SaldoInventoryT1 on SaldoInventoryT1.ItemID=Inv.ID and SaldoInventoryT1.yearperiod=" + yearperiod + " where ItemID is null";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrFC_Items = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrFC_Items.Add(GenerateObjectBaru(sqlDataReader));
                }
            }
            else
                arrFC_Items.Add(new FC_Items());
            return arrFC_Items;
        }
        public ArrayList RetrieveByItemIDBaruT1(string yearperiod)
        {
            string strJenisBrg = string.Empty;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "select ID,isnull(SaldoInventoryT1.ItemID,0) as ItemID,inv.GroupID,inv.itemtypeid,RowStatus " +
                "from (select ID,GroupID,itemtypeid,RowStatus from fc_items where itemtypeid=1 and RowStatus>-1 union select ID,GroupID,itemtypeid,RowStatus from fc_items where (SUBSTRING(partno,4,3)='-1-' or SUBSTRING(partno,5,3)='-1-' or SUBSTRING(partno,6,3)='-1-') and RowStatus>-1) as Inv " +
                "left join SaldoInventoryT1 on SaldoInventoryT1.ItemID=Inv.ID and SaldoInventoryT1.yearperiod=" + yearperiod + " where ItemID is null";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrFC_Items = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrFC_Items.Add(GenerateObjectBaru(sqlDataReader));
                }
            }
            else
                arrFC_Items.Add(new FC_Items());
            return arrFC_Items;
        }
        public ArrayList RetrieveByItemIDBaruBJ(string yearperiod)
        {
            string strJenisBrg = string.Empty;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "select inv.ID,isnull(SaldoInventoryBJ.ItemID,0) as ItemID,inv.GroupID,inv.itemtypeid,inv.RowStatus " +
                "from (select distinct I.ID,I.GroupID,I.itemtypeid,I.RowStatus from fc_items I) as Inv " +
                "left join SaldoInventoryBJ on SaldoInventoryBJ.ItemID=Inv.ID and SaldoInventoryBJ.yearperiod=" + yearperiod + " where ItemID is null";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrFC_Items = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrFC_Items.Add(GenerateObjectBaru(sqlDataReader));
                }
            }
            else
                arrFC_Items.Add(new FC_Items());
            return arrFC_Items;
        }
        public ArrayList RetrieveByGroupMarketing(string groupM,string thbln)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "select distinct * from ( " +
                "select case when GroupID>0 then (select [Groups] from t3_groupM where ID=A.groupID) " +
                "else  'NOGroup' end GroupM,Tebal,Lebar,Panjang from FC_Items A where SUBSTRING(partno,5,1)='3' and id in  " +
                "(select itemid from T3_Rekap where LEFT(CONVERT(varchar,createdtime,112),6)='" + thbln + 
                "' and process not like'ex%')) as A1 where GroupM='" + groupM + "' order by Tebal,Lebar,Panjang";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrFC_Items = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrFC_Items.Add(GenerateObjectGroup(sqlDataReader));
                }
            }
            else
                arrFC_Items.Add(new FC_Items());
            return arrFC_Items;
        }
        public ArrayList RetrieveforSaldo(string thbl)
        {
            string strJenisBrg = string.Empty;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string username = ((Users)HttpContext.Current.Session["Users"]).UserName.Trim();
            string strsql = 
                "declare @thnbln varchar(6) " +
                "declare @thnbln0 varchar(6) " +
                "declare @tgl datetime " +
                "declare @users varchar(25) " +
                "set @thnbln='" + thbl + "' " +
                "set @users='" + username +"' " +
                "set @tgl=CAST( (@thnbln+ '01') as datetime) " +
                "set @tgl= DATEADD(month,-1,@tgl) " +
                "set @thnbln0=LEFT(convert(char,@tgl,112),6) " +
                "delete t1_saldoListPlank where thnbln=@thnbln   " +
                "insert into t1_saldoListPlank   " +
                "select * from ( select @thnbln thnbln,ItemID,sum(Saldo)Saldo,process, 0 rowstatus,GETDATE() CreatedTime,@users CreatedBy, " +
                "GETDATE() LastModifiedTime, @users LastModifiedBy,itemid0 from (  select itemid,SUM(qty)Saldo,process,ItemID0   " +
                "from vw_KartuStockListplank A where   LEFT(convert(char,tanggal,112),6)=@thnbln and A.process not like'%adj%' group by ItemID0,ItemID,process   " +
                " " +
                "union all  " +
                "select itemid,SUM(qty)Saldo,case when A.process='Adjini99' then 'i99' when A.process='Adjouti99' then 'i99'  " +
                "when A.process='Adjinruningsaw' then 'runingsaw' when A.process='Adjoutruningsaw' then 'runingsaw' when A.process='Adjinbevel'  " +
                "then 'bevel' when A.process='Adjoutbevel' then 'bevel' when A.process='Adjinstraping' then 'straping' when A.process='Adjoutstraping'  " +
                "then 'straping'  end process,ItemID0  from vw_KartuStockListplank A where LEFT(convert(char,tanggal,112),6)=@thnbln  and A.process like'%adj%'  " +
                "group by ItemID0,ItemID,process  " +
                "union all   " +
                "select itemid,saldo,process,ItemID0  from t1_SaldoListplank A where thnbln=@thnbln0   ) as L group by  " +
                "ItemID,itemid0,process ) LL  " +
                " " +
                "delete t1_SaldoListplankR1 where thnbln=@thnbln insert into t1_SaldoListplankR1   " +
                "select * from ( select  @thnbln thnbln,ItemID, sum(Saldo)Saldo,process, 0 rowstatus,GETDATE() CreatedTime,@users CreatedBy,  " +
                "GETDATE() LastModifiedTime,@users LastModifiedBy,itemid0 from (  select itemid,SUM(qty)Saldo,process,ItemID0   " +
                "from vw_KartuStockListplankR1 A where   LEFT(convert(char,tanggal,112),6)=@thnbln and A.process not like'%adj%' group by ItemID0,ItemID,process        " +
                "union all   " +
                "select itemid,SUM(qty)Saldo,case when A.process='Adjini99R1' then 'i99' when A.process='Adjouti99R1' then 'i99'  " +
                "when A.process='AdjinRSR1' then 'runingsaw' when A.process='AdjoutRSR1' then 'runingsaw' when A.process='AdjinbevelR1' then 'bevel'  " +
                "when A.process='AdjoutbevelR1' then 'bevel' when A.process='AdjinstrapingR1' then 'strapingR1' when A.process='AdjoutstrapingR1' then 'strapingR1'   " +
                "end process,ItemID0  from vw_KartuStockListplankR1 A where LEFT(convert(char,tanggal,112),6)=@thnbln  and A.process like'%adj%'  " +
                "group by ItemID0,ItemID,process      " +
                "union all       " +
                "select itemid,Saldo,process,ItemID0  from t1_SaldoListplankR1 A where thnbln=@thnbln0   ) as L  group by ItemID,itemid0,process ) LL  " +
                "where Saldo>=0 order by process  " +
                " " +
                "delete t1_SaldoListplankR2 where thnbln=@thnbln   " +
                "insert into t1_SaldoListplankR2   " +
                "select * from ( select   @thnbln thnbln,ItemID,sum(Saldo)Saldo,process,  0 rowstatus,GETDATE() CreatedTime,@users CreatedBy, " +
                "GETDATE() LastModifiedTime,@users LastModifiedBy,itemid0 from ( select itemid,SUM(qty)Saldo,process,ItemID0  from vw_KartuStockListplankR2 A where       LEFT(convert(char,tanggal,112),6)=@thnbln  and A.process not like'%adj%' group by ItemID0,ItemID,process        " +
                "union all   " +
                "select itemid,SUM(qty)Saldo,case when A.process='Adjini99R2' then 'i99' when A.process='Adjouti99R2' then 'i99' when A.process='AdjinRSR2'  " +
                "then 'runingsaw' when A.process='AdjoutRSR2' then 'runingsaw' when A.process='AdjinbevelR2' then 'bevel' when A.process='AdjoutbevelR2'  " +
                "then 'bevel' when A.process='AdjinstrapingR2' then 'strapingR2' when A.process='AdjoutstrapingR2' then 'strapingR2'  end process,ItemID0   " +
                "from vw_KartuStockListplankR2 A where LEFT(convert(char,tanggal,112),6)=@thnbln  and A.process like'%adj%' group by ItemID0,ItemID,process      " +
                "union all " +
                "select itemid,Saldo,process,ItemID0  from t1_SaldoListplankR2 A where thnbln=@thnbln0   ) as L  group by ItemID,itemid0,process ) LL where Saldo>=0  " +
                "order by process  ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrFC_Items = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrFC_Items.Add(GenerateObjectSaldo(sqlDataReader));
                }
            }
            else
                arrFC_Items.Add(new FC_Items());
            return arrFC_Items;
        }
        public ArrayList RetrieveforSaldoBJ(string thbl)
        {
            string strJenisBrg = string.Empty;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //string strsql = "select ID,SUM(Penerimaan)-SUM(pengeluaran) as stock,0 as price,groupid,itemtypeid from ( " +
            //                "SELECT B.ID,B.groupid,B.itemtypeid, sum(A.Qtyin) as Penerimaan,sum(A.Qtyout) as Pengeluaran,ISNULL(AVG(A.hpp),0)as hpp " +
            //                "FROM FC_Items AS B INNER JOIN T3_Rekap  AS A ON B.ID = A.ItemID  where  A.process not like 'ex%' and A.Status>-1 and  LEFT(convert(varchar,A.createdtime,112),6) ='" + thbl + "' " +
            //                " group by  B.itemtypeID,B.GroupID,B.ID union " +
            //                "SELECT B.ID,B.groupid,B.itemtypeid,0 as Penerimaan, isnull(sum(A.Qty),0) as Pengeluaran,ISNULL(AVG(A.hpp),0)as hpp " +
            //                "FROM T3_KirimDetail  AS A INNER JOIN FC_Items AS B ON A.ItemID = B.ID " +
            //                "where   A.KirimID>0 and A.rowStatus>-1 and LEFT(convert(varchar,A.createdtime,112),6) ='" + thbl + "' group by  B.itemtypeID,B.GroupID,B.ID union " +
            //                "SELECT B.ID,B.groupid,B.itemtypeid,isnull(sum(A.Qty),0) as Penerimaan,0 as Pengeluaran,ISNULL(AVG(A.hpp),0)as hpp " +
            //                "FROM T3_Retur  AS A INNER JOIN FC_Items AS B ON A.ItemID = B.ID   " +
            //                "where  A.rowStatus>-1 and LEFT(convert(varchar,A.createdtime,112),6) ='" + thbl + "' group by  B.itemtypeID,B.GroupID,B.ID " +
            //                "union " +
            //                "SELECT B.ID,B.groupid,B.itemtypeid,isnull(sum(A.Qtyin),0) as Penerimaan,isnull(sum(A.QtyOut ),0) as Pengeluaran,ISNULL(AVG(A.hpp),0)as hpp FROM T3_AdjustDetail   AS A INNER JOIN  " +
            //                "FC_Items AS B ON A.ItemID = B.ID  inner join T3_Adjust as C on C.id=A.adjustID where  A.apv>0 and A.rowStatus>-1 and LEFT(convert(varchar,C.createdtime,112),6) ='" + thbl + "' group by  B.itemtypeID,B.GroupID,B.ID " +
            //                ") as a group by  itemtypeID,GroupID,ID";
            string strsql =string.Empty;
            int depo = ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID;
            string periode = new Inifile(HttpContext.Current.Server.MapPath("~/App_Data/Factory.ini")).Read("AwalPeriode" + depo.ToString(), "Report");
            if (Convert.ToInt32(thbl) <= Convert.ToInt32(periode))
                strsql = "SELECT  isnull(sum(A.qty),0) AS stock, B.ID, B.GroupID, B.ItemTypeID,0 as price  FROM  vw_KartuStockBJ AS A INNER JOIN FC_Items AS B ON A.ItemID = B.ID " +
                         "WHERE  (LEFT(CONVERT(varchar, A.tanggal, 112), 6) = '" + thbl + "') GROUP BY B.ID, B.GroupID, B.ItemTypeID";
            else
                strsql = "SELECT  isnull(sum(A.qty),0)  AS stock, B.ID, B.GroupID, B.ItemTypeID,0 as price  FROM  vw_KartuStockBJNew AS A INNER JOIN FC_Items AS B ON A.ItemID = B.ID " +
                         "WHERE  (LEFT(CONVERT(varchar, A.tanggal, 112), 6) = '" + thbl + "') GROUP BY B.ID, B.GroupID, B.ItemTypeID";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrFC_Items = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrFC_Items.Add(GenerateObjectSaldo(sqlDataReader));
                }
            }
            else
                arrFC_Items.Add(new FC_Items());
            return arrFC_Items;
        }
        public ArrayList GetKode()
        {
            string strJenisBrg = string.Empty;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "select distinct SUBSTRING(partno,1,3) kode from fc_items where RowStatus>-1 order by kode";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrFC_Items = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrFC_Items.Add(GenerateObjectKode(sqlDataReader));
                }
            }
            else
                arrFC_Items.Add(new FC_Items());
            return arrFC_Items;
        }
        public ArrayList GetSisi()
        {
            string strJenisBrg = string.Empty;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "select distinct rtrim(SUBSTRING(partno,18,3)) as sisi from FC_Items where ItemTypeID=3 and rtrim(SUBSTRING(partno,18,3))<>' ' order by rtrim(SUBSTRING(partno,18,3))";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrFC_Items = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrFC_Items.Add(GenerateObjectSisi(sqlDataReader));
                }
            }
            else
                arrFC_Items.Add(new FC_Items());
            return arrFC_Items;
        }
        public int Check(string PartNo)
        {
            string strSQL = "SELECT * from fc_items where Partno='" + PartNo + "' and rowstatus>-1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            int ada = 0;
            if (sqlDataReader.HasRows)
            {
                ada = 1;
            }
            return ada;
        }
        public ArrayList RetrieveByUkuran(string periode1,string periode2,string vwstock)
        {
            string strJenisBrg = string.Empty;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "select distinct * from (select distinct  tebal,Lebar,Panjang,RTRIM(CAST(CAST(Tebal AS decimal(18, 1)) AS CHAR))+ ' X ' " +
                "+ RTRIM(CAST(CAST(Lebar AS decimal(18)) AS CHAR)) + ' X ' + RTRIM(CAST(CAST(Panjang AS decimal(18)) AS CHAR)) AS ukuran "+
                "from fc_items where rowstatus>-1) as A where ukuran in (select distinct ukuran from " + vwstock + " where Periode in ('" + 
                periode1 + "','" + periode2 + "') and Qty>0) order by tebal,Lebar,Panjang ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrFC_Items = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrFC_Items.Add(GenerateObjectUkuran(sqlDataReader));
                }
            }
            else
                arrFC_Items.Add(new FC_Items());
            return arrFC_Items;
        }
        public ArrayList RetrieveByItems(string ukuran)
        {
            string strJenisBrg = string.Empty;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "select distinct Items ,tebal,Lebar,Panjang from ( "+
                "select distinct tebal,Lebar,Panjang,partno,RTRIM(CAST(CAST(Tebal AS decimal(18, 1)) AS CHAR))+ ' X ' + RTRIM(CAST(CAST(Lebar AS decimal(18)) AS CHAR)) +  "+
                "' X ' + RTRIM(CAST(CAST(Panjang AS decimal(18)) AS CHAR)) as Ukuran,SUBSTRING(PartNo, CHARINDEX('-1-', PartNo, 0) + 3, 21) as Items  "+
                "from fc_items where rowstatus>-1 and PartNo like '%-1-%' "+
                "union "+
                "select distinct tebal,Lebar,Panjang,partno,RTRIM(CAST(CAST(Tebal AS decimal(18, 1)) AS CHAR))+ ' X ' + RTRIM(CAST(CAST(Lebar AS decimal(18)) AS CHAR)) +  "+
                "' X ' + RTRIM(CAST(CAST(Panjang AS decimal(18)) AS CHAR)) as Ukuran,SUBSTRING(PartNo, CHARINDEX('-3-', PartNo, 0) + 3, 21) as Items  "+
                "from fc_items where rowstatus>-1 and PartNo like '%-3-%' "+
                "union "+
                "select distinct tebal,Lebar,Panjang,partno,RTRIM(CAST(CAST(Tebal AS decimal(18, 1)) AS CHAR))+ ' X ' + RTRIM(CAST(CAST(Lebar AS decimal(18)) AS CHAR)) +  "+
                "' X ' + RTRIM(CAST(CAST(Panjang AS decimal(18)) AS CHAR)) as Ukuran,SUBSTRING(PartNo, CHARINDEX('-W-', PartNo, 0) + 3, 21) as Items  "+
                "from fc_items where rowstatus>-1 and PartNo like '%-W-%' "+
                "union "+
                "select distinct tebal,Lebar,Panjang,partno,RTRIM(CAST(CAST(Tebal AS decimal(18, 1)) AS CHAR))+ ' X ' + RTRIM(CAST(CAST(Lebar AS decimal(18)) AS CHAR)) +  " +
                "' X ' + RTRIM(CAST(CAST(Panjang AS decimal(18)) AS CHAR)) as Ukuran,SUBSTRING(PartNo, CHARINDEX('-M-', PartNo, 0) + 3, 21) as Items  " +
                "from fc_items where rowstatus>-1 and PartNo like '%-M-%' " +
                "union " +
                "select distinct tebal,Lebar,Panjang,partno,RTRIM(CAST(CAST(Tebal AS decimal(18, 1)) AS CHAR))+ ' X ' + RTRIM(CAST(CAST(Lebar AS decimal(18)) AS CHAR)) +  "+
                "' X ' + RTRIM(CAST(CAST(Panjang AS decimal(18)) AS CHAR)) as Ukuran,SUBSTRING(PartNo, CHARINDEX('-P-', PartNo, 0) + 3, 21) as Items  "+
                "from fc_items where rowstatus>-1 and PartNo like '%-P-%' "+
                "union "+
                "select distinct tebal,Lebar,Panjang,partno,RTRIM(CAST(CAST(Tebal AS decimal(18, 1)) AS CHAR))+ ' X ' + RTRIM(CAST(CAST(Lebar AS decimal(18)) AS CHAR)) +  "+
                "' X ' + RTRIM(CAST(CAST(Panjang AS decimal(18)) AS CHAR)) as Ukuran,SUBSTRING(PartNo, CHARINDEX('-S-', PartNo, 0) + 3, 21) as Items  " +
                "from fc_items where rowstatus>-1 and PartNo like '%-S-%') as A where ukuran='" + ukuran + "' order by tebal,Lebar,Panjang";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrFC_Items = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrFC_Items.Add(GenerateObjectItems(sqlDataReader));
                }
            }
            else
                arrFC_Items.Add(new FC_Items());
            return arrFC_Items;
        }
        public ArrayList RetrieveByPelarian()
        {
            string strSQL = "select * from fc_items where ID in(select distinct ItemID0  from t1_serah where qtyin>qtyout and sfrom ='lari' ) order by PartNo ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrFC_Items = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrFC_Items.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrFC_Items.Add(new FC_Items());

            return arrFC_Items;
        }
        public ArrayList RetrieveByListPlank(string tblName)
        {
            tblName = tblName.ToUpper();
            arrFC_Items = new ArrayList();
            if (tblName == "")
            {
                arrFC_Items.Add(new FC_Items());
                return arrFC_Items;
            }
            string rowstatus=string.Empty;
            if (tblName == "T1_SERAH" || tblName == "T1_LISTPLANK" || tblName == "T1_LR1_LISTPLANK" 
                || tblName == "T1_LR2_LISTPLANK" || tblName == "T1_LR3_LISTPLANK")
                rowstatus = "[status]";
            else
                rowstatus = "[rowstatus]";
            string strSQL = "select * from fc_items where ID in(select distinct ItemID from "+tblName+" where qtyin>qtyout and " + rowstatus + ">-1 ) order by PartNo ";
            if (tblName == "T1_SERAH")
                strSQL = "select * from fc_items where ID in(select distinct ItemID from " + tblName + " A inner join fc_items I on A.itemid=I.id where I.tebal in (8,9) and A.qtyin>A.qtyout and A.sfrom='lari' and A." + rowstatus + ">-1 ) order by PartNo ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrFC_Items = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrFC_Items.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrFC_Items.Add(new FC_Items());

            return arrFC_Items;
        }
        public ArrayList RetrieveByUkuranPelarian()
        {
            string strSQL = "select distinct left(cast(B.Lebar as char),4)+ ' X ' +left(cast(B.Panjang as char),4) Ukuran " +
                "from T1_Serah A inner join FC_Items B on A.ItemID =B.ID where SFrom='lari' and QtyIn> QtyOut";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrFC_Items = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrFC_Items.Add(GenerateObjectUkuran(sqlDataReader));
                }
            }
            else
                arrFC_Items.Add(new FC_Items());

            return arrFC_Items;
        }
        public FC_Items GenerateObjectUkuran(SqlDataReader sqlDataReader)
        {
            objFC_Items = new FC_Items();
            objFC_Items.Ukuran  = sqlDataReader["Ukuran"].ToString().Trim();
            return objFC_Items;
        }
        public FC_Items GenerateObjectItems(SqlDataReader sqlDataReader)
        {
            objFC_Items = new FC_Items();
            objFC_Items.Items = sqlDataReader["Items"].ToString().Trim();
            return objFC_Items;
        }
        public FC_Items GenerateObjectSisi(SqlDataReader sqlDataReader)
        {
            objFC_Items = new FC_Items();
            objFC_Items.Sisi = sqlDataReader["Sisi"].ToString().Trim();
            return objFC_Items;
        }
        public FC_Items GenerateObjectKode(SqlDataReader sqlDataReader)
        {
            objFC_Items = new FC_Items();
            objFC_Items.Kode  = sqlDataReader["Kode"].ToString().Trim() ;
            return objFC_Items;
        }
        public FC_Items GenerateObjectTebal(SqlDataReader sqlDataReader)
        {
            objFC_Items = new FC_Items();
            objFC_Items.Tebal  = Convert.ToDecimal(sqlDataReader["tebal"]);
            return objFC_Items;
        }
        public FC_Items GenerateObjectBaru(SqlDataReader sqlDataReader)
        {
            objFC_Items = new FC_Items();
            objFC_Items.ID = Convert.ToInt32(sqlDataReader["id"]);
            objFC_Items.ItemID = Convert.ToInt32(sqlDataReader["itemid"]);
            objFC_Items.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objFC_Items.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"]);
            return objFC_Items;
        }
        public FC_Items GenerateObjectSaldo(SqlDataReader sqlDataReader)
        {
            objFC_Items = new FC_Items();
            objFC_Items.ItemID  = Convert.ToInt32(sqlDataReader["ID"]);
            objFC_Items.Stock = Convert.ToInt32(sqlDataReader["stock"]);
            objFC_Items.Price  = Convert.ToInt32(sqlDataReader["price"]);
            objFC_Items.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objFC_Items.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"]);
            return objFC_Items;
        }
        public FC_Items GenerateObject(SqlDataReader sqlDataReader)
        {
            objFC_Items = new FC_Items();
            objFC_Items.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objFC_Items.GroupID = Convert.ToInt32(sqlDataReader["groupID"]);
            objFC_Items.ItemDesc = sqlDataReader["ItemDesc"].ToString();
            objFC_Items.Partno = sqlDataReader["partno"].ToString();
            objFC_Items.Kode  = sqlDataReader["kode"].ToString().Trim();
            objFC_Items.Tebal = Convert.ToDecimal(sqlDataReader["Tebal"]);
            objFC_Items.Lebar = Convert.ToInt32(sqlDataReader["Lebar"]);
            objFC_Items.Panjang = Convert.ToInt32(sqlDataReader["Panjang"]);
            objFC_Items.Volume = (sqlDataReader["Volume"] ==DBNull.Value)?0:Convert.ToDecimal(sqlDataReader["Volume"]);
            objFC_Items.Stock = (sqlDataReader["stock"] == DBNull.Value) ? 0 : Convert.ToInt32(sqlDataReader["stock"]);
            return objFC_Items;
        }

        public FC_Items GenerateObjectStd(SqlDataReader sqlDataReader)
        {
            objFC_Items = new FC_Items();
            objFC_Items.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objFC_Items.Kode = sqlDataReader["Jenis"].ToString().Trim();
            objFC_Items.Tebal = Convert.ToDecimal(sqlDataReader["Tebal"]);
            objFC_Items.Lebar = Convert.ToInt32(sqlDataReader["Lebar"]);
            objFC_Items.Panjang = Convert.ToInt32(sqlDataReader["Panjang"]);
            return objFC_Items;
        }
        public FC_Items GenerateObjectGroup(SqlDataReader sqlDataReader)
        {
            objFC_Items = new FC_Items();
            objFC_Items.Partno = sqlDataReader["groupm"].ToString();
            objFC_Items.Tebal = Convert.ToDecimal(sqlDataReader["Tebal"]);
            objFC_Items.Lebar = Convert.ToInt32(sqlDataReader["Lebar"]);
            objFC_Items.Panjang = Convert.ToInt32(sqlDataReader["Panjang"]);
            return objFC_Items;
        }
    }
}
