﻿using System;
using System.Collections.Generic;
using Game.Data;
using Game.Data.GameplayData;
using ServiceLocator;
using Services.EventService;
using Services.PersistenceService;
using UnityEngine;

namespace Services.GameDataService
{
    public class GameDataService : IGameDataService
    {
        private IPersistenceService _persistenceService;
        private IEventService _eventService;

        private IEventHandler _deathevEventHandler;
        private IEventHandler _playerDeathEventHandler;

        public GameplayData GameplayData { get; private set; }
        
        public GameDataService()
        {
            _persistenceService = Locator.Current.Get<IPersistenceService>();
            _eventService = Locator.Current.Get<IEventService>();
        }

        public void Initialize()
        {
            GameplayData = LoadOrCreateGameplayData();
            ListenToEvents();
        }

        public GameplayData LoadOrCreateGameplayData()
        {
            var Data = new GameplayData();
            if (_persistenceService.RetrieveBool(GameplayPersistenceKeys.GAMEPLAY_DATA_KEY))
            {
                var PlayerName = _persistenceService.RetrieveString(
                    $"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_PLAYER_NAME_MOD}");
                var PlayerLevel = _persistenceService.RetrieveInt(
                    $"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_PLAYER_LEVEL_MOD}");
                var PlayerXp = _persistenceService.RetrieveInt(
                    $"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_PLAYER_XP_MOD}");
                var PlayerAttack = _persistenceService.RetrieveInt(
                    $"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_PLAYER_ATK_MOD}");
                var PlayerHp = _persistenceService.RetrieveInt(
                    $"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_PLAYER_HP_MOD}");
                var PlayerArmor = _persistenceService.RetrieveInt(
                    $"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_PLAYER_ARMOR_MOD}");
                var PlayerDeaths = _persistenceService.RetrieveInt(
                    $"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_PLAYER_DEATH_MOD}");
                var PlayerCharacter = new PlayerCharacter(PlayerName,PlayerLevel,PlayerXp,PlayerHp,PlayerArmor,PlayerAttack,
                    (pchar) => { },PlayerDeaths);

                var EnemyCount = _persistenceService.RetrieveInt(
                    $"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_ENEMY_COUNT_MOD}");

                var EnemyDataList = new List<EnemyData>(EnemyCount);
                for (var i = 0; i < EnemyCount; i++)
                {
                    var EnemyId = _persistenceService.RetrieveInt(
                        $"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_ENEMY_ID_MOD.Replace(GameplayPersistenceKeys.PERSISTENCE_COUNT_MARKUP, i.ToString())}");
                    var EnemyName = _persistenceService.RetrieveString(
                        $"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_ENEMY_NAME_MOD.Replace(GameplayPersistenceKeys.PERSISTENCE_COUNT_MARKUP, i.ToString())}");
                    var EnemyKillCount = _persistenceService.RetrieveInt(
                        $"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_ENEMY_KILLCOUNT_MOD.Replace(GameplayPersistenceKeys.PERSISTENCE_COUNT_MARKUP, i.ToString())}");
                    EnemyDataList.Add(new EnemyData
                    {
                        EnemyId = EnemyId,
                        EnemyName = EnemyName,
                        KillCount = EnemyKillCount
                    });
                }

                Data = new GameplayData
                {
                    EnemyData = EnemyDataList,
                    PlayerCharacter = PlayerCharacter
                };
            }
            return Data;
        }

        public bool PersistGameplayData()
        {
            try
            {
                _persistenceService.Persist(true,GameplayPersistenceKeys.GAMEPLAY_DATA_KEY);
                _persistenceService.Persist(GameplayData.PlayerCharacter.Name,$"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_PLAYER_NAME_MOD}");
                _persistenceService.Persist(GameplayData.PlayerCharacter.Level,$"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_PLAYER_LEVEL_MOD}");
                _persistenceService.Persist(GameplayData.PlayerCharacter.ExperiencePoints,$"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_PLAYER_XP_MOD}");
                _persistenceService.Persist(GameplayData.PlayerCharacter.AttackPoints,$"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_PLAYER_ATK_MOD}");
                _persistenceService.Persist(GameplayData.PlayerCharacter.HealthPoints,$"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_PLAYER_HP_MOD}");
                _persistenceService.Persist(GameplayData.PlayerCharacter.ArmorPoints,$"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_PLAYER_ARMOR_MOD}");
                _persistenceService.Persist(GameplayData.PlayerCharacter.DeathCount,$"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_PLAYER_DEATH_MOD}");

                var EnemyCount = GameplayData.EnemyData.Count;
                for (var i = 0; i < EnemyCount; i++)
                {
                    _persistenceService.Persist(GameplayData.EnemyData[i].EnemyId,$"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_ENEMY_ID_MOD.Replace(GameplayPersistenceKeys.PERSISTENCE_COUNT_MARKUP, i.ToString())}");
                    _persistenceService.Persist(GameplayData.EnemyData[i].EnemyName,$"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_ENEMY_NAME_MOD.Replace(GameplayPersistenceKeys.PERSISTENCE_COUNT_MARKUP, i.ToString())}");
                    _persistenceService.Persist(GameplayData.EnemyData[i].KillCount,$"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_ENEMY_KILLCOUNT_MOD.Replace(GameplayPersistenceKeys.PERSISTENCE_COUNT_MARKUP, i.ToString())}");
                }
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Thrown an exception while persisting GameplayData: {e.Message}");
                return false;
            }
        }

        private void ListenToEvents()
        {
            _deathevEventHandler = new DeathEventHandler(OnDeath);
            _playerDeathEventHandler = new PlayerDeathEventHandler(OnPlayerDeath);
            
            _eventService.RegisterListener(_deathevEventHandler,EventPipelineType.GameplayPipeline);
            _eventService.RegisterListener(_playerDeathEventHandler,EventPipelineType.GameplayPipeline);
        }

        private void OnPlayerDeath(IPlayerDeathEvent playerDeathEvent)
        {
            GameplayData = new GameplayData
            {
                EnemyData = GameplayData.EnemyData,
                PlayerCharacter = playerDeathEvent.PlayerCharacter.GetConcrete()
            };
            //GameplayData updated - A specific type for player data updated
        }

        private void OnDeath(IDeathEvent deathEvent)//TODO - Death shoulnd't be the only responsible for adding this. There should be an event to set this right
        {
            var DeadEnemy = deathEvent.DeadCharacter;
            if (GameplayData.EnemyData == null)
            {
                GameplayData = new GameplayData
                {
                    EnemyData = new List<EnemyData>
                    {
                        new EnemyData
                        {
                            EnemyId = DeadEnemy.Id,
                            EnemyName = DeadEnemy.Name,
                            KillCount = DeadEnemy.KillCount
                        }
                    },
                    PlayerCharacter = GameplayData.PlayerCharacter
                };
                return;
            }
            for (var Index = 0; Index < GameplayData.EnemyData.Count; Index++)
            {
                var EnemyData = GameplayData.EnemyData[Index];
                if (EnemyData.EnemyId == DeadEnemy.Id)
                {
                    GameplayData.EnemyData[Index] = new EnemyData
                    {
                        EnemyId = DeadEnemy.Id,
                        EnemyName = DeadEnemy.Name,
                        KillCount = DeadEnemy.KillCount
                    };
                }
            }

            //GameplayData updated - A specific type for enemy data updated
        }
    }
}