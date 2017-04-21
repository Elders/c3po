using c_3po.Messages.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c_3po.Messages
{
    public class C3poTalk
    {
        public IC3poSpeachProvider speach;

        public C3poTalk(C3poVoiceInterface speachProvider = C3poVoiceInterface.Silent)
        {
            Says = HowToSpeakMaster(speachProvider);
        }

        public IC3poSpeachProvider Says { get; set; }

        private IC3poSpeachProvider HowToSpeakMaster(C3poVoiceInterface speachProvider)
        {
            switch (speachProvider)
            {
                case C3poVoiceInterface.Console:
                    return new C3poConsoleSpeachProvider();
                default:
                    return null;
            }
        }
    }

    public enum C3poVoiceInterface
    {
        Silent,
        Console
    }
}
