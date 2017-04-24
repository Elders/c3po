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
        public IRestResponse AddPipelineToEnvironment(string environment,string pipeline, Authenticator authenticator = null)
        {
            var enviroments = GetEnviroments();
            var currentEnvironment = enviroments._embedded.environments.FirstOrDefault(x => x.name == environment);

            return UpdateEnviroment(new UpdateEnvironmentPut()
            {   
                name = environment,
                pipelines = new List<PipelineUpdate> { new PipelineUpdate() { name = pipeline } }
            }, enviroments.ETag);
        }
        
    }
}
