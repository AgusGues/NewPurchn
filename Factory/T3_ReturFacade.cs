using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using BusinessFacade;
using DataAccessLayer;

namespace Factory
{
    public class T3_ReturFacade : AbstractTransactionFacadeF
    {
        private T3_Retur objT3_Retur = new T3_Retur();
        private ArrayList arrT3_Retur;
        private List<SqlParameter> sqlListParam;

        public T3_ReturFacade(object objDomain)
            : base(objDomain)
        {
            objT3_Retur = (T3_Retur)objDomain;
        }

        public T3_ReturFacade()
        {
        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                objT3_Retur = (T3_Retur)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@SJNO", objT3_Retur.SJNO));
                sqlListParam.Add(new SqlParameter("@OPNO", objT3_Retur.OPNO));
                sqlListParam.Add(new SqlParameter("@Customer", objT3_Retur.Customer));
                sqlListParam.Add(new SqlParameter("@SerahID", objT3_Retur.SerahID));
                sqlListParam.Add(new SqlParameter("@LokID", objT3_Retur.LokasiID));
                sqlListParam.Add(new SqlParameter("@ItemID", objT3_Retur.ItemIDSer));
                sqlListParam.Add(new SqlParameter("@ItemIDSJ", objT3_Retur.ItemIDSJ ));
                sqlListParam.Add(new SqlParameter("@TglTrans", objT3_Retur.Tgltrans ));
                sqlListParam.Add(new SqlParameter("@Qty", objT3_Retur.Qty));
                sqlListParam.Add(new SqlParameter("@HPP", objT3_Retur.HPP));
                sqlListParam.Add(new SqlParameter("@GroupID", objT3_Retur.GroupID));
                sqlListParam.Add(new SqlParameter("@ReturID", objT3_Retur.ReturID ));
                sqlListParam.Add(new SqlParameter("@Expedisi", objT3_Retur.Expedisi ));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objT3_Retur.CreatedBy));
                sqlListParam.Add(new SqlParameter("@SA", objT3_Retur.SA));
                sqlListParam.Add(new SqlParameter("@TypeR", objT3_Retur.TypeR));

                sqlListParam.Add(new SqlParameter("@JDefect", objT3_Retur.JDefect));
                sqlListParam.Add(new SqlParameter("@TglProd", objT3_Retur.TglProd));
                sqlListParam.Add(new SqlParameter("@GroupProd", objT3_Retur.GroupProd));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertT3_Retur");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
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
        public override System.Collections.ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }

        public ArrayList RetrieveByTgl(string tgl, string sjno)
        {
            string strSQL = "SELECT A.TglTrans, G.Groups AS groupdesc, I1.PartNo AS partnoKrm, L2.Lokasi AS Lokasiser, A.Qty, L1.Lokasi AS Lokasikrm, A.Customer, A.SJNo, A.OPNo "+
                "FROM FC_Lokasi AS L2 RIGHT OUTER JOIN T3_Serah AS B RIGHT OUTER JOIN T3_Retur AS A ON B.ID = A.SerahID LEFT OUTER JOIN  "+
                "FC_Items AS I1 ON A.ItemID = I1.ID LEFT OUTER JOIN FC_Lokasi AS L1 ON A.LokID = L1.ID LEFT OUTER JOIN T3_GroupM AS G ON A.GroupID = G.ID ON L2.ID = B.LokID " +
                "where CONVERT(varchar, A.tgltrans, 112)='" + tgl + "' or sjno='" + sjno + "'order by A.ID desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_Retur = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_Retur.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT3_Retur.Add(new T3_Retur());

            return arrT3_Retur;
        }

        public ArrayList RetrieveByTgl0(string tgl)
        {
            string strSQL = "SELECT A.ID,A.TglTrans, G.Groups AS groupdesc, I1.PartNo AS partnoKrm, L2.Lokasi AS Lokasiser, A.Qty, L1.Lokasi AS Lokasikrm, A.Customer, A.SJNo, A.OPNo " +
                "FROM FC_Lokasi AS L2 RIGHT OUTER JOIN T3_Serah AS B RIGHT OUTER JOIN T3_Retur AS A ON B.ID = A.SerahID LEFT OUTER JOIN  " +
                "FC_Items AS I1 ON A.ItemID = I1.ID LEFT OUTER JOIN FC_Lokasi AS L1 ON A.LokID = L1.ID LEFT OUTER JOIN T3_GroupM AS G ON A.GroupID = G.ID ON L2.ID = B.LokID " +
                "where CONVERT(varchar, A.tgltrans, 112)='" + tgl +  "' and A.RowStatus >-1 order by A.ID desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_Retur = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_Retur.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT3_Retur.Add(new T3_Retur());

            return arrT3_Retur;
        }
        public ArrayList RetrieveBySJ(string sjno,string itemID)
        {
            string strSQL = "SELECT A.TglTrans, G.Groups AS groupdesc, I1.PartNo AS partnortr, L2.Lokasi AS Lokasiser, A.Qty, "+
                        "L1.Lokasi AS Lokasirtr, A.Customer, A.SJNo, A.OPNo " +
                      "FROM T3_Retur AS A left JOIN " +
                      "T3_Serah AS B ON A.SerahID = B.ID left JOIN " +
                      "FC_Lokasi AS L2 ON B.LokID = L2.ID left JOIN " +
                      "FC_Items AS I1 ON A.ItemID = I1.ID left JOIN " +
                      "FC_Lokasi AS L1 ON A.LokID = L1.ID left JOIN " +
                      "T3_GroupM AS G ON A.GroupID = G.ID where A.sjno='" + sjno + "' and A.itemidSJ=" + itemID  + " order by A.ID desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_Retur = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_Retur.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT3_Retur.Add(new T3_Retur());

            return arrT3_Retur;
        }

        public T3_Retur GenerateObject(SqlDataReader sqlDataReader)
        {
            objT3_Retur = new T3_Retur();
            objT3_Retur.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objT3_Retur.SJNO = (sqlDataReader["SJNO"]).ToString();
            objT3_Retur.OPNO = (sqlDataReader["OPNO"]).ToString();
            objT3_Retur.Customer = (sqlDataReader["Customer"]).ToString();
            objT3_Retur.LokasiSer = (sqlDataReader["LokasiSer"]).ToString();
            objT3_Retur.GroupDesc = (sqlDataReader["GroupDesc"]).ToString();
            objT3_Retur.PartnoRtr = (sqlDataReader["PartnoKrm"]).ToString();
            objT3_Retur.Qty = Convert.ToInt32(sqlDataReader["Qty"]);
            objT3_Retur.LokasiRtr = (sqlDataReader["LokasiKrm"]).ToString();
            objT3_Retur.Tgltrans = Convert.ToDateTime(sqlDataReader["TglTrans"]);
            return objT3_Retur;
        }

        public override int Update(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Delete(TransactionManager transManager)
        {
            throw new NotImplementedException();
        }
    }
}
