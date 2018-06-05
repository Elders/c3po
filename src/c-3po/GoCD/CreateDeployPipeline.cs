using System.Collections.Generic;
using System.Linq;
using RestSharp;
using thegit;

namespace c_3po
{
    public partial class GocdClient
    {
        public IEnumerable<IRestResponse> CreateDeployPipeline(C3poConfig config)
        {
            var cfgPackage = FindOrCreatePackage(config.GetCfgNugetName(), config.GetCfgNugetRepo(), config.IncludeCfgPrereleases(), config);
            var hostPackage = FindOrCreatePackage(config.GetHostNugetName(), config.GetAppNugetRepo(), config.IncludeAppPrereleases(), config);

            var createPipeline = new CreatePipelinePost()
            {
                Group = config.GetPipelineGroup(),
                Pipeline = new Pipeline()
                {
                    name = config.GetPipelineName(),
                    template = config.GetPipelineTemplate(),
                    materials = new List<Material>()
                    {
                        new Material() {
                            type = "package",
                            attributes = new PackageMaterialAttributes() { Ref = cfgPackage.Id }
                        },
                        new Material() {
                            type = "package",
                            attributes = new PackageMaterialAttributes() { Ref = hostPackage.Id }
                        }
                    },
                    parameters = new List<Parameter>() {
                        new Parameter() { name = "tenant", value = config.GetClientId() },
                        new Parameter() { name = "application", value = config.GetApplication() },
                        new Parameter() { name = "cfg_nuget", value = config.GetCfgNugetName() },
                        new Parameter() { name = "host_nuget", value = config.GetHostNugetName() },
                        new Parameter() { name = "host_name", value = config.HostName },
                        new Parameter() { name = "host_type", value = config.GetHostType() },
                        new Parameter() { name = "host_dpl_par", value = config.GetHostDeplPar() },
                        new Parameter() { name = "cfg_nuget_version_envvar", value = $"GO_PACKAGE_{cfgPackage.GetFullyQualifiedGoCdName()}_VERSION".AsGoCdEnvironmentString() },
                        new Parameter() { name = "host_nuget_version_envvar", value = $"GO_PACKAGE_{hostPackage.GetFullyQualifiedGoCdName()}_VERSION".AsGoCdEnvironmentString() },
                    }
                }
            };

            yield return CreatePipeline(createPipeline);
            yield return AddPipelineToEnvironment(config.Environment, config.GetPipelineName());
        }
    }

    public static class GoCdEnvironmentVariablesExtensions
    {
        const char Separator = '_';

        public static string AsGoCdEnvironmentString(this string value)
        {
            var chars = value.Select(x => char.IsLetterOrDigit(x) || x == Separator ? x : Separator);
            return string.Concat(chars).ToUpper();
        }
    }
}
