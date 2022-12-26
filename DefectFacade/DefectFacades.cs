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

namespace DefectFacade
{
    public class DefectFacades : AbstractTransactionFacade
    {
        private Defect objDefect = new Defect();
        private ArrayList arrDefect;
        private List<SqlParameter> sqlListParam;

        public DefectFacades(object objDomain)
            : base(objDomain)
        {
            objDefect = (Defect)objDomain;
        }

        public DefectFacades()
        {
        }
        public override int Insert(TransactionManager transManager)
        {
            try
            {
                objDefect = (Defect)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Tgl", objDefect.Tgl));
                sqlListParam.Add(new SqlParameter("@DefectNo", objDefect.DefectNo));
                sqlListParam.Add(new SqlParameter("@JenisID", objDefect.JenisID));
                sqlListParam.Add(new SqlParameter("@TglProduksi", objDefect.TglProduksi));
                sqlListParam.Add(new SqlParameter("@ProdID", objDefect.ProdID));
                sqlListParam.Add(new SqlParameter("@GroupCutID", objDefect.GroupCutID));
                sqlListParam.Add(new SqlParameter("@GroupProdID", objDefect.GroupProdID));
                sqlListParam.Add(new SqlParameter("@GroupJemurID", objDefect.GroupJemurID));
                sqlListParam.Add(new SqlParameter("@UkuranID", objDefect.UkuranID));
                sqlListParam.Add(new SqlParameter("@PaletID", objDefect.PaletID));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objDefect.CreatedBy));
                sqlListParam.Add(new SqlParameter("@SerahID", objDefect.SerahID));
                sqlListParam.Add(new SqlParameter("@DestID", objDefect.DestID));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertDef_Defect1");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Update(TransactionManager transManager)
        {
            try
            {
                objDefect = (Defect)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Tgl", objDefect.Tgl));
                sqlListParam.Add(new SqlParameter("@DefectNo", objDefect.DefectNo));
                sqlListParam.Add(new SqlParameter("@JenisID", objDefect.JenisID));
                sqlListParam.Add(new SqlParameter("@TglProduksi", objDefect.TglProduksi));
                sqlListParam.Add(new SqlParameter("@ProdID", objDefect.ProdID));
                sqlListParam.Add(new SqlParameter("@GroupCutID", objDefect.GroupCutID));
                sqlListParam.Add(new SqlParameter("@GroupProdID", objDefect.GroupProdID));
                sqlListParam.Add(new SqlParameter("@GroupJemurID", objDefect.GroupJemurID));
                sqlListParam.Add(new SqlParameter("@UkuranID", objDefect.UkuranID));
                sqlListParam.Add(new SqlParameter("@PaletID", objDefect.PaletID));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objDefect.CreatedBy));
                sqlListParam.Add(new SqlParameter("@SerahID", objDefect.SerahID));
                sqlListParam.Add(new SqlParameter("@DestID", objDefect.DestID));
                sqlListParam.Add(new SqlParameter("@TPotong", objDefect.TPot));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertDef_Defect1A");
                strError = transManager.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int GetTotalBP()
        {
            int TBP = 0;
            string strSQL = "select isnull(sum(qty),0)TBP from tempdefectGP";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrDefect = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    TBP=Convert.ToInt32(sqlDataReader["TBP"]);
                }
            }
            return TBP;
        }
        public Decimal GetTotalBPKubik()
        {
            Decimal TBP = 0;
            string strSQL = "select isnull(sum(kubik),0)TBP from tempdefectGP";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrDefect = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    TBP = Convert.ToDecimal(sqlDataReader["TBP"]);
                }
            }
            return TBP;
        }

        public int GetTotalPotong()
        {
            int TBP = 0;
            string strSQL = "select isnull(sum(totpotong),0)TBP from tempdefectGPTot";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrDefect = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    TBP = Convert.ToInt32(sqlDataReader["TBP"]);
                }
            }
            return TBP;
        }
        public Decimal GetTotalPotongKubik()
        {
            Decimal TBP = 0;
            string strSQL = "select isnull(sum(totkubik),0)TBP from tempdefectGPTot";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrDefect = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    TBP = Convert.ToDecimal(sqlDataReader["TBP"]);
                }
            }
            return TBP;
        }
        public int CancelDefect(string id)
        {
            string strSQL = "update def_defect set status=-1 where id=" + id + " " +
                "update def_defectdetail set rowstatus=-1 where defectid=" + id + " " +
                "update t1_serah set status=0 where ID in (select serahID from def_defect where ID=" + id + " )";
            int intError = 0;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (strError != string.Empty)
                intError = 1;
            return intError;
        }
        public int getDefectID(string nopalet,string tglperiksa)
        {
            string strSQL = "select top 1 ID from def_defect where [status]>-1 and CONVERT(char,tgl,112)='" + tglperiksa +
                "' and PaletID in (select ID from BM_Palet where NoPAlet='" + nopalet + "')";
            int intError = 0;
            int ID = 0;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    ID = Convert.ToInt32(sqlDataReader["ID"]);
                }
            }
            return ID;
        }
        public ArrayList RetrieveByTgl(string tglPeriksa)
        {
            string strSQL = "SELECT A.ID,A.Tgl, A.DefectNo, A.TglProduksi, J.Jenis, GP.[Group] as GProd, GJ.GroupJemurCode as GJmr, GC.GroupCutCode as GCut, U.Description as Ukuran, P.NoPAlet," +
                "(select ISNULL(sum(qty),0) from  Def_DefectDetail where DefectID=A.ID) as Qty " +
                "FROM Def_Defect AS A LEFT OUTER JOIN BM_PlantGroup AS GP ON A.GroupProdID = GP.ID LEFT OUTER JOIN " +
                "FC_Jenis AS J ON A.JenisID = J.ID LEFT OUTER JOIN BM_Palet AS P ON A.PaletID = P.ID LEFT OUTER JOIN " +
                "Def_GroupJemur AS GJ ON A.GroupJemurID = GJ.ID LEFT OUTER JOIN Def_GroupCutter AS GC ON A.GroupCutID = GC.ID LEFT OUTER JOIN " +
                "Def_Ukuran AS U ON A.UkuranID = U.ID where A.status>-1 and convert(char,A.tgl,112)='" + tglPeriksa + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrDefect = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDefect.Add(GenerateObjectView(sqlDataReader));
                }
            }
            else
                arrDefect.Add(new Defect());

            return arrDefect;
        }
        public Defect GenerateObjectView(SqlDataReader sqlDataReader)
        {
            objDefect = new Defect();
            objDefect.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objDefect.Tgl = Convert.ToDateTime(sqlDataReader["Tgl"]);
            objDefect.TglProduksi = Convert.ToDateTime(sqlDataReader["TglProduksi"]);
            objDefect.DefectNo = sqlDataReader["DefectNo"].ToString();
            objDefect.Jenis = sqlDataReader["Jenis"].ToString();
            objDefect.GCut = sqlDataReader["GCut"].ToString();
            objDefect.GProd = sqlDataReader["GProd"].ToString();
            objDefect.GJmr = sqlDataReader["GJmr"].ToString();
            objDefect.Ukuran = sqlDataReader["Ukuran"].ToString();
            objDefect.NoPalet = sqlDataReader["NoPalet"].ToString();
            objDefect.Qty = Convert.ToInt32(sqlDataReader["Qty"]);
            return objDefect;
        }

        public Defect GenerateObject(SqlDataReader sqlDataReader)
        {
            objDefect = new Defect();
            objDefect.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objDefect.Tgl = Convert.ToDateTime(sqlDataReader["Tgl"]);
            objDefect.TglProduksi = Convert.ToDateTime(sqlDataReader["TglProduksi"]);
            objDefect.JenisID = Convert.ToInt32(sqlDataReader["JenisID"]);
            objDefect.GroupCutID = Convert.ToInt32(sqlDataReader["GroupCutID"]);
            objDefect.GroupProdID = Convert.ToInt32(sqlDataReader["GroupProdID"]);
            objDefect.GroupJemurID = Convert.ToInt32(sqlDataReader["GroupJemurID"]);
            objDefect.UkuranID = Convert.ToInt32(sqlDataReader["UkuranID"]);
            objDefect.PaletID = Convert.ToInt32(sqlDataReader["PaletID"]);
            return objDefect;
        }
        public Defect RetrieveMaxId()
        {
            string strSQL = "select isnull(MAX(LEFT(DefectNo,6)),0) as ID from Def_Defect";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrDefect = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectID(sqlDataReader);
                }
            }

            return new Defect();
        }
        public Defect GenerateObjectID(SqlDataReader sqlDataReader)
        {
            //objGrade = new MasterTransaksi();
            objDefect = new Defect();
            //objGrade.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objDefect.ID = Convert.ToInt32(sqlDataReader["ID"]);
            //return objGrade;
            return objDefect;
        }

        //public override int Insert(DataAccessLayer.TransactionManager transManager)
        //{
        //    throw new NotImplementedException();
        //}

        //public override int Update(DataAccessLayer.TransactionManager transManager)
        //{
        //    throw new NotImplementedException();
        //}

        public override int Delete(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override System.Collections.ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }
    }
}
