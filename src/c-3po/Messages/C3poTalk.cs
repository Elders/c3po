using c_3po.Messages.Console;
using c_3po.Messages.LibLog;

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
                case C3poVoiceInterface.LibLog:
                    return new C3poLibLogSpeachProvider();
                default:
                    return null;
            }
        }
    }

    public enum C3poVoiceInterface
    {
        Silent,
        Console,
        LibLog
    }
}
