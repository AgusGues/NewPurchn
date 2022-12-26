using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using Domain;
using DataAccessLayer;

namespace BusinessFacade
{
    public class MataUangFacade : AbstractFacade
    {
        private MataUang objMataUang = new MataUang();
        private ArrayList arrMataUang;
        private List<SqlParameter> sqlListParam;


        public MataUangFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objMataUang = (MataUang)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Lambang", objMataUang.Lambang));
                sqlListParam.Add(new SqlParameter("@Nama", objMataUang.Nama));
                               

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertMataUang");

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
                objMataUang = (MataUang)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Lambang", objMataUang.Lambang));
                sqlListParam.Add(new SqlParameter("@Nama", objMataUang.Nama));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateMataUang");

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
                objMataUang = (MataUang)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objMataUang.ID));
                //sqlListParam.Add(new SqlParameter("@LastModifiedBy", objGroups.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteMataUang");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.Nama,A.Lambang,A.Twoisoletter from MataUang as A");
            strError = dataAccess.Error;
            arrMataUang = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrMataUang.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrMataUang.Add(new MataUang());

            return arrMataUang;
        }

        public MataUang RetrieveById(int Id)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.Nama,A.Lambang,A.Twoisoletter from MataUang as A where A.ID = " + Id);
            strError = dataAccess.Error;
            arrMataUang = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new MataUang();
        }

        public MataUang Retrieveby2ISO(string striso)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.Nama,A.Lambang,A.Twoisoletter from MataUang as A where A.twoisoletter ='" + striso + "'");
            strError = dataAccess.Error;
            arrMataUang = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new MataUang();
        }
        public MataUang GenerateObject(SqlDataReader sqlDataReader)
        {
            objMataUang = new MataUang();
            objMataUang.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objMataUang.Nama = sqlDataReader["Nama"].ToString();
            objMataUang.Lambang = sqlDataReader["Lambang"].ToString();
            objMataUang.Twoiso  = sqlDataReader["Twoisoletter"].ToString();
            return objMataUang;
        }
        private string Criteria()
        {
            string strQuery = (HttpContext.Current.Session["criteria"] != null) ? HttpContext.Current.Session["criteria"].ToString() : string.Empty;
            return strQuery;
        }
        public ArrayList RetrieveList()
        {
            string strSQL = "select top 100 drTgl  as Tanggal,Max(sdTgl) as sdTanggal,MAX(USD)USD,MAX(SGD)SGD,MAX(JPY)JPY,MAX(EUR)EUR from( " +
                            "select drTgl,sdTgl,isnull([USD],0)USD,isnull([SGD],0)SGD,isnull([JPY],0)JPY,isnull([EUR],0)EUR from( " +
                            "select m.IDR,m.Code,k.Kurs,k.drTgl,k.sdTgl,m.Nama from MataUangKurs as k " +
                            "left join MataUang as m " +
                            "on m.ID=k.MUID " +
                            "where k.rowstatus>-1 and TypeKurs=1  " +
                            ") as p  " +
                            "pivot (max(kurs) for code in(USD,[JPY],[SGD],[EUR])) " +
                            "as pvt " +
                            ") as d " + this.Criteria() +
                            "group by d.drTgl " +
                            "order by d.drTgl desc";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            arrMataUang = new ArrayList();
            if (dataAccess.Error == string.Empty && sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrMataUang.Add(new ListKurs
                    {
                        Tanggal = Convert.ToDateTime(sqlDataReader["Tanggal"].ToString()),
                        USD = Convert.ToDecimal(sqlDataReader["USD"].ToString()),
                        SGD = Convert.ToDecimal(sqlDataReader["SGD"].ToString()),
                        JPY = Convert.ToDecimal(sqlDataReader["JPY"].ToString()),
                        EUR = Convert.ToDecimal(sqlDataReader["EUR"].ToString()),
                        sdTanggal=Convert.ToDateTime(sqlDataReader["sdTanggal"].ToString()),
                        Periode=Convert.ToDateTime(sqlDataReader["Tanggal"].ToString()).ToShortDateString().ToString()+" - "+
                                Convert.ToDateTime(sqlDataReader["sdTanggal"].ToString()).ToShortDateString().ToString()
                    });
                }
            }
            return arrMataUang;
        }
    }
}
