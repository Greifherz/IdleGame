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
            var Data = new ScheduleData();
            Data.TargetTime = Time.time + timeInSecsFromNow;
            
            var Handle = new ScheduleHandle();
            Data.Handle = Handle;
            
            if (_scheduledData == null)
            {
                _scheduledData = Data;
            }
            else
            {
                if (timeInSecsFromNow < _scheduledData.TargetTime)
                {
                    Data.Next = _scheduledData;
                    _scheduledData = Data;
                }
                else if(_scheduledData.Next == null)
                {
                    _scheduledData.Next = Data;
                }
                else
                {
                    FitInSchedule(Data);
                }
            }

            return Handle;
        }

        public ISchedulerHandle Schedule(DateTime time)
        {
            var Diff = Time.time + (float)(time - DateTime.UtcNow).TotalSeconds;
            return Schedule(Diff);
        }

        private void FitInSchedule(ScheduleData data)
        {
            var CurrentScheduleData = _scheduledData;
            var Fit = false;
            while (!Fit)
            {
                if (data.TargetTime < CurrentScheduleData.TargetTime)
                {
                    data.Next = CurrentScheduleData;
                    Fit = true;
                }
                else if(CurrentScheduleData.Next == null)
                {
                    CurrentScheduleData.Next = data;
                    Fit = true;
                }
                else
                {
                    CurrentScheduleData = CurrentScheduleData.Next;
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
                var Data = _scheduledData;
                _scheduledData = _scheduledData.Next;
                Data.Next = null;
                Data.Handle.Tick(this);
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