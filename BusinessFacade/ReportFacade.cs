using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using BusinessFacade;
using DataAccessLayer;
using Domain;
using Dapper;

namespace BusinessFacade
{

    public class ReportFacade
    {
        private ArrayList arrReport;

        //public string ViewLapLoadingTime(string drTgl, string sdTgl, int KendaraanID, int WaktuAwal)
        //{
        //    #region Query Lama di nonaktifkan
        //    //       return " select C.*, " +
        //    //"case when Status2>TargetLoading then 'Lewat'  else 'OK' end Status, " +
        //    //           // "case when Status2>TargetLoading then 'Lewat'  else 'OK' end StatusLoading, " +
        //    //"case when MobilSendiri=0 then case when Status2=999 then 'Lewat' else " +
        //    //"case when Status2>TargetLoading then case when Rit1=1 then 'Lewat' else 'OK' end else case when Rit1=1 then 'OK' else 'Lewat' end end end else " +
        //    //"case when Status2=999 then 'Lewat' else  case when Status2>TargetLoading then 'Lewat' else 'OK' end end " +
        //    //"end StatusL, " +
        //    //"case when MobilSendiri=0 then " +
        //    //"case when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) <=390 AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))<=390 ) " +
        //    //"then 'OK' " +
        //    //"when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) <=390 AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))<540 )	" +
        //    //"then 'Lewat' " +
        //    //"when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) <=390 AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))>540 ) " +
        //    //"then 'Lewat' " +
        //    //"when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) >390 AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))<=540 ) " +
        //    //"then 'Lewat2' " +
        //    //"when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) >390 AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))>540 and Status2<TargetLoading ) " +
        //    //"then 'OK2' " +
        //    //"when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) >390 AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))>540 and Status2>TargetLoading ) " +
        //    //"then 'Lewat3' " +
        //    //"when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) > 540 and Status2<TargetLoading ) " +
        //    //"then 'OK3'" +
        //    //"end else " +
        //    //           //"case when Status2>TargetLoading then 'Lewat'  else 'OK' end end Status2 " +
        //    // "case when Status2>TargetLoading then 'Lewat'  else 'OK' end end StatusLoading " +
        //    //"from(select A.ID,A.UrutanNo,case when A.MobilSendiri=0 then 'BPAS' else '' end Ket,A.MobilSendiri,A.TimeIn,A.TimeOut,A.NoPolisi,B.JenisMobil,B.Target as TargetLoading, " +
        //    //"case when A.MobilSendiri=0 then  " +
        //    //           //--TimeIn jam 3 s/d 9 = Rit1
        //    //"case when ( ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) >=180 AND ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<=540 ) " +
        //    //"then 1 else 2 end else 1 end Rit1, " +
        //    //"case when A.MobilSendiri=0 then " +
        //    //"case when datediff(DAY,A.TimeIn,A.TimeOut)=0 then " +
        //    //           //--jk timeIn kurang dari 6.30 dan keluar lewat 6.30
        //    //"case when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)<=390 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) >390 " +
        //    //           //--jk TimeIn lebih 6.30 dan TimeOut lebih dari 6.30
        //    //" then  case	when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)>390 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>390 " +
        //    //           //--jika dateng lewat dari 6.30
        //    //"then case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= 780 " +
        //    //           //--jika keluar antara jam 12 s/d 13 maka 12
        //    //"then 720 - ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))  " +
        //    //"else case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780 " +
        //    //           //--jika keluar lewat jam 13
        //    //"then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) " +
        //    //           //-- +60 gak term asuk jam istirahat
        //    //"else case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 540 " +
        //    //           //--jika keluar lewat jam 9 " +
        //    //"then ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) " +
        //    //           //--kelaru sebelum jam 12 dari 6.30 mobilsendiri
        //    //"else ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) " +
        //    //"end end " +
        //    //           //-- +60 utk kurangi jam istirahat
        //    //"end else " +
        //    //"case when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)<=390 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>390 " +
        //    //"then 999 " +
        //    //"end end else " +
        //    //"case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= 780 " +
        //    //           //--mobil sendiri  keluar jam istirahat
        //    //"then 720 - ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))  " +
        //    //"else " +
        //    //"case when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)<=390 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= 390 " +
        //    //"then ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) " +
        //    //"else " +
        //    //"((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) " +
        //    //"end	end	end	else 999999	end else " +
        //    //           //--mobil luar
        //    //"case when datediff(DAY,A.TimeIn,A.TimeOut)=0 then " +
        //    //"case when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)>="+WaktuAwal+" " +
        //    //           //--jika lewat jam 7 " +
        //    //"then case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= 780 " +
        //    //           //--jika keluar antara jam 12 s/d 13 maka 12
        //    //"then 720 - ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))  " +
        //    //"else case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780 and ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))>780 " +
        //    //           //--jika keluar lewat jam 13	 			
        //    //"then case when ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) > 720 and ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) <= 780 " +
        //    //"then ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) - 780 " +
        //    //"else (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- (((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))) " +
        //    //           //-- +60 gak term asuk jam istirahat " +
        //    //"end	else  case when ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))<720 " +
        //    //"then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn) ) " +
        //    //"when  ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780 " +
        //    //"then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)+60 ) " +
        //    //"else (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))-  ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) " +
        //    //           //   --kelaru sebelum jam 12 
        //    //"end	end " +
        //    //           //-- +60 utk kurangi jam istirahat
        //    //"end else " +
        //    //"case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= 780 " +
        //    //"then 720 - ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))  " +
        //    //"else ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-"+WaktuAwal+"	end	" +
        //    //"end	else 999 end " +
        //    //"end Status2 " +
        //    //"from LoadingTime as A, MasterKendaraan as B where A.KendaraanID=B.ID and A.TimeOut is not null and A.TimeOut>A.TimeIn " +
        //    //"and Convert(varchar,A.TimeIn,112) >= '" + drTgl + "' and Convert(varchar,A.TimeIn,112) <= '" + sdTgl + "'  ) as C order by MobilSendiri,TimeIn ";
        //    #endregion
        //    #region Query Baru
        //    string strSql = "/*--gak liat Rit */" +
        //                  "  select C.*, " +
        //                  "  case when Status2>TargetLoading then 'Lewat'  else 'OK'  " +
        //                  "  end StatusLoading," +
        //                  "  case when MobilSendiri=0 then case when Status2=999 then 'Lewat' else  " +
        //                  "      case when Status2>TargetLoading then " +
        //                  "          case when Rit1=1 then 'Lewat' else 'OK' end " +
        //                  "      else case when Rit1=1 then 'OK' else 'Lewat' end end end " +
        //                  "  else " +
        //                  "       case when Status2=999 then 'Lewat' else  case when Status2>TargetLoading then 'Lewat' else 'OK' end end " +
        //                  "  end StatusL," +
        //                  "  case when MobilSendiri=0 then " +
        //                  "      case when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) <=" + WaktuAwal + " AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))<=" + WaktuAwal + " ) " +
        //                  "          then 'OK' " +
        //                  "      when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) <=" + WaktuAwal + " AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))<540 )		" +
        //                  "          then 'Lewat' " +
        //                  "      when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) <=" + WaktuAwal + " AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))>540 ) " +
        //                  "          then 'Lewat' " +
        //                  "      when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) >" + WaktuAwal + " AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))<=540 ) " +
        //                  "          then 'Lewat2' " +
        //                  "      when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) >" + WaktuAwal + " AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))>540 and Status2<TargetLoading ) " +
        //                  "          then 'OK2' " +
        //                  "      when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) >" + WaktuAwal + " AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))>540 and Status2>TargetLoading ) " +
        //                  "          then 'Lewat3' " +
        //                  "      when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) > 540 and Status2<TargetLoading ) " +
        //                  "          then 'OK3' " +
        //                  "      end " +
        //                  "  else " +
        //                  "      case when Status2>TargetLoading then 'Lewat'  else 'OK' end  " +

        //                  "  end Status1  " +


        //                  "  from(select A.ID,A.UrutanNo,case when A.MobilSendiri=0 then 'BPAS' else '' end Ket  " +
        //                  "  ,A.MobilSendiri,A.TimeIn,A.TimeOut,A.NoPolisi,B.JenisMobil,B.Target as TargetLoading,  " +
        //                  "  case when A.MobilSendiri=0 then  " +
        //                  " /*--TimeIn jam 3 s/d 9 = Rit1 */ " +
        //                  "      case when ( ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) >=180 AND ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<=540 )  " +
        //                  "          then 1 else 2 end  " +
        //                  "  else 1 end Rit1,  " +

        //                  "  case when A.MobilSendiri=0 then  " +
        //                  "      case when datediff(DAY,A.TimeIn,A.TimeOut)=0 then  " +
        //                  "         /* --jk timeIn kurang dari 6.30 dan keluar lewat 6.30  */" +
        //                  "          case when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)<=" + WaktuAwal + " and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) >" + WaktuAwal +
        //                  "              /*--jk TimeIn lebih 6.30 dan TimeOut lebih dari 6.30 */ " +
        //                  "               then  case	when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)> " + WaktuAwal + " and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>" + WaktuAwal +
        //                  "                     /* --jika dateng lewat dari 6.30*/  " +
        //                  "                      then case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= 780  " +
        //                  "	                          /*  --jika keluar antara jam 12 s/d 13 maka 12 */ " +
        //                  " 	                            then 720 - ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))   " +
        //                  "                          else  " +
        //                  "	                            case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780 " +
        //                  "                         /*--jika keluar lewat jam 13 */ " +
        //                  "		                            then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))  " +
        //                  "                         /*-- +60 gak term asuk jam istirahat */ " +
        //                  "	                            else  " +
        //                  "		                            case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 540 " +
        //                  "                         /*--jika keluar lewat jam 9   */" +
        //                  "		                            then ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))  " +
        //                  "                             /*--kelaru sebelum jam 12 dari 6.30 mobilsendiri */ " +
        //                  "		                            else ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))  " +
        //                  "		                            end  " +
        //                  "	                            end " +
        //                  "                             /*-- +60 utk kurangi jam istirahat */ " +
        //                  "                          end  " +
        //                  "                      else  " +
        //                  "                          case when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)<=390 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>390	 " +
        //                  "	                             then 999  " +
        //                  "                          end		 " +
        //                  "                      end  " +
        //                  "          else  " +
        //                  "              case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= 780  " +
        //                  "                      /*--mobil sendiri  keluar jam istirahat */ " +
        //                  "                      then 720 - ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))   " +
        //                  "                  else		 " +
        //                  "                      case when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)<=" + WaktuAwal + " and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= " + WaktuAwal + "  " +
        //                  "                          then ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))  " +
        //                  "                      /*--jk masuk antara jam 12 dan 13, 16Sept2014  */" +
        //                  "                      when ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))<720  " +
        //                  "                          then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn) )   " +
        //                  "                      when  ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780  " +
        //                  "                          then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)+60 )   " +
        //                  "                      when  ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))>=720 and ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<=780  " +
        //                  "                      and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780  " +
        //                  "                          then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- 780   " +
        //                  "                     /* --jk masuk antara jam 12 dan 13, 16Sept2014 */ " +
        //                  "                      else	 " +
        //                  "                          ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))  " +
        //                  "                      end  " +
        //                  "                  end  " +
        //                  "          end  " +
        //                  "      else  " +
        //                  "          999999  " +
        //                  "      end  " +
        //                  "  else  " +
        //                  "      /*--mobil luar */ " +
        //                  "      case when datediff(DAY,A.TimeIn,A.TimeOut)=0 then  " +
        //                  "          case when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)>=" + WaktuAwal +
        //                  "         /*--jika lewat jam 7 */ " +
        //                  "          then  " +
        //                  "              case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= 780  " +
        //                  "              /*--jika keluar antara jam 12 s/d 13 maka 12*/  " +
        //                  "                  then 720 - ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))   " +
        //                  "              else  " +
        //                  "                  case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780 and ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))>780 " +
        //                  "             /*--jika keluar lewat jam 13 */ " +
        //                  "                  then case when ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) > 720 and ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) <= 780  " +
        //                  "                       then ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) - 780  " +
        //                  "                       else (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- (((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))) " +
        //                  "                 /*-- +60 gak term asuk jam istirahat */ " +
        //                  "                  end  " +
        //                  "                  else  case when ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))<720   " +
        //                  "                          then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn) )  " +
        //                  "                        when  ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780  " +
        //                  "                          then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)+60 )  " +
        //                  "                      /*  --jk masuk antara jam 12 dan 13, 16Sept2014 */ " +
        //                  "                        when  ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))>=720 and ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<=780  " +
        //                  "                         and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780  " +
        //                  "                          then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- 780   " +
        //                  "                      /*  --jk masuk antara jam 12 dan 13 */ " +
        //                  "                        else (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))-  ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))  " +
        //                  "                      /*  --kelaru sebelum jam 12*/  " +
        //                  "                        end  " +
        //                  "                  end   " +
        //                  "                /* -- +60 utk kurangi jam istirahat*/ " +
        //                  "              end  " +
        //                  "          else  " +
        //                  "              case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= 780  " +
        //                  "              then 720 - ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))  " +
        //                  "              else  " +
        //                  "              ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-" + WaktuAwal +
        //                  "             end	 " +
        //                  "          end  " +
        //                  "      else  " +
        //                  "          999  " +
        //                  "      end  " +
        //                  "  end Status2  " +

        //                  "  from LoadingTime as A, MasterKendaraan as B where A.KendaraanID=B.ID and A.TimeOut is not null and A.TimeOut>A.TimeIn  " +
        //                  "  and Convert(varchar,A.TimeIn,112) >= '" + drTgl + "' and Convert(varchar,A.TimeIn,112) <= '" + sdTgl + "' ) as C order by MobilSendiri,TimeIn";



        //    #endregion
        //    return strSql;
        //}

        //public string ViewLapLoadingTime(string drTgl, string sdTgl, int KendaraanID, int WaktuAwal)
        //{
        //    #region Query Lama di nonaktifkan
        //    //       return " select C.*, " +
        //    //"case when Status2>TargetLoading then 'Lewat'  else 'OK' end Status, " +
        //    //           // "case when Status2>TargetLoading then 'Lewat'  else 'OK' end StatusLoading, " +
        //    //"case when MobilSendiri=0 then case when Status2=999 then 'Lewat' else " +
        //    //"case when Status2>TargetLoading then case when Rit1=1 then 'Lewat' else 'OK' end else case when Rit1=1 then 'OK' else 'Lewat' end end end else " +
        //    //"case when Status2=999 then 'Lewat' else  case when Status2>TargetLoading then 'Lewat' else 'OK' end end " +
        //    //"end StatusL, " +
        //    //"case when MobilSendiri=0 then " +
        //    //"case when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) <=390 AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))<=390 ) " +
        //    //"then 'OK' " +
        //    //"when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) <=390 AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))<540 )	" +
        //    //"then 'Lewat' " +
        //    //"when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) <=390 AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))>540 ) " +
        //    //"then 'Lewat' " +
        //    //"when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) >390 AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))<=540 ) " +
        //    //"then 'Lewat2' " +
        //    //"when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) >390 AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))>540 and Status2<TargetLoading ) " +
        //    //"then 'OK2' " +
        //    //"when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) >390 AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))>540 and Status2>TargetLoading ) " +
        //    //"then 'Lewat3' " +
        //    //"when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) > 540 and Status2<TargetLoading ) " +
        //    //"then 'OK3'" +
        //    //"end else " +
        //    //           //"case when Status2>TargetLoading then 'Lewat'  else 'OK' end end Status2 " +
        //    // "case when Status2>TargetLoading then 'Lewat'  else 'OK' end end StatusLoading " +
        //    //"from(select A.ID,A.UrutanNo,case when A.MobilSendiri=0 then 'BPAS' else '' end Ket,A.MobilSendiri,A.TimeIn,A.TimeOut,A.NoPolisi,B.JenisMobil,B.Target as TargetLoading, " +
        //    //"case when A.MobilSendiri=0 then  " +
        //    //           //--TimeIn jam 3 s/d 9 = Rit1
        //    //"case when ( ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) >=180 AND ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<=540 ) " +
        //    //"then 1 else 2 end else 1 end Rit1, " +
        //    //"case when A.MobilSendiri=0 then " +
        //    //"case when datediff(DAY,A.TimeIn,A.TimeOut)=0 then " +
        //    //           //--jk timeIn kurang dari 6.30 dan keluar lewat 6.30
        //    //"case when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)<=390 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) >390 " +
        //    //           //--jk TimeIn lebih 6.30 dan TimeOut lebih dari 6.30
        //    //" then  case	when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)>390 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>390 " +
        //    //           //--jika dateng lewat dari 6.30
        //    //"then case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= 780 " +
        //    //           //--jika keluar antara jam 12 s/d 13 maka 12
        //    //"then 720 - ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))  " +
        //    //"else case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780 " +
        //    //           //--jika keluar lewat jam 13
        //    //"then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) " +
        //    //           //-- +60 gak term asuk jam istirahat
        //    //"else case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 540 " +
        //    //           //--jika keluar lewat jam 9 " +
        //    //"then ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) " +
        //    //           //--kelaru sebelum jam 12 dari 6.30 mobilsendiri
        //    //"else ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) " +
        //    //"end end " +
        //    //           //-- +60 utk kurangi jam istirahat
        //    //"end else " +
        //    //"case when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)<=390 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>390 " +
        //    //"then 999 " +
        //    //"end end else " +
        //    //"case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= 780 " +
        //    //           //--mobil sendiri  keluar jam istirahat
        //    //"then 720 - ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))  " +
        //    //"else " +
        //    //"case when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)<=390 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= 390 " +
        //    //"then ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) " +
        //    //"else " +
        //    //"((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) " +
        //    //"end	end	end	else 999999	end else " +
        //    //           //--mobil luar
        //    //"case when datediff(DAY,A.TimeIn,A.TimeOut)=0 then " +
        //    //"case when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)>="+WaktuAwal+" " +
        //    //           //--jika lewat jam 7 " +
        //    //"then case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= 780 " +
        //    //           //--jika keluar antara jam 12 s/d 13 maka 12
        //    //"then 720 - ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))  " +
        //    //"else case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780 and ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))>780 " +
        //    //           //--jika keluar lewat jam 13	 			
        //    //"then case when ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) > 720 and ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) <= 780 " +
        //    //"then ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) - 780 " +
        //    //"else (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- (((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))) " +
        //    //           //-- +60 gak term asuk jam istirahat " +
        //    //"end	else  case when ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))<720 " +
        //    //"then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn) ) " +
        //    //"when  ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780 " +
        //    //"then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)+60 ) " +
        //    //"else (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))-  ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) " +
        //    //           //   --kelaru sebelum jam 12 
        //    //"end	end " +
        //    //           //-- +60 utk kurangi jam istirahat
        //    //"end else " +
        //    //"case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= 780 " +
        //    //"then 720 - ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))  " +
        //    //"else ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-"+WaktuAwal+"	end	" +
        //    //"end	else 999 end " +
        //    //"end Status2 " +
        //    //"from LoadingTime as A, MasterKendaraan as B where A.KendaraanID=B.ID and A.TimeOut is not null and A.TimeOut>A.TimeIn " +
        //    //"and Convert(varchar,A.TimeIn,112) >= '" + drTgl + "' and Convert(varchar,A.TimeIn,112) <= '" + sdTgl + "'  ) as C order by MobilSendiri,TimeIn ";
        //    #endregion
        //    #region Query Baru
        //    string strSql01 =
        //    " select *,case when Sts=999 then ((DATEPART(HOUR,Data1.TimeOut)*60)+DATEPART(MINUTE,Data1.TimeOut))-((DATEPART(HOUR,Data1.TimeIn)*60)+DATEPART(MINUTE,Data1.TimeIn)) else Sts end Status2 from ( " +
        //                  "/*--gak liat Rit */" +
        //                  "  select C.*, " +
        //                  "  case when Sts>TargetLoading then 'Lewat'  else 'OK'  " +
        //                  "  end StatusLoading," +
        //                  "  case when MobilSendiri=0 then case when Sts=999 then 'Lewat' else  " +
        //                  "      case when Sts>TargetLoading then " +
        //                  "          case when Rit1=1 then 'Lewat' else 'OK' end " +
        //                  "      else case when Rit1=1 then 'OK' else 'Lewat' end end end " +
        //                  "  else " +
        //                  "       case when Sts=999 then 'Lewat' else  case when Sts>TargetLoading then 'Lewat' else 'OK' end end " +
        //                  "  end StatusL," +
        //                  "  case when MobilSendiri=0 then " +
        //                  "      case when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) <=" + WaktuAwal + " AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))<=" + WaktuAwal + " ) " +
        //                  "          then 'OK' " +
        //                  "      when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) <=" + WaktuAwal + " AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))<540 )		" +
        //                  "          then 'Lewat' " +
        //                  "      when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) <=" + WaktuAwal + " AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))>540 ) " +
        //                  "          then 'Lewat' " +
        //                  "      when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) >" + WaktuAwal + " AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))<=540 ) " +
        //                  "          then 'Lewat2' " +
        //                  "      when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) >" + WaktuAwal + " AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))>540 and Sts<TargetLoading ) " +
        //                  "          then 'OK2' " +
        //                  "      when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) >" + WaktuAwal + " AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))>540 and Sts>TargetLoading ) " +
        //                  "          then 'Lewat3' " +
        //                  "      when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) > 540 and Sts<TargetLoading ) " +
        //                  "          then 'OK3' " +
        //                  "      end " +
        //                  "  else " +
        //                  "      case when Sts>TargetLoading then 'Lewat'  else 'OK' end  " +

        //                  "  end Status1  " +


        //                  "  from(select A.ID,A.UrutanNo,case when A.MobilSendiri=0 then 'BPAS' else '' end Ket  " +
        //                  "  ,A.MobilSendiri,A.TimeIn,A.TimeOut,A.NoPolisi,B.JenisMobil,B.Target as TargetLoading,  " +
        //                  "  case when A.MobilSendiri=0 then  " +
        //                  " /*--TimeIn jam 3 s/d 9 = Rit1 */ " +
        //                  "      case when ( ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) >=180 AND ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<=540 )  " +
        //                  "          then 1 else 2 end  " +
        //                  "  else 1 end Rit1,  " +

        //                  "  case when A.MobilSendiri=0 then  " +
        //                  "      case when datediff(DAY,A.TimeIn,A.TimeOut)=0 then  " +
        //                  "         /* --jk timeIn kurang dari 6.30 dan keluar lewat 6.30  */" +
        //                  "          case when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)<=" + WaktuAwal + " and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) >" + WaktuAwal +
        //                  "              /*--jk TimeIn lebih 6.30 dan TimeOut lebih dari 6.30 */ " +
        //                  "               then  case	when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)> " + WaktuAwal + " and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>" + WaktuAwal +
        //                  "                     /* --jika dateng lewat dari 6.30*/  " +
        //                  "                      then case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= 780  " +
        //                  "	                          /*  --jika keluar antara jam 12 s/d 13 maka 12 */ " +
        //                  " 	                            then 720 - ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))   " +
        //                  "                          else  " +
        //                  "	                            case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780 " +
        //                  "                         /*--jika keluar lewat jam 13 */ " +
        //                  "		                            then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))  " +
        //                  "                         /*-- +60 gak term asuk jam istirahat */ " +
        //                  "	                            else  " +
        //                  "		                            case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 540 " +
        //                  "                         /*--jika keluar lewat jam 9   */" +
        //                  "		                            then ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))  " +
        //                  "                             /*--kelaru sebelum jam 12 dari 6.30 mobilsendiri */ " +
        //                  "		                            else ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))  " +
        //                  "		                            end  " +
        //                  "	                            end " +
        //                  "                             /*-- +60 utk kurangi jam istirahat */ " +
        //                  "                          end  " +
        //                  "                      else  " +
        //                  "                          case when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)<=390 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>390	 " +
        //                  "	                             then 999  " +
        //                  "                          end		 " +
        //                  "                      end  " +
        //                  "          else  " +
        //                  "              case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= 780  " +
        //                  "                      /*--mobil sendiri  keluar jam istirahat */ " +
        //                  "                      then 720 - ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))   " +
        //                  "                  else		 " +
        //                  "                      case when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)<=" + WaktuAwal + " and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= " + WaktuAwal + "  " +
        //                  "                          then ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))  " +
        //                  "                      /*--jk masuk antara jam 12 dan 13, 16Sept2014  */" +
        //                  "                      when ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))<720  " +
        //                  "                          then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn) )   " +
        //                  "                      when  ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780  " +
        //                  "                          then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)+60 )   " +
        //                  "                      when  ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))>=720 and ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<=780  " +
        //                  "                      and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780  " +
        //                  "                          then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- 780   " +
        //                  "                     /* --jk masuk antara jam 12 dan 13, 16Sept2014 */ " +
        //                  "                      else	 " +
        //                  "                          ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))  " +
        //                  "                      end  " +
        //                  "                  end  " +
        //                  "          end  " +
        //                  "      else  " +
        //                  "          999999  " +
        //                  "      end  " +
        //                  "  else  " +
        //                  "      /*--mobil luar */ " +
        //                  "      case when datediff(DAY,A.TimeIn,A.TimeOut)=0 then  " +
        //                  "          case when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)>=" + WaktuAwal +
        //                  "         /*--jika lewat jam 7 */ " +
        //                  "          then  " +
        //                  "              case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= 780  " +
        //                  "              /*--jika keluar antara jam 12 s/d 13 maka 12*/  " +
        //                  "                  then 720 - ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))   " +
        //                  "              else  " +
        //                  "                  case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780 and ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))>780 " +

        //                  "             /*--jika keluar lewat jam 13 */ " +
        //                  "                  then case when ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) > 720 and ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) <= 780  " +
        //                  "                       then ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) - 780  " +
        //        // Added tanggal 12 Oktober 2018 Oleh Beny , istirahat jam 6 sore tidak di hitung
        //                  "                 /*-- Added tanggal 12 Oktober 2018 Oleh Beny */ " +
        //                  "                 /*-- Untuk Mobil yg keluar diantara jam 18:00 dan jam 19:00 */ " +
        //                  "                      when ((DATEPART(HOUR,A.TimeOut)*60))>=1080 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))<=1140 " +
        //                  "                      then 1080 -  ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) " +
        //                  "                 /*-- Untuk Mobil yg masuk dibawah jam 18:00 dan keluar diatas jam 19:00 */ " +
        //                  "                      when ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeOut))<1080 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>1140 " +
        //                  "                      then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) -  ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)))-60  " +
        //                  "                 /*-- End Added */ " +
        //        // end Added
        //                  "                       else (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- (((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))) " +
        //                  "                 /*-- +60 gak term asuk jam istirahat */ " +
        //                  "                  end  " +
        //                  "                  else  case when ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))<720   " +
        //                  "                          then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn) )  " +
        //                  "                        when  ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780  " +
        //                  "                          then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)+60 )  " +
        //                  "                      /*  --jk masuk antara jam 12 dan 13, 16Sept2014 */ " +
        //                  "                        when  ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))>=720 and ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<=780  " +
        //                  "                         and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780  " +
        //                  "                          then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- 780   " +
        //                  "                      /*  --jk masuk antara jam 12 dan 13 */ " +
        //                  "                        else (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))-  ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))  " +
        //                  "                      /*  --kelaru sebelum jam 12*/  " +
        //                  "                        end  " +
        //                  "                  end   " +
        //                  "                /* -- +60 utk kurangi jam istirahat*/ " +
        //                  "              end  " +
        //                  "          else  " +
        //                  "              case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= 780  " +
        //                  "              then 720 - ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))  " +
        //                  "              else  " +
        //                  "              ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-" + WaktuAwal +
        //                  "             end	 " +
        //                  "          end  " +
        //                  "      else  " +
        //                  "          999  " +
        //                  "      end  " +
        //                  "  end Sts  " +

        //                  "  from LoadingTime as A, MasterKendaraan as B where A.KendaraanID=B.ID and A.TimeOut is not null and A.TimeOut>A.TimeIn  " +
        //                  "  and Convert(varchar,A.TimeIn,112) >= '" + drTgl + "' and Convert(varchar,A.TimeIn,112) <= '" + sdTgl + "' ) as C ) as Data1 order by MobilSendiri,TimeIn";



        //    #endregion
        //    #region Baru Lagi
        //    string A = string.Empty; string B = string.Empty;

        //    if (Convert.ToInt32(drTgl.Substring(0,6)) > Convert.ToInt32("201911"))
        //    {
        //        A = " when /**UrutanNo<=100**/ Tujuan='DPulau' and MobilSendiri=1 and hari<>'Saturday' and (DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)>1260 " +
        //            " then ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut)-360) " +
        //            " when /**UrutanNo<=100**/ Tujuan='DPulau' and MobilSendiri=1 and hari='Saturday' and (DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)>900 " +
        //            " then ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut)-360) " +
        //            " when /**UrutanNo>100**/ Tujuan='LPulau' and MobilSendiri=1 and hari<>'Saturday' and (DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)>1080 " +
        //            " then ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut)-360) " +
        //            " when /**UrutanNo>100**/ Tujuan='LPulau' and MobilSendiri=1 and hari='Saturday' and (DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)>720 " +
        //            " then ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut)-360) ";
        //        B = " ,Tujuan ";
        //    }
        //    else
        //    {
        //        A = " "; B = " ";
        //    }

        //    string strSql =
        //        //" select *,case when Status2>TargetLoading then 'Lewat'  else 'OK'    end StatusLoading " +

        //    //Revisi 22 April 2019
        //    " select *,case when Tujuan='DPulau' then 'Dalam Pulau' when Tujuan='LPulau' then 'Luar Pulau' else '-' end Tujuan2" +
        //    " ,case  " +
        //    " when Status2>TargetLoading and Ket<>'BPAS' and Rit1=1 then 'Lewat' " +
        //    " when Ket='BPAS' and Rit1=1 and ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn))>390 then 'Lewat' "+
        //    " when Ket='BPAS' and Rit1=1 and ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))<=390 and Status2>TargetLoading then 'Lewat' "+
        //    " when Ket='BPAS' and Rit1=1 and ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))<=390 and Status2<=TargetLoading then 'OK' "+
        //    " when Ket='BPAS' and Rit1=1 and (DATEPART(HOUR,TimeIn)*60)<=390 and ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))>390 then 'Lewat' " +
        //    " when ket='BPAS' and Rit1>1 and Status2>TargetLoading then 'Lewat' "+
        //    " when TimeIn=[TimeOut] then 'TimeIn=TimeOut' " +
        //    " else 'OK'    end StatusLoading " +
        //        // End Revisi

        //    " from ( " +
        //    " select *,case " +

        //    /** Perhitungan yang beda Hari TimeIn dan TimeOut **/
        //    " when MobilSendiri=1 and left(convert(char,timein,112),8)<>left(convert(char,timeout,112),8) then " +
        //    " (select ((((((DATEPART(HOUR,'23:59:00.000')*60) /60) +1) - ((DATEPART(HOUR,TimeIN)*60) / 60))*60)+ (DATEPART(MINUTE,TimeIN))) " +
        //    " + ((DATEPART(HOUR,TimeOut)*60) + (DATEPART(MINUTE,TimeOut)))) " +

        //    /** Perhitungan u/ Armada luar BPAS dgn Tujuan dalam kota / Depo  
        //     *  1. Range No Kartu Proxy : 1 - 100 per Tanggal 31-01-2020 DiAbaikan, tidak melihat penomoran Kartu
        //     *  2. Range Proses Loading Senin - Jumat : 06:00 - 21:00 , Sabtu : 06:00 - 15:00 
        //     *  3. Ketika Proxy Masuk diatas Jam 21:00 maka proses perhitungan dimulai Jam : 06:00 ( Senin - Jumat )
        //     *  4. Ketika Proxy Masuk diatas Jam 15:00 maka proses perhitungan dimulai Jam : 06:00 ( Sabtu )
        //     *  **/
        //    ""+A+""+
        //    //" when /**UrutanNo<=100**/ Tujuan='Depo' and MobilSendiri=1 and hari<>'Saturday' and (DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)>1260 " +
        //    //" then ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut)-360) " +
        //    //" when /**UrutanNo<=100**/ Tujuan='Depo' and MobilSendiri=1 and hari='Saturday' and (DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)>900 " +
        //    //" then ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut)-360) " +

        //    /** Perhitungan u/ Armada luar BPAS dgn Tujuan luar Pulau  
        //     *  1. Range No Kartu Proxy : > 100 per Tanggal 31-01-2020 Diabaikan, tidak melihat penomoran Kartu
        //     *  2. Range Proses Loading Senin - Jumat : 06:00 - 18:00 , Sabtu : 06:00 - 12:00 
        //     *  3. Ketika Proxy Masuk diatas Jam 18:00 maka proses perhitungan dimulai Jam : 06:00 ( Senin - Jumat )
        //     *  4. Ketika Proxy Masuk diatas Jam 12:00 maka proses perhitungan dimulai Jam : 06:00 ( Sabtu )
        //     *  **/
        //    //" when /**UrutanNo>100**/ Tujuan='LPulau' and MobilSendiri=1 and hari<>'Saturday' and (DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)>1080 " +
        //    //" then ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut)-360) " +
        //    //" when /**UrutanNo>100**/ Tujuan='LPulau' and MobilSendiri=1 and hari='Saturday' and (DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)>720 " +
        //    //" then ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut)-360) " +


        //    " when Hari='Friday' and ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn))<=690 and ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))>780 then ((((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))-90)- ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn))) " +
        //    " when Hari='Friday' and ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn))>690 and ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn))<780 then (((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))-90)- 690 " +
        //    " when Hari='Friday' and ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))>=690 and ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))<=780 then  " +
        //    " 690 - ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) " +
        //    " when (LEFT(convert(char,timein,112),8))<(LEFT(convert(char,TimeOut,112),8)) then (1440 - ((DATEPART(HOUR,timein)*60)+DATEPART(MINUTE,timein))) + ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut)) " +
        //    " else Status22 end Status2  from( " +
        //    " select *,case when Sts=999 then ((DATEPART(HOUR,Data1.TimeOut)*60)+DATEPART(MINUTE,Data1.TimeOut))-((DATEPART(HOUR,Data1.TimeIn)*60)+DATEPART(MINUTE,Data1.TimeIn)) else Sts end Status22 from ( " +

        //    /*--gak liat Rit */
        //    " select C.*, " +
        //    " case when Sts>TargetLoading then 'Lewat'  else 'OK'    end StatusLoading_lama, " +
        //    " case when MobilSendiri=0 then case when Sts=999 then 'Lewat' else case when Sts>TargetLoading then case when Rit1=1 then 'Lewat' else 'OK' end else case when Rit1=1 then 'OK' else 'Lewat' end end end else case when Sts=999 then 'Lewat' else  case when Sts>TargetLoading then 'Lewat' else 'OK' end end end StatusL, " +
        //    " case when MobilSendiri=0 then case when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) <=390 AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))<=390 ) then 'OK' " +
        //    " when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) <=390 AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))<540 )then 'Lewat' when (((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) <=390 AND " +
        //    " ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))>540 ) then 'Lewat' when (((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) >390 AND " +
        //    " ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))<=540 ) then 'Lewat2' when (((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) >390 AND " +
        //    " ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))>540 and Sts<TargetLoading )    " +
        //    " then 'OK2' when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) >390 AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))>540 and Sts>TargetLoading ) " +
        //    " then 'Lewat3' when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) > 540 and Sts<TargetLoading ) then 'OK3' end   else " +
        //    " case when Sts>TargetLoading then 'Lewat'  else 'OK' end    end Status1 ,DATENAME(WEEKDAY,TimeIn)Hari  " +
        //    " from(select A.ID,A.UrutanNo,case when A.MobilSendiri=0 then 'BPAS' else '' end Ket,A.MobilSendiri,A.TimeIn,A.TimeOut,A.NoPolisi," +
        //    " B.JenisMobil,B.Target as TargetLoading,case when A.MobilSendiri=0 then  " +

        //    /*--TimeIn jam 3 s/d 9 = Rit1 */
        //    " case when ( ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) >=180 AND ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<=540 )then 1 else 2 end    else 1 end Rit1, " +
        //    " case when A.MobilSendiri=0 then case when datediff(DAY,A.TimeIn,A.TimeOut)=0 then  " +

        //    /* --jk timeIn kurang dari 6.30 dan keluar lewat 6.30  */
        //    " case when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)<=390 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) >390 " +

        //    /*--jk TimeIn lebih 6.30 dan TimeOut lebih dari 6.30 */
        //    " then  case	when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)> 390 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>390   " +

        //    /* --jika dateng lewat dari 6.30*/
        //    " then case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= 780 " +

        //    /*  --jika keluar antara jam 12 s/d 13 maka 12 */
        //    " then 720 - ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) else case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780  " +

        //    /*--jika keluar lewat jam 13 */
        //    " then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))   " +

        //    /*-- +60 gak term asuk jam istirahat */
        //    " else case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 540  " +

        //    /*--jika keluar lewat jam 9   */
        //    " then ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) " +

        //    /*--kelaru sebelum jam 12 dari 6.30 mobilsendiri */
        //    " else ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) end end " +

        //    /*-- +60 utk kurangi jam istirahat */
        //    " end else case when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)<=390 and ((DATEPART(HOUR,A.TimeOut)*60)+" +
        //    " DATEPART(MINUTE,A.TimeOut))>390 then 999 end end else case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 720 and " +
        //    " ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= 780  " +

        //    /*--mobil sendiri  keluar jam istirahat */
        //    " then 720 - ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) else case when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)<=390 " +
        //    " and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= 390 then ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))" +
        //    " -((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) " +

        //    /*--jk masuk antara jam 12 dan 13, 16Sept2014  */
        //    " when ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))<720 then " +
        //    " (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn) ) " +
        //    " when  ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780 " +
        //    " then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)+60 ) " +
        //    " when  ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))>=720 and ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<=780 " +
        //    " and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780 then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- 780  " +

        //    /* --jk masuk antara jam 12 dan 13, 16Sept2014 */
        //    " else ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) end end " +
        //    " end else 999999 end else " +

        //    /*--mobil luar */
        //    " case when datediff(DAY,A.TimeIn,A.TimeOut)=0 then case when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)>=390   " +

        //    /*--jika lewat jam 7 */
        //    " then case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= 780   " +

        //    /*--jika keluar antara jam 12 s/d 13 maka 12*/
        //    " then 720 - ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) else case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780 " +
        //    " and ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))>780    " +

        //    /*--jika keluar lewat jam 13 */
        //    " then case when ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) > 720 and ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))" +
        //    "  <= 780 then ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) - 780  " +

        //    /*-- Added tanggal 12 Oktober 2018 Oleh Beny */
        //        /*-- Untuk Mobil yg keluar diantara jam 18:00 dan jam 19:00 */
        //    " when ((DATEPART(HOUR,A.TimeOut)*60))>=1082 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))<=1140 then " +
        //    " 1080-((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))         " +

        //    /*-- Untuk Mobil yg masuk dibawah jam 18:00 dan keluar diatas jam 19:00 */
        //    " when ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeOut))<1080 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>1140 " +
        //    " then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) -  ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)))-60 " +

        //    /*-- End Added */
        //    " else (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- (((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)))   " +

        //    /*-- +60 gak termasuk jam istirahat */
        //    " end else case " +
        //    " when ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))<720 " +
        //    " then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn) )  " +
        //    " when ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780 " +
        //    " then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)+60 ) " +

        //    /*  --jk masuk antara jam 12 dan 13, 16Sept2014 */
        //    " when ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))>=720 and ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<=780 " +
        //    " and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780 then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- 780 " +

        //    /*  --jk masuk antara jam 12 dan 13 */
        //    " else (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))-  ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) " +

        //    /*  --keluar sebelum jam 12*/
        //    " end end " +

        //     /* -- +60 utk kurangi jam istirahat*/
        //    " end else case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 720 and ((DATEPART(HOUR,A.TimeOut)*60)+ " +
        //    " DATEPART(MINUTE,A.TimeOut)) <= 780   then 720 - ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) " +
        //    " when ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) >=0 and ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) <= 360 " +
        //    " then ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) - ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) " +
           
        //    /** Perbaikan karena ada minus di loading time Jombang **/
        //    //" else ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-390 "+
        //    "  when A.MobilSendiri=1 then  "+
        //    " ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) "+
        //    " else "+
        //    " ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-390  "+

        //    " end	end else        999 end end Sts "+B+" " +
        //    " from LoadingTime as A, MasterKendaraan as B  " +
        //    " where A.KendaraanID=B.ID  " +
        //    " and Convert(varchar,A.TimeOut,112) >= '" + drTgl + "' and Convert(varchar,A.TimeOut,112) <= '" + sdTgl + "'  ) as C ) as Data1 ) as Data2 ) as Data3 " +
        //    " order by MobilSendiri,TimeIn ";

        //    #endregion
        //    return strSql;
        //}

        public string ViewLapLoadingTime(string drTgl, string sdTgl, int KendaraanID, int WaktuAwal, string query1)
        {

            string strSql =
" ;with  " +
" data_awal as ( " +
" select case when ((DATEPART(HOUR,TimeDaftar)*60)+DATEPART(MINUTE,TimeDaftar)) >= 1140 then '0' else Ritase1 end Ritase,* from ( " +
" select ROW_NUMBER() OVER(PARTITION BY left(convert(char,TimeIn,112),8),A.NoPolisi ORDER BY TimeIn asc) as Ritase1,A.ID,isnull(C.Time1,'')TimeDaftar,TimeIn,TimeOut,DATEPART(dw,TimeIn)-1 as Hari0,isnull(Tujuan,'-')Tujuan,DATEPART(DAY,TimeIn)Day1,DATEPART(DAY,TimeOut)Day2,A.NoPolisi,UrutanNo,MobilSendiri,EkspedisiName,KendaraanID,(select top 1 Flag from MasterKendaraan_periode where left(convert(char,TimeIn,112),6)>=left(convert(char,Periode1,112),6) and left(convert(char,TimeIn,112),6)<=left(convert(char,Periode2,112),6))Flag,case when  ((DATEPART(HOUR,C.Time1)*60)+DATEPART(MINUTE,C.Time1))>=1140 and A.MobilSendiri=0 then '99' else '0' end Flag2  " +
" from LoadingTime A left join MasterKendaraan B ON A.KendaraanID=B.ID left join LoadingTimeEst C ON A.ID=C.LoadingID  " +
                //" left(convert(char,timein,112),6)='202110') as x), " +
" where A.Status>-1 and left(convert(char,TimeIn,112),8)>='" + drTgl + "' and left(convert(char,TimeIn,112),8)<='" + sdTgl + "') as x), " +
"  " +
" data_awal0 as (select *,case when Hari0=1 then 'Senin' when Hari0='2' then 'Selasa' when Hari0='3' then 'Rabu' when Hari0='4' then 'Kamis' when Hari0='5' then 'Jumat' when Hari0='6' then 'Sabtu' when Hari0='0' then 'Minggu' end Hari from data_awal), " +


" data_100 as ( " +
" select A.*,B.JenisMobil,case  " +
" when Day1<>Day2 then (1440-((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn))) + (DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut) " +
" when MobilSendiri=0 and Ritase1>1 and Hari='Jumat' and ((DATEPART(HOUR,timein)*60)+DATEPART(MINUTE,timein))<='690' and ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))>='780' then  " +
" ((((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))-((DATEPART(HOUR,timein)*60)+DATEPART(MINUTE,timein))-90)) " +
" when MobilSendiri=1 and Hari='Jumat' and ((DATEPART(HOUR,timein)*60)+DATEPART(MINUTE,timein))<='690' and ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))>='780' then  " +
" ((((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))-((DATEPART(HOUR,timein)*60)+DATEPART(MINUTE,timein))-90)) " +
" when MobilSendiri=1 and Hari='Jumat' and ((DATEPART(HOUR,timein)*60)+DATEPART(MINUTE,timein))<='690' and ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))>='690' and ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))<='780' then (( 690 - ((DATEPART(HOUR,timein)*60)+DATEPART(MINUTE,timein)))) " +
" when Ritase1>1 and Hari<>'Jumat' and ((DATEPART(HOUR,timein)*60)+DATEPART(MINUTE,timein))<='720' and ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))>='780' then  " +
" ((((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))-((DATEPART(HOUR,timein)*60)+DATEPART(MINUTE,timein))-60)) " +
" when MobilSendiri=1 and Hari<>'Jumat' and ((DATEPART(HOUR,timein)*60)+DATEPART(MINUTE,timein))<='720' and ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))>='780' then  " +
" ((((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))-((DATEPART(HOUR,timein)*60)+DATEPART(MINUTE,timein))-60)) " +
" when (DATEPART(HOUR,timein)*60)+DATEPART(MINUTE,timein)<='1080' and (DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut)>='1140' then  " +
" ((((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))-((DATEPART(HOUR,timein)*60)+DATEPART(MINUTE,timein))-60)) " +
" else " +
" ((((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))-((DATEPART(HOUR,timein)*60)+DATEPART(MINUTE,timein))))  end WaktuLoading0 , " +
"  " +
" case when Day1=Day2 then ((DATEPART(HOUR,timein)*60)+DATEPART(MINUTE,timein)) when Day1<>Day2 then (1440-((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn))) end WaktuMasuk,  " +
" ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut)) WaktuKeluar " +
" from data_awal0 A inner join MasterKendaraan B ON A.KendaraanID=B.ID    ), " +
"  " +
" data_200 as ( " +
" select case when MobilSendiri=0 then case  " +
" when Hari<>'Senin' and JenisMobil not like'%TRONTON%' and JenisMobil not like'%FUSO%'  and WaktuMasuk<=390 and WaktuKeluar<=390 then WaktuKeluar-WaktuMasuk " +
" when Hari<>'Senin' and JenisMobil not like'%TRONTON%' and JenisMobil not like'%FUSO%' and WaktuKeluar>390 then  '9991' " +
" when Hari='Senin' and JenisMobil not like'%TRONTON%' and JenisMobil not like'%FUSO%' and WaktuMasuk<=510 and WaktuKeluar<=510 then WaktuKeluar-WaktuMasuk " +
" when Hari='Senin' and JenisMobil not like'%TRONTON%' and JenisMobil not like'%FUSO%' and WaktuKeluar>510 then  '9993' " +
" when JenisMobil like'%TRONTON%' and JenisMobil not like'%FUSO%' then  WaktuKeluar-WaktuMasuk " +
" else WaktuKeluar-WaktuMasuk " +
" end " +
" when MobilSendiri<>0 and Day1<>Day2 then  WaktuKeluar+WaktuMasuk else WaktuKeluar-WaktuMasuk end WaktuLoading1,* from data_100 ), " +
"  " +
" data_300 as ( " +
" select B.LoadingID,B.Time1,A.* from data_200 A left join LoadingTimeEst B ON A.ID=B.LoadingID ), " +
"  " +
" data_400 as ( " +
" select case when B.ID>0 then 'Dispensasi' else '' end Noted2, " +
" case  " +
" when MobilSendiri>0 and Hari<>'Jumat' and Tujuan='DPulau' and ((DATEPART(HOUR,TimeDaftar)*60)+DATEPART(MINUTE,TimeDaftar)) > 1260 then 'Pinalti'  " +
" when MobilSendiri>0 and Hari<>'Jumat' and Tujuan='LPulau' and ((DATEPART(HOUR,TimeDaftar)*60)+DATEPART(MINUTE,TimeDaftar)) > 1080 then 'Pinalti'  " +
" when MobilSendiri>0 and Hari='Sabtu' and Tujuan='DPulau' and ((DATEPART(HOUR,TimeDaftar)*60)+DATEPART(MINUTE,TimeDaftar)) > 900 then 'Pinalti'  " +
" when MobilSendiri>0 and Hari='Sabtu' and Tujuan='LPulau' and ((DATEPART(HOUR,TimeDaftar)*60)+DATEPART(MINUTE,TimeDaftar)) > 720 then 'Pinalti'  " +
" when WaktuLoading1='9991' then 'Lewat Jam 6:30'  " +
" when WaktuLoading1='9993' then 'Lewat Jam 8:30'  " +
" else '' " +
" end Noted, " +
" case " +
" when MobilSendiri>0 and Hari<>'Sabtu' and Tujuan='DPulau' and ((DATEPART(HOUR,TimeDaftar)*60)+DATEPART(MINUTE,TimeDaftar)) > 1260 then '9992'  " +
" when MobilSendiri>0 and Hari='Sabtu' and Tujuan='DPulau' and ((DATEPART(HOUR,TimeDaftar)*60)+DATEPART(MINUTE,TimeDaftar)) > 900 then '9992'  " +
" when MobilSendiri>0 and Hari<>'Sabtu' and Tujuan='LPulau' and ((DATEPART(HOUR,TimeDaftar)*60)+DATEPART(MINUTE,TimeDaftar)) > 1080 then '9992'  " +
" when MobilSendiri>0 and Hari='Sabtu' and Tujuan='LPulau' and ((DATEPART(HOUR,TimeDaftar)*60)+DATEPART(MINUTE,TimeDaftar)) > 720 then '9992'  " +
" else WaktuLoading1 " +
" end WaktuLoading, " +
" A.* from data_300 A left join LoadingTime_Exc B ON left(convert(char,A.timein,112),8)=left(convert(char,B.Tanggal,112),8) and A.NoPolisi=B.NoPolisi and B.RowStatus>-1 ), " +
"  " +
" data_500 as ( " +
" select B.Target ,A.* from data_400 A left join MasterKendaraan B ON A.JenisMobil=B.JenisMobil and A.Flag=B.FLag), " +
"  " +
" data_600 as ( " +
" select Ritase,case  " +
" when Target>=WaktuLoading and Noted='Pinalti' and Noted2='' then 'Lewat'  " +
" when Target>=WaktuLoading and Noted='' and Noted2='' then 'OK'  " +
" when Target>=WaktuLoading  then 'OK'  " +
" when Target>=WaktuLoading0 and Noted='Pinalti' and Noted2='Dispensasi' then 'OK'  " +
" when Target>=WaktuLoading0 and Noted='Lewat Jam 6:30' and Noted2='Dispensasi' then 'OK' " +
" when Target>=WaktuLoading0 and Noted='Lewat Jam 8:30' and Noted2='Dispensasi' then 'OK' " +
" when Flag2='99' and MobilSendiri=0 and ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))>=1140 and ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))<=1440 then  'OK' " +
" when Flag2='0'  and Target>=WaktuLoading0 then 'OK'  " +
" when Target<WaktuLoading0 then 'Lewat' else '?'  " +
"  " +
" end StatusLoading,cast(Target as int)Target,Noted2,case when Noted2='' and Noted='Pinalti' then Noted+' (' +hari+ ') ' when Noted2='Dispensasi' then Noted+' ('+Noted2+')' " +
" when Noted='Lewat Jam 8:30' then Noted+' ('+hari+')' when Noted='Lewat Jam 6:30' then Noted+' ('+hari+')' when TimeIn=TimeOut then 'TimeIn=TimeOut' else Noted end Noted, " + 
"isnull(WaktuLoading,0)WaktuLoading,isnull(WaktuLoading0,0)WaktuLoading0,TimeDaftar,TimeIn,TimeOut,case when Tujuan='DPulau' then 'Dalam Pulau' when Tujuan='LPulau' then 'Luar Pulau' else '' " + 
"end Tujuan2,NoPolisi,UrutanNo,JenisMobil,case when MobilSendiri=0 then 'BPAS' else '' end Keterangan,MobilSendiri,Hari,Flag2,Day1,day2 from data_500 ) " +
"  " +
" select case when WaktuLoading0<0 then '?' else isnull(WaktuLoading,'') end Status,/**isnull(WaktuLoading,'') Status2,**/isnull(Target,0)Target,Noted2,case when Flag2='99' then 'Antaran Besok'  " +
"when Flag2='0' then '' else Noted end  Noted,case when Flag2='99' then WaktuLoading0 when Flag2=0 then isnull(WaktuLoading0,0) else isnull(WaktuLoading,0) end Status2,TimeDaftar,TimeIn,isnull(TimeOut,'')TimeOut, " +
"Tujuan2,NoPolisi,UrutanNo,JenisMobil,Keterangan,MobilSendiri,Hari,/*isnull(StatusLoading,'')*/ case when target < isnull(WaktuLoading0,0) then 'Lewat' else isnull( StatusLoading,'') end StatusLoading,Keterangan Ket " +


" " + query1 + "";


            return strSql;
        }

        public string ViewLapLoadingTime_asli(string drTgl, string sdTgl, int KendaraanID, int WaktuAwal)
        {
            #region Query Lama di nonaktifkan
            //       return " select C.*, " +
            //"case when Status2>TargetLoading then 'Lewat'  else 'OK' end Status, " +
            //           // "case when Status2>TargetLoading then 'Lewat'  else 'OK' end StatusLoading, " +
            //"case when MobilSendiri=0 then case when Status2=999 then 'Lewat' else " +
            //"case when Status2>TargetLoading then case when Rit1=1 then 'Lewat' else 'OK' end else case when Rit1=1 then 'OK' else 'Lewat' end end end else " +
            //"case when Status2=999 then 'Lewat' else  case when Status2>TargetLoading then 'Lewat' else 'OK' end end " +
            //"end StatusL, " +
            //"case when MobilSendiri=0 then " +
            //"case when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) <=390 AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))<=390 ) " +
            //"then 'OK' " +
            //"when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) <=390 AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))<540 )	" +
            //"then 'Lewat' " +
            //"when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) <=390 AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))>540 ) " +
            //"then 'Lewat' " +
            //"when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) >390 AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))<=540 ) " +
            //"then 'Lewat2' " +
            //"when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) >390 AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))>540 and Status2<TargetLoading ) " +
            //"then 'OK2' " +
            //"when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) >390 AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))>540 and Status2>TargetLoading ) " +
            //"then 'Lewat3' " +
            //"when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) > 540 and Status2<TargetLoading ) " +
            //"then 'OK3'" +
            //"end else " +
            //           //"case when Status2>TargetLoading then 'Lewat'  else 'OK' end end Status2 " +
            // "case when Status2>TargetLoading then 'Lewat'  else 'OK' end end StatusLoading " +
            //"from(select A.ID,A.UrutanNo,case when A.MobilSendiri=0 then 'BPAS' else '' end Ket,A.MobilSendiri,A.TimeIn,A.TimeOut,A.NoPolisi,B.JenisMobil,B.Target as TargetLoading, " +
            //"case when A.MobilSendiri=0 then  " +
            //           //--TimeIn jam 3 s/d 9 = Rit1
            //"case when ( ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) >=180 AND ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<=540 ) " +
            //"then 1 else 2 end else 1 end Rit1, " +
            //"case when A.MobilSendiri=0 then " +
            //"case when datediff(DAY,A.TimeIn,A.TimeOut)=0 then " +
            //           //--jk timeIn kurang dari 6.30 dan keluar lewat 6.30
            //"case when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)<=390 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) >390 " +
            //           //--jk TimeIn lebih 6.30 dan TimeOut lebih dari 6.30
            //" then  case	when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)>390 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>390 " +
            //           //--jika dateng lewat dari 6.30
            //"then case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= 780 " +
            //           //--jika keluar antara jam 12 s/d 13 maka 12
            //"then 720 - ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))  " +
            //"else case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780 " +
            //           //--jika keluar lewat jam 13
            //"then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) " +
            //           //-- +60 gak term asuk jam istirahat
            //"else case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 540 " +
            //           //--jika keluar lewat jam 9 " +
            //"then ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) " +
            //           //--kelaru sebelum jam 12 dari 6.30 mobilsendiri
            //"else ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) " +
            //"end end " +
            //           //-- +60 utk kurangi jam istirahat
            //"end else " +
            //"case when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)<=390 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>390 " +
            //"then 999 " +
            //"end end else " +
            //"case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= 780 " +
            //           //--mobil sendiri  keluar jam istirahat
            //"then 720 - ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))  " +
            //"else " +
            //"case when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)<=390 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= 390 " +
            //"then ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) " +
            //"else " +
            //"((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) " +
            //"end	end	end	else 999999	end else " +
            //           //--mobil luar
            //"case when datediff(DAY,A.TimeIn,A.TimeOut)=0 then " +
            //"case when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)>="+WaktuAwal+" " +
            //           //--jika lewat jam 7 " +
            //"then case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= 780 " +
            //           //--jika keluar antara jam 12 s/d 13 maka 12
            //"then 720 - ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))  " +
            //"else case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780 and ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))>780 " +
            //           //--jika keluar lewat jam 13	 			
            //"then case when ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) > 720 and ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) <= 780 " +
            //"then ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) - 780 " +
            //"else (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- (((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))) " +
            //           //-- +60 gak term asuk jam istirahat " +
            //"end	else  case when ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))<720 " +
            //"then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn) ) " +
            //"when  ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780 " +
            //"then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)+60 ) " +
            //"else (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))-  ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) " +
            //           //   --kelaru sebelum jam 12 
            //"end	end " +
            //           //-- +60 utk kurangi jam istirahat
            //"end else " +
            //"case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= 780 " +
            //"then 720 - ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))  " +
            //"else ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-"+WaktuAwal+"	end	" +
            //"end	else 999 end " +
            //"end Status2 " +
            //"from LoadingTime as A, MasterKendaraan as B where A.KendaraanID=B.ID and A.TimeOut is not null and A.TimeOut>A.TimeIn " +
            //"and Convert(varchar,A.TimeIn,112) >= '" + drTgl + "' and Convert(varchar,A.TimeIn,112) <= '" + sdTgl + "'  ) as C order by MobilSendiri,TimeIn ";
            #endregion
            #region Query Baru
            string strSql01 =
            " select *,case when Sts=999 then ((DATEPART(HOUR,Data1.TimeOut)*60)+DATEPART(MINUTE,Data1.TimeOut))-((DATEPART(HOUR,Data1.TimeIn)*60)+DATEPART(MINUTE,Data1.TimeIn)) else Sts end Status2 from ( " +
                          "/*--gak liat Rit */" +
                          "  select C.*, " +
                          "  case when Sts>TargetLoading then 'Lewat'  else 'OK'  " +
                          "  end StatusLoading," +
                          "  case when MobilSendiri=0 then case when Sts=999 then 'Lewat' else  " +
                          "      case when Sts>TargetLoading then " +
                          "          case when Rit1=1 then 'Lewat' else 'OK' end " +
                          "      else case when Rit1=1 then 'OK' else 'Lewat' end end end " +
                          "  else " +
                          "       case when Sts=999 then 'Lewat' else  case when Sts>TargetLoading then 'Lewat' else 'OK' end end " +
                          "  end StatusL," +
                          "  case when MobilSendiri=0 then " +
                          "      case when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) <=" + WaktuAwal + " AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))<=" + WaktuAwal + " ) " +
                          "          then 'OK' " +
                          "      when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) <=" + WaktuAwal + " AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))<540 )		" +
                          "          then 'Lewat' " +
                          "      when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) <=" + WaktuAwal + " AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))>540 ) " +
                          "          then 'Lewat' " +
                          "      when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) >" + WaktuAwal + " AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))<=540 ) " +
                          "          then 'Lewat2' " +
                          "      when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) >" + WaktuAwal + " AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))>540 and Sts<TargetLoading ) " +
                          "          then 'OK2' " +
                          "      when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) >" + WaktuAwal + " AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))>540 and Sts>TargetLoading ) " +
                          "          then 'Lewat3' " +
                          "      when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) > 540 and Sts<TargetLoading ) " +
                          "          then 'OK3' " +
                          "      end " +
                          "  else " +
                          "      case when Sts>TargetLoading then 'Lewat'  else 'OK' end  " +

                          "  end Status1  " +


                          "  from(select A.ID,A.UrutanNo,case when A.MobilSendiri=0 then 'BPAS' else '' end Ket  " +
                          "  ,A.MobilSendiri,A.TimeIn,A.TimeOut,A.NoPolisi,B.JenisMobil,B.Target as TargetLoading,  " +
                          "  case when A.MobilSendiri=0 then  " +
                          " /*--TimeIn jam 3 s/d 9 = Rit1 */ " +
                          "      case when ( ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) >=180 AND ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<=540 )  " +
                          "          then 1 else 2 end  " +
                          "  else 1 end Rit1,  " +

                          "  case when A.MobilSendiri=0 then  " +
                          "      case when datediff(DAY,A.TimeIn,A.TimeOut)=0 then  " +
                          "         /* --jk timeIn kurang dari 6.30 dan keluar lewat 6.30  */" +
                          "          case when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)<=" + WaktuAwal + " and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) >" + WaktuAwal +
                          "              /*--jk TimeIn lebih 6.30 dan TimeOut lebih dari 6.30 */ " +
                          "               then  case	when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)> " + WaktuAwal + " and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>" + WaktuAwal +
                          "                     /* --jika dateng lewat dari 6.30*/  " +
                          "                      then case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= 780  " +
                          "	                          /*  --jika keluar antara jam 12 s/d 13 maka 12 */ " +
                          " 	                            then 720 - ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))   " +
                          "                          else  " +
                          "	                            case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780 " +
                          "                         /*--jika keluar lewat jam 13 */ " +
                          "		                            then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))  " +
                          "                         /*-- +60 gak term asuk jam istirahat */ " +
                          "	                            else  " +
                          "		                            case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 540 " +
                          "                         /*--jika keluar lewat jam 9   */" +
                          "		                            then ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))  " +
                          "                             /*--kelaru sebelum jam 12 dari 6.30 mobilsendiri */ " +
                          "		                            else ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))  " +
                          "		                            end  " +
                          "	                            end " +
                          "                             /*-- +60 utk kurangi jam istirahat */ " +
                          "                          end  " +
                          "                      else  " +
                          "                          case when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)<=390 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>390	 " +
                          "	                             then 999  " +
                          "                          end		 " +
                          "                      end  " +
                          "          else  " +
                          "              case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= 780  " +
                          "                      /*--mobil sendiri  keluar jam istirahat */ " +
                          "                      then 720 - ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))   " +
                          "                  else		 " +
                          "                      case when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)<=" + WaktuAwal + " and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= " + WaktuAwal + "  " +
                          "                          then ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))  " +
                          "                      /*--jk masuk antara jam 12 dan 13, 16Sept2014  */" +
                          "                      when ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))<720  " +
                          "                          then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn) )   " +
                          "                      when  ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780  " +
                          "                          then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)+60 )   " +
                          "                      when  ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))>=720 and ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<=780  " +
                          "                      and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780  " +
                          "                          then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- 780   " +
                          "                     /* --jk masuk antara jam 12 dan 13, 16Sept2014 */ " +
                          "                      else	 " +
                          "                          ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))  " +
                          "                      end  " +
                          "                  end  " +
                          "          end  " +
                          "      else  " +
                          "          999999  " +
                          "      end  " +
                          "  else  " +
                          "      /*--mobil luar */ " +
                          "      case when datediff(DAY,A.TimeIn,A.TimeOut)=0 then  " +
                          "          case when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)>=" + WaktuAwal +
                          "         /*--jika lewat jam 7 */ " +
                          "          then  " +
                          "              case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= 780  " +
                          "              /*--jika keluar antara jam 12 s/d 13 maka 12*/  " +
                          "                  then 720 - ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))   " +
                          "              else  " +
                          "                  case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780 and ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))>780 " +

                          "             /*--jika keluar lewat jam 13 */ " +
                          "                  then case when ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) > 720 and ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) <= 780  " +
                          "                       then ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) - 780  " +
                // Added tanggal 12 Oktober 2018 Oleh Beny , istirahat jam 6 sore tidak di hitung
                          "                 /*-- Added tanggal 12 Oktober 2018 Oleh Beny */ " +
                          "                 /*-- Untuk Mobil yg keluar diantara jam 18:00 dan jam 19:00 */ " +
                          "                      when ((DATEPART(HOUR,A.TimeOut)*60))>=1080 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))<=1140 " +
                          "                      then 1080 -  ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) " +
                          "                 /*-- Untuk Mobil yg masuk dibawah jam 18:00 dan keluar diatas jam 19:00 */ " +
                          "                      when ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeOut))<1080 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>1140 " +
                          "                      then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) -  ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)))-60  " +
                          "                 /*-- End Added */ " +
                // end Added
                          "                       else (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- (((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))) " +
                          "                 /*-- +60 gak term asuk jam istirahat */ " +
                          "                  end  " +
                          "                  else  case when ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))<720   " +
                          "                          then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn) )  " +
                          "                        when  ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780  " +
                          "                          then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)+60 )  " +
                          "                      /*  --jk masuk antara jam 12 dan 13, 16Sept2014 */ " +
                          "                        when  ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))>=720 and ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<=780  " +
                          "                         and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780  " +
                          "                          then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- 780   " +
                          "                      /*  --jk masuk antara jam 12 dan 13 */ " +
                          "                        else (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))-  ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))  " +
                          "                      /*  --kelaru sebelum jam 12*/  " +
                          "                        end  " +
                          "                  end   " +
                          "                /* -- +60 utk kurangi jam istirahat*/ " +
                          "              end  " +
                          "          else  " +
                          "              case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= 780  " +
                          "              then 720 - ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))  " +
                          "              else  " +
                          "              ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-" + WaktuAwal +
                          "             end	 " +
                          "          end  " +
                          "      else  " +
                          "          999  " +
                          "      end  " +
                          "  end Sts  " +

                          "  from LoadingTime as A, MasterKendaraan as B where A.KendaraanID=B.ID and A.TimeOut is not null and A.TimeOut>A.TimeIn  " +
                          "  and Convert(varchar,A.TimeIn,112) >= '" + drTgl + "' and Convert(varchar,A.TimeIn,112) <= '" + sdTgl + "' ) as C ) as Data1 order by MobilSendiri,TimeIn";



            #endregion
            #region Baru Lagi
            string A = string.Empty; string B = string.Empty;

            if (Convert.ToInt32(drTgl.Substring(0, 6)) > Convert.ToInt32("201911"))
            {
                A = " when /**UrutanNo<=100**/ Tujuan='DPulau' and MobilSendiri=1 and hari<>'Saturday' and (DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)>1260 " +
                    " then ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut)-360) " +
                    " when /**UrutanNo<=100**/ Tujuan='DPulau' and MobilSendiri=1 and hari='Saturday' and (DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)>900 " +
                    " then ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut)-360) " +
                    " when /**UrutanNo>100**/ Tujuan='LPulau' and MobilSendiri=1 and hari<>'Saturday' and (DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)>1080 " +
                    " then ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut)-360) " +
                    " when /**UrutanNo>100**/ Tujuan='LPulau' and MobilSendiri=1 and hari='Saturday' and (DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)>720 " +
                    " then ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut)-360) ";
                B = " ,Tujuan ";
            }
            else
            {
                A = " "; B = " ";
            }

            string strSql =
                //" select *,case when Status2>TargetLoading then 'Lewat'  else 'OK'    end StatusLoading " +

            //Revisi 22 April 2019
            " select *,case when Tujuan='DPulau' then 'Dalam Pulau' when Tujuan='LPulau' then 'Luar Pulau' else '-' end Tujuan2" +
            " ,case  " +
            " when Status2>TargetLoading and Ket<>'BPAS' and Rit1=1 then 'Lewat' " +
            " when Ket='BPAS' and Rit1=1 and ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn))>390 then 'Lewat' " +
            " when Ket='BPAS' and Rit1=1 and ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))<=390 and Status2>TargetLoading then 'Lewat' " +
            " when Ket='BPAS' and Rit1=1 and ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))<=390 and Status2<=TargetLoading then 'OK' " +
            " when Ket='BPAS' and Rit1=1 and (DATEPART(HOUR,TimeIn)*60)<=390 and ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))>390 then 'Lewat' " +
            " when ket='BPAS' and Rit1>1 and Status2>TargetLoading then 'Lewat' " +
            " when TimeIn=[TimeOut] then 'TimeIn=TimeOut' " +
            " else 'OK'    end StatusLoading,'1900-01-01'TimeDaftar,'-'Noted,''Target " +
                // End Revisi

            " from ( " +
            " select *,case " +

            /** Perhitungan yang beda Hari TimeIn dan TimeOut **/
            " when MobilSendiri=1 and left(convert(char,timein,112),8)<>left(convert(char,timeout,112),8) then " +
            " (select ((((((DATEPART(HOUR,'23:59:00.000')*60) /60) +1) - ((DATEPART(HOUR,TimeIN)*60) / 60))*60)+ (DATEPART(MINUTE,TimeIN))) " +
            " + ((DATEPART(HOUR,TimeOut)*60) + (DATEPART(MINUTE,TimeOut)))) " +

            /** Perhitungan u/ Armada luar BPAS dgn Tujuan dalam kota / Depo  
             *  1. Range No Kartu Proxy : 1 - 100 per Tanggal 31-01-2020 DiAbaikan, tidak melihat penomoran Kartu
             *  2. Range Proses Loading Senin - Jumat : 06:00 - 21:00 , Sabtu : 06:00 - 15:00 
             *  3. Ketika Proxy Masuk diatas Jam 21:00 maka proses perhitungan dimulai Jam : 06:00 ( Senin - Jumat )
             *  4. Ketika Proxy Masuk diatas Jam 15:00 maka proses perhitungan dimulai Jam : 06:00 ( Sabtu )
             *  **/
            "" + A + "" +
                //" when /**UrutanNo<=100**/ Tujuan='Depo' and MobilSendiri=1 and hari<>'Saturday' and (DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)>1260 " +
                //" then ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut)-360) " +
                //" when /**UrutanNo<=100**/ Tujuan='Depo' and MobilSendiri=1 and hari='Saturday' and (DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)>900 " +
                //" then ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut)-360) " +

            /** Perhitungan u/ Armada luar BPAS dgn Tujuan luar Pulau  
             *  1. Range No Kartu Proxy : > 100 per Tanggal 31-01-2020 Diabaikan, tidak melihat penomoran Kartu
             *  2. Range Proses Loading Senin - Jumat : 06:00 - 18:00 , Sabtu : 06:00 - 12:00 
             *  3. Ketika Proxy Masuk diatas Jam 18:00 maka proses perhitungan dimulai Jam : 06:00 ( Senin - Jumat )
             *  4. Ketika Proxy Masuk diatas Jam 12:00 maka proses perhitungan dimulai Jam : 06:00 ( Sabtu )
             *  **/
                //" when /**UrutanNo>100**/ Tujuan='LPulau' and MobilSendiri=1 and hari<>'Saturday' and (DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)>1080 " +
                //" then ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut)-360) " +
                //" when /**UrutanNo>100**/ Tujuan='LPulau' and MobilSendiri=1 and hari='Saturday' and (DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)>720 " +
                //" then ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut)-360) " +


            " when Hari='Friday' and ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn))<=690 and ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))>780 then ((((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))-90)- ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn))) " +
            " when Hari='Friday' and ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn))>690 and ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn))<780 then (((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))-90)- 690 " +
            " when Hari='Friday' and ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))>=690 and ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))<=780 then  " +
            " 690 - ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) " +
            " when (LEFT(convert(char,timein,112),8))<(LEFT(convert(char,TimeOut,112),8)) then (1440 - ((DATEPART(HOUR,timein)*60)+DATEPART(MINUTE,timein))) + ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut)) " +
            " else Status22 end Status2  from( " +
            " select *,case when Sts=999 then ((DATEPART(HOUR,Data1.TimeOut)*60)+DATEPART(MINUTE,Data1.TimeOut))-((DATEPART(HOUR,Data1.TimeIn)*60)+DATEPART(MINUTE,Data1.TimeIn)) else Sts end Status22 from ( " +

            /*--gak liat Rit */
            " select C.*, " +
            " case when Sts>TargetLoading then 'Lewat'  else 'OK'    end StatusLoading_lama, " +
            " case when MobilSendiri=0 then case when Sts=999 then 'Lewat' else case when Sts>TargetLoading then case when Rit1=1 then 'Lewat' else 'OK' end else case when Rit1=1 then 'OK' else 'Lewat' end end end else case when Sts=999 then 'Lewat' else  case when Sts>TargetLoading then 'Lewat' else 'OK' end end end StatusL, " +
            " case when MobilSendiri=0 then case when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) <=390 AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))<=390 ) then 'OK' " +
            " when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) <=390 AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))<540 )then 'Lewat' when (((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) <=390 AND " +
            " ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))>540 ) then 'Lewat' when (((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) >390 AND " +
            " ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))<=540 ) then 'Lewat2' when (((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) >390 AND " +
            " ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))>540 and Sts<TargetLoading )    " +
            " then 'OK2' when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) >390 AND ((DATEPART(HOUR,TimeOut)*60)+DATEPART(MINUTE,TimeOut))>540 and Sts>TargetLoading ) " +
            " then 'Lewat3' when ( ((DATEPART(HOUR,TimeIn)*60)+DATEPART(MINUTE,TimeIn)) > 540 and Sts<TargetLoading ) then 'OK3' end   else " +
            " case when Sts>TargetLoading then 'Lewat'  else 'OK' end    end Status1 ,DATENAME(WEEKDAY,TimeIn)Hari  " +
            " from(select A.ID,A.UrutanNo,case when A.MobilSendiri=0 then 'BPAS' else '' end Ket,A.MobilSendiri,A.TimeIn,A.TimeOut,A.NoPolisi," +
            " B.JenisMobil,B.Target as TargetLoading,case when A.MobilSendiri=0 then  " +

            /*--TimeIn jam 3 s/d 9 = Rit1 */
            " case when ( ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) >=180 AND ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<=540 )then 1 else 2 end    else 1 end Rit1, " +
            " case when A.MobilSendiri=0 then case when datediff(DAY,A.TimeIn,A.TimeOut)=0 then  " +

            /* --jk timeIn kurang dari 6.30 dan keluar lewat 6.30  */
            " case when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)<=390 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) >390 " +

            /*--jk TimeIn lebih 6.30 dan TimeOut lebih dari 6.30 */
            " then  case	when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)> 390 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>390   " +

            /* --jika dateng lewat dari 6.30*/
            " then case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= 780 " +

            /*  --jika keluar antara jam 12 s/d 13 maka 12 */
            " then 720 - ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) else case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780  " +

            /*--jika keluar lewat jam 13 */
            " then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))   " +

            /*-- +60 gak term asuk jam istirahat */
            " else case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 540  " +

            /*--jika keluar lewat jam 9   */
            " then ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) " +

            /*--kelaru sebelum jam 12 dari 6.30 mobilsendiri */
            " else ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) end end " +

            /*-- +60 utk kurangi jam istirahat */
            " end else case when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)<=390 and ((DATEPART(HOUR,A.TimeOut)*60)+" +
            " DATEPART(MINUTE,A.TimeOut))>390 then 999 end end else case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 720 and " +
            " ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= 780  " +

            /*--mobil sendiri  keluar jam istirahat */
            " then 720 - ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) else case when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)<=390 " +
            " and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= 390 then ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))" +
            " -((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) " +

            /*--jk masuk antara jam 12 dan 13, 16Sept2014  */
            " when ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))<720 then " +
            " (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn) ) " +
            " when  ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780 " +
            " then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)+60 ) " +
            " when  ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))>=720 and ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<=780 " +
            " and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780 then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- 780  " +

            /* --jk masuk antara jam 12 dan 13, 16Sept2014 */
            " else ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) end end " +
            " end else 999999 end else " +

            /*--mobil luar */
            " case when datediff(DAY,A.TimeIn,A.TimeOut)=0 then case when (DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)>=390   " +

            /*--jika lewat jam 7 */
            " then case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) <= 780   " +

            /*--jika keluar antara jam 12 s/d 13 maka 12*/
            " then 720 - ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) else case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780 " +
            " and ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))>780    " +

            /*--jika keluar lewat jam 13 */
            " then case when ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) > 720 and ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))" +
            "  <= 780 then ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) - 780  " +

            /*-- Added tanggal 12 Oktober 2018 Oleh Beny */
                /*-- Untuk Mobil yg keluar diantara jam 18:00 dan jam 19:00 */
            " when ((DATEPART(HOUR,A.TimeOut)*60))>=1082 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))<=1140 then " +
            " 1080-((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))         " +

            /*-- Untuk Mobil yg masuk dibawah jam 18:00 dan keluar diatas jam 19:00 */
            " when ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeOut))<1080 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>1140 " +
            " then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) -  ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)))-60 " +

            /*-- End Added */
            " else (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- (((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)))   " +

            /*-- +60 gak termasuk jam istirahat */
            " end else case " +
            " when ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))<720 " +
            " then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn) )  " +
            " when ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<720 and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780 " +
            " then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)+60 ) " +

            /*  --jk masuk antara jam 12 dan 13, 16Sept2014 */
            " when ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))>=720 and ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn))<=780 " +
            " and ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))>780 then (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))- 780 " +

            /*  --jk masuk antara jam 12 dan 13 */
            " else (((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)))-  ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) " +

            /*  --keluar sebelum jam 12*/
            " end end " +

             /* -- +60 utk kurangi jam istirahat*/
            " end else case when ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) > 720 and ((DATEPART(HOUR,A.TimeOut)*60)+ " +
            " DATEPART(MINUTE,A.TimeOut)) <= 780   then 720 - ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) " +
            " when ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) >=0 and ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) <= 360 " +
            " then ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut)) - ((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) " +

            /** Perbaikan karena ada minus di loading time Jombang **/
                //" else ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-390 "+
            "  when A.MobilSendiri=1 then  " +
            " ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-((DATEPART(HOUR,A.TimeIn)*60)+DATEPART(MINUTE,A.TimeIn)) " +
            " else " +
            " ((DATEPART(HOUR,A.TimeOut)*60)+DATEPART(MINUTE,A.TimeOut))-390  " +

            " end	end else        999 end end Sts " + B + " " +
            " from LoadingTime as A, MasterKendaraan as B  " +
            " where A.KendaraanID=B.ID  " +
            " and Convert(varchar,A.TimeOut,112) >= '" + drTgl + "' and Convert(varchar,A.TimeOut,112) <= '" + sdTgl + "'  ) as C ) as Data1 ) as Data2 ) as Data3 " +
            " order by MobilSendiri,TimeIn ";

            #endregion
            return strSql;
        }


        public string ViewTask2(string periodeAwal, string periodeAkhir, int depoID, int deptID, int userID, int iso_userid)
        {
            return "Select A.TaskNo,Depo.ID as DepoID, " +
                   "case Depo.ID when 1 then 'Citerep' when 7 then 'Karawang' when 100 then 'CPD' else '' end PT, " +
                   "A.DeptID,C.DeptName,A.BagianID,D.BagianName,A.PIC,REPLACE(CONVERT(VARCHAR(11), A.TglMulai, 106), ' ', '-') as TglMulai,A.TaskName,A.CategoryID,E.Category,A.NilaiBobot, " +
                   "B.TargetKe,REPLACE(CONVERT(VARCHAR(11), B.TglTargetSelesai, 106), ' ', '-') as TglTargetSelesai,REPLACE(CONVERT(VARCHAR(11), A.TglSelesai, 106), ' ', '-') as TglSelesai, " +
                   "case when B.Status=0 then 'Open' when B.Status=1 then 'UnSolved' when B.Status=2 then 'Solved' else '' end Status, " +
                   "case when B.Approval=0 then 'Belum di-Approval' when B.Approval=1 then 'Approval 1' when B.Approval=2 then 'Approval 2' else '' end Approval, " +
                   "case when B.Status=2 then PointNilai else 0 End Nilai " +
                   "from ISO_Task as A,ISO_TaskDetail as B, ISO_Dept as C, ISO_Bagian as D, ISO_Category as E,Depo,users " +
                   "where A.ID=B.TaskID and A.RowStatus>-1 and B.RowStatus>-1  and A.DeptID = " + deptID + " and A.UserID = " + userID + " and A.BagianID=D.ID and A.CategoryID=E.ID and A.UserID = Users.ID and Users.UnitKerjaID = Depo.ID " +
                   "and Depo.ID = " + depoID + " and A.DeptID=C.ID and CONVERT(varchar,A.TglMulai,112) >= '" + periodeAwal + "' and CONVERT(varchar,A.TglMulai,112) <= '" + periodeAkhir + "' and A.ISO_UserID=" + iso_userid +
                   "order by A.TaskNo,A.TglMulai,TargetKe ";
        }
        public string ViewTask3(string periodeAwal, string periodeAkhir, int deptID, string pic, int intPic)
        {
            string strTargetKet = string.Empty;
            string strISOuserID = string.Empty;
            string strDepoID = string.Empty;
            string strDeptID = string.Empty;

            if (intPic > 0)
                strISOuserID = " and A.PIC='" + pic.TrimEnd() + "' ";
            if (deptID > 0)
                strDeptID = " and A.DeptID = " + deptID + " ";
            string addQuery = "select *,Case When Approval=2 then (CAST(NilaiBobot as decimal)/CAST(( " +
                            "select SUM(NilaiBobot) from ISO_Task where PIC=xd.PIC and Convert(CHAR,TglSelesai,112) >=" +
                            "'" + periodeAwal + "' and Convert(CHAR,TglSelesai,112)<='" + periodeAkhir + "' " +
                            "and RowStatus >-1 and RowStatus !=9 and Approval=2) as decimal )*PointNilai) Else 0 End Score from(";
            string addLast = ") as xd ";
            string strSQL = addQuery + " select TaskIDnya,CONVERT(VARCHAR, ISO_Task.TglMulai, 106) as TglMulai,(select top 1 Users.UserName " +
                            "from ISO_Dept,Users where ISO_Dept.DeptID=ISO_Task.DeptID " +
                            "and ISO_Dept.UserGroupID=100 and ISO_Dept.UserID=Users.ID and ISO_Dept.RowStatus >-1 and Users.Apv>0) as PemberiTask," +
                            "ISO_Task.PIC,ISO_Task.TaskName,ISO_Task.DeptID, (select top 1 DeptName from ISO_Dept where ISO_Dept.DeptID = ISO_Task.DeptID) as DeptName,ISO_Bagian.BagianName," +
                            "Pivot1.TaskNo,convert(varchar,T1,106) as T1,convert(varchar,T2,106) as T2,convert(varchar,T3,106) as T3,convert(varchar,T4,106) as T4 " +
                            ",convert(varchar,T5,106) as T5,convert(varchar,T6,106) as T6,convert(varchar,ISO_Task.TglSelesai,106) as Selesai,ISO_Task.NilaiBobot," +
                            "case when TaskIDnya>1 then (select top 1 PointNilai from ISO_TaskDetail as Z where Z.TaskID=TaskIDnya and Z.Status=2 and Z.Approval=2) else null end PointNilai,Approval " +
                            "from (select AA.* from (Select A.ID as TaskIDnya ,A.TaskNo,'T'+convert(varchar(1),B.TargetKe) as TargetKe,B.TglTargetSelesai,B.Approval " +
                            "from ISO_Task as A,ISO_TaskDetail as B, ISO_Dept as C, ISO_Bagian as D, ISO_Category as E,users " +
                            "where A.ID=B.TaskID and A.RowStatus>-1 and B.RowStatus>-1 and A.RowStatus !=9 and A.BagianID=D.ID and A.CategoryID=E.ID and A.UserID = Users.ID " +
                            "and A.DeptID=C.ID and CONVERT(varchar,A.TglSelesai,112) >= '" + periodeAwal + "' and " +
                            "CONVERT(varchar,A.TglSelesai,112) <= '" + periodeAkhir + "' " + strDeptID + strISOuserID + " ) As AA group by TaskIDnya,TaskNo,AA.TargetKe,TglTargetSelesai,AA.Approval) " +
                            "up pivot (MAX(TglTargetSelesai) for TargetKe in (T1,T2,T3,T4,T5,T6) ) as Pivot1 " +
                            "inner join ISO_Task on ISO_Task.ID = Pivot1.TaskIDnya and ISO_Task.RowStatus>-1 and ISO_Task.RowStatus !=9 " +
                            "inner join ISO_Bagian on ISO_Bagian.ID = ISO_Task.BagianID " +
                            addLast + " order by PIC,TaskNo";
            return strSQL;
        }

        public string ViewTask(string periodeAwal, string periodeAkhir, int depoID, int deptID, int intTargetKe, int iso_userid)
        {
            string strTargetKet = string.Empty;
            if (intTargetKe >= 1 && intTargetKe <= 6)
                strTargetKet = " and B.TargetKe=1 and A.Status=0 and Approval in (1,2) ";
            else if (intTargetKe == 7)
                strTargetKet = " and B.Approval=2 and A.Status=2 ";
            else if (intTargetKe == 8)
                strTargetKet = " and A.RowStatus=-1 ";

            return "Select A.TaskNo,Depo.ID as DepoID, " +
                   "case Depo.ID when 1 then 'Citerep' when 7 then 'Karawang' when 100 then 'CPD' else '' end PT, " +
                   "A.DeptID,C.DeptName,A.BagianID,D.BagianName,A.PIC,REPLACE(CONVERT(VARCHAR(11), A.TglMulai, 106), ' ', '-') as TglMulai,A.TaskName,A.CategoryID,E.Category,A.NilaiBobot, " +
                   "B.TargetKe,REPLACE(CONVERT(VARCHAR(11), B.TglTargetSelesai, 106), ' ', '-') as TglTargetSelesai,REPLACE(CONVERT(VARCHAR(11), A.TglSelesai, 106), ' ', '-') as TglSelesai, " +
                   "case when B.Status=0 then 'Open' when B.Status=1 then 'UnSolved' when B.Status=2 then 'Solved' else '' end Status, " +
                   "case when B.Approval=0 then 'Belum di-Approval' when B.Approval=1 then 'Approval 1' when B.Approval=2 then 'Approval 2' else '' end Approval, " +
                   "case when B.Status=2 then PointNilai else 0 End Nilai " +
                   "from ISO_Task as A,ISO_TaskDetail as B, ISO_Dept as C, ISO_Bagian as D, ISO_Category as E,Depo,users " +
                   "where A.ID=B.TaskID and A.RowStatus>-1 and B.RowStatus>-1  and A.DeptID = " + deptID + " and A.BagianID=D.ID and A.CategoryID=E.ID and A.UserID = Users.ID and Users.UnitKerjaID = Depo.ID " +
                   "and Depo.ID = " + depoID + " and A.DeptID=C.ID and CONVERT(varchar,A.TglMulai,112) >= '" + periodeAwal + "' and CONVERT(varchar,A.TglMulai,112) <= '" + periodeAkhir + "' " + strTargetKet + " and A.ISO_UserID=" + iso_userid +
                   "order by A.TaskNo,A.TglMulai,TargetKe ";

        }
        public string ViewTask2(string periodeAwal, string periodeAkhir, int depoID, int deptID, int userID)
        {
            return "Select A.TaskNo,Depo.ID as DepoID, " +
                   "case Depo.ID when 1 then 'Citerep' when 7 then 'Karawang' when 100 then 'CPD' else '' end PT, " +
                   "A.DeptID,C.DeptName,A.BagianID,D.BagianName,A.PIC,REPLACE(CONVERT(VARCHAR(11), A.TglMulai, 106), ' ', '-') as TglMulai,A.TaskName,A.CategoryID,E.Category,A.NilaiBobot, " +
                   "B.TargetKe,REPLACE(CONVERT(VARCHAR(11), B.TglTargetSelesai, 106), ' ', '-') as TglTargetSelesai,REPLACE(CONVERT(VARCHAR(11), A.TglSelesai, 106), ' ', '-') as TglSelesai, " +
                   "case when B.Status=0 then 'Open' when B.Status=1 then 'UnSolved' when B.Status=2 then 'Solved' else '' end Status, " +
                   "case when B.Approval=0 then 'Belum di-Approval' when B.Approval=1 then 'Approval 1' when B.Approval=2 then 'Approval 2' else '' end Approval, " +
                   "case when B.Status=2 then PointNilai else 0 End Nilai " +
                   "from ISO_Task as A,ISO_TaskDetail as B, ISO_Dept as C, ISO_Bagian as D, ISO_Category as E,Depo,users " +
                   "where A.ID=B.TaskID and A.RowStatus>-1 and B.RowStatus>-1  and A.DeptID = " + deptID + " and A.UserID = " + userID + " and A.BagianID=D.ID and A.CategoryID=E.ID and A.UserID = Users.ID and Users.UnitKerjaID = Depo.ID " +
                   "and Depo.ID = " + depoID + " and A.DeptID=C.ID and CONVERT(varchar,A.TglMulai,112) >= '" + periodeAwal + "' and CONVERT(varchar,A.TglMulai,112) <= '" + periodeAkhir + "' " +
                   "order by A.TaskNo,A.TglMulai,TargetKe ";
        }
        public string ViewTask(string periodeAwal, string periodeAkhir, int depoID, int deptID, int intTargetKe)
        {
            string strTargetKet = string.Empty;
            if (intTargetKe >= 1 && intTargetKe <= 6)
                strTargetKet = " and B.TargetKe=1 and A.Status=0 and Approval in (1,2) ";
            else if (intTargetKe == 7)
                strTargetKet = " and B.Approval=2 and A.Status=2 ";
            else if (intTargetKe == 8)
                strTargetKet = " and A.RowStatus=-1 ";

            return "Select A.TaskNo,Depo.ID as DepoID, " +
                   "case Depo.ID when 1 then 'Citerep' when 7 then 'Karawang' when 100 then 'CPD' else '' end PT, " +
                   "A.DeptID,C.DeptName,A.BagianID,D.BagianName,A.PIC,REPLACE(CONVERT(VARCHAR(11), A.TglMulai, 106), ' ', '-') as TglMulai,A.TaskName,A.CategoryID,E.Category,A.NilaiBobot, " +
                   "B.TargetKe,REPLACE(CONVERT(VARCHAR(11), B.TglTargetSelesai, 106), ' ', '-') as TglTargetSelesai,REPLACE(CONVERT(VARCHAR(11), A.TglSelesai, 106), ' ', '-') as TglSelesai, " +
                   "case when B.Status=0 then 'Open' when B.Status=1 then 'UnSolved' when B.Status=2 then 'Solved' else '' end Status, " +
                   "case when B.Approval=0 then 'Belum di-Approval' when B.Approval=1 then 'Approval 1' when B.Approval=2 then 'Approval 2' else '' end Approval, " +
                   "case when B.Status=2 then PointNilai else 0 End Nilai " +
                   "from ISO_Task as A,ISO_TaskDetail as B, ISO_Dept as C, ISO_Bagian as D, ISO_Category as E,Depo,users " +
                   "where A.ID=B.TaskID and A.RowStatus>-1 and B.RowStatus>-1  and A.DeptID = " + deptID + " and A.BagianID=D.ID and A.CategoryID=E.ID and A.UserID = Users.ID and Users.UnitKerjaID = Depo.ID " +
                   "and Depo.ID = " + depoID + " and A.DeptID=C.ID and CONVERT(varchar,A.TglMulai,112) >= '" + periodeAwal + "' and CONVERT(varchar,A.TglMulai,112) <= '" + periodeAkhir + "' " + strTargetKet +
                   "order by A.TaskNo,A.TglMulai,TargetKe ";

        }
        public string ViewPembelianBarang(string ketQtyBlnLalu, string ketAvgBlnLalu, int yearPeriod, string thBl, int itemTypeID, int groupID)
        {
            /** menampilkan price harga kertas added on 10-09-2015*/
            string query = "";
            string[] arrDept = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ViewPriceKertas", "COGS").Split(',');
            query = "select ItemID,ItemCode,ItemName,UOMCode," + ketQtyBlnLalu + " as QtySaldo," + ketAvgBlnLalu + " as HppSaldo," + ketQtyBlnLalu + "*" + ketAvgBlnLalu + " as TotSaldo," +
                "QtyReceipt as QtyMasuk,AvgPriceReceipt as AvgHargaBeli,QtyReceipt*AvgPriceReceipt as AvgTotBeli," +
                "QtyPakai," + ketAvgBlnLalu + " as HppSaldoPakai,QtyPakai*" + ketAvgBlnLalu + " as TotHppSaldoPakai," +
                "QtyAdjustTambah," + ketAvgBlnLalu + " as HppSaldoAdjustTambah,QtyAdjustTambah*" + ketAvgBlnLalu + " as TotHppSaldoQtyAdjustTambah," +
                "QtyAdjustKurang," + ketAvgBlnLalu + " as HppSaldoAdjustKurang,QtyAdjustKurang*" + ketAvgBlnLalu + " as TotHppSaldoQtyAdjustKurang," +
                "QtyRetur," + ketAvgBlnLalu + " as HppSaldoRetur,QtyAdjustKurang*" + ketAvgBlnLalu + " as TotHppSaldoQtyRetur," +
                "cast((" + ketQtyBlnLalu + "+QtyReceipt+QtyAdjustTambah+QtyRetur-QtyPakai-QtyAdjustKurang) as decimal(16,2)) as EndStok," +
                "case when (" + ketQtyBlnLalu + "+QtyReceipt+QtyAdjustTambah+QtyRetur-QtyPakai-QtyAdjustKurang) = 0 then 0 else " +
                "cast(((" + ketQtyBlnLalu + "*" + ketAvgBlnLalu + ")+(QtyReceipt*AvgPriceReceipt)+(QtyAdjustTambah*" + ketAvgBlnLalu + ")+(QtyAdjustTambah*" + ketAvgBlnLalu + ")+(QtyRetur*" + ketAvgBlnLalu + ")- " +
                "(QtyPakai*" + ketAvgBlnLalu + ")-(QtyAdjustKurang*" + ketAvgBlnLalu + "))/(" + ketQtyBlnLalu + "+QtyReceipt+QtyAdjustTambah+QtyRetur-QtyPakai-QtyAdjustKurang) as decimal(16,2)) " +
                "end AvgPrice," +
                "case when (" + ketQtyBlnLalu + "+QtyReceipt+QtyAdjustTambah+QtyRetur-QtyPakai-QtyAdjustKurang) = 0 then 0 else " +
                "cast(((" + ketQtyBlnLalu + "*" + ketAvgBlnLalu + ")+(QtyReceipt*AvgPriceReceipt)+(QtyAdjustTambah*" + ketAvgBlnLalu + ")+(QtyAdjustTambah*" + ketAvgBlnLalu + ")+(QtyRetur*" + ketAvgBlnLalu + ")- " +
                "(QtyPakai*" + ketAvgBlnLalu + ")-(QtyAdjustKurang*" + ketAvgBlnLalu + "))/(" + ketQtyBlnLalu + "+QtyReceipt+QtyAdjustTambah+QtyRetur-QtyPakai-QtyAdjustKurang) as decimal(16,2)) " +
                "* cast((" + ketQtyBlnLalu + "+QtyReceipt+QtyAdjustTambah+QtyRetur-QtyPakai-QtyAdjustKurang) as decimal(16,2)) " +
                "end TotAvgPrice,FakturPajak " +

                "from(select A0.ItemID,A1.ItemCode,A1.ItemName,A2.UOMCode,cast(isnull(A0." + ketQtyBlnLalu + ",0) as decimal(16,2)) as " + ketQtyBlnLalu + ",cast(isnull(A0." + ketAvgBlnLalu + ",0) as decimal) as " + ketAvgBlnLalu + ", " +
                "case when A0.ItemID>0 then (select top 1 A.FakturPajak  " +
                "from Receipt as A,ReceiptDetail as B where A.ID=B.ReceiptID and A.Status>-1   " +
                "and B.RowStatus>-1 and B.GroupID=A1.GroupID and B.ItemTypeID=A1.ItemTypeID and LEFT(convert(varchar,A.ReceiptDate,112),6) = '" + thBl + "'  " +
                "and B.ItemID=A0.ItemID) else '' end FakturPajak, " +
                "case when A0.ItemID>0 then (select cast(isnull(SUM(Quantity),0) as decimal(16,2)) from " +
                "(select B.ItemID,B.Quantity,B.Price*B.Quantity as Total " +
                "from Receipt as A,ReceiptDetail as B where A.ID=B.ReceiptID and A.Status>-1  and B.RowStatus>-1 and " +
                "B.GroupID=A1.GroupID and B.ItemTypeID=A1.ItemTypeID and LEFT(convert(varchar,A.ReceiptDate,112),6) = '" + thBl + "' and B.ItemID=A0.ItemID) as P ) else 0 end QtyReceipt," +

                //"case when A0.ItemID>0 then (select cast(isnull(sum(Total)/SUM(Quantity),0) as decimal(16,2)) from "+
                //"(select B.ItemID,B.Quantity,B.Price*B.Quantity as Total "+
                //"from Receipt as A,ReceiptDetail as B where A.ID=B.ReceiptID and A.Status>-1  and B.RowStatus>-1 and "+
                //"B.GroupID=A1.GroupID and B.ItemTypeID=A1.ItemTypeID and LEFT(convert(varchar,A.ReceiptDate,112),6) = '" + thBl + "' and B.ItemID=A0.ItemID) as P ) else 0 end AvgPriceReceipt," +
                "case when A0.ItemID>0 then (select cast(isnull(SUM(NetPrice)/SUM(Quantity),0) as decimal(16,2)) from (select B.ItemID,B.Quantity,B.Price*B.Quantity as Total," +
                "case when C.Disc>0 then (D.Price*B.Quantity)-((D.Price*B.Quantity)*(C.Disc/100)) else " +
                "case when D.Price>0 and C.Disc =0 then D.Price*B.Quantity else ";
            if (arrDept.Contains(((Users)HttpContext.Current.Session["Users"]).DeptID.ToString()))
            {
                query += "((ISNULL((select top 1 Harga from HargaKertas where ItemID=D.ItemID and SupplierID=C.SupplierID order by ID desc),0)*B.Quantity)) ";
            }
            else
            {
                query += "0 ";
            }
            query += "End End NetPrice " +
                "from Receipt as A,ReceiptDetail as B,POPurchn as C,POPurchnDetail as D " +
                "where A.ID=B.ReceiptID and A.Status>-1  and B.RowStatus>-1 and B.POID=C.ID and B.PODetailID=D.ID and C.ID=D.POID and C.Status>-1 " +
                "and D.Status>-1 and B.GroupID=A1.GroupID and B.ItemTypeID=A1.ItemTypeID and LEFT(convert(varchar,A.ReceiptDate,112),6) = '" + thBl + "' " +
                "and B.ItemID=A0.ItemID ) as P) else 0 end AvgPriceReceipt," +

                "case when A0.ItemID>0 then (select cast(isnull(SUM(Quantity),0) as decimal(16,2)) from " +
                "(select B.ItemID,B.Quantity,A0." + ketAvgBlnLalu + "*B.Quantity as Total " +
                "from Pakai as A,PakaiDetail as B where A.ID=B.PakaiID and A.Status>-1  and B.RowStatus>-1 and " +
                "B.GroupID=A1.GroupID and B.ItemTypeID=A1.ItemTypeID and LEFT(convert(varchar,A.PakaiDate,112),6) = '" + thBl + "' and B.ItemID=A0.ItemID) as P ) else 0 end QtyPakai," +

                "case when A0.ItemID>0 then (select cast(isnull(SUM(Quantity),0) as decimal(16,2)) from " +
                "(select B.ItemID,B.Quantity,A0." + ketAvgBlnLalu + "*B.Quantity as Total " +
                "from Adjust as A,AdjustDetail as B where B.apv>0 and  A.ID=B.AdjustID and A.Status>-1  and B.RowStatus>-1 and " +
                "B.GroupID=A1.GroupID and B.ItemTypeID=A1.ItemTypeID and LEFT(convert(varchar,A.AdjustDate,112),6) = '" + thBl + "' and left(A.AdjustType,6) = 'Tambah' and B.ItemID=A0.ItemID) as P ) else 0 " +
                "end QtyAdjustTambah," +

                "case when A0.ItemID>0 then (select cast(isnull(SUM(Quantity),0) as decimal(16,2)) from " +
                "(select B.ItemID,B.Quantity,A0." + ketAvgBlnLalu + "*B.Quantity as Total " +
                "from Adjust as A,AdjustDetail as B where B.apv>0 and A.ID=B.AdjustID and A.Status>-1  and B.RowStatus>-1 and " +
                "B.GroupID=A1.GroupID and B.ItemTypeID=A1.ItemTypeID and LEFT(convert(varchar,A.AdjustDate,112),6) = '" + thBl + "' and left(A.AdjustType,6) = 'Kurang' and B.ItemID=A0.ItemID) as P ) else 0 " +
                "end QtyAdjustKurang," +
                "case when A0.ItemID>0 then (select cast(isnull(SUM(Quantity),0) as decimal(16,2)) from " +
                "(select B.ItemID,B.Quantity,A0." + ketAvgBlnLalu + "*B.Quantity as Total " +
                "from ReturPakai as A,ReturPakaiDetail as B where A.ID=B.ReturID and A.Status>-1 and " +
                "B.GroupID=A1.GroupID and B.ItemTypeID=A1.ItemTypeID and LEFT(convert(varchar,A.ReturDate,112),6) = '" + thBl + "' and B.ItemID=A0.ItemID) as P ) else 0 end QtyRetur " +
                "from SaldoInventory as A0,Inventory as A1, UOM as A2 where A1.ID=A0.ItemID and A1.GroupID=" + groupID + " and A1.ItemTypeID=" + itemTypeID + " and A1.UOMID=A2.ID and " +
                "A0.YearPeriod=" + yearPeriod + " and A0.ItemTypeID=" + itemTypeID + " and A0.GroupID=" + groupID + ") as A1 " +
                "where (" + ketQtyBlnLalu + " + QtyReceipt + QtyPakai + QtyAdjustTambah + QtyAdjustKurang + QtyRetur)>0 order by ItemCode";
            return query;
        }
        public string ViewHarian5(string ketBlnLalu, int yearPeriod, string tglAwal, string tglAkhir, string tgl1, string tgl2, int groupID)
        {
            string invGroupID = string.Empty;
            invGroupID = "Inventory.GroupID in (" + groupID + ")";

            return
                //                "--lap harian utk tgl 2 keatas
        "select ItemID,ItemCode,ItemName,UomID,UOMCode,(SAwal+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal) as StokAwal," +
        "0 as Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian,(SAwal+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-" +
        "AdjustKurangAwal) as StokAkhir,0 as DeptID,'' as DeptCode,'' as NoDoc,1 as Urutan,0 as GroupID from (select ItemID,case when " +
        "SaldoInventory.ItemID>0 then (select Inventory.ItemCode from Inventory where Inventory.ID=SaldoInventory.ItemID) else '' end ItemCode,case when " +
        "SaldoInventory.ItemID>0 then (select Inventory.ItemName from Inventory where Inventory.ID=SaldoInventory.ItemID) else '' end ItemName,0 as UomID," +
        "case when SaldoInventory.ItemID>0 then (select UOM.UOMCode from Inventory,UOM where Inventory.UOMID=UOM.ID and Inventory.ID=SaldoInventory.ItemID) " +
        "else '' end UOMCode, " + ketBlnLalu + " as SAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian," + ketBlnLalu +
        " as StokAkhir,0 as DeptID,'' as DeptCode,'' as NoDoc,1 as Urutan,0 as GroupID, CASE WHEN SaldoInventory.ItemID>0 THEN " +
        "(SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=SaldoInventory.ItemID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt " +
        "WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" +
        tglAkhir + "' )) END  PemasukanAwal,CASE WHEN SaldoInventory.ItemID > 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE " +
        "ReturDetail.ItemID=SaldoInventory.ItemID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal +
        "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir +
        "' and Status>-1 )) END  ReturAwal, CASE WHEN SaldoInventory.ItemID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE AdjustDetail.apv>0 and " +
        "ItemID = SaldoInventory.ItemID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND " +
        "convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambahAwal," +
        "CASE WHEN SaldoInventory.ItemID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = SaldoInventory.ItemID and RowStatus>-1" +
        " AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal +
        "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurangAwal,CASE WHEN SaldoInventory.ItemID > 0 THEN " +
        "(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=SaldoInventory.ItemID and PakaiDetail.RowStatus>-1 and " +
        "PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir +
        "' and Status>-1)) END  PemakaianAwal from SaldoInventory where SaldoInventory.YearPeriod=2011 and SaldoInventory.ItemTypeID=1 and " +
        "SaldoInventory.ItemID in (select InvID from (SELECT Inventory.id as InvID,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, " +
        "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN " +
        "(SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tgl1 +
        "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan2,CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM " +
        " AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND " +
        "convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah2, CASE WHEN " +
        "Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID " +
        "FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 +
        "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang2, CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QTY),0) FROM " +
        "ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tgl1 +
        "' AND convert(varchar,CreatedTime,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur2, CASE WHEN Inventory.ID > 0 THEN " +
        "(SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN " +
        "(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 +
        "' and Status>-1)) END  Pemakaian2 FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID +
        ")) as AA where (Pemasukan2>0 or AdjustKurang2>0 or AdjustTambah2>0 or Pemakaian2>0 or Retur2>0)) ) as AA1 " +
        "union select ItemID1 as ItemID,ItemCode,ItemName,UOMID,UOMCode," +
        "case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
        "SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal) " +
        "end StokAwal,Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian," +
        "case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and SaldoInventory.YearPeriod=" +
        yearPeriod + " and SaldoInventory.ItemTypeID=1)+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal+Pemasukan) end StokAkhir," +
        "0 as DeptID,'' as DeptCode,NoDoc,2 as Urutan,GroupID from (SELECT Inventory.id as ItemID1,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, " +
        "UOM.UOMCode,Inventory.GroupID, " +
        "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN " +
        "(SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tgl1 +
        "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan," +

        "CASE WHEN Inventory.ID>0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  PemasukanAwal," +
        "CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, " +
        "CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambahAwal," +
        "CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurangAwal," +
        "CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PemakaianAwal," +
        "CASE WHEN Inventory.ID > 0 THEN (select top 1 Receipt.ReceiptNo from Receipt,ReceiptDetail where Receipt.ID=ReceiptDetail.ReceiptID and " +

        "convert(varchar,Receipt.ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,Receipt.ReceiptDate,112) <= '" + tgl2 + "' and Status>-1 and " +
        "ReceiptDetail.RowStatus>-1 and ReceiptDetail.ItemID=Inventory.ID)  END NoDoc " +
        "FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where Pemasukan>0 " +

        "union " +
                //--pemakaian, urutan = 3
        "select ItemID1 as ItemID,ItemCode,ItemName,UOMID,UOMCode," +
        "case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
                //--add pemasukan di saldoawal
        "SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal+Pemasukan) " +
        "end StokAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,Pemakaian," +
        "case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
                //--add pemasukan di saldoakhir
        "SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal-Pemakaian+Pemasukan) " +
        "end StokAkhir ,DeptID,DeptCode,NoDoc,3 as Urutan,GroupID from " +
        "(SELECT Inventory.id as ItemID1,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, " +
        "CASE WHEN Inventory.ID > 0 THEN (select top 1 Pakai.DeptID from Pakai,PakaiDetail where Pakai.ID=PakaiDetail.PakaiID and convert(varchar,Pakai.PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,Pakai.PakaiDate,112) <= '" + tgl2 + "' and Status>-1 and PakaiDetail.RowStatus>-1 and PakaiDetail.ItemID=Inventory.ID)  END DeptID, " +
        "CASE WHEN Inventory.ID > 0 THEN (select top 1 Dept.DeptCode from Pakai,PakaiDetail,Dept where Pakai.ID=PakaiDetail.PakaiID and convert(varchar,Pakai.PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,Pakai.PakaiDate,112) <= '" + tgl2 + "' and Status>-1 and PakaiDetail.RowStatus>-1 and PakaiDetail.ItemID=Inventory.ID and Pakai.DeptID=Dept.ID)  END DeptCode," +
        "CASE WHEN Inventory.ID > 0 THEN (select top 1 Pakai.PakaiNo from Pakai,PakaiDetail where Pakai.ID=PakaiDetail.PakaiID and convert(varchar,Pakai.PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,Pakai.PakaiDate,112) <= '" + tgl2 + "' and Status>-1 and PakaiDetail.RowStatus>-1 and PakaiDetail.ItemID=Inventory.ID)  END NoDoc, " +

        "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian," +
                //--add pemasukan dulu
        "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan," +
                //--
        "CASE WHEN Inventory.ID>0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  PemasukanAwal," +
        "CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, " +
        "CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambahAwal," +
        "CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurangAwal," +
        "CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PemakaianAwal FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where Pemakaian>0 " +

        "union " +
                //--adjustkurang, urutan = 4
        "select ItemID1 as ItemID,ItemCode,ItemName,UOMID,UOMCode," +
        "case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
                //--add pemasukan, kurang pemakaian pada saldoawal
        "SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal+Pemasukan-Pemakaian) " +
        "end StokAwal ,0 as Pemasukan,0 as Retur,0 as AdjustTambah,AdjustKurang,0 as Pemakaian," +
                //--add pemasukan, kurang pemakaian pada saldoakhir
        "case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
        "SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal-AdjustKurang+Pemasukan-Pemakaian) " +
        "end StokAkhir ,0 as DeptID,'' as DeptCode,'' as NoDoc," +
        "4 as Urutan,GroupID from (SELECT Inventory.id as ItemID1,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, " +
        "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "'  AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang, " +
                //--
        "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan," +
        "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian," +
                //--
        "CASE WHEN Inventory.ID>0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  PemasukanAwal," +
        "CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, " +
        "CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambahAwal," +
        "CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurangAwal," +
        "CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PemakaianAwal " +
        "FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where AdjustKurang>0 " +

        "union " +
                //--adjusttambah, urutan = 5
        "select ItemID1 as ItemID,ItemCode,ItemName,UOMID,UOMCode," +
                //--add & kurang pada stokawal & stokakhir utk pemasukan,pemakaian,adjustkurang
        "case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
        "SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal+Pemasukan-Pemakaian-AdjustKurang) " +
        "end StokAwal ,0 as Pemasukan,0 as Retur,AdjustTambah,0 as AdjustKurang,0 as Pemakaian," +
        "case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
        "SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal+AdjustTambah+Pemasukan-Pemakaian-AdjustKurang) " +
        "end StokAkhir ,0 as DeptID,'' as DeptCode,'' as NoDoc," +
        "5 as Urutan,GroupID from (SELECT Inventory.id as ItemID1,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, " +
        "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah," +
                //--
        "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan," +
        "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian," +
        "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "'  AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang," +
                //--
        "CASE WHEN Inventory.ID>0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  PemasukanAwal," +
        "CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, " +
        "CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambahAwal," +
        "CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurangAwal," +
        "CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PemakaianAwal FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where AdjustTambah>0 " +

        "union select ItemID1 as ItemID,ItemCode,ItemName,UOMID,UOMCode,case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal) end StokAwal,0 as Pemasukan,Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian,case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)+PemasukanAwal+ReturAwal+AdjustTambahAwal-PemakaianAwal-AdjustKurangAwal+Retur) end StokAkhir ,0 as DeptID,'' as DeptCode,'' as NoDoc,2 as Urutan,GroupID from (SELECT Inventory.id as ItemID1,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,CreatedTime,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur,CASE WHEN Inventory.ID>0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  PemasukanAwal,CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambahAwal,CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurangAwal,CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PemakaianAwal FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where Retur>0 " +

        "order by ItemCode,Urutan";

        }
        public string ViewHarian5a(string ketBlnLalu, int yearPeriod, string tglAwal, string tglAkhir, string tgl1, string tgl2, int groupID)
        {
            string invGroupID = string.Empty;
            invGroupID = "Inventory.GroupID in (" + groupID + ")";

            return
                //                "--lap harian utk tgl 2 keatas
            "select ItemID,ItemCode,ItemName,UomID,UOMCode,(SAwal) as StokAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian,(SAwal) as StokAkhir,0 as DeptID,'' as DeptCode,'' as NoDoc,1 as Urutan,0 as GroupID " +
            "from (select ItemID,case when SaldoInventory.ItemID>0 then (select Inventory.ItemCode from Inventory where Inventory.ID=SaldoInventory.ItemID) else '' end ItemCode,case when SaldoInventory.ItemID>0 then (select Inventory.ItemName from Inventory where Inventory.ID=SaldoInventory.ItemID) else '' end ItemName,0 as UomID," +
            "case when SaldoInventory.ItemID>0 then (select UOM.UOMCode from Inventory,UOM where Inventory.UOMID=UOM.ID and Inventory.ID=SaldoInventory.ItemID) else '' end UOMCode, " + ketBlnLalu + " as SAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian," + ketBlnLalu + " as StokAkhir,0 as DeptID,'' as DeptCode,'' as NoDoc,1 as Urutan,0 as GroupID," +
            "CASE WHEN SaldoInventory.ItemID>0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=SaldoInventory.ItemID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  PemasukanAwal,CASE WHEN SaldoInventory.ItemID > 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=SaldoInventory.ItemID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, CASE WHEN SaldoInventory.ItemID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = SaldoInventory.ItemID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambahAwal,CASE WHEN SaldoInventory.ItemID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = SaldoInventory.ItemID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurangAwal,CASE WHEN SaldoInventory.ItemID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=SaldoInventory.ItemID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PemakaianAwal " +
            "from SaldoInventory where SaldoInventory.YearPeriod=2011 and SaldoInventory.ItemTypeID=1 and SaldoInventory.ItemID in (select InvID from (SELECT Inventory.id as InvID,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan2,CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah2, CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang2, CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,CreatedTime,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur2, CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian2 FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ")) as AA where (Pemasukan2>0 or AdjustKurang2>0 or AdjustTambah2>0 or Pemakaian2>0 or Retur2>0)) ) as AA1 " +
                //--pemasukan, urutan = 2
            "union select ItemID1 as ItemID,ItemCode,ItemName,UOMID,UOMCode," +
            "case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
            "SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)) " +
            "end StokAwal,Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian," +
            "case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)+Pemasukan) end StokAkhir,0 as DeptID,'' as DeptCode,NoDoc,2 as Urutan,GroupID from (SELECT Inventory.id as ItemID1,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, " +
            "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan," +

            //"CASE WHEN Inventory.ID>0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  PemasukanAwal," +
                //"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, " +
                //"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambahAwal," +
                //"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurangAwal," +
                //"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PemakaianAwal," +

            "CASE WHEN Inventory.ID > 0 THEN (select top 1 Receipt.ReceiptNo from Receipt,ReceiptDetail where Receipt.ID=ReceiptDetail.ReceiptID and " +
            "convert(varchar,Receipt.ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,Receipt.ReceiptDate,112) <= '" + tgl2 + "' and Status>-1 and " +
            "ReceiptDetail.RowStatus>-1 and ReceiptDetail.ItemID=Inventory.ID)  END NoDoc " +
            "FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where Pemasukan>0 " +

            "union " +
                //--pemakaian, urutan = 3
            "select ItemID1 as ItemID,ItemCode,ItemName,UOMID,UOMCode," +
            "case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
                //--add pemasukan di saldoawal
            "SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)+Pemasukan) " +
            "end StokAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,Pemakaian," +
            "case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
                //--add pemasukan di saldoakhir
            "SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)-Pemakaian+Pemasukan) " +
            "end StokAkhir ,DeptID,DeptCode,NoDoc,3 as Urutan,GroupID from " +
            "(SELECT Inventory.id as ItemID1,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, " +
            "CASE WHEN Inventory.ID > 0 THEN (select top 1 Pakai.DeptID from Pakai,PakaiDetail where Pakai.ID=PakaiDetail.PakaiID and convert(varchar,Pakai.PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,Pakai.PakaiDate,112) <= '" + tgl2 + "' and Status>-1 and PakaiDetail.RowStatus>-1 and PakaiDetail.ItemID=Inventory.ID)  END DeptID, " +
            "CASE WHEN Inventory.ID > 0 THEN (select top 1 Dept.DeptCode from Pakai,PakaiDetail,Dept where Pakai.ID=PakaiDetail.PakaiID and convert(varchar,Pakai.PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,Pakai.PakaiDate,112) <= '" + tgl2 + "' and Status>-1 and PakaiDetail.RowStatus>-1 and PakaiDetail.ItemID=Inventory.ID and Pakai.DeptID=Dept.ID)  END DeptCode," +
            "CASE WHEN Inventory.ID > 0 THEN (select top 1 Pakai.PakaiNo from Pakai,PakaiDetail where Pakai.ID=PakaiDetail.PakaiID and convert(varchar,Pakai.PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,Pakai.PakaiDate,112) <= '" + tgl2 + "' and Status>-1 and PakaiDetail.RowStatus>-1 and PakaiDetail.ItemID=Inventory.ID)  END NoDoc, " +

            "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian," +
                //--add pemasukan dulu
            "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan " +
                //--
                //"CASE WHEN Inventory.ID>0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  PemasukanAwal," +
                //"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, " +
                //"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambahAwal," +
                //"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurangAwal," +
                //"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PemakaianAwal "+

            "FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where Pemakaian>0 " +

            "union " +
                //--adjustkurang, urutan = 4
            "select ItemID1 as ItemID,ItemCode,ItemName,UOMID,UOMCode," +
            "case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
                //--add pemasukan, kurang pemakaian pada saldoawal
            "SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)+Pemasukan-Pemakaian) " +
            "end StokAwal ,0 as Pemasukan,0 as Retur,0 as AdjustTambah,AdjustKurang,0 as Pemakaian," +
                //--add pemasukan, kurang pemakaian pada saldoakhir
            "case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
            "SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)-AdjustKurang+Pemasukan-Pemakaian) " +
            "end StokAkhir ,0 as DeptID,'' as DeptCode,'' as NoDoc," +
            "4 as Urutan,GroupID from (SELECT Inventory.id as ItemID1,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, " +
            "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "'  AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang, " +
                //--
            "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan," +
            "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian " +
                //--
                //"CASE WHEN Inventory.ID>0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  PemasukanAwal," +
                //"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, " +
                //"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambahAwal," +
                //"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurangAwal," +
                //"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PemakaianAwal " +

            "FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where AdjustKurang>0 " +

            "union " +
                //--adjusttambah, urutan = 5
            "select ItemID1 as ItemID,ItemCode,ItemName,UOMID,UOMCode," +
                //--add & kurang pada stokawal & stokakhir utk pemasukan,pemakaian,adjustkurang
            "case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
            "SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)+Pemasukan-Pemakaian-AdjustKurang) " +
            "end StokAwal ,0 as Pemasukan,0 as Retur,AdjustTambah,0 as AdjustKurang,0 as Pemakaian," +
            "case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
            "SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)+AdjustTambah+Pemasukan-Pemakaian-AdjustKurang) " +
            "end StokAkhir ,0 as DeptID,'' as DeptCode,'' as NoDoc," +
            "5 as Urutan,GroupID from (SELECT Inventory.id as ItemID1,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, " +
            "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah," +
                //--
            "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan," +
            "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian," +
            "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "'  AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang " +
                //--
                //"CASE WHEN Inventory.ID>0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  PemasukanAwal," +
                //"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, " +
                //"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambahAwal," +
                //"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurangAwal," +
                //"CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PemakaianAwal "+

            "FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where AdjustTambah>0 " +

            "union select ItemID1 as ItemID,ItemCode,ItemName,UOMID,UOMCode,case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)) end StokAwal,0 as Pemasukan,Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian,case when ItemID1>0 then ((select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)+Retur) end StokAkhir ,0 as DeptID,'' as DeptCode,'' as NoDoc,2 as Urutan,GroupID from (SELECT Inventory.id as ItemID1,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,CreatedTime,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur " +
                //"CASE WHEN Inventory.ID>0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  PemasukanAwal,CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambahAwal,CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurangAwal,CASE WHEN Inventory.ID> 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PemakaianAwal "+
            "FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where Retur>0 " +

            "order by ItemCode,Urutan";

        }
        public string ViewHarianA3(string ketBlnLalu, int yearPeriod, string tglAwal, string tglAkhir, string tgl1, string tgl2, int groupID)
        {
            return "select ItemID,case when SaldoInventory.ItemID>0 then (select Inventory.ItemCode from Inventory where Inventory.ID=SaldoInventory.ItemID) else '' end ItemCode," +
            "case when SaldoInventory.ItemID>0 then (select Inventory.ItemName from Inventory where Inventory.ID=SaldoInventory.ItemID) else '' end ItemName,0 as UomID,case when SaldoInventory.ItemID>0 then (select UOM.UOMCode from Inventory,UOM where Inventory.UOMID=UOM.ID and Inventory.ID=SaldoInventory.ItemID) else '' end UOMCode, " +
            ketBlnLalu + " as StokAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian," + ketBlnLalu + " as StokAkhir,0 as DeptID,'' as DeptCode,'' as NoDoc,1 as Urutan,0 as GroupID from SaldoInventory where SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1 and SaldoInventory.ItemID in " +
            "(select InvID from (SELECT Inventory.id as InvID,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, " +
            "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  Pemasukan," +
            "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambah," +
            "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE AdjustDetail.apv>0 and  ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurang," +
            "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  Retur, " +
            "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  Pemakaian, " +
            "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan2," +
            "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah2," +
            "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE AdjustDetail.apv>0 and  ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang2," +
            "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,CreatedTime,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur2, " +
            "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian2 " +
            "FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (Inventory.GroupID in (" + groupID + "))) as AA " +
            "where (Pemasukan2>0 or AdjustKurang2>0 or AdjustTambah2>0 or Pemakaian2>0 or Retur2>0)) " +

            "union select ItemID1 as ItemID,ItemCode,ItemName,UOMID,UOMCode," +
            "case when ItemID1>0 then (select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
            "SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1) end StokAwal,Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian," +
            "case when ItemID1>0 then (select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
            "SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)-Pemasukan end StokAkhir,0 as DeptID,'' as DeptCode,NoDoc,2 as Urutan,GroupID " +
            "from (SELECT Inventory.id as ItemID1,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan,CASE WHEN Inventory.ID > 0 THEN (select top 1 Receipt.ReceiptNo from Receipt,ReceiptDetail where Receipt.ID=ReceiptDetail.ReceiptID and convert(varchar,Receipt.ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,Receipt.ReceiptDate,112) <= '" + tgl2 + "' and Status>-1 and ReceiptDetail.RowStatus>-1 and ReceiptDetail.ItemID=Inventory.ID)  END NoDoc FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (Inventory.GroupID in (" + groupID + ")) ) as AA where Pemasukan>0 " +

            "union select ItemID1 as ItemID,ItemCode,ItemName,UOMID,UOMCode,case when ItemID1>0 then (select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
            "SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1) end StokAwal,0 as Pemasukan,Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian," +
            "case when ItemID1>0 then (select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)-Retur end StokAkhir " +
            ",0 as DeptID,'' as DeptCode,'' as NoDoc,2 as Urutan,GroupID from (SELECT Inventory.id as ItemID1,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,CreatedTime,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (Inventory.GroupID in (" + groupID + ")) ) as AA where Retur>0 " +

            "union select ItemID1 as ItemID,ItemCode,ItemName,UOMID,UOMCode,case when ItemID1>0 then (select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
            "SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1) end StokAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,Pemakaian," +
            "case when ItemID1>0 then (select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)-Pemakaian end StokAkhir " +
            ",DeptID,DeptCode,NoDoc,2 as Urutan,GroupID from (SELECT Inventory.id as ItemID1,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, CASE WHEN Inventory.ID > 0 THEN (select top 1 Pakai.DeptID from Pakai,PakaiDetail where Pakai.ID=PakaiDetail.PakaiID and convert(varchar,Pakai.PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,Pakai.PakaiDate,112) <= '" + tgl2 + "' and Status>-1 and PakaiDetail.RowStatus>-1 and PakaiDetail.ItemID=Inventory.ID)  END DeptID, CASE WHEN Inventory.ID > 0 THEN (select top 1 Dept.DeptCode from Pakai,PakaiDetail,Dept where Pakai.ID=PakaiDetail.PakaiID and convert(varchar,Pakai.PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,Pakai.PakaiDate,112) <= '" + tgl2 + "' and Status>-1 and PakaiDetail.RowStatus>-1 and PakaiDetail.ItemID=Inventory.ID and Pakai.DeptID=Dept.ID)  END DeptCode,CASE WHEN Inventory.ID > 0 THEN (select top 1 Pakai.PakaiNo from Pakai,PakaiDetail where Pakai.ID=PakaiDetail.PakaiID and convert(varchar,Pakai.PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,Pakai.PakaiDate,112) <= '" + tgl2 + "' and Status>-1 and PakaiDetail.RowStatus>-1 and PakaiDetail.ItemID=Inventory.ID)  END NoDoc, " +
            "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian " +
            "FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (Inventory.GroupID in (" + groupID + ")) ) as AA where Pemakaian>0 " +

            "union select ItemID1 as ItemID,ItemCode,ItemName,UOMID,UOMCode," +
            "case when ItemID1>0 then (select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
            "SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1) end StokAwal " +
            ",0 as Pemasukan,0 as Retur,AdjustTambah,0 as AdjustKurang,0 as Pemakaian," +
            "case when ItemID1>0 then (select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
            "SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)-AdjustTambah end StokAkhir " +
            ",0 as DeptID,'' as DeptCode,'' as NoDoc,2 as Urutan,GroupID from (SELECT Inventory.id as ItemID1,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (Inventory.GroupID in (" + groupID + ")) ) as AA where AdjustTambah>0 " +

            "union select ItemID1 as ItemID,ItemCode,ItemName,UOMID,UOMCode,case when ItemID1>0 then (select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and " +
            "SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1) end StokAwal " +
            ",0 as Pemasukan,0 as Retur,0 as AdjustTambah,AdjustKurang,0 as Pemakaian," +
            "case when ItemID1>0 then (select " + ketBlnLalu + " from SaldoInventory where SaldoInventory.ItemID=ItemID1 and SaldoInventory.YearPeriod=" + yearPeriod + " and SaldoInventory.ItemTypeID=1)-AdjustKurang end StokAkhir " +
            ",0 as DeptID,'' as DeptCode,'' as NoDoc,2 as Urutan,GroupID from (SELECT Inventory.id as ItemID1,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (Inventory.GroupID in (" + groupID + ")) ) as AA where AdjustKurang>0 " +
            "order by ItemCode,Urutan";

        }
        public string ViewHarianA2(string ketBlnLalu, int yearPeriod, string tglAwal, string tglAkhir, string tgl1, string tgl2, int groupID)
        {
            return
                //utk saldo awal
            "select ItemID," +
            "case when SaldoInventory.ItemID>0 then (select Inventory.ItemCode from Inventory where Inventory.ID=SaldoInventory.ItemID) " +
            "else '' end ItemCode," +
            "case when SaldoInventory.ItemID>0 then (select Inventory.ItemName from Inventory where Inventory.ID=SaldoInventory.ItemID) " +
            "else '' end ItemName,0 as UomID," +
            "case when SaldoInventory.ItemID>0 then (select UOM.UOMCode from Inventory,UOM where Inventory.UOMID=UOM.ID and " +
            "Inventory.ID=SaldoInventory.ItemID) else '' end UOMCode, " + ketBlnLalu + " as SaldoAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian," +
            "0 as StokAkhir,0 as DeptID,'' as DeptCode,'' as NoDoc,1 as Urutan,0 as GroupID " +
            "from SaldoInventory where SaldoInventory.YearPeriod=" + yearPeriod + " and " +
            "SaldoInventory.ItemTypeID=1 and SaldoInventory.ItemID in (select InvID " +
            "from (SELECT Inventory.id as InvID,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, " +
            "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND " +
            "ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND " +
            "convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  Pemasukan," +
            "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND " +
            "AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND " +
            "convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjustTambah," +
            "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND " +
            "AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND " +
            "convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjustKurang," +
            "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN " +
            "(SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) " +
            "END  Retur, " +
            "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and " +
            "PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND " +
            "convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  Pemakaian " +
            "FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (Inventory.GroupID in (" + groupID + ")) ) as AA " +
            "where Pemasukan>0 or AdjustKurang>0 or AdjustTambah>0 or Pemakaian>0 or Retur>0) " +
            "union " +

            //utk Pemasukan
            "select ItemID,ItemCode,ItemName,UOMID,UOMCode,0 as SaldoAwal,Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian," +
            "0 as StokAkhir,0 as DeptID,'' as DeptCode,NoDoc,2 as Urutan,GroupID " +
            "from (SELECT Inventory.id as ItemID,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, " +
            "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND " +
            "ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND " +
            "convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan," +
            "CASE WHEN Inventory.ID > 0 THEN (select top 1 Receipt.ReceiptNo from Receipt,ReceiptDetail where Receipt.ID=ReceiptDetail.ReceiptID and convert(varchar,Receipt.ReceiptDate,112) >= '" + tgl1 + "' AND " +
            "convert(varchar,Receipt.ReceiptDate,112) <= '" + tgl2 + "' and Status>-1 and ReceiptDetail.RowStatus>-1 and ReceiptDetail.ItemID=Inventory.ID) " +
            " END NoDoc " +
            "FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (Inventory.GroupID in (" + groupID + ")) ) as AA " +
            "where Pemasukan>0 " +
            "union " +

            //--utk retur
            "select ItemID,ItemCode,ItemName,UOMID,UOMCode,0 as SaldoAwal,0 as Pemasukan,Retur,0 as AdjustTambah,0 as AdjustKurang,0 as Pemakaian," +
            "0 as StokAkhir,0 as DeptID,'' as DeptCode,'' as NoDoc,2 as Urutan,GroupID " +
            "from (SELECT Inventory.id as ItemID,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, " +
            "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QTY),0) FROM ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN " +
            "(SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,CreatedTime,112) <= '" + tgl2 + "' and Status>-1 )) " +
            "END  Retur " +
            "FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (Inventory.GroupID in (" + groupID + ")) ) as AA " +
            "where Retur>0 " +
            "union " +

            //--utk pemakaian
            "select ItemID,ItemCode,ItemName,UOMID,UOMCode,0 as SaldoAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah,0 as AdjustKurang,Pemakaian," +
            "0 as StokAkhir,DeptID,DeptCode,NoDoc,2 as Urutan,GroupID " +
            "from (SELECT Inventory.id as ItemID,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, " +
            "CASE WHEN Inventory.ID > 0 THEN (select top 1 Pakai.DeptID from Pakai,PakaiDetail where Pakai.ID=PakaiDetail.PakaiID and convert(varchar,Pakai.PakaiDate,112) >= '" + tgl1 + "' AND " +
            "convert(varchar,Pakai.PakaiDate,112) <= '" + tgl2 + "' and Status>-1 and PakaiDetail.RowStatus>-1 and PakaiDetail.ItemID=Inventory.ID) " +
            " END DeptID, " +
            "CASE WHEN Inventory.ID > 0 THEN (select top 1 Dept.DeptCode from Pakai,PakaiDetail,Dept where Pakai.ID=PakaiDetail.PakaiID and convert(varchar,Pakai.PakaiDate,112) >= '" + tgl1 + "' AND " +
            "convert(varchar,Pakai.PakaiDate,112) <= '" + tgl2 + "' and Status>-1 and PakaiDetail.RowStatus>-1 and PakaiDetail.ItemID=Inventory.ID and Pakai.DeptID=Dept.ID) " +
            " END DeptCode," +
            "CASE WHEN Inventory.ID > 0 THEN (select top 1 Pakai.PakaiNo from Pakai,PakaiDetail where Pakai.ID=PakaiDetail.PakaiID and convert(varchar,Pakai.PakaiDate,112) >= '" + tgl1 + "' AND " +
            "convert(varchar,Pakai.PakaiDate,112) <= '" + tgl2 + "' and Status>-1 and PakaiDetail.RowStatus>-1 and PakaiDetail.ItemID=Inventory.ID) " +
            " END NoDoc, " +
            "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and " +
            "PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND " +
            "convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian " +
            "FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (Inventory.GroupID in (" + groupID + ")) ) as AA " +
            "where Pemakaian>0 " +
            "union " +

            //--utk adjustTambah
            "select ItemID,ItemCode,ItemName,UOMID,UOMCode,0 as SaldoAwal,0 as Pemasukan,0 as Retur,AdjustTambah,0 as AdjustKurang,0 as Pemakaian," +
            "0 as StokAkhir,0 as DeptID,'' as DeptCode,'' as NoDoc,2 as Urutan,GroupID " +
            "from (SELECT Inventory.id as ItemID,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, " +
            "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND " +
            "AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND " +
            "convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah " +
            "FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (Inventory.GroupID in (" + groupID + ")) ) as AA " +
            "where AdjustTambah>0 " +
            "union " +

            //--utk adjustKurang
            "select ItemID,ItemCode,ItemName,UOMID,UOMCode,0 as SaldoAwal,0 as Pemasukan,0 as Retur,0 as AdjustTambah,AdjustKurang,0 as Pemakaian," +
            "0 as StokAkhir,0 as DeptID,'' as DeptCode,'' as NoDoc,2 as Urutan,GroupID " +
            "from (SELECT Inventory.id as ItemID,Inventory.ItemCode, Inventory.ItemName,Inventory.UOMID, UOM.UOMCode,Inventory.GroupID, " +
            "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND " +
            "AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND " +
            "convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang " +
            "FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (Inventory.GroupID in (" + groupID + ")) ) as AA " +
            "where AdjustKurang>0 " + "order by ItemCode,Urutan";
        }
        public string ViewRekapSPB(string tgl1, string tgl2, int GroupID, int DeptID, int VP)
        {
            string strGroupID = string.Empty;
            string strDeptid = string.Empty;
            string SaldoAwal = string.Empty;
            string ItemIDKertas = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ItemKertas" + ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID.ToString(), "SPB");
            int Bulan = int.Parse(tgl1.Substring(4, 2));
            int Tahun = int.Parse(tgl1.Substring(0, 4));
            if (Bulan == 1) { Tahun = Tahun - 1; }
            string jam = DateTime.Now.ToString("yyMMss");
            string UserView = ((Users)System.Web.HttpContext.Current.Session["Users"]).ID.ToString() + "_RPA" + jam;
            SaldoAwal = Global.nBulan((Bulan == 1) ? 12 : Bulan - 1, true) + "AvgPrice";
            string Groupsss = (GroupID > 0) ? " and GroupID in(" + GroupID + ")" : "";
            strGroupID = (GroupID > 0) ? " and PakaiDetail.GroupID=" + GroupID : string.Empty;
            strDeptid = (DeptID > 0) ? " and Pakai.DeptID=" + DeptID : string.Empty;
            int itemType = 0;
            switch (GroupID)
            {
                case 6: itemType = 2; break;//asset
                case 5: itemType = 3; break;//biaya
                default: itemType = 1; break;//inventory
            }
            #region proses posting avgprice 
            string sqlposting = "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto_lapmutasitmp_" + UserView + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto_lapmutasitmp_" + UserView + "]  " +
                                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto_lapmutasitmpx_" + UserView + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto_lapmutasitmpx_" + UserView + "]  " +
                                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto_lapmutasitmpxx_" + UserView + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto_lapmutasitmpxx_" + UserView + "]  " +
                                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto_lapmutasireport_" + UserView + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto_lapmutasireport_" + UserView + "]  " +
                                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto_mutasireport_" + UserView + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto_mutasireport_" + UserView + "]  " +
                                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto_mutasisaldo_" + UserView + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto_mutasisaldo_" + UserView + "]  " +
                                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto_lapsaldoawal_" + UserView + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto_lapsaldoawal_" + UserView + "]  " +
                                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto_mutasireport_" + UserView + "1]') AND type in (N'U')) DROP TABLE [dbo].[Auto_mutasireport_" + UserView + "1]  " +
                                "/** Kumpulkan data dari beberapa tabel dengan UNION ALL all */  " +
                                "declare @thbln char(6)  " +
                                "set @thbln=LEFT(convert(char,getdate(),112),6) " +
                                "declare @thnbln0 varchar(6)  " +
                                "declare @tgl datetime  " +
                                "declare @itemtypeID int " +
                                "set @itemtypeID= " + itemType +
                                "set @tgl=CAST( (@thbln+ '01') as datetime)  " +
                                "set @tgl= DATEADD(month,-1,@tgl)  " +
                                "set @thnbln0=LEFT(convert(char,@tgl,112),6)  " +
                                "declare @thnAwal  varchar(4) " +
                                "declare @blnAwal varchar(2) " +
                                "declare @AwalQty varchar(7) " +
                                "declare @AwalAvgPrice varchar(11) " +
                                "declare @tglawal varchar(8) " +
                                "set @tglawal='01-' + right(@thbln,2)+'-'+ LEFT(@thbln,4)  " +
                                "set @thnAwal =left(@thnbln0,4) " +
                                "set @blnAwal=RIGHT(@thnbln0,2) " +
                                "if right(@blnAwal,2)='01' begin set @AwalQty='janqty' set @AwalAvgPrice='janAvgprice'  end " +
                                "if right(@blnAwal,2)='02' begin set @AwalQty='febqty' set @AwalAvgPrice='febAvgprice'  end " +
                                "if right(@blnAwal,2)='03' begin set @AwalQty='marqty' set @AwalAvgPrice='marAvgprice'  end " +
                                "if right(@blnAwal,2)='04' begin set @AwalQty='aprqty' set @AwalAvgPrice='aprAvgprice'  end " +
                                "if right(@blnAwal,2)='05' begin set @AwalQty='meiqty' set @AwalAvgPrice='meiAvgprice'  end " +
                                "if right(@blnAwal,2)='06' begin set @AwalQty='junqty' set @AwalAvgPrice='junAvgprice'  end " +
                                "if right(@blnAwal,2)='07' begin set @AwalQty='julqty' set @AwalAvgPrice='julAvgprice'  end " +
                                "if right(@blnAwal,2)='08' begin set @AwalQty='aguqty' set @AwalAvgPrice='aguAvgprice'  end " +
                                "if right(@blnAwal,2)='09' begin set @AwalQty='sepqty' set @AwalAvgPrice='sepAvgprice'  end " +
                                "if right(@blnAwal,2)='10' begin set @AwalQty='oktqty' set @AwalAvgPrice='oktAvgprice'  end " +
                                "if right(@blnAwal,2)='11' begin set @AwalQty='novqty' set @AwalAvgPrice='novAvgprice'  end " +
                                "if right(@blnAwal,2)='12' begin set @AwalQty='desqty' set @AwalAvgPrice='desAvgprice'  end " +
                                "declare @thnCur  varchar(4) " +
                                "declare @blnCur varchar(2) " +
                                "declare @CurQty varchar(7) " +
                                "declare @CurAvgPrice varchar(11) " +
                                "set  @thnCur =left(@thbln,4) " +
                                "set @blnCur=RIGHT(@thbln,2) " +
                                "if right(@blnCur,2)='01' begin set @CurQty='janqty' set @CurAvgPrice='janAvgprice'  end " +
                                "if right(@blnCur,2)='02' begin set @CurQty='febqty' set @CurAvgPrice='febAvgprice'  end " +
                                "if right(@blnCur,2)='03' begin set @CurQty='marqty' set @CurAvgPrice='marAvgprice'  end " +
                                "if right(@blnCur,2)='04' begin set @CurQty='aprqty' set @CurAvgPrice='aprAvgprice'  end " +
                                "if right(@blnCur,2)='05' begin set @CurQty='meiqty' set @CurAvgPrice='meiAvgprice'  end " +
                                "if right(@blnCur,2)='06' begin set @CurQty='junqty' set @CurAvgPrice='junAvgprice'  end " +
                                "if right(@blnCur,2)='07' begin set @CurQty='julqty' set @CurAvgPrice='julAvgprice'  end " +
                                "if right(@blnCur,2)='08' begin set @CurQty='aguqty' set @CurAvgPrice='aguAvgprice'  end " +
                                "if right(@blnCur,2)='09' begin set @CurQty='sepqty' set @CurAvgPrice='sepAvgprice'  end " +
                                "if right(@blnCur,2)='10' begin set @CurQty='oktqty' set @CurAvgPrice='oktAvgprice'  end " +
                                "if right(@blnCur,2)='11' begin set @CurQty='novqty' set @CurAvgPrice='novAvgprice'  end " +
                                "if right(@blnCur,2)='12' begin set @CurQty='desqty' set @CurAvgPrice='desAvgprice'  end " +
                                "declare @sqlP nvarchar(max) " +
                                "set @sqlP='SELECT * into Auto_lapmutasitmp_" + UserView + " FROM(  " +
                                "(SELECT ''0'' AS Tipe,'''+@tglawal+''' AS Tanggal,''Saldo Awal'' AS DocNo,si.ItemID,si.'+@AwalAvgPrice+' AS SaldoHS,  " +
                                "si.'+@AwalQty+' AS SaldoQty,CASE WHEN ISNULL(si.'+@AwalAvgPrice+',0) >0 THEN si.'+@AwalAvgPrice+' ELSE 0 END AvgPrice,(si.'+@AwalQty+'*si.'+@AwalAvgPrice+') AS TotalPrice  " +
                                "FROM SaldoInventory AS si WHERE si.YearPeriod='+ @thnAwal +'  AND si.ItemTypeID=''1'' and ( " +
                                "si.ItemID IN(select distinct ItemID from vw_StockPurchn where YM='+@thbln+' " + Groupsss + " and (AvgPrice<=0 or Avgprice is null))) )  " +
                                "UNION ALL  " +
                                "(SELECT ''1'' AS Tipe,CONVERT(CHAR,Tanggal,105) AS Tanggal,ReceiptNo,ItemID, " +
                                "CASE WHEN x.Price >0 THEN " + //jika price=0
                                " CASE WHEN x.crc >1 then CASE WHEN x.Flag =2 Then  " + //jika kurs bukan rp dan supp harus bayar dolar ambil dari Matauangkurs
                                "  CASE WHEN MONTH(x.Tanggal)>8 AND YEAR(x.Tanggal) >=2016 THEN " + //di tambahkan untuk transaksi stlah bln ags 2016
                                "       (x.NilaiKurs * x.Price) ELSE " + //kurs diambil dari nilai kurs
                                "   ((select top 1 isnull(kurs,1)kurs from matauangkurs where muID=x.crc and drTgl =x.Tanggal )* x.Price) END ELSE " +
                                "   CASE WHEN x.NilaiKurs >0  " + //jika nilaikurs di table popurchn >0 kalikan dengan nilai kurs
                                "       THEN (x.NilaiKurs * x.Price) ELSE " + // jika nilai kurs =0 ambil dari table mataunga kurs base on tgl receipt
                                "       ((select top 1 isnull(kurs,1)kurs from matauangkurs where muID=x.crc and drTgl =x.Tanggal)* x.Price)  " +
                                "       END END ELSE x.Price END " +
                                "ELSE CASE WHEN x.Crc <=1 THEN x.HargaSatuan END END Price,Quantity,  " +
                                "CASE WHEN x.Price > 0 THEN " +
                                "CASE WHEN (x.Crc >1 ) THEN CASE WHEN x.flag=2 THEN " +
                                "  CASE WHEN MONTH(x.Tanggal)>8 AND YEAR(x.Tanggal) >=2016 THEN " +
                                "       (x.NilaiKurs * x.Price) ELSE ( " +
                                "           (select top 1 isnull(kurs,1)kurs from MataUangKurs where muID=x.crc and drTgl =x.Tanggal)*(isnull((x.Price),0))) " +
                                "       END ELSE CASE WHEN x.NilaiKurs>0 THEN (x.NilaiKurs * x.Price) ELSE  " +
                                "((select top 1 isnull(kurs,1)kurs from MataUangKurs where muID=x.crc and drTgl =x.Tanggal)* " +
                                "(isnull((x.Price),0))) END END ELSE isnull((x.Price),0) END ELSE CASE WHEN(x.Crc <=1) THEN  " +
                                "x.HargaSatuan END END AvgPrice," +
                                "CASE WHEN(x.Crc >1 ) THEN CASE WHEN x.flag=2 THEN  " +
                                "  CASE WHEN MONTH(x.Tanggal)>8 AND YEAR(x.Tanggal) >=2016 THEN " +
                                "       (x.NilaiKurs * x.Price) ELSE " +
                                "((select top 1 isnull(kurs,1)kurs from MataUangKurs where muID=x.crc and drTgl =x.Tanggal)* " +
                                "(isnull((x.Price*x.Quantity),0))) END ELSE  " +
                                "CASE WHEN x.NilaiKurs>0 THEN (x.NilaiKurs * x.Price*x.Quantity) ELSE  " +
                                "((select top 1 isnull(kurs,1)kurs from MataUangKurs where muID=x.crc and drTgl = x.Tanggal)*(isnull((x.Price*x.Quantity),0)))  " +
                                "END END ELSE CASE WHEN x.Price>0 THEN isnull((x.Price*x.Quantity),0) ELSE (x.HargaSatuan * x.Quantity)  " +
                                "END END TotalPrice " +
                                "FROM( SELECT ''1'' as Tipe, p.ReceiptDate as Tanggal,p.ReceiptNo ,rd.ItemID,  " +
                                "Case When pod.Price=0 then pod.Price2 else pod.Price end Price,rd.Quantity,  " +
                                "pod.crc,pod.flag,pod.NilaiKurs,ISNULL(pod.SubCompanyID,0)SubCompanyID,p.SupplierID,  " +
                                "(pod.Price*rd.Quantity) as TotalPrice,rd.POID,rd.ID as ReceiptID,pod.Price2 as HargaSatuan,bo.Qty  " +
                                "FROM Receipt as p LEFT JOIN ReceiptDetail as rd on rd.ReceiptID=p.ID LEFT JOIN vw_PObukanRP as pod on rd.PODetailID=pod.PODetailID  " +
                                "LEFT JOIN TabelHargaBankOut AS bo ON bo.ReceiptDetailID=rd.ID WHERE (left(convert(varchar,p.ReceiptDate,112),6)='+ @thbln +')  " +
                                "AND rd.ItemID IN (select distinct ItemID from vw_StockPurchn where YM='+@thbln+' " + Groupsss + " )  " +
                                "AND p.Status >-1 AND rd.RowStatus >-1 ) as x )  " +

                                "UNION ALL  " +
                                "(SELECT ''2'' AS Tipe,cONvert(VARCHAR,k.PakaiDate,105) AS Tanggal,k.PakaiNo,pk.ItemID,ISNULL(pk.AvgPrice,0)AvgPrice,pk.Quantity,  " +
                                "ISNULL(pk.AvgPrice,0) AS AvgPrice,(pk.Quantity*ISNULL(pk.AvgPrice,0)) AS TotalPrice FROM Pakai AS k LEFT JOIN PakaiDetail AS pk ON pk.PakaiID=k.ID  " +
                                "WHERE (left(cONvert(VARCHAR,k.PakaiDate,112),6)='+ @thbln +') AND pk.ItemID IN( " +
                                "select distinct ItemID from vw_StockPurchn where YM='+@thbln+' " + Groupsss + " and (AvgPrice<=0 or Avgprice is null))AND k.Status >-1 AND pk.RowStatus >-1 )  " +
                                "UNION ALL  " +
                                "(SELECT ''3'' AS Tipe, CONVERT(VARCHAR,rp.ReturDate,105) AS Tanggal,rp.ReturNo,rpd.ItemID,rpd.AvgPrice,rpd.Quantity,  " +
                                "(rpd.AvgPrice) AS AvgPrice,(rpd.Quantity*rpd.AvgPrice) AS TotalPrice FROM ReturPakai AS rp LEFT JOIN ReturPakaiDetail AS rpd  " +
                                "ON rpd.ReturID=rp.ID WHERE (left(cONvert(VARCHAR,rp.ReturDate,112),6)='+ @thbln +')  " +
                                "AND rpd.ItemID IN(select distinct ItemID from vw_StockPurchn where YM='+@thbln+' " + Groupsss + " and (AvgPrice<=0 or Avgprice is null)) AND rp.Status >-1 AND rpd.RowStatus >-1 )  " +
                                "UNION ALL  " +
                                "(SELECT ''4'' AS Tipe,CONVERT(VARCHAR,a.AdjustDate,105) AS Tanggal,  " +
                                "CASE when a.AdjustType=''Tambah'' THEN (a.AdjustNo)ELSE a.AdjustNo END NoM ,ad.ItemID,ad.AvgPrice As SaldoHS,  " +
                                "CASE When a.AdjustType=''Tambah'' THEN ad.Quantity ELSE ad.Quantity END AdjQty,''0'' AS AvgPrice, (ad.Quantity*ad.AvgPrice) AS TotalPrice  " +
                                "FROM Adjust AS a LEFT JOIN AdjustDetail AS ad ON ad.AdjustID=a.ID WHERE (left(cONvert(VARCHAR,a.AdjustDate,112),6)='+ @thbln +')  " +
                                "AND a.AdjustType=''Tambah'' AND ad.ItemID IN(select distinct ItemID from vw_StockPurchn where YM='+@thbln+' " + Groupsss + " and (AvgPrice<=0 or Avgprice is null))  " +
                                "AND a.Status >-1 AND ad.RowStatus >-1 ) " +
                                "UNION ALL  " +
                                "(SELECT ''5'' AS Tipe,CONVERT(VARCHAR,a.AdjustDate,105)  " +
                                "AS Tanggal, CASE when a.AdjustType=''Kurang'' THEN (a.AdjustNo)ELSE a.AdjustNo END NoM ,ad.ItemID,ad.AvgPrice,  " +
                                "CASE When a.AdjustType=''Kurang'' THEN ad.Quantity ELSE ad.Quantity END AdjQty,''0'' AS AvgPriceK, (ad.Quantity*ad.AvgPrice) AS TotalPriceK  " +
                                "FROM Adjust AS a LEFT JOIN AdjustDetail AS ad ON ad.AdjustID=a.ID WHERE (left(cONvert(VARCHAR,a.AdjustDate,112),6)='+ @thbln +')  " +
                                "AND a.AdjustType=''Kurang'' AND ad.ItemID IN(select distinct ItemID from vw_StockPurchn where YM='+@thbln+' " + Groupsss + " and (AvgPrice<=0 or Avgprice is null))  " +
                                "AND a.Status >-1 AND ad.RowStatus >-1 ) " +
                                "UNION ALL  " +
                                "(SELECT ''6'' AS Tipe,CONVERT(VARCHAR,rs.ReturDate,105) as Tanggal,rs.ReceiptNo,rsd.ItemID,rsd.Quantity as AvgPrice,rsd.Quantity,  " +
                                "CAST(''0'' AS Decimal(18,6)) AS AvgPrice ,CAST(''0'' AS Decimal(18,6)) AS Totalprice FROM ReturSupplier AS rs LEFT JOIN ReturSupplierDetail AS rsd  " +
                                "ON rsd.ReturID=rs.ID where (left(convert(varchar,rs.ReturDate,112),6)='+ @thbln +')  " +
                                "AND rsd.ItemID IN(select distinct ItemID from vw_StockPurchn where YM='+@thbln+' " + Groupsss + " and (AvgPrice<=0 or Avgprice is null)) AND rs.Status >-1  " +
                                "AND rsd.RowStatus >-1 ) ) as K' " +
                                "exec sp_executesql @sqlP, N'' " +
                                "/** susun sesuai dengan kolom laporan */  " +
                                "SELECT ID,Tipe,ItemID,Tanggal,DocNo,SaldoAwalQty,HPP,(SaldoAwalQty*HPP) AS SaAmt, BeliQty,BeliHS,(BeliQty*BeliHS) as BeliAmt,  " +
                                "AdjustQty,AdjustHS AS AdjustHS,(AdjustQty*HPP) as AdjAmt, ReturnQty,ReturHS AS ReturHS,(ReturnQty*HPP) as RetAmt,  " +
                                "ProdQty,ProdHS AS ProdHS,(ProdQty*HPP) as ProdAmt, AdjProdQty,AdjProdHS AS AdjProdHS,(AdjProdQty*HPP) as AdjPAmt,RetSupQty,  " +
                                "RetSupHS,(RetSupQty*RetSupHS) as RetSupAmt, (SaldoAwalQty+BeliQty+AdjustQty-ProdQty-AdjProdQty+ReturnQty-RetSupQty) as TotalQty,  " +
                                "((SaldoAwalQty*HPP)+(BeliQty*BeliHS)+(ReturnQty*ReturHS )+(AdjustQty*AdjustHS)- (ProdQty*ProdHS)-(AdjProdQty*AdjProdHS)-(RetSupQty*RetSupHs)) as TotalAmt  " +
                                "INTO Auto_lapmutasitmpx_" + UserView + " FROM ( SELECT ROW_NUMBER() OVER(ORDER By Tanggal,Tipe,DocNo) as ID,Tipe,itemID,Tanggal,DocNo,  " +
                                "CASE WHEN Tipe='0' THEN ISNULL(SaldoQty,0) ELSE 0 END SaldoAwalQty, CASE WHEN Tipe='0' THEN ISNULL(SaldoHS,0) ELSE 0 END HPP,  " +
                                "CASE WHEN Tipe='0' THEN ISNULL(TotalPrice,0) ELSE 0 END TotalPa, CASE WHEN Tipe='1' THEN ISNULL(SaldoQty,0) ELSE 0 END BeliQty,  " +
                                "CASE WHEN Tipe='1' THEN ISNULL(SaldoHS,0) ELSE 0 END BeliHS, CASE WHEN Tipe='1' THEN ISNULL(TotalPrice,0) ELSE 0 END TotalPB,  " +
                                "CASE WHEN Tipe='2' THEN ISNULL(SaldoQty,0) ELSE 0 END ProdQty, CASE WHEN Tipe='2' THEN ISNULL(SaldoHS,0) ELSE 0 END ProdHS,  " +
                                "CASE WHEN Tipe='2' THEN ISNULL(TotalPrice,0) ELSE 0 END TotalPAd, CASE WHEN Tipe='3' THEN ISNULL(SaldoQty,0) ELSE 0 END ReturnQty,  " +
                                "CASE WHEN Tipe='3' THEN ISNULL(SaldoHS,0) ELSE 0 END ReturHS, CASE WHEN Tipe='3' THEN ISNULL(TotalPrice,0) ELSE 0 END TotalPP,  " +
                                "CASE WHEN Tipe='4' THEN ISNULL(SaldoQty,0) ELSE 0 END AdjustQty, CASE WHEN Tipe='4' THEN ISNULL(SaldoHS,0) ELSE 0 END AdjustHS,  " +
                                "CASE WHEN Tipe='4' THEN ISNULL(TotalPrice,0) ELSE 0 END TotalPR, CASE WHEN Tipe='5' THEN ISNULL(SaldoQty,0) ELSE 0 END AdjProdQty,  " +
                                "CASE WHEN Tipe='5' THEN ISNULL(SaldoHS,0) ELSE 0 END AdjProdHS, CASE WHEN Tipe='5' THEN ISNULL(TotalPrice,0) ELSE 0 END TotalPAdjP,  " +
                                "CASE WHEN Tipe='6' THEN ISNULL(SaldoQty,0) ELSE 0 END RetSupQty, CASE WHEN Tipe='6' THEN ISNULL(SaldoHS,0) ELSE 0 END RetSupHS,  " +
                                "CASE WHEN Tipe='6' THEN ISNULL(TotalPrice,0) ELSE 0 END TotalRetSup FROM Auto_lapmutasitmp_" + UserView + " as x) AS Z ORDER BY z.Tanggal  " +
                                "/** susun data berdasarkan item id dan bentuk id baru */  " +
                                "SELECT ROW_NUMBER() OVER(PARTITION BY itemID ORDER BY itemID,ID,DocNo) as IDn,* INTO Auto_lapmutasitmpxx_" + UserView + " FROM Auto_lapmutasitmpx_" + UserView + "  " +
                                "/**Susun data tabular */  " +
                                "SELECT ROW_NUMBER() OVER(Partition BY itemID ORDER BY ID,Tanggal,Tipe,DocNo) AS IDn,ID,Tipe,itemID,Tanggal,DocNo,  " +
                                "BeliQty,BeliHS,(BeliQty*BeliHS) As BeliAmt,AdjustQty, CASE WHEN A.ID>1 AND A.AdjustQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0  " +
                                "THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM Auto_lapmutasitmpxx_" + UserView + " WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END AdjustHS,  " +
                                "ProdQty, CASE WHEN A.ID>1 AND A.ProdQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END  " +
                                "FROM Auto_lapmutasitmpxx_" + UserView + " WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END ProdHS, AdjProdQty,  " +
                                "CASE WHEN A.ID>1 AND A.AdjProdQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END  " +
                                "FROM Auto_lapmutasitmpxx_" + UserView + " WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END AdjProdHS, A.ReturnQty, CASE WHEN A.ID>1 AND A.ReturnQty >0  " +
                                "THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM Auto_lapmutasitmpxx_" + UserView + " WHERE ID <A.ID AND ItemID=A.ItemID)  " +
                                "ELSE A.HPP END ReturnHS, A.RetSupQty, CASE WHEN A.ID>1 AND A.RetSupQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0  " +
                                "THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM Auto_lapmutasitmpxx_" + UserView + " WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END RetSupHS,  " +
                                "CASE WHEN A.ID>1 THEN (SELECT SUM(totalqty) FROM Auto_lapmutasitmpxx_" + UserView + " WHERE ID <=A.ID AND ItemID=A.ItemID )ELSE TotalQty END SaldoAwalQty,  " +
                                "CASE WHEN A.ID>1 THEN CASE WHEN (SELECT SUM(totalqty)FROM Auto_lapmutasitmpxx_" + UserView + " WHERE ID <=A.ID AND ItemID=A.ItemID )>0 THEN  " +
                                "((SELECT SUM(totalamt) FROM Auto_lapmutasitmpxx_" + UserView + " WHERE ID <=A.ID AND ItemID=A.ItemID )/ (ABS((SELECT SUM(totalqty)FROM Auto_lapmutasitmpxx_" + UserView + "  " +
                                "WHERE ID <=A.ID AND ItemID=A.ItemID ))))ELSE 0 END ELSE HPP END HS, CASE WHEN A.ID>1 THEN (SELECT SUM(totalamt) FROM Auto_lapmutasitmpxx_" + UserView + "  " +
                                "WHERE ID <=A.ID AND ItemID=A.ItemID)ELSE Totalamt END TotalAmt INTO Auto_lapmutasireport_" + UserView + " FROM Auto_lapmutasitmpxx_" + UserView + " as A  " +
                                "ORDER by itemID,A.Tanggal,A.IDn,a.Tipe,a.DocNo  " +
                                "/** Generate Detail Report without saldo akhir */  " +
                                "SELECT ROW_NUMBER() OVER(Partition BY itemID ORDER BY Tanggal,Tipe)as ID, l.ItemID,l.Tanggal,l.DocNo,l.BeliQty,l.BeliHS,  " +
                                "(l.BeliQty*l.BeliHS) as BeliAmt,l.AdjustQty, CASE WHEN L.IDn >1 AND l.AdjustQty >0 THEN(SELECT HS FROM Auto_lapmutasireport_" + UserView + " WHERE IDn=(L.IDn-1)  " +
                                "AND ItemID=L.ItemID)ELSE 0 END AdjustHS, CASE WHEN L.IDn >1 AND l.AdjustQty >0 THEN(SELECT HS FROM Auto_lapmutasireport_" + UserView + " WHERE IDn=(L.IDn-1)  " +
                                "AND ItemID=L.ItemID)* L.AdjustQty ELSE 0 END AdjustAmt, l.ProdQty, CASE WHEN L.IDn >1 AND l.ProdQty >0 THEN (SELECT HS  " +
                                "FROM Auto_lapmutasireport_" + UserView + " WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END ProdHS, CASE WHEN L.IDn >1 AND l.ProdQty >0 THEN(SELECT HS  " +
                                "FROM Auto_lapmutasireport_" + UserView + " WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.ProdQty ELSE 0 END ProdAmt, l.AdjProdQty,  " +
                                "CASE WHEN L.IDn >1 AND l.AdjProdQty >0 THEN (SELECT HS FROM Auto_lapmutasireport_" + UserView + " WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END AdjProdHS,  " +
                                "CASE WHEN L.IDn >1 AND l.AdjProdQty >0 THEN(SELECT HS FROM Auto_lapmutasireport_" + UserView + " WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.AdjProdQty ELSE 0 END AdjProdAmt,  " +
                                "l.ReturnQty, CASE WHEN L.IDn >1 AND l.ReturnQty >0 THEN (SELECT HS FROM Auto_lapmutasireport_" + UserView + " WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0  " +
                                "END ReturnHS, CASE WHEN L.IDn >1 AND l.ReturnQty >0 THEN(SELECT HS FROM Auto_lapmutasireport_" + UserView + " WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.ReturnQty  " +
                                "ELSE 0 END returnAmt, l.RetSupQty, CASE WHEN L.IDn >1 AND l.RetSupQty >0 THEN (SELECT HS FROM Auto_lapmutasireport_" + UserView + " WHERE IDn=(L.IDn-1)  " +
                                "AND ItemID=L.ItemID)ELSE 0 END RetSupHS, CASE WHEN L.IDn >1 AND l.RetSupQty >0 THEN(SELECT HS FROM Auto_lapmutasireport_" + UserView + " WHERE IDn=(L.IDn-1) AND  " +
                                "ItemID=L.ItemID)* L.RetSupQty ELSE 0 END RetSupAmt, l.SaldoAwalQty,l.HS,l.TotalAmt INTO Auto_mutasireport_" + UserView + " FROM Auto_lapmutasireport_" + UserView + " AS L  " +
                                "ORDER BY L.itemID,L.Tipe,L.Tanggal  " +
                                "/** update colom amt dan colom hs */  " +
                                "select row_number() over(order by itemID) as IDn,itemid into Auto_mutasireport_" + UserView + "1 from Auto_mutasireport_" + UserView + " group by itemID order by itemid  " +
                                "declare @sqlSet nvarchar(max) " +
                                "set @sqlSet =' " +
                                "declare @i int declare @b int declare @hs decimal(18,6) declare @amt decimal(18,6) declare @avgp decimal(18,6)  " +
                                "declare @c int declare @itm int declare @itmID int  set @c=0 set @itm=(select COUNT(IDn) from Auto_mutasireport_" + UserView + "1)  " +
                                "While @c <=@itm Begin set @b=0; set @itmID=(select isnull(itemID,0) from Auto_mutasireport_" + UserView + "1 where IDn=@c)  " +
                                "set @avgp=(select top 1 '+ @AwalAvgPrice +' from SaldoInventory where ItemID = @itmID and YearPeriod='+ @thnAwal +') " +
                                "if ISNULL(@avgp,0)=0 " +
                                "begin " +
                                "set @avgp=(select top 1 HS from Auto_mutasireport_" + UserView + " where itemid=@itmID and HS>0 ) " +
                                "end " +
                                "set @i=(select COUNT(id) from Auto_mutasireport_" + UserView + " where itemid=@itmID) While @b<=@i  " +
                                "Begin set @hs=CASE WHEN @b >1 THEN (select hs from Auto_mutasireport_" + UserView + " where ID=(@b) and itemid=@itmID)  " +
                                "ELSE CASE WHEN(SELECT hs from Auto_mutasireport_" + UserView + " where ID=1 and itemid=@itmID)>0 THEN 	  " +
                                "(SELECT hs from Auto_mutasireport_" + UserView + " where ID=1 and itemid=@itmID)ELSE @avgp END 	 END  " +
                                "set @amt=CASE WHEN @b >1 THEN (select TotalAmt from Auto_mutasireport_" + UserView + " where ID=(@b) and itemid=@itmID)  " +
                                "eLSE (SELECT TotalAmt from Auto_mutasireport_" + UserView + " where ID=1 and itemid=@itmID) END  " +
                                 /** update semua hs */
                                "update Auto_mutasireport_" + UserView + " set AdjustHS	=CASE WHEN (SELECT AdjustQty FROM Auto_mutasireport_" + UserView + " WHERE ID=(@b+1) and itemid=@itmID)>0  " +
                                "THEN @hs ELSE 0 END, ProdHS		=CASE WHEN (SELECT ProdQty FROM Auto_mutasireport_" + UserView + " WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END,  " +
                                "ReturnHS=CASE WHEN (SELECT ReturnQty FROM Auto_mutasireport_" + UserView + " WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END,  " +
                                "AdjProdHS=CASE WHEN (SELECT AdjProdQty FROM Auto_mutasireport_" + UserView + " WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END,  " +
                                "RetSupHS=CASE WHEN (SELECT RetSupQty FROM Auto_mutasireport_" + UserView + " WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END,  " +
                                "ProdAmt=(ProdQty*@hs), AdjustAmt =(AdjustQty*@hs), AdjProdAmt =(AdjProdQty*@hs), returnAmt =(ReturnQty*@hs),  " +
                                "RetSupAmt =(RetSupQty*@hs), totalamt =((BeliQty*BeliHs)+(adjustQty*@hs)+(returnQty*@hs)-(ProdQty*@hs)-(AdjprodQty*@hs)+(@amt)),  " +
                                "hs=case when abs(SaldoAwalQty)>0 then (((beliQty*BeliHs)+(adjustQty*@hs)+(returnQty*@hs)-(ProdQty*@hs)-(AdjprodQty*@hs)+@amt )/SaldoAwalQty)  " +
                                "else @avgp end where ID=(@b+1) and itemid=@itmID set @b=@b+1 END set @c=@c+1 END' " +
                                "exec sp_executesql @sqlSet, N'' " +
                                "/** Generate Saldo Awal */  " +
                                "SELECT ItemID,SaldoAwalQty,HS,TotalAmt INTO Auto_lapsaldoawal_" + UserView + " FROM Auto_mutasireport_" + UserView + " as m WHERE m.DocNo='Saldo Awal'  " +
                                "/** Generate Saldo Akhir */ " +
                                "SELECT (COUNT(m.itemID)+1)as ID,m.itemID, ' ' as Tanggal,'Total' as DocNo, /** pembelian */ (SUM(m.BeliQty)) As BeliQty, " +
                                "CASE WHEN SUM(m.BeliAmt) > 0 THEN (SUM(m.BeliAmt)/SUM(m.BeliQty))ELSE 0 END BeliHS, (SUM(m.BeliAmt)) As BeliAmt, " +
                                "/** Ajdut Plust */ (SUM(m.AdjustQty)) As AdjustQty, CASE WHEN SUM(m.AdjustAmt) > 0 THEN (SUM(m.AdjustAmt)/SUM(m.AdjustQty))ELSE 0 END  " +
                                "AdjustHS, (SUM(m.AdjustAmt)) As AdjustAmt,  " +
                                "/** Pemakaian Produksi */  " +
                                "(SUM(m.ProdQty)) As ProdQty, " +
                                "CASE WHEN SUM(m.ProdAmt) > 0 THEN (SUM(m.ProdAmt)/SUM(m.ProdQty))ELSE 0 END ProdHS, (SUM(m.ProdAmt)) As ProdAmt, " +
                                "/** Adjut minus */ " +
                                "(SUM(m.AdjProdQty)) As AdjProdQty, CASE WHEN SUM(m.AdjProdAmt) > 0 THEN (SUM(m.AdjProdAmt)/SUM(m.AdjProdQty))ELSE 0 END AdjProdHS,  " +
                                "(SUM(m.AdjProdAmt)) As AdjProdAmt, /** Return */ (SUM(m.ReturnQty)) As ReturnQty, CASE WHEN SUM(m.returnAmt) > 0 THEN  " +
                                "(SUM(m.returnAmt)/SUM(m.ReturnQty))ELSE 0 END ReturnHS, (SUM(m.returnAmt)) As returnAmt, " +
                                "/** Return Supplier */ " +
                                "(SUM(m.RetSupQty)) As RetSupQty, CASE WHEN SUM(m.RetSupQty) > 0 THEN (SUM(m.RetSupAmt)/ABS(SUM(m.RetSupQty)))ELSE 0 END RetSupHS, (SUM(m.RetSupAmt))  " +
                                "As RetSupAmt,  " +
                                "/** Saldo Akhir */ " +
                                "(SELECT TOP 1 SaldoAwalQty FROM Auto_mutasireport_" + UserView + " AS b WHERE b.itemID=m.itemID ORDER BY b.ID DESC) As SaldoAwalQty, " +
                                "(SELECT TOP 1 HS FROM Auto_mutasireport_" + UserView + " AS b WHERE b.itemID=m.itemID ORDER BY b.ID DESC) As HS, " +
                                "CASE when (SELECT TOP 1 SaldoAwalQty FROM Auto_mutasireport_" + UserView + " AS b WHERE b.itemID=m.itemID ORDER BY b.ID DESC)>0 then " +
                                "(SELECT TOP 1 TotalAmt FROM Auto_mutasireport_" + UserView + " AS b WHERE b.itemID=m.itemID ORDER BY b.ID DESC) ELSE 0 END As TotalAmt " +
                                "INTO Auto_mutasisaldo_" + UserView + " FROM Auto_mutasireport_" + UserView + " AS m GROUP BY m.ItemID " +
                                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto_UpdateAvgPrice_" + UserView + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto_UpdateAvgPrice_" + UserView + "]  " +
                                "/** Kumpulkan data update average price*/  " +
                                "SELECT  " +
                                "CASE WHEN m.ProdQty >0 THEN (SELECT TOP 1 ID FROM Pakai WHERE Pakai.PakaiNo=m.DocNo)  " +
                                "WHEN m.AdjustQty >0 THEN (SELECT TOP 1 ID FROM Adjust WHERE Adjust.AdjustNo=m.DocNo AND Adjust.AdjustType='Tambah')  " +
                                "WHEN m.AdjProdQty >0 THEN (SELECT TOP 1 ID FROM Adjust WHERE Adjust.AdjustNo=m.DocNo AND Adjust.AdjustType='Kurang')  " +
                                "WHEN m.ReturnQty >0 THEN (SELECT TOP 1 ID FROM ReturPakai WHERE ReturPakai.ReturNo=m.DocNo)  " +
                                "WHEN m.RetSupQty >0 THEN (SELECT TOP 1 ID FROM ReturSupplier WHERE ReturSupplier.ReturNo=m.DocNo)  " +
                                "WHEN m.BeliQty > 0 THEN (SELECT TOP 1 ID FROM Receipt WHERE Receipt.ReceiptNo=m.DocNo)END ID,  " +
                                "CASE WHEN m.ProdQty >0 THEN m.ItemID WHEN m.AdjustQty >0 THEN m.ItemID WHEN m.AdjProdQty >0 THEN m.ItemID WHEN m.ReturnQty >0 THEN m.ItemID " +
                                "WHEN m.RetSupQty >0 THEN m.ItemID WHEN m.BeliQty >0 THEN m.ItemID END itemID,  " +
                                "CASE WHEN m.ProdQty >0 THEN m.ProdHS WHEN m.AdjustQty >0 THEN m.AdjustHS " +
                                "WHEN m.AdjProdQty >0 THEN m.AdjProdHS WHEN m.ReturnQty >0 THEN m.ReturnHS WHEN m.RetSupQty >0 THEN m.RetSupHS WHEN m.BeliQty >0 THEN m.BeliHS END AvgPrice, " +
                                "CASE WHEN m.ProdQty >0 THEN 'PakaiDetail' WHEN m.AdjustQty>0 THEN 'AdjustDetailT' WHEN m.AdjProdQty>0 THEN 'AdjustDetailK'  " +
                                "WHEN m.ReturnQty >0 THEN 'ReturPakaiDetail' WHEN m.RetSupQty>0 THEN 'ReturSupplierDetail' WHEN m.BeliQty>0 THEN 'ReceiptDetail'  " +
                                "END Tabel INTO Auto_UpdateAvgPrice_" + UserView + " FROM Auto_mutasireport_" + UserView + " as m  " +
                                "/** update avgprice setiap tabel */ /** Produksi */  " +
                                "UPDATE p SET p.AvgPrice=a.AvgPrice FROM PakaiDetail as p INNER JOIN Auto_UpdateAvgPrice_" + UserView + " as a ON P.PakaiID=a.ID WHERE a.Tabel='PakaiDetail' and  " +
                                "p.ItemID=a.itemID  if @itemtypeID='3' begin " +
                                "update PakaiDetail set AvgPrice=(SELECT TOP 1 vw_HargaReceipt.Price FROM vw_HargaReceipt where vw_HargaReceipt.ItemID=PakaiDetail.ItemID    " +
                                "and vw_HargaReceipt.ItemTypeID=3 and vw_HargaReceipt.ReceiptDate<=(select pakaidate from Pakai where ID=PakaiDetail.PakaiID))   " +
                                "where PakaiID in (select ID from Pakai where ItemTypeID=3 and LEFT(convert(char,pakaidate,112),6)= @thbln) end " +
                                "/** penerimaan*/  " +
                                "UPDATE p SET p.AvgPrice=a.AvgPrice FROM ReceiptDetail as p INNER JOIN Auto_UpdateAvgPrice_" + UserView + " as a ON P.ReceiptID=a.ID  " +
                                "WHERE a.Tabel='ReceiptDetail' and p.ItemID=a.itemID /**penyesuaian produksi */ UPDATE p SET p.AvgPrice=a.AvgPrice FROM ReturPakaiDetail as p  " +
                                "INNER JOIN Auto_UpdateAvgPrice_" + UserView + " as a ON P.ReturID=a.ID WHERE a.Tabel='ReturPakaiDetail' and p.ItemID=a.itemID /** adjust Tambah */  " +
                                "UPDATE p SET p.AvgPrice=a.AvgPrice FROM AdjustDetail as p INNER JOIN Auto_UpdateAvgPrice_" + UserView + " as a ON P.AdjustID=a.ID WHERE a.Tabel='AdjustDetailT'  " +
                                "and p.ItemID=a.itemID /** Adjust Kurang */ UPDATE p SET p.AvgPrice=a.AvgPrice FROM AdjustDetail as p INNER JOIN Auto_UpdateAvgPrice_" + UserView + " as a ON P.AdjustID=a.ID  " +
                                "WHERE a.Tabel='AdjustDetailK' and p.ItemID=a.itemID  " +
                                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto_UpdateAvgPrice_" + UserView + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto_UpdateAvgPrice_" + UserView + "]  ";
                                
            #endregion
            #region proses report pemakaian
            string strSQL =  "select PakaiNo,convert(varchar,PakaiDate,103) as PakaiDate,ItemCode,ItemName,UOMCode,SUM(Quantity) as Jumlah, " +
                          "  case when crc >1 THEN " +
                          "  (/*(select Top 1 Kurs from MataUangKurs where MUID=crc and  sdTgl<=PakaiDate order by ID desc)*/Harga) " +
                          "   else " +
                          "   isnull(Harga,0) end Harga,Case When ProdLine=0 Then Keterangan else 'Prod. Line '+ Cast(ProdLine as Char(2))+'-'+Keterangan end Keterangan," +
                          "   /*Keterangan ,*/GroupID,GroupDescription,DeptName,Status, " +
                          "   crc,ItemID " +
                          "    from  " +
                          "  (SELECT  " +
                          "      isnull((select top 1 crc from POPurchn where POPurchn.ID in(select (POID) from ReceiptDetail where ReceiptDetail.ItemID=PakaiDetail.ItemID  " +
                          "      and ReceiptID in(select ID from Receipt where month(receipt.ReceiptDate) <= month(pakai.PakaiDate) and  " +
                          "      YEAR(receipt.ReceiptDate)<=YEAR(pakai.PakaiDate ))) order by POPurchn.ID desc),1)as crc, " +
                          "      CASE PakaiDetail.ItemTypeID   " +
                          "          WHEN 1 THEN (SELECT ItemCode FROM Inventory WHERE ID = PakaiDetail.ItemID)   " +
                          "          WHEN 2 THEN (SELECT ItemCode FROM Asset WHERE ID = PakaiDetail.ItemID)   " +
                          "          ELSE (SELECT ItemCode FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS ItemCode,   " +
                          "      CASE PakaiDetail.ItemTypeID   " +
                          "          WHEN 1 THEN (SELECT ItemName FROM Inventory WHERE ID = PakaiDetail.ItemID)   " +
                          "          WHEN 2 THEN (SELECT ItemName FROM Asset WHERE ID = PakaiDetail.ItemID)  " +
                          "          ELSE (SELECT ItemName FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS ItemName,   " +
                          "      CASE PakaiDetail.ItemTypeID   " +
                          "          WHEN 1 THEN (SELECT GroupID FROM Inventory WHERE ID = PakaiDetail.ItemID)   " +
                          "          WHEN 2 THEN (SELECT GroupID FROM Asset WHERE ID = PakaiDetail.ItemID)   " +
                          "          ELSE (SELECT GroupID FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS GroupID,   " +
                          "      CASE WHEN PakaiDetail.ItemTypeID=3 THEN " +
                          "          ( " +
                          "          SELECT TOP 1 vw_HargaReceipt.Price FROM vw_HargaReceipt where vw_HargaReceipt.ItemID=PakaiDetail.ItemID  " +
                          "          and vw_HargaReceipt.ItemTypeID=3 and vw_HargaReceipt.ReceiptDate<=Pakai.PakaiDate order by id desc " +
                          "          ) " +
                          "      ELSE " +
                          "      ISNULL(PakaiDetail.AvgPrice,  " +
                          "      (select top 1 " + SaldoAwal + " From SaldoInventory where SaldoInventory.ItemID=PakaiDetail.ItemID and YearPeriod=" + Tahun.ToString() + "))	  " +
                          "      END Harga,        " +
                          "      CASE when PakaiDetail.GroupID>0 THEN  " +
                          "          (SELECT GroupDescription FROM GroupsPurchn WHERE GroupsPurchn.ID = PakaiDetail.GroupID)  ELSE 'xxx' END AS GroupDescription,   " +
                          "      CASE Pakai.Status  WHEN 0 THEN ('Open')   " +
                          "          WHEN 1 THEN ('Head')  ELSE ('Gudang') END AS Status,PakaiDetail.ItemID, " +
                          "      PakaiDetail.Quantity,PakaiDetail.Keterangan,Pakai.PakaiNo,UOM.UOMCode,Pakai.PakaiDate," +
                          "      Pakai.DeptID,Dept.DeptName,PakaiDetail.ProdLine  " +
                          "      FROM Pakai  " +
                          "      INNER JOIN PakaiDetail  " +
                          "      ON Pakai.ID = PakaiDetail.PakaiID and PakaiDetail.RowStatus>-1  " +
                          "      INNER JOIN UOM  " +
                          "      ON PakaiDetail.UomID = UOM.ID and UOM.RowStatus>-1  " +
                          "      INNER JOIN Dept  " +
                          "      ON Pakai.DeptID = Dept.ID and Dept.RowStatus>-1  " +
                          "      where Pakai.status=3 and convert(varchar,Pakai.PakaiDate,112)>='" + tgl1 + "' and   " +
                          "      convert(varchar,Pakai.PakaiDate,112)<='" + tgl2 + "' " +
                                 strGroupID + strDeptid + ") as AA  " +
                          "      group by DeptName,ItemCode,ItemName,UOMCode,Harga,GroupID,GroupDescription,PakaiNo," +
                          "      PakaiDate,Status,Keterangan,ItemID,crc,ProdLine " +
                          "      order by GroupID,ItemName,ItemCode ";
           
            #region hapus temporary table
            //      strSQL += "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto_lapmutasitmp_" + UserView + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto_lapmutasitmp_" + UserView + "]  " +
            //                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto_lapmutasitmpx_" + UserView + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto_lapmutasitmpx_" + UserView + "]  " +
            //                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto_lapmutasitmpxx_" + UserView + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto_lapmutasitmpxx_" + UserView + "]  " +
            //                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto_lapmutasireport_" + UserView + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto_lapmutasireport_" + UserView + "]  " +
            //                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto_mutasireport_" + UserView + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto_mutasireport_" + UserView + "]  " +
            //                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto_mutasisaldo_" + UserView + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto_mutasisaldo_" + UserView + "]  " +
            //                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto_lapsaldoawal_" + UserView + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto_lapsaldoawal_" + UserView + "]";
            #endregion
            #endregion
            return strSQL; 
        }
        public string ViewRekapSPBR(string tgl1, string tgl2, int GroupID, int DeptID, int VP)
        {
            string strGroupID = string.Empty;
            string strDeptid = string.Empty;
            string SaldoAwal = string.Empty;
            string ItemIDKertas = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ItemKertas" + ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID.ToString(), "SPB");
            int Bulan = int.Parse(tgl1.Substring(4, 2));
            int Tahun = int.Parse(tgl1.Substring(0, 4));
            if (Bulan == 1) { Tahun = Tahun - 1; }
            SaldoAwal = Global.nBulan((Bulan == 1) ? 12 : Bulan - 1, true) + "AvgPrice";
            strGroupID = (GroupID > 0) ? " and PakaiDetail.GroupID=" + GroupID : string.Empty;
            strDeptid = (DeptID > 0) ? " and Pakai.DeptID=" + DeptID : string.Empty;
            string strSQL = "select GroupID,GroupDescription,ItemCode,ItemName,UOMCode,sum(Jumlah)Jumlah,avg(Harga)Harga,DeptName,crc,ItemID from ( "+
                          "select PakaiNo,convert(varchar,PakaiDate,103) as PakaiDate,ItemCode,ItemName,UOMCode,SUM(Quantity) as Jumlah, " +
                          "  case when crc >1 THEN " +
                          "  (/*(select Top 1 Kurs from MataUangKurs where MUID=crc and  sdTgl<=PakaiDate order by ID desc)*/Harga) " +
                          "   else " +
                          "   isnull(Harga,0) end Harga,Case When ProdLine=0 Then Keterangan else 'Prod. Line '+ Cast(ProdLine as Char(2))+'-'+Keterangan end Keterangan," +
                          "   /*Keterangan ,*/GroupID,GroupDescription,DeptName,Status, " +
                          "   crc,ItemID " +
                          "    from  " +
                          "  (SELECT  " +
                          "      isnull((select top 1 crc from POPurchn where POPurchn.ID in(select (POID) from ReceiptDetail where ReceiptDetail.ItemID=PakaiDetail.ItemID  " +
                          "      and ReceiptID in(select ID from Receipt where month(receipt.ReceiptDate) <= month(pakai.PakaiDate) and  " +
                          "      YEAR(receipt.ReceiptDate)<=YEAR(pakai.PakaiDate ))) order by POPurchn.ID desc),1)as crc, " +
                          "      CASE PakaiDetail.ItemTypeID   " +
                          "          WHEN 1 THEN (SELECT ItemCode FROM Inventory WHERE ID = PakaiDetail.ItemID)   " +
                          "          WHEN 2 THEN (SELECT ItemCode FROM Asset WHERE ID = PakaiDetail.ItemID)   " +
                          "          ELSE (SELECT ItemCode FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS ItemCode,   " +
                          "      CASE PakaiDetail.ItemTypeID   " +
                          "          WHEN 1 THEN (SELECT ItemName FROM Inventory WHERE ID = PakaiDetail.ItemID)   " +
                          "          WHEN 2 THEN (SELECT ItemName FROM Asset WHERE ID = PakaiDetail.ItemID)  " +
                          "          ELSE (SELECT ItemName FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS ItemName,   " +
                          "      CASE PakaiDetail.ItemTypeID   " +
                          "          WHEN 1 THEN (SELECT GroupID FROM Inventory WHERE ID = PakaiDetail.ItemID)   " +
                          "          WHEN 2 THEN (SELECT GroupID FROM Asset WHERE ID = PakaiDetail.ItemID)   " +
                          "          ELSE (SELECT GroupID FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS GroupID,   " +
                          "      CASE WHEN PakaiDetail.ItemTypeID=3 THEN " +
                          "          ( " +
                          "          SELECT TOP 1 vw_HargaReceipt.Price FROM vw_HargaReceipt where vw_HargaReceipt.ItemID=PakaiDetail.ItemID  " +
                          "          and vw_HargaReceipt.ItemTypeID=3 and vw_HargaReceipt.ReceiptDate<=Pakai.PakaiDate order by id desc" +
                          "          ) " +
                          "      ELSE " +
                          "      ISNULL(PakaiDetail.AvgPrice,  " +
                          "      (select top 1 " + SaldoAwal + " From SaldoInventory where SaldoInventory.ItemID=PakaiDetail.ItemID and YearPeriod=" + Tahun.ToString() + "))	  " +
                          "      END Harga,        " +
                          "      CASE when PakaiDetail.GroupID>0 THEN  " +
                          "          (SELECT GroupDescription FROM GroupsPurchn WHERE GroupsPurchn.ID = PakaiDetail.GroupID)  ELSE 'xxx' END AS GroupDescription,   " +
                          "      CASE Pakai.Status  WHEN 0 THEN ('Open')   " +
                          "          WHEN 1 THEN ('Head')  ELSE ('Gudang') END AS Status,PakaiDetail.ItemID, " +
                          "      PakaiDetail.Quantity,PakaiDetail.Keterangan,Pakai.PakaiNo,UOM.UOMCode,Pakai.PakaiDate," +
                          "      Pakai.DeptID,Dept.DeptName,PakaiDetail.ProdLine  " +
                          "      FROM Pakai  " +
                          "      INNER JOIN PakaiDetail  " +
                          "      ON Pakai.ID = PakaiDetail.PakaiID and PakaiDetail.RowStatus>-1  " +
                          "      INNER JOIN UOM  " +
                          "      ON PakaiDetail.UomID = UOM.ID and UOM.RowStatus>-1  " +
                          "      INNER JOIN Dept  " +
                          "      ON Pakai.DeptID = Dept.ID and Dept.RowStatus>-1  " +
                          "      where Pakai.status=3 and convert(varchar,Pakai.PakaiDate,112)>='" + tgl1 + "' and   " +
                          "      convert(varchar,Pakai.PakaiDate,112)<='" + tgl2 + "' " +
                                 strGroupID + strDeptid + ") as AA  " +
                          "      group by DeptName,ItemCode,ItemName,UOMCode,Harga,GroupID,GroupDescription,PakaiNo," +
                          "      PakaiDate,Status,Keterangan,ItemID,crc,ProdLine " +
                          "      ) A group by GroupID,GroupDescription,ItemCode,ItemName,UOMCode,DeptName,crc,ItemID order by GroupDescription, ItemName ";
            return strSQL;
        }
        public string ViewRekapPakai(string tgl1, string tgl2, int groupID, int deptID)
        {
            string strGroupID = string.Empty;
            string strDeptid = string.Empty;
            if (groupID > 0)
                strGroupID = " and PakaiDetail.GroupID=" + groupID;
            if (deptID > 0)
                strDeptid = "and Pakai.DeptID=" + deptID + " ";

            return "select PakaiNo,convert(varchar,PakaiDate,103) as PakaiDate,ItemCode,ItemName,UOMCode,SUM(Quantity) as Jumlah,isnull(Harga,0) as Harga,Keterangan,GroupID,GroupDescription,DeptName,Status from (" +
                "SELECT CASE PakaiDetail.ItemTypeID  WHEN 1 THEN (SELECT ItemCode FROM Inventory WHERE ID = PakaiDetail.ItemID)  " +
                "WHEN 2 THEN (SELECT ItemCode FROM Asset WHERE ID = PakaiDetail.ItemID)  ELSE " +
                "(SELECT ItemCode FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS ItemCode,  " +
                "CASE PakaiDetail.ItemTypeID  WHEN 1 THEN (SELECT ItemName FROM Inventory WHERE ID = PakaiDetail.ItemID)  " +
                "WHEN 2 THEN (SELECT ItemName FROM Asset WHERE ID = PakaiDetail.ItemID)  ELSE " +
                "(SELECT ItemName FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS ItemName,  " +
                "CASE PakaiDetail.ItemTypeID  WHEN 1 THEN (SELECT GroupID FROM Inventory WHERE ID = PakaiDetail.ItemID)  " +
                "WHEN 2 THEN (SELECT GroupID FROM Asset WHERE ID = PakaiDetail.ItemID)  ELSE " +
                "(SELECT GroupID FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS GroupID,  " +
                "CASE WHEN PakaiDetail.ItemID>0 and (select isnull(harga,0) from Inventory where ID=PakaiDetail.ItemID)=0 THEN (SELECT top 1 isnull(POPurchnDetail.Price,0) FROM POPurchn,POPurchnDetail  " +
                "WHERE POPurchn.ID=POPurchnDetail.POID and POPurchnDetail.ItemID=PakaiDetail.ItemID and POPurchnDetail.GroupID=PakaiDetail.GroupID and POPurchn.Status>-1 and " +
                "POPurchnDetail.Status>-1 order by POPurchnDate Desc) else 0 end Harga," +
                "CASE when PakaiDetail.GroupID>0 THEN (SELECT GroupDescription FROM GroupsPurchn WHERE GroupsPurchn.ID = PakaiDetail.GroupID)  " +
                "ELSE 'xxx' END AS GroupDescription,  " +
                "CASE Pakai.Status  WHEN 0 THEN ('Open')  " +
                "WHEN 1 THEN ('Head')  ELSE " +
                "('Gudang') END AS Status," +
                "PakaiDetail.Quantity,PakaiDetail.Keterangan,Pakai.PakaiNo,UOM.UOMCode,Pakai.PakaiDate,Pakai.DeptID,Dept.DeptName FROM Pakai " +
                "INNER JOIN PakaiDetail ON Pakai.ID = PakaiDetail.PakaiID and PakaiDetail.RowStatus>-1 " +
                "INNER JOIN UOM ON PakaiDetail.UomID = UOM.ID and UOM.RowStatus>-1 " +
                "INNER JOIN Dept ON Pakai.DeptID = Dept.ID and Dept.RowStatus>-1 " +
                "where Pakai.status=3 and convert(varchar,Pakai.PakaiDate,112)>='" + tgl1 + "' and  convert(varchar,Pakai.PakaiDate,112)<=" + tgl2 +
                strDeptid + strGroupID + ") as AA group by DeptName,ItemCode,ItemName,UOMCode,Harga,GroupID,GroupDescription,PakaiNo,PakaiDate,Status,Keterangan order by GroupID,ItemCode";
        }
        public string ViewRekapPakaiByPrice0(string tgl1, string tgl2, int groupID, int deptID)
        {
            string strGroupID = string.Empty;
            string strDeptid = string.Empty;
            if (groupID > 0)
                strGroupID = " and PakaiDetail.GroupID=" + groupID;
            if (deptID > 0)
                strDeptid = "and Pakai.DeptID=" + deptID + " ";

            return "select PakaiNo,convert(varchar,PakaiDate,103) as PakaiDate,ItemCode,ItemName,UOMCode,SUM(Quantity) as Jumlah,isnull(Harga,0) as Harga,Keterangan,GroupID,GroupDescription,DeptName,Status from (" +
                "SELECT CASE PakaiDetail.ItemTypeID  WHEN 1 THEN (SELECT ItemCode FROM Inventory WHERE ID = PakaiDetail.ItemID)  " +
                "WHEN 2 THEN (SELECT ItemCode FROM Asset WHERE ID = PakaiDetail.ItemID)  ELSE " +
                "(SELECT ItemCode FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS ItemCode,  " +
                "CASE PakaiDetail.ItemTypeID  WHEN 1 THEN (SELECT ItemName FROM Inventory WHERE ID = PakaiDetail.ItemID)  " +
                "WHEN 2 THEN (SELECT ItemName FROM Asset WHERE ID = PakaiDetail.ItemID)  ELSE " +
                "(SELECT ItemName FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS ItemName,  " +
                "CASE PakaiDetail.ItemTypeID  WHEN 1 THEN (SELECT GroupID FROM Inventory WHERE ID = PakaiDetail.ItemID)  " +
                "WHEN 2 THEN (SELECT GroupID FROM Asset WHERE ID = PakaiDetail.ItemID)  ELSE " +
                "(SELECT GroupID FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS GroupID,  " +
                " 0 as Harga," +
                "CASE when PakaiDetail.GroupID>0 THEN (SELECT GroupDescription FROM GroupsPurchn WHERE GroupsPurchn.ID = PakaiDetail.GroupID)  " +
                "ELSE 'xxx' END AS GroupDescription,  " +
                "CASE Pakai.Status  WHEN 0 THEN ('Open')  " +
                "WHEN 1 THEN ('Head')  ELSE " +
                "('Gudang') END AS Status," +
                "PakaiDetail.Quantity,PakaiDetail.Keterangan,Pakai.PakaiNo,UOM.UOMCode,Pakai.PakaiDate,Pakai.DeptID,Dept.DeptName FROM Pakai " +
                "INNER JOIN PakaiDetail ON Pakai.ID = PakaiDetail.PakaiID and PakaiDetail.RowStatus>-1 " +
                "INNER JOIN UOM ON PakaiDetail.UomID = UOM.ID and UOM.RowStatus>-1 " +
                "INNER JOIN Dept ON Pakai.DeptID = Dept.ID and Dept.RowStatus>-1 " +
                "where  Pakai.status=3 and convert(varchar,Pakai.PakaiDate,112)>='" + tgl1 + "' and  convert(varchar,Pakai.PakaiDate,112)<=" + tgl2 +
                strDeptid + strGroupID + ") as AA group by DeptName,ItemCode,ItemName,UOMCode,Harga,GroupID,GroupDescription,PakaiNo,PakaiDate,Status,Keterangan order by GroupID,ItemCode";
        }
        public string ViewRekapPakai2(string tgl1, string tgl2, int groupID, int deptID)
        {
            string strGroupID = string.Empty;
            string strDeptid = string.Empty;
            if (groupID > 0)
                strGroupID = " and PakaiDetail.GroupID=" + groupID;
            if (deptID > 0)
                strDeptid = "and Pakai.DeptID=" + deptID + " ";

            return "select PakaiNo,convert(varchar,PakaiDate,103) as PakaiDate,ItemCode,ItemName,UOMCode,SUM(Quantity) as Jumlah," +
                "isnull(Harga,0) as Harga,Keterangan,GroupID,GroupDescription,DeptName,Status from (" +
                "SELECT CASE PakaiDetail.ItemTypeID  WHEN 1 THEN (SELECT ItemCode FROM Inventory WHERE ID = PakaiDetail.ItemID)  " +
                "WHEN 2 THEN (SELECT ItemCode FROM Asset WHERE ID = PakaiDetail.ItemID)  ELSE " +
                "(SELECT ItemCode FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS ItemCode,  " +
                "CASE PakaiDetail.ItemTypeID  WHEN 1 THEN (SELECT ItemName FROM Inventory WHERE ID = PakaiDetail.ItemID)  " +
                "WHEN 2 THEN (SELECT ItemName FROM Asset WHERE ID = PakaiDetail.ItemID)  ELSE " +
                "(SELECT ItemName FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS ItemName,  " +
                "CASE PakaiDetail.ItemTypeID  WHEN 1 THEN (SELECT GroupID FROM Inventory WHERE ID = PakaiDetail.ItemID)  " +
                "WHEN 2 THEN (SELECT GroupID FROM Asset WHERE ID = PakaiDetail.ItemID)  ELSE " +
                "(SELECT GroupID FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS GroupID,  " +
                "CASE WHEN PakaiDetail.ItemID>0 THEN (SELECT top 1 isnull(POPurchnDetail.Price,0) FROM POPurchn,POPurchnDetail  " +
                "WHERE POPurchn.ID=POPurchnDetail.POID and POPurchnDetail.ItemID=PakaiDetail.ItemID and POPurchnDetail.GroupID=PakaiDetail.GroupID " +
                "and POPurchn.Status>-1 and " +
                "POPurchnDetail.Status>-1 order by POPurchnDate Desc) else 0 end Harga," +
                "CASE when PakaiDetail.GroupID>0 THEN (SELECT GroupDescription FROM GroupsPurchn WHERE GroupsPurchn.ID = PakaiDetail.GroupID)  " +
                "ELSE 'xxx' END AS GroupDescription,  " +
                "CASE Pakai.Status  WHEN 0 THEN ('Open')  " +
                "WHEN 1 THEN ('Head')  ELSE " +
                "('Gudang') END AS Status," +
                "PakaiDetail.Quantity,PakaiDetail.Keterangan,Pakai.PakaiNo,UOM.UOMCode,Pakai.PakaiDate,Pakai.DeptID,Dept.DeptName FROM Pakai " +
                "INNER JOIN PakaiDetail ON Pakai.ID = PakaiDetail.PakaiID and PakaiDetail.RowStatus>-1 " +
                "INNER JOIN UOM ON PakaiDetail.UomID = UOM.ID and UOM.RowStatus>-1 " +
                "INNER JOIN Dept ON Pakai.DeptID = Dept.ID and Dept.RowStatus>-1 " +
                "where Pakai.status=3 and convert(varchar,Pakai.PakaiDate,112)>='" + tgl1 + "' and  convert(varchar,Pakai.PakaiDate,112)<=" + tgl2 +
                strDeptid + strGroupID + ") as AA group by DeptName,ItemCode,ItemName,UOMCode,Harga,GroupID,GroupDescription,PakaiNo,PakaiDate,Status,Keterangan order by GroupID,ItemCode";
        }


        public string ViewSuratKeluar(string NoSJ)
        {
            return " select A.ID,A.NoSJ,convert (varchar, A.TglSJ,106) as TglSJ, A.Tujuan,A.Ket,A.Jumlah,A.NoPolisi,A.UOM," +
                                     " case A.ItemTypeID when 1 then (select ItemCode from Inventory where Inventory.ID=A.ItemID) " +
                                     " when 2 then (select ItemCode from Asset where Asset.ID=A.ItemID)else '' end ItemCode, " +
                                     " Case A.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=A.ItemID) " +
                                     " when 2 then (select ItemName from Asset where Asset.ID=A.ItemID) " +
                                     " else ItemName end ItemName,A.ItemID,A.ItemTypeID,A.CreatedBy " +
                                     " from INV_suratjalan_keluar as A where  A.RowStatus > -1 and A.NoSJ = '" + NoSJ + "'";
        }

        public string ViewSlipPakai(string strNo)
        {
           string strSQL="Select RowNumber,PakaiNo,TglPakai,Dept,KodeBarang,Satuan,Jumlah,Rak,Keterangan, " +
                   "Case When ProdLine >0 Then NamaBarang + ' ( LINE '+ CAST(ProdLine as CHAR(1))+' )' Else NamaBarang END NamaBarang From( " +
                   "select row_number() OVER (ORDER BY PakaiID) AS RowNumber,A.PakaiNo,convert(varchar,A.PakaiDate,106) as TglPakai," +
                   "C.DeptName + ' ' + C.DeptCode as Dept,E.RakNo as Rak, " +
                   "case B.ItemTypeID " +
                   "When 1 then (select ItemCode from Inventory where ID = B.ItemID) " +
                   "When 2 then (select ItemCode from Asset where ID = B.ItemID) " +
                   "When 3 then (select ItemCode from Biaya where ID = B.ItemID) end KodeBarang, " +
                   "case B.ItemTypeID " +
                   "When 1 then (select ItemName from Inventory where ID = B.ItemID) " +
                   "When 2 then (select ItemName from Asset where ID = B.ItemID) " +
                   "When 3 then (select ItemName from Biaya where ID = B.ItemID) end NamaBarang, " +
                   "D.UOMCode as Satuan,B.Quantity as Jumlah,B.Keterangan,B.ProdLine " +
                   "from Pakai as A,PakaiDetail as B,Dept as C,UOM as D, INV_MasterRak as E " +
                   "where A.PakaiNo = '" + strNo + "' and B.PakaiID = A.ID and A.DeptID = C.ID and E.ID= B.RakID " +
                   "and B.UomID = D.ID and B.RowStatus > -1 ) as w";

           strSQL = "SELECT RowNumber,PakaiNo,TglPakai,Dept,KodeBarang,Satuan,Jumlah,Keterangan, CASE When ProdLine >0  " +
                  "     Then NamaBarang + ' ( LINE '+ CAST(ProdLine as CHAR(1))+' )' Else NamaBarang END NamaBarang,Rak From(  " +
                  "     select row_number() OVER (ORDER BY PakaiID) AS RowNumber,A.PakaiNo,convert(varchar,A.PakaiDate,106) as TglPakai, " +
                  "     C.DeptName + ' ' + C.DeptCode as Dept, (Select dbo.ItemCodeInv(b.ItemID,b.ItemTypeID))KodeBarang,  " +
                  "     (Select dbo.ItemNameInv(b.ItemID,b.ItemTypeID)) NamaBarang, D.UOMCode as Satuan,B.Quantity as Jumlah,B.Keterangan,B.ProdLine " +
                  "     ,E.RakNo as Rak from  " +
                  "     Pakai as A " +
                  "     LEFT JOIN PakaiDetail as B ON B.PakaiID=A.ID " +
                  "     LEFT JOIN Dept as C ON C.ID=A.DeptID " +
                  "     LEFT JOIN UOM as D ON D.ID =B.UomID " +
                  "     LEFT JOIN INV_MasterRak as E ON E.ID=B.RakID " +
                  "     WHERE A.PakaiNo = '" + strNo + "' AND a.Status>-1 " +
                  "     and B.RowStatus > -1 " +
                  "  ) as w";
           return strSQL;
        }
        public string ViewSlipRetur(string strNo)
        {
            return "select row_number() OVER (ORDER BY PakaiID) AS RowNumber,A.ReturNo as PakaiNo,convert(varchar,A.ReturDate,106) as " +
                    "TglPakai,C.DeptName + ' ' + C.DeptCode as Dept, " +
                    "case B.ItemTypeID " +
                    "When 1 then (select ItemCode from Inventory where ID = B.ItemID) " +
                    "When 2 then (select ItemCode from Asset where ID = B.ItemID) " +
                    "When 3 then (select ItemCode from Biaya where ID = B.ItemID) end KodeBarang, " +
                    "case B.ItemTypeID " +
                    "When 1 then (select ItemName from Inventory where ID = B.ItemID) " +
                    "When 2 then (select ItemName from Asset where ID = B.ItemID) " +
                    "When 3 then (select ItemName from Biaya where ID = B.ItemID) end NamaBarang, " +
                    "D.UOMCode as Satuan,B.Quantity as Jumlah,B.Keterangan " +
                    "from ReturPakai as A,ReturPakaiDetail as B,Dept as C,UOM as D " +
                    "where A.ReturNo = '" + strNo + "' and B.ReturID = A.ID and A.DeptID = C.ID " +
                    "and B.UomID = D.ID and B.RowStatus > -1";
        }
        public string ViewSlipReceipt(string strNo)
        {
            string strSQL = "select A.ReceiptNo,convert(varchar,A.ReceiptDate,106) as ReceiptDate,C.SupplierName,C.Alamat as Address,B.PONo," +
                    "case B.ItemTypeID " +
                    "when 1 then(select ItemCode from Inventory where Inventory.ID = B.ItemID and Inventory.Aktif = 1) " +
                    "when 2 then(select ItemCode from Asset where Asset.ID = B.ItemID and Asset.Aktif = 1) " +
                    "when 3 then " + ItemCodeBiayaNew("B") + " end ItemCode, " +
                    "case B.ItemTypeID " +
                    "when 1 then(select ItemName from Inventory where Inventory.ID = B.ItemID and Inventory.Aktif = 1) " +
                    "when 2 then(select ItemName from Asset where Asset.ID = B.ItemID and Asset.Aktif = 1) " +
                    "when 3 then " + ItemSPPBiayaReceipt("B") + " end ItemName, " +
                    "D.UOMCode,B.Quantity,B.SPPNo,/*E.PaymentType*/Case When E.Termin='Cash' then 0 else 1 end as CaCr," +
                    "E.ItemFrom as LoIm,B.Keterangan " +
                    "from Receipt as A,ReceiptDetail as B,SuppPurch as C,UOM as D,POPurchn as E " +
                    "where A.ReceiptNo = '" + strNo + "' and B.ReceiptID = A.ID and A.SupplierId = C.ID " +
                    "and B.UomID = D.ID and A.POID = E.ID and A.status>-1 and B.rowstatus>-1";
            return strSQL;
        }
        public string ViewSlipSPP(string strNo)
        {
            return "select TOP 9 A.ID,A.NoSPP,convert(varchar,A.CreatedTime,103) as Tanggal," +
                    "case when (A.ItemTypeID!=3) then B.Keterangan else '' end Keterangan,B.Quantity,C.UOMCode,A.CreatedBy,A.DepoID," +
                    "case A.PermintaanType when 1 then 'Top Urgent' when 2 then 'Biasa' when 3 then 'Sesuai Schedule' else '' end TipePermintaan," +
                    "case A.ItemTypeID when 1 then (select ItemName from Inventory where ID=B.ItemID) " +
                    "when 2 then (select ItemName from Asset where ID=B.ItemID) " +
                    "when 3 then (select ItemName from Biaya where ID=B.ItemID)+ ' - ' + B.Keterangan end Description," +
                    "case A.ItemTypeID when 1 then (select ItemCode from Inventory where ID=B.ItemID) " +
                    "when 2 then (select ItemCode from Asset where ID=B.ItemID) " +
                    "when 3 then (select ItemCode from Biaya where ID=B.ItemID) end ItemCode " +
                    "from SPP as A, SPPDetail as B, UOM as C " +
                    "where A.ID=B.SPPID and B.UOMID=C.ID and B.Status>-1 and A.NoSPP='" + strNo + "'";
        }
        public string ViewLapBul(int userID, int groupID, string awalReport, string akhirReport)
        {
            return "select ItemCode,ItemName,UomCode,StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,isnull([010],0) as [010],isnull([012],0) as [012]," +
                "ISNULL([021],0) as [021], ISNULL([022],0) as [022], ISNULL([031],0) as [031], ISNULL([032],0) as [032], ISNULL([033],0) as [033], " +
                "ISNULL([034],0) as [034], ISNULL([041],0) as [041], ISNULL([042],0) as [042], ISNULL([051],0) as [051], " +
                "ISNULL([052],0) as [052], ISNULL([061],0) as [061], ISNULL([070],0) as [070], ISNULL([091],0) as [091],ISNULL([134],0) as [134]" +
                "ISNULL([101],0) as [101], ISNULL([111],0) as [111], ISNULL([131],0) as [131], ISNULL([132],0) as [132] , ISNULL([133],0) as [133] " +
                "from (select ItemCode,ItemName,UomCode,StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,Pemakaian,DeptCode " +
                "from LaporanBulanan where UserID=" + userID + " and GroupID=" + groupID + " and TglCetak>='" + awalReport + "' and TglCetak<='" + akhirReport + "' and " +
                "(StokAwal>0 or Pemasukan>0 or Retur >0 or AdjustTambah > 0 or AdjustKurang > 0 or Pemakaian>0) " +
                ") up pivot (sum(pemakaian) for DeptCode in ([010],[012],[021],[022],[031],[032],[033],[034],[041],[042],[051],[052],[061],[070],[091]," +
                "[101],[111],[131],[134],[132],[133],[137])) as A1 order by ItemCode";
        }
        public string ViewLapBul2ForRepackOnly(string ketBlnLalu, int yearPeriod, string tgl1, string tgl2, int groupID, string stock)
        {
            string strItemTypeID = string.Empty;
            string strJenisBrg = string.Empty;
            string strStock = string.Empty;
            string sts = "3";
            #region select groupid
            if (groupID == 5)
            {
                strItemTypeID = " and A.ItemTypeID = 3";
                strJenisBrg = "Biaya";
            }
            else
            {
                if (groupID == 4)
                {
                    strItemTypeID = " and A.ItemTypeID = 2";
                    strJenisBrg = "Asset";
                }
                else
                {
                    strItemTypeID = " and A.ItemTypeID = 1";
                    strJenisBrg = "Inventory";
                }
            }
            if (stock == "0")
                strStock = "and  " + strJenisBrg + ".Stock = 0";
            else
                if (stock == "1")
                    strStock = "and  " + strJenisBrg + ".Stock = 9";
                else
                    strStock = " ";
            #endregion
            #region nonactiveline
            //return "SELECT ItemCode,ItemName,UOMCode,StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,[010],[021],[022],[031],[032],[033],[034]" +
            //",[041],[042],[051],[052],[061],[062],[070],[091],[101],[111],[012],[131],[132],[133] from (SELECT " + strJenisBrg + ".id," +
            //strJenisBrg + ".ItemCode, " + strJenisBrg + ".ItemName, UOM.UOMCode, " +

            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT isnull(sum(" + ketBlnLalu + "),0) FROM SaldoInventory A WHERE ITEMID=" +
            //strJenisBrg + ".id AND YearPeriod=" + yearPeriod + strItemTypeID + ") END StokAwal, " +

            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (select ISNULL(SUM(ToQty),0) from Convertan where ToItemID=" + strJenisBrg + ".ID and Convertan.RowStatus>-1 and " +
            //"convert(varchar,Convertan.CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,Convertan.CreatedTime,112) <= '" + tgl2 + "' ) end Pemasukan, " +

            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail A inner join adjust B on B.id = A.adjustID " +
            //" WHERE A.ItemID = " + strJenisBrg + ".ID AND A.RowStatus>-1  " + strItemTypeID + " AND B.ADJUSTTYPE='tambah' and B.status > -1  " +
            //"and convert(varchar,B.AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,B.AdjustDate,112) <= '" + tgl2 + "' ) END  AdjustTambah, " +

            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail A inner join adjust B on B.id = A.adjustID " +
            //" WHERE A.ItemID = " + strJenisBrg + ".ID AND A.RowStatus>-1  " + strItemTypeID + " AND B.ADJUSTTYPE='kurang' and B.status > -1  " +
            //"and convert(varchar,B.AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,B.AdjustDate,112) <= '" + tgl2 + "' ) END  AdjustKurang, " +

            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(Quantity),0) FROM  ReturPakaiDetail A inner join ReturPakai B on " +
            //" B.ID = A.returID WHERE A.ItemID = " + strJenisBrg + ".ID AND A.RowStatus>-1  " + strItemTypeID + " AND B.status > -1 AND  " +
            //"convert(varchar, B.ReturDate,112) >= '" + tgl1 + "' AND convert(varchar, B.ReturDate,112) <= '" + tgl2 + "' ) END  Retur, " +

            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status=2) " +
            //"AND (C.DeptCode = '010')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [010], " +

            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '021')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [021], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '022')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [022], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '031')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [031], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '032')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [032], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '033')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [033], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '034')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [034], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '041')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [041], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '042')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [042], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '051')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [051], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '052')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [052], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '061')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [061], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '062')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [062], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '070')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [070], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '091')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [091], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '101')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [101], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '111')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [111], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '012')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [012], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '131')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [131], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '132')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [132], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '133')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [133] " +            
            //"FROM  " + strJenisBrg + " INNER JOIN UOM ON " + strJenisBrg + ".UOMID = UOM.ID WHERE (" + strJenisBrg + ".GroupID = " + 
            //groupID  + strStock + " ) ) AS AA where (StokAwal>0 or Pemasukan>0 or Retur>0 or AdjustTambah>0 or AdjustKurang>0) ORDER BY ItemCode ";
            #endregion
            #region querystring
            string txt = (((Users)HttpContext.Current.Session["Users"]).ViewPrice > 0) ? "SELECT ItemCode,ItemName,UOMCode,StokAwal,PriceL,PriceC,Pemasukan,priceM,priceP,Retur,priceR,AdjustTambah,AdjustKurang,priceA" :
                "SELECT ItemCode,ItemName,UOMCode,StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang ";
            string strSQL = "SELECT ItemCode,ItemName,UOMCode,StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang " +
            ",isnull([010],0) as[010],isnull([021],0)as [021],isnull([022],0)as [022],isnull([031],0)as [031],isnull([032],0)as [032], " +
            "isnull([033],0)as [033],isnull([034],0)as [034],isnull([041],0)as [041],isnull([042],0)as [042],isnull([051],0)as [051], " +
            "isnull([052],0)as [052],isnull([061],0)as [061],isnull([062],0)as [062],isnull([070],0)as [070],isnull([091],0)as [091], " +
            "isnull([101],0)as [101],isnull([111],0)as [111],isnull([012],0)as [012],isnull([131],0)as [131],isnull([132],0)as [132], " +
            "isnull([133] ,0)as [133],isnull([135],0) as [135],isnull([139],0) as [139],isnull([142],0) as [142]  " +
            "from (SELECT " + strJenisBrg + ".id  as itemid," +
            strJenisBrg + ".ItemCode, " + strJenisBrg + ".ItemName, UOM.UOMCode, " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT isnull(sum(" + ketBlnLalu + "),0) FROM SaldoInventory A WHERE ITEMID=" +
            strJenisBrg + ".id AND YearPeriod=" + yearPeriod + strItemTypeID + ") + " + "(select isnull(SUM(quantity),0) from vw_StockPurchn where ItemID=" +
            strJenisBrg + ".ID and YMD<'" + tgl1 + "' and YM=SUBSTRING('" + tgl1 + "',1,6))  END StokAwal, " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (select ISNULL(SUM(ToQty),0) from Convertan where ToItemID=" + strJenisBrg + ".ID and Convertan.RowStatus>-1 and " +
            "convert(varchar,Convertan.CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,Convertan.CreatedTime,112) <= '" + tgl2 + "' ) end Pemasukan, " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail A inner join adjust B on B.id = A.adjustID " +
            " WHERE  A.apv>0 and A.ItemID = " + strJenisBrg + ".ID AND A.RowStatus>-1  " + strItemTypeID + " AND B.ADJUSTTYPE='tambah' and B.status > -1  " +
            "and convert(varchar,B.AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,B.AdjustDate,112) <= '" + tgl2 + "' ) END  AdjustTambah, " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail A inner join adjust B on B.id = A.adjustID " +
            " WHERE  A.apv>0 and A.ItemID = " + strJenisBrg + ".ID AND A.RowStatus>-1  " + strItemTypeID + " AND B.ADJUSTTYPE='kurang' and B.status > -1  " +
            "and convert(varchar,B.AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,B.AdjustDate,112) <= '" + tgl2 + "' ) END  AdjustKurang, " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(Quantity),0) FROM  ReturPakaiDetail A inner join ReturPakai B on " +
            " B.ID = A.returID WHERE A.ItemID = " + strJenisBrg + ".ID AND A.RowStatus>-1  " + strItemTypeID + " AND B.status > -1 AND  " +
            "convert(varchar, B.ReturDate,112) >= '" + tgl1 + "' AND convert(varchar, B.ReturDate,112) <= '" + tgl2 + "' ) END  Retur " +
            "FROM  " + strJenisBrg + " INNER JOIN UOM ON " + strJenisBrg + ".UOMID = UOM.ID WHERE (" + strJenisBrg + ".GroupID = " +
            groupID + strStock + " )) AS AA left join " +
            "(select  itemid1=case when grouping(ItemID)=1 then 0 else 1 end, itemid, " +
            "sum([010]) as [010],sum([021]) as[021],sum([022]) as[022],sum([031]) as[031],sum([032]) as[032],sum([033]) as[033]  " +
            ",sum([034]) as[034],sum([041]) as[041],sum([042]) as[042],sum([051]) as[051],SUM([052]) as[052],SUM([061]) as[061],SUM([062]) as[062], " +
            "SUM([070]) as[070],SUM([091]) as[091],SUM([101]) as[101],SUM([111]) as[111],SUM([012]) as[012],SUM([131]) as[131],SUM([132]) as[132], " +
            "sum([133]) as[133],sum([135]) as[135],sum([139]) as[139],sum([142]) as[142] " +
            "from ( " +
            "select  ItemID,sum(Quantity) as [010], 0 as [021] ,0 as [022],0 as [031],0 as [032],0 as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai  " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID  " +
            "in(select ID from Dept where DeptCode='010')) group by ItemID  union all " +

            "select ItemID,0 as [010], sum(Quantity)  as [021] ,0 as [022],0 as [031],0 as [032],0 as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + "  and RowStatus>=0 and PakaiID in(select ID from Pakai  " +
            "where status>=" + sts + " and  convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='021')) group by ItemID union all " +

            "select ItemID,0 as [010], 0 as [021] ,sum(Quantity) as [022],0 as [031],0 as [032],0 as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + "  and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='022')) group by ItemID union all " +

            "select ItemID,0 as [010], 0 as [021] ,0 as [022],sum(Quantity) as [031],0 as [032],0 as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + "  and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='031')) group by ItemID union all " +

            "select ItemID,0 as [010], 0 as [021] ,0 as [022],0 as [031],sum(Quantity) as  [032],0 as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + "  and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='032')) group by ItemID union all " +

            "select ItemID,0 as [010], 0 as [021] ,0 as [022],0 as [031],0 as  [032],sum(Quantity) as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + "  and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='033')) group by ItemID union all " +

            "select ItemID,0 as [010], 0 as [021] ,0 as [022],0 as [031], 0 as  [032],0 as [033],sum(Quantity) as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='034')) group by ItemID union all " +

            "select ItemID,0 as [010], 0 as [021] ,0 as [022],0 as [031],0 as  [032],0 as [033],0 as [034],sum(Quantity) as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='041')) group by ItemID union all " +

            "select ItemID,0 as [010], 0 as [021] ,0 as [022],0 as [031],0 as  [032],0 as [033],0 as [034],0 as [041],sum(Quantity) as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='042')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],sum(Quantity) as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='051')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "sum(Quantity) as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='052')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],sum(Quantity) as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='061')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],sum(Quantity) as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='062')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],sum(Quantity) as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='070')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],sum(Quantity) as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='091')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],sum(Quantity) as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='101')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],sum(Quantity) as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='111')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],sum(Quantity) as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='012')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],sum(Quantity) as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='134')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],sum(Quantity) as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='133')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],sum(Quantity) as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode in('132','131'))) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],sum(Quantity) as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='139')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],sum(Quantity) as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode in ('141','142'))) group by ItemID union all " +

            LapBullDept("135", tgl1, tgl2, groupID, strItemTypeID, sts) + ") as pemakaian group by itemid with rollup " +
            ") as AB on AB.ItemID =AA.ItemID where (AA.StokAwal >0 or AA.Pemasukan>0 or AA.Retur>0 or AA.AdjustTambah>0 or AA.AdjustKurang>0)  " +
            "ORDER BY ItemCode";
            #endregion
            return strSQL;
        }
        public string ViewAsset(string ketBlnLalu, int yearPeriod, string tgl1, string tgl2, int groupID)
        {
            string strItemTypeID = string.Empty;
            string strJenisBrg = string.Empty;

            if (groupID == 5)
            {
                strItemTypeID = " and ItemTypeID = 3";
                strJenisBrg = "Biaya";
            }
            else
            {
                if (groupID == 4)
                {
                    strItemTypeID = " and ItemTypeID = 2";
                    strJenisBrg = "Asset";
                }
                else
                {
                    strItemTypeID = " and ItemTypeID = 1";
                    strJenisBrg = "Inventory";
                }
            }

            string strSQL = "SELECT ItemCode,ItemName, UOMCode,StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,Pemakaian  from (SELECT " + strJenisBrg + ".id," + strJenisBrg + ".ItemCode, " + strJenisBrg + ".ItemName, UOM.UOMCode, " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT isnull(sum(" + ketBlnLalu +
            "),0) FROM SaldoInventory WHERE ITEMID=" + strJenisBrg + ".id AND YearPeriod=" +
            yearPeriod + strItemTypeID + ") END StokAwal, " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=" +
            strJenisBrg + ".ID and RowStatus>-1  " + strItemTypeID + " AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND Receipt.status > -1 AND convert(varchar,ReceiptDate,112) >= '" + tgl1 +
            "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan, " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail WHERE AdjustDetail.apv>0 and ItemID = " +
            strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Adjust.status > -1  AND convert(varchar,AdjustDate,112) >= '" +
            tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustTambah, " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail WHERE AdjustDetail.apv>0 and ItemID = " +
            strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND AdjustID IN  " +
            "(SELECT ID FROM Adjust WHERE (AdjustType='kurang' or AdjustType='Disposal') and Adjust.status > -1 AND (nonstok IS null OR nonstok != 1) and " +
            "convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang, " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(Quantity),0) FROM  ReturPakaiDetail WHERE rowstatus>-1   " + strItemTypeID + " and ItemID = " +
            strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND ReturID IN (SELECT ID FROM ReturPakai WHERE ReturPakai.status > -1 AND convert(varchar,ReturDate,112) >= '" +
            tgl1 + "' AND convert(varchar,ReturDate,112) <= '" + tgl2 + "' )) END  Retur, " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM PakaiDetail  WHERE ItemID=" +
            strJenisBrg + ".ID AND RowStatus>-1  " + strItemTypeID + " AND PakaiID IN( SELECT ID FROM Pakai WHERE Pakai.status > -1 AND convert(varchar,PakaiDate,112) >= '" + tgl1 +
            "'  AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "') ) END  Pemakaian FROM  " + strJenisBrg +
            " INNER JOIN UOM ON " + strJenisBrg + ".UOMID = UOM.ID WHERE (" +
            strJenisBrg + ".GroupID = " + groupID + ") ) AS AA where (StokAwal>0 or Pemasukan>0 or Retur>0 or AdjustTambah>0 or AdjustKurang>0) ORDER BY ItemCode ";
            return strSQL;
        }
        public string ViewLapBul2(string ketBlnLalu, int yearPeriod, string tgl1, string tgl2, int groupID, string stock)
        {
            /** update on 10-03-2015
             * Change dept 131 to 134 ( project to ISO)
             * 131 di gabungkan dengan 132
             */
            string strItemTypeID = string.Empty;
            string strJenisBrg = string.Empty;
            string strStock = string.Empty;
            string onlyAsset = string.Empty;
            string sts = "3";
            #region pilih group data
            onlyAsset = (groupID == 4 || groupID == 12) ? " AdjustTambah,AdjustKurang " : "AdjustTambah,AdjustTambahReInt,AdjustKurang ";
            if (groupID == 5)
            {
                strItemTypeID = " and A.ItemTypeID = 3";
                strJenisBrg = "Biaya";
            }
            else
            {
                if (groupID == 4 || groupID == 12)
                {
                    strItemTypeID = " and A.ItemTypeID = 2";
                    strJenisBrg = "Asset";
                }
                else
                {
                    strItemTypeID = " and A.ItemTypeID = 1";
                    strJenisBrg = "Inventory";
                }
            }
            if (stock == "0")
                strStock = " and  " + strJenisBrg + ".Stock = 0 and " + strJenisBrg + ".aktif=1 ";
            else
                if (stock == "1")
                    strStock = " and  " + strJenisBrg + ".Stock = 1 and " + strJenisBrg + ".aktif=1";
                else
                    strStock = " ";
            #endregion
            string strSQL = "SELECT ItemCode,ItemName,UOMCode,StokAwal,Pemasukan,Retur," + onlyAsset +
            ",isnull([010],0) as[010],isnull([021],0)as [021],isnull([022],0)as [022],isnull([031],0)as [031],isnull([032],0)as [032], " +
            "isnull([033],0)as [033],isnull([034],0)as [034],isnull([041],0)as [041],isnull([042],0)as [042],isnull([051],0)as [051], " +
            "isnull([052],0)as [052],isnull([061],0)as [061],isnull([062],0)as [062],isnull([070],0)as [070],isnull([091],0)as [091], " +
            "isnull([101],0)as [101],isnull([111],0)as [111],isnull([012],0)as [012],isnull([131],0)as [131],isnull([132],0)as [132], " +
            "isnull([133] ,0)as [133],isnull([135],0) as [135],isnull([139],0) as [139],isnull([142],0) as [142]   " +
            "from (SELECT " + strJenisBrg + ".id  as itemid," +
            strJenisBrg + ".ItemCode, " + strJenisBrg + ".ItemName, UOM.UOMCode, " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT isnull(sum(" + ketBlnLalu + "),0) FROM SaldoInventory A WHERE ITEMID=" +
            strJenisBrg + ".id AND YearPeriod=" + yearPeriod + strItemTypeID + ") + " + "(select isnull(SUM(quantity),0) from vw_StockPurchn where ItemID=" +
            strJenisBrg + ".ID and YMD<'" + tgl1 + "' and YM=SUBSTRING('" + tgl1 + "',1,6))  END StokAwal, " +

            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  ReceiptDetail A inner join receipt B on " +
            //" A.receiptID=B.ID where A.ItemID=" + strJenisBrg + ".ID and A.RowStatus>-1  " + strItemTypeID + " and B.status > -1 AND " +
            //"convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' ) END  Pemasukan, " +
            
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (select isnull(sum(qty),0) from (SELECT ISNULL(SUM(QUANTITY),0)qty FROM  ReceiptDetail A inner join receipt B on " +
            " A.receiptID=B.ID where A.ItemID=" + strJenisBrg + ".ID and A.RowStatus>-1  " + strItemTypeID + " and B.status > -1 AND " +
            "convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' " +
            "union all select ISNULL(SUM(ToQty),0)qty from Convertan where ToItemID=" + strJenisBrg + ".ID and Convertan.RowStatus>-1 and " +
            "convert(varchar,Convertan.CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,Convertan.CreatedTime,112) <= '" + tgl2 + "')A ) END  Pemasukan, " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail A inner join adjust B on B.id = A.adjustID " +
            " WHERE  A.apv>0 and A.ItemID = " + strJenisBrg + ".ID AND A.RowStatus>-1  " + strItemTypeID + " AND B.ADJUSTTYPE='tambah' and B.status > -1  " +
            "and convert(varchar,B.AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,B.AdjustDate,112) <= '" + tgl2 + "' and "+
            " A.AdjustID not in (select A1.ID from Adjust A1 where A1.ID=A.AdjustID and Keterangan1 like'%ReturIntLog%')) END  AdjustTambah, " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail A inner join adjust B on B.id = A.adjustID " +
            " WHERE  A.apv>0 and A.ItemID = " + strJenisBrg + ".ID AND A.RowStatus>-1  " + strItemTypeID + " AND B.ADJUSTTYPE='tambah' and B.status > -1  " +
            "and convert(varchar,B.AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,B.AdjustDate,112) <= '" + tgl2 + "' and " +
            "A.AdjustID in (select A1.ID from Adjust A1 where A1.ID=A.AdjustID and Keterangan1 like'%ReturIntLog%')) END  AdjustTambahReInt, " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail A inner join adjust B on B.id = A.adjustID " +
            " WHERE  A.apv>0 and A.ItemID = " + strJenisBrg + ".ID AND A.RowStatus>-1  " + strItemTypeID + " AND B.ADJUSTTYPE='kurang' and B.status > -1 AND (nonstok IS null OR nonstok != 1) " +
            "and convert(varchar,B.AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,B.AdjustDate,112) <= '" + tgl2 + "' ) END  AdjustKurang, " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(Quantity),0) FROM  ReturPakaiDetail A inner join ReturPakai B on " +
            " B.ID = A.returID WHERE A.ItemID = " + strJenisBrg + ".ID AND A.RowStatus>-1  " + strItemTypeID + " AND B.status > -1 AND  " +
            "convert(varchar, B.ReturDate,112) >= '" + tgl1 + "' AND convert(varchar, B.ReturDate,112) <= '" + tgl2 + "' ) END  Retur " +
            "FROM  " + strJenisBrg + " INNER JOIN UOM ON " + strJenisBrg + ".UOMID = UOM.ID WHERE (" + strJenisBrg + ".GroupID = " +
            groupID + strStock + " )) AS AA left join " +
            "(select  itemid1=case when grouping(ItemID)=1 then 0 else 1 end, itemid, " +
            "sum([010]) as [010],sum([021]) as[021],sum([022]) as[022],sum([031]) as[031],sum([032]) as[032],sum([033]) as[033]  " +
            ",sum([034]) as[034],sum([041]) as[041],sum([042]) as[042],sum([051]) as[051],SUM([052]) as[052],SUM([061]) as[061],SUM([062]) as[062], " +
            "SUM([070]) as[070],SUM([091]) as[091],SUM([101]) as[101],SUM([111]) as[111],SUM([012]) as[012],SUM([131]) as[131],SUM([132]) as[132], " +
            "sum([133]) as[133],sum([135]) as[135],sum([139]) as[139],sum([142]) as[142] " +
            "from ( " +
            "select  ItemID,sum(Quantity) as [010], 0 as [021] ,0 as [022],0 as [031],0 as [032],0 as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai  " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID  " +
            "in(select ID from Dept where DeptCode='137')) group by ItemID  union all " +

            "select ItemID,0 as [010], sum(Quantity)  as [021] ,0 as [022],0 as [031],0 as [032],0 as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + "  and RowStatus>=0 and PakaiID in(select ID from Pakai  " +
            "where status>=" + sts + " and  convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='021')) group by ItemID union all " +

            "select ItemID,0 as [010], 0 as [021] ,sum(Quantity) as [022],0 as [031],0 as [032],0 as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + "  and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='022')) group by ItemID union all " +

            "select ItemID,0 as [010], 0 as [021] ,0 as [022],sum(Quantity) as [031],0 as [032],0 as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + "  and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='031')) group by ItemID union all " +

            "select ItemID,0 as [010], 0 as [021] ,0 as [022],0 as [031],sum(Quantity) as  [032],0 as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + "  and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='032')) group by ItemID union all " +

            "select ItemID,0 as [010], 0 as [021] ,0 as [022],0 as [031],0 as  [032],sum(Quantity) as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + "  and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='033')) group by ItemID union all " +

            "select ItemID,0 as [010], 0 as [021] ,0 as [022],0 as [031], 0 as  [032],0 as [033],sum(Quantity) as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='034')) group by ItemID union all " +

            "select ItemID,0 as [010], 0 as [021] ,0 as [022],0 as [031],0 as  [032],0 as [033],0 as [034],sum(Quantity) as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='041')) group by ItemID union all " +

            "select ItemID,0 as [010], 0 as [021] ,0 as [022],0 as [031],0 as  [032],0 as [033],0 as [034],0 as [041],sum(Quantity) as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='042')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],sum(Quantity) as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='051')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "sum(Quantity) as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='052')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],sum(Quantity) as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='061')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],sum(Quantity) as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='062')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],sum(Quantity) as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='070')) group by ItemID union " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],sum(Quantity) as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='091')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],sum(Quantity) as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='101')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],sum(Quantity) as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='111')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],sum(Quantity) as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='012')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],sum(Quantity) as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='134')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],sum(Quantity) as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode in('132','131'))) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],sum(Quantity) as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='133')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],sum(Quantity) as[139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='139')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as[139],sum(Quantity) as[142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode in ('141','142'))) group by ItemID union all " +

            LapBullDept("135", tgl1, tgl2, groupID, strItemTypeID, sts) +
            ") as pemakaian group by itemid with rollup " +
            ") as AB on AB.ItemID =AA.ItemID where (AA.StokAwal <>0 or AA.Pemasukan<>0 or AA.Retur<>0 or AA.AdjustTambah<>0 or AA.AdjustKurang<>0)  " +
            "ORDER BY ItemCode";

            return strSQL;
        }
        #region 2 void ini di ganti dengan void bagian bawah
        private string ViewLapBul2VP_old(string PriceBlnLalu, string ketBlnLalu, int yearPeriod, string tgl1, string tgl2, int groupID, string stock)
        {
            /**
             * Update on 10-03-2015
             * Change 131 to 134
             * 131 di gabung dengan 134
             */
            string strItemTypeID = string.Empty;
            string strJenisBrg = string.Empty;
            string strStock = string.Empty;
            string sts = "2";

            #region pilih group
            if (groupID == 5)
            {
                strItemTypeID = " and A.ItemTypeID = 3";
                strJenisBrg = "Biaya";
            }
            else
            {
                if (groupID == 4)
                {
                    strItemTypeID = " and A.ItemTypeID = 2";
                    strJenisBrg = "Asset";
                }
                else
                {
                    strItemTypeID = " and A.ItemTypeID = 1";
                    strJenisBrg = "Inventory";
                }
            }
            if (stock == "0")
            {
                strStock = " and  " + strJenisBrg + ".Stock = 0 and " + strJenisBrg + ".aktif=1 ";
            }
            else if (stock == "1")
            {
                strStock = " and  " + strJenisBrg + ".Stock = 1 and " + strJenisBrg + ".aktif=1 ";
            }
            else
            {
                strStock = " ";
            }
            #endregion

            string strSQL = "SELECT ItemCode,ItemName,UOMCode,StokAwal,PriceL,PriceC,Pemasukan,priceM,priceP,Retur,priceR,AdjustTambah,AdjustKurang,priceAT,priceAK" +
            ",isnull([010],0) as[010],isnull([021],0)as [021],isnull([022],0)as [022],isnull([031],0)as [031],isnull([032],0)as [032], " +
            "isnull([033],0)as [033],isnull([034],0)as [034],isnull([041],0)as [041],isnull([042],0)as [042],isnull([051],0)as [051], " +
            "isnull([052],0)as [052],isnull([061],0)as [061],isnull([062],0)as [062],isnull([070],0)as [070],isnull([091],0)as [091], " +
            "isnull([101],0)as [101],isnull([111],0)as [111],isnull([012],0)as [012],isnull([131],0)as [131],isnull([132],0)as [132], " +
            "isnull([133] ,0)as [133],isnull([135],0) as [135]  " +
            "from (SELECT " + strJenisBrg + ".id  as itemid," +
            strJenisBrg + ".ItemCode, " + strJenisBrg + ".ItemName, UOM.UOMCode, " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT isnull(sum(" + ketBlnLalu + "),0) FROM SaldoInventory A WHERE ITEMID=" +
            strJenisBrg + ".id AND YearPeriod=" + yearPeriod + strItemTypeID + ") + " + "(select isnull(SUM(quantity),0) "+
            "from vw_StockPurchn where ItemID="+strJenisBrg + ".ID and YMD<'" + tgl1 + "' and YM=SUBSTRING('" + tgl1 + "',1,6)) END StokAwal, " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT isnull(sum(" + PriceBlnLalu + "),0) FROM SaldoInventory A WHERE ITEMID=" +
            strJenisBrg + ".id AND YearPeriod=" + yearPeriod + strItemTypeID + ") END PriceL, " +


            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  ISNULL(SUM(QUANTITY),0) FROM  PakaiDetail A inner join Pakai B on B.id = A.PakaiID   " +
            "WHERE A.ItemID =" + strJenisBrg + ".ID and A.RowStatus>-1   and A.ItemTypeID = 1 and B.status > 1   " +
            "and convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,B.PakaiDate,112) <= '" + tgl2 + "') END  sumqtyPakai, " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  ISNULL(sum(quantity*AvgPrice),0) FROM  PakaiDetail A inner join Pakai B on B.id = A.PakaiID   " +
            "WHERE A.ItemID =" + strJenisBrg + ".ID and  A.RowStatus>-1   and A.ItemTypeID = 1 and B.status > 1   " +
            "and convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,B.PakaiDate,112) <= '" + tgl2 + "' ) END  priceP, " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (select top 1 Price from vw_AwalStocknPrice where itemid=" + strJenisBrg + ".ID and groupid=" + groupID + 
            " and YM='" + tgl1.Substring(0, 6) + "' ) end PriceC," +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  ReceiptDetail A inner join receipt B on " +
            " A.receiptID=B.ID where A.ItemID=" + strJenisBrg + ".ID and A.RowStatus>-1  " + strItemTypeID + " and B.status > -1 AND " +
            "convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' ) END  Pemasukan, " +

            "/* Receipt tidak di average price nya langsung ambil dari po "+
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  ISNULL(sum(avgPrice*Quantity),0) FROM  ReceiptDetail A inner join Receipt B on B.id = A.ReceiptID   " +
            "WHERE A.ItemID =" + strJenisBrg + ".ID and  A.RowStatus>-1   and A.ItemTypeID = 1 and B.status > -1   " +
            "and convert(varchar,B.ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,B.ReceiptDate,112) <= '" + tgl2 + "' ) END  priceM,*/ " +
            
            "/* price Receipt diambil dari po dan di kurs in */ "+
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN ("+
            "SELECT ISNULL(SUM(Price*Quantity),0) FROM ( "+
            "SELECT rd.ItemID,rc.ReceiptNo,rd.PONo, rd.Quantity, "+
            "CASE WHEN pd.Price>0 THEN pd.Price ELSE pd.Price2 END Price,p.Crc, "+
            "CASE WHEN p.NilaiKurs=0 and p.Crc>1 THEN "+
            "(SELECT TOP 1 Kurs FROM MataUangKurs WHERE MUID=p.Crc and Convert(Char,drTgl,112)=Convert(Char,rc.ReceiptDate,112)  "+
            "and MataUangKurs.rowstatus=1) "+
            "ELSE p.NilaiKurs END NilaiKurs "+
            "FROM Receipt rc "+
            "LEFT JOIN ReceiptDetail rd on rd.ReceiptID=rc.ID "+
            "LEFT JOIN POPurchnDetail pd on pd.ID=rd.PODetailID "+
            "LEFT JOIN POPurchn p ON p.ID=pd.POID "+
            "WHERE convert(char,rc.ReceiptDate,112) between '" + tgl1 + "' and '" + tgl2 + "' and rd.ItemTypeID=1 " +
            "AND rd.ItemID="+strJenisBrg + ".ID and rc.Status>-1 and rd.RowStatus>-1 "+
            ") as x ) END priceM, " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail A inner join adjust B on B.id = A.adjustID " +
            " WHERE  A.apv>0 and A.ItemID = " + strJenisBrg + ".ID AND A.RowStatus>-1  " + strItemTypeID + " AND B.ADJUSTTYPE='tambah' and B.status > -1  " +
            "and convert(varchar,B.AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,B.AdjustDate,112) <= '" + tgl2 + "' ) END  AdjustTambah, " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail A inner join adjust B on B.id = A.adjustID " +
            " WHERE  A.apv>0 and A.ItemID = " + strJenisBrg + ".ID AND A.RowStatus>-1  " + strItemTypeID + " AND B.ADJUSTTYPE='kurang' and B.status > -1  " +
            "and convert(varchar,B.AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,B.AdjustDate,112) <= '" + tgl2 + "' ) END  AdjustKurang, " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  ISNULL(sum(quantity*AvgPrice),0) FROM  AdjustDetail A inner join Adjust B on B.id = A.AdjustID   " +
            "WHERE  A.apv>0 and A.ItemID =" + strJenisBrg + ".ID and  A.RowStatus>-1   and A.ItemTypeID = 1 AND B.ADJUSTTYPE='tambah' and B.status > -1   " +
            "and convert(varchar,B.AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,B.AdjustDate,112) <= '" + tgl2 + "' ) END  priceAT, " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  ISNULL(sum(quantity*AvgPrice),0) FROM  AdjustDetail A inner join Adjust B on B.id = A.AdjustID   " +
            "WHERE  A.apv>0 and A.ItemID =" + strJenisBrg + ".ID and  A.RowStatus>-1   and A.ItemTypeID = 1 and  B.ADJUSTTYPE='kurang' AND B.status > -1   " +
            "and convert(varchar,B.AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,B.AdjustDate,112) <= '" + tgl2 + "' ) END  priceAK, " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(Quantity),0) FROM  ReturPakaiDetail A inner join ReturPakai B on " +
            " B.ID = A.returID WHERE A.ItemID = " + strJenisBrg + ".ID AND A.RowStatus>-1  " + strItemTypeID + " AND B.status > -1 AND  " +
            "convert(varchar, B.ReturDate,112) >= '" + tgl1 + "' AND convert(varchar, B.ReturDate,112) <= '" + tgl2 + "' ) END  Retur, " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  ISNULL(sum(quantity*AvgPrice),0) FROM  ReturPakaiDetail A inner join ReturPakai B on B.id = A.ReturID   " +
            "WHERE A.ItemID =" + strJenisBrg + ".ID and  A.RowStatus>-1   and A.ItemTypeID = 1 and B.status > -1   " +
            "and convert(varchar,B.ReturDate,112) >= '" + tgl1 + "' AND convert(varchar,B.ReturDate,112) <= '" + tgl2 + "' ) END  priceR " +

            "FROM  " + strJenisBrg + " INNER JOIN UOM ON " + strJenisBrg + ".UOMID = UOM.ID WHERE (" + strJenisBrg + ".GroupID = " +
            groupID + strStock + " )) AS AA " +
            "left join " +
            "(select  itemid1=case when grouping(ItemID)=1 then 0 else 1 end, itemid, " +
            "sum([010]) as [010],sum([021]) as[021],sum([022]) as[022],sum([031]) as[031],sum([032]) as[032],sum([033]) as[033]  " +
            ",sum([034]) as[034],sum([041]) as[041],sum([042]) as[042],sum([051]) as[051],SUM([052]) as[052],SUM([061]) as[061],SUM([062]) as[062], " +
            "SUM([070]) as[070],SUM([091]) as[091],SUM([101]) as[101],SUM([111]) as[111],SUM([012]) as[012],SUM([131]) as[131],SUM([132]) as[132], " +
            "sum([133]) as[133], sum([135]) as[135]" +
            "from ( " +
            "(select  ItemID,sum(Quantity) as [010], 0 as [021] ,0 as [022],0 as [031],0 as [032],0 as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai  " +
            "where status >=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID  " +
            "in(select ID from Dept where DeptCode='137'/*'010'*/)) group by ItemID ) union all ( " +
            "select ItemID,0 as [010], sum(Quantity)  as [021] ,0 as [022],0 as [031],0 as [032],0 as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + "  and RowStatus>=0 and PakaiID in(select ID from Pakai  " +
            "where status >=" + sts + " and  convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='021')) group by ItemID  ) union all ( " +
            "select ItemID,0 as [010], 0 as [021] ,sum(Quantity) as [022],0 as [031],0 as [032],0 as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + "  and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='022')) group by ItemID  ) union all ( " +
            "select ItemID,0 as [010], 0 as [021] ,0 as [022],sum(Quantity) as [031],0 as [032],0 as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + "  and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='031')) group by ItemID  ) union all ( " +
            "select ItemID,0 as [010], 0 as [021] ,0 as [022],0 as [031],sum(Quantity) as  [032],0 as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + "  and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='032')) group by ItemID  ) union all ( " +
            "select ItemID,0 as [010], 0 as [021] ,0 as [022],0 as [031],0 as  [032],sum(Quantity) as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + "  and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='033')) group by ItemID  ) union all ( " +
            "select ItemID,0 as [010], 0 as [021] ,0 as [022],0 as [031], 0 as  [032],0 as [033],sum(Quantity) as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='034')) group by ItemID  ) union all ( " +
            "select ItemID,0 as [010], 0 as [021] ,0 as [022],0 as [031],0 as  [032],0 as [033],0 as [034],sum(Quantity) as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='041')) group by ItemID  ) union all ( " +
            "select ItemID,0 as [010], 0 as [021] ,0 as [022],0 as [031],0 as  [032],0 as [033],0 as [034],0 as [041],sum(Quantity) as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='042')) group by ItemID  ) union all ( " +
            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],sum(Quantity) as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='051')) group by ItemID  ) union all ( " +
            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "sum(Quantity) as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='052')) group by ItemID  ) union all ( " +
            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],sum(Quantity) as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='061')) group by ItemID  ) union all ( " +
            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],sum(Quantity) as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='062')) group by ItemID  ) union all ( " +
            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],sum(Quantity) as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='070')) group by ItemID  ) union all ( " +
            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],sum(Quantity) as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='091')) group by ItemID  ) union all ( " +
            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],sum(Quantity) as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='101')) group by ItemID  ) union all ( " +
            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],sum(Quantity) as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='111')) group by ItemID  ) union all ( " +
            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],sum(Quantity) as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='012')) group by ItemID  ) union all ( " +
            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],sum(Quantity) as[131],0 as[132],0 as[133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='134')) group by ItemID  ) union all ( " +
            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],sum(Quantity) as[132],0 as[133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode in('132','131'))) group by ItemID  ) union all ( " +
            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],sum(Quantity) as[133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='133')) group by ItemID) union all (" +
                LapBullDept("135", tgl1, tgl2, groupID, strItemTypeID, sts) +
            ")) as pemakaian group by itemid with rollup " +
            ") as AB on AB.ItemID =AA.ItemID where (AA.StokAwal <>0 or AA.Pemasukan<>0 or AA.Retur<>0 or AA.AdjustTambah<>0 or AA.AdjustKurang<>0)  " +
            "ORDER BY ItemCode";
            return strSQL;
        }
        private string ViewLapBul2ForAtkOnly_old(string ketBlnLalu, int yearPeriod, string tgl1, string tgl2, int groupID, string stock)
        {
            string strItemTypeID = string.Empty;
            string strJenisBrg = string.Empty;
            string strStock = string.Empty;
            string sts = "2";
            #region pilih group
            if (groupID == 5)
            {
                strItemTypeID = " and A.ItemTypeID = 3";
                strJenisBrg = "Biaya";
            }
            else
            {
                if (groupID == 4)
                {
                    strItemTypeID = " and A.ItemTypeID = 2";
                    strJenisBrg = "Asset";
                }
                else
                {
                    strItemTypeID = " and A.ItemTypeID = 1";
                    strJenisBrg = "Inventory";
                }
            }
            if (stock == "0")
                strStock = "and  " + strJenisBrg + ".Stock = 0";
            else
                if (stock == "1")
                    strStock = "and  " + strJenisBrg + ".Stock = 9";
                else
                    strStock = " ";
            #endregion
            #region
            //return "SELECT ItemCode,ItemName,UOMCode,StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,[010],[021],[022],[031],[032],[033],[034]" +
            //",[041],[042],[051],[052],[061],[062],[070],[091],[101],[111],[012],[131],[132],[133] from (SELECT " + strJenisBrg + ".id," +
            //strJenisBrg + ".ItemCode, " + strJenisBrg + ".ItemName, UOM.UOMCode, " +

            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT isnull(sum(" + ketBlnLalu + "),0) FROM SaldoInventory A WHERE ITEMID=" +
            //strJenisBrg + ".id AND YearPeriod=" + yearPeriod + strItemTypeID + ") END StokAwal, " +

            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  ReceiptDetail A inner join receipt B on " +
            //" A.receiptID=B.ID where A.ItemID=" + strJenisBrg + ".ID and A.RowStatus>-1  " + strItemTypeID + " and B.status > -1 AND " +
            //"convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' ) END  Pemasukan, " +

            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail A inner join adjust B on B.id = A.adjustID " +
            //" WHERE A.ItemID = " + strJenisBrg + ".ID AND A.RowStatus>-1  " + strItemTypeID + " AND B.ADJUSTTYPE='tambah' and B.status > -1  " +
            //"and convert(varchar,B.AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,B.AdjustDate,112) <= '" + tgl2 + "' ) END  AdjustTambah, " +

            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail A inner join adjust B on B.id = A.adjustID " +
            //" WHERE A.ItemID = " + strJenisBrg + ".ID AND A.RowStatus>-1  " + strItemTypeID + " AND B.ADJUSTTYPE='kurang' and B.status > -1  " +
            //"and convert(varchar,B.AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,B.AdjustDate,112) <= '" + tgl2 + "' ) END  AdjustKurang, " +

            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(Quantity),0) FROM  ReturPakaiDetail A inner join ReturPakai B on " +
            //" B.ID = A.returID WHERE A.ItemID = " + strJenisBrg + ".ID AND A.RowStatus>-1  " + strItemTypeID + " AND B.status > -1 AND  " +
            //"convert(varchar, B.ReturDate,112) >= '" + tgl1 + "' AND convert(varchar, B.ReturDate,112) <= '" + tgl2 + "' ) END  Retur, " +

            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '010')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [010], " +

            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '021')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [021], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '022')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [022], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '031')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [031], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '032')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [032], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '033')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [033], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '034')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [034], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status=2) " +
            //"AND (C.DeptCode = '041')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [041], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '042')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [042], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status=2) " +
            //"AND (C.DeptCode = '051')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [051], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '052')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [052], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status=2) " +
            //"AND (C.DeptCode = '061')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [061], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '062')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [062], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            //"AND (C.DeptCode = '070')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [070], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '091')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [091], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '101')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [101], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            //"AND (C.DeptCode = '111')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [111], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '012')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [012], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '131')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [131], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '132')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [132], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '133')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [133] " +
            //" FROM  " + strJenisBrg + " INNER JOIN   UOM ON " + strJenisBrg + ".UOMID = UOM.ID WHERE (" + strJenisBrg + ".GroupID = " + groupID +
            //" and left(Inventory.ItemCode,5)='AT-OF'" + strStock +") ) AS AA where (StokAwal>0 or Pemasukan>0 or Retur>0 or AdjustTambah>0 or AdjustKurang>0) ORDER BY ItemCode ";
            #endregion
            #region string query
            string strSQL = "SELECT ItemCode,ItemName,UOMCode,StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang " +
            ",isnull([010],0) as[010],isnull([021],0)as [021],isnull([022],0)as [022],isnull([031],0)as [031],isnull([032],0)as [032], " +
            "isnull([033],0)as [033],isnull([034],0)as [034],isnull([041],0)as [041],isnull([042],0)as [042],isnull([051],0)as [051], " +
            "isnull([052],0)as [052],isnull([061],0)as [061],isnull([062],0)as [062],isnull([070],0)as [070],isnull([091],0)as [091], " +
            "isnull([101],0)as [101],isnull([111],0)as [111],isnull([012],0)as [012],isnull([131],0)as [131],isnull([132],0)as [132], " +
            "isnull([133] ,0)as [133],isnull([135],0) as [135]  " +
            "from (SELECT " + strJenisBrg + ".id  as itemid," +
            strJenisBrg + ".ItemCode, " + strJenisBrg + ".ItemName, UOM.UOMCode, " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT isnull(sum(" + ketBlnLalu + "),0) FROM SaldoInventory A WHERE ITEMID=" +
            strJenisBrg + ".id AND YearPeriod=" + yearPeriod + strItemTypeID + ") + " + "(select isnull(SUM(quantity),0) from vw_StockPurchn where ItemID=" +
            strJenisBrg + ".ID and YMD<'" + tgl1 + "' and YM=SUBSTRING('" + tgl1 + "',1,6)) END StokAwal, " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  ReceiptDetail A inner join receipt B on " +
            " A.receiptID=B.ID where A.ItemID=" + strJenisBrg + ".ID and A.RowStatus>-1  " + strItemTypeID + " and B.status > -1 AND " +
            "convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' ) END  Pemasukan, " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail A inner join adjust B on B.id = A.adjustID " +
            " WHERE  A.apv>0 and A.ItemID = " + strJenisBrg + ".ID AND A.RowStatus>-1  " + strItemTypeID + " AND B.ADJUSTTYPE='tambah' and B.status > -1  " +
            "and convert(varchar,B.AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,B.AdjustDate,112) <= '" + tgl2 + "' ) END  AdjustTambah, " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail A inner join adjust B on B.id = A.adjustID " +
            " WHERE  A.apv>0 and A.ItemID = " + strJenisBrg + ".ID AND A.RowStatus>-1  " + strItemTypeID + " AND B.ADJUSTTYPE='kurang' and B.status > -1  " +
            "and convert(varchar,B.AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,B.AdjustDate,112) <= '" + tgl2 + "' ) END  AdjustKurang, " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(Quantity),0) FROM  ReturPakaiDetail A inner join ReturPakai B on " +
            " B.ID = A.returID WHERE A.ItemID = " + strJenisBrg + ".ID AND A.RowStatus>-1  " + strItemTypeID + " AND B.status > -1 AND  " +
            "convert(varchar, B.ReturDate,112) >= '" + tgl1 + "' AND convert(varchar, B.ReturDate,112) <= '" + tgl2 + "' ) END  Retur " +
            "FROM  " + strJenisBrg + " INNER JOIN UOM ON " + strJenisBrg + ".UOMID = UOM.ID WHERE (" + strJenisBrg + ".GroupID = " +
            groupID + strStock + " )) AS AA left join " +
            "(select  itemid1=case when grouping(ItemID)=1 then 0 else 1 end, itemid, " +
            "sum([010]) as [010],sum([021]) as[021],sum([022]) as[022],sum([031]) as[031],sum([032]) as[032],sum([033]) as[033]  " +
            ",sum([034]) as[034],sum([041]) as[041],sum([042]) as[042],sum([051]) as[051],SUM([052]) as[052],SUM([061]) as[061],SUM([062]) as[062], " +
            "SUM([070]) as[070],SUM([091]) as[091],SUM([101]) as[101],SUM([111]) as[111],SUM([012]) as[012],SUM([131]) as[131],SUM([132]) as[132], " +
            "sum([133]) as[133],sum([135]) as [135] " +
            "from ( " +
            "select  ItemID,sum(Quantity) as [010], 0 as [021] ,0 as [022],0 as [031],0 as [032],0 as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai  " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID  " +
            "in(select ID from Dept where DeptCode='137')) group by ItemID  union all " +
            "select ItemID,0 as [010], sum(Quantity)  as [021] ,0 as [022],0 as [031],0 as [032],0 as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + "  and RowStatus>=0 and PakaiID in(select ID from Pakai  " +
            "where status>=" + sts + " and  convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='021')) group by ItemID union all " +
            "select ItemID,0 as [010], 0 as [021] ,sum(Quantity) as [022],0 as [031],0 as [032],0 as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + "  and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='022')) group by ItemID union all " +
            "select ItemID,0 as [010], 0 as [021] ,0 as [022],sum(Quantity) as [031],0 as [032],0 as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + "  and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='031')) group by ItemID union all " +
            "select ItemID,0 as [010], 0 as [021] ,0 as [022],0 as [031],sum(Quantity) as  [032],0 as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + "  and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='032')) group by ItemID union " +
            "select ItemID,0 as [010], 0 as [021] ,0 as [022],0 as [031],0 as  [032],sum(Quantity) as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + "  and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='033')) group by ItemID union all " +
            "select ItemID,0 as [010], 0 as [021] ,0 as [022],0 as [031], 0 as  [032],0 as [033],sum(Quantity) as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='034')) group by ItemID union all " +
            "select ItemID,0 as [010], 0 as [021] ,0 as [022],0 as [031],0 as  [032],0 as [033],0 as [034],sum(Quantity) as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='041')) group by ItemID union all " +
            "select ItemID,0 as [010], 0 as [021] ,0 as [022],0 as [031],0 as  [032],0 as [033],0 as [034],0 as [041],sum(Quantity) as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='042')) group by ItemID union all " +
            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],sum(Quantity) as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='051')) group by ItemID union all " +
            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "sum(Quantity) as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='052')) group by ItemID union all " +
            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],sum(Quantity) as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='061')) group by ItemID union all " +
            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],sum(Quantity) as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='062')) group by ItemID union all " +
            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],sum(Quantity) as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='070')) group by ItemID union all " +
            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],sum(Quantity) as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='091')) group by ItemID union all " +
            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],sum(Quantity) as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='101')) group by ItemID union all " +
            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],sum(Quantity) as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='111')) group by ItemID union all " +
            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],sum(Quantity) as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='012')) group by ItemID union all " +
            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],sum(Quantity) as[131],0 as[132],0 as[133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='134')) group by ItemID union all " +
            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],sum(Quantity) as[132],0 as[133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode in('132','131'))) group by ItemID union all " +
            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],sum(Quantity) as[133] " +
            ",0 as [135] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='133')) group by ItemID union all " +
            LapBullDept("135", tgl1, tgl2, groupID, strItemTypeID, sts) + " ) as pemakaian group by itemid with rollup " +
            ") as AB on AB.ItemID =AA.ItemID where (AA.StokAwal >0 or AA.Pemasukan>0 or AA.Retur>0 or AA.AdjustTambah>0 or AA.AdjustKurang>0)  " +
            "ORDER BY ItemCode";
            #endregion
            return strSQL;
        }
        #endregion
        public string ViewLapBul2stock(string ketBlnLalu, int yearPeriod, string tgl1, string tgl2, int groupID)
        {
            string strItemTypeID = string.Empty;
            string strJenisBrg = string.Empty;
            #region pilih group
            if (groupID == 5)
            {
                strItemTypeID = " and A.ItemTypeID = 3";
                strJenisBrg = "Biaya";
            }
            else
            {
                if (groupID == 4)
                {
                    strItemTypeID = " and A.ItemTypeID = 2";
                    strJenisBrg = "Asset";
                }
                else
                {
                    strItemTypeID = " and A.ItemTypeID = 1";
                    strJenisBrg = "Inventory";
                }
            }
            #endregion
            #region Query String
            string strSQL = "SELECT ItemCode,ItemName,UOMCode,StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,[010],[021],[022],[031],[032],[033],[034]" +
            ",[041],[042],[051],[052],[061],[062],[070],[091],[101],[111],[012],[131],[132],[133] from (SELECT " + strJenisBrg + ".id," +
            strJenisBrg + ".ItemCode, " + strJenisBrg + ".ItemName, UOM.UOMCode, " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT isnull(sum(" + ketBlnLalu + "),0) FROM SaldoInventory A WHERE ITEMID=" +
            strJenisBrg + ".id AND YearPeriod=" + yearPeriod + strItemTypeID + ") + " + "(select isnull(SUM(quantity),0) from vw_StockPurchn where ItemID=" +
            strJenisBrg + ".ID and YMD<'" + tgl1 + "' and YM=SUBSTRING('" + tgl1 + "',1,6))  END StokAwal, " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  ReceiptDetail A inner join receipt B on " +
            " A.receiptID=B.ID where A.ItemID=" + strJenisBrg + ".ID and A.RowStatus>-1  " + strItemTypeID + " and B.status > -1 AND " +
            "convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' ) END  Pemasukan, " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail A inner join adjust B on B.id = A.adjustID " +
            " WHERE  A.apv>0 and A.ItemID = " + strJenisBrg + ".ID AND A.RowStatus>-1  " + strItemTypeID + " AND B.ADJUSTTYPE='tambah' and B.status > -1  " +
            "and convert(varchar,B.AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,B.AdjustDate,112) <= '" + tgl2 + "' ) END  AdjustTambah, " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail A inner join adjust B on B.id = A.adjustID " +
            " WHERE  A.apv>0 and A.ItemID = " + strJenisBrg + ".ID AND A.RowStatus>-1  " + strItemTypeID + " AND B.ADJUSTTYPE='kurang' and B.status > -1  " +
            "and convert(varchar,B.AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,B.AdjustDate,112) <= '" + tgl2 + "' ) END  AdjustKurang, " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(Quantity),0) FROM  ReturPakaiDetail A inner join ReturPakai B on " +
            " B.ID = A.returID WHERE A.ItemID = " + strJenisBrg + ".ID AND A.RowStatus>-1  " + strItemTypeID + " AND B.status > -1 AND  " +
            "convert(varchar, B.ReturDate,112) >= '" + tgl1 + "' AND convert(varchar, B.ReturDate,112) <= '" + tgl2 + "' ) END  Retur, " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '010')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [010], " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '021')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [021], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '022')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [022], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '031')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [031], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '032')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [032], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '033')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [033], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '034')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [034], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '041')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [041], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '042')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [042], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '051')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [051], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '052')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [052], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '061')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [061], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '062')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [062], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '070')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [070], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '091')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [091], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '101')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [101], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '111')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [111], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '012')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [012], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '134')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [131], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '132')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [132], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode in('133','131'))AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [133], " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '135')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [135] " +

            "FROM  " + strJenisBrg + " INNER JOIN UOM ON " + strJenisBrg + ".UOMID = UOM.ID " +
            "WHERE (" + strJenisBrg + ".GroupID = " +
            groupID + " and  " + strJenisBrg + ".Stock = 1 and " + strJenisBrg + ".aktif=1) ) AS AA " +
            "where (StokAwal>0 or Pemasukan>0 or Retur>0 or AdjustTambah>0 or AdjustKurang>0) ORDER BY ItemCode ";
            #endregion
            return strSQL;
        }
        public string ViewLapBul2nonstock(string ketBlnLalu, int yearPeriod, string tgl1, string tgl2, int groupID)
        {
            string strItemTypeID = string.Empty;
            string strJenisBrg = string.Empty;

            if (groupID == 5)
            {
                strItemTypeID = " and A.ItemTypeID = 3";
                strJenisBrg = "Biaya";
            }
            else
            {
                if (groupID == 4)
                {
                    strItemTypeID = " and A.ItemTypeID = 2";
                    strJenisBrg = "Asset";
                }
                else
                {
                    strItemTypeID = " and A.ItemTypeID = 1";
                    strJenisBrg = "Inventory";
                }
            }

            return "SELECT ItemCode,ItemName,UOMCode,StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,[010],[021],[022],[031],[032],[033],[034]" +
            ",[041],[042],[051],[052],[061],[062],[070],[091],[101],[111],[012],[131],[132],[133] from (SELECT " + strJenisBrg + ".id," +
            strJenisBrg + ".ItemCode, " + strJenisBrg + ".ItemName, UOM.UOMCode, " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT isnull(sum(" + ketBlnLalu + "),0) FROM SaldoInventory A WHERE ITEMID=" +
            strJenisBrg + ".id AND YearPeriod=" + yearPeriod + strItemTypeID + ") + " + "(select isnull(SUM(quantity),0) from vw_StockPurchn where ItemID=" +
            strJenisBrg + ".ID and YMD<'" + tgl1 + "' and YM=SUBSTRING('" + tgl1 + "',1,6))  END StokAwal, " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  ReceiptDetail A inner join receipt B on " +
            " A.receiptID=B.ID where A.ItemID=" + strJenisBrg + ".ID and A.RowStatus>-1  " + strItemTypeID + " and B.status > -1 AND " +
            "convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' ) END  Pemasukan, " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail A inner join adjust B on B.id = A.adjustID " +
            " WHERE  A.apv>0 and A.ItemID = " + strJenisBrg + ".ID AND A.RowStatus>-1  " + strItemTypeID + " AND B.ADJUSTTYPE='tambah' and B.status > -1  " +
            "and convert(varchar,B.AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,B.AdjustDate,112) <= '" + tgl2 + "' ) END  AdjustTambah, " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail A inner join adjust B on B.id = A.adjustID " +
            " WHERE  A.apv>0 and A.ItemID = " + strJenisBrg + ".ID AND A.RowStatus>-1  " + strItemTypeID + " AND B.ADJUSTTYPE='kurang' and B.status > -1  " +
            "and convert(varchar,B.AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,B.AdjustDate,112) <= '" + tgl2 + "' ) END  AdjustKurang, " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(Quantity),0) FROM  ReturPakaiDetail A inner join ReturPakai B on " +
            " B.ID = A.returID WHERE A.ItemID = " + strJenisBrg + ".ID AND A.RowStatus>-1  " + strItemTypeID + " AND B.status > -1 AND  " +
            "convert(varchar, B.ReturDate,112) >= '" + tgl1 + "' AND convert(varchar, B.ReturDate,112) <= '" + tgl2 + "' ) END  Retur, " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '010')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [010], " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '021')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [021], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '022')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [022], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '031')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [031], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '032')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [032], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '033')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [033], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '034')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [034], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '041')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [041], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '042')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [042], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '051')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [051], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '052')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [052], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '061')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [061], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '062')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [062], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '070')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [070], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '091')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [091], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '101')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [101], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '111')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [111], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '012')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [012], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '131')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [131], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '132')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [132], " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            "ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            "AND (C.DeptCode = '133')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [133] " +
            "FROM  " + strJenisBrg + " INNER JOIN UOM ON " + strJenisBrg + ".UOMID = UOM.ID WHERE (" + strJenisBrg + ".GroupID = " + groupID +
            " and  " + strJenisBrg + ".Stock = 0" + ") ) AS AA where (StokAwal>0 or Pemasukan>0 or Retur>0 or AdjustTambah>0 or AdjustKurang>0) ORDER BY ItemCode ";


        }
        public string ViewHarianBakuBantu(int userID, int groupID, string awalReport, string akhirReport)
        {
            return "select A.ItemCode,A.ItemName,A.UomCode,A.StokAwal,A.Pemasukan,A.Retur,A.AdjustTambah,A.AdjustKurang,A.Pemakaian,A.DeptCode,B.Jumlah as EndingStok,A.GroupID " +
                "from LaporanBulanan as A, Inventory as B where A.ItemID=B.ID and A.UserID=" + userID + " and A.GroupID in (1,2) and A.TglCetak>='" + awalReport + "' and A.TglCetak<='" + akhirReport + "' " +
                "and (A.StokAwal+A.Pemasukan+A.Retur+AdjustTambah+A.AdjustTambah+A.Pemakaian+B.Jumlah)>0 order by A.GroupID,A.ItemCode";
        }
        public string ViewHarianBakuBantu3a(string ketBlnLalu, int yearPeriod, string tglAwal, string tglAkhir, string tgl1, string tgl2, int groupID, int itemtypeID)
        {
            string invGroupID = string.Empty;
            if (groupID == 1 || groupID == 2)
            {
                invGroupID = "Inventory.GroupID in (1,2)";
            }
            else
                invGroupID = "Inventory.GroupID in (" + groupID + ")";
            if (groupID == 10)
            {
                return "select ItemCode,ItemName,UOMCode,stokawal as StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,Pemakaian," +
                "(stokawal+Pemasukan+Retur+AdjustTambah-AdjustKurang-Pemakaian) as EndingStok," +
                "GroupID,MinStock from (SELECT Inventory.minstock, Inventory.id,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, " +
                "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(" + ketBlnLalu + ",0) FROM SaldoInventory WHERE ITEMID=Inventory.id AND YearPeriod=" + yearPeriod + " and ItemTypeID=" + itemtypeID + ") END stokawal," +
                    //"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  MasukAwal," +
                    //"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjTambahAwal," +
                    //"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjKurangAwal," +
                    //"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QTY),0) FROM  ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, " +
                    //"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PakaiAwal, " +

                //"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID  AND Status > -1 AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan," +
                "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(toQTY),0) FROM  convertan WHERE toItemID=Inventory.ID and RowStatus>-1  and ItemTypeID=1 AND convert(varchar,createdtime,112) >= '" + tgl1 + "' AND convert(varchar,createdtime,112) <= '" + tgl2 + "' ) END  Pemasukan," +
                "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1  and ItemTypeID=" + itemtypeID + " AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah," +
                "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1  and ItemTypeID=" + itemtypeID + " AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang," +
                "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReturPakaiDetail WHERE ReturPakaiDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM ReturPakai WHERE convert(varchar,ReturDate,112) >= '" + tgl1 + "' AND convert(varchar,ReturDate,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur, " +
                "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiDetail.ItemTypeID=" + itemtypeID + " and PakaiID IN (SELECT ID FROM Pakai WHERE  Status = 3 and convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status=3)) END  Pemakaian " +
                "FROM  Inventory INNER JOIN       UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where ((stokawal)>0 or (stokawal+Pemasukan+AdjustTambah+AdjustKurang+Pemakaian)>0) ORDER BY ItemCode";
            }
            else
            {
                return "select ItemCode,ItemName,UOMCode,stokawal as StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,Pemakaian," +
                "(stokawal+Pemasukan+Retur+AdjustTambah-AdjustKurang-Pemakaian) as EndingStok," +
                "GroupID,MinStock from (SELECT Inventory.minstock,Inventory.id,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, " +
                "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(" + ketBlnLalu + ",0) FROM SaldoInventory WHERE ITEMID=Inventory.id AND YearPeriod=" + yearPeriod + " and ItemTypeID=" + itemtypeID + ") END stokawal," +
                    //"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  MasukAwal," +
                    //"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjTambahAwal," +
                    //"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjKurangAwal," +
                    //"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QTY),0) FROM  ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, " +
                    //"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PakaiAwal, " +

                "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 and ItemTypeID=" + itemtypeID + "  AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID  AND Status > -1 AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan," +
                "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 and ItemTypeID=" + itemtypeID + "  AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah," +
                "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 and ItemTypeID=" + itemtypeID + "  AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang," +
                "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReturPakaiDetail WHERE ReturPakaiDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM ReturPakai WHERE convert(varchar,ReturDate,112) >= '" + tgl1 + "' AND convert(varchar,ReturDate,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur, " +
                "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiDetail.ItemTypeID=" + itemtypeID + " and PakaiID IN (SELECT ID FROM Pakai WHERE  Status = 3 and convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status=3)) END  Pemakaian " +
                "FROM  Inventory INNER JOIN       UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where ((stokawal)>0 or (stokawal+Pemasukan+AdjustTambah+AdjustKurang+Pemakaian)>0) ORDER BY ItemCode";
            }
        }
        public string ViewHarianBakuBantu3(string ketBlnLalu, int yearPeriod, string tglAwal, string tglAkhir, string tgl1, string tgl2, int groupID, int itemtypeID)
        {
            string invGroupID = string.Empty;
            if (groupID == 1 || groupID == 2)
            {
                invGroupID = "Inventory.GroupID in (1,2)";
            }
            else
                invGroupID = "Inventory.GroupID in (" + groupID + ")";
            if (groupID == 10)
            {
                return "select ItemCode,ItemName,UOMCode,stokawal+MasukAwal+ReturAwal+AdjTambahAwal-AdjKurangAwal-PakaiAwal as StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,Pemakaian," +
                    "((stokawal+MasukAwal+AdjTambahAwal+ReturAwal-AdjKurangAwal-PakaiAwal)+Pemasukan+Retur+AdjustTambah-AdjustKurang-Pemakaian) as EndingStok," +
                    "GroupID,MinStock from (SELECT Inventory.minstock,Inventory.id,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, " +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(" + ketBlnLalu + ",0) FROM SaldoInventory WHERE ITEMID=Inventory.id AND YearPeriod=" + yearPeriod + " and ItemTypeID=" + itemtypeID + ") END stokawal," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(toQTY),0) FROM  convertan WHERE toItemID=Inventory.ID and RowStatus>-1 and ItemTypeID=1  AND convert(varchar,createdtime,112) >= '" + tglAwal + "' AND convert(varchar,createdtime,112) <= '" + tglAkhir + "' ) END  MasukAwal," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 and ItemTypeID=" + itemtypeID + "  AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjTambahAwal," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 and ItemTypeID=" + itemtypeID + "  AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjKurangAwal," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReturPakaiDetail WHERE ReturPakaiDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM ReturPakai WHERE convert(varchar,ReturDate,112) >= '" + tglAwal + "' AND convert(varchar,ReturDate,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, " +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PakaiAwal, " +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(toQTY),0) FROM  convertan WHERE toItemID=Inventory.ID and RowStatus>-1 and ItemTypeID=" + itemtypeID + "  AND convert(varchar,createdtime,112) >= '" + tgl1 + "' AND convert(varchar,createdtime,112) <= '" + tgl2 + "' ) END  Pemasukan," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 and ItemTypeID=" + itemtypeID + "  AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 and ItemTypeID=" + itemtypeID + "  AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReturPakaiDetail WHERE ReturPakaiDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM ReturPakai WHERE convert(varchar,ReturDate,112) >= '" + tgl1 + "' AND convert(varchar,ReturDate,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur, " +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiDetail.ItemTypeID=" + itemtypeID + " and PakaiID IN (SELECT ID FROM Pakai WHERE Status = 3 and convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian " +
                    "FROM  Inventory INNER JOIN  UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where ((stokawal+MasukAwal+AdjTambahAwal+AdjKurangAwal+PakaiAwal)>0 or ((stokawal+MasukAwal+AdjTambahAwal+ReturAwal+AdjKurangAwal+PakaiAwal)+Pemasukan+AdjustTambah+AdjustKurang+Pemakaian)>0) ORDER BY ItemCode";
            }
            else
            {
                return "select ItemCode,ItemName,UOMCode,stokawal+MasukAwal+ReturAwal+AdjTambahAwal-AdjKurangAwal-PakaiAwal as StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,Pemakaian," +
                    "((stokawal+MasukAwal+AdjTambahAwal+ReturAwal-AdjKurangAwal-PakaiAwal)+Pemasukan+Retur+AdjustTambah-AdjustKurang-Pemakaian) as EndingStok," +
                    "GroupID ,MinStock from (SELECT Inventory.minstock,Inventory.id,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, " +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(" + ketBlnLalu + ",0) FROM SaldoInventory WHERE ITEMID=Inventory.id AND YearPeriod=" + yearPeriod + " and ItemTypeID=" + itemtypeID + ") END stokawal," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 and ItemTypeID=" + itemtypeID + "  AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID  AND Status > -1 AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  MasukAwal," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 and ItemTypeID=" + itemtypeID + "  AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjTambahAwal," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 and ItemTypeID=" + itemtypeID + "  AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjKurangAwal," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReturPakaiDetail WHERE ReturPakaiDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM ReturPakai WHERE convert(varchar,ReturDate,112) >= '" + tglAwal + "' AND convert(varchar,ReturDate,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, " +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiDetail.ItemTypeID=" + itemtypeID + " and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PakaiAwal, " +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 and ItemTypeID=" + itemtypeID + "  AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND Status>-1 and  convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 and ItemTypeID=" + itemtypeID + "  AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 and ItemTypeID=" + itemtypeID + "  AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReturPakaiDetail WHERE ReturPakaiDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM ReturPakai WHERE convert(varchar,ReturDate,112) >= '" + tgl1 + "' AND convert(varchar,ReturDate,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur, " +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiDetail.ItemTypeID=1 and PakaiID IN (SELECT ID FROM Pakai WHERE  Status = 3 and convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian " +
                    "FROM  Inventory INNER JOIN UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where ((stokawal+MasukAwal+AdjTambahAwal+AdjKurangAwal+PakaiAwal)>0 or ((stokawal+MasukAwal+AdjTambahAwal+ReturAwal+AdjKurangAwal+PakaiAwal)+Pemasukan+AdjustTambah+AdjustKurang+Pemakaian)>0) ORDER BY ItemCode";
            }
        }
        public string ViewWarningOrdera(string ketBlnLalu, int yearPeriod, string tglAwal, string tglAkhir, string tgl1, string tgl2, int groupID)
        {
            string invGroupID = string.Empty;
            //if (groupID == 1 || groupID == 2)
            //{
            //    invGroupID = "Inventory.GroupID in (1,2)";
            //}
            //else
            invGroupID = "Inventory.GroupID in (" + groupID + ") and Inventory.Aktif=1 and stock=1 and MinStock>=0 ";
            if (groupID == 10)
            {
                return "select ItemCode,ItemName,UOMCode,stokawal as StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,Pemakaian," +
                "(stokawal+Pemasukan+Retur+AdjustTambah-AdjustKurang-Pemakaian) as EndingStok," +
                "GroupID,MinStock,reorder from (SELECT Inventory.reorder,Inventory.minstock, Inventory.id,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, " +
                "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(" + ketBlnLalu + ",0) FROM SaldoInventory WHERE ITEMID=Inventory.id AND YearPeriod=" + yearPeriod + " and ItemTypeID=1) END stokawal," +
                    //"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  MasukAwal," +
                    //"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjTambahAwal," +
                    //"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjKurangAwal," +
                    //"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QTY),0) FROM  ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, " +
                    //"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PakaiAwal, " +

                //"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID  AND Status > -1 AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan," +
                "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(toQTY),0) FROM  convertan WHERE toItemID=Inventory.ID and RowStatus>-1 AND convert(varchar,createdtime,112) >= '" + tgl1 + "' AND convert(varchar,createdtime,112) <= '" + tgl2 + "' ) END  Pemasukan," +
                "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah," +
                "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang," +
                "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReturPakaiDetail WHERE ReturPakaiDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM ReturPakai WHERE convert(varchar,ReturDate,112) >= '" + tgl1 + "' AND convert(varchar,ReturDate,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur, " +
                "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian " +
                "FROM  Inventory INNER JOIN       UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where (stokawal+Pemasukan+Retur+AdjustTambah-AdjustKurang-Pemakaian)<=reorder ORDER BY ItemCode";
            }
            else
            {
                return "select ItemCode,ItemName,UOMCode,stokawal as StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,Pemakaian," +
                "(stokawal+Pemasukan+Retur+AdjustTambah-AdjustKurang-Pemakaian) as EndingStok," +
                "GroupID,MinStock ,reorder from (SELECT Inventory.reorder, Inventory.minstock,Inventory.id,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, " +
                "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(" + ketBlnLalu + ",0) FROM SaldoInventory WHERE ITEMID=Inventory.id AND YearPeriod=" + yearPeriod + " and ItemTypeID=1) END stokawal," +
                    //"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  MasukAwal," +
                    //"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjTambahAwal," +
                    //"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjKurangAwal," +
                    //"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QTY),0) FROM  ReturDetail WHERE ReturDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM Retur WHERE convert(varchar,CreatedTime,112) >= '" + tglAwal + "' AND convert(varchar,CreatedTime,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, " +
                    //"CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PakaiAwal, " +

                "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID  AND Status > -1 AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan," +
                "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah," +
                "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang," +
                "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReturPakaiDetail WHERE ReturPakaiDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM ReturPakai WHERE convert(varchar,ReturDate,112) >= '" + tgl1 + "' AND convert(varchar,ReturDate,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur, " +
                "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian " +
                "FROM  Inventory INNER JOIN  UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where (stokawal+Pemasukan+Retur+AdjustTambah-AdjustKurang-Pemakaian)<=reorder ORDER BY ItemCode";
            }
        }
        public string ViewWarningOrder(string ketBlnLalu, int yearPeriod, string tglAwal, string tglAkhir, string tgl1, string tgl2, int groupID)
        {
            string invGroupID = string.Empty;
            //if (groupID == 1 || groupID == 2)
            //{
            //    invGroupID = "Inventory.GroupID in (1,2)";
            //}
            //else
            invGroupID = "Inventory.GroupID in (" + groupID + ") and Inventory.Aktif=1 and Stock=1 and MinStock>=0 ";
            if (groupID == 10)
            {
                return "select ItemCode,ItemName,UOMCode,stokawal+MasukAwal+ReturAwal+AdjTambahAwal-AdjKurangAwal-PakaiAwal as StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,Pemakaian," +
                    "((stokawal+MasukAwal+AdjTambahAwal+ReturAwal-AdjKurangAwal-PakaiAwal)+Pemasukan+Retur+AdjustTambah-AdjustKurang-Pemakaian) as EndingStok," +
                    "GroupID,MinStock ,reorder from (SELECT Inventory.reorder, Inventory.minstock,Inventory.id,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, " +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(" + ketBlnLalu + ",0) FROM SaldoInventory WHERE ITEMID=Inventory.id AND YearPeriod=" + yearPeriod + " and ItemTypeID=1) END stokawal," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(toQTY),0) FROM  convertan WHERE toItemID=Inventory.ID and RowStatus>-1 AND convert(varchar,createdtime,112) >= '" + tglAwal + "' AND convert(varchar,createdtime,112) <= '" + tglAkhir + "' ) END  MasukAwal," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjTambahAwal," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjKurangAwal," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReturPakaiDetail WHERE ReturPakaiDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM ReturPakai WHERE convert(varchar,ReturDate,112) >= '" + tglAwal + "' AND convert(varchar,ReturDate,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, " +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PakaiAwal, " +

                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(toQTY),0) FROM  convertan WHERE toItemID=Inventory.ID and RowStatus>-1 AND convert(varchar,createdtime,112) >= '" + tgl1 + "' AND convert(varchar,createdtime,112) <= '" + tgl2 + "' ) END  Pemasukan," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReturPakaiDetail WHERE ReturPakaiDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM ReturPakai WHERE convert(varchar,ReturDate,112) >= '" + tgl1 + "' AND convert(varchar,ReturDate,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur, " +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian " +
                    "FROM  Inventory INNER JOIN       UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where ((stokawal+MasukAwal+AdjTambahAwal+ReturAwal-AdjKurangAwal-PakaiAwal)+Pemasukan+Retur+AdjustTambah-AdjustKurang-Pemakaian)<=reorder ORDER BY ItemCode";
            }
            else
            {
                return "select ItemCode,ItemName,UOMCode,stokawal+MasukAwal+ReturAwal+AdjTambahAwal-AdjKurangAwal-PakaiAwal as StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,Pemakaian," +
                    "((stokawal+MasukAwal+AdjTambahAwal+ReturAwal-AdjKurangAwal-PakaiAwal)+Pemasukan+Retur+AdjustTambah-AdjustKurang-Pemakaian) as EndingStok," +
                    "GroupID ,MinStock ,reorder from (SELECT Inventory.reorder, Inventory.minstock,Inventory.id,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, " +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(" + ketBlnLalu + ",0) FROM SaldoInventory WHERE ITEMID=Inventory.id AND YearPeriod=" + yearPeriod + " and ItemTypeID=1) END stokawal," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID  AND Status > -1 AND convert(varchar,ReceiptDate,112) >= '" + tglAwal + "' AND convert(varchar,ReceiptDate,112) <= '" + tglAkhir + "' )) END  MasukAwal," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "') ) END  AdjTambahAwal," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tglAwal + "' AND convert(varchar,AdjustDate,112) <= '" + tglAkhir + "' )) END  AdjKurangAwal," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReturPakaiDetail WHERE ReturPakaiDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM ReturPakai WHERE convert(varchar,ReturDate,112) >= '" + tglAwal + "' AND convert(varchar,ReturDate,112) <= '" + tglAkhir + "' and Status>-1 )) END  ReturAwal, " +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tglAwal + "' AND convert(varchar,PakaiDate,112) <= '" + tglAkhir + "' and Status>-1)) END  PakaiAwal, " +

                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN (SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND Status>-1 and  convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN (SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang," +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(QUANTITY),0) FROM  ReturPakaiDetail WHERE ReturPakaiDetail.ItemID=Inventory.ID and ReturID IN (SELECT ID FROM ReturPakai WHERE convert(varchar,ReturDate,112) >= '" + tgl1 + "' AND convert(varchar,ReturDate,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur, " +
                    "CASE WHEN Inventory.ID > 0 THEN (SELECT isnull(SUM(Quantity),0) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN (SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian " +
                    "FROM  Inventory INNER JOIN       UOM ON Inventory.UOMID = UOM.ID WHERE (" + invGroupID + ") ) as AA where ((stokawal+MasukAwal+AdjTambahAwal+ReturAwal-AdjKurangAwal-PakaiAwal)+Pemasukan+Retur+AdjustTambah-AdjustKurang-Pemakaian)<=reorder ORDER BY ItemCode";
            }
        }
        public string ViewHarianBakuBantu2(string ketBlnLalu, int yearPeriod, string tgl1, string tgl2, int groupID)
        {
            string invGroupID = string.Empty;
            if (groupID == 1)
            {
                invGroupID = "Inventory.GroupID in (1,2)";
            }
            else
                invGroupID = "Inventory.GroupID in (" + groupID + ")";

            return "select ItemCode,ItemName,UOMCode,stokawal,Pemasukan,Retur,AdjustTambah,AdjustKurang,Pemakaian,(stokawal+Pemasukan+AdjustTambah-AdjustKurang-Pemakaian) as EndingStok,GroupID " +
                "from (SELECT Inventory.id,Inventory.ItemCode, Inventory.ItemName, UOM.UOMCode,Inventory.GroupID, " +
                "CASE WHEN Inventory.ID > 0 THEN " +
                "(SELECT cast(isnull(" + ketBlnLalu + ",0) as decimal) FROM SaldoInventory WHERE ITEMID=Inventory.id AND YearPeriod=" + yearPeriod + " and ItemTypeID=1) END stokawal," +
                "CASE WHEN Inventory.ID > 0 THEN " +
                "(SELECT cast(ISNULL(SUM(QUANTITY),0) as decimal) FROM  ReceiptDetail WHERE ItemID=Inventory.ID and RowStatus>-1 AND ReceiptID IN " +
                "(SELECT ID FROM Receipt WHERE ID = ReceiptDetail.ReceiptID AND convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' )) END  Pemasukan," +
                "CASE WHEN Inventory.ID > 0 THEN " +
                "(SELECT cast(ISNULL(SUM(QUANTITY),0) as decimal) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN " +
                "(SELECT ID FROM Adjust WHERE ADJUSTTYPE='tambah' and Status>-1 AND convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "') ) END  AdjustTambah," +
                "CASE WHEN Inventory.ID > 0 THEN " +
                "(SELECT cast(ISNULL(SUM(QUANTITY),0) as decimal) FROM  AdjustDetail WHERE  AdjustDetail.apv>0 and ItemID = Inventory.ID and RowStatus>-1 AND AdjustID IN " +
                "(SELECT ID FROM Adjust WHERE ADJUSTTYPE='kurang' and Status>-1 AND  convert(varchar,AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,AdjustDate,112) <= '" + tgl2 + "' )) END  AdjustKurang," +
                "CASE WHEN Inventory.ID > 0 THEN " +
                "(SELECT cast(ISNULL(SUM(QTY),0) as decimal) FROM  ReturPakaiDetail WHERE ReturPakaiDetail.ItemID=Inventory.ID and ReturPakaiDetail.ReturID IN " +
                "(SELECT ID FROM ReturPakai WHERE convert(varchar,CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,CreatedTime,112) <= '" + tgl2 + "' and Status>-1 )) END  Retur, " +
                "CASE WHEN Inventory.ID > 0 THEN " +
                "(SELECT cast(ISNULL(SUM(Quantity),0) as decimal) FROM  PakaiDetail WHERE PakaiDetail.ItemID=Inventory.ID and PakaiDetail.RowStatus>-1 and PakaiID IN " +
                "(SELECT ID FROM Pakai WHERE convert(varchar,PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,PakaiDate,112) <= '" + tgl2 + "' and Status>-1)) END  Pemakaian " +
                "FROM  Inventory INNER JOIN " +
                "      UOM ON Inventory.UOMID = UOM.ID " +
                "WHERE (" + invGroupID + ") " +
                ") as AA where stokawal>0 or Pemasukan>0 or AdjustKurang>0 or AdjustTambah>0 or Retur>0 or Pemakaian>0 " +
                "ORDER BY ItemCode";
        }
        public string ViewHarianA(int userID, int groupID, string awalReport)
        {
            //return "select A.ItemCode,A.ItemName,A.UomCode,A.NoDoc,A.StokAwal,A.Pemasukan,A.Retur,A.AdjustTambah,A.AdjustKurang,A.Pemakaian,A.DeptCode,A.StokAkhir,A.GroupID,A.Urutan,((A.StokAwal+A.Pemasukan+A.Retur+A.AdjustTambah)-(A.AdjustKurang+A.Pemakaian)) as SaldoAkhir " +
            //    "from LaporanHarian as A, Inventory as B where A.ItemID=B.ID and A.UserID=" + userID + " and A.GroupID in (1,2) and A.TglCetak>='" + awalReport + "' and A.TglCetak<='" + akhirReport + "' " +
            //    "and (A.StokAwal+A.Pemasukan+A.Retur+AdjustTambah+A.AdjustTambah+A.Pemakaian+A.StokAkhir)>0 order by A.ItemCode,A.Urutan";
            string strSQL = "select A.ItemCode,A.ItemName,A.UomCode,A.NoDoc,A.StokAwal,A.Pemasukan,A.Retur,A.AdjustTambah,A.AdjustKurang,A.Pemakaian,A.DeptCode,A.StokAkhir,A.GroupID,A.Urutan,A.ID,recid,((A.StokAwal+A.Pemasukan+A.Retur+A.AdjustTambah)-(A.AdjustKurang+A.Pemakaian)) as SaldoAkhir " +
                "from LaporanHarian as A, Inventory as B where A.ItemID=B.ID and A.UserID=" + userID + " and A.GroupID in (" + groupID + ") and A.TglCetak='" + awalReport + "' " +
                "and (A.StokAwal>0 or A.Pemasukan>0 or A.Retur>0 or AdjustTambah>0 or A.Adjustkurang>0 or A.Pemakaian>0 or A.StokAkhir>0 ) order by ItemCode,Urutan,StokAkhir desc";
            return strSQL;
            //" order by ItemCode,Urutan,StokAkhir desc";
        }
        public string ViewOutstandingSPP(string drTgl, string sdTgl, int groupID)
        {
            string strGroupID = string.Empty;
            if (groupID > 0)
                strGroupID = " and B.GroupId =" + groupID;
            else
                strGroupID = " ";

            //return "select A.ID,A.NoSPP,convert(varchar,A.Minta,103) as TglSPP,convert(varchar,A.lastmodifiedtime,103) as lastmodifiedtime,B.Quantity,B.QtyPO,B.Quantity-B.QtyPO as QtySisa, " +
            //    "case B.ItemTypeID when 1 then (select ItemName from Inventory where B.ItemID=Inventory.ID) "+
            //    "when 2 then (select ItemName from Asset where B.ItemID=Asset.ID) "+
            //    "when 3 then (select ItemName from Biaya where B.ItemID=Biaya.ID) "+
            //    "else '' end ItemName," +
            //    "case when A.HeadID >=0 then(select username from Users where ID=A.HeadID) end NamaHead,case permintaantype " +
            //    " when 1 then 'Top Urgent' when 2 then 'Biasa' when 3 then 'SesuaiSchedule' end Prioritas,convert(varchar,A.createdtime,103) as createdtime   from SPP as A, SPPDetail as B where A.ID=B.SPPID and " +
            //    "A.Status>-1 and B.Status>-1  and B.Quantity-B.QtyPO > 0 and A.approval>=2 and B.ID not in (select sppdetailid from popurchndetail where poid in(select id from popurchn where convert(varchar,createdtime,112) >='" + drTgl + "' )) " +
            //    " and convert(varchar,A.Minta,112) >='" + drTgl + "' and convert(varchar,A.Minta,112)<='" + sdTgl + "' " + strGroupID + " order by A.NoSPP";
            //add by razib WO-IT-K0120620 Tgl:02072020
            return "select A.ID,A.NoSPP,convert(varchar,A.Minta,103) as TglSPP,convert(varchar,A.lastmodifiedtime,103) as lastmodifiedtime,B.Quantity,B.QtyPO,B.Quantity-B.QtyPO as QtySisa, " +
               "case B.ItemTypeID when 1 then (select ItemName from Inventory where B.ItemID=Inventory.ID) " +
               "when 2 then (select ItemName from Asset where B.ItemID=Asset.ID) " +
               "when 3 then (select ItemName from Biaya where B.ItemID=Biaya.ID)+' - '+ B.Keterangan  " +
               "else '' end ItemName," +
               "case B.ItemTypeID when 1 then (select ItemCode from Inventory where B.ItemID=Inventory.ID) " +
               "when 2 then (select ItemCode from Asset where B.ItemID=Asset.ID) " +
               "when 3 then (select ItemCode from Biaya where B.ItemID=Biaya.ID) " +
               "else '' end ItemCode," +
               "case when A.HeadID >=0 then(select username from Users where ID=A.HeadID) end NamaHead,case permintaantype " +
               " when 1 then 'Top Urgent' when 2 then 'Biasa' when 3 then 'SesuaiSchedule' end Prioritas," +
               " case when B.uomid>0 then (select uomcode from uom where id=B.uomid)end satuan, convert(varchar,A.createdtime,103) as createdtime,"+
               " case when B.ItemTypeID=3 Then B.Keterangan1 else B.Keterangan end as Keterangan,(select stock from Inventory where B.ItemID=Inventory.id)Stock," +
			   " case when (select stock from Inventory where B.ItemID=Inventory.id)=1 then 'Stock' else 'Non Stock' end Ket2 " +
               " from SPP as A, SPPDetail as B where A.ID=B.SPPID and " +
               "A.Status>-1 and B.Status>-1  and B.Quantity-B.QtyPO > 0 and A.approval>=3  " +
               "  and convert(varchar,A.ApproveDate3,112) >='" + drTgl + "' and convert(varchar,A.ApproveDate3,112)<='" + sdTgl + "' " + strGroupID + " order by A.NoSPP";
        }
        public string ViewOutstandingSPP1(string drTgl, string sdTgl, string docPref)
        {
            string strGroupID = string.Empty;
            if (docPref == "0")
                strGroupID = " ";
            else
                strGroupID = " and A.nospp like '%" + docPref + "%' ";
            //return "select A.ID,A.NoSPP,convert(varchar,A.Minta,103) as TglSPP,convert(varchar,A.lastmodifiedtime,103) as lastmodifiedtime,B.Quantity,B.QtyPO,B.Quantity-B.QtyPO as QtySisa, " +
            //    "case B.ItemTypeID when 1 then (select ItemName from Inventory where B.ItemID=Inventory.ID) " +
            //    "when 2 then (select ItemName from Asset where B.ItemID=Asset.ID) " +
            //    "when 3 then (select ItemName from Biaya where B.ItemID=Biaya.ID) " +
            //    "else '' end ItemName," +
            //    "case when A.HeadID >=0 then(select username from Users where ID=A.HeadID) end NamaHead,case permintaantype " +
            //    " when 1 then 'Top Urgent' when 2 then 'Biasa' when 3 then 'SesuaiSchedule' end Prioritas,convert(varchar,A.createdtime,103) as createdtime from SPP as A, SPPDetail as B where A.ID=B.SPPID and " +
            //    "A.Status>-1 and B.Status>-1  and B.Quantity-B.QtyPO > 0 and A.approval>=2 and B.ID not in (select sppdetailid from popurchndetail where poid in(select id from popurchn where convert(varchar,createdtime,112) >='" + drTgl + "' )) " +
            //    " and convert(varchar,A.Minta,112) >='" + drTgl + "' and convert(varchar,A.Minta,112)<='" + sdTgl + "' " + strGroupID + " order by A.NoSPP";
            //add by razib WO-IT-K0120620 Tgl:02072020
            return "select A.ID,A.NoSPP,convert(varchar,A.Minta,103) as TglSPP,convert(varchar,A.lastmodifiedtime,103) as lastmodifiedtime,B.Quantity,B.QtyPO,B.Quantity-B.QtyPO as QtySisa, " +
                "case B.ItemTypeID when 1 then (select ItemName from Inventory where B.ItemID=Inventory.ID) " +
                "when 2 then (select ItemName from Asset where B.ItemID=Asset.ID) " +
                "when 3 then (select ItemName from Biaya where B.ItemID=Biaya.ID)+' - '+ B.Keterangan " +
                "else '' end ItemName," +
                "case B.ItemTypeID when 1 then (select ItemCode from Inventory where B.ItemID=Inventory.ID) " +
               "when 2 then (select ItemCode from Asset where B.ItemID=Asset.ID) " +
               "when 3 then (select ItemCode from Biaya where B.ItemID=Biaya.ID) " +
               "else '' end ItemCode," +
                "case when A.HeadID >=0 then(select username from Users where ID=A.HeadID) end NamaHead,case permintaantype " +
                " when 1 then 'Top Urgent' when 2 then 'Biasa' when 3 then 'SesuaiSchedule' end Prioritas," +
                " case when B.uomid>0 then (select uomcode from uom where id=B.uomid)end satuan,convert(varchar,A.createdtime,103) as createdtime, " +
                " case when B.ItemTypeID=3 Then B.Keterangan1 else B.Keterangan end as Keterangan,(select stock from Inventory where B.ItemID=Inventory.id)Stock," +
			   " case when (select stock from Inventory where B.ItemID=Inventory.id)=1 then 'Stock' else 'Non Stock' end Ket2 " +
                " from SPP as A, SPPDetail as B where A.ID=B.SPPID and " +
                "A.Status>-1 and B.Status>-1  and B.Quantity-B.QtyPO > 0 and A.approval>=3  " +
                " and convert(varchar,A.createdtime,112) >='" + drTgl + "' and convert(varchar,A.createdtime,112)<='" + sdTgl + "' " + strGroupID + " order by A.NoSPP";
        }
        public string ViewOutstandingSPP2(string drTgl, string sdTgl, int groupID)
        {
            string strGroupID = string.Empty;
            if (groupID > 0)
                strGroupID = " and B.GroupId =" + groupID;
            else
                strGroupID = " ";
            return "select A.ID,A.NoSPP,convert(varchar,A.Minta,103) as TglSPP,convert(varchar,A.lastmodifiedtime,103) as lastmodifiedtime,B.Quantity,B.QtyPO,B.Quantity-B.QtyPO as QtySisa, " +
               "case B.ItemTypeID when 1 then (select ItemName from Inventory where B.ItemID=Inventory.ID) " +
               "when 2 then (select ItemName from Asset where B.ItemID=Asset.ID) " +
               "when 3 then (select ItemName from Biaya where B.ItemID=Biaya.ID)+' - '+ B.Keterangan  " +
               "else '' end ItemName," +
               "case B.ItemTypeID when 1 then (select ItemCode from Inventory where B.ItemID=Inventory.ID) " +
               "when 2 then (select ItemCode from Asset where B.ItemID=Asset.ID) " +
               "when 3 then (select ItemCode from Biaya where B.ItemID=Biaya.ID) " +
               "else '' end ItemCode," +
               "case when A.HeadID >=0 then(select username from Users where ID=A.HeadID) end NamaHead,case permintaantype " +
               " when 1 then 'Top Urgent' when 2 then 'Biasa' when 3 then 'SesuaiSchedule' end Prioritas," +
               " case when B.uomid>0 then (select uomcode from uom where id=B.uomid)end satuan, convert(varchar,A.createdtime,103) as createdtime," +
               " case when B.ItemTypeID=3 Then B.Keterangan1 else B.Keterangan end as Keterangan,(select stock from Inventory where B.ItemID=Inventory.id)Stock," +
               " case when (select stock from Inventory where B.ItemID=Inventory.id)=1 then 'Stock' else 'Non Stock' end Ket2 " +
               " from SPP as A, SPPDetail as B, Inventory as I where A.ID=B.SPPID and B.ItemID=I.ID and " +
               "A.Status>-1 and B.Status>-1  and B.Quantity-B.QtyPO > 0 and A.approval>=3 and I.Stock=1 " +
               "  and convert(varchar,A.ApproveDate3,112) >='" + drTgl + "' and convert(varchar,A.ApproveDate3,112)<='" + sdTgl + "' " + strGroupID + " and B.GroupId in(8,9,12) order by A.NoSPP";
        }
        public string ViewOutstandingSPP3(string drTgl, string sdTgl, int groupID)
        {
            string strGroupID = string.Empty;
            if (groupID > 0)
                strGroupID = " and B.GroupId =" + groupID;
            else
                strGroupID = " ";
            return "select A.ID,A.NoSPP,convert(varchar,A.Minta,103) as TglSPP,convert(varchar,A.lastmodifiedtime,103) as lastmodifiedtime,B.Quantity,B.QtyPO,B.Quantity-B.QtyPO as QtySisa, " +
               "case B.ItemTypeID when 1 then (select ItemName from Inventory where B.ItemID=Inventory.ID) " +
               "when 2 then (select ItemName from Asset where B.ItemID=Asset.ID) " +
               "when 3 then (select ItemName from Biaya where B.ItemID=Biaya.ID)+' - '+ B.Keterangan  " +
               "else '' end ItemName," +
               "case B.ItemTypeID when 1 then (select ItemCode from Inventory where B.ItemID=Inventory.ID) " +
               "when 2 then (select ItemCode from Asset where B.ItemID=Asset.ID) " +
               "when 3 then (select ItemCode from Biaya where B.ItemID=Biaya.ID) " +
               "else '' end ItemCode," +
               "case when A.HeadID >=0 then(select username from Users where ID=A.HeadID) end NamaHead,case permintaantype " +
               " when 1 then 'Top Urgent' when 2 then 'Biasa' when 3 then 'SesuaiSchedule' end Prioritas," +
               " case when B.uomid>0 then (select uomcode from uom where id=B.uomid)end satuan, convert(varchar,A.createdtime,103) as createdtime," +
               " case when B.ItemTypeID=3 Then B.Keterangan1 else B.Keterangan end as Keterangan,(select stock from Inventory where B.ItemID=Inventory.id)Stock," +
               " case when (select stock from Inventory where B.ItemID=Inventory.id)=1 then 'Stock' else 'Non Stock' end Ket2 " +
               " from SPP as A, SPPDetail as B, Inventory as I where A.ID=B.SPPID and B.ItemID=I.ID and " +
               "A.Status>-1 and B.Status>-1  and B.Quantity-B.QtyPO > 0 and A.approval>=3 and I.Stock=0 " +
               "  and convert(varchar,A.ApproveDate3,112) >='" + drTgl + "' and convert(varchar,A.ApproveDate3,112)<='" + sdTgl + "' " + strGroupID + " and B.GroupId in(8,9,12) order by A.NoSPP";
        }
        public string ViewOutstandingPO(string drTgl, string sdTgl, int groupID)
        {
            string strGroupID = string.Empty;
            if (groupID > 0)
                strGroupID = " and GroupId =" + groupID;
            else
                strGroupID = " ";
            #region dinonaktifkan
            //return "select A.NoPO,Convert(varchar,A.CreatedTime,103) as TglPO,A.SupplierID,E.SupplierName,B.DocumentNo, " +
            //"C.ReceiptNo,Convert(varchar,C.ReceiptDate,103) as ReceiptDate,B.GroupID,B.Qty as QtyPO,D.Quantity as QtyTerima, " +
            //"B.Qty-D.Quantity as QtySisa,case B.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=B.ItemID) " + 
            //"when 2 then (select ItemName from Asset where Asset.ID=B.ItemID) " + 
            //"when 3 then (select ItemName from Biaya where Biaya.ID=B.ItemID) " + 
            //"else '' end ItemName, UPPER(G.UserName) as NamaHead " +
            //"from POPurchn as A, POPurchnDetail as B, Receipt as C, ReceiptDetail as D,Supppurch as E,SPP as F, Users as G " +
            //"where A.ID=B.POID and A.Status>-1 and B.Status>-1 and A.ID=C.POID and B.ID=D.PODetailID and " +
            //"C.ID=D.ReceiptID and C.Status>-1 and  A.SupplierID=E.ID and F.ID = B.SPPID and F.HeadID = G.ID " +
            //"and D.RowStatus>-1 and B.Qty-D.Quantity> 0  and convert(varchar,A.CreatedTime,112) >='" + drTgl + "' " +
            //"and convert(varchar,A.CreatedTime,112) <='" + sdTgl + "' " + strGroupID + " order by A.NoPO";

            //return  "select ID,nopo,tglPO,SupplierID,SupplierName,DocumentNo,ReceiptNo,ReceiptDate,GroupID, " +
            //"QtyPO,QtyPO-QtyPOCrnt as QtyPOCrnt ,QtyTerima,SumQtyTerima,QtyPO-QtyPOCrnt-QtyTerima as QtySisa,rtrim(ItemName) as ItemName,NamaHead,DlvDate from ( " +
            //"select d.ID , A.NoPO,Convert(varchar,A.popurchndate,103) as TglPO,A.SupplierID,E.SupplierName,B.DocumentNo,  " +
            //"C.ReceiptNo,Convert(varchar,C.ReceiptDate,103) as ReceiptDate,B.GroupID, " +
            //"case when B.ItemID>0 then (select ISNULL(SUM(quantity),0) from ReceiptDetail C " +
            //"where C.PONo=A.NoPO and C.ItemID=B.ItemID ) end SumQtyTerima, " +
            //"case when d.ID >0 then (select isnull(SUM(quantity),0) from ReceiptDetail  " +
            //"where itemid = B.ItemID and POID in(select ID from POPurchn where NoPO=A.NoPO) and ID<d.ID) end QtyPOCrnt, " +
            //"B.Qty as QtyPO,D.Quantity as QtyTerima, B.Qty-D.Quantity as QtySisa, " +
            //"case B.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=B.ItemID)  " +
            //"when 2 then (select ItemName from Asset where Asset.ID=B.ItemID)  " +
            //"when 3 then (select ItemName from Biaya where Biaya.ID=B.ItemID)  " +
            //"else '' end ItemName,  " +
            //"UPPER(G.UserName) as NamaHead, convert(varchar,B.DlvDate,106) as DlvDate " +
            //"from POPurchn as A, POPurchnDetail as B, Receipt as C, ReceiptDetail as D,Supppurch as E,SPP as F, Users as G  " +
            //"where A.ID=B.POID and A.Status>-1 and B.Status>-1 and A.ID=C.POID and B.ID=D.PODetailID and C.ID=D.ReceiptID and  " +
            //"C.Status>-1 and  A.SupplierID=E.ID and F.ID = B.SPPID and F.HeadID = G.ID and D.RowStatus>-1 and B.Qty-D.Quantity> 0  " +
            //"and convert(varchar,A.CreatedTime,112) >='" + drTgl + "' and convert(varchar,A.popurchndate,112) <='" + sdTgl + "' " + strGroupID + " ) as OutStanding  where qtyPO>SumQtyTerima " + 
            //"union " + 
            //"select ID,nopo,tglPO,SupplierID,SupplierName,DocumentNo,'-' as ReceiptNo,ReceiptDate,GroupID, " +
            //"QtyPO,QtyPO-QtyPOCrnt as QtyPOCrnt ,0 as QtyTerima,0 as SumQtyTerima, QtyPO as QtySisa,rtrim(ItemName) as ItemName,NamaHead,DlvDate from  " +
            //"(select B.ID , A.NoPO,Convert(varchar,A.popurchndate,103) as TglPO,A.SupplierID,E.SupplierName,B.DocumentNo,  " + 
            //" '-' as ReceiptNo,'-' as ReceiptDate,B.GroupID,0 as QtyPOCrnt, " + 
            //"B.Qty as QtyPO,0 as QtyTerima, 0 as QtySisa, " + 
            //"case B.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=B.ItemID)  " + 
            //"when 2 then (select ItemName from Asset where Asset.ID=B.ItemID)  " + 
            //"when 3 then (select ItemName from Biaya where Biaya.ID=B.ItemID)  " + 
            //"else '' end ItemName,  " +
            //"UPPER(G.UserName) as NamaHead,convert(varchar,B.DlvDate,106)as DlvDate " + 
            //"from POPurchn as A, POPurchnDetail as B, Supppurch as E,SPP as F, Users as G  " + 
            //"where A.ID=B.POID and A.Status>-1 and B.Status>-1 and   A.SupplierID=E.ID and F.ID = B.SPPID  " +
            //"and F.HeadID = G.ID and convert(varchar,A.popurchndate,112) >='" + drTgl + "' and convert(varchar,A.CreatedTime,112) <='" + sdTgl + "' " + strGroupID +
            //") as OutStanding2 left join (select receiptdetail.podetailid from receiptdetail inner join Receipt on  " +
            //"Receipt.pono=Receiptdetail.pono  and Receipt.ID=Receiptdetail.receiptID where convert(varchar,Receipt.receiptdate,112) >='" + drTgl + "'  " +
            //") as NotReceipt  on OutStanding2.ID =NotReceipt.PODetailID where ISNULL(NotReceipt.PODetailID,0)=0 order by NoPO,ID";

            //return "select ID,nopo,tglPO,SupplierID,SupplierName,DocumentNo,ReceiptNo,ReceiptDate,GroupID, QtyPO,QtyPO-QtyPOCrnt as  " +
            //"QtyPOCrnt ,QtyTerima,SumQtyTerima,QtyPO-QtyPOCrnt-QtyTerima as QtySisa,rtrim(ItemName) as ItemName,NamaHead,DlvDate  " +
            //"from ( select D.ID , A.NoPO,Convert(varchar,A.popurchndate,103) as TglPO,A.SupplierID,E.SupplierName,B.DocumentNo,   " +
            //"C.ReceiptNo,Convert(varchar,C.ReceiptDate,103) as ReceiptDate,B.GroupID,  " +
            //"case when B.ItemID>0 then (select ISNULL(SUM(quantity),0) from ReceiptDetail C  " +
            //"where C.PONo=A.NoPO and C.ItemID=B.ItemID ) end SumQtyTerima,  " +
            //"case when D.ID >0 then (select isnull(SUM(quantity),0) from ReceiptDetail  " +
            //"where  POID =A.ID  and ID<D.ID) end QtyPOCrnt,  " +
            //"B.Qty as QtyPO,D.Quantity as QtyTerima, B.Qty-D.Quantity as QtySisa,  " +
            //"case B.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=B.ItemID)  " +
            //" when 2 then (select ItemName from Asset where Asset.ID=B.ItemID)   " +
            //" when 3 then (select ItemName from Biaya where Biaya.ID=B.ItemID)  else '' end ItemName,   " +
            //"UPPER(G.UserName) as NamaHead, convert(varchar,B.DlvDate,106) as DlvDate  " +
            //"from POPurchn as A, POPurchnDetail as B, Receipt as C, ReceiptDetail as D,Supppurch as E,SPP as F, Users as G   " +
            //"where A.ID=B.POID and A.Status>-1 and B.Status>-1 and A.ID=C.POID and B.ID=D.PODetailID and C.ID=D.ReceiptID and   " +
            //"C.Status>-1 and  A.SupplierID=E.ID and F.ID = B.SPPID and  F.HeadID = G.ID and D.RowStatus>-1 and B.Qty-D.Quantity> 0   " +
            //"and convert(varchar,A.CreatedTime,112) >='" + drTgl + "'  and convert(varchar,A.popurchndate,112) <='" + sdTgl + "' " + strGroupID + "  )  " +
            //"as OutStanding  where qtyPO>SumQtyTerima  " +
            //"  union  " +
            //"select ID,nopo,tglPO,SupplierID,SupplierName,DocumentNo,'-' as ReceiptNo,ReceiptDate,GroupID, QtyPO,QtyPO-QtyPOCrnt  " +
            //"as QtyPOCrnt ,0 as QtyTerima,0 as SumQtyTerima, QtyPO as QtySisa,rtrim(ItemName) as ItemName,NamaHead,DlvDate from   " +
            //"(select B.ID , A.NoPO,Convert(varchar,A.popurchndate,103) as TglPO,A.SupplierID,E.SupplierName,B.DocumentNo,   '-'  " +
            //"as ReceiptNo,'-' as ReceiptDate,B.GroupID,0 as QtyPOCrnt, B.Qty as QtyPO,0 as QtyTerima, 0 as QtySisa,  " +
            //"case B.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=B.ItemID)   " +
            //"when 2 then (select ItemName from Asset where Asset.ID=B.ItemID)   " +
            //"when 3 then (select ItemName from Biaya where Biaya.ID=B.ItemID)   " +
            //"else '' end ItemName,   " +
            //"UPPER(G.UserName) as NamaHead,convert(varchar,B.DlvDate,106)as DlvDate from POPurchn as A,  " +
            //"POPurchnDetail as B, Supppurch as E,SPP as F, Users as G  where A.ID=B.POID and A.Status>-1 and B.Status>-1 and    " +
            //"A.SupplierID=E.ID and F.ID = B.SPPID  and F.HeadID = G.ID and convert(varchar,A.popurchndate,112) >='" + drTgl + "'  " +
            //"and convert(varchar,A.CreatedTime,112) <='" + sdTgl + "' " + strGroupID + "  and B.ID in " +
            //"(select y.ID from POPurchn x, POPurchnDetail y where x.ID =y.POID and  convert(varchar,x.popurchndate,112) >='" + drTgl + "'  " +
            //"and convert(varchar,x.popurchndate,112) <='" + sdTgl + "' " + strGroupID +
            //" except select y.PODetailID  from receipt x, receiptDetail y where x.ID =y.receiptID and  convert(varchar,x.receiptdate,112) >='" + drTgl + "')) as OutStanding2  order by NoPO,ID";
            #endregion
            string strSQL = "select ID, NoPO, Convert(varchar,popurchndate,103) as tglPO,groupid, SupplierName, NoSPP as documentno, ItemCode," +
            "ItemName,Qty as qtyPO,Quantity as qtyterima,Qty-qtyRcrnt as qtyPOcrnt, sumterima,Convert(varchar,DlvDate,103) as DlvDate, " +
            "case when headID>0 then (select username from users where id=headID)end namahead, ReceiptNo,  " +
            "Convert(varchar,ReceiptDate,103) as ReceiptDate from ( " +
            "SELECT pod.ID, po.NoPO, po.POPurchnDate, pod.Qty, pod.DlvDate,pod.documentno as NOSPP, pod.groupid," +
            "case when pod.sppid>0 then (select HeadID from SPP where ID=pod.sppid)end headID, " +
            "case when pod.ID>0 then(select isnull(SUM(quantity),0) from ReceiptDetail where PODetailID=pod.ID and rowStatus>=0 ) end sumterima, " +
            "case pod.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=pod.ItemID) " +
            "when 2 then (select ItemName from Asset where Asset.ID=pod.ItemID) " +
            "when 3 then " + ItemSPPBiayaNew("pod") + "  else '' end ItemName, " +
            "case pod.ItemTypeID when 1 then (select ItemCode from Inventory where Inventory.ID=pod.ItemID) " +
            "when 2 then (select ItemCode from Asset where Asset.ID=pod.ItemID) " +
            "when 3 then (select ItemCode from Biaya where Biaya.ID=pod.ItemID)  else '' end ItemCode, " +
            "case when po.SupplierID>0 then (select SupplierName from SuppPurch where ID=po.SupplierID) end SupplierName " +

            "FROM POPurchn AS po INNER JOIN POPurchnDetail AS pod ON po.ID = pod.POID and pod.status>=0 " +
            "where  convert(varchar,pod.dlvdate,112) >='" + drTgl + "'  and convert(varchar,pod.dlvdate,112) <='" + sdTgl +
            "' and po.Status>=0 and pod.Status>=0) as A1 left join  " +
            "(SELECT RD.PODetailID,  R.ReceiptNo, R.ReceiptDate, RD.Quantity, " +
            "case when RD.ID>0 then (select isnull(SUM(quantity),0) from ReceiptDetail,Receipt  where ReceiptDetail.ReceiptID=Receipt.ID and " +
            " Receipt.ReceiptNo<R.ReceiptNo and  PODetailID =RD.PODetailID and RowStatus>=0) end qtyRcrnt " +
            "FROM   Receipt AS R INNER JOIN ReceiptDetail AS RD ON R.ID = RD.ReceiptID where R.Status>=0 and RD.RowStatus>=0) A2  " +
            "on A1.ID =A2.PODetailID where Qty>sumterima  " + strGroupID + " order by ID  ";
            return strSQL;
        }
        public string ViewOutstandingPO2(string drTgl, string sdTgl)
        {          
            string strSQL = "select ID, NoPO, Convert(varchar,popurchndate,103) as tglPO,groupid, SupplierName, NoSPP as documentno, ItemCode," +
            "ItemName,Qty as qtyPO,Quantity as qtyterima,Qty-qtyRcrnt as qtyPOcrnt, sumterima,Convert(varchar,DlvDate,103) as DlvDate, " +
            "case when headID>0 then (select username from users where id=headID)end namahead, ReceiptNo,  " +
            "Convert(varchar,ReceiptDate,103) as ReceiptDate from ( " +
            "SELECT pod.ID, po.NoPO, po.POPurchnDate, pod.Qty, pod.DlvDate,pod.documentno as NOSPP, pod.groupid," +
            "case when pod.sppid>0 then (select HeadID from SPP where ID=pod.sppid)end headID, " +
            "case when pod.ID>0 then(select isnull(SUM(quantity),0) from ReceiptDetail where PODetailID=pod.ID and rowStatus>=0 ) end sumterima, " +
            "case pod.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=pod.ItemID) " +
            "when 2 then (select ItemName from Asset where Asset.ID=pod.ItemID) " +
            "when 3 then " + ItemSPPBiayaNew("pod") + "  else '' end ItemName, " +
            "case pod.ItemTypeID when 1 then (select ItemCode from Inventory where Inventory.ID=pod.ItemID) " +
            "when 2 then (select ItemCode from Asset where Asset.ID=pod.ItemID) " +
            "when 3 then (select ItemCode from Biaya where Biaya.ID=pod.ItemID)  else '' end ItemCode, " +
            "case when po.SupplierID>0 then (select SupplierName from SuppPurch where ID=po.SupplierID) end SupplierName " +

            "FROM POPurchn AS po INNER JOIN POPurchnDetail AS pod ON po.ID = pod.POID and pod.status>=0 " +
            "INNER JOIN Inventory as I on pod.ItemID=I.ID " +
            "where I.Stock=1 and convert(varchar,pod.dlvdate,112) >='" + drTgl + "'  and convert(varchar,pod.dlvdate,112) <='" + sdTgl +
            "' and po.Status>=0 and pod.Status>=0) as A1 left join  " +
            "(SELECT RD.PODetailID,  R.ReceiptNo, R.ReceiptDate, RD.Quantity, " +
            "case when RD.ID>0 then (select isnull(SUM(quantity),0) from ReceiptDetail,Receipt  where ReceiptDetail.ReceiptID=Receipt.ID and " +
            " Receipt.ReceiptNo<R.ReceiptNo and  PODetailID =RD.PODetailID and RowStatus>=0) end qtyRcrnt " +
            "FROM   Receipt AS R INNER JOIN ReceiptDetail AS RD ON R.ID = RD.ReceiptID where R.Status>=0 and RD.RowStatus>=0) A2  " +
            "on A1.ID =A2.PODetailID where Qty>sumterima and GroupId in (8,9,13) order by ID  ";
            return strSQL;
        }
        public string ViewOutstandingPO3(string drTgl, string sdTgl)
        {
            string strSQL = "select ID, NoPO, Convert(varchar,popurchndate,103) as tglPO,groupid, SupplierName, NoSPP as documentno, ItemCode," +
            "ItemName,Qty as qtyPO,Quantity as qtyterima,Qty-qtyRcrnt as qtyPOcrnt, sumterima,Convert(varchar,DlvDate,103) as DlvDate, " +
            "case when headID>0 then (select username from users where id=headID)end namahead, ReceiptNo,  " +
            "Convert(varchar,ReceiptDate,103) as ReceiptDate from ( " +
            "SELECT pod.ID, po.NoPO, po.POPurchnDate, pod.Qty, pod.DlvDate,pod.documentno as NOSPP, pod.groupid," +
            "case when pod.sppid>0 then (select HeadID from SPP where ID=pod.sppid)end headID, " +
            "case when pod.ID>0 then(select isnull(SUM(quantity),0) from ReceiptDetail where PODetailID=pod.ID and rowStatus>=0 ) end sumterima, " +
            "case pod.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=pod.ItemID) " +
            "when 2 then (select ItemName from Asset where Asset.ID=pod.ItemID) " +
            "when 3 then " + ItemSPPBiayaNew("pod") + "  else '' end ItemName, " +
            "case pod.ItemTypeID when 1 then (select ItemCode from Inventory where Inventory.ID=pod.ItemID) " +
            "when 2 then (select ItemCode from Asset where Asset.ID=pod.ItemID) " +
            "when 3 then (select ItemCode from Biaya where Biaya.ID=pod.ItemID)  else '' end ItemCode, " +
            "case when po.SupplierID>0 then (select SupplierName from SuppPurch where ID=po.SupplierID) end SupplierName " +

            "FROM POPurchn AS po INNER JOIN POPurchnDetail AS pod ON po.ID = pod.POID and pod.status>=0 " +
            "INNER JOIN Inventory as I on pod.ItemID=I.ID " +
            "where I.Stock=0 and convert(varchar,pod.dlvdate,112) >='" + drTgl + "'  and convert(varchar,pod.dlvdate,112) <='" + sdTgl +
            "' and po.Status>=0 and pod.Status>=0) as A1 left join  " +
            "(SELECT RD.PODetailID,  R.ReceiptNo, R.ReceiptDate, RD.Quantity, " +
            "case when RD.ID>0 then (select isnull(SUM(quantity),0) from ReceiptDetail,Receipt  where ReceiptDetail.ReceiptID=Receipt.ID and " +
            " Receipt.ReceiptNo<R.ReceiptNo and  PODetailID =RD.PODetailID and RowStatus>=0) end qtyRcrnt " +
            "FROM   Receipt AS R INNER JOIN ReceiptDetail AS RD ON R.ID = RD.ReceiptID where R.Status>=0 and RD.RowStatus>=0) A2  " +
            "on A1.ID =A2.PODetailID where Qty>sumterima and GroupId in (8,9,13) order by ID  ";
            return strSQL;
        }
        public string ViewOutstandingPO1(string drTgl, string sdTgl, string groupID)
        {
            string strGroupID = string.Empty;
            if (groupID == "0")
                strGroupID = " ";
            else
                strGroupID = " and NoPO like '%" + groupID + "%'";
            #region dinonaktifkan
            //return "select ID,nopo,tglPO,SupplierID,SupplierName,DocumentNo,ReceiptNo,ReceiptDate,GroupID, QtyPO,QtyPO-QtyPOCrnt as  " +
            //"QtyPOCrnt ,QtyTerima,SumQtyTerima,QtyPO-QtyPOCrnt-QtyTerima as QtySisa,rtrim(ItemName) as ItemName,NamaHead,DlvDate  " +
            //"from ( select D.ID , A.NoPO,Convert(varchar,A.popurchndate,103) as TglPO,A.SupplierID,E.SupplierName,B.DocumentNo,   " +
            //"C.ReceiptNo,Convert(varchar,C.ReceiptDate,103) as ReceiptDate,B.GroupID,  " +
            //"case when B.ItemID>0 then (select ISNULL(SUM(quantity),0) from ReceiptDetail C  " +
            //"where C.PONo=A.NoPO and C.ItemID=B.ItemID ) end SumQtyTerima,  " +
            //"case when D.ID >0 then (select isnull(SUM(quantity),0) from ReceiptDetail  " +
            //"where  POID =A.ID  and ID<D.ID) end QtyPOCrnt,  " +
            //"B.Qty as QtyPO,D.Quantity as QtyTerima, B.Qty-D.Quantity as QtySisa,  " +
            //"case B.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=B.ItemID)  " +
            //" when 2 then (select ItemName from Asset where Asset.ID=B.ItemID)   " +
            //" when 3 then (select ItemName from Biaya where Biaya.ID=B.ItemID)  else '' end ItemName,   " +
            //"UPPER(G.UserName) as NamaHead, convert(varchar,B.DlvDate,106) as DlvDate  " +
            //"from POPurchn as A, POPurchnDetail as B, Receipt as C, ReceiptDetail as D,Supppurch as E,SPP as F, Users as G   " +
            //"where A.ID=B.POID and A.Status>-1 and B.Status>-1 and A.ID=C.POID and B.ID=D.PODetailID and C.ID=D.ReceiptID and   " +
            //"C.Status>-1 and  A.SupplierID=E.ID and F.ID = B.SPPID and  F.HeadID = G.ID and D.RowStatus>-1 and B.Qty-D.Quantity> 0   " +
            //"and convert(varchar,A.CreatedTime,112) >='" + drTgl + "'  and convert(varchar,A.popurchndate,112) <='" + sdTgl + "' " + strGroupID + "  )  " +
            //"as OutStanding  where qtyPO>SumQtyTerima  " +
            //"  union  " +
            //"select ID,nopo,tglPO,SupplierID,SupplierName,DocumentNo,'-' as ReceiptNo,ReceiptDate,GroupID, QtyPO,QtyPO-QtyPOCrnt  " +
            //"as QtyPOCrnt ,0 as QtyTerima,0 as SumQtyTerima, QtyPO as QtySisa,rtrim(ItemName) as ItemName,NamaHead,DlvDate from   " +
            //"(select B.ID , A.NoPO,Convert(varchar,A.popurchndate,103) as TglPO,A.SupplierID,E.SupplierName,B.DocumentNo,   '-'  " +
            //"as ReceiptNo,'-' as ReceiptDate,B.GroupID,0 as QtyPOCrnt, B.Qty as QtyPO,0 as QtyTerima, 0 as QtySisa,  " +
            //"case B.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=B.ItemID)   " +
            //"when 2 then (select ItemName from Asset where Asset.ID=B.ItemID)   " +
            //"when 3 then (select ItemName from Biaya where Biaya.ID=B.ItemID)   " +
            //"else '' end ItemName,   " +
            //"UPPER(G.UserName) as NamaHead,convert(varchar,B.DlvDate,106)as DlvDate from POPurchn as A,  " +
            //"POPurchnDetail as B, Supppurch as E,SPP as F, Users as G  where A.ID=B.POID and A.Status>-1 and B.Status>-1 and    " +
            //"A.SupplierID=E.ID and F.ID = B.SPPID  and F.HeadID = G.ID and convert(varchar,A.popurchndate,112) >='" + drTgl + "'  " +
            //"and convert(varchar,A.CreatedTime,112) <='" + sdTgl + "' " + strGroupID + "  and B.ID in " +
            //"(select y.ID from POPurchn x, POPurchnDetail y where x.ID =y.POID and  convert(varchar,x.popurchndate,112) >='" + drTgl + "'  " +
            //"and convert(varchar,x.popurchndate,112) <='" + sdTgl + "' " + strGroupID +
            //" except select y.PODetailID  from receipt x, receiptDetail y where x.ID =y.receiptID and  convert(varchar,x.receiptdate,112) >='" + drTgl + "')) as OutStanding2  order by NoPO,ID";
            #endregion
            string strSQL = "select ID, NoPO, Convert(varchar,popurchndate,103) as tglPO,groupid, SupplierName, NoSPP as documentno, ItemCode," +
             "ItemName,Qty as qtyPO,Quantity as qtyterima,Qty-qtyRcrnt as qtyPOcrnt, sumterima,Convert(varchar,DlvDate,103) as DlvDate, " +
             "case when headID>0 then (select username from users where id=headID)end namahead, ReceiptNo,  " +
             "Convert(varchar,ReceiptDate,103) as ReceiptDate from ( " +
             "SELECT    pod.ID, po.NoPO, po.POPurchnDate, pod.Qty, pod.DlvDate,pod.documentno as NOSPP, pod.groupid," +
             "case when pod.sppid>0 then (select HeadID from SPP where ID=pod.sppid)end headID, " +
             "case when pod.ID>0 then(select isnull(SUM(quantity),0) from ReceiptDetail where PODetailID=pod.ID and RowStatus>=0) end sumterima, " +
             "case pod.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=pod.ItemID)   " +
             "when 2 then (select ItemName from Asset where Asset.ID=pod.ItemID) " +
             "when 3 then " + ItemSPPBiayaNew("pod") + "  else '' end ItemName, " +
             "case pod.ItemTypeID when 1 then (select ItemCode from Inventory where Inventory.ID=pod.ItemID) " +
             "when 2 then (select ItemCode from Asset where Asset.ID=pod.ItemID) " +
             "when 3 then (select ItemCode from Biaya where Biaya.ID=pod.ItemID)  else '' end ItemCode, " +
             "case when po.SupplierID>0 then (select SupplierName from SuppPurch where ID=po.SupplierID) end SupplierName " +
             "FROM POPurchn AS po INNER JOIN POPurchnDetail AS pod ON po.ID = pod.POID  " +
             "where  convert(varchar,pod.dlvdate,112) >='" + drTgl + "'  and convert(varchar,pod.dlvdate,112) <='" + sdTgl +
             "' and po.Status>=0 and pod.Status>=0) as A1 left join  " +
             "(SELECT RD.PODetailID,  R.ReceiptNo, R.ReceiptDate, RD.Quantity, " +
            "case when RD.ID>0 then (select isnull(SUM(quantity),0) from ReceiptDetail,Receipt  where ReceiptDetail.ReceiptID=Receipt.ID and " +
            " Receipt.ReceiptNo<R.ReceiptNo and  PODetailID =RD.PODetailID and RowStatus>=0) end qtyRcrnt " +
            "FROM   Receipt AS R INNER JOIN ReceiptDetail AS RD ON R.ID = RD.ReceiptID where R.Status>=0 and RD.RowStatus>=0) A2 " +
             "on A1.ID =A2.PODetailID where Qty>sumterima  " + strGroupID + " order by ID  ";
            return strSQL;
        }
        public string ViewOutstandingPObySup(string drTgl, string sdTgl, string groupID)
        {
            string strGroupID = string.Empty;
            if (groupID == "0")
                strGroupID = " ";
            else
                strGroupID = " and SupplierName like '%" + groupID + "%'";
            #region dinonaktifkan
            //return "select ID,nopo,tglPO,SupplierID,SupplierName,DocumentNo,ReceiptNo,ReceiptDate,GroupID, QtyPO,QtyPO-QtyPOCrnt as  " +
            //"QtyPOCrnt ,QtyTerima,SumQtyTerima,QtyPO-QtyPOCrnt-QtyTerima as QtySisa,rtrim(ItemName) as ItemName,NamaHead,DlvDate  " +
            //"from ( select D.ID , A.NoPO,Convert(varchar,A.popurchndate,103) as TglPO,A.SupplierID,E.SupplierName,B.DocumentNo,   " +
            //"C.ReceiptNo,Convert(varchar,C.ReceiptDate,103) as ReceiptDate,B.GroupID,  " +
            //"case when B.ItemID>0 then (select ISNULL(SUM(quantity),0) from ReceiptDetail C  " +
            //"where C.PONo=A.NoPO and C.ItemID=B.ItemID ) end SumQtyTerima,  " +
            //"case when D.ID >0 then (select isnull(SUM(quantity),0) from ReceiptDetail  " +
            //"where  POID =A.ID  and ID<D.ID) end QtyPOCrnt,  " +
            //"B.Qty as QtyPO,D.Quantity as QtyTerima, B.Qty-D.Quantity as QtySisa,  " +
            //"case B.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=B.ItemID)  " +
            //" when 2 then (select ItemName from Asset where Asset.ID=B.ItemID)   " +
            //" when 3 then (select ItemName from Biaya where Biaya.ID=B.ItemID)  else '' end ItemName,   " +
            //"UPPER(G.UserName) as NamaHead, convert(varchar,B.DlvDate,106) as DlvDate  " +
            //"from POPurchn as A, POPurchnDetail as B, Receipt as C, ReceiptDetail as D,Supppurch as E,SPP as F, Users as G   " +
            //"where A.ID=B.POID and A.Status>-1 and B.Status>-1 and A.ID=C.POID and B.ID=D.PODetailID and C.ID=D.ReceiptID and   " +
            //"C.Status>-1 and  A.SupplierID=E.ID and F.ID = B.SPPID and  F.HeadID = G.ID and D.RowStatus>-1 and B.Qty-D.Quantity> 0   " +
            //"and convert(varchar,A.CreatedTime,112) >='" + drTgl + "'  and convert(varchar,A.popurchndate,112) <='" + sdTgl + "' " + strGroupID + "  )  " +
            //"as OutStanding  where qtyPO>SumQtyTerima  " +
            //"  union  " +
            //"select ID,nopo,tglPO,SupplierID,SupplierName,DocumentNo,'-' as ReceiptNo,ReceiptDate,GroupID, QtyPO,QtyPO-QtyPOCrnt  " +
            //"as QtyPOCrnt ,0 as QtyTerima,0 as SumQtyTerima, QtyPO as QtySisa,rtrim(ItemName) as ItemName,NamaHead,DlvDate from   " +
            //"(select B.ID , A.NoPO,Convert(varchar,A.popurchndate,103) as TglPO,A.SupplierID,E.SupplierName,B.DocumentNo,   '-'  " +
            //"as ReceiptNo,'-' as ReceiptDate,B.GroupID,0 as QtyPOCrnt, B.Qty as QtyPO,0 as QtyTerima, 0 as QtySisa,  " +
            //"case B.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=B.ItemID)   " +
            //"when 2 then (select ItemName from Asset where Asset.ID=B.ItemID)   " +
            //"when 3 then (select ItemName from Biaya where Biaya.ID=B.ItemID)   " +
            //"else '' end ItemName,   " +
            //"UPPER(G.UserName) as NamaHead,convert(varchar,B.DlvDate,106)as DlvDate from POPurchn as A,  " +
            //"POPurchnDetail as B, Supppurch as E,SPP as F, Users as G  where A.ID=B.POID and A.Status>-1 and B.Status>-1 and    " +
            //"A.SupplierID=E.ID and F.ID = B.SPPID  and F.HeadID = G.ID and convert(varchar,A.popurchndate,112) >='" + drTgl + "'  " +
            //"and convert(varchar,A.CreatedTime,112) <='" + sdTgl + "' " + strGroupID + "  and B.ID in " +
            //"(select y.ID from POPurchn x, POPurchnDetail y where x.ID =y.POID and  convert(varchar,x.popurchndate,112) >='" + drTgl + "'  " +
            //"and convert(varchar,x.popurchndate,112) <='" + sdTgl + "' " + strGroupID +
            //" except select y.PODetailID  from receipt x, receiptDetail y where x.ID =y.receiptID and  convert(varchar,x.receiptdate,112) >='" + drTgl + "')) as OutStanding2  order by NoPO,ID";
            #endregion
            #region cara lama
            //string strSQL="select ID, NoPO, Convert(varchar,popurchndate,103) as tglPO,groupid, SupplierName, NoSPP as documentno,ItemCode, " +
            //"ItemName,Qty as qtyPO,Quantity as qtyterima,Qty-qtyRcrnt as qtyPOcrnt, sumterima,Convert(varchar,DlvDate,103) as DlvDate, " +
            //"case when headID>0 then (select username from users where id=headID)end namahead, ReceiptNo,  " +
            //"Convert(varchar,ReceiptDate,103) as ReceiptDate from ( " +
            //"SELECT    pod.ID, po.NoPO, po.POPurchnDate, pod.Qty, pod.DlvDate,pod.documentno as NOSPP, pod.groupid," +
            //"case when pod.sppid>0 then (select HeadID from SPP where ID=pod.sppid)end headID, " +
            //"case when pod.ID>0 then(select isnull(SUM(quantity),0) from ReceiptDetail where PODetailID=pod.ID and RowStatus>=0) end sumterima, " +
            //"case pod.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=pod.ItemID)   " +
            //"when 2 then (select ItemName from Asset where Asset.ID=pod.ItemID) " +
            //"when 3 then "+ItemSPPBiayaNew("pod")+"  else '' end ItemName, " +
            //"case pod.ItemTypeID when 1 then (select ItemCode from Inventory where Inventory.ID=pod.ItemID) " +
            //"when 2 then (select ItemCode from Asset where Asset.ID=pod.ItemID) " +
            //"when 3 then (select ItemCode from Biaya where Biaya.ID=pod.ItemID)  else '' end ItemCode, " +
            //"case when po.SupplierID>0 then (select SupplierName from SuppPurch where ID=po.SupplierID) end SupplierName " +
            //"FROM POPurchn AS po INNER JOIN POPurchnDetail AS pod ON po.ID = pod.POID  " +
            //"where  convert(varchar,pod.dlvdate,112) >='" + drTgl + "'  and convert(varchar,pod.dlvdate,112) <='" + sdTgl +
            //"' and po.Status>=0 and pod.Status>=0) as A1 left join  " +
            //"(SELECT RD.PODetailID,  R.ReceiptNo, R.ReceiptDate, RD.Quantity, " +
            //"case when RD.ID>0 then (select isnull(SUM(quantity),0) from ReceiptDetail  where ID<RD.ID and PODetailID =RD.PODetailID and RD.RowStatus>=0) end qtyRcrnt " +
            //"FROM   Receipt AS R INNER JOIN ReceiptDetail AS RD ON R.ID = RD.ReceiptID where R.Status>=0 and RD.RowStatus>=0) A2  " +
            //"on A1.ID =A2.PODetailID where Qty>sumterima  " + strGroupID + "order by ID  ";
            //cara lama 2
            //            string strSQL = "select * from (select ID, NoPO, Convert(varchar,popurchndate,103) as tglPO,groupid, SupplierName, NoSPP as documentno, ItemCode," +
            //             "ItemName,Qty as qtyPO,Quantity as qtyterima,Qty-qtyRcrnt as qtyPOcrnt, sumterima,Convert(varchar,DlvDate,103) as DlvDate, " +
            //             "case when headID>0 then (select username from users where id=headID)end namahead, ReceiptNo,  " +
            //             "Convert(varchar,ReceiptDate,103) as ReceiptDate from ( " +
            //             "SELECT    pod.ID, po.NoPO, po.POPurchnDate, pod.Qty, pod.DlvDate,pod.documentno as NOSPP, pod.groupid," +
            //             "case when pod.sppid>0 then (select HeadID from SPP where ID=pod.sppid)end headID, " +
            //             "case when pod.ID>0 then(select isnull(SUM(quantity),0) from ReceiptDetail where PODetailID=pod.ID and RowStatus>=0) end sumterima, " +
            //             "case pod.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=pod.ItemID)   " +
            //             "when 2 then (select ItemName from Asset where Asset.ID=pod.ItemID) " +
            //             "when 3 then " + ItemSPPBiayaNew("pod") + "  else '' end ItemName, " +
            //             "case pod.ItemTypeID when 1 then (select ItemCode from Inventory where Inventory.ID=pod.ItemID) " +
            //             "when 2 then (select ItemCode from Asset where Asset.ID=pod.ItemID) " +
            //             "when 3 then (select ItemCode from Biaya where Biaya.ID=pod.ItemID)  else '' end ItemCode, " +
            //             "case when po.SupplierID>0 then (select SupplierName from SuppPurch where ID=po.SupplierID) end SupplierName " +
            //             "FROM POPurchn AS po INNER JOIN POPurchnDetail AS pod ON po.ID = pod.POID  " +
            //             "where  convert(varchar,pod.dlvdate,112) >='" + drTgl + "'  and convert(varchar,pod.dlvdate,112) <='" + sdTgl +
            //             "' and po.Status>=0 and pod.Status>=0) as A1 left join  " +
            //             "(SELECT RD.PODetailID,  R.ReceiptNo, R.ReceiptDate, RD.Quantity, " +
            //             "case when RD.ID>0 then (select isnull(SUM(quantity),0) from ReceiptDetail where ID<RD.ID and PODetailID =RD.PODetailID and RowStatus>=0) end qtyRcrnt " +
            //             "FROM   Receipt AS R INNER JOIN ReceiptDetail AS RD ON R.ID = RD.ReceiptID where R.Status>=0 and RD.RowStatus>=0) A2  " +
            //             "on A1.ID =A2.PODetailID where Qty>sumterima  ) as w  "+
            //             strGroupID + " order by w.ID";
            //=======
            #endregion
            string strSQL = "select ID, NoPO, Convert(varchar,popurchndate,103) as tglPO,groupid, SupplierName, NoSPP as documentno,ItemCode, " +
            "ItemName,Qty as qtyPO,Quantity as qtyterima,Qty-qtyRcrnt as qtyPOcrnt, sumterima,Convert(varchar,DlvDate,103) as DlvDate, " +
            "case when headID>0 then (select username from users where id=headID)end namahead, ReceiptNo,  " +
            "Convert(varchar,ReceiptDate,103) as ReceiptDate from ( " +
            "SELECT    pod.ID, po.NoPO, po.POPurchnDate, pod.Qty, pod.DlvDate,pod.documentno as NOSPP, pod.groupid," +
            "case when pod.sppid>0 then (select HeadID from SPP where ID=pod.sppid)end headID, " +
            "case when pod.ID>0 then(select isnull(SUM(quantity),0) from ReceiptDetail where PODetailID=pod.ID and RowStatus>=0) end sumterima, " +
            "case pod.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=pod.ItemID)   " +
            "when 2 then (select ItemName from Asset where Asset.ID=pod.ItemID) " +
            "when 3 then " + ItemSPPBiayaNew("pod") + "  else '' end ItemName, " +
            "case pod.ItemTypeID when 1 then (select ItemCode from Inventory where Inventory.ID=pod.ItemID) " +
            "when 2 then (select ItemCode from Asset where Asset.ID=pod.ItemID) " +
            "when 3 then (select ItemCode from Biaya where Biaya.ID=pod.ItemID)  else '' end ItemCode, " +
            "case when po.SupplierID>0 then (select SupplierName from SuppPurch where ID=po.SupplierID) end SupplierName " +
            "FROM POPurchn AS po INNER JOIN POPurchnDetail AS pod ON po.ID = pod.POID  " +
            "where  convert(varchar,pod.dlvdate,112) >='" + drTgl + "'  and convert(varchar,pod.dlvdate,112) <='" + sdTgl +
            "' and po.Status>=0 and pod.Status>=0) as A1 left join  " +
            "(SELECT RD.PODetailID,  R.ReceiptNo, R.ReceiptDate, RD.Quantity, " +
            "case when RD.ID>0 then (select isnull(SUM(quantity),0) from ReceiptDetail,Receipt  where ReceiptDetail.ReceiptID=Receipt.ID and " +
            " Receipt.ReceiptNo<R.ReceiptNo and  PODetailID =RD.PODetailID and RowStatus>=0) end qtyRcrnt " +
            "FROM   Receipt AS R INNER JOIN ReceiptDetail AS RD ON R.ID = RD.ReceiptID where R.Status>=0 and RD.RowStatus>=0) A2 " +
            "on A1.ID =A2.PODetailID where Qty>sumterima  " + strGroupID + "order by ID  ";
            return strSQL;
        }
        public string ViewOutstandingPOLS(string drTgl, string sdTgl, int groupID)
        {
            string strGroupID = string.Empty;
            if (groupID > 0)
                strGroupID = " and GroupId =" + groupID;
            else
                strGroupID = " ";

            string strSQL = "select ID, NoPO, Convert(varchar,popurchndate,103) as tglPO,groupid, SupplierName, NoSPP as documentno,ItemCode, " +
            "ItemName,Qty as qtyPO,0 as qtyterima,0 as qtyPOcrnt, sumterima,Convert(varchar,DlvDate,103) as DlvDate, " +
            "case when headID>0 then (select username from users where id=headID)end namahead,'' as ReceiptNo,  " +
            "'' as ReceiptDate from ( " +
            "SELECT pod.ID, po.NoPO, po.POPurchnDate, pod.Qty, pod.DlvDate,pod.documentno as NOSPP, pod.groupid," +
            "case when pod.sppid>0 then (select HeadID from SPP where ID=pod.sppid)end headID, " +
            "case when pod.ID>0 then(select isnull(SUM(quantity),0) from ReceiptDetail where PODetailID=pod.ID and rowStatus>=0 ) end sumterima, " +
            "case pod.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=pod.ItemID) " +
            "when 2 then (select ItemName from Asset where Asset.ID=pod.ItemID) " +
            "when 3 then " + ItemSPPBiayaNew("pod") + "  else '' end ItemName, " +
            "case pod.ItemTypeID when 1 then (select ItemCode from Inventory where Inventory.ID=pod.ItemID) " +
            "when 2 then (select ItemCode from Asset where Asset.ID=pod.ItemID) " +
            "when 3 then (select ItemCode from Biaya where Biaya.ID=pod.ItemID)  else '' end ItemCode, " +
            "case when po.SupplierID>0 then (select SupplierName from SuppPurch where ID=po.SupplierID) end SupplierName " +
            "FROM POPurchn AS po INNER JOIN POPurchnDetail AS pod ON po.ID = pod.POID and pod.status>=0 " +
            "where convert(varchar,pod.dlvdate,112) >='" + drTgl + "'  and convert(varchar,pod.dlvdate,112) <='" + sdTgl +
            "' and po.Status>=0 and pod.Status>=0) as A1 " +
            " where Qty>sumterima  " + strGroupID + "order by ID  ";
            return strSQL;
        }
        public string ViewOutstandingPO2LS(string drTgl, string sdTgl)
        {
            string strSQL = "select ID, NoPO, Convert(varchar,popurchndate,103) as tglPO,groupid, SupplierName, NoSPP as documentno,ItemCode, " +
            "ItemName,Qty as qtyPO,0 as qtyterima,0 as qtyPOcrnt, sumterima,Convert(varchar,DlvDate,103) as DlvDate, " +
            "case when headID>0 then (select username from users where id=headID)end namahead,'' as ReceiptNo,  " +
            "'' as ReceiptDate from ( " +
            "SELECT pod.ID, po.NoPO, po.POPurchnDate, pod.Qty, pod.DlvDate,pod.documentno as NOSPP, pod.groupid," +
            "case when pod.sppid>0 then (select HeadID from SPP where ID=pod.sppid)end headID, " +
            "case when pod.ID>0 then(select isnull(SUM(quantity),0) from ReceiptDetail where PODetailID=pod.ID and rowStatus>=0 ) end sumterima, " +
            "case pod.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=pod.ItemID) " +
            "when 2 then (select ItemName from Asset where Asset.ID=pod.ItemID) " +
            "when 3 then " + ItemSPPBiayaNew("pod") + "  else '' end ItemName, " +
            "case pod.ItemTypeID when 1 then (select ItemCode from Inventory where Inventory.ID=pod.ItemID) " +
            "when 2 then (select ItemCode from Asset where Asset.ID=pod.ItemID) " +
            "when 3 then (select ItemCode from Biaya where Biaya.ID=pod.ItemID)  else '' end ItemCode, " +
            "case when po.SupplierID>0 then (select SupplierName from SuppPurch where ID=po.SupplierID) end SupplierName " +
            "FROM POPurchn AS po INNER JOIN POPurchnDetail AS pod ON po.ID = pod.POID and pod.status>=0 " +
            "INNER JOIN Inventory AS I ON pod.ItemID=I.ID " +
            "where I.Stock=1 and convert(varchar,pod.dlvdate,112) >='" + drTgl + "'  and convert(varchar,pod.dlvdate,112) <='" + sdTgl +
            "' and po.Status>=0 and pod.Status>=0) as A1 " +
            " where Qty>sumterima and GroupId in (8,9,12) order by ID  ";
            return strSQL;
        }
        public string ViewOutstandingPO3LS(string drTgl, string sdTgl)
        {
            string strSQL = "select ID, NoPO, Convert(varchar,popurchndate,103) as tglPO,groupid, SupplierName, NoSPP as documentno,ItemCode, " +
            "ItemName,Qty as qtyPO,0 as qtyterima,0 as qtyPOcrnt, sumterima,Convert(varchar,DlvDate,103) as DlvDate, " +
            "case when headID>0 then (select username from users where id=headID)end namahead,'' as ReceiptNo,  " +
            "'' as ReceiptDate from ( " +
            "SELECT pod.ID, po.NoPO, po.POPurchnDate, pod.Qty, pod.DlvDate,pod.documentno as NOSPP, pod.groupid," +
            "case when pod.sppid>0 then (select HeadID from SPP where ID=pod.sppid)end headID, " +
            "case when pod.ID>0 then(select isnull(SUM(quantity),0) from ReceiptDetail where PODetailID=pod.ID and rowStatus>=0 ) end sumterima, " +
            "case pod.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=pod.ItemID) " +
            "when 2 then (select ItemName from Asset where Asset.ID=pod.ItemID) " +
            "when 3 then " + ItemSPPBiayaNew("pod") + "  else '' end ItemName, " +
            "case pod.ItemTypeID when 1 then (select ItemCode from Inventory where Inventory.ID=pod.ItemID) " +
            "when 2 then (select ItemCode from Asset where Asset.ID=pod.ItemID) " +
            "when 3 then (select ItemCode from Biaya where Biaya.ID=pod.ItemID)  else '' end ItemCode, " +
            "case when po.SupplierID>0 then (select SupplierName from SuppPurch where ID=po.SupplierID) end SupplierName " +
            "FROM POPurchn AS po INNER JOIN POPurchnDetail AS pod ON po.ID = pod.POID and pod.status>=0 " +
            "INNER JOIN Inventory AS I ON pod.ItemID=I.ID " +
            "where I.Stock=0 and convert(varchar,pod.dlvdate,112) >='" + drTgl + "'  and convert(varchar,pod.dlvdate,112) <='" + sdTgl +
            "' and po.Status>=0 and pod.Status>=0) as A1 " +
            " where Qty>sumterima and GroupId in (8,9,12) order by ID  ";
            return strSQL;
        }
        public string ViewOutstandingPO1LS(string drTgl, string sdTgl, string groupID)
        {
            string strGroupID = string.Empty;
            if (groupID == "0")
                strGroupID = " ";
            else
                strGroupID = " and NoPO like '%" + groupID + "%'";

            string strSQL = "select ID, NoPO, Convert(varchar,popurchndate,103) as tglPO,groupid, SupplierName, NoSPP as documentno,ItemCode, " +
             "ItemName,Qty as qtyPO,0 as qtyterima,0 as qtyPOcrnt, sumterima,Convert(varchar,DlvDate,103) as DlvDate, " +
             "case when headID>0 then (select username from users where id=headID)end namahead, '' as ReceiptNo,  " +
            "'' as ReceiptDate from ( " +
             "SELECT    pod.ID, po.NoPO, po.POPurchnDate, pod.Qty, pod.DlvDate,pod.documentno as NOSPP, pod.groupid," +
             "case when pod.sppid>0 then (select HeadID from SPP where ID=pod.sppid)end headID, " +
             "case when pod.ID>0 then(select isnull(SUM(quantity),0) from ReceiptDetail where PODetailID=pod.ID and RowStatus>=0) end sumterima, " +
             "case pod.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=pod.ItemID)   " +
             "when 2 then (select ItemName from Asset where Asset.ID=pod.ItemID) " +
             "when 3 then " + ItemSPPBiayaNew("pod") + "  else '' end ItemName, " +
             "case pod.ItemTypeID when 1 then (select ItemCode from Inventory where Inventory.ID=pod.ItemID) " +
            "when 2 then (select ItemCode from Asset where Asset.ID=pod.ItemID) " +
            "when 3 then (select ItemCode from Biaya where Biaya.ID=pod.ItemID)  else '' end ItemCode, " +
             "case when po.SupplierID>0 then (select SupplierName from SuppPurch where ID=po.SupplierID) end SupplierName " +
             "FROM POPurchn AS po INNER JOIN POPurchnDetail AS pod ON po.ID = pod.POID  " +
             "where  convert(varchar,pod.dlvdate,112) >='" + drTgl + "'  and convert(varchar,pod.dlvdate,112) <='" + sdTgl +
             "' and po.Status>=0 and pod.Status>=0) as A1 " +
            "where Qty>sumterima  " + strGroupID + "order by ID  ";
            return strSQL;
        }
        public string ViewOutstandingPObySupLS(string drTgl, string sdTgl, string groupID)
        {
            string strGroupID = string.Empty;
            if (groupID == "0")
                strGroupID = " ";
            else
                strGroupID = " and SupplierName like '%" + groupID + "%'";

            string strSQL = "select ID, NoPO, Convert(varchar,popurchndate,103) as tglPO,groupid, SupplierName, NoSPP as documentno,ItemCode, " +
           "ItemName,Qty as qtyPO,0 as qtyterima,0 as qtyPOcrnt, sumterima,Convert(varchar,DlvDate,103) as DlvDate, " +
           "case when headID>0 then (select username from users where id=headID)end namahead,'' as ReceiptNo,  " +
           "'' as ReceiptDate from ( " +
           "SELECT pod.ID, po.NoPO, po.POPurchnDate, pod.Qty, pod.DlvDate,pod.documentno as NOSPP, pod.groupid," +
           "case when pod.sppid>0 then (select HeadID from SPP where ID=pod.sppid)end headID, " +
           "case when pod.ID>0 then(select isnull(SUM(quantity),0) from ReceiptDetail where PODetailID=pod.ID and rowStatus>=0 ) end sumterima, " +
           "case pod.ItemTypeID when 1 then (select ItemName from Inventory where Inventory.ID=pod.ItemID) " +
           "when 2 then (select ItemName from Asset where Asset.ID=pod.ItemID) " +
           "when 3 then " + ItemSPPBiayaNew("pod") + "  else '' end ItemName, " +
           "case pod.ItemTypeID when 1 then (select ItemCode from Inventory where Inventory.ID=pod.ItemID) " +
            "when 2 then (select ItemCode from Asset where Asset.ID=pod.ItemID) " +
            "when 3 then (select ItemCode from Biaya where Biaya.ID=pod.ItemID)  else '' end ItemCode, " +
           "case when po.SupplierID>0 then (select SupplierName from SuppPurch where ID=po.SupplierID) end SupplierName " +
           "FROM POPurchn AS po INNER JOIN POPurchnDetail AS pod ON po.ID = pod.POID and pod.status>=0 " +
           "where  convert(varchar,pod.dlvdate,112) >='" + drTgl + "'  and convert(varchar,pod.dlvdate,112) <='" + sdTgl +
           "' and po.Status>=0 and pod.Status>=0) as A1 " +
           " where Qty>sumterima  " + strGroupID + "order by ID  ";
            return strSQL;
        }
        public string ViewPOPurchn(int id, int viewprice)
        {
            string strSQL = string.Empty;
            /**
             * Jika System Nilai Kurs diaktifkan
             * added on 04-07-2015
             */
            string inputKursAktif = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("InputNilaiKurs", "PO").ToString();
            string JmlPrice = (inputKursAktif == "Aktif") ? "CASE WHEN E.flag=0 and B.NilaiKurs>0 and B.Crc>1 THEN (A.Qty*(A.Price*B.NilaiKurs))ELSE(A.Qty*A.Price) END" : "(A.Qty*A.Price)";
            string Price = (inputKursAktif == "Aktif") ? " CASE WHEN E.flag=0 and B.NilaiKurs>0 and B.Crc>1 THEN (B.NilaiKurs*A.Price) ELSE A.Price END" : "A.Price";
            if (viewprice < 1)
                strSQL = "SET DATEFIRST 1;select A.ID,A.POID,B.NoPO,Convert(varchar,B.POPurchnDate,106) as CreatedTime,B.Termin,B.Delivery,C.NoSPP, " +
                   "UPPER(SUBSTRING(D.UOMCode,1,1)) + lower(SUBSTRING(D.UOMCode,2,LEN(D.UOMCode)-1)) as UOMCode," +
                   "E.SupplierName,E.UP,E.Telepon," +
                   "case when (select isnull(harga,0) from Inventory where ID=A.ItemID)=0 then(A.Qty * A.Price) else 0 end  Jumlah, " +
                   "E.Fax,A.SPPID,A.GroupID,A.ItemID,case when (select isnull(harga,0) from Inventory where ID=A.ItemID)=0 then A.Price else 0 end Price,A.Qty,A.ItemTypeID,A.UOMID,A.Status,A.NoUrut,A.SPPDetailID,A.DocumentNo, " +
                   "case A.ItemTypeID when 1  then (select UPPER(SUBSTRING(ItemName,1,1)) + lower(SUBSTRING(ItemName,2,LEN(ItemName)-1)) as ItemName  from Inventory where ID=A.ItemID and RowStatus > -1) " +
                   "when 2 then (select UPPER(SUBSTRING(ItemName,1,1)) + lower(SUBSTRING(ItemName,2,LEN(ItemName)-1)) as ItemName  from Asset where ID=A.ItemID and RowStatus > -1) " +
                   "else " + ItemPOBiayaNew("A") + " end ItemName, " +
                   "case A.ItemTypeID when 1  then (select ItemCode from Inventory where ID=A.ItemID and RowStatus > -1) " +
                   "when 2 then (select ItemCode from Asset where ID=A.ItemID and RowStatus > -1) " +
                   "else (select ItemCode from Biaya where ID=A.ItemID and RowStatus > -1) end ItemCode,convert(varchar,isnull(dlvdate,'1/1/1900'),103)  as dlvdate " +
                   ",Convert(varchar,(Select dbo.GetSchDeliveryOnPO(DlvDate)),103)SchOnPO,C.PermintaanType from POPurchnDetail as A,POPurchn as B,SPP as C,UOM as D,SuppPurch as E " +
                   "where A.status>-1 and A.POID = B.ID and A.SPPID = C.ID and A.UOMID = D.ID and B.SupplierID = E.ID and A.POID = " + id;
            else
                strSQL = "SET DATEFIRST 1; select A.ID,A.POID,B.NoPO,Convert(varchar,B.POPurchnDate,106) as CreatedTime,B.Termin,B.Delivery,C.NoSPP," +
                    "UPPER(SUBSTRING(D.UOMCode,1,1)) + lower(SUBSTRING(D.UOMCode,2,LEN(D.UOMCode)-1)) as UOMCode, " +
                    "E.SupplierName,E.UP,E.Telepon," +
                    JmlPrice + " as Jumlah, " +
                   "E.Fax,A.SPPID,A.GroupID,A.ItemID," + Price + " as Price,A.Qty,A.ItemTypeID,A.UOMID,A.Status,A.NoUrut,A.SPPDetailID,A.DocumentNo, " +
                   "case A.ItemTypeID when 1  then (select UPPER(SUBSTRING(ItemName,1,1)) + lower(SUBSTRING(ItemName,2,LEN(ItemName)-1)) as ItemName  from Inventory where ID=A.ItemID and RowStatus > -1) " +
                   "when 2 then (select UPPER(SUBSTRING(ItemName,1,1)) + lower(SUBSTRING(ItemName,2,LEN(ItemName)-1)) as ItemName  from Asset where ID=A.ItemID and RowStatus > -1) " +
                   "else " + ItemPOBiayaNew("A") + " end ItemName, " +
                   "case A.ItemTypeID when 1  then (select ItemCode from Inventory where ID=A.ItemID and RowStatus > -1) " +
                   "when 2 then (select ItemCode from Asset where ID=A.ItemID and RowStatus > -1) " +
                   "else (select ItemCode from Biaya where ID=A.ItemID and RowStatus > -1) end ItemCode,convert(varchar,isnull(dlvdate,'1/1/1900'),103)  as dlvdate " +
                   ",Convert(varchar,(Select dbo.GetSchDeliveryOnPO(DlvDate)),103)SchOnPO,C.PermintaanType from POPurchnDetail as A,POPurchn as B,SPP as C,UOM as D,SuppPurch as E " +
                   "where A.status>-1 and A.POID = B.ID and A.SPPID = C.ID and A.UOMID = D.ID and B.SupplierID = E.ID and A.POID = " + id;
            return strSQL;
        }
        public string ViewTawarReport(int id)
        {
            return "select CONVERT(varchar(11),TglTawar,113) as TglPenawaran,C.SupplierName as NamaSupplier,C.UP,C.Fax,C.Telepon as Telp, " +
                "A.NoTawar as NoPenawaran,DATENAME(month,A.TglTawar)+' '+DATENAME(YEAR,A.TglTawar) as BulanKirim,D.NoSPP, " +
                "case B.ItemTypeID " +
                "when 1 then(select ItemName from Inventory where ID = B.ItemID) " +
                "when 2 then(select ItemName from Asset where ID = B.ItemID) " +
                "when 3 then " + ItemSPPBiayaNew("B") + " end NamaBarang, " +
                "B.Qty,E.UOMCode as Sat " +
                "from Tawar as A,TawarDetail as B,SuppPurch as C,SPP as D,UOM as E " +
                "where A.ID = B.TawarID and A.SupplierID = C.ID and B.SPPID = D.ID and B.UOMID = E.ID and A.ID = " + id;
        }
        public string ViewRekapSPP(string tgl1, string tgl2)
        {
            #region
            //return "SELECT SPP.NoSPP, case SPP.Approval " +
            //        "when 0 then 'Open' when 1 then 'Head' when 2 then 'Manager' when 3 then 'Purchasing' end Approval, " +
            //        "CONVERT(varchar,SPP.ApproveDate1,103) as TglApprove, " +
            //        "Inventory.ItemCode, Inventory.ItemName, SPPDetail.Quantity, " + 
            //        "SPPDetail.Quantity - SPPDetail.QtyPO AS SISA, UOM.UOMCode," +
            //        "SPPDetail.Keterangan, CONVERT(varchar,SPP.CreatedTime,103) as Minta " +
            //        "FROM         SPP INNER JOIN SPPDetail ON SPP.ID = SPPDetail.SPPID INNER JOIN " +
            //        "Inventory ON SPPDetail.ItemID = Inventory.ID INNER JOIN " +
            //        "UOM ON SPPDetail.UOMID = UOM.ID " +
            //        "where convert(varchar,SPP.CreatedTime,112)>='" + tgl1 + "' and  convert(varchar,SPP.CreatedTime,112)<='" + 
            //            tgl2 + "' ORDER BY SPP.NoSPP";
            #endregion
            string strsql = "SELECT SPP.NoSPP, case SPP.Approval " +
                     "when 0 then 'Open' when 1 then 'Head' when 2 then 'Manager' when 3 then 'Plant Manager' when 4 then 'Purchasing' end Approval, " +
                     "CONVERT(varchar,SPP.ApproveDate1,103) as TglApprove,CONVERT(varchar,SPP.LastModifiedTime,113) as LastModified, " +
                     "case SPPDetail.ItemTypeID when 1  then (select ItemName from Inventory where ID=SPPDetail.ItemID and RowStatus > -1)  " +
                     "when 2 then (select ItemName from Asset where ID=SPPDetail.ItemID and RowStatus > -1) " +
                     "else (select ItemName from Biaya where ID=SPPDetail.ItemID and RowStatus > -1)+' - '+ SPPDetail.Keterangan end ItemName, " +
                     "case SPPDetail.ItemTypeID when 1  then (select ItemCode from Inventory where ID=SPPDetail.ItemID and RowStatus > -1) " +
                     "when 2 then (select ItemCode from Asset where ID=SPPDetail.ItemID and RowStatus > -1) " +
                     "else (select ItemCode from Biaya where ID=SPPDetail.ItemID and RowStatus > -1) end ItemCode, " +
                     "SPPDetail.Quantity, " +
                     "SPPDetail.Quantity - SPPDetail.QtyPO AS SISA, UOM.UOMCode, " +
                     "Case when SPPDetail.ItemTypeID<>3 Then SPPDetail.Keterangan ELSE isnull(SPPDetail.Keterangan1,'-') END as Keterangan, CONVERT(varchar,SPP.CreatedTime,103) as Minta  " +
                     "FROM SPP INNER JOIN SPPDetail ON SPP.ID = SPPDetail.SPPID and SPP.Status>-1 and SPPDetail.Status>-1 " +
                     "INNER JOIN UOM ON SPPDetail.UOMID = UOM.ID " +
                     "where convert(varchar,SPP.CreatedTime,112)>='" + tgl1 + "' and  convert(varchar,SPP.CreatedTime,112)<='" +
                     tgl2 + "' ORDER BY SPP.NoSPP";
            return strsql;
        }
        public string ViewRekapPO(string tgl1, string tgl2, int viewPrice)
        {
            #region
            //return "SELECT POPurchn.NoPO, SuppPurch.SupplierName,POPurchn.Cetak,POPurchn.Approval, POPurchn.PPN, MataUang.Nama, CASE POPurchnDetail.ItemTypeID WHEN 1 THEN " +
            //       "(SELECT ItemName FROM Inventory WHERE ID = POPurchnDetail.ItemID AND RowStatus > - 1) WHEN 2 THEN " +
            //       "(SELECT ItemName FROM Asset WHERE ID = POPurchnDetail.ItemID AND RowStatus > - 1) ELSE " +
            //       "(SELECT ItemName FROM Biaya WHERE ID = POPurchnDetail.ItemID AND RowStatus > - 1) END AS ItemName, SPP.NoSPP, POPurchnDetail.Price, POPurchnDetail.Qty,  " +
            //       "UOM.UOMCode, POPurchnDetail.Disc, POPurchnDetail.Price * POPurchnDetail.Qty AS TOTAL, POPurchn.POPurchnDate " +
            //       "FROM POPurchn INNER JOIN POPurchnDetail ON POPurchn.ID = POPurchnDetail.POID INNER JOIN " +
            //       "SPP ON POPurchnDetail.SPPID = SPP.ID INNER JOIN " +
            //       "UOM ON POPurchnDetail.UOMID = UOM.ID INNER JOIN " +
            //       "SuppPurch ON POPurchn.SupplierID = SuppPurch.ID INNER JOIN " +
            //       "MataUang ON POPurchn.Crc = MataUang.ID " +
            //        "where  POPurchn.status>-1 and convert(varchar,POPurchn.POPurchndate,112)>='" + tgl1 +
            //        "' and  convert(varchar,POPurchn.POPurchndate,112)<='" +
            //        tgl2 + "' order by POPurchn.NoPO";
            #endregion
            string strSQL = string.Empty;
            if (viewPrice == 0)
                strSQL = "SELECT POPurchn.NoPO, SuppPurch.SupplierName,POPurchn.Cetak,POPurchn.Approval, POPurchn.PPN,POPurchn.PPH, MataUang.Nama, CASE POPurchnDetail.ItemTypeID WHEN 1 THEN " +
              "(SELECT ItemName FROM Inventory WHERE ID = POPurchnDetail.ItemID AND RowStatus > - 1) WHEN 2 THEN " + "(SELECT ItemName FROM Asset WHERE ID = POPurchnDetail.ItemID AND RowStatus > - 1) ELSE " +
              ItemSPPBiayaNew("POPurchnDetail") + " END AS ItemName, SPP.NoSPP, POPurchnDetail.Price as Price2, POPurchnDetail.Qty,  " +
              "UOM.UOMCode, POPurchn.Disc, 0 Price,0 as Total, " +
              "POPurchnDetail.Price * POPurchnDetail.Qty AS TOT2, POPurchn.POPurchnDate, GROUPsPURCHN.groupdescription as groupdesc " +
              "FROM POPurchn INNER JOIN POPurchnDetail ON POPurchn.ID = POPurchnDetail.POID " +
              "INNER JOIN SPP ON POPurchnDetail.SPPID = SPP.ID INNER JOIN UOM ON POPurchnDetail.UOMID = UOM.ID " +
              "INNER JOIN SuppPurch ON POPurchn.SupplierID = SuppPurch.ID " +
              "INNER JOIN MataUang ON POPurchn.Crc = MataUang.ID INNER JOIN GROUPsPURCHN ON POPurchnDetail.groupid = GROUPsPURCHN.id where  POPurchn.status>-1 and " +
              "convert(varchar,POPurchn.POPurchndate,112)>='" + tgl1 + "' and  convert(varchar,POPurchn.POPurchndate,112)<='" + tgl2 + "' and POPurchnDetail.Status >-1 order by POPurchn.NoPO";
            if (viewPrice == 1)
                strSQL = "select NoPO,SupplierName,Cetak,Approval,PPN,PPH,Nama,ItemName,NoSPP, case when (select isnull(harga,0) from Inventory where ID=A.ItemID)=0  " +
             "then Price2 else 0 end Price2,Qty,UOMCode,Disc,case when (select isnull(harga,0) from Inventory where ID=A.ItemID)=0  " +
             "then Price else 0 end Price,case when (select isnull(harga,0) from Inventory where ID=A.ItemID)=0  " +
             "then Total else 0 end Total,case when (select isnull(harga,0) from Inventory where ID=A.ItemID)=0  " +
             "then TOT2 else 0 end TOT2,POPurchnDate,groupdesc from ( " +
             "SELECT popurchndetail.itemid, POPurchn.NoPO, SuppPurch.SupplierName,POPurchn.Cetak,POPurchn.Approval, POPurchn.PPN,POPurchn.PPH, MataUang.Nama, CASE POPurchnDetail.ItemTypeID WHEN 1 THEN " +
             "(SELECT ItemName FROM Inventory WHERE ID = POPurchnDetail.ItemID AND RowStatus > - 1) WHEN 2 THEN " + "(SELECT ItemName FROM Asset WHERE ID = POPurchnDetail.ItemID AND RowStatus > - 1) ELSE " +
             ItemSPPBiayaNew("POPurchnDetail") + " END AS ItemName, SPP.NoSPP, POPurchnDetail.Price as Price2, POPurchnDetail.Qty,  " +
             "UOM.UOMCode, POPurchn.Disc, case when POPurchn.Disc>0 then " +
             "(POPurchnDetail.Price-(POPurchnDetail.Price*POPurchn.Disc/100)) " +
             "else POPurchnDetail.Price end Price,(case when POPurchn.Disc>0 then " +
             "(POPurchnDetail.Price-(POPurchnDetail.Price*POPurchn.Disc/100)) " +
             "else POPurchnDetail.Price end)*POPurchnDetail.Qty as Total, " +
             "POPurchnDetail.Price * POPurchnDetail.Qty AS TOT2, POPurchn.POPurchnDate, GROUPsPURCHN.groupdescription as groupdesc " +
             "FROM POPurchn INNER JOIN POPurchnDetail ON POPurchn.ID = POPurchnDetail.POID " +
             "INNER JOIN SPP ON POPurchnDetail.SPPID = SPP.ID INNER JOIN UOM ON POPurchnDetail.UOMID = UOM.ID " +
             "INNER JOIN SuppPurch ON POPurchn.SupplierID = SuppPurch.ID " +
             "INNER JOIN MataUang ON POPurchn.Crc = MataUang.ID INNER JOIN GROUPsPURCHN ON POPurchnDetail.groupid = GROUPsPURCHN.id where  POPurchn.status>-1 and " +
             "convert(varchar,POPurchn.POPurchndate,112)>='" + tgl1 + "' and  convert(varchar,POPurchn.POPurchndate,112)<='" + tgl2 + "' and POPurchnDetail.Status >-1 )as A order by NoPO";
            if (viewPrice == 2)
                strSQL = "SELECT POPurchn.NoPO, SuppPurch.SupplierName,POPurchn.Cetak,POPurchn.Approval, POPurchn.PPN,POPurchn.PPH, MataUang.Nama, CASE POPurchnDetail.ItemTypeID WHEN 1 THEN " +
              "(SELECT ItemName FROM Inventory WHERE ID = POPurchnDetail.ItemID AND RowStatus > - 1) WHEN 2 THEN " + "(SELECT ItemName FROM Asset WHERE ID = POPurchnDetail.ItemID AND RowStatus > - 1) ELSE " +
              ItemSPPBiayaNew("POPurchnDetail") + " END AS ItemName, SPP.NoSPP, POPurchnDetail.Price as Price2, POPurchnDetail.Qty,  " +
              "UOM.UOMCode, POPurchn.Disc, case when POPurchn.Disc>0 then " +
              "(POPurchnDetail.Price-(POPurchnDetail.Price*POPurchn.Disc/100)) " +
              "else POPurchnDetail.Price end Price,(case when POPurchn.Disc>0 then " +
              "(POPurchnDetail.Price-(POPurchnDetail.Price*POPurchn.Disc/100)) " +
              "else POPurchnDetail.Price end)*POPurchnDetail.Qty as Total, " +
              "POPurchnDetail.Price * POPurchnDetail.Qty AS TOT2, POPurchn.POPurchnDate, GROUPsPURCHN.groupdescription as groupdesc " +
              "FROM POPurchn INNER JOIN POPurchnDetail ON POPurchn.ID = POPurchnDetail.POID " +
              "INNER JOIN SPP ON POPurchnDetail.SPPID = SPP.ID INNER JOIN UOM ON POPurchnDetail.UOMID = UOM.ID " +
              "INNER JOIN SuppPurch ON POPurchn.SupplierID = SuppPurch.ID " +
              "INNER JOIN MataUang ON POPurchn.Crc = MataUang.ID INNER JOIN GROUPsPURCHN ON POPurchnDetail.groupid = GROUPsPURCHN.id where  POPurchn.status>-1 and " +
              "convert(varchar,POPurchn.POPurchndate,112)>='" + tgl1 + "' and  convert(varchar,POPurchn.POPurchndate,112)<='" + tgl2 + "' and POPurchnDetail.Status >-1  order by POPurchn.NoPO";
            return strSQL;
        }
        public string ViewRekapReceipt(string tgl1, string tgl2)
        {
            return "SELECT Receipt.PONo, Receipt.ReceiptNo, SuppPurch.SupplierName, POPurchn.PaymentType, POPurchn.ItemFrom, ReceiptDetail.Keterangan," +
                    "CASE ReceiptDetail.ItemTypeID  " +
                    "WHEN 1 THEN (SELECT ItemCode FROM Inventory WHERE ID = ReceiptDetail.ItemID)  " +
                    "WHEN 2 THEN (SELECT ItemCode FROM Asset WHERE ID = ReceiptDetail.ItemID)  " +
                    "ELSE (SELECT ItemCode FROM Biaya WHERE ID = ReceiptDetail.ItemID) END AS ItemCode,  " +
                    "CASE ReceiptDetail.ItemTypeID  " +
                    "WHEN 1 THEN (SELECT ItemName FROM Inventory WHERE ID = ReceiptDetail.ItemID)  " +
                    "WHEN 2 THEN (SELECT ItemName FROM Asset WHERE ID = ReceiptDetail.ItemID)  " +
                    "ELSE (SELECT ItemName FROM Biaya WHERE ID = ReceiptDetail.ItemID) END AS ItemName,  " +
                    "ReceiptDetail.Quantity,ReceiptDetail.SPPNo, UOM.UOMCode, Receipt.ReceiptDate " +
                    "FROM Receipt INNER JOIN " +
                    "ReceiptDetail ON Receipt.ID = ReceiptDetail.ReceiptID and ReceiptDetail.RowStatus > -1  INNER JOIN " +
                    "POPurchn ON Receipt.POID = POPurchn.ID and ReceiptDetail.RowStatus>-1 INNER JOIN " +
                    "SuppPurch ON POPurchn.SupplierID = SuppPurch.ID INNER JOIN " +
                    "UOM ON ReceiptDetail.UomID = UOM.ID " +
                    "where  Receipt.status>-1 and convert(varchar,Receipt.Receiptdate,112)>='" + tgl1 +
                    "' and  convert(varchar,Receipt.Receiptdate,112)<='" + tgl2 + "'";
        }
        public string ViewRekapReceipt2(string tgl1, string tgl2, string grup, int viewP)
        {
            string strgrup = string.Empty;
            if (grup != "0")
                if (IsNumeric(grup) == false)
                    strgrup = " and RCP.receiptno like '%" + grup + "%'  ";
                else
                    strgrup = " and RCD.GroupID =  " + grup + " ";
            else
                strgrup = "";
            string strSQL = "select receiptdetailID,kurs, HargaPO,ID,PONo, ReceiptNo, SupplierName,PaymentType,ItemFrom, CRC ,Keterangan, ItemCode,ItemName,  " +
            "ItemID, SPPNo, UOMCode, NamaGrup, ReceiptDate,disc, kadarair,PPN,remark,PODetailID,qtyPO,  " +
            "Quantity,qtyPO-TotRCP as sisaPO,  " +
            "case when viewP=2 then (Harga2*kurs) else (Harga1*kurs) end Harga,  " +
            "case when viewP=2 then (Total2*kurs) else (Total1*kurs) end total,FakturPajak as NoFaktur,FakturPajakDate as TglFaktur,PKP  from (  " +
            "SELECT  " + viewP + " as viewP,POD.price as HargaPO,RCD.ID, RCP.PONo, RCP.ReceiptNo,RCP.FakturPajak,RCP.FakturPajakDate, " +
            "RCD.id as receiptdetailID, " +
            "case when (select ISNULL(subcompanyid,0) from SuppPurch where ID=Sup.ID) >0 and LEFT(convert(char,RCP.ReceiptDate,112),6)>='201602' "+
            "and RCD.groupid=1 then (select rtrim(SupplierName) from SuppPurch where COID=Sup.subcompanyid) else Sup.SupplierName end SupplierName,PO.PaymentType,PO.ItemFrom, MU.Nama as CRC ,RCD.Keterangan,   " +
            //"Sup.SupplierName,PO.PaymentType,PO.ItemFrom, MU.Nama as CRC ,RCD.Keterangan,   " +
            "CASE RCD.ItemTypeID     " +
            "WHEN 1 THEN (SELECT ItemCode FROM Inventory WHERE ID = RCD.ItemID)     " +
            "WHEN 2 THEN (SELECT ItemCode FROM Asset WHERE ID = RCD.ItemID)     " +
            "ELSE (SELECT ItemCode FROM Biaya WHERE ID = RCD.ItemID) END AS ItemCode,     " +
            "CASE RCD.ItemTypeID     " +
            "WHEN 1 THEN (SELECT ItemName FROM Inventory WHERE ID = RCD.ItemID)     " +
            "WHEN 2 THEN (SELECT ItemName FROM Asset WHERE ID = RCD.ItemID)     " +
            "ELSE /*(SELECT ItemName FROM Biaya WHERE ID = RCD.ItemID)*/ " +
            ItemSPPBiayaNew("POD") + " END AS ItemName,    " +
            "RCD.ItemID, RCD.SPPNo, U.UOMCode, UPPER(G.GroupDescription) as NamaGrup, RCP.ReceiptDate,  " +
            "PO.disc, RCD.kadarair,PO.PPN,RCD.Keterangan as remark,RCD.Quantity ,RCD.PODetailID,   " +
            "case when RCD.PODetailID>0 then (select SUM(Quantity)  from ReceiptDetail inRCD inner join Receipt inRC on inRC.ID=inRCD.ReceiptID   " +
            "where inRCD.PODetailID=RCD.PODetailID  and inRC.Status>=0  and inRCD.ID<=RCD.ID) end TotRCP, POD.Qty as qtyPO, POD.PRICE as Harga1,";
            
            //"case when RCD.PRICE=0  then (select top 1 isnull(THB.Harga,0) from HargaKertas THB where THB.ItemID =RCD.ItemID and THB.SupplierID=PO.SupplierID)  " +
            //"    when RCD.PRICE=0 and RCD.kadarair<25 then  (select isnull(THB.Harga,0) from HargaKertas THB  where THB.ItemID =RCD.ItemID and THB.SupplierID=PO.SupplierID )  " +
            //"    else isnull(POD.PRICE,0) end  Harga2, isnull(sup.pkp,'No')  PKP,  " +
            //"    ((POD.PRICE * RCD.quantity)-((POD.price *PO.disc * RCD.quantity) / 100)) as Total1,  " +
            //"case when RCD.PRICE=0 and RCD.kadarair>=25 then (select top 1 ((THB.Harga)*RCD.quantity)-((POD.price *PO.disc * RCD.quantity) / 100)   " +
            //"        from HargaKertas THB  where  THB.ItemID =RCD.ItemID and THB.SupplierID=PO.SupplierID /*and THB.KadarAir>=25*/ )  " +
            //"    when RCD.PRICE=0 and RCD.kadarair<25 then (select top 1 (THB.Harga*RCD.quantity)-((POD.price *PO.disc * RCD.quantity) / 100)   " +
            //"        from HargaKertas THB where  THB.ItemID =RCD.ItemID and THB.SupplierID=PO.SupplierID /*and THB.KadarAir<25*/)  " +
            //"    else (POD.PRICE*RCD.quantity)-((POD.price *PO.disc * RCD.quantity) / 100) end  Total2," +
            /**
             * Semua PO yang Field Price =0 maka price di ganti ke Price2
             * Price 2 di isi oleh Accounting
             * based on meeting at 12-05-2016
             */
            strSQL += "CASE WHEN POD.Price=0 THEN ISNULL(POD.Price2,0) ELSE POD.Price END Harga2,ISNULL(sup.Pkp,'No')PKP," +
                      "((POD.PRICE * RCD.quantity)-((POD.price *PO.disc * RCD.quantity) / 100)) as Total1," +
                      "CASE WHEN RCD.Price=0 THEN ((ISNULL(POD.Price2,0)*RCD.Quantity)-(((ISNULL(POD.Price2,0)*PO.Disc*RCD.Quantity)/100)))" +
                      "ELSE ((POD.PRICE * RCD.quantity)-((POD.price *PO.disc * RCD.quantity) / 100)) END Total2,";
            strSQL +=
            " case when MU.ID >1 then (select case when NilaiKurs  >1 then NilaiKurs else (select top 1 isnull(kurs,1) from MataUangKurs where RowStatus>=0 and drTgl <=RCP.ReceiptDate " +
            "and PO.Crc=MataUangKurs.MUID and sdTgl <=RCP.ReceiptDate order by ID desc ) end nilaikurs " +
            "from POPurchn  where ID=PO.ID  " +
            ") else 1 end kurs " +
            "FROM Receipt RCP  " +
            "LEFT JOIN ReceiptDetail RCD ON RCP.ID = RCD.ReceiptID and RCD.RowStatus > -1 " +
            "LEFT JOIN POPurchn PO ON RCD.POID =PO.ID and RCD.RowStatus>-1  " +
            "LEFT JOIN POPurchnDetail POD on RCD.PODetailID =POD.ID and POD.Status!=-1 " +
            "LEFT JOIN SuppPurch Sup ON PO.SupplierID = Sup.ID   " +
            "LEFT JOIN MataUang MU ON PO.Crc = MU.ID   " +
            "LEFT JOIN UOM U ON RCD.UomID = U.ID  " +
            "LEFT JOIN GroupsPurchn G ON  RCD.GroupID = G.ID  " +
            "where  RCP.status>-1 and convert(varchar,RCP.Receiptdate,112)>='" + tgl1 + "' and   " +
            "convert(varchar,RCP.Receiptdate,112)<='" + tgl2 + "' " + strgrup + " ) as query order by PODetailID, ID";
            return strSQL;
        }
        public string ViewRekapReceipt2P(string tgl1, string tgl2, string grup, int viewP)
        {
            string strgrup = string.Empty;
            if (grup != "0")
                if (IsNumeric(grup) == false)
                    strgrup = " and RCP.receiptno like '%" + grup + "%'  ";
                else
                    strgrup = " and RCD.GroupID =  " + grup + " ";
            else
                strgrup = "";
            string strSQL = "select receiptdetailID,kurs, HargaPO,ID,PONo, ReceiptNo, SupplierName,PaymentType,ItemFrom, CRC ,Keterangan, ItemCode,ItemName,  " +
            "ItemID, SPPNo, UOMCode, NamaGrup, ReceiptDate,disc, kadarair,PPN,remark,PODetailID,qtyPO,  " +
            "Quantity,qtyPO-TotRCP as sisaPO,  " +
            "case when viewP=2 then (Harga2*kurs) else (Harga1*kurs) end Harga,  " +
            "case when viewP=2 then (Total2*kurs) else (Total1*kurs) end total,FakturPajak as NoFaktur,FakturPajakDate as TglFaktur,PKP  from (  " +
            "SELECT  " + viewP + " as viewP,POD.price as HargaPO,RCD.ID, RCP.PONo, RCP.ReceiptNo,RCP.FakturPajak,RCP.FakturPajakDate, RCD.id as receiptdetailID, " +
            "Sup.SupplierName ,PO.PaymentType,PO.ItemFrom, MU.Nama as CRC ,RCD.Keterangan,   " +
            "CASE RCD.ItemTypeID     " +
            "WHEN 1 THEN (SELECT ItemCode FROM Inventory WHERE ID = RCD.ItemID)     " +
            "WHEN 2 THEN (SELECT ItemCode FROM Asset WHERE ID = RCD.ItemID)     " +
            "ELSE (SELECT ItemCode FROM Biaya WHERE ID = RCD.ItemID) END AS ItemCode,     " +
            "CASE RCD.ItemTypeID     " +
            "WHEN 1 THEN (SELECT ItemName FROM Inventory WHERE ID = RCD.ItemID)     " +
            "WHEN 2 THEN (SELECT ItemName FROM Asset WHERE ID = RCD.ItemID)     " +
            "ELSE /*(SELECT ItemName FROM Biaya WHERE ID = RCD.ItemID)*/ " +
            ItemSPPBiayaNew("POD") + " END AS ItemName,    " +
            "RCD.ItemID, RCD.SPPNo, U.UOMCode, UPPER(G.GroupDescription) as NamaGrup, RCP.ReceiptDate,  " +
            "PO.disc, RCD.kadarair,PO.PPN,RCD.Keterangan as remark,RCD.Quantity ,RCD.PODetailID,   " +
            "case when RCD.PODetailID>0 then (select SUM(Quantity)  from ReceiptDetail inRCD inner join Receipt inRC on inRC.ID=inRCD.ReceiptID   " +
            "where inRCD.PODetailID=RCD.PODetailID  and inRC.Status>=0  and inRCD.ID<=RCD.ID) end TotRCP, POD.Qty as qtyPO, POD.PRICE as Harga1,  " +
            "case when RCD.PRICE=0  then (select top 1 isnull(THB.Harga,0) from HargaKertas THB where THB.ItemID =RCD.ItemID and THB.SupplierID=PO.SupplierID)  " +
            "    when RCD.PRICE=0 and RCD.kadarair<25 then  (select isnull(THB.Harga,0) from HargaKertas THB  where THB.ItemID =RCD.ItemID and THB.SupplierID=PO.SupplierID )  " +
            "    else isnull(POD.PRICE,0) end  Harga2, isnull(sup.pkp,'No')  PKP,  " +
            "    ((POD.PRICE * RCD.quantity)-((POD.price *PO.disc * RCD.quantity) / 100)) as Total1,  " +
            "case when RCD.PRICE=0 and RCD.kadarair>=25 then (select top 1 ((THB.Harga)*RCD.quantity)-((POD.price *PO.disc * RCD.quantity) / 100)   " +
            "        from HargaKertas THB  where  THB.ItemID =RCD.ItemID and THB.SupplierID=PO.SupplierID /*and THB.KadarAir>=25*/ )  " +
            "    when RCD.PRICE=0 and RCD.kadarair<25 then (select top 1 (THB.Harga*RCD.quantity)-((POD.price *PO.disc * RCD.quantity) / 100)   " +
            "        from HargaKertas THB where  THB.ItemID =RCD.ItemID and THB.SupplierID=PO.SupplierID /*and THB.KadarAir<25*/)  " +
            "    else (POD.PRICE*RCD.quantity)-((POD.price *PO.disc * RCD.quantity) / 100) end  Total2," +
            " case when MU.ID >1 then (select case when NilaiKurs  >1 then NilaiKurs else (select top 1 isnull(kurs,1) from MataUangKurs where RowStatus>=0 and drTgl <=RCP.ReceiptDate " +
            "and PO.Crc=MataUangKurs.MUID and sdTgl <=RCP.ReceiptDate order by ID desc ) end nilaikurs " +
            "from POPurchn  where ID=PO.ID  " +
            ") else 1 end kurs " +
            "FROM Receipt RCP  left JOIN ReceiptDetail RCD ON RCP.ID = RCD.ReceiptID  " +
             "and RCD.RowStatus > -1 left JOIN POPurchn PO ON RCD.POID =PO.ID and RCD.RowStatus>-1  left join POPurchnDetail POD on  " +
             "RCD.PODetailID =POD.ID and POD.Status!=-1 left JOIN SuppPurch Sup ON PO.SupplierID = Sup.ID   " +
             "left JOIN MataUang MU ON PO.Crc = MU.ID   left JOIN UOM U ON RCD.UomID = U.ID  left JOIN GroupsPurchn G ON  " +
             "RCD.GroupID = G.ID   where  RCP.status>-1 and convert(varchar,RCP.Receiptdate,112)>='" + tgl1 + "' and   " +
            "convert(varchar,RCP.Receiptdate,112)<='" + tgl2 + "' " + strgrup + " ) as query order by PODetailID, ID";
            return strSQL;
        }
        public string ViewRekapReceipt2ByItemCode(string tgl1, string tgl2, int viewP, int itemid, int itemtypeid, int groupid)
        {
            string strgrup = string.Empty;
            strgrup = " and RCD.itemid =" + itemid + " and RCD.groupid=" + groupid + " and RCD.itemtypeid=" + itemtypeid + " ";

            string strSQL = "select receiptdetailID,kurs, HargaPO,ID,PONo, ReceiptNo, SupplierName,PaymentType,ItemFrom, CRC ,Keterangan, ItemCode,ItemName,  " +
            "ItemID, SPPNo, UOMCode, NamaGrup, ReceiptDate,disc, kadarair,PPN,remark,PODetailID,qtyPO,  " +
            "Quantity,qtyPO-TotRCP as sisaPO,  " +
            "case when viewP=2 then (Harga2*kurs) else (Harga1*kurs) end Harga,  " +
            "case when viewP=2 then (Total2*kurs) else (Total1*kurs) end total,FakturPajak as NoFaktur,FakturPajakDate as TglFaktur,PKP  from (  " +
            "SELECT  " + viewP + " as viewP,POD.price as HargaPO,RCD.ID, RCP.PONo, RCP.ReceiptNo,RCP.FakturPajak,RCP.FakturPajakDate, RCD.id as receiptdetailID, " +
            "case when (select ISNULL(subcompanyid,0) from SuppPurch where ID=Sup.ID)>0 and LEFT(convert(char,RCP.ReceiptDate,112),6)>='201602' then (select rtrim(SupplierName) from SuppPurch where COID=Sup.subcompanyid) else Sup.SupplierName end SupplierName,PO.PaymentType,PO.ItemFrom, MU.Nama as CRC ,RCD.Keterangan,   " +
            "CASE RCD.ItemTypeID     " +
            "WHEN 1 THEN (SELECT ItemCode FROM Inventory WHERE ID = RCD.ItemID)     " +
            "WHEN 2 THEN (SELECT ItemCode FROM Asset WHERE ID = RCD.ItemID)     " +
            "ELSE (SELECT ItemCode FROM Biaya WHERE ID = RCD.ItemID) END AS ItemCode,     " +
            "CASE RCD.ItemTypeID     " +
            "WHEN 1 THEN (SELECT ItemName FROM Inventory WHERE ID = RCD.ItemID)     " +
            "WHEN 2 THEN (SELECT ItemName FROM Asset WHERE ID = RCD.ItemID)     " +
            "ELSE /*(SELECT ItemName FROM Biaya WHERE ID = RCD.ItemID)*/ " +
            ItemSPPBiayaNew("POD") + " END AS ItemName,    " +
            "RCD.ItemID, RCD.SPPNo, U.UOMCode, UPPER(G.GroupDescription) as NamaGrup, RCP.ReceiptDate,  " +
            "PO.disc, RCD.kadarair,PO.PPN,RCD.Keterangan as remark,RCD.Quantity ,RCD.PODetailID,   " +
            "case when RCD.PODetailID>0 then (select SUM(Quantity)  from ReceiptDetail inRCD inner join Receipt inRC on inRC.ID=inRCD.ReceiptID   " +
            "where inRCD.PODetailID=RCD.PODetailID  and inRC.Status>=0  and inRCD.ID<=RCD.ID) end TotRCP, POD.Qty as qtyPO, POD.PRICE as Harga1,  " +
            "case when RCD.PRICE=0  then (select top 1 (THB.hargasatuan) from TabelHargaBankOut THB where THB.ReceiptDetailID =RCD.ID)  " +
            "    when RCD.PRICE=0 and RCD.kadarair<25 then  (select isnull(THB.hargasatuan,0) from TabelHargaBankOut THB  where THB.ReceiptDetailID =RCD.ID )  " +
            "    else isnull(POD.PRICE,0) end  Harga2, isnull(sup.pkp,'No')  PKP,  " +
            "    ((POD.PRICE * RCD.quantity)-((POD.price *PO.disc * RCD.quantity) / 100)) as Total1,  " +
            "case when RCD.PRICE=0 and RCD.kadarair>=25 then (select top 1 ((THB.hargasatuan)*RCD.quantity)-((POD.price *PO.disc * RCD.quantity) / 100)   " +
            "        from TabelHargaBankOut THB  where  THB.ReceiptDetailID =RCD.ID )  " +
            "    when RCD.PRICE=0 and RCD.kadarair<25 then (select top 1 (THB.hargasatuan*RCD.quantity)-((POD.price *PO.disc * RCD.quantity) / 100)   " +
            "        from TabelHargaBankOut THB where THB.ReceiptDetailID =RCD.ID )  " +
            "    else (POD.PRICE*RCD.quantity)-((POD.price *PO.disc * RCD.quantity) / 100) end  Total2," +
            " case when MU.ID >1 then (select case when NilaiKurs  >1 then NilaiKurs else (select top 1 isnull(kurs,1) from MataUangKurs where RowStatus>=0 and drTgl <=RCP.ReceiptDate " +
            "and PO.Crc=MataUangKurs.MUID and sdTgl <=RCP.ReceiptDate order by ID desc ) end nilaikurs " +
            "from POPurchn  where ID=PO.ID  " +
            ") else 1 end kurs " +
              "FROM Receipt RCP  left JOIN ReceiptDetail RCD ON RCP.ID = RCD.ReceiptID  " +
             "and RCD.RowStatus > -1 left JOIN POPurchn PO ON RCD.POID =PO.ID and RCD.RowStatus>-1  left join POPurchnDetail POD on  " +
             "RCD.PODetailID =POD.ID and POD.Status!=-1 left JOIN SuppPurch Sup ON PO.SupplierID = Sup.ID   " +
             "left JOIN MataUang MU ON PO.Crc = MU.ID   left JOIN UOM U ON RCD.UomID = U.ID  left JOIN GroupsPurchn G ON  " +
             "RCD.GroupID = G.ID   where  RCP.status>-1 and convert(varchar,RCP.Receiptdate,112)>='" + tgl1 + "' and   " +
              "convert(varchar,RCP.Receiptdate,112)<='" + tgl2 + "' " + strgrup + " ) as query order by PODetailID, ID";
            return strSQL;
        }
        public string ViewRekapReceipt2PByItemCode(string tgl1, string tgl2, int viewP, int itemid, int itemtypeid, int groupid)
        {
            string strgrup = string.Empty;
            strgrup = " and RCD.itemid =" + itemid + " and RCD.groupid=" + groupid + " and RCD.itemtypeid=" + itemtypeid + " ";

            string strSQL = "select receiptdetailID,kurs, HargaPO,ID,PONo, ReceiptNo, SupplierName,PaymentType,ItemFrom, CRC ,Keterangan, ItemCode,ItemName,  " +
            "ItemID, SPPNo, UOMCode, NamaGrup, ReceiptDate,disc, kadarair,PPN,remark,PODetailID,qtyPO,  " +
            "Quantity,qtyPO-TotRCP as sisaPO,  " +
            "case when viewP=2 then (Harga2*kurs) else (Harga1*kurs) end Harga,  " +
            "case when viewP=2 then (Total2*kurs) else (Total1*kurs) end total,FakturPajak as NoFaktur,FakturPajakDate as TglFaktur,PKP  from (  " +
            "SELECT  " + viewP + " as viewP,POD.price as HargaPO,RCD.ID, RCP.PONo, RCP.ReceiptNo,RCP.FakturPajak,RCP.FakturPajakDate, RCD.id as receiptdetailID, " +
            "Sup.SupplierName ,PO.PaymentType,PO.ItemFrom, MU.Nama as CRC ,RCD.Keterangan,   " +
            "CASE RCD.ItemTypeID     " +
            "WHEN 1 THEN (SELECT ItemCode FROM Inventory WHERE ID = RCD.ItemID)     " +
            "WHEN 2 THEN (SELECT ItemCode FROM Asset WHERE ID = RCD.ItemID)     " +
            "ELSE (SELECT ItemCode FROM Biaya WHERE ID = RCD.ItemID) END AS ItemCode,     " +
            "CASE RCD.ItemTypeID     " +
            "WHEN 1 THEN (SELECT ItemName FROM Inventory WHERE ID = RCD.ItemID)     " +
            "WHEN 2 THEN (SELECT ItemName FROM Asset WHERE ID = RCD.ItemID)     " +
            "ELSE /*(SELECT ItemName FROM Biaya WHERE ID = RCD.ItemID)*/ " +
            ItemSPPBiayaNew("POD") + " END AS ItemName,    " +
            "RCD.ItemID, RCD.SPPNo, U.UOMCode, UPPER(G.GroupDescription) as NamaGrup, RCP.ReceiptDate,  " +
            "PO.disc, RCD.kadarair,PO.PPN,RCD.Keterangan as remark,RCD.Quantity ,RCD.PODetailID,   " +
            "case when RCD.PODetailID>0 then (select SUM(Quantity)  from ReceiptDetail inRCD inner join Receipt inRC on inRC.ID=inRCD.ReceiptID   " +
            "where inRCD.PODetailID=RCD.PODetailID  and inRC.Status>=0  and inRCD.ID<=RCD.ID) end TotRCP, POD.Qty as qtyPO, POD.PRICE as Harga1,  " +
            "case when RCD.PRICE=0  then (select top 1 (THB.hargasatuan) from TabelHargaBankOut THB where THB.ReceiptDetailID =RCD.ID)  " +
            "    when RCD.PRICE=0 and RCD.kadarair<25 then  (select isnull(THB.hargasatuan,0) from TabelHargaBankOut THB  where THB.ReceiptDetailID =RCD.ID )  " +
            "    else isnull(POD.PRICE,0) end  Harga2, isnull(sup.pkp,'No')  PKP,  " +
            "    ((POD.PRICE * RCD.quantity)-((POD.price *PO.disc * RCD.quantity) / 100)) as Total1,  " +
            "case when RCD.PRICE=0 and RCD.kadarair>=25 then  (select top 1((THB.hargasatuan)*RCD.quantity)-((POD.price *PO.disc * RCD.quantity) / 100)   " +
            "        from TabelHargaBankOut THB  where  THB.ReceiptDetailID =RCD.ID )  " +
            "    when RCD.PRICE=0 and RCD.kadarair<25 then  (select top 1(THB.hargasatuan*RCD.quantity)-((POD.price *PO.disc * RCD.quantity) / 100)   " +
            "        from TabelHargaBankOut THB where THB.ReceiptDetailID =RCD.ID )  " +
            "    else (POD.PRICE*RCD.quantity)-((POD.price *PO.disc * RCD.quantity) / 100) end  Total2," +
            " case when MU.ID >1 then (select case when NilaiKurs  >1 then NilaiKurs else (select top 1 isnull(kurs,1) from MataUangKurs where RowStatus>=0 and drTgl <=RCP.ReceiptDate " +
            "and PO.Crc=MataUangKurs.MUID and sdTgl <=RCP.ReceiptDate order by ID desc ) end nilaikurs " +
            "from POPurchn  where ID=PO.ID  " +
            ") else 1 end kurs " +
              "FROM Receipt RCP  left JOIN ReceiptDetail RCD ON RCP.ID = RCD.ReceiptID  " +
             "and RCD.RowStatus > -1 left JOIN POPurchn PO ON RCD.POID =PO.ID and RCD.RowStatus>-1  left join POPurchnDetail POD on  " +
             "RCD.PODetailID =POD.ID and POD.Status!=-1 left JOIN SuppPurch Sup ON PO.SupplierID = Sup.ID   " +
             "left JOIN MataUang MU ON PO.Crc = MU.ID   left JOIN UOM U ON RCD.UomID = U.ID  left JOIN GroupsPurchn G ON  " +
             "RCD.GroupID = G.ID   where  RCP.status>-1 and convert(varchar,RCP.Receiptdate,112)>='" + tgl1 + "' and   " +
              "convert(varchar,RCP.Receiptdate,112)<='" + tgl2 + "' " + strgrup + " ) as query order by PODetailID, ID";
            return strSQL;
        }
        public string ViewRekapReceipt2BySupp(string tgl1, string tgl2, string Supplier, int viewP)
        {
            string strgrup = string.Empty;
            strgrup = " and RCP.supplierid in ( select id from SuppPurch where suppliername like '%" + Supplier.Trim() + "%')  ";

            string strSQL = "select receiptdetailID,kurs, HargaPO,ID,PONo, ReceiptNo, SupplierName,PaymentType,ItemFrom, CRC ,Keterangan, ItemCode,ItemName,  " +
            "ItemID, SPPNo, UOMCode, NamaGrup, ReceiptDate,disc, kadarair,PPN,remark,PODetailID,qtyPO,  " +
            "Quantity,qtyPO-TotRCP as sisaPO,  " +
            "case when viewP=2 then (Harga2*kurs) else (Harga1*kurs) end Harga,  " +
            "case when viewP=2 then (Total2*kurs) else (Total1*kurs) end total,FakturPajak as NoFaktur,FakturPajakDate as TglFaktur,PKP  from (  " +
            "SELECT  " + viewP + " as viewP,POD.price as HargaPO,RCD.ID, RCP.PONo, RCP.ReceiptNo,RCP.FakturPajak,RCP.FakturPajakDate, RCD.id as receiptdetailID, " +
            "case when (select ISNULL(subcompanyid,0) from SuppPurch where ID=Sup.ID)>0 and LEFT(convert(char,RCP.ReceiptDate,112),6)>='201602' then (select rtrim(SupplierName) from SuppPurch where COID=Sup.subcompanyid) else Sup.SupplierName end SupplierName,PO.PaymentType,PO.ItemFrom, MU.Nama as CRC ,RCD.Keterangan,   " +
            "CASE RCD.ItemTypeID     " +
            "WHEN 1 THEN (SELECT ItemCode FROM Inventory WHERE ID = RCD.ItemID)     " +
            "WHEN 2 THEN (SELECT ItemCode FROM Asset WHERE ID = RCD.ItemID)     " +
            "ELSE (SELECT ItemCode FROM Biaya WHERE ID = RCD.ItemID) END AS ItemCode,     " +
            "CASE RCD.ItemTypeID     " +
            "WHEN 1 THEN (SELECT ItemName FROM Inventory WHERE ID = RCD.ItemID)     " +
            "WHEN 2 THEN (SELECT ItemName FROM Asset WHERE ID = RCD.ItemID)     " +
            "ELSE /*(SELECT ItemName FROM Biaya WHERE ID = RCD.ItemID)*/ " +
            ItemSPPBiayaNew("POD") + " END AS ItemName,    " +
            "RCD.ItemID, RCD.SPPNo, U.UOMCode, UPPER(G.GroupDescription) as NamaGrup, RCP.ReceiptDate,  " +
            "PO.disc, RCD.kadarair,PO.PPN,RCD.Keterangan as remark,RCD.Quantity ,RCD.PODetailID,   " +
            "case when RCD.PODetailID>0 then (select SUM(Quantity)  from ReceiptDetail inRCD inner join Receipt inRC on inRC.ID=inRCD.ReceiptID   " +
            "where inRCD.PODetailID=RCD.PODetailID  and inRC.Status>=0  and inRCD.ID<=RCD.ID) end TotRCP, POD.Qty as qtyPO, POD.PRICE as Harga1,  " +
            "case when RCD.PRICE=0  then (select top 1 (THB.hargasatuan) from TabelHargaBankOut THB where THB.ReceiptDetailID =RCD.ID)  " +
            "    when RCD.PRICE=0 and RCD.kadarair<25 then  (select isnull(THB.hargasatuan,0) from TabelHargaBankOut THB  where THB.ReceiptDetailID =RCD.ID )  " +
            "    else isnull(POD.PRICE,0) end  Harga2, isnull(sup.pkp,'No')  PKP,  " +
            "    ((POD.PRICE * RCD.quantity)-((POD.price *PO.disc * RCD.quantity) / 100)) as Total1,  " +
            "case when RCD.PRICE=0 and RCD.kadarair>=25 then (select top 1 ((THB.hargasatuan)*RCD.quantity)-((POD.price *PO.disc * RCD.quantity) / 100)   " +
            "        from TabelHargaBankOut THB  where  THB.ReceiptDetailID =RCD.ID )  " +
            "    when RCD.PRICE=0 and RCD.kadarair<25 then (select top 1 (THB.hargasatuan*RCD.quantity)-((POD.price *PO.disc * RCD.quantity) / 100)   " +
            "        from TabelHargaBankOut THB where THB.ReceiptDetailID =RCD.ID )  " +
            "    else (POD.PRICE*RCD.quantity)-((POD.price *PO.disc * RCD.quantity) / 100) end  Total2," +
            " case when MU.ID >1 then (select case when NilaiKurs  >1 then NilaiKurs else (select top 1 isnull(kurs,1) from MataUangKurs where RowStatus>=0 and drTgl <=RCP.ReceiptDate " +
            "and PO.Crc=MataUangKurs.MUID and sdTgl <=RCP.ReceiptDate order by ID desc ) end nilaikurs " +
            "from POPurchn  where ID=PO.ID  " +
            ") else 1 end kurs " +
            "FROM Receipt RCP  left JOIN ReceiptDetail RCD ON RCP.ID = RCD.ReceiptID  " +
             "and RCD.RowStatus > -1 left JOIN POPurchn PO ON RCD.POID =PO.ID and RCD.RowStatus>-1  left join POPurchnDetail POD on  " +
             "RCD.PODetailID =POD.ID and POD.Status!=-1 left JOIN SuppPurch Sup ON PO.SupplierID = Sup.ID   " +
             "left JOIN MataUang MU ON PO.Crc = MU.ID   left JOIN UOM U ON RCD.UomID = U.ID  left JOIN GroupsPurchn G ON  " +
             "RCD.GroupID = G.ID   where  RCP.status>-1 and convert(varchar,RCP.Receiptdate,112)>='" + tgl1 + "' and   " +
              "convert(varchar,RCP.Receiptdate,112)<='" + tgl2 + "' " + strgrup + " ) as query order by PODetailID, ID";
            return strSQL;
        }
        public string ViewRekapReceipt2PBySupp(string tgl1, string tgl2, string Supplier, int viewP)
        {
            string strgrup = string.Empty;
            strgrup = " and RCP.supplierid in ( select id from SuppPurch where suppliername like '%" + Supplier.Trim() + "%')  ";

            string strSQL = "select receiptdetailID,kurs, HargaPO,ID,PONo, ReceiptNo, SupplierName,PaymentType,ItemFrom, CRC ,Keterangan, ItemCode,ItemName,  " +
            "ItemID, SPPNo, UOMCode, NamaGrup, ReceiptDate,disc, kadarair,PPN,remark,PODetailID,qtyPO,  " +
            "Quantity,qtyPO-TotRCP as sisaPO,  " +
            "case when viewP=2 then (Harga2*kurs) else (Harga1*kurs) end Harga,  " +
            "case when viewP=2 then (Total2*kurs) else (Total1*kurs) end total,FakturPajak as NoFaktur,FakturPajakDate as TglFaktur,PKP  from (  " +
            "SELECT  " + viewP + " as viewP,POD.price as HargaPO,RCD.ID, RCP.PONo, RCP.ReceiptNo,RCP.FakturPajak,RCP.FakturPajakDate, RCD.id as receiptdetailID, " +
            "Sup.SupplierName,PO.PaymentType,PO.ItemFrom, MU.Nama as CRC ,RCD.Keterangan,   " +
            "CASE RCD.ItemTypeID     " +
            "WHEN 1 THEN (SELECT ItemCode FROM Inventory WHERE ID = RCD.ItemID)     " +
            "WHEN 2 THEN (SELECT ItemCode FROM Asset WHERE ID = RCD.ItemID)     " +
            "ELSE (SELECT ItemCode FROM Biaya WHERE ID = RCD.ItemID) END AS ItemCode,     " +
            "CASE RCD.ItemTypeID     " +
            "WHEN 1 THEN (SELECT ItemName FROM Inventory WHERE ID = RCD.ItemID)     " +
            "WHEN 2 THEN (SELECT ItemName FROM Asset WHERE ID = RCD.ItemID)     " +
            "ELSE /*(SELECT ItemName FROM Biaya WHERE ID = RCD.ItemID)*/ " +
            ItemSPPBiayaNew("POD") + " END AS ItemName,    " +
            "RCD.ItemID, RCD.SPPNo, U.UOMCode, UPPER(G.GroupDescription) as NamaGrup, RCP.ReceiptDate,  " +
            "PO.disc, RCD.kadarair,PO.PPN,RCD.Keterangan as remark,RCD.Quantity ,RCD.PODetailID,   " +
            "case when RCD.PODetailID>0 then (select SUM(Quantity)  from ReceiptDetail inRCD inner join Receipt inRC on inRC.ID=inRCD.ReceiptID   " +
            "where inRCD.PODetailID=RCD.PODetailID  and inRC.Status>=0  and inRCD.ID<=RCD.ID) end TotRCP, POD.Qty as qtyPO, POD.PRICE as Harga1,  " +
            "case when RCD.PRICE=0  then (select top 1 (THB.hargasatuan) from TabelHargaBankOut THB where THB.ReceiptDetailID =RCD.ID)  " +
            "    when RCD.PRICE=0 and RCD.kadarair<25 then  (select isnull(THB.hargasatuan,0) from TabelHargaBankOut THB  where THB.ReceiptDetailID =RCD.ID )  " +
            "    else isnull(POD.PRICE,0) end  Harga2, isnull(sup.pkp,'No')  PKP,  " +
            "    ((POD.PRICE * RCD.quantity)-((POD.price *PO.disc * RCD.quantity) / 100)) as Total1,  " +
            "case when RCD.PRICE=0 and RCD.kadarair>=25 then (select top 1 ((THB.hargasatuan)*RCD.quantity)-((POD.price *PO.disc * RCD.quantity) / 100)   " +
            "        from TabelHargaBankOut THB  where  THB.ReceiptDetailID =RCD.ID )  " +
            "    when RCD.PRICE=0 and RCD.kadarair<25 then (select top 1 (THB.hargasatuan*RCD.quantity)-((POD.price *PO.disc * RCD.quantity) / 100)   " +
            "        from TabelHargaBankOut THB where THB.ReceiptDetailID =RCD.ID )  " +
            "    else (POD.PRICE*RCD.quantity)-((POD.price *PO.disc * RCD.quantity) / 100) end  Total2," +
            " case when MU.ID >1 then (select case when NilaiKurs  >1 then NilaiKurs else (select top 1 isnull(kurs,1) from MataUangKurs where RowStatus>=0 and drTgl <=RCP.ReceiptDate " +
            "and PO.Crc=MataUangKurs.MUID and sdTgl <=RCP.ReceiptDate order by ID desc ) end nilaikurs " +
            "from POPurchn  where ID=PO.ID  " +
            ") else 1 end kurs " +
            "FROM Receipt RCP  left JOIN ReceiptDetail RCD ON RCP.ID = RCD.ReceiptID  " +
             "and RCD.RowStatus > -1 left JOIN POPurchn PO ON RCD.POID =PO.ID and RCD.RowStatus>-1  left join POPurchnDetail POD on  " +
             "RCD.PODetailID =POD.ID and POD.Status!=-1 left JOIN SuppPurch Sup ON PO.SupplierID = Sup.ID   " +
             "left JOIN MataUang MU ON PO.Crc = MU.ID   left JOIN UOM U ON RCD.UomID = U.ID  left JOIN GroupsPurchn G ON  " +
             "RCD.GroupID = G.ID   where  RCP.status>-1 and convert(varchar,RCP.Receiptdate,112)>='" + tgl1 + "' and   " +
              "convert(varchar,RCP.Receiptdate,112)<='" + tgl2 + "' " + strgrup + " ) as query order by PODetailID, ID";
            return strSQL;
        }
        public string ViewRekapReceipt3(string tgl1, string tgl2, string grup)
        {
            string strgrup = string.Empty;
            if (grup != "0")
                if (IsNumeric(grup) == false)
                    strgrup = " and Receipt.receiptno like '%" + grup + "%'  ";
                else
                    strgrup = " and ReceiptDetail.GroupID =  " + grup;
            else
                strgrup = "";
            return "SELECT Receipt.PONo, Receipt.ReceiptNo, SuppPurch.SupplierName, POPurchn.PaymentType, POPurchn.ItemFrom, ReceiptDetail.Keterangan,MataUang.Nama as CRC, " +
                    "CASE ReceiptDetail.ItemTypeID   " +
                    "WHEN 1 THEN (SELECT ItemCode FROM Inventory WHERE ID = ReceiptDetail.ItemID)   " +
                    "WHEN 2 THEN (SELECT ItemCode FROM Asset WHERE ID = ReceiptDetail.ItemID)   " +
                    "ELSE (SELECT ItemCode FROM Biaya WHERE ID = ReceiptDetail.ItemID) END AS ItemCode,   " +
                    "CASE ReceiptDetail.ItemTypeID   " +
                    "WHEN 1 THEN (SELECT ItemName FROM Inventory WHERE ID = ReceiptDetail.ItemID)   " +
                    "WHEN 2 THEN (SELECT ItemName FROM Asset WHERE ID = ReceiptDetail.ItemID)   " +
                    "ELSE /*(SELECT ItemName FROM Biaya WHERE ID = ReceiptDetail.ItemID)*/ " +
                    ItemSPPBiayaNew2("POPurchnDetail") + " END AS ItemName,   " +
                    "ReceiptDetail.Quantity,ReceiptDetail.SPPNo, UOM.UOMCode, UPPER(GroupsPurchn.GroupDescription) as NamaGrup, Receipt.ReceiptDate, " +
                    "0 as Harga, 0 as disc, 0 as total,0 as PPN,ReceiptDetail.Keterangan as remark " +
                    "FROM Receipt INNER JOIN  " +
                    "ReceiptDetail ON Receipt.ID = ReceiptDetail.ReceiptID and ReceiptDetail.RowStatus > -1 " + strgrup + " INNER JOIN  " +
                    "POPurchn ON Receipt.POID = POPurchn.ID and ReceiptDetail.RowStatus>-1 INNER JOIN  " +
                    "SuppPurch ON POPurchn.SupplierID = SuppPurch.ID INNER JOIN MataUang ON POPurchn.Crc = MataUang.ID INNER JOIN  " +
                    "UOM ON ReceiptDetail.UomID = UOM.ID INNER JOIN GroupsPurchn ON ReceiptDetail.GroupID = GroupsPurchn.ID  " +
                    "/*INNER JOIN POPurchnDetail ON POPurchnDetail.POID=POPurchn.ID */" +
                    "where  Receipt.status>-1 and convert(varchar,Receipt.Receiptdate,112)>='" + tgl1 +
                    "' and  convert(varchar,Receipt.Receiptdate,112)<='" + tgl2 + "'  " + strgrup + " ";
        }
        public string ViewRekapReceipt3ByItemCode(string tgl1, string tgl2, int viewP, int itemid, int itemtypeid, int groupid)
        {
            string strgrup = string.Empty;
            strgrup = " and ReceiptDetail.itemid =" + itemid + " and ReceiptDetail.groupid=" + groupid + " and ReceiptDetail.itemtypeid=" + itemtypeid + " ";

            return "SELECT Receipt.PONo, Receipt.ReceiptNo, SuppPurch.SupplierName, POPurchn.PaymentType, POPurchn.ItemFrom, ReceiptDetail.Keterangan,MataUang.Nama as CRC, " +
                    "CASE ReceiptDetail.ItemTypeID   " +
                    "WHEN 1 THEN (SELECT ItemCode FROM Inventory WHERE ID = ReceiptDetail.ItemID)   " +
                    "WHEN 2 THEN (SELECT ItemCode FROM Asset WHERE ID = ReceiptDetail.ItemID)   " +
                    "ELSE (SELECT ItemCode FROM Biaya WHERE ID = ReceiptDetail.ItemID) END AS ItemCode,   " +
                    "CASE ReceiptDetail.ItemTypeID   " +
                    "WHEN 1 THEN (SELECT ItemName FROM Inventory WHERE ID = ReceiptDetail.ItemID)   " +
                    "WHEN 2 THEN (SELECT ItemName FROM Asset WHERE ID = ReceiptDetail.ItemID)   " +
                    "ELSE /*(SELECT ItemName FROM Biaya WHERE ID = ReceiptDetail.ItemID)*/ " +
                    ItemSPPBiayaNew2("POPurchnDetail") + " END AS ItemName,   " +
                    "ReceiptDetail.Quantity,ReceiptDetail.SPPNo, UOM.UOMCode, UPPER(GroupsPurchn.GroupDescription) as NamaGrup, Receipt.ReceiptDate, " +
                    "0 as Harga, 0 as disc, 0 as total,0 as PPN,ReceiptDetail.Keterangan as remark " +
                    "FROM Receipt INNER JOIN  " +
                    "ReceiptDetail ON Receipt.ID = ReceiptDetail.ReceiptID and ReceiptDetail.RowStatus > -1 " + strgrup + " INNER JOIN  " +
                    "POPurchn ON Receipt.POID = POPurchn.ID and ReceiptDetail.RowStatus>-1 INNER JOIN  " +
                    "SuppPurch ON POPurchn.SupplierID = SuppPurch.ID INNER JOIN MataUang ON POPurchn.Crc = MataUang.ID INNER JOIN  " +
                    "UOM ON ReceiptDetail.UomID = UOM.ID INNER JOIN GroupsPurchn ON ReceiptDetail.GroupID = GroupsPurchn.ID  " +
                //"INNER JOIN POPurchnDetail ON POPurchnDetail.POID=POPurchn.ID " +
                    "where Receipt.status>-1 and convert(varchar,Receipt.Receiptdate,112)>='" + tgl1 + "' and   " +
                    "convert(varchar,Receipt.Receiptdate,112)<='" + tgl2 + "'  " + strgrup + " ";
        }
        public string ViewRekapReceipt3BySupp(string tgl1, string tgl2, string Supplier)
        {
            string strgrup = string.Empty;
            strgrup = " and Receipt.supplierid in ( select id from SuppPurch where suppliername like '%" + Supplier.Trim() + "%')  ";
            return "SELECT Receipt.PONo, Receipt.ReceiptNo, SuppPurch.SupplierName, POPurchn.PaymentType, POPurchn.ItemFrom, ReceiptDetail.Keterangan,MataUang.Nama as CRC, " +
                    "CASE ReceiptDetail.ItemTypeID   " +
                    "WHEN 1 THEN (SELECT ItemCode FROM Inventory WHERE ID = ReceiptDetail.ItemID)   " +
                    "WHEN 2 THEN (SELECT ItemCode FROM Asset WHERE ID = ReceiptDetail.ItemID)   " +
                    "ELSE (SELECT ItemCode FROM Biaya WHERE ID = ReceiptDetail.ItemID) END AS ItemCode,   " +
                    "CASE ReceiptDetail.ItemTypeID   " +
                    "WHEN 1 THEN (SELECT ItemName FROM Inventory WHERE ID = ReceiptDetail.ItemID)   " +
                    "WHEN 2 THEN (SELECT ItemName FROM Asset WHERE ID = ReceiptDetail.ItemID)   " +
                    "ELSE /*(SELECT ItemName FROM Biaya WHERE ID = ReceiptDetail.ItemID)*/ " +
                    ItemSPPBiayaNew2("POPurchnDetail") + " END AS ItemName,   " +
                    "ReceiptDetail.Quantity,ReceiptDetail.SPPNo, UOM.UOMCode, UPPER(GroupsPurchn.GroupDescription) as NamaGrup, Receipt.ReceiptDate, " +
                    "0 as Harga, 0 as disc, 0 as total,0 as PPN,ReceiptDetail.Keterangan as remark " +
                    "FROM Receipt INNER JOIN  " +
                    "ReceiptDetail ON Receipt.ID = ReceiptDetail.ReceiptID and ReceiptDetail.RowStatus > -1 " + strgrup + " INNER JOIN  " +
                    "POPurchn ON Receipt.POID = POPurchn.ID and ReceiptDetail.RowStatus>-1 INNER JOIN  " +
                    "SuppPurch ON POPurchn.SupplierID = SuppPurch.ID INNER JOIN MataUang ON POPurchn.Crc = MataUang.ID INNER JOIN  " +
                    "UOM ON ReceiptDetail.UomID = UOM.ID INNER JOIN GroupsPurchn ON ReceiptDetail.GroupID = GroupsPurchn.ID  " +
                //"INNER JOIN POPurchnDetail ON POPurchnDetail.POID=POPurchn.ID " +
                    "where  Receipt.status>-1 and convert(varchar,Receipt.Receiptdate,112)>='" + tgl1 + "' and  convert(varchar,Receipt.Receiptdate,112)<='" + tgl2 + "' " + strgrup;
        }
         public static string ItemSPPBiayaNew(string TableName)
        {
            string strSQL = "CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())<= " +
                " (Select CreatedTime from SPP where SPP.ID=" + TableName + ".SPPID)) " +
                " THEN(select ItemName from Biaya where Biaya.ID=" + TableName + ".ItemID and Biaya.RowStatus>-1)+' - '+ " +
                " (Select SPPDetail.Keterangan From SPPDetail where SPPDetail.ID=" + TableName + ".SPPDetailID and " +
                " SPPDetail.SPPID=" + TableName + ".SPPID) ELSE " +
                " (select ItemName from biaya where ID=" + TableName + ".ItemID and biaya.RowStatus>-1) END ";
            return strSQL;
        }
        public static List<object> ViewLapPemantauanPurchn(string drTgl, string sdTgl, int groupID)
        {
            List<object> all = new List<object>();
            using (var connection = new SqlConnection(Global.ConnectionString()))
            {
                try
                {
                    string strsql = "select A.NoSPP, Convert(varchar,A.Minta,103) as TglSPP, " +
                   "case A.Approval " +
                   "when 0 then 'user' " +
                   "when 1 then 'head' " +
                   "when 2 then 'manager' " +
                   "when 3 then 'purchasing' end ApprovalSPP, " +
                   "case B.ItemTypeID " +
                   "when 1 then (select ItemName from Inventory where B.ItemID=Inventory.ID and RowStatus > -1) " +
                   "when 2 then (select ItemName from Asset where B.ItemID=Asset.ID and RowStatus > -1) " +
                   "when 3 then " + ItemSPPBiayaNew("D") + " end NamaBarang, " +
                   "case B.ItemTypeID " +
                   "when 1 then (select ItemCode from Inventory where B.ItemID=Inventory.ID and RowStatus > -1) " +
                   "when 2 then (select ItemCode from Asset where B.ItemID=Asset.ID and RowStatus > -1) " +
                   "when 3 then (select ItemCode from Biaya where B.ItemID=Biaya.ID and RowStatus > -1) end KodeBarang, G.UOMCode as Satuan, B.Quantity as JumlahSPP, A.CreatedBy as UserName, " +
                   "C.NoPO as NoPO, Convert(varchar,C.POPurchnDate,103) as TglPO, Convert(varchar,C.ApproveDate1,103) as TglApprovalPO, D.Qty as JumlahPO, B.Quantity - D.Qty as SisaSPP, C.Indent, E.ReceiptNo as NoReceipt, " +
                   "case E.Status when 0 then 'Open' when 1 then 'App Head' when 2 then 'Parsial' when 3 then 'Buat Giro' when 4 then 'Serah Terima' when 5 then 'Release' end StatusReceipt,E.status as stReceipt, Convert(varchar,E.ReceiptDate,103) as TglReceipt, " +
                   "Convert(varchar,E.ApproveDate,103) as TglApprovalReceipt, F.Quantity as JumlahReceipt, D.Qty - F.Quantity as SisaPO " +
                   "from SPP as A, SPPDetail as B, POPurchn as C, POPurchnDetail as D, Receipt as E, ReceiptDetail as F, UOM as G " +
                   "where A.ID=B.SPPID and C.ID=D.POID and E.ID=F.ReceiptID and A.ID=D.SPPID  and C.ID=E.POID and B.UOMID=G.ID " +
                   "and D.ID=F.PODetailID and B.ID=D.SppDetailID and A.Status > -1 and B.Status > -1 and C.Status > -1 and D.Status > -1 and E.Status  > -1 and F.RowStatus  > -1 and G.RowStatus > -1 " +
                   "and Convert(varchar,A.Minta,112) >='" + drTgl + "' and Convert(varchar,A.Minta,112) <='" + sdTgl + "' order by C.POPurchnDate";

                    all = connection.Query<object>(strsql).ToList();
                }
                catch (Exception e)
                {
                    string mes = e.ToString();
                    all = null;
                }
            }
            return all;
        }
        public string ViewRekapPakaiDeptItem(string tgl1, string tgl2, int ItemID, int deptID, int itemtypeID)
        {
            return "select A.PakaiNo,CONVERT(VARCHAR,A.PakaiDate,103) as PakaiDate, C.DeptName, " +
                     "case B.ItemTypeID " +
                     "when 1 then (select ItemCode from Inventory where ID = B.ItemID) " +
                     "when 2 then (select ItemCode from Asset where ID = B.ItemID) " +
                     "when 3 then (select ItemCode from Biaya where ID = B.ItemID) end ItemCode, " +
                     "case B.ItemTypeID " +
                     "when 1 then (select ItemName from Inventory where ID = B.ItemID) " +
                     "when 2 then (select ItemName from Asset where ID = B.ItemID) " +
                     "when 3 then (select ItemName from Biaya where ID = B.ItemID) end ItemName, " +
                     "B.Quantity,D.UOMCode, " +
                     "CASE A.Status  WHEN 0 THEN ('Open')  " +
                     "WHEN 1 THEN ('Head')  ELSE " +
                     "('Gudang') END AS Status," +
                     "case when B.ItemID>0 and (select isnull(harga,0) from Inventory where ID=B.ItemID)=0 then " +
                     "(isnull((select top 1 Price from POPurchnDetail where ItemID = B.ItemID order by ID desc),0))  end Harga, B.Keterangan " +
                     "from Pakai  as A, PakaiDetail as B, Dept as C,UOM as D " +
                     "where A.ID = B.PakaiID and PakaiDate >='" + tgl1 + "' and PakaiDate <='" + tgl2 + "' and " +
                     "A.DeptID = C.ID  and A.status>-1 and B.rowstatus>-1 and D.ID = B.UomID and B.itemtypeID=" + itemtypeID + "  and B.ItemID = " + ItemID + " and A.DeptID =" + deptID;
        }
        public string ViewRekapPakaiDeptItem2(string tgl1, string tgl2, int ItemID, int deptID, int itemtypeID)
        {
            return "select A.PakaiNo,CONVERT(VARCHAR,A.PakaiDate,103) as PakaiDate, C.DeptName, " +
                     "case B.ItemTypeID " +
                     "when 1 then (select ItemCode from Inventory where ID = B.ItemID) " +
                     "when 2 then (select ItemCode from Asset where ID = B.ItemID) " +
                     "when 3 then (select ItemCode from Biaya where ID = B.ItemID) end ItemCode, " +
                     "case B.ItemTypeID " +
                     "when 1 then (select ItemName from Inventory where ID = B.ItemID) " +
                     "when 2 then (select ItemName from Asset where ID = B.ItemID) " +
                     "when 3 then (select ItemName from Biaya where ID = B.ItemID) end ItemName, " +
                     "B.Quantity,D.UOMCode, " +
                     "CASE A.Status  WHEN 0 THEN ('Open')  " +
                     "WHEN 1 THEN ('Head')  ELSE " +
                     "('Gudang') END AS Status," +
                     "case when B.ItemID>0 then (isnull((select top 1 Price from POPurchnDetail where ItemID = B.ItemID order by ID desc),0))  end Harga, B.Keterangan " +
                     "from Pakai  as A, PakaiDetail as B, Dept as C,UOM as D " +
                     "where A.ID = B.PakaiID and PakaiDate >='" + tgl1 + "' and PakaiDate <='" + tgl2 + "' and " +
                     "A.DeptID = C.ID  and A.status>-1 and B.rowstatus>-1 and D.ID = B.UomID and B.itemtypeID=" + itemtypeID + "  and B.ItemID = " + ItemID + " and A.DeptID =" + deptID;
        }
        public string ViewRekapPakaiDeptItemByPrice0(string tgl1, string tgl2, int ItemID, int deptID, int itemtypeID)
        {
            return "select A.PakaiNo,CONVERT(VARCHAR,A.PakaiDate,103) as PakaiDate, C.DeptName, " +
                     "case B.ItemTypeID " +
                     "when 1 then (select ItemCode from Inventory where ID = B.ItemID) " +
                     "when 2 then (select ItemCode from Asset where ID = B.ItemID) " +
                     "when 3 then (select ItemCode from Biaya where ID = B.ItemID) end ItemCode, " +
                     "case B.ItemTypeID " +
                     "when 1 then (select ItemName from Inventory where ID = B.ItemID) " +
                     "when 2 then (select ItemName from Asset where ID = B.ItemID) " +
                     "when 3 then (select ItemName from Biaya where ID = B.ItemID) end ItemName, " +
                     "B.Quantity,D.UOMCode, " +
                     "CASE A.Status  WHEN 0 THEN ('Open')  " +
                     "WHEN 1 THEN ('Head')  ELSE " +
                     "('Gudang') END AS Status," +
                     "0 as  Harga, B.Keterangan " +
                     "from Pakai  as A, PakaiDetail as B, Dept as C,UOM as D " +
                     "where A.ID = B.PakaiID and PakaiDate >='" + tgl1 + "' and PakaiDate <='" + tgl2 + "' and " +
                     "A.DeptID = C.ID and D.ID = B.UomID  and A.status>-1 and B.rowstatus>-1 and B.itemtypeID=" + itemtypeID + "  and B.ItemID = " + ItemID + " and A.DeptID =" + deptID;
        }
        public string ViewRekapPakaiDept(string tgl1, string tgl2, int deptID, int itemtypeID)
        {
            return "select A.PakaiNo,CONVERT(VARCHAR,A.PakaiDate,103) as PakaiDate,C.DeptName, " +
                     "case B.ItemTypeID " +
                     "when 1 then (select ItemCode from Inventory where ID = B.ItemID) " +
                     "when 2 then (select ItemCode from Asset where ID = B.ItemID) " +
                     "when 3 then (select ItemCode from Biaya where ID = B.ItemID) end ItemCode, " +
                     "case B.ItemTypeID " +
                     "when 1 then (select ItemName from Inventory where ID = B.ItemID) " +
                     "when 2 then (select ItemName from Asset where ID = B.ItemID) " +
                     "when 3 then (select ItemName from Biaya where ID = B.ItemID) end ItemName, " +
                     "B.Quantity,D.UOMCode,  " +
                     "CASE A.Status  WHEN 0 THEN ('Open')  " +
                     "WHEN 1 THEN ('Head')  ELSE " +
                     "('Gudang') END AS Status," +
                     "case when B.ItemID>0 and (select isnull(harga,0) from Inventory where ID=B.ItemID)=0 then (isnull((select top 1 Price from POPurchnDetail where ItemID = B.ItemID order by ID desc),0))  end Harga, B.Keterangan " +
                     "from Pakai  as A, PakaiDetail as B ,Dept as C,UOM as D " +
                     "where A.ID = B.PakaiID and PakaiDate >='" + tgl1 + "' and PakaiDate <='" + tgl2 + "' and " +
                     "A.DeptID = C.ID and D.ID = B.UomID  and A.status>-1 and B.rowstatus>-1  and B.itemtypeID=" + itemtypeID + "  and A.DeptID =" + deptID;
        }
        public string ViewRekapPakaiDept2(string tgl1, string tgl2, int deptID, int itemtypeID)
        {
            return "select A.PakaiNo,CONVERT(VARCHAR,A.PakaiDate,103) as PakaiDate,C.DeptName, " +
                     "case B.ItemTypeID " +
                     "when 1 then (select ItemCode from Inventory where ID = B.ItemID) " +
                     "when 2 then (select ItemCode from Asset where ID = B.ItemID) " +
                     "when 3 then (select ItemCode from Biaya where ID = B.ItemID) end ItemCode, " +
                     "case B.ItemTypeID " +
                     "when 1 then (select ItemName from Inventory where ID = B.ItemID) " +
                     "when 2 then (select ItemName from Asset where ID = B.ItemID) " +
                     "when 3 then (select ItemName from Biaya where ID = B.ItemID) end ItemName, " +
                     "B.Quantity,D.UOMCode,  " +
                     "CASE A.Status  WHEN 0 THEN ('Open')  " +
                     "WHEN 1 THEN ('Head')  ELSE " +
                     "('Gudang') END AS Status," +
                     "case when B.ItemID>0 then (isnull((select top 1 Price from POPurchnDetail where ItemID = B.ItemID order by ID desc),0))  end Harga, B.Keterangan " +
                     "from Pakai  as A, PakaiDetail as B ,Dept as C,UOM as D " +
                     "where A.ID = B.PakaiID and PakaiDate >='" + tgl1 + "' and PakaiDate <='" + tgl2 + "' and " +
                     "A.DeptID = C.ID and D.ID = B.UomID  and A.status>-1 and B.rowstatus>-1  and B.itemtypeID=" + itemtypeID + "  and A.DeptID =" + deptID;
        }
        public string ViewRekapPakaiDeptByPrice0(string tgl1, string tgl2, int deptID, int itemtypeID)
        {
            return "select A.PakaiNo,CONVERT(VARCHAR,A.PakaiDate,103) as PakaiDate,C.DeptName, " +
                     "case B.ItemTypeID " +
                     "when 1 then (select ItemCode from Inventory where ID = B.ItemID) " +
                     "when 2 then (select ItemCode from Asset where ID = B.ItemID) " +
                     "when 3 then (select ItemCode from Biaya where ID = B.ItemID) end ItemCode, " +
                     "case B.ItemTypeID " +
                     "when 1 then (select ItemName from Inventory where ID = B.ItemID) " +
                     "when 2 then (select ItemName from Asset where ID = B.ItemID) " +
                     "when 3 then (select ItemName from Biaya where ID = B.ItemID) end ItemName, " +
                     "B.Quantity,D.UOMCode,  " +
                     "CASE A.Status  WHEN 0 THEN ('Open')  " +
                     "WHEN 1 THEN ('Head')  ELSE " +
                     "('Gudang') END AS Status," +
                     "0 as  Harga, B.Keterangan " +
                     "from Pakai  as A, PakaiDetail as B ,Dept as C,UOM as D " +
                     "where A.ID = B.PakaiID and PakaiDate >='" + tgl1 + "' and PakaiDate <='" + tgl2 + "' and " +
                     "A.DeptID = C.ID and A.status>-1 and B.rowstatus>-1  and D.ID = B.UomID and B.itemtypeID=" + itemtypeID + "  and A.DeptID =" + deptID;
        }
        public string ViewRekapPakaiItem(string tgl1, string tgl2, int ItemID, int itemtypeID)
        {
            return "select A.PakaiNo,CONVERT(VARCHAR,A.PakaiDate,103) as PakaiDate, C.DeptName, " +
                     "case B.ItemTypeID " +
                     "when 1 then (select ItemCode from Inventory where ID = B.ItemID) " +
                     "when 2 then (select ItemCode from Asset where ID = B.ItemID) " +
                     "when 3 then (select ItemCode from Biaya where ID = B.ItemID) end ItemCode, " +
                     "case B.ItemTypeID " +
                     "when 1 then (select ItemName from Inventory where ID = B.ItemID) " +
                     "when 2 then (select ItemName from Asset where ID = B.ItemID) " +
                     "when 3 then (select ItemName from Biaya where ID = B.ItemID) end ItemName, " +
                     "B.Quantity,D.UOMCode,  " +
                     "CASE A.Status  WHEN 0 THEN ('Open')  " +
                     "WHEN 1 THEN ('Head')  ELSE " +
                     "('Gudang') END AS Status," +
                     "case when B.ItemID>0  and (select isnull(harga,0) from Inventory where ID=B.ItemID)=0 then (isnull((select top 1 Price from POPurchnDetail where ItemID = B.ItemID order by ID desc),0))  end Harga, B.Keterangan " +
                     "from Pakai  as A, PakaiDetail as B, Dept as C,UOM as D " +
                     "where A.ID = B.PakaiID and PakaiDate >='" + tgl1 + "' and PakaiDate <='" + tgl2 + "' and " +
                     "A.DeptID = C.ID  and A.status>-1 and B.rowstatus>-1 and D.ID = B.UomID and B.itemtypeID=" + itemtypeID + "  and B.ItemID =" + ItemID;
        }
        public string ViewRekapPakaiItem2(string tgl1, string tgl2, int ItemID, int itemtypeID)
        {
            return "select A.PakaiNo,CONVERT(VARCHAR,A.PakaiDate,103) as PakaiDate, C.DeptName, " +
                     "case B.ItemTypeID " +
                     "when 1 then (select ItemCode from Inventory where ID = B.ItemID) " +
                     "when 2 then (select ItemCode from Asset where ID = B.ItemID) " +
                     "when 3 then (select ItemCode from Biaya where ID = B.ItemID) end ItemCode, " +
                     "case B.ItemTypeID " +
                     "when 1 then (select ItemName from Inventory where ID = B.ItemID) " +
                     "when 2 then (select ItemName from Asset where ID = B.ItemID) " +
                     "when 3 then (select ItemName from Biaya where ID = B.ItemID) end ItemName, " +
                     "B.Quantity,D.UOMCode,  " +
                     "CASE A.Status  WHEN 0 THEN ('Open')  " +
                     "WHEN 1 THEN ('Head')  ELSE " +
                     "('Gudang') END AS Status," +
                     "case when B.ItemID>0 then (isnull((select top 1 Price from POPurchnDetail where ItemID = B.ItemID order by ID desc),0))  end Harga, B.Keterangan " +
                     "from Pakai  as A, PakaiDetail as B, Dept as C,UOM as D " +
                     "where A.ID = B.PakaiID and PakaiDate >='" + tgl1 + "' and PakaiDate <='" + tgl2 + "' and " +
                     "A.DeptID = C.ID  and A.status>-1 and B.rowstatus>-1 and D.ID = B.UomID and B.itemtypeID=" + itemtypeID + "  and B.ItemID =" + ItemID;
        }
        public string ViewRekapPakaiItemByPrice0(string tgl1, string tgl2, int ItemID, int itemtypeID)
        {

            return "select A.PakaiNo,CONVERT(VARCHAR,A.PakaiDate,103) as PakaiDate, C.DeptName, " +
                     "case B.ItemTypeID " +
                     "when 1 then (select ItemCode from Inventory where ID = B.ItemID) " +
                     "when 2 then (select ItemCode from Asset where ID = B.ItemID) " +
                     "when 3 then (select ItemCode from Biaya where ID = B.ItemID) end ItemCode, " +
                     "case B.ItemTypeID " +
                     "when 1 then (select ItemName from Inventory where ID = B.ItemID) " +
                     "when 2 then (select ItemName from Asset where ID = B.ItemID) " +
                     "when 3 then (select ItemName from Biaya where ID = B.ItemID) end ItemName, " +
                     "B.Quantity,D.UOMCode,  " +
                     "CASE A.Status  WHEN 0 THEN ('Open')  " +
                     "WHEN 1 THEN ('Head')  ELSE " +
                     "('Gudang') END AS Status," +
                     "0 as Harga, B.Keterangan " +
                     "from Pakai  as A, PakaiDetail as B, Dept as C,UOM as D " +
                     "where A.ID = B.PakaiID and PakaiDate >='" + tgl1 + "' and PakaiDate <='" + tgl2 + "' and " +
                     "A.DeptID = C.ID and A.status>-1 and B.rowstatus>-1  and D.ID = B.UomID and B.itemtypeID=" + itemtypeID + " and B.ItemID =" + ItemID;
        }
        public string ViewLapBarang(int dgItemTypeID, int valstock, int valgroup, int valaktif, string tipeBarang)
        {
            string cmdQuery = string.Empty;
            string cmdTipeBarang = string.Empty;
            if (valstock == 0 && valaktif == 0)
            {
                cmdQuery = " and " + "A.Jumlah > 0 and A.GroupID = " + valgroup + " and A.ItemTypeID = " + dgItemTypeID;
            }

            if (valstock == 0 && valaktif == 1)
            {
                cmdQuery = " and " + "A.Jumlah > 0 and A.Aktif = 1 and A.GroupID = " + valgroup + " and A.ItemTypeID = " + dgItemTypeID;
            }
            if (valstock == 0 && valaktif == 2)
            {
                cmdQuery = " and " + "A.Jumlah > 0 and A.Aktif = 0 and A.GroupID = " + valgroup + " and A.ItemTypeID = " + dgItemTypeID;
            }
            if (valstock == 1 && valaktif == 0)
            {
                cmdQuery = " and " + "A.Jumlah > 0 and A.Stock = 1 and A.GroupID = " + valgroup + " and A.ItemTypeID = " + dgItemTypeID;
            }
            if (valstock == 1 && valaktif == 1)
            {
                cmdQuery = " and " + "A.Jumlah > 0 and A.Aktif = 1 and A.Stock = 1 and A.GroupID = " + valgroup + " and A.ItemTypeID = " + dgItemTypeID;
            }
            if (valstock == 1 && valaktif == 2)
            {
                cmdQuery = " and " + "A.Jumlah > 0 and A.Aktif = 0 and A.Stock = 1 and A.GroupID = " + valgroup + " and A.ItemTypeID = " + dgItemTypeID;
            }

            if (valstock == 2 && valaktif == 0)
            {
                cmdQuery = " and " + "A.Jumlah > 0 and A.Stock = 0 and A.GroupID = " + valgroup + " and A.ItemTypeID = " + dgItemTypeID;
            }
            if (valstock == 2 && valaktif == 1)
            {
                cmdQuery = " and " + "A.Jumlah > 0 and A.Aktif = 1 and A.Stock = 0 and A.GroupID = " + valgroup + " and A.ItemTypeID = " + dgItemTypeID;
            }
            if (valstock == 2 && valaktif == 2)
            {
                cmdQuery = " and " + "A.Jumlah > 0 and A.Aktif = 0 and A.Stock = 0 and A.GroupID = " + valgroup + " and A.ItemTypeID = " + dgItemTypeID;
            }

            if (tipeBarang == "Inventory")
            {
                cmdTipeBarang = "from Inventory as A, UOM as B, GroupsPurchn as C ";
            }
            if (tipeBarang == "Asset")
            {
                cmdTipeBarang = "from Asset as A, UOM as B, GroupsPurchn as C ";
            }
            if (tipeBarang == "Biaya")
            {
                cmdTipeBarang = "from Biaya as A, UOM as B, GroupsPurchn as C ";
            }

            return "select A.ItemCode,A.ItemName,A.Jumlah,B.UOMCode,C.GroupDescription,jmltransit, " +
                   "case when A.ID > 1 then A.MaxStock ELSE 0 END StockMax, " +
                   "case when A.ID > 1 then A.MinStock ELSE 0 END StockMin, " +
                   "case when A.ID > 1 then A.ReOrder ELSE 0 END ReOrder,A.CreatedBy " + cmdTipeBarang +
                   "where A.UOMID = B.ID and A.GroupID = C.ID and A.RowStatus > -1 " + cmdQuery;
        }
        //public string ViewKartuStock(string tgl1, string tgl2, string itemid, string itemtypeid, string tglSA, string yearSA, string monthSA)
        //{
        //    string strSQL;
        //    strSQL = "SELECT * FROM (select 0 as Tipe, '0' as id,cast('" + tglSA + "' as DATE) as tanggal, '-' as Faktur," + monthSA + " as masuk,0 as keluar,'Saldo Awal' as keterangan from SaldoInventory where YearPeriod =" + yearSA + " and ItemID = '" + itemid + "' and ItemTypeID=" + itemtypeid +
        //    "union " +
        //    "SELECT 1 as Tipe,convert(char(8),Receipt.ReceiptDate ,112) + '1' + CAST(ReceiptDetail.ID as CHAR(10))as id, Receipt.ReceiptDate, Receipt.ReceiptNo, ReceiptDetail.Quantity AS masuk, 0 AS keluar, SuppPurch.SupplierName AS keterangan " +
        //    "FROM ReceiptDetail INNER JOIN Receipt ON ReceiptDetail.ReceiptID = Receipt.ID INNER JOIN SuppPurch ON Receipt.SupplierId = SuppPurch.ID " +
        //    "WHERE (ReceiptDetail.ItemTypeID = " + itemtypeid + ") AND (ReceiptDetail.ItemID = " + itemid + ") AND (ReceiptDetail.RowStatus >= 0) AND (Receipt.Status >= 0) AND (convert(varchar,Receipt.ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,Receipt.ReceiptDate,112) < '" + tgl2 + "') " +
        //    "union " +
        //    "SELECT 2 as Tipe,convert(char(8),Pakai.PakaiDate ,112) + '2' + CAST(PakaiDetail.ID as CHAR(10))as id,Pakai.PakaiDate, Pakai.PakaiNo, 0 as masuk,PakaiDetail.Quantity as keluar, Dept.DeptCode as keterangan " +
        //    "FROM PakaiDetail INNER JOIN Pakai ON PakaiDetail.PakaiID = Pakai.ID INNER JOIN Dept ON Pakai.DeptID = Dept.ID " +
        //    "WHERE (PakaiDetail.ItemTypeID = " + itemtypeid + ") AND (PakaiDetail.ItemID = " + itemid + ") AND (PakaiDetail.RowStatus >= 0) AND (Pakai.Status >= 2) AND (convert(varchar,Pakai.PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,Pakai.PakaiDate,112) < '" + tgl2 + "') " +
        //    "union " +
        //    "SELECT 3 as Tipe,CONVERT(char(8), Adjust.AdjustDate, 112) + '3'  + CAST(AdjustDetail.ID as CHAR(10))AS id, Adjust.AdjustDate, 'Adjust In -' +AdjustDetail.keterangan, AdjustDetail.Quantity AS masuk, 0 AS keluar,' ' as keterangan " +
        //    "FROM AdjustDetail INNER JOIN Adjust ON AdjustDetail.AdjustID = Adjust.ID " +
        //    "WHERE AdjustDetail.apv>0 and  (Adjust.AdjustType  = 'Tambah') and  (AdjustDetail.ItemTypeID = " + itemtypeid + ") AND (AdjustDetail.ItemID = " + itemid + ") AND (AdjustDetail.RowStatus >= 0) AND (Adjust.Status >= 0) AND (convert(varchar,adjust.adjustDate,112) >= '" + tgl1 + "' AND convert(varchar,adjust.adjustDate,112) <= '" + tgl2 + "') " +
        //    "union " +
        //    "SELECT 4 as Tipe,CONVERT(char(8), Adjust.AdjustDate, 112) + '4'  + CAST(AdjustDetail.ID as CHAR(10))AS id, Adjust.AdjustDate, 'Adjust Out -' + AdjustDetail.keterangan, 0 AS masuk, AdjustDetail.Quantity AS keluar,' ' as keterangan " +
        //    "FROM AdjustDetail INNER JOIN Adjust ON AdjustDetail.AdjustID = Adjust.ID " +
        //    "WHERE AdjustDetail.apv>0 and  (Adjust.AdjustType  = 'Kurang') and  (AdjustDetail.ItemTypeID = " + itemtypeid + ") AND (AdjustDetail.ItemID = " + itemid + ") AND (AdjustDetail.RowStatus >= 0) AND (Adjust.Status >= 0) AND (convert(varchar,adjust.adjustDate,112) >= '" + tgl1 + "' AND convert(varchar,adjust.adjustDate,112) < '" + tgl2 + "') " +
        //    "union " +
        //    "SELECT 5 as Tipe,CONVERT(char(8), returpakai.returDate, 112) + '5'  + CAST(returpakaiDetail.ID as CHAR(10))AS id, returpakai.returDate, returpakai.returNo,returpakaiDetail.Quantity AS masuk, 0 AS keluar,'Retur Pakai' as keterangan " +
        //    "FROM returpakaiDetail INNER JOIN returpakai ON returpakaiDetail.returID = returpakai.ID " +
        //    "WHERE (returpakaiDetail.ItemTypeID = " + itemtypeid + ") AND (returpakaiDetail.ItemID = " + itemid + ") AND (returpakaiDetail.RowStatus >= 0) AND (returpakai.Status >= 0) AND (convert(varchar,returpakai.returDate,112) >= '" + tgl1 + "' AND convert(varchar,returpakai.returDate,112) < '" + tgl2 + "') " +
        //        //"union select CONVERT(char(8), GETDATE(), 112) + '4'  + CAST(ID as CHAR(10))AS id, " +
        //        //"cast('" + tglSA + "' as DATE) as tanggal, ItemCode,0 AS masuk, JmlTransit AS keluar,'Ending Stock' as keterangan  " +
        //        //"from Inventory where ID = " + itemid + " and ItemTypeID= " + itemtypeid + " and RowStatus>-1 and JmlTransit>0 ";
        //    "UNION ALL " +
        //    "SELECT 6 as Tipe, CAST((convert(char(8),GETDATE() ,112) + '6' + (RIGHT('000000000'+RTRIM(CAST(pd.ItemID as CHAR)),10)))AS bigint) as id, cast('" + tglSA + "' as DATE) as tanggal," +
        //    "(select dbo.ItemCodeInv(itemid,pd.ItemTypeID)) ItemCode,0 AS masuk, SUM(Quantity) AS keluar,'Ending Stock -'+ p.PakaiNo as keterangan " +
        //    "FROM PakaiDetail pd LEFT JOIN pakai p on p.ID=pd.PakaiID "+
        //    "WHERE itemID= " + itemid + " and pd.ItemTypeID= " + itemtypeid + "  AND Status in(0,1) AND " +
        //    "RowStatus>-1 GROUP by ItemID,pd.ItemTypeID,p.PakaiNo ) as x order by Tanggal,Tipe ";
        //    return strSQL;
        //}
        public string ViewKartuStock(string tgl1, string tgl2, string itemid, string itemtypeid, string tglSA, string yearSA, string monthSA)
        {
            string strSQL;
            strSQL = "SELECT * FROM (select 0 as Tipe,1 as urut, '0' as id,cast('" + tglSA + "' as DATE) as tanggal, '-' as Faktur," + monthSA + " as masuk,0 as keluar,'Saldo Awal' as keterangan from SaldoInventory where YearPeriod =" + yearSA + " and ItemID = '" + itemid + "' and ItemTypeID=" + itemtypeid +
            "union " +
            
            "SELECT 1 as Tipe,1 as urut,convert(char(8),Receipt.ReceiptDate ,112) + '1' + CAST(ReceiptDetail.ID as CHAR(10))as id, " +
            "Receipt.ReceiptDate, Receipt.ReceiptNo, ReceiptDetail.Quantity AS masuk, 0 AS keluar, SuppPurch.SupplierName AS keterangan " +
            "FROM ReceiptDetail INNER JOIN Receipt ON ReceiptDetail.ReceiptID = Receipt.ID INNER JOIN SuppPurch ON Receipt.SupplierId = SuppPurch.ID " +
            "WHERE (ReceiptDetail.ItemTypeID = " + itemtypeid + ") AND (ReceiptDetail.ItemID = " + itemid + 
            ") AND (ReceiptDetail.RowStatus >= 0) AND (Receipt.Status >= 0) AND (convert(varchar,Receipt.ReceiptDate,112) >= '" + tgl1 + 
            "' AND convert(varchar,Receipt.ReceiptDate,112) < '" + tgl2 + "') " +
            "union " +
            "SELECT 1 as Tipe,1 as urut,CAST((convert(char(8),convertan.createdtime ,112) + '1' + " +
            "(RIGHT('000000000'+RTRIM(CAST(convertan.ID as CHAR)),10)))AS bigint) as id, convertan.createdtime, convertan.RepackNo,  " + 
            "convertan.toqty AS masuk, 0 AS keluar, '-' AS keterangan " +
            "FROM convertan WHERE (convertan.toItemID = " + itemid + 
            ") AND (convertan.RowStatus >= 0) AND (convert(varchar,convertan.createdtime,112) >= '" + tgl1 + 
            "' AND convert(varchar,convertan.createdtime,112) < '" + tgl2 + "') " +
            "union " +
            
            "SELECT 2 as Tipe,1 as urut,convert(char(8),Pakai.PakaiDate ,112) + '2' + CAST(PakaiDetail.ID as CHAR(10))as id,Pakai.PakaiDate, Pakai.PakaiNo, 0 as masuk,PakaiDetail.Quantity as keluar, Dept.DeptCode as keterangan " +
            "FROM PakaiDetail INNER JOIN Pakai ON PakaiDetail.PakaiID = Pakai.ID INNER JOIN Dept ON Pakai.DeptID = Dept.ID " +
            "WHERE (PakaiDetail.ItemTypeID = " + itemtypeid + ") AND (PakaiDetail.ItemID = " + itemid + ") AND (PakaiDetail.RowStatus >= 0) AND (Pakai.Status >= 3) AND (convert(varchar,Pakai.PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,Pakai.PakaiDate,112) < '" + tgl2 + "') " +
            "union " +
            "SELECT 3 as Tipe,1 as urut,CONVERT(char(8), Adjust.AdjustDate, 112) + '3'  + CAST(AdjustDetail.ID as CHAR(10))AS id, Adjust.AdjustDate, 'Adjust In -' +AdjustDetail.keterangan, AdjustDetail.Quantity AS masuk, 0 AS keluar,' ' as keterangan " +
            "FROM AdjustDetail INNER JOIN Adjust ON AdjustDetail.AdjustID = Adjust.ID " +
            "WHERE AdjustDetail.apv>0 and  (Adjust.AdjustType  = 'Tambah') and  (AdjustDetail.ItemTypeID = " + itemtypeid + ") AND (AdjustDetail.ItemID = " + itemid + ") AND (AdjustDetail.RowStatus >= 0) AND (Adjust.Status >= 0) AND (convert(varchar,adjust.adjustDate,112) >= '" + tgl1 + "' AND convert(varchar,adjust.adjustDate,112) <= '" + tgl2 + "') " +
            "union " +
            "SELECT 4 as Tipe,1 as urut,CONVERT(char(8), Adjust.AdjustDate, 112) + '4'  + CAST(AdjustDetail.ID as CHAR(10))AS id, Adjust.AdjustDate, 'Adjust Out -' + AdjustDetail.keterangan, 0 AS masuk, AdjustDetail.Quantity AS keluar,' ' as keterangan " +
            "FROM AdjustDetail INNER JOIN Adjust ON AdjustDetail.AdjustID = Adjust.ID " +
            "WHERE AdjustDetail.apv>0 AND (nonstok IS null OR nonstok != 1) and  (Adjust.AdjustType  = 'Kurang') and  (AdjustDetail.ItemTypeID = " + itemtypeid + ") AND (AdjustDetail.ItemID = " + itemid + ") AND (AdjustDetail.RowStatus >= 0) AND (Adjust.Status >= 0) AND (convert(varchar,adjust.adjustDate,112) >= '" + tgl1 + "' AND convert(varchar,adjust.adjustDate,112) < '" + tgl2 + "') " +
            "union " +
            "SELECT 5 as Tipe,1 as urut,CONVERT(char(8), returpakai.returDate, 112) + '5'  + CAST(returpakaiDetail.ID as CHAR(10))AS id, returpakai.returDate, returpakai.returNo,returpakaiDetail.Quantity AS masuk, 0 AS keluar,'Retur Pakai' as keterangan " +
            "FROM returpakaiDetail INNER JOIN returpakai ON returpakaiDetail.returID = returpakai.ID " +
            "WHERE (returpakaiDetail.ItemTypeID = " + itemtypeid + ") AND (returpakaiDetail.ItemID = " + itemid + ") AND (returpakaiDetail.RowStatus >= 0) AND (returpakai.Status >= 0) AND (convert(varchar,returpakai.returDate,112) >= '" + tgl1 + "' AND convert(varchar,returpakai.returDate,112) < '" + tgl2 + "') " +
                //"union select CONVERT(char(8), GETDATE(), 112) + '4'  + CAST(ID as CHAR(10))AS id, " +
                //"cast('" + tglSA + "' as DATE) as tanggal, ItemCode,0 AS masuk, JmlTransit AS keluar,'Ending Stock' as keterangan  " +
                //"from Inventory where ID = " + itemid + " and ItemTypeID= " + itemtypeid + " and RowStatus>-1 and JmlTransit>0 ";
            "UNION ALL " +
            "SELECT 6 as Tipe,2 as urut, CAST((convert(char(8),GETDATE() ,112) + '6' + (RIGHT('000000000'+RTRIM(CAST(pd.ItemID as CHAR)),10)))AS bigint) as id, cast('" + tglSA + "' as DATE) as tanggal," +
            "(select dbo.ItemCodeInv(itemid,pd.ItemTypeID)) ItemCode,0 AS masuk, SUM(Quantity) AS keluar,'Ending Stock -'+ p.PakaiNo as keterangan " +
            "FROM PakaiDetail pd LEFT JOIN pakai p on p.ID=pd.PakaiID " +
            "WHERE itemID= " + itemid + " and pd.ItemTypeID= " + itemtypeid + "  AND Status in(0,1,2) AND " +
            "RowStatus>-1 GROUP by ItemID,pd.ItemTypeID,p.PakaiNo ) as x order by Tanggal,Tipe ";
            return strSQL;
        }
        public string ViewKartuStockHarga(string tgl1, string tgl2, string itemid, string itemtypeid, string tglSA, string yearSA, string monthSA)
        {
            string strSQL;
            strSQL = "create table #adminksHarga(id varchar(100),tanggal datetime,faktur varchar(15),masuk decimal(18,2),keluar decimal(18,2),keterangan varchar(100),price decimal(18,2),lastprice decimal(18,2)) " +
                "declare @id varchar(100) declare @tanggal datetime declare @faktur varchar(15)declare @masuk decimal(18,2) " +
                "declare @keluar decimal(18,2) declare @price decimal(18,2) declare @lastprice decimal(18,2) declare @keterangan varchar(100) " +
                "declare kursor cursor for  " +
                "select * from (  " +
                "select '0' as id,cast('" + tglSA + "' as DATE) as tanggal, 'saldo awal' as Faktur," + monthSA + " as masuk,0 as keluar,'-' as keterangan," + monthSA.Substring(0, 3) + "AvgPrice  as price , " +
                monthSA.Substring(0, 3) + "AvgPrice  as LastPrice " +
                "from SaldoInventory where YearPeriod =" + yearSA + " and ItemID = " + itemid + " and ItemTypeID=" + itemtypeid + " " +
                "union " +
                "SELECT convert(char(8),Receipt.ReceiptDate ,112) + '1' + CAST(ReceiptDetail.ID as CHAR(10))as id, Receipt.ReceiptDate, " +
                "Receipt.ReceiptNo, ReceiptDetail.Quantity AS masuk, 0 AS keluar, SuppPurch.SupplierName AS keterangan,ReceiptDetail.Price, " +
                "case when Itemid>0 then (select top 1 price from receiptdetail A where A.ItemID = ReceiptDetail.ItemID  and A.receiptID in  " +
                "(select id from receipt B where B.ReceiptDate < Receipt.ReceiptDate ) order by id desc ) end LastPrice " +
                "FROM ReceiptDetail INNER JOIN Receipt ON ReceiptDetail.ReceiptID = Receipt.ID INNER JOIN SuppPurch ON Receipt.SupplierId = SuppPurch.ID " +
                "WHERE (ReceiptDetail.ItemTypeID=" + itemtypeid + " ) AND (ReceiptDetail.ItemID =  " + itemid + ") AND (ReceiptDetail.RowStatus >= 0) AND (Receipt.Status >= 0) " +
                "AND (convert(varchar,Receipt.ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,Receipt.ReceiptDate,112) < '" + tgl2 + "') " +
                "union " +
                "SELECT convert(char(8),Pakai.PakaiDate ,112) + '2' + CAST(PakaiDetail.ID as CHAR(10))as id,Pakai.PakaiDate, Pakai.PakaiNo, 0 as masuk, " +
                "PakaiDetail.Quantity as keluar, Dept.DeptCode as keterangan,PakaiDetail.AvgPrice as Price, " +
                "case when Itemid>0 then (select top 1 price from receiptdetail A where A.ItemID = PakaiDetail.ItemID  and A.receiptID in " +
                "(select id from receipt B where B.ReceiptDate < Pakai.PakaiDate ) order by id desc ) end LastPrice " +
                "FROM PakaiDetail INNER JOIN Pakai ON PakaiDetail.PakaiID = Pakai.ID INNER JOIN Dept ON Pakai.DeptID = Dept.ID " +
                "WHERE (PakaiDetail.ItemTypeID=" + itemtypeid + " ) AND (PakaiDetail.ItemID =  " + itemid + ") AND (PakaiDetail.RowStatus >= 0) AND (Pakai.Status >= 3) AND " +
                "(convert(varchar,Pakai.PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,Pakai.PakaiDate,112) < '" + tgl2 + "') " +
                "union " +
                "SELECT CONVERT(char(8), Adjust.AdjustDate, 112) + '3'  + CAST(AdjustDetail.ID as CHAR(10))AS id, Adjust.AdjustDate, " +
                "'Adjust In ' +AdjustDetail.keterangan, AdjustDetail.Quantity AS masuk, 0 AS keluar,' ' as keterangan, " +
                "case when  AdjustDetail.ItemID >0 then (select junavgprice from SaldoInventory where  YearPeriod =2013 and ItemID = AdjustDetail.ItemID and ItemTypeID=1) end price, " +
                "case when Itemid>0 then (select top 1 price from receiptdetail A where A.ItemID = AdjustDetail.ItemID  and A.receiptID in " +
                "(select id from receipt B where B.ReceiptDate < Adjust.adjustDate ) order by id desc ) end LastPrice " +
                "FROM AdjustDetail INNER JOIN Adjust ON AdjustDetail.AdjustID = Adjust.ID " +
                "WHERE  AdjustDetail.apv>0 and (Adjust.AdjustType  = 'Tambah') and  (AdjustDetail.ItemTypeID=" + itemtypeid + " ) AND (AdjustDetail.ItemID =  " + itemid + ") AND (AdjustDetail.RowStatus >= 0) AND " +
                "(Adjust.Status >= 0) AND (convert(varchar,adjust.adjustDate,112) >= '" + tgl1 + "' AND convert(varchar,adjust.adjustDate,112) <= '" + tgl2 + "') " +
                "union " +
                "SELECT CONVERT(char(8), Adjust.AdjustDate, 112) + '4'  + CAST(AdjustDetail.ID as CHAR(10))AS id, Adjust.AdjustDate, " +
                "'Adjust Out ' + AdjustDetail.keterangan, 0 AS masuk, AdjustDetail.Quantity AS keluar,' ' as keterangan ," +
                "case when  AdjustDetail.ItemID >0 then (select junavgprice from SaldoInventory where  YearPeriod =2013 and ItemID = AdjustDetail.ItemID and ItemTypeID=1) end price, " +
                "case when Itemid>0 then (select top 1 price from receiptdetail A where A.ItemID = AdjustDetail.ItemID  and A.receiptID in " +
                "(select id from receipt B where B.ReceiptDate < Adjust.adjustDate ) order by id desc ) end LastPrice " +
                "FROM AdjustDetail INNER JOIN Adjust ON AdjustDetail.AdjustID = Adjust.ID " +
                "WHERE  AdjustDetail.apv>0 and (Adjust.AdjustType  = 'Kurang') and  (AdjustDetail.ItemTypeID=" + itemtypeid + " ) AND (AdjustDetail.ItemID =  " + itemid + ") AND (AdjustDetail.RowStatus >= 0) AND " +
                "(Adjust.Status >= 0) AND (convert(varchar,adjust.adjustDate,112) >= '" + tgl1 + "' AND convert(varchar,adjust.adjustDate,112) < '" + tgl2 + "') " +
                "union " +
                "SELECT CONVERT(char(8), returpakai.returDate, 112) + '4'  + CAST(returpakaiDetail.ID as CHAR(10))AS id, returpakai.returDate, " +
                "returpakai.returNo,returpakaiDetail.Quantity AS masuk, 0 AS keluar,'retur pakai' as keterangan ," +
                "case when  returpakaiDetail.ItemID >0 then (select junavgprice from SaldoInventory where  YearPeriod =2013 and ItemID = returpakaiDetail.ItemID and ItemTypeID=1) end price, " +
                "case when Itemid>0 then (select top 1 price from receiptdetail A where A.ItemID = returpakaiDetail.ItemID  and A.receiptID in " +
                "(select id from receipt B where B.ReceiptDate < returpakai.returDate ) order by id desc ) end LastPrice " +
                "FROM returpakaiDetail INNER JOIN returpakai ON returpakaiDetail.returID = returpakai.ID " +
                "WHERE (returpakaiDetail.ItemTypeID=" + itemtypeid + " ) AND (returpakaiDetail.ItemID =  " + itemid + ") AND (returpakaiDetail.RowStatus >= 0) AND (returpakai.Status >= 0) AND " +
                "(convert(varchar,returpakai.returDate,112) >= '" + tgl1 + "' AND convert(varchar,returpakai.returDate,112) < '" + tgl2 + "') " +
                "union " +
                "select CONVERT(char(8), GETDATE(), 112) + '4'  + CAST(ID as CHAR(10))AS id, " +
                "cast('" + tgl1 + "' as DATE) as tanggal, ItemCode,0 AS masuk, JmlTransit AS keluar,'Ending Stock' as keterangan," +
                "case when  Inventory.ID >0 then (select junavgprice from SaldoInventory where  YearPeriod =2013 and ItemID = Inventory.ID and ItemTypeID=1) end price, " +
                "case when ID>0 then (select top 1 price from receiptdetail A where A.ItemID = Inventory.ID  and A.receiptID in " +
                "(select id from receipt B where convert(varchar,B.ReceiptDate,112) < '" + tgl2 + "' ) order by id desc ) end LastPrice " +
                "from Inventory where ID =  " + itemid + " and ItemTypeID= 1  and RowStatus>-1 and JmlTransit>0 " +
                ") as A order by id  " +
                "open kursor " +
                "FETCH NEXT FROM kursor " +
                "INTO @id,@tanggal,@faktur ,@masuk ,@keluar,@keterangan,@price,@lastprice " +
                "WHILE @@FETCH_STATUS = 0 " +
                "begin " +
                "    insert into #adminksHarga(id,tanggal,faktur ,masuk ,keluar,keterangan,price,lastprice)values(@id,@tanggal,@faktur ,@masuk ,@keluar,@keterangan,@price,@lastprice) " +
                "    FETCH NEXT FROM kursor	INTO @id,@tanggal,@faktur ,@masuk ,@keluar,@keterangan,@price,@lastprice " +
                "END " +
                "CLOSE kursor " +
                "DEALLOCATE kursor " +
                "select id,tanggal,faktur ,masuk ,keluar,case when masuk>0 then (price) else avgprice end price,keterangan,lastprice,lastStock,avgprice from( " +
                "select id,tanggal,faktur ,masuk ,keluar,keterangan,price,lastprice,lastStock,case when avgprice=0 then ( " +
                "select top 1 avgprice from (select * from (select *, case when masuk>0 then ((((lastStock)*lastprice)+(masuk*price))/(lastStock+masuk)) else 0 end avgprice from ( " +
                "select *,case when id<>'' then (select isnull(SUM(masuk)-SUM(keluar),0) from #adminksHarga where id <A1.id ) end lastStock from #adminksHarga A1) as B1 " +
                ") as C1) as D where  avgprice>0 and D.id<C.id  order by id desc ) else avgprice end avgprice " +
                "from ( " +
                "select *, case when masuk>0 then ((((lastStock)*lastprice)+(masuk*price))/(lastStock+masuk)) else 0 end avgprice from ( " +
                "select *,case when id<>'' then (select isnull(SUM(masuk)-SUM(keluar),0) from #adminksHarga where id <A.id ) end lastStock from #adminksHarga A) as B) as C) as E " +
                "drop table #adminksHarga";
            return strSQL;
        }
        public string ViewKartuStockRepack(string tgl1, string tgl2, string itemid, string itemtypeid, string tglSA, string yearSA, string monthSA)
        {
            string strSQL;
            strSQL =
            "select 0 as Tipe,1 as urut,'0' as id,cast('" + tglSA + "' as DATE) as tanggal, '-' as Faktur," + monthSA + " as masuk,0 as keluar,'saldo awal' as keterangan from SaldoInventory where YearPeriod =" + yearSA + " and ItemID = '" + itemid + "' and ItemTypeID=" + itemtypeid +
            "union " +
            "SELECT 1 as Tipe,1 as urut,CAST((convert(char(8),convertan.createdtime ,112) + '1' + (RIGHT('000000000'+RTRIM(CAST(convertan.ID as CHAR)),10)))AS bigint) as id, convertan.createdtime, convertan.RepackNo, convertan.toqty AS masuk, 0 AS keluar, '-' AS keterangan " +
            "FROM convertan " +
            "WHERE (convertan.toItemID = " + itemid + ") AND (convertan.RowStatus >= 0) AND (convert(varchar,convertan.createdtime,112) >= '" + tgl1 + "' AND convert(varchar,convertan.createdtime,112) < '" + tgl2 + "') " +
            "union " +
            "SELECT 2 as Tipe,1 as urut,CAST((convert(char(8),Pakai.PakaiDate ,112) + '2' + (RIGHT('000000000'+RTRIM(CAST(PakaiDetail.ID as CHAR)),10)))AS bigint) as id,Pakai.PakaiDate, Pakai.PakaiNo, 0 as masuk,PakaiDetail.Quantity as keluar, Dept.DeptCode as keterangan " +
            "FROM PakaiDetail INNER JOIN Pakai ON PakaiDetail.PakaiID = Pakai.ID INNER JOIN Dept ON Pakai.DeptID = Dept.ID " +
            "WHERE (PakaiDetail.ItemTypeID = " + itemtypeid + ") AND (PakaiDetail.ItemID = " + itemid + ") AND (PakaiDetail.RowStatus >= 0) AND (Pakai.Status >= 3) AND (convert(varchar,Pakai.PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,Pakai.PakaiDate,112) < '" + tgl2 + "') " +
            "union " +
            "SELECT 3 as Tipe,1 as urut,CAST((convert(char(8),Adjust.AdjustDate ,112) + '3' + (RIGHT('000000000'+RTRIM(CAST(AdjustDetail.ID as CHAR)),10)))AS bigint) as id, Adjust.AdjustDate, 'Adjust In ' +AdjustDetail.keterangan, AdjustDetail.Quantity AS masuk, 0 AS keluar,' ' as keterangan " +
            "FROM AdjustDetail INNER JOIN Adjust ON AdjustDetail.AdjustID = Adjust.ID " +
            "WHERE  AdjustDetail.apv>0 and (Adjust.AdjustType  = 'Tambah') and  (AdjustDetail.ItemTypeID = " + itemtypeid + ") AND (AdjustDetail.ItemID = " + itemid + ") AND (AdjustDetail.RowStatus >= 0) AND (Adjust.Status >= 0) AND (convert(varchar,adjust.adjustDate,112) >= '" + tgl1 + "' AND convert(varchar,adjust.adjustDate,112) <= '" + tgl2 + "') " +
            "union " +
            "SELECT 4 as Tipe,1 as urut,CAST((convert(char(8),Adjust.AdjustDate ,112) + '4' + (RIGHT('000000000'+RTRIM(CAST(AdjustDetail.ID as CHAR)),10)))AS bigint) as id, Adjust.AdjustDate, 'Adjust Out ' + AdjustDetail.keterangan, 0 AS masuk, AdjustDetail.Quantity AS keluar,' ' as keterangan " +
            "FROM AdjustDetail INNER JOIN Adjust ON AdjustDetail.AdjustID = Adjust.ID " +
            "WHERE  AdjustDetail.apv>0 and (Adjust.AdjustType  = 'Kurang') and  (AdjustDetail.ItemTypeID = " + itemtypeid + ") AND (AdjustDetail.ItemID = " + itemid + ") AND (AdjustDetail.RowStatus >= 0) AND (Adjust.Status >= 0) AND (convert(varchar,adjust.adjustDate,112) >= '" + tgl1 + "' AND convert(varchar,adjust.adjustDate,112) < '" + tgl2 + "') " +
            "union " +
            "SELECT 5 as Tipe,1 as  urut,CAST((convert(char(8),returpakai.returDate ,112) + '5' + (RIGHT('000000000'+RTRIM(CAST(returpakaiDetail.ID as CHAR)),10)))AS bigint) as id, returpakai.returDate, returpakai.returNo,returpakaiDetail.Quantity AS masuk, 0 AS keluar,'retur pakai' as keterangan " +
            "FROM returpakaiDetail INNER JOIN returpakai ON returpakaiDetail.returID = returpakai.ID " +
            "WHERE (returpakaiDetail.ItemTypeID = " + itemtypeid + ") AND (returpakaiDetail.ItemID = " + itemid + ") AND (returpakaiDetail.RowStatus >= 0) AND (returpakai.Status >= 0) AND (convert(varchar,returpakai.returDate,112) >= '" + tgl1 + "' AND convert(varchar,returpakai.returDate,112) < '" + tgl2 + "') " +
            //"union select CONVERT(char(8), GETDATE(), 112) + '4'  + CAST(ID as CHAR(10))AS id, " +
            //"cast('" + tglSA + "' as DATE) as tanggal, ItemCode,0 AS masuk, JmlTransit AS keluar,'Ending Stock' as keterangan  " +
            //"from Inventory where ID = " + itemid + " and ItemTypeID= " + itemtypeid + " and RowStatus>-1 and JmlTransit>0 ";
            "UNION ALL " +
            "SELECT 6 as Tipe,1 as urut, CAST((convert(char(8),GETDATE() ,112) + '6' + (RIGHT('000000000'+RTRIM(CAST(PakaiDetail.ItemID as CHAR)),10)))AS bigint) as id, cast('" + tglSA + "' as DATE) as tanggal," +
            "(select dbo.ItemCodeInv(itemid,ItemTypeID)) ItemCode,0 AS masuk, SUM(Quantity) AS keluar,'Ending Stock' as keterangan " +
            "FROM PakaiDetail WHERE itemID= " + itemid + " and ItemTypeID= " + itemtypeid + " and PakaiID in(" +
            "SELECT ID FROM Pakai WHERE LEFT(convert(varchar,PakaiDate,112),6)='" + yearSA + tglSA.Substring(0, 2) + "' AND Status BETWEEN 0 AND 1) AND " +
            "RowStatus>-1 GROUP by ItemID,ItemTypeID ";
            return strSQL;
        }
        public string ViewDeathStock(string BadStock, string GroupID)
        {
            string strSQL;
            #region nonaktif line
            //string strstock = string.Empty;
            //switch (stock )
            //    {
            //    case "All":
            //        strstock = " ";
            //        break;
            //    case "Stok":
            //        strstock = " and SUBSTRING(itemcode,7,1)='9'";
            //        break;
            //    case "Non Stok":
            //        strstock = " and SUBSTRING(itemcode,7,1)='0'";
            //        break;
            //    }
            #endregion
            #region old Query tidak di pakai
            if (BadStock == "YES")
            {
                strSQL = "select  Uomdesc as satuan,ID,GroupID,ItemCode,itemname,isnull(badstock,0) as flag,lastreceipt,lastPrice,CRC,lastPakai,Jumlah as stock, " +
                    "DATEDIFF(DAY,startcount,getdate())/convert(decimal(9,4),30) as ltimecrnt from ( " +
	                "    select Uomdesc,ID,GroupID,ItemCode,itemname,badstock,lastreceipt,lastPrice,CRC,lastPakai,Jumlah,  " +
	                "    case when isnull(M.lastPakai,'1/1/1900')= '1/1/1900' then lastreceipt  " +
	                "    else case when (lastreceipt>M.lastPakai )then lastreceipt else M.lastPakai end end startcount   from ( " +
		            "        select Uomdesc,I.ID,GroupID,ItemCode,itemname,badstock,case when I.ID>0 then (select top 1 A.ReceiptDate from Receipt A, ReceiptDetail B  " +
		            "        where A.ID=B.ReceiptID and B.ItemID=I.ID and B.GroupID=I.GroupID and B.RowStatus>-1  order by A.ReceiptDate desc) end lastreceipt, " +
		            "        case when I.ID>0 then (select top 1 B.Price from Receipt A, ReceiptDetail B  " +
		            "        where A.ID=B.ReceiptID and B.ItemID=I.ID and B.GroupID=I.GroupID and B.RowStatus>-1  order by A.ReceiptDate desc) end lastPrice, " +
		            "        case when I.ID>0 then (select top 1 D.Nama  from Receipt A, ReceiptDetail B ,POPurchn C,MataUang D " +
		            "        where A.ID=B.ReceiptID and B.ItemID=I.ID and B.POID=C.ID and C.Crc=D.ID and B.GroupID=I.GroupID and B.RowStatus>-1  order by A.ReceiptDate desc) end CRC, " +
		            "        case when I.ID>0 then (select top 1 A.PakaiDate from Pakai A, PakaiDetail B  " +
		            "        where A.ID=B.PakaiID and B.ItemID=I.ID and B.GroupID=I.GroupID and B.RowStatus>-1 order by A.PakaiDate desc ) end lastPakai,Jumlah    " +
		            "        from Inventory I,UOM U where I.uomID=U.ID and /*SUBSTRING(I.itemcode,7,1)='0'*/ I.stock=0 and i.aktif=1 and I.Jumlah>0 " +
	                "    ) as M  " +
                    ") as MM  " +
                    "where MM.startcount<DATEADD(month,-3,getdate())  and MM.GroupID=" + GroupID;
            }
            else
            {
                strSQL = "select  Uomdesc as satuan,ID,GroupID,ItemCode,itemname,isnull(badstock,0) as flag,lastreceipt,lastPrice,CRC,lastPakai,Jumlah as stock, " +
                    "DATEDIFF(DAY,startcount,getdate())/convert(decimal(9,4),30) as ltimecrnt from ( " +
	                "    select Uomdesc,ID,GroupID,ItemCode,itemname,badstock,lastreceipt,lastPrice,CRC,lastPakai,Jumlah,  " +
	                "    case when isnull(M.lastPakai,'1/1/1900')= '1/1/1900' then lastreceipt  " +
	                "    else case when (lastreceipt>M.lastPakai )then lastreceipt else M.lastPakai end end startcount   from ( " +
		            "        select Uomdesc,I.ID,GroupID,ItemCode,itemname,badstock,case when I.ID>0 then (select top 1 A.ReceiptDate from Receipt A, ReceiptDetail B  " +
		            "        where A.ID=B.ReceiptID and B.ItemID=I.ID and B.GroupID=I.GroupID and B.RowStatus>-1  order by A.ReceiptDate desc) end lastreceipt, " +
		            "        case when I.ID>0 then (select top 1 B.Price from Receipt A, ReceiptDetail B  " +
		            "        where A.ID=B.ReceiptID and B.ItemID=I.ID and B.GroupID=I.GroupID and B.RowStatus>-1  order by A.ReceiptDate desc) end lastPrice, " +
		            "        case when I.ID>0 then (select top 1 D.Nama  from Receipt A, ReceiptDetail B ,POPurchn C,MataUang D " +
		            "        where A.ID=B.ReceiptID and B.ItemID=I.ID and B.POID=C.ID and C.Crc=D.ID and B.GroupID=I.GroupID and B.RowStatus>-1  order by A.ReceiptDate desc) end CRC, " +
		            "        case when I.ID>0 then (select top 1 A.PakaiDate from Pakai A, PakaiDetail B  " +
		            "        where A.ID=B.PakaiID and B.ItemID=I.ID and B.GroupID=I.GroupID and B.RowStatus>-1 order by A.PakaiDate desc ) end lastPakai,Jumlah    " +
		            "        from Inventory I,UOM U where I.uomID=U.ID and /*SUBSTRING(I.itemcode,7,1)='0'*/ I.stock=0 and i.aktif=1 and I.Jumlah>0 " +
	                "    ) as M  " +
                    ") as MM  " +
                    "where MM.startcount>DATEADD(month,-3,getdate())  and MM.GroupID=" + GroupID;
            }
            #endregion
            
            return strSQL;
        }
        public string ViewDeathStock(string BadStock, string GroupID, string pilih)
        {
            string strSQL = string.Empty;
            #region new query by beny
            if (BadStock == "YES")
            {
                strSQL = "select  Uomdesc as satuan,ID,GroupID,ItemCode,itemname,isnull(badstock,0) as flag,lastreceipt,lastPrice,CRC,lastPakai,lastAdjust,Jumlah as stock, " +
                    "DATEDIFF(DAY,startcount,getdate())/convert(decimal(9,4),30) as ltimecrnt from ( " +
                    "    select Uomdesc,ID,GroupID,ItemCode,itemname,badstock,lastreceipt,lastPrice,CRC,lastPakai,lastAdjust,Jumlah,  " +
                    "        case when (lastreceipt is null and M.lastPakai is null) then lastAdjust " +
                    "        else case when (lastreceipt>lastPakai and lastreceipt>lastAdjust  )then lastreceipt  " +
                    "        else case when (lastAdjust>lastreceipt and  lastAdjust>lastPakai)then lastAdjust " +
                    "        else case when (lastPakai>lastreceipt and  lastPakai>lastAdjust)then lastPakai " +
                    "        else case when (lastAdjust is null and lastPakai is null)then lastreceipt " +
                    "        else case when (lastPakai>lastreceipt and lastAdjust is null)then lastPakai " +
                    "        else case when (lastreceipt>lastPakai and lastAdjust is null)then lastreceipt " +
                    "        else case when (lastPakai is null and lastreceipt>lastAdjust)then lastreceipt " +
                    "        else case when (lastreceipt is null and lastPakai>lastAdjust)then lastPakai " +
                    "        end end end end end end end end end startcount  from (" +
                    "        select Uomdesc,I.ID,GroupID,ItemCode,itemname,badstock,case when I.ID>0 then (select top 1 A.ReceiptDate from Receipt A, ReceiptDetail B  " +
                    "        where A.ID=B.ReceiptID and B.ItemID=I.ID and B.GroupID=I.GroupID and B.RowStatus>-1  order by A.ReceiptDate desc) end lastreceipt, " +
                    "        case when I.ID>0 then (select top 1 B.Price from Receipt A, ReceiptDetail B  " +
                    "        where A.ID=B.ReceiptID and B.ItemID=I.ID and B.GroupID=I.GroupID and B.RowStatus>-1  order by A.ReceiptDate desc) end lastPrice, " +
                    "        case when I.ID>0 then (select top 1 D.Nama  from Receipt A, ReceiptDetail B ,POPurchn C,MataUang D " +
                    "        where A.ID=B.ReceiptID and B.ItemID=I.ID and B.POID=C.ID and C.Crc=D.ID and B.GroupID=I.GroupID and B.RowStatus>-1  order by A.ReceiptDate desc) end CRC, " +
                    "        case when I.ID>0 then (select top 1 A.PakaiDate from Pakai A, PakaiDetail B  " +
                    "        where A.ID=B.PakaiID and B.ItemID=I.ID and B.GroupID=I.GroupID and B.RowStatus>-1 order by A.PakaiDate desc ) end lastPakai,Jumlah,    " +
                    "        case when I.ID>0 then (select top 1 A.AdjustDate from Adjust A, AdjustDetail B where A.ID=B.AdjustID and B.ItemID=I.ID and B.GroupID=I.GroupID " +
                    "        and B.RowStatus>-1 order by A.AdjustDate desc ) end lastAdjust " +
                    "        from Inventory I,UOM U where I.uomID=U.ID and /*SUBSTRING(I.itemcode,7,1)='0'*/ I.stock=" + pilih + " and i.aktif=1 and I.Jumlah>0 " +
                    "    ) as M  " +
                    ") as MM  " +
                    "where MM.startcount<DATEADD(month,-3,getdate())  and MM.GroupID= " + GroupID;
            }
            else
            {
                strSQL = "select  Uomdesc as satuan,ID,GroupID,ItemCode,itemname,isnull(badstock,0) as flag,lastreceipt,lastPrice,CRC,lastPakai,Jumlah as stock, " +
                    "DATEDIFF(DAY,startcount,getdate())/convert(decimal(9,4),30) as ltimecrnt from ( " +
                    "    select Uomdesc,ID,GroupID,ItemCode,itemname,badstock,lastreceipt,lastPrice,CRC,lastPakai,Jumlah,  " +
                    "    case when isnull(M.lastPakai,'1/1/1900')= '1/1/1900' then lastreceipt  " +
                    "    else case when (lastreceipt>M.lastPakai )then lastreceipt else M.lastPakai end end startcount   from ( " +
                    "        select Uomdesc,I.ID,GroupID,ItemCode,itemname,badstock,case when I.ID>0 then (select top 1 A.ReceiptDate from Receipt A, ReceiptDetail B  " +
                    "        where A.ID=B.ReceiptID and B.ItemID=I.ID and B.GroupID=I.GroupID and B.RowStatus>-1  order by A.ReceiptDate desc) end lastreceipt, " +
                    "        case when I.ID>0 then (select top 1 B.Price from Receipt A, ReceiptDetail B  " +
                    "        where A.ID=B.ReceiptID and B.ItemID=I.ID and B.GroupID=I.GroupID and B.RowStatus>-1  order by A.ReceiptDate desc) end lastPrice, " +
                    "        case when I.ID>0 then (select top 1 D.Nama  from Receipt A, ReceiptDetail B ,POPurchn C,MataUang D " +
                    "        where A.ID=B.ReceiptID and B.ItemID=I.ID and B.POID=C.ID and C.Crc=D.ID and B.GroupID=I.GroupID and B.RowStatus>-1  order by A.ReceiptDate desc) end CRC, " +
                    "        case when I.ID>0 then (select top 1 A.PakaiDate from Pakai A, PakaiDetail B  " +
                    "        where A.ID=B.PakaiID and B.ItemID=I.ID and B.GroupID=I.GroupID and B.RowStatus>-1 order by A.PakaiDate desc ) end lastPakai,Jumlah    " +
                    "        from Inventory I,UOM U where I.uomID=U.ID and /*SUBSTRING(I.itemcode,7,1)='0'*/ I.stock=" + pilih + " and i.aktif=1 and I.Jumlah>0 " +
                    "    ) as M  " +
                    ") as MM  " +
                    "where MM.startcount>DATEADD(month,-3,getdate())  and MM.GroupID= " + GroupID;
            }
            #endregion
            return strSQL;
        }
        public string ViewLJTempoBayar(string tgl1, string tgl2, int type)
        {
            string kriteria = string.Empty;
            if (type == 2)
                kriteria = " where  convert(char(8),A.approveDate ,112)>= " + tgl1 + " and convert(char(8),A.approveDate ,112)<='" + tgl2 + "'";
            if (type == 1)
                kriteria = " where  convert(char(8),C.popurchndate ,112)>= '" + tgl1 + "' and convert(char(8),C.popurchndate ,112)<='" + tgl2 + "'";
            if (type == 0)
                kriteria = " where convert(char(8),A.ReceiptDate ,112)>= " + tgl1 + " and convert(char(8),A.ReceiptDate ,112)<='" + tgl2 + "'";
            string strSQL;
            strSQL = "SELECT A.PONo, A.ReceiptNo, A.kurspajak,A.ReceiptDate , " +
            "case A.ItemTypeID when 1 then (select itemcode from Inventory where id=B.ItemID ) " +
            "	when 2 then (select itemcode from Asset where id=B.ItemID ) " +
            "	when 3 then (select itemcode from Biaya where id=B.ItemID ) end itemcode, " +
            "case A.ItemTypeID when 1 then (select itemname from Inventory where id=B.ItemID ) " +
            "	when 2 then (select itemname from Asset where id=B.ItemID ) " +
            "	when 3 then " + ItemSPPBiayaNew("D") + " end itemname,	 " +
            " case when C.SupplierID>0 then (select SupplierName  from SuppPurch where ID=C.SupplierID ) end Supplier, " +
            "B.Quantity, D.Price, (B.Quantity * D.Price) as total,(((B.Quantity * D.Price)-(B.Quantity * D.Price * C.Disc/100)) * C.PPN/100 ) as PPN," +
            "(B.Quantity * D.Price * C.PPH/100 ) as PPH,C.Ongkos as ongkir,(B.Quantity * D.Price * C.Disc/100)as diskon," +
            "(B.Quantity * D.Price)+(((B.Quantity * D.Price)-(B.Quantity * D.Price * C.Disc/100)) * C.PPN/100 )+(B.Quantity * D.Price * C.PPH/100 )-" +
            "(B.Quantity * D.Price * C.Disc/100)  as totalbayar, A.TTagihanDate, A.JTempoDate, A.FakturPajak," +
            "cast((GETDATE()-A.TTagihanDate) as decimal)  as umur,invoiceno,fakturpajakdate,approvedate as tglapprove " +
            "FROM POPurchnDetail AS D LEFT OUTER JOIN POPurchn AS C ON D.POID = C.ID " +
            "LEFT OUTER JOIN Receipt AS A INNER JOIN ReceiptDetail AS B ON A.ID = B.ReceiptID ON D.ID = B.PODetailID " +
            kriteria + " and A.status<=1 and A.status>-1 and B.rowstatus>-1";
            return strSQL;
        }
        public string ViewLPPnBM(string tgl1, string tgl2)
        {
            string kriteria = string.Empty;
            kriteria = " where  convert(char(8),tanggalFakturDokumen ,112)>= " + tgl1 + " and convert(char(8),tanggalFakturDokumen ,112)<='" + tgl2 + "'";
            string strSQL;
            strSQL = "select * ,DPP*10/100 as PPN,0 as PPnBM from ( " +
                "SELECT 'B' AS kodepajak, 2 AS kodetransaksi, 1 AS kodestatus, 1 AS kodedokumen, 0 AS flagvat, S.NPWP AS NPWPLawanTransaksi,  " +
                "S.SupplierName AS NamaLawanTransaksi, R.FakturPajak AS NomorDockumen, 0 AS JenisDokumen, ' ' AS NomorSeriYangDigantiDiretur,  " +
                "' ' AS JenisDokumenYangDigantiDiretur,R.FakturPajakDate AS tanggalFakturDokumen, ' ' AS TanggalSSP, REPLACE(STR(MONTH(R.FakturPajakDate), 2), SPACE(1), '0') +  " +
                "REPLACE(STR(MONTH(R.FakturPajakDate), 2), SPACE(1), '0') AS MasaPajak, YEAR(R.JTempoDate) AS Tahun, 0 AS Pembetulan, " +
                "case when R.ID >0 then  " +
                "(select TotPrice from (select ReceiptID,SUM(TotPrice) as TotPrice from(  " +
                "select ReceiptID,Quantity,price,Quantity*price as TotPrice  from ( " +
                "select ReceiptID,Quantity,ISNULL(price,0) as price from ( " +
                "select ReceiptID,Quantity,case when podetailid>0 then(select Price from POPurchnDetail where ID=ReceiptDetail.podetailid )  " +
                "end Price from ReceiptDetail)as RD ) as RD1) as RD2 group By RD2.ReceiptID ) as RD3 where RD3.ReceiptID=R.ID ) end DPP " +
                "FROM Receipt AS R INNER JOIN SuppPurch AS S ON R.SupplierId = S.ID INNER JOIN POPurchn AS PO ON R.POID = PO.ID " +
                "WHERE LEN(R.FakturPajak) > 5  and PO.PPN>0) as PPNBM " + kriteria;
            return strSQL;
        }
        static bool IsNumeric(object Expression)
        {
            bool isNum;
            double retNum;
            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }
        /**
      * added on 28-04-2014
      * untuk perubahan pada itemname table biaya
      * dan stock per itemnya
      */
        public string ItemSPPBiayaReceipt(string TableName)
        {
            string strSQL = "CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())<= " +
                " (Select CreatedTime from SPP where SPP.ID=" + TableName + ".SPPID)) " +
                " THEN(select ItemName from Biaya where Biaya.ID=" + TableName + ".JenisBiaya and Biaya.RowStatus>-1)+' - '+ " +
                " (Select SPPDetail.Keterangan From SPPDetail where SPPDetail.ID=(SELECT SPPDetailID From POPurchnDetail where ID=" + TableName + ".PODetailID) and " +
                " SPPDetail.SPPID=" + TableName + ".SPPID) ELSE " +
                " (select ItemName from biaya where ID=" + TableName + ".ItemID and biaya.RowStatus>-1) END ";
            return strSQL;
        }
        public string ItemCodeBiayaNew(string TableName)
        {
            string strSQL = "CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())<= " +
                   "(Select CreatedTime from SPP where SPP.ID=(SELECT POPurchnDetail.SPPID FROM POPurchnDetail where POPurchnDetail.ID=" + TableName + ".PODetailID)))  " +
                   "THEN (Select ItemCode from Biaya where ID=" + TableName + ".JenisBiaya) ELSE" +
                   "(SELECT ItemCode From Biaya where ID=" + TableName + ".ItemID)END";
            return strSQL;
        }
        public string ItemSPPBiayaNew2(string TableName)
        {
            return "CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())<= " +
                   "(Select CreatedTime from SPP where SPP.ID=(SELECT POPurchnDetail.SPPID FROM POPurchnDetail where POPurchnDetail.ID=ReceiptDetail.PODetailID)))  " +
                   "THEN(select ItemName from Biaya where Biaya.ID=(SELECT POPurchnDetail.ItemID FROM POPurchnDetail where POPurchnDetail.ID=ReceiptDetail.PODetailID)  " +
                   "and Biaya.RowStatus>-1)+' - '+   " +
                   "(Select SPPDetail.Keterangan From SPPDetail where SPPDetail.ID= " +
                   "(SELECT POPurchnDetail.SPPDetailID FROM POPurchnDetail where POPurchnDetail.ID=ReceiptDetail.PODetailID)) ELSE   " +
                   "(select ItemName from biaya where ID=(SELECT POPurchnDetail.ItemID FROM POPurchnDetail where POPurchnDetail.ID=ReceiptDetail.PODetailID) " +
                   "and biaya.RowStatus>-1) END ";
        }
        public string ItemPOBiayaNew(string TableName)
        {
            string strSQL = "CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())<= " +
                " (Select CreatedTime from SPP where SPP.ID=" + TableName + ".SPPID)) " +
                " THEN (Select UPPER(SUBSTRING(SPPDetail.Keterangan,1,1))+ Lower(SUBSTRING(SPPDetail.Keterangan,2,LEN(SPPDetail.Keterangan)-1))" +
                " From SPPDetail where SPPDetail.ID=" + TableName + ".SPPDetailID /*and " +
                " SPPDetail.SPPID=" + TableName + ".SPPID*/) ELSE " +
                " (select ItemName from biaya where ID=" + TableName + ".ItemID and biaya.RowStatus>-1) END ";
            return strSQL;
        }
        public string StockBiayaNew(string TableName)
        {
            string strSQL = "CASE WHEN (isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())<= " +
                " (Select CreatedTime from SPP where SPP.ID=" + TableName + ".SPPID)) THEN " +
                " (SELECT isnull(sum(jumlah),0) from Biaya where Biaya.ItemName=(Select SPPDetail.Keterangan From SPPDetail " +
                " where SPPDetail.ItemID=" + TableName + ".ItemID and SPPDetail.SPPID=" + TableName + ".SPPID)) " +
                " ELSE (SELECT isnull(sum(Jumlah),0) from Biaya where Biaya.ID=" + TableName + ".ItemID and Biaya.RowStatus>-1) END";
            return strSQL;
        }
        public string LapBullDept(string DeptCode, string tgl1, string tgl2, int groupID, string strItemTypeID, string sts)
        {
            string strSQL = "select  ItemID,0 as [010], 0 as [021] ,0 as [022],0 as [031],0 as [032],0 as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133], "+
            "sum(Quantity) as [135],0 as [139],0 as [142] " +
            "from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai  " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID  " +
            "in(select ID from Dept where DeptCode='" + DeptCode + "')) group by ItemID";
            return strSQL;
        }
        public string LapImpDetail(int Tahun, int Bulan, int GroupID, int Depo, string Stock)
        {
            string Bln = (Bulan == 0) ? "" : " and MONTH(CreatedTime)=" + Bulan;
            string Grp = (GroupID == 0) ? "" : " and GroupID=" + GroupID;
            string Dpo = (Depo == 0) ? "" : " and UnitKerjaID=" + Depo;
            string OrdBy = (Grp == string.Empty) ? " Order By GroupID" : "Order By ID";
            string strSQL = "Select ROW_NUMBER() OVER(" + OrdBy + ") as Num,ItemCode,ItemName," +
                          "(SELECT UOM.UOMCode From UOM where UOM.ID=Improvement.UOMID) as UOMCode," +
                          "Case When Stock=0 Then 'Non Stock' ELSE 'Stock' END Stocked,improvement.stock," +
                          "Case UnitKerjaID When 7 Then 'Karawang' when 1 then 'Citeureup' END UnitKerja, " +
                          "GroupID, DeptID, Approval FROM Improvement " +
                          "where YEAR(CreatedTime)=" + Tahun + Bln + Grp + Dpo + Stock +
                          "and RowStatus>-1 and Approval=2" +
                          OrdBy;
            return strSQL;
        }
        public string LapImpRekap(int Tahun, int Depo)
        {
            string pln = (Depo == 0) ? "" : " and UnitKerjaID=" + Depo;
            string strSQL = "SELECT  Tahun,Bulan,sum(BB) as BB,sum(BP) as BP,sum(ATK)as ATK, " +
                            "sum(PYK) as PYK,sum(MEK)as MEK,sum(ELK)as ELK,sum(MKT) as MKT,sum(RKP) as RKP " +
                            "    FROM( " +
                            "    Select Tahun,Bulan, " +
                            "    Case When GroupID=1 THEN COUNT(ID) ELSE 0 END AS BB, " +
                            "    Case When GroupID=2 THEN COUNT(ID) ELSE 0 END AS BP , " +
                            "    Case When GroupID=3 THEN COUNT(ID) ELSE 0 END AS ATK , " +
                            "    Case When GroupID=6 THEN COUNT(ID) ELSE 0 END AS PYK , " +
                            "    Case When GroupID=7 THEN COUNT(ID) ELSE 0 END AS MKT , " +
                            "    Case When GroupID=8 THEN COUNT(ID) ELSE 0 END AS MEK , " +
                            "    Case When GroupID=9 THEN COUNT(ID) ELSE 0 END AS ELK , " +
                            "    Case When GroupID=10 THEN COUNT(ID) ELSE 0 END AS RKP ,GroupID " +
                            "    FROM( " +
                            "        SELECT YEAR(CreatedTime) as Tahun,MONTH(CreatedTime) as Bulan,ID, GroupID  " +
                            "        FROM Improvement where YEAR(CreatedTime)=" + Tahun + " and RowStatus>-1 " + pln +
                            "    ) as m group by GroupID,Bulan,Tahun " +
                            ")  " +
                            "as w " +
                            "Group By Bulan,Tahun " +
                            "Order by Bulan, Tahun";
            return strSQL;
        }
        public string ViewRekapRetur(string tgl1, string tgl2)
        {
            #region depreciated query
            //string strsql = "select CONVERT(varchar,T3_Retur.TglTrans,103) as tgltrans," +
            //                "T3_Retur.Customer,T3_Retur.SJNo,T3_Retur.Qty,FC_Items.PartNo,T3_Retur.OPNo " +
            //               "from T3_Simetris " +
            //               "left join T3_Serah on T3_Simetris.SerahID=T3_Serah.ID " +
            //               "left join t3_retur on T3_Serah.LokID=T3_Retur.LokID and T3_Serah.ItemID=T3_Retur.ItemID and T3_Retur.TglTrans=T3_Simetris.TglTrans " +
            //               "left join FC_Items on T3_Retur.ItemID=FC_Items.ID " +
            //               "where T3_Serah.LokID in (select ID from FC_Lokasi where Lokasi='R99') " +
            //               "and convert(varchar,t3_retur.tgltrans,112)>='" + tgl1 + "' and  convert(varchar,t3_retur.tgltrans,112)<='" +
            //                   tgl2 + "' " + this.SortBy();
            #endregion
            string strsql = "select CONVERT(varchar,T3_Retur.TglTrans,103) as tgltrans," +
                "T3_Retur.Customer,T3_Retur.SJNo,T3_Retur.Qty,FC_Items.PartNo,T3_Retur.OPNo " +
                "from T3_Retur " +
                "left join FC_Items on T3_Retur.ItemID=FC_Items.ID " +
                "where T3_Retur.RowStatus > -1 and convert(varchar,t3_retur.tgltrans,112)>='" + tgl1 + "' and  convert(varchar,t3_retur.tgltrans,112)<='" +
                 tgl2 + "' " + this.SortBy();

            return strsql;
        }
        public string ViewFormTask(string taskno)
        {
            string strSQL = "select A.taskno as NoTask,Co.Lokasi as Plant,De.DeptName as departemen,TglMulai as tglmasuk," +
                "(Select UserName from UserAccount where RowStatus>-1 and UserID=(select ID from ISO_Users where DeptID=A.DeptID and DeptJabatanID in " +
                "(select ID from ISO_Bagian where UserGroupID=100) and RowStatus>-1)) as head, " +
                "(select BagianName from ISO_Bagian where ID in (select DeptJabatanID  from ISO_Users where DeptID=A.DeptID and DeptJabatanID in " +
                "(select ID from ISO_Bagian where UserGroupID=100) and RowStatus>-1)) as headJab, " +
                "(Select UserName from UserAccount where RowStatus>-1 and UserID=(select ID from ISO_Users where Username=A.PIC and RowStatus>-1)) as users," +
                "B.BagianName  as usersjab,TaskName, NilaiBobot as Bobot, TglSelesai,det.PointNilai  as Point,A.AlasanCancel,  " +
                "A.* from ISO_Task A inner join Users U on A.UserID=U.ID inner join ISO_Users UI on A.UserID=UI.UserID  " +
                "inner join ISO_Bagian B on A.BagianID=B.ID inner join Company Co on Co.DepoID =U.UnitKerjaID  " +
                "inner join Dept De on UI.DeptID=De.ID inner join ISO_TaskDetail det on A.ID=det.TaskID  " +
                " where A.TaskNo='" + taskno + "'";
            return strSQL;
        }
        public string ViewFormTask(string taskno, bool forPrint)
        {
            string strSQL = "WITH TaskView AS ( " +
                          "  SELECT a.TaskNo NoTask,c.Lokasi Plant, d.DeptName departemen,a.TglMulai tglmasuk,u.UserName users,A.*, " +
                          "  ib.BagianName  as usersjab, NilaiBobot as Bobot, det.PointNilai  as Point " +
                          "  FROM ISO_Task A  " +
                          "  LEFT JOIN ISO_TaskDetail det on det.TaskID=a.ID " +
                          "  LEFT JOIN ISO_Bagian ib ON ib.ID=A.BagianID " +
                          "  LEFT JOIN Iso_users iu On iu.UserName=a.PIC " +
                          "  LEFT JOIN UserAccount u ON u.UserID=iu.ID " +
                          "  LEFT JOIN Dept d on d.ID=a.DeptID " +
                          "  LEFT JOIN Company c On c.DepoID=iu.UnitKerjaID " +
                          "   WHERE A.TaskNo='" + taskno + "' " +
                          "  ) " +
                          "  ,HeadName AS ( " +
	                          "  SELECT isd.*,u.UserName,ib.BagianName FROM ISO_Dept isd " +
	                          "  LEFT JOIN ISO_BagianHead iu ON iu.DeptApp in(''+CAST(isd.DeptID as CHAR)+'') " +
	                          "  LEFT JOIN UserAccount u ON u.UserID=iu.ISOuserID " +
	                          "  LEFT JOIN ISO_Bagian ib ON ib.ID=u.BagianID AND ib.RowStatus>-1 " +
                              "  WHERE isd.DeptID IN(SELECT DeptID FROM TaskView) AND isd.UserGroupID=100 " +
                          "  ) " +
                          "  SELECT t.* " +
                          "  ,h.UserName Head,h.BagianName HeadJab  " +
                          "  FROM TaskView t " +
                          "  LEFT JOIN HeadName h on h.DeptID=t.DeptID";
            return strSQL;
        }
        private string SortBy()
        {
            string strQuery = (HttpContext.Current.Session["SortBy"] != null) ? HttpContext.Current.Session["SortBy"].ToString() : "order by T3_Retur.Customer";
            return strQuery;
        }
        public ArrayList ViewRekapReturPrev(string tgl1, string tgl2)
        {

            string strQuery = this.ViewRekapRetur(tgl1, tgl2);
            DataAccess dta = new DataAccess(Global.ConnectionString());
            SqlDataReader sda = dta.RetrieveDataByString(strQuery);
            arrReport = new ArrayList();
            decimal sTotal = 0;
            if (dta.Error == string.Empty && sda.HasRows)
            {
                while (sda.Read())
                {
                    sTotal += Convert.ToDecimal(sda["Qty"].ToString());
                    arrReport.Add(new ReturnBJ
                    {
                        Tanggal = Convert.ToDateTime(sda["tgltrans"].ToString()),
                        SJNo = sda["SJNo"].ToString(),
                        OPNo = sda["OPNo"].ToString(),
                        Customer = sda["Customer"].ToString(),
                        PartNo = sda["PartNo"].ToString(),
                        Qty = Convert.ToDecimal(sda["Qty"].ToString()),
                        Total = sTotal
                    });
                }
            }
            return arrReport;
        }
        /** aded on : 02-03-2015
         * added by : beny
         */
        public string ViewLapOpname(string periodeAwal, string periodeAkhir, int groupID, string bulan, int thn)
        {

            string strSQL;

            string strSQL2 = "select C.ItemCode,C.ItemName,D.UOMCode as UOMDesc,(A." + bulan + "+sum(B.quantity)) as qty from SaldoInventory as A,vw_StockPurchn as B, " +
                     "Inventory as C, UOM as D where A.ItemID=B.ItemID and B.ItemID=C.ID and C.UOMID=D.ID and B.YMD between '" + periodeAwal + "' and '" + periodeAkhir + "' " +
                     "and B.GroupID=" + groupID + " and A.YearPeriod=" + thn + " and C.Aktif=1  group by B.ItemID,A." + bulan + ",C.ItemCode,C.ItemName,D.UOMCode,C.ID " +
                     "union " +
                     "select B.ItemCode,B.ItemName,C.UOMCode as UOMDesc,(A." + bulan + ") as qty from SaldoInventory as A, Inventory as B,UOM as C " +
                     "where not exists (select ItemID from vw_StockPurchn as H where A.ItemID=H.ItemID and tanggal between '" + periodeAwal + "' and '" + periodeAkhir + "') " +
                     "and A.ItemID=B.ID and B.UOMID=C.ID and A." + bulan + " > 0 and A.GroupID=" + groupID + " and A.YearPeriod=" + thn + " " +
                     "union " +
                     "select A.ItemCode,A.ItemName,C.UOMCode as UOMDesc,sum(B.quantity) as qty from Inventory as A,vw_StockPurchn as B,UOM as C where " +
                     "not exists (select ItemID from SaldoInventory as H where B.ItemID=H.ItemID  and H.YearPeriod=" + thn + " ) and A.UOMID=C.ID " +
                     "and A.ID=B.ItemID and B.tanggal between '" + periodeAwal + "' and '" + periodeAkhir + "' and B.GroupID=" + groupID + " group by A.ItemCode, " +
                     "A.ItemName,C.UOMCode order by C.ItemName asc ";
            /**
             * New Query
             * Added on : 06-10-2015
             * Author   : Beny
             */
            strSQL = "select C.ItemCode,C.ItemName,D.UOMCode as UOMDesc,(A." + bulan + "+sum(B.quantity)) as qty from SaldoInventory as A,vw_StockPurchn as B, " +
                     "Inventory as C, UOM as D where A.ItemID=B.ItemID and B.ItemID=C.ID and C.UOMID=D.ID and B.YMD between '" + periodeAwal + "' and '" + periodeAkhir + "' " +
                     "and B.GroupID=" + groupID + " and A.YearPeriod=" + thn + " and C.Aktif=1  group by B.ItemID,A." + bulan + ",C.ItemCode,C.ItemName,D.UOMCode,C.ID " +
                     "union " +
                     "select B.ItemCode,B.ItemName,C.UOMCode as UOMDesc,(A." + bulan + ") as qty from SaldoInventory as A, Inventory as B,UOM as C " +
                     "where not exists (select ItemID from vw_StockPurchn as H where A.ItemID=H.ItemID and YMD between '" + periodeAwal + "' and '" + periodeAkhir + "') " +
                     "and A.ItemID=B.ID and B.UOMID=C.ID and A." + bulan + " > 0 and A.GroupID=" + groupID + " and A.YearPeriod=" + thn + " " +
                     "union " +
                     "select A.ItemCode,A.ItemName,C.UOMCode as UOMDesc,sum(B.quantity) as qty from Inventory as A,vw_StockPurchn as B,UOM as C where " +
                     "not exists (select ItemID from SaldoInventory as H where B.ItemID=H.ItemID  and H.YearPeriod=" + thn + " ) and A.UOMID=C.ID " +
                     "and A.ID=B.ItemID and B.YMD between '" + periodeAwal + "' and '" + periodeAkhir + "' and B.GroupID=" + groupID + " group by A.ItemCode, " +
                     "A.ItemName,C.UOMCode order by C.ItemName asc "; ;
            return strSQL;

        }
        public string ViewRekapCatridge(string tgl1, string tgl2)
        {
            string query = "select IT_PakaiCatridge.GantiNo, CONVERT(varchar,IT_PakaiCatridge.Tanggal,103)as Tanggal,IT_PakaiCatridge.Nama,IT_PakaiCatridge.DeptName," +
                           "IT_MCatridge.TypeCatridge,IT_PakaiCatridge.Jumlah " +
                           "from IT_PakaiCatridge, IT_MCatridge where IT_PakaiCatridge.TypeCatridgeID=IT_MCatridge.ID and " +
                           "CONVERT(varchar,IT_PakaiCatridge.Tanggal,112)>='" + tgl1 + "' and " +
                           "CONVERT(varchar,IT_PakaiCatridge.Tanggal,112)<='" + tgl2 + "' and IT_PakaiCatridge.RowStatus > -1 " +
                           "order by IT_PakaiCatridge.ID desc";

            return query;
        }
        public string ViewRekapReceiptNew(string dari, string sampai, string groupid)
        {
            string query = string.Empty;
            query = "SELECT receiptdetailID,kurs, HargaPO,ID,PONo, ReceiptNo, SupplierName,PaymentType,ItemFROM, CRC ,Keterangan, ItemCode,ItemName, " +
                    "ItemID, SPPNo, UOMCode, NamaGrup, ReceiptDate,disc, kadarair,PPN,remark,PODetailID,qtyPO,  Quantity,qtyPO-TotRCP AS sisaPO, " +
                    "CASE WHEN viewP=2 THEN (Harga2*kurs) ELSE (Harga1*kurs) END Harga, " +
                    "CASE WHEN viewP=2 THEN (Total2*kurs) ELSE (Total1*kurs) END total, " +
                    "FakturPajak AS NoFaktur,FakturPajakDate AS TglFaktur,PKP  FROM (  " +
                    "SELECT  2 AS viewP,POD.price AS HargaPO,RCD.ID, RCP.PONo, RCP.ReceiptNo,RCP.FakturPajak,RCP.FakturPajakDate, " +
                    "RCD.id AS receiptdetailID, case when (select ISNULL(subcompanyid,0) from SuppPurch where ID=Sup.ID)>0 and LEFT(convert(char,RCP.ReceiptDate,112),6)>='201602' then (select rtrim(SupplierName) from SuppPurch where COID=Sup.subcompanyid) else Sup.SupplierName end SupplierName,PO.PaymentType,PO.ItemFROM, MU.Nama AS CRC ,RCD.Keterangan, " +
                    "CASE RCD.ItemTypeID " +
                    "    WHEN 1 THEN (SELECT ItemCode FROM Inventory WHERE ID = RCD.ItemID)  " +
                    "    WHEN 2 THEN (SELECT ItemCode FROM ASset WHERE ID = RCD.ItemID) " +
                    "ELSE (SELECT ItemCode FROM Biaya WHERE ID = RCD.ItemID) END AS ItemCode,  " +
                    "CASE RCD.ItemTypeID     " +
                    "    WHEN 1 THEN (SELECT ItemName FROM Inventory WHERE ID = RCD.ItemID)  " +
                    "    WHEN 2 THEN (SELECT ItemName FROM ASset WHERE ID = RCD.ItemID)  " +
                    "    ELSE /*(SELECT ItemName FROM Biaya WHERE ID = RCD.ItemID)*/  " +
                    "        CASE WHEN(isnull((SELECT ModifiedTime FROM BiayaNew WHERE RowStatus=1),GETDATE())<=  (SELECT CreatedTime FROM SPP WHERE SPP.ID=POD.SPPID))   " +
                    "        THEN(SELECT ItemName FROM Biaya WHERE Biaya.ID=POD.ItemID and Biaya.RowStatus>-1)+' - '+   " +
                    "        (SELECT SPPDetail.Keterangan FROM SPPDetail WHERE SPPDetail.ID=POD.SPPDetailID and  SPPDetail.SPPID=POD.SPPID)  " +
                    "        ELSE  (SELECT ItemName FROM biaya WHERE ID=POD.ItemID and biaya.RowStatus>-1)  " +
                    "        END   " +
                    "END AS ItemName,     " +
                    "RCD.ItemID, RCD.SPPNo, U.UOMCode, UPPER(G.GroupDescription) AS NamaGrup, RCP.ReceiptDate,  PO.disc, RCD.kadarair,PO.PPN, " +
                    "RCD.Keterangan AS remark,RCD.Quantity ,RCD.PODetailID,   " +
                    "CASE WHEN RCD.PODetailID>0 THEN ( " +
                    "SELECT SUM(Quantity)  FROM ReceiptDetail inRCD  " +
                    "inner join Receipt inRC on inRC.ID=inRCD.ReceiptID   WHERE inRCD.PODetailID=RCD.PODetailID  and inRC.Status>=0  and inRCD.ID<=RCD.ID)  " +
                    "END TotRCP,  " +
                    "POD.Qty AS qtyPO, POD.PRICE AS Harga1,  " +
                    " /*harga kertAS*/ " +
                    "    CASE WHEN RCD.PRICE=0  THEN  " +
                    "        TB.HargASatuan " +
                    "        /*(SELECT top 1 isnull(THB.Harga,0) FROM HargaKertAS THB WHERE THB.ItemID =RCD.ItemID and THB.SupplierID=PO.SupplierID)*/  " +
                    "    WHEN RCD.PRICE=0 and RCD.kadarair<25 THEN " +
                    "        TB.HargASatuan   " +
                    "    /*(SELECT isnull(THB.Harga,0) FROM HargaKertAS THB  WHERE THB.ItemID =RCD.ItemID and THB.SupplierID=PO.SupplierID )   */  " +
                    "    ELSE isnull(POD.PRICE,0) END  Harga2, " +
                    "isnull(sup.pkp,'No')  PKP,       " +
                    "((POD.PRICE * RCD.quantity)-((POD.price *PO.disc * RCD.quantity) / 100)) AS Total1, " +
                    "CASE WHEN RCD.PRICE=0 and RCD.kadarair>=25 THEN ((TB.HargASatuan)*RCD.quantity)-((POD.price *PO.disc * RCD.quantity) / 100)  " +
                    "/*(SELECT top 1 ((THB.Harga)*RCD.quantity)-((POD.price *PO.disc * RCD.quantity) / 100)     " +
                    "FROM HargaKertAS THB  WHERE  THB.ItemID =RCD.ItemID and THB.SupplierID=PO.SupplierID /*and THB.KadarAir>=25*/ )  */  " +
                    "WHEN RCD.PRICE=0 and RCD.kadarair<25 THEN ((TB.HargASatuan)*RCD.quantity)-((POD.price *PO.disc * RCD.quantity) / 100)  " +
                    "/*(SELECT top 1 (THB.Harga*RCD.quantity)-((POD.price *PO.disc * RCD.quantity) / 100)            " +
                    "FROM HargaKertAS THB WHERE  THB.ItemID =RCD.ItemID and THB.SupplierID=PO.SupplierID /*and THB.KadarAir<25*/) */    " +
                    "ELSE (POD.PRICE*RCD.quantity)-((POD.price *PO.disc * RCD.quantity) / 100) END  Total2, " +
                    "CASE WHEN PO.Crc >1 THEN  " +
                    "    CASE WHEN Sup.flag=2 THEN " +
                    "    (SELECT top 1 isnull(kurs,1) FROM MataUangKurs WHERE RowStatus>=0 and drTgl =RCP.ReceiptDate and MataUangKurs.MUID =PO.Crc ) " +
                    "    ELSE  " +
                    "        CASE WHEN PO.NilaiKurs>0 THEN PO.NilaiKurs ELSE  " +
                    "        (SELECT top 1 isnull(kurs,1) FROM MataUangKurs WHERE RowStatus>=0 and drTgl =RCP.ReceiptDate and MataUangKurs.MUID =PO.Crc) " +
                    "        END " +
                    "    END " +
                    "ELSE 1 END kurs  " +
                    "FROM Receipt RCP   " +
                    "LEFT JOIN ReceiptDetail RCD ON RCP.ID = RCD.ReceiptID  and RCD.RowStatus > -1  " +
                    "LEFT JOIN POPurchn PO ON RCD.POID =PO.ID and RCD.RowStatus>-1  " +
                    "LEFT join POPurchnDetail POD on  RCD.PODetailID =POD.ID   " +
                    "LEFT JOIN SuppPurch Sup ON PO.SupplierID = Sup.ID   " +
                    "LEFT JOIN MataUang MU ON PO.Crc = MU.ID    " +
                    "LEFT JOIN UOM U ON RCD.UomID = U.ID  " +
                    "LEFT JOIN GroupsPurchn G ON  RCD.GroupID = G.ID " +
                    "LEFT JOIN TabelHargaBankOut TB on TB.ReceiptDetailID=RCD.ID   " +
                    "WHERE  RCP.status>-1 and convert(varchar,RCP.Receiptdate,112)>='" + dari + "' and " +
                    "convert(varchar,RCP.Receiptdate,112)<='" + sampai + "'   " +
                    "and RCD.GroupID =  " + groupid +
                    ") AS query  " +
                    "ORDER BY PODetailID, ID";
            return query;
        }

        /** Query For Labul with Price **/
        public string ViewLapBul2VP(string PriceBlnLalu, string ketBlnLalu, int yearPeriod, string tgl1, string tgl2, int groupID, string stock)
        {
            /**
             * Update on 10-03-2015
             * Change 131 to 134
             * 131 di gabung dengan 134
             */
            string strItemTypeID = string.Empty;
            string strJenisBrg = string.Empty;
            string strStock = string.Empty;
            string sts = "3";
            string created = ((Users)HttpContext.Current.Session["Users"]).ID.ToString();
            #region pilih group
            if (groupID == 5)
            {
                strItemTypeID = " and A.ItemTypeID = 3";
                strJenisBrg = "Biaya";
            }
            else
            {
                if (groupID == 4 || groupID == 12)
                {
                    strItemTypeID = " and A.ItemTypeID = 2";
                    strJenisBrg = "Asset";
                }
                else
                {
                    strItemTypeID = " and A.ItemTypeID = 1";
                    strJenisBrg = "Inventory";
                }
            }
            if (stock == "0")
            {
                strStock = " and  " + strJenisBrg + ".Stock = 0 and " + strJenisBrg + ".aktif=1 ";
            }
            else if (stock == "1")
            {
                strStock = " and  " + strJenisBrg + ".Stock = 1 and " + strJenisBrg + ".aktif=1 ";
            }
            else
            {
                strStock = " ";
            }
            #endregion

            string strSQL =
            "SELECT ItemCode,ItemName,UOMCode,StokAwal,PriceL,PriceC,Pemasukan,priceM,priceP,Retur,priceR,AdjustTambah,AdjustTambahReInt,AdjustKurang,priceAT,priceAK" +
            ",isnull([010],0) as[010],isnull([021],0)as [021],isnull([022],0)as [022],isnull([031],0)as [031],isnull([032],0)as [032], " +
            "isnull([033],0)as [033],isnull([034],0)as [034],isnull([041],0)as [041],isnull([042],0)as [042],isnull([051],0)as [051], " +
            "isnull([052],0)as [052],isnull([061],0)as [061],isnull([062],0)as [062],isnull([070],0)as [070],isnull([091],0)as [091], " +
            "isnull([101],0)as [101],isnull([111],0)as [111],isnull([012],0)as [012],isnull([131],0)as [131],isnull([132],0)as [132], " +
            "isnull([133] ,0)as [133],isnull([135],0) as [135],isnull([139],0) as [139],isnull([142],0) as [142]  " +
            "from (SELECT " + strJenisBrg + ".id  as itemid," +
            strJenisBrg + ".ItemCode, " + strJenisBrg + ".ItemName, UOM.UOMCode, " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT isnull(sum(" + ketBlnLalu + "),0) FROM SaldoInventory A WHERE ITEMID=" +
            strJenisBrg + ".id AND YearPeriod=" + yearPeriod + strItemTypeID + ") + " + "(select isnull(SUM(quantity),0) " +
            "from vw_StockPurchn where ItemID=" + strJenisBrg + ".ID and YMD<'" + tgl1 + "' and YM=SUBSTRING('" + tgl1 + "',1,6)) END StokAwal, " +

            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT isnull(sum(" + PriceBlnLalu + "),0) FROM SaldoInventory A WHERE ITEMID=" +
                //    strJenisBrg + ".id AND YearPeriod=" + yearPeriod + strItemTypeID + ") END PriceL, " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT isnull(sum(HS),0) FROM lapbul_" + created + "_lapsaldoawal A WHERE ITEMID=" +
            strJenisBrg + ".id ) END PriceL, " +


            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  ISNULL(SUM(QUANTITY),0) FROM  PakaiDetail A inner join Pakai B on B.id = A.PakaiID   " +
            "WHERE A.ItemID =" + strJenisBrg + ".ID and A.RowStatus>-1   and A.ItemTypeID = 1 and B.status > 1   " +
            "and convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,B.PakaiDate,112) <= '" + tgl2 + "') END  sumqtyPakai, " +

            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  ISNULL(sum(quantity*AvgPrice),0) FROM  PakaiDetail A inner join Pakai B on B.id = A.PakaiID   " +
                //"WHERE A.ItemID =" + strJenisBrg + ".ID and  A.RowStatus>-1   and A.ItemTypeID = 1 and B.status > 1   " +
                //"and convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "' AND convert(varchar,B.PakaiDate,112) <= '" + tgl2 + "' ) END  priceP, " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT isnull(sum(ProdHS),0) FROM lapbul_" + created + "_mutASisaldo A WHERE ITEMID=" +
            strJenisBrg + ".id ) END  priceP, " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT isnull(sum(HS),0) FROM lapbul_" + created + "_mutASisaldo A WHERE ITEMID=" +
            strJenisBrg + ".id )  end PriceC," +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (select isnull(sum(qty),0) from (SELECT ISNULL(SUM(QUANTITY),0)qty FROM  ReceiptDetail A inner join receipt B on " +
            " A.receiptID=B.ID where A.ItemID=" + strJenisBrg + ".ID and A.RowStatus>-1  " + strItemTypeID + " and B.status > -1 AND " +
            "convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' " +
            "union all select ISNULL(SUM(ToQty),0)qty from Convertan where ToItemID=" + strJenisBrg + ".ID and Convertan.RowStatus>-1 and " +
            "convert(varchar,Convertan.CreatedTime,112) >= '" + tgl1 + "' AND convert(varchar,Convertan.CreatedTime,112) <= '" + tgl2 + "')A ) END  Pemasukan, " +

            //"/* Receipt tidak di average price nya langsung ambil dari po " +
                //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  ISNULL(sum(avgPrice*Quantity),0) FROM  ReceiptDetail A inner join Receipt B on B.id = A.ReceiptID   " +
                //"WHERE A.ItemID =" + strJenisBrg + ".ID and  A.RowStatus>-1   and A.ItemTypeID = 1 and B.status > -1   " +
                //"and convert(varchar,B.ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,B.ReceiptDate,112) <= '" + tgl2 + "' ) END  priceM,*/ " +

            //"/* price Receipt diambil dari po dan di kurs in */ " +
                //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (" +
                //"SELECT ISNULL(SUM(Price*Quantity),0) FROM ( " +
                //"SELECT rd.ItemID,rc.ReceiptNo,rd.PONo, rd.Quantity, " +
                //"CASE WHEN pd.Price>0 THEN pd.Price ELSE pd.Price2 END Price,p.Crc, " +
                //"CASE WHEN p.NilaiKurs=0 and p.Crc>1 THEN " +
                //"(SELECT TOP 1 Kurs FROM MataUangKurs WHERE MUID=p.Crc and Convert(Char,drTgl,112)=Convert(Char,rc.ReceiptDate,112)  " +
                //"and MataUangKurs.rowstatus=1) " +
                //"ELSE p.NilaiKurs END NilaiKurs " +
                //"FROM Receipt rc " +
                //"LEFT JOIN ReceiptDetail rd on rd.ReceiptID=rc.ID " +
                //"LEFT JOIN POPurchnDetail pd on pd.ID=rd.PODetailID " +
                //"LEFT JOIN POPurchn p ON p.ID=pd.POID " +
                //"WHERE convert(char,rc.ReceiptDate,112) between '" + tgl1 + "' and '" + tgl2 + "' and rd.ItemTypeID=1 " +
                //"AND rd.ItemID=" + strJenisBrg + ".ID and rc.Status>-1 and rd.RowStatus>-1 " +
                //") as x ) END priceM, " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT isnull(sum(BeliHS),0) FROM lapbul_" + created + "_mutASisaldo A WHERE ITEMID=" +
            strJenisBrg + ".id ) END priceM, " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail A inner join adjust B on B.id = A.adjustID " +
            " WHERE  A.apv>0 and A.ItemID = " + strJenisBrg + ".ID AND A.RowStatus>-1  " + strItemTypeID + " AND B.ADJUSTTYPE='tambah' and B.status > -1  " +
            "and convert(varchar,B.AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,B.AdjustDate,112) <= '" + tgl2 + "' and "+
            "A.AdjustID not in (select A1.ID from Adjust A1 where A1.ID=A.AdjustID and Keterangan1 like'%ReturIntLog%')) END  AdjustTambah, " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail A inner join adjust B on B.id = A.adjustID " +
            " WHERE  A.apv>0 and A.ItemID = " + strJenisBrg + ".ID AND A.RowStatus>-1  " + strItemTypeID + " AND B.ADJUSTTYPE='tambah' and B.status > -1  " +
            "and convert(varchar,B.AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,B.AdjustDate,112) <= '" + tgl2 + "' and " +
            "A.AdjustID in (select A1.ID from Adjust A1 where A1.ID=A.AdjustID and Keterangan1 like'%ReturIntLog%')) END  AdjustTambahReInt, " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail A inner join adjust B on B.id = A.adjustID " +
            " WHERE  A.apv>0 and A.ItemID = " + strJenisBrg + ".ID AND A.RowStatus>-1  " + strItemTypeID + " AND B.ADJUSTTYPE='kurang' and B.status > -1  AND (nonstok IS null OR nonstok != 1) " +
            "and convert(varchar,B.AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,B.AdjustDate,112) <= '" + tgl2 + "' ) END  AdjustKurang, " +

            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  ISNULL(sum(quantity*AvgPrice),0) FROM  AdjustDetail A inner join Adjust B on B.id = A.AdjustID   " +
                //"WHERE  A.apv>0 and A.ItemID =" + strJenisBrg + ".ID and  A.RowStatus>-1   and A.ItemTypeID = 1 AND B.ADJUSTTYPE='tambah' and B.status > -1   " +
                //"and convert(varchar,B.AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,B.AdjustDate,112) <= '" + tgl2 + "' ) END  priceAT, " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT isnull(sum(AdjustHS),0) FROM lapbul_" + created + "_mutASisaldo A WHERE ITEMID=" +
            strJenisBrg + ".id ) END priceAT, " +

            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  ISNULL(sum(quantity*AvgPrice),0) FROM  AdjustDetail A inner join Adjust B on B.id = A.AdjustID   " +
                //"WHERE  A.apv>0 and A.ItemID =" + strJenisBrg + ".ID and  A.RowStatus>-1   and A.ItemTypeID = 1 and  B.ADJUSTTYPE='kurang' AND B.status > -1   " +
                //"and convert(varchar,B.AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,B.AdjustDate,112) <= '" + tgl2 + "' ) END  priceAK, " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT isnull(sum(AdjProdHS),0) FROM lapbul_" + created + "_mutASisaldo A WHERE ITEMID=" +
            strJenisBrg + ".id ) END priceAK, " +

            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(Quantity),0) FROM  ReturPakaiDetail A inner join ReturPakai B on " +
            " B.ID = A.returID WHERE A.ItemID = " + strJenisBrg + ".ID AND A.RowStatus>-1  " + strItemTypeID + " AND B.status > -1 AND  " +
            "convert(varchar, B.ReturDate,112) >= '" + tgl1 + "' AND convert(varchar, B.ReturDate,112) <= '" + tgl2 + "' ) END  Retur, " +

            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  ISNULL(sum(quantity*AvgPrice),0) FROM  ReturPakaiDetail A inner join ReturPakai B on B.id = A.ReturID   " +
                //"WHERE A.ItemID =" + strJenisBrg + ".ID and  A.RowStatus>-1   and A.ItemTypeID = 1 and B.status > -1   " +
                //"and convert(varchar,B.ReturDate,112) >= '" + tgl1 + "' AND convert(varchar,B.ReturDate,112) <= '" + tgl2 + "' ) END  priceR " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT isnull(sum(ReturnHS),0) FROM lapbul_" + created + "_mutASisaldo A WHERE ITEMID=" +
            strJenisBrg + ".id ) END priceR " +

            "FROM  " + strJenisBrg + " INNER JOIN UOM ON " + strJenisBrg + ".UOMID = UOM.ID WHERE (" + strJenisBrg + ".GroupID = " +
            groupID + strStock + " )) AS AA " +
            "left join " +
            "(select  itemid1=case when grouping(ItemID)=1 then 0 else 1 end, itemid, " +
            "sum([010]) as [010],sum([021]) as[021],sum([022]) as[022],sum([031]) as[031],sum([032]) as[032],sum([033]) as[033]  " +
            ",sum([034]) as[034],sum([041]) as[041],sum([042]) as[042],sum([051]) as[051],SUM([052]) as[052],SUM([061]) as[061],SUM([062]) as[062], " +
            "SUM([070]) as[070],SUM([091]) as[091],SUM([101]) as[101],SUM([111]) as[111],SUM([012]) as[012],SUM([131]) as[131],SUM([132]) as[132], " +
            "sum([133]) as[133], sum([135]) as[135], sum([139]) as[139], sum([142]) as[142]" +
            "from ( " +
            "(select  ItemID,sum(Quantity) as [010], 0 as [021] ,0 as [022],0 as [031],0 as [032],0 as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai  " +
            "where status >=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID  " +
            "in(select ID from Dept where DeptCode='137'/*'010'*/)) group by ItemID ) union all ( " +

            "select ItemID,0 as [010], sum(Quantity)  as [021] ,0 as [022],0 as [031],0 as [032],0 as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + "  and RowStatus>=0 and PakaiID in(select ID from Pakai  " +
            "where status >=" + sts + " and  convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='021')) group by ItemID  ) union all ( " +

            "select ItemID,0 as [010], 0 as [021] ,sum(Quantity) as [022],0 as [031],0 as [032],0 as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + "  and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='022')) group by ItemID  ) union all ( " +

            "select ItemID,0 as [010], 0 as [021] ,0 as [022],sum(Quantity) as [031],0 as [032],0 as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + "  and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='031')) group by ItemID  ) union all ( " +

            "select ItemID,0 as [010], 0 as [021] ,0 as [022],0 as [031],sum(Quantity) as  [032],0 as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + "  and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='032')) group by ItemID  ) union all ( " +

            "select ItemID,0 as [010], 0 as [021] ,0 as [022],0 as [031],0 as  [032],sum(Quantity) as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + "  and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='033')) group by ItemID  ) union all ( " +

            "select ItemID,0 as [010], 0 as [021] ,0 as [022],0 as [031], 0 as  [032],0 as [033],sum(Quantity) as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='034')) group by ItemID  ) union all ( " +

            "select ItemID,0 as [010], 0 as [021] ,0 as [022],0 as [031],0 as  [032],0 as [033],0 as [034],sum(Quantity) as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='041')) group by ItemID  ) union all ( " +

            "select ItemID,0 as [010], 0 as [021] ,0 as [022],0 as [031],0 as  [032],0 as [033],0 as [034],0 as [041],sum(Quantity) as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='042')) group by ItemID  ) union all ( " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],sum(Quantity) as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='051')) group by ItemID  ) union all ( " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "sum(Quantity) as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='052')) group by ItemID  ) union all ( " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],sum(Quantity) as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='061')) group by ItemID  ) union all ( " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],sum(Quantity) as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='062')) group by ItemID  ) union all ( " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],sum(Quantity) as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='070')) group by ItemID  ) union all ( " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],sum(Quantity) as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='091')) group by ItemID  ) union all ( " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],sum(Quantity) as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='101')) group by ItemID  ) union all ( " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],sum(Quantity) as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='111')) group by ItemID  ) union all ( " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],sum(Quantity) as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='012')) group by ItemID  ) union all ( " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],sum(Quantity) as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='134')) group by ItemID  ) union all ( " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],sum(Quantity) as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='133')) group by ItemID  ) union all ( " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],sum(Quantity) as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode in('132','131'))) group by ItemID  ) union all ( " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],sum(Quantity) as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='139')) group by ItemID) union all (" +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],sum(Quantity) as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode in ('141','142'))) group by ItemID) union all (" +

                LapBullDept("135", tgl1, tgl2, groupID, strItemTypeID, sts) +
            ")) as pemakaian group by itemid with rollup " +
            ") as AB on AB.ItemID =AA.ItemID where (AA.StokAwal<>0 or AB.[142]<>0 or AA.Pemasukan<>0 or AA.Retur<>0 or AA.AdjustTambah<>0 or AA.AdjustKurang<>0)  " +
            "ORDER BY ItemCode ";
            //"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapbul_" + created + "_lapmutasitmp]') AND type in (N'U')) DROP TABLE [dbo].[lapbul_" + created + "_lapmutasitmp] " +
            //"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapbul_" + created + "_lapmutasitmpx]') AND type in (N'U')) DROP TABLE [dbo].[lapbul_" + created + "_lapmutasitmpx] " +
            //"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapbul_" + created + "_lapmutasitmpxx]') AND type in (N'U')) DROP TABLE [dbo].[lapbul_" + created + "_lapmutasitmpxx] " +
            //"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapbul_" + created + "_lapmutasireport]') AND type in (N'U')) DROP TABLE [dbo].[lapbul_" + created + "_lapmutasireport] " +
            //"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapbul_" + created + "_mutasireport]') AND type in (N'U')) DROP TABLE [dbo].[lapbul_" + created + "_mutasireport] " +
            //"IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapbul_" + created + "_mutasisaldo]') AND type in (N'U')) DROP TABLE [dbo].[lapbul_" + created + "_mutasisaldo] " +
            //"/**/IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lapbul_" + created + "_lapsaldoawal]') AND type in (N'U')) DROP TABLE [dbo].[lapbul_" + created + "_lapsaldoawal] ";
            return strSQL;
        }
        //public string ViewLapBreakBMPlan(string drTgl, string sdTgl, string line)
        //{
        //    string Criteria = string.Empty;
        //    Criteria = (line == "All") ? " and DP is not null" : "and line='line " + line + "' and DP is not null";
        //    //Criteria += (line == "") ? "" : " and PlantID=" + line";
        //    string strQuery;
        //    strQuery = " with BreakDownBM as ( " +
        //             " select line, convert (varchar,TglBreak,103) as TglBreak,TTLPS,RowStatus,convert(varchar,StartBD,108)as StartBD,convert(varchar,FinishBD,108) as FinishBD ,convert(varchar,FinaltyBD,108) as FinaltyBD,Syarat, " +

        //             " 480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) ,cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT1 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP1," +
        //             " 480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) ,cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT2 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP2, " +
        //             " 480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) ,cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT3 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP3, " +
        //             " 480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) ,cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT4 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP4, " +

        //            " case when Pinalti=0 then BDNPMS_M else (BDNPMS_M * pinalti /100) end as BDNPMS_M," +
        //            " case when Pinalti=0 then BDNPMS_E else (BDNPMS_E * Pinalti/100) end as BDNPMS_E," +
        //            " case when Pinalti=0 then BDNPMS_G1 else (BDNPMS_G1 * Pinalti/100) end as BDNPMS_G1," +
        //            " case when Pinalti=0 then BDNPMS_G2 else (BDNPMS_G2 * Pinalti/100) end as BDNPMS_G2, " +
        //            " case when Pinalti=0 then BDNPMS_G3 else (BDNPMS_G3 * Pinalti/100) end as BDNPMS_G3," +
        //            " case when Pinalti=0 then BDNPMS_G4 else (BDNPMS_G4 * Pinalti/100) end as BDNPMS_G4, " +
        //            " case when Pinalti=0 then BDNPMS_L else (BDNPMS_L * Pinalti/100) end as BDNPMS_L, " +
        //            " case when Pinalti=0 then BDNPMS_S else (BDNPMS_S * Pinalti/100) end as BDNPMS_S,Ket,Pinalti,DP,DIC,GroupOff " +

        //            " from ( " +
        //            " select (select PlanName from MasterPlan where ID=A.BM_PlantID) as line, TglBreak,RowStatus, " +
        //            " 1440-isnull( " +
        //            " ( " +
        //            " select sum(DATEDIFF(Minute,StartBD ,FinaltyBD)) " +
        //            " from BreakBM " +
        //            " where breakbm_masterchargeID=4 and BM_PlantID=A.BM_PlantID and BreakBM.TglBreak=A.TglBreak " +
        //            " ),0) as TTLPS, " +
        //            " StartBD,FinishBD,FinaltyBD,Syarat,0 as GP1,0 as GP2,0 as GP3,0 as GP4," +
        //            " case when SUBSTRING(Syarat,1,1)='M' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_M, " +
        //            " case when SUBSTRING(Syarat,1,1)='E' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_E, " +
        //            " case when SUBSTRING(Syarat,1,2)=(select top 1 [group] from " +
        //            " (select top 4 * from BM_PlantGroup  where PlantID =A.BM_PlantID and " +
        //            " LEN([group])>1 order by [group] desc ) as Gr order by [group]) and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G1, " +
        //            " case when SUBSTRING(Syarat,1,2)=(select top 1 [group] from " +
        //            " (select top 3 * from BM_PlantGroup  where PlantID =A.BM_PlantID and " +
        //            " LEN([group])>1 order by [group] desc ) as Gr order by [group]) and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G2," +
        //            " case when	SUBSTRING(Syarat,1,2)=(select top 1 [group] from " +
        //            " (select top 2 * from BM_PlantGroup  where PlantID =A.BM_PlantID and " +
        //            " LEN([group])>1 order by [group] desc ) as Gr order by [group])" +
        //            " and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G3," +
        //            " case when SUBSTRING(Syarat,1,2)=(select top 1 [group] from " +
        //            " (select top 1 * from BM_PlantGroup  where PlantID =A.BM_PlantID and " +
        //            " LEN([group])>1 order by [group] desc ) as Gr order by [group])  and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G4," +
        //            " case when SUBSTRING(Syarat,1,1)='L' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_L,case when SUBSTRING(Syarat,1,2)='KH' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_S,Ket, CAST(Pinalti as decimal(18,2))Pinalti," +
        //            " (select lokasiproblem from breakbmproblem where ID=A.Breakbm_masterproblemID) as DP, " +
        //            " case when LEN(Syarat)=2 then 'BoardMill' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='L' then 'Logistik' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='E' then 'Elektrik' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,2)='KH' then'Schedule'  when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='M' then 'Mekanik' end DIC, " +
        //            " (select top 1 [group] from (select top 4 * from BM_PlantGroup where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT1," +
        //            " (select top 1 [group] from (select top 3 * from BM_PlantGroup where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT2, " +
        //            " (select top 1 [group] from (select top 2 * from BM_PlantGroup where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT3, " +
        //            " (select top 1 [group] from (select top 1 * from BM_PlantGroup where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT4 ,GroupOff " +
        //            " from( " +
        //            " select  isnull(xx.minutex,0) as menit,* from BreakBM  " +
        //            " left join(select x.IDs, DATEDIFF(minute,sbd,finbd) minutex,sbd,finbd from( " +
        //            " select d.ID as IDs, Convert(datetime,tglbreak+startbd) as sbd,StartBD," +
        //            " Case when Cast(startBD as int)>=23 and CAST(FinaltyBD as int)<=1 then convert(datetime,DATEADD(day,1,tglbreak)+FinaltyBD)" +
        //            " else Convert(datetime,TglBreak+FinishBD) end finbd,FinaltyBD,TglBreak " +
        //            " from BreakBM as d where d.RowStatus='0' " +
        //            " )as x " +
        //            " ) as xx on xx.IDs=BreakBM.ID " +
        //            " ) as A " +
        //            " ) " +
        //            " as B where TglBreak between '" + drTgl + "' and ' " + sdTgl + "' " + Criteria +

        //            " ) " +
        //             " SELECT line,TglBreak,TTLPS,StartBD,FinishBD,FinaltyBD,syarat,GroupOff,RowStatus, " +

        //             " CASE WHEN GroupOff='KA' OR GroupOff='KE' OR GroupOff='KI' OR GroupOff='KM' OR GroupOff='KQ' OR GroupOff='KU' THEN (GP1-480) ELSE GP1 END GP1," +
        //             " CASE WHEN GroupOff='KB' OR GroupOff='KF' OR GroupOff='KJ' OR GroupOff='KN' OR GroupOff='KR' OR GroupOff='KV' THEN (GP2-480) ELSE GP2  END GP2," +
        //             " CASE WHEN GroupOff='KC' OR GroupOff='KG' OR GroupOff='KK' OR GroupOff='KO' OR GroupOff='KS' OR GroupOff='KW' THEN (GP3-480) ELSE GP3  END GP3," +
        //             " CASE WHEN GroupOff='KD' OR GroupOff='KH' OR GroupOff='KL' OR GroupOff='KP' OR GroupOff='KT' OR GroupOff='KX' THEN (GP4-480) ELSE GP4 END GP4 , " +
        //             " BDNPMS_M,BDNPMS_E,BDNPMS_G1,BDNPMS_G2,BDNPMS_G3,BDNPMS_G4,BDNPMS_L,Pinalti, " +
        //             " BDNPMS_S,Ket,DP,DIC" +
        //             " From BreakDownBM where RowStatus=0 order by TglBreak,StartBD,line ";

        //    return strQuery;
        //}

        //public string ViewLapBreakBMPlan(string drTgl, string sdTgl, string line, string depo)
        //{
        //    string Criteria = string.Empty;
        //    Criteria = (line == "All") ? " and DP is not null" : "and line='line " + line + "' and DP is not null";
        //    //Criteria += (line == "") ? "" : " and PlantID=" + line";
        //    string strQuery;
        //    if (depo == "1")
        //    {
        //        strQuery = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempBreakDown]') AND type in (N'U')) DROP TABLE [dbo].[TempBreakDown] ; " +
        //                " with BreakDownBM as ( " +
        //                   " select line, convert (varchar,TglBreak,103) as TglBreak,TTLPS,RowStatus,convert(varchar,StartBD,108)as StartBD,convert(varchar,FinishBD,108) as FinishBD ,convert(varchar,FinaltyBD,108) as FinaltyBD,Syarat, " +

        //                   " 480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) ,cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT1 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP1, " +
        //                   " 480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) ,cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT2 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP2, " +
        //                   " 480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) ,cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT3 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP3, " +
        //                   " 480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) ,cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT4 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP4, " +

        //                   " case when Pinalti=0 then BDNPMS_M else (BDNPMS_M * pinalti /100) end as BDNPMS_M, " +
        //                   " case when Pinalti=0 then BDNPMS_E else (BDNPMS_E * Pinalti/100) end as BDNPMS_E, " +
        //                   " case when Pinalti=0 then BDNPMS_U else (BDNPMS_U * Pinalti/100) end as BDNPMS_U, " +
        //                   " case when Pinalti=0 then BDNPMS_G1 else (BDNPMS_G1 * Pinalti/100) end as BDNPMS_G1, " +
        //                   " case when Pinalti=0 then BDNPMS_G2 else (BDNPMS_G2 * Pinalti/100) end as BDNPMS_G2, " +
        //                   " case when Pinalti=0 then BDNPMS_G3 else (BDNPMS_G3 * Pinalti/100) end as BDNPMS_G3," +
        //                   " case when Pinalti=0 then BDNPMS_G4 else (BDNPMS_G4 * Pinalti/100) end as BDNPMS_G4, " +
        //                   " case when Pinalti=0 then BDNPMS_L else (BDNPMS_L * Pinalti/100) end as BDNPMS_L, " +
        //                   " case when Pinalti=0 then BDNPMS_S else (BDNPMS_S * Pinalti/100) end as BDNPMS_S,Ket,Pinalti,DP,DIC,GroupOff " +

        //                   " from ( " +
        //                   " select (select PlanName from MasterPlan where ID=A.BM_PlantID) as line, TglBreak,RowStatus, " +
        //                   " 1440-isnull( " +
        //                   " ( " +
        //                   " select sum(DATEDIFF(Minute,StartBD ,FinaltyBD)) " +
        //                   " from BreakBM " +
        //                   " where breakbm_masterchargeID=4 and BM_PlantID=A.BM_PlantID and BreakBM.TglBreak=A.TglBreak " +
        //                   " ),0) as TTLPS, " +
        //                   " StartBD,FinishBD,FinaltyBD,Syarat,0 as GP1,0 as GP2,0 as GP3,0 as GP4," +
        //                   " case when SUBSTRING(Syarat,1,1)='M' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_M, " +
        //                   " case when SUBSTRING(Syarat,1,1)='E' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_E, " +
        //                   " case when SUBSTRING(Syarat,1,1)='U' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_U, " +
        //                   " case when SUBSTRING(Syarat,1,2)=(select top 1 [group] from " +
        //                   " (select top 4 * from BM_PlantGroup  where PlantID =A.BM_PlantID and " +
        //                   " LEN([group])>1 order by [group] desc ) as Gr order by [group]) and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G1, " +
        //                   "  case when SUBSTRING(Syarat,1,2)=(select top 1 [group] from " +
        //                   "  (select top 3 * from BM_PlantGroup  where PlantID =A.BM_PlantID and " +
        //                   "  LEN([group])>1 order by [group] desc ) as Gr order by [group]) and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G2," +
        //                   "  case when	SUBSTRING(Syarat,1,2)=(select top 1 [group] from " +
        //                   "  (select top 2 * from BM_PlantGroup  where PlantID =A.BM_PlantID and " +
        //                   "  LEN([group])>1 order by [group] desc ) as Gr order by [group])" +
        //                   "  and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G3," +
        //                   "  case when SUBSTRING(Syarat,1,2)=(select top 1 [group] from " +
        //                   "  (select top 1 * from BM_PlantGroup  where PlantID =A.BM_PlantID and " +
        //                  "  LEN([group])>1 order by [group] desc ) as Gr order by [group])  and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G4," +
        //                   "  case when SUBSTRING(Syarat,1,1)='L' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_L,case when SUBSTRING(Syarat,1,2)='KH' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_S,Ket, CAST(Pinalti as decimal(18,2))Pinalti," +
        //                   " (select lokasiproblem from breakbmproblem where ID=A.Breakbm_masterproblemID) as DP, " +
        //                   " case when LEN(Syarat)=2 then 'BoardMill' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='L' then 'Logistik' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='E' then 'Elektrik' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,2)='KH' then'Schedule'  when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='M' then 'Mekanik' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='U' then 'Utility' end DIC,  " +
        //                   " (select top 1 [group] from (select top 4 * from BM_PlantGroup where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT1," +
        //                   " (select top 1 [group] from (select top 3 * from BM_PlantGroup where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT2, " +
        //                   " (select top 1 [group] from (select top 2 * from BM_PlantGroup where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT3, " +
        //                   " (select top 1 [group] from (select top 1 * from BM_PlantGroup where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT4 ,GroupOff " +
        //                   " from(  " +
        //                   " select  isnull(xx.minutex,0) as menit,* from BreakBM  " +
        //                   " left join(select x.IDs, DATEDIFF(minute,sbd,finbd) minutex,sbd,finbd from( " +
        //                   " select d.ID as IDs, Convert(datetime,tglbreak+startbd) as sbd,StartBD," +
        //                   " Case when Cast(startBD as int)>=23 and CAST(FinaltyBD as int)<=1 then convert(datetime,DATEADD(day,1,tglbreak)+FinaltyBD)" +
        //                   " else Convert(datetime,TglBreak+FinishBD) end finbd,FinaltyBD,TglBreak " +
        //                   " from BreakBM as d where d.RowStatus='0' " +
        //                   " )as x " +
        //                   " ) as xx on xx.IDs=BreakBM.ID " +
        //                   " ) as A " +
        //                   " ) " +
        //                   " as B where TglBreak between '" + drTgl + "' and ' " + sdTgl + "' " + Criteria +

        //                   " ) " +
        //                  " SELECT line,TglBreak,TTLPS,StartBD,FinishBD,FinaltyBD,syarat,GroupOff,RowStatus, " +

        //                   " CASE WHEN GroupOff='CA' OR GroupOff='CE' OR GroupOff='CI' OR GroupOff='CM'  THEN (GP1-480) ELSE GP1 END GP1," +
        //                   " CASE WHEN GroupOff='CB' OR GroupOff='CF' OR GroupOff='CJ' OR GroupOff='CN' THEN (GP2-480) ELSE GP2  END GP2," +
        //                   " CASE WHEN GroupOff='CC' OR GroupOff='CG' OR GroupOff='CK' OR GroupOff='CO' THEN (GP3-480) ELSE GP3  END GP3," +
        //                   " CASE WHEN GroupOff='CD' OR GroupOff='CH' OR GroupOff='CL' OR GroupOff='CP'THEN (GP4-480) ELSE GP4 END GP4 , " +
        //                   " BDNPMS_M,BDNPMS_E,BDNPMS_U,BDNPMS_G1,BDNPMS_G2,BDNPMS_G3,BDNPMS_G4,BDNPMS_L,Pinalti, " +
        //                   " BDNPMS_S,Ket,DP,DIC " +
        //                   " into TempBreakDown From BreakDownBM where RowStatus=0 order by TglBreak,StartBD,line  select * from TempBreakDown";
        //        return strQuery;
        //    }
        //    else
        //    {
        //        strQuery = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempBreakDown]') AND type in (N'U')) DROP TABLE [dbo].[TempBreakDown] ; " +
        //            " with BreakDownBM as ( " +
        //            " select line, convert (varchar,TglBreak,103) as TglBreak,TTLPS,RowStatus,convert(varchar,StartBD,108)as StartBD,convert(varchar,FinishBD,108) as FinishBD ,convert(varchar,FinaltyBD,108) as FinaltyBD,Syarat, " +

        //            " 480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) ,cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT1 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP1," +
        //            " 480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) ,cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT2 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP2, " +
        //            " 480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) ,cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT3 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP3, " +
        //            " 480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) ,cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT4 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP4, " +

        //           " case when Pinalti=0 then BDNPMS_M else (BDNPMS_M * pinalti /100) end as BDNPMS_M," +
        //           " case when Pinalti=0 then BDNPMS_E else (BDNPMS_E * Pinalti/100) end as BDNPMS_E," +
        //           " case when Pinalti=0 then BDNPMS_U else (BDNPMS_U * Pinalti/100) end as BDNPMS_U, " +
        //           " case when Pinalti=0 then BDNPMS_G1 else (BDNPMS_G1 * Pinalti/100) end as BDNPMS_G1," +
        //           " case when Pinalti=0 then BDNPMS_G2 else (BDNPMS_G2 * Pinalti/100) end as BDNPMS_G2, " +
        //           " case when Pinalti=0 then BDNPMS_G3 else (BDNPMS_G3 * Pinalti/100) end as BDNPMS_G3," +
        //           " case when Pinalti=0 then BDNPMS_G4 else (BDNPMS_G4 * Pinalti/100) end as BDNPMS_G4, " +
        //           " case when Pinalti=0 then BDNPMS_L else (BDNPMS_L * Pinalti/100) end as BDNPMS_L, " +
        //           " case when Pinalti=0 then BDNPMS_S else (BDNPMS_S * Pinalti/100) end as BDNPMS_S,Ket,Pinalti,DP,DIC,GroupOff " +

        //           " from ( " +
        //           " select (select PlanName from MasterPlan where ID=A.BM_PlantID) as line, TglBreak,RowStatus, " +
        //           " 1440-isnull( " +
        //           " ( " +
        //           " select sum(DATEDIFF(Minute,StartBD ,FinaltyBD)) " +
        //           " from BreakBM " +
        //           " where breakbm_masterchargeID=4 and BM_PlantID=A.BM_PlantID and BreakBM.TglBreak=A.TglBreak " +
        //           " ),0) as TTLPS, " +
        //           " StartBD,FinishBD,FinaltyBD,Syarat,0 as GP1,0 as GP2,0 as GP3,0 as GP4," +
        //           " case when SUBSTRING(Syarat,1,1)='M' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_M, " +
        //           " case when SUBSTRING(Syarat,1,1)='E' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_E, " +
        //           " case when SUBSTRING(Syarat,1,1)='U' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_U, " +
        //           " case when SUBSTRING(Syarat,1,2)=(select top 1 [group] from " +
        //           " (select top 4 * from BM_PlantGroupbr  where PlantID =A.BM_PlantID and " +
        //           " LEN([group])>1 order by [group] desc ) as Gr order by [group]) and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G1, " +
        //           " case when SUBSTRING(Syarat,1,2)=(select top 1 [group] from " +
        //           " (select top 3 * from BM_PlantGroupbr  where PlantID =A.BM_PlantID and " +
        //           " LEN([group])>1 order by [group] desc ) as Gr order by [group]) and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G2," +
        //           " case when	SUBSTRING(Syarat,1,2)=(select top 1 [group] from " +
        //           " (select top 2 * from BM_PlantGroupbr  where PlantID =A.BM_PlantID and " +
        //           " LEN([group])>1 order by [group] desc ) as Gr order by [group])" +
        //           " and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G3," +
        //           " case when SUBSTRING(Syarat,1,2)=(select top 1 [group] from " +
        //           " (select top 1 * from BM_PlantGroupbr where PlantID =A.BM_PlantID and " +
        //           " LEN([group])>1 order by [group] desc ) as Gr order by [group])  and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G4," +
        //           " case when SUBSTRING(Syarat,1,1)='L' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_L,case when SUBSTRING(Syarat,1,2)='KH' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_S,Ket, CAST(Pinalti as decimal(18,2))Pinalti," +
        //           " (select lokasiproblem from breakbmproblem where ID=A.Breakbm_masterproblemID) as DP, " +
        //           " case when LEN(Syarat)=2 then 'BoardMill' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='L' then 'Logistik' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='E' then 'Elektrik' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,2)='KH' then'Schedule'  when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='M' then 'Mekanik' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='U' then 'Utility' end DIC, " +
        //           " (select top 1 [group] from (select top 4 * from BM_PlantGroupbr where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT1," +
        //           " (select top 1 [group] from (select top 3 * from BM_PlantGroupbr where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT2, " +
        //           " (select top 1 [group] from (select top 2 * from BM_PlantGroupbr where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT3, " +
        //           " (select top 1 [group] from (select top 1 * from BM_PlantGroupbr where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT4 ,GroupOff " +
        //           " from( " +
        //           " select  isnull(xx.minutex,0) as menit,* from BreakBM  " +
        //           " left join(select x.IDs, DATEDIFF(minute,sbd,finbd) minutex,sbd,finbd from( " +
        //           " select d.ID as IDs, Convert(datetime,tglbreak+startbd) as sbd,StartBD," +
        //           " Case when Cast(startBD as int)>=23 and CAST(FinaltyBD as int)<=1 then convert(datetime,DATEADD(day,1,tglbreak)+FinaltyBD)" +
        //           " else Convert(datetime,TglBreak+FinishBD) end finbd,FinaltyBD,TglBreak " +
        //           " from BreakBM as d where d.RowStatus='0' " +
        //           " )as x " +
        //           " ) as xx on xx.IDs=BreakBM.ID " +
        //           " ) as A " +
        //           " ) " +
        //           " as B where TglBreak between '" + drTgl + "' and ' " + sdTgl + "' " + Criteria +

        //           " ) " +
        //            " SELECT line,TglBreak,TTLPS,StartBD,FinishBD,FinaltyBD,syarat,GroupOff,RowStatus, " +

        //            " CASE WHEN GroupOff='KA' OR GroupOff='KE' OR GroupOff='KI' OR GroupOff='KM' OR GroupOff='KQ' OR GroupOff='KU' THEN (GP1-480) ELSE GP1 END GP1," +
        //            " CASE WHEN GroupOff='KB' OR GroupOff='KF' OR GroupOff='KJ' OR GroupOff='KN' OR GroupOff='KR' OR GroupOff='KV' THEN (GP2-480) ELSE GP2  END GP2," +
        //            " CASE WHEN GroupOff='KC' OR GroupOff='KG' OR GroupOff='KK' OR GroupOff='KO' OR GroupOff='KS' OR GroupOff='KW' THEN (GP3-480) ELSE GP3  END GP3," +
        //            " CASE WHEN GroupOff='KD' OR GroupOff='KH' OR GroupOff='KL' OR GroupOff='KP' OR GroupOff='KT' OR GroupOff='KX' THEN (GP4-480) ELSE GP4 END GP4 , " +
        //            " BDNPMS_M,BDNPMS_E,BDNPMS_U,BDNPMS_G1,BDNPMS_G2,BDNPMS_G3,BDNPMS_G4,BDNPMS_L,Pinalti, " +
        //            " BDNPMS_S,Ket,DP,DIC" +
        //            " into TempBreakDown From BreakDownBM where RowStatus=0 order by TglBreak,StartBD,line select * from TempBreakDown";
        //        return strQuery;
        //    }


        //}

        public string ViewLapBreakBMPlan(string drTgl, string sdTgl, string line, string depo)
        {
            string Criteria = string.Empty;
            Criteria = (line == "All") ? " and DP is not null" : "and line='line " + line + "' and DP is not null";
            //Criteria += (line == "") ? "" : " and PlantID=" + line";
            string strQuery;
            if (depo == "1")
            {
                strQuery = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempBreakDown]') AND type in (N'U')) DROP TABLE [dbo].[TempBreakDown] ; " +
                        " with BreakDownBM as ( " +
                           " select line, convert (varchar,TglBreak,103) as TglBreak,TTLPS,RowStatus,convert(varchar,StartBD,108)as StartBD,convert(varchar,FinishBD,108) as FinishBD ,convert(varchar,FinaltyBD,108) as FinaltyBD,Syarat, " +

                           " 480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) ,cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT1 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP1, " +
                           " 480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) ,cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT2 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP2, " +
                           " 480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) ,cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT3 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP3, " +
                           " 480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) ,cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT4 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP4, " +

                           " case when Pinalti=0 then BDNPMS_M else round((BDNPMS_M * pinalti /100),0) end as BDNPMS_M, " +
                           " case when Pinalti=0 then BDNPMS_E else round((BDNPMS_E * Pinalti/100),0) end as BDNPMS_E, " +
                           " case when Pinalti=0 then BDNPMS_U else round((BDNPMS_U * Pinalti/100),0) end as BDNPMS_U, " +
                           " case when Pinalti=0 then BDNPMS_G1 else round((BDNPMS_G1 * Pinalti/100),0) end as BDNPMS_G1, " +
                           " case when Pinalti=0 then BDNPMS_G2 else round((BDNPMS_G2 * Pinalti/100),0) end as BDNPMS_G2, " +
                           " case when Pinalti=0 then BDNPMS_G3 else round((BDNPMS_G3 * Pinalti/100),0) end as BDNPMS_G3," +
                           " case when Pinalti=0 then BDNPMS_G4 else round((BDNPMS_G4 * Pinalti/100),0) end as BDNPMS_G4, " +
                           " case when Pinalti=0 then BDNPMS_L else round((BDNPMS_L * Pinalti/100),0) end as BDNPMS_L, " +
                           " case when Pinalti=0 then BDNPMS_S else round((BDNPMS_S * Pinalti/100),0) end as BDNPMS_S,Ket,Pinalti,DP,DIC,GroupOff " +

                           " from ( " +
                           " select (select PlanName from MasterPlan where ID=A.BM_PlantID) as line, TglBreak,RowStatus, " +
                           " 1440-isnull( " +
                           " ( " +
                           " select sum(DATEDIFF(Minute,StartBD ,FinaltyBD)) " +
                           " from BreakBM " +
                           " where breakbm_masterchargeID=4 and BM_PlantID=A.BM_PlantID and BreakBM.TglBreak=A.TglBreak " +
                           " ),0) as TTLPS, " +
                           " StartBD,FinishBD,FinaltyBD,Syarat,0 as GP1,0 as GP2,0 as GP3,0 as GP4," +
                           " case when SUBSTRING(Syarat,1,1)='M' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_M, " +
                           " case when SUBSTRING(Syarat,1,1)='E' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_E, " +
                           " case when SUBSTRING(Syarat,1,1)='U' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_U, " +
                           " case when SUBSTRING(Syarat,1,2)=(select top 1 [group] from " +
                           " (select top 4 * from BM_PlantGroup  where PlantID =A.BM_PlantID and " +
                           " LEN([group])>1 order by [group] desc ) as Gr order by [group]) and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G1, " +
                           "  case when SUBSTRING(Syarat,1,2)=(select top 1 [group] from " +
                           "  (select top 3 * from BM_PlantGroup  where PlantID =A.BM_PlantID and " +
                           "  LEN([group])>1 order by [group] desc ) as Gr order by [group]) and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G2," +
                           "  case when	SUBSTRING(Syarat,1,2)=(select top 1 [group] from " +
                           "  (select top 2 * from BM_PlantGroup  where PlantID =A.BM_PlantID and " +
                           "  LEN([group])>1 order by [group] desc ) as Gr order by [group])" +
                           "  and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G3," +
                           "  case when SUBSTRING(Syarat,1,2)=(select top 1 [group] from " +
                           "  (select top 1 * from BM_PlantGroup  where PlantID =A.BM_PlantID and " +
                          "  LEN([group])>1 order by [group] desc ) as Gr order by [group])  and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G4," +
                           "  case when SUBSTRING(Syarat,1,1)='L' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_L,case when SUBSTRING(Syarat,1,2)='KH' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_S,Ket, CAST(Pinalti as decimal(18,2))Pinalti," +
                           " (select lokasiproblem from breakbmproblem where ID=A.Breakbm_masterproblemID) as DP, " +
                           " case when LEN(Syarat)=2 then 'BoardMill' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='L' then 'Logistik' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='E' then 'Elektrik' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,2)='KH' then'Schedule'  when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='M' then 'Mekanik' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='U' then 'Utility' end DIC,  " +
                           " (select top 1 [group] from (select top 4 * from BM_PlantGroup where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT1," +
                           " (select top 1 [group] from (select top 3 * from BM_PlantGroup where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT2, " +
                           " (select top 1 [group] from (select top 2 * from BM_PlantGroup where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT3, " +
                           " (select top 1 [group] from (select top 1 * from BM_PlantGroup where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT4 ,GroupOff " +
                           " from(  " +
                           " select  isnull(xx.minutex,0) as menit,* from BreakBM  " +
                           " left join(select x.IDs, DATEDIFF(minute,sbd,finbd) minutex,sbd,finbd from( " +
                           " select d.ID as IDs, Convert(datetime,tglbreak+startbd) as sbd,StartBD," +
                           " Case when Cast(startBD as int)>=23 and CAST(FinaltyBD as int)<=1 then convert(datetime,DATEADD(day,1,tglbreak)+FinaltyBD)" +
                           " else Convert(datetime,TglBreak+FinishBD) end finbd,FinaltyBD,TglBreak " +
                           " from BreakBM as d where d.RowStatus='0' " +
                           " )as x " +
                           " ) as xx on xx.IDs=BreakBM.ID " +
                           " ) as A " +
                           " ) " +
                           " as B where TglBreak between '" + drTgl + "' and ' " + sdTgl + "' " + Criteria +

                           " ) " +
                          " SELECT line,TglBreak,TTLPS,StartBD,FinishBD,FinaltyBD,syarat,GroupOff,RowStatus, " +

                           " CASE WHEN GroupOff='CA' OR GroupOff='CE' OR GroupOff='CI' OR GroupOff='CM'  THEN (GP1-480) ELSE GP1 END GP1," +
                           " CASE WHEN GroupOff='CB' OR GroupOff='CF' OR GroupOff='CJ' OR GroupOff='CN' THEN (GP2-480) ELSE GP2  END GP2," +
                           " CASE WHEN GroupOff='CC' OR GroupOff='CG' OR GroupOff='CK' OR GroupOff='CO' THEN (GP3-480) ELSE GP3  END GP3," +
                           " CASE WHEN GroupOff='CD' OR GroupOff='CH' OR GroupOff='CL' OR GroupOff='CP'THEN (GP4-480) ELSE GP4 END GP4 , " +
                           " BDNPMS_M,BDNPMS_E,BDNPMS_U,BDNPMS_G1,BDNPMS_G2,BDNPMS_G3,BDNPMS_G4,BDNPMS_L,Pinalti, " +
                           " BDNPMS_S,Ket,DP,DIC " +
                           " into TempBreakDown From BreakDownBM where RowStatus=0 order by TglBreak,StartBD,line  select * from TempBreakDown";
                return strQuery;
            }
            else if (depo == "7")
            {
                strQuery = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempBreakDown]') AND type in (N'U')) DROP TABLE [dbo].[TempBreakDown] ; " +
                    " with BreakDownBM as ( " +
                    " select line, convert (varchar,TglBreak,103) as TglBreak,TTLPS,RowStatus,convert(varchar,StartBD,108)as StartBD,convert(varchar,FinishBD,108) as FinishBD ,convert(varchar,FinaltyBD,108) as FinaltyBD,Syarat, " +

                    " 480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) ,cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT1 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP1," +
                    " 480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) ,cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT2 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP2, " +
                    " 480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) ,cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT3 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP3, " +
                    " 480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) ,cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT4 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP4, " +

                   " case when Pinalti=0 then BDNPMS_M else round((BDNPMS_M * pinalti /100),0) end as BDNPMS_M," +
                   " case when Pinalti=0 then BDNPMS_E else round((BDNPMS_E * Pinalti/100),0) end as BDNPMS_E," +
                   " case when Pinalti=0 then BDNPMS_U else round((BDNPMS_U * Pinalti/100),0) end as BDNPMS_U, " +
                   " case when Pinalti=0 then BDNPMS_G1 else round((BDNPMS_G1 * Pinalti/100),0) end as BDNPMS_G1," +
                   " case when Pinalti=0 then BDNPMS_G2 else round((BDNPMS_G2 * Pinalti/100),0) end as BDNPMS_G2, " +
                   " case when Pinalti=0 then BDNPMS_G3 else round((BDNPMS_G3 * Pinalti/100),0) end as BDNPMS_G3," +
                   " case when Pinalti=0 then BDNPMS_G4 else round((BDNPMS_G4 * Pinalti/100),0) end as BDNPMS_G4, " +
                   " case when Pinalti=0 then BDNPMS_L else round((BDNPMS_L * Pinalti/100),0) end as BDNPMS_L, " +
                   " case when Pinalti=0 then BDNPMS_S else round((BDNPMS_S * Pinalti/100),0) end as BDNPMS_S,Ket,Pinalti,DP,DIC,GroupOff " +

                   " from ( " +
                   " select (select PlanName from MasterPlan where ID=A.BM_PlantID) as line, TglBreak,RowStatus, " +
                   " 1440-isnull( " +
                   " ( " +
                   " select sum(DATEDIFF(Minute,StartBD ,FinaltyBD)) " +
                   " from BreakBM " +
                   " where breakbm_masterchargeID=4 and BM_PlantID=A.BM_PlantID and BreakBM.TglBreak=A.TglBreak " +
                   " ),0) as TTLPS, " +
                   " StartBD,FinishBD,FinaltyBD,Syarat,0 as GP1,0 as GP2,0 as GP3,0 as GP4," +
                   " case when SUBSTRING(Syarat,1,1)='M' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_M, " +
                   " case when SUBSTRING(Syarat,1,1)='E' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_E, " +
                   " case when SUBSTRING(Syarat,1,1)='U' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_U, " +
                   " case when SUBSTRING(Syarat,1,2)=(select top 1 [group] from " +
                   " (select top 4 * from BM_PlantGroupbr  where PlantID =A.BM_PlantID and " +
                   " LEN([group])>1 order by [group] desc ) as Gr order by [group]) and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G1, " +
                   " case when SUBSTRING(Syarat,1,2)=(select top 1 [group] from " +
                   " (select top 3 * from BM_PlantGroupbr  where PlantID =A.BM_PlantID and " +
                   " LEN([group])>1 order by [group] desc ) as Gr order by [group]) and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G2," +
                   " case when	SUBSTRING(Syarat,1,2)=(select top 1 [group] from " +
                   " (select top 2 * from BM_PlantGroupbr  where PlantID =A.BM_PlantID and " +
                   " LEN([group])>1 order by [group] desc ) as Gr order by [group])" +
                   " and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G3," +
                   " case when SUBSTRING(Syarat,1,2)=(select top 1 [group] from " +
                   " (select top 1 * from BM_PlantGroupbr where PlantID =A.BM_PlantID and " +
                   " LEN([group])>1 order by [group] desc ) as Gr order by [group])  and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G4," +
                   " case when SUBSTRING(Syarat,1,1)='L' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_L,case when SUBSTRING(Syarat,1,2)='KH' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_S,Ket, CAST(Pinalti as decimal(18,2))Pinalti," +
                   " (select lokasiproblem from breakbmproblem where ID=A.Breakbm_masterproblemID) as DP, " +
                   " case when LEN(Syarat)=2 then 'BoardMill' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='L' then 'Logistik' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='E' then 'Elektrik' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,2)='KH' then'Schedule'  when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='M' then 'Mekanik' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='U' then 'Utility' end DIC, " +
                   " (select top 1 [group] from (select top 4 * from BM_PlantGroupbr where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT1," +
                   " (select top 1 [group] from (select top 3 * from BM_PlantGroupbr where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT2, " +
                   " (select top 1 [group] from (select top 2 * from BM_PlantGroupbr where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT3, " +
                   " (select top 1 [group] from (select top 1 * from BM_PlantGroupbr where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT4 ,GroupOff " +
                   " from( " +
                   " select  isnull(xx.minutex,0) as menit,* from BreakBM  " +
                   " left join(select x.IDs, DATEDIFF(minute,sbd,finbd) minutex,sbd,finbd from( " +
                   " select d.ID as IDs, Convert(datetime,tglbreak+startbd) as sbd,StartBD," +
                   " Case when Cast(startBD as int)>=23 and CAST(FinaltyBD as int)<=1 then convert(datetime,DATEADD(day,1,tglbreak)+FinaltyBD)" +
                   " else Convert(datetime,TglBreak+FinishBD) end finbd,FinaltyBD,TglBreak " +
                   " from BreakBM as d where d.RowStatus='0' " +
                   " )as x " +
                   " ) as xx on xx.IDs=BreakBM.ID " +
                   " ) as A " +
                   " ) " +
                   " as B where TglBreak between '" + drTgl + "' and ' " + sdTgl + "' " + Criteria +

                   " ) " +
                    " SELECT line,TglBreak,TTLPS,StartBD,FinishBD,FinaltyBD,syarat,GroupOff,RowStatus, " +

                    " CASE WHEN GroupOff='KA' OR GroupOff='KE' OR GroupOff='KI' OR GroupOff='KM' OR GroupOff='KQ' OR GroupOff='KU' THEN (GP1-480) ELSE GP1 END GP1," +
                    " CASE WHEN GroupOff='KB' OR GroupOff='KF' OR GroupOff='KJ' OR GroupOff='KN' OR GroupOff='KR' OR GroupOff='KV' THEN (GP2-480) ELSE GP2  END GP2," +
                    " CASE WHEN GroupOff='KC' OR GroupOff='KG' OR GroupOff='KK' OR GroupOff='KO' OR GroupOff='KS' OR GroupOff='KW' THEN (GP3-480) ELSE GP3  END GP3," +
                    " CASE WHEN GroupOff='KD' OR GroupOff='KH' OR GroupOff='KL' OR GroupOff='KP' OR GroupOff='KT' OR GroupOff='KX' THEN (GP4-480) ELSE GP4 END GP4 , " +
                    " BDNPMS_M,BDNPMS_E,BDNPMS_U,BDNPMS_G1,BDNPMS_G2,BDNPMS_G3,BDNPMS_G4,BDNPMS_L,Pinalti, " +
                    " BDNPMS_S,Ket,DP,DIC" +
                    " into TempBreakDown From BreakDownBM where RowStatus=0 order by TglBreak,StartBD,line select * from TempBreakDown";
                return strQuery;
            }
            else
            {
                strQuery = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempBreakDown]') AND type in (N'U')) DROP TABLE [dbo].[TempBreakDown] ; " +
                        " with BreakDownBM as ( " +
                           " select line, convert (varchar,TglBreak,103) as TglBreak,TTLPS,RowStatus,convert(varchar,StartBD,108)as StartBD,convert(varchar,FinishBD,108) as FinishBD ,convert(varchar,FinaltyBD,108) as FinaltyBD,Syarat, " +

                           " 480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) ,cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT1 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP1, " +
                           " 480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) ,cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT2 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP2, " +
                           " 480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) ,cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT3 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP3, " +
                           " 480-(isnull((select SUM(DATEDIFF(Minute,cast (StartBD as datetime) ,cast (FinaltyBD as datetime )))from BreakBM where RIGHT( rtrim (Syarat),2) = B.GPAT4 and TglBreak=B.TglBreak and RowStatus>-1 ),0))as GP4, " +

                           " case when Pinalti=0 then BDNPMS_M else round((BDNPMS_M * pinalti /100),0) end as BDNPMS_M, " +
                           " case when Pinalti=0 then BDNPMS_E else round((BDNPMS_E * Pinalti/100),0) end as BDNPMS_E, " +
                           " case when Pinalti=0 then BDNPMS_U else round((BDNPMS_U * Pinalti/100),0) end as BDNPMS_U, " +
                           " case when Pinalti=0 then BDNPMS_G1 else round((BDNPMS_G1 * Pinalti/100),0) end as BDNPMS_G1, " +
                           " case when Pinalti=0 then BDNPMS_G2 else round((BDNPMS_G2 * Pinalti/100),0) end as BDNPMS_G2, " +
                           " case when Pinalti=0 then BDNPMS_G3 else round((BDNPMS_G3 * Pinalti/100),0) end as BDNPMS_G3," +
                           " case when Pinalti=0 then BDNPMS_G4 else round((BDNPMS_G4 * Pinalti/100),0) end as BDNPMS_G4, " +
                           " case when Pinalti=0 then BDNPMS_L else round((BDNPMS_L * Pinalti/100),0) end as BDNPMS_L, " +
                           " case when Pinalti=0 then BDNPMS_S else round((BDNPMS_S * Pinalti/100),0) end as BDNPMS_S,Ket,Pinalti,DP,DIC,GroupOff " +

                           " from ( " +
                           " select (select PlanName from MasterPlan where ID=A.BM_PlantID) as line, TglBreak,RowStatus, " +
                           " 1440-isnull( " +
                           " ( " +
                           " select sum(DATEDIFF(Minute,StartBD ,FinaltyBD)) " +
                           " from BreakBM " +
                           " where breakbm_masterchargeID=4 and BM_PlantID=A.BM_PlantID and BreakBM.TglBreak=A.TglBreak " +
                           " ),0) as TTLPS, " +
                           " StartBD,FinishBD,FinaltyBD,Syarat,0 as GP1,0 as GP2,0 as GP3,0 as GP4," +
                           " case when SUBSTRING(Syarat,1,1)='M' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_M, " +
                           " case when SUBSTRING(Syarat,1,1)='E' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_E, " +
                           " case when SUBSTRING(Syarat,1,1)='U' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_U, " +
                           " case when SUBSTRING(Syarat,1,2)=(select top 1 [group] from " +
                           " (select top 4 * from BM_PlantGroup  where PlantID =A.BM_PlantID and " +
                           " LEN([group])>1 order by [group] desc ) as Gr order by [group]) and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G1, " +
                           "  case when SUBSTRING(Syarat,1,2)=(select top 1 [group] from " +
                           "  (select top 3 * from BM_PlantGroup  where PlantID =A.BM_PlantID and " +
                           "  LEN([group])>1 order by [group] desc ) as Gr order by [group]) and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G2," +
                           "  case when	SUBSTRING(Syarat,1,2)=(select top 1 [group] from " +
                           "  (select top 2 * from BM_PlantGroup  where PlantID =A.BM_PlantID and " +
                           "  LEN([group])>1 order by [group] desc ) as Gr order by [group])" +
                           "  and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G3," +
                           "  case when SUBSTRING(Syarat,1,2)=(select top 1 [group] from " +
                           "  (select top 1 * from BM_PlantGroup  where PlantID =A.BM_PlantID and " +
                          "  LEN([group])>1 order by [group] desc ) as Gr order by [group])  and LEN(Syarat)=2 then menit else 0 end  BDNPMS_G4," +
                           "  case when SUBSTRING(Syarat,1,1)='L' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_L,case when SUBSTRING(Syarat,1,2)='KH' and LEN(Syarat)>2 then menit else 0 end  BDNPMS_S,Ket, CAST(Pinalti as decimal(18,2))Pinalti," +
                           " (select lokasiproblem from breakbmproblem where ID=A.Breakbm_masterproblemID) as DP, " +
                           " case when LEN(Syarat)=2 then 'BoardMill' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='L' then 'Logistik' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='E' then 'Elektrik' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,2)='KH' then'Schedule'  when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='M' then 'Mekanik' when LEN(Syarat)>2 and SUBSTRING(Syarat,1,1)='U' then 'Utility' end DIC,  " +
                           " (select top 1 [group] from (select top 4 * from BM_PlantGroup where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT1," +
                           " (select top 1 [group] from (select top 3 * from BM_PlantGroup where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT2, " +
                           " (select top 1 [group] from (select top 2 * from BM_PlantGroup where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT3, " +
                           " (select top 1 [group] from (select top 1 * from BM_PlantGroup where PlantID =A.BM_PlantID and LEN([group])>1 order by [group] desc ) as Gr order by [group]) as GPAT4 ,GroupOff " +
                           " from(  " +
                           " select  isnull(xx.minutex,0) as menit,* from BreakBM  " +
                           " left join(select x.IDs, DATEDIFF(minute,sbd,finbd) minutex,sbd,finbd from( " +
                           " select d.ID as IDs, Convert(datetime,tglbreak+startbd) as sbd,StartBD," +
                           " Case when Cast(startBD as int)>=23 and CAST(FinaltyBD as int)<=1 then convert(datetime,DATEADD(day,1,tglbreak)+FinaltyBD)" +
                           " else Convert(datetime,TglBreak+FinishBD) end finbd,FinaltyBD,TglBreak " +
                           " from BreakBM as d where d.RowStatus='0' " +
                           " )as x " +
                           " ) as xx on xx.IDs=BreakBM.ID " +
                           " ) as A " +
                           " ) " +
                           " as B where TglBreak between '" + drTgl + "' and ' " + sdTgl + "' " + Criteria +

                           " ) " +
                          " SELECT line,TglBreak,TTLPS,StartBD,FinishBD,FinaltyBD,syarat,GroupOff,RowStatus, " +

                           " CASE WHEN GroupOff='JA' OR GroupOff='JE' OR GroupOff='JI' OR GroupOff='JM'  THEN (GP1-480) ELSE GP1 END GP1," +
                           " CASE WHEN GroupOff='JB' OR GroupOff='JF' OR GroupOff='JJ' OR GroupOff='JN' THEN (GP2-480) ELSE GP2  END GP2," +
                           " CASE WHEN GroupOff='JC' OR GroupOff='JG' OR GroupOff='JK' OR GroupOff='JO' THEN (GP3-480) ELSE GP3  END GP3," +
                           " CASE WHEN GroupOff='JD' OR GroupOff='JH' OR GroupOff='JL' OR GroupOff='JP'THEN (GP4-480) ELSE GP4 END GP4 , " +
                           " BDNPMS_M,BDNPMS_E,BDNPMS_U,BDNPMS_G1,BDNPMS_G2,BDNPMS_G3,BDNPMS_G4,BDNPMS_L,Pinalti, " +
                           " BDNPMS_S,Ket,DP,DIC " +
                           " into TempBreakDown From BreakDownBM where RowStatus=0 order by TglBreak,StartBD,line  select * from TempBreakDown";
                return strQuery;
            }

        }


        public string ViewLapBul2ForAtkOnly(string ketBlnLalu, int yearPeriod, string tgl1, string tgl2, int groupID, string stock)
        {
            string strItemTypeID = string.Empty;
            string strJenisBrg = string.Empty;
            string strStock = string.Empty;
            string sts = "3";
            #region pilih group
            if (groupID == 5)
            {
                strItemTypeID = " and A.ItemTypeID = 3";
                strJenisBrg = "Biaya";
            }
            else
            {
                if (groupID == 4)
                {
                    strItemTypeID = " and A.ItemTypeID = 2";
                    strJenisBrg = "Asset";
                }
                else
                {
                    strItemTypeID = " and A.ItemTypeID = 1";
                    strJenisBrg = "Inventory";
                }
            }
            if (stock == "0")
                strStock = "and  " + strJenisBrg + ".Stock = 0";
            else
                if (stock == "1")
                    strStock = "and  " + strJenisBrg + ".Stock = 9";
                else
                    strStock = " ";
            #endregion
            #region
            //return "SELECT ItemCode,ItemName,UOMCode,StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang,[010],[021],[022],[031],[032],[033],[034]" +
            //",[041],[042],[051],[052],[061],[062],[070],[091],[101],[111],[012],[131],[132],[133] from (SELECT " + strJenisBrg + ".id," +
            //strJenisBrg + ".ItemCode, " + strJenisBrg + ".ItemName, UOM.UOMCode, " +

            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT isnull(sum(" + ketBlnLalu + "),0) FROM SaldoInventory A WHERE ITEMID=" +
            //strJenisBrg + ".id AND YearPeriod=" + yearPeriod + strItemTypeID + ") END StokAwal, " +

            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  ReceiptDetail A inner join receipt B on " +
            //" A.receiptID=B.ID where A.ItemID=" + strJenisBrg + ".ID and A.RowStatus>-1  " + strItemTypeID + " and B.status > -1 AND " +
            //"convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' ) END  Pemasukan, " +

            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail A inner join adjust B on B.id = A.adjustID " +
            //" WHERE A.ItemID = " + strJenisBrg + ".ID AND A.RowStatus>-1  " + strItemTypeID + " AND B.ADJUSTTYPE='tambah' and B.status > -1  " +
            //"and convert(varchar,B.AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,B.AdjustDate,112) <= '" + tgl2 + "' ) END  AdjustTambah, " +

            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail A inner join adjust B on B.id = A.adjustID " +
            //" WHERE A.ItemID = " + strJenisBrg + ".ID AND A.RowStatus>-1  " + strItemTypeID + " AND B.ADJUSTTYPE='kurang' and B.status > -1  " +
            //"and convert(varchar,B.AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,B.AdjustDate,112) <= '" + tgl2 + "' ) END  AdjustKurang, " +

            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(Quantity),0) FROM  ReturPakaiDetail A inner join ReturPakai B on " +
            //" B.ID = A.returID WHERE A.ItemID = " + strJenisBrg + ".ID AND A.RowStatus>-1  " + strItemTypeID + " AND B.status > -1 AND  " +
            //"convert(varchar, B.ReturDate,112) >= '" + tgl1 + "' AND convert(varchar, B.ReturDate,112) <= '" + tgl2 + "' ) END  Retur, " +

            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '010')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [010], " +

            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '021')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [021], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '022')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [022], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '031')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [031], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '032')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [032], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '033')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [033], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '034')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [034], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status=2) " +
            //"AND (C.DeptCode = '041')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [041], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '042')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [042], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status=2) " +
            //"AND (C.DeptCode = '051')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [051], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '052')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [052], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status=2) " +
            //"AND (C.DeptCode = '061')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [061], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '062')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [062], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            //"AND (C.DeptCode = '070')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [070], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '091')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [091], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '101')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [101], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status > - 1) " +
            //"AND (C.DeptCode = '111')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [111], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '012')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [012], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '131')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [131], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '132')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [132], " +
            //"CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT  isnull(sum(A.Quantity),0) FROM PakaiDetail AS A INNER JOIN Pakai AS B " +
            //"ON A.PakaiID = B.ID INNER JOIN Dept AS C ON B.DeptID = C.ID WHERE (A.RowStatus > - 1) " + strItemTypeID + " AND (B.Status =2) " +
            //"AND (C.DeptCode = '133')AND convert(varchar,B.PakaiDate,112) >= '" + tgl1 + "'  AND convert(varchar,B.PakaiDate,112) <= '" +
            //tgl2 + "' and A.itemid= " + strJenisBrg + ".ID) END  [133] " +
            //" FROM  " + strJenisBrg + " INNER JOIN   UOM ON " + strJenisBrg + ".UOMID = UOM.ID WHERE (" + strJenisBrg + ".GroupID = " + groupID +
            //" and left(Inventory.ItemCode,5)='AT-OF'" + strStock +") ) AS AA where (StokAwal>0 or Pemasukan>0 or Retur>0 or AdjustTambah>0 or AdjustKurang>0) ORDER BY ItemCode ";
            #endregion
            #region string query
            string strSQL = "SELECT ItemCode,ItemName,UOMCode,StokAwal,Pemasukan,Retur,AdjustTambah,AdjustKurang " +
            ",isnull([010],0) as[010],isnull([021],0)as [021],isnull([022],0)as [022],isnull([031],0)as [031],isnull([032],0)as [032], " +
            "isnull([033],0)as [033],isnull([034],0)as [034],isnull([041],0)as [041],isnull([042],0)as [042],isnull([051],0)as [051], " +
            "isnull([052],0)as [052],isnull([061],0)as [061],isnull([062],0)as [062],isnull([070],0)as [070],isnull([091],0)as [091], " +
            "isnull([101],0)as [101],isnull([111],0)as [111],isnull([012],0)as [012],isnull([131],0)as [131],isnull([132],0)as [132], " +
            "isnull([133] ,0)as [133],isnull([135],0) as [135],isnull([139],0) as [139],isnull([142],0) as [142]  " +
            "from (SELECT " + strJenisBrg + ".id  as itemid," +
            strJenisBrg + ".ItemCode, " + strJenisBrg + ".ItemName, UOM.UOMCode, " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT isnull(sum(" + ketBlnLalu + "),0) FROM SaldoInventory A WHERE ITEMID=" +
            strJenisBrg + ".id AND YearPeriod=" + yearPeriod + strItemTypeID + ") + " + "(select isnull(SUM(quantity),0) from vw_StockPurchn where ItemID=" +
            strJenisBrg + ".ID and YMD<'" + tgl1 + "' and YM=SUBSTRING('" + tgl1 + "',1,6)) END StokAwal, " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  ReceiptDetail A inner join receipt B on " +
            " A.receiptID=B.ID where A.ItemID=" + strJenisBrg + ".ID and A.RowStatus>-1  " + strItemTypeID + " and B.status > -1 AND " +
            "convert(varchar,ReceiptDate,112) >= '" + tgl1 + "' AND convert(varchar,ReceiptDate,112) <= '" + tgl2 + "' ) END  Pemasukan, " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail A inner join adjust B on B.id = A.adjustID " +
            " WHERE  A.apv>0 and A.ItemID = " + strJenisBrg + ".ID AND A.RowStatus>-1  " + strItemTypeID + " AND B.ADJUSTTYPE='tambah' and B.status > -1  " +
            "and convert(varchar,B.AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,B.AdjustDate,112) <= '" + tgl2 + "' ) END  AdjustTambah, " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(QUANTITY),0) FROM  AdjustDetail A inner join adjust B on B.id = A.adjustID " +
            " WHERE  A.apv>0 and A.ItemID = " + strJenisBrg + ".ID AND A.RowStatus>-1  " + strItemTypeID + " AND B.ADJUSTTYPE='kurang' and B.status > -1  " +
            "and convert(varchar,B.AdjustDate,112) >= '" + tgl1 + "' AND convert(varchar,B.AdjustDate,112) <= '" + tgl2 + "' ) END  AdjustKurang, " +
            "CASE WHEN " + strJenisBrg + ".ID > 0 THEN (SELECT ISNULL(SUM(Quantity),0) FROM  ReturPakaiDetail A inner join ReturPakai B on " +
            " B.ID = A.returID WHERE A.ItemID = " + strJenisBrg + ".ID AND A.RowStatus>-1  " + strItemTypeID + " AND B.status > -1 AND  " +
            "convert(varchar, B.ReturDate,112) >= '" + tgl1 + "' AND convert(varchar, B.ReturDate,112) <= '" + tgl2 + "' ) END  Retur " +
            "FROM  " + strJenisBrg + " INNER JOIN UOM ON " + strJenisBrg + ".UOMID = UOM.ID WHERE (" + strJenisBrg + ".GroupID = " +
            groupID + strStock + " )) AS AA left join " +
            "(select  itemid1=case when grouping(ItemID)=1 then 0 else 1 end, itemid, " +
            "sum([010]) as [010],sum([021]) as[021],sum([022]) as[022],sum([031]) as[031],sum([032]) as[032],sum([033]) as[033]  " +
            ",sum([034]) as[034],sum([041]) as[041],sum([042]) as[042],sum([051]) as[051],SUM([052]) as[052],SUM([061]) as[061],SUM([062]) as[062], " +
            "SUM([070]) as[070],SUM([091]) as[091],SUM([101]) as[101],SUM([111]) as[111],SUM([012]) as[012],SUM([131]) as[131],SUM([132]) as[132], " +
            "sum([133]) as[133],sum([135]) as [135],sum([139]) as [139],sum([142]) as [142] " +
            "from ( " +
            "select  ItemID,sum(Quantity) as [010], 0 as [021] ,0 as [022],0 as [031],0 as [032],0 as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai  " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID  " +
            "in(select ID from Dept where DeptCode='137')) group by ItemID  union all " +

            "select ItemID,0 as [010], sum(Quantity)  as [021] ,0 as [022],0 as [031],0 as [032],0 as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + "  and RowStatus>=0 and PakaiID in(select ID from Pakai  " +
            "where status>=" + sts + " and  convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='021')) group by ItemID union all " +

            "select ItemID,0 as [010], 0 as [021] ,sum(Quantity) as [022],0 as [031],0 as [032],0 as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + "  and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='022')) group by ItemID union all " +

            "select ItemID,0 as [010], 0 as [021] ,0 as [022],sum(Quantity) as [031],0 as [032],0 as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + "  and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='031')) group by ItemID union all " +

            "select ItemID,0 as [010], 0 as [021] ,0 as [022],0 as [031],sum(Quantity) as  [032],0 as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + "  and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='032')) group by ItemID union " +

            "select ItemID,0 as [010], 0 as [021] ,0 as [022],0 as [031],0 as  [032],sum(Quantity) as [033],0 as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + "  and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='033')) group by ItemID union all " +

            "select ItemID,0 as [010], 0 as [021] ,0 as [022],0 as [031], 0 as  [032],0 as [033],sum(Quantity) as [034],0 as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='034')) group by ItemID union all " +

            "select ItemID,0 as [010], 0 as [021] ,0 as [022],0 as [031],0 as  [032],0 as [033],0 as [034],sum(Quantity) as [041],0 as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='041')) group by ItemID union all " +

            "select ItemID,0 as [010], 0 as [021] ,0 as [022],0 as [031],0 as  [032],0 as [033],0 as [034],0 as [041],sum(Quantity) as [042], " +
            "0 as [051],0 as [052],0 as [061],0 as [062],0 as [070],0 as [091],0 as [101],0 as [111],0 as [012],0 as [131],0 as [132],0 as [133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='042')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],sum(Quantity) as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='051')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "sum(Quantity) as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='052')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],sum(Quantity) as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='061')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],sum(Quantity) as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='062')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],sum(Quantity) as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='070')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],sum(Quantity) as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='091')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],sum(Quantity) as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='101')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],sum(Quantity) as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='111')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],sum(Quantity) as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='012')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],sum(Quantity) as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='134')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],sum(Quantity) as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='133')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],sum(Quantity) as[132],0 as[133] " +
            ",0 as [135],0 as [139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode in('132','131'))) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],sum(Quantity) as[139],0 as [142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode='139')) group by ItemID union all " +

            "select ItemID,0 as[010], 0 as[021] ,0 as[022],0 as[031],0 as[032],0 as[033],0 as[034],0 as[041],0 as[042],0 as[051], " +
            "0 as[052],0 as[061],0 as[062],0 as[070],0 as[091],0 as[101],0 as[111],0 as[012],0 as[131],0 as[132],0 as[133] " +
            ",0 as [135],0 as [139],sum(Quantity) as[142] from PakaiDetail A where A.groupID=" + groupID + " " + strItemTypeID + " and RowStatus>=0 and PakaiID in(select ID from Pakai   " +
            "where status>=" + sts + " and convert(varchar,PakaiDate,112) >='" + tgl1 + "' and  convert(varchar,PakaiDate,112) <='" + tgl2 + "' and DeptID   " +
            "in(select ID from Dept where DeptCode in ('141','142'))) group by ItemID union all " +

            LapBullDept("135", tgl1, tgl2, groupID, strItemTypeID, sts) + " ) as pemakaian group by itemid with rollup " +
            ") as AB on AB.ItemID =AA.ItemID where (AA.StokAwal >0 or AA.Pemasukan>0 or AA.Retur>0 or AA.AdjustTambah>0 or AA.AdjustKurang>0)  " +
            "ORDER BY ItemCode";
            #endregion
            return strSQL;
        }
        public string ViewRekapPakai3(string tgl1, string tgl2, int groupID, string alias)
        {
            string strGroupID = string.Empty;
            string strDeptid = string.Empty;
            if (groupID > 0)
            {
                strGroupID = " and PakaiDetail.GroupID=" + groupID;
                strDeptid = (alias == "-- ALL --") ? string.Empty : " and Pakai.DeptID in (select ID from dept where alias='" + alias + "')";
            }
            else
            {
                strDeptid = (groupID == 0) ? string.Empty : " and PakaiDetail.GroupID=" + groupID;
                strDeptid = (alias == "-- ALL --") ? string.Empty : " and Pakai.DeptID in (select ID from dept where alias='" + alias + "')";
            }


            return "select PakaiNo,convert(varchar,PakaiDate,103) as PakaiDate,ItemCode,ItemName,UOMCode,SUM(Quantity) as Jumlah," +
                "isnull(Harga,0) as Harga,Keterangan,GroupID,GroupDescription,DeptName,Status from (" +
                "SELECT CASE PakaiDetail.ItemTypeID  WHEN 1 THEN (SELECT ItemCode FROM Inventory WHERE ID = PakaiDetail.ItemID)  " +
                "WHEN 2 THEN (SELECT ItemCode FROM Asset WHERE ID = PakaiDetail.ItemID)  ELSE " +
                "(SELECT ItemCode FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS ItemCode,  " +
                "CASE PakaiDetail.ItemTypeID  WHEN 1 THEN (SELECT ItemName FROM Inventory WHERE ID = PakaiDetail.ItemID)  " +
                "WHEN 2 THEN (SELECT ItemName FROM Asset WHERE ID = PakaiDetail.ItemID)  ELSE " +
                "(SELECT ItemName FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS ItemName,  " +
                "CASE PakaiDetail.ItemTypeID  WHEN 1 THEN (SELECT GroupID FROM Inventory WHERE ID = PakaiDetail.ItemID)  " +
                "WHEN 2 THEN (SELECT GroupID FROM Asset WHERE ID = PakaiDetail.ItemID)  ELSE " +
                "(SELECT GroupID FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS GroupID,  " +
                "CASE WHEN PakaiDetail.ItemID>0 THEN (SELECT top 1 isnull(POPurchnDetail.Price,0) FROM POPurchn,POPurchnDetail  " +
                "WHERE POPurchn.ID=POPurchnDetail.POID and POPurchnDetail.ItemID=PakaiDetail.ItemID and POPurchnDetail.GroupID=PakaiDetail.GroupID " +
                "and POPurchn.Status>-1 and " +
                "POPurchnDetail.Status>-1 order by POPurchnDate Desc) else 0 end Harga," +
                "CASE when PakaiDetail.GroupID>0 THEN (SELECT GroupDescription FROM GroupsPurchn WHERE GroupsPurchn.ID = PakaiDetail.GroupID)  " +
                "ELSE 'xxx' END AS GroupDescription,  " +
                "CASE Pakai.Status  WHEN 0 THEN ('Open')  " +
                "WHEN 1 THEN ('Head')  ELSE " +
                "('Gudang') END AS Status," +
                "PakaiDetail.Quantity,PakaiDetail.Keterangan,Pakai.PakaiNo,UOM.UOMCode,Pakai.PakaiDate,Pakai.DeptID,Dept.DeptName FROM Pakai " +
                "INNER JOIN PakaiDetail ON Pakai.ID = PakaiDetail.PakaiID and PakaiDetail.RowStatus>-1 " +
                "INNER JOIN UOM ON PakaiDetail.UomID = UOM.ID and UOM.RowStatus>-1 " +
                "INNER JOIN Dept ON Pakai.DeptID = Dept.ID and Dept.RowStatus>-1 " +
                "where Pakai.status=3 and convert(varchar,Pakai.PakaiDate,112)>='" + tgl1 + "' and  convert(varchar,Pakai.PakaiDate,112)<=" + tgl2 +
                strDeptid + strGroupID + ") as AA group by DeptName,ItemCode,ItemName,UOMCode,Harga,GroupID,GroupDescription,PakaiNo,PakaiDate,Status,Keterangan order by GroupID,ItemCode";
        }
        public string ViewRekapSPB1(string tgl1, string tgl2, int GroupID, string alias, int VP)
        {
            string strGroupID = string.Empty;
            string strDeptid = string.Empty;
            string SaldoAwal = string.Empty;
            string strAll = string.Empty;
            string ItemIDKertas = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ItemKertas" + ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID.ToString(), "SPB");
            int Bulan = int.Parse(tgl1.Substring(4, 2));
            int Tahun = int.Parse(tgl1.Substring(0, 4));
            if (Bulan == 1) { Tahun = Tahun - 1; }
            string jam = DateTime.Now.ToString("yyMMss");
            string UserView = ((Users)System.Web.HttpContext.Current.Session["Users"]).ID.ToString() + "_RPA" + jam;
            SaldoAwal = Global.nBulan((Bulan == 1) ? 12 : Bulan - 1, true) + "AvgPrice";
            string Groupsss = (GroupID > 0) ? " and GroupID in(" + GroupID + ")" : "";
            strGroupID = (GroupID > 0) ? " and PakaiDetail.GroupID=" + GroupID : string.Empty;
            strDeptid = (alias == "-- ALL --") ? string.Empty : " and Pakai.DeptID in (select ID from dept where alias='" + alias + "')";
            int itemType = 0;
            switch (GroupID)
            {
                case 6: itemType = 2; break;//asset
                case 5: itemType = 3; break;//biaya
                default: itemType = 1; break;//inventory
            }
            #region proses posting avgprice
            string sqlposting = "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto_lapmutasitmp_" + UserView + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto_lapmutasitmp_" + UserView + "]  " +
                                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto_lapmutasitmpx_" + UserView + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto_lapmutasitmpx_" + UserView + "]  " +
                                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto_lapmutasitmpxx_" + UserView + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto_lapmutasitmpxx_" + UserView + "]  " +
                                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto_lapmutasireport_" + UserView + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto_lapmutasireport_" + UserView + "]  " +
                                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto_mutasireport_" + UserView + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto_mutasireport_" + UserView + "]  " +
                                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto_mutasisaldo_" + UserView + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto_mutasisaldo_" + UserView + "]  " +
                                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto_lapsaldoawal_" + UserView + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto_lapsaldoawal_" + UserView + "]  " +
                                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto_mutasireport_" + UserView + "1]') AND type in (N'U')) DROP TABLE [dbo].[Auto_mutasireport_" + UserView + "1]  " +
                                "/** Kumpulkan data dari beberapa tabel dengan UNION ALL all */  " +
                                "declare @thbln char(6)  " +
                                "set @thbln=LEFT(convert(char,getdate(),112),6) " +
                                "declare @thnbln0 varchar(6)  " +
                                "declare @tgl datetime  " +
                                "declare @itemtypeID int " +
                                "set @itemtypeID= " + itemType +
                                "set @tgl=CAST( (@thbln+ '01') as datetime)  " +
                                "set @tgl= DATEADD(month,-1,@tgl)  " +
                                "set @thnbln0=LEFT(convert(char,@tgl,112),6)  " +
                                "declare @thnAwal  varchar(4) " +
                                "declare @blnAwal varchar(2) " +
                                "declare @AwalQty varchar(7) " +
                                "declare @AwalAvgPrice varchar(11) " +
                                "declare @tglawal varchar(8) " +
                                "set @tglawal='01-' + right(@thbln,2)+'-'+ LEFT(@thbln,4)  " +
                                "set @thnAwal =left(@thnbln0,4) " +
                                "set @blnAwal=RIGHT(@thnbln0,2) " +
                                "if right(@blnAwal,2)='01' begin set @AwalQty='janqty' set @AwalAvgPrice='janAvgprice'  end " +
                                "if right(@blnAwal,2)='02' begin set @AwalQty='febqty' set @AwalAvgPrice='febAvgprice'  end " +
                                "if right(@blnAwal,2)='03' begin set @AwalQty='marqty' set @AwalAvgPrice='marAvgprice'  end " +
                                "if right(@blnAwal,2)='04' begin set @AwalQty='aprqty' set @AwalAvgPrice='aprAvgprice'  end " +
                                "if right(@blnAwal,2)='05' begin set @AwalQty='meiqty' set @AwalAvgPrice='meiAvgprice'  end " +
                                "if right(@blnAwal,2)='06' begin set @AwalQty='junqty' set @AwalAvgPrice='junAvgprice'  end " +
                                "if right(@blnAwal,2)='07' begin set @AwalQty='julqty' set @AwalAvgPrice='julAvgprice'  end " +
                                "if right(@blnAwal,2)='08' begin set @AwalQty='aguqty' set @AwalAvgPrice='aguAvgprice'  end " +
                                "if right(@blnAwal,2)='09' begin set @AwalQty='sepqty' set @AwalAvgPrice='sepAvgprice'  end " +
                                "if right(@blnAwal,2)='10' begin set @AwalQty='oktqty' set @AwalAvgPrice='oktAvgprice'  end " +
                                "if right(@blnAwal,2)='11' begin set @AwalQty='novqty' set @AwalAvgPrice='novAvgprice'  end " +
                                "if right(@blnAwal,2)='12' begin set @AwalQty='desqty' set @AwalAvgPrice='desAvgprice'  end " +
                                "declare @thnCur  varchar(4) " +
                                "declare @blnCur varchar(2) " +
                                "declare @CurQty varchar(7) " +
                                "declare @CurAvgPrice varchar(11) " +
                                "set  @thnCur =left(@thbln,4) " +
                                "set @blnCur=RIGHT(@thbln,2) " +
                                "if right(@blnCur,2)='01' begin set @CurQty='janqty' set @CurAvgPrice='janAvgprice'  end " +
                                "if right(@blnCur,2)='02' begin set @CurQty='febqty' set @CurAvgPrice='febAvgprice'  end " +
                                "if right(@blnCur,2)='03' begin set @CurQty='marqty' set @CurAvgPrice='marAvgprice'  end " +
                                "if right(@blnCur,2)='04' begin set @CurQty='aprqty' set @CurAvgPrice='aprAvgprice'  end " +
                                "if right(@blnCur,2)='05' begin set @CurQty='meiqty' set @CurAvgPrice='meiAvgprice'  end " +
                                "if right(@blnCur,2)='06' begin set @CurQty='junqty' set @CurAvgPrice='junAvgprice'  end " +
                                "if right(@blnCur,2)='07' begin set @CurQty='julqty' set @CurAvgPrice='julAvgprice'  end " +
                                "if right(@blnCur,2)='08' begin set @CurQty='aguqty' set @CurAvgPrice='aguAvgprice'  end " +
                                "if right(@blnCur,2)='09' begin set @CurQty='sepqty' set @CurAvgPrice='sepAvgprice'  end " +
                                "if right(@blnCur,2)='10' begin set @CurQty='oktqty' set @CurAvgPrice='oktAvgprice'  end " +
                                "if right(@blnCur,2)='11' begin set @CurQty='novqty' set @CurAvgPrice='novAvgprice'  end " +
                                "if right(@blnCur,2)='12' begin set @CurQty='desqty' set @CurAvgPrice='desAvgprice'  end " +
                                "declare @sqlP nvarchar(max) " +
                                "set @sqlP='SELECT * into Auto_lapmutasitmp_" + UserView + " FROM(  " +
                                "(SELECT ''0'' AS Tipe,'''+@tglawal+''' AS Tanggal,''Saldo Awal'' AS DocNo,si.ItemID,si.'+@AwalAvgPrice+' AS SaldoHS,  " +
                                "si.'+@AwalQty+' AS SaldoQty,CASE WHEN ISNULL(si.'+@AwalAvgPrice+',0) >0 THEN si.'+@AwalAvgPrice+' ELSE 0 END AvgPrice,(si.'+@AwalQty+'*si.'+@AwalAvgPrice+') AS TotalPrice  " +
                                "FROM SaldoInventory AS si WHERE si.YearPeriod='+ @thnAwal +'  AND si.ItemTypeID=''1'' and ( " +
                                "si.ItemID IN(select distinct ItemID from vw_StockPurchn where YM='+@thbln+' " + Groupsss + " and (AvgPrice<=0 or Avgprice is null))) )  " +
                                "UNION ALL  " +
                                "(SELECT ''1'' AS Tipe,CONVERT(CHAR,Tanggal,105) AS Tanggal,ReceiptNo,ItemID, " +
                                "CASE WHEN x.Price >0 THEN " + //jika price=0
                                " CASE WHEN x.crc >1 then CASE WHEN x.Flag =2 Then  " + //jika kurs bukan rp dan supp harus bayar dolar ambil dari Matauangkurs
                                "  CASE WHEN MONTH(x.Tanggal)>8 AND YEAR(x.Tanggal) >=2016 THEN " + //di tambahkan untuk transaksi stlah bln ags 2016
                                "       (x.NilaiKurs * x.Price) ELSE " + //kurs diambil dari nilai kurs
                                "   ((select top 1 isnull(kurs,1)kurs from matauangkurs where muID=x.crc and drTgl =x.Tanggal )* x.Price) END ELSE " +
                                "   CASE WHEN x.NilaiKurs >0  " + //jika nilaikurs di table popurchn >0 kalikan dengan nilai kurs
                                "       THEN (x.NilaiKurs * x.Price) ELSE " + // jika nilai kurs =0 ambil dari table mataunga kurs base on tgl receipt
                                "       ((select top 1 isnull(kurs,1)kurs from matauangkurs where muID=x.crc and drTgl =x.Tanggal)* x.Price)  " +
                                "       END END ELSE x.Price END " +
                                "ELSE CASE WHEN x.Crc <=1 THEN x.HargaSatuan END END Price,Quantity,  " +
                                "CASE WHEN x.Price > 0 THEN " +
                                "CASE WHEN (x.Crc >1 ) THEN CASE WHEN x.flag=2 THEN " +
                                "  CASE WHEN MONTH(x.Tanggal)>8 AND YEAR(x.Tanggal) >=2016 THEN " +
                                "       (x.NilaiKurs * x.Price) ELSE ( " +
                                "           (select top 1 isnull(kurs,1)kurs from MataUangKurs where muID=x.crc and drTgl =x.Tanggal)*(isnull((x.Price),0))) " +
                                "       END ELSE CASE WHEN x.NilaiKurs>0 THEN (x.NilaiKurs * x.Price) ELSE  " +
                                "((select top 1 isnull(kurs,1)kurs from MataUangKurs where muID=x.crc and drTgl =x.Tanggal)* " +
                                "(isnull((x.Price),0))) END END ELSE isnull((x.Price),0) END ELSE CASE WHEN(x.Crc <=1) THEN  " +
                                "x.HargaSatuan END END AvgPrice," +
                                "CASE WHEN(x.Crc >1 ) THEN CASE WHEN x.flag=2 THEN  " +
                                "  CASE WHEN MONTH(x.Tanggal)>8 AND YEAR(x.Tanggal) >=2016 THEN " +
                                "       (x.NilaiKurs * x.Price) ELSE " +
                                "((select top 1 isnull(kurs,1)kurs from MataUangKurs where muID=x.crc and drTgl =x.Tanggal)* " +
                                "(isnull((x.Price*x.Quantity),0))) END ELSE  " +
                                "CASE WHEN x.NilaiKurs>0 THEN (x.NilaiKurs * x.Price*x.Quantity) ELSE  " +
                                "((select top 1 isnull(kurs,1)kurs from MataUangKurs where muID=x.crc and drTgl = x.Tanggal)*(isnull((x.Price*x.Quantity),0)))  " +
                                "END END ELSE CASE WHEN x.Price>0 THEN isnull((x.Price*x.Quantity),0) ELSE (x.HargaSatuan * x.Quantity)  " +
                                "END END TotalPrice " +
                                "FROM( SELECT ''1'' as Tipe, p.ReceiptDate as Tanggal,p.ReceiptNo ,rd.ItemID,  " +
                                "Case When pod.Price=0 then pod.Price2 else pod.Price end Price,rd.Quantity,  " +
                                "pod.crc,pod.flag,pod.NilaiKurs,ISNULL(pod.SubCompanyID,0)SubCompanyID,p.SupplierID,  " +
                                "(pod.Price*rd.Quantity) as TotalPrice,rd.POID,rd.ID as ReceiptID,pod.Price2 as HargaSatuan,bo.Qty  " +
                                "FROM Receipt as p LEFT JOIN ReceiptDetail as rd on rd.ReceiptID=p.ID LEFT JOIN vw_PObukanRP as pod on rd.PODetailID=pod.PODetailID  " +
                                "LEFT JOIN TabelHargaBankOut AS bo ON bo.ReceiptDetailID=rd.ID WHERE (left(convert(varchar,p.ReceiptDate,112),6)='+ @thbln +')  " +
                                "AND rd.ItemID IN (select distinct ItemID from vw_StockPurchn where YM='+@thbln+' " + Groupsss + " )  " +
                                "AND p.Status >-1 AND rd.RowStatus >-1 ) as x )  " +

                                "UNION ALL  " +
                                "(SELECT ''2'' AS Tipe,cONvert(VARCHAR,k.PakaiDate,105) AS Tanggal,k.PakaiNo,pk.ItemID,ISNULL(pk.AvgPrice,0)AvgPrice,pk.Quantity,  " +
                                "ISNULL(pk.AvgPrice,0) AS AvgPrice,(pk.Quantity*ISNULL(pk.AvgPrice,0)) AS TotalPrice FROM Pakai AS k LEFT JOIN PakaiDetail AS pk ON pk.PakaiID=k.ID  " +
                                "WHERE (left(cONvert(VARCHAR,k.PakaiDate,112),6)='+ @thbln +') AND pk.ItemID IN( " +
                                "select distinct ItemID from vw_StockPurchn where YM='+@thbln+' " + Groupsss + " and (AvgPrice<=0 or Avgprice is null))AND k.Status >-1 AND pk.RowStatus >-1 )  " +
                                "UNION ALL  " +
                                "(SELECT ''3'' AS Tipe, CONVERT(VARCHAR,rp.ReturDate,105) AS Tanggal,rp.ReturNo,rpd.ItemID,rpd.AvgPrice,rpd.Quantity,  " +
                                "(rpd.AvgPrice) AS AvgPrice,(rpd.Quantity*rpd.AvgPrice) AS TotalPrice FROM ReturPakai AS rp LEFT JOIN ReturPakaiDetail AS rpd  " +
                                "ON rpd.ReturID=rp.ID WHERE (left(cONvert(VARCHAR,rp.ReturDate,112),6)='+ @thbln +')  " +
                                "AND rpd.ItemID IN(select distinct ItemID from vw_StockPurchn where YM='+@thbln+' " + Groupsss + " and (AvgPrice<=0 or Avgprice is null)) AND rp.Status >-1 AND rpd.RowStatus >-1 )  " +
                                "UNION ALL  " +
                                "(SELECT ''4'' AS Tipe,CONVERT(VARCHAR,a.AdjustDate,105) AS Tanggal,  " +
                                "CASE when a.AdjustType=''Tambah'' THEN (a.AdjustNo)ELSE a.AdjustNo END NoM ,ad.ItemID,ad.AvgPrice As SaldoHS,  " +
                                "CASE When a.AdjustType=''Tambah'' THEN ad.Quantity ELSE ad.Quantity END AdjQty,''0'' AS AvgPrice, (ad.Quantity*ad.AvgPrice) AS TotalPrice  " +
                                "FROM Adjust AS a LEFT JOIN AdjustDetail AS ad ON ad.AdjustID=a.ID WHERE (left(cONvert(VARCHAR,a.AdjustDate,112),6)='+ @thbln +')  " +
                                "AND a.AdjustType=''Tambah'' AND ad.ItemID IN(select distinct ItemID from vw_StockPurchn where YM='+@thbln+' " + Groupsss + " and (AvgPrice<=0 or Avgprice is null))  " +
                                "AND a.Status >-1 AND ad.RowStatus >-1 ) " +
                                "UNION ALL  " +
                                "(SELECT ''5'' AS Tipe,CONVERT(VARCHAR,a.AdjustDate,105)  " +
                                "AS Tanggal, CASE when a.AdjustType=''Kurang'' THEN (a.AdjustNo)ELSE a.AdjustNo END NoM ,ad.ItemID,ad.AvgPrice,  " +
                                "CASE When a.AdjustType=''Kurang'' THEN ad.Quantity ELSE ad.Quantity END AdjQty,''0'' AS AvgPriceK, (ad.Quantity*ad.AvgPrice) AS TotalPriceK  " +
                                "FROM Adjust AS a LEFT JOIN AdjustDetail AS ad ON ad.AdjustID=a.ID WHERE (left(cONvert(VARCHAR,a.AdjustDate,112),6)='+ @thbln +')  " +
                                "AND a.AdjustType=''Kurang'' AND ad.ItemID IN(select distinct ItemID from vw_StockPurchn where YM='+@thbln+' " + Groupsss + " and (AvgPrice<=0 or Avgprice is null))  " +
                                "AND a.Status >-1 AND ad.RowStatus >-1 ) " +
                                "UNION ALL  " +
                                "(SELECT ''6'' AS Tipe,CONVERT(VARCHAR,rs.ReturDate,105) as Tanggal,rs.ReceiptNo,rsd.ItemID,rsd.Quantity as AvgPrice,rsd.Quantity,  " +
                                "CAST(''0'' AS Decimal(18,6)) AS AvgPrice ,CAST(''0'' AS Decimal(18,6)) AS Totalprice FROM ReturSupplier AS rs LEFT JOIN ReturSupplierDetail AS rsd  " +
                                "ON rsd.ReturID=rs.ID where (left(convert(varchar,rs.ReturDate,112),6)='+ @thbln +')  " +
                                "AND rsd.ItemID IN(select distinct ItemID from vw_StockPurchn where YM='+@thbln+' " + Groupsss + " and (AvgPrice<=0 or Avgprice is null)) AND rs.Status >-1  " +
                                "AND rsd.RowStatus >-1 ) ) as K' " +
                                "exec sp_executesql @sqlP, N'' " +
                                "/** susun sesuai dengan kolom laporan */  " +
                                "SELECT ID,Tipe,ItemID,Tanggal,DocNo,SaldoAwalQty,HPP,(SaldoAwalQty*HPP) AS SaAmt, BeliQty,BeliHS,(BeliQty*BeliHS) as BeliAmt,  " +
                                "AdjustQty,AdjustHS AS AdjustHS,(AdjustQty*HPP) as AdjAmt, ReturnQty,ReturHS AS ReturHS,(ReturnQty*HPP) as RetAmt,  " +
                                "ProdQty,ProdHS AS ProdHS,(ProdQty*HPP) as ProdAmt, AdjProdQty,AdjProdHS AS AdjProdHS,(AdjProdQty*HPP) as AdjPAmt,RetSupQty,  " +
                                "RetSupHS,(RetSupQty*RetSupHS) as RetSupAmt, (SaldoAwalQty+BeliQty+AdjustQty-ProdQty-AdjProdQty+ReturnQty-RetSupQty) as TotalQty,  " +
                                "((SaldoAwalQty*HPP)+(BeliQty*BeliHS)+(ReturnQty*ReturHS )+(AdjustQty*AdjustHS)- (ProdQty*ProdHS)-(AdjProdQty*AdjProdHS)-(RetSupQty*RetSupHs)) as TotalAmt  " +
                                "INTO Auto_lapmutasitmpx_" + UserView + " FROM ( SELECT ROW_NUMBER() OVER(ORDER By Tanggal,Tipe,DocNo) as ID,Tipe,itemID,Tanggal,DocNo,  " +
                                "CASE WHEN Tipe='0' THEN ISNULL(SaldoQty,0) ELSE 0 END SaldoAwalQty, CASE WHEN Tipe='0' THEN ISNULL(SaldoHS,0) ELSE 0 END HPP,  " +
                                "CASE WHEN Tipe='0' THEN ISNULL(TotalPrice,0) ELSE 0 END TotalPa, CASE WHEN Tipe='1' THEN ISNULL(SaldoQty,0) ELSE 0 END BeliQty,  " +
                                "CASE WHEN Tipe='1' THEN ISNULL(SaldoHS,0) ELSE 0 END BeliHS, CASE WHEN Tipe='1' THEN ISNULL(TotalPrice,0) ELSE 0 END TotalPB,  " +
                                "CASE WHEN Tipe='2' THEN ISNULL(SaldoQty,0) ELSE 0 END ProdQty, CASE WHEN Tipe='2' THEN ISNULL(SaldoHS,0) ELSE 0 END ProdHS,  " +
                                "CASE WHEN Tipe='2' THEN ISNULL(TotalPrice,0) ELSE 0 END TotalPAd, CASE WHEN Tipe='3' THEN ISNULL(SaldoQty,0) ELSE 0 END ReturnQty,  " +
                                "CASE WHEN Tipe='3' THEN ISNULL(SaldoHS,0) ELSE 0 END ReturHS, CASE WHEN Tipe='3' THEN ISNULL(TotalPrice,0) ELSE 0 END TotalPP,  " +
                                "CASE WHEN Tipe='4' THEN ISNULL(SaldoQty,0) ELSE 0 END AdjustQty, CASE WHEN Tipe='4' THEN ISNULL(SaldoHS,0) ELSE 0 END AdjustHS,  " +
                                "CASE WHEN Tipe='4' THEN ISNULL(TotalPrice,0) ELSE 0 END TotalPR, CASE WHEN Tipe='5' THEN ISNULL(SaldoQty,0) ELSE 0 END AdjProdQty,  " +
                                "CASE WHEN Tipe='5' THEN ISNULL(SaldoHS,0) ELSE 0 END AdjProdHS, CASE WHEN Tipe='5' THEN ISNULL(TotalPrice,0) ELSE 0 END TotalPAdjP,  " +
                                "CASE WHEN Tipe='6' THEN ISNULL(SaldoQty,0) ELSE 0 END RetSupQty, CASE WHEN Tipe='6' THEN ISNULL(SaldoHS,0) ELSE 0 END RetSupHS,  " +
                                "CASE WHEN Tipe='6' THEN ISNULL(TotalPrice,0) ELSE 0 END TotalRetSup FROM Auto_lapmutasitmp_" + UserView + " as x) AS Z ORDER BY z.Tanggal  " +
                                "/** susun data berdasarkan item id dan bentuk id baru */  " +
                                "SELECT ROW_NUMBER() OVER(PARTITION BY itemID ORDER BY itemID,ID,DocNo) as IDn,* INTO Auto_lapmutasitmpxx_" + UserView + " FROM Auto_lapmutasitmpx_" + UserView + "  " +
                                "/**Susun data tabular */  " +
                                "SELECT ROW_NUMBER() OVER(Partition BY itemID ORDER BY ID,Tanggal,Tipe,DocNo) AS IDn,ID,Tipe,itemID,Tanggal,DocNo,  " +
                                "BeliQty,BeliHS,(BeliQty*BeliHS) As BeliAmt,AdjustQty, CASE WHEN A.ID>1 AND A.AdjustQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0  " +
                                "THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM Auto_lapmutasitmpxx_" + UserView + " WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END AdjustHS,  " +
                                "ProdQty, CASE WHEN A.ID>1 AND A.ProdQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END  " +
                                "FROM Auto_lapmutasitmpxx_" + UserView + " WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END ProdHS, AdjProdQty,  " +
                                "CASE WHEN A.ID>1 AND A.AdjProdQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END  " +
                                "FROM Auto_lapmutasitmpxx_" + UserView + " WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END AdjProdHS, A.ReturnQty, CASE WHEN A.ID>1 AND A.ReturnQty >0  " +
                                "THEN (SELECT CASE WHEN SUM(TotalQty) >0 THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM Auto_lapmutasitmpxx_" + UserView + " WHERE ID <A.ID AND ItemID=A.ItemID)  " +
                                "ELSE A.HPP END ReturnHS, A.RetSupQty, CASE WHEN A.ID>1 AND A.RetSupQty >0 THEN (SELECT CASE WHEN SUM(TotalQty) >0  " +
                                "THEN (SUM(TotalAmt)/NULLIF(SUM(totalqty),0))ELSE 0 END FROM Auto_lapmutasitmpxx_" + UserView + " WHERE ID <A.ID AND ItemID=A.ItemID) ELSE A.HPP END RetSupHS,  " +
                                "CASE WHEN A.ID>1 THEN (SELECT SUM(totalqty) FROM Auto_lapmutasitmpxx_" + UserView + " WHERE ID <=A.ID AND ItemID=A.ItemID )ELSE TotalQty END SaldoAwalQty,  " +
                                "CASE WHEN A.ID>1 THEN CASE WHEN (SELECT SUM(totalqty)FROM Auto_lapmutasitmpxx_" + UserView + " WHERE ID <=A.ID AND ItemID=A.ItemID )>0 THEN  " +
                                "((SELECT SUM(totalamt) FROM Auto_lapmutasitmpxx_" + UserView + " WHERE ID <=A.ID AND ItemID=A.ItemID )/ (ABS((SELECT SUM(totalqty)FROM Auto_lapmutasitmpxx_" + UserView + "  " +
                                "WHERE ID <=A.ID AND ItemID=A.ItemID ))))ELSE 0 END ELSE HPP END HS, CASE WHEN A.ID>1 THEN (SELECT SUM(totalamt) FROM Auto_lapmutasitmpxx_" + UserView + "  " +
                                "WHERE ID <=A.ID AND ItemID=A.ItemID)ELSE Totalamt END TotalAmt INTO Auto_lapmutasireport_" + UserView + " FROM Auto_lapmutasitmpxx_" + UserView + " as A  " +
                                "ORDER by itemID,A.Tanggal,A.IDn,a.Tipe,a.DocNo  " +
                                "/** Generate Detail Report without saldo akhir */  " +
                                "SELECT ROW_NUMBER() OVER(Partition BY itemID ORDER BY Tanggal,Tipe)as ID, l.ItemID,l.Tanggal,l.DocNo,l.BeliQty,l.BeliHS,  " +
                                "(l.BeliQty*l.BeliHS) as BeliAmt,l.AdjustQty, CASE WHEN L.IDn >1 AND l.AdjustQty >0 THEN(SELECT HS FROM Auto_lapmutasireport_" + UserView + " WHERE IDn=(L.IDn-1)  " +
                                "AND ItemID=L.ItemID)ELSE 0 END AdjustHS, CASE WHEN L.IDn >1 AND l.AdjustQty >0 THEN(SELECT HS FROM Auto_lapmutasireport_" + UserView + " WHERE IDn=(L.IDn-1)  " +
                                "AND ItemID=L.ItemID)* L.AdjustQty ELSE 0 END AdjustAmt, l.ProdQty, CASE WHEN L.IDn >1 AND l.ProdQty >0 THEN (SELECT HS  " +
                                "FROM Auto_lapmutasireport_" + UserView + " WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END ProdHS, CASE WHEN L.IDn >1 AND l.ProdQty >0 THEN(SELECT HS  " +
                                "FROM Auto_lapmutasireport_" + UserView + " WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.ProdQty ELSE 0 END ProdAmt, l.AdjProdQty,  " +
                                "CASE WHEN L.IDn >1 AND l.AdjProdQty >0 THEN (SELECT HS FROM Auto_lapmutasireport_" + UserView + " WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0 END AdjProdHS,  " +
                                "CASE WHEN L.IDn >1 AND l.AdjProdQty >0 THEN(SELECT HS FROM Auto_lapmutasireport_" + UserView + " WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.AdjProdQty ELSE 0 END AdjProdAmt,  " +
                                "l.ReturnQty, CASE WHEN L.IDn >1 AND l.ReturnQty >0 THEN (SELECT HS FROM Auto_lapmutasireport_" + UserView + " WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)ELSE 0  " +
                                "END ReturnHS, CASE WHEN L.IDn >1 AND l.ReturnQty >0 THEN(SELECT HS FROM Auto_lapmutasireport_" + UserView + " WHERE IDn=(L.IDn-1) AND ItemID=L.ItemID)* L.ReturnQty  " +
                                "ELSE 0 END returnAmt, l.RetSupQty, CASE WHEN L.IDn >1 AND l.RetSupQty >0 THEN (SELECT HS FROM Auto_lapmutasireport_" + UserView + " WHERE IDn=(L.IDn-1)  " +
                                "AND ItemID=L.ItemID)ELSE 0 END RetSupHS, CASE WHEN L.IDn >1 AND l.RetSupQty >0 THEN(SELECT HS FROM Auto_lapmutasireport_" + UserView + " WHERE IDn=(L.IDn-1) AND  " +
                                "ItemID=L.ItemID)* L.RetSupQty ELSE 0 END RetSupAmt, l.SaldoAwalQty,l.HS,l.TotalAmt INTO Auto_mutasireport_" + UserView + " FROM Auto_lapmutasireport_" + UserView + " AS L  " +
                                "ORDER BY L.itemID,L.Tipe,L.Tanggal  " +
                                "/** update colom amt dan colom hs */  " +
                                "select row_number() over(order by itemID) as IDn,itemid into Auto_mutasireport_" + UserView + "1 from Auto_mutasireport_" + UserView + " group by itemID order by itemid  " +
                                "declare @sqlSet nvarchar(max) " +
                                "set @sqlSet =' " +
                                "declare @i int declare @b int declare @hs decimal(18,6) declare @amt decimal(18,6) declare @avgp decimal(18,6)  " +
                                "declare @c int declare @itm int declare @itmID int  set @c=0 set @itm=(select COUNT(IDn) from Auto_mutasireport_" + UserView + "1)  " +
                                "While @c <=@itm Begin set @b=0; set @itmID=(select isnull(itemID,0) from Auto_mutasireport_" + UserView + "1 where IDn=@c)  " +
                                "set @avgp=(select top 1 '+ @AwalAvgPrice +' from SaldoInventory where ItemID = @itmID and YearPeriod='+ @thnAwal +') " +
                                "if ISNULL(@avgp,0)=0 " +
                                "begin " +
                                "set @avgp=(select top 1 HS from Auto_mutasireport_" + UserView + " where itemid=@itmID and HS>0 ) " +
                                "end " +
                                "set @i=(select COUNT(id) from Auto_mutasireport_" + UserView + " where itemid=@itmID) While @b<=@i  " +
                                "Begin set @hs=CASE WHEN @b >1 THEN (select hs from Auto_mutasireport_" + UserView + " where ID=(@b) and itemid=@itmID)  " +
                                "ELSE CASE WHEN(SELECT hs from Auto_mutasireport_" + UserView + " where ID=1 and itemid=@itmID)>0 THEN 	  " +
                                "(SELECT hs from Auto_mutasireport_" + UserView + " where ID=1 and itemid=@itmID)ELSE @avgp END 	 END  " +
                                "set @amt=CASE WHEN @b >1 THEN (select TotalAmt from Auto_mutasireport_" + UserView + " where ID=(@b) and itemid=@itmID)  " +
                                "eLSE (SELECT TotalAmt from Auto_mutasireport_" + UserView + " where ID=1 and itemid=@itmID) END  " +
                /** update semua hs */
                                "update Auto_mutasireport_" + UserView + " set AdjustHS	=CASE WHEN (SELECT AdjustQty FROM Auto_mutasireport_" + UserView + " WHERE ID=(@b+1) and itemid=@itmID)>0  " +
                                "THEN @hs ELSE 0 END, ProdHS		=CASE WHEN (SELECT ProdQty FROM Auto_mutasireport_" + UserView + " WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END,  " +
                                "ReturnHS=CASE WHEN (SELECT ReturnQty FROM Auto_mutasireport_" + UserView + " WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END,  " +
                                "AdjProdHS=CASE WHEN (SELECT AdjProdQty FROM Auto_mutasireport_" + UserView + " WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END,  " +
                                "RetSupHS=CASE WHEN (SELECT RetSupQty FROM Auto_mutasireport_" + UserView + " WHERE ID=(@b+1) and itemid=@itmID)>0 THEN @hs ELSE 0 END,  " +
                                "ProdAmt=(ProdQty*@hs), AdjustAmt =(AdjustQty*@hs), AdjProdAmt =(AdjProdQty*@hs), returnAmt =(ReturnQty*@hs),  " +
                                "RetSupAmt =(RetSupQty*@hs), totalamt =((BeliQty*BeliHs)+(adjustQty*@hs)+(returnQty*@hs)-(ProdQty*@hs)-(AdjprodQty*@hs)+(@amt)),  " +
                                "hs=case when abs(SaldoAwalQty)>0 then (((beliQty*BeliHs)+(adjustQty*@hs)+(returnQty*@hs)-(ProdQty*@hs)-(AdjprodQty*@hs)+@amt )/SaldoAwalQty)  " +
                                "else @avgp end where ID=(@b+1) and itemid=@itmID set @b=@b+1 END set @c=@c+1 END' " +
                                "exec sp_executesql @sqlSet, N'' " +
                                "/** Generate Saldo Awal */  " +
                                "SELECT ItemID,SaldoAwalQty,HS,TotalAmt INTO Auto_lapsaldoawal_" + UserView + " FROM Auto_mutasireport_" + UserView + " as m WHERE m.DocNo='Saldo Awal'  " +
                                "/** Generate Saldo Akhir */ " +
                                "SELECT (COUNT(m.itemID)+1)as ID,m.itemID, ' ' as Tanggal,'Total' as DocNo, /** pembelian */ (SUM(m.BeliQty)) As BeliQty, " +
                                "CASE WHEN SUM(m.BeliAmt) > 0 THEN (SUM(m.BeliAmt)/SUM(m.BeliQty))ELSE 0 END BeliHS, (SUM(m.BeliAmt)) As BeliAmt, " +
                                "/** Ajdut Plust */ (SUM(m.AdjustQty)) As AdjustQty, CASE WHEN SUM(m.AdjustAmt) > 0 THEN (SUM(m.AdjustAmt)/SUM(m.AdjustQty))ELSE 0 END  " +
                                "AdjustHS, (SUM(m.AdjustAmt)) As AdjustAmt,  " +
                                "/** Pemakaian Produksi */  " +
                                "(SUM(m.ProdQty)) As ProdQty, " +
                                "CASE WHEN SUM(m.ProdAmt) > 0 THEN (SUM(m.ProdAmt)/SUM(m.ProdQty))ELSE 0 END ProdHS, (SUM(m.ProdAmt)) As ProdAmt, " +
                                "/** Adjut minus */ " +
                                "(SUM(m.AdjProdQty)) As AdjProdQty, CASE WHEN SUM(m.AdjProdAmt) > 0 THEN (SUM(m.AdjProdAmt)/SUM(m.AdjProdQty))ELSE 0 END AdjProdHS,  " +
                                "(SUM(m.AdjProdAmt)) As AdjProdAmt, /** Return */ (SUM(m.ReturnQty)) As ReturnQty, CASE WHEN SUM(m.returnAmt) > 0 THEN  " +
                                "(SUM(m.returnAmt)/SUM(m.ReturnQty))ELSE 0 END ReturnHS, (SUM(m.returnAmt)) As returnAmt, " +
                                "/** Return Supplier */ " +
                                "(SUM(m.RetSupQty)) As RetSupQty, CASE WHEN SUM(m.RetSupQty) > 0 THEN (SUM(m.RetSupAmt)/ABS(SUM(m.RetSupQty)))ELSE 0 END RetSupHS, (SUM(m.RetSupAmt))  " +
                                "As RetSupAmt,  " +
                                "/** Saldo Akhir */ " +
                                "(SELECT TOP 1 SaldoAwalQty FROM Auto_mutasireport_" + UserView + " AS b WHERE b.itemID=m.itemID ORDER BY b.ID DESC) As SaldoAwalQty, " +
                                "(SELECT TOP 1 HS FROM Auto_mutasireport_" + UserView + " AS b WHERE b.itemID=m.itemID ORDER BY b.ID DESC) As HS, " +
                                "CASE when (SELECT TOP 1 SaldoAwalQty FROM Auto_mutasireport_" + UserView + " AS b WHERE b.itemID=m.itemID ORDER BY b.ID DESC)>0 then " +
                                "(SELECT TOP 1 TotalAmt FROM Auto_mutasireport_" + UserView + " AS b WHERE b.itemID=m.itemID ORDER BY b.ID DESC) ELSE 0 END As TotalAmt " +
                                "INTO Auto_mutasisaldo_" + UserView + " FROM Auto_mutasireport_" + UserView + " AS m GROUP BY m.ItemID " +
                                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto_UpdateAvgPrice_" + UserView + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto_UpdateAvgPrice_" + UserView + "]  " +
                                "/** Kumpulkan data update average price*/  " +
                                "SELECT  " +
                                "CASE WHEN m.ProdQty >0 THEN (SELECT TOP 1 ID FROM Pakai WHERE Pakai.PakaiNo=m.DocNo)  " +
                                "WHEN m.AdjustQty >0 THEN (SELECT TOP 1 ID FROM Adjust WHERE Adjust.AdjustNo=m.DocNo AND Adjust.AdjustType='Tambah')  " +
                                "WHEN m.AdjProdQty >0 THEN (SELECT TOP 1 ID FROM Adjust WHERE Adjust.AdjustNo=m.DocNo AND Adjust.AdjustType='Kurang')  " +
                                "WHEN m.ReturnQty >0 THEN (SELECT TOP 1 ID FROM ReturPakai WHERE ReturPakai.ReturNo=m.DocNo)  " +
                                "WHEN m.RetSupQty >0 THEN (SELECT TOP 1 ID FROM ReturSupplier WHERE ReturSupplier.ReturNo=m.DocNo)  " +
                                "WHEN m.BeliQty > 0 THEN (SELECT TOP 1 ID FROM Receipt WHERE Receipt.ReceiptNo=m.DocNo)END ID,  " +
                                "CASE WHEN m.ProdQty >0 THEN m.ItemID WHEN m.AdjustQty >0 THEN m.ItemID WHEN m.AdjProdQty >0 THEN m.ItemID WHEN m.ReturnQty >0 THEN m.ItemID " +
                                "WHEN m.RetSupQty >0 THEN m.ItemID WHEN m.BeliQty >0 THEN m.ItemID END itemID,  " +
                                "CASE WHEN m.ProdQty >0 THEN m.ProdHS WHEN m.AdjustQty >0 THEN m.AdjustHS " +
                                "WHEN m.AdjProdQty >0 THEN m.AdjProdHS WHEN m.ReturnQty >0 THEN m.ReturnHS WHEN m.RetSupQty >0 THEN m.RetSupHS WHEN m.BeliQty >0 THEN m.BeliHS END AvgPrice, " +
                                "CASE WHEN m.ProdQty >0 THEN 'PakaiDetail' WHEN m.AdjustQty>0 THEN 'AdjustDetailT' WHEN m.AdjProdQty>0 THEN 'AdjustDetailK'  " +
                                "WHEN m.ReturnQty >0 THEN 'ReturPakaiDetail' WHEN m.RetSupQty>0 THEN 'ReturSupplierDetail' WHEN m.BeliQty>0 THEN 'ReceiptDetail'  " +
                                "END Tabel INTO Auto_UpdateAvgPrice_" + UserView + " FROM Auto_mutasireport_" + UserView + " as m  " +
                                "/** update avgprice setiap tabel */ /** Produksi */  " +
                                "UPDATE p SET p.AvgPrice=a.AvgPrice FROM PakaiDetail as p INNER JOIN Auto_UpdateAvgPrice_" + UserView + " as a ON P.PakaiID=a.ID WHERE a.Tabel='PakaiDetail' and  " +
                                "p.ItemID=a.itemID  if @itemtypeID='3' begin " +
                                "update PakaiDetail set AvgPrice=(SELECT TOP 1 vw_HargaReceipt.Price FROM vw_HargaReceipt where vw_HargaReceipt.ItemID=PakaiDetail.ItemID    " +
                                "and vw_HargaReceipt.ItemTypeID=3 and vw_HargaReceipt.ReceiptDate<=(select pakaidate from Pakai where ID=PakaiDetail.PakaiID))   " +
                                "where PakaiID in (select ID from Pakai where ItemTypeID=3 and LEFT(convert(char,pakaidate,112),6)= @thbln) end " +
                                "/** penerimaan*/  " +
                                "UPDATE p SET p.AvgPrice=a.AvgPrice FROM ReceiptDetail as p INNER JOIN Auto_UpdateAvgPrice_" + UserView + " as a ON P.ReceiptID=a.ID  " +
                                "WHERE a.Tabel='ReceiptDetail' and p.ItemID=a.itemID /**penyesuaian produksi */ UPDATE p SET p.AvgPrice=a.AvgPrice FROM ReturPakaiDetail as p  " +
                                "INNER JOIN Auto_UpdateAvgPrice_" + UserView + " as a ON P.ReturID=a.ID WHERE a.Tabel='ReturPakaiDetail' and p.ItemID=a.itemID /** adjust Tambah */  " +
                                "UPDATE p SET p.AvgPrice=a.AvgPrice FROM AdjustDetail as p INNER JOIN Auto_UpdateAvgPrice_" + UserView + " as a ON P.AdjustID=a.ID WHERE a.Tabel='AdjustDetailT'  " +
                                "and p.ItemID=a.itemID /** Adjust Kurang */ UPDATE p SET p.AvgPrice=a.AvgPrice FROM AdjustDetail as p INNER JOIN Auto_UpdateAvgPrice_" + UserView + " as a ON P.AdjustID=a.ID  " +
                                "WHERE a.Tabel='AdjustDetailK' and p.ItemID=a.itemID  " +
                                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto_UpdateAvgPrice_" + UserView + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto_UpdateAvgPrice_" + UserView + "]  ";

            #endregion
            #region proses report pemakaian
            string strSQL = "select PakaiNo,convert(varchar,PakaiDate,103) as PakaiDate,ItemCode,ItemName,UOMCode,SUM(Quantity) as Jumlah, " +
                          "  case when crc >1 THEN " +
                          "  (/*(select Top 1 Kurs from MataUangKurs where MUID=crc and  sdTgl<=PakaiDate order by ID desc)*/Harga) " +
                          "   else " +
                          "   isnull(Harga,0) end Harga,Case When ProdLine=0 Then Keterangan else 'Prod. Line '+ Cast(ProdLine as Char(2))+'-'+Keterangan end Keterangan," +
                          "   /*Keterangan ,*/GroupID,GroupDescription,DeptName,Status, " +
                          "   crc,ItemID,ISNULL(NoPol,'')NoPol " +
                          "    from  " +
                          "  (SELECT  " +
                          "      isnull((select top 1 crc from POPurchn where POPurchn.ID in(select (POID) from ReceiptDetail where ReceiptDetail.ItemID=PakaiDetail.ItemID  " +
                          "      and ReceiptID in(select ID from Receipt where month(receipt.ReceiptDate) <= month(pakai.PakaiDate) and  " +
                          "      YEAR(receipt.ReceiptDate)<=YEAR(pakai.PakaiDate ))) order by POPurchn.ID desc),1)as crc, " +
                          "      CASE PakaiDetail.ItemTypeID   " +
                          "          WHEN 1 THEN (SELECT ItemCode FROM Inventory WHERE ID = PakaiDetail.ItemID)   " +
                          "          WHEN 2 THEN (SELECT ItemCode FROM Asset WHERE ID = PakaiDetail.ItemID)   " +
                          "          ELSE (SELECT ItemCode FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS ItemCode,   " +
                          "      CASE PakaiDetail.ItemTypeID   " +
                          "          WHEN 1 THEN (SELECT ItemName FROM Inventory WHERE ID = PakaiDetail.ItemID)   " +
                          "          WHEN 2 THEN (SELECT ItemName FROM Asset WHERE ID = PakaiDetail.ItemID)  " +
                          "          ELSE (SELECT ItemName FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS ItemName,   " +
                          "      CASE PakaiDetail.ItemTypeID   " +
                          "          WHEN 1 THEN (SELECT GroupID FROM Inventory WHERE ID = PakaiDetail.ItemID)   " +
                          "          WHEN 2 THEN (SELECT GroupID FROM Asset WHERE ID = PakaiDetail.ItemID)   " +
                          "          ELSE (SELECT GroupID FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS GroupID,   " +
                          "      CASE WHEN PakaiDetail.ItemTypeID=3 THEN " +
                          "          ( " +
                          "          SELECT TOP 1 vw_HargaReceipt.Price FROM vw_HargaReceipt where vw_HargaReceipt.ItemID=PakaiDetail.ItemID  " +
                          "          and vw_HargaReceipt.ItemTypeID=3 and vw_HargaReceipt.ReceiptDate<=Pakai.PakaiDate order by id desc " +
                          "          ) " +
                          "      ELSE " +
                          "      ISNULL(PakaiDetail.AvgPrice,  " +
                          "      (select top 1 " + SaldoAwal + " From SaldoInventory where SaldoInventory.ItemID=PakaiDetail.ItemID and YearPeriod=" + Tahun.ToString() + "))	  " +
                          "      END Harga,        " +
                          "      CASE when PakaiDetail.GroupID>0 THEN  " +
                          "          (SELECT GroupDescription FROM GroupsPurchn WHERE GroupsPurchn.ID = PakaiDetail.GroupID)  ELSE 'xxx' END AS GroupDescription,   " +
                          "      CASE Pakai.Status  WHEN 0 THEN ('Open')   " +
                          "          WHEN 1 THEN ('Head')  ELSE ('Gudang') END AS Status,PakaiDetail.ItemID, " +
                          "      PakaiDetail.Quantity,PakaiDetail.Keterangan,Pakai.PakaiNo,UOM.UOMCode,Pakai.PakaiDate," +
                          "      Pakai.DeptID,Dept.DeptName,PakaiDetail.ProdLine,PakaiDetail.NoPol  " +
                          "      FROM Pakai  " +
                          "      INNER JOIN PakaiDetail  " +
                          "      ON Pakai.ID = PakaiDetail.PakaiID and PakaiDetail.RowStatus>-1  " +
                          "      INNER JOIN UOM  " +
                          "      ON PakaiDetail.UomID = UOM.ID and UOM.RowStatus>-1  " +
                          "      INNER JOIN Dept  " +
                          "      ON Pakai.DeptID = Dept.ID and Dept.RowStatus>-1  " +
                          "      where Pakai.status=3 and convert(varchar,Pakai.PakaiDate,112)>='" + tgl1 + "' and   " +
                          "      convert(varchar,Pakai.PakaiDate,112)<='" + tgl2 + "' " +
                                 strGroupID + strDeptid + ") as AA  " +
                          "      group by DeptName,ItemCode,ItemName,UOMCode,Harga,GroupID,GroupDescription,PakaiNo," +
                          "      PakaiDate,Status,Keterangan,ItemID,crc,ProdLine,NoPol " +
                          "      order by GroupID,ItemName,ItemCode ";

            #region hapus temporary table
            //      strSQL += "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto_lapmutasitmp_" + UserView + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto_lapmutasitmp_" + UserView + "]  " +
            //                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto_lapmutasitmpx_" + UserView + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto_lapmutasitmpx_" + UserView + "]  " +
            //                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto_lapmutasitmpxx_" + UserView + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto_lapmutasitmpxx_" + UserView + "]  " +
            //                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto_lapmutasireport_" + UserView + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto_lapmutasireport_" + UserView + "]  " +
            //                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto_mutasireport_" + UserView + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto_mutasireport_" + UserView + "]  " +
            //                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto_mutasisaldo_" + UserView + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto_mutasisaldo_" + UserView + "]  " +
            //                "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Auto_lapsaldoawal_" + UserView + "]') AND type in (N'U')) DROP TABLE [dbo].[Auto_lapsaldoawal_" + UserView + "]";
            #endregion
            #endregion
            return strSQL;
        }
        public string ViewRekapSPBR1(string tgl1, string tgl2, int GroupID, string alias, int VP)
        {
            string strGroupID = string.Empty;
            string strDeptid = string.Empty;
            string SaldoAwal = string.Empty;
            string ItemIDKertas = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ItemKertas" + ((Users)HttpContext.Current.Session["Users"]).UnitKerjaID.ToString(), "SPB");
            int Bulan = int.Parse(tgl1.Substring(4, 2));
            int Tahun = int.Parse(tgl1.Substring(0, 4));
            if (Bulan == 1) { Tahun = Tahun - 1; }
            SaldoAwal = Global.nBulan((Bulan == 1) ? 12 : Bulan - 1, true) + "AvgPrice";
            strGroupID = (GroupID > 0) ? " and PakaiDetail.GroupID=" + GroupID : string.Empty;
            strDeptid = (alias == "-- ALL --") ? string.Empty : " and Pakai.DeptID in (select ID from dept where alias='" + alias + "')";
            string strSQL = "select GroupID,GroupDescription,ItemCode,ItemName,UOMCode,sum(Jumlah)Jumlah,case when sum(jumlah)>0 then "+
                          "sum(Harga* Jumlah)/sum(jumlah) else 0 end Harga,DeptName,crc,ItemID from ( " +
                          "select PakaiNo,convert(varchar,PakaiDate,103) as PakaiDate,ItemCode,ItemName,UOMCode,SUM(Quantity) as Jumlah, " +
                          "  case when crc >1 THEN " +
                          "  (/*(select Top 1 Kurs from MataUangKurs where MUID=crc and  sdTgl<=PakaiDate order by ID desc)*/Harga) " +
                          "   else " +
                          "   isnull(Harga,0) end Harga,Case When ProdLine=0 Then Keterangan else 'Prod. Line '+ Cast(ProdLine as Char(2))+'-'+Keterangan end Keterangan," +
                          "   /*Keterangan ,*/GroupID,GroupDescription,DeptName,Status, " +
                          "   crc,ItemID " +
                          "    from  " +
                          "  (SELECT  " +
                          "      isnull((select top 1 crc from POPurchn where POPurchn.ID in(select (POID) from ReceiptDetail where ReceiptDetail.ItemID=PakaiDetail.ItemID  " +
                          "      and ReceiptID in(select ID from Receipt where month(receipt.ReceiptDate) <= month(pakai.PakaiDate) and  " +
                          "      YEAR(receipt.ReceiptDate)<=YEAR(pakai.PakaiDate ))) order by POPurchn.ID desc),1)as crc, " +
                          "      CASE PakaiDetail.ItemTypeID   " +
                          "          WHEN 1 THEN (SELECT ItemCode FROM Inventory WHERE ID = PakaiDetail.ItemID)   " +
                          "          WHEN 2 THEN (SELECT ItemCode FROM Asset WHERE ID = PakaiDetail.ItemID)   " +
                          "          ELSE (SELECT ItemCode FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS ItemCode,   " +
                          "      CASE PakaiDetail.ItemTypeID   " +
                          "          WHEN 1 THEN (SELECT ItemName FROM Inventory WHERE ID = PakaiDetail.ItemID)   " +
                          "          WHEN 2 THEN (SELECT ItemName FROM Asset WHERE ID = PakaiDetail.ItemID)  " +
                          "          ELSE (SELECT ItemName FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS ItemName,   " +
                          "      CASE PakaiDetail.ItemTypeID   " +
                          "          WHEN 1 THEN (SELECT GroupID FROM Inventory WHERE ID = PakaiDetail.ItemID)   " +
                          "          WHEN 2 THEN (SELECT GroupID FROM Asset WHERE ID = PakaiDetail.ItemID)   " +
                          "          ELSE (SELECT GroupID FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS GroupID,   " +
                          "      PakaiDetail.AvgPrice  Harga,        " +
                          "      CASE when PakaiDetail.GroupID>0 THEN  " +
                          "          (SELECT GroupDescription FROM GroupsPurchn WHERE GroupsPurchn.ID = PakaiDetail.GroupID)  ELSE 'xxx' END AS GroupDescription,   " +
                          "      CASE Pakai.Status  WHEN 0 THEN ('Open')   " +
                          "          WHEN 1 THEN ('Head')  ELSE ('Gudang') END AS Status,PakaiDetail.ItemID, " +
                          "      PakaiDetail.Quantity,PakaiDetail.Keterangan,Pakai.PakaiNo,UOM.UOMCode,Pakai.PakaiDate," +
                          "      Pakai.DeptID,Dept.DeptName,PakaiDetail.ProdLine  " +
                          "      FROM Pakai  " +
                          "      INNER JOIN PakaiDetail  " +
                          "      ON Pakai.ID = PakaiDetail.PakaiID and PakaiDetail.RowStatus>-1  " +
                          "      INNER JOIN UOM  " +
                          "      ON PakaiDetail.UomID = UOM.ID and UOM.RowStatus>-1  " +
                          "      INNER JOIN Dept  " +
                          "      ON Pakai.DeptID = Dept.ID and Dept.RowStatus>-1  " +
                          "      where Pakai.status=3 and convert(varchar,Pakai.PakaiDate,112)>='" + tgl1 + "' and   " +
                          "      convert(varchar,Pakai.PakaiDate,112)<='" + tgl2 + "' " +
                                 strGroupID + strDeptid + ") as AA  " +
                          "      group by DeptName,ItemCode,ItemName,UOMCode,Harga,GroupID,GroupDescription,PakaiNo," +
                          "      PakaiDate,Status,Keterangan,ItemID,crc,ProdLine " +
                          "      ) A group by GroupID,GroupDescription,ItemCode,ItemName,UOMCode,DeptName,crc,ItemID order by GroupDescription, ItemName ";
            return strSQL;
        }
        public string ViewRekapPakai1(string tgl1, string tgl2, int groupID, string alias)
        {
            string strGroupID = string.Empty;
            string strDeptid = string.Empty;
            if (groupID > 0)
                strGroupID = " and PakaiDetail.GroupID=" + groupID;
            strDeptid = (alias == "-- ALL --") ? string.Empty : " and Pakai.DeptID in (select ID from dept where alias='" + alias + "')";

            return "select PakaiNo,convert(varchar,PakaiDate,103) as PakaiDate,ItemCode,ItemName,UOMCode,SUM(Quantity) as Jumlah,isnull(Harga,0) as Harga,Keterangan,GroupID,GroupDescription,DeptName,Status from (" +
                "SELECT CASE PakaiDetail.ItemTypeID  WHEN 1 THEN (SELECT ItemCode FROM Inventory WHERE ID = PakaiDetail.ItemID)  " +
                "WHEN 2 THEN (SELECT ItemCode FROM Asset WHERE ID = PakaiDetail.ItemID)  ELSE " +
                "(SELECT ItemCode FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS ItemCode,  " +
                "CASE PakaiDetail.ItemTypeID  WHEN 1 THEN (SELECT ItemName FROM Inventory WHERE ID = PakaiDetail.ItemID)  " +
                "WHEN 2 THEN (SELECT ItemName FROM Asset WHERE ID = PakaiDetail.ItemID)  ELSE " +
                "(SELECT ItemName FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS ItemName,  " +
                "CASE PakaiDetail.ItemTypeID  WHEN 1 THEN (SELECT GroupID FROM Inventory WHERE ID = PakaiDetail.ItemID)  " +
                "WHEN 2 THEN (SELECT GroupID FROM Asset WHERE ID = PakaiDetail.ItemID)  ELSE " +
                "(SELECT GroupID FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS GroupID,  " +
                "CASE WHEN PakaiDetail.ItemID>0 and (select isnull(harga,0) from Inventory where ID=PakaiDetail.ItemID)=0 THEN (SELECT top 1 isnull(POPurchnDetail.Price,0) FROM POPurchn,POPurchnDetail  " +
                "WHERE POPurchn.ID=POPurchnDetail.POID and POPurchnDetail.ItemID=PakaiDetail.ItemID and POPurchnDetail.GroupID=PakaiDetail.GroupID and POPurchn.Status>-1 and " +
                "POPurchnDetail.Status>-1 order by POPurchnDate Desc) else 0 end Harga," +
                "CASE when PakaiDetail.GroupID>0 THEN (SELECT GroupDescription FROM GroupsPurchn WHERE GroupsPurchn.ID = PakaiDetail.GroupID)  " +
                "ELSE 'xxx' END AS GroupDescription,  " +
                "CASE Pakai.Status  WHEN 0 THEN ('Open')  " +
                "WHEN 1 THEN ('Head')  ELSE " +
                "('Gudang') END AS Status," +
                "PakaiDetail.Quantity,PakaiDetail.Keterangan,Pakai.PakaiNo,UOM.UOMCode,Pakai.PakaiDate,Pakai.DeptID,Dept.DeptName FROM Pakai " +
                "INNER JOIN PakaiDetail ON Pakai.ID = PakaiDetail.PakaiID and PakaiDetail.RowStatus>-1 " +
                "INNER JOIN UOM ON PakaiDetail.UomID = UOM.ID and UOM.RowStatus>-1 " +
                "INNER JOIN Dept ON Pakai.DeptID = Dept.ID and Dept.RowStatus>-1 " +
                "where Pakai.status=3 and convert(varchar,Pakai.PakaiDate,112)>='" + tgl1 + "' and  convert(varchar,Pakai.PakaiDate,112)<=" + tgl2 +
                strDeptid + strGroupID + ") as AA group by DeptName,ItemCode,ItemName,UOMCode,Harga,GroupID,GroupDescription,PakaiNo,PakaiDate,Status,Keterangan order by GroupID,ItemCode";
        }
        public string ViewRekapPakaiByPrice01(string tgl1, string tgl2, int groupID, string alias)
        {
            string strGroupID = string.Empty;
            string strDeptid = string.Empty;
            if (groupID > 0)
                strGroupID = " and PakaiDetail.GroupID=" + groupID;
            strDeptid = (alias == "-- ALL --") ? string.Empty : " and Pakai.DeptID in (select ID from dept where alias='" + alias + "')";

            return "select PakaiNo,convert(varchar,PakaiDate,103) as PakaiDate,ItemCode,ItemName,UOMCode,SUM(Quantity) as Jumlah,isnull(Harga,0) as Harga,Keterangan,GroupID,GroupDescription,DeptName,Status from (" +
                "SELECT CASE PakaiDetail.ItemTypeID  WHEN 1 THEN (SELECT ItemCode FROM Inventory WHERE ID = PakaiDetail.ItemID)  " +
                "WHEN 2 THEN (SELECT ItemCode FROM Asset WHERE ID = PakaiDetail.ItemID)  ELSE " +
                "(SELECT ItemCode FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS ItemCode,  " +
                "CASE PakaiDetail.ItemTypeID  WHEN 1 THEN (SELECT ItemName FROM Inventory WHERE ID = PakaiDetail.ItemID)  " +
                "WHEN 2 THEN (SELECT ItemName FROM Asset WHERE ID = PakaiDetail.ItemID)  ELSE " +
                "(SELECT ItemName FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS ItemName,  " +
                "CASE PakaiDetail.ItemTypeID  WHEN 1 THEN (SELECT GroupID FROM Inventory WHERE ID = PakaiDetail.ItemID)  " +
                "WHEN 2 THEN (SELECT GroupID FROM Asset WHERE ID = PakaiDetail.ItemID)  ELSE " +
                "(SELECT GroupID FROM Biaya WHERE ID = PakaiDetail.ItemID) END AS GroupID,  " +
                " 0 as Harga," +
                "CASE when PakaiDetail.GroupID>0 THEN (SELECT GroupDescription FROM GroupsPurchn WHERE GroupsPurchn.ID = PakaiDetail.GroupID)  " +
                "ELSE 'xxx' END AS GroupDescription,  " +
                "CASE Pakai.Status  WHEN 0 THEN ('Open')  " +
                "WHEN 1 THEN ('Head')  ELSE " +
                "('Gudang') END AS Status," +
                "PakaiDetail.Quantity,PakaiDetail.Keterangan,Pakai.PakaiNo,UOM.UOMCode,Pakai.PakaiDate,Pakai.DeptID,Dept.DeptName FROM Pakai " +
                "INNER JOIN PakaiDetail ON Pakai.ID = PakaiDetail.PakaiID and PakaiDetail.RowStatus>-1 " +
                "INNER JOIN UOM ON PakaiDetail.UomID = UOM.ID and UOM.RowStatus>-1 " +
                "INNER JOIN Dept ON Pakai.DeptID = Dept.ID and Dept.RowStatus>-1 " +
                "where  Pakai.status=3 and convert(varchar,Pakai.PakaiDate,112)>='" + tgl1 + "' and  convert(varchar,Pakai.PakaiDate,112)<=" + tgl2 +
                strDeptid + strGroupID + ") as AA group by DeptName,ItemCode,ItemName,UOMCode,Harga,GroupID,GroupDescription,PakaiNo,PakaiDate,Status,Keterangan order by GroupID,ItemCode";
        }

        public string ViewRekapKomplain(string drTgl, string sdTgl, string dept, int deptid)
        {
            string where = string.Empty;
            where = (deptid == 0) ? "" : " and deptid=" + dept;
            string strQuery;
            strQuery = "select HelpDeskKeluhan.ID, convert (varchar, HelpdeskKeluhan.HelpTgl,103)as Tanggal,HelpdeskKeluhan.HelpDeskNo,HelpdeskKeluhan.DeptName,HelpdeskKeluhan.Keluhan," +
            "HelpDeskKeluhan.Analisa," +
            "case " +
                "when HelpDeskKeluhan.KategoriPenyelesaianID = 0 then 'K1'" +
                "when HelpDeskKeluhan.KategoriPenyelesaianID = 1 then 'K2'" +
                "when HelpDeskKeluhan.KategoriPenyelesaianID = 2 then 'K3'" +
                "end KategoriPenyelesaian," +
            "HelpDeskKeluhan.Perbaikan,HelpDeskCategory.HelpCategory,HelpDeskKeluhan.PIC, " +
            "case " +
                "when HelpdeskKeluhan.Status = 0 then 'Open' " +
                "when HelpdeskKeluhan.Status = 1 then 'Progress' " +
                "when HelpdeskKeluhan.Status = 2 then 'Solved' " +
                "end Status," +
                "HelpDeskKeluhan.CreatedBy,convert (varchar,HelpDeskKeluhan.TglPerbaikan,103)as TanggalPerbaikan " +
            "from HelpDeskKeluhan,HelpDeskCategory where HelpDeskKeluhan.HelpDeskCategoryID=HelpDeskCategory.ID " +
            "and HelpDeskKeluhan.HelpTgl between '" + drTgl + "' and ' " + sdTgl + "' " + where +
            " and HelpdeskKeluhan.RowStatus > -1 order by HelpdeskKeluhan.HelpTgl desc";
            return strQuery;

        }
    }


    
    //public class ReturnBJ : GRCBaseDomain
    //{
    //    public virtual DateTime Tanggal { get; set; }
    //    public virtual string SJNo { get; set; }
    //    public virtual string OPNo { get; set; }
    //    public virtual string Customer { get; set; }
    //    public virtual string PartNo { get; set; }
    //    public virtual decimal Qty { get; set; }
    //    public virtual decimal Total { get; set; }
    //}
}

