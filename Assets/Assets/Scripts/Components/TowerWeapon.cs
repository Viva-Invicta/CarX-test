using TowerDefence;
using UnityEngine;

public class TowerWeapon : MonoBehaviour
{
    private const int PoolCapacity = 10;

    [SerializeField]
    private Projectile projectilePrefab;

    [SerializeField]
    private Transform shootPoint;

    private Transform selectedTarget;
    private MonoBehaviourObjectPool<Projectile> pool;

    private void OnEnable()
    {
        pool = new MonoBehaviourObjectPool<Projectile>(PoolCapacity, transform, projectilePrefab);
    }

    public void SetTarget(Transform target)
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