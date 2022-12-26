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
    public class SarMutPencapaianNilaiFacade : AbstractFacade
    {
        private SarMutPencapaianNilai objSarMutPencapaianNilai = new SarMutPencapaianNilai();
        private ArrayList arrSarMutPencapaianNilai;
        private List<SqlParameter> sqlListParam;


        public SarMutPencapaianNilaiFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            try
            {
                objSarMutPencapaianNilai = (SarMutPencapaianNilai)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@CreatedBy", objSarMutPencapaianNilai.CreatedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertRules");

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
                objSarMutPencapaianNilai = (SarMutPencapaianNilai)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objSarMutPencapaianNilai.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objSarMutPencapaianNilai.CreatedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateRules");

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
                objSarMutPencapaianNilai = (SarMutPencapaianNilai)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@ID", objSarMutPencapaianNilai.ID));
                sqlListParam.Add(new SqlParameter("@LastModifiedBy", objSarMutPencapaianNilai.LastModifiedBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spDeleteRules");

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
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from SarMutPencapaianNilai where RowStatus = 0");
            strError = dataAccess.Error;
            arrSarMutPencapaianNilai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSarMutPencapaianNilai.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSarMutPencapaianNilai.Add(new SarMutPencapaianNilai());

            return arrSarMutPencapaianNilai;
        }

        public SarMutPencapaianNilai RetrieveByCode(string ruleName)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from SarMutPencapaianNilai where RowStatus = 0 and RuleName = '" + ruleName + "'");
            strError = dataAccess.Error;
            arrSarMutPencapaianNilai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject(sqlDataReader);
                }
            }

            return new SarMutPencapaianNilai();
        }

        public ArrayList RetrieveByCriteria(string strField, string strValue)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("Select * from SarMutPencapaianNilai where RowStatus = 0 and " + strField + " like '%" + strValue + "%'");
            strError = dataAccess.Error;
            arrSarMutPencapaianNilai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSarMutPencapaianNilai.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSarMutPencapaianNilai.Add(new SarMutPencapaianNilai());

            return arrSarMutPencapaianNilai;
        }
        public ArrayList RetrieveByUserID2(int userID)
        {
            //masih ada Rulemenu yg double
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select a.UserName,c.RolesName,e.ID,e.RuleName,e.sort,e.Level,e.Href from Users as a, UserRoles as b,Roles as c,RoleSarMutPencapaianNilai as d, SarMutPencapaianNilai as e where a.RowStatus = 0 and a.ID = " + userID + " and a.ID = b.UserID and b.RoleID = c.ID and c.RowStatus = 0 and c.ID = d.RoleID and d.RuleID = e.ID and e.RowStatus = 0 and e.sort is not null and e.Level > 0 order by e.sort, e.Level, e.RuleName");
            strError = dataAccess.Error;
            arrSarMutPencapaianNilai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSarMutPencapaianNilai.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSarMutPencapaianNilai.Add(new SarMutPencapaianNilai());

            return arrSarMutPencapaianNilai;
        }
        public ArrayList RetrieveByUserID(int userID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select aa.*,(select top 1 RolesName " +
                "from Users as a, UserRoles as b, Roles as c where a.RowStatus = 0 and a.ID = " + userID + " and a.ID = b.UserID and b.RoleID = c.ID and c.RowStatus = 0) as RolesName " +
                "from(select distinct a.ID, a.UserName, d.RuleID, e.RuleName, e.IDname, e.sort, e.Level, e.Href from Users as a, UserRoles as b, Roles as c, RoleSarMutPencapaianNilai as d, SarMutPencapaianNilai as e " +
                "where a.RowStatus = 0 and a.ID = " + userID + " and a.ID = b.UserID and b.RoleID = c.ID and c.RowStatus = 0 and c.ID = d.RoleID and d.RuleID = e.ID) as aa " +
                "where sort is not null and level> 0 order by sort,Level");
            strError = dataAccess.Error;
            arrSarMutPencapaianNilai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSarMutPencapaianNilai.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSarMutPencapaianNilai.Add(new SarMutPencapaianNilai());

            return arrSarMutPencapaianNilai;
        }
        public ArrayList RetrieveByAllMenuActive()
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select distinct d.RuleID as ID, e.RuleName, e.IDname, e.sort, e.Level, e.Href from UserRoles as b, Roles as c, RoleSarMutPencapaianNilai as d, SarMutPencapaianNilai as e " +
                "where b.RoleID = c.ID and c.RowStatus = 0 and c.ID = d.RoleID and d.RuleID = e.ID and e.Sort is not null and e.Level > 0 order by sort, Level");
            strError = dataAccess.Error;
            arrSarMutPencapaianNilai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSarMutPencapaianNilai.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrSarMutPencapaianNilai.Add(new SarMutPencapaianNilai());

            return arrSarMutPencapaianNilai;
        }

        public SarMutPencapaianNilai RetrieveBySortAndLevel(string sort, int level)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 1 * from SarMutPencapaianNilai where level=" + level + " and Sort like '%" + sort + "%' and RowStatus = 0");
            strError = dataAccess.Error;
            arrSarMutPencapaianNilai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new SarMutPencapaianNilai();
        }
        public SarMutPencapaianNilai RetrieveByIDname(string idname)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString("select top 1 * from SarMutPencapaianNilai where IDname like '%" + idname + "%' and RowStatus=0");
            strError = dataAccess.Error;
            arrSarMutPencapaianNilai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    return GenerateObject2(sqlDataReader);
                }
            }

            return new SarMutPencapaianNilai();
        }
        public ArrayList OutputProduksi(int depoID, int thn)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strQuery = "select aa.*,isnull(a.ID,0) as ID,isnull(a.SarMutParameterTerukurID,0) as SarMutParameterTerukurID,isnull(a.DepoID,0) as DepoID,isnull(a.Tipe,0) as Tipe,isnull(a.Tahun,0) as Tahun,isnull(a.Jan,0) as Jan,isnull(a.Feb,0) as Feb,isnull(a.Mar,0) as Mar,isnull(a.Apr,0) as Apr,isnull(a.Mei,0) as Mei,isnull(a.Jun,0) as Jun,isnull(a.Jul,0) as Jul,isnull(a.Agu,0) as Agu,isnull(a.Sep,0) as Sep,isnull(a.Okt,0) as Okt,isnull(a.Nov,0) as Nov,isnull(a.Des,0) as Des,isnull(a.Jan,0)+isnull(a.Feb,0)+isnull(a.Mar,0)+isnull(a.Apr,0)+isnull(a.Mei,0)+isnull(a.Jun,0) as Smt1TahunIni,isnull(a.Jul,0)+isnull(a.Agu,0)+isnull(a.Sep,0)+isnull(a.Okt,0)+isnull(a.Nov,0)+isnull(a.Des,0) as Smt2TahunIni, "+
                "isnull((select a.Jan + a.Feb + a.Mar + a.Apr + a.Mei + a.Jun from SarMut_PencapaianNilai as a1 where a1.Tahun = 2016 and DepoID = 1),0) as Smt1LY,isnull((select a.Jul + a.Agu + a.Sep + a.Okt + a.Nov + a.Des from SarMut_PencapaianNilai as a1 where a1.Tahun = 2016 and DepoID = 1),0) as Smt2LY "+
                "from (select d.Dimensi, d.SarMutPerusahaan, e.DeptName, c.SarMutDepartment, b.ParameterTerukur, b.ID as ParameterTerukurID, d.Urutan as urut1, c.Urutan as urut2, b.Urutan as urut3 " +
                "from SarMut_ParameterTerukur as b, SarMut_Departemen as c, SarMut_Perusahaan as d, ISO_Dept as e where b.SarMutPerusahaanID = d.ID and b.SarMutDepartemenID = c.ID and b.DeptID = e.ID and d.DepoID = "+depoID+") as aa "+
                "left join SarMut_PencapaianNilai as a on a.SarMutParameterTerukurID = aa.ParameterTerukurID and a.Tahun = "+thn+" order by urut1,urut2,urut3";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strQuery);
            strError = dataAccess.Error;
            arrSarMutPencapaianNilai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSarMutPencapaianNilai.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSarMutPencapaianNilai.Add(new SarMutPencapaianNilai());

            return arrSarMutPencapaianNilai;
        }
        public ArrayList PencapaianSarMut(int thn, int sarMutDeptID, int tipeSarMut)
        {
            //gak usah depoID krn beda server
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strQuery = "select aa.*,isnull(a.Header,1) as Header,isnull(a.ID,0) as ID,isnull(a.SarMutParameterTerukurID,0) as SarMutParameterTerukurID,isnull(a.DepoID,0) as DepoID,isnull(a.Tipe,0) as Tipe,isnull(a.Tahun,0) as Tahun "+
", isnull(a.Jan, 0) as Jan,isnull(a.Feb, 0) as Feb,isnull(a.Mar, 0) as Mar,isnull(a.Apr, 0) as Apr,isnull(a.Mei, 0) as Mei,isnull(a.Jun, 0) as Jun,isnull(a.Jul, 0) as Jul, " +
"isnull(a.Agu, 0) as Agu,isnull(a.Sep, 0) as Sep,isnull(a.Okt, 0) as Okt,isnull(a.Nov, 0) as Nov,isnull(a.Des, 0) as Des " +
",case when a.Header = 0 then " +
"convert(decimal(16,2), isnull( round((case when ISNUMERIC(rtrim(Jan)) = 1 then CONVERT(decimal(16, 2), rtrim(Jan)) else 0 end + case when ISNUMERIC(rtrim(Feb)) = 1 then CONVERT(decimal(16, 2), rtrim(Feb)) else 0 end + " +
"case when ISNUMERIC(rtrim(Mar)) = 1 then CONVERT(decimal(16, 2), rtrim(Mar)) else 0 end + case when ISNUMERIC(rtrim(Apr)) = 1 then CONVERT(decimal(16, 2), rtrim(Apr)) else 0 end + " +
"case when ISNUMERIC(rtrim(Mei)) = 1 then CONVERT(decimal(16, 2), rtrim(Mei)) else 0 end + case when ISNUMERIC(rtrim(Jun)) = 1 then CONVERT(decimal(16, 2), rtrim(Jun)) else 0 end " +
") / (select isnull(sum(JmlSmt1), 1) from[sqlctrp.grcboard.com].[bpasctrp].[dbo].[vw_GetJmlBlnForAverageISO] where SarMutDepartemenID = "+sarMutDeptID+" and Tahun = "+thn+" and Tipe = "+tipeSarMut+") ,2) ,0) )  " +
"else 0 end Smt1TahunIni, " +
"case when a.Header = 0 then " +
"convert(decimal(16,2), isnull( round((case when ISNUMERIC(rtrim(Jul)) = 1 then CONVERT(decimal(16, 2), rtrim(Jul)) else 0 end + case when ISNUMERIC(rtrim(Agu)) = 1 then CONVERT(decimal(16, 2), rtrim(Agu)) else 0 end + " +
"case when ISNUMERIC(rtrim(Sep)) = 1 then CONVERT(decimal(16, 2), rtrim(Sep)) else 0 end + case when ISNUMERIC(rtrim(Okt)) = 1 then CONVERT(decimal(16, 2), rtrim(Okt)) else 0 end + " +
"case when ISNUMERIC(rtrim(Nov)) = 1 then CONVERT(decimal(16, 2), rtrim(Nov)) else 0 end + case when ISNUMERIC(rtrim(Des)) = 1 then CONVERT(decimal(16, 2), rtrim(Des)) else 0 end " +
") / (select isnull(sum(JmlSmt2), 1) from[sqlctrp.grcboard.com].[bpasctrp].[dbo].[vw_GetJmlBlnForAverageISO] where SarMutDepartemenID = "+sarMutDeptID+" and Tahun = "+thn+ " and Tipe = " + tipeSarMut + ") ,2) ,0) ) else 0 end Smt2TahunIni " +

",case when a.Header = 0 then " +
"convert(decimal(16,2), isnull( round(((select sum(case when ISNUMERIC(rtrim(Jan)) = 1 then CONVERT(decimal(16, 2), rtrim(Jan)) else 0 end + case when ISNUMERIC(rtrim(Feb)) = 1 then CONVERT(decimal(16, 2), rtrim(Feb)) else 0 end + " +
"case when ISNUMERIC(rtrim(Mar)) = 1 then CONVERT(decimal(16, 2), rtrim(Mar)) else 0 end + case when ISNUMERIC(rtrim(Apr)) = 1 then CONVERT(decimal(16, 2), rtrim(Apr)) else 0 end + " +
"case when ISNUMERIC(rtrim(Mei)) = 1 then CONVERT(decimal(16, 2), rtrim(Mei)) else 0 end + case when ISNUMERIC(rtrim(Jun)) = 1 then CONVERT(decimal(16, 2), rtrim(Jun)) else 0 end " +
") from [sqlctrp.grcboard.com].[bpasctrp].[dbo].[SarMut_PencapaianNilai] as a1, [sqlctrp.grcboard.com].[bpasctrp].[dbo].[SarMut_ParameterTerukur] as a2 where a1.SarMutParameterTerukurID=a2.ID and a2.SarMutDepartemenID="+sarMutDeptID+" and a1.Tahun = "+(thn-1)+ " and a1.Tipe=" + tipeSarMut + ") / (select isnull(sum(JmlSmt1), 1) from[sqlctrp.grcboard.com].[bpasctrp].[dbo].[vw_GetJmlBlnForAverageISO] where SarMutDepartemenID = " + sarMutDeptID+" and Tahun = "+(thn-1)+ " and Tipe = " + tipeSarMut + ") )  " +
",2) ,0) ) else 0 end Smt1LY, " +
"case when a.Header = 0 then " +
"convert(decimal(16,2), isnull( round(((select sum(case when ISNUMERIC(rtrim(Jul)) = 1 then CONVERT(decimal(16, 2), rtrim(Jul)) else 0 end + case when ISNUMERIC(rtrim(Agu)) = 1 then CONVERT(decimal(16, 2), rtrim(Agu)) else 0 end + " +
"case when ISNUMERIC(rtrim(Sep)) = 1 then CONVERT(decimal(16, 2), rtrim(Sep)) else 0 end + case when ISNUMERIC(rtrim(Okt)) = 1 then CONVERT(decimal(16, 2), rtrim(Okt)) else 0 end + " +
"case when ISNUMERIC(rtrim(Nov)) = 1 then CONVERT(decimal(16, 2), rtrim(Nov)) else 0 end + case when ISNUMERIC(rtrim(Des)) = 1 then CONVERT(decimal(16, 2), rtrim(Des)) else 0 end " +
") from [sqlctrp.grcboard.com].[bpasctrp].[dbo].[SarMut_PencapaianNilai] as a1, [sqlctrp.grcboard.com].[bpasctrp].[dbo].[SarMut_ParameterTerukur] as a2 where a1.SarMutParameterTerukurID=a2.ID and a2.SarMutDepartemenID="+sarMutDeptID+" and a1.Tahun = "+(thn-1)+ " and a1.Tipe=" + tipeSarMut + ") " +
"/ (select isnull(sum(JmlSmt2), 1) from[sqlctrp.grcboard.com].[bpasctrp].[dbo].[vw_GetJmlBlnForAverageISO] where SarMutDepartemenID = "+sarMutDeptID+" and Tahun = "+(thn-1)+ " and Tipe = " + tipeSarMut + ") ) ,2) ,0) ) else 0 end Smt2LY " +

"from(select d.Dimensi, d.SarMutPerusahaan, e.DeptName, c.SarMutDepartment, b.ParameterTerukur, b.ID as ParameterTerukurID, d.Urutan as urut1, c.Urutan as urut2, b.Urutan as urut3, isnull(c.DivHtmlNo, 0) as DivHtmlNo " +
"from[sqlctrp.grcboard.com].[bpasctrp].[dbo].[SarMut_ParameterTerukur] as b, [sqlctrp.grcboard.com].[bpasctrp].[dbo].[SarMut_Departemen] as c, [sqlctrp.grcboard.com].[bpasctrp].[dbo].[SarMut_Perusahaan] as d, [sqlctrp.grcboard.com].[bpasctrp].[dbo].[ISO_Dept] as e where b.SarMutPerusahaanID = d.ID and b.SarMutDepartemenID = c.ID and b.DeptID = e.ID and d.DepoID = 1 and c.ID = "+sarMutDeptID+ " and b.Tipe = " + tipeSarMut + ") as aa " +
"left join[sqlctrp.grcboard.com].[bpasctrp].[dbo].[SarMut_PencapaianNilai] as a on a.SarMutParameterTerukurID = aa.ParameterTerukurID and a.Tahun = "+thn+ " and Tipe = " + tipeSarMut  +
"order by urut1,urut2,urut3";

            //string strQuery = "select aa.*,isnull(a.ID,0) as ID,isnull(a.SarMutParameterTerukurID,0) as SarMutParameterTerukurID,isnull(a.DepoID,0) as DepoID,isnull(a.Tipe,0) as Tipe,isnull(a.Tahun,0) as Tahun,isnull(a.Jan,0) as Jan,isnull(a.Feb,0) as Feb,isnull(a.Mar,0) as Mar,isnull(a.Apr,0) as Apr,isnull(a.Mei,0) as Mei,isnull(a.Jun,0) as Jun,isnull(a.Jul,0) as Jul,isnull(a.Agu,0) as Agu,isnull(a.Sep,0) as Sep,isnull(a.Okt,0) as Okt,isnull(a.Nov,0) as Nov,isnull(a.Des,0) as Des,isnull(a.Jan,0)+isnull(a.Feb,0)+isnull(a.Mar,0)+isnull(a.Apr,0)+isnull(a.Mei,0)+isnull(a.Jun,0) as Smt1TahunIni,isnull(a.Jul,0)+isnull(a.Agu,0)+isnull(a.Sep,0)+isnull(a.Okt,0)+isnull(a.Nov,0)+isnull(a.Des,0) as Smt2TahunIni, "+
            //    "isnull((select a.Jan + a.Feb + a.Mar + a.Apr + a.Mei + a.Jun from[sqlctrp.grcboard.com].[bpasctrp].[dbo].[SarMut_PencapaianNilai] as a1 where a1.Tahun = "+(thn-1)+ " and DepoID = " + depoID + "),0) as Smt1LY,isnull((select a.Jul + a.Agu + a.Sep + a.Okt + a.Nov + a.Des from[sqlctrp.grcboard.com].[bpasctrp].[dbo].[SarMut_PencapaianNilai] as a1 where a1.Tahun = " + (thn-1)+" and DepoID = "+depoID+"),0) as Smt2LY " +
            //    "from(select d.Dimensi, d.SarMutPerusahaan, e.DeptName, c.SarMutDepartment, b.ParameterTerukur, b.ID as ParameterTerukurID, d.Urutan as urut1, c.Urutan as urut2, b.Urutan as urut3 " +
            //    "from[sqlctrp.grcboard.com].[bpasctrp].[dbo].[SarMut_ParameterTerukur] as b, [sqlctrp.grcboard.com].[bpasctrp].[dbo].[SarMut_Departemen] as c, [sqlctrp.grcboard.com].[bpasctrp].[dbo].[SarMut_Perusahaan] as d, [sqlctrp.grcboard.com].[bpasctrp].[dbo].[ISO_Dept] as e where b.SarMutPerusahaanID = d.ID and b.SarMutDepartemenID = c.ID and b.DeptID = e.ID and d.DepoID = " + depoID + " and c.ID = " + sarMutDeptID +
            //    ") as aa left join[sqlctrp.grcboard.com].[bpasctrp].[dbo].[SarMut_PencapaianNilai] as a on a.SarMutParameterTerukurID = aa.ParameterTerukurID and a.Tahun = "+thn+" order by urut1,urut2,urut3";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strQuery);
            strError = dataAccess.Error;
            arrSarMutPencapaianNilai = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrSarMutPencapaianNilai.Add(GenerateObject(sqlDataReader));
                }
            }
            else
                arrSarMutPencapaianNilai.Add(new SarMutPencapaianNilai());

            return arrSarMutPencapaianNilai;
        }
        public DataTable dtPencapaianSarMut(int depoID, int thn, int sarMutDeptID)
        {
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            string strQuery = "select aa.*,isnull(a.ID,0) as ID,isnull(a.SarMutParameterTerukurID,0) as SarMutParameterTerukurID,isnull(a.DepoID,0) as DepoID,isnull(a.Tipe,0) as Tipe,isnull(a.Tahun,0) as Tahun,isnull(a.Jan,0) as Jan,isnull(a.Feb,0) as Feb,isnull(a.Mar,0) as Mar,isnull(a.Apr,0) as Apr,isnull(a.Mei,0) as Mei,isnull(a.Jun,0) as Jun,isnull(a.Jul,0) as Jul,isnull(a.Agu,0) as Agu,isnull(a.Sep,0) as Sep,isnull(a.Okt,0) as Okt,isnull(a.Nov,0) as Nov,isnull(a.Des,0) as Des,isnull(a.Jan,0)+isnull(a.Feb,0)+isnull(a.Mar,0)+isnull(a.Apr,0)+isnull(a.Mei,0)+isnull(a.Jun,0) as Smt1TahunIni,isnull(a.Jul,0)+isnull(a.Agu,0)+isnull(a.Sep,0)+isnull(a.Okt,0)+isnull(a.Nov,0)+isnull(a.Des,0) as Smt2TahunIni, " +
                "isnull((select a.Jan + a.Feb + a.Mar + a.Apr + a.Mei + a.Jun from[sqlctrp.grcboard.com].[bpasctrp].[dbo].[SarMut_PencapaianNilai] as a1 where a1.Tahun = " + (thn - 1) + " and DepoID = " + depoID + "),0) as Smt1LY,isnull((select a.Jul + a.Agu + a.Sep + a.Okt + a.Nov + a.Des from[sqlctrp.grcboard.com].[bpasctrp].[dbo].[SarMut_PencapaianNilai] as a1 where a1.Tahun = " + (thn - 1) + " and DepoID = " + depoID + "),0) as Smt2LY " +
                "from(select d.Dimensi, d.SarMutPerusahaan, e.DeptName, c.SarMutDepartment, b.ParameterTerukur, b.ID as ParameterTerukurID, d.Urutan as urut1, c.Urutan as urut2, b.Urutan as urut3 " +
                "from[sqlctrp.grcboard.com].[bpasctrp].[dbo].[SarMut_ParameterTerukur] as b, [sqlctrp.grcboard.com].[bpasctrp].[dbo].[SarMut_Departemen] as c, [sqlctrp.grcboard.com].[bpasctrp].[dbo].[SarMut_Perusahaan] as d, [sqlctrp.grcboard.com].[bpasctrp].[dbo].[ISO_Dept] as e where b.SarMutPerusahaanID = d.ID and b.SarMutDepartemenID = c.ID and b.DeptID = e.ID and d.DepoID = " + depoID + " and c.ID = " + sarMutDeptID +
                ") as aa left join[sqlctrp.grcboard.com].[bpasctrp].[dbo].[SarMut_PencapaianNilai] as a on a.SarMutParameterTerukurID = aa.ParameterTerukurID and a.Tahun = " + thn + " order by urut1,urut2,urut3";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strQuery);
            strError = dataAccess.Error;
            DataTable dt = new DataTable();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    dt.Load(sqlDataReader);
                }
            }

            return dt;
        }

        public SarMutPencapaianNilai GenerateObject(SqlDataReader sqlDataReader)
        {
            objSarMutPencapaianNilai = new SarMutPencapaianNilai();
            objSarMutPencapaianNilai.Dimensi = sqlDataReader["Dimensi"].ToString();
            objSarMutPencapaianNilai.SarMutPerusahaan = sqlDataReader["SarMutPerusahaan"].ToString();
            objSarMutPencapaianNilai.DeptName = sqlDataReader["DeptName"].ToString();
            objSarMutPencapaianNilai.SarMutDepartment = sqlDataReader["SarMutDepartment"].ToString();
            objSarMutPencapaianNilai.ParameterTerukur = sqlDataReader["ParameterTerukur"].ToString();
            objSarMutPencapaianNilai.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objSarMutPencapaianNilai.SarMutParameterTerukurID = Convert.ToInt32(sqlDataReader["SarMutParameterTerukurID"]);
            objSarMutPencapaianNilai.DepoID = Convert.ToInt32(sqlDataReader["DepoID"]);
            objSarMutPencapaianNilai.Tipe = Convert.ToInt32(sqlDataReader["Tipe"]);
            objSarMutPencapaianNilai.DivHtmlNo = Convert.ToInt32(sqlDataReader["DivHtmlNo"]);
            objSarMutPencapaianNilai.Header = Convert.ToInt32(sqlDataReader["Header"]);

            if (string.IsNullOrEmpty(sqlDataReader["Tahun"].ToString()) || sqlDataReader["Tahun"].ToString() == "0")
                objSarMutPencapaianNilai.Tahun = string.Empty;
            else
                objSarMutPencapaianNilai.Tahun = sqlDataReader["Tahun"].ToString();
            if (string.IsNullOrEmpty(sqlDataReader["Jan"].ToString()) || sqlDataReader["Jan"].ToString() == "0")
                objSarMutPencapaianNilai.Jan = string.Empty;
            else
                objSarMutPencapaianNilai.Jan = sqlDataReader["Jan"].ToString();
            if (string.IsNullOrEmpty(sqlDataReader["Feb"].ToString()) || sqlDataReader["Feb"].ToString() == "0")
                objSarMutPencapaianNilai.Feb = string.Empty;
            else
                objSarMutPencapaianNilai.Feb = sqlDataReader["Feb"].ToString();
            if (string.IsNullOrEmpty(sqlDataReader["Mar"].ToString()) || sqlDataReader["Mar"].ToString() == "0")
                objSarMutPencapaianNilai.Mar = string.Empty;
            else
                objSarMutPencapaianNilai.Mar = sqlDataReader["Mar"].ToString();
            if (string.IsNullOrEmpty(sqlDataReader["Apr"].ToString()) || sqlDataReader["Apr"].ToString() == "0")
                objSarMutPencapaianNilai.Apr = string.Empty;
            else
                objSarMutPencapaianNilai.Apr = sqlDataReader["Apr"].ToString();
            if (string.IsNullOrEmpty(sqlDataReader["Mei"].ToString()) || sqlDataReader["Mei"].ToString() == "0")
                objSarMutPencapaianNilai.Mei = string.Empty;
            else
                objSarMutPencapaianNilai.Mei = sqlDataReader["Mei"].ToString();
            if (string.IsNullOrEmpty(sqlDataReader["Jun"].ToString()) || sqlDataReader["Jun"].ToString() == "0")
                objSarMutPencapaianNilai.Jun = string.Empty;
            else
                objSarMutPencapaianNilai.Jun = sqlDataReader["Jun"].ToString();
            if (string.IsNullOrEmpty(sqlDataReader["Jul"].ToString()) || sqlDataReader["Jul"].ToString() == "0")
                objSarMutPencapaianNilai.Jul = string.Empty;
            else
                objSarMutPencapaianNilai.Jul = sqlDataReader["Jul"].ToString();
            if (string.IsNullOrEmpty(sqlDataReader["Agu"].ToString()) || sqlDataReader["Agu"].ToString() == "0")
                objSarMutPencapaianNilai.Agu = string.Empty;
            else
                objSarMutPencapaianNilai.Agu = sqlDataReader["Agu"].ToString();
            if (string.IsNullOrEmpty(sqlDataReader["Sep"].ToString()) || sqlDataReader["Sep"].ToString() == "0")
                objSarMutPencapaianNilai.Sep = string.Empty;
            else
                objSarMutPencapaianNilai.Sep = sqlDataReader["Sep"].ToString();
            if (string.IsNullOrEmpty(sqlDataReader["Okt"].ToString()) || sqlDataReader["Okt"].ToString() == "0")
                objSarMutPencapaianNilai.Oct = string.Empty;
            else
                objSarMutPencapaianNilai.Oct = sqlDataReader["Okt"].ToString();
            if (string.IsNullOrEmpty(sqlDataReader["Nov"].ToString()) || sqlDataReader["Nov"].ToString() == "0")
                objSarMutPencapaianNilai.Nov = string.Empty;
            else
                objSarMutPencapaianNilai.Nov = sqlDataReader["Nov"].ToString();
            if (string.IsNullOrEmpty(sqlDataReader["Des"].ToString()) || sqlDataReader["Des"].ToString() == "0")
                objSarMutPencapaianNilai.Des = string.Empty;
            else
                objSarMutPencapaianNilai.Des = sqlDataReader["Des"].ToString();
            if (string.IsNullOrEmpty(sqlDataReader["Smt1TahunIni"].ToString()) || sqlDataReader["Smt1TahunIni"].ToString() == "0")
                objSarMutPencapaianNilai.Smt1TahunIni = string.Empty;
            else
                objSarMutPencapaianNilai.Smt1TahunIni = sqlDataReader["Smt1TahunIni"].ToString();
            if (string.IsNullOrEmpty(sqlDataReader["Smt2TahunIni"].ToString()) || sqlDataReader["Smt2TahunIni"].ToString() == "0")
                objSarMutPencapaianNilai.Smt2TahunIni = string.Empty;
            else
                objSarMutPencapaianNilai.Smt2TahunIni = sqlDataReader["Smt2TahunIni"].ToString();
            if (string.IsNullOrEmpty(sqlDataReader["Smt1LY"].ToString()) || sqlDataReader["Smt1LY"].ToString() == "0")
                objSarMutPencapaianNilai.Smt1LY = string.Empty;
            else
                objSarMutPencapaianNilai.Smt1LY = sqlDataReader["Smt1LY"].ToString();
            if (string.IsNullOrEmpty(sqlDataReader["Smt2LY"].ToString()) || sqlDataReader["Smt2LY"].ToString() == "0")
                objSarMutPencapaianNilai.Smt2LY = string.Empty;
            else
                objSarMutPencapaianNilai.Smt2LY = sqlDataReader["Smt2LY"].ToString();

            //if (string.IsNullOrEmpty(sqlDataReader["Jan"].ToString()) || Convert.ToDecimal(sqlDataReader["Jan"].ToString()) == 0)
            //    objSarMutPencapaianNilai.Jan = string.Empty;
            //else
            //    objSarMutPencapaianNilai.Jan = sqlDataReader["Jan"].ToString();
            //if (string.IsNullOrEmpty(sqlDataReader["Feb"].ToString()) || Convert.ToDecimal(sqlDataReader["Feb"].ToString()) == 0)
            //    objSarMutPencapaianNilai.Feb = string.Empty;
            //else
            //    objSarMutPencapaianNilai.Feb = sqlDataReader["Feb"].ToString();
            //if (string.IsNullOrEmpty(sqlDataReader["Mar"].ToString()) || Convert.ToDecimal(sqlDataReader["Mar"].ToString()) == 0)
            //    objSarMutPencapaianNilai.Mar = string.Empty;
            //else
            //    objSarMutPencapaianNilai.Mar = sqlDataReader["Mar"].ToString();
            //if (string.IsNullOrEmpty(sqlDataReader["Apr"].ToString()) || Convert.ToDecimal(sqlDataReader["Apr"].ToString()) == 0)
            //    objSarMutPencapaianNilai.Apr = string.Empty;
            //else
            //    objSarMutPencapaianNilai.Apr = sqlDataReader["Apr"].ToString();
            //if (string.IsNullOrEmpty(sqlDataReader["Mei"].ToString()) || Convert.ToDecimal(sqlDataReader["Mei"].ToString()) == 0)
            //    objSarMutPencapaianNilai.Mei = string.Empty;
            //else
            //    objSarMutPencapaianNilai.Mei = sqlDataReader["Mei"].ToString();
            //if (string.IsNullOrEmpty(sqlDataReader["Jun"].ToString()) || Convert.ToDecimal(sqlDataReader["Jun"].ToString()) == 0)
            //    objSarMutPencapaianNilai.Jun = string.Empty;
            //else
            //    objSarMutPencapaianNilai.Jun = sqlDataReader["Jun"].ToString();
            //if (string.IsNullOrEmpty(sqlDataReader["Jul"].ToString()) || Convert.ToDecimal(sqlDataReader["Jul"].ToString()) == 0)
            //    objSarMutPencapaianNilai.Jul = string.Empty;
            //else
            //    objSarMutPencapaianNilai.Jul = sqlDataReader["Jul"].ToString();
            //if (string.IsNullOrEmpty(sqlDataReader["Agu"].ToString()) || Convert.ToDecimal(sqlDataReader["Agu"].ToString()) == 0)
            //    objSarMutPencapaianNilai.Agu = string.Empty;
            //else
            //    objSarMutPencapaianNilai.Agu = sqlDataReader["Agu"].ToString();
            //if (string.IsNullOrEmpty(sqlDataReader["Sep"].ToString()) || Convert.ToDecimal(sqlDataReader["Sep"].ToString()) == 0)
            //    objSarMutPencapaianNilai.Sep = string.Empty;
            //else
            //    objSarMutPencapaianNilai.Sep = sqlDataReader["Sep"].ToString();
            //if (string.IsNullOrEmpty(sqlDataReader["Okt"].ToString()) || Convert.ToDecimal(sqlDataReader["Okt"].ToString()) == 0)
            //    objSarMutPencapaianNilai.Oct = string.Empty;
            //else
            //    objSarMutPencapaianNilai.Oct = sqlDataReader["Okt"].ToString();
            //if (string.IsNullOrEmpty(sqlDataReader["Nov"].ToString()) || Convert.ToDecimal(sqlDataReader["Nov"].ToString()) == 0)
            //    objSarMutPencapaianNilai.Nov = string.Empty;
            //else
            //    objSarMutPencapaianNilai.Nov = sqlDataReader["Nov"].ToString();
            //if (string.IsNullOrEmpty(sqlDataReader["Des"].ToString()) || Convert.ToDecimal(sqlDataReader["Des"].ToString()) == 0)
            //    objSarMutPencapaianNilai.Des = string.Empty;
            //else
            //    objSarMutPencapaianNilai.Des = sqlDataReader["Des"].ToString();
            //if (string.IsNullOrEmpty(sqlDataReader["Smt1TahunIni"].ToString()) || Convert.ToDecimal(sqlDataReader["Smt1TahunIni"].ToString()) == 0)
            //    objSarMutPencapaianNilai.Smt1TahunIni = string.Empty;
            //else
            //    objSarMutPencapaianNilai.Smt1TahunIni = sqlDataReader["Smt1TahunIni"].ToString();
            //if (string.IsNullOrEmpty(sqlDataReader["Smt2TahunIni"].ToString()) || Convert.ToDecimal(sqlDataReader["Smt2TahunIni"].ToString()) == 0)
            //    objSarMutPencapaianNilai.Smt2TahunIni = string.Empty;
            //else
            //    objSarMutPencapaianNilai.Smt2TahunIni = sqlDataReader["Smt2TahunIni"].ToString();
            //if (string.IsNullOrEmpty(sqlDataReader["Smt1LY"].ToString()) || Convert.ToDecimal(sqlDataReader["Smt1LY"].ToString()) == 0)
            //    objSarMutPencapaianNilai.Smt1LY = string.Empty;
            //else
            //    objSarMutPencapaianNilai.Smt1LY = sqlDataReader["Smt1LY"].ToString();
            //if (string.IsNullOrEmpty(sqlDataReader["Smt2LY"].ToString()) || Convert.ToDecimal(sqlDataReader["Smt2LY"].ToString()) == 0)
            //    objSarMutPencapaianNilai.Smt2LY = string.Empty;
            //else
            //    objSarMutPencapaianNilai.Smt2LY = sqlDataReader["Smt2LY"].ToString();

            return objSarMutPencapaianNilai;
        }
        public SarMutPencapaianNilai GenerateObject2(SqlDataReader sqlDataReader)
        {
            objSarMutPencapaianNilai = new SarMutPencapaianNilai();
            objSarMutPencapaianNilai.ID = Convert.ToInt32(sqlDataReader["ID"]);

            return objSarMutPencapaianNilai;
        }


    }



}
