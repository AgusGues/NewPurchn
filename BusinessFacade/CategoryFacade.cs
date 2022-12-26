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
    public class CategoryFacade : AbstractFacade
    {
        private Category objCategory = new Category();
        private ArrayList arrCategory;
        private List<SqlParameter> sqlListParam;


        public CategoryFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objCategory = (Category)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@Category", objCategory.Kategory));
                sqlListParam.Add(new SqlParameter("@CategoryDescription", objCategory.CategoryDescription));
                sqlListParam.Add(new SqlParameter("@Bobot", objCategory.Bobot));
                sqlListParam.Add(new SqlParameter("@DeptID", objCategory.DeptID));
                sqlListParam.Add(new SqlParameter("@SectionID", objCategory.SectionID));
                sqlListParam.Add(new SqlParameter("@CreatedBy", objCategory.CreatedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertCategory");

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
                objCategory = (Category)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objCategory.ID));
                sqlListParam.Add(new SqlParameter("@Category", objCategory.Kategory));
                sqlListParam.Add(new SqlParameter("@CategoryDescription", objCategory.CategoryDescription));
                sqlListParam.Add(new SqlParameter("@Bobot", objCategory.Bobot));
                sqlListParam.Add(new SqlParameter("@DeptID", objCategory.DeptID));
                sqlListParam.Add(new SqlParameter("@SectionID", objCategory.SectionID));

                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objCategory.CreatedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateCategory");

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
                objCategory = (Category)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objCategory.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objCategory.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteCategory");

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
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.DeptName,A.RowStatus from ISO_Category as A where A.RowStatus = 0");
            strError = dataAccess.Error;
            arrCategory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrCategory.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrCategory.Add(new Category());

            return arrCategory;
        }

        public ArrayList RetrieveBySectionIDByDeptID(int deptID,int sectionID)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ISO_Category where DeptID=" + deptID + " and SectionID="+sectionID);
            strError = dataAccess.Error;
            arrCategory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrCategory.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrCategory.Add(new Category());

            return arrCategory;
        }
        public ArrayList RetrieveByPesType(int pesType)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ISO_Category where PesType="+ pesType);
            strError = dataAccess.Error;
            arrCategory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrCategory.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrCategory.Add(new Category());

            return arrCategory;
        }
        public Category RetrieveById(int Id)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ISO_Category where ID= " + Id);
            strError = dataAccess.Error;
            arrCategory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Category();
        }

        public Category RetrieveByCode(string deptCode)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.DeptCode,A.DeptName,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.NamaHead from Category as A where A.RowStatus = 0 and A.DeptCode = '" + deptCode + "'");
            strError = dataAccess.Error;
            arrCategory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Category();
        }


        public Category RetrieveByName(string deptName)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.DeptCode,A.DeptName,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.NamaHead from Category as A where A.RowStatus = 0 and A.DeptName = '" + deptName + "'");
            strError = dataAccess.Error;
            arrCategory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new Category();
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select A.ID,A.DeptCode,A.DeptName,A.RowStatus,A.CreatedBy,A.CreatedTime,A.LastModifiedBy,A.LastModifiedTime,A.NamaHead from Category as A where A.RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrCategory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrCategory.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrCategory.Add(new Category());

            return arrCategory;
        }
        public ArrayList RetrieveByPesTypePlantDeptIDUrutan(int pesType, int plant, int deptID, string kodeUrutan)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ISO_Category where PesType=" + pesType + " and Plant=" + plant + " and DeptID=" + deptID + " and KodeUrutan='" + kodeUrutan + "'");
            strError = dataAccess.Error;
            arrCategory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrCategory.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrCategory.Add(new Category());

            return arrCategory;
        }
        public ArrayList RetrieveByPesTypePlantDeptIDSectionID(int pesType, int plant, int deptID, int secID)
        {
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select * from ISO_Category where PesType=" + pesType + " and Plant=" + plant + " and DeptID=" + deptID + " and SectionID='" + secID + "'");
            strError = dataAccess.Error;
            arrCategory = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrCategory.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrCategory.Add(new Category());

            return arrCategory;
        }

        public Category GenerateObject(SqlDataReader sqlDataReader)
        {
            objCategory = new Category();
            objCategory.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objCategory.Kategory = sqlDataReader["Category"].ToString();
            objCategory.CategoryDescription = sqlDataReader["Description"].ToString();
            objCategory.Bobot = Convert.ToInt32(sqlDataReader["Bobot"]);
            objCategory.SectionID = Convert.ToInt32(sqlDataReader["SectionID"]);
            objCategory.DeptID = Convert.ToInt32(sqlDataReader["DeptID"]);

            return objCategory;
        }
        public string Field { get; set; }
        public string Criteria { get; set; }
        public ArrayList RetrieveNewCat()
        {
            arrCategory = new ArrayList();
            string strSQL = "select "+ this.Field+" uc.CategoryID,uc.ID,ic.Description,uc.Bobot,uc.TypeBobot,ic.Target from ISO_UserCategory as uc " +
                          "  left join ISO_Category as ic " +
                          "  on ic.ID=uc.CategoryID where ic.RowStatus >-1 and uc.RowStatus >-1" +
                            this.Criteria;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrCategory.Add(new Category
                    {
                        ID = Convert.ToInt32(sdr["ID"].ToString()),
                        CategoryDescription = sdr["Description"].ToString(),
                        Bobot = Convert.ToDecimal(sdr["Bobot"].ToString()),
                        TypeBobot = sdr["TypeBobot"].ToString(),
                        Target = sdr["Target"].ToString(),
                        CategoryID = Convert.ToInt32(sdr["CategoryID"].ToString())
                    });
                }
            }
            return arrCategory;
        }

        public Category RetrieveDetail()
        {
            objCategory = new Category();
            arrCategory = this.RetrieveNewCat();
            foreach (Category ct in arrCategory)
            {
                objCategory.ID = ct.ID;
                objCategory.Target = ct.Target;
                objCategory.Bobot = ct.Bobot;
                objCategory.TypeBobot = ct.TypeBobot;
                objCategory.CategoryID = ct.CategoryID;
            }
            return objCategory;
        }
    }
}
