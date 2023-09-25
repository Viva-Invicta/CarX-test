using TowerDefence;
using UnityEngine;

namespace TowerDefence.Components
{
    public abstract class Weapon : MonoBehaviour
    {
        private const int PoolCapacity = 5;

        [SerializeField]
        private Transform projectilesRoot;

        [SerializeField]
        protected Transform shootPoint;

        [SerializeField]
        protected TowerProjectile projectilePrefab;

        [SerializeField]
        private ProjectileParticles particlesPrefab;

        protected TowerTarget selectedTarget;
        protected MonoBehaviourObjectPool<TowerProjectile> projectilesPool;
        private MonoBehaviourObjectPool<ProjectileParticles> particlesPool;

        private void OnEnable()
        {
            projectilesPool = new MonoBehaviourObjectPool<TowerProjectile>(PoolCapacity, projectilesRoot, projectilePrefab);

            if (particlesPrefab)
                particlesPool = new MonoBehaviourObjectPool<ProjectileParticles>(PoolCapacity, projectilesRoot, particlesPrefab);
        }

        public void SetTarget(TowerTarget target)
        {
            selectedTarget = target;
        }

        public void Shoot()
        {
            if (!selectedTarget)
                return;

            var projectile = ShootInternal();
            if (projectile && particlesPool != null)
            {
                var particles = particlesPool.GetInstance();
                particles.SetAssociatedProjectile(projectile);
            }
        }

        protected abstract TowerProjectile ShootInternal();
    }
}