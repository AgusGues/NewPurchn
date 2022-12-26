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
    public class ISO_UpdMasterDocFacade : AbstractFacade       //T3_GroupsFacade : AbstractFacade
    {
        private ISO_UpdMasterDoc objIsoMasterD = new ISO_UpdMasterDoc();
        private ArrayList arrIsoMasterD;
        private List<SqlParameter> sqlListParam;

        public ISO_UpdMasterDocFacade()
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
            string strSQL = "select * from ISO_UpdDocCategory where type = 1 and rowstatus > -1 order by ID";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrIsoMasterD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrIsoMasterD.Add(GenerateObject(sqlDataReader));
                }
            }
            //else
            //    arrIsoMasterD.Add(new KartuStock());

            return arrIsoMasterD;
        }

        public ArrayList RetrieveProject()
        {
            string strSQL = "select * from ISO_UpdDocCategory where type = 1 and rowstatus > -1 and ID in (10,11) order by ID";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrIsoMasterD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrIsoMasterD.Add(GenerateObject(sqlDataReader));
                }
            }
            //else
            //    arrIsoMasterD.Add(new KartuStock());

            return arrIsoMasterD;
        }

        public ArrayList RetrieveDept()
        {
            //string strSQL = "select Deptid as idDept,namaDept from ISO_UPDDept where RowStatus>-1 order by namadept";
            string strSQL =
            " select * from (select ID idDept , case when ID=2 then 'BM' when ID=3 then 'Finishing' when ID=4 then 'Maintenance' " +
            " when ID=6 then 'Logistik BJ' when ID=7 then 'HRD' when ID=9 then 'QA' when ID=10 then 'Logistik BB' " +
            " when ID=11 then 'PPIC' when ID=13 then 'Marketing' when ID=14 then 'IT' when ID=15 then 'Purchasing' " +
            " when ID=23 then 'ISO' when ID=26 then 'Transportation' when ID=22 then 'Project' " +
            " when ID=24 then 'Accounting' when ID=12 then 'Finance' when ID=38 then 'PM' when ID=28 then 'Product Development' " +
            " when ID=30 then 'Research and Development' end namaDept from (select distinct(deptid) as ID " +
            " from ISO_Dept where RowStatus > -1 and DeptID not in (27,25,5,8,16,18,19,21)) as Dept ) as xx where namaDept  is not null order by namaDept ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrIsoMasterD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrIsoMasterD.Add(GenerateObjectD(sqlDataReader));
                }
            }
            //else
            //    arrIsoMasterD.Add(new KartuStock());

            return arrIsoMasterD;
        }

        public ArrayList RetrieveDeptNew(string DeptID)
        {
            //string strSQL = "select Deptid as idDept,namaDept from ISO_UPDDept where RowStatus>-1 order by namadept";
            string strSQL =
            " select * from (select ID idDept , case when ID=2 then 'BM' when ID=3 then 'Finishing' when ID=4 then 'Maintenance' " +
            " when ID=6 then 'Logistik BJ' when ID=7 then 'HRD' when ID=9 then 'QA' when ID=10 then 'Logistik BB' " +
            " when ID=11 then 'PPIC' when ID=13 then 'Marketing' when ID=14 then 'IT' when ID=15 then 'Purchasing' " +
            " when ID=23 then 'ISO' when ID=26 then 'Transportation' when ID=22 then 'Project' " +
            " when ID=24 then 'Accounting' when ID=12 then 'Finance' when ID=38 then 'PM' when ID=28 then 'Product Development' " +
            " when ID=30 then 'Research and Development' end namaDept from (select distinct(deptid) as ID " +
            " from ISO_Dept where RowStatus > -1 ) as Dept ) as xx where namaDept  is not null and idDept in ("+DeptID+") order by namaDept ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrIsoMasterD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrIsoMasterD.Add(GenerateObjectD(sqlDataReader));
                }
            }
            //else
            //    arrIsoMasterD.Add(new KartuStock());

            return arrIsoMasterD;
        }


        //public override System.Collections.ArrayList Retrieve1()
        public ArrayList Retrieve1()
        {
            string strSQL = "select * from ISO_UpdDocCategory where rowstatus > -1 order by ID";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrIsoMasterD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrIsoMasterD.Add(GenerateObject(sqlDataReader));
                }
            }
            //else
            //    arrIsoMasterD.Add(new KartuStock());

            return arrIsoMasterD;
        }

        public ArrayList GetNamaDept(int DeptID)
        {
            string userDeptID = string.Empty;

            switch (DeptID)
            {                
                case 19:
                    userDeptID = "in (4)";
                    break;
                case 5:
                    userDeptID = "in (4)";
                    break;
                case 18:
                    userDeptID = "in (4)";
                    break;
                default:
                    userDeptID = "in (" + DeptID + ")";
                    break;
            }
            string strSQL = "select * from ISO_UPDDept where RowStatus>-1 and deptID " + userDeptID + "";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrIsoMasterD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrIsoMasterD.Add(GenerateObjectNamaDept(sqlDataReader));
                }
            }          

            return arrIsoMasterD;
        }

        public ArrayList RetrieveJenis()
        {           

            string strSQL = "select id as ID,DocCategory from ISO_UpdDocCategory where RowStatus>-1 order by DocCategory";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrIsoMasterD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrIsoMasterD.Add(GenerateObjectJn(sqlDataReader));
                }
            }
            //else
            //    arrIsoMasterD.Add(new KartuStock());

            return arrIsoMasterD;
        }


        //public ISO_UpdMasterDoc GetNamaDept(int DeptID)
        //{
        //    string strSQL = "select * from ISO_UPDDept where RowStatus>-1 and deptID="+DeptID+"";
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        //    strError = dataAccess.Error;
        //    arrIsoMasterD = new ArrayList();

        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            return GenerateObjectNamaDept(sqlDataReader);
        //        }
        //    }

        //    return new ISO_UpdMasterDoc();
        //}

        public int GetID(string masterD)
        {
            int masterID = 0;
            string strSQL = "select * from ISO_UpdDocCategory where ID='" + masterD + "'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrIsoMasterD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    masterID = Convert.ToInt32(sqlDataReader["ID"]);
                }
            }

            return masterID;
        }

        public ArrayList RetrieveDeptUPD()
        {
            //string strSQL = "select deptID idDept,namaDept from ISO_UPDDept where RowStatus>-1";
            string strSQL =
                //"select ID idDept , case when ID=2 then 'BM' when ID=3 then 'Finishing' when ID=4 then 'Maintenance' " +
                //" when ID=6 then 'Logistik BJ' when ID=7 then 'HRD' when ID=9 then 'QA' when ID=10 then 'Logistik BB' "+
                //" when ID=11 then 'PPIC' when ID=13 then 'Marketing' when ID=14 then 'IT' when ID=15 then 'Purchasing' "+
                //" when ID=23 then 'ISO' when ID=26 then 'Transportation' when ID=22 then 'Project' "+
                //" when ID=24 then 'Accounting' when ID=12 then 'Finance' when ID=38 then 'PM' when ID=28 then 'Product Development' "+
                //" when ID=31 then 'Research and Development' end namaDept from (select distinct(deptid) as ID " +
                //" from ISO_Dept where RowStatus > -1 and DeptID not in (27,25,5,8,16,18,19,21)) as Dept order by namaDept";
            " select xx.DeptID idDept, A.Alias namaDept from (select distinct(DeptID)DeptID from ISO_Dept where RowStatus>-1 ) as xx "+
            " inner join Dept A ON A.ID=xx.DeptID where A.RowStatus>-1 and xx.DeptID not in (27,25,5,8,16,18,19,21) order by namaDept ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrIsoMasterD = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrIsoMasterD.Add(GenerateObjectDept(sqlDataReader));
                }
            }

            return arrIsoMasterD;
        }

        //public ISO_UpdMasterDoc RetrieveByItem(string partno)
        //{
        //    string strSQL = "SELECT * from ISO_UpdMasterDoc where id in (select groupid from fc_items where partno='" + partno + "')";
        //    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        //    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
        //    strError = dataAccess.Error;
        //    if (sqlDataReader.HasRows)
        //    {
        //        while (sqlDataReader.Read())
        //        {
        //            return GenerateObject(sqlDataReader);
        //        }
        //    }
        //    return new T3_Groups();
        //}
        public ISO_UpdMasterDoc GenerateObject(SqlDataReader sqlDataReader)
        {
            objIsoMasterD = new ISO_UpdMasterDoc();
            objIsoMasterD.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objIsoMasterD.DocCategory = (sqlDataReader["DocCategory"]).ToString();
            return objIsoMasterD;
        }
        public ISO_UpdMasterDoc GenerateObjectD(SqlDataReader sqlDataReader)
        {
            objIsoMasterD = new ISO_UpdMasterDoc();
            objIsoMasterD.idDept = Convert.ToInt32(sqlDataReader["idDept"]);
            objIsoMasterD.namaDept = (sqlDataReader["namaDept"]).ToString();
            return objIsoMasterD;
        }
        public ISO_UpdMasterDoc GenerateObjectNamaDept(SqlDataReader sqlDataReader)
        {
            objIsoMasterD = new ISO_UpdMasterDoc();
            objIsoMasterD.idDept = Convert.ToInt32(sqlDataReader["Deptid"]);
            objIsoMasterD.namaDept = (sqlDataReader["namaDept"]).ToString();
            return objIsoMasterD;
        }
        public ISO_UpdMasterDoc GenerateObjectJn(SqlDataReader sqlDataReader)
        {
            objIsoMasterD = new ISO_UpdMasterDoc();
            objIsoMasterD.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objIsoMasterD.DocCategory = (sqlDataReader["DocCategory"]).ToString();
            return objIsoMasterD;
        }
        public ISO_UpdMasterDoc GenerateObjectDept(SqlDataReader sqlDataReader)
        {
            objIsoMasterD = new ISO_UpdMasterDoc();
            objIsoMasterD.idDept = Convert.ToInt32(sqlDataReader["idDept"]);
            objIsoMasterD.namaDept = (sqlDataReader["namaDept"]).ToString();            
            return objIsoMasterD;
        }

        public ISO_UpdMasterDoc RetrieveDataDept(string ID)
        {
            string StrSql =

            " select namaDept,DeptID idDept from ISO_UPDDept where RowStatus>-1 and deptID in (select ID from Dept where ID in (" + ID + ") and RowStatus>-1) ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject_RetrieveDataDept(sqlDataReader);
                }
            }

            return new ISO_UpdMasterDoc();
        }

        public ISO_UpdMasterDoc GenerateObject_RetrieveDataDept(SqlDataReader sqlDataReader)
        {
            objIsoMasterD = new ISO_UpdMasterDoc();
            objIsoMasterD.idDept = Convert.ToInt32(sqlDataReader["idDept"]);
            objIsoMasterD.namaDept = (sqlDataReader["namaDept"]).ToString();   
            return objIsoMasterD;
        }

        public ISO_UpdMasterDoc RetrieveDataListReport(int ID)
        {
            string StrSql =

            " select DeptID DeptIDstring,UserID ID from ISO_UpdListReport where UserID=" + ID + " and RowStatus>-1 ";
       

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject_RetrieveDataListReport(sqlDataReader);
                }
            }

            return new ISO_UpdMasterDoc();
        }

        public ISO_UpdMasterDoc GenerateObject_RetrieveDataListReport(SqlDataReader sqlDataReader)
        {
            objIsoMasterD = new ISO_UpdMasterDoc();
            objIsoMasterD.DeptIDstring = sqlDataReader["DeptIDstring"].ToString();
            objIsoMasterD.ID = Convert.ToInt32(sqlDataReader["ID"]);
            return objIsoMasterD;
        }

    }



}

