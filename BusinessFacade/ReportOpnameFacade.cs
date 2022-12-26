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
using System.Web.UI.WebControls;

namespace BusinessFacade
{
    public class ReportOpnameFacade : GRCBaseDomain
    {
        protected string strError = string.Empty;
        private GroupsPurchn objPurch = new GroupsPurchn();
        private ArrayList arrPurch;
        private List<SqlParameter> sqlListParam;

        public ReportOpnameFacade()
            : base()
        { }

        public ArrayList getGroup()
        {
            string strSQL = "select ID,groupdescription from GroupsPurchn where RowStatus > -1";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            arrPurch = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPurch.Add(GenerateObjectTes(sqlDataReader));
                }
            }
            else 

                arrPurch.Add(new GroupsPurchn());

            return arrPurch;
        }

        public string ViewLapOpname(string periodeAwal, string periodeAkhir, int groupID, string bulan, int thn)
        {

            string strSQL; string a = string.Empty;
            if (groupID == 12)
            {
                a = " asset ";
            }
            else
            {
                a = " inventory ";
            }

            string strSQL2 = "select C.ItemCode,C.ItemName,D.UOMCode as UOMDesc,(A." + bulan + "+sum(B.quantity)) as qty from SaldoInventory as A,vw_StockPurchn as B, " +
                     "Inventory as C, UOM as D where A.ItemID=B.ItemID and B.ItemID=C.ID and C.UOMID=D.ID and B.YMD between '" + periodeAwal + "' and '" + periodeAkhir + "' " +
                     "and B.GroupID=" + groupID + " and A.YearPeriod=" + thn + " and C.Aktif=1  group by B.ItemID,A." + bulan + ",C.ItemCode,C.ItemName,D.UOMCode,C.ID " +
                     "union " +
                     "select B.ItemCode,B.ItemName,C.UOMCode as UOMDesc,(A." + bulan + ") as qty from SaldoInventory as A, Inventory as B,UOM as C " +
                     "where not exists (select ItemID from vw_StockPurchn as H where A.ItemID=H.ItemID and tanggal between '" + periodeAwal + "' and '" + periodeAkhir + "') " +
                     "and A.ItemID=B.ID and B.UOMID=C.ID and A." + bulan + " > 0 and A.GroupID=" + groupID + " and A.YearPeriod=" + thn + " " +
                     "union " +
                     "select A.ItemCode,A.ItemName,C.UOMCode as UOMDesc,sum(B.quantity) as qty from Inventory as A,vw_StockPurchn as B,UOM as C where " +
                     "not exists (select ItemID from SaldoInventory as H where B.ItemID=H.ItemID  and H.YearPeriod=" + thn + " ) and A.UOMID=C.ID " +
                     "and A.ID=B.ItemID and B.tanggal between '" + periodeAwal + "' and '" + periodeAkhir + "' and B.GroupID=" + groupID + " group by A.ItemCode, " +
                     "A.ItemName,C.UOMCode order by C.ItemName asc ";
            /**
             * New Query
             * Added on : 06-10-2015
             * Author   : Beny
             */
            strSQL = "select C.ItemCode,C.ItemName,D.UOMCode as UOMDesc,(A." + bulan + "+sum(B.quantity)) as qty from SaldoInventory as A,vw_StockPurchn as B, " +
                     "" + a + " as C, UOM as D where A.ItemID=B.ItemID and B.ItemID=C.ID and C.UOMID=D.ID and A.GroupID=C.GroupID and B.YMD between '" + periodeAwal + "' and '" + periodeAkhir + "' " +
                     "and B.GroupID=" + groupID + " and A.YearPeriod=" + thn + " and C.Aktif=1  group by B.ItemID,A." + bulan + ",C.ItemCode,C.ItemName,D.UOMCode,C.ID " +
                     "union " +
                     "select B.ItemCode,B.ItemName,C.UOMCode as UOMDesc,(A." + bulan + ") as qty from SaldoInventory as A, "+a+" as B,UOM as C " +
                     "where not exists (select ItemID from vw_StockPurchn as H where A.ItemID=H.ItemID and YMD between '" + periodeAwal + "' and '" + periodeAkhir + "') " +
                     "and A.ItemID=B.ID and B.UOMID=C.ID and A." + bulan + " > 0 and A.GroupID=" + groupID + " and A.YearPeriod=" + thn + " " +
                     "union " +
                     "select A.ItemCode,A.ItemName,C.UOMCode as UOMDesc,sum(B.quantity) as qty from "+a+" as A,vw_StockPurchn as B,UOM as C where " +
                     "not exists (select ItemID from SaldoInventory as H where B.ItemID=H.ItemID  and H.YearPeriod=" + thn + " and groupID=" + groupID + ") and A.UOMID=C.ID " +
                     "and A.ID=B.ItemID and B.YMD between '" + periodeAwal + "' and '" + periodeAkhir + "' and B.GroupID=" + groupID + " group by A.ItemCode, " +
                     "A.ItemName,C.UOMCode order by C.ItemName asc "; ;
            return strSQL;

        }

        public string ViewLaporanT3(string Nama, int DeptID)
        {
            string NamaDept = string.Empty;
            string query = string.Empty;

            if (DeptID == 6)
            {
                NamaDept = "<>'FINISHING'";
                query = " and (Dept is null or Dept<>'' or Dept" + NamaDept + ") ";
            }
            else if (DeptID == 3)
            {
                NamaDept = "='FINISHING'";
                query = " and (Dept is null or Dept<>'' or Dept" + NamaDept + ") ";
            }
            else if (DeptID == 24)
            {
                query = "";
            }

            string strSQL;            
            strSQL = 
                //" select Nama,Lokasi,PartNo,SUM(qty) as Qty from T3_Serah as A " +
                //    " left join MasterStokerLink as B ON A.itemid=B.itemid inner join FC_Items as C ON A.ItemID=C.ID " +
                //    " inner join FC_Lokasi  as D ON A.LokID=D.ID left join MasterStoker as E ON B.StokerID=E.ID " +
                //    " where A.RowStatus > -1 and A.Qty > 0 and E.Nama = '" + Nama + "'" +                    
                //    //" and (D.Dept is null or D.Dept<>'' or D.Dept" + NamaDept + ") " +
                //    ""+query+""+
                //    "group by Lokasi,PartNo,Nama order by lokasi";
            " with DataPartno as (select ItemID,Qty,LokID from T3_Serah where RowStatus>-1 and Qty>0 and ItemID>0 and " +
            " LokID not in (select ID from FC_Lokasi where Lokasi in ('BSAUTO','S99'))), " +
            " DataStokerLink as (select distinct(ItemID),StokerID from MasterStokerLink where RowStatus>-1), " +
            " DataA as (select A.ItemID,A.LokID,A.Qty,isnull(B.StokerID,'')StokerID from DataPartno A LEFT JOIN DataStokerLink B ON A.ItemID=B.ItemID), " +
            " DataB as (select B.PartNo,A.LokID,A.Qty,A.StokerID,B.Dept from  DataA A LEFT join fc_items B ON A.ItemID=B.ID ), " +
            " DataFinal as (select A.PartNo,B.Lokasi,A.Qty,isnull(C.Nama,'-')Nama,isnull(A.Dept,'-')Dept " +
            " from DataB A LEFT JOIN FC_Lokasi B ON A.LokID=B.ID LEFT JOIN MasterStoker C ON C.ID=A.StokerID) " +
            " select Nama,Lokasi,PartNo,Qty from DataFinal where Nama= '" + Nama + "' " + query + " and Lokasi not like'%KAT%' order by Nama,PartNo,Lokasi ";
            return strSQL;
        }

        public string ViewLaporanT3fin(string Nama)
        {
            string strSQL;            
            strSQL = " select Nama,Lokasi,PartNo,SUM(qty) as Qty from T3_Serah as A " +
                    " left join MasterStokerLink as B ON A.itemid=B.itemid and B.LokID=A.LokID and B.RowStatus>-1 " +
                    " inner join FC_Items as C ON A.ItemID=C.ID " +
                    " inner join FC_Lokasi  as D ON A.LokID=D.ID "+
                    " left join MasterStoker as E ON B.StokerID=E.ID and E.RowStatus>-1 " +
                    " where A.RowStatus > -1 and B.RowStatus > -1 and A.Qty > 0 and E.Nama = '" + Nama + "'" +
                    " and D.Dept='FINISHING' " +
                    "group by Lokasi,PartNo,Nama order by lokasi";
            return strSQL;
        }

        public string ViewLaporanT3opname(string Partno, int DeptID)
        {
            string Nama = string.Empty;
            string query = string.Empty;

            if (DeptID == 6)
            {
                Nama = "<>'FINISHING'";
                query = " and Nama in (select Nama from MasterStoker where DeptID=6) ";
            }
            else if (DeptID == 3)
            {
                Nama = "='FINISHING'";
                query = " and Nama in (select Nama from MasterStoker where DeptID=3) ";
            }
            else if (DeptID == 24)
            {
                query = "";
            }

            string strSQL;
            strSQL =
                //"select Nama,Lokasi,Partno,SUM(A.Qty) as Qty from T3_Serah as A " +
                // "left join MasterStokerLink as B ON A.itemid=B.itemid " +
                // "inner join FC_Items as C ON A.ItemID=C.ID " +
                // "inner join FC_Lokasi  as D ON A.LokID=D.ID " +
                // "left join MasterStoker as E ON B.StokerID=E.ID " +
                // "where  A.RowStatus > -1 and A.Qty > 0  and C.PartNo='" + Partno + "' " +                   
                // //" and (D.Dept is null or D.Dept<>'' or D.Dept"+Nama+") "+
                // ""+query+""+
                // "group by Nama,Lokasi,PartNo order by Lokasi";

            //" with DataPartno as (select ItemID,Qty,LokID from T3_Serah where RowStatus>-1 and Qty>0 and ItemID>0 and " +
            //" LokID not in (select ID from FC_Lokasi where Lokasi in ('BSAUTO','S99'))), " +
            //" DataStokerLink as (select distinct(ItemID),StokerID,LokID from MasterStokerLink where RowStatus>-1), " +
            //" DataA as (select A.ItemID,A.LokID,A.Qty,isnull(B.StokerID,'')StokerID from DataPartno A LEFT JOIN DataStokerLink B ON A.ItemID=B.ItemID and B.LokID=A.LokID), " +
            //" DataB as (select B.PartNo,A.LokID,A.Qty,A.StokerID,B.Dept from  DataA A LEFT join fc_items B ON A.ItemID=B.ID ), " +
            //" DataFinal as (select A.PartNo,B.Lokasi,A.Qty,isnull(C.Nama,'???')Nama,isnull(A.Dept,'-')Dept " +
            //" from DataB A LEFT JOIN FC_Lokasi B ON A.LokID=B.ID LEFT JOIN MasterStoker C ON C.ID=A.StokerID and A.LokID=B.ID) " +

              " with DataPartno as (select ItemID,Qty,LokID from T3_Serah where RowStatus>-1 and Qty>0 and ItemID>0 and  "+
              " LokID not in (select ID from FC_Lokasi where Lokasi in ('BSAUTO','S99'))),  DataStokerLink as (select distinct(ItemID),StokerID "+
              " from MasterStokerLink where RowStatus>-1), "+  
              " DataA as (select A.ItemID,A.LokID,A.Qty,isnull(B.StokerID,'')StokerID from DataPartno A LEFT JOIN DataStokerLink B ON A.ItemID=B.ItemID), "+
              " DataB as (select B.PartNo,A.LokID,A.Qty,A.StokerID,B.Dept from  DataA A LEFT join fc_items B ON A.ItemID=B.ID ),  "+  
              " DataFinal as (select A.PartNo,B.Lokasi,A.Qty,isnull(C.Nama,'-')Nama,isnull(A.Dept,'-')Dept  from DataB A "+
              " LEFT JOIN FC_Lokasi B ON A.LokID=B.ID LEFT JOIN MasterStoker C ON C.ID=A.StokerID) " +            
              " select Nama,Lokasi,PartNo,Qty from DataFinal where PartNo='" + Partno + "' "+query+" and Lokasi not like'%KAT%' "+
              " order by Nama,PartNo,Lokasi ";

            return strSQL;
        }

        public string ViewLaporanT3opnamefin(string Partno)
        {
            string strSQL;            
            strSQL = "select Nama,Lokasi,Partno,SUM(A.Qty) as Qty from T3_Serah as A " +
                     "left join MasterStokerLink as B ON A.itemid=B.itemid and A.LokID=B.LokID  and B.RowStatus>-1 " +
                     "inner join FC_Items as C ON A.ItemID=C.ID " +
                     "inner join FC_Lokasi  as D ON A.LokID=D.ID " +
                     "left join MasterStoker as E ON B.StokerID=E.ID and E.RowStatus>-1 " +
                     "where  A.RowStatus > -1 and B.RowStatus > -1 and A.Qty > 0  and C.PartNo='" + Partno + "' " +
                     "and D.Lokasi in (select namalokasi from fc_lokasidept where (deptid=3) and rowstatus>-1)" +
                     "group by Nama,Lokasi,PartNo order by Lokasi";
            return strSQL;
        }

        public GroupsPurchn GenerateObjectTes(SqlDataReader sqlDataReader)
        {
            objPurch = new GroupsPurchn();
            objPurch.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objPurch.GroupDescription = sqlDataReader["GroupDescription"].ToString();                   
            return objPurch;
        }
        
    }
}
