using System;
using UnityEditor;
using UnityEngine;

namespace Bootstrap
{
    //This class might not be needed ever, but in case we need to initialize things using a monobehaviour it's here
    public class UnityBootstrapService : MonoBehaviour
    {
        private void Awake()
        {
            Startup();
        }

        private void Startup()
        {
            //Initialize Service Locator (Sort of Dependency Injection)
            //Initialize TickService
            //Initialize Persistence Service
            //Initialize EventService
        }
    }
}