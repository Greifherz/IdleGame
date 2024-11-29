using System;
using ServiceLocator;
using Services.TickService;
using UnityEngine;

namespace Services.Scheduler
{
    public class SchedulerService : ISchedulerService
    {
        private ITickService _tickService;
        private ScheduleData _scheduledData = null;
        
        public void Initialize()
        {
            _tickService = Locator.Current.Get<ITickService>();
            _tickService.RegisterTick(TickWrap);
        }
        
        public ISchedulerHandle Schedule(float timeInSecsFromNow)
        {
            var data = new ScheduleData();
            data.TargetTime = timeInSecsFromNow;
            
            var handle = new ScheduleHandle();
            data.Handle = handle;
            
            if (_scheduledData == null)
            {
                _scheduledData = data;
            }
            else
            {
                if (timeInSecsFromNow < _scheduledData.TargetTime)
                {
                    data.Next = _scheduledData;
                    _scheduledData = data;
                }
                else if(_scheduledData.Next == null)
                {
                    _scheduledData.Next = data;
                }
                else
                {
                    FitInSchedule(data);
                }
            }

            return handle;
        }

        public ISchedulerHandle Schedule(DateTime time)
        {
            var diff = Time.time + (float)(time - DateTime.UtcNow).TotalSeconds;
            return Schedule(diff);
        }

        private void FitInSchedule(ScheduleData data)
        {
            var currentScheduleData = _scheduledData;
            var fit = false;
            while (!fit)
            {
                if (data.TargetTime < currentScheduleData.TargetTime)
                {
                    data.Next = currentScheduleData;
                    fit = true;
                }
                else if(currentScheduleData.Next == null)
                {
                    currentScheduleData.Next = data;
                    fit = true;
                }
                else
                {
                    currentScheduleData = currentScheduleData.Next;
                }
            }
        }

        private void TickWrap()
        {
            _tickService.RunOnMainThread(Tick);
        }

        private void Tick()
        {
            if (_scheduledData == null)
            {
                return;
            }
            
            if (_scheduledData.TargetTime < Time.time)
            {
                var data = _scheduledData;
                _scheduledData = _scheduledData.Next;
                data.Next = null;
                data.Handle.Tick(this);
                Tick();
            }
        }

        internal class ScheduleData
        {
            public float TargetTime;
            public ScheduleHandle Handle;
            public ScheduleData Next = null;
        }
    }
}