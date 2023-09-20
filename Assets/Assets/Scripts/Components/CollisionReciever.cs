using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CollisionReciever : MonoBehaviour
{
    private Collider collider;

    private void OnEnable()
    {
        collider = GetComponent<Collider>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        EventBus.RaiseEvent(new CollisionEvent { Collider = collider, OtherCollider = other });
    }
}