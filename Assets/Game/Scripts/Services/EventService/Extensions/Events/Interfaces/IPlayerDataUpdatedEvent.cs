using Game.Data;

namespace Services.EventService
{
    public interface IPlayerDataUpdatedEvent : IEvent
    {
        IPlayerCharacter PlayerCharacter { get; }
    }
}