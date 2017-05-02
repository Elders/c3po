using System.Collections.Generic;
using RestSharp;

namespace c_3po
{
    public partial class GocdClient
    {
        public Scm GetScm(string name, Authenticator authenticator = null)
        {
            const string resource = "admin/scms/";

            var request = CreateRestRequest(resource + name.ToLower(), Method.GET, authenticator);
            request.AddHeader("Accept", "application/vnd.go.cd.v1+json");

            var response = CreateRestClient().Get<Scm>(request);
            if (ReferenceEquals(response, null) || ReferenceEquals(response.Data, null) || ReferenceEquals(response.Data.Id, null))
                return null;

            return response.Data;
        }
    }

    public partial class GocdClient
    {
        public IEnumerable<PackageRepository> GetRepositories(Authenticator authenticator = null)
        {
            const string resource = "admin/repositories";

            var request = CreateRestRequest(resource, Method.GET, authenticator);
            request.AddHeader("Accept", "application/vnd.go.cd.v1+json");

            var response = CreateRestClient().Get<RepositoriesResult>(request);
            if ((ReferenceEquals(response, null) == true) || (ReferenceEquals(response.Data, null) == true))
                return new List<PackageRepository>();

            return response.Data._embedded.package_repositories;
        }

        public class RepositoriesResult
        {
            public Embedded<PackageRepository> _embedded { get; set; }
        }

        public class Package
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }

        public class Embedded<T>
        {
            public List<T> package_repositories { get; set; }

            public List<T> packages { get; set; }
        }

        public class PackageRepository
        {
            public string repo_id { get; set; }

            public string name { get; set; }

            public Embedded<Package> _embedded { get; set; }
        }
    }
}
