namespace Services.EventService
{
    public interface IMinerGoldCollectEvent : IEvent
    {
        int GoldQuantity { get; }
    }
}