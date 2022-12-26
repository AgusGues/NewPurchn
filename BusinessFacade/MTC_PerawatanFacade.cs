using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;

namespace BusinessFacade
{
    public class MTC_PerawatanFacade : AbstractFacade
    {
        private MTC_Perawatan objMTC_Perawatan = new MTC_Perawatan();
        private ArrayList arrMTC_Perawatan;
        private List<SqlParameter> sqlListParam;

        public MTC_PerawatanFacade()
            : base()
        {
           
        }

        public override int Insert(object objDomain)
        {
            try
            {
                objMTC_Perawatan = (MTC_Perawatan)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@DeptID", objMTC_Perawatan.DeptID));
                sqlListParam.Add(new SqlParameter("@PlantID", objMTC_Perawatan.PlantID));
                sqlListParam.Add(new SqlParameter("@ZonaID", objMTC_Perawatan.ZonaID));
                sqlListParam.Add(new SqlParameter("@MacSysID", objMTC_Perawatan.MacSysID));
                sqlListParam.Add(new SqlParameter("@MacSysPartID", objMTC_Perawatan.MacSysPartID));
                sqlListParam.Add(new SqlParameter("@PerawatanName", objMTC_Perawatan.PerawatanName));
                sqlListParam.Add(new SqlParameter("@PerawatanDesc", objMTC_Perawatan.PerawatanDesc));
                sqlListParam.Add(new SqlParameter("@Tanggal", objMTC_Perawatan.Tanggal));
                sqlListParam.Add(new SqlParameter("@JmlMenit", objMTC_Perawatan.JmlMenit));
                sqlListParam.Add(new SqlParameter("@PakaiDetailID", objMTC_Perawatan.PakaiDetailID));
                sqlListParam.Add(new SqlParameter("@ItemID", objMTC_Perawatan.ItemID));
                sqlListParam.Add(new SqlParameter("@Quantity", objMTC_Perawatan.Quantity));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objMTC_Perawatan.CreatedBy));
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertMTC_Perawatan");
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
            throw new NotImplementedException();
        }

        public override int Delete(object objDomain)
        {
            objMTC_Perawatan = (MTC_Perawatan)objDomain;
            sqlListParam = new List<SqlParameter>();
            sqlListParam.Add(new SqlParameter("@ID", objMTC_Perawatan.ID));
            sqlListParam.Add(new SqlParameter("@createdBy", objMTC_Perawatan.CreatedBy ));
            int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteMTC_Perawatan");
            strError = dataAccess.Error;
            return intResult;
        }

        public override System.Collections.ArrayList Retrieve()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "SELECT ID,DeptID, case when DeptID >0 then (select deptname from dept where id= A.DeptID) end DeptName, " +
                "PlantID,case when PlantID>0 then (select PlantName from bm_plant where id= A.PlantID) end PalntName,  " +
                "ZonaID, case when ZonaID>0 then (select ZonaName from MTC_Zona where id = A.ZonaID) end ZonaName,  " +
                "MacSysID, case when  MacSysID>0 then (select MacSysName from MTC_MacSys where id = A.MacSysID) end MacSysName,  " +
                "MacSysPartID,case when MacSysPartID>0 then (select MacSysPartName from mtc_MacSysPart where id =MacSysPartID) end MacSysPartName,  " +
                "PerawatanName, PerawatanDesc, Tanggal, JmlMenit, PakaiDetailID,  " +
                "ItemID, case when  ItemID>0 then (select itemname from Inventory where ID=A.itemid) end ItemName, Quantity " +
                "FROM MTC_Perawatan A where A.rowstatus>-1";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrMTC_Perawatan = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrMTC_Perawatan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrMTC_Perawatan.Add(new MTC_Perawatan());

            return arrMTC_Perawatan;
        }

        public ArrayList RetrieveByTgl(string tanggal)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "SELECT ID,DeptID, case when DeptID >0 then (select deptname from dept where id= A.DeptID) end DeptName, " +
                "PlantID,case when PlantID>0 then (select PlantName from bm_plant where id= A.PlantID) end PlantName,  " +
                "ZonaID, case when ZonaID>0 then (select ZonaName from MTC_Zona where id = A.ZonaID) end ZonaName,  " +
                "MacSysID, case when  MacSysID>0 then (select MacSysName from MTC_MacSys where id = A.MacSysID) end MacSysName,  " +
                "MacSysPartID,case when MacSysPartID>0 then (select MacSysPartName from mtc_MacSysPart where id =MacSysPartID) end MacSysPartName,  " +
                "PerawatanName, PerawatanDesc, Tanggal, JmlMenit, PakaiDetailID,  " +
                "ItemID, case when  ItemID>0 then (select itemname from Inventory where ID=A.itemid) end ItemName, Quantity " +
                "FROM MTC_Perawatan A where  A.rowstatus>-1 and convert(varchar,tanggal,112)='" + tanggal + "'";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrMTC_Perawatan = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrMTC_Perawatan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrMTC_Perawatan.Add(new MTC_Perawatan());
            return arrMTC_Perawatan;
        }

        public ArrayList RetrieveByMC(string kode)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "SELECT ID,DeptID, case when DeptID >0 then (select deptname from dept where id= A.DeptID) end DeptName, " +
                "PlantID,case when PlantID>0 then (select PlantName from bm_plant where id= A.PlantID) end PlantName,  " +
                "ZonaID, case when ZonaID>0 then (select ZonaName from MTC_Zona where id = A.ZonaID) end ZonaName,  " +
                "MacSysID, case when  MacSysID>0 then (select MacSysName from MTC_MacSys where id = A.MacSysID) end MacSysName,  " +
                "MacSysPartID,case when MacSysPartID>0 then (select MacSysPartName from mtc_MacSysPart where id =MacSysPartID) end MacSysPartName,  " +
                "PerawatanName, PerawatanDesc, Tanggal, JmlMenit, PakaiDetailID,  " +
                "ItemID, case when  ItemID>0 then (select itemname from Inventory where ID=A.itemid) end ItemName, Quantity " +
                "FROM MTC_Perawatan A where   A.rowstatus>-1 and MacSysPartID in (select id from MTC_MacSysPart where MacSysPartCode like'" + kode + "%')";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrMTC_Perawatan = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrMTC_Perawatan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrMTC_Perawatan.Add(new MTC_Perawatan());
            return arrMTC_Perawatan;
        }

        public MTC_Perawatan  RetrieveByID(string ID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strsql = "SELECT ID,DeptID, case when DeptID >0 then (select deptname from dept where id= A.DeptID) end DeptName, " +
                "PlantID,case when PlantID>0 then (select PlantName from bm_plant where id= A.PlantID) end PlantName,  " +
                "ZonaID, case when ZonaID>0 then (select ZonaName from MTC_Zona where id = A.ZonaID) end ZonaName,  " +
                "MacSysID, case when  MacSysID>0 then (select MacSysName from MTC_MacSys where id = A.MacSysID) end MacSysName,  " +
                "MacSysPartID,case when MacSysPartID>0 then (select MacSysPartName from mtc_MacSysPart where id =MacSysPartID) end MacSysPartName,  " +
                "PerawatanName, PerawatanDesc, Tanggal, JmlMenit, PakaiDetailID,  " +
                "ItemID, case when  ItemID>0 then (select itemname from Inventory where ID=A.itemid) end ItemName, Quantity " +
                "FROM MTC_Perawatan A where   A.rowstatus>-1 and A.id =" + ID;
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new MTC_Perawatan() ;
        }

        public MTC_Perawatan GenerateObject(SqlDataReader sqlDataReader)
        {
            objMTC_Perawatan = new MTC_Perawatan();
            objMTC_Perawatan.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objMTC_Perawatan.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objMTC_Perawatan.DeptName = sqlDataReader["DeptName"].ToString();
            objMTC_Perawatan.PlantID = Convert.ToInt32(sqlDataReader["PlantID"]);
            objMTC_Perawatan.PlantName = sqlDataReader["PlantName"].ToString();
            objMTC_Perawatan.ZonaID = Convert.ToInt32(sqlDataReader["ZonaID"]);
            objMTC_Perawatan.ZonaName = sqlDataReader["ZonaName"].ToString();
            objMTC_Perawatan.MacSysID = Convert.ToInt32(sqlDataReader["MacSysID"]);
            objMTC_Perawatan.MacSysName = sqlDataReader["MacSysName"].ToString();
            objMTC_Perawatan.MacSysPartID = Convert.ToInt32(sqlDataReader["MacSysPartID"]);
            objMTC_Perawatan.MacSysPartName = sqlDataReader["MacSysPartName"].ToString();
            objMTC_Perawatan.PerawatanName = sqlDataReader["PerawatanName"].ToString();
            objMTC_Perawatan.PerawatanDesc = sqlDataReader["PerawatanDesc"].ToString();
            objMTC_Perawatan.Tanggal = DateTime.Parse( sqlDataReader["Tanggal"].ToString() );
            objMTC_Perawatan.JmlMenit = Convert.ToInt32(sqlDataReader["JmlMenit"]);
            objMTC_Perawatan.PakaiDetailID = Convert.ToInt32(sqlDataReader["PakaiDetailID"]);
            objMTC_Perawatan.ItemID = Convert.ToInt32(sqlDataReader["ItemID"]);
            objMTC_Perawatan.ItemName = sqlDataReader["ItemName"].ToString();
            objMTC_Perawatan.Quantity = Convert.ToInt32(sqlDataReader["Quantity"]);
            return objMTC_Perawatan;
        }
    }
}
