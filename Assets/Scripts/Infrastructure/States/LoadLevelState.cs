using Scripts.Services;
using UnityEngine;

namespace Scripts.Infrastructure.States
{
    public class LoadLevelState : IState
    {
        private const string LEVEL_SCENE_NAME = "Level";
        
        private readonly GameStateMachine _stateMachine;

        private readonly ISceneLoaderService _sceneLoader;   

        public LoadLevelState
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
            Debug.Log("Load Level State");
          
            _sceneLoader.Load(LEVEL_SCENE_NAME , OnLoaded);          
        }

        public void Exit()
        {         
        }
     
        private void OnLoaded()
        { 
            _sceneLoader.HideLoadingCurtain();

            _stateMachine.Enter<GameLoopState>();                       
        }      
    }
}