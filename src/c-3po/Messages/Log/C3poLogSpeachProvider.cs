using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace c_3po.Messages.Log
{
    public class C3poLogSpeachProvider : IC3poSpeachProvider
    {
        public void CreatingDeployPipeline(string applicationName, string pipelineName)
        {
            throw new NotImplementedException();
        }

        public void CreatingMonoRepoPipeline(string applicationName, string pipelineName)
        {
            throw new NotImplementedException();
        }

        public void CreatingRepoPipeline(string applicationName, string pipelineName)
        {
            throw new NotImplementedException();
        }

        public void FinishedExtractingConfigurations(long extractedCfgs)
        {
            throw new NotImplementedException();
        }

        public void GoodByeMaster()
        {
            throw new NotImplementedException();
        }

        public void HelloMaster()
        {
            throw new NotImplementedException();
        }

        public void R2d2Responded(HttpStatusCode r2d2response, string applicationName)
        {
            throw new NotImplementedException();
        }

        public void StartedCreating()
        {
            throw new NotImplementedException();
        }

        public void StartedExtractingConfigurations()
        {
            throw new NotImplementedException();
        }
    }
}
