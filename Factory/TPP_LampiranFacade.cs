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
    public class TPP_LampiranFacade : AbstractFacade
    {
        private TPP_Lampiran objTPPLampir = new TPP_Lampiran();        
        private ArrayList arrTPPLampir;
        private List<SqlParameter> sqlListParam;

        public TPP_LampiranFacade()
            : base()
        {
        }

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

        public override ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }

        public int insertLampiran(object objDomain)
        {
            try
            {
                objTPPLampir = (TPP_Lampiran)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@TPP_ID", objTPPLampir.TPP_ID));
                sqlListParam.Add(new SqlParameter("@FileName", objTPPLampir.FileName));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objTPPLampir.CreatedBy));
               
                int intResult = dataAccess.ProcessData(sqlListParam, "TPP_InsertLampiran");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public ArrayList RetrieveLampiran(string idTPP)
        {

            string strSQL = "select ID ,TPP_ID,FileName,CreatedTime from TPP_Lampiran where ID=" + idTPP + " and rowstatus > -1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrTPPLampir = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTPPLampir.Add(GenerateObjectLampiran(sqlDataReader));
                }
            }
            else
                arrTPPLampir.Add(new TPP_Lampiran());

            return arrTPPLampir;
        }
        public ArrayList RetrieveLampiranByNo(string laporan)
        {

            string strSQL = "select ID,TPP_ID as TPP_ID,FileName,CreatedTime from TPP_Lampiran where TPP_ID in (select ID from tpp where laporan_no='" + laporan + "') and rowstatus > -1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrTPPLampir = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrTPPLampir.Add(GenerateObjectLampiran(sqlDataReader));
                }
            }
            else
                arrTPPLampir.Add(new TPP_Lampiran());

            return arrTPPLampir;
        }
        public string GetIDlampiran(string idTPP)
        {
            string strsql = "select ID from TPP_Lampiran where TPP_ID=" + idTPP + " and RowStatus > -1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return sqlDataReader["ID"].ToString();
                }
            }
            return string.Empty;
        }

        public string GetFileName(string idTPP)
        {
            string strsql = "select filename from TPP_Lampiran where TPP_ID=" + idTPP + " and RowStatus > -1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return sqlDataReader["filename"].ToString();
                }
            }
            return string.Empty;
        }

        public string GetApvTPP(string idTPP)
        {
            string strsql = "select apv from TPP where ID=" + idTPP + " and RowStatus > -1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return sqlDataReader["apv"].ToString();
                }
            }
            return string.Empty;
        }

        public string hapus(string idTPP)
        {
            string strsql = "update tpp_lampiran set RowStatus = -1 where ID=" + idTPP;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            
            return string.Empty;
        }
        public TPP_Lampiran GenerateObjectLampiran(SqlDataReader sqlDataReader)
        {
            objTPPLampir = new TPP_Lampiran();
            objTPPLampir.FileName = sqlDataReader["FileName"].ToString();
            objTPPLampir.TanggalUpload = sqlDataReader["CreatedTime"].ToString();
            objTPPLampir.TPP_ID = Convert.ToInt32(sqlDataReader["TPP_ID"]);
            objTPPLampir.ID = Convert.ToInt32(sqlDataReader["ID"]);
            return objTPPLampir;
        }

    }
}
