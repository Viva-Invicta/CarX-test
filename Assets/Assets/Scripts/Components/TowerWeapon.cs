using TowerDefence;
using UnityEngine;

public class TowerWeapon : MonoBehaviour
{
    private const int PoolCapacity = 10;

    [SerializeField]
    private TowerProjectile projectilePrefab;

    [SerializeField]
    private Transform shootPoint;

    [SerializeField]
    private Transform projectilesRoot;

    private TowerTarget selectedTarget;
    private MonoBehaviourObjectPool<TowerProjectile> pool;

    private void OnEnable()
    {
        pool = new MonoBehaviourObjectPool<TowerProjectile>(PoolCapacity, projectilesRoot, projectilePrefab);
    }

    public void SetTarget(TowerTarget target)
    {
        selectedTarget = target;
    }

    public void Shoot()
    {
        if (pool == null)
            return;

        if (!selectedTarget)
            return;

        var projectile = pool.GetInstance();
        projectile.transform.position = shootPoint.position;

        projectile.SetTarget(selectedTarget);
    }
}