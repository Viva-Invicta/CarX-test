using TowerDefence;
using UnityEngine;

namespace TowerDefence.Components
{
    public abstract class Weapon : MonoBehaviour
    {
        private const int PoolCapacity = 10;

        [SerializeField]
        private Transform projectilesRoot;

        [SerializeField]
        protected Transform shootPoint;

        [SerializeField]
        protected TowerProjectile projectilePrefab;

        protected TowerTarget selectedTarget;
        protected MonoBehaviourObjectPool<TowerProjectile> projectilesPool;

        private void OnEnable()
        {
            projectilesPool = new MonoBehaviourObjectPool<TowerProjectile>(PoolCapacity, projectilesRoot, projectilePrefab);
        }

        public void SetTarget(TowerTarget target)
        {
            selectedTarget = target;
        }

        public void Shoot()
        {
            if (!selectedTarget)
                return;

            ShootInternal();
        }

        protected abstract void ShootInternal();
    }
}