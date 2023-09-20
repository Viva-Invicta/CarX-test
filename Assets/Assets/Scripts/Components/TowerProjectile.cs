using UnityEngine;

public class TowerProjectile : MonoBehaviour
{
    [SerializeField]
    private Mover mover;

    [SerializeField]
    private ProjectileType projectileType;

    private TowerTarget selectedTarget;

    public void SetTarget(TowerTarget target)
    {
        selectedTarget = target;

        if (selectedTarget != null)
        {
            if (projectileType == ProjectileType.StaticTarget)
            {
                var timeToTargetCurrentPosition = mover.CalculateTimeToPosition(selectedTarget.transform.position);

                var targetFuturePosition = target.Mover.CalculateFuturePosition(timeToTargetCurrentPosition);
                var timeToFuturePosition = mover.CalculateTimeToPosition(targetFuturePosition);

                targetFuturePosition = target.Mover.CalculateFuturePosition(timeToFuturePosition);

                mover.SetTargetPosition(targetFuturePosition);
            }
            else
            {
                mover.SetTargetPosition(selectedTarget.transform.position);
            }
        }
        else
        {
            mover.SetTargetPosition(null);
        }
    }

    private void OnEnable()
    {
        Debug.Assert(mover != null);

        mover.TargetAchieved += HandleTargetAchieved;
    }

    private void Update()
    {
        if (projectileType == ProjectileType.FollowTarget && selectedTarget)
            mover.SetTargetPosition(selectedTarget.transform.position);
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