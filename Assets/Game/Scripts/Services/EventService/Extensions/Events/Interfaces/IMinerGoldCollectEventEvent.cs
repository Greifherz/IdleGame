namespace Services.EventService
{
    public interface IMinerGoldCollectEventEvent : IEvent
    {
        int GoldQuantity { get; }
    }
}