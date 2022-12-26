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
    public class ISO_DocumentNoFacade : AbstractFacade
    {
        private ISO_DocumentNo objDocumentNo = new ISO_DocumentNo();
        private ArrayList arrDocumentNo;
        private List<SqlParameter> sqlListParam;


        public ISO_DocumentNoFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objDocumentNo = (ISO_DocumentNo)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@PesType", objDocumentNo.PesType));
                sqlListParam.Add(new SqlParameter("@DeptID", objDocumentNo.DeptID));
                sqlListParam.Add(new SqlParameter("@Tahun", objDocumentNo.Tahun));
                sqlListParam.Add(new SqlParameter("@DocNo", objDocumentNo.DocNo));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertISODocumentNo");

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
                objDocumentNo = (ISO_DocumentNo)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objDocumentNo.ID));
                sqlListParam.Add(new SqlParameter("@DocNo", objDocumentNo.DocNo));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateISODocumentNo");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ISO_DocumentNo");
            strError = dataAccess.Error;
            arrDocumentNo = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDocumentNo.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrDocumentNo.Add(new ISO_DocumentNo());

            return arrDocumentNo;
        }

        public ISO_DocumentNo RetrieveByDept(int perType, int deptID, int thn)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ISO_DocumentNo where PesType="+perType+" and DeptID="+deptID+" and Tahun="+thn);
            strError = dataAccess.Error;
            arrDocumentNo = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new ISO_DocumentNo();
        }

        public ISO_DocumentNo GenerateObject(SqlDataReader sqlDataReader)
        {
            objDocumentNo = new ISO_DocumentNo();
            objDocumentNo.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objDocumentNo.PesType = Convert.ToInt32(sqlDataReader["PesType"]);
            objDocumentNo.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objDocumentNo.Tahun = Convert.ToInt32(sqlDataReader["Tahun"]);
            objDocumentNo.DocNo = Convert.ToInt32(sqlDataReader["DocNo"]);

            return objDocumentNo;

        }
    }

}
