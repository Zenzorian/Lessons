using UnityEngine;
using Zenject;
using Scripts.Services;

namespace Scripts.Infrastructure.States
{
    public class GameLoopState : IState
    {   
        private readonly GameStateMachine _gameStateMachine;        
        private readonly ISceneLoaderService _sceneLoader;       

        public GameLoopState(GameStateMachine stateMachine, ISceneLoaderService sceneLoader)
        {          
            _gameStateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Exit()
        {            
            _sceneLoader.ShowLoadingCurtain();            
        }

        public void Enter()
        {
            Debug.Log("Game Loop State");  
        }
       
    }
}