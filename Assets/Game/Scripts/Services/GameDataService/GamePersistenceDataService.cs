using System;
using System.Collections.Generic;
using Game.Data;
using Game.Data.GameplayData;
using Game.Data.PersistentData;
using ServiceLocator;
using Services.EventService;
using Services.PersistenceService;
using UnityEngine;

namespace Services.GameDataService
{
    public class GamePersistenceDataService : IGamePersistenceDataService
    {
        private IPersistenceService _persistenceService;
        private IEventService _eventService;

        private IEventHandler _deathevEventHandler;
        private IEventHandler _playerDeathEventHandler;
        
        public GamePersistenceDataService()
        {
            _persistenceService = Locator.Current.Get<IPersistenceService>();
            _eventService = Locator.Current.Get<IEventService>();
        }

        public void Initialize()
        {
            
        }

        public GameplayPersistentData LoadPersistentGameplayData()
        {
            var Data = GameplayPersistentData.CreateDefaultPersistentData();
            if (!_persistenceService.RetrieveBool(GameplayPersistenceKeys.GAMEPLAY_DATA_KEY)) return Data;
            
            var PlayerData = LoadPlayerData();

            var EnemyDataList = LoadEnemyData();

            Data = new GameplayPersistentData(PlayerData, EnemyDataList.ToArray());
            return Data;
        }

        private List<EnemyPersistentData> LoadEnemyData()
        {
            var EnemyCount = _persistenceService.RetrieveInt(
                $"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_ENEMY_COUNT_MOD}");

            var EnemyDataList = new List<EnemyPersistentData>(EnemyCount);
            for (var i = 0; i < EnemyCount; i++)
            {
                var EnemyId = _persistenceService.RetrieveInt(
                    $"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_ENEMY_ID_MOD.Replace(GameplayPersistenceKeys.PERSISTENCE_COUNT_MARKUP, i.ToString())}");
                var EnemyKillCount = _persistenceService.RetrieveInt(
                    $"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_ENEMY_KILLCOUNT_MOD.Replace(GameplayPersistenceKeys.PERSISTENCE_COUNT_MARKUP, i.ToString())}");
                var EnemyCurrentHp = _persistenceService.RetrieveInt(
                    $"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_ENEMY_HP_MOD.Replace(GameplayPersistenceKeys.PERSISTENCE_COUNT_MARKUP, i.ToString())}");

                var EnemyData = new EnemyPersistentData(EnemyId, EnemyKillCount, EnemyCurrentHp);
                EnemyDataList.Add(EnemyData);
            }

            return EnemyDataList;
        }

        private PlayerPersistentData LoadPlayerData()
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
            var PlayerCurrentHp = _persistenceService.RetrieveInt(
                $"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_PLAYER_CURRENTHP_MOD}");
            var PlayerArmor = _persistenceService.RetrieveInt(
                $"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_PLAYER_ARMOR_MOD}");
            var PlayerDeaths = _persistenceService.RetrieveInt(
                $"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_PLAYER_DEATH_MOD}");

            var PlayerData = new PlayerPersistentData
                (PlayerName, PlayerHp, PlayerCurrentHp, PlayerArmor, PlayerAttack, PlayerLevel, PlayerXp, PlayerDeaths);
            return PlayerData;
        }

        public bool PersistGameplayData(GameplayData toPersist)
        {
            try
            {
                _persistenceService.Persist(true,GameplayPersistenceKeys.GAMEPLAY_DATA_KEY);
                _persistenceService.Persist(toPersist.PlayerCharacter.Name,$"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_PLAYER_NAME_MOD}");
                _persistenceService.Persist(toPersist.PlayerCharacter.Level,$"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_PLAYER_LEVEL_MOD}");
                _persistenceService.Persist(toPersist.PlayerCharacter.ExperiencePoints,$"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_PLAYER_XP_MOD}");
                _persistenceService.Persist(toPersist.PlayerCharacter.AttackPoints,$"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_PLAYER_ATK_MOD}");
                _persistenceService.Persist(toPersist.PlayerCharacter.HealthPoints,$"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_PLAYER_HP_MOD}");
                _persistenceService.Persist(toPersist.PlayerCharacter.CurrentHealthPoints,$"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_PLAYER_CURRENTHP_MOD}");
                _persistenceService.Persist(toPersist.PlayerCharacter.ArmorPoints,$"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_PLAYER_ARMOR_MOD}");
                _persistenceService.Persist(toPersist.PlayerCharacter.DeathCount,$"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_PLAYER_DEATH_MOD}");
            
                var EnemyCount = toPersist.EnemyData.Count;
                for (var i = 0; i < EnemyCount; i++)
                {
                    _persistenceService.Persist(toPersist.EnemyData[i].EnemyId,$"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_ENEMY_ID_MOD.Replace(GameplayPersistenceKeys.PERSISTENCE_COUNT_MARKUP, i.ToString())}");
                    _persistenceService.Persist(toPersist.EnemyData[i].PersistentData.CurrentHealthPoints,$"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_ENEMY_HP_MOD.Replace(GameplayPersistenceKeys.PERSISTENCE_COUNT_MARKUP, i.ToString())}");
                    _persistenceService.Persist(toPersist.EnemyData[i].PersistentData.KillCount,$"{GameplayPersistenceKeys.GAMEPLAY_DATA_KEY}{GameplayPersistenceKeys.GAMEPLAY_DATA_ENEMY_KILLCOUNT_MOD.Replace(GameplayPersistenceKeys.PERSISTENCE_COUNT_MARKUP, i.ToString())}");
                }
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Thrown an exception while persisting GameplayData: {e.Message}");
                return false;
            }
        }
    }
}