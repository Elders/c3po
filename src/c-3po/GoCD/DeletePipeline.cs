using RestSharp;

namespace c_3po
{
    public partial class GocdClient
    {
        public IRestResponse DeletePipeline(string deletePipeline, Authenticator authenticator = null)
        {
            string resource = $"admin/pipelines/{deletePipeline}";

            var request = CreateRestRequest(resource, Method.DELETE, authenticator);
            request.AddHeader("Accept", "application/vnd.go.cd.v3+json");

            var response = CreateRestClient().Delete(request);
            return response;
        }
    }
}
