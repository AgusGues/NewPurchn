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
    public class DocumentIDFacade : AbstractFacade
    {
        private DocumentID objDocumentID = new DocumentID();
        private ArrayList arrDocumentID;
        private List<SqlParameter> sqlListParam;


        public DocumentIDFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objDocumentID = (DocumentID)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@OPID1", objDocumentID.OPID1));
                sqlListParam.Add(new SqlParameter("@OPID2", objDocumentID.OPID2));
                sqlListParam.Add(new SqlParameter("@DOID1", objDocumentID.DOID1));
                sqlListParam.Add(new SqlParameter("@DOID2", objDocumentID.DOID2));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertDocumentID");

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
                objDocumentID = (DocumentID)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@OPID1", objDocumentID.OPID1));
                sqlListParam.Add(new SqlParameter("@OPID2", objDocumentID.OPID2));
                sqlListParam.Add(new SqlParameter("@DOID1", objDocumentID.DOID1));
                sqlListParam.Add(new SqlParameter("@DOID2", objDocumentID.DOID2));
                sqlListParam.Add(new SqlParameter("@SJID1", objDocumentID.SJID1));
                sqlListParam.Add(new SqlParameter("@SJID2", objDocumentID.SJID2));
                sqlListParam.Add(new SqlParameter("@INVID1", objDocumentID.INVID1));
                sqlListParam.Add(new SqlParameter("@INVID2", objDocumentID.INVID2));
                sqlListParam.Add(new SqlParameter("@TypeID", objDocumentID.TypeID));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateDocumentID");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from DocumentID");
            strError = dataAccess.Error;
            arrDocumentID = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDocumentID.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrDocumentID.Add(new DocumentID());

            return arrDocumentID;
        }

        public DocumentID RetrieveByAll()
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from DocumentID");
            strError = dataAccess.Error;
            arrDocumentID = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new DocumentID();
        }                

        public DocumentID GenerateObject(SqlDataReader sqlDataReader)
        {
            objDocumentID = new DocumentID();
            objDocumentID.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objDocumentID.OPID1 = Convert.ToInt32(sqlDataReader["OPID1"]);
            objDocumentID.OPID2 = Convert.ToInt32(sqlDataReader["OPID2"]);
            objDocumentID.DOID1 = Convert.ToInt32(sqlDataReader["DOID1"]);
            objDocumentID.DOID2 = Convert.ToInt32(sqlDataReader["DOID2"]);
            objDocumentID.SJID1 = Convert.ToInt32(sqlDataReader["SJID1"]);
            objDocumentID.SJID2 = Convert.ToInt32(sqlDataReader["SJID2"]);
            objDocumentID.INVID1 = Convert.ToInt32(sqlDataReader["INVID1"]);
            objDocumentID.INVID2 = Convert.ToInt32(sqlDataReader["INVID2"]);            
            return objDocumentID;

        }
    }
}
