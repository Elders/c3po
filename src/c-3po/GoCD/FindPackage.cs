using System.Linq;
using System.Text.RegularExpressions;
using thegit;

namespace c_3po
{
    public partial class GocdClient
    {
        public Package FindPackage(string name, string repository)
        {
            return GetRepositories()
                .Where(x => x.name.Equals(repository, System.StringComparison.OrdinalIgnoreCase))
                .SingleOrDefault()?._embedded?.packages
                ?.Where(x => x.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase))
                ?.Select(x =>
                {
                    x.RepositoryName = repository;
                    return x;
                })
                ?.SingleOrDefault();
        }

        public Package FindOrCreatePackage(string name, string repository, bool includePreReleases, C3poConfig config)
        {
            string gocdName = name;
            if (includePreReleases)
                gocdName = name + "_" + config.Branch;

            var package = FindPackage(gocdName, repository);
            if (ReferenceEquals(null, package))
            {
                var regex = new Regex(@"(?<major>\d+)\.(?<minor>\d+)\.(?<patch>\d+)$");
                var regMatch = regex.Match(config.Branch);
                if (includePreReleases && regMatch.Success)
                {
                    int major = int.Parse(regMatch.Groups["major"].Value);
                    int minor = int.Parse(regMatch.Groups["minor"].Value);
                    string pollFrom = $"{major}.{minor}.0-beta0000";
                    string pollTo = $"{major}.{++minor}.0";
                    var response = CreatePackage(repository, name, gocdName, includePreReleases, pollFrom, pollTo);
                }
                else
                {
                    var response = CreatePackage(repository, name, gocdName, false);
                }
                package = FindPackage(gocdName, repository);
            }
            if (ReferenceEquals(null, package))
                throw new System.Exception("Unable to find package " + gocdName);

            return package;
        }
    }
}
