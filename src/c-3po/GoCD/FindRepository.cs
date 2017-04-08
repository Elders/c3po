using System.Linq;

namespace c_3po
{
    public partial class GocdClient
    {
        public PackageRepository FindRepository(string repository)
        {
            return GetRepositories()
                .Where(x => x.name.Equals(repository, System.StringComparison.OrdinalIgnoreCase))
                .SingleOrDefault();
        }
    }
}
