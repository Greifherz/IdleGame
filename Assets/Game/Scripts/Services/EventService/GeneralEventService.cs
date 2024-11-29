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
        
        private Dictionary<Type,object> GeneralListeners = new Dictionary<Type, object>();

        public void RegisterGeneralListener<T>(Action<T> onEvent)
        {
            if (onEvent == null)
            {
                Debug.LogError($"Tried to assign a null action as GeneralListener of {typeof(T).ToString()}");
                return;
            }

            Type type = typeof(T);
            if (GeneralListeners.TryGetValue(type, out object objAction))
            {
                Action<T> action = (Action<T>) objAction;
                action += onEvent;
                GeneralListeners[type] = action;
            }
            else
            {
                GeneralListeners.Add(type,onEvent);
            }
        }

        public void UnregisterGeneralListener<T>(Action<T> removedEvent)
        {
            Type type = typeof(T);
            if (GeneralListeners.TryGetValue(type, out var listener))
            {
                Action<T> action = (Action<T>) listener;
                action -= removedEvent;
                GeneralListeners[type] = action;
            }
        }

        public void RaiseGeneralEvent<T>(T sentEvent)
        {
            Type type = typeof(T);
            if (GeneralListeners.TryGetValue(type, out var listener))
            {
                Action<T> action = (Action<T>) listener;
                action?.Invoke(sentEvent);
            }
        }
    }
}