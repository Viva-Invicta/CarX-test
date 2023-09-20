using System.Collections.Generic;
using UnityEngine;

public class TowerTargetsService : IService
{
    private HashSet<TowerTarget> availableTargets;

    public void Initialize()
    {
        availableTargets = new HashSet<TowerTarget>();
        EventBus.Subscribe<TowerTargetStatusUpdatedEvent>(HandleTargetUpdated);
    }

    public TowerTarget GetNearestTargetFromPosition(Vector3 position, out float sqrDistance)
    {
        TowerTarget nearestTarget = null;
        sqrDistance = float.MaxValue;

        foreach (var target in availableTargets) 
        {
            var sqrDistanceToAvailableTarget = (position - target.transform.position).sqrMagnitude;

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

    private void HandleTargetAdded(TowerTarget target)
    {
        if (!availableTargets.Contains(target))
            availableTargets.Add(target);
    }

    private void HandleTargetRemoved(TowerTarget target)
    {
        if (availableTargets.Contains(target))
            availableTargets.Remove(target);    
    }
}