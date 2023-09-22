using UnityEngine;

public class TowerProjectile : MonoBehaviour
{
    private TowerTarget followTarget;

    [field: SerializeField] public Mover Mover { get; private set; }

    public void FollowTarget(TowerTarget target)
    {
        followTarget = target;
    }

    public void GoToPosition(Vector3 position)
    {
        followTarget = null;

        Mover.SetTargetPosition(position);

        OnAfterSetPosition();
    }

    protected virtual void OnAfterSetPosition() { }

    private void OnEnable()
    {
        Debug.Assert(Mover != null);

        Mover.TargetAchieved += HandleTargetAchieved;
    }

    private void Update()
    {
        if (followTarget)
            Mover.SetTargetPosition(followTarget.transform.position);
    }

    private void OnDisable()
    {
        Mover.TargetAchieved -= HandleTargetAchieved;
    }

    private void HandleTargetAchieved()
    {
        gameObject.SetActive(false);
    }
}