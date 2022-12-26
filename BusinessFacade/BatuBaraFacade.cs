using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;

namespace BusinessFacade
{
    public class BatuBaraFacade : AbstractFacade
    {
        private Batubara objB = new Batubara();
        private ArrayList arrB;
        private ArrayList arrBK;
        private ArrayList arrData = new ArrayList();
        private List<SqlParameter> sqlListParam;
        
        public BatuBaraFacade()
            : base()
        {
            
        }
        public override int Delete(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override int Insert(object objDomain)
        {
            throw new NotImplementedException();
        }

        public override ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }

        public override int Update(object objDomain)
        {
            throw new NotImplementedException();
        }


        public ArrayList RetrieveBatubaraAkumulasi(string tahun, string bulan)
        {
            string strSQL = QueryRetrieveAkumulasi(tahun, bulan);
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrBK = new ArrayList();

            int n = 0;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                   
                    arrBK.Add(new Batubara
                    {
                        
                        Keterangan = sqlDataReader["Keterangan"].ToString(),
                        Jmllebar = Convert.ToDecimal(sqlDataReader["Lembar"].ToString()),
                        Jmlm3 = Convert.ToDecimal(sqlDataReader["M3"].ToString()),
                        Jmlbatubara = Convert.ToDecimal(sqlDataReader["Batubara"].ToString()),
                        Jmlkgm3 = Convert.ToDecimal(sqlDataReader["Kgm3"].ToString())

                    });
                }
            }
            else
            {
                arrBK.Add(new Batubara());
            }

            return arrBK;
        }

        public ArrayList RetrieveBatubara(string tahun, string bulan)
        {
            string strSQL = QueryRetrieve(tahun, bulan);
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrB = new ArrayList();

            int n = 0;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    n = n + 1;
                    arrB.Add(new Batubara
                    {
                        Nom = n,
                        Tanggal = sqlDataReader["Tanggal"].ToString(),
                        Lembar = Convert.ToDecimal(sqlDataReader["Lembar"].ToString()),
                        M3 = Convert.ToDecimal(sqlDataReader["M3"].ToString()),
                        QtyBatubara = Convert.ToDecimal(sqlDataReader["Batubara"].ToString()),
                        Kgm3 = Convert.ToDecimal(sqlDataReader["kgm3"].ToString())
                        
                    });
                }
            }
            else
            {
                arrB.Add(new Batubara());
            }

            return arrB;
        }

        private string QueryRetrieveAkumulasi(string tahun, string bulan)
        {
            string oven = string.Empty;

            oven = "1,2,3,4";
            return
                "with r as ( " +
                "SELECT  A.TglSerah Tanggal, rtrim(I1.PartNo)PartNo , A.QtyIn AS qty, " +
                "case when I1.Volume>=0.01190720 then I1.Volume else 0.01190720 end volume  FROM T1_Serah A " +
                "INNER JOIN FC_Items AS I1 ON A.itemID0 = I1.ID " +
                "left join T1_Jemur J on J.DestID=A.DestID where A.sfrom='jemur' and A.status>-1 " +
                "and left(convert(char,A.tglserah,112),6)='" + tahun.Trim() + bulan.PadLeft(2, '0') + "' and A.Oven in("+oven+") " +

                "union all " +

                "select tanggal,partno,qty,volume from ( select TglJemur tanggal,PartNo2 partno,Qty1 qty, " +
                "(select top 1 oven from t1_serah where [status]>-1 and destid=lagi.DestID and oven>0 order by id  )Oven , volume  from (  " +
                "select lari.DestID,TglJemur,PartNo2,Lokasi2, case when (select COUNT(destid) " +
                "from T1_Serah where JemurID=s.JemurID and  ID<s.serahID and [status]>-1 and itemid0=s.itemID0 and DestID=s.DestID) > 0  " +
                "then 0 else Qty1 end Qty1,Volume from (SELECT B.ID,B.DestID,B.TglJemur, I2.PartNo AS PartNo2, L2.Lokasi AS Lokasi2, " +
                "B.Qtyin  AS Qty1,((I2.Tebal*I2.Lebar*I2.Panjang)/1000000000)Volume " +
                "FROM FC_Lokasi AS L2 inner JOIN T1_JemurLg AS B ON L2.ID = B.LokID0 " +
                "inner join BM_Destacking D on B.DestID=D.ID " +
                "inner JOIN FC_Items AS I2 ON D.ItemID = I2.ID WHERE (B.Status > - 1)) as lari " +
                "left join (SELECT C.ID, C.DestID, C.jemurID, C.itemID0, C.TglSerah,C.CreatedTime as tglinputserah,C.ID as serahID, " +
                "C.QtyIn AS QTY2 FROM T1_Serah AS C WHERE C.Status > - 1) as s on lari.DestID=s.DestID and lari.ID=s.JemurID   ) as lagi " +
                "where  Qty1>0)lagi2  where left(CONVERT(char,tanggal, 112),6)='" + tahun.Trim() + bulan.PadLeft(2, '0') + "' and Oven in("+oven+")), " +

                "b as ( " +

                "select convert(varchar,p.PakaiDate,105)Tanggal, pd.Quantity qtyBatubara from Pakai p,PakaiDetail pd where " +
                "pd.PakaiID = p.ID " +
                "and p.DeptID = 18 and p.Status > -1 " +
                "and pd.ItemID in(select ID from Inventory where ID = 38955) and Pd.RowStatus > -1 " +
                "and left(convert(char, p.PakaiDate, 112), 6)= '" + tahun.Trim() + bulan.PadLeft(2, '0') + "'), " +

                "c as (" +
                "select convert(varchar, r.Tanggal, 105)Tanggal, isnull(sum(r.qty), 0)Lembar, round(isnull(sum((r.qty * r.volume)), 0), 2)M3 " +
                "from r Group By r.Tanggal), " +

                "d as ("+
                "select c.Tanggal,c.Lembar,c.M3,b.qtyBatubara Batubara, round(isnull((b.qtyBatubara / c.M3), 0), 2) Kgm3 from c,b " +
                "where b.Tanggal = c.Tanggal)" +

                "select 'JUMLAH' Keterangan, isnull(sum(Lembar), 0)Lembar,isnull(sum(M3), 0)M3,isnull(sum(Batubara), 0)Batubara,isnull(sum(kgm3), 0)Kgm3 from d " +

                "union all " +

                "select 'RATA-RATA' Keterangan,isnull(0, 0) Lembar," +
                "round((cast(sum(M3) as decimal(18, 2)) / cast(count(Tanggal) as decimal(18, 2))),2) M3, " +
                "isnull(0, 0) Batubara, " +
                "round(cast(sum(Batubara) as decimal(18, 2)) / cast(sum(M3) as decimal(18, 2)),2) Kgm3 from d ";
        }

        private string QueryRetrieve(string tahun, string bulan)
        {
            string oven = string.Empty;
            
            oven = "1,2,3,4";
            return
                "with r as ( " +
                "SELECT  A.TglSerah Tanggal, rtrim(I1.PartNo)PartNo , A.QtyIn AS qty, " +
                "case when I1.Volume>=0.01190720 then I1.Volume else 0.01190720 end volume  FROM T1_Serah A " +
                "INNER JOIN FC_Items AS I1 ON A.itemID0 = I1.ID " +
                "left join T1_Jemur J on J.DestID=A.DestID where A.sfrom='jemur' and A.status>-1 " +
                "and left(convert(char,A.tglserah,112),6)='" + tahun.Trim() + bulan.PadLeft(2, '0') + "' and A.Oven in(" + oven+") " +

                "union all " +

                "select tanggal,partno,qty,volume from ( select TglJemur tanggal,PartNo2 partno,Qty1 qty, " +
                "(select top 1 oven from t1_serah where [status]>-1 and destid=lagi.DestID and oven>0 order by id  )Oven , volume  from (  " +
                "select lari.DestID,TglJemur,PartNo2,Lokasi2, case when (select COUNT(destid) " +
                "from T1_Serah where JemurID=s.JemurID and  ID<s.serahID and [status]>-1 and itemid0=s.itemID0 and DestID=s.DestID) > 0  " +
                "then 0 else Qty1 end Qty1,Volume from (SELECT B.ID,B.DestID,B.TglJemur, I2.PartNo AS PartNo2, L2.Lokasi AS Lokasi2, " +
                "B.Qtyin  AS Qty1,((I2.Tebal*I2.Lebar*I2.Panjang)/1000000000)Volume " +
                "FROM FC_Lokasi AS L2 inner JOIN T1_JemurLg AS B ON L2.ID = B.LokID0 " +
                "inner join BM_Destacking D on B.DestID=D.ID " +
                "inner JOIN FC_Items AS I2 ON D.ItemID = I2.ID WHERE (B.Status > - 1)) as lari " +
                "left join (SELECT C.ID, C.DestID, C.jemurID, C.itemID0, C.TglSerah,C.CreatedTime as tglinputserah,C.ID as serahID, " +
                "C.QtyIn AS QTY2 FROM T1_Serah AS C WHERE C.Status > - 1     ) as s on lari.DestID=s.DestID and lari.ID=s.JemurID   ) as lagi " +
                "where  Qty1>0)lagi2  where left(CONVERT(char,tanggal, 112),6)='" + tahun.Trim() + bulan.PadLeft(2, '0') + "' and Oven in("+oven+")), " +

                "b as ( " +

                "select convert(varchar,p.PakaiDate,105)Tanggal, pd.Quantity qtyBatubara from Pakai p,PakaiDetail pd where " +
                "pd.PakaiID = p.ID " +
                "and p.DeptID = 18 and p.Status > -1 " +
                "and pd.ItemID in(select ID from Inventory where ID = 38955) and Pd.RowStatus > -1 " +
                "and left(convert(char, p.PakaiDate, 112), 6)= '" + tahun.Trim() + bulan.PadLeft(2, '0') + "'), " +

               "c as ( " +
               "select convert(varchar, r.Tanggal, 105)Tanggal, isnull(sum(r.qty), 0)Lembar, round(isnull(sum((r.qty * r.volume)), 0), 2)M3 " +
               "from r Group By r.Tanggal) " +

               "select c.Tanggal,c.Lembar,c.M3,b.qtyBatubara Batubara, round(isnull((b.qtyBatubara / c.M3), 0), 2) Kgm3 " +
               "from c,b where b.Tanggal = c.Tanggal order by c.Tanggal asc";

        }

        public Batubara GenerateObject(SqlDataReader sqlDataReader)
        {
            objB = new Batubara();
            objB.Nom = Convert.ToInt32(sqlDataReader["n"]);
            objB.Tanggal = sqlDataReader["Tanggal"].ToString();
            objB.Lembar = Convert.ToDecimal(sqlDataReader["Lembar"]);
            objB.M3 = Convert.ToDecimal(sqlDataReader["M3"]);
            objB.QtyBatubara = Convert.ToDecimal(sqlDataReader["Batubara"]);
            objB.Kgm3 = Convert.ToDecimal(sqlDataReader["kgm3"]);

            return objB;
        }

    }
}
