namespace Services.EventService
{
    public interface ITransitionEvent : IEvent
    {
        TransitionTarget Target { get; }
    }

    public enum TransitionTarget
    {
        Back,
        Lobby,
        Mining,
        ArmyView,
    }
}