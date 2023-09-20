using System;
using UnityEngine;

public class TranslationMover : Mover
{
    private const float AchieveTreshold = 0.01f;

    public override event Action TargetAchieved;

    [SerializeField]
    private float speed;

    private Vector3? targetPosition = null;

    public override void SetTargetPosition(Vector3? targetPosition)
    {
        this.targetPosition = targetPosition;
    }

    private void Update()
    {
        if (targetPosition.HasValue)
        {
            var translation = targetPosition.Value - transform.position;
            var magnitude = translation.magnitude;

            if (magnitude < AchieveTreshold)
            {
                TargetAchieved?.Invoke();
                return;
            }

            translation = translation.normalized * speed * Time.deltaTime;
            transform.Translate(translation);
        }
    }
}