using System;
using TowerDefence.Events;
using UnityEngine;

namespace TowerDefence.Components
{
    public class TowerTarget : MonoBehaviour
    {
        public event Action Disabled;

        [field: SerializeField]
        public TranslationMover Mover
        {
            get;
            private set;
        }

        private void OnEnable()
        {
            StaticEventBus.RaiseEvent(new TowerTargetStatusUpdatedEvent(this, true));
        }

        private void OnDisable()
        {
            Disabled?.Invoke();

            StaticEventBus.RaiseEvent(new TowerTargetStatusUpdatedEvent(this, false));
        }
    }
}