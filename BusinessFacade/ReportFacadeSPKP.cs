using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Domain;
using DataAccessLayer;

namespace BusinessFacade
{
    public class ReportFacadeSPKP
    {
        private ArrayList arrSPK = new ArrayList();
        private SPKP_Master objSPK = new SPKP_Master();
        public string Criteria { get; set; }
        public string ViewSPKPMaster(int ID)
        {
            return "SELECT * FROM SPKP_Master where rowstatus>=0 and ID=" + ID;
        }
        public string ViewSPKPDesc(int ID)
        {
            return "SELECT * FROM SPKP_Desc where  rowstatus>=0 and spkpid=" + ID;
        }
        public string ViewSPKPSch(string firsdate,int ID)
        {
            return "select plant,formula,Tebal,luas, "+
             "(select lembar from SPKP_Sch where Tanggal = t1.tgl and Shift=1 and formula=t1.formula and plant=t1.plant and spkpid=t1.spkpid and tebal=t1.tebal and luas=t1.luas) as L11, "+
             "(select lembar from SPKP_Sch where Tanggal = t1.tgl and Shift=2 and formula=t1.formula and plant=t1.plant and spkpid=t1.spkpid and tebal=t1.tebal and luas=t1.luas) as L12, "+
             "(select lembar from SPKP_Sch where Tanggal = t1.tgl and Shift=3 and formula=t1.formula and plant=t1.plant and spkpid=t1.spkpid and tebal=t1.tebal and luas=t1.luas) as L13, "+
             "(select sum(lembar) from SPKP_Sch where Tanggal = t1.tgl and formula=t1.formula and plant=t1.plant and spkpid=t1.spkpid and tebal=t1.tebal and luas=t1.luas) as LS1, "+
             "(select lembar from SPKP_Sch where Tanggal = DATEADD(day,1,t1.tgl) and Shift=1 and formula=t1.formula and plant=t1.plant and spkpid=t1.spkpid and tebal=t1.tebal and luas=t1.luas) as L21, "+
             "(select lembar from SPKP_Sch where Tanggal = DATEADD(day,1,t1.tgl) and Shift=2 and formula=t1.formula and plant=t1.plant and spkpid=t1.spkpid and tebal=t1.tebal and luas=t1.luas) as L22, "+
             "(select lembar from SPKP_Sch where Tanggal = DATEADD(day,1,t1.tgl) and Shift=3 and formula=t1.formula and plant=t1.plant and spkpid=t1.spkpid and tebal=t1.tebal and luas=t1.luas) as L23, "+
             "(select sum(lembar) from SPKP_Sch where Tanggal = DATEADD(day,1,t1.tgl) and formula=t1.formula and plant=t1.plant and spkpid=t1.spkpid and tebal=t1.tebal and luas=t1.luas) as LS2, "+
             "(select lembar from SPKP_Sch where Tanggal = DATEADD(day,2,t1.tgl) and Shift=1 and formula=t1.formula and plant=t1.plant and spkpid=t1.spkpid and tebal=t1.tebal and luas=t1.luas) as L31, "+
             "(select lembar from SPKP_Sch where Tanggal = DATEADD(day,2,t1.tgl) and Shift=2 and formula=t1.formula and plant=t1.plant and spkpid=t1.spkpid and tebal=t1.tebal and luas=t1.luas) as L32, "+
             "(select lembar from SPKP_Sch where Tanggal = DATEADD(day,2,t1.tgl) and Shift=3 and formula=t1.formula and plant=t1.plant and spkpid=t1.spkpid and tebal=t1.tebal and luas=t1.luas) as L33, "+
             "(select sum(lembar) from SPKP_Sch where Tanggal = DATEADD(day,2,t1.tgl) and formula=t1.formula and plant=t1.plant and spkpid=t1.spkpid and tebal=t1.tebal and luas=t1.luas) as LS3, "+
             "(select lembar from SPKP_Sch where Tanggal = DATEADD(day,3,t1.tgl) and Shift=1 and formula=t1.formula and plant=t1.plant and spkpid=t1.spkpid and tebal=t1.tebal and luas=t1.luas) as L41, "+
             "(select lembar from SPKP_Sch where Tanggal = DATEADD(day,3,t1.tgl) and Shift=2 and formula=t1.formula and plant=t1.plant and spkpid=t1.spkpid and tebal=t1.tebal and luas=t1.luas) as L42, "+
             "(select lembar from SPKP_Sch where Tanggal = DATEADD(day,3,t1.tgl) and Shift=3 and formula=t1.formula and plant=t1.plant and spkpid=t1.spkpid and tebal=t1.tebal and luas=t1.luas) as L43, "+
             "(select sum(lembar) from SPKP_Sch where Tanggal = DATEADD(day,3,t1.tgl) and formula=t1.formula and plant=t1.plant and spkpid=t1.spkpid and tebal=t1.tebal and luas=t1.luas) as LS4, "+
             "(select lembar from SPKP_Sch where Tanggal = DATEADD(day,4,t1.tgl) and Shift=1 and formula=t1.formula and plant=t1.plant and spkpid=t1.spkpid and tebal=t1.tebal and luas=t1.luas) as L51, "+
             "(select lembar from SPKP_Sch where Tanggal = DATEADD(day,4,t1.tgl) and Shift=2 and formula=t1.formula and plant=t1.plant and spkpid=t1.spkpid and tebal=t1.tebal and luas=t1.luas) as L52, "+
             "(select lembar from SPKP_Sch where Tanggal = DATEADD(day,4,t1.tgl) and Shift=3 and formula=t1.formula and plant=t1.plant and spkpid=t1.spkpid and tebal=t1.tebal and luas=t1.luas) as L53, "+
             "(select sum(lembar) from SPKP_Sch where Tanggal = DATEADD(day,4,t1.tgl) and formula=t1.formula and plant=t1.plant and spkpid=t1.spkpid and tebal=t1.tebal and luas=t1.luas) as LS5 , "+
             "(select lembar from SPKP_Sch where Tanggal = DATEADD(day,5,t1.tgl) and Shift=1 and formula=t1.formula and plant=t1.plant and spkpid=t1.spkpid and tebal=t1.tebal and luas=t1.luas) as L61, "+
             "(select lembar from SPKP_Sch where Tanggal = DATEADD(day,5,t1.tgl) and Shift=2 and formula=t1.formula and plant=t1.plant and spkpid=t1.spkpid and tebal=t1.tebal and luas=t1.luas) as L62, "+
             "(select lembar from SPKP_Sch where Tanggal = DATEADD(day,5,t1.tgl) and Shift=3 and formula=t1.formula and plant=t1.plant and spkpid=t1.spkpid and tebal=t1.tebal and luas=t1.luas) as L63, "+
             "(select sum(lembar) from SPKP_Sch where Tanggal = DATEADD(day,5,t1.tgl) and formula=t1.formula and plant=t1.plant and spkpid=t1.spkpid and tebal=t1.tebal and luas=t1.luas) as LS6, "+
             " (select lembar from SPKP_Sch where Tanggal = DATEADD(day,6,t1.tgl) and Shift=1 and formula=t1.formula and plant=t1.plant and spkpid=t1.spkpid and tebal=t1.tebal and luas=t1.luas) as L71, "+
             "(select lembar from SPKP_Sch where Tanggal = DATEADD(day,6,t1.tgl) and Shift=2 and formula=t1.formula and plant=t1.plant and spkpid=t1.spkpid and tebal=t1.tebal and luas=t1.luas) as L72, "+
             "(select lembar from SPKP_Sch where Tanggal = DATEADD(day,6,t1.tgl) and Shift=3 and formula=t1.formula and plant=t1.plant and spkpid=t1.spkpid and tebal=t1.tebal and luas=t1.luas) as L73, "+
             "(select sum(lembar) from SPKP_Sch where Tanggal = DATEADD(day,6,t1.tgl) and formula=t1.formula and plant=t1.plant and spkpid=t1.spkpid and tebal=t1.tebal and luas=t1.luas) as LS7, "+
             "(select sum(lembar) from SPKP_Sch where formula=t1.formula and plant=t1.plant and spkpid=t1.spkpid and tebal=t1.tebal and luas=t1.luas) as Total " +
             "from ( " +
             "select distinct '" + firsdate + "' as tgl,spkpid,plant,formula,Tebal,luas from SPKP_Sch where  rowstatus>=0 and spkpid=" + ID + " ) as t1";
        }
        private string stringQuery()
        {
            #region oldquery
            string strSQL = "select * from ( " +
                            "select ROW_NUMBER()over(partition by j.RakID order by tglJemur) as nom, b.ID,fc.PartNo, b.TglProduksi,p.NoPAlet," +
                            "r.Rak,j.TglJemur,b.Qty,(j.QtyIn-j.QtyOut)QtyJemur,j.Status from T1_Jemur as j " +
                            "left join BM_Destacking as b " +
                            "on b.ID=j.DestID " +
                            "left join BM_Palet as p " +
                            "on p.ID=b.PaletID " +
                            "left join FC_Items as fc " +
                            "on fc.ID=b.ItemID " +
                            "left join FC_Rak as r " +
                            "on r.ID=j.RakID " +
                            "where j.QtyIn>j.QtyOut and j.RowStatus >-1 and year(b.TglProduksi) >=2014  " +
                            " ) as x " +
                            " where x.nom >1 and Convert(Char,x.TglJemur,112) <= '" + this.TanggalJemur() + "'" +
                            " order by x.PartNo,x.TglJemur ";
            #endregion
            strSQL = "select bm.ID,bm.ItemID,(fc.PartNo+' [ '+RTRIM(CAST(CAST(fc.Tebal as int) as CHAR))+' mm x '+RTRIM(CAST(CAST(fc.lebar as int) as CHAR))+' mm x '+RTRIM(CAST(CAST(fc.Panjang as int) as Char))+' mm ]')PartNo "+
                   " ,p.NoPAlet,bm.TglProduksi,r.Rak,x.TglJemur,bm.Qty, " +
                   " x.QtyIn,x.QtyOut,(x.QtyIn-x.QtyOut)QtyJemur,x.Status from ( " +
                   " select ROW_NUMBER() Over(Partition by RakID Order By TglJemur Desc)N,*  " +
                   " from T1_Jemur where RakID not in(2807) and QtyOut<QtyIn  " +
                   " and Year(tglJemur)>2014 and fail is null and RowStatus>-1 " +
                   " ) as x   " +
                   " LEFT JOIN BM_Destacking as bm ON bm.ID=x.DestID " +
                   " LEFT JOIN BM_Palet as p on p.ID=bm.PaletID  " +
                   " LEFT JOIN FC_Items as fc on fc.ID=bm.ItemID  " +
                   " LEFT JOIN FC_Rak as r on r.ID=x.RakID " +
                   " WHERE x.n>1 and Convert(Char,x.TglJemur,112)<='" + this.TanggalJemur() + "'" +
                   " ORDER by x.rakID ";
            strSQL = "select bm.ID,bm.ItemID,(fc.PartNo+' [ '+RTRIM(CAST(CAST(fc.Tebal as int) as CHAR))+' mm x '+ " +
                   "RTRIM(CAST(CAST(fc.lebar as int) as CHAR))+' mm x '+RTRIM(CAST(CAST(fc.Panjang as int) as Char))+' mm ]')PartNo, " +
                   "p.NoPAlet,bm.TglProduksi,r.Rak,x.TglJemur,bm.Qty,x.QtyIn,(select isnull(SUM(QtyIn),0) from T1_Serah as s where s.DestID=x.DestID and s.Status>-1 group by s.DestID)QtyOut,(x.QtyIn-QtyOut)QtyJemur,x.[Status] " +
                   "from (select * from (select CONVERT(varchar, TglJemur, 112) + RTRIM(CAST(RakID AS CHAR(10))) as IDnew,* " +
                   "from T1_Jemur where Year(tglJemur)>2014 and fail is null and rowstatus > -1) as xx1 where xx1.IDnew " +
                   "not in " +
                   "(select x2.IDnew " +
                   "from (select ROW_NUMBER() Over(Partition by RakID Order By TglJemur Desc)N,CONVERT(varchar, TglJemur, 112) +  " +
                   "RTRIM(CAST(RakID AS CHAR(10))) as IDnew,RakID,TglJemur from T1_Jemur where LEFT(convert(char,TglJemur,112),4)>'2014' " +
                   "and CONVERT(char,tgljemur,112)<='" + this.TanggalJemur() + "' and RakID not in(2807) and rowstatus > -1 group by RakID,TglJemur) as x2 where x2.N=1) and  xx1.QtyIn > xx1.QtyOut ) as x " +
                   "LEFT JOIN BM_Destacking as bm ON bm.ID=x.DestID " +
                   "LEFT JOIN BM_Palet as p on p.ID=bm.PaletID " +
                   "LEFT JOIN FC_Items as fc on fc.ID=bm.ItemID " +
                   "LEFT JOIN FC_Rak as r on r.ID=x.RakID " +
                   "where CONVERT(char,x.tgljemur,112)<='" + this.TanggalJemur() + "'" +
                   "ORDER by x.rakID";
            return strSQL;
        }
        public string TanggalJemur()
        {
            return (HttpContext.Current.Session["tglJemur"] != null) ? HttpContext.Current.Session["tglJemur"].ToString() : DateTime.Now.ToString("yyyyMMdd");
        }
        public ArrayList RetrieveSiapPotong(string PartNo)
        {
            string strSQL = this.stringQuery();
            strSQL = "SELECT * FROM (";
            strSQL += this.stringQuery().Replace("ORDER by x.rakID", "");
            strSQL +=") as xx  Where xx.PartNo='" + PartNo + "' order by xx.PartNo,xx.Rak";
            DataAccess dta = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = dta.RetrieveDataByString(strSQL);
            arrSPK = new ArrayList();
            decimal total = 0;
            if (dta.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    total +=  Convert.ToDecimal(sdr["QtyJemur"].ToString());
                    arrSPK.Add(new SPKP_Master
                    {
                        PartNo = sdr["PartNo"].ToString(),
                        PaletNo = sdr["NoPalet"].ToString(),
                        TglProduksi = Convert.ToDateTime(sdr["TglProduksi"].ToString()),
                        TglJemur = Convert.ToDateTime(sdr["TglJemur"].ToString()),
                        Qty = Convert.ToDecimal(sdr["QtyJemur"].ToString()),
                        Status=Convert.ToInt32(sdr["Status"].ToString()),
                        Rak=sdr["Rak"].ToString()
                        //Total=total
                    });
                }
            }
            return arrSPK;
        }
        public ArrayList RetrievePartNo()
        {
            string strQuery = string.Empty;
            strQuery = "SELECT PartNo,SUM(QtyJemur) as Qty FROM (";
            strQuery += this.stringQuery().Replace("ORDER by x.rakID", "");
            strQuery += ") as xx Group by xx.PartNo order by xx.PartNo";
            DataAccess dta = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = dta.RetrieveDataByString(strQuery);
            arrSPK = new ArrayList();
            if (dta.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrSPK.Add(new SPKP_Master 
                    { 
                        PartNo = sdr["PartNo"].ToString(),
                        Total=Convert.ToDecimal(sdr["Qty"].ToString())
                    });
                   
                }
            }
            return arrSPK;
        }
        public ArrayList RetrieveMasaCuring()
        {
            string MasaCuring = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("MasaCuring", "SPKP");
            arrSPK = new ArrayList();
            string strSQL = "SELECT * FROM ( " +
                          "SELECT bm.ID,bm.TglProduksi,DATEDIFF(DAY,bm.TglProduksi,GETDATE())Selisih,bm.ItemID, " +
                          "(fc.PartNo+' [ '+RTRIM(CAST(CAST(fc.Tebal as int) as CHAR))+' mm x '+RTRIM(CAST(CAST(fc.lebar as int) as CHAR))+' mm x '+RTRIM(CAST(CAST(fc.Panjang as int) as Char))+' mm ]')PartNo,"+
                          "bm.Qty,pl.NoPAlet,lo.Lokasi  " +
                          "FROM BM_Destacking bm " +
                          "LEFT JOIN FC_Items as fc ON fc.ID=bm.ItemID " +
                          "LEFT JOIN BM_Palet as pl on pl.ID=bm.PaletID " +
                          "LEFT JOIN FC_Lokasi as lo on lo.ID=bm.LokasiID " +
                          "WHERE bm.Status=0 and bm.RowStatus>-1 and bm.Qty>0 " +
                          ") as x " +
                          "WHERE x.Selisih " + MasaCuring + this.Criteria +
                          "ORDER By x.PartNo,x.TglProduksi";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrSPK.Add(GenerateObject(sdr));
                }
            }
            return arrSPK;
        }
        public ArrayList RetrieveMasaCuring(bool Total)
        {
            string MasaCuring = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("MasaCuring", "SPKP");
            arrSPK = new ArrayList();
            string strSQL ="SELECT PartNo,SUM(Qty)Qty FROM ( SELECT * FROM ( " +
                          "SELECT bm.ID,bm.TglProduksi,DATEDIFF(DAY,bm.TglProduksi,GETDATE())Selisih,bm.ItemID, " +
                          "(fc.PartNo+' [ '+RTRIM(CAST(CAST(fc.Tebal as int) as CHAR))+' mm x '+RTRIM(CAST(CAST(fc.lebar as int) as CHAR))+' mm x '+RTRIM(CAST(CAST(fc.Panjang as int) as Char))+' mm ]')PartNo," +
                          "bm.Qty,pl.NoPAlet,lo.Lokasi  " +
                          "FROM BM_Destacking bm " +
                          "LEFT JOIN FC_Items as fc ON fc.ID=bm.ItemID " +
                          "LEFT JOIN BM_Palet as pl on pl.ID=bm.PaletID " +
                          "LEFT JOIN FC_Lokasi as lo on lo.ID=bm.LokasiID " +
                          "WHERE bm.Status=0 and bm.RowStatus>-1 and bm.Qty>0 " +
                          ") as x " +
                          "WHERE x.Selisih " + MasaCuring +
                          ") as xx Group By PartNo ORDER By xx.PartNo";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrSPK.Add(GenerateObject(sdr,Total));
                }
            }
            return arrSPK;
        }

        public ArrayList RetrieveMasaJemur()
        {
            arrSPK = new ArrayList();
            string MasaJemur = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("MasaJemur", "SPKP");
            string strSQL = "SELECT * FROM ( " +
                          "SELECT bm.ID,tj.TglJemur as tglProduksi,DATEDIFF(DAY,tj.TglJemur,GETDATE())Selisih,bm.ItemID, " +
                          "(fc.PartNo+' [ '+RTRIM(CAST(CAST(fc.Tebal as int) as CHAR))+' mm x '+RTRIM(CAST(CAST(fc.lebar as int) as CHAR))+' mm x '+RTRIM(CAST(CAST(fc.Panjang as int) as Char))+' mm ]')PartNo,"+
                          "(tj.QtyIn-tj.QtyOut)Qty,pl.NoPAlet,r.Rak Lokasi  " +
                          "FROM T1_Jemur tj " +
                          "LEFT JOIN BM_Destacking bm ON bm.ID=tj.DestID " +
                          "LEFT JOIN FC_Items as fc ON fc.ID=bm.ItemID " +
                          "LEFT JOIN BM_Palet as pl on pl.ID=bm.PaletID " +
                          "LEFT JOIN FC_Rak as r on r.ID=tj.RakID " +
                          "WHERE tj.RowStatus>-1 and tj.QtyIn<>tj.QtyOut and Year(tglJemur)>2014 " +
                          ") as x " +
                          "WHERE x.Selisih " + MasaJemur + this.Criteria +
                          "ORDER By x.PartNo,x.TglProduksi";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrSPK.Add(GenerateObject(sdr));
                }
            }
            return arrSPK;
        }
        public ArrayList RetrieveMasaJemur(bool Total)
        {
            arrSPK = new ArrayList();
            string MasaJemur = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("MasaJemur", "SPKP");
            string strSQL ="SELECT PartNo,SUM(Qty) Qty FROM(SELECT * FROM ( " +
                          "SELECT bm.ID,tj.TglJemur as tglProduksi,DATEDIFF(DAY,tj.TglJemur,GETDATE())Selisih,bm.ItemID, " +
                          "(fc.PartNo+' [ '+RTRIM(CAST(CAST(fc.Tebal as int) as CHAR))+' mm x '+RTRIM(CAST(CAST(fc.lebar as int) as CHAR))+' mm x '+RTRIM(CAST(CAST(fc.Panjang as int) as Char))+' mm ]')PartNo," +
                          "(tj.QtyIn-tj.QtyOut)Qty,pl.NoPAlet,r.Rak Lokasi  " +
                          "FROM T1_Jemur tj " +
                          "LEFT JOIN BM_Destacking bm ON bm.ID=tj.DestID " +
                          "LEFT JOIN FC_Items as fc ON fc.ID=bm.ItemID " +
                          "LEFT JOIN BM_Palet as pl on pl.ID=bm.PaletID " +
                          "LEFT JOIN FC_Rak as r on r.ID=tj.RakID " +
                          "WHERE tj.RowStatus>-1 and tj.QtyIn<>tj.QtyOut and Year(tglJemur)>2014 " +
                          ") as x " +
                          "WHERE x.Selisih " + MasaJemur +
                          ") as xx Group By PartNo ORDER By xx.PartNo";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrSPK.Add(GenerateObject(sdr,Total));
                }
            }
            return arrSPK;
        }
        public SPKP_Master GenerateObject(SqlDataReader sdr)
        {
            objSPK = new SPKP_Master();
            objSPK.ID = Convert.ToInt32(sdr["ID"].ToString());
            objSPK.TglProduksi = Convert.ToDateTime(sdr["TglProduksi"].ToString());
            objSPK.PartNo = sdr["PartNo"].ToString();
            objSPK.PaletNo = sdr["NoPalet"].ToString();
            objSPK.Rak = sdr["Lokasi"].ToString();
            objSPK.Total = Convert.ToDecimal(sdr["Qty"].ToString());
            objSPK.Selisih = Convert.ToInt32(sdr["Selisih"].ToString());
            return objSPK;

        }
        public SPKP_Master GenerateObject(SqlDataReader sdr, bool Total)
        {
            objSPK = new SPKP_Master();
            objSPK.PartNo = sdr["PartNo"].ToString();
            objSPK.Total = Convert.ToDecimal(sdr["Qty"].ToString());
            return objSPK;
        }
    }
}
