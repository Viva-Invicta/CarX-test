using UnityEngine;

public class TowerTargetStatusUpdatedEvent
{
    public TowerTarget Target { get; private set; }
    public bool IsAvailable { get; private set; }   

    public TowerTargetStatusUpdatedEvent(TowerTarget target, bool isAvailable)
    {
        Target = target;
        IsAvailable = isAvailable;
    }
}