using UnityEngine;

public class TowerTarget : MonoBehaviour
{
    private void OnEnable()
    {
        EventBus.RaiseEvent(new TowerTargetStatusUpdatedEvent(transform, true));
    }


    private void OnDisable()
    {
        EventBus.RaiseEvent(new TowerTargetStatusUpdatedEvent(transform, false));
    }
}