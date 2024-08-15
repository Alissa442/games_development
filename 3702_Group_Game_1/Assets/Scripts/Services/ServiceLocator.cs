using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

/// <summary>
/// A simple service locator to store and retrieve services
/// </summary>
[CreateAssetMenu(fileName = "ServiceLocator", menuName = "ScriptableObjects/Service Locator")]
public class ServiceLocator : ScriptableObject
{
    public Dictionary<string, object> services = new Dictionary<string, object>();

    [SerializeField]
    [ReadOnly]
    private int amountOfServices = 0;

    private static ServiceLocator instance;

    // Lazy Loading Singleton pattern inside a SCRIPTABLE OBJECT!
    public static ServiceLocator Instance
    {
        get 
        {
            // Lazy loading. If it's not
            if (ServiceLocator.instance == null)
            {
                instance = Resources.Load<ServiceLocator>("ServiceLocator");
            }
            return instance;
        }
        private set { instance = value; }
    }

    /// <summary>
    /// Register a service into the Service Locator
    /// </summary>
    /// <param name="serviceName">The name of the service.</param>
    /// <param name="service">An object being the service.</param>
    public void RegisterService(string serviceName, object service)
    {
        if (!services.ContainsKey(serviceName))
        {
            services[serviceName] = service;
            amountOfServices++;
        }
    }

    /// <summary>
    /// Unregisters a service from the service locator
    /// </summary>
    /// <param name="serviceName"></param>
    public void UnregisterService(string serviceName) {
        if (services.ContainsKey(serviceName))
        {
            services.Remove(serviceName);
            amountOfServices--;
        }
    }

    /// <summary>
    /// Get a service from the service locator
    /// To get a service call GetService<T>("ServiceName")
    /// Where T is the class/type of the service
    /// </summary>
    /// <typeparam name="T">Type/Class of the service</typeparam>
    /// <param name="serviceName">The name of the service</param>
    /// <returns>The service cast as type T</returns>
    public T GetService<T>(string serviceName) where T : ScriptableObject
    {
        if (services.ContainsKey(serviceName))
        {
            return services[serviceName] as T;
        }
        return null;
    }

    // TODO DELETE THIS
    ///// <summary>
    ///// Sets the instance of the service locator
    ///// This way, anyone can access the service locator by calling ServiceLocatorSO.instance
    ///// </summary>
    //private void OnEnable()
    //{
    //    ServiceLocator.instance = this;
    //    services = new Dictionary<string, object>();
    //    amountOfServices = 0;
    //}

    ///// <summary>
    ///// Unsets the instance of the service locator
    ///// and clears the dictionary.
    ///// </summary>
    //private void OnDisable()
    //{
    //    ServiceLocator.instance = null;
    //    services.Clear();
    //}

}
