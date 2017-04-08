using System.Collections.Generic;
using RestSharp;
using thegit;

namespace c_3po
{
    public partial class GocdClient
    {
        public IRestResponse CreateBuildPipeline(C3poConfig config)
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
                            type = "git",
                            attributes = new GitMaterialAttributes() {
                                    url = config.GetGitRemote(),
                                    branch = config.Branch,
                                    name = "git"
                            }
                        }
                    },
                    parameters = new List<Parameter>() {
                        new Parameter() { name = "nuget_api_key", value = config.GetNugetApiKey() },
                        new Parameter() { name = "app_name", value = config.AppName },
                    }
                }
            };

            return CreatePipeline(createPipeline);
        }

        public IRestResponse CreateBuildMonoRepoPipeline(C3poConfig config)
        {
            Scm scm = GetScm(config.AppName);
            if (ReferenceEquals(null, scm))
                scm = CreateGitPathScm(config.AppName, config.GetGitRemote(), config.AppName);

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
                            type = "plugin",
                            attributes = new PackageMaterialAttributes() {
                                Ref = scm.Id
                            }
                        }
                    },
                    parameters = new List<Parameter>() {
                        new Parameter() { name = "nuget_api_key", value = config.GetNugetApiKey() },
                        new Parameter() { name = "app_name", value = config.AppName },
                    }
                }
            };

            return CreatePipeline(createPipeline);
        }
    }
}
