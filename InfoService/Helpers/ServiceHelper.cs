using System;
using System.Collections;
using System.Configuration.Install;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.ServiceProcess;
using System.Text;

namespace InfoService.Helpers
{
    public static class ServiceHelper
    {
        private static AssemblyInstaller GetInstaller(Type serviceType) => new AssemblyInstaller
        {
            Assembly = serviceType.Assembly,
            CommandLine = null,
            UseNewContext = true
        };

        private static bool IsServiceInstalled(string serviceName)
        {
            using (var controller = new ServiceController(serviceName))
                try
                {
                    var status = controller.Status;
                    return true;
                }
                catch
                {
                    return false;
                }
        }

        private static bool IsServiceRunning(string serviceName)
        {
            using (var controller = new ServiceController(serviceName))
            {
                if (!IsServiceInstalled(serviceName))
                    return false;
                return (controller.Status == ServiceControllerStatus.Running);
            }
        }

        public static Version GetAPIVersion() => Assembly.GetExecutingAssembly().GetName().Version;

        public static string GetHash(string fileName)
        {
            var bytes = MD5.Create().ComputeHash(new FileStream(fileName, FileMode.Open));
            var builder = new StringBuilder();
            foreach (var b in bytes)
                builder.AppendFormat("{0:X2}", b);
            return builder.ToString();
        }

        public static void InstallService(string serviceName)
        {
            if (IsServiceInstalled(serviceName))
                return;
            using (var installer = GetInstaller(typeof(InfoService)))
            {
                IDictionary state = new Hashtable();
                try
                {
                    installer.Install(state);
                    installer.Commit(state);
                }
                catch
                {
                    installer.Rollback(state);
                }
            }
        }

        public static void StartService(string serviceName)
        {
            if (!IsServiceInstalled(serviceName))
                return;
            using (var controller = new ServiceController(serviceName))
            {
                if (controller.Status != ServiceControllerStatus.Running)
                {
                    controller.Start();
                    controller.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(10));
                }
            }
        }

        public static void StopService(string serviceName)
        {
            if (!IsServiceInstalled(serviceName))
                return;
            using (var controller = new ServiceController(serviceName))
            {
                if (controller.Status != ServiceControllerStatus.Stopped)
                {
                    controller.Stop();
                    controller.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(10));
                }
            }
        }

        public static void UninstallService(string serviceName)
        {
            if (!IsServiceInstalled(serviceName))
                return;
            using (var installer = GetInstaller(typeof(InfoService)))
            {
                IDictionary state = new Hashtable();
                installer.Uninstall(state);
            }
        }
    }
}