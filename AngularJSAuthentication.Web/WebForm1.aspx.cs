using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;
using System.Web.Script.Serialization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
//using System.Web;
using System.Threading.Tasks;
using System.Web.UI;

namespace AngularJSAuthentication.Web
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private string xTenant;
        static HttpClient client = new HttpClient();
        static readonly string baseUrl = "http://localhost:26264";
        protected void Page_Load(object sender, EventArgs e)
        {
            //test1();    //ok

            RunAsync().Wait();

        }

        private void test1()
        {
            //var httpClient = new HttpClient();
            //httpClient.DefaultRequestHeaders.Add("X-Tenant", xTenant);
            //var stringContent = String.Concat("grant_type=password&username=", HttpUtility.UrlEncode("1"),
            //    "&123456=", HttpUtility.UrlEncode("MySuperP@ss!"));
            //var httpContent1 = new StringContent(stringContent, Encoding.UTF8, "application/x-www-form-urlencoded");
            //var postTask = httpClient.PostAsync("http://localhost:26264/token", httpContent1);
            //postTask.Wait();
            //var postResult = postTask.Result;
            //var content = postResult.Content.ReadAsStringAsync().Result;
            //dynamic jsonRes = JsonConvert.DeserializeObject(content);
            //string access_token = jsonRes.access_token;

            var apiUrl = "http://localhost:26264/token";
            var client = new HttpClient();
            client.Timeout = new TimeSpan(1, 0, 0);
            var loginData = new Dictionary<string, string>
                {
                    {"UserName", "1"},
                    {"Password", "123456"},
                    {"grant_type", "password"}
                };
            var content = new FormUrlEncodedContent(loginData);
            var response = client.PostAsync(apiUrl, content).Result;

            var postResult = response.Content.ReadAsStringAsync();

            //var result = JsonConvert.DeserializeObject<>(postResult);
        }

        private async Task RunAsync()
        {
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                myLogin login = new myLogin();
                login.UserName = "1";
                login.Password = "123456";
                var accessToken = await Authenticate(login);
                client.DefaultRequestHeaders.Add("Authorization","Brearer"+accessToken.auth_token);

                DisplayAJAXMessage(this, accessToken.auth_token.ToString());
            }
            catch (Exception e)
            {
                DisplayAJAXMessage(this, "App Error : " + e.Message);
            }
        }
        public async Task<SecurityToken> Authenticate(myLogin login)
        {
            var apiUrl = "http://localhost:26264/token";
            var client = new HttpClient();
            client.Timeout = new TimeSpan(1, 0, 0);
            var loginData = new Dictionary<string, string>
                {
                    {"UserName", login.UserName},
                    {"Password", login.Password},
                    {"grant_type", "password"}
                };
            var content = new FormUrlEncodedContent(loginData);
            var response = client.PostAsync(apiUrl, content).Result;

            var token = await response.Content.ReadAsStringAsync();
            SecurityToken myToken = new SecurityToken();
            dynamic jsonRes = JsonConvert.DeserializeObject(token);
            string access_token = jsonRes.access_token;
            myToken.auth_token = jsonRes.access_token;

            return myToken;
        }
        static async Task<TransactionResult> DeserializeResponseContent(HttpResponseMessage response)
        {
            var transactionResult = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TransactionResult>(transactionResult);
        }
        public class TransactionResult
        {
            public int? AccountNumber { get; set; }
            public bool IsSuccessful { get; set; }
            public decimal? Balance { get; set; }
            public string Currency { get; set; }
            public string Message { get; set; }
        }
        public class SecurityToken
        {
            public string auth_token { get; set; }
        }
        public class myLogin
        {
            public string UserName { get; set; }
            public string Password { get; set; }

        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            System.Web.UI.ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }






    }
}