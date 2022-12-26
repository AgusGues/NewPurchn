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
    public class T3_AsimetrisFacade : BusinessFacade.AbstractTransactionFacadeF
    {
        private T3_Asimetris objT3_Asimetris = new T3_Asimetris();
        private ArrayList arrT3_Asimetris;
        private List<SqlParameter> sqlListParam;

       
        public T3_AsimetrisFacade(object objDomain)
            : base(objDomain)
        {
            objT3_Asimetris = (T3_Asimetris)objDomain;
        }
        public T3_AsimetrisFacade()
        {
        }
        public override int Insert1(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }
        public override int Update1(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }
        public override int Update2(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }
        public override int Update(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Delete(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }
        public override int Insert(TransactionManager transManager)
        {
            try
            {
                objT3_Asimetris = (T3_Asimetris)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@rekapID", objT3_Asimetris.RekapID));
                sqlListParam.Add(new SqlParameter("@Docno", objT3_Asimetris.DocNo ));
                sqlListParam.Add(new SqlParameter("@SerahID", objT3_Asimetris.SerahID));
                sqlListParam.Add(new SqlParameter("@GroupID", objT3_Asimetris.GroupID));
                sqlListParam.Add(new SqlParameter("@LokID", objT3_Asimetris.LokIDOut ));
                sqlListParam.Add(new SqlParameter("@ItemID", objT3_Asimetris.ItemIDOut ));
                sqlListParam.Add(new SqlParameter("@TglTrans", objT3_Asimetris.TglTrans ));
                sqlListParam.Add(new SqlParameter("@QtyIn", objT3_Asimetris.QtyIn ));
                sqlListParam.Add(new SqlParameter("@QtyOut", objT3_Asimetris.QtyOut));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT3_Asimetris.CreatedBy));
                sqlListParam.Add(new SqlParameter("@MCutter", objT3_Asimetris.MCutter));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertT3_Asimetris");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        
        public ArrayList RetrieveBytgl(string tgl)
        {
            string strSQL = "SELECT distinct A.docno, A.tgltrans, I.PartNo as PartNoIn , L.Lokasi as LokasiIn, A.QtyIn FROM T3_Asimetris AS A " +
                    "INNER JOIN T3_Serah AS B ON A.SerahID = B.ID INNER JOIN FC_Lokasi AS L ON B.LokID = L.ID INNER JOIN FC_Items AS I ON "+
            "B.ItemID = I.ID where A.RowStatus >-1 and convert(varchar,A.tgltrans,112)='" + tgl + "' "; 
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_Asimetris = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_Asimetris.Add(GenerateObjectMaster(sqlDataReader));
                }
            }
            else
                arrT3_Asimetris.Add(new T3_Asimetris());

            return arrT3_Asimetris;
        }
        public ArrayList RetrieveByDocNo(string DocNo)
        {
            string strSQL = "SELECT I.PartNo as PartNoOut , L.Lokasi as LokasiOut,A.QtyOut, D.Groups FROM T3_Asimetris AS A INNER JOIN " +
                            "FC_Lokasi AS L ON A.LokID = L.ID INNER JOIN FC_Items AS I ON A.ItemID = I.ID LEFT JOIN T3_GroupM AS D "+
                            "ON A.GroupID = D.ID where A.rowstatus>-1 and A.docno='" + DocNo + "' ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_Asimetris = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_Asimetris.Add(GenerateObjectDetail(sqlDataReader));
                }
            }
            else
                arrT3_Asimetris.Add(new T3_Asimetris());

            return arrT3_Asimetris;
        }
        public int  GetDocNo(DateTime  tgltrans)
        {
            int docnocount = 0;
            string strSQL = "select count(docno)as docnocount from ( select distinct docno from t3_asimetris where month(TglTrans) =" +
                tgltrans.Month + " and year(TglTrans)=" + tgltrans.Year  + " ) as a ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_Asimetris = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    docnocount = Convert.ToInt16(sqlDataReader["docnocount"]);
                }
            }
            return docnocount;
        }
        public T3_Asimetris GenerateObjectMaster(SqlDataReader sqlDataReader)
        {
            objT3_Asimetris = new T3_Asimetris();
            objT3_Asimetris.DocNo  = sqlDataReader["Docno"].ToString();
            objT3_Asimetris.PartnoIn = sqlDataReader["PartnoIn"].ToString();
            objT3_Asimetris.LokasiIn = sqlDataReader["LokasiIn"].ToString();
            objT3_Asimetris.QtyIn = Convert.ToInt32(sqlDataReader["QtyIn"]);
            objT3_Asimetris.TglTrans = Convert.ToDateTime(sqlDataReader["TglTrans"]);
            return objT3_Asimetris;
        }
        public T3_Asimetris GenerateObjectDetail(SqlDataReader sqlDataReader)
        {
            objT3_Asimetris = new T3_Asimetris();
            objT3_Asimetris.PartnoOut = sqlDataReader["PartnoOut"].ToString();
            objT3_Asimetris.LokasiOut = sqlDataReader["LokasiOut"].ToString();
            objT3_Asimetris.QtyOut = Convert.ToInt32(sqlDataReader["QtyOut"]);
            objT3_Asimetris.GroupName = (sqlDataReader["groups"]).ToString();
            return objT3_Asimetris;
        }

        public override ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }

    }
}
