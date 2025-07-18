using System.Collections.Generic;
using Game.Gameplay;
using Services.EventService;

namespace Game.GameFlow
{
    public class GameFlowStateFactory
    {
        private readonly Dictionary<GameFlowStateType, IGameFlowState> _stateCache;
        private readonly IEventService _eventService;

        public GameFlowStateFactory(IEventService eventService)
        {
            _eventService = eventService;
            _stateCache = new Dictionary<GameFlowStateType, IGameFlowState>();
        }

        public IGameFlowState GetState(GameFlowStateType stateType)
        {
            // If we already created this state, return the cached instance.
            if (_stateCache.TryGetValue(stateType, out var cachedState))
            {
                return cachedState;
            }

            // Otherwise, create a new instance based on the type.
            IGameFlowState newState = CreateNewState(stateType);

            // Add it to the cache for next time.
            _stateCache.Add(stateType, newState);

            return newState;
        }

        private IGameFlowState CreateNewState(GameFlowStateType stateType)
        {
            switch (stateType)
            {
                case GameFlowStateType.Lobby:
                    return new GameFlowLobbyState(this,_eventService);
                case GameFlowStateType.Mining:
                    return new GameFlowMiningState(this,_eventService);
                case GameFlowStateType.ArmyView:
                    return new GameFlowArmyState(this,_eventService);
                case GameFlowStateType.Start:
                    return new GameStartState(this,_eventService);
                case GameFlowStateType.Battle:
                    return new GameFlowBattleState(this,_eventService);
                default:
                    throw new System.ArgumentOutOfRangeException(nameof(stateType), stateType, "No state definition exists for this type.");
            }
        }
    }
}