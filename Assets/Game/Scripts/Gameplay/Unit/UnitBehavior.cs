using Game.Scripts.Services.GameDataService;
using ServiceLocator;
using Services.TickService;
using UnityEngine;

namespace Game.Gameplay
{
    //This class was created as deprecated. It was created to create the logic that will eventually be ported to other scripts, non-monobehaviour as a prototype.
    public class UnitBehavior : MonoBehaviour
    {
        [SerializeField] private Transform Self;
        [SerializeField] private SpriteRenderer Renderer;
        [SerializeField] private Transform Target;
        [SerializeField] private int MoveSpeed = 15;

        // private float _lastDistance = 1000;
        [SerializeField] private bool _active = false;

        [SerializeField] private float AtkCooldown = 1.5f;

        private float _cooldown = 0;

        private SpriteAnimationPlayer _animationPlayer;
        private AnimationDatabase _animationDatabase;

        enum AnimationState { Idle, Move, Attack, Block }
        private AnimationState _currentState = AnimationState.Idle;
        
        private void Start()
        {
            var databaseProvider = Locator.Current.Get<IDatabaseProviderService>();
            _animationDatabase = databaseProvider.AnimationDatabase;
            
            _animationPlayer = new SpriteAnimationPlayer(Locator.Current.Get<ITickService>());
            _animationPlayer.SetRenderer(Renderer);
            _animationPlayer.SetAnimation(_animationDatabase.GetAnimationData(AnimationType.Idle));
            _animationPlayer.PlayAnimation();
        }

        private void Update()
        {
            if(!_active) return;
            
            if (_cooldown > 0)
            {
                _cooldown -= Time.deltaTime;
            }

            var Dir = Target.position - Self.position;
            if (Dir.magnitude < 50) //In range
            {
                //Don't move
                if (_cooldown <= 0)
                {
                    Attack();
                }
                else
                {
                    Idle();
                }
            }
            else
            {
                Move(Dir);
            }
        }

        private void Move(Vector3 Dir)
        {
            var moveAmount = Dir.normalized * (MoveSpeed * Time.deltaTime);
            transform.position = transform.position + moveAmount;
            if (_currentState != AnimationState.Move)
            {
                _currentState = AnimationState.Move;
                _animationPlayer.SetAnimation(_animationDatabase.GetAnimationData(AnimationType.Move));
                _animationPlayer.PlayAnimation();
            }
        }

        private void Idle()
        {
            if (_currentState != AnimationState.Idle)
            {
                _currentState = AnimationState.Idle;
                _animationPlayer.SetAnimation(_animationDatabase.GetAnimationData(AnimationType.Idle));
                _animationPlayer.PlayAnimation();
            }
        }

        private void Attack()
        {
            if (_currentState != AnimationState.Attack)
            {
                _currentState = AnimationState.Attack;
                _animationPlayer.SetAnimation(_animationDatabase.GetAnimationData(AnimationType.Attack));
                _animationPlayer.PlayAnimation();
                _animationPlayer.SetCallback(() => { _cooldown = AtkCooldown + UnityEngine.Random.Range(-0.2f,0.2f); });
            }
        }
    }
}
