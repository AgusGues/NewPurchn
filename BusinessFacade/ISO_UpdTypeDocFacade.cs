using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using BusinessFacade;
using DataAccessLayer;


namespace BusinessFacade
{
    public class ISO_UpdTypeDocFacade : AbstractFacade       //T3_GroupsFacade : AbstractFacade
    {
        private ISO_UpdTypeDoc objIsoTypeD = new ISO_UpdTypeDoc();
        private ArrayList arrIsoTypeD;
        private List<SqlParameter> sqlListParam;

        public ISO_UpdTypeDocFacade()
            : base()
        {

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
            string strSQL = "select * from ISO_UpdDocType where rowstatus > -1 order by ID";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrIsoTypeD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrIsoTypeD.Add(GenerateObject(sqlDataReader));
                }
            }
           
            return arrIsoTypeD;
        }

        public ArrayList RetrieveProject()
        {
            string strSQL = "select * from ISO_UpdDocType where rowstatus > -1 and ID in (1,5)  order by ID";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrIsoTypeD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrIsoTypeD.Add(GenerateObject(sqlDataReader));
                }
            }

            return arrIsoTypeD;
        }


        public int GetID(string typeD)
        {
            int typeID = 0;
            string strSQL = "select * from ISO_UpdDocType where ID='" + typeD + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrIsoTypeD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    typeID = Convert.ToInt32(sqlDataReader["ID"]);
                }
            }
            return typeID;
        }
        
        public ISO_UpdTypeDoc GenerateObject(SqlDataReader sqlDataReader)
        {

            objIsoTypeD = new ISO_UpdTypeDoc();
            objIsoTypeD.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objIsoTypeD.DocTypeName = (sqlDataReader["DocTypeName"]).ToString();
            return objIsoTypeD;
        }
    }



}

