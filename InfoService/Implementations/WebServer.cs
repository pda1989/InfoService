using InfoService.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace InfoService.Implementations
{
    public class WebServer : IInfoServer
    {
        protected HttpClient _client;
        protected IMessageHandler _messageHandler;
        protected ServiceSettings _settings;

        public WebServer(IMessageHandler messageHandler, ServiceSettings settings, HttpClient client)
        {
            _messageHandler = messageHandler;
            _settings = settings;
            _client = client;
        }

        public void PerformCommand()
        {
            //string message = RequestMessage();
            var message = "{\"Command\":\"GetInfo\"}";
            string result = _messageHandler.ProcessMessage(message);
            //SendResult(result);
        }

        public virtual string RequestMessage()
        {
            string uri = _settings?.ServerName;
            var responseData = _client?.GetStringAsync(uri);
            return responseData.Result.Trim();
        }

        public virtual void SendResult(string result)
        {
            string uri = _settings?.ServerName;
            var values = new Dictionary<string, string>
            {
                { "result", result }
            };
            var content = new FormUrlEncodedContent(values);
            var response = _client?.PostAsync(uri, content);
            var responseData = response.Result.Content.ReadAsStringAsync();
            string resultFromServer = responseData.Result.Trim();
            if (resultFromServer != "OK")
                throw new WebException($"Invalid server result: \n {resultFromServer}");
        }
    }
}