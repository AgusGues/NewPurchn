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
    public class BreakBMFacade : AbstractFacade
    {
        private BreakBM objBreakBM = new BreakBM();
        private BreakBMCharge objBreakBMCharge = new BreakBMCharge();
        private BreakBMGroup objBreakBMGroup = new BreakBMGroup();
        private BreakBMPlant objBreakBMPlant = new BreakBMPlant();
        private planName objBreakplt = new planName();
        private BreakBMProblem objBreakBMProblem = new BreakBMProblem();
        private ArrayList arrBreakBM;
        private List<SqlParameter> sqlListParam;

        public BreakBMFacade()
            :base()

        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objBreakBM = (BreakBM)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@BreakDownNo", objBreakBM.BreakDownNo));
                sqlListParam.Add(new SqlParameter("@TglBreak", objBreakBM.TglBreak));
                sqlListParam.Add(new SqlParameter("@OperationalSche", objBreakBM.OperationalSche));
                sqlListParam.Add(new SqlParameter("@StartBD", objBreakBM.StartBD));
                sqlListParam.Add(new SqlParameter("@FinishBD", objBreakBM.FinishBD));
                sqlListParam.Add(new SqlParameter("@BDTime", objBreakBM.BDTime));
                sqlListParam.Add(new SqlParameter("@FinaltyBD", objBreakBM.FinaltyBD));
                sqlListParam.Add(new SqlParameter("@FrekBD", objBreakBM.FrekBD));
                sqlListParam.Add(new SqlParameter("@Syarat", objBreakBM.Syarat));
                sqlListParam.Add(new SqlParameter("@BM_plantGroupID", objBreakBM.BM_plantGroupID));
                sqlListParam.Add(new SqlParameter("@BM_PlantID", objBreakBM.BM_PlantID));
                sqlListParam.Add(new SqlParameter("@OperationalTime", objBreakBM.OperationalTime));
                sqlListParam.Add(new SqlParameter("@Ket", objBreakBM.Ket));
                sqlListParam.Add(new SqlParameter("@Pinalti", objBreakBM.Pinalti));
                sqlListParam.Add(new SqlParameter("@BreakBM_MasterProblemID", objBreakBM.BreakBM_MasterProblemID));
                sqlListParam.Add(new SqlParameter("@BreakBM_MasterChargeID", objBreakBM.BreakBM_MasterChargeID));
                sqlListParam.Add(new SqlParameter("@RowStatus", objBreakBM.RowStatus));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objBreakBM.CreatedBy));
                sqlListParam.Add(new SqlParameter("@GroupOff", objBreakBM.GroupOff));
                sqlListParam.Add(new SqlParameter("@Ketebalan", objBreakBM.Ketebalan));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertBreakBM");
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
                objBreakBM = (BreakBM)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objBreakBM.ID));
                //sqlListParam.Add(new SqlParameter("@BreakDownNo", objBreakBM.BreakDownNo));
                sqlListParam.Add(new SqlParameter("@TglBreak", objBreakBM.TglBreak));
                //sqlListParam.Add(new SqlParameter("@OperationalSche", objBreakBM.OperationalSche));
                sqlListParam.Add(new SqlParameter("@StartBD", objBreakBM.StartBD));
                sqlListParam.Add(new SqlParameter("@FinishBD", objBreakBM.FinishBD));
                sqlListParam.Add(new SqlParameter("@BDTime", objBreakBM.BDTime));
                sqlListParam.Add(new SqlParameter("@FinaltyBD", objBreakBM.FinaltyBD));
                sqlListParam.Add(new SqlParameter("@FrekBD", objBreakBM.FrekBD));
                sqlListParam.Add(new SqlParameter("@Syarat", objBreakBM.Syarat));
                sqlListParam.Add(new SqlParameter("@BM_plantGroupID", objBreakBM.BM_plantGroupID));
                sqlListParam.Add(new SqlParameter("@BM_PlantID", objBreakBM.BM_PlantID));
                //sqlListParam.Add(new SqlParameter("@OperationalTime", objBreakBM.OperationalTime));
                sqlListParam.Add(new SqlParameter("@Ket", objBreakBM.Ket));
                sqlListParam.Add(new SqlParameter("@Pinalti", objBreakBM.Pinalti));
                sqlListParam.Add(new SqlParameter("@BreakBM_MasterProblemID", objBreakBM.BreakBM_MasterProblemID));
                sqlListParam.Add(new SqlParameter("@BreakBM_MasterChargeID", objBreakBM.BreakBM_MasterChargeID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objBreakBM.LastModifiedBy));
                sqlListParam.Add(new SqlParameter("@GroupOff", objBreakBM.GroupOff));
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdatetBreakBM");
                strError = dataAccess.Error;
                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

        public override int  Delete(object objDomain)
        {
            try
            {
                objBreakBM = (BreakBM)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objBreakBM.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objBreakBM.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteBreakBM");
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
            
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select ID,PelarianNo,IDRegu,ReguCode,IDType,NamaType,IDUkuran,Ukuran,TglProduksi,TglTransaksi,KodePelarian,convert(int, Jumlah ) as Jumlah,RowStatus,CreatedBy,CreatedTime,LastModifiedBy,LastModifiedTime from Pel_Transaksi where RowStatus > -1");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from BreakBM where RowStatus > -1 order by ID Desc");
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select  ID,BreakDownNo,TglBreak,StartBD,FinishBD,BDTime,Syarat,BM_plantGroupID,BM_PlantID,Ket,Pinalti,BreakBM_MasterProblemID,BreakBM_MasterChargeID from BreakBM where RowStatus > -1 order by ID Desc");
            strError = dataAccess.Error;
            arrBreakBM = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBreakBM.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrBreakBM.Add(new BreakBM());

            return arrBreakBM;
        }

        public ArrayList RetrieveByLine(int BM_PlantID)
        {
            //string where = string.Empty;
            ////string strBM_PlantID = string.Empty;
            //where = (BM_PlantID == "0") ? " and BreakBM.BM_PlantID=" + BM_PlantID : string.Empty;
            //where = (BM_PlantID > 0) ? "" : " and BM_PlantID=" + BM_PlantID;
            string strSQL = "Select * from BreakBM  where RowStatus > -1 and BM_PlantID = " + BM_PlantID + " order by TglBreak Desc ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            arrBreakBM = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBreakBM.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrBreakBM.Add(new BreakBM());

            return arrBreakBM;
        }


        public BreakBM RetrieveMaxId()
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select isnull(MAX(LEFT(BreakDownNo,6)),0) as ID from BreakBM");
            strError = dataAccess.Error;
            arrBreakBM = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject5(sqlDataReader);
                }
            }

            return new BreakBM();
        }

        public BreakBM RetrieveById(int Id)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from BreakBM where RowStatus = 0 and ID = " + Id);
            strError = dataAccess.Error;
            arrBreakBM = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new BreakBM();
        }

        public ArrayList RetrieveBMCharge()
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from BreakBMCharge where RowStatus > -1");
            strError = dataAccess.Error;
            arrBreakBM = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBreakBM.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrBreakBM.Add(new BreakBMCharge());

            return arrBreakBM;
        }

        public ArrayList RetrieveKetebalan()
        {
            string strSQL = "Select * from BreakBMCharge where RowStatus > -1";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrBreakBM = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBreakBM.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrBreakBM.Add(new BreakBMCharge());

            return arrBreakBM;
        }

        public ArrayList RetrieveBMGroup()
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from Pel_MasterRegu where RowStatus > -1");
            strError = dataAccess.Error;
            arrBreakBM = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBreakBM.Add(GenerateObject3(sqlDataReader));
                }
            }
            else
                arrBreakBM.Add(new BreakBMGroup());

            return arrBreakBM;
        }

        public ArrayList RetrieveBMGroupbyparam(string param)
        {
            string strsql = "Select * from Pel_MasterRegu where RowStatus > -1 and ReguCode like '%" + param + "%'";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strsql);
            strError = dataAccess.Error;
            arrBreakBM = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBreakBM.Add(GenerateObject3(sqlDataReader));
                }
            }
            else
                arrBreakBM.Add(new BreakBMGroup());

            return arrBreakBM;
        }

        public ArrayList RetrieveBMPlant()
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from MasterPlan where RowStatus > -1");
            strError = dataAccess.Error;
            arrBreakBM = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBreakBM.Add(GenerateObject4(sqlDataReader));
                }
            }
            else
                arrBreakBM.Add(new BreakBMPlant());

            return arrBreakBM;
        }


        public ArrayList RetrieveBMProblem()
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from BreakBMProblem where RowStatus > -1 order by ID Desc");
            strError = dataAccess.Error;
            arrBreakBM = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBreakBM.Add(GenerateObject6(sqlDataReader));
                }
            }
            else
                arrBreakBM.Add(new BreakBMProblem());

            return arrBreakBM;
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from BreakBM where RowStatus = 0 and " + strField + " = '" + strValue + "'");
            strError = dataAccess.Error;
            arrBreakBM = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBreakBM.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrBreakBM.Add(new BreakBM());

            return arrBreakBM;
        }


        public BreakBMCharge GenerateObject2(SqlDataReader sqlDataReader)
        {
            objBreakBMCharge = new BreakBMCharge();
            objBreakBMCharge.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objBreakBMCharge.LokasiCharge = sqlDataReader["LokasiCharge"].ToString();
            objBreakBMCharge.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objBreakBMCharge.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objBreakBMCharge.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objBreakBMCharge.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objBreakBMCharge.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objBreakBMCharge;
        }

        public BreakBMGroup GenerateObject3(SqlDataReader sqlDataReader)
        {
            objBreakBMGroup = new BreakBMGroup();
            objBreakBMGroup.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objBreakBMGroup.ReguCode = sqlDataReader["ReguCode"].ToString();
            objBreakBMGroup.PlanID = Convert.ToInt32(sqlDataReader["PlanID"]);
            objBreakBMGroup.PlanName = sqlDataReader["PlanName"].ToString();
            objBreakBMGroup.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objBreakBMGroup.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objBreakBMGroup.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objBreakBMGroup.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objBreakBMGroup.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objBreakBMGroup;
        }

        public BreakBMPlant GenerateObject4(SqlDataReader sqlDataReader)
        {
            objBreakBMPlant = new BreakBMPlant();
            objBreakBMPlant.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objBreakBMPlant.PlanCode = sqlDataReader["PlanCode"].ToString();
            objBreakBMPlant.PlanName = sqlDataReader["PlanName"].ToString();
            objBreakBMPlant.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objBreakBMPlant.DeptName = sqlDataReader["DeptName"].ToString();
            objBreakBMPlant.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objBreakBMPlant.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objBreakBMPlant.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objBreakBMPlant.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objBreakBMPlant.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            //objBreakBMPlant.MasterPlanID = sqlDataReader["MasterPlanID"].ToString();
            return objBreakBMPlant;
        }

        public BreakBMProblem GenerateObject6(SqlDataReader sqlDataReader)
        {
            objBreakBMProblem = new BreakBMProblem();
            objBreakBMProblem.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objBreakBMProblem.LokasiProblem = sqlDataReader["LokasiProblem"].ToString();
            objBreakBMProblem.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objBreakBMProblem.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objBreakBMProblem.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objBreakBMProblem.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objBreakBMProblem.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objBreakBMProblem;
        }

        public BreakBM GenerateObject5(SqlDataReader sqlDataReader)
        {
            objBreakBM = new BreakBM();
            objBreakBM.ID = Convert.ToInt32(sqlDataReader["ID"]);
            return objBreakBM;
        }

        public BreakBM GenerateObject(SqlDataReader sqlDataReader)
        {
            objBreakBM = new BreakBM();
            objBreakBM.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objBreakBM.BreakDownNo = sqlDataReader["BreakDownNo"].ToString();
            objBreakBM.TglBreak = Convert.ToDateTime(sqlDataReader["TglBreak"]);
            objBreakBM.OperationalSche = sqlDataReader["OperationalSche"].ToString();
            //objBreakBM.StartBD = sqlDataReader["StartBD"].ToString();
            //objBreakBM.FinishBD = sqlDataReader["FinishBD"].ToString();
            objBreakBM.StartBD = Convert.ToDateTime(sqlDataReader["StartBD"]);
            objBreakBM.FinishBD = Convert.ToDateTime(sqlDataReader["FinishBD"]);
            objBreakBM.BDTime = Convert.ToDateTime(sqlDataReader["BDTime"]);
            //objBreakBM.FinaltyBD = Convert.ToDateTime(sqlDataReader["FinaltyBD"]);
            objBreakBM.FrekBD = Convert.ToInt32(sqlDataReader["FrekBD"]);
            objBreakBM.Syarat = sqlDataReader["Syarat"].ToString();
            objBreakBM.BM_plantGroupID = Convert.ToInt32(sqlDataReader["BM_plantGroupID"]);
            objBreakBM.BM_PlantID = Convert.ToInt32(sqlDataReader["BM_PlantID"]);
            objBreakBM.OperationalTime = sqlDataReader["OperationalTime"].ToString();
            objBreakBM.Ket = sqlDataReader["Ket"].ToString();
            objBreakBM.Pinalti = Convert.ToDecimal(sqlDataReader["Pinalti"]);
            objBreakBM.BreakBM_MasterProblemID = Convert.ToInt32(sqlDataReader["BreakBM_MasterProblemID"]);
            objBreakBM.BreakBM_MasterChargeID = Convert.ToInt32(sqlDataReader["BreakBM_MasterChargeID"]);
            objBreakBM.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objBreakBM.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objBreakBM.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objBreakBM.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objBreakBM.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            objBreakBM.GroupOff = sqlDataReader["GroupOff"].ToString();
            //objBreakBM.MasterPlanID = sqlDataReader["MasterPlanID"].ToString();
            return objBreakBM;
        }

        //public planName GenerateObjectCoba(SqlDataReader sqlDataReader)
        //{
        //    objBreakplt = new planName();
        //    objBreakplt.ID = objBreakplt.BM_PlantID = Convert.ToInt32(sqlDataReader["ID"]);
        //    objBreakplt.PlantName = sqlDataReader["PlantName"].ToString();
        //    objBreakplt.PlantCode = sqlDataReader["PlantCode"].ToString();
        //    objBreakplt.KodeSemen = sqlDataReader["KodeSemen"].ToString();
        //    objBreakplt.KodeKalsium = sqlDataReader["KodeKalsium"].ToString();
        //    return objBreakplt;
        //}


    }
}
