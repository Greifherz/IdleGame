namespace Services.EventService
{
    public interface IApplicationPauseUnityEvent : IEvent
    {
        bool PauseStatus { get; }
    }
}