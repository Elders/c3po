using System;
using c_3po.Messages;

namespace c_3po
{
    public sealed partial class GocdClient
    {
        Options options;
        Authenticator authenticator;
        C3poSpeachProgram c3poSpeakProgram;

        public GocdClient(Options options, C3poSpeachProgram c3poSpeakProgram, Authenticator authenticator = null)
        {
            if (ReferenceEquals(null, options)) throw new ArgumentNullException(nameof(options));

            this.options = options;

            if (ReferenceEquals(null, c3poSpeakProgram)) throw new ArgumentNullException(nameof(c3poSpeakProgram));
            this.c3poSpeakProgram = c3poSpeakProgram;

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
