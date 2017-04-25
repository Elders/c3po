using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c_3po
{
    public partial class GocdClient
    {
        public EnviromentResult GetEnvironment(string environmentName,Authenticator authenticator = null)
        {
            string resource = $"admin/environments/{environmentName}";

            var request = CreateRestRequest(resource, Method.GET, authenticator);
            request.AddHeader("Accept", "application/vnd.go.cd.v2+json");
            var response = CreateRestClient().Execute<EnviromentResult>(request);

            response.Data.ETag = response.Headers.FirstOrDefault(x => x.Name == "ETag").Value.ToString();

            if ((ReferenceEquals(response, null) == true) || (ReferenceEquals(response.Data, null) == true))
                return new EnviromentResult();

            return response.Data;
        }

        public class EnviromentResult
        {
            public string name { get; set; }

            public IEnumerable<Pipeline> pipelines { get; set; }

            public IEnumerable<AgentPost> agents { get; set; }

            public IEnumerable<EnviromentVariablePost> environment_variables { get; set; }

            public string ETag { get; set; }
        }
    }


}
