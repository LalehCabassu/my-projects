using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Vitruvian.Services;
using UserInterfaceComponents;

namespace SimulationViewer
{
    class Program
    {
        static void Main(string[] args)
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
