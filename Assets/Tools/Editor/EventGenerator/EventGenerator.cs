#if UNITY_EDITOR

using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Tools.EventGenerator
{
    public class EventGenerator : EditorWindow
    {
        private const string InterfaceTemplatePath = @"Assets\Tools\Editor\Templates\EventInterface.txt";
        private const string ConcreteTemplatePath = @"Assets\Tools\Editor\Templates\EventConcrete.txt";
        private const string HandlerTemplatePath = @"Assets\Tools\Editor\Templates\EventHandler.txt";

        private const string InterfacePath = @"Assets\Services\EventService\Extensions\Events\Interfaces";
        private const string ConcretePath = @"Assets\Services\EventService\Extensions\Events";
        private const string HandlerPath = @"Assets\Services\EventService\EventHandlers";

        private const string EnumPath = @"Assets\Services\EventService\Extensions\Events\EventType.cs";

        private static string InputClassName;

        private static void CreateEvent()
        {
            Debug.Log($"Creating {InputClassName}Event");
            var ClassName = InputClassName;
            var CamelClassName = ClassName.Substring(0, 1).ToLower() + ClassName.Substring(1, ClassName.Length - 1);

            var EnumText = File.ReadAllText(EnumPath);
            var EnumFlagNumber = int.Parse(EnumText.Split("##")[1]);
            EnumText = EnumText.Replace(@$"//##{EnumFlagNumber}##//", @$"{InputClassName} = {(EnumFlagNumber*2)},{Environment.NewLine}        //##{EnumFlagNumber*2}##//");
            File.WriteAllText(EnumPath,EnumText);
            
            var InterfaceText = File.ReadAllText(InterfaceTemplatePath);
            InterfaceText = InterfaceText.Replace("%%", ClassName);
            InterfaceText = InterfaceText.Replace("$$", CamelClassName);
            
            var ConcreteText = File.ReadAllText(ConcreteTemplatePath);
            ConcreteText = ConcreteText.Replace("%%", ClassName);
            ConcreteText = ConcreteText.Replace("$$", CamelClassName);
            
            var HandlerText = File.ReadAllText(HandlerTemplatePath);
            HandlerText = HandlerText.Replace("%%", ClassName);
            HandlerText = HandlerText.Replace("$$", CamelClassName);
            
            var NewInterfacePath = @$"{InterfacePath}\I{ClassName}Event.cs";
            var NewConcretePath = @$"{ConcretePath}\{ClassName}Event.cs";
            var NewHandlerPath = @$"{HandlerPath}\{ClassName}EventHandler.cs";
            
            File.WriteAllText(NewInterfacePath,InterfaceText);
            File.WriteAllText(NewConcretePath,ConcreteText);
            File.WriteAllText(NewHandlerPath,HandlerText);
            Debug.Log($"{InputClassName}Event created.");
        }


        [MenuItem("Code Generator/Create Event")]
        private static void ShowWindow()
        {
            EventGenerator window = CreateInstance<EventGenerator>();
            window.position = new Rect(Screen.width / 2, Screen.height / 2, 400, 150);
            window.ShowUtility();
        }
        
        void OnGUI()
        {
            GUILayout.Space(10);
            EditorGUILayout.LabelField("Name of the event to be created", EditorStyles.wordWrappedLabel);
            GUILayout.Space(10);
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Event Name:",GUILayout.Width(80));
            InputClassName = EditorGUILayout.TextField(InputClassName,GUILayout.Width(295));
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(60);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Create")) {
                CreateEvent();
                Close();
            }
            if (GUILayout.Button("Cancel")) Close();
            EditorGUILayout.EndHorizontal();
        }
    }
}

#endif