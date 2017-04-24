using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c_3po
{
    public partial class GocdClient
    {
        public IRestResponse CreateEnvironmentIfDoesntExists(string enviroment, Authenticator authenticator = null)
        {
            var enviroments = GetEnviroments();

            if (!enviroments._embedded.environments.Any(x => x.name.Equals(enviroment, System.StringComparison.OrdinalIgnoreCase)))
            {
                return CreateEnvironment(new CreateEnvironmentPost { name = enviroment }, authenticator);
            }

            return null;
        }
    }
}
