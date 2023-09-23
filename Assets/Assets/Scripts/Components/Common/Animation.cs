using UnityEngine;

namespace TowerDefence.Components
{
    public abstract class Animation : MonoBehaviour
    {
        protected float duration;
        protected float passedTime;
        private bool isStarted;

        private void Update()
        {
            if (isStarted)
                Process(Time.deltaTime);
        }

        public void StartAnimation(float duration)
        {
            this.duration = duration;
            isStarted = true;
        }

        public void StopAnimation()
        {
            duration = 0;
            passedTime = 0;
            isStarted = false;
        }

        protected abstract void Process(float deltaTime);
    }
}