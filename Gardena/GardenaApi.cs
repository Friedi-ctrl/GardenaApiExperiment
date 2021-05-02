using Newtonsoft.Json;
using System.Linq;
using RestSharp;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data;

namespace GardenaApi.Gardena
{
    class GardenaApi
    {
        const string loginUrl = "https://api.authentication.husqvarnagroup.dev";
        const string smartURL = "https://api.smart.gardena.dev";
        const string clientId = "8fb199ee-8dab-4df4-a1b5-7b83a0fc6a3f";
        const string grantType = "password";
        const string userName = "s-friede@gmx.de";
        const string passWord = "69678b9aa";

        LoginData GardenaLoginData = new LoginData();
        SmartData GardenaSmartData = new SmartData();
        public GardenaApi()
        {
            var loginData = Properties.Settings.Default.GardenaLoginData;                // read App Config data
            GardenaLoginData = JsonConvert.DeserializeObject<LoginData>(loginData);

            var SmartData = Properties.Settings.Default.GardenaSmartData;                // read App Config data
            //GardenaSmartData = JsonConvert.DeserializeObject<SmartData>(SmartData);
        }
        
        public string GetToken()
        {
            var client = new RestClient(loginUrl);
            var request = new RestRequest("/v1/oauth2/token");

            request.AddParameter("client_id", clientId);
            request.AddParameter("grant_type", grantType);
            request.AddParameter("username", userName);
            request.AddParameter("password", passWord);

            return PostRequest(client, request);
        }

        public string RefreshToken() // have to use within 10d after first login
        {
            var client = new RestClient(loginUrl);
            var request = new RestRequest("/v1/oauth2/token");
 
            request.AddParameter("grant_type", "refresh_token");
            request.AddParameter("client_id", clientId);
            request.AddParameter("refresh_token", GardenaLoginData.refresh_token);

            return PostRequest(client, request);
        }

        public string GetLocation()
        {
            var client = new RestClient(smartURL);
            var request = new RestRequest("/v1/locations");
            var pt = ParameterType.HttpHeader;

            request.AddParameter("accept", "application/vnd.api+json", pt);
            request.AddParameter( "X-Api-Key", clientId, pt);
            request.AddParameter("Authorization", GardenaLoginData.token_type +" "+ GardenaLoginData.access_token , pt );
            request.AddParameter("Authorization-Provider", GardenaLoginData.provider, pt);

            return GetRequest(client, request);
        }

        public DataTable GetStatus()
        {
            var id = (from c in GardenaSmartData.Data select c.Id).FirstOrDefault();            // get id
            var client = new RestClient(smartURL);
            var request = new RestRequest("/v1/locations/" + id);
            var pt = ParameterType.HttpHeader;

            request.AddParameter("accept", "application/vnd.api+json", pt);
            request.AddParameter("Authorization", GardenaLoginData.token_type + " " + GardenaLoginData.access_token + "", pt);
            request.AddParameter("Authorization-Provider", GardenaLoginData.provider, pt);
            request.AddParameter("X-Api-Key", clientId, pt);


            var answer = GetRequest(client, request);
            //var response = client.Get(request);

            //var GardenaRespons = JObject.Parse(response.Content);
            //var resultsFiltered = GardenaRespons["included"][2]["attributes"].Children();
            DataTable  dt = new();
            dt.Columns.Add("Description");
            dt.Columns.Add("Value");;

            //foreach (JToken result in resultsFiltered)
            //{
            //    JProperty prop = result.ToObject<JProperty>();
            //    JProperty values = prop.ToObject<JProperty>();
            //    dt.Rows.Add(prop.Name, values.Value.First.First);
            //}

            return dt;
        }

        #region Request Methods
        private string PostRequest(RestClient client, RestRequest request)
        {
            var requestParameter = RequestParamToSring(request); // Debug - Show request parameter

            var response = client.Post(request);
            if (response.IsSuccessful)
            {
                Properties.Settings.Default.GardenaLoginData = response.Content;
                Properties.Settings.Default.Save();

                GardenaLoginData = JsonConvert.DeserializeObject<LoginData>(response.Content);            // convert json respons to c# classes
                var reslultJson = JsonConvert.SerializeObject(GardenaLoginData, Formatting.Indented);
                return requestParameter + "\n" + reslultJson + "\n";
            }
            else
            {
                var strJson = JsonConvert.DeserializeObject(response.Content);
                var res = strJson != null ? strJson.ToString() : response.ErrorMessage;
                return res;
            }
        }

        private string GetRequest(RestClient client, RestRequest request)
        {
            var requestParameter = RequestParamToSring(request); // Debug - Show request parameter

            var response = client.Get(request);
            if (response.IsSuccessful)
            {
                Properties.Settings.Default.GardenaSmartData = response.Content;
                Properties.Settings.Default.Save();

                GardenaSmartData = JsonConvert.DeserializeObject<SmartData>(response.Content);            // convert json respons to c# classes
                var reslultJson = JsonConvert.SerializeObject(GardenaSmartData, Formatting.Indented);

                var id = JObject.Parse(response.Content);
                _ = id["data"][0]["id"].ToString().Trim((char)1);


                return requestParameter + "\n" + reslultJson + "\n";
            }
            else
            {
                var resultJson = JsonConvert.DeserializeObject(response.Content);
                var res = resultJson != null ? resultJson.ToString() : response.ErrorMessage;
                return res;
            }
        } 
        #endregion

        #region Debug - Show request parameter
        private static string RequestParamToSring(RestRequest request)
        {

            var sb = new StringBuilder();
            foreach (var param in request.Parameters)
            {
                sb.AppendFormat("{0}: {1}\r\n", param.Name, param.Value);
            }
            return $"\n{sb}\n";
        }
        #endregion
    }
}
