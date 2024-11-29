using System;

namespace Services.Scheduler
{
    public interface ISchedulerHandle
    {
        event Action OnScheduleTick;
        void Tick(ISchedulerService schedulerService); //Needing the service is a trick to force this to only be called by the scheduler,even if we're not using
    }
}