﻿using System;
using System.Net;

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

        public void CreatingDeployPipeline(string applicationName, string pipelineName)
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
            System.Console.WriteLine($"I extracted {extractedCfgs} configurations.");
        }

        public void StartedCreating()
        {
            SetColor(ConsoleColor.DarkYellow);
            System.Console.WriteLine($"I start sending messages to r2d2 to start creating...");
        }

        public void StartedExtractingConfigurations()
        {
            SetColor(ConsoleColor.DarkYellow);
            System.Console.WriteLine("Started extacting configurations.");
        }

        public void R2d2Responded(HttpStatusCode r2d2response, string item, string application, string message)
        {
            System.Console.WriteLine();

            switch (r2d2response)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.Created:
                case HttpStatusCode.Accepted:
                    SetColor(ConsoleColor.DarkGreen);
                    System.Console.WriteLine($"     Excuse me sir, but that R2-D2 is in prime condition, a real bargain. He created item \"{item}\" for \"{application}\".");
                    break;
                case HttpStatusCode.NotFound:
                    SetColor(ConsoleColor.DarkRed);
                    System.Console.WriteLine($"     R2-D2, where are you?");
                    break;
                case (HttpStatusCode)422:
                    SetColor(ConsoleColor.Yellow);
                    System.Console.WriteLine($"     An item \"{item}\" for appliciation \"{application}\" already exists. R2-D2 says: ");
                    System.Console.WriteLine($"     {message}");
                    break;
                default:
                    SetColor(ConsoleColor.DarkRed);
                    System.Console.WriteLine($"     We're doomed! R2d2 cannot create pipiline for {application}. R2-D2 says: ");
                    System.Console.WriteLine($"     {message}");
                    break;
            }

            System.Console.WriteLine();
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
            System.Console.WriteLine("===============================");
            System.Console.WriteLine();
        }

        public void ThereIsError(string message)
        {
            SetColor(ConsoleColor.DarkRed);
            System.Console.WriteLine(message);
        }
    }
}
