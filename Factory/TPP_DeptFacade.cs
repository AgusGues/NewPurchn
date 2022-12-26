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
    public class TPP_DeptFacade : BusinessFacade.AbstractTransactionFacadeF
    {
        private TPP_Dept objDept = new TPP_Dept();
        private ArrayList arrDept;
        private List<SqlParameter> sqlListParam;
        public override int Insert(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Update(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Insert1(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Update1(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Update2(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }

        public override int Delete(DataAccessLayer.TransactionManager transManager)
        {
            throw new NotImplementedException();
        }
              
        public override ArrayList Retrieve()
        {
            string strSQL = "select * from tpp_Dept as A where A.RowStatus >-1   order by A.Departemen";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrDept = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDept.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrDept.Add(new Dept());

            return arrDept;
        }
        public  ArrayList RetrieveMasalah()
        {
            string strSQL = "select A.ID,A.Asal_Masalah departemen,A.kode from TPP_Asal_Masalah as A where A.RowStatus >-1   order by A.Asal_Masalah";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrDept = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrDept.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrDept.Add(new Dept());

            return arrDept;
        }
        public TPP_Dept GetUserDept(int userid)
        {
            string strSQL = "select A.*  from TPP_Dept  A inner join TPP_Users B on A.ID=B.Dept_ID  where A.rowstatus>-1 and  B.rowstatus>-1 and B.User_ID=" + userid;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }
            return new TPP_Dept();
        }


        private TPP_Dept GenerateObject(SqlDataReader sqlDataReader)
        {
            objDept = new TPP_Dept();
            objDept.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objDept.Kode = sqlDataReader["Kode"].ToString();
            objDept.Departemen = sqlDataReader["Departemen"].ToString();
            return objDept;
        }


        public ArrayList RetrieveData_TPP(string A, string tgl1, string tgl2)
        {
            arrDept = new ArrayList();

            string strSQL =
            " select A.ID,A.Laporan_No,left(convert(char,A.CreatedTime,106),11)CreatedTime,left(convert(char,A.TPP_Date,106),11)TPP_Date,B.Departemen, " +
            " isnull((select username from Users where ID in (select top 1 mgrid from TPP_ListApproval where UserID=A.User_ID )),'') PIC, " +
            " A.Keterangan Keterangan,A.Uraian ,A.Ketidaksesuaian,case when ISNULL(A.Closed,0)=0 then 'Open' else 'CLosed' end Status   " +
            " from tpp A inner join TPP_Dept B on A.Dept_ID=B.ID where convert(char,A.tpp_date,112)>='" + tgl1 + "' " +
            " and convert(char,A.tpp_date,112)<='" + tgl2 + "' " +
            " and A.RowStatus>-1 " + A + " order by B.Departemen,A.TPP_Date ";

            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);


            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrDept.Add(new TPP_Dept
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        Laporan_No = sdr["Laporan_No"].ToString(),
                        CreatedTime = sdr["CreatedTime"].ToString(),
                        TPP_Date = sdr["TPP_Date"].ToString(),
                        Departemen = sdr["Departemen"].ToString(),
                        PIC = sdr["PIC"].ToString(),
                        Keterangan = sdr["Keterangan"].ToString(),
                        Uraian = sdr["Uraian"].ToString(),
                        Ketidaksesuaian = sdr["Ketidaksesuaian"].ToString(),
                        Status = sdr["Status"].ToString()
                    });
                }
            }
            return arrDept;
        }

    }
}
