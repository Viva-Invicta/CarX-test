using UnityEngine;

namespace TowerDefence.Utilities
{
    public static class TranslationCalculator
    {
        public static Vector3 CalculateTranslation(
            Vector3 startPosition, Vector3 targetPosition, float translationMagnitude, bool allowTranslationOverTarget = false)
        {
            var vectorToTarget = targetPosition - startPosition;

            if (!allowTranslationOverTarget && translationMagnitude > vectorToTarget.magnitude)
                return vectorToTarget;

            var translation = translationMagnitude * vectorToTarget.normalized;

            return translation;
        }
    }
}