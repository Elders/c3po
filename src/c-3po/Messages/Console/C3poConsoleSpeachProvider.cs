using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace c_3po.Messages.Console
{
    class C3poConsoleSpeachProvider : IC3poSpeachProvider
    {
        public void HelloMaster()
        {
            SetColor(ConsoleColor.DarkYellow);
            DrawC3po("Hello! It is I, C-3PO!");
        }

        public void GoodByeMaster()
        {
            SetColor(ConsoleColor.DarkYellow);
            DrawC3po("Fairwell, Master!");
        }

        public void CreatingDeployPipeline(string applicationName,string pipelineName)
        {
            SetColor(ConsoleColor.DarkYellow);
            System.Console.WriteLine($"     ...Deploy pipeline '{pipelineName}' for application '{applicationName}'");
        }

        public void CreatingMonoRepoPipeline(string applicationName, string pipelineName)
        {
            SetColor(ConsoleColor.DarkYellow);
            System.Console.WriteLine($"     ...Mono-Repo pipeline '{pipelineName}' for application '{applicationName}'");
        }

        public void CreatingRepoPipeline(string applicationName, string pipelineName)
        {
            SetColor(ConsoleColor.DarkYellow);
            System.Console.WriteLine($"     ...Repo pipeline '{pipelineName}' for application '{applicationName}'");
        }

        public void FinishedExtractingConfigurations(long extractedCfgs)
        {
            SetColor(ConsoleColor.DarkYellow);
            System.Console.WriteLine($"-- I extracted {extractedCfgs} configurations.");
        }

        public void StartedCreating()
        {
            SetColor(ConsoleColor.DarkYellow);
            System.Console.WriteLine($"-- I start messaging to r2d2 to start creating...");
        }

        public void StartedExtractingConfigurations()
        {
            SetColor(ConsoleColor.DarkYellow);
            System.Console.WriteLine("-- Started extacting configurations.");
        }

        public void R2d2Responded(HttpStatusCode r2d2response,string applicationName)
        {
            switch (r2d2response)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.Created:
                case HttpStatusCode.Accepted:
                    SetColor(ConsoleColor.DarkGreen);
                    System.Console.WriteLine($"-Excuse me sir, but that R2-D2 is in prime condition, a real bargain. He created pipe for \"{applicationName}\".");
                    break;
                case HttpStatusCode.NotFound:
                    SetColor(ConsoleColor.DarkRed);
                    System.Console.WriteLine($"-R2-D2, where are you?");
                    break;
                default:
                    SetColor(ConsoleColor.DarkRed);
                    System.Console.WriteLine($"-We're doomed! R2d2 cannot create pipiline for {applicationName}.");
                    break;
            }
        }

        private void SetColor(ConsoleColor color)
        {
            System.Console.ForegroundColor = color;
        }

        private void DrawC3po(string message)
        {
            System.Console.WriteLine();
            System.Console.WriteLine("===============================");
            System.Console.WriteLine($"      /~\\   {message}");
            System.Console.WriteLine("     (O O) _/           ");
            System.Console.WriteLine("     _\\=/_");
            System.Console.WriteLine("    /  _  \\  ");
            System.Console.WriteLine("   //|/.\\|\\");
            System.Console.WriteLine("  ||  \\_/  || ");
            System.Console.WriteLine("  || |\\ /| || ");
            System.Console.WriteLine("   # \\_ _/ # ");
            System.Console.WriteLine("     | | |   ");
            System.Console.WriteLine("     | | |    ");
            System.Console.WriteLine("     []|[]     ");
            System.Console.WriteLine("     | | |    ");
            System.Console.WriteLine("    /_]_[_\\_    ");
            System.Console.WriteLine("===============================");
            System.Console.WriteLine();
        }
    }
}
