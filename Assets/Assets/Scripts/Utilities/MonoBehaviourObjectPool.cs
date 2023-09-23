using System.Collections.Generic;
using System.Linq;
using TowerDefence.Components;
using UnityEngine;

namespace TowerDefence
{
    public class MonoBehaviourObjectPool<TMainComponent>
        where TMainComponent : MonoBehaviour
    {
        private readonly int capacity;
        private readonly Transform parent;
        private readonly TMainComponent objectPrefab;

        private HashSet<Poolable> availableInstances;

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

            for (var i = 0; i < capacity; i++)
            {
                AddNewInstance();
            }
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

        private void OnInstanceEnabled(Poolable instance)
        {
            availableInstances.Remove(instance);
        }

        private void OnInstanceDisabled(Poolable instance)
        {
            availableInstances.Add(instance);
        }
    }
}
