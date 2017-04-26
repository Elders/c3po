using System.Collections.Generic;

namespace c_3po
{
    public class UpdateEnvironmentPut
    {
        public string Name { get; set; }

        public IEnumerable<PipelineUpdate> Pipelines { get; set; }

        public IEnumerable<AgentPost> Agents { get; set; }
    }

    public class PipelineUpdate
    {
        public string Name { get; set; }
    }
}
