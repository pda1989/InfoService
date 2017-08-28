using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;

namespace InfoService
{
    public class WebUpdater : IUpdater
    {
        public void Update(Assembly assembly, string updateFilePath, string fileHash)
        {
            // download file
            string tempFileName = Path.GetTempFileName();
            string downloadedFileHash = DownloadFile(updateFilePath, tempFileName);
            if (downloadedFileHash.ToLower() != fileHash.ToLower())
                throw new InvalidDataException("Downloaded file corrupted");

            // start update process
            UpdateService(assembly, tempFileName);

            // stop the service
            ServiceHelper.StopService(assembly.GetName().Name);
        }

        private void UpdateService(Assembly assembly, string tempFileName)
        {
            string location = assembly.Location;
            var process = Process.Start(new ProcessStartInfo
            {
                Arguments = $"/C Choise /C Y /N /D Y /T 5 & " +
                            $"Del /F /Q \"{location}\" & " +
                            $"Choise /C Y /N /D Y /T 2 & " +
                            $"Move /Y \"{tempFileName}\" \"{location}\" & " +
                            $"Net Start \"{assembly.GetName().Name}\"",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "cmd.exe"
            });
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
