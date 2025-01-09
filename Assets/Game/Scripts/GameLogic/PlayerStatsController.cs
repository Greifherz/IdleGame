using System;
using Game.Data;
using Game.Scripts.Data;
using Game.UI;
using Game.UI.Aggregators;
using ServiceLocator;
using Services.EventService;
using Services.GameDataService;

namespace Game.GameLogic
{
    public class PlayerStatsController : IDisposable
    {
        private IEventService _EventService;
        private IGamePersistenceDataService _gamePersistenceDataService;
        private IPlayerCharacter Player;
        private StatsAggregatorContext _AggregatorContext;

        private Func<IPlayerCharacter> _UndecorateFunc;

        public PlayerStatsController()
        {
            _gamePersistenceDataService = Locator.Current.Get<IGamePersistenceDataService>();
            _AggregatorContext = Locator.Current.Get<IUIRefProviderService>().StatsAggregatorContext;
            
            //Decorate player character to store the temporary changes
            
            var PersistentData = _gamePersistenceDataService.LoadPersistentGameplayData();
            var player = new PlayerCharacter(PersistentData.PlayerPersistentData, (ev) => { });
            Player = new PlayerCharacterTempStatsDecorator(player,ref _UndecorateFunc);

            SetupButtons();
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
            Player.ModifyAttack();
        }

        public void IncreaseArmor()
        {
            Player.ModifyArmor();
        }

        public void IncreaseHealthPoints()
        {
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
            undec.ModifyHealthPoints(Player.HealthPoints - undec.HealthPoints);
            _EventService.Raise(new PlayerDataUpdatedEvent(undec));
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