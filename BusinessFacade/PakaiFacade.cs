using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;
using System.Web.UI.WebControls;
using System.Web;

namespace BusinessFacade
{
    public class PakaiFacade : AbstractTransactionFacade
    {
        private Pakai objPakai = new Pakai();
        private Evaluasi eBudget = new Evaluasi();
        private ArrayList arrPakai;
        private List<SqlParameter> sqlListParam;
        private ArrayList arrData = new ArrayList();
        public PakaiFacade(object objDomain)
            : base(objDomain)
        {
            objPakai = (Pakai)objDomain;
        }

        public PakaiFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@PakaiNo", objPakai.PakaiNo));
                sqlListParam.Add(new SqlParameter("@PakaiDate", objPakai.PakaiDate));
                sqlListParam.Add(new SqlParameter("@DeptID", objPakai.DeptID));
                sqlListParam.Add(new SqlParameter("@DepoID", objPakai.DepoID));
                sqlListParam.Add(new SqlParameter("@Status", objPakai.Status));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objPakai.CreatedBy));
                sqlListParam.Add(new SqlParameter("@PakaiTipe", objPakai.PakaiTipe));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objPakai.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@PlanningID", GetPlanningID(objPakai.PakaiDate)));
                int intResult = transManager.DoTransaction(sqlListParam, "spInsertPakai_New");
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
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objPakai.ID));
                sqlListParam.Add(new SqlParameter("@PakaiNo", objPakai.PakaiNo));
                sqlListParam.Add(new SqlParameter("@PakaiDate", objPakai.PakaiDate));
                sqlListParam.Add(new SqlParameter("@DeptID", objPakai.DeptID));
                sqlListParam.Add(new SqlParameter("@DepoID", objPakai.DepoID));
                sqlListParam.Add(new SqlParameter("@Status", objPakai.Status));
                sqlListParam.Add(new SqlParameter("@AlasanCancel", objPakai.AlasanCancel));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objPakai.CreatedBy));
                sqlListParam.Add(new SqlParameter("@PakaiTipe", objPakai.PakaiTipe));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objPakai.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@ApprovalDate", objPakai.ApprovalDate));
                sqlListParam.Add(new SqlParameter("@ApprovalBy", objPakai.ApprovalBy));
                sqlListParam.Add(new SqlParameter("@Ready", objPakai.Ready));
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdatePakai");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override int Delete(TransactionManager transManager)
        {

            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objPakai.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objPakai.LastModifiedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spCancelPakai");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateReady(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objPakai.ID));
                sqlListParam.Add(new SqlParameter("@Ready", objPakai.Ready ));
                int intResult = transManager.DoTransaction(sqlListParam, "spUpdatePakaiReady");

                strError = transManager.Error;

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
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 50 A.ID,A.ScheduleNo,A.ScheduleDate,A.ExpedisiDetailID,B.ExpedisiName,C.CarType,A.TotalKubikasi,A.Status,A.Rate,A.Keterangan,A.DepoID,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from Pakai as A,Expedisi as B,ExpedisiDetail as C where A.ExpedisiDetailID = C.ID and C.ExpedisiID = B.ID and A.RowStatus = 0 order by A.ID");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 50 * from Pakai as A where A.Status>-1 order by ID");
            strError = dataAccess.Error;
            arrPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPakai.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrPakai.Add(new Pakai());

            return arrPakai;
        }
        public int UpdateRelease(int ID, int Status)
        {
            string strSQL = "Update Pakai Set Status=" + Status + " where ID=" + ID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader=dataAccess.RetrieveDataByString(strSQL);
            strError=dataAccess.Error;
            if(strError==string.Empty)
            {
                return 1;
            }
            return 0;
        }
        private string LimitedView()
        {
            string Query = string.Empty;
            Users usr = (Users)System.Web.HttpContext.Current.Session["Users"];
            Query = (usr.Apv == 0 && usr.DeptID != 10) ? " and (A.Createdby='" + usr.UserName + "' or +A.DeptID=" + usr.DeptID+" )" : string.Empty;
            Query += (usr.Apv == 1 && usr.DeptID != 10) ? " and A.DeptID=" + usr.DeptID : string.Empty;
            Query = (usr.Apv > 1) ? string.Empty : Query;
            return Query;
        }
        public ArrayList RetrieveOpenStatus(string strTipePakai)
        {
            string tipePakai = string.Empty;
            tipePakai = " and pakaino like '" + strTipePakai + "%'";
            string strSQL = "select A.ID,A.PakaiNo,A.PakaiDate,A.DeptID,A.DepoID,A.PakaiTipe,A.CreatedBy,A.Status,B.ItemID,B.Quantity,A.ApproveDate, " +
                            "B.Keterangan,B.UomID,C.ItemCode,C.ItemName,D.UOMCode,A.ItemTypeID,isnull(A.ready,0) as ready, CASE A.Status  WHEN 0 THEN ('Open')  " +
                            "WHEN 1 THEN ('Head')   WHEN 2 THEN ('Manager') else ('Gudang') END AS Apv,isnull(JenisBiaya,0) as JenisBiaya from Pakai as A, PakaiDetail as B,Inventory as C, UOM as D where A.Status>=0 and A.ID=B.PakaiID  " +
                            "and B.ItemID=C.ID and B.RowStatus>-1 and B.UomID=D.ID " + tipePakai + LimitedView()+" order by A.Pakaidate desc,A.PakaiNo desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPakai = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPakai.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrPakai.Add(new Pakai());
            return arrPakai;
        }

        public ArrayList RetrieveOpenStatusReady(string strTipePakai)
        {
            string tipePakai = string.Empty;
            tipePakai = " and pakaino like '" + strTipePakai + "%'";
            
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select A.ID,A.PakaiNo,A.PakaiDate,A.DeptID,A.DepoID,A.PakaiTipe,A.CreatedBy,A.Status,B.ItemID,B.Quantity,A.ApproveDate, " +
                "B.Keterangan,B.UomID,C.ItemCode,C.ItemName,D.UOMCode,A.ItemTypeID,isnull(A.ready,0) as ready, CASE A.Status  WHEN 0 THEN ('Open')  " +
                "WHEN 1 THEN ('Head')  WHEN 2 THEN ('Manager') ELSE ('Gudang') END AS Apv,isnull(JenisBiaya,0) as JenisBiaya from Pakai as A, PakaiDetail as B,Inventory as C, UOM as D where A.Status>=0 and A.ID=B.PakaiID  " +
                "and B.ItemID=C.ID and B.RowStatus>-1 and A.ready=1 and B.UomID=D.ID " + tipePakai + " order by A.Pakaidate desc,A.PakaiNo desc";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPakai = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPakai.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrPakai.Add(new Pakai());

            return arrPakai;
        }

        public ArrayList RetrieveOpenStatusByApv(int UserID, int apv)
        {
            apv = apv - 1;
            string strSQL;
            if (apv != 1)
            {
                strSQL = "select A.ID,A.PakaiNo,A.PakaiDate,A.DeptID,A.DepoID,A.PakaiTipe,A.CreatedBy,A.Status," +
                "B.ItemID,B.Quantity,B.Keterangan,B.UomID, A.ApproveDate," +
                "case B.ItemtypeID when 1 then(select itemcode from Inventory where ID=B.ItemID ) " +
                "	when 2 then(select itemcode from asset where ID=B.ItemID )  " +
                "	when 3 then(select itemcode from Biaya where ID=B.ItemID ) end ItemCode, " +
                "case B.ItemtypeID when 1 then(select ItemName from Inventory where ID=B.ItemID )  " +
                "	when 2 then(select ItemName from asset where ID=B.ItemID )  " +
                "	when 3 then(select ItemName from Biaya where ID=B.ItemID ) end ItemName, " +
                "	D.UOMCode,A.ItemTypeID,isnull(A.ready,0) as ready,isnull(JenisBiaya,0) as JenisBiaya  " +
                "from Pakai as A, PakaiDetail as B,Dept C, UOM as D where A.Status=" + apv + " and A.ID=B.PakaiID  " +
                "and B.RowStatus>-1 and B.UomID=D.ID and C.ID=A.deptID order by A.PakaiNo ";
            }
            else
            {
                strSQL="select A.ID,A.PakaiNo,A.PakaiDate,A.DeptID,A.DepoID,A.PakaiTipe,A.CreatedBy,A.Status,"+
                "B.ItemID,B.Quantity,B.Keterangan,B.UomID, A.ApproveDate, " +
                "case B.ItemtypeID when 1 then(select itemcode from Inventory where ID=B.ItemID ) " +
                "	when 2 then(select itemcode from asset where ID=B.ItemID )  " +
                "	when 3 then(select itemcode from Biaya where ID=B.ItemID ) end ItemCode, " +
                "case B.ItemtypeID when 1 then(select ItemName from Inventory where ID=B.ItemID )  " +
                "	when 2 then(select ItemName from asset where ID=B.ItemID )  " +
                "	when 3 then(select ItemName from Biaya where ID=B.ItemID ) end ItemName, " +
                "	D.UOMCode,A.ItemTypeID,isnull(A.ready,0) as ready , CASE A.Status  WHEN 0 THEN ('Open')  " +
                "WHEN 1 THEN ('Head') WHEN 2 THEN ('Manager') ELSE ('Gudang') END AS Apv ,isnull(JenisBiaya,0) as JenisBiaya " +
                "from Pakai as A, PakaiDetail as B,Dept C,UOM as D where A.Status=" + apv + " and A.ID=B.PakaiID  " +
                "and B.RowStatus>-1 and B.UomID=D.ID and C.ID=A.deptID and C.HeadID =" + UserID + " order by A.PakaiNo ";
            }
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPakai.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrPakai.Add(new Pakai());

            return arrPakai;
        }

        public ArrayList AssetRetrieveOpenStatus(string strTipePakai)
        {
            string tipePakai = string.Empty;

            if (strTipePakai.Substring(1,1) == "C")
                tipePakai = "and A.PakaiTipe in (4,12)";
           
            string strSQL = "select A.ID,A.PakaiNo,A.PakaiDate,A.DeptID,A.DepoID,A.PakaiTipe,A.CreatedBy,A.Status,B.ItemID,B.Quantity,B.Keterangan,A.ApproveDate,"+
                            "B.UomID,C.ItemCode,C.ItemName,D.UOMCode,A.ItemTypeID ,isnull(A.ready,0) as ready, CASE A.Status  WHEN 0 THEN ('Open')  " +
                            "WHEN 1 THEN ('Head') WHEN 2 THEN ('Manager') ELSE ('Gudang') END AS Apv ,isnull(JenisBiaya,0) as JenisBiaya from Pakai as A, PakaiDetail as B," +
                            "Asset as C, UOM as D  where A.Status>=0 and A.ID=B.PakaiID and B.ItemID=C.ID and B.RowStatus>-1 and B.UomID=D.ID " + 
                            tipePakai + LimitedView()+ " order by A.Pakaidate desc, A.PakaiNo desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPakai.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrPakai.Add(new Pakai());

            return arrPakai;
        }        

        public ArrayList BiayaRetrieveOpenStatus(string strTipePakai)
        {
            string tipePakai = string.Empty;

            if (strTipePakai.Substring(1,1) == "B")
                tipePakai = "and A.PakaiTipe=5";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "select A.ID,A.PakaiNo,A.PakaiDate,A.DeptID,A.DepoID,A.PakaiTipe,A.CreatedBy,A.Status,B.ItemID,B.Quantity,B.Keterangan,A.ApproveDate,"+
                            "B.UomID,C.ItemCode,C.ItemName,D.UOMCode,isnull(A.ready,0) as ready, CASE A.Status  WHEN 0 THEN ('Open')  " +
                            "WHEN 1 THEN ('Head')  WHEN 2 THEN ('Manager') ELSE ('Gudang') END AS Apv,isnull(JenisBiaya,0) as JenisBiaya  from Pakai as A, " +
                            "PakaiDetail as B,Biaya as C, UOM as D  where A.Status>=0 and A.ID=B.PakaiID and B.ItemID=C.ID and B.RowStatus>-1 "+
                            "and B.UomID=D.ID " + tipePakai + LimitedView()+ " order by A.Pakaidate desc,A.PakaiNo";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPakai.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrPakai.Add(new Pakai());

            return arrPakai;
        }

        public ArrayList AllRetrieveOpenStatusByNo(string pakaiNo,int apv)
        {
            apv = apv - 1;
            string strSQL = "select A.ID,A.PakaiNo,A.PakaiDate,A.DeptID,A.DepoID,A.PakaiTipe,A.CreatedBy,A.Status,A.ItemTypeID,A.ApproveDate,"+
                            "B.ItemID,B.Quantity,B.Keterangan,B.UomID,C.DeptCode,C.DeptName,case A.ItemTypeID when 1 then "+
                            "(select Inventory.ItemCode from Inventory where Inventory.ID=B.ItemID) when 2 then "+
                            "(select Biaya.ItemCode from Biaya where Biaya.ID=B.ItemID) when 3 then (select Asset.ItemCode from Asset "+
                            "where Asset.ID=B.ItemID) else '' end ItemCode," +
                            "case A.ItemTypeID when 1 then (select Inventory.ItemName from Inventory where Inventory.ID=B.ItemID) when 2 "+
                            "then (select Biaya.ItemName from Biaya where Biaya.ID=B.ItemID) when 3 then (select Asset.ItemName from Asset "+
                            "where Asset.ID=B.ItemID) else '' end ItemName," +
                            "D.UOMCode,isnull(A.ready,0) as ready, CASE A.Status  WHEN 0 THEN ('Open')  " +
                            "WHEN 1 THEN ('Head')  WHEN 2 THEN ('Manager') ELSE ('Gudang') END AS Apv,isnull(JenisBiaya,0) as JenisBiaya from " +
                            "Pakai as A, PakaiDetail as B, Dept as C,UOM as D where /*A.Status=" + apv + " and*/ A.ID=B.PakaiID and A.DeptID=C.ID "+
                            "and B.RowStatus>-1 and B.UomID=D.ID and A.PakaiNo='" + pakaiNo + "' order by A.PakaiNo desc";

            //if (strTipePakai.Substring(1, 1) == "B")
            //    tipePakai = "and A.PakaiTipe=6";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    //arrPakai.Add(GenerateObjectList(sqlDataReader));
                    arrPakai.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrPakai.Add(new Pakai());

            return arrPakai;
        }

        public ArrayList RetrieveOpenStatusCriteria(string pakaiNo, string strTipePakai)
        {
            string tipePakai = string.Empty;

            if (strTipePakai.Substring(1, 1) == "P")
                tipePakai = "and A.PakaiTipe=2";
            else if (strTipePakai.Substring(1, 1) == "S")
                tipePakai = "and A.PakaiTipe=9";
            else if (strTipePakai.Substring(1, 1) == "M")
                tipePakai = "and A.PakaiTipe=3";
            else if (strTipePakai.Substring(1, 1) == "A")
                tipePakai = "and A.PakaiTipe=4";
            else if (strTipePakai.Substring(1, 1) == "O")
                tipePakai = "and A.PakaiTipe=7";
            else if (strTipePakai.Substring(1, 1) == "K")
                tipePakai = "and A.PakaiTipe=8";

            string strSQL = "select A.ID,A.PakaiNo,A.PakaiDate,A.DeptID,A.DepoID,A.PakaiTipe,A.CreatedBy,A.Status,B.ItemID,B.Quantity,A.ApproveDate,"+
                            "B.Keterangan,B.UomID,C.ItemCode,C.ItemName,D.UOMCode,isnull(A.ready,0) as ready, CASE A.Status  WHEN 0 THEN ('Open')  " +
                            "WHEN 1 THEN ('Head')  WHEN 2 THEN ('Manager') ELSE ('Gudang') END AS Apv ,isnull(JenisBiaya,0) as JenisBiaya from Pakai as A, PakaiDetail as B, " +
                            "Inventory as C, UOM as D where /*A.Status=0 and*/ A.ID=B.PakaiID and B.ItemID=C.ID and B.RowStatus>-1 and B.UomID=D.ID " +
                            tipePakai + LimitedView()+ " and A.PakaiNo like '%" + pakaiNo + "%'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPakai.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrPakai.Add(new Pakai());

            return arrPakai;
        }

        public ArrayList AssetRetrieveOpenStatusCriteria(string pakaiNo, string strTipePakai)
        {
            string tipePakai = string.Empty;

            if (strTipePakai.Substring(1,1) == "C")
                tipePakai = "and A.PakaiTipe=5";

            string strSQL = "select A.ID,A.PakaiNo,A.PakaiDate,A.DeptID,A.DepoID,A.PakaiTipe,A.CreatedBy,A.Status,B.ItemID,B.Quantity,B.Keterangan,A.ApproveDate,"+
                            "B.UomID,C.ItemCode,C.ItemName,D.UOMCode,isnull(A.ready,0) as ready, CASE A.Status  WHEN 0 THEN ('Open')  " +
                            "WHEN 1 THEN ('Head')  WHEN 2 THEN ('Manager') ELSE ('Gudang') END AS Apv,isnull(JenisBiaya,0) as JenisBiaya  from Pakai as A, PakaiDetail as B," +
                            "Asset as C, UOM as D where /*A.Status=0 and */ A.ID=B.PakaiID and B.ItemID=C.ID and B.RowStatus>-1 and B.UomID=D.ID " + 
                            tipePakai + LimitedView()+ " and A.PakaiNo like '%" + pakaiNo + "%'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.ReceiptNo,A.ReceiptDate,A.PONo,B.SPPNo,A.POID,A.SupplierId,A.Status,A.CreatedBy,B.ItemID,B.Quantity,C.ItemCode,C.ItemName from Pakai as A, ReceiptDetail as B,Inventory as C where A.Status=0 and A.ID=B.ReceiptID and B.ItemID=C.ID and A.ReceiptNo like '%" + pakaiNo + "%' " + tipePakai);
            strError = dataAccess.Error;
            arrPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPakai.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrPakai.Add(new Pakai());

            return arrPakai;
        }

        public ArrayList BiayaRetrieveOpenStatusCriteria(string pakaiNo, string strTipePakai)
        {
            string tipePakai = string.Empty;

            if (strTipePakai.Substring(1, 1) == "B")
                tipePakai = "and A.PakaiTipe=6";
            string strSQL = "select A.ID,A.PakaiNo,A.PakaiDate,A.DeptID,A.DepoID,A.PakaiTipe,A.CreatedBy,A.Status,B.ItemID,B.Quantity,B.Keterangan,A.ApproveDate,"+
                            "B.UomID,C.ItemCode,C.ItemName,D.UOMCode,isnull(A.ready,0) as ready,isnull(JenisBiaya,0) as JenisBiaya, CASE A.Status  "+
                            "WHEN 0 THEN ('Open') WHEN 1 THEN ('Head')  WHEN 2 THEN ('Manager') ELSE ('Gudang') END AS Apv  from Pakai as A, PakaiDetail as B,Biaya as C, " +
                            "UOM as D where /*A.Status=0 and*/ A.ID=B.PakaiID and B.ItemID=C.ID and B.RowStatus>-1 and B.UomID=D.ID " + 
                            tipePakai + LimitedView()+ " and A.PakaiNo like '%" + pakaiNo.Trim() + "%'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPakai.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrPakai.Add(new Pakai());

            return arrPakai;
        }

        public Pakai CekLastDate(string strPakaiCode, int DeptId)
        {
            string strSQL = "select isnull(max(PakaiDate),GETDATE()) as PakaiDate from Pakai where Status>-1 and left(PakaiNo,4)='" + strPakaiCode + "' and deptid=" + DeptId;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectLastEntry(sqlDataReader);
                }
            }

            return new Pakai();
        }
        public string CekLastTrans(string strPakaiCode, int DeptId)
        {
            string strSQL = "select max(PakaiDate) as PakaiDate from Pakai where Status>-1 and left(PakaiNo,4)='" + strPakaiCode + "' and deptid=" + DeptId;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return sqlDataReader["PakaiDate"].ToString();
                }
            }

            return string.Empty;
        }
        //public Pakai RetrieveByNo(string strPakaiNo)
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select ID,ReceiptNo,ReceiptDate,ReceiptType,ReceiptType,PONo,POID,SupplierId,Status,CreatedTime,CreatedBy,LastModifiedBy,LastModifiedTime from Pakai where Status>-1 and PakaiNo='" + strPakaiNo + "'");
        //    strError = dataAccess.Error;
        //    arrPakai = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            return GenerateObject(sqlDataReader);
        //        }
        //    }

        //    return new Pakai();
        //}

        public ArrayList RetrieveByPeriode(string drTgl, string sdTgl, int itemTypeID)
        {

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.PakaiNo,A.PakaiDate,A.DeptID,A.DepoID,A.Status,A.PakaiTipe,A.CreatedBy,B.DeptCode,B.DeptName,A.ItemTypeID,isnull(A.ready,0) as ready,isnull(JenisBiaya,0) as JenisBiaya  from Pakai as A, Dept as B  where A.Status>-1 and A.DeptID=B.ID and convert(varchar,A.PakaiDate,112) >= '" + drTgl + "' and convert(varchar,A.PakaiDate,112) <='" + sdTgl + "' and A.ItemTypeID = " + itemTypeID);
            strError = dataAccess.Error;
            arrPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPakai.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrPakai.Add(new Pakai());

            return arrPakai;
        }

        public ArrayList RetrieveBySamaTgl(int tgl, int bln, int thn, int itemTypeID, int pakaiTipe)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.PakaiNo,A.PakaiDate,A.DeptID,A.DepoID,A.Status,A.PakaiTipe,A.CreatedBy,B.DeptCode,B.DeptName,A.ItemTypeID,isnull(A.ready,0) as ready,isnull(JenisBiaya,0) as JenisBiaya from Pakai as A, Dept as B  where A.Status>-1 and A.DeptID=B.ID and day(A.PakaiDate)= " + tgl + " and month(A.PakaiDate) =" + bln + " and year(A.PakaiDate) =" + thn + " and A.ItemTypeID =" + itemTypeID + " and A.PakaiTipe=" + pakaiTipe);
            strError = dataAccess.Error;
            arrPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPakai.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrPakai.Add(new Pakai());

            return arrPakai;
        }
        public ArrayList RetrieveByKurangTgl(int tgl, int bln, int thn, int itemTypeID, int pakaiTipe)
        {

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.PakaiNo,A.PakaiDate,A.DeptID,A.DepoID,A.Status,A.PakaiTipe,A.CreatedBy,B.DeptCode,B.DeptName,A.ItemTypeID,isnull(A.ready,0) as ready,isnull(JenisBiaya,0) as JenisBiaya  from Pakai as A, Dept as B where A.Status>-1 and A.DeptID=B.ID and day(A.PakaiDate)< " + tgl + " and month(A.PakaiDate) =" + bln + " and year(A.PakaiDate) =" + thn + " and A.ItemTypeID =" + itemTypeID + " and A.PakaiTipe=" + pakaiTipe);
            strError = dataAccess.Error;
            arrPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPakai.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrPakai.Add(new Pakai());

            return arrPakai;
        }
        public ArrayList RetrieveByTgl(int tgl, int bln, int thn)
        {

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.PakaiNo,A.PakaiDate,A.DeptID,A.DepoID,A.Status,A.PakaiTipe,A.CreatedBy,B.DeptCode,B.DeptName,A.ItemTypeID,isnull(A.ready,0) as ready,isnull(JenisBiaya,0) as JenisBiaya  from Pakai as A, Dept as B where A.Status>-1 and A.DeptID=B.ID and day(A.PakaiDate)<= " + tgl + " and month(A.PakaiDate) =" + bln + " and year(A.PakaiDate) =" + thn);
            strError = dataAccess.Error;
            arrPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPakai.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrPakai.Add(new Pakai());

            return arrPakai;
        }

        public ArrayList RetrieveOpenStatusForAll(string thbl, int itemTypeID, int groupID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.PakaiNo,A.PakaiDate,A.DeptID,A.DepoID,A.Status,A.PakaiTipe,A.CreatedBy,B.DeptCode,B.DeptName,A.ItemTypeID from Pakai as A, Dept as B where A.Status>-1 and A.DeptID=B.ID and LEFT(convert(varchar,A.PakaiDate,112),6) = '" + thbl + "'");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select distinct A.ID,A.PakaiNo,A.PakaiDate,A.DeptID,A.DepoID,A.Status,A.PakaiTipe,A.CreatedBy,C.DeptCode,C.DeptName,B.ItemTypeID,isnull(A.ready,0) as ready,isnull(JenisBiaya,0) as JenisBiaya  " +
                "from Pakai as A, PakaiDetail as B,Dept as C where A.ID=B.PakaiID and  A.Status>-1 and B.RowStatus>-1 and B.ItemTypeID=" + itemTypeID + " and B.GroupID=" + groupID + " and " +
                "A.DeptID=C.ID and LEFT(convert(varchar,A.PakaiDate,112),6) = '" + thbl + "'");
            strError = dataAccess.Error;
            arrPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPakai.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrPakai.Add(new Pakai());

            return arrPakai;
        }
        
        public ArrayList RetrieveOpenStatusForLogistik(int HeadID, int apv,int GroupID)
        {
            try
            {
                Users user = (Users)HttpContext.Current.Session["Users"];
                string IDGudangs = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DeptIDGudang", "SPB");
                int IDGudang = 0;
                int.TryParse(IDGudangs, out IDGudang);
                string strGroup = string.Empty;
                if (GroupID == 3)
                    strGroup = " and pakaitipe=" + GroupID + " ";
                else
                    strGroup = " /*and pakaitipe<>3 */";

                string strSQL;
                ArrayList arrDept = new DeptFacade().GetDeptFromHead(user.ID);
                if (apv==2)
                     arrDept = new DeptFacade().GetDeptFromMgr(user.ID);
                else
                     arrDept = new DeptFacade().GetDeptFromHead(user.ID);
                string DepID = "";
                string head = "";
                foreach (Dept d in arrDept)
                {
                    DepID += d.DeptID + ",";
                }
                if (apv < 2)
                    apv = (user.DeptID == IDGudang && arrDept.Count == 0) ? 2 : 0;
                else
                    apv = 1;
                head = (user.DeptID == IDGudang && arrDept.Count == 0 && DepID.Length == 0) ? "A.status=" + apv :
                    (DepID.Length > 1) ?
                    "A.DeptID in(" + DepID.Substring(0, (DepID.Length - 1)) + ") and A.status=" + apv :
                    "A.DeptID in(" + DepID.Substring(0, (DepID.Length)) + ") and A.status=" + apv;
                strSQL = "select A.ID,A.PakaiNo,A.PakaiDate,A.DeptID,A.DepoID,A.Status,A.PakaiTipe,A.CreatedBy,'' DeptCode,''DeptName," +
                             "A.ItemTypeID,isnull(A.ready,0) as ready,isnull(JenisBiaya,0) as JenisBiaya  " +
                             "from Pakai as A  where " + head +
                             " order by A.ID,A.PakaiNo";
                DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
                strError = dataAccess.Error;
                arrPakai = new ArrayList();

                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        arrPakai.Add(GenerateObject(sqlDataReader));
                    }
                }
            }
            catch (Exception ex)
            {
                arrPakai = new ArrayList();
            }
            return arrPakai;

        }

        public ArrayList RetrieveOpenStatusForLogistikList(int UserID, int apv, int GroupID)
        {
            string strGroup = string.Empty;
            if (GroupID == 3)
                strGroup = " and pakaitipe=" + GroupID + " ";
            else
                strGroup = " and pakaitipe<>3 ";

            string strSQL;
            if (apv != 1)
            {
                //apv = apv - 1;
                strSQL = "select A.ID,A.PakaiNo,A.PakaiDate,A.DeptID,A.DepoID,A.PakaiTipe,A.CreatedBy,A.Status," +
                "B.ItemID,B.Quantity,B.Keterangan,B.UomID, " +
                "case B.ItemtypeID when 1 then(select itemcode from Inventory where ID=B.ItemID ) " +
                "	when 2 then(select itemcode from asset where ID=B.ItemID )  " +
                "	when 3 then(select itemcode from Biaya where ID=B.ItemID ) end ItemCode, " +
                "case B.ItemtypeID when 1 then(select ItemName from Inventory where ID=B.ItemID )  " +
                "	when 2 then(select ItemName from asset where ID=B.ItemID )  " +
                "	when 3 then(select ItemName from Biaya where ID=B.ItemID ) end ItemName, " +
                "	D.UOMCode,A.ItemTypeID,isnull(A.ready,0) as ready , CASE A.Status  WHEN 0 THEN ('Open')  " +
                "WHEN 1 THEN ('Head')  WHEN 2 THEN ('Manager') ELSE ('Gudang') END AS Apv,isnull(JenisBiaya,0) as JenisBiaya   " +
                "from Pakai as A, PakaiDetail as B,Dept C, UOM as D where A.Status=" + apv + strGroup+" and A.ID=B.PakaiID  " +
                "and B.RowStatus>-1 and B.UomID=D.ID and C.ID=A.deptID order by A.PakaiNo ";
            }
            else
            {
                apv = apv - 1;
                strSQL = "select A.ID,A.PakaiNo,A.PakaiDate,A.DeptID,A.DepoID,A.PakaiTipe,A.CreatedBy,A.Status," +
                "B.ItemID,B.Quantity,B.Keterangan,B.UomID, " +
                "case B.ItemtypeID when 1 then(select itemcode from Inventory where ID=B.ItemID ) " +
                "	when 2 then(select itemcode from asset where ID=B.ItemID )  " +
                "	when 3 then(select itemcode from Biaya where ID=B.ItemID ) end ItemCode, " +
                "case B.ItemtypeID when 1 then(select ItemName from Inventory where ID=B.ItemID )  " +
                "	when 2 then(select ItemName from asset where ID=B.ItemID )  " +
                "	when 3 then(select ItemName from Biaya where ID=B.ItemID ) end ItemName, " +
                "	D.UOMCode,A.ItemTypeID,isnull(A.ready,0) as ready, CASE A.Status  WHEN 0 THEN ('Open')  " +
                "WHEN 1 THEN ('Head')  WHEN 2 THEN ('Manager') ELSE ('Gudang') END AS Apv ,isnull(JenisBiaya,0) as JenisBiaya  " +
                "from Pakai as A, PakaiDetail as B,Dept C,UOM as D where A.Status=" + apv + strGroup + " and A.ID=B.PakaiID  " +
                "and B.RowStatus>-1 and B.UomID=D.ID and C.ID=A.deptID and C.HeadID =" + UserID + " order by A.PakaiNo ";
            }

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPakai = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPakai.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrPakai.Add(new Pakai());
            return arrPakai;
        }

        public Pakai RetrieveByNoWithStatus(string strPakaiNo, string strPakaiTipe)
        {
            string pakaiTipe = string.Empty;
            if (strPakaiTipe.Substring(1,1) == "P")
                pakaiTipe = "and A.PakaiTipe=1";
            else if (strPakaiTipe.Substring(1, 1) == "S")
                pakaiTipe = "and A.PakaiTipe in (8,9)";
            else if (strPakaiTipe.Substring(1, 1) == "M")
                pakaiTipe = "and A.PakaiTipe=2";
            else if (strPakaiTipe.Substring(1, 1) == "A")
                pakaiTipe = "and A.PakaiTipe=3";
            else if (strPakaiTipe.Substring(1, 1) == "O")
                pakaiTipe = "and A.PakaiTipe=6";
            else if (strPakaiTipe.Substring(1, 1) == "K")
                pakaiTipe = "and A.PakaiTipe in (7,10)";
            else if (strPakaiTipe.Substring(1, 1) == "C")
                pakaiTipe = "and A.PakaiTipe=4";
            else if (strPakaiTipe.Substring(1, 1) == "B")
                pakaiTipe = "and A.PakaiTipe=5";
            else if (strPakaiTipe.Substring(1, 1) == "E")
                pakaiTipe = "and A.PakaiTipe=12";
            //else if (strPakaiTipe.Substring(1, 1) == "R")
            //    pakaiTipe = "and A.PakaiTipe=10";

            string strSQL = "select A.ID,a.PakaiNo,A.PakaiDate,A.DeptID,A.DepoID,A.PakaiTipe,A.Status,A.AlasanCancel,A.CreatedBy,B.DeptCode,B.DeptName,"+
                            "A.ItemTypeID,isnull(A.ready,0) as ready, isnull(A.JenisBiaya,0) as JenisBiaya  from Pakai as A,Dept as B where A.Status>=0 "+
                            "and A.DeptID=B.ID and A.PakaiNo='" + strPakaiNo + "' " + pakaiTipe;
             DataAccess dataAccess = new DataAccess(Global.ConnectionString());
           SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new Pakai();
        }

        public ArrayList RetrieveBySamaTgl2(int tgl, int bln, int thn, int itemTypeID, int groupID)
        {

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "select distinct A.ID,A.PakaiNo,A.PakaiDate,A.DeptID,A.DepoID,A.Status,A.PakaiTipe,A.CreatedBy,C.DeptCode,C.DeptName,A.ItemTypeID,isnull(A.ready,0) as ready,isnull(JenisBiaya,0) as JenisBiaya  " +
                " from Pakai as A, PakaiDetail as B, Dept as C where A.ID=B.PakaiID and  A.Status=3 and A.DeptID=C.ID and B.RowStatus>-1 and " +
                "day(A.PakaiDate)= " + tgl + " and month(A.PakaiDate) =" + bln + " and year(A.PakaiDate) =" + thn + " and B.ItemTypeID =" + itemTypeID + " and B.GroupID=" + groupID;
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPakai.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrPakai.Add(new Pakai());

            return arrPakai;
        }

        public ArrayList RetrieveByKurangTgl2(int tgl, int bln, int thn, int itemTypeID, int groupID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select distinct A.ID,A.PakaiNo,A.PakaiDate,A.DeptID,A.DepoID,A.Status,A.PakaiTipe,A.CreatedBy,C.DeptCode,C.DeptName,A.ItemTypeID,isnull(A.ready,0) as ready,isnull(JenisBiaya,0) as JenisBiaya  " +
                "from Pakai as A, PakaiDetail as B, Dept as C where A.ID=B.PakaiID and  A.Status>-1 and A.DeptID=C.ID and B.RowStatus>-1 and " +
                "day(A.PakaiDate)< " + tgl + " and month(A.PakaiDate) =" + bln + " and year(A.PakaiDate) =" + thn + " and B.ItemTypeID =" + itemTypeID + " and B.GroupID=" + groupID);
            strError = dataAccess.Error;
            arrPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPakai.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrPakai.Add(new Pakai());

            return arrPakai;
        }

        public int RetrieveNew(string  tanggal)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select * from pakai where createdtime>='" + tanggal + "'";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPakai = new ArrayList();
            int jumlah=0;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    jumlah=1;
                }
            }
            else
                jumlah = 0;

            return jumlah;
        }

        public Pakai GenerateObject(SqlDataReader sqlDataReader)
        {
            objPakai = new Pakai();
            objPakai.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objPakai.PakaiNo = sqlDataReader["PakaiNo"].ToString();
            objPakai.PakaiDate = Convert.ToDateTime(sqlDataReader["PakaiDate"]);
            objPakai.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objPakai.DepoID = Convert.ToInt32(sqlDataReader["DepoID"]);
            objPakai.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objPakai.PakaiTipe = Convert.ToInt32(sqlDataReader["PakaiTipe"]);
            objPakai.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objPakai.DeptCode = sqlDataReader["DeptCode"].ToString();
            objPakai.DeptName = sqlDataReader["DeptName"].ToString();
            objPakai.JenisBiaya =Convert.ToInt32(sqlDataReader["JenisBiaya"].ToString());
            //
            if (string.IsNullOrEmpty(sqlDataReader["ItemTypeID"].ToString()))
                objPakai.ItemTypeID = 0;
            else
                objPakai.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"]);
            objPakai.Ready  = Convert.ToInt32(sqlDataReader["ready"]);
            return objPakai;
        }

        public Pakai GenerateObjectLastEntry(SqlDataReader sqlDataReader)
        {
            objPakai = new Pakai();
            objPakai.PakaiDate = Convert.ToDateTime(sqlDataReader["PakaiDate"]);
            return objPakai;
        }
        public Pakai GenerateObjectList(SqlDataReader sqlDataReader)
        {
            objPakai = new Pakai();
            objPakai.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objPakai.PakaiNo = sqlDataReader["PakaiNo"].ToString();
            objPakai.PakaiDate = Convert.ToDateTime(sqlDataReader["PakaiDate"]);
            objPakai.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objPakai.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objPakai.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objPakai.DepoID = Convert.ToInt32(sqlDataReader["DepoID"]);
            objPakai.PakaiTipe = Convert.ToInt32(sqlDataReader["PakaiTipe"]);
            objPakai.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objPakai.Quantity = Convert.ToDecimal(sqlDataReader["Quantity"]);
            objPakai.Keterangan = sqlDataReader["Keterangan"].ToString();
            objPakai.UomID = Convert.ToInt32(sqlDataReader["UomID"]);
            objPakai.UomCode = sqlDataReader["UomCode"].ToString();
            objPakai.ItemCode = sqlDataReader["ItemCode"].ToString();
            objPakai.ItemName = sqlDataReader["ItemName"].ToString();
            objPakai.Ready = Convert.ToInt32(sqlDataReader["ready"]);
            objPakai.Apv  = (sqlDataReader["apv"]).ToString();
            return objPakai;
        }
        /**
         * added on 24-04-2014
         * populate year dropdown in report
         */
        public ArrayList GetYearTrans()
        {
            string strSQL = "select * from (select Year(PakaiDate) as PakaiDate from Pakai group by year(pakaidate) ) as w order by w.PakaiDate";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPakai.Add(GenerateObjectYear(sqlDataReader));
                }
            }
            else
                arrPakai.Add(new Pakai());

            return arrPakai;
        }
        public Pakai GenerateObjectYear(SqlDataReader sqlDataReader)
        {
            objPakai = new Pakai();
            objPakai.Tahun = Convert.ToInt16(sqlDataReader["PakaiDate"]);
            return objPakai;
        }
        private int userDeptID(int HeadID)
        {
            int result = 0;
            string strSQL = "Select DeptID from Users where ID=" + HeadID + " and Apv >0";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPakai = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    result = Convert.ToInt32(sqlDataReader["DeptID"].ToString());
                }
            }
            return result;
        }
        /**
         * Evaluasi Budget report
         * by Razib
         * on 25-08-2016
         */
        public string Where { get; set; }
        public ArrayList RetrieveEvBudget(string Bulan, string Tahun, string DeptID)
        {
            string where = string.Empty;
            where = (DeptID == "0") ? "" : " and D.DeptID=" + DeptID;
            string strSQL = "SELECT * FROM (select ItemID,Dept,ItemCode,ItemName,Satuan,MaxQty as BudgetQty,BBulan,BTahun,Kategori,SUM(Quantity)Pemakaian,"+
                            "(MaxQty-(SUM(Quantity)))Selisih,''Keterangan from ( " +
                            "select D.ItemID, B.DeptName as Dept,A.ItemCode,A.ItemName,C.UOMCode as Satuan,D.MaxQty," +
                            "Case when D.RuleCalc<=1 then D.MaxQty else D.MaxQty/RuleCalc end as BBulan," +
                            "Case when D.RuleCalc>1 then D.MaxQty*D.RuleCalc else D.MaxQty*12 end as BTahun," +
                            "Case when D.RuleCalc <=1 then " +
                            "case when D.RuleCalc=6 Then 'Semester' " +
                            "else  'Bulanan' end " +
                            "else 'Tahunan' end as Kategori,Pd.Quantity " +
                            "FROM  BudgetSP as D " +
                            "LEFT JOIN PakaiDetail as Pd on Pd.ItemID=D.ItemID " +
                            "LEFT JOIN Pakai as P ON P.ID=Pd.PakaiID and P.DeptID=D.DeptID " +
                            "LEFT JOIN Inventory as A ON A.ID=D.ItemID " +
                            "LEFT JOIN Dept as B ON B.ID=D.DeptID " +
                            "LEFT JOIN UOM as C ON C.ID=A.UOMID " +
                            "WHERE Month(P.PakaiDate)=" + Bulan + "  and Year(PakaiDate)=" + Tahun + "  and D.Tahun=" + Tahun + " " +
                            " and P.Status>1 and Pd.RowStatus>-1 and D.RowStatus>-1 " + where +
                            ") as x  "+this.Criteria+" group by ItemCode,ItemName,Satuan,ItemID,Dept,MaxQty,BBulan,BTahun,Kategori " +
                            "" + Where + ") as x order by Dept,Pemakaian Desc,ItemName  ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPakai.Add(GenerateObjectList2(sqlDataReader));
                }
            }

            return arrPakai;

        }
        public void GetTahun(DropDownList ddl)
        {
            arrData = new ArrayList();
            string strSQL = "select * from (select distinct YEAR(CreatedTime) Tahun from Pakai where YEAR(CreatedTime)>= year(getdate())-3 )A order by tahun";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            ddl.Items.Clear();
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddl.Items.Add(new ListItem(sdr["Tahun"].ToString(), sdr["Tahun"].ToString()));
                }
            }
        }
        public Evaluasi GenerateObjectList2(SqlDataReader sqlDataReader)
        {
            eBudget = new Evaluasi();
            eBudget.ItemID = int.Parse(sqlDataReader["ItemID"].ToString());
            eBudget.DeptName = sqlDataReader["Dept"].ToString();
            eBudget.ItemCode = sqlDataReader["ItemCode"].ToString();
            eBudget.ItemName = sqlDataReader["ItemName"].ToString();
            eBudget.MaxQty = Convert.ToDecimal(sqlDataReader["BudgetQty"].ToString());
            eBudget.BBulan = Convert.ToDecimal(sqlDataReader["BBulan"].ToString());
            eBudget.BTahun = Convert.ToDecimal(sqlDataReader["BTahun"].ToString());
            eBudget.Kategori = sqlDataReader["Kategori"].ToString();
            eBudget.Pemakaian = Convert.ToDecimal(sqlDataReader["Pemakaian"].ToString());
            eBudget.Selisih = Convert.ToDecimal(sqlDataReader["Selisih"].ToString());
            eBudget.Keterangan = sqlDataReader["Keterangan"].ToString();
            eBudget.Satuan = sqlDataReader["Satuan"].ToString();
            return eBudget;

        }

        public ArrayList RetrieveOpenStatus(string TipePakai, string DeptID)
        {
            string Aktif = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("CheckSPBPending", "SPB");
            string interval = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("LamaPending", "SPB");
            arrData = new ArrayList();
            if (Aktif == "1")
            {
                string strQuery = "SELECT PakaiNo,Convert(Char,PakaiDate,112)Tanggal,Status FROM Pakai where Status between 0 and 2 " +
                                "AND DATEDIFF(DAY,PakaiDate,GETDATE())>" + interval + " AND DeptID=" + DeptID;
                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(strQuery);
                if (da.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(GeneratObjectX(sdr));
                    }
                }
            }
            return arrData;
        }

        private object GeneratObjectX(SqlDataReader sdr)
        {
            objPakai = new Pakai();
            objPakai.PakaiNo = sdr["PakaiNo"].ToString();
            objPakai.Tanggal = sdr["Tanggal"].ToString();
            objPakai.Status = int.Parse(sdr["Status"].ToString());
            return objPakai;
        }
        private int GetPlanningID(DateTime PakaiDate)
        {
            int result = 0;
            Planning pl = new Planning();
            PlanningFacade pf = new PlanningFacade();
            string periode = " AND Bulan=" + PakaiDate.Month.ToString() + " AND Tahun=" + PakaiDate.Year.ToString();
            ArrayList p = pf.Retrieve(periode, true);
            foreach (Planning pp in p)
            {
                pl = pp;
            }
            result = pl.ID;
            return result;
        }
        public int GetPlanningIDUsed(int PlanningID)
        {
            int result=0;
            string strSQL = "SELECT COUNT(ID) ID FROM Pakai Where Status>-1 AND PlanningID=" + PlanningID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = int.Parse(sdr["ID"].ToString());
                }
            }
            return result;
        }
        public ArrayList RetrieveEvBudget1(string Dept)
        {
            arrData = new ArrayList();
            string where = (Dept == "0") ? "" : " where DeptID=" + Dept;
            string strSql = "SELECT s.DeptID,d.DeptName,d.DeptCode FROM BudgetSP s LEFT JOIN Dept d ON d.ID=s.DeptID " +
                            where + " GROUP By s.DeptID,d.DeptName,d.DeptCode ORDER By d.DeptName";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSql);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenObject(sdr));
                }
            }
            return arrData;
        }
        private Budget GenObject(SqlDataReader sdr)
        {
            Budget b = new Budget();
            b.DeptID = int.Parse(sdr["DeptID"].ToString());
            b.DeptName = sdr["DeptName"].ToString();
            b.DeptCode = sdr["DeptCode"].ToString();
            return b;
        }
        public string Criteria { get; set; }
        public ArrayList RetrieveEvBudget1(string Bulan, string Tahun, string DeptID)
        {
            //string test = string.Empty;
            //test = (DeptID == "0") ? "" : " and DeptID=" + DeptID;
            string strSQL = " WITH MasterBudget AS ( " +
                            " SELECT sp.ItemID,(SELECT dbo.ItemCodeInv(itemID,1))ItemCode," +
                            "(SELECT dbo.ItemNameInv(ItemID,1))ItemName,(Select dbo.SatuanInv(ItemID,ItemTypeID))Satuan,MaxQty," +
                            " Case when RuleCalc <=1 then " +
                            " case when RuleCalc=6 Then 'Semester' " +
                            " else  'Bulanan' end " +
                            " else 'Tahunan' end as RuleCalc " +
                            " FROM BudgetSP sp " +
                            " where sp.DeptID=" + DeptID + " AND Tahun=" + Tahun + "  AND sp.RowStatus>-1 AND itemID is Not Null AND MaxQty>0 " +
                            " ), BudgetPakai AS ( " +
                                " SELECT pd.ItemID,pd.ItemTypeID,SUM(pd.Quantity)Quantity,MAX(pd.BudgetQty)BudgetQty,AVG(pd.AvgPrice)AvgPrice " +
                                " FROM Pakai p " +
                                " LEFT JOIN PakaiDetail pd ON pd.PakaiID=p.ID " +
                                " WHERE Status>1 AND pd.RowStatus>-1  AND Month(PakaiDate)=" + Bulan + " AND YEAR(PakaiDate)=" + Tahun + " " +
                                " AND pd.GroupID in(8,9,2) AND DeptID=" + DeptID + " " +
                                " GROUP By ItemID,pd.ItemTypeID " +
                            " )" +
                               " SELECT m.*,ISNULL(bp.Quantity,0)Quantity,ISNULL(bp.BudgetQty,0)Budget,ISNULL((bp.BudgetQty - bp.Quantity),0)Selisih " +
                               " FROM MasterBudget m " +
                               " LEFT JOIN BudgetPakai bp ON bp.ItemID=m.ItemID " +
                               this.Criteria +
                               " ORDER BY m.ItemName,m.ItemCode ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPakai.Add(GenerateObjectListRzb(sqlDataReader));
                }
            }

            return arrPakai;

        }
        /**
         * Evaluasi Budget report
         * by Razib
         * on 25-08-2016
         */

        public ArrayList RetrievePemantauanForkLift(string Bulan, string Tahun, string ForkLift)
        {
            string strSQL;
            string where = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/GroupArmadaOnly.ini")).Read("F" + 
                ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID.ToString(), "Forklift");//
            where = where.Replace(",", "','");
            strSQL = "SELECT convert (varchar, p.PakaiDate,103) as Tanggal ,p.PakaiNo,pd.ItemID,(Select dbo.ItemCodeInv(pd.ItemID,pd.ItemTypeID))ItemCode," +
                            "(Select dbo.ItemNameInv(pd.ItemID,pd.ItemTypeID))ItemName,st.UOMCode as Unit,Quantity,ISNULL(AvgPrice,0)AvgPrice,ISNULL(AvgPrice,0) * Quantity Jumlah, ";
            strSQL += "REPLACE(LTRIM(RIGHT(pd.Keterangan,LEN(pd.Keterangan) - CHARINDEX('[',pd.Keterangan) )),']','') as Keterangan";
            strSQL += " FROM PakaiDetail pd " +
                            "LEFT JOIN Pakai p ON p.ID=pd.PakaiID " +
                            "LEFT JOIN UOM st on pd.UomID=st.ID " +
                            "WHERE (pd.Keterangan like '%" + ForkLift + "%' or pd.nopol like '%" + ForkLift + "%') and MONTH(p.Pakaidate)=" + Bulan + " and Year(p.Pakaidate)=" + Tahun + " AND p.Status>-1 AND pd.RowStatus>-1 " +
                            "order by p.ID Desc ";


            if (ForkLift == "0")
            {
                string newSQL = "SELECT * FROM (" + strSQL.Replace("WHERE", "WHERE /*").Replace("%') and", "%') and */").Replace("order by p.ID Desc", " ");
                newSQL += " ) AS x WHERE x.Keterangan in('" + where.Substring(0, (where.Length - 1)) + "') order by Tanggal";
                strSQL = newSQL;
            }
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPakai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPakai.Add(GenerateObjectListFK(sqlDataReader));
                }
            }

            return arrPakai;

        }
        public Rzb GenerateObjectListRzb(SqlDataReader sqlDataReader)
        {
            Rzb eRzb = new Rzb();
            eRzb.ItemID = int.Parse(sqlDataReader["ItemID"].ToString());
            eRzb.ItemCode = sqlDataReader["ItemCode"].ToString();
            eRzb.ItemName = sqlDataReader["ItemName"].ToString();
            eRzb.Satuan = sqlDataReader["Satuan"].ToString();
            eRzb.MaxQty = Convert.ToDecimal(sqlDataReader["Budget"].ToString());
            eRzb.Quantity = Convert.ToDecimal(sqlDataReader["Quantity"].ToString());
            //eRzb.Budget = Convert.ToDecimal(sqlDataReader["Budget"].ToString());
            eRzb.RuleCalc = sqlDataReader["RuleCalc"].ToString();
            eRzb.Selisih = Convert.ToDecimal(sqlDataReader["Selisih"].ToString());
            return eRzb;
        }
        public ForkLift GenerateObjectListFK(SqlDataReader sqlDataReader)
        {
            ForkLift FK = new ForkLift();
            FK.Tanggal = sqlDataReader["Tanggal"].ToString();
            FK.PakaiNo = sqlDataReader["PakaiNo"].ToString();
            FK.ItemCode = sqlDataReader["ItemCode"].ToString();
            FK.ItemName = sqlDataReader["ItemName"].ToString();
            FK.Unit = sqlDataReader["Unit"].ToString();
            FK.Quantity = Convert.ToDecimal(sqlDataReader["Quantity"].ToString());
            FK.AvgPrice = Convert.ToDecimal(sqlDataReader["AvgPrice"].ToString());
            FK.Jumlah = Convert.ToDecimal(sqlDataReader["Jumlah"].ToString());
            FK.Keterangan = sqlDataReader["Keterangan"].ToString();
            return FK;
        }

         public ArrayList RetrieveOpenStatusx(int bulan, int tahun, int status,int userDeptID)
        {
            string Query = string.Empty; string Query1 = string.Empty;
            if (userDeptID == 10)
            {
                Query = "";
            }
            else
            {
                Query = "" + LimitedView() + "";
            }

            if (status == 4)
            {
                Query1 = "";
            }
            else { Query1 = " and A.Status='" + status + "' "; }

            string tipePakai = string.Empty;
            //tipePakai = " and pakaino like '" + strTipePakai + "%'";
            string strSQL = "select A.ID,A.PakaiNo,A.PakaiDate,A.DeptID,A.DepoID,A.PakaiTipe,A.CreatedBy,A.Status,B.ItemID,B.Quantity,A.ApproveDate, " +
                            "B.Keterangan,B.UomID,C.ItemCode,C.ItemName,D.UOMCode,A.ItemTypeID,isnull(A.ready,0) as ready, CASE A.Status  WHEN 0 THEN ('Open')  " +
                            "WHEN 1 THEN ('Head')   WHEN 2 THEN ('Manager') when 3 then ('Gudang') END AS Apv,isnull(JenisBiaya,0) as JenisBiaya from Pakai as A, PakaiDetail as B,Inventory as C, UOM as D where A.Status>=0 and A.ID=B.PakaiID  " +
                            "and B.ItemID=C.ID and B.RowStatus>-1 and B.UomID=D.ID " + Query + " and Month(A.PakaiDate)='" + bulan + "' "+
                            "and YEAR(A.Pakaidate)='" + tahun + "'  " + Query1 + "order by A.Pakaidate desc,A.PakaiNo desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrPakai = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPakai.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrPakai.Add(new Pakai());
            return arrPakai;
        }

         public int Cekstatus(string pakaino)
         {
             int result = 0;
             string strSQL = "Select status from pakai where pakaino='" + pakaino + "' and PakaiTipe in(8,9)";
             DataAccess dataAccess = new DataAccess(Global.ConnectionString());
             SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
             strError = dataAccess.Error;
             arrPakai = new ArrayList();
             if (sqlDataReader.HasRows)
             {
                 while (sqlDataReader.Read())
                 {
                     result = Convert.ToInt32(sqlDataReader["Status"].ToString());
                 }
             }
             return result;
         }

         public ArrayList RetrieveTebal()
         {
             arrData = new ArrayList();
             string strSql = " select Keterangan,ID from BM_PFloculantInfo where Rowstatus>-1 order by ID ";
             DataAccess da = new DataAccess(Global.ConnectionString());
             SqlDataReader sdr = da.RetrieveDataByString(strSql);
             if (da.Error == string.Empty && sdr.HasRows)
             {
                 while (sdr.Read())
                 {
                     arrData.Add(GenObjectTebal(sdr));
                 }
             }
             return arrData;
         }

         public Pakai GenObjectTebal(SqlDataReader sqlDataReader)
         {
             Pakai pk = new Pakai();
             pk.Keterangan = sqlDataReader["Keterangan"].ToString();
             return pk;
         }
        
    }
}
