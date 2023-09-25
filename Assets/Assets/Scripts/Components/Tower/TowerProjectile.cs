using System;
using UnityEngine;

namespace TowerDefence.Components
{
    public class TowerProjectile : MonoBehaviour
    {
        public event Action TargetAchieved;

        [SerializeField]
        private Animation animation;

        private TowerTarget followTarget;

        [field: SerializeField]
        public Transform Visual
        {
            get;
            private set;
        }

        [field: SerializeField]
        public TranslationMover Mover
        {
            get;
            private set;
        }

        private void OnEnable()
        {
            Debug.Assert(Mover != null);

            Mover.TargetAchieved += HandleTargetAchieved;
        }

        private void FixedUpdate()
        {
            if (followTarget)
                Mover.SetTargetPosition(followTarget.transform.position);
        }

        private void OnDisable()
        {
            Mover.TargetAchieved -= HandleTargetAchieved;
        }

        public void FollowTarget(TowerTarget target)
        {
            if (followTarget)
                followTarget.Disabled -= OnTargetDisabled;

            followTarget = target;

            if (followTarget)
                followTarget.Disabled += OnTargetDisabled;
        }

        public void GoToPosition(Vector3 position)
        {
            if (followTarget)
            {
                followTarget.Disabled -= OnTargetDisabled;
                followTarget = null;
            }

            Mover.SetTargetPosition(position);

            if (animation)
            {
                var timeToPosition = (position - transform.position).magnitude / Mover.SpeedPerSecond;
                animation.StartAnimation(timeToPosition);
            }
        }

        private void OnTargetDisabled()
        {
            if (!followTarget)
                return;
            
            GoToPosition(followTarget.transform.position);
        }

        private void HandleTargetAchieved()
        {
            if (animation)
                animation.StopAnimation();

            TargetAchieved?.Invoke();
            gameObject.SetActive(false);
        }
    }
}