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
    public class LaporanBulananFacade : AbstractFacade
    {
        private LaporanBulanan objLapBul = new LaporanBulanan();
        private ArrayList arrLapBul;
        private List<SqlParameter> sqlListParam;


        public LaporanBulananFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override int Update(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override int Delete(object objDomain)
        {
            throw new NotImplementedException();
        }
        public override ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }

        public int InsertLapBul(object objDomain)
        {
            try
            {
                objLapBul = (LaporanBulanan)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@GroupID", objLapBul.GroupID));
                sqlListParam.Add(new SqlParameter("@Bulan", objLapBul.Bulan));
                sqlListParam.Add(new SqlParameter("@Tahun", objLapBul.Tahun));
                sqlListParam.Add(new SqlParameter("@Status", objLapBul.Status));
                sqlListParam.Add(new SqlParameter("@Users", objLapBul.Users));
                int intResult = dataAccess.ProcessData(sqlListParam, "LaporanBulanan_sp_insert");
                strError = dataAccess.Error;
                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int InsertLapBulFileName(object objDomain)
        {
            try
            {
                objLapBul = (LaporanBulanan)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@LapID", objLapBul.LapID));
                sqlListParam.Add(new SqlParameter("@FileName", objLapBul.FileName));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objLapBul.CreatedBy));
                int intResult = dataAccess.ProcessData(sqlListParam, "LaporanBulanan_sp_insertFile");
                strError = dataAccess.Error;
                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateLapBul(object objDomain)
        {
            try
            {
                objLapBul = (LaporanBulanan)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objLapBul.ID));
                sqlListParam.Add(new SqlParameter("@Bulan", objLapBul.Bulan));
                sqlListParam.Add(new SqlParameter("@Tahun", objLapBul.Tahun));
                sqlListParam.Add(new SqlParameter("@Status", objLapBul.Status));
                sqlListParam.Add(new SqlParameter("@Users", objLapBul.Users));
                int intResult = dataAccess.ProcessData(sqlListParam, "LaporanBulanan_sp_update");
                strError = dataAccess.Error;
                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateDataCetak(object objDomain)
        {
            try
            {
                objLapBul = (LaporanBulanan)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objLapBul.LapID));
                //sqlListParam.Add(new SqlParameter("@Bulan", objLapBul.Bulan));
                //sqlListParam.Add(new SqlParameter("@Tahun", objLapBul.Tahun));
                sqlListParam.Add(new SqlParameter("@Status", objLapBul.Status));
                //sqlListParam.Add(new SqlParameter("@Users", objLapBul.Users));
                int intResult = dataAccess.ProcessData(sqlListParam, "LaporanBulanan_sp_updateCetak");
                strError = dataAccess.Error;
                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateLapBulFileName(object objDomain)
        {
            try
            {
                objLapBul = (LaporanBulanan)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@FileName", objLapBul.FileName));
                //sqlListParam.Add(new SqlParameter("@Bulan", objLapBul.Bulan));
                //sqlListParam.Add(new SqlParameter("@Tahun", objLapBul.Tahun));
                //sqlListParam.Add(new SqlParameter("@Status", objLapBul.Status));
                sqlListParam.Add(new SqlParameter("@Users", objLapBul.Users));
                int intResult = dataAccess.ProcessData(sqlListParam, "LaporanBulanan_sp_update");
                strError = dataAccess.Error;
                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateLapBul2(object objDomain)
        {
            try
            {
                objLapBul = (LaporanBulanan)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objLapBul.ID));
                //sqlListParam.Add(new SqlParameter("@Bulan", objLapBul.Bulan));
                //sqlListParam.Add(new SqlParameter("@Tahun", objLapBul.Tahun));
                //sqlListParam.Add(new SqlParameter("@Status", objLapBul.Status));
                sqlListParam.Add(new SqlParameter("@Users", objLapBul.Users));
                int intResult = dataAccess.ProcessData(sqlListParam, "LaporanBulanan_sp_updateAPM");
                strError = dataAccess.Error;
                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateLapbulEmail(object objDomain)
        {
            try
            {
                objLapBul = (LaporanBulanan)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objLapBul.ID));                
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objLapBul.LastModifiedBy));                
                int intResult = dataAccess.ProcessData(sqlListParam, "LaporanBulanan_sp_update2");
                strError = dataAccess.Error;
                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int Hapus(object objDomain)
        {
            try
            {
                objLapBul = (LaporanBulanan)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objLapBul.ID));
                sqlListParam.Add(new SqlParameter("@Users", objLapBul.Users));
                int intResult = dataAccess.ProcessData(sqlListParam, "LaporanBulanan_sp_hapus");
                strError = dataAccess.Error;
                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int HapusT13(object objDomain)
        {
            try
            {
                objLapBul = (LaporanBulanan)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objLapBul.ID));
                sqlListParam.Add(new SqlParameter("@Users", objLapBul.Users));
                int intResult = dataAccess.ProcessData(sqlListParam, "LaporanBulananT13_sp_hapus");
                strError = dataAccess.Error;
                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        //public ArrayList RetrieveGroupPurchn(int Tahun, int Bulan, int GroupID, string UnitKerja)
        //{
        //    string strSQL = string.Empty;
        //    string Query1 = string.Empty;
        //    string Query2 = string.Empty;

        //    if (UnitKerja != "0")
        //    {
        //        if (UnitKerja == "1")
        //        {
        //            switch (GroupID)
        //            {
        //                case 1:
        //                    Query1 = "GroupID in (11,1,2)";
        //                    break;
        //                case 2:
        //                    Query1 = "GroupID in (3,8,9,5,4)";
        //                    break;
        //                case 3:
        //                    Query1 = "GroupID in (7,10)";
        //                    break;
        //                case 0:
        //                    Query1 = "";
        //                    break;
        //            }
        //        }
        //        else if (UnitKerja == "7")
        //        {
        //            switch (GroupID)
        //            {
        //                case 1:
        //                    Query1 = "GroupID in (3,7,8,9,10,5,4,6)";
        //                    break;
        //                case 2:
        //                    Query1 = "GroupID in (1,2,11)";
        //                    break;                       
        //                case 0:
        //                    Query1 = "";
        //                    break;
        //            }
        //        }
        //    }
              
        //    strSQL = " select GroupID,ISNULL(ID,0)ID,GroupDescription,CASE WHEN  Keterangan=1 THEN 'Release' WHEN Keterangan=2 " +
        //    " THEN 'Approved Mgr Log'  WHEN Keterangan=3  THEN 'Approved PM' WHEN Keterangan=4 THEN 'Email Sent'  " +
        //    " ELSE 'Open' END Keterangan from (select A.GroupID,(select ID from elapbul as E where E.grouppurchn=B.ID and E.Bulan="+Bulan+" and E.Tahun="+Tahun+" and " +
        //    " Rowstatus > -1)ID,B.GroupDescription,(select E1.Status from elapbul as E1 where E1.grouppurchn=B.ID and E1.Bulan=" + Bulan + " and E1.Tahun=" + Tahun + " " +
        //    " and E1.Rowstatus>-1)Keterangan from ELapbul_GroupPurchn as A INNER JOIN GroupsPurchn as B ON A.GroupID=B.ID) as x1 where " + Query1 + " ";

        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        //    strError = dataAccess.Error;
        //    arrLapBul = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrLapBul.Add(GenerateObjectLapBul(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrLapBul.Add(new LaporanBulanan());

        //    return arrLapBul;
        //}

        public ArrayList RetrieveGroupPurchn(string BulanS, int Tahun, int Bulan, int GroupID, string UnitKerja)
        {
            string strSQL = string.Empty;
            string Query1 = string.Empty;
            string Query2 = string.Empty;

            if (UnitKerja != "0")
            {
                if (UnitKerja == "1")
                {
                    switch (GroupID)
                    {
                        case 1:
                            Query1 = "GroupID in (11,1,2)";
                            break;
                        case 2:
                            Query1 = "GroupID in (3,8,9,5,4,12)";
                            break;
                        case 3:
                            Query1 = "GroupID in (7,10,13,14)";
                            break;
                        case 0:
                            Query1 = "";
                            break;
                    }
                }
                else if (UnitKerja == "7")
                {
                    switch (GroupID)
                    {
                        case 1:
                            Query1 = "GroupID in (3,7,8,9,10,5,4,6,12,13,14)";
                            break;
                        case 2:
                            Query1 = "GroupID in (1,2,11)";
                            break;
                        case 0:
                            Query1 = "";
                            break;
                    }
                }
                else if (UnitKerja == "13")
                {
                    switch (GroupID)
                    {
                        case 1:
                            Query1 = "GroupID in (1,2,11)";
                            break;
                        case 2:
                            Query1 = "GroupID in (3,7,8,9,10,5,4,6,12,13,14,1,2,11)";
                            break;
                        case 0:
                            Query1 = "";
                            break;
                    }
                }
            }
            //strSQL = " select GroupID,ISNULL(ID,0)ID,GroupDescription, "+
            //" case when Bulan = 1 then 'Jan - 2018' " +
            //" when  Bulan = 2 then 'Feb - 2018' when  Bulan = 3 then 'Mrt - 2018' when  Bulan = 4 then 'Apr - 2018' when  Bulan = 5 then 'Mei - 2018' " +
            //" when  Bulan = 6 then 'Jun - 2018' when  Bulan = 7 then 'Jul - 2018' when  Bulan = 8 then 'Agst - 2018' when  Bulan = 9 then 'Sept - 2018' " +
            //" when  Bulan = 10 then 'Okt - 2018' when  Bulan = 11 then 'Nov - 2018' when  Bulan = 12 then 'Des - 2018'  " +
            //" end Periode, " +

            //" CASE WHEN  Keterangan=1 THEN 'Release' WHEN Keterangan=2 " +
            //" THEN 'Approved Mgr Log'  WHEN Keterangan=3  THEN 'Approved PM' WHEN Keterangan=4 THEN 'Email Sent'  " +
            //" ELSE 'Open' END Keterangan from (select A.GroupID,(select ID from elapbul as E where E.grouppurchn=B.ID and E.Bulan="+Bulan+" and E.Tahun="+Tahun+" and " +
            //" Rowstatus > -1)ID,B.GroupDescription,(select E1.Status from elapbul as E1 where E1.grouppurchn=B.ID and E1.Bulan=" + Bulan + " and E1.Tahun=" + Tahun + " " +
            //" and E1.Rowstatus>-1)Keterangan from ELapbul_GroupPurchn as A INNER JOIN GroupsPurchn as B ON A.GroupID=B.ID) as x1 where " + Query1 + " ";
            strSQL =
            " select ID,Data1.GroupID,NamaLaporan GroupDescription,Periode,Keterangan " +
            " from (select ISNULL(B.ID,0)ID,C.GroupDescription NamaLaporan,ISNULL(B.Cetak,'')Cetak,A.GroupID,ISNULL(B.Bulan,0)Bulan,ISNULL(B.Tahun,0)Tahun, " +
            " CASE " +
            " WHEN  B.Status=1  THEN 'Release : Next step Request Apv Mgr. Logistik' " +
            " WHEN B.Status=2  THEN 'Approved Mgr. Logistik : Next step Request Apv PM' " +

            " WHEN (B.Status=3 and B.Cetak is null)  THEN 'Approved Manager Dept. : Next step Do Print'  " +
            " WHEN (B.Status=3 and Cetak=1) THEN 'Printed : Next step Kirim Email'  " +

            " WHEN B.Status=4 THEN 'Email Sent : Finished'   ELSE 'Open : Next step Release'  END Keterangan, " +
            " CASE WHEN Bulan IS NULL and Tahun IS NULL  then '" + BulanS + " - " + Tahun + " '  WHEN Bulan = 1 then 'Januari-" + Tahun + "' " +
            " WHEN Bulan = 2 then 'Februari-" + Tahun + "' " +
            " WHEN Bulan = 3 then 'Maret-" + Tahun + "' " +
            " WHEN Bulan = 4 then 'April-" + Tahun + "' " +
            " WHEN Bulan = 5 then 'Mei-" + Tahun + "' " +
            " WHEN Bulan = 6 then 'Juni-" + Tahun + "' " +
            " WHEN Bulan = 7 then 'Juli-" + Tahun + "' " +
            " WHEN Bulan = 8 then 'Agustus-" + Tahun + "' " +
            " WHEN Bulan = 9 then 'September-" + Tahun + "' " +
            " WHEN Bulan = 10 then 'Oktober-" + Tahun + "' " +
            " WHEN Bulan = 11 then 'November-" + Tahun + "' " +
            " WHEN Bulan = 12 then 'Desember-" + Tahun + "'  end Periode " +
            " from ELapbul_GroupPurchn A  " +
            " LEFT JOIN ELapbul B ON A.GroupID=B.GroupPurchn and B.Tahun=" + Tahun + " and B.Bulan=" + Bulan + "  and B.Rowstatus>-1 " +
            " INNER JOIN GroupsPurchn C ON C.ID=A.GroupID where A." + Query1 + " ) as Data1  ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrLapBul = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrLapBul.Add(GenerateObjectLapBul(sqlDataReader));
                }
            }
            else
                arrLapBul.Add(new LaporanBulanan());

            return arrLapBul;
        }

        //public ArrayList RetrieveGroupPurchn2(int Tahun, int Bulan)
        //{
        //    string strSQL = string.Empty;
        //    strSQL = " select A.ID,B.GroupDescription,CASE WHEN  A.Status=1 THEN 'Release' WHEN A.Status=2 THEN 'Approved Mgr Log'  " +
        //    " WHEN A.Status=3 THEN 'Approved PM' WHEN A.Status=4 THEN 'Email Sent' ELSE 'Open' END Keterangan,ISNULL(A.Status,0)Status from ELapbul as A " +
        //    " INNER JOIN GroupsPurchn as B ON A.GroupPurchn=B.ID " +
        //    " where A.Rowstatus > -1 and A.Status=1 ";
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        //    strError = dataAccess.Error;
        //    arrLapBul = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrLapBul.Add(GenerateObjectLapBul22(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrLapBul.Add(new LaporanBulanan());

        //    return arrLapBul;
        //}

        public ArrayList RetrieveGroupPurchn2(int Tahun, int Bulan)
        {
            string strSQL = string.Empty;
            strSQL =
                //" select A.ID,B.GroupDescription,CASE WHEN  A.Status=1 THEN 'Release' WHEN A.Status=2 THEN 'Approved Mgr Log'  " +
                //" WHEN A.Status=3 THEN 'Approved PM' WHEN A.Status=4 THEN 'Email Sent' ELSE 'Open' END Keterangan,ISNULL(A.Status,0)Status from ELapbul as A " +
                //" INNER JOIN GroupsPurchn as B ON A.GroupPurchn=B.ID " +
                //" where A.Rowstatus > -1 and A.Status <> 4 ";

            " select ID,GroupDescription,Keterangan,Status,  case when Bulan = 1 then 'Jan-" + Tahun + "'  " +
            " when  Bulan = 2 then 'Feb-" + Tahun + "' " +
            " when  Bulan = 3 then 'Mrt-" + Tahun + "' when  Bulan = 4 then 'Apr-" + Tahun + "' " +
            " when  Bulan = 5 then 'Mei-" + Tahun + "'  when  Bulan = 6 then 'Jun-" + Tahun + "' " +
            " when  Bulan = 7 then 'Jul-" + Tahun + "' when  Bulan = 8 then 'Agst-" + Tahun + "' when  Bulan = 9 then 'Sept-" + Tahun + "'  " +
            " when  Bulan = 10 then 'Okt-" + Tahun + "' when  Bulan = 11 then 'Nov-" + Tahun + "' when  Bulan = 12 then 'Des-" + Tahun + "'  end Periode, " +
            " case when Status=1 then LEFT(convert(char,Tanggal1,106),11)  end TanggalBuat from (select A.ID,B.GroupDescription, " +
            " CASE WHEN  A.Status=1 THEN 'Release' "+

            " WHEN A.Status=3 THEN 'Approved Mgr Log'   "+

            //" WHEN A.Status=3 THEN 'Approved PM' " +

            " WHEN A.Status=4 THEN 'Email Sent' ELSE 'Open' END Keterangan,ISNULL(A.Status,0)Status,Bulan,Tahun,A.CreatedTime Tanggal1, " +
            " A.LastModifiedTime Tanggal2  " +
            " from ELapbul as A  " +
            " INNER JOIN GroupsPurchn as B ON A.GroupPurchn=B.ID  " +
            " where A.Rowstatus > -1 and A.Status = 1) as Data1 ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrLapBul = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrLapBul.Add(GenerateObjectLapBul22(sqlDataReader));
                }
            }
            else
                arrLapBul.Add(new LaporanBulanan());

            return arrLapBul;
        }

        //public ArrayList RetrieveGroupPurchn3(int Tahun, int Bulan)
        //{
        //    string strSQL = string.Empty;
        //    strSQL = " select A.ID,B.GroupDescription,CASE WHEN  A.Status=1 THEN 'Release' WHEN A.Status=2 THEN 'Approved Mgr Log'  " +
        //    " WHEN A.Status=3 THEN 'Approved PM' WHEN A.Status=4 THEN 'Email Sent' ELSE 'Open' END Keterangan,ISNULL(A.Status,0)Status from ELapbul as A " +
        //    " INNER JOIN GroupsPurchn as B ON A.GroupPurchn=B.ID " +
        //    " where A.Rowstatus > -1 and A.Status=2 ";
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        //    strError = dataAccess.Error;
        //    arrLapBul = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrLapBul.Add(GenerateObjectLapBul22(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrLapBul.Add(new LaporanBulanan());

        //    return arrLapBul;
        //}

        public ArrayList RetrieveGroupPurchn3(int Tahun, int Bulan)
        {
            string strSQL = string.Empty;
            strSQL =
            " select ID,GroupDescription,Keterangan,Status,  case when Bulan = 1 then 'Jan-" + Tahun + "'   when  Bulan = 2 then 'Feb-" + Tahun + "'  " +
            " when  Bulan = 3 then 'Mrt-" + Tahun + "' when  Bulan = 4 then 'Apr-" + Tahun + "'  when  Bulan = 5 then 'Mei-" + Tahun + "'  " +
            " when  Bulan = 6 then 'Jun-" + Tahun + "'  when  Bulan = 7 then 'Jul-" + Tahun + "' when  Bulan = 8 then 'Agst-" + Tahun + "' when  Bulan = 9 then 'Sept-" + Tahun + "' " +
            " when  Bulan = 10 then 'Okt-" + Tahun + "' when  Bulan = 11 then 'Nov-" + Tahun + "' when  Bulan = 12 then 'Des-" + Tahun + "'  end Periode, " +
            " case when Status=2 then LEFT(convert(char,Tanggal2,106),11)  end TanggalBuat from ( " +
            " select A.ID,B.GroupDescription,CASE WHEN  A.Status=1 THEN 'Release' WHEN A.Status=2 THEN 'Approved Mgr Log'  " +
            " WHEN A.Status=3 THEN 'Approved PM' WHEN A.Status=4 THEN 'Email Sent' ELSE 'Open' END Keterangan,ISNULL(A.Status,0)Status,Bulan,Tahun,A.CreatedTime Tanggal1,  A.LastModifiedTime Tanggal2 from ELapbul as A " +
            " INNER JOIN GroupsPurchn as B ON A.GroupPurchn=B.ID " +
            " where A.Rowstatus > -1 and A.Status=2 ) as Data1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrLapBul = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrLapBul.Add(GenerateObjectLapBul22(sqlDataReader));
                }
            }
            else
                arrLapBul.Add(new LaporanBulanan());

            return arrLapBul;
        }

        public LaporanBulanan cekStatus1(int Tahun, int Bulan, int GroupID)
        {
            string StrSql = "select ID,[Status] from ELapbul where Tahun=" + Tahun + " and Bulan=" + Bulan + " and GroupPurchn=" + GroupID + " and Rowstatus>-1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectLapBul23(sqlDataReader);
                }
            }

            return new LaporanBulanan();
        }

        public LaporanBulanan RetrieveTTD(int Tahun, int Bulan, int GroupID)
        {
            string StrSql = " select A.Status,A.GroupPurchn GroupID,ISNULL(B.PIC,'')PIC from Elapbul as A " +
                            " LEFT JOIN Elapbul_PIC as B ON A.GroupPurchn=B.GroupID " +
                            " where A.Tahun=" + Tahun + " and A.Bulan=" + Bulan + " and A.GroupPurchn=" + GroupID + " and A.Rowstatus>-1  and B.RowStatus > -1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectLapBul24(sqlDataReader);
                }
            }

            return new LaporanBulanan();
        }

        public LaporanBulanan GenerateObjectLapBul24(SqlDataReader sqlDataReader)
        {
            objLapBul = new LaporanBulanan();
            objLapBul.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objLapBul.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objLapBul.PIC = sqlDataReader["PIC"].ToString();
            return objLapBul;
        }

        public LaporanBulanan RetrieveTTDT13(int ID)
        {
            string StrSql = " select UserTTD1 PIC1,UserTTD2 PIC2,UserTTD3 PIC3 from Elapbul_SignT13 where UserName in " +
                            " (select top 1 CreatedBy from ELapbul_T13 where RowStatus>-1 and ID="+ID+") and  RowStatus > -1 ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectLapBul24T13(sqlDataReader);
                }
            }

            return new LaporanBulanan();
        }

        public LaporanBulanan GenerateObjectLapBul24T13(SqlDataReader sqlDataReader)
        {
            objLapBul = new LaporanBulanan();
            objLapBul.PIC1 = sqlDataReader["PIC1"].ToString();
            objLapBul.PIC2 = sqlDataReader["PIC2"].ToString();
            objLapBul.PIC3 = sqlDataReader["PIC3"].ToString();
            return objLapBul;
        }

        public LaporanBulanan GenerateObjectLapBul23(SqlDataReader sqlDataReader)
        {
            objLapBul = new LaporanBulanan();
            objLapBul.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objLapBul.ID = Convert.ToInt32(sqlDataReader["ID"]);
            return objLapBul;
        }

        public LaporanBulanan GenerateObjectLapBul25(SqlDataReader sqlDataReader)
        {
            objLapBul = new LaporanBulanan();
            objLapBul.PIC = sqlDataReader["PIC"].ToString();
            objLapBul.Sign2 = sqlDataReader["Sign2"].ToString();
            objLapBul.Sign3 = sqlDataReader["Sign3"].ToString();
            objLapBul.UserName = sqlDataReader["UserName"].ToString();

            return objLapBul;
        }

        public LaporanBulanan GenerateObjectLapBul(SqlDataReader sqlDataReader)
        {
            objLapBul = new LaporanBulanan();
            objLapBul.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objLapBul.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objLapBul.GroupDescription = sqlDataReader["GroupDescription"].ToString();
            objLapBul.Keterangan = sqlDataReader["Keterangan"].ToString();
            //objLapBul.Status = Convert.ToInt32(sqlDataReader["Status"]);
            return objLapBul;
        }

        public LaporanBulanan GenerateObjectLapBul22(SqlDataReader sqlDataReader)
        {
            objLapBul = new LaporanBulanan();
            objLapBul.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objLapBul.GroupDescription = sqlDataReader["GroupDescription"].ToString();
            objLapBul.Keterangan = sqlDataReader["Keterangan"].ToString();
            objLapBul.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objLapBul.Periode = sqlDataReader["Periode"].ToString();
            objLapBul.TanggalBuat = sqlDataReader["TanggalBuat"].ToString();
            return objLapBul;
        }

        public ArrayList cekStatus(int Tahun, int Bulan)
        {
            string StrSql = "select ID,[Status] from ELapbul where Tahun=" + Tahun + " and Bulan=" + Bulan + " and Rowstatus>-1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;
            arrLapBul = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrLapBul.Add(GenerateObjectLapBul1(sqlDataReader));
                }
            }
            else
                arrLapBul.Add(new LaporanBulanan());

            return arrLapBul;
        }

        public ArrayList cekFile(string Tahun, string Bulan, int GroupID, string UnitKerja)
        {
            string Query = string.Empty;

            if (UnitKerja != "0")
            {
                if (UnitKerja == "1")
                {
                    if (GroupID != 0)
                    {
                        if (GroupID == 1)
                        {
                            Query = "and B.GroupPurchn in (1,2,11)";
                        }
                        else if (GroupID == 2)
                        {
                            Query = "and B.GroupPurchn in (3,8,9,4,12)";
                        }
                        else if (GroupID == 3)
                        {
                            Query = "and B.GroupPurchn in (7,10,13,14)";
                        }
                        else
                        {
                            Query = "";
                        }
                    }
                }
                else if (UnitKerja == "7")
                {
                    if (GroupID != 0)
                    {
                        if (GroupID == 1)
                        {
                            Query = "and B.GroupPurchn in (3,7,8,9,10,4,5,6,12,13,14)";
                        }
                        else if (GroupID == 2)
                        {
                            Query = "and B.GroupPurchn in (1,2,11)";
                        }
                        else
                        {
                            Query = "";
                        }
                    }
                }
                else if (UnitKerja == "13")
                {
                    if (GroupID != 0)
                    {
                        if (GroupID == 1)
                        {
                            Query = "and B.GroupPurchn in (1,2,11)";
                        }
                        else if (GroupID == 2)
                        {
                            Query = "and B.GroupPurchn in (3,7,8,9,10,4,5,6,12,13,14,1,2,11)";
                        }
                        else
                        {
                            Query = "";
                        }
                    }
                }
            }                      

            string StrSql = " select A.ID,C.GroupDescription,A.FileName from Elapbul_File as A " +
                            " INNER JOIN ELapbul as B ON A.LapID=B.ID " +
                            " INNER JOIN GroupsPurchn as C ON C.ID=B.GroupPurchn " +
                            " where B.Rowstatus > -1 and A.RowStatus > -1 and B.Tahun=" + Tahun + " and B.Bulan=" + Bulan + " " + Query + "";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;
            arrLapBul = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrLapBul.Add(GenerateObjectLapBulFile(sqlDataReader));
                }
            }
            else
                arrLapBul.Add(new LaporanBulanan());

            return arrLapBul;
        }

        public ArrayList cekFileAll(string Tahun, string Bulan, string Query1)
        {
            string StrSql = " select A.ID,C.GroupDescription,A.FileName from Elapbul_File as A " +
                            " INNER JOIN ELapbul as B ON A.LapID=B.ID " +
                            " INNER JOIN GroupsPurchn as C ON C.ID=B.GroupPurchn " +
                            " where B.Rowstatus > -1 and A.RowStatus > -1 " + Query1 + " ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;
            arrLapBul = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrLapBul.Add(GenerateObjectLapBulFile(sqlDataReader));
                }
            }
            else
                arrLapBul.Add(new LaporanBulanan());

            return arrLapBul;
        }

        public ArrayList RetrieveEmailLapBul(string Tahun, string Bulan, int GroupID, string UnitKerja)
        {
            string Query = string.Empty;


            //if (GroupID != 0)
            //{
            //    if (GroupID == 1)
            //    { Query = "and B.GroupPurchn in (1,2,11)";
            //    }
            //    else if (GroupID == 2)
            //    {
            //        Query = "and B.GroupPurchn in (3,8,9)";
            //    }
            //    else if (GroupID == 3)
            //    {
            //        Query = "and B.GroupPurchn in (7,10)";
            //    }
            //    else
            //    {
            //        Query = "";
            //    }
            //}   

            if (UnitKerja != "0")
            {
                if (UnitKerja == "1")
                {
                    if (GroupID != 0)
                    {
                        if (GroupID == 1)
                        {
                            Query = "and B.GroupPurchn in (1,2,11)";
                        }
                        else if (GroupID == 2)
                        {
                            Query = "and B.GroupPurchn in (3,8,9,5,4,12)";
                        }
                        else if (GroupID == 3)
                        {
                            Query = "and B.GroupPurchn in (7,10,13,14)";
                        }
                        else
                        {
                            Query = "";
                        }
                    }
                }
                else if (UnitKerja == "7")
                {
                    if (GroupID != 0)
                    {
                        if (GroupID == 1)
                        {
                            Query = "and B.GroupPurchn in (3,7,8,9,10,4,5,6,12,13,14)";
                        }
                        else if (GroupID == 2)
                        {
                            Query = "and B.GroupPurchn in (1,2,11)";
                        }
                        else
                        {
                            Query = "";
                        }
                    }
                }
                else if (UnitKerja == "13")
                {
                    if (GroupID != 0)
                    {
                        if (GroupID == 1)
                        {
                            Query = "and B.GroupPurchn in (1,2,11)";
                        }
                        else if (GroupID == 2)
                        {
                            Query = "and B.GroupPurchn in (3,7,8,9,10,4,5,6,12,13,14,1,2,11)";
                        }
                        else
                        {
                            Query = "";
                        }
                    }
                }
            }

            string StrSql = " select B.ID,C.GroupDescription,A.FileName, case when B.Status=4 then 'Email sent' when B.Status=1 " +
                            " then 'Release' when B.Status=2 then 'Approved Mgr Log' when B.Status=3 and B.Cetak is null then 'Approved PM' "+
                            " when B.Cetak=1 and B.status=3 then 'Printed' end keterangan,isnull(B.SentEmailTime,0) as TglKirim" +
                            " from Elapbul_File as A " +
                            " INNER JOIN ELapbul as B ON A.LapID=B.ID " +
                            " INNER JOIN GroupsPurchn as C ON C.ID=B.GroupPurchn where A.RowStatus>-1 and B.Rowstatus>-1 and B.tahun=" + Tahun + " and B.bulan=" + Bulan + " " + Query + "";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;
            arrLapBul = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrLapBul.Add(GenerateObjectLapBulREmail(sqlDataReader));
                }
            }
            else
                arrLapBul.Add(new LaporanBulanan());

            return arrLapBul;
        }

        public LaporanBulanan GenerateObjectLapBulREmail(SqlDataReader sqlDataReader)
        {
            objLapBul = new LaporanBulanan();
            objLapBul.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objLapBul.GroupDescription = sqlDataReader["GroupDescription"].ToString();
            objLapBul.FileName = sqlDataReader["FileName"].ToString();
            objLapBul.Keterangan = sqlDataReader["Keterangan"].ToString();
            objLapBul.TglKirim = Convert.ToDateTime(sqlDataReader["TglKirim"].ToString());
            return objLapBul;
        }

        //public LaporanBulanan GenerateObjectLapBulREmailT13(SqlDataReader sqlDataReader)
        //{
        //    objLapBul = new LaporanBulanan();
        //    objLapBul.ID = Convert.ToInt32(sqlDataReader["ID"]);
        //    objLapBul.GroupDescription = sqlDataReader["GroupDescription"].ToString();
        //    objLapBul.FileName = sqlDataReader["FileName"].ToString();
        //    objLapBul.Keterangan = sqlDataReader["Keterangan"].ToString();
        //    objLapBul.TglKirim = Convert.ToDateTime(sqlDataReader["TglKirim"].ToString());
        //    return objLapBul;
        //}

        public ArrayList RetrieveFileName(string ID)
        {
            string StrSql = " select FileName from Elapbul_File where ID=" + ID + "";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;
            arrLapBul = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrLapBul.Add(GenerateObjectFileName(sqlDataReader));
                }
            }
            else
                arrLapBul.Add(new LaporanBulanan());

            return arrLapBul;
        }

        public LaporanBulanan GenerateObjectFileName(SqlDataReader sqlDataReader)
        {
            objLapBul = new LaporanBulanan();
            //objGroupsPurchn.ID = Convert.ToInt32(sqlDataReader["ID"]);
            //objGroupsPurchn.GroupDescription = sqlDataReader["GroupDescription"].ToString();
            objLapBul.FileName = sqlDataReader["FileName"].ToString();
            return objLapBul;
        }


        public LaporanBulanan GenerateObjectLapBulFile(SqlDataReader sqlDataReader)
        {
            objLapBul = new LaporanBulanan();
            objLapBul.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objLapBul.GroupDescription = sqlDataReader["GroupDescription"].ToString();
            objLapBul.FileName = sqlDataReader["FileName"].ToString();
            return objLapBul;
        }
        public LaporanBulanan GenerateObjectLapBul1(SqlDataReader sqlDataReader)
        {
            objLapBul = new LaporanBulanan();
            objLapBul.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objLapBul.ID = Convert.ToInt32(sqlDataReader["ID"]);
            return objLapBul;
        }

        public LaporanBulanan cekStatus(int Tahun, int Bulan, int GroupID)
        {
            string StrSql = "select ID,[Status] from ELapbul where Tahun=" + Tahun + " and Bulan=" + Bulan + " and GroupPurchn=" + GroupID + " and Rowstatus>-1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectLapBul2(sqlDataReader);
                }
            }

            return new LaporanBulanan();
        }

        public LaporanBulanan GenerateObjectLapBul2(SqlDataReader sqlDataReader)
        {
            objLapBul = new LaporanBulanan();
            objLapBul.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objLapBul.ID = Convert.ToInt32(sqlDataReader["ID"]);
            return objLapBul;
        }

        public int RetrieveGroupID(int id)
        {

            string StrSql = "select GroupPurchn from ELapbul where ID=" + id + "";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["GroupPurchn"]);
                }
            }

            return 0;
        }

        public int RetrieveFileID(int id)
        {

            string StrSql = "select Status from ELapbul where ID=" + id + "";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Status"]);
                }
            }

            return 0;
        }

        public LaporanBulanan cekUserID(int id)
        {

            string StrSql = "select flag,userid ID,GroupID,Noted,GroupName from elapbul_flag where rowstatus > -1 and userid=" + id + "";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectLapBulFlag(sqlDataReader);
                }
            }

            return new LaporanBulanan();
        }

        public LaporanBulanan cekUser(int id)
        {

            string StrSql = "select flag,userid,groupid  from elapbul_flag where rowstatus > -1 and userid=" + id + "";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectLapBulUser(sqlDataReader);
                }
            }

            return new LaporanBulanan();
        }

        public LaporanBulanan GenerateObjectLapBulUser(SqlDataReader sqlDataReader)
        {
            objLapBul = new LaporanBulanan();
            objLapBul.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objLapBul.Flag = Convert.ToInt32(sqlDataReader["Flag"]);
            objLapBul.UserID = Convert.ToInt32(sqlDataReader["UserID"]);
            return objLapBul;
        }


        public LaporanBulanan GenerateObjectLapBulFlag(SqlDataReader sqlDataReader)
        {
            objLapBul = new LaporanBulanan();
            objLapBul.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objLapBul.Flag = Convert.ToInt32(sqlDataReader["Flag"]);
            objLapBul.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objLapBul.Noted = sqlDataReader["Noted"].ToString();
            objLapBul.GroupName = sqlDataReader["GroupName"].ToString();
            return objLapBul;
        }

        public LaporanBulanan RetrieveGroupIDPurchn(int id)
        {           
            string StrSql = "select GroupPurchn GroupID,B.GroupDescription from ELapbul as A INNER JOIN GroupsPurchn as B ON A.GroupPurchn=B.ID where A.ID=" + id + "";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectLapBul3(sqlDataReader);
                }
            }

            return new LaporanBulanan();
        }

        public LaporanBulanan RetrieveFileID(string filename, int IDLap)
        {
            string StrSql = "select ISNULL(ID,0)ID from Elapbul_File where FileName='" + filename + "' and LapID=" + IDLap + " and RowStatus > -1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectRetrieveFileID(sqlDataReader);
                }
            }

            return new LaporanBulanan();
        }

        public LaporanBulanan GenerateObjectRetrieveFileID(SqlDataReader sqlDataReader)
        {
            objLapBul = new LaporanBulanan();
            objLapBul.ID = Convert.ToInt32(sqlDataReader["ID"]);
            //objLapBul.GroupDescription = sqlDataReader["GroupDescription"].ToString();
            return objLapBul;
        }

        public LaporanBulanan GenerateObjectLapBul3(SqlDataReader sqlDataReader)
        {
            objLapBul = new LaporanBulanan();
            objLapBul.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objLapBul.GroupDescription = sqlDataReader["GroupDescription"].ToString();
            return objLapBul;
        }





        // 30 Januari 2018
        // Tambahan ElapBul T13

        public LaporanBulanan cekUserT13(int id)
        {

            string StrSql = "select flag,userid,groupid  from Elapbul_FlagT13 where rowstatus > -1 and userid=" + id + "";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectLapBulUserT13(sqlDataReader);
                }
            }

            return new LaporanBulanan();
        }

        public LaporanBulanan GenerateObjectLapBulUserT13(SqlDataReader sqlDataReader)
        {
            objLapBul = new LaporanBulanan();
            objLapBul.Flag = Convert.ToInt32(sqlDataReader["Flag"]);
            objLapBul.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objLapBul.UserID = Convert.ToInt32(sqlDataReader["UserID"]);
            return objLapBul;
        }

        public ArrayList RetrieveGroupPurchn3T13(int Tahun, int Bulan)
        {
            string strSQL = string.Empty;
            strSQL = 
            //" select A.ID,B.GroupName GroupDescription,"+
            //" CASE WHEN  A.Status=1 THEN 'Release' "+
            ////" WHEN A.Status=2 THEN 'Approved Mgr Log'  " +
            //" WHEN A.Status=2 and B.DeptID=6 THEN 'Approved Mgr Log'  "+
            //" WHEN A.Status=2 and B.DeptID=3 THEN 'Approved Finishing' "+
            //" WHEN A.Status=3 THEN 'Approved PM' WHEN A.Status=4 THEN 'Email Sent' ELSE 'Open' END Keterangan,ISNULL(A.Status,0)Status from ELapbul_T13 as A " +
            //" INNER JOIN Elapbul_GroupT13 as B ON A.GroupID=B.ID " +
            //" where A.Rowstatus > -1 and A.Status <> 4 order by B.DeptID,B.GroupName";

            " select ID,GroupDescription,Keterangan,Status, " +
            " case  when Bulan = 1 then 'Jan'  + ' ' + '-' + ' ' + CAST(Data1.Tahun AS VARCHAR(4)) " +
            " when  Bulan = 2 then 'Feb'  + ' ' + '-' + ' ' + CAST(Data1.Tahun AS VARCHAR(4)) " +
            " when  Bulan = 3 then 'Mrt'  + ' ' + '-' + ' ' + CAST(Data1.Tahun AS VARCHAR(4)) " +
            " when  Bulan = 4 then 'Apr'  + ' ' + '-' + ' ' + CAST(Data1.Tahun AS VARCHAR(4)) " +
            " when  Bulan = 5 then 'Mei'  + ' ' + '-' + ' ' + CAST(Data1.Tahun AS VARCHAR(4)) " +
            " when  Bulan = 6 then 'Jun'  + ' ' + '-' + ' ' + CAST(Data1.Tahun AS VARCHAR(4)) " +
            " when  Bulan = 7 then 'Jul'  + ' ' + '-' + ' ' + CAST(Data1.Tahun AS VARCHAR(4)) " +
            " when  Bulan = 8 then 'Agst'  + ' ' + '-' + ' ' + CAST(Data1.Tahun AS VARCHAR(4)) " +
            " when  Bulan = 9 then 'Sept'  + ' ' + '-' + ' ' + CAST(Data1.Tahun AS VARCHAR(4)) " +
            " when  Bulan = 10 then 'Okt'  + ' ' + '-' + ' ' + CAST(Data1.Tahun AS VARCHAR(4)) " +
            " when  Bulan = 11 then 'Nov'  + ' ' + '-' + ' ' + CAST(Data1.Tahun AS VARCHAR(4)) " +
            " when  Bulan = 12 then 'Des'  + ' ' + '-' + ' ' + CAST(Data1.Tahun AS VARCHAR(4)) " +
            " end Periode, " +
            " LEFT(convert(char,Tanggal1,106),11) " +
            " TanggalBuat from (select A.ID,B.GroupName GroupDescription,CASE WHEN  A.Status=1 THEN 'Release' "+
            " WHEN A.Status=3 and B.DeptID=6 THEN 'Approved Mgr Log' "+
            " WHEN A.Status=3 and B.DeptID=3 THEN 'Approved Mgr Finishing'  "+
            //" WHEN A.Status=3 THEN 'Approved PM' "+
            " WHEN A.Status=4 THEN 'Email Sent' ELSE 'Open' " +
            " END Keterangan,ISNULL(A.Status,0)Status,Bulan,Tahun,A.CreatedTime Tanggal1,A.LastModifiedTime Tanggal2  from ELapbul_T13 as A  " +
            " INNER JOIN Elapbul_GroupT13 as B ON A.GroupID=B.ID  where A.Rowstatus > -1 and A.Status=2) as Data1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrLapBul = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrLapBul.Add(GenerateObjectLapBul22T13(sqlDataReader));
                }
            }
            else
                arrLapBul.Add(new LaporanBulanan());

            return arrLapBul;
        }

        public LaporanBulanan GenerateObjectLapBul22T13(SqlDataReader sqlDataReader)
        {
            objLapBul = new LaporanBulanan();
            objLapBul.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objLapBul.GroupDescription = sqlDataReader["GroupDescription"].ToString();
            objLapBul.Keterangan = sqlDataReader["Keterangan"].ToString();
            objLapBul.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objLapBul.Periode = sqlDataReader["Periode"].ToString();
            objLapBul.TanggalBuat = sqlDataReader["TanggalBuat"].ToString();
            

            return objLapBul;
        }

        public int retrieveDeptID(int id)
        {

            string StrSql = " select DeptID from Elapbul_FlagT13 where UserID=" + id + " and Rowstatus > -1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["DeptID"]);
                }
            }

            return 0;
        }

        //public ArrayList cekFileAllT13(string Tahun, string Bulan, int Flag, int DeptID)
        //{
        //    string Query1 = string.Empty;
        //    if (Flag == 1 || Flag == 3)
        //    {
        //        Query1 = "and C.DeptID=" + DeptID + "";
        //    }
        //    else if (Flag == 4)
        //    {
        //        Query1 = "";
        //    }
        //    string StrSql = " select A.ID,C.GroupName GroupDescription,A.FileName " +
        //                    " from Elapbul_FileT13 as A " +
        //                    " INNER JOIN ELapbul_T13 as B ON A.LapID=B.ID " +
        //                    " INNER JOIN Elapbul_GroupT13 as C ON C.ID=B.GroupID " +
        //                    " where B.Rowstatus > -1 and A.RowStatus > -1 and B.Tahun=" + Tahun + " and B.Bulan=" + Bulan + " " + Query1 + "";
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
        //    strError = dataAccess.Error;
        //    arrLapBul = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrLapBul.Add(GenerateObjectLapBulFile(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrLapBul.Add(new LaporanBulanan());

        //    return arrLapBul;
        //}

        public ArrayList cekFileAllT13(string Tahun, string Bulan, int Flag, int DeptID, int Apv)
        {
            string Query1 = string.Empty; string Query2 = string.Empty;

            if (Apv > 2)
            {
                Query2 = " and B.Status=2 ";
            }
            else if (Apv > 1)
            {
                Query2 = " and B.Status=1 ";
            }

            if (Flag == 1 || Flag == 3)
            {
                Query1 = "and C.DeptID=" + DeptID + "";
            }
            else if (Flag == 4)
            {
                Query1 = "";
            }
            string StrSql = " select A.ID,C.GroupName GroupDescription,A.FileName " +
                            " from Elapbul_FileT13 as A " +
                            " INNER JOIN ELapbul_T13 as B ON A.LapID=B.ID " +
                            " INNER JOIN Elapbul_GroupT13 as C ON C.ID=B.GroupID " +
                //" where B.Rowstatus > -1 and A.RowStatus > -1 and B.Tahun=" + Tahun + " and B.Bulan=" + Bulan + " " + Query1 + "";
                            " where B.Rowstatus > -1 and A.RowStatus > -1 " + Query2 + " " + Query1 + "";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;
            arrLapBul = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrLapBul.Add(GenerateObjectLapBulFile(sqlDataReader));
                }
            }
            else
                arrLapBul.Add(new LaporanBulanan());

            return arrLapBul;
        }

        public ArrayList RetrieveGroupPurchn2T13(int Tahun, int Bulan, int Flag, int DeptID)
        {
            string strSQL = string.Empty;
            string Query1 = string.Empty;

            if (Flag == 3)
            {
                Query1 = "and B.DeptID=" + DeptID + "";
            }
            
           strSQL = 
            //" select A.ID,B.GroupName GroupDescription,"+
            //" CASE WHEN  A.Status=1 THEN 'Release' "+
            ////" WHEN A.Status=2 THEN 'Approved Mgr Log'  " +
            //" WHEN A.Status=2 and B.DeptID=6 THEN 'Approved Mgr Log' "+ 
            //" WHEN A.Status=2 and B.DeptID=3 THEN 'Approved Finishing' "+
            //" WHEN A.Status=3 THEN 'Approved PM' WHEN A.Status=4 THEN 'Email Sent' ELSE 'Open' END Keterangan,ISNULL(A.Status,0)Status " +
            //" from ELapbul_T13 as A " +
            //" INNER JOIN Elapbul_GroupT13 as B ON A.GroupID=B.ID " +
            //" where A.Rowstatus > -1 and A.Status <> 4 " + Query1 + " order by B.GroupName ";

            " select ID,GroupDescription,Keterangan,Status, " +
            " case  when Bulan = 1 then 'Jan'  + ' ' + '-' + ' ' + CAST(Data1.Tahun AS VARCHAR(4)) " +
            " when  Bulan = 2 then 'Feb'  + ' ' + '-' + ' ' + CAST(Data1.Tahun AS VARCHAR(4)) " +
            " when  Bulan = 3 then 'Mrt'  + ' ' + '-' + ' ' + CAST(Data1.Tahun AS VARCHAR(4)) " +
            " when  Bulan = 4 then 'Apr'  + ' ' + '-' + ' ' + CAST(Data1.Tahun AS VARCHAR(4)) " +
            " when  Bulan = 5 then 'Mei'  + ' ' + '-' + ' ' + CAST(Data1.Tahun AS VARCHAR(4)) " +
            " when  Bulan = 6 then 'Jun'  + ' ' + '-' + ' ' + CAST(Data1.Tahun AS VARCHAR(4)) " +
            " when  Bulan = 7 then 'Jul'  + ' ' + '-' + ' ' + CAST(Data1.Tahun AS VARCHAR(4)) " +
            " when  Bulan = 8 then 'Agst'  + ' ' + '-' + ' ' + CAST(Data1.Tahun AS VARCHAR(4)) " +
            " when  Bulan = 9 then 'Sept'  + ' ' + '-' + ' ' + CAST(Data1.Tahun AS VARCHAR(4)) " +
            " when  Bulan = 10 then 'Okt'  + ' ' + '-' + ' ' + CAST(Data1.Tahun AS VARCHAR(4)) " +
            " when  Bulan = 11 then 'Nov'  + ' ' + '-' + ' ' + CAST(Data1.Tahun AS VARCHAR(4)) " +
            " when  Bulan = 12 then 'Des'  + ' ' + '-' + ' ' + CAST(Data1.Tahun AS VARCHAR(4)) " +
            " end Periode, " +
            " LEFT(convert(char,Tanggal1,106),11) " +
            " TanggalBuat from (select A.ID,B.GroupName GroupDescription,CASE WHEN  A.Status=1 THEN 'Release' "+
            " WHEN A.Status=3 and B.DeptID=6 THEN 'Approved Mgr Log' "+
            " WHEN A.Status=3 and B.DeptID=3 THEN 'Approved Mgr Finishing'  "+
            //" WHEN A.Status=3 THEN 'Approved PM' "+
            " WHEN A.Status=4 THEN 'Email Sent' ELSE 'Open' " +
            //" THEN 'Approved PM' WHEN A.Status=4 THEN 'Email Sent' ELSE 'Open' " +
            //" THEN 'Approved PM' WHEN A.Status=4 THEN 'Email Sent' ELSE 'Open' " +
            " END Keterangan,ISNULL(A.Status,0)Status,Bulan,Tahun,A.CreatedTime Tanggal1,A.LastModifiedTime Tanggal2  from ELapbul_T13 as A  " +
            " INNER JOIN Elapbul_GroupT13 as B ON A.GroupID=B.ID  where A.Rowstatus > -1 and A.Status=1) as Data1 ";

            
           

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrLapBul = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrLapBul.Add(GenerateObjectLapBul22T13(sqlDataReader));
                }
            }
            else
                arrLapBul.Add(new LaporanBulanan());

            return arrLapBul;
        }

        public int UpdateLapBulT13(object objDomain)
        {
            try
            {
                objLapBul = (LaporanBulanan)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objLapBul.ID));
                sqlListParam.Add(new SqlParameter("@Bulan", objLapBul.Bulan));
                sqlListParam.Add(new SqlParameter("@Tahun", objLapBul.Tahun));
                sqlListParam.Add(new SqlParameter("@Status", objLapBul.Status));
                sqlListParam.Add(new SqlParameter("@Users", objLapBul.Users));
                int intResult = dataAccess.ProcessData(sqlListParam, "LaporanBulananT13_sp_update");
                strError = dataAccess.Error;
                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateLapBul2T13(object objDomain)
        {
            try
            {
                objLapBul = (LaporanBulanan)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objLapBul.ID));

                sqlListParam.Add(new SqlParameter("@Users", objLapBul.Users));
                int intResult = dataAccess.ProcessData(sqlListParam, "LaporanBulananT13_sp_updateAPM");
                strError = dataAccess.Error;
                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public ArrayList cekFileT13(string Tahun, string Bulan, int FLag, int DeptID)
        {
            string Query = string.Empty;
            string Query1 = string.Empty;
            if (FLag == 1 || FLag == 3)
            {
                Query1 = "and C.DeptID=" + DeptID + "";
            }
            else if (FLag == 4)
            {
                Query1 = " ";
            }
            string StrSql = " select A.ID,C.GroupName GroupDescription,A.FileName " +
                            " from Elapbul_FileT13 as A " +
                            " INNER JOIN ELapbul_T13 as B ON A.LapID=B.ID and B.Rowstatus > -1 " +
                            " INNER JOIN Elapbul_GroupT13 as C ON B.GroupID=C.ID and C.RowStatus > -1 " +
                            " where A.RowStatus > -1 and B.Tahun=" + Tahun + " and B.Bulan=" + Bulan + " " + Query1 + " ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;
            arrLapBul = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrLapBul.Add(GenerateObjectLapBulFile(sqlDataReader));
                }
            }
            else
                arrLapBul.Add(new LaporanBulanan());

            return arrLapBul;
        }

        public ArrayList RetrieveEmailLapBulT13(string Tahun, string Bulan, int GroupID, string UnitKerja, int UserID)
        {
            string Query = string.Empty;


            string StrSql = " select B.ID,C.GroupName GroupDescription,A.FileName, case when B.Status=4 then 'Email sent : Finished' when B.Status=1 " +
                            " then 'Release : Next step Request Apv Mgr Logistik' when B.Status=2 then 'Approved Mgr Log : Next step Request Apv PM' when (B.Status=3 and B.Cetak is null) then 'Approved PM' " +
                            " when (B.Cetak=1 and B.status=3) then 'Printed : Next step SENT EMAIL' end keterangan,isnull(B.SentEmailTime,0) as TglKirim" +
                            " from Elapbul_FileT13 as A " +
                            " INNER JOIN ELapbul_T13 as B ON A.LapID=B.ID and B.RowStatus>-1" +
                            " INNER JOIN Elapbul_GroupT13 as C ON C.ID=B.GroupID and C.RowStatus>-1 where B.tahun=" + Tahun + " and B.bulan=" + Bulan + " " +
                            " and C.DeptID in (select DeptID from Elapbul_FlagT13 where UserID=" + UserID + " and RowStatus>-1) and A.Rowstatus > -1";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;
            arrLapBul = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrLapBul.Add(GenerateObjectLapBulREmail(sqlDataReader));
                }
            }
            else
                arrLapBul.Add(new LaporanBulanan());

            return arrLapBul;
        }

        public LaporanBulanan GenerateObjectLapBulREmailT13(SqlDataReader sqlDataReader)
        {
            objLapBul = new LaporanBulanan();
            objLapBul.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objLapBul.GroupDescription = sqlDataReader["GroupDescription"].ToString();
            objLapBul.FileName = sqlDataReader["FileName"].ToString();
            objLapBul.Keterangan = sqlDataReader["Keterangan"].ToString();
            objLapBul.TglKirim = Convert.ToDateTime(sqlDataReader["TglKirim"].ToString());
            return objLapBul;
        }

        //public ArrayList RetrieveLapBulT13(int Tahun, int Bulan, int DeptID)
        //{
        //    string strSQL = string.Empty;
        //    strSQL = " select GroupID,ISNULL(ID,0)ID,GroupName, " +
        //    " CASE WHEN  Keterangan=1 and Data1.DeptID=3 THEN 'Release : Next step Request Apv Mgr. Finishing'  " +
        //    " WHEN  Keterangan=1 and Data1.DeptID=6 THEN 'Release : Next step Request Apv Mgr. Logistik' " +
        //    " WHEN Keterangan=2 and Data1.DeptID=3 THEN 'Approved Mgr Finishing : Next step Request Apv PM' " +
        //    " WHEN Keterangan=2 and Data1.DeptID=6 THEN 'Approved Mgr Logistik : Next step Request Apv PM' " +
        //    " WHEN (Keterangan=3 and Cetak is null)  THEN 'Approved PM : Next step Do Print'  " +
        //    " WHEN (Keterangan=3 and Cetak=1) THEN 'Printed : Next step SENT EMAIL'  " +
        //    " WHEN Keterangan=4 THEN 'Email Sent : Finished'   ELSE 'Open : Next step Release' " +
        //    " END Keterangan " +
        //    " from (select A.ID GroupID,A.DeptID, " +
        //    " (select ID from ELapbul_T13  B where B.GroupID=A.ID and B.Bulan=" + Bulan + " and B.Tahun=" + Tahun + " and B.Rowstatus > -1)ID,A.GroupName, " +
        //    " (select B1.Status from ELapbul_T13 B1 where B1.GroupID=A.ID and B1.Bulan=" + Bulan + " and B1.Tahun=" + Tahun + "  and B1.Rowstatus>-1)Keterangan , " +
        //    " (select B2.Cetak from ELapbul_T13 B2 where B2.GroupID=A.ID and B2.Bulan=" + Bulan + " and B2.Tahun=" + Tahun + "  and B2.Rowstatus>-1)Cetak " +
        //    " from Elapbul_GroupT13 A where A.RowStatus > -1 and A.DeptID=" + DeptID + ") as Data1 ";

        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        //    strError = dataAccess.Error;
        //    arrLapBul = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrLapBul.Add(GenerateObjectLapBulT13(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrLapBul.Add(new LaporanBulanan());

        //    return arrLapBul;
        //}

        public ArrayList RetrieveLapBulT13(string BulanS, int Tahun, int Bulan, int DeptID)
        {
            string strSQL = string.Empty;
            strSQL = " select GroupID,ISNULL(ID,0)ID,GroupName, " +
            " CASE WHEN  Keterangan=1 and Data1.DeptID=3 THEN 'Release : Next step Request Apv Mgr. Finishing'  " +
            " WHEN  Keterangan=1 and Data1.DeptID=6 THEN 'Release : Next step Request Apv Mgr. Logistik' " +

            " WHEN Keterangan=2 and Data1.DeptID=3 THEN 'Approved Mgr Finishing : Next step Request Apv PM' " +
            " WHEN Keterangan=2 and Data1.DeptID=6 THEN 'Approved Mgr Logistik : Next step Request Apv PM' " +

            " WHEN (Keterangan=3 and Cetak is null and Data1.DeptID=6)  THEN 'Approved Mgr Logistik : Next step Do Print'  " +
            " WHEN (Keterangan=3 and Cetak is null and Data1.DeptID=3)  THEN 'Approved Mgr Finishing : Next step Do Print'  " +

            " WHEN (Keterangan=3 and Cetak=1) THEN 'Printed : Next step SENT EMAIL'  " +
            " WHEN Keterangan=4 THEN 'Email Sent : Finished'   ELSE 'Open : Next step Release' " +
            " END Keterangan, " +
            " CASE WHEN Bulan IS NULL and Tahun IS NULL  then '" + BulanS + " - " + Tahun + "' " +
            " WHEN Bulan = 1 then 'JANUARI -" + Tahun + "' " +
            " WHEN Bulan = 2 then 'FRBRUARI -" + Tahun + "' " +
            " WHEN Bulan = 3 then 'MARET -" + Tahun + "' " +
            " WHEN Bulan = 4 then 'APRIL -" + Tahun + "' " +
            " WHEN Bulan = 5 then 'MEI -" + Tahun + "' " +
            " WHEN Bulan = 6 then 'JUNI -" + Tahun + "' " +
            " WHEN Bulan = 7 then 'JULI -" + Tahun + "' " +
            " WHEN Bulan = 8 then 'AGUSTUS -" + Tahun + "' " +
            " WHEN Bulan = 9 then 'SEPTEMBER -" + Tahun + "' " +
            " WHEN Bulan = 10 then 'OKTOBER -" + Tahun + "' " +
            " WHEN Bulan = 11 then 'NOVEMBER -" + Tahun + "' " +
            " WHEN Bulan = 12 then 'DESEMBER -" + Tahun + "' " +
            " end Periode " +
            " from (select A.ID GroupID,A.DeptID, " +
            " (select ID from ELapbul_T13  B where B.GroupID=A.ID and B.Bulan=" + Bulan + " and B.Tahun=" + Tahun + " and B.Rowstatus > -1)ID,A.GroupName, " +
            " (select B1.Status from ELapbul_T13 B1 where B1.GroupID=A.ID and B1.Bulan=" + Bulan + " and B1.Tahun=" + Tahun + "  and B1.Rowstatus>-1)Keterangan , " +
            " (select B2.Cetak from ELapbul_T13 B2 where B2.GroupID=A.ID and B2.Bulan=" + Bulan + " and B2.Tahun=" + Tahun + "  and B2.Rowstatus>-1)Cetak, " +
            " (select B3.Bulan from ELapbul_T13 B3 where B3.GroupID=A.ID and B3.Bulan=" + Bulan + " and B3.Tahun=" + Tahun + "  and B3.Rowstatus>-1)Bulan, " +
            " (select B4.Tahun from ELapbul_T13 B4 where B4.GroupID=A.ID and B4.Bulan=" + Bulan + " and B4.Tahun=" + Tahun + "  and B4.Rowstatus>-1)Tahun " +
            " from Elapbul_GroupT13 A where A.RowStatus > -1 and A.DeptID=" + DeptID + ") as Data1 ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrLapBul = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrLapBul.Add(GenerateObjectLapBulT13(sqlDataReader));
                }
            }
            else
                arrLapBul.Add(new LaporanBulanan());

            return arrLapBul;
        }

        public LaporanBulanan GenerateObjectLapBulT13(SqlDataReader sqlDataReader)
        {
            objLapBul = new LaporanBulanan();
            objLapBul.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objLapBul.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objLapBul.GroupName = sqlDataReader["GroupName"].ToString();
            objLapBul.Keterangan = sqlDataReader["Keterangan"].ToString();
            //objLapBul.Status = Convert.ToInt32(sqlDataReader["Status"]);
            return objLapBul;
        }


        public int InsertLapBulT13(object objDomain)
        {
            try
            {
                objLapBul = (LaporanBulanan)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@GroupID", objLapBul.GroupID));
                sqlListParam.Add(new SqlParameter("@Bulan", objLapBul.Bulan));
                sqlListParam.Add(new SqlParameter("@Tahun", objLapBul.Tahun));
                sqlListParam.Add(new SqlParameter("@Status", objLapBul.Status));
                sqlListParam.Add(new SqlParameter("@Users", objLapBul.Users));
                int intResult = dataAccess.ProcessData(sqlListParam, "LaporanBulananT13_sp_insert");
                strError = dataAccess.Error;
                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public ArrayList RetrieveFileNameT13(string ID)
        {
            string StrSql = " select FileName from Elapbul_FileT13 where ID=" + ID + "";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;
            arrLapBul = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrLapBul.Add(GenerateObjectFileName(sqlDataReader));
                }
            }
            else
                arrLapBul.Add(new LaporanBulanan());

            return arrLapBul;
        }

        public LaporanBulanan cekUserIDT13(int id)
        {

            string StrSql = "select flag,userid ID,GroupID,Noted,GroupName from Elapbul_FlagT13 where rowstatus > -1 and userid=" + id + "";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectLapBulFlag(sqlDataReader);
                }
            }

            return new LaporanBulanan();
        }

        public LaporanBulanan CekAccountEmailFrom(int id)
        {

            string StrSql = " select AccountEmail,UserName from ELapbul_Email where UserID=" + id + " and RowStatus > -1 and KetEmail='From' ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectAccountEmail(sqlDataReader);
                }
            }

            return new LaporanBulanan();
        }

        public LaporanBulanan GenerateObjectAccountEmail(SqlDataReader sqlDataReader)
        {
            objLapBul = new LaporanBulanan();
            objLapBul.AccountEmail = sqlDataReader["AccountEmail"].ToString();
            objLapBul.UserName = sqlDataReader["UserName"].ToString();
            return objLapBul;
        }

        public ArrayList CekAccountEmailTo()
        {
            string strSQL = string.Empty;
            strSQL =
            " select AccountEmail,UserName from ELapbul_Email where RowStatus > -1 and KetEmail='To' ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrLapBul = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrLapBul.Add(GenerateObjectAccountEmailTo(sqlDataReader));
                }
            }
            else
                arrLapBul.Add(new LaporanBulanan());

            return arrLapBul;
        }

        public LaporanBulanan GenerateObjectAccountEmailTo(SqlDataReader sqlDataReader)
        {
            objLapBul = new LaporanBulanan();
            objLapBul.AccountEmail = sqlDataReader["AccountEmail"].ToString();
            objLapBul.UserName = sqlDataReader["UserName"].ToString();
            return objLapBul;
        }

        public ArrayList CekAccountEmailCC(int DeptID)
        {
            string strSQL = string.Empty;
            strSQL =
            " select AccountEmail,UserName from ELapbul_Email where RowStatus > -1 and KetEmail='CC' and DeptID in (0," + DeptID + ")";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrLapBul = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrLapBul.Add(GenerateObjectAccountEmailCC(sqlDataReader));
                }
            }
            else
                arrLapBul.Add(new LaporanBulanan());

            return arrLapBul;
        }

        public LaporanBulanan GenerateObjectAccountEmailCC(SqlDataReader sqlDataReader)
        {
            objLapBul = new LaporanBulanan();
            objLapBul.AccountEmail = sqlDataReader["AccountEmail"].ToString();
            objLapBul.UserName = sqlDataReader["UserName"].ToString();
            return objLapBul;
        }

        public LaporanBulanan CekAccountEmailAdmin(int id)
        {
            string StrSql = " select accountemail,password from ELapbul_accountemailadmin where UserID=" + id + " and RowStatus > -1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectAdminEmail(sqlDataReader);
                }
            }

            return new LaporanBulanan();
        }

        public LaporanBulanan GenerateObjectAdminEmail(SqlDataReader sqlDataReader)
        {
            objLapBul = new LaporanBulanan();
            objLapBul.AccountEmail = sqlDataReader["AccountEmail"].ToString();
            objLapBul.PassWord = sqlDataReader["PassWord"].ToString();
            return objLapBul;
        }

        //public ArrayList RetrieveEmailLapBul(string Tahun, string Bulan, int GroupID, string UnitKerja, int UserID)
        //{
        //    string Query = string.Empty;


        //    string StrSql = " select B.ID,C.GroupDescription,A.FileName, case when B.Status=4 then 'Email sent : Finished' when B.Status=1 " +
        //                    " then 'Release : Next step Request Apv Mgr Logistik' when B.Status=2 then 'Approved Mgr Log : Next step Request Apv PM' when (B.Status=3 and B.Cetak is null) then 'Approved PM' " +
        //                    " when (B.Cetak=1 and B.status=3) then 'Printed : Next step SENT EMAIL' end keterangan,isnull(B.SentEmailTime,0) as TglKirim" +
        //                    " from Elapbul_File as A " +
        //                    " INNER JOIN ELapbul as B ON A.LapID=B.ID " +
        //                    " INNER JOIN GroupsPurchn as C ON C.ID=B.GroupPurchn where B.tahun=" + Tahun + " and B.bulan=" + Bulan + " " +
        //                    " and B.GroupPurchn in (select GroupID from Elapbul_UserAdmin where UserID=" + UserID + ")";

        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
        //    strError = dataAccess.Error;
        //    arrLapBul = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrLapBul.Add(GenerateObjectLapBulREmail(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrLapBul.Add(new LaporanBulanan());

        //    return arrLapBul;
        //}

        //public ArrayList RetrieveEmailLapBulT13(string Tahun, string Bulan, int GroupID, string UnitKerja, int UserID)
        //{
        //    string Query = string.Empty;


        //    string StrSql = " select B.ID,C.GroupDescription,A.FileName, case when B.Status=4 then 'Email sent : Finished' when B.Status=1 " +
        //                    " then 'Release : Next step Request Apv Mgr Logistik' when B.Status=2 then 'Approved Mgr Log : Next step Request Apv PM' when (B.Status=3 and B.Cetak is null) then 'Approved PM' " +
        //                    " when (B.Cetak=1 and B.status=3) then 'Printed : Next step SENT EMAIL' end keterangan,isnull(B.SentEmailTime,0) as TglKirim" +
        //                    " from Elapbul_File as A " +
        //                    " INNER JOIN ELapbul as B ON A.LapID=B.ID " +
        //                    " INNER JOIN GroupsPurchn as C ON C.ID=B.GroupPurchn where B.tahun=" + Tahun + " and B.bulan=" + Bulan + " " +
        //                    " and B.GroupPurchn in (select GroupID from Elapbul_UserAdmin where UserID=" + UserID + ")";

        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
        //    strError = dataAccess.Error;
        //    arrLapBul = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrLapBul.Add(GenerateObjectLapBulREmailT13(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrLapBul.Add(new LaporanBulanan());

        //    return arrLapBul;
        //}


        public LaporanBulanan cekStatus1T13(int ID)
        {
            string StrSql = "select ID,[Status] from ELapbul_T13 where ID=" + ID + " and Rowstatus>-1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectLapBul23(sqlDataReader);
                }
            }

            return new LaporanBulanan();
        }

        public LaporanBulanan cekPICT13(int ID)
        {
            //string StrSql = "select top 1 PIC from Elapbul_PICT13 where userid="+ID+" and Rowstatus > -1";
            //string StrSql = " select top 1 Noted PIC from Elapbul_FlagT13 where userid=" + ID + " and Rowstatus > -1 ";
            string StrSql = " select PIC,TTD UserName,UserTTD2 Sign2,UserTTD3 Sign3 from (select A.Noted PIC,(select UserName from users B where B.ID=A.UserID )TTD " +
                            " from Elapbul_FlagT13 A where userid="+ID+" and Rowstatus > -1 ) as Data1 INNER JOIN Elapbul_SignT13 C ON " +
                            " Data1.TTD=C.UserName where C.RowStatus>-1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectLapBul25(sqlDataReader);
                }
            }

            return new LaporanBulanan();
        }

        public int RetrieveFileIDT13(int id)
        {

            string StrSql = "select Status from ELapbul_T13 where ID=" + id + "";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Status"]);
                }
            }

            return 0;
        }

        public LaporanBulanan RetrieveFileIDT13(string filename, int IDLap)
        {
            string StrSql = "select ISNULL(ID,0)ID from Elapbul_FileT13 where FileName='" + filename + "' and LapID=" + IDLap + " and RowStatus > -1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectRetrieveFileID(sqlDataReader);
                }
            }

            return new LaporanBulanan();
        }

        public int InsertLapBulFileNameT13(object objDomain)
        {
            try
            {
                objLapBul = (LaporanBulanan)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@LapID", objLapBul.LapID));
                sqlListParam.Add(new SqlParameter("@FileName", objLapBul.FileName));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objLapBul.CreatedBy));
                int intResult = dataAccess.ProcessData(sqlListParam, "LaporanBulananT13_sp_insertFile");
                strError = dataAccess.Error;
                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int cekStatusapvT13(int id)
        {

            string StrSql = "select [Status] from ELapbul_T13 where ID=" + id + "";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Status"]);
                }
            }

            return 0;
        }

        public int UpdateDataCetakT13(object objDomain)
        {
            try
            {
                objLapBul = (LaporanBulanan)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objLapBul.LapID));
                int intResult = dataAccess.ProcessData(sqlListParam, "LaporanBulananT13_sp_updateCetak");
                strError = dataAccess.Error;
                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateLapbulEmailT13(object objDomain)
        {
            try
            {
                objLapBul = (LaporanBulanan)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objLapBul.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objLapBul.LastModifiedBy));
                int intResult = dataAccess.ProcessData(sqlListParam, "LaporanBulananT13_sp_update2");
                strError = dataAccess.Error;
                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int RetrieveKunci(string Ket)
        {
            string StrSql = "select lock from elapbul_kunci where rowstatus>-1 and ELapbul='" + Ket + "' ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["lock"]);
                }
            }

            return 0;
        }

        public ArrayList CheckingDataSelisih(string SA, string TahunSA, string TahunBulan, string Tahun)
        {
            string strSQL = string.Empty;
            strSQL =
            " select PartNo,ltrim(left(CONVERT(VARCHAR,convert(money,SA),12),len(CONVERT(VARCHAR,convert(money,SA),12))-3)) as SA," +
            " ltrim(left(CONVERT(VARCHAR,convert(money,Trans),12),len(CONVERT(VARCHAR,convert(money,Trans),12))-3)) as Trans," +
            " ltrim(left(CONVERT(VARCHAR,convert(money,QtyAcc),12),len(CONVERT(VARCHAR,convert(money,QtyAcc),12))-3)) as QtyAcc, " +
            " ltrim(left(CONVERT(VARCHAR,convert(money,QtyLok),12),len(CONVERT(VARCHAR,convert(money,QtyLok),12))-3)) as QtyLok," +
            " case when selisih<0 then -1*selisih else selisih end Selisih,case when QtyLok>QtyAcc " +
            " then 'Lebih di Saldo All Lokasi' else 'Lebih di Acc Report' end Keterangan from (" +
            " select I.PartNo  ,C.* from (select * from(select ItemID, " +
            " isnull((select " + SA + " from SaldoInventoryBJ  where  YearPeriod=" + TahunSA + " and ItemID=A.ItemID),0)SA, " +
            " isnull((select SUM(qty) from vw_KartuStockBJNew  where  left(CONVERT(char,tanggal,112),6)=" + TahunBulan + " and ItemID=A.ItemID),0)Trans, " +
            " isnull((select " + SA + "  from SaldoInventoryBJ  where  YearPeriod=" + TahunSA + " and ItemID=A.ItemID),0) + " +
            " isnull((select SUM(qty) from vw_KartuStockBJNew  where  left(CONVERT(char,tanggal,112),6)=" + TahunBulan + " and ItemID=A.ItemID),0)as QtyAcc, " +
            " SUM(Qty)QtyLok,isnull((select " + SA + "  from SaldoInventoryBJ  where  YearPeriod=" + TahunSA + " and ItemID=A.ItemID),0) + " +
            " isnull((select SUM(qty) from vw_KartuStockBJNew  where  left(CONVERT(char,tanggal,112),6)=" + TahunBulan + " and ItemID=A.ItemID),0) - SUM(Qty) " +
            " as Selisih from T3_Serah A group by ItemID " +
            " ) as B) as C inner join FC_Items I on  I.ID=C.ItemID where selisih <> 0) as Data1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrLapBul = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrLapBul.Add(GenerateObjectChecking(sqlDataReader));
                }
            }
            else
                arrLapBul.Add(new LaporanBulanan());

            return arrLapBul;
        }

        public LaporanBulanan GenerateObjectChecking(SqlDataReader sqlDataReader)
        {
            objLapBul = new LaporanBulanan();
            objLapBul.Partno = sqlDataReader["Partno"].ToString();
            objLapBul.SA = sqlDataReader["SA"].ToString();
            objLapBul.Trans = Convert.ToDecimal(sqlDataReader["Trans"]);
            objLapBul.QtyAcc = Convert.ToDecimal(sqlDataReader["QtyAcc"]);
            objLapBul.QtyLok = Convert.ToDecimal(sqlDataReader["QtyLok"]);
            objLapBul.Selisih = Convert.ToDecimal(sqlDataReader["Selisih"]);
            objLapBul.Keterangan = sqlDataReader["Keterangan"].ToString();
            return objLapBul;
        }

    }
}
