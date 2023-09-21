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

        var newRotation = Quaternion.LookRotation(targetDirection);
        weapon.transform.rotation = Quaternion.RotateTowards(weapon.transform.rotation, newRotation, rotationSpeed);
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}