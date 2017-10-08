using InfoService.Interfaces;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

namespace InfoService.Models
{
    public class PCInfo : IInstanceInfo
    {
        public List<Process> ActiveProcesses
        {
            get
            {
                return new List<Process>();
            }
        }

        public string DomainName
        {
            get
            {
                return string.Empty;
            }
        }

        public string IPAddress
        {
            get
            {
                return new IPAddress(0).ToString();
            }
        }
    }
}