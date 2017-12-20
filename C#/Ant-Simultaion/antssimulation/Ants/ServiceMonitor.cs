using System;
using System.Collections.Generic;
using System.Text;
using Vitruvian.Services;
using Vitruvian.Distribution;

namespace Ants
{
    public static class ServiceMonitor
    {
        private class Monitor
        {
            private ServiceHandler _serviceAdded = null;
            private ServiceHandler _serviceRemoved = null;

            public Monitor(ServiceHandler serviceAdded, ServiceHandler serviceRemoved)
            {
                _serviceAdded = serviceAdded;
                _serviceRemoved = serviceRemoved;
            }

            public ServiceHandler ServiceAdded
            {
                get { return _serviceAdded; }
            }

            public ServiceHandler ServiceRemoved
            {
                get { return _serviceRemoved; }
            }
        }

        private static Dictionary<Type, List<Monitor>> _monitors = new Dictionary<Type, List<Monitor>>();

        static public void StartMonitoring<T>(ServiceHandler serviceAdded, ServiceHandler serviceRemoved) where T : IService
        {
            lock (_monitors)
            {
                if (_monitors.Count == 0)
                {
                    ServiceRegistry.ServiceAdded += ServiceAdded;
                    ServiceRegistry.ServiceRemoved += ServiceRemoved;
                }

                Type type = typeof(T);
                if (!_monitors.ContainsKey(type))
                    _monitors.Add(type, new List<Monitor>());

                _monitors[type].Add(new Monitor(serviceAdded, serviceRemoved));

                List<T> services = ServiceRegistry.GetServices<T>();
                foreach (T service in services)
                {
                    if (serviceAdded != null)
                        serviceAdded(service);
                }
            }
        }

        static public void StopMonitoring<T>(ServiceHandler serviceAdded, ServiceHandler serviceRemoved) where T : IService
        {
            lock (_monitors)
            {
                Type type = typeof(T);
                if (_monitors.ContainsKey(type))
                {
                    List<Monitor> monitors = _monitors[type];

                    foreach (Monitor monitor in monitors)
                    {
                        if (monitor.ServiceAdded == serviceAdded &&
                            monitor.ServiceRemoved == serviceRemoved)
                        {
                            monitors.Remove(monitor);
                            break;
                        }
                    }

                    if (_monitors[type].Count == 0)
                        _monitors.Remove(type);
                }

                if (_monitors.Count == 0)
                {
                    ServiceRegistry.ServiceAdded -= ServiceAdded;
                    ServiceRegistry.ServiceRemoved -= ServiceRemoved;
                }
            }
        }

        private static Type GetBaseType(Type T)
        {
            // TODO: Finish
            return T;
        }

        private static void ServiceAdded(IService service)
        {
            lock (_monitors)
            {
                Type type = GetBaseType(service.GetType());

                if (_monitors.ContainsKey(type))
                {
                    // do this in case the monitor is removed from the list in the delegate
                    Monitor[] monitors = _monitors[type].ToArray();

                    foreach (Monitor monitor in monitors)
                    {
                        if (monitor.ServiceAdded != null)
                            monitor.ServiceAdded(service);
                    }
                }
            }
        }

        private static void ServiceRemoved(IService service)
        {
            lock (_monitors)
            {
                Type type = GetBaseType(service.GetType());

                if (_monitors.ContainsKey(type))
                {
                    // do this in case the monitor is removed from the list in the delegate
                    Monitor[] monitors = _monitors[type].ToArray();

                    foreach (Monitor monitor in monitors)
                    {
                        if (monitor.ServiceRemoved != null)
                            monitor.ServiceRemoved(service);
                    }
                }
            }
        }
    }
}
