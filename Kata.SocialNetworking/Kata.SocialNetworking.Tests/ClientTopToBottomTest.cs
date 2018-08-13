using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Kata.SocialNetworking.Client;
using TechTalk.SpecFlow;

namespace Kata.SocialNetworking.Tests
{
    public class ClientTopToBottomTest
    {
        private ProcessStartInfo clientInfo;
        private Process clientProcess;

        [BeforeScenario]
        public void BeforeScenario()
        {
            var path = Assembly.GetAssembly(typeof(Program)).Location;
            clientInfo = new ProcessStartInfo()
            {
                FileName = path,
                UseShellExecute = false,
                RedirectStandardOutput = false,
                RedirectStandardInput = true
            };

            clientProcess = new Process
            {
                StartInfo = clientInfo
            };

            clientProcess.Start();
            clientProcess.EnableRaisingEvents = true;
            clientProcess.OutputDataReceived += process_OutputDataReceived;
            clientProcess.ErrorDataReceived += process_ErrorDataReceived;
            clientProcess.Exited += process_Exited;
        }

        [AfterScenario]
        public void AfterScenario()
        {
            clientProcess.Dispose();
        }

        protected void WriteToClient(string text)
        {
            clientProcess.StandardInput.WriteLineAsync(text);
        }

        protected string ReadClientText()
        {
            return clientProcess.StandardOutput.ReadToEndAsync().Result;
        }


        void process_Exited(object sender, System.EventArgs e)
        {
            // do something when process terminates;
        }

        void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            // a line is writen to the out stream. you can use it like:
            string s = e.Data;
        }

        void process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            // a line is writen to the out stream. you can use it like:
            string s = e.Data;
        }
    }
}
