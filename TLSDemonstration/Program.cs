using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TLSDemonstration
{
    static class Program
    {
        public static Logger logger = LogManager.GetLogger("ApplicationLogger");

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // event handler for handling UI thread exceptions
            Application.ThreadException += new ThreadExceptionEventHandler(OnThreadException);

            // force all Windows Forms errors to go through our handler
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // event handler for handling non-UI thread exceptions
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(OnUnhandledException);

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);

            if (!Directory.Exists(Settings.LogFolder)) Directory.CreateDirectory(Settings.LogFolder);

            Application.Run(new Form1());
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            logger.Error((Exception)args.ExceptionObject);
        }

        private static void OnThreadException(object sender, ThreadExceptionEventArgs e)
        {
            logger.Error(e.Exception);
        }

        private static void OnProcessExit(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
