using TowerDefence.Components;

namespace TowerDefence.Events
{
    public record TowerTargetStatusUpdatedEvent(TowerTarget Target, bool IsAvailable)
    {
        public TowerTarget Target { get; } = Target;
        public bool IsAvailable { get; } = IsAvailable;
    }
}