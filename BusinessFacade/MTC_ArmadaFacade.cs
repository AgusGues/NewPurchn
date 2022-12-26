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
    public class MTC_ArmadaFacade : AbstractTransactionFacade
    {
        private MTC_Armada objKend = new MTC_Armada();
        private ArrayList arrKendaraan;
        private List<SqlParameter> sqlListParam;

        public MTC_ArmadaFacade(object objDomain)
            : base(objDomain)
        {
            objKend = (MTC_Armada)objDomain;
        }

        public MTC_ArmadaFacade()
        {

        }

        public override int Insert(DataAccessLayer.TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@IDKendaraan", objKend.IDKendaraan));
                sqlListParam.Add(new SqlParameter("@Nopol", objKend.NoPol));
                sqlListParam.Add(new SqlParameter("@DeptID", objKend.DeptID));
                sqlListParam.Add(new SqlParameter("@ItemID", objKend.ItemID));
                sqlListParam.Add(new SqlParameter("@Quantity", objKend.Quantity));
                sqlListParam.Add(new SqlParameter("@AvgPrice", objKend.AvgPrice));
                sqlListParam.Add(new SqlParameter("@SPBDate", objKend.SPBDate));
                sqlListParam.Add(new SqlParameter("@SPBno", objKend.SPBNo));
                sqlListParam.Add(new SqlParameter("@Createdby", objKend.CreatedBy));
                sqlListParam.Add(new SqlParameter("@GroupID", objKend.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objKend.ItemTypeID));
                int intResult = transManager.DoTransaction(sqlListParam, "spArmadaInsert");
                strError = transManager.Error;

                return intResult;
            }
            catch (Exception e)
            {
                strError = e.Message;
                return -1;
            }
        }

        public override int Update(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Delete(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override System.Collections.ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }

        //public ArrayList DetailTransArmada(int IDKendaraan, string FromDate, string ToDate)
        public ArrayList DetailTransArmada(string IDKendaraan, string FromDate, string ToDate)
        {
            #region Query
            //string Criteria = (IDKendaraan == 0) ? "" : " and IDKendaraan=" + IDKendaraan;
            string Criteria = (IDKendaraan == string.Empty) ? "" : " and mt.NoPol='" + IDKendaraan + "'";
            string Criteria2 = (IDKendaraan == string.Empty) ? "" : " and B.NoPol='" + IDKendaraan + "'";
            string strSQL =
            "select * from (" +
            "select mt.NoPol,Convert(varchar,SPBDate,103) as Tanggal, " +
            "CASE mt.ItemTypeID  " +
            "    WHEN 1 THEN (SELECT ItemCode From Inventory where ID=mt.ItemID) " +
            "    WHEN 2 THEN (SELECT ItemCode FROM Asset where ID=mt.ItemID) " +
            "    WHEN 3 THEN (SELECT ItemCode FROM Biaya WHERE ID=mt.ItemID) " +
            "END ItemCode, " +
            "CASE mt.ItemTypeID  " +
            "    WHEN 1 THEN (SELECT (SELECT UOMCode FROM UOM WHERE ID=Inventory.UOMID) From Inventory where ID=mt.ItemID) " +
            "    WHEN 2 THEN (SELECT (SELECT UOMCode FROM UOM WHERE ID=Asset.UOMID) FROM Asset where ID=mt.ItemID) " +
            "    WHEN 3 THEN (SELECT (SELECT UOMCode FROM UOM WHERE ID=Biaya.UOMID) FROM Biaya WHERE ID=mt.ItemID) " +
            "END Satuan, " +
            "CASE mt.ItemTypeID  " +
            "    WHEN 1 THEN (SELECT ItemName From Inventory where ID=mt.ItemID) " +
            "    WHEN 2 THEN (SELECT ItemName FROM Asset where ID=mt.ItemID) " +
            "    WHEN 3 THEN (SELECT ItemName FROM Biaya WHERE ID=mt.ItemID) " +
            "END ItemName,pd.Quantity,pd.AvgPrice,(pd.Quantity*pd.AvgPrice) as Total, " +
            "(SELECT DeptName from Dept where ID=mt.DeptID) as DeptName " +
            "FROM MTC_Armada as mt " +
            "LEFT JOIN Pakai as p ON p.PakaiNo=mt.SPBNo and p.DeptID=mt.DeptID " +
            "LEFT JOIN PakaiDetail as pd ON pd.ItemID=mt.ItemID and pd.PakaiID=p.ID " +
            "where  " +
            "CONVERT(varchar,SPBDate,112) between '" + FromDate + "' and '" + ToDate + "'  " +
            "and pd.GroupID in (8,9) and mt.RowStatus>-1 and p.Status>1 and pd.RowStatus >-1 " + Criteria +

            "union all " +

            "select B.NoPol,Convert(varchar,A.PakaiDate,103) as Tanggal,(SELECT ItemCode From Biaya A where A.ID=ItemID and A.RowStatus>-1)ItemCode, " +
            "(SELECT UOMCode FROM UOM B WHERE B.ID=UOMID) Satuan , (SELECT ItemName FROM Biaya C WHERE C.ID=ItemID) ItemName,B.Quantity,B.AvgPrice, " +
            "B.Quantity*B.AvgPrice Total,'ARMADA'DeptName " +
            "from Pakai A INNER JOIN PakaiDetail B ON A.ID=B.PakaiID  " +
            "where  CONVERT(varchar,A.PakaiDate,112) between '" + FromDate + "' and '" + ToDate + "' and A.DeptID=26 " +
            " " + Criteria2 + " and B.GroupID=5 ) as xx Order by Nopol,Tanggal ";
            //" Order by IDKendaraan,SPBDate";
            #endregion
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrKendaraan = new ArrayList();
            int n = 0; decimal total = 0; decimal totAvg = 0;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    n = n + 1;
                    total = total + Convert.ToDecimal(sqlDataReader["Total"].ToString());
                    totAvg = totAvg + Convert.ToDecimal(sqlDataReader["AvgPrice"].ToString());
                    arrKendaraan.Add(new MTC_Armada
                    {
                        ID      =n,
                        NoPol   = sqlDataReader["NoPol"].ToString(),
                        SPBDate = Convert.ToDateTime(sqlDataReader["Tanggal"].ToString()),
                        ItemCode = sqlDataReader["ItemCode"].ToString(),
                        ItemName = sqlDataReader["ItemName"].ToString(),
                        Quantity = Convert.ToDecimal(sqlDataReader["Quantity"].ToString()),
                        AvgPrice = Convert.ToDecimal(sqlDataReader["AvgPrice"].ToString()),
                        Total  = Convert.ToDecimal(sqlDataReader["Total"].ToString()),
                        CreatedBy = sqlDataReader["DeptName"].ToString(),
                        Satuan=sqlDataReader["Satuan"].ToString(),
                        DeptName=sqlDataReader["DeptName"].ToString(),
                        TotalS=total,
                        TotAvg=totAvg
                    });
                }
            }
            //else
            //     arrKendaraan.Add(new MTC_Armada());

            return arrKendaraan;

        }
        public string GetAlias(string NoPol)
        {
            string strSQL = "Select NamaKendaraan From MTC_NamaArmada where NoPol='" + NoPol + "'";
             DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrKendaraan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return sqlDataReader["NamaKendaraan"].ToString();
                }
            }
            return string.Empty;
        }
    }
}
