using System;
using System.Reflection;

namespace InfoService
{
    public static class ServiceHelper
    {
        public static Version GetAPIVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version;
        }
    }
}
