using System;
using ServiceLocator;

public interface ISceneDataService : IGameService
{
    /// <summary>
    /// Stores a data payload, using its own type as the key.
    /// Overwrites any existing data of the same type.
    /// </summary>
    void SetData<T>(T data) where T : class;

    /// <summary>
    /// Retrieves the data payload for a specific type and immediately clears it.
    /// Returns null if no data of that type is found.
    /// </summary>
    T GetData<T>() where T : class;

    /// <summary>
    /// Checks if data for a specific type exists without consuming it.
    /// </summary>
    bool HasData<T>() where T : class;
}