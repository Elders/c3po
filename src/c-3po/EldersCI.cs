using thegit;

namespace c_3po
{
    public class EldersCI
    {
        const string MonoRepo = "mono-repo";

        readonly GocdClient gocd;
        readonly App app;

        public EldersCI(GocdClient gocd, App app)
        {
            this.app = app;
            this.gocd = gocd;
        }

        public void Magic()
        {
            var cfgs = app.GetC3poConfigurations();

            foreach (var cfg in cfgs)
            {
                if (cfg.GetC3poType().Equals(MonoRepo, System.StringComparison.OrdinalIgnoreCase))
                {
                    gocd.CreateBuildMonoRepoPipeline(cfg);
                }


                //if (cfg.Environment.Equals("build", System.StringComparison.OrdinalIgnoreCase))
                //    gocd.CreateBuildPipeline(cfg);
                //else
                //    gocd.CreateDeployPipeline(cfg);
            }
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
