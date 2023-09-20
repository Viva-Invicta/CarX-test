using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private Mover mover;

    [SerializeField]
    private ProjectileType projectileType;

    [SerializeField]
    private Transform selectedTarget;

    public void SetTarget(Transform target)
    {
        selectedTarget = target;

        if (selectedTarget != null)
            mover.SetTargetPosition(selectedTarget.position);
        else
            mover.SetTargetPosition(null);
    }

    private void OnEnable()
    {
        Debug.Assert(mover != null);

        mover.TargetAchieved += HandleTargetAchieved;
    }

    private void Update()
    {
        if (projectileType == ProjectileType.FollowTarget)
            mover.SetTargetPosition(selectedTarget.position);
    }

    private void OnDisable()
    {
        mover.TargetAchieved -= HandleTargetAchieved;
    }

    private void HandleTargetAchieved()
    {
        gameObject.SetActive(false);
    }
}

public enum ProjectileType
{
    StaticTarget,
    FollowTarget
}