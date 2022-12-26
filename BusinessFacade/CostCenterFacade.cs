using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using DataAccessLayer;
using System.Web;
namespace BusinessFacade
{
    public class CostCenterFacade:AbstractFacade
    {
        private CostCenter objCost = new CostCenter();
        ArrayList arrData = new ArrayList();
        List<SqlParameter> sqlList;
        public string StartPeriode { get; set; }
        public string StartBulan { get; set; }
        public string EndPeriode { get; set; }
        public int MatCCGroupID { get; set; }
        public string Criteria { get; set; }
        public string Bulan {get;set;}
        public string Tahun {get;set;}
        public CostCenterFacade()
            : base()
        {

        }

        public override int Insert(object objDomain)
        {
            objCost = (CostCenter)objDomain;
            sqlList = new List<SqlParameter>();
            sqlList.Add(new SqlParameter("@ItemID", objCost.ItemID));
            sqlList.Add(new SqlParameter("@MaterialCCID", objCost.MaterialCCID));
            sqlList.Add(new SqlParameter("@MateriaCCGroupID", objCost.MaterialGroupID));
            sqlList.Add(new SqlParameter("@ItemTypeID", objCost.ItemTypeID));
            sqlList.Add(new SqlParameter("@RowStatus", objCost.RowStatus));
            sqlList.Add(new SqlParameter("@CreatedBy", objCost.CreatedBy));
            //DataAccess da = new DataAccess(Global.ConnectionString());
            int result = dataAccess.ProcessData(sqlList, "spMaterialPPGroup_Insert");
            return result;
        }
        public override int Update(object objDomain)
        {
            int result = -1;
            ZetroLib zl = new ZetroLib();
            zl.hlp = new CostFIN();
            zl.Criteria = "ID,RowStatus,LastModifiedBy,LastModifiedTime";
            zl.StoreProcedurName = "spMaterialPPGroup_Update";
            zl.TableName = "MaterialPPGroup";
            zl.Option = "Update";
            zl.ReturnID = false;
            string rst = zl.CreateProcedure();
            if (rst == string.Empty)
            {
                zl.hlp = objDomain;
                result = zl.ProcessData();
            }
            return result;
        }
        public override int Delete(object objDomain)
        {
            throw new NotImplementedException();
        }
        public override ArrayList Retrieve()
        {
            arrData = new ArrayList();
            string strSQL = "SELECT * FROM MaterialCC WHERE RowStatus >-1 ORDER BY GroupName";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateObject(sdr));
                }
            }
            return arrData;
        }
        
        public ArrayList Retrieve(int MaterialCCID,int Tahun,int bulan)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string Line = (users.UnitKerjaID == 7) ? "6" : "4";
            string  line1 = PlanningProdLine();
            arrData = new ArrayList();
            //string strSQL = "WITH Monitoring AS( " +
            //                "SELECT ID,MaterialCCID,GroupName," + PlanningProdLine() + " RunLine,SortOrder  " +
            //                "FROM MaterialCCGroup " +
            //                "WHERE RowStatus>-1 AND MaterialCCID=" + MaterialCCID +
            //                "),  " +
            //                "Monitoring1 AS ( " +
            //                " SELECT m.*,mx.CostValue FROM Monitoring m " +
            //                " LEFT JOIN MaterialCCMatrix mx ON mx.MaterialCCID=m.MaterialCCID AND mx.MaterialGroupID=m.ID AND  " +
            //                " mx.RunningLine=m.RunLine " +
            //                " ), " +
            //                " Monitoring2 AS( " +
            //                " SELECT m.*,ISNULL(xx.price,0)CostActual FROM Monitoring1 m " +
            //                SPBData(MaterialCCID) +
            //                "),Monitoring3 AS ( " +
            //                "    SELECT ID,GroupName,RunLine,ISNULL(CostValue,0)CostValue,ISNULL(CostActual,0)CostActual,SortOrder,  " +
            //                "    CASE WHEN CostActual > 0 AND CostValue >0 THEN ((CostActual/CostValue)*100) ELSE 0 END AS Prosen, " +
            //                "    1 Urutan FROM Monitoring2 " +
            //                "    UNION ALL  " +
            //                "    SELECT '' ID,'TOTAL 'GroupName,''RunLine,ISNULL(SUM(CostValue),0),ISNULL(SUM(CostActual),0),99 SortOrder, " +
            //                "    CASE WHEN SUM(CostActual) > 0 THEN((SUM(CostActual)/SUM(CostValue))*100) ELSE 0 END AS Prosen,  " +
            //                "    2 Urutan FROM Monitoring2 " +
            //                "    ) " +
            //                "    SELECT * FROM Monitoring3 ORDER BY Urutan,SortOrder ";

            string strSQL = "WITH Monitoring AS( " +
                            "SELECT ID,MaterialCCID,GroupName," + PlanningProdLine() + " RunLine,SortOrder  " +
                            "FROM MaterialCCGroup " +
                            "WHERE RowStatus>-1 AND MaterialCCID=" + MaterialCCID +
                            "),  " +
                            "Monitoring1 AS ( " +
                            " SELECT m.*,mx.CostValue FROM Monitoring m " +
                            " LEFT JOIN MaterialCCMatrix mx ON mx.MaterialCCID=m.MaterialCCID AND mx.MaterialGroupID=m.ID AND  " +
                            " mx.RunningLine=m.RunLine and mx.RowStatus>-1 and mx.costperiod=" + bulan + " and mx.costyear=" + Tahun + 
                            " ), " +
                            " Monitoring2 AS( " +
                            " SELECT m.*,ISNULL(xx.price,0)CostActual FROM Monitoring1 m " +
                            SPBData(MaterialCCID) +
                            "),Monitoring3 AS ( " +
                            "    SELECT ID,GroupName,RunLine,ISNULL(CostValue,0)CostValue,ISNULL(CostActual,0)CostActual,SortOrder,  " +
                            "    CASE WHEN CostActual > 0 AND CostValue >0 THEN ((CostActual/CostValue)*100) ELSE 0 END AS Prosen, " +
                            "    1 Urutan FROM Monitoring2 " +
                            "    UNION ALL  " +
                            "    SELECT '' ID,'TOTAL 'GroupName,''RunLine,ISNULL(SUM(CostValue),0),ISNULL(SUM(CostActual),0),99 SortOrder, " +
                            "    CASE WHEN SUM(CostActual) > 0 THEN((SUM(CostActual)/SUM(CostValue))*100) ELSE 0 END AS Prosen,  " +
                            "    2 Urutan FROM Monitoring2 " +
                            "    ) " +
                            "    SELECT * FROM Monitoring3 ORDER BY Urutan,SortOrder ";

            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateObject(sdr, GenerateObject(sdr)));
                }
            }
            return arrData;
        }
        public int GetActual(int MaterialCCID, int Tahun, int bulan)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string Line = (users.UnitKerjaID == 7) ? "6" : "4";
             Line = (users.UnitKerjaID == 13) ? "1" : "4";
            string line1 = PlanningProdLine();
            int result = 0;

            string strSQL = "WITH Monitoring AS( " +
                            "SELECT ID,MaterialCCID,GroupName," + PlanningProdLine() + " RunLine,SortOrder  " +
                            "FROM MaterialCCGroup " +
                            "WHERE RowStatus>-1 AND MaterialCCID=" + MaterialCCID +
                            "),  " +
                            "Monitoring1 AS ( " +
                            " SELECT m.*,mx.CostValue FROM Monitoring m " +
                            " LEFT JOIN MaterialCCMatrix mx ON mx.MaterialCCID=m.MaterialCCID AND mx.MaterialGroupID=m.ID AND  " +
                            " mx.RunningLine=m.RunLine and mx.RowStatus>-1 and mx.costperiod=" + bulan + " and mx.costyear=" + Tahun +
                            " ), " +
                            " Monitoring2 AS( " +
                            " SELECT m.*,ISNULL(xx.price,0)CostActual FROM Monitoring1 m " +
                            SPBData(MaterialCCID) +
                            "),Monitoring3 AS ( " +
                            "    SELECT ID,GroupName,RunLine,ISNULL(CostValue,0)CostValue,ISNULL(CostActual,0)CostActual,SortOrder,  " +
                            "    CASE WHEN CostActual > 0 AND CostValue >0 THEN ((CostActual/CostValue)*100) ELSE 0 END AS Prosen, " +
                            "    1 Urutan FROM Monitoring2 " +
                            "    UNION ALL  " +
                            "    SELECT '' ID,'TOTAL 'GroupName,''RunLine,ISNULL(SUM(CostValue),0),ISNULL(SUM(CostActual),0),99 SortOrder, " +
                            "    CASE WHEN SUM(CostActual) > 0 THEN((SUM(CostActual)/SUM(CostValue))*100) ELSE 0 END AS Prosen,  " +
                            "    2 Urutan FROM Monitoring2 " +
                            "    ) " +
                            "    SELECT cast(CostActual as int) CostActual FROM Monitoring3 where groupname='total' ";

            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = int.Parse(sdr["CostActual"].ToString());
                }
            }
            return result;
        }
        public string PlanningProdLine()
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string Line = (users.UnitKerjaID == 7) ? "5" : "4";
             Line = (users.UnitKerjaID == 13) ? "1" : "4";
            string result = Line;
            string strSQL = "SELECT top 1 PlanningID,COUNT(PakaiDate) Fequensi,(Select Planning From MaterialPP Where ID=PlanningID)Line FROM " +
                            "( " +
                            "    SELECT PakaiDate,PlanningID FROM pakai  " +
                            "    WHERE Month(PakaiDate)=" + this.StartPeriode.Substring(4, 2) + " and Year(PakaiDate)=" +
                                this.EndPeriode.Substring(0, 4) + " AND PlanningID>0 AND DeptID=2 AND Status>-1 " +
                            "    GROUP BY PakaiDate,PlanningID " +
                            ") AS x " +
                            "GROUP By PlanningID " +
                            "HAVING COUNT(PakaiDate)>7 " +
                            "ORDER BY Fequensi DESC";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = sdr["Line"].ToString();
                }
            }
            else
            {
                result = PlanningProdLine(true);
            }
            return result;
        }
        private string PlanningProdLine( bool CheckLagi)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string Line = (users.UnitKerjaID == 7) ? "5" : "4";
            Line = (users.UnitKerjaID == 13) ? "1" : "4";
            string result = Line;
            string strSQL = "SELECT top 1 PlanningID,COUNT(PakaiDate) Fequensi,(Select Planning From MaterialPP Where ID=PlanningID)Line FROM " +
                            "( " +
                            "    SELECT PakaiDate,PlanningID FROM pakai  " +
                            "    WHERE Month(PakaiDate)=" + this.StartPeriode.Substring(4, 2) + " and Year(PakaiDate)=" +
                                this.EndPeriode.Substring(0, 4) + " AND PlanningID>0 AND DeptID=2 AND Status>-1 " +
                            "    GROUP BY PakaiDate,PlanningID " +
                            ") AS x " +
                            "GROUP By PlanningID " +
                            "/*HAVING COUNT(PakaiDate)>7*/ " +
                            "ORDER BY Fequensi DESC";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = sdr["Line"].ToString();
                }
            }
            return result;
        }
        public ArrayList Retrieve(int MaterialCCID, bool ShowDetail)
        {
            arrData = new ArrayList();
            DataAccess da = new DataAccess(Global.ConnectionString());
            string strSQL = this.DetailBudget(MaterialCCID);
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(DetailObject(sdr, MaterialCCID));
                }
            }
            return arrData;
        }
        public ArrayList RetrieveBM(int MaterialCCID, bool ShowDetail)
        {
            arrData = new ArrayList();
            DataAccess da = new DataAccess(Global.ConnectionString());
            string strSQL = this.DetailBudgetBM();
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(DetailObject(sdr, MaterialCCID));
                }
            }
            return arrData;
        }

        
        private string SPBData(int MatCCID)
        {
            string result = string.Empty;
            switch (MatCCID)
            {
                case 1:
                    result = " LEFT JOIN  " +
                            " ( " +
                            "    SELECT SUM(x.Quantity)Qty,SUM((x.Quantity*x.AvgPrice))Price,x.MatCCGroupID FROM ( " +
                            "    SELECT pd.ItemID,pd.Quantity,pd.AvgPrice,mp.MatCCGroupID  " +
                            "    FROM PakaiDetail pd " +
                            "    INNER JOIN Pakai p ON p.ID=pd.PakaiID " +
                            "    INNER JOIN MaterialPPGroup AS mp ON mp.ItemID=pd.ItemID AND mp.ItemTypeID=pd.ItemTypeID " +
                            "    WHERE pd.RowStatus>-1 AND p.Status>1 AND mp.MatCCID=1 AND CONVERT(CHAR,p.PakaiDate,112) " +
                            "    Between '" + this.StartPeriode + "' AND '" + this.EndPeriode + "' " +
                            "    AND DeptID=2 AND mp.RowStatus>-1" +
                            "    ) AS x GROUP BY MatCCGroupID " +
                            "   UNION ALL " +
                            "    SELECT SUM(x.Quantity)Qty,SUM((x.Quantity*x.AvgPrice))Price,x.MatCCGroupID FROM ( " +
                            "       SELECT pd.ItemID,pd.Quantity,pd.AvgPrice,14 MatCCGroupID  " +
                            "       FROM PakaiDetail pd " +
                            "       INNER JOIN Pakai p ON p.ID=pd.PakaiID	   " +
                            "       WHERE pd.RowStatus>-1 AND CONVERT(CHAR,p.PakaiDate,112)  " +
                            "       Between '" + this.StartPeriode + "' AND '" + this.EndPeriode + "' AND p.Status>1 " +
                            "       AND pd.ItemID NOT IN(SELECT ItemID FROM MaterialPPGroup WHERE MatCCID=1 AND RowStatus>-1)  " +
                            "       AND p.DeptID=2  AND pd.GroupID IN(8,9) " +
                            "    ) AS x GROUP BY MatCCGroupID " +
                            "  ) AS xx ON xx.MatCCGroupID=m.ID ";
                    break;
                case 4:
                    result = "LEFT JOIN " +
                           "  ( " +
                           "     SELECT DeptID MatCCGroupID,ISNULL(SUM((Quantity*AvgPrice)),0)Price  " +
                           "     FROM (   " +
                           "        SELECT ls.SarmutID, ls.PakaiNo,ls.DeptiD, pd.ItemID,pd.Quantity,pd.AvgPrice,'Pakai' TipeTrans " +
                           "        FROM PakaiDetail pd " +
                           "        INNER JOIN Pakai p ON p.ID=pd.PakaiID " +
                           "        INNER JOIN MTC_LapSarmut AS ls ON ls.ItemID=pd.ItemID AND ls.ItemTypeID=pd.ItemTypeID  " +
                           "        AND ls.PakaiNo=p.PakaiNo and ls.rowstatus>-1 " +
                           "        WHERE pd.RowStatus>-1  AND CONVERT(CHAR,p.PakaiDate,112) " +
                           "        Between '" + this.StartPeriode + "' AND '" + this.EndPeriode + "' " +
                           "        AND p.DeptID in(SELECT SortOrder FROM MaterialCCGroup WHERE MaterialCCID=4 )  AND p.Status>1  " +
                           "   UNION ALL " +
                           // Tambahan Return Pakai
                             "   SELECT mt.SarmutID,rp.ReturNo,rp.DeptID,rpd.ItemID,rpd.Quantity,(-1*(rpd.AvgPrice))AvgPrice,'Return' TipeTrans  " +
                             "   FROM ReturPakaiDetail as rpd " +
                             "   INNER JOIN ReturPakai rp ON rp.ID=rpd.ReturID " +
                             "   LEFT JOIN MTC_LapSarmut mt ON mt.ItemID=rpd.ItemID AND mt.ItemTypeID=rpd.ItemTypeID " +
                          // Tambahan Link
                             "   INNER JOIN Pakai as pk ON pk.PakaiNo=rp.PakaiNo and pk.ItemTypeID=rp.ItemTypeID and mt.PakaiNo=pk.PakaiNo  "+
                             "   LEFT JOIN MTC_GroupSarmut as gs ON gs.ID=mt.SarmutID   "+
                          // End Tambahan
                             "   WHERE  CONVERT(CHAR,rp.ReturDate,112) Between '" + this.StartPeriode + "' AND '" + this.EndPeriode + "' AND " +
                             "   rp.DeptID in(SELECT SortOrder FROM MaterialCCGroup WHERE MaterialCCID=4 ) and rpd.RowStatus>-1 And rp.Status>-1 " +
                             "   AND rpd.ItemID in(Select mt.ItemID From MTC_LapSarmut mt WHERE mt.ItemTypeID=rpd.ItemTypeID Group By mt.ItemID) " +
                           "      ) AS x " +
                           "      LEFT JOIN vw_SarmutGroup AS s ON s.SarmutID=x.SarmutID " +
                           "      LEFT JOIN MaterialMTCGroup AS m ON m.ID=s.ID " +
                           "      WHERE m.RowStatus>-1 AND x.DeptID IS NOT NULL " +
                           "     GROUP By DeptID " +
                           "   ) AS xx ON xx.MatCCGroupID=m.SortOrder";
                    break;
            }
            return result;
        }
        private string DetailBudgetBM()
        {
            string result =
                        "  WITH  DetailBM AS ( " +
                             "   SELECT pd.ID, pd.ItemID,pd.Quantity,pd.AvgPrice,mp.MatCCGroupID,pd.ItemTypeID " +
                             "   FROM PakaiDetail pd " +
                             "   INNER JOIN Pakai p ON p.ID=pd.PakaiID " +
                             "   INNER JOIN MaterialPPGroup AS mp ON mp.ItemID=pd.ItemID AND mp.ItemTypeID=pd.ItemTypeID " +
                             "   WHERE pd.RowStatus>-1 AND mp.MatCCID=1 AND CONVERT(CHAR,p.PakaiDate,112) " +
                             "   Between '" + this.StartPeriode + "' AND '" + this.EndPeriode + "' " +
                             "   AND p.DeptID=2  AND p.Status>1" +
                             "   UNION ALL " +
                             "   SELECT pd.ID, pd.ItemID,pd.Quantity,pd.AvgPrice,14 MatCCGroupID,pd.ItemTypeID " +
                             "   FROM PakaiDetail pd " +
                             "   INNER JOIN Pakai p ON p.ID=pd.PakaiID    " +
                             "   WHERE pd.RowStatus>-1 AND ItemID NOT IN(SELECT ItemID FROM MaterialPPGroup WHERE MatCCID=1 AND RowStatus>-1) " +
                             "   AND CONVERT(CHAR,p.PakaiDate,112) Between '" + this.StartPeriode + "' AND '" + this.EndPeriode + "'  " +
                             "   AND p.DeptID=2 AND pd.GroupID in(8,9) AND pd.ItemTypeID=1 AND p.Status>1" +
                             " ) " +

                             " ,DetailBM0 AS ( " +
                             " SELECT pd.ID,p.DeptID,p.PakaiDate,pd.ItemID,pd.ItemTypeID,pd.Quantity,p.PlanningID,ISNULL(mp.Planning,2)Planning, " +
                             "   bm.BudgetQty,(select dbo.ItemNameInv(pd.Itemid,1))ItemName " +
                             "   FROM PakaiDetail pd " +
                             "   LEFT JOIN Pakai p ON pd.PakaiID=p.ID " +
                             "   LEFT JOIN MaterialPP mp ON mp.ID=p.PlanningID " +
                             "   LEFT JOIN MaterialPPBudgetBM bm ON bm.ItemID=pd.ItemID AND bm.RunningLine=ISNULL(mp.Planning,2) " +
                             "   WHERE CONVERT(CHAR,p.PakaiDate,112) Between '" + this.StartPeriode + "' AND '" + this.EndPeriode + "'  " +
                             "   AND pd.ItemID IN(SELECT ItemID FROM MaterialPPGroup WHERE MaterialPPGroup.MatCCID=1 and rowstatus>-1 Group By ItemID) " +
                             "   AND p.Status>-1 AND pd.RowStatus>-1 and bm.rowstatus>-1 and mp.rowstatus>-1" +
                             "   AND DeptID=2 " +
                             "), " +

                             " DetailBM1 AS( " +
                             " SELECT D.*,mg.GroupName,MaterialCCID,SortOrder,bm.BudgetQty FROM DetailBM  AS D " +
                             " LEFT JOIN MaterialCCGroup AS mg ON mg.SortOrder=D.MatCCGroupID AND mg.MaterialCCID=1 " +
                             " LEFT JOIN DetailBM0 bm ON bm.ID=d.ID " +
                             " ), " +
                             " DetailBM2 AS( " +
                             " SELECT SortOrder,GroupName,(SELECT Dbo.ItemCodeInv(ItemID,ItemTypeID))ItemCode,ItemID, " +
                             " (SELECT dbo.ItemNameInv(ItemID,ItemTypeID))ItemName,SUM(Quantity)Quantity,(SUM(Quantity)*AvgPrice)Price,BudgetQty " +
                             " FROM DetailBM1 " +
                             " GROUP BY ItemID,ItemTypeID,GroupName,AvgPrice,SortOrder,BudgetQty " +
                             " ) " +
                             ",DetailBM3 AS( " +
                             " SELECT SortOrder,GroupName,ItemCode,ItemName," +
                             " ISNULL(BudgetQty,0)MaxQty" +
                             ",Quantity,Price, 1 Urutan FROM DetailBM2 db " +
                             " /*LEFT JOIN BudgetSP b On b.ItemID=db.ItemID AND b.ItemTypeID=1 */" +
                // " WHERE  SortOrder=" + this.MatCCGroupID +
                             " /*UNION ALL " +
                             " SELECT 9999 SortOrder,'TOTAL 'GroupName,'' ItemCode,'' ItemName,SUM( MaxQty)MaxQty,SUM(Quantity),SUM(Price), 2 Urutan FROM DetailBM2  " +
                //" WHERE SortOrder=" + this.MatCCGroupID +
                             " */),DetailBM4 AS( " +
                             " SELECT * FROM DetailBM3 " +
                             " UNION ALL " +
                             " SELECT 9999 SortOrder,'TOTAL 'GroupName,'' ItemCode,'' ItemName,ISNULL(SUM(MaxQty),0)MaxQty," +
                             " ISNULL(SUM(Quantity),0)Quantity,ISNULL(SUM(Price),0)Price, 2 Urutan FROM DetailBM3  " +
                             ") SELECT * FROM DetailBM4 " +
                             " ORDER BY SortOrder,Urutan,ItemName ";
            return result;
        }
        //private string DetailBudget(int MaterialCCID)
        //{
        //    string result = string.Empty;
        //    switch (MaterialCCID)
        //    {
        //        case 1:
        //            result =
        //                "  WITH  DetailBM AS ( " +
        //                     "   SELECT pd.ID, pd.ItemID,pd.Quantity,pd.AvgPrice,mp.MatCCGroupID,pd.ItemTypeID " +
        //                     "   FROM PakaiDetail pd " +
        //                     "   INNER JOIN Pakai p ON p.ID=pd.PakaiID " +
        //                     "   INNER JOIN MaterialPPGroup AS mp ON mp.ItemID=pd.ItemID AND mp.ItemTypeID=pd.ItemTypeID " +
        //                     "   WHERE pd.RowStatus>-1 AND mp.MatCCID=1 AND CONVERT(CHAR,p.PakaiDate,112) " +
        //                     "   Between '" + this.StartPeriode + "' AND '" + this.EndPeriode + "' " +
        //                     "   AND p.DeptID=2  AND p.Status>1" +
        //                     "   UNION ALL " +
        //                     "   SELECT pd.ID, pd.ItemID,pd.Quantity,pd.AvgPrice,14 MatCCGroupID,pd.ItemTypeID " +
        //                     "   FROM PakaiDetail pd " +
        //                     "   INNER JOIN Pakai p ON p.ID=pd.PakaiID    " +
        //                     "   WHERE pd.RowStatus>-1 AND ItemID NOT IN(SELECT ItemID FROM MaterialPPGroup WHERE MatCCID=1 AND pd.RowStatus>-1) " +
        //                     "   AND CONVERT(CHAR,p.PakaiDate,112) Between '" + this.StartPeriode + "' AND '" + this.EndPeriode + "'  " +
        //                     "   AND p.DeptID=2 AND pd.GroupID in(8,9) AND pd.ItemTypeID=1 AND p.Status>1" +
        //                     " ) " +
        //                     " ,DetailBM0 AS ( " +
        //                     " SELECT pd.ID,p.DeptID,p.PakaiDate,pd.ItemID,pd.ItemTypeID,pd.Quantity,p.PlanningID,ISNULL(mp.Planning,2)Planning, " +
        //                     "   bm.BudgetQty,(select dbo.ItemNameInv(pd.Itemid,1))ItemName " +
        //                     "   FROM PakaiDetail pd " +
        //                     "   LEFT JOIN Pakai p ON pd.PakaiID=p.ID " +
        //                     "   LEFT JOIN MaterialPP mp ON mp.ID=p.PlanningID " +
        //                     "   LEFT JOIN MaterialPPBudgetBM bm ON bm.ItemID=pd.ItemID AND bm.RunningLine=ISNULL(mp.Planning,2) " +
        //                     "   WHERE CONVERT(CHAR,p.PakaiDate,112) Between '" + this.StartPeriode + "' AND '" + this.EndPeriode + "'  " +
        //                     "   AND pd.ItemID IN(SELECT ItemID FROM MaterialPPGroup WHERE MaterialPPGroup.MatCCID=1 Group By ItemID) " +
        //                     "   AND p.Status>-1 AND pd.RowStatus>-1 " +
        //                     "   AND DeptID=2 " +
        //                     "), " +
        //                     " DetailBM1 AS( " +
        //                     " SELECT D.*,mg.GroupName,MaterialCCID,SortOrder,bm.BudgetQty FROM DetailBM  AS D " +
        //                     " LEFT JOIN MaterialCCGroup AS mg ON mg.SortOrder=D.MatCCGroupID AND mg.MaterialCCID=1 " +
        //                     " LEFT JOIN DetailBM0 bm ON bm.ID=d.ID " +
        //                     " ), " +
        //                     " DetailBM2 AS( " +
        //                     " SELECT SortOrder,GroupName,(SELECT Dbo.ItemCodeInv(ItemID,ItemTypeID))ItemCode,ItemID, " +
        //                     " (SELECT dbo.ItemNameInv(ItemID,ItemTypeID))ItemName,SUM(Quantity)Quantity,(SUM(Quantity)*AvgPrice)Price,BudgetQty " +
        //                     " FROM DetailBM1 " +
        //                     " GROUP BY ItemID,ItemTypeID,GroupName,AvgPrice,SortOrder,BudgetQty " +
        //                     " ) " +
        //                     ",DetailBM3 AS( " +
        //                     " SELECT SortOrder,GroupName,ItemCode,ItemName," +
        //                     " ISNULL(BudgetQty,0)MaxQty" +
        //                     ",Quantity,Price, 1 Urutan FROM DetailBM2 db " +
        //                     " /*LEFT JOIN BudgetSP b On b.ItemID=db.ItemID AND b.ItemTypeID=1 */" +
        //                     " WHERE  SortOrder=" + this.MatCCGroupID +
        //                     " /*UNION ALL " +
        //                     " SELECT 9999 SortOrder,'TOTAL 'GroupName,'' ItemCode,'' ItemName,SUM( MaxQty)MaxQty,SUM(Quantity),SUM(Price), 2 Urutan FROM DetailBM2  " +
        //                     " WHERE SortOrder=" + this.MatCCGroupID +
        //                     " */),DetailBM4 AS( " +
        //                     " SELECT * FROM DetailBM3 " +
        //                     " UNION ALL " +
        //                     " SELECT 9999 SortOrder,'TOTAL 'GroupName,'' ItemCode,'' ItemName,ISNULL(SUM(MaxQty),0)MaxQty," +
        //                     " ISNULL(SUM(Quantity),0)Quantity,ISNULL(SUM(Price),0)Price, 2 Urutan FROM DetailBM3  " +
        //                     ") SELECT * FROM DetailBM4 " +
        //                     " ORDER BY SortOrder,Urutan,ItemName ";

                        
        //            break;
        //        case 4:
        //            result = "WITH SarmutMTC AS ( " +
        //                     "   SELECT x.*,m.GroupNaME,m.Kode FROM ( " +
        //                     "   SELECT ls.SarmutID,ls.PakaiNo,ls.DeptiD, pd.ItemID,pd.Quantity,pd.AvgPrice,'Pakai' TipeTrans  " +
        //                     "   FROM PakaiDetail pd " +
        //                     "   INNER JOIN Pakai p ON p.ID=pd.PakaiID " +
        //                     "   INNER JOIN MTC_LapSarmut AS ls ON ls.ItemID=pd.ItemID AND ls.ItemTypeID=pd.ItemTypeID AND ls.PakaiNo=p.PakaiNo " +
        //                     "   WHERE pd.RowStatus>-1  AND CONVERT(CHAR,p.PakaiDate,112) Between '" + this.StartPeriode + "' AND '" + this.EndPeriode + "'  " +
        //                     "   AND p.DeptID in(SELECT SortOrder FROM MaterialCCGroup WHERE MaterialCCID=4 )  AND p.Status>1  " +
                             
        //                     "   UNION ALL "+
        //                     //  Tambahan Retur Pakai
        //                     "   SELECT mt.SarmutID,rp.ReturNo,rp.DeptID,rpd.ItemID,rpd.Quantity,(-1*(rpd.AvgPrice))AvgPrice,'Return' TipeTrans  "+
        //                     "   FROM ReturPakaiDetail as rpd "+
        //                     "   INNER JOIN ReturPakai rp ON rp.ID=rpd.ReturID "+
        //                     "   LEFT JOIN MTC_LapSarmut mt ON mt.ItemID=rpd.ItemID AND mt.ItemTypeID=rpd.ItemTypeID "+
        //                     //  Tambahan Link
        //                     "   INNER JOIN Pakai as pk ON pk.PakaiNo=rp.PakaiNo and pk.ItemTypeID=rp.ItemTypeID and mt.PakaiNo=pk.PakaiNo "+   
        //                     "   LEFT JOIN MTC_GroupSarmut as gs ON gs.ID=mt.SarmutID "+
        //                     //  End Tambahan
        //                     "   WHERE  CONVERT(CHAR,rp.ReturDate,112) Between '" + this.StartPeriode + "' AND '" + this.EndPeriode + "' AND " +
        //                     "   rp.DeptID in(SELECT SortOrder FROM MaterialCCGroup WHERE MaterialCCID=4 ) and rpd.RowStatus>-1 And rp.Status>-1 " +
        //                     "   AND rpd.ItemID in(Select mt.ItemID From MTC_LapSarmut mt WHERE mt.ItemTypeID=rpd.ItemTypeID Group By mt.ItemID) " +
        //                     "   ) AS X " +
        //                     "   LEFT JOIN vw_SarmutGroup AS s ON s.SarmutID=x.SarmutID " +
        //                     "   LEFT JOIN MaterialMTCGroup AS m ON m.ID=s.ID " +
        //                     "   WHERE x.DeptID=(SELECT SortOrder FROM MaterialCCGroup WHERE MaterialCCID=4 AND ID=" + this.MatCCGroupID + ") AND m.RowStatus>-1 " +
        //                    "), " +
        //                    "SarmutMTC1 AS ( " +
        //                    "SELECT m.Kode, m.GroupName,ISNULL(SUM((Quantity*AvgPrice)),0) Price " +
        //                    "FROM MaterialMTCGroup AS m " +
        //                    "LEFT JOIN SarmutMTC AS sm ON sm.Kode=m.Kode " +
        //                    "WHERE m.RowStatus>-1 " +
        //                    "GROUP BY m.Kode, m.GroupName " +
        //                    "), " +
        //                    "SarmutMTC2 AS ( " +
        //                    "SELECT Kode,GroupName,ISNULL(Price,0)Price,1 Urutan FROM SarmutMTC1 " +
        //                    "UNION " +
        //                    "SELECT 99 Kode,'TOTAL ' GroupName,ISNULL(SUM(Price),0) Price, 2 Urutan FROM SarmutMTC1 " +
        //                    ") " +
        //                    "SELECT *,CASE WHEN Price>0 THEN (Price / (Select Price From SarmutMTC2 Where Urutan=2)) ELSE 0 END Prosen " +
        //                    "FROM SarmutMTC2 " +
        //                    "Order By Urutan,Kode";
        //            break;
        //    }
        //    return result;
        //}

        private string DetailBudget(int MaterialCCID)
        {
            string result = string.Empty;
            switch (MaterialCCID)
            {
                case 1:
                    result =
                        "  WITH  DetailBM AS ( " +
                             "   SELECT pd.ID, pd.ItemID,pd.Quantity,pd.AvgPrice,mp.MatCCGroupID,pd.ItemTypeID " +
                             "   FROM PakaiDetail pd " +
                             "   INNER JOIN Pakai p ON p.ID=pd.PakaiID " +
                             "   INNER JOIN MaterialPPGroup AS mp ON mp.ItemID=pd.ItemID AND mp.ItemTypeID=pd.ItemTypeID " +
                             "   WHERE pd.RowStatus>-1 AND mp.MatCCID=1 AND CONVERT(CHAR,p.PakaiDate,112) " +
                             "   Between '" + this.StartPeriode + "' AND '" + this.EndPeriode + "' " +
                             "   AND p.DeptID=2  AND p.Status>1 and mp.rowstatus>-1" +

                             "   UNION ALL " +

                             "   SELECT pd.ID, pd.ItemID,pd.Quantity,pd.AvgPrice,14 MatCCGroupID,pd.ItemTypeID " +
                             "   FROM PakaiDetail pd " +
                             "   INNER JOIN Pakai p ON p.ID=pd.PakaiID    " +
                             "   WHERE pd.RowStatus>-1 AND ItemID NOT IN(SELECT ItemID FROM MaterialPPGroup WHERE MatCCID=1 AND RowStatus>-1) " +
                             "   AND CONVERT(CHAR,p.PakaiDate,112) Between '" + this.StartPeriode + "' AND '" + this.EndPeriode + "'  " +
                             "   AND p.DeptID=2 AND pd.GroupID in(8,9) AND pd.ItemTypeID=1 AND p.Status>1" +
                             " ) " +
                             " ,DetailBM0 AS ( " +
                             " SELECT pd.ID,p.DeptID,p.PakaiDate,pd.ItemID,pd.ItemTypeID,pd.Quantity,p.PlanningID,ISNULL(mp.Planning,2)Planning, " +
                             "   bm.BudgetQty,(select dbo.ItemNameInv(pd.Itemid,1))ItemName " +
                             "   FROM PakaiDetail pd " +
                             "   LEFT JOIN Pakai p ON pd.PakaiID=p.ID " +
                             "   LEFT JOIN MaterialPP mp ON mp.ID=p.PlanningID " +
                             "   LEFT JOIN MaterialPPBudgetBM bm ON bm.ItemID=pd.ItemID AND bm.RunningLine=ISNULL(mp.Planning,2) " +
                             "   WHERE CONVERT(CHAR,p.PakaiDate,112) Between '" + this.StartPeriode + "' AND '" + this.EndPeriode + "' " +
                             "   and bm.RowStatus>-1  and mp.rowstatus>-1 " +
                             "   AND pd.ItemID IN(SELECT ItemID FROM MaterialPPGroup WHERE MaterialPPGroup.MatCCID=1 Group By ItemID) " +
                             "   AND p.Status>-1 AND pd.RowStatus>-1 " +
                             "   AND DeptID=2 " +
                             "), " +
                             " DetailBM1 AS( " +
                             " SELECT D.*,mg.GroupName,MaterialCCID,SortOrder,bm.BudgetQty FROM DetailBM  AS D " +
                             " LEFT JOIN MaterialCCGroup AS mg ON mg.SortOrder=D.MatCCGroupID AND mg.MaterialCCID=1 " +
                             " LEFT JOIN DetailBM0 bm ON bm.ID=d.ID " +
                             " ), " +
                             " DetailBM2 AS( " +
                             " SELECT SortOrder,GroupName,(SELECT Dbo.ItemCodeInv(ItemID,ItemTypeID))ItemCode,ItemID, " +
                             " (SELECT dbo.ItemNameInv(ItemID,ItemTypeID))ItemName,SUM(Quantity)Quantity,(SUM(Quantity)*AvgPrice)Price,BudgetQty " +
                             " FROM DetailBM1 " +
                             " GROUP BY ItemID,ItemTypeID,GroupName,AvgPrice,SortOrder,BudgetQty " +
                             " ) " +
                             ",DetailBM3 AS( " +
                             " SELECT SortOrder,GroupName,ItemCode,ItemName," +
                             " ISNULL(BudgetQty,0)MaxQty" +
                             ",Quantity,Price, 1 Urutan FROM DetailBM2 db " +
                             " /*LEFT JOIN BudgetSP b On b.ItemID=db.ItemID AND b.ItemTypeID=1 */" +
                             " WHERE  SortOrder=" + this.MatCCGroupID +
                             " /*UNION ALL " +
                             " SELECT 9999 SortOrder,'TOTAL 'GroupName,'' ItemCode,'' ItemName,SUM( MaxQty)MaxQty,SUM(Quantity),SUM(Price), 2 Urutan FROM DetailBM2  " +
                             " WHERE SortOrder=" + this.MatCCGroupID +
                             " */),DetailBM4 AS( " +
                             " SELECT * FROM DetailBM3 " +
                             " UNION ALL " +
                             " SELECT 9999 SortOrder,'TOTAL 'GroupName,'' ItemCode,'' ItemName,ISNULL(SUM(MaxQty),0)MaxQty," +
                             " ISNULL(SUM(Quantity),0)Quantity,ISNULL(SUM(Price),0)Price, 2 Urutan FROM DetailBM3  " +
                             ") SELECT * FROM DetailBM4 " +
                             " ORDER BY SortOrder,Urutan,ItemName ";


                    break;
                case 4:
                    result = "WITH SarmutMTC AS ( " +
                             "   SELECT x.*,m.GroupNaME,m.Kode FROM ( " +
                             "   SELECT ls.SarmutID,ls.PakaiNo,ls.DeptiD, pd.ItemID,pd.Quantity,pd.AvgPrice,'Pakai' TipeTrans  " +
                             "   FROM PakaiDetail pd " +
                             "   INNER JOIN Pakai p ON p.ID=pd.PakaiID " +
                             "   INNER JOIN MTC_LapSarmut AS ls ON ls.ItemID=pd.ItemID AND ls.ItemTypeID=pd.ItemTypeID AND ls.PakaiNo=p.PakaiNo " +
                             "   WHERE ls.RowStatus>-1 and pd.RowStatus>-1  AND CONVERT(CHAR,p.PakaiDate,112) Between '" + this.StartPeriode + "' AND '" + this.EndPeriode + "'  " +
                             "   AND p.DeptID in(SELECT SortOrder FROM MaterialCCGroup WHERE MaterialCCID=4 )  AND p.Status>1  " +

                             "   UNION ALL " +
                        //  Tambahan Retur Pakai
                             "   SELECT mt.SarmutID,rp.ReturNo,rp.DeptID,rpd.ItemID,rpd.Quantity,(-1*(rpd.AvgPrice))AvgPrice,'Return' TipeTrans  " +
                             "   FROM ReturPakaiDetail as rpd " +
                             "   INNER JOIN ReturPakai rp ON rp.ID=rpd.ReturID " +
                             "   LEFT JOIN MTC_LapSarmut mt ON mt.ItemID=rpd.ItemID AND mt.ItemTypeID=rpd.ItemTypeID " +
                        //  Tambahan Link
                             "   INNER JOIN Pakai as pk ON pk.PakaiNo=rp.PakaiNo and pk.ItemTypeID=rp.ItemTypeID and mt.PakaiNo=pk.PakaiNo " +
                             "   LEFT JOIN MTC_GroupSarmut as gs ON gs.ID=mt.SarmutID " +
                        //  End Tambahan
                             "   WHERE  CONVERT(CHAR,rp.ReturDate,112) Between '" + this.StartPeriode + "' AND '" + this.EndPeriode + "' AND " +
                             "   rp.DeptID in(SELECT SortOrder FROM MaterialCCGroup WHERE MaterialCCID=4 ) and rpd.RowStatus>-1 And rp.Status>-1 " +
                             "   AND rpd.ItemID in(Select mt.ItemID From MTC_LapSarmut mt WHERE mt.ItemTypeID=rpd.ItemTypeID Group By mt.ItemID) " +
                             "   ) AS X " +
                             "   LEFT JOIN vw_SarmutGroup AS s ON s.SarmutID=x.SarmutID " +
                             "   LEFT JOIN MaterialMTCGroup AS m ON m.ID=s.ID " +
                             "   WHERE x.DeptID=(SELECT SortOrder FROM MaterialCCGroup WHERE MaterialCCID=4 AND ID=" + this.MatCCGroupID + ") AND m.RowStatus>-1 " +
                            "), " +
                            "SarmutMTC1 AS ( " +
                            "SELECT m.Kode, m.GroupName,ISNULL(SUM((Quantity*AvgPrice)),0) Price " +
                            "FROM MaterialMTCGroup AS m " +
                            "LEFT JOIN SarmutMTC AS sm ON sm.Kode=m.Kode " +
                            "WHERE m.RowStatus>-1 " +
                            "GROUP BY m.Kode, m.GroupName " +
                            "), " +
                            "SarmutMTC2 AS ( " +
                            "SELECT Kode,GroupName,ISNULL(Price,0)Price,1 Urutan FROM SarmutMTC1 " +
                            "UNION " +
                            "SELECT 99 Kode,'TOTAL ' GroupName,ISNULL(SUM(Price),0) Price, 2 Urutan FROM SarmutMTC1 " +
                            ") " +
                            "SELECT *,CASE WHEN Price>0 THEN (Price / (Select Price From SarmutMTC2 Where Urutan=2)) ELSE 0 END Prosen " +
                            "FROM SarmutMTC2 " +
                            "Order By Urutan,Kode";
                    break;
            }
            return result;
        }

        public void GetCostCenter(DropDownList ddl)
        {
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem(" ", "0"));
            foreach (CostCenter cs in this.Retrieve())
            {
                ddl.Items.Add(new ListItem(cs.GroupName.ToString(),cs.ID.ToString()));
            }

        }
        public ArrayList GetMaterialGroup(string CCID)
        {
            arrData = new ArrayList();
            string strSQL = "SELECT * FROM MaterialCCGroup WHERE RowStatus>-1 AND MaterialCCID=" + CCID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (sdr.HasRows && da.Error == string.Empty)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateObject(sdr));
                }
            }
            return arrData;
        }
        public ArrayList GetMaterialPPGroup()
        {
            arrData = new ArrayList();
            string strSQL = "SELECT *,(SELECT dbo.ItemCodeInv(ItemID,ItemTypeID))ItemCode,(SELECT dbo.ItemNameInv(ItemID,ItemTypeID))ItemName "+
                            "FROM MaterialPPGroup WHERE RowStatus>-1 ";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (sdr.HasRows && da.Error == string.Empty)
            {
                while (sdr.Read())
                {
                    arrData.Add(GeneratePPObject(sdr));
                }
            }
            return arrData;
        }
        public ArrayList GetMaterialPPGroup(string MatCCID)
        {
            arrData = new ArrayList();
            string strSQL = "SELECT *,(SELECT dbo.ItemCodeInv(ItemID,ItemTypeID))ItemCode,(SELECT dbo.ItemNameInv(ItemID,ItemTypeID))ItemName "+
                            "FROM MaterialPPGroup WHERE RowStatus>-1 AND MatCCID=" + MatCCID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (sdr.HasRows && da.Error == string.Empty)
            {
                while (sdr.Read())
                {
                    arrData.Add(GeneratePPObject(sdr));
                }
            }
            return arrData;
        }
        public ArrayList GetMaterialPPGroup(string MatCCID, string MatCCGroupID)
        {
            arrData = new ArrayList();
            string strSQL = "SELECT *,(SELECT dbo.ItemCodeInv(ItemID,ItemTypeID))ItemCode,(SELECT dbo.ItemNameInv(ItemID,ItemTypeID))ItemName "+
                            "FROM MaterialPPGroup WHERE RowStatus>-1 AND MatCCID=" + MatCCID + " AND MatCCGroupID=" + MatCCGroupID;

            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (sdr.HasRows && da.Error == string.Empty)
            {
                while (sdr.Read())
                {
                    arrData.Add(GeneratePPObject(sdr));
                }
            }
            return arrData;
        }
        private CostCenter GeneratePPObject(SqlDataReader sdr)
        {
            objCost = new CostCenter();
            objCost.ID = int.Parse(sdr["ID"].ToString());
            objCost.ItemCode = sdr["ItemCode"].ToString();
            objCost.ItemName = sdr["ItemName"].ToString();
            objCost.ItemID = int.Parse(sdr["ItemID"].ToString());
            objCost.ItemTypeID = int.Parse(sdr["ItemTypeID"].ToString());
            return objCost;
        }
        private CostCenter GenerateObject(SqlDataReader sdr)
        {
            objCost = new CostCenter();
            objCost.ID = int.Parse(sdr["ID"].ToString());
            objCost.GroupName = sdr["GroupName"].ToString();
            return objCost;
        }
        private CostCenter GenerateObject(SqlDataReader sdr, CostCenter DomainCostCenter)
        {
            objCost = (CostCenter)DomainCostCenter;
            objCost.SortOrder = int.Parse(sdr["SortOrder"].ToString());
            objCost.RunningLine = sdr["RunLine"].ToString();
            objCost.CostBudget = decimal.Parse(sdr["CostValue"].ToString());
            objCost.CostActual = decimal.Parse(sdr["CostActual"].ToString());
            objCost.Prosen = (sdr["Prosen"] == DBNull.Value) ? 0 : decimal.Parse(sdr["Prosen"].ToString());
            return objCost;
        }
        private CostCenter DetailObject(SqlDataReader sdr, int MaterialCCID)
        {
            objCost = new CostCenter();
            switch (MaterialCCID)
            {
                case 1:
                    objCost.ItemCode = sdr["ItemCode"].ToString();
                    objCost.ItemName = sdr["ItemName"].ToString();
                    objCost.CostBudget = decimal.Parse(sdr["Quantity"].ToString());
                    objCost.CostActual = decimal.Parse(sdr["Price"].ToString());
                    objCost.QtyBudget = decimal.Parse(sdr["MaxQty"].ToString());
                    break;
                case 4:
                    objCost.Kode = int.Parse(sdr["Kode"].ToString());
                    objCost.ItemName = sdr["GroupName"].ToString();
                    objCost.CostBudget = decimal.Parse(sdr["Price"].ToString());
                    objCost.CostActual = decimal.Parse(sdr["Prosen"].ToString());
                    break;
            }
            return objCost;
        }
        /*
         * Matrix for Logistik PJ
         * Added on 12-03-2017
         */
        public int InsertNilai(object objDomain)
        {
            int result = -1;
            ZetroLib zl = new ZetroLib();
            zl.hlp = new CostPJ();
            zl.Criteria = "Bulan,Tahun,Urutan,Pengiriman,JenisPacking,Nilai,Approval,RowStatus,ApprovalBy,CreatedBy,CreatedTime";
            zl.StoreProcedurName = "spMaterialCCNilaiPJ_Insert1";
            zl.TableName = "MaterialCCNilaiPJ";
            zl.Option = "Insert";
            zl.ReturnID = false;
            string rst = zl.CreateProcedure();
            if (rst == string.Empty)
            {
                zl.hlp = objDomain;
                result = zl.ProcessData();
            }
            return result;
        }
        public int InsertNilai(object objDomain,bool Update)
        {
            int result = -1;
            ZetroLib zl = new ZetroLib();
            zl.hlp = new CostPJ();
            zl.Criteria = "ID,Bulan,Tahun,Urutan,Pengiriman,JenisPacking,Nilai,Approval,ApprovalBy,RowStatus,CreatedBy,CreatedTime";
            zl.StoreProcedurName = "spMaterialCCNilaiPJ_Update1";
            zl.TableName = "MaterialCCNilaiPJ";
            zl.Option = "Update";
            zl.ReturnID = false;
            string rst = zl.CreateProcedure();
            if (rst == string.Empty)
            {
                zl.hlp = objDomain;
                result = zl.ProcessData();
            }
            return result;
        }
        private string MatrixQuery()
        {
            string result = " WITH CostPJ AS ( " +
                           "     SELECT mx.*,mg.GroupName " +
                           "     FROM MaterialCCMatrixPJ mx " +
                           "     LEFT JOIN MaterialCCGroup mg ON mg.ID=mx.MaterialCCGroupID " +
                           "     WHERE mx.rowstatus>-1 and mx.Tahun =" + this.StartPeriode + 
                           " and bulan =(select top 1 bulan from MaterialCCMatrixPJ where Tahun=" + this.StartPeriode + 
                           "  and Bulan <= " + this.StartBulan + " order by Bulan desc) " +
                           " ) " +
                           " ,CostPJ_1 AS ( " +
                           " SELECT Urutan,Pengiriman,JenisPacking,ISNULL([15],0)PKA,ISNULL([16],0)BKA,ISNULL([17],0)BEY,ISNULL([18],0)KPL, " +
                           " ISNULL([19],0)KKR,ISNULL([20],0)PPE FROm CostPJ p " +
                           " PIVOT (SUM(p.Quantity) FOR p.MaterialCCGroupID IN([15],[16],[17],[18],[19],[20])) AS pvt " +
                           " ) " +
                           " SELECT Urutan,Pengiriman,JenisPacking,SUM(PKA)PKA,SUM(BKA)BKA,SUM(BEY)BEY,SUM(KPL)KPL,SUM(KKR)KKR,SUM(PPE)PPE " +
                           " FROM CostPJ_1 GROUP BY JenisPacking,Pengiriman,Urutan " +
                           " ORDER BY Urutan,JenisPacking,Pengiriman";
            return result;
        }
        private string MatrixQuery1(string pengiriman)
        {
            string result = " WITH CostPJ AS ( " +
                           "     SELECT mx.*,mg.GroupName " +
                           "     FROM MaterialCCMatrixPJ mx " +
                           "     LEFT JOIN MaterialCCGroup mg ON mg.ID=mx.MaterialCCGroupID " +
                           "     WHERE mx.pengiriman='" + pengiriman.Trim() + "' and mx.rowstatus>-1 and mx.Tahun =" + this.StartPeriode +
                           " and bulan =(select top 1 bulan from MaterialCCMatrixPJ where Tahun=" + this.StartPeriode +
                           "  and Bulan <= " + this.StartBulan + " order by Bulan desc) " +
                           " ) " +
                           " ,CostPJ_1 AS ( " +
                           " SELECT Urutan,Pengiriman,JenisPacking,ISNULL([15],0)PKA,ISNULL([16],0)BKA FROm CostPJ p " +
                           " PIVOT (SUM(p.Quantity) FOR p.MaterialCCGroupID IN([15],[16])) AS pvt " +
                           " ) " +
                           " SELECT Urutan,Pengiriman,JenisPacking,SUM(PKA)PKA,SUM(BKA)BKA " +
                           " FROM CostPJ_1 GROUP BY JenisPacking,Pengiriman,Urutan " +
                           " ORDER BY Urutan,JenisPacking,Pengiriman";
            return result;
        }
        public ArrayList RetrieveMatrixPJ()
        {
            arrData = new ArrayList();
            string strsql = this.MatrixQuery();
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(this.MatrixQuery());
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateObjectPJ(sdr));
                }
            }
            return arrData;
        }
        public ArrayList RetrieveMatrixPJ1(string pengiriman)
        {
            arrData = new ArrayList();
            string strsql = this.MatrixQuery1(pengiriman);
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(this.MatrixQuery1(pengiriman));
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateObjectPJ1(sdr));
                }
            }
            return arrData;
        }
        public CostPJ RetrieveNilai()
        {
            CostPJ cpj = new CostPJ();
            string strSQL = "SELECT top 1 * FROM MaterialCCNilaiPJ WHERE RowStatus>-1 " + this.Criteria + " order by createdtime desc";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    cpj = GenerateObjectPJ(sdr, true);
                }
            }
            return cpj;
        }
        public CostPJ RetrieveNilaiP()
        {
            CostPJ cpj = new CostPJ();
            string strSQL = "SELECT 0 ID, isnull(sum(jmlPalet),0) Nilai FROM T3_kirimdetail WHERE RowStatus>-1 " + this.Criteria;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    cpj = GenerateObjectPJ(sdr, true);
                }
            }
            return cpj;
        }
        /// <summary>
        /// Retrieves jumlah qty spn.
        /// </summary>
        /// <param name="SPB">if set to <c>true</c> [SPB].</param>
        /// <returns></returns>
        public decimal RetrieveNilai(bool SPB)
        {
            decimal result = 0;
            string strSQL = "SELECT SUM(pd.Quantity)Jml " +
                            "FROM PakaiDetail pd " +
                            "LEFT JOIN Pakai p ON p.ID=pd.PakaiID " +
                            "LEFT JOIN MaterialPPGroup mp ON mp.ItemID=pd.ItemID and MP.ItemTypeID=pd.ItemTypeID " +
                            "WHERE mp.MatCCID=3 " + this.Criteria +
                            "AND pd.RowStatus>-1 AND p.Status>-1 AND mp.RowStatus>-1 and p.DeptID=6 " +
                            "Group By mp.MatCCID,mp.MatCCGroupID ";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = decimal.Parse(sdr["Jml"].ToString());
                }
            }
            return result;
        }
        private CostPJ GenerateObjectPJ(SqlDataReader sdr)
        {
            CostPJ cpj = new CostPJ();
            cpj.Urutan = int.Parse(sdr["Urutan"].ToString());
            cpj.Pengiriman = sdr["Pengiriman"].ToString();
            cpj.JenisPacking = sdr["JenisPacking"].ToString();
            cpj.PKA = decimal.Parse(sdr["PKA"].ToString());
            cpj.BKA = decimal.Parse(sdr["BKA"].ToString());
            cpj.BEY = decimal.Parse(sdr["BEY"].ToString());
            cpj.KPL = decimal.Parse(sdr["KPL"].ToString());
            cpj.KKR = decimal.Parse(sdr["KKR"].ToString());
            cpj.PPE = decimal.Parse(sdr["PPE"].ToString());
            return cpj;
        }
        private CostPJ GenerateObjectPJ1(SqlDataReader sdr)
        {
            CostPJ cpj = new CostPJ();
            cpj.Urutan = int.Parse(sdr["Urutan"].ToString());
            cpj.Pengiriman = sdr["Pengiriman"].ToString();
            cpj.JenisPacking = sdr["JenisPacking"].ToString();
            cpj.PKA = decimal.Parse(sdr["PKA"].ToString());
            cpj.BKA = decimal.Parse(sdr["BKA"].ToString());
            return cpj;
        }
        private CostPJ GenerateObjectPJ(SqlDataReader sdr,bool detail)
        {
            CostPJ cpj = new CostPJ();
            cpj.ID = int.Parse(sdr["ID"].ToString());
            cpj.Nilai = decimal.Parse(sdr["Nilai"].ToString());
            return cpj;
        }

        /*
         * Matrix for Finishing
         * Added on 10-05-2017
         */
        public int InsertPartNo(object objDomain)
        {
            int result = -1;
            ZetroLib zl = new ZetroLib();
            zl.hlp = new CostFIN();
            zl.Criteria = "MatCCMatrixFinID,PartNo,PartNoID,Lokasi,LokasiID,RowStatus,CreatedBy,CreatedTime";
            zl.StoreProcedurName = "spMaterialCCMatrixFINProduk_Insert";
            zl.TableName = "MaterialCCMatrixFINProduk";
            zl.Option = "Insert";
            zl.ReturnID = false;
            string rst = zl.CreateProcedure();
            if (rst == string.Empty)
            {
                zl.hlp = objDomain;
                result = zl.ProcessData();
            }
            return result;
        }
        public int InsertPartNo(object objDomain,bool Updated)
        {
            int result = -1;
            ZetroLib zl = new ZetroLib();
            zl.hlp = new CostFIN();
            zl.Criteria = "ID,RowStatus,CreatedBy,CreatedTime";
            zl.StoreProcedurName = "spMaterialCCMatrixFINProduk_Update";
            zl.TableName = "MaterialCCMatrixFINProduk";
            zl.Option = "Update";
            zl.ReturnID = false;
            string rst = zl.CreateProcedure();
            if (rst == string.Empty)
            {
                zl.hlp = objDomain;
                result = zl.ProcessData();
            }
            return result;
        }
        
        public ArrayList RetrievePartNo()
        {
            arrData = new ArrayList();
            string strSQL = "SELECT ID, MatCCMatrixFinID, PartNo, isnull(PartNoID,0)PartNoID, Lokasi, isnull(LokasiID,0)LokasiID, RowStatus, CreatedBy, CreatedTime FROM MaterialCCMatrixFINProduk WHERE RowStatus>-1 " + this.Criteria;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateObjectFin(sdr));
                }
            }
            return arrData;
        }
        public CostFIN RetrievePartNo(bool detail)
        {
            CostFIN cfn = new CostFIN();
            string strSQL = "SELECT ID, MatCCMatrixFinID, PartNo, isnull(PartNoID,0)PartNoID, Lokasi, isnull(LokasiID,0)LokasiID, RowStatus, CreatedBy, CreatedTime FROM MaterialCCMatrixFINProduk  WHERE RowStatus>-1 " + this.Criteria;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    cfn = (GenerateObjectFin(sdr));
                }
            }
            return cfn;
        }
        /// <summary>
        /// Generates the object fin.
        /// </summary>
        /// <param name="sdr">Data Reader</param>
        /// <returns>CostFIN Objecr</returns>
        private CostFIN GenerateObjectFin(SqlDataReader sdr)
        {
            CostFIN cfn = new CostFIN();
            cfn.ID = int.Parse(sdr["ID"].ToString());
            cfn.MatCCMatrixFinID = int.Parse(sdr["MatCCMatrixFinID"].ToString());
            cfn.PartNo = sdr["PartNo"].ToString();
            cfn.PartNoID = int.Parse(sdr["PartNoID"].ToString());
            cfn.Lokasi = sdr["Lokasi"].ToString();
            cfn.LokasiID = int.Parse(sdr["LokasiID"].ToString());
            cfn.RowStatus = int.Parse(sdr["RowStatus"].ToString());
            return cfn;
        }
        /// <summary>
        /// Queries for budgeting finishing.
        /// </summary>
        /// <returns></returns>
        private string QueryFinishing()
        {
            string strSQl = "WITH Finishing AS( " +
                            "    SELECT mp.ID,mp.MatCCID,mp.MatCCGroupID,mp.ItemID,(SELECT dbo.ItemNameInv(ItemID,ItemTypeID))ItemName, " +
                            "    (SELECT dbo.SatuanInv(ItemID,ItemTypeID))UomDesc,mp.ItemTypeID,mg.GroupName " +
                            "    FROM MaterialPPGroup mp " +
                            "    LEFT JOIN MaterialCCGroup mg ON mg.ID=mp.MatCCGroupID AND mg.MaterialCCID=mp.MatCCID " +
                            "    WHERE mp.MatCCID=2 " +
                            ") " +
                            ", FinishingPakai AS " +
                            "( " +
                            "    SELECT pd.ItemID,pd.ItemTypeID,SUM(pd.Quantity)Quantity,MAX(pd.BudgetQty)BudgetQty,AVG(pd.AvgPrice)AvgPrice " +
                            "    FROM Pakai p " +
                            "    LEFT JOIN PakaiDetail pd ON pd.PakaiID=p.ID " +
                            "    WHERE Status>1 AND pd.RowStatus>-1 AND DeptID=3 AND Month(PakaiDate)=" + this.Bulan + " AND YEAR(PakaiDate)=" + this.Tahun +
                            "    AND pd.GroupID in(8,9,2) " +
                            "    GROUP By ItemID,pd.ItemTypeID " +
                            "), " +
                            " Finishing2 AS (  " +
                            "    SELECT F.ID, " +
                            "     ISNULL(MatCCID,2)MatCCID,ISNULL(MatCCGroupID,30)MatCCGroupID,ISNULL(p.ItemID,f.ItemID)ItemID, " +
                            "    (SELECT dbo.ItemNameInv(ISNULL(f.ItemID,p.itemID),f.ItemTypeID))ItemName,UomDesc,ISNULL(GroupName,'LAIN - LAIN')GroupName, " +
                            "    ISNULL(p.Quantity,0)Quantity,p.BudgetQty,p.AvgPrice,ISNULL(mb.Barang,0)Barang,ISNULL(mb.Lembar,0)Lembar  " +
                            "    FROM Finishing F " +
                            "    FULL OUTER JOIN FinishingPakai p ON p.ItemID=F.ItemID " +
                            "    LEFT JOIN MAterialCCBudgetFinishing mb ON mb.MatCCMatrixFinID=f.ID AND mb.RowStatus>-1 " +
                            //"    ) " +
                            ") " +
                            "SELECT ISNULL(ID,0)MatCCID,MatCCGroupID,GroupName,ItemID,ISNULL(ItemName,(SELECT dbo.ItemNameInv(ItemID,1)))ItemName,UomDesc," +
                            "SUM(Quantity)Quantity,1 Urutan  " +
                            "FROM Finishing2 Group By ItemID,ItemName,UomDesc,MatccGroupID,ID,GroupName,MatCCID " +
                            "UNION SELECT  (15*MatCCGroupID) MatCCID,MatCCGroupID, GroupName,0 ItemID,GroupName ItemName,'' UomDesc,SUM(Quantity)Quantity, 2 Urutan  " +
                            "FROM Finishing2 Group By GroupName,MatccGroupID " +
                            "ORDER By MatccGroupID,Urutan,ItemID";
            return strSQl;
        }
        private string QueryFinishing(bool New)
        {
              string strSQL = 
                  "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempQFinishing2]') AND type in (N'U')) DROP TABLE [dbo].tempQFinishing2 " +
                  "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempQFinishing3]') AND type in (N'U')) DROP TABLE [dbo].tempQFinishing3 " +
                  "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpPartnoOut]') AND type in (N'U')) DROP TABLE [dbo].tmpPartnoOut  " +
                  "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpPMAterialCCBudgetFinishing]') AND type in (N'U')) DROP TABLE [dbo].tmpPMAterialCCBudgetFinishing " +
                  "select ROW_NUMBER() OVER (ORDER BY MatCCMatrixFinID ,tahun desc,bulan desc) nomor, id,tahun,bulan,MatCCMatrixFinID  into tmpPMAterialCCBudgetFinishing " +
                  "from MAterialCCBudgetFinishing where tahun<=" + this.Tahun + " and Bulan<=" + this.Bulan + "  order by MatCCMatrixFinID ,tahun desc,bulan desc " +
                  ";WITH Finishing AS( " +
                  "     SELECT mp.ID, mp.MatCCID,mp.MatCCGroupID,mp.ItemID,REPLACE(REPLACE((SELECT dbo.ItemNameInv(ItemID,ItemTypeID)),CHAR(13),''),CHAR(10),'')ItemName, " +
                  "      (SELECT dbo.SatuanInv(ItemID,ItemTypeID))UomDesc,mp.ItemTypeID,mg.GroupName " +
                  "      FROM MaterialPPGroup mp " +
                  "      LEFT JOIN MaterialCCGroup mg ON mg.ID=mp.MatCCGroupID AND mg.MaterialCCID=mp.MatCCID " +
                  "      WHERE mp.MatCCID=2 AND mp.RowStatus>-1 AND mg.RowStatus>-1  and  isnull(mp.tahun,0)<=" + this.Tahun + " and isnull(mp.Bulan,0)<=" + this.Bulan + "" +
                  "  ) " +
                  "  , FinishingPakai AS " +
                  "  ( " +
                  "      SELECT pd.ItemID,pd.ItemTypeID,SUM(pd.Quantity)Quantity,MAX(pd.BudgetQty)BudgetQty,(SUM(pd.Quantity*pd.AvgPrice))AvgPrice " +
                  "      FROM Pakai p " +
                  "      LEFT JOIN PakaiDetail pd ON pd.PakaiID=p.ID " +
                  "      WHERE Status>1 AND pd.RowStatus>-1 AND DeptID=3 AND Month(PakaiDate)=" + this.Bulan + " AND YEAR(PakaiDate)=" + this.Tahun +
                  "      AND pd.GroupID in(8,9,5) " +
                  "      GROUP By ItemID,pd.ItemTypeID/*,pd.AvgPrice*/ " +
                  "  ), " +
                  "  Finishing2 AS ( " +
                  "  SELECT ISNULL(F.ID,450)ID, " +
                  "   ISNULL(MatCCID,2)MatCCID,ISNULL(MatCCGroupID,100)MatCCGroupID,ISNULL(p.ItemID,f.ItemID)ItemID, " +
                  "  (SELECT dbo.ItemNameInv(ISNULL(f.ItemID,p.itemID),P.ItemTypeID))ItemName,UomDesc,ISNULL(GroupName,'LAIN - LAIN')GroupName, " +
                  "  ISNULL(p.Quantity,0)Quantity,p.BudgetQty,(ISNULL((p.AvgPrice),0))AvgPrice,ISNULL(mb.Barang,0)Barang,ISNULL(mb.Lembar,0)Lembar, " +
                  "  ISNULL(mb.RupiahPerBln,0)RupiahPerBln  " +
                  "  FROM Finishing F " +
                  "  FULL OUTER JOIN FinishingPakai p ON p.ItemID=F.ItemID " +
                  "  LEFT JOIN MAterialCCBudgetFinishing mb ON mb.MatCCMatrixFinID=ISNULL(f.ID,450) AND mb.RowStatus>-1  and mb.id in ( " +
	              "select ID from ( " +
		          "  select *, case when (select count(MatCCMatrixFinID) from tmpPMAterialCCBudgetFinishing where nomor<A.nomor and MatCCMatrixFinID=A.MatCCMatrixFinID)=0 then MatCCMatrixFinID else 0  end cek from ( " +
		          "  select ROW_NUMBER() OVER (ORDER BY MatCCMatrixFinID ,tahun desc,bulan desc)nomor, id,tahun,bulan,MatCCMatrixFinID   " +
                  "  from MAterialCCBudgetFinishing where tahun<=" + this.Tahun + " and Bulan<=" + this.Bulan + " )A )B where cek>0 ))  " +
                  "select * into tempQFinishing2 from  Finishing2" +
                  "      SELECT p.MatCCMatrixFinID, t1.PartNoID,p.PartNo,Lokasi,LokID,SUM(Qty)Qty  into tmpPartnoOut FROM  dbo.vw_MapingPartnoBudgetFin t1 " +
                  "      inner JOIN (select distinct MatCCMatrixFinID,PartNo,PartNoID,LokasiID,Lokasi,RowStatus from MaterialCCMatrixFINProduk) p ON p.PartNoID=t1.PartNoID AND p.LokasiID=t1.LokID " +
                  "      WHERE MONTH(t1.TglTrans)=" + this.Bulan + " AND YEAR(t1.TglTrans)=" + this.Tahun + " AND RowStatus>-1 " +
                  "      GROUP BY t1.PartNoID,PartNo,Lokasi,LokID,MatCCMatrixFinID " +
                  "  SELECT ISNULL(ID,0)MatCCID,MatccGroupID,GroupName,ItemID,ISNULL(ItemName,(SELECT dbo.ItemNameInv(ItemID,1)))ItemName, " +
                  "  (SELECT dbo.SatuanInv(ItemID,1))UomDesc, " +
                  "  /*CASE WHEN SUM(f2.Quantity)>0 THEN */(SELECT ISNULL(SUM(p.Qty),0) FROM tmpPartnoOut p WHERE p.MatCCMatrixFinID=f2.ID)/*ELSE 0 END*/ ProdukOut,  " +
                  "  SUM(Quantity)Quantity, Barang,Lembar,AvgPrice,RupiahPerBln,1 Urutan into tempQFinishing3 " +
                  "  FROM tempQFinishing2 f2    " +
                  "  /*LEFT JOIN PartNoOut p ON p.MatCCMatrixFinID=f2.ID*/    " +
                  "  Group By ItemID,ItemName,MatccGroupID,GroupName,ID,Barang,Lembar,AvgPrice,RupiahPerBln  " +
                  "  SELECT * FROM ( " +
                  "select  MatCCID, MatccGroupID, GroupName, ItemID, ItemName, UomDesc, CAST( ProdukOut1 as int) ProdukOut, Quantity, Barang,  " +
	              "  Lembar, AvgPrice, RupiahPerBln, Urutan  from ( " +
                  "  select *,case when (select SUM(produkout) from tempQFinishing3 where matccgroupid=B.matccgroupid)>0 and totPakai>0  then " +
	              "  (((quantity/totPakai))*(select SUM(produkout) from tempQFinishing3 where matccgroupid=B.matccgroupid) ) else 0 end ProdukOut1 from ( " +
	              "  select *,(select SUM(quantity) from tempQFinishing3 where matccgroupid=A.matccgroupid)TotPakai from ( " +
                  "      SELECT * FROM tempQFinishing3  " +
	              "  )A)B)C  " +
	              "  UNION ALL        " +
                  "  SELECT (20*MatccGroupID) MatCCID, MatccGroupID, GroupName,0 ItemID,'Sub Total ' + GroupName ItemName,'' UomDesc, SUM(ProdukOut)ProdukOut,    " +    
	              " SUM(Quantity)Quantity,   0 Barang,0 Lembar,SUM(AvgPrice),Max(RupiahPerBln), 2 Urutan FROM tempQFinishing3 f2 Group By GroupName,MatccGroupID " +
                  " UNION ALL " +
	              "  SELECT (MatccGroupID) MatCCID, MatccGroupID, GroupName,0 ItemID,GroupName ItemName,'' UomDesc,  0 ProdukOut,  " +
                  "  0 Quantity,   0 Barang,0 Lembar,0,0, 0 Urutan FROM tempQFinishing3 f2 Group By GroupName,MatccGroupID " +
                  "  ) as x   " +
                  "   ORDER By MatccGroupID,Urutan,MatCCID,ItemID " +
                  "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempQFinishing2]') AND type in (N'U')) DROP TABLE [dbo].tempQFinishing2 " +
                  "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempQFinishing3]') AND type in (N'U')) DROP TABLE [dbo].tempQFinishing3 " +
                  "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tmpPartnoOut]') AND type in (N'U')) DROP TABLE [dbo].tmpPartnoOut  " ;
          return strSQL;
        }
        private string QueryFinishingPart()
        {
            string strSQL = ",PartNoBudget AS " +
                          "  ( SELECT ID, MatCCMatrixFinID, PartNo, isnull(PartNoID,0)PartNoID, Lokasi, isnull(LokasiID,0)LokasiID, RowStatus, CreatedBy, CreatedTime FROM MaterialCCMatrixFINProduk  WHERE RowStatus>-1  ), " +
                          "  PartNoOut AS " +
                          "  ( " +
                          "      SELECT p.MatCCMatrixFinID, t1.PartNoID,p.PartNo,Lokasi,LokID,SUM(Qty)Qty FROM  dbo.vw_MapingPartnoBudgetFin t1 " +
                          "      LEFT JOIN PartNoBudget p ON p.PartNoID=t1.PartNoID AND p.LokasiID=t1.LokID " +
                          "      WHERE MONTH(t1.TglTrans)=" + this.Bulan + " AND YEAR(t1.TglTrans)=" + this.Tahun + " AND RowStatus>-1 " +
                          "      GROUP BY t1.PartNoID,PartNo,Lokasi,LokID,MatCCMatrixFinID " +
                          "  ) " +
                          " /* SELECT MatCCMatrixFinID,SUM(Qty)Jumlah, 1 Urutan FROM PartNoOut  GROUP BY MatCCMatrixFinID*/ ";
            return strSQL;
        }
        public ArrayList RetrieveBudgetFinishing()
        {
            arrData = new ArrayList();
            string strSQL = this.QueryFinishing();
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty || sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GeneratObjectFinishing(sdr));
                }
            }
            return arrData;
        }
        public ArrayList RetrieveBudgetFinishing(bool budget)
        {
            arrData = new ArrayList();
            string strSQL = this.QueryFinishing(true);
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty || sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GeneratObjectFinishing(sdr,GeneratObjectFinishing(sdr)));
                }
            }
            return arrData;
        }

        public int GetBudgetSarungTanganB8(string tahun, string bulan)
        {
            int Qty = 0;
            string strSQL = "select isnull(sum(qtystB8),0) Qty from [MaterialSTMSBudgetFIN] where tahun=" + tahun + "and bulan=" + bulan;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty || sdr.HasRows)
            {
                while (sdr.Read())
                {
                    Qty = int.Parse(sdr["qty"].ToString());
                }
            }
            return Qty;
        }

        public int GetBudgetSarungTangan(string tahun, string bulan)
        {
            int Qty = 0;
            string strSQL = "select isnull(sum(qtyst),0) Qty from [MaterialSTMSBudgetFIN] where tahun=" + tahun + "and bulan=" + bulan;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty || sdr.HasRows)
            {
                while (sdr.Read())
                {
                    Qty = int.Parse(sdr["qty"].ToString());
                }
            }
            return Qty;
        }

        public int GetBudgetSarungTangan1(string tahun, string bulan)
        {
            int Qty = 0;
            string strSQL = "select isnull(sum(QtySTB8),0) Qty from [MaterialSTMSBudgetFIN] where tahun=" + tahun + "and bulan=" + bulan;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty || sdr.HasRows)
            {
                while (sdr.Read())
                {
                    Qty = int.Parse(sdr["qty"].ToString());
                }
            }
            return Qty;
        }

        public decimal GetBudgetlainfin()
        {
            decimal  Qty = 0;
            string strSQL = "select top 1 rupiahperbln Qty from [MAterialCCBudgetFinishing] where matccmatrixfinid=450 and rowstatus>-1";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty || sdr.HasRows)
            {
                while (sdr.Read())
                {
                    Qty = decimal.Parse(sdr["Qty"].ToString());
                }
            }
            return Qty;
        }
        public int GetBudgetMasker(string tahun, string bulan)
        {
            int Qty = 0;
            string strSQL = "select isnull(sum(qtyms),0) Qty from [MaterialSTMSBudgetFIN] where tahun=" + tahun + "and bulan=" + bulan;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty || sdr.HasRows)
            {
                while (sdr.Read())
                {
                    Qty = int.Parse(sdr["qty"].ToString());
                }
            }
            return Qty;
        }

        public int GetBudgetMaskerKT(string tahun, string bulan)
        {
            int Qty = 0;
            string strSQL = "select isnull(sum(qtymskt),0) Qty from [MaterialSTMSBudgetFIN] where tahun=" + tahun + "and bulan=" + bulan;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty || sdr.HasRows)
            {
                while (sdr.Read())
                {
                    Qty = int.Parse(sdr["qty"].ToString());
                }
            }
            return Qty;
        }

        private CostFIN GeneratObjectFinishing(SqlDataReader sdr)
        {
            CostFIN cfn = new CostFIN();
            cfn.MaterialGroupID = int.Parse(sdr["MatCCGroupID"].ToString());
            cfn.GroupName = sdr["GroupName"].ToString();
            cfn.Unit = sdr["UomDesc"].ToString();
            cfn.ItemName = sdr["ItemName"].ToString();
            cfn.ItemID = int.Parse(sdr["ItemID"].ToString());
            cfn.Quantity = decimal.Parse(sdr["Quantity"].ToString());
            cfn.RowStatus = int.Parse(sdr["Urutan"].ToString());
            cfn.ItemCode = GetItemCode(sdr["ItemID"].ToString());
            cfn.ID = int.Parse(sdr["MatCCID"].ToString());
            return cfn;
        }
        private CostFIN GeneratObjectFinishing(SqlDataReader sdr, CostFIN cf)
        {
            CostFIN cfn = (CostFIN)cf;
            cfn.Barang = decimal.Parse(LoadBudget(sdr["MatCCID"].ToString(), "Barang"));
            cfn.Lembar = decimal.Parse(LoadBudget(sdr["MatCCID"].ToString(), "Lembar"));
            cfn.MaterialCCID = int.Parse(LoadBudget(sdr["MatCCID"].ToString(), "ID"));
            cfn.RupiahPerBln = decimal.Parse(LoadBudget(sdr["MatCCID"].ToString(), "RupiahPerBln"));
            //cfn.Barang = decimal.Parse(sdr["Barang"].ToString());
            //cfn.Lembar = decimal.Parse(sdr["Lembar"].ToString());
            //cfn.MaterialCCID = int.Parse(sdr["MatCCID"].ToString());
            //cfn.RupiahPerBln = decimal.Parse(sdr["RupiahPerBln"].ToString());
            cfn.Price = decimal.Parse(sdr["AvgPrice"].ToString());
            cfn.ProdukOut = decimal.Parse(sdr["ProdukOut"].ToString());
            return cfn;
        }
        private string GetItemCode(string ID)
        {
            InventoryFacade ivd = new InventoryFacade();
            Inventory i = new Inventory();
            i = ivd.RetrieveById(int.Parse(ID));
            return i.ItemCode;
        }
        public int InsertBudgetFin(ArrayList arrData)
        {
            int result = 0;
            foreach (CostFIN cs in arrData)
            {
                CostFIN c = new CostFIN();
                c = cs;
                result = (cs.ID == 0) ? InsertBudgetFin(c, true) : InsertBudgetFin(c, false);
            }
            return result;
        }
        //public string Criteria { get; set; }
        private string LoadBudget(string ID, string Field)
        {
            string result = "0";
            string strSQL = "SELECT top 1 " + Field + " FROM MAterialCCBudgetFinishing WHERE " +
                "cast(tahun as varchar(4))+ REPLACE(STR(Bulan, 2), SPACE(1), '0') <='"+ this.Tahun.ToString().Trim() + 
                this.Bulan.ToString().PadLeft(2,'0')  +"' and RowStatus>-1 AND MatCCMatrixFinID=" + ID + this.Criteria + " order by id desc";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = (sdr[Field] == DBNull.Value) ? "0" : (sdr[Field].ToString());
                }
            }
            return result;
        }
        private decimal LoadOutputPartNo(string MatCCID)
        {
            decimal result = 0;
            return result;
        }
        private int InsertBudgetFin(object objDomain, bool insert)
        {
            int result = -1;
            ZetroLib zl = new ZetroLib();
            zl.hlp = new CostFIN();
            zl.Criteria = (insert == true) ? "Tahun,Bulan,MatCCMatrixFinID,Barang,Lembar,RupiahPerBln,RowStatus,Createdby,CreatedTime" :
                        "ID,Tahun,Bulan,MatCCMatrixFinID,Barang,Lembar,RupiahPerBln,RowStatus,LastModifiedBy,LastModifiedTime";
            zl.StoreProcedurName =(insert==true)?  "spMaterialCCBudgetFinishing_Insert":"spMaterialCCBudgetFinishing_Update";
            zl.TableName = "MAterialCCBudgetFinishing";
            zl.Option = (insert == true) ? "Insert" : "Update";
            zl.ReturnID = false;
            string rst = zl.CreateProcedure();
            if (rst == string.Empty)
            {
                zl.hlp = objDomain;
                result = zl.ProcessData();
            }
            return result;
        }

        public int InsertBudgetNilaiFin(CostFIN data, bool insert)
        {
            int result = -1;
            ZetroLib zl = new ZetroLib();
            zl.hlp = new CostFIN();
            zl.Criteria = (insert == true) ? "ItemID,Price,Bulan,Tahun,RowStatus,CreatedBy,CreatedTime" :
                                             "ID,ItemID,Price,Bulan,Tahun,RowStatus,,CreatedBy,CreatedTime";
            zl.TableName = "MaterialCCNilaiFIN";
            zl.StoreProcedurName = (insert == true) ? "spMaterialCCNilaiFIN_Insert" : "spMaterialCCNilaiFIN_Update";
            zl.Option = (insert == true) ? "Insert" : "Update";
            zl.ReturnID = false;
            string rst = zl.CreateProcedure();
            if (rst == string.Empty)
            {
                zl.hlp = data;
                result = zl.ProcessData();
               
            }
            return result;
        }
        public string RetieveNilaiFIN(int ItemID, string Field)
        {
            string result = "0";
            string strSQL = "SELECT "+Field+" FROM MaterialCCNilaiFIN Where ItemID=" + ItemID + " AND Bulan=" + this.Bulan + " AND Tahun=" + this.Tahun + " and RowStatus>-1";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = (sdr[Field].ToString());
                }
            }
            else
            {
                result = RetieveNilaiFIN(ItemID, Field, false);
            }
            return result;
        }
        private string RetieveNilaiFIN(int ItemID, string Field, bool LastPrice)
        {
            string result = "0";
            string Bln, Thn;
            switch (int.Parse(this.Bulan))
            {
                case 1: Bln = "12"; Thn = (int.Parse(this.Tahun) - 1).ToString(); break;
                default: Bln = (int.Parse(this.Bulan) - 1).ToString(); Thn = this.Tahun; break;
            }
            string strSQL = (LastPrice == true) ? 
                "SELECT top 1 " + Field + " FROM MaterialCCNilaiFIN Where ItemID=" + ItemID + " AND RowStatus>-1 ORDER By ID Desc" :
                "SELECT " + Global.nBulan(int.Parse(Bln), true) + "AvgPrice as " + Field + " FROM SaldoInventory WHERE yearPeriod=" + Thn + " AND ItemID=" + ItemID + " AND ItemTypeID=1";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = (sdr[Field].ToString());
                }
            }
            return result;
        }
        public decimal RetrieveTotalNilaiFIN(int MatPPGroupID)
        {
            decimal result = 0;
            string Bln, Thn, YM = "";
            switch (int.Parse(this.Bulan))
            {
                case 1: Bln = "12"; Thn = (int.Parse(this.Tahun) - 1).ToString();   break;
                default: Bln = (int.Parse(this.Bulan) - 1).ToString().PadLeft(2,'0'); Thn = this.Tahun; break;
            }
            string strSQL = 
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GroupPP]') AND type in (N'U')) DROP TABLE [dbo].GroupPP " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ItemYngDipakai]') AND type in (N'U')) DROP TABLE [dbo].ItemYngDipakai  " +
                        //"WITH GroupPP AS ( " +
                            "SELECT ID,MatCCGroupID,ItemID,ItemTypeID into GroupPP FROM MaterialPPGroup WHERE RowStatus>-1 AND MatCCID=2 " +
                            //") " +
                            //",ItemYngDipakai AS ( " +
                            "    SELECT ItemID into ItemYngDipakai FROM vw_StockPurchn WHERE ItemID in(Select ItemID FROM GroupPP WHERE MatCCGroupID=" + MatPPGroupID + ") " +
                            "     AND ItemTypeID in(Select ItemTypeID FROM GroupPP WHERE MatCCGroupID=" + MatPPGroupID + ") " +
                            "    AND YM=" + this.Tahun + this.Bulan + " AND Process='pakaiDetail' GROUP By ItemID " +
                            //") " +
                            "SELECT ISNULL(AVG(" + Global.nBulan(int.Parse(Bln), true) + "AvgPrice),0)" + "AvgPrice FROM SaldoInventory WHERE ItemID IN(Select ItemID FROM ItemYngDipakai ) " +
                            "AND YearPeriod=" + Thn + "AND (" + Global.nBulan(int.Parse(Bln),true) + "AvgPrice>0 OR " + Global.nBulan(int.Parse(Bln),true) + "AvgPrice IS Not NULL) " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GroupPP]') AND type in (N'U')) DROP TABLE [dbo].GroupPP " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ItemYngDipakai]') AND type in (N'U')) DROP TABLE [dbo].ItemYngDipakai ";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = decimal.Parse(sdr["AvgPrice"].ToString());
                }
            }
            return result;
        }

    }
}
