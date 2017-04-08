using System;

namespace c_3po
{
    public sealed partial class GocdClient
    {
        Options options;
        Authenticator authenticator;

        public GocdClient(Options options, Authenticator authenticator = null)
        {
            if (ReferenceEquals(null, options)) throw new ArgumentNullException(nameof(options));
            this.options = options;

            this.authenticator = authenticator;
        }

        public sealed class Options
        {
            public Options(Uri apiAddress)
            {
                ApiAddress = apiAddress;
                JsonSerializer = NewtonsoftJsonSerializer.Default();
            }

            public Uri ApiAddress { get; private set; }
            public IJsonSerializer JsonSerializer { get; private set; }
        }
    }
}
