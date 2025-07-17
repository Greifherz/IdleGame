using System;
using System.Collections.Generic;

namespace Services.SceneDataService
{
    public class SceneDataService : ISceneDataService
    {
        // The dictionary holds different payloads, keyed by their type.
        private readonly Dictionary<Type, object> _dataPayloads = new Dictionary<Type, object>();

        public void Initialize()
        {
            
        }

        public void SetData<T>(T data) where T : class
        {
            // Use the data's own type as the key for storage.
            _dataPayloads[typeof(T)] = data;
        }

        public T GetData<T>() where T : class
        {
            var key = typeof(T);
            if (_dataPayloads.TryGetValue(key, out var payload))
            {
                // We found the data. Remove it to prevent stale reads.
                _dataPayloads.Remove(key);
                // The 'as' cast here is now safe because we stored it by its type.
                return payload as T;
            }

            return null; // No data of this type was found.
        }

        public bool HasData<T>() where T : class
        {
            return _dataPayloads.ContainsKey(typeof(T));
        }
    }
}