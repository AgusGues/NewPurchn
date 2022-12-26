using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using Domain;

namespace BusinessFacade
{
    public class ISO_PES:AbstractFacade
    {
        public ISO_PES()
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
        public override ArrayList Retrieve()
        {
            throw new NotImplementedException();
        }
        public string PESTipe { get; set; }
        public string Semester { get; set; }
        public int Tahun { get; set; }
        public string Criteria { get; set; }
        public string PICName { get; set; }
        public int DeptID { get; set; }
        public int BagianID { get; set; }
        private string Periode()
        {
            string result = "";
            switch (Semester)
            {
                case "1":
                    result= " AND MONTH(ik.TglMulai) BETWEEN 1 AND 6 AND YEAR(ik.TglMulai)=" + this.Tahun;
                    break;
                case "2":
                    result = " AND MONTH(ik.TglMulai) BETWEEN 7 AND 12 AND YEAR(ik.TglMulai)=" + this.Tahun;
                    break;
                case "12":
                    result = " AND MONTH(ik.TglMulai) BETWEEN 1 AND 12 AND YEAR(ik.TglMulai)=" + this.Tahun;
                    break;
            }
            return result;
        }
        private string TaskPeriod()
        {
            string result = "";
            switch (Semester)
            {
                case "1": result = this.Tahun + "06"; break;
                case "2": result = this.Tahun + "12"; break;
                default: result = this.Tahun + "12"; break;
            }
            return result;
        }
        private string Periode(bool New)
        {
            string result = "";
            if (New == true)
            {
                switch (Semester)
                {
                    case "1": result = "1,6," + this.Tahun; break;
                    case "2": result = "7,12," + this.Tahun; break;
                    case "12": result = "1,12," + this.Tahun; break;
                }
            }
            else
            {
                switch (Semester)
                {
                    case "1": result = this.Tahun + "0101','" + this.Tahun + "0630"; break;
                    case "2": result = this.Tahun + "0701','" + this.Tahun + "1231"; break;
                    case "12": result = this.Tahun + "0101','" + this.Tahun + "1231"; break;
                }
            }
            return result;
        }
        
        private string Query()
        {
            string result = "WITH ISOKPI AS( " +
                            "SELECT ik.PIC, ik.ID,ik.TglMulai,ik.KPIName,ik.DeptID,ik.BagianID,ik.CategoryID,ik.NilaiBobot,ikd.KPIID,ikd.KetTargetKe, " +
                            "ikd.SopScoreID,ius.Bobot,iks.PointNilai,ikd.Rebobot,ius.Penilaian,ikd.Approval,ik.Status,ik.ISO_UserID " +
                            "FROM ISO_KPI ik " +
                            "LEFT JOIN ISO_KPIDetail ikd ON ikd.KPIID=ik.ID " +
                            "LEFT JOIN ISO_SOPScore iks ON iks.ID=ikd.SopScoreID " +
                            "WHERE PIC ='" + this.PICName + "' " + this.Periode() +
                            " AND ik.RowStatus>-1 AND ikd.RowStatus>-1 " +
                            ")";
            result += ", ISOKPI_1 AS ( " +
                     "SELECT PIC,ISO_UserID, BagianID, MONTH(TglMulai)Bulan,YEAR(TglMulai)Tahun,CategoryID,KPIName,(Bobot*100)Bobot, " +
                     "CASE WHEN (SELECT SUM(Rebobot) FROM ISOKPI ikp WHERE ikp.CategoryID=isp.CategoryID GROUP BY ikp.CategoryID)>0  THEN " +
                     "    CASE Penilaian WHEN 6 THEN " +
                     "         (SELECT SUM(PointNilai) FROM ISOKPI ikp WHERE ikp.CategoryID=isp.CategoryID AND ikp.Rebobot>0 GROUP BY CategoryID)/ " +
                     "         (SELECT SUM(Rebobot) FROM ISOKPI ikp WHERE ikp.CategoryID=isp.CategoryID GROUP BY ikp.CategoryID) " +
                     "         WHEN 12 THEN " +
                     "         (SELECT SUM(PointNilai) FROM ISOKPI ikp WHERE ikp.CategoryID=isp.CategoryID AND ikp.Rebobot>0 GROUP BY CategoryID)/ " +
                     "         (SELECT SUM(Rebobot) FROM ISOKPI ikp WHERE ikp.CategoryID=isp.CategoryID GROUP BY ikp.CategoryID) " +
                     "    END " +
                     "ELSE ISNULL(PointNilai,0) END " +
                     "PointNilai,Rebobot,Penilaian,Approval  " +
                     "FROM ISOKPI isp " +
                     ")  " +
                     ",ISOKPI_2 AS( " +
                     "SELECT PIC, BagianID, CategoryID,KPIName,Bobot,Tahun, " +
                     "    SUM(ISNULL([1],0))Jan, " +
                     "    SUM(ISNULL([2],0))Feb, " +
                     "    SUM(ISNULL([3],0))Mar, " +
                     "    SUM(ISNULL([4],0))Apr, " +
                     "    SUM(ISNULL([5],0))Mei, " +
                     "    SUM(ISNULL([6],0))Jun, " +
                     "    SUM(ISNULL([7],0))Jul, " +
                     "    SUM(ISNULL([8],0))Ags, " +
                     "    SUM(ISNULL([9],0))Sep, " +
                     "    SUM(ISNULL([10],0))Okt, " +
                     "    SUM(ISNULL([11],0))Nov, " +
                     "    SUM(ISNULL([12],0))Desm,Penilaian FROM ISOKPI_1 " +
                     "PIVOT (SUM(PointNilai) FOR Bulan IN([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12]))as x " +
                     "GROUP BY PIC, BagianID, x.CategoryID,x.KPIName,x.Bobot,x.Tahun,x.Penilaian " +
                     ") " +
                     ",ISOKPI_3 AS ( " +
                     "SELECT PIC, BagianID,CategoryiD,KPIName,Tahun,Bobot, " +
                     "      Jan JanP,(Jan * Bobot/100)JanN,Feb FebP,(Feb * Bobot/100)FebN,Mar MarP,(Mar * Bobot/100)MarN,Apr AprP,(Apr * Bobot/100)AprN, " +
                     "      Mei MeiP,(Mei * Bobot/100)MeiN,Jun JunP,(Jun * Bobot/100)JunN,Jul JulP,(Jul * Bobot/100)JulN,Ags AgsP,(Ags * Bobot/100)AgsN, " +
                     "      Sep SepP,(Sep * Bobot/100)SepN,Okt OktP,(Okt * Bobot/100)OktN,Nov NovP,(Nov * Bobot/100)NovN,Desm DesmP,(Desm * Bobot/100)DesmN, " +
                     "      Penilaian " +
                     "FROM ISOKPI_2) " +
                     ",ISOKPI_4 AS( " +
                     "SELECT *, " +
                     "    CASE WHEN Penilaian <7 THEN  " +
                     "        ((JanN+FebN+MarN+AprN+MeiN+JunN)/6) ELSE ((JanN+FebN+MarN+AprN+MeiN+JunN)+(JulN+AgsN+SepN+OktN+NovN+DesmN)) END Smt1, " +
                     "    CASE WHEN Penilaian <7 THEN ((JulN+AgsN+SepN+OktN+NovN+DesmN)/6) ELSE ((JanN+FebN+MarN+AprN+MeiN+JunN)+(JulN+AgsN+SepN+OktN+NovN+DesmN)) END Smt2 "+
                     "FROM ISOKPI_3 " +
                     ") " +
                     "SELECT *,(Smt1+smt2)/2 Tahunan FROM ISOKPI_4 " +
                     "ORDER By BagianID, KPIName,Tahun";
            return result;
        }
        private string QueryPES()
        {
            string result = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Q1]') AND type in (N'U')) DROP TABLE [dbo].[Q1] " +
                          " DECLARE @task INT;DECLARE @kpi INT;DECLARE @sop INT; DECLARE @disp INT  "+
                          "  CREATE Table Q1  " +
                          "  ( " +
                          "      PIC varchar(100) NULL,DeptID int NULL,	BagianID int NULL,	CategoryID varchar NULL,KPIName varchar(max),Tahun int,Bobot decimal(18,2), " +
                          "      JanP decimal(18,2),	JanN decimal(18,2),	FebP decimal(18,2),	FebN decimal(18,2),	MarP decimal(18,2),	MarN decimal(18,2), " +
                          "      AprP decimal(18,2),	AprN decimal(18,2),	MeiP decimal(18,2),	MeiN decimal(18,2),	JunP decimal(18,2),	JunN decimal(18,2), " +
                          "      JulP decimal(18,2),	JulN decimal(18,2),	AgsP decimal(18,2),	AgsN decimal(18,2),	SepP decimal(18,2),	SepN decimal(18,2), " +
                          "      OktP decimal(18,2),	OktN decimal(18,2),	NovP decimal(18,2),	NovN decimal(18,2),	DesP decimal(18,2),	DesN decimal(18,2), " +
                          "      JanB decimal(18,2), FebB decimal(18,2), MarB decimal(18,2), AprB decimal(18,2), MeiB decimal(18,2), JunB decimal(18,2), " +
                          "      JulB decimal(18,2), AgsB decimal(18,2), SepB decimal(18,2), OktB decimal(18,2), NovB decimal(18,2), DesB decimal(18,2),Approval int  " +
                          "      ,Penilaian int,bulan varchar(max),ThnMulai int,Urutan int " +
                          "      ) SET ANSI_WARNINGS OFF " +
                          "  INSERT INTO Q1 exec dbo.RekapPES_KPI '" + this.PICName + "'," + this.Periode(true) + ",'true' " +
                          "  INSERT INTO Q1 exec dbo.RekapPES_SOP '" + this.PICName + "'," + this.Periode(true) + ",'true' " +
                          "  INSERT INTO Q1 exec dbo.RekapDisiplin '" + this.PICName + "'," + this.DeptID + "," + this.BagianID + "," + this.Tahun +
                          "  INSERT INTO Q1 exec dbo.RekapTask '" + this.PICName + "'," + this.DeptID + "," + this.BagianID + ",'" + this.Periode(false) + "', " + this.Tahun +
                          "  SET @task =(Select Count(CategoryID) From Q1 Where CategoryID=2) "+
                          "  IF @task=0 BEGIN INSERT INTO Q1 (KPIName,PIC,DeptID,BagianID,CategoryID)VALUES('TASK','" + this.PICName + "'," + this.DeptID + "," + this.BagianID + ",2) END " +
                          "  SET @kpi =(Select Count(CategoryID) From Q1 Where CategoryID=1) "+
                          "  IF @kpi=0 BEGIN INSERT INTO Q1 (KPIName,PIC,DeptID,BagianID,CategoryID)VALUES('KPI','" + this.PICName + "'," + this.DeptID + "," + this.BagianID + ",1) END " +
                          "  SET @sop =(Select Count(CategoryID) From Q1 Where CategoryID=3) "+
                          "  IF @sop=0 BEGIN INSERT INTO Q1 (KPIName,PIC,DeptID,BagianID,CategoryID)VALUES('SOP','" + this.PICName + "'," + this.DeptID + "," + this.BagianID + ",3) END " +
                          "  SET @disp =(Select Count(CategoryID) From Q1 Where CategoryID=4) " +
                          "  IF @disp=0 BEGIN INSERT INTO Q1 (KPIName,PIC,DeptID,BagianID,CategoryID)VALUES('DISIPLIN','" + this.PICName + "'," + this.DeptID + "," + this.BagianID + ",4) END " +
                          " SELECT xx.*," +
                          " CASE WHEN xx.ID=2 THEN (SELECT dbo.GetNilaiTask('" + this.PICName + "'," + this.DeptID + "," + this.BagianID + ",'" + this.Periode(false) + "',1,1))" +
                          "   ELSE ((JanT+FebT+MarT+AprT+MeiT+JunT))END Total1, " +
                          "  CASE WHEN xx.ID=2 THEN (SELECT dbo.GetNilaiTask('" + this.PICName + "'," + this.DeptID + "," + this.BagianID + ",'" + this.Periode(false) + "',2,1)) ELSE " +
                          "  ((JulT+AgsT+SepT+OktT+NovT+DesT)) END Total2, " +
                          " CASE WHEN xx.ID=2 THEN (SELECT dbo.GetNilaiTask('" + this.PICName + "'," + this.DeptID + "," + this.BagianID + ",'" + this.Periode(false) + "',1,2)) ELSE 0 END NilaiBobot1," +
                          " CASE WHEN xx.ID=2 THEN (SELECT dbo.GetNilaiTask('" + this.PICName + "'," + this.DeptID + "," + this.BagianID + ",'" + this.Periode(false) + "',2,2)) ELSE 0 END NilaiBobot2" +
                          " FROM ( " +
                          "  SELECT DISTINCT ip.PESName, " +
                          "  ISNULL(PIC,'" + this.PICName + "')PIC,ISNULL(Q1.DeptID,ib.DeptID)DeptID,ISNULL(Q1.BagianID,ib.ID)BagianID,ip.ID, " +
                          "  /*ISNULL(Case When JulB>0 Then DesB*100 ELSE JunB*100 END,*/((SELECT dbo.GetBobotPES(" + this.DeptID + "," + this.BagianID + ",'"+this.TaskPeriod()+"',ip.ID))*100) Bobot," +
                          "  ISNULL(Tahun," + this.Tahun + ")Tahun,    " +
                          "  ISNULL(JanN,0)JanN,ISNULL(JanB,0)JanB,ISNULL((JanN*JanB),0)JanT, " +
                          "  ISNULL(FebN,0)FebN,ISNULL(FebB,0)FebB,ISNULL((FebN*FebB),0)FebT, " +
                          "  ISNULL(MarN,0)MarN,ISNULL(MarB,0)MarB,ISNULL((MarN*MarB),0)MarT, " +
                          "  ISNULL(AprN,0)AprN,ISNULL(AprB,0)AprB,ISNULL((AprN*AprB),0)AprT, " +
                          "  ISNULL(MeiN,0)MeiN,ISNULL(MeiB,0)MeiB,ISNULL((MeiN*MeiB),0)MeiT, " +
                          "  ISNULL(JunN,0)JunN,ISNULL(JunB,0)JunB,ISNULL((JunN*JunB),0)JunT, " +
                          "  ISNULL(JulN,0)JulN,ISNULL(JulB,0)JulB,ISNULL((JulN*JulB),0)JulT, " +
                          "  ISNULL(AgsN,0)AgsN,ISNULL(AgsB,0)AgsB,ISNULL((AgsN*AgsB),0)AgsT, " +
                          "  ISNULL(SepN,0)SepN,ISNULL(SepB,0)SepB,ISNULL((SepN*SepB),0)SepT, " +
                          "  ISNULL(OktN,0)OktN,ISNULL(OktB,0)OktB,ISNULL((OktN*OktB),0)OktT, " +
                          "  ISNULL(NovN,0)NovN,ISNULL(NovB,0)NovB,ISNULL((NovN*NovB),0)NovT, " +
                          "  ISNULL(DesN,0)DesN,ISNULL(DesB,0)DesB,ISNULL((DesN*DesB),0)DesT, " +
                          "  ib.Urutan,ISNULL(Penilaian,0)Penilaian,ISNULL(Approval,2)Approval " +
                          "  FROM  Q1 " +
                          "  FULL OUTER JOIN ISO_PES ip ON ip.ID=Q1.CategoryID " +
                          //"  LEFT JOIN ISO_BobotPES bp ON bp.PesType=ip.ID "+
                          "  LEFT JOIN ISO_Bagian ib ON ib.ID=Q1.BagianID " +
                          "  WHERE ip.ID in(1,2,3,4) and Q1.BagianID=" + this.BagianID +
                          "  AND ib.RowStatus>-1 " +
                          "  ) as xx " + this.Criteria +
                          "  order by xx.ID,Urutan,PIC,DeptID,BagianID " +
                          "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Q1]') AND type in (N'U')) DROP TABLE [dbo].[Q1]";
            return result;
        }
        private string QueryPIC()
        {
            string result = "WITH PICS AS( " +
                            "SELECT PIC,ISO_UserID,DeptID,BagianID,YEAR(TglMulai)Tahun FROM ISO_KPI ik "+
                            "WHERE RowStatus>-1 " + this.Periode().Replace("YEAR(ik.TglMulai)=", "YEAR(ik.TglMulai)<=") +
                            " Group BY PIC,ISO_UserID,DeptID,BagianID,YEAR(TglMulai)  " +
                            "UNION ALL " +
                            "SELECT PIC,ISO_UserID,DeptID,BagianID,YEAR(TglMulai)Tahun FROM ISO_SOP ik "+
                            " WHERE RowStatus>-1 " + this.Periode().Replace("YEAR(ik.TglMulai)=", "YEAR(ik.TglMulai)<=") +
                            " Group BY PIC,ISO_UserID,DeptID,BagianID,YEAR(TglMulai) " +
                            ") " +
                            ",PICS_1 AS( " +
                            "    SELECT iu.UserID, iu.ID,iu.UserName,Alias DeptName,PICS.DeptID,BagianName,PICS.BagianID,ib.Urutan,Tahun FROM PICS " +
                            "    LEFT JOIN Dept ON Dept.ID=PICS.DeptID " +
                            "    LEFT JOIN ISO_Bagian ib ON ib.ID=PICS.BagianID " +
                            "    LEFT JOIN ISO_Users iu ON iu.ID=PICS.ISO_UserID AND iu.RowStatus>-1 AND iu.DeptID=PICs.DeptID " +
                            "    RIGHT JOIN UserAccount au ON au.UserID=iu.ID " +
                            "    WHERE PIC IS NOT NULL AND Tahun=" + this.Tahun + 
                            ") ";
            return result;
        }
        
        private string QueryDept()
        {
            string result = this.QueryPIC();
            result += "SELECT DeptID,DeptName FROM PICS_1 "+this.Criteria+" GROUP BY DeptID,DeptName ORDER BY DeptName";
            return result;
        }
        private string QueryPIC(bool detail)
        {
            string result = this.QueryPIC();
            result += "SELECT p.UserID,p.ID,p.UserName,P.BagianID,P.DeptID,P.DeptName,p.BagianName,p.Urutan,A.UserName Nama FROM PICS_1 as P " +
                     " LEFT JOIN UserAccount as A ON A.UserID=p.ID AND A.BagianID=p.BagianID " +
                     " WHERE p.BagianID in(SELECT pis.BagianID FROM PICS_1 pis WHERE pis.Tahun=" + this.Tahun + " ) " + this.Criteria +
                     " GROUP BY P.ID,P.UserID,P.UserName,A.UserName,P.DeptName,P.DeptID,P.BagianID,P.Urutan,P.BagianName " +
                     " Order By P.DeptID,A.UserName,Urutan,P.BagianID";
            return result;
        }
        public int MulaiPes()
        {
            int result = 0;
            string sql = "SELECT dbo.fnTglMulaiPES('" + this.PICName + "',"+this.BagianID+") as Mulai";
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = sql;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    return int.Parse(sdr["Mulai"].ToString());
                }
            }
            return result;
        }
        public int MulaiPes(bool EndOfPES)
        {
            int result = 0;
            string sql = "SELECT isnull(dbo.fnTglAkhirPES('" + this.PICName + "'," + this.BagianID + "),0) as Mulai";
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = sql;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    return int.Parse(sdr["Mulai"].ToString());
                }
            }
            return result;
        }
        public ArrayList LoadTahun()
        {
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select Distinct(Year(TglMulai))Tahun From ISO_KPI WHERE Year(TglMulai) IS NOT NULL Order By Year(TglMulai)";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateObjectThn(sdr));
                }
            }
            return arrData;
        }

        private PES2016 GenerateObjectThn(SqlDataReader sdr)
        {
            PES2016 objP = new PES2016();
            objP.Tahun = int.Parse(sdr["Tahun"].ToString());
            return objP;
        }
        public ArrayList LoadDept()
        {
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = this.QueryDept();
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateObject(sdr));
                }
            }
            return arrData;
        }
        public ArrayList LoadDeptRPES(int userid)
        {
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select distinct * from (select distinct DeptID, (select distinct top 1 DeptName from ISO_Dept where DeptID=ISO_SOP.deptid) Deptname "+
                "from ISO_SOP where rowstatus>-1  and ISO_UserID in (select userid from UserAccount where UserID in (select ID from ISO_Users " +
                "where UserID=" + userid + ")) union all " +
                "select distinct DeptID, (select distinct top 1 DeptName from ISO_Dept where DeptID=ISO_SOP.deptid) Deptname "+
                "from ISO_SOP where rowstatus>-1  and DeptID  in (select DeptID  from ISO_Users " +
                "where UserID=" + userid + ")) A";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateObject(sdr));
                }
            }
            return arrData;
        }
        //public ArrayList LoadDept(bool HO)
        //{
        //    //ArrayList arrData = new ArrayList();
            
        //}
        private PES2016 GenerateObject(SqlDataReader sdr)
        {
            PES2016 objP = new PES2016();
            objP.DeptID = int.Parse(sdr["DeptID"].ToString());
            objP.DeptName = sdr["DeptName"].ToString();
            return objP;
        }
        private PES2016 GenerateObject(SqlDataReader sdr, bool pic)
        {
            PES2016 objP = new PES2016();
            objP.ID = int.Parse(sdr["ID"].ToString());
            objP.UserName = sdr["UserName"].ToString();
            objP.UserID = int.Parse(sdr["UserID"].ToString());
            objP.BagianID = int.Parse(sdr["BagianID"].ToString());
            objP.BagianName = sdr["BagianName"].ToString();
            objP.Nama = sdr["Nama"].ToString();
            objP.DeptID = int.Parse(sdr["DeptID"].ToString());
            return objP;
        }

        public ArrayList LoadPIC()
        {
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = this.QueryPIC(true);
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateObject(sdr,true));
                }
            }
            return arrData;
        }
        public ArrayList LoadPES()
        {
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = this.QueryPES();
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateObjectPES(sdr));
                }
            }
            return arrData;
        }
        public ArrayList LoadPES(string PESName, bool detail)
        {
            string typePes = "";
            string strSQL = "declare @maxBobot int " +
                        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Q2]') AND type in (N'U')) DROP TABLE [dbo].[Q2]  " +
                        "CREATE Table Q2    (     " +
                        "PIC varchar(100) NULL, " +
                        "DeptID int,	 " +
                        "BagianID int ,	 " +
                        "CategoryID int , " +
                        "KPIName varchar(max), " +
                        "Tahun int,Bobot decimal(18,2),  " +
                        "JanP decimal(18,2),JanN decimal(18,2),	FebP decimal(18,2),	FebN decimal(18,2),	MarP decimal(18,2),	MarN decimal(18,2), " +
                        "AprP decimal(18,2),AprN decimal(18,2),	MeiP decimal(18,2),	MeiN decimal(18,2),	JunP decimal(18,2),	JunN decimal(18,2), " +
                        "JulP decimal(18,2),JulN decimal(18,2),	AgsP decimal(18,2),	AgsN decimal(18,2),	SepP decimal(18,2),	SepN decimal(18,2), " +
                        "OktP decimal(18,2),OktN decimal(18,2),	NovP decimal(18,2),	NovN decimal(18,2),	DesP decimal(18,2),	DesN decimal(18,2), " +
                        "JanB decimal(18,2), FebB decimal(18,2), MarB decimal(18,2), AprB decimal(18,2), MeiB decimal(18,2), JunB decimal(18,2), " +
                        "JulB decimal(18,2), AgsB decimal(18,2), SepB decimal(18,2), OktB decimal(18,2), NovB decimal(18,2), DesB decimal(18,2), " +
                        "Approval int,Penilaian int,bulan varchar(max),ThnMulai int,Urutan int " +
                        ") ";
            switch (PESName)
            {
                case "KPI":
                    typePes = "ISO_KPI";
                    strSQL += " INSERT INTO Q2 exec dbo.RekapPES_KPI '" + this.PICName + "'," + this.Periode(true) + ",'false' ";
                    strSQL += " INSERT INTO Q2 exec dbo.RekapPES_KPI '" + this.PICName + "'," + this.Periode(true) + ",'true' ";
                    break;
                case "SOP":
                    typePes = "ISO_SOP";
                    strSQL += " INSERT INTO Q2 exec dbo.RekapPES_SOP '" + this.PICName + "'," + this.Periode(true) + ",'false' ";
                    strSQL += " INSERT INTO Q2 exec dbo.RekapPES_SOP '" + this.PICName + "'," + this.Periode(true) + ",'true' ";
                    break;
            }
            strSQL +=
";with q as ( " +
    "select * from ( " +
        "SELECT * " +
        "FROM Q2 WHERE Urutan < 999 " +
        "UNION  ALL " +
        "SELECT PIC, DeptID, BagianID, 3 CategoryID, 'KPI' KPIName, Tahun, SUM(Bobot)Bobot, " +
        "0 JanP, SUM(JanN)JanN, 0 FebP, SUM(FebN)FebN, 0 MarP, SUM(MarN)MarN, " +
        "0 AprP, SUM(AprN)AprN, 0 MeiP, SUM(MeiN)MeiN, 0 JunP, SUM(JunN)JunN, " +
        "0 JulP, SUM(JulN)JulN, 0 AgsP, SUM(AgsN)AgsN, 0 SepP, SUM(SepN)SepN, " +
        "0 OktP, SUM(OktN)OktN, 0 NovP, SUM(NovN)NovN, 0 DesP, SUM(DesN)DesN, " +
        "SUM(JanB)JanB, SUM(FebB)FebB, SUM(MarB)MarB, SUM(AprB)AprB, SUM(MeiB)MeiB, SUM(JunB)JunB, " +
        "SUM(JulB)JulB, SUM(AgsB)AgsB, SUM(SepB)SepB, SUM(OktB)OktB, SUM(NovB)NovB, SUM(DesB)DesB, " +
        "MIN(Approval)Approval, 0 Penilaian, '0' bulan, 0 ThnMulai, 999 Urutan " +
        "FROM Q2 WHERE urutan < 999 " +
        "GROUP By PIC, DeptID, BagianID, Tahun " +
    ") as q1 " +
") " +
"select PIC,DeptID,BagianID,CategoryID,KPIName,Tahun,Bobot, " +
"case when(select COUNT(k.id) from "+typePes+" k where MONTH(k.TglMulai) = 1 and YEAR(k.TglMulai) = q.Tahun and k.PIC = '" + this.PICName + "') > 0 " +
"then cast(q.JanP as char(11)) else '' end JanP, " +
"case when(select COUNT(k.id) from " + typePes + "  k where MONTH(k.TglMulai) = 1 and YEAR(k.TglMulai) = q.Tahun and k.PIC = '" + this.PICName + "') > 0 " +
"then cast(q.JanN as char(11)) else '' end JanN, " +
"case when(select COUNT(k.id) from " + typePes + "  k where MONTH(k.TglMulai) = 2 and YEAR(k.TglMulai) = q.Tahun and k.PIC = '" + this.PICName + "') > 0 " +
"then cast(q.FebP as char(11)) else '' end FebP, " +
"case when(select COUNT(k.id) from " + typePes + "  k where MONTH(k.TglMulai) = 2 and YEAR(k.TglMulai) = q.Tahun and k.PIC = '" + this.PICName + "') > 0 " +
"then cast(q.FebN as char(11)) else '' end FebN, " +
"case when(select COUNT(k.id) from " + typePes + "  k where MONTH(k.TglMulai) = 3 and YEAR(k.TglMulai) = q.Tahun and k.PIC = '" + this.PICName + "') > 0 " +
"then cast(q.MarP as char(11)) else '' end MarP, " +
"case when(select COUNT(k.id) from " + typePes + "  k where MONTH(k.TglMulai) = 3 and YEAR(k.TglMulai) = q.Tahun and k.PIC = '" + this.PICName + "') > 0 " +
"then cast(q.MarN as char(11)) else '' end MarN, " +
"case when(select COUNT(k.id) from " + typePes + "  k where MONTH(k.TglMulai) = 4 and YEAR(k.TglMulai) = q.Tahun and k.PIC = '" + this.PICName + "') > 0 " +
"then cast(q.AprP as char(11)) else '' end AprP, " +
"case when(select COUNT(k.id) from " + typePes + "  k where MONTH(k.TglMulai) = 4 and YEAR(k.TglMulai) = q.Tahun and k.PIC = '" + this.PICName + "') > 0 " +
"then cast(q.AprN as char(11)) else '' end AprN, " +
"case when(select COUNT(k.id) from " + typePes + "  k where MONTH(k.TglMulai) = 5 and YEAR(k.TglMulai) = q.Tahun and k.PIC = '" + this.PICName + "') > 0 " +
"then cast(q.MeiP as char(11)) else '' end MeiP, " +
"case when(select COUNT(k.id) from " + typePes + "  k where MONTH(k.TglMulai) = 5 and YEAR(k.TglMulai) = q.Tahun and k.PIC = '" + this.PICName + "') > 0 " +
"then cast(q.MeiN as char(11)) else '' end MeiN, " +
"case when(select COUNT(k.id) from " + typePes + "  k where MONTH(k.TglMulai) = 6 and YEAR(k.TglMulai) = q.Tahun and k.PIC = '" + this.PICName + "') > 0 " +
"then cast(q.JunP as char(11)) else '' end JunP, " +
"case when(select COUNT(k.id) from " + typePes + "  k where MONTH(k.TglMulai) = 6 and YEAR(k.TglMulai) = q.Tahun and k.PIC = '" + this.PICName + "') > 0 " +
"then cast(q.JunN as char(11)) else '' end JunN, " +
"case when(select COUNT(k.id) from " + typePes + "  k where MONTH(k.TglMulai) = 7 and YEAR(k.TglMulai) = q.Tahun and k.PIC = '" + this.PICName + "') > 0 " +
"then cast(q.JulP as char(11)) else '' end JulP, " +
"case when(select COUNT(k.id) from " + typePes + "  k where MONTH(k.TglMulai) = 7 and YEAR(k.TglMulai) = q.Tahun and k.PIC = '" + this.PICName + "') > 0 " +
"then cast(q.JulN as char(11)) else '' end JulN, " +
"case when(select COUNT(k.id) from " + typePes + "  k where MONTH(k.TglMulai) = 8 and YEAR(k.TglMulai) = q.Tahun and k.PIC = '" + this.PICName + "') > 0 " +
"then cast(q.AgsP as char(11)) else '' end AgsP, " +
"case when(select COUNT(k.id) from " + typePes + "  k where MONTH(k.TglMulai) = 8 and YEAR(k.TglMulai) = q.Tahun and k.PIC = '" + this.PICName + "') > 0 " +
"then cast(q.AgsN as char(11)) else '' end AgsN, " +
"case when(select COUNT(k.id) from " + typePes + "  k where MONTH(k.TglMulai) = 9 and YEAR(k.TglMulai) = q.Tahun and k.PIC = '" + this.PICName + "') > 0 " +
"then cast(q.SepP as char(11)) else '' end SepP, " +
"case when(select COUNT(k.id) from " + typePes + "  k where MONTH(k.TglMulai) = 9 and YEAR(k.TglMulai) = q.Tahun and k.PIC = '" + this.PICName + "') > 0 " +
"then cast(q.SepN as char(11)) else '' end SepN, " +
"case when(select COUNT(k.id) from " + typePes + "  k where MONTH(k.TglMulai) = 10 and YEAR(k.TglMulai) = q.Tahun and k.PIC = '" + this.PICName + "') > 0 " +
"then cast(q.OktP as char(11)) else '' end OktP, " +
"case when(select COUNT(k.id) from " + typePes + "  k where MONTH(k.TglMulai) = 10 and YEAR(k.TglMulai) = q.Tahun and k.PIC = '" + this.PICName + "') > 0 " +
"then cast(q.OktN as char(11)) else '' end OktN, " +
"case when(select COUNT(k.id) from " + typePes + "  k where MONTH(k.TglMulai) = 11 and YEAR(k.TglMulai) = q.Tahun and k.PIC = '" + this.PICName + "') > 0 " +
"then cast(q.NovP as char(11)) else '' end NovP, " +
"case when(select COUNT(k.id) from " + typePes + "  k where MONTH(k.TglMulai) = 11 and YEAR(k.TglMulai) = q.Tahun and k.PIC = '" + this.PICName + "') > 0 " +
"then cast(q.NovN as char(11)) else '' end NovN, " +
"case when(select COUNT(k.id) from " + typePes + "  k where MONTH(k.TglMulai) = 12 and YEAR(k.TglMulai) = q.Tahun and k.PIC = '" + this.PICName + "') > 0 " +
"then cast(q.DesP as char(11)) else '' end DesP, " +
"case when(select COUNT(k.id) from " + typePes + "  k where MONTH(k.TglMulai) = 12 and YEAR(k.TglMulai) = q.Tahun and k.PIC = '" + this.PICName + "') > 0 " +
"then cast(q.DesN as char(11)) else '' end DesN, " +
"case when(select COUNT(k.id) from " + typePes + "  k where MONTH(k.TglMulai) = 1 and YEAR(k.TglMulai) = q.Tahun and k.PIC = '" + this.PICName + "') > 0 " +
"then cast(q.JanB as char(11)) else '' end JanB, " +
"case when(select COUNT(k.id) from " + typePes + "  k where MONTH(k.TglMulai) = 2 and YEAR(k.TglMulai) = q.Tahun and k.PIC = '" + this.PICName + "') > 0 " +
"then cast(q.FebB as char(11)) else '' end FebB, " +
"case when(select COUNT(k.id) from " + typePes + "  k where MONTH(k.TglMulai) = 3 and YEAR(k.TglMulai) = q.Tahun and k.PIC = '" + this.PICName + "') > 0 " +
"then cast(q.MarB as char(11)) else '' end MarB, " +
"case when(select COUNT(k.id) from " + typePes + "  k where MONTH(k.TglMulai) = 4 and YEAR(k.TglMulai) = q.Tahun and k.PIC = '" + this.PICName + "') > 0 " +
"then cast(q.AprB as char(11)) else '' end AprB, " +
"case when(select COUNT(k.id) from " + typePes + "  k where MONTH(k.TglMulai) = 5 and YEAR(k.TglMulai) = q.Tahun and k.PIC = '" + this.PICName + "') > 0 " +
"then cast(q.MeiB as char(11)) else '' end MeiB, " +
"case when(select COUNT(k.id) from " + typePes + "  k where MONTH(k.TglMulai) = 6 and YEAR(k.TglMulai) = q.Tahun and k.PIC = '" + this.PICName + "') > 0 " +
"then cast(q.JunB as char(11)) else '' end JunB, " +
"case when(select COUNT(k.id) from " + typePes + "  k where MONTH(k.TglMulai) = 7 and YEAR(k.TglMulai) = q.Tahun and k.PIC = '" + this.PICName + "') > 0 " +
"then cast(q.JulB as char(11)) else '' end JulB, " +
"case when(select COUNT(k.id) from " + typePes + "  k where MONTH(k.TglMulai) = 8 and YEAR(k.TglMulai) = q.Tahun and k.PIC = '" + this.PICName + "') > 0 " +
"then cast(q.AgsB as char(11)) else '' end AgsB, " +
"case when(select COUNT(k.id) from " + typePes + "  k where MONTH(k.TglMulai) = 9 and YEAR(k.TglMulai) = q.Tahun and k.PIC = '" + this.PICName + "') > 0 " +
"then cast(q.SepB as char(11)) else '' end SepB, " +
"case when(select COUNT(k.id) from " + typePes + "  k where MONTH(k.TglMulai) = 10 and YEAR(k.TglMulai) = q.Tahun and k.PIC = '" + this.PICName + "') > 0 " +
"then cast(q.OktB as char(11)) else '' end OktB, " +
"case when(select COUNT(k.id) from " + typePes + "  k where MONTH(k.TglMulai) = 11 and YEAR(k.TglMulai) = q.Tahun and k.PIC = '" + this.PICName + "') > 0 " +
"then cast(q.NovB as char(11)) else '' end NovB, " +
"case when(select COUNT(k.id) from " + typePes + "  k where MONTH(k.TglMulai) = 12 and YEAR(k.TglMulai) = q.Tahun and k.PIC = '" + this.PICName + "') > 0 " +
"then cast(q.DesB as char(11)) else '' end DesB, " +
"Approval, Penilaian, case when bulan='' then '0' else bulan end bulan, ThnMulai, Urutan " +
"from q ORDER BY Urutan " +
"IF  EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Q2]') AND type in (N'U')) DROP TABLE[dbo].[Q2]";
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = strSQL;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateObjectPES(sdr,true));
                }
            }
            return arrData;
        }
        private PES2016 GenerateObjectPES(SqlDataReader sdr)
        {
            PES2016 objP = new PES2016();
            objP.PesType = (sdr["ID"]== DBNull.Value)?0: int.Parse(sdr["ID"].ToString());
            objP.PESName = sdr["PESName"].ToString();
            objP.Bobot = (sdr["Bobot"] == DBNull.Value) ? 0 : decimal.Parse(sdr["Bobot"].ToString());
            objP.Tahun = (sdr["Tahun"] == DBNull.Value) ? 0 : int.Parse(sdr["Tahun"].ToString());
            objP.Jan = (sdr["JanN"] == DBNull.Value) ? 0 : decimal.Parse(sdr["JanN"].ToString());
            objP.Feb = (sdr["FebN"] == DBNull.Value) ? 0 : decimal.Parse(sdr["FebN"].ToString());
            objP.Mar = (sdr["MarN"] == DBNull.Value) ? 0 : decimal.Parse(sdr["MarN"].ToString());
            objP.Apr = (sdr["AprN"] == DBNull.Value) ? 0 : decimal.Parse(sdr["AprN"].ToString());
            objP.Mei = (sdr["MeiN"] == DBNull.Value) ? 0 : decimal.Parse(sdr["MeiN"].ToString());
            objP.Jun = (sdr["JunN"] == DBNull.Value) ? 0 : decimal.Parse(sdr["JunN"].ToString());
            objP.Jul = (sdr["JulN"] == DBNull.Value) ? 0 : decimal.Parse(sdr["JulN"].ToString());
            objP.Ags = (sdr["AgsN"] == DBNull.Value) ? 0 : decimal.Parse(sdr["AgsN"].ToString());
            objP.Sep = (sdr["SepN"] == DBNull.Value) ? 0 : decimal.Parse(sdr["SepN"].ToString());
            objP.Okt = (sdr["OktN"] == DBNull.Value) ? 0 : decimal.Parse(sdr["OktN"].ToString());
            objP.Nop = (sdr["NovN"] == DBNull.Value) ? 0 : decimal.Parse(sdr["NovN"].ToString());
            objP.Des = (sdr["DesN"] == DBNull.Value) ? 0 : decimal.Parse(sdr["DesN"].ToString());

            objP.JanN = (sdr["JanT"] == DBNull.Value) ? 0 : decimal.Parse(sdr["JanT"].ToString());
            objP.FebN = (sdr["FebT"] == DBNull.Value) ? 0 : decimal.Parse(sdr["FebT"].ToString());
            objP.MarN = (sdr["MarT"] == DBNull.Value) ? 0 : decimal.Parse(sdr["MarT"].ToString());
            objP.AprN = (sdr["AprT"] == DBNull.Value) ? 0 : decimal.Parse(sdr["AprT"].ToString());
            objP.MeiN = (sdr["MeiT"] == DBNull.Value) ? 0 : decimal.Parse(sdr["MeiT"].ToString());
            objP.JunN = (sdr["JunT"] == DBNull.Value) ? 0 : decimal.Parse(sdr["JunT"].ToString());
            objP.JulN = (sdr["JulT"] == DBNull.Value) ? 0 : decimal.Parse(sdr["JulT"].ToString());
            objP.AgsN = (sdr["AgsT"] == DBNull.Value) ? 0 : decimal.Parse(sdr["AgsT"].ToString());
            objP.SepN = (sdr["SepT"] == DBNull.Value) ? 0 : decimal.Parse(sdr["SepT"].ToString());
            objP.OktN = (sdr["OktT"] == DBNull.Value) ? 0 : decimal.Parse(sdr["OktT"].ToString());
            objP.NopN = (sdr["NovT"] == DBNull.Value) ? 0 : decimal.Parse(sdr["NovT"].ToString());
            objP.DesN = (sdr["DesT"] == DBNull.Value) ? 0 : decimal.Parse(sdr["DesT"].ToString());

            objP.JanB = (sdr["JanB"] == DBNull.Value) ? 0 : decimal.Parse(sdr["JanB"].ToString());
            objP.FebB = (sdr["FebB"] == DBNull.Value) ? 0 : decimal.Parse(sdr["FebB"].ToString());
            objP.MarB = (sdr["MarB"] == DBNull.Value) ? 0 : decimal.Parse(sdr["MarB"].ToString());
            objP.AprB = (sdr["AprB"] == DBNull.Value) ? 0 : decimal.Parse(sdr["AprB"].ToString());
            objP.MeiB = (sdr["MeiB"] == DBNull.Value) ? 0 : decimal.Parse(sdr["MeiB"].ToString());
            objP.JunB = (sdr["JunB"] == DBNull.Value) ? 0 : decimal.Parse(sdr["JunB"].ToString());
            objP.JulB = (sdr["JulB"] == DBNull.Value) ? 0 : decimal.Parse(sdr["JulB"].ToString());
            objP.AgsB = (sdr["AgsB"] == DBNull.Value) ? 0 : decimal.Parse(sdr["AgsB"].ToString());
            objP.SepB = (sdr["SepB"] == DBNull.Value) ? 0 : decimal.Parse(sdr["SepB"].ToString());
            objP.OktB = (sdr["OktB"] == DBNull.Value) ? 0 : decimal.Parse(sdr["OktB"].ToString());
            objP.NopB = (sdr["NovB"] == DBNull.Value) ? 0 : decimal.Parse(sdr["NovB"].ToString());
            objP.DesB = (sdr["DesB"] == DBNull.Value) ? 0 : decimal.Parse(sdr["DesB"].ToString());

            objP.Semester1 = (sdr["Total1"] == DBNull.Value) ? 0 : decimal.Parse(sdr["Total1"].ToString());
            objP.Semester2 = (sdr["Total2"] == DBNull.Value) ? 0 : decimal.Parse(sdr["Total2"].ToString());
            objP.Nama = sdr["PIC"].ToString();
            objP.DeptID = (sdr["DeptID"] == DBNull.Value) ? 0 : int.Parse(sdr["DeptID"].ToString());
            objP.BagianID = (sdr["BagianID"] == DBNull.Value) ? 0 : int.Parse(sdr["BagianID"].ToString());
            objP.BobotSmt1 = (sdr["NilaiBobot1"] == DBNull.Value) ? 0 : decimal.Parse(sdr["NilaiBobot1"].ToString());
            objP.BobotSmt2 = (sdr["NilaiBobot2"] == DBNull.Value) ? 0 : decimal.Parse(sdr["NilaiBobot2"].ToString());
            objP.Penilaian = int.Parse(sdr["Penilaian"].ToString());
            objP.Approval = int.Parse(sdr["Approval"].ToString());
            return objP;
        }
        private RekapPesDetail GenerateObjectPES(SqlDataReader sdr, bool detail)
        {
            RekapPesDetail objP = new RekapPesDetail();
            objP.PESName = sdr["KPIName"].ToString();
            objP.Bobot = decimal.Parse(sdr["Bobot"].ToString());
            objP.Tahun = int.Parse(sdr["Tahun"].ToString());
            objP.Jan = sdr["JanN"].ToString();
            objP.Feb = sdr["FebN"].ToString();
            objP.Mar = sdr["MarN"].ToString();
            objP.Apr = sdr["AprN"].ToString();
            objP.Mei = sdr["MeiN"].ToString();
            objP.Jun = sdr["JunN"].ToString();
            objP.Jul = sdr["JulN"].ToString();
            objP.Ags = sdr["AgsN"].ToString();
            objP.Sep = sdr["SepN"].ToString();
            objP.Okt = sdr["OktN"].ToString();
            objP.Nop = sdr["NovN"].ToString();
            objP.Des = sdr["DesN"].ToString();

            objP.JanN = sdr["JanP"].ToString();
            objP.FebN = sdr["FebP"].ToString();
            objP.MarN = sdr["MarP"].ToString();
            objP.AprN = sdr["AprP"].ToString();
            objP.MeiN = sdr["MeiP"].ToString();
            objP.JunN = sdr["JunP"].ToString();
            objP.JulN = sdr["JulP"].ToString();
            objP.AgsN = sdr["AgsP"].ToString();
            objP.SepN = sdr["SepP"].ToString();
            objP.OktN = sdr["OktP"].ToString();
            objP.NopN = sdr["NovP"].ToString();
            objP.DesN = sdr["DesP"].ToString();

            objP.JanB = sdr["JanB"].ToString();
            objP.FebB = sdr["FebB"].ToString();
            objP.MarB = sdr["MarB"].ToString();
            objP.AprB = sdr["AprB"].ToString();
            objP.MeiB = sdr["MeiB"].ToString();
            objP.JunB = sdr["JunB"].ToString();
            objP.JulB = sdr["JulB"].ToString();
            objP.AgsB = sdr["AgsB"].ToString();
            objP.SepB = sdr["SepB"].ToString();
            objP.OktB = sdr["OktB"].ToString();
            objP.NopB = sdr["NovB"].ToString();
            objP.DesB = sdr["DesB"].ToString();
            objP.Nama = sdr["PIC"].ToString();
            objP.DeptID = (sdr["DeptID"] == DBNull.Value) ? 0 : int.Parse(sdr["DeptID"].ToString());
            objP.BagianID = (sdr["BagianID"] == DBNull.Value) ? 0 : int.Parse(sdr["BagianID"].ToString());
            objP.Penilaian = int.Parse(sdr["Penilaian"].ToString());
            objP.Rebobot = (sdr["Bulan"].ToString());
            objP.Urutan = int.Parse(sdr["Urutan"].ToString());
            objP.Approval = (sdr["Approval"] != DBNull.Value) ? int.Parse(sdr["Approval"].ToString()) : 0;
            return objP;
        }

        public PES2016 CheckApproval(string PESType)
        {
            PES2016 objP = new PES2016();
            string strSQL = "SELECT COUNT(ID) JmlItem,ISNULL(SUM(Approval),0)Approval,COUNT(Rebobot)Penilaian FROM ";
            strSQL += "(SELECT ID,Status AS Approval,(Select Penilaian From ISO_UserCategory where ISO_UserCategory.ID=ISO_" + PESType +
                       ".CategoryID AND Penilaian >0 )Rebobot " +
                         "FROM ISO_" + PESType + " WHERE RowStatus>-1 " + this.Criteria;
             strSQL += ") AS x ";
             DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    objP.Approval = int.Parse(sdr["Approval"].ToString());
                    objP.JmlItemPES = int.Parse(sdr["JmlItem"].ToString());
                    objP.Penilaian = int.Parse(sdr["Penilaian"].ToString());
                }
            }
            return objP;
        }
    }
    public class Organisasi:ISO_PES
    {
        public string Limit { get; set; }
        public ArrayList Struktur(string Criteria)
        {
            ArrayList arrData = new ArrayList();
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select " + Limit + " * From ISO_Org where RowStatus>-1 " + Criteria + " Order by Revisi desc";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateObject(sdr));
                }
            }
            return arrData;
        }

        private PESOrg GenerateObject(SqlDataReader sdr)
        {
            PESOrg objOrg = new PESOrg();
            objOrg.DeptID = int.Parse(sdr["DeptID"].ToString());
            objOrg.Revisi = int.Parse(sdr["Revisi"].ToString());
            objOrg.FileName = sdr["FileName"].ToString();
            return objOrg;
        }
    }
}
