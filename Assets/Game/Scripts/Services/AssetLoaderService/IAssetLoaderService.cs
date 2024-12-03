using System;
using ServiceLocator;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Game.Services.AssetLoaderService
{
    public interface IAssetLoaderService : IGameService
    {
        AsyncOperationHandle<T> LoadAssetAsyncWithHandle<T>(string assetName);
        void LoadAssetAsync<T>(string assetName, Action<T> onLoaded);
    }
}