using System;
using UnityEngine;

namespace Game.Scripts.Mining
{
    [Serializable]
    public class MiningData : ISerializationCallbackReceiver
    {
        [NonSerialized] public DateTime LastCollectedTime = DateTime.UtcNow;

        [SerializeField] private string _lastCollectedTimeTicksString;//Don't like saving this as string but parsing beats reflection from newtonsoft
        
        public int AcumulatedGold;
        public int ActiveMiners = 1;
        public int GoldPerMiner = 1;


        public void OnBeforeSerialize()
        {
            _lastCollectedTimeTicksString = LastCollectedTime.Ticks.ToString();
        }

        public void OnAfterDeserialize()
        {
            if (!string.IsNullOrEmpty(_lastCollectedTimeTicksString))
            {
                if (long.TryParse(_lastCollectedTimeTicksString, out long ticks))
                {
                    LastCollectedTime = new DateTime(ticks);
                }
            }
        }
    }
}