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
using Dapper;

namespace Factory
{
    public class BM_DestackingFacade : AbstractFacade
    {
        private BM_Destacking objBM_Destacking = new BM_Destacking();
        private ArrayList arrBM_Destacking;
        private List<SqlParameter> sqlListParam;

        public BM_DestackingFacade()
            : base()
        {

        }

        public int Insert(BM_Destacking.BMDestacking destacking)
        {
            try
            {
                DateTime tglproduksi = DateTime.Parse(destacking.TglProduksi);
                DateTime drjam = DateTime.Parse(destacking.DrJam);
                DateTime sdjam = DateTime.Parse(destacking.SdJam);
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@PlantID", destacking.PlantID));
                sqlListParam.Add(new SqlParameter("@PlantGroupID", destacking.PlantGroupID));
                sqlListParam.Add(new SqlParameter("@FormulaID", destacking.FormulaID));
                sqlListParam.Add(new SqlParameter("@LokasiID", destacking.LokasiID));
                sqlListParam.Add(new SqlParameter("@PaletID", destacking.PaletID));
                sqlListParam.Add(new SqlParameter("@ItemID", destacking.ItemID));
                sqlListParam.Add(new SqlParameter("@TglProduksi", tglproduksi));
                sqlListParam.Add(new SqlParameter("@Qty", destacking.Qty ));
                sqlListParam.Add(new SqlParameter("@id_dstk", destacking.Id_dstk));
                sqlListParam.Add(new SqlParameter("@CreatedBy", destacking.CreatedBy));
                sqlListParam.Add(new SqlParameter("@Shift", destacking.Shift));
                sqlListParam.Add(new SqlParameter("@drJam", drjam));
                sqlListParam.Add(new SqlParameter("@SdJam", sdjam));
                //sqlListParam.Add(new SqlParameter("@DepoID", destacking.DepoID));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertBM_DestackingA");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdatePalet(int ID, int status)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", ID));
                sqlListParam.Add(new SqlParameter("@Status", status));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateBM_Palet");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateStatus(int ID, int status)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", ID));
                sqlListParam.Add(new SqlParameter("@Status", status));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateBM_Destackingbyjemur");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateHPP(int Plantid, int plantgroupID, int formulaid, string tgproduksi,decimal HPP)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Plantid", Plantid));
                sqlListParam.Add(new SqlParameter("@plantgroupID", plantgroupID));
                sqlListParam.Add(new SqlParameter("@formulaid", formulaid));
                sqlListParam.Add(new SqlParameter("@tgproduksi", tgproduksi));
                sqlListParam.Add(new SqlParameter("@HPP", HPP));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateHPPDestacking");
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
                objBM_Destacking = (BM_Destacking)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objBM_Destacking.ID));
                sqlListParam.Add(new SqlParameter("@PlantID", objBM_Destacking.PlantID));
                sqlListParam.Add(new SqlParameter("@PlantGroupID", objBM_Destacking.PlantGroupID));
                sqlListParam.Add(new SqlParameter("@FormulaID", objBM_Destacking.FormulaID));
                sqlListParam.Add(new SqlParameter("@LokasiID", objBM_Destacking.LokasiID));
                sqlListParam.Add(new SqlParameter("@PaletID", objBM_Destacking.PaletID));
                sqlListParam.Add(new SqlParameter("@ItemID", objBM_Destacking.ItemID));
                sqlListParam.Add(new SqlParameter("@Qty", objBM_Destacking.Qty ));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objBM_Destacking.CreatedBy));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateBM_Destacking");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateForAdjust(object objDomain)
        {
            try
            {
                objBM_Destacking = (BM_Destacking)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objBM_Destacking.ID));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objBM_Destacking.CreatedBy));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateBM_DestackingAdj");
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
                objBM_Destacking = (BM_Destacking)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objBM_Destacking.ID));
               
                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteBM_Destacking");
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
            throw new NotImplementedException();
        }

        public int GetTotalProduk(int Plantid, int plantgroupID, int formulaid, string tgproduksi)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(sum(qty),0) as qty from bm_destacking where rowstatus>-1 and plantid=" + Plantid +
                " and plantgroupid= " + plantgroupID + "and formulaid= " + formulaid + "and convert(varchar,TglProduksi,112)='" + tgproduksi + " '");
            strError = dataAccess.Error;
            int qty = 0;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    qty = Convert.ToInt32(sqlDataReader["qty"]);
                }
            }
            else
                qty = 0;

            return qty;
        }
        public int GetTotalProdukBln( string blnthn)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(sum(qty),0) as qty from bm_destacking where rowstatus>-1 " +
                " and left(convert(varchar,TglProduksi,112),6)='" + blnthn + " '");
            strError = dataAccess.Error;
            int qty = 0;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    qty = Convert.ToInt32(sqlDataReader["qty"]);
                }
            }
            else
                qty = 0;

            return qty;
        }
        public int GetTotalSerahBln(string blnthn)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(sum(qtyin),0) as qty from t1_serah where status>-1 " +
                " and left(convert(varchar,Tglserah,112),6)='" + blnthn + " '");
            strError = dataAccess.Error;
            int qty = 0;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    qty = Convert.ToInt32(sqlDataReader["qty"]);
                }
            }
            else
                qty = 0;

            return qty;
        }
        public int  GetPartnoID(string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "Select id from fc_items where  rowstatus>-1 and  partno='" + strValue + "'";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            int Partno = 0;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    Partno = Convert.ToInt32(sqlDataReader["id"]);
                }
            }
            else
                Partno = 0;

            return Partno;
        }
        
        public int GetLokID(string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select id from fc_Lokasi where  rowstatus>-1 and Lokasi='" + strValue + "'");
            strError = dataAccess.Error;
            int LokID = 0;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    LokID = Convert.ToInt32(sqlDataReader["id"]);
                 
                }
            }
            else
                LokID = 0;

            return LokID;
        }

        public string GetLokasiID(string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select id from fc_Lokasi where  rowstatus>-1 and Lokasi='" + strValue + "'");
            strError = dataAccess.Error;
            string LokID = null;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    LokID = (sqlDataReader["id"]).ToString();
                }
            }
            else
                LokID = null;

            return LokID;
        }

        public int GetLokIDBM(string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select id from fc_Lokasi where  loktypeid=1 and rowstatus>-1 and Lokasi='" + strValue + "'");
            strError = dataAccess.Error;
            int LokID = 0;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    LokID = Convert.ToInt32(sqlDataReader["id"]);
                }
            }
            else
                LokID = 0;

            return LokID;
        }

        public int GetPaletID(string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select id from BM_Palet where  rowstatus>-1 and noPalet='" + strValue + "'");
            strError = dataAccess.Error;
            int PaletID = 0;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    PaletID = Convert.ToInt32(sqlDataReader["id"]);
                }
            }
            else
                PaletID = 0;

            return PaletID;
        }
        public int GetID(string nopalet,string tglproduksi)
        {
            string strSQL = "Select id from BM_destacking where  rowstatus>-1 and paletID in (select ID from bm_palet where noPalet='" + nopalet +
                "') and convert(char,tglproduksi,112)='" + tglproduksi + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            int ID = 0;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    ID = Convert.ToInt32(sqlDataReader["id"]);
                }
            }

            return ID;
        }
        public BM_Destacking RetrieveByNoPalet(string strPalet,string tanggal)
        {
            string strSQL = "SELECT A.ID,D.PlantName, B.[Group], E.FormulaCode, F.NoPAlet, A.TglProduksi, A.Qty, C.Lokasi, G.PartNo,A.ItemID,A.lokasiID, " +
                    "case A.status when 0 then 'Curing' when 1 then 'Jemur' when 2 then 'Serah' end Status,isnull(A.drJam,'1/1/1900')drJam,isnull(A.sdJam,'1/1/1900')sdJam  FROM BM_Destacking AS A LEFT OUTER JOIN  " +
                    "BM_Formula AS E ON A.FormulaID = E.ID LEFT OUTER JOIN FC_Lokasi AS C ON A.LokasiID = C.ID LEFT OUTER JOIN " +
                    "BM_PlantGroup AS B ON A.PlantGroupID = B.ID LEFT OUTER JOIN BM_Palet AS F ON A.PaletID = F.ID LEFT OUTER JOIN " +
                    "FC_Items AS G ON A.ItemID = G.ID LEFT OUTER JOIN BM_Plant AS D ON A.PlantID = D.ID where  A.rowstatus>-1 and convert(varchar,tglProduksi,112)='" +
                    tanggal + "' and F.Nopalet='" + strPalet + "'";
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
            return new BM_Destacking();
        }

        public ArrayList  RetrieveByTglProduksi(string  tglProduksi)
        {
            string strSQL = "SELECT A.ID,D.PlantName, B.[Group], E.FormulaCode, F.NoPAlet, A.TglProduksi, A.Qty, C.Lokasi, G.PartNo,A.ItemID,A.lokasiID, " +
                    "case A.status when 0 then 'Curing' when 1 then 'Jemur' when 2 then 'Serah' end Status,isnull(A.drJam,'1/1/1900')drJam,isnull(A.sdJam,'1/1/1900')sdJam  FROM BM_Destacking AS A LEFT OUTER JOIN  " +
                    "BM_Formula AS E ON A.FormulaID = E.ID LEFT OUTER JOIN FC_Lokasi AS C ON A.LokasiID = C.ID LEFT OUTER JOIN " +
                    "BM_PlantGroup AS B ON A.PlantGroupID = B.ID LEFT OUTER JOIN BM_Palet AS F ON A.PaletID = F.ID LEFT OUTER JOIN " +
                    "FC_Items AS G ON A.ItemID = G.ID LEFT OUTER JOIN BM_Plant AS D ON A.PlantID = D.ID where  A.rowstatus>-1 and convert(varchar,tglProduksi,112)='" + 
                    tglProduksi + "' order by A.ID desc"; 
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrBM_Destacking = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBM_Destacking.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrBM_Destacking.Add(new BM_Destacking());

            return  arrBM_Destacking;
        }
        public ArrayList RetrieveByTglProduksi1(string tglProduksi1, string tglProduksi2)
        {
            string strSQL = "SELECT A.ID,D.PlantName, A.Shift,B.[Group], E.FormulaCode, F.NoPAlet, A.TglProduksi, A.Qty, C.Lokasi, G.PartNo,A.ItemID,A.lokasiID, " +
                    "case A.status when 0 then 'Curing' when 1 then 'Jemur' when 2 then 'Serah' end Status,isnull(A.drJam,'1/1/1900')drJam,isnull(A.sdJam,'1/1/1900')sdJam FROM BM_Destacking AS A LEFT OUTER JOIN  " +
                    "BM_Formula AS E ON A.FormulaID = E.ID LEFT OUTER JOIN FC_Lokasi AS C ON A.LokasiID = C.ID LEFT OUTER JOIN " +
                    "BM_PlantGroup AS B ON A.PlantGroupID = B.ID LEFT OUTER JOIN BM_Palet AS F ON A.PaletID = F.ID LEFT OUTER JOIN " +
                    "FC_Items AS G ON A.ItemID = G.ID LEFT OUTER JOIN BM_Plant AS D ON A.PlantID = D.ID where  A.rowstatus>-1 and " +
                    "convert(varchar,tglProduksi,112)>='" + tglProduksi1 + "'and convert(varchar,tglProduksi,112)<='" + tglProduksi2 + "' order by D.PlantName,A.Shift,B.[Group]";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrBM_Destacking = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBM_Destacking.Add(GenerateObject1(sqlDataReader));
                }
            }
            else
                arrBM_Destacking.Add(new BM_Destacking());

            return arrBM_Destacking;
        }

        public ArrayList RetrieveCriteria(string Criteria,string strvalue,string tanggal)
        {
            string strSQL = "SELECT A.ID,D.PlantName, B.[Group], E.FormulaCode, F.NoPAlet, A.TglProduksi, A.Qty, C.Lokasi, G.PartNo,A.ItemID,A.lokasiID, " +
                      "case A.status when 0 then 'Curing' when 1 then 'Jemur' when 2 then 'Serah' end Status,isnull(A.drJam,'1/1/1900')drJam,isnull(A.sdJam,'1/1/1900')sdJam  FROM BM_Destacking AS A LEFT OUTER JOIN " +
                      "BM_Plant AS D ON A.PlantID = D.ID LEFT OUTER JOIN " +
                      "BM_PlantGroup AS B ON A.PlantGroupID = B.ID LEFT OUTER JOIN " +
                      "BM_Formula AS E ON A.FormulaID = E.ID LEFT OUTER JOIN " +
                      "FC_Lokasi AS C ON A.LokasiID = C.ID LEFT OUTER JOIN " +
                      "BM_Palet AS F ON A.PaletID = F.ID LEFT OUTER JOIN " +
                      "FC_Items AS G ON A.ItemID = G.ID where  A.rowstatus>-1 and A.TglProduksi ='" + tanggal + "' and  " + Criteria + strvalue + " order by A.id desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrBM_Destacking = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBM_Destacking.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrBM_Destacking.Add(new BM_Destacking());

            return arrBM_Destacking;
        }

        public ArrayList RetrieveForcuring(string tglProduksi)
        {
            string strSQL = "SELECT A.ID,D.PlantName, B.[Group], E.FormulaCode, F.NoPAlet, A.TglProduksi, A.Qty, C.Lokasi, G.PartNo,A.ItemID,A.lokasiID, " +
                      "case A.status when 0 then 'Curing' when 1 then 'Jemur' when 2 then 'Serah' end Status,isnull(A.drJam,'1/1/1900')drJam,isnull(A.sdJam,'1/1/1900')sdJam  FROM BM_Destacking AS A LEFT OUTER JOIN " +
                      "BM_Plant AS D ON A.PlantID = D.ID LEFT OUTER JOIN " +
                      "BM_PlantGroup AS B ON A.PlantGroupID = B.ID LEFT OUTER JOIN " +
                      "BM_Formula AS E ON A.FormulaID = E.ID LEFT OUTER JOIN " +
                      "FC_Lokasi AS C ON A.LokasiID = C.ID LEFT OUTER JOIN " +
                      "BM_Palet AS F ON A.PaletID = F.ID LEFT OUTER JOIN " +
                      "FC_Items AS G ON A.ItemID = G.ID where  A.rowstatus>-1 and A.status=0 and A.tglProduksi='" + tglProduksi + "' order by A.id desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrBM_Destacking = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBM_Destacking.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrBM_Destacking.Add(new BM_Destacking());

            return arrBM_Destacking;
        }

        public BM_Destacking GenerateObject(SqlDataReader sqlDataReader)
        {
            objBM_Destacking = new BM_Destacking();
            objBM_Destacking.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objBM_Destacking.PlantGroup = (sqlDataReader["Group"]).ToString();
            objBM_Destacking.Formula  = (sqlDataReader["FormulaCode"]).ToString();
            objBM_Destacking.Palet  = (sqlDataReader["NoPAlet"]).ToString();
            objBM_Destacking.TglProduksi = Convert.ToDateTime(sqlDataReader["TglProduksi"]);
            objBM_Destacking.Qty = Convert.ToInt32(sqlDataReader["Qty"]);
            objBM_Destacking.Lokasi = (sqlDataReader["Lokasi"]).ToString();
            objBM_Destacking.Partno = (sqlDataReader["PartNo"]).ToString();
            objBM_Destacking.Status = (sqlDataReader["Status"]).ToString();
            objBM_Destacking.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objBM_Destacking.LokasiID = Convert.ToInt32(sqlDataReader["LokasiID"]);
            objBM_Destacking.DrJam = Convert.ToDateTime(sqlDataReader["DrJam"]);
            objBM_Destacking.SdJam = Convert.ToDateTime(sqlDataReader["SdJam"]);
            return objBM_Destacking;

        }
        public BM_Destacking GenerateObject1(SqlDataReader sqlDataReader)
        {
            objBM_Destacking = new BM_Destacking();
            objBM_Destacking.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objBM_Destacking.PlantGroup = (sqlDataReader["Group"]).ToString();
            objBM_Destacking.Formula = (sqlDataReader["FormulaCode"]).ToString();
            objBM_Destacking.Palet = (sqlDataReader["NoPAlet"]).ToString();
            objBM_Destacking.TglProduksi = Convert.ToDateTime(sqlDataReader["TglProduksi"]);
            objBM_Destacking.Qty = Convert.ToInt32(sqlDataReader["Qty"]);
            objBM_Destacking.Lokasi = (sqlDataReader["Lokasi"]).ToString();
            objBM_Destacking.Partno = (sqlDataReader["PartNo"]).ToString();
            objBM_Destacking.Status = (sqlDataReader["Status"]).ToString();
            objBM_Destacking.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objBM_Destacking.LokasiID = Convert.ToInt32(sqlDataReader["LokasiID"]);
            objBM_Destacking.Shift = Convert.ToInt32(sqlDataReader["Shift"]);
            objBM_Destacking.PlantName = (sqlDataReader["PlantName"]).ToString();
            objBM_Destacking.DrJam = Convert.ToDateTime(sqlDataReader["DrJam"]);
            objBM_Destacking.SdJam = Convert.ToDateTime(sqlDataReader["SdJam"]);
            return objBM_Destacking;

        }


        public static List<BM_Destacking.Destacking> GetListDestackingbyTgl(string tgl)
        {
            List<BM_Destacking.Destacking> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "SELECT A.ID,D.PlantName, B.[Group], E.FormulaCode, F.NoPAlet, A.TglProduksi, A.Qty, C.Lokasi, G.PartNo,A.ItemID,A.lokasiID, case A.status when 0 then 'Curing' when 1 then 'Jemur' when 2 then 'Serah' end Status,isnull(A.drJam,'1/1/1900')drJam,isnull(A.sdJam,'1/1/1900')sdJam " +
                        " FROM BM_Destacking AS A LEFT OUTER JOIN BM_Formula AS E ON A.FormulaID = E.ID LEFT OUTER JOIN FC_Lokasi AS C ON A.LokasiID = C.ID LEFT OUTER JOIN BM_PlantGroup AS B ON A.PlantGroupID = B.ID LEFT OUTER JOIN BM_Palet AS F ON A.PaletID = F.ID LEFT OUTER JOIN FC_Items AS G ON A.ItemID = G.ID LEFT OUTER JOIN BM_Plant AS D ON A.PlantID = D.ID where  A.rowstatus>-1 and convert(varchar,tglProduksi,112)='" + tgl + "' order by A.ID desc";
                    AllData = connection.Query<BM_Destacking.Destacking>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }

        public int GetRecordCountInYear(string tgl)
        {
            int recCount;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "Select top 1 SUBSTRING( id_dstk ,6,5) as id from bm_destacking where rowstatus>-1 and id_dstk !='' and LEFT(convert(varchar,TglProduksi,112),6)='" + tgl + "' and id_dstk!='' order by id_dstk desc";
                    recCount = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {
                    recCount = 0;
                }
            }
            return recCount;
        }

        public int GetPlantID(int id)
        {
            int recCount;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "Select plantid from bm_plantgroup where ID=" + id +"";
                    recCount = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {
                    recCount = 0;
                }
            }
            return recCount;
        }

        public int CekStatusSerah(int id)
        {
            int recCount;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "SELECT COUNT(*) FROM T1_SERAH WHERE DESTID = '"+ id +"'";
                    recCount = connection.QueryFirstOrDefault<int>(query);
                }
                catch (Exception e)
                {
                    recCount = 0;
                }
            }
            return recCount;
        }

        public int CancelDestacking(int ID)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", ID));
                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteBM_Destacking");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Insert(object objDomain)
        {
            try
            {
                objBM_Destacking = (BM_Destacking)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@PlantID", objBM_Destacking.PlantID));
                sqlListParam.Add(new SqlParameter("@PlantGroupID", objBM_Destacking.PlantGroupID));
                sqlListParam.Add(new SqlParameter("@FormulaID", objBM_Destacking.FormulaID));
                sqlListParam.Add(new SqlParameter("@LokasiID", objBM_Destacking.LokasiID));
                sqlListParam.Add(new SqlParameter("@PaletID", objBM_Destacking.PaletID));
                sqlListParam.Add(new SqlParameter("@ItemID", objBM_Destacking.ItemID));
                sqlListParam.Add(new SqlParameter("@TglProduksi", objBM_Destacking.TglProduksi));
                sqlListParam.Add(new SqlParameter("@Qty", objBM_Destacking.Qty));
                sqlListParam.Add(new SqlParameter("@id_dstk", objBM_Destacking.Id_dstk));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objBM_Destacking.CreatedBy));
                sqlListParam.Add(new SqlParameter("@Shift", objBM_Destacking.Shift));
                sqlListParam.Add(new SqlParameter("@drJam", objBM_Destacking.DrJam));
                sqlListParam.Add(new SqlParameter("@SdJam", objBM_Destacking.SdJam));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertBM_DestackingA");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
    }
}
