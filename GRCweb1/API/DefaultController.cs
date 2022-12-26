using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json;
using Domain;
using BusinessFacade;
using System.Collections;

namespace GRCweb1.API
{
    public class DefaultController : ApiController
    {
        // GET api/<controller>

        // GET api/<controller>

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };

        }
        public string Get(int opid)
        {
            //http://localhost:10858/API/Default?opid=1

            return "OK";
        }
        //public string Get(int opid, string test)
        //{
        //    //http://localhost:10858/API/Default?opid=1&test="1"
        //    //http://localhost:10858/API/Default?opid=1&test=1
        //    return "OK";
        //}
        public string Get(int opid, string tglsche)
        {
            DataFor data = new DataFor();
            DataForFacade dfF = new DataForFacade();
            string arrData =  dfF.ViewStokPabrikperOPid(opid, tglsche);
            string strData = string.Empty;
            if (arrData.Length > 0)
                strData = "Sisa Stok : " + arrData;
            else
                strData = "zxzxzx";

            return JsonConvert.SerializeObject(strData, Newtonsoft.Json.Formatting.Indented);
            //return "iko";

        }

    }
}
