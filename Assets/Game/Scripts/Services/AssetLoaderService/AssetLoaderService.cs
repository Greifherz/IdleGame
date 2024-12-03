using System;
using System.Collections;
using System.Collections.Generic;
using ServiceLocator;
using Services.TickService;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.XR;

namespace Game.Services.AssetLoaderService
{
    public class AssetLoaderService : IAssetLoaderService
    {
        private ITickService _tickService;

        ~AssetLoaderService()
        {
            
        }
        
        public void Initialize()
        {
            _tickService = Locator.Current.Get<ITickService>();
        }

        public AsyncOperationHandle<T> LoadAssetAsyncWithHandle<T>(string assetName)
        {
            return Addressables.LoadAssetAsync<T>(assetName);
        }

        public void LoadAssetAsync<T>(string assetName, Action<T> onLoaded)
        {
            var Handle = Addressables.LoadAssetAsync<T>(assetName);
            // Handle.ReleaseHandleOnCompletion();
            _tickService.RunCoroutine(AssetLoadWait(Handle, onLoaded));
        }

        private IEnumerator AssetLoadWait<T>(AsyncOperationHandle<T> handle,Action<T> onLoaded)
        {
            yield return handle;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                onLoaded(handle.Result);
            }
            else
            {
                Debug.LogError($"Loading of asset {handle.DebugName} failed - Return status - {handle.Status}");
            }
        }
    }
}