using RestSharp;

namespace c_3po
{
    public partial class GocdClient
    {
        public PipelineStatusResult GetPipelineStatus(Authenticator authenticator = null)
        {
            const string resource = "pipelines/configuration/status";

            var request = CreateRestRequest(resource, Method.GET, authenticator);
            var response = CreateRestClient().Execute<PipelineStatusResult>(request);
            if ((ReferenceEquals(response, null) == true) || (ReferenceEquals(response.Data, null) == true))
                return new PipelineStatusResult();

            return response.Data;
        }

        public class PipelineStatusResult
        {
            public bool Locked { get; set; }

            public bool Paused { get; set; }

            public string PausedCause { get; set; }
        }
    }
}
