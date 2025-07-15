using Game.Gameplay;
using Game.Scripts.Army;
using UnityEngine;

namespace Game.Scripts.Services.GameDataService
{
    /// <summary>
    /// A service to provide the databases of static data.
    /// Will be deprecated/changed in the future in favor of a remote option.
    /// </summary>
    public class DatabaseProviderService : IDatabaseProviderService
    {
        private const string DATABASE_ROOT = "Databases/";
        private const string ARMY_UNIT_DATABASE = "ArmyUnitDatabase";
        private const string ANIMATION_DATABASE = "AnimationDatabase";
        public ArmyUnitDatabase ArmyUnitDatabase { get; private set; }
        public AnimationDatabase AnimationDatabase { get; private set; }
        public void Initialize()
        {
            ArmyUnitDatabase = Resources.Load<ArmyUnitDatabase>(DATABASE_ROOT + ARMY_UNIT_DATABASE);
            AnimationDatabase = Resources.Load<AnimationDatabase>(DATABASE_ROOT + ANIMATION_DATABASE);
        }
    }
}