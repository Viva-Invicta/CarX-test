using System;
using UnityEngine;

namespace TowerDefence
{
    public class Poolable : MonoBehaviour
    {
        public event Action<Poolable> Enabled;
        public event Action<Poolable> Disabled;

        public MonoBehaviour CachedComponent { get; private set; }

        public void CacheComponent<T>()
            where T : MonoBehaviour
        {
            CachedComponent = GetComponent<T>();

            Debug.Assert(CachedComponent);
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