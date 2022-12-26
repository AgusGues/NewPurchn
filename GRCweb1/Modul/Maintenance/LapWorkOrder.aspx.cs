using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dapper;
using GRCweb1.Models;
using Newtonsoft.Json;
using Factory;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using System.Collections;

namespace GRCweb1.Modul.Maintenance
{
    public partial class LapWorkOrder : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<LapWorkOrderNf.ParamHakAkses> HakAkses()
        {
            List<LapWorkOrderNf.ParamHakAkses> list = new List<LapWorkOrderNf.ParamHakAkses>();
            list = LapWorkOrderNfFacade.HakAkses();
            return list;
        }

        [WebMethod]
        public static List<LapWorkOrderNf.ParamTahun> ListTahun()
        {
            List<LapWorkOrderNf.ParamTahun> list = new List<LapWorkOrderNf.ParamTahun>();
            list = LapWorkOrderNfFacade.ListTahun();
            return list;
        }

        [WebMethod]
        public static List<LapWorkOrderNf.ParamData> ListData(int TypeLapoaran, int Department, int Bulan, int Tahun)
        {
            ZetroView zSave = new ZetroView();
            zSave.QueryType = Operation.CUSTOM;
            zSave.CustomQuery =
            " IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[temp_dataWO]') AND type in (N'U')) DROP TABLE [dbo].[temp_dataWO] ";
            SqlDataReader sdSave = zSave.Retrieve();

            Users users = (Users)HttpContext.Current.Session["Users"];
            int StatusReport = LapWorkOrderNfFacade.StatusReport(users.ID);
            int StatusApv1 = LapWorkOrderNfFacade.StatusApv1(users.ID);
            string Corporate = LapWorkOrderNfFacade.Corporate();
            int stsUser = LapWorkOrderNfFacade.StsUser(users.ID);
            int FlagUser = 0; if(stsUser > 0) { FlagUser = stsUser; }
            string NamaSub = LapWorkOrderNfFacade.NamaSub(users.ID);
            int DeptIDUser = users.DeptID; HttpContext.Current.Session["DeptIDUser"] = DeptIDUser;
            if (StatusApv1 != 0) { DeptIDUser = LapWorkOrderNfFacade.DeptIDUser(users.ID); HttpContext.Current.Session["DeptIDUser"] = DeptIDUser; }
            string Flag = "";
            int PilihDept = 0;
            if (StatusReport == 5)// Untuk Department ISO dan HRD PES : Bisa lihat WO Masuk Dept. HRD - IT - MTN
            {
                if (TypeLapoaran == 1)// WO Keluar
                {
                    PilihDept = users.DeptID;
                    Flag = "4";
                }
                else if (TypeLapoaran==2)// WO per Bulan
                {
                    if (StatusReport == 5){ PilihDept = Department;}
                    Flag = "3";
                }
                else if (TypeLapoaran == 3)// WO Pemantauan open close
                {
                    if (StatusReport == 5) { PilihDept = Department; }
                    Flag = "2";
                }
            }

            // Untuk Department diluar HRD PES - ISO
            else if (StatusReport == 0)
            {
                if (TypeLapoaran == 1 && FlagUser == 0)//wo keluar
                {
                    PilihDept = users.DeptID;
                    Flag = "4";
                }
                else if (TypeLapoaran == 1 && FlagUser > 0)//wo keluar
                {
                    PilihDept = users.DeptID;
                    Flag = "41";
                }
                else if (TypeLapoaran == 2)//WO per Bulan
                {
                    PilihDept = users.DeptID;
                    Flag = "3";
                }
                else if (TypeLapoaran == 3)// WO Pemantauan open close
                {
                    PilihDept = users.DeptID;
                    Flag = "2";
                }
            }

            int DeptID = PilihDept;
            string Flag1 = Flag;
            HttpContext.Current.Session["Flag"] = Flag;
            HttpContext.Current.Session["PiihDept"] = DeptID;
            /** added by Beny 29 Oktober 2021, revisi-an **/
            LapWorkOrderNfFacade.LoadDataMaster_WO(Flag1, DeptID, users.UnitKerjaID, Tahun, Bulan);
            string query = "";
            if (Flag1 == "2")// Pemantauan WO
            {
                query = LapWorkOrderNfFacade.RetrieveNamaDept_PemantauanWO(StatusApv1.ToString(), DeptID, Tahun, Bulan.ToString(), Flag1, users.UnitKerjaID);
            }
            else if (Flag1 == "3")// Pencapaian WO
            {
                query = LapWorkOrderNfFacade.RetrieveNamaDept_PencapaianWO(StatusApv1.ToString(), DeptID, Tahun, Bulan.ToString(), Flag1, users.UnitKerjaID);
            }
            else if (Flag1 == "4") //WO Keluar
            {
                if (users.DeptID == 23) { FlagUser = Department; }
                query = LapWorkOrderNfFacade.RetrieveNamaDept_Keluar(StatusApv1.ToString(), DeptID, Tahun, Bulan.ToString(), Flag1, users.UnitKerjaID, FlagUser, NamaSub);
            }
            else if (Flag1 == "41")//WO Keluar
            {
                query = LapWorkOrderNfFacade.RetrieveNamaDept_Keluar(StatusApv1.ToString(), DeptID, Tahun, Bulan.ToString(), Flag1, users.UnitKerjaID, FlagUser, NamaSub);
            }
            if (Flag1 == "5" || Flag1 == "51")//WO Masuk
            {
                query = LapWorkOrderNfFacade.RetrieveNamaDept_Masuk(StatusApv1.ToString(), DeptID, Tahun, Bulan.ToString(), Flag1, StatusReport, Corporate, users.UnitKerjaID);
            }
            List<LapWorkOrderNf.ParamData> list = new List<LapWorkOrderNf.ParamData>();
            list = LapWorkOrderNfFacade.ListData(query);
            return list;
        }

        [WebMethod]
        public static List<LapWorkOrderNf.ParamDataWo> ListDataWo(string DeptFrom, string DeptTo, int Bulan , int Tahun)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            int StatusReport = LapWorkOrderNfFacade.StatusReport(users.ID);
            string Tanda2 = HttpContext.Current.Session["Flag"].ToString();
            int UserID1 = Convert.ToInt32(HttpContext.Current.Session["DeptIDUser"]);
            int StatusApv = LapWorkOrderNfFacade.StatusApv1(users.ID);
            ArrayList arrData5 = new ArrayList();
           
            string Thn1 = DateTime.Now.Year.ToString();
            int Bln1 = DateTime.Now.Month;
            string DPenerimaWOTemp = "";
            string DUsersWOTemp = "";
            
            if (StatusReport == 5)//Session["PilihDept"]
            {
                if (Tanda2 == "4")
                {
                    if (users.DeptID == 23)
                    {
                        DPenerimaWOTemp = DeptTo.Trim();
                        DUsersWOTemp = DeptFrom.Trim();
                    }
                    else
                    {
                        DPenerimaWOTemp = DeptFrom.Trim();
                        DUsersWOTemp = LapWorkOrderNfFacade.RetrieveDeptName(users.DeptID.ToString());
                    }
                }
                else
                {
                    DPenerimaWOTemp = LapWorkOrderNfFacade.RetrieveDeptName(Convert.ToInt32(HttpContext.Current.Session["PiihDept"]).ToString());
                    DUsersWOTemp = DeptFrom.Trim();
                }
            }

            else if (StatusReport == 0)
            {
                if (Tanda2 == "2")// Pemantauan WO
                {
                    DPenerimaWOTemp = LapWorkOrderNfFacade.RetrieveDeptName(users.DeptID.ToString());
                    DUsersWOTemp = DeptFrom.Trim();
                }
                else if (Tanda2 == "3") // Pencapaian WO
                {
                    DPenerimaWOTemp = LapWorkOrderNfFacade.RetrieveDeptName(users.DeptID.ToString());
                    DUsersWOTemp = DeptFrom.Trim();
                }
                else if (Tanda2 == "4") // WO Keluar
                {
                    DPenerimaWOTemp = DeptFrom.Trim();
                    DUsersWOTemp = LapWorkOrderNfFacade.RetrieveDeptName(users.DeptID.ToString());
                }
                else if (Tanda2 == "41")// WO Keluar
                {
                    DPenerimaWOTemp = DeptFrom.Trim();
                    DUsersWOTemp = LapWorkOrderNfFacade.RetrieveDeptNameSub(users.ID.ToString());
                }
                else if (Tanda2 == "5")// WO Masuk
                {
                    DPenerimaWOTemp = DeptFrom.Trim();
                    DUsersWOTemp = LapWorkOrderNfFacade.RetrieveDeptName(users.DeptID.ToString());
                }
                else if (Tanda2 == "51")// WO Masuk
                {
                    DPenerimaWOTemp = LapWorkOrderNfFacade.RetrieveDeptName(users.DeptID.ToString());
                    DUsersWOTemp = DeptFrom.Trim();
                }
            }
            string PenerimaWO = DPenerimaWOTemp.Trim(); HttpContext.Current.Session["PenerimaWO"] = PenerimaWO;
            string UsersWO = DUsersWOTemp; HttpContext.Current.Session["UsersWO"] = UsersWO;
            string NamaSub = string.Empty;
            if (UsersWO.Contains("HRD -"))
            {
                
                UsersWO = "HRD & GA";
                NamaSub = UsersWO.Trim();
            }
            else
            {
                NamaSub = "";
            }

            string NamaSubDept = NamaSub.ToString().Trim();
            string WaktuSkr = DateTime.Now.ToString("yyyyMMdd");
            string query = LapWorkOrderNfFacade.RetrieveListWO(NamaSubDept.Trim(), PenerimaWO.Trim(), UsersWO.Trim(), Bulan.ToString(), Tahun, Tanda2, WaktuSkr, users.UnitKerjaID);
            List<LapWorkOrderNf.ParamDataWo> list = new List<LapWorkOrderNf.ParamDataWo>();
            list = LapWorkOrderNfFacade.ListDataWo(query);
            return list;
        }

        [WebMethod]
        public static List<LapWorkOrderNf.ParamPencapaianNilai> PencapaianNilai(string DeptFrom, string DeptTo, int Bulan1, int Tahun1)
        {
            List<LapWorkOrderNf.ParamPencapaianNilai> List = new List<LapWorkOrderNf.ParamPencapaianNilai>();
            Users users = (Users)HttpContext.Current.Session["Users"];
            int StatusReport = LapWorkOrderNfFacade.StatusReport(users.ID);
            string Tanda2 = HttpContext.Current.Session["Flag"].ToString();
            int UserID1 = Convert.ToInt32(HttpContext.Current.Session["DeptIDUser"]);
            int StatusApv = LapWorkOrderNfFacade.StatusApv1(users.ID);
            ArrayList arrData5 = new ArrayList();

            string Thn1 = DateTime.Now.Year.ToString();
            int Bln1 = DateTime.Now.Month;
            string DPenerimaWOTemp = "";
            string DUsersWOTemp = "";

            if (StatusReport == 5)//Session["PilihDept"]
            {
                if (Tanda2 == "4")
                {
                    if (users.DeptID == 23)
                    {
                        DPenerimaWOTemp = DeptTo.Trim();
                        DUsersWOTemp = DeptFrom.Trim();
                    }
                    else
                    {
                        DPenerimaWOTemp = DeptFrom.Trim();
                        DUsersWOTemp = LapWorkOrderNfFacade.RetrieveDeptName(users.DeptID.ToString());
                    }
                }
                else
                {
                    DPenerimaWOTemp = LapWorkOrderNfFacade.RetrieveDeptName(Convert.ToInt32(HttpContext.Current.Session["PiihDept"]).ToString());
                    DUsersWOTemp = DeptFrom.Trim();
                }
            }

            else if (StatusReport == 0)
            {
                if (Tanda2 == "2")// Pemantauan WO
                {
                    DPenerimaWOTemp = LapWorkOrderNfFacade.RetrieveDeptName(users.DeptID.ToString());
                    DUsersWOTemp = DeptFrom.Trim();
                }
                else if (Tanda2 == "3") // Pencapaian WO
                {
                    DPenerimaWOTemp = LapWorkOrderNfFacade.RetrieveDeptName(users.DeptID.ToString());
                    DUsersWOTemp = DeptFrom.Trim();
                }
                else if (Tanda2 == "4") // WO Keluar
                {
                    DPenerimaWOTemp = DeptFrom.Trim();
                    DUsersWOTemp = LapWorkOrderNfFacade.RetrieveDeptName(users.DeptID.ToString());
                }
                else if (Tanda2 == "41")// WO Keluar
                {
                    DPenerimaWOTemp = DeptFrom.Trim();
                    DUsersWOTemp = LapWorkOrderNfFacade.RetrieveDeptNameSub(users.ID.ToString());
                }
                else if (Tanda2 == "5")// WO Masuk
                {
                    DPenerimaWOTemp = DeptFrom.Trim();
                    DUsersWOTemp = LapWorkOrderNfFacade.RetrieveDeptName(users.DeptID.ToString());
                }
                else if (Tanda2 == "51")// WO Masuk
                {
                    DPenerimaWOTemp = LapWorkOrderNfFacade.RetrieveDeptName(users.DeptID.ToString());
                    DUsersWOTemp = DeptFrom.Trim();
                }
            }
            string PenerimaWO = DPenerimaWOTemp.Trim(); HttpContext.Current.Session["PenerimaWO"] = PenerimaWO;
            string UsersWO = DUsersWOTemp; HttpContext.Current.Session["UsersWO"] = UsersWO;

            int TotalWO = LapWorkOrderNfFacade.RetrieveTotalWO(Bulan1.ToString(), Tahun1.ToString(), UserID1, Tanda2, DeptTo, StatusReport);
            decimal TotalWO2 = Convert.ToDecimal(TotalWO);
            string wo3 = LapWorkOrderNfFacade.RetrieveTtlWO_Break(Bulan1.ToString(), Tahun1.ToString(), UserID1, Tanda2, DeptTo, StatusReport);
            string wo04 = LapWorkOrderNfFacade.RetrieveTotalWO_PerHead(Bulan1.ToString(), Tahun1.ToString(), UserID1, Tanda2, DeptTo, StatusReport);

            string Tahun = Tahun1.ToString();
            int Bul = Convert.ToInt32(Bulan1);
            string Bulan = "";
            if (Bul < 10)
            {
                Bulan = '0' + Bul.ToString();
            }
            else
            {
                Bulan = Bul.ToString();
            }
            string BulanR = Bulan;
            string RangePeriode = Tahun + BulanR;
            string WaktuSkr = DateTime.Now.ToString("yyyyMMdd");
            string txtTotal = "";
            string txtTarget = "";
            string txtPersen = "";
            string KetTotal = "";
            string KetTarget = "";
            string KetPersen = "";
            string LabelTotalNilai = "";
            string LabelTargetNilai = "";
            string LabelPersenNilai = "";

            if (Tanda2 == "3" && TotalWO > 0)
            {
                string wo2 = LapWorkOrderNfFacade.RetrieveStatusWO(PenerimaWO, UsersWO, Bulan, Convert.ToInt32(Tahun), Tanda2, WaktuSkr, RangePeriode);
                string wo4 = LapWorkOrderNfFacade.RetrieveStatusWO_break(PenerimaWO, UsersWO, Bulan, Convert.ToInt32(Tahun), Tanda2, WaktuSkr, RangePeriode);
                string wo5 = LapWorkOrderNfFacade.RetrieveStatusWO_breakPersen(PenerimaWO, UsersWO, Bulan, Convert.ToInt32(Tahun), Tanda2, WaktuSkr, RangePeriode);
                decimal Total = Convert.ToDecimal(wo2);
                decimal Persen = Math.Round((Total / TotalWO2), 2, MidpointRounding.AwayFromZero) * 100;

                
                //txtTotal,KetTotal,LabelTotalNilai,txtTarget,KetTarget,LabelTargetNilai,txtPersen,KetPersen,LabelPersenNilai

                //update sarmut
                decimal aktual = Persen;
                string sarmutPrs = string.Empty; int deptID2 = 0;
                int deptID_P = LapWorkOrderNfFacade.getDeptID_P(PenerimaWO.Trim());

                /** Revisi **/
                if (PenerimaWO.Trim() == "MAINTENANCE") { deptID2 = 19; }
                else if (PenerimaWO.Trim() == "HRD & GA") { deptID2 = 7; }
                else { deptID2 = deptID_P; }

                //if (Session["DeptIDUser"].ToString().Trim() == deptID_P.ToString())
                if (HttpContext.Current.Session["DeptIDUser"].ToString().Trim() == "19") { sarmutPrs = "Pencapaian Work Order Maintenance"; }
                else { sarmutPrs = "Pencapaian Work Order"; }

                /** End Revisi **/

                /** int deptid = getDeptID(deptID_P.ToString()); **/
                int deptid = LapWorkOrderNfFacade.getDeptID(deptID2.ToString());
                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery =
                    "update SPD_TransPrs set actual= " + aktual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + Tahun +
                    " and Bulan=" + Bulan +
                    " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "' and deptid=" + deptid + " ) ";
                SqlDataReader sdr1 = zl1.Retrieve();
                //end update sarmut

                txtTotal = TotalWO.ToString();
                KetTotal = "( " + wo3 + " )";
                KetTarget = "( " + wo4 + " )";
                KetPersen = "( " + wo5 + " )";

                txtTarget = wo2.ToString();
                txtPersen = Persen.ToString() + " " + "%";
                LabelTotalNilai = TotalWO.ToString();
                LabelTargetNilai = wo2.ToString();
                LabelPersenNilai = Persen.ToString() + " " + "%";
            }
            List = new List<LapWorkOrderNf.ParamPencapaianNilai>()
            {
                new LapWorkOrderNf.ParamPencapaianNilai() {
                    txtTotal=txtTotal,
                    txtTarget=txtTarget,
                    txtPersen=txtPersen,
                    KetTotal=KetTotal,
                    KetTarget=KetTarget,
                    KetPersen=KetPersen,
                    LabelTotalNilai=LabelTotalNilai,
                    LabelTargetNilai=LabelTargetNilai,
                    LabelPersenNilai=LabelPersenNilai
                }
            };
            return List;
        }

    }
}