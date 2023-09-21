using UnityEngine;

public class WeaponRotator : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private Transform weapon;

    [SerializeField]
    private float targetAngle = 5f;

    private TowerTarget selectedTarget;
    
    public bool IsLookingAtTarget
    {
        get
        {
            if (!selectedTarget)
                return false;

            var targetDirection = selectedTarget.transform.position - weapon.transform.position;
            var angle = Vector3.Angle(weapon.transform.forward, targetDirection);

            return angle <= targetAngle;
        }
    }

    private void FixedUpdate()
    {
        if (!selectedTarget)
            return;

        var targetDirection = selectedTarget.transform.position - weapon.transform.position;

        var newRotation = Quaternion.LookRotation(targetDirection);
        weapon.transform.rotation = Quaternion.RotateTowards(weapon.transform.rotation, newRotation, rotationSpeed);
    }

    public void SetTarget(TowerTarget target)
    {
        selectedTarget = target;
    }
}