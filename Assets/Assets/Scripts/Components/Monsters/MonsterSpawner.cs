using System.Collections;
using UnityEngine;

namespace TowerDefence.Components
{
    public class MonsterSpawner : MonoBehaviour
    {
        private const int PoolCapacity = 20;

        [SerializeField]
        private float interval;

        [SerializeField]
        private Transform spawnPlacement;

        [SerializeField]
        private Transform moveTarget;

        [SerializeField]
        private Monster monsterPrefab;

        private MonoBehaviourObjectPool<Monster> monsterPool;

        private void OnEnable()
        {
            monsterPool = new MonoBehaviourObjectPool<Monster>(PoolCapacity, transform, monsterPrefab);
        }

        public void Start()
        {
            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            while (true)
            {
                var newMonster = monsterPool.GetInstance();

                newMonster.transform.position = spawnPlacement.position;
                newMonster.Mover.SetTargetPosition(moveTarget.position);

                yield return new WaitForSecondsRealtime(interval);
            }
        }
    }
}