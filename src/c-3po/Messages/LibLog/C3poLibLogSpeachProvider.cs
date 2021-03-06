﻿using c_3po.Logging;
using System.Net;

namespace c_3po.Messages.LibLog
{
    public class C3poLibLogSpeachProvider : IC3poSpeachProvider
    {
        private static readonly ILog Logger = LogProvider.GetCurrentClassLogger();


        public void CreatingDeployPipeline(string applicationName, string pipelineName)
        {
            Logger.Info("...Deploy pipeline '{pipelineName}' for application '{applicationName}'");
        }

        public void CreatingMonoRepoPipeline(string applicationName, string pipelineName)
        {
            Logger.Info($"...Mono-Repo pipeline '{pipelineName}' for application '{applicationName}'");
        }

        public void CreatingRepoPipeline(string applicationName, string pipelineName)
        {
            Logger.Info($"...Repo pipeline '{pipelineName}' for application '{applicationName}'");
        }

        public void FinishedExtractingConfigurations(long extractedCfgs)
        {
            Logger.Info($"I extracted {extractedCfgs} configurations.");
        }

        public void GoodByeMaster()
        {
            Logger.Info($"Fairwell, Master!");
        }

        public void HelloMaster()
        {
            Logger.Info("Hello! It is I, C-3PO!");
        }

        public void R2d2Responded(HttpStatusCode r2d2response, string item, string application, string message)
        {
            switch (r2d2response)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.Created:
                case HttpStatusCode.Accepted:
                    Logger.Info($"Excuse me sir, but that R2-D2 is in prime condition, a real bargain. He created \"{item}\" for appliciation \"{application}\".");
                    break;
                case HttpStatusCode.NotFound:
                    Logger.Error($"R2-D2, where are you?");
                    break;
                case (HttpStatusCode)422:
                    Logger.Warn($"An item \"{item}\" for appliciation \"{application}\" already exists.");
                    break;
                default:
                    Logger.Error($"We're doomed! R2d2 cannot create pipiline for \"{application}\".");
                    Logger.Error($"     R2-D2 says: {message}");
                    break;
            }
        }

        public void StartedCreating()
        {
            Logger.Info($"I start sending messages to r2d2 to start creating...");
        }

        public void StartedExtractingConfigurations()
        {
            Logger.Info($"Started extacting configurations.");
        }

        public void ThereIsError(string message)
        {
            Logger.Error(message);
        }
    }
}
