using UnityEngine;

public class TowerTarget : MonoBehaviour
{
    [field: SerializeField]
    public Mover Mover { get; private set; }

    private void OnEnable()
    {
        EventBus.RaiseEvent(new TowerTargetStatusUpdatedEvent(this, true));
    }

    private void OnDisable()
    {
        EventBus.RaiseEvent(new TowerTargetStatusUpdatedEvent(this, false));
    }
}