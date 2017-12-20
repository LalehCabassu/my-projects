using System;
using System.Collections.Generic;
using System.Text;

using Vitruvian.Services;
using Vitruvian.Logging;

namespace GroundResourceManager
{
    static class Program
    {
        private static Logger _logger = Logger.GetLogger(typeof(Program));

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            ServiceRegistry.Load(typeof(Program), @"Config.Services.xml");

            ServiceRegistry.Init();

            try
            {
                ServiceRegistry.Run();
            }
            finally
            {
                ServiceRegistry.Cleanup();
            }
        }
    }
}
