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
    public class MTC_ProjectFacade_Rev1 : AbstractFacade, IReadOnlySessionState,IRequiresSessionState
    {
        private MTC_Project_Rev1 objProject = new MTC_Project_Rev1();
        private EstimasiMaterial_Rev1 objEsm = new EstimasiMaterial_Rev1();
        private ArrayList arrProject;
        private List<SqlParameter> sqlListParam;


        public MTC_ProjectFacade_Rev1()  : base()
        {

        }
        public override int Insert(object objDomain)
        {
            try
            {
                objProject = (MTC_Project_Rev1)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ProjectName", objProject.NamaProject));
                sqlListParam.Add(new SqlParameter("@FromDate",objProject.FromDate));
                sqlListParam.Add(new SqlParameter("@ToDate", objProject.ToDate));
                sqlListParam.Add(new SqlParameter("@Biaya", objProject.Biaya));
                sqlListParam.Add(new SqlParameter("@DeptID", objProject.DeptID));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objProject.CreatedBy));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertMTCProject_Rev1");

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
                objEsm = (EstimasiMaterial_Rev1)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ItemID", objEsm.ItemID));
                sqlListParam.Add(new SqlParameter("@ProjectID", objEsm.ProjectID));
                sqlListParam.Add(new SqlParameter("@Jumlah", objEsm.Jumlah));
                sqlListParam.Add(new SqlParameter("@Harga", objEsm.Harga));
                sqlListParam.Add(new SqlParameter("@Schedule", objEsm.Schedule));
                sqlListParam.Add(new SqlParameter("@ItemTypeID", objEsm.ItemTypeID));
                sqlListParam.Add(new SqlParameter("@CreatedBy",((Users)HttpContext.Current.Session["Users"]).UserID));

                if (Material == false)
                {
                    sqlListParam.Add(new SqlParameter("@RowStatus", objEsm.RowStatus));
                    result = dataAccess.ProcessData(sqlListParam, "spMTC_ProjectMaterial_Insert1_Rev1");
                }
                else
                {
                    result = dataAccess.ProcessData(sqlListParam, "spMTC_ProjectMaterial_Insert_Rev1");
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
                objProject=(MTC_Project_Rev1)objDomain;
                sqlListParam=new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ProjectName", objProject.NamaProject));
                sqlListParam.Add(new SqlParameter("@ProjectDate",objProject.ProjectDate));
                sqlListParam.Add(new SqlParameter("@FinishDate", objProject.FinishDate));             
                sqlListParam.Add(new SqlParameter("@DeptID", objProject.DeptID));
                sqlListParam.Add(new SqlParameter("@Sasaran", objProject.Sasaran));
                sqlListParam.Add(new SqlParameter("@GroupID", objProject.GroupID));
                sqlListParam.Add(new SqlParameter("@ProdLine", objProject.ProdLine));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objProject.CreatedBy));
                sqlListParam.Add(new SqlParameter("@Qty", objProject.Quantity));
                sqlListParam.Add(new SqlParameter("@Uom", objProject.UOMID));
                sqlListParam.Add(new SqlParameter("@Nomor", objProject.Nomor.ToUpper()));
                sqlListParam.Add(new SqlParameter("@Approval", objProject.Approval));
                sqlListParam.Add(new SqlParameter("@DetailSasaran", objProject.DetailSasaran));
                sqlListParam.Add(new SqlParameter("@Zona", objProject.Zona));
                sqlListParam.Add(new SqlParameter("@ToDept", objProject.ToDept));
                sqlListParam.Add(new SqlParameter("@NamaHead", objProject.NamaHead));
                sqlListParam.Add(new SqlParameter("@KondisiSebelum", objProject.KondisiSebelum));
                sqlListParam.Add(new SqlParameter("@KondisiYangDiharapkan", objProject.KondisiYangDiharapkan));
                int intResult = dataAccess.ProcessData(sqlListParam, "spMTCProjectInsert_Rev1");

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
                objProject = (MTC_Project_Rev1)objDomain;
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
                int intResult = dataAccess.ProcessData(sqlListParam,(model==string.Empty)?"spUpdateMTCProject_Rev1":"spMTCProjectUpdate_Rev1");

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
                objEsm = (EstimasiMaterial_Rev1)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objEsm.ID));
                sqlListParam.Add(new SqlParameter("@Jumlah", objEsm.Jumlah));
                sqlListParam.Add(new SqlParameter("@Schedule", objEsm.Schedule));
                sqlListParam.Add(new SqlParameter("@Harga", objEsm.Harga));
                sqlListParam.Add(new SqlParameter("@RowStatus", objEsm.RowStatus));
                sqlListParam.Add(new SqlParameter("@CreatedBy", ((Users)HttpContext.Current.Session["Users"]).UserID));
                result = dataAccess.ProcessData(sqlListParam, "spMTC_ProjectMaterial_Update_Rev1");
            }
            catch {
                result= -1;
            }
            return result;
        }
        //delete estimasi material
        public int Delete(object objDomain, bool Material)
        {
            int result = 0;
            objProject = (MTC_Project_Rev1)objDomain;
            sqlListParam = new List<SqlParameter>();
            sqlListParam.Add(new SqlParameter("@ID", objProject.ID));
            sqlListParam.Add(new SqlParameter("@AktualFinish", objProject.FinishDate));
            sqlListParam.Add(new SqlParameter("@Approval", objProject.Approval));
            //sqlListParam.Add(new SqlParameter("@Status", objProject.Status));
            //sqlListParam.Add(new SqlParameter("@RowStatus", objProject.RowStatus));
            result = dataAccess.ProcessData(sqlListParam, "spMTC_Project_serah_Rev1");
            return result;
        }
        public int InsertLog(object objDomain)
        {
            int result = 0;
            objProject = (MTC_Project_Rev1)objDomain;
            sqlListParam = new List<SqlParameter>();
            sqlListParam.Add(new SqlParameter("@ProjectID", objProject.ID));
            sqlListParam.Add(new SqlParameter("@Level", objProject.Approval));
            sqlListParam.Add(new SqlParameter("@CreatedBy", objProject.CreatedBy));
            sqlListParam.Add(new SqlParameter("@Statuse", objProject.Statuse));
            sqlListParam.Add(new SqlParameter("@IPAddress", HttpContext.Current.Request.ServerVariables["remote_addr"].ToString()));
            result = dataAccess.ProcessData(sqlListParam, "spMTC_Project_log_Insert_Rev1");
              
            return result;
        }
        public override int Delete(object objDomain)
        {
            try
            {
                objProject = (MTC_Project_Rev1)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objProject.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objProject.LastModifiedBy));
                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteMTC_Project_Rev1");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception e)
            {
                strError = e.Message;
                return -1;
            }
        }

        public int insertNo(object objDomain)
        {
            int result = 0;
            try
            {
                objProject = (MTC_Project_Rev1)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Bulan", objProject.Bulan));
                sqlListParam.Add(new SqlParameter("@Tahun", objProject.Tahun));
                //sqlListParam.Add(new SqlParameter("@Count", objProject.Count));
                //sqlListParam.Add(new SqlParameter("@Harga", objEsm.Harga));
                //sqlListParam.Add(new SqlParameter("@RowStatus", objEsm.RowStatus));
                //sqlListParam.Add(new SqlParameter("@CreatedBy", ((Users)HttpContext.Current.Session["Users"]).UserID));
                result = dataAccess.ProcessData(sqlListParam, "spMTC_ProjectNomorInsert_Rev1");
                strError = dataAccess.Error;
                return result;
            }
            catch
            {
                result = -1;
            }
            return result;
        }

        public int UpdateNo(object objDomain)
        {           
            try
            {
                objProject = (MTC_Project_Rev1)objDomain;
                sqlListParam = new List<SqlParameter>();   
        
                sqlListParam.Add(new SqlParameter("@IDno", objProject.IDno));
                sqlListParam.Add(new SqlParameter("@Count", objProject.Count));   
            
                int result = dataAccess.ProcessData(sqlListParam, "spMTC_ProjectNomorUpdate_Rev1");
                strError = dataAccess.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

       

        public int UpdateProject(object objDomain)
        {
            int result = 0;
            try
            {
                objProject = (MTC_Project_Rev1)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objProject.ID));
                sqlListParam.Add(new SqlParameter("@FinishDate", objProject.FinishDate));
                sqlListParam.Add(new SqlParameter("@Biaya", objProject.Biaya));
                result = dataAccess.ProcessData(sqlListParam, "spMTC_ProjectUpdateMTN_Rev1");
            }
            catch
            {
                result = -1;
            }
            return result;
        }

        public int Approval(object objDomain)
        {
            try
            {
                objProject = (MTC_Project_Rev1)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objProject.ID));
                sqlListParam.Add(new SqlParameter("@Status", objProject.Status));
                sqlListParam.Add(new SqlParameter("@Approval", objProject.Approval));
                sqlListParam.Add(new SqlParameter("@RowStatus", objProject.RowStatus));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objProject.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@Biaya", objProject.Biaya));
                sqlListParam.Add(new SqlParameter("@FinishDate", objProject.FinishDate));
                sqlListParam.Add(new SqlParameter("@VerDate", objProject.VerDate));
                sqlListParam.Add(new SqlParameter("@VerPM", objProject.VerPM));
                sqlListParam.Add(new SqlParameter("@Flag", objProject.Flag));
                sqlListParam.Add(new SqlParameter("@ApvDir", objProject.ApvDir));
                sqlListParam.Add(new SqlParameter("@Noted1", objProject.Noted1));

                int intResult = dataAccess.ProcessData(sqlListParam, "spApprovalMTC_Project_Rev1");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception e)
            {
                strError = e.Message;
                return -1;
            }
        }

        public int CancelProject(object objDomain)
        {
            int result = 0;
            try
            {
                objProject = (MTC_Project_Rev1)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objProject.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objProject.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedTime", objProject.LastModifiedTime));
                result = dataAccess.ProcessData(sqlListParam, "spMTC_ProjectCancel_Rev1");
            }
            catch
            {
                result = -1;
            }
            return result;
        }

        public int CancelProjectPM2(object objDomain)
        {
            int result = 0;
            try
            {
                objProject = (MTC_Project_Rev1)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objProject.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objProject.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedTime", objProject.LastModifiedTime));
                result = dataAccess.ProcessData(sqlListParam, "spMTC_ProjectCancelPM2_Rev1");
                strError = dataAccess.Error;
                return result;
            }
            catch
            {
                result = -1;
            }
            return result;
        }

        public int CancelProjectPM3(object objDomain)
        {
            int result = 0;
            try
            {
                objProject = (MTC_Project_Rev1)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objProject.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objProject.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedTime", objProject.LastModifiedTime));
                result = dataAccess.ProcessData(sqlListParam, "spMTC_ProjectCancelPM3_Rev1");
                strError = dataAccess.Error;
                return result;
            }
            catch
            {
                result = -1;
            }
            return result;
        }

        public int ReSchProject(object objDomain)
        {
            int result = 0;
            try
            {
                objProject = (MTC_Project_Rev1)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objProject.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objProject.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedTime", objProject.LastModifiedTime));
                result = dataAccess.ProcessData(sqlListParam, "spMTC_ReSchProject_Rev1");
            }
            catch
            {
                result = -1;
            }
            return result;
        }

        private string Criteria()
        {
            string strC=string.Empty;
                string Sess = HttpContext.Current.Session["StProject"].ToString();
                string ProjID = HttpContext.Current.Session["ProjectID"].ToString();
                string SubPj = HttpContext.Current.Session["SubPj"].ToString();
                string search = HttpContext.Current.Session["Search"].ToString();
                strC = (Sess == string.Empty) ? string.Empty :  Sess;
                strC += (ProjID == "0" ||ProjID==string.Empty) ? string.Empty :  ProjID;
                strC += (SubPj == string.Empty) ? string.Empty : SubPj;
                strC += (search == string.Empty) ? string.Empty : search;
                
               return strC;
        }
        public override ArrayList Retrieve()
        {
            string CancelStatus = HttpContext.Current.Session["Cancel"].ToString();
            string strSQL = "Select * from MTC_Project where RowStatus >-1"+ this.Criteria()+" order by ID Desc";
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
                arrProject.Add(new MTC_Project_Rev1());

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
        public ArrayList RetrieveByDept(int DeptID)
        {
            string OrderBy = HttpContext.Current.Session["Orderby"].ToString();
            OrderBy = (OrderBy == string.Empty) ? "Order by ProjectName" : OrderBy;
            string sts = " and Status =2";
            string strDept = (DeptID == 0) ? "" : " and DeptID=" + DeptID;
            string strSQL = "Select * from MTC_Project where RowStatus =0 " + sts + " " + strDept + " "+ OrderBy;
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
            OrderBy = (OrderBy == string.Empty) ? "Order by ProjectName" : OrderBy;
            string sts = " and Approval =2 and RowStatus=0";
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
        public MTC_Project_Rev1 RetrieveByID(int ProjectID)
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

            return new MTC_Project_Rev1();
        }

        public MTC_Project_Rev1 GetUser(int ProjectID)
        {
            string strSQL = "select createdby from MTC_ProjectLog where ProjectID="+ProjectID+" and Approval=1 and projectID > 0";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            //arrProject = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {                    
                    return (new MTC_Project_Rev1
                    {
                        //ProjectID = Convert.ToInt32(sqlDataReader["ProjectID"].ToString()),
                        CreatedBy = sqlDataReader["CreatedBy"].ToString()
                    });
                }
            }

            return new MTC_Project_Rev1();
        }

        public MTC_Project_Rev1 cekNomorImprovment(int bln, string tahun)
        {
            string strSQL = "select ID IDno,Count from mtc_projectNo where Bulan="+bln+" and Tahun="+tahun+"";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;           
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return (new MTC_Project_Rev1
                    {
                        IDno = Convert.ToInt32(sqlDataReader["IDno"]),
                        Count = Convert.ToInt32(sqlDataReader["Count"])
                    });
                }
            }

            return new MTC_Project_Rev1();
        }

        //public MTC_Project cekNomorImprovmentCount(int bln, string tahun)
        //{
        //    string strSQL = "select Count from mtc_projectNo where Bulan=" + bln + " and Tahun=" + tahun + "";
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        //    strError = dataAccess.Error;
        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            return (new MTC_Project
        //            {
        //                Count = Convert.ToInt32(sqlDataReader["Count"]),
        //            });
        //        }
        //    }

        //    return new MTC_Project();
        //}

        public MTC_Project_Rev1 GetUser2(int ProjectID, string tambah)
        {
            string strSQL = "select createdby from MTC_ProjectLog where ProjectID=" + ProjectID + " "+tambah+" and projectID > 0";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            //arrProject = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    //return GenerateObject2(sqlDataReader, GenerateObject(sqlDataReader));
                    return (new MTC_Project_Rev1
                    {
                        //ProjectID = Convert.ToInt32(sqlDataReader["ProjectID"].ToString()),
                        CreatedBy = sqlDataReader["CreatedBy"].ToString(),
                    });
                }
            }

            return new MTC_Project_Rev1();
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
                    arrProject.Add(new MTC_Project_Rev1
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
            string strSQL = "select isnull(SUM("+Field+"),0) as Biaya from vw_mtcproject where ProjectID in(" + ProjectID + ") and RowStatus >-1 ";
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
        public ArrayList RetrieveProject(string where,bool list)
        {
            arrProject = new ArrayList();

            string where2 = HttpContext.Current.Session["where2"].ToString();
           
            string strSQL = 
            " SELECT mp.*,d.DeptName, " +                       
            " Case when bm.PlantName is null then ma.AreaName else bm.PlantName end AreaImprove, " +
            " gp.NamaGroup,u.UomCode,Case mp.Status when 0 then 'Open' when 1 then 'Open' when 2 then 'Release' " +
            " when 3  then 'Close' when 4 then 'Pending' else 'Cancel' end Statuse,ISNULL(ActualFinish,'')Schedule,mp.DetailSasaran " +
            " FROM MTC_Project mp " +
            " LEFT JOIN Dept d on d.ID=mp.DeptID " +
            " LEFT JOIN BM_Plant bm on bm.ID=mp.ProdLine " +
            " LEFT JOIN MTC_Area ma on ma.ID=mp.ProdLine " +
            " LEFT JOIN AM_Group gp on gp.ID=mp.ProjectGroup " +
            " LEFT JOIN UOM u on u.ID=mp.UomID " +
            " where mp.RowStatus>-1 " + where;

            strSQL = 
            " WITH ProjectImprovement AS ( " +
            " SELECT  " +
            " mp.ID,gp.NamaGroup,d.DeptName,mp.ProjectName,mp.KondisiSebelum,mp.KondisiYangDiHarapkan,LEFT(convert(char,mp.FromDate,106),11)FromDate2," +

            //" case when mp.Approval=2 and mp.ApvPM=2  then 'FinishDate Belum Ditentukan' else LEFT(convert(char,mp.todate,106),11) end ToDate2,"+
            " "+where2+" "+

            " mp.UomID,mp.DeptID,mp.lastmodifiedby, " +                 
            " case when mp.ProdLine = 1 then 'Zona 1' when mp.ProdLine = 2 then 'Zona 2' when mp.ProdLine = 3 then 'Zona 3' when mp.ProdLine = 4 then 'Zona 4' when mp.ProdLine = 97 then 'Material Preparation' when mp.ProdLine = 98 then 'WTP'  when mp.ProdLine = 99 then 'General' when bm.PlantName is null then ma.AreaName else bm.PlantName end AreaImprove, "+
            " mp.Biaya,mp.Quantity,u.UOMCode,mp.Sasaran,mp.Nomor,mp.ActualFinish,mp.ProjectGroup,ToDate, " +
            " mp.Approval,mp.Status,mp.RowStatus,isnull(mp.ProdLine,0)ProdLine,SubProject,mp.DetailSasaran,"+
            " mp.zona,mp.ApvPM,ISNULL(mp.Release,0)Release,ISNULL(mp.VerDate,0)VerUser,mp.CreatedBy,ISNULL(mp.ApvDireksi,0)ApvDir,ISNULL(mp.ToDeptID,0)ToDeptID,ISNULL(mp.Noted,0)Noted1,mp.NamaHead,mp.LastModifiedBy LastModifiedBy2 " +
            " FROM MTC_Project mp " +
            " LEFT JOIN Dept d on d.ID=mp.DeptID  " +
            " LEFT JOIN BM_Plant bm on bm.ID=mp.ProdLine " +
            " LEFT JOIN MTC_Area ma on ma.ID=mp.ProdLine " +
            " LEFT JOIN AM_Group gp on gp.ID=mp.ProjectGroup " +
            " LEFT JOIN UOM u on u.ID=mp.UomID  " +
            " where mp.RowStatus>-1" +
            " ) " +
            " SELECT ISNULL(mp.ApvPM,'') VerPM,ISNULL(mp.VerUser,'')VerUser,ISNULL(mp.Release,'')Flag,* FROM ProjectImprovement mp where mp.RowStatus>-1" + where;
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
        public ArrayList RetrieveProject1(string where, bool list, string date1, string date2)
        {
            arrProject = new ArrayList();

            string where2 = HttpContext.Current.Session["where2"].ToString();

            string strSQL =
            " SELECT mp.*,d.DeptName, " +
            " Case when bm.PlantName is null then ma.AreaName else bm.PlantName end AreaImprove, " +
            " gp.NamaGroup,u.UomCode,Case mp.Status when 0 then 'Open' when 1 then 'Open' when 2 then 'Release' " +
            " when 3  then 'Close' when 4 then 'Pending' else 'Cancel' end Statuse,ISNULL(ActualFinish,'')Schedule,mp.DetailSasaran " +
            " FROM MTC_Project mp " +
            " LEFT JOIN Dept d on d.ID=mp.DeptID " +
            " LEFT JOIN BM_Plant bm on bm.ID=mp.ProdLine " +
            " LEFT JOIN MTC_Area ma on ma.ID=mp.ProdLine " +
            " LEFT JOIN AM_Group gp on gp.ID=mp.ProjectGroup " +
            " LEFT JOIN UOM u on u.ID=mp.UomID " +
            " where mp.RowStatus>-1 " + where;

            strSQL =
            " WITH ProjectImprovement AS ( " +
            " SELECT  " +
            " mp.ID,gp.NamaGroup,d.DeptName,mp.ProjectName,mp.KondisiSebelum,mp.KondisiYangDiHarapkan,LEFT(convert(char,mp.FromDate,106),11)FromDate2," +

            //" case when mp.Approval=2 and mp.ApvPM=2  then 'FinishDate Belum Ditentukan' else LEFT(convert(char,mp.todate,106),11) end ToDate2,"+
            " " + where2 + " " +

            " mp.UomID,mp.DeptID,mp.lastmodifiedby, " +
            " case when mp.ProdLine = 1 then 'Zona 1' when mp.ProdLine = 2 then 'Zona 2' when mp.ProdLine = 3 then 'Zona 3' when mp.ProdLine = 4 then 'Zona 4' when mp.ProdLine = 97 then 'Material Preparation' when mp.ProdLine = 98 then 'WTP'  when mp.ProdLine = 99 then 'General' when bm.PlantName is null then ma.AreaName else bm.PlantName end AreaImprove, " +
            " mp.Biaya,mp.Quantity,u.UOMCode,mp.Sasaran,mp.Nomor,mp.ActualFinish,mp.ProjectGroup,ToDate, " +
            " mp.Approval,mp.Status,mp.RowStatus,isnull(mp.ProdLine,0)ProdLine,SubProject,mp.DetailSasaran," +
            " mp.zona,mp.ApvPM,ISNULL(mp.Release,0)Release,ISNULL(mp.VerDate,0)VerUser,mp.CreatedBy,ISNULL(mp.ApvDireksi,0)ApvDir,ISNULL(mp.ToDeptID,0)ToDeptID,ISNULL(mp.Noted,0)Noted1 " +
            " ,isnull(mp.NamaHead,'')NamaHead "+
            " ,case when mp.LastModifiedBy is null then '' when mp.LastModifiedBy is not null then(select A.UserAlias from users A where A.UserName=mp.LastModifiedBy and A.RowStatus>-1) end LastModifiedBy2 " +
            " FROM MTC_Project mp " +
            " LEFT JOIN Dept d on d.ID=mp.DeptID  " +
            " LEFT JOIN BM_Plant bm on bm.ID=mp.ProdLine " +
            " LEFT JOIN MTC_Area ma on ma.ID=mp.ProdLine " +
            " LEFT JOIN AM_Group gp on gp.ID=mp.ProjectGroup " +
            " LEFT JOIN UOM u on u.ID=mp.UomID  " +
            " where mp.RowStatus>-1 and mp.fromdate>='" + date1 + "' and mp.fromdate<='" + date2 + "'" +
            " ) " +
            " SELECT ISNULL(mp.ApvPM,'') VerPM,ISNULL(mp.VerUser,'')VerUser,ISNULL(mp.Release,'')Flag,* FROM ProjectImprovement mp where mp.RowStatus>-1" + where;
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
        public MTC_Project_Rev1 RetrieveProject1(string where,string nomor, bool list)
        {
            objProject = new MTC_Project_Rev1();
            string strSQL = "SELECT mp.*,d.DeptName, " +
                //"SELECT mp.*,d.Alias as DeptName, " +
                        "Case when bm.PlantName is null then ma.AreaName else bm.PlantName end AreaImprove, " +
                        "gp.NamaGroup,u.UomCode,Case mp.Status when 0 then 'Open' when 1 then 'Open' when 2 then 'Release' " +
                        "when 3  then 'Close' when 4 then 'Pending' else 'Cancel' end Statuse,ISNULL(ActualFinish,'')Schedule,mp.DetailSasaran " +
                        "FROM MTC_Project mp " +
                        "LEFT JOIN Dept d on d.ID=mp.DeptID " +
                        "LEFT JOIN BM_Plant bm on bm.ID=mp.ProdLine " +
                        "LEFT JOIN MTC_Area ma on ma.ID=mp.ProdLine " +
                        "LEFT JOIN AM_Group gp on gp.ID=mp.ProjectGroup " +
                        "LEFT JOIN UOM u on u.ID=mp.UomID " +
                        "where mp.RowStatus>-1 " + where;
            strSQL = "WITH ProjectImprovement AS ( " +
                   " SELECT  " +
                //" mp.ID,gp.NamaGroup,d.Alias DeptName,mp.ProjectName,mp.FromDate,mp.ToDate,mp.UomID,mp.DeptID,mp.lastmodifiedby, " +
                   " mp.ID,gp.NamaGroup,d.DeptName,mp.ProjectName,LEFT(convert(char,mp.FromDate,106),11)FromDate2," +
                   "case when mp.Approval=2 and mp.ApvPM=2  then 'FinishDate Belum Ditentukan'  " +
                   " else LEFT(convert(char,mp.todate,106),11) end ToDate2,mp.UomID,mp.DeptID,mp.lastmodifiedby, " +
                //" Case when bm.PlantName is null then ma.AreaName else bm.PlantName end AreaImprove, " +
                   " case when mp.ProdLine = 1 then 'Zona 1' when mp.ProdLine = 2 then 'Zona 2' when mp.ProdLine = 3 then 'Zona 3' when mp.ProdLine = 4 then 'Zona 4' when mp.ProdLine = 97 then 'Material Preparation' when mp.ProdLine = 98 then 'WTP'  when mp.ProdLine = 99 then 'General' when bm.PlantName is null then ma.AreaName else bm.PlantName end AreaImprove, " +
                   " mp.Biaya,mp.Quantity,u.UOMCode,mp.Sasaran,mp.Nomor,mp.ActualFinish,mp.ProjectGroup,ToDate, " +
                   " mp.Approval,mp.Status,mp.RowStatus,isnull(mp.ProdLine,0)ProdLine,SubProject,mp.DetailSasaran," +
                   " mp.zona,mp.ApvPM,ISNULL(mp.Release,0)Release,mp.VerDate,mp.CreatedBy,ISNULL(mp.ApvDireksi,0)ApvDir,ISNULL(mp.ToDeptID,0)ToDeptID " +
                   " ,ISNULL(mp.Noted,'-')Noted1 "+
                   " FROM MTC_Project mp " +
                   " LEFT JOIN Dept d on d.ID=mp.DeptID  " +
                   " LEFT JOIN BM_Plant bm on bm.ID=mp.ProdLine " +
                   " LEFT JOIN MTC_Area ma on ma.ID=mp.ProdLine " +
                   " LEFT JOIN AM_Group gp on gp.ID=mp.ProjectGroup " +
                   " LEFT JOIN UOM u on u.ID=mp.UomID  " +
                   " where mp.RowStatus>-1" +
                   " ) " +
                   " select * from (SELECT ISNULL(mp.ApvPM,'') VerPM,ISNULL(mp.VerDate,'')VerUser,ISNULL(mp.Release,'')Flag,* " +
                   "FROM ProjectImprovement mp where mp.RowStatus>-1" + where + ")A where nomor='" +nomor + "'";
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
        public MTC_Project_Rev1 RetrieveIDProject(string NomorProject)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "Select ID ProjectID from MTC_Project where Nomor='" + NomorProject + "'";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectIDProject(sqlDataReader);
                }
            }

            return new MTC_Project_Rev1();
        }

        public MTC_Project_Rev1 RetrieveGrid(int ID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = " select ProjectName NamaProject,ISNULL(Biaya,0)Biaya from MTC_Project as A, Dept as B where A.DeptID=B.ID and A.ID=" + ID + " ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return (new MTC_Project_Rev1
                    {
                        NamaProject = sqlDataReader["NamaProject"].ToString(),
                        //Nomor = sqlDataReader["Nomor"].ToString(),
                        //ProjectDate = Convert.ToDateTime(sqlDataReader["ProjectDate"].ToString()),
                        //FinishDate = Convert.ToDateTime(sqlDataReader["FinishDate"].ToString()),
                        Biaya = Convert.ToInt32(sqlDataReader["Biaya"].ToString()),
                        //StatusAproval = sqlDataReader["StatusAproval"].ToString()
                    });
                }
            }

            return new MTC_Project_Rev1();
        }  

        public MTC_Project_Rev1 GetDeptID(string NomorProject)
        {
            string strSQL = "Select DeptID from MTC_Project where Nomor='" + NomorProject + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;            
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return (new MTC_Project_Rev1
                    {
                        DeptID = Convert.ToInt32(sqlDataReader["DeptID"].ToString()),                        
                    });
                }
            }
            return new MTC_Project_Rev1();
        }

        public MTC_Project_Rev1 GetNamaProject(string ProjectName)
        {
            string strSQL = "Select ProjectName NamaProject from MTC_Project where Nomor='" + ProjectName + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return (new MTC_Project_Rev1
                    {
                        NamaProject = sqlDataReader["NamaProject"].ToString(),

                    });
                }
            }
            return new MTC_Project_Rev1();
        }

        public MTC_Project_Rev1 GenerateObjectIDProject(SqlDataReader sqlDataReader)
        {
            objProject = new MTC_Project_Rev1();
            objProject.ProjectID = Convert.ToInt32(sqlDataReader["ProjectID"].ToString());
            return objProject;
        }

        public MTC_Project_Rev1 GenerateObject(SqlDataReader sqlDataReader)
        {
            objProject = new MTC_Project_Rev1();
            objProject.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objProject.NamaProject = sqlDataReader["ProjectName"].ToString();
            
            //penambahan agus 10-05-2022
            //objProject.KondisiSebelum = sqlDataReader["KondisiSebelum"].ToString();
            //objProject.KondisiYangDiharapkan = sqlDataReader["KondisiYangDiHarapkan"].ToString();
            //penambahan agus 10-05-2022
            
            //objProject.FromDate = Convert.ToDateTime(sqlDataReader["FromDate"]);
            //objProject.ToDate = Convert.ToDateTime(sqlDataReader["ToDate"]);
            objProject.Approval = int.Parse(sqlDataReader["Approval"].ToString());
            objProject.Status = int.Parse(sqlDataReader["Status"].ToString());
            objProject.RowStatus = int.Parse(sqlDataReader["RowStatus"].ToString());
            objProject.DeptID = int.Parse(sqlDataReader["DeptID"].ToString());
            objProject.Biaya = Convert.ToDecimal(sqlDataReader["Biaya"].ToString());
            objProject.SubProjectName = sqlDataReader["SubProject"].ToString();
            objProject.ProdLine = (sqlDataReader["ProdLine"] == DBNull.Value) ? 0 : Convert.ToInt32(sqlDataReader["ProdLine"].ToString());
            objProject.Progress = Convert.ToInt32(sqlDataReader["Status"].ToString());
            //objProject.ProjectDate = Convert.ToDateTime(sqlDataReader["FromDate"]);
            objProject.FinishDate = Convert.ToDateTime(sqlDataReader["ToDate"]);

            //objProject.FromDate2 = sqlDataReader["FromDate2"].ToString();
            //objProject.ToDate2 = sqlDataReader["ToDate2"].ToString();
            objProject.ToDate = Convert.ToDateTime(sqlDataReader["ToDate"]);
           
            objProject.GroupID = (sqlDataReader["ProjectGroup"] == DBNull.Value) ? 0 : Convert.ToInt32(sqlDataReader["ProjectGroup"].ToString());
            objProject.Quantity = (sqlDataReader["Quantity"] == DBNull.Value) ? 0 : Convert.ToInt32(sqlDataReader["Quantity"].ToString());
            objProject.UOMID = (sqlDataReader["UOMID"] == DBNull.Value) ? 0 : Convert.ToInt32(sqlDataReader["UOMID"].ToString());
            objProject.VerPM = int.Parse(sqlDataReader["VerPM"].ToString());
            objProject.ToDeptID = int.Parse(sqlDataReader["ToDeptID"].ToString());
            objProject.Noted1 = Convert.ToInt32(sqlDataReader["Noted1"]);

            objProject.VerUser = Convert.ToInt32(sqlDataReader["VerUser"]);

            return objProject;
        }
        private MTC_Project_Rev1 GenerateObject2(SqlDataReader sdr, MTC_Project_Rev1 mpp)
        {
            objProject = (MTC_Project_Rev1)mpp;
            objProject.Nomor = sdr["Nomor"].ToString();
            objProject.Approval = int.Parse(sdr["Approval"].ToString());
            return objProject;
        }
        public MTC_Project_Rev1 GenerateObject(SqlDataReader sdr, MTC_Project_Rev1 mpp)
        {
            objProject = (MTC_Project_Rev1)mpp;
            objProject.DeptName = sdr["DeptName"].ToString();
            objProject.AreaImprove = sdr["AreaImprove"].ToString();
            objProject.GroupName = sdr["NamaGroup"].ToString();
            objProject.Nomor = sdr["Nomor"].ToString();
            objProject.Sasaran = sdr["Sasaran"].ToString();
            objProject.FromDate2 = sdr["FromDate2"].ToString();
            objProject.ToDate2 = sdr["ToDate2"].ToString();
            
            objProject.UomCode = sdr["UomCode"].ToString();
            objProject.LastModifiedBy = sdr["LastModifiedBy"].ToString();
            //objProject.Statuse = (sdr["Statuse"] != DBNull.Value) ? sdr["Statuse"].ToString() : "";
            objProject.AktualFinish = sdr["ActualFinish"].ToString();
            objProject.Approval = int.Parse(sdr["Approval"].ToString());
            objProject.Status = int.Parse(sdr["Status"].ToString());
            objProject.DeptID = int.Parse(sdr["DeptID"].ToString());
            objProject.DetailSasaran = sdr["DetailSasaran"].ToString();
            objProject.Quantity = int.Parse(sdr["Quantity"].ToString());
            objProject.RowStatus = int.Parse(sdr["RowStatus"].ToString());
            objProject.ProdLine = int.Parse(sdr["ProdLine"].ToString());
            objProject.Zona = sdr["Zona"].ToString();
            objProject.ToDate =Convert.ToDateTime(sdr["ToDate"].ToString());
            objProject.VerPM = int.Parse(sdr["VerPM"].ToString());
            //objProject.VerDate = int.Parse(sdr["VerDate"].ToString());
            objProject.VerUser = int.Parse(sdr["VerUser"].ToString());
            objProject.Flag = int.Parse(sdr["Flag"].ToString());
            objProject.CreatedBy = sdr["CreatedBy"].ToString();
            objProject.NamaProject = sdr["ProjectName"].ToString();

            //penambahan agus 10-05-2022

            objProject.KondisiSebelum = sdr["KondisiSebelum"].ToString();
            objProject.KondisiYangDiharapkan = sdr["KondisiYangDiHarapkan"].ToString();

            //penambahan agus 10-05-2022

            objProject.ApvDir = int.Parse(sdr["ApvDir"].ToString());
            objProject.ToDeptID = int.Parse(sdr["ToDeptID"].ToString());
            objProject.Noted1 = int.Parse(sdr["Noted1"].ToString());
            objProject.CreatedBy = sdr["CreatedBy"].ToString();
            objProject.NamaHead = sdr["NamaHead"].ToString();
            objProject.LastModifiedBy2 = sdr["LastModifiedBy2"].ToString();
            return objProject;
        }
        public ArrayList RetrieveOpenProject(int UserID, string tambah, string tambah2, string tambah3, string tambah4,string tambah5)
        {
            arrProject = new ArrayList();
            string strSQL = "SELECT mp.*,d.DeptName,mp.DeptID, " +
                        "Case when bm.PlantName is null then ma.AreaName else bm.PlantName end AreaImprove, " +
                        "gp.NamaGroup,u.UomCode,Case mp.Status when 0 then 'Open' when 1 then 'Open' when 2 then 'Release' " +
                        "when 3 then 'Close' when 4 then 'Pending' else 'Cancel' end Statuse,ISNULL(ActualFinish,'')Schedule,"+
                        "mp.DetailSasaran,mp.ToDate ToDate2,mp.FromDate FromDate2,ISNULL(mp.ApvPM,0)VerPM,ISNULL(mp.VerDate,0)VerUser,"+
                        "ISNULL(mp.ApvDireksi,0)ApvDir,"+
                        "ISNULL(Release,0)Flag,ISNULL(mp.ProjectName,'')NamaProject,ISNULL(mp.ToDeptID,0) ToDeptID,ISNULL(mp.Noted,0)Noted1,mp.CreatedBy " +
                        ",case when mp.LastModifiedBy is null then '' when mp.LastModifiedBy is not null then(select A.UserAlias from users A where A.UserName=mp.LastModifiedBy and A.RowStatus>-1) end LastModifiedBy2 "+
                        "FROM MTC_Project mp " +
                        "LEFT JOIN Dept d on d.ID=mp.DeptID " +
                        "LEFT JOIN BM_Plant bm on bm.ID=mp.ProdLine " +
                        "LEFT JOIN MTC_Area ma on ma.ID=mp.ProdLine " +
                        "LEFT JOIN AM_Group gp on gp.ID=mp.ProjectGroup " +
                        "LEFT JOIN UOM u on u.ID=mp.UomID " +
                
                        "where " + tambah3 + " " + tambah2 + "" +
                        
                        "" + tambah4 + "" +
                        "" + tambah5 + "";
            DataAccess da=new DataAccess(Global.ConnectionString());
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
        public ArrayList RetrieveOpenProject1()
        {
            arrProject = new ArrayList();
            string strSQL = "SELECT mp.*,d.DeptName, " +
                        "Case when bm.PlantName is null then ma.AreaName else bm.PlantName end AreaImprove, " +
                        "gp.NamaGroup,u.UomCode,Case mp.Status when 0 then 'Open' when 1 then 'Open' when 2 then 'Release' " +
                        "when 3 then 'Close' when 4 then 'Pending' else 'Cancel' end Statuse,ISNULL(ActualFinish,'')Schedule,mp.DetailSasaran,ISNULL(mp.Noted,0)Noted1 " +
                        ",case when mp.LastModifiedBy is null then '' when mp.LastModifiedBy is not null then(select A.UserAlias from users A where A.UserName=mp.LastModifiedBy and A.RowStatus>-1) end LastModifiedBy2 " +
                        "FROM MTC_Project mp " +
                        "LEFT JOIN Dept d on d.ID=mp.DeptID " +
                        "LEFT JOIN BM_Plant bm on bm.ID=mp.ProdLine " +
                        "LEFT JOIN MTC_Area ma on ma.ID=mp.ProdLine " +
                        "LEFT JOIN AM_Group gp on gp.ID=mp.ProjectGroup " +
                        "LEFT JOIN UOM u on u.ID=mp.UomID " +
                //"where mp.RowStatus>-1 and mp.Status = " + UserID;
                        "where mp.RowStatus>-1 and mp.Status = 0 and mp.rowstatus=1 and mp.Approval=1 and mp.LastModifiedBy is not null ";
                        //"and mp.CreatedBy in (select UserName from Users where  RowStatus>-1)";
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
        public MTC_Project_Rev1 RetrieveProject(string ProjectNo)
        {
            objProject = new MTC_Project_Rev1();
            string strSQL = "Select ApvPM VerPM,ISNULL(Noted,0)Noted1,ISNULL(VerDate,0) VerUser,* from MTC_Project where Nomor='" + ProjectNo + "'";
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
            string strSQL = "SELECT * FROM (SELECT mpm.*, "+
                          "CASE mpm.ItemTypeID WHEN 1 THEN inv.ItemCode WHEN 2 THEN A.ItemCode ELSE B.ItemCode END ItemCode, " +
                          "CASE mpm.ItemTypeID WHEN 1 THEN Inv.ItemName WHEN 2 THEN A.ItemName ELSE B.ItemName END ItemName, "+
                          "UomCode,mp.Nomor,mp.ProjectName " +
                          "FROM MTC_ProjectMaterial mpm " +
                          "LEFT JOIN Inventory inv on inv.ID=mpm.ItemID " +
                          "LEFT JOIN UOM u on u.ID=inv.UOMID " +
                          "LEFT JOIN MTC_Project mp on mp.ID=mpm.ProjectID " +
                          "LEFT JOIN Biaya as B on B.ID=mpm.ItemID "+
                          "LEFT JOIN Asset as A on A.ID=mpm.ItemID "+
                          "WHERE mpm.RowStatus>-1 " +
                          " and ProjectID=" + ProjectID +
                          ") as x Order by ItemName,ItemCode";
            #endregion
            //strSQL = "WITH BiayaProject AS ( " +
            //       "select ProjectID,ItemID,ItemtypeID,Jumlah,Harga,(Jumlah*Harga)TotalHarga,0 QuanTity,0 AvgPrice,0 AktualHarga, " +
            //       "1 Planning from MTC_ProjectMaterial where ProjectID=" + ProjectID + " And RowStatus>-2 " +
            //       " Union All " +
            //       " select ProjectID,ItemID,ItemTypeID,0 Jumlah,0 Harga, 0 TotalHarga,QuanTity,AvgPrice,(Quantity* AvgPrice)AktualHarga, " +
            //       "2 Aktual from vw_mtcproject where ProjectID=" + ProjectID +
            //       " ), " +
            //       "  BiayaProject1 AS( " +
            //       "     SELECT ProjectID,ItemID,ItemTypeID, " +
            //       "     (Select dbo.ItemCodeInv(ItemID,ItemTypeID))ItemCode, " +
            //       "     (Select dbo.ItemNameInv(ItemID,ItemTypeID))ItemName, " +
            //       "     (Select dbo.SatuanInv(ItemID,ItemTypeID))UomCode, " +
            //       "     SUM(Jumlah)Jumlah,Avg(Harga)Harga,Sum(TotalHarga)TotalHarga, " +
            //       "     SUM(Quantity)QtyAktual,avg(AvgPrice)HargaAktual,Sum(AktualHarga)TotalAktual From BiayaProject " +
            //       "     Group By ItemID,ItemTypeID,ProjectID " +
            //       " ) " +
            //       " SELECT *,(TotalAktual-TotalHarga)Selisih,p.Nomor,p.ProjectName,p.ToDate Schedule FROM BiayaProject1 b " +
            //       " LEFT JOIN MTC_Project p ON p.ID=b.ProjectID " +
            //       "order by ItemName";
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
        public EstimasiMaterial_Rev1 RetrieveEstimasiMaterial(int ProjectID,bool detail, int ID)
        {
            EstimasiMaterial_Rev1 esm = new EstimasiMaterial_Rev1();
            foreach (EstimasiMaterial_Rev1 em in RetrieveEstimasiMaterial(ProjectID, true))
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
        private EstimasiMaterial_Rev1 GeneateObject1(SqlDataReader sdr)
        {
            EstimasiMaterial_Rev1 obj = new EstimasiMaterial_Rev1();
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
            //obj.QtyAktual = decimal.Parse(sdr["QtyAktual"].ToString());
            //obj.AvgPrice = decimal.Parse(sdr["TotalAktual"].ToString());
            //obj.Selisih = Convert.ToDecimal(sdr["Selisih"].ToString());
            obj.ID=int.Parse(sdr["ID"].ToString());
            return obj;
        }
        private EstimasiMaterial_Rev1 GenerateObject2(SqlDataReader sdr)
        {
            EstimasiMaterial_Rev1 obj = new EstimasiMaterial_Rev1();
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

        public ArrayList GetArea(string ID, int DeptID)
        {
            string strSQL = "Select AreaName,ID IDarea from MTC_AreaImprovment where rowstatus > -1 and DeptID="+DeptID+" order by ID";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            ArrayList arrArea = new ArrayList();
            if (dataAccess.Error == string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrArea.Add(new MTC_Project_Rev1
                    {
                        IDarea = Convert.ToInt32(sqlDataReader["IDarea"].ToString()),
                        AreaName = sqlDataReader["AreaName"].ToString()
                    });
                }
            }
            return arrArea;
        }

        public EstimasiMaterial_Rev1 RetrieveDataProject(string Nomor)
        {
            string strSQL = "select Status,RowStatus,Approval from MTC_Project where nomor='" + Nomor + "' and RowStatus > -1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
           
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return (new EstimasiMaterial_Rev1
                    {
                        Status = Convert.ToInt32(sqlDataReader["Status"].ToString()),
                        RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"].ToString()),
                        Approval = Convert.ToInt32(sqlDataReader["Approval"].ToString())
                    });
                }
            }

            return new EstimasiMaterial_Rev1();
        }

        public MTC_Project_Rev1 RetrieveDataLastModif (string Nomor)
        {
            //string strSQL = "select LastModifiedBy,ISNULL(VerDate,'')VerDate from MTC_Project where Nomor='" + Nomor + "' and RowStatus >-1";
            string strSQL = " select top 1 Data1.*,ISNULL(mp.ID,'')ProjectID from (select LastModifiedBy,ISNULL(VerDate,'')VerDate,ID,Biaya from MTC_Project " +
             " where Nomor='" + Nomor + "' and RowStatus >-1) as  Data1 LEFT JOIN MTC_ProjectMaterial mp ON Data1.ID=mp.ProjectID order by ID desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return (new MTC_Project_Rev1
                    {
                        Biaya = Convert.ToDecimal(sqlDataReader["Biaya"].ToString()),
                        VerDate = Convert.ToInt32(sqlDataReader["VerDate"].ToString()),
                        ProjectID = Convert.ToInt32(sqlDataReader["ProjectID"].ToString()),
                        LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString()
                       
                    });
                }
            }

            return new MTC_Project_Rev1();
        }

        public int CekData(string Nomor)
        {
            int result = 0;
            string strSQL = "select SUM(ID)ID from (select ID from MTC_ProjectMaterial where ProjectID in "+
                            "(select ID from MTC_Project where Nomor='"+Nomor+"' and RowStatus>-1) and RowStatus >-1 "+
                            " union all "+
                            " select top 1 '0'ID from  MTC_ProjectMaterial) as Data2 ";
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
        /**
         * List Open project
         * added on 16-06-2016
         */
        public ArrayList RetrieveEstimasiMaterialSerah(string ProjectID)
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
                          " and nomor='" + ProjectID +
                          "') as x Order by ItemName,ItemCode";
            #endregion
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrProject.Add(GeneateObject11(sdr));
                }
            }
            return arrProject;
        }

        private EstimasiMaterial_Rev1 GeneateObject11(SqlDataReader sdr)
        {
            EstimasiMaterial_Rev1 obj = new EstimasiMaterial_Rev1();
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
            //obj.QtyAktual = decimal.Parse(sdr["QtyAktual"].ToString());
            //obj.AvgPrice = decimal.Parse(sdr["TotalAktual"].ToString());
            //obj.Selisih = Convert.ToDecimal(sdr["Selisih"].ToString());
            obj.ID = int.Parse(sdr["ID"].ToString());
            return obj;
        }

        public ArrayList GetHead(int UserID)
        {
            string strSQL =
            " select Nama1 NamaHead from  (select RTRIM(NamaHead)Nama1,RTRIM(NamaSub)Nama2 from mtc_projectsubbagian where deptid="+UserID+" ) as xx ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            ArrayList arrArea = new ArrayList();
            if (dataAccess.Error == string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrArea.Add(new MTC_Project_Rev1
                    {
                        //IDarea = Convert.ToInt32(sqlDataReader["IDarea"].ToString()),
                        NamaHead = sqlDataReader["NamaHead"].ToString()
                    });
                }
            }
            return arrArea;
        }


    }
}
