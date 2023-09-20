using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CollisionReciever : MonoBehaviour
{
    private Collider collider;

    private void OnEnable()
    {
        var collider = GetComponent<Collider>();    
    }

    private void OnCollisionEnter(Collision collision)
    {
        EventBus.RaiseEvent(new CollisionEvent { Collider = collider, OtherCollider = collision.collider });
    }
}