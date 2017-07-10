using RestSharp;

namespace c_3po
{
    public partial class GocdClient
    {
        public IRestResponse UpdateEnviroment(UpdateEnvironmentPut updateEnvironment, string etag, Authenticator authenticator = null)
        {
            string resource = $"admin/environments/{updateEnvironment.Name}";

            var request = CreateRestRequest(resource, Method.PUT, authenticator);
            request.AddHeader("Accept", "application/vnd.go.cd.v2+json");
            request.AddHeader("If-Match", etag);

            request.AddNewtonsoftJsonBody(updateEnvironment);
            var response = CreateRestClient().Put(request);

            if ((ReferenceEquals(response, null) == true))
            {
                var error = $"Error occurred while updating envierment {updateEnvironment} in GOCD.";
                c3poSpeakProgram.ThereIsError(error);
                throw new System.Exception(error);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var error = $"Unauthorized exception while updating envierment {updateEnvironment} in GOCD.";
                c3poSpeakProgram.ThereIsError(error);
                throw new System.Exception(error);
            }

            return response;
        }
    }
}
