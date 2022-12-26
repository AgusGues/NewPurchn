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
    public class ISO_DMDFacade : AbstractFacade
    {
        private ISO_UpdDMD objUPDM = new ISO_UpdDMD();
        private ISO_Upd objUPD = new ISO_Upd();
        //private ISO_Upd objUPD = new ISO_Upd();
        private ArrayList arrUPD;
        private List<SqlParameter> sqlListParam;

        public ISO_DMDFacade()
            : base()
        {
        }

        public override int Insert(object objDomain)
        {
            try
            {
                objUPDM = (ISO_UpdDMD)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@DocName", objUPDM.DocName));
                sqlListParam.Add(new SqlParameter("@NoDocument", objUPDM.NoDocument));
                sqlListParam.Add(new SqlParameter("@TglBerlaku", objUPDM.TglBerlaku));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objUPDM.CreatedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPDM.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@RevisiNo", objUPDM.RevisiNo));
                sqlListParam.Add(new SqlParameter("@CategoryUPD", objUPDM.CategoryUPD));               
                sqlListParam.Add(new SqlParameter("@Type", objUPDM.Type));
                sqlListParam.Add(new SqlParameter("@DeptID", objUPDM.DeptID));
                sqlListParam.Add(new SqlParameter("@ID", objUPDM.ID));
                sqlListParam.Add(new SqlParameter("@PlantID", objUPDM.PlantID));
                sqlListParam.Add(new SqlParameter("@StatusShare", objUPDM.StatusShare));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertISO_UpdDMD");

                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int InsertShare(object objDomain)
        {
            try
            {
                objUPDM = (ISO_UpdDMD)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@DocName", objUPDM.DocName));
                sqlListParam.Add(new SqlParameter("@NoDocument", objUPDM.NoDocument));
                sqlListParam.Add(new SqlParameter("@TglBerlaku", objUPDM.TglBerlaku));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objUPDM.CreatedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPDM.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@RevisiNo", objUPDM.RevisiNo));
                sqlListParam.Add(new SqlParameter("@CategoryUPD", objUPDM.CategoryUPD));
                sqlListParam.Add(new SqlParameter("@Type", objUPDM.Type));
                sqlListParam.Add(new SqlParameter("@DeptID", objUPDM.DeptID));
                sqlListParam.Add(new SqlParameter("@ID", objUPDM.DeptID));
                sqlListParam.Add(new SqlParameter("@LinkID", objUPDM.LinkID));
                sqlListParam.Add(new SqlParameter("@PlantID", objUPDM.PlantID));
                sqlListParam.Add(new SqlParameter("@UnitKerjaID", objUPDM.UnitKerjaID));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertISO_UpdDMDShare");

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
                objUPDM = (ISO_UpdDMD)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@id", objUPDM.ID));
               
                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteRules");

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
                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@id", objUPDM.ID));
                int intResult = dataAccess.ProcessData(sqlListParam, "sp_updateUPDStatus");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateH(object objDomain)
        {
            try
            {
                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@IDmaster", objUPD.IDmaster));
                int intResult = dataAccess.ProcessData(sqlListParam, "sp_updateH");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int CancelUPD(object objDomain)
        {
            try
            {
                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objUPD.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));
                int intResult = dataAccess.ProcessData(sqlListParam, "sp_IsoUPDCancel");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int InsertLampiran(object objDomain)
        {
            try
            {
                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@UPDid", objUPD.UPDid));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objUPD.CreatedBy));
                sqlListParam.Add(new SqlParameter("@NamaFile", objUPD.NamaFile));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertISO_UPDLampiran");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateApvPM(object objDomain)
        {
            try
            {
                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objUPD.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));
                //sqlListParam.Add(new SqlParameter("@apv", objUPD.apv));
                int intResult = dataAccess.ProcessData(sqlListParam, "sp_updateApvPM");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateApvLevel1ISO(object objDomain)
        {
            try
            {
                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objUPD.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy)); 
                int intResult = dataAccess.ProcessData(sqlListParam, "sp_updateApvLevel1ISO");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateApvLevel1(object objDomain)
        {
            try
            {
                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objUPD.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));
                //sqlListParam.Add(new SqlParameter("@deptID", objUPD.DeptID));
                sqlListParam.Add(new SqlParameter("@apV", objUPD.apV));
                sqlListParam.Add(new SqlParameter("@TypE", objUPD.TypE));

                int intResult = dataAccess.ProcessData(sqlListParam, "sp_updateApvMgrISO");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateApvMgrHRD(object objDomain)
        {
            try
            {
                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objUPD.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@deptID", objUPD.DeptID));
                sqlListParam.Add(new SqlParameter("@TypE", objUPD.TypE));
                //sqlListParam.Add(new SqlParameter("@apv", objUPD.apv));

                int intResult = dataAccess.ProcessData(sqlListParam, "sp_updateApvHrdMGR");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateApvMgrHRD2(object objDomain)
        {
            try
            {
                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objUPD.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));
                //sqlListParam.Add(new SqlParameter("@deptID", objUPD.DeptID));
                //sqlListParam.Add(new SqlParameter("@TypE", objUPD.TypE));
                //sqlListParam.Add(new SqlParameter("@apv", objUPD.apv));

                int intResult = dataAccess.ProcessData(sqlListParam, "sp_updateApvHrdMGR2");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateApvMgrPurch(object objDomain)
        {
            try
            {
                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objUPD.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));
                //sqlListParam.Add(new SqlParameter("@deptID", objUPD.DeptID));
                sqlListParam.Add(new SqlParameter("@TypE", objUPD.TypE));

                int intResult = dataAccess.ProcessData(sqlListParam, "sp_updateApvPurchMGR");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateApvLevel(object objDomain)
        {
            try
            {
                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objUPD.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));               

                int intResult = dataAccess.ProcessData(sqlListParam, "sp_updateApvLevel");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
            

        public int UpdateApvLevel0(object objDomain)
        {
            try
            {
                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objUPD.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@deptID", objUPD.DeptID));
                sqlListParam.Add(new SqlParameter("@Apv", objUPD.apv));
                //sqlListParam.Add(new SqlParameter("@TypE", objUPD.TypE));

                int intResult = dataAccess.ProcessData(sqlListParam, "sp_updateApvLevel0");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateApv2(object objDomain)
        {
            try
            {
                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objUPD.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));
                //sqlListParam.Add(new SqlParameter("@deptID", objUPD.DeptID));
                sqlListParam.Add(new SqlParameter("@Apv", objUPD.apv));                
                int intResult = dataAccess.ProcessData(sqlListParam, "sp_updateApv2");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateApv1(object objDomain)
        {
            try
            {
                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objUPD.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy2", objUPD.LastModifiedBy2));    
                sqlListParam.Add(new SqlParameter("@Apv", objUPD.apv));
                sqlListParam.Add(new SqlParameter("@RowStatus", objUPD.RowStatus));
                sqlListParam.Add(new SqlParameter("@DeptID", objUPD.DeptID));
                sqlListParam.Add(new SqlParameter("@type", objUPD.type));
                int intResult = dataAccess.ProcessData(sqlListParam, "sp_updateApv1");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateApvLevel3ISO(object objDomain)
        {
            try
            {
                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objUPD.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));
                int intResult = dataAccess.ProcessData(sqlListParam, "sp_updateApvLevel3ISO");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateApvLevel1HRD(object objDomain)
        {
            try
            {
                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objUPD.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));
                int intResult = dataAccess.ProcessData(sqlListParam, "sp_updateApvLevel1HRD");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateApvLevel3HRD(object objDomain)
        {
            try
            {
                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objUPD.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));
                int intResult = dataAccess.ProcessData(sqlListParam, "sp_updateApvLevel3HRD");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateApvLevel3(object objDomain)
        {
            try
            {
                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objUPD.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));
                int intResult = dataAccess.ProcessData(sqlListParam, "sp_updateApvLevel3");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateApvHeadISO(object objDomain)
        {
            try
            {
                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objUPD.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@apV", objUPD.apV));
                sqlListParam.Add(new SqlParameter("@TypE", objUPD.TypE));
                int intResult = dataAccess.ProcessData(sqlListParam, "sp_updateHeadISO");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateNotApvPM(object objDomain)
        {
            try
            {
                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
               
                sqlListParam.Add(new SqlParameter("@NoDokumen", objUPD.NoDokumen));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@alasan2", objUPD.Alasan2));

                int intResult = dataAccess.ProcessData(sqlListParam, "sp_updateApv2");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateApvISO(object objDomain)
        {
            try
            {

                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@id", objUPDM.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));
                //sqlListParam.Add(new SqlParameter("@apv", objUPD.apv));


                int intResult = dataAccess.ProcessData(sqlListParam, "sp_updateApvISO");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateLampiran(object objDomain)
        {
            try
            {
                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objUPD.ID));
                sqlListParam.Add(new SqlParameter("@FileNama", objUPD.FileNama));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@DeptID", objUPD.DeptID));
                int intResult = dataAccess.ProcessData(sqlListParam, "sp_UpdateUpdLampiranRev");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateShare(object objDomain)
        {
            try
            {
                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@NoDokumen", objUPD.NoDokumen));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));
                //sqlListParam.Add(new SqlParameter("@Keterangan", objUPD.Keterangan));
                sqlListParam.Add(new SqlParameter("@NamaDokumen", objUPD.NamaDokumen));
                sqlListParam.Add(new SqlParameter("@Type", objUPD.Type));
                sqlListParam.Add(new SqlParameter("@IDmaster", objUPD.IDmaster));

                int intResult = dataAccess.ProcessData(sqlListParam, "UPD_SP_UpdateShareDokumen");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateYesNoApproved(object objDomain)
        {
            try
            {
                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@NoDokumen", objUPD.NoDokumen));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@NamaDokumen", objUPD.NamaDokumen));
                sqlListParam.Add(new SqlParameter("@Tipe", objUPD.Type));
                sqlListParam.Add(new SqlParameter("@Alasan", objUPD.Alasan));
                //sqlListParam.Add(new SqlParameter("@Keterangan", objUPD.Keterangan));

                int intResult = dataAccess.ProcessData(sqlListParam, "UPD_SP_UpdateYesNoApprove");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateNoApproved(object objDomain)
        {
            //try
            //{
            //    objUPD = (ISO_Upd)objDomain;
            //    sqlListParam = new List<SqlParameter>();
            //    sqlListParam.Add(new SqlParameter("@NoDokumen", objUPD.NoDokumen));
            //    sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));
            //    sqlListParam.Add(new SqlParameter("@Alasan", objUPD.Alasan));

            //    int intResult = dataAccess.ProcessData(sqlListParam, "UPD_SP_UpdateNoApprove");
            //    strError = dataAccess.Error;
            //    return intResult;
            //}
            //catch (Exception ex)
            //{
            //    strError = ex.Message;
            //    return -1;
            //}

            try
            {
                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@NoDokumen", objUPD.NoDokumen));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@NamaDokumen", objUPD.NamaDokumen));
                sqlListParam.Add(new SqlParameter("@Tipe", objUPD.Type));
                //sqlListParam.Add(new SqlParameter("@Keterangan", objUPD.Keterangan));

                int intResult = dataAccess.ProcessData(sqlListParam, "UPD_SP_UpdateNoApprove");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateNotShare(object objDomain)
        {
            try
            {
                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@NoDokumen", objUPD.NoDokumen));
                sqlListParam.Add(new SqlParameter("@NamaDokumen", objUPD.NamaDokumen));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@AlasanNo", objUPD.AlasanNo));
                sqlListParam.Add(new SqlParameter("@Tipe", objUPD.Type));

                int intResult = dataAccess.ProcessData(sqlListParam, "UPD_SP_UpdateNoShareDokumen");
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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from Rules where RowStatus = 0 order by rulename");
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new Rules());

            return arrUPD;
        }
        public ArrayList RetrieveByapv(string apv)
        {
            string strSQL = "SELECT A.id,A.itemid,B.partno,C.lokasi,A.qty,A.itemid " +
                "FROM  t3_serah AS A , fc_items AS B , fc_lokasi as C where A.itemid=B.id and A.lokid=C.id and A.qty>0 and A.lokid in (select id from fc_lokasi where lokasi='bsauto')";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public int UpdateMasterRev(object objDomain)
        {
            try
            {

                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objUPD.ID));
                sqlListParam.Add(new SqlParameter("@RevNo", objUPD.RevNo));
                sqlListParam.Add(new SqlParameter("@Tanggal", objUPD.Tanggal));
                sqlListParam.Add(new SqlParameter("@FormNO", objUPD.FormNO));

                int intResult = dataAccess.ProcessData(sqlListParam, "sp_updateMasterRev");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public ISO_UpdDMD  RetrieveByUPDno(string strUPDM)
        {
            string strsql = "select * from iso_upddmd where nodocument='" + strUPDM + "'" ;
           
            
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new ISO_UpdDMD();
        }

        public ISO_UpdDMD RetrieveByNama(string strNama)
        {
            string strsql = "select * from iso_upddmd where docname='" + strNama + "' and rowstatus > -1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new ISO_UpdDMD();
        }

        public ISO_Upd cekDept(string UpdID)
        {
            string strsql = "select DeptID from ISO_UPD where ID=" + UpdID + " and RowStatus > -1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectDept(sqlDataReader);
                }
            }
            return new ISO_Upd();
        }

        public ISO_Upd GenerateObjectDept(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            return objUPD;
        }

        public ISO_UpdDMD RetrieveByNamaWithNomor(string strNomor)
        {
            string strsql = "select * from iso_upddmd where NoDocument='" + strNomor + "' and rowstatus > -1 and aktif=2";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new ISO_UpdDMD();
        }

        public ISO_UpdDMD GenerateObject(SqlDataReader sqlDataReader)
        {
            objUPDM = new ISO_UpdDMD();
            objUPDM.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objUPDM.iD = sqlDataReader["iD"].ToString();
            objUPDM.DocName = sqlDataReader["DocName"].ToString();
            objUPDM.CategoryUPD = sqlDataReader["CategoryUPD"].ToString();
            objUPDM.Deptm = sqlDataReader["Deptm"].ToString();
            objUPDM.NoDocument = sqlDataReader["NoDocument"].ToString();
            objUPDM.Type = sqlDataReader["Type"].ToString();

            
         
            return objUPDM;
        }

        public int CekLampiran(int ID)
        {
            string StrSql = " select ID from iso_updlampiran where RowStatus>-1 and IDupd=" + ID + " ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["ID"]);
                }
            }

            return 0;
        }

        public ISO_UpdDMD RetrieveData(int IDMaster)
        {
            string strsql = "select * from iso_upddmd where ID='" + IDMaster + "'";


            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject_RData(sqlDataReader);
                }
            }
            return new ISO_UpdDMD();
        }

        public ISO_UpdDMD GenerateObject_RData(SqlDataReader sqlDataReader)
        {
            objUPDM = new ISO_UpdDMD();
            objUPDM.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objUPDM.StatusShare = Convert.ToInt32(sqlDataReader["StatusShare"]);
            objUPDM.PlantID = Convert.ToInt32(sqlDataReader["PlantID"]);
            objUPDM.CategoryUPD = sqlDataReader["CategoryUPD"].ToString();
            objUPDM.Dept = sqlDataReader["Dept"].ToString();
            //objUPDM.NoDocument = sqlDataReader["NoDocument"].ToString();
            objUPDM.Type = sqlDataReader["Type"].ToString();



            return objUPDM;
        }

        public int CekShare(int ID)
        {
            string StrSql = 
                " select SUM(ID)StatusShare from (select ID from ISO_UpdDMD where ID="+ID+" and "+
                " StatusShare in (2,4) and Aktif=1 and RowStatus>-1 "+
                " union all "+
                " select '0'ID from ISO_UpdDMD ) as Data1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["StatusShare"]);
                }
            }

            return 0;
        }

        //public int RetrieveRev(int ID)
        //{
        //    string StrSql =
        //        " select RevisiNo from iso_upd where id="+ID+"";
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
        //    strError = dataAccess.Error;

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            return Convert.ToInt32(sqlDataReader["RevisiNo"]);
        //        }
        //    }

        //    return 0;
        //}

        public string RetrieveNamaFile(int ID)
        {
            string result = string.Empty;
            string StrSql = " select NamaFile from ISO_UPDLampiran where IDupd in (select ID from ISO_UPD where ID="+ID+" and RowStatus > -1) and RowStatus > -1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    result = sqlDataReader["NamaFile"].ToString();
                }
            }

            return result;
        }

        public ISO_UpdDMD RetrieveData(string NoUPD)
        {
            string strsql = "  select B.Alasan2 Alasan from ISO_UPD A INNER JOIN ISO_UpdDetail B ON A.ID=B.UPDid where A.RowStatus=-2  and A.UpdNo='"+NoUPD+"' ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectData(sqlDataReader);
                }
            }
            return new ISO_UpdDMD();
        }

        public ISO_UpdDMD GenerateObjectData(SqlDataReader sqlDataReader)
        {
            objUPDM = new ISO_UpdDMD();            
            objUPDM.Alasan = sqlDataReader["Alasan"].ToString();
           
            return objUPDM;
        }

        public ISO_UpdDMD RetrieveAlasan(string NoUPD)
        {
            string result = string.Empty;
            string StrSql = 
            " select A.IDmaster,B.Alasan,A.ID from ISO_UPD A INNER JOIN ISO_UpdDetail B ON A.ID=B.UPDid where A.RowStatus>-1 and B.RowStatus>-1 and A.UpdNo='"+NoUPD+"' ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectData2(sqlDataReader);
                }
            }
            return new ISO_UpdDMD();
        }

        public ISO_UpdDMD GenerateObjectData2(SqlDataReader sqlDataReader)
        {
            objUPDM = new ISO_UpdDMD();
            objUPDM.Alasan = sqlDataReader["Alasan"].ToString();
            objUPDM.IDmaster = Convert.ToInt32(sqlDataReader["IDmaster"]);
            objUPDM.ID = Convert.ToInt32(sqlDataReader["ID"]);
            return objUPDM;
        }

        public ISO_UpdDMD RetrieveBiasa(string NoDokumen)
        {
            string result = string.Empty;
            string StrSql = " select ID IDmaster,NoDocument,DocName,(select top 1 DeptName from ISO_Dept dp where dp.DeptID=dept)DeptName,(select doccategory from ISO_UpdDocCategory cat where cat.id=categoryupd and cat.RowStatus>-1)CategoryUPD from ISO_UpdDMD where NoDocument='" + NoDokumen + "' and RowStatus>-1 and Aktif>0 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectData3(sqlDataReader);
                }
            }
            return new ISO_UpdDMD();
        }

        public ISO_UpdDMD RetrieveKhusus(string NamaDokumen)
        {
            string result = string.Empty;
            string StrSql = " select ID IDmaster,NoDocument,DocName,(select top 1 DeptName from ISO_Dept dp where dp.DeptID=dept)DeptName,(select doccategory from ISO_UpdDocCategory cat where cat.id=categoryupd and cat.RowStatus>-1)CategoryUPD from ISO_UpdDMD where DocName='" + NamaDokumen + "' and RowStatus>-1 and Aktif>0 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectData3(sqlDataReader);
                }
            }
            return new ISO_UpdDMD();
        }

        public ISO_UpdDMD GenerateObjectData3(SqlDataReader sqlDataReader)
        {
            objUPDM = new ISO_UpdDMD();
            objUPDM.NoDocument = sqlDataReader["NoDocument"].ToString();
            objUPDM.DocName = sqlDataReader["DocName"].ToString();
            objUPDM.DeptName = sqlDataReader["DeptName"].ToString();
            objUPDM.CategoryUPD = sqlDataReader["CategoryUPD"].ToString();
            objUPDM.IDmaster = Convert.ToInt32(sqlDataReader["IDmaster"]);
            return objUPDM;
        }

        public ISO_UpdDMD RetrieveData2Insert(int ID)
        {
            string result = string.Empty;
            string StrSql = " select * from ISO_UPD where ID="+ID+" ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectData2Insert(sqlDataReader);
                }
            }
            return new ISO_UpdDMD();
        }

        public ISO_UpdDMD GenerateObjectData2Insert(SqlDataReader sqlDataReader)
        {
            objUPDM = new ISO_UpdDMD();
            objUPDM.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objUPDM.Type = sqlDataReader["Type"].ToString();    
            objUPDM.DeptID = sqlDataReader["DeptID"].ToString();           
            objUPDM.CategoryUPD = sqlDataReader["CategoryUPD"].ToString();         
            return objUPDM;
        }

        public ISO_UpdDMD RetrieveRev(int ID)
        {
            string StrSql =
                " select *,case when IDMaster1='' then '0' else IDMaster1 end IDMaster from ( " +
                " select ID,Type,DeptID,CategoryUPD,RevisiNo,isnull(IDmaster,'0')IDMaster1 from iso_upd where id=" + ID + ""+
                " ) as x ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectRetrieveRev(sqlDataReader);
                }
            }
            return new ISO_UpdDMD();
        }

        public ISO_UpdDMD GenerateObjectRetrieveRev(SqlDataReader sqlDataReader)
        {
            objUPDM = new ISO_UpdDMD();
            objUPDM.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objUPDM.Type = sqlDataReader["Type"].ToString();
            objUPDM.DeptID = sqlDataReader["DeptID"].ToString();
            objUPDM.CategoryUPD = sqlDataReader["CategoryUPD"].ToString();
            objUPDM.RevisiNo = sqlDataReader["RevisiNo"].ToString();
            objUPDM.IDmaster = Convert.ToInt32(sqlDataReader["IDmaster"]);
            return objUPDM;
        }

    }
}

   
