using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static c_3po.GocdClient;

namespace c_3po
{
    public class CreateEnvironmentPost
    {
        public string name { get; set; }

        public List<Pipeline> pipelines { get; set; }

        public List<AgentPost> agents { get; set; }

        public List<EnviromentVariablePost> environment_variables { get; set; }
    }

    public class AgentPost
    {
        public string Uuid { get; set; }
    }

    public class EnviromentVariablePost
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public string Secure { get; set; }
    }
}
