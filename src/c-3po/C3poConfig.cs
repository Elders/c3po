using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using Elders.Pandora.Box;

namespace thegit
{
    public class C3poConfig
    {
        const string ClientIdKey = "tenant";
        const string ApplicationKey = "application";
        const string PipelineNameKey = "pipeline_name";
        const string PipelineNamePrefixKey = "pipeline_name_prefix";
        const string PipelineNameSufixKey = "pipeline_name_sufix";
        const string PipelineTemplateKey = "pipeline_template";
        const string PipelineGroupKey = "pipeline_group";
        const string CfgNugetRepoKey = "cfg_nuget_repo";
        const string CfgNugetKey = "cfg_nuget";
        const string CfgNugetIncludePrereleaseKey = "cfg_nuget_prerelease";
        const string AppNugetRepoKey = "host_nuget_repo";
        const string AppNugetKey = "host_nuget";
        const string AppNugetIncludePrereleaseKey = "host_nuget_prerelease";
        const string AppTypeKey = "host_type";
        const string AppDeploymentParKey = "host_dpl_par";
        const string NugetApiKeyKey = "nuget_api_key";
        const string C3poTypeKey = "c3po_type";
        const string GitBranchKey = "git_branch";
        const string GitRemoteKey = "git_remote";
        const string MaterialFilter = "material_filter";
        const string MaterialFilterIsInverted = "material_filter_is_inverted";

        readonly IDictionary<string, object> configuration;

        public C3poConfig(IDictionary<string, object> configuration)
        {
            this.configuration = configuration;
        }

        public string GetApplication() { return GetSetting(ApplicationKey); }

        public string GetGitRemote() { return GetSetting(GitRemoteKey); }

        public string GetC3poType() { return GetSetting(C3poTypeKey); }

        public string GetCfgNugetRepo() { return GetSetting(CfgNugetRepoKey); }

        public string GetCfgNugetName() { return GetSetting(CfgNugetKey); }

        public string GetAppNugetRepo() { return GetSetting(AppNugetRepoKey); }

        public string GetHostNugetName() { return GetSetting(AppNugetKey); }

        public string GetClientId() { return GetSetting(ClientIdKey); }

        public string GetHostType() { return GetSetting(AppTypeKey); }

        public string GetNugetApiKey() { return GetSetting(NugetApiKeyKey); }

        public string GetHostDeplPar() { return GetSetting(AppDeploymentParKey); }

        public bool IncludeAppPrereleases() { return "yes".Equals(GetSetting(AppNugetIncludePrereleaseKey), System.StringComparison.OrdinalIgnoreCase); }

        public bool IncludeCfgPrereleases() { return "yes".Equals(GetSetting(CfgNugetIncludePrereleaseKey), System.StringComparison.OrdinalIgnoreCase); }

        public string GetPipelineName()
        {
            return $"{GetSetting(PipelineNamePrefixKey)}_{GetSetting(PipelineNameKey)}_{GetSetting(PipelineNameSufixKey)}".Trim('_');
        }

        public string GetPipelineTemplate() { return GetSetting(PipelineTemplateKey); }

        public string GetPipelineGroup() { return GetSetting(PipelineGroupKey); }

        public string GetMaterialFilter() { return GetSetting(MaterialFilter); }

        public string GetMaterialFilterIsReversed() { return GetSetting(MaterialFilterIsInverted); }

        public string Branch
        { get { return GetSetting(GitBranchKey); } }
        public string Environment { get; set; }
        public string HostName { get; set; }
        public string SoftwareName { get; set; }

        string GetSetting(string key)
        {
            string theKey = NameBuilder.GetSettingName(HostName, Environment, Machine.NotSpecified, key.ToLower()).ToLower();

            if (!configuration.ContainsKey(theKey))
                throw new KeyNotFoundException($"The corresponding key is missing - {theKey}");

            string theValue = (string)configuration[theKey];

            string parameterPattern = @"\${(.*?)}";
            var regex = new Regex(parameterPattern, RegexOptions.IgnoreCase);
            Match match = regex.Match(theValue);
            if (match.Success)
            {
                string wrappedKey = match.Value;
                string internalKey = match.Groups[1].Value;
                string internalValue = GetSetting(internalKey);
                string evaluated = theValue.Replace(wrappedKey, internalValue);
                return evaluated;
            }

            return theValue;
        }
    }

    public static class C3poConfigExtension
    {
        public static bool IsValid(this C3poConfig cfg)
        {
            var requiredSettingsKeysPropNames = new string[]
            {
                "ClientIdKey",
                "ApplicationKey",
                "GitRemoteKey",
                "GitBranchKey",
                "PipelineNameKey",
                "PipelineGroupKey",
                "PipelineTemplateKey",
                "NugetApiKeyKey",
                "C3poTypeKey"
            };

            try
            {
                foreach (var keyPropName in requiredSettingsKeysPropNames)
                {
                    var keyPropValue = cfg.GetType().GetField(keyPropName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static).GetRawConstantValue();
                    var cfgValue = cfg.GetType().GetMethod("GetSetting", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(cfg, new[] { keyPropValue });

                    if (string.IsNullOrEmpty((string)cfgValue))
                        return false;
                }


                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }

        }
    }
}
