using UnityEngine;

public class TowerTargetStatusUpdatedEvent
{
    public Transform Target { get; private set; }
    public bool IsAvailable { get; private set; }   

    public TowerTargetStatusUpdatedEvent(Transform target, bool isAvailable)
    {
        this.Target = target;
        this.IsAvailable = isAvailable;
    }
}