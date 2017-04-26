using RestSharp;

namespace c_3po
{
    public partial class GocdClient
    {
        public IRestResponse AddPipelineToEnvironment(string environment, string pipeline, Authenticator authenticator = null)
        {
            var currentEnviroment = GetEnvironment(environment);
            currentEnviroment.pipelines.Add(new PipelineUpdate() { Name = pipeline });
            return UpdateEnviroment(new UpdateEnvironmentPut()
            {
                Name = environment,
                Pipelines = currentEnviroment.pipelines,
                Agents = currentEnviroment.agents
            }, currentEnviroment.ETag);
        }

    }
}
