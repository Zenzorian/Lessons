using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


namespace Scripts.Infrastructure.AssetManagement
{
   public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> _completedCashe = new Dictionary<string, AsyncOperationHandle>();
        private readonly Dictionary<string, List<AsyncOperationHandle>> _handles = new Dictionary<string, List<AsyncOperationHandle>>();
        private readonly object _lock = new object();

        public void Initialize()
        {
            Addressables.InitializeAsync();
        }

        public async Task<T> Load<T>(string address) where T : class
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentException("Asset address cannot be null or empty.", nameof(address));

            if (_completedCashe.TryGetValue(address, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T;

            try
            {
                AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(address);
                return await RunWithCacheOnComplete(handle, address);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load asset at {address}: {ex.Message}");
                return null;
            }
        }

        public async Task<GameObject> Instantiate(string address)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentException("Asset address cannot be null or empty.", nameof(address));

            try
            {
                AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(address);
                return await RunWithCacheOnComplete(handle, address);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to instantiate asset at {address}: {ex.Message}");
                return null;
            }
        }

        public async Task<GameObject> Instantiate(string address, Vector3 at)
        {
            if (string.IsNullOrEmpty(address))
                throw new ArgumentException("Asset address cannot be null or empty.", nameof(address));

            try
            {
                AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(address, at, Quaternion.identity);
                return await RunWithCacheOnComplete(handle, address);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to instantiate asset at {address}: {ex.Message}");
                return null;
            }
        }
       
        public void Release(string key)
        {
            if (_handles.TryGetValue(key, out var resourceHandles))
            {
                foreach (var handle in resourceHandles)
                {
                    if (handle.IsValid())
                        Addressables.Release(handle);
                }
                _handles.Remove(key);
            }

            if (_completedCashe.ContainsKey(key))
            {
                _completedCashe.Remove(key);
            }
        }

        public void Cleanup()
        {
            foreach (var resourceHandles in _handles.Values)
            {
                foreach (var handle in resourceHandles)
                {
                    if (handle.IsValid())
                        Addressables.Release(handle);
                }
            }

            _completedCashe.Clear();
            _handles.Clear();
        }

        private async Task<T> RunWithCacheOnComplete<T>(AsyncOperationHandle<T> handle, string cacheKey) where T : class
        {
            handle.Completed += completeHandle =>
            {
                lock (_lock)
                {
                    _completedCashe[cacheKey] = completeHandle;
                }
            };

            AddHandle<T>(cacheKey, handle);
            return await handle.Task;
        }
      
        private void AddHandle<T>(string key, AsyncOperationHandle handle) where T : class
        {
            lock (_lock)
            {
                if (!_handles.TryGetValue(key, out List<AsyncOperationHandle> resourceHandles))
                {
                    resourceHandles = new List<AsyncOperationHandle>();
                    _handles[key] = resourceHandles;
                }

                resourceHandles.Add(handle);
            }
        }
    }
}

