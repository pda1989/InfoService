using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;

namespace InfoService
{
    public class WebUpdater : IUpdater
    {
        protected ServiceEventLog _log;

        public WebUpdater(ServiceEventLog log)
        {
            _log = log;
        }

        public void Update(Assembly assembly, string updateFilePath, string fileHash, string tempFileName)
        {
            // download file
            string downloadedFileHash = DownloadFile(updateFilePath, @"C:\123.txt");
            if (downloadedFileHash.ToUpper() != fileHash.ToUpper())
                throw new InvalidDataException("Downloaded file corrupted");

            // start update process
            Process.Start(new ProcessStartInfo
            {
                Arguments = "/C Choise /C Y /N /D Y /T 4 & Del /F /Q {0} & Choise /C Y /N /D Y /T 2 & Move /Y {1} {2} & Start \"\" /D \"dir\" \"file name\" args",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "cmd.exe"
            });

            // stop the service
            ServiceHelper.StopService(assembly.GetName().Name);
        }

        private string DownloadFile(string uri, string tempFileName)
        {
            using (var client = new WebClient())
            {
                var file = client.DownloadData(uri);
                File.WriteAllBytes(tempFileName, file);
                return ServiceHelper.GetHash(tempFileName);
            }
        }
    }
}
