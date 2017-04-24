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
        public IRestResponse UpdateEnviroment(UpdateEnvironmentPut updateEnvironment,string etag, Authenticator authenticator = null)
        {
            string resource = $"admin/environments/{updateEnvironment.name}";

            var request = CreateRestRequest(resource, Method.PUT, authenticator);
            request.AddHeader("Accept", "application/vnd.go.cd.v2+json");
            request.AddHeader("If-Match", etag);

            request.AddNewtonsoftJsonBody(updateEnvironment);
            var response = CreateRestClient().Put(request);

            return response;
        }
    }
}
