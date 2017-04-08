using System;
using System.Threading.Tasks;

namespace c_3po
{
    public sealed class Authenticator
    {
        public Authenticator(string basicToken)
        {
            AccessToken = basicToken;
        }

        public string AccessToken { get; private set; }

        public int ExpiresIn { get { return int.MaxValue; } }

        public Task<Authenticator> GetClientCredentialsAuthenticatorAsync()
        {
            return Task.FromResult(new Authenticator(AccessToken));
        }

        public sealed class Options
        {
            public Uri Authority { get; set; }

            public string ClientId { get; set; }

            public string ClientSecret { get; set; }

            public string Scope { get; set; }
        }
    }
}
