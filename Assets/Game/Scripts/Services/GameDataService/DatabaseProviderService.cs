using Game.Scripts.Army;
using ServiceLocator;
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
        public ArmyUnitDatabase ArmyUnitDatabase { get; private set; }
        public void Initialize()
        {
            ArmyUnitDatabase = Resources.Load<ArmyUnitDatabase>(DATABASE_ROOT);
        }
    }
}