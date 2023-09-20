using System.Collections.Generic;
using UnityEngine;

public class TowerTargetsService : IService
{
    private HashSet<Transform> availableTargets;

    public void Initialize()
    {
        availableTargets = new HashSet<Transform>();
        EventBus.Subscribe<TowerTargetStatusUpdatedEvent>(HandleTargetUpdated);
    }

    public Transform GetNearestTargetFromPosition(Vector3 position, out float sqrDistance)
    {
        Transform nearestTarget = null;
        sqrDistance = float.MaxValue;

        foreach (var target in availableTargets) 
        {
            var sqrDistanceToAvailableTarget = (position - target.position).sqrMagnitude;

            if (sqrDistanceToAvailableTarget < sqrDistance)
            {
                nearestTarget = target;
                sqrDistance = sqrDistanceToAvailableTarget;
            }
        }
        return nearestTarget;
    }

    private void HandleTargetUpdated(TowerTargetStatusUpdatedEvent targetUpdatedEvent)
    {
        var target = targetUpdatedEvent.Target;

        if (target)
        {
            if (targetUpdatedEvent.IsAvailable)
            {
                HandleTargetAdded(target);
            }
            else
            { 
                HandleTargetRemoved(target);
            }
        } 
    }

    private void HandleTargetAdded(Transform target)
    {
        if (!availableTargets.Contains(target))
            availableTargets.Add(target);
    }

    private void HandleTargetRemoved(Transform target)
    {
        if (availableTargets.Contains(target))
            availableTargets.Remove(target);    
    }
}