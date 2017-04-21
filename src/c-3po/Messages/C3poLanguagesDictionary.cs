using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace c_3po.Messages
{
    class C3poSpeachProgram : IC3poSpeachProvider
    {
        IList<C3poTalk> languages = new List<C3poTalk>();

        public void AddVoiceInterface(C3poVoiceInterface voiceInterface)
        {
            languages.Add(new C3poTalk(voiceInterface));
        }

        public void CreatingDeployPipeline(string applicationName, string pipelineName)
        {
            foreach (var language in languages)
            {
                language.Says.CreatingDeployPipeline(applicationName, pipelineName);
            }
        }

        public void CreatingMonoRepoPipeline(string applicationName, string pipelineName)
        {
            foreach (var language in languages)
            {
                language.Says.CreatingMonoRepoPipeline(applicationName, pipelineName);
            }
        }

        public void CreatingRepoPipeline(string applicationName, string pipelineName)
        {
            foreach (var language in languages)
            {
                language.Says.CreatingRepoPipeline(applicationName, pipelineName);
            }
        }

        public void FinishedExtractingConfigurations(long extractedCfgs)
        {
            foreach (var language in languages)
            {
                language.Says.FinishedExtractingConfigurations(extractedCfgs);
            }
        }

        public void GoodByeMaster()
        {
            foreach (var language in languages)
            {
                language.Says.GoodByeMaster();
            }
        }

        public void HelloMaster()
        {
            foreach (var language in languages)
            {
                language.Says.HelloMaster();
            }
        }

        public void R2d2Responded(HttpStatusCode r2d2response, string applicationName,string errorMessage)
        {
            foreach (var language in languages)
            {
                language.Says.R2d2Responded(r2d2response, applicationName, errorMessage);
            }
        }

        public void StartedCreating()
        {
            foreach (var language in languages)
            {
                language.Says.StartedCreating();
            }
        }

        public void StartedExtractingConfigurations()
        {
            foreach (var language in languages)
            {
                language.Says.StartedExtractingConfigurations();
            }
        }
    }
}
