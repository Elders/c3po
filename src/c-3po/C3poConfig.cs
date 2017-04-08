using System.Collections.Generic;
using Elders.Pandora.Box;

namespace thegit
{
    public class C3poConfig
    {
        const string ClientIdKey = "client_id";
        const string PipelineNameKey = "pipeline_name";
        const string PipelineNamePrefixKey = "pipeline_name_prefix";
        const string PipelineNameSufixKey = "pipeline_name_sufix";
        const string PipelineTemplateKey = "pipeline_template";
        const string PipelineGroupKey = "pipeline_group";
        const string CfgNugetRepoKey = "cfg_nuget_repo";
        const string CfgNugetKey = "cfg_nuget";
        const string CfgNugetIncludePrereleaseKey = "cfg_nuget_prerelease";
        const string AppNugetRepoKey = "app_nuget_repo";
        const string AppNugetKey = "app_nuget";
        const string AppNugetIncludePrereleaseKey = "app_nuget_prerelease";
        const string AppTypeKey = "app_type";
        const string AppDeploymentParKey = "app_dpl_par";
        const string NugetApiKeyKey = "nuget_api_key";
        const string C3poTypeKey = "c3po_type";
        const string GitBranchKey = "git_branch";
        const string GitRemoteKey = "git_remote";


        readonly IDictionary<string, object> configuration;

        public C3poConfig(IDictionary<string, object> configuration)
        {
            this.configuration = configuration;
        }

        public string GetGitRemote() { return GetSetting(GitRemoteKey); }

        public string GetC3poType() { return GetSetting(C3poTypeKey); }

        public string GetCfgNugetRepo() { return GetSetting(CfgNugetRepoKey); }

        public string GetCfgNugetName() { return GetSetting(CfgNugetKey); }

        public string GetAppNugetRepo() { return GetSetting(AppNugetRepoKey); }

        public string GetAppNugetName() { return GetSetting(AppNugetKey); }

        public string GetClientId() { return GetSetting(ClientIdKey); }

        public string GetAppType() { return GetSetting(AppTypeKey); }

        public string GetNugetApiKey() { return GetSetting(NugetApiKeyKey); }

        public string GetAppDeplPar() { return GetSetting(AppDeploymentParKey); }

        public bool IncludeAppPrereleases() { return "yes".Equals(GetSetting(AppNugetIncludePrereleaseKey), System.StringComparison.OrdinalIgnoreCase); }

        public bool IncludeCfgPrereleases() { return "yes".Equals(GetSetting(CfgNugetIncludePrereleaseKey), System.StringComparison.OrdinalIgnoreCase); }

        public string GetPipelineName()
        {
            return $"{GetSetting(PipelineNamePrefixKey)}_{GetSetting(PipelineNameKey)}_{Branch.ToLower()}_{GetSetting(PipelineNameSufixKey)}".Trim('_');
        }

        public string GetPipelineTemplate() { return GetSetting(PipelineTemplateKey); }

        public string GetPipelineGroup() { return GetSetting(PipelineGroupKey); }

        public string Branch { get { return GetSetting(GitBranchKey); } }
        public string Environment { get; set; }
        public string AppName { get; set; }

        string GetSetting(string key)
        {
            string theKey = NameBuilder.GetSettingName(AppName, Environment, Machine.NotSpecified, key.ToLower()).ToLower();
            return (string)configuration[theKey];
        }
    }
}
