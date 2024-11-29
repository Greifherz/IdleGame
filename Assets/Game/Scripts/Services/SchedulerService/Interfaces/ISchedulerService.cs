using System;
using ServiceLocator;

namespace Services.Scheduler
{
    public interface ISchedulerService : IGameService
    {
        ISchedulerHandle Schedule(float timeFromNow);
        ISchedulerHandle Schedule(DateTime time);
    }
}