using System;
using Game.Data.GameplayData;
using Services.EventService;
using Services.Scheduler;
using Services.ViewProvider.View;

namespace Game.Scripts.Army
{
    public class ArmyPresenter : IDisposable
    {
        private readonly ISchedulerService _schedulerService;
        private readonly IEventService _eventService;
        private readonly IArmyView _view;

        private ArmyModel _armyModel;
        
        public ArmyPresenter(GameplayData gameplayData)
        {
            var armyDatabase = new ArmyUnitDatabase();//Retrieve from service
            
            _armyModel = new ArmyModel(gameplayData,armyDatabase);
        }

        public void Hire(int armyUnitType)
        {
            var cost = _armyModel.Hire((ArmyUnitType)armyUnitType);
            _eventService.Raise(new GoldChangeEvent(-cost),EventPipelineType.GameplayPipeline);
            UpdateView();
            
        }

        private void UpdateView()
        {
            
        }

        public void Dispose()
        {
        }
    }
}