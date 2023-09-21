using TowerDefence;
using UnityEngine;

public abstract class TowerWeapon : MonoBehaviour
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
        if (projectilesPool == null)
            return;

        if (!selectedTarget)
            return;

        ShootInternal();
    }

    protected abstract void ShootInternal();
    protected virtual void OnAfterTargetSet() { }
}