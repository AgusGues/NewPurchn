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
    public class StockMovementFacade : AbstractFacade
    {
        private StockMovement objStockMovement = new StockMovement();
        private ArrayList arrStockMovement;
        private List<SqlParameter> sqlListParam;

        public StockMovementFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objStockMovement = (StockMovement)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@TypeDoc", objStockMovement.TypeDoc));
                sqlListParam.Add(new SqlParameter("@NoDoc", objStockMovement.NoDoc));
                sqlListParam.Add(new SqlParameter("@TglDoc", objStockMovement.TglDoc));
                sqlListParam.Add(new SqlParameter("@ItemID", objStockMovement.ItemID));                
                sqlListParam.Add(new SqlParameter("@DepoID", objStockMovement.DepoID));
                sqlListParam.Add(new SqlParameter("@Quantity", objStockMovement.Quantity));
                sqlListParam.Add(new SqlParameter("@Status", objStockMovement.Status));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objStockMovement.CreatedBy));
                sqlListParam.Add(new SqlParameter("@ToDepoID", objStockMovement.ToDepoID));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertStockMovement");

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
            return 0;
        }

        public override int Delete(object objDomain)
        {

            return 0;
        }

        public override ArrayList Retrieve()
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select StockMovement.ID,StockMovement.TypeDoc,StockMovement.NoDoc,StockMovement.TglDoc,StockMovement.ItemID,Items.ItemCode,Items.Description as ItemName,StockMovement.DepoID,Depo.DepoName,StockMovement.Quantity,StockMovement.Status,StockMovement.CreatedBy from StockMovement,Depo,Items where StockMovement.ItemID = Items.ID and StockMovement.DepoID = Depo.ID");
            strError = dataAccess.Error;
            arrStockMovement = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrStockMovement.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrStockMovement.Add(new StockMovement());

            return arrStockMovement;
        }
     
     

        public StockMovement GenerateObject(SqlDataReader sqlDataReader)
        {
            objStockMovement = new StockMovement();
            objStockMovement.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objStockMovement.TypeDoc = Convert.ToInt32(sqlDataReader["TypeDoc"]);
            objStockMovement.NoDoc = sqlDataReader["NoDoc"].ToString();
            objStockMovement.TglDoc = Convert.ToDateTime(sqlDataReader["TglDoc"]);
            objStockMovement.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objStockMovement.ItemCode = sqlDataReader["ItemCode"].ToString();
            objStockMovement.ItemName = sqlDataReader["ItemName"].ToString();
            objStockMovement.DepoID = Convert.ToInt32(sqlDataReader["DepoID"]);
            objStockMovement.DepoName = sqlDataReader["DepoName"].ToString();
            objStockMovement.Quantity = Convert.ToInt32(sqlDataReader["Quantity"]);
            objStockMovement.Status = Convert.ToInt32(sqlDataReader["Status"]);           
            objStockMovement.CreatedBy = sqlDataReader["CreatedBy"].ToString();            
            return objStockMovement;

        }
    }
}

