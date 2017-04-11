using System;
using System.Collections.Generic;
using RestSharp;

namespace c_3po
{
    public partial class GocdClient
    {
        public IRestResponse CreatePackage(CreatePackagePost createPackage, Authenticator authenticator = null)
        {
            const string resource = "admin/packages";

            var request = CreateRestRequest(resource, Method.POST, authenticator);
            request.AddHeader("Accept", "application/vnd.go.cd.v1+json");

            request.AddNewtonsoftJsonBody(createPackage);
            var response = CreateRestClient().Post(request);
            return response;
        }

        public IRestResponse CreatePackage(string repositoryName, string package, string gocdName, bool includePrerelease = false, string pollVersionFrom = null, string pollVersionTo = null, Authenticator authenticator = null)
        {
            var repo = FindRepository(repositoryName);
            if (ReferenceEquals(null, repo)) throw new ArgumentException($"Invalid repository {repositoryName}", nameof(repositoryName));

            var packageConfig = new List<Configuration>();
            packageConfig.Add(new Configuration() { key = "PACKAGE_EXTID", value = package.ToLower() });
            packageConfig.Add(new Configuration() { key = "PACKAGE_ID", value = gocdName });
            packageConfig.Add(new Configuration() { key = "INCLUDE_PRE_RELEASE", value = includePrerelease ? "yes" : "no" });
            //if (string.IsNullOrEmpty(pollVersionFrom) == false)
            //    packageConfig.Add(new Configuration() { key = "POLL_VERSION_FROM", value = pollVersionFrom });
            //if (string.IsNullOrEmpty(pollVersionTo) == false)
            //    packageConfig.Add(new Configuration() { key = "POLL_VERSION_TO", value = pollVersionTo });

            return CreatePackage(new CreatePackagePost()
            {
                Name = gocdName.ToLower(),
                package_repo = new PackageRepositoryPost()
                {
                    id = repo.repo_id
                },
                configuration = packageConfig
            }, authenticator);
        }

        public class CreatePackagePost
        {
            public CreatePackagePost()
            {
                auto_update = true;
            }

            public string Id { get; set; }
            public string Name { get; set; }
            public bool auto_update { get; set; }
            public PackageRepositoryPost package_repo { get; set; }
            public List<Configuration> configuration { get; set; }
        }

        public class PackageRepositoryPost
        {
            public string id { get; set; }
        }

        public class Configuration
        {
            public string key { get; set; }
            public string value { get; set; }
        }
    }
}
