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
    public class ISO_MasterUPD : AbstractFacade
    {
        private ISO_Upd objUPD = new ISO_Upd();
        private ArrayList arrUPD;
        private List<SqlParameter> sqlListParam;

        public ISO_MasterUPD()
            : base()
        {
        }

        public override int Insert(object objDomain)
        {
            try
            {
                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@DocName", objUPD.CreatedTime));
                
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertISO_UpdDMD");

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
                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@id", objUPD.IDmaster));
               

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteRules");

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

                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@IDmaster", objUPD.IDmaster));

                int intResult = dataAccess.ProcessData(sqlListParam, "sp_updateUPDMaster");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateShare(object objDomain)
        {
            try
            {

                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objUPD.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "sp_updateUPDMasterShare");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateHapus(object objDomain)
        {
            try
            {

                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@IDmaster", objUPD.IDmaster));
                sqlListParam.Add(new SqlParameter("@TglNonAktif", objUPD.TglNonAktif));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@ID", objUPD.ID));
                sqlListParam.Add(new SqlParameter("@Alasan", objUPD.Alasan));
                int intResult = dataAccess.ProcessData(sqlListParam, "sp_updateHapus");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateHapusISO(object objDomain)
        {
            try
            {

                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@IDmaster", objUPD.IDmaster));
                sqlListParam.Add(new SqlParameter("@TglNonAktif", objUPD.TglNonAktif));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@Alasan", objUPD.Alasan));

                int intResult = dataAccess.ProcessData(sqlListParam, "sp_updateHapusISO");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateHapusISOKhusus(object objDomain)
        {
            try
            {

                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@NamaDokumen", objUPD.NamaDokumen));
                sqlListParam.Add(new SqlParameter("@TglNonAktif", objUPD.TglNonAktif));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@Alasan", objUPD.Alasan));

                int intResult = dataAccess.ProcessData(sqlListParam, "sp_updateHapusISOKhusus");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from Rules where RowStatus = 0 order by rulename");
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new Rules());

            return arrUPD;
        }
        public ArrayList RetrieveByapv(string apv)
        {
            string strSQL = "SELECT A.id,A.itemid,B.partno,C.lokasi,A.qty,A.itemid " +
                "FROM  t3_serah AS A , fc_items AS B , fc_lokasi as C where A.itemid=B.id and A.lokid=C.id and A.qty>0 and A.lokid in (select id from fc_lokasi where lokasi='bsauto')";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ISO_Upd RetrieveByUPDno(string strUPDM)
        {
            string strsql = "select id,docname,categoryupd from iso_upddmd where nodocument='" + strUPDM + "'";
           
            
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new ISO_Upd();
        }

        public ISO_Upd GenerateObject(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            ////objUPD.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objUPD.IDmaster = sqlDataReader["IDmaster"].ToString();
            //objUPD2.Aktif = sqlDataReader["Aktif"].ToString();
            //objUPD2.CategoryUPD = sqlDataReader["CategoryUPD"].ToString();
            //objT3_SerahBS.AdjustDate = Convert.ToDateTime(sqlDataReader["AdjustDate"]);
            //objUPDM.NoDocument = sqlDataReader["NoDocument"].ToString();
            //objUPDM.DocName = sqlDataReader["CreatedBy"].ToString();
            //objUPDM.DocName = sqlDataReader["LastModifiedBy"].ToString();
            //objUPDM.DocName = sqlDataReader["RowStatus"].ToString();
            //objUPDM.TglBerlaku = Convert.ToDateTime(sqlDataReader["tglBerlaku"]);
            //objUPD.ID = Convert.ToInt32(sqlDataReader["id"]);
            //objUPD.Status = Convert.ToInt32(sqlDataReader["id"]);
            //objUPD.Qty = Convert.ToInt32(sqlDataReader["qty"]);
            
            return objUPD;
        }


    }
}

   
