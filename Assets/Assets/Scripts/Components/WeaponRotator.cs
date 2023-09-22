using UnityEngine;

public class WeaponRotator : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private Transform weapon;

    [SerializeField]
    private float targetAngle = 5f;

    private Vector3? targetPosition;
    
    public bool IsLookingAtTarget
    {
        get
        {
            if (!targetPosition.HasValue)
                return false;

            var targetDirection = targetPosition.Value - weapon.transform.position;
            var angle = Vector3.Angle(weapon.transform.forward, targetDirection);

            return angle <= targetAngle;
        }
    }

    private void FixedUpdate()
    {
        if (!targetPosition.HasValue)
            return;

        var targetDirection = targetPosition.Value - weapon.transform.position;

        var targetRotation = Quaternion.LookRotation(targetDirection);
        var newRotationY = Quaternion.RotateTowards(weapon.transform.rotation, targetRotation, rotationSpeed).eulerAngles.y;

        var rotation = weapon.transform.rotation.eulerAngles;
        rotation.y = newRotationY;

        weapon.transform.rotation = Quaternion.Euler(rotation);
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}