using System.Collections.Generic;

namespace InfoService
{
    public class InputMessage
    {
        public string Command { get; set; }
        public List<string> Parameters { get; set; }
    }
}
