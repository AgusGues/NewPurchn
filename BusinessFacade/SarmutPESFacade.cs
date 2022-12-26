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
    public class SarmutPESFacade : AbstractTransactionFacade
    {
        private SarmutPes objSarPes = new SarmutPes();
        private ArrayList arrSarPes;
        private List<SqlParameter> sqlListParam;

        public SarmutPESFacade(object objDomain)
        : base(objDomain)
        {
            objSarPes = (SarmutPes)objDomain;
        }

        public SarmutPESFacade()
        {

        }
        public override int Insert(TransactionManager transManager)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@SopNo", objSarPes.SopNo));
                sqlListParam.Add(new SqlParameter("@NewSop", objSarPes.NewSop));
                sqlListParam.Add(new SqlParameter("@DeptID", objSarPes.DeptID));
                sqlListParam.Add(new SqlParameter("@BagianID", objSarPes.BagianID));
                sqlListParam.Add(new SqlParameter("@TglMulai", objSarPes.TglMulai));
                sqlListParam.Add(new SqlParameter("@TglTarget", objSarPes.TglTarget));
                sqlListParam.Add(new SqlParameter("@CategoryID", objSarPes.CategoryID));
                sqlListParam.Add(new SqlParameter("@BobotNilai", objSarPes.BobotNilai));
                sqlListParam.Add(new SqlParameter("@Ket", objSarPes.Ket));
                sqlListParam.Add(new SqlParameter("@Pic", objSarPes.Pic));
                sqlListParam.Add(new SqlParameter("@UserID", objSarPes.UserID));
                sqlListParam.Add(new SqlParameter("@UserGroupID", objSarPes.UserGroupID));
                sqlListParam.Add(new SqlParameter("@TargetKe", objSarPes.TargetKe));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objSarPes.CreatedBy));
                sqlListParam.Add(new SqlParameter("@Iso_UserID", objSarPes.Iso_UserID));

                int intResult = transManager.DoTransaction(sqlListParam, "spInsertSOP");

                strError = transManager.Error;


                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int InsertKPI(TransactionManager transManager)
        {
            try
            {
                objSarPes = (SarmutPes)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@KpiNo", objSarPes.KpiNo));
                sqlListParam.Add(new SqlParameter("@NewKpi", objSarPes.Description));
                sqlListParam.Add(new SqlParameter("@DeptID", objSarPes.DeptID));
                sqlListParam.Add(new SqlParameter("@BagianID", objSarPes.BagianID));
                sqlListParam.Add(new SqlParameter("@TglMulai", objSarPes.TglMulai));
                //sqlListParam.Add(new SqlParameter("@TglTarget", objSarPes.TglTarget));
                sqlListParam.Add(new SqlParameter("@CategoryID", objSarPes.IDUserCategory));
                sqlListParam.Add(new SqlParameter("@BobotNilai", objSarPes.BobotNilai));
                sqlListParam.Add(new SqlParameter("@Ket", objSarPes.Actual));
                sqlListParam.Add(new SqlParameter("@Pic", objSarPes.Pic));
                sqlListParam.Add(new SqlParameter("@UserID", objSarPes.UserID));
                //sqlListParam.Add(new SqlParameter("@Actual", objSarPes.Actual));
                //sqlListParam.Add(new SqlParameter("@Percent", objSarPes.Percent));
                //sqlListParam.Add(new SqlParameter("@CreatedBy", objSarPes.CreatedBy));
                sqlListParam.Add(new SqlParameter("@Iso_UserID", objSarPes.UserID));

                int intResult = transManager.DoTransaction(sqlListParam, "SarPesBM_KPI_Insert");

                strError = transManager.Error;


                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        private string NewInputRebobot = new Inifiles(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("OtoRebobotAktif", "PES");
        public int InsertKPIDetail(TransactionManager transManager)
        {
            try
            {
                int intResult = 0;
                objSarPes = (SarmutPes)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@KpiID", objSarPes.KpiID));
                //sqlListParam.Add(new SqlParameter("@TargetKe", objSarPes.TargetKe));
                //sqlListParam.Add(new SqlParameter("@TglTarget", objSarPes.TglTarget));
                //sqlListParam.Add(new SqlParameter("@Status", objSarPes.Status));
                sqlListParam.Add(new SqlParameter("@PointNilai", objSarPes.PointNilai));
                sqlListParam.Add(new SqlParameter("@KetTargetKe", objSarPes.KetTargetKe));
                sqlListParam.Add(new SqlParameter("@SopScoreID", objSarPes.SopScoreID));
                //if (NewInputRebobot == "1")
                //{
                //    sqlListParam.Add(new SqlParameter("@Rebobot", objSarPes.Rebobot));
                //    intResult = transManager.DoTransaction(sqlListParam, "spPES_KPI_DetailInsertNew");
                //}
                //else
                //{
                intResult = transManager.DoTransaction(sqlListParam, "SarPesBM_KPI_DetailInsert");
                //}
                strError = transManager.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int UpdateKpi(TransactionManager transManager)
        {
            try
            {
                objSarPes = (SarmutPes)objDomain;
                sqlListParam = new List<SqlParameter>();

                sqlListParam.Add(new SqlParameter("@KPIID", objSarPes.KPIID));
                sqlListParam.Add(new SqlParameter("@Ket", objSarPes.Ket));
                sqlListParam.Add(new SqlParameter("@SopScoreID", objSarPes.SopScoreID));
                sqlListParam.Add(new SqlParameter("@KetTargetKe", objSarPes.KetTargetKe));
                sqlListParam.Add(new SqlParameter("@PointNilai", objSarPes.PointNilai));

                int intResult = transManager.DoTransaction(sqlListParam, "SarPesBM_KPI_Update");

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
                objSarPes = (SarmutPes)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@SopNo", objSarPes.SopNo));
                sqlListParam.Add(new SqlParameter("@NewSop", objSarPes.NewSop));
                sqlListParam.Add(new SqlParameter("@DeptID", objSarPes.DeptID));
                sqlListParam.Add(new SqlParameter("@BagianID", objSarPes.BagianID));
                sqlListParam.Add(new SqlParameter("@TglMulai", objSarPes.TglMulai));
                sqlListParam.Add(new SqlParameter("@TglTarget", objSarPes.TglTarget));
                sqlListParam.Add(new SqlParameter("@CategoryID", objSarPes.CategoryID));
                sqlListParam.Add(new SqlParameter("@BobotNilai", objSarPes.BobotNilai));
                sqlListParam.Add(new SqlParameter("@Ket", objSarPes.Ket));
                sqlListParam.Add(new SqlParameter("@TglSelesai", objSarPes.TglSelesai));
                sqlListParam.Add(new SqlParameter("@Pic", objSarPes.Pic));
                sqlListParam.Add(new SqlParameter("@UserID", objSarPes.UserID));
                sqlListParam.Add(new SqlParameter("@UserGroupID", objSarPes.UserGroupID));
                sqlListParam.Add(new SqlParameter("@Status", objSarPes.Status));
                sqlListParam.Add(new SqlParameter("@RowStatus", objSarPes.RowStatus));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objSarPes.CreatedBy));
                sqlListParam.Add(new SqlParameter("@Iso_UserID", objSarPes.Iso_UserID));

                int intResult = transManager.DoTransaction(sqlListParam, "spUpdateSOP");

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
                objSarPes = (SarmutPes)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objSarPes.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objSarPes.LastModifiedBy));

                int intResult = transManager.DoTransaction(sqlListParam, "spDeleteSOP");

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
            string strSQL = "select iuc.ID,iuc.UserID,iuc.SectionID,iuc.CategoryID,iuc.Bobot,iu.UserName,iu.DeptID from ISO_UserCategory as iuc " +
                            "left join ISO_Users as iu on iuc.UserID=iu.ID where iuc.CategoryID in (select ID from ISO_Category where " +
                            "Description like '%Budget%' and RowStatus>-1) and iuc.SectionID in (190,126) and iuc.RowStatus>-1 and iu.RowStatus>-1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);

            strError = dataAccess.Error;
            arrSarPes = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSarPes.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSarPes.Add(new SarmutPes());

            return arrSarPes;
        }

        public ArrayList RetrieveUserCategory(string sarmutPrs)
        {
            arrSarPes = new ArrayList();
                string strSQL = "select iuc.ID,iuc.UserID,iuc.SectionID,iuc.CategoryID,iuc.Bobot,iu.UserName,iu.DeptID,ic.Description,iuc. " +
                                "PesType,ib.Sarmut from ISO_UserCategory as iuc left join ISO_Users as iu on iuc.UserID=iu.ID " +
                                "left join ISO_Category as ic on iuc.CategoryID=ic.ID left join ISO_Bagian as ib on iu.DeptJabatanID=ib.ID " +
                                "where iuc.PesType=1 and ib.Sarmut like '%" + sarmutPrs + "%' and iuc.SectionID>0 and iuc.RowStatus>-1 " +
                                "and iu.RowStatus>-1 order by iu.UserName";
                DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
                strError = dataAccess.Error;
                //arrUsers = new ArrayList();

                if (dataAccess.Error == string.Empty && sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        arrSarPes.Add(GenerateObject(sqlDataReader));
                    }
                }
                return arrSarPes;           
        }
        public ArrayList RetrieveUserCategory2(string sarmutPrs)
        {
            arrSarPes = new ArrayList();
            string strSQL = "select iuc.ID,iuc.UserID,iuc.SectionID,iuc.CategoryID,iuc.Bobot,iu.UserName,iu.DeptID,ic.Description,iuc. " +
                            "PesType,ib.Sarmut from ISO_UserCategory as iuc left join ISO_Users as iu on iuc.UserID=iu.ID " +
                            "left join ISO_Category as ic on iuc.CategoryID=ic.ID left join ISO_Bagian as ib on iu.DeptJabatanID=ib.ID " +
                            "where iuc.PesType=1 and iuc.Sarmut='" + sarmutPrs + "' and iuc.SectionID>0 and iuc.RowStatus>-1 " +
                            "and iu.RowStatus>-1 order by iu.UserName";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            //arrUsers = new ArrayList();

            if (dataAccess.Error == string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSarPes.Add(GenerateObject(sqlDataReader));
                }
            }
            return arrSarPes;
        }
        public ArrayList RetrieveUserCategory3(string sarmutPrs)
        {
            arrSarPes = new ArrayList();
            string strSQL = "select iuc.ID,iuc.UserID,iuc.SectionID,iuc.CategoryID,iuc.Bobot,iu.UserName,iu.DeptID,ic.Description,iuc. " +
                            "PesType,ib.Sarmut,iuc.BagianAutoPES from ISO_UserCategory as iuc left join ISO_Users as iu on iuc.UserID=iu.ID " +
                            "left join ISO_Category as ic on iuc.CategoryID=ic.ID left join ISO_Bagian as ib on iu.DeptJabatanID=ib.ID " +
                            "where iuc.PesType=1 and ib.Sarmut like '%" + sarmutPrs + "%' and iuc.SectionID>0 and iuc.RowStatus>-1 " +
                            "and iu.RowStatus>-1 order by iu.UserName";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            //arrUsers = new ArrayList();

            if (dataAccess.Error == string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSarPes.Add(GenerateObjectMTN(sqlDataReader));
                }
            }
            return arrSarPes;
        }
        public ArrayList RetrieveUserCategory4(string sarmutPrs)
        {
            arrSarPes = new ArrayList();
            string strSQL = "select iuc.ID,iuc.UserID,iuc.SectionID,iuc.CategoryID,iuc.Bobot,iu.UserName,iu.DeptID,ic.Description,iuc. " +
                            "PesType,ib.Sarmut,iuc.BagianAutoPES from ISO_UserCategory as iuc left join ISO_Users as iu on iuc.UserID=iu.ID " +
                            "left join ISO_Category as ic on iuc.CategoryID=ic.ID left join ISO_Bagian as ib on iu.DeptJabatanID=ib.ID " +
                            "where iuc.PesType=1 and iuc.Sarmut='" + sarmutPrs + "' and iuc.SectionID>0 and iuc.RowStatus>-1 " +
                            "and iu.RowStatus>-1 order by iu.UserName";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            //arrUsers = new ArrayList();

            if (dataAccess.Error == string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSarPes.Add(GenerateObjectMTN(sqlDataReader));
                }
            }
            return arrSarPes;
        }

        public ArrayList RetrieveID(int deptID, string ddlBulan, string ddlTahun, int categoryID)
        {
            arrSarPes = new ArrayList();
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select ik.ID,ik.KPINo,ik.KPIName,ik.DeptID,ik.BagianID,ik.CategoryID,ik.NilaiBobot,ik.Keterangan,ik.PIC " +
                            "from ISO_KPI as ik left join ISO_KPIDetail as ikd on ik.ID=ikd.KPIID " +
                            "left join ISO_UserCategory as iuc on ik.CategoryID=iuc.ID " +
                            "where ik.DeptID=" + deptID + " and month(ik.TglMulai)=" + ddlBulan + " " +
                            "AND year(ik.TglMulai)=" + ddlTahun + " and ik.RowStatus>-1 and ikd.RowStatus>-1 " +
                            "and iuc.CategoryID='" + categoryID + "' ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            //arrSarPes = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    //return GenerateObjectUpdate(sqlDataReader);
                    arrSarPes.Add(GenerateObjectUpdate(sqlDataReader));
                }
            }

            return arrSarPes;
        }
        public SarmutPes RetrieveUpdateScore(int categoryID, string ketPes)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "select ID,CategoryID,TargetKe,PointNilai from ISO_SOPScore where CategoryID=" + categoryID + " " +
                            "and RowStatus>-1 and TargetKe='" + ketPes + "' ";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            //arrSarPes = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectScore(sqlDataReader);
                }
            }

            return new SarmutPes();
        }

        public SarmutPes RetrieveJmlWO()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strSQL = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempDataPvt]') AND type in (N'U')) DROP TABLE [dbo].[tempDataPvt] " +
                                          "select A,cast(B as int)B into tempDataPvt from tempData03 " +
                                          "DECLARE @DynamicPivotQuery AS NVARCHAR(MAX) " +
                                          "DECLARE @ColumnName AS NVARCHAR(MAX) " +
                                          "SELECT @ColumnName= ISNULL(@ColumnName + ',','') + QUOTENAME(A) " +
                                          "FROM (SELECT DISTINCT A FROM tempDataPvt) AS A " +
                                          "SET @DynamicPivotQuery = " +
                                          "N'SELECT  ' + @ColumnName + ' " +
                                          "FROM tempDataPvt " +
                                          "PIVOT(sum(B) " +
                                          "FOR [A] IN (' + @ColumnName + ')) AS PVTTable' " +
                                          "EXEC sp_executesql @DynamicPivotQuery";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            //arrSarPes = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectJmlWO(sqlDataReader);
                }
            }

            return new SarmutPes();
        }

        public SarmutPes GenerateObject(SqlDataReader sqlDataReader)
        {
            objSarPes = new SarmutPes();
            objSarPes.IDUserCategory = Convert.ToInt32(sqlDataReader["ID"]);
            objSarPes.UserID = Convert.ToInt32(sqlDataReader["UserID"]);
            objSarPes.BagianID = Convert.ToInt32(sqlDataReader["SectionID"]);
            objSarPes.CategoryID = Convert.ToInt32(sqlDataReader["CategoryID"]);
            objSarPes.BobotNilai = Convert.ToDecimal(sqlDataReader["Bobot"].ToString());
            objSarPes.Pic = sqlDataReader["UserName"].ToString();
            objSarPes.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objSarPes.Description = sqlDataReader["Description"].ToString();
            objSarPes.PesType = Convert.ToInt32(sqlDataReader["PesType"]);
            objSarPes.CategoryID = Convert.ToInt32(sqlDataReader["CategoryID"]);
            return objSarPes;
        }
        public SarmutPes GenerateObjectMTN(SqlDataReader sqlDataReader)
        {
            objSarPes = new SarmutPes();
            objSarPes.IDUserCategory = Convert.ToInt32(sqlDataReader["ID"]);
            objSarPes.UserID = Convert.ToInt32(sqlDataReader["UserID"]);
            objSarPes.BagianID = Convert.ToInt32(sqlDataReader["SectionID"]);
            objSarPes.CategoryID = Convert.ToInt32(sqlDataReader["CategoryID"]);
            objSarPes.BobotNilai = Convert.ToDecimal(sqlDataReader["Bobot"].ToString());
            objSarPes.Pic = sqlDataReader["UserName"].ToString();
            objSarPes.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objSarPes.Description = sqlDataReader["Description"].ToString();
            objSarPes.PesType = Convert.ToInt32(sqlDataReader["PesType"]);
            objSarPes.CategoryID = Convert.ToInt32(sqlDataReader["CategoryID"]);
            objSarPes.BagianAutoPES = sqlDataReader["BagianAutoPES"].ToString();
            return objSarPes;
        }
        public SarmutPes GenerateObjectUpdate(SqlDataReader sqlDataReader)
        {
            objSarPes = new SarmutPes();
            objSarPes.ID = Convert.ToInt32(sqlDataReader["ID"]);

            return objSarPes;
        }
        public SarmutPes GenerateObjectScore(SqlDataReader sqlDataReader)
        {
            objSarPes = new SarmutPes();
            objSarPes.IDScore = Convert.ToInt32(sqlDataReader["ID"]);
            objSarPes.CategoryID = Convert.ToInt32(sqlDataReader["CategoryID"]);
            objSarPes.KetTargetKe = sqlDataReader["TargetKe"].ToString();
            objSarPes.PointNilai = Convert.ToDecimal(sqlDataReader["PointNilai"]);

            return objSarPes;
        }
        public SarmutPes GenerateObjectJmlWO(SqlDataReader sqlDataReader)
        {
            objSarPes = new SarmutPes();
            objSarPes.ElektrikJml2 = Convert.ToDecimal(sqlDataReader["Head Elektrik "]);
            objSarPes.MekanikJml2 = Convert.ToDecimal(sqlDataReader["Head Mekanik "]);
            objSarPes.UtilityJml2 = Convert.ToDecimal(sqlDataReader["Head Utility "]);

            return objSarPes;
        }

        public ArrayList RetrieveUserDefect()
        {
            arrSarPes = new ArrayList();
            string strSQL = " select distinct UserID,Sarmut ItemSarMut from ISO_UserCategory where sarmut like'%defect%' and RowStatus>-1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            //arrUsers = new ArrayList();

            if (dataAccess.Error == string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSarPes.Add(GenerateObjectUserDefect(sqlDataReader));
                }
            }
            return arrSarPes;
        }

        public SarmutPes GenerateObjectUserDefect(SqlDataReader sqlDataReader)
        {
            objSarPes = new SarmutPes();
            objSarPes.UserID = Convert.ToInt32(sqlDataReader["UserID"]);
            objSarPes.ItemSarMut = sqlDataReader["ItemSarMut"].ToString();
            return objSarPes;
        }

        public ArrayList RetrieveUserPES(string sarmutPrs)
        {
            arrSarPes = new ArrayList();
            string strSQL = " select UserID from ISO_UserCategory where Sarmut='" + sarmutPrs + "' and RowStatus>-1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (dataAccess.Error == string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSarPes.Add(GenerateObjectUserPES(sqlDataReader));
                }
            }
            return arrSarPes;
        }

        public SarmutPes GenerateObjectUserPES(SqlDataReader sqlDataReader)
        {
            objSarPes = new SarmutPes();
            objSarPes.UserID = Convert.ToInt32(sqlDataReader["UserID"]);
            return objSarPes;
        }
    }
}
