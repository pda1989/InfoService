using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

namespace InfoService
{
    public interface IInstanceInfo
    {
        IPAddress IPAddress { get; }
        string DomainName { get; }
        List<Process> ActiveProcesses { get; }
    }
}
