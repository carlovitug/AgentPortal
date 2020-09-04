using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ABMS_Backend.Utils
{
    public class SFTP
    {
        // Enter your host name or IP here
        private static string host = "192.168.70.17";
        // Enter your sftp username here test use: dev
        private static string username = "dev";
        // Enter your sftp password here
        private static string password = "test1234";
        private static int port = 22;
        public void Save(string path)
        {
            string fileName = Path.GetFileName(path);
            var connectionInfo = new ConnectionInfo(host, port, username, new PasswordAuthenticationMethod(username, password));
            // Upload File
            using (var sftp = new SftpClient(connectionInfo))
            {

                sftp.Connect();
                ///home/dev/SettlementReports/GeneratedReports
                //sftp.ChangeDirectory("/home/Allbank/sftp/REPORTS");
                sftp.ChangeDirectory("/home/dev/SettlementReports/GeneratedReports");
                // sftp.ChangeDirectory("/home/Allbank/sftp/REPORTS");
                using (var uplfileStream = System.IO.File.OpenRead(path))
                {
                    sftp.UploadFile(uplfileStream, fileName, true);
                }
                sftp.Disconnect();
            }
        }

    }
}