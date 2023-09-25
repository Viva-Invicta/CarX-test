using TowerDefence.Utilities;
using UnityEngine;

namespace TowerDefence.Components
{
    public class CannonWeapon : Weapon
    {
        private const int WeaponRotationPredictAccuracy = 1;
        private const int ShootPositionPredictAccuracy = 6;

        [SerializeField]
        private WeaponRotator weaponRotator;

        private void Update()
        {
            if (selectedTarget && weaponRotator)
            {
                weaponRotator.SetTargetPosition(GetPredictedTargetPosition(WeaponRotationPredictAccuracy));
            }
        }

        protected override TowerProjectile ShootInternal()
        {
            if (weaponRotator.IsLookingAtTargetPosition)
            {
                var projectile = projectilesPool.GetInstance();
                projectile.transform.position = shootPoint.position;

                projectile.GoToPosition(GetPredictedTargetPosition(ShootPositionPredictAccuracy));

                return projectile;
            }

            return null;
        }

        private Vector3 GetPredictedTargetPosition(int accuracy)
        {
            var projectileMover = projectilePrefab.Mover;
            var targetMover = selectedTarget.Mover;

            if (!targetMover.TargetPosition.HasValue)
                return targetMover.transform.position;

            return PreemptiveShootingUtility.GetPredictedTargetPosition(
               projectileMover.SpeedPerSecond,
               targetMover.SpeedPerSecond,
               shootPoint.position,
               targetMover.transform.position,
               targetMover.TargetPosition.Value,
               accuracy);
        }
    }
}