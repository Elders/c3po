using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace c_3po
{
    public partial class GocdClient
    {
        public EnviromentResult GetEnvironment(string environmentName, Authenticator authenticator = null)
        {
            string resource = $"admin/environments/{environmentName}";

            var request = CreateRestRequest(resource, Method.GET, authenticator);
            request.AddHeader("Accept", "application/vnd.go.cd.v2+json");
            var response = CreateRestClient().Execute<EnviromentResult>(request);

            var etag = response.Headers.FirstOrDefault(x => x.Name == "ETag")?.Value;
            response.Data.ETag = ReferenceEquals(null, etag) ? null : etag.ToString();

            if ((ReferenceEquals(response, null) == true) || (ReferenceEquals(response.Data, null) == true))
                return new EnviromentResult();

            return response.Data;
        }

        public class EnviromentResult
        {
            public string name { get; set; }

            public IList<PipelineUpdate> pipelines { get; set; }

            public IList<AgentPost> agents { get; set; }

            public IEnumerable<EnviromentVariablePost> environment_variables { get; set; }

            public string ETag { get; set; }
        }
    }


}
