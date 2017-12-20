using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Vitruvian.Services;
using Vitruvian.Logging;

namespace ColonyResourceManager
{
    class Program
    {
        private static Logger _logger = Logger.GetLogger(typeof(Program));

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            ServiceRegistry.Load(typeof(Program), @"Config.Services.xml");

            ServiceRegistry.Init();

            try
            {
                _logger.Debug("Just before ServiceRegistry.Run");
                ServiceRegistry.Run();
            }
            finally
            {
                ServiceRegistry.Cleanup();
            }
        }
    }
}
