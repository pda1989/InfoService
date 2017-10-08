using InfoService.Interfaces;
using Newtonsoft.Json;
using System;

namespace InfoService.Implementations
{
    public class JsonSerialyzer : ISerializer
    {
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
    }
}