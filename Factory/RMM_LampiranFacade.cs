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
    public class RMM_LampiranFacade : AbstractFacade
    {
        private RMM_Lampiran objRMMLampiran = new RMM_Lampiran();
        private ArrayList arrRMMLampiran;
        private List<SqlParameter> sqlListParam;

        public RMM_LampiranFacade()
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
                objRMMLampiran = (RMM_Lampiran)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@RMM_ID", objRMMLampiran.RMM_ID));
                sqlListParam.Add(new SqlParameter("@FileName", objRMMLampiran.FileName));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objRMMLampiran.CreatedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "RMM_InsertLampiran");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public ArrayList RetrieveLampiran(string idRMM)
        {

            string strSQL = "select ID ,RMM_ID,FileName from RMM_Lampiran where ID=" + idRMM + " and rowstatus > -1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrRMMLampiran = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrRMMLampiran.Add(GenerateObjectLampiran(sqlDataReader));
                }
            }
            else
                arrRMMLampiran.Add(new RMM_Lampiran());

            return arrRMMLampiran;
        }

        public ArrayList RetrieveLampiranByNo(string laporan)
        {

            string strSQL = "select ID,RMM_ID as RMM_ID,FileName,format(CreatedTime,'dd/MM/yyyy')CreatedTime from RMM_Lampiran where RMM_ID in (select ID from RMM where RMM_No='" + laporan + "') and rowstatus > -1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrRMMLampiran = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrRMMLampiran.Add(GenerateObjectLampiran(sqlDataReader));
                }
            }
            else
                arrRMMLampiran.Add(new RMM_Lampiran());

            return arrRMMLampiran;
        }

        public string GetIDlampiran(string idRMM)
        {
            string strsql = "select ID from RMM_Lampiran where RMM_ID=" + idRMM + " and RowStatus > -1";
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

        public string GetFileName(string idRMM)
        {
            string strsql = "select filename from RMM_Lampiran where RMM_ID=" + idRMM + " and RowStatus > -1";
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

        public string GetApvTPP(string idRMM)
        {
            string strsql = "select apv from RMM where ID=" + idRMM + " and RowStatus > -1";
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

        public string hapus(string idRMM)
        {
            string strsql = "update RMM_lampiran set RowStatus = -1 where ID=" + idRMM;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;


            return string.Empty;
        }

        public RMM_Lampiran GenerateObjectLampiran(SqlDataReader sqlDataReader)
        {
            objRMMLampiran = new RMM_Lampiran();
            objRMMLampiran.FileName = sqlDataReader["FileName"].ToString();
            objRMMLampiran.TanggalUpload = sqlDataReader["CreatedTime"].ToString();
            objRMMLampiran.RMM_ID = Convert.ToInt32(sqlDataReader["RMM_ID"]);
            objRMMLampiran.ID = Convert.ToInt32(sqlDataReader["ID"]);
            return objRMMLampiran;
        }
    }

}
