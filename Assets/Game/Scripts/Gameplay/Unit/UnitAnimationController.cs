namespace Game.Gameplay
{
    public class UnitAnimationController
    {
        public enum UnitAnimationState { Idle, Move, Attack }
    
        private readonly SpriteAnimationPlayer _animationPlayer;
        private readonly AnimationDatabase _animationDatabase;
        private UnitAnimationState _currentState;
        private bool _isAttackLocked; // Prevents interrupting an attack animation

        public UnitAnimationController(SpriteAnimationPlayer player, AnimationDatabase database)
        {
            _animationPlayer = player;
            _animationDatabase = database;
            SetState(UnitAnimationState.Idle);
        }

        public void SetState(UnitAnimationState newState)
        {
            if (_currentState == newState || _isAttackLocked) return;

            _currentState = newState;
            switch (_currentState)
            {
                case UnitAnimationState.Idle:
                    _animationPlayer.SetAnimation(_animationDatabase.GetAnimationData(AnimationType.Idle));
                    _animationPlayer.PlayAnimation();
                    break;
                case UnitAnimationState.Move:
                    _animationPlayer.SetAnimation(_animationDatabase.GetAnimationData(AnimationType.Move));
                    _animationPlayer.PlayAnimation();
                    break;
                case UnitAnimationState.Attack:
                    _isAttackLocked = true;
                    _animationPlayer.SetAnimation(_animationDatabase.GetAnimationData(AnimationType.Attack));
                    _animationPlayer.SetCallback(() => {
                        _isAttackLocked = false;
                        SetState(UnitAnimationState.Idle); // Go back to idle after attacking
                    });
                    _animationPlayer.PlayAnimation();
                    break;
            }
        }
    }
}