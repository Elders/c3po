using System.Collections.Generic;
using System.IO;
using Elders.Pandora;
using Elders.Pandora.Box;
using Newtonsoft.Json;

namespace thegit
{
    public class App
    {
        public class Settings
        {
            public Settings(string name, string repositoryPath = null)
            {
                Name = name;
                RepositoryPath = repositoryPath;
            }

            public string Name { get; private set; }
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
            string appDir = Path.Combine(basePath, settings.Name, "src", settings.Name + ".c3po");
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
                        cfg.AppName = box.Name;
                        yield return cfg;
                    }
                }
            }
        }
    }
}
