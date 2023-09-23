using UnityEngine;

namespace TowerDefence.Utilities
{
    public static class PreemptiveShootingUtility
    {
        public static Vector3 GetPredictedTargetPosition(
            float projectileSpeedPerSecond,
            float targetSpeedPerSecond,
            Vector3 projectileStartPosition,
            Vector3 targetStartPosition,
            Vector3 targetEndPosition,
            int iterations)
        {
            var targetFuturePosition = targetStartPosition;

            var projectileTimeToPosition = CalculateMovingObjectTimeToPosition(
                projectileStartPosition,
                targetFuturePosition,
                projectileSpeedPerSecond);

            for (var i = 0; i < iterations; i++)
            {
                targetFuturePosition = CalculateMovingObjectPositionBySeconds(
                    targetStartPosition,
                    targetEndPosition,
                    targetSpeedPerSecond,
                    projectileTimeToPosition);

                projectileTimeToPosition = CalculateMovingObjectTimeToPosition(
                    projectileStartPosition,
                    targetFuturePosition,
                    projectileSpeedPerSecond);
            }

            return targetFuturePosition;
        }

        private static Vector3 CalculateMovingObjectPositionBySeconds(
            Vector3 startPosition,
            Vector3 targetPosition,
            float speedPerSecond,
            float seconds)
        {
            var translation = TranslationCalculator.CalculateTranslation(startPosition, targetPosition, speedPerSecond, true);
            return startPosition + translation * seconds;
        }

        private static float CalculateMovingObjectTimeToPosition(
            Vector3 startPosition,
            Vector3 targetPosition,
            float speedPerSecond)
        {
            var vectorToPosition = targetPosition - startPosition;
            var time = vectorToPosition.magnitude / speedPerSecond;

            return time;
        }
    }
}