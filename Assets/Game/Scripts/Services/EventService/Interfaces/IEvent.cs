namespace Services.EventService
{
    public interface IEvent
    {
        void Visit(IEventHandler handler);
    }
}
