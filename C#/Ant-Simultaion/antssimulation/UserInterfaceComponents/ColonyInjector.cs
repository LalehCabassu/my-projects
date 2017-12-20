using System;
using System.Collections.Generic;
using System.Text;

using Vitruvian.Services;
using Vitruvian.Serialization;
using Vitruvian.Distribution;

using Ants;

namespace AntsUI
{
    public class ColonyInjector : IService
    {
        private Guid _id = Guid.NewGuid();
        private Colony _colony = null;

        #region IService Members

        public Guid Id
        {
            get { return _id; }
        }

        public string Name
        {
            get { return "Colony Injector"; }
        }

        public void Init()
        {
            ServiceMonitor.StartMonitoring<Ground>(GroundAvailable, null);
        }

        public void Cleanup()
        {
            ServiceMonitor.StopMonitoring<Ground>(GroundAvailable, null);
        }

        #endregion

        [Serialize]
        public Colony Colony
        {
            get { return _colony; }
            set { _colony = value; }
        }

        private void GroundAvailable(IService service)
        {
            ServiceMonitor.StopMonitoring<Ground>(GroundAvailable, null);

            if (_colony != null)
            {
                _colony.Init();
                ServiceRegistry.GetPreferredService<DistributionService>().Services.Add(_colony);
                ServiceRegistry.Add(_colony);
            }
        }
    }
}
