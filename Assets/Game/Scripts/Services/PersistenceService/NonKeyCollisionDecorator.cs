namespace Services.PersistenceService
{
    public class NonKeyCollisionDecorator : IPersistenceService
    {
        private IPersistenceService _persistenceServiceImplementation;

        private const string IntPrefix = "INT_";
        private const string StringPrefix = "STRING_";
        private const string BoolPrefix = "BOOL_";
        private const string FloatPrefix = "FLOAT_";

        public void Initialize()
        {
            
        }

        public NonKeyCollisionDecorator(IPersistenceService persistenceServiceImplementation)
        {
            _persistenceServiceImplementation = persistenceServiceImplementation;
        }
        
        public void Persist(int intData, string id)
        {
            var NewId = IntPrefix + id;
            _persistenceServiceImplementation.Persist(intData, NewId);
        }

        public void Persist(string stringData, string id)
        {
            var NewId = StringPrefix + id;
            _persistenceServiceImplementation.Persist(stringData, NewId);
        }

        public void Persist(bool boolData, string id)
        {
            var NewId = BoolPrefix + id;
            _persistenceServiceImplementation.Persist(boolData, NewId);
        }

        public void Persist(float floatData, string id)
        {
            var NewId = FloatPrefix + id;
            _persistenceServiceImplementation.Persist(floatData, NewId);
        }

        public void Commit()
        {
            _persistenceServiceImplementation.Commit();
        }

        public int RetrieveInt(string id)
        {
            var NewId = IntPrefix + id;
            return _persistenceServiceImplementation.RetrieveInt(NewId);
        }

        public string RetrieveString(string id)
        {
            var NewId = StringPrefix + id;
            return _persistenceServiceImplementation.RetrieveString(NewId);
        }

        public bool RetrieveBool(string id)
        {
            var NewId = BoolPrefix + id;
            return _persistenceServiceImplementation.RetrieveBool(NewId);
        }

        public float RetrieveFloat(string id)
        {
            var NewId = FloatPrefix + id;
            return _persistenceServiceImplementation.RetrieveFloat(NewId);
        }
    }
}