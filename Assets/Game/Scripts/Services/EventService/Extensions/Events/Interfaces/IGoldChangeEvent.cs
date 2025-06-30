namespace Services.EventService
{
    public interface IGoldChangeEvent : IEvent
    {
        int GoldQuantity { get; }
    }
}