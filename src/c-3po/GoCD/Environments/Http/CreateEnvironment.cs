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
        public IRestResponse CreateEnvironment(CreateEnvironmentPost createEnvironment, Authenticator authenticator = null)
        {
            const string resource = "admin/environments";

            var request = CreateRestRequest(resource, Method.POST, authenticator);
            request.AddHeader("Accept", "application/vnd.go.cd.v2+json");

            request.AddNewtonsoftJsonBody(createEnvironment);
            var response = CreateRestClient().Post(request);

            return response;
        }
    }
}
