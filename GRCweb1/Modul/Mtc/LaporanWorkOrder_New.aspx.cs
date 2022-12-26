using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using BusinessFacade;
using Domain;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace GRCweb1.Modul.MTC
{
    public partial class LaporanWorkOrder_New : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";

                Users user = ((Users)Session["Users"]);


                WorkOrder_New domainSR = new WorkOrder_New();
                WorkOrderFacade_New facadeSR = new WorkOrderFacade_New();
                domainSR = facadeSR.Retrieve_StatusReport(user.ID);

                int Flag = Convert.ToInt32(Session["Flag"]);

                if (domainSR.StatusReport == "5" || domainSR.StatusReport == "0" && domainSR.StatusApv == "3")
                {
                    Session["FlagRB"] = "RB";
                }
                else
                {
                    Session["FlagRB"] = "NonRB";
                }

                if (Convert.ToInt32(Session["Flag"]) != 1)
                {
                    WorkOrder_New wor1 = new WorkOrder_New();
                    WorkOrderFacade_New worf1 = new WorkOrderFacade_New();
                    ArrayList arrOut = new ArrayList();
                    arrOut = worf1.RetrieveOutWO(user.DeptID, domainSR.StatusReport.ToString());
                    if (arrOut.Count > 0)
                    {
                        Response.Redirect("LaporanOutstandingWO.aspx");
                    }
                }

                //Response.Redirect("LaporanOutstandingWO.aspx");            
                RB2.Checked = true;
                WorkOrder_New wor = new WorkOrder_New();
                WorkOrderFacade_New worf = new WorkOrderFacade_New();
                int StatusApv = worf.RetrieveUserLevel(user.ID);

                WorkOrder_New wor2 = new WorkOrder_New();
                WorkOrderFacade_New worf2 = new WorkOrderFacade_New();
                int Dept = worf.RetrieveUserDept(user.DeptID);

                //WorkOrder_New domainSR = new WorkOrder_New();
                //WorkOrderFacade_New facadeSR = new WorkOrderFacade_New();
                //int StatusReport = facadeSR.Retrieve_StatusReport(user.ID);           

                WorkOrder_New domainCorp = new WorkOrder_New();
                WorkOrderFacade_New facadeCorp = new WorkOrderFacade_New();
                string Corporate = facadeCorp.Retrieve_Corporate();

                Session["Corporate"] = Corporate;
                Session["StatusApv"] = StatusApv;
                Session["StatusReport"] = domainSR.StatusReport.ToString();

                // Untuk HRD PES - ISO
                if (domainSR.StatusReport == "5")
                {
                    if (user.DeptID == 2 || user.DeptID == 3 || user.DeptID == 6 || user.DeptID == 9 || user.DeptID == 10 || user.DeptID == 11)
                    {
                        RB1.Visible = false;
                    }
                    else
                    {
                        RB1.Visible = true;
                    }

                    RB2.Visible = true; RB1.Checked = false; RB2.Checked = false; RBK.Visible = true; RBK.Checked = false;
                    ddlDept.Visible = true; LabelDept.Visible = true; RBPO.Visible = true; RB2.Checked = true;
                }

                // Untuk Manager Penerima WO
                else if (domainSR.StatusReport == "0" && StatusApv == 3 || domainSR.StatusReport == "0" && StatusApv == 9)
                {
                    RBK.Checked = false; RBK.Visible = true; RB1.Visible = true; RB2.Visible = true; RBPO.Visible = true;
                    ddlDept.Visible = false; LabelDept.Visible = false;
                }
                // Untuk User Pembuat WO
                else if (domainSR.StatusReport == "0" && StatusApv == 0)
                {
                    RBK.Checked = true; RBK.Visible = true; RB1.Visible = true; RB2.Visible = false; RBPO.Visible = false;
                    ddlDept.Visible = false; LabelDept.Visible = false; RBLewat.Visible = false;
                }
                else if (domainSR.StatusReport == "" && StatusApv == 0)
                {
                    RBK.Checked = true; RBK.Visible = true; RB1.Visible = false; RB2.Visible = false; RBPO.Visible = false;
                    ddlDept.Visible = false; LabelDept.Visible = false; RBLewat.Visible = false;
                }

                LoadBulan();
                LoadTahun();
                //LoadCekCLoseWO();

                int LastDay = DateTime.DaysInMonth(int.Parse(ddlTahun.SelectedValue.ToString()), int.Parse(ddlBulan.SelectedValue.ToString()));
                txtTglMulai.Text = LastDay.ToString().PadLeft(2, '0') + "-" + ddlBulan.SelectedValue.PadLeft(2, '0') + "-" + ddlTahun.SelectedValue.ToString();
            }
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('thr', 450, 150 , 40 ,false); </script>", false);
        }

        private void LoadCekCLoseWO()
        {
            string TglSkr = DateTime.Now.ToString("yyyyMMdd");
            WorkOrder_New domainClose = new WorkOrder_New();
            WorkOrderFacade_New facadeClose = new WorkOrderFacade_New();
            ArrayList arrClose = new ArrayList();

            arrClose = facadeClose.RetrieveSerahTerimaWO();

            foreach (WorkOrder_New serah in arrClose)
            {
                int WoID = serah.WOID;
                int ToDept = serah.ToDept;
                string TglSelesai = serah.TglSelesaiWO;
                string AreaWo = serah.AreaWO;

                WorkOrder_New domainClose1 = new WorkOrder_New();
                WorkOrderFacade_New facadeClose1 = new WorkOrderFacade_New();
                int Selisih = facadeClose1.CekRange(TglSelesai, TglSkr);

                if (Selisih > 3)
                {
                    WorkOrder_New domainClose2 = new WorkOrder_New();
                    WorkOrderFacade_New facadeClose2 = new WorkOrderFacade_New();
                    domainClose2.WOID = WoID; domainClose2.ToDept = ToDept; domainClose2.AreaWO = AreaWo;

                    int intResult = 0;
                    intResult = facadeClose2.InsertClosedOto(domainClose2);
                }
            }
        }

        private void LoadBulan()
        {
            ddlBulan.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i).ToString(), i.ToString().PadLeft(2, '0')));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString().PadLeft(2, '0');
        }

        private void LoadTahun()
        {

            if (RBK.Checked == true)
            {
                ArrayList arrD = this.ListWOTahun();
                Session["arrD"] = arrD;
            }
            else
            {
                ArrayList arrD = this.ListWOTahun2();
                Session["arrD"] = arrD;
            }

            ArrayList arrD2 = new ArrayList();
            arrD2 = (ArrayList)Session["arrD"];

            ddlTahun.Items.Clear();
            foreach (WorkOrder_New wo in arrD2)
            {
                ddlTahun.Items.Add(new ListItem(wo.Tahun.ToString(), wo.Tahun.ToString()));
            }

            //ddlTahun.Items.Add(new ListItem(DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString()));
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }

        private void LoadDataMaster_WO(string FLag, int DeptID, int PlantID, string temp)
        {
            string query = string.Empty; string DeptID_MTC = string.Empty;

            /** Pencapaian WO Bulanan **/
            if (FLag == "3")
            {
                query =
                " where YEAR(DueDateWO)='" + ddlTahun.SelectedValue + "' and MONTH(DueDateWO)='" + ddlBulan.SelectedValue + "' " +
                " and DeptID_PenerimaWO='" + DeptID + "' ";
            }
            /** Pemantauan WO masuk status Open(apv mgr peminta) - Close **/
            else if (FLag == "2")
            {
                query =
                //" where YEAR(ApvMgrUser)='" + ddlTahun.SelectedValue + "' and MONTH(ApvMgrUser)='" + ddlBulan.SelectedValue + "' "+
                //" and DeptID_PenerimaWO='" + DeptID + "'  ";
                " where " +
                " (DueDateWO is null and YEAR(ApvMgrUser)='" + ddlTahun.SelectedValue + "' and MONTH(ApvMgrUser)='" + ddlBulan.SelectedValue + "' and DeptID_PenerimaWO='" + DeptID + "'  and PlantID='" + PlantID + "') " +
                " or " +
                " (DueDateWO is not null and YEAR(DueDateWO)='" + ddlTahun.SelectedValue + "' and MONTH(DueDateWO)='" + ddlBulan.SelectedValue + "' and DeptID_PenerimaWO='" + DeptID + "'  and PlantID='" + PlantID + "') order by NoWO,Target ";
            }
            /** Pencapaian WO Keluar **/
            else if (FLag == "4")
            {
                query =
                " where YEAR(CreatedTime)='" + ddlTahun.SelectedValue + "' and MONTH(CreatedTime)='" + ddlBulan.SelectedValue + "' " +
                " and DeptID_Users='" + DeptID + "'  ";
            }
            /** Pencapaian WO Masuk **/
            else if (FLag == "5" || FLag == "51")
            {
                query =
                " where YEAR(ApvMgrUser)='" + ddlTahun.SelectedValue + "' and MONTH(ApvMgrUser)='" + ddlBulan.SelectedValue + "' " +
                " and DeptID_PenerimaWO='" + DeptID + "'  ";
            }

            ZetroView lst = new ZetroView();
            lst.QueryType = Operation.CUSTOM;
            lst.CustomQuery =
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + temp.Trim() + "]') AND type in (N'U')) DROP TABLE [dbo].[" + temp.Trim() + "] " +

            " /** Break 7 **/ " +
            " select woID,NoWO,UraianPekerjaan,DeptID_Users,DeptID_PenerimaWO,Target,CreatedTime,ApvMgrUser ApvMgr,case when isnull(UpdateTargetTime,'1/1/1900' ) !='1/1/1900' and isnull(UpdatePelaksanaTime,'1/1/1900' ) ='1/1/1900' then UpdateTargetTime else  left(convert(char,UpdatePelaksanaTime,106),12) end UpdatePelaksanaTime,UpdateTargetTime Waktu2,DueDateWO,FinishDate FinishDate2,Pelaksana,CreatedBy,selisih_apv Selisih,case when SisaHari=0 then 0 else SisaHari end SisaHari,StatusWO,StatusApv,UsersWO,PenerimaWO,AreaWO SubArea2,AreaWO,PlantID,NamaSubDept,DateApvOP into " + temp.Trim() + " from ( " +
            " /** Break 6 **/ " +
            " select woID,StatusWO,StatusApv,NoWO,UraianPekerjaan,UsersWO,PenerimaWO,AreaWO,DeptID_Users,DeptID_PenerimaWO,Target, " +
            " left(convert(char,CreatedTime,106),12)CreatedTime,left(convert(char,ApvMgrUser,106),12)ApvMgrUser,UpdatePelaksanaTime,left(convert(char,UpdateTargetTime,106),12)UpdateTargetTime,left(convert(char,DueDateWO,106),12)DueDateWO,left(convert(char,FinishDate,106),12)FinishDate,Pelaksana,CreatedBy,ApvMgrUser waktu1,UpdateTargetTime waktu2,Apv,selisih_apv,case when selisih_pekerjaan<0 then '0 HK' else selisih_pekerjaan + ' HK' end selisih_pekerjaan,WaktuSelesai,WaktuDateLine,  case   " +
            " when Target=1 and   YEAR(WaktuSelesai)>1900   then 0     " +
            " when Target=1 and YEAR(WaktuSelesai)=1900 then selisih_pekerjaan     " +
            " when Target=1 and WaktuSelesai = '' then 0 " +
            " when (Target=1 or Target=2 or Target=3) and WaktuSelesai <> '' then 0 " +
            " when (Target=1 or Target=2 or Target=3) and WaktuSelesai = '' then selisih_pekerjaan  end SisaHari,PlantID,NamaSubDept,DateApvOP from ( " +
            " /** Break 5 **/ " +
            //" select *, convert(varchar,(select SUM(Selisih)Selisih from  (  select datediff(dd, Waktu1, Waktu2) - case when DATEPART(dw,Waktu2)=7  then 1 when DATEPART(dw,Waktu2)=1 then 1 else 0  end  - (datediff(wk, Waktu1, Waktu2) * 2) - " +
            //"  case when datepart(dw, Waktu1) = 1 then 1 else 0 end +  case when datepart(dw, Waktu2) = 1  then 1 else 0 end Selisih   " +

            //" union all  " +
            //" select COUNT(HariLibur)*-1 Selisih from CalenderOffDay where LEFT(convert(char,harilibur,112),8)>=Waktu1 and  LEFT(convert(char,harilibur,112),8)<=Waktu2) as selisih))+' '+'HK' selisih_apv, " +
            "select *,  convert(varchar, ( " +
            "select SUM(Selisih)Selisih from( " +
            "     select datediff(dd, UpdatePelaksanaTime, Waktu2) - " +
            "     case when DATEPART(dw, Waktu2) = 7  then 1 when DATEPART(dw, Waktu2) = 1 then 1 else 0  end - " +
            "           (datediff(wk, UpdatePelaksanaTime, Waktu2) * 2) - " +
            "     case when datepart(dw, UpdatePelaksanaTime) = 1 then 1 else 0 end + " +
            "     case when datepart(dw, Waktu2) = 1  then 1 else 0 end Selisih " +
            "     union all " +
            "     select COUNT(HariLibur)*-1 Selisih from CalenderOffDay where LEFT(convert(char, harilibur, 112), 8) >= UpdatePelaksanaTime and LEFT(convert(char, harilibur,112),8)<= Waktu2 " +
            ") as selisih " +
            " ))+' ' + 'HK' selisih_apv, " +
            " convert(varchar,(select SUM(Selisih)Selisih from  (  select datediff(dd, WaktuNow, DueDateWO) - case when DATEPART(dw,DueDateWO)=7  then 1 when DATEPART(dw,DueDateWO)=1 then 1 else 0  end  - " +
            "(datediff(wk, WaktuNow, DueDateWO) * 2) -   case when datepart(dw, WaktuNow) = 1 then 1 else 0 end +  case when datepart(dw, DueDateWO) = 1  then 1 else 0 end Selisih  " +
            " union all  " +
            " select COUNT(HariLibur)*-1 Selisih from CalenderOffDay where LEFT(convert(char,harilibur,112),8)>=WaktuNow and  LEFT(convert(char,harilibur,112),8)<=DueDateWO) as selisih)) selisih_pekerjaan, " +
            " case  " +
            " when DeptID_PenerimaWO=14 and Apv=1 and DeptID_Users not in (7,15,23,26,24,12,22,28,30,31) then 'Next: PM' " +
            " when Apv=0 and DeptID_PenerimaWO=7 and DeptID_Users=7 then 'Next: Head'+' - '+NamaSubDept   " +
            " when Apv=0 then 'Next: Mgr Dept'  " +
            " when Cancel = 1 then 'Cancel - T3'  " +
            " when (Target is null or Target =1 ) and FinishDate is not null and WaktuSelesai>WaktuDateLine then 'Lewat'  " +
            " when  Target =2  and FinishDate is not null and WaktuSelesai>WaktuDateLine then 'Lewat - T2'  " +
            " when  Target =3  and FinishDate is not null and WaktuSelesai>WaktuDateLine then 'Lewat - T3'  when FinishDate is not null and FinishDate<=DueDateWO and Apv=4 then 'Finish'   " +
            " when FinishDate is not null and WaktuSelesai<=WaktuDateLine and Apv=4 then 'Finish'  " +
            " when (Target is null or Target =1 ) and FinishDate is null and WaktuNow=WaktuDateLine  then 'Jatuh Tempo' " +
            " when Target =2 and FinishDate is null and WaktuNow=WaktuDateLine  then 'Jatuh Tempo T2'   " +
            " when Target =3 and FinishDate is null and WaktuNow=WaktuDateLine  then 'Jatuh Tempo T3'  " +
            " when FinishDate is null and DueDateWO is null and DeptID_PenerimaWO=7 and Pelaksana<>'' and DueDateWO is not null  then 'Next: Mgr HRD' " +
            " when FinishDate is null and DueDateWO is null and DeptID_PenerimaWO=14 and DeptID_Users in (15,23,26,24,12,22,28,30,31) and Apv=2 and VerISO is null and AreaWO like'%Software%'  then 'Next: Verifikasi ISO'  " +
            " when FinishDate is null and DueDateWO is null and DeptID_PenerimaWO=14 and DeptID_Users in (15,23,26,24,12,22,28,30,31) and Apv=2 and VerISO=1 and ApvOP=2 and AreaWO like'%Software%'  then 'Next: Mgr IT'   " +
            " when FinishDate is null and DueDateWO is null and DeptID_PenerimaWO=14 and DeptID_Users not in (15,23,26,24,12,22,28,30,31) and Apv=2 and VerISO=1 and ApvOP=2 and AreaWO like'%Software%'  then 'Next: Mgr IT'   " +
            " when FinishDate is null and DueDateWO is null and DeptID_PenerimaWO=14 and DeptID_Users not in (15,23,26,24,12,22,28,30,31) and Apv=2 and VerISO=1 and ApvOP is null and AreaWO like'%Software%'  then 'Next: Mgr Dept - Plant Terkait'   " +
            " when FinishDate is null and DueDateWO is null and DeptID_PenerimaWO=14 and DeptID_Users not in (15,23,26,24,12,22,28,30,31) and Apv=2 and VerISO is null and AreaWO like'%Software%' then 'Next: Verifikasi ISO'   " +
            " when Pelaksana is not null and DueDateWO is null   and DeptID_PenerimaWO=19  then 'Next:' + Pelaksana   " +
            " when Pelaksana is not null and DueDateWO is not null   and DeptID_PenerimaWO=19  and Apv=2  then 'Next: Mgr MTN'   " +
            " when Pelaksana = '' and DueDateWO is null   and DeptID_PenerimaWO=19  then 'Next: Mgr MTN'   " +
            " when Pelaksana = '' and DueDateWO is null   and DeptID_PenerimaWO=14  and Apv=2 and AreaWO='HardWare'  then 'Next: Mgr IT'   " +
            " when Pelaksana = '' and DueDateWO is null   and DeptID_PenerimaWO=7  and Apv=2 and VerSec=1  then 'Next: Mgr HRD'   " +
            " when Pelaksana = '' and DueDateWO is null   and DeptID_PenerimaWO=7  and Apv=2 and AreaWO<>'Kendaraan'  then 'Next: Mgr HRD'  " +
            " when Pelaksana <> '' and DueDateWO is not null   and DeptID_PenerimaWO=7  and Apv=2  then 'Next: Mgr HRD'  " +
            " when Pelaksana = 'SPV GA' and Apv=2 and DeptID_PenerimaWO=7 and DueDateWO is null then 'Next : Head GA'  " +
            " when Pelaksana = 'SPV GA' and Apv=2 and DeptID_PenerimaWO=7 and DueDateWO is not null then 'Next: Mgr HRD'   " +
            " when Target =3 and  FinishDate is not null and WaktuSelesai<=WaktuDateLine and Apv=5 then 'Tercapai T3'   " +
            " when Target =2 and  FinishDate is not null and WaktuSelesai<=WaktuDateLine and Apv=5 then 'Tercapai T2'  " +
            " when (Target is null or Target =1 ) and FinishDate <> '' and WaktuSelesai<=WaktuDateLine and Apv=5 then 'Tercapai' " +
            " when Target=3 and WaktuNow<WaktuDateLine then 'Progress - T3'  " +
            " when Target=2 and WaktuNow<WaktuDateLine then 'Progress - T2'   " +
            " when Target=1 and WaktuNow<WaktuDateLine then 'Progress - T1'  " +
            " when Target is null then 'Progress'   " +
            " when Target=3 and WaktuNow>WaktuDateLine then 'Lewat - T3'   " +
            " when Target=2 and WaktuNow>WaktuDateLine then 'Lewat - T2' " +
            " when Target=1 and WaktuNow>DueDateWO then 'Lewat - T1'   " +
            " when Target is null then 'Lewat'  end StatusWO,   " +
            " case  when apv=0 then 'Open'  " +
            " when Apv=2 and DeptID_PenerimaWO=19 and Pelaksana='' then 'Apv Mgr'   " +
            " when Apv=2 and ((Pelaksana is not null or Pelaksana <> '') and (DueDateWO is null or DueDateWO='')) and DeptID_PenerimaWO=19 then 'Apv Mgr MTN-1'   " +
            " when Apv=2 and ((Pelaksana is not null or Pelaksana <> '') and (DueDateWO is not null or DueDateWO<>'')) and DeptID_PenerimaWO=19 then 'Apv Head'+'-'+Pelaksana   " +
            " when Apv=2 and DeptID_PenerimaWO=7 and DeptID_Users<>7 and Pelaksana='SPV GA' and DueDateWO is null then 'Apv Mgr'    " +
            " when Apv=2 and DeptID_PenerimaWO=7 and Pelaksana='SPV GA' and DueDateWO is not null then 'Apv Head GA'   " +
            " when Apv=2 and DeptID_PenerimaWO=7 and DeptID_Users=7 then 'Apv Head '+NamaSubDept   " +
            " when Apv=2 then 'Apv Mgr'  " +
            " when apv=3 and DeptID_PenerimaWO=19 then 'Apv Mgr MTN'   " +
            " when apv=3 and DeptID_PenerimaWO=7 then 'Apv Mgr HRD'    " +
            " when apv=3 and DeptID_PenerimaWO=14 then 'Apv Mgr IT'   " +
            " when apv=4 then 'Finish' " +
            " when apv=5 and waktu0>=0 then 'Closed'  " +
            " when apv=5 and waktu0<0 then 'Lewat' " +
            " end StatusApv " +
            " from ( " +
            " /** Break 4 **/ " +
            " select woID,NoWO,UraianPekerjaan,DeptID_Users,DeptID_PenerimaWO,Target,CreatedTime,ApvMgrUser,UpdatePelaksanaTime,UpdateTargetTime,DueDateWO,/**case when waktu0<0 then null when waktu0 is null then null else FinishDate end **/FinishDate,Pelaksana,CreatedBy,ApvMgrUser waktu1,UpdateTargetTime waktu2,Apv,NamaSubDept,VerISO,WaktuSelesai,WaktuDateLine,AreaWO,ApvOP,VerSec,Cancel,WaktuNow,UsersWO,PenerimaWO,PlantID,waktu0 " +
            " , case when StatusTarget is null " +
            " then case when AreaWO='Hardware' then isnull(LEFT(convert(char,ApvMgrUser,106),12),'') when AreaWO='SoftWare' then  isnull(LEFT(convert(char,DateApvOP,106),12),'') end " +
            " when StatusTarget=2 then (select isnull(LEFT(convert(char,b1.CreatedTime,106),12),'') from WorkOrder_Target b1 where b1.WoID=x2.woID and b1.RowStatus>-1 and b1.Target=2) " +
            " when StatusTarget=3 then (select isnull(LEFT(convert(char,b2.CreatedTime,106),12),'') from WorkOrder_Target b2 where b2.WoID=x2.woID and b2.RowStatus>-1 and b2.Target=3) " +
            " end DateApvOP from ( " +
            " /** Break 3 **/ " +
            " select *, " +
            " case  " +
            " when (Target=1 and DeptID_PenerimaWO<>14)  or (DeptID_PenerimaWO=14 and AreaWO='HardWare') then (select top 1 a1.CreatedTime from WorkOrder_LogApproval a1 where a1.WOID=x1.woID  and a1.RowStatus>-1 and a1.Urutan=1)  " +
            " when (Target=2 and DeptID_PenerimaWO<>14)  or (DeptID_PenerimaWO=14 and AreaWO='HardWare') then (select top 1 a2.CreatedTime from WorkOrder_Target a2 where a2.WoID=x1.woID and a2.RowStatus>-1 and a2.Target=2) " +
            " when (Target=3 and DeptID_PenerimaWO<>14)  or (DeptID_PenerimaWO=14 and AreaWO='HardWare') then (select top 1 a2.CreatedTime from WorkOrder_Target a2 where a2.WoID=x1.woID and a2.RowStatus>-1 and a2.Target=3) " +
            /** Tambahan **/
            " when (Target=2 and DeptID_PenerimaWO=14 and AreaWO='SoftWare') then (select top 1 a2.CreatedTime from WorkOrder_Target a2 where a2.WoID=x1.woID and a2.RowStatus>-1 and a2.Target=2) " +
            " when (Target=3 and DeptID_PenerimaWO=14 and AreaWO='SoftWare') then (select top 1 a2.CreatedTime from WorkOrder_Target a2 where a2.WoID=x1.woID and a2.RowStatus>-1 and a2.Target=3)  " +
            /** Tambahan **/
            " else DateApvOP end ApvMgrUser, " +
            " case  " +
            " when (target=1 and DeptID_PenerimaWO<>14)  or (DeptID_PenerimaWO=14 and AreaWO='HardWare') then (select top 1 aa.UpdateTargetTime from WorkOrder aa where aa.ID=x1.woID and aa.RowStatus>-1)  " +
            " when (target=2 and DeptID_PenerimaWO<>14)  or (DeptID_PenerimaWO=14 and AreaWO='HardWare') then (select top 1 aa.CreatedTime from WorkOrder_Target aa where aa.WoID=x1.woID and aa.RowStatus>-1 and aa.Target=2)  " +
            " when (target=3 and DeptID_PenerimaWO<>14)  or (DeptID_PenerimaWO=14 and AreaWO='HardWare') then (select top 1 aa.CreatedTime from WorkOrder_Target aa where aa.WoID=x1.woID and aa.RowStatus>-1 and aa.Target=3)  " +
            /** Tambahan **/
            " when (target=2 and DeptID_PenerimaWO=14 and AreaWO='SoftWare') then (select top 1 aa.CreatedTime from WorkOrder_Target aa where aa.WoID=x1.woID and aa.RowStatus>-1 and aa.Target=2) " +
            " when (target=3 and DeptID_PenerimaWO=14 and AreaWO='SoftWare') then (select top 1 aa.CreatedTime from WorkOrder_Target aa where aa.WoID=x1.woID and aa.RowStatus>-1 and aa.Target=3) " +
            /** Tambahan **/
            " else UpdateTargetTime0 end UpdateTargetTime, " +
            " DATEDIFF(DAY,LEFT(CONVERT(CHAR,FinishDate,112),8),DueDateWO)waktu0,LEFT(convert(char,DueDateWO,112),8) WaktuDateLine from ( " +
            " /** Break 2 **/ " +
            " select woID,NoWO,UraianPekerjaan,DeptID_Users,DeptID_PenerimaWO,StatusTarget, " +
            " case when Target is null then '1' else Target end Target,case when TglTarget is null then DueDateWO_awal else TglTarget end DueDateWO,FinishDate, " +
            " case when Target is null then CreatedTime when Target=1 then CreatedTime else CreatedTime2 end CreatedTime,UpdatePelaksanaTime,UpdateTargetTime0,Pelaksana,CreatedBy,Apv,VerISO,NamaSubDept,WaktuSelesai,AreaWO,ApvOP,VerSec,Cancel,WaktuNow,UsersWO,PenerimaWO,PlantID,DateApvOP from ( " +
            " /** Break 1 **/ " +
            " select A.ID woID,A.NoWO,A.UraianPekerjaan,A.DeptID_Users,A.DeptID_PenerimaWO,A.StatusTarget,A.DueDateWO DueDateWO_awal,D.TglTarget,D.Target,A.FinishDate,A.CreatedTime,D.CreatedTime CreatedTime2,A.UpdatePelaksanaTime,A.UpdateTargetTime UpdateTargetTime0,Pelaksana,A.CreatedBy,A.Apv,A.VerISO,A.NamaSubDept,isnull(LEFT(convert(char,FinishDate,112),8),'')WaktuSelesai,case when A.SubArea ='' or A.SubArea is null then TRIM(A.AreaWO) else TRIM(A.AreaWO) +' - ' + TRIM(A.SubArea) end AreaWO,A.ApvOP,A.VerSec,A.Cancel,GETDATE()WaktuNow,C.Alias UsersWO,C1.Alias PenerimaWO,A.PlantID " +
            " ,case when A.ApvOP=2 and A.DeptID_Users in (/**7,**/23,24,23,26,12,13) then (select top 1 a1.CreatedTime from WorkOrder_LogApproval a1 where a1.WOID=A.ID and a1.RowStatus>-1 and a1.Urutan=3) else A.DateApvOP end DateApvOP " +
            " from WorkOrder as A  " +
            " LEFT JOIN Dept as C ON A.DeptID_Users=C.ID  " +
            " LEFT JOIN Dept as C1 ON A.DeptID_PenerimaWO=C1.ID  " +
            " LEFT JOIN WorkOrder_Target D ON D.WoID=A.ID  where A.RowStatus>-1" +
            " /** end Break 1 **/ " +
            " ) as x    " +
            " /** end Break 2 **/ " +
            " ) as x1 " +
            " /** end Break 3 **/ " +
            " ) as x2  " +
            " /** end Break 4 **/ " +
            " ) as x3  " +
            " /** end Break 5 **/ " +
            " ) as x4  " +
            " /** end Break 6 **/ " +
            " ) as x5 " +
            //" where YEAR(DueDateWO)='" + Tahun + "' and MONTH(DueDateWO)='" + Periode + "' and UsersWO='" + UsersWO + "' and PenerimaWO='" + PenerimaWO + "'  order by NoWO,Target " +
            "" + query + "" +
            " /** end Break 7 **/ ";
            SqlDataReader lst2 = lst.Retrieve();

        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"]; string temp = string.Empty;
            if (users.DeptID == 14)
            {
                temp = " temp_dataWOIT ";
            }
            else if (users.DeptID == 23)
            {
                temp = " temp_dataWOISO ";
            }
            else if (users.DeptID == 19 || users.DeptID == 4 || users.DeptID == 5 || users.DeptID == 18)
            {
                temp = " temp_dataWOMTC ";
            }
            else if (users.DeptID == 7)
            {
                temp = " temp_dataWOHRD ";
            }
            else
            {
                temp = " temp_dataWOext ";
            }

            ZetroView zSave = new ZetroView();
            zSave.QueryType = Operation.CUSTOM;
            zSave.CustomQuery =
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + temp.Trim() + "]') AND type in (N'U')) DROP TABLE [dbo].[" + temp.Trim() + "] ";
            SqlDataReader sdSave = zSave.Retrieve();

            //Users users = (Users)Session["Users"];        
            int StatusReport = Convert.ToInt32(Session["StatusReport"]);
            string StatusApv1 = Session["StatusApv"].ToString();
            string Corporate = Session["Corporate"].ToString();

            WorkOrder_New dom1 = new WorkOrder_New();
            WorkOrderFacade_New fac1 = new WorkOrderFacade_New();
            int stsUser = fac1.RetrieveStsUser(users.ID);
            int FlagUser = (stsUser > 0) ? stsUser : 0; Session["FlagUser"] = FlagUser;

            if (StatusReport == 5 && ddlDept.SelectedValue == "0" && RBK.Checked == false)
            {
                DisplayAJAXMessage(this, " Dept. Belum Dipilih !! ");
                return;
            }

            string NamaSub = fac1.RetrieveNamaSub(users.ID);

            ArrayList arrDept = new ArrayList();
            WorkOrder_New woN = new WorkOrder_New();
            WorkOrderFacade_New wofN = new WorkOrderFacade_New();

            woN.Periode = ddlBulan.SelectedValue;
            woN.Tahun = Convert.ToInt32(ddlTahun.SelectedValue);

            if (StatusApv1 != "0")
            {
                WorkOrder_New woN1 = new WorkOrder_New();
                WorkOrderFacade_New wofN1 = new WorkOrderFacade_New();
                int DeptIDUser = wofN1.Retrieve_Dept(users.ID);
                Session["DeptIDUser"] = DeptIDUser;
            }
            else
            {
                int DeptIDUser = users.DeptID;
                Session["DeptIDUser"] = DeptIDUser;
            }

            // Untuk Department ISO dan HRD PES : Bisa lihat WO Masuk Dept. HRD - IT - MTN
            if (StatusReport == 5)
            {
                // WO Masuk
                if (RB1.Checked == true)
                {
                    if (ddlDept.SelectedValue != "0" && StatusReport == 5)
                    { int PilihDept = Convert.ToInt32(ddlDept.SelectedValue); Session["PilihDept"] = PilihDept; }
                    string Flag = "5"; Session["Flag"] = Flag;
                }
                // WO Pencapaian WO
                else if (RB2.Checked == true)
                {
                    if (ddlDept.SelectedValue != "0" && StatusReport == 5)
                    { int PilihDept = Convert.ToInt32(ddlDept.SelectedValue); Session["PilihDept"] = PilihDept; }
                    string Flag = "3"; Session["Flag"] = Flag;
                }
                // WO Keluar
                else if (RBK.Checked == true)
                {
                    string Flag = "4"; Session["Flag"] = Flag;
                    int PilihDept = users.DeptID; Session["PilihDept"] = PilihDept;
                }
                // WO Pemantauan 
                else if (RBPO.Checked == true)
                {
                    if (ddlDept.SelectedValue != "0" && StatusReport == 5)
                    { int PilihDept = Convert.ToInt32(ddlDept.SelectedValue); Session["PilihDept"] = PilihDept; }
                    string Flag = "2"; Session["Flag"] = Flag;
                }
            }

            // Untuk Department diluar HRD PES - ISO
            else if (StatusReport == 0)
            {
                if (RB1.Checked == true)
                {
                    string Flag = "51"; Session["Flag"] = Flag;
                    int PilihDept = (users.DeptID == 4) ? 19 : users.DeptID;
                    Session["PilihDept"] = PilihDept;
                }
                else if (RBK.Checked == true && FlagUser == 0)
                {
                    string Flag = "4"; Session["Flag"] = Flag;
                    int PilihDept = (users.DeptID == 4) ? 19 : users.DeptID;
                    Session["PilihDept"] = PilihDept;
                }
                else if (RBK.Checked == true && FlagUser > 0)
                {
                    string Flag = "41"; Session["Flag"] = Flag;
                    int PilihDept = (users.DeptID == 4) ? 19 : users.DeptID;
                    Session["PilihDept"] = PilihDept;
                }
                else if (RB2.Checked == true)
                {
                    string Flag = "3"; Session["Flag"] = Flag;
                    int PilihDept = (users.DeptID == 4) ? 19 : users.DeptID;
                    Session["PilihDept"] = PilihDept;
                }
                else if (RBPO.Checked == true)
                {
                    string Flag = "2"; Session["Flag"] = Flag;
                    int PilihDept = (users.DeptID == 4) ? 19 : users.DeptID;
                    Session["PilihDept"] = PilihDept;
                }
            }

            int DeptID = Convert.ToInt32(Session["PilihDept"]);
            string Flag1 = Session["Flag"].ToString();

            if (DeptID == 4 || DeptID == 5 || DeptID == 18)
            {
                DeptID = 19;
            }

            /** added by Beny 29 Oktober 2021, revisi-an **/
            LoadDataMaster_WO(Flag1, DeptID, users.UnitKerjaID, temp);

            // Pemantauan WO : Open Closed
            if (Flag1 == "2")
            {
                arrDept = wofN.RetrieveNamaDept_PemantauanWO(StatusApv1, DeptID, woN.Tahun, woN.Periode, Flag1, users.UnitKerjaID, temp);
            }
            // Pencapaian WO : Per Bulan
            else if (Flag1 == "3")
            {
                arrDept = wofN.RetrieveNamaDept_PencapaianWO(StatusApv1, DeptID, woN.Tahun, woN.Periode, Flag1, users.UnitKerjaID, temp);
            }

            //WO Keluar
            else if (Flag1 == "4")
            {
                if (users.DeptID == 23)
                {
                    FlagUser = Convert.ToInt32(ddlDept.SelectedValue);
                }

                arrDept = wofN.RetrieveNamaDept_Keluar(StatusApv1, DeptID, woN.Tahun, woN.Periode, Flag1, users.UnitKerjaID, FlagUser, NamaSub, temp);
            }
            //WO Keluar
            else if (Flag1 == "41")
            {
                arrDept = wofN.RetrieveNamaDept_Keluar(StatusApv1, DeptID, woN.Tahun, woN.Periode, Flag1, users.UnitKerjaID, FlagUser, NamaSub, temp);
            }
            //WO Masuk
            if (Flag1 == "5" || Flag1 == "51")
            {
                arrDept = wofN.RetrieveNamaDept_Masuk(StatusApv1, DeptID, woN.Tahun, woN.Periode, Flag1, StatusReport, Corporate, users.UnitKerjaID, temp);

                txtPeriod.Text = string.Empty;
                txtTotal.Text = string.Empty;
                txtTarget.Text = string.Empty;
                TxtPersen.Text = string.Empty;
            }
            //if (Flag1 == "1")
            //{
            //    txtPeriod.Text = string.Empty;
            //    txtTotal.Text = string.Empty;
            //    txtTarget.Text = string.Empty;
            //    TxtPersen.Text = string.Empty;
            //}        
            string DeptName = string.Empty;
            lstDept.DataSource = arrDept;
            lstDept.DataBind();
        }

        protected int getDeptID(string ID)
        {
            int result = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select id from spd_dept where dptid =" + ID;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Int32.Parse(sdr["ID"].ToString());
                }
            }
            return result;
        }
        protected int getDeptID_P(string deptname)
        {
            int result = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            //zl.CustomQuery = "select id from dept where deptname='" + deptname + "'";
            zl.CustomQuery = "select top 1 id from dept where alias='" + deptname + "'";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Int32.Parse(sdr["ID"].ToString());
                }
            }
            return result;
        }
        protected void lstDept_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Users users = (Users)Session["Users"]; string temp = string.Empty;
            if (users.DeptID == 14)
            {
                temp = " temp_dataWOIT ";
            }
            else if (users.DeptID == 23)
            {
                temp = " temp_dataWOISO ";
            }
            else if (users.DeptID == 19 || users.DeptID == 4 || users.DeptID == 5 || users.DeptID == 18)
            {
                temp = " temp_dataWOMTC ";
            }
            else if (users.DeptID == 7)
            {
                temp = " temp_dataWOHRD ";
            }
            else
            {
                temp = " temp_dataWOext ";
            }

            int StatusReport = Convert.ToInt32(Session["StatusReport"]);
            string Tanda2 = Session["Flag"].ToString();
            int UserID1 = Convert.ToInt32(Session["DeptIDUser"]);
            //Users users = (Users)Session["Users"];
            int StatusApv = Convert.ToInt32(Session["StatusApv"]);
            ArrayList arrData5 = new ArrayList();
            WorkOrder_New pm2 = (WorkOrder_New)e.Item.DataItem;
            Repeater lstSOP = (Repeater)e.Item.FindControl("lstBA");
            string WaktuSkr = DateTime.Now.ToString("yyyyMMdd");
            WorkOrderFacade_New mp = new WorkOrderFacade_New();
            Repeater lstP = (Repeater)sender;

            WorkOrder_New pm5 = (WorkOrder_New)e.Item.DataItem;
            WorkOrderFacade_New mp5 = new WorkOrderFacade_New();
            string Thn1 = DateTime.Now.Year.ToString();
            int Bln1 = DateTime.Now.Month;



            //Session["PilihDept"]
            if (StatusReport == 5)
            {
                if (Tanda2 == "4")
                {
                    if (users.DeptID == 23)
                    {
                        string DPenerimaWOTemp = ddlDept.SelectedItem.ToString().Trim(); Session["DPenerimaWOTemp"] = DPenerimaWOTemp;
                        string DUsersWOTemp = pm2.DeptName.Trim(); Session["DUsersWOTemp"] = DUsersWOTemp;
                    }
                    else
                    {
                        string DPenerimaWOTemp = pm2.DeptName.Trim(); Session["DPenerimaWOTemp"] = DPenerimaWOTemp;
                        string DUsersWOTemp = mp5.RetrieveDeptName(users.DeptID.ToString()); Session["DUsersWOTemp"] = DUsersWOTemp;
                    }
                }
                else
                {
                    string DPenerimaWOTemp = mp5.RetrieveDeptName(Convert.ToInt32(Session["PilihDept"]).ToString()); Session["DPenerimaWOTemp"] = DPenerimaWOTemp;
                    string DUsersWOTemp = pm2.DeptName.Trim(); Session["DUsersWOTemp"] = DUsersWOTemp;
                }
            }

            else if (StatusReport == 0)
            {
                // Pemantauan WO
                if (Tanda2 == "2")
                {
                    string DPenerimaWOTemp = mp5.RetrieveDeptName(users.DeptID.ToString()); Session["DPenerimaWOTemp"] = DPenerimaWOTemp;
                    string DUsersWOTemp = pm2.DeptName.Trim(); Session["DUsersWOTemp"] = DUsersWOTemp;
                }
                // Pencapaian WO
                else if (Tanda2 == "3")
                {
                    string DPenerimaWOTemp = mp5.RetrieveDeptName(users.DeptID.ToString()); Session["DPenerimaWOTemp"] = DPenerimaWOTemp;
                    string DUsersWOTemp = pm2.DeptName.Trim(); Session["DUsersWOTemp"] = DUsersWOTemp;
                }
                // WO Keluar
                else if (Tanda2 == "4")
                {
                    string DPenerimaWOTemp = pm2.DeptName.Trim(); Session["DPenerimaWOTemp"] = DPenerimaWOTemp;
                    string DUsersWOTemp = mp5.RetrieveDeptName(users.DeptID.ToString()); Session["DUsersWOTemp"] = DUsersWOTemp;
                }
                // WO Keluar
                else if (Tanda2 == "41")
                {
                    string DPenerimaWOTemp = pm2.DeptName.Trim(); Session["DPenerimaWOTemp"] = DPenerimaWOTemp;
                    string DUsersWOTemp = mp5.RetrieveDeptNameSub(users.ID.ToString()); Session["DUsersWOTemp"] = DUsersWOTemp;
                }
                // WO Masuk
                else if (Tanda2 == "5")
                {
                    string DPenerimaWOTemp = pm2.DeptName.Trim(); Session["DPenerimaWOTemp"] = DPenerimaWOTemp;
                    string DUsersWOTemp = mp5.RetrieveDeptName(users.DeptID.ToString()); Session["DUsersWOTemp"] = DUsersWOTemp;
                }
                // WO Masuk
                else if (Tanda2 == "51")
                {
                    string DPenerimaWOTemp = mp5.RetrieveDeptName(users.DeptID.ToString()); Session["DPenerimaWOTemp"] = DPenerimaWOTemp;
                    string DUsersWOTemp = pm2.DeptName.Trim(); Session["DUsersWOTemp"] = DUsersWOTemp;
                }
            }
            string PenerimaWO = Session["DPenerimaWOTemp"].ToString().Trim(); string UsersWO = Session["DUsersWOTemp"].ToString();


            //int FlagUsers = Convert.ToInt32(Session["FlagUser"]);
            //WorkOrder_New pm55 = new WorkOrder_New();
            //WorkOrderFacade_New mp55 = new WorkOrderFacade_New();
            //string NamaSub = mp55.RetrieveNamaSub(users.ID);
            //string NamaSubs = string.Empty;

            //if (users.DeptID == 7 && FlagUsers > 0)
            //{
            //    NamaSubs = NamaSub; Session["NamaSubs"] = NamaSubs;
            //}
            //else if (users.DeptID == 7 && FlagUsers == 0)
            //{
            //    NamaSubs = "Ext"; Session["NamaSubs"] = NamaSubs;
            //}
            //else
            //{
            //    NamaSubs = ""; Session["NamaSubs"] = NamaSubs;
            //}

            if (UsersWO.Contains("HRD -"))
            {
                string NamaSub = string.Empty;
                UsersWO = "HRD & GA";
                //NamaSub = pm2.DeptName.Trim(); Session["NamaSub"] = NamaSub;
                NamaSub = Session["DUsersWOTemp"].ToString().Trim(); Session["NamaSub"] = NamaSub;
            }
            else
            {
                string NamaSub = string.Empty;
                NamaSub = ""; Session["NamaSub"] = NamaSub;
            }

            string NamaSubDept = Session["NamaSub"].ToString().Trim();
            arrData5 = mp.RetrieveListWO(NamaSubDept.Trim(), PenerimaWO.Trim(), UsersWO.Trim(), ddlBulan.SelectedValue, Convert.ToInt32(ddlTahun.SelectedValue), Tanda2, WaktuSkr, users.UnitKerjaID, temp);

            WorkOrder_New wo1 = new WorkOrder_New();
            WorkOrderFacade_New woF1 = new WorkOrderFacade_New();
            int TotalWO = woF1.RetrieveTotalWO(ddlBulan.SelectedValue.ToString(), ddlTahun.SelectedValue.ToString(), UserID1, Tanda2, ddlDept.SelectedValue, StatusReport, temp);
            decimal TotalWO2 = Convert.ToDecimal(TotalWO);
            txtPeriod.Text = ddlBulan.SelectedItem.ToString();

            WorkOrder_New wo3 = new WorkOrder_New();
            WorkOrderFacade_New woF3 = new WorkOrderFacade_New();
            wo3 = woF3.RetrieveTtlWO_Break(ddlBulan.SelectedValue.ToString(), ddlTahun.SelectedValue.ToString(), UserID1, Tanda2, ddlDept.SelectedValue, StatusReport, temp);

            WorkOrder_New wo04 = new WorkOrder_New();
            WorkOrderFacade_New woF04 = new WorkOrderFacade_New();
            wo04 = woF04.RetrieveTotalWO_PerHead(ddlBulan.SelectedValue.ToString(), ddlTahun.SelectedValue.ToString(), UserID1, Tanda2, ddlDept.SelectedValue, StatusReport, temp);

            string Tahun = ddlTahun.SelectedItem.ToString();
            int Bul = Convert.ToInt32(ddlBulan.SelectedValue);
            if (Bul < 10)
            {
                string Bulan = '0' + Bul.ToString(); Session["Bulan"] = Bulan;
            }
            else
            {
                string Bulan = Bul.ToString(); Session["Bulan"] = Bulan;
            }
            string BulanR = Session["Bulan"].ToString();
            string RangePeriode = Tahun + BulanR;

            if (Tanda2 == "3" && TotalWO > 0)
            {
                WorkOrder_New wo2 = new WorkOrder_New();
                WorkOrderFacade_New woF2 = new WorkOrderFacade_New();
                wo2 = woF1.RetrieveStatusWO(PenerimaWO, UsersWO, ddlBulan.SelectedValue, Convert.ToInt32(ddlTahun.SelectedValue), Tanda2, WaktuSkr, RangePeriode, temp);

                WorkOrder_New wo4 = new WorkOrder_New();
                WorkOrderFacade_New woF4 = new WorkOrderFacade_New();
                wo4 = woF4.RetrieveStatusWO_break(PenerimaWO, UsersWO, ddlBulan.SelectedValue, Convert.ToInt32(ddlTahun.SelectedValue), Tanda2, WaktuSkr, RangePeriode, temp);

                WorkOrder_New wo5 = new WorkOrder_New();
                WorkOrderFacade_New woF5 = new WorkOrderFacade_New();
                wo5 = woF5.RetrieveStatusWO_breakPersen(PenerimaWO, UsersWO, ddlBulan.SelectedValue, Convert.ToInt32(ddlTahun.SelectedValue), Tanda2, WaktuSkr, RangePeriode, temp);

                decimal Total = Convert.ToDecimal(wo2.Total);
                txtTotal.Text = TotalWO.ToString();
                Label01.Text = "( " + wo3.Ket + " )"; //Label01.Visible = true;
                Label02.Text = "( " + wo4.Ket + " )"; //Label02.Visible = true;
                Label03.Text = "( " + wo5.Ket + " )"; //Label03.Visible = true;

                txtTarget.Text = wo2.Total.ToString();
                decimal Persen = Math.Round((Total / TotalWO2), 2, MidpointRounding.AwayFromZero) * 100;
                TxtPersen.Text = Persen.ToString() + " " + "%";

                if (users.DeptID == 2)
                {
                    LabelTotal.Visible = false; LabelTotalNilai.Visible = false;
                    LabelTarget.Visible = false; LabelTargetNilai.Visible = false;
                    LabelPersen.Visible = false; LabelPersenNilai.Visible = false;
                    Label01.Visible = false; Label02.Visible = false; Label03.Visible = false;
                }
                else
                {
                    LabelTotal.Visible = true; LabelTotalNilai.Visible = true;
                    LabelTarget.Visible = true; LabelTargetNilai.Visible = true;
                    LabelPersen.Visible = true; LabelPersenNilai.Visible = true;
                    Label01.Visible = true; Label02.Visible = true; Label03.Visible = true;
                }
                //LabelTotal.Visible = true; LabelTotalNilai.Visible = true;
                LabelTotal.Text = "Total WO";
                LabelTotalNilai.Text = ":" + " " + TotalWO.ToString();

                //LabelTarget.Visible = true; LabelTargetNilai.Visible = true;
                LabelTarget.Text = "Tercapai";
                LabelTargetNilai.Text = ":" + " " + wo2.Total.ToString();

                //LabelPersen.Visible = true; LabelPersenNilai.Visible = true;
                LabelPersen.Text = "Persentase";
                LabelPersenNilai.Text = ":" + " " + Persen.ToString() + " " + "%";

                //update sarmut
                decimal aktual = Persen;
                string sarmutPrs = string.Empty; int deptID2 = 0;
                int deptID_P = getDeptID_P(Session["DPenerimaWOTemp"].ToString().Trim());

                /** Revisi **/
                if (Session["DPenerimaWOTemp"].ToString().Trim() == "MAINTENANCE")
                {

                    deptID2 = 19;
                }
                else if (Session["DPenerimaWOTemp"].ToString().Trim() == "HRD & GA")
                {
                    deptID2 = 7;
                }
                else
                {
                    deptID2 = deptID_P;
                }


                //if (Session["DeptIDUser"].ToString().Trim() == deptID_P.ToString())
                if (Session["DeptIDUser"].ToString().Trim() == "19")
                    sarmutPrs = "Pencapaian Work Order Maintenance";
                else
                    sarmutPrs = "Pencapaian Work Order";

                /** End Revisi **/

                /** int deptid = getDeptID(deptID_P.ToString()); **/
                int deptid = getDeptID(deptID2.ToString());
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery =
                    "update SPD_TransPrs set actual= " + aktual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddlTahun.SelectedValue +
                    " and Bulan=" + ddlBulan.SelectedValue +
                    " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "' and deptid=" + deptid + " ) ";
                SqlDataReader sdr1 = zl1.Retrieve();
                //end update sarmut
                try
                {
                    #region Yoga SarmutToPES
                    if (deptID2 == 7)
                    {
                        ArrayList arrData = new ArrayList();
                        ZetroView zs = new ZetroView();
                        zs.QueryType = Operation.CUSTOM;
                        decimal targetSarmutHrd = 0;
                        int bulan = Convert.ToInt32(ddlBulan.SelectedValue) - 1;
                        zs.CustomQuery = "SELECT * FROM SPD_TransPrs WHERE SarmutPID IN (SELECT ID from SPD_Perusahaan where Rowstatus>-1 and " +
                                         "SarMutPerusahaan='" + sarmutPrs + "' and deptid=" + deptid + ") AND Approval>1 and Target>0 order by id desc ";
                        SqlDataReader adr = zs.Retrieve();
                        if (adr.HasRows)
                        {
                            while (adr.Read())
                            {
                                targetSarmutHrd = (adr["Target"] == DBNull.Value) ? 0 : Convert.ToDecimal(adr["Target"]);
                            }
                        }
                        if (Convert.ToDecimal(aktual) != 0)
                        {
                            string strError = "";
                            int valueActual = 0;

                            string ketPes = (aktual == 100) ? "100%" : (aktual >= 80 && aktual <= 99) ? "80 - 99 %" : (aktual >= 60 && aktual <= 79) ? "60 - 79 %" : (aktual >= 40 && aktual <= 59) ? "40 - 59 %" : "< 40%";

                            double percentActual = Convert.ToDouble(valueActual);
                            ZetroView zv = new ZetroView();
                            zv.QueryType = Operation.CUSTOM;
                            int IdKPI = 0;
                            string kpiName = string.Empty;
                            zv.CustomQuery = "SELECT * FROM ISO_KPI WHERE CategoryID in (select ID from ISO_UserCategory where Sarmut='" + sarmutPrs + "') and DeptID=7 and month(TglMulai)=" + ddlBulan.SelectedValue + " AND year(TglMulai)=" + ddlTahun.SelectedValue + " ORDER BY ID desc";
                            SqlDataReader dr = zv.Retrieve();
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    IdKPI = Int32.Parse(dr["ID"].ToString());
                                    kpiName = dr["KPIName"].ToString();
                                }
                            }
                            if (kpiName == string.Empty)
                            {
                                int LastDay = DateTime.DaysInMonth(int.Parse(ddlTahun.SelectedValue.ToString()), int.Parse(ddlBulan.SelectedValue.ToString()));
                                ArrayList arrSarPes = new ArrayList();
                                SarmutPESFacade sarPesFacade = new SarmutPESFacade();
                                arrSarPes = sarPesFacade.RetrieveUserCategory(sarmutPrs);
                                foreach (SarmutPes uc in arrSarPes)
                                {
                                    int idUserCategory = uc.IDUserCategory;
                                    int userID = uc.UserID;
                                    int bagianID = uc.BagianID;
                                    decimal bobotNilai = uc.BobotNilai;
                                    string pic = uc.Pic;
                                    int deptID = uc.DeptID;
                                    string description = uc.Description;
                                    int pesType = uc.PesType;
                                    int categoryID = uc.CategoryID;
                                    //uc.Actual = string.Concat(actual.ToString(), valueActual.ToString()) ;
                                    uc.Ket = Math.Round(aktual) + "%";
                                    //uc.Percent = valueActual.ToString();
                                    //uc.TglMulai = Convert.ToDateTime(txtTglMulai.Text);
                                    int pjgDept = ((Users)Session["Users"]).DeptID;
                                    string DdlDept = "HRD & GA";
                                    uc.DeptName = (pjgDept >= 4) ? DdlDept.Substring(0, 3) : DdlDept.Substring(0, pjgDept);
                                    txtTglMulai.Text = Convert.ToDateTime(uc.TglMulai).ToString();
                                    uc.TglMulai = Convert.ToDateTime(LastDay.ToString().PadLeft(2, '0') + "-" + ddlBulan.SelectedValue.PadLeft(2, '0') + "-" + ddlTahun.SelectedValue.ToString());

                                    ZetroView zx = new ZetroView();
                                    zx.QueryType = Operation.CUSTOM;
                                    int idSopScore = 0;
                                    string targetKe = string.Empty;
                                    decimal pointNilai = 0;
                                    string ketActual = string.Empty;
                                    zx.CustomQuery = "select ID,CategoryID,TargetKe,PointNilai from ISO_SOPScore where CategoryID=" + categoryID + " " +
                                                     "and RowStatus>-1 and TargetKe='" + ketPes + "' " +
                                                     "and CategoryID in (select CategoryID from ISO_UserCategory where Sarmut like '%" + sarmutPrs + "%') ";
                                    SqlDataReader xdr = zx.Retrieve();
                                    if (xdr.HasRows)
                                    {
                                        while (xdr.Read())
                                        {
                                            idSopScore = Int32.Parse(xdr["ID"].ToString());
                                            targetKe = xdr["TargetKe"].ToString();
                                            pointNilai = Convert.ToDecimal(xdr["PointNilai"].ToString());
                                            if (idSopScore > 0)
                                            {
                                                ketActual = aktual + "%";
                                            }
                                        }
                                    }
                                    uc.SopScoreID = idSopScore;
                                    uc.KetTargetKe = targetKe;
                                    uc.PointNilai = pointNilai;
                                    uc.Actual = ketActual;

                                    arrData.Add(uc);
                                    strError = SimpanKPI(uc);
                                }
                            }
                            else
                            {
                                ArrayList arrSarPesUPdate = new ArrayList();
                                ArrayList arrSarPesUPdate2 = new ArrayList();
                                SarmutPESFacade updatesarPesFacade = new SarmutPESFacade();
                                arrSarPesUPdate = updatesarPesFacade.RetrieveUserCategory2(sarmutPrs);
                                foreach (SarmutPes up in arrSarPesUPdate)
                                {
                                    int categoryID = up.CategoryID;
                                    int deptID = up.DeptID;

                                    arrSarPesUPdate2 = updatesarPesFacade.RetrieveID(deptID, ddlBulan.SelectedValue, ddlTahun.SelectedValue, categoryID);
                                    foreach (SarmutPes tp in arrSarPesUPdate2)
                                    {
                                        //SarmutPes updateSarPes = updatesarPesFacade.RetrieveID(deptID, ddlBulan.SelectedValue, ddlTahun.SelectedValue, categoryID);
                                        int id = tp.ID;

                                        SarmutPes updateSarPesScore = updatesarPesFacade.RetrieveUpdateScore(categoryID, ketPes);
                                        int IDScore = updateSarPesScore.IDScore;
                                        int CategoryID = updateSarPesScore.CategoryID;
                                        string TargetKe = updateSarPesScore.KetTargetKe;
                                        decimal PointNilai = updateSarPesScore.PointNilai;

                                        up.KPIID = id;
                                        up.Ket = aktual + "%";
                                        up.SopScoreID = IDScore;
                                        up.KetTargetKe = TargetKe;
                                        up.PointNilai = PointNilai;
                                        arrData.Add(up);
                                        strError = UpdateKPI(up);
                                    }
                                }
                            }
                        }
                    }
                    else if (deptID2 == 19)
                    {
                        string picHead = wo5.Ket;
                        int startIndexE = 54;
                        int startIndexM = (picHead.Length.ToString() == "57") ? 0 : 113;
                        int startIndexU = (picHead.Length.ToString() == "118") ? 0 : (picHead.Length.ToString() == "117") ? 0 : (picHead.Length.ToString() == "119") ? 0 : 173;
                        int startE = 1;
                        //int startM = 61;
                        //int startU = 117;

                        string HeadElektrik = (picHead.Substring(startE) != string.Empty && picHead.Substring(startIndexE, 1) == "0") ? picHead.Substring(startIndexE, 2) : (picHead.Substring(startE) != string.Empty && picHead.Substring(startIndexE, 1) != "0") ? picHead.Substring(startIndexE, 3) : "100";
                        string HeadMekanik = (startIndexM.ToString() == "0") ? "100" : (picHead.Substring(startIndexM, 1) == "0") ? picHead.Substring(startIndexM, 2) : (picHead.Substring(startIndexM, 1) != "0") ? picHead.Substring(startIndexM, 3) : "100";
                        string HeadUtility = (startIndexU.ToString() == "0") ? "100" : (startIndexM.ToString() == "0") ? "100" : (picHead.Substring(172, 1) == "0") ? "100" : picHead.Substring(startIndexU, 3);


                        ArrayList arrData = new ArrayList();
                        ZetroView zs = new ZetroView();
                        zs.QueryType = Operation.CUSTOM;
                        decimal targetSarmutMtn = 0;
                        int bulan = Convert.ToInt32(ddlBulan.SelectedValue) - 1;
                        zs.CustomQuery = "SELECT top 1 * FROM SPD_TransPrs WHERE SarmutPID IN (SELECT ID from SPD_Perusahaan where Rowstatus>-1 and " +
                                         "SarMutPerusahaan='" + sarmutPrs + "') AND Approval>1 and Target>0 order by id desc ";
                        SqlDataReader adr = zs.Retrieve();
                        if (adr.HasRows)
                        {
                            while (adr.Read())
                            {
                                targetSarmutMtn = (adr["Target"] == DBNull.Value) ? 0 : Convert.ToDecimal(adr["Target"]);
                            }
                        }

                        if (Convert.ToDecimal(aktual) != 0)
                        {
                            string strError = "";
                            int valueActual = 0;

                            string ketPes = string.Empty;
                            string ketPesElektrik = string.Empty;
                            string ketPesMekanik = string.Empty;
                            string ketPesUtility = string.Empty;
                            string ketPesNew = string.Empty;
                            string AktualNew = string.Empty;
                            //if (HeadElektrik.Trim() == "0 %")
                            HeadElektrik = HeadElektrik.Replace("%", " ");
                            //if (HeadMekanik.Trim() == "0 %")
                            HeadMekanik = HeadMekanik.Replace("%", " ");
                            //if (HeadUtility.Trim() == "0 %")
                            HeadUtility = HeadUtility.Replace("%", " ");
                            ketPes = (aktual > 95) ? ">95%" : (aktual >= 86 && aktual <= 95) ? "86%-95%" : (aktual >= 71 && aktual <= 85) ? "71%-85%" : (aktual >= 61 && aktual <= 70) ? "61%-70%" : "<=60%";
                            ketPesElektrik = (Convert.ToDecimal(HeadElektrik) > 95) ? ">95%" : (Convert.ToDecimal(HeadElektrik) >= 86 && Convert.ToDecimal(HeadElektrik) <= 95) ? "86%-95%" : (Convert.ToDecimal(HeadElektrik) >= 71 && Convert.ToDecimal(HeadElektrik) <= 85) ? "71%-85%" : (Convert.ToDecimal(HeadElektrik) >= 61 && Convert.ToDecimal(HeadElektrik) <= 70) ? "61%-70%" : (Convert.ToDecimal(HeadElektrik) > 0 && Convert.ToDecimal(HeadElektrik) <= 60) ? "<=60%" : "100%";
                            ketPesMekanik = (Convert.ToDecimal(HeadMekanik) > 95) ? ">95%" : (Convert.ToDecimal(HeadMekanik) >= 86 && Convert.ToDecimal(HeadMekanik) <= 95) ? "86%-95%" : (Convert.ToDecimal(HeadMekanik) >= 71 && Convert.ToDecimal(HeadMekanik) <= 85) ? "71%-85%" : (Convert.ToDecimal(HeadMekanik) >= 61 && Convert.ToDecimal(HeadMekanik) <= 70) ? "61%-70%" : (Convert.ToDecimal(HeadMekanik) > 0 && Convert.ToDecimal(HeadMekanik) <= 60) ? "<=60%" : "100%";
                            ketPesUtility = (Convert.ToDecimal(HeadUtility) > 95) ? ">95%" : (Convert.ToDecimal(HeadUtility) >= 86 && Convert.ToDecimal(HeadUtility) <= 95) ? "86%-95%" : (Convert.ToDecimal(HeadUtility) >= 71 && Convert.ToDecimal(HeadUtility) <= 85) ? "71%-85%" : (Convert.ToDecimal(HeadUtility) >= 61 && Convert.ToDecimal(HeadUtility) <= 70) ? "61%-70%" : (Convert.ToDecimal(HeadUtility) > 0 && Convert.ToDecimal(HeadUtility) <= 60) ? "<=60%" : "100%";

                            double percentActual = Convert.ToDouble(valueActual);
                            ZetroView zv = new ZetroView();
                            zv.QueryType = Operation.CUSTOM;
                            int IdKPI = 0;
                            string kpiName = string.Empty;
                            zv.CustomQuery = "SELECT top 1 * FROM ISO_KPI WHERE CategoryID in (select ID from ISO_UserCategory where Sarmut='" + sarmutPrs + "') and DeptID=19 and month(TglMulai)=" + ddlBulan.SelectedValue + " AND year(TglMulai)=" + ddlTahun.SelectedValue + " ORDER BY ID desc";
                            SqlDataReader dr = zv.Retrieve();
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    IdKPI = Int32.Parse(dr["ID"].ToString());
                                    kpiName = dr["KPIName"].ToString();
                                }
                            }
                            if (kpiName == string.Empty)
                            {
                                int LastDay = DateTime.DaysInMonth(int.Parse(ddlTahun.SelectedValue.ToString()), int.Parse(ddlBulan.SelectedValue.ToString()));
                                ArrayList arrSarPes = new ArrayList();
                                SarmutPESFacade sarPesFacade = new SarmutPESFacade();
                                arrSarPes = sarPesFacade.RetrieveUserCategory3(sarmutPrs);
                                foreach (SarmutPes uc in arrSarPes)
                                {
                                    int idUserCategory = uc.IDUserCategory;
                                    int userID = uc.UserID;
                                    int bagianID = uc.BagianID;
                                    decimal bobotNilai = uc.BobotNilai;
                                    string pic = uc.Pic;
                                    int deptID = uc.DeptID;
                                    string description = uc.Description;
                                    int pesType = uc.PesType;
                                    int categoryID = uc.CategoryID;
                                    string bagianAutoPES = uc.BagianAutoPES;
                                    int pjgDept = ((Users)Session["Users"]).DeptID;
                                    string DdlDept = "MAINTENANCE";
                                    uc.DeptName = (pjgDept >= 4) ? DdlDept.Substring(0, 3) : DdlDept.Substring(0, pjgDept);
                                    txtTglMulai.Text = Convert.ToDateTime(uc.TglMulai).ToString();
                                    uc.TglMulai = Convert.ToDateTime(LastDay.ToString().PadLeft(2, '0') + "-" + ddlBulan.SelectedValue.PadLeft(2, '0') + "-" + ddlTahun.SelectedValue.ToString());

                                    ZetroView zx = new ZetroView();
                                    zx.QueryType = Operation.CUSTOM;
                                    int idSopScore = 0;
                                    string targetKe = string.Empty;
                                    decimal pointNilai = 0;
                                    string ketActual = string.Empty;
                                    ketPesNew = (bagianAutoPES == "Head Elektrik") ? ketPesElektrik : (bagianAutoPES == "Head Mekanik") ? ketPesMekanik : (bagianAutoPES == "Head Utility") ? ketPesUtility : (bagianAutoPES == "Manager Maintenance") ? ketPes : string.Empty;
                                    AktualNew = (bagianAutoPES == "Head Elektrik") ? HeadElektrik : (bagianAutoPES == "Head Mekanik") ? HeadMekanik : (bagianAutoPES == "Head Utility") ? HeadUtility : (bagianAutoPES == "Manager Maintenance") ? aktual.ToString() : string.Empty;

                                    zx.CustomQuery = "select ID,CategoryID,TargetKe,PointNilai from ISO_SOPScore where CategoryID=" + categoryID + " " +
                                                     "and RowStatus>-1 and TargetKe='" + ketPesNew + "' " +
                                                     "and CategoryID in (select CategoryID from ISO_UserCategory where Sarmut like '%" + sarmutPrs + "%') ";
                                    SqlDataReader xdr = zx.Retrieve();
                                    if (xdr.HasRows)
                                    {
                                        while (xdr.Read())
                                        {
                                            idSopScore = Int32.Parse(xdr["ID"].ToString());
                                            targetKe = xdr["TargetKe"].ToString();
                                            pointNilai = Convert.ToDecimal(xdr["PointNilai"].ToString());
                                            if (idSopScore > 0)
                                            {
                                                ketActual = Math.Round(Convert.ToDouble(AktualNew)) + "%";
                                            }
                                        }
                                    }
                                    uc.SopScoreID = idSopScore;
                                    uc.KetTargetKe = targetKe;
                                    uc.PointNilai = pointNilai;
                                    uc.Actual = ketActual;

                                    arrData.Add(uc);
                                    strError = SimpanKPI(uc);
                                }
                            }
                            else
                            {
                                ArrayList arrSarPesUPdate = new ArrayList();
                                ArrayList arrSarPesUPdate2 = new ArrayList();
                                string ketPes2 = string.Empty;
                                string ketPesElektrik2 = string.Empty;
                                string ketPesMekanik2 = string.Empty;
                                string ketPesUtility2 = string.Empty;
                                string ketPesNew2 = string.Empty;
                                string AktualNew2 = string.Empty;

                                SarmutPESFacade updatesarPesFacade = new SarmutPESFacade();

                                ketPes2 = (aktual > 95) ? ">95%" : (aktual >= 86 && aktual <= 95) ? "86%-95%" : (aktual >= 71 && aktual <= 85) ? "71%-85%" : (aktual >= 61 && aktual <= 70) ? "61%-70%" : "<=60%";
                                ketPesElektrik2 = (Convert.ToDecimal(HeadElektrik) > 95) ? ">95%" : (Convert.ToDecimal(HeadElektrik) >= 86 && Convert.ToDecimal(HeadElektrik) <= 95) ? "86%-95%" : (Convert.ToDecimal(HeadElektrik) >= 71 && Convert.ToDecimal(HeadElektrik) <= 85) ? "71%-85%" : (Convert.ToDecimal(HeadElektrik) >= 61 && Convert.ToDecimal(HeadElektrik) <= 70) ? "61%-70%" : "<=60%";
                                ketPesMekanik2 = (Convert.ToDecimal(HeadMekanik) > 95) ? ">95%" : (Convert.ToDecimal(HeadMekanik) >= 86 && Convert.ToDecimal(HeadMekanik) <= 95) ? "86%-95%" : (Convert.ToDecimal(HeadMekanik) >= 71 && Convert.ToDecimal(HeadMekanik) <= 85) ? "71%-85%" : (Convert.ToDecimal(HeadMekanik) >= 61 && Convert.ToDecimal(HeadMekanik) <= 70) ? "61%-70%" : "<=60%";
                                ketPesUtility2 = (Convert.ToDecimal(HeadUtility) > 95) ? ">95%" : (Convert.ToDecimal(HeadUtility) >= 86 && Convert.ToDecimal(HeadUtility) <= 95) ? "86%-95%" : (Convert.ToDecimal(HeadUtility) >= 71 && Convert.ToDecimal(HeadUtility) <= 85) ? "71%-85%" : (Convert.ToDecimal(HeadUtility) >= 61 && Convert.ToDecimal(HeadUtility) <= 70) ? "61%-70%" : "<=60%";

                                arrSarPesUPdate = updatesarPesFacade.RetrieveUserCategory4(sarmutPrs);
                                foreach (SarmutPes up in arrSarPesUPdate)
                                {
                                    int categoryID = up.CategoryID;
                                    int deptID = up.DeptID;
                                    string bagianAutoPES = up.BagianAutoPES;

                                    ketPesNew2 = (bagianAutoPES == "Head Elektrik") ? ketPesElektrik2 : (bagianAutoPES == "Head Mekanik") ? ketPesMekanik2 : (bagianAutoPES == "Head Utility") ? ketPesUtility2 : (bagianAutoPES == "Manager Maintenance") ? ketPes2 : string.Empty;
                                    AktualNew2 = (bagianAutoPES == "Head Elektrik") ? HeadElektrik : (bagianAutoPES == "Head Mekanik") ? HeadMekanik : (bagianAutoPES == "Head Utility") ? HeadUtility : (bagianAutoPES == "Manager Maintenance") ? aktual.ToString() : string.Empty;

                                    arrSarPesUPdate2 = updatesarPesFacade.RetrieveID(deptID, ddlBulan.SelectedValue, ddlTahun.SelectedValue, categoryID);
                                    foreach (SarmutPes tp in arrSarPesUPdate2)
                                    {
                                        //SarmutPes updateSarPes = updatesarPesFacade.RetrieveID(deptID, ddlBulan.SelectedValue, ddlTahun.SelectedValue, categoryID);
                                        int id = tp.ID;

                                        SarmutPes updateSarPesScore = updatesarPesFacade.RetrieveUpdateScore(categoryID, ketPesNew2);
                                        int IDScore = updateSarPesScore.IDScore;
                                        int CategoryID = updateSarPesScore.CategoryID;
                                        string TargetKe = updateSarPesScore.KetTargetKe;
                                        decimal PointNilai = updateSarPesScore.PointNilai;

                                        up.KPIID = id;
                                        up.Ket = Math.Round(Convert.ToDouble(AktualNew2)) + "%";
                                        up.SopScoreID = IDScore;
                                        up.KetTargetKe = TargetKe;
                                        up.PointNilai = PointNilai;
                                        arrData.Add(up);
                                        strError = UpdateKPI(up);
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                }
                catch { }
                LPeriode.Visible = true; LTotal.Visible = true; LTarget.Visible = true; LPersen.Visible = true;
                txtTarget.Visible = true; TxtPersen.Visible = true; txtTotal.Visible = true; txtPeriod.Visible = true;

                lstSOP.DataSource = arrData5;
                lstSOP.DataBind();
            }
            else if (Tanda2 != "3")
            {
                txtTarget.Text = ""; TxtPersen.Text = ""; txtTotal.Text = ""; LPeriode.Visible = false;
                LTotal.Visible = false; LTarget.Visible = false; LPersen.Visible = false; txtTarget.Visible = false;
                TxtPersen.Visible = false; txtTotal.Visible = false; txtPeriod.Visible = false;

                lstSOP.DataSource = arrData5;
                lstSOP.DataBind();
            }


            else if (Tanda2 == "2")
            {
                lstSOP.DataSource = arrData5;
                lstSOP.DataBind();
            }
            else if (Tanda2 == "5" || Tanda2 == "3")
            {
                lstSOP.DataSource = arrData5;
                lstSOP.DataBind();
            }
            else if (Tanda2 == "51")
            {
                lstSOP.DataSource = arrData5;
                lstSOP.DataBind();
            }
        }
        private string SimpanKPI(SarmutPes sop)
        {
            //string strEvent = "Insert";
            ISO_DocumentNoFacade docNoFacade = new ISO_DocumentNoFacade();
            ISO_DocumentNo docNo = docNoFacade.RetrieveByDept(1, 7, Convert.ToDateTime(txtTglMulai.Text).Year);
            if (docNo.ID > 0)
            {
                docNo.DocNo = docNo.DocNo + 1;
                //docNo.PesType = int.Parse(txtPES.Text); //sop
                docNo.DeptID = 7;
                docNo.Tahun = Convert.ToDateTime(txtTglMulai.Text).Year;
                Company cp = new CompanyFacade().RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                docNo.Plant = cp.KodeLokasi;
            }
            else
            {
                docNo.DocNo = 1;
                //docNo.PesType = int.Parse(txtPES.Text); //sop
                docNo.DeptID = 7;
                docNo.Tahun = Convert.ToDateTime(txtTglMulai.Text).Year;
                //HO ikut C dulu
                Company cp = new CompanyFacade().RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                docNo.Plant = cp.KodeLokasi;
            }

            SarmutPESProcessFacade sarpesProcessFacade = new SarmutPESProcessFacade(sop, docNo);
            string strError = string.Empty;
            strError = sarpesProcessFacade.Insert();
            if (strError == string.Empty)
            {
                //InsertLog(strEvent);
                //txtTaskNo.Text = "Doc No. : " + sarpesProcessFacade.sopNonya;
            }
            return strError;
        }
        private string UpdateKPI(SarmutPes sop)
        {
            //string strEvent = "Update";
            ISO_DocumentNoFacade docNoFacade = new ISO_DocumentNoFacade();
            ISO_DocumentNo docNo = docNoFacade.RetrieveByDept(1, 7, Convert.ToInt32(ddlTahun.SelectedValue));
            if (docNo.ID > 0)
            {
                docNo.DocNo = docNo.DocNo + 1;
                //docNo.PesType = int.Parse(txtPES.Text); //sop
                docNo.DeptID = 7;
                docNo.Tahun = Convert.ToDateTime(txtTglMulai.Text).Year;
                Company cp = new CompanyFacade().RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                docNo.Plant = cp.KodeLokasi;
            }
            else
            {
                docNo.DocNo = 1;
                //docNo.PesType = int.Parse(txtPES.Text); //sop
                docNo.DeptID = 7;
                docNo.Tahun = Convert.ToDateTime(txtTglMulai.Text).Year;
                //HO ikut C dulu
                Company cp = new CompanyFacade().RetrieveByDepoId(((Users)Session["Users"]).UnitKerjaID);
                docNo.Plant = cp.KodeLokasi;
            }

            SarmutPESProcessFacade sarpesProcessFacade = new SarmutPESProcessFacade(sop, docNo);
            string strError = string.Empty;
            strError = sarpesProcessFacade.UpdateKpi();
            if (strError == string.Empty)
            {
                //InsertLog(strEvent);
                //txtTaskNo.Text = "Doc No. : " + sarpesProcessFacade.sopNonya;
            }
            return strError;
        }

        //}

        protected void lstBA_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Users users = (Users)Session["Users"];
            int StatusApv = Convert.ToInt32(Session["StatusApv"]);
            WorkOrder_New wo = (WorkOrder_New)e.Item.DataItem;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=PemantauanWO" + "_" + ddlBulan.SelectedItem + ddlTahun.SelectedItem + ".xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            //string Html = "Periode :";
            //string HtmlEnd = "";
            lst.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("collapse\">", "collapse\" border=\"1\">");
            Contents = Contents.Replace("cls\">", "\">'");
            Response.Write(Contents);
            Response.Flush();
            Response.End();

            //System.IO.StringWriter sw = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
            //lst.RenderControl(htw);
            //string renderedGridView = sw.ToString();
            //string map = Server.MapPath("~\\Report\\DownloadFile\\LaporanWO.xls");
            //File.WriteAllText(map, renderedGridView);
            //string myScript = "window.open('../Report/DownloadFile/LaporanWO.xls','_blank');";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "MyScript", myScript, true);
        }

        protected void attachm_DataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Image pre = (Image)e.Item.FindControl("lihat");
                Image hps = (Image)e.Item.FindControl("hapus");
                WorkOrder_New att = (WorkOrder_New)e.Item.DataItem;
                WorkOrder_New wo = new WorkOrder_New();
                WorkOrderFacade_New wof = new WorkOrderFacade_New();
                wo.ID = Convert.ToInt32(hps.CssClass);
                int StatApvWO = wof.Retrieve_apv_wo_atch(wo.ID);

                hps.Visible = false;
            }
        }

        private bool CheckAttach(int WOID)
        {
            bool result = false;
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "Select ID from WorkOrder_Lampiran where RowStatus>-1 and WOID=" + WOID.ToString();
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                result = true;
            }
            return result;
        }

        //private ArrayList TercapaiWOPIC()
        //{
        //    ArrayList arrData = new ArrayList();
        //    ZetroView zw = new ZetroView();
        //    zw.QueryType = Operation.CUSTOM;
        //    zw.CustomQuery = "Select * from " +
        //                     "(SELECT count(ID) AS ElektrikTercapai FROM WorkOrder WHERE DeptID_PenerimaWO=19 AND RowStatus>-1 " +
        //                     "and LEFT(convert(char,DueDateWO,112),6)='202101' and Apv>4 and pelaksana='Head Elektrik')as a, " +
        //                     "(SELECT count(ID) AS MekanikTercapai FROM WorkOrder WHERE DeptID_PenerimaWO=19 AND RowStatus>-1 " +
        //                     "and LEFT(convert(char,DueDateWO,112),6)='202101' and Apv>4 and pelaksana='Head Mekanik') as b, " +
        //                     "(SELECT count(ID) AS UtilityTercapai FROM WorkOrder WHERE DeptID_PenerimaWO=19 AND RowStatus>-1 " +
        //                     "and LEFT(convert(char,DueDateWO,112),6)='202101' and Apv>4 and pelaksana='Head Utility') as c" ;
        //    SqlDataReader sdr = zw.Retrieve();
        //    if (sdr.HasRows)
        //    {
        //        while (sdr.Read())
        //        {
        //            arrData.Add(new WorkOrder_New
        //            {
        //                Tahun = Convert.ToInt32(sdr["Tahun"].ToString())
        //            });
        //        }
        //    }
        //}

        private ArrayList ListWOTahun()
        {
            ArrayList arrData = new ArrayList();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            //zw.CustomQuery = " select DISTINCT(LEFT(convert(char,createdtime,112),4))Tahun from WorkOrder_LogApproval where RowStatus > -1 ";
            zw.CustomQuery = " with tahun as (select top 1 Tahun + 1 Tahun from ( " +
            " select DISTINCT(LEFT(convert(char,createdtime,112),4))Tahun from WorkOrder_LogApproval where RowStatus > -1 ) as x order by tahun desc), " +
            " tahun1 as (select DISTINCT(LEFT(convert(char,createdtime,112),4))Tahun from WorkOrder_LogApproval where RowStatus > -1) " +
            " select * from ( " +
            " select * from tahun  " +
            " union all " +
            " select * from tahun1) as x order by tahun ";
            SqlDataReader sdr = zw.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new WorkOrder_New
                        {
                            Tahun = Convert.ToInt32(sdr["Tahun"].ToString())
                        });
                    }
                }
            }
            else
            {
                arrData.Add(new WorkOrder_New { Tahun = DateTime.Now.Year });
            }
            return arrData;
        }

        private ArrayList ListWOTahun2()
        {
            ArrayList arrData = new ArrayList();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            //zw.CustomQuery = "select DISTINCT(LEFT(convert(char,Createdtime,112),4))Tahun from WorkOrder where RowStatus > -1 and (LEFT(convert(char,Createdtime,112),4)>2016) ";
            zw.CustomQuery = " with tahun as (select top 1 Tahun + 1 Tahun from ( " +
            " select DISTINCT(LEFT(convert(char,Createdtime,112),4))Tahun from WorkOrder where RowStatus > -1 and (LEFT(convert(char,Createdtime,112),4)>2016) ) as x " +
            " order by tahun desc), " +
            " tahun1 as (select DISTINCT(LEFT(convert(char,Createdtime,112),4))Tahun from WorkOrder where RowStatus > -1 and (LEFT(convert(char,Createdtime,112),4)>2016) ) " +
            " select * from ( " +
            " select * from tahun  " +
            " union all  " +
            " select * from tahun1) as x order by tahun ";
            SqlDataReader sdr = zw.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        arrData.Add(new WorkOrder_New
                        {
                            Tahun = Convert.ToInt32(sdr["Tahun"].ToString())
                        });
                    }
                }
            }
            else
            {
                arrData.Add(new WorkOrder_New { Tahun = DateTime.Now.Year });
            }
            return arrData;
        }

        protected void RBLewat_CheckedChanged(object sender, EventArgs e)
        {
            if (RBLewat.Checked == true)
            {
                RB1.Checked = false; RB2.Checked = false; RBK.Checked = false; RBPO.Checked = false; ddlDept.Enabled = true;
                LabelTotal.Visible = false; LabelTotalNilai.Visible = false; LabelTarget.Visible = false; LabelTargetNilai.Visible = false;
                LabelPersen.Visible = false; LabelPersenNilai.Visible = false;
                Response.Redirect("LaporanOutstandingWO.aspx");
            }
        }

        protected void RB1_CheckedChanged(object sender, EventArgs e)
        {
            if (RB1.Checked == true)
            {
                RB2.Checked = false; RBK.Checked = false; RBPO.Checked = false; ddlDept.Enabled = true;
                LabelTotal.Visible = false; LabelTotalNilai.Visible = false; LabelTarget.Visible = false; LabelTargetNilai.Visible = false;
                LabelPersen.Visible = false; LabelPersenNilai.Visible = false;
            }
        }

        protected void RB2_CheckedChanged(object sender, EventArgs e)
        {
            if (RB2.Checked == true)
            { RB1.Checked = false; RBK.Checked = false; RBPO.Checked = false; ddlDept.Enabled = true; }
        }

        protected void RBK_CheckedChanged(object sender, EventArgs e)
        {
            if (RBK.Checked == true)
            {
                RB1.Checked = false; RB2.Checked = false; RBPO.Checked = false; ddlDept.Enabled = false;
                LabelTotal.Visible = false; LabelTotalNilai.Visible = false; LabelTarget.Visible = false; LabelTargetNilai.Visible = false;
                LabelPersen.Visible = false; LabelPersenNilai.Visible = false;
            }
        }
        protected void RBPO_CheckedChanged(object sender, EventArgs e)
        {
            if (RBPO.Checked == true)
            {
                RB2.Checked = false; RBK.Checked = false; RB1.Checked = false; ddlDept.Enabled = true;
                LabelTotal.Visible = false; LabelTotalNilai.Visible = false; LabelTarget.Visible = false; LabelTargetNilai.Visible = false;
                LabelPersen.Visible = false; LabelPersenNilai.Visible = false;
            }
        }


        protected void ddlDept_change(object sender, EventArgs e)
        {
            //if (ddlStatus.SelectedValue == "4")
            //{
            //    ddlBulan.Visible = true; ddlTahun.Visible = true;
            //    LabelPeriode.Visible = true;
            //    LabelPeriode.Text = "Periode";
            //}
            //else
            //{
            //    ddlBulan.Visible = false; ddlTahun.Visible = false;
            //    LabelPeriode.Visible = false;
            //}
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        protected void ddlBulan_Change(object sender, EventArgs e)
        {
            int LastDay = DateTime.DaysInMonth(int.Parse(ddlTahun.SelectedValue.ToString()), int.Parse(ddlBulan.SelectedValue.ToString()));
            txtTglMulai.Text = LastDay.ToString().PadLeft(2, '0') + "-" + ddlBulan.SelectedValue.PadLeft(2, '0') + "-" + ddlTahun.SelectedValue.ToString();
        }
        protected void ddlTahun_Change(object sender, EventArgs e)
        {
            int LastDay = DateTime.DaysInMonth(int.Parse(ddlTahun.SelectedValue.ToString()), int.Parse(ddlBulan.SelectedValue.ToString()));
            txtTglMulai.Text = LastDay.ToString().PadLeft(2, '0') + "-" + ddlBulan.SelectedValue.PadLeft(2, '0') + "-" + ddlTahun.SelectedValue.ToString();
        }

    }
}