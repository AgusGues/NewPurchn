using Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessFacade;

namespace GRCweb1.Modul.Maintenance
{
    public partial class InputPemakaianListrik : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            keterangan();
        }
        public void keterangan()
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            string ket1 = string.Empty;
            string ket2 = string.Empty;
            if(users.UnitKerjaID == 7)
            {
                ket1 = "Ket : 1 s/d 4 target 110%";
                ket2 = "5 s/d 6 target 100%";
            }
            else if (users.UnitKerjaID == 1)
            {
                ket1 = "Ket : 1 s/d 2 target 110%";
                ket2 = "3 s/d 4 target 100%";
            }
            else if (users.UnitKerjaID == 13)
            {
                ket1 = "Ket : target 110%";
                ket2 = "";
            }
            keterangan1.Text = ket1;
            keterangan2.Text = ket2;
        }
        [WebMethod]
        public static string GetPemakaian(string tgl)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            int depo= users.UnitKerjaID;
            List<MTC_Listrik> objListrik = new List<MTC_Listrik>();
            objListrik = MTC_ListrikFacade.RetrivePemakaian(tgl, depo);
            return JsonConvert.SerializeObject(objListrik);
        }

        [WebMethod]
        public static string GetEfisiensi(string tgl)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            int depo = users.UnitKerjaID;
            List<MTC_Listrik> objListrik = new List<MTC_Listrik>();
            objListrik = MTC_ListrikFacade.RetriveEfisiensi(tgl, depo);
            return JsonConvert.SerializeObject(objListrik);
        }
        [WebMethod]
        public static int InputPemakaian(MTC_Listrik obj)
        {
            int insert;
            DateTime Tanggal = obj.Tanggal;
            int Line = obj.Line;
            decimal kWhPJT = obj.kWhPJT;
            decimal kVarhPJT = obj.kVarhPJT;
            decimal kWhPLN = obj.kWhPLN;
            decimal kVarhPLN = obj.kVarhPLN;
            string Keterangan = obj.Keterangan;

            MTC_ListrikFacade inslist = new MTC_ListrikFacade();
            insert = inslist.InsertPemakaian(Tanggal,Line,kWhPJT,kVarhPJT,kWhPLN,kVarhPLN,Keterangan);
            return insert;
        }
        [WebMethod]
        public static int UpdatePemakaian(MTC_Listrik obj)
        {
            int update;
            DateTime Tanggal = obj.Tanggal;
            int Line = obj.Line;
            decimal kWhPJT = obj.kWhPJT;
            decimal kVarhPJT = obj.kVarhPJT;
            decimal kWhPLN = obj.kWhPLN;
            decimal kVarhPLN = obj.kVarhPLN;
            string Keterangan = obj.Keterangan;

            MTC_ListrikFacade updatelistrik = new MTC_ListrikFacade();
            update = updatelistrik.UpdatePemakaian(Tanggal, Line, kWhPJT, kVarhPJT, kWhPLN, kVarhPLN, Keterangan);
            return update;
        }

        [WebMethod]
        public static int Penyesuaian(MTC_Listrik obj)
        {
            int update;
            DateTime Tanggal = obj.Tanggal;
            decimal kWhPJT = obj.kWhPJT;
            decimal kVarhPJT = obj.kVarhPJT;
            decimal kWhPLN = obj.kWhPLN;
            decimal kVarhPLN = obj.kVarhPLN;

            MTC_ListrikFacade updatelistrik = new MTC_ListrikFacade();
            update = updatelistrik.Penyesuaian(Tanggal, kWhPJT, kVarhPJT, kWhPLN, kVarhPLN);
            return update;
        }

        

        [WebMethod]
        public static string Updatesarmut(string tahun,string bulan)
         {
            string update;
            MTC_ListrikFacade updatelistrik = new MTC_ListrikFacade();
            update = updatelistrik.updatesarmut(tahun,bulan);
            return update;
        }
        
    }
}