using System;

namespace Services.Scheduler
{
    public class ScheduleHandle : ISchedulerHandle
    {
        public event Action OnScheduleTick;
        
        public void Tick(ISchedulerService schedulerService)
        {
            OnScheduleTick?.Invoke();
        }
    }
}