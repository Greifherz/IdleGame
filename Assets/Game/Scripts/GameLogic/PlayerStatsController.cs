using Game.Data;
using Game.Scripts.Data;

namespace Game.GameLogic
{
    public class PlayerStatsController
    {
        private IPlayerCharacter Player;

        public PlayerStatsController(IPlayerCharacter player)
        {
            //Decorate player character to store the temporary changes
            Player = new PlayerCharacterTempStatsDecorator(player);

            //Apply creates a new player based on the decorated one
        }

        public void Undo()
        {
            Player = new PlayerCharacterTempStatsDecorator(Player.Undecorate());
        }

        public void Apply()
        {
            
        }
    }
}