using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

namespace InfoService
{
    public class PCInfo : IInstanceInfo
    {
        public IPAddress IPAddress
        {
            get
            {
                return new IPAddress(0);
            }
        }
        public string DomainName
        {
            get
            {
                return string.Empty;
            }
        }
        public List<Process> ActiveProcesses
        {
            get
            {
                return new List<Process>();
            }
        }
    }
}
