using System;
using System.Collections.Generic;
using ServiceLocator;
using Services.TickService;
using UnityEngine;

namespace Services.Scheduler
{
    public class SchedulerService : ISchedulerService
    {
        private ITickService _tickService;
        private ScheduleData _scheduledData = null;
        private List<ScheduleData> _conditionScheduledData = new List<ScheduleData>(10);
        
        private static List<ScheduleData> itemsToTick = new List<ScheduleData>();
        
        public void Initialize()
        {
            _tickService = Locator.Current.Get<ITickService>();
            _tickService.RegisterTick(TickWrap);
        }
        
        public ISchedulerHandle Schedule(float timeInSecsFromNow)
        {
            var data = new ScheduleData
            {
                TargetTime = Time.time + timeInSecsFromNow,
                Handle = new ScheduleHandle()
            };

            if (_scheduledData == null || data.TargetTime < _scheduledData.TargetTime)
            {
                data.Next = _scheduledData;
                _scheduledData = data;
                return data.Handle;
            }
    
            var current = _scheduledData;
            while (current.Next != null && current.Next.TargetTime < data.TargetTime)
            {
                current = current.Next;
            }
    
            data.Next = current.Next;
            current.Next = data;

            return data.Handle;
        }

        public ISchedulerHandle Schedule(DateTime time)
        {
            var Diff = Time.time + (float)(time - DateTime.UtcNow).TotalSeconds;
            return Schedule(Diff);
        }

        public ISchedulerHandle Schedule(Func<bool> conditionCheck)
        {
            var Handle = new ScheduleHandle();
            var data = new ScheduleData
            {
                Handle = Handle,
                ConditionCheck = conditionCheck
            };
            _conditionScheduledData.Add(data);
            return Handle;
        }

        private void TickWrap()
        {
            _tickService.RunOnMainThread(Tick);
        }

        private void Tick()
        {
            if (_conditionScheduledData.Count > 0)
            {
                foreach (var data in _conditionScheduledData)
                {
                    if (data.ConditionCheck())
                    {
                        itemsToTick.Add(data);
                    }
                }

                if (itemsToTick.Count > 0)
                {
                    foreach (var data in itemsToTick)
                    {
                        data.Handle.Tick(this);
                        _conditionScheduledData.Remove(data);
                    }
                }
                itemsToTick.Clear();
            }

            if (_scheduledData == null)
            {
                return;
            }
            
            
            while (_scheduledData != null && _scheduledData.TargetTime < Time.time)
            {
                var dataToProcess = _scheduledData;
                _scheduledData = _scheduledData.Next;
                dataToProcess.Next = null;
        
                dataToProcess.Handle.Tick(this);
            }
            
        }

        internal class ScheduleData
        {
            public float TargetTime;
            public ScheduleHandle Handle;
            public ScheduleData Next = null;
            public Func<bool> ConditionCheck = null;
        }
    }
}