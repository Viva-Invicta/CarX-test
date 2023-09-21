public class TowerGuidedWeapon : TowerWeapon
{
    protected override void ShootInternal()
    {
        if (selectedTarget)
        {
            var projectileInstance = projectilesPool.GetInstance();
            projectileInstance.transform.position = shootPoint.position;

            projectileInstance.FollowTarget(selectedTarget);
        }
    }
}