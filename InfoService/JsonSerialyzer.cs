using System;
using Newtonsoft.Json;

namespace InfoService
{
    public class JsonSerialyzer : ISerializer
    {
        public string Serialize<T>(T message)
        {
            try
            {
                return JsonConvert.SerializeObject(message);
            }
            catch (Exception exception)
            {
                throw new ArgumentException($"Cannot serialize message: \n{exception.ToString()}");
            }
        }

        public T Deserialize<T>(string message)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(message);
            }
            catch (Exception exception)
            {
                throw new ArgumentException($"Cannot deserialize message: \n{exception.ToString()}");
            }
        }
    }
}
