using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TowerDefence
{
    public class MonoBehaviourObjectPool<TMainComponent>
        where TMainComponent : MonoBehaviour
    {
        private readonly TMainComponent objectPrefab;
        private readonly int capacity;
        private readonly Transform parent;

        private HashSet<Poolable> availableInstances;
        private HashSet<Poolable> lockedInstances;

        public MonoBehaviourObjectPool(int capacity, Transform parent, TMainComponent objectPrefab)
        {
            this.objectPrefab = objectPrefab;
            this.capacity = capacity;
            this.parent = parent;

            Initialize();
        }

        private void Initialize()
        {
            Debug.Assert(objectPrefab);
            Debug.Assert(capacity > 0);
            Debug.Assert(parent);

            availableInstances = new HashSet<Poolable>(capacity);
            lockedInstances = new HashSet<Poolable>(capacity);

            for (var i = 0; i < capacity; i++)
            {
                AddNewInstance();
            }
        }

        public void Release()
        {
            foreach (var instance in availableInstances)
                DestroyInstance(instance);

            foreach (var instance in lockedInstances)
                DestroyInstance(instance);

            availableInstances = null;
            lockedInstances = null;
        }

        public TMainComponent GetInstance()
        {
            TMainComponent instance;
            if (availableInstances.Count == 0)
                instance = AddNewInstance().CachedComponent as TMainComponent;
            else
                instance = availableInstances.First().CachedComponent as TMainComponent;

            instance.gameObject.SetActive(true);

            return instance;
        }

        private Poolable AddNewInstance()
        {
            var instance = Object.Instantiate(objectPrefab).gameObject;
            var poolableComponent = instance.AddComponent<Poolable>();

            poolableComponent.CacheComponent<TMainComponent>();

            poolableComponent.Enabled += OnInstanceEnabled;
            poolableComponent.Disabled += OnInstanceDisabled;

            instance.SetActive(false);
            instance.gameObject.transform.SetParent(parent, worldPositionStays: false);

            availableInstances.Add(poolableComponent);

            return poolableComponent;
        }

        private void DestroyInstance(Poolable instance)
        {
            instance.Enabled -= OnInstanceEnabled;
            instance.Disabled -= OnInstanceDisabled;

            Object.Destroy(instance.gameObject);
        }

        private void OnInstanceEnabled(Poolable instance)
        {
            availableInstances.Remove(instance);
            lockedInstances.Add(instance);
        }

        private void OnInstanceDisabled(Poolable instance)
        {
            lockedInstances.Remove(instance);
            availableInstances.Add(instance);
        }
    }
}
