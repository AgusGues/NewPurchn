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
    public class PakaiFormulaFacade : AbstractFacade
    {
        private PakaiFormula  objPakaiFormula = new PakaiFormula();
        //private ArrayList arrPakaiFormula;
        private List<SqlParameter> sqlListParam;


        public PakaiFormulaFacade()
            : base()
        {

        }


        public override int Insert(object objDomain)
        {
            try
            {
                objPakaiFormula = (PakaiFormula)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@PakaiID", objPakaiFormula.PakaiID ));
                sqlListParam.Add(new SqlParameter("@FormulaID", objPakaiFormula.FormulaID ));
                sqlListParam.Add(new SqlParameter("@PlantID", objPakaiFormula.PlantID ));
                sqlListParam.Add(new SqlParameter("@TglProduksi", objPakaiFormula.TglProduksi ));
                sqlListParam.Add(new SqlParameter("@JmlMix", objPakaiFormula.JmlMix ));
                sqlListParam.Add(new SqlParameter("@Keterangan", objPakaiFormula.Keterangan ));
                sqlListParam.Add(new SqlParameter("@PlantGroupID", objPakaiFormula.PlantGroupID ));
                
                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertPakaiFormula");
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
            throw new NotImplementedException();
        }

        public override System.Collections.ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }
    }
}
