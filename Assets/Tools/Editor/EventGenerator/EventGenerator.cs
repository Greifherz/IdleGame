#if UNITY_EDITOR

using System.IO;
using UnityEditor;
using UnityEngine;

namespace Tools.EventGenerator
{
    public class EventGenerator : Editor
    {
        private const string InterfaceTemplatePath = @"Assets\Tools\Editor\Templates\EventInterface.txt";
        private const string ConcreteTemplatePath = @"Assets\Tools\Editor\Templates\EventConcrete.txt";
        private const string HandlerTemplatePath = @"Assets\Tools\Editor\Templates\EventHandler.txt";

        private const string InterfacePath = @"Assets\Services\EventService\Extensions\Events\Interfaces";
        private const string ConcretePath = @"Assets\Services\EventService\Extensions\Events";
        private const string HandlerPath = @"Assets\Services\EventService\EventHandlers";

        [MenuItem("CustomTools/Create Event")]
        private static void CreateEvent()
        {
            Debug.Log("CONTEXT!");

            var interfaceText = File.ReadAllText(InterfaceTemplatePath);
            interfaceText = interfaceText.Replace("%%", "PlayerCharacterDataUpdate");
            interfaceText = interfaceText.Replace("$$", "playerCharacterDataUpdate");
            var newInterfacePath = @$"{InterfacePath}\IPlayerCharacterDataUpdateEvent.cs";
            
            File.WriteAllText(newInterfacePath,interfaceText);
        }
    }
}

#endif