using System.Threading.Tasks;
using UnityEngine;
using Scripts.ScriptableObjects;
using Scripts.UI.Markers;

namespace Scripts.Services
{
    public interface IGameFactoryService
    {   
        Task<GameObject> CreateHud();
        Task<MainMenuButtons> CreateMainMenu();       
        Task<GameObject> CreateCharacter(Vector3 at, CharacterConfig characterConfig);
              
        void Cleanup();
    }
}