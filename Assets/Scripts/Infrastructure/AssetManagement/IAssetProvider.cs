using System.Collections.Generic;
using System.Threading.Tasks;
using Scripts.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Scripts.Infrastructure.AssetManagement
{
  public interface IAssetProvider
  {
    void Initialize();
    Task<T> Load<T>(string address) where T : class;
    Task<GameObject> Instantiate(string address);
    Task<GameObject> Instantiate(string address, Vector3 at);
    void Release(string key);
    void Cleanup();
  }
}