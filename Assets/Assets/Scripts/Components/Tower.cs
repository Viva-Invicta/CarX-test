using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private float shootInterval = 0.5f;

    [SerializeField]
    private TowerWeapon weapon;

    [SerializeField]
    private TowerTargetSelector targetSelector;

    private void Start()
    {
        Debug.Assert(weapon);
        Debug.Assert(targetSelector);

        targetSelector.TargetUpdated += OnTargetUpdated;

        StartCoroutine(Shoot());
    }

    private void OnDisable()
    {
        targetSelector.TargetUpdated -= OnTargetUpdated;
    }

    private void OnTargetUpdated(Transform target)
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