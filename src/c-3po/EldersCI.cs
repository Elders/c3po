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
                else if (cfg.GetC3poType().Equals(Repo, System.StringComparison.OrdinalIgnoreCase))
                {
                    gocd.CreateBuildAllPipeline(cfg);
                }
                else if (cfg.GetC3poType().Equals(Deploy, System.StringComparison.OrdinalIgnoreCase))
                {
                    gocd.CreateDeployPipeline(cfg);
                }
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
