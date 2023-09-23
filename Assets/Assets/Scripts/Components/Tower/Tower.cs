using System.Collections;
using UnityEngine;

namespace TowerDefence.Components
{
    public class Tower : MonoBehaviour
    {
        [SerializeField]
        private float shootInterval = 0.5f;

        [SerializeField]
        private Weapon weapon;

        [SerializeField]
        private TowerTargetSelector targetSelector;

        private void OnEnable()
        {
            Debug.Assert(targetSelector);
            targetSelector.TargetUpdated += OnTargetUpdated;
        }

        private void Start()
        {
            Debug.Assert(weapon);
            StartCoroutine(Shoot());
        }

        private void OnDisable()
        {
            targetSelector.TargetUpdated -= OnTargetUpdated;
        }

        private void OnTargetUpdated(TowerTarget target)
        {
            weapon.SetTarget(target);
        }

        private IEnumerator Shoot()
        {
            while (true)
            {
                weapon.Shoot();

                yield return new WaitForSecondsRealtime(shootInterval);
            }
        }
    }
}