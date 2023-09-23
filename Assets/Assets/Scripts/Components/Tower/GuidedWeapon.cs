namespace TowerDefence.Components
{
    public class GuidedWeapon : Weapon
    {
        protected override void ShootInternal()
        {
            var projectileInstance = projectilesPool.GetInstance();
            projectileInstance.transform.position = shootPoint.position;

            projectileInstance.FollowTarget(selectedTarget);
        }
    }
}