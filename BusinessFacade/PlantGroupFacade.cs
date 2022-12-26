using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using Domain;
using DataAccessLayer;
using Dapper;

namespace BusinessFacade
{
    public class PlantGroupFacade : AbstractFacade
    {
        private PlantGroup objGroupPlant = new PlantGroup();
        private ArrayList arrGroupPlant;
        private List<SqlParameter> sqlListParam;

        public PlantGroupFacade()
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
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from bm_plantGroup order by [group]");
            strError = dataAccess.Error;
            arrGroupPlant = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGroupPlant.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGroupPlant.Add(new Asset());

            return arrGroupPlant;
        }
        public ArrayList Retrieve1(int ID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from bm_plantGroup where plantID="+ ID +" order by ID ");
            strError = dataAccess.Error;
            arrGroupPlant = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrGroupPlant.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrGroupPlant.Add(new Asset());

            return arrGroupPlant;
        }
        public int GetID(string group)
        {
            int ID = 0;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from bm_plantGroup where [group]='" + group + "'");
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    ID = Convert.ToInt32(sqlDataReader["ID"]);
                }
            }

            return ID;
        }
        public PlantGroup GenerateObject(SqlDataReader sqlDataReader)
        {
            objGroupPlant = new PlantGroup();
            objGroupPlant.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objGroupPlant.PlantID = Convert.ToInt32(sqlDataReader["PlantID"]);
            objGroupPlant.Group = sqlDataReader["Group"].ToString();

            return objGroupPlant;
        }

        public static List<PlantGroup.BM_PlantGroup> GetListPlantGroup()
        {
            List<PlantGroup.BM_PlantGroup> AllData = null;
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string query = "SELECT * FROM BM_PLANTGROUP ORDER BY [GROUP]";
                    AllData = connection.Query<PlantGroup.BM_PlantGroup>(query).ToList();
                }
                catch (Exception e)
                {
                    AllData = null;
                }
            }
            return AllData;
        }
    }
}
