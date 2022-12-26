using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;
using BusinessFacade;

namespace Factory
{
    public class T1_PaletPelarianFacade : BusinessFacade.AbstractTransactionFacadeF
    {
        private T1_PaletPelarian objT1_PaletPelarian = new T1_PaletPelarian();
        private ArrayList arrT1_PaletPelarian;
        private List<SqlParameter> sqlListParam;

        public T1_PaletPelarianFacade(object objDomain)
            : base(objDomain)
        {
            objT1_PaletPelarian = (T1_PaletPelarian)objDomain;
        }
        public T1_PaletPelarianFacade()
        {
        }
       
        public override int Delete(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }
        
        public override int Insert(TransactionManager transManager)
        {
            try
            {
                objT1_PaletPelarian = (T1_PaletPelarian)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@TglPotong", objT1_PaletPelarian.TglPotong));
                sqlListParam.Add(new SqlParameter("@NoPalet", objT1_PaletPelarian.NoPalet));
                sqlListParam.Add(new SqlParameter("@PartnoAsal", objT1_PaletPelarian.PartnoAsal));
                sqlListParam.Add(new SqlParameter("@QtyAsal", objT1_PaletPelarian.QtyAsal));
                sqlListParam.Add(new SqlParameter("@PartnoOK", objT1_PaletPelarian.PartnoOK));
                sqlListParam.Add(new SqlParameter("@QtyOK", objT1_PaletPelarian.QtyOK));
                sqlListParam.Add(new SqlParameter("@PartnoBP", objT1_PaletPelarian.PartnoBP));
                sqlListParam.Add(new SqlParameter("@QtyBP", objT1_PaletPelarian.QtyBP));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT1_PaletPelarian.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertT1_PaletPelarian");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public override int Update(TransactionManager transManager)//UpdateQtyJemur
        {
            try
            {
                objT1_PaletPelarian = (T1_PaletPelarian)objDomain;
                sqlListParam = new List<SqlParameter>();
                //sqlListParam.Add(new SqlParameter("@Flag", objT1_PaletPelarian.Flag));
                //sqlListParam.Add(new SqlParameter("@ID", objT1_PaletPelarian.ID));
                //sqlListParam.Add(new SqlParameter("@QtyOut", objT1_PaletPelarian.QtyOut));
                //sqlListParam.Add(new SqlParameter("@CreatedBy", objT1_PaletPelarian.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateQtyT1_PaletPelarian");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public override int Insert1(TransactionManager transManager) //insert jemurlg
        {
            int intResult=0;
            try
            {
                objT1_PaletPelarian = (T1_PaletPelarian)objDomain;
                sqlListParam = new List<SqlParameter>();
                //sqlListParam.Add(new SqlParameter("@ItemID0", objT1_PaletPelarian.ItemID0 ));
                //sqlListParam.Add(new SqlParameter("@lokid0", objT1_PaletPelarian.LokasiID0 ));
                //sqlListParam.Add(new SqlParameter("@lokid", objT1_PaletPelarian.LokasiID ));
                //sqlListParam.Add(new SqlParameter("@JemurID", objT1_PaletPelarian.ID));
                //sqlListParam.Add(new SqlParameter("@DestID", objT1_PaletPelarian.DestID));
                //sqlListParam.Add(new SqlParameter("@ItemID", objT1_PaletPelarian.ItemID));
                //sqlListParam.Add(new SqlParameter("@RakID", objT1_PaletPelarian.RakID));
                //sqlListParam.Add(new SqlParameter("@TglTrans", objT1_PaletPelarian.TglTrans));
                //sqlListParam.Add(new SqlParameter("@QtyIn", objT1_PaletPelarian.QtyIn ));
                //sqlListParam.Add(new SqlParameter("@HPP", objT1_PaletPelarian.HPP));
                //sqlListParam.Add(new SqlParameter("@CreatedBy", objT1_PaletPelarian.CreatedBy));
                intResult = transManager.DoTransaction(sqlListParam, "spInsertT1_PaletPelarian");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public override int Update1(TransactionManager transManager) //Update QtyJemurlg
        {
            try
            {
                objT1_PaletPelarian = (T1_PaletPelarian)objDomain;
                sqlListParam = new List<SqlParameter>();
                //sqlListParam.Add(new SqlParameter("@lokid", objT1_PaletPelarian.LokasiID));
                //sqlListParam.Add(new SqlParameter("@ID", objT1_PaletPelarian.ID));
                //sqlListParam.Add(new SqlParameter("@ItemID", objT1_PaletPelarian.ItemID));
                //sqlListParam.Add(new SqlParameter("@QtyOut", objT1_PaletPelarian.QtyOut ));
                //sqlListParam.Add(new SqlParameter("@CreatedBy", objT1_PaletPelarian.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateT1_PaletPelarian");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public override int Update2(TransactionManager transManager) //UpdateQtyJemurfromserah
        {
            try
            {
                objT1_PaletPelarian = (T1_PaletPelarian)objDomain;
                sqlListParam = new List<SqlParameter>();
                //sqlListParam.Add(new SqlParameter("@ID", objT1_PaletPelarian.ID));
                //sqlListParam.Add(new SqlParameter("@QtyOut", objT1_PaletPelarian.QtyOut));
                //sqlListParam.Add(new SqlParameter("@CreatedBy", objT1_PaletPelarian.CreatedBy));
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateQtyT1_PaletPelarian");
                strError = transManager.Error;
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
            throw new NotImplementedException();
        }
        public int UpdateRak(int ID, int status, TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", ID));
                sqlListParam.Add(new SqlParameter("@Status", status));
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateFC_Rak");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int GetCount(string palet)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select count(id) as ID from t1_paletpelarian where nopalet like'" + palet + "%'");
            strError = dataAccess.Error;
            int RakID = 0;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    RakID = Convert.ToInt32(sqlDataReader["ID"]);
                }
            }
            else
                RakID = 0;

            return RakID;
        }
        public ArrayList RetrieveByTgl(string tglPotong)
        {
            string strSQL = "SELECT * from t1_paletPelarian where convert(char,tglpotong,112)='" + tglPotong  + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT1_PaletPelarian = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT1_PaletPelarian.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT1_PaletPelarian.Add(new T1_PaletPelarian());

            return arrT1_PaletPelarian;
        }
        public T1_PaletPelarian  RetrieveByID(int ID)
        {
            string strSQL = "select * from t1_paletpelarian where ID=" + ID;
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
            return new T1_PaletPelarian() ;
        }
        public int UpdateFail(int id,int qty)
        {
            string strSQL = "update T1_PaletPelarian set qtyout=" + qty + " where id=" + id;
            int intError = 0;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (strError != string.Empty)
                intError = 1;
            return intError;
        }
        public T1_PaletPelarian GenerateObject(SqlDataReader sqlDataReader)
        {
            objT1_PaletPelarian = new T1_PaletPelarian();
            objT1_PaletPelarian.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objT1_PaletPelarian.TglPotong = Convert.ToDateTime(sqlDataReader["TglPotong"]);
            objT1_PaletPelarian.NoPalet = (sqlDataReader["NoPalet"]).ToString();
            objT1_PaletPelarian.PartnoAsal = (sqlDataReader["PartnoAsal"]).ToString();
            objT1_PaletPelarian.QtyAsal = Convert.ToInt32(sqlDataReader["QtyAsal"]);
            objT1_PaletPelarian.PartnoOK = sqlDataReader["PartnoOK"].ToString();
            objT1_PaletPelarian.QtyOK = Convert.ToInt32(sqlDataReader["QtyOK"]);
            objT1_PaletPelarian.PartnoBP = sqlDataReader["PartnoBP"].ToString();
            objT1_PaletPelarian.QtyBP = Convert.ToInt32(sqlDataReader["QtyBP"]);
            return objT1_PaletPelarian;
        }
    }
}
