using System.Net;
using System.Net.Http;

namespace InfoService
{
    public class WebServer : IInfoServer
    {
        protected IMessageHandler _messageHandler;


        public WebServer(IMessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
        }

        public void PerformCommand()
        {
            string message = RequestMessage();
            string result = _messageHandler.ProcessMessage(message);
            SendResult(result);
        }

        public string RequestMessage()
        {
            string uri = ServiceSettings.GetInstance().ServerName;
            var client = new HttpClient();
            var responseData = client.GetStringAsync(uri);
            return responseData.Result.Trim();
        }

        public void SendResult(string result)
        {
            string uri = ServiceSettings.GetInstance().ServerName;
            var client = new HttpClient();
            var content = new StringContent(result);
            var response = client.PostAsync(uri, content);
            var responseData = response.Result.Content.ReadAsStringAsync();
            string resultFromServer = responseData.Result.Trim();
            if (resultFromServer != "OK")
                throw new WebException($"Invalid server result: \n {resultFromServer}");
        }
    }
}
