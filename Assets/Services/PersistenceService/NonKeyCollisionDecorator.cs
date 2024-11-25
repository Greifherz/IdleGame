namespace Services.PersistenceService
{
    public class NonKeyCollisionDecorator : IPersistenceService
    {
        private IPersistenceService _persistenceServiceImplementation;

        private const string INT_PREFIX = "INT_";
        private const string STRING_PREFIX = "STRING_";
        private const string BOOL_PREFIX = "BOOL_";
        private const string FLOAT_PREFIX = "FLOAT_";

        public void Initialize()
        {
            
        }

        public NonKeyCollisionDecorator(IPersistenceService persistenceServiceImplementation)
        {
            _persistenceServiceImplementation = persistenceServiceImplementation;
        }
        
        public void Persist(int intData, string id)
        {
            var newId = INT_PREFIX + id;
            _persistenceServiceImplementation.Persist(intData, newId);
        }

        public void Persist(string stringData, string id)
        {
            var newId = STRING_PREFIX + id;
            _persistenceServiceImplementation.Persist(stringData, newId);
        }

        public void Persist(bool boolData, string id)
        {
            var newId = BOOL_PREFIX + id;
            _persistenceServiceImplementation.Persist(boolData, newId);
        }

        public void Persist(float floatData, string id)
        {
            var newId = FLOAT_PREFIX + id;
            _persistenceServiceImplementation.Persist(floatData, newId);
        }

        public int RetrieveInt(string id)
        {
            var newId = INT_PREFIX + id;
            return _persistenceServiceImplementation.RetrieveInt(newId);
        }

        public string RetrieveString(string id)
        {
            var newId = STRING_PREFIX + id;
            return _persistenceServiceImplementation.RetrieveString(newId);
        }

        public bool RetrieveBool(string id)
        {
            var newId = BOOL_PREFIX + id;
            return _persistenceServiceImplementation.RetrieveBool(newId);
        }

        public float RetrieveFloat(string id)
        {
            var newId = FLOAT_PREFIX + id;
            return _persistenceServiceImplementation.RetrieveFloat(newId);
        }
    }
}