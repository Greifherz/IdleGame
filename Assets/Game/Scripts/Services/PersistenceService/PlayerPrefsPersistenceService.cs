using UnityEngine;

namespace Services.PersistenceService
{
    public class PlayerPrefsPersistenceService : IPersistenceService
    {
        public void Initialize()
        {
            
        }
        
        public void Persist(int intData, string id)
        {
            PlayerPrefs.SetInt(id, intData);
            PlayerPrefs.Save();
        }

        public void Persist(string stringData, string id)
        {
            PlayerPrefs.SetString(id, stringData);
            PlayerPrefs.Save();
        }

        public void Persist(bool boolData, string id)
        {
            if(boolData)PlayerPrefs.SetInt(id, 0);
            else PlayerPrefs.DeleteKey(id);
            PlayerPrefs.Save();
        }

        public void Persist(float floatData, string id)
        {
            PlayerPrefs.SetFloat(id, floatData);
            PlayerPrefs.Save();
        }

        public int RetrieveInt(string id)
        {
            return PlayerPrefs.GetInt(id, int.MinValue);
        }

        public string RetrieveString(string id)
        {
            return PlayerPrefs.GetString(id, ""); //TODO - Use a const that's not empty, easier to debug in the future
        }

        public bool RetrieveBool(string id)
        {
            return PlayerPrefs.HasKey(id);
        }

        public float RetrieveFloat(string id)
        {
            return PlayerPrefs.GetFloat(id, float.MinValue);
        }
    }
}