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
    public class HelpDeskKeluhanFacade : AbstractFacade
    {
        private HelpDeskKeluhan objHelpDeskKeluhan = new HelpDeskKeluhan();
        private Dept objDept = new Dept();
        private HelpDeskCategory objHelpDeskCategory = new HelpDeskCategory();
        private ArrayList arrHelpDeskKeluhan;
        private List<SqlParameter> sqlListParam;

        public HelpDeskKeluhanFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objHelpDeskKeluhan = (HelpDeskKeluhan)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@HelpTgl", objHelpDeskKeluhan.HelpTgl));
                sqlListParam.Add(new SqlParameter("@HelpDeskNo", objHelpDeskKeluhan.HelpDeskNo));
                sqlListParam.Add(new SqlParameter("@DeptID", objHelpDeskKeluhan.DeptID));
                sqlListParam.Add(new SqlParameter("@DeptName", objHelpDeskKeluhan.DeptName));
                sqlListParam.Add(new SqlParameter("@Status", objHelpDeskKeluhan.Status));
                sqlListParam.Add(new SqlParameter("@HelpDeskCategoryID", objHelpDeskKeluhan.HelpDeskCategoryID));
                sqlListParam.Add(new SqlParameter("@PIC", objHelpDeskKeluhan.PIC));
                sqlListParam.Add(new SqlParameter("@Keluhan", objHelpDeskKeluhan.Keluhan));
                sqlListParam.Add(new SqlParameter("@Analisa", objHelpDeskKeluhan.Analisa));
                sqlListParam.Add(new SqlParameter("@KategoriPenyelesaianID", objHelpDeskKeluhan.KategoriPenyelesaianID));
                sqlListParam.Add(new SqlParameter("@Perbaikan", objHelpDeskKeluhan.Perbaikan));
                sqlListParam.Add(new SqlParameter("@TglPerbaikan", objHelpDeskKeluhan.TglPerbaikan));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objHelpDeskKeluhan.CreatedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertHelpDeskKeluhan");

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
                objHelpDeskKeluhan = (HelpDeskKeluhan)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objHelpDeskKeluhan.ID));
                sqlListParam.Add(new SqlParameter("@HelpTgl", objHelpDeskKeluhan.HelpTgl));
                sqlListParam.Add(new SqlParameter("@HelpDeskNo", objHelpDeskKeluhan.HelpDeskNo));
                sqlListParam.Add(new SqlParameter("@DeptID", objHelpDeskKeluhan.DeptID));
                sqlListParam.Add(new SqlParameter("@DeptName", objHelpDeskKeluhan.DeptName));
                sqlListParam.Add(new SqlParameter("@Status", objHelpDeskKeluhan.Status));
                sqlListParam.Add(new SqlParameter("@HelpDeskCategoryID", objHelpDeskKeluhan.HelpDeskCategoryID));
                sqlListParam.Add(new SqlParameter("@PIC", objHelpDeskKeluhan.PIC));
                sqlListParam.Add(new SqlParameter("@Keluhan", objHelpDeskKeluhan.Keluhan));
                sqlListParam.Add(new SqlParameter("@Analisa", objHelpDeskKeluhan.Analisa));
                sqlListParam.Add(new SqlParameter("@KategoriPenyelesaianID", objHelpDeskKeluhan.KategoriPenyelesaianID));
                sqlListParam.Add(new SqlParameter("@Perbaikan", objHelpDeskKeluhan.Perbaikan));
                sqlListParam.Add(new SqlParameter("@TglPerbaikan", objHelpDeskKeluhan.TglPerbaikan));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objHelpDeskKeluhan.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateHelpDeskKeluhan");

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
                objHelpDeskKeluhan = (HelpDeskKeluhan)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objHelpDeskKeluhan.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objHelpDeskKeluhan.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteHelpDeskKeluhan");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from HelpDeskKeluhan where RowStatus = 0 order by id Desc");
            strError = dataAccess.Error;
            arrHelpDeskKeluhan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrHelpDeskKeluhan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrHelpDeskKeluhan.Add(new HelpDeskKeluhan());

            return arrHelpDeskKeluhan;
        }

        public  ArrayList RetrieveByUser(string createdby)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from HelpDeskKeluhan where RowStatus = 0 and HelpDeskKeluhan.CreatedBy= '" + createdby + "' order by ID Desc" );
            strError = dataAccess.Error;
            arrHelpDeskKeluhan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrHelpDeskKeluhan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrHelpDeskKeluhan.Add(new HelpDeskKeluhan());

            return arrHelpDeskKeluhan;
        }

        public ArrayList RetrieveDept()
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from Dept where RowStatus > -1");
            strError = dataAccess.Error;
            arrHelpDeskKeluhan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrHelpDeskKeluhan.Add(GenerateObject1(sqlDataReader));
                }
            }
            else
                arrHelpDeskKeluhan.Add(new Dept());

            return arrHelpDeskKeluhan;
        }

        public ArrayList RetrieveHelpDeskCategory()
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from HelpDeskCategory where RowStatus = 0");
            strError = dataAccess.Error;
            arrHelpDeskKeluhan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrHelpDeskKeluhan.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrHelpDeskKeluhan.Add(new HelpDeskKeluhan());

            return arrHelpDeskKeluhan;
        }

        public HelpDeskKeluhan RetrieveByCode(string helpdeskno)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from HelpDeskKeluhan where RowStatus = 0 and HelpDeskNo = '" + helpdeskno + "'");
            strError = dataAccess.Error;
            arrHelpDeskKeluhan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new HelpDeskKeluhan();

        }

        public HelpDeskKeluhan RetrieveById(int Id)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from HelpDeskKeluhan where RowStatus = 0 and ID = " + Id);
            strError = dataAccess.Error;
            arrHelpDeskKeluhan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new HelpDeskKeluhan();
        }

        public int CountHelpKeluhan()
        {
            string strSQL = "Select COUNT(Helpdeskno) as ID from HelpDeskKeluhan";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            //SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("SELECT MAX(SUBSTRING(HelpDeskNo,3,4)) as ID from HelpDeskKeluhan");
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return Convert.ToInt32(sqlDataReader["id"]);
                }
            }

            return 0;
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from HelpDeskKeluhan where RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrHelpDeskKeluhan = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrHelpDeskKeluhan.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrHelpDeskKeluhan.Add(new HelpDeskKeluhan());

            return arrHelpDeskKeluhan;

        }



        public HelpDeskKeluhan GenerateObject(SqlDataReader sqlDataReader)
        {
            objHelpDeskKeluhan = new HelpDeskKeluhan();
            objHelpDeskKeluhan.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objHelpDeskKeluhan.HelpTgl = Convert.ToDateTime(sqlDataReader["HelpTgl"]);
            objHelpDeskKeluhan.HelpDeskNo = sqlDataReader["HelpDeskNo"].ToString();
            objHelpDeskKeluhan.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);
            objHelpDeskKeluhan.DeptName = sqlDataReader["DeptName"].ToString();
            objHelpDeskKeluhan.Status = Convert.ToInt32(sqlDataReader["Status"]);
            objHelpDeskKeluhan.HelpDeskCategoryID = Convert.ToInt32(sqlDataReader["HelpDeskCategoryID"]);
            objHelpDeskKeluhan.Keluhan = sqlDataReader["Keluhan"].ToString();
            objHelpDeskKeluhan.Perbaikan = sqlDataReader["Perbaikan"].ToString();
            objHelpDeskKeluhan.TglPerbaikan = Convert.ToDateTime(sqlDataReader["TglPerbaikan"]);
            //objHelpDeskKeluhan.TglPerbaikan =(sqlDataReader["TglPerbaikan"]==DBNull.Value)?DateTime.MinValue:Convert.To.DateTime((sqlDataReader["TglPerbaikan"]));
            objHelpDeskKeluhan.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objHelpDeskKeluhan.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objHelpDeskKeluhan.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objHelpDeskKeluhan.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objHelpDeskKeluhan.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            objHelpDeskKeluhan.PIC = sqlDataReader["PIC"].ToString();
            objHelpDeskKeluhan.Analisa = sqlDataReader["Analisa"].ToString();
            return objHelpDeskKeluhan;
        }

        public Dept GenerateObject1(SqlDataReader sqlDataReader)
        {
            objDept = new Dept();
            objDept.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objDept.DeptCode = sqlDataReader["DeptCode"].ToString();
            objDept.DeptName = sqlDataReader["DeptName"].ToString();
            objDept.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objDept.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objDept.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objDept.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objDept.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objDept;
        }

        public HelpDeskCategory GenerateObject2(SqlDataReader sqlDataReader)
        {
            objHelpDeskCategory = new HelpDeskCategory();
            objHelpDeskCategory.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objHelpDeskCategory.HelpCategory = sqlDataReader["HelpCategory"].ToString();
            objHelpDeskCategory.RowStatus = Convert.ToInt32(sqlDataReader["RowStatus"]);
            objHelpDeskCategory.CreatedBy = sqlDataReader["CreatedBy"].ToString();
            objHelpDeskCategory.CreatedTime = Convert.ToDateTime(sqlDataReader["CreatedTime"]);
            objHelpDeskCategory.LastModifiedBy = sqlDataReader["LastModifiedBy"].ToString();
            objHelpDeskCategory.LastModifiedTime = Convert.ToDateTime(sqlDataReader["LastModifiedTime"]);
            return objHelpDeskCategory;
        }

        //public string QueryKeluhan(string drTgl)
        //{
        //    //string strQuery = "select * from HelpDeskKeluhan order by ID Asc";
        //    //return strQuery;


        //  string strQuery =  "select convert (varchar, HelpdeskKeluhan.HelpTgl,103)as Tanggal,HelpdeskKeluhan.HelpDeskNo,HelpdeskKeluhan.DeptName,HelpdeskKeluhan.Keluhan," + 
        //    "HelpDeskKeluhan.Analisa," +
        //    "case" + 
        //        "when HelpDeskKeluhan.KategoriPenyelesaianID = 0 then 'K1'" +
        //        "when HelpDeskKeluhan.KategoriPenyelesaianID = 1 then 'K2'" +
        //        "when HelpDeskKeluhan.KategoriPenyelesaianID = 2 then 'K3'" + 
        //        "end KategoriPenyelesaian,"+
        //    "HelpDeskKeluhan.Perbaikan,HelpDeskCategory.HelpCategory,HelpDeskKeluhan.PIC, "+
        //    "case"+ 
        //        "when HelpdeskKeluhan.Status = 0 then 'Open' "+
        //        "when HelpdeskKeluhan.Status = 1 then 'Progress'"+ 
        //        "when HelpdeskKeluhan.Status = 2 then 'Solved'"+
        //        "end Status,"+
        //        "HelpDeskKeluhan.CreatedBy,convert (varchar,HelpDeskKeluhan.LastModifiedTime,103)as TanggalPerbaikan"+
        //    "from HelpDeskKeluhan,HelpDeskCategory where HelpDeskKeluhan.HelpDeskCategoryID=HelpDeskCategory.ID"+
        //    "and HelpDeskKeluhan.HelpTgl between 'drTgl' and 'sdTgl' " +
        //    "and HelpdeskKeluhan.RowStatus > -1 order by HelpdeskKeluhan.ID desc";
        //  return strQuery;
        //}

    }
}
