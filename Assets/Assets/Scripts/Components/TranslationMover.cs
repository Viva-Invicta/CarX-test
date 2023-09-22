using System;
using UnityEngine;

public class TranslationMover : Mover
{
    private const float AchieveTreshold = 0.05f;

    public override event Action TargetAchieved;

    public override Vector3? TargetPosition { get; protected set; }

    public override void SetTargetPosition(Vector3? targetPosition)
    {
        this.TargetPosition = targetPosition;
    }

    private void FixedUpdate()
    {
        if (TargetPosition.HasValue)
        {
            var vectorToTarget = TargetPosition.Value - transform.position;
            var vectorToTargetMagnitude = vectorToTarget.magnitude;

            if (vectorToTargetMagnitude < AchieveTreshold)
            {
                TargetAchieved?.Invoke();
                return;
            }

            var translation = vectorToTarget.normalized * speed;

            if (translation.magnitude > vectorToTargetMagnitude)
            {
                TargetAchieved?.Invoke();
                return;
            }

            transform.Translate(translation);
        }
    }

    public override Vector3 CalculateFuturePosition(float seconds)
    {
        var translationsInSecond = 1f / Time.fixedDeltaTime;

        var vectorToTarget = TargetPosition.Value - transform.position;
        var translation = vectorToTarget.normalized * speed;

        var futurePosition = transform.position + translation * translationsInSecond * seconds;

        return futurePosition;
    }

    public override float CalculateTimeToPosition(Vector3 startPosition, Vector3 targetPosition)
    {
        var translationsInSecond = 1f / Time.fixedDeltaTime;

        var vectorToPosition = targetPosition - startPosition;
        var translation = vectorToPosition.normalized * speed;
        var translationMagnitude = translation.magnitude;

        var speedPerSecond = translationMagnitude * translationsInSecond;
        var time = vectorToPosition.magnitude / speedPerSecond;

        return time;
    }
}