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
using System.Globalization;

namespace Factory
{
    public class T3_SiapKirimFacade : BusinessFacade.AbstractTransactionFacadeF
    {
        private T3_SiapKirim objT3_SiapKirim = new T3_SiapKirim();
        private ArrayList arrT3_SiapKirim;
        private List<SqlParameter> sqlListParam;

        public T3_SiapKirimFacade(object objDomain)
            : base(objDomain)
        {
            objT3_SiapKirim = (T3_SiapKirim)objDomain;
        }
        public T3_SiapKirimFacade()
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
        

        public override int Delete(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }
        public override int Insert(TransactionManager transManager)
        {
            try
            {
                objT3_SiapKirim = (T3_SiapKirim)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@SerahID", objT3_SiapKirim.SerahID));
                sqlListParam.Add(new SqlParameter("@LokID", objT3_SiapKirim.LokasiID));
                sqlListParam.Add(new SqlParameter("@ItemID", objT3_SiapKirim.ItemIDSer));
                sqlListParam.Add(new SqlParameter("@TglTrans", objT3_SiapKirim.Tgltrans));
                sqlListParam.Add(new SqlParameter("@Qty", objT3_SiapKirim.Qty));
                sqlListParam.Add(new SqlParameter("@HPP", objT3_SiapKirim.HPP));
                sqlListParam.Add(new SqlParameter("@GroupID", objT3_SiapKirim.GroupID));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT3_SiapKirim.CreatedBy));
                sqlListParam.Add(new SqlParameter("@SJno", objT3_SiapKirim.SJNo ));
                sqlListParam.Add(new SqlParameter("@SA", objT3_SiapKirim.SA));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertT3_SiapKirim");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Update(TransactionManager transManager)
        {
            try
            {
                objT3_SiapKirim = (T3_SiapKirim)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objT3_SiapKirim.ID ));
                sqlListParam.Add(new SqlParameter("@qty", objT3_SiapKirim.Qty ));
                sqlListParam.Add(new SqlParameter("@lastmodifiedby", objT3_SiapKirim.LastModifiedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateT3_SiapKirim");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int GetKonversiDeco(int itemid)
        {
            int Jumlah=1;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from konvdeco where itemid=" + itemid );
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    Jumlah = Convert.ToInt32(sqlDataReader["Qty1Dos"]);
                }
            }
            return Jumlah;
        }
        public override ArrayList Retrieve()
        {
            string strSQL = "SELECT A.ID,A.ItemID,A.groupid,A.serahid,A.TglTrans, G.Groups as groupdesc, I1.PartNo as partnoKrm, L2.Lokasi as Lokasiser, " +
                       "A.Qtyin-A.Qtyout as Qty, L1.Lokasi AS Lokasikrm " +
                       "FROM T3_SiapKirim AS A left JOIN " +
                       "T3_Serah AS B ON A.SerahID = B.ID left JOIN " +
                       "FC_Lokasi AS L2 ON B.LokID = L2.ID left JOIN " +
                       "FC_Items AS I1 ON A.ItemID = I1.ID left JOIN " +
                       "FC_Lokasi AS L1 ON A.LokID = L1.ID left JOIN " +
                       "t3_Groupm AS G ON A.GroupID = G.ID where A.qtyin-A.Qtyout>0 order by A.ID ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_SiapKirim = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_SiapKirim.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT3_SiapKirim.Add(new T3_SiapKirim());

            return arrT3_SiapKirim;
        }
        public  T3_SiapKirim  RetrieveByID(int ID)
        {
            string strSQL = "SELECT A.ID,A.ItemID,A.groupid,A.serahid,A.TglTrans, G.Groups as groupdesc, I1.PartNo as partnoKrm, L2.Lokasi as Lokasiser, " +
                       "A.Qtyin-A.Qtyout as Qty, L1.Lokasi AS Lokasikrm " +
                       "FROM T3_SiapKirim AS A left JOIN " +
                       "T3_Serah AS B ON A.SerahID = B.ID left JOIN " +
                       "FC_Lokasi AS L2 ON B.LokID = L2.ID left JOIN " +
                       "FC_Items AS I1 ON A.ItemID = I1.ID left JOIN " +
                       "FC_Lokasi AS L1 ON A.LokID = L1.ID left JOIN " +
                       "t3_Groupm AS G ON A.GroupID = G.ID where A.qtyin-A.Qtyout>0 and A.id=" + ID;
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

            return new T3_SiapKirim();
        }

        public  ArrayList Retrievebyluas(int luas,int itemidmkt)
        {
            string strSQL = "SELECT A.ID,A.ItemID,A.groupid,A.serahid,A.TglTrans, G.Groups as groupdesc, I1.PartNo as partnoKrm, L2.Lokasi as Lokasiser, " +
                       "A.Qtyin-A.Qtyout as Qty, L1.Lokasi AS Lokasikrm " +
                       "FROM T3_SiapKirim AS A left JOIN " +
                       "T3_Serah AS B ON A.SerahID = B.ID left JOIN " +
                       "FC_Lokasi AS L2 ON B.LokID = L2.ID left JOIN " +
                       "FC_Items AS I1 ON A.ItemID = I1.ID left JOIN " +
                       "FC_Lokasi AS L1 ON A.LokID = L1.ID left JOIN " +
                       "t3_Groupm AS G ON A.GroupID = G.ID where A.rowstatus>-1 and A.qtyin-A.Qtyout>0 and I1.panjang*I1.lebar="+
                       luas + "  and A.ItemID in(select itemidfc from FC_LinkItemMkt where itemidmkt=" + itemidmkt + ") order by A.ID ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_SiapKirim = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_SiapKirim.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT3_SiapKirim.Add(new T3_SiapKirim());

            return arrT3_SiapKirim;
        }

        public ArrayList RetrievebyVolume(decimal   Volume, int  itemidmkt)
        {
            int VVolume = 0;
            VVolume = Convert.ToInt32(Volume);
            string strSQL = "SELECT A.ID,A.ItemID,A.groupid,A.serahid,A.TglTrans, G.Groups as groupdesc, I1.PartNo as partnoKrm, L2.Lokasi as Lokasiser, " +
                       "A.Qtyin-A.Qtyout as Qty, L1.Lokasi AS Lokasikrm " +
                       "FROM T3_SiapKirim AS A left JOIN " +
                       "T3_Serah AS B ON A.SerahID = B.ID left JOIN " +
                       "FC_Lokasi AS L2 ON B.LokID = L2.ID left JOIN " +
                       "FC_Items AS I1 ON A.ItemID = I1.ID left JOIN " +
                       "FC_Lokasi AS L1 ON A.LokID = L1.ID left JOIN " +
                       "t3_Groupm AS G ON A.GroupID = G.ID where A.qtyin-A.Qtyout>0 and cast(I1.tebal*I1.panjang*I1.lebar as int)=" +
                       VVolume + "  and A.ItemID in(select itemidfc from FC_LinkItemMkt where itemidmkt=" + itemidmkt + ") order by A.ID ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_SiapKirim = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_SiapKirim.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT3_SiapKirim.Add(new T3_SiapKirim());

            return arrT3_SiapKirim;
        }
        public ArrayList RetrievebyVolumeNew(decimal tebal,int panjang,int lebar,string itemidsj)
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";
            string strSQL = "SELECT B.ID,B.ItemID,B.groupid,B.ID serahid,B.lastmodifiedtime TglTrans, G.Groups  as groupdesc, " +
               "I1.PartNo as partnoKrm, L2.Lokasi as Lokasiser, B.Qty, L1.Lokasi AS Lokasikrm     " +
               "FROM T3_Serah AS B left JOIN FC_Lokasi AS L2 ON B.LokID = L2.ID left JOIN FC_Items AS I1 ON B.ItemID = I1.ID left JOIN     " +
               "FC_Lokasi AS L1 ON B.LokID = L1.ID  left JOIN t3_Groupm AS G ON I1.GroupID = G.ID    " +
               "where B.rowstatus>-1 and B.qty>0 and I1.Tebal* I1.panjang*I1.lebar= " + (tebal * panjang * lebar).ToString(nfi) +
               " and (L1.LOkasi = 'L99' or L1.LOkasi = 'M99') and itemid in (select ItemIDFc  from FC_LinkItemMkt where ItemIDMkt =" + itemidsj + ") order by B.ID";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_SiapKirim = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_SiapKirim.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT3_SiapKirim.Add(new T3_SiapKirim());

            return arrT3_SiapKirim;
        }
        public ArrayList RetrievebySJNo(string  SJNo, int itemidmkt)
        {
            //int VVolume = 0;
            //VVolume = Convert.ToInt32(Volume);
            string strSQL = "SELECT A.ID,A.ItemID,A.groupid,A.serahid,A.TglTrans, G.Groups as groupdesc, I1.PartNo as partnoKrm, L2.Lokasi as Lokasiser, " +
                       "A.Qtyin-A.Qtyout as Qty, L1.Lokasi AS Lokasikrm " +
                       "FROM T3_SiapKirim AS A left JOIN " +
                       "T3_Serah AS B ON A.SerahID = B.ID left JOIN " +
                       "FC_Lokasi AS L2 ON B.LokID = L2.ID left JOIN " +
                       "FC_Items AS I1 ON A.ItemID = I1.ID left JOIN " +
                       "FC_Lokasi AS L1 ON A.LokID = L1.ID left JOIN " +
                       "t3_Groupm AS G ON A.GroupID = G.ID where A.qtyin-A.Qtyout>0 and A.SJNO='" +
                       SJNo + "'  and A.ItemID in(select itemidfc from FC_LinkItemMkt where itemidmkt=" + itemidmkt + ") order by A.ID ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_SiapKirim = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_SiapKirim.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT3_SiapKirim.Add(new T3_SiapKirim());

            return arrT3_SiapKirim;
        }
        //public int UpdatebyKirim(TransactionManager transManager )
        //{
        //    try
        //    {
        //        objT3_SiapKirim = (T3_SiapKirim)objDomain;
        //        sqlListParam = new List<SqlParameter>();
        //        sqlListParam.Add(new SqlParameter("@ID", objT3_SiapKirim.ID ));
        //        sqlListParam.Add(new SqlParameter("@qty", objT3_SiapKirim.Qty ));
        //        sqlListParam.Add(new SqlParameter("@lastmodifiedby",objT3_SiapKirim.LastModifiedBy ));
        //        int intResult = transManager.DoTransaction(sqlListParam, "spUpdateT3_SiapKirim");
        //        strError = transManager.Error;
        //        return intResult;
        //    }
        //    catch (Exception ex)
        //    {
        //        strError = ex.Message;
        //        return -1;
        //    }
        //}

        public ArrayList RetrieveByTgl(string tgl)
        {
            string strSQL = "SELECT A.ID,A.ItemID,A.groupid,A.serahid,A.TglTrans, G.Groups as groupdesc, I1.PartNo as partnoKrm, L2.Lokasi as Lokasiser, " +
                        "A.Qtyin as Qty, L1.Lokasi AS Lokasikrm "+
                        "FROM T3_SiapKirim AS A left JOIN "+
                        "T3_Serah AS B ON A.SerahID = B.ID left JOIN "+
                        "FC_Lokasi AS L2 ON B.LokID = L2.ID left JOIN "+
                        "FC_Items AS I1 ON A.ItemID = I1.ID left JOIN "+
                        "FC_Lokasi AS L1 ON A.LokID = L1.ID left JOIN " +
                        "t3_Groupm AS G ON A.GroupID = G.ID where A.qtyin-A.Qtyout>0 and CONVERT(varchar, A.tgltrans, 112)='" + tgl + "' order by A.ID desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_SiapKirim = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_SiapKirim.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT3_SiapKirim.Add(new T3_SiapKirim());

            return arrT3_SiapKirim;
        }
        public T3_SiapKirim GenerateObject(SqlDataReader sqlDataReader)
        {
            objT3_SiapKirim = new T3_SiapKirim();
            objT3_SiapKirim.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objT3_SiapKirim.SerahID = Convert.ToInt32(sqlDataReader["serahID"]);
            objT3_SiapKirim.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objT3_SiapKirim.ItemIDSer  = Convert.ToInt32(sqlDataReader["itemid"]);
            objT3_SiapKirim.LokasiSer = (sqlDataReader["LokasiSer"]).ToString();
            objT3_SiapKirim.GroupDesc = (sqlDataReader["GroupDesc"]).ToString();
            objT3_SiapKirim.PartnoKrm = (sqlDataReader["PartnoKrm"]).ToString();
            objT3_SiapKirim.Qty = Convert.ToInt32(sqlDataReader["Qty"]);
            objT3_SiapKirim.LokasiKrm = (sqlDataReader["LokasiKrm"]).ToString();
            objT3_SiapKirim.Tgltrans = Convert.ToDateTime(sqlDataReader["TglTrans"]);
            return objT3_SiapKirim;
        }


    }
}
