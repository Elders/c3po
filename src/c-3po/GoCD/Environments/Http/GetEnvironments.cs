﻿using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace c_3po
{
    public partial class GocdClient
    {
        public EnviromentsResult GetEnviroments(Authenticator authenticator = null)
        {
            const string resource = "admin/environments";
            var request = CreateRestRequest(resource, Method.GET, authenticator);
            request.AddHeader("Accept", "application/vnd.go.cd.v2+json");
            var response = CreateRestClient().Execute<EnviromentsResult>(request);
            var etag = response.Headers.FirstOrDefault(x => x.Name == "ETag")?.Value;

            response.Data.ETag = ReferenceEquals(null, etag) ? null : etag.ToString();
            if ((ReferenceEquals(response, null) == true) || (ReferenceEquals(response.Data, null) == true))
            {
                var error = "Error occurred while retrieving envierments from GOCD.";
                c3poSpeakProgram.ThereIsError(error);
                throw new System.Exception(error);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var error = "Unauthorized exception while retrieving envierments from GOCD.";
                c3poSpeakProgram.ThereIsError(error);
                throw new System.Exception(error);
            }

            return response.Data;
        }

        public class EnviromentsResult
        {
            public EmbeddedEnviroment _embedded { get; set; }
            public string ETag { get; set; }
        }

        public class EmbeddedEnviroment
        {
            public EmbeddedEnviroment()
            {
                environments = new List<Environment>();
            }

            public IList<Environment> environments { get; set; }
        }

        public class Environment
        {
            public string name { get; set; }
            public IEnumerable<Pipeline> pipelines { get; set; }
        }
    }
}
