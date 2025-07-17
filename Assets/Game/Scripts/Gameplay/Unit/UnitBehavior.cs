using System;
using System.Collections.Generic;
using Game.Scripts.Army;
using Game.Scripts.Services.GameDataService;
using ServiceLocator;
using Services.TickService;
using UnityEngine;

namespace Game.Gameplay
{
    public class UnitBehavior : IDisposable
    {
        // --- Dependencies (Injected) ---
        private readonly IUnitView _view;
        private readonly ITickService _tickService;
        private readonly IUnitTargeter _targeter;
        private readonly List<IUnitView> _possibleTargets;

        // --- Owned Components ---
        private readonly UnitMovementHandler _movement;
        private readonly UnitAttackHandler _attackHandler;
        private readonly UnitAnimationController _animationController;
        
        private IUnitView _currentTarget;
        private ArmyUnitData _unitData;
        private bool _isActive = true;
        private int _amount = 1;

        public UnitBehavior(ITickService tickService,AnimationDatabase animationDatabase, ArmyUnitData unitData,int amount,IUnitView view,List<IUnitView> possibleTargets)
        {
            _amount = amount;
            _unitData = unitData;
            _view = view;
            _tickService = tickService;
            _possibleTargets = possibleTargets;
            _targeter = new DistanceUnitTargeter(); // Or get from a service

            // Create the specialized components
            _movement = new UnitMovementHandler(_view, _unitData.MoveSpeed);
            _attackHandler = new UnitAttackHandler(_unitData.AtkSpeed);
            
            // The animation controller would also be created and passed the SpriteAnimationPlayer
            _animationController = new UnitAnimationController(new SpriteAnimationPlayer(_tickService),animationDatabase );

            _tickService.RegisterTick(OnTick);
        }

        private void OnTick()
        {
            if (!_isActive) return;

            // Update components that need a tick
            _attackHandler.Tick(Time.deltaTime);

            // --- Core AI Logic ---
            if (_currentTarget == null)
            {
                // Find a target if we don't have one
                _currentTarget = _targeter.GetTarget(_view, _possibleTargets);
                _animationController.SetState(UnitAnimationController.UnitAnimationState.Idle);
                return;
            }

            float distanceToTarget = Vector3.Distance(_view.GetPosition(), _currentTarget.GetPosition());

            if (distanceToTarget <= _unitData.AttackRange)
            {
                // In range: try to attack
                if (_attackHandler.CanAttack())
                {
                    _attackHandler.PerformAttack(() => {
                        // This callback happens when the attack animation starts
                        _animationController.SetState(UnitAnimationController.UnitAnimationState.Attack);
                    });
                }
                else
                {
                    // In range but on cooldown: idle
                    _animationController.SetState(UnitAnimationController.UnitAnimationState.Idle);
                }
            }
            else
            {
                // Out of range: move
                _movement.MoveTowards(_currentTarget.GetPosition());
                _animationController.SetState(UnitAnimationController.UnitAnimationState.Move);
            }
        }

        public void Dispose()
        {
            _tickService.UnregisterTick(OnTick);
        }
    }
}