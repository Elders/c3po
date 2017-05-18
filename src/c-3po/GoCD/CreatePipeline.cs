using System.Collections.Generic;
using RestSharp;

namespace c_3po
{
    public partial class GocdClient
    {
        public IRestResponse CreatePipeline(CreatePipelinePost createPipeline, Authenticator authenticator = null)
        {
            const string resource = "admin/pipelines";

            var request = CreateRestRequest(resource, Method.POST, authenticator);
            request.AddHeader("Accept", "application/vnd.go.cd.v3+json");

            request.AddNewtonsoftJsonBody(createPipeline);
            var response = CreateRestClient().Post(request);



            return response;
        }

        public class CreatePipelinePost
        {
            public string Group { get; set; }

            public Pipeline Pipeline { get; set; }
        }

        public class Pipeline
        {
            public Pipeline()
            {
                label_template = "${COUNT}";
                enable_pipeline_locking = true;
            }

            public string label_template { get; set; }

            public bool enable_pipeline_locking { get; set; }

            public string name { get; set; }

            public string template { get; set; }

            public List<Material> materials { get; set; }

            public List<Parameter> parameters { get; set; }
        }

        public class Material
        {
            public string type { get; set; }

            public IMaterialAttributes attributes { get; set; }
        }

        public interface IMaterialAttributes { }

        public class GitMaterialAttributes : IMaterialAttributes
        {
            public GitMaterialAttributes()
            {
                auto_update = true;
            }

            public string url { get; set; }
            public string destination { get; set; }
            public Filter filter { get; set; }
            public string name { get; set; }
            public bool auto_update { get; set; }
            public string branch { get; set; }
            public string submodule_folder { get; set; }
            public string inverted_filter { get; set; }
        }

        public class PackageMaterialAttributes : IMaterialAttributes
        {
            public string Ref { get; set; }
            public string Destination { get; set; }
        }

        public class Parameter
        {
            public string name { get; set; }
            public string value { get; set; }
        }
    }
}
