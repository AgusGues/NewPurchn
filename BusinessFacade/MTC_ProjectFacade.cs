using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;
using System.Web;
using System.Web.UI;
using System.Web.SessionState;

namespace BusinessFacade
{
    public class MTC_ProjectFacade : AbstractFacade, IReadOnlySessionState, IRequiresSessionState
    {
        private MTC_Project objProject = new MTC_Project();
        private EstimasiMaterial objEsm = new EstimasiMaterial();
        private ArrayList arrProject;
        private List<SqlParameter> sqlListParam;


        public MTC_ProjectFacade()
            : base()
        {

        }
        public override int Insert(object objDomain)
        {
            try
            {
                objProject = (MTC_Project)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ProjectName", objProject.NamaProject));
                sqlListParam.Add(new SqlParameter("@FromDate", objProject.FromDate));
                sqlListParam.Add(new SqlParameter("@ToDate", objProject.ToDate));
                sqlListParam.Add(new SqlParameter("@Biaya", objProject.Biaya));
                sqlListParam.Add(new SqlParameter("@DeptID", objProject.DeptID));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objProject.CreatedBy));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertMTCProject");

                strError = dataAccess.Error;

                return intResult;
            }
            catch (Exception e)
            {
                strError = e.Message;
                return -1;
            }
        }
        //input estimasi material
        public int Insert(object objDomain, bool Material)
        {
            int result = 0;
            try
            {
                objEsm = (EstimasiMaterial)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", objEsm.ItemID));
                sqlListParam.Add(new SqlParameter("@ProjectID", objEsm.ProjectID));
                sqlListParam.Add(new SqlParameter("@Jumlah", objEsm.Jumlah));
                sqlListParam.Add(new SqlParameter("@Harga", objEsm.Harga));
                sqlListParam.Add(new SqlParameter("@Schedule", objEsm.Schedule));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objEsm.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@CreatedBy", ((Users)HttpContext.Current.Session["Users"]).UserID));
                if (Material == false)
                {
                    sqlListParam.Add(new SqlParameter("@RowStatus", objEsm.RowStatus));
                    result = dataAccess.ProcessData(sqlListParam, "spMTC_ProjectMaterial_Insert1");
                }
                else
                {
                    result = dataAccess.ProcessData(sqlListParam, "spMTC_ProjectMaterial_Insert");
                }

            }
            catch
            {
                result = -1;
            }
            return result;
        }
        //insert project name modul baru
        public int InsertNew(object objDomain)
        {
            try
            {
                objProject = (MTC_Project)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ProjectName", objProject.NamaProject));
                sqlListParam.Add(new SqlParameter("@ProjectDate", objProject.ProjectDate));
                sqlListParam.Add(new SqlParameter("@FinishDate", objProject.FinishDate));
                sqlListParam.Add(new SqlParameter("@Biaya", objProject.Biaya));
                sqlListParam.Add(new SqlParameter("@DeptID", objProject.DeptID));
                sqlListParam.Add(new SqlParameter("@Sasaran", objProject.Sasaran));
                sqlListParam.Add(new SqlParameter("@GroupID", objProject.GroupID));
                sqlListParam.Add(new SqlParameter("@ProdLine", objProject.ProdLine));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objProject.CreatedBy));
                sqlListParam.Add(new SqlParameter("@Qty", objProject.Quantity));
                sqlListParam.Add(new SqlParameter("@Uom", objProject.UOMID));
                sqlListParam.Add(new SqlParameter("@Nomor", objProject.Nomor.ToUpper()));
                sqlListParam.Add(new SqlParameter("@Approval", objProject.Approval));
                int intResult = dataAccess.ProcessData(sqlListParam, "spMTCProjectInsert");

                strError = dataAccess.Error;

                return intResult;
            }
            catch (Exception e)
            {
                strError = e.Message;
                return -1;
            }
        }
        public override int Update(object objDomain)
        {
            string model = HttpContext.Current.Session["Model"].ToString();

            try
            {
                objProject = (MTC_Project)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objProject.ID));
                sqlListParam.Add(new SqlParameter("@ProjectName", objProject.NamaProject));
                if (model == string.Empty)
                {
                    sqlListParam.Add(new SqlParameter("@FromDate", objProject.FromDate));
                    sqlListParam.Add(new SqlParameter("@ToDate", objProject.ToDate));
                }
                else
                {
                    sqlListParam.Add(new SqlParameter("@SubProject", objProject.SubProjectName));
                    sqlListParam.Add(new SqlParameter("@GroupID", objProject.GroupID));
                    sqlListParam.Add(new SqlParameter("@ProjectDate", objProject.ProjectDate));
                    sqlListParam.Add(new SqlParameter("@FinishDate", objProject.FinishDate));
                    sqlListParam.Add(new SqlParameter("@ProdLine", objProject.ProdLine));
                    sqlListParam.Add(new SqlParameter("@DeptID", objProject.DeptID));
                    sqlListParam.Add(new SqlParameter("@Qty", objProject.Quantity));
                    sqlListParam.Add(new SqlParameter("@Uom", objProject.UOMID));
                }
                sqlListParam.Add(new SqlParameter("@Biaya", objProject.Biaya));
                sqlListParam.Add(new SqlParameter("@Status", objProject.Progress));
                sqlListParam.Add(new SqlParameter("@RowStatus", objProject.RowStatus));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objProject.LastModifiedBy));
                int intResult = dataAccess.ProcessData(sqlListParam, (model == string.Empty) ? "spUpdateMTCProject" : "spMTCProjectUpdate");

                strError = dataAccess.Error;

                return intResult;
            }
            catch (Exception e)
            {
                strError = e.Message;
                return -1;
            }
        }
        //update estimasi material
        public int Update(object objDomain, bool Material)
        {
            int result = 0;
            try
            {
                objEsm = (EstimasiMaterial)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objEsm.ID));
                sqlListParam.Add(new SqlParameter("@Jumlah", objEsm.Jumlah));
                sqlListParam.Add(new SqlParameter("@Schedule", objEsm.Schedule));
                sqlListParam.Add(new SqlParameter("@Harga", objEsm.Harga));
                sqlListParam.Add(new SqlParameter("@RowStatus", objEsm.RowStatus));
                sqlListParam.Add(new SqlParameter("@CreatedBy", ((Users)HttpContext.Current.Session["Users"]).UserID));
                result = dataAccess.ProcessData(sqlListParam, "spMTC_ProjectMaterial_Update");
            }
            catch
            {
                result = -1;
            }
            return result;
        }
        //delete estimasi material
        public int Delete(object objDomain, bool Material)
        {
            int result = 0;
            objProject = (MTC_Project)objDomain;
            sqlListParam = new List<SqlParameter>();
            sqlListParam.Add(new SqlParameter("@ID", objProject.ID));
            sqlListParam.Add(new SqlParameter("@AktualFinish", objProject.FinishDate));
            sqlListParam.Add(new SqlParameter("@RowStatus", objProject.RowStatus));
            sqlListParam.Add(new SqlParameter("@Status", objProject.Status));
            result = dataAccess.ProcessData(sqlListParam, "spMTC_Project_serah");
            return result;
        }
        public int InsertLog(object objDomain)
        {
            int result = 0;
            objProject = (MTC_Project)objDomain;
            sqlListParam = new List<SqlParameter>();
            sqlListParam.Add(new SqlParameter("@ProjectID", objProject.ID));
            sqlListParam.Add(new SqlParameter("@Level", objProject.Approval));
            sqlListParam.Add(new SqlParameter("@CreatedBy", objProject.CreatedBy));
            sqlListParam.Add(new SqlParameter("@Statuse", objProject.Statuse));
            sqlListParam.Add(new SqlParameter("@IPAddress", HttpContext.Current.Request.ServerVariables["remote_addr"].ToString()));
            result = dataAccess.ProcessData(sqlListParam, "spMTC_Project_log_Insert");

            return result;
        }
        public override int Delete(object objDomain)
        {
            try
            {
                objProject = (MTC_Project)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objProject.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objProject.LastModifiedBy));
                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteMTC_Project");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception e)
            {
                strError = e.Message;
                return -1;
            }
        }

        public int Approval(object objDomain)
        {
            try
            {
                objProject = (MTC_Project)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objProject.ID));
                sqlListParam.Add(new SqlParameter("@Status", objProject.Status));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objProject.LastModifiedBy));
                int intResult = dataAccess.ProcessData(sqlListParam, "spApprovalMTC_Project");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception e)
            {
                strError = e.Message;
                return -1;
            }
        }
        private string Criteria()
        {
            string strC = string.Empty;
            string Sess = HttpContext.Current.Session["StProject"].ToString();
            string ProjID = HttpContext.Current.Session["ProjectID"].ToString();
            string SubPj = HttpContext.Current.Session["SubPj"].ToString();
            string search = HttpContext.Current.Session["Search"].ToString();
            strC = (Sess == string.Empty) ? string.Empty : Sess;
            strC += (ProjID == "0" || ProjID == string.Empty) ? string.Empty : ProjID;
            strC += (SubPj == string.Empty) ? string.Empty : SubPj;
            strC += (search == string.Empty) ? string.Empty : search;

            return strC;
        }
        public override ArrayList Retrieve()
        {
            string CancelStatus = HttpContext.Current.Session["Cancel"].ToString();
            string strSQL = "Select * from MTC_Project where RowStatus >-1" + this.Criteria() + " order by ID Desc";
            //string strSQL = "Select * from MTC_Project where RowStatus >-1 and A.Approval=1 and A.Status=0 and A.RowStatus=0 "+
            //                "and DeptID="+DeptID+" order by ID Desc";
            strSQL = (CancelStatus == "yes") ? strSQL.Replace("RowStatus >-1", "RowStatus >-3") : strSQL;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrProject = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrProject.Add(GenerateObject2(sqlDataReader, GenerateObject(sqlDataReader)));
                }
            }
            else
                arrProject.Add(new MTC_Project());

            return arrProject;
        }
        public ArrayList RetrieveByCriteria(string ProjectName)
        {
            string strSQL = "Select * from MTC_Project where ProjectName='" + ProjectName + "' and SubProject is not null and SubProject !='' and RowStatus >-1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrProject = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrProject.Add(GenerateObject(sqlDataReader));
                }
            }
            //else
            //    arrProject.Add(new MTC_Project());

            return arrProject;

        }
        #region untuk MtcProject
        public ArrayList RetrieveByItemDept(int item)
        {
            string strSQL = "SELECT p.* FROM MTC_ProjectMaterial m, MTC_Project p WHERE m.ProjectID=p.ID AND m.RowStatus>-1 AND p.RowStatus>-1 AND p.Approval=2 AND m.ItemTypeID=3 AND m.ItemID=" + item;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrProject = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrProject.Add(GenerateObject2(sqlDataReader, GenerateObject(sqlDataReader)));
                }
            }
            return arrProject;
        }
        #endregion
        public ArrayList RetrieveByDept(int DeptID)
        {
            string OrderBy = HttpContext.Current.Session["Orderby"].ToString();
            OrderBy = (OrderBy == string.Empty) ? "Order by RIGHT( nomor,2) desc,ProjectName" : OrderBy;
            string sts = " and Status =2";
            string strDept = (DeptID == 0) ? "" : " and DeptID=" + DeptID;
            string strSQL = "Select * from MTC_Project where RowStatus =0 " + sts + " " + strDept + " " + OrderBy;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrProject = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrProject.Add(GenerateObject2(sqlDataReader, GenerateObject(sqlDataReader)));
                }
            }


            return arrProject;
        }
        public ArrayList RetrieveByDept(int DeptID, bool FromInputan)
        {
            string OrderBy = HttpContext.Current.Session["Orderby"].ToString();
            OrderBy = (OrderBy == string.Empty) ? "Order by RIGHT( nomor,2) desc,ProjectName" : OrderBy;
            string sts = " and Approval =2 and RowStatus>-1";
            string strDept = (DeptID == 0) ? "" : " and DeptID=" + DeptID;
            string strSQL = "Select * from MTC_Project where RowStatus >-1 " + sts + " " + strDept + " " + OrderBy;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrProject = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrProject.Add(GenerateObject2(sqlDataReader, GenerateObject(sqlDataReader)));
                }
            }
            return arrProject;
        }

        public MTC_Project RetrieveByID(int ProjectID)
        {
            string strSQL = "Select * from MTC_Project where ID=" + ProjectID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrProject = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader, GenerateObject(sqlDataReader));
                }
            }

            return new MTC_Project();
        }
        public int CheckProject(int ProjectID)
        {
            string newCount = HttpContext.Current.Session["Jml"].ToString();
            string strSQL = "select count(ProjectID) as jml from MTC_Project_pakai where projectid=" + ProjectID;
            strSQL = (newCount == string.Empty) ? strSQL : "select Count(ID)as jml from MTC_Project where ProjectGroup=" + ProjectID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrProject = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["jml"]);
                }

            }
            return 0;
        }

        /**
         * List Detail Project by project ID
         */
        public ArrayList RetrieveProjectDetail(int ProjectID)
        {
            string strSQL = "select * from( " +
                          " select ID,ProjectID,ItemID, " +
                          "     Case ItemTypeID " +
                          "      When 1 Then (Select ItemCode from Inventory where ID=ItemID) " +
                          "      When 2 Then (Select ItemCode from Asset where ID=ItemID) " +
                          "      when 3 Then (Select ItemCode from Biaya where ID=ItemID) " +
                          "      END ItemCode, " +
                          "      Case ItemTypeID " +
                          "      When 1 Then (Select UomCode from Uom where ID=(Select UOMID from Inventory where ID=ItemID)) " +
                          "      When 2 Then (Select UomCode from Uom where ID=(Select UOMID from Asset where ID=ItemID)) " +
                          "      when 3 Then (Select UomCode from Uom where ID=(Select UOMID from Biaya where ID=ItemID)) " +
                          "      END UOMCode, " +
                          "      Case ItemTypeID " +
                          "      When 1 Then (Select ItemName from Inventory where ID=ItemID) " +
                          "      When 2 Then (Select ItemName from Asset where ID=ItemID) " +
                          "      when 3 Then (Select ItemName from Biaya where ID=ItemID) " +
                          "      END ItemName, " +
                          "      Qty,Avgprice,(Qty*AvgPrice) as Harga, " +
                          "      (select PakaiDate from Pakai where ID=PakaiDetailID) as Tanggal " +
                          "   from MTC_Project_Pakai ) as w " +
                          "   where w.ProjectID=" + ProjectID +
                          "   Order by w.Tanggal,w.ItemName";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrProject = new ArrayList();
            if (strError == string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrProject.Add(new MTC_Project
                    {
                        ID = Convert.ToInt32(sqlDataReader["ProjectID"].ToString()),
                        ItemCode = sqlDataReader["ItemCode"].ToString(),
                        ItemName = sqlDataReader["ItemName"].ToString(),
                        Jumlah = Convert.ToDecimal(sqlDataReader["Qty"].ToString()),
                        AvgPrice = Convert.ToDecimal(sqlDataReader["AvgPrice"].ToString()),
                        Harga = Convert.ToDecimal(sqlDataReader["Harga"].ToString()),
                        Tanggal = Convert.ToDateTime(sqlDataReader["Tanggal"].ToString())
                    });
                }
            }

            return arrProject;

        }
        public decimal GetTotalBiaya(string ProjectGroup)
        {
            string strSQL = "select SUM(Biaya) as Biaya from MTC_Project where RowStatus>-1 " + this.Criteria() + " and ProjectName='" + ProjectGroup +
                          "' Group by ProjectName ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            if (dataAccess.Error == string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToDecimal(sqlDataReader["Biaya"]);
                }
            }
            return 0;

        }
        public decimal GetTotalBiayaPakai()
        {
            string strSQL = "select isnull(SUM(avgprice),0) as Biaya from vw_mtcproject where ProjectID in(select ID from MTC_Project " + this.Criteria() + ") and RowStatus >-1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            if (dataAccess.Error == string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToDecimal(sqlDataReader["Biaya"]);
                }
            }
            return 0;
        }
        public decimal GetTotalBiayaPakai(int ProjectID, string Field)
        {
            string strSQL = "select isnull(SUM(" + Field + "),0) as Biaya from vw_mtcproject where ProjectID in(" + ProjectID + ") and RowStatus >-1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            if (dataAccess.Error == string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToDecimal(sqlDataReader["Biaya"]);
                }
            }
            return 0;
        }
        public decimal GetTotalBiayaPakai(int ProjectID, string Field, string ItemID)
        {
            string strSQL = "select isnull(SUM(" + Field + "),0) as Biaya from vw_mtcproject " +
                            "where ProjectID in(" + ProjectID + ") and RowStatus >-1 " +
                            "and ItemID=" + ItemID +
                            "Group by " + ItemID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            if (dataAccess.Error == string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToDecimal(sqlDataReader["Biaya"]);
                }
            }
            return 0;
        }
        public int CheckStatusApprove(int ProjectID)
        {
            int result = 0;
            string strSQL = "Select Status From MTC_Project where ID=" + ProjectID + " and RowStatus>-1";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = int.Parse(sdr["Status"].ToString());
                }
            }
            return result;
        }
        /**
         * List Open project
         * added on 16-06-2016
         */
        public ArrayList RetrieveProject(string where, bool list)
        {
            arrProject = new ArrayList();
            string strSQL = "SELECT mp.*,d.Alias as DeptName, " +
                        "Case when bm.PlantName is null then ma.AreaName else bm.PlantName end AreaImprove, " +
                        "gp.NamaGroup,u.UomCode,Case mp.Status when 0 then 'Open' when 1 then 'Open' when 2 then 'Release' " +
                        "when 3  then 'Close' when 4 then 'Pending' else 'Cancel' end Statuse,ISNULL(ActualFinish,'')Schedule " +
                        "FROM MTC_Project mp " +
                        "LEFT JOIN Dept d on d.ID=mp.DeptID " +
                        "LEFT JOIN BM_Plant bm on bm.ID=mp.ProdLine " +
                        "LEFT JOIN MTC_Area ma on ma.ID=mp.ProdLine " +
                        "LEFT JOIN AM_Group gp on gp.ID=mp.ProjectGroup " +
                        "LEFT JOIN UOM u on u.ID=mp.UomID " +
                        "where mp.RowStatus>-1 " + where;
            strSQL = "WITH ProjectImprovement AS ( " +
                   " SELECT  " +
                   " mp.ID,gp.NamaGroup,d.Alias DeptName,mp.ProjectName,mp.FromDate,mp.ToDate,mp.UomID, " +
                   " Case when bm.PlantName is null then ma.AreaName else bm.PlantName end AreaImprove, " +
                   " mp.Biaya,mp.Quantity,u.UOMCode,mp.Sasaran,mp.Nomor,mp.ActualFinish,mp.ProjectGroup, " +
                   " mp.Approval,mp.Status,mp.RowStatus,mp.DeptID,isnull(mp.ProdLine,0)ProdLine,SubProject " +
                   " FROM MTC_Project mp " +
                   " LEFT JOIN Dept d on d.ID=mp.DeptID  " +
                   " LEFT JOIN BM_Plant bm on bm.ID=mp.ProdLine " +
                   " LEFT JOIN MTC_Area ma on ma.ID=mp.ProdLine " +
                   " LEFT JOIN AM_Group gp on gp.ID=mp.ProjectGroup " +
                   " LEFT JOIN UOM u on u.ID=mp.UomID  " +
                   " where mp.RowStatus>-1 " +
                   " ) " +
                   " SELECT * FROM ProjectImprovement mp where mp.RowStatus>-1" + where;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrProject.Add(GenerateObject(sdr, GenerateObject(sdr)));
                }
            }
            return arrProject;
        }
        public int getID(string nama)
        {
            int ID = 0;
            string strSQL = "Select ID from MTC_Project where nomor='" + nama + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrProject = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return ID = Convert.ToInt32(sqlDataReader["ID"]);
                }
            }

            return ID;
        }
        public MTC_Project GenerateObject(SqlDataReader sqlDataReader)
        {
            objProject = new MTC_Project();
            objProject.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objProject.NamaProject = sqlDataReader["ProjectName"].ToString();
            objProject.FromDate = Convert.ToDateTime(sqlDataReader["FromDate"]);
            objProject.ToDate = Convert.ToDateTime(sqlDataReader["ToDate"]);
            objProject.RowStatus = int.Parse(sqlDataReader["RowStatus"].ToString());
            objProject.DeptID = int.Parse(sqlDataReader["DeptID"].ToString());
            objProject.Biaya = Convert.ToDecimal(sqlDataReader["Biaya"].ToString());
            objProject.SubProjectName = sqlDataReader["SubProject"].ToString();
            objProject.ProdLine = (sqlDataReader["ProdLine"] == DBNull.Value) ? 0 : Convert.ToInt32(sqlDataReader["ProdLine"].ToString());
            objProject.Progress = Convert.ToInt32(sqlDataReader["Status"].ToString());
            objProject.ProjectDate = Convert.ToDateTime(sqlDataReader["FromDate"]);
            objProject.FinishDate = Convert.ToDateTime(sqlDataReader["ToDate"]);
            objProject.GroupID = (sqlDataReader["ProjectGroup"] == DBNull.Value) ? 0 : Convert.ToInt32(sqlDataReader["ProjectGroup"].ToString());
            objProject.Quantity = (sqlDataReader["Quantity"] == DBNull.Value) ? 0 : Convert.ToInt32(sqlDataReader["Quantity"].ToString());
            objProject.UOMID = (sqlDataReader["UOMID"] == DBNull.Value) ? 0 : Convert.ToInt32(sqlDataReader["UOMID"].ToString());
            return objProject;
        }
        private MTC_Project GenerateObject2(SqlDataReader sdr, MTC_Project mpp)
        {
            objProject = (MTC_Project)mpp;
            objProject.Nomor = sdr["Nomor"].ToString();
            objProject.Approval = int.Parse(sdr["Approval"].ToString());
            return objProject;
        }
        public MTC_Project GenerateObject(SqlDataReader sdr, MTC_Project mpp)
        {
            objProject = (MTC_Project)mpp;
            objProject.DeptName = sdr["DeptName"].ToString();
            objProject.AreaImprove = sdr["AreaImprove"].ToString();
            objProject.GroupName = sdr["NamaGroup"].ToString();
            objProject.Nomor = sdr["Nomor"].ToString();
            objProject.Sasaran = sdr["Sasaran"].ToString();
            objProject.UomCode = sdr["UomCode"].ToString();
            //objProject.Statuse = (sdr["Statuse"] != DBNull.Value) ? sdr["Statuse"].ToString() : "";
            objProject.AktualFinish = sdr["ActualFinish"].ToString();
            objProject.Approval = int.Parse(sdr["Approval"].ToString());
            objProject.Status = int.Parse(sdr["Status"].ToString());
            return objProject;
        }
        public ArrayList RetrieveOpenProject(int UserID)
        {
            arrProject = new ArrayList();
            string strSQL = "SELECT mp.*,d.DeptName, " +
                        "Case when bm.PlantName is null then ma.AreaName else bm.PlantName end AreaImprove, " +
                        "gp.NamaGroup,u.UomCode,Case mp.Status when 0 then 'Open' when 1 then 'Open' when 2 then 'Release' " +
                        "when 3 then 'Close' when 4 then 'Pending' else 'Cancel' end Statuse,ISNULL(ActualFinish,'')Schedule " +
                        "FROM MTC_Project mp " +
                        "LEFT JOIN Dept d on d.ID=mp.DeptID " +
                        "LEFT JOIN BM_Plant bm on bm.ID=mp.ProdLine " +
                        "LEFT JOIN MTC_Area ma on ma.ID=mp.ProdLine " +
                        "LEFT JOIN AM_Group gp on gp.ID=mp.ProjectGroup " +
                        "LEFT JOIN UOM u on u.ID=mp.UomID " +
                        "where mp.RowStatus>-1 and mp.Status = " + UserID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrProject.Add(GenerateObject(sdr, GenerateObject(sdr)));
                }
            }
            return arrProject;
        }
        public MTC_Project RetrieveProject(string ProjectNo)
        {
            objProject = new MTC_Project();
            string strSQL = "Select * from MTC_Project where Nomor='" + ProjectNo + "'";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    objProject = GenerateObject(sdr);
                }
            }
            return objProject;
        }
        public ArrayList RetrieveEstimasiMaterial(int ProjectID)
        {
            arrProject = new ArrayList();
            #region oldQuery -- not used
            string strSQL = "SELECT * FROM (SELECT mpm.*, " +
                          "CASE mpm.ItemTypeID WHEN 1 THEN inv.ItemCode WHEN 2 THEN A.ItemCode ELSE B.ItemCode END ItemCode, " +
                          "CASE mpm.ItemTypeID WHEN 1 THEN Inv.ItemName WHEN 2 THEN A.ItemName ELSE B.ItemName END ItemName, " +
                          "UomCode,mp.Nomor,mp.ProjectName " +
                          "FROM MTC_ProjectMaterial mpm " +
                          "LEFT JOIN Inventory inv on inv.ID=mpm.ItemID " +
                          "LEFT JOIN UOM u on u.ID=inv.UOMID " +
                          "LEFT JOIN MTC_Project mp on mp.ID=mpm.ProjectID " +
                          "LEFT JOIN Biaya as B on B.ID=mpm.ItemID " +
                          "LEFT JOIN Asset as A on A.ID=mpm.ItemID " +
                          "WHERE mpm.RowStatus>-1 " +
                          " and ProjectID=" + ProjectID +
                          ") as x Order by ItemName,ItemCode";
            #endregion
            strSQL = "WITH BiayaProject AS ( " +
                   "select ProjectID,ItemID,ItemtypeID,Jumlah,Harga,(Jumlah*Harga)TotalHarga,0 QuanTity,0 AvgPrice,0 AktualHarga, " +
                   "1 Planning from MTC_ProjectMaterial where ProjectID=" + ProjectID + " And RowStatus>-2 " +
                   " Union All " +
                   " select ProjectID,ItemID,ItemTypeID,0 Jumlah,0 Harga, 0 TotalHarga,QuanTity,AvgPrice,(Quantity* AvgPrice)AktualHarga, " +
                   "2 Aktual from vw_mtcproject where ProjectID=" + ProjectID +
                   " ), " +
                   "  BiayaProject1 AS( " +
                   "     SELECT ProjectID,ItemID,ItemTypeID, " +
                   "     (Select dbo.ItemCodeInv(ItemID,ItemTypeID))ItemCode, " +
                   "     (Select dbo.ItemNameInv(ItemID,ItemTypeID))ItemName, " +
                   "     (Select dbo.SatuanInv(ItemID,ItemTypeID))UomCode, " +
                   "     SUM(Jumlah)Jumlah,Avg(Harga)Harga,Sum(TotalHarga)TotalHarga, " +
                   "     SUM(Quantity)QtyAktual,avg(AvgPrice)HargaAktual,isnull(Sum(AktualHarga),0)TotalAktual From BiayaProject " +
                   "     Group By ItemID,ItemTypeID,ProjectID " +
                   " ) " +
                   " SELECT *,(TotalAktual-TotalHarga)Selisih,p.Nomor,p.ProjectName,p.ToDate Schedule FROM BiayaProject1 b " +
                   " LEFT JOIN MTC_Project p ON p.ID=b.ProjectID " +
                   "order by ItemName";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrProject.Add(GeneateObject1(sdr));
                }
            }
            return arrProject;
        }
        public ArrayList RetrieveEstimasiMaterial(int ProjectID, bool draft)
        {

            arrProject = new ArrayList();
            string strSQL = "WITH EstimasiMat AS( " +
                    "SELECT ID,ProjectID,ItemID,ItemTypeID, " +
                    "(SELECT dbo.ItemCodeInv(ItemID,ItemTypeID))ItemCode, " +
                    "(SELECT dbo.ItemNameInv(ItemID,ItemTypeID))ItemName, " +
                    "(SELECT dbo.SatuanInv(ItemID,ItemTypeID))UomCode, " +
                    "Jumlah,Harga,(Jumlah*Harga)TotalHarga,Schedule,RowStatus  " +
                    "FROM MTC_ProjectMaterial WHERE RowStatus>-2 AND ProjectID= " + ProjectID +
                    "), " +
                    "EstimasiMat1 AS ( " +
                    "SELECT  *,1 Urutan FROM EstimasiMat " +
                    "UNION ALL " +
                    "SELECT 999 ID,ProjectID,0 ItemD,1 ItemTypeID,''ItemCode,'TOTAL ' ItemName,''UomCode,0 Jumlah,0  " +
                    "Harga,Sum(TotalHarga),GetDate() Schedule,2 RowStatus,2 Urutan " +
                    "FROM EstimasiMat " +
                    "GROUP BY ProjectID " +
                    ") " +
                    "SELECT * FROM EstimasiMat1 order by Urutan,ItemName";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrProject.Add(GenerateObject2(sdr));
                }
            }
            return arrProject;
        }
        public EstimasiMaterial RetrieveEstimasiMaterial(int ProjectID, bool detail, int ID)
        {
            EstimasiMaterial esm = new EstimasiMaterial();
            foreach (EstimasiMaterial em in RetrieveEstimasiMaterial(ProjectID, true))
            {
                if (em.ID == ID)
                {
                    esm = em;
                }
            }
            return esm;
        }
        public ArrayList RetrieveEstimasiMaterialSearch(string ProjectName)
        {
            arrProject = new ArrayList();
            string strSQL = "SELECT * FROM (SELECT mpm.*, " +
                          "(SELECT dbo.ItemCodeInv(mpm.ItemID,mpm.ItemTypeID)) ItemCode, " +
                          "(SELECT dbo.ItemNameInv(mpm.ItemID,mpm.ItemTypeID)) ItemName, " +
                          "UomCode,mp.Nomor,mp.ProjectName " +
                          "FROM MTC_ProjectMaterial mpm " +
                          "LEFT JOIN Inventory inv on inv.ID=mpm.ItemID " +
                          "LEFT JOIN UOM u on u.ID=inv.UOMID " +
                          "LEFT JOIN MTC_Project mp on mp.ID=mpm.ProjectID " +
                          "WHERE mpm.RowStatus>-1 " +
                          " AND Nomor='" + ProjectName + "' " +
                          ") as x Order by ItemName,ItemCode";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrProject.Add(GeneateObject1(sdr));
                }
            }
            return arrProject;
        }
        public ArrayList RetrieveEstimasiMaterialList(string ProjectID)
        {
            arrProject = new ArrayList();
            string strSQL = "SELECT * FROM (SELECT mpm.*, " +
                          "(SELECT dbo.ItemCodeInv(mpm.ItemID,mpm.ItemTypeID)) ItemCode, " +
                          "(SELECT dbo.ItemNameInv(mpm.ItemID,mpm.ItemTypeID)) ItemName, " +
                          "UomCode,mp.Nomor,mp.ProjectName,(mpm.Jumlah*mpm.Harga)TotalHarga " +
                          "FROM MTC_ProjectMaterial mpm " +
                          "LEFT JOIN Inventory inv on inv.ID=mpm.ItemID " +
                          "LEFT JOIN UOM u on u.ID=inv.UOMID " +
                          "LEFT JOIN MTC_Project mp on mp.ID=mpm.ProjectID " +
                          "WHERE mpm.RowStatus>-1 " +
                          " AND ProjectID='" + ProjectID + "' " +
                          ") as x Order by ItemName,ItemCode";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrProject.Add(GenerateObject2(sdr));
                }
            }
            return arrProject;
        }
        private EstimasiMaterial GeneateObject1(SqlDataReader sdr)
        {
            EstimasiMaterial obj = new EstimasiMaterial();
            obj.Nomor = sdr["Nomor"].ToString();
            obj.NamaProject = sdr["ProjectName"].ToString();
            obj.ItemID = int.Parse(sdr["ItemID"].ToString());
            obj.ID = int.Parse(sdr["ID"].ToString());
            obj.ItemCode = sdr["ItemCode"].ToString();
            obj.ItemName = sdr["ItemName"].ToString();
            obj.UomCode = sdr["UomCode"].ToString();
            obj.Jumlah = Convert.ToDecimal(sdr["Jumlah"].ToString());
            obj.Harga = Convert.ToDecimal(sdr["Harga"].ToString());
            obj.Schedule = Convert.ToDateTime(sdr["Schedule"].ToString());
            obj.ItemTypeID = int.Parse(sdr["ItemTypeID"].ToString());
            obj.ProjectID = int.Parse(sdr["ProjectID"].ToString());
            obj.QtyAktual = decimal.Parse(sdr["QtyAktual"].ToString());
            obj.AvgPrice = decimal.Parse(sdr["TotalAktual"].ToString());
            obj.Selisih = Convert.ToDecimal(sdr["Selisih"].ToString());
            obj.ID = int.Parse(sdr["ID"].ToString());
            return obj;
        }
        private EstimasiMaterial GenerateObject2(SqlDataReader sdr)
        {
            EstimasiMaterial obj = new EstimasiMaterial();
            obj.ID = int.Parse(sdr["ID"].ToString());
            obj.ProjectID = int.Parse(sdr["ProjectID"].ToString());
            obj.ItemCode = sdr["ItemCode"].ToString();
            obj.ItemName = sdr["ItemName"].ToString();
            obj.UomCode = sdr["UomCode"].ToString();
            obj.ItemID = int.Parse(sdr["ItemID"].ToString());
            obj.Jumlah = Convert.ToDecimal(sdr["Jumlah"].ToString());
            obj.Schedule = Convert.ToDateTime(sdr["Schedule"].ToString());
            obj.ItemTypeID = int.Parse(sdr["ItemTypeID"].ToString());
            obj.Harga = Convert.ToDecimal(sdr["Harga"].ToString());
            obj.TotalHarga = Convert.ToDecimal(sdr["TotalHarga"].ToString());
            return obj;
        }

    }
}
