using System;
using Game.Data;
using Game.Scripts.Data;
using Services.EventService;

namespace Game.GameLogic
{
    public class PlayerStatsController
    {
        private IEventService _EventService;
        private IPlayerCharacter Player;

        private Func<IPlayerCharacter> undecorateFunc;

        public PlayerStatsController(IPlayerCharacter player)
        {
            //Decorate player character to store the temporary changes
            Player = new PlayerCharacterTempStatsDecorator(player,ref undecorateFunc);

            //Apply creates a new player based on the decorated one
        }

        public void Undo()
        {
            var undec = undecorateFunc();
            Player = new PlayerCharacterTempStatsDecorator(undec,ref undecorateFunc);
        }

        public void Apply()
        {
            //Stats can never go down at this moment, so new - old lowest value is 0
            IPlayerCharacter undec = undecorateFunc();
            undec.ModifyArmor(Player.ArmorPoints - undec.ArmorPoints);
            undec.ModifyAttack(Player.AttackPoints - undec.AttackPoints);
            undec.ModifyHealthPoints(Player.HealthPoints - undec.HealthPoints);
            _EventService.Raise(new PlayerDataUpdatedEvent(undec));
        }
    }
}