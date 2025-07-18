using System;

namespace Services.EventService
{
    [Flags]
    public enum EventType
    {
        None = 0,
        Common = 1,
        Back = 2,
        GameFlowState = 4,
        GameplayDataPersistence = 8,
        MinerGoldCollectEvent = 16,
        MinerGoldAccumulated = 32,
        ApplicationPauseUnity = 64,
        GoldChange = 128,
        ArmyHire = 256,
        //##256##//
    }
}