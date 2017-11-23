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


            C3poSpeachProgram c3poSpeakProgram = new C3poSpeachProgram();
            c3poSpeakProgram.AddVoiceInterface(C3poVoiceInterface.Console);

            #region GOCD
            var gocdOptions = new GocdClient.Options(server);
            var auth = new Authenticator(Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}")));
            var gocd = new GocdClient(gocdOptions, c3poSpeakProgram, auth);
            #endregion

            var elders = new EldersCI(gocd, c3poSpeakProgram);

            if (appName.Equals("mono-repo", StringComparison.OrdinalIgnoreCase))
            {
                string workingDir = repositoryPath ?? ".";
                var bcs = Directory.GetDirectories(workingDir).Where(dir => dir.EndsWith(".git") == false).Select(x => new DirectoryInfo(x));

                foreach (var bc in bcs)
                {
                    var appSettings = new App.Settings(bc.Name, workingDir);
                    var app = new App(appSettings);
                    elders.SetApp(app);
                    elders.Magic();
                }
            }
            else
            {
                var appSettings = new App.Settings(appName, repositoryPath);
                var app = new App(appSettings);
                elders.SetApp(app);
                elders.Magic();
            }

            elders.Dispose();
        }
    }
}
