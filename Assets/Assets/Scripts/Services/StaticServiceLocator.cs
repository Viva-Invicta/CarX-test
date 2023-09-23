using System;
using System.Collections.Generic;
using System.Linq;

namespace TowerDefence.Services
{
    public static class StaticServiceLocator
    {
        public static event Action Initialized;

        private static HashSet<IService> services = new HashSet<IService>();

        public static bool IsInitialized { get; private set; }

        public static void InitializeServices(IEnumerable<IService> services)
        {
            foreach (var service in services)
            {
                service.Initialize();
            }

            StaticServiceLocator.services = services.ToHashSet();

            IsInitialized = true;
            Initialized?.Invoke();
        }

        public static T GetService<T>() where T : IService
        {
            return (T)services.FirstOrDefault(service => service is T);
        }
    }
}