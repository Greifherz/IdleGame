namespace Game.GameFlow
{
    public interface IGameFlowState
    {
        GameFlowStateType Type { get; }

        void StateExit();
        void StateEnter();
        bool CanTransitionTo(GameFlowStateType type);
        IGameFlowState TransitionTo(GameFlowStateType type);

        GameFlowStateType GetBackState();
    }
}