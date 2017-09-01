using System.Collections.Generic;

namespace InfoService
{
    public class InputMessage
    {
        public string Command { get; set; } = string.Empty;
        public List<string> Parameters { get; set; } = new List<string>();
    }
}
