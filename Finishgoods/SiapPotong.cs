using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using BusinessFacade;
using Domain;
using Factory;
using DataAccessLayer;

namespace FinishGoods
{
    public class SiapPotong:AbstractFacade
    {
        private SP objSP = new SP();
        private ArrayList arrSP;
        //private List<SqlParameter> sqlListParam;

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
        private string Criteria()
        {
            string strquery=(HttpContext.Current.Session["where"]==null)?string.Empty:
                            HttpContext.Current.Session["where"].ToString();
            return strquery;
        }
        public override ArrayList Retrieve()
        {
            #region query

            string Ukuran = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/FinishedGoods.ini")).Read("LamaJemur", this.Criteria());
            string strSQL = "SELECT Top 100 Tj.ID,Tj.DestID,Tj.TglJemur,Tj.CreatedTime,isnull(Bm.HPP,0)HPP,BM.ID,Fc.PartNo,FC.Tebal,P.NoPAlet, " +
                            "R.Rak,Tj.QtyIn,Bm.Qty,Tj.QtyOut,BM.ItemID,BM.TglProduksi,Tj.RakID,  " +
                            "DATEDIFF(D,Tj.TglJemur,GETDATE()) Umur " +
                            "FROM T1_Jemur AS Tj " +
                            "LEFT JOIN BM_Destacking AS Bm " +
                            "ON Bm.ID=tj.DestID " +
                            "LEFT JOIN FC_Items AS FC " +
                            "ON FC.ID=BM.ItemID " +
                            "LEFT JOIN BM_Palet AS P " +
                            "ON P.ID=BM.PaletID " +
                            "LEFT JOIN FC_Rak AS R " +
                            "ON R.ID=Tj.RakID " +
                            "WHERE Tj.TglJemur<=(DATEADD(DD,DATEDIFF(DY,0,GETDATE())," + Ukuran + "))  " +
                            "and Tj.QtyOut=0 AND Tj.qtyIn >0 and Tj.RowStatus>-1  " +
                            "and YEAR(tglJemur)>=2014 " +
                            "and FC.Tebal=" + this.Criteria().Substring(5, (this.Criteria().Length - 5)) +
                            "ORDER BY Bm.TglProduksi,Tj.ID";
            #endregion
            DataAccess dataAcces = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAcces.RetrieveDataByString(strSQL);
            arrSP=new ArrayList();
            if (dataAcces.Error == string.Empty && sqlDataReader.HasRows)
            {
                while(sqlDataReader.Read())
                {
                    arrSP.Add(new SP
                    {
                        ID = Convert.ToInt32(sqlDataReader["ID"].ToString()),
                        ItemID = Convert.ToInt32(sqlDataReader["ItemID"].ToString()),
                        DestID = Convert.ToInt32(sqlDataReader["DestID"].ToString()),
                        PartNo = sqlDataReader["PartNo"].ToString(),
                        RakNo = sqlDataReader["Rak"].ToString(),
                        RakID = Convert.ToInt32(sqlDataReader["RakID"].ToString()),
                        PaletNo = sqlDataReader["NoPAlet"].ToString(),
                        Qty = Convert.ToInt32(sqlDataReader["Qty"].ToString()),
                        QtyIn = Convert.ToInt32(sqlDataReader["QtyIn"].ToString()),
                        Tebal = Convert.ToDecimal(sqlDataReader["Tebal"].ToString()),
                        TglProduksi = Convert.ToDateTime(sqlDataReader["TglProduksi"].ToString()),
                        TglJemur = Convert.ToDateTime(sqlDataReader["TglJemur"].ToString()),
                        UmurJemur = Convert.ToInt32(sqlDataReader["Umur"].ToString())
                    });
                }
            }
            return arrSP;
        }
        public SP RetrieveDetail()
        {
            string strSQL = "SELECT Tj.ID,Tj.DestID,Tj.TglJemur,Tj.CreatedTime," +
                            "isnull(Bm.HPP,0)HPP,BM.ID,Fc.PartNo,FC.Tebal,P.NoPAlet,R.Rak,Tj.QtyIn,Bm.Qty,Tj.QtyOut " +
                            "FROM T1_Jemur AS Tj LEFT JOIN BM_Destacking AS Bm ON Bm.ID=tj.DestID LEFT JOIN FC_Items AS FC " +
                            "ON FC.ID=BM.ItemID LEFT JOIN BM_Palet AS P ON P.ID=BM.PaletID " +
                            "LEFT JOIN FC_Rak AS R ON R.ID=Tj.RakID " + this.Criteria();

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            if (dataAccess.Error == string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    objSP=new SP();
                    objSP.PartNo = sqlDataReader["PartNo"].ToString();
                    objSP.TglJemur =Convert.ToDateTime(sqlDataReader["TglJemur"].ToString());
                    objSP.RakNo = sqlDataReader["Rak"].ToString();
                    objSP.Qty = Convert.ToInt32(sqlDataReader["QtyIn"].ToString());
                    return objSP;
                }
            }
            return new SP();
        }
    }
}
