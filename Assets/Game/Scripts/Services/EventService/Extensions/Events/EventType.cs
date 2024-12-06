using System;

namespace Services.EventService
{
    [Flags]
    public enum EventType
    {
        None = 0,
        Common = 1,
        Service = 2,
        View = 4,
        Back = 8,
        Transition = 16,
        GameFlowState = 32,
        Death = 64,
        PlayerDeath = 128,
        PlayerDataUpdated = 256,
        EnemyDataUpdated = 512,
        GameplayDataPersistence = 1024,
        PlayerXPGain = 2048,
        PlayerLevelUp = 4096,
        //##4096##//
    }
}