using c_3po.Messages;
using RestSharp;
using System;
using thegit;

namespace c_3po
{
    public class EldersCI : IDisposable
    {
        const string MonoRepo = "mono-repo";
        const string Repo = "repo";
        const string Deploy = "deploy";

        readonly GocdClient gocd;
        App app;

        private C3poSpeachProgram c3poSpeakProgram = new C3poSpeachProgram();

        public EldersCI(GocdClient gocd)
        {
            this.gocd = gocd;

            c3poSpeakProgram.AddVoiceInterface(C3poVoiceInterface.LibLog);

        }

        public EldersCI(GocdClient gocd, App app)
        {
            this.app = app;
            this.gocd = gocd;

            c3poSpeakProgram.AddVoiceInterface(C3poVoiceInterface.LibLog);
        }

        public void Magic()
        {
            c3poSpeakProgram.HelloMaster();

            //c3po.Says.StartedExtractingConfigurations();
            var cfgs = app.GetC3poConfigurations();

            //c3po.Says.FinishedExtractingConfigurations(((ICollection<C3poConfig>)cfgs).Count);

            c3poSpeakProgram.StartedCreating();

            foreach (var cfg in cfgs)
            {
                IRestResponse r2d2Response = null;

                var enviromentResponse = gocd.CreateEnvironmentIfDoesntExists(cfg.Environment);

                if (!ReferenceEquals(null, enviromentResponse))
                {
                    c3poSpeakProgram.R2d2Responded(enviromentResponse.StatusCode, cfg.Environment, cfg.GetApplication(), enviromentResponse.Content);
                }

                if (cfg.GetC3poType().Equals(MonoRepo, System.StringComparison.OrdinalIgnoreCase))
                {
                    c3poSpeakProgram.CreatingMonoRepoPipeline(cfg.GetApplication(), cfg.GetPipelineName());
                    r2d2Response = gocd.CreateBuildMonoRepoPipeline(cfg);
                }
                else if (cfg.GetC3poType().Equals(Repo, System.StringComparison.OrdinalIgnoreCase))
                {
                    c3poSpeakProgram.CreatingRepoPipeline(cfg.GetApplication(), cfg.GetPipelineName());
                    r2d2Response = gocd.CreateBuildAllPipeline(cfg);
                }
                else if (cfg.GetC3poType().Equals(Deploy, System.StringComparison.OrdinalIgnoreCase))
                {
                    c3poSpeakProgram.CreatingDeployPipeline(cfg.GetApplication(), cfg.GetPipelineName());
                    r2d2Response = gocd.CreateDeployPipeline(cfg);
                }

                if (!ReferenceEquals(null, r2d2Response))
                    c3poSpeakProgram.R2d2Responded(r2d2Response.StatusCode, cfg.GetPipelineName(), cfg.GetApplication(), r2d2Response.Content);

                r2d2Response = gocd.AddPipelineToEnvironment(cfg.Environment, cfg.GetPipelineName());
                //c3poSpeakProgram.R2d2Responded(r2d2Response.StatusCode, cfg.GetPipelineName(), cfg.GetApplication(), r2d2Response.ErrorMessage);
            }


        }

        public void Addc3poVoiceInterface(C3poVoiceInterface whatInterface)
        {
            c3poSpeakProgram.AddVoiceInterface(whatInterface);
        }

        public void SetApp(App app)
        {
            this.app = app;
        }

        public void Dispose()
        {
            app = null;
            c3poSpeakProgram.GoodByeMaster();
        }

        //public void Cleanup(string branch)
        //{
        //    var cfgs = git.GetBranchConfigurationsForC3po(branch);

        //    foreach (var cfg in cfgs)
        //    {
        //        gocd.DeletePipeline(cfg.GetPipelineName());
        //    }
        //}
    }
}
