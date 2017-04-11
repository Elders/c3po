using System.Collections.Generic;
using RestSharp;

namespace c_3po
{
    public partial class GocdClient
    {
        public Scm CreateScm(CreateScmPost post, Authenticator authenticator = null)
        {
            const string resource = "admin/scms";

            var request = CreateRestRequest(resource, Method.POST, authenticator);
            request.AddHeader("Accept", "application/vnd.go.cd.v1+json");

            request.AddNewtonsoftJsonBody(post);
            var response = CreateRestClient().Post<Scm>(request);
            return response.Data;
        }

        public Scm CreateGitPathScm(string name, string repositoryUrl, string path, string branch = "master")
        {
            var configuration = new List<Configuration>();
            configuration.Add(new Configuration() { key = "url", value = repositoryUrl.ToLower() });
            configuration.Add(new Configuration() { key = "path", value = path.ToLower() });
            configuration.Add(new Configuration() { key = "branch", value = branch.ToLower() });

            return CreateScm(new CreateScmPost()
            {
                Name = name.ToLower(),
                plugin_metadata = new PluginMetaDataPost()
                {
                    id = "GitPathMaterial",
                    version = "1"
                },
                configuration = configuration
            }, authenticator);
        }

        public Scm CreateGitFeatureBranchScm(string name, string repositoryUrl, string branch = "master")
        {
            var configuration = new List<Configuration>();
            configuration.Add(new Configuration() { key = "url", value = repositoryUrl.ToLower() });
            configuration.Add(new Configuration() { key = "defaultBranch", value = branch.ToLower() });

            return CreateScm(new CreateScmPost()
            {
                Name = name.ToLower(),
                plugin_metadata = new PluginMetaDataPost()
                {
                    id = "git.fb",
                    version = "1"
                },
                configuration = configuration
            }, authenticator);
        }

        public class CreateScmPost
        {
            public CreateScmPost()
            {
                auto_update = true;
            }

            public string Id { get; set; }
            public string Name { get; set; }
            public bool auto_update { get; set; }
            public PluginMetaDataPost plugin_metadata { get; set; }
            public List<Configuration> configuration { get; set; }
        }

        public class PluginMetaDataPost
        {
            public string id { get; set; }
            public string version { get; set; }
        }

        public class Scm
        {
            public string Id { get; set; }

            public string Name { get; set; }
        }
    }
}
