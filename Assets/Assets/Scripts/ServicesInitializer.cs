using UnityEngine;

namespace TowerDefence.Services
{
    public class ServicesInitializer : MonoBehaviour
    {
        private void Start()
        {
            var services = new IService[] { new TowerTargetsService(), new CollisionHandlerService() };
            StaticServiceLocator.InitializeServices(services);
        }
    }
}