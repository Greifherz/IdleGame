using System;
using Game.Data;
using Game.Data.GameplayData;
using Game.Scripts.Data;
using Game.UI;
using Game.UI.Aggregators;
using ServiceLocator;
using Services.EventService;
using Services.GameDataService;
using UnityEngine;

namespace Game.GameLogic
{
    public class PlayerStatsController : IDisposable
    {
        private IEventService _EventService;
        private IGameplayDataService _GameplayDataService;
        
        private IPlayerCharacter Player;
        private StatsAggregatorContext _AggregatorContext;

        private Func<IPlayerCharacter> _UndecorateFunc;

        public PlayerStatsController()
        {
            _EventService = Locator.Current.Get<IEventService>();
            _GameplayDataService = Locator.Current.Get<IGameplayDataService>();
            _AggregatorContext = Locator.Current.Get<IUIRefProviderService>().StatsAggregatorContext;
            
            //Decorate player character to store the temporary changes

            SetupButtons();
        }

        public void Display()
        {
            var player = _GameplayDataService.GameplayData.PlayerCharacter;
            
            Player = new PlayerCharacterTempStatsDecorator(player,ref _UndecorateFunc);
            
            SetupStatValues();
        }

        private void SetupButtons()
        {
            _AggregatorContext.ApplyButton.onClick.AddListener(Apply);
            _AggregatorContext.UndoButton.onClick.AddListener(Undo);
            _AggregatorContext.UndoButton.onClick.AddListener(SetupStatValues);
            _AggregatorContext.IncreaseArmorButton.onClick.AddListener(IncreaseArmor);
            _AggregatorContext.IncreaseArmorButton.onClick.AddListener(SetupStatValues);
            _AggregatorContext.IncreaseAttackButton.onClick.AddListener(IncreaseAttack);
            _AggregatorContext.IncreaseAttackButton.onClick.AddListener(SetupStatValues);
            _AggregatorContext.IncreaseHealthButton.onClick.AddListener(IncreaseHealthPoints);
            _AggregatorContext.IncreaseHealthButton.onClick.AddListener(SetupStatValues);
        }

        private void SetupStatValues()
        {
            _AggregatorContext.ArmorValue.text = Player.ArmorPoints.ToString();
            _AggregatorContext.AttackValue.text = Player.AttackPoints.ToString();
            _AggregatorContext.HealthValue.text = Player.HealthPoints.ToString();
            _AggregatorContext.PointsLeftValue.text = Player.PointsToDistribute.ToString();
        }

        public void IncreaseAttack()
        {
            if(Player.PointsToDistribute > 0)
                Player.ModifyAttack();
        }

        public void IncreaseArmor()
        {
            if(Player.PointsToDistribute > 0)
                Player.ModifyArmor();
        }

        public void IncreaseHealthPoints()
        {
            if(Player.PointsToDistribute > 0)
                Player.ModifyHealthPoints();
        }

        public void Undo()
        {
            var undec = _UndecorateFunc();
            Player = new PlayerCharacterTempStatsDecorator(undec,ref _UndecorateFunc);
        }

        public void Apply()
        {
            //Stats can never go down at this moment, so new - old lowest value is 0
            IPlayerCharacter undec = _UndecorateFunc();
            undec.ModifyArmor(Player.ArmorPoints - undec.ArmorPoints);
            undec.ModifyAttack(Player.AttackPoints - undec.AttackPoints);
            var hpMod = (Player.HealthPoints - undec.HealthPoints) / 5;
            undec.ModifyHealthPoints(hpMod);
            Debug.Log($"HPMod -> {hpMod}");
            if (hpMod > 0)
            {
                _EventService.Raise(new PlayerHealthUpdateViewEvent(undec.HealthPercentage,$"{undec.CurrentHealthPoints}/{undec.HealthPoints}"),EventPipelineType.ViewPipeline);
            }
                
            _EventService.Raise(new PlayerDataUpdatedEvent(undec),EventPipelineType.GameplayPipeline);
        }

        public void Dispose()
        {
            _AggregatorContext.ApplyButton.onClick.RemoveAllListeners();
            _AggregatorContext.UndoButton.onClick.RemoveAllListeners();
            _AggregatorContext.IncreaseArmorButton.onClick.RemoveAllListeners();
            _AggregatorContext.IncreaseAttackButton.onClick.RemoveAllListeners();
            _AggregatorContext.IncreaseHealthButton.onClick.RemoveAllListeners();
        }
    }
}