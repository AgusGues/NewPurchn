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

namespace BusinessFacade
{
    public class ISO_UPD2Facade : AbstractFacade
    {
        
        private ISO_Upd objUPD = new ISO_Upd();
        private ISO_UpdDMD objUPD2 = new ISO_UpdDMD();
        private ArrayList arrUPD;
        private List<SqlParameter> sqlListParam;

        public ISO_UPD2Facade()
            : base()
        {
        }

        public int KirimFileOtherPlant(object objDomain)
        {
            try
            {
                objUPD2 = (ISO_UpdDMD)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@NamaFile", objUPD2.NamaFile));
                sqlListParam.Add(new SqlParameter("@Unit", objUPD2.Unit)); 

                int intResult = dataAccess.ProcessData(sqlListParam, "WorkOrder_KirimFileOtherPlant");
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
                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                //sqlListParam.Add(new SqlParameter("@DocName", objUPD2.DocName));
                //sqlListParam.Add(new SqlParameter("@NoDocument", objUPD2.NoDocument));
                sqlListParam.Add(new SqlParameter("@TglBerlaku", objUPD.TglBerlaku));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objUPD.CreatedBy));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));
                //sqlListParam.Add(new SqlParameter("@RowStatus", objUPDM.RowStatus));
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
        public override int Delete(object objDomain)
        {

            try
            {
                objUPD= (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@id", objUPD.ID));
              
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
                sqlListParam.Add(new SqlParameter("@id", objUPD.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "sp_updateUPDApv");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateNotApv(object objDomain)
        {
            try
            {

                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@id", objUPD.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "sp_updateUPDApv2");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateCancel(object objDomain)
        {
            try
            {

                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@id", objUPD.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "sp_updateUPDcancel");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int Hapus(object objDomain)
        {
            try
            {

                objUPD2 = (ISO_UpdDMD)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objUPD2.ID));
                //sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "sp_CancelUPDDistribusi");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int HapusMasterUPD(object objDomain)
        {
            try
            {

                objUPD2 = (ISO_UpdDMD)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objUPD2.ID));               

                int intResult = dataAccess.ProcessData(sqlListParam, "UPD_SP_HapusMaster");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateHRD(object objDomain)
        {
            try
            {

                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@id", objUPD.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "sp_updateUPDhrd");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateHRDKhusus(object objDomain)
        {
            try
            {

                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@id", objUPD.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "sp_updateUPDhrdKhusus");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateMasterStatus(object objDomain)
        {
            try
            {

                objUPD2 = (ISO_UpdDMD)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@IDmaster", objUPD2.IDmaster));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD2.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "sp_updateMasterStatus");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateMasterStatusShare(object objDomain)
        {
            try
            {

                objUPD2 = (ISO_UpdDMD)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@IDmaster", objUPD2.IDmaster));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD2.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "sp_updateMasterStatusShare");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateDataShare(object objDomain)
        {
            try
            {

                objUPD2 = (ISO_UpdDMD)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@id", objUPD2.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD2.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "UPD_SP_UpdateShare");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateDataUPD(object objDomain)
        {
            try
            {

                objUPD2 = (ISO_UpdDMD)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objUPD2.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD2.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "UPD_SP_UpdateUPD");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int InsertD(object objDomain)
        {
            try
            {

                objUPD2 = (ISO_UpdDMD)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objUPD2.ID));
                sqlListParam.Add(new SqlParameter("@IDmaster", objUPD2.IDmaster));
                sqlListParam.Add(new SqlParameter("@Kategory", objUPD2.Kategory));
                sqlListParam.Add(new SqlParameter("@Dept2", objUPD2.Dept2));
                sqlListParam.Add(new SqlParameter("@Urutan", objUPD2.Urutan));
              
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertISO_UPDFile");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int HapusRecord(object objDomain)
        {
            try
            {

                objUPD2 = (ISO_UpdDMD)objDomain;
                sqlListParam = new List<SqlParameter>();
               
                sqlListParam.Add(new SqlParameter("@IDmaster", objUPD2.IDmaster));                

                int intResult = dataAccess.ProcessData(sqlListParam, "UPD_SP_HapusDistribusi");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int InsertDokumen(object objDomain)
        {
            try
            {

                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@NoDokumen", objUPD.NoDokumen));
                sqlListParam.Add(new SqlParameter("@NamaDokumen", objUPD.NamaDokumen));
                sqlListParam.Add(new SqlParameter("@RevisiNo", objUPD.RevisiNo));
                sqlListParam.Add(new SqlParameter("@DeptID", objUPD.DeptID));
                sqlListParam.Add(new SqlParameter("@TglBerlaku", objUPD.TglBerlaku));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objUPD.CreatedBy));
                sqlListParam.Add(new SqlParameter("@CategoryUPD", objUPD.CategoryUPD));
                sqlListParam.Add(new SqlParameter("@Type", objUPD.Type));
                sqlListParam.Add(new SqlParameter("@PlantID", objUPD.PlantID));
                //sqlListParam.Add(new SqlParameter("@LastModifiedBy", objUPD.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertISO_UPDMaster");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int InsertLampiranFile(object objDomain)
        {
            try
            {

                objUPD = (ISO_Upd)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@updID", objUPD.updID));
                sqlListParam.Add(new SqlParameter("@Lampiran", objUPD.Lampiran));

                int intResult = dataAccess.ProcessData(sqlListParam, "sp_ISO_UPDinsertFileLampiran");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public int InsertDisFile(object objDomain)
        {
            try
            {
                objUPD2 = (ISO_UpdDMD)objDomain;
                sqlListParam = new List<SqlParameter>();               
                sqlListParam.Add(new SqlParameter("@IDmaster", objUPD2.IDmaster));  
                sqlListParam.Add(new SqlParameter("@FileName", objUPD2.filename));
                //sqlListParam.Add(new SqlParameter("@attachfile", objUPD2.attachfile));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objUPD2.CreatedBy));
                sqlListParam.Add(new SqlParameter("@CreatedTime", objUPD2.CreatedTime));
                sqlListParam.Add(new SqlParameter("@ID", objUPD2.ID));
                sqlListParam.Add(new SqlParameter("@Kategory", objUPD2.Kategory));
                sqlListParam.Add(new SqlParameter("@Dept2", objUPD2.Dept2));
                
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertISO_DisFile1");
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

        public int UpdateDisFile(object objDomain)
        {
            try
            {

                objUPD2 = (ISO_UpdDMD)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@IDmaster", objUPD2.IDmaster));
                sqlListParam.Add(new SqlParameter("@FileName", objUPD2.FileName));

                int intResult = dataAccess.ProcessData(sqlListParam, "sp_updateDisUPD");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateDisFile2(object objDomain)
        {
            try
            {

                objUPD2 = (ISO_UpdDMD)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@IDmaster", objUPD2.IDmaster));
                sqlListParam.Add(new SqlParameter("@RevisiNo", objUPD2.RevisiNo));
                sqlListParam.Add(new SqlParameter("@TglBerlaku", objUPD2.TglBerlaku));
                sqlListParam.Add(new SqlParameter("@DocName", objUPD2.DocName));
                sqlListParam.Add(new SqlParameter("@NoDocument", objUPD2.NoDocument));

                int intResult = dataAccess.ProcessData(sqlListParam, "sp_updateDisUPD2");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public ArrayList RetrieveStatus0(string status)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.UpdNo,A.UpdName,A.tglpengajuan,C.DeptName,A.categoryupd,A.apv,A.NamaDokumen " +
                "from ISO_UPD as A, ISO_UpdDetail as B, Dept as C, ISO_UpdDocCategory as D where A.categoryupd=D.id and A.DeptID=C.ID and  A.ID=B.UPDid and A.Apv=0 " +
                "and B.RowStatus=0 order by A.ID desc ");


            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ArrayList RetrieveUPDid(int id)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.UpdNo,A.UpdName,A.tglpengajuan,C.DeptName,A.categoryupd,A.apv,A.NamaDokumen " +
                "from ISO_UPD as A, ISO_UpdDetail as B, Dept as C, ISO_UpdDocCategory as D where A.categoryupd=D.id and A.DeptID=C.ID and  A.ID=B.UPDid and A.Apv=0 " +
                "and B.RowStatus=0 order by A.ID desc ");


            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectHeader(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_Upd());

            return arrUPD;
        }

        public ISO_Upd RetrieveByDMD(string NoDocument)
        {
            string strSQL = "SELECT docname from iso_upddmd where  nodocument='" + NoDocument + "'";
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
            return new ISO_Upd();
        }

        //public ISO_Upd RetrieveFile(int ID)
        //{
        //    string strSQL = " select B.NamaFile,D.[FileName]FileLama from ISO_UPD as A LEFT JOIN ISO_UPDLampiran as B ON A.ID=B.IDupd "+
        //                    " LEFT JOIN ISO_UpdDMD as C ON A.IDmaster=C.ID LEFT JOIN ISO_UPDdistribusiFile as D ON C.ID=D.idMaster "+
        //                    " where A.ID in ("+ID+") and (B.RowStatus > -1 or D.RowStatus>-1 ) ";
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        //    strError = dataAccess.Error;
        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            return GenerateObjectFile(sqlDataReader);
        //        }
        //    }
        //    return new ISO_Upd();
        //}

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

        public ArrayList RetrieveByUPDFilter(string perintah, string Tanda)
        {
            string query = string.Empty;
            string query1 = string.Empty;

            if (perintah == "CreatedTime")
            {
                query = "Order By ID desc";
            }

            if (Tanda == "2")
            {
                query1 =
                " select ROW_NUMBER() over (order by ID asc) as No,* from ( " +
                " select *,ISNULL(DeptName1,'-')DeptName from (select ID,NoDocument,DocName,(select DocCategory from ISO_UpdDocCategory as  C " +
                " where C.ID=A.CategoryUPD and C.RowStatus > -1) as CategoryUPD ,A.CategoryUPD Kategory, A.Dept Dept2, " +
                " (select top 1 Alias from Dept as z where z.ID=A.Dept and z.RowStatus > -1 order by z.ID desc)  as DeptName1 " +
                " ,A.CategoryUPD IDCategoryUPD,A.[Type] from ISO_UpdDMD as A where LEFT(convert(char,tglberlaku,112),6)>='201807' " +
                " and RowStatus > -1 and aktif=1 and (A.StatusShare is null or A.StatusShare in (0,1) or A.StatusShare = 11)" +                            
                " and A.RowStatus > -1) as Data1 ) as x " + query + "";
            }
            else if (Tanda == "1")
            {
                query1 =
               " select ROW_NUMBER() over (order by ID asc) as No,* from ( " +
               " select ID,NoDocument,DocName,(select DocCategory from ISO_UpdDocCategory as  C  where C.ID=A.CategoryUPD and C.RowStatus > -1) " +
               " as CategoryUPD ,A.CategoryUPD Kategory, A.Dept Dept2, (select top 1 Alias from Dept as z where z.ID=A.Dept and z.RowStatus > -1 order by z.ID desc)  as DeptName  " +
               " ,A.CategoryUPD,A.[Type] from ISO_UpdDMD as A where  A.Aktif =1 and A.RowStatus > -1 and A.StatusShare=1 " +
               " ) as x order by Dept,CategoryUPD,DocName ";
            }
            //string strSQL = " select ID,NoDocument,DocName,(select DocCategory from ISO_UpdDocCategory as  C " +
            //                " where C.ID=A.CategoryUPD and C.RowStatus > -1) as CategoryUPD ,A.CategoryUPD Kategory, A.Dept Dept2, " +
            //                " (select DeptName from Dept as z where z.ID=A.Dept and z.RowStatus > -1) " +
            //                " as DeptName,A.CategoryUPD,A.[Type] from ISO_UpdDMD as A where LEFT(convert(char,tglberlaku,112),6)>='201605' "+
            //                " and RowStatus > -1 and aktif=1 and (A.StatusShare is null or A.StatusShare = 0)" +
            //                "union all " +
            //                "select ID,NoDocument,DocName,(select DocCategory from ISO_UpdDocCategory as  C  where C.ID=A.CategoryUPD and " + 
            //                "C.RowStatus > -1) as CategoryUPD ,A.CategoryUPD Kategory, A.Dept Dept2,  (select DeptName from Dept as z "+
            //                "where z.ID=A.Dept and z.RowStatus > -1)  as DeptName,A.CategoryUPD,A.[Type] from ISO_UpdDMD as A where A.ID "+
            //                "not in (select idmaster from ISO_UPDrelasi where RowStatus > -1) and A.Aktif =1 and LEFT(convert(char,A.tglberlaku,112),6)<'201605' "+
            //                "and A.RowStatus > -1 " + query + "";

            string strSQL = query1;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObject3(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_UpdDMD());

            return arrUPD;
        }

        public ArrayList RetrieveByUPDFilter2(string perintah, string perintah2, string Tanda)
        {
            string query = string.Empty;
            string query1 = string.Empty;
            if (Tanda == "2")
            {
                query1 =
                " select ROW_NUMBER() over (order by ID asc) as No,* from ( " +
                " select ID,NoDocument,DocName,(select DocCategory from ISO_UpdDocCategory as  C " +
                " where C.ID=A.CategoryUPD and C.RowStatus > -1) as CategoryUPD ,A.CategoryUPD Kategory, A.Dept Dept2, " +
                " (select DeptName from Dept as z where z.ID=A.Dept and z.RowStatus > -1) " +
                " as DeptName,A.CategoryUPD CategoryUPD_ID,A.[Type] from ISO_UpdDMD as A where LEFT(convert(char,tglberlaku,112),6)>='201605' " +
                "and RowStatus > -1 and aktif=1 and A.Dept=" + perintah2 + " and A.CategoryUPD=" + perintah + " and (A.StatusShare is null or A.StatusShare = 0 or A.StatusShare = 11)" +
                "union all " +
                "select ID,NoDocument,DocName,(select DocCategory from ISO_UpdDocCategory as  C  where C.ID=A.CategoryUPD and " +
                "C.RowStatus > -1) as CategoryUPD ,A.CategoryUPD Kategory, A.Dept Dept2,  (select DeptName from Dept as z " +
                "where z.ID=A.Dept and z.RowStatus > -1)  as DeptName,A.CategoryUPD CategoryUPD_ID,A.[Type] from ISO_UpdDMD as A where A.ID " +
                "not in (select idmaster from ISO_UPDrelasi where RowStatus > -1) and A.Aktif =1 and LEFT(convert(char,A.tglberlaku,112),6)<'201605' " +
                "and A.RowStatus > -1 and A.Dept=" + perintah2 + " and A.CategoryUPD=" + perintah + " ) as x";
            }
            else if (Tanda == "1")
            {
                query1 =
                " select ROW_NUMBER() over (order by ID asc) as No,* from ( " +    
                " select ID,NoDocument,DocName,(select DocCategory from ISO_UpdDocCategory as  C  where C.ID=A.CategoryUPD and C.RowStatus > -1) " +
                " as CategoryUPD ,A.CategoryUPD Kategory, A.Dept Dept2,  (select DeptName from Dept as z where z.ID=A.Dept and z.RowStatus > -1)  " +
                " as DeptName,A.CategoryUPD CategoryUPD_ID,A.[Type] from ISO_UpdDMD as A where  A.Aktif =1 and A.RowStatus > -1 and A.StatusShare=1 " +
                " and  A.Dept=" + perintah2 + " and A.CategoryUPD=" + perintah + " ) as x" +
                " order by Dept,CategoryUPD,DocName ";
            }
            //string strSQL = " select ID,NoDocument,DocName,(select DocCategory from ISO_UpdDocCategory as  C " +
            //                " where C.ID=A.CategoryUPD and C.RowStatus > -1) as CategoryUPD ,A.CategoryUPD Kategory, A.Dept Dept2, " +
            //                " (select DeptName from Dept as z where z.ID=A.Dept and z.RowStatus > -1) " +
            //                " as DeptName,A.CategoryUPD,A.[Type] from ISO_UpdDMD as A where LEFT(convert(char,tglberlaku,112),6)>='201605' "+
            //                "and RowStatus > -1 and aktif=1 and A.Dept=" + perintah2 + " and A.CategoryUPD=" + perintah + " " +
            //                "union all " +
            //                "select ID,NoDocument,DocName,(select DocCategory from ISO_UpdDocCategory as  C  where C.ID=A.CategoryUPD and " +
            //                "C.RowStatus > -1) as CategoryUPD ,A.CategoryUPD Kategory, A.Dept Dept2,  (select DeptName from Dept as z " +
            //                "where z.ID=A.Dept and z.RowStatus > -1)  as DeptName,A.CategoryUPD,A.[Type] from ISO_UpdDMD as A where A.ID " +
            //                "not in (select idmaster from ISO_UPDrelasi where RowStatus > -1) and A.Aktif =1 and LEFT(convert(char,A.tglberlaku,112),6)<'201605' " +
            //                "and A.RowStatus > -1 and A.Dept=" + perintah2 + " and A.CategoryUPD=" + perintah + "";
            string strSQL = query1;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObject3(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_UpdDMD());

            return arrUPD;
        }

        public ArrayList RetrieveByUPDFilterByDept(string perintah, string perintah2, string Tanda)
        {
            string query = string.Empty;
            string query1 = string.Empty;
            if (Tanda == "2")
            {
                query1 =
                " select ROW_NUMBER() over (order by ID asc) as No,* from ( " +
                " select ID,NoDocument,DocName,(select DocCategory from ISO_UpdDocCategory as  C " +
                " where C.ID=A.CategoryUPD and C.RowStatus > -1) as CategoryUPD ,A.CategoryUPD Kategory, A.Dept Dept2, " +
                " (select DeptName from Dept as z where z.ID=A.Dept and z.RowStatus > -1) " +
                " as DeptName,A.CategoryUPD CategoryUPD_ID,A.[Type] from ISO_UpdDMD as A where LEFT(convert(char,tglberlaku,112),6)>='201605' " +
                "and RowStatus > -1 and aktif=1 and A.Dept=" + perintah2 + " and (A.StatusShare is null or A.StatusShare = 0 or A.StatusShare = 11)" +
                "union all " +
                "select ID,NoDocument,DocName,(select DocCategory from ISO_UpdDocCategory as  C  where C.ID=A.CategoryUPD and " +
                "C.RowStatus > -1) as CategoryUPD ,A.CategoryUPD Kategory, A.Dept Dept2,  (select DeptName from Dept as z " +
                "where z.ID=A.Dept and z.RowStatus > -1)  as DeptName,A.CategoryUPD CategoryUPD_ID,A.[Type] from ISO_UpdDMD as A where A.ID " +
                "not in (select idmaster from ISO_UPDrelasi where RowStatus > -1) and A.Aktif =1 and LEFT(convert(char,A.tglberlaku,112),6)<'201605' " +
                "and A.RowStatus > -1 and A.Dept=" + perintah2 + " ) as x";
            }
            else if (Tanda == "1")
            {
                query1 =
                " select ROW_NUMBER() over (order by ID asc) as No,* from ( " +
                " select ID,NoDocument,DocName,(select DocCategory from ISO_UpdDocCategory as  C  where C.ID=A.CategoryUPD and C.RowStatus > -1) " +
                " as CategoryUPD ,A.CategoryUPD Kategory, A.Dept Dept2,  (select DeptName from Dept as z where z.ID=A.Dept and z.RowStatus > -1)  " +
                " as DeptName,A.CategoryUPD CategoryUPD_ID,A.[Type] from ISO_UpdDMD as A where  A.Aktif =1 and A.RowStatus > -1 and A.StatusShare=1 " +
                " and  A.Dept=" + perintah2 + " ) as x" +
                " order by Dept,CategoryUPD,DocName ";
            }
            //string strSQL = " select ID,NoDocument,DocName,(select DocCategory from ISO_UpdDocCategory as  C " +
            //                " where C.ID=A.CategoryUPD and C.RowStatus > -1) as CategoryUPD ,A.CategoryUPD Kategory, A.Dept Dept2, " +
            //                " (select DeptName from Dept as z where z.ID=A.Dept and z.RowStatus > -1) " +
            //                " as DeptName,A.CategoryUPD,A.[Type] from ISO_UpdDMD as A where LEFT(convert(char,tglberlaku,112),6)>='201605' "+
            //                "and RowStatus > -1 and aktif=1 and A.Dept=" + perintah2 + " and A.CategoryUPD=" + perintah + " " +
            //                "union all " +
            //                "select ID,NoDocument,DocName,(select DocCategory from ISO_UpdDocCategory as  C  where C.ID=A.CategoryUPD and " +
            //                "C.RowStatus > -1) as CategoryUPD ,A.CategoryUPD Kategory, A.Dept Dept2,  (select DeptName from Dept as z " +
            //                "where z.ID=A.Dept and z.RowStatus > -1)  as DeptName,A.CategoryUPD,A.[Type] from ISO_UpdDMD as A where A.ID " +
            //                "not in (select idmaster from ISO_UPDrelasi where RowStatus > -1) and A.Aktif =1 and LEFT(convert(char,A.tglberlaku,112),6)<'201605' " +
            //                "and A.RowStatus > -1 and A.Dept=" + perintah2 + " and A.CategoryUPD=" + perintah + "";
            string strSQL = query1;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObject3(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_UpdDMD());

            return arrUPD;
        }

        public ArrayList RetrieveByUPD()
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            //string strSQL = " select ID,NoDocument,DocName,(select DocCategory from ISO_UpdDocCategory as  C " +
            //                " where C.ID=A.CategoryUPD and C.RowStatus > -1) as CategoryUPD ,A.CategoryUPD Kategory, A.Dept Dept2, " +
            //                " (select DeptName from Dept as z where z.ID=A.Dept and z.RowStatus > -1) " +
            //                " as DeptName,A.CategoryUPD,A.[Type] from ISO_UpdDMD as A where LEFT(convert(char,tglberlaku,112),4)>='2018' "+
            //                " and RowStatus > -1 and aktif=1 and (A.StatusShare is null or A.StatusShare = 0 or A.StatusShare = 11 or A.StatusShare = 1)" +
            //                "union all " +
            //                "select ID,NoDocument,DocName,(select DocCategory from ISO_UpdDocCategory as  C  where C.ID=A.CategoryUPD and "+ 
            //                "C.RowStatus > -1) as CategoryUPD ,A.CategoryUPD Kategory, A.Dept Dept2,  (select DeptName from Dept as z "+
            //                "where z.ID=A.Dept and z.RowStatus > -1)  as DeptName,A.CategoryUPD,A.[Type] from ISO_UpdDMD as A where A.ID "+
            //                "not in (select idmaster from ISO_UPDrelasi where RowStatus > -1) and A.Aktif =1 and LEFT(convert(char,A.tglberlaku,112),6)<'201605' "+
            //                "and A.RowStatus > -1 order by A.Dept,A.CategoryUPD,A.DocName ";

            string strSQL =
            " select ROW_NUMBER() over (order by ID asc) as No,* from ( " +
            " select ID,NoDocument,DocName,(select DocCategory from ISO_UpdDocCategory as  C " +
            " where C.ID=A.CategoryUPD and C.RowStatus > -1) as CategoryUPD ,A.CategoryUPD Kategory, A.Dept Dept2, " +
            " (select DeptName from Dept as z where z.ID=A.Dept and z.RowStatus > -1) " +
            " as DeptName,A.CategoryUPD CategoryUPD_ID,A.[Type] from ISO_UpdDMD as A where LEFT(convert(char,tglberlaku,112),4)>='2018' " +
            " and RowStatus > -1 and aktif=1 and (A.StatusShare is null /**or A.StatusShare = 0**/ or A.StatusShare = 11 or A.StatusShare = 1) " +
            " and A.idUPD in (select ID from ISO_UPD where LastModifiedBy='head iso')) as x " +
           
            " union all " +

            " select ROW_NUMBER() over (order by ID asc) as No,* from (  select ID,NoDocument,DocName,(select DocCategory from ISO_UpdDocCategory as  C  " +
            " where C.ID=A.CategoryUPD and C.RowStatus > -1) as CategoryUPD ,A.CategoryUPD Kategory, A.Dept Dept2,  " +
            "(select DeptName from Dept as z where z.ID=A.Dept and z.RowStatus > -1)  as DeptName,A.CategoryUPD CategoryUPD_ID,A.[Type] " +
            " from ISO_UpdDMD as A where LEFT(convert(char,tglberlaku,112),4)>='2018' "+
            //" and RowStatus > -1 and aktif=1 and (A.StatusShare = 0 or A.StatusShare = 11)) as x ";
            " and RowStatus > -1 and aktif=1 and (A.StatusShare = 2) and A.PlantID<>'" + users.UnitKerjaID.ToString() + "' ) as x ";



            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObject3(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_UpdDMD());

            return arrUPD;
        }

        //public ArrayList RetrieveByUPDSearch(string Nomor)
        //{
        //    string strSQL = " select ID,NoDocument,DocName,(select DocCategory from ISO_UpdDocCategory as  C " +
        //                    " where C.ID=A.CategoryUPD and C.RowStatus > -1) as CategoryUPD ,A.CategoryUPD Kategory, A.Dept Dept2, " +
        //                    " (select DeptName from Dept as z where z.ID=A.Dept and z.RowStatus > -1) " +
        //                    " as DeptName,A.CategoryUPD,A.[Type] from ISO_UpdDMD as A where " +
        //                    " RowStatus > -1 and aktif=1 and A.NoDocument='"+Nomor+"'";

        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        //    strError = dataAccess.Error;
        //    arrUPD = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            arrUPD.Add(GenerateObject3(sqlDataReader));
        //        }
        //    }
        //    else
        //        arrUPD.Add(new ISO_UpdDMD());

        //    return arrUPD;
        //}

        public ArrayList RetrieveByUPDSearch(string Nomor, string cara)
        {
            string query = string.Empty;

            if (cara == "Nama")
            {
                query = "A.DocName='" + Nomor + "'";
            }
            else if (cara == "Nomor")
            {
                query = "A.NoDocument='" + Nomor + "'";
            }
            string strSQL =
            " select ROW_NUMBER() over (order by ID asc) as No,* from ( " +    
            " select ID,NoDocument,DocName,(select DocCategory from ISO_UpdDocCategory as  C " +
                            " where C.ID=A.CategoryUPD and C.RowStatus > -1) as CategoryUPD ,A.CategoryUPD Kategory, A.Dept Dept2, " +
                            " (select DeptName from Dept as z where z.ID=A.Dept and z.RowStatus > -1) " +
                            " as DeptName,A.CategoryUPD CategoryUPD_ID,A.[Type] from ISO_UpdDMD as A where " +
                //" RowStatus > -1 and aktif=1 and A.NoDocument='"+Nomor+"'";
                            " RowStatus > -1 and aktif=1 and " + query + " ) as x";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObject3(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_UpdDMD());

            return arrUPD;
        }

        public ArrayList RetrieveByUPDShare()
        {
            string strSQL =
            " select ROW_NUMBER() over (order by ID asc) as No,* from ( " +
            " select ID,NoDocument,DocName,(select DocCategory from ISO_UpdDocCategory as  C  where C.ID=A.CategoryUPD and C.RowStatus > -1) "+
            " as CategoryUPD ,A.CategoryUPD Kategory, A.Dept Dept2,  (select DeptName from Dept as z where z.ID=A.Dept and z.RowStatus > -1)  "+
            " as DeptName,A.CategoryUPD CategoryUPD_ID,A.[Type] from ISO_UpdDMD as A where  A.Aktif =1 and A.RowStatus > -1 and A.StatusShare=2 ) as x" +
            " order by Dept,CategoryUPD,DocName ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObject3(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_UpdDMD());

            return arrUPD;
        }

        public ArrayList RetrieveByUPD1(string aa, string bb, string Tanda)
        {
            string query = string.Empty;

            if (Tanda == "2")
            {
                query =
                    " select ROW_NUMBER() over (order by ID asc) as No,* from ( " +
                    " select ID,NoDocument,DocName,(select DocCategory from ISO_UpdDocCategory as  C " +
                                " where C.ID=A.CategoryUPD and C.RowStatus > -1) as CategoryUPD ,A.CategoryUPD Kategory, A.Dept Dept2, " +
                                " (select DeptName from Dept as z where z.ID=A.Dept and z.RowStatus > -1) " +
                                " as DeptName,A.CategoryUPD CategoryUPD_ID,A.[Type] from ISO_UpdDMD as A where " +
                                " LEFT(convert(char,tglberlaku,112),6)>='201605' and RowStatus > -1 and aktif=1 and A.Dept=" + aa + " and A.CategoryUPD=" + bb + " and (A.StatusShare is null or A.StatusShare = 0 or A.StatusShare = 11)" +
                                "union all " +
                                "select ID,NoDocument,DocName,(select DocCategory from ISO_UpdDocCategory as  C  where C.ID=A.CategoryUPD and " +
                                "C.RowStatus > -1) as CategoryUPD ,A.CategoryUPD Kategory, A.Dept Dept2,  (select DeptName from Dept as z " +
                                "where z.ID=A.Dept and z.RowStatus > -1)  as DeptName,A.CategoryUPD CategoryUPD_ID,A.[Type] from ISO_UpdDMD as A where A.ID " +
                                "not in (select idmaster from ISO_UPDrelasi where RowStatus > -1) and aktif=1 and A.Dept=" + aa + " and A.CategoryUPD=" + bb + " and A.Aktif =1 and LEFT(convert(char,A.tglberlaku,112),6)<'201605' " +
                                "and A.RowStatus > -1 ) as x order by ID";
            }
            else if (Tanda == "1")
            {
                query =
                    " select ROW_NUMBER() over (order by ID asc) as No,* from ( " +
                    " select ID,NoDocument,DocName,(select DocCategory from ISO_UpdDocCategory as  C  where C.ID=A.CategoryUPD and C.RowStatus > -1) " +
                   " as CategoryUPD ,A.CategoryUPD Kategory, A.Dept Dept2,  (select DeptName from Dept as z where z.ID=A.Dept and z.RowStatus > -1)  " +
                   " as DeptName,A.CategoryUPD CategoryUPD_ID,A.[Type] from ISO_UpdDMD as A where  A.Aktif =1 and A.RowStatus > -1 and A.StatusShare=1 " +
                   " and A.Dept=" + aa + " and A.CategoryUPD=" + bb + " ) as x order by Dept,CategoryUPD,DocName ";
            }
            //string strSQL = " select ID,NoDocument,DocName,(select DocCategory from ISO_UpdDocCategory as  C " +
            //                " where C.ID=A.CategoryUPD and C.RowStatus > -1) as CategoryUPD ,A.CategoryUPD Kategory, A.Dept Dept2, " +
            //                " (select DeptName from Dept as z where z.ID=A.Dept and z.RowStatus > -1) " +
            //                " as DeptName,A.CategoryUPD,A.[Type] from ISO_UpdDMD as A where "+
            //                " LEFT(convert(char,tglberlaku,112),6)>='201605' and RowStatus > -1 and aktif=1 and A.Dept=" + aa + " and A.CategoryUPD=" + bb + " and (A.StatusShare is null or A.StatusShare = 0)" +
            //                "union all " +
            //                "select ID,NoDocument,DocName,(select DocCategory from ISO_UpdDocCategory as  C  where C.ID=A.CategoryUPD and " +
            //                "C.RowStatus > -1) as CategoryUPD ,A.CategoryUPD Kategory, A.Dept Dept2,  (select DeptName from Dept as z " +
            //                "where z.ID=A.Dept and z.RowStatus > -1)  as DeptName,A.CategoryUPD,A.[Type] from ISO_UpdDMD as A where A.ID " +
            //                "not in (select idmaster from ISO_UPDrelasi where RowStatus > -1) and aktif=1 and A.Dept=" + aa + " and A.CategoryUPD=" + bb + " and A.Aktif =1 and LEFT(convert(char,A.tglberlaku,112),6)<'201605' " +
            //                "and A.RowStatus > -1 order by A.ID";
            string strSQL = query;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObject3(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_UpdDMD());

            return arrUPD;
        }

        public ArrayList RetrieveByDept()
        {
            string strSQL = " select xx.deptID DeptID,xx.namaDept DeptName from (select distinct(Dept)Dept from " +
                            "ISO_UpdDMD where ID not in (select idMaster from ISO_UPDrelasi where RowStatus > -1) " +
                            "and Aktif=1) as x inner join ISO_UPDDept as xx ON x.Dept=xx.deptID where xx.RowStatus > -1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectD(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_UpdDMD());

            return arrUPD;
        }

        public ArrayList RetrieveByJenis()
        {
            string strSQL = "select xx.ID CategoryID,xx.DocCategory CategoryUPD from (select distinct(CategoryUPD) CategoryUPD from ISO_UpdDMD " +
                            "where ID not in (select idMaster from ISO_UPDrelasi where RowStatus > -1) and Aktif=1) as x " +
                            "inner join ISO_UpdDocCategory as xx ON x.CategoryUPD=xx.ID where xx.RowStatus > -1";                           
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectJ(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_UpdDMD());

            return arrUPD;
        }

        public ArrayList RetrieveByUPDLama()
        {

            string strSQL = " select ID,NoDocument,DocName,(select DocCategory from ISO_UpdDocCategory as  C " +
                            " where C.ID=A.CategoryUPD and C.RowStatus > -1) as CategoryUPD ,A.CategoryUPD Kategory, A.Dept Dept2, " +
                            " (select DeptName from Dept as z where z.ID=A.Dept and z.RowStatus > -1) " +
                            " as DeptName,A.Type from ISO_UpdDMD as A where LEFT(convert(char,tglberlaku,112),6)<='201604' and RowStatus > -1 and aktif=1 " +
                            " and ID not in (select idmaster from ISO_UPDdistribusiFile where RowStatus > -1)";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObject3(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_UpdDMD());

            return arrUPD;
        }

        public ArrayList RetrieveDept()
        {
            //string strSQL = "select distinct(deptid) as ID,deptname from ISO_Dept where RowStatus > -1 and DeptID not in (27,25,12,24) ";

            //string strSQL = " select * , case when ID=2 then 'BM' when ID=3 then 'Finishing' when ID=4 then 'Maintenance' "+
            //                " when ID=6 then 'Logistik BJ' when ID=7 then 'HRD' when ID=9 then 'QA' when ID=10 then 'Logistik BB' "+
            //                " when ID=11 then 'PPIC' when ID=13 then 'Marketing' when ID=14 then 'IT' when ID=15 then 'Purchasing' "+
            //                " when ID=23 then 'ISO' when ID=26 then 'Transportation' when ID=22 then 'Project' "+
            //                " when ID=24 then 'Accounting' when ID=12 then 'Finance' when ID=38 then 'PM' when ID=39 then 'Product Development' "+
            //                " when ID=31 then 'Research and Development' end DeptName from (select distinct(deptid) as ID " +
            //                " from ISO_Dept where RowStatus > -1 and DeptID not in (27,25,5,8,16,18,19,21)) as Dept order by DeptName";

            string strSQL = "select ID,alias deptname from Dept where RowStatus > -1 and ID not in (27,25,18,1,4,5,8,17,21,16,31) ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObject4(sqlDataReader));
                }
            }
            //else
            //    arrUPD.Add(new ISO_UpdDMD());
            return arrUPD;
        }

        public ArrayList RetrieveDok()
        {
            string strSQL = " select A.ID,B.NoDocument,B.DocName,A.[FileName],(select DeptName from Dept as z where z.ID=A.DeptID and z.RowStatus > -1) " +
                            " as DeptName,(select DocCategory from ISO_UpdDocCategory as  C " +
                            " where C.ID=A.CategoryUPD and C.RowStatus > -1) as CategoryUPD, B.RevisiNo, (LEFT(convert(char,B.tglberlaku,111),10)) as TglBerlaku " +
                            " from ISO_UPDdistribusiFile as A,ISO_UpdDMD as B where A.idMaster=B.ID and A.RowStatus > -1 order by A.ID desc ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObject5(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_UpdDMD());

            return arrUPD;
        }

        public ArrayList RetrieveDok2(string Deptm1, string jenis, int DeptID, string deptid1, string Query, string QueryNo)
        {
            HttpContext context = HttpContext.Current; string CekUser = string.Empty; string CekUser2 = string.Empty;
            CekUser2 = context.Session["CekUser"].ToString();
            if (CekUser2 == "0")
            { CekUser = "0";}
            else
            { CekUser = CekUser2; }


            string userDeptID = string.Empty;
            string Query1 = string.Empty;
            string Query2 = string.Empty;
            string Deptm = string.Empty; string deptid = string.Empty;

            if (deptid1 != "0")
            {
                if (deptid1 == "30" || deptid1 == "31")
                {
                    Deptm = "(30,31)"; deptid = "(30,31)";
                }
                else if (deptid1 == "19" || deptid1 == "4")
                {
                    Deptm = "(4,19)"; deptid = "(4,19)";
                }
                else
                {
                    Deptm = "(" + Deptm1 + ")"; deptid = "(" + deptid1 + ")";
                }
            }
            else
            {
                if (deptid1 == "30" || deptid1 == "31")
                {
                    Deptm = "(30,31)"; deptid = "(30,31)";
                }
                else if (deptid1 == "19" || deptid1 == "19")
                {
                    Deptm = "(4,19)"; deptid = "(4,19)";
                }
                else if (deptid1 == "0" && Deptm1 == "30" || deptid1 == "0" && Deptm1 == "31")
                {
                    Deptm = "(30,31)"; deptid = "";
                }
                else if (deptid1 == "0" && Deptm1 == "19" || deptid1 == "0" && Deptm1 == "18" || deptid1 == "0" && Deptm1 == "4" || deptid1 == "0" && Deptm1 == "5")
                {
                    Deptm = "(4,19,5,18)"; deptid = "";
                }
                else
                {
                    Deptm = "(" + Deptm1 + ")"; deptid = "";
                }
            }

            if (jenis == "0" && CekUser == "0")
            {
                switch (DeptID)
                {
                    case 19:
                        userDeptID = "in " + Deptm + "";
                        //Query1 = " and A.CategoryUPD=" + jenis + " ";
                        Query1 = "";
                        Query2 = "";
                        break;
                    case 23:
                        userDeptID = "in " + Deptm + "";
                        Query1 = ""; Query2 = "";
                        break;
                    case 7:
                        userDeptID = "in " + Deptm + "";
                        Query1 = ""; Query2 = "";
                        break;
                    default:
                        userDeptID = "in " + Deptm + "";
                        //Query1 = " and A.CategoryUPD=" + jenis + " ";
                        Query1 = "";
                        Query2 = "";
                        break;
                }
            }
            else if (jenis == "0" && CekUser != "0")
            {
                userDeptID = "in " + Deptm + "";
                Query1 = ""; Query2 = "";
            }
            else if (jenis != "0" && deptid == "" && CekUser == "0")
            {
                switch (DeptID)
                {
                    case 19:
                        userDeptID = "in " + Deptm + "";
                        Query1 = " and A.CategoryUPD=" + jenis + " ";
                        Query2 = " ";
                        break;
                    case 23:
                        userDeptID = "in " + Deptm + "";
                        Query1 = " and A.CategoryUPD=" + jenis + " ";
                        Query2 = " ";
                        break;
                    case 7:
                        userDeptID = "in " + Deptm + "";
                        Query1 = " and A.CategoryUPD=" + jenis + " ";
                        Query2 = "  ";
                        break;
                    case 30:
                        userDeptID = "in " + Deptm + "";
                        Query1 = " and A.CategoryUPD=" + jenis + " ";
                        Query2 = "  ";
                        break;
                    case 31:
                        userDeptID = "in " + Deptm + "";
                        Query1 = " and A.CategoryUPD=" + jenis + " ";
                        Query2 = "  ";
                        break;
                    default:
                        userDeptID = "in " + Deptm + "";
                        Query1 = " and A.CategoryUPD=" + jenis + " ";
                        Query2 = " ";
                        break;
                }

            }
            else if (jenis != "0" && deptid == "" && CekUser != "0")
            {
                userDeptID = "in " + Deptm + "";
                Query1 = " and A.CategoryUPD=" + jenis + " ";
                Query2 = " ";
            }
            else if (jenis != "0" && deptid != "" && CekUser == "0")
            {
                switch (DeptID)
                {
                    case 19:
                        userDeptID = "in " + Deptm + "";
                        Query1 = " and A.CategoryUPD=" + jenis + " ";
                        Query2 = " and xx.DeptID in " + deptid + " ";
                        break;
                    case 23:
                        userDeptID = "in " + Deptm + "";
                        Query1 = " and A.CategoryUPD=" + jenis + " ";
                        Query2 = " and xx.DeptID in " + deptid + " ";
                        break;
                    case 7:
                        userDeptID = "in " + Deptm + "";
                        Query1 = " and A.CategoryUPD=" + jenis + " ";
                        Query2 = " and xx.DeptID in " + deptid + " ";
                        break;
                    default:
                        userDeptID = "in " + Deptm + "";
                        Query1 = " and A.CategoryUPD=" + jenis + " ";
                        Query2 = " and xx.DeptID in " + deptid + " ";
                        break;
                }

            }
            else if (jenis != "0" && deptid != "" && CekUser != "0")
            {
                userDeptID = "in " + Deptm + "";
                Query1 = " and A.CategoryUPD=" + jenis + " ";
                Query2 = " and xx.DeptID in " + deptid + " ";
            }
            string sdept = string.Empty;
         
            string strSQL = 
            //" select "+QueryNo+" ,xx.NoDocument,xx.DocName,xx.RevisiNo, " +
            " " + QueryNo + " select TRIM(xx.NoDocument) AS NoDocument,xx.DocName,xx.RevisiNo, " +
            " isnull(xx.TglBerlaku,0) as TglBerlaku,isnull(C.CreatedTime,0) as TglDistribusi " +
            " ,case when C.FileName is null then '-' " +
            " else C.[FileName] end [FileName],D.namaDept as DeptName,(select DocCategory from ISO_UpdDocCategory as E " +
            " where E.ID=xx.CategoryUPD  and E.RowStatus > -1) as CategoryUPD,isnull(C.ID,0) as ID,xx.DeptID,IDm from  " +
            " (select B.NoDocument,B.DocName,B.RevisiNo,B.TglBerlaku,B.ID as IDm, A.DeptF,A.CategoryUPD,A.Urutan,A.rowstatus,B.Dept DeptID  " + 
            " from ISO_UPDrelasi as A INNER JOIN ISO_UpdDMD as B ON A.idMaster=B.ID  where DeptF " + userDeptID + "  " + Query1 +  " and B.Aktif>0) as xx " +
            " LEFT JOIN ISO_UPDdistribusiFile as C ON xx.IDm=C.idMaster  LEFT JOIN ISO_UPDDept as D ON xx.DeptF=D.deptIDalias " +
            " where xx.rowstatus > -1 and D.RowStatus > -1 and C.RowStatus>-1 " + Query2 + " " + Query + "  ";
           

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObject7(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_UpdDMD());

            return arrUPD;
        }

        public ArrayList RetrieveDok22(string Nomor)
        {
            string userDeptID = string.Empty;
            string Query1 = string.Empty;  
               
            string strSQL = " select ROW_NUMBER() over (order by xx.urutan,xx.nodocument asc) as No ,xx.NoDocument,xx.DocName,xx.RevisiNo, " +
                              " isnull(xx.TglBerlaku,0) as TglBerlaku,isnull(C.CreatedTime,0) as TglDistribusi " +
                              " ,  case when C.FileName is null then '-' " +
                              " else C.[FileName] end [FileName],D.namaDept as DeptName,(select DocCategory from ISO_UpdDocCategory as E " +
                              " where E.ID=xx.CategoryUPD  and E.RowStatus > -1) as CategoryUPD,isnull(C.ID,0) as ID from  " +
                              " (select B.NoDocument,B.DocName,B.RevisiNo,B.TglBerlaku,B.ID as IDm, A.DeptF,A.CategoryUPD,A.Urutan,A.rowstatus " +
                              " from ISO_UPDrelasi as A INNER JOIN ISO_UpdDMD as B ON A.idMaster=B.ID  where B.NoDocument='"+Nomor+"' and B.Aktif>0) as xx " +
                              " LEFT JOIN ISO_UPDdistribusiFile as C ON xx.IDm=C.idMaster  LEFT JOIN ISO_UPDDept as D ON xx.DeptF=D.deptIDalias " +
                              " where xx.rowstatus > -1 and D.RowStatus > -1 and C.RowStatus>-1 order by xx.Urutan,xx.NoDocument ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObject7(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_UpdDMD());

            return arrUPD;
        }

        public ArrayList RetrieveFile(int ID)
        {            
            string strSQL = " select B.NamaFile,D.[FileName]FileLama from ISO_UPD as A "+ 
                            " LEFT JOIN ISO_UPDLampiran as B ON A.ID=B.IDupd "+
                            " LEFT JOIN ISO_UpdDMD as C ON A.IDmaster=C.ID "+
                            " LEFT JOIN ISO_UPDdistribusiFile as D ON C.ID=D.idMaster "+
                            " where A.ID in ("+ID+") and (B.RowStatus > -1 or D.RowStatus>-1 )";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectFile(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_UpdDMD());

            return arrUPD;
        }

        public ArrayList RetrievePDF(string ba)
        {
            string strSQL = " select * from ISO_UPDdistribusiFile where rowstatus > -1 and ID=" + ba ;   
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObject6(sqlDataReader));
                }
            }
            else
                arrUPD.Add(new ISO_UpdDMD());

            return arrUPD;
        }

        public ISO_Upd RetrieveMasterDokumen(string NoDocument, int ID)
        {
            string strSQL = "select NoDocument NoDokumen,aktif from ISO_UpdDMD where RowStatus > -1 and CategoryUPD=" + ID + " and NoDocument='" + NoDocument + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectNO(sqlDataReader);
                }
            }
            return new ISO_Upd();
        }

        public ISO_Upd RetrieveMasterDokumenKhusus(string NamaDokumen, int ID)
        {
            string strSQL = " select aktif,DocName NamaDokumen from ISO_UpdDMD where RowStatus > -1 and " +
                            " (DocName='" + NamaDokumen + "' and aktif>0)";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectNOK(sqlDataReader);
                }
            }
            return new ISO_Upd();
        }


        public ISO_Upd RetrieveTipe(int ID)
        {
            string strSQL = "select Type from ISO_UpdDocCategory where RowStatus > -1 and ID=" + ID + "";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectTipe(sqlDataReader);
                }
            }
            return new ISO_Upd();
        }
        public ISO_Upd GenerateObject(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objUPD2.NoDocument = sqlDataReader["NoDocument"].ToString();
            return objUPD;
        }

        public ISO_Upd GenerateObjectFile(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.NamaFile = sqlDataReader["NamaFile"].ToString();
            objUPD2.FileName = sqlDataReader["FileName"].ToString();
            return objUPD;
        }

        public ISO_Upd GenerateObjectHeader(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objUPD2.NoDocument = sqlDataReader["NoDocument"].ToString();            
            return objUPD;
        }

        public ISO_Upd GenerateObject1(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();            
            return objUPD;
        }

        public ISO_Upd GenerateObject2(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.Lampiran = Convert.ToString(sqlDataReader["Lampiran"]);
            return objUPD;
        }

        public ISO_UpdDMD GenerateObject3(SqlDataReader sqlDataReader)
        {
            objUPD2 = new ISO_UpdDMD();
            objUPD2.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objUPD2.NoDocument = sqlDataReader["NoDocument"].ToString();
            objUPD2.DocName = sqlDataReader["DocName"].ToString();
            objUPD2.DeptName = sqlDataReader["DeptName"].ToString();
            objUPD2.CategoryUPD = sqlDataReader["CategoryUPD"].ToString();
            objUPD2.Kategory = int.Parse(sqlDataReader["Kategory"].ToString());
            objUPD2.Dept2 = int.Parse(sqlDataReader["Dept2"].ToString());
            objUPD2.Type = sqlDataReader["Type"].ToString();
            //objUPD2.CategoryUPD_ID = sqlDataReader["CategoryUPD_ID"].ToString();
            objUPD2.No = int.Parse(sqlDataReader["No"].ToString());
            //objUPD2.Waktu = Convert.ToDateTime(sqlDataReader["Waktu"]);
            return objUPD2;
        }

        public ISO_UpdDMD GenerateObjectD(SqlDataReader sqlDataReader)
        {
            objUPD2 = new ISO_UpdDMD();
            objUPD2.DeptID = sqlDataReader["DeptID"].ToString();          
            objUPD2.DeptName = sqlDataReader["DeptName"].ToString();           
            return objUPD2;
        }

        public ISO_UpdDMD GenerateObjectJ(SqlDataReader sqlDataReader)
        {
            objUPD2 = new ISO_UpdDMD();
            objUPD2.CategoryUPD = sqlDataReader["CategoryUPD"].ToString();
            objUPD2.CategoryID = Convert.ToInt32(sqlDataReader["CategoryID"]);
            return objUPD2;
        }

        public ISO_UpdDMD GenerateObject4(SqlDataReader sqlDataReader)
        {
            objUPD2 = new ISO_UpdDMD();
            objUPD2.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objUPD2.DeptName = sqlDataReader["DeptName"].ToString();            
            return objUPD2;
        }

        public ISO_UpdDMD GenerateObject5(SqlDataReader sqlDataReader)
        {
            objUPD2 = new ISO_UpdDMD();
            objUPD2.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objUPD2.FileName = sqlDataReader["FileName"].ToString();
            objUPD2.NoDocument = sqlDataReader["NoDocument"].ToString();
            objUPD2.DocName = sqlDataReader["DocName"].ToString();
            objUPD2.RevisiNo = sqlDataReader["RevisiNo"].ToString();
            objUPD2.TglBerlaku = Convert.ToDateTime(sqlDataReader["TglBerlaku"]);
            objUPD2.DeptName = sqlDataReader["DeptName"].ToString();
            objUPD2.CategoryUPD = sqlDataReader["CategoryUPD"].ToString();
            return objUPD2;
        }

        public ISO_UpdDMD GenerateObject6(SqlDataReader sqlDataReader)
        {
            objUPD2 = new ISO_UpdDMD();
            objUPD2.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objUPD2.FileName = sqlDataReader["FileName"].ToString();
            //objUPD2.attachfile = (byte[])sqlDataReader["AttachFile"];
            return objUPD2;
        }

        public ISO_UpdDMD GenerateObject7(SqlDataReader sqlDataReader)
        {
            objUPD2 = new ISO_UpdDMD();
            objUPD2.IDm = Convert.ToInt32(sqlDataReader["IDm"]);
            objUPD2.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objUPD2.No = Convert.ToInt32(sqlDataReader["No"]);
            objUPD2.FileName = sqlDataReader["FileName"].ToString();
            objUPD2.NoDocument = sqlDataReader["NoDocument"].ToString();
            objUPD2.DocName = sqlDataReader["DocName"].ToString();
            objUPD2.RevisiNo = sqlDataReader["RevisiNo"].ToString();
            objUPD2.TglBerlaku = Convert.ToDateTime(sqlDataReader["TglBerlaku"]);
            objUPD2.TglDistribusi = Convert.ToDateTime(sqlDataReader["TglDistribusi"]);
            objUPD2.DeptName = sqlDataReader["DeptName"].ToString();
            objUPD2.CategoryUPD = sqlDataReader["CategoryUPD"].ToString();
            return objUPD2;
        }

        public ISO_Upd GenerateObjectNO(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.Aktif = sqlDataReader["Aktif"].ToString();
            objUPD.NoDokumen = sqlDataReader["NoDokumen"].ToString();
            //objUPD.NamaDokumen = sqlDataReader["NamaDokumen"].ToString();
            return objUPD;
        }

        public ISO_Upd GenerateObjectNOK(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.Aktif = sqlDataReader["Aktif"].ToString();
            //objUPD.NoDokumen = sqlDataReader["NoDokumen"].ToString();
            objUPD.NamaDokumen = sqlDataReader["NamaDokumen"].ToString();
            return objUPD;
        }

        public ISO_Upd GenerateObjectTipe(SqlDataReader sqlDataReader)
        {
            objUPD = new ISO_Upd();
            objUPD.Type = Convert.ToInt32(sqlDataReader["Type"]);
            return objUPD;
        }

        public ISO_UpdDMD RetrieveData(int IDmaster)
        {
            string strSQL = " select A.RevisiNo,A.TglBerlaku TglBerlakuSS,left(convert(char,A.TglBerlaku,113),11)TglBerlakuS,B.[FileName]NamaFile, A.DocName,A.NoDocument from " +
                            " ISO_UpdDMD as A LEFT JOIN ISO_UPDdistribusiFile as B ON A.ID=B.idMaster where B.ID="+IDmaster+" ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject_RetrieveData(sqlDataReader);
                }
            }
            return new ISO_UpdDMD();
        }

        public ISO_UpdDMD GenerateObject_RetrieveData(SqlDataReader sqlDataReader)
        {
            objUPD2 = new ISO_UpdDMD();

            objUPD2.RevisiNo = sqlDataReader["RevisiNo"].ToString();
            objUPD2.TglBerlakuS = sqlDataReader["TglBerlakuS"].ToString();
            objUPD2.TglBerlakuSS = sqlDataReader["TglBerlakuSS"].ToString();
            objUPD2.NamaFile = sqlDataReader["NamaFile"].ToString();
            objUPD2.DocName = sqlDataReader["DocName"].ToString();
            objUPD2.NoDocument = sqlDataReader["NoDocument"].ToString();

            return objUPD2;
        }

        public ISO_UpdDMD AmbilData(int ID, string Query1)
        {
            string strSQL =
            " select NoDocument,DocName,RevisiNo,CreatedBy,CategoryUPD,DeptID,Type,PlantID,StatusShare,ISNULL(JenisUPD,0)JenisUPD,ISNULL(alasan,'')Alasan,Aktif,ISNULL(idUPD,0)ID,ISNULL(TglShare,'')TglShare from " +
            "(select NoDocument,DocName,RevisiNo,CreatedBy,CategoryUPD,Dept DeptID,Type,ISNULL(PlantID,0)PlantID,ISNULL(StatusShare,0)StatusShare, " +
            "" + Query1 + "JenisUPD,(select Alasan from ISO_UpdDetail where UPDid in (select ID from ISO_UPD A where A.ID=idUPD and RowStatus>-1))Alasan,Aktif,idUPD,TglBerlaku TglShare from ISO_UpdDMD where RowStatus > -1 and ID=" + ID + ") as Data1";
            //"" + Query1 + "Aktif from ISO_UpdDMD where RowStatus > -1 and ID=" + ID + ") as Data1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectAmbilData(sqlDataReader);
                }
            }
            return new ISO_UpdDMD();
        }

        public ISO_UpdDMD AmbilDataNonAktif(int ID, string Query1)
        {
            string strSQL =
            " select Data1.ID,NoDocument,DocName,RevisiNo,Data1.CreatedBy,Data1.CategoryUPD,Data1.DeptID,Type,PlantID,StatusShare,TglShare, " +
            " "+Query1+" "+
            " Aktif,ISNULL(iso.[FileName],'')FileName from " +
            " (select ID,NoDocument,DocName,RevisiNo,CreatedBy,CategoryUPD,Dept DeptID,Type,ISNULL(PlantID,0)PlantID,ISNULL(StatusShare,0)StatusShare, " +
            " Aktif,TglNonAktif TglShare from ISO_UpdDMD where RowStatus > -1 and ID=" + ID + ") as Data1 " +
            " INNER JOIN ISO_UPDdistribusiFile iso ON iso.idMaster=Data1.ID where iso.RowStatus>-1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObjectAmbilData(sqlDataReader);
                }
            }
            return new ISO_UpdDMD();
        }

        public ISO_UpdDMD GenerateObjectAmbilData(SqlDataReader sqlDataReader)
        {
            objUPD2 = new ISO_UpdDMD();
            objUPD2.NoDocument = sqlDataReader["NoDocument"].ToString();
            objUPD2.DocName = sqlDataReader["DocName"].ToString();
            objUPD2.RevisiNo = sqlDataReader["RevisiNo"].ToString();
            objUPD2.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objUPD2.CategoryUPD = sqlDataReader["CategoryUPD"].ToString();
            objUPD2.DeptID = sqlDataReader["DeptID"].ToString();
            objUPD2.Type = sqlDataReader["Type"].ToString();
            objUPD2.PlantID = Convert.ToInt32(sqlDataReader["PlantID"]);
            objUPD2.StatusShare = Convert.ToInt32(sqlDataReader["StatusShare"]);
            objUPD2.JenisUPD = Convert.ToInt32(sqlDataReader["JenisUPD"]);
            objUPD2.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objUPD2.Alasan = sqlDataReader["Alasan"].ToString();
            objUPD2.Aktif = sqlDataReader["Aktif"].ToString();
            objUPD2.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objUPD2.TglShare = Convert.ToDateTime(sqlDataReader["TglShare"]);
            //objUPD2.FileName = sqlDataReader["FileName"].ToString();
            return objUPD2;
        }

        public int CekType(int ID)
        {
            string StrSql = "select Type from ISO_UpdDocCategory where RowStatus>-1 and ID="+ID+"";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["Type"]);
                }
            }

            return 0;
        }

        public int CekDataShare(int ID)
        {
            string StrSql = " select SUM(ID)ID from (select ID from ISO_UPDTemp where ID="+ID+" and RowStatus>-1 "+
                            " union all "+
                            " select '0'ID from ISO_UPDTemp) as Data1 " ;
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

        public ISO_UpdDMD CekData(int IDmaster)
        {
            string strSQL = " select ISNULL(StatusShare,0)StatusShare,ISNULL(PlantID,0)PlantID,ISNULL(Aktif,0)Aktif,ISNULL(TglBerlaku,'')TglBerlaku from (select StatusShare,PlantID,Aktif,TglBerlaku from ISO_UpdDMD " +
                "where ID="+IDmaster+" and rowstatus>-1 ) as Data1 ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject_CekData(sqlDataReader);
                }
            }
            return new ISO_UpdDMD();
        }

        public ISO_UpdDMD GenerateObject_CekData(SqlDataReader sqlDataReader)
        {
            objUPD2 = new ISO_UpdDMD();

            objUPD2.StatusShare = Convert.ToInt32(sqlDataReader["StatusShare"]);
            objUPD2.PlantID = Convert.ToInt32(sqlDataReader["PlantID"]);
            objUPD2.Aktif = sqlDataReader["Aktif"].ToString();
            objUPD2.TglBerlaku =Convert.ToDateTime(sqlDataReader["TglBerlaku"].ToString());
            
            return objUPD2;
        }

        public int RetrieveSShare(int ID)
        {
            string StrSql = " select ISNULL(StatusShare,0)StatusShare from (select StatusShare from ISO_UPDDMD where RowStatus>-1 and ID=" + ID + " and aktif>0 ) as Data1  ";
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

        public ISO_UpdDMD RetrieveDataEdit(int IDmaster)
        {
            string strSQL = " select ID,DocName,NoDocument from iso_upddmd where id=" + IDmaster + " and RowStatus>-1 ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject_CekDataMaster(sqlDataReader);
                }
            }
            return new ISO_UpdDMD();
        }

        public ISO_UpdDMD GenerateObject_CekDataMaster(SqlDataReader sqlDataReader)
        {
            objUPD2 = new ISO_UpdDMD();

            objUPD2.ID = Convert.ToInt32(sqlDataReader["ID"]);
            //objUPD2.PlantID = Convert.ToInt32(sqlDataReader["PlantID"]);
            objUPD2.DocName = sqlDataReader["DocName"].ToString();
            objUPD2.NoDocument = sqlDataReader["NoDocument"].ToString();

            return objUPD2;
        }

        public int RetrieveUser(int ID)
        {
            string StrSql = " select ID from ISO_UpdListReport where UserID="+ID+" and RowStatus>-1  ";
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

        public ArrayList RetrieveData(string IDm)
        {
            string StrSql =
            " select IDmaster,DeptF Deptm from iso_updrelasi where idmaster=" + IDm + " and rowstatus>-1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectData(sqlDataReader));
                }
            }          
            return arrUPD;
        }

        public ISO_UpdDMD GenerateObjectData(SqlDataReader sqlDataReader)
        {
            objUPD2 = new ISO_UpdDMD();

            objUPD2.IDmaster = Convert.ToInt32(sqlDataReader["IDmaster"]);
            objUPD2.Deptm = sqlDataReader["Deptm"].ToString();
            //objUPD2.NoDocument = sqlDataReader["NoDocument"].ToString();

            return objUPD2;
        }

        public ISO_UpdDMD RetrieveDataDistribusiLama(string IDmaster)
        {
            string strSQL =
            " select IDmaster,CategoryUPD,DeptID,Urutan  from iso_updrelasi where idmaster=" + IDmaster + " and rowstatus>-1 ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject_Distribusi(sqlDataReader);
                }
            }
            return new ISO_UpdDMD();
        }

        public ISO_UpdDMD GenerateObject_Distribusi(SqlDataReader sqlDataReader)
        {
            objUPD2 = new ISO_UpdDMD();

            objUPD2.IDmaster = Convert.ToInt32(sqlDataReader["IDmaster"]);
            objUPD2.CategoryUPD = sqlDataReader["CategoryUPD"].ToString();
            objUPD2.DeptID = sqlDataReader["DeptID"].ToString();
            objUPD2.Urutan = sqlDataReader["Urutan"].ToString();
            //objUPD2.NoDocument = sqlDataReader["NoDocument"].ToString();

            return objUPD2;
        }

        public ArrayList RetrieveDataAwal(string IDm)
        {
            string StrSql =
            " select Dept DeptID from ISO_UpdDMD where ID="+IDm+" and rowstatus>-1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectDataAwal(sqlDataReader));
                }
            }
            return arrUPD;
        }

        public ArrayList RetrieveDataAwal0(string IDm)
        {
            string StrSql =
            " select Dept DeptID from ISO_UpdDMD where ID=" + IDm + " and rowstatus>-1 union all select 23 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;
            arrUPD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrUPD.Add(GenerateObjectDataAwal(sqlDataReader));
                }
            }
            return arrUPD;
        }

        public ISO_UpdDMD GenerateObjectDataAwal(SqlDataReader sqlDataReader)
        {
            objUPD2 = new ISO_UpdDMD();

            objUPD2.DeptID = sqlDataReader["DeptID"].ToString();
            //objUPD2.Deptm = sqlDataReader["Deptm"].ToString();
            //objUPD2.NoDocument = sqlDataReader["NoDocument"].ToString();

            return objUPD2;
        }
    }
}


    


   

