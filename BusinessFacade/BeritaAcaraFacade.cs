using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;
using System.Web.UI.WebControls;
namespace BusinessFacade
{
    public class BeritaAcaraFacade : AbstractFacade
    {
        private BeritaAcara objBA = new BeritaAcara();
        private PemantauanBA objPBA = new PemantauanBA();
        private PemantauanBA objRBA = new PemantauanBA();
        private ArrayList arrData = new ArrayList();
        private List<SqlParameter> sqlParam;

        public BeritaAcaraFacade()
            : base()
        {

        }
        public override int Insert(object objDomain)
        {
            throw new NotImplementedException();
        }
        public override int Delete(object objDomain)
        {
            throw new NotImplementedException();
        }
        public override int Update(object objDomain)
        {
            throw new NotImplementedException();
        }
        public override ArrayList Retrieve()
        {
            arrData = new ArrayList();
            string strSQL = "Select * from BeritaAcara where RowStatus>-1 order by ID";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateObject(sdr));
                }
            }
            return arrData;
        }
        public ArrayList Retrieve(bool ForPemantauan)
        {
            arrData = new ArrayList();
            string startdate = new Inifiles(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("StartDocument", "BeritaAcara");
            string strSQL = "Select *,(Select dbo.ItemNameInv(ItemID,1))ItemName from BeritaAcara where RowStatus>-1 and ID Not In(" +
                          "Select BAID From BeritaAcaraPemantauan where RowStatus>-1 group by BAID) and Left(Convert(char,badate,112),6)>=" + startdate +
                          " order by ID";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateObject(sdr));
                }
            }
            return arrData;
        }
        public ArrayList LoadDetailBA(string BAID)
        {
            ArrayList arrDetailBA = new ArrayList();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "Select *,(select ReceiptDate from Receipt where ID=BeritaAcaraDetail.ReceiptNo)ReceiptDate ";
            zw.CustomQuery += ",(select ReceiptNo from Receipt where ID=BeritaAcaraDetail.ReceiptNo)ReceiptNo1 ";
            zw.CustomQuery += "from BeritaAcaraDetail where RowStatus>-1 and BAID=" + BAID;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows && sdr != null)
            {
                while (sdr.Read())
                {
                    arrDetailBA.Add(ObjectReceipt(sdr));
                }
            }
            return arrDetailBA;
        }
        public BeritaAcara RetrieveBA(int BAID)
        {
            objBA = new BeritaAcara();
            arrData = new ArrayList();
            arrData = this.Retrieve(true);
            if (arrData.Count == 0) { return objBA; }
            foreach (BeritaAcara ba in arrData)
            {
                if (ba.ID == BAID) { objBA = ba; }
            }
            return objBA;
        }
        public Receipt LoadDetailBA(string BAID, string RMSID)
        {
            Receipt receipt=new Receipt();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "Select *,(select ReceiptDate from Receipt where ID=BeritaAcaraDetail.ReceiptNo)ReceiptDate ";
            zw.CustomQuery += ",(select ReceiptNo from Receipt where ID=BeritaAcaraDetail.ReceiptNo)ReceiptNo1 ";
            zw.CustomQuery += "from BeritaAcaraDetail where RowStatus>-1 and BAID=" + BAID + " and ReceiptNo=" + RMSID;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows && sdr != null)
            {
                while (sdr.Read())
                {
                    receipt = ObjectReceipt(sdr);
                }
            }
            return receipt;
        }
        public ArrayList RetrievePemantauan()
        {
            arrData = new ArrayList();
            string strSQL = "SELECT top 50 ID,BANum, ItemName,ReceiptDate, SupplierName,RMSNo, Nopol,NetDepo,NetBPAS, " +
                            "(NetDepo-NetBPAS)NetSelisih,((NetDepo-NetBPAS)/NetDepo)NetProsen, " +
                            "GrossDepo, GrossBPAS,(GrossDepo-GrossBPAS)GrossSelisih,((GrossDepo-GrossBPAS)/GrossDepo)GrossProsen, " +
                            "BalDepo,BalBPAS,(BalDepo-BalBpas)BalSelisih, KADepo,KABpas,(KADepo-KABpas)KASelisih, " +
                            "TglBADepo,TglBABPAS,(DATEDIFF(Day,TglBADepo,TglBABPAS))SelisihBA, Keterangan, Createdby,  " +
                            "CreatedTime,RowStatus,BAID " +
                            "FROM BeritaAcaraPemantauan where RowStatus >-1 " +
                            "ORDER BY ID Desc";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateObjectPemantauan(sdr));
                }
            }
            return arrData;
        }
        public ArrayList RetrievePemantauan(string Where)
        {
            arrData = new ArrayList();
            string strSQL = "SELECT ID,BANum, ItemName,ReceiptDate, SupplierName,RMSNo, Nopol,NetDepo,NetBPAS, " +
                            "(NetDepo-NetBPAS)NetSelisih,((NetDepo-NetBPAS)/NetDepo)NetProsen, " +
                            "GrossDepo, GrossBPAS,(GrossDepo-GrossBPAS)GrossSelisih,((GrossDepo-GrossBPAS)/GrossDepo)GrossProsen, " +
                            "BalDepo,BalBPAS,(BalDepo-BalBpas)BalSelisih, KADepo,KABpas,(KADepo-KABpas)KASelisih, " +
                            "TglBADepo,TglBABPAS,(DATEDIFF(Day,TglBADepo,TglBABPAS))SelisihBA, Keterangan, Createdby,  " +
                            "CreatedTime,RowStatus,BAID " +
                            "FROM BeritaAcaraPemantauan where RowStatus >-1  " + Where +
                            "ORDER BY BANum,ID Desc";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateObjectPemantauan(sdr));
                }
            }
            return arrData;
        }
        public void GetTahun(DropDownList ddl)
        {
            arrData = new ArrayList();
            string strSQL = "select distinct YEAR(CreatedTime) Tahun from BeritaAcara order by year(CreatedTime)";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddl.Items.Add(new ListItem(sdr["Tahun"].ToString(), sdr["Tahun"].ToString()));
                }
            }
        }
        public ArrayList RetieveItemBA()
        {
            string strSQL = "select ItemID,(Select dbo.ItemNameInv(ItemID,ItemTypeID))ItemName from ReceiptDetail " +
                          "where receiptid in(Select ReceiptNo from BeritaAcaraDetail where RowStatus>-1) " +
                          "Group By ItemID,ItemTypeID " +
                          "ORDER BY ItemID";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateObject(sdr,true));
                }
            }
            return arrData;
        }

        private object GenerateObject(SqlDataReader sdr, bool p)
        {
            objBA = new BeritaAcara();
            objBA.ItemID = int.Parse(sdr["ItemID"].ToString());
            objBA.ItemName = sdr["ItemName"].ToString();
            return objBA;
        }
        private BeritaAcara GenerateObject(SqlDataReader sdr)
        {
            objBA = new BeritaAcara();
            objBA.ID = int.Parse(sdr["ID"].ToString());
            objBA.BANum = sdr["BANum"].ToString();
            objBA.ItemName = sdr["ItemName"].ToString();
            objBA.BADate = Convert.ToDateTime(sdr["BADate"].ToString());
            objBA.TotalBPAS = Convert.ToDecimal(sdr["TotalBPAS"].ToString());
            objBA.TotalSup = Convert.ToDecimal(sdr["TotalSup"].ToString());
            objBA.JmlBalBPAS = Convert.ToDecimal(sdr["JmlBalBPAS"].ToString());
            objBA.KadarAirBPAS = Convert.ToDecimal(sdr["KadarAirBPAS"].ToString());
            objBA.Netto = Convert.ToDecimal(sdr["Netto"].ToString());
            return objBA;
        }
        private PemantauanBA GenerateObjectPemantauan(SqlDataReader sdr)
        {
            objPBA = new PemantauanBA();
            objPBA.BAID = int.Parse(sdr["BAID"].ToString());
            objPBA.BANum = sdr["BANum"].ToString();
            objPBA.ItemName = sdr["ItemName"].ToString();
            objPBA.ReceiptDate = (sdr["ReceiptDate"] != DBNull.Value) ? DateTime.Parse(sdr["ReceiptDate"].ToString()) : DateTime.MinValue;
            objPBA.SupplierName = sdr["SupplierName"].ToString();
            objPBA.RMSNo = sdr["RMSNo"].ToString();
            objPBA.Nopol = sdr["Nopol"].ToString();
            objPBA.NetBPAS = decimal.Parse(sdr["NetBPAS"].ToString());
            objPBA.NetDepo = decimal.Parse(sdr["NetDepo"].ToString());
            objPBA.Nopol = sdr["NoPol"].ToString();
            objPBA.NetSelisih = decimal.Parse(sdr["NetSelisih"].ToString());
            objPBA.NetProsen = decimal.Parse(sdr["NetProsen"].ToString());
            objPBA.GrossBPAS = decimal.Parse(sdr["GrossBPAS"].ToString());
            objPBA.GrossDepo = decimal.Parse(sdr["GrossDepo"].ToString());
            objPBA.GrossSelisih = decimal.Parse(sdr["GrossSelisih"].ToString());
            objPBA.GrossProsen = decimal.Parse(sdr["GrossProsen"].ToString());
            objPBA.BalBPAS = decimal.Parse(sdr["BalBPAS"].ToString());
            objPBA.BalDepo = decimal.Parse(sdr["BalDepo"].ToString());
            objPBA.BalSelisih = decimal.Parse(sdr["BalSelisih"].ToString());
            objPBA.KABpas = decimal.Parse(sdr["KABpas"].ToString());
            objPBA.KADepo = decimal.Parse(sdr["KADepo"].ToString());
            objPBA.KASelisih = decimal.Parse(sdr["KASelisih"].ToString());
            objPBA.TglBABPAS = DateTime.Parse(sdr["TglBABPAS"].ToString());
            objPBA.TglBADepo = DateTime.Parse(sdr["TglBADepo"].ToString());
            objPBA.SelisihBA = int.Parse(sdr["SelisihBA"].ToString());
            objPBA.Keterangan = sdr["Keterangan"].ToString();
            return objPBA;
        }
        private Receipt ObjectReceipt(SqlDataReader sdr)
        {
            Receipt rcp = new Receipt();
            rcp.ReceiptNo = sdr["ReceiptNo1"].ToString();
            rcp.ReceiptDate = (sdr["ReceiptDate"] == DBNull.Value) ? DateTime.MinValue : DateTime.Parse(sdr["ReceiptDate"].ToString());
            rcp.SupplierName = sdr["SupplierName"].ToString();
            rcp.SupplierID = int.Parse(sdr["SupplierID"].ToString());
            rcp.Gross = decimal.Parse(sdr["Gross"].ToString());
            rcp.Netto = decimal.Parse(sdr["Netto"].ToString());
            rcp.Quantity = decimal.Parse(sdr["Quantity"].ToString());
            rcp.KadarAir = decimal.Parse(sdr["KadarAir"].ToString());
            rcp.JmlBal = decimal.Parse(sdr["jmlBal"].ToString());
            rcp.NoSJ = sdr["NoSJ"].ToString();
            rcp.ID = int.Parse(sdr["ReceiptNo"].ToString());
            return rcp;
        }
        public ArrayList RetrieveTahunBA()
        {
            arrData = new ArrayList();
            string strSQL = "select YEAR(BADate)Tahun from BeritaAcara group by YEAR(badate) order by Year(BADate)Desc";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new BeritaAcara
                    {
                        Tahun=int.Parse(sdr["Tahun"].ToString())
                    });
                }
            }
            return arrData;
        }
        public ArrayList RetrieveBaPemantauan(string Bulan, string Tahun, string DepoKertasID, string ItemId)
        {
            string where = string.Empty;
            string lastApp = new Inifiles(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("LastApproval", "BeritaAcara");
            where = (DepoKertasID == "0") ? " " : " and ba.DepoKertasID=" + DepoKertasID;
            where += (ItemId == "0") ? "" : " and rd.ItemID=" + ItemId;
            where += " And baa.UserID=" + lastApp;
            #region Query Rev 0
            string strSQL = "select *,ROUND((x.NetSelisih/x.NetDepo)*100,2) as Persen from ( " +
                            "select A.ItemName,A.BANum,A.ReceiptDate,C.DepoName,A.SupplierName, " +
                            "A.RMSNo,A.NetDepo,A.NetBPAS,(A.NetBPAS-A.NetDepo) as NetSelisih from BeritaAcaraPemantauan as A," +
                            "BeritaAcara as B,DepoKertas as C where C.ID=B.DepoKertasID and A.BAID=B.ID " + where +
                            "and MONTH(A.ReceiptDate)=" + Bulan + "  and YEAR(A.ReceiptDate)=" + Tahun + " " +
                            ") as x order by x.DepoName Desc ";
            #endregion
            #region Query Rev 1
            strSQL = 
                   " WITH Depox AS ( " +
                   " SELECT  " +
                   " x.ItemName,x.BANum,x.Tanggal,x.DepoName,x.SupplierName,(SELECT dbo.ListRMSNoToRows(x.BANum)) AS RMSNo," +
                   " SUM(x.NetDepo)NetDepo, " +
                   " SUM(x.NetBPAS)NetBPAS, " +
                   " SUM(x.NetSelisih)NetSelisih, " +
                   " ROUND((SUM(x.NetSelisih)/SUM(x.NetDepo))*100,2) as Persen ,1 OrderBy " +
                   " from ( select A.ItemName,A.BANum,CONVERT(CHAR,A.ReceiptDate,103)Tanggal,A.ReceiptDate,C.DepoName,A.SupplierName, "+
                   " A.RMSNo,A. NetBPAS,A.NetDepo,(A.NetBPAS- A.NetDepo) as NetSelisih,A.BAID " +
                   " from BeritaAcaraPemantauan as A " +
                   " LEFT JOIN BeritaAcara as B ON B.ID=A.BAID " +
                   " LEFT JOIN BeritaAcaraDetail as BD ON BD.BAID=A.BAID " +
                   " LEFT JOIN DepoKertas as C ON C.ID=B.DepoKertasID " + where +
                   " MONTH(A.ReceiptDate)=" + Bulan + "  and YEAR(A.ReceiptDate)=" + Tahun + " ) as x  " +
                   " GROUP By x.BANum,x.BAID,x.DepoName,x.RMSNo,x.Tanggal,x.SupplierName,x.ItemName " +
                   " UNION  " +
                   " ( " +
                   " SELECT ''ItemName,''BANum,''Tanggal,DepoName,('Sub Total '+ DepoName) as Supp,''RMS, " +
                   " SUM(x.NetDepo)NetDepo, " +
                   " SUM(x.NetBPAS)NetBPAS, " +
                   " SUM(x.NetSelisih)NetSelisih, " +
                   " ROUND((SUM(x.NetSelisih)/SUM(x.NetDepo))*100,2) as Persen ,2 OrderBy " +
                   " from ( select A.ItemName,A.BANum,A.ReceiptDate,C.DepoName,A.SupplierName, A.RMSNo,A.NetBPAS," +
                   " A.NetDepo,(A.NetBPAS- A.NetDepo) as NetSelisih,A.BAID " +
                   " from BeritaAcaraPemantauan as A " +
                   " LEFT JOIN BeritaAcara as B ON B.ID=A.BAID " +
                   " LEFT JOIN BeritaAcaraDetail as BD ON BD.BAID=A.BAID " +
                   " LEFT JOIN DepoKertas as C ON C.ID=B.DepoKertasID " + where +
                   "  MONTH(A.ReceiptDate)=" + Bulan + "  and YEAR(A.ReceiptDate)=" + Tahun + " ) as x  " +
                   " Group by x.DepoName " +
                   " ) " +
                   " ) " +
                   " SELECT * FROM Depox Order By DepoName,OrderBy ";
            #endregion
            #region Query Rev 2 - used
            strSQL = "WITH Pemantauan AS( " +
                     "SELECT inv.ItemName,ba.BANum,CONVERT(CHAR,ba.BADate,103)Tanggal,r.ReceiptNo RMSNo,ba.ID BAID, " +
	                 "r.ReceiptDate,ba.TotalSup NetDepo,ba.Netto NetBPAS, " +
	                 "(ba.Netto-ba.TotalSup)NetSelisih,ba.DepoKertasID,bad.SupplierName " +
	                 "FROM BeritaAcaraDetail bad " +
	                 "LEFT JOIN BeritaAcara ba on bad.BAID=ba.ID " +
	                 "LEFT JOIN Receipt r on r.id=bad.ReceiptNo " +
	                 "LEFT JOIN ReceiptDetail rd On rd.ReceiptID=r.ID " +
	                 "LEFT JOIN Inventory inv on inv.ID=rd.ItemID " +
                     "LEFT JOIN BeritaAcaraApproval baa on baa.BAID=ba.ID  " +
                     "   WHERE ba.RowStatus>-1 and Bad.RowStatus>-1 AND MONTH(BADate)=" + Bulan + " AND YEAR(BADate)=" + Tahun + where +
                     "), " +
                     "Pemantauan1 AS( " +
                     "SELECT z.ItemName,z.BANum,z.Tanggal,z.ReceiptDate,(SELECT dbo.ListRMSNoToRows(z.BAID)) AS RMSNo, z.NetDepo, " +
                     "(z.NetBPAS)NetBPAS,(z.NetBPAS - (z.NetDepo))NetSelisih,z.BAID, " +
                     "(SELECT dbo.ListSupplierToRows(z.BAID)) AS SupplierName FROM Pemantauan AS z " +
                     "GROUP BY z.BAID,z.BANum,z.ReceiptDate,z.ItemName,z.NetDepo,z.Tanggal,z.NetBPAS,z.NetDepo " +
                     ") " +
                     ", " +
                     "Pemantauan2 AS ( " +
                     "SELECT x.ItemName,x.BANum,x.Tanggal,c.DepoName,x.SupplierName, x.RMSNo, x.NetBPAS,x.NetDepo,  " +
                     "x.NetSelisih, Case When x.NetSelisih!=0 Then ROUND(((x.NetSelisih)/(x.NetDepo))*100,2) ELSE 0 END as Persen ,1 OrderBy  " +
                     "FROM Pemantauan1 as x " +
                     "LEFT JOIN BeritaAcara AS b ON b.ID=x.BAID " +
                     "LEFT JOIN DepoKertas as C ON C.ID=b.DepoKertasID " +
                     "), " +
                     "Pemantauan3 AS( " +
                     "   Select ItemName,BANum,Tanggal,DepoName,SupplierName, RMSNo, NetBPAS,NetDepo,NetSelisih,Persen ,OrderBy FROm Pemantauan2 " +
                     "   UNION ALL " +
	                 "   Select ItemName,'' BANum,'' Tanggal, DepoName +' SUB TOTAL' DepoName, ''SupplierName, '' RMSNo, SUM(NetBPAS),SUM(NetDepo), " +
                     "   SUM(NetSelisih),(AVG(Persen))Persen ,2 OrderBy FROm Pemantauan2 GROUP BY DepoName,ItemName " +
                     "   UNION ALL " +
                     "   Select UPPER(ItemName)+' SUB TOTAL'  ItemName,'' BANum,'' Tanggal, '' DepoName, ''SupplierName, '' RMSNo, SUM(NetBPAS),SUM(NetDepo), " +
                     "   SUM(NetSelisih),(AVG(Persen))Persen ,3 OrderBy FROm Pemantauan2 GROUP BY ItemName " +
                     "   ) " +
                     "SELECT * FROM Pemantauan3 ORDER BY ItemName,DepoName,OrderBy,BANum ";
            #endregion
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrData = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrData.Add(GenerateObjectRekapBA(sqlDataReader, GenerateObjectRekapBA(sqlDataReader)));
                }
            }
            return arrData;
        }
        private PemantauanBA GenerateObjectRekapBA(SqlDataReader sdr)
        {
            objRBA = new PemantauanBA();
            objRBA.ItemName = sdr["ItemName"].ToString();
            objRBA.BANum = sdr["BANum"].ToString();
            objRBA.DepoName = sdr["DepoName"].ToString();
            objRBA.SupplierName = sdr["SupplierName"].ToString();
            objRBA.RMSNo = sdr["RMSNo"].ToString();
            objRBA.NetDepo = decimal.Parse(sdr["NetDepo"].ToString());
            objRBA.NetBPAS = decimal.Parse(sdr["NetBPAS"].ToString());
            objRBA.NetSelisih = decimal.Parse(sdr["NetSelisih"].ToString());
            objRBA.Persen = decimal.Parse(sdr["Persen"].ToString());
            objRBA.CreatedBy = sdr["Tanggal"].ToString();
            return objRBA;
        }
        private PemantauanBA GenerateObjectRekapBA(SqlDataReader sdr, PemantauanBA ba)
        {
            objRBA = (PemantauanBA)ba;
            objRBA.RowStatus = int.Parse(sdr["OrderBy"].ToString());
            return objRBA;
        }
        public ArrayList RetrievePemantauanBuat(int Bulan, int Tahun)
        {
            arrData = new ArrayList();
            string strSQL = "SET DATEFIRST 1; " +
                            "WITH BA AS ( " +
                            "SELECT b.ID,b.BADate,b.BANum,b.DepoKertasID,d.DepoName,d.GroupDepo, " +
                            "(SELECT dbo.ItemNameInv(ItemID,1))ItemName,'KG' Unit,b.Selisih, " +
                            "CASE WHEN b.Selisih>0 THEN 'AdjIN' ELSE 'AdjOut'END Tipe,b.ProsSelisih,b.CreatedTime " +
                            "FROM BeritaAcara b " +
                            "LEFT JOIN DepoKertas d ON d.ID=b.DepoKertasID " +
                            "WHERE MONTH(b.BADate)=" + Bulan + " AND YEAR(b.BADate)=" + Tahun +
                            "AND b.ItemID IN(16868,16871) AND b.RowStatus>-1 " +
                            ") " +
                            ",RMSDate AS ( " +
                            "    SELECT b.BAID,MAX(rd.CreatedTime)ReceiptDate FROM BeritaAcaraDetail b " +
                            "    LEFT JOIN Receipt rd ON rd.ID=b.ReceiptNo  " +
                            "    WHERE b.BAID in(SELECT ID From BA) AND rd.Status>-1 " +
                            "    GROUP BY b.BAID " +
                            ") " +
                            ",PODate AS ( " +
                            "    SELECT ba.BAID,MAX(p.CreatedTime)CreatePO  " +
                            "    FROM BeritaAcaraDetail ba " +
                            "    LEFT JOIN Receipt r ON r.ID=ba.ReceiptNo " +
                            //"    --LEFT JOIN ReceiptDetail rd ON rd.ID=ba.ReceiptNo " +
                            "    LEFT JOIN POPurchn p ON p.ID=r.POID " +
                            "    WHERE ba.BAID IN (SELECT ID From BA) AND r.Status>-1 AND p.Status>-1 " +
                            "    GROUP BY ba.BAID " +
                            ") " +
                            ",AttachDate AS( " +
                            "    SELECT BAID,MAX(CreatedTime)AttachDate FROM BeritaAcaraAttachment " +
                            "    WHERE BAID IN(SELECT ID FROM BA) AND RowStatus>-1 " +
                            "    GROUP BY BAID " +
                            ") " +
                            ",BA1 AS ( " +
                            "SELECT b.ID, b.BANum,d.ReceiptDate,b.CreatedTime CreateBA,p.BAID,p.CreatePO,a.AttachDate,b.DepoName, " +
                            "b.ItemName,b.Unit,b.Selisih,b.Tipe,ProsSelisih, " +
                            "DATEDIFF(DAY,p.CreatePO,a.AttachDate)Lama,(SELECT dbo.GetOFFDay(CreatePO,AttachDate))HariLibur, " +
                            "(SELECT COUNT(ID) FROM CalenderOffDay WHERE CONVERT(CHAR,HariLibur,112)  " +
                            "    Between CONVERT(CHAR,p.CreatePO,112) AND CONVERT(CHAR,a.AttachDate,112)  " +
                            "   AND DATEPART(WEEKDAY,HariLibur)NOT IN(6,7))LiburNasional " +
                            "FROM BA b " +
                            "LEFT JOIN PODate p ON p.BAID=b.ID " +
                            "LEFT JOIN RMSDate d ON d.BAID=b.ID " +
                            "LEFT JOIN AttachDate a ON a.BAID=b.ID " +
                            ") " +
                            "SELECT *,(Lama-HariLibur-LiburNasional)TotalWaktu, " +
                            "    CASE WHEN ProsSelisih <-5 THEN " +
                            "        CASE WHEN (Lama-HariLibur-LiburNasional) > 3  THEN 0 ELSE 1 END  " +
                            "    ELSE " +
                            "        CASE WHEN (Lama-HariLibur-LiburNasional) > 2 THEN 0 ELSE 1 END " +
                            "    END Status " +
                            "FROM BA1 ORDER By BANum,ID";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateObjectBuat(sdr));
                }
            }
            return arrData;
        }
        private PembuatanBA GenerateObjectBuat(SqlDataReader sdr)
        {
            PembuatanBA objBU = new PembuatanBA();
            objBU.BANum = sdr["BANum"].ToString();
            objBU.ReceiptDate = DateTime.Parse(sdr["ReceiptDate"].ToString());
            objBU.TglBABPAS = DateTime.Parse(sdr["CreateBA"].ToString());
            objBU.PODate = DateTime.Parse(sdr["CreatePO"].ToString());
            objBU.AttDate = DateTime.Parse(sdr["AttachDate"].ToString());
            objBU.DepoName = sdr["DepoName"].ToString();
            objBU.ItemName=sdr["ItemName"].ToString();
            objBU.Unit = sdr["Unit"].ToString();
            objBU.NetSelisih = decimal.Parse(sdr["Selisih"].ToString());
            objBU.Persen = decimal.Parse(sdr["ProsSelisih"].ToString());
            objBU.RowStatus = int.Parse(sdr["Status"].ToString());
            objBU.Tipe = sdr["Tipe"].ToString();
            return objBU;
        }


        public ArrayList RetrieveUnApprove(string PosApproval)
        {
            arrData = new ArrayList();
            if (PosApproval.Trim() == "2")
                return arrData;
            string strSQL = "select * from BeritaAcara where year(BADate)>2016 AND  RowStatus>-1 and Approval=" + PosApproval;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    //arrData.Add(GenerateObject(sdr));
                    arrData.Add(new BeritaAcara
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        BANum = sdr["BANum"].ToString(),
                        CreatedBy = sdr["CreatedBy"].ToString(),
                    });
                }
            }
            return arrData;
        }
    }
    
}
