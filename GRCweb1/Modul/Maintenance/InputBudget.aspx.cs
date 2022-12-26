using BusinessFacade;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;

namespace GRCweb1.Modul.Maintenance
{
    public partial class InputBudget : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string Loadlist()
        {
            List<object> objbudget = new List<object>();
            objbudget = MTC_TargetBudgetFacade.Retrivenew();
            return JsonConvert.SerializeObject(objbudget);
        }

        [WebMethod]
        public static string Loaddetail(string id)
        {
            List<object> objbudget = new List<object>();
            objbudget = MTC_TargetBudgetFacade.Retrivebyid(id);
            return JsonConvert.SerializeObject(objbudget);
        }

        [WebMethod]

        public static int Simpan(MTC_TargetBudget obj)
        {
            int insert=0;
            Users users = (Users)HttpContext.Current.Session["Users"];
            string user = users.UserName;
            MTC_TargetBudget objt = new MTC_TargetBudget();
            objt.ID = obj.ID;
            objt.Tahun = obj.Tahun;
            objt.Smt = obj.Smt;
            objt.Jumlah = obj.Jumlah;
            objt.CreatedBy = user;
            int id = obj.ID;
           
            MTC_TargetBudgetFacade budget = new MTC_TargetBudgetFacade();
            if (id == 0)
            {
                insert = budget.Insert(objt);
            }
            else if (id != 0)
            {
                insert = budget.Update(objt);
            }
            return insert;
        }
    }
}