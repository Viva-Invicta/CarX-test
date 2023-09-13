using System;
using UnityEngine;

namespace TowerDefence
{
    public class PoolableComponent<TMainComponent> : MonoBehaviour
        where TMainComponent : MonoBehaviour
    {
        public event Action<PoolableComponent<TMainComponent>> Enabled;
        public event Action<PoolableComponent<TMainComponent>> Disabled;

        public TMainComponent CachedComponent => cachedComponent;

        private TMainComponent cachedComponent;

        private void Awake()
        {
            if (!cachedComponent)
                cachedComponent = GetComponent<TMainComponent>();

            Debug.Assert(cachedComponent);
        }

        private void OnEnable()
        {
            Enabled?.Invoke(this);
        }

        private void OnDisable()
        {
            Disabled?.Invoke(this);
        }
    }
}