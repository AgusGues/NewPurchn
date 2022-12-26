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
using Dapper;

namespace BusinessFacade
{
    public class FC_ItemsNFFacade : AbstractFacade
    {
        private FC_Items objFC_Items = new FC_Items();
        private List<SqlParameter> sqlListParam;

        public FC_ItemsNFFacade()
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
                sqlListParam.Add(new SqlParameter("@Kode", objFC_Items.Kode));
                sqlListParam.Add(new SqlParameter("@Tebal", objFC_Items.Tebal));
                sqlListParam.Add(new SqlParameter("@Panjang", objFC_Items.Panjang));
                sqlListParam.Add(new SqlParameter("@Lebar", objFC_Items.Lebar));
                sqlListParam.Add(new SqlParameter("@Volume", objFC_Items.Volume));
                sqlListParam.Add(new SqlParameter("@Partno", objFC_Items.Partno));
                sqlListParam.Add(new SqlParameter("@itemdesc", objFC_Items.ItemDesc));
                sqlListParam.Add(new SqlParameter("@Stock", objFC_Items.Stock));
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
                sqlListParam.Add(new SqlParameter("@ItemtypeID", objFC_Items.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@Kode", objFC_Items.Kode));
                sqlListParam.Add(new SqlParameter("@Tebal", objFC_Items.Tebal));
                sqlListParam.Add(new SqlParameter("@Panjang", objFC_Items.Panjang));
                sqlListParam.Add(new SqlParameter("@Lebar", objFC_Items.Lebar));
                sqlListParam.Add(new SqlParameter("@Volume", objFC_Items.Volume));
                sqlListParam.Add(new SqlParameter("@Partno", objFC_Items.Partno));
                sqlListParam.Add(new SqlParameter("@itemdesc", objFC_Items.ItemDesc));
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
            throw new NotImplementedException();
        }


        public static FC_ItemsNF CekPartno(string partno)
        {
            FC_ItemsNF AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "SELECT top 1 * from fc_items where rowstatus > -1 and Partno='" + partno + "' order by Partno asc ";
                    AllData = connection.QueryFirstOrDefault<FC_ItemsNF>(query);
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }


        public static List<FC_ItemsNF> CekListPartno(string partno)
        {
            List<FC_ItemsNF> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "SELECT top 1 * from fc_items where rowstatus > -1 and Partno='" + partno + "' order by Partno asc ";
                    AllData = connection.Query<FC_ItemsNF>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<FC_ItemsNF> GetListPartnoT1(string partno)
        {
            List<FC_ItemsNF> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "SELECT * FROM FC_ITEMS WHERE ROWSTATUS > -1 AND ItemTypeID = 1 AND PartNo LIKE '%-1-%' ORDER BY SUBSTRING(PARTNO, 1, 3),TEBAL,LEBAR,PANJANG ";
                    AllData = connection.Query<FC_ItemsNF>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public int GetItemIDPartno(string partno)
        {
            int itemid;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "SELECT TOP 1 ID FROM FC_ITEMS WHERE PARTNO ='" + partno.Trim() + "' AND ROWSTATUS > -1 ";
                    itemid = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {
                    itemid = 0;
                }
            }
            return itemid;
        }


        public static List<FC_ItemsNF> GetListPartnoPelarian()
        {
            List<FC_ItemsNF> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "SELECT A.Destid,A.itemID as ItemIDSer,A.LokID,isnull(C.GroupID,0) as GroupID,B.Lokasi, A.QtyIn-A.QtyOut  as QtyIn,A.QtyOut, A.ID,isnull(A.HPP,0) as HPP,C.tebal,C.lebar,C.panjang, D.tglproduksi,A.TglSerah, C.Partno,B.Lokasi as LokasiSer  FROM  T1_serah AS A INNER JOIN FC_Items AS C ON A.ItemID0 = C.ID INNER JOIN FC_Lokasi AS B ON A.LokID = B.ID left join bm_destacking D on A.destid=D.ID  left join fc_items B1 on B1.ID= D.itemID   where A.QtyIn>A.QtyOut and A.sfrom ='lari' and A.status>-1 and C.tebal in (8,9) and isnull(A.destid,0)>0  order by C.Partno,A.tglserah";
                    AllData = connection.Query<FC_ItemsNF>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<FC_ItemsNF> GetListPartnoPelarianUkuran()
        {
            List<FC_ItemsNF> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "select distinct left(cast(B.Lebar as char),4)+ ' X ' +left(cast(B.Panjang as char),4) Ukuran " +
                        "from T1_Serah A inner join FC_Items B on A.ItemID =B.ID where SFrom='lari' and QtyIn> QtyOut and status> -1";
                    AllData = connection.Query<FC_ItemsNF>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<FC_ItemsNF> GetPartnoT1(string partno)
        {
            List<FC_ItemsNF> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query;
                    query = "SELECT top 10 TRIM(PartNo) PartNo, Tebal, Panjang, Lebar, TRIM(ItemDesc) PartName, id from fc_items Where rowstatus> -1 and partno like '%-1-%' and PartNo like '" + partno + "%'";
                    AllData = connection.Query<FC_ItemsNF>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<FC_ItemsNF> GetPartnoT3(string partno)
        {
            List<FC_ItemsNF> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query;
                    query = "SELECT top 10 TRIM(PartNo) PartNo, Tebal, Panjang, Lebar, TRIM(ItemDesc) PartName, id from fc_items Where rowstatus> -1 and PartNo like '" + partno + "%'";
                    AllData = connection.Query<FC_ItemsNF>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public static List<FC_ItemsNF> GetPartnoT3byGroup(string partno, string groupid)
        {
            List<FC_ItemsNF> AllData = null;
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

                    AllData = connection.Query<FC_ItemsNF>(query).ToList();
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
