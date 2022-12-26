using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Web;
using Domain;
using DataAccessLayer;

namespace BusinessFacade
{
    public class SPPFacade : AbstractTransactionFacade
    {
        private SPP objSPP = new SPP();
        private ArrayList arrSPP;
        private List<SqlParameter> sqlListParam;

        public SPPFacade(object objDomain)
            : base(objDomain)
        {
            objSPP = (SPP)objDomain;
        }

        public SPPFacade()
        {

        }

        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@NoSPP", objSPP.NoSPP));
                sqlListParam.Add(new SqlParameter("@Minta", objSPP.Minta));
                sqlListParam.Add(new SqlParameter("@PermintaanType", objSPP.PermintaanType));
                sqlListParam.Add(new SqlParameter("@SatuanID", objSPP.SatuanID));
                sqlListParam.Add(new SqlParameter("@GroupID", objSPP.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objSPP.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@Jumlah", objSPP.Jumlah));
                sqlListParam.Add(new SqlParameter("@JumlahSisa", objSPP.JumlahSisa));
                sqlListParam.Add(new SqlParameter("@Keterangan", objSPP.Keterangan));
                sqlListParam.Add(new SqlParameter("@Sudah", objSPP.Sudah));
                sqlListParam.Add(new SqlParameter("@FCetak", objSPP.FCetak));
                sqlListParam.Add(new SqlParameter("@UserID", objSPP.UserID));
                sqlListParam.Add(new SqlParameter("@Pending", objSPP.Pending));
                sqlListParam.Add(new SqlParameter("@Inden", objSPP.Inden));
                sqlListParam.Add(new SqlParameter("@AlasanBatal", objSPP.AlasanBatal));
                sqlListParam.Add(new SqlParameter("@AlasanCLS", objSPP.AlasanCLS));
                sqlListParam.Add(new SqlParameter("@Status", objSPP.Status));
                sqlListParam.Add(new SqlParameter("@Approval", objSPP.Approval));
                sqlListParam.Add(new SqlParameter("@DepoID", objSPP.DepoID));
                sqlListParam.Add(new SqlParameter("@ApproveDate1", objSPP.ApproveDate1));
                sqlListParam.Add(new SqlParameter("@ApproveDate2", objSPP.ApproveDate2));
                sqlListParam.Add(new SqlParameter("@ApproveDate3", objSPP.ApproveDate3));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objSPP.CreatedBy));
                sqlListParam.Add(new SqlParameter("@HeadID", objSPP.HeadID));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertSPP");

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
                sqlListParam.Add(new SqlParameter("@ID", objSPP.ID));
                sqlListParam.Add(new SqlParameter("@NoSPP", objSPP.NoSPP));
                sqlListParam.Add(new SqlParameter("@Minta", objSPP.Minta));
                sqlListParam.Add(new SqlParameter("@PermintaanType", objSPP.PermintaanType));
                sqlListParam.Add(new SqlParameter("@SatuanID", objSPP.SatuanID));
                sqlListParam.Add(new SqlParameter("@GroupID", objSPP.GroupID));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objSPP.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@Jumlah", objSPP.Jumlah));
                sqlListParam.Add(new SqlParameter("@JumlahSisa", objSPP.JumlahSisa));
                sqlListParam.Add(new SqlParameter("@Keterangan", objSPP.Keterangan));
                sqlListParam.Add(new SqlParameter("@Sudah", objSPP.Sudah));
                sqlListParam.Add(new SqlParameter("@FCetak", objSPP.FCetak));
                sqlListParam.Add(new SqlParameter("@UserID", objSPP.UserID));
                sqlListParam.Add(new SqlParameter("@Pending", objSPP.Pending));
                sqlListParam.Add(new SqlParameter("@Inden", objSPP.Inden));
                sqlListParam.Add(new SqlParameter("@AlasanBatal", objSPP.AlasanBatal));
                sqlListParam.Add(new SqlParameter("@AlasanCLS", objSPP.AlasanCLS));
                sqlListParam.Add(new SqlParameter("@Status", objSPP.Status));
                sqlListParam.Add(new SqlParameter("@Approval", objSPP.Approval));
                sqlListParam.Add(new SqlParameter("@DepoID", objSPP.DepoID));
                sqlListParam.Add(new SqlParameter("@ApproveDate1", objSPP.ApproveDate1));
                sqlListParam.Add(new SqlParameter("@ApproveDate2", objSPP.ApproveDate2));
                sqlListParam.Add(new SqlParameter("@ApproveDate3", objSPP.ApproveDate3));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objSPP.CreatedBy));
                sqlListParam.Add(new SqlParameter("@HeadID", objSPP.HeadID));                

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateSPP");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateStatusSPP(TransactionManager transManager)
        {

            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objSPP.ID));
                sqlListParam.Add(new SqlParameter("@Status", objSPP.Status));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateStatusSPP");

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
                sqlListParam.Add(new SqlParameter("@ID", objSPP.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objSPP.LastModifiedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteSPP");

                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        //public override int Approve(TransactionManager transManager)
        //{
        //    try
        //    {

        //        sqlListParam = new List<SqlParameter>();
        //        sqlListParam.Add(new SqlParameter("@ID", objSPP.ID));
        //        sqlListParam.Add(new SqlParameter("@Aprv", objSPP.Aprv));
        //        sqlListParam.Add(new SqlParameter("@LastModifiedBy", objSPP.LastModifiedBy));

        //        int intResult = transManager.DoTransaction(sqlListParam, "spApproveSPP");

        //        strError = transManager.Error;

        //        return intResult;

        //    }
        //    catch (Exception ex)
        //    {
        //        strError = ex.Message;
        //        return -1;
        //    }
        //}

        //public int UpdateApproveDate(TransactionManager transManager, int flag)
        //{

        //try
        //{
        //    sqlListParam = new List<SqlParameter>();
        //    sqlListParam.Add(new SqlParameter("@ID", objSPP.ID));
        //    sqlListParam.Add(new SqlParameter("@Flag", flag));

        //    int intResult = transManager.DoTransaction(sqlListParam, "spUpdateApproveDate");

        //    strError = transManager.Error;

        //    return intResult;

        //}
        //catch (Exception ex)
        //{
        //    strError = ex.Message;
        //    return -1;
        //}
        //}
        public string Criteria { get; set; }
        public int GetHeadIDForMgrID(int mgrid)
        {
            int headID = 0;
            string strSQL = "select top 1 headID from listuserhead where RowStatus >-1 AND mgrid=" + mgrid + " order by HeadID";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSPP = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    headID = Convert.ToInt32(sqlDataReader["headID"]);
                }
            }
            return headID;
        }
        public bool isAuthHead(int UserID)
        {
            string strSQL = "select * from listuserhead where RowStatus >-1 AND HeadID=" + UserID + " order by HeadID";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return sqlDataReader.HasRows;
        }
        public bool isAuthManager(int UserID)
        {
            string strSQL = "select * from listuserhead where RowStatus >-1 AND mgrid=" + UserID + " order by HeadID";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return sqlDataReader.HasRows;
        }
        public bool isAuthPM(int UserID)
        {
            string strSQL = "select * from listuserhead where RowStatus >-1 AND ManagerID=" + UserID + " order by HeadID";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            return sqlDataReader.HasRows;
        }
        public string Where { get; set; }
        public string Wherex { get; set; }
        public string isAuthHead(int userID, bool getUserID)
        {
            string result = "";
            string strSQL = "select HeadID from listuserhead where RowStatus >-1 "+this.Wherex+" order by userID";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSPP = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    result += Convert.ToInt32(sqlDataReader["HeadID"]) + ",";
                }
            }
            int poskoma = Array.IndexOf(result.Split(','), ',');
            return result.TrimEnd(',');
        }
        public string isAuthHead(int userID, bool getUserID,bool HeadID)
        {
            string result = "";
            string strSQL = (getUserID == true) ? "select UserID from listuserhead where RowStatus >-1 " :
                                           "select HeadID as UserID from listuserhead where RowStatus >-1";
            strSQL += this.Where + " order by userID";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSPP = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    result += Convert.ToInt32(sqlDataReader["UserID"]) + ",";
                }
            }
            int poskoma = Array.IndexOf(result.Split(','), ',');
            return result.TrimEnd(',');
        }
        
        public override ArrayList Retrieve()
        {
            string strSQL = "select top 50 A.ID,A.NoSPP,A.HeadID,A.Minta,A.ItemID,B.ItemCode,B.ItemName,SatuanID,C.UOMDesc,A.GroupID," +
                          "D.GroupDescription,D.GroupCode,A.ItemTypeID,E.TypeDescription,A.Jumlah,A.JumlahSisa,A.Keterangan,A.Sudah," +
                          "A.FCetak,A.UserID,A.Pending,A.Inden,A.AlasanBatal,A.AlasanCLS,A.Status,A.Approval,A.CreatedBy," +
                          "A.CreatedTime as Tanggal,A.LastModifiedBy,A.LastModifiedTime,A.Approvedate1,A.Approvedate2,A.Approvedate3 " +
                          "from SPP as A,Inventory as B,UOM as C," +
                          "GroupsPurchn as D,ItemTypePurchn as E where A.ItemID = B.ID and A.SatuanID = C.ID and A.GroupID = D.ID " +
                          "and A.ItemTypeID = E.ID and A.Status = 0 " + this.Criteria;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSPP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSPP.Add(GenerateObject(sqlDataReader));
                }
            }

            return arrSPP;
        }

        public ArrayList RetrieveByDepo(int DepoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 50 A.ID,A.NoSPP,A.HeadID,A.Minta,A.PermintaanType,B.ItemCode,B.ItemName,SatuanID,C.UOMDesc,A.GroupID,D.GroupDescription,D.GroupCode,A.ItemTypeID,E.TypeDescription,A.Jumlah,A.JumlahSisa,A.Keterangan,A.Sudah,A.FCetak,A.UserID,A.Pending,A.Inden,A.AlasanBatal,A.AlasanCLS,A.Status,A.Approval,A.CreatedBy,A.CreatedTime as Tanggal,A.LastModifiedBy,A.LastModifiedTime,A.Approvedate1,A.Approvedate2,A.Approvedate3 from SPP as A,Inventory as B,UOM as C,GroupsPurchn as D,ItemTypePurchn as E where A.ItemID = B.ID and A.SatuanID = C.ID and A.GroupID = D.ID and A.ItemTypeID = E.ID and A.DepoID = " + DepoID);
            strError = dataAccess.Error;
            arrSPP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSPP.Add(GenerateObject(sqlDataReader));
                }
            }

            return arrSPP;
        }

        public ArrayList RetrieveStatusOpenDepo(int DepoID)
        {
            string strSQL = "select top 50 A.ID,A.NoSPP,A.HeadID,A.Minta,A.PermintaanType,B.ItemCode,B.ItemName,SatuanID," +
                          "C.UOMDesc,A.GroupID,D.GroupDescription,D.GroupCode,A.ItemTypeID,E.TypeDescription,A.Jumlah," +
                          "A.JumlahSisa,A.Keterangan,A.Sudah,A.FCetak,A.UserID,A.Pending,A.Inden,A.AlasanBatal,A.AlasanCLS," +
                          "A.Status,A.Approval,A.CreatedBy,A.CreatedTime as Tanggal,A.LastModifiedBy,A.LastModifiedTime," +
                          "A.Approvedate1,A.Approvedate2,A.Approvedate3 " +
                          "from SPP as A,Inventory as B,UOM as C,GroupsPurchn as D,ItemTypePurchn as E " +
                          "where A.ItemID = B.ID and A.SatuanID = C.ID and A.GroupID = D.ID and A.ItemTypeID = E.ID " +
                          "and A.Status = 0 and A.DepoID = " + DepoID + this.Criteria;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSPP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSPP.Add(GenerateObject(sqlDataReader));
                }
            }
            return arrSPP;
        }

        public SPP RetrieveByID2(int id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 50 A.ID,A.NoSPP,A.HeadID,A.Minta,A.PermintaanType,SatuanID,A.GroupID,A.ItemTypeID,A.Jumlah,A.JumlahSisa,A.Keterangan,A.Sudah,A.FCetak,A.UserID,A.Pending,A.Inden,A.AlasanBatal,A.AlasanCLS,A.Status,A.Approval,A.CreatedBy,A.CreatedTime as Tanggal,A.LastModifiedBy,A.LastModifiedTime,A.Approvedate1,A.Approvedate2,A.Approvedate3 from SPP as A where A.ID = " + id);
            strError = dataAccess.Error;
            arrSPP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new SPP();
        }

        //
        public SPP RetrieveByCriteriaSPP(string strField, string strValue)
        {
            string strSQL = "select A.ID,A.NoSPP,A.HeadID,A.Minta,A.PermintaanType,A.SatuanID,A.GroupID,A.ItemTypeID,A.Jumlah,A.JumlahSisa," +
                            "A.Keterangan,A.Sudah,A.FCetak,A.UserID,A.Pending,A.Inden,A.AlasanBatal,A.AlasanCLS,A.Status,A.Status," +
                            "A.CreatedBy,A.CreatedTime as Tanggal,A.LastModifiedBy,A.LastModifiedTime,A.Approval,A.DepoID," +
                            "A.Approvedate1,A.Approvedate2,A.Approvedate3 from SPP as A " +
                            "where " + strField + " like '%" + strValue + "%'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            //pake A.Status
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.NoSPP,A.Minta,A.PermintaanType,A.SatuanID,A.GroupID,A.ItemTypeID,A.Jumlah,A.JumlahSisa,A.Keterangan,A.Sudah,A.FCetak,A.UserID,A.Pending,A.Inden,A.AlasanBatal,A.AlasanCLS,A.Status,A.Status,A.CreatedBy,A.CreatedTime as Tanggal,A.LastModifiedBy,A.LastModifiedTime,A.Approval,A.DepoID from SPP as A where A.Status = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrSPP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new SPP();
        }

        public SPP RetrieveById(int Id)
        {
            string strSQL = "select A.ID,A.NoSPP,A.HeadID,A.Minta,A.PermintaanType,A.SatuanID,A.GroupID,A.ItemTypeID,A.Jumlah," +
                            "A.JumlahSisa,A.Keterangan,A.Sudah,A.FCetak,A.UserID,A.Pending,A.Inden,A.AlasanBatal,A.AlasanCLS," +
                            "A.Status,A.Approval,A.CreatedBy,A.DepoID,A.CreatedTime,A.Approvedate1,A.Approvedate2,A.Approvedate3 as Tanggal from SPP as A where A.ID = " + Id;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSPP = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new SPP();
        }

        public SPP RetrieveByIdStatus(int Id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(
                "select A.ID,A.NoSPP,A.HeadID,A.Minta,A.PermintaanType,A.SatuanID,A.GroupID,A.ItemTypeID,A.Jumlah,A.JumlahSisa,A.Keterangan,A.Sudah," +
                "A.FCetak,A.UserID,A.Pending,A.Inden,A.AlasanBatal,A.AlasanCLS,A.Status,A.Approval,A.DepoID,A.CreatedBy,A.CreatedTime as Tanggal," +
                "A.LastModifiedBy,A.LastModifiedTime,A.Approvedate1,A.Approvedate2,A.Approvedate3 from SPP as A where A.ID = " + Id);
            strError = dataAccess.Error;
            arrSPP = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new SPP();
        }

        public SPP RetrieveIDForPO(int Id)
        {
            string strSQL = "select A.ID,A.NoSPP,A.PermintaanType,A.HeadID,A.Minta,A.SatuanID,A.GroupID,A.ItemTypeID,A.Jumlah," +
                           "A.JumlahSisa,A.Keterangan,A.Sudah,A.FCetak,A.UserID,A.Pending,A.Inden,A.AlasanBatal,A.AlasanCLS," +
                           "A.Status,A.Approval,A.CreatedBy,A.CreatedTime as Tanggal,A.LastModifiedBy,A.LastModifiedTime,A.Approvedate1,A.Approvedate2,A.Approvedate3 " +
                           "from SPP as A where A.ID = " + Id;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrSPP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new SPP();
        }

        public SPP RetrieveByCode(string groupsCode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.GroupCode,A.HeadID,A.GroupDescription,A.ItemTypeID,A.DeptID,B.TypeDescription,A.Approval,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.Approvedate1,A.Approvedate2,A.Approvedate3 from GroupsPurchn as A,ItemTypePurchn as B where A.ItemTypeID = B.ID and A.Status = 0 and A.GroupCode = '" + groupsCode + "'");
            strError = dataAccess.Error;
            arrSPP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new SPP();
        }

        public SPP RetrieveByName(string groupsName)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.GroupCode,A.HeadID,A.GroupDescription,A.ItemTypeID,A.DeptID,B.TypeDescription,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime from GroupsPurchn as A,ItemTypePurchn as B where A.ItemTypeID = B.ID and A.Status = 0 and A.GroupDescription = '" + groupsName + "'");
            strError = dataAccess.Error;
            arrSPP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new SPP();
        }
        public ArrayList RetrieveByApprovalOpen()
        {
            string strSQL = "select A.ID,A.NoSPP,A.HeadID,A.Minta,A.ItemID,A.SatuanID,A.GroupID,A.ItemTypeID,A.Jumlah,A.JumlahSisa," +
                            "A.Keterangan,A.Sudah,A.FCetak,A.UserID,A.Pending,A.Inden,A.AlasanBatal,A.AlasanCLS,A.Status,A.Approval," +
                            "A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ApproveDate1,A.ApproveDate2," +
                            "A.ApproveDate3,B.ID as DepoID, B.DepoName as DepoName from SPP as A,Depo as B where A.DepoID = B.ID " +
                            "and A.Approval = 0 order by A.ID";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.NoSPP,A.Minta,A.ItemID,A.SatuanID,A.GroupID,A.ItemTypeID,A.Jumlah,A.JumlahSisa,A.Keterangan,A.Sudah,A.FCetak,A.UserID,A.Pending,A.Inden,A.AlasanBatal,A.AlasanCLS,A.Status,A.Approval,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ApproveDate1,A.ApproveDate2,A.ApproveDate3,B.ID as DepoID, B.DepoName as DepoName from SPP as A,Depo as B where A.DepoID = B.ID and A.Status = 0 and A.Approval = 0 and A.DepoID =  2 order by A.ID");
            strError = dataAccess.Error;
            arrSPP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSPP.Add(GenerateObject(sqlDataReader));
                }
            }

            return arrSPP;
        }
        public ArrayList RetrieveForApproval()
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            arrSPP = new ArrayList();
            switch (users.Apv)
            {
                case 1:
                    arrSPP = RetrieveAppOpenByDepo1(0, users.UnitKerjaID, users.ID);
                    break;
                case 2:
                    arrSPP = RetrieveAppOpenByDepo2(1, users.UnitKerjaID, users.ID);
                    break;
                case 3:
                    arrSPP = RetrieveAppOpenByDepo3(2, users.UnitKerjaID, users.ID);
                    break;
            }
            return arrSPP;
        }
        public ArrayList RetrieveForApproval(int AppLevel)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            arrSPP = new ArrayList();
            switch (users.Apv)
            {
                case 1:
                    arrSPP = RetrieveAppOpenByDepo1(AppLevel, users.UnitKerjaID, users.ID);
                    break;
                case 2:
                    arrSPP = RetrieveAppOpenByDepo2(AppLevel, users.UnitKerjaID, users.ID);
                    break;
                case 3:
                    arrSPP = RetrieveAppOpenByDepo3(AppLevel, users.UnitKerjaID, users.ID);
                    break;
            }
            //arrSPP = RetrieveSPP4Aproval();
            return arrSPP;
        }
        public ArrayList RetrieveAppOpenByDepo2(int Approval, int DepoID, int userID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.NoSPP,A.Minta,A.HeadID,A.HeadID,A.PermintaanType,A.SatuanID,A.GroupID,A.ItemTypeID,A.Jumlah,A.JumlahSisa,A.Keterangan,A.Sudah,A.FCetak,A.UserID,A.Pending,A.Inden,A.AlasanBatal,A.AlasanCLS,A.Status,A.Approval,A.CreatedBy,A.CreatedTime as Tanggal,A.LastModifiedBy,A.LastModifiedTime,A.ApproveDate1,A.ApproveDate2,A.ApproveDate3,B.ID as DepoID, B.DepoName as DepoName from SPP as A,Depo as B where A.DepoID = B.ID and A.Status = 0 and A.Approval = " + Approval + " and A.DepoID =  " + DepoID + " order by A.ID");
            string strsql = "select A.ID,A.NoSPP,A.Minta,A.HeadID,A.HeadID,A.PermintaanType,A.SatuanID,A.GroupID,A.ItemTypeID, " +
                                        "A.Jumlah,A.JumlahSisa,A.Keterangan,A.Sudah,A.FCetak,A.UserID,A.Pending,A.Inden,A.AlasanBatal, " +
                                        "A.AlasanCLS,A.Status,A.Approval,A.CreatedBy,A.CreatedTime as Tanggal,A.LastModifiedBy, " +
                                        "A.LastModifiedTime,A.ApproveDate1,A.ApproveDate2,A.ApproveDate3,B.ID as DepoID, B.DepoName as DepoName,  " +
                                        "case when A.HeadID > 0 then(select UserName from Users where ID = A.HeadID) end HeadName, " +
                                        "case when A.UserID > 0 then(select UserName from Users where ID = A.UserID) end UserName  " +
                                        "from SPP as A,Depo as B ,sppdetail C " +
                                        "where A.ID=C.sppid and A.nospp<>'' and A.DepoID = B.ID and A.Status = 0 and A.Approval = " + Approval + " and A.DepoID =  " + DepoID +
                                        " and a.userid in (select userid from listuserhead where mgrid=" + userID +" and ListUserHead.RowStatus>-1) "+
                                        " and a.HeadID in (select HeadID from listuserhead where mgrid=" + userID + " and ListUserHead.RowStatus>-1) " +
                                        " and C.Status>-1 order by A.ID";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrSPP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSPP.Add(GenerateObjectUserHead(sqlDataReader));
                }
            }

            return arrSPP;
        }
        public ArrayList RetrieveSPP4Aproval()
        {
            string strSQL = "select A.ID,A.NoSPP,A.Minta,A.HeadID,A.HeadID,A.PermintaanType,A.SatuanID,A.GroupID,A.ItemTypeID, "+
                            "A.Jumlah,A.JumlahSisa,A.Keterangan,A.Sudah,A.FCetak,A.UserID,A.Pending,A.Inden,A.AlasanBatal, "+
                            "A.AlasanCLS,A.Status,A.Approval,A.CreatedBy,A.CreatedTime as Tanggal,A.LastModifiedBy, A.LastModifiedTime,"+
                            "A.ApproveDate1,A.ApproveDate2,A.ApproveDate3,B.ID as DepoID, B.DepoName as DepoName,  case when A.HeadID > 0 "+
                            "then(select UserName from Users where ID = A.HeadID) end HeadName, case when A.UserID > 0 "+
                            "then(select UserName from Users where ID = A.UserID) end UserName  "+
                            "from SPP as A,Depo as B ,sppdetail C where A.ID=C.sppid and A.nospp<>'' AND YEAR(A.Minta)>2016 and A.DepoID = B.ID "+
                            "and A.Status = 0 "+this.Criteria+"/*and ((Approval=1 AND HeadID in(273) OR Approval=2)*/ and C.Status>-1 order by A.ID";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSPP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSPP.Add(GenerateObjectUserHead(sqlDataReader));
                }
            }

            return arrSPP;
        }
        public ArrayList RetrieveAppOpenByDepo3(int Approval, int DepoID, int userID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.NoSPP,A.Minta,A.HeadID,A.HeadID,A.PermintaanType,A.SatuanID,A.GroupID,A.ItemTypeID,A.Jumlah,A.JumlahSisa,A.Keterangan,A.Sudah,A.FCetak,A.UserID,A.Pending,A.Inden,A.AlasanBatal,A.AlasanCLS,A.Status,A.Approval,A.CreatedBy,A.CreatedTime as Tanggal,A.LastModifiedBy,A.LastModifiedTime,A.ApproveDate1,A.ApproveDate2,A.ApproveDate3,B.ID as DepoID, B.DepoName as DepoName from SPP as A,Depo as B where A.DepoID = B.ID and A.Status = 0 and A.Approval = " + Approval + " and A.DepoID =  " + DepoID + " order by A.ID");
            string strsql = "select A.ID,A.NoSPP,A.Minta,A.HeadID,A.HeadID,A.PermintaanType,A.SatuanID,A.GroupID,A.ItemTypeID, " +
                "A.Jumlah,A.JumlahSisa,A.Keterangan,A.Sudah,A.FCetak,A.UserID,A.Pending,A.Inden,A.AlasanBatal, " +
                "A.AlasanCLS,A.Status,A.Approval,A.CreatedBy,A.CreatedTime as Tanggal,A.LastModifiedBy, " +
                "A.LastModifiedTime,A.ApproveDate1,A.ApproveDate2,A.ApproveDate3,B.ID as DepoID, B.DepoName as DepoName,  " +
                "case when A.HeadID > 0 then(select UserName from Users where ID = A.HeadID) end HeadName, " +
                "case when A.UserID > 0 then(select UserName from Users where ID = A.UserID) end UserName  " +
                "from SPP as A,Depo as B ,sppdetail C " +
                "where A.ID=C.sppid and A.nospp<>'' and A.DepoID = B.ID and A.Status = 0 and A.Approval = " + Approval + " and A.DepoID =  " + DepoID +
                " and a.userid in (select userid from listuserhead where managerid=" + userID +
                ") and C.Status>-1 order by A.ID";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrSPP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSPP.Add(GenerateObjectUserHead(sqlDataReader));
                }
            }

            return arrSPP;
        }
        //public ArrayList RetrieveAppOpenByDepo1(int Approval, int DepoID, int groupID)
        public ArrayList RetrieveAppOpenByDepo1(int Approval, int DepoID, int headID)
        {
            string strGroupID = string.Empty;

            strGroupID = " and A.HeadID = " + headID;

            string strSQL = "select A.ID,A.NoSPP,A.HeadID,A.Minta,A.PermintaanType,A.SatuanID," +
                "A.GroupID,A.ItemTypeID,A.Jumlah,A.JumlahSisa,A.Keterangan,A.Sudah,A.FCetak,A.UserID,A.Pending,A.Inden,A.AlasanBatal,A.AlasanCLS," +
                "A.Status,A.Approval,A.CreatedBy,A.CreatedTime as Tanggal,A.LastModifiedBy,A.LastModifiedTime,A.ApproveDate1,A.ApproveDate2," +
                "A.ApproveDate3,B.ID as DepoID, B.DepoName as DepoName,(Select UserName from users where ID=A.UserID)UserName," +
                "(Select UserName from users where ID=A.HeadID)HeadName " +
                "FROM SPP as A,Depo as B, sppdetail C where A.nospp<>'' and A.DepoID = B.ID " +
                "and A.ID=C.sppid and A.Status = 0 and A.Approval = " +
                Approval + " and C.Status >-1 and A.DepoID =  " + DepoID + strGroupID + " order by A.ID";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSPP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSPP.Add(GeneratUserObject(sqlDataReader, GenerateObject(sqlDataReader)));
                }
            }

            return arrSPP;
        }

        public SPP RetrieveAppDepoByID(int DepoID, string noSPP)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "select A.ID,A.NoSPP,A.Approval from SPP A where A.Status > -1 and A.DepoID = " + DepoID + " and A.NoSPP = '" + noSPP + "'";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.NoSPP from SPP as A where A.Status > -1 and A.Approval > 1 and A.DepoID = " + DepoID + " and A.NoSPP = '" + noSPP + "'");
            strError = dataAccess.Error;
            arrSPP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new SPP();
        }

        //public ArrayList RetrieveByApprovalHead()
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.NoSPP,A.Minta,A.ItemID,A.SatuanID,A.GroupID,A.ItemID,A.Jumlah,A.JumlahSisa,A.Keterangan,A.Sudah,A.FCetak,A.UserID,A.Pending,A.Inden,A.AlasanBatal,A.AlasanCLS,A.Status,A.Approval,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ApproveDate1,A.ApproveDate2,A.ApproveDate3,B.ID as DepoID, B.DepoName as DepoName from SPP as A,Depo as B where A.DepoID = B.ID and A.Approval = 1 order by A.ID");
        //    strError = dataAccess.Error;
        //    arrSPP = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrSPP.Add(GenerateObject(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrSPP.Add(new SPP());

        //    return arrSPP;
        //}

        //public ArrayList RetrieveByApprovalPM()
        //{
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.NoSPP,A.Minta,A.ItemID,A.SatuanID,A.GroupID,A.ItemID,A.Jumlah,A.JumlahSisa,A.Keterangan,A.Sudah,A.FCetak,A.UserID,A.Pending,A.Inden,A.AlasanBatal,A.AlasanCLS,A.Status,A.Approval,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.ApproveDate1,A.ApproveDate2,A.ApproveDate3,B.ID as DepoID, B.DepoName as DepoName from SPP as A,Depo as B where A.DepoID = B.ID and A.Approval = 2 order by A.ID");
        //    strError = dataAccess.Error;
        //    arrSPP = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrSPP.Add(GenerateObject(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrSPP.Add(new SPP());

        //    return arrSPP;
        //}

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            string strSQL = "select A.ID,A.NoSPP,A.HeadID,A.Minta,A.ItemID,A.SatuanID,A.GroupID,A.ItemTypeID,A.Jumlah,A.JumlahSisa," +
                          "A.Keterangan,A.Sudah,A.FCetak,A.UserID,A.Pending,A.Inden,A.AlasanBatal,A.AlasanCLS,A.Status,A.Status,A.CreatedBy," +
                          "A.CreatedTime as Tanggal,A.LastModifiedBy,A.LastModifiedTime,A.Approvedate1,A.Approvedate2,A.Approvedate3 from SPP as A where A.Status = 0 and " + strField +
                          " like '%" + strValue + "%'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            #region depreciated line
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.NoSPP,A.Minta,A.GroupID,A.ItemTypeID,A.Keterangan,B.ItemID,B.Quantity,B.ItemID,C.UOMCode,A.SatuanID,A.Jumlah,A.JumlahSisa,A.Sudah,A.FCetak,A.UserID,A.Pending,A.Inden,A.AlasanBatal,A.AlasanCLS,A.Status,A.Approval,A.DepoID," +
            //                                                                "case A.ItemTypeID when 1 then (select ItemName from Inventory where ID=B.ItemID) "+
            //                                                                "when 2 then (select ItemName from Asset where ID=B.ItemID) "+
            //                                                                "when 3 then (select ItemName from Biaya where ID=B.ItemID) end ItemName,"+
            //                                                                "case A.ItemTypeID when 1 then (select ItemCode from Inventory where ID=B.ItemID) "+
            //                                                                "when 2 then (select ItemCode from Asset where ID=B.ItemID) "+
            //                                                                "when 3 then (select ItemCode from Biaya where ID=B.ItemID) end ItemCode "+
            //                                                                "from SPP as A, SPPDetail as B, UOM as C "+
            //                                                                "where A.ID=B.SPPID and B.ItemID=C.ID and " + strField + " like '%" + strValue + "%'");
            #endregion
            strError = dataAccess.Error;
            arrSPP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSPP.Add(GenerateObject(sqlDataReader));
                }
            }
            return arrSPP;
        }

        public string HeadID()
        {
            Users arr = (Users)System.Web.HttpContext.Current.Session["Users"];

            string strSQL = (arr.Apv < 1) ? " and (HeadID in(Select HeadID from ListUserHead where userID=" + arr.ID + ") or UserID=" + arr.ID + ") " : string.Empty;
            strSQL += (arr.Apv == 1 && arr.UserLevel < 1) ? " and HeadID=" + arr.ID : "";
            return strSQL;
        }
        public ArrayList RetrieveByAll(int groupID)
        {
            string strGroupID = string.Empty;
            if (groupID == 1 || groupID == 2)
            {
                strGroupID = " and A.GroupID in (1,2)";
            }
            if (groupID == 3)
            {
                strGroupID = " and A.GroupID in (3)";
            }
            if (groupID == 4)
            {
                strGroupID = " and A.GroupID in (4)";
            }
            if (groupID == 5)
            {
                strGroupID = " and A.GroupID in (5)";
            }
            if (groupID == 6 || groupID == 7 || groupID == 8 || groupID == 9)
            {
                strGroupID = " and A.GroupID in (4,5,6,7,8,9)";
            }
            strGroupID = string.Empty;
            string limit = (strGroupID == string.Empty) ? "TOP 250 " : "";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select " + limit + " A.ID,A.NoSPP,A.HeadID,A.Minta,B.Status,A.Approval,A.CreatedBy,B.ItemID," +
                "case when B.ItemTypeID!=3 then B.Keterangan else '' end Keterangan,B.ItemTypeID,B.GroupID," +
                "case B.ItemTypeID when 1 then (select ItemCode from Inventory where Inventory.ID=B.ItemID) " +
                "when 2 then (select ItemCode from Asset where Asset.ID=B.ItemID) " +
                "when 3 then (select ItemCode from Biaya where Biaya.ID=B.ItemID) else '' end ItemCode," +
                "case B.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=B.ItemID) " +
                "when 2 then (select ItemName from Asset where Asset.ID=B.ItemID) " +
                "when 3 then (select ItemName from Biaya where Biaya.ID=B.ItemID)+ ' - '+ B.Keterangan else '' end ItemName, C.UOMCode " +
                "from SPP as A, SPPDetail as B, UOM as C where A.ID=B.SPPID and B.Status>-1 and B.UOMID=C.ID and A.Status>-1 " +
                strGroupID + HeadID() + " order by A.ID Desc";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrSPP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSPP.Add(GenerateObjectList(sqlDataReader));
                }
            }
           

            return arrSPP;
        }

        public ArrayList RetrieveByAllByUserID(int userID, int headID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.NoSPP,A.HeadID,A.Minta,A.Status,A.Approval,A.CreatedBy,B.ItemID,B.Keterangan,B.ItemTypeID,B.GroupID," +
                "case B.ItemTypeID when 1 then (select ItemCode from Inventory where Inventory.ID=B.ItemID) " +
                "when 2 then (select ItemCode from Asset where Asset.ID=B.ItemID) " +
                "when 3 then (select ItemCode from Biaya where Biaya.ID=B.ItemID) else '' end ItemCode," +
                "case B.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=B.ItemID) " +
                "when 2 then (select ItemName from Asset where Asset.ID=B.ItemID) " +
                "when 3 then (select ItemName from Biaya where Biaya.ID=B.ItemID) else '' end ItemName, C.UOMCode " +
                "from SPP as A, SPPDetail as B, UOM as C where A.ID=B.SPPID and B.Status>-1 and B.UOMID=C.ID and A.Status>-1 A.usetID=" + userID + " or  A.headID=" + headID + " order by A.ID Desc");

            strError = dataAccess.Error;
            arrSPP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSPP.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrSPP.Add(new SPP());

            return arrSPP;
        }

        //public ArrayList RetrieveByAllWithStatus(int groupID,string strField, string strValue)
        public ArrayList RetrieveByAllWithStatus(int headID, string strField, string strValue)
        {
            Users arr = (Users)System.Web.HttpContext.Current.Session["Users"];
            string strGroupID = string.Empty;
            int apv = arr.Apv;
            strGroupID = (headID > 0 && apv < 2) ? " and A.HeadID = " + headID : "";
            #region depreciated line
            //if (groupID == 1 || groupID == 2)
            //{
            //    strGroupID = " and A.GroupID in (1,2)";
            //}
            //if (groupID == 3)
            //{
            //    strGroupID = " and A.GroupID in (3)";
            //}
            //if (groupID == 4)
            //{
            //    strGroupID = " and A.GroupID in (4)";
            //}
            //if (groupID == 5)
            //{
            //    strGroupID = " and A.GroupID in (5)";
            //}
            //if (groupID == 6 || groupID == 7 || groupID == 8 || groupID == 9)
            //{
            //    strGroupID = " and A.GroupID in (6,7,8,9)";
            //}

            #endregion
            string strSQL = "select A.ID,A.NoSPP,A.Minta,B.Status,A.Approval,A.CreatedBy,B.ItemID,B.Keterangan,B.ItemTypeID,B.GroupID," +
                "case B.ItemTypeID when 1 then (select ItemCode from Inventory where Inventory.ID=B.ItemID) " +
                "when 2 then (select ItemCode from Asset where Asset.ID=B.ItemID) " +
                "when 3 then (select ItemCode from Biaya where Biaya.ID=B.ItemID) else '' end ItemCode," +
                "case B.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=B.ItemID) " +
                "when 2 then (select ItemName from Asset where Asset.ID=B.ItemID) " +
                "when 3 then (select ItemName from Biaya where Biaya.ID=B.ItemID)+'-'+B.Keterangan else '' end ItemName, C.UOMCode " +
                "from SPP as A, SPPDetail as B, UOM as C where A.ID=B.SPPID and A.Status>-1 and B.Status>-1 and B.UOMID=C.ID " + strGroupID +
                " and " + strField + " like '%" + strValue + "%'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrSPP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSPP.Add(GenerateObjectList(sqlDataReader));
                }
            }
            else
                arrSPP.Add(new SPP());

            return arrSPP;
        }
        private string LimitData()
        {
            string strQuery = (HttpContext.Current.Session["Limit"] == null) ? string.Empty : HttpContext.Current.Session["Limit"].ToString();
            return strQuery;
        }
        //public ArrayList RetrieveByAll2(int groupID,int approval)
        public ArrayList RetrieveByAll2(string strHeadID, int approval)
        {
            string strGroupID = string.Empty;
            //if (strHeadID!=string.Empty)
            //    strGroupID =(approval==0)?" and HeadID in("+ strHeadID+")": " and A.HeadID in (" + strHeadID + ")";
            strGroupID = strHeadID;
            string strSQL = "select " + this.LimitData() + " A.ID,A.NoSPP,A.Minta,A.Status,A.Approval,A.CreatedBy,B.ItemID,B.Keterangan,B.ItemTypeID,B.GroupID," +
                "case B.ItemTypeID when 1 then (select ItemCode from Inventory where Inventory.ID=B.ItemID) " +
                "when 2 then (select ItemCode from Asset where Asset.ID=B.ItemID) " +
                "when 3 then (select ItemCode from Biaya where Biaya.ID=B.ItemID) else '' end ItemCode," +
                "case B.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=B.ItemID) " +
                "when 2 then (select ItemName from Asset where Asset.ID=B.ItemID) " +
                "when 3 then " + ItemSPPBiayaNew("B") + " end ItemName, C.UOMCode,B.Quantity,B.QtyPO,A.UserID,A.HeadID " +
                "from SPP as A, SPPDetail as B, UOM as C where A.nospp<>'' and A.ID=B.SPPID and B.Status>-1 and " +
                "B.UOMID=C.ID and A.Status=0 and A.Approval= " + approval + strGroupID + " order by A.ID Desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrSPP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSPP.Add(GenerateObjectList(sqlDataReader, GenerateObjectList(sqlDataReader)));
                }
            }
            else
                arrSPP.Add(new SPP());

            return arrSPP;
        }
        public ArrayList RetrieveByAll3(string strHeadID, int approval, string SortBy)
        {
            string strGroupID = string.Empty;
            //if (strHeadID!=string.Empty)
            //    strGroupID =(approval==0)?" and HeadID in("+ strHeadID+")": " and A.HeadID in (" + strHeadID + ")";
            strGroupID = strHeadID;
            string strSQL = "select " + this.LimitData() + " A.ID,A.NoSPP,A.Minta,A.Status,A.Approval,A.CreatedBy,B.ItemID,B.Keterangan,B.ItemTypeID,B.GroupID," +
                "case B.ItemTypeID when 1 then (select ItemCode from Inventory where Inventory.ID=B.ItemID) " +
                "when 2 then (select ItemCode from Asset where Asset.ID=B.ItemID) " +
                "when 3 then (select ItemCode from Biaya where Biaya.ID=B.ItemID) else '' end ItemCode," +
                "case B.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=B.ItemID) " +
                "when 2 then (select ItemName from Asset where Asset.ID=B.ItemID) " +
                "when 3 then " + ItemSPPBiayaNew("B") + " end ItemName, C.UOMCode,B.Quantity,B.QtyPO,A.UserID,A.HeadID " +
                "from SPP as A, SPPDetail as B, UOM as C where A.nospp<>'' and A.ID=B.SPPID and B.Status>-1 and " +
                "B.UOMID=C.ID and A.Status=0 and A.Approval= " + approval + strGroupID + SortBy;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrSPP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSPP.Add(GenerateObjectList(sqlDataReader, GenerateObjectList(sqlDataReader)));
                }
            }
            else
                arrSPP.Add(new SPP());

            return arrSPP;
        }
        public int CountNoSPP(string preSPP)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            ////SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT MAX(SUBSTRING(DestackingID,6,5)) as id from Destacking");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT isnull(MAX(SUBSTRING(NoSPP,3,4)),0) as id from SPP where LEFT(SPP.NoSPP,2) = '" + preSPP + "'");

            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["id"]);
                }
            }

            return 0;
        }

        public SPP RetrieveByNo(string NOSPP)
        {
            string strSQL = "select A.ID,A.NoSPP,A.HeadID,A.Minta,A.PermintaanType,A.HeadID,A.SatuanID,A.GroupID,A.ItemTypeID," +
                            "A.Jumlah,A.JumlahSisa,A.Keterangan,A.Sudah,A.FCetak,A.UserID,A.Pending,A.Inden,AlasanBatal,A.AlasanCLS," +
                            "A.Status,A.Approval,A.DepoID,A.CreatedTime as Tanggal,A.CreatedBy,A.ApproveDate1,A.ApproveDate2,A.ApproveDate3 " +
                            ",(Select UserName from users where ID=A.UserID)UserName,(Select UserName from users where ID=A.HeadID)HeadName " +
                            "from SPP as A where A.NoSPP = '" + NOSPP + "'  " +
                            "and Status >-1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSPP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GeneratUserObject(sqlDataReader, GenerateObject(sqlDataReader));
                }
            }

            return new SPP();
        }

        public SPP RetrieveAliasUser(string NOSPP)
        {
            string strSQL =
            "with data1 as (select userid,HeadID from spp where NoSPP='"+NOSPP+"' and Status>-1), " +
            "data2 as (select B.UserAlias UserAdmin,C.UserAlias UserHead from data1 A left join users B ON A.UserID=B.ID " +
            "left join Users C ON C.ID=A.HeadID) " +

            "select * from data2 ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSPP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GeneratUserObjectAlias(sqlDataReader);
                }
            }

            return new SPP();
        }

        public SPP GeneratUserObjectAlias(SqlDataReader sqlDataReader)
        {
            objSPP = new SPP();
            objSPP.UserAdmin = sqlDataReader["UserAdmin"].ToString();
            objSPP.UserHead = sqlDataReader["UserHead"].ToString();
            //objSPP.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            return objSPP;
        }

        public SPP GenerateObject(SqlDataReader sqlDataReader)
        {
            objSPP = new SPP();
            objSPP.ID = Convert.ToInt32(sqlDataReader["ID"]);

            objSPP.NoSPP = sqlDataReader["NoSPP"].ToString();
            objSPP.Minta = Convert.ToDateTime(sqlDataReader["Minta"]);
            objSPP.PermintaanType = Convert.ToInt32(sqlDataReader["PermintaanType"]);

            objSPP.SatuanID = Convert.ToInt32(sqlDataReader["SatuanID"]);
            objSPP.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objSPP.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"]);
            objSPP.Jumlah = Convert.ToDecimal(sqlDataReader["Jumlah"]);
            objSPP.JumlahSisa = Convert.ToDecimal(sqlDataReader["JumlahSisa"]);
            objSPP.Keterangan = sqlDataReader["Keterangan"].ToString();
            objSPP.Sudah = Convert.ToInt32(sqlDataReader["Sudah"]);
            objSPP.FCetak = Convert.ToInt32(sqlDataReader["FCetak"]);
            objSPP.UserID = Convert.ToInt32(sqlDataReader["UserID"]);
            objSPP.Pending = Convert.ToInt32(sqlDataReader["Pending"]);
            objSPP.Inden = Convert.ToInt32(sqlDataReader["Inden"]);
            objSPP.AlasanBatal = sqlDataReader["AlasanBatal"].ToString();
            objSPP.AlasanCLS = sqlDataReader["AlasanCLS"].ToString();
            objSPP.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objSPP.Approval = Convert.ToInt32(sqlDataReader["Approval"]);
            objSPP.DepoID = Convert.ToInt32(sqlDataReader["DepoID"]);
            objSPP.Tanggal = DateTime.Parse(sqlDataReader["Tanggal"].ToString());
            objSPP.CreatedBy = sqlDataReader["CreatedBy"].ToString();

            objSPP.HeadID = Convert.ToInt32(sqlDataReader["HeadID"]);
            objSPP.ApproveDate1 = DateTime.Parse(sqlDataReader["ApproveDate1"].ToString());
            objSPP.ApproveDate2 = DateTime.Parse(sqlDataReader["ApproveDate2"].ToString());
            //objSPP.ApproveDate3 = DateTime.Parse(sqlDataReader["ApproveDate3"].ToString());
            //objSPP.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            //objSPP.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            //objSPP.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            //objSPP.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objSPP;
        }
        public SPP GeneratUserObject(SqlDataReader sqlDataReader, SPP sppne)
        {
            objSPP = (SPP)sppne;
            objSPP.UserName = sqlDataReader["UserName"].ToString();
            objSPP.HeadName = sqlDataReader["HeadName"].ToString();
            objSPP.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            return objSPP;
        }

        public SPP GenerateObjectUserHead(SqlDataReader sqlDataReader)
        {
            objSPP = new SPP();
            objSPP.ID = Convert.ToInt32(sqlDataReader["ID"]);

            objSPP.NoSPP = sqlDataReader["NoSPP"].ToString();
            objSPP.Minta = Convert.ToDateTime(sqlDataReader["Minta"]);
            objSPP.PermintaanType = Convert.ToInt32(sqlDataReader["PermintaanType"]);

            objSPP.SatuanID = Convert.ToInt32(sqlDataReader["SatuanID"]);
            objSPP.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objSPP.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"]);
            objSPP.Jumlah = Convert.ToDecimal(sqlDataReader["Jumlah"]);
            objSPP.JumlahSisa = Convert.ToDecimal(sqlDataReader["JumlahSisa"]);
            objSPP.Keterangan = sqlDataReader["Keterangan"].ToString();
            objSPP.Sudah = Convert.ToInt32(sqlDataReader["Sudah"]);
            objSPP.FCetak = Convert.ToInt32(sqlDataReader["FCetak"]);
            objSPP.UserID = Convert.ToInt32(sqlDataReader["UserID"]);
            objSPP.Pending = Convert.ToInt32(sqlDataReader["Pending"]);
            objSPP.Inden = Convert.ToInt32(sqlDataReader["Inden"]);
            objSPP.AlasanBatal = sqlDataReader["AlasanBatal"].ToString();
            objSPP.AlasanCLS = sqlDataReader["AlasanCLS"].ToString();
            objSPP.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objSPP.Approval = Convert.ToInt32(sqlDataReader["Approval"]);
            objSPP.DepoID = Convert.ToInt32(sqlDataReader["DepoID"]);
            objSPP.Tanggal = DateTime.Parse(sqlDataReader["Tanggal"].ToString());
            objSPP.CreatedBy = sqlDataReader["CreatedBy"].ToString();

            objSPP.HeadID = Convert.ToInt32(sqlDataReader["HeadID"]);
            objSPP.UserName = sqlDataReader["UserName"].ToString();
            objSPP.HeadName = sqlDataReader["HeadName"].ToString();
            if (Convert.ToInt32(sqlDataReader["Approval"]) == 1)
            {
                objSPP.ApproveDate1 = DateTime.Parse(sqlDataReader["ApproveDate1"].ToString());
            }
            if (Convert.ToInt32(sqlDataReader["Approval"]) == 2)
            {
                objSPP.ApproveDate2 = DateTime.Parse(sqlDataReader["ApproveDate2"].ToString());

            }
            if (Convert.ToInt32(sqlDataReader["Approval"]) == 3)
            {
                objSPP.ApproveDate3 = DateTime.Parse(sqlDataReader["ApproveDate3"].ToString());
            }
            //objSPP.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            //objSPP.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            //objSPP.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            //objSPP.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objSPP;
        }

        public SPP GenerateObject2(SqlDataReader sqlDataReader)
        {
            objSPP = new SPP();
            objSPP.ID = Convert.ToInt32(sqlDataReader["ID"]);

            objSPP.NoSPP = sqlDataReader["NoSPP"].ToString();
            objSPP.Approval = Convert.ToInt32(sqlDataReader["Approval"]);

            return objSPP;
        }

        public SPP GenerateObjectList(SqlDataReader sqlDataReader)
        {
            objSPP = new SPP();
            objSPP.ID = Convert.ToInt32(sqlDataReader["ID"]);

            objSPP.NoSPP = sqlDataReader["NoSPP"].ToString();
            objSPP.Minta = Convert.ToDateTime(sqlDataReader["Minta"]);
            objSPP.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objSPP.Approval = Convert.ToInt32(sqlDataReader["Approval"]);
            objSPP.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objSPP.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objSPP.Keterangan = sqlDataReader["Keterangan"].ToString();
            objSPP.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
            objSPP.ItemTypeID = Convert.ToInt32(sqlDataReader["ItemTypeID"]);
            objSPP.ItemCode = sqlDataReader["ItemCode"].ToString();
            objSPP.ItemName = sqlDataReader["ItemName"].ToString();
            objSPP.UomCode = sqlDataReader["UomCode"].ToString();

            return objSPP;
        }
        private SPP GenerateObjectList(SqlDataReader sdr, SPP oldSPPObject)
        {
            objSPP = (SPP)oldSPPObject;
            objSPP.UserID = Convert.ToInt32(sdr["UserID"].ToString());
            objSPP.HeadID = Convert.ToInt32(sdr["HeadID"].ToString());
            objSPP.JumlahSisa = Convert.ToDecimal(sdr["Quantity"].ToString());
            return objSPP;
        }
        /**
         * added on 28-04-2014
         * untuk perubahan pada itemname table biaya
         * dan stock per itemnya
         */

        public string ItemSPPBiayaNew(string TableName)
        {
            string strSQL = "CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())< " +
                " (Select CreatedTime from SPP where SPP.ID=" + TableName + ".SPPID)) " +
                " THEN(select ItemName from Biaya where Biaya.ID=" + TableName + ".ItemID and Biaya.RowStatus>-1)+' - '+ " +
                " (Select SPPDetail.Keterangan From SPPDetail where /*SPPDetail.ItemID=" + TableName + ".ItemID and*/ " +
                " SPPDetail.ID=" + TableName + ".ID) ELSE " +
                " (select ItemName from biaya where ID=" + TableName + ".ItemID and biaya.RowStatus>-1) END ";
            return strSQL;
        }
        /**
         * added on 04-06-2014
         * untuk menampilkan info update terbaru sistem
         * jika user pertama kali membuka halaman yang telah terupdate
         */
        public int ShowInfoStatus(int UserID, string ModulName)
        {
            string strSQL = "Select ID from EventLogInfo where UserID=" + UserID + " and ModulName='" + ModulName + "' and RowStatus=1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSPP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["ID"]);
                }
            }
            return 0;
        }
        public int ShowModulInfoUpdate(string ModulName)
        {
            string strSQL = "Select InfoStatus From EventLogModul where ModulName='" + ModulName + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSPP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["ID"]);
                }
            }
            return 0;
        }
        /**
         * Added on 16-07-2014
         * For Pemantaun SPP
         */
        public ArrayList TahunSPP()
        {
            string strSQL = "Select Year(CreatedTime) as Tahun from SPP group by Year(CreatedTime) order by Year(CreatedTime) Desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSPP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSPP.Add(new SPP { ID = Convert.ToInt32(sqlDataReader["Tahun"].ToString()) });
                }
            }
            else
                arrSPP.Add(new SPP());

            return arrSPP;
        }
        public int  GetLockingATK(int userid,string itemid)
        {
            string strSQL = "declare @userid int " +
               " declare @itemid int " +
               " declare @budget int " +
               " declare @locking int " +
               " declare @dept varchar(3) " +
               " set @userid=" + userid + " " +
               " set @itemid=" + itemid + " " +
               " set @budget=(select COUNT(itemid) from BudgetATKMaster where RowStatus>-1 and itemid=@itemid and tahun =year(GETDATE())) " +
               " if @budget>0  " +
               " begin " +
	           "     set @dept=( select deptname from Dept where ID in (select deptid from Users where ID=@userid)) " +
	           "     if @dept like '%log%' " +
		       "         set @locking=0 " +
	           "     else " +
		       "         set @locking=1 " +
               " end " +
               " else " +
               " begin " +
	           "     set @dept=( select deptname from Dept where ID in (select deptid from Users where ID=@userid)) " +
	           "     if @dept  like '%hrd%' or @dept  like '%iso%'  " +
		       "         set @locking=0 " +
	           "     else " +
		       "         set @locking=1 " +
               " end " +
               " declare @locking1 int " +
               " declare @username varchar(max) " +
               " set @username=(select RTRIM(username) from Users where ID=@userid) " +
               " if @username like '%iso-%' or @username like '%it-%' " +
	           "     set @locking1=0 " +
               " else " +
               "     set @locking1=1 " +
               " if @locking=1 and @locking1=0 set @locking=0 else set  @locking=@locking" + 
               " select @locking locking";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            int locking = 0;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    locking = Convert.ToInt32(sqlDataReader["locking"].ToString());
                }
            }

            return locking;
        }
        public int GetUserMTC(string  username)
        {
            string strSQL = 
            //" select count(id)usercount from Users where UserName='" + username + "' and  RowStatus>-1 and " +
            //" DeptID in (select ID from Dept where Alias='maintenance')";
            " select count(id)usercount from Users where UserName='" + username + "' and  RowStatus>-1 and DeptID in " +
            " (select ID from Dept where Alias='maintenance' and ID not in (19) and RowStatus>-1) ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            int locking = 0;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    locking = Convert.ToInt32(sqlDataReader["usercount"].ToString());
                }
            }

            return locking;
        }
        public ArrayList RetrieveSPP1(int Tahun, int Bulan, int Tipe, int GroupID)
        {

            string strSQL = QueryPemantauanSPP1(Tahun, Bulan, Tipe, GroupID);
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSPP = new ArrayList();
            int n = 0;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    n = n + 1;
                    arrSPP.Add(new SPPPantau
                    {
                        Nom = n,
                        ID = Convert.ToInt32(sqlDataReader["ID"].ToString()),
                        TglSPP = sqlDataReader["TglSPP"].ToString(),
                        NoSPP = sqlDataReader["NoSpp"].ToString(),
                        TipeSPP = sqlDataReader["SppTipe"].ToString(),
                        ApvStatus = sqlDataReader["Apv"].ToString(),
                        ApvDate = sqlDataReader["ApvDate"].ToString(),
                        ItemCode = sqlDataReader["ItemCode"].ToString(),
                        ItemName = sqlDataReader["ItemName"].ToString(),
                        Uom = sqlDataReader["Uom"].ToString(),
                        Qty = Convert.ToDecimal(sqlDataReader["Quantity"].ToString())
                    });
                }
            }
            else
            {
                arrSPP.Add(new SPPPantau());
            }
            return arrSPP;
        }

        //Edit Agus 21-07-2021
        public ArrayList RetrieveSPP(int Tipe, int GroupID, int Criteria, string Kode, string Tdari, string Tsampai)
        {

            string strSQL = QueryPemantauanSPP(Tipe, GroupID, Criteria, Kode, Tdari, Tsampai);
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSPP = new ArrayList();
            int n = 0;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    n = n + 1;
                    arrSPP.Add(new SPPPantau
                    {
                        Nom = n,
                        ID = Convert.ToInt32(sqlDataReader["ID"].ToString()),
                        TglSPP = sqlDataReader["TglSPP"].ToString(),
                        UserAlias = sqlDataReader["UserAlias"].ToString(),
                        DeptName = sqlDataReader["DeptName"].ToString(),
                        NoSPP = sqlDataReader["NoSPP"].ToString(),
                        TipeSPP = sqlDataReader["SPP Tipe"].ToString(),
                        ApvStatus = sqlDataReader["Apv"].ToString(),
                        ApvDate = sqlDataReader["ApvDate"].ToString(),
                        ItemCode = sqlDataReader["ItemCode"].ToString(),
                        ItemName = sqlDataReader["ItemName"].ToString(),
                        Uom = sqlDataReader["Uom"].ToString(),
                        HargaPo = Convert.ToDecimal(sqlDataReader["Price"].ToString()),
                        QtySPP = Convert.ToDecimal(sqlDataReader["QtySPP"].ToString()),
                        PurchnDate = sqlDataReader["TglPO"].ToString(),
                        NopemantauanPO = sqlDataReader["NoPO"].ToString(),
                        ApprovalPo = sqlDataReader["PoApp"].ToString(),
                        TglReceipt = sqlDataReader["TglReceipt"].ToString(),
                        NoReceipt = sqlDataReader["NoReceipt"].ToString(),
                        QtyReceipt = Convert.ToDecimal(sqlDataReader["QtyReceipt"].ToString()),
                        QtyPO2 = Convert.ToDecimal(sqlDataReader["Qty"].ToString())

                    });
                }
            }
            else
            {
                arrSPP.Add(new SPPPantau());
            }
            return arrSPP;
        }
        //Edit Agus 21-07-2021

        public ArrayList RetrievePO(int Tahun, int Bulan, int Tipe, int ID, int GroupID)
        {
            string strSQL = QueryPemantauanPO(Tahun, Bulan, Tipe, ID, GroupID);
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSPP = new ArrayList();
            int n = 0;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    if (sqlDataReader["PoDate"].ToString() != string.Empty)
                    {
                        n = n + 1;
                        arrSPP.Add(new SPPPantau
                        {
                            Nom = n,
                            ApvStatus = sqlDataReader["PoApp"].ToString(),
                            ApvDate = sqlDataReader["PoDate"].ToString(),
                            DlvDate = sqlDataReader["DeliveryDate"].ToString(),
                            NoPO = sqlDataReader["NoPO"].ToString(),
                            Qty = Convert.ToDecimal(sqlDataReader["Qty"].ToString())
                        });
                    }
                }
            }
            else
            {
                arrSPP.Add(new SPPPantau());
            }
            return arrSPP;
        }
        public ArrayList RetrieveRMS(int Tahun, int Bulan, int Tipe, int ID, int GroupID)
        {
            string strSQL = QueryPemantauanRMS(Tahun, Bulan, Tipe, ID, GroupID);
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSPP = new ArrayList();
            int n = 0;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    if (sqlDataReader["ReceiptDate"].ToString() != string.Empty)
                    {
                        n = n + 1;
                        arrSPP.Add(new SPPPantau
                        {
                            Nom = n,

                            ApvDate = sqlDataReader["ReceiptDate"].ToString(),
                            NoPO = sqlDataReader["ReceiptNo"].ToString(),
                            Qty = Convert.ToDecimal(sqlDataReader["rQty"].ToString())
                        });
                    }
                }
            }
            else
            {
                arrSPP.Add(new SPPPantau());
            }
            return arrSPP;
        }
        public string QueryPemantauan(int Tahun, int Bulan, int Tipe, int GroupID)
        {
            string purchn = string.Empty;
            purchn = (GroupID == 0) ? "" : " and SPPDetail.GroupID=" + GroupID;
            return "select SPPDetail.ID,(Select Convert(varchar,CreatedTime,105) from SPP where ID=SPPDetail.SppID) TglSPP, " +
                   "(select NoSPP from SPP where ID=SPPDetail.SPPID) NoSPP, " +
                   "(select Case SPP.PermintaanType when 1 then 'Urgent' when 2 then 'Biasa' when 3 then 'Sesuai Sch' end from SPP where ID=SPPDetail.SPPID) SppTipe, " +
                   "(select Case Approval When 0 then 'Open' when 1 then 'Head' when 2 then 'Mgr' when 3 then 'PM' end From SPP where ID=SPPDetail.SPPID) Apv, " +
                   "(select Convert(varchar,SPP.ApproveDate3,105) From SPP where ID=SPPDetail.SPPID) ApvDate, " +
                   "SPPDetail.GroupID,SPPDetail.ItemTypeID, " +
                   "Case SPPDetail.ItemTypeID  " +
                   "     when 1 then " +
                   "     (Select ItemCode From Inventory Where ID=SPPDetail.ItemID) " +
                   "     when 2 then   " +
                   "     (Select ItemCode From Asset Where ID=SPPDetail.ItemID) " +
                   "     when 3 then " +
                   "     (Select ItemCode From Biaya Where ID=SPPDetail.ItemID) " +
                   "     end ItemCode, " +
                   "Case SPPDetail.ItemTypeID  " +
                   "     when 1 then " +
                   "     (Select ItemName From Inventory Where ID=SPPDetail.ItemID) " +
                   "     when 2 then   " +
                   "     (Select ItemName From Asset Where ID=SPPDetail.ItemID) " +
                   "     when 3 then " +
                   "         case when (isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())<= " +
                   "         (Select CreatedTime from SPP where SPP.ID=SPPDetail.SPPID)) Then " +
                   "         (Select ItemName From Biaya Where ID=SPPDetail.ItemID)+' - '+ UPPER(SPPDetail.Keterangan) " +
                   "         else " +
                   "         (Select ItemName From Biaya Where ID=SPPDetail.ItemID) " +
                   "         End " +
                   "     end ItemName, " +
                   " SPPDetail.Quantity,(Select UOMCode From UOM where ID=SPPDetail.UOMID)Uom,POPurchn.NoPO,Convert(varchar,POPurchn.POPurchnDate,105) as PoDate, " +
                   " case POPurchn.Approval when 0 then 'Open' when 1 then 'Head' when 2 then 'Mgr' end PoApp, " +
                   " Convert(varchar,POPurchnDetail.DlvDate,105) as DeliveryDate, " +
                   " POPurchnDetail.Qty,Receipt.ReceiptNo, " +
                   " Convert(varchar,Receipt.ReceiptDate,105) as ReceiptDate,ReceiptDetail.Quantity as rQty " +
                   " from SPPDetail  " +
                   " left Join POPurchnDetail " +
                   " on POPurchnDetail.SppDetailID=SPPDetail.ID and POPurchnDetail.Status>-1 " +
                   " Left Join POPurchn " +
                   " on POPurchn.ID=POPurchnDetail.POID and POPurchn.Status>-1 " +
                   " Left Join ReceiptDetail " +
                   " on ReceiptDetail.PODetailID=POPurchnDetail.ID and ReceiptDetail.RowStatus>-1 " +
                   " left Join Receipt " +
                   " on Receipt.ID=ReceiptDetail.ReceiptID and Receipt.Status>-1 " +
                   " where SPPDetail.SPPID in(select ID from SPP where YEAR(CreatedTime)=" + Tahun + " and MONTH(createdTime)=" + Bulan +
                   " and SPP.Status>-1 and SPP.ItemTypeID=" + Tipe + ") " +
                   " and SPPDetail.Status>-1  " + purchn +
                   " order by SPPDetail.SPPID ";
        }

        //Edit Agus 21-07-2021
        private string QueryPemantauanSPP(int Tipe, int GroupID, int Criteria, string Kode, string TglDari, string TglSampai)
        {
            string purchn = string.Empty;
            purchn = (GroupID == 0) ? "" : " and SPP.GroupID =" + GroupID;
            return "select * from ( " +
                    "select SPP.ID, Convert(varchar,SPP.Minta,105) as 'TglSPP',Users.UserAlias,Dept.DeptName,SPP.NoSPP, " +

                    "case SPP.PermintaanType " +
                    "when 1 then 'Urgent' " +
                    "when 2 then 'Biasa' " +
                    "when 3 then 'Sesuai Sch' end as 'SPP Tipe', " +

                    "case SPP.Approval " +
                    "when 0 then 'Open' " +
                    "when 1 then 'Head' " +
                    "when 2 then 'Mgr' " +
                    "when 3 then 'PM' end as 'Apv',  Convert(varchar,SPP.ApproveDate3,105) as 'ApvDate', " +

                    "CASE SPPDetail.ItemTypeID " +
                    "when 1 then (select ItemCode  from Inventory where ID =SPPDetail.ItemID ) " +
                    "when 2 then (select ItemCode  from asset where ID =SPPDetail.ItemID ) " +
                    "when 3 then (select ItemCode  from biaya where ID =SPPDetail.ItemID ) end ItemCode, " +

                    "case SPPDetail.ItemTypeID " +
                    "when 1 then (Select ItemName From Inventory Where ID=SPPDetail.ItemID) " +
                    "when 2 then (Select ItemName From Asset Where ID=SPPDetail.ItemID) " +
                    "when 3 then CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE()) <  (Select Minta from SPP where SPP.ID=POPurchnDetail.SPPID)) " +
                    "THEN(select ItemName from Biaya where ID=POPurchnDetail.ItemID and Biaya.RowStatus>-1)+' - '+ " +
                    "(Select SPPDetail.Keterangan From SPPDetail where SPPDetail.ID=POPurchnDetail.SPPDetailID) ELSE " +
                    "(select ItemName from biaya where ID=SPPDetail.ItemID and biaya.RowStatus>-1) END  end ItemName, " +
                    "SPPDetail.Quantity as QtySPP,uom.UOMDesc as 'Uom',Convert(varchar,POPurchn.POPurchnDate,105) as TglPO,POPurchn.NoPO, " +

                    "case isnull(convert(varchar,POPurchn.Approval,105),'') " +
                    "when '' then ''  " +
                    "when 0 then 'Open'  " +
                    "when 1 then 'Head' " +
                    "when 2 then 'Mgr' end PoApp, " +
                    "isnull(POPurchnDetail.Qty,0)Qty,isnull(POPurchnDetail.Price,0)Price,isnull(Convert(varchar,Receipt.ReceiptDate,105),'') TglReceipt,isnull(Receipt.ReceiptNo,'') NoReceipt, isnull(ReceiptDetail.Quantity ,0)QtyReceipt " +

                    "FROM SPPDetail " +
                    "LEFT JOIN SPP ON SPPDetail.SPPID= SPP.ID " +
                    "LEFT JOIN Users ON SPP.UserID = Users.ID " +
                    "LEFT JOIN Dept ON Users.DeptID = Dept.ID " +
                    "LEFT JOIN UOM ON SPPDetail.UOMID = UOM.ID " +

                    "LEFT JOIN POPurchnDetail ON  SPPDetail.ID=POPurchnDetail.SPPDetailID AND popurchndetail.Status >-1 " +
                    
                    "LEFT JOIN POPurchn ON POPurchn.ID=POPurchnDetail.POID  and POPurchn.Status>-1 " +
                    "LEFT JOIN ReceiptDetail ON ReceiptDetail.PODetailID=POPurchnDetail.ID and ReceiptDetail.RowStatus>-1 " +
                    "LEFT JOIN Receipt on Receipt.ID=ReceiptDetail.ReceiptID and Receipt.Status>-1 where " +

                    "SPP.Minta between '" + TglDari + "' and '" + TglSampai + "' and SPPDetail.ItemTypeID=" + Tipe + " and  " +
                    "SPPDetail.Status>-1 and spp.Status >-1 and UOM.RowStatus >-1 and Users.RowStatus >-1 " + purchn + ") A1  where " +
                    "case " + Criteria + " " +
                    "when 1 then ItemCode " +
                    "when 2 then ItemName " +
                    "when 3 then NoPO " +
                    "when 4 then NoSPP " +
                    "end like '%" + Kode + "%' ";
        }
        //Edit Agus 21-07-2021

        private string QueryPemantauanSPP1(int Tahun, int Bulan, int Tipe, int GroupID)
        {
            string purchn = string.Empty;
            purchn = (GroupID == 0) ? "" : " and SPPDetail.GroupID=" + GroupID;
            return "select SPPDetail.ID,(Select Convert(varchar,CreatedTime,105) from SPP where ID=SPPDetail.SppID) TglSPP, " +
                    "(select NoSPP from SPP where ID=SPPDetail.SPPID) NoSPP,  " +
                    "(select Case SPP.PermintaanType when 1 then 'Urgent' when 2 then 'Biasa' when 3 then 'Sesuai Sch' end from SPP where ID=SPPDetail.SPPID) SppTipe,  " +
                    "(select Case Approval When 0 then 'Open' when 1 then 'Head' when 2 then 'Mgr' when 3 then 'PM' end From SPP where ID=SPPDetail.SPPID) Apv,  " +
                    "(select Convert(varchar,SPP.ApproveDate3,105) From SPP where ID=SPPDetail.SPPID) ApvDate, SPPDetail.GroupID,SPPDetail.ItemTypeID,  " +
                    "(SELECT dbo.ItemCodeInv(SPPDetail.ItemID,SPPDetail.ItemTypeID))ItemCode, " +
                    "(SELECT dbo.ItemNameInv(SPPDetail.ItemID,SPPDetail.ItemTypeID))ItemName, " +
                    "SPPDetail.Quantity, " +
                    "(Select UOMCode From UOM where ID=SPPDetail.UOMID)Uom " +
                    "FROM SPPDetail " +
                    "where SPPDetail.SPPID in(select ID from SPP where YEAR(CreatedTime)=" + Tahun + " and MONTH(createdTime)=" + Bulan + " and SPP.Status>-1 " +
                    "and SPP.ItemTypeID=" + Tipe + ") " +
                    "and SPPDetail.Status>-1 " + purchn +
                    "order by SPPDetail.SPPID ";
        }
        private string QueryPemantauanPO(int Tahun, int Bulan, int Tipe, int SppDetailID, int GroupID)
        {
            string purchn = string.Empty;
            purchn = (GroupID == 0) ? "" : " and SPPDetail.GroupID=" + GroupID;
            return "Select PODate,NoPO,Qty,PoApp,DeliveryDate From( " +
                   "select SPPDetail.ID,(Select Convert(varchar,CreatedTime,105) from SPP where ID=SPPDetail.SppID) TglSPP, " +
                   "(select NoSPP from SPP where ID=SPPDetail.SPPID) NoSPP, " +
                   "(select Case SPP.PermintaanType when 1 then 'Urgent' when 2 then 'Biasa' when 3 then 'Sesuai Sch' end from SPP where ID=SPPDetail.SPPID) SppTipe, " +
                   "(select Case Approval When 0 then 'Open' when 1 then 'Head' when 2 then 'Mgr' end From SPP where ID=SPPDetail.SPPID) Apv, " +
                   "(select Convert(varchar,SPP.ApproveDate3,105) From SPP where ID=SPPDetail.SPPID) ApvDate, " +
                   "SPPDetail.GroupID,SPPDetail.ItemTypeID, " +
                   "Case SPPDetail.ItemTypeID  " +
                   "     when 1 then " +
                   "     (Select ItemCode From Inventory Where ID=SPPDetail.ItemID) " +
                   "     when 2 then   " +
                   "     (Select ItemCode From Asset Where ID=SPPDetail.ItemID) " +
                   "     when 3 then " +
                   "     (Select ItemCode From Biaya Where ID=SPPDetail.ItemID) " +
                   "     end ItemCode, " +
                   "Case SPPDetail.ItemTypeID  " +
                   "     when 1 then " +
                   "     (Select ItemName From Inventory Where ID=SPPDetail.ItemID) " +
                   "     when 2 then   " +
                   "     (Select ItemName From Asset Where ID=SPPDetail.ItemID) " +
                   "     when 3 then " +
                   "         case when (isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())<= " +
                   "         (Select CreatedTime from SPP where SPP.ID=SPPDetail.SPPID)) Then " +
                   "         (Select ItemName From Biaya Where ID=SPPDetail.ItemID)+' - '+ UPPER(SPPDetail.Keterangan) " +
                   "         else " +
                   "         (Select ItemName From Biaya Where ID=SPPDetail.ItemID) " +
                   "         End " +
                   "     end ItemName, " +
                   " SPPDetail.Quantity,(Select UOMCode From UOM where ID=SPPDetail.UOMID)Uom,POPurchn.NoPO,Convert(varchar,POPurchn.POPurchnDate,105) as PoDate, " +
                   " case POPurchn.Approval when 0 then 'Open' when 1 then 'Head' when 2 then 'Mgr' end PoApp, " +
                   " Convert(varchar,POPurchnDetail.DlvDate,105) as DeliveryDate, " +
                   " POPurchnDetail.Qty,Receipt.ReceiptNo, " +
                   " Convert(varchar,Receipt.ReceiptDate,105) as ReceiptDate,ReceiptDetail.Quantity as rQty " +
                   " from SPPDetail  " +
                   " left Join POPurchnDetail " +
                   " on POPurchnDetail.SppDetailID=SPPDetail.ID and POPurchnDetail.Status>-1 " +
                   " Left Join POPurchn " +
                   " on POPurchn.ID=POPurchnDetail.POID and POPurchn.Status>-1 " +
                   " Left Join ReceiptDetail " +
                   " on ReceiptDetail.PODetailID=POPurchnDetail.ID and ReceiptDetail.RowStatus>-1 " +
                   " left Join Receipt " +
                   " on Receipt.ID=ReceiptDetail.ReceiptID and Receipt.Status>-1 " +
                   " where SPPDetail.SPPID in(select ID from SPP where YEAR(CreatedTime)=" + Tahun + " and MONTH(createdTime)=" + Bulan +
                   " and SPP.Status>-1 and SPP.ItemTypeID=" + Tipe + ") " +
                   " and SPPDetail.Status>-1  " + purchn +
                   ") as w where w.ID=" + SppDetailID +
                   " group by NoPO,PODate,Qty,PoApp,DeliveryDate order by PoDate ";
        }
        private string QueryPemantauanRMS(int Tahun, int Bulan, int Tipe, int SppDetailID, int GroupID)
        {
            string purchn = string.Empty;
            purchn = (GroupID == 0) ? "" : " and SPPDetail.GroupID=" + GroupID;
            return "Select ReceiptNo,ReceiptDate,rQty From( " +
                   "select SPPDetail.ID,(Select Convert(varchar,CreatedTime,105) from SPP where ID=SPPDetail.SppID) TglSPP, " +
                   "(select NoSPP from SPP where ID=SPPDetail.SPPID) NoSPP, " +
                   "(select Case SPP.PermintaanType when 1 then 'Urgent' when 2 then 'Biasa' when 3 then 'Sesuai Sch' end from SPP where ID=SPPDetail.SPPID) SppTipe, " +
                   "(select Case Approval When 0 then 'Open' when 1 then 'Head' when 2 then 'Mgr' end From SPP where ID=SPPDetail.SPPID) Apv, " +
                   "(select Convert(varchar,SPP.ApproveDate3,105) From SPP where ID=SPPDetail.SPPID) ApvDate, " +
                   "SPPDetail.GroupID,SPPDetail.ItemTypeID, " +
                   "Case SPPDetail.ItemTypeID  " +
                   "     when 1 then " +
                   "     (Select ItemCode From Inventory Where ID=SPPDetail.ItemID) " +
                   "     when 2 then   " +
                   "     (Select ItemCode From Asset Where ID=SPPDetail.ItemID) " +
                   "     when 3 then " +
                   "     (Select ItemCode From Biaya Where ID=SPPDetail.ItemID) " +
                   "     end ItemCode, " +
                   "Case SPPDetail.ItemTypeID  " +
                   "     when 1 then " +
                   "     (Select ItemName From Inventory Where ID=SPPDetail.ItemID) " +
                   "     when 2 then   " +
                   "     (Select ItemName From Asset Where ID=SPPDetail.ItemID) " +
                   "     when 3 then " +
                   "         case when (isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())<= " +
                   "         (Select CreatedTime from SPP where SPP.ID=SPPDetail.SPPID)) Then " +
                   "         (Select ItemName From Biaya Where ID=SPPDetail.ItemID)+' - '+ UPPER(SPPDetail.Keterangan) " +
                   "         else " +
                   "         (Select ItemName From Biaya Where ID=SPPDetail.ItemID) " +
                   "         End " +
                   "     end ItemName, " +
                   " SPPDetail.Quantity,(Select UOMCode From UOM where ID=SPPDetail.UOMID)Uom,POPurchn.NoPO,Convert(varchar,POPurchn.POPurchnDate,105) as PoDate, " +
                   " case POPurchn.Approval when 0 then 'Open' when 1 then 'Head' when 2 then 'Mgr' end PoApp, " +
                   " Convert(varchar,POPurchnDetail.DlvDate,105) as DeliveryDate, " +
                   " POPurchnDetail.Qty,Receipt.ReceiptNo, " +
                   " Convert(varchar,Receipt.ReceiptDate,105) as ReceiptDate,ReceiptDetail.Quantity as rQty " +
                   " from SPPDetail  " +
                   " left Join POPurchnDetail " +
                   " on POPurchnDetail.SppDetailID=SPPDetail.ID and POPurchnDetail.Status>-1 " +
                   " Left Join POPurchn " +
                   " on POPurchn.ID=POPurchnDetail.POID and POPurchn.Status>-1 " +
                   " Left Join ReceiptDetail " +
                   " on ReceiptDetail.PODetailID=POPurchnDetail.ID and ReceiptDetail.RowStatus>-1 " +
                   " left Join Receipt " +
                   " on Receipt.ID=ReceiptDetail.ReceiptID and Receipt.Status>-1 " +
                   " where SPPDetail.SPPID in(select ID from SPP where YEAR(CreatedTime)=" + Tahun + " and MONTH(createdTime)=" + Bulan +
                   " and SPP.Status>-1 and SPP.ItemTypeID=" + Tipe + ") " +
                   " and SPPDetail.Status>-1  " + purchn +
                   ") as w where w.ID=" + SppDetailID +
                   " group by ReceiptNo,ReceiptDate,rQty order by w.ReceiptNo ";
        }

        public ArrayList ListSPP(string Approval)
        {
            string where = (HttpContext.Current.Session["status"] == null) ? " ) as x where x.Detail > 0" : HttpContext.Current.Session["status"].ToString();
            string strSQL = "Select * from (select s.ID,s.NoSPP,s.Minta,s.GroupID,s.ItemTypeID,s.CreatedBy,s.ApproveDate3,u.UserName," +
                            "case s.PermintaanType when 1 then 'Top Urgent' when 2 then 'Biasa' when 3 then 'Sesuai Schedule' end MintaTipe, " +
                            "(select COUNT(ID) from SPPDetail where SPPID=s.ID and Status>-1 and Quantity >QtyPO) Detail,Approval,Status " +
                            " from SPP as s LEFT JOIN Users as U " +
                            "on U.ID=s.HeadID where s.Approval =" + Approval + where;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSPP = new ArrayList();
            if (strError == string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSPP.Add(new SPP
                    {
                        ID = Convert.ToInt32(sqlDataReader["ID"].ToString()),
                        NoSPP = sqlDataReader["NoSPP"].ToString(),
                        Minta = Convert.ToDateTime(sqlDataReader["Minta"].ToString()),
                        HeadName = sqlDataReader["UserName"].ToString(),
                        Gudang = sqlDataReader["MintaTipe"].ToString(),
                        ApproveDate3 = Convert.ToDateTime(sqlDataReader["ApproveDate3"].ToString())
                    });
                }
            }
            return arrSPP;
        }
        public ArrayList ListDetailSPP(int SPPID)
        {
            string strSQL = "select ID, SPPID," +
                          "  Case ItemTypeID  " +
                          "      when 1 then (Select ItemCode from Inventory where ID=ItemID) " +
                          "      when 2 then (Select ItemCode from Asset where ID=ItemID) " +
                          "      when 3 then (select ItemCode from Biaya where ID=ItemID)  " +
                          "  end ItemCode, " +
                          "  Case ItemTypeID  " +
                          "      when 1 then (Select ItemName from Inventory where ID=ItemID) " +
                          "      when 2 then (Select ItemName from Asset where ID=ItemID) " +
                          "      when 3 then " + ItemSPPBiayaNew("SPPDetail") +
                          "  end ItemName, " +
                          "  Case ItemTypeID  " +
                          "      when 1 then (Select isnull(LeadTime,0) from Inventory where ID=ItemID) " +
                          "      when 2 then (Select isnull(LeadTime,0) from Asset where ID=ItemID) " +
                          "      when 3 then (select isnull(LeadTime,0) from Biaya where ID=ItemID)  " +
                          "  end LeadTime, " +
                          "  (Select UomCode from UOM where ID=UOMID)Satuan,UomID, " +
                          "  Quantity,QtyPO,Status,Keterangan,tglkirim,isnull(PendingPO,0)PendingPO,isnull(AlasanPending,'')AlasanPending " +
                          " from SPPDetail where SPPID=" + SPPID + " order by ID desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSPP = new ArrayList();
            if (strError == string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSPP.Add(new SPPDetail
                    {
                        ID = Convert.ToInt32(sqlDataReader["ID"].ToString()),
                        SPPID = Convert.ToInt32(sqlDataReader["SPPID"].ToString()),
                        ItemCode = sqlDataReader["ItemCode"].ToString(),
                        ItemName = sqlDataReader["ItemName"].ToString(),
                        Satuan = sqlDataReader["Satuan"].ToString(),
                        UOMID = Convert.ToInt32(sqlDataReader["UomID"].ToString()),
                        Quantity = Convert.ToDecimal(sqlDataReader["Quantity"].ToString()),
                        Status = Convert.ToInt32(sqlDataReader["Status"].ToString()),
                        QtyPO = Convert.ToDecimal(sqlDataReader["QtyPO"].ToString()),
                        PendingPO = Convert.ToInt32(sqlDataReader["PendingPO"].ToString()),
                        AlasanPending = sqlDataReader["AlasanPending"].ToString(),
                        TglKirim = Convert.ToDateTime(sqlDataReader["tglkirim"].ToString()),
                        ItemTypeID = Convert.ToInt32(sqlDataReader["LeadTime"].ToString())
                    });
                }
            }
            return arrSPP;
        }
        public DateTime ApprovalSPP(int SPPID)
        {
            DateTime apvDate = new DateTime();
            //string strSQL = "Select ApproveDate3";

            return apvDate;
        }
        public string PICMinMax(int ItemID)
        {
            string result = string.Empty;
            string strSQL = "Select PICMinMax from BudgetSP where ItemID=" + ItemID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = sdr["PICMinMax"].ToString();
                }
            }
            return result;
        }
        public ArrayList ListUserHead()
        {
            arrSPP = new ArrayList();
            string strSQL = "Select * From ListUserHead WHERE RowStatus>-1";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrSPP.Add(ObjectUserHead(sdr));
                }
            }
            return arrSPP;
        }
        public ArrayList ListUserHead(int UserID)
        {
            arrSPP = new ArrayList();
            string strSQL = "Select * From ListUserHead WHERE RowStatus>-1 and HeadID=" + UserID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrSPP.Add(ObjectUserHead(sdr));
                }
            }
            return arrSPP;
        }
        public ArrayList ListUserHead(int UserID, bool Manager)
        {
            arrSPP = new ArrayList();
            string strSQL = (Manager == true) ? 
                "Select ID,UserID,HeadID,ManagerID,Keterangan,ISNULL(MgrID,0)MgrID,RowStatus From ListUserHead WHERE RowStatus>-1 and MgrID=" + UserID :
                "Select * From ListUserHead WHERE RowStatus>-1 and ManagerID=" + UserID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrSPP.Add(ObjectUserHead(sdr));
                }
            }
            return arrSPP;
        }
        private object ObjectUserHead(SqlDataReader sdr)
        {
            SPPHead spph = new SPPHead();
            spph.UserID = int.Parse(sdr["UserID"].ToString());
            spph.HeadID = int.Parse(sdr["HeadID"].ToString());
            spph.ManagerID = int.Parse(sdr["MgrID"].ToString());
            spph.PlantMgrID = int.Parse(sdr["ManagerID"].ToString());
            return spph;
        }

        private string QueryOutSPPNew(string drTgl, string sdTgl, int groupID)
        {
            string strGroupID = string.Empty;
            if (groupID > 0)
                strGroupID = " and B.GroupId =" + groupID;
            else
                strGroupID = " ";
            return " with R As " +
            " ( " +
               " select A.ID,A.NoSPP,convert(varchar,A.Minta,103) as TglSPP,convert(varchar,A.lastmodifiedtime,103) as ApvDate, " +
               " DATEDIFF(D,LastModifiedTime,GETDATE())-DATEDIFF(WK,LastModifiedTime,GETDATE())*2-(select COUNT(ID) from Holidays where HolidayDate >= A.LastModifiedTime and HolidayDate <=GETDATE()) as Selisih," +
               " case B.ItemTypeID when 1 then ISNULL((select Leadtime from Inventory where B.ItemID=Inventory.ID),0) " +
               " when 2 then ISNULL((select Leadtime from Asset where B.ItemID=Asset.ID),0) " +
               " when 3 then ISNULL((select Leadtime from Biaya where B.ItemID=Biaya.ID),0) " +
               " else '' end LeadTime, " +
               " B.Quantity,B.QtyPO,B.Quantity-B.QtyPO as QtySisa, " +
               " case B.ItemTypeID when 1 then (select ItemName from Inventory where B.ItemID=Inventory.ID) " +
               " when 2 then (select ItemName from Asset where B.ItemID=Asset.ID) " +
               " when 3 then (select ItemName from Biaya where B.ItemID=Biaya.ID)+' - '+ B.Keterangan " +
               " else '' end ItemName, " +
               " case B.ItemTypeID when 1 then (select ItemCode from Inventory where B.ItemID=Inventory.ID) " +
               " when 2 then (select ItemCode from Asset where B.ItemID=Asset.ID) " +
               " when 3 then (select ItemCode from Biaya where B.ItemID=Biaya.ID) " +
               " else '' end ItemCode, " +
               " case when A.HeadID >=0 then(select username from Users where ID=A.HeadID) end NamaHead,case permintaantype " +
               " when 1 then 'Top Urgent' when 2 then 'Biasa' when 3 then 'SesuaiSchedule' end Prioritas, " +
               " case when B.uomid>0 then (select uomcode from uom where id=B.uomid)end Uom, convert(varchar,A.createdtime,103) as createdtime, " +
               " case when B.ItemTypeID=3 Then B.Keterangan1 else B.Keterangan end as Keterangan " +
               " from SPP as A, SPPDetail as B where A.ID=B.SPPID and " +
               " A.Status>-1 and B.Status>-1  and B.Quantity-B.QtyPO > 0 and A.approval=3 " +
               " and convert(varchar,A.createdtime,112) >='" + drTgl + "' and convert(varchar,A.createdtime,112)<='" + sdTgl + "' " + strGroupID + " " +
            " ) " +
            " Select ID,NoSPP,TglSPP,ApvDate,Selisih,LeadTime,Quantity,QtyPO,QtySisa,ItemCode,ItemName,NamaHead,Prioritas,Uom,createdtime From R where Selisih > LeadTime order by NoSPP "; 
        }

        public ArrayList RetrieveOSNew(string drTgl, string sdTgl, int groupID)
        {
            string strSQL = QueryOutSPPNew(drTgl, sdTgl, groupID);
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSPP = new ArrayList();
            int n = 0;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    n = n + 1;
                    arrSPP.Add(new SPPPantau
                    {
                        Nom = n,
                        ID = Convert.ToInt32(sqlDataReader["ID"].ToString()),
                        NoSPP = sqlDataReader["NoSpp"].ToString(),
                        TglSPP = sqlDataReader["TglSPP"].ToString(),
                        ApvDate = sqlDataReader["ApvDate"].ToString(),
                        Selisih = sqlDataReader["Selisih"].ToString(),
                        LeadTime = sqlDataReader["LeadTime"].ToString(),
                        Quantity = Convert.ToDecimal(sqlDataReader["Quantity"].ToString()),
                        QtyPO = Convert.ToDecimal(sqlDataReader["QtyPO"].ToString()),
                        ItemCode = sqlDataReader["ItemCode"].ToString(),
                        ItemName = sqlDataReader["ItemName"].ToString(),
                        Uom = sqlDataReader["Uom"].ToString(),
                        NamaHead = sqlDataReader["NamaHead"].ToString(),
                        Prioritas = sqlDataReader["Prioritas"].ToString(),
                        CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"].ToString())
                        //TipeSPP = sqlDataReader["SppTipe"].ToString(),
                        //ApvStatus = sqlDataReader["Apv"].ToString(),
                        //ApvDate = sqlDataReader["ApvDate"].ToString(),
                        
                        //Qty = Convert.ToDecimal(sqlDataReader["Quantity"].ToString())
                    });
                }
            }
            else
            {
                arrSPP.Add(new SPPPantau());
            }
            return arrSPP;
        }

        private string QueryPemantauanSPPx(int Tahun, int Bulan, int Tipe)
        {
            string purchn = string.Empty;
            purchn = (Tipe == 0) ? "and spp.GroupID in (8,9)" : " and spp.GroupID= " + Tipe;
            return " select data1.ItemCode,data1.ItemName,data1.UOMCode,data1.Quantity Qty,data1.NoSPP,data1.CreatedBy from ( " +
                   " SELECT SPP.NoSPP,SPP.CreatedBy,SPP.UserID,(Select DeptID from Users where ID=Spp.UserID)DeptID, " +
                   " case SPP.Approval when 0 then 'Open' " +
                   " when 1 then 'Head' " +
                   " when 2 then 'Manager' " +
                   " when 3 then 'Plant Manager' " +
                   " when 4 then 'Purchasing' end Approval, " +
                   " CONVERT(varchar,SPP.ApproveDate1,103) as TglApprove,CONVERT(varchar,SPP.LastModifiedTime,113) as LastModified, " +
                   " case SPPDetail.ItemTypeID when 1  then (select ItemName from Inventory where ID=SPPDetail.ItemID and RowStatus > -1)  " +
                   " when 2 then (select ItemName from Asset where ID=SPPDetail.ItemID and RowStatus > -1) " +
                   " else (select ItemName from Biaya where ID=SPPDetail.ItemID and RowStatus > -1)+' - '+ SPPDetail.Keterangan end ItemName," +
                   " (select ItemTypeID from Inventory where ID=SPPDetail.ItemID)ItemTypeID," +
                   " case SPPDetail.ItemTypeID when 1  then (select ItemCode from Inventory where ID=SPPDetail.ItemID and RowStatus > -1)" +
                   " when 2 then (select ItemCode from Asset where ID=SPPDetail.ItemID and RowStatus > -1) " +
                   " else (select ItemCode from Biaya where ID=SPPDetail.ItemID and RowStatus > -1) end ItemCode, " +
                   " SPPDetail.Quantity, SPPDetail.Quantity - SPPDetail.QtyPO AS SISA, UOM.UOMCode, " +
                   " Case when SPPDetail.ItemTypeID<>3 Then SPPDetail.Keterangan ELSE isnull(SPPDetail.Keterangan1,'-') END as Keterangan," +
                   " (select Stock from Inventory where ID=SPPDetail.ItemID)Stock," +
                   " (select GroupID from Inventory where ID=SPPDetail.ItemID)Gid, " +
                   " CONVERT(varchar,SPP.CreatedTime,103) as Minta  FROM SPP INNER JOIN SPPDetail ON SPP.ID = SPPDetail.SPPID and SPP.Status>-1 and SPPDetail.Status>-1 INNER JOIN UOM ON SPPDetail.UOMID = UOM.ID " +
                   " where Month(SPP.CreatedTime)=' " + Bulan + " ' and Year(SPP.CreatedTime)=' " + Tahun + " ' and spp.ItemTypeID=1 " + purchn +
                   " ) data1 where Stock=0 order By ItemName desc";
        }


        public ArrayList RetrieveSPPx(int Tahun, int Bulan, int Tipe)
        {
            string strSQL = QueryPemantauanSPPx(Tahun, Bulan, Tipe);
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrSPP = new ArrayList();
            int n = 0;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    n = n + 1;
                    arrSPP.Add(new SPPPantau
                    {
                        Nom = n,
                        //ID = Convert.ToInt32(sqlDataReader["ID"].ToString()),
                        ItemCode = sqlDataReader["ItemCode"].ToString(),
                        ItemName = sqlDataReader["ItemName"].ToString(),
                        UOMCode = sqlDataReader["UOMCode"].ToString(),
                        Qty = Convert.ToDecimal(sqlDataReader["Qty"].ToString()),
                        NoSPP = sqlDataReader["NoSpp"].ToString(),
                        CreatedBy = sqlDataReader["CreatedBy"].ToString()

                    });
                }
            }
            else
            {
                arrSPP.Add(new SPPPantau());
            }
            return arrSPP;
        }

        
    }
}
