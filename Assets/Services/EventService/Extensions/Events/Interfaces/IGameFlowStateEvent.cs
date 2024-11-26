using Game.GameFlow;

namespace Services.EventService
{
    public interface IGameFlowStateEvent : IEvent
    {
        GameFlowStateType GameFlowStateType { get; }
    }
}