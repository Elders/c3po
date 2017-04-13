using System.Collections.Generic;
using RestSharp;
using thegit;

namespace c_3po
{
    public partial class GocdClient
    {
        public IRestResponse CreateDeployPipeline(C3poConfig config)
        {
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
                            attributes = new PackageMaterialAttributes() { Ref = FindOrCreatePackage(config.GetCfgNugetName(), config.GetCfgNugetRepo(), config.IncludeCfgPrereleases(), config).Id }
                        },
                        new Material() {
                            type = "package",
                            attributes = new PackageMaterialAttributes() { Ref = FindOrCreatePackage(config.GetHostNugetName(), config.GetAppNugetRepo(), config.IncludeAppPrereleases(), config).Id }
                        }
                    },
                    parameters = new List<Parameter>() {
                        new Parameter() { name = "tenant", value = config.GetClientId() },
                        new Parameter() { name = "application", value = config.GetApplication() },
                        new Parameter() { name = "cfg_nuget", value = config.GetCfgNugetName() },
                        new Parameter() { name = "host_nuget", value = config.GetHostNugetName() },
                        new Parameter() { name = "host_name", value = config.HostName },
                        new Parameter() { name = "host_type", value = config.GetHostType() },
                        new Parameter() { name = "host_dpl_par", value = config.GetHostDeplPar() }
                    }
                }
            };

            return CreatePipeline(createPipeline);
        }
    }
}
