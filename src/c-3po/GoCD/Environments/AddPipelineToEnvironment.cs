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
            var currentEnviroment = GetEnvironment(environment);

            return UpdateEnviroment(new UpdateEnvironmentPut()
            {   
                Name = environment,
                Pipelines = new List<PipelineUpdate> { new PipelineUpdate() { Name = pipeline } }
            }, currentEnviroment.ETag);
        }
        
    }
}
