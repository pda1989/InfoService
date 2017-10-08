using System.Collections.Generic;
using System.Diagnostics;

namespace InfoService.Interfaces
{
    public interface IInstanceInfo
    {
        List<Process> ActiveProcesses { get; }
        string DomainName { get; }
        string IPAddress { get; }
    }
}