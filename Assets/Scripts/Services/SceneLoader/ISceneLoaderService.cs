using System;

namespace Scripts.Services
{
    public interface ISceneLoaderService
    {
        void Load(string name, Action onLoaded = null);
        void ShowLoadingCurtain();
        void HideLoadingCurtain();
    }
}