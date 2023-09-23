using UnityEngine;

namespace TowerDefence.Events
{
    public record CollisionEvent(Collider Collider, Collider OtherCollider)
    {
        public Collider Collider { get; } = Collider;
        public Collider OtherCollider { get; } = OtherCollider;
    }
}