using System;
using System.Collections.Generic;
using UnityEngine;


// This is staple code, you can find it easily on the internet. It's a simple service locator to use instead of a full fledged dependency injection.
// Of course a dependency injection would be better - Less coupling, more extendable and reliable (Can decorate stuff, etc). Especially if it's a on-market solution such as zenject or something
// But since this is a simple test I rather avoid using market solutions and let all of the code transparent
namespace ServiceLocator
{
    /// <summary>
    /// Simple service locator for <see cref="IGameService"/> instances.
    /// </summary>
    public class Locator
    {
        private Locator() { }

        /// <summary>
        /// currently registered services.
        /// </summary>
        private readonly Dictionary<string, IGameService> services = new Dictionary<string, IGameService>();

        /// <summary>
        /// Gets the currently active service locator instance.
        /// </summary>
        public static Locator Current { get; private set; }

        /// <summary>
        /// Initalizes the service locator with a new instance.
        /// </summary>
        public static void Initialize()
        {
            Current = new Locator();
        }

        /// <summary>
        /// Gets the service instance of the given type.
        /// </summary>
        /// <typeparam name="T">The type of the service to lookup.</typeparam>
        /// <returns>The service instance.</returns>
        public T Get<T>() where T : IGameService
        {
            string key = typeof(T).Name;
            if (!services.ContainsKey(key))
            {
                Debug.LogError($"{key} not registered with {GetType().Name}");
                throw new InvalidOperationException();
            }

            return (T)services[key];
        }

        /// <summary>
        /// Registers the service with the current service locator.
        /// </summary>
        /// <typeparam name="T">Service type.</typeparam>
        /// <param name="service">Service instance.</param>
        public void Register<T>(T service) where T : IGameService
        {
            string key = typeof(T).Name;
            if (services.ContainsKey(key))
            {
                Debug.LogError($"Attempted to register service of type {key} which is already registered with the {GetType().Name}.");
                return;
            }

            services.Add(key, service);
        }

        /// <summary>
        /// Unregisters the service from the current service locator.
        /// </summary>
        /// <typeparam name="T">Service type.</typeparam>
        public void Unregister<T>() where T : IGameService
        {
            string key = typeof(T).Name;
            if (!services.ContainsKey(key))
            {
                Debug.LogError($"Attempted to unregister service of type {key} which is not registered with the {GetType().Name}.");
                return;
            }

            services.Remove(key);
        }
    }
}