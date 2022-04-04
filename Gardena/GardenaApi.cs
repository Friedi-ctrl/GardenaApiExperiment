using Newtonsoft.Json;
using WebSocketSharp;
using RestSharp;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Threading.Tasks;
using System;
using GardenaApi.Gardena.WebSocketBody;

namespace GardenaApi.Gardena
{
    class GardenaApi
    {
        const string loginUrl = "https://api.authentication.husqvarnagroup.dev";
        const string smartUrl = "https://api.smart.gardena.dev";
        const string xApiKey = "858a768c-3bcc-49bc-8f55-cd1cf5047d9a";
        const string userName = "s-friede@gmx.de";
        const string passWord = "69678b9aa";
        string locationId, webSocketUrl;
        RestClient client;
        LoginData GardenaLoginData = new LoginData();

        public GardenaApi()
        {
            var loginData = Properties.Settings.Default.GardenaLoginData;                // read App Configuration data
            GardenaLoginData = JsonConvert.DeserializeObject<LoginData>(loginData);
            locationId = Properties.Settings.Default.LocationId;
        }

        public async Task<string> GetToken()                                                          // get new token by new login
        {

            var request = new RestRequest("/v1/oauth2/token");

            request.AddParameter("client_id", xApiKey);
            request.AddParameter("grant_type", "password");
            request.AddParameter("username", userName);
            request.AddParameter("password", passWord);

            var apiReturn = await GetApiData(loginUrl, request, Method.Post);
            var debugText = "\r\nLogging in and  getting new token... \r\n" + apiReturn.FormatedOutput;
            Console.WriteLine(debugText);

            if (apiReturn.Rest.IsSuccessful)
            {
                GardenaLoginData = JsonConvert.DeserializeObject<LoginData>(apiReturn.Rest.Content);            // convert json Response to c# classes
                Properties.Settings.Default.GardenaLoginData = apiReturn.Rest.Content;
                Properties.Settings.Default.Save();
            }
            return debugText;
        }

        public async Task<string> RefrechToken()                                                      // just refresh token within 10d after first login
        {
            var request = new RestRequest("/v1/oauth2/token");
            request.AddParameter("grant_type", "refresh_token");
            request.AddParameter("client_id", xApiKey);
            request.AddParameter("refresh_token", GardenaLoginData.refresh_token);


            var apiReturn = await GetApiData(loginUrl, request, Method.Post);
            var debugText = "Refreshing Token...\r\n" + apiReturn.FormatedOutput;
            Console.WriteLine(debugText);

            if (apiReturn.Rest.IsSuccessful)
            {
                GardenaLoginData = JsonConvert.DeserializeObject<LoginData>(apiReturn.Rest.Content);
                Properties.Settings.Default.GardenaLoginData = apiReturn.Rest.Content;
                Properties.Settings.Default.Save();
            }
            return debugText;
        }

        public async Task<string> GetLocationId()
        {
            var request = new RestRequest("/v1/locations");

            request.AddHeader("accept", "application/vnd.api+json");
            request.AddHeader("X-Api-Key", xApiKey);
            request.AddHeader("Authorization", GardenaLoginData.token_type + " " + GardenaLoginData.access_token);
            request.AddHeader("Authorization-Provider", GardenaLoginData.provider);

            var apiReturn = await GetApiData(smartUrl, request, Method.Get);
            var debugText = "Getting location ID... \r\n" + apiReturn.FormatedOutput;
            Console.WriteLine(debugText);

            if (apiReturn.Rest.IsSuccessful)
            {
                var cont = JObject.Parse(apiReturn.Rest.Content);
                locationId = cont["data"][0]["id"].ToString().Trim((char)1);
                Properties.Settings.Default.LocationId = locationId;
                Properties.Settings.Default.Save();
                //StartWebSocket();
            }

            return debugText;
        }

        public async Task<DataTable>  GetStatus()
        {
            var request = new RestRequest("/v1/locations/" + locationId);
            //var pt = ParameterType.HttpHeader;

            request.AddHeader("accept", "application/vnd.api+json");
            request.AddHeader("Authorization", GardenaLoginData.token_type + " " + GardenaLoginData.access_token);
            request.AddHeader("Authorization-Provider", GardenaLoginData.provider);
            request.AddHeader("X-Api-Key", xApiKey);

            var apiReturn = await GetApiData(smartUrl, request, Method.Get);
            var debugText = "Getting Status... \r\n" + apiReturn.FormatedOutput;
            Console.WriteLine(debugText);

            var GardenaRespons = JObject.Parse(apiReturn.Rest.Content);
            var resultsFiltered = GardenaRespons["included"][2]["attributes"].Children();
            DataTable dt = new();
            dt.Columns.Add("Description");
            dt.Columns.Add("Value"); ;

            foreach (JToken result in resultsFiltered)
            {
                JProperty prop = result.ToObject<JProperty>();
                JProperty values = prop.ToObject<JProperty>();
                dt.Rows.Add(prop.Name, values.Value.First.First);
            }

            return dt;
        }
        public async Task<string> GetWebSocketUrl()
        {
            var body = new WebSocketJsonBody
            {
                data = new WebSocketBody.Data()
            };

            body.data.id = "request-1";
            body.data.type = "WEBSOCKET";
            body.data.attributes = new WebSocketBody.Attributes
            {
                locationId = locationId
            };

            var request = new RestRequest("/v1/websocket");

            request.AddHeader("accept", "application/vnd.api+json");
            request.AddHeader("X-Api-Key", xApiKey);
            request.AddHeader("Authorization", GardenaLoginData.token_type + " " + GardenaLoginData.access_token);
            request.AddHeader("Authorization-Provider", GardenaLoginData.provider);
            request.AddHeader("Content-Type", "application/vnd.api+json");
            request.AddJsonBody(body);



            var apiReturn = await GetApiData(smartUrl, request, Method.Post);
            var debugText = "Getting web socket URL...\r\n" + apiReturn.FormatedOutput;
            Console.WriteLine(debugText);

            var cont = JObject.Parse(apiReturn.Rest.Content);
            webSocketUrl = cont["data"]["attributes"]["url"].ToString();
            return debugText;
        }
        public string StartWebSocket()
        {
            try
            {
                WebSocket ws = new(webSocketUrl);
                ws.OnMessage += Ws_OnMessage;
                ws.OnOpen += Ws_OnOpen;
                ws.OnError += Ws_OnError;
                ws.OnClose += Ws_OnClose;
                ws.ConnectAsync();
                return "starting Web Socket client...";
            }
            catch (Exception e)
            {
                var debugText = "Error - could establish Web Socket connection.\r\n" + e;
                Console.WriteLine(debugText);
                return debugText;
            }
        }
        private void Ws_OnOpen(object sender, EventArgs e)
        {
            var debugText = "\r\nGardena WebSocket opened...\r\n";
            Console.WriteLine(debugText);
        }
        private void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            try
            {

                var debugText = FormatJson(e.Data);
                Console.WriteLine("\r\nWeb Socket Event at " + DateTime.Now + "\r\n" + debugText);

                var jt = JToken.Parse(e.Data);

                var type = jt["type"].ToString();
                //if (type == "MOWER")
                //{
                //    mowerJson = e.Data;
                //}
                //else if (type == "COMMON")
                //{
                //    commonJson = e.Data;
                //}
            }
            catch (Exception ex)
            {
                var debugText = "\r\nGardena WebSocket error (Ws_OnMessage event) = " + ex.Message;
                Console.WriteLine(debugText);
            }
        }
        private void Ws_OnError(object sender, ErrorEventArgs e)
        {

            var debugText = "\r\nGardena WebSocket error = " + e.Message;
            Console.WriteLine(debugText);

        }
        private void Ws_OnClose(object sender, CloseEventArgs e)
        {
            if (!e.WasClean)
            {
                var debugText = "\r\nGardena WebSocket closing..\n" + e.Reason;
                Console.WriteLine(debugText);
            }
        }

        #region Request Methods
        private async Task<ApiReturn> GetApiData(string url, RestRequest request, Method method)
        {
            switch (method)                 // check if method is supported
            {
                case Method.Post:
                    break;
                case Method.Get:
                    break;
                case Method.Put:
                    break;
                default:
                    Console.WriteLine("Request Method not supported!");
                    throw new ArgumentException("Request method is not supported", nameof(method));
            }

            var requestParameter = RequestParamToSring(request); // Debug - Show request parameter
            client = new RestClient(url);
            RestResponse response;
            request.Method = method;
            response = await client.ExecuteAsync(request);

            string jConvert;
            if (response.Content.Length > 0)
            {
                jConvert = FormatJson(response.Content);
            }
            else
            {
                jConvert = response.StatusCode.ToString();
            }

            return new ApiReturn
            {
                Rest = response,
                FormatedOutput = requestParameter + "\r\n" + response.StatusDescription + "\r\n" + jConvert + "\r\n\r\n"
            };
        }

        //private string GetRequest(RestClient client, RestRequest request)
        //{
        //    var requestParameter = RequestParamToSring(request); // Debug - Show request parameter

        //    var response = client.g(request);
        //    if (response.IsSuccessful)
        //    {
        //        Properties.Settings.Default.GardenaSmartData = response.Content;
        //        Properties.Settings.Default.Save();

        //        GardenaSmartData = JsonConvert.DeserializeObject<SmartData>(response.Content);            // convert json respons to c# classes
        //        var reslultJson = JsonConvert.SerializeObject(GardenaSmartData, Formatting.Indented);

        //        var id = JObject.Parse(response.Content);
        //        _ = id["data"][0]["id"].ToString().Trim((char)1);


        //        return requestParameter + "\n" + reslultJson + "\n";
        //    }
        //    else
        //    {
        //        var resultJson = JsonConvert.DeserializeObject(response.Content);
        //        var res = resultJson != null ? resultJson.ToString() : response.ErrorMessage;
        //        return res;
        //    }
        //}
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

        static string FormatJson(string content)// (RestResponse response)
        {
            try
            {
                JToken jt = JToken.Parse(content);//(response.Content);
                var jConvert = jt.ToString(Formatting.Indented);
                return jConvert;
            }
            catch (JsonReaderException ex)
            {
                return ReturnJsonErrorMsg(content, ex);
            }
            catch (Exception ex)
            {
                return ReturnJsonErrorMsg(content, ex);
            }
        }
        private static string ReturnJsonErrorMsg(string content, Exception ex)
        {
            var myData = new                                        //Create object
            {
                stringToConvert = content,
                errorFormatJsonMethode = ex.Message,
            };

            string jsonData = JsonConvert.SerializeObject(myData);  //Tarnform it to Json object

            Console.WriteLine(jsonData);                            //Print the Json object 
            //HomeControlEvents.WriteWinEvent(jsonData, System.Diagnostics.EventLogEntryType.Warning);
            return jsonData;
        }
    }
}
