﻿using ServiceLocator;
using Services.EventService;
using UnityEngine;

namespace Services.PersistenceService
{
    public class PlayerPrefsSaveDecorator : IPersistenceService
    {
        private IPersistenceService _persistenceServiceImplementation;

        public PlayerPrefsSaveDecorator(IPersistenceService persistenceServiceImplementation)
        {
            _persistenceServiceImplementation = persistenceServiceImplementation;
        }

        private IEventService _eventService;

        private ApplicationPauseUnityEventHandler _applicationPauseUnityEventHandler;
        
        public void Initialize()
        {
            _eventService = Locator.Current.Get<IEventService>();
            
            _applicationPauseUnityEventHandler = new ApplicationPauseUnityEventHandler((pauseStatusEvent) => { Commit(); });
            
            _eventService.RegisterListener(_applicationPauseUnityEventHandler,EventPipelineType.UnityPipeline);
            
            Application.focusChanged += (hasFocus) => { if(!hasFocus) Commit(); };
            Application.quitting += () => { Commit(); };

            _persistenceServiceImplementation.Initialize();
        }

        public void Persist(int intData, string id)
        {
            _persistenceServiceImplementation.Persist(intData, id);
        }

        public void Persist(string stringData, string id)
        {
            _persistenceServiceImplementation.Persist(stringData, id);
        }

        public void Persist(bool boolData, string id)
        {
            _persistenceServiceImplementation.Persist(boolData, id);
        }

        public void Persist(float floatData, string id)
        {
            _persistenceServiceImplementation.Persist(floatData, id);
        }

        public void Commit()
        {
            _persistenceServiceImplementation.Commit();
        }

        public int RetrieveInt(string id)
        {
            return _persistenceServiceImplementation.RetrieveInt(id);
        }

        public string RetrieveString(string id)
        {
            return _persistenceServiceImplementation.RetrieveString(id);
        }

        public bool RetrieveBool(string id)
        {
            return _persistenceServiceImplementation.RetrieveBool(id);
        }

        public float RetrieveFloat(string id)
        {
            return _persistenceServiceImplementation.RetrieveFloat(id);
        }
    }
}