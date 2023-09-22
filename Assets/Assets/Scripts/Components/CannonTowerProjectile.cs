using UnityEngine;

public class CannonTowerProjectile : TowerProjectile
{
    [SerializeField]
    private float maxHeight;

    [SerializeField]
    private Transform animatedProjectile;

    [SerializeField]
    private AnimationCurve gravityAnimationCurve;

    private float time;
    private float passedTime;
    private bool animationStarted;

    protected override void OnAfterSetPosition()
    {
        StartAnimation();
    }

    private void StartAnimation()
    {
        time = Mover.CalculateTimeToPosition(transform.position, Mover.TargetPosition.Value);
        passedTime = 0f;
        animationStarted = true;
    }

    private void Update()
    {
        if (animationStarted)
        {
            var passedTimeInverseLerp = Mathf.InverseLerp(0, time, passedTime);

            if (passedTimeInverseLerp == 1)
            {
                animationStarted = false;
                return;
            }

            passedTime += Time.deltaTime;

            var animationPoint = gravityAnimationCurve.Evaluate(passedTimeInverseLerp);
            var height = Mathf.Lerp(0, maxHeight, animationPoint);

            var currentPosition = animatedProjectile.transform.localPosition;
            currentPosition.y = height;

            animatedProjectile.transform.localPosition = currentPosition;
        }
    }
}