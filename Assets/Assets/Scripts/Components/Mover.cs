using System;
using UnityEngine;

public abstract class Mover : MonoBehaviour
{
    [field: SerializeField]
    protected float speed;

    public abstract Vector3? TargetPosition { get; protected set; }

    public abstract event Action TargetAchieved;

    public abstract Vector3 CalculateFuturePosition(float seconds);
    public abstract float CalculateTimeToPosition(Vector3 startPosition, Vector3 position);
    public abstract void SetTargetPosition(Vector3? targetPosition);
}