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
    public class StokerFacade : AbstractFacade
    {
        private Stoker objStoker = new Stoker();       
        private ArrayList arrStoker;      
        private List<SqlParameter> sqlListParam; 

        public StokerFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objStoker = (Stoker)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Nama", objStoker.Nama));
                sqlListParam.Add(new SqlParameter("@DeptID", objStoker.DeptID));            

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertStoker");

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
                objStoker = (Stoker)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objStoker.ID));
                sqlListParam.Add(new SqlParameter("@Nama", objStoker.Nama));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objStoker.LastModifiedBy));              
                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateStoker");

                strError = dataAccess.Error;

                return intResult;

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
                objStoker = (Stoker)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objStoker.ID));                

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteStoker");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int insertMasterLink(object objDomain)
        {
            try
            {
                objStoker = (Stoker)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Nama", objStoker.Nama));
                sqlListParam.Add(new SqlParameter("@ID", objStoker.ID));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertMasterLink");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int insertMasterLinkNew(object objDomain)
        {
            try
            {
                objStoker = (Stoker)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Nama", objStoker.Nama));
                sqlListParam.Add(new SqlParameter("@Partno", objStoker.Partno));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertMasterLink");

                strError = dataAccess.Error;

                return intResult;

            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int UpdateMasterLink(object objDomain)
        {
            try
            {
                objStoker = (Stoker)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Nama", objStoker.Nama));
                sqlListParam.Add(new SqlParameter("@Partno", objStoker.Partno));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateMasterLinkNew");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select ID,Nama from masterstoker where rowstatus > -1 order by id desc");
            strError = dataAccess.Error;
            arrStoker = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrStoker.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrStoker.Add(new Stoker());

            return arrStoker;
        }


        public ArrayList RetrieveByStoker(int DeptID)
        {
            string query = string.Empty;
            if (DeptID == 24)
            {
                query = "";
            }
            else
            {
                query = " and deptid=" + DeptID + " ";
            }

            string strSQL = "select * from masterstoker where rowstatus > -1 "+query+" order by Nama desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrStoker = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrStoker.Add(GenerateObject2(sqlDataReader));
                }
            }


            return arrStoker;
        }
        public string retriveDeptbyStocker(string stocker)
        {
            string strSQL = "select deptid from MasterStoker where Nama='" + stocker + "' and RowStatus>-1";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            string dept = string.Empty;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    dept= sqlDataReader["deptid"].ToString();
                }
            }
            return dept;
        }

        public ArrayList RetrieveStoker2Grid(string PartNo)
        {            
            string strSQL = " select top 1 A.ID,C.PartNo,'-'Lokasi,B.Nama from MasterStokerLink A "+
                            " INNER JOIN MasterStoker B  ON A.StokerID=B.ID "+
                            " INNER JOIN FC_Items C ON A.ItemID=C.ID "+
                            " where C.PartNo='"+ PartNo +"' and B.RowStatus>-1 and A.RowStatus>-1  order by A.ID desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrStoker = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrStoker.Add(GenerateObject4(sqlDataReader));
                }
            }
            return arrStoker;
        }

        public ArrayList RetrieveNoStokerGrid()
        {
            string strSQL =
            " with DataPartno as (select ItemID,Qty,LokID from T3_Serah where RowStatus>-1 and Qty>0 and ItemID>0 and LokID not in " +
            " (select ID from FC_Lokasi where Lokasi in ('BSAUTO','S99'))), " +
            " DataStokerLink as (select distinct(ItemID),StokerID from MasterStokerLink where RowStatus>-1), " +
            " DataA as (select A.ItemID,A.LokID,A.Qty,isnull(B.StokerID,'')StokerID from DataPartno A LEFT JOIN DataStokerLink B ON A.ItemID=B.ItemID), " +
            " DataB as (select B.PartNo,A.LokID,A.Qty,A.StokerID,B.Dept from  DataA A LEFT join fc_items B ON A.ItemID=B.ID ), " +
            " DataFinal as (select A.PartNo,B.Lokasi,A.Qty,isnull(C.Nama,'???')Nama,isnull(A.Dept,'-')NamaDept " +
            " from DataB A LEFT JOIN FC_Lokasi B ON A.LokID=B.ID LEFT JOIN MasterStoker C ON C.ID=A.StokerID) " +
            " select Nama,Lokasi,PartNo,Qty from DataFinal where Nama='???' and Lokasi not like'%KAT%' order by Nama,PartNo,Lokasi ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrStoker = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrStoker.Add(GenerateObjectNoStoker(sqlDataReader));
                }
            }
            return arrStoker;
        }

        public ArrayList RetrieveStoker3Grid(int DeptID)
        {            
            string Query = "";
            switch (DeptID)
            {
                case 6: Query = " select '0'ID,FC.PartNo,'-'Lokasi,'-'Nama from (select ItemID from T3_Serah where ItemID not in " +
                                " (select ItemID from MasterStokerLink where RowStatus > -1) and LokID not in (select LokID from FC_LokasiDept " +
                                " where RowStatus > -1) and Qty > 0 and RowStatus > -1) as data1 INNER JOIN FC_Items FC ON FC.ID=data1.ItemID ";
                    break;

                case 3: Query = " select '0'ID,FC.PartNo,FL.Lokasi,'-'Nama from (select ItemID,LokID from T3_Serah where LokID not in "+
                                " (select LokID from MasterStokerLink where RowStatus > -1) and LokID  in (select LokID from FC_LokasiDept "+
                                " where RowStatus > -1) and Qty > 0 and RowStatus > -1) as data1 "+
                                " INNER JOIN FC_Items FC ON FC.ID=data1.ItemID "+
                                " INNER JOIN FC_Lokasi FL ON FL.ID=data1.LokID where FC.PartNo not like'%-S-%' order by Lokasi";
                    break;
            }


            string strSQL =""+Query+"";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrStoker = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrStoker.Add(GenerateObject4(sqlDataReader));
                }
            }
            return arrStoker;
        }


        public ArrayList RetrieveByListStoker()
        {
            string strSQL = "select A.ID as IDserah,(select PartNo from FC_Items where ID=A.ItemID) as Partno, " +
                            "(select Lokasi from FC_Lokasi where ID=A.LokID) as Lokasi, " +
                            "(select Nama from MasterStoker where ID=B.StokerID and RowStatus > -1) as Nama  " +
                            "from t3_serah as A INNER JOIN MasterStokerLink as B ON A.ID=B.IDserah " +
                            "where A.RowStatus > -1 order by Nama,Lokasi ";

            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrStoker = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrStoker.Add(GenerateObject3(sqlDataReader));
                }
            }


            return arrStoker;
        }

        public ArrayList RetrieveDDLstoker()
        {
            string strSQL = "select * from masterstoker where rowstatus > -1 order by nama ";
                           
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrStoker = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrStoker.Add(GenerateObject2(sqlDataReader));
                }
            }


            return arrStoker;
        }

        public Stoker CekDataStoker(string Nama)
        {

            string StrSql = " select StokerID ID from MasterStokerLink where RowStatus > -1 and StokerID in (select ID from MasterStoker where Nama='"+Nama+"' and RowStatus > -1)";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject1(sqlDataReader);
                }
            }

            return new Stoker();
        }

        public Stoker GenerateObject1(SqlDataReader sqlDataReader)
        {
            objStoker = new Stoker();
            objStoker.ID = Convert.ToInt32(sqlDataReader["ID"]);
            //objStoker.Nama = sqlDataReader["Nama"].ToString();
            //objStoker.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            return objStoker;
        }

        public Stoker CekPartno(string Partno)
        {

            string StrSql = " select top 1 ItemID ID from MasterStokerLink where ItemID in (select ID from FC_Items where PartNo='"+Partno+"') and RowStatus > -1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject5(sqlDataReader);
                }
            }

            return new Stoker();
        }

        public Stoker cekstoker(string stoker)
        {

            string StrSql = "select ID,Nama from MasterStoker where RowStatus > -1 and Nama='"+stoker+"'";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject55(sqlDataReader);
                }
            }

            return new Stoker();
        }

        public Stoker GenerateObject55(SqlDataReader sqlDataReader)
        {
            objStoker = new Stoker();
            objStoker.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objStoker.Nama = sqlDataReader["Nama"].ToString();
            //objStoker.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            return objStoker;
        }


        public Stoker GenerateObject5(SqlDataReader sqlDataReader)
        {
            objStoker = new Stoker();
            objStoker.ID = Convert.ToInt32(sqlDataReader["ID"]);
            //objStoker.Nama = sqlDataReader["Nama"].ToString();
            //objStoker.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            return objStoker;
        }


        public Stoker GenerateObject(SqlDataReader sqlDataReader)
        {
            objStoker = new Stoker();
            objStoker.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objStoker.Nama = sqlDataReader["Nama"].ToString();            
            objStoker.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);            
            return objStoker;
        }

        public Stoker GenerateObject2(SqlDataReader sqlDataReader)
        {
            objStoker = new Stoker();
            objStoker.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objStoker.Nama = sqlDataReader["Nama"].ToString();
            return objStoker;
        }

        public Stoker GenerateObject3(SqlDataReader sqlDataReader)
        {
            objStoker = new Stoker();
            objStoker.IDserah = Convert.ToInt32(sqlDataReader["IDserah"]);
            objStoker.Nama = sqlDataReader["Nama"].ToString();
            objStoker.Lokasi = sqlDataReader["Lokasi"].ToString();
            objStoker.Partno = sqlDataReader["Partno"].ToString();
            return objStoker;
        }

        public Stoker GenerateObject4(SqlDataReader sqlDataReader)
        {
            objStoker = new Stoker();
            objStoker.ID = Convert.ToInt32(sqlDataReader["ID"]);            
            objStoker.Nama = sqlDataReader["Nama"].ToString();
            objStoker.Lokasi = sqlDataReader["Lokasi"].ToString();
            objStoker.Partno = sqlDataReader["Partno"].ToString();
            return objStoker;
        }

        public Stoker GenerateObjectNoStoker(SqlDataReader sqlDataReader)
        {
            objStoker = new Stoker();
            //objStoker.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objStoker.Nama = sqlDataReader["Nama"].ToString();
            objStoker.Lokasi = sqlDataReader["Lokasi"].ToString();
            objStoker.Partno = sqlDataReader["Partno"].ToString();
            objStoker.Qty = Convert.ToDecimal(sqlDataReader["Qty"]);
            return objStoker;
        }

        public int CekNamaStoker(string Nama, int DeptID)
        {
            string StrSql = " select ID from MasterStoker where Nama='"+Nama+"' and DeptID="+DeptID+" and RowStatus>-1 ";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["ID"]);
                }
            }
            return 0;
        }

    }
}
