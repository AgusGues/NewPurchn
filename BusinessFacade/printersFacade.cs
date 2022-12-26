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
    public class printersFacade : AbstractFacade
    {
        private System.Collections.ArrayList arrPrinters;
        private Printers  objPrinters;
        private List<SqlParameter> sqlListParam;

        public int Insertraw(string loc, string paper, int rawkind)
        {
            try
            {
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Location", loc));
                sqlListParam.Add(new SqlParameter("@PaperName", paper));
                sqlListParam.Add(new SqlParameter("@Rawkind", rawkind));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertPrintersRawKind");

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
            throw new NotImplementedException();
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

        public  ArrayList Retrieve(int depoID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from printers where depoid = " + depoID );
            strError = dataAccess.Error;
            arrPrinters = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPrinters.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrPrinters.Add(new Printers());

            return arrPrinters;
        }
        public ArrayList RetrieveByUserID(int depoID, int userID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from printers where depoid = " + depoID + " and UserID=" + userID);
            strError = dataAccess.Error;
            arrPrinters = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrPrinters.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrPrinters.Add(new Printers());

            return arrPrinters;
        }
        public int RetrievebyKind(string Location, string paper)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select isnull(rawkind,0) as rawkind from PrintersRawKind where  Location='" + Location + "' and papername='" + paper + "' ");
            strError = dataAccess.Error;
            arrPrinters = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                sqlDataReader.Read();
                {
                    return Convert.ToInt32(sqlDataReader["rawkind"]);
                }

            }
            else
            {
                return 0;
            }

        }

        public Printers GenerateObject(SqlDataReader sqlDataReader)
        {
            objPrinters = new Printers();
            objPrinters.DepoID = Convert.ToInt32(sqlDataReader["depoID"]);
            objPrinters.Location = sqlDataReader["location"].ToString();
            objPrinters.PrinterName = sqlDataReader["printername"].ToString();
            //objPrinters.UserID = Convert.ToInt32(sqlDataReader["UserID"]);

            return objPrinters;

        }
    }
}
