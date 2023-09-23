using UnityEngine;

namespace TowerDefence.Components
{
    public class Monster : MonoBehaviour
    {
        [SerializeField]
        private TranslationMover mover;

        [SerializeField]
        private HealthPoints healthPoints;

        public TranslationMover Mover => mover;

        private void OnEnable()
        {
            mover.TargetAchieved += OnTargetAchieved;
            healthPoints.HPExpired += OnHealthPointsExpired;
        }

        private void OnDisable()
        {
            mover.TargetAchieved -= OnTargetAchieved;
            healthPoints.HPExpired -= OnHealthPointsExpired;
        }

        private void OnHealthPointsExpired()
        {
            gameObject.SetActive(false);
        }

        private void OnTargetAchieved()
        {
            gameObject.SetActive(false);
        }
    }
}