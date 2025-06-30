namespace Services.EventService
{
    public interface IApplicationFocusUnityEvent : IEvent
    {
        bool HasFocus { get; }
    }
}