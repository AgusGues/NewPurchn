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
    public class ROPFacade : AbstractFacade
    {
        private ROP objROP = new ROP();
        private ArrayList arrROP;
        private List<SqlParameter> sqlListParam;


        public ROPFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objROP = (ROP)objDomain;
                sqlListParam = new List<SqlParameter>();
                //sqlListParam.Add(new SqlParameter("@userid", users));
                sqlListParam.Add(new SqlParameter("@ItemID", objROP.ItemID));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertROP");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public  int InsertROP(int itemid,int users)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@userid", users));
                sqlListParam.Add(new SqlParameter("@ItemID", itemid));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertROP");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public override int Update(object objDomain)
        {
            try
            {
                objROP = (ROP)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objROP.UserID));
                sqlListParam.Add(new SqlParameter("@RuleName", objROP.ItemID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objROP.CreatedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateROP");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateROPBySPP(int itemid,int sppid, decimal sppqty)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", itemid));
                sqlListParam.Add(new SqlParameter("@sppid", sppid));
                sqlListParam.Add(new SqlParameter("@sppqty", sppqty));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateROPBySPP");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateROPByReceipt(int itemid, decimal sppqty)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", itemid));
                sqlListParam.Add(new SqlParameter("@sppqty", sppqty));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateROPByReceipt");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Delete(object objDomain)
        {
            try
            {
                objROP = (ROP)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objROP.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objROP.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteROP");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override ArrayList Retrieve()
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from ROP where Status = 0");
            strError = dataAccess.Error;
            arrROP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrROP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrROP.Add(new ROP());

            return arrROP;
        }

        public ROP RetrieveByCode(string ruleName)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from ROP where RowStatus = 0 and RuleName = '" + ruleName + "'");
            strError = dataAccess.Error;
            arrROP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new ROP();
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from ROP where RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrROP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrROP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrROP.Add(new ROP());

            return arrROP;
        }

        public ROP GenerateObject(SqlDataReader sqlDataReader)
        {
            objROP = new ROP();
            objROP.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objROP.ItemID = Convert.ToInt32(sqlDataReader["RuleName"]);
            objROP.Status = Convert.ToInt32(sqlDataReader["RowStatus"]);
            
            return objROP;

        }
        public decimal JumlahSPPBlmPO(int ItemID, int ItemTypeID)
        {
            decimal data = 0;
            string strSQL = "select ISNULL((SUM(Quantity)-SUM(QtyPO)),0)Jumlah from SPPDetail sp " +
                            "LEFT JOIN SPP s ON s.ID =sp.SPPID " +
                            "where sp.status>-1 and s.Status>-1 and sp.itemid=" + ItemID + " and sp.ItemTypeID=" + ItemTypeID +
                            "and Quantity >QtyPO";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            data = (da.Error == string.Empty) ? ProcessData(sdr) : 0;
            return data;
        }
        public decimal JumlahPOblmReceipt(int ItemID, int ItemTypeID)
        {
            decimal data = 0;
            //string strSQL = "select ISNULL(SUM(PO.Qty),0)AS Jumlah from POPurchnDetail AS PO " +
            //                "LEFT JOIN PoPurchn as P on P.ID=PO.POID " +
            //                "WHERE PO.Status>-1 and PO.ItemTypeID=" + ItemTypeID + " and PO.itemid =" + ItemID + " and " +
            //                "PO.ID not in(select PODetailID from ReceiptDetail AS RD where RD.RowStatus>-1 and RD.ItemID=" +
            //                ItemID + " and RD.ItemTypeID=" + ItemTypeID + ") " +
            //                "AND P.Status>-1";

            string strSQL =

            "select isnull(sum(qtyp) - sum(Quantityrd),0) Jumlah from( " +
            "select pd.ID idp, pd.ItemID itemidp, sum(pd.Qty) qtyp, p.NoPO pop from POPurchn p " +
            "left join POPurchnDetail pd on p.ID = pd.POID where pd.ItemID = " + ItemID + " and pd.ItemTypeID = " + ItemTypeID + " and pd.Status > -1 group by pd.ID, pd.ItemID, p.NoPO) A " +
            "full outer join " +
            "(select PODetailID podetailrd, ItemID itemidrd, sum(rd.Quantity) Quantityrd, PONo pord " +
            "from ReceiptDetail rd where rd.ItemID = " + ItemID + " and rd.ItemTypeID = " + ItemTypeID + " and RowStatus > -1 group by rd.PODetailID, rd.ItemID, rd.PONo) B on A.idp = B.podetailrd";


            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            data = (da.Error == string.Empty) ? ProcessData(sdr) : 0;
            return data;
        }
        public decimal ProcessData(SqlDataReader sdr)
        {
            decimal data = 0;
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    data = Convert.ToDecimal(sdr["Jumlah"].ToString());
                }
            }
            return data;
        }
    }
}
