using System;
using ServiceLocator;

namespace Services.TickService
{
    public interface ITickService : IGameService
    {
        void RegisterTick(Action tickAction);
        void UnregisterTick(Action tickAction);

        void RunOnMainThread(Action mainThreadAction);
        void RunOnLateMainThread(Action lateMainThreadAction);
        void RunOnFixedMainThread(Action fixedMainThreadAction);

        void Disable();
    }
}