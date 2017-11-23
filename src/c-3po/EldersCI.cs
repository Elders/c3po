using System;
using System.Collections.Generic;
using System.Linq;
using c_3po.Messages;
using RestSharp;
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
        C3poSpeachProgram c3poSpeakProgram;

        public EldersCI(GocdClient gocd, C3poSpeachProgram c3poSpeakProgram)
        {
            this.gocd = gocd;
            this.c3poSpeakProgram = c3poSpeakProgram;
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

            var cfgs = app.GetC3poConfigurations();

            c3poSpeakProgram.StartedCreating();

            foreach (var cfg in cfgs)
            {
                IEnumerable<IRestResponse> r2d2Responses = null;

                var enviromentResponse = gocd.CreateEnvironmentIfDoesntExists(cfg.Environment);

                try
                {
                    if (!ReferenceEquals(null, enviromentResponse))
                    {
                        c3poSpeakProgram.R2d2Responded(enviromentResponse.StatusCode, cfg.Environment, cfg.GetApplication(), SimpleJson.DeserializeObject<dynamic>(enviromentResponse.Content).Message);
                    }

                    if (cfg.GetC3poType().Equals(MonoRepo, System.StringComparison.OrdinalIgnoreCase))
                    {
                        c3poSpeakProgram.CreatingMonoRepoPipeline(cfg.GetApplication(), cfg.GetPipelineName());
                        r2d2Responses = gocd.CreateBuildMonoRepoPipeline(cfg);
                    }
                    else if (cfg.GetC3poType().Equals(Repo, System.StringComparison.OrdinalIgnoreCase))
                    {
                        c3poSpeakProgram.CreatingRepoPipeline(cfg.GetApplication(), cfg.GetPipelineName());
                        r2d2Responses = gocd.CreateBuildPipelines(cfg).ToList();
                    }
                    else if (cfg.GetC3poType().Equals(Deploy, System.StringComparison.OrdinalIgnoreCase))
                    {
                        c3poSpeakProgram.CreatingDeployPipeline(cfg.GetApplication(), cfg.GetPipelineName());
                        r2d2Responses = gocd.CreateDeployPipeline(cfg);
                    }

                    if (ReferenceEquals(null, r2d2Responses) == false)
                    {
                        foreach (var r2d2Response in r2d2Responses)
                        {
                            c3poSpeakProgram.R2d2Responded(r2d2Response.StatusCode, cfg.GetPipelineName(), cfg.GetApplication(), SimpleJson.DeserializeObject<dynamic>(r2d2Response.Content).message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    c3poSpeakProgram.ThereIsError(ex.Message);
                }

            }


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
