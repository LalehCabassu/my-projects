using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Vitruvian.Services;

namespace SimulationController
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

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