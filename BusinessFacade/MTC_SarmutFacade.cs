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
    public class MTC_SarmutFacade : AbstractTransactionFacade
    {
        private MTC_Sarmut objSarmut = new MTC_Sarmut();
        private ArrayList arrSarmut;
        private SarmutMaintenance objSm = new SarmutMaintenance();
        private List<SqlParameter> sqlListParam;

        public MTC_SarmutFacade(object objDomain) : base(objDomain)
        {
            objSarmut = (MTC_Sarmut)objDomain;
        }

        public MTC_SarmutFacade()
        {

        }
        public override int Insert(DataAccessLayer.TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@SarmutID", objSarmut.SarmutID));
                sqlListParam.Add(new SqlParameter("@ItemID", objSarmut.ItemID));
                sqlListParam.Add(new SqlParameter("@DeptCode", objSarmut.DeptCode));
                sqlListParam.Add(new SqlParameter("@DeptID", objSarmut.DeptID));
                sqlListParam.Add(new SqlParameter("@Qty", objSarmut.Qty));
                sqlListParam.Add(new SqlParameter("@AvgPrice", objSarmut.AvgPrice));
                sqlListParam.Add(new SqlParameter("@SPBDate", objSarmut.SPBDate));
                sqlListParam.Add(new SqlParameter("@SPBID", objSarmut.SPBID));
                sqlListParam.Add(new SqlParameter("@itemTypeID", objSarmut.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@Kelompok", objSarmut.Kelompok));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertMTC_LapSarmut");
                strError = transManager.Error;

                return intResult;
            }
            catch (Exception e)
            {
                strError = e.Message;
                return -1;
            }
        }
        public override int Update(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@SarmutID", objSarmut.SarmutID));
                sqlListParam.Add(new SqlParameter("@ItemID", objSarmut.ItemID));
                sqlListParam.Add(new SqlParameter("@DeptCode", objSarmut.DeptCode));
                sqlListParam.Add(new SqlParameter("@DeptID", objSarmut.DeptID));
                sqlListParam.Add(new SqlParameter("@Qty", objSarmut.Qty));
                sqlListParam.Add(new SqlParameter("@AvgPrice", objSarmut.AvgPrice));
                sqlListParam.Add(new SqlParameter("@SPBDate", objSarmut.SPBDate));
                sqlListParam.Add(new SqlParameter("@SPBID", objSarmut.SPBID));
                sqlListParam.Add(new SqlParameter("@RowStatus", objSarmut.RowStatus));
                sqlListParam.Add(new SqlParameter("@ID", objSarmut.ID));
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateMTC_LapSarmut");
                strError = transManager.Error;

                return intResult;
            }
            catch (Exception e)
            {
                strError = e.Message;
                return -1;
            }
        }

        public override int Delete(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@RowStatus", objSarmut.RowStatus));
                sqlListParam.Add(new SqlParameter("@ID", objSarmut.ID));
                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteMTC_LapSarmut");
                strError = transManager.Error;

                return intResult;
            }
            catch (Exception e)
            {
                strError = e.Message;
                return -1;
            }
        }
        public override ArrayList Retrieve()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from MTC_LapSarmut and RowStatus>-1 order by ID");
            strError = dataAccess.Error;
            arrSarmut = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSarmut.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSarmut.Add(new MTC_Sarmut());

            return arrSarmut;
        }
        public ArrayList RetrieveByCriteria(string Field, string Value)
        {
            string strSQL = "Select * from MTC_LapSarmut where " + Field + " ='" + Value + "' and RowStatus >-1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSarmut = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSarmut.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSarmut.Add(new MTC_Sarmut());

            return arrSarmut;
        }

        public ArrayList RetrieveTransID(string PakaiNo, int ItemID)
        {
            string strSQL = (ItemID > 0) ?
                "select * MTC_LapSarmut where PakaiNo='" + PakaiNo + "' and ItemID=" + ItemID :
                "select * from MTC_LapSarmut where PakaiNo='" + PakaiNo + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSarmut = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSarmut.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSarmut.Add(new MTC_Sarmut());

            return arrSarmut;
        }

        public ArrayList RetrieveSarmutRpt(string StarDate,string StopDate, int DeptID)
        {
            string perDept = (DeptID == 0) ? "" : " and sm.DeptID=" + DeptID;
            string strSQL = "select ID,Kode,GroupName,isnull(avgPrice,0.00)as AvgPrice,isnull((avgPrice/Total)*100,0.00) as Persen from( "+
                            "Select a.ID,a.Kode,a.GroupName, (select sum(avgprice) from MTC_LapSarmut where convert(varchar,SPBDate,112) "+
                            "between '" + StarDate + "' and '" + StopDate + "' and SarmutID=a.ID and RowStatus >-1"+perDept+" group by SarmutID) as avgPrice, " +
                            "(select sum(avgprice) from MTC_LapSarmut where convert(varchar,SPBDate,112) between '" + StarDate + "' and '" + StopDate + "'  " +
                            "and RowStatus >-1 "+perDept+") as total from MTC_GroupSarmut as a where a.RowStatus >-1) "+
                            "as w order by w.GroupName";
            //new query
            strSQL = "select ID,Kode,GroupName, " +
                     "   sum(isnull(avgPrice,0.00))as AvgPrice," +
                     "   SUM(avgPrice)as Persen " +
                     "   from( " +
                     "   Select a.ID,a.Kode,a.GroupName, " +
                     "   (pd.Quantity* pd.AvgPrice) as AvgPrice" +
                     "   from MTC_GroupSarmut as a " +
                     "   left Join MTC_LapSarmut  as sm On sm.SarmutID=a.ID" +
                     "   left join Pakai as p on p.PakaiNo=sm.PakaiNo and p.DeptID=sm.DeptID" +
                     "   left join PakaiDetail as pd on pd.ItemID=sm.ItemID and pd.PakaiID=p.ID" +
                     "   where a.RowStatus >-1 and sm.RowStatus>-1 and pd.RowStatus>-1 and p.Status >1 "+
                     "   and sm.SPBDate between '" + StarDate + "' and '" + StopDate + "'" + perDept +
                     "   ) as w " +
                     "   group by w.ID,w.GroupName,w.Kode" +
                     "   order by w.GroupName";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSarmut = new ArrayList();
            int n = 0;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    n = n + 1;
                    arrSarmut.Add(new MTC_Sarmut
                    {
                        ID = n /*Convert.ToInt32(sqlDataReader["ID"].ToString())*/,
                        SarmutCode = sqlDataReader["Kode"].ToString(),
                        SarmutName = sqlDataReader["GroupName"].ToString(),
                        AvgPrice = Convert.ToDecimal(sqlDataReader["AvgPrice"].ToString()),
                        Qty = Convert.ToDecimal(sqlDataReader["Persen"].ToString())
                    });
                }
            }
            else
                arrSarmut.Add(new MTC_Sarmut());

            return arrSarmut;
        }

        public ArrayList RetrieveRekap(int Tahun, string Smter,string Bln)
        {
            string strSQL = RetrieveRekapSarmut(Tahun, Smter);
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSarmut = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSarmut.Add(new MTC_Sarmut
                    {
                        Tahun = Convert.ToInt32(sqlDataReader["Tahun"].ToString()),
                        Jan = Convert.ToDecimal(sqlDataReader["Jan"].ToString()),
                        Feb = Convert.ToDecimal(sqlDataReader["Feb"].ToString()),
                        Mar = Convert.ToDecimal(sqlDataReader["Mar"].ToString()),
                        Apr = Convert.ToDecimal(sqlDataReader["Apr"].ToString()),
                        Mei = Convert.ToDecimal(sqlDataReader["Mei"].ToString()),
                        Jun = Convert.ToDecimal(sqlDataReader["Jun"].ToString()),
                        Smt = Smter.ToString(),
                        TotalSM =Convert.ToDecimal(sqlDataReader["Total"].ToString()),
                        Budget=Convert.ToDecimal(sqlDataReader["Bugdet"].ToString()),
                        Total = (Convert.ToDecimal(sqlDataReader["Bugdet"].ToString()) == 0) ? 0 :
                                (Convert.ToDecimal(sqlDataReader[Bln].ToString()) / 
                                Convert.ToDecimal(sqlDataReader["Bugdet"].ToString())*100),
                        Qty = Convert.ToDecimal(sqlDataReader[Bln].ToString())
                    });
                }
            }else
                arrSarmut.Add(new MTC_Sarmut());

            return arrSarmut;
        }
        public MTC_Sarmut RetrieveByID(int SarmutID)
        {
            string strSQL = "select * from MTC_LapSarmut where SarmutID=" + SarmutID + " and RowStatus >-1";
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
            return new MTC_Sarmut();
        }
        public MTC_Sarmut GenerateObject2(SqlDataReader sqlDataReader)
        {
            /* for report sarmut*/
            objSarmut = new MTC_Sarmut();
            objSarmut.ID = int.Parse(sqlDataReader ["ID"].ToString());
            objSarmut.SarmutCode = sqlDataReader["Kode"].ToString();
            objSarmut.SarmutName = sqlDataReader["GroupName"].ToString();
            objSarmut.AvgPrice = Convert.IsDBNull(sqlDataReader["avgPrice"])?0: Convert.ToDecimal(sqlDataReader["avgPrice"]);
            return objSarmut;
        }
        public MTC_Sarmut GenerateObject(SqlDataReader sqlDataReader)
        {
            objSarmut = new MTC_Sarmut();
            objSarmut.ID = int.Parse(sqlDataReader["ID"].ToString());
            objSarmut.DeptCode = sqlDataReader["DeptKode"].ToString();
            objSarmut.DeptID = int.Parse(sqlDataReader["DeptID"].ToString());
            objSarmut.ItemID = int.Parse(sqlDataReader["ItemID"].ToString());
            objSarmut.Qty = Convert.ToDecimal(sqlDataReader["Qty"].ToString());
            objSarmut.AvgPrice = Convert.ToDecimal(sqlDataReader["AvgPrice"].ToString());
            objSarmut.SPBDate = Convert.ToDateTime(sqlDataReader["SPBDate"].ToString());
            objSarmut.SPBID = sqlDataReader["PakaiNo"].ToString();
            return objSarmut;
        }
        /**
         * Retrieve for report sarmut
         */
        public string RetrieveBySarmut(string StarDate, string StopDate,int DeptID)
        {
            string perDept = (DeptID == 0) ? "" : " and DeptID=" + DeptID;
            string strSQL = "select ID,Kode,GroupName,isnull(avgPrice,0.00)as AvgPrice,isnull((avgPrice/Total)*100,0.00) as Persen from( " +
                            "Select a.ID,a.Kode,a.GroupName, (select sum(avgprice) from MTC_LapSarmut where convert(varchar,SPBDate,112) " +
                            "between '" + StarDate + "' and '" + StopDate + "' and SarmutID=a.ID and RowStatus >-1"+perDept+" group by SarmutID) as avgPrice, " +
                            "(select sum(avgprice) from MTC_LapSarmut where convert(varchar,SPBDate,112) between '" + StarDate + "' and '" + StopDate + "'  " +
                            "and RowStatus >-1 "+perDept+") as total from MTC_GroupSarmut as a where a.RowStatus >-1) " +
                            "as w order by w.GroupName";
            return strSQL;
        }

        public string RetrieveRekapSarmut(int tahun,string smt)
        {
            string fld = (smt == "I") ? "isnull(Jan,0) as Jan,isnull(Feb,0)Feb,isnull(Mar,0)Mar,isnull(Apr,0)Apr,isnull(Mei,0)Mei,isnull(Jun,0)Jun" : 
                                "isnull(Jul,0) as Jan,isnull(Ags,0) as Feb,isnull(Sept,0) as Mar,isnull(Okt,0) as Apr,isnull(Nov,0) as Mei,isnull(Des,0) as Jun";
            string total = (smt == "I") ? " (isnull(Jan,0)+isnull(Feb,0)+isnull(Mar,0)+isnull(Apr,0)+isnull(Mei,0)+isnull(Jun,0)) as Total" : 
                    "(isnull(Jul,0)+isnull(Ags,0)+isnull(Sept,0)+isnull(Okt,0)+isnull(Nov,0)+isnull(Des,0)) as Total";
            string strSQL = "Select ID,Tahun,"+fld+","+ total+","+Budget(smt)+",'"+smt+"' as Smt from vw_rekapsarmut where Tahun="+tahun;

            return strSQL;
        }
        public string Budget(string smt)
        {
            return "isnull((select jumlah from MTC_TargetBudget where Tahun=vw_rekapsarmut.Tahun and Smt='" + smt + "'),0) as Bugdet";

        }
        /**
         * Perawatan Kendaraan
         * added on 30-06-2013
         */
        public ArrayList RetrieveListKendaraan(int Plant)
        {
            string strSQL = "select ISNULL(IDKend,0)ID,NoPol,NamaKendaraan from( "+
                            "Select (select top 1 IDKendaraan from MTC_Armada where NoPol=MTC_NamaArmada.NoPol) as IDKend, "+
                            "*,left(NamaKendaraan,CHARINDEX(' ',NamaKendaraan,1)) nama, "+
                            "CASE WHEN ISNUMERIC(right(NamaKendaraan,(LEN(NamaKendaraan)-CHARINDEX(' ',NamaKendaraan,1))))=1 THEN "+
	                        "CAST(right(NamaKendaraan,(LEN(NamaKendaraan)-CHARINDEX(' ',NamaKendaraan,1))) as INT) "+
	                        "ELSE 0 END Nomor "+
                            "from MTC_NamaArmada where Lokasi="+Plant+" ) "+
                            "as w " +
                            "order by nama,Nomor";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSarmut = new ArrayList();
            int n = 0;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    n = n + 1;
                    arrSarmut.Add(new MTC_Sarmut
                    {
                        ID = n,
                        IDKendaraan=Convert.ToInt32(sqlDataReader["ID"].ToString()),
                        NoPol = sqlDataReader["NoPol"].ToString() + " - " + sqlDataReader["NamaKendaraan"]
                    });
                }
                arrSarmut.Add(new MTC_Sarmut
                {
                    ID = n + 1,
                    IDKendaraan = 1001,
                    NoPol = "Forklift"
                });
                arrSarmut.Add(new MTC_Sarmut
                {
                    ID = n + 2,
                    IDKendaraan = 1000,
                    NoPol = "Umum"
                });
            }
            else
            {
                arrSarmut.Add(new MTC_Sarmut());
            }
            return arrSarmut;
        }
        public MTC_Sarmut RetriveFoRekap(int IDKendaraan,string FromDate, string ToDate)
        {
            string mobil = (IDKendaraan == 0) ? "" : " and IDKendaraan=" + IDKendaraan;
            string strSQL = "select isnull(SUM(Quantity*AvgPrice),0) as Total from MTC_Armada " +
                            "where Convert(VARCHAR,SPBDate,112) between '" + FromDate + "' and '" + ToDate + "' and RowStatus >-1 " +
                            mobil + " group by IDKendaraan";
            strSQL = "SELECT sum(mt.Quantity*pd.AvgPrice)Total " +
                    "FROM MTC_Armada as mt " +
                    "LEFT JOIN Pakai as p ON p.PakaiNo=mt.SPBNo and p.DeptID=mt.DeptID " +
                    "LEFT JOIN PakaiDetail as pd ON pd.ItemID=mt.ItemID and pd.PakaiID=p.ID " +
                    "where Convert(VARCHAR,SPBDate,112) between '" + FromDate + "' and '" + ToDate + "'  " +
                    "and pd.RowStatus >-1 and p.Status>1 and mt.RowStatus>-1 " +
                       mobil + " group by IDKendaraan";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSarmut = new ArrayList();
            int n = 0;
            decimal total = 0;
            if (sqlDataReader.HasRows)
            {
                
                while (sqlDataReader.Read())
                {
                    n = n + 1;
                    total += Convert.ToDecimal(sqlDataReader["Total"]);
                    objSarmut = new MTC_Sarmut
                    {
                        ID = n,
                        AvgPrice = Convert.ToDecimal(sqlDataReader["Total"].ToString()),
                        Qty = total
                    };
                    
                }
                return objSarmut;
            }
            else
                return new MTC_Sarmut();
           
        }
        public decimal RetrieveTotal(string FromDate, string ToDate, string Kendaraan)
        {
            string strSQL = "select isnull(SUM(Quantity*AvgPrice),0) as Total from MTC_Armada " +
                            "where Convert(VARCHAR,SPBDate,112) between '" + FromDate + "' and '" + ToDate + "' and RowStatus >-1 " +
                            Kendaraan;
            strSQL = "SELECT sum(mt.Quantity)mt,sum(pd.Quantity)pd, " +
                     "sum(mt.Quantity*pd.AvgPrice)Total " +
                     "FROM MTC_Armada as mt " +
                     "LEFT JOIN Pakai as p ON p.PakaiNo=mt.SPBNo and p.DeptID=mt.DeptID " +
                     "LEFT JOIN PakaiDetail as pd ON pd.ItemID=mt.ItemID and pd.PakaiID=p.ID " +
                     "where Convert(VARCHAR,SPBDate,112) between '" + FromDate + "' and '" + ToDate + "'  " +
                     "and pd.RowStatus >-1 and p.Status>1 and mt.RowStatus>-1 " +
                        Kendaraan;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSarmut = new ArrayList();
            int n = 0;
            //decimal total = 0;
            if (sqlDataReader.HasRows)
            {

                while (sqlDataReader.Read())
                {
                    return Convert.ToDecimal(sqlDataReader["Total"].ToString());
                }
            }
            return 0;
        }
        public ArrayList DetailSarmut(int ID, string Dari, string Sampai, string Zona)
        {
            #region oldQurey
            string strSQL = "select * from ( "+
                            "select *,(Qty*avgPrice)Harga from (select SPBDate, " +
                            "    Case ItemTypeID " +
                            "    When 1 Then (Select Itemcode From Inventory where ID=ItemID) " +
                            "    When 2 Then (select ItemCode from Asset where ID=ItemID) " +
                            "    When 3 Then (select ItemCode from Biaya where ID=ItemID) END ItemCode, " +
                            "    Case ItemTypeID " +
                            "    When 1 Then (Select ItemName From Inventory where ID=ItemID) " +
                            "    When 2 Then (select ItemName from Asset where ID=ItemID) " +
                            "    When 3 Then (select ItemName from Biaya where ID=ItemID) END ItemName, " +
                            "    Case ItemTypeID " +
                            "    When 1 Then (select UomCode from Uom where ID=(Select UomID From Inventory where ID=ItemID)) " +
                            "    When 2 Then (select UomCode from Uom where ID=(select UomID from Asset where ID=ItemID)) " +
                            "    When 3 Then (select UomCode from Uom where ID=(select UomID from Biaya where ID=ItemID)) END Unit, " +
                            "    Qty,/*(avgPrice/Qty)avgPrice,*/" +
                            "   isnull((select isnull(AvgPrice,0)AvgPrice from PakaiDetail where PakaiID in( " +
                            "   select ID from Pakai where PakaiNo=MTC_LapSarmut.PakaiNo and RowStatus>-1 ) " +
                            "   and ItemID=MTC_LapSarmut.ItemID and RowStatus>-1),0)as avgPrice, " +
                            "   (Select ZonaMTC from PakaiDetail where PakaiID in(select ID from Pakai where PakaiNo=MTC_LapSarmut.PakaiNo " +
                            "   and RowStatus>-1 ) and ItemID=MTC_LapSarmut.ItemID and RowStatus>-1) as ZonaMTC " +
                            "from MTC_LapSarmut " +
                            "Where SarmutID=" + ID + " and Convert(Char,SPBDate,112) Between '" + Dari + "' and '" + Sampai + "'  and RowStatus >-1 ) as w " +
                            ") as xx "+ Zona +
                            "order by xx.SPBDate,ItemCode";
            #endregion
            #region secondQuery -- not used
            strSQL =
                            "select *,(Qty*avgPrice)Harga from (select SPBDate, " +
                            "    Case ItemTypeID " +
                            "    When 1 Then (Select Itemcode From Inventory where ID=ItemID) " +
                            "    When 2 Then (select ItemCode from Asset where ID=ItemID) " +
                            "    When 3 Then (select ItemCode from Biaya where ID=ItemID) END ItemCode, " +
                            "    Case ItemTypeID " +
                            "    When 1 Then (Select ItemName From Inventory where ID=ItemID) " +
                            "    When 2 Then (select ItemName from Asset where ID=ItemID) " +
                            "    When 3 Then (select ItemName from Biaya where ID=ItemID) END ItemName, " +
                            "    Case ItemTypeID " +
                            "    When 1 Then (select UomCode from Uom where ID=(Select UomID From Inventory where ID=ItemID)) " +
                            "    When 2 Then (select UomCode from Uom where ID=(select UomID from Asset where ID=ItemID)) " +
                            "    When 3 Then (select UomCode from Uom where ID=(select UomID from Biaya where ID=ItemID)) END Unit, " +
                            "    Qty,/*(avgPrice/Qty)avgPrice,*/" +
                            "   isnull((select isnull(AvgPrice,0)AvgPrice from PakaiDetail where PakaiID in( " +
                            "   select ID from Pakai where PakaiNo=MTC_LapSarmut.PakaiNo and RowStatus>-1 ) " +
                            "   and ItemID=MTC_LapSarmut.ItemID and RowStatus>-1),0)as avgPrice, " +
                            "   (Select ZonaMTC from PakaiDetail where PakaiID in(select ID from Pakai where PakaiNo=MTC_LapSarmut.PakaiNo " +
                            "   and RowStatus>-1 ) and ItemID=MTC_LapSarmut.ItemID and RowStatus>-1) as ZonaMTC, " +
                            "  (Select Keterangan from PakaiDetail where PakaiID in(select ID from Pakai where PakaiNo=MTC_LapSarmut.PakaiNo " +
                            "   and RowStatus>-1 ) and ItemID=MTC_LapSarmut.ItemID and RowStatus>-1) as keterangan " +
                            "from MTC_LapSarmut " +
                            "Where SarmutID=" + ID + " and Convert(Char,SPBDate,112) Between '" + Dari + "' and '" + Sampai + "'  and RowStatus >-1 /*) as w*/ " +
                            ") as xx " + Zona +
                            "order by xx.SPBDate,ItemCode";
            #endregion
            #region NewQuery
            strSQL = "SELECT ls.SPBDate,ls.PakaiNo, " +
                     "Case  ls.ItemTypeID  " +
                     "   When 1 Then (select dbo.ItemCodeInv(ls.ItemID,1))  " +
                     "   when 2 then (select dbo.ItemCodeInv(ls.ItemID,2)) else (select dbo.ItemCodeInv(ls.ItemID,3)) end ItemCode,  " +
                     "Case  ls.ItemTypeID  " +
                     "   When 1 Then (select dbo.ItemNameInv(ls.ItemID,1))  " +
                     "   when 2 then (select dbo.ItemNameInv(ls.ItemID,2)) else (select dbo.ItemNameInv(ls.ItemID,3)) end ItemName, " +
                     "   (select UomCode from UOM where ID=xx.UomID) as Unit, " +
                     "xx.Quantity,isnull(xx.AvgPrice,0)AvgPrice,ISNULL((xx.Quantity*xx.AvgPrice),0)Harga,xx.ZonaMTC,xx.Keterangan " +
                     "    FROM MTC_LapSarmut ls " +
                     "   LEFT JOIN Pakai as P ON P.PakaiNo=ls.PakaiNo and P.DeptID=ls.DeptID " +
                     "   LEFT JOIN PakaiDetail as xx ON  xx.PakaiID=p.ID and xx.ItemID=ls.ItemID " +
                     "   /*LEFT JOIN Inventory as iv on iv.ID=ls.ItemID " +
                     "   LEFT JOIN Biaya as b on b.ID=ls.ItemID " +
                     "   LEFT JOIN Asset as a on a.ID=ls.ItemID " +
                     "   LEFT JOIN UOM as u on u.ID=iv.UOMID or u.ID=b.UOMID or u.ID=a.UOMID*/ " +
                     "   where ls.SarmutID=" + ID + " and Convert(Char,ls.SPBDate,112) Between '" + Dari + "' and '" + Sampai + "'  " +
                     "   and xx.RowStatus>-1 and P.Status >1 and ls.RowStatus>-1 " + Zona +
                // Tambahan Retur Pakai
                     "  UNION ALL " +
                     "  select ls.ReturDate,ls.ReturNo, Case  B.ItemTypeID     When 1 Then (select dbo.ItemCodeInv(B.ItemID,1)) " +
                     "  when 2 then (select dbo.ItemCodeInv(B.ItemID,2)) else (select dbo.ItemCodeInv(B.ItemID,3)) end ItemCode, " +
                     "  Case  B.ItemTypeID     When 1 Then (select dbo.ItemNameInv(B.ItemID,1)) when 2 then (select dbo.ItemNameInv(B.ItemID,2)) " +
                     "  else (select dbo.ItemNameInv(B.ItemID,3)) end ItemName,    (select UomCode from UOM where ID=B.UomID) " +
                     "  as Unit,(-1*(B.Quantity))Quantity,isnull(B.AvgPrice,0)AvgPrice,ISNULL((-1*(B.Quantity)*B.AvgPrice),0)Harga,xx.ZonaMTC, " +
                     "  B.Keterangan from ReturPakai as ls " +
                     "  INNER JOIN ReturPakaiDetail as B ON ls.ID=B.ReturID " +
                     "  LEFT JOIN Pakai as C ON ls.PakaiNo=C.PakaiNo and ls.ItemTypeID=C.ItemTypeID " +
                     "  LEFT JOIN PakaiDetail as xx ON C.ID=xx.PakaiID and xx.ItemID=B.ItemID and xx.GroupID=B.GroupID and C.DeptID=ls.DeptID " +
                     "  LEFT JOIN MTC_LapSarmut as E ON E.PakaiNo=ls.PakaiNo and E.ItemID=B.ItemID " +
                     "  where E.SarmutID=" + ID + " " + Zona + " and ls.ReturDate between '" + Dari + "' and '" + Sampai + "'"+
                     "  order by ls.SPBDate,ItemCode";
            #endregion
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSarmut = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSarmut.Add(new MTC_Sarmut
                    {
                        SPBDate = Convert.ToDateTime(sqlDataReader["SPBDate"].ToString()),
                        SarmutCode = sqlDataReader["ItemCode"].ToString(),
                        SarmutName = sqlDataReader["ItemName"].ToString(),
                        SPBID = sqlDataReader["Unit"].ToString(),
                        Qty = Convert.ToDecimal(sqlDataReader["Quantity"].ToString()),
                        AvgPrice = Convert.ToDecimal(sqlDataReader["avgPrice"].ToString()),
                        Total = Convert.ToDecimal(sqlDataReader["Harga"].ToString()),
                        ZonaMTC = sqlDataReader["ZonaMTC"].ToString(),
                        keterangan = sqlDataReader["keterangan"].ToString(),
                        NoPol=sqlDataReader["PakaiNo"].ToString()
                    });
                }
            }
            return arrSarmut;
        }
        public ArrayList DetailSarmutNewReg(int ID, string ThnBln, string Zona)
        {
            
            #region NewQuery
            string strSQL = "SELECT ls.SPBDate,ls.PakaiNo, " +
                     "Case  ls.ItemTypeID  " +
                     "   When 1 Then (select dbo.ItemCodeInv(ls.ItemID,1))  " +
                     "   when 2 then (select dbo.ItemCodeInv(ls.ItemID,2)) else (select dbo.ItemCodeInv(ls.ItemID,3)) end ItemCode,  " +
                     "Case  ls.ItemTypeID  " +
                     "   When 1 Then (select dbo.ItemNameInv(ls.ItemID,1))  " +
                     "   when 2 then (select dbo.ItemNameInv(ls.ItemID,2)) else (select dbo.ItemNameInv(ls.ItemID,3)) end ItemName, " +
                     "   (select UomCode from UOM where ID=xx.UomID) as Unit, " +
                     "xx.Quantity,isnull(xx.AvgPrice,0)AvgPrice,ISNULL((xx.Quantity*xx.AvgPrice),0)Harga,xx.ZonaMTC,xx.Keterangan,isnull(xx.groupsp,'')groupsp " +
                     "    FROM MTC_LapSarmut ls " +
                     "   LEFT JOIN Pakai as P ON P.PakaiNo=ls.PakaiNo and P.DeptID=ls.DeptID " +
                     "   LEFT JOIN PakaiDetail as xx ON  xx.PakaiID=p.ID and xx.ItemID=ls.ItemID " +
                     "   /*LEFT JOIN Inventory as iv on iv.ID=ls.ItemID " +
                     "   LEFT JOIN Biaya as b on b.ID=ls.ItemID " +
                     "   LEFT JOIN Asset as a on a.ID=ls.ItemID " +
                     "   LEFT JOIN UOM as u on u.ID=iv.UOMID or u.ID=b.UOMID or u.ID=a.UOMID*/ " +
                     "   where xx.groupsp like 'regular%' and  ls.SarmutID=" + ID + " and left(Convert(Char,ls.SPBDate,112),6) = '" + ThnBln + "'  " +
                     "   and xx.RowStatus>-1 and P.Status >1 and ls.RowStatus>-1 " + Zona +
                // Tambahan Retur Pakai
                     "  UNION ALL " +
                     "  select ls.ReturDate,ls.ReturNo, Case  B.ItemTypeID     When 1 Then (select dbo.ItemCodeInv(B.ItemID,1)) " +
                     "  when 2 then (select dbo.ItemCodeInv(B.ItemID,2)) else (select dbo.ItemCodeInv(B.ItemID,3)) end ItemCode, " +
                     "  Case  B.ItemTypeID     When 1 Then (select dbo.ItemNameInv(B.ItemID,1)) when 2 then (select dbo.ItemNameInv(B.ItemID,2)) " +
                     "  else (select dbo.ItemNameInv(B.ItemID,3)) end ItemName,    (select UomCode from UOM where ID=B.UomID) " +
                     "  as Unit,(-1*(B.Quantity))Quantity,isnull(B.AvgPrice,0)AvgPrice,ISNULL((-1*(B.Quantity)*B.AvgPrice),0)Harga,xx.ZonaMTC, " +
                     "  B.Keterangan,isnull(xx.groupsp,'')groupsp from ReturPakai as ls " +
                     "  INNER JOIN ReturPakaiDetail as B ON ls.ID=B.ReturID " +
                     "  LEFT JOIN Pakai as C ON ls.PakaiNo=C.PakaiNo and ls.ItemTypeID=C.ItemTypeID " +
                     "  LEFT JOIN PakaiDetail as xx ON C.ID=xx.PakaiID and xx.ItemID=B.ItemID and xx.GroupID=B.GroupID and C.DeptID=ls.DeptID " +
                     "  LEFT JOIN MTC_LapSarmut as E ON E.PakaiNo=ls.PakaiNo and E.ItemID=B.ItemID " +
                     "  where xx.groupsp like 'regular%' and E.SarmutID=" + ID + " " + Zona + " and left(Convert(Char,ls.ReturDate,112) ,6) = '" + ThnBln + "'  " +
                     "  order by ls.SPBDate,ItemCode";
            #endregion
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSarmut = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSarmut.Add(new MTC_Sarmut
                    {
                        SPBDate = Convert.ToDateTime(sqlDataReader["SPBDate"].ToString()),
                        SarmutCode = sqlDataReader["ItemCode"].ToString(),
                        SarmutName = sqlDataReader["ItemName"].ToString(),
                        SPBID = sqlDataReader["Unit"].ToString(),
                        Qty = Convert.ToDecimal(sqlDataReader["Quantity"].ToString()),
                        AvgPrice = Convert.ToDecimal(sqlDataReader["avgPrice"].ToString()),
                        Total = Convert.ToDecimal(sqlDataReader["Harga"].ToString()),
                        ZonaMTC = sqlDataReader["ZonaMTC"].ToString(),
                        keterangan = sqlDataReader["keterangan"].ToString(),
                        NoPol = sqlDataReader["PakaiNo"].ToString(),
                        Groupsp = sqlDataReader["Groupsp"].ToString()
                    });
                }
            }
            return arrSarmut;
        }
        public ArrayList DetailSarmutNewNonReg(int ID, string ThnBln, string Zona)
        {
            #region NewQuery
            string strSQL = "SELECT ls.SPBDate,ls.PakaiNo, " +
                     "Case  ls.ItemTypeID  " +
                     "   When 1 Then (select dbo.ItemCodeInv(ls.ItemID,1))  " +
                     "   when 2 then (select dbo.ItemCodeInv(ls.ItemID,2)) else (select dbo.ItemCodeInv(ls.ItemID,3)) end ItemCode,  " +
                     "Case  ls.ItemTypeID  " +
                     "   When 1 Then (select dbo.ItemNameInv(ls.ItemID,1))  " +
                     "   when 2 then (select dbo.ItemNameInv(ls.ItemID,2)) else (select dbo.ItemNameInv(ls.ItemID,3)) end ItemName, " +
                     "   (select UomCode from UOM where ID=xx.UomID) as Unit, " +
                     "xx.Quantity,isnull(xx.AvgPrice,0)AvgPrice,ISNULL((xx.Quantity*xx.AvgPrice),0)Harga,xx.ZonaMTC,xx.Keterangan,isnull(xx.groupsp,'')groupsp " +
                     "    FROM MTC_LapSarmut ls " +
                     "   LEFT JOIN Pakai as P ON P.PakaiNo=ls.PakaiNo and P.DeptID=ls.DeptID " +
                     "   LEFT JOIN PakaiDetail as xx ON  xx.PakaiID=p.ID and xx.ItemID=ls.ItemID " +
                     "   /*LEFT JOIN Inventory as iv on iv.ID=ls.ItemID " +
                     "   LEFT JOIN Biaya as b on b.ID=ls.ItemID " +
                     "   LEFT JOIN Asset as a on a.ID=ls.ItemID " +
                     "   LEFT JOIN UOM as u on u.ID=iv.UOMID or u.ID=b.UOMID or u.ID=a.UOMID*/ " +
                     "   where xx.groupsp like 'Non%' and  ls.SarmutID=" + ID + " and left(Convert(Char,ls.SPBDate,112),6) = '" + ThnBln + "'  " +
                     "   and xx.RowStatus>-1 and P.Status >1 and ls.RowStatus>-1 " + Zona +
                // Tambahan Retur Pakai
                     "  UNION ALL " +
                     "  select ls.ReturDate,ls.ReturNo, Case  B.ItemTypeID     When 1 Then (select dbo.ItemCodeInv(B.ItemID,1)) " +
                     "  when 2 then (select dbo.ItemCodeInv(B.ItemID,2)) else (select dbo.ItemCodeInv(B.ItemID,3)) end ItemCode, " +
                     "  Case  B.ItemTypeID     When 1 Then (select dbo.ItemNameInv(B.ItemID,1)) when 2 then (select dbo.ItemNameInv(B.ItemID,2)) " +
                     "  else (select dbo.ItemNameInv(B.ItemID,3)) end ItemName,    (select UomCode from UOM where ID=B.UomID) " +
                     "  as Unit,(-1*(B.Quantity))Quantity,isnull(B.AvgPrice,0)AvgPrice,ISNULL((-1*(B.Quantity)*B.AvgPrice),0)Harga,xx.ZonaMTC, " +
                     "  B.Keterangan,isnull(xx.groupsp,'')groupsp from ReturPakai as ls " +
                     "  INNER JOIN ReturPakaiDetail as B ON ls.ID=B.ReturID " +
                     "  LEFT JOIN Pakai as C ON ls.PakaiNo=C.PakaiNo and ls.ItemTypeID=C.ItemTypeID " +
                     "  LEFT JOIN PakaiDetail as xx ON C.ID=xx.PakaiID and xx.ItemID=B.ItemID and xx.GroupID=B.GroupID and C.DeptID=ls.DeptID " +
                     "  LEFT JOIN MTC_LapSarmut as E ON E.PakaiNo=ls.PakaiNo and E.ItemID=B.ItemID " +
                     "  where xx.groupsp like 'Non%' and E.SarmutID=" + ID + " " + Zona + " and left(Convert(Char,ls.ReturDate,112) ,6) = '" + ThnBln + "'  " +
                     "  order by ls.SPBDate,ItemCode";
            #endregion
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSarmut = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSarmut.Add(new MTC_Sarmut
                    {
                        SPBDate = Convert.ToDateTime(sqlDataReader["SPBDate"].ToString()),
                        SarmutCode = sqlDataReader["ItemCode"].ToString(),
                        SarmutName = sqlDataReader["ItemName"].ToString(),
                        SPBID = sqlDataReader["Unit"].ToString(),
                        Qty = Convert.ToDecimal(sqlDataReader["Quantity"].ToString()),
                        AvgPrice = Convert.ToDecimal(sqlDataReader["avgPrice"].ToString()),
                        Total = Convert.ToDecimal(sqlDataReader["Harga"].ToString()),
                        ZonaMTC = sqlDataReader["ZonaMTC"].ToString(),
                        keterangan = sqlDataReader["keterangan"].ToString(),
                        NoPol = sqlDataReader["PakaiNo"].ToString(),
                        Groupsp = sqlDataReader["Groupsp"].ToString()
                    });
                }
            }
            return arrSarmut;
        }
        public ArrayList DetailSarmutNewLain(int ID, string ThnBln, string Zona)
        {
            #region NewQuery
            string strSQL = "SELECT ls.SPBDate,ls.PakaiNo, " +
                     "Case  ls.ItemTypeID  " +
                     "   When 1 Then (select dbo.ItemCodeInv(ls.ItemID,1))  " +
                     "   when 2 then (select dbo.ItemCodeInv(ls.ItemID,2)) else (select dbo.ItemCodeInv(ls.ItemID,3)) end ItemCode,  " +
                     "Case  ls.ItemTypeID  " +
                     "   When 1 Then (select dbo.ItemNameInv(ls.ItemID,1))  " +
                     "   when 2 then (select dbo.ItemNameInv(ls.ItemID,2)) else (select dbo.ItemNameInv(ls.ItemID,3)) end ItemName, " +
                     "   (select UomCode from UOM where ID=xx.UomID) as Unit, " +
                     "xx.Quantity,isnull(xx.AvgPrice,0)AvgPrice,ISNULL((xx.Quantity*xx.AvgPrice),0)Harga,xx.ZonaMTC,xx.Keterangan,isnull(xx.groupsp,'')groupsp " +
                     "    FROM MTC_LapSarmut ls " +
                     "   LEFT JOIN Pakai as P ON P.PakaiNo=ls.PakaiNo and P.DeptID=ls.DeptID " +
                     "   LEFT JOIN PakaiDetail as xx ON  xx.PakaiID=p.ID and xx.ItemID=ls.ItemID " +
                     "   /*LEFT JOIN Inventory as iv on iv.ID=ls.ItemID " +
                     "   LEFT JOIN Biaya as b on b.ID=ls.ItemID " +
                     "   LEFT JOIN Asset as a on a.ID=ls.ItemID " +
                     "   LEFT JOIN UOM as u on u.ID=iv.UOMID or u.ID=b.UOMID or u.ID=a.UOMID*/ " +
                     "   where xx.groupsp not like '%regular%' and  ls.SarmutID=" + ID + " and left(Convert(Char,ls.SPBDate,112),6) = '" + ThnBln + "'  " +
                     "   and xx.RowStatus>-1 and P.Status >1 and ls.RowStatus>-1 " + Zona +
                // Tambahan Retur Pakai
                     "  UNION ALL " +
                     "  select ls.ReturDate,ls.ReturNo, Case  B.ItemTypeID     When 1 Then (select dbo.ItemCodeInv(B.ItemID,1)) " +
                     "  when 2 then (select dbo.ItemCodeInv(B.ItemID,2)) else (select dbo.ItemCodeInv(B.ItemID,3)) end ItemCode, " +
                     "  Case  B.ItemTypeID     When 1 Then (select dbo.ItemNameInv(B.ItemID,1)) when 2 then (select dbo.ItemNameInv(B.ItemID,2)) " +
                     "  else (select dbo.ItemNameInv(B.ItemID,3)) end ItemName,    (select UomCode from UOM where ID=B.UomID) " +
                     "  as Unit,(-1*(B.Quantity))Quantity,isnull(B.AvgPrice,0)AvgPrice,ISNULL((-1*(B.Quantity)*B.AvgPrice),0)Harga,xx.ZonaMTC, " +
                     "  B.Keterangan,isnull(xx.groupsp,'')groupsp from ReturPakai as ls " +
                     "  INNER JOIN ReturPakaiDetail as B ON ls.ID=B.ReturID " +
                     "  LEFT JOIN Pakai as C ON ls.PakaiNo=C.PakaiNo and ls.ItemTypeID=C.ItemTypeID " +
                     "  LEFT JOIN PakaiDetail as xx ON C.ID=xx.PakaiID and xx.ItemID=B.ItemID and xx.GroupID=B.GroupID and C.DeptID=ls.DeptID " +
                     "  LEFT JOIN MTC_LapSarmut as E ON E.PakaiNo=ls.PakaiNo and E.ItemID=B.ItemID " +
                     "  where xx.groupsp not like '%regular%' and E.SarmutID=" + ID + " " + Zona + " and left(Convert(Char,ls.ReturDate,112) ,6) = '" + ThnBln + "'  " +
                     "  order by ls.SPBDate,ItemCode";
            #endregion
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSarmut = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSarmut.Add(new MTC_Sarmut
                    {
                        SPBDate = Convert.ToDateTime(sqlDataReader["SPBDate"].ToString()),
                        SarmutCode = sqlDataReader["ItemCode"].ToString(),
                        SarmutName = sqlDataReader["ItemName"].ToString(),
                        SPBID = sqlDataReader["Unit"].ToString(),
                        Qty = Convert.ToDecimal(sqlDataReader["Quantity"].ToString()),
                        AvgPrice = Convert.ToDecimal(sqlDataReader["avgPrice"].ToString()),
                        Total = Convert.ToDecimal(sqlDataReader["Harga"].ToString()),
                        ZonaMTC = sqlDataReader["ZonaMTC"].ToString(),
                        keterangan = sqlDataReader["keterangan"].ToString(),
                        NoPol = sqlDataReader["PakaiNo"].ToString(),
                        Groupsp = sqlDataReader["Groupsp"].ToString()
                    });
                }
            }
            return arrSarmut;
        }
        public ArrayList DetailSarmutChart(string Thn, string legend)
        {
            #region NewQuery
            arrSarmut = new ArrayList();
            string strSQL =
                "declare @thn varchar(4) " +
                "set @thn='" + Thn + "' " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempsrmtpakai]') AND type in (N'U')) DROP TABLE [dbo].tempsrmtpakai " +
                "select Bulan,groupReg,sum(Harga)Harga into tempsrmtpakai from ( " +
                "select A.SPBDate, DATEPART(Month,A.spbdate)bulan, B.GroupNaME,B.GroupNaME+' ' + A.groupsp GroupReg,A.Harga    from ( " +
                "SELECT ls.SarmutID,ls.SPBDate,ls.PakaiNo,  " +
                "Case  ls.ItemTypeID   " +
                "   When 1 Then (select dbo.ItemCodeInv(ls.ItemID,1))   " +
                "   when 2 then (select dbo.ItemCodeInv(ls.ItemID,2)) else (select dbo.ItemCodeInv(ls.ItemID,3)) end ItemCode,   " +
                "Case  ls.ItemTypeID   " +
                "   When 1 Then (select dbo.ItemNameInv(ls.ItemID,1))   " +
                "   when 2 then (select dbo.ItemNameInv(ls.ItemID,2)) else (select dbo.ItemNameInv(ls.ItemID,3)) end ItemName,  " +
                "   (select UomCode from UOM where ID=xx.UomID) as Unit,  " +
                "xx.Quantity,isnull(xx.AvgPrice,0)AvgPrice,ISNULL((xx.Quantity*xx.AvgPrice),0)Harga,xx.ZonaMTC,xx.Keterangan,isnull(xx.groupsp,'')groupsp  " +
                "    FROM MTC_LapSarmut ls  " +
                "   LEFT JOIN Pakai as P ON P.PakaiNo=ls.PakaiNo and P.DeptID=ls.DeptID  " +
                "   LEFT JOIN PakaiDetail as xx ON  xx.PakaiID=p.ID and xx.ItemID=ls.ItemID  " +
                "   where  xx.groupsp like 'regular%'  and  left(Convert(Char,ls.SPBDate,112),4) = @thn " +
                "   and xx.RowStatus>-1 and P.Status >1 and ls.RowStatus>-1   " +
                "UNION ALL  " +
                "  select E.SarmutID,ls.ReturDate,ls.ReturNo, Case  B.ItemTypeID     When 1 Then (select dbo.ItemCodeInv(B.ItemID,1))  " +
                "  when 2 then (select dbo.ItemCodeInv(B.ItemID,2)) else (select dbo.ItemCodeInv(B.ItemID,3)) end ItemCode,  " +
                "  Case  B.ItemTypeID     When 1 Then (select dbo.ItemNameInv(B.ItemID,1)) when 2 then (select dbo.ItemNameInv(B.ItemID,2))  " +
                "  else (select dbo.ItemNameInv(B.ItemID,3)) end ItemName,    (select UomCode from UOM where ID=B.UomID)  " +
                "  as Unit,(-1*(B.Quantity))Quantity,isnull(B.AvgPrice,0)AvgPrice,ISNULL((-1*(B.Quantity)*B.AvgPrice),0)Harga,xx.ZonaMTC,  " +
                "  B.Keterangan,isnull(xx.groupsp,'')groupsp from ReturPakai as ls  " +
                "  INNER JOIN ReturPakaiDetail as B ON ls.ID=B.ReturID  " +
                "  LEFT JOIN Pakai as C ON ls.PakaiNo=C.PakaiNo and ls.ItemTypeID=C.ItemTypeID  " +
                "  LEFT JOIN PakaiDetail as xx ON C.ID=xx.PakaiID and xx.ItemID=B.ItemID and xx.GroupID=B.GroupID and C.DeptID=ls.DeptID  " +
                "  LEFT JOIN MTC_LapSarmut as E ON E.PakaiNo=ls.PakaiNo and E.ItemID=B.ItemID  " +
                "  where xx.groupsp like 'regular%'  and left(Convert(Char,ls.ReturDate,112) ,4) = @thn)A inner join ( " +
                "  select vw.*,m.GroupNaME FROM vw_SarmutGroup as vw  LEFT JOIN MaterialMTCGroup as m ON m.ID=vw.ID  WHERE m.RowStatus >-2) B on A.SarmutID =B.SarmutID ) C group by  Bulan,groupReg " +
                "select Bulan,groupReg,cast(harga as int)harga from tempsrmtpakai " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempsrmtpakai]') AND type in (N'U')) DROP TABLE [dbo].tempsrmtpakai";
            #endregion
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrSarmut.Add(GenerateMTCSarmutChart(sdr));
                }
            }
            return arrSarmut;
        }
        public ArrayList DetailSarmutChartReg(string Thn, string legend)
        {
            #region NewQuery
            arrSarmut = new ArrayList();
            string strSQL =
                "declare @thn varchar(4) " +
                "set @thn='" + Thn + "' " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempsrmtpakai]') AND type in (N'U')) DROP TABLE [dbo].tempsrmtpakai " +
                "select Bulan,groupReg,sum(Harga)Harga into tempsrmtpakai from ( " +
                "select A.SPBDate, DATEPART(Month,A.spbdate)bulan, B.GroupNaME,B.GroupNaME+' ' + A.groupsp GroupReg,A.Harga    from ( " +
                "SELECT ls.SarmutID,ls.SPBDate,ls.PakaiNo,  " +
                "Case  ls.ItemTypeID   " +
                "   When 1 Then (select dbo.ItemCodeInv(ls.ItemID,1))   " +
                "   when 2 then (select dbo.ItemCodeInv(ls.ItemID,2)) else (select dbo.ItemCodeInv(ls.ItemID,3)) end ItemCode,   " +
                "Case  ls.ItemTypeID   " +
                "   When 1 Then (select dbo.ItemNameInv(ls.ItemID,1))   " +
                "   when 2 then (select dbo.ItemNameInv(ls.ItemID,2)) else (select dbo.ItemNameInv(ls.ItemID,3)) end ItemName,  " +
                "   (select UomCode from UOM where ID=xx.UomID) as Unit,  " +
                "xx.Quantity,isnull(xx.AvgPrice,0)AvgPrice,ISNULL((xx.Quantity*xx.AvgPrice),0)Harga,xx.ZonaMTC,xx.Keterangan,isnull(xx.groupsp,'')groupsp  " +
                "    FROM MTC_LapSarmut ls  " +
                "   LEFT JOIN Pakai as P ON P.PakaiNo=ls.PakaiNo and P.DeptID=ls.DeptID  " +
                "   LEFT JOIN PakaiDetail as xx ON  xx.PakaiID=p.ID and xx.ItemID=ls.ItemID  " +
                "   where  xx.groupsp like 'regular%'  and  left(Convert(Char,ls.SPBDate,112),4) = @thn " +
                "   and xx.RowStatus>-1 and P.Status >1 and ls.RowStatus>-1   " +
                "UNION ALL  " +
                "  select E.SarmutID,ls.ReturDate,ls.ReturNo, Case  B.ItemTypeID     When 1 Then (select dbo.ItemCodeInv(B.ItemID,1))  " +
                "  when 2 then (select dbo.ItemCodeInv(B.ItemID,2)) else (select dbo.ItemCodeInv(B.ItemID,3)) end ItemCode,  " +
                "  Case  B.ItemTypeID     When 1 Then (select dbo.ItemNameInv(B.ItemID,1)) when 2 then (select dbo.ItemNameInv(B.ItemID,2))  " +
                "  else (select dbo.ItemNameInv(B.ItemID,3)) end ItemName,    (select UomCode from UOM where ID=B.UomID)  " +
                "  as Unit,(-1*(B.Quantity))Quantity,isnull(B.AvgPrice,0)AvgPrice,ISNULL((-1*(B.Quantity)*B.AvgPrice),0)Harga,xx.ZonaMTC,  " +
                "  B.Keterangan,isnull(xx.groupsp,'')groupsp from ReturPakai as ls  " +
                "  INNER JOIN ReturPakaiDetail as B ON ls.ID=B.ReturID  " +
                "  LEFT JOIN Pakai as C ON ls.PakaiNo=C.PakaiNo and ls.ItemTypeID=C.ItemTypeID  " +
                "  LEFT JOIN PakaiDetail as xx ON C.ID=xx.PakaiID and xx.ItemID=B.ItemID and xx.GroupID=B.GroupID and C.DeptID=ls.DeptID  " +
                "  LEFT JOIN MTC_LapSarmut as E ON E.PakaiNo=ls.PakaiNo and E.ItemID=B.ItemID  " +
                "  where xx.groupsp like 'regular%'  and left(Convert(Char,ls.ReturDate,112) ,4) = @thn)A inner join ( " +
                "  select vw.*,m.GroupNaME FROM vw_SarmutGroup as vw  LEFT JOIN MaterialMTCGroup as m ON m.ID=vw.ID  WHERE m.RowStatus >-2) B on A.SarmutID =B.SarmutID ) C group by  Bulan,groupReg " +
                "select Bulan,groupReg,cast(harga as int)harga from tempsrmtpakai where groupreg='" + legend + "' " +
            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempsrmtpakai]') AND type in (N'U')) DROP TABLE [dbo].tempsrmtpakai";
            #endregion
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrSarmut.Add(GenerateMTCSarmutChart(sdr));
                }
            }
            return arrSarmut;
        }
        public ArrayList DetailSarmutChartLegend(string Thn)
        {
            #region NewQuery
            arrSarmut = new ArrayList();
            string strSQL = "declare @thn varchar(4) " +
                "set @thn='" + Thn + "' " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempsrmtpakai]') AND type in (N'U')) DROP TABLE [dbo].tempsrmtpakai " +
                "select Bulan,groupReg,sum(Harga)Harga into tempsrmtpakai from ( " +
                "select A.SPBDate, DATEPART(Month,A.spbdate)bulan, B.GroupNaME,B.GroupNaME+' ' + A.groupsp GroupReg,A.Harga    from ( " +
                "SELECT ls.SarmutID,ls.SPBDate,ls.PakaiNo,  " +
                "Case  ls.ItemTypeID   " +
                "   When 1 Then (select dbo.ItemCodeInv(ls.ItemID,1))   " +
                "   when 2 then (select dbo.ItemCodeInv(ls.ItemID,2)) else (select dbo.ItemCodeInv(ls.ItemID,3)) end ItemCode,   " +
                "Case  ls.ItemTypeID   " +
                "   When 1 Then (select dbo.ItemNameInv(ls.ItemID,1))   " +
                "   when 2 then (select dbo.ItemNameInv(ls.ItemID,2)) else (select dbo.ItemNameInv(ls.ItemID,3)) end ItemName,  " +
                "   (select UomCode from UOM where ID=xx.UomID) as Unit,  " +
                "xx.Quantity,isnull(xx.AvgPrice,0)AvgPrice,ISNULL((xx.Quantity*xx.AvgPrice),0)Harga,xx.ZonaMTC,xx.Keterangan,isnull(xx.groupsp,'')groupsp  " +
                "    FROM MTC_LapSarmut ls  " +
                "   LEFT JOIN Pakai as P ON P.PakaiNo=ls.PakaiNo and P.DeptID=ls.DeptID  " +
                "   LEFT JOIN PakaiDetail as xx ON  xx.PakaiID=p.ID and xx.ItemID=ls.ItemID  " +
                "   where  xx.groupsp like 'regular%'  and  left(Convert(Char,ls.SPBDate,112),4) = @thn " +
                "   and xx.RowStatus>-1 and P.Status >1 and ls.RowStatus>-1   " +
                "UNION ALL  " +
                "  select E.SarmutID,ls.ReturDate,ls.ReturNo, Case  B.ItemTypeID     When 1 Then (select dbo.ItemCodeInv(B.ItemID,1))  " +
                "  when 2 then (select dbo.ItemCodeInv(B.ItemID,2)) else (select dbo.ItemCodeInv(B.ItemID,3)) end ItemCode,  " +
                "  Case  B.ItemTypeID     When 1 Then (select dbo.ItemNameInv(B.ItemID,1)) when 2 then (select dbo.ItemNameInv(B.ItemID,2))  " +
                "  else (select dbo.ItemNameInv(B.ItemID,3)) end ItemName,    (select UomCode from UOM where ID=B.UomID)  " +
                "  as Unit,(-1*(B.Quantity))Quantity,isnull(B.AvgPrice,0)AvgPrice,ISNULL((-1*(B.Quantity)*B.AvgPrice),0)Harga,xx.ZonaMTC,  " +
                "  B.Keterangan,isnull(xx.groupsp,'')groupsp from ReturPakai as ls  " +
                "  INNER JOIN ReturPakaiDetail as B ON ls.ID=B.ReturID  " +
                "  LEFT JOIN Pakai as C ON ls.PakaiNo=C.PakaiNo and ls.ItemTypeID=C.ItemTypeID  " +
                "  LEFT JOIN PakaiDetail as xx ON C.ID=xx.PakaiID and xx.ItemID=B.ItemID and xx.GroupID=B.GroupID and C.DeptID=ls.DeptID  " +
                "  LEFT JOIN MTC_LapSarmut as E ON E.PakaiNo=ls.PakaiNo and E.ItemID=B.ItemID  " +
                "  where xx.groupsp like 'regular%'  and left(Convert(Char,ls.ReturDate,112) ,4) = @thn)A inner join ( " +
                "  select vw.*,m.GroupNaME FROM vw_SarmutGroup as vw  LEFT JOIN MaterialMTCGroup as m ON m.ID=vw.ID  WHERE m.RowStatus >-2) B on A.SarmutID =B.SarmutID ) C group by  Bulan,groupReg " +
                "select distinct groupReg from tempsrmtpakai ";
                //"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempsrmtpakai]') AND type in (N'U')) DROP TABLE [dbo].tempsrmtpakai";
            #endregion
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrSarmut.Add(GenerateMTCSarmutChartLegend(sdr));
                }
            }
            return arrSarmut;
        }
        public int Bulan { get; set; }
        public int Tahun { get; set; }
        public string StartDates { get; set; }
        public string EndDates { get; set; }
        public string Where { get; set; }
        public string WhereDept { get; set; }
        public string OrderBy { get; set; }
        private string QuerySarmut()
        {
            string result = "WITH SarmutMTC AS " +
                          "  ( " +
                          "  SELECT CONVERT(CHAR,p.PakaiDate,103)PakaiDate, p.PakaiNo,pd.ItemID,"+
                          "  Case When pd.ItemTypeID=1 THEN iv.ItemCode When pd.ItemTypeID=2 THEN c.ItemCode  ELSE b.ItemCode END ItemCode, " +
                          "  Case when pd.ItemTypeID=1 THEN iv.ItemName When pd.ItemTypeID=2 THEN c.ItemName  ELSE b.ItemName END ItemName, " +
                          "  uom.UOMCode,pd.Quantity,pd.AvgPrice,m.SarmutID,mt.GroupName " +
                          "  FROM Pakai AS p " +
                          "  LEFT JOIN PakaiDetail AS pd ON pd.PakaiID=p.ID " +
                          "  LEFT JOIN MTC_LapSarmut AS m ON m.PakaiNo=p.PakaiNo AND m.ItemID=pd.ItemID AND m.DeptID=p.DeptID " +
                          "  LEFT JOIN Inventory AS iv ON iv.ID=pd.ItemID  AND pd.ItemTypeID=1 " +
                          "  LEFT JOIN Biaya AS b ON b.ID=pd.ItemID AND pd.ItemTypeID=3 "+
                          "  LEFT JOIN Asset AS c oN c.ID=pd.ItemID AND pd.ItemTypeID=2 "+
                          "  LEFT JOIN UOM ON UOM.ID=pd.UomID " +
                          "  LEFT JOIN MaterialMTCGroup AS mt ON mt.ID=(SELECT vw_SarmutGroup.ID FROM vw_SarmutGroup " +
                          "  WHERE vw_SarmutGroup.SarmutID=m.SarmutID AND RowStatus >-1) " +
                          "  WHERE p.Status>1 AND pd.RowStatus>-1 AND m.RowStatus>-1 AND mt.RowStatus>-1 " +
                          "  AND p.DeptID in(4,5,18) AND m.SarmutID in(SELECT SarmutID FROM vw_SarmutGroup WHERE RowStatus >-1) " +
                          "  AND CONVERT(CHAR,p.PakaiDate,112) between '" + this.StartDates + "' AND '" + this.EndDates + "' " +
                          " AND pd.GroupID in(4,5,2,8,9,11) AND pd.ItemTypeID IN (1,2,3) " + this.WhereDept +
                          //"  ), " +
                          " UNION ALL " +
                          " select CONVERT(CHAR,p.ReturDate,103)ReturDate,p.ReturNo,rpd.ItemID,"+
                          "  Case When rpd.ItemTypeID=1 THEN inv.ItemCode When rpd.ItemTypeID=2 THEN c.ItemCode  ELSE b.ItemCode END ItemCode, " +
                          "  Case when rpd.ItemTypeID=1 THEN inv.ItemName When rpd.ItemTypeID=2 THEN c.ItemName  ELSE b.ItemName END ItemName,uo.UOMCode, " +
                          " (-1*(rpd.Quantity))Quantity,(rpd.AvgPrice)AvgPrice,mtc.SarmutID,mt.GroupNaME  from ReturPakai p  "+
                          " INNER JOIN ReturPakaiDetail rpd ON p.ID=rpd.ReturID  "+
                          " LEFT JOIN MTC_LapSarmut mtc ON mtc.ItemID=rpd.ItemID and mtc.PakaiNo=p.PakaiNo and mtc.ItemTypeID=rpd.ItemTypeID  "+
                          " LEFT JOIN Inventory inv ON inv.ID=rpd.ItemID and inv.ItemTypeID=rpd.ItemTypeID and inv.GroupID=rpd.GroupID  "+
                          " LEFT JOIN Biaya AS b ON b.ID=rpd.ItemID AND rpd.ItemTypeID=3 " +
                          " LEFT JOIN Asset AS c oN c.ID=rpd.ItemID AND rpd.ItemTypeID=2 " +
                          " LEFT JOIN UOM uo ON uo.ID=inv.UOMID  "+
                          " LEFT JOIN MaterialMTCGroup AS mt ON mt.ID=(SELECT vw_SarmutGroup.ID  FROM vw_SarmutGroup  "+
                          " WHERE vw_SarmutGroup.SarmutID=mtc.SarmutID AND RowStatus >-1)  where p.ReturDate between '" + this.StartDates + "' AND '" + this.EndDates + "'" +
                          " and rpd.RowStatus>-1 and mtc.DeptID in (4,5,18)  and rpd.GroupID in (2,4,5,8,9,11) and rpd.ItemTypeID in (1,2,3) " + this.WhereDept +
                          " ), "+
                          // End Tambahan
                          "  SarmutGroup AS( " +
                          "      SELECT PakaiDate,PakaiNo,ItemCode,ItemName,GroupName,UomCode,Quantity,ISNULL(AvgPrice,0)AvgPrice, " +
                          "      ISNULL((AvgPrice * Quantity),0) as TotalPrice,1 as SortOrder FROM SarmutMTC " +
                          "      UNION " +
                          "      SELECT '' PakaiDate,'' PakaiNo,'' ItemCode,('Total ' + GroupName) as ItemName,GroupName,''  " +
                          "      UomCode,SUM(Quantity)Quantity,0 AvgPrice, " +
                          "      ISNULL(SUM(AvgPrice * Quantity),0) TotalPrice,2 SortOrder FROM SarmutMTC GROUP BY GroupName " +
                          "  ) " +
                        "SELECT  sg.*,mg.ID,mg.Kode,(sg.TotalPrice/(select SUM(TotalPrice) FROM SarmutGroup sgp WHERE sgp.SortOrder=sg.SortOrder )*100) Prosen, " +
                        "(select SUM(TotalPrice) FROM SarmutGroup sgp WHERE sgp.SortOrder=sg.SortOrder )TotalP " +
                        "FROM SarmutGroup AS sg " +
                        "LEFT JOIN MaterialMTCGroup as mg ON mg.GroupName=sg.GroupName " +
                        this.Where + this.OrderBy;
            return result;
        }
        public ArrayList Retrieve(bool NewGroup)
        {
            arrSarmut = new ArrayList();
            string strSQL = this.QuerySarmut();
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrSarmut.Add(GenerateSarmut(sdr));
                }
            }
            return arrSarmut;

        }
        public ArrayList Retrieve(bool NewSarmut, string Detail)
        {
            arrSarmut = new ArrayList();
            string strSQL = this.QuerySarmut();
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrSarmut.Add(GenerateSarmut(sdr, GenerateSarmut(sdr)));
                }
            }
            return arrSarmut;
        }
        public ArrayList RetrieveMTCSarmut()
        {
            arrSarmut = new ArrayList();
            string strSQL = "SELECT * FROM MaterialMTCGroup WHERE RowStatus>-1 ORDER BY Kode";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrSarmut.Add(GenerateMTCSarmut(sdr));
                }
            }
            return arrSarmut;
        }
        
        private object GenerateMTCSarmut(SqlDataReader sdr)
        {
            objSm = new SarmutMaintenance();
            objSm.ID = int.Parse(sdr["ID"].ToString());
            objSm.GroupName = sdr["GroupName"].ToString();
            objSm.SarmutGroup = sdr["SarmutGroup"].ToString();
            return objSm;
        }
        private SarmutMaintenance GenerateSarmut(SqlDataReader sdr)
        {
            objSm=new SarmutMaintenance();
            objSm.ID = int.Parse(sdr["ID"].ToString());
            objSm.GroupName = sdr["GroupName"].ToString();
            objSm.Kode = int.Parse(sdr["Kode"].ToString());
            objSm.Total = decimal.Parse(sdr["TotalPrice"].ToString());
            objSm.Prosen = decimal.Parse(sdr["Prosen"].ToString());
            objSm.TotalPrice = decimal.Parse(sdr["TotalP"].ToString());
            return objSm;
        }
        private SarmutMaintenance GenerateSarmut(SqlDataReader sdr, SarmutMaintenance smt)
        {
            objSm = (SarmutMaintenance)smt;
            objSm.Tanggal = sdr["PakaiDate"].ToString();
            objSm.PakaiNo = sdr["PakaiNo"].ToString();
            objSm.ItemCode = sdr["ItemCode"].ToString();
            objSm.ItemName = sdr["ItemName"].ToString();
            objSm.Unit = sdr["UOMCode"].ToString();
            objSm.Quantity = decimal.Parse(sdr["Quantity"].ToString());
            objSm.AvgPrice = decimal.Parse(sdr["AvgPrice"].ToString());
            objSm.TotalPrice = decimal.Parse(sdr["TotalPrice"].ToString());
            return objSm;
        }
        private object GenerateMTCSarmutChart(SqlDataReader sdr)
        {
            objSarmut = new MTC_Sarmut();
            objSarmut.GroupReg = sdr["GroupReg"].ToString();
            objSarmut.Bulan = Convert.ToInt32(sdr["Bulan"].ToString());
            objSarmut.Harga = Convert.ToInt32(sdr["Harga"].ToString());
            return objSarmut;
        }
        private object GenerateMTCSarmutChartLegend(SqlDataReader sdr)
        {
            objSarmut = new MTC_Sarmut();
            objSarmut.GroupReg = sdr["GroupReg"].ToString();
            return objSarmut;
        }
    }
}
