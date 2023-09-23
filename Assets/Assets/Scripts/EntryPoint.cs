using TowerDefence.Services;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    private void Start()
    {
        var services = new IService[] { new TowerTargetsService(), new CollisionHandlerService() };
        StaticServiceLocator.InitializeServices(services);
    }
}
