using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Newtonsoft.Json;
namespace GRCweb1.Models
{
    public class JsonHelper
    {
        public string ToStringJson(object obj)
        {
            JsonSerializerSettings jsSettings = new JsonSerializerSettings();
            jsSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            StringBuilder sb = new StringBuilder();
            sb.Append(JsonConvert.SerializeObject(obj, Formatting.None, jsSettings));
            return sb.ToString();
        }
    }
}