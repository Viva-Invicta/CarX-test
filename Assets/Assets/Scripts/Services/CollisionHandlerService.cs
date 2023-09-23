using TowerDefence.Components;
using TowerDefence.Events;

namespace TowerDefence.Services
{
    public class CollisionHandlerService : IService
    {
        public void Initialize()
        {
            StaticEventBus.Subscribe<CollisionEvent>(HandleCollision);
        }

        private void HandleCollision(CollisionEvent collisionEvent)
        {
            var healthComponent = collisionEvent.Collider.GetComponent<HealthPoints>();
            var damageComponent = collisionEvent.Collider.GetComponent<Damager>();

            var otherHealthComponent = collisionEvent.OtherCollider.GetComponent<HealthPoints>();
            var otherDamageComponent = collisionEvent.OtherCollider.GetComponent<Damager>();

            if (healthComponent && otherDamageComponent)
                healthComponent.DecreaseHP(otherDamageComponent.Damage);

            if (damageComponent && otherHealthComponent)
                otherHealthComponent.DecreaseHP(damageComponent.Damage);
        }
    }
}