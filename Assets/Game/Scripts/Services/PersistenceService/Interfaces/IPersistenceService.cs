using ServiceLocator;

namespace Services.PersistenceService
{
    // I rather not use Save and Load names not to confuse with "Save" for savegame, if there's this feature and "Load" not to confuse with a resources loader or a loader overall
    //In the persistence context, these namings make total sense
    public interface IPersistenceService : IGameService
    {
        void Persist(int intData,string id);
        void Persist(string stringData,string id);
        void Persist(bool boolData, string id);
        void Persist(float floatData, string id);

        int RetrieveInt(string id);
        string RetrieveString(string id);
        bool RetrieveBool(string id);
        float RetrieveFloat(string id);
    }
}