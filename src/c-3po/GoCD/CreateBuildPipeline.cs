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
                        new Parameter() { name = "host_name", value = config.HostName },
                    }
                }
            };

            return CreatePipeline(createPipeline);
        }

        public IRestResponse CreateBuildMonoRepoPipeline(C3poConfig config)
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
                                    name = "git",
                                    destination = "local",
                                    filter = new Filter(){ ignore = new List<string>{ $"{config.GetApplication()}/**/*" } },
                                    inverted_filter = "true"
                            }
                        }
                    },
                    parameters = new List<Parameter>() {
                        new Parameter() { name = "nuget_api_key", value = config.GetNugetApiKey() },
                        new Parameter() { name = "application", value = config.GetApplication() },
                    }
                }
            };

            return CreatePipeline(createPipeline);
        }

        public IRestResponse CreateBuildAllPipeline(C3poConfig config)
        {
            Scm scm = GetScm(config.HostName);
            if (ReferenceEquals(null, scm))
                scm = CreateGitFeatureBranchScm(config.HostName, config.GetGitRemote());

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
                                Ref = scm.Id,
                                Destination = "local"
                            }
                        }
                    },
                    parameters = new List<Parameter>() {
                        new Parameter() { name = "nuget_api_key", value = config.GetNugetApiKey() },
                        new Parameter() { name = "host_name", value = config.HostName },
                    }
                }
            };

            return CreatePipeline(createPipeline);
        }
    }


    public class Filter
    {
        public List<string> ignore { get; set; }
    }
}
