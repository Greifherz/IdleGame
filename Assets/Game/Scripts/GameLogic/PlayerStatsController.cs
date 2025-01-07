using System;
using Game.Data;
using Game.Scripts.Data;
using Game.UI.Aggregators;
using ServiceLocator;
using Services.EventService;
using Services.GameDataService;

namespace Game.GameLogic
{
    public class PlayerStatsController
    {
        private IEventService _EventService;
        private IGamePersistenceDataService _gamePersistenceDataService;
        private IPlayerCharacter Player;
        private StatsAggregatorContext _AggregatorContext;

        private Func<IPlayerCharacter> _UndecorateFunc;

        public PlayerStatsController(StatsAggregatorContext aggregatorContext)
        {
            _gamePersistenceDataService = Locator.Current.Get<IGamePersistenceDataService>();
            
            _AggregatorContext = aggregatorContext;
            //Decorate player character to store the temporary changes
            
            var PersistentData = _gamePersistenceDataService.LoadPersistentGameplayData();
            var player = new PlayerCharacter(PersistentData.PlayerPersistentData, (ev) => { });
            Player = new PlayerCharacterTempStatsDecorator(player,ref _UndecorateFunc);
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
    }
}