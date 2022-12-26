using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public class BendingStrengthFacade : AbstractFacade
    {
        private BendingStrength objBS = new BendingStrength();
        private ArrayList arrBS;
        private ArrayList arrData = new ArrayList();
        private List<SqlParameter> sqlListParam;
        public string Limit { get; set; }
        public BendingStrengthFacade()
            : base()
        {
           
        }

        public override int Insert(object objDomain)
        {
            try
            {
                objBS = (BendingStrength)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@id_prod", objBS.IdProd));
                sqlListParam.Add(new SqlParameter("@tgl_prod", objBS.ProdDate));
                sqlListParam.Add(new SqlParameter("@formula", objBS.Formula));
                sqlListParam.Add(new SqlParameter("@bk", objBS.BK));
                sqlListParam.Add(new SqlParameter("@t", objBS.T));
                sqlListParam.Add(new SqlParameter("@l", objBS.L));
                sqlListParam.Add(new SqlParameter("@p", objBS.P));
                sqlListParam.Add(new SqlParameter("@ba", objBS.BA));
                sqlListParam.Add(new SqlParameter("@bb", objBS.BB));
                sqlListParam.Add(new SqlParameter("@bk2", objBS.BK2));
                sqlListParam.Add(new SqlParameter("@lbc", objBS.LBC));
                sqlListParam.Add(new SqlParameter("@lbl", objBS.LBL));
                sqlListParam.Add(new SqlParameter("@lkc", objBS.LKC));
                sqlListParam.Add(new SqlParameter("@lkl", objBS.LKL));
                sqlListParam.Add(new SqlParameter("@CreateBy", objBS.CreateBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertBsRountineTest");
                strError = dataAccess.Error;
                return intResult;
            }
            catch (Exception ex) 
            {
                strError = ex.Message;
                return -1;
            }
        }

        public int InsertProduction(object objDomain)
        {
            try
            {
                objBS = (BendingStrength)objDomain;
                sqlListParam = new List<SqlParameter>();
                sqlListParam.Add(new SqlParameter("@tgl_prod", objBS.ProdDate));
                sqlListParam.Add(new SqlParameter("@formula", objBS.Formula));
                sqlListParam.Add(new SqlParameter("@group_produksi", objBS.GroupProduksi));
                sqlListParam.Add(new SqlParameter("@jenis_produksi", objBS.JenisProduksi));
                sqlListParam.Add(new SqlParameter("@thicknessC", objBS.ThicknessC));
                sqlListParam.Add(new SqlParameter("@thicknessL", objBS.ThicknessL));
                sqlListParam.Add(new SqlParameter("@peakLoadC", objBS.PeakLoadC));
                sqlListParam.Add(new SqlParameter("@peakLoadL", objBS.PeakLoadL));
                sqlListParam.Add(new SqlParameter("@peakElongationC", objBS.PeakElongationC));
                sqlListParam.Add(new SqlParameter("@peakElongationL", objBS.PeakElongationL));
                sqlListParam.Add(new SqlParameter("@bendingStrengthC", objBS.BendingStrengthC));
                sqlListParam.Add(new SqlParameter("@bendingStrengthL", objBS.BendingStrengthL));
                sqlListParam.Add(new SqlParameter("@areaUnderCurveC", objBS.AreaUnderCurveC));
                sqlListParam.Add(new SqlParameter("@areaUnderCurveL", objBS.AreaUnderCurveL));
                sqlListParam.Add(new SqlParameter("@CreateBy", objBS.CreateBy));

                int intResult = dataAccess.ProcessData(sqlListParam, "spInsertBsProductionTest");

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
                objBS = (BendingStrength)objDomain;
                sqlListParam = new List<SqlParameter>();
                
                sqlListParam.Add(new SqlParameter("@IdProd", objBS.IdProd));
                sqlListParam.Add(new SqlParameter("@chek", objBS.Chek));

                int intResult = dataAccess.ProcessData(sqlListParam, "spUpdateBSProductionChek");

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
            throw new NotImplementedException();
        }

        public override ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }

        

        public ArrayList RetrieveFormula()
        {
            DataAccess da = new DataAccess(Global.ConnectionString());
            string strSQL = 
            "select "+
            "isnull(bptr.ID,0) ID, "+ 
            "isnull(CONVERT(varchar,bptr.tgl_prod,105),0) TglProd, "+
            "Bf.FormulaName Formula,"+ 
            "bptr.group_produksi,"+
            "bptr.jenis_produksi,"+
            "bptr.thicknessC,bptr.thicknessL,"+
            "bptr.peakLoadC,bptr.peakLoadL,"+
            "bptr.peakElongationC,bptr.peakElongationL,"+
            "bptr.bendingStrengthC,bptr.bendingStrengthL,"+
            "bptr.areaUnderCurveC,bptr.areaUnderCurveL "+
            "from BM_Formula bf left Join BS_Production_Testing_Report bptr on bptr.formula = bf.FormulaCode "+
            "where bptr.tgl_prod > 0 and bptr.chek > -1 " +
            "order by bptr.tgl_prod asc";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrBS = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBS.Add(GenerateObject2(sqlDataReader));
                }
            }
            else
                arrBS.Add(new BendingStrength());

            return arrBS;
        }

        public ArrayList RetrieveFormula1()
        {
            DataAccess da = new DataAccess(Global.ConnectionString());
            string strSQL =
            "select * from BM_Formula";

            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrBS = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBS.Add(GenerateObject8(sqlDataReader));
                }
            }
            else
                arrBS.Add(new BendingStrength());

            return arrBS;
        }

        public ArrayList RetrieveLine()
        {
            DataAccess da = new DataAccess(Global.ConnectionString());
            string strSQL = "select * from BS_Line";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrBS = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBS.Add(GenerateObject5(sqlDataReader));
                }
            }
            else
                arrBS.Add(new BendingStrength());

            return arrBS;
        }

        public ArrayList RetrieveKelompokProduk()
        {
            DataAccess da = new DataAccess(Global.ConnectionString());
            string strSQL = "select * from BS_Group order by id asc";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrBS = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBS.Add(GenerateObject6(sqlDataReader));
                }
            }
            else
                arrBS.Add(new BendingStrength());

            return arrBS;
        }

        public ArrayList RetrieveGroupProduksi()
        {
            DataAccess da = new DataAccess(Global.ConnectionString());
            string strSQL = "select * from BM_PlantGroup order by PlantID asc";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrBS = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBS.Add(GenerateObject3(sqlDataReader));
                }
            }
            else
                arrBS.Add(new BendingStrength());

            return arrBS;
        }

        public ArrayList RetrieveJenisProduksi()
        {
            DataAccess da = new DataAccess(Global.ConnectionString());
            string strSQL = "select DISTINCT Kode from FC_Items where RowStatus >-1 union all select Kode from BS_KodeTambahan";
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrBS = new ArrayList();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    arrBS.Add(GenerateObject4(sqlDataReader));
                }
            }
            else
                arrBS.Add(new BendingStrength());

            return arrBS;
        }

        public ArrayList RetrieveFormulaTipe(string pilihan, string line, string tDari, string tSampai)
        {
            string strSQL = QuerypemantauanRountineTest(pilihan, line ,tDari, tSampai);
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrBS = new ArrayList();

            int n = 0;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    n = n + 1;
                    arrBS.Add(new BendingStrength
                    {
                        Nom = n,
                        ID = Convert.ToInt32(sqlDataReader["ID"].ToString()),
                        Tanggal = sqlDataReader["TglProd"].ToString(),
                        Formula = sqlDataReader["formula"].ToString(),
                        BK = Convert.ToDecimal(sqlDataReader["BK"].ToString()),
                        T = Convert.ToDecimal(sqlDataReader["t"].ToString()),
                        L = Convert.ToDecimal(sqlDataReader["l"].ToString()),
                        P = Convert.ToDecimal(sqlDataReader["p"].ToString()),
                        V = Convert.ToDecimal(sqlDataReader["V"].ToString()),
                        Denisty = Convert.ToDecimal(sqlDataReader["Denisty"].ToString()),
                        BA = Convert.ToDecimal(sqlDataReader["BA"].ToString()),
                        BK2 = Convert.ToDecimal(sqlDataReader["BKW"].ToString()),
                        WC = Convert.ToDecimal(sqlDataReader["WC"].ToString()),
                        BB = Convert.ToDecimal(sqlDataReader["BB"].ToString()),
                        BK3 = Convert.ToDecimal(sqlDataReader["BKWA"].ToString()),
                        WA = Convert.ToDecimal(sqlDataReader["WA"].ToString()),
                        LBC = Convert.ToDecimal(sqlDataReader["LBC"].ToString()),
                        LBL = Convert.ToDecimal(sqlDataReader["LBL"].ToString()),
                        LKC = Convert.ToDecimal(sqlDataReader["LKC"].ToString()),
                        LKL = Convert.ToDecimal(sqlDataReader["LKL"].ToString()),
                        DimentioC = Convert.ToDecimal(sqlDataReader["DimentionC"].ToString()),
                        DimentioL = Convert.ToDecimal(sqlDataReader["DimentionL"].ToString())



                    });
                }
            }
            else
            {
                arrBS.Add(new BendingStrength());
            }
            return arrBS;

        }

        public ArrayList RetrieveFormulaTipe2(string pilihanLine,string pilihanPressing,string tDari, string tSampai)
        {
            
            string strSQL = QuerypemantauanProductionTest(pilihanLine,pilihanPressing, tDari, tSampai);
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            strError = dataAccess.Error;
            arrBS = new ArrayList();

            int n = 0;
            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    n = n + 1;
                    arrBS.Add(new BendingStrength
                    {
                        Nom = n,
                        ID = Convert.ToInt32(sqlDataReader["id"].ToString()),
                        Tanggal = sqlDataReader["tgl_prod"].ToString(),
                        Formula = sqlDataReader["Formula"].ToString(),
                        GroupProduksi = sqlDataReader["group_produksi"].ToString(),
                        JenisProduksi = sqlDataReader["jenis_produksi"].ToString(),
                        
                        ThicknessC = Convert.ToDecimal(sqlDataReader["thicknessC"].ToString()),
                        ThicknessL = Convert.ToDecimal(sqlDataReader["thicknessL"].ToString()),
                        
                        PeakLoadC = Convert.ToDecimal(sqlDataReader["peakLoadC"].ToString()),
                        PeakLoadL = Convert.ToDecimal(sqlDataReader["peakLoadL"].ToString()),
                        PeakLoadCL = Convert.ToDecimal(sqlDataReader["peakLoadCL"].ToString()),
                        
                        PeakElongationC = Convert.ToDecimal(sqlDataReader["peakElongationC"].ToString()),
                        PeakElongationL = Convert.ToDecimal(sqlDataReader["peakElongationL"].ToString()),
                        PeakElongationCL = Convert.ToDecimal(sqlDataReader["peakElongationCL"].ToString()),
                        
                        BendingStrengthC = Convert.ToDecimal(sqlDataReader["bendingStrengthC"].ToString()),
                        BendingStrengthL = Convert.ToDecimal(sqlDataReader["bendingStrengthL"].ToString()),
                        BendingStrengthCL = Convert.ToDecimal(sqlDataReader["bendingStrengthCL"].ToString()),

                        AreaUnderCurveC = Convert.ToDecimal(sqlDataReader["areaUnderCurveC"].ToString()),
                        AreaUnderCurveL = Convert.ToDecimal(sqlDataReader["areaUnderCurveL"].ToString()),
                        AreaUnderCurveCL = Convert.ToDecimal(sqlDataReader["areaUnderCurveCL"].ToString()),
                        
                        Denisty = Convert.ToDecimal(sqlDataReader["Denisty"].ToString()),
                        WC = Convert.ToDecimal(sqlDataReader["WC"].ToString()),
                        WA = Convert.ToDecimal(sqlDataReader["WA"].ToString()),
                        DimentioC = Convert.ToDecimal(sqlDataReader["DimentionC"].ToString()),
                        DimentioL = Convert.ToDecimal(sqlDataReader["DimentionL"].ToString())



                    });
                }
            }
            else
            {
                arrBS.Add(new BendingStrength());
            }
            return arrBS;

        }

        private string QuerypemantauanRountineTest(string pilihan, string line, string tDari, string tSampai) 
        {

            string pilihanLine = string.Empty;
            string pilihanFormula = string.Empty;
            pilihanLine = (line == "-- Pilih --") ? "" : " and bp.PlantID = '" + line + "'";
            pilihanFormula = (pilihan == "-- Pilih --") ? "" : " and bt.formula = '" + pilihan + "'";
            return
            "select distinct " +
            "bt.ID,CONVERT(varchar,bt.tgl_prod,105)as TglProd,bt.formula as Formula,"+
            "isnull(bt.bk,0) as BK,"+
            "isnull(bt.t,0) as t,"+
            "isnull(bt.l,0) as l,"+
            "isnull(bt.p,0) as p,"+
            "isnull(((bt.t) * (bt.l) * (bt.p) / 1000),0) as V, "+
            "isnull(((bt.bk) / ((bt.t) * (bt.l) * (bt.p) / 1000)),0) as Denisty,"+
            "isnull(bt.ba,0) as BA,"+
            "isnull(bt.bk,0) as BKW,"+
            "isnull((bt.ba-bt.bk )/ bt.bk * 100,0) as WC,"+
            "isnull(bt.bb,0) as BB,"+
            "isnull(bt.bk2,0) as BKWA,"+
            "isnull((bt.bb-bt.bk2)/bt.bk2 * 100,0) as WA,"+
            "isnull(bt.lbc,0) as LBC,"+
            "isnull(bt.lbl,0) as LBL,"+
            "isnull(bt.lkc,0) LKC,"+
            "isnull(bt.lkl,0) LKL,"+
            "isnull((bt.lbc-bt.lkc)/bt.lkc * 100,0) As DimentionC," +
            "isnull((bt.lbl-bt.lkl)/bt.lkl * 100,0) as DimentionL "+
            "from "+
            "BS_Rountine_Test "+ 
            "bt "+
            "left join BS_Production_Testing_Report bptr on bt.tgl_prod=bptr.tgl_prod and bt.formula = bptr.formula " +
            "left join BM_PlantGroup bp on bptr.group_produksi = bp.[Group] and bptr.id = bt.id_prod " +
            "where bt.RowStatus >-1 "+
            //"and CONVERT(varchar,bt.tgl_prod,105) between '" + tDari + "' and '" + tSampai + "' " + pilihanFormula + " " + pilihanLine + " order by TglProd asc ";
            "and bt.tgl_prod between '" + tDari + "' and '" + tSampai + "' " + pilihanFormula + " " + pilihanLine + " order by TglProd asc ";
            
        }

        public string groupReportProduksi(string jenisReportProduksi)
        {

            string hasil = string.Empty;
            hasil = (jenisReportProduksi == "-- Pilih --") ? "''" : "'" + jenisReportProduksi + "'";
            string strsql = "select GroupReport from BS_Group where jenis  ='" + jenisReportProduksi + "' ";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strsql);
            if (da.Error == string.Empty || sdr.HasRows)
            {
                while (sdr.Read())
                {

                    hasil = sdr["GroupReport"].ToString();
                }
            }
            return hasil;
        }

        private string QuerypemantauanProductionTest(string pilihLine, string pilihPressing,string tDari, string tSampai) 
        {
            string pilihanLine = string.Empty;
            string pilihanPressing = string.Empty;
            

            pilihanLine = (pilihLine == "-- Pilih --") ? "" : " and line = '" + pilihLine + "'";
            pilihanPressing = (pilihPressing == "''") ? "" : " and jenis_produksi in (" + pilihPressing + ")";
            
            return
            ";with r as ( " +
            "select bt.id,bt.tgl_prod,bp.plantID as line,bt.formula,bt.group_produksi," +
            "bt.jenis_produksi,bt.thicknessC,bt.thicknessL," +
            "bt.peakLoadC,bt.peakLoadL, ((bt.peakLoadL)/(bt.peakLoadC) * 100) PeakLoadCL," +
            "bt.peakElongationC,bt.peakElongationL,((bt.peakElongationL)/(bt.peakElongationC)*100) peakElongationCL," +
            "bt.bendingStrengthC,bt.bendingStrengthL,((bt.bendingStrengthL)/(bt.bendingStrengthC)*100) bendingStrengthCL," +
            "bt.areaUnderCurveC,bt.areaUnderCurveL, (bt.areaUnderCurveL + bt.areaUnderCurveC) areaUnderCurveCL, " +
            "isnull((br.bk)/((br.t)*(br.l)*(br.p)/1000),0) Denisty, " +
            "isnull(((br.ba - br.bk)/br.bk * 100),0) WC, "+
            "isnull(((br.bb-br.bk2)/br.bk2 * 100),0) WA, "+
            "isnull(((br.lbc-br.lkc)/br.lkc * 100),0) DimentionC, "+
            "isnull(((br.lbl - br.lkl)/br.lkl * 100),0) DimentionL "+
            "from BS_Production_Testing_Report bt left join BS_Rountine_Test br on bt.tgl_prod = br.tgl_prod and bt.formula = br.formula and br.id_prod = bt.ID " +
            "left join BM_PlantGroup bp on bp.[Group]=bt.group_produksi " +
            "where bt.tgl_prod between '" + tDari + "' and '" + tSampai + "')" +

            "select id id,CONVERT(varchar,tgl_prod,105) tgl_prod,line,formula,group_produksi,jenis_produksi, " +
            "thicknessC,thicknessL, "+
            "peakLoadC,PeakLoadL,PeakLoadCL, "+
            "peakElongationC,peakElongationL,peakElongationCL, "+
            "bendingStrengthC,bendingStrengthL,bendingStrengthCL, "+
            "areaUnderCurveC,areaUnderCurveL,areaUnderCurveCL, "+
            "Denisty,WC,WA, "+
            "DimentionC,DimentionL "+
            "from r where tgl_prod between '" + tDari + "' and '" + tSampai + "' " + pilihanLine + " " + pilihanPressing + " " +

            "union all " +

            "select " +
            "'' ID," +
            "'AVERAGE' TglProd," +
            "'' line, "+
            "'' formula," +
            "'' group_produksi," +
            "'' jenis_produkasi," +
            "isnull(AVG(thicknessC),0) ThicknessC," +
            "isnull(AVG(thicknessL),0) ThicknessL," +
            "isnull(AVG(peakLoadC),0) PeakLoadC," +
            "isnull(AVG(peakLoadL),0) PeakLoadL," +
            "isnull(AVG(PeakLoadCL),0) PeakLoadCL," +
            "isnull(AVG(peakElongationC),0) PeakEloangatinC," +
            "isnull(AVG(peakElongationL),0) peakElongationL," +
            "isnull(AVG(peakElongationCL),0) peakElongationCL," +
            "isnull(AVG(bendingStrengthC),0) bendingStrengthC," +
            "isnull(AVG(bendingStrengthL),0) bendingStrengthL," +
            "isnull(AVG(bendingStrengthCL),0) bendingStrengthCL," +
            "isnull(AVG(areaUnderCurveC),0) areaUnderCurveC," +
            "isnull(AVG(areaUnderCurveL),0) areaUnderCurveL," +
            "isnull(AVG(areaUnderCurveCL),0) areaUnderCurveCL," +
            "isnull(AVG(Denisty),0) Denisty," +
            "isnull(AVG(WC),0) WC," +
            "isnull(AVG(WA),0) WA," +
            "isnull(AVG(DimentionC),0) DimentionC," +
            "isnull(AVG(DimentionL),0) DimentionL " +
            "from r where tgl_prod between '" + tDari + "' and '" + tSampai + "' " + pilihanLine + " " + pilihanPressing + " " +

            "union all " +

            "select " +
            "'' ID," +
            "'STDEV' TglProd," +
            "'' line, " +
            "'' formula," +
            "'' group_produksi," +
            "'' jenis_produkasi," +
            "isnull(STDEV(thicknessC),0) ThicknessC," +
            "isnull(STDEV(thicknessL),0) ThicknessL," +
            "isnull(STDEV(peakLoadC),0) PeakLoadC," +
            "isnull(STDEV(peakLoadL),0) PeakLoadL," +
            "isnull(STDEV(PeakLoadCL),0) PeakLoadCL," +
            "isnull(STDEV(peakElongationC),0) PeakEloangatinC," +
            "isnull(STDEV(peakElongationL),0) peakElongationL," +
            "isnull(STDEV(peakElongationCL),0) peakElongationCL," +
            "isnull(STDEV(bendingStrengthC),0) bendingStrengthC," +
            "isnull(STDEV(bendingStrengthL),0) bendingStrengthL," +
            "isnull(STDEV(bendingStrengthCL),0) bendingStrengthCL," +
            "isnull(STDEV(areaUnderCurveC),0) areaUnderCurveC," +
            "isnull(STDEV(areaUnderCurveL),0) areaUnderCurveL," +
            "isnull(STDEV(areaUnderCurveCL),0) areaUnderCurveCL," +
            "isnull(STDEV(Denisty),0) Denisty," +
            "isnull(STDEV(WC),0) WC," +
            "isnull(STDEV(WA),0) WA," +
            "isnull(STDEV(DimentionC),0) DimentionC," +
            "isnull(STDEV(DimentionL),0) DimentionL " +
            "from r where tgl_prod between '" + tDari + "' and '" + tSampai + "' " + pilihanLine + " " + pilihanPressing + " " +

            "union all " +
            "select " +
            "'' ID, " +
            "'MIN' TglProd," +
            "'' line, " +
            "'' formula," +
            "'' group_produksi," +
            "'' jenis_produkasi," +
            "isnull(MIN(thicknessC),0) ThicknessC," +
            "isnull(MIN(thicknessL),0) ThicknessL," +
            "isnull(MIN(peakLoadC),0) PeakLoadC," +
            "isnull(MIN(peakLoadL),0) PeakLoadL," +
            "isnull(MIN(PeakLoadCL),0) PeakLoadCL," +
            "isnull(MIN(peakElongationC),0) PeakEloangatinC," +
            "isnull(MIN(peakElongationL),0) peakElongationL," +
            "isnull(MIN(peakElongationCL),0) peakElongationCL," +
            "isnull(MIN(bendingStrengthC),0) bendingStrengthC, " +
            "isnull(MIN(bendingStrengthL),0) bendingStrengthL, " +
            "isnull(MIN(bendingStrengthCL),0) bendingStrengthCL, " +
            "isnull(MIN(areaUnderCurveC),0) areaUnderCurveC, " +
            "isnull(MIN(areaUnderCurveL),0) areaUnderCurveL, " +
            "isnull(MIN(areaUnderCurveCL),0) areaUnderCurveCL, " +
            "isnull(MIN(Denisty),0) Denisty, " +
            "isnull(MIN(WC),0) WC, " +
            "isnull(MIN(WA),0) WA," +
            "isnull(MIN(DimentionC),0) DimentionC, " +
            "isnull(MIN(DimentionL),0) DimentionL " +
            "from r where tgl_prod between '" + tDari + "' and '" + tSampai + "' " + pilihanLine + " " + pilihanPressing + " " +

            "union all " +

            "select " +
            "'' ID," +
            "'MAX' TglProd," +
            "'' line, " +
            "'' formula," +
            "'' group_produksi," +
            "'' jenis_produkasi," +
            "isnull(MAX(thicknessC),0) ThicknessC," +
            "isnull(MAX(thicknessL),0) ThicknessL," +
            "isnull(MAX(peakLoadC),0) PeakLoadC," +
            "isnull(MAX(peakLoadL),0) PeakLoadL," +
            "isnull(MAX(PeakLoadCL),0) PeakLoadCL," +
            "isnull(MAX(peakElongationC),0) PeakEloangatinC," +
            "isnull(MAX(peakElongationL),0) peakElongationL," +
            "isnull(MAX(peakElongationCL),0) peakElongationCL," +
            "isnull(MAX(bendingStrengthC),0) bendingStrengthC," +
            "isnull(MAX(bendingStrengthL),0) bendingStrengthL," +
            "isnull(MAX(bendingStrengthCL),0) bendingStrengthCL," +
            "isnull(MAX(areaUnderCurveC),0) areaUnderCurveC," +
            "isnull(MAX(areaUnderCurveL),0) areaUnderCurveL," +
            "isnull(MAX(areaUnderCurveCL),0) areaUnderCurveCL," +
            "isnull(MAX(Denisty),0) Denisty, " +
            "isnull(MAX(WC),0) WC, " +
            "isnull(MAX(WA),0) WA, " +
            "isnull(MAX(DimentionC),0) DimentionC, " +
            "isnull(MAX(DimentionL),0) DimentionL " +
            "from r where tgl_prod between '" + tDari + "' and '" + tSampai + "' " + pilihanLine + " " + pilihanPressing + " order by tgl_prod asc ";
        }


        public string cekIDFormulaProduksi(string formula)
        {
            string formulaProd = string.Empty;

            string strsql = "select formula from BS_Production_Testing_Report where  RowStatus > -1 and  ID='"+ formula +"'";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strsql);

            if (da.Error == string.Empty || sdr.HasRows)
            {
                while (sdr.Read())
                {
                    formulaProd = sdr["formula"].ToString();

                }
            }

            return formulaProd;
        }

        public string cekIDFormulaProduksiTanggal(string formula)
        {
            string tglProduksi = string.Empty;

            string strsql = "select CONVERT(varchar,tgl_prod,105) tglProd from BS_Production_Testing_Report where  RowStatus > -1 and  ID='"+formula+"'";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strsql);

            if (da.Error == string.Empty || sdr.HasRows)
            {
                while (sdr.Read())
                {
                    tglProduksi = sdr["tglProd"].ToString();

                }
            }

            return tglProduksi;
        }

        

        public BendingStrength GenerateObject2(SqlDataReader sqlDataReader)
        {
            objBS = new BendingStrength();
            objBS.FormulaID = Convert.ToInt32(sqlDataReader["ID"]);
            objBS.Tanggal = sqlDataReader["TglProd"].ToString();
            objBS.Formula = sqlDataReader["Formula"].ToString();
            //objBS.FormulaName  = sqlDataReader["FormulaName"].ToString();
            objBS.GroupProduksi = sqlDataReader["group_produksi"].ToString();
            objBS.JenisProduksi = sqlDataReader["jenis_produksi"].ToString();
            

            return objBS;
        }

        public BendingStrength GenerateObject8(SqlDataReader sqlDataReader)
        {
            objBS = new BendingStrength();
            objBS.FormulaID = Convert.ToInt32(sqlDataReader["ID"]);
            objBS.FormulaName  = sqlDataReader["FormulaName"].ToString();
            
            return objBS;
        }


        public BendingStrength GenerateObject3(SqlDataReader sqlDataReader)
        {
            objBS = new BendingStrength();
            objBS.FormulaID = Convert.ToInt32(sqlDataReader["ID"]);
            objBS.Group = sqlDataReader["Group"].ToString();

            return objBS;
        }

        public BendingStrength GenerateObject4(SqlDataReader sqlDataReader)
        {
            objBS = new BendingStrength();
            objBS.Kode = sqlDataReader["Kode"].ToString();

            return objBS;
        }

        public BendingStrength GenerateObject5(SqlDataReader sqlDataReader)
        {
            objBS = new BendingStrength();
            objBS.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objBS.Line =Convert.ToInt32 (sqlDataReader["Line"].ToString());

            return objBS;
        }

        public BendingStrength GenerateObject6(SqlDataReader sqlDataReader)
        {
            objBS = new BendingStrength();
            objBS.ID = Convert.ToInt32(sqlDataReader["ID"]);
            objBS.Jenis = sqlDataReader["Jenis"].ToString();
            objBS.GroupReport = sqlDataReader["GroupReport"].ToString();

            return objBS;
        }

    }
}
