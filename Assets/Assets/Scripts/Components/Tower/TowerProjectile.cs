using UnityEngine;

namespace TowerDefence.Components
{
    public class TowerProjectile : MonoBehaviour
    {
        private TowerTarget followTarget;

        [SerializeField]
        private Animation animation;

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
            followTarget = target;

            if (followTarget)
                followTarget.Disabled += OnTargetDisabled;
        }

        public void GoToPosition(Vector3 position)
        {
            followTarget = null;

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
            
            followTarget.Disabled -= OnTargetDisabled;
            GoToPosition(followTarget.transform.position);
        }

        private void HandleTargetAchieved()
        {
            if (animation)
                animation.StopAnimation();

            gameObject.SetActive(false);
        }
    }
}