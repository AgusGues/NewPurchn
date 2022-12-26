using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.ListReportDefect
{
    public partial class TotalDefect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "../../Default.aspx";
                DateTime tgl = DateTime.Parse("01-" + DateTime.Now.ToString("MMM") + "-" + DateTime.Now.ToString("yyyy"));
                txtdrtanggal.Text = "01-" + tgl.ToString("MMM-yyyy");
                DateTime endOfMonth = new DateTime(tgl.Year, tgl.Month, DateTime.DaysInMonth(tgl.Year, tgl.Month));
                txtsdtanggal.Text = endOfMonth.ToString("dd-MMM-yyyy");
            }
        }

        #region BtnPreview Awal
        //protected void BtnPreview_Click(object sender, EventArgs e)
        //{
        //    if (txtdrtanggal.Text == string.Empty || txtsdtanggal.Text == string.Empty)
        //    {
        //        return;
        //    }
        //    DateTime intgl1 = DateTime.Parse(txtdrtanggal.Text);
        //    DateTime intgl2 = DateTime.Parse(txtsdtanggal.Text);
        //    string tglakhir = string.Empty;
        //    int tambahtgl = 0;
        //    long jmlHari = DateDiff(DateInterval.Day, intgl1, intgl2) + 1;
        //    if (jmlHari > 31)
        //    {
        //        string periode = DateTime.Parse(txtdrtanggal.Text).ToString("dd MMMM yyyy") + "  s/d  " +
        //            DateTime.Parse(txtdrtanggal.Text).AddDays(40).ToString("dd MMMM yyyy");
        //        tglakhir = DateTime.Parse(txtdrtanggal.Text).AddDays(40).ToString("MM/dd/yyyy");
        //        Session["periode"] =  periode;
        //    }
        //    else
        //    {
        //        string periode = DateTime.Parse(txtdrtanggal.Text).ToString("dd MMMM yyyy") + "  s/d  " +
        //            DateTime.Parse(txtsdtanggal.Text).ToString("dd MMMM yyyy");
        //        tglakhir = DateTime.Parse(txtsdtanggal.Text).ToString("MM/dd/yyyy");
        //        Session["periode"] = periode;
        //    }
        //    string tglproses = string.Empty;
        //    string sqlselect = string.Empty;
        //    string sqlinpivot = string.Empty;
        //    string sqlselect1 = string.Empty;
        //    string sqlinpivot1 = string.Empty;
        //    for (int i = 0; i < 31; i++)
        //    {
        //        if (i == 0)
        //            tglproses = (i + 1).ToString().PadLeft(2, '0') + "-" + DateTime.Parse(txtdrtanggal.Text).ToString("MMM") + "-" + DateTime.Parse(txtdrtanggal.Text).ToString("yyyy");
        //        else
        //            tglproses = DateTime.Parse(tglproses).AddDays(1).ToString("dd-MMM-yyyy");
        //        if (DateTime.Parse(tglproses) >= DateTime.Parse(txtdrtanggal.Text) && DateTime.Parse(tglproses) <= DateTime.Parse(txtsdtanggal.Text))
        //        {
        //            if (i  <30)
        //            {
        //                sqlselect = sqlselect + "isnull([" + DateTime.Parse(txtdrtanggal.Text).AddDays(i).ToString("yyyy") +
        //                    DateTime.Parse(txtdrtanggal.Text).ToString("MM") + (i + 1).ToString().PadLeft(2, '0') + "],0) as " +
        //                    "[" + (i + 1).ToString() + "],";
        //                sqlinpivot = sqlinpivot + "[" + DateTime.Parse(txtdrtanggal.Text).AddDays(i).ToString("yyyy") +
        //                    DateTime.Parse(txtdrtanggal.Text).ToString("MM") + (i + 1).ToString().PadLeft(2, '0') + "],";
        //            }
        //            else
        //            {
        //                sqlselect = sqlselect + "isnull([" + DateTime.Parse(txtdrtanggal.Text).AddDays(i).ToString("yyyy") +
        //                   DateTime.Parse(txtdrtanggal.Text).ToString("MM") + (i + 1).ToString().PadLeft(2, '0') + "],0) as " +
        //                   "[" + (i + 1).ToString() + "]";
        //                sqlinpivot = sqlinpivot + "[" + DateTime.Parse(txtdrtanggal.Text).AddDays(i).ToString("yyyy") +
        //                    DateTime.Parse(txtdrtanggal.Text).ToString("MM") + (i + 1).ToString().PadLeft(2, '0') + "]";
        //            }

        //        }
        //        else
        //        {
        //            if (i < 30)
        //                sqlselect = sqlselect + "isnull([" + (i + 1).ToString() + "],0)as " +
        //                "[" + (i + 1).ToString() + "],";
        //            else
        //                sqlselect = sqlselect + "isnull([" + (i + 1).ToString() + "],0)as " +
        //                "[" + (i + 1).ToString() + "]";
        //            if (i < 30)
        //                sqlinpivot = sqlinpivot + "[" + (i + 1).ToString() + "],";
        //            else
        //                sqlinpivot = sqlinpivot + "[" + (i + 1).ToString() + "]";
        //        }


        //        #region remark
        //        //}
        //        //else
        //        //{
        //        //    sqltanggal[i] = DateTime.Parse("01/01/1900").ToString("MM/dd/yyyy");
        //        //    if (i < 30)
        //        //        sqlselect = sqlselect + "isnull([" + (i + 1).ToString() + "],0)as " +
        //        //        "[" + (i + 1).ToString() + "],";
        //        //    else
        //        //        sqlselect = sqlselect + "isnull([" + (i + 1).ToString() + "],0)as " +
        //        //        "[" + (i + 1).ToString() + "]";
        //        //    if (i < 30)
        //        //        sqlinpivot = sqlinpivot + "[" + (i + 1).ToString() + "],";
        //        //    else
        //        //        sqlinpivot = sqlinpivot + "[" + (i + 1).ToString() + "]";
        //        //}
        //            //string tempstr = sqltanggal[i];
        //            #endregion

        //    }
        //    #region kubik
        //    for (int i = 0; i < 31; i++)
        //    {
        //        if (i == 0)
        //            tglproses = (i + 1).ToString().PadLeft(2, '0') + "-" + DateTime.Parse(txtdrtanggal.Text).ToString("MMM") + "-" + DateTime.Parse(txtdrtanggal.Text).ToString("yyyy");
        //        else
        //            tglproses = DateTime.Parse(tglproses).AddDays(1).ToString("dd-MMM-yyyy");
        //        if (DateTime.Parse(tglproses) >= DateTime.Parse(txtdrtanggal.Text) && DateTime.Parse(tglproses) <= DateTime.Parse(txtsdtanggal.Text))
        //        {
        //            if (i < 30)
        //            {
        //                sqlselect1 = sqlselect1 + "isnull([" + DateTime.Parse(txtdrtanggal.Text).AddDays(i).ToString("yyyy") +
        //                    DateTime.Parse(txtdrtanggal.Text).ToString("MM") + (i + 1).ToString().PadLeft(2, '0') + "K],0) as " +
        //                    "[" + (i + 1).ToString() + "K],";
        //                sqlinpivot1 = sqlinpivot1 + "[" + DateTime.Parse(txtdrtanggal.Text).AddDays(i).ToString("yyyy") +
        //                    DateTime.Parse(txtdrtanggal.Text).ToString("MM") + (i + 1).ToString().PadLeft(2, '0') + "K],";
        //            }
        //            else
        //            {
        //                sqlselect1 = sqlselect1 + "isnull([" + DateTime.Parse(txtdrtanggal.Text).AddDays(i).ToString("yyyy") +
        //                   DateTime.Parse(txtdrtanggal.Text).ToString("MM") + (i + 1).ToString().PadLeft(2, '0') + "K],0) as " +
        //                   "[" + (i + 1).ToString() + "K]";
        //                sqlinpivot1 = sqlinpivot1 + "[" + DateTime.Parse(txtdrtanggal.Text).AddDays(i).ToString("yyyy") +
        //                    DateTime.Parse(txtdrtanggal.Text).ToString("MM") + (i + 1).ToString().PadLeft(2, '0') + "K]";
        //            }
        //        }
        //        else
        //        {
        //            if (i < 30)
        //                sqlselect1 = sqlselect1 + "isnull([" + (i + 1).ToString() + "K],0)as " +
        //                "[" + (i + 1).ToString() + "K],";
        //            else
        //                sqlselect1 = sqlselect1 + "isnull([" + (i + 1).ToString() + "K],0)as " +
        //                "[" + (i + 1).ToString() + "K]";
        //            if (i < 30)
        //                sqlinpivot1 = sqlinpivot1 + "[" + (i + 1).ToString() + "K],";
        //            else
        //                sqlinpivot1 = sqlinpivot1 + "[" + (i + 1).ToString() + "K]";
        //        }
        //    }
        //    #endregion
        //    //LemburFacade lemburfacade = new LemburFacade();
        //    Users users = (Users)Session["Users"];
        //    //if (ddlDepartemen.SelectedIndex == 0)
        //    //{ return; }
        //    int DeptID = Convert.ToInt32(ddlDepartemen.SelectedValue);
        //    string deptname = ddlDepartemen.SelectedItem.Text;
        //    string strQuery = string.Empty;
        //    string tgl1 = DateTime.Parse(txtdrtanggal.Text).ToString("dd MMMM yyyy");
        //    string tgl2 = DateTime.Parse(txtsdtanggal.Text).ToString("dd MMMM yyyy");
        //    string stgl1 = DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd");
        //    string stgl2 = DateTime.Parse(txtsdtanggal.Text).ToString("yyyyMMdd");

        //    string strdept =string.Empty;
        //    string strOven = string.Empty;
        //    string strGJ = string.Empty;
        //    string isoven = string.Empty;
        //    if (DateTime.Parse(txtdrtanggal.Text) < DateTime.Parse("01-jul-2016"))
        //    {
        //        strGJ = "where A.ID=B.DefectID and A.SerahID=C.ID and B.MasterID=D.ID and E.ID=C.DestID  and C.ItemID0=I1.ID and B.RowStatus>-1";
        //        strOven = "case when D.DeptID=0 then case when (B.Qty/E.Qty) *100 >5 then 2 else 3 end else D.DeptID end deptID1";
        //    }
        //    else
        //    {
        //        if (Convert.ToInt32(stgl1.Substring(0, 6)) < 201805)
        //        {
        //            strGJ = ",Def_GroupJemur GJ  " +
        //                    "where A.ID=B.DefectID and A.SerahID=C.ID and B.MasterID=D.ID and D.RowStatus>-1 and E.ID=C.DestID  and C.ItemID0=I1.ID and " +
        //                    " GJ.ID=A.GroupJemurID and B.RowStatus>-1 ";
        //            strOven = "case when GJ.GroupJemurName In ('M','N','O','P','Q','R','S','T','U','V','W','X') then " +
        //                      "      case when D.DeptID=0 then  " +
        //                      "          case when D.DefName='retak' then 2  " +
        //                      "              when D.DefName='gompal' then 3 " +
        //                      "          end  " +
        //                      "      else  " +
        //                      "          D.DeptID  " +
        //                      "      end  " +
        //                      "  else  " +
        //                      "      case when D.DeptID=0 then  " +
        //                      "          case when (B.Qty/E.Qty) *100 >5 then 2  " +
        //                      "          else 3   " +
        //                      "          end " +
        //                      "      else  " +
        //                      "          D.DeptID  " +
        //                      "      end  " +
        //                      "  end deptID1 ";
        //        }
        //        else
        //        {
        //            strGJ = ",Def_GroupJemur GJ  " +
        //                    " where A.ID=B.DefectID  and B.MasterID=D.ID and D.RowStatus>-1 and  GJ.ID=A.GroupJemurID and B.RowStatus>-1  and E.ID=A.Destid and I1.ID=E.ItemID ";
        //            strOven = "case when GJ.GroupJemurName In ('M','N','O','P','Q','R','S','T','U','V','W','X') then " +
        //                      "      case when D.DeptID=0 then  " +
        //                      "          case when D.DefName='retak' then 2  " +
        //                      "              when D.DefName='gompal' then 3 " +
        //                      "          end  " +
        //                      "      else  " +
        //                      "          D.DeptID  " +
        //                      "      end  " +
        //                      "  else  " +
        //                      "      case when D.DeptID=0 then  " +
        //                      "          case when (B.Qty/A.Tpotong) *100 >5 then 2  " +
        //                      "          else 3   " +
        //                      "          end " +
        //                      "      else  " +
        //                      "          D.DeptID  " +
        //                      "      end  " +
        //                      "  end deptID1 ";
        //        }
        //    }
        //    if (ddlDepartemen0.SelectedValue == "0")
        //    {
        //        isoven = string.Empty;
        //        Session["proses"] = " ";
        //    }
        //    if (ddlDepartemen0.SelectedValue == "1")
        //    {
        //        isoven = " and GJ.GroupJemurName In ('M','N','O','P','Q','R','S','T','U','V','W','X') ";
        //    Session["proses"] = ddlDepartemen0.SelectedItem.Text.Trim().ToUpper();
        //    }
        //    if (ddlDepartemen0.SelectedValue == "2")
        //    {
        //        isoven = " and GJ.GroupJemurName Not In ('M','N','O','P','Q','R','S','T','U','V','W','X') ";
        //        Session["proses"] = ddlDepartemen0.SelectedItem.Text.Trim().ToUpper();
        //    }
        //    string wheredestid1 = string.Empty;
        //    string wheredestid2 = string.Empty;
        //    if (ddlDepartemen0.SelectedValue == "1")
        //    {
        //        wheredestid1 = " S.destID in (select distinct Destid from T1_Jemur A inner join FC_Rak B on A.RakID=B.ID where B.Rak='00') and ";
        //        wheredestid2 = " destID in (select distinct Destid from T1_Jemur A inner join FC_Rak B on A.RakID=B.ID where B.Rak='00') and ";
        //    }
        //    if (ddlDepartemen0.SelectedValue == "2")
        //    {
        //        wheredestid1 = " S.destID in (select distinct Destid from T1_Jemur A inner join FC_Rak B on A.RakID=B.ID where B.Rak<>'00') and ";
        //        wheredestid2 = " destID in (select distinct Destid from T1_Jemur A inner join FC_Rak B on A.RakID=B.ID where B.Rak<>'00') and ";
        //    }
        //    if (ddlDepartemen0.SelectedValue == "0")
        //    {
        //        wheredestid1 = " ";
        //        wheredestid2 = " ";
        //    }
        //    string tpotong = string.Empty;
        //    string strSQL = string.Empty;
        //    if (DeptID > 0)
        //        strdept = "and DeptID1=" + ddlDepartemen.SelectedValue + " ";
        //    if (Convert.ToInt32(stgl1.Substring(0, 6)) >= 201805)
        //    {
        //        tpotong = "A.TPotong";
        //        strSQL = "select ID,defectID,masterID,defname,Qty,M3,TotPotong,TotPotongM3,'' sfrom into tempdefect from (   " +
        //        "select D.DefName,D.DeptID,B.ID,B.DefectID,B.MasterID,B.Qty,A.DestID," + tpotong + " as TotPotong,   " +
        //        strOven + ",A.Tgl,B.Qty*I1.Volume as M3," + tpotong + " as TotPotongM3 " +
        //        "from Def_DefectDetail B,Def_Defect A,Def_MasterDefect D,BM_Destacking E ,FC_Items I1 " +
        //        strGJ + isoven + " ) as Def " +
        //        "where CONVERT(varchar, Tgl,112)>='" + stgl1 + "' and CONVERT(varchar, Tgl ,112)<='" + stgl2 + "' " + strdept +

        //        "select ID,defectID,masterID,Qty,M3,TotPotong,TotPotongM3,'' sfrom into tempdefect1 from (   " +
        //        "select D.DefName,D.DeptID,B.ID,B.DefectID,B.MasterID,B.Qty,A.DestID," + tpotong + " as TotPotong, " +
        //        strOven + ",A.Tgl,B.Qty*I1.Volume as M3 ," + tpotong + " as TotPotongM3 " +
        //        "from Def_DefectDetail B,Def_Defect A,Def_MasterDefect D,BM_Destacking E ,FC_Items I1 " +
        //        strGJ + isoven + " ) as Def " +
        //        "where CONVERT(varchar, Tgl,112)>='" + stgl1 + "' and CONVERT(varchar, Tgl ,112)<='" + stgl2 + "' " +

        //        "select ROW_NUMBER() over(order by DefName) as rNo, SUBSTRING(DefName,2,100) as defect," + sqlselect + "," + sqlselect1 +
        //        " from ( " +
        //        "select 'A' + B.DefName as DefName, " +
        //        " CONVERT(varchar,A.Tgl ,112) as hari,cast(QTY as varchar(max)) as QTY from Def_Defect A inner join tempdefect B on A.ID=B.DefectID " +
        //        "  WHERE CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
        //        " union all " +
        //        "  select 'A' + B.DefName as DefName, " +
        //        " CONVERT(varchar,A.Tgl ,112)+'K' as hari,cast(M3 as varchar(max)) as QTY from Def_Defect A inner join tempdefect B on A.ID=B.DefectID   " +
        //        "  WHERE CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
        //        " union all  " +
        //        "    " +
        //        " select 'ETotal BP Pelarian' as DefName,CONVERT(varchar, A.Tgl ,112) as hari, (select cast(SUM(qty )as varchar(max) )from tempdefect  " +
        //        " where sfrom='lari' and  defectID =A.ID) as QTY from Def_Defect A WHERE  CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
        //        " union all    " +
        //        " select 'ETotal BP Pelarian' as DefName,CONVERT(varchar, A.Tgl ,112)+'K' as hari, (select cast(SUM(M3 )as varchar(max) )from tempdefect  " +
        //        " where sfrom='lari' and  defectID =A.ID) as QTY from Def_Defect A WHERE  CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
        //        " union all    " +
        //        "  " +
        //        " select 'BTotal BP' as DefName,CONVERT(varchar, A.Tgl ,112) as hari, (select cast(SUM(qty )as varchar(max) )from tempdefect  " +
        //        " where defectID =A.ID) as QTY from Def_Defect A WHERE CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
        //        " union all    " +
        //        " select 'BTotal BP' as DefName,CONVERT(varchar, A.Tgl ,112)+'K' as hari, (select cast(SUM(M3 )as varchar(max) )from tempdefect  " +
        //        " where defectID =A.ID) as QTY from Def_Defect A  WHERE CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
        //        " union all   " +
        //        "  " +
        //        " select 'CTotal OK' as DefName,CONVERT(varchar, A.Tgl ,112) as hari, (select cast(SUM(qty *-1)as varchar(max) )from tempdefect  " +
        //        " where defectID =A.ID) as QTY from Def_Defect A WHERE CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
        //        " union all    " +
        //        " select 'CTotal OK' as DefName,CONVERT(varchar, A.Tgl ,112)+'K' as hari, (select cast(SUM(M3 *-1)as varchar(max) )from tempdefect  " +
        //        " where defectID =A.ID) as QTY from Def_Defect A  WHERE CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
        //        " union all   " +
        //        " select 'CTotal OK' as DefName,CONVERT(varchar, A.Tgl ,112) as hari, tpotong QTY from Def_Defect A WHERE A.status>-1 and " +
        //        " CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
        //        " union all    " +
        //        " select 'CTotal OK' as DefName,CONVERT(varchar, A.Tgl ,112)+'K' as hari, tpotong*I.Volume   QTY  " +
        //        " from Def_Defect A  inner join BM_Destacking D on A.Destid=D.ID inner join FC_Items I on I.ID=D.ItemID WHERE A.status>-1 and CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
        //        " union all   " +
        //        //" select 'CTotal OK' as DefName,CONVERT(varchar, A.Tgl ,112) as hari, tpotong-isnull((select sum(qty)from def_defectdetail where rowstatus>-1 and defectid =A.ID),0) QTY from Def_Defect A WHERE A.status>-1 and CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
        //        //" union all    " +
        //        //" select 'CTotal OK' as DefName,CONVERT(varchar, A.Tgl ,112)+'K' as hari, (tpotong-isnull((select sum(qty)from def_defectdetail where rowstatus>-1 and defectid =A.ID),0))*I.Volume   QTY  " +
        //        //" from Def_Defect A  inner join BM_Destacking D on A.Destid=D.ID inner join FC_Items I on I.ID=D.ItemID WHERE A.status>-1 and CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
        //        //" union all   " +
        //        //"  " +
        //        " select 'DTotal Potong' as DefName,CONVERT(varchar, A.Tgl ,112) as hari, tpotong QTY from Def_Defect A WHERE A.status>-1 and " + 
        //        " CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
        //        " union all    " +
        //        " select 'DTotal Potong' as DefName,CONVERT(varchar, A.Tgl ,112)+'K' as hari, tpotong*I.Volume   QTY  " +
        //        " from Def_Defect A  inner join BM_Destacking D on A.Destid=D.ID inner join FC_Items I on I.ID=D.ItemID WHERE A.status>-1 and CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
        //        " union all   " +
        //        " " +
        //        "select  DefName, hari, case when sum(QTYB ) >0 then(SUM(QTYA) / sum(QTYB )) *100 else 0 end  QTY from (        " +
        //        "    select DefName,Hari, SUM(QTY) as QTYA,0 as QTYB from (      " +
        //        "        select 'ETotal Prosentase BP' as DefName,CONVERT(varchar, A.Tgl ,112) as hari,      " +
        //        "        (select SUM(qty)from tempdefect where defectID =A.ID) as QTY from Def_Defect A )as D group by DefName,Hari " +
        //        "    union ALL      " +
        //        "    select Defname,Hari, 0 as QTYA, SUM(qty) as QTYB from (          " +
        //        "        select 'ETotal Prosentase BP' as DefName ,CONVERT(varchar, A.Tgl ,112) as hari, tpotong QTY from Def_Defect A WHERE A.status>-1 and " + 
        //        " CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "' "+
        //        "    ) as C group by  DefName, hari      " +
        //        ")  as A  group by DefName, hari ) up pivot (sum(QTY)for hari in (" + sqlinpivot + "," + sqlinpivot1 + ")) as A1 ";
        //    }
        //    else
        //    {
        //        tpotong = "E.Qty";
        //        strSQL = "select ID,defectID,masterID,Qty,M3,sfrom into tempdefect from (   " +
        //        "select D.DefName,D.DeptID,B.ID,B.DefectID,B.MasterID,B.Qty,C.DestID," + tpotong + " as TotPotong,C.sfrom,   " +
        //        strOven + ",A.Tgl,B.Qty*I1.Volume as M3 " +
        //        "from Def_DefectDetail B,Def_Defect A,T1_Serah C,Def_MasterDefect D,BM_Destacking E ,FC_Items I1   " +
        //        strGJ + isoven + " ) as Def " +
        //        "where CONVERT(varchar, Tgl,112)>='" + stgl1 + "' and CONVERT(varchar, Tgl ,112)<='" + stgl2 + "' " + strdept +

        //        "select ID,defectID,masterID,Qty,M3 into tempdefect1 from (   " +
        //        "select D.DefName,D.DeptID,B.ID,B.DefectID,B.MasterID,B.Qty,C.DestID," + tpotong + " as TotPotong, " +
        //        strOven + ",A.Tgl,B.Qty*I1.Volume as M3  " +
        //        "from Def_DefectDetail B,Def_Defect A,T1_Serah C,Def_MasterDefect D,BM_Destacking E,FC_Items I1 " +
        //        strGJ + isoven + " ) as Def " +
        //        "where CONVERT(varchar, Tgl,112)>='" + stgl1 + "' and CONVERT(varchar, Tgl ,112)<='" + stgl2 + "' " +

        //        "select ID,defectID,masterID,Qty,M3 into tempdefect1 from (   " +
        //        "select D.DefName,D.DeptID,B.ID,B.DefectID,B.MasterID,B.Qty,A.DestID," + tpotong + " as TotPotong, " +
        //        strOven + ",A.Tgl,B.Qty*I1.Volume as M3  " +
        //        "from Def_DefectDetail B,Def_Defect A,Def_MasterDefect D,BM_Destacking E ,FC_Items I1 " +
        //        strGJ + isoven + " ) as Def " +
        //        "where CONVERT(varchar, Tgl,112)>='" + stgl1 + "' and CONVERT(varchar, Tgl ,112)<='" + stgl2 + "' " +

        //        "select ROW_NUMBER() over(order by DefName) as rNo, SUBSTRING(DefName,2,100) as defect," + sqlselect + "," + sqlselect1 +
        //        " from ( " +
        //        "select 'A' + C.DefName as DefName, " +
        //        " CONVERT(varchar,A.Tgl ,112) as hari,cast(QTY as varchar(max)) as QTY from Def_Defect A inner join tempdefect B on A.ID=B.DefectID " +
        //        " inner join Def_MasterDefect C on B.MasterID =C.ID WHERE CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
        //        " union all " +
        //        "  select 'A' + C.DefName as DefName, " +
        //        "CONVERT(varchar,A.Tgl ,112)+'K' as hari,cast(M3 as varchar(max)) as QTY from Def_Defect A inner join tempdefect B on A.ID=B.DefectID   " +
        //        " inner join Def_MasterDefect C on B.MasterID =C.ID WHERE CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
        //        " union all  " +
        //        "    " +
        //        " select 'ETotal BP Pelarian' as DefName,CONVERT(varchar, A.Tgl ,112) as hari, (select cast(SUM(qty )as varchar(max) )from tempdefect  " +
        //        " where sfrom='lari' and  defectID =A.ID) as QTY from Def_Defect A WHERE  CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
        //        " union all    " +
        //        " select 'ETotal BP Pelarian' as DefName,CONVERT(varchar, A.Tgl ,112)+'K' as hari, (select cast(SUM(M3 )as varchar(max) )from tempdefect  " +
        //        " where sfrom='lari' and  defectID =A.ID) as QTY from Def_Defect A WHERE  CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
        //        " union all    " +

        //        " select 'BTotal BP' as DefName,CONVERT(varchar, A.Tgl ,112) as hari, (select cast(SUM(qty )as varchar(max) )from tempdefect  " +
        //        " where defectID =A.ID) as QTY from Def_Defect A WHERE CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
        //        " union all    " +
        //        " select 'BTotal BP' as DefName,CONVERT(varchar, A.Tgl ,112)+'K' as hari, (select cast(SUM(M3 )as varchar(max) )from tempdefect  " +
        //        " where defectID =A.ID) as QTY from Def_Defect A  WHERE CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
        //        " union all   " +
        //        "  " +
        //        " select  DefName, hari, cast(SUM(qty)   as varchar(max))as QTY from (        " +
        //        "    select 'CTotal OK' as DefName,CONVERT(varchar, TglSerah ,112) as hari, sum(QtyIn) as QTY from T1_Serah S  inner join FC_Items I1 on S.ItemID0=I1.ID  inner join FC_Items I on S.ItemID=I.ID  " +
        //        "    where " + wheredestid1 + "S.Status>-1 and (I.PartNo like '%-W-%' or I.PartNo like '%-3-%') and CONVERT(char,S.tglserah,112)>='" + stgl1 + "'  and CONVERT(char,S.tglserah,112)<='" + stgl2 + "' group by TglSerah  " +
        //        ")  as A  group by DefName, hari  " +
        //        "union all " +
        //        " select  DefName, hari, cast(SUM(qty)   as varchar(max))as QTY from (        " +
        //        "    select 'CTotal OK' as DefName,CONVERT(varchar, TglSerah ,112)+'K' as hari, sum(QtyIn*I1.Volume) as QTY from T1_Serah S  inner join FC_Items I1 on S.ItemID0=I1.ID inner join FC_Items I on S.ItemID=I.ID   " +
        //        "    where   " + wheredestid1 + " S.Status>-1 and (I.PartNo like '%-W-%' or I.PartNo like '%-3-%') and CONVERT(char,S.tglserah,112)>='" + stgl1 + "'  and CONVERT(char,S.tglserah,112)<='" + stgl2 + "' group by TglSerah  " +
        //        ")  as A  group by DefName, hari   " +
        //        "union all    " +
        //        " " +
        //        "select Defname,Hari, cast(SUM(qty) as varchar(max)) as QTY from (  " +
        //        "    select 'DTotal Potong' as DefName,CONVERT(varchar, A.Tgl ,112) as hari, (select SUM(qty)from tempdefect1 where defectID =A.ID) as QTY from Def_Defect A WHERE CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
        //        "    union all    " +
        //        "    select  DefName, hari, cast(SUM(qty) as varchar(max)) as QTY from (        " +
        //        "        select 'DTotal Potong' as DefName,CONVERT(varchar, TglSerah ,112) as hari, sum(QtyIn) as QTY from T1_Serah S  inner join FC_Items I on S.ItemID=I.ID  " +
        //        "        where  " + wheredestid1 + " S.Status>-1 and (I.PartNo like '%-W-%' or I.PartNo like '%-3-%') and CONVERT(char,S.tglserah,112)>='" + stgl1 + "'  and CONVERT(char,S.tglserah,112)<='" + stgl2 + "'  group by TglSerah  " +
        //        "        )  as A  group by DefName, hari  " +
        //        "    ) as C group by  DefName, hari  " +
        //        "union all   " +
        //        "SELECT 'DTotal Potong' as DefName,HARI, cast(SUM(qty) as varchar(max)) QTY FROM ( " +
        //        "select CONVERT(varchar, A.Tgl ,112)+'K' as hari, (select SUM(M3)from tempdefect1 where defectID =A.ID) as QTY from Def_Defect A    " +
        //        "WHERE CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
        //        "UNION ALL " +
        //        "select CONVERT(varchar, TglSerah ,112)+'K' as hari,sum(QtyIn*I1.Volume) as QTY from T1_Serah S  inner join FC_Items I on S.ItemID=I.ID  inner join FC_Items I1 on S.itemID0=I1.ID  " +
        //        "        where " + wheredestid1 + " S.Status>-1 and (I.PartNo like '%-W-%' or I.PartNo like '%-3-%')  " +
        //        "        and CONVERT(char,S.tglserah,112)>='" + stgl1 + "'  and CONVERT(char,S.tglserah,112)<='" + stgl2 + "'  GROUP BY TglSerah) AS c GROUP BY HARI  " +
        //        "union all   " +
        //        " " +
        //        "select  DefName, hari, case when sum(QTYB ) >0 then(SUM(QTYA) / sum(QTYB )) *100 else 0 end  QTY from (        " +
        //        "    select DefName,Hari, SUM(QTY) as QTYA,0 as QTYB from (      " +
        //        "        select 'ETotal Prosentase BP' as DefName,CONVERT(varchar, A.Tgl ,112) as hari,      " +
        //        "        (select SUM(qty)from tempdefect where defectID =A.ID) as QTY from Def_Defect A )as D group by DefName,Hari " +
        //        "    union ALL      " +
        //        "    select Defname,Hari, 0 as QTYA, SUM(qty) as QTYB from (          " +
        //        "        select 'ETotal Prosentase BP' as DefName,CONVERT(varchar, A.Tgl ,112) as hari,          " +
        //        "        (select SUM(cast(qty as decimal))from tempdefect1 where defectID =A.ID) as QTY from Def_Defect A " +
        //        "        union all            " +
        //        "        select  DefName, hari, SUM(qty)as QTY from (            " +
        //        "        select 'ETotal Prosentase BP' as DefName,CONVERT(varchar, TglSerah ,112) as hari, QtyIn as QTY from T1_Serah where status>-1 and " + wheredestid2 + " ItemID in " +
        //        "        (select ID from FC_Items where PartNo like '%-W-%' or PartNo like '%-3-%') " +
        //        "        )  as A  group by DefName, hari  " +
        //        "    ) as C group by  DefName, hari      " +
        //        ")  as A  group by DefName, hari ) up pivot (sum(QTY)for hari in (" + sqlinpivot + "," + sqlinpivot1 + ")) as A1 ";
        //    }

        //    strQuery = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempdefect]') AND type in (N'U')) DROP TABLE [dbo].[tempdefect] " +
        //        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempdefect1]') AND type in (N'U')) DROP TABLE [dbo].[tempdefect1] " +
        //        strSQL +
        //        "/*IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempdefect]') AND type in (N'U')) DROP TABLE [dbo].[tempdefect] " +
        //        "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempdefect1]') AND type in (N'U')) DROP TABLE [dbo].[tempdefect1] */";
        //    Session["Query"] = strQuery;
        //    Session["dept"] = deptname;
        //    Session["prd1"] = tgl1;
        //    Session["prd2"] = tgl2; 
        //    Cetak(this);
        //}
        #endregion
        #region btnPreview After Add 26 Agustus 2018
        protected void BtnPreview_Click(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];
            #region Untuk Citeureup
            if (users.UnitKerjaID == 11)
            {
                if (txtdrtanggal.Text == string.Empty || txtsdtanggal.Text == string.Empty)
                {
                    return;
                }
                DateTime intgl1 = DateTime.Parse(txtdrtanggal.Text);
                DateTime intgl2 = DateTime.Parse(txtsdtanggal.Text);
                string tglakhir = string.Empty;
                int tambahtgl = 0;
                long jmlHari = DateDiff(DateInterval.Day, intgl1, intgl2) + 1;
                if (jmlHari > 31)
                {
                    string periode = DateTime.Parse(txtdrtanggal.Text).ToString("dd MMMM yyyy") + "  s/d  " +
                        DateTime.Parse(txtdrtanggal.Text).AddDays(40).ToString("dd MMMM yyyy");
                    tglakhir = DateTime.Parse(txtdrtanggal.Text).AddDays(40).ToString("MM/dd/yyyy");
                    Session["periode"] = periode;
                }
                else
                {
                    string periode = DateTime.Parse(txtdrtanggal.Text).ToString("dd MMMM yyyy") + "  s/d  " +
                        DateTime.Parse(txtsdtanggal.Text).ToString("dd MMMM yyyy");
                    tglakhir = DateTime.Parse(txtsdtanggal.Text).ToString("MM/dd/yyyy");
                    Session["periode"] = periode;
                }
                string tglproses = string.Empty;
                string sqlselect = string.Empty;
                string sqlinpivot = string.Empty;
                string sqlselect1 = string.Empty;
                string sqlinpivot1 = string.Empty;
                for (int i = 0; i < 31; i++)
                {
                    if (i == 0)
                        tglproses = (i + 1).ToString().PadLeft(2, '0') + "-" + DateTime.Parse(txtdrtanggal.Text).ToString("MMM") + "-" + DateTime.Parse(txtdrtanggal.Text).ToString("yyyy");
                    else
                        tglproses = DateTime.Parse(tglproses).AddDays(1).ToString("dd-MMM-yyyy");
                    if (DateTime.Parse(tglproses) >= DateTime.Parse(txtdrtanggal.Text) && DateTime.Parse(tglproses) <= DateTime.Parse(txtsdtanggal.Text))
                    {
                        if (i < 30)
                        {
                            sqlselect = sqlselect + "isnull([" + DateTime.Parse(txtdrtanggal.Text).AddDays(i).ToString("yyyy") +
                                DateTime.Parse(txtdrtanggal.Text).ToString("MM") + (i + 1).ToString().PadLeft(2, '0') + "],0) as " +
                                "[" + (i + 1).ToString() + "],";
                            sqlinpivot = sqlinpivot + "[" + DateTime.Parse(txtdrtanggal.Text).AddDays(i).ToString("yyyy") +
                                DateTime.Parse(txtdrtanggal.Text).ToString("MM") + (i + 1).ToString().PadLeft(2, '0') + "],";
                        }
                        else
                        {
                            sqlselect = sqlselect + "isnull([" + DateTime.Parse(txtdrtanggal.Text).AddDays(i).ToString("yyyy") +
                               DateTime.Parse(txtdrtanggal.Text).ToString("MM") + (i + 1).ToString().PadLeft(2, '0') + "],0) as " +
                               "[" + (i + 1).ToString() + "]";
                            sqlinpivot = sqlinpivot + "[" + DateTime.Parse(txtdrtanggal.Text).AddDays(i).ToString("yyyy") +
                                DateTime.Parse(txtdrtanggal.Text).ToString("MM") + (i + 1).ToString().PadLeft(2, '0') + "]";
                        }

                    }
                    else
                    {
                        if (i < 30)
                            sqlselect = sqlselect + "isnull([" + (i + 1).ToString() + "],0)as " +
                            "[" + (i + 1).ToString() + "],";
                        else
                            sqlselect = sqlselect + "isnull([" + (i + 1).ToString() + "],0)as " +
                            "[" + (i + 1).ToString() + "]";
                        if (i < 30)
                            sqlinpivot = sqlinpivot + "[" + (i + 1).ToString() + "],";
                        else
                            sqlinpivot = sqlinpivot + "[" + (i + 1).ToString() + "]";
                    }


                    #region remark
                    //}
                    //else
                    //{
                    //    sqltanggal[i] = DateTime.Parse("01/01/1900").ToString("MM/dd/yyyy");
                    //    if (i < 30)
                    //        sqlselect = sqlselect + "isnull([" + (i + 1).ToString() + "],0)as " +
                    //        "[" + (i + 1).ToString() + "],";
                    //    else
                    //        sqlselect = sqlselect + "isnull([" + (i + 1).ToString() + "],0)as " +
                    //        "[" + (i + 1).ToString() + "]";
                    //    if (i < 30)
                    //        sqlinpivot = sqlinpivot + "[" + (i + 1).ToString() + "],";
                    //    else
                    //        sqlinpivot = sqlinpivot + "[" + (i + 1).ToString() + "]";
                    //}
                    //string tempstr = sqltanggal[i];
                    #endregion

                }
                #region kubik
                for (int i = 0; i < 31; i++)
                {
                    if (i == 0)
                        tglproses = (i + 1).ToString().PadLeft(2, '0') + "-" + DateTime.Parse(txtdrtanggal.Text).ToString("MMM") + "-" + DateTime.Parse(txtdrtanggal.Text).ToString("yyyy");
                    else
                        tglproses = DateTime.Parse(tglproses).AddDays(1).ToString("dd-MMM-yyyy");
                    if (DateTime.Parse(tglproses) >= DateTime.Parse(txtdrtanggal.Text) && DateTime.Parse(tglproses) <= DateTime.Parse(txtsdtanggal.Text))
                    {
                        if (i < 30)
                        {
                            sqlselect1 = sqlselect1 + "isnull([" + DateTime.Parse(txtdrtanggal.Text).AddDays(i).ToString("yyyy") +
                                DateTime.Parse(txtdrtanggal.Text).ToString("MM") + (i + 1).ToString().PadLeft(2, '0') + "K],0) as " +
                                "[" + (i + 1).ToString() + "K],";
                            sqlinpivot1 = sqlinpivot1 + "[" + DateTime.Parse(txtdrtanggal.Text).AddDays(i).ToString("yyyy") +
                                DateTime.Parse(txtdrtanggal.Text).ToString("MM") + (i + 1).ToString().PadLeft(2, '0') + "K],";
                        }
                        else
                        {
                            sqlselect1 = sqlselect1 + "isnull([" + DateTime.Parse(txtdrtanggal.Text).AddDays(i).ToString("yyyy") +
                               DateTime.Parse(txtdrtanggal.Text).ToString("MM") + (i + 1).ToString().PadLeft(2, '0') + "K],0) as " +
                               "[" + (i + 1).ToString() + "K]";
                            sqlinpivot1 = sqlinpivot1 + "[" + DateTime.Parse(txtdrtanggal.Text).AddDays(i).ToString("yyyy") +
                                DateTime.Parse(txtdrtanggal.Text).ToString("MM") + (i + 1).ToString().PadLeft(2, '0') + "K]";
                        }
                    }
                    else
                    {
                        if (i < 30)
                            sqlselect1 = sqlselect1 + "isnull([" + (i + 1).ToString() + "K],0)as " +
                            "[" + (i + 1).ToString() + "K],";
                        else
                            sqlselect1 = sqlselect1 + "isnull([" + (i + 1).ToString() + "K],0)as " +
                            "[" + (i + 1).ToString() + "K]";
                        if (i < 30)
                            sqlinpivot1 = sqlinpivot1 + "[" + (i + 1).ToString() + "K],";
                        else
                            sqlinpivot1 = sqlinpivot1 + "[" + (i + 1).ToString() + "K]";
                    }
                }
                #endregion
                //LemburFacade lemburfacade = new LemburFacade();
                //Users users = (Users)Session["Users"];
                //if (ddlDepartemen.SelectedIndex == 0)
                //{ return; }
                int DeptID = Convert.ToInt32(ddlDepartemen.SelectedValue);
                string deptname = ddlDepartemen.SelectedItem.Text;
                string strQuery = string.Empty;
                string tgl1 = DateTime.Parse(txtdrtanggal.Text).ToString("dd MMMM yyyy");
                string tgl2 = DateTime.Parse(txtsdtanggal.Text).ToString("dd MMMM yyyy");
                string stgl1 = DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd");
                string stgl2 = DateTime.Parse(txtsdtanggal.Text).ToString("yyyyMMdd");

                string strdept = string.Empty;
                string strOven = string.Empty;
                string strGJ = string.Empty;
                string isoven = string.Empty;
                if (DateTime.Parse(txtdrtanggal.Text) < DateTime.Parse("01-jul-2016"))
                {
                    strGJ = "where A.ID=B.DefectID and A.SerahID=C.ID and B.MasterID=D.ID and E.ID=C.DestID  and C.ItemID0=I1.ID and B.RowStatus>-1";
                    strOven = "case when D.DeptID=0 then case when (B.Qty/E.Qty) *100 >5 then 2 else 3 end else D.DeptID end deptID1";
                }
                else
                {
                    strGJ = ",Def_GroupJemur GJ  " +
                            "where A.ID=B.DefectID and A.SerahID=C.ID and B.MasterID=D.ID and E.ID=C.DestID  and C.ItemID0=I1.ID and " +
                            " GJ.ID=A.GroupJemurID and B.RowStatus>-1";
                    strOven = "case when GJ.GroupJemurName In ('K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z') then " +
                              "      case when D.DeptID=0 then  " +
                              "          case when D.DefName='retak' then 2  " +
                              "              when D.DefName='gompal' then 3 " +
                              "          end  " +
                              "      else  " +
                              "          D.DeptID  " +
                              "      end  " +
                              "  else  " +
                              "      case when D.DeptID=0 then  " +
                              "          case when (B.Qty/E.Qty) *100 >5 then 2  " +
                              "          else 3   " +
                              "          end " +
                              "      else  " +
                              "          D.DeptID  " +
                              "      end  " +
                              "  end deptID1 ";
                }
                if (ddlDepartemen0.SelectedValue == "0")
                {
                    isoven = string.Empty;
                    Session["proses"] = " ";
                }
                if (ddlDepartemen0.SelectedValue == "1")
                {
                    isoven = " and GJ.GroupJemurName In ('K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z') ";
                    Session["proses"] = ddlDepartemen0.SelectedItem.Text.Trim().ToUpper();
                }
                if (ddlDepartemen0.SelectedValue == "2")
                {
                    isoven = " and GJ.GroupJemurName Not In ('K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z') ";
                    Session["proses"] = ddlDepartemen0.SelectedItem.Text.Trim().ToUpper();
                }
                string wheredestid1 = string.Empty;
                string wheredestid2 = string.Empty;
                if (ddlDepartemen0.SelectedValue == "1")
                {
                    wheredestid1 = " S.destID in (select distinct Destid from T1_Jemur A inner join FC_Rak B on A.RakID=B.ID where B.Rak='00') and ";
                    wheredestid2 = " destID in (select distinct Destid from T1_Jemur A inner join FC_Rak B on A.RakID=B.ID where B.Rak='00') and ";
                }
                if (ddlDepartemen0.SelectedValue == "2")
                {
                    wheredestid1 = " S.destID in (select distinct Destid from T1_Jemur A inner join FC_Rak B on A.RakID=B.ID where B.Rak<>'00') and ";
                    wheredestid2 = " destID in (select distinct Destid from T1_Jemur A inner join FC_Rak B on A.RakID=B.ID where B.Rak<>'00') and ";
                }
                if (ddlDepartemen0.SelectedValue == "0")
                {
                    wheredestid1 = " ";
                    wheredestid2 = " ";
                }
                if (DeptID > 0)
                    strdept = "and DeptID1=" + ddlDepartemen.SelectedValue + " ";
                    strQuery = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempdefect]') AND type in (N'U')) DROP TABLE [dbo].[tempdefect] " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempdefect1]') AND type in (N'U')) DROP TABLE [dbo].[tempdefect1] " +
                    "select ID,defectID,masterID,Qty,M3,sfrom into tempdefect from (   " +
                    "select D.DefName,D.DeptID,B.ID,B.DefectID,B.MasterID,B.Qty,C.DestID,C.QtyIn,E.Qty as TotPotong,C.sfrom,   " +
                    strOven + ",A.Tgl,B.Qty*((I1.tebal*I1.panjang*I1.lebar)/1000000000) as M3 " +
                    "from Def_DefectDetail B,Def_Defect A,T1_Serah C,Def_MasterDefect D,BM_Destacking E ,FC_Items I1   " +
                    strGJ + isoven + " ) as Def " +
                    "where CONVERT(varchar, Tgl,112)>='" + stgl1 + "' and CONVERT(varchar, Tgl ,112)<='" + stgl2 + "' " + strdept +

                    "select ID,defectID,masterID,Qty,M3 into tempdefect1 from (   " +
                    "select D.DefName,D.DeptID,B.ID,B.DefectID,B.MasterID,B.Qty,C.DestID,C.QtyIn,E.Qty as TotPotong, " +
                    strOven + ",A.Tgl,B.Qty*((I1.tebal*I1.panjang*I1.lebar)/1000000000) as M3  " +
                    "from Def_DefectDetail B,Def_Defect A,T1_Serah C,Def_MasterDefect D,BM_Destacking E,FC_Items I1 " +
                    strGJ + isoven + " ) as Def " +
                    "where CONVERT(varchar, Tgl,112)>='" + stgl1 + "' and CONVERT(varchar, Tgl ,112)<='" + stgl2 + "' " +

                    "select ROW_NUMBER() over(order by DefName) as rNo, SUBSTRING(DefName,2,100) as defect," + sqlselect + "," + sqlselect1 +
                    " from ( " +
                    "select 'A' + C.DefName as DefName, " +
                    " CONVERT(varchar,A.Tgl ,112) as hari,cast(QTY as varchar(max)) as QTY from Def_Defect A inner join tempdefect B on A.ID=B.DefectID " +
                    " inner join Def_MasterDefect C on B.MasterID =C.ID WHERE CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
                    " union all " +
                    "  select 'A' + C.DefName as DefName, " +
                    "CONVERT(varchar,A.Tgl ,112)+'K' as hari,cast(M3 as varchar(max)) as QTY from Def_Defect A inner join tempdefect B on A.ID=B.DefectID   " +
                    " inner join Def_MasterDefect C on B.MasterID =C.ID WHERE CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
                    " union all  " +
                    "    " +
                    " select 'ETotal BP Pelarian' as DefName,CONVERT(varchar, A.Tgl ,112) as hari, (select cast(SUM(qty )as varchar(max) )from tempdefect  " +
                    " where sfrom='lari' and  defectID =A.ID) as QTY from Def_Defect A WHERE  CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
                    " union all    " +
                    " select 'ETotal BP Pelarian' as DefName,CONVERT(varchar, A.Tgl ,112)+'K' as hari, (select cast(SUM(M3 )as varchar(max) )from tempdefect  " +
                    " where sfrom='lari' and  defectID =A.ID) as QTY from Def_Defect A WHERE  CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
                    " union all    " +

                    " select 'BTotal BP' as DefName,CONVERT(varchar, A.Tgl ,112) as hari, (select cast(SUM(qty )as varchar(max) )from tempdefect  " +
                    " where defectID =A.ID) as QTY from Def_Defect A WHERE CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
                    " union all    " +
                    " select 'BTotal BP' as DefName,CONVERT(varchar, A.Tgl ,112)+'K' as hari, (select cast(SUM(M3 )as varchar(max) )from tempdefect  " +
                    " where defectID =A.ID) as QTY from Def_Defect A  WHERE CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
                    " union all   " +
                    "  " +
                    " select  DefName, hari, cast(SUM(qty)   as varchar(max))as QTY from (        " +
                    "    select 'CTotal OK' as DefName,CONVERT(varchar, TglSerah ,112) as hari, sum(QtyIn) as QTY from T1_Serah S  inner join FC_Items I1 on S.ItemID0=I1.ID  inner join FC_Items I on S.ItemID=I.ID  " +
                    "    where " + wheredestid1 + "S.Status>-1 and (I.PartNo like '%-W-%' or I.PartNo like '%-3-%') and CONVERT(char,S.tglserah,112)>='" + stgl1 + "'  and CONVERT(char,S.tglserah,112)<='" + stgl2 + "' group by TglSerah  " +
                    ")  as A  group by DefName, hari  " +
                    "union all " +
                    " select  DefName, hari, cast(SUM(qty)   as varchar(max))as QTY from (        " +
                    "    select 'CTotal OK' as DefName,CONVERT(varchar, TglSerah ,112)+'K' as hari, sum(QtyIn*I1.Volume) as QTY from T1_Serah S  inner join FC_Items I1 on S.ItemID0=I1.ID inner join FC_Items I on S.ItemID=I.ID   " +
                    "    where   " + wheredestid1 + " S.Status>-1 and (I.PartNo like '%-W-%' or I.PartNo like '%-3-%') and CONVERT(char,S.tglserah,112)>='" + stgl1 + "'  and CONVERT(char,S.tglserah,112)<='" + stgl2 + "' group by TglSerah  " +
                    ")  as A  group by DefName, hari   " +
                    "union all    " +
                    " " +
                    "select Defname,Hari, cast(SUM(qty) as varchar(max)) as QTY from (  " +
                    "    select 'DTotal Produk' as DefName,CONVERT(varchar, A.Tgl ,112) as hari, (select SUM(qty)from tempdefect1 where defectID =A.ID) as QTY from Def_Defect A WHERE CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
                    "    union all    " +
                    "    select  DefName, hari, cast(SUM(qty) as varchar(max)) as QTY from (        " +
                    "        select 'DTotal Produk' as DefName,CONVERT(varchar, TglSerah ,112) as hari, sum(QtyIn) as QTY from T1_Serah S  inner join FC_Items I on S.ItemID=I.ID  " +
                    "        where  " + wheredestid1 + " S.Status>-1 and (I.PartNo like '%-W-%' or I.PartNo like '%-3-%') and CONVERT(char,S.tglserah,112)>='" + stgl1 + "'  and CONVERT(char,S.tglserah,112)<='" + stgl2 + "'  group by TglSerah  " +
                    "        )  as A  group by DefName, hari  " +
                    "    ) as C group by  DefName, hari  " +
                    "union all   " +
                    "SELECT 'DTotal Produk' as DefName,HARI, cast(SUM(qty) as varchar(max)) QTY FROM ( " +
                    "select CONVERT(varchar, A.Tgl ,112)+'K' as hari, (select SUM(M3)from tempdefect1 where defectID =A.ID) as QTY from Def_Defect A    " +
                    "WHERE CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
                    "UNION ALL " +
                    "select CONVERT(varchar, TglSerah ,112)+'K' as hari,sum(QtyIn*I1.Volume) as QTY from T1_Serah S  inner join FC_Items I on S.ItemID=I.ID  inner join FC_Items I1 on S.itemID0=I1.ID  " +
                    "        where " + wheredestid1 + " S.Status>-1 and (I.PartNo like '%-W-%' or I.PartNo like '%-3-%')  " +
                    "        and CONVERT(char,S.tglserah,112)>='" + stgl1 + "'  and CONVERT(char,S.tglserah,112)<='" + stgl2 + "'  GROUP BY TglSerah) AS c GROUP BY HARI  " +
                    "union all   " +
                    " " +
                    "select  DefName, hari, case when sum(QTYB ) >0 then(SUM(QTYA) / sum(QTYB )) *100 else 0 end  QTY from (        " +
                    "    select DefName,Hari, SUM(QTY) as QTYA,0 as QTYB from (      " +
                    "        select 'ETotal Prosentase BP' as DefName,CONVERT(varchar, A.Tgl ,112) as hari,      " +
                    "        (select SUM(qty)from tempdefect where defectID =A.ID) as QTY from Def_Defect A )as D group by DefName,Hari " +
                    "    union ALL      " +
                    "    select Defname,Hari, 0 as QTYA, SUM(qty) as QTYB from (          " +
                    "        select 'ETotal Prosentase BP' as DefName,CONVERT(varchar, A.Tgl ,112) as hari,          " +
                    "        (select SUM(cast(qty as decimal))from tempdefect1 where defectID =A.ID) as QTY from Def_Defect A " +
                    "        union all            " +
                    "        select  DefName, hari, SUM(qty)as QTY from (            " +
                    "        select 'ETotal Prosentase BP' as DefName,CONVERT(varchar, TglSerah ,112) as hari, QtyIn as QTY from T1_Serah where status>-1 and " + wheredestid2 + " ItemID in " +
                    "        (select ID from FC_Items where PartNo like '%-W-%' or PartNo like '%-3-%') " +
                    "        )  as A  group by DefName, hari  " +
                    "    ) as C group by  DefName, hari      " +
                    ")  as A  group by DefName, hari ) up pivot (sum(QTY)for hari in (" + sqlinpivot + "," + sqlinpivot1 + ")) as A1 " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempdefect]') AND type in (N'U')) DROP TABLE [dbo].[tempdefect] " +
                    "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempdefect1]') AND type in (N'U')) DROP TABLE [dbo].[tempdefect1] ";
                Session["Query"] = strQuery;
                Session["dept"] = deptname;
                Session["prd1"] = tgl1;
                Session["prd2"] = tgl2;
                Cetak(this);
            }
            #endregion
            #region Untuk Karawang
            else if (users.UnitKerjaID == 7 || users.UnitKerjaID == 1 || users.UnitKerjaID == 13)
            {
                if (txtdrtanggal.Text == string.Empty || txtsdtanggal.Text == string.Empty)
                {
                    return;
                }
                DateTime intgl1 = DateTime.Parse(txtdrtanggal.Text);
                DateTime intgl2 = DateTime.Parse(txtsdtanggal.Text);
                string tglakhir = string.Empty;
                int tambahtgl = 0;
                long jmlHari = DateDiff(DateInterval.Day, intgl1, intgl2) + 1;
                if (jmlHari > 31)
                {
                    string periode = DateTime.Parse(txtdrtanggal.Text).ToString("dd MMMM yyyy") + "  s/d  " +
                        DateTime.Parse(txtdrtanggal.Text).AddDays(40).ToString("dd MMMM yyyy");
                    tglakhir = DateTime.Parse(txtdrtanggal.Text).AddDays(40).ToString("MM/dd/yyyy");
                    Session["periode"] = periode;
                }
                else
                {
                    string periode = DateTime.Parse(txtdrtanggal.Text).ToString("dd MMMM yyyy") + "  s/d  " +
                        DateTime.Parse(txtsdtanggal.Text).ToString("dd MMMM yyyy");
                    tglakhir = DateTime.Parse(txtsdtanggal.Text).ToString("MM/dd/yyyy");
                    Session["periode"] = periode;
                }
                string tglproses = string.Empty;
                string sqlselect = string.Empty;
                string sqlinpivot = string.Empty;
                string sqlselect1 = string.Empty;
                string sqlinpivot1 = string.Empty;
                for (int i = 0; i < 31; i++)
                {
                    if (i == 0)
                        tglproses = (i + 1).ToString().PadLeft(2, '0') + "-" + DateTime.Parse(txtdrtanggal.Text).ToString("MMM") + "-" + DateTime.Parse(txtdrtanggal.Text).ToString("yyyy");
                    else
                        tglproses = DateTime.Parse(tglproses).AddDays(1).ToString("dd-MMM-yyyy");
                    if (DateTime.Parse(tglproses) >= DateTime.Parse(txtdrtanggal.Text) && DateTime.Parse(tglproses) <= DateTime.Parse(txtsdtanggal.Text))
                    {
                        if (i < 30)
                        {
                            sqlselect = sqlselect + "isnull([" + DateTime.Parse(txtdrtanggal.Text).AddDays(i).ToString("yyyy") +
                                DateTime.Parse(txtdrtanggal.Text).ToString("MM") + (i + 1).ToString().PadLeft(2, '0') + "],0) as " +
                                "[" + (i + 1).ToString() + "],";
                            sqlinpivot = sqlinpivot + "[" + DateTime.Parse(txtdrtanggal.Text).AddDays(i).ToString("yyyy") +
                                DateTime.Parse(txtdrtanggal.Text).ToString("MM") + (i + 1).ToString().PadLeft(2, '0') + "],";
                        }
                        else
                        {
                            sqlselect = sqlselect + "isnull([" + DateTime.Parse(txtdrtanggal.Text).AddDays(i).ToString("yyyy") +
                               DateTime.Parse(txtdrtanggal.Text).ToString("MM") + (i + 1).ToString().PadLeft(2, '0') + "],0) as " +
                               "[" + (i + 1).ToString() + "]";
                            sqlinpivot = sqlinpivot + "[" + DateTime.Parse(txtdrtanggal.Text).AddDays(i).ToString("yyyy") +
                                DateTime.Parse(txtdrtanggal.Text).ToString("MM") + (i + 1).ToString().PadLeft(2, '0') + "]";
                        }

                    }
                    else
                    {
                        if (i < 30)
                            sqlselect = sqlselect + "isnull([" + (i + 1).ToString() + "],0)as " +
                            "[" + (i + 1).ToString() + "],";
                        else
                            sqlselect = sqlselect + "isnull([" + (i + 1).ToString() + "],0)as " +
                            "[" + (i + 1).ToString() + "]";
                        if (i < 30)
                            sqlinpivot = sqlinpivot + "[" + (i + 1).ToString() + "],";
                        else
                            sqlinpivot = sqlinpivot + "[" + (i + 1).ToString() + "]";
                    }


                    #region remark
                    //}
                    //else
                    //{
                    //    sqltanggal[i] = DateTime.Parse("01/01/1900").ToString("MM/dd/yyyy");
                    //    if (i < 30)
                    //        sqlselect = sqlselect + "isnull([" + (i + 1).ToString() + "],0)as " +
                    //        "[" + (i + 1).ToString() + "],";
                    //    else
                    //        sqlselect = sqlselect + "isnull([" + (i + 1).ToString() + "],0)as " +
                    //        "[" + (i + 1).ToString() + "]";
                    //    if (i < 30)
                    //        sqlinpivot = sqlinpivot + "[" + (i + 1).ToString() + "],";
                    //    else
                    //        sqlinpivot = sqlinpivot + "[" + (i + 1).ToString() + "]";
                    //}
                    //string tempstr = sqltanggal[i];
                    #endregion

                }
                #region kubik
                for (int i = 0; i < 31; i++)
                {
                    if (i == 0)
                        tglproses = (i + 1).ToString().PadLeft(2, '0') + "-" + DateTime.Parse(txtdrtanggal.Text).ToString("MMM") + "-" + DateTime.Parse(txtdrtanggal.Text).ToString("yyyy");
                    else
                        tglproses = DateTime.Parse(tglproses).AddDays(1).ToString("dd-MMM-yyyy");
                    if (DateTime.Parse(tglproses) >= DateTime.Parse(txtdrtanggal.Text) && DateTime.Parse(tglproses) <= DateTime.Parse(txtsdtanggal.Text))
                    {
                        if (i < 30)
                        {
                            sqlselect1 = sqlselect1 + "isnull([" + DateTime.Parse(txtdrtanggal.Text).AddDays(i).ToString("yyyy") +
                                DateTime.Parse(txtdrtanggal.Text).ToString("MM") + (i + 1).ToString().PadLeft(2, '0') + "K],0) as " +
                                "[" + (i + 1).ToString() + "K],";
                            sqlinpivot1 = sqlinpivot1 + "[" + DateTime.Parse(txtdrtanggal.Text).AddDays(i).ToString("yyyy") +
                                DateTime.Parse(txtdrtanggal.Text).ToString("MM") + (i + 1).ToString().PadLeft(2, '0') + "K],";
                        }
                        else
                        {
                            sqlselect1 = sqlselect1 + "isnull([" + DateTime.Parse(txtdrtanggal.Text).AddDays(i).ToString("yyyy") +
                               DateTime.Parse(txtdrtanggal.Text).ToString("MM") + (i + 1).ToString().PadLeft(2, '0') + "K],0) as " +
                               "[" + (i + 1).ToString() + "K]";
                            sqlinpivot1 = sqlinpivot1 + "[" + DateTime.Parse(txtdrtanggal.Text).AddDays(i).ToString("yyyy") +
                                DateTime.Parse(txtdrtanggal.Text).ToString("MM") + (i + 1).ToString().PadLeft(2, '0') + "K]";
                        }
                    }
                    else
                    {
                        if (i < 30)
                            sqlselect1 = sqlselect1 + "isnull([" + (i + 1).ToString() + "K],0)as " +
                            "[" + (i + 1).ToString() + "K],";
                        else
                            sqlselect1 = sqlselect1 + "isnull([" + (i + 1).ToString() + "K],0)as " +
                            "[" + (i + 1).ToString() + "K]";
                        if (i < 30)
                            sqlinpivot1 = sqlinpivot1 + "[" + (i + 1).ToString() + "K],";
                        else
                            sqlinpivot1 = sqlinpivot1 + "[" + (i + 1).ToString() + "K]";
                    }
                }
                #endregion
                //LemburFacade lemburfacade = new LemburFacade();
                //Users users = (Users)Session["Users"];
                //if (ddlDepartemen.SelectedIndex == 0)
                //{ return; }
                int DeptID = Convert.ToInt32(ddlDepartemen.SelectedValue);
                string deptname = ddlDepartemen.SelectedItem.Text;
                string strQuery = string.Empty;
                string tgl1 = DateTime.Parse(txtdrtanggal.Text).ToString("dd MMMM yyyy");
                string tgl2 = DateTime.Parse(txtsdtanggal.Text).ToString("dd MMMM yyyy");
                string stgl1 = DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd");
                string stgl2 = DateTime.Parse(txtsdtanggal.Text).ToString("yyyyMMdd");

                string strdept = string.Empty;
                string strOven = string.Empty;
                string strGJ = string.Empty;
                string isoven = string.Empty;
                if (DateTime.Parse(txtdrtanggal.Text) < DateTime.Parse("01-jul-2016"))
                {
                    strGJ = "where A.ID=B.DefectID and A.SerahID=C.ID and B.MasterID=D.ID and E.ID=C.DestID  and C.ItemID0=I1.ID and B.RowStatus>-1";
                    strOven = "case when D.DeptID=0 then case when (B.Qty/E.Qty) *100 >5 then 2 else 3 end else D.DeptID end deptID1";
                }
                else
                {
                    if (Convert.ToInt32(stgl1.Substring(0, 6)) < 201805)
                    {
                        strGJ = ",Def_GroupJemur GJ  " +
                                "where A.ID=B.DefectID and A.SerahID=C.ID and B.MasterID=D.ID and D.RowStatus>-1 and E.ID=C.DestID  and C.ItemID0=I1.ID and " +
                                " GJ.ID=A.GroupJemurID and B.RowStatus>-1 ";
                        strOven = "case when GJ.GroupJemurName In ('K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z') then " +
                                  "      case when D.DeptID=0 then  " +
                                  "          case when D.DefName='retak' then 2  " +
                                  "              when D.DefName='gompal' then 3 " +
                                  "          end  " +
                                  "      else  " +
                                  "          D.DeptID  " +
                                  "      end  " +
                                  "  else  " +
                                  "      case when D.DeptID=0 then  " +
                                  "          case when (B.Qty/E.Qty) *100 >5 then 2  " +
                                  "          else 3   " +
                                  "          end " +
                                  "      else  " +
                                  "          D.DeptID  " +
                                  "      end  " +
                                  "  end deptID1 ";
                    }
                    else
                    {
                        strGJ = ",Def_GroupJemur GJ  " +
                                " where A.ID=B.DefectID  and B.MasterID=D.ID and D.RowStatus>-1 and  GJ.ID=A.GroupJemurID and B.RowStatus>-1  and E.ID=A.Destid and I1.ID=E.ItemID ";
                        strOven = "case when GJ.GroupJemurName In ('K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z') then " +
                                  "      case when D.DeptID=0 then  " +
                                  "          case when D.DefName='retak' then 2  " +
                                  "              when D.DefName='gompal' then 3 " +
                                  "          end  " +
                                  "      else  " +
                                  "          D.DeptID  " +
                                  "      end  " +
                                  "  else  " +
                                  "      case when D.DeptID=0 then  " +
                                  "          case when (B.Qty/A.Tpotong) *100 >5 then 2  " +
                                  "          else 3   " +
                                  "          end " +
                                  "      else  " +
                                  "          D.DeptID  " +
                                  "      end  " +
                                  "  end deptID1 ";
                    }
                }
                if (ddlDepartemen0.SelectedValue == "0")
                {
                    isoven = string.Empty;
                    Session["proses"] = " ";
                }
                if (ddlDepartemen0.SelectedValue == "1")
                {
                    isoven = " and GJ.GroupJemurName In ('K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z') ";
                    Session["proses"] = ddlDepartemen0.SelectedItem.Text.Trim().ToUpper();
                }
                if (ddlDepartemen0.SelectedValue == "2")
                {
                    isoven = " and GJ.GroupJemurName Not In ('K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z') ";
                    Session["proses"] = ddlDepartemen0.SelectedItem.Text.Trim().ToUpper();
                }
                string wheredestid1 = string.Empty;
                string wheredestid2 = string.Empty;
                if (ddlDepartemen0.SelectedValue == "1")
                {
                    wheredestid1 = " S.destID in (select distinct Destid from T1_Jemur A inner join FC_Rak B on A.RakID=B.ID where B.Rak='00') and ";
                    wheredestid2 = " destID in (select distinct Destid from T1_Jemur A inner join FC_Rak B on A.RakID=B.ID where B.Rak='00') and ";
                }
                if (ddlDepartemen0.SelectedValue == "2")
                {
                    wheredestid1 = " S.destID in (select distinct Destid from T1_Jemur A inner join FC_Rak B on A.RakID=B.ID where B.Rak<>'00') and ";
                    wheredestid2 = " destID in (select distinct Destid from T1_Jemur A inner join FC_Rak B on A.RakID=B.ID where B.Rak<>'00') and ";
                }
                if (ddlDepartemen0.SelectedValue == "0")
                {
                    wheredestid1 = " ";
                    wheredestid2 = " ";
                }
                string ispressing = string.Empty;
                Session["pressing"] = "";
                if (ddlPressing.SelectedValue.Trim().ToUpper() == "YES")
                {
                    ispressing = " and I1.pressing='YES' ";
                    Session["pressing"] = ddlPressing.SelectedItem.Text.Trim().ToUpper();
                }
                if (ddlPressing.SelectedValue.Trim().ToUpper() == "NO")
                {
                    ispressing = " and I1.pressing='NO' ";
                    Session["pressing"] = ddlPressing.SelectedItem.Text.Trim().ToUpper();
                }
                string tpotong = string.Empty;
                string strSQL = string.Empty;
                if (DeptID > 0)
                    strdept = "and DeptID1=" + ddlDepartemen.SelectedValue + " ";
                if (Convert.ToInt32(stgl1.Substring(0, 6)) >= 201805)
                #region 201805
                {
                    tpotong = "A.TPotong";
                    strSQL = "select ID,defectID,masterID,defname,Qty,M3,TotPotong,TotPotongM3,Tkw,tkwM3,'' sfrom into tempdefect from (   " +
                    "select D.DefName,D.DeptID,B.ID,B.DefectID,B.MasterID,B.Qty,A.DestID," + tpotong + " as TotPotong,   " +
                    strOven + ",A.Tgl,B.Qty*I1.Volume as M3," + tpotong + " as TotPotongM3,isnull(A.Tkw,0)Tkw,isnull(A.Tkw,0)*((I1.tebal*I1.panjang*I1.lebar)/1000000000) tkwM3,I1.Pressing " +
                    "from Def_DefectDetail B,Def_Defect A,Def_MasterDefect D,BM_Destacking E ,FC_Items I1 " +
                    strGJ + isoven + ispressing + " ) as Def " +
                    "where CONVERT(varchar, Tgl,112)>='" + stgl1 + "' and CONVERT(varchar, Tgl ,112)<='" + stgl2 + "' " + strdept +

                    "select ID,defectID,masterID,Qty,M3,TotPotong,TotPotongM3,Tkw,tkwM3,'' sfrom into tempdefect1 from (   " +
                    "select D.DefName,D.DeptID,B.ID,B.DefectID,B.MasterID,B.Qty,A.DestID," + tpotong + " as TotPotong, " +
                    strOven + ",A.Tgl,B.Qty*((I1.tebal*I1.panjang*I1.lebar)/1000000000) as M3 ,isnull(A.Tkw,0)Tkw,isnull(A.Tkw,0)*((I1.tebal*I1.panjang*I1.lebar)/1000000000) tkwM3," + tpotong + " as TotPotongM3,I1.Pressing  " +
                    "from Def_DefectDetail B,Def_Defect A,Def_MasterDefect D,BM_Destacking E ,FC_Items I1 " +
                    strGJ + isoven + ispressing + " ) as Def " +
                    "where CONVERT(varchar, Tgl,112)>='" + stgl1 + "' and CONVERT(varchar, Tgl ,112)<='" + stgl2 + "' " +

                    "select ROW_NUMBER() over(order by DefName) as rNo, SUBSTRING(DefName,2,100) as defect," + sqlselect + "," + sqlselect1 +
                    " from ( " +
                    "select 'A' + B.DefName as DefName, " +
                    " CONVERT(varchar,A.Tgl ,112) as hari,cast(QTY as varchar(max)) as QTY from Def_Defect A inner join tempdefect B on A.ID=B.DefectID " +
                    "  WHERE CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
                    " union all " +
                    "  select 'A' + B.DefName as DefName, " +
                    " CONVERT(varchar,A.Tgl ,112)+'K' as hari,cast(M3 as varchar(max)) as QTY from Def_Defect A inner join tempdefect B on A.ID=B.DefectID   " +
                    "  WHERE CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
                    " union all  " +
                    "    " +
                    " select 'ETotal BP Pelarian' as DefName,CONVERT(varchar, A.Tgl ,112) as hari, (select cast(SUM(qty )as varchar(max) )from tempdefect  " +
                    " where sfrom='lari' and  defectID =A.ID) as QTY from Def_Defect A WHERE  CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
                    " union all    " +
                    " select 'ETotal BP Pelarian' as DefName,CONVERT(varchar, A.Tgl ,112)+'K' as hari, (select cast(SUM(M3 )as varchar(max) )from tempdefect  " +
                    " where sfrom='lari' and  defectID =A.ID) as QTY from Def_Defect A WHERE  CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
                    " union all    " +
                    "  " +
                    " select 'BTotal BP' as DefName,CONVERT(varchar, A.Tgl ,112) as hari, (select cast(SUM(qty )as varchar(max) )from tempdefect  " +
                    " where defectID =A.ID) as QTY from Def_Defect A WHERE CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
                    " union all    " +
                    " select 'BTotal BP' as DefName,CONVERT(varchar, A.Tgl ,112)+'K' as hari, (select cast(SUM(M3 )as varchar(max) )from tempdefect  " +
                    " where defectID =A.ID) as QTY from Def_Defect A  WHERE CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
                    " union all   " +
                    "  " +
                    " select 'CTotal KW' as DefName,CONVERT(varchar, A.tgl ,112) as hari,  cast(SUM(A.tkw )as varchar(max))  as QTY from Def_Defect A  " +
                    " WHERE CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  group by CONVERT(varchar, A.tgl ,112)" +
                    " union all    " +
                    " select 'CTotal KW' as DefName,CONVERT(varchar, A.Tgl ,112)+'K' as hari, cast(SUM(tkw *((I1.tebal*I1.panjang*I1.lebar)/1000000000) )as varchar(max) ) as QTY from Def_Defect A "+
                    " inner join bm_destacking B on A.Destid=B.id inner join fc_items I1 on B.itemid=I1.id WHERE CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  group by CONVERT(varchar, A.tgl ,112) " +
                    " union all   " +
                    "  " +
                    " select 'CTotal OK' as DefName,CONVERT(varchar, A.Tgl ,112) as hari, (select cast(SUM(qty *-1)as varchar(max) )from tempdefect  " +
                    " where defectID =A.ID) as QTY from Def_Defect A WHERE CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
                    " union all    " +
                    " select 'CTotal OK' as DefName,CONVERT(varchar, A.Tgl ,112)+'K' as hari, (select cast(SUM(M3 *-1)as varchar(max) )from tempdefect  " +
                    " where defectID =A.ID) as QTY from Def_Defect A  WHERE CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
                    " union all   " +

                    " select 'CTotal OK' as DefName,CONVERT(varchar, A.Tgl ,112) as hari,cast(SUM(A.tkw *-1)as varchar(max)) as QTY from Def_Defect A WHERE CONVERT(char, A.Tgl,112)>='" + stgl1 + 
                    " '  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "' group by CONVERT(varchar, A.tgl ,112) " +
                    " union all    " +
                    " select 'CTotal OK' as DefName,CONVERT(varchar, A.Tgl ,112)+'K' as hari, cast(SUM(-1 * tkw *((I1.tebal*I1.panjang*I1.lebar)/1000000000) )as varchar(max) ) as QTY from Def_Defect A  "+
                    "inner join bm_destacking B on A.Destid=B.id inner join fc_items I1 on B.itemid=I1.id   WHERE CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "' group by CONVERT(varchar, A.tgl ,112) " +
                    " union all   " +

                    " select 'CTotal OK' as DefName,CONVERT(varchar, A.Tgl ,112) as hari, tpotong QTY from Def_Defect A WHERE A.status>-1 and " +
                    " CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
                    " union all    " +
                    " select 'CTotal OK' as DefName,CONVERT(varchar, A.Tgl ,112)+'K' as hari, tpotong*I.Volume   QTY  " +
                    " from Def_Defect A  inner join BM_Destacking D on A.Destid=D.ID inner join FC_Items I on I.ID=D.ItemID WHERE A.status>-1 and CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
                    " union all   " +
                    " select 'DTotal Potong' as DefName,CONVERT(varchar, A.Tgl ,112) as hari, tpotong QTY from Def_Defect A WHERE A.status>-1 and " +
                    " CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
                    " union all    " +
                    " select 'DTotal Potong' as DefName,CONVERT(varchar, A.Tgl ,112)+'K' as hari, tpotong*I.Volume   QTY  " +
                    " from Def_Defect A  inner join BM_Destacking D on A.Destid=D.ID inner join FC_Items I on I.ID=D.ItemID WHERE A.status>-1 and CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
                    " union all   " +
                    " " +
                    "select  DefName, hari, case when sum(QTYB ) >0 then(SUM(QTYA) / sum(QTYB )) *100 else 0 end  QTY from (        " +
                    "    select DefName,Hari, SUM(QTY) as QTYA,0 as QTYB from (      " +
                    "        select 'ETotal Prosentase BP' as DefName,CONVERT(varchar, A.Tgl ,112) as hari,      " +
                    "        (select SUM(qty)from tempdefect where defectID =A.ID) as QTY from Def_Defect A )as D group by DefName,Hari " +
                    "    union ALL      " +
                    "    select Defname,Hari, 0 as QTYA, SUM(qty) as QTYB from (          " +
                    "        select 'ETotal Prosentase BP' as DefName ,CONVERT(varchar, A.Tgl ,112) as hari, tpotong QTY from Def_Defect A WHERE A.status>-1 and " +
                    " CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "' " +
                    "    ) as C group by  DefName, hari      " +
                    ")  as A  group by DefName, hari ) up pivot (sum(QTY)for hari in (" + sqlinpivot + "," + sqlinpivot1 + ")) as A1 ";
                }
                #endregion
                else
                {
                    tpotong = "E.Qty";
                    strSQL = "select ID,defectID,masterID,Qty,M3,sfrom into tempdefect from (   " +
                    "select D.DefName,D.DeptID,B.ID,B.DefectID,B.MasterID,B.Qty,C.DestID," + tpotong + " as TotPotong,C.sfrom,   " +
                    strOven + ",A.Tgl,B.Qty*((I1.tebal*I1.panjang*I1.lebar)/1000000000) as M3 " +
                    "from Def_DefectDetail B,Def_Defect A,T1_Serah C,Def_MasterDefect D,BM_Destacking E ,FC_Items I1   " +
                    strGJ + isoven + " ) as Def " +
                    "where CONVERT(varchar, Tgl,112)>='" + stgl1 + "' and CONVERT(varchar, Tgl ,112)<='" + stgl2 + "' " + strdept +

                    "select ID,defectID,masterID,Qty,M3 into tempdefect1 from (   " +
                    "select D.DefName,D.DeptID,B.ID,B.DefectID,B.MasterID,B.Qty,C.DestID," + tpotong + " as TotPotong, " +
                    strOven + ",A.Tgl,B.Qty*((I1.tebal*I1.panjang*I1.lebar)/1000000000) as M3  " +
                    "from Def_DefectDetail B,Def_Defect A,T1_Serah C,Def_MasterDefect D,BM_Destacking E,FC_Items I1 " +
                    strGJ + isoven + " ) as Def " +
                    "where CONVERT(varchar, Tgl,112)>='" + stgl1 + "' and CONVERT(varchar, Tgl ,112)<='" + stgl2 + "' " +
                    "select ROW_NUMBER() over(order by DefName) as rNo, SUBSTRING(DefName,2,100) as defect," + sqlselect + "," + sqlselect1 +
                    " from ( " +
                    "select 'A' + C.DefName as DefName, " +
                    " CONVERT(varchar,A.Tgl ,112) as hari,cast(QTY as varchar(max)) as QTY from Def_Defect A inner join tempdefect B on A.ID=B.DefectID " +
                    " inner join Def_MasterDefect C on B.MasterID =C.ID WHERE CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
                    " union all " +
                    "  select 'A' + C.DefName as DefName, " +
                    "CONVERT(varchar,A.Tgl ,112)+'K' as hari,cast(M3 as varchar(max)) as QTY from Def_Defect A inner join tempdefect B on A.ID=B.DefectID   " +
                    " inner join Def_MasterDefect C on B.MasterID =C.ID WHERE CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
                    " union all  " +
                    "    " +
                    " select 'ETotal BP Pelarian' as DefName,CONVERT(varchar, A.Tgl ,112) as hari, (select cast(SUM(qty )as varchar(max) )from tempdefect  " +
                    " where sfrom='lari' and  defectID =A.ID) as QTY from Def_Defect A WHERE  CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
                    " union all    " +
                    " select 'ETotal BP Pelarian' as DefName,CONVERT(varchar, A.Tgl ,112)+'K' as hari, (select cast(SUM(M3 )as varchar(max) )from tempdefect  " +
                    " where sfrom='lari' and  defectID =A.ID) as QTY from Def_Defect A WHERE  CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
                    " union all    " +

                    " select 'BTotal BP' as DefName,CONVERT(varchar, A.Tgl ,112) as hari, (select cast(SUM(qty )as varchar(max) )from tempdefect  " +
                    " where defectID =A.ID) as QTY from Def_Defect A WHERE CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
                    " union all    " +
                    " select 'BTotal BP' as DefName,CONVERT(varchar, A.Tgl ,112)+'K' as hari, (select cast(SUM(M3 )as varchar(max) )from tempdefect  " +
                    " where defectID =A.ID) as QTY from Def_Defect A  WHERE CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
                    " union all   " +
                    "  " +
                    " select  DefName, hari, cast(SUM(qty)   as varchar(max))as QTY from (        " +
                    "    select 'CTotal OK' as DefName,CONVERT(varchar, TglSerah ,112) as hari, sum(QtyIn) as QTY from T1_Serah S  inner join FC_Items I1 on S.ItemID0=I1.ID  inner join FC_Items I on S.ItemID=I.ID  " +
                    "    where " + wheredestid1 + "S.Status>-1 and (I.PartNo like '%-W-%' or I.PartNo like '%-3-%') and CONVERT(char,S.tglserah,112)>='" + stgl1 + "'  and CONVERT(char,S.tglserah,112)<='" + stgl2 + "' group by TglSerah  " +
                    ")  as A  group by DefName, hari  " +
                    "union all " +
                    " select  DefName, hari, cast(SUM(qty)   as varchar(max))as QTY from (        " +
                    "    select 'CTotal OK' as DefName,CONVERT(varchar, TglSerah ,112)+'K' as hari, sum(QtyIn*I1.Volume) as QTY from T1_Serah S  inner join FC_Items I1 on S.ItemID0=I1.ID inner join FC_Items I on S.ItemID=I.ID   " +
                    "    where   " + wheredestid1 + " S.Status>-1 and (I.PartNo like '%-W-%' or I.PartNo like '%-3-%') and CONVERT(char,S.tglserah,112)>='" + stgl1 + "'  and CONVERT(char,S.tglserah,112)<='" + stgl2 + "' group by TglSerah  " +
                    ")  as A  group by DefName, hari   " +
                    "union all    " +
                    " " +
                    "select Defname,Hari, cast(SUM(qty) as varchar(max)) as QTY from (  " +
                    "    select 'DTotal Potong' as DefName,CONVERT(varchar, A.Tgl ,112) as hari, (select SUM(qty)from tempdefect1 where defectID =A.ID) as QTY from Def_Defect A WHERE CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
                    "    union all    " +
                    "    select  DefName, hari, cast(SUM(qty) as varchar(max)) as QTY from (        " +
                    "        select 'DTotal Potong' as DefName,CONVERT(varchar, TglSerah ,112) as hari, sum(QtyIn) as QTY from T1_Serah S  inner join FC_Items I on S.ItemID=I.ID  " +
                    "        where  " + wheredestid1 + " S.Status>-1 and (I.PartNo like '%-W-%' or I.PartNo like '%-3-%') and CONVERT(char,S.tglserah,112)>='" + stgl1 + "'  and CONVERT(char,S.tglserah,112)<='" + stgl2 + "'  group by TglSerah  " +
                    "        )  as A  group by DefName, hari  " +
                    "    ) as C group by  DefName, hari  " +
                    "union all   " +
                    "SELECT 'DTotal Potong' as DefName,HARI, cast(SUM(qty) as varchar(max)) QTY FROM ( " +
                    "select CONVERT(varchar, A.Tgl ,112)+'K' as hari, (select SUM(M3)from tempdefect1 where defectID =A.ID) as QTY from Def_Defect A    " +
                    "WHERE CONVERT(char, A.Tgl,112)>='" + stgl1 + "'  and  CONVERT(char, A.Tgl,112)<='" + stgl2 + "'  " +
                    "UNION ALL " +
                    "select CONVERT(varchar, TglSerah ,112)+'K' as hari,sum(QtyIn*I1.Volume) as QTY from T1_Serah S  inner join FC_Items I on S.ItemID=I.ID  inner join FC_Items I1 on S.itemID0=I1.ID  " +
                    "        where " + wheredestid1 + " S.Status>-1 and (I.PartNo like '%-W-%' or I.PartNo like '%-3-%')  " +
                    "        and CONVERT(char,S.tglserah,112)>='" + stgl1 + "'  and CONVERT(char,S.tglserah,112)<='" + stgl2 + "'  GROUP BY TglSerah) AS c GROUP BY HARI  " +
                    "union all   " +
                    " " +
                    "select  DefName, hari, case when sum(QTYB ) >0 then(SUM(QTYA) / sum(QTYB )) *100 else 0 end  QTY from (        " +
                    "    select DefName,Hari, SUM(QTY) as QTYA,0 as QTYB from (      " +
                    "        select 'ETotal Prosentase BP' as DefName,CONVERT(varchar, A.Tgl ,112) as hari,      " +
                    "        (select SUM(qty)from tempdefect where defectID =A.ID) as QTY from Def_Defect A )as D group by DefName,Hari " +
                    "    union ALL      " +
                    "    select Defname,Hari, 0 as QTYA, SUM(qty) as QTYB from (          " +
                    "        select 'ETotal Prosentase BP' as DefName,CONVERT(varchar, A.Tgl ,112) as hari,          " +
                    "        (select SUM(cast(qty as decimal))from tempdefect1 where defectID =A.ID) as QTY from Def_Defect A " +
                    "        union all            " +
                    "        select  DefName, hari, SUM(qty)as QTY from (            " +
                    "        select 'ETotal Prosentase BP' as DefName,CONVERT(varchar, TglSerah ,112) as hari, QtyIn as QTY from T1_Serah where status>-1 and " + wheredestid2 + " ItemID in " +
                    "        (select ID from FC_Items where PartNo like '%-W-%' or PartNo like '%-3-%') " +
                    "        )  as A  group by DefName, hari  " +
                    "    ) as C group by  DefName, hari      " +
                    ")  as A  group by DefName, hari ) up pivot (sum(QTY)for hari in (" + sqlinpivot + "," + sqlinpivot1 + ")) as A1 ";
                }
                strQuery = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempdefect]') AND type in (N'U')) DROP TABLE [dbo].[tempdefect] " +
                   "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempdefect1]') AND type in (N'U')) DROP TABLE [dbo].[tempdefect1] " +
                   strSQL +
                   "/*IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempdefect]') AND type in (N'U')) DROP TABLE [dbo].[tempdefect] " +
                   "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempdefect1]') AND type in (N'U')) DROP TABLE [dbo].[tempdefect1] */";
                Session["Query"] = strQuery;
                Session["dept"] = deptname;
                Session["prd1"] = tgl1;
                Session["prd2"] = tgl2;
                //Cetak(this);
            }
            #endregion
        }
        #endregion

        static public void Cetak(Control page)
        {
            string myScript = "var wn = window.showModalDialog('../../ReportDefect/Report.aspx?IdReport=RekapDefect', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        public enum DateInterval
        {
            Day,
            DayOfYear,
            Hour,
            Minute,
            Month,
            Quarter,
            Second,
            Weekday,
            WeekOfYear,
            Year
        }

        public static long DateDiff(DateInterval interval, DateTime dt1, DateTime dt2)
        {
            return DateDiff(interval, dt1, dt2, System.Globalization.DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek);
        }

        private static int GetQuarter(int nMonth)
        {
            if (nMonth <= 3)
                return 1;
            if (nMonth <= 6)
                return 2;
            if (nMonth <= 9)
                return 3;
            return 4;
        }

        public static long DateDiff(DateInterval interval, DateTime dt1, DateTime dt2, DayOfWeek eFirstDayOfWeek)
        {
            if (interval == DateInterval.Year)
                return dt2.Year - dt1.Year;

            if (interval == DateInterval.Month)
                return (dt2.Month - dt1.Month) + (12 * (dt2.Year - dt1.Year));

            TimeSpan ts = dt2 - dt1;

            if (interval == DateInterval.Day || interval == DateInterval.DayOfYear)
                return Round(ts.TotalDays);

            if (interval == DateInterval.Hour)
                return Round(ts.TotalHours);

            if (interval == DateInterval.Minute)
                return Round(ts.TotalMinutes);

            if (interval == DateInterval.Second)
                return Round(ts.TotalSeconds);

            if (interval == DateInterval.Weekday)
            {
                return Round(ts.TotalDays / 7.0);
            }

            if (interval == DateInterval.WeekOfYear)
            {
                while (dt2.DayOfWeek != eFirstDayOfWeek)
                    dt2 = dt2.AddDays(-1);
                while (dt1.DayOfWeek != eFirstDayOfWeek)
                    dt1 = dt1.AddDays(-1);
                ts = dt2 - dt1;
                return Round(ts.TotalDays / 7.0);
            }

            if (interval == DateInterval.Quarter)
            {
                double d1Quarter = GetQuarter(dt1.Month);
                double d2Quarter = GetQuarter(dt2.Month);
                double d1 = d2Quarter - d1Quarter;
                double d2 = (4 * (dt2.Year - dt1.Year));
                return Round(d1 + d2);
            }

            return 0;

        }

        private static long Round(double dVal)
        {
            if (dVal >= 0)
                return (long)Math.Floor(dVal);
            return (long)Math.Ceiling(dVal);
        }
    }
}