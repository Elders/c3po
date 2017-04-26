using System.Collections.Generic;
using System.Net;

namespace c_3po.Messages
{
    class C3poSpeachProgram : ILanguageDictionary
    {
        IList<C3poTalk> languages = new List<C3poTalk>();
        bool hasStarted = false;
        bool isFirstStart = true;

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
            if (!isFirstStart)
                return;

            foreach (var language in languages)
            {
                language.Says.GoodByeMaster();
            }

            isFirstStart = false;
        }

        public void HelloMaster()
        {
            if (hasStarted)
                return;

            foreach (var language in languages)
            {
                language.Says.HelloMaster();
            }

            hasStarted = true;
        }

        public void R2d2Responded(HttpStatusCode r2d2response, string itemName, string applicationName, string errorMessage)
        {
            foreach (var language in languages)
            {
                language.Says.R2d2Responded(r2d2response, itemName, applicationName, errorMessage);
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
