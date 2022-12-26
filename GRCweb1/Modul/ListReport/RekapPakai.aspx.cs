using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dapper;
using GRCweb1.Models;
using Newtonsoft.Json;
using Factory;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using System.Collections;

namespace GRCweb1.Modul.ListReport
{
    public partial class RekapPakai : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<RekapPakaiNf.ParamGroupItem> ListGroupItem()
        {
            List<RekapPakaiNf.ParamGroupItem> list = new List<RekapPakaiNf.ParamGroupItem>();
            list = RekapPakaiNfFacade.GetListGroupItem();
            return list;
        }

        [WebMethod]
        public static List<RekapPakaiNf.ParamDept> ListDept()
        {
            List<RekapPakaiNf.ParamDept> list = new List<RekapPakaiNf.ParamDept>();
            list = RekapPakaiNfFacade.GetListDept();
            return list;
        }

        [WebMethod]
        public static List<RekapPakaiNf.ParamDataDetail> ListDataDetail(string DariTanggal, string SampaiTanggal, string DeptName, int TypeItem)
        {
            string drTgl = DateTime.Parse(DariTanggal).ToString("yyyyMMdd");
            string sdTgl = DateTime.Parse(SampaiTanggal).ToString("yyyyMMdd");
            int Bulan = int.Parse(drTgl.Substring(4, 2));
            int Tahun = int.Parse(drTgl.Substring(0, 4));
            if (Bulan == 1) { Tahun = Tahun - 1; }
            string jam = DateTime.Now.ToString("yyMMss");
            string SaldoAwal = string.Empty;
            SaldoAwal = Global.nBulan((Bulan == 1) ? 12 : Bulan - 1, true) + "AvgPrice";

            string GroupID = string.Empty;
            string DeptID = string.Empty;
            if (TypeItem > 0) { GroupID = " and GroupID=" + TypeItem + " "; }
            if (DeptName != "") { DeptID = " and DeptID in (select ID from dept where alias='" + DeptName + "') "; }
            string strSQL = string.Empty;
            strSQL =
                    "select (case when GROUPING(DeptName) = 0 and " +
                    "      GROUPING(GroupDescription) = 1 and  " +
                    "		  GROUPING(PakaiNo) = 1 and GROUPING(PakaiDate) = 1 and GROUPING(ItemCode) = 1 and GROUPING(ItemName) = 1 and GROUPING(UOMCode) = 1 and  " +
                    "		  GROUPING(Jumlah) = 1 and GROUPING(Harga) = 1  " +
                    "then 'Sub Total ' + DeptName  " +
                    " when GROUPING(DeptName) = 1 and " +
                    "      GROUPING(GroupDescription) = 1 and  " +
                    "	  GROUPING(PakaiNo) = 1 and GROUPING(PakaiDate) = 1 and GROUPING(ItemCode) = 1 and GROUPING(ItemName) = 1 and GROUPING(UOMCode) = 1 and  " +
                    "	  GROUPING(Jumlah) = 1 and GROUPING(Harga) = 1 then 'Grand Total' " +
                    " else DeptName " +
                    "    end) as DeptName,(case when GROUPING(DeptName) = 0 and " +
                    "      GROUPING(GroupDescription) = 0 and  " +
                    "		  GROUPING(PakaiNo) = 1 and GROUPING(PakaiDate) = 1 and GROUPING(ItemCode) = 1 and GROUPING(ItemName) = 1 and GROUPING(UOMCode) = 1 and  " +
                    "	  GROUPING(Jumlah) = 1 and GROUPING(Harga) = 1  " +
                    " then 'Sub Total ' + GroupDescription else GroupDescription end )GroupDescription, " +
                     "PakaiNo,PakaiDate,ItemCode,ItemName,UOMCode,Jumlah,Harga,sum(Total)Total ,Keterangan,Status,NoPol " +
                    "    from ( " +
                    "select PakaiNo,convert(varchar,PakaiDate,103) as PakaiDate,ItemCode,ItemName,UOMCode,SUM(Quantity) as Jumlah, " +
                    "  case when crc >1 THEN (Harga) " +
                    "   else " +
                    "   isnull(Harga,0) end Harga,Harga*SUM(Quantity) Total, Case When ProdLine=0 Then Keterangan else 'Prod. Line '+ Cast(ProdLine as Char(2))+'-'+Keterangan end Keterangan," +
                    "   GroupID,GroupDescription,DeptName,Status, " +
                    "   crc,ItemID,ISNULL(NoPol,'')NoPol " +
                    "    from  " +
                    "  (SELECT  " +
                    "      1 crc, " +
                    "      CASE PakaiDetail.ItemTypeID   " +
                    "          WHEN 1 THEN (SELECT ItemCode FROM Inventory WHERE ID = PakaiDetail.ItemID)   " +
                    "          WHEN 2 THEN (SELECT ItemCode FROM Asset WHERE ID = PakaiDetail.ItemID)   " +
                    "          ELSE (SELECT ItemCode FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS ItemCode,   " +
                    "      CASE PakaiDetail.ItemTypeID   " +
                    "          WHEN 1 THEN (SELECT ItemName FROM Inventory WHERE ID = PakaiDetail.ItemID)   " +
                    "          WHEN 2 THEN (SELECT ItemName FROM Asset WHERE ID = PakaiDetail.ItemID)  " +
                    "          ELSE (SELECT ItemName FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS ItemName,   " +
                    "      CASE PakaiDetail.ItemTypeID   " +
                    "          WHEN 1 THEN (SELECT GroupID FROM Inventory WHERE ID = PakaiDetail.ItemID)   " +
                    "          WHEN 2 THEN (SELECT GroupID FROM Asset WHERE ID = PakaiDetail.ItemID)   " +
                    "          ELSE (SELECT GroupID FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS GroupID,   " +
                    "      CASE WHEN PakaiDetail.ItemTypeID=3 THEN " +
                    "          ( " +
                    "          SELECT TOP 1 vw_HargaReceipt.Price FROM vw_HargaReceipt where vw_HargaReceipt.ItemID=PakaiDetail.ItemID  " +
                    "          and vw_HargaReceipt.ItemTypeID=3 and vw_HargaReceipt.ReceiptDate<=Pakai.PakaiDate order by id desc " +
                    "          ) " +
                    "      ELSE " +
                    "      ISNULL(PakaiDetail.AvgPrice,  " +
                    "      (select top 1 " + SaldoAwal + " From SaldoInventory where SaldoInventory.ItemID=PakaiDetail.ItemID and YearPeriod=year(Pakai.PakaiDate)))	  " +
                    "      END Harga,        " +
                    "      CASE when PakaiDetail.GroupID>0 THEN  " +
                    "          (SELECT GroupDescription FROM GroupsPurchn WHERE GroupsPurchn.ID = PakaiDetail.GroupID)  ELSE 'xxx' END AS GroupDescription,   " +
                    "      CASE Pakai.Status  WHEN 0 THEN ('Open')   " +
                    "          WHEN 1 THEN ('Head')  ELSE ('Gudang') END AS Status,PakaiDetail.ItemID, " +
                    "      PakaiDetail.Quantity,PakaiDetail.Keterangan,Pakai.PakaiNo,UOM.UOMCode,Pakai.PakaiDate," +
                    "      Pakai.DeptID,Dept.DeptName,PakaiDetail.ProdLine,PakaiDetail.NoPol  " +
                    "      FROM Pakai  " +
                    "      INNER JOIN PakaiDetail  " +
                    "      ON Pakai.ID = PakaiDetail.PakaiID and PakaiDetail.RowStatus>-1  " +
                    "      INNER JOIN UOM  " +
                    "      ON PakaiDetail.UomID = UOM.ID and UOM.RowStatus>-1  " +
                    "      INNER JOIN Dept  " +
                    "      ON Pakai.DeptID = Dept.ID and Dept.RowStatus>-1  " +
                    "      where Pakai.status=3 and convert(varchar,Pakai.PakaiDate,112)>='" + drTgl + "' and   " +
                    "      convert(varchar,Pakai.PakaiDate,112)<='" + sdTgl + "'" + GroupID + DeptID + " ) as AA  " +
                    "      group by DeptName,ItemCode,ItemName,UOMCode,Harga,GroupID,GroupDescription,PakaiNo," +
                    "      PakaiDate,Status,Keterangan,ItemID,crc,ProdLine,NoPol " +
                    "      ) s group by grouping sets((GroupDescription,Deptname,PakaiNo,PakaiDate,ItemCode,ItemName," +
                    "UOMCode,Jumlah,Harga,Keterangan,Status,NoPol),(GroupDescription,DeptName),(DeptName),()) ";

            List<RekapPakaiNf.ParamDataDetail> list = new List<RekapPakaiNf.ParamDataDetail>();
            list = RekapPakaiNfFacade.GetListDataDetail(strSQL);
            return list;
        }

        [WebMethod]
        public static List<RekapPakaiNf.ParamDataRekap> ListDataRekap(string DariTanggal, string SampaiTanggal, string DeptName, int TypeItem)
        {
            string drTgl = DateTime.Parse(DariTanggal).ToString("yyyyMMdd");
            string sdTgl = DateTime.Parse(SampaiTanggal).ToString("yyyyMMdd");
            int Bulan = int.Parse(drTgl.Substring(4, 2));
            int Tahun = int.Parse(drTgl.Substring(0, 4));
            if (Bulan == 1) { Tahun = Tahun - 1; }
            string jam = DateTime.Now.ToString("yyMMss");
            string SaldoAwal = string.Empty;
            SaldoAwal = Global.nBulan((Bulan == 1) ? 12 : Bulan - 1, true) + "AvgPrice";

            string GroupID = string.Empty;
            string DeptID = string.Empty;
            if (TypeItem > 0) { GroupID = " and GroupID=" + TypeItem + " "; }
            if (DeptName != "") { DeptID = " and DeptID in (select ID from dept where alias='" + DeptName + "') "; }
            string strSQL = string.Empty;
            strSQL =
               "select (case when GROUPING(DeptName) = 0 and " +
               "      GROUPING(GroupDescription) = 1 and  " +
               "		  GROUPING(ItemCode) = 1 and GROUPING(ItemName) = 1 and GROUPING(UOMCode) = 1 and  " +
               "		  GROUPING(Jumlah) = 1 and GROUPING(Harga) = 1  " +
               "then 'Sub Total ' + DeptName  " +
               " when GROUPING(DeptName) = 1 and  GROUPING(GroupDescription) = 1 and  " +
               "	  GROUPING(ItemCode) = 1 and GROUPING(ItemName) = 1 and GROUPING(UOMCode) = 1 and  " +
               "	  GROUPING(Jumlah) = 1 and GROUPING(Harga) = 1 then 'Grand Total' " +
               " else DeptName " +
               "    end) as DeptName,(case when GROUPING(DeptName) = 0 and " +
               "      GROUPING(GroupDescription) = 0 and  " +
               "	   GROUPING(ItemCode) = 1 and GROUPING(ItemName) = 1 and GROUPING(UOMCode) = 1 and  " +
               "	  GROUPING(Jumlah) = 1 and GROUPING(Harga) = 1  " +
               " then 'Sub Total ' + GroupDescription else GroupDescription end )GroupDescription, " +
               " ItemCode,ItemName,UOMCode,Jumlah,Harga,sum(Total)Total from ( " +
               "select GroupID,GroupDescription,ItemCode,ItemName,UOMCode,sum(Jumlah)Jumlah,case when sum(jumlah)>0 then " +
               "sum(Harga* Jumlah)/sum(jumlah) else 0 end Harga,case when sum(jumlah)>0 then " +
               "(sum(Harga* Jumlah)/sum(jumlah))*sum(Jumlah) else 0 end Total,DeptName from ( " +
               "select PakaiNo,convert(varchar,PakaiDate,103) as PakaiDate,ItemCode,ItemName,UOMCode,SUM(Quantity) as Jumlah, " +
               "  case when crc >1 THEN (Harga) " +
               "   else " +
               "   isnull(Harga,0) end Harga,Harga*SUM(Quantity) Total, Case When ProdLine=0 Then Keterangan else 'Prod. Line '+ Cast(ProdLine as Char(2))+'-'+Keterangan end Keterangan," +
               "   GroupID,GroupDescription,DeptName,Status, " +
               "   crc,ItemID,ISNULL(NoPol,'')NoPol " +
               "    from  " +
               "  (SELECT  " +
               "      1 crc, " +
               "      CASE PakaiDetail.ItemTypeID   " +
               "          WHEN 1 THEN (SELECT ItemCode FROM Inventory WHERE ID = PakaiDetail.ItemID)   " +
               "          WHEN 2 THEN (SELECT ItemCode FROM Asset WHERE ID = PakaiDetail.ItemID)   " +
               "          ELSE (SELECT ItemCode FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS ItemCode,   " +
               "      CASE PakaiDetail.ItemTypeID   " +
               "          WHEN 1 THEN (SELECT ItemName FROM Inventory WHERE ID = PakaiDetail.ItemID)   " +
               "          WHEN 2 THEN (SELECT ItemName FROM Asset WHERE ID = PakaiDetail.ItemID)  " +
               "          ELSE (SELECT ItemName FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS ItemName,   " +
               "      CASE PakaiDetail.ItemTypeID   " +
               "          WHEN 1 THEN (SELECT GroupID FROM Inventory WHERE ID = PakaiDetail.ItemID)   " +
               "          WHEN 2 THEN (SELECT GroupID FROM Asset WHERE ID = PakaiDetail.ItemID)   " +
               "          ELSE (SELECT GroupID FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS GroupID,   " +
               "      CASE WHEN PakaiDetail.ItemTypeID=3 THEN " +
               "          ( " +
               "          SELECT TOP 1 vw_HargaReceipt.Price FROM vw_HargaReceipt where vw_HargaReceipt.ItemID=PakaiDetail.ItemID  " +
               "          and vw_HargaReceipt.ItemTypeID=3 and vw_HargaReceipt.ReceiptDate<=Pakai.PakaiDate order by id desc " +
               "          ) " +
               "      ELSE " +
               "      ISNULL(PakaiDetail.AvgPrice,  " +
               "      (select top 1 " + SaldoAwal + " From SaldoInventory where SaldoInventory.ItemID=PakaiDetail.ItemID and YearPeriod=year(Pakai.PakaiDate)))	  " +
               "      END Harga,        " +
               "      CASE when PakaiDetail.GroupID>0 THEN  " +
               "          (SELECT GroupDescription FROM GroupsPurchn WHERE GroupsPurchn.ID = PakaiDetail.GroupID)  ELSE 'xxx' END AS GroupDescription,   " +
               "      CASE Pakai.Status  WHEN 0 THEN ('Open')   " +
               "          WHEN 1 THEN ('Head')  ELSE ('Gudang') END AS Status,PakaiDetail.ItemID, " +
               "      PakaiDetail.Quantity,PakaiDetail.Keterangan,Pakai.PakaiNo,UOM.UOMCode,Pakai.PakaiDate," +
               "      Pakai.DeptID,Dept.DeptName,PakaiDetail.ProdLine,PakaiDetail.NoPol  " +
               "      FROM Pakai  " +
               "      INNER JOIN PakaiDetail  " +
               "      ON Pakai.ID = PakaiDetail.PakaiID and PakaiDetail.RowStatus>-1  " +
               "      INNER JOIN UOM  " +
               "      ON PakaiDetail.UomID = UOM.ID and UOM.RowStatus>-1  " +
               "      INNER JOIN Dept  " +
               "      ON Pakai.DeptID = Dept.ID and Dept.RowStatus>-1  " +
               "      where Pakai.status=3 and convert(varchar,Pakai.PakaiDate,112)>='" + drTgl + "' and   " +
               "      convert(varchar,Pakai.PakaiDate,112)<='" + sdTgl + "'" + GroupID + DeptID + " ) as AA  " +
               "      group by DeptName,ItemCode,ItemName,UOMCode,Harga,GroupID,GroupDescription,PakaiNo," +
               "      PakaiDate,Status,Keterangan,ItemID,crc,ProdLine,NoPol " +
               "      ) A group by GroupID,DeptName,GroupDescription,ItemCode,ItemName,UOMCode,Harga " +
               "      ) s group by grouping sets((GroupDescription,Deptname,ItemCode,ItemName," +
               "UOMCode,Jumlah,Harga),(GroupDescription,DeptName),(DeptName),()) ";

            List<RekapPakaiNf.ParamDataRekap> list = new List<RekapPakaiNf.ParamDataRekap>();
            list = RekapPakaiNfFacade.GetListDataRekap(strSQL);
            return list;
        }

    }
}