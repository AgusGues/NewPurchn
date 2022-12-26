using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using BusinessFacade;
using Domain;
using DataAccessLayer;

namespace Cogs
{
    public class COGSFabrikasiFacade :AbstractFacade3
    {
        private Fabrikasi objCogs = new Fabrikasi();
        private ArrayList arrCogs = new ArrayList();
        private List<SqlParameter> sqlListParam;
        private DataAccess dataAccess = new DataAccess(Global.ConnectionString());

        public COGSFabrikasiFacade(): base()
        {

        }

        public BusinessFacade.Global Global
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    
        public override int Insert(object objDomain)
        {
            try
            {
                int result=0;
                objCogs = (Fabrikasi)objDomain;
                sqlListParam=new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Tahun", objCogs.Tahun));
                sqlListParam.Add(new SqlParameter("@Bulan", objCogs.Bulan));
                sqlListParam.Add(new SqlParameter("@Jumlah", objCogs.Jumlah));
                sqlListParam.Add(new SqlParameter("@Createdby", objCogs.CreatedBy));

                result = dataAccess.ProcessData(sqlListParam,"spCOGSInsertFabrikasi");
                strError = dataAccess.Error;
                return result;
            }catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }

        }

        public override int Update(object objDomain)
        {
            try
            {
                int result = 0;
                objCogs = (Fabrikasi)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objCogs.ID));
                sqlListParam.Add(new SqlParameter("@RowStatus", objCogs.RowStatus));
                sqlListParam.Add(new SqlParameter("@Jumlah", objCogs.Jumlah));
                sqlListParam.Add(new SqlParameter("@LastModifiedby", objCogs.LastModifiedBy));

                result = dataAccess.ProcessData(sqlListParam, "spCOGSUpdateFabrikasi");
                strError = dataAccess.Error;
                return result;
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
                int result = 0;
                objCogs = (Fabrikasi)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objCogs.ID));
                sqlListParam.Add(new SqlParameter("@RowStatus", objCogs.RowStatus));
                sqlListParam.Add(new SqlParameter("@LastModifiedby", objCogs.LastModifiedBy));

                result = dataAccess.ProcessData(sqlListParam, "spCOGSDeleteFabrikasi");
                strError = dataAccess.Error;
                return result;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public override ArrayList Retrieve()
        {
            string strSQL = "Select ID,Tahun,Bulan,Jumlah,RowStatus from COGS_Fabrikasi where RowStatus >-1 order by ID desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrCogs = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrCogs.Add(GeneratDataObject(sqlDataReader));
                }
            }else{
                arrCogs.Add(new Fabrikasi());
            }
            return arrCogs;

        }
        public ArrayList GetTahun()
        {
            string strSQL = "Select isnull(Year(tglProduksi),0) as Tahun from BM_Destacking group by Year(tglProduksi) order by Year(tglProduksi) ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrCogs = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrCogs.Add(new Fabrikasi { Tahun = Convert.ToInt32(sqlDataReader["Tahun"].ToString()) });
                }
            }
           
            return arrCogs;
        }
        public ArrayList RetrieveByTahun(int Tahun)
        {
            string strSQL = "Select * from COGS_Fabrikasi where Tahun="+Tahun+" and RowStatus >-1 order by Bulan";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrCogs = new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrCogs.Add(GeneratDataObject(sqlDataReader));
                }
            }
            
            return arrCogs;
        }
        public Fabrikasi RetrieveByID(int ID)
        {
            string strSQL="SELECT * FROM COGS_Fabrikasi where ID="+ID;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrCogs=new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GeneratDataObject(sqlDataReader);
                }
            }
            return new Fabrikasi();
        }
        public Fabrikasi RetrievebyBulan(int Tahun,int Bulan)
        {
            string strSQL="SELECT * FROM COGS_Fabrikasi where Tahun="+Tahun+" and Bulan="+Bulan+" and RowStatus >-1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrCogs=new ArrayList();
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GeneratDataObject(sqlDataReader);
                }
            }
            return new Fabrikasi();
        }
        public Fabrikasi GetDataTahun(SqlDataReader sqlDataReader)
        {
            objCogs = new Fabrikasi();
            objCogs.ID = int.Parse(sqlDataReader["Tahun"].ToString());
            return objCogs;
        }
        public Fabrikasi GeneratDataObject(SqlDataReader sqlDataReader)
        {
            objCogs         = new Fabrikasi();
            objCogs.ID      =int.Parse(sqlDataReader["ID"].ToString());
            objCogs.Tahun   =int.Parse(sqlDataReader["Tahun"].ToString());
            objCogs.Bulan   = int.Parse(sqlDataReader["Bulan"].ToString());
            objCogs.Jumlah  = decimal.Parse(sqlDataReader["Jumlah"].ToString());
            objCogs.RowStatus = int.Parse(sqlDataReader["RowStatus"].ToString());
            return objCogs;
        }
        /**
         * Biaya Bahan Baku dan BP
         */
    }
}
