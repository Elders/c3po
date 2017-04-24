using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static c_3po.GocdClient;

namespace c_3po
{
    public class UpdateEnvironmentPut
    {
        public string name { get; set; }

        public IEnumerable<PipelineUpdate> pipelines { get; set; }
    }

    public class PipelineUpdate
    {
        public string name { get; set; }
    }
}
