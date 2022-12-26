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
   public class ISO_UpdDocNoFacade : AbstractFacade
   {
        private ISO_UpdDocNo objDocNo = new ISO_UpdDocNo();
        private ArrayList arrDocNo;
        private List<SqlParameter> sqlListParam;


        public ISO_UpdDocNoFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objDocNo = (ISO_UpdDocNo)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@DocType", objDocNo.DocType));
                sqlListParam.Add(new SqlParameter("@DocCat", objDocNo.DocCat));
                sqlListParam.Add(new SqlParameter("@DeptID", objDocNo.DeptID));
                sqlListParam.Add(new SqlParameter("@Tahun", objDocNo.Tahun));
                sqlListParam.Add(new SqlParameter("@DocNo", objDocNo.DocNo));
                sqlListParam.Add(new SqlParameter("@RevNo", objDocNo.RevNo));
                sqlListParam.Add(new SqlParameter("@Plant", objDocNo.Plant));
                sqlListParam.Add(new SqlParameter("@DeptCode", objDocNo.DeptCode));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertISO_UpdDocNo");

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
                objDocNo = (ISO_UpdDocNo)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objDocNo.ID));
                sqlListParam.Add(new SqlParameter("@DocNo", objDocNo.DocNo));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateISO_UpdDocNo");

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
            return 0;
        }

        public override ArrayList Retrieve()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ISO_UpdDocNo");
            strError = dataAccess.Error;
            arrDocNo = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDocNo.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrDocNo.Add(new ISO_UpdDocNo());

            return arrDocNo;
        }

        public ISO_UpdDocNo RetrieveByDept(int DocType, int deptID, int thn, string deptCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ISO_UpdDocNo where DocType=" + DocType + " and DeptID=" + deptID + " and Tahun=" + thn + "and DeptCode" + deptCode );
            strError = dataAccess.Error;
            arrDocNo = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new ISO_UpdDocNo();
        }
        public ISO_UpdDocNo RetrieveByDocTypeID(int docType)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ISO_UpdDocNo where DocType = " + docType);
            strError = dataAccess.Error;
            arrDocNo = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new ISO_UpdDocNo();
        }
        public ISO_UpdDocNo GenerateObject(SqlDataReader sqlDataReader)
        {
            objDocNo = new ISO_UpdDocNo();
            objDocNo.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objDocNo.DocType = Convert.ToInt32(sqlDataReader["DocType"]);
            objDocNo.DocCat = Convert.ToInt32(sqlDataReader["DocCat"]);
            objDocNo.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objDocNo.Tahun = Convert.ToInt32(sqlDataReader["Tahun"]);
            objDocNo.DocNo = Convert.ToInt32(sqlDataReader["DocNo"]);
            objDocNo.RevNo = Convert.ToInt32(sqlDataReader["RevNo"]);
            objDocNo.Plant = sqlDataReader["Plant"].ToString();
            //objDocNo.DeptCode = sqlDataReader["DeptCode"].ToString();

            return objDocNo;

        }
    }

}
