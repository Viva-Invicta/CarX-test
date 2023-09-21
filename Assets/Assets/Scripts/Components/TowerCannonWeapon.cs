using UnityEngine;

public class TowerCannonWeapon : TowerWeapon
{
    [SerializeField]
    private WeaponRotator weaponRotator;

    private void Update()
    {
        if (selectedTarget)
            weaponRotator.SetTargetPosition(GetPreemptiveShootingPosition());
    }

    protected override void ShootInternal()
    {
        if (selectedTarget && weaponRotator.IsLookingAtTarget)
        {
            var projectile = projectilesPool.GetInstance();
            projectile.transform.position = shootPoint.position;

            projectile.GoToPosition(GetPreemptiveShootingPosition());
        }
    }

    private Vector3 GetPreemptiveShootingPosition()
    {
        var timeToTargetCurrentPosition = projectilePrefab.Mover.CalculateTimeToPosition(shootPoint.position, selectedTarget.transform.position);

        var targetFuturePosition = selectedTarget.Mover.CalculateFuturePosition(timeToTargetCurrentPosition);
        var timeToFuturePosition = projectilePrefab.Mover.CalculateTimeToPosition(shootPoint.position, targetFuturePosition);

        targetFuturePosition = selectedTarget.Mover.CalculateFuturePosition(timeToFuturePosition);

        return targetFuturePosition;
    }
}