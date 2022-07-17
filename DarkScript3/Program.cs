using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices;

namespace DarkScript3
{
    static class Program
    {
        // https://stackoverflow.com/questions/7198639/c-sharp-application-both-gui-and-commandline
        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);
        private const int ATTACH_PARENT_PROCESS = -1;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            bool commandLine = args.Contains("/cmd");
#if DEBUG
            commandLine = args.Length > 0;
#endif
            if (commandLine)
            {
                // Command line things for testing
                AttachConsole(-1);
                if (args.Contains("test"))
                {
                    new CondTestingTool().Run(args);
                }
                else if (args.Contains("html"))
                {
                    EMEDF2HTML.Generate(args);
                }
                else
                {
                    RoundTripTool.Run(args);
                }
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GUI());
        }
    }
}
