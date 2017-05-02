using Elders.Pandora;
using Elders.Pandora.Box;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace thegit
{
    public class App
    {
        public class Settings
        {
            public Settings(string softwareName, string repositoryPath = null)
            {
                Software = softwareName;
                RepositoryPath = repositoryPath;
            }

            public string Software { get; private set; }
            public string RepositoryPath { get; private set; }
        }

        readonly Settings settings;
        public App(Settings settings)
        {
            this.settings = settings;
        }

        public IEnumerable<C3poConfig> GetC3poConfigurations()
        {
            string basePath = settings.RepositoryPath ?? "";
            string srcPath = Path.Combine(basePath, settings.Software, "src");
            var appDirectories = Directory.GetDirectories(srcPath, "*.c3po", SearchOption.TopDirectoryOnly);
            foreach (var appDir in appDirectories)
            {
                if (Directory.Exists(appDir))
                {
                    var files = Directory.GetFiles(appDir, "c3po*", SearchOption.AllDirectories);
                    foreach (var c3po in files)
                    {
                        var cfgRaw = File.ReadAllText(c3po);
                        var jar = JsonConvert.DeserializeObject<Jar>(cfgRaw);
                        var box = Box.Mistranslate(jar);
                        PandoraBoxOpener opener = new PandoraBoxOpener(box);

                        foreach (var environment in box.Clusters)
                        {
                            var configurations = opener.Open(new PandoraOptions(environment.Name, Machine.NotSpecified));
                            var cfg = new C3poConfig(configurations.AsDictionary());
                            cfg.Environment = environment.Name;
                            cfg.HostName = box.Name;
                            cfg.SoftwareName = settings.Software;
                            yield return cfg;
                        }
                    }
                }
            }
        }

    }
}
