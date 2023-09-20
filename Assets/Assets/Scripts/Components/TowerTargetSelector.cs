using System;
using System.Collections;
using UnityEngine;

public class TowerTargetSelector : MonoBehaviour
{
    public event Action<TowerTarget> TargetUpdated;

    [SerializeField]
    private float range = 4f;

    [SerializeField]
    private float targetCheckFrequency = 0.1f;

    private float rangeSqr;
    private TowerTargetsService towerTargetsService;
    private TowerTarget selectedTarget;

    private void OnEnable()
    {
        rangeSqr = range * range;

        if (StaticServiceLocator.IsInitialized)
            towerTargetsService = StaticServiceLocator.GetService<TowerTargetsService>();
        else
            StaticServiceLocator.Initialized += OnServiceLocatorInitialized;
    }

    private void Start()
    {
        StartCoroutine(TargetSelection());
    }

    private void OnServiceLocatorInitialized()
    {
        towerTargetsService = StaticServiceLocator.GetService<TowerTargetsService>();
    }

    private IEnumerator TargetSelection()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(targetCheckFrequency);

            if (towerTargetsService == null)
                continue;

            var newTarget = towerTargetsService.GetNearestTargetFromPosition(transform.position, out var distanceSqr);

            if (newTarget != selectedTarget &&  (distanceSqr < rangeSqr || !newTarget))
            {
                selectedTarget = newTarget;
                TargetUpdated?.Invoke(selectedTarget);  
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}