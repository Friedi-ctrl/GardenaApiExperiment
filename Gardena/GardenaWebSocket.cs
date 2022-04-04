using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Websocket.Client;

namespace GardenaApi.Gardena
{
    internal class GardenaWebSocket
    {
        async void StartWebSocket()
        {
            //var body = new WebSocketJsonBody
            //{
            //    data = new WebSocketBody.Data()
            //};

            //body.data.id = "request-1";
            //body.data.type = "WEBSOCKET";
            //body.data.attributes = new WebSocketBody.Attributes
            //{
            //    locationId = locationId
            //};

            //var request = new RestRequest("/v1/websocket");
            //var pt = ParameterType.HttpHeader;

            //request.AddParameter("accept", "application/vnd.api+json", pt);
            //request.AddParameter("X-Api-Key", clientId, pt);
            //request.AddParameter("Authorization", GardenaLoginData.token_type + " " + GardenaLoginData.access_token, pt);
            //request.AddParameter("Authorization-Provider", GardenaLoginData.provider, pt);
            //request.AddParameter("Content-Type", "application/vnd.api+json", pt);
            //request.AddJsonBody(body);


            //try
            //{
            //    var apiReturn = await GetApiData(smartUrl, request, Method.Post);
            //    var debugText = "Getting web socket URL...\r\n" + apiReturn.FormatedOutput;
            //    Console.WriteLine(debugText);

            //    var cont = JObject.Parse(apiReturn.Rest.Content);
            //    var webSocketUrl = cont["data"]["attributes"]["url"].ToString();


            //    WebSocket ws = new(webSocketUrl);
            //    ws.OnMessage += Ws_OnMessage;
            //    ws.OnOpen += Ws_OnOpen;
            //    ws.OnError += Ws_OnError;
            //    ws.OnClose += Ws_OnClose;

            //    pingTimer.Elapsed += (sender, args) =>
            //    {
            //        ws.Ping();
            //    };
            //    ws.ConnectAsync();

            //    pingTimer.Enabled = true;
            }
            //catch (Exception e)
            //{
            //    var debugText = "Error - could establish Web Socket connection.\r\n" + e;
            //    Console.WriteLine(debugText);
            //    HomeControlEvents.WriteWinEvent(debugText, System.Diagnostics.EventLogEntryType.Warning);
            //}
        }
        //private void Ws_OnOpen(object sender, EventArgs e)
        //{
        //    WebSocketRunning = true;
        //    var debugText = "\r\nGardena WebSocket opened...\r\n";
        //    Console.WriteLine(debugText);
        //}
        //private void Ws_OnMessage(object sender, MessageEventArgs e)
        //{
        //    try
        //    {
        //        TimerDelayWebSocketEvent.Stop();
        //        TimerDelayWebSocketEvent.Start();

        //        var debugText = FormatJson(e.Data);
        //        Console.WriteLine("\r\nWeb Socket Event at " + DateTime.Now + "\r\n" + debugText);

        //        var jt = JToken.Parse(e.Data);

        //        var type = jt["type"].ToString();
        //        if (type == "MOWER")
        //        {
        //            mowerJson = e.Data;
        //        }
        //        else if (type == "COMMON")
        //        {
        //            commonJson = e.Data;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var debugText = "\r\nGardena WebSocket error (Ws_OnMessage event) = " + ex.Message;
        //        Console.WriteLine(debugText);
        //        HomeControlEvents.WriteWinEvent(debugText, System.Diagnostics.EventLogEntryType.Error); ;
        //    }
        //}
        //private void Ws_OnError(object sender, ErrorEventArgs e)
        //{
        //    WebSocketRunning = false;
        //    var debugText = "\r\nGardena WebSocket error = " + e.Message;
        //    Console.WriteLine(debugText);
        //    HomeControlEvents.WriteWinEvent(debugText, System.Diagnostics.EventLogEntryType.Error);
        //    StartGardenaWebConnection();
        //}

        //private void Ws_OnClose(object sender, CloseEventArgs e)
        //{
        //    if (!e.WasClean)
        //    {
        //        WebSocketRunning = false;
        //        var debugText = "\r\nGardena WebSocket closing..\n" + e.Reason;
        //        Console.WriteLine(debugText);
        //        HomeControlEvents.WriteWinEvent(debugText, System.Diagnostics.EventLogEntryType.Warning);
        //        StartGardenaWebConnection();
        //    }
        //}
    //}
}
