using GardenaApi.Gardena.WebSocketBody;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Websocket.Client;
using WebSocketSharp;

namespace GardenaApi.Gardena
{
    internal class GardenaApi
    {
        private const string loginUrl = "https://api.authentication.husqvarnagroup.dev";
        private const string passWord = "69678b9aa";
        private const string smartUrl = "https://api.smart.gardena.dev";
        private const string userName = "s-friede@gmx.de";
        private const string xApiKey = "858a768c-3bcc-49bc-8f55-cd1cf5047d9a";
        private RestClient client;
        private LoginData GardenaLoginData = new();
        private string locationId;

        public GardenaApi()
        {
            var loginData = Properties.Settings.Default.GardenaLoginData;                // read App Configuration data
            GardenaLoginData = JsonConvert.DeserializeObject<LoginData>(loginData);
            locationId = Properties.Settings.Default.LocationId;
        }

        public async Task GetToken()                                                          // get new token by new login
        {
            var request = new RestRequest("/v1/oauth2/token");

            request.AddParameter("client_id", xApiKey);
            request.AddParameter("grant_type", "password");
            request.AddParameter("username", userName);
            request.AddParameter("password", passWord);

            var apiReturn = await GetApiData(loginUrl, request, Method.Post);
            var debugText = "\r\nLogging in and  getting new token... \r\n" + apiReturn.FormatedOutput;
            OnUpdateDebugText(new DebugTextEventArgs(debugText));

            if (apiReturn.Response.IsSuccessful)
            {
                GardenaLoginData = JsonConvert.DeserializeObject<LoginData>(apiReturn.Response.Content);            // convert json Response to c# classes
                Properties.Settings.Default.GardenaLoginData = apiReturn.Response.Content;
                Properties.Settings.Default.Save();
            }
        }

        public async Task RefrechToken()                                                      // just refresh token within 10d after first login
        {
            var request = new RestRequest("/v1/oauth2/token");
            request.AddParameter("grant_type", "refresh_token");
            request.AddParameter("client_id", xApiKey);
            request.AddParameter("refresh_token", GardenaLoginData.refresh_token);

            var apiReturn = await GetApiData(loginUrl, request, Method.Post);
            var debugText = "\r\nRefreshing Token...\r\n" + apiReturn.FormatedOutput;
            OnUpdateDebugText(new DebugTextEventArgs(debugText));

            if (apiReturn.Response.IsSuccessful)
            {
                GardenaLoginData = JsonConvert.DeserializeObject<LoginData>(apiReturn.Response.Content);
                Properties.Settings.Default.GardenaLoginData = apiReturn.Response.Content;
                Properties.Settings.Default.Save();
            }
        }

        public async Task GetLocationId()
        {
            var request = new RestRequest("/v1/locations");

            request.AddHeader("accept", "application/vnd.api+json");
            request.AddHeader("X-Api-Key", xApiKey);
            request.AddHeader("Authorization", GardenaLoginData.token_type + " " + GardenaLoginData.access_token);
            request.AddHeader("Authorization-Provider", GardenaLoginData.provider);

            var apiReturn = await GetApiData(smartUrl, request, Method.Get);
            var debugText = "\r\nGetting location ID... \r\n" + apiReturn.FormatedOutput;
            OnUpdateDebugText(new DebugTextEventArgs(debugText));

            if (apiReturn.Response.IsSuccessful)
            {
                var cont = JObject.Parse(apiReturn.Response.Content);
                locationId = cont["data"][0]["id"].ToString().Trim((char)1);
                Properties.Settings.Default.LocationId = locationId;
                Properties.Settings.Default.Save();
            }
        }

        public async Task<DataTable> GetStatus()
        {
            var request = new RestRequest("/v1/locations/" + locationId);

            request.AddHeader("accept", "application/vnd.api+json");
            request.AddHeader("Authorization", GardenaLoginData.token_type + " " + GardenaLoginData.access_token);
            request.AddHeader("Authorization-Provider", GardenaLoginData.provider);
            request.AddHeader("X-Api-Key", xApiKey);

            var apiReturn = await GetApiData(smartUrl, request, Method.Get);
            var debugText = "\r\nGetting Status... \r\n" + apiReturn.FormatedOutput;
            OnUpdateDebugText(new DebugTextEventArgs(debugText));

            var GardenaRespons = JObject.Parse(apiReturn.Response.Content);
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

        private async Task<Uri> GetWebSocketUrl()
        {
            var body = new WebSocketJsonBody
            {
                data = new Data()
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
            var debugText = "\r\nGetting web socket URL...\r\n" + apiReturn.FormatedOutput;
            OnUpdateDebugText(new DebugTextEventArgs(debugText));

            var cont = JObject.Parse(apiReturn.Response.Content);
            var webSocketUrl = cont["data"]["attributes"]["url"].ToString();

            return new Uri(webSocketUrl);
        }

        public async Task StartWebSocketClient()
        {
            var uri = await GetWebSocketUrl();
            using (var client = new WebsocketClient(uri))
            {
                client.ReconnectTimeout = null;
                client.ErrorReconnectTimeout = TimeSpan.FromSeconds(30);
                client.MessageReceived.Subscribe(msg => OnUpdateDebugText(new DebugTextEventArgs(FormatJson(msg.Text))));
                client.ReconnectionHappened.Subscribe(msg => OnUpdateDebugText(new DebugTextEventArgs(FormatJson(msg.ToString()))));
                client.DisconnectionHappened.Subscribe(msg => OnUpdateDebugText(new DebugTextEventArgs(FormatJson(msg.ToString()))));
                await client.Start();
                await StartSendingPing(client);
            }

        }

        private static string FormatJson(string content)// (RestResponse response)
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
            return jsonData;
        }

        private async Task StartSendingPing(WebsocketClient client)
        {
            while (client.IsRunning)
            {
                await Task.Delay(1000 * 150);
                client.Send(" ");
                OnUpdateDebugText(new DebugTextEventArgs(DateTime.Now + " Client is running -> ping sent\n"));
            }
        }

        //public string StartWebSocket()
        //{
        //    try
        //    {
        //        var webSocketUrl = GetWebSocketUrl();
        //        WebSocket ws = new(webSocketUrl);
        //        ws.OnMessage += Ws_OnMessage;
        //        ws.OnOpen += Ws_OnOpen;
        //        ws.OnError += Ws_OnError;
        //        ws.OnClose += Ws_OnClose;
        //        ws.ConnectAsync();
        //        return "starting Web Socket client...";
        //    }
        //    catch (Exception e)
        //    {
        //        var debugText = "Error - could establish Web Socket connection.\r\n" + e;
        //        Console.WriteLine(debugText);
        //        return debugText;
        //    }
        //}


        //private void Ws_OnClose(object sender, CloseEventArgs e)
        //{
        //    if (!e.WasClean)
        //    {
        //        var debugText = "\r\nGardena WebSocket closing..\n" + e.Reason;
        //        Console.WriteLine(debugText);
        //    }
        //}

        //private void Ws_OnError(object sender, ErrorEventArgs e)
        //{
        //    var debugText = "\r\nGardena WebSocket error = " + e.Message;
        //    Console.WriteLine(debugText);
        //}

        //private void Ws_OnMessage(object sender, MessageEventArgs e)
        //{
        //    try
        //    {
        //        var debugText = FormatJson(e.Data);
        //        Console.WriteLine("\r\nWeb Socket Event at " + DateTime.Now + "\r\n" + debugText);

        //        var jt = JToken.Parse(e.Data);

        //        var type = jt["type"].ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        var debugText = "\r\nGardena WebSocket error (Ws_OnMessage event) = " + ex.Message;
        //        Console.WriteLine(debugText);
        //    }
        //}

        //private void Ws_OnOpen(object sender, EventArgs e)
        //{
        //    var debugText = "\r\nGardena WebSocket opened...\r\n";
        //    Console.WriteLine(debugText);
        //}

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
                Response = response,
                FormatedOutput = requestParameter + "\r\n" + response.StatusDescription + "\r\n" + jConvert + "\r\n\r\n"
            };
        }


        #endregion Request Methods

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

        #endregion Debug - Show request parameter

        public event EventHandler<DebugTextEventArgs> UpdateDebugText;

        protected virtual void OnUpdateDebugText(DebugTextEventArgs e)
        {
            UpdateDebugText?.Invoke(this, e);
        }
    }
}