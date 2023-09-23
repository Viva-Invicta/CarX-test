using System;
using TowerDefence.Utilities;
using UnityEngine;

namespace TowerDefence.Components
{
    public class TranslationMover : MonoBehaviour
    {
        private const float AchieveTreshold = 0.2f;

        public event Action TargetAchieved;

        private float? speedPerSecond;

        [SerializeField]
        private float speed;

        public float SpeedPerSecond
        {
            get
            {
                if (speedPerSecond == null)
                {
                    var translationsInSecond = 1 / Time.fixedDeltaTime;
                    speedPerSecond = speed * translationsInSecond;
                }
                return speedPerSecond.Value;
            }
        }

        public Vector3? TargetPosition
        {
            get;
            private set;
        }

        public void SetTargetPosition(Vector3? targetPosition)
        {
            TargetPosition = targetPosition;
        }

        private void FixedUpdate()
        {
            if (TargetPosition.HasValue)
            {
                var vectorToTargetMagnitude = (TargetPosition.Value - transform.position).magnitude;

                if (vectorToTargetMagnitude < AchieveTreshold)
                {
                    TargetAchieved?.Invoke();
                    return;
                }

                var translation = TranslationCalculator.CalculateTranslation(transform.position, TargetPosition.Value, speed);
                transform.Translate(translation);
            }
        }
    }
}