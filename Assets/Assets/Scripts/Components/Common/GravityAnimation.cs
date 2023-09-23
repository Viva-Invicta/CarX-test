using UnityEngine;

namespace TowerDefence.Components
{
    public class GravityAnimation : Animation
    {
        [SerializeField]
        private float maxHeight;

        [SerializeField]
        private AnimationCurve gravityAnimationCurve;

        [SerializeField]
        private Transform animationTarget;

        protected override void Process(float deltaTime)
        {
            var passedTimePart = Mathf.InverseLerp(0, duration, passedTime);

            if (passedTimePart == 1)
            {
                StopAnimation();
                return;
            }

            passedTime += deltaTime;

            var animationPoint = gravityAnimationCurve.Evaluate(passedTimePart);
            var height = Mathf.Lerp(0, maxHeight, animationPoint);

            var currentPosition = animationTarget.transform.localPosition;
            currentPosition.y = height;

            animationTarget.transform.localPosition = currentPosition;
        }
    }
}