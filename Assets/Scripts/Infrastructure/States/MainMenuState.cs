using Scripts.Services;
using UnityEngine;
using Scripts.UI.Markers;

namespace Scripts.Infrastructure.States
{
    public class MainMenuState : IPayloadedState<ConfigData>
    {
        private const  string MAIN_MENU_SCENE_NAME = "MainMenu";

        private readonly GameStateMachine _stateMachine;
        private readonly ISceneLoaderService _sceneLoader;   
        private readonly IGameFactoryService _gameFactory;
        private readonly IInputManagerService _inputManager;

        private ConfigData _configData;
        
        public MainMenuState
        (
            GameStateMachine gameStateMachine,          
            ISceneLoaderService sceneLoader,
            IGameFactoryService gameFactoryService,
            IInputManagerService inputManager
        )
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactoryService;
            _inputManager = inputManager;
        }

        public void Enter(ConfigData configData)
        {
            Debug.Log("MainMenuState State");   
            
            _gameFactory.Cleanup();
            
            _configData = configData;

            _sceneLoader.Load(MAIN_MENU_SCENE_NAME, OnLoaded);
        }

        public void Exit()
        {    
           
        }
     
        private async void OnLoaded()
        {          
            MainMenuButtons mainMenu = await _gameFactory.CreateMainMenu();            
            
            _inputManager.SwitchToKeyboard();

            mainMenu.exitButton.onClick.AddListener(() => Application.Quit());
            mainMenu.keyboardButton.onClick.AddListener(() => _inputManager.SwitchToKeyboard());
            mainMenu.gamePadButton.onClick.AddListener(() => _inputManager.SwitchToGamepad());

            mainMenu.playButton.onClick.AddListener(() =>_stateMachine.Enter<LoadLevelState,ConfigData>(_configData));
            
            _stateMachine.Enter<GameLoopState>();
            _sceneLoader.HideLoadingCurtain();
        }
    }
}