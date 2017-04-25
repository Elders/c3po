using c_3po.Messages;
using System;
using System.IO;
using System.Linq;
using System.Text;
using thegit;

namespace c_3po.cli
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);

            Uri server = new Uri(args[0]);
            string username = args[1];
            string password = args[2];
            string appName = args[3];
            string repositoryPath = args[4];

            #region GOCD

            var gocdOptions = new GocdClient.Options(server);

            var auth = new Authenticator(Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}")));
            var gocd = new GocdClient(gocdOptions, auth);
            #endregion

            if (appName.Equals("mono-repo", StringComparison.OrdinalIgnoreCase))
            {
                string workingDir = repositoryPath ?? ".";
                var bcs = Directory.GetDirectories(workingDir).Where(dir => dir.EndsWith(".git") == false).Select(x => new DirectoryInfo(x));
                foreach (var bc in bcs)
                {
                    var appSettings = new App.Settings(bc.Name, workingDir);
                    var app = new App(appSettings);
                    var elders = new EldersCI(gocd, app);
                    elders.Addc3poVoiceInterface(C3poVoiceInterface.Console);
                    elders.Magic();
                }
            }
            else
            {
                var appSettings = new App.Settings(appName, repositoryPath);
                var app = new App(appSettings);
                var elders = new EldersCI(gocd, app);
                elders.Addc3poVoiceInterface(C3poVoiceInterface.Console);
                elders.Magic();
            }
        }
    }
}
