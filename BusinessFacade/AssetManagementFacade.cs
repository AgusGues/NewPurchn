using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;
using System.Runtime.InteropServices;
using System.Reflection;

namespace BusinessFacade
{
    public class AssetManagementFacade : AbstractFacade2
    {
        private AssetManagement objObject = new AssetManagement();
        private Disposal objDisposal = new Disposal();
        private ArrayList arrObject;
        private List<SqlParameter> sqlListParam;
        private string strSQL = string.Empty;
        private string strFld = string.Empty;

        public AssetManagementFacade()
            : base()
        {

        }
        public AssetManagementFacade(object objDomain)
        {
            objObject = (AssetManagement)objDomain;
        }

        /// <summary>
        /// Insert Data Disposal ke table AM_Asset_Disposal
        /// with procedure spAM_Disposal_insert
        /// </summary>
        /// <param name="ObjDomain"></param>
        /// <returns></returns>
        public int Insert(object ObjDomain)
        {
            try
            {
                int result = 0;
                objDisposal=(Disposal)ObjDomain;
                sqlListParam=new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@BANumber",objDisposal.BANumber));
                sqlListParam.Add(new SqlParameter("@AdjustNo",objDisposal.AdjustNo));
                sqlListParam.Add(new SqlParameter("@AdjustID",objDisposal.AdjustID));
                sqlListParam.Add(new SqlParameter("@Alasan",objDisposal.AlasanDispoal));
                sqlListParam.Add(new SqlParameter("@DeptID",objDisposal.DeptID));
                sqlListParam.Add(new SqlParameter("@AssetID",objDisposal.AssetID));
                sqlListParam.Add(new SqlParameter("@TanggalBA",objDisposal.TanggalBA));
                sqlListParam.Add(new SqlParameter("@CreatedBy",objDisposal.CreatedBy));
                result = dataAccess.ProcessData(sqlListParam, "spAM_AssetDisposal_Insert");
                return result;
            }
            catch
            {
                return -1;
            }
        }
        /// <summary>
        /// Update Table Asset proses disposal
        /// </summary>
        /// <param name="objDomain"></param>
        /// <returns></returns>
        public int Update(object objDomain)
        {
            try
            {
                int result = 0;
                objDisposal = (Disposal)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objDisposal.ID));
                sqlListParam.Add(new SqlParameter("@RowStatus", objDisposal.RowStatus));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objDisposal.LastModifiedBy));
                result = dataAccess.ProcessData(sqlListParam, "spAM_AssetDisposal_Update");
                return result;
            }
            catch
            {
                return -1;
            }
        }
        /// <summary>
        /// Update table Adjust Kolom keterangan dengan ID disposal
        /// </summary>
        /// <param name="objDomain"></param>
        /// <returns></returns>
        public int UpdateAdjust(object objDomain)
        {
            try
            {
                int result = 0;
                objDisposal = (Disposal)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objDisposal.ID));
                sqlListParam.Add(new SqlParameter("@Keterangan", objDisposal.AssetName));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objDisposal.LastModifiedBy));
                result = dataAccess.ProcessData(sqlListParam, "spAdjust_Disposal_Update");
                return result;
            }
            catch
            {
                return -1;
            }
        }
        /// <summary>
        /// Insert Data Asset ke table AM_ [option]
        /// seusia dengan TableName Parameter
        /// </summary>
        /// <param name="objDomain"></param>
        /// <param name="TabelName"></param>
        /// <returns></returns>
        public override int Insert(object objDomain,string TabelName)
        {
            try
            {
                int intResult = 0;
                objObject       = (AssetManagement)objDomain;
                sqlListParam    = new List<SqlParameter>();
                switch (TabelName)
                {
                    case "spInsertAM_Group":
                        sqlListParam.Add(new SqlParameter("@KodeGroup", objObject.KodeGroup));
                        sqlListParam.Add(new SqlParameter("@NamaGroup", objObject.NamaGroup));
                        sqlListParam.Add(new SqlParameter("@RowStatus", objObject.RowStatus));
                        sqlListParam.Add(new SqlParameter("@UmurPakai", objObject.UmurAsset));
                        break;
                    case "spInsertAM_Class":
                        sqlListParam.Add(new SqlParameter("@GroupID", objObject.GroupID));
                        sqlListParam.Add(new SqlParameter("@KodeClass", objObject.KodeGroup));
                        sqlListParam.Add(new SqlParameter("@NamaClass", objObject.NamaGroup));
                        sqlListParam.Add(new SqlParameter("@RowStatus", objObject.RowStatus));
                        break;
                    case "spInsertAM_SubClass":
                        sqlListParam.Add(new SqlParameter("@ClassID", objObject.GroupID));
                        sqlListParam.Add(new SqlParameter("@KodeClass", objObject.KodeGroup));
                        sqlListParam.Add(new SqlParameter("@NamaClass", objObject.NamaGroup));
                        sqlListParam.Add(new SqlParameter("@RowStatus", objObject.RowStatus));
                        break;
                    case "spInsertAM_Lokasi":
                        sqlListParam.Add(new SqlParameter("@AliasName", objObject.NamaClass));
                        sqlListParam.Add(new SqlParameter("@KodeGroup", objObject.KodeGroup));
                        sqlListParam.Add(new SqlParameter("@NamaGroup", objObject.NamaGroup));
                        sqlListParam.Add(new SqlParameter("@RowStatus", objObject.RowStatus));
                        break;
                    case "spInsertAM_Asset":
                        sqlListParam.Add(new SqlParameter("@GroupID", objObject.GroupID));//1
                        sqlListParam.Add(new SqlParameter("@ClassID", objObject.ClassID));//2
                        sqlListParam.Add(new SqlParameter("@SubClassID", objObject.SubClassID));//3
                        sqlListParam.Add(new SqlParameter("@LokasiID", objObject.LokasiID));//4
                        sqlListParam.Add(new SqlParameter("@KodeAsset", objObject.KodeAsset));//6
                        sqlListParam.Add(new SqlParameter("@NamaAsset", objObject.Deskripsi));//7
                        sqlListParam.Add(new SqlParameter("@NilaiAsset", objObject.NilaiAsset));//8
                        sqlListParam.Add(new SqlParameter("@AssyDate", objObject.TglAsset));//98
                        sqlListParam.Add(new SqlParameter("@MfgDate", objObject.TglAsset));//108
                        sqlListParam.Add(new SqlParameter("@MfgYear", objObject.TglAsset.Year));//118
                        sqlListParam.Add(new SqlParameter("@LifeTime", objObject.UmurAsset));//129
                        sqlListParam.Add(new SqlParameter("@DepreciatID", objObject.MethodDep));//1310
                        sqlListParam.Add(new SqlParameter("@StartDeprec", objObject.TglSusut));//1411
                        sqlListParam.Add(new SqlParameter("@ItemKode", objObject.ItemKode));//151
                        sqlListParam.Add(new SqlParameter("@PicDept", objObject.PicDept));
                        sqlListParam.Add(new SqlParameter("@PicPerson", objObject.PicPerson));
                        sqlListParam.Add(new SqlParameter("@PlantID", objObject.PlantID));
                        sqlListParam.Add(new SqlParameter("@RowStatus", objObject.RowStatus));

                        //4mei
                        sqlListParam.Add(new SqlParameter("@TipeAsset", objObject.TipeAsset));
                        sqlListParam.Add(new SqlParameter("@UomID", objObject.UomID));
                        sqlListParam.Add(new SqlParameter("@OwnerDeptID", objObject.OwnerDeptID));
                        //4mei
                        sqlListParam.Add(new SqlParameter("@AssetID", objObject.AssetID));
                        sqlListParam.Add(new SqlParameter("@CreatedBy", objObject.CreatedBy));
                        
                        break;
                    case "spInsertAM_Asset_Mutasi":
                        sqlListParam.Add(new SqlParameter("@KodeAsset",objObject.KodeAsset));
                        sqlListParam.Add(new SqlParameter("@TglMutasi", objObject.TglMutasi));
                        sqlListParam.Add(new SqlParameter("@MutasiAsal", objObject.MutasiAsal));
                        sqlListParam.Add(new SqlParameter("@MutasiTujuan", objObject.MutasiTujuan));
                        sqlListParam.Add(new SqlParameter("@MutasiReson", objObject.MutasiReson));
                        sqlListParam.Add(new SqlParameter("@EfektifDate", objObject.TglEfektif));
                        sqlListParam.Add(new SqlParameter("@RowStatus", objObject.RowStatus));
                    break;
                }
                intResult = dataAccess.ProcessData(sqlListParam, TabelName);
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// Update Data Asset
        /// </summary>
        /// <param name="objDomain"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public override int Update(object objDomain,string TableName)
        {
            try
            {
                objObject = (AssetManagement)objDomain;
                sqlListParam = new List<SqlParameter>();
                switch (TableName)
                {
                    case "spUpdateAM_Group":
                        sqlListParam.Add(new SqlParameter("@ID", objObject.ID));
                        sqlListParam.Add(new SqlParameter("@KodeGroup", objObject.KodeGroup));
                        sqlListParam.Add(new SqlParameter("@NamaGroup", objObject.NamaGroup));
                        sqlListParam.Add(new SqlParameter("@RowStatus", objObject.RowStatus));
                        sqlListParam.Add(new SqlParameter("@UmurPakai", objObject.UmurAsset));
                        break;
                    case "spUpdateAM_Class":
                        sqlListParam.Add(new SqlParameter("@ID", objObject.ID));
                        sqlListParam.Add(new SqlParameter("@KodeClass", objObject.KodeGroup));
                        sqlListParam.Add(new SqlParameter("@NamaClass", objObject.NamaGroup));
                        sqlListParam.Add(new SqlParameter("@RowStatus", objObject.RowStatus));
                        sqlListParam.Add(new SqlParameter("@GroupID", objObject.GroupID));
                        break;
                    case "spUpdateAM_SubClass":
                        sqlListParam.Add(new SqlParameter("@ID", objObject.ID));
                        sqlListParam.Add(new SqlParameter("@KodeClass", objObject.KodeGroup));
                        sqlListParam.Add(new SqlParameter("@NamaClass", objObject.NamaGroup));
                        sqlListParam.Add(new SqlParameter("@RowStatus", objObject.RowStatus));
                        sqlListParam.Add(new SqlParameter("@ClassID", objObject.GroupID));
                        break;
                    case "spUpdateAM_Lokasi":
                        sqlListParam.Add(new SqlParameter("@ID", objObject.ID));
                        sqlListParam.Add(new SqlParameter("@KodeLokasi", objObject.KodeGroup));
                        sqlListParam.Add(new SqlParameter("@NamaLokasi", objObject.NamaGroup));
                        sqlListParam.Add(new SqlParameter("@RowStatus", objObject.RowStatus));
                        sqlListParam.Add(new SqlParameter("@AliasName", objObject.NamaClass));
                        break;
                    case "spUpdateAM_Asset":
                        sqlListParam.Add(new SqlParameter("@ID", objObject.ID));
                        sqlListParam.Add(new SqlParameter("@GroupID", objObject.GroupID));//1
                        sqlListParam.Add(new SqlParameter("@ClassID", objObject.ClassID));//2
                        sqlListParam.Add(new SqlParameter("@SubClassID", objObject.SubClassID));//3
                        sqlListParam.Add(new SqlParameter("@LokasiID", objObject.LokasiID));//4
                        sqlListParam.Add(new SqlParameter("@KodeAsset", objObject.KodeAsset));//6
                        sqlListParam.Add(new SqlParameter("@NamaAsset", objObject.Deskripsi));//7
                        sqlListParam.Add(new SqlParameter("@NilaiAsset", objObject.NilaiAsset));//8
                        sqlListParam.Add(new SqlParameter("@AssyDate", objObject.TglAsset));//9
                        sqlListParam.Add(new SqlParameter("@MfgDate", objObject.TglAsset));//10
                        sqlListParam.Add(new SqlParameter("@MfgYear", objObject.TglAsset.Year));//11
                        sqlListParam.Add(new SqlParameter("@LifeTime", objObject.UmurAsset));//12
                        sqlListParam.Add(new SqlParameter("@DepreciatID", objObject.MethodDep));//13
                        sqlListParam.Add(new SqlParameter("@StartDeprec", objObject.TglSusut));//14
                        sqlListParam.Add(new SqlParameter("@ItemKode", objObject.ItemKode));//15
                        sqlListParam.Add(new SqlParameter("@PicDept", objObject.PicDept));
                        sqlListParam.Add(new SqlParameter("@PicPerson", objObject.PicPerson));
                        sqlListParam.Add(new SqlParameter("@PlantID", objObject.PlantID));
                        sqlListParam.Add(new SqlParameter("@RowStatus", objObject.RowStatus));
                        break;
                    case "spUpdateAM_Asset_Mutasi":
                        sqlListParam.Add(new SqlParameter("@ID", objObject.ID)); 
                        sqlListParam.Add(new SqlParameter("@KodeAsset", objObject.KodeAsset));
                        sqlListParam.Add(new SqlParameter("@TglMutasi", objObject.TglMutasi));
                        sqlListParam.Add(new SqlParameter("@MutasiAsal", objObject.MutasiAsal));
                        sqlListParam.Add(new SqlParameter("@MutasiTujuan", objObject.MutasiTujuan));
                        sqlListParam.Add(new SqlParameter("@MutasiReson", objObject.MutasiReson));
                        sqlListParam.Add(new SqlParameter("@EfektifDate", objObject.TglEfektif));
                        sqlListParam.Add(new SqlParameter("@RowStatus", objObject.RowStatus));
                        break;
                }
                int intResult = dataAccess.ProcessData(sqlListParam, TableName);
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }
        public override int Delete(object objDomain,string TableName)
        {
            return 0;            
        }
        public override ArrayList Retrieve()
        {
            arrObject = new ArrayList();
            return arrObject;
        }
        /// <summary>
        /// Retrieve Data
        /// </summary>
        /// <param name="table"></param>
        /// <param name="orderby"></param>
        /// <returns>ArrayList</returns>
        public ArrayList Retrieve(string table, string orderby)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string query = string.Empty; string query2 = string.Empty; string query3 = string.Empty;
            if (table == "AM_Class")
            {
                query = " ,(select COUNT(B.NamaClass) from AM_SubClass B where B.ClassID=A.ID and B.RowStatus>-1 and B.ID in (select SUbClassID from AM_Asset where RowStatus>-1 and ValAsset>0))JumlahAsset ";
                query2 = " A "; query3 = "A.ID";
            }
            else
            {
                query = ""; query2 = " "; query3 = "ID";
            }
           
            strSQL =               
            " SELECT Row_Number()Over(Order by "+query3+") as IDn,* " + query + " " +
            " FROM " + table + query2 + orderby ;
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrObject = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read()) { arrObject.Add(GenerateObject(table,sqlDataReader)); }
            }
            else { arrObject.Add(new AssetManagement()); }
            return arrObject;
        }
        public ArrayList RetrieveNewID(string table, string orderby, string QueryFld)
        {
            string query = string.Empty;

            if (table == "AM_Class")
            {
                query = " ,0'JumlahAsset' ";
            }
            else
            {
                query = "";
            }
            strSQL = "SELECT " + QueryFld + query + " FROM " + table + orderby;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrObject = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read()) { arrObject.Add(GenerateObject(table,sqlDataReader)); }
            }
            else { 
                arrObject.Add(new AssetManagement());
            }
            return arrObject;
        }
        public int GetItemIDAssetPlant(string ItemCode, string table)
        { 
            int ItemID = 0;
            try
            {
                string tbl = (table == string.Empty) ? "Asset where ItemCode" : table;
               
                string strSQL = "Select Top 1 ID from " + tbl + "='" + ItemCode + "'";
                DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
                strError = dataAccess.Error;
                arrObject = new ArrayList();
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        ItemID = Convert.ToInt32(sqlDataReader["ID"].ToString());
                    }
                }
            }
            catch
            {
                ItemID = 0;
            }
                return ItemID;
           
        }
        public int GetLastNumAssetKomponen(int groupID, int classID, int subClassID)
        {
            int lastNum = 0;
            try
            {
                string strSQL = "select MAX( CONVERT(int, RIGHT(KodeAsset,3) ) ) as LastCode  from AM_Asset where GroupID="+groupID+" and ClassID="+classID+" and SubClassID="+subClassID+" and LEN(KodeAsset)=12 Group by GroupID,ClassID,SubClassID";
                DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
                strError = dataAccess.Error;
                arrObject = new ArrayList();
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        lastNum = Convert.ToInt32(sqlDataReader["LastCode"].ToString());
                    }
                }
            }
            catch
            {
                lastNum = 0;
            }
            return lastNum;

        }
        public AssetManagement GenerateObject(string Table, SqlDataReader sqlDataReader)
        {
            objObject = new AssetManagement();
            switch (Table)
            {
                case "AM_Group":
                objObject.KodeGroup= Convert.ToString(sqlDataReader["KodeGroup"]);
                objObject.NamaGroup= Convert.ToString(sqlDataReader["NamaGroup"]);
                objObject.UmurAsset = Convert.ToInt32(sqlDataReader["UmurPakai"]);
                objObject.ID = Convert.ToInt32(sqlDataReader["ID"]);
                objObject.IDn = Convert.ToInt32(sqlDataReader["IDn"]);
                objObject.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
                break;

                case "AM_Class":
                objObject.KodeClass= Convert.ToString(sqlDataReader["KodeClass"]);
                objObject.NamaClass= Convert.ToString(sqlDataReader["NamaClass"]);
                objObject.ID = Convert.ToInt32(sqlDataReader["ID"]);
                objObject.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
                objObject.GroupID = Convert.ToInt32(sqlDataReader["GroupID"]);
                objObject.JumlahAsset = Convert.ToDecimal(sqlDataReader["JumlahAsset"]);
                break;

                case "AM_Class_view":
                objObject.ID        = Convert.ToInt32(sqlDataReader["ID"]);
                objObject.IDn       = Convert.ToInt32(sqlDataReader["IDn"]);
                objObject.KodeClass = Convert.ToString(sqlDataReader["KodeClass"]);
                objObject.NamaClass = Convert.ToString(sqlDataReader["NamaClass"]);
                objObject.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
                objObject.GroupID   = Convert.ToInt32(sqlDataReader["GroupID"]);
                objObject.NamaGroup = Convert.ToString(sqlDataReader["NamaGroup"]);
                break;
                case "AM_SubClass":
                objObject.KodeGroup = Convert.ToString(sqlDataReader["KodeClass"]);
                objObject.NamaGroup = Convert.ToString(sqlDataReader["NamaClass"]);
                objObject.ID        = Convert.ToInt32(sqlDataReader["ID"]);
                objObject.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
                objObject.ClassID   = Convert.ToInt32(sqlDataReader["ClassID"]);
                break;
                case "AM_SubClass_view":
                objObject.ID        = Convert.ToInt32(sqlDataReader["ID"]);
                objObject.IDn       = Convert.ToInt32(sqlDataReader["IDn"]);
                objObject.NamaClass = Convert.ToString(sqlDataReader["nm_class"]);
                objObject.KodeGroup = Convert.ToString(sqlDataReader["KodeClass"]);
                objObject.NamaGroup = Convert.ToString(sqlDataReader["NamaClass"]);
                objObject.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
                objObject.ClassID = Convert.ToInt32(sqlDataReader["ClassID"]);
                break;
                case "AM_Lokasi":
                objObject.ID = Convert.ToInt32(sqlDataReader["ID"]);
                objObject.NamaClass=Convert.ToString(sqlDataReader["AliasName"]);
                objObject.KodeGroup=Convert.ToString(sqlDataReader["KodeLokasi"]);
                objObject.NamaGroup = Convert.ToString(sqlDataReader["NamaLokasi"]);
                objObject.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
                break;
                case "AM_Asset_Material_v":
                    objObject.ID = Convert.ToInt32(sqlDataReader["ID"]);
                    objObject.KodeGroup=Convert.ToString(sqlDataReader["ItemCode"]);
                    objObject.NamaGroup=Convert.ToString(sqlDataReader["ItemName"]);
                    objObject.KodeClass = Convert.ToString(sqlDataReader["Harga"]);
                break;
                case "AM_Asset_v":
                    objObject.ID        = Convert.ToInt32(sqlDataReader["ID"]);
                    objObject.GroupID   =Convert.ToInt32(sqlDataReader["GroupID"]);
                    objObject.ClassID   =Convert.ToInt32(sqlDataReader["ClassID"]);
                    objObject.SubClassID=Convert.ToInt32(sqlDataReader["SubClassID"]);
                    objObject.LokasiID  =Convert.ToInt32(sqlDataReader["LokasiID"]);
                    objObject.KodeGroup = Convert.ToString(sqlDataReader["KodeGroup"]);
                    objObject.KodeClass = Convert.ToString(sqlDataReader["KodeClass"]);
                    objObject.KodeSubClass = Convert.ToString(sqlDataReader["KodeSubClass"]);
                    objObject.NamaGroup = Convert.ToString(sqlDataReader["NamaGroup"]);
                    objObject.NamaClass = Convert.ToString(sqlDataReader["NamaClass"]);
                    objObject.NamaSubClass = Convert.ToString(sqlDataReader["NamaSubClass"]);
                    objObject.KodeLokasi=Convert.ToString(sqlDataReader["KodeLokasi"]);
                    objObject.NamaLokasi=Convert.ToString(sqlDataReader["NamaLokasi"]);
                    objObject.KodeAsset =Convert.ToString(sqlDataReader["KodeAsset"]);
                    objObject.ItemKode = Convert.ToString(sqlDataReader["ItemKode"]);
                    objObject.Deskripsi =Convert.ToString(sqlDataReader["NamaAsset"]);
                    objObject.TglAsset  =(sqlDataReader["AssyDate"]==DBNull.Value)?DateTime.MinValue: Convert.ToDateTime(sqlDataReader["AssyDate"]);
                    objObject.NilaiAsset =(sqlDataReader["ValAsset"]==DBNull.Value)?0: Convert.ToDecimal (sqlDataReader["ValAsset"]);
                    objObject.TglSusut = (sqlDataReader["StatDeprec"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(sqlDataReader["StatDeprec"]);
                    objObject.UmurAsset = (sqlDataReader["LifeTime"] != DBNull.Value) ? Convert.ToInt32(sqlDataReader["LifeTime"]) : 0;
                    objObject.RowStatus =Convert.ToInt32(sqlDataReader["RowStatus"]);
                    objObject.MethodDep = (sqlDataReader["DepreciatID"] != DBNull.Value) ? Convert.ToInt32(sqlDataReader["DepreciatID"]) : 0;
                    objObject.PicDept = (sqlDataReader["PICDept"] != DBNull.Value) ? Convert.ToInt32(sqlDataReader["PICDept"]) : 0;
                    objObject.PicPerson = Convert.ToString(sqlDataReader["PicPerson"]);
                    objObject.PlantID = (sqlDataReader["PlantID"] != DBNull.Value) ? Convert.ToInt32(sqlDataReader["PlantID"]) : 0;
                break;
                case "Dept":
                    objObject.ID = Convert.ToInt32(sqlDataReader["ID"]);
                    objObject.IDn = Convert.ToInt32(sqlDataReader["IDn"]);
                    objObject.KodeGroup = Convert.ToString(sqlDataReader["DeptCode"]);
                    objObject.NamaGroup = Convert.ToString(sqlDataReader["DeptName"]);
                    objObject.Deskripsi = Convert.ToString(sqlDataReader["NamaHead"]);
                    objObject.GroupID = (Convert.IsDBNull(sqlDataReader["HeadID"]))?0:Convert.ToInt32(sqlDataReader["HeadID"]);
                break;
                case "Plant":
                    objObject.ID = Convert.ToInt32(sqlDataReader["ID"]);
                    objObject.IDn = Convert.ToInt32(sqlDataReader["ID"]);
                    objObject.KodeGroup = Convert.ToString(sqlDataReader["KodePlant"]);
                    objObject.NamaGroup = Convert.ToString(sqlDataReader["NamaPlant"]);
               break;
                case "AM_Asset_Mutasi_v":
                    
                    objObject.ID = Convert.ToInt32(sqlDataReader["ID"]);
                    objObject.IDn = Convert.ToInt32(sqlDataReader["IDn"]);
                    objObject.KodeAsset = Convert.ToString(sqlDataReader["KodeAsset"]);
                    objObject.Deskripsi = Convert.ToString(sqlDataReader["NamaAsset"]);
                    objObject.MutasiAsal = Convert.ToString(sqlDataReader["MutasiAsale"]);
                    objObject.MutasiTujuan = Convert.ToString(sqlDataReader["MutasiTujuane"]);
                    objObject.MutasiReson = Convert.ToString(sqlDataReader["AlasanMutasi"]);
                    objObject.TglMutasi2 = Convert.ToString(sqlDataReader["TglMutasi"].ToString());
                    objObject.TglEfektif2 = Convert.ToString(sqlDataReader["TglEfektif"].ToString());
                    break;
                case "AM_Asset_Detail_v":
                    objObject.ID = Convert.ToInt32(sqlDataReader["ID"]);
                    objObject.TglMutasi2 = Convert.ToString(sqlDataReader["ReceiptDate"]);
                    objObject.TglEfektif2 = Convert.ToString(sqlDataReader["PakaiDate"]);
                    objObject.NilaiAsset = Convert.ToInt32(sqlDataReader["Harga"]);
                    break;
                case "MTC_GroupSarmut":
                    objObject.ID = Convert.ToInt32(sqlDataReader["ID"]);
                    objObject.KodeGroup = Convert.ToString(sqlDataReader["Kode"]);
                    objObject.NamaGroup = Convert.ToString(sqlDataReader["GroupName"]);
                    objObject.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
                    break;
                case "MaterialMTCGroup":
                    objObject.ID = Convert.ToInt32(sqlDataReader["ID"]);
                    objObject.KodeGroup = Convert.ToString(sqlDataReader["Kode"]);
                    objObject.NamaGroup = Convert.ToString(sqlDataReader["GroupName"]);
                    objObject.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
                    break;
                case "AM_Asset_v_New":
                    objObject.KodeGroup = Convert.ToString(sqlDataReader["KodeClass"]);
                    objObject.NamaGroup = Convert.ToString(sqlDataReader["NamaClass"]);
                    objObject.ID = Convert.ToInt32(sqlDataReader["ID"]);
                    objObject.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
                    objObject.ClassID = Convert.ToInt32(sqlDataReader["ClassID"]);
                    objObject.JumlahAsset = Convert.ToDecimal(sqlDataReader["JumlahAsset"]);
                    objObject.KodeAsset = Convert.ToString(sqlDataReader["KodeAsset"]);
                    objObject.NilaiAsset = (sqlDataReader["ValAsset"] == DBNull.Value) ? 0 : Convert.ToDecimal(sqlDataReader["ValAsset"]);
                    objObject.NamaLokasi = Convert.ToString(sqlDataReader["NamaLokasi"]);

                    break;
                case "asset":
                    objObject.ItemName = sqlDataReader["ItemName"].ToString();
                    objObject.AMLokasiID = Convert.ToInt32(sqlDataReader["AMLokasiID"]);
                  
                    break;

            }
            return objObject;
        }

        public ArrayList RetrieveID(string TableName)
        {
            strSQL = "SELECT ROW_NUMBER() OVER(ORDER BY ID) AS IDn,* FROM " + TableName + " WHERE RowStatus >-1 ORDER BY ID";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrObject = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read()) { arrObject.Add(GenerateObject(TableName, sqlDataReader)); }
            }
            else 
            { 
                arrObject.Add(new AssetManagement()); 
            }
            return arrObject;
        }

        public ArrayList RetrieveClassID(string TableName, string GroupID)
        {
            strSQL = 
            "SELECT ROW_NUMBER() OVER(ORDER BY ID) AS IDn,* FROM " + TableName + " "+
            "WHERE GroupID="+GroupID+" and RowStatus >-1 ORDER BY ID";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrObject = new ArrayList();

            if (dataAccess.Error == string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrObject.Add(new AssetManagement
                    {
                        ID = Convert.ToInt32(sqlDataReader["ID"].ToString()),
                        //DeptID = Convert.ToInt32(sdr["DeptID"].ToString()),
                        //UserID = Convert.ToInt32(sdr["UserID"].ToString()),
                        NamaClass = sqlDataReader["NamaClass"].ToString()
                    });
                }
            }
            return arrObject;
        }

        public string GetGroupName(int idGroup)
        {
            string grpName = string.Empty;
            string strSQL = "Select * from AM_Group where ID=" + idGroup;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            if (sqlDataReader.HasRows && dataAccess.Error == string.Empty)
            {
                while (sqlDataReader.Read())
                {
                    grpName = sqlDataReader["NamaGroup"].ToString();
                }
            }
            return grpName;
        }

        public AssetManagement GetAssetDetail( string ID,string TableName)
        {
            objObject = new AssetManagement();
            string strSQL = "Select * from " + TableName + " where ID=" + ID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    return GenerateObject(TableName, sdr);
                }
            }
            return objObject;
        }

        public ArrayList TahunData()
        {
            arrObject = new ArrayList();
            string strSQL = "Select Year(TanggalBA)Tahun From AM_Asset_Disposal group by Year(TanggalBA)";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrObject.Add(GenerateDisposal(sdr));
                }
            }

            return arrObject;
        }
        private Disposal GenerateDisposal(SqlDataReader sdr)
        {
            objDisposal = new Disposal();
            objDisposal.Tahun = int.Parse(sdr["Tahun"].ToString());
            return objDisposal;
        }
        private Disposal GenerateDisposal(SqlDataReader sdr, bool DataList)
        {
            objDisposal = new Disposal();
            objDisposal.DeptID = int.Parse(sdr["DeptID"].ToString());
            objDisposal.BANumber = sdr["BANUmber"].ToString();
            objDisposal.AdjustNo = sdr["AdjustNo"].ToString();
            objDisposal.AdjustID = int.Parse(sdr["AdjustID"].ToString());
            objDisposal.AlasanDisposal = sdr["AlasanDisposal"].ToString();
            objDisposal.DeptName = sdr["DeptName"].ToString();
            objDisposal.AssetID = int.Parse(sdr["AssetID"].ToString());
            objDisposal.AssetCode = sdr["KodeAsset"].ToString();
            objDisposal.AssetName = sdr["NamaAsset"].ToString();
            objDisposal.Lokasi = sdr["NamaLokasi"].ToString();
            objDisposal.TanggalBA = DateTime.Parse(sdr["TanggalBA"].ToString());
            return objDisposal;
        }
        public ArrayList RetieveDisposal()
        {
            arrObject = new ArrayList();
            string strSQL = "SELECT amd.*,ama.KodeAsset,ama.NamaAsset,ama.LifeTime,aml.NamaLokasi,d.DeptName " +
                          "  FROM AM_Asset_disposal amd " +
                          "  LEFT JOIN AM_Asset ama ON ama.ID=amd.AssetID " +
                          "  LEFT JOIN AM_Lokasi aml ON aml.ID=ama.LokasiID " +
                          "  LEFT JOIN Dept as d ON d.ID=amd.DeptID " +
                          "  where amd.RowStatus>-1" + this.Criteria;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrObject.Add(GenerateDisposal(sdr, true));
                }
            }
            return arrObject;
        }

        public string CekDataClass(string Group, string NamaAsset)
        {
            string result = string.Empty;
            string StrSql = " select NamaClass from AM_Class where NamaClass='"+NamaAsset+"' and GroupID="+Group+" and RowStatus>-1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    result = sqlDataReader["NamaClass"].ToString();
                }
            }

            return result;
        }

        public int CekLastKodeClass(string groupID)
        {
            int lastcode = 0;
            try
            {
                string strSQL = " select COUNT(NamaClass)lastcode from AM_Class where GroupID=" + groupID + " and RowStatus>-1 ";
                DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
                strError = dataAccess.Error;
                arrObject = new ArrayList();
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        lastcode = Convert.ToInt32(sqlDataReader["lastcode"].ToString());
                    }
                }
            }
            catch
            {
                lastcode = 0;
            }
            return lastcode;

        }

        public ArrayList Retrieve_New1(string select, string table, string orderby, string Where2)
        {
            //objObject.ID = Convert.ToInt32(sqlDataReader["ID"]);
            //objObject.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            //objObject.ClassID = Convert.ToInt32(sqlDataReader["ClassID"]);
            //objObject.JumlahAsset = Convert.ToDecimal(sqlDataReader["JumlahAsset"]);
            //objObject.KodeAsset = Convert.ToString(sqlDataReader["KodeAsset"]);
            //objObject.NilaiAsset = (sqlDataReader["ValAsset"] == DBNull.Value) ? 0 : Convert.ToDecimal(sqlDataReader["ValAsset"]);
            //objObject.NamaLokasi = Convert.ToString(sqlDataReader["NamaLokasi"]);
            string B = string.Empty;
            if (table == "AM_SubClass")
            {
                B = "AM_Asset_v";
            }
            else
            {
                B = "AM_Asset_v";
            }

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            if (select == "MTC")                
                strSQL =
                " select distinct SubClassID as ID,KodeSubClass as KodeClass,"+                
                " case when B.NamaAsset=A.NamaClass then B.KodeAsset else case "+
                " when LEN(PlantID)=1 then SUBSTRING(CAST(PlantID as NCHAR),1,1)+'.'+SUBSTRING(CAST(GroupID as CHAR),1,1)+'.'+(select case when LEN(KodeClass)=1 then '00'+KodeClass when LEN(KodeClass)=2 then '0'+KodeClass else KodeClass end KodeClass from AM_Class A2 where A2.ID=B.ClassID and A2.RowStatus>-1)+'.'+(select case when LEN(DeptID)=1 then substring(cast(DeptID as NCHAR),1,1) when LEN(DeptID)=2 then substring(cast(DeptID as NCHAR),1,2) end DeptID from AM_Department where RowStatus>-1 and DeptID_ID=B.OwnerDeptID)+'.'+(select cast(KodeClass as NCHAR) from AM_SubClass A1 where A1.ID=B.SubClassID and A1.RowStatus>-1) "+
                " when LEN(PlantID)=2 then SUBSTRING(CAST(PlantID as NCHAR),1,2)+'.'+SUBSTRING(CAST(GroupID as CHAR),1,1)+'.'+(select case when LEN(KodeClass)=1 then '00'+KodeClass when LEN(KodeClass)=2 then '0'+KodeClass else KodeClass end KodeClass from AM_Class A2 where A2.ID=B.ClassID and A2.RowStatus>-1)+'.'+(select case when LEN(DeptID)=1 then substring(cast(DeptID as NCHAR),1,1) when LEN(DeptID)=2 then substring(cast(DeptID as NCHAR),1,2) end DeptID from AM_Department where RowStatus>-1 and DeptID_ID=B.OwnerDeptID)+'.'+(select cast(KodeClass as NCHAR) from AM_SubClass A1 where A1.ID=B.SubClassID and A1.RowStatus>-1) " +
                " end end KodeAsset, "+
                " NamaSubClass as NamaClass,sum(ValAsset)ValAsset,0 as RowStatus,A.ClassID,NamaLokasi,case when sum(ValAsset)=0 then 0 else 1 end JumlahAsset "+
                " FROM " + table + orderby +
                " group by KodeSubClass,SubClassID,A.RowStatus,A.ClassID,B.ClassID,NamaLokasi,NamaAsset,A.NamaClass,OwnerDeptID,KodeAsset,PlantID,GroupID,B.NamaSubClass ";
           
            else                
                strSQL =                    
                //" select '0'ID,'0'RowStatus,'0'ClassID,GroupID2 KodeClass,NamaClass,case when CodeAsset like'%.000%' then REPLACE(CodeAsset,'.000','') else CodeAsset end KodeAsset,ValAsset,NamaLokasi,Qty JumlahAsset " +
                //" from (select *,Data2.KodeAsset+'.'+case when LEN(No)=1 then '00'+ cast (No as NCHAR) when LEN(No)=2 then '0'+ cast (No as NCHAR) when LEN(No)=3 then  cast (No as NCHAR) end CodeAsset,case when Data2.GroupID=0 then '' else Data2.GroupID end GroupID2 " +
                //" from (select Urutan,CAST (GroupID as nchar)GroupID,NamaClass,KodeAsset,case when Data1.NamaClass='' then '' when Data1.NamaClass<>'' then  ROW_NUMBER() OVER(ORDER BY Data1.NamaClass asc)-1 end No,case when Data1.Qty>0 then Data1.Qty else COUNT(Data1.NamaClass)  end Qty ,ValAsset,ISNULL(NamaLokasi,'')NamaLokasi,SubClassID " +
                //" from (select ID SubClassID,(select top 1 am.GroupID from AM_Asset_v am where am.SubClassID=A.ID and am.ClassID=A.ClassID and am.RowStatus>-1)GroupID,'1'Urutan,NamaClass KodeAsset,''NamaClass,(select COUNT(C.NamaClass) from AM_Asset_v C where C.ClassID=A.ClassID and C.SubClassID=A.ID and C.RowStatus>-1 group by C.ClassID,C.SubClassID)Qty,(select SUM(D.ValAsset) from AM_Asset_v D where D.ClassID=A.ClassID and D.SubClassID=A.ID and D.RowStatus>-1 group by D.ClassID,D.SubClassID)ValAsset,''AssyDate,''NamaLokasi " +
                //" from " + Where2 + "" +
               
                //" union all " +

                //" select SubClassID,''GroupID,'2'Urutan,SUBSTRING(NewKodeAsset,1,13)KodeAsset,NamaAsset NamaClass,''Qty,ValAsset,AssyDate,NamaLokasi " +
                //" from " + table + orderby;

                " select '0'ID,'0'RowStatus,'0'ClassID,GroupID2 KodeClass,NamaClass,KodeAsset,ValAsset,''NamaLokasi,Qty JumlahAsset " +
                " from (select *,case when Data2.GroupID=0 then '' else Data2.GroupID end GroupID2 " +
                " from (select Urutan,CAST (GroupID as nchar)GroupID,NamaClass,KodeAsset, Qty ,sum(ValAsset)ValAsset,SubClassID " +
                " from (select ID SubClassID,(select top 1 am.GroupID from AM_Asset_v am where am.SubClassID=A.ID and am.ClassID=A.ClassID and am.RowStatus>-1)GroupID,'1'Urutan,NamaClass KodeAsset,''NamaClass,0 Qty,0 ValAsset " +
                " from " + Where2 + "" +

                " union all " +

                " select SubClassID,''GroupID,'2'Urutan,SUBSTRING(NewKodeAsset,1,13)KodeAsset,NamaAsset NamaClass,COUNT(NamaAsset)Qty,SUM(ValAsset)ValAsset " +
                " from " + table + orderby;

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrObject = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                //while (sqlDataReader.Read()) { arrObject.Add(GenerateObject(table + "_New", sqlDataReader)); }
                while (sqlDataReader.Read()) { arrObject.Add(GenerateObject(B + "_New", sqlDataReader)); }
            }
            else { arrObject.Add(new AssetManagement()); }
            return arrObject;
        }

        public string GetPICperson(int classID)
        {
            string grpName = string.Empty;
            string strSQL = "SELECT top 1 PICPerson FROM AM_Asset_v where ClassID=" + classID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            if (sqlDataReader.HasRows && dataAccess.Error == string.Empty)
            {
                while (sqlDataReader.Read())
                {
                    grpName = sqlDataReader["PICPerson"].ToString();
                }
            }
            return grpName;
        }

        public ArrayList RetrieveSubCLass(string Grp, string classID)
        {
            ArrayList arrData = new ArrayList();
            string strSQL =

            " select ID,NamaClass NamaSubClass from AM_SubClass where ClassID in (select ID from AM_Class where ID=" + classID + " and " +
            " GroupID=" + Grp + " and RowStatus>-1) and RowStatus>-1 ";

            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new AssetManagement
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),                       
                        NamaSubClass = sdr["NamaSubClass"].ToString()
                    });
                }
            }
            return arrData;
        }

        public ArrayList RetrieveClass(string where)
        {
            ArrayList arrData = new ArrayList();
            string strSQL =

            " select Row_number() OVER(ORDER BY ID) as IDn,* from AM_SubClass " + where + " ";

            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new AssetManagement
                    {
                        //ID = Convert.ToInt32(sdr["ID"].ToString()),
                        IDn = Convert.ToInt32(sdr["IDn"].ToString()),
                        KodeClass = sdr["KodeClass"].ToString(),
                        NamaClass = sdr["NamaClass"].ToString()
                    });
                }
            }
            return arrData;
        }

        public string Criteria { get; set; }
         
    }
}
