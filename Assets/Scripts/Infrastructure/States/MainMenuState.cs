using Scripts.Services;
using UnityEngine;

namespace Scripts.Infrastructure.States
{
    public class MainMenuState : IState
    {
        private const  string MAIN_MENU_SCENE_NAME = "MainMenu";

        private readonly GameStateMachine _stateMachine;
        private readonly ISceneLoaderService _sceneLoader;            

        public MainMenuState
        (
            GameStateMachine gameStateMachine,          
            ISceneLoaderService sceneLoader           
        )
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
           
        }

        public void Enter()
        {
            Debug.Log("MainMenuState State");   

            _sceneLoader.Load(MAIN_MENU_SCENE_NAME, OnLoaded);
        }

        public void Exit()
        {    
           
        }
     
        private void OnLoaded()
        {             
            _stateMachine.Enter<GameLoopState>();
            _sceneLoader.HideLoadingCurtain();
        }
    }
}