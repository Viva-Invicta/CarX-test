using System;
using UnityEngine;

public abstract class Mover : MonoBehaviour
{
    public abstract event Action TargetAchieved;

    public abstract void SetTargetPosition(Vector3? targetPosition);
}