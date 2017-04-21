using c_3po.Messages;
using RestSharp;
using System.Collections.Generic;
using thegit;

namespace c_3po
{
    public class EldersCI
    {
        const string MonoRepo = "mono-repo";
        const string Repo = "repo";
        const string Deploy = "deploy";

        readonly GocdClient gocd;
        readonly App app;

        private C3poSpeachProgram c3poSpeakProgram = new C3poSpeachProgram();

        public EldersCI(GocdClient gocd, App app )
        {
            this.app = app;
            this.gocd = gocd;
        }

        public void Magic()
        {
            //c3po.Says.StartedExtractingConfigurations();
            var cfgs = app.GetC3poConfigurations();

            //c3po.Says.FinishedExtractingConfigurations(((ICollection<C3poConfig>)cfgs).Count);

            c3poSpeakProgram.StartedCreating();

            foreach (var cfg in cfgs)
            {
                IRestResponse r2d2Response;

                if (cfg.GetC3poType().Equals(MonoRepo, System.StringComparison.OrdinalIgnoreCase))
                {
                    c3poSpeakProgram.CreatingMonoRepoPipeline(cfg.GetApplication(), cfg.GetPipelineName());
                    r2d2Response = gocd.CreateBuildMonoRepoPipeline(cfg);
                    c3poSpeakProgram.R2d2Responded(r2d2Response.StatusCode,cfg.GetApplication(), r2d2Response.ErrorMessage);
                }
                else if (cfg.GetC3poType().Equals(Repo, System.StringComparison.OrdinalIgnoreCase))
                {
                    c3poSpeakProgram.CreatingRepoPipeline(cfg.GetApplication(), cfg.GetPipelineName());
                    r2d2Response = gocd.CreateBuildAllPipeline(cfg);
                    c3poSpeakProgram.R2d2Responded(r2d2Response.StatusCode, cfg.GetApplication(), r2d2Response.ErrorMessage);
                }
                else if (cfg.GetC3poType().Equals(Deploy, System.StringComparison.OrdinalIgnoreCase))
                {
                    c3poSpeakProgram.CreatingDeployPipeline(cfg.GetApplication(), cfg.GetPipelineName());
                    r2d2Response = gocd.CreateDeployPipeline(cfg);
                    c3poSpeakProgram.R2d2Responded(r2d2Response.StatusCode, cfg.GetApplication(), r2d2Response.ErrorMessage);
                }
            }

            c3poSpeakProgram.GoodByeMaster();
        }

        public void Addc3poVoiceInterface(C3poVoiceInterface whatInterface)
        {
            c3poSpeakProgram.AddVoiceInterface(whatInterface);
            c3poSpeakProgram.HelloMaster();
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
