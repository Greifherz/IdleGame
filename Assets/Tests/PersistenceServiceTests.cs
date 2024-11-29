using System;
using NUnit.Framework;
using Services.PersistenceService;

namespace Tests
{
    public class PersistenceServiceTests
    {
        [Test]
        public void PlayerPrefs_Bool_Persistence_Test()
        {
            var PersistenceService = new PlayerPrefsPersistenceService();
            PersistenceService.Initialize();

            var Key = "boolTestKey";
            
            PersistenceService.Persist(true,Key);

            var RetrievedInfo = PersistenceService.RetrieveBool(Key);
            
            Assert.IsTrue(RetrievedInfo);
        }
        
        [Test]
        public void PlayerPrefs_Int_Persistence_Test()
        {
            var PersistenceService = new PlayerPrefsPersistenceService();
            PersistenceService.Initialize();

            var Key = "intTestKey";
            var Value = 224532;
            
            PersistenceService.Persist(Value,Key);

            var RetrievedInfo = PersistenceService.RetrieveInt(Key);
            
            Assert.AreEqual(RetrievedInfo,Value);
        }
        
        [Test]
        public void PlayerPrefs_String_Persistence_Test()
        {
            var PersistenceService = new PlayerPrefsPersistenceService();
            PersistenceService.Initialize();

            var Key = "stringTestKey";
            var Value = "efbewoiEQAQTt4gq4@#%T214BfvSFADGqetge";
            
            PersistenceService.Persist(Value,Key);

            var RetrievedInfo = PersistenceService.RetrieveString(Key);
            
            Assert.AreEqual(RetrievedInfo,Value);
        }
        
        [Test]
        public void PlayerPrefs_Float_Persistence_Test()
        {
            var PersistenceService = new PlayerPrefsPersistenceService();
            PersistenceService.Initialize();

            var Key = "floatTestKey";
            var Value = (float)Math.PI;
            
            PersistenceService.Persist(Value,Key);

            var RetrievedInfo = PersistenceService.RetrieveFloat(Key);
            
            Assert.AreEqual(RetrievedInfo,Value);
        }
    }
}