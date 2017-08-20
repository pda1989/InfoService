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
            return "{\"Command\":\"GetAPIVersion\"}";
        }

        public void SendResult(string result)
        {
        }
    }
}
