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

        private HashSet<PoolableComponent<TMainComponent>> availableInstances;
        private HashSet<PoolableComponent<TMainComponent>> lockedInstances;

        public MonoBehaviourObjectPool(int capacity, TMainComponent objectPrefab)
        {
            this.objectPrefab = objectPrefab;
            this.capacity = capacity;
        }

        public void Initialize()
        {
            Debug.Assert(objectPrefab);
            Debug.Assert(capacity > 0);

            availableInstances = new HashSet<PoolableComponent<TMainComponent>>(capacity);
            lockedInstances = new HashSet<PoolableComponent<TMainComponent>>(capacity);

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
            if (availableInstances.Count == 0)
                return AddNewInstance().CachedComponent;
            else
                return availableInstances.First().CachedComponent;
        }

        private PoolableComponent<TMainComponent> AddNewInstance()
        {
            var instance = Object.Instantiate(objectPrefab).gameObject;
            var poolableComponent = instance.AddComponent<PoolableComponent<TMainComponent>>();

            instance.SetActive(false);

            poolableComponent.Enabled += OnInstanceEnabled;
            poolableComponent.Disabled += OnInstanceDisabled;

            availableInstances.Add(poolableComponent);

            return poolableComponent;
        }

        private void DestroyInstance(PoolableComponent<TMainComponent> instance)
        {
            instance.Enabled -= OnInstanceEnabled;
            instance.Disabled -= OnInstanceDisabled;

            Object.Destroy(instance.gameObject);
        }

        private void OnInstanceEnabled(PoolableComponent<TMainComponent> instance)
        {
            availableInstances.Remove(instance);
            lockedInstances.Add(instance);
        }

        private void OnInstanceDisabled(PoolableComponent<TMainComponent> instance)
        {
            lockedInstances.Remove(instance);
            availableInstances.Add(instance);
        }
    }
}
