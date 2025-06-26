namespace Services.EventService
{
    public interface IMinerGoldAccumulatedEvent : IEvent
    {
        int GoldQuantity { get; }
    }
}