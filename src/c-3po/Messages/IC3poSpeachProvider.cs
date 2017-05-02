using System.Net;

namespace c_3po.Messages
{
    public interface IC3poSpeachProvider
    {
        void HelloMaster();
        void GoodByeMaster();
        void StartedExtractingConfigurations();
        void FinishedExtractingConfigurations(long extractedCfgs);
        void StartedCreating();
        void CreatingMonoRepoPipeline(string applicationName, string pipelineName);
        void CreatingRepoPipeline(string applicationName, string pipelineName);
        void CreatingDeployPipeline(string applicationName, string pipelineName);
        void R2d2Responded(HttpStatusCode r2d2response, string itemValue, string applicationName, string errorMessage);
        void ThereIsError(string message);
    }
}
