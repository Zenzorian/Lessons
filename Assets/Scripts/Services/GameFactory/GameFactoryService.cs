using System.Threading.Tasks;
using Scripts.Infrastructure.AssetManagement;
using UnityEngine;      
using Scripts.ScriptableObjects;
using Scripts.UI.Markers;
using Scripts.Logic;
using System;

namespace Scripts.Services
{
    public class GameFactoryService : IGameFactoryService
    { 
        private const float CHARACTER_Y_OFFSET = 5f;

        private readonly IAssetProvider _assets;       
        private readonly IInputManagerService _inputManager;       
                  
        public GameFactoryService
        (
            IAssetProvider assets,            
            IInputManagerService inputManager           
        )
        {
            _assets = assets;           
            _inputManager = inputManager;               
        }
            
        public async Task<MainMenuButtons> CreateMainMenu()
        {           
            GameObject mainMenu = await _assets.Instantiate(AssetAddress.MainMenuPath);
            return mainMenu.GetComponent<MainMenuButtons>();           
        }
        
        public async Task<GameObject> CreateHud()
        {  
            GameObject hud = await _assets.Instantiate(AssetAddress.HudPath);
            return hud;          
        }
        
        public async Task<GameObject> CreateCharacter(Vector3 at, CharacterConfig characterConfig)
        {  
           
            Vector3 spawnPosition = new Vector3(at.x, at.y + CHARACTER_Y_OFFSET, at.z);
            GameObject characterObject = await _assets.Instantiate(AssetAddress.CharacterPath, spawnPosition);            
            characterObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                
            Character character = characterObject.GetComponent<Character>();
            if (character == null)
            {
                throw new NullReferenceException("Character component not found on instantiated object");
            }

            character.Construct(_inputManager, characterConfig);

            Camera camera = null;// = GameObject.FindAnyObjectByType<Camera>();
            if (camera == null)
            {
                throw new NullReferenceException("Camera not found in scene");
            }

            camera.gameObject.AddComponent<CameraFollow>().target = characterObject.transform;

            return characterObject;
           
        }

        public void Cleanup()
        {           
            _assets.Cleanup();        
        }              
    }
}