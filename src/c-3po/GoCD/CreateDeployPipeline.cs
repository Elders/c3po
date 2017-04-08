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
                            attributes = new PackageMaterialAttributes() { Ref = FindOrCreatePackage(config.GetAppNugetName(), config.GetAppNugetRepo(), config.IncludeAppPrereleases(), config).Id }
                        }
                    },
                    parameters = new List<Parameter>() {
                        new Parameter() { name = "client_id", value = config.GetClientId() },
                        new Parameter() { name = "cfg_nuget", value = config.GetCfgNugetName() },
                        new Parameter() { name = "app_nuget", value = config.GetAppNugetName() },
                        new Parameter() { name = "app_name", value = config.AppName },
                        new Parameter() { name = "app_type", value = config.GetAppType() },
                        new Parameter() { name = "app_dpl_par", value = config.GetAppDeplPar() }
                    }
                }
            };

            return CreatePipeline(createPipeline);
        }
    }
}
