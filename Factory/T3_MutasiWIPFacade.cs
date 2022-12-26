using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;
using BusinessFacade;

namespace Factory
{
   public class T3_MutasiWIPFacade:AbstractFacade
   {
       private T3_MutasiWIP objT3_MutasiWIP = new T3_MutasiWIP();
       private ArrayList arrT3_MutasiWIP;
       private List<SqlParameter> sqlListParam;

       public T3_MutasiWIPFacade()
           :base()
       {

       }
       public override int Insert(object objDomain)
       {
           throw new NotImplementedException();
       }

       public override int Delete(object objDomain)
       {
           throw new NotImplementedException();
       }
       public override int Update(object objDomain)
       {
           throw new NotImplementedException();
       }
       public override ArrayList Retrieve()
       {
           throw new NotImplementedException();
       }

       public ArrayList BM_Tahun()
        {               
            /**
             * Added on : 09-10-2013
             * Author   : Zetrosoft
             * Last Upd : 09-10-2013
             * Remark   : Get year for dropdown
             */
            string strSQL = "select YEAR(tglproduksi)as Tahun from BM_Destacking group by YEAR(tglproduksi) union select YEAR(tglserah)as Tahun from t1_serah group by YEAR(tglserah)";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrT3_MutasiWIP = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrT3_MutasiWIP.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrT3_MutasiWIP.Add(new T3_MutasiWIP());

            return arrT3_MutasiWIP;
        }

        public T3_MutasiWIP GenerateObject(SqlDataReader sqlDataReader)
        {
            try
            {
                objT3_MutasiWIP = new T3_MutasiWIP();
                objT3_MutasiWIP.Tahune = Convert.ToInt32(sqlDataReader["Tahun"]);
            }
            catch
            { }
            return objT3_MutasiWIP;
        }
}
}
