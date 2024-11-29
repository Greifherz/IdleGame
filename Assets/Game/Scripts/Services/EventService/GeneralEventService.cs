using System;
using System.Collections.Generic;
using UnityEngine;

namespace Services.EventService
{
    public class GeneralEventService : IGeneralEventService
    {
        public void Initialize()
        {
            
        }
        
        private Dictionary<Type,object> _generalListeners = new Dictionary<Type, object>();

        public void RegisterGeneralListener<T>(Action<T> onEvent)
        {
            if (onEvent == null)
            {
                Debug.LogError($"Tried to assign a null action as GeneralListener of {typeof(T).ToString()}");
                return;
            }

            var Type = typeof(T);
            if (_generalListeners.TryGetValue(Type, out object ObjAction))
            {
                Action<T> Action = (Action<T>) ObjAction;
                Action += onEvent;
                _generalListeners[Type] = Action;
            }
            else
            {
                _generalListeners.Add(Type,onEvent);
            }
        }

        public void UnregisterGeneralListener<T>(Action<T> removedEvent)
        {
            Type Type = typeof(T);
            if (_generalListeners.TryGetValue(Type, out var Listener))
            {
                Action<T> Action = (Action<T>) Listener;
                Action -= removedEvent;
                _generalListeners[Type] = Action;
            }
        }

        public void RaiseGeneralEvent<T>(T sentEvent)
        {
            Type Type = typeof(T);
            if (_generalListeners.TryGetValue(Type, out var Listener))
            {
                Action<T> Action = (Action<T>) Listener;
                Action?.Invoke(sentEvent);
            }
        }
    }
}