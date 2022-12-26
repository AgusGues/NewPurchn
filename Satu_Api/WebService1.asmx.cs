using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json;
using Domain;
using BusinessFacade;

////https://www.c-sharpcorner.com/uploadfile/ansh06031982/creating-web-services-in-net-which-returns-xml-and-json-dat/

namespace Satu_Api
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public DataSet xml_getAllItems(string userName, string password)
        {
            DataSet ds = new DataSet();
            UsersFacade usersFacade = new UsersFacade();
            string sql = "select top 1 ItemID as ItemCode, Description from Items";
            SqlDataAdapter da = new SqlDataAdapter(sql, ConfigurationManager.ConnectionStrings["ApiGRCBoard"].ToString());
            da.Fill(ds);

            return ds;
        }

        [WebMethod]
        public string json_getAllItems(string userName, string password)
        {
            DataSet ds = new DataSet();
            UsersFacade usersFacade = new UsersFacade();
            Users users = new Users();
            string sql = "select top 1 ItemID as ItemCode, Description from Items";
            SqlDataAdapter da = new SqlDataAdapter(sql, ConfigurationManager.ConnectionStrings["ApiGRCBoard"].ToString());
            da.Fill(ds);

            return JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);
        }
    }
}
