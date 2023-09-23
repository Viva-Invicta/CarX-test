using TowerDefence.Events;
using UnityEngine;

namespace TowerDefence.Components
{
    [RequireComponent(typeof(Collider))]
    public class CollisionReciever : MonoBehaviour
    {
        private Collider collider;

        private void OnEnable()
        {
            collider = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider otherCollider)
        {
            StaticEventBus.RaiseEvent(new CollisionEvent(collider, otherCollider));
        }
    }
}