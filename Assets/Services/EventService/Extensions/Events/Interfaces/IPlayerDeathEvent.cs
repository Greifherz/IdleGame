using Game.Data;

namespace Services.EventService
{
    public interface IPlayerDeathEvent : IEvent
    {
        IPlayerCharacter PlayerCharacter { get; }
    }
}