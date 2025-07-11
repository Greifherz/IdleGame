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
        GameplayPlayerStatsVisibility = 8192,
        MinerGoldCollectEvent = 16384,
        MinerGoldAccumulated = 32768,
        ApplicationFocusUnity = 65536,
        ApplicationPauseUnity = 131072,
        ApplicationQuitUnity = 262144,
        GoldChange = 524288,
        ArmyHire = 1048576,
        //##1048576##//
    }
}